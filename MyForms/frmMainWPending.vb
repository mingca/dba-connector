Imports System.Data.SqlClient
Imports System.Text

Public Class frmMainWPending

    Dim bDelete, bCancelClose As Boolean
    Public isLoaded As Boolean = False
    Dim bDup As Boolean = False 'check forduplicate entries
    Public bChanged As Boolean = False

    Dim tblOrgs As New DataTable("tblOrgs")
    Dim tblContacts As New DataTable("tblContacts")
    Dim ctlNeutral, ctlIdentify As Control
    Dim iContact, iUID, iEvent, iSID, iOrg, xOrg As Integer
    Dim xOID, xSID As Integer   'for found entries
    Dim objHowClose As New structCloseMethod 'Object = Me.btnSaveExit 'identify object calling close


#Region "Initializing"

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

    End Sub

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
        Forms.Remove(Me)
    End Sub

#End Region 'initializing

#Region "Load"

    'LOAD
    Private Sub MainWPending_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles MyBase.Load

        MouseWait()
        Me.cboSrchContact.SelectedIndex = 0
        Me.cboSrchOrg.SelectedIndex = 0
FormSetup:
        Dim tbx As DataGridTextBoxColumn
        Dim tbs As DataGridTableStyle
        For Each tbs In Me.dgContact.TableStyles
            For Each tbx In tbs.GridColumnStyles
                ' AddHandler tbx.TextBox.MouseDown, New MouseEventHandler(AddressOf dataGridDouble)
                tbx.TextBox.Enabled = False
                tbx.NullText = ""
            Next
        Next
        For Each tbs In Me.dgOrg.TableStyles
            For Each tbx In tbs.GridColumnStyles
                ' AddHandler tbx.TextBox.MouseDown, New MouseEventHandler(AddressOf dataGridDouble)
                tbx.TextBox.Enabled = False
                tbx.NullText = ""
            Next
        Next

        Forms.Add(Me)
        isLoaded = True
        MouseDefault()

    End Sub

    'RELOAD PENDING
    Private Sub cboRegion_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles cboRegion.SelectedIndexChanged
        If isLoaded Then
            If Me.cboRegion.Text = "All Indiana" Then
                Me.tblRegPendingTableAdapter.FillAll(Me.DsRegPending.tblEventRegPending2)
            Else
                Me.tblRegPendingTableAdapter.FillByRegion(Me.DsRegPending.tblEventRegPending2, Me.cboRegion.Text)
            End If
        End If
    End Sub

    'FILL ORG TABLE
    Private Function LoadOrgs(ByVal strWhich As String) As Integer
        Dim cmd As New SqlCommand
        cmd.Connection = sc
        tblOrgs.Clear()
        ClearTxtMatched()
        cmd.CommandText = "SELECT OrgName, Street1, City, State, Zip, Email, Phone, StreetAddress, OrgID FROM tblOrg inner join (SELECT orgID as orgnum FROM vwgetvalidorgs WHERE Active = 1) as vw on tblorg.orgid = orgnum "
        Select Case strWhich
            Case Is = "Search"
                Select Case cboSrchOrg.Text
                    Case Is = "Street"
                        cmd.CommandText = cmd.CommandText & " WHERE Street1 like '" & modGlobalVar.GetWild(Me.txtSrchOrg.Text, Me.chkWildcard.Checked) & "'  OR StreetAddress like '" & Replace(modGlobalVar.GetWild(Me.txtSrchOrg.Text, Me.chkWildcard.Checked), "'", "''") & "'"
                    Case Else 'orgname
                        cmd.CommandText = cmd.CommandText & " WHERE " & Me.cboSrchOrg.Text & " like '" & Replace(modGlobalVar.GetWild(Me.txtSrchOrg.Text, Me.chkWildcard.Checked), "'", "''") & "'"
                End Select
            Case Is = "Contact"
                cmd.CommandText = cmd.CommandText & " WHERE OrgID = " & IsNull(iOrg, 0)
        End Select
        cmd.CommandText = cmd.CommandText & " ORDER BY OrgName, City"
        If Not SCConnect() Then
            Exit Function
        End If
        Try
            Dim myReader As SqlDataReader = cmd.ExecuteReader()
            tblOrgs.Load(myReader)
            myReader.Close()
            sc.Close()
            If tblOrgs.Rows.Count > 0 Then
                dgOrg.DataSource = tblOrgs
                LoadOrgs = tblOrgs.Rows.Count
            Else
                modGlobalVar.msg("No matches found", " ".PadLeft(30, " "), MessageBoxButtons.OK, MessageBoxIcon.Information)
                LoadOrgs = 0
            End If
        Catch ex As Exception
            modGlobalVar.msg("ERROR: load Org table", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
            LoadOrgs = 0
        Finally
        End Try

    End Function

    'FILL CONTACT TABLE
    Private Sub LoadContacts(ByVal strWhat As String)
        Dim cmd As New SqlCommand

        Me.tblContacts.Clear()
        ClearTxtMatched()
        cmd.Connection = sc
        cmd.CommandText = "SELECT   CASE WHEN ActiveContact = 0 OR  activeOrg = 0 THEN '~' ELSE '' END + ISNULL(tContact.Lastname, '') AS Lastname, tContact.FirstName, tContact.Street1, tContact.City, tContact.Zip, tContact.Email,   tContact.PrimaryPhone, tContact.Phone, tContact.CellPhone, tContact.WorkPhone, tContact.ContactID, tContact.OrgNum, tContact.UID, tContact.Goesby FROM   (SELECT  ContactID, OrgNum, Lastname, FirstName, Street1, City, Zip, Email, PrimaryPhone, Phone, CellPhone, WorkPhone, OnlineUID AS UID, Goesby,  Active AS ActiveContact  FROM    tblContact) AS tContact INNER JOIN   (SELECT   vwGetValidContacts.ContactID, vwGetValidContacts.OrgNum, vwo.ActiveOrg  FROM     vwGetValidContacts INNER JOIN  (SELECT    OrgID, Active AS ActiveOrg  FROM  vwGetValidOrgs) AS vwo ON vwGetValidContacts.OrgNum = vwo.OrgID) AS vw ON tContact.ContactID = vw.ContactID"
        Select Case strWhat
            Case Is = "Search"
                Select Case Me.cboSrchContact.Text
                    Case Is = "Street"
                        cmd.CommandText = cmd.CommandText & " WHERE Street1 like '" & modGlobalVar.GetWild(Me.txtSrchContact.Text, Me.chkWildcard.Checked) & "'" ' & "' ORDER BY LastName, Firstname"
                    Case Is = "Email"
                        cmd.CommandText = cmd.CommandText & " WHERE Email like '" & modGlobalVar.GetWild(Me.txtSrchContact.Text, Me.chkWildcard.Checked) & "'" '& " ORDER BY LastName, Firstname"
                    Case Else 'name
                        cmd.CommandText = cmd.CommandText & " WHERE " & Me.cboSrchContact.Text & " like '" & Replace(modGlobalVar.GetWild(Me.txtSrchContact.Text, Me.chkWildcard.Checked), "'", "''") & "'" ' & "' ORDER BY LastName, Firstname"
                End Select

            Case Is = "Org"

                'restore after testing
                'inner join (SELECT ContactID, OrgNum FROM vwgetvalidContacts inner join vwgetvalidorgs on orgnum = orgid
                ' cmd.CommandText = "SELECT  Lastname, Firstname, Street1, City, Zip, Email, PrimaryPhone, Phone, CellPhone, WorkPhone,tblContact.ContactID, tblcontact.OrgNum, onlineuid as UID, Goesby FROM tblContact inner join (SELECT orgid from tblorg) as tOrg on orgnum = orgid  WHERE tblcontact.active = 1 AND tblcontact.orgnum = " & IsNull(xOrg, 0) & " ORDER BY LastName, Firstname"
                cmd.CommandText = cmd.CommandText & " WHERE  tcontact.orgnum = " & IsNull(xOrg, 0)
        End Select
        cmd.CommandText = cmd.CommandText & " ORDER BY lastname, firstname" 'REPLACE(LastName,''','''') , replace(Firstname,''','''')"
        If Not SCConnect() Then
            Exit Sub
        End If

        Try
            Dim myReader As SqlDataReader = cmd.ExecuteReader()
            tblContacts.Load(myReader)
            myReader.Close()
            If tblContacts.Rows.Count > 0 Then
                dgContact.DataSource = tblContacts
            Else
                modGlobalVar.msg("No matches found", " ".PadLeft(30, " "), MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Catch ex As Exception
            modGlobalVar.msg("ERROR: load Contact table", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            sc.Close()
        End Try

    End Sub

#End Region 'load

#Region "General"

    'SELECT ALL TEXT from click
    Private Sub MouseClickTxtbox(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) _
        Handles txtSrchOrg.MouseClick, txtSrchContact.MouseClick
        CType(sender, TextBox).SelectAll()
    End Sub

    'SELECT ALL TEXT from enter
    Private Sub EnterTxtbox(ByVal sender As System.Object, ByVal e As System.EventArgs) _
    Handles txtSrchOrg.Enter, txtSrchContact.Enter
        sender.SelectAll()
    End Sub

    'CLEAR MATCHED texgbox
    Private Sub ClearTxtMatched()
        Me.txtMatched.Text = String.Empty
    End Sub

    'COPY EMAIL
    Private Sub btnCopyEmail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCopyEmail.Click
        My.Computer.Clipboard.SetText(IsNull(Me.fldEmail.Text, ""))
    End Sub

    'OPEN ORG SEARCH WINDOW
    Private Sub btnGotoSrchOrg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnGotoSrchOrg.Click
        Dim frm As New frmSrchOrg
        MouseWait()

        Try
            '   opn = arFormOpen("SrchOrg")  'form is already open
            frm.BringToFront()
            frm.WindowState = FormWindowState.Normal
            GoTo Finish
        Catch ex As System.Exception
        End Try
        frm.Show()
Finish:
        MouseDefault()
    End Sub

    'HELP BUTTON TEXT
    Private Sub btnHelp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles btnHelp.Click, miHelp.Click
        modGlobalVar.msg("Automatic Registration Help", "GOAL: fill small yellow box with contact and organization from our database that matches the registration shown in the large yellow box." & NextLine _
        & "STEPS:" & NextLine & NextLine & "1) SEARCH " & NextLine & "   for the person either by Organization or Contact using the dropdown boxes by each grid." & NextLine _
        & "     If you can't find them, do a fuller search from the regular Contact/Organization Search window, add them if necessary, then return here and repeat the search." & NextLine & NextLine _
        & "2) SELECT the matching Contact Name from lower grid." & NextLine & NextLine _
& "3) COMPARE the names, email, and address to see what didn't match." & NextLine & NextLine _
& "4) EDIT our database if necessary, to correct email address, add Goesby, etc." & NextLine & NextLine _
& "5) CLICK BUTTON to process registration." & NextLine & "  This will insert the Registration, and remove this registration from the Pending List.", MessageBoxButtons.OK, MessageBoxIcon.Information)

    End Sub

    'COPY into CLIPBOARD
    Private Sub fldOID_Click(sender As System.Object, e As System.EventArgs) Handles fldOID.DoubleClick
        'My.Computer.Clipboard.SetText(IsNull(Me.fldOID.Text, ""))
        Clipboard.SetText(IsNull(Me.fldOID.Text, ""))
    End Sub

#End Region 'general

#Region "Open Detail Forms"

    'OPEN CONTACT
    Private Sub dgContact_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgContact.DoubleClick

        Try
            modGlobalVar.OpenMainContact(Me.dgContact.Item(dgContact.CurrentRowIndex, tblContacts.Columns("ContactID").Ordinal), Me.dgContact.Item(dgContact.CurrentRowIndex, tblContacts.Columns("LastName").Ordinal), Me.dgOrg.Item(0, tblOrgs.Columns("OrgName").Ordinal), Me.dgContact.Item(dgContact.CurrentRowIndex, tblContacts.Columns("OrgNum").Ordinal))
        Catch ex As Exception
            'enables Invalid contacts to open
            modGlobalVar.OpenMainContact(Me.dgContact.Item(dgContact.CurrentRowIndex, tblContacts.Columns("ContactID").Ordinal), Me.dgContact.Item(dgContact.CurrentRowIndex, tblContacts.Columns("LastName").Ordinal), "dbl-click to open Organization", Me.dgContact.Item(dgContact.CurrentRowIndex, tblContacts.Columns("OrgNum").Ordinal))
        End Try

    End Sub

    'OPEN ORG
    Private Sub dgOrg_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgOrg.DoubleClick
        'accidental double click triggered when sorting
        Try
            modGlobalVar.OpenMainOrg(Me.dgOrg.Item(dgOrg.CurrentRowIndex, tblOrgs.Columns("OrgID").Ordinal), Me.dgOrg.Item(dgOrg.CurrentRowIndex, tblOrgs.Columns("OrgName").Ordinal))
        Catch ex As Exception
        End Try
    End Sub

#End Region 'Detail forms

#Region "GRIDS"

    'call Load Orgs
    Private Sub txtSrchOrg_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnOrgGo.Enter  'txtSrchOrg.Leave, 
        LoadOrgs("Search")
        Me.tblContacts.Clear()
    End Sub

    'call Load Contacts
    Private Sub txtSrchContact_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnContactGo.Enter ' txtSrchContact.Leave,
        Me.tblOrgs.Clear()
        LoadContacts("Search")
    End Sub

    'PROCESS REGISTRATION
    Private Sub btnAccept_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAccept.Click
        'only works if columns in same order as table
        '  If iContact > 0 Then
        If Me.txtMatched.Text > "" And iContact > 0 Then
        Else
            modGlobalVar.msg("cancelling request", "no contact selected", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If
        iUID = IsNull(Me.fldUID.Text, 0)
        iEvent = Me.fldEventID.Text
        iSID = Me.fldSID.Text

        Dim strMismatch As String
        Dim DrupalName, OurName, DrupalEmail, OurEmail As String
        DrupalName = IsNull(Me.tblRegPendingBindingSource.Current("Name"), "").ToString
        OurName = IsNull(Me.dgContact.Item(Me.dgContact.CurrentRowIndex, tblContacts.Columns("Firstname").Ordinal), "") & " " & IsNull(Me.dgContact.Item(Me.dgContact.CurrentRowIndex, tblContacts.Columns("Lastname").Ordinal), "")
        DrupalEmail = IsNull(Me.tblRegPendingBindingSource.Current("Email"), "").ToString
        OurEmail = IsNull(Me.dgContact.Item(Me.dgContact.CurrentRowIndex, tblContacts.Columns("Email").Ordinal), "")
        '  OurEmail2 = IsNull(Me.dgContact.Item(Me.dgContact.CurrentRowIndex, tblContacts.Columns("Email2").Ordinal), "")

        If (UCase(DrupalEmail) = UCase(OurEmail)) Then
            strMismatch = DrupalEmail & NextLine & OurEmail
            'ElseIf (UCase(DrupalEmail) = UCase(OurEmail2)) Then
            '    strMismatch = DrupalEmail& NextLine &  OurEmail2 & " (secondary email)"
        Else
            strMismatch = NextLine & "Note: MISMATCHING EMAIL: " & NextLine & DrupalEmail & NextLine & OurEmail
        End If

        Select Case modGlobalVar.msg("Confirm linking...", DrupalName & NextLine & OurName & NextLine & strMismatch, MessageBoxButtons.OKCancel, MessageBoxIcon.Question)
            Case Is = MsgBoxResult.Cancel
                GoTo CloseAll
            Case Is = MsgBoxResult.Ok
                Dim sql As New SqlCommand
                sql.Connection = sc
                If Not SCConnect() Then
                    GoTo CloseAll
                End If
UpdateContactUID:
                sql.CommandText = "UPDATE  tblContact SET OnlineUID = " & iUID & " WHERE Contactid = " & iContact
                sql.CommandType = CommandType.Text
                Dim x As Integer = sql.ExecuteNonQuery
                If x = 1 Then   'Success UID update
                Else
                    modGlobalVar.msg("CANCELLING REQUEST", "contact update failed", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    GoTo CloseAll
                End If

InsertRegistration:
                'TODO use datareader so can compare ordernum
                'see if registration already exists
                sql.CommandText = "SELECT ContactNum, isnull( OrderNum,0) as OrderNum, isnull(onlineSID,0) as OnlineSID,  EventNum FROM tbleventreg2 WHERE EventNum = " & Me.fldEventID.Text & " AND ContactNum = " & iContact & " AND (Notes is null OR Notes not like '%DELETE%')"
                'TODO confirm OID, SID
                'TODO get paymentNum of order
                Dim myReader As SqlDataReader = sql.ExecuteReader()
                'x = sql.ExecuteScalar
                If myReader.HasRows Then    'contact alreadly registered at this event
                    myReader.Read()
                    xOID = myReader.GetInt32(1)

                    xSID = myReader.GetInt32(2)
                    myReader.Close()
                    'If x > 0 Then   'contact already registered - update  SID
                    Select Case xOID
                        Case Is = Me.fldOID.Text    'ORDER, CONTACT MATCH, checkSID, delete
                            '    If 
                            modGlobalVar.msg("FYI: registration found", " updating Confirmation Num", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) '= msgboxResult.Ok Then
                            GoTo UpdateSID
                            '  Else
                            '  GoTo CloseAll
                            '  End If
                        Case Is > 0      'ORDER MISMATCH show different order num, flag
                            modGlobalVar.msg("OOPS: other registration found", "this contact also registered with Order# " & xOID.ToString & NextLine & "confirmation#: " & xSID.ToString & NextLine & NextLine & "click the REJECT button to delete this pending.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            GoTo CloseAll
                        Case Else       'registration exists but oid blank, update, checkSID, delete
                            ' If 
                            modGlobalVar.msg("FYI: registration found", "updating Numbers", MessageBoxButtons.OK, MessageBoxIcon.Information) '= msgboxResult.Ok Then
                            'update oid
                            Try
                                sql.CommandText = "UPDATE  tblEventReg2 SET OrderNum = " & Me.fldOID.Text & " WHERE Contactnum = " & iContact & " AND EventNum = " & Me.fldEventID.Text & " AND (OrderNum is null OR OrderNum < 1)"
                                x = sql.ExecuteNonQuery()
                            Catch ex As Exception
                                modGlobalVar.msg("ERROR: OID", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
                            End Try
                            GoTo updateSID
                            ' Else
                            '  GoTo CloseAll
                            '  End If
                    End Select
                Else 'NOT FOUND, INSERT pulling from DrupalMaster, delete 
                    myReader.Close()
                    x = 0
                    sql.CommandType = CommandType.StoredProcedure
                    ' sql.Parameters.Add("@oid", SqlDbType.Int).Value = Me.fldOID.Text
                    sql.Parameters.Add("@SID", SqlDbType.Int).Value = Me.fldSID.Text
                    sql.Parameters.Add("@ContactID", SqlDbType.Int).Value = iContact
                    sql.Parameters.Add("@EventID", SqlDbType.Int).Value = Me.fldEventID.Text
                    sql.Parameters.Add("@Staff", SqlDbType.VarChar).Value = usrFirst
                    sql.Parameters.Add("@Which", SqlDbType.VarChar).Value = "Registration"
                    sql.CommandText = "[EventPendingApply]"
                    Try
                        x = sql.ExecuteScalar
                    Catch ex As Exception
                        modGlobalVar.msg("ERROR: inserting registration", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        GoTo CloseAll
                    End Try

                    If x > 0 Then   'Success insert registration
                        Me.fldName.Text = "PROCESSED " & Me.fldName.Text
                        GoTo DeletePending
                    Else
                        modGlobalVar.msg("ERROR: Not Inserted" & x.ToString, " check that Order exists in Order table", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        GoTo CloseAll
                    End If
                    '   modGlobalVar.Msg(Me.tblRegPendingBindingSource.Current("SID").ToString, , "about to delete")
                End If

UpdateSID:
                Select Case xSID
                    Case Is = Me.fldSID.Text 'do nothing
                    Case Is > 0
                        modGlobalVar.msg("PROBLEM:", "this registration has a different Confirmation # in InfoCtr:" & NextLine _
                        & "InfoCtr Order#: " & xOID.ToString & NextLine & "InfoCtr Confirmation#: " & xSID.ToString, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        GoTo CloseAll
                    Case Else
                        Try
                            sql.CommandText = "UPDATE  tblEventReg2 SET OnlineSID = " & Me.fldSID.Text & " WHERE Contactnum = " & iContact & " AND EventNum = " & Me.fldEventID.Text & " AND (OnlineSID is null or OnlineSID < 1)"
                            '  modGlobalVar.Msg(sql.CommandText, , "query")
                            x = sql.ExecuteNonQuery()
                            ' modGlobalVar.Msg(x.ToString, , "SID updated")
                        Catch ex As Exception
                            modGlobalVar.msg("ERROR: SID", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        End Try
                End Select
                GoTo DeletePending
        End Select

DeletePending:
        Try
            Me.tblRegPendingTableAdapter.Delete(Me.fldSID.Text)
            Me.tblRegPendingBindingSource.RemoveCurrent()  'does not remove from table
        Catch ex As Exception
            modGlobalVar.msg("ERROR: deleting pending ", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

CloseAll:
        Try
            sc.Close()
            DrupalEmail = Nothing
            OurEmail = Nothing
            DrupalName = Nothing
            OurName = Nothing
            strMismatch = Nothing
        Catch ex As Exception
        End Try
    End Sub

    'Goto Srch Org btn
    Private Sub txtSrchOrg_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtSrchOrg.KeyDown
        If e.KeyData.ToString() = "Return" Then
            Me.btnOrgGo.Focus()
        End If

    End Sub

    'Goto srch Contact btn
    Private Sub txtSrchContact_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtSrchContact.KeyDown

        Select Case e.KeyData.ToString
            Case Is = "Return"  ' = System.Windows.Forms.Keys.Enter Then

                Me.btnContactGo.Focus()
            Case Else
        End Select
    End Sub

    'REFRESH CONTACT GRID FROM ORG
    Private Sub dgOrg_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs) _
        Handles dgOrg.CurrentCellChanged
        xOrg = IsNull(Me.dgOrg.Item(dgOrg.CurrentRowIndex, tblOrgs.Columns("OrgID").Ordinal), 0)
        LoadContacts("Org")
    End Sub

    'REFRESH ORG GRID FROM CONTACT
    Private Sub dgContact_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs) _
        Handles dgContact.CurrentCellChanged ', dgContact.MouseDown 'dgContact.Click,  ; dgContact.MouseDown = last cell clicked, not current one';'dgContact.GotFocus    - loop

        If Me.dgContact.CurrentRowIndex > -1 Then
            '  Me.dgContact.Select(Me.dgContact.CurrentRowIndex)
            iContact = Me.dgContact.Item(Me.dgContact.CurrentRowIndex, tblContacts.Columns("ContactID").Ordinal)
            iOrg = Me.dgContact.Item(Me.dgContact.CurrentRowIndex, tblContacts.Columns("OrgNum").Ordinal)
        Else
            Exit Sub
        End If
        Try
            If LoadOrgs("Contact") > 0 Then
                LoadTxtMatched()
            Else
                '  Me.dgContact.UnSelect(dgContact.CurrentRowIndex)
            End If
        Catch ex As Exception
        End Try
        '   
    End Sub

    'LOAD MATCHING TEXTBOX
    Private Sub LoadTxtMatched()
        'only works if columns in same order as table
        Dim strbFound As New StringBuilder
        Dim strStreet As String = IsNull(Me.dgOrg.Item(0, tblOrgs.Columns("Street1").Ordinal), "")
        Dim strPhysical As String = IsNull(Me.dgOrg.Item(0, tblOrgs.Columns("StreetAddress").Ordinal), "")

        strbFound.Append(IsNull(Me.dgContact.Item(Me.dgContact.CurrentRowIndex, tblContacts.Columns("Firstname").Ordinal), "") & " " & IsNull(Me.dgContact.Item(Me.dgContact.CurrentRowIndex, tblContacts.Columns("Lastname").Ordinal), "") & "     (" & IsNull(Me.dgContact.Item(Me.dgContact.CurrentRowIndex, tblContacts.Columns("Goesby").Ordinal), " ") & ")")
        strbFound.Append(NextLine & IsNull(Me.dgOrg.Item(0, tblOrgs.Columns("OrgName").Ordinal), ""))
        strbFound.Append(NextLine & IsNull(strStreet, ""))
        strbFound.Append(NextLine & IsNull(Me.dgOrg.Item(0, tblOrgs.Columns("City").Ordinal), "") & ", " & IsNull(Me.dgOrg.Item(0, tblOrgs.Columns("State").Ordinal), "") & " " & IsNull(Me.dgOrg.Item(0, tblOrgs.Columns("Zip").Ordinal), ""))
        If strPhysical = String.Empty Or strStreet = String.Empty Then
        Else
            If strStreet.Substring(0, 1) = "P" Then
                ' modGlobalVar.Msg("yes PO ")
                strbFound.Append(NextLine & "       " & strPhysical & "")
            Else
                '  modGlobalVar.Msg("no PO")
            End If
        End If
        strbFound.Append(NextLine & NextLine & IsNull(Me.dgContact.Item(Me.dgContact.CurrentRowIndex, tblContacts.Columns("Email").Ordinal), "no email"))

        Me.txtMatched.Text = strbFound.ToString
        strbFound = Nothing

    End Sub

    'CLEAR GRIDS
    Private Sub OnCurrent(ByVal sender As Object, ByVal e As System.EventArgs) _
        Handles BindingNavigatorMoveNextItem.Click, BindingNavigatorMoveFirstItem.Click, BindingNavigatorMoveLastItem.Click, BindingNavigatorMovePreviousItem.Click
        tblOrgs.Clear()
        tblContacts.Clear()
        Me.txtMatched.Clear()
    End Sub

#End Region 'grids

#Region "FormSpecific"

    'STORE REJECTED PENDINGs so aren't re-imported
    Private Sub btnReject_Click(sender As System.Object, e As System.EventArgs) Handles btnReject.Click

        Dim strReason As String
        Dim sql As New SqlCommand("[RegistrationReject]", sc)

        sql.Parameters.Add("@What", SqlDbType.VarChar).Value = "pending"
        sql.Parameters.Add("@origSID", SqlDbType.Int).Value = Me.fldSID.Text

        strReason = InputBox("Enter a reason why this is not an acceptable registration.", "Confirm: Delete this Pending?", "replaced by another order. ")
        If strReason > " " Then
            Try
                sql.Parameters.Add("@Why", SqlDbType.VarChar).Value = strReason
                sql.Parameters.Add("@GoodOID", SqlDbType.Int).Value = InputBox("", "Enter replacement Order#", IsNull(xOID, 0))
                sql.Parameters.Add("@GoodSID", SqlDbType.Int).Value = InputBox("", "Enter replacement Confirmation #", IsNull(xSID, 0))
                '  MsgBox(sql.Parameters("@origSID").Value.ToString, , Me.fldSID.Text)
                If SCConnect() Then
                    ' If sql.ExecuteNonQuery() > 0 
                    sql.ExecuteNonQuery()

                    ' Else : GoTo CloseAll
                    ' End If
                End If
            Catch ex As System.Exception
                ' Dim email As New ClassEmail
                ' SendEmail.SendHTMLEmail(DBAdmin.StaffEmail, "DELETE PENDING: ", Environment.UserName & " SID: " & Me.fldSID.Text.ToString) '"App: InfoCtr&#xaConnection String:\n" & sc.ConnectionString.ToString & "&#x0aComputer Name: " & IsNull(Environ("ComputerName"), "na") & "%OD%OAError: " & exc.Message)

                ' modGlobalVar.msg("ERROR: entering BadOID ", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
                GoTo CloseAll
            End Try
            Try
                ' Me.tblRegPendingTableAdapter.Delete(Me.fldSID.Text)
                Me.tblRegPendingBindingSource.RemoveCurrent()  'does not remove from table, sproc does that
            Catch ex As Exception
                modGlobalVar.msg("ERROR: deleting pending ", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        Else            'user cancelled reject
        End If
CloseAll:
        sc.Close()
        sql = Nothing
    End Sub
#End Region


End Class