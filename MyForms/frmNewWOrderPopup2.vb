Imports System.Data.SqlClient

Public Class frmNewWOrderPopup2

    Dim bDelete, bCancelClose, isLoaded As Boolean
    Dim bDup As Boolean = False 'check forduplicate entries
    Public bChanged As Boolean = False
    Dim mainTbl As DataTable
    Dim ctlNeutral, ctlIdentify As Control
    Dim objHowClose As New structCloseMethod 'Object = Me.btnSaveExit 'identify object calling close


#Region "Load"

    Private Sub NewWOrderPopup_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        MouseWait()
        Me.TblWRegPOpupTableAdapter.Connection = sc
        Try
            'Me.cboEvent.DisplayMember = "EventName"
            'Me.cboEvent.ValueMember = "EventID"
            Me.cboEvent.DataSource = tblWEvents
        Catch ex As System.Exception
            modGlobalVar.Msg("ERROR: loading events  ", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        bChanged = False

        mainTbl = Me.DsWRegPopup.tblWRegPopup
        ctlIdentify = Me.cboContact 'Me.lblRegistrantName
        ctlNeutral = Me.btnHelp

        isLoaded = True
        MouseDefault()

    End Sub

    Public Sub LoadOrgDD(ByVal strRegion As String)
        '  On Error GoTo errorMsg
        Dim tOrg As New DataTable
        Dim qryFill As New SqlCommand("[GetOrgsByRegion]", sc)
        '   Console.WriteLine("loading org " & strRegion) '~~
        qryFill.CommandType = System.Data.CommandType.StoredProcedure
        qryFill.Parameters.Add("@Region", SqlDbType.VarChar).Value = strRegion
        If Not SCConnect() Then
            Exit Sub
        End If

        Try
            tOrg.Load(qryFill.ExecuteReader)
            Me.cboOrg.DataSource = tOrg
        Catch ex As Exception
            modGlobalVar.msg("ERROR: loading org dd ", strRegion & NextLine & ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
            ' LoadOrgDD = False
            GoTo CloseAll
        End Try
        '  GOTO LAST ORG USED
        If iPayeeOrgNum > 0 Then
            Try
                Dim foundRows() As Data.DataRow
                foundRows = tOrg.Select("OrgID = " & iPayeeOrgNum)
                Me.cboOrg.SelectedIndex = Me.cboOrg.FindStringExact(foundRows(0)("Org").ToString)
                'why doesn't this generate selected index changed?
                LoadContactDD(Me.cboOrg.SelectedValue)
            Catch ex As Exception
                modGlobalVar.msg("error finding popup org", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        Else
            Me.cboOrg.SelectedIndex = -1
            Me.cboOrg.SelectedIndex = -1
        End If
CloseAll:
        sc.Close()
        qryFill = Nothing

    End Sub

    Public Function LoadContactDD(ByVal ID As Integer) As Boolean
        '   On Error GoTo errorMsg
        Dim tContact As New DataTable
        ' Dim qryFill As New SqlCommand("[GetContactDDAddress]", sc)
        Dim qryFill As New SqlCommand("[luContactNames]", sc)
        qryFill.CommandType = System.Data.CommandType.StoredProcedure
        qryFill.Parameters.Add("@IDval", SqlDbType.Int).Value = ID
        '    qryFill.Parameters.Add("@Refresh", SqlDbType.Bit).Value = bActive
        If Not SCConnect() Then
            Return False
        End If
        Try
            tContact.Load(qryFill.ExecuteReader)
            Me.cboContact.DataSource = tContact
            Me.cboContact.SelectedIndex = -1
            LoadContactDD = True
        Catch ex As Exception
            modGlobalVar.msg("ERROR: loading contact ", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
            LoadContactDD = False
        End Try
        ' mdGlobalVar.LoadDataTable(dtRegistrant, qryFill)
        qryFill = Nothing
        ' Me.cboContact.SelectedIndex = -1
    End Function

#End Region 'load

#Region "Update Main"

    'SAVE
    Private Sub miSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles miSave.Click
        '  mainBSrce.EndEdit()
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
        '  modGlobalVar.Msg(objHowClose.sh.ToString, , "in close form")
        objHowClose.sh = modGlobalVar.CloseDetailForm(Me.Tag, IsNull(ctlIdentify.Text, "no registrant"), Me.TblWRegPOpupBindingSource, ctlNeutral, mainTbl, bChanged)
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

        MouseWait()
        Select Case objHowClose.sh
            Case Is = ObjClose.miDelete
                bCancelClose = False
                GoTo ReleaseForm
            Case Is = ObjClose.DontSaveClose, ObjClose.noChanges
                bCancelClose = False
                GoTo ReleaseForm
        End Select


CheckRequiredFields:

        '     If Me.cboContact.SelectedIndex > -1 Then
        'CheckforChanges:
        If objHowClose.sh > 1 Then 'anychanges has already run
        Else
            If modGlobalVar.AnyChanges(ctlNeutral, Me.TblWRegPOpupBindingSource, mainTbl, bChanged) = True Then
                ' modGlobalVar.Msg("yes changes")
                Try
                    UpdateDB("Closing")

                    '      iPayeeOrgNum = IsNull(Me.cboOrg.SelectedValue, 0)
                Catch ex As Exception
                    modGlobalVar.msg("ERROR: running update from close", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            Else
                ' modGlobalVar.Msg("no changes")
            End If
            bCancelClose = False
        End If
        ' Else 'don't save empty(record)
        ' If modGlobalVar.Msg("This record will be deleted", MessageBoxButtons.YesNo, "MISSING REGISTRANT") = DialogResult.Yes Then
        ' Me.ErrorProvider1.SetError(ctlIdentify, "")
        ' DoDelete()
        ' bCancelClose = False
        ' Else
        ' Me.ErrorProvider1.SetError(ctlIdentify, "Select a registrant")
        ' bCancelClose = True
        ' End If
        '   End If

ReleaseForm:
        '  arCtls = Nothing
        '  ctl = Nothing
        If bCancelClose = False Then   'user OKs close form
            '   ClassOpenForms.frmfrmMainRegistration = Nothing 'reset global var
            objHowClose = Nothing
        Else
        End If
        '   modGlobalVar.Msg("out close form")
        MouseDefault()
        e.Cancel = bCancelClose

    End Sub

    'UPDATE
    Public Sub UpdateDB(ByVal How As String)

        MouseWait()
        If Me.cboContact.SelectedIndex = -1 Then 'don't save emtpy record
            If modGlobalVar.msg("MISSING INFORMATION", "No Contact selected." & NextLine & "This record will be deleted.", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) = DialogResult.OK Then
                Me.rtbNotes.Text = "DELETE " & Me.rtbNotes.Text
            Else
                Exit Sub
            End If
        End If
        Try
            Me.TblWRegPOpupBindingSource.EndEdit()
            Me.DsWRegPopup.EnforceConstraints = False
            Me.TblWRegPOpupTableAdapter.Update(Me.DsWRegPopup.tblWRegPopup)
            bChanged = False

        Catch ex As Exception
            modGlobalVar.msg("ERROR: Save new order popup", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
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

    'RIGHT CLICK MENU
    Private Sub textbox_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) _
        Handles rtbReason.MouseDown, rtbNotes.MouseDown
        If e.Button = System.Windows.Forms.MouseButtons.Right Then
            Dim pp As New ClassRTBContextMenu(sender)
            pp.Show(Me, PointToClient(Control.MousePosition))
        End If
    End Sub

    'BTN HELP
    Private Sub btnHelp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnHelp.Click, miHelp.Click
        modGlobalVar.msg("REGISTRATION HELP", "SELECT Contact Name from DropDown box.  Inactive names are indicated with ~ and are at the bottom of the list." & NextLine & NextLine & "FOR MULTIPLE REGISTRATIONS: click the menu item 'Multiple Event Registrations'", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    'DATE DELETE
    Private Sub txtRegDate_Leave(ByVal sender As Object, ByVal e As System.EventArgs)
        modGlobalVar.DeleteDate(sender, Me.DsWRegPopup.tblWRegPopup.Rows(0).Item(sender.DataBindings.Item("text").BindingMemberInfo.Bindingfield))
    End Sub

#End Region

#Region "EDIT Buttons"

    'DELETE
    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles miDelete.Click, btnDelete.Click
        'modGlobalVar.Msg("...removed by request of management."& NextLine &  "If this registration was entered in error, email the delete request to " & DBAdmin.StaffName & ". "& NextLine &  "If the person cancelled their registration, simply click the 'Cancelled' checkbox on their Registration Detail page.", , "Delete option... .")
        If modGlobalVar.msg("CONFIRM DELETE", "Registration for: " + IsNull(Me.cboContact.Text, "") & NextLine & " will be marked for deletion and the window closed.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            Me.rtbNotes.Text = "DELETE: " & IsNull(Me.rtbNotes.Text, "")
            objHowClose.sh = ObjClose.miDelete
            UpdateDB("delete")
            Me.Close()
        End If
    End Sub

    'CANCEL
    Private Sub miCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles miCancel.Click
        Me.tblWRegPopupBindingSource.CancelEdit()
    End Sub

    'COPY
    Private Sub miCopy_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        modGlobalVar.Msg("not available", "ask " & DBAdmin.StaffName & "for assistance.", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

#End Region

#Region "form specific"

    'RELOAD CONTACTS
    Private Sub cboOrg_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboOrg.SelectedIndexChanged
        If isLoaded And cboOrg.SelectedIndex > -1 Then
            LoadContactDD(Me.cboOrg.SelectedValue)
            'iPayeeOrgNum = Me.cboOrg.SelectedValue
        End If
    End Sub

    'RELOAD ORG DD
    Private Sub cboRegion_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboRegion.SelectedIndexChanged
        If cboRegion.SelectedIndex > -1 Then
            grRegion = cboRegion.Text
            LoadOrgDD(Me.cboRegion.Text)
        End If
    End Sub

    'VALIDATE REQUIRED DROPDOWNS
    Private Sub cboMethod_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs)

        If modGlobalVar.ValidateBoundDD(sender, True, Me.ErrorProvider1, ObjClose.CloseByControl) = usrInput.OK Then
            e.Cancel = False
        Else
            e.Cancel = True
            sender.DroppedDown = True
        End If

    End Sub

    'CHECK FOR DUPLICATE
    Private Sub cboContact_SelectionChangeCommitted(ByVal sender As Object, ByVal e As System.EventArgs) _
        Handles cboContact.SelectionChangeCommitted

        If Me.cboContact.SelectedIndex = -1 Then
            Exit Sub
        End If

        Dim iReg As Integer
        If Not SCConnect() Then
            Exit Sub
        End If

        Dim cmdR As New SqlClient.SqlCommand("SELECT RegistrationID FROM tblEventReg2 WHERE (EventNum = " & Me.cboEvent.SelectedValue & ") AND (ContactNum = " & Me.cboContact.SelectedValue & ") ", sc) ' AND (registrationID <> " & Me.fldRegistrationID.Text & ")", sc)
        iReg = cmdR.ExecuteScalar
        sc.Close()
        cmdR.Dispose()

        If IsNull(iReg, 0) = 0 Then 'INSERT
            GetClergy()
            '     OpenRegPopup(Me.fldEventNum.Text, Me.cboContact.SelectedValue, Me.fldRegistrationID.Text, Me.fldOrderNum.Text)
        Else    'EDIT
            modGlobalVar.msg("STOP  - DUPLICATE registration " & iReg.ToString, "Registration already exists for this person for this event " & NextLine & "choose someone else", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            cboContact.SelectedIndex = -1
            Me.cboContact.Focus()
        End If

        'GetMailingAddress:  '? or does this take too long for offsite folks

    End Sub

    'GET CLERGY & GENDER
    Private Sub GetClergy()

        Dim strSQL As New SqlCommand("SELECT  ContactID, Gender, CASE WHEN Staff = N'Pastoral' THEN 'C' ELSE 'L' END AS ClergyLay FROM tblContact WHERE (ContactID = " & Me.cboContact.SelectedValue & " )", sc)
        If sc.State = ConnectionState.Open Then
        Else
            Try
                sc.Open()
            Catch ex As Exception
                '  modGlobalVar.Msg(ex.Message, MessageBoxIcon.Error, "couldn't open connection")
                Exit Sub
            End Try
        End If

        Dim drd As SqlDataReader = strSQL.ExecuteReader(CommandBehavior.CloseConnection)
        While drd.Read
            If IsDBNull(drd(1)) Then
                ' modGlobalVar.Msg("no Gender")
            Else
                Me.cboGender.SelectedIndex = cboGender.FindString(drd.GetString(1))
            End If
            If IsDBNull(drd(2)) Then
                '   modGlobalVar.Msg("no staff")
            Else
                Me.cboClergy.SelectedIndex = cboClergy.FindString(drd.GetString(2))

            End If
        End While
        drd.Close()

    End Sub

    'REFRESH CONTACT DROPDOWN
    Private Sub btnRefreshContacts_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnRefreshContacts.Click
        Dim s As String = "ContactID = '" & IsNull(Me.cboContact.SelectedValue, 0) & "'"
        Dim t As String
        'Me.cboRegistrant.SelectedValue
        Try
            Dim r As DataRow() = tblRegistrant.Select(s)
            t = r(0).Item("ContactOrgCity")
        Catch ex As System.Exception
            ' modGlobalVar.Msg(x.Message)
        End Try

        If modGlobalVar.LoadRegistrantDD(False) Then
            If s Is String.Empty Then
                Exit Sub
            End If
            Me.isLoaded = False
            Me.cboContact.SuspendLayout()
            '   Me.cboRegistrant.SelectedIndex = Me.cboRegistrant.f(s)
            Try
                Me.cboContact.SelectedIndex = Me.cboContact.FindStringExact(t)
            Catch ex As Exception
                '  modGlobalVar.Msg(ex.Message, , "can't select original value")
            End Try

            Me.cboContact.ResumeLayout()
            Me.isLoaded = True
        End If
        s = Nothing
        t = Nothing

    End Sub

    'NEW CONTACT
    Private Sub btnNewContact_Click(sender As Object, e As System.EventArgs) Handles btnNewContact.Click
        Dim str As String = "INSERT INTO tblContact(FirstName,OrgNum, CreateStaffNum, CreateDate) VALUES (N'new person', " & Me.cboOrg.SelectedValue & ", " & usr & ", GETDATE()); SELECT @@IDENTITY"

        Dim cmd As New SqlClient.SqlCommand(str, sc)
        Dim newID As Integer

        If Not SCConnect() Then
            Exit Sub
        End If

        newID = cmd.ExecuteScalar()
        modGlobalVar.OpenMainContact(newID, "Entering new contact", Me.cboOrg.Text, Me.cboOrg.SelectedValue)
    End Sub

#End Region'form specific

End Class