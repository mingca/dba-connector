Imports System.Data.SqlClient


Public Class frmNewWOrder2

    Dim bDelete, bCancelClose, isLoaded As Boolean
    Dim bDup As Boolean = False 'check forduplicate entries
    Public bChanged As Boolean = False
    Dim mainTbl As DataTable
    Dim ctlNeutral, ctlIdentify As Control
    Dim cntGroup As Integer = 1
    Dim iFee As Integer ' = 30
    Dim iDiscount As Integer '= 5
    Dim iGroupMin As Integer '=3
    Dim cntTeam As Integer
    Dim objHowClose As New structCloseMethod 'Object = Me.btnSaveExit 'identify object calling close

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
    Private Sub NewWOrder_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles MyBase.Load

        MouseWait()

        bChanged = False
        Me.lblRegion.Text = usrRegion
        Me.cboEvent.DataSource = tblWEvents

        mainTbl = Me.DsNewWRegistration.tblNewWRegistration
        ctlIdentify = Me.txtRegDate
        ctlNeutral = Me.btnHelp

        modGlobalVar.FormatCurrencyFld(Me.fldCalcAmount, "text", Me.DsNewWRegistration, "tblEventDD.Fee")
        modGlobalVar.FormatCurrencyFld(Me.txtAmount, "text", Me.DsNewWRegistration, "tblNewWRegistration.AmountTotOrder")
        Forms.Add(Me)
        isLoaded = True
        MouseDefault()

    End Sub

    'RELOAD
    Public Sub Reload(ByVal bPayee As Boolean)
        '  modGlobalVar.Msg("in reload")
        Me.btnPopupPayee.Visible = bPayee
        Me.btnPopupGroup.Visible = Not bPayee
        ' GetFees(EventID)
        LoadGrid() ', Me.fldEventNum.Text)
        CalcDiscount()
    End Sub

    'LOAD GRID
    Public Sub LoadGrid() 'ByVal iOrder As Integer) ', ByVal iEvent As Integer)
        Dim iEvent As Integer = Me.DsNewWRegistration.tblNewWRegistration.Rows(0)("EventNum")
        Dim iOrgNum As Integer = Me.DsNewWRegistration.tblNewWRegistration.Rows(0)("OrgNum")
        Dim iOrderNum As Integer = Me.DsNewWRegistration.tblNewWRegistration.Rows(0)("OrderNum")
        'why does this only work the first time?
        '        Dim dv As DataView
        Me.tblRegOrderTableAdapter.Fill(Me.DsNewWRegistration.tblRegOrder, iOrderNum)
        dv = Me.DsNewWRegistration.tblRegOrder.DefaultView
        cntGroup = dv.Count '= #people in order

        Dim foundRows() As Data.DataRow
        foundRows = Me.DsNewWRegistration.tblRegOrder.Select()

        '  make it so matches online - groups must register in one order to receive the discount
        '  easy to fix in InfoCenter, can't be done online
        foundRows = Me.DsNewWRegistration.tblRegOrder.Select("OrgNum = " & iOrgNum & " AND EventNum = " & iEvent)
        cntTeam = foundRows.Length  '= #people at this event

        If cntTeam > 2 Then
            Me.dgrd.CaptionText = cntGroup.ToString + " total.  " + cntTeam.ToString & " from same congregation - Discount applies"
        Else
            Me.dgrd.CaptionText = cntGroup.ToString & " covered by this payment. No discount."
        End If
        dv = Nothing

    End Sub

#End Region 'load

#Region "Update Main"

    'SAVE
    Private Sub miSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles miSave.Click
        UpdateDB("save")
    End Sub

    'SAVE & EXIT
    Private Sub miSaveExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles btnSaveExit.Click
        objHowClose.sh = ObjClose.btnSaveExit
        Me.Close()
    End Sub

    'ALLOW CLOSE WITHOUT SAVING
    Protected Sub miCloseForm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
           Handles miClose.Click

        MouseWait()
        objHowClose.sh = modGlobalVar.CloseDetailForm(Me.Tag, IsNull(ctlIdentify.Text, "no registrant"), Me.TblNewWRegistrationBindingSource, ctlNeutral, mainTbl, bChanged)

        Select Case objHowClose.sh
            Case Is = ObjClose.SaveClose    'save changes, close
                UpdateDB("miClose")
            Case Is = ObjClose.cancelClose      'cancel close
                bCancelClose = True
                GoTo CloseAll
            Case Is = ObjClose.DontSaveClose       'discard changes, close
                bChanged = False
            Case Is = ObjClose.noChanges        'no changes, close
        End Select
        Me.Close()
