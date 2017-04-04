Imports System.Data.SqlClient
Imports System.Text

Public Class frmMainGrantTGI

    Public isLoaded As Boolean = False
    Dim isGrant As Boolean
    Dim dtContact As New DataTable("luContacts")
    Public bChanged As Boolean
    Dim bDelete As Boolean
    Dim bCancelClose As Boolean
    Dim ctlIdentify As Control 'fields for delete and messages
    Dim ctlNeutral As Control 'will never be invalidated
    Dim mainTbl As DataTable    'for global function
    Dim cmgr As CurrencyManager

#Region "Load"
    'LOAD
    Private Sub frmMainTGI_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        isLoaded = False
        Me.SuspendLayout()
        'iOrgID = 2068

        ' LoadOrgBasedCombos()

LoadStaffCombos:
        modGlobalVar.LoadStaffCombo(Me.cboCallNotified, False, StaffComboChoices.Selectable)
        modGlobalVar.LoadStaffCombo(Me.cboDeterminationStaff, False, StaffComboChoices.Selectable)

FormatFields:
        Dim ctl As Control
        Dim pnl As Control

        For Each pnl In Me.Controls
            If pnl.GetType.ToString = "System.Windows.Forms.Panel" Then
                For Each ctl In pnl.Controls  'Me.pnlApplication.Controls
                    If ctl.Name.Substring(0, 2) = "dt" Then
                        '  FormatDateFld(ctl, "text", Me.DsMainGrantTGI1, "MainGrantTGI." & ctl.Name.Substring(2, Len(ctl.Name) - 2) & "dt")
                        AddHandler ctl.DataBindings(0).Parse, AddressOf modGlobalVar.DateParse

                        ' AddHandler ctl.Leave, AddressOf LeaveDateField
                        'Else
                    End If
                Next ctl
            End If
        Next pnl

        ctlIdentify = Me.txtRemarks
        ctlNeutral = Me.btnHelp
        mainTbl = Me.DsMainGrantTGI.MainGrantTGI
        cmgr = Me.BindingSource1.CurrencyManager

        Me.ResumeLayout()
        Forms.Add(Me)
        CloseButton.Disable(Me)
        isLoaded = True
        StatusBar1.Panels(0).Text = "Done"

    End Sub

    Public Sub LoadOrgBasedCombos(ByVal orgid As Integer)
        Dim strsql As New SqlCommand("SELECT ContactID, ContactStaff FROM vwgetContacts where orgid = " & orgid, sc)
        Dim dtCase As New DataTable("dtCase")
        If SCConnect() Then
            Dim dr As SqlDataReader = strsql.ExecuteReader()
            dtContact.Load(dr)

            'LOAD CASES
            strsql.CommandText = "[luCaseNames]"
            strsql.CommandType = CommandType.StoredProcedure
            strsql.Parameters.Add("OrgID", SqlDbType.Int)
            strsql.Parameters("OrgID").Value = orgid
            dr = strsql.ExecuteReader(CommandBehavior.CloseConnection)
            dtCase.Load(dr)
            dr.Close()
            dr = Nothing
        End If


        Me.cboPD.DisplayMember = "ContactStaff"
        Me.cboPD.ValueMember = "ContactID"
        Me.cboPD.DataSource = dtContact.DefaultView

        Me.cboClergyLeader.DisplayMember = "ContactStaff"
        Me.cboClergyLeader.ValueMember = "ContactID"
        Me.cboClergyLeader.DataSource = dtContact

        Me.cboCase.DisplayMember = "CaseName"
        Me.cboCase.ValueMember = "CaseID"
        Me.cboCase.DataSource = dtCase.DefaultView

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

    '    SAVE
    Private Sub miSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles miSave.Click
        ' mainBSrce.EndEdit()
        UpdateDB("Save")
    End Sub

    'SAVE & EXIT
    Private Sub btnSaveExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnSaveExit.Click
        '  Dim ctl As Control
        Dim bUpdated As Boolean = False
        MouseWait()
Update:
        If modGlobalVar.AnyChanges(ctlNeutral, cmgr, mainTbl, bChanged) = True Then
            UpdateDB("SaveExit")
            bUpdated = True
        End If
        Me.Close()
        MouseDefault()
    End Sub

    'ALLOW CLOSE WITHOUT SAVING
    Protected Sub miCloseForm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles miClose.Click
        MouseWait()

        Dim bUpdated As Boolean = False
