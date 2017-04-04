Imports System.Data.SqlClient

Public Class frmReviewOrgSimple

    '...filter grid
    Dim htivw As DataGridView.HitTestInfo
    Dim dvM As DataView
    Dim Source1 As New BindingSource
    Dim strDGM, strDGS As String
    Dim tbl As Object
    Dim iGridM, iGridS As Integer    'count rows of [filtered] grid
    Dim bFilter As Boolean = False 'filter is set
    Dim isLoaded As Boolean = False
    Dim iMainID As Integer = 0

    ' Dim relationrelOrgContact As Global.System.Data.DataRelation = New Global.System.Data.DataRelation("relOrgContact", New Global.System.Data.DataColumn() {Me.tabletblOrg.OrgIDColumn}, New Global.System.Data.DataColumn() {Me.tabletblContact.OrgNumColumn}, False)
    '  Me.relationrelOrgContact = New Global.System.Data.DataRelation("relOrgContact", New Global.System.Data.DataColumn() {Me.tabletblOrg.OrgIDColumn}, New Global.System.Data.DataColumn() {Me.tabletblContact.OrgNumColumn}, false)


#Region "LOAD"
    Private Sub frmReviewOrgSimple_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '   ReloadGrids()
        strDGM = "ORGANIZATIONS"
        strDGS = "CONTACTS"

        If usr = DBAdmin.StaffID Then    'only catharine can delete from here
            Try
                Me.grdvwMain.Columns("CreateDateDataGridViewTextBoxColumn").ReadOnly = False
                Me.grdvwMain.Columns("CreateStaffNumDataGridViewTextBoxColumn").ReadOnly = False
                Me.grdvwMain.Columns("LastChangeDateDataGridViewTextBoxColumn").ReadOnly = False
                Me.grdvwMain.Columns("LastChangeStaffnumDataGridViewTextBoxColumn").ReadOnly = False

                Me.grdvwSec.Columns("ContactCreateDate").ReadOnly = False
                Me.grdvwSec.Columns("ContactCreateStaffNum").ReadOnly = False
                Me.grdvwSec.Columns("ContactLastChangeDate").ReadOnly = False
                Me.grdvwSec.Columns("ContactLastChangeStaffnum").ReadOnly = False
            Catch ex As Exception
            End Try

            '   Else
            'For Each dgv As DataGridView In Me.Controls
            '    dgv.AllowUserToDeleteRows = True
            'Next
        End If

        isLoaded = True

        '  LoadLowerGrids()

    End Sub

    'FILL DS
    Private Sub tscboRegion_SelectedIndexChange(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles tscboRegion.SelectedIndexChanged
        If Not isLoaded Then
            Exit Sub
        End If
        ' tsSaveItem.PerformClick()
        Me.ZipToolStripTextBox.Text = String.Empty
        LoadGrids()

        'Select Case Me.tscboWhat.Text
        '    Case "Organizations"
        '        LoadMainGrid()
        '    Case "Contacts"
        '        LoadSecGrid()
        'End Select

    End Sub

    Private Sub LoadLowerGrids()
        tsSaveItem.PerformClick()
        ' Me.TblConversationTableAdapter.FillByRegion(Me.dsReviewOrgIntern.tblConversation, GetRegion)
        ' Me.TblRegistrationTableAdapter.Fill(Me.dsReviewOrgIntern.tblRegistration)
        ' Me.TblCaseConversationTableAdapter.Fill(Me.dsReviewOrgIntern.tblCaseConversation)
        ' Me.TblCaseTableAdapter.Fill(Me.dsReviewOrgIntern.tblCase)
        '  Me.TblGrantTableAdapter.Fill(Me.dsReviewOrgIntern.tblGrant)

        'TODO: This line of code loads data into the 'dsReviewOrgIntern.vwCntRefContact' table. You can move, or remove it, as needed.
        '   Me.VwCntRefContactTableAdapter.FillContactRef(Me.dsReviewOrgIntern.vwCntRefContact)
        'TODO: This line of code loads data into the 'dsReviewOrgIntern.vwCntRefOrg' table. You can move, or remove it, as needed.
        '   Me.VwCntRefOrgTableAdapter.FillOrgRef(Me.dsReviewOrgIntern.vwCntRefOrg)
    End Sub

#End Region 'load

#Region "EDIT BUTTONS"
    'SAVE
    Private Sub TblOrgBindingNavigatorSaveItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        UpdateDB()
    End Sub

    'SAVE ON CLOSE
    Private Sub frm_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) _
            Handles MyBase.FormClosing
        ' Me.TblOrgBindingNavigatorSaveItem.PerformClick()
        UpdateDB()
    End Sub


    'EDIT LAST CHANGED FIELDS
    Private Sub grdvwMain_CellValueChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) _
            Handles grdvwMain.CellValueChanged
        'TODO change this to rowstate not each cell edit
        If Not isLoaded Then
            Exit Sub
        End If

        Select Case sender.name
            Case Is = "grdvwMain"

                '   grdvwMain.Item("LastChangeDate", grdvwMain.CurrentRow.Index).Value = Today
                '   grdvwMain.Item("LastChangeStaffNum", grdvwMain.CurrentRow.Index).Value = usr

                Dim drw As DataRow = CType(Me.dsReviewOrgIntern.tblOrg, DataTable).Rows.Find(iMainID)
                '    If drw.RowState = DataRowState.Modified Then
                If usr = DBAdmin.StaffID Then
                Else
                    drw.Item("LastChangeDate") = Now
                    drw.Item("LastChangeStaffNum") = usr
                End If
                drw = Nothing
                'End If
            Case Is = "grdvwSec"
                ' grdvwSec.Item("LastChangeDate", grdvwSec.CurrentRow.Index).Value = Today
                ' grdvwSec.Item("LastChangeStaffNum", grdvwSec.CurrentRow.Index).Value = usr
                '   modGlobalVar.Msg(Me.grdvwContacts.CurrentRow.Index.ToString, , Me.grdvwContacts.Item(0, Me.grdvwContacts.CurrentRow.Index).Value.ToString)
                Dim drw As DataRow
                'Try
                drw = CType(Me.dsReviewOrgIntern.tblContact, DataTable).Rows.Find(Me.grdvwSec.Item(0, Me.grdvwSec.CurrentRow.Index).Value)
                '    If drw.RowState = DataRowState.Modified Then
                If usr = DBAdmin.StaffID Then
                Else
                    drw.Item("LastChangeDate") = Now
                    drw.Item("LastChangeStaffNum") = usr
                End If
                drw = Nothing
                '        drw = Nothing
                '    End If
                'Catch ex As Exception
                'End Try
        End Select
        '    Me.grdvwSec.Rows(Me.grdvwSec.CurrentRow).cells(, value = Today)

        '    DataGridView1.Rows(intTheTargetRow).Cells(intTheTargetCell).Value = strTheString



        '        Try
        '            drw = CType(Me.dsReviewOrgIntern.tblContact, DataTable).Rows.Find(Me.grdvwSec.Item(0, Me.grdvwSec.CurrentRow.Index).Value)
        '            drw.Item("LastChangeDate") = Today
        '            drw.Item("LastChangeStaffNum") = usr
        '        Catch ex As Exception
        '            Exit Sub
        '        End Try

        '        drw = Nothing
        '    Case Else
        '        modGlobalVar.Msg(sender.name, , "Not found")
        'End Select

    End Sub
#End Region  'edit buttons

#Region "Update"

    'RUN UPDATE
    Private Sub UpdateDB()
        Me.Validate()
        Me.tblOrgBindingSource.EndEdit()
        Me.TblContactBindingSource.EndEdit()
        '    Me.RelOrgCaseBindingSource.EndEdit()
        '   Me.RelContactRegistrationBindingSource1.EndEdit()    'TblRegistrationBindingSource.EndEdit()
        '   Me.RelContactConversationBindingSource1.EndEdit()

        '   Me.CaseConvBindingSouce.EndEdit()
        ' Me.RelCaseConversationBindingSource1.EndEdit()

        Try
            Me.TblOrgTableAdapter.Update(Me.dsReviewOrgIntern.tblOrg)
        Catch ex As Exception
            modGlobalVar.Msg("ERROR: Org update", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Try
            Me.TblContactTableAdapter.Update(Me.dsReviewOrgIntern.tblContact)
        Catch ex As Exception
            modGlobalVar.Msg("ERROR: Contact update", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        '     Me.TblCaseTableAdapter.Update(Me.dsReviewOrgIntern.tblCase)
        'Try
        '    Me.TblConversationTableAdapter.Update(Me.dsReviewOrgIntern.tblConversation)
        'Catch ex As Exception
        '    modGlobalVar.Msg(ex.Message, , "conversation")
        'End Try
        'Try
        '    Me.TblCaseConversationTableAdapter.Update(Me.dsReviewOrgIntern.tblCaseConversation)
        'Catch ex As Exception
        '    modGlobalVar.Msg(ex.Message, , "covnersation1")
        'End Try
        'Try
        '    Me.TblRegistrationTableAdapter.Update(Me.dsReviewOrgIntern.tblRegistration)
        'Catch ex As Exception
        '    modGlobalVar.Msg(ex.Message, , "registration")
        'End Try
        'Try
        '    Me.TblGrantTableAdapter.Update(Me.dsReviewOrgIntern.tblGrant)
        'Catch ex As Exception
        '    modGlobalVar.Msg(ex.Message, , "grant")
        'End Try


    End Sub

#End Region 'update

#Region "FILL SETUP"
    'SET VISIBILITY OF GRIDS
    Private Sub tscboWhat_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles tscboWhat.SelectedIndexChanged
        Me.tsBtnSearch.Text = ""
        ClearFilter("Main")
        ClearFilter("Secondary")
        Select Case tscboWhat.SelectedItem
            Case "Organizations", "Orgs to Delete"
                '  Me.tsBtnSearch.Text = "Search Org Name"
                '   LoadMainGrid()
                Me.pnlOrg.Visible = True
                '  Me.grdvwSec.DataSource = Me.tblOrgBindingSource
                '   Me.grdvwSec.DataMember = "tblorg.relOrgContact" '"RelOrgContactBindingSource"  '
                '     Me.pnlSec.Location = New System.Drawing.Point(14, 435)
                '  Me.pnlSec.Size = New System.Drawing.Size(1132, 300)
                Me.lblMainGrid.Text = strDGM '"Organizations"
            Case "Contacts", "Contacts to Delete"
                Me.pnlOrg.Visible = False
                ' Me.tsBtnSearch.Text = "Search Last Name"
                '    Me.grdvwSec.DataSource = Me.dsReviewOrgIntern
                '     Me.grdvwSec.DataMember = "tblContact"
                '       Me.pnlSec.Location = New System.Drawing.Point(14, 45)
                '275,675,1025
                'Me.pnlSec.Size = New System.Drawing.Size(1132, 675)
                Me.lblMainGrid.Text = strDGS    '"Contacts"
                '  LoadSecGrid()
            Case Else
                modGlobalVar.Msg("NOT FOUND", tscboWhat.SelectedItem, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Select
        LoadGrids()
    End Sub
#End Region 'setup

#Region "Load Grids"

    'LOAD ORG AND CONTACT GRID
    Private Sub LoadGrids()
        Dim strRegion As String
        If isLoaded Then
        Else
            Exit Sub
        End If
        MouseWait()
        Me.tsSaveItem.PerformClick()

        If Me.tscboRegion.SelectedIndex > -1 Then
            Me.dsReviewOrgIntern.EnforceConstraints = False
            strRegion = GetRegion()
            If bFilter Then
                ClearFilter("Main")
                ClearFilter("Secondary")
            End If

            Select Case Me.tscboWhat.SelectedItem
                Case Is = "Organizations"
                    Me.BS3 = Me.RelOrgContactBindingSource
                    Try
                        Me.TblContactTableAdapter.FillbyRegion(Me.dsReviewOrgIntern.tblContact, strRegion)
                        Me.TblOrgTableAdapter.FillbyRegion(Me.dsReviewOrgIntern.tblOrg, strRegion)

                    Catch ex As Exception
                        modGlobalVar.Msg("ERROR: filling contacts fillbyOrg", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try

                Case Is = "Orgs to Delete"
                    Me.BS3 = Me.RelOrgContactBindingSource
                    Me.TblOrgTableAdapter.FillByDelete(Me.dsReviewOrgIntern.tblOrg, strRegion)
                    Try
                        Me.TblContactTableAdapter.FillByDeleteOrg(Me.dsReviewOrgIntern.tblContact, strRegion)
                    Catch ex As Exception
                        modGlobalVar.Msg("ERROR: filling contacts fillbyOrg", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try
                    FilterContacts(Me.grdvwMain, 0, 0)

                Case Is = "Contacts"
                    Me.BS3 = Me.TblContactBindingSource
                    Try
                        Me.TblContactTableAdapter.FillbyRegion(Me.dsReviewOrgIntern.tblContact, strRegion)
                        Me.TblOrgTableAdapter.FillbyRegion(Me.dsReviewOrgIntern.tblOrg, strRegion)

                    Catch ex As Exception
                        modGlobalVar.Msg("ERROR: filling contacts fillbyRegion", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try
                    Me.tsBtnSearch.Text = "Search ContactID"
                Case Is = "Contacts to Delete"
                    Me.BS3 = Me.TblContactBindingSource
                    Try
                        Me.TblContactTableAdapter.FillByDeleteOrg(Me.dsReviewOrgIntern.tblContact, strRegion)
                    Catch ex As Exception
                        modGlobalVar.Msg("ERROR: filling contacts fillbyToDelete", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try
                    Me.tsBtnSearch.Text = "Search ContactID"

            End Select
        Else
        End If

        MouseDefault()
    End Sub

    ''LOAD SECONDARY GRIDS - not required with  relation
    'Protected Sub LoadSecGrid()
    '    If Me.tscboRegion.SelectedIndex > -1 Then
    '        'Me.TblContactTableAdapter.Fill(Me.dsReviewOrgIntern.tblContact, 0)
    '        If Me.tscboRegion.SelectedItem = "All Regions" Then
    '            Me.TblContactTableAdapter.FillByRegion(Me.dsReviewOrgIntern.tblContact, "%")
    '        Else
    '            Me.TblContactTableAdapter.FillByRegion(Me.dsReviewOrgIntern.tblContact, Me.tscboRegion.SelectedItem)
    '        End If
    '    End If
    'End Sub

    'COUNT SECONDARY ITEMS
    Private Sub GetCounts()
        '    ' contacts, recommendations, grants
    End Sub

    Private Function GetRegion() As String
        If Me.tscboRegion.SelectedItem = "All Regions" Then
            Return "%"
        Else
            Return Me.tscboRegion.SelectedItem
        End If
    End Function

    Private Sub CountContacts()
        Dim cmdCntID As New SqlCommand
        '    Dim SQLRecommend As String = "SELECT COUNT(RecommendID) FROM tblResourceRecommend WHERE CaseNum = " & iCol 'Contact   'Me.txtCurrent.Text
        '    Dim SQLGrant As String = "SELECT COUNT(GrantIDtxt) FROM tblGrant WHERE CaseNum = " & iCol 'Org 'Me.txtCurrent.Text
        '    Dim SQLConvers As String = "SELECT COUNT(ConversID) FROM tblConversation WHERE CaseNum = " & iCol 'Contact   'Me.txtCurrent.Text
        Dim SQLContact As String = "SELECT COUNT(ContactID) FROM tblContact WHERE OrgNum = " & iMainID 'F& " AND tblcontact.Active = 1" ' & Me.chkActive.Checked 'Contact   'Me.txtCurrent.Text

        '    Dim drd As SqlDataReader
        cmdCntID.Connection = sc
        If Not SCConnect() Then
            Exit Sub
        End If

        'GET RECOMMEND COUNT
        Dim i As Integer = 0
        cmdCntID.CommandText = SQLContact
        Try
            i = cmdCntID.ExecuteScalar
        Catch ex As Exception
            modGlobalVar.Msg("ERROR: can't run count contacts", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        '     Me.lblSecGrid.Text = i.ToString() & "  " & strDGS

        '    'GET RECOMMEND COUNT
        '    Dim i As Integer = 0
        '    cmdCntID.CommandText = SQLRecommend
        '    Try
        '        i = cmdCntID.ExecuteScalar
        '    Catch ex As Exception
        '        modGlobalVar.Msg(ex.Message, MessageBoxIcon.Error, "can;t run getrecommend")
        '    End Try
        '    Me.cntRecommend.Text = i.ToString()

        '    'GET CONVERSATION COUNT
        '    cmdCntID.CommandText = SQLConvers
        '    i = 0
        '    Try
        '        i = cmdCntID.ExecuteScalar
        '    Catch ex As Exception
        '        modGlobalVar.Msg(ex.Message, , "can't run get Conversations")
        '    End Try
        '    Me.cntConvers.Text = i.ToString()

        '    'GET GRANT COUNT
        '    cmdCntID.CommandText = SQLGrant
        '    i = 0
        '    Try
        '        i = cmdCntID.ExecuteScalar
        '    Catch ex As Exception
        '        modGlobalVar.Msg(ex.Message, , "can't run get Grants")
        '    End Try
        '    Me.cntConvers.Text = i.ToString()
        '    Me.cntGrant.Text = i.ToString

        '    cmdCntID.Dispose()
        '    sc.Close()
        '    'cmdCntID.CommandText = SQLGrant
        '    'cmdCntID.CommandType = CommandType.StoredProcedure
        '    'cmdCntID.Parameters.Add("IDFld", SqlDbType.Text)
        '    'cmdCntID.Parameters.Add("IDVal", SqlDbType.Int)
        '    'cmdCntID.Parameters("IDFld").Value = "CaseNum"
        '    'cmdCntID.Parameters("IDVal").Value = iCol
        '    'i = Nothing
        '    'drd = cmdCntID.ExecuteReader
        '    'Try
        '    '    While drd.Read
        '    '        i += 1
        '    '    End While
        '    '    ' drd.Close()
        '    'Catch ex As Exception
        '    '    modGlobalVar.Msg(ex.Message, , "can't run get grants")
        '    'Finally
        '    '    drd.Close()
        '    '    sc.Close()
        '    'End Try


    End Sub

#End Region 'load grids

#Region "Grid Filter"

    ' CAPTURE RIGHT MOUSE CLICK TO FILTER APPROPRIATE GRID
    Protected Sub grdAll_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) _
            Handles grdvwMain.MouseDown, grdvwSec.MouseDown   'grdSecond1.MouseDown, grdSecond3.MouseDown, grdvwMain.MouseDown, grdSecond2.MouseDown

        Dim strHdr As String    'text for grid header
        ' Dim i As Integer

        htivw = sender.HitTest(e.X, e.Y)

SetTbl:
        Select Case sender.tag
            Case Is = "Main"
                tbl = Me.dsReviewOrgIntern.tblOrg
                '  strHdr = strDGM
            Case Is = "Secondary"
                tbl = Me.dsReviewOrgIntern.tblContact
                ' strHdr = strDGS
            Case Else
                Exit Sub
        End Select

RightClick:
        If e.Button = System.Windows.Forms.MouseButtons.Right Then     'filter

            'FILTER or CLEAR FILTER if is CELL or GRID
            If htivw.Type = DataGrid.HitTestType.Cell Then

                'CHECK FOR NULLS
                If IsDBNull(sender.Item(htivw.ColumnIndex, htivw.RowIndex).value) Then 'nulls cause filter error
                    modGlobalVar.Msg(MsgCodes.filterEmpty)
                    Exit Sub
                End If
                'CHECK FOR APOSTROPHE
                If CType(sender.item(htivw.ColumnIndex, htivw.RowIndex).value, String).IndexOf("'") > 0 Then
                    modGlobalVar.Msg(MsgCodes.filterApostrophe)
                    Exit Sub
                End If
                SetSearchLabel(sender.tag, htivw.ColumnIndex)

                'CALL FILTER
                grdFilter(sender, Me.dsReviewOrgIntern, tbl, dvM)
            Else        'clear filter
                ClearFilter(sender.tag)
            End If
LeftClick:
        Else    'is not right mouse button

            If htivw.Type = DataGrid.HitTestType.Cell Then
                If sender.tag = "Main" Then
                    iMainID = sender.Item(0, IsNull(sender.CurrentRow.Index, 0)).value
                End If
                SetSearchLabel(sender.tag, htivw.ColumnIndex)
            End If
        End If


        strHdr = Nothing
    End Sub

    'FILTER METHOD
    Private Sub grdFilter(ByVal grd As Object, ByVal ds As Object, ByVal tbl As Object, ByVal dv As DataView)
        Dim strFilter As String
        'Dim myColumns As GridColumnStylesCollection

        If grd.name = "grdvwMain" Then
            '    Me.TblOrgTableAdapter.FillByZip(Me.dsReviewOrgIntern.tblOrg, grd.Item(htivw.ColumnIndex, htivw.RowIndex).value)

            ' setting filter on binding source does nothing when datasource is directly on dataset
            'if use binding source for datasource, grandchild table does not resync.
            Try
                strFilter = grd.columns(htivw.ColumnIndex).datapropertyname & " = '" & grd.Item(htivw.ColumnIndex, htivw.RowIndex).value & "'"
                Me.tblOrgBindingSource.Filter = strFilter
            Catch ex As Exception
                modGlobalVar.Msg("ERROR: filtering main grid", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
            iGridM = tblOrgBindingSource.Count
            Me.tslblFilter.Text = Me.lblMainGrid.Text & " filtered by " & grd.columns(htivw.ColumnIndex).HeaderText '& " = " & grd.Item(hti.Row, hti.Column)
            Me.lblMainGrid.Text = iGridM.ToString & "/" & tbl.Rows.Count.ToString & "  " & Me.lblMainGrid.Text
            Me.grdvwMain.CurrentCell = grdvwMain.FirstDisplayedCell
        Else
            If grd.name = "grdvwSec" Then
                Try
                    strFilter = grd.columns(htivw.ColumnIndex).datapropertyname & " = '" & grd.Item(htivw.ColumnIndex, htivw.RowIndex).value & "'"
                    '  modGlobalVar.Msg(strFilter)
                    Me.BS3.Filter = strFilter
                Catch ex As Exception
                    modGlobalVar.Msg("ERROR: filtering secondary grid", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
                Me.tslblFilter.Text = Me.lblMainGrid.Text & " filtered by " & grd.columns(htivw.ColumnIndex).HeaderText '& " = " & grd.Item(hti.Row, hti.Column)
                '   Me.lblSecGrid.Text = TblContactBindingSource.Count.ToString & "/" & iGridS & "  " & Me.lblMainGrid.Text    'tbl.Rows.Count.ToString & "  " & strHdr
            End If
        End If
        Me.StatusBarPanel1.Text = "Filter set"
        strFilter = Nothing
    End Sub

    'REMOVE MAIN GRID FILTER
    Private Sub ClearFilter(ByVal WhichGrid As String)
        Select Case WhichGrid
            Case "Main"
                Me.tblOrgBindingSource.RemoveFilter()
                Me.lblMainGrid.Text = Me.dsReviewOrgIntern.tblOrg.Rows.Count.ToString & "  " & strDGM
                iGridM = Me.dsReviewOrgIntern.tblOrg.Rows.Count
                Me.tslblFilter.Text = " "
                iMainID = 2 '??

                Me.grdvwMain.CurrentCell = grdvwMain.FirstDisplayedCell
                FilterContacts(Me.grdvwMain, 0, 0)
            Case "Secondary"
                If Me.tscboWhat.SelectedItem = "Organizations" Then
                    Me.TblContactBindingSource.Filter = "Orgnum = " & iMainID
                Else
                    Me.TblContactBindingSource.RemoveFilter()
                End If
                ' iGridS = Me.dsReviewOrgIntern.tblContact.Rows.Count
                '   Me.lblSecGrid.Text = iGridS.ToString & " " & strDGS
                Me.tslblFilter.Text = " "
        End Select
        If Me.pnlOrg.Visible = True Then
            Me.lblMainGrid.Text = strDGM
        Else
            Me.lblMainGrid.Text = strDGS
        End If
        Me.StatusBarPanel1.Text = "Filter Cleared"
    End Sub

    'lbl
    Private Sub lblMainGrid_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) _
        Handles lblMainGrid.MouseClick
        Dim strOrg
        Select Case sender.ToString
            Case "Main"
                If e.Button = System.Windows.Forms.MouseButtons.Right Then
                    ClearFilter("Main")
                Else
                    modGlobalVar.OpenMainOrg(iMainID, grdvwMain.Item(2, grdvwMain.CurrentRow.Index).Value)
                End If
            Case "Secondary"
                If e.Button = System.Windows.Forms.MouseButtons.Right Then
                    ClearFilter("Secondary")
                Else
                    If iMainID > 0 Then
                        strOrg = Me.dsReviewOrgIntern.tblOrg.Rows.Find(iMainID)
                    Else
                        strOrg = "Orgname and phone"
                    End If
                    modGlobalVar.OpenMainContact(grdvwSec.Item(0, grdvwSec.CurrentRow.Index).Value, grdvwSec.Item(2, grdvwSec.CurrentRow.Index).Value, strOrg, grdvwSec.Item(1, grdvwSec.CurrentRow.Index).Value)
                End If
        End Select
    End Sub

    ''CLEAR SECONDARY GRIDS
    'Private Sub ClearSecGrids()
    '    Try
    '        Me.dsReviewOrgIntern.tblContact.Clear()
    '    Catch e As Exception
    '    End Try

    'End Sub



#End Region 'filter grid

#Region "SEARCH DATASET"
    'set text on search button
    Private Sub SetSearchLabel(ByVal s As String, ByVal i As Integer)

        If s = "Main" Then
            Me.tsBtnSearch.Text = "Find " & grdvwMain.Columns(i).DataPropertyName
        Else
            If s = "Secondary" And Me.tscboWhat.SelectedItem = "Contacts" Then
                Me.tsBtnSearch.Text = "Find " & grdvwSec.Columns(i).DataPropertyName
            End If
        End If
    End Sub

    'SET 'LIKE' SEARCH ON DATASET BASED ON USER STRING
    Private Sub FilterDataset(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles tsBtnSearch.Click

        Dim strFilter As String

        If Me.tsTxtSearch.Text = "Enter filter string" Then
            Exit Sub
        End If

        '  1 do Contact grid same way
        '      2 add Active option

        tsSaveItem.PerformClick()

        'CONTACTS
        '--GO BY WHICH GRID IS SELECTED INSTEAD
        ' modGlobalVar.Msg(Me.tscboWhat.SelectedItem, , "tscboWhat.selecteditem")

        If Me.tscboWhat.SelectedItem = "Contacts" Then
            If Me.dsReviewOrgIntern.tblContact.Rows.Count > 1 Then
                Dim i As Integer = 1
                Try
                    i = grdvwSec.CurrentCell.ColumnIndex
                Catch ex As Exception
                End Try
                Select Case i
                    Case Is > 0
                        strFilter = grdvwSec.Columns(htivw.ColumnIndex).DataPropertyName & " like '" & modGlobalVar.GetWild(tsTxtSearch.Text, False) & "'"
                        Me.BS3.Filter = strFilter
                    Case Is = 0 '?
                        Me.tsBtnSearch.Text = "Find ContactID"
                        If IsNumeric(tsTxtSearch.Text) Then
                            strFilter = " ContactID = " & tsTxtSearch.Text
                        Else
                            modGlobalVar.Msg("incompatible search type", "select another field to search, or enter only a number", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        End If
                End Select

                Me.TblContactBindingSource.Filter = strFilter
                Me.tsBtnViewAll.Visible = True
                Me.tsBtnSearch.Text = "Find " & Me.grdvwSec.Columns(htivw.ColumnIndex).DataPropertyName()

                Exit Sub
            End If
        End If

        'ORGS
        '  If iMainID > 0 Then

        If Me.dsReviewOrgIntern.tblOrg.Rows.Count > 1 Then
            ' If Me.grdvwMain.SelectedRows.Count > 0 Then
            'modGlobalVar.Msg(Me.grdvwMain.Columns(htivw.ColumnIndex).DataPropertyName, , Me.grdvwMain.CurrentCell.ColumnIndex.ToString)
            Dim i As Integer = 1
            Try
                i = grdvwMain.CurrentCell.ColumnIndex
            Catch ex As Exception
            End Try
            Select Case i
                Case Is > 0
                    strFilter = grdvwMain.Columns(htivw.ColumnIndex).DataPropertyName & " like '" & modGlobalVar.GetWild(tsTxtSearch.Text, False) & "'"
                    ' Me.tblOrgBindingSource.Filter = strFilter
                    ' Me.tsBtnViewAll.Visible = True
                Case Is = 0 '?
                    Me.tsBtnSearch.Text = "Find OrgID"
                    If IsNumeric(tsTxtSearch.Text) Then
                        strFilter = " OrgID = " & tsTxtSearch.Text
                        '  Me.tblOrgBindingSource.Filter = strFilter
                    Else
                        modGlobalVar.Msg("incompatible search type", "select another field to search, or enter only a number", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    End If
            End Select
            '   Else 'no rows selected
            'use default cell of Orgname
            '      Me.tsBtnSearch.Text = "Filter Org Name"
            '     strFilter = "OrgName like '" & mdGlobalVar.GetWild(tsTxtSearch.Text, False) & "'"
            '      modGlobalVar.Msg(strFilter)

            '   End Select
            Me.tblOrgBindingSource.Filter = strFilter
            Me.tsBtnViewAll.Visible = True
        End If 'row selected

        'Else
        '    modGlobalVar.Msg("please select a column to search and try again", MessageBoxIcon.Exclamation, "Cannot complete search")
        '  End If 'empty dataset
        '   modGlobalVar.Msg(strFilter, , "Filter")
        '  End If
        '  iMainID = 0

        ' Me.tsBtnSearch.Text = "filter list"
    End Sub

    'CLEAR FILTER
    Private Sub tsViewAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles tsBtnViewAll.Click
        tsSaveItem.PerformClick()
        Me.tblOrgBindingSource.RemoveFilter()
        Me.TblContactBindingSource.RemoveFilter()
        Me.tsBtnViewAll.Visible = False
    End Sub

#End Region 'find

#Region "Grid DoubleClick"

    Private Sub grdvwMain_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) _
            Handles grdvwMain.DoubleClick, grdvwSec.DoubleClick

        Select Case sender.name.ToString
            Case Is = "grdvwMain"
                modGlobalVar.OpenMainOrg(iMainID, IsNull(grdvwMain.Item(2, grdvwMain.CurrentRow.Index).Value, ""))
            Case Is = "grdvwSec"
                modGlobalVar.OpenMainContact(grdvwSec.Item(0, grdvwSec.CurrentRow.Index).Value, IsNull(grdvwSec.Item("LastName", grdvwSec.CurrentRow.Index).Value, ""), grdvwSec.Item("OrgName", grdvwSec.CurrentRow.Index).Value, grdvwSec("OrgNum", grdvwSec.CurrentRow.Index).Value)

            Case Else
                modGlobalVar.Msg("ERROR: sender name not matched", sender.name.ToString, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Select

    End Sub



    ' Private Sub grdMain_SelectionChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
    'CurrentCellChanged, grdvwSec.CurrentCellChanged        ', grdvwSec.SelectionChanged 


    ''CALL REFRESH SEC GRID and SEARCH COLUMN TEXT
    'Private Sub grdvwMain_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) _
    '        Handles grdvwMain.RowEnter ',grdvwMain.CellClick,
    '    If Not isLoaded Then
    '        Exit Sub
    '    End If
    '    'do this way instead of relation else doesn't work when Contact is main grid
    '    FilterContacts(sender, e.RowIndex, e.ColumnIndex)

    'End Sub

    'REFRESH SEC GRID and SEARCH COLUMN TEXT
    Private Sub FilterContacts(ByVal grd As DataGridView, ByVal iRow As Integer, ByVal iCol As Integer)
        '     Me.tsBtnSearch.Text = "Search " & sender.Columns(sender.CurrentCell.ColumnIndex).DataPropertyName '   sender.currentcell.DataPropertyName
        If grd.Name = "grdvwMain" Then
            If Me.dsReviewOrgIntern.tblOrg.Rows.Count > 0 And Me.grdvwMain.SelectedCells.Count > 0 Then
                '          LoadSecondary()
                Try
                    iMainID = grdvwMain(0, iRow).Value
                Catch ex As Exception
                    Exit Sub
                End Try

                Me.tsBtnSearch.Text = "Find " & grdvwMain.Columns(iCol).DataPropertyName
                'FILTER SECONDARY
                '   Me.TblContactBindingSource.Filter = grdvwSec.Columns(1).DataPropertyName & " = " & iMainID
                ' modGlobalVar.Msg(Me.TblContactBindingSource.Filter.ToString)
                iGridS = TblContactBindingSource.Count
                '   Me.lblSecGrid.Text = iGridS.ToString & "  " & strDGS

                ' CountContacts()
            Else
                Me.TblContactBindingSource.Filter = "OrgNum = 0"
                Me.tsBtnSearch.Text = "Find"

            End If
        Else
            If Me.dsReviewOrgIntern.tblContact.Rows.Count > 0 And Me.grdvwSec.SelectedCells.Count > 0 Then
                Me.tsBtnSearch.Text = "Find " & grdvwSec.Columns(iCol).DataPropertyName
            Else
                Me.tsBtnSearch.Text = "Find"
            End If
        End If

    End Sub


    'Private Sub DataGridDouble(ByVal sender As Object, ByVal e As MouseEventArgs)
    '    If (DateTime.Now < mdGlobalVar.CheckDouble(sender, e).AddMilliseconds(SystemInformation.DoubleClickTime)) Then

    '        Select Case sender.name.ToString
    '            Case Is = "grdvwMain"
    '                '     miGotoOrg.PerformClick()

    '                OpenMainOrg(iCol, sender.Item(2, sender.CurrentRow.Index).Value) ', ClassOpenForms.frmMainOrg.DsMainOrg1, ClassOpenForms.frmMainOrg.daMainOrg, ClassOpenForms.frmMainOrg.miSave, "MainOrg", ClassOpenForms.frmMainOrg.daMainOrg.SelectCommand.Parameters("@OrgID"))
    '            Case Is = "grdvwSec"
    '                '   miGotoContact.PerformClick()

    '            Case Else
    '                modGlobalVar.Msg(sender.name.ToString, MessageBoxIcon.Error, "sender name not matched")
    '        End Select

    '    End If
    'End Sub

    ' 'FILL DETAIL and txtcurrent

    'calls get count for sec grid


#End Region 'grid doubleclick

#Region "GENERAL"

    Private Sub HelpToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsBtnHelp.Click
        modGlobalVar.Msg("HELP for EDITING ORGANIZATIONS and CONTACTS", "To FILL GRID: - select region from dropdown box to trigger filling grid." & NextLine & NextLine & _
        "Organizations, if selected in the first dropdown, appear in the upper grid.  Below this are smaller grids that show Cases and Contacts for the selected Organization.  " & NextLine & NextLine & _
        "With Cases are Conversations that refresh with a new Case row is selected." & NextLine & NextLine & _
        "Under Contacts are Conversations and Registrations belonging to the selected Contact row." & NextLine & NextLine & _
        "TO MOVE  Case or Contact to a new Organization: change the OrgNum each grid to the correct Org Number." & NextLine & NextLine & _
        "TO MOVE Conversation to a new Case: change the CaseNum in the Case Conversation Grid.  If you need to also move the conversation to a new Contact, SAVE, before making next change." & NextLine & _
        "TO MOVE Conversation to a new Contact: change the ContactNum in the Contact Conversation grid." & NextLine & NextLine & _
        "TO MOVE Registration to a new person: change the ContactNum in the Contact Registration grid." & NextLine & NextLine & _
        "To SEARCH GRID: " & NextLine & "1) select a column to search in the grid. " & NextLine & _
        "2) enter search string; insert wildcards (*) where required." & NextLine & _
        "3) click Search button" & NextLine & NextLine & _
        "TO DELETE Organization or Contact:" & NextLine & _
        "Change the Org name or Last name to DDelete..." & NextLine & NextLine & _
        "To GO TO DETAIL WINDOW: Double-Click, (but edits there will conflict with any edits made here)" & NextLine & NextLine & _
"To CONFIRM DELETE: change Delete to DDelete.", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub tsTxtSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsTxtSearch.Click
        Me.tsTxtSearch.SelectAll()
    End Sub

#End Region 'general



    'Private Sub grdvwSec_UserAddedRow(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewRowEventArgs) Handles grdvwSec.UserAddedRow
    '    If Not isLoaded Then Exit Sub

    '    modGlobalVar.Msg("user added row")
    'End Sub

    ''Private Sub grdvwSec_RowStateChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowStateChangedEventArgs) Handles grdvwSec.RowStateChanged
    ''    If Not isLoaded Then Exit Sub
    ''    modGlobalVar.Msg("row state changed", , e.StateChanged.Displayed.ToString)
    ''End Sub

    ''Private Sub grdvwSec_RowValidating(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellCancelEventArgs) Handles grdvwSec.RowValidating
    ''    If Not isLoaded Then Exit Sub
    ''    modGlobalVar.Msg("row validating")
    ''End Sub


    'Private Sub grdvwSec_RowsAdded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsAddedEventArgs) Handles grdvwSec.RowsAdded
    '    If Not isLoaded Then Exit Sub
    '    modGlobalVar.Msg("row added")
    'End Sub



    'Private Sub FillToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FillToolStripButton.Click
    '    Try
    '        Me.TblContactTableAdapter.Fill(Me.dsReviewOrgIntern.tblContact, New System.Nullable(Of Integer)(CType(IDToolStripTextBox.Text, Integer)))
    '    Catch ex As System.Exception
    '        System.Windows.Forms.MessageBox.Show(ex.Message)
    '    End Try

    'End Sub


    Private Sub tsSaveItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsSaveItem.Click
        UpdateDB()
    End Sub


    Private Sub tsReload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsReload.Click
        '  LoadLowerGrids()
        LoadGrids()
        Me.tsBtnSearch.Text = "Find"
    End Sub


    Private Sub FillByZipToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FillByZipToolStripButton.Click
        Try
            Me.dsReviewOrgIntern.EnforceConstraints = False
            Me.TblContactTableAdapter.FillByZip(Me.dsReviewOrgIntern.tblContact, modGlobalVar.GetWild(ZipToolStripTextBox.Text, False))
            Me.TblOrgTableAdapter.FillByZip(Me.dsReviewOrgIntern.tblOrg, modGlobalVar.GetWild(ZipToolStripTextBox.Text, False))

        Catch ex As System.Exception
            modGlobalVar.Msg("ERROR: zip tool strip", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            '   Me.dsReviewOrgIntern.EnforceConstraints = True
        End Try

    End Sub

    'Private Sub dgvOrgCases_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
    '    Me.lblAttachedFiles.Text = ListFoundFiles("Cases", Me.dgvOrgCases.Item(0, Me.dgvOrgCases.CurrentRow.Index).Value)

    'End Sub


    'Private Sub dgvOrgCases_RowEnter(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvOrgCases.RowEnter
    '    'NOTE: this event occurs before Me.dgvOrgCases.CurrentRow.Index is set.
    '    Me.lblAttachedFiles.Text = FindFilesCount("Cases", Me.dgvOrgCases.Item(0, e.RowIndex).Value)
    'End Sub

    'Private Sub btnCaseConv_Click(sender As System.Object, e As System.EventArgs) Handles btnCaseConv.Click
    '    Dim frm As New frmReviewContactPopup
    '    frm.TblConversationTableAdapter.FillbyCase(frm.DsReviewConversation1.tblConversation, Me.dgvOrgCases.Item(0, Me.dgvOrgCases.CurrentRow.Index).Value)
    '    frm.ShowDialog()
    'End Sub

    'Private Sub btnContactConv_Click(sender As System.Object, e As System.EventArgs)
    '    Dim frm As New frmReviewContactPopup
    '    frm.TblConversationTableAdapter.FillByContact(frm.DsReviewConversation1.tblConversation, Me.grdvwSec.Item(0, Me.grdvwSec.CurrentRow.Index).Value)
    '    frm.ShowDialog()
    'End Sub

    'filter grid to 2 items; good one on top
    Private Sub btnGoodBadFilter_Click(sender As System.Object, e As System.EventArgs) Handles btnGoodBadFilter.Click
        '   If Me.fldGoodID.Text > 0 Or Me.fldBadID.Text > 0 Then
        Me.tblOrgBindingSource.Filter = "ORGID = " & IsNull(Me.fldGoodID.Text, 0) & " OR ORGID = " & IsNull(Me.fldBadID.Text, 0)
        iMainID = 0
        '  Else
        '  modGlobalVar.Msg("enter a good and bad organization ID number", , "the good/bad fields are empty")
        '  End If
    End Sub

    'indicate "bad" org info has been transferred/checked
    Private Sub btnOKDeleteOrg_Click(sender As System.Object, e As System.EventArgs) Handles btnOKDeleteOrg.Click
        If Me.grdvwMain.SelectedRows.Count > 0 Then
            iMainID = Me.grdvwMain.Item(0, grdvwMain.CurrentRow.Index).Value
            If modGlobalVar.Msg("CONFIRM has been cleared to delete", "# " & iMainID.ToString & NextLine & Me.grdvwMain.Item(3, IsNull(grdvwMain.CurrentRow.Index, 0)).Value, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) = MsgBoxResult.Ok Then
                Me.grdvwMain.Item(3, IsNull(grdvwMain.CurrentRow.Index, 0)).Value = "OKdelete-" & usr & " " & Me.grdvwMain.Item(3, IsNull(grdvwMain.CurrentRow.Index, 0)).Value
                '  tsSaveItem.PerformClick()
            Else
            End If
        Else
            modGlobalVar.Msg("ATTENTION: INsufficient information", "no organization selected", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If

    End Sub



    'Private Sub grdvwMain_CurrentCellChanged(sender As Object, e As System.EventArgs) Handles grdvwMain.CurrentCellChanged
    '    If Me.grdvwMain.SelectedRows.Count > 0 Then
    '        iMainID = Me.grdvwMain.Item(0, Me.grdvwMain.CurrentRow.Index).Value
    '    End If
    'End Sub


    Private Sub ZipToolStripTextBox_TextChanged(sender As Object, e As System.EventArgs) Handles ZipToolStripTextBox.TextChanged
        Me.tscboRegion.SelectedIndex = -1
    End Sub



End Class