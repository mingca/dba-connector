Public Class frmMainStaffConversation

    Public bChanged As Boolean = False
    Dim bCancelClose As Boolean = False
    Dim ctlIdentify As Control
    Dim localorgid As Integer

    Friend WithEvents daspCaseNames As System.Data.SqlClient.SqlDataAdapter

    '
    'SqlSelectCommand3
    '



#Region "Load"
    Private Sub frmMainStaffConversation_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'DsStaffConv1.tblStaffConversation' table. You can move, or remove it, as needed.

        ctlIdentify = Me.txtBriefSummary

        'STAFF
        modGlobalVar.LoadStaffCombo(Me.cboCaller, False, StaffComboChoices.Selectable)
        'MODE OF CONTACT
        Dim i As Integer
        For i = 0 To colModelu.Count - 1
            Me.cboMode.Items.Add(colModelu(i + 1))
        Next
    End Sub


    'LOAD CASES
    Public Sub LoadCases(ByVal ID As Integer)
        daspCaseNames = New System.Data.SqlClient.SqlDataAdapter
        daspCaseNames.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "luCaseNames", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("CaseID", "CaseID"), New System.Data.Common.DataColumnMapping("CaseName", "CaseName")})})
        daspCaseNames.SelectCommand.CommandText = ("[luCaseNames]")
        daspCaseNames.SelectCommand.Connection = sc
        daspCaseNames.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure
        daspCaseNames.SelectCommand.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, False, CType(0, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing), New System.Data.SqlClient.SqlParameter("@OrgID", System.Data.SqlDbType.Int, 4)})

        Me.daspCaseNames.SelectCommand.Parameters("@OrgID").Value = ID
        Me.DsCaseNames1.Clear()
        Try
            daspCaseNames.Fill(DsCaseNames1)
        Catch ex As Exception
            modGlobalVar.Msg("ERROR: can't fill case combo", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
#End Region 'load


#Region "Update Main"


    '   SAVE
    Private Sub miSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles miSave.Click
        '  mainBSrce.EndEdit()
        UpdateDB("save")
    End Sub

    'SAVE & EXIT
    Private Sub btnSaveExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
          Handles btnSaveExit.Click

        Dim bUpdated As Boolean = False
        MouseWait()
        'Update:
        '       If AnyChanges(ctlNeutral, cmgr, mainTbl, bChanged) = True Then
        UpdateDB("SaveExit")
        'bUpdated = True
        'End If
ValidateClose:
        'ctl = CheckRequired()
        'If ctl Is ctlNeutral Then
        ' bCancelClose = False
        Me.Close()
        ' Else
        ' GetMissingMessage(bUpdated, ctl)
        ''If ctl.Name = Me.cboCase.Name Or ctl.Name = Me.cboCaller.Name Then
        ' ctl.Focus()
        ''End If
        'End If
        MouseDefault()
    End Sub

    'ALLOW CLOSE WITHOUT: SAVING & Required fields
    Protected Sub miCloseForm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
           Handles miClose.Click
        MouseWait()
        ' Dim ctl As Control
        Dim bUpdated As Boolean = False

AskSave:
        '     If AnyChanges(ctlNeutral, cmgr, mainTbl, bChanged) = True Then
        ' Select Case modGlobalVar.Msg(Me.Tag + ": " + ctlIdentify.Text, MessageBoxButtons.YesNoCancel, "CLOSING " + Me.Text + ".  Save changes before closing?")
        ' 'IF THERE ARE CHANGES
        '     Case Is = DialogResult.Yes
Update:
        UpdateDB("miclose")
        '  bUpdated = True
ValidateClose:
        '  ctl = CheckRequired()
        '  If ctl Is ctlNeutral Then
        ' bCancelClose = False
        ' Me.Close()
        ' Else
        ' GetMissingMessage(bUpdated, ctl)
        ' 'If ctl.Name = Me.cboCase.Name Then  'required field
        ' ctl.Focus()
        ' 'End If
        ' Exit Sub
        ' End If
        '     Case Is = DialogResult.No 'discard changes, close 
        ' ' mainTbl.RejectChanges()
        ' Me.cmgr.CancelCurrentEdit()
        ' bCancelClose = False
        ' bChanged = False
        ' Me.Close()
        '     Case Is = msgboxResult.Cancel   'don't close
        ' End Select
        ' Else        'THERE ARE NO CHANGES
WarningClose:
        ' ctl = CheckRequired()
        ' If ctl Is ctlNeutral Then
        ' If modGlobalVar.Msg(Me.Tag + ": " + IsNull(ctlIdentify.Text, IsNull(Me.dtpDate.Value, "")), MessageBoxButtons.OKCancel, "Closing " + Me.Name + " with NO Changes") = msgboxResult.Ok Then
        ' bCancelClose = False
        ' Me.Close()
        ' Else 'user cancels close
        ' End If
        ' Else    'missing info
        '  mandatory fields - caller.  Also case name, but can't insert default case.
        ' Dim strmsg As String
        ' If ctl.Name = Me.cboCaller.Name Then
        ' 'modGlobalVar.Msg("Fill in " + IsNull(ctl.Tag, ctl.Name) + " and try again.", MessageBoxIcon.Exclamation, "WAIT Cannot close with MISSING REQUIRED INFORMATION")
        ''bCancelClose = True
        ''ctl.Focus()
        'strmsg = "...text will be added for you."
        'End If
        ''allow to close with missing info
        'If modGlobalVar.Msg("But Conversation " + IsNull(ctlIdentify.Text, IsNull(Me.dtpDate.Value, "")) + " is missing the " + UCase(IsNull(ctl.Tag, ctl.Name)), MessageBoxButtons.OKCancel, "NOTE: Closing " + Me.Name + "  with NO Changes") = msgboxResult.Ok Then
        ' bCancelClose = False
        ' Me.Close()
        ' Else    'user cancels close
        ' End If
        ' ' End If
        ' strmsg = Nothing
        ' End If
        ' End If
        MouseDefault()
    End Sub

    'CLOSING
    Private Sub frm_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) _
            Handles MyBase.FormClosing
        UpdateDB("close")

        '        Dim ctl As Control
        '        Dim bUpdated As Boolean = False
        '        Dim Mystery As Boolean = False

        'AskSave:
        '        If AnyChanges(ctlNeutral, cmgr, mainTbl, bChanged) = True Then
        '            Dim dr As DataRow = mainTbl.Rows(0)
        '            For Each c As DataColumn In mainTbl.Columns 'else is finding changes where are none
        '                If dr(c, DataRowVersion.Proposed) Is dr(c, DataRowVersion.Current) Then
        '                Else
        '                    If dr.Item(c, DataRowVersion.Current).ToString = dr.Item(c, DataRowVersion.Proposed).ToString Then
        '                    Else
        '                        'modGlobalVar.Msg("current: '" & dr.Item(c, DataRowVersion.Current) & "'"& NextLine &  "Proposed: '" & dr.Item(c, DataRowVersion.Proposed) & "'"& NextLine &  "Original: '" & dr.Item(c, DataRowVersion.Original) & "'", , c.ColumnName)
        '                        Mystery = True
        '                        Exit For
        '                    End If
        '                End If
        '            Next c
        '            If Mystery = True Then
        '                Select Case modGlobalVar.Msg(Me.Tag + " " + ctlIdentify.Text, MessageBoxButtons.YesNoCancel, "CLOSING " + Me.Text + "  - Save changes before closing?")
        '                    Case Is = DialogResult.Yes
        '                        UpdateDB("close")
        '                        bUpdated = True
        '                    Case Is = DialogResult.No 'discard changes, but close
        '                        bCancelClose = False
        '                    Case Is = msgboxResult.Cancel
        '                        bCancelClose = True
        '                End Select
        '            End If
        '        End If

        'InsertDefaultData:  'caller
        '        Me.cboCaller.SelectedIndex = Me.cboCaller.FindStringExact(usrName)
        '        bChanged = True
        '        bCancelClose = False
        '        Me.UpdateDB("insert default")
        'ReleaseForm:
        '        If bCancelClose = False Then   'user OKs close form
        ClassOpenForms.frmMainStaffConversation = Nothing 'reset global var
        '        Else
        '        End If
        '        e.Cancel = bCancelClose
    End Sub

    'UPDATE
    Public Sub UpdateDB(ByVal How As String)
        MouseWait()
        Me.StatusBarPanel1.Text = "Updating server"

        Try
            '       Me.TblStaffConversationTableAdapter.Update(Me.DsStaffConv1.MainStaffConversation, Me.fldID.Text)
            ' modGlobalVar.Msg("updated")
        Catch ex As Exception
            '  modGlobalVar.Msg(ex.Message, , "couldn't save changes")
        End Try

        Me.StatusBarPanel1.Text = "Update Finished"
        MouseDefault()
    End Sub

    'CHECK REQUIRED FIELDS
    'Private Function CheckRequired() As Control
    '    Dim uI As New usrInput
    '    Dim ctl As ComboBox
    '    'mode
    '    ctl = Me.cboMode
    '    Me.StatusBarPanel1.Text = "Checking " + IsNull(ctl.Tag, ctl.Name)
    '    uI = ValidateCBO(ctl, IsNull(ctl.Tag, ctl.Name), True, CanAddNew.None)
    '    If uI = usrInput.OK Then
    '        Me.ErrorProvider1.SetError(ctl, "")
    '    Else
    '        SetError(Me.ErrorProvider1, ctl, uI, False)
    '        Return ctl
    '    End If
    '    'caller
    '    ctl = Me.cboCaller
    '    Me.StatusBarPanel1.Text = "Checking " + IsNull(ctl.Tag, ctl.Name)
    '    uI = ValidateCBO(ctl, IsNull(ctl.Tag, ctl.Name), True, CanAddNew.None)
    '    If uI = usrInput.OK Then
    '        Me.ErrorProvider1.SetError(ctl, "")
    '    Else
    '        SetError(Me.ErrorProvider1, ctl, uI, False)
    '        Return ctl
    '    End If
    '    'case
    '    ctl = Me.cboCase
    '    Me.StatusBarPanel1.Text = "Checking " + IsNull(ctl.Tag, ctl.Name)
    '    uI = ValidateCBO(ctl, IsNull(ctl.Tag, ctl.Name), True, CanAddNew.None)
    '    If uI = usrInput.OK Then
    '        Me.ErrorProvider1.SetError(ctl, "")
    '    Else
    '        SetError(Me.ErrorProvider1, ctl, uI, False)
    '        Return ctl
    '    End If
    '    'contact
    '    ctl = Me.cboContact
    '    Me.StatusBarPanel1.Text = "Checking " + IsNull(ctl.Tag, ctl.Name)
    '    Select Case ValidateCBO(ctl, "Contact", False, CanAddNew.OKBlank)
    '        Case Is = usrInput.Yes
    '            If modGlobalVar.Msg("Did you have this conversation with someone?", MessageBoxButtons.YesNo, "No Contact is selected") = DialogResult.Yes Then
    '                ctl.Focus()
    '                Me.ErrorProvider1.SetError(ctl, "Select the contact with whom you had the conversation")
    '                bOKNoContact = False
    '                Return Me.cboContact
    '            Else
    '                bOKNoContact = True
    '                Me.ErrorProvider1.SetError(ctl, "")
    '                Return ctlNeutral
    '            End If
    '        Case usrInput.Retry
    '            Me.ErrorProvider1.SetError(ctl, "Select the contact with whom you had the conversation")
    '            Return Me.cboContact
    '        Case usrInput.OK
    '            Me.ErrorProvider1.SetError(ctl, "")
    '            Return ctlNeutral
    '    End Select

    '    Me.StatusBarPanel1.Text = "Finished Required Fields check"
    '    Return ctlNeutral

    'End Function

#End Region 'update

#Region "EditButtons"
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
           Handles miSave.Click ', btnCancel.Click
        Me.BindingSource1.EndEdit()
        UpdateDB("Save")

    End Sub
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles miCancel.Click ', btnCancel.Click
        Me.BindingSource1.CancelEdit()
        'cmgr.CancelCurrentEdit()
        bChanged = False
        Me.StatusBarPanel1.Text = "Changes cancelled"
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles miDelete.Click

        If modGlobalVar.msg("CONFIRM DELETE", "The conversation from " & Me.txtConverseDate.Text & NextLine & "will be marked for deletion and the window closed.", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) = DialogResult.Yes Then
            ctlIdentify.Text = "DELETE: " & ctlIdentify.Text
            UpdateDB("Delete Yes")
            ' bDelete = True
            Me.Close()
        Else
            ' bDelete = False
        End If
    End Sub


#End Region 'edit buttons

    Private Sub cboOrg_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboOrg.SelectedIndexChanged
        If cboOrg.SelectedItem > 0 Then

            LoadCases(Me.cboOrg.SelectedItem)
        End If
    End Sub

    Private Sub Label1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label1.Click

    End Sub
End Class