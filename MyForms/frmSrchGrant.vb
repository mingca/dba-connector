Option Explicit On 
Imports System.data.SqlClient
Imports System.Windows.Forms.PaintEventArgs
Imports System.drawing
Imports System.Text
Imports System.Threading
Imports System.Windows.Forms.Control

Public Class frmSrchGrant
    Inherits System.Windows.Forms.Form

    Dim colGrant As New Microsoft.VisualBasic.Collection
    Dim colMGI As New Microsoft.VisualBasic.Collection
    Dim strCol As String 'selected ID column value
    Dim daM As New SqlDataAdapter
    Dim tbl As New DataTable

    Dim hti As System.Windows.Forms.DataGrid.HitTestInfo
    Dim dv, dvM, dvM2 As DataView   'filter for each datagrid
    Dim statusM, statusS1, statusS2 As String 'filter text for status bar
    Dim isLoaded As Boolean = False
    Dim strDGM, strDGM2, strHdr As String  'header text on datagrids
    Dim gridMouseDownTime As DateTime
    Dim strbActiveGrid As New StringBuilder

#Region "Initialize"
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

#End Region

#Region " Windows Form Designer generated code "

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents DataGridTextBoxColumn7 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn6 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents daspMGI As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents SqlCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents DataGridTextBoxColumn18 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents cboStaff As System.Windows.Forms.ComboBox
    Friend WithEvents miDefinitions As System.Windows.Forms.MenuItem
    Friend WithEvents miHelp As System.Windows.Forms.MenuItem
    Friend WithEvents cboRegion As System.Windows.Forms.ComboBox
    Friend WithEvents DataGridTextBoxColumn20 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn21 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn22 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Protected WithEvents Label7 As System.Windows.Forms.Label
    Protected WithEvents Label2 As System.Windows.Forms.Label
    Protected WithEvents lblRegion As System.Windows.Forms.Label
    Protected WithEvents lblMGI As System.Windows.Forms.Label
    Friend WithEvents cboStatus As System.Windows.Forms.ComboBox
    Protected WithEvents lblStaff As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents DataGridTextBoxColumn23 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Protected WithEvents cboCRG As System.Windows.Forms.ComboBox
    Friend WithEvents StatusBarPanelID As System.Windows.Forms.StatusBarPanel
    Friend WithEvents btnSendMail As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents StatusBar1 As System.Windows.Forms.StatusBar
    Friend WithEvents HelpProvider1 As System.Windows.Forms.HelpProvider
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents MainMenu1 As System.Windows.Forms.MainMenu
    Friend WithEvents MenuItem1 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem2 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem3 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem4 As System.Windows.Forms.MenuItem
    Friend WithEvents miClearSearch As System.Windows.Forms.MenuItem
    Friend WithEvents tstyleGrant As System.Windows.Forms.DataGridTableStyle
    Friend WithEvents tstyleMGI As System.Windows.Forms.DataGridTableStyle
    Friend WithEvents DataGridTextBoxColumn1 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn2 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn3 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn4 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn5 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents miCloseForm As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem6 As System.Windows.Forms.MenuItem
    Friend WithEvents grdMain As System.Windows.Forms.DataGrid
    Friend WithEvents miSrch As System.Windows.Forms.MenuItem
    Friend WithEvents txtCurrent As System.Windows.Forms.TextBox
    Friend WithEvents StatusBarPanel1 As System.Windows.Forms.StatusBarPanel
    Friend WithEvents StatusBarPanel2 As System.Windows.Forms.StatusBarPanel
    Friend WithEvents DataGridTextBoxColumn13 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn14 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn15 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents txtSelection As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents DataGridTextBoxColumn16 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents lstStatus As System.Windows.Forms.ListBox
    Friend WithEvents mmiHelp As System.Windows.Forms.MenuItem
    Friend WithEvents daspGrant As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents DsSrchGrant1 As InfoCtr.dsSrchGrant
    Friend WithEvents SqlSelectCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents miGotoGrant As System.Windows.Forms.MenuItem
    Friend WithEvents DataGridTextBoxColumn8 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn19 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn9 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn10 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn11 As New System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn12 As New System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn17 As New System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents btnHelp As System.Windows.Forms.Button
    Friend WithEvents cboGrant As System.Windows.Forms.ComboBox
    Friend WithEvents cboMGI As System.Windows.Forms.ComboBox

    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSrchGrant))
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.btnSendMail = New System.Windows.Forms.Button()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.cboCRG = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.lblStaff = New System.Windows.Forms.Label()
        Me.lblMGI = New System.Windows.Forms.Label()
        Me.cboStatus = New System.Windows.Forms.ComboBox()
        Me.lblRegion = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.cboRegion = New System.Windows.Forms.ComboBox()
        Me.cboStaff = New System.Windows.Forms.ComboBox()
        Me.cboMGI = New System.Windows.Forms.ComboBox()
        Me.cboGrant = New System.Windows.Forms.ComboBox()
        Me.lstStatus = New System.Windows.Forms.ListBox()
        Me.StatusBar1 = New System.Windows.Forms.StatusBar()
        Me.StatusBarPanel1 = New System.Windows.Forms.StatusBarPanel()
        Me.StatusBarPanelID = New System.Windows.Forms.StatusBarPanel()
        Me.StatusBarPanel2 = New System.Windows.Forms.StatusBarPanel()
        Me.HelpProvider1 = New System.Windows.Forms.HelpProvider()
        Me.grdMain = New System.Windows.Forms.DataGrid()
        Me.DsSrchGrant1 = New InfoCtr.dsSrchGrant()
        Me.tstyleGrant = New System.Windows.Forms.DataGridTableStyle()
        Me.DataGridTextBoxColumn1 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn16 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn2 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn3 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn4 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn5 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn14 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn23 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn13 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn18 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn20 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn21 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn22 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.tstyleMGI = New System.Windows.Forms.DataGridTableStyle()
        Me.DataGridTextBoxColumn8 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn7 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn19 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn9 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn6 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn10 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn11 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn12 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn17 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.MainMenu1 = New System.Windows.Forms.MainMenu(Me.components)
        Me.MenuItem1 = New System.Windows.Forms.MenuItem()
        Me.miCloseForm = New System.Windows.Forms.MenuItem()
        Me.MenuItem2 = New System.Windows.Forms.MenuItem()
        Me.miSrch = New System.Windows.Forms.MenuItem()
        Me.miClearSearch = New System.Windows.Forms.MenuItem()
        Me.MenuItem3 = New System.Windows.Forms.MenuItem()
        Me.miGotoGrant = New System.Windows.Forms.MenuItem()
        Me.MenuItem4 = New System.Windows.Forms.MenuItem()
        Me.MenuItem6 = New System.Windows.Forms.MenuItem()
        Me.mmiHelp = New System.Windows.Forms.MenuItem()
        Me.miHelp = New System.Windows.Forms.MenuItem()
        Me.miDefinitions = New System.Windows.Forms.MenuItem()
        Me.txtCurrent = New System.Windows.Forms.TextBox()
        Me.txtSelection = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.daspGrant = New System.Data.SqlClient.SqlDataAdapter()
        Me.SqlSelectCommand1 = New System.Data.SqlClient.SqlCommand()
        Me.btnHelp = New System.Windows.Forms.Button()
        Me.daspMGI = New System.Data.SqlClient.SqlDataAdapter()
        Me.SqlCommand1 = New System.Data.SqlClient.SqlCommand()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Panel2.SuspendLayout()
        CType(Me.StatusBarPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.StatusBarPanelID, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.StatusBarPanel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdMain, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsSrchGrant1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(12, 33)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(95, 17)
        Me.Label3.TabIndex = 191
        Me.Label3.Text = "Search Criteria"
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.SystemColors.InactiveBorder
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Controls.Add(Me.btnSendMail)
        Me.Panel2.Controls.Add(Me.Label8)
        Me.Panel2.Controls.Add(Me.cboCRG)
        Me.Panel2.Controls.Add(Me.Label5)
        Me.Panel2.Controls.Add(Me.lblStaff)
        Me.Panel2.Controls.Add(Me.lblMGI)
        Me.Panel2.Controls.Add(Me.cboStatus)
        Me.Panel2.Controls.Add(Me.lblRegion)
        Me.Panel2.Controls.Add(Me.Label2)
        Me.Panel2.Controls.Add(Me.Label7)
        Me.Panel2.Controls.Add(Me.btnSearch)
        Me.Panel2.Controls.Add(Me.cboRegion)
        Me.Panel2.Controls.Add(Me.cboStaff)
        Me.Panel2.Controls.Add(Me.cboMGI)
        Me.Panel2.Controls.Add(Me.cboGrant)
        Me.Panel2.Controls.Add(Me.lstStatus)
        Me.Panel2.Location = New System.Drawing.Point(7, 51)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(203, 512)
        Me.Panel2.TabIndex = 190
        '
        'btnSendMail
        '
        Me.btnSendMail.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.btnSendMail.Location = New System.Drawing.Point(49, 270)
        Me.btnSendMail.Name = "btnSendMail"
        Me.btnSendMail.Size = New System.Drawing.Size(69, 41)
        Me.btnSendMail.TabIndex = 421
        Me.btnSendMail.Text = "Send E-Mail"
        Me.ToolTip1.SetToolTip(Me.btnSendMail, "Send email to all grant applications not Denied.")
        Me.btnSendMail.UseVisualStyleBackColor = False
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.ForeColor = System.Drawing.Color.Maroon
        Me.Label8.Location = New System.Drawing.Point(17, 380)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(58, 13)
        Me.Label8.TabIndex = 231
        Me.Label8.Text = "CRG Issue"
        '
        'cboCRG
        '
        Me.cboCRG.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCRG.DropDownWidth = 500
        Me.cboCRG.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cboCRG.ItemHeight = 13
        Me.cboCRG.Items.AddRange(New Object() {"Case Manager", "Caller"})
        Me.cboCRG.Location = New System.Drawing.Point(14, 397)
        Me.cboCRG.Name = "cboCRG"
        Me.cboCRG.Size = New System.Drawing.Size(173, 21)
        Me.cboCRG.TabIndex = 230
        Me.ToolTip1.SetToolTip(Me.cboCRG, "Right click to search.  Escape twice to select none.")
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.ForeColor = System.Drawing.Color.Maroon
        Me.Label5.Location = New System.Drawing.Point(17, 439)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(176, 65)
        Me.Label5.TabIndex = 229
        Me.Label5.Text = "Community Ministry Grant = CMG" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Computer Grant = CMGI" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Technology Grant 2010 = TG" & _
    "I" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Technology && Ministry 2012 = TMGI" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Youth Ministry Grant = YMGI"
        '
        'lblStaff
        '
        Me.lblStaff.BackColor = System.Drawing.SystemColors.InactiveBorder
        Me.lblStaff.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStaff.ForeColor = System.Drawing.Color.Maroon
        Me.lblStaff.Location = New System.Drawing.Point(4, 165)
        Me.lblStaff.Name = "lblStaff"
        Me.lblStaff.Size = New System.Drawing.Size(165, 17)
        Me.lblStaff.TabIndex = 228
        Me.lblStaff.Text = "Grant Staff"
        Me.lblStaff.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblMGI
        '
        Me.lblMGI.BackColor = System.Drawing.SystemColors.InactiveBorder
        Me.lblMGI.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMGI.ForeColor = System.Drawing.Color.Maroon
        Me.lblMGI.Location = New System.Drawing.Point(4, 116)
        Me.lblMGI.Name = "lblMGI"
        Me.lblMGI.Size = New System.Drawing.Size(114, 15)
        Me.lblMGI.TabIndex = 227
        Me.lblMGI.Text = "Major G I Application"
        Me.lblMGI.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cboStatus
        '
        Me.cboStatus.FormattingEnabled = True
        Me.cboStatus.Location = New System.Drawing.Point(7, 228)
        Me.cboStatus.Name = "cboStatus"
        Me.cboStatus.Size = New System.Drawing.Size(180, 21)
        Me.cboStatus.TabIndex = 226
        '
        'lblRegion
        '
        Me.lblRegion.BackColor = System.Drawing.SystemColors.InactiveBorder
        Me.lblRegion.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRegion.ForeColor = System.Drawing.Color.Maroon
        Me.lblRegion.Location = New System.Drawing.Point(4, 52)
        Me.lblRegion.Name = "lblRegion"
        Me.lblRegion.Size = New System.Drawing.Size(141, 13)
        Me.lblRegion.TabIndex = 225
        Me.lblRegion.Text = "Satellite Region "
        Me.lblRegion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.SystemColors.InactiveBorder
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Maroon
        Me.Label2.Location = New System.Drawing.Point(4, 4)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(80, 17)
        Me.Label2.TabIndex = 224
        Me.Label2.Text = "Type of Grant"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.SystemColors.InactiveBorder
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.Maroon
        Me.Label7.Location = New System.Drawing.Point(4, 210)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(80, 17)
        Me.Label7.TabIndex = 223
        Me.Label7.Text = "Grant Status"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnSearch
        '
        Me.btnSearch.BackColor = System.Drawing.SystemColors.Control
        Me.btnSearch.Location = New System.Drawing.Point(115, 93)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(75, 25)
        Me.btnSearch.TabIndex = 214
        Me.btnSearch.Text = "Search"
        Me.btnSearch.UseVisualStyleBackColor = False
        '
        'cboRegion
        '
        Me.cboRegion.FormattingEnabled = True
        Me.cboRegion.Location = New System.Drawing.Point(7, 66)
        Me.cboRegion.Name = "cboRegion"
        Me.cboRegion.Size = New System.Drawing.Size(183, 21)
        Me.cboRegion.TabIndex = 213
        Me.cboRegion.Tag = "Satellite Region"
        Me.cboRegion.Text = "Region"
        Me.ToolTip1.SetToolTip(Me.cboRegion, "Satellite Region")
        '
        'cboStaff
        '
        Me.cboStaff.FormattingEnabled = True
        Me.cboStaff.Items.AddRange(New Object() {"202", "204", "213"})
        Me.cboStaff.Location = New System.Drawing.Point(6, 183)
        Me.cboStaff.Name = "cboStaff"
        Me.cboStaff.Size = New System.Drawing.Size(183, 21)
        Me.cboStaff.TabIndex = 212
        Me.cboStaff.Tag = "Grant Staff"
        Me.cboStaff.Text = "Grant Staff"
        Me.ToolTip1.SetToolTip(Me.cboStaff, "Grant Staff")
        Me.cboStaff.Visible = False
        '
        'cboMGI
        '
        Me.cboMGI.DropDownWidth = 250
        Me.cboMGI.Items.AddRange(New Object() {"CMG  Community Ministry 2016 Applications", "YMGI  Youth Ministry 2015 Applications", "TMGI  Technology & Ministry 2012 Applications ", "TGI   Technology 2010 Applications", "LTGI  Life Together Applications", "", "----- Details Archived --------", "YMG1 Youth Ministry 2013 Applications", "SSGI  Sacred Space Applications ", "CMGI  Computer Grant Applications"})
        Me.cboMGI.Location = New System.Drawing.Point(6, 134)
        Me.cboMGI.Name = "cboMGI"
        Me.cboMGI.Size = New System.Drawing.Size(183, 21)
        Me.cboMGI.TabIndex = 211
        Me.cboMGI.Tag = "Major Grant Initiative"
        Me.cboMGI.Text = "MGI"
        Me.ToolTip1.SetToolTip(Me.cboMGI, "Major Grant Initiatives")
        Me.cboMGI.Visible = False
        '
        'cboGrant
        '
        Me.cboGrant.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cboGrant.Location = New System.Drawing.Point(6, 22)
        Me.cboGrant.Name = "cboGrant"
        Me.cboGrant.Size = New System.Drawing.Size(183, 21)
        Me.cboGrant.TabIndex = 210
        Me.cboGrant.Tag = "Type of Grant"
        Me.ToolTip1.SetToolTip(Me.cboGrant, "Type of Grant")
        '
        'lstStatus
        '
        Me.lstStatus.Location = New System.Drawing.Point(7, 355)
        Me.lstStatus.Name = "lstStatus"
        Me.lstStatus.Size = New System.Drawing.Size(182, 17)
        Me.lstStatus.TabIndex = 188
        Me.ToolTip1.SetToolTip(Me.lstStatus, "Grant Status")
        Me.lstStatus.Visible = False
        '
        'StatusBar1
        '
        Me.StatusBar1.Location = New System.Drawing.Point(0, 610)
        Me.StatusBar1.Name = "StatusBar1"
        Me.StatusBar1.Panels.AddRange(New System.Windows.Forms.StatusBarPanel() {Me.StatusBarPanel1, Me.StatusBarPanelID, Me.StatusBarPanel2})
        Me.StatusBar1.ShowPanels = True
        Me.StatusBar1.Size = New System.Drawing.Size(1155, 22)
        Me.StatusBar1.TabIndex = 192
        '
        'StatusBarPanel1
        '
        Me.StatusBarPanel1.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents
        Me.StatusBarPanel1.MinWidth = 200
        Me.StatusBarPanel1.Name = "StatusBarPanel1"
        Me.StatusBarPanel1.Width = 200
        '
        'StatusBarPanelID
        '
        Me.StatusBarPanelID.Alignment = System.Windows.Forms.HorizontalAlignment.Center
        Me.StatusBarPanelID.MinWidth = 200
        Me.StatusBarPanelID.Name = "StatusBarPanelID"
        Me.StatusBarPanelID.Text = "Grant ID: "
        Me.StatusBarPanelID.Width = 200
        '
        'StatusBarPanel2
        '
        Me.StatusBarPanel2.Alignment = System.Windows.Forms.HorizontalAlignment.Right
        Me.StatusBarPanel2.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring
        Me.StatusBarPanel2.Name = "StatusBarPanel2"
        Me.StatusBarPanel2.Text = "Use this window to find Grants."
        Me.StatusBarPanel2.Width = 738
        '
        'grdMain
        '
        Me.grdMain.AlternatingBackColor = System.Drawing.Color.FromArgb(CType(CType(204, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.grdMain.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdMain.BackgroundColor = System.Drawing.SystemColors.Control
        Me.grdMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.grdMain.CaptionBackColor = System.Drawing.SystemColors.InactiveCaption
        Me.grdMain.DataMember = "SrchGrant"
        Me.grdMain.DataSource = Me.DsSrchGrant1
        Me.grdMain.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.HelpProvider1.SetHelpString(Me.grdMain, "hello")
        Me.grdMain.Location = New System.Drawing.Point(217, 51)
        Me.grdMain.Name = "grdMain"
        Me.grdMain.ParentRowsBackColor = System.Drawing.SystemColors.Window
        Me.grdMain.ParentRowsVisible = False
        Me.grdMain.ReadOnly = True
        Me.grdMain.RowHeaderWidth = 15
        Me.grdMain.SelectionBackColor = System.Drawing.SystemColors.Desktop
        Me.grdMain.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        Me.HelpProvider1.SetShowHelp(Me.grdMain, True)
        Me.grdMain.Size = New System.Drawing.Size(926, 491)
        Me.grdMain.TabIndex = 194
        Me.grdMain.TableStyles.AddRange(New System.Windows.Forms.DataGridTableStyle() {Me.tstyleGrant, Me.tstyleMGI})
        Me.grdMain.Tag = "Double-click for detail.  Right-click to filter; right-click border to clear filt" & _
    "er."
        Me.ToolTip1.SetToolTip(Me.grdMain, "Click in grid, then press F1 for grid help")
        '
        'DsSrchGrant1
        '
        Me.DsSrchGrant1.DataSetName = "dsSrchGrant"
        Me.DsSrchGrant1.Locale = New System.Globalization.CultureInfo("en-US")
        Me.DsSrchGrant1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'tstyleGrant
        '
        Me.tstyleGrant.AlternatingBackColor = System.Drawing.Color.FromArgb(CType(CType(204, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.tstyleGrant.DataGrid = Me.grdMain
        Me.tstyleGrant.ForeColor = System.Drawing.Color.DarkGreen
        Me.tstyleGrant.GridColumnStyles.AddRange(New System.Windows.Forms.DataGridColumnStyle() {Me.DataGridTextBoxColumn1, Me.DataGridTextBoxColumn16, Me.DataGridTextBoxColumn2, Me.DataGridTextBoxColumn3, Me.DataGridTextBoxColumn4, Me.DataGridTextBoxColumn5, Me.DataGridTextBoxColumn14, Me.DataGridTextBoxColumn23, Me.DataGridTextBoxColumn13, Me.DataGridTextBoxColumn18, Me.DataGridTextBoxColumn20, Me.DataGridTextBoxColumn21, Me.DataGridTextBoxColumn22})
        Me.tstyleGrant.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tstyleGrant.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.tstyleGrant.MappingName = "SrchGrant"
        Me.tstyleGrant.RowHeaderWidth = 15
        Me.tstyleGrant.SelectionBackColor = System.Drawing.SystemColors.Highlight
        '
        'DataGridTextBoxColumn1
        '
        Me.DataGridTextBoxColumn1.Format = ""
        Me.DataGridTextBoxColumn1.FormatInfo = Nothing
        Me.DataGridTextBoxColumn1.HeaderText = "GrantID"
        Me.DataGridTextBoxColumn1.MappingName = "GrantIDTxt"
        Me.DataGridTextBoxColumn1.Width = 0
        '
        'DataGridTextBoxColumn16
        '
        Me.DataGridTextBoxColumn16.Format = ""
        Me.DataGridTextBoxColumn16.FormatInfo = Nothing
        Me.DataGridTextBoxColumn16.HeaderText = "OrgNum"
        Me.DataGridTextBoxColumn16.MappingName = "OrgNum"
        Me.DataGridTextBoxColumn16.Width = 0
        '
        'DataGridTextBoxColumn2
        '
        Me.DataGridTextBoxColumn2.Format = ""
        Me.DataGridTextBoxColumn2.FormatInfo = Nothing
        Me.DataGridTextBoxColumn2.HeaderText = "Organization"
        Me.DataGridTextBoxColumn2.MappingName = "OrgCity"
        Me.DataGridTextBoxColumn2.Width = 170
        '
        'DataGridTextBoxColumn3
        '
        Me.DataGridTextBoxColumn3.Format = ""
        Me.DataGridTextBoxColumn3.FormatInfo = Nothing
        Me.DataGridTextBoxColumn3.HeaderText = "CaseName"
        Me.DataGridTextBoxColumn3.MappingName = "CaseName"
        Me.DataGridTextBoxColumn3.Width = 120
        '
        'DataGridTextBoxColumn4
        '
        Me.DataGridTextBoxColumn4.Alignment = System.Windows.Forms.HorizontalAlignment.Right
        Me.DataGridTextBoxColumn4.Format = "d"
        Me.DataGridTextBoxColumn4.FormatInfo = Nothing
        Me.DataGridTextBoxColumn4.HeaderText = "Determination Dt."
        Me.DataGridTextBoxColumn4.MappingName = "DeterminationDate"
        Me.DataGridTextBoxColumn4.Width = 75
        '
        'DataGridTextBoxColumn5
        '
        Me.DataGridTextBoxColumn5.Alignment = System.Windows.Forms.HorizontalAlignment.Right
        Me.DataGridTextBoxColumn5.Format = "$##,###,###"
        Me.DataGridTextBoxColumn5.FormatInfo = Nothing
        Me.DataGridTextBoxColumn5.HeaderText = "Amount       ."
        Me.DataGridTextBoxColumn5.MappingName = "Amount"
        Me.DataGridTextBoxColumn5.NullText = ""
        Me.DataGridTextBoxColumn5.Width = 65
        '
        'DataGridTextBoxColumn14
        '
        Me.DataGridTextBoxColumn14.Format = ""
        Me.DataGridTextBoxColumn14.FormatInfo = Nothing
        Me.DataGridTextBoxColumn14.HeaderText = "Case Mgr"
        Me.DataGridTextBoxColumn14.MappingName = "CaseMgr"
        Me.DataGridTextBoxColumn14.Width = 75
        '
        'DataGridTextBoxColumn23
        '
        Me.DataGridTextBoxColumn23.Format = ""
        Me.DataGridTextBoxColumn23.FormatInfo = Nothing
        Me.DataGridTextBoxColumn23.HeaderText = "Next Rpt Due"
        Me.DataGridTextBoxColumn23.MappingName = "NextReportDue"
        Me.DataGridTextBoxColumn23.Width = 125
        '
        'DataGridTextBoxColumn13
        '
        Me.DataGridTextBoxColumn13.Format = ""
        Me.DataGridTextBoxColumn13.FormatInfo = Nothing
        Me.DataGridTextBoxColumn13.HeaderText = "Congr Final Report"
        Me.DataGridTextBoxColumn13.MappingName = "CongregationFinalReport"
        Me.DataGridTextBoxColumn13.Width = 115
        '
        'DataGridTextBoxColumn18
        '
        Me.DataGridTextBoxColumn18.Format = ""
        Me.DataGridTextBoxColumn18.FormatInfo = Nothing
        Me.DataGridTextBoxColumn18.HeaderText = "OrgandPhone"
        Me.DataGridTextBoxColumn18.MappingName = "OrgandPhone"
        Me.DataGridTextBoxColumn18.Width = 0
        '
        'DataGridTextBoxColumn20
        '
        Me.DataGridTextBoxColumn20.Format = ""
        Me.DataGridTextBoxColumn20.FormatInfo = Nothing
        Me.DataGridTextBoxColumn20.HeaderText = "Type"
        Me.DataGridTextBoxColumn20.MappingName = "TypeofGrant"
        Me.DataGridTextBoxColumn20.Width = 60
        '
        'DataGridTextBoxColumn21
        '
        Me.DataGridTextBoxColumn21.Format = ""
        Me.DataGridTextBoxColumn21.FormatInfo = Nothing
        Me.DataGridTextBoxColumn21.HeaderText = "Funded"
        Me.DataGridTextBoxColumn21.MappingName = "NumFunded"
        Me.DataGridTextBoxColumn21.Width = 45
        '
        'DataGridTextBoxColumn22
        '
        Me.DataGridTextBoxColumn22.Format = ""
        Me.DataGridTextBoxColumn22.FormatInfo = Nothing
        Me.DataGridTextBoxColumn22.HeaderText = "Feedback"
        Me.DataGridTextBoxColumn22.MappingName = "NumFeedback"
        Me.DataGridTextBoxColumn22.Width = 50
        '
        'tstyleMGI
        '
        Me.tstyleMGI.AlternatingBackColor = System.Drawing.Color.FromArgb(CType(CType(204, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.tstyleMGI.DataGrid = Me.grdMain
        Me.tstyleMGI.ForeColor = System.Drawing.Color.DarkGreen
        Me.tstyleMGI.GridColumnStyles.AddRange(New System.Windows.Forms.DataGridColumnStyle() {Me.DataGridTextBoxColumn8, Me.DataGridTextBoxColumn7, Me.DataGridTextBoxColumn19, Me.DataGridTextBoxColumn9, Me.DataGridTextBoxColumn6, Me.DataGridTextBoxColumn10, Me.DataGridTextBoxColumn11, Me.DataGridTextBoxColumn12, Me.DataGridTextBoxColumn18, Me.DataGridTextBoxColumn17})
        Me.tstyleMGI.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.tstyleMGI.MappingName = "SrchMGI"
        Me.tstyleMGI.RowHeaderWidth = 15
        '
        'DataGridTextBoxColumn8
        '
        Me.DataGridTextBoxColumn8.Format = ""
        Me.DataGridTextBoxColumn8.FormatInfo = Nothing
        Me.DataGridTextBoxColumn8.HeaderText = "#"
        Me.DataGridTextBoxColumn8.MappingName = "GIID"
        Me.DataGridTextBoxColumn8.ReadOnly = True
        Me.DataGridTextBoxColumn8.Width = 25
        '
        'DataGridTextBoxColumn7
        '
        Me.DataGridTextBoxColumn7.Format = ""
        Me.DataGridTextBoxColumn7.FormatInfo = Nothing
        Me.DataGridTextBoxColumn7.HeaderText = "CaseNum"
        Me.DataGridTextBoxColumn7.MappingName = "CaseNum"
        Me.DataGridTextBoxColumn7.Width = 0
        '
        'DataGridTextBoxColumn19
        '
        Me.DataGridTextBoxColumn19.Format = ""
        Me.DataGridTextBoxColumn19.FormatInfo = Nothing
        Me.DataGridTextBoxColumn19.HeaderText = "OrgNum"
        Me.DataGridTextBoxColumn19.MappingName = "OrgNum"
        Me.DataGridTextBoxColumn19.Width = 0
        '
        'DataGridTextBoxColumn9
        '
        Me.DataGridTextBoxColumn9.Format = ""
        Me.DataGridTextBoxColumn9.FormatInfo = Nothing
        Me.DataGridTextBoxColumn9.HeaderText = "Organization"
        Me.DataGridTextBoxColumn9.MappingName = "OrgCity"
        Me.DataGridTextBoxColumn9.Width = 200
        '
        'DataGridTextBoxColumn6
        '
        Me.DataGridTextBoxColumn6.Format = ""
        Me.DataGridTextBoxColumn6.FormatInfo = Nothing
        Me.DataGridTextBoxColumn6.HeaderText = "CaseName"
        Me.DataGridTextBoxColumn6.MappingName = "CaseName"
        Me.DataGridTextBoxColumn6.Width = 150
        '
        'DataGridTextBoxColumn10
        '
        Me.DataGridTextBoxColumn10.Format = ""
        Me.DataGridTextBoxColumn10.FormatInfo = Nothing
        Me.DataGridTextBoxColumn10.HeaderText = "Determination"
        Me.DataGridTextBoxColumn10.MappingName = "Determination"
        Me.DataGridTextBoxColumn10.Width = 75
        '
        'DataGridTextBoxColumn11
        '
        Me.DataGridTextBoxColumn11.Format = ""
        Me.DataGridTextBoxColumn11.FormatInfo = Nothing
        Me.DataGridTextBoxColumn11.HeaderText = "Class Group"
        Me.DataGridTextBoxColumn11.MappingName = "ClassGroup"
        Me.DataGridTextBoxColumn11.Width = 75
        '
        'DataGridTextBoxColumn12
        '
        Me.DataGridTextBoxColumn12.Format = "d"
        Me.DataGridTextBoxColumn12.FormatInfo = Nothing
        Me.DataGridTextBoxColumn12.HeaderText = "Date Rec'd"
        Me.DataGridTextBoxColumn12.MappingName = "AppRecdDt"
        Me.DataGridTextBoxColumn12.Width = 75
        '
        'DataGridTextBoxColumn17
        '
        Me.DataGridTextBoxColumn17.Format = ""
        Me.DataGridTextBoxColumn17.FormatInfo = Nothing
        Me.DataGridTextBoxColumn17.HeaderText = "Case Mgr"
        Me.DataGridTextBoxColumn17.MappingName = "CaseMgr"
        Me.DataGridTextBoxColumn17.Width = 120
        '
        'MainMenu1
        '
        Me.MainMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem1, Me.MenuItem2, Me.MenuItem3, Me.MenuItem4, Me.mmiHelp})
        '
        'MenuItem1
        '
        Me.MenuItem1.Index = 0
        Me.MenuItem1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.miCloseForm})
        Me.MenuItem1.Text = "File"
        '
        'miCloseForm
        '
        Me.miCloseForm.Index = 0
        Me.miCloseForm.Text = "Close Window"
        '
        'MenuItem2
        '
        Me.MenuItem2.Index = 1
        Me.MenuItem2.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.miSrch, Me.miClearSearch})
        Me.MenuItem2.Text = "Search"
        '
        'miSrch
        '
        Me.miSrch.Index = 0
        Me.miSrch.Text = "Begin Search"
        '
        'miClearSearch
        '
        Me.miClearSearch.Index = 1
        Me.miClearSearch.Text = "Clear Criteria"
        '
        'MenuItem3
        '
        Me.MenuItem3.Enabled = False
        Me.MenuItem3.Index = 2
        Me.MenuItem3.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.miGotoGrant})
        Me.MenuItem3.Text = "Go to"
        Me.MenuItem3.Visible = False
        '
        'miGotoGrant
        '
        Me.miGotoGrant.Index = 0
        Me.miGotoGrant.Text = "Grant"
        '
        'MenuItem4
        '
        Me.MenuItem4.Enabled = False
        Me.MenuItem4.Index = 3
        Me.MenuItem4.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem6})
        Me.MenuItem4.Text = "Reports"
        '
        'MenuItem6
        '
        Me.MenuItem6.Enabled = False
        Me.MenuItem6.Index = 0
        Me.MenuItem6.Text = "Print This List"
        '
        'mmiHelp
        '
        Me.mmiHelp.Index = 4
        Me.mmiHelp.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.miHelp, Me.miDefinitions})
        Me.mmiHelp.Text = "Help"
        '
        'miHelp
        '
        Me.miHelp.Index = 0
        Me.miHelp.Text = "Help"
        '
        'miDefinitions
        '
        Me.miDefinitions.Index = 1
        Me.miDefinitions.Text = "Search term Definitions"
        '
        'txtCurrent
        '
        Me.txtCurrent.BackColor = System.Drawing.SystemColors.Control
        Me.txtCurrent.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtCurrent.Enabled = False
        Me.txtCurrent.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCurrent.ForeColor = System.Drawing.SystemColors.GrayText
        Me.txtCurrent.Location = New System.Drawing.Point(949, 8)
        Me.txtCurrent.Name = "txtCurrent"
        Me.txtCurrent.Size = New System.Drawing.Size(56, 14)
        Me.txtCurrent.TabIndex = 199
        '
        'txtSelection
        '
        Me.txtSelection.BackColor = System.Drawing.SystemColors.Control
        Me.txtSelection.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtSelection.Enabled = False
        Me.txtSelection.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSelection.ForeColor = System.Drawing.SystemColors.GrayText
        Me.txtSelection.Location = New System.Drawing.Point(878, 29)
        Me.txtSelection.Name = "txtSelection"
        Me.txtSelection.Size = New System.Drawing.Size(49, 14)
        Me.txtSelection.TabIndex = 226
        Me.txtSelection.TabStop = False
        Me.txtSelection.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlDark
        Me.Label4.Location = New System.Drawing.Point(892, 5)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(35, 14)
        Me.Label4.TabIndex = 227
        Me.Label4.Text = "Grant #"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'daspGrant
        '
        Me.daspGrant.SelectCommand = Me.SqlSelectCommand1
        Me.daspGrant.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "SrchGrants", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("GrantIDTxt", "GrantIDTxt"), New System.Data.Common.DataColumnMapping("OrgNum", "OrgNum"), New System.Data.Common.DataColumnMapping("CaseName", "CaseName"), New System.Data.Common.DataColumnMapping("DeterminationDate", "DeterminationDate"), New System.Data.Common.DataColumnMapping("Amount", "Amount"), New System.Data.Common.DataColumnMapping("CaseMgr", "CaseMgr"), New System.Data.Common.DataColumnMapping("CongregationFinalReport", "CongregationFinalReport"), New System.Data.Common.DataColumnMapping("OrgCity", "OrgCity"), New System.Data.Common.DataColumnMapping("TypeofGrant", "TypeofGrant"), New System.Data.Common.DataColumnMapping("NumFunded", "NumFunded"), New System.Data.Common.DataColumnMapping("NumFeedback", "NumFeedback"), New System.Data.Common.DataColumnMapping("NextReportDue", "NextReportDue")})})
        '
        'SqlSelectCommand1
        '
        Me.SqlSelectCommand1.CommandText = "dbo.SrchGrants"
        Me.SqlSelectCommand1.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlSelectCommand1.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, False, CType(0, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing), New System.Data.SqlClient.SqlParameter("@Type", System.Data.SqlDbType.VarChar, 50), New System.Data.SqlClient.SqlParameter("@begin", System.Data.SqlDbType.DateTime, 8), New System.Data.SqlClient.SqlParameter("@Today", System.Data.SqlDbType.DateTime, 8), New System.Data.SqlClient.SqlParameter("@Whn", System.Data.SqlDbType.VarChar, 30), New System.Data.SqlClient.SqlParameter("@approve", System.Data.SqlDbType.VarChar, 15), New System.Data.SqlClient.SqlParameter("@Who", System.Data.SqlDbType.Int, 4), New System.Data.SqlClient.SqlParameter("@empty", System.Data.SqlDbType.VarChar, 1), New System.Data.SqlClient.SqlParameter("@Region", System.Data.SqlDbType.VarChar, 20), New System.Data.SqlClient.SqlParameter("@CRG", System.Data.SqlDbType.VarChar, 100)})
        '
        'btnHelp
        '
        Me.btnHelp.BackColor = System.Drawing.SystemColors.Control
        Me.btnHelp.Font = New System.Drawing.Font("Microsoft Sans Serif", 22.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnHelp.ForeColor = System.Drawing.SystemColors.Control
        Me.btnHelp.Image = CType(resources.GetObject("btnHelp.Image"), System.Drawing.Image)
        Me.btnHelp.ImageAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnHelp.Location = New System.Drawing.Point(851, -3)
        Me.btnHelp.Name = "btnHelp"
        Me.btnHelp.Size = New System.Drawing.Size(25, 25)
        Me.btnHelp.TabIndex = 228
        Me.btnHelp.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnHelp.UseVisualStyleBackColor = False
        '
        'daspMGI
        '
        Me.daspMGI.SelectCommand = Me.SqlCommand1
        Me.daspMGI.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "SrchMGI", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("GIID", "GIID"), New System.Data.Common.DataColumnMapping("CaseNum", "CaseNum"), New System.Data.Common.DataColumnMapping("OrgNum", "OrgNum"), New System.Data.Common.DataColumnMapping("CaseName", "CaseName"), New System.Data.Common.DataColumnMapping("ClassGroup", "ClassGroup"), New System.Data.Common.DataColumnMapping("Determination", "Determination"), New System.Data.Common.DataColumnMapping("CaseMgr", "CaseMgr"), New System.Data.Common.DataColumnMapping("AppRecdDt", "AppRecdDt"), New System.Data.Common.DataColumnMapping("OrderReceived", "OrderReceived"), New System.Data.Common.DataColumnMapping("OrgCity", "OrgCity")})})
        '
        'SqlCommand1
        '
        Me.SqlCommand1.CommandText = "dbo.SrchMGITest"
        Me.SqlCommand1.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlCommand1.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, False, CType(0, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing), New System.Data.SqlClient.SqlParameter("@WhichGrant", System.Data.SqlDbType.VarChar, 50), New System.Data.SqlClient.SqlParameter("@Determination", System.Data.SqlDbType.VarChar, 30), New System.Data.SqlClient.SqlParameter("@Region", System.Data.SqlDbType.VarChar, 15)})
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(224, 29)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(95, 17)
        Me.Label1.TabIndex = 229
        Me.Label1.Text = "Results"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Maroon
        Me.Label6.Location = New System.Drawing.Point(242, 8)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(559, 16)
        Me.Label6.TabIndex = 230
        Me.Label6.Text = "Hint: start new Application for Major Grant Initiative from the organization's Ca" & _
    "se Detail window."
        '
        'frmSrchGrant
        '
        Me.AcceptButton = Me.btnSearch
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(1155, 632)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnHelp)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txtSelection)
        Me.Controls.Add(Me.txtCurrent)
        Me.Controls.Add(Me.grdMain)
        Me.Controls.Add(Me.StatusBar1)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Panel2)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Menu = Me.MainMenu1
        Me.Name = "frmSrchGrant"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "FIND GRANT"
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.StatusBarPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.StatusBarPanelID, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.StatusBarPanel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdMain, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsSrchGrant1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

