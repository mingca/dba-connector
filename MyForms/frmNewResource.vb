Option Explicit On

Imports System.Data.SqlClient


Public Class frmNewResource

    Dim IsLoaded As Boolean
    Public bDirty As Boolean 'crg combo search
    Public ThisID As Integer
    Dim newID As Integer ' new addressid for address update param

    '===== for Detail form  closing routines
    Dim ctlIdentify As Control 'fields for delete and messages
    Dim ctlNeutral As Control 'will never be invalidated
    Dim objHowClose As Short  ' structCloseMethod 'Object = Me.btnSaveExit 'identify object calling close
    Dim mainDS As DataSet 'for generic module calls like CloseDetailForm
    Dim mainTopic As String 'name of entity of this form - case, contact, etc
    Dim mainBSrce As System.Windows.Forms.BindingSource
    Dim mainTbl As DataTable


#Region "Windows Setup"

    Dim tResourceTypeTableAdapter As New InfoCtr.dsTypeTableAdapters.tTypeTableAdapter
    Dim tResourceSubtypeTableAdapter As New InfoCtr.dsTypeTableAdapters.tSubtypeTableAdapter

#End Region


#Region "Load"

    Private Sub frmNewResource_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        mainTbl = Me.DsNewResource1.tblNewResource
        Me.TblNewResourceTableAdapter.Connection = sc
        tResourceSubtypeTableAdapter.Connection = sc
        tResourceTypeTableAdapter.Connection = sc

        'LOAD CBOs
        tResourceSubtypeTableAdapter.Fill(Me.DsType1.tSubtype)
        tResourceTypeTableAdapter.Fill(Me.DsType1.tType)
        Me.cboType.DataSource = Me.DsType1
        Me.cboSubtype.DataSource = Me.DsType1
        modGlobalVar.LoadCRGCombo(Me.cboKey1)

        TblNewResourceTableAdapter.Fill(Me.DsNewResource1.tblNewResource, ThisID)
    End Sub

    'TODO
    'format phones
    'tag
    'author

    'PANEL VISIBILITY BASED ON RESOURCE TYPE
    Private Sub cboType_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cboType.SelectedIndexChanged

        Select Case cboType.Text
            Case Is = "Book", "Article", "Media", "Software", "Periodical"
                Me.pnlPublisher.Visible = True
                Me.pnlAddress.Visible = False
            Case Else '',"Web Resource" organization, person, event
                Me.pnlPublisher.Visible = False
                Me.pnlAddress.Visible = True
        End Select
    End Sub

#End Region  'load

#Region "Update"
    'btnSaveExit
    Private Sub btnSaveExit_Click(sender As System.Object, e As System.EventArgs) Handles btnSaveExit.Click
        objHowClose = ObjClose.btnSaveExit
        Me.Close()
    End Sub

    'CLOSING & ask user & data validation
    Private Sub frm_Closing(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles MyBase.FormClosing

        If IsNull(editStreet.Text, "") & IsNull(editURL.Text, "") = String.Empty Then
            e.Cancel = True
            msg(MsgCodes.missingInformation)
            Exit Sub
        End If

        DoUpdate()
    End Sub

    'UPDATE
    Private Sub DoUpdate()
        Dim sql As New SqlCommand("", sc)
        sql.CommandType = CommandType.Text
        'MAIN TABLE
        Try
            BindingSource1.EndEdit()
            Me.TblNewResourceTableAdapter.Update(Me.DsNewResource1.tblNewResource)
        Catch ex As Exception
            MsgBox(ex.Message, , "error new resource update")
        End Try

        'insert into auxiliary tables
        If Not SCConnect() Then
            Exit Sub
        End If

        'INDEX TERM
        If Me.editIndexTerm.Text > "" Then
            Dim i As Integer = CreateIndexterm(Me.editIndexTerm.Text)
            sql.CommandText = "INSERT INTO tblResourceIndexTerm (ResourceNum, IndexTermNum) VALUES(" & ThisID & ", " & i & ")"
            Try
                sql.ExecuteScalar()
            Catch ex As Exception
                MsgBox(ex.Message, , "error Flag")
            End Try
        End If

        'AUTHOR
        If IsNull(Me.editFirst.Text, "") & IsNull(Me.editLast, "") = String.Empty Then
        Else
            sql.CommandText = "INSERT INTO tblResourceExtra (ResourceNum, Firstname, Lastname, AuthorEditor) " & _
            "VALUES (" & ThisID & ", '" & IsNull(Me.editFirst.Text, "") & "', '" & IsNull(Me.editLast.Text, "") & "', ' " & IsNull(Me.cboAuthor.SelectedItem, "") & "')"
            Try
                sql.ExecuteScalar()
            Catch ex As Exception
                MsgBox(ex.Message, , "error Extra")
            End Try
        End If

        sc.Close()

        'ADDRESS separate table later
    End Sub

#End Region 'update

#Region "General"

    'FORMAT PHONE
    Private Sub editPhone_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles editPhone800.Leave, editPhone.Leave
        Dim strCntry As String
        If Me.editCountry.Text = String.Empty Then
            strCntry = "USA"
        Else
            strCntry = Me.editCountry.Text
        End If

        If Len(sender.text) > 0 Then
            If modGlobalVar.LeavePhone(sender, strCntry) = False Then
                Me.ErrorProvider1.SetError(sender, "enter valid number")
            Else
                Me.ErrorProvider1.SetError(sender, "")
            End If
        End If

    End Sub

    'CRG Filter
    Private Sub cboKey1_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) _
            Handles cboKey1.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Right Then
            modPopup.SearchCRG(Me, PointToClient(Control.MousePosition), Me.cboKey1)
        End If
    End Sub

#End Region 'general

#Region "Insert Address" 'future
    ''ADD ADDRESS
    'Private Sub btnAddress_Click(sender As System.Object, e As System.EventArgs)
    '    Dim Str As String = "INSERT INTO tblAddress (EntityType, EntityNum, AddressType, Country, CreateDate, CreateStaffNum) " & _
    ' "VALUES ('Resource', " & ThisID & ",'Main', 'USA', GETDATE(), " & usr & "); SELECT @@IDENTITY"

    '    If Not SCConnect() Then
    '        Exit Sub
    '    End If
    '    Dim cmd As New SqlClient.SqlCommand(Str, sc)

    '    Try
    '        newID = cmd.ExecuteScalar()
    '    Catch exce As Exception
    '        modGlobalVar.msg("ERROR: insert address", exce.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    Finally
    '        sc.Close()
    '    End Try
    '    tblAddressTableAdapter.Fill(Me.DsNewResourceWAddress1.tblAddress, newID)
    '    sender.visible = False
    '    Me.editStreet.ReadOnly = False
    '    Me.editCity.ReadOnly = False
    '    Me.editStreet.ReadOnly = False
    '    Me.editCountry.ReadOnly = False
    '    Me.editStreet.Focus()

    'End Sub


    ''INSERT AUTHOR
    'Private Sub btnAuthor_Click(sender As System.Object, e As System.EventArgs)
    '    Dim Str As String = "INSERT INTO tblResourceExtra (ResourceNum) VALUES (" & ThisID & " ) ; SELECT @@IDENTITY"

    '    If Not SCConnect() Then
    '        Exit Sub
    '    End If
    '    Dim cmd As New SqlClient.SqlCommand(Str, sc)

    '    Try
    '        newID = cmd.ExecuteScalar()
    '    Catch exce As Exception
    '        modGlobalVar.msg("ERROR: insert author", exce.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    Finally
    '        sc.Close()
    '    End Try
    '    tblResourceExtraTableAdapter.Fill(Me.DsNewResourceWAddress1.tblResourceExtra, newID)
    '    sender.visible = False
    '    Me.cboAuthor.Enabled = True
    '    Me.cboAuthor.Focus()
    '    Me.editFirst.ReadOnly = False
    '    Me.editLast.ReadOnly = False
    'End Sub


#End Region

End Class