AskSave:
        If modGlobalVar.AnyChanges(ctlNeutral, cmgr, mainTbl, bChanged) = True Then
            Select Case modGlobalVar.Msg("CONFIRM: Closing - Save Changes?", Me.Text + ": " + ctlIdentify.Text & NextLine &
                                         "click Yes to save and close; No to discard changes and close, Cancel to remain open.", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
                'IF THERE ARE CHANGES
                Case Is = DialogResult.Yes
update:
                    UpdateDB("miclose")
                    bUpdated = True
                    Me.Close()
                Case Is = DialogResult.No 'discard changes, close 
                    Me.BindingSource1.EndEdit()
                    bCancelClose = False
                    bChanged = False
                    Me.Close()
                Case Is = MsgBoxResult.Cancel   'don't close
            End Select
        Else        'THERE ARE NO CHANGES
WarningClose:
            If modGlobalVar.Msg("CONFIRM: Closing with No Changes", Me.Text + ": " + IsNull(ctlIdentify.Text, "") & NextLine &
                                "Click OK to continue, Cancel to remain open.", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) = System.Windows.Forms.DialogResult.OK Then
                bCancelClose = False
                Me.Close()
            Else 'user cancels close
            End If
        End If
        MouseDefault()
    End Sub

    'CLOSING
    Private Sub frm_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) _
        Handles MyBase.FormClosing

        ' Dim ctl As Control
        Dim bUpdated As Boolean = False
        Dim Mystery As Boolean = False
        'ctl = CheckRequired()
