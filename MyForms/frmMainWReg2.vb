Imports System.Data.SqlClient
Imports System.Text

'keep cboRegistrant and cboEvent unbound; use goto lbl fields to bind
Public Class frmMainWReg2

    Dim bDelete, bCancelClose As Boolean
    Public isLoaded As Boolean = False
    Dim bDup As Boolean = False 'check forduplicate entries
    Dim iDD As Integer 'reset combo
    Dim arPayee As String() = {"----- Order from this individual -----", "Cash", "Check", "Credit(Card)", "Will pay at event", "Unpaid"}
    Dim arMember As String() = {"----- Registration Fee for this individual covered by -----", "Coupon", "Free Event", "Group(Registration)", "Host(Congregation)", "Other (describe in Notes field)"}
    '===== for Detail form  closing routines
    Dim ctlIdentify As Control 'fields for delete and messages
    Dim ctlNeutral As Control 'will never be invalidated
    Dim objHowClose As Short ' structCloseMethod 'Object = Me.btnSaveExit 'identify object calling close
    Dim mainDS As DataSet 'for generic module calls like CloseDetailForm
    Dim mainTopic As String 'name of entity of this form - case, contact, etc
    Dim mainDAdapt As SqlDataAdapter
    Dim mainTbl As DataTable
    Public ThisID, LocalEventID As Integer
    Dim mainBSrce As System.Windows.Forms.BindingSource
    Dim iOrig As Integer

#Region "Initialize"

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
        Forms.Remove(Me)
    End Sub

#End Region

#Region "Load"

    'LOAD
    Private Sub frmMainWReg2_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles Me.Load
        Me.SuspendLayout()
        SetStatusBarText("loading")

SetMainDSConnection:
        Me.MainEventReg2TableAdapter1.Connection = sc

SetDefaults:
        ctlIdentify = Me.cboRegistrant 'Me.fldContactNum
        ctlNeutral = Me.btnHelp 'Me.btnRefreshContacts
        mainDS = Me.dsMainEventReg2
        mainTopic = "Registration"
        mainBSrce = MainEventReg2BindingSource
        mainTbl = Me.dsMainEventReg2.MainEventReg2
        ' mainDAdapt = no data adapter on this form - uses tableadapter directly

LoadCombos:
        If tblRegistrant.Rows.Count > 0 Then
        Else
            modGlobalVar.LoadRegistrantDD(False)
        End If
        Me.cboRegistrant.DataSource = tblRegistrant
        modGlobalVar.LoadWEventDD("all") '"upcoming")
        Me.cboEvent.DataSource = tblWEvents

FormSetup:
        If usr = ITDir.StaffID Or usr = DBAdmin.StaffID Or usr = PayPalAdmin.StaffID Then
            Me.lblGotoRefund.Enabled = True
            Me.btnRequestRefund.Visible = True
        Else
            Me.lblGotoRefund.Enabled = False
            Me.btnRequestRefund.Visible = False
        End If

        Forms.Add(Me)
        Me.ResumeLayout()
        isLoaded = True
        SetStatusBarText("Done ")
    End Sub

    'REFRESH DATA, COMBOS, AND GRIDS
    Public Sub ReLoad()  '
        SetStatusBarText("Reloading")

ResetVars:
        objHowClose = ObjClose.btnSaveExit
        Me.StatusBarPanelID.Text = mainTopic & " ID: " & ThisID.ToString
        Me.cboEvent.SelectedValue = Me.fldEventNum.Text
        iOrig = mainTbl.Rows(0)("ContactNum")

        SetStatusBarText("Done")
    End Sub

    
#End Region 'load