#Region "Load"
    'LOAD
    Private Sub frmSrchGrant_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles MyBase.Load
        Dim cmd As New SqlCommand

SetCaptionStrings:
        strDGM = "GRANTS"
        strDGM2 = "MGI APPLICATIONS"

DataAdapters:
        Me.daspGrant.SelectCommand.Connection = sc
        Me.daspMGI.SelectCommand.Connection = sc

SetDefaultComboboxes:
        LoadList()
        modGlobalVar.LoadCRGCombo(Me.cboCRG)
        Me.cboCRG.SelectedIndex = -1
        Me.cboStatus.DataSource = Me.colGrant
        modGlobalVar.LoadStaffCombo(Me.cboStaff, False, StaffComboChoices.AllAndNo)
        modGlobalVar.LoadRegionCombo(Me.cboRegion, "Indiana")
        Me.cboRegion.SelectedIndex = cboRegion.FindString(usrRegion)
        Me.cboStatus.SelectedIndex = 1
        Me.cboGrant.SelectedIndex = Me.cboGrant.FindStringExact("My Grants") 'Resource Grant")
        Me.cboMGI.SelectedIndex = 0

        dvM = New DataView(Me.DsSrchGrant1.SrchGrant)
        dvM2 = New DataView(Me.DsSrchGrant1.SrchMGI)
        Me.grdMain.DataSource = dvM
        strbActiveGrid.Append("grdMain")    'use this for doubleclick code

