Imports System.Data.SqlClient
Imports System.Text

'1/16 USED BY ContactDetail, EventDetail, OrderDetail
'gathers all 3 above ids from user, as well as date of registration
'before inserting new reg into BE
Public Class frmNewWReg2


    '===== for Detail form  closing routines
    Dim ctlIdentify As Control 'fields for delete and messages
    Dim ctlNeutral As Control 'will never be invalidated
    Dim isLoaded As Boolean = False
    Dim objHowClose As Short ' structCloseMethod 'Object = Me.btnSaveExit 'identify object calling close

#Region "Initialize"

    Public Sub New()
        MyBase.New()
        'This call is required by the Windows Form Designer.
        InitializeComponent()
    End Sub

#End Region


#Region "Load"

    'LOAD
    Private Sub frmNewWReg2_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles Me.Load

        Me.SuspendLayout()
SetDefaults:
        ctlNeutral = Me.btnHelp 'Me.btnRefreshContacts
FormSetup:

        Me.ResumeLayout()
        isLoaded = True
        SetStatusBarText("Done ")

    End Sub

    'RELOAD let calling form call this so is done before assign value
    Public Function Loadcombos() As Boolean
        'CONTACTS
        If tblRegistrant.Rows.Count > 0 Then
        Else
            modGlobalVar.LoadRegistrantDD(False)
        End If
        Me.cboRegistrant.DataSource = tblRegistrant
        'EVENTS
        modGlobalVar.LoadWEventDD("upcoming")
        Me.cboEvent.DataSource = tblWEvents
        Me.cboEvent.SelectedIndex = -1
        Me.cboRegistrant.SelectedIndex = -1
        Return True
    End Function

    'PRESET EVENT from calling form
    Public Sub SelectCombo(ByRef ctl As ComboBox, ByVal id As Integer)
        ctl.SelectedValue = id
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
        objHowClose = ObjClose.DontSaveClose
        Me.Close()

    End Sub

    'CLOSING
    Private Sub frm_Closing(ByVal sender As Object, ByVal e As FormClosingEventArgs) _
         Handles MyBase.FormClosing

        Dim ctl As Control
        Dim arCtls() As Control

        'check required fields; allow user to leave anyway if used menu
        Select Case objHowClose
            Case Is = ObjClose.DontSaveClose, ObjClose.miDelete
                e.Cancel = False
                GoTo ReleaseForm
            Case Is = ObjClose.cancelClose
                e.Cancel = True
                GoTo ReleaseForm
            Case Else 'btnSaveExit, SaveClose,, ObjClose.noChanges
ACheckRequired:
                arCtls = CheckRequired()
                If arCtls.GetLength(0) > 1 Then 'required info missing
                    ctl = arCtls(0)

                    Dim strbListFields As New StringBuilder
                    For x As Integer = 0 To arCtls.GetLength(0) - 2
                        strbListFields.Append(", " & arCtls(x).Tag)
                    Next
                    e.Cancel = True
                    GoTo releaseform
                End If
BCheckDuplicateRegistration:
                If CheckDuplicateRegistration() = True Then
CEnterRegistrationinBE:
                    UpdateDB()
                    e.Cancel = False
                Else
                    e.Cancel = True
                End If
        End Select

ReleaseForm:
    End Sub

    'INSERT IN BE
    Public Function UpdateDB() As Boolean
        MouseWait()
        modPopup.InsertRegistration(Me.fldOrderNum.Text, Me.cboEvent.SelectedValue, Me.cboRegistrant.SelectedValue, Me.fldRegisterDate.Text)
        MouseDefault()
    End Function

    'CHECK REQUIRED FIELDS w Error Provider
    Private Function CheckRequired() As Control()
        'add the Neutral control to the array last to indicate rest are ok if its the first one
        Dim arCtls(0) As Control
        Dim ctl As Control
        Dim i As Integer = 0

        If Me.cboEvent.SelectedIndex = -1 Then
            ctl = cboEvent
            Me.ErrorProvider1.SetError(ctl, "please enter a " & IsNull(ctl.Tag, ctl.Name))
            arCtls(i) = ctl
            i = i + 1
            ReDim Preserve arCtls(arCtls.GetUpperBound(0) + 1)
        End If

        If Me.cboRegistrant.SelectedIndex = -1 Then
            ctl = cboRegistrant
            Me.ErrorProvider1.SetError(ctl, "please enter a " & IsNull(ctl.Tag, ctl.Name))
            arCtls(i) = ctl
            i = i + 1
            ReDim Preserve arCtls(arCtls.GetUpperBound(0) + 1)
        End If

        If IsNull(Me.fldOrderNum.Text, 0) = "0" Then
            ctl = Me.fldOrderNum
            Me.ErrorProvider1.SetError(ctl, "please enter a " & IsNull(ctl.Tag, ctl.Name))
            arCtls(i) = ctl
            i = i + 1
            ReDim Preserve arCtls(arCtls.GetUpperBound(0) + 1)
        End If

        If Me.fldRegisterDate.Text = String.Empty Then
            ctl = Me.fldRegisterDate
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

    'call check duplicate registration
    Private Sub cboEvent_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles cboEvent.SelectionChangeCommitted, cboRegistrant.SelectionChangeCommitted
        If Me.cboEvent.SelectedIndex = -1 Or Me.cboRegistrant.SelectedIndex = -1 Then
            Exit Sub
        End If
        CheckDuplicateRegistration()
    End Sub

    'CHECK FOR DUPLICATE REGISTRATION
    Private Function CheckDuplicateRegistration() As Boolean
        Dim iEvent As Integer = Me.cboEvent.SelectedValue
        Dim iReg As Integer 'found registrationID
        Dim cmdR As New SqlClient.SqlCommand("SELECT RegistrationID FROM tblEventReg2 WHERE (EventNum = " &
            iEvent & ") AND (ContactNum = " & Me.cboRegistrant.SelectedValue & ") ", sc)
        'AND (Notes is null or Notes not like 'Delete%')", sc)
        If Not SCConnect() Then
            Return False
        End If
        If cmdR.ExecuteScalar > 1 Then
            modGlobalVar.msg("STOP  -  see Registration# " & iReg.ToString, "Registration already exists for this person for this event.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            CheckDuplicateRegistration = False
            Me.cboEvent.Focus()
        Else
            CheckDuplicateRegistration = True
        End If

        sc.Close()
        cmdR.Dispose()
    End Function

    'date validation
    Private Sub txtRegDate_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) _
        Handles fldRegisterDate.Validating
        e.Cancel = modGlobalVar.ValidateDateA(sender, Me.ErrorProvider1)
    End Sub

#End Region 'validating

#Region "EDIT Buttons"

    'DELETE
    'todo didn't admins get permission to do real delete?
    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles miDelete.Click
        If modGlobalVar.msg("CONFIRM DELETE", "Registration will be deleted and the window closed.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            objHowClose = ObjClose.miDelete
            Me.Close()
        End If
    End Sub

    'RELOAD EVENT DD
    Private Sub btnRefreshEvents_Click(sender As Object, e As System.EventArgs) Handles btnRefreshEvents.Click
        modGlobalVar.LoadWEventDD("all")
    End Sub

#End Region

#Region "General"

    'NEW ORDER #
    Private Sub btnNewOrder_Click(sender As System.Object, e As System.EventArgs) Handles btnNewOrder.Click
        Me.fldOrderNum.Text = modPopup.NextOrderNumber()
    End Sub

    'SET STATUS BAR LEFT TEXT
    Private Sub SetStatusBarText(ByVal str As String)
        Me.StatusBarPanel1.Text = str
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
            Dim pp As New ClassRTBContextMenu(sender)
            pp.Show(Me, PointToClient(Control.MousePosition))
        End If
    End Sub

    'HELP BUTTON
    Private Sub btnHelp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnHelp.Click, miHelp.Click
        modGlobalVar.msg("Registration Help", "SELECT Event Name and Contact Name from DropDown boxes.  (Inactive contacts are indicated with ~ and are at the bottom of the list)." & NextLine &
                         "Order Number and date are also required.", MessageBoxButtons.OK, MessageBoxIcon.Information)
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

    'Today's date
    Private Sub fldRegisterDate_DoubleClick(sender As Object, e As System.EventArgs) Handles fldRegisterDate.DoubleClick
        Clipboard.SetText(Today()) 'note If is NOW, Undo doesn't work!!
        sender.SelectAll()
        sender.Paste()
    End Sub

#End Region 'general

End Class