CloseAll:
        MouseDefault()
    End Sub

    'CLOSING
    Private Sub frm_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) _
               Handles MyBase.FormClosing
        Dim ctl As Control
        Dim arCtls() As Control

        MouseWait()

        Select Case objHowClose.sh
            Case Is = ObjClose.miDelete
                bCancelClose = False
                GoTo ReleaseForm
            Case Is = ObjClose.DontSaveClose, ObjClose.noChanges, ObjClose.btnSaveExit
                bCancelClose = False
                ctl = CheckRequiredFields(2)
                If ctl Is Nothing Then
                Else
                    modGlobalVar.msg("cancelling request", "please fill in " & ctl.Name, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    bCancelClose = True
                    GoTo ReleaseForm
                End If
                GoTo ReleaseForm
        End Select

CheckRequiredFields:
        'CheckforChanges:
        If objHowClose.sh > 1 Then 'anychanges has already run
        Else
            If modGlobalVar.AnyChanges(ctlNeutral, Me.TblNewWRegistrationBindingSource, mainTbl, bChanged) = True Then
                ' modGlobalVar.Msg("yes changes")
                Try
                    UpdateDB("Closing")
                Catch ex As Exception
                    modGlobalVar.msg("ERROR: update from close", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            Else
                ' modGlobalVar.Msg("no changes")
            End If
            bCancelClose = False
        End If
        'Else 'don't save empty(record)
        '    If modGlobalVar.Msg("This record will be deleted", MessageBoxButtons.YesNo, "MISSING REGISTRANT") = DialogResult.Yes Then
        '        Me.ErrorProvider1.SetError(ctlIdentify, "")
        '        DoDelete()
        '        bCancelClose = False
        '    Else
        '        Me.ErrorProvider1.SetError(ctlIdentify, "Select a registrant")
        '        bCancelClose = True
        '    End If
        'End If

ReleaseForm:
        iPayeeOrgNum = 0
        arCtls = Nothing
        ctl = Nothing
        If bCancelClose = False Then   'user OKs close form
            ClassOpenForms.frmNewWOrder = Nothing 'reset global var
            objHowClose = Nothing
        Else
        End If
        MouseDefault()
        e.Cancel = bCancelClose

    End Sub

    'UPDATE
    Public Sub UpdateDB(ByVal How As String)
        MouseWait()
        Try
            Me.TblNewWRegistrationBindingSource.EndEdit()
            Me.TblNewWRegistrationTableAdapter.Update(Me.DsNewWRegistration.tblNewWRegistration)
            bChanged = False

        Catch ex As Exception
            modGlobalVar.msg("ERROR: Save ", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        MouseDefault()
    End Sub

#End Region

#Region "General"

    'UNDO
    Protected Overrides Function ProcessDialogKey(ByVal keyData As Keys) As Boolean
        If keyData = System.Windows.Forms.Keys.Escape Then
            modPopup.UndoCtl(Me.ActiveControl)
            bChanged = True
            Return True  ' True means we've processed the key
        Else
            Return MyBase.ProcessDialogKey(keyData)
        End If
    End Function

    'HELP btn
    Private Sub btnHelp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles btnHelp.Click, miHelp.Click
        modGlobalVar.Msg("Registration Help", "SELECT Contact Name from DropDown box.  Inactive names are indicated with ~ and are at the bottom of the list." & NextLine & NextLine & "FOR MULTIPLE REGISTRATIONS: click the menu item 'Multiple Event Registrations'", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    'RIGHT CLICK MENU
    Private Sub textbox_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) _
        Handles rtbNotes.MouseDown
        If e.Button = System.Windows.Forms.MouseButtons.Right Then
            Dim pp As New ClassRTBContextMenu(sender)
            pp.Show(Me, PointToClient(Control.MousePosition))
        End If
    End Sub
#End Region

#Region "EDIT Buttons"
    'DELETE
    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles miDelete.Click, btnDelete.Click
        If modGlobalVar.msg("CONFIRM DELETE", "The Registrar's registration " & NextLine & " will be marked for deletion and the window closed.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            Me.rtbNotes.Text = "DELETE: " & IsNull(Me.rtbNotes.Text, "")
            objHowClose.sh = ObjClose.miDelete
            UpdateDB("delete")
            Me.Close()
        End If
    End Sub

    'CANCEL CHANGES
    Private Sub miCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles miCancel.Click
        Me.TblNewWRegistrationBindingSource.CancelEdit()
        bChanged = False
    End Sub

#End Region 'edit buttons

#Region "Validation"

    'DATE VALIDATE
    Private Sub txtRegDate_Leave_1(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles txtRegDate.Leave

        modGlobalVar.DeleteDate(sender, Me.DsNewWRegistration.tblNewWRegistration.Rows(0).Item(sender.DataBindings.Item("text").BindingMemberInfo.Bindingfield))
    End Sub

    'CHECK REQD FIELDS
    Private Function CheckRequiredFields(ByVal i As Integer) As Control

        'required for popup
        If Me.fldOrderNum.Text = String.Empty Then
            Return Me.fldOrderNum
        End If
        If Me.txtRegDate.Text = String.Empty Then
            Return Me.txtRegDate
        End If
        'required for leaving
        If i = 2 Then
            If Me.txtAmount.Text = String.Empty Then
                Return Me.txtAmount
            Else
            End If
            'PAYMENT METHOD
            If Me.cboMethod.Text = String.Empty Then
                Return Me.cboMethod
            Else
            End If
        End If
        Return Nothing
    End Function

#End Region 'validation

#Region "Form Specific"

    'BTN POPUP PAYEE
    Private Sub btnPopupPayee_Click(ByVal sender As Object, ByVal e As System.EventArgs) _
        Handles btnPopupPayee.Click
        Dim ctl As Control = CheckRequiredFields(1)
        If ctl Is Nothing Then
        Else
            modGlobalVar.msg("cancelling request", "please fill in " & ctl.Name, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

LoadPopup:
        Try
            Me.miSave.PerformClick()    'THIS SETS eventid, contactID, unique RegID, OrderID, amount; and PAYMENT METHOD for POPUP
            Me.StatusLabel1.Text = "loading controls"
            MouseWait()
            Dim frm As New frmNewWOrderPopup2
            frm.DsWRegPopup.EnforceConstraints = False
            modGlobalVar.LoadRegionCombo(frm.cboRegion, "All", usrRegion) 'IsNull(Me.lblRegion.Text, "NE"))
            ' frm.LoadOrgDD(IsNull(Me.lblRegion.Text, "NE"))
            frm.TblWRegPOpupTableAdapter.Fill(frm.DsWRegPopup.tblWRegPopup, Me.fldRegistrationID.Text)
EventCBO:
            frm.cboEvent.ValueMember = Me.cboEvent.ValueMember
            frm.cboEvent.DisplayMember = Me.cboEvent.DisplayMember
            frm.cboEvent.DataSource = Me.cboEvent.DataSource
            frm.cboEvent.SelectedIndex = Me.cboEvent.SelectedIndex
            '  frm.cboRegion.SelectedIndex = frm.cboRegion.FindStringExact("All Indiana") 'usrRegion)
            Me.StatusLabel1.Text = "loading form"
OpenForm:
            MouseDefault()
            frm.ShowDialog()
        Catch ex As Exception
            modGlobalVar.msg("ERROR with popup 1", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
        MouseWait()
        Me.StatusLabel1.Text = "setting controls"
        '   If iPayeeOrgNum > 0 Then
        Me.fldOrgNum.Text = iPayeeOrgNum.ToString
        Me.btnPopupPayee.Visible = False
        Me.btnPopupGroup.Visible = True
        Me.lblRegion.Text = grRegion

        Me.StatusLabel1.Text = "refreshing grid"
        Me.TblNewWRegistrationTableAdapter.FillbyRegID(Me.DsNewWRegistration.tblNewWRegistration, Me.fldRegistrationID.Text)   'REFRESH for any payment changes
RefreshGrid:
        LoadGrid()
        CalcDiscount()
        MouseDefault()
        '  End If
    End Sub

    'BTN POPUP GROUP
    Private Sub btnPopupGroup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles btnPopupGroup.Click
        Dim ctl As Control = CheckRequiredFields(1)
        If ctl Is Nothing Then
        Else
            modGlobalVar.msg("cancelling request", "please fill in " & ctl.Name, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        Dim newID As Integer
        Dim str As String
        MouseWait()
        CheckRequiredFields(2)
        'TODO check for existing registrations at this event
        Try
InsertNewRegistration:

            Dim cmd As New SqlClient.SqlCommand("INSERT INTO tblEventReg2 (EventNum,  RegDate, OrderNum,  EnteredBy) VALUES (" & Me.fldEventNum.Text + ",  N'" & DateTime.Now & "'," & Me.fldOrderNum.Text & ", N'Group Registration', N'" & usrFirst & "'); SELECT @@IDENTITY", sc)
            If Not SCConnect() Then
                Exit Sub
            End If
            Try
                newID = cmd.ExecuteScalar()
            Catch exce As Exception
                modGlobalVar.msg("ERROR: insert reg nonquery", exce.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            Finally
                sc.Close()
                cmd = Nothing
                str = Nothing
            End Try

LoadData:
            Dim frm As New frmNewWOrderPopup2
            frm.DsWRegPopup.EnforceConstraints = False
 
            Try
                frm.TblWRegPOpupTableAdapter.Fill(frm.DsWRegPopup.tblWRegPopup, newID)
                'frm.LoadOrgDD(Me.lblRegion.Text)
            Catch ex As Exception
                modGlobalVar.msg("ERROR: filling popup table", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try

EventCBO:
            Try
                frm.cboEvent.ValueMember = Me.cboEvent.ValueMember
                frm.cboEvent.DisplayMember = Me.cboEvent.DisplayMember
                frm.cboEvent.DataSource = Me.cboEvent.DataSource
                frm.cboEvent.SelectedIndex = Me.cboEvent.SelectedIndex
                modGlobalVar.LoadRegionCombo(frm.cboRegion, "All", IsNull(Me.lblRegion.Text, usrRegion))
            Catch ex As Exception
                modGlobalVar.msg("ERROR: setup event dd", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
            MouseDefault()
OpenForm:
            frm.ShowDialog()

        Catch ex As Exception
            modGlobalVar.msg("ERROR: can't open new reg popup", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        MouseWait()
RefreshGrid:
        LoadGrid()
        CalcDiscount()
        MouseDefault()
    End Sub

    'CALC DISCOUNT & FEE
    Private Sub CalcDiscount()
        ' no fee depending on payment method
        Select Case Me.cboMethod.SelectedItem
            Case Is = "Cash", "Check", "Credit Card", "Will pay at event", "Unpaid"
                If cntTeam >= iGroupMin And iFee = 30 Then 'fldQty.Text > 2 Then 'and all have same org id
                    Me.fldDiscount.Text = "Discount: $ " & (cntTeam * iDiscount).ToString
                    Me.fldCalcAmount.Text = "Calculated Total: $ " & (iFee * cntGroup) - (iDiscount * cntTeam) 'fldQty.Text - (iDiscount * 5)
                    '  Me.fldAmount.Text = (fldFee.Text * fldQty.Text) - (fldQty.Text * fldDiscount.Text)
                Else
                    Me.fldDiscount.Text = 0
                    Me.fldCalcAmount.Text = "Calculated Amount: $ " & iFee * cntGroup
                End If

            Case Else   'no charge
                fldDiscount.Text = 0
                Me.fldCalcAmount.Text = cboMethod.SelectedItem & " $0"
                '    Me.lbldisplayc.text = "no charge"
        End Select
    End Sub

    'CALC DISCOUNT
    Private Sub btnCalc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCalc.Click
        CalcDiscount()
    End Sub

    'BTN FIND ORDER
    Private Sub FindOrder_Click(sender As System.Object, e As System.EventArgs) Handles FindOrder.Click

        Me.DsNewWRegistration.tblNewWRegistration.Clear()
        Me.DsNewWRegistration.tblRegOrder.Clear()

        Try
            OpenNewWOrder2(CType(InputBox("", "Enter orderID"), Integer), "View Existing Order", True, False)
        Catch ex As Exception
            modGlobalVar.msg("ERROR: opening order detail", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region 'form specific

End Class