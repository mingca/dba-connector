Imports System.Text
Imports System.Data.SqlClient
Imports System.IO
Imports Microsoft.Office.Interop


Public Class frmGeography
    Inherits System.Windows.Forms.Form
    Dim WhatRField As String

    Dim arCity As New ArrayList
    Dim arCounty As New ArrayList
    Dim arZip As New ArrayList
    Dim arFIPS As New ArrayList

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Me.ClientSize = New System.Drawing.Size(1000, 660)
        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
        Forms.Remove(Me)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents pnlLookUpRegion As System.Windows.Forms.Panel
    Friend WithEvents StatusBar2 As System.Windows.Forms.StatusBar
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents rtxtRegionResult As System.Windows.Forms.RichTextBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents grpWhat As System.Windows.Forms.GroupBox
    Friend WithEvents rbRPrefix As System.Windows.Forms.RadioButton
    Friend WithEvents rbRZip As System.Windows.Forms.RadioButton
    Friend WithEvents rbRRegion As System.Windows.Forms.RadioButton
    Friend WithEvents rbRCounty As System.Windows.Forms.RadioButton
    Friend WithEvents rbRCity As System.Windows.Forms.RadioButton
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents txtInput As System.Windows.Forms.TextBox
    Friend WithEvents cboLst As System.Windows.Forms.ComboBox
    Friend WithEvents StatusBarPanel1 As System.Windows.Forms.StatusBarPanel
    Friend WithEvents StatusBarPanel2 As System.Windows.Forms.StatusBarPanel
    Friend WithEvents btnHelpRegion As System.Windows.Forms.Button
    Friend WithEvents MainMenu1 As System.Windows.Forms.MainMenu
    Friend WithEvents MenuItem1 As System.Windows.Forms.MenuItem
    Friend WithEvents miClose As System.Windows.Forms.MenuItem
    Friend WithEvents miSearch As System.Windows.Forms.MenuItem
    Friend WithEvents miHelp As System.Windows.Forms.MenuItem
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents miCountyMap As System.Windows.Forms.MenuItem
    Friend WithEvents LinkLabel1 As System.Windows.Forms.LinkLabel
    Friend WithEvents Label2 As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmGeography))
        Me.pnlLookUpRegion = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.rtxtRegionResult = New System.Windows.Forms.RichTextBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.grpWhat = New System.Windows.Forms.GroupBox()
        Me.rbRPrefix = New System.Windows.Forms.RadioButton()
        Me.rbRZip = New System.Windows.Forms.RadioButton()
        Me.rbRRegion = New System.Windows.Forms.RadioButton()
        Me.rbRCounty = New System.Windows.Forms.RadioButton()
        Me.rbRCity = New System.Windows.Forms.RadioButton()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.txtInput = New System.Windows.Forms.TextBox()
        Me.cboLst = New System.Windows.Forms.ComboBox()
        Me.StatusBar2 = New System.Windows.Forms.StatusBar()
        Me.StatusBarPanel1 = New System.Windows.Forms.StatusBarPanel()
        Me.StatusBarPanel2 = New System.Windows.Forms.StatusBarPanel()
        Me.btnHelpRegion = New System.Windows.Forms.Button()
        Me.MainMenu1 = New System.Windows.Forms.MainMenu(Me.components)
        Me.MenuItem1 = New System.Windows.Forms.MenuItem()
        Me.miCountyMap = New System.Windows.Forms.MenuItem()
        Me.miClose = New System.Windows.Forms.MenuItem()
        Me.miSearch = New System.Windows.Forms.MenuItem()
        Me.miHelp = New System.Windows.Forms.MenuItem()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.Button1 = New System.Windows.Forms.Button()
        Me.LinkLabel1 = New System.Windows.Forms.LinkLabel()
        Me.pnlLookUpRegion.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.grpWhat.SuspendLayout()
        CType(Me.StatusBarPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.StatusBarPanel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlLookUpRegion
        '
        Me.pnlLookUpRegion.BackColor = System.Drawing.SystemColors.Control
        Me.pnlLookUpRegion.Controls.Add(Me.Label1)
        Me.pnlLookUpRegion.Controls.Add(Me.rtxtRegionResult)
        Me.pnlLookUpRegion.Controls.Add(Me.Panel1)
        Me.pnlLookUpRegion.Location = New System.Drawing.Point(13, 43)
        Me.pnlLookUpRegion.Name = "pnlLookUpRegion"
        Me.pnlLookUpRegion.Size = New System.Drawing.Size(916, 530)
        Me.pnlLookUpRegion.TabIndex = 1
        Me.pnlLookUpRegion.TabStop = True
        Me.pnlLookUpRegion.Tag = "SatelliteRegion"
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(7, 17)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(80, 14)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = "Search Criteria"
        '
        'rtxtRegionResult
        '
        Me.rtxtRegionResult.AcceptsTab = True
        Me.rtxtRegionResult.Location = New System.Drawing.Point(164, 31)
        Me.rtxtRegionResult.Name = "rtxtRegionResult"
        Me.rtxtRegionResult.ReadOnly = True
        Me.rtxtRegionResult.Size = New System.Drawing.Size(723, 470)
        Me.rtxtRegionResult.TabIndex = 0
        Me.rtxtRegionResult.TabStop = False
        Me.rtxtRegionResult.Text = "Result appears here."
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.InactiveBorder
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.grpWhat)
        Me.Panel1.Controls.Add(Me.btnSearch)
        Me.Panel1.Controls.Add(Me.txtInput)
        Me.Panel1.Controls.Add(Me.cboLst)
        Me.Panel1.Location = New System.Drawing.Point(4, 32)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(152, 330)
        Me.Panel1.TabIndex = 5
        '
        'grpWhat
        '
        Me.grpWhat.Controls.Add(Me.rbRPrefix)
        Me.grpWhat.Controls.Add(Me.rbRZip)
        Me.grpWhat.Controls.Add(Me.rbRRegion)
        Me.grpWhat.Controls.Add(Me.rbRCounty)
        Me.grpWhat.Controls.Add(Me.rbRCity)
        Me.grpWhat.Location = New System.Drawing.Point(18, 112)
        Me.grpWhat.Name = "grpWhat"
        Me.grpWhat.Size = New System.Drawing.Size(118, 145)
        Me.grpWhat.TabIndex = 2
        Me.grpWhat.TabStop = False
        Me.grpWhat.Text = "Select Search Field"
        '
        'rbRPrefix
        '
        Me.rbRPrefix.ForeColor = System.Drawing.SystemColors.ControlDark
        Me.rbRPrefix.Location = New System.Drawing.Point(13, 110)
        Me.rbRPrefix.Name = "rbRPrefix"
        Me.rbRPrefix.Size = New System.Drawing.Size(97, 18)
        Me.rbRPrefix.TabIndex = 4
        Me.rbRPrefix.Tag = ""
        Me.rbRPrefix.Text = "phone prefix"
        '
        'rbRZip
        '
        Me.rbRZip.Location = New System.Drawing.Point(13, 88)
        Me.rbRZip.Name = "rbRZip"
        Me.rbRZip.Size = New System.Drawing.Size(97, 18)
        Me.rbRZip.TabIndex = 3
        Me.rbRZip.Tag = ""
        Me.rbRZip.Text = "Zip"
        '
        'rbRRegion
        '
        Me.rbRRegion.Location = New System.Drawing.Point(13, 66)
        Me.rbRRegion.Name = "rbRRegion"
        Me.rbRRegion.Size = New System.Drawing.Size(84, 18)
        Me.rbRRegion.TabIndex = 2
        Me.rbRRegion.Text = "Region"
        '
        'rbRCounty
        '
        Me.rbRCounty.Location = New System.Drawing.Point(13, 45)
        Me.rbRCounty.Name = "rbRCounty"
        Me.rbRCounty.Size = New System.Drawing.Size(94, 17)
        Me.rbRCounty.TabIndex = 1
        Me.rbRCounty.Text = "County"
        '
        'rbRCity
        '
        Me.rbRCity.Checked = True
        Me.rbRCity.Location = New System.Drawing.Point(13, 25)
        Me.rbRCity.Name = "rbRCity"
        Me.rbRCity.Size = New System.Drawing.Size(91, 16)
        Me.rbRCity.TabIndex = 0
        Me.rbRCity.TabStop = True
        Me.rbRCity.Text = "City"
        '
        'btnSearch
        '
        Me.btnSearch.BackColor = System.Drawing.SystemColors.Control
        Me.btnSearch.Location = New System.Drawing.Point(40, 72)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(75, 25)
        Me.btnSearch.TabIndex = 1
        Me.btnSearch.Text = "Search"
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'txtInput
        '
        Me.txtInput.Location = New System.Drawing.Point(16, 16)
        Me.txtInput.Name = "txtInput"
        Me.txtInput.Size = New System.Drawing.Size(120, 20)
        Me.txtInput.TabIndex = 0
        Me.txtInput.Text = "Enter search text here"
        Me.ToolTip1.SetToolTip(Me.txtInput, "Enter search text here")
        '
        'cboLst
        '
        Me.cboLst.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboLst.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboLst.Location = New System.Drawing.Point(5, 42)
        Me.cboLst.Name = "cboLst"
        Me.cboLst.Size = New System.Drawing.Size(137, 21)
        Me.cboLst.TabIndex = 1
        Me.cboLst.TabStop = False
        Me.ToolTip1.SetToolTip(Me.cboLst, "Select from list")
        Me.cboLst.Visible = False
        '
        'StatusBar2
        '
        Me.StatusBar2.Location = New System.Drawing.Point(0, 617)
        Me.StatusBar2.Name = "StatusBar2"
        Me.StatusBar2.Panels.AddRange(New System.Windows.Forms.StatusBarPanel() {Me.StatusBarPanel1, Me.StatusBarPanel2})
        Me.StatusBar2.ShowPanels = True
        Me.StatusBar2.Size = New System.Drawing.Size(984, 24)
        Me.StatusBar2.TabIndex = 8
        '
        'StatusBarPanel1
        '
        Me.StatusBarPanel1.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents
        Me.StatusBarPanel1.MinWidth = 200
        Me.StatusBarPanel1.Name = "StatusBarPanel1"
        Me.StatusBarPanel1.Width = 200
        '
        'StatusBarPanel2
        '
        Me.StatusBarPanel2.Alignment = System.Windows.Forms.HorizontalAlignment.Right
        Me.StatusBarPanel2.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring
        Me.StatusBarPanel2.Name = "StatusBarPanel2"
        Me.StatusBarPanel2.Text = "Use this window to get the Region, Counties, Cities, and Zip Codes by entering an" & _
    "y one.  TimeZone is included in result."
        Me.StatusBarPanel2.Width = 767
        '
        'btnHelpRegion
        '
        Me.btnHelpRegion.BackColor = System.Drawing.SystemColors.Control
        Me.btnHelpRegion.Font = New System.Drawing.Font("Microsoft Sans Serif", 22.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnHelpRegion.ForeColor = System.Drawing.SystemColors.Control
        Me.btnHelpRegion.Image = Global.InfoCtr.My.Resources.Resources.btnHelp
        Me.btnHelpRegion.Location = New System.Drawing.Point(860, 5)
        Me.btnHelpRegion.Name = "btnHelpRegion"
        Me.btnHelpRegion.Size = New System.Drawing.Size(25, 25)
        Me.btnHelpRegion.TabIndex = 217
        Me.btnHelpRegion.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnHelpRegion.UseVisualStyleBackColor = False
        '
        'MainMenu1
        '
        Me.MainMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem1, Me.miSearch, Me.miHelp})
        '
        'MenuItem1
        '
        Me.MenuItem1.Index = 0
        Me.MenuItem1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.miCountyMap, Me.miClose})
        Me.MenuItem1.Text = "File"
        '
        'miCountyMap
        '
        Me.miCountyMap.Index = 0
        Me.miCountyMap.Text = "Open Indiana County Map"
        '
        'miClose
        '
        Me.miClose.Index = 1
        Me.miClose.Text = "Close Window"
        '
        'miSearch
        '
        Me.miSearch.Index = 1
        Me.miSearch.Text = "Search"
        '
        'miHelp
        '
        Me.miHelp.Index = 2
        Me.miHelp.Text = "Help"
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.Desktop
        Me.Label2.Location = New System.Drawing.Point(16, 20)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(416, 14)
        Me.Label2.TabIndex = 222
        Me.Label2.Text = "FIND CITY, COUNTY, ZIP and TIMEZONE per REGION"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(677, 2)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(103, 35)
        Me.Button1.TabIndex = 3
        Me.Button1.Text = "Center Satellite / County Map"
        Me.ToolTip1.SetToolTip(Me.Button1, "Open Word document")
        Me.Button1.UseVisualStyleBackColor = True
        '
        'LinkLabel1
        '
        Me.LinkLabel1.AutoSize = True
        Me.LinkLabel1.BackColor = System.Drawing.Color.LightBlue
        Me.LinkLabel1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LinkLabel1.Image = CType(resources.GetObject("LinkLabel1.Image"), System.Drawing.Image)
        Me.LinkLabel1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LinkLabel1.Location = New System.Drawing.Point(566, 4)
        Me.LinkLabel1.MaximumSize = New System.Drawing.Size(100, 0)
        Me.LinkLabel1.MinimumSize = New System.Drawing.Size(70, 30)
        Me.LinkLabel1.Name = "LinkLabel1"
        Me.LinkLabel1.Size = New System.Drawing.Size(70, 30)
        Me.LinkLabel1.TabIndex = 223
        Me.LinkLabel1.TabStop = True
        Me.LinkLabel1.Text = "Map It"
        Me.LinkLabel1.TextAlign = System.Drawing.ContentAlignment.BottomRight
        Me.ToolTip1.SetToolTip(Me.LinkLabel1, "Open web locator map")
        Me.LinkLabel1.Visible = False
        '
        'frmGeography
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(984, 641)
        Me.Controls.Add(Me.LinkLabel1)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.StatusBar2)
        Me.Controls.Add(Me.pnlLookUpRegion)
        Me.Controls.Add(Me.btnHelpRegion)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Menu = Me.MainMenu1
        Me.Name = "frmGeography"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "INDIANA SEARCH"
        Me.pnlLookUpRegion.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.grpWhat.ResumeLayout(False)
        CType(Me.StatusBarPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.StatusBarPanel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

#Region "Load"
    'LOAD
    Private Sub frmGeography_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        WhatRField = "City"
        'Me.txtInput.Text = "Enter City here"
        Me.txtInput.Focus()
        Me.txtInput.SelectAll()
        Me.AcceptButton = Me.btnSearch
        ' Me.btnHelpRegion.Image = SystemIcons.Question.ToBitmap
        '    Me.ClientSize = New System.Drawing.Size(800, 575)
        Forms.Add(Me)
    End Sub

    'CLOSE
    Protected Sub miCloseForm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
           Handles miClose.Click
        Me.Close()
    End Sub
#End Region

#Region "Search"

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles btnSearch.Click, cboLst.LostFocus, miSearch.Click  ', txtInput.Leave, cboRegionlu.SelectedIndexChanged
        Dim usrSrch As String
        Dim ctl As Control
        MouseWait()
        Me.StatusBarPanel1.Text = "Searching..."

        Select Case WhatRField
            Case "Region", "County"
                If cboLst.SelectedIndex > -1 Then
                    usrSrch = Me.cboLst.Text          
                Else
                    ctl = cboLst
                    GoTo UsrError
                End If
            Case Else
                If Me.txtInput.Text = "Enter search text here" Or Me.txtInput.Text = String.Empty Then
                    ctl = Me.txtInput
                    GoTo UsrError
                Else
                    usrSrch = Me.txtInput.Text
                End If
        End Select

        If runChkRegion(WhatRField, usrSrch) > 0 Then
            If WhatRField = "County" Or WhatRField = "Zip" Then
                Me.LinkLabel1.Text = "Map It: " & NextLine & usrSrch
                Me.LinkLabel1.Visible = True
            ElseIf WhatRField = "City" Then
                Me.LinkLabel1.Text = "Map It:" & NextLine & arCity(0)
                Me.LinkLabel1.Visible = True
            Else
                Me.LinkLabel1.Visible = False
            End If
        End If
        Me.StatusBarPanel1.Text = "Done."
        MouseDefault()
        Exit Sub
UsrError:
        modGlobalVar.Msg("Incomplete information", "Please enter search text", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        ctl.Focus()
        Me.StatusBarPanel1.Text = "Done."
        MouseDefault()
    End Sub

    Private Function runChkRegion(ByVal Fld As String, ByVal What As String) As Integer

        'RUN QUERY; USE dataREADER; LOAD ARRAYLIST; SORT; 
        'TRANSFER TO STRINGBUILDER; PRINT TO .TXT FILE; OPEN
        Dim sbCity As New StringBuilder
        Dim sbZip As New StringBuilder
        Dim sbCounty As New StringBuilder
        Dim sbRegion As New StringBuilder
        Dim sbTZ As New StringBuilder

        Dim arRegion As New ArrayList
        Dim arTimeZone As New ArrayList

        arCounty.Clear()
        arCity.Clear()
        arZip.Clear()
        arFIPS.Clear()

        Dim cntCity, cntZip, cntCnty, cntRegion, cntTZ As Integer    'count of each item
        '  Dim lenCity As Int16    'lenth of city array for forced cr by length
        Dim x As Integer = 0 'count total rows
        ' Dim l As Integer

        Dim strRegion As String 'capture 'not in region'
        Dim dr As SqlDataReader
        If WhatRField = "Region" Then
            Fld = "SatelliteRegion"
        End If
        Dim cmd As New SqlCommand("SELECT distinct CASE WHEN POAlternate > '' THEN POAlternate ELSE City END AS City, Zip, County, SatelliteRegion, TimeZone, MailingRegion, CountyFIPS from luCountyZip WHERE " & Fld & " like '" & Replace(What, "*", "%") & "' ", sc)
        ' Dim strWhatRegion As String 'capture radio button

        'NOTE no wildcards is intentional so return only 1 entity
        If Not SCConnect() Then
            Exit Function
        End If

        Try
            dr = cmd.ExecuteReader()

        Catch ex As Exception
            modGlobalVar.Msg("ERROR: reading exception", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        If dr.HasRows Then
        Else
            modGlobalVar.Msg("No items found for...", What, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.rtxtRegionResult.Text = ""
            GoTo CloseWriter
        End If

        While dr.Read() 'check for duplicates, then add to arraylist
            If IsDBNull(dr(0)) Then
            Else
                If arCity.Contains(dr.GetString(0)) Then
                    'strNull = IsNull(dr.GetString(0), "")
                    'If arCity.Contains(strNull) Or strNull = "" Then
                Else
                    arCity.Add(dr.GetString(0))
                    cntCity = cntCity + 1
                End If
            End If
            'strNull = IsNull(dr.GetString(1), "")
            'If arZip.Contains(strNull) Or strNull = "" Then
            If IsDBNull(dr(1)) Then
            Else
                If arZip.Contains(dr.GetString(1)) Then
                Else
                    arZip.Add(dr.GetString(1))
                    cntZip = cntZip + 1
                End If
            End If
            'strNull = IsNull(dr.GetString(2), "")
            'If arCounty.Contains(strNull) Or strNull = "" Then
            If IsDBNull(dr(2)) Then
            Else
                If arCounty.Contains(dr.GetString(2)) Then
                Else
                    arCounty.Add(dr.GetString(2))
                    cntCnty = cntCnty + 1
                    arFIPS.Add(dr.GetString(6))
                End If
            End If
            'If IsNull(dr.GetValue(3), " ") = " " Then
            If IsDBNull(dr(3)) Then
                strRegion = "none"
            Else
                strRegion = dr.GetString(3)
            End If
            If arRegion.Contains(strRegion) Then
            Else
                'strNull = dr.GetString(3)
                ' If arRegion.Contains(dr.GetString(3)) Then
                'Else
                arRegion.Add(strRegion)
                cntRegion = cntRegion + 1
                'End If
            End If

            If IsDBNull(dr(4)) Then
            Else
                If arTimeZone.Contains(dr.GetString(4)) Then
                Else
                    arTimeZone.Add(dr.GetString(4))
                    cntTZ = cntTZ + 1
                End If
            End If
            x += 1    'count number of rows returned
        End While

        dr.Close()
        sc.Close()
        cmd.Dispose()
        'SORT ARRAYLISTS
        arZip.Sort()
        arCity.Sort()
        arCounty.Sort()
        arRegion.Sort()

        'copy arraylist to stringbuilder
        Dim y As Int16
        For y = 0 To x   'potential entries
            If y >= cntCity Then
            Else
                If y Mod 9 = 0 Then 'force carriage return
                    sbCity.Append(NextLine & "        ")
                End If
                sbCity.Append(arCity(y) & ", ")
            End If
            If y >= cntCnty Then
            Else
                If y Mod 9 = 0 Then 'force carriage return
                    sbCounty.Append(NextLine & "        ")
                End If
                sbCounty.Append(arCounty(y) & ", ")
            End If
            If y >= cntZip Then
            Else
                If y Mod 9 = 0 Then 'force carriage return
                    sbZip.Append(NextLine & "        ")
                End If
                sbZip.Append(arZip(y) & ", ")
            End If
            If y >= cntRegion Then
            Else
                If y Mod 9 = 0 Then 'force carriage return
                    sbRegion.Append(NextLine & "        ")
                End If
                sbRegion.Append(arRegion(y) & ", ")
            End If

            If y >= cntTZ Then
            Else
                sbTZ.Append(arTimeZone(y) & ", ")
            End If
        Next

        'remove final comma
        If sbCity.Length > 0 Then sbCity.Remove(sbCity.Length - 2, 2)
        If sbZip.Length > 0 Then sbZip.Remove(sbZip.Length - 2, 2)
        If sbCounty.Length > 0 Then sbCounty.Remove(sbCounty.Length - 2, 2)
        If sbRegion.Length > 0 Then sbRegion.Remove(sbRegion.Length - 2, 2)
        If sbTZ.Length > 0 Then
            sbTZ.Remove(sbTZ.Length - 2, 2)
        Else
            sbTZ.Append("unknown")
        End If
        Me.rtxtRegionResult.Text = "                   Results for " & UCase(WhatRField) & ": " & UCase(What) & "  -  " & UCase(sbTZ.ToString) & " Time Zone" & NextLine & NextLine & NextLine
        ' Me.rtxtRegionResult.Text = Me.rtxtRegionResult.Text & "                   ------------------------------------------------------------"& NextLine &  NextLine
        Me.rtxtRegionResult.Text = Me.rtxtRegionResult.Text & "REGION: " & sbRegion.ToString & NextLine & NextLine
        Me.rtxtRegionResult.Text = Me.rtxtRegionResult.Text & "COUNTY: " & sbCounty.ToString & NextLine & NextLine
        Me.rtxtRegionResult.Text = Me.rtxtRegionResult.Text & "CITY: " & sbCity.ToString & NextLine & NextLine
        Me.rtxtRegionResult.Text = Me.rtxtRegionResult.Text & "ZIPCODE: " & sbZip.ToString & NextLine

        Return 1
        '...................... 
        ''write to text file
        'sw.WriteLine("SEARCH FOR " & Fld & " " & usrSel)
        'sw.WriteLine("-------------------------------------" & NextLine)
        'sw.WriteLine("   REGION:")
        'sw.WriteLine(sbRegion.ToString & NextLine)
        'sw.WriteLine("   COUNTY:")
        'sw.WriteLine(sbCounty.ToString & NextLine)
        'sw.WriteLine("   CITY:")
        'sw.WriteLine(sbCity.ToString & NextLine)
        'sw.WriteLine("   ZIPCODE:")
        'sw.WriteLine(sbZip.ToString)

        ''open text file
        'Try
        '    System.Diagnostics.Process.Start(path)
        'Catch ex As Exception
        '    modGlobalVar.Msg(ex.Message, , "can't open")
        'End Try
        '.....................................
CloseWriter:
        dr.Close()
        sc.Close()
        cmd.Dispose()
        Return 0
        'usrSel = Nothing

    End Function
#End Region 'search

#Region "General"
    'CLEAR RESULT BOX
    Private Sub txtInput_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) _
            Handles txtInput.TextChanged
        '   Me.rtxtRegionResult.Text = ""
        Me.txtInput.Focus()
    End Sub

    'HELP BUTTON
    Private Sub btnHelpRegion_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
          Handles btnHelpRegion.Click, miHelp.Click
        modGlobalVar.Msg("HELP", "TO SEARCH: for Cities, Counties or Zips in a SatelliteRegion." & NextLine & "1. Use the radio buttons to select a field to search. " & NextLine & " Select a County or Region from the drop-down box; type a City name or Zip code in the 'Enter search text' box.  " & NextLine & "2. Click the Search button or press Enter." & NextLine & NextLine & "NOTE: The wildcard * will not work on this page; you must type the full City or Zip," & NextLine & " or select the County or Region from the dropdown box." & NextLine & NextLine & "To COPY the result:" & NextLine & "Highlight text you wish to copy (or press ctl+a to select all) and press Ctl+c to copy.", MessageBoxButtons.OK, MessageBoxIcon.Information)
        'Select search criteria using the radio buttons and drop down boxes. " & NextLine & "If applicable, enter any text you wish to find." & NextLine & "3. Click the Search button, or press the Enter key." & NextLine & "Note: 
    End Sub

    'CHANGE SEARCH FIELD
    Private Sub RegionRadioButtons_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles rbRZip.Click, rbRRegion.Click, rbRCounty.Click, rbRCity.Click
        WhatRField = sender.text
        'TODO coordinate textbox entry and combobox choices
        'suggestion: use array of choices, if is not in array, user has entered so find in combobox
        Select Case WhatRField
            Case Is = "Region"
                Me.cboLst.Visible = True
                cboLst.DataSource = colRegion5lu
                '   Me.txtInput.Text = "Select Region from dropdown"
                Me.txtInput.Enabled = False
                Me.cboLst.Focus()
            Case Is = "County" 'load cbo with counties
                cboLst.DataSource = colCountylu
                Me.cboLst.Visible = True
                '   Me.txtInput.Text = "Select County from dropdown"
                Me.txtInput.Enabled = False
                Me.cboLst.Focus()
            Case Is = "Zip"
                Me.cboLst.Visible = False
                Me.txtInput.Enabled = True
                '   Me.txtInput.Text = "type zip code here"
            Case Is = "City"
                Me.cboLst.Visible = False
                Me.txtInput.Enabled = True
                '  Me.txtInput.Text = "type city here"
            Case Else
                Me.cboLst.Visible = False
                Me.txtInput.Enabled = True
                '  If Me.txtInput.Text.Contains("dropdown") Then
                ' Me.txtInput.Text = "Seelct from dropdown box"
                ' End If
                'if instr(txtinput.Text,"from list") > 0 the 'change default text
                '    txtInput.Text = "Type " & WhatRField & " here"
                '  End If
                '  If txtInput.Text = "Enter search text here" Then    'nothing entered
                Me.txtInput.Focus()
                Me.txtInput.SelectAll()
                'Else    'text already entered
                '    If txtInput.Text = "Enter search text here" Then    'nothing entered
                ' End If

                '' Me.txtInput.Text = "Enter search text here"
                'Me.txtInput.Focus()
                'Me.txtInput.SelectAll()
        End Select

    End Sub

#End Region 'get region of usr city/zip/county




    Private Sub cboLst_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles cboLst.SelectedIndexChanged
        '        CheckCBO(sender, "What to search")
        modGlobalVar.ValidateCBO(sender, "What to search", True, False)
    End Sub


    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles Button1.Click, miCountyMap.Click
        ' Create an instance of Word, and make it visible.
        Dim wrdApp As Word.Application
        Dim wrdDoc As Word.Document
        wrdApp = CreateObject("Word.Application")
        wrdApp.Visible = True

        '        ' Add a new document.

        wrdDoc = wrdApp.Documents.Add
        wrdDoc = wrdApp.Documents.Open("\\ICCNAS1\Users\Shared\Satellites\GeneralInfo\StateMap - Counties&Satellites.doc") 'Population Centers.doc")
        '        wrdDoc.Select()
        wrdDoc = Nothing
    End Sub

    'Open City Locator Map - Build URL from County and City
    Private Sub LinkLabel1_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        'CITY          'http://indiana.hometownlocator.com/IN/Hendricks/Avon.cfm
        'COUNTY          'http://indiana.hometownlocator.com/maps/CountyMap,CFIPS,003,c,Allen.cfm
        'ZIP          'http://showmyzip.com/index.php
        'http://indiana.hometownlocator.com/zip-codes/data,zipcode,46156.cfm


        Dim sbPath As New StringBuilder

        Select Case WhatRField
            Case "City"
                sbPath.Append("http://indiana.hometownlocator.com/IN/" + Trim(arCounty(0)) + "/" + Replace(Trim(arCity(0)), " ", "-") + ".cfm")
            Case "County"
                sbPath.Append("http://indiana.hometownlocator.com/maps/CountyMap,CFIPS," + arFIPS(0).substring(0, 3) + ",c," + Trim(arCounty(0)) + ".cfm")
            Case "Zip"
                sbPath.Append("http://indiana.hometownlocator.com/zip-codes/data,zipcode," + arZip(0).ToString + ".cfm")
            Case Else
                modGlobalVar.Msg("Insufficient Information", "Can only map city or county", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
        End Select
        Try
            System.Diagnostics.Process.Start(sbPath.ToString)
        Catch ex As Exception
            modGlobalVar.Msg("ERROR: Cannot open website", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

 
End Class