FormSetup:
        Dim tbx As DataGridTextBoxColumn
        Dim tbs As DataGridTableStyle

        For Each tbs In Me.grdMain.TableStyles
            For Each tbx In tbs.GridColumnStyles
                tbx.TextBox.Enabled = False
                tbx.NullText = ""
            Next
        Next

        Me.HelpProvider1.SetHelpString(Me.grdMain, strHelpGrid)

        Forms.Add(Me)
        Me.StatusBarPanel1.Text = "Done"
        Me.miSrch.PerformClick()
        isLoaded = True
    End Sub

    'CLOSE
    Protected Sub miCloseForm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles miCloseForm.Click
        Me.Close()
    End Sub

#End Region 'load

    'TODO  Add org name for opening form
#Region "Search"

    'DO SEARCH
    Private Sub doSearch(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles miSrch.Click, btnSearch.Click
        Dim iTbl As Integer 'table number in dataset
        Dim strtbl As String
        Dim da As New SqlDataAdapter

        '0 VALIDATE USER ENTRIES
        If isLoaded Then
            If modGlobalVar.NewValidateCombo(Me.cboStatus, True) Then
            Else
                Exit Sub
            End If
            If Me.cboStaff.Visible = True Then
                If modGlobalVar.NewValidateStaff(cboStaff, True) Then ', conStaffHeading.Selectable) Then
                Else
                    Exit Sub
                End If
            End If
            If modGlobalVar.NewValidateCombo(Me.cboGrant, True) Then
            Else
                Exit Sub
            End If
            If modGlobalVar.NewValidateCombo(Me.cboRegion, True) Then
            Else
                Exit Sub
            End If
            If Me.cboMGI.Visible = True Then
                If modGlobalVar.NewValidateCombo(Me.cboMGI, True) Then
                Else
                    Exit Sub
                End If
            End If

        End If

        Me.btnSearch.BackColor = Color.FromKnownColor(KnownColor.InactiveCaption)

        '....................................
        '1 SET STATUS BAR & CURSOR HOURGLASS
        MouseWait()
        Me.StatusBar1.Panels(0).Text = "Searching..."

        '2 REFERENCE CONNECTION FROM STARTUP FORM
        Me.daspGrant.SelectCommand.Connection = sc
        Me.daspMGI.SelectCommand.Connection = sc

        '3 CLEAR EXISTING DATA, SET DEFAULT PARAMETERS
        Me.DsSrchGrant1.Clear()
        Me.grdMain.Refresh()
        Me.grdMain.CaptionText = ""
        Me.txtCurrent.Text = ""
        Me.StatusBarPanelID.Text = "Grant ID:"

        '4 ASSIGN SP PARAMETERS; DETERMINE IF GRANT OR MGI BEING SEARCHED
        'NOTE leave in this order so GoTos work

        If Me.cboGrant.SelectedItem = "Major Grant Initiative Applications" Then 'MGI APPS
            iTbl = 1
            strtbl = "SrchMGI"
            tbl = Me.DsSrchGrant1.SrchMGI
            Me.daspMGI.SelectCommand.Parameters("@WhichGrant").Value = "tblGrant" & RTrim(Microsoft.VisualBasic.Left(Me.cboMGI.SelectedItem, 4))
            Me.daspMGI.SelectCommand.Parameters("@Determination").Value = Me.cboStatus.SelectedItem
            If Me.cboRegion.SelectedValue = "All Regions" Then
                Me.daspMGI.SelectCommand.Parameters("@Region").Value = "%"
            Else
                Me.daspMGI.SelectCommand.Parameters("@Region").Value = Me.cboRegion.Text ''SelectedValue
            End If
            Me.grdMain.DataSource = dvM2
            strHdr = strDGM2
            da = Me.daspMGI
            GoTo FindMGI
        Else 'REAL GRANTS
            iTbl = 0
            strtbl = "SrchGrant"
            tbl = Me.DsSrchGrant1.SrchGrant
            Me.grdMain.DataSource = dvM
            strHdr = strDGM
        End If
        If Me.cboGrant.SelectedItem = "All Grants" Then
            Me.daspGrant.SelectCommand.Parameters("@Type").Value = "%"
        Else
            Me.daspGrant.SelectCommand.Parameters("@Type").Value = Me.cboGrant.SelectedItem
        End If
        If Me.cboGrant.SelectedItem = "My Grants" Then
            Me.daspGrant.SelectCommand.Parameters("@Type").Value = "%"
            Me.daspGrant.SelectCommand.Parameters("@Who").Value = cboStaff.SelectedValue
        Else
            Me.daspGrant.SelectCommand.Parameters("@Who").Value = 0
        End If
        Me.daspGrant.SelectCommand.Parameters("@Today").Value = Today 'DateAdd(DateInterval.Day, 30, Today)
        Me.daspGrant.SelectCommand.Parameters("@Whn").Value = Me.cboStatus.SelectedItem 'Me.lstStatus.SelectedItem
        If Me.cboRegion.SelectedValue = "All Regions" Then
            Me.daspGrant.SelectCommand.Parameters("@Region").Value = "%"
        Else
            Me.daspGrant.SelectCommand.Parameters("@Region").Value = Me.cboRegion.SelectedValue
        End If

        'CRG
        If Me.cboCRG.SelectedIndex = -1 Then
            Me.daspGrant.SelectCommand.Parameters("@CRG").Value = "%"
        Else
            Me.daspGrant.SelectCommand.Parameters("@CRG").Value = Me.cboCRG.Text & "%"
        End If

        da = Me.daspGrant
        GoTo FilLDataset

FindMGI:
        '........................
        '5 FILL DATASET
FilLDataset:
        Try
            Me.DsSrchGrant1.EnforceConstraints = False
            da.Fill(Me.DsSrchGrant1.Tables(strtbl))

        Catch exc As System.FormatException
            modGlobalVar.msg(MsgCodes.invalidSearch)
            Me.StatusBarPanel1.Text = "grant grid fill error"
            Exit Sub
        Catch exc As Exception
            modGlobalVar.msg("ERROR: can't fill main grant grid", exc.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        '6 SET HEADINGS:
        If Me.DsSrchGrant1.Tables(strtbl).Rows.Count > 0 Then
            Me.grdMain.DataSource = Me.DsSrchGrant1.Tables(strtbl)
            Me.grdMain.CaptionText = DsSrchGrant1.Tables(strtbl).Rows.Count.ToString & "  " & strDGM
            strCol = grdMain.Item(0, 0)
            Me.StatusBar1.Panels(0).Text = "Done" ' searching" & Me.DsSrchGrant1.Tables(strtbl).Rows.Count.ToString
        Else
            Me.grdMain.CaptionText = "NO MATCHING " & strHdr & " FOUND"
            If isLoaded Then
                Me.StatusBarPanel1.Text = "No matches found:" & " " & Me.cboGrant.Text & " " & Me.cboRegion.Text & " " & Me.cboStatus.Text 'Me.lstStatus.Text
            End If
        End If

        Me.btnSearch.BackColor = Color.FromKnownColor(KnownColor.Control)
        MouseDefault()
    End Sub

#End Region

#Region "Datagrid"

    'MAIN GRID HIGHLIGHT ROW, FILL DETAIL DS IF USER REQUESTS
    Private Sub grdMain_CurrentCellChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles grdMain.CurrentCellChanged

        strCol = Me.grdMain.Item(grdMain.CurrentCell.RowNumber, 0)
        txtCurrent.Text = strCol.ToString
        Me.StatusBarPanelID.Text = "Grant ID: " & strCol.ToString
        ' ClearSecGrids()
        Me.grdMain.Select(grdMain.CurrentCell.RowNumber)
        'If Me.chkDetail.Checked Then
        '    LoadSecondary()
        'Else
        'End If

    End Sub

    'SECONDARY GRID HIGHLIGHT ROW
    Private Sub grdSecond_CurrentCellChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim dg As DataGrid
        dg = sender
        dg.Select(dg.CurrentCell.RowNumber)
        Me.txtSelection.Text = dg.Item(dg.CurrentCell.RowNumber, 0)
    End Sub

    'CAPTURE RIGHT MOUSE CLICK TO FILTER APPROPRIATE GRID
    Protected Sub grdAll_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grdMain.MouseDown
        ' modGlobalVar.Msg("click")
        'Dim tbl As Object
        'Dim strHdr As String    'text for grid header
        '  Dim statusMbj As Object

        'modGlobalVar.Msg(Me.grdMain.Item(grdMain.CurrentCell.RowNumber, 1))'captures previously selected

RightMouseClick:
        If e.Button = System.Windows.Forms.MouseButtons.Right Then
            hti = sender.HitTest(e.X, e.Y)

SetFilter:
            If hti.Type = DataGrid.HitTestType.Cell Then    'SET FILTER
                'CHECK FOR NULLS
                If IsDBNull(sender.Item(hti.Row, hti.Column)) Then 'nulls cause filter error
                    modGlobalVar.msg(MsgCodes.filterEmpty)
                    Exit Sub
                End If
                'CHECK FOR APOSTROPHE
                If Object.ReferenceEquals(sender.item(hti.Row, hti.Column).GetType, Now.GetType) Or Object.ReferenceEquals(sender.item(hti.Row, hti.Column).GetType, GetType(Integer)) Or Object.ReferenceEquals(sender.item(hti.Row, hti.Column).GetType, GetType(Decimal)) Then
                Else    'or throws an error on date fields
                    If sender.item(hti.Row, hti.Column).indexof("'") > 0 Then
                        modGlobalVar.msg(MsgCodes.filterApostrophe)
                        Exit Sub
                    End If
                End If
                'CALL SET FILTER
                Select Case strHdr
                    Case Is = strDGM
                        grdFilter(sender, Me.DsSrchGrant1, Me.DsSrchGrant1.SrchGrant, strHdr, dvM)
                    Case Is = strDGM2
                        grdFilter(sender, Me.DsSrchGrant1, Me.DsSrchGrant1.SrchMGI, strHdr, dvM2)

                End Select
ClearFilter:
            Else                                            'CLEAR FILTER
                sender.DataSource = tbl 'removes dv.rowfilter
                sender.CaptionText = tbl.Rows.Count.ToString & "  " & strHdr 'DsOrgSearch1.tblOrg.Rows.Count.ToString
                '  statusM = "Clearing Filter"
                ' SetStatusBarText()

                Select Case strHdr
                    Case Is = strDGM, strDGM2
                        statusM = "Done"
                    Case Else
                        modGlobalVar.msg("ERROR: clear filter", strHdr, MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Select
                SetStatusBarText()
            End If
LeftMouseClick:
        Else    'left click
            strbActiveGrid.Replace(strbActiveGrid.ToString, sender.name)
            '   iCol = Me.grdMain.Item(grdMain.CurrentCell.RowNumber, 0)
        End If
    End Sub


    'SET STATUS BAR LEFT TEXT
    Private Sub SetStatusBarText()
        Me.StatusBar1.Panels(0).Text = statusM & " " & statusS1 & " " & statusS2
    End Sub

    'CLEAR SELECTION FROM DATAGRIDS AND LOSE FOCUS (note: selection stays with arrow)
    Private Sub grdUnselect_Click(ByVal sender As Object, ByVal e As System.EventArgs) _
        Handles MyBase.Click
        If grdMain.CurrentRowIndex > -1 Then
            Me.grdMain.UnSelect(grdMain.CurrentRowIndex)
            Me.grdMain.NavigateBack()
        End If
        'If grdSecond1.CurrentRowIndex > -1 Then
        '    Me.grdSecond1.UnSelect(grdSecond1.CurrentRowIndex)
        '    Me.grdSecond1.NavigateBack()
        'End If
        'If grdSecond2.CurrentRowIndex > -1 Then
        '    Me.grdSecond2.UnSelect(grdSecond2.CurrentRowIndex)
        '    Me.grdSecond2.NavigateBack()
        'End If
    End Sub

    'SET SEARCH CRITERIA TO DEFAULTS AND CLEAR DATASETS
    Protected Sub miClearSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles miClearSearch.Click
        isLoaded = False
        ' Me.cboStaff.SelectedIndex = cboStaff.FindString(usr)
        '   Me.lstStatus.SelectedIndex = -1
        Me.cboStatus.SelectedIndex = -1
        Me.cboStatus.SelectedIndex = -1
        '  Me.txtSearch.Text = "optional - enter search text"
        '  Me.cboCaller.SelectedItem = "Case Manager"
        Me.DsSrchGrant1.Clear()
        '   ClearSecGrids()
        Me.Refresh()
        ' EnableMenuItems(False)
        dv.RowFilter = ""
        isLoaded = True
    End Sub

    'CLEAR SECONDARY GRIDS
    'Private Sub ClearSecGrids()
    '    Try
    '        Me.DsCaseSecondary1.Clear()
    '    Catch e As Exception

    '    End Try
    '    ' Me.grdMain.CaptionText = ""
    '    Me.grdSecond1.CaptionText = ""
    '    Me.grdSecond2.CaptionText = ""
    '    statusS1 = ""
    '    statusS2 = ""
    '    SetStatusBarText()
    'End Sub


    'HIDE/SHOW SECONDARY GRIDS
    'Private Sub chkDetail_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
    '        Handles chkDetail.CheckedChanged, RadioButton1.CheckedChanged, RadioButton2.CheckedChanged
    '    If chkDetail.Checked = True Then
    '        ' Me.pnlSecGrids.Visible = True
    '        Me.grdSecond1.Visible = RadioButton1.Checked
    '        Me.grdSecond2.Visible = RadioButton2.Checked
    '        Me.grdSecond3.Visible = RadioButton2.Checked
    '    Else
    '        Me.grdSecond1.Visible = False
    '        Me.grdSecond2.Visible = False
    '        Me.grdSecond3.Visible = False
    '    End If
    '    '    LoadSecondary()

    'End Sub



#Region "Grid Filter"

    'FILTER METHOD
    Private Sub grdFilter(ByVal grd As Object, ByVal ds As Object, ByVal tbl As DataTable, ByVal strHdr As String, ByVal dv As DataView)
        Dim strFilter As String
        Dim myColumns As GridColumnStylesCollection
        Select Case tbl.ToString
            Case Is = "SrchGrant"
                myColumns = grd.TableStyles(0).GridColumnStyles
            Case Else
                myColumns = grd.TableStyles(1).GridColumnStyles
        End Select

        strFilter = myColumns(hti.Column).MappingName
        strFilter = strFilter & " = '" & grd.Item(hti.Row, hti.Column) & " '"
        dv.RowFilter = strFilter
        grd.DataSource = dv
        grd.CaptionText = dv.Count.ToString & "/" & tbl.Rows.Count.ToString & "  " & strHdr
        '   Select Case strHdr
        '       Case Is = strDGM
        statusM = strHdr & " filtered on " & myColumns(hti.Column).HeaderText

        '  End Select
        SetStatusBarText()
    End Sub

#End Region


#Region "Grid DoubleClick"

    'Private Sub DataGridDouble(ByVal sender As Object, ByVal e As MouseEventArgs)
    '    If (DateTime.Now < modGlobalVar.CheckDouble(sender, e).AddMilliseconds(SystemInformation.DoubleClickTime)) Then
    '        Select Case strbActiveGrid.ToString
    '            Case Is = "grdMain"
    '                miGotoGrant.PerformClick()
    '                'Case Is = "grdSecond1"
    '                '   miGotoConversation.PerformClick()
    '                'Case Is = "grdSecond2"
    '                '  miGotoGrant.PerformClick()
    '            Case Else
    '                ' modGlobalVar.Msg("ERROR: grid not found","",MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        End Select

    '    End If
    'End Sub
#End Region



#End Region     'datagrid

#Region "Load Secondary"

    ''LOAD SECONDARY GRIDS
    'Protected Sub LoadSecondary()
    '    If isLoaded Then
    '        If Me.chkDetail.Checked Then
    '        Else
    '            Exit Sub
    '        End If

    '        Me.DsRecommendations1.Clear()
    '        Me.DsCaseSecondary1.Clear()

    'If RadioButton1.Checked Then    'load first grid
    '    Me.DsCaseSecondary1.EnforceConstraints = False
    '    sc.Open()
    '    Try
    '        daSecond1.SelectCommand.Parameters("@IDVal").Value = iCol
    '        daSecond1.SelectCommand.Parameters("@IDFld").Value = "Case"
    '        daSecond1.Fill(Me.DsCaseSecondary1.Tables(0))
    '        Me.grdSecond1.DataSource = Me.DsCaseSecondary1.Tables(0)
    '        Me.grdSecond1.CaptionText = Me.DsCaseSecondary1.Tables(0).Rows.Count.ToString & "  " & strDGS1
    '    Catch exc As Exception
    '        modGlobalVar.Msg(exc.Message, MessageBoxIcon.Error, "da2 error")
    '    End Try

    'ElseIf RadioButton2.Checked = True Then
    'Try    'fill second table of secondary grid for 2nd secondary grid
    '    daSecond2.SelectCommand.Parameters("@IDFld").Value = "Case"
    '    daSecond2.SelectCommand.Parameters("@IDVal").Value = iCol
    '    daSecond2.Fill(Me.DsCaseSecondary1.Tables(1))
    '    Me.grdSecond2.DataSource = Me.DsCaseSecondary1.Tables(1)
    '    Me.grdSecond2.CaptionText = Me.DsCaseSecondary1.Tables(1).Rows.Count.ToString & "  " & strDGS2
    'Catch exc As Exception
    '    modGlobalVar.Msg(exc.Message, MessageBoxIcon.Error, "da3 error")
    'End Try
    'Try    'fill 3rd grid
    '    Me.DsRecommendations1.EnforceConstraints = False
    '    Me.daspGetRecommendations.SelectCommand.Parameters("@IDFld").Value = "Case"
    '    Me.daspGetRecommendations.SelectCommand.Parameters("@IDVal").Value = iCol
    '    Me.daspGetRecommendations.Fill(Me.DsRecommendations1.Tables(0))
    '    Me.grdSecond3.DataSource = Me.DsRecommendations1.Tables(0)
    '    Me.grdSecond3.CaptionText = Me.DsRecommendations1.Tables(0).Rows.Count.ToString & "  RECOMMENDATIONS" ' & strDGS3
    'Catch exc As Exception
    '    modGlobalVar.Msg(exc.Message, MessageBoxIcon.Error, "daRec error")
    'End Try
    '        End If
    '        sc.Close()
    '    End If
    'End Sub
#End Region

    'TODO change which form opens if is MGI
#Region "Open Main Form"
    'OPEN MAIN CASE FORM
    Protected Sub btnOpenMain_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles grdMain.DoubleClick, miGotoGrant.Click
        '  modGlobalVar.Msg("opening main grant form", , strCol)

        If Me.cboGrant.SelectedItem = "Major Grant Initiative Applications" Then
            Select Case RTrim(Microsoft.VisualBasic.Left(Me.cboMGI.SelectedItem, 4))
                Case Is = "LTGI"
                    Try
                        modGlobalVar.OpenMainLTGI(Me.grdMain.Item(Me.grdMain.CurrentRowIndex, 0), Me.grdMain.Item(Me.grdMain.CurrentRowIndex, 8), Me.grdMain.Item(Me.grdMain.CurrentRowIndex, 2)) 'Me.grdMain.Item(Me.grdMain.CurrentRowIndex, 2), Me.grdMain.Item(Me.grdMain.CurrentRowIndex, 1))
                    Catch ex As Exception
                        modGlobalVar.msg("ERROR: open LTGI", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try
                Case Is = "TGI"
                    modGlobalVar.OpenMainTGI(Me.grdMain.Item(Me.grdMain.CurrentRowIndex, 0), Me.grdMain.Item(Me.grdMain.CurrentRowIndex, 8), Me.grdMain.Item(Me.grdMain.CurrentRowIndex, 2)) 'Me.grdMain.Item(Me.grdMain.CurrentRowIndex, 2), Me.grdMain.Item(Me.grdMain.CurrentRowIndex, 1))
                Case Is = "TMGI"
                    modGlobalVar.OpenMainTMGI(Me.grdMain.Item(Me.grdMain.CurrentRowIndex, 0), Me.grdMain.Item(Me.grdMain.CurrentRowIndex, 8), Me.grdMain.Item(Me.grdMain.CurrentRowIndex, 2)) 'Me.grdMain.Item(Me.grdMain.CurrentRowIndex, 2), Me.grdMain.Item(Me.grdMain.CurrentRowIndex, 1))
                Case Is = "YMGI"
                    modGlobalVar.OpenMainYMGI(Me.grdMain.Item(Me.grdMain.CurrentRowIndex, 0), Me.grdMain.Item(Me.grdMain.CurrentRowIndex, 8), Me.grdMain.Item(Me.grdMain.CurrentRowIndex, 2))
                Case Is = "CMG"
                    modGlobalVar.OpenMainCMG(Me.grdMain.Item(Me.grdMain.CurrentRowIndex, 0), Me.grdMain.Item(Me.grdMain.CurrentRowIndex, 8), Me.grdMain.Item(Me.grdMain.CurrentRowIndex, 2))

                Case Else
                    'If Me.cboMGI.Text.Contains("SSGI") Or Me.cboMGI.Text.Contains("CMGI") Then
                    modGlobalVar.msg("ARCHIVED INFORMATION", "See " & DBAdmin.StaffName & " for SSGI or CMGI Application details", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    '    Exit Sub
                    'End If
            End Select
        Else

            ' icolEventSKU = Me.grdMain.TableStyles("tStyleGrant").GridColumnStyles.IndexOf(CType(Me.grdMain.TableStyles("tStyleGrant").GridColumnStyles("OrgandPhone"), DataGridColumnStyle))
            'tStyleMGI
            modGlobalVar.OpenMainGrant(strCol, Me.grdMain.Item(Me.grdMain.CurrentRowIndex, 2), Me.grdMain.Item(Me.grdMain.CurrentRowIndex, 9), Me.grdMain.Item(Me.grdMain.CurrentRowIndex, 1))
        End If
    End Sub

#End Region

#Region "SetChoices"
    'TODO change this to luStatus query
    Private Sub LoadList()
        colGrant.Add("----- GRANT PROCESS -----")
        colGrant.Add("In Approval Process")
        colGrant.Add("Post Approval Process")
        colGrant.Add("Congregation In Action")
        colGrant.Add("Congregation Report Due")
        colGrant.Add("Ready For ICC Followup Report")
        colGrant.Add("Completed")
        colGrant.Add("")
        colGrant.Add("----- DETERMINATION -----")
        colGrant.Add("Approved")
        colGrant.Add("Denied")
        colGrant.Add("Rescinded")
        colGrant.Add("Returned")
        colGrant.Add("Withdrawn")
        colGrant.Add("")
        colGrant.Add("----- CLOSURE STATUS -----")
        colGrant.Add("Abandoned")
        colGrant.Add("Completed")
        colGrant.Add("Withdrawn")
        colGrant.Add("")
        colGrant.Add("----- ALL or OVERDUE -----")
        colGrant.Add("All Closed Grants")
        colGrant.Add("All Open Grants")
        colGrant.Add("All Overdue Grants")
        colGrant.Add("All")


        colMGI.Add("All Applications")
        colMGI.Add("Approved")
        colMGI.Add("Denied")
        colMGI.Add("Waiting List")
        colMGI.Add("Undetermined")

        Dim cmd As New SqlCommand("SELECT  GrantTypeName from dbo.luGrantTokenTbl WHERE ExcludeFromDropDowns = 0 ORDER BY SortFld", sc)
        Dim drdr As SqlDataReader
        Me.cboGrant.Items.Add("All Grants")
        Me.cboGrant.Items.Add("My Grants")
        Me.cboGrant.Items.Add("----------")
        If Not SCConnect() Then
            Exit Sub
        End If

        Try
            drdr = cmd.ExecuteReader
        Catch ex As Exception
            modGlobalVar.msg("ERROR: grant type dr", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
        While drdr.Read()
            Me.cboGrant.Items.Add(drdr.GetString(0))
        End While
        drdr.Close()
        sc.Close()
        Me.cboGrant.Items.Add("------")
        Me.cboGrant.Items.Add("Major Grant Initiative Applications")

    End Sub

#End Region

#Region "General"

    'FIND CRG TERM
    Private Sub cboCRG_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) _
            Handles cboCRG.MouseDown
        If e.Button = System.Windows.Forms.MouseButtons.Right Then
            modPopup.SearchCRG(Me, PointToClient(Control.MousePosition), Me.cboCRG)
        Else
        End If
    End Sub

    'SET LIST BASED ON TYPE OF GRANT
    Private Sub cboGrant_Change(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles cboGrant.SelectedIndexChanged ', cboGrant.Leave

        If modGlobalVar.NewIsHeading(sender) = True Then
            Exit Sub
        End If
        Dim i As Integer
        MouseWait()
        Me.DsSrchGrant1.Clear()

        If Me.cboStatus.DataSource Is Me.colGrant Then
            i = Me.cboStatus.SelectedIndex
        Else
            i = 1
        End If
        Select Case Me.cboGrant.SelectedItem.ToString

            Case "Major Grant Initiative Applications"
                Me.cboMGI.Visible = True
                Me.cboStatus.DataSource = Me.colMGI
                Me.cboStaff.Visible = False
                Me.btnSendMail.Visible = True
            Case ""
                MouseDefault()
                Exit Sub
            Case Else
                Me.cboStatus.DataSource = Me.colGrant
                Me.cboStatus.SelectedIndex = i
                Me.cboMGI.Visible = False
                Me.cboRegion.Visible = True
                If Me.cboGrant.SelectedItem = "My Grants" Then
                    Me.cboStaff.Visible = True
                    Me.cboStaff.SelectedIndex = Me.cboStaff.FindString(usrName)
                    Me.cboRegion.SelectedIndex = Me.cboRegion.FindString(usrRegion)
                Else
                    Me.cboStaff.Visible = False
                End If
                Me.btnSendMail.Visible = False
        End Select
        Me.lblRegion.Visible = Me.cboRegion.Visible
        Me.lblMGI.Visible = Me.cboMGI.Visible
        Me.lblStaff.Visible = Me.cboStaff.Visible
        miSrch.PerformClick()
        MouseDefault()
    End Sub

    'HELP BUTTON
    Private Sub btnHelp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
             Handles btnHelp.Click, miHelp.Click
        modGlobalVar.msg("HELP: SEARCH for GRANT or MGI APPLICATIONS:", "* Asterisk after the Case Name indicates an Overdue Grant Report" & NextLine & NextLine & "HOW TO SEARCH:" & NextLine & "1. Select search criteria using the drop down box and list. " & NextLine & "2. The result grid will update automatically, or click the Search button, or press the Enter key." & NextLine & NextLine & "ADD NEW GRANT:" & NextLine & "First go to the Organization Detail window, then click the yellow New button in that window. " & NextLine & "or use the menu: File/New Grant.", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    'HELP definitions
    Private Sub miDefinitions_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles miDefinitions.Click
        modPopup.ShowDefinitions("Grant")
    End Sub

    'REGION CALL SEARCH
    Private Sub cboRegion_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) _
        Handles cboRegion.SelectedIndexChanged, cboMGI.SelectedIndexChanged, cboStaff.SelectedIndexChanged
        If isLoaded Then
            ' If mdGlobalVar.NewValidateCombo(sender, True) Then
            Me.btnSearch.PerformClick()
            'End If
        End If
    End Sub

    'STATUS CALL SEARCH
    Private Sub cbostatus_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) _
        Handles cboStatus.SelectedIndexChanged ', cboRegion.SelectionChangeCommitted, cboMGI.SelectionChangeCommitted
        If isLoaded Then
            If modGlobalVar.NewIsHeading(sender) = True Then
            Else
                Me.btnSearch.PerformClick()
            End If
        End If
    End Sub

    'CRG CALL SEARCH
    Private Sub cboCRG_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCRG.SelectedIndexChanged
        If isLoaded Then
            If sender.selectedindex > -1 Then
                Me.btnSearch.PerformClick()
            End If
        End If
    End Sub

#End Region 'general

#Region "EMAIL"

    'GENERATE BULK EMAIL LIST  - TODO perhaps add choice of postal mail instead of email
    Private Sub btnSendMail_Click(sender As System.Object, e As System.EventArgs) Handles btnSendMail.Click
        'need choice of PD or TemporaryContact
        Dim ppup As New ContextMenu
        Dim eh As EventHandler = AddressOf MailListWho
        ppup.MenuItems.Add("Choose Recipient:")
        ppup.MenuItems.Add("-----------------")
        If RTrim(Microsoft.VisualBasic.Left(Me.cboMGI.SelectedItem, 4)) = "YMGI" Then
            ppup.MenuItems.Add("Cong Rep", eh)
        Else
            ppup.MenuItems.Add("Clergy Leader", eh)
        End If
        ppup.MenuItems.Add("Project Director", eh)
        If RTrim(Microsoft.VisualBasic.Left(Me.cboMGI.SelectedItem, 4)) = "CMG" Then
            ppup.MenuItems.Add("Temporary Contact", eh)
        End If
        ppup.Show(Me, PointToClient(Control.MousePosition))
    End Sub

    'POPUP  HANDLER
    Private Sub MailListWho(ByVal obj As Object, ByVal ea As EventArgs)
        Dim qbe As New SqlCommand
        Dim sbjectLine As String
        qbe.CommandText = ("[MailUserGrant]")
        qbe.Connection = sc
        qbe.CommandType = CommandType.StoredProcedure

        qbe.Parameters.Add("@What", SqlDbType.VarChar).Value = RTrim(Microsoft.VisualBasic.Left(Me.cboMGI.SelectedItem, 4))
        qbe.Parameters.Add("@Which", SqlDbType.VarChar).Value = "AppName"
        'TODO change this to lookup query
        '     Me.cboMGI.Items.AddRange(New Object() {"CMG  Community Ministry 2016 Applications", "YMGI  Youth Ministry 2015 Applications", "TMGI  Technology & Ministry 2012 Applications ", "TGI   Technology 2010 Applications", "LTGI  Life Together Applications", "", "----- Details Archived --------", "YMG1 Youth Ministry 2013 Applications", "SSGI  Sacred Space Applications ", "CMGI  Computer Grant Applications"})
        If Not SCConnect() Then
            Exit Sub
        End If
        sbjectLine = qbe.ExecuteScalar()
        sc.Close()
        qbe.Parameters("@Which").Value = Replace(obj.text, " ", "")
        OpenEmailForm(qbe, "Center for Congregations " & sbjectLine & " Application")
        qbe = Nothing
        sbjectLine = Nothing
    End Sub

#End Region

End Class