#Region "Update Main"

    'SAVE & EXIT
    Private Sub miSaveExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnSaveExit.Click
        objHowClose = ObjClose.btnSaveExit
        Me.Close()
    End Sub

    'ALLOW CLOSE WITHOUT SAVING
    Protected Sub miCloseForm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles miClose.Click
        MouseWait()
        ctlNeutral.Focus()
        mainBSrce.EndEdit()
        objHowClose = AskAcceptChanges(mainDS, mainTopic)
        Me.Close()
        MouseDefault()
    End Sub

    'CLOSING
    Private Sub frm_Closing(ByVal sender As Object, ByVal e As FormClosingEventArgs) _
         Handles MyBase.FormClosing

        Dim ctl As Control
        Dim arCtls() As Control

CallUpdate:
        If objHowClose = ObjClose.DontSaveClose Or objHowClose = ObjClose.cancelClose Then
            GoTo CheckReqdFlds
        End If
        If UpdateDB() Then
            e.Cancel = False
        Else
            e.Cancel = True 'don't close form
            GoTo ReleaseForm
        End If

CheckReqdFlds:  'check required fields; allow user to leave anyway if used menu
        Select Case objHowClose
            Case Is = ObjClose.DontSaveClose, ObjClose.miDelete
                e.Cancel = False
                GoTo ReleaseForm
            Case Is = ObjClose.cancelClose
                e.Cancel = True
                GoTo ReleaseForm
            Case Else 'btnSaveExit, SaveClose,, ObjClose.noChanges
                arCtls = CheckRequired()
                If arCtls.GetLength(0) > 1 Then 'required info missing
                    ctl = arCtls(0)
                    'INSERT DEFAULT DATA
                    If objHowClose = ObjClose.SaveClose Or e.CloseReason = System.Windows.Forms.CloseReason.UserClosing Then
                        If ctl Is ctlIdentify Then
                            'TODO REGISTRANT IS MISSING
                        End If
                        '    ctl.Text = usrName & " " & Today.ToShortDateString
                        '    mainBSrce.EndEdit()
                        '    mainDAdapt.Update(mainDS) 'save default data
                        'End If
                    End If
                    Dim strbListFields As New StringBuilder
                    For x As Integer = 0 To arCtls.GetLength(0) - 2
                        strbListFields.Append(", " & arCtls(x).Tag)
                    Next
                    e.Cancel = Not (modGlobalVar.AskCloseWithMissingInfo(objHowClose, ctl, strbListFields.ToString.Substring(2)))
                Else
                    e.Cancel = False
                End If
        End Select

ReleaseForm:  'TODO ? is this still necessary?
        If e.Cancel = False Then   'user OKs close form
            ClassOpenForms.frmMainWReg2 = Nothing 'reset global var
        Else
        End If
    End Sub

    'UPDATE
    Public Function UpdateDB() As Boolean
        MouseWait()
        SetStatusBarText("Updating server")

            Me.MainEventReg2BindingSource.EndEdit()
            'check req'd fields: contactnum, eventnum, ordernum
            Try
                Me.MainEventReg2TableAdapter1.Update(Me.dsMainEventReg2.MainEventReg2)
                UpdateDB = True
            Catch ex As Exception
                modGlobalVar.msg("ERROR: save", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
                UpdateDB = False
            End Try
CloseAll:
            SetStatusBarText("Update routine complete")
            MouseDefault()
    End Function

    'CHECK REQUIRED FIELDS w Error Provider
    Private Function CheckRequired() As Control()
        'add the Neutral control to the array last to indicate rest are ok if its the first one
        Dim arCtls(0) As Control
        Dim ctl As Control
        Dim i As Integer = 0

        If CheckNameChange() = True Then
        Else
            ctl = ctlIdentify
            Me.ErrorProvider1.SetError(ctl, "please enter a " & IsNull(ctl.Tag, ctl.Name))
            arCtls(i) = ctl
            i = i + 1
            ReDim Preserve arCtls(arCtls.GetUpperBound(0) + 1)
        End If

        arCtls(i) = ctlNeutral
        Return arCtls

    End Function 'reqd fields

#End Region 'update

#Region "Validating"

    'CONTACT ie REGISTRANT IS REQUIRED
    Private Function CheckNameChange() As Boolean
        'Janice accidentally changes contact, perhaps through mouse scroll??  add warning here.
        If Me.fldContactNum.Text = String.Empty Or Me.fldContactNum.Text = "0" Then
            Return False
        End If
        If iOrig = Me.cboRegistrant.SelectedValue Then
            Return True
        Else
            If modGlobalVar.msg("STOP  - did you intend to change the registrant?", iOrig.ToString & NextLine & Me.cboRegistrant.SelectedValue, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                Return True
            Else
                Return False
            End If
        End If
    End Function

    'ASSIGN EVENT NUM based on non-bound event combo. call check duplicate registration
    Private Sub cboEvent_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles cboEvent.SelectionChangeCommitted
        If Not isLoaded Then
            Exit Sub
        End If
        If Me.cboEvent.SelectedIndex = -1 Then
            Exit Sub
        End If

        'CONFIRM change is intended
        Select Case Me.fldEventNum.Text
            Case Is = String.Empty, "0", Me.cboEvent.SelectedValue
            Case Else
                If MsgBox("Click Yes to change Event to " & NextLine & Me.cboEvent.Text, 36, "WARNING - EVENT HAS CHANGED") = MsgBoxResult.Yes Then
                    'ASSIGN VALUE to bound field
                    Me.fldEventNum.Text = Me.cboEvent.SelectedValue
                    'VERIFY is unique contactNum + eventNum
                    CheckDuplicateRegistration()
                End If
        End Select

    End Sub

    'CALL check duplicate registration
    Private Sub cboRegistrant_SelectionChangeCommitted(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles cboRegistrant.SelectionChangeCommitted
        CheckDuplicateRegistration()
        Me.ErrorProvider1.SetError(sender, "")
    End Sub

    'CHECK FOR DUPLICATE REGISTRATION before update is called, RELOAD ORG
    Private Sub CheckDuplicateRegistration()
        If Not isLoaded Then
            Exit Sub
        End If
        If Me.cboRegistrant.SelectedIndex = -1 Then
            Exit Sub
        End If
        If String.IsNullOrEmpty(Me.fldEventNum.Text) Then
            Exit Sub
        End If
        '--------------
        Dim iReg As Integer
        Dim iEvent As Integer
        iEvent = Me.fldEventNum.Text

        Dim cmdR As New SqlClient.SqlCommand("SELECT RegistrationID FROM tblEventReg2 WHERE (EventNum = " &
            iEvent & ") AND (ContactNum = " & Me.cboRegistrant.SelectedValue &
            ")  AND (Notes is null or Notes not like 'Delete%')", sc)
        If Not SCConnect() Then
            Exit Sub
        End If
        iReg = cmdR.ExecuteScalar
        sc.Close()
        cmdR.Dispose()

        Select Case IsNull(iReg, 0)
            Case Is = ThisID 'found same one
            Case Is = 0 'no duplicate registrations
                'DisplayOrgNum
                Try
                    ' modGlobalVar.Msg(dtRegistrant.Rows(cboRegistrant.SelectedIndex)("OrgNum"), , "orgnum Committed")
                    Me.fldOrgNum.Text = tblRegistrant.Rows(cboRegistrant.SelectedIndex)("OrgNum")
                Catch ex As Exception
                End Try
                'does this take too much time for offsite folks?
                'DisplayMailingAddress
            Case Else
                modGlobalVar.msg("STOP  -  see Registration# " & iReg.ToString, "Registration already exists for this person for this event and this one will not be saved", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Me.cboEvent.Focus()
        End Select
    End Sub

#End Region 'validating

#Region "EDIT Buttons"

    'SAVE
    Private Sub miSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles miSave.Click

        CheckNameChange()
        UpdateDB()
    End Sub

    'DELETE    'todo didn't admins get permission to do real delete --no re online reference?
    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles miDelete.Click

        Select Case modGlobalVar.msg("CONFIRM DELETE", mainTopic & ": " + IsNull(ctlIdentify.Text, "") & NextLine & " will be marked for deletion and the window closed.", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            Case Is = DialogResult.Yes
                Me.rtbNotes.Text = "DELETE: " & IsNull(rtbNotes.Text, "")
                objHowClose = ObjClose.miDelete
                mainBSrce.EndEdit()
                Me.Close()
            Case Else   'do nothing
        End Select
    End Sub

    'CANCEL CHANGES
    Private Sub miCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) _
            Handles miCancel.Click
        mainBSrce.CancelEdit()
    End Sub

    'COPY 
    Private Sub miCopy_Click(ByVal sender As Object, ByVal e As System.EventArgs) _
        Handles miCopy.Click
        Dim frm As New frmAddNew
        modGlobalVar.HideTabPage("tbRegistrationCopy", frm.TabControl1)
        frm.TabControl1.SelectedTab = frm.tbRegistrationCopy
        frm.Text = "Copy Registration to Additional Event"
        frm.lblRegistrationDetail.Text = fldOrderNum.Text & " - " & Me.cboRegistrant.Text
        frm.lblRegIDtoCopy.Text = ThisID
        frm.LoadEventDD() 'Me.fldEventNum.Text)
        frm.ShowDialog()
        ' GetNewMailFlag = frm.returnval
        frm = Nothing
        '  modGlobalVar.msg("not available", "ask " & DBAdmin.StaffName & "for assistance.", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

#End Region

#Region "General"

    'SET STATUS BAR LEFT TEXT
    Private Sub SetStatusBarText(ByVal str As String)
        Me.StatusBarPanel1.Text = str
    End Sub

    'COPY CURRENT ID
    Private Sub StatusBar1_DoubleClick(sender As System.Object, e As System.EventArgs) Handles StatusBar1.DoubleClick
        Clipboard.SetText(ThisID)
    End Sub

    'UNDO
    Protected Overrides Function ProcessDialogKey(ByVal keyData As Keys) As Boolean
        If keyData = System.Windows.Forms.Keys.Escape Then
            modPopup.UndoCtl(Me.ActiveControl)
            Return True  ' True means we've processed the key
        Else
            Return MyBase.ProcessDialogKey(keyData)
        End If
    End Function

    'RIGHT CLICK MENU
    Private Sub textbox_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        If e.Button = System.Windows.Forms.MouseButtons.Right Then
            Dim pp As New ClassRtbContextMenu(sender)
            pp.Show(Me, PointToClient(Control.MousePosition))
        End If
    End Sub

    'HELP BUTTON
    Private Sub btnHelp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnHelp.Click, miHelp.Click
        modGlobalVar.Msg("Registration Help", "SELECT Contact Name from DropDown box.  Inactive names are indicated with ~ and are at the bottom of the list." & NextLine & NextLine & "FOR MULTIPLE REGISTRATIONS: click the menu item 'Multiple Event Registrations'" _
              & NextLine & "TO MAKE A PAYMENT: Double=click the green 'Order#', then click New --> Payment on the window that opens.", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    'REFRESH CONTACT DROPDOWN
    Private Sub btnRefreshContacts_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnRefreshContacts.Click, miRefreshContacts.Click

        Dim str As String = Me.cboRegistrant.Text

        modGlobalVar.LoadRegistrantDD(True)

        If IsNull(str, "") = String.Empty Then
        Else
            Try
                Me.cboRegistrant.SelectedIndex = Me.cboRegistrant.FindStringExact(str)
                'triggers duplicate record
            Catch ex As Exception
            End Try
        End If

    End Sub

#End Region 'general

#Region "Goto"

    'OPEN CONTACT DETAIL
    Private Sub lblGotoContact_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If Me.cboRegistrant.SelectedIndex > -1 Then
            ' If IsNull(Me.cboRegistrant.Selectedvalue, 0) = 0 Then
            Dim i As Integer = Me.cboRegistrant.Text.IndexOf("-")
            modGlobalVar.OpenMainContact(Me.cboRegistrant.SelectedValue, Me.cboRegistrant.Text, Me.cboRegistrant.Text.Substring(i + 2), 0)
        End If
    End Sub

    'OPEN PAYMENT DETAIL
    Private Sub fldPaymentNum_DoubleClick(sender As System.Object, e As System.EventArgs) _
        Handles fldPaymentNum.DoubleClick
        If Me.fldPaymentNum.Text = String.Empty Then
            Exit Sub
        Else
            modGlobalVar.OpenMainWPayment(Me.fldPaymentNum.Text)
        End If
    End Sub

    'OPEN REFUND DETAIL
    Private Sub lblGotoRefund_DoubleClick(sender As System.Object, e As System.EventArgs) _
        Handles lblGotoRefund.DoubleClick
        If Me.lblGotoRefund.Text = String.Empty Then
            Exit Sub
        Else
            modGlobalVar.OpenMainWPayment(Me.lblGotoRefund.Text)
        End If
    End Sub

    'OPEN EVENT DETAIL:
    Private Sub lblGotoevent_doubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles lblGotoEvent.DoubleClick
        If Me.cboEvent.SelectedIndex > -1 Then
            ' Dim i As Integer = Me.cboRegistrant.Text.IndexOf("-")
            '  MsgBox(lblGotoEvent.Text.Substring(i))
            modGlobalVar.OpenMainWEvent(Me.fldEventNum.Text, Me.cboEvent.Text, False) 'lblGotoEvent.Text.Substring(i), False)
        End If
    End Sub

    'TODO 2013 reestablish this
    'SHOW SERIES MULTIPLE EVENTS
    Private Sub miMultiple_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles miMultiple.Click
        ''1. open form showing events in this series, with any existing registrations indicated
        ''2. return event IDs from newly selected items from list
        ''3. run insert queries
        'Me.miSave.PerformClick()
        'Dim frm As New frmRegistrationSeries
        ''Dim dt, dt1 As Date
        ''If Me.txtPayDate.Text = String.Empty Then
        ''    dt = CType("1/1/1911", Date)
        ''Else
        ''    dt = CType(Me.txtPayDate.Text, Date)
        ''End If
        ''If Me.txtRegDate.Text = String.Empty Then
        ''    dt1 = CType("1/1/1911", Date)
        ''Else
        ''    dt1 = CType(Me.txtRegDate.Text, Date)
        ''End If

        ''GET CASE NAME (may have opened form from Contact OR Event)
        'Dim strCase As String
        'If Not SCConnect() Then
        '    Exit Sub
        'End If

        'Dim cmdE As New SqlClient.SqlCommand("SELECT MasterWorkshopName FROM tblEventSetup LEFT JOIN tblEventReg2 on EventID = EventNum WHERE tblEventReg2.contactnum = " & Me.fldContactNum.Text & " AND tblEventSetup.EventID = " & Me.fldEventNum.Text, sc)
        'strCase = cmdE.ExecuteScalar
        'sc.Close()
        'cmdE.Dispose()

        ''set selection parameters
        'frm.SqlDataAdapter1.SelectCommand.Parameters("@CaseName").Value = mdGlobalVar.GetWild(strCase, True)
        'frm.SqlDataAdapter1.SelectCommand.Parameters("@ContactNum").Value = Me.cboRegistrant.SelectedValue
        ''set insert parameters
        '' globalContactID = Me.cboRegistrant.SelectedValue
        ''     frm.SqlDataAdapter1.InsertCommand.Parameters("@RegDate").Value = dt1
        'frm.SqlDataAdapter1.InsertCommand.Parameters("@Fee").Value = IsNull(Me.txtTotOrder.Text, "")
        ''    frm.SqlDataAdapter1.InsertCommand.Parameters("@PayDate").Value = dt
        'frm.SqlDataAdapter1.InsertCommand.Parameters("@Clergy").Value = IsNull(Me.cboClergy.SelectedItem, "")
        'frm.SqlDataAdapter1.InsertCommand.Parameters("@Gender").Value = IsNull(Me.cboGender.SelectedItem, "")
        ''   frm.SqlDataAdapter1.InsertCommand.Parameters("@PayMethod").Value = IsNull(Me.cboPayment.SelectedItem, "")
        ''frm.SqlDataAdapter1.InsertCommand.Parameters("@Multiple").Value = Me.chkMultiple.Checked
        ''frm.SqlDataAdapter1.InsertCommand.Parameters("@Group").Value = Me.chkGroup.Checked
        'frm.SqlDataAdapter1.InsertCommand.Parameters("@Heard").Value = IsNull(Me.cboHeard.SelectedItem, "")
        'frm.SqlDataAdapter1.InsertCommand.Parameters("@Registered").Value = IsNull(Me.cboRegistered.SelectedItem, "")
        'frm.SqlDataAdapter1.InsertCommand.Parameters("@Veg").Value = Me.chkVeg.Checked
        'frm.SqlDataAdapter1.InsertCommand.Parameters("@Notes").Value = IsNull(Me.rtbNotes.Text, "")

        'frm.DsEventSeries1.EnforceConstraints = False
        'frm.SqlDataAdapter1.InsertCommand.Connection = sc
        'frm.SqlDataAdapter1.SelectCommand.Connection = sc
        'frm.SqlDataAdapter1.Fill(frm.DsEventSeries1)
        'frm.ShowDialog()
        'For x As Integer = 1 To colEventSeriesID.Count
        '    ' modGlobalVar.Msg(colEventSeriesID(x).ToString)
        'Next
    End Sub

    'OPEN ORDER OVERVIEW = Ordertab on Event Search
    Private Sub miGotoOrder_Click(sender As System.Object, e As System.EventArgs) _
        Handles miGotoOrder.Click, fldGotoOrder.DoubleClick

        OpenSrchWEvent(IsNull(Me.fldOrderNum.Text, 0), True)

    End Sub

    'OPEN EVENT DETAIL
    Private Sub miGotoEvent_click(sender As System.Object, e As System.EventArgs) _
        Handles miGotoEvent.Click
        modGlobalVar.OpenMainWEvent(Me.fldEventNum.Text, "event name", False)
    End Sub

#End Region 'goto

#Region "Payments"

    'REQUEST REFUND - TODO NOW move this to Payment Form
    Private Sub chkRefund_click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles chkRefund.Click
        Dim strAmount, strOrder As String
        If isLoaded And sender.CheckState = CheckState.Checked Then
            strOrder = Me.fldOrderNum.Text

            strAmount = InputBox("ENTER REFUND AMOUNT in space below.", "Opening Refund Request form", "")
            If strAmount = String.Empty Then
            Else
                Try
                    modPopup.MergeRefundRequest(ThisID, strAmount, strOrder)
                    ' System.Diagnostics.Process.Start("http://centerforcongregations.org/registrant-cancellationrefund")
                Catch ex As Exception
                    modGlobalVar.Msg("ERROR: Open Refund Form", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End If
        End If
    End Sub

#End Region 'refund

#Region "validation"
    'date validation
    Private Sub dtRegistration_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) _
        Handles DtRegistration.Validating, dtRefundRequest.Validating
        e.Cancel = modGlobalVar.ValidateDateA(sender, Me.ErrorProvider1)
    End Sub

#End Region 'validation

    Private Sub rtbNotes_TextChanged(sender As System.Object, e As System.EventArgs) Handles rtbNotes.TextChanged

    End Sub
    Private Sub NotesLabel_Click(sender As System.Object, e As System.EventArgs)

    End Sub
End Class