AskSave:
        If modGlobalVar.AnyChanges(ctlNeutral, cmgr, mainTbl, bChanged) = True Then
            Dim dr As DataRow = mainTbl.Rows(0)
            Try
                For Each c As DataColumn In mainTbl.Columns
                    If dr(c, DataRowVersion.Proposed) Is dr(c, DataRowVersion.Current) Then
                    Else
                        If dr.Item(c, DataRowVersion.Current).ToString = dr.Item(c, DataRowVersion.Proposed).ToString Then
                        Else
                            '  modGlobalVar.Msg(c.ColumnName, "current: '" & dr.Item(c, DataRowVersion.Current) & "'" & NextLine & "Proposed: '" & dr.Item(c, DataRowVersion.Proposed) & "'" & NextLine & "Original: '" & dr.Item(c, DataRowVersion.Original) & "'", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Mystery = True
                            Exit For
                        End If
                    End If
                Next c
                If Mystery = True Then
                    Select Case modGlobalVar.msg("CONFIRM: Closing - Save Changes?", Me.Text + ": " + ctlIdentify.Text & NextLine &
                                         "click Yes to save and close; No to discard changes and close, Cancel to remain open.", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
                        Case Is = DialogResult.Yes
                            UpdateDB("close")
                            bUpdated = True
                        Case Is = DialogResult.No 'discard changes, but close
                            bCancelClose = False
                        Case Is = MsgBoxResult.Cancel
                            bCancelClose = True
                    End Select
                End If
            Catch ex As Exception
            End Try

        End If

ReleaseForm:
        If bCancelClose = False Then   'user OKs close form
            ClassOpenForms.frmMainTGI = Nothing 'reset global var
        Else
        End If
        e.Cancel = bCancelClose

    End Sub

    'UPDATE
    Public Sub UpdateDB(ByVal How As String)
        MouseWait()
        Me.StatusBarPanel1.Text = "Updating server"
        Me.DsMainGrantTGI.EnforceConstraints = False

        '      modGlobalVar.Msg(cboCase.SelectedIndex.ToString, , "in update")
        'ERROR HANDLING FOR END EDIT LIKE VALIDATION
        Try
            Me.Validate()
            Me.BindingSource1.EndEdit()
            'If CType(cmgr.Current, DataRowView).Row.HasVersion(DataRowVersion.Proposed) Then 'doesn't work
            '   If Me.DsMainContact1.HasChanges(DataRowState.Modified) Then 'works, but now cancel doesn't
            bChanged = False
            '          modGlobalVar.Msg(cboCase.SelectedIndex.ToString, , "after cmgr endedit")
        Catch ECONSTRAINT As ConstraintException
            modGlobalVar.Msg("CONSTRAINT ERROR", ECONSTRAINT.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ENULL As NoNullAllowedException
            modGlobalVar.Msg("NULL ERROR", ENULL.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch EDATA As DataException
            modGlobalVar.Msg("ADO.NET DATA ERROR", EDATA.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ESYSTEM As Exception
            modGlobalVar.Msg("ESYSTEM ERROR", ESYSTEM.Message & NextLine & ESYSTEM.GetType.ToString, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        'If How = "minimize" And Me.DsMainResourceRecommend1.HasChanges Then
        '    If modGlobalVar.Msg(" ", MessageBoxButtons.YesNo, "Recommendation window already open - save changes before closing?") = DialogResult.No Then
        '        Exit Sub
        '    End If
        'End If

BeginTransaction:
        If Not SCConnect() Then
            Exit Sub
        End If

        Try
            'Me.daMainRecommendation.UpdateCommand.Parameters("@ID").Value = Me.fldID.Text
            'Me.daMainRecommendation.Update(Me.DsMainResourceRecommend1.MainResRecommend)
            Me.MainGrantTGITableAdapter.Update(Me.DsMainGrantTGI.MainGrantTGI)
            '  myTransaction.Commit()
            bCancelClose = False
        Catch dbcex As DBConcurrencyException
            modGlobalVar.Msg("ERROR: update concurrency", "someone else has changed this Recommendation", MessageBoxButtons.OK, MessageBoxIcon.Error)
            'TODO put code here to capture changes and/or override
            '  myTransaction.Rollback()
            bCancelClose = True
        Catch eUpdate As System.Exception
            modGlobalVar.Msg("ERROR:", eUpdate.Message & NextLine & eUpdate.GetType.ToString, MessageBoxButtons.OK, MessageBoxIcon.Error)
            bCancelClose = True
            '  myTransaction.Rollback()
        Finally
            sc.Close()
        End Try
        Me.StatusBarPanel1.Text = "Update Finished"
        MouseDefault()
    End Sub

#End Region 'update main

#Region "Edit buttons"


    'CANCEL CHANGES
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        BindingSource1.CancelEdit()
        bChanged = False
        Me.StatusBarPanel1.Text = "Changes cancelled"
    End Sub

    'DELETE RECOMMENDATION
    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)


        Dim ctl As Control = Me.txtNotes

        If modGlobalVar.Msg("CONFIRM DELETE", "This TGI application will be marked for deletion and the window closed.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            ctl.Text = "DELETE: " & IsNull(ctl.Text, "")
            UpdateDB("delete")
            bDelete = True
            Me.btnSaveExit.PerformClick()
        End If

    End Sub

#End Region 'edit


#Region "General"

    'GENERATE NEW  GRANT
    'todo if already is a grant, open instead of generating new
    Private Sub migotoGrant_click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        If IsNull(mainTbl.Rows(0).Item("GrantIDTxt"), "").ToString > "" Then
            modGlobalVar.OpenMainGrant(mainTbl.Rows(0).Item("GrantIDTxt"), "TGI Grant", Me.fldGotoOrg.Text, Me.fldOrgID.Text)

        Else
            If CType(IsNull(Me.dtGrantAppRecd.Text, "1/1/1911"), Date) > CType("1/1/2010", Date) Then
                modPopup.NewGrant(fldOrgID.Text, "Technology Grant", True, "TECHNOLOGY", "TGI", Me.fldGINum.Text, 0, Me.dtGrantAppRecd.Text, IsNull(Me.cboPD.SelectedValue, 0), IsNull(Me.cboClergyLeader.SelectedValue, 0), usr)
                btnGrant.Text = "Go to Grant"
            Else
                modGlobalVar.Msg("ATTENTION: MISSING INFORMATION", "Please fill in date we receive the grant form", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If
        End If
        'End If
    End Sub

    'SET DEADLINE DATE
    Private Sub cboClassGroup_SelectionChangeCommitted(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Select Case sender.selectedindex
            Case Is = -1
            Case Is = 0
                Me.dtGrantAppDeadline.Text = "12/15/2010" 'DateAdd(DateInterval.Day, 90, CType("9/23/2010", Date))
            Case Is = 1
                Me.dtGrantAppDeadline.Text = DateAdd(DateInterval.Day, 90, CType("4/14/2011", Date))
        End Select

    End Sub


    'UNDO
    Protected Overrides Function ProcessDialogKey(ByVal keyData As Keys) As Boolean
        If keyData = System.Windows.Forms.Keys.Escape Then
            modPopup.UndoCtl(Me.ActiveControl)
            bChanged = True
            Return True  ' True means we've processed the escape key
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


    'delete dates
    '  Private Sub dtAgreementMailed_Leave(ByVal sender As Object, ByVal e As System.EventArgs) _
    '   Handles dtAgreementMail.Leave, dtAgreementRecd.Leave, dtAppRecd.Leave, dtAppReceiptMail.Leave, dtClassConfirm.Leave, dtDetermination.Leave, dtGrantAppDeadline.Leave, dtGrantAppRecd.Leave, dtNotifyCall.Leave, dtNotifyMail.Leave
    ' modGlobalVar.Msg(sender.Name.Substring(2, Len(sender.Name) - 2) & "dt")

    'If DeleteDate(sender, Me.DsMainGrantTGI1.MainGrantTGI.Rows(0).Item(sender.Name.Substring(2, Len(sender.Name) - 2) & "dt")) = True Then
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



    Private Sub btnGrant_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGrant.Click
        Select Case btnGrant.Text
            Case "Go to Grant"
                miGotoGrant.PerformClick()
            Case "Create Grant"
                miGotoGrant.PerformClick()
        End Select
    End Sub



    'Private Sub LeaveDateField(ByVal sender As Object, ByVal e As System.EventArgs)
    '    '        Handles dtAgreementMail.Leave, dtAgreementRecd.Leave, dtAppRecd.Leave, dtAppReceiptMail.Leave, dtClassConfirm.Leave, dtDetermination.Leave, dtGrantAppDeadline.Leave, dtGrantAppRecd.Leave, dtNotifyCall.Leave, dtNotifyMail.Leave ''ByVal e As System.ComponentModel.CancelEventArgs)
    '    'handler added from load instead
    '    'ByVal e As System.ComponentModel.CancelEventArgs)
    '    '    modGlobalVar.Msg(mainTbl.Rows(0).Item(sender.DataBindings.Item("text").BindingMemberInfo.Bindingfield).ToString, , "in deldate")
    '    ' If
    '    If isLoaded Then
    '        DeleteDate(sender, mainTbl.Rows(0).Item(sender.DataBindings.Item("text").BindingMemberInfo.Bindingfield), Me.StatusBarPanel1) ' = True Then
    '    End If
    '    '   bChanged = True
    '    ' Me.StatusBarPanel1.Text = "(1/1/1911 indicates a blank date)"
    '    'End If
    'End Sub
    'Private Sub dtAppRecd_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles dtAppRecd.Validating
    '    'modGlobalVar.Msg("validating")
    '    If sender.Text = String.Empty Or sender.text = " " Then
    '        sender.text = "1/1/1911" ' mainTbl.Rows(0).Item("AppRecdDt") = System.DBNull.Value
    '        bChanged = True
    '    End If
    'End Sub


    Private Sub btnHelp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnHelp.Click
        modGlobalVar.Msg("TGI HELP", "to create a grant, go to the Case Detail window.", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    'GOTO ORG
    Private Sub fldGotoOrg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles fldGotoOrg.DoubleClick

        Me.StatusBarPanel1.Text = "Opening Organization Detail window"
        modGlobalVar.OpenMainOrg(Me.fldOrgID.Text, Me.fldGotoOrg.Text)
        Me.StatusBarPanel1.Text = "Done"
    End Sub
    'GOTO CASE
    Private Sub FldGotoCase_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles fldGotoCase.DoubleClick
        If cboCase.SelectedIndex = -1 Then
            ' modGlobalVar.Msg("Select a case", MessageBoxIcon.Exclamation, "Incomplete Entry")
            cboCase.Focus() 'makes this form come ontop of case form
        Else
            modGlobalVar.OpenMainCase(cboCase.SelectedValue, cboCase.Text, Me.fldGotoOrg.Text, Me.fldOrgID.Text)
        End If
    End Sub


    'Private Sub dtGrantAppRecd_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtGrantAppRecd.Leave
    '    If IsNull(sender.text, "1/1/1911") > "1/1/1911" Then
    '        Me.btnGrant.Visible = True

    '    End If
    'End Sub


    Private Sub DateFields_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles dtAgreementMail.Validating,
        dtAgreementRecd.Validating, dtAppRecd.Validating, dtAppReceiptMail.Validating, dtClassConfirm.Validating, dtDetermination.Validating,
        dtGrantAppDeadline.Validating, dtGrantAppRecd.Validating, dtNotifyCall.Validating, dtNotifyMail.Validating
        e.Cancel = modGlobalVar.ValidateDateA(sender)
    End Sub

End Class