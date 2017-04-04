Imports System.Data.SqlClient
Imports System.Text

Public Class frmMainGrantYMGI

    Public isLoaded As Boolean = False
    Dim isGrant As Boolean
    Dim tblContact As New DataTable("luContacts")
    ' Public bChanged As Boolean
    ' Dim bDelete As Boolean
    ' Dim bCancelClose As Boolean
    Public ThisID, LocalOrgID As Integer
    '===== for Detail form  closing routines
    Dim ctlIdentify As Control 'fields for delete and messages
    Dim ctlNeutral As Control 'will never be invalidated
    Dim objHowClose As Short ' structCloseMethod 'Object = Me.btnSaveExit 'identify object calling close
    Dim mainTopic As String 'name of entity of this form - case, contact, etc
    Dim mainDS As DataSet 'for generic module calls like CloseDetailForm
    Dim mainTbl As DataTable
    Dim mainBSrce As System.Windows.Forms.BindingSource


#Region "Load"
    'LOAD
    Private Sub frmMainGrantYMGI_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles MyBase.Load
        Me.SuspendLayout()
        SetStatusBarText("loading")

SetConnections:
        Me.tblGrantYMGITableAdapter.Connection = sc

SetDefaults:
        ctlIdentify = Me.txtRemarks
        ctlNeutral = Me.btnHelp
        mainTopic = "YMGI Grant Apps"
        mainDS = Me.DsMainGrantYMGI
        mainTbl = Me.DsMainGrantYMGI.tblGrantYMGI
        mainBSrce = Me.MainYMGIBindingSource



        '    mainTbl = Me.DsMainGrantYMGI.tblGrantYMGI
        '   cmgr = Me.BindingSource1.CurrencyManager
LoadStaffCombos:
        modGlobalVar.LoadStaffCombo(Me.cboCallNotified, False, StaffComboChoices.Selectable)
        modGlobalVar.LoadStaffCombo(Me.cboDeterminationStaff, False, StaffComboChoices.Selectable)

        'FormatFields:
        '        Dim ctl As Control
        '        Dim pnl As Control

        '        For Each pnl In Me.Controls
        '            If pnl.GetType.ToString = "System.Windows.Forms.Panel" Then
        '                For Each ctl In pnl.Controls  'Me.pnlApplication.Controls
        '                    If ctl.Name.Substring(0, 2) = "dt" Then
        '                        AddHandler ctl.DataBindings(0).Parse, AddressOf modGlobalVar.DateParse
        '                        ' AddHandler ctl.Leave, AddressOf LeaveDateField
        '                        ' FormatDateFld(ctl, "text", Me.DsMainGrantYMGI, "MainGrantYMGI." & ctl.Name.Substring(2, Len(ctl.Name) - 2))
        '                    End If
        '                Next ctl
        '            End If
        '        Next pnl



        '   Me.ResumeLayout()
        Forms.Add(Me)
        '  CloseButton.Disable(Me)
        isLoaded = True
        Me.ResumeLayout()
        SetStatusBarText("Done")

    End Sub

    Public Sub LoadOrgBasedCombos(ByVal orgid As Integer)

        Dim strsql As New SqlCommand("SELECT ContactID, ContactStaff FROM vwgetContacts where orgid = " & orgid, sc)
        Dim tblCase As New DataTable("tCase")
        Me.StatusBarPanelID.Text = mainTopic & " ID: " & ThisID.ToString

        If SCConnect() Then
            Dim dr As SqlDataReader = strsql.ExecuteReader()
            tblContact.Load(dr)

            'LOAD CASES
            strsql.CommandText = "[luCaseNames]"
            strsql.CommandType = CommandType.StoredProcedure
            strsql.Parameters.Add("OrgID", SqlDbType.Int)
            strsql.Parameters("OrgID").Value = orgid
            dr = strsql.ExecuteReader(CommandBehavior.CloseConnection)
            tblCase.Load(dr)
            dr.Close()
            dr = Nothing
        End If

DropDowns:
        Me.cboCongRep.DisplayMember = "ContactStaff"
        Me.cboCongRep.ValueMember = "ContactID"
        Me.cboCongRep.DataSource = tblContact.DefaultView

        Me.cboPD.DisplayMember = "ContactStaff"
        Me.cboPD.ValueMember = "ContactID"
        Me.cboPD.DataSource = tblContact

        '  Me.cboCase.DisplayMember = "CaseName"
        '  Me.cboCase.ValueMember = "CaseID"
        Me.cboCase.DataSource = tblCase.DefaultView

    End Sub

    'grant button visibility
    Public Sub GrantButtonVisible()
        '    If IsNull(mainTbl.Rows(0).Item("GrantAppRecdDt"), "1/1/1911") > "1/1/1911" Then
        If isLoaded Then
            ' 
            If IsNull(mainTbl.Rows(0).Item("GrantIDTxt"), "a").ToString = "a" Then  'grant does not exist yet
                'Me.btnGrant.Visible = False
                Me.btnGrant.Text = "Create Grant"
            Else
                'Me.btnGrant.Visible = True
                Me.btnGrant.Text = "Go to Grant"
            End If

        End If
    End Sub

    ''DISABLES X CLOSE BUTTON IN CONTROL BOX
    'Protected Overrides ReadOnly Property CreateParams() As CreateParams
    '    Get
    '        Dim cp As CreateParams = MyBase.CreateParams
    '        Const CS_NOCLOSE As Integer = &H200
    '        cp.ClassStyle = cp.ClassStyle Or CS_NOCLOSE
    '        Return cp
    '    End Get
    'End Property

#End Region 'load


#Region "Update Main"


    'SAVE & EXIT
    Private Sub btnSaveExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
          Handles btnSaveExit.Click
        objHowClose = ObjClose.btnSaveExit
        Me.Close()
    End Sub


    'CLOSING & ask user & data validation & Release Form
    Private Sub frm_Closing(ByVal sender As Object, ByVal e As FormClosingEventArgs) _
               Handles MyBase.FormClosing

        Dim arCtls(0) As Control
        '  Dim ctl As Control
        Dim bChanges As Boolean

LookForChanges:
        bChanges = modGlobalVar.AnyChanges(ctlNeutral, mainBSrce, mainTbl)

CheckCallingMethod:
        If objHowClose = ObjClose.miAskSave Then
            Select Case AskAcceptChanges(bChanges, mainTopic)
                Case Is = ObjClose.cancelClose
                    e.Cancel = True
                    GoTo ReleaseForm
                Case Is = ObjClose.DontSaveClose
                    ' GoTo validate ' notify of missing fields??
                    e.Cancel = False
                    GoTo ReleaseForm
                Case Is = ObjClose.SaveClose
                    e.Cancel = False
                    GoTo callupdate
            End Select
        ElseIf objHowClose = ObjClose.miDelete Then
            GoTo callupdate
        End If
SkipUpdate:  'so LastChangeDate doesn't get updated
        If bChanges = False Then
            e.Cancel = False
            GoTo ReleaseForm
        End If
CallUpdate:
        If DoUpdate() Then
            e.Cancel = False
        Else                'update didn't work
            e.Cancel = True 'don't close form
            GoTo ReleaseForm
        End If

VALIDATE:  'no required fields on this form
        Select Case objHowClose
            Case Is = ObjClose.DontSaveClose, ObjClose.miDelete
                e.Cancel = False
                '  GoTo ReleaseForm
            Case Is = ObjClose.cancelClose
                e.Cancel = True
                '  GoTo ReleaseForm
            Case Else 'btnSaveExit, SaveClose,, ObjClose.noChanges
                'no required fields on this form
                e.Cancel = False
        End Select

ReleaseForm:
        If e.Cancel = False Then   'user OKs close form
            ClassOpenForms.frmMainYMGI = Nothing 'reset global var
        Else
        End If
    End Sub

    'UPDATE
    Public Function DoUpdate() As Boolean
        Dim i As Integer

        MouseWait()
        mainBSrce.EndEdit() 'gets rid of proposed version

UpdateBackend:
        SetStatusBarText("Updating server 1")
        Try
            i = Me.tblGrantYMGITableAdapter.Update(mainTbl)

            DoUpdate = True
        Catch ex As Exception
            modGlobalVar.Msg("ERROR: updating " & mainTopic, ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
            DoUpdate = False
        Finally
            '  modGlobalVar.Msg("main: " & i.ToString& NextLine &  "addresses: " & f.ToString, , "updated")
        End Try

CloseAll:
        SetStatusBarText("Update routine complete")
        MouseDefault()
    End Function




    '    'CLOSING
    '    Private Sub frm_Closing(ByVal sender As Object, ByVal e As FormClosingEventArgs) _
    '     Handles MyBase.FormClosing
    '        Dim arCtls(0) As Control
    '        ' Dim ctl As Control
    '        ctlNeutral.Focus()  'safely calls endedit if was in edit mode

    'CallUpdate:
    '        If objHowClose = ObjClose.DontSaveClose Or objHowClose = ObjClose.cancelClose Then
    '            GoTo CheckRequiredFields
    '        End If

    '        If DoUpdate() Then
    '            e.Cancel = False
    '            '  modGlobalVar.Msg("back from update")
    '        Else
    '            ' modGlobalVar.Msg("back from update")
    '            e.Cancel = True 'don't close form
    '            GoTo ReleaseForm
    '        End If

    'CheckRequiredFields:  'check required fields; allow user to leave anyway if used menu
    '        Select Case objHowClose
    '            Case Is = ObjClose.DontSaveClose, ObjClose.miDelete
    '                e.Cancel = False
    '                GoTo ReleaseForm
    '            Case Is = ObjClose.cancelClose
    '                e.Cancel = True
    '                GoTo ReleaseForm
    '            Case Else
    '                'arCtls = CheckRequired()
    '                'If arCtls.GetLength(0) > 1 Then 'required info missing
    '                '    ctl = arCtls(0)
    '                '    If objHowClose = ObjClose.SaveClose Or e.CloseReason = System.Windows.Forms.CloseReason.UserClosing Then
    '                '        If ctl Is ctlIdentify Then  'insert default data
    '                '            ctl.Text = usrName & " " & Today.ToShortDateString
    '                '            mainBSrce.EndEdit()
    '                '            Me.tblGrantYMGITableAdapter.Update(mainTbl) 'save default data
    '                '        End If
    '                '    End If
    '                '    Dim strbListFields As New StringBuilder
    '                '    For x As Integer = 0 To arCtls.GetLength(0) - 2
    '                '        strbListFields.Append(", " & arCtls(x).Tag)
    '                '    Next
    '                '    e.Cancel = Not (mdGlobalVar.AskCloseWithMissingInfo(objHowClose, ctl, strbListFields.ToString.Substring(2)))
    '                'Else
    '                '    e.Cancel = False
    '                'End If
    '        End Select

    'ReleaseForm:
    '        If e.Cancel = False Then   'user OKs close form
    '            ClassOpenForms.frmMainYMGI = Nothing 'reset global var
    '            objHowClose = Nothing
    '        Else
    '        End If
    '        arCtls = Nothing
    '        MouseDefault()
    '    End Sub

    '    'UPDATE BACK END, return number of records updated, return false if error updating
    '    Public Function DoUpdate() As Boolean
    '        Dim i As Integer
    '        MouseWait()

    '        If mainDS.HasChanges Or
    '            mainDS.Tables(0).Rows(0).HasVersion(DataRowVersion.Proposed) Then
    '            mainBSrce.EndEdit()
    '        Else
    '            DoUpdate = True
    '            GoTo CloseAll
    '        End If

    '        '   mainBSrce.EndEdit() 'gets rid of proposed version
    '        If mainDS.HasChanges = True Then 'this catches delete, save, asksave, save/exit, anyclose
    '            'ASSIGN CHANGE DATE/STAFF
    '            'mainDAdapt.UpdateCommand.Parameters("@LastChangeStaffNum").Value = usr
    '            'mainDAdapt.UpdateCommand.Parameters("@LastChangeDate").Value = Now()

    'UpdateBackend:
    '            SetStatusBarText("Updating server")
    '            Try
    '                i = Me.tblGrantYMGITableAdapter.Update(mainDS)
    '                DoUpdate = True
    '            Catch ex As Exception
    '                modGlobalVar.Msg(ex.Message, MessageBoxIcon.Error, "Error updating " & mainTopic)
    '                DoUpdate = False
    '            End Try
    '        Else
    '            DoUpdate = True 'completed action though no updates to be made
    '        End If

    'CloseAll:
    '        SetStatusBarText("Update routine complete")
    '        MouseDefault()
    '    End Function


#End Region 'update main

#Region "Menu Items"

    'ALLOW CLOSE WITHOUT SAVING
    Private Sub miCloseForm_Click(ByVal sender As Object, ByVal e As System.EventArgs) _
        Handles miClose.Click
        objHowClose = ObjClose.miAskSave
        Me.Close()
    End Sub

    'mi SAVE
    Private Sub miSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles miSave.Click
        objHowClose = ObjClose.miSave
        DoUpdate()
    End Sub

    'CANCEL CHANGES
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles miCancel.Click

        Try
            mainBSrce.CancelEdit()
            mainDS.RejectChanges()
        Catch ex As System.Exception
            modGlobalVar.Msg("ERROR: cancelling changes ", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Me.StatusBarPanel1.Text = "Changes cancelled"
    End Sub

    'DELETE YMGI App
    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles miDelete.Click

        ' Select Case modGlobalVar.Msg(mainTopic & ": " + IsNull(ctlIdentify.Text, "") & NextLine & " will be marked for deletion and the window closed.", MessageBoxButtons.YesNo, "CONFIRM DELETE")
        If modGlobalVar.Msg("CONFIRM DELETE", mainTopic & ": " & IsNull(ctlIdentify.Text, "") &
 NextLine & " will be marked for deletion and the window closed.", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) = DialogResult.Yes Then
            ctlIdentify.Text = "DELETE: " & IsNull(ctlIdentify.Text, "")
            objHowClose = ObjClose.miDelete
            ' mainBSrce.EndEdit()
            Me.Close()
        End If

        '  Dim ctl As Control = Me.txtNotes
        'Select Case modGlobalVar.Msg("This YMGI application will be marked for deletion and the window closed.", MessageBoxButtons.YesNoCancel, "CONFIRM DELETE")
        '    Case Is = DialogResult.Yes
        '        ctl.Text = "DELETE: " & IsNull(ctl.Text, "")
        '        UpdateDB("delete")
        '        bDelete = True
        '        Me.btnSaveExit.PerformClick()
        '    Case Is = DialogResult.No
        '        bDelete = False
        '        Me.btnSaveExit.PerformClick()
        '    Case Else   'don't close if the cancel
        'End Select

    End Sub

    '    'ALLOW CLOSE WITHOUT SAVING
    '    Protected Sub miCloseForm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
    '     Handles miClose.Click

    '        MouseWait()
    '        ctlNeutral.Focus()
    '        If mainDS.Tables(0).Rows(0).HasVersion(DataRowVersion.Proposed) Or mainDS.HasChanges Then
    '            mainBSrce.EndEdit()
    '        End If

    '        objHowClose = AskAcceptChanges(mainDS, mainTopic)
    '        Me.Close()
    'CloseAll:
    '        MouseDefault()
    '    End Sub

#End Region

    Private Sub dtGrantDeadline_MouseDoubleClick(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles dtGrantDeadline.MouseDoubleClick
        Me.dtGrantDeadline.Text = ""
        mainBSrce.Current("GrantAppDeadlineDt") = DBNull.Value
        Me.rbJan.Checked = False
        Me.rbMarch.Checked = False
    End Sub 'edit


    Private Sub DateFields_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles dtAgreementMail.Validating,
    dtAgreementRecd.Validating, dtAppRecd.Validating, dtAppReceiptMail.Validating, dtClassConfirm.Validating, dtDetermination.Validating,
    dtGrantDeadline.Validating, dtGrantAppRecd.Validating, dtNotifyCall.Validating, dtNotifyMail.Validating
        e.Cancel = modGlobalVar.ValidateDateA(sender)
    End Sub

#Region "General"


    'SET STATUS BAR LEFT TEXT
    Private Sub SetStatusBarText(ByVal str As String)
        Me.StatusBar1.Panels(0).Text = str
    End Sub

    ' COPY CURRENT ID
    Private Sub StatusBar1_DoubleClick(sender As System.Object, e As System.EventArgs) Handles StatusBar1.DoubleClick
        Clipboard.SetText(ThisID)
    End Sub



    'UNDO
    Protected Overrides Function ProcessDialogKey(ByVal keyData As Keys) As Boolean
        If keyData = System.Windows.Forms.Keys.Escape Then
            modPopup.UndoCtl(Me.ActiveControl)
            '  bChanged = True
            Return True  ' True means we've processed the escape key
        Else
            Return MyBase.ProcessDialogKey(keyData)
        End If
    End Function

    'RIGHT CLICK MENU
    Private Sub textbox_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) _
         Handles txtRemarks.MouseDown, txtTentative.MouseDown, txtNotes.MouseDown
        If e.Button = System.Windows.Forms.MouseButtons.Right Then
            Dim pp As New ClassRtbContextMenu(sender)
            pp.Show(Me, PointToClient(Control.MousePosition))
        End If
    End Sub



    ''SET DEADLINE DATE
    'Private Sub cboClassGroup_SelectionChangeCommitted(ByVal sender As System.Object, ByVal e As System.EventArgs)


    '    Select Case sender.selectedindex
    '        Case Is = -1
    '        Case Is = 0
    '            Me.dtGrantAppDeadline.Text = "11/16/2012" 'DateAdd(DateInterval.Day, 90, CType("9/23/2010", Date))
    '        Case Is = 1
    '            Me.dtGrantAppDeadline.Text = "5/10/2013" 'DateAdd(DateInterval.Day, 90, CType("3/7/2013", Date))
    '    End Select

    'End Sub

    '-----------------
    ''Date delete
    'Private Sub LeaveDateField(ByVal sender As Object, ByVal e As System.EventArgs) _
    '    Handles dtAgreementMail.Leave, dtAgreementRecd.Leave, dtAppRecd.Leave, dtAppReceiptMail.Leave, dtClassConfirm.Leave, dtDetermination.Leave, dtGrantAppRecd.Leave, dtNotifyCall.Leave, dtNotifyMail.Leave ''ByVal e As System.ComponentModel.CancelEventArgs)
    '    '    modGlobalVar.Msg(mainTbl.Rows(0).Item(sender.DataBindings.Item("text").BindingMemberInfo.Bindingfield).ToString, , "in deldate")
    '    ' If
    '    If isLoaded Then
    '        Try
    '            DeleteDate(sender, mainTbl.Rows(0).Item(sender.DataBindings.Item("text").BindingMemberInfo.Bindingfield), Me.StatusBarPanel1) ' = True Then
    '        Catch ex As Exception
    '        End Try
    '    End If

    'End Sub

    Private Sub btnHelp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
    Handles btnHelp.Click
        modGlobalVar.Msg("YMGI HELP", "to create a grant, click the button on the Case Detail window.", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    'GOTO ORG
    Private Sub fldGotoOrg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles fldGotoOrg.Click

        Me.StatusBarPanel1.Text = "Opening Organization Detail window"
        modGlobalVar.OpenMainOrg(Me.fldOrgID.Text, Me.fldGotoOrg.Text)
        Me.StatusBarPanel1.Text = "Done"
    End Sub
    'GOTO CASE
    Private Sub FldGotoCase_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles lblGotoCase.DoubleClick
        If cboCase.SelectedIndex = -1 Then
            ' modGlobalVar.Msg("Select a case", MessageBoxIcon.Exclamation, "Incomplete Entry")
            cboCase.Focus() 'makes this form come ontop of case form
        Else
            modGlobalVar.OpenMainCase(cboCase.SelectedValue, cboCase.Text, Me.fldGotoOrg.Text, Me.fldOrgID.Text)
        End If
    End Sub

    'set deadline date
    Private Sub rbJan_checkedchanged(sender As System.Object, e As System.EventArgs) _
        Handles rbJan.CheckedChanged, rbMarch.CheckedChanged

        If isLoaded And sender.checked = True Then
            Me.dtGrantDeadline.Text = sender.text
            mainBSrce.Current("GrantAppDeadlineDt") = sender.text
        End If
    End Sub

    'delete dates
    '  Private Sub dtAgreementMailed_Leave(ByVal sender As Object, ByVal e As System.EventArgs) _
    '   Handles dtAgreementMail.Leave, dtAgreementRecd.Leave, dtAppRecd.Leave, dtAppReceiptMail.Leave, dtClassConfirm.Leave, dtDetermination.Leave, dtGrantAppDeadline.Leave, dtGrantAppRecd.Leave, dtNotifyCall.Leave, dtNotifyMail.Leave
    ' modGlobalVar.Msg(sender.Name.Substring(2, Len(sender.Name) - 2) & "dt")

    'If DeleteDate(sender, Me.DsMainGrantTMGI1.tblGrantTMGI.Rows(0).Item(sender.Name.Substring(2, Len(sender.Name) - 2) & "dt")) = True Then
    '    bChanged = True
    'End If

    ' ''NULL DATES
    'Private Sub LeaveDateField(ByVal sender As Object, ByVal e As System.EventArgs)
    '    Dim fldName As String = sender.DataBindings.Item("text").BindingMemberInfo.Bindingfield()
    '    'if
    '    DeleteDate(sender, mainTbl.Rows(0).Item(fldName)) '= True Then
    '    '        'If DeleteDate(IsNull(sender.text, ""), mainTbl.Rows(0).Item(fldName), cmgr) = True Then
    '    'bchanged = True
    '    'End If
    '    fldName = Nothing
    'End Sub



#End Region 'general

#Region "New Grant"

    'GENERATE NEW  GRANT
    'todo if already is a grant, open instead of generating new
    Private Sub migotoGrant_click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles miGotoGrant.Click, btnGrant.Click

        If IsNull(mainTbl.Rows(0).Item("GrantIDTxt"), "a").ToString = "a" Then  'grant does not exist yet
            If CType(IsNull(Me.dtGrantAppRecd.Text, "1/1/1911"), Date) > CType("1/1/2012", Date) Then
                Try
                    modPopup.NewGrant(fldOrgID.Text, "Youth Ministry Grant", True, "YOUTH MINISTRY GRANT", "YMGI", Me.fldGINum.Text, fldCaseID.Text, Me.dtGrantAppRecd.Text, IsNull(Me.cboCongRep.SelectedValue, 0), IsNull(Me.cboPD.SelectedValue, 0), usr)
                    Me.btnGrant.Visible = True
                Catch ex As Exception
                End Try

            Else
                modGlobalVar.Msg("ATTENTION: MISSING INFORMATION", "Please fill in date we receive the grant form", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If

        Else
            modGlobalVar.OpenMainGrant(mainTbl.Rows(0).Item("GrantIDTxt"), "YMGI Grant", Me.fldGotoOrg.Text, Me.fldOrgID.Text)
        End If
        'End If
    End Sub

    'create grant if this date filled in
    Private Sub dtGrantAppRecd_Leave(ByVal sender As Object, ByVal e As System.EventArgs) _
    Handles dtGrantAppRecd.Leave
        'validate
        If isLoaded = True Then
            Try
                If CType(IsNull(sender.text, "1/1/1911"), Date) > CType("1/2/1911", Date) Then
                    '  Me.btnGrant.Visible = True
                    'GrantButtonTxt()
                    Me.btnGrant.PerformClick()
                Else
                    Me.btnGrant.Visible = False
                End If
            Catch ex As Exception
            End Try
        End If
    End Sub

#End Region 'new grant

    'Private Sub dtAppRecd_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles dtAppRecd.Validating
    '    'modGlobalVar.Msg("validating")
    '    If sender.Text = String.Empty Or sender.text = " " Then
    '        sender.text = "1/1/1911" ' mainTbl.Rows(0).Item("AppRecdDt") = System.DBNull.Value
    '        bChanged = True
    '    End If
    'End Sub


    'GOTO CONTACT
    Private Sub ComboBox_DblClick(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles lblGotoPD.DoubleClick, lblGotoCongRep.Click

        Me.StatusBarPanel1.Text = "Opening " & sender.tag & " Detail window"
        OpenMainContact(sender.SelectedValue, sender.DisplayMember, fldGotoOrg.Text, LocalOrgID)
        Me.StatusBarPanel1.Text = "Done"
    End Sub


End Class