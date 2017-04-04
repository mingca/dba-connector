Imports System.Data.SqlClient
Imports System.Text
Imports System.Text.RegularExpressions


Public Class frmLibraryLbl

    Dim isLoaded As Boolean = False
    Dim bUpdate As Boolean = False
    Dim colSort As New Collection
    Dim strbSort As New StringBuilder
    Dim bPrint As Boolean = False
    Dim paramRegion, paramType As String
    Dim source1 As New BindingSource()
    Dim hti As System.Windows.Forms.DataGrid.HitTestInfo
    Dim htivw As DataGridView.HitTestInfo
    Dim iGrid As Integer    'count rows of [filtered] grid
    Dim statusM As String
    Dim i As Integer = 0
    Dim SrchTotal As Integer 'keep count for display 

#Region "Load"
    'LOAD
    Private Sub frmLibraryLbl_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Me.LuLocationStatusTableAdapter.FillStatusPlacement(Me.DsResourceLocationDD1.luLocationStatus)

        'LOAD DROPDOWNS
        modGlobalVar.LoadRegionCombo(Me.cboRegion, "Library")
        LoadLocation()
        LoadSortList()
        Me.cboRegion.SelectedIndex = Me.cboRegion.FindStringExact(usrRegion)

        'SET DEFAULTS
        '   Me.cboStatus.SelectedIndex = Me.cboStatus.FindStringExact("Filed")
        Me.cboWhat.SelectedIndex = Me.cboWhat.FindStringExact("Books Ready to Label")
        Me.cboLabels.SelectedIndex = Me.cboLabels.FindStringExact("Book Labels (30 up)")

        RefreshCombos()

       Me.StatusBarPanel1.Text = "Ready"

        Forms.Add(Me)
        isLoaded = True

        Me.btnSearch.PerformClick()

    End Sub

    'CLOSING
    Private Sub frm_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) _
          Handles MyBase.FormClosing
        If bUpdate = True Then
            If modGlobalVar.msg("STOP --  " & UCase(usrFirst.Substring(0, InStr(usrFirst, " "))), "Do you want to update any resources to indicate labels have been printed?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                e.Cancel = True
            End If
        End If
    End Sub

    'UPDATE LOCATION and STATUS COMBOs
    Private Sub LoadLocation()
        Me.LuLocationOverrideTableAdapter.FillLocationwUtility(Me.DsResourceLocationDD1.luLocationOverride, Me.cboRegion.SelectedValue)
        Me.LuLocationStatusTableAdapter.FillStatusPlacement(Me.DsResourceLocationDD1.luLocationStatus)

    End Sub

    'COMBOS
    Private Sub RefreshCombos()
        Me.StatusBarPanel1.Text = "Refreshing"
         Try
            'STATUS  
            Dim dvS As DataView = New DataView(Me.DsResourceLocationDD1.Tables("luLocationStatus"))
            dvS.RowFilter = "((SatelliteRegion = '" + Me.cboRegion.Text + "' OR SatelliteRegion = 'All') AND (Area <> 'placement'))"
            Me.cboStatus.DataSource = dvS
            Me.cboStatus.SelectedIndex = -1
        Catch ex As Exception
            modGlobalVar.Msg("ERROR: can't load status", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        'OVERRIDE LOCATION  
        Dim dvO As DataView = New DataView(Me.DsResourceLocationDD1.Tables("luLocationOverride"))
        dvO.RowFilter = "(SatelliteRegion = '" + Me.cboRegion.Text + "' OR SatelliteRegion = 'All')"
        Me.cboOverride.DataSource = dvO
        Me.cboOverride.SelectedIndex = -1

        'PLACEMENT 
        Dim dvF As DataView = New DataView(Me.DsResourceLocationDD1.Tables("luLocationStatus"))
        dvF.RowFilter = "(Area = 'placement') or (Area = 'utility')"
        Me.cboFiled.DataSource = dvF
        Me.cboFiled.SelectedIndex = -1


        Me.StatusBarPanel1.Text = "Ready"
    End Sub

    'SORT LIST
    Private Sub LoadSortList()
        Me.lstSortChoice.Items.Clear()
        Me.lstSortChoice.Items.Add("ResourceName")
        Me.lstSortChoice.Items.Add("Author")
        Me.lstSortChoice.Items.Add("CurrentLocation")
        Me.lstSortChoice.Items.Add("CRGMain")
        Me.lstSortChoice.Items.Add("Status")
        Me.lstSortChoice.Items.Add("Placement")
        Me.lstSortChoice.Items.Add("Publisher")

        Try
            dv.Sort = String.Empty '("CRGMain,Author,Resoucename")
        Catch ex As System.Exception
            '  modGlobalVar.Msg(ex.Message, , "ERROR:   ")
        End Try

    End Sub

#End Region 'load

#Region "Export/Print"
    'EXPORT DATA and MERGE
    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnPrint.Click
        Me.StatusBarPanel1.Text = "Exporting Data"
        MouseWait()
        Dim dataFileName As String = "DataLibLbl"
        Dim sheetLabel As String = "LibLabels"

        If Me.grdvwMain.SelectedRows.Count > 0 Then
            If modPopup.StrmWriter(Me.grdvwMain, dataFileName, fldNumSkip.Text, fldNumRepeat.Text) Then
                Try
                    If modPopup.DataToExcel(dataFileName, sheetLabel) = String.Empty Then
                        modGlobalVar.Msg("ERROR DataToExcel", "could not create datafile", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        GoTo closeall
                    Else
                        'book labels
                        Select Case Me.cboLabels.Text
                            Case Is = "Book Labels (30 up)"
                                modPopup.MergePerform(SOPPath & "MergeInfoCtrtoWord\lblLibBook.dot", dataFileName, sheetLabel)  '.dotx", strData) ', ".xls")
                            Case Is = "Manilla File Labels  (30 up)"
                                modPopup.MergePerform(SOPPath & "MergeInfoCtrtoWord\lblFileFolder.dotx", dataFileName, sheetLabel) ', ".xls")
                            Case Is = "Hanging File Labels  (10 up)"
                                modPopup.MergePerform(SOPPath & "MergeInfoCtrtoWord\lblHangingFile.dotx", dataFileName, sheetLabel) ', ".xls")
                                'TODO create merge template for various label sizes

                                '  
                                '                            Case Is = "Printout (Org address summary on regular paper)"
                                '                           Case Is = "None - Data file only"
                        End Select
                    End If
                Catch ex As Exception
                    modGlobalVar.Msg("ERROR: Can't merge", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    GoTo closeall
                End Try

                Try
                    '  Me.TabControl1.SelectedIndex = 2 ' Me.pgUpdate
                    Me.TabControl1.SelectTab(2)
                    Me.TabControl1.SelectedTab.Focus()
                    Me.cboFiled.SelectedIndex = cboFiled.FindStringExact("Filed")
                Catch ex As Exception
                    modGlobalVar.Msg("ERROR: Can't goto update tab", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End If
        Else
            modGlobalVar.Msg("nothing selected", "select row(s) and try again", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            bUpdate = False
            GoTo closeAll
        End If
        bUpdate = True
closeAll:
        MouseDefault()
        Me.StatusBarPanel1.Text = "Done"
    End Sub


    Private Sub btnShelfLabel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShelfLabel.Click
        System.Diagnostics.Process.Start(SOPPath & "DocumentTemplates\lblLibraryShelf.doc")
    End Sub

#End Region 'export

#Region "Update"
    'UPDATE
    Private Sub btnUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles btnUpdate.Click
        If Not isLoaded Then
            Exit Sub
        End If
        If usrRegion = Me.cboRegion.Text Or usr = DBAdmin.StaffID Then
        Else
            modGlobalVar.Msg("Permission Denied", "only staff at the " & Me.cboRegion.Text & " office can edit their data" & NextLine & "See " + DBAdmin.StaffName + " if you need further assistance.", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If
        Dim iCurrentID As Integer 'Id from selected row iteration

        MouseWait()

        Me.StatusBarPanel1.Text = "Updating"
        If Me.grdvwMain.SelectedRows.Count > 0 Then
        Else
            modGlobalVar.Msg("nothing selected", "select row(s) and try again", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        'modGlobalVar.Msg(DirectCast(Me.cboStatus.SelectedItem, DataRowView).Item("LocationSetupID").ToString)
        '& NextLine & "Location to: " & IsNull(DirectCast(Me.cboStatus.SelectedItem, DataRowView).Item("LocationSetupID").ToString, "(no change)")& NextLine &  "Placement to: " & IsNull(Me.cboFiled.Text, "")

CONFIRM:
        Dim strb As New StringBuilder

        If Me.cboFiled.Text = "Filed" Then
            strb.Append("update Placement to 'Filed'," & NextLine & "update Actual Location to Print Location, " & NextLine & "and clear Status")
        Else
            If Me.cboStatus.SelectedIndex = -1 Then
                strb.Append("Status: no change")
            Else
                strb.Append("Status to: " & DirectCast(Me.cboStatus.SelectedItem, DataRowView).Item("LocationName").ToString)
            End If

            If Me.cboOverride.SelectedIndex = -1 Then
                strb.Append(NextLine & "OverrideLocation: no change")
            Else
                strb.Append(NextLine & "OverrideLocation to: " & DirectCast(Me.cboOverride.SelectedItem, DataRowView).Item("LocationName"))
            End If

            If Me.cboFiled.SelectedIndex > -1 Then
                strb.Append(NextLine & "Placement to: " & Me.cboFiled.Text)
            Else
                strb.Append(NextLine & "Placement: no change")
            End If
        End If

        If Not modGlobalVar.Msg("CONFIRM...", "Change the selected resources: " & NextLine & strb.ToString, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            Exit Sub
        End If

        If Not SCConnect() Then
            Exit Sub
        End If

AddParams:
        Dim SQLUpdate As New SqlCommand("[LibLabelsUpdate]", sc)
        SQLUpdate.CommandType = CommandType.StoredProcedure
        SQLUpdate.Parameters.Add("@ID", SqlDbType.Int)
        SQLUpdate.Parameters.Add("@Status", SqlDbType.Int)
        SQLUpdate.Parameters.Add("@Location", SqlDbType.Int)
        SQLUpdate.Parameters.Add("@Override", SqlDbType.Int)
        SQLUpdate.Parameters.Add("@StaffNum", SqlDbType.Int)
        SQLUpdate.Parameters.Add("@Filed", SqlDbType.VarChar)
        'NOTE: SpreadsheetButtonVisible ParameterDirection assignemnt throughout following sections
        'else if user is using filter, rows/values will disappear from grid
        'before being assigned to the parameters.

        'EDIT GRID
        ' Dim dr As DataRow
        For Each Row As DataGridViewRow In Me.grdvwMain.SelectedRows
            iCurrentID = Me.grdvwMain.Item(0, Row.Index).Value

            SQLUpdate.Parameters("@ID").Value = iCurrentID
            SQLUpdate.Parameters("@StaffNum").Value = Me.grdvwMain.Item("LocationStaffNum", Row.Index).Value
            SQLUpdate.Parameters("@Location").Value = Me.grdvwMain.Item("NewLocationNum", Row.Index).Value
            SQLUpdate.Parameters("@Filed").Value = Me.cboFiled.Text
            SQLUpdate.Parameters("@Status").Value = Me.grdvwMain.Item("StatusNum", Row.Index).Value
            SQLUpdate.Parameters("@Override").Value = Me.grdvwMain.Item("OverrideLocationNum", Row.Index).Value

UpdatePlacement:
            If Me.cboFiled.SelectedIndex = -1 Then
            Else    'marking books filed, assign null and printlocation
                If Me.cboFiled.Text = "clearout" Then
                    SQLUpdate.Parameters("@Filed").Value = String.Empty
                    Me.grdvwMain.Item("Placement", Row.Index).Value = String.Empty
                Else
                    Me.grdvwMain.Item("Placement", Row.Index).Value = Me.cboFiled.Text

PlacementEffectsOtherCols:
                    Select Case Me.cboFiled.Text
                        Case Is = "Filed"
                            SQLUpdate.Parameters("@Status").Value = 0
                            Try
                                Me.grdvwMain.Item("ActualLocationNum", Row.Index).Value = Me.grdvwMain.Item("NewLocationNum", Row.Index).Value
                                Me.grdvwMain.Item("CurrentLocation", Row.Index).Value = Me.grdvwMain.Item("PrintLocation", Row.Index).Value
                                Me.grdvwMain.Item("StatusNum", Row.Index).Value = 0
                                Me.grdvwMain.Item("Status", Row.Index).Value = String.Empty
                            Catch ex As Exception
                                modGlobalVar.Msg("can't do Filed", ex.Message & NextLine & iCurrentID & NextLine & IsNull(Row.Index, 222).ToString, MessageBoxButtons.OK, MessageBoxIcon.Error)
                            End Try
                        Case Is = "Removed"
                            SQLUpdate.Parameters("@Location").Value = 0
                            Try
                                Me.grdvwMain.Item("ActualLocationNum", Row.Index).Value = 0
                                Me.grdvwMain.Item("CurrentLocation", Row.Index).Value = 0 'String.Empty
                            Catch ex As Exception
                                modGlobalVar.Msg("can't do Removed", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
                            End Try
                        Case Else
                    End Select
                End If
            End If

UpdateStatus:
            If Me.cboStatus.SelectedIndex = -1 Then
            Else
                If Me.cboStatus.Text = "clearout" Then
                    SQLUpdate.Parameters("@Status").Value = 0
                    Me.grdvwMain.Item("StatusNum", Row.Index).Value = 0
                    Me.grdvwMain.Item("Status", Row.Index).Value = String.Empty
                Else
                    SQLUpdate.Parameters("@Status").Value = DirectCast(Me.cboStatus.SelectedItem, DataRowView).Item("LocationSetupID")
                    Me.grdvwMain.Item("StatusNum", Row.Index).Value = DirectCast(Me.cboStatus.SelectedItem, DataRowView).Item("LocationSetupID")
                    Me.grdvwMain.Item("Status", Row.Index).Value = Me.cboStatus.Text
                End If
            End If


UpdateOverrideLocation:
            If Me.cboOverride.SelectedIndex = -1 Then
            Else
                Me.grdvwMain.Item("LocationEditDt", Row.Index).Value = Now()
                Me.grdvwMain.Item("Staff", Row.Index).Value = usrName

                If Me.cboOverride.Text = "clearout" Then
                    SQLUpdate.Parameters("@Override").Value = 0
                    Me.grdvwMain.Item("OverrideLocationNum", Row.Index).Value = 0
                Else
                    SQLUpdate.Parameters("@Override").Value = DirectCast(Me.cboOverride.SelectedItem, DataRowView).Item("LocationSetupID")
                    Me.grdvwMain.Item("OverrideLocationNum", Row.Index).Value = DirectCast(Me.cboOverride.SelectedItem, DataRowView).Item("LocationSetupID")
                End If
            End If

UpdateBackend:
            Try
                SQLUpdate.ExecuteNonQuery()
            Catch ex As Exception
                modGlobalVar.Msg("ERROR: update", ex.Message & NextLine & SQLUpdate.Parameters().ToString, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
            '   modGlobalVar.Msg("done that row")
        Next Row

CloseAll:
        ' 
        bUpdate = False
        Try
            SQLUpdate = Nothing
            sc.Close()
        Catch ex As Exception
        End Try
        MouseDefault()
        Me.StatusBarPanel1.Text = "Done"
    End Sub
#End Region 'update

#Region "Search"

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        DoSearch()
        SrchTotal = getCount()
    End Sub

    'SEARCH
    Private Sub DoSearch()
        Dim chk As CheckBox
        Dim strbType As New StringBuilder
        If Not isLoaded Then
            Exit Sub
        End If

        MouseWait()
        Me.StatusBarPanel1.Text = "Searching"
        For Each chk In Me.GrpType.Controls
            If chk.Checked Then
                strbType.Append(",'" + chk.Text + "'")
            End If
        Next
        strbType.Remove(0, 1)
        '  modGlobalVar.Msg(strbType.ToString)
        paramType = strbType.ToString

        paramRegion = Me.cboRegion.Text

        Select Case cboWhat.Text
            '  Case "Public Library Shelves"
            '      Me.DsLibrary1.EnforceConstraints = False
            '      Me.LibLabelsTableAdapter.FillByPublic(Me.DsLibrary1.Tables(0), gRegion)
            '      Me.lblHeading.Text = FullCount.ToString & "  " & Me.cboRegion.Text & ": Public Library"
            '      RefillGrid("PublicLibrary", 1, "Public Shelves")

            Case "All Active Books"
                RefillGrid("Active Books", "Active Books")
            Case "All Inactive Books"
                RefillGrid("Inactive Books", "Inactive Books")
            Case Is = "Resources to Purchase (Place Order)"
                RefillGrid("PlaceOrder", "Resources to Purchase (Place Order)")
            Case Is = "Resources On Order"
                RefillGrid("Ordered", "Resources On Order")
            Case Is = "Resources on Resource Guides"
                RefillGrid("RG", "On Resource Guide")
            Case Is = "Resources Recommended"
                RefillGrid("Recommend", "Recommended")
            Case Is = "Resources Declined Purchase"
                RefillGrid("Declined", "Declined Purchase")
            Case Is = "Resources Removed Or Discarded"
                RefillGrid("Removed", "Removed or Discard")
            Case Is = "Resources for Workshop Giveaway"
                RefillGrid("WG", "Workshop Giveaway")

            Case Else
                RefillGrid(Me.cboWhat.Text, Me.cboWhat.Text)

        End Select
 
        RefreshCombos()

        Me.StatusBarPanel1.Text = "Done"
        MouseDefault()
    End Sub

    'SET DEFAULT TYPE RADIO BUTTONS
    Private Sub cboWhat_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
         Handles cboWhat.SelectedIndexChanged
        Dim chk As CheckBox

        Select Case cboWhat.Text
            Case Is = "Files Ready to Label"
                For Each chk In Me.GrpType.Controls
                    If chk.Tag = "2" Then
                        chk.Checked = True
                    Else
                        chk.Checked = False
                    End If
                Next
            Case Else   'physical media
                For Each chk In Me.GrpType.Controls
                    If chk.Tag = "1" Then
                        chk.Checked = True
                    Else
                        chk.Checked = False
                    End If
                Next
        End Select
        Me.btnSearch.PerformClick()
    End Sub

    'LOAD cboWHAT, Call Search
    Private Sub cboRegion_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
         Handles cboRegion.SelectedIndexChanged

        Me.cboWhat.Items.Clear()

        Dim strItems As String() = {"Books Ready to Label", "Files Ready to Label"}
        Me.cboWhat.Items.AddRange(strItems)

        Me.cboWhat.Items.Add("--------------")
        'RESET LOCATIONS
        Dim sql As New SqlCommand("SELECT DISTINCT luLocation.LocationSetupID, luLocation.LocationName, luLocation.SatelliteRegion, luLocation.Area, luLocation.SortFld, luLocation.DropDown FROM  luLocation INNER JOIN vwGetValidResourceLocations ON luLocation.LocationSetupID = COALESCE (vwGetValidResourceLocations.ActualLocationNum,  vwGetValidResourceLocations.LocationStatusNum) WHERE        (luLocation.DropDown = 'Location') AND (luLocation.SatelliteRegion =  '" & Me.cboRegion.Text & "') ORDER BY luLocation.SortFld", sc)
        ' & " SatelliteRegion = 'All')", sc)

        Dim rdr As SqlDataReader
        If Not SCConnect() Then
            Exit Sub
        End If
        Try
            rdr = sql.ExecuteReader(System.Data.CommandBehavior.CloseConnection)
            If rdr.HasRows Then
                While rdr.Read
                    Me.cboWhat.Items.Add(rdr.GetString(1))
                End While
            Else
                modGlobalVar.Msg("ERROR: no locations found", Me.cboRegion.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Catch ex As Exception
            modGlobalVar.Msg("ERROR: datareader", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            rdr.Close()
        End Try
        Me.cboWhat.Items.Add("--------------")
        '"--------------"
        '"++++++++++++++"

        strItems = New String() {"Resources On Order", "Resources to Purchase (Place Order)", "Resources Declined", "Resources Removed Or Discarded", "++++++++++++++", "All Active Books", "All Inactive Books", "--------------", "Resources Recommended", "Resources on Resource Guides", "Resources for Workshop Giveaway"}
        Me.cboWhat.Items.AddRange(strItems)

        btnSearch.PerformClick()

    End Sub

    'LOAD MAINGRID
    Public Sub RefillGrid(ByVal What As String, ByVal strHeading As String)
        'modGlobalVar.Msg("filling grid")
        MouseWait()
        Me.DsLibrary1.EnforceConstraints = False
        ' modGlobalVar.Msg(paramRegion & NextLine & What & NextLine & paramType, , strHeading)
        '  modGlobalVar.Msg(What.ToString, , paramRegion.ToString)
        Try
            Me.LibLabelsTableAdapter.Fill(Me.DsLibrary1.Tables(0), paramRegion, What, paramType)
            '  modGlobalVar.Msg(Me.DsLibrary1.tmpLib.Rows.Count.ToString, , "Rows returned")
        Catch ex As Exception
            modGlobalVar.Msg("ERROR: filling dataset", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Me.lblHeading.Text = FullCount.ToString & "  " & Me.cboRegion.Text & ": " & strHeading

        'Select Case Me.grdvwMain.Columns("CRGName").
        '    Case Is > ""
        '        DataGridViewCellStyle1.BackColor = System.Drawing.Color.Gray
        '    Case Else
        '        DataGridViewCellStyle1.BackColor = System.Drawing.Color.Red
        'End Select

        MouseDefault()

    End Sub

    'number of rows in grid
    Private Function FullCount()
        FullCount = Me.DsLibrary1.LibLabelsNew.DefaultView.Count    'Me.grdvwMain.RowCount 'CType(grdvwMain.DataSource, DataView).Count   ' Me.grdvwMain.RowCount 'Me.DsLibrary1.Tables(0).Rows.Count
    End Function

    'number of highlighted rows
    Private Sub GetCounts()
        Me.lblHeading.Text = Me.grdvwMain.Rows.GetRowCount(DataGridViewElementStates.Selected).ToString & " selected of " & SrchTotal.ToString
    End Sub

    'highlight missing data
    Private Sub grdvwMain_CellFormatting(ByVal sender As Object, ByVal e As DataGridViewCellFormattingEventArgs) Handles grdvwMain.CellFormatting

NotActive:  'Inactive: color Title and skip other formatting
        '  If Me.grdvwMain.Columns(e.ColumnIndex).Name = "Active" Then
        If IsNull(Me.grdvwMain.Item("Active", e.RowIndex).Value, "T") = "F" Then
            Me.grdvwMain("ResourceName", e.RowIndex).Style.ForeColor = Color.Coral
            Me.grdvwMain("Author", e.RowIndex).Style.ForeColor = Color.Coral
        Else
NoCRG:      'CRG is blank: shade CRG cell
            If Me.grdvwMain.Columns(e.ColumnIndex).Name = "CRGMain" Then ')e.ColumnIndex = 5 Then   'do on first cell in case rest are out of sight and don't call format
                If IsDBNull(Me.grdvwMain.Item("CRGMain", e.RowIndex).Value) Then
                    Me.grdvwMain.Rows(e.RowIndex).HeaderCell.Style.BackColor = Color.LemonChiffon
                    Me.grdvwMain("CRGMain", e.RowIndex).Style.BackColor = Color.LemonChiffon
                End If
            End If
NoLocation:  'no existing location at that region: gray title & author
            If Me.grdvwMain.Columns(e.ColumnIndex).Name = "CurrentLocation" Then
                If IsDBNull(Me.grdvwMain.Item("ResourceLocationID", e.RowIndex).Value) Then
                    Me.grdvwMain("ResourceName", e.RowIndex).Style.ForeColor = Color.Gray
                    Me.grdvwMain("Author", e.RowIndex).Style.ForeColor = Color.Gray
                End If
            End If
HighlightRequired:  'Should be in public library: shade row
            Dim bHighlight As Boolean = False
            'If Me.grdvwMain.Columns(e.ColumnIndex).Name = "Why" Then ' THEN  And Regex.IsMatch(e.Value.ToString, "WG") Or Regex.IsMatch(e.Value.ToString, "RG") Then
            '    If Regex.IsMatch(e.Value.ToString, "WG") Or Regex.IsMatch(e.Value.ToString, "RG") Then
            '        bHighlight = True
            '        GoTo DoHighlight
            '    End If
            'End If
            If Me.grdvwMain.Columns(e.ColumnIndex).Name = "OverrideLocation" Then
                If IsNull(Me.grdvwMain.Item("OverrideLocation", e.RowIndex).Value, "") = "Public" Then
                    bHighlight = True
                End If
            End If
DoHighlight:
            If bHighlight = True Then
                Me.grdvwMain.Rows(e.RowIndex).DefaultCellStyle.BackColor = Color.FromArgb(230, 255, 230) 'colours whole row
            End If
        End If
    End Sub

#End Region 'search

#Region "General"
    'UNDO
    Protected Overrides Function ProcessDialogKey(ByVal keyData As Keys) As Boolean
        If keyData = System.Windows.Forms.Keys.Escape Then
            modPopup.UndoCtl(Me.ActiveControl)
            Return True  ' True means we've processed the escape key
        Else
            Return MyBase.ProcessDialogKey(keyData)
        End If
    End Function

    'SORT BUTTON
    Private Sub btnSort_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSort.Click

        Me.StatusBarPanel1.Text = "Sorting"
        Dim dv As DataView
        dv = Me.DsLibrary1.Tables(0).DefaultView '"LibLabelsNew" 'LibLabels").DefaultView

        Dim s As String = strbSort.ToString.Substring(0, Len(strbSort.ToString) - 2)
        dv.Sort = s & " ASC"
        Me.grdvwMain.DataSource = dv
        'view.Sort = "C2 ASC, C3 ASC";
        ' BindingSource.DataSource = dv
        strbSort.Replace(strbSort.ToString, "")
        lstSortChoice.ClearSelected()
        i = 0
        LoadSortList()
        ' colSort.Clear()
        Me.StatusBarPanel1.Text = "Done"
    End Sub

    'SORT LIST
    Private Sub lstSort_Click(ByVal sender As Object, ByVal e As System.EventArgs) _
            Handles lstSortChoice.Click
        ' colSort.Add(lstSortOrder.SelectedItem)
        ' Me.lstSortOrder.Items.Add(lstSortChoice.SelectedItem)
        strbSort.Append(lstSortChoice.SelectedItem & ", ")
        i += 1
        Dim x As Integer
        x = Me.lstSortChoice.SelectedIndex
        Me.lstSortChoice.Items.Insert(x, Me.lstSortChoice.SelectedItem & " " & i.ToString)
        Me.lstSortChoice.Items.RemoveAt(x + 1)

    End Sub

    'CLEAR
    Private Sub btnClear_click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnClear.Click

        Me.cboStatus.SelectedIndex = -1
        '  Me.cboOverride.SelectedIndex = -1
        Me.cboFiled.SelectedIndex = -1

    End Sub

    'cboFILED
    Private Sub cboFiled_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboFiled.SelectedIndexChanged
        If Me.cboFiled.Text = "Filed" Then
            ' Me.cboOverride.SelectedIndex = -1
            Me.cboStatus.SelectedIndex = -1
        End If
    End Sub

    'GRID DBL CLICK
    Private Sub grdvwMain_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdvwMain.DoubleClick
        MouseWait()
        Me.StatusBarPanel1.Text = "Opening main resource window"
        modGlobalVar.OpenResourceChoice(Me.grdvwMain.Item("ResourceNum", Me.grdvwMain.CurrentRow.Index).Value, Me.grdvwMain.Item("ResourceName", Me.grdvwMain.CurrentRow.Index).Value) ', ClassOpenForms.frmMainOrg.DsMainOrg1, ClassOpenForms.frmMainOrg.daMainOrg, ClassOpenForms.frmMainOrg.miSave, "MainOrg", ClassOpenForms.frmMainOrg.daMainOrg.SelectCommand.Parameters("@OrgID"))
        Me.StatusBarPanel1.Text = "done"
        MouseDefault()
    End Sub

    'count grid rows    --? why gets called more often than is necessary
    Private Sub grdvwMain_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdvwMain.SelectionChanged
        If isLoaded Then
            GetCounts()
        End If
    End Sub

    'CLEAR SORT
    Private Sub btnClearSort_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClearSort.Click
        LoadSortList()
        Dim dv As DataView
        dv = Me.DsLibrary1.Tables(0).DefaultView '"LibLabelsNew" 'LibLabels").DefaultView
        dv.Sort = "CRGMain, Author, ResourceName"
        Me.grdvwMain.DataSource = dv
    End Sub

    'HELP BTN
    Private Sub btnHelp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHelp.Click
        modGlobalVar.Msg("LIBRARY HELP", "GRID COLOR LEGEND:" & NextLine _
        & "CORAL Title and Author: Inactive resource." & NextLine _
        & "YELLOW CRG cell: missing Keyword1.  The resource cannot be filed without one. " & NextLine _
        & "GRAYED OUT Title and Author: no location has been entered for this resource at this region." & NextLine _
        & "GREEN row: the resource is on a Resource Guide or intentionally in library.", MessageBoxButtons.OK, MessageBoxIcon.Error)

    End Sub

    'SET STATUS BAR LEFT TEXT
    Private Sub SetStatusBarText(ByVal str As String)
        Me.StatusBar1.Panels(0).Text = str
    End Sub


#End Region 'general

#Region "FilterGrid"

    'CAPTURE RIGHT MOUSE CLICK TO FILTER APPROPRIATE GRID
    Protected Sub grdAll_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) _
            Handles grdvwMain.MouseDown    'grdSecond1.MouseDown, grdSecond3.MouseDown, grdvwMain.MouseDown, grdSecond2.MouseDown
        Dim str() As String = modGlobalVar.FilterDataGridView(sender, e, Me.DsLibrary1.Tables(0), False)

        If str(1) = String.Empty Or str(1) = "LEFT" Then
            Me.lblHeading.Text = GetCount.ToString
        Else
            Me.lblHeading.Text = str(0) & "/" & SrchTotal & str(1) 'dont count duplicate resources in filtered number
        End If

    End Sub

    'count distinct resources in main grid re duplicates re multiple authors
    Private Function GetCount() As Integer
        Dim tDistinct As New DataTable("tDistinct")
        tDistinct = Me.DsLibrary1.Tables(0).DefaultView.ToTable(True, "ICCResourceID")
        GetCount = tDistinct.Rows.Count
        tDistinct = Nothing
    End Function

#End Region 'filter

End Class

