Imports System.Data.SqlClient
Imports System.Text

Public Class frmMainGrantCMG

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
    Dim bDirty As Boolean 'class group radio buttons
    'Dim myRadioButtons As New ArrayList 'for grouped radio buttons

#Region "Load"

    'LOAD
    Private Sub frmMainGrantCMG_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles MyBase.Load
        Me.SuspendLayout()
        SetStatusBarText("loading")

SetConnections:
        Me.MainGrantCMGTableAdapter.Connection = sc

SetDefaults:
        ctlIdentify = Me.txtRemarks
        ctlNeutral = Me.btnHelp
        mainTopic = "CMG Grant Apps"
        mainDS = Me.DsMainGrantCMG
        mainTbl = Me.DsMainGrantCMG.MainGrantCMG
        mainBSrce = Me.MainCMGBindingSource

        'TODO waiting data from ND
        'load coach cbo ITEMS
        'load consultnat cbo ITEMS


LoadStaffCombos:
        modGlobalVar.LoadStaffCombo(Me.cboCallNotified, True, StaffComboChoices.Selectable)
        modGlobalVar.LoadStaffCombo(Me.cboDeterminationStaff, True, StaffComboChoices.Selectable)
        modGlobalVar.LoadStaffCombo(Me.cboClassGroupStaff, True, StaffComboChoices.Selectable)

        'FormatFields:'dates
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

        Me.ResumeLayout()
        SetStatusBarText("Done")

    End Sub

    'LoadOrgBased Combos
    Public Sub LoadOrgBasedCombos(ByVal orgid As Integer)

        Dim strsql As New SqlCommand("SELECT ContactID, ContactStaff FROM vwgetContacts where orgid = " & orgid, sc)
        Dim tblCase As New DataTable("tCase")
        Me.StatusBarPanelID.Text = mainTopic & " ID: " & ThisID.ToString

        If SCConnect() Then
            ''LOAD CONTACTS
            tblContact.Clear()
            Dim dr As SqlDataReader '= strsql.ExecuteReader()
            'dtContact.Load(dr)

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
        Me.cboCase.DataSource = tblCase.DefaultView

        'LOAD  PROJ DIR and TEmpContact
        modGlobalVar.LoadContactCombo(Me.cboPD, tblContact, orgid)
        Me.cboPD.DisplayMember = "ContactStaff"
        Me.cboPD.DataSource = tblContact

        Dim dv5 As DataView
        dv5 = New DataView(tblContact)
        Me.cboTmpContact.DisplayMember = "ContactStaff"
        Me.cboTmpContact.DataSource = dv5

        'LOAD SR CLERGY
        Dim dvClergy As New DataView
        dvClergy.Table = tblContact
        dvClergy.Sort = "Active DESC, PrimaryContact DESC,  Staff DESC, Lastname, FirstName"
        Me.cboClergy.ValueMember = "ContactID"
        Me.cboClergy.DisplayMember = "ContactStaff"
        Me.cboClergy.DataSource = dvClergy

        'Dim ArCongrRep As New ArrayList()
        'ArCongrRep.Add({Me.cboCongRep1, Me.cboCongRep2, Me.cboCongRep3, Me.cboCongRep4})
        '' For Each c As ComboBoxRelaxed In ArCongrRep
        'For i As Integer = 0 To 3
        '    ' ArCongrRep(i).DisplayMember = "ContactStaff"
        '    ' ArCongrRep(i).ValueMember = "ContactID"
        '    ArCongrRep(i).DataSource = dtContact
        'Next
        Dim dv, dv2, dv3, dv4 As DataView
        dv = New DataView(tblContact)
        dv2 = New DataView(tblContact)
        dv3 = New DataView(tblContact)
        dv4 = New DataView(tblContact)
        Me.cboCongRep1.DataSource = dv
        Me.cboCongRep2.DataSource = dv2
        Me.cboCongRep3.DataSource = dv3
        Me.cboCongRep4.DataSource = dv4

        ' GetCaseMgr()
        'dvClergy = Nothing
    End Sub

    'GET CURRENT CASE MANAGER
    Public Sub GetCaseMgr()
        Dim s As String = ""
        If Me.cboCase.SelectedIndex > -1 Then
            Dim scommand As New SqlCommand("Select  stafffirstnamefirst as Staff  from LUstaff INNER JOIN (SELECT CaseID, CaseMgrNum FROM tblCase) as tCase on StaffID = CaseMgrNum AND caseid = " & Me.cboCase.SelectedValue, sc)
            If Not SCConnect() Then
                Exit Sub
            End If
            s = scommand.ExecuteScalar
            sc.Close()
            scommand = Nothing
        End If
        If s = String.Empty Then
            Me.LabelCaseMgrDoes.Text = "CASE MANAGER fills out all the rest..."
        Else
            Me.LabelCaseMgrDoes.Text = IsNull(UCase(s), "Case Manager") & " fills out all the rest..."
        End If
    End Sub

    'called aftr dataset filled
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

        ''Grouped radio buttons
        'Dim r As Integer = CType(Me.MainCMGBindingSource.Current("ClassGroup"), Integer)
        'If r > 0 Then
        '    myRadioButtons.Item(r - 1).checked = True
        'End If


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
        Dim bChanges As Boolean
        '  Dim ctl As Control
        ctlNeutral.Focus()  'safely calls endedit if was in edit mode


        If objHowClose = ObjClose.DontSaveClose Or objHowClose = ObjClose.cancelClose Then '
            GoTo CheckRequiredFields
        End If
LookForChanges:
        bChanges = modGlobalVar.AnyChanges(ctlNeutral, mainBSrce, mainTbl)

AllowCloseWoutSaving:
        If objHowClose = ObjClose.miAskSave Then 'allow close without saving
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
                Case Else
                    GoTo ReleaseForm
            End Select
        End If

CallUpdate:
        If DoUpdate() Then
            e.Cancel = False
        Else
            e.Cancel = True 'don't close form
            GoTo ReleaseForm
        End If

CheckRequiredFields:  'check required fields; allow user to leave anyway if used menu
        Select Case objHowClose
            Case Is = ObjClose.DontSaveClose, ObjClose.miDelete
                e.Cancel = False
                GoTo ReleaseForm
            Case Is = ObjClose.cancelClose
                e.Cancel = True
                GoTo ReleaseForm
            Case Else
                'arCtls = CheckRequired()
                'If arCtls.GetLength(0) > 1 Then 'required info missing
                '    ctl = arCtls(0)
                '    If objHowClose = ObjClose.SaveClose Or e.CloseReason = System.Windows.Forms.CloseReason.UserClosing Then
                '        If ctl Is ctlIdentify Then  'insert default data
                '            ctl.Text = usrName & " " & Today.ToShortDateString
                '            mainBSrce.EndEdit()
                '            Me.MainGrantCMGTableAdapter.Update(mainTbl) 'save default data
                '        End If
                '    End If
                '    Dim strbListFields As New StringBuilder
                '    For x As Integer = 0 To arCtls.GetLength(0) - 2
                '        strbListFields.Append(", " & arCtls(x).Tag)
                '    Next
                '    e.Cancel = Not (modGlobalVar.AskCloseWithMissingInfo(objHowClose, ctl, strbListFields.ToString.Substring(2)))
                'Else
                '    e.Cancel = False
                'End If
        End Select


        '------------------
        '        Dim arCtls(0) As Control
        '        '  Dim ctl As Control
        '        ' Dim bChanges As Boolean

        '        'LookForChanges:
        '        '        bChanges = modGlobalVar.AnyChanges(ctlNeutral, mainBSrce)

        'CheckCallingMethod:
        '        If objHowClose = ObjClose.miAskSave Then
        '            Select Case AskAcceptChanges(bChanges, mainTopic)
        '                Case Is = ObjClose.cancelClose
        '                    e.Cancel = True
        '                    GoTo ReleaseForm
        '                Case Is = ObjClose.DontSaveClose
        '                    ' GoTo validate ' notify of missing fields??
        '                    e.Cancel = False
        '                    GoTo ReleaseForm
        '                Case Is = ObjClose.SaveClose
        '                    e.Cancel = False
        '                    GoTo callupdate
        '            End Select
        '        ElseIf objHowClose = ObjClose.miDelete Then
        '            GoTo callupdate
        '        End If
        'SkipUpdate:  'so LastChangeDate doesn't get updated
        '        If bChanges = False Then
        '            e.Cancel = False
        '            GoTo ReleaseForm
        '        End If
        'CallUpdate:
        '        If DoUpdate() Then
        '            e.Cancel = False
        '        Else                'update didn't work
        '            e.Cancel = True 'don't close form
        '            GoTo ReleaseForm
        '        End If

        'VALIDATE:  'no required fields on this form
        '        Select Case objHowClose
        '            Case Is = ObjClose.DontSaveClose, ObjClose.miDelete
        '                e.Cancel = False
        '                '  GoTo ReleaseForm
        '            Case Is = ObjClose.cancelClose
        '                e.Cancel = True
        '                '  GoTo ReleaseForm
        '            Case Else 'btnSaveExit, SaveClose,, ObjClose.noChanges
        '                'no required fields on this form
        '                e.Cancel = False
        '        End Select

ReleaseForm:
        If e.Cancel = False Then   'user OKs close form
            ClassOpenForms.frmMainCMG = Nothing 'reset global var
            objHowClose = Nothing
        Else
        End If
        arCtls = Nothing
        MouseDefault()
    End Sub

    'UPDATE
    Public Function DoUpdate() As Boolean
        Dim i As Integer

        MouseWait()
        mainBSrce.EndEdit()

        'If mainDS.HasChanges Or bDirty = True Or
        '    mainDS.Tables(0).Rows(0).HasVersion(DataRowVersion.Proposed) Then
        '    mainBSrce.EndEdit()
        'Else
        '    DoUpdate = True
        '    GoTo CloseAll
        'End If

        '   If mainDS.HasChanges = True Or bDirty = True Then 'this catches delete, crgcbo, save, asksave, save/exit, anyclose
UpdateBackend:
        SetStatusBarText("Updating server 1")
        Try
            ' i =
            Me.MainGrantCMGTableAdapter.Update(mainTbl)
            DoUpdate = True
        Catch ex As Exception
            modGlobalVar.msg("ERROR: updating " & mainTopic, ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
            DoUpdate = False
        Finally
            '  modGlobalVar.Msg("main: " & i.ToString& NextLine &  "addresses: " & f.ToString, , "updated")
        End Try
        '        Else
        '       DoUpdate = True 'completed action though no updates to be made
        '      End If

CloseAll:
        SetStatusBarText("Update routine complete")
        MouseDefault()
    End Function

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
            modGlobalVar.msg("ERROR: cancelling changes ", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Me.StatusBarPanel1.Text = "Changes cancelled"
    End Sub

    'DELETE CMG App
    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles miDelete.Click

        ' Select Case modGlobalVar.Msg(mainTopic & ": " + IsNull(ctlIdentify.Text, "") & NextLine & " will be marked for deletion and the window closed.", MessageBoxButtons.YesNo, "CONFIRM DELETE")
        If modGlobalVar.msg("CONFIRM DELETE", mainTopic & ": " & IsNull(ctlIdentify.Text, "") &
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

    ''CLEAR DEADLINE AND GROUP
    'Private Sub dtGrantDeadline_MouseDoubleClick(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles dtGrantDeadline.MouseDoubleClick
    '    Me.dtGrantDeadline.Text = ""
    '    mainBSrce.Current("GrantAppDeadlineDt") = DBNull.Value
    '    mainBSrce.Current("ClassGroup") = DBNull.Value
    '    Me.rbGroup1.Checked = False
    '    Me.rbGroup2.Checked = False

    'End Sub 'edit

#Region "Validation"
    'date fields
    Private Sub DateFields_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles dtAgreementMail.Validating,
    dtAgreementRecd.Validating, dtAppRecd.Validating, dtAppReceiptMail.Validating, dtClassConfirm.Validating, dtDetermination.Validating,
    dtGrantDeadline.Validating, dtGrantAppRecd.Validating, dtNotifyCall.Validating, dtDeposit.Validating
        e.Cancel = modGlobalVar.ValidateDateA(sender)
    End Sub

    'CASE - required field, allow add new
    Private Sub cboCase_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) _
        Handles cboCase.Validating        ' SetError(Me.ErrorProvider1, sender, ValidateCBO(sender, IsNull(sender.tag, sender.name), True, CanAddNew.AddNew), True)

        Dim tblCases As New DataTable
        Dim i As Integer
        Dim strUsrEntry As String
        strUsrEntry = IsNull(Me.cboCase.Text, "")

        Select Case modGlobalVar.ValidateBoundDD(sender, True, Me.ErrorProvider1, ObjClose.CloseByControl)
            Case usrInput.Search
                i = modPopup.NewCase(LocalOrgID, strUsrEntry, ResourceDir.StaffID, "Case", Me, IsNull(Me.fldGotoOrg.Text, ""), 218) 'CMGcrg
                If i > 0 Then
                    modGlobalVar.LoadCaseCombo(Me.cboCase, tblCases, LocalOrgID)
                    Me.ErrorProvider1.SetError(sender, "")
                    Me.cboCase.SelectedValue = i
                End If
            Case usrInput.OK

            Case Else
                'Me.ErrorProvider1.SetError(sender, "select a case name from the dropdown box, or type a new name and fill out the popup form.")
        End Select

    End Sub



#End Region 'validation

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
            Dim pp As New ClassRTBContextMenu(sender)
            pp.Show(Me, PointToClient(Control.MousePosition))
        End If
    End Sub

    'btn Help
    Private Sub btnHelp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
    Handles btnHelp.Click
        modGlobalVar.msg("CMG HELP", "to create a grant, click the button on the Case Detail window.", MessageBoxButtons.OK, MessageBoxIcon.Information)
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

#Region "This Form"

    'CALL GET CURRENT CASE MANAGER
    Private Sub cboCase_Selectedindexchanged(sender As Object, e As System.EventArgs) Handles cboCase.SelectedIndexChanged
        If isLoaded Then
            GetCaseMgr()
        End If
    End Sub

    'set deadline date
    Private Sub rbJan_checkedchanged(sender As System.Object, e As System.EventArgs)
        If isLoaded And sender.checked = True Then
            Me.dtGrantDeadline.Text = sender.text
            mainBSrce.Current("GrantAppDeadlineDt") = sender.text
        End If
    End Sub

    'GENERATE NEW  GRANT
    Private Sub migotoGrant_click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles miGotoGrant.Click, btnGrant.Click
        'todo if already is a grant, open instead of generating new
        If IsNull(mainTbl.Rows(0).Item("GrantIDTxt"), "a").ToString = "a" Then  'grant does not exist yet
            If CType(IsNull(Me.dtGrantAppRecd.Text, "1/1/1911"), Date) > CType("1/1/2012", Date) Then
                Try
                    modPopup.NewGrant(LocalOrgID, "Community Ministry Grant", True, "COMMUNITY MINISTRY GRANT", "CMG", Me.fldGINum.Text, IsNull(Me.cboCase.SelectedValue, 0), Me.dtGrantAppRecd.Text, IsNull(Me.cboClergy.SelectedValue, 0), IsNull(Me.cboPD.SelectedValue, 0), usr)
                    Me.btnGrant.Visible = True
                Catch ex As Exception
                End Try

            Else
                modGlobalVar.msg("ATTENTION: MISSING INFORMATION", "Please fill in date we receive the grant form", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If

        Else
            modGlobalVar.OpenMainGrant(mainTbl.Rows(0).Item("GrantIDTxt"), "CMG Grant", Me.fldGotoOrg.Text, LocalOrgID)
        End If
        'End If
    End Sub

    'create grant if this date filled in
    Private Sub dtGrantAppRecd_Leave(ByVal sender As Object, ByVal e As System.EventArgs) _
    Handles dtGrantAppRecd.Leave
        'validate
        If isLoaded = True Then
        Else
            Exit Sub
        End If

        If Me.cboDetermination.SelectedItem = "Approved" Then
        Else
            ' MsgBox("not approved")
            Exit Sub
        End If
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

    End Sub

    'update class group and appl deadline
    Private Sub rbGroup1_CheckedChanged(sender As RadioButton, e As System.EventArgs) _
        Handles rbGroup1.CheckedChanged, rbGroup2.CheckedChanged

        If isLoaded Then

            If sender.Checked = True Then

                'SET GROUP
                Me.fldGroup.Text = sender.Name.Substring(7)

                'SET DEADLINE
                If sender.Name = "rbGroup1" Then
                    ' mainBSrce.Current("ClassGroup") = "8/1/2017"
                    Me.dtGrantDeadline.Text = "8/1/2017"
                    bDirty = True
                End If
                If sender.Name = "rbGroup2" Then
                    'mainBSrce.Current("ClassGroup") = "11/1/2017"
                    Me.dtGrantDeadline.Text = "11/1/2017"
                    bDirty = True
                End If
                'June(1, 2017) first draft
                'Aug(1, 2017) application deadline

                'Sept(1, 2017)
                'Nov(1, 2017)
                ' mainBSrce.EndEdit()
                ' Me.dtClassConfirm.Focus()

            End If
        End If
    End Sub


#End Region 'this form


#Region "Goto "

    'GOTO ORG
    Private Sub fldGotoOrg_doubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles fldGotoOrg.DoubleClick

        Me.StatusBarPanel1.Text = "Opening Organization Detail window"
        modGlobalVar.OpenMainOrg(LocalOrgID, Me.fldGotoOrg.Text)
        Me.StatusBarPanel1.Text = "Done"
    End Sub

    'GOTO CASE
    Private Sub gotoCase_doubleclick(sender As System.Object, e As System.EventArgs) Handles lblGoToCase.DoubleClick
        If cboCase.SelectedIndex = -1 Then
            modGlobalVar.msg("ATTENTION: Incomplete Entry", "Select a case", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            cboCase.Focus() 'makes this form come ontop of case form
        Else
            modGlobalVar.OpenMainCase(cboCase.SelectedValue, cboCase.Text, Me.fldGotoOrg.Text, LocalOrgID)
        End If
    End Sub

    'GOTO CONTACT
    Private Sub ComboBox_DblClick(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles lblGotoPD.DoubleClick, lblGotoCL.Click, lblGotoTempContact.Click

        Me.StatusBarPanel1.Text = "Opening " & sender.tag & " Detail window"
        OpenMainContact(sender.SelectedValue, sender.DisplayMember, fldGotoOrg.Text, LocalOrgID)
        Me.StatusBarPanel1.Text = "Done"
    End Sub '

#End Region 'goto 

    'Private Sub dtAppRecd_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles dtAppRecd.Validating
    '    'modGlobalVar.Msg("validating")
    '    If sender.Text = String.Empty Or sender.text = " " Then
    '        sender.text = "1/1/1911" ' mainTbl.Rows(0).Item("AppRecdDt") = System.DBNull.Value
    '        bChanged = True
    '    End If
    'End Sub

 
End Class