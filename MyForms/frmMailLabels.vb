
Imports System.Data.SqlClient
Imports System.IO
Imports System.Text
'Imports InfoCtr.ClassStaff

Public Class frmMailLabels
    Inherits System.Windows.Forms.Form

    Dim myNode As TreeNode
    Dim node, Node2 As TreeNode
    Dim NodeRegion, NodeArea, NodeCounty, NodeCity, NodeState As TreeNode
    Dim sw As IO.StreamWriter

    Dim isLoaded As Boolean = False 'check default boxes after adding, before checked activates
    Dim param As SqlParameter   'error checking
    Dim a As Boolean = False 'area has been selected
    Dim tnAll As Integer
    Dim bAllChildren As Boolean = True
    Dim bAllChecked As Boolean = True
    Dim BoldFont As Font
    Dim daMailList As New SqlDataAdapter
    Dim sqlGetMailListDetail As New SqlCommand("[MainMailList]")
    Dim sqlUpdateMailListDetail As New SqlCommand
    Dim dsMailList As New DataSet
    Dim tMailDetail As New DataTable("tMailDetail")
    Dim tMailMembers As New DataTable("tMailMembers")
    Dim FirstTime As Integer = 0 'don't run selected index changed when first move to tab



#Region "Load"

    'LOAD
    Private Sub MailLabels_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        LoadType()

        'SetupSavedMailList()
        'BindMailFields()

        'OPEN with TREEVIEW EXPANDED
        Me.trvArea.ExpandAll()
        Me.trvArea.Nodes("OutsideIndiana").Collapse()
        Me.trvType.ExpandAll()
        Me.trvType.Nodes(0).Checked = True
        Me.trvType.Nodes(0).FirstNode.Checked = True
        For Each node In Me.trvType.Nodes(0).FirstNode.Nodes
            node.Checked = True
        Next node

        Try
            Me.trvType.Nodes("OrgType").Nodes("Individuals").Checked = True
        Catch ex As Exception
            '   modGlobalVar.Msg(ex.Message, , "error type")
        Finally

        End Try

        Me.trvPeople.ExpandAll()
        Me.trvPeople.Nodes(0).Checked = True
        trvPeople.Nodes(0).FirstNode.Checked = True

        LoadCounties()
        ' BoldFont = New Font(Me.trvArea.Font, FontStyle.Bold)
        '  Me.trvArea.Nodes("AllIndiana").Checked = True

        For Each tbs As DataGridTableStyle In Me.dgrdMember.TableStyles
            For Each tbx As DataGridTextBoxColumn In tbs.GridColumnStyles
                tbx.TextBox.Enabled = False
                tbx.NullText = ""
            Next
        Next

        LoadSavedMailLIstStuff()

        Forms.Add(Me)
        isLoaded = True

    End Sub

    'FILL IN COUNTIES
    Private Sub LoadCounties()
        Dim dr As SqlDataReader
        Dim strbRegion As New StringBuilder
        Dim strbContig As New StringBuilder
        MouseWait()
        Dim cmd As New SqlCommand("Select DISTINCT  TOP (100) PERCENT tz.County, UPPER(tOrg.County) + '  [' + CAST(ISNULL(tOrg.NumOrgs, 0) AS varchar) + ']' AS Orgs, tz.SatelliteRegion, tz.Contiguous, " _
                & " tz.MailingRegion FROM         (SELECT     County, Contiguous, MailingRegion, SatelliteRegion" _
                & " FROM dbo.luCountyZip   WHERE      ((Contiguous NOT LIKE 'partial') OR (Contiguous IS NULL))) AS tz LEFT OUTER JOIN (SELECT     TOP (100) PERCENT dbo.tblOrg.County, COUNT(DISTINCT dbo.tblOrg.OrgName) AS NumOrgs" _
                & "        FROM          dbo.vwGetValidOrgs INNER JOIN  dbo.tblOrg ON dbo.vwGetValidOrgs.OrgID = dbo.tblOrg.OrgID AND dbo.tblOrg.Active = 1 AND dbo.tblOrg.MailPreference <> 'No' AND dbo.tblOrg.Street1 > ' '  GROUP BY dbo.tblOrg.County) AS tOrg ON tz.County = tOrg.County ORDER BY tz.County")

        '"SELECT DISTINCT upper(left(County,1)) as County, SatelliteRegion, Contiguous, MailingRegion FROM luCountyZip ORDER BY SatelliteRegion, County")
        cmd.Connection = sc
        If Not SCConnect() Then
            MouseDefault()
            Exit Sub
        End If
        strbRegion.Append("begin")
        strbContig.Append("begin")

        Try
            dr = cmd.ExecuteReader()

            While dr.Read
                If IsDBNull(dr(0)) Then
                    Continue While
                Else
                    If IsDBNull(dr(3)) Then 'contiguous
                        strbContig.Replace(strbContig.ToString, "X")
                    Else
                        If dr.GetString(3) = "Partial" Then
                            Continue While
                        Else
                            strbContig.Replace(strbContig.ToString, dr.GetString(3))
                        End If
                    End If
                    If IsDBNull(dr.GetString(2)) Then   'satelliteregion
                        modGlobalVar.msg("ATTENTION: Missing Data", "Inform " & DBAdmin.StaffName & " " & dr.GetString(2) & " is missing the Satellite Region", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        strbRegion.Replace(strbRegion.ToString, "Y")
                    Else
                        strbRegion.Replace(strbRegion.ToString, Replace(dr.GetString(2), " ", ""))
                        'ADD COUNTIES TO SATELLITE REGION NODE
                        '  CType(Me.trvArea.Nodes("SatelliteRegions").Nodes("Satellite" & strbRegion.ToString), TreeNode).Nodes.Add(dr.GetString(0), dr.GetString(1))

                        'HIGHLIGHT main county of original satellite regions; italize outlying counties
                        Select Case strbContig.ToString
                            Case "Main" 'upper case and bold
                                CType(Me.trvArea.Nodes("SatelliteRegions").Nodes("Satellite" & strbRegion.ToString), TreeNode).Nodes.Add(dr.GetString(0), dr.GetString(1))
                                'CType(Me.trvArea.Nodes("SatelliteRegions").Nodes("Satellite" & strbRegion.ToString), TreeNode).Nodes(dr.GetString(0)).NodeFont = New System.Drawing.Font(Me.trvArea.Font.Style, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte)) ' BoldFont 

                                CType(Me.trvArea.Nodes("SatelliteRegions").Nodes("Satellite" & strbRegion.ToString), TreeNode).Nodes(dr.GetString(0)).NodeFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte)) ' BoldFont 
                            Case "Contiguous" ', "Partial"    'upper case
                                CType(Me.trvArea.Nodes("SatelliteRegions").Nodes("Satellite" & strbRegion.ToString), TreeNode).Nodes.Add(dr.GetString(0), dr.GetString(1))
                            Case Else 'Initial caps
                                CType(Me.trvArea.Nodes("SatelliteRegions").Nodes("Satellite" & strbRegion.ToString), TreeNode).Nodes.Add(dr.GetString(0), StrConv(dr.GetString(1), VbStrConv.ProperCase))
                                'CType(Me.trvArea.Nodes("SatelliteRegions").Nodes("Satellite" & strbRegion.ToString), TreeNode).Nodes.Add(dr.GetString(0), Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(dr.GetString(1)))

                                '  italic
                                'CType(Me.trvArea.Nodes("SatelliteRegions").Nodes("Satellite" & strbRegion.ToString), TreeNode).Nodes(dr.GetString(0)).NodeFont = New System.Drawing.Font("Comic Sans MS", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
                                'initial caps
                                ' CType(Me.trvArea.Nodes("SatelliteRegions").Nodes("Satellite" & strbRegion.ToString), TreeNode).Nodes(dr.GetString(0)).NodeFont = Format(Nodes("SatelliteRegions").Nodes("Satellite" & strbRegion.ToString), TreeNode).Nodes(dr.GetString(0)).NodeFont
                        End Select
                    End If
                    'ADD COUNTY TO AllIndiana node
                    ' CType(Me.trvArea.Nodes("AllIndiana"), TreeNode).Nodes.Add(dr.GetString(0), dr.GetString(1))

                    'ADD COUNTIES TO MAILING REGION NODE
                    If IsDBNull(dr.GetString(4)) Then
                        modGlobalVar.msg("ATTENTION: Missing Data", "Inform " & DBAdmin.StaffName & " " & dr.GetString(2) & " is missing the Mailing Region", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        '     strbMailing.Replace(strbMailing.ToString, "X")
                    Else

                        'future development of Central mailing area; for now use AllIndiana
                        If dr.GetString(4) = "Central" Then
                            For Each NodeArea In Me.trvArea.Nodes("MailingAreas").Nodes
                                CType(NodeArea, TreeNode).Nodes.Add(dr.GetString(0), dr.GetString(1))
                            Next NodeArea
                        Else
                            CType(Me.trvArea.Nodes("MailingAreas").Nodes(dr.GetString(4)), TreeNode).Nodes.Add(dr.GetString(0), dr.GetString(1))
                            '                            CType(Me.trvArea.Nodes("AllIndiana"), TreeNode).Nodes.Add(dr.GetString(0), dr.GetString(1))

                        End If
                    End If

                End If
            End While
        Catch ex As Exception
            modGlobalVar.msg("ERROR: load counties", strbRegion.ToString & " " & strbContig.ToString & NextLine & ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            dr.Close()
            dr = Nothing
            cmd = Nothing
            sc.Close()
        End Try


        If Not SCConnect() Then
            Exit Sub
        End If

LoadCities:
        'only 1 datareader at a time
        'add cities to SatelliteRegion County Node

        Dim cmd2 As New SqlCommand("Select", sc)
        Dim dr2 As SqlDataReader
        For Each NodeRegion In Me.trvArea.Nodes("SatelliteRegions").Nodes

            For Each NodeCounty In NodeRegion.Nodes
                cmd2.CommandText = "SELECT DISTINCT TOP (100) PERCENT tblorg.City, tblorg.City + '  [' + ISNULL(CAST(COUNT(DISTINCT OrgName) AS varchar), 0) + ']' AS NumOrgs           FROM dbo.tblOrg inner join vwgetvalidorgs ON dbo.tblOrg.OrgID = dbo.vwGetValidOrgs.OrgID WHERE  tblorg.city > '' AND (tblorg.County = '" & NodeCounty.Name & "' AND tblorg.Active <>0) GROUP BY tblorg.City ORDER BY tblorg.City " '"SELECT distinct TOP (100) percent luCountyZip.City, luCountyZip.City + '  [' + CAST(ISNULL(tOrg.NumOrgs, 0) AS varchar) + ']' AS Orgs FROM         luCountyZip LEFT OUTER JOIN (SELECT  TOP (100) percent   City, COUNT(distinct tblorg.orgname) AS NumOrgs   FROM tblOrg  inner join vwgetvalidorgs on tblorg.orgid = vwgetvalidorgs.orgid WHERE(tblorg.Active = 1 and street1 > ' ') GROUP BY City) AS tOrg ON luCountyZip.City = tOrg.City WHERE     (luCountyZip.City > '') AND (luCountyZip.County = '" & NodeCounty.Name & "') ORDER BY lucountyzip.City "  'County = '" & nodeCounty.Text & "' ORDER BY"

                dr2 = cmd2.ExecuteReader()
                While dr2.Read
                    Try
                        CType(NodeCounty, TreeNode).Nodes.Add(dr2.GetString(0), dr2.GetString(1))
                        '                            CType(Me.trvArea.Nodes(2).Nodes(), TreeNode).Nodes.Add(dr2.GetString(0), dr2.GetString(0))
                    Catch ex As Exception
                        modGlobalVar.msg("ERROR: city query", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try
                End While
                dr2.Close()
            Next
        Next
        ' cmd2 = Nothing
        ' dr2 = Nothing
        sc.Close()
        '  tnAll = Me.trvArea.Nodes("AllIndiana").Index

        If Not SCConnect() Then
            Exit Sub
        End If

        'STATES and COUNTRIES
        cmd2.CommandText = "SELECT TOP (100) percent State, State + ' [' + cast( isnull(COUNT(ContactID) ,'') as varchar) + ']' as NumContacts FROM dbo.vwGetContactAddrActive WHERE     (NOT (State LIKE 'IN%')) AND (City > '' AND Country = 'USA') GROUP BY State ORDER BY State; " _
        & " SELECT TOP (100) PERCENT dbo.tblOrg.Country, dbo.tblOrg.Country + ' [' + CAST(COUNT(dbo.tblOrg.OrgID) AS varchar) + ']' AS NumOrgs FROM         dbo.tblOrg INNER JOIN dbo.vwGetValidOrgs ON dbo.tblOrg.OrgID = dbo.vwGetValidOrgs.OrgID WHERE     (dbo.tblOrg.Country <> 'USA') AND (dbo.tblOrg.Country > '') GROUP BY dbo.tblOrg.Country"
        dr2 = cmd2.ExecuteReader()
        '  CType(Me.trvArea.Nodes("SatelliteRegions").Nodes("Satellite" & strbRegion.ToString), TreeNode).Nodes.Add(dr.GetString(0), dr.GetString(1))

        While dr2.Read
            CType(Me.trvArea.Nodes("OutsideIndiana").Nodes("OtherStates"), TreeNode).Nodes.Add(dr2.GetString(0), dr2.GetString(1))
        End While

        dr2.NextResult()
        If dr2.Read Then
            CType(Me.trvArea.Nodes("OutsideIndiana").Nodes("International"), TreeNode).ForeColor = Color.Black
            CType(Me.trvArea.Nodes("OutsideIndiana").Nodes("International"), TreeNode).Nodes.Add(dr2.GetString(0), dr2.GetString(1))
            While dr2.Read
                CType(Me.trvArea.Nodes("OutsideIndiana").Nodes("International"), TreeNode).Nodes.Add(dr2.GetString(0), dr2.GetString(1))
            End While
        Else
            CType(Me.trvArea.Nodes("OutsideIndiana").Nodes("International"), TreeNode).ForeColor = Color.DarkGray
            ' CType(Me.trvArea.Nodes("OutsideIndiana").Nodes("International")
        End If

        dr2.Close()
        sc.Close()
        cmd2 = Nothing
        dr2 = Nothing
    End Sub

    'FILL TYPE TREE
    Private Sub LoadType()
        Dim dr As SqlDataReader
        Dim cmdType As New SqlCommand("SELECT DISTINCT OrgType FROM tblOrg WHERE NOT (OrgType like 'duplicat%') AND NOT (OrgType IN ('church', 'mosque', 'denomination', 'synagogue', 'temple', 'notfound',  'pending', 'rectory/parsonage/manse','convent','national learning')) ORDER BY OrgType")
        cmdType.Connection = sc
        ' Me.StatusBar1.Panels(0).Text = "Loading..."
        If Not SCConnect() Then
            Exit Sub
        End If
        Try
            dr = cmdType.ExecuteReader()

            While dr.Read()
                CType(Me.trvType.Nodes("OrgType"), TreeNode).Nodes.Add(dr.GetString(0), dr.GetString(0))
            End While
        Catch ex As Exception
            modGlobalVar.msg("ERROR: cmdtype execute reader", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        dr.Close()
        cmdType.Dispose()
        sc.Close()
    End Sub

    'FILL PRINT FLAG COMBO
    Private Sub LoadPrintFlag()
        Dim dr As SqlDataReader
        Dim cmdFlag As New SqlCommand("[GetPrintFlag]", sc)
        cmdFlag.CommandType = CommandType.StoredProcedure
        ' Me.cboPrintFlag.Items.Add("Annual Report")
        If Not SCConnect() Then
            Exit Sub
        End If
        dr = cmdFlag.ExecuteReader(CommandBehavior.CloseConnection)
        Dim dt As New DataTable("PrintFlag")
        dt.Load(dr)
        Me.cboPrintFlag.DataSource = dt
        dr.Close()
        Me.cboPrintFlag.SelectedIndex = -1

    End Sub

    Private Sub LoadSavedMailListStuff()
        dsMailList.Tables.Add(tMailDetail)
        'daMailList.SelectCommand = sqlGetMailListDetail
        ' daMailList.UpdateCommand = sqlUpdateMailListDetail
        sqlGetMailListDetail.CommandType = CommandType.StoredProcedure
        sqlUpdateMailListDetail.CommandType = CommandType.Text
        sqlGetMailListDetail.Connection = sc
        sqlUpdateMailListDetail.Connection = sc
        sqlGetMailListDetail.Parameters.Add("@ListID", SqlDbType.Int)

    End Sub

#End Region 'load

#Region "Search"
    'ASSIGN SPROC VARIABLES
    Private Sub btnGo_Click(ByVal sender As Object, ByVal e As System.EventArgs) _
            Handles btnGo.Click
        'NODE FORECOLOR IS DARK GRAY when only some child nodes are selected
        'NODE FORECOLOR is BLACK when all or none child nodes are selected

        MouseWait()
        Dim strSQL As New SqlCommand
        Dim strbParam As New StringBuilder
        Dim strbArea As New StringBuilder
        Dim strbState As New StringBuilder
        Dim strbCounty As New StringBuilder
        Dim strbCity As New StringBuilder
        Dim strbTmp As New StringBuilder
        Dim strbCntry As New StringBuilder
        Dim strbRegion As New StringBuilder
        Dim bNoGeography As Boolean
        Dim iUnchecked As Integer
        Dim strEmailFormHeading As String

        ' If rbAddress.Checked = True Then
        'If Me.cboPrintFlag.SelectedIndex > -1 Then
        '    strSQL.CommandText = "dbo.MailUserFlagList"
        'Else
        '    strSQL.CommandText = "dbo.MailUserGeography"
        'End If
        'ElseIf rbEmail.Checked = True Then
        '    strSQL.CommandText = "dbo.MailLabelsEmail"
        'Else
        '    modGlobalVar.msg("problem choosing sproc")
        'End If
        strSQL.CommandType = CommandType.StoredProcedure
        strSQL.Connection = sc
        strSQL.Parameters.Add("@FileType", SqlDbType.Text)

        If Me.rbAddress.Checked = True Then
            strSQL.Parameters("@FileType").Value = "label"
        Else 'rbEmail.checked = true
            strSQL.Parameters("@FileType").Value = "email"
        End If

SPECIALMAILFLAG:
        'ignores all other criteria

        If TabControl1.SelectedTab.Tag = "SavedMailList" Then
            If Me.cboPrintFlag.SelectedIndex > -1 Then
                strEmailFormHeading = "SEND EMAIL LIST: " & Me.cboPrintFlag.Text

                strSQL.CommandText = "dbo.MailUserSavedMailList"
                strSQL.Parameters.Add("@MailID", SqlDbType.Int)
                strSQL.Parameters("@MailID").Value = Me.cboPrintFlag.SelectedValue
                GoTo BranchBetweenMailEmail 'STREAMWRITER
            Else
                msg("ATTENTION: no row selected", "Select a Mail LIst in the dropdown box", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If
        Else
            strEmailFormHeading = "SEND EMAIL: selected by Geography"
            strSQL.CommandText = "dbo.MailUserGeography"

        End If

        Dim i As Integer = 0
        Dim j As Integer = 0
        Dim k As Integer = 0
        Dim h As Integer = 0

        '.................................................

PEOPLE:  'node tag = param name
        For x As Integer = 0 To Me.trvPeople.Nodes.Count - 1
            For Each node In Me.trvPeople.Nodes(x).Nodes
                If node.Checked = True Then
                    If node.Tag > "" Then
                        strSQL.Parameters.Add(node.Tag, SqlDbType.Bit)
                        strSQL.Parameters(node.Tag).value = True '"@" & node.Name).Value = 1
                        i = i + 1
                    Else
                        '   strSQL.Parameters(node.Tag).value = 0
                    End If
                End If
            Next
        Next x
        If i > 0 Then
        Else
            modGlobalVar.msg("ATTENTION: incomplete information", "no Type of Contact has been selected" & NextLine & "'Primary Contact' will be used", MessageBoxButtons.OK, MessageBoxIcon.Information)
            If strSQL.Parameters.Contains("@Primary") Then
            Else
                strSQL.Parameters.Add("@Primary", SqlDbType.Bit)
            End If
            strSQL.Parameters("@Primary").Value = True
        End If
        'modGlobalVar.Msg(strSQL.Parameters("@Primary").Value.ToString, , "primary")
        '.................................................
ORGTYPE:  'node tag = criteria
        i = 0
        If Me.trvType.Nodes("OrgType").Checked = True And Me.trvType.Nodes("OrgType").ForeColor = Color.Black Then
            i = 333
        Else
            For Each node In CType(Me.trvType.Nodes("OrgType"), TreeNode).Nodes
                If node.Name = "Religious" Then 'default in stored proc var
                    For Each Node2 In CType(Me.trvType.Nodes("OrgType").Nodes("Religious"), TreeNode).Nodes
                        If Node2.Checked = True Then
                            If Node2.Tag > "" Then
                                strbParam.Append("'" & Node2.Tag & "',")
                                i = i + 1
                            End If
                        End If
                    Next
                    'End If
                Else
                    If node.Checked = True Then
                        strbParam.Append("'" & node.Text & "',")
                        i = i + 1
                    End If
                End If
            Next
        End If

        If i > 0 Then
            strSQL.Parameters.Add("@Type", SqlDbType.Text)
            If i = 333 Then
                strSQL.Parameters("@Type").Value = "All"
            Else
                strSQL.Parameters("@Type").Value = strbParam.ToString.Substring(0, Len(strbParam.ToString) - 1) '"'Denomination', 'Synagogue'" '"'church', 'temple','mosque', 'synagogue', 'denomination'",
                strbParam.Replace(strbParam.ToString, "")
            End If
            ' If Me.trvType.Nodes("NationalLearning").Checked = True Then 'must a separate mailing
            ' Me.trvType.Nodes("NationalLearning").Checked = False
            'End If
        Else
            ' If Me.trvType.Nodes("NationalLearning").Checked = True Then
            '    strSQL.Parameters.Add("@Type", SqlDbType.Text)
            '    strSQL.Parameters("@Type").Value = "'NationalLearning'"
            'Else
            modGlobalVar.msg("ATTENTION: incomplete information", "Please select one or more Type of Organizations", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            GoTo closeall
            'End If
        End If

        '.......................................
GEOGRAPHY:
        'start with int'l and out of state, then indiana
        ' strbState.Append("nothing")
States:
        'OtherStates
        '  modGlobalVar.Msg(Me.trvArea.Nodes("OutsideIndiana").Nodes("OtherStates").ForeColor.ToString)
        If Me.trvArea.Nodes("OutsideIndiana").Nodes("OtherStates").Checked = True _
            And Me.trvArea.Nodes("OutsideIndiana").Nodes("OtherStates").ForeColor = Color.Black Then
            strbState.Append("Other,")
            k = 100
        Else
            For Each NodeState In Me.trvArea.Nodes("OutsideIndiana").Nodes("OtherStates").Nodes
                If NodeState.Checked = True Then
                    strbState.Append("'" & NodeState.Name & "',")
                    k = k + 1
                Else
                    '         bAllChildren = False
                End If
            Next NodeState
        End If
        'End If
Indiana:
        If Me.trvArea.Nodes("AllIndiana").Checked = True Then
            '   If k = 100 Then
            'strbState.Replace(strbState.ToString, "%,")
            'Else
            strbState.Append("'IN',")
            'k = k + 1
            'End If
        End If

COUNTRY:
        'modGlobalVar.Msg(Me.trvArea.Nodes("OutsideIndiana").Nodes("International").ForeColor.ToKnownColor, , "INternational")
        If Me.trvArea.Nodes("OutsideIndiana").Nodes("International").Checked = True And Me.trvArea.Nodes("OutsideIndiana").Nodes("International").ForeColor.ToKnownColor = 35 Then
            strbCntry.Append("All,")
            h = 100
        Else
            If Me.trvArea.Nodes("OutsideIndiana").Nodes("International").Nodes.Count > 0 Then
                For Each node In Me.trvArea.Nodes("OutsideIndiana").Nodes("International").Nodes
                    If node.Checked = True Then
                        strbCntry.Append("'" & node.Tag & "',")
                        h += 1
                    End If
                Next node

            End If
        End If

SATELLITERegion:
        For Each RegionNode As TreeNode In Me.trvArea.Nodes("SatelliteRegions").Nodes   'EACH SATELLITE REGION
            ' modGlobalVar.Msg(Rgn.Checked.ToString, , Rgn.ForeColor.ToString)'color[empty]
            If RegionNode.Checked = True Then   'SATELLITE REGION CHECKED
Region:
                If RegionNode.ForeColor = Color.DarkGray Then   'some children checked

                    For Each NodeCounty In RegionNode.Nodes 'EACH COUNTY
                        iUnchecked = 0
                        If NodeCounty.Checked = True And NodeCounty.Nodes.Count > 0 Then 'NODE HAS CHILDREN
City:
                            For Each CityNode As TreeNode In NodeCounty.Nodes
                                If CityNode.Checked = False Then    'COUNT UNCHECKED CITIES
                                    iUnchecked = iUnchecked + 1
                                End If
                            Next CityNode
County:
                            If iUnchecked = 0 Then  'all cities are selected

                                strbCounty.Append("'" & NodeCounty.Name & "',") 'APPEND COUNTY INSTEAD
                            Else
                                For Each CityNode As TreeNode In NodeCounty.Nodes
                                    '  Me.lblTmp.Text = UCase(NodeCounty.Name) & " " & CityNode.Name
                                    If CityNode.Checked = False Then    'COUNT UNCHECKED CITIES
                                    Else
                                        strbCity.Append("'" & CityNode.Name & "',")    'APPEND EACH CITY
                                    End If
                                Next CityNode
                            End If
                        Else 'county not checked

                        End If  'end has children
                    Next NodeCounty 'NodeCounty 
                Else    'ALL CHILDREN checked USE REGION
                    strbRegion.Append("'" & RegionNode.Tag & "',")
                End If
            Else 'SKIP REGION ALTOGETEHR
                ' End If  'end All Children Checked
            End If 'end Satellite Region checked
        Next RegionNode

MailingArea:
        For Each AreaNode As TreeNode In Me.trvArea.Nodes("MailingAreas").Nodes   'EACH SATELLITE REGION
            ' modGlobalVar.Msg(Rgn.Checked.ToString, , Rgn.ForeColor.ToString)'color[empty]
            If AreaNode.Checked = True Then   'MAILING AREA CHECKED
Area:
                '  Me.lblTmp.Text = RegionNode.Name
                '     If (AreaNode.ForeColor = Nothing Or AreaNode.ForeColor = Color.Black) Then  'ALL CHILDREN CHECKED
                'strbArea.Append("'" & AreaNode.Tag & "',")
                'Else    'LOOK FOR UNCHECKED CHILDREN
MACounty:
                For Each NodeCounty In AreaNode.Nodes 'EACH COUNTY
                    If NodeCounty.Checked = True Then
                        strbCounty.Append("'" & NodeCounty.Name & "',") 'APPEND COUNTY INSTEAD
                    End If
                Next NodeCounty
                '   End If   'whole area selected
            End If   'area selected
        Next AreaNode


        bNoGeography = True
        If strbCntry.ToString = String.Empty Then
        Else
            bNoGeography = False
            strSQL.Parameters.Add("@Cntry", SqlDbType.Text)
            strSQL.Parameters("@Cntry").Value = strbCntry.ToString.Substring(0, Len(strbCntry.ToString) - 1)
        End If
        If strbRegion.ToString = String.Empty Then
        Else
            bNoGeography = False
            strSQL.Parameters.Add("@Region", SqlDbType.Text)
            strSQL.Parameters("@Region").Value = strbRegion.ToString.Substring(0, Len(strbRegion.ToString) - 1)
        End If

        If strbCounty.ToString = String.Empty Then
        Else
            bNoGeography = False
            strSQL.Parameters.Add("@County", SqlDbType.Text)
            strSQL.Parameters("@County").Value = strbCounty.ToString.Substring(0, Len(strbCounty.ToString) - 1)
        End If
        ' If j > 0 Then
        If strbCity.ToString = String.Empty Then
        Else
            bNoGeography = False
            strSQL.Parameters.Add("@City", SqlDbType.Text)
            strSQL.Parameters("@City").Value = strbCity.ToString.Substring(0, Len(strbCity.ToString) - 1)
        End If
        '  If k > 0 Then
        If strbState.ToString = String.Empty Then
        Else
            bNoGeography = False
            strSQL.Parameters.Add("@State", SqlDbType.Text)
            ' Select Case k
            '     Case Is = 333
            ' strSQL.Parameters("@State").Value = "out"
            '   Case Is = 444
            '       strSQL.Parameters("@State").Value = "('IN')"
            '     Case Else
            strSQL.Parameters("@State").Value = strbState.ToString.Substring(0, Len(strbState.ToString) - 1)
            ' End Select
            ' modGlobalVar.Msg(strSQL.Parameters("@State").Value.ToString, , "@State")
        End If
        ' End If
        If bNoGeography = True Then
            modGlobalVar.msg("FYI Incomplete Geographic Area", "Select a city, county, region, mailing area, state, or country", MessageBoxButtons.OK, MessageBoxIcon.Information)
            GoTo closeall
        End If

BranchBetweenMailEmail:
        If Me.rbAddress.Checked = True Then
            GoTo STREAMWRITER
        Else 'rbEmail.checked = true
            ' GoTo EmailForm
            OpenEmailForm(strSQL, strEmailFormHeading)
            GoTo CloseAll
        End If


STREAMWRITER:

        Try
            sw = New IO.StreamWriter((UserPath & "MailLbl.txt"))
        Catch ex As Exception
            modGlobalVar.msg("ERROR", "CLOSE the document listed below then try again." & NextLine & ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
            GoTo CloseStreamWriter
        End Try

        If Not SCConnect() Then
            Exit Sub
        End If

        Dim drdr As SqlDataReader
        Try
            drdr = strSQL.ExecuteReader
            ' modGlobalVar.Msg(strSQL.CommandText.ToString, , drdr.HasRows.ToString)
        Catch ex As Exception
            modGlobalVar.msg("ERROR: DR", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
            GoTo CloseStreamWriter
        End Try
        'For y As Integer = 0 To strSQL.Parameters.Count - 1
        '    modGlobalVar.Msg(strSQL.Parameters(y).ParameterName, , strSQL.Parameters(y).Value.ToString)
        'Next

        Dim r As Int16 = 0
        Dim m As Integer = drdr.FieldCount
        If drdr.HasRows Then

            'get col headings
            For x As Int16 = 0 To drdr.FieldCount - 1
                sw.Write(drdr.GetName(x).ToString & Chr(9))
            Next x
            sw.WriteLine()
            'TODO why is this -1 with 'mailing labels' added
            While drdr.Read
                For x As Int16 = 0 To drdr.FieldCount - 2
                    If IsDBNull(drdr(x)) Then
                        sw.Write(Chr(9))
                    Else
                        sw.Write(drdr.GetValue(x) & Chr(9))
                    End If
                Next x
                ' sw.Write("Mailing Labels")
                sw.WriteLine()
                r = r + 1
            End While
            sw.Close()
        Else
            modGlobalVar.msg(MsgCodes.noResultCancel) '"no data found", "Cancelling mailing label request", MessageBoxButtons.OK, MessageBoxIcon.Information)
            For y As Integer = 0 To strSQL.Parameters.Count - 1
                '   modGlobalVar.Msg(strSQL.Parameters(y).Value.ToString, , strSQL.Parameters(y).ParameterName)
            Next
            ' sw.Close()
            GoTo CloseStreamWriter
        End If
        ' sw.Close()
CommentOutForTest:
        Dim datafileName As String = "MailLbl"
        Dim fullDatafileName As String = modPopup.DataToExcel(datafileName, "MailLbl")
        If fullDatafileName = String.Empty Then
            modGlobalVar.msg("Error: create DataToExcel", "MailLbl could not create excel", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Else
            If OpenFile(fullDatafileName) = True Then
                If Me.rbEmail.Checked = True Then
                    modGlobalVar.msg("Done: " & r.ToString, " email list datafile", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Else
                    modGlobalVar.msg("Done: " & r.ToString, "Mailing label datafile", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            End If
        End If

CloseStreamWriter:
        Try
            drdr.Close()
            fullDatafileName = Nothing
            datafileName = Nothing
        Catch ex As Exception
        Finally
        End Try
        sc.Close()
        Try
            sw.Close()
        Catch ex As Exception
        End Try
        sw = Nothing
        GoTo CloseAll

EmailForm:
        '  OpenEmailForm(strSQL)


CloseAll:
        Try
            strSQL.Parameters.Clear()
            strbParam = Nothing
            strbState = Nothing
            strbArea = Nothing
            strbCity = Nothing
            strbCounty = Nothing
        Catch ex As Exception
        End Try

        MouseDefault()

    End Sub 'btnGo



#End Region 'search

#Region "General"

    'TREEVIEW CHECK UNCHECK
    Private Sub TreeView1_AfterCheck(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) _
        Handles trvArea.AfterCheck, trvType.AfterCheck, trvPeople.AfterCheck
        '  modGlobalVar.Msg(e.Node.Name, , "in AfterCheck")


        If e.Action <> TreeViewAction.Unknown Then   'prevents recursive action resetting checks
            CheckAllNodes(e.Node)
            If e.Node.Level = 0 Then
            Else
                ShadeParent(e.Node)
            End If
        End If

        'If Not isLoaded Then
        '    TickChildren(e.Node)
        'End If
        'If e.Node.Checked = True Then

        ' End If
        ''can't gray tick, leave checked and use gray text for now
        'If e.Action <> TreeViewAction.Unknown Then   'prevents recursive action resetting checks
        '    If e.Node.Nodes.Count > 0 Then
        '        '     If e.Node.Level = 0 Then    'is parent
        '        TickChildren(e.Node)

        '        If e.Node.Checked = True Then
        '            e.Node.ForeColor = Color.Black
        '        Else
        '            e.Node.ForeColor = Color.DarkGray
        '        End If
        '    End If
        '    If e.Node.Level = 0 Then
        '    Else
        '        ShadeParent(e.Node)
        '    End If
        'Else
        'End If
    End Sub

    'TICK CHILDREN
    Private Sub CheckAllNodes(ByRef SelectedNode As TreeNode)
        'TO USE
        'CheckAllNodes(RootNode)        ' checks only the RootNode
        'CheckAllNodes(RootNode, True)     ' checks only the RootNode and its immediate children, but not children of children and onwards.
        'CheckAllNodes(RootNode, True, True)  ' checks RootNode and its entire tree
        '   http://social.msdn.microsoft.com/Forums/en/vbgeneral/thread/e6310a84-1433-437e-b64a-b3026a9ffc1f

        ' treeNode.Checked = True
        '  If checkChildren Then
        If SelectedNode.Checked = True And SelectedNode.GetNodeCount(False) > 0 Then
            SelectedNode.ForeColor = Color.Black
        End If
        Try
            For Each Childnode As TreeNode In SelectedNode.Nodes
                Childnode.Checked = SelectedNode.Checked
                For Each Grandchildnode As TreeNode In Childnode.Nodes
                    Grandchildnode.Checked = SelectedNode.Checked
                    Try
                        For Each ggchildnode As TreeNode In Grandchildnode.Nodes
                            ggchildnode.Checked = SelectedNode.Checked
                        Next
                    Catch ex As Exception

                    End Try
                Next
                ' If checkEntireTree Then CheckAllNodes(node, True, True)
            Next
        Catch ex As Exception
        End Try


    End Sub

    'TREEVIEW FORMAT PARENT
    Private Sub ShadeParent(ByVal TickedNode As TreeNode)
        Dim pNode As TreeNode
        Dim cntUnchecked As Integer = 0
        Dim cntChecked As Integer = 0
        Dim bChecked As Boolean = False
        Dim bShaded As Boolean = False
        Dim clr As Color

        pNode = TickedNode.Parent
        For Each childnode As TreeNode In pNode.Nodes()
            If childnode.Checked = False Or childnode.ForeColor = Color.DarkGray Then
                cntUnchecked = cntUnchecked + 1
            End If
        Next childnode
        '     modGlobalVar.Msg(cntUnchecked, , "Pnode:" + pNode.Nodes.Count.ToString)
        Select Case cntUnchecked
            Case Is = pNode.Nodes.Count 'all unchecked
                bChecked = False
                clr = Color.Black
            Case Is > 0    'some unchecked
                bChecked = True
                clr = Color.DarkGray
            Case Else       'all checked
                bChecked = True
                clr = Color.Black
        End Select

        Try
Parent:
            pNode.Checked = bChecked
            pNode.ForeColor = clr
Grandparent:
            For Each node In pNode.Parent.Nodes
                If node.Checked = True Then 'some sister nodes are checked leave grandparent alone
                    cntChecked = cntChecked + 1
                End If
                If node.ForeColor = Color.DarkGray Then
                    bShaded = True
                End If
            Next

            If cntChecked = 0 Then
                pNode.Parent.Checked = False
                pNode.Parent.ForeColor = Color.Black
            Else
                pNode.Parent.Checked = True
                If cntChecked = pNode.Parent.GetNodeCount(False) Then
                    pNode.Parent.ForeColor = pNode.ForeColor
                Else
                    If bShaded = True Then
                        pNode.Parent.ForeColor = Color.DarkGray
                    Else
                        pNode.Parent.ForeColor = Color.Black
                    End If
                End If
            End If

        Catch ex As Exception

        End Try

    End Sub

    'OPEN INDIANA REGION MAP
    Private Sub btnMap_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
         Handles btnMap.Click ', miCountyMap.Click
        ' Create an instance of Word, and make it visible.
        Dim wrdApp As Microsoft.Office.Interop.Word.Application
        Dim wrdDoc As Microsoft.Office.Interop.Word.Document
        wrdApp = CreateObject("Word.Application")
        wrdApp.Visible = True

        '        ' Add a new document.

        wrdDoc = wrdApp.Documents.Add
        wrdDoc = wrdApp.Documents.Open("\\ICCNAS1\Users\Shared\Satellites\GeneralInfo\StateMap - Counties&Satellites.doc") 'Population Centers.doc")
        '        wrdDoc.Select()
        wrdDoc = Nothing
    End Sub

    'BTN HELP
    Private Sub btnHelp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHelp.Click
        Try
            Process.Start(SOPPath & "Graphics\InfoCtrHelpMailingLbl2.htm")
        Catch ex As Exception

            modGlobalVar.msg("MAILING LABEL HELP", "1) Click as many of the checkboxes as needed to obtain the addresses you require." & NextLine & "2) Click the Go button." & NextLine & "3) It may take afew moments if there are many names.  " & NextLine & "     Excel will open with the results to sort or use as merge data." & NextLine & NextLine _
            & "Indiana Map: " & NextLine & "click Indiana Map button to view Satellite Regions and Mailing Areas." & NextLine & NextLine _
    & "- The central county of a Satellite Region is Bold; contiguous counties are in upper case." & NextLine _
    & "- Cities are listed under the counties listed under Satellite Regions." & NextLine _
    & "- Numbers after a City, County, or Country indicate the number of active Organizations of any type. " & NextLine _
    & "- Numbers after a State indicate the number of active Contacts of any type." & NextLine & NextLine _
    & "Note: 'Test...' organizations will not show up on labels.", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub

    'UNDO
    Protected Overrides Function ProcessDialogKey(ByVal keyData As Keys) As Boolean
        If keyData = System.Windows.Forms.Keys.Escape Then
            modPopup.UndoCtl(Me.ActiveControl)
            Return True  ' True means we've processed the escape key
        Else
            Return MyBase.ProcessDialogKey(keyData)
        End If
    End Function

#End Region   'General

#Region "Saved Mail Lists"

    'call Load Saved MailList Combo
    Private Sub TabControl1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
         Handles TabControl1.SelectedIndexChanged

        Select Case TabControl1.SelectedTab.Tag
            Case Is = "SavedMailList"
                LoadMailListCBO()

            Case Else

        End Select

    End Sub

    'load mail list cbo
    Private Sub LoadMailListCBO()
        'use dataset so is editable
        Dim tbl As New DataTable("tbl")

        Select Case usr
            Case Is = DBAdmin.StaffID
                LoadDataTable(tbl, "SELECT MailListID,  LEFT(MailListName + SPACE(15), 20) + Cast(Createstaffnum as varchar) as MailListName from luMailList")
            Case Is = 222 '222 =jerri, 225 = tim
                LoadDataTable(tbl, "SELECT MailListID, MailListName from luMailList WHERE createstaffnum in (222,225)")
            Case Else
                LoadDataTable(tbl, "SELECT MailListID, MailListName from luMailList WHERE createstaffnum = " & usr)
        End Select

        Try
            Me.cboPrintFlag.DataSource = tbl
        Catch ex As Exception
            MsgBox(ex.Message, , "error load cbo  ")
        End Try
        Me.cboPrintFlag.SelectedIndex = -1
        Me.cboPrintFlag.SelectedIndex = -1

    End Sub

    'LOAD DATASET for MEMBERS of SELECTED MAIL LIST
    Private Sub LoadMailMembers()
        If Me.chkShowMembers.Checked = True Then
            LoadDataTable(tMailMembers, "SELECT MailListNum, ContactNum, OrgName, Lastname, FirstName, JobTitle, CASE WHEN OKEmail = 1 THEN '' ELSE 'N' END as OKEmail, CASE WHEN OKPostal = 1 then '' ELSE 'N' END as OKPostal, Email from vwMailListsMembers  WHERE MailListnum = " & Me.cboPrintFlag.SelectedValue)
        Else
            tMailMembers.Clear()
        End If
    End Sub

    'LOAD DATASET for DETAIL of SELECTED MAIL LIST
    Private Sub LoadMailDetail()
        dsMailList.Tables(0).Clear()
        sqlGetMailListDetail.Parameters("@ListID").Value = Me.cboPrintFlag.SelectedValue

        '   MsgBox(Me.cboPrintFlag.SelectedValue.ToString, , "loading detail")
        ' daMailList.Fill(dsMailList.Tables(0)) 'tMailDetail)
        LoadDataTable(dsMailList.Tables(0), sqlGetMailListDetail)
    End Sub

    'CALL RELOAD DATASETS
    Private Sub cboPrintFlag_Selectionchangecommited(sender As System.Object, e As System.EventArgs) _
      Handles cboPrintFlag.SelectedIndexChanged 'cboPrintFlag.SelectionChangeCommitted, cboPrintFlag.DropDownClosed

        If Me.cboPrintFlag.SelectedIndex = -1 Then
            tMailMembers.Clear()
            tMailDetail.Clear()
            Exit Sub
        End If

        LoadMailMembers()
        LoadMailDetail()

        If FirstTime = 0 Then 'bind fields first time round
            BindMailListFields()
            Me.dgrdMember.DataSource = tMailMembers
            FirstTime += 1
            Exit Sub
        Else 'update previous selection
            ' UpdateMailListFields()
        End If

    End Sub

    'BIND MAIL DETAIL FIELDS TO ADHOC TABLE
    Private Sub BindMailListFields()
        Me.fldCreateDate.DataBindings.Add(New System.Windows.Forms.Binding("Text", dsMailList.Tables(0), "CreateDate", True))
        Me.fldNumMembers.DataBindings.Add(New System.Windows.Forms.Binding("Text", dsMailList.Tables(0), "NumMembers"))
        Me.fldCreatedBy.DataBindings.Add(New System.Windows.Forms.Binding("Text", dsMailList.Tables(0), "ListOwner", True))
        Me.fldDescription.DataBindings.Add(New System.Windows.Forms.Binding("Text", dsMailList.Tables(0), "Description", True))
        Me.fldExpireDate.DataBindings.Add(New System.Windows.Forms.Binding("Text", dsMailList.Tables(0), "ExpirationDate", True))
        Me.fldMailListID.DataBindings.Add(New System.Windows.Forms.Binding("Text", dsMailList.Tables(0), "MailListID", True))
 
    End Sub

    Private Sub fldDescription_Leave(sender As System.Object, e As System.EventArgs) Handles fldDescription.Leave, fldExpireDate.Leave

        If Me.fldExpireDate.Text > String.Empty Then
            sqlUpdateMailListDetail.CommandText = "UPDATE luMailList SET ExpirationDate = '" & Me.fldExpireDate.Text & "', Description = '" & Me.fldDescription.Text &
                "' WHERE MailListID = " & Me.fldMailListID.Text
        Else
            sqlUpdateMailListDetail.CommandText = "UPDATE luMailList SET  Description = '" & Me.fldDescription.Text &
                "' WHERE MailListID = " & Me.fldMailListID.Text
        End If
        Try
            ' daMailList.Update(dsMailList.Tables(0)) 'tMailDetail)
            If Not SCConnect() Then
                Exit Sub
            End If
            sqlUpdateMailListDetail.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox(ex.Message, , "UpdateError")
        Finally
            sc.Close()
        End Try

        ' daMailList.Update(dsMailList.Tables(0))
    End Sub



    'GRID VISIBILITY
    Private Sub chkShowMembers_CheckedChanged(sender As Object, e As System.EventArgs) Handles chkShowMembers.CheckedChanged
        Me.dgrdMember.Visible = Me.chkShowMembers.Checked
        If Me.cboPrintFlag.SelectedIndex > -1 Then
            LoadMailMembers()
        End If

    End Sub

    'open contact detail
    Private Sub dgrdMember_DoubleClick(sender As Object, e As System.EventArgs) Handles dgrdMember.DoubleClick
        OpenMainContact(Me.dgrdMember.Item(dgrdMember.CurrentRowIndex, tMailMembers.Columns("Contactnum").Ordinal), Me.dgrdMember.Item(dgrdMember.CurrentRowIndex, tMailMembers.Columns("Lastname").Ordinal), "", 0)
    End Sub



#End Region 'saved mail lists



 

End Class