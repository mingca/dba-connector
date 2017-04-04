Option Explicit On
Imports System.data.SqlClient
Imports System.Windows.Forms.PaintEventArgs
Imports System.drawing
Imports System.Text
Imports System.Threading
Imports System.Windows.Forms.Control
Imports System.IO
Imports Microsoft.Office.Interop

Public Class frmSrchCase
    Inherits System.Windows.Forms.Form

    Dim daM As New SqlDataAdapter
    Dim daSecond1 As New SqlDataAdapter
    Dim daSecond2 As New SqlDataAdapter
    Dim daSecond3 As New SqlDataAdapter
    Dim usrRadio As New StringBuilder 'text from radio button
    Dim hti As System.Windows.Forms.DataGrid.HitTestInfo
    Dim htivw As DataGridView.HitTestInfo
    Dim dvS1, dvS2, dvS3 As DataView   'filter for each datagrid
    Dim statusM, statusS1, statusS2, statusS3 As String 'filter text for status bar
    Dim isLoaded As Boolean = False
    Dim gridMouseDownTime As DateTime
    Dim iCaseID, iOrgID As Integer 'selected ID column value
    Dim strbActiveGrid As New StringBuilder
    Dim bFoundCase As Boolean   'disallow open forms if no record in grid
    Dim bFoundGrant, bFoundConversation, bfoundResource As Boolean
    Dim iGrid As Integer    'count rows of [filtered] grid
    Dim strGoTo As String 'for goto orgname + phone from grid
    Dim SrchTotal As Integer 'keep count for display 
    Dim dvMain As DataView 'for main dgview filter

    Const strDGM As String = "CASES" 'header text on datagrid
    Const strDGS1 As String = "CONVERSATIONS"
    Const strDGS2 As String = "RECOMMENDATIONS"
    Friend WithEvents miPrintContact As System.Windows.Forms.MenuItem
    Const strDGS3 As String = "GRANTS"


#Region "Initialize"

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        modGlobalVar.LoadStatusList(Me.lstStatus, "Full") ', "Case")

        Dim tbx As DataGridTextBoxColumn
        Dim tbs As DataGridTableStyle

  
        For Each tbs In Me.grdConversation.TableStyles
            For Each tbx In tbs.GridColumnStyles
                '  AddHandler tbx.TextBox.MouseDown, New MouseEventHandler(AddressOf DataGridDouble)
                tbx.TextBox.Enabled = False
                tbx.NullText = ""
            Next
        Next
        For Each tbs In Me.grdGrant.TableStyles
            For Each tbx In tbs.GridColumnStyles
                'AddHandler tbx.TextBox.MouseDown, New MouseEventHandler(AddressOf DataGridDouble)
                tbx.TextBox.Enabled = False
                tbx.NullText = ""
            Next
        Next

        For Each tbs In Me.grdRecommendation.TableStyles
            For Each tbx In tbs.GridColumnStyles
                'AddHandler tbx.TextBox.MouseDown, New MouseEventHandler(AddressOf DataGridDouble)
                tbx.TextBox.Enabled = False
                tbx.NullText = ""
            Next
        Next
        ' Me.btnHelp.Image = SystemIcons.Question.ToBitmap
        Me.HelpProvider1.SetHelpString(Me.grdvwMain, strHelpGrid)
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

#End Region 'initialize

#Region " Windows Form Designer generated code "

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents grdRecommendation As System.Windows.Forms.DataGrid
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents btnSearch As System.Windows.Forms.Button

    Friend WithEvents miHelp As System.Windows.Forms.MenuItem
    Friend WithEvents DataGridTextBoxColumn27 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn28 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn29 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents cboStaff As InfoCtr.ComboBoxRelaxed
    Friend WithEvents cntRecommend As System.Windows.Forms.Label
    Friend WithEvents cntConvers As System.Windows.Forms.Label
    Friend WithEvents lblSelectionID As System.Windows.Forms.Label
    Friend WithEvents fldSelectionNum As System.Windows.Forms.TextBox
    Friend WithEvents cntGrant As System.Windows.Forms.Label
    Friend WithEvents rb3Grant As System.Windows.Forms.RadioButton
    Friend WithEvents grdvwMain As System.Windows.Forms.DataGridView
    Friend WithEvents lblMaino As System.Windows.Forms.Label
    Friend WithEvents lblMainGrid As System.Windows.Forms.Label
    Friend WithEvents DataGridTableStyle2 As System.Windows.Forms.DataGridTableStyle
    Friend WithEvents DataGridTextBoxColumn9 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn10 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn11 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn12 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn21 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTableStyle4 As System.Windows.Forms.DataGridTableStyle
    Friend WithEvents DataGridTextBoxColumn6 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn7 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn8 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn17 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn18 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents cboCRG As InfoCtr.ComboBoxRelaxed 'System.Windows.Forms.ComboBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents DataGridTextBoxColumn19 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents SqlConnection1 As System.Data.SqlClient.SqlConnection
    Friend WithEvents DataGridTextBoxColumn20 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents cboFromCRG As System.Windows.Forms.CheckBox
    Friend WithEvents CaseIDColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents OrgNumColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents OrgandCityColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CaseNameColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents StatusDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents StatusNumDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents LastCallDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CaseMgrDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PhoneColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ResourcesDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents FromCRGColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CRGConsultingOrgColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents StatusBarPanel3 As System.Windows.Forms.StatusBarPanel
    Friend WithEvents StatusBarPanelID As System.Windows.Forms.StatusBarPanel
    Friend WithEvents CheckBox1 As System.Windows.Forms.CheckBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents RadioButton4 As System.Windows.Forms.RadioButton
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents RadioButton5 As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButton6 As System.Windows.Forms.RadioButton
    Friend WithEvents ListBox1 As System.Windows.Forms.ListBox
    Friend WithEvents miDefinitions As System.Windows.Forms.MenuItem
    Friend WithEvents StatusBar1 As System.Windows.Forms.StatusBar
    Friend WithEvents HelpProvider1 As System.Windows.Forms.HelpProvider
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents MainMenu1 As System.Windows.Forms.MainMenu
    Friend WithEvents MenuItem1 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem2 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem3 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem4 As System.Windows.Forms.MenuItem
    Friend WithEvents miClearSearch As System.Windows.Forms.MenuItem
    '  Friend WithEvents SqlDataAdapter1 As System.Data.SqlClient.SqlDataAdapter
    '  Friend WithEvents SqlConnection1 As System.Data.SqlClient.SqlConnection
    Friend WithEvents DataGridTableStyle1 As System.Windows.Forms.DataGridTableStyle
    Friend WithEvents DataGridTextBoxColumn1 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn2 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn3 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn4 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn5 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents miPrintConv As System.Windows.Forms.MenuItem
    Friend WithEvents grdConversation As System.Windows.Forms.DataGrid
    Friend WithEvents grdGrant As System.Windows.Forms.DataGrid
    '  Protected WithEvents cboStatus As InfoCtr.ComboBoxRelaxed
    Friend WithEvents cboCaller As InfoCtr.ComboBoxRelaxed
    '   Protected WithEvents ocboStaff As InfoCtr.ComboBoxRelaxed
    Friend WithEvents sqlspSrchCases As System.Data.SqlClient.SqlCommand
    Friend WithEvents DsSrchCase1 As InfoCtr.dsSrchCase
    Friend WithEvents miSrch As System.Windows.Forms.MenuItem
    Friend WithEvents miGotoCase As System.Windows.Forms.MenuItem
    ' Friend WithEvents SqlDataAdapter2 As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents sqlspGetConversations As System.Data.SqlClient.SqlCommand
    Friend WithEvents sqlspGetRecommendations As System.Data.SqlClient.SqlCommand
    Friend WithEvents DsCaseSecondary1 As InfoCtr.dsCaseSecondary
    'Friend WithEvents SqlDataAdapter3 As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents sqlspGetGrants As System.Data.SqlClient.SqlCommand
    Friend WithEvents fldOrgNum As System.Windows.Forms.TextBox
    Friend WithEvents StatusBarPanel1 As System.Windows.Forms.StatusBarPanel
    Friend WithEvents StatusBarPanel2 As System.Windows.Forms.StatusBarPanel
    Friend WithEvents DataGridTextBoxColumn13 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents miGotoGrant As System.Windows.Forms.MenuItem
    Friend WithEvents miGotoConversation As System.Windows.Forms.MenuItem
    Friend WithEvents DataGridTextBoxColumn14 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn15 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents DataGridTextBoxColumn16 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents btnNew As System.Windows.Forms.Button
    Friend WithEvents lstStatus As System.Windows.Forms.ListBox
    Friend WithEvents pnlSecGrids As System.Windows.Forms.Panel
    Friend WithEvents mmHelp As System.Windows.Forms.MenuItem
    '  Friend WithEvents daspGetRecommendations As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents SqlSelectCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents DataGridTableStyle3 As System.Windows.Forms.DataGridTableStyle
    Friend WithEvents DataGridTextBoxColumn22 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn23 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn24 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn25 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents rb2Recommend As System.Windows.Forms.RadioButton
    Friend WithEvents rb1Convers As System.Windows.Forms.RadioButton
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents chkDetail As System.Windows.Forms.CheckBox
    Friend WithEvents lblOverdue As System.Windows.Forms.Label
    Friend WithEvents miGotoRecommendation As System.Windows.Forms.MenuItem
    Friend WithEvents btnHelp As System.Windows.Forms.Button
    Friend WithEvents grdMain As System.Windows.Forms.DataGrid
    Friend WithEvents miNew As System.Windows.Forms.MenuItem
    Friend WithEvents miCloseForm As System.Windows.Forms.MenuItem
    Friend WithEvents SqlSelectCommand2 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlSelectCommand3 As System.Data.SqlClient.SqlCommand
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSrchCase))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.cboFromCRG = New System.Windows.Forms.CheckBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cntGrant = New System.Windows.Forms.Label()
        Me.rb3Grant = New System.Windows.Forms.RadioButton()
        Me.cntRecommend = New System.Windows.Forms.Label()
        Me.cntConvers = New System.Windows.Forms.Label()
        Me.rb1Convers = New System.Windows.Forms.RadioButton()
        Me.rb2Recommend = New System.Windows.Forms.RadioButton()
        Me.lstStatus = New System.Windows.Forms.ListBox()
        Me.chkDetail = New System.Windows.Forms.CheckBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.StatusBar1 = New System.Windows.Forms.StatusBar()
        Me.StatusBarPanel1 = New System.Windows.Forms.StatusBarPanel()
        Me.StatusBarPanelID = New System.Windows.Forms.StatusBarPanel()
        Me.StatusBarPanel2 = New System.Windows.Forms.StatusBarPanel()
        Me.HelpProvider1 = New System.Windows.Forms.HelpProvider()
        Me.grdConversation = New System.Windows.Forms.DataGrid()
        Me.DsCaseSecondary1 = New InfoCtr.dsCaseSecondary()
        Me.DataGridTableStyle4 = New System.Windows.Forms.DataGridTableStyle()
        Me.DataGridTextBoxColumn6 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn7 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn8 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn17 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn20 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.grdRecommendation = New System.Windows.Forms.DataGrid()
        Me.DataGridTableStyle2 = New System.Windows.Forms.DataGridTableStyle()
        Me.DataGridTextBoxColumn9 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn10 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn11 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn12 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn21 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn18 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn19 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.grdMain = New System.Windows.Forms.DataGrid()
        Me.DsSrchCase1 = New InfoCtr.dsSrchCase()
        Me.DataGridTableStyle1 = New System.Windows.Forms.DataGridTableStyle()
        Me.DataGridTextBoxColumn1 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn16 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn2 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn3 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn4 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn5 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn14 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn13 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn28 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.btnHelp = New System.Windows.Forms.Button()
        Me.btnNew = New System.Windows.Forms.Button()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.RadioButton4 = New System.Windows.Forms.RadioButton()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.RadioButton5 = New System.Windows.Forms.RadioButton()
        Me.RadioButton6 = New System.Windows.Forms.RadioButton()
        Me.ListBox1 = New System.Windows.Forms.ListBox()
        Me.grdvwMain = New System.Windows.Forms.DataGridView()
        Me.CaseIDColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.OrgNumColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.OrgandCityColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CaseNameColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.StatusDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.StatusNumDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.LastCallDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CaseMgrDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PhoneColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ResourcesDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.FromCRGColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CRGConsultingOrgColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cboCRG = New InfoCtr.ComboBoxRelaxed()
        Me.cboStaff = New InfoCtr.ComboBoxRelaxed()
        Me.cboCaller = New InfoCtr.ComboBoxRelaxed()
        Me.MainMenu1 = New System.Windows.Forms.MainMenu(Me.components)
        Me.MenuItem1 = New System.Windows.Forms.MenuItem()
        Me.miNew = New System.Windows.Forms.MenuItem()
        Me.miCloseForm = New System.Windows.Forms.MenuItem()
        Me.MenuItem2 = New System.Windows.Forms.MenuItem()
        Me.miSrch = New System.Windows.Forms.MenuItem()
        Me.miClearSearch = New System.Windows.Forms.MenuItem()
        Me.MenuItem3 = New System.Windows.Forms.MenuItem()
        Me.miGotoCase = New System.Windows.Forms.MenuItem()
        Me.miGotoConversation = New System.Windows.Forms.MenuItem()
        Me.miGotoGrant = New System.Windows.Forms.MenuItem()
        Me.miGotoRecommendation = New System.Windows.Forms.MenuItem()
        Me.MenuItem4 = New System.Windows.Forms.MenuItem()
        Me.miPrintConv = New System.Windows.Forms.MenuItem()
        Me.miPrintContact = New System.Windows.Forms.MenuItem()
        Me.mmHelp = New System.Windows.Forms.MenuItem()
        Me.miHelp = New System.Windows.Forms.MenuItem()
        Me.miDefinitions = New System.Windows.Forms.MenuItem()
        Me.sqlspSrchCases = New System.Data.SqlClient.SqlCommand()
        Me.sqlspGetConversations = New System.Data.SqlClient.SqlCommand()
        Me.SqlConnection1 = New System.Data.SqlClient.SqlConnection()
        Me.sqlspGetRecommendations = New System.Data.SqlClient.SqlCommand()
        Me.sqlspGetGrants = New System.Data.SqlClient.SqlCommand()
        Me.fldOrgNum = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.pnlSecGrids = New System.Windows.Forms.Panel()
        Me.grdGrant = New System.Windows.Forms.DataGrid()
        Me.DataGridTableStyle3 = New System.Windows.Forms.DataGridTableStyle()
        Me.DataGridTextBoxColumn22 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn23 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn24 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn25 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn29 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn27 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.SqlSelectCommand1 = New System.Data.SqlClient.SqlCommand()
        Me.lblOverdue = New System.Windows.Forms.Label()
        Me.SqlSelectCommand2 = New System.Data.SqlClient.SqlCommand()
        Me.lblSelectionID = New System.Windows.Forms.Label()
        Me.fldSelectionNum = New System.Windows.Forms.TextBox()
        Me.lblMaino = New System.Windows.Forms.Label()
        Me.lblMainGrid = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.CheckBox1 = New System.Windows.Forms.CheckBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.GroupBox1.SuspendLayout()
        CType(Me.StatusBarPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.StatusBarPanelID, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.StatusBarPanel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdConversation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsCaseSecondary1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdRecommendation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdMain, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsSrchCase1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        CType(Me.grdvwMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlSecGrids.SuspendLayout()
        CType(Me.grdGrant, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(11, 28)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(95, 17)
        Me.Label3.TabIndex = 191
        Me.Label3.Text = "Search Criteria"
        '
        'btnSearch
        '
        Me.btnSearch.BackColor = System.Drawing.SystemColors.Control
        Me.btnSearch.Location = New System.Drawing.Point(59, 36)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(75, 25)
        Me.btnSearch.TabIndex = 2
        Me.btnSearch.Text = "Search"
        Me.btnSearch.UseVisualStyleBackColor = False
        '
        'cboFromCRG
        '
        Me.cboFromCRG.AutoSize = True
        Me.cboFromCRG.Location = New System.Drawing.Point(19, 290)
        Me.cboFromCRG.Name = "cboFromCRG"
        Me.cboFromCRG.Size = New System.Drawing.Size(154, 17)
        Me.cboFromCRG.TabIndex = 215
        Me.cboFromCRG.Text = "Initiated from CRG Website"
        Me.cboFromCRG.UseVisualStyleBackColor = True
        Me.cboFromCRG.Visible = False
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.ForeColor = System.Drawing.Color.Maroon
        Me.Label7.Location = New System.Drawing.Point(6, 316)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(75, 13)
        Me.Label7.TabIndex = 214
        Me.Label7.Text = "Scope of Staff"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.ForeColor = System.Drawing.Color.Maroon
        Me.Label6.Location = New System.Drawing.Point(10, 65)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(64, 13)
        Me.Label6.TabIndex = 213
        Me.Label6.Text = "Case Status"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.ForeColor = System.Drawing.Color.Maroon
        Me.Label5.Location = New System.Drawing.Point(7, 363)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(58, 13)
        Me.Label5.TabIndex = 212
        Me.Label5.Text = "CRG Issue"
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.SystemColors.InactiveBorder
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.cntGrant)
        Me.GroupBox1.Controls.Add(Me.rb3Grant)
        Me.GroupBox1.Controls.Add(Me.cntRecommend)
        Me.GroupBox1.Controls.Add(Me.cntConvers)
        Me.GroupBox1.Controls.Add(Me.rb1Convers)
        Me.GroupBox1.Controls.Add(Me.rb2Recommend)
        Me.GroupBox1.Location = New System.Drawing.Point(8, 403)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(173, 104)
        Me.GroupBox1.TabIndex = 4
        Me.GroupBox1.TabStop = False
        Me.ToolTip1.SetToolTip(Me.GroupBox1, "Uncheck this box to speed up the search.")
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.ForeColor = System.Drawing.Color.Maroon
        Me.Label2.Location = New System.Drawing.Point(2, 14)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(72, 13)
        Me.Label2.TabIndex = 214
        Me.Label2.Text = "Related Items"
        '
        'cntGrant
        '
        Me.cntGrant.AutoSize = True
        Me.cntGrant.Location = New System.Drawing.Point(143, 82)
        Me.cntGrant.MinimumSize = New System.Drawing.Size(20, 0)
        Me.cntGrant.Name = "cntGrant"
        Me.cntGrant.Size = New System.Drawing.Size(20, 13)
        Me.cntGrant.TabIndex = 213
        '
        'rb3Grant
        '
        Me.rb3Grant.Location = New System.Drawing.Point(24, 78)
        Me.rb3Grant.Name = "rb3Grant"
        Me.rb3Grant.Size = New System.Drawing.Size(120, 20)
        Me.rb3Grant.TabIndex = 212
        Me.rb3Grant.Tag = "Grant"
        Me.rb3Grant.Text = "Grants"
        '
        'cntRecommend
        '
        Me.cntRecommend.AutoSize = True
        Me.cntRecommend.Location = New System.Drawing.Point(143, 58)
        Me.cntRecommend.MinimumSize = New System.Drawing.Size(20, 0)
        Me.cntRecommend.Name = "cntRecommend"
        Me.cntRecommend.Size = New System.Drawing.Size(20, 13)
        Me.cntRecommend.TabIndex = 211
        '
        'cntConvers
        '
        Me.cntConvers.AutoSize = True
        Me.cntConvers.Location = New System.Drawing.Point(143, 35)
        Me.cntConvers.MinimumSize = New System.Drawing.Size(20, 0)
        Me.cntConvers.Name = "cntConvers"
        Me.cntConvers.Size = New System.Drawing.Size(20, 13)
        Me.cntConvers.TabIndex = 9
        '
        'rb1Convers
        '
        Me.rb1Convers.Checked = True
        Me.rb1Convers.Location = New System.Drawing.Point(24, 30)
        Me.rb1Convers.Name = "rb1Convers"
        Me.rb1Convers.Size = New System.Drawing.Size(106, 20)
        Me.rb1Convers.TabIndex = 6
        Me.rb1Convers.TabStop = True
        Me.rb1Convers.Tag = "Conversation"
        Me.rb1Convers.Text = "Conversations"
        '
        'rb2Recommend
        '
        Me.rb2Recommend.Location = New System.Drawing.Point(24, 45)
        Me.rb2Recommend.Name = "rb2Recommend"
        Me.rb2Recommend.Size = New System.Drawing.Size(120, 37)
        Me.rb2Recommend.TabIndex = 7
        Me.rb2Recommend.Tag = "Recommend"
        Me.rb2Recommend.Text = "Resource Recommendations"
        '
        'lstStatus
        '
        Me.lstStatus.Location = New System.Drawing.Point(8, 80)
        Me.lstStatus.Name = "lstStatus"
        Me.lstStatus.Size = New System.Drawing.Size(173, 199)
        Me.lstStatus.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.lstStatus, "Case Status")
        '
        'chkDetail
        '
        Me.chkDetail.Checked = True
        Me.chkDetail.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkDetail.Location = New System.Drawing.Point(217, 348)
        Me.chkDetail.Name = "chkDetail"
        Me.chkDetail.Size = New System.Drawing.Size(16, 16)
        Me.chkDetail.TabIndex = 2
        Me.ToolTip1.SetToolTip(Me.chkDetail, "Uncheck this box to speed up the search.")
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Location = New System.Drawing.Point(214, 345)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(124, 20)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "     Show Related Items"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'StatusBar1
        '
        Me.StatusBar1.Location = New System.Drawing.Point(0, 597)
        Me.StatusBar1.Name = "StatusBar1"
        Me.StatusBar1.Panels.AddRange(New System.Windows.Forms.StatusBarPanel() {Me.StatusBarPanel1, Me.StatusBarPanelID, Me.StatusBarPanel2})
        Me.StatusBar1.ShowPanels = True
        Me.StatusBar1.Size = New System.Drawing.Size(1084, 22)
        Me.StatusBar1.TabIndex = 192
        '
        'StatusBarPanel1
        '
        Me.StatusBarPanel1.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents
        Me.StatusBarPanel1.BorderStyle = System.Windows.Forms.StatusBarPanelBorderStyle.None
        Me.StatusBarPanel1.MinWidth = 200
        Me.StatusBarPanel1.Name = "StatusBarPanel1"
        Me.StatusBarPanel1.Text = "Ready"
        Me.StatusBarPanel1.Width = 200
        '
        'StatusBarPanelID
        '
        Me.StatusBarPanelID.MinWidth = 200
        Me.StatusBarPanelID.Name = "StatusBarPanelID"
        Me.StatusBarPanelID.Text = "Case ID:"
        Me.StatusBarPanelID.Width = 200
        '
        'StatusBarPanel2
        '
        Me.StatusBarPanel2.Alignment = System.Windows.Forms.HorizontalAlignment.Right
        Me.StatusBarPanel2.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring
        Me.StatusBarPanel2.Name = "StatusBarPanel2"
        Me.StatusBarPanel2.Text = "Use this window to find Cases.  ShowDetail Checkbox reveals list of related Conve" & _
    "rsations or Resources recommended and Grants. "
        Me.StatusBarPanel2.Width = 667
        '
        'grdConversation
        '
        Me.grdConversation.AlternatingBackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.grdConversation.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.grdConversation.CaptionBackColor = System.Drawing.SystemColors.InactiveCaption
        Me.grdConversation.CaptionText = "Conversation"
        Me.grdConversation.DataMember = "GetConversations"
        Me.grdConversation.DataSource = Me.DsCaseSecondary1
        Me.grdConversation.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.HelpProvider1.SetHelpString(Me.grdConversation, "")
        Me.grdConversation.Location = New System.Drawing.Point(3, 3)
        Me.grdConversation.Name = "grdConversation"
        Me.grdConversation.ParentRowsVisible = False
        Me.grdConversation.ReadOnly = True
        Me.grdConversation.RowHeaderWidth = 15
        Me.HelpProvider1.SetShowHelp(Me.grdConversation, True)
        Me.grdConversation.Size = New System.Drawing.Size(770, 195)
        Me.grdConversation.TabIndex = 195
        Me.grdConversation.TableStyles.AddRange(New System.Windows.Forms.DataGridTableStyle() {Me.DataGridTableStyle4})
        Me.grdConversation.Tag = "Conversation"
        '
        'DsCaseSecondary1
        '
        Me.DsCaseSecondary1.DataSetName = "dsCaseSecondary"
        Me.DsCaseSecondary1.Locale = New System.Globalization.CultureInfo("en-US")
        Me.DsCaseSecondary1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'DataGridTableStyle4
        '
        Me.DataGridTableStyle4.AlternatingBackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.DataGridTableStyle4.DataGrid = Me.grdConversation
        Me.DataGridTableStyle4.GridColumnStyles.AddRange(New System.Windows.Forms.DataGridColumnStyle() {Me.DataGridTextBoxColumn6, Me.DataGridTextBoxColumn7, Me.DataGridTextBoxColumn8, Me.DataGridTextBoxColumn17, Me.DataGridTextBoxColumn20})
        Me.DataGridTableStyle4.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.DataGridTableStyle4.MappingName = "GetConversations"
        Me.DataGridTableStyle4.RowHeaderWidth = 15
        '
        'DataGridTextBoxColumn6
        '
        Me.DataGridTextBoxColumn6.Format = ""
        Me.DataGridTextBoxColumn6.FormatInfo = Nothing
        Me.DataGridTextBoxColumn6.HeaderText = "ConverseID"
        Me.DataGridTextBoxColumn6.MappingName = "ConversID"
        Me.DataGridTextBoxColumn6.Width = 0
        '
        'DataGridTextBoxColumn7
        '
        Me.DataGridTextBoxColumn7.Format = "d"
        Me.DataGridTextBoxColumn7.FormatInfo = Nothing
        Me.DataGridTextBoxColumn7.HeaderText = "Date"
        Me.DataGridTextBoxColumn7.MappingName = "ConversDate"
        Me.DataGridTextBoxColumn7.Width = 75
        '
        'DataGridTextBoxColumn8
        '
        Me.DataGridTextBoxColumn8.Format = ""
        Me.DataGridTextBoxColumn8.FormatInfo = Nothing
        Me.DataGridTextBoxColumn8.HeaderText = "Contact"
        Me.DataGridTextBoxColumn8.MappingName = "ContactName"
        Me.DataGridTextBoxColumn8.Width = 120
        '
        'DataGridTextBoxColumn17
        '
        Me.DataGridTextBoxColumn17.Format = ""
        Me.DataGridTextBoxColumn17.FormatInfo = Nothing
        Me.DataGridTextBoxColumn17.HeaderText = "Brief Summary"
        Me.DataGridTextBoxColumn17.MappingName = "BriefSummary"
        Me.DataGridTextBoxColumn17.Width = 350
        '
        'DataGridTextBoxColumn20
        '
        Me.DataGridTextBoxColumn20.Format = ""
        Me.DataGridTextBoxColumn20.FormatInfo = Nothing
        Me.DataGridTextBoxColumn20.HeaderText = "Center Staff"
        Me.DataGridTextBoxColumn20.MappingName = "StaffName"
        Me.DataGridTextBoxColumn20.Width = 150
        '
        'grdRecommendation
        '
        Me.grdRecommendation.AlternatingBackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.grdRecommendation.CaptionBackColor = System.Drawing.SystemColors.InactiveCaption
        Me.grdRecommendation.CaptionText = "Recommendations"
        Me.grdRecommendation.DataMember = "GetRecommendations"
        Me.grdRecommendation.DataSource = Me.DsCaseSecondary1
        Me.grdRecommendation.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.HelpProvider1.SetHelpString(Me.grdRecommendation, "")
        Me.grdRecommendation.Location = New System.Drawing.Point(3, 3)
        Me.grdRecommendation.Name = "grdRecommendation"
        Me.grdRecommendation.ParentRowsVisible = False
        Me.grdRecommendation.ReadOnly = True
        Me.grdRecommendation.RowHeaderWidth = 15
        Me.HelpProvider1.SetShowHelp(Me.grdRecommendation, True)
        Me.grdRecommendation.Size = New System.Drawing.Size(770, 184)
        Me.grdRecommendation.TabIndex = 197
        Me.grdRecommendation.TableStyles.AddRange(New System.Windows.Forms.DataGridTableStyle() {Me.DataGridTableStyle2})
        Me.grdRecommendation.Tag = "Recommendation"
        '
        'DataGridTableStyle2
        '
        Me.DataGridTableStyle2.DataGrid = Me.grdRecommendation
        Me.DataGridTableStyle2.GridColumnStyles.AddRange(New System.Windows.Forms.DataGridColumnStyle() {Me.DataGridTextBoxColumn9, Me.DataGridTextBoxColumn10, Me.DataGridTextBoxColumn11, Me.DataGridTextBoxColumn12, Me.DataGridTextBoxColumn21, Me.DataGridTextBoxColumn18, Me.DataGridTextBoxColumn19})
        Me.DataGridTableStyle2.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.DataGridTableStyle2.MappingName = "GetRecommendations"
        Me.DataGridTableStyle2.RowHeaderWidth = 15
        '
        'DataGridTextBoxColumn9
        '
        Me.DataGridTextBoxColumn9.Format = ""
        Me.DataGridTextBoxColumn9.FormatInfo = Nothing
        Me.DataGridTextBoxColumn9.HeaderText = "RecommendID"
        Me.DataGridTextBoxColumn9.MappingName = "RecommendID"
        Me.DataGridTextBoxColumn9.Width = 0
        '
        'DataGridTextBoxColumn10
        '
        Me.DataGridTextBoxColumn10.Format = ""
        Me.DataGridTextBoxColumn10.FormatInfo = Nothing
        Me.DataGridTextBoxColumn10.HeaderText = "ResourceNum"
        Me.DataGridTextBoxColumn10.MappingName = "ResourceNum"
        Me.DataGridTextBoxColumn10.Width = 0
        '
        'DataGridTextBoxColumn11
        '
        Me.DataGridTextBoxColumn11.Format = ""
        Me.DataGridTextBoxColumn11.FormatInfo = Nothing
        Me.DataGridTextBoxColumn11.HeaderText = "Resource Name"
        Me.DataGridTextBoxColumn11.MappingName = "ResourceName"
        Me.DataGridTextBoxColumn11.Width = 250
        '
        'DataGridTextBoxColumn12
        '
        Me.DataGridTextBoxColumn12.Format = ""
        Me.DataGridTextBoxColumn12.FormatInfo = Nothing
        Me.DataGridTextBoxColumn12.HeaderText = "Type"
        Me.DataGridTextBoxColumn12.MappingName = "Type"
        Me.DataGridTextBoxColumn12.Width = 75
        '
        'DataGridTextBoxColumn21
        '
        Me.DataGridTextBoxColumn21.Format = "d"
        Me.DataGridTextBoxColumn21.FormatInfo = Nothing
        Me.DataGridTextBoxColumn21.HeaderText = "Date"
        Me.DataGridTextBoxColumn21.MappingName = "RecommendDate"
        Me.DataGridTextBoxColumn21.Width = 75
        '
        'DataGridTextBoxColumn18
        '
        Me.DataGridTextBoxColumn18.Format = ""
        Me.DataGridTextBoxColumn18.FormatInfo = Nothing
        Me.DataGridTextBoxColumn18.HeaderText = "OrgNum"
        Me.DataGridTextBoxColumn18.MappingName = "OrgNum"
        Me.DataGridTextBoxColumn18.Width = 0
        '
        'DataGridTextBoxColumn19
        '
        Me.DataGridTextBoxColumn19.Format = ""
        Me.DataGridTextBoxColumn19.FormatInfo = Nothing
        Me.DataGridTextBoxColumn19.HeaderText = "Who Recommended"
        Me.DataGridTextBoxColumn19.MappingName = "WhoRecommended"
        Me.DataGridTextBoxColumn19.Width = 200
        '
        'grdMain
        '
        Me.grdMain.AlternatingBackColor = System.Drawing.Color.LightSteelBlue
        Me.grdMain.BackgroundColor = System.Drawing.SystemColors.Control
        Me.grdMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.grdMain.CaptionBackColor = System.Drawing.SystemColors.InactiveCaption
        Me.grdMain.DataMember = "SrchCases"
        Me.grdMain.DataSource = Me.DsSrchCase1
        Me.grdMain.ForeColor = System.Drawing.Color.ForestGreen
        Me.grdMain.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.HelpProvider1.SetHelpString(Me.grdMain, "hello")
        Me.grdMain.Location = New System.Drawing.Point(945, 276)
        Me.grdMain.Name = "grdMain"
        Me.grdMain.ParentRowsBackColor = System.Drawing.SystemColors.Window
        Me.grdMain.ParentRowsVisible = False
        Me.grdMain.ReadOnly = True
        Me.grdMain.RowHeaderWidth = 15
        Me.grdMain.SelectionBackColor = System.Drawing.SystemColors.Desktop
        Me.grdMain.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        Me.HelpProvider1.SetShowHelp(Me.grdMain, True)
        Me.grdMain.Size = New System.Drawing.Size(32, 73)
        Me.grdMain.TabIndex = 194
        Me.grdMain.TableStyles.AddRange(New System.Windows.Forms.DataGridTableStyle() {Me.DataGridTableStyle1})
        Me.grdMain.Tag = "Main"
        Me.ToolTip1.SetToolTip(Me.grdMain, "Click in grid, then press F1 for grid help")
        '
        'DsSrchCase1
        '
        Me.DsSrchCase1.DataSetName = "dsSrchCase"
        Me.DsSrchCase1.Locale = New System.Globalization.CultureInfo("en-US")
        Me.DsSrchCase1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'DataGridTableStyle1
        '
        Me.DataGridTableStyle1.AlternatingBackColor = System.Drawing.Color.FromArgb(CType(CType(204, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.DataGridTableStyle1.DataGrid = Me.grdMain
        Me.DataGridTableStyle1.ForeColor = System.Drawing.Color.DarkGreen
        Me.DataGridTableStyle1.GridColumnStyles.AddRange(New System.Windows.Forms.DataGridColumnStyle() {Me.DataGridTextBoxColumn1, Me.DataGridTextBoxColumn16, Me.DataGridTextBoxColumn2, Me.DataGridTextBoxColumn3, Me.DataGridTextBoxColumn4, Me.DataGridTextBoxColumn5, Me.DataGridTextBoxColumn14, Me.DataGridTextBoxColumn13, Me.DataGridTextBoxColumn28})
        Me.DataGridTableStyle1.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.DataGridTableStyle1.MappingName = "srchCases"
        Me.DataGridTableStyle1.RowHeaderWidth = 15
        Me.DataGridTableStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        '
        'DataGridTextBoxColumn1
        '
        Me.DataGridTextBoxColumn1.Format = ""
        Me.DataGridTextBoxColumn1.FormatInfo = Nothing
        Me.DataGridTextBoxColumn1.HeaderText = "CaseID"
        Me.DataGridTextBoxColumn1.MappingName = "CaseID"
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
        Me.DataGridTextBoxColumn2.MappingName = "OrgandCity"
        Me.DataGridTextBoxColumn2.Width = 175
        '
        'DataGridTextBoxColumn3
        '
        Me.DataGridTextBoxColumn3.Format = ""
        Me.DataGridTextBoxColumn3.FormatInfo = Nothing
        Me.DataGridTextBoxColumn3.HeaderText = "CaseName"
        Me.DataGridTextBoxColumn3.MappingName = "CaseName"
        Me.DataGridTextBoxColumn3.Width = 150
        '
        'DataGridTextBoxColumn4
        '
        Me.DataGridTextBoxColumn4.Format = ""
        Me.DataGridTextBoxColumn4.FormatInfo = Nothing
        Me.DataGridTextBoxColumn4.HeaderText = "Status"
        Me.DataGridTextBoxColumn4.MappingName = "Status"
        Me.DataGridTextBoxColumn4.Width = 120
        '
        'DataGridTextBoxColumn5
        '
        Me.DataGridTextBoxColumn5.Format = "d"
        Me.DataGridTextBoxColumn5.FormatInfo = Nothing
        Me.DataGridTextBoxColumn5.HeaderText = "Last Call  "
        Me.DataGridTextBoxColumn5.MappingName = "LastCall"
        Me.DataGridTextBoxColumn5.NullText = ""
        Me.DataGridTextBoxColumn5.Width = 75
        '
        'DataGridTextBoxColumn14
        '
        Me.DataGridTextBoxColumn14.Format = ""
        Me.DataGridTextBoxColumn14.FormatInfo = Nothing
        Me.DataGridTextBoxColumn14.HeaderText = "Phone"
        Me.DataGridTextBoxColumn14.MappingName = "Phone"
        Me.DataGridTextBoxColumn14.Width = 0
        '
        'DataGridTextBoxColumn13
        '
        Me.DataGridTextBoxColumn13.Format = ""
        Me.DataGridTextBoxColumn13.FormatInfo = Nothing
        Me.DataGridTextBoxColumn13.HeaderText = "CaseMgr"
        Me.DataGridTextBoxColumn13.MappingName = "CaseMgr"
        Me.DataGridTextBoxColumn13.Width = 75
        '
        'DataGridTextBoxColumn28
        '
        Me.DataGridTextBoxColumn28.Format = ""
        Me.DataGridTextBoxColumn28.FormatInfo = Nothing
        Me.DataGridTextBoxColumn28.HeaderText = "ResourceReqrd"
        Me.DataGridTextBoxColumn28.MappingName = "Resources"
        Me.DataGridTextBoxColumn28.Width = 60
        '
        'btnHelp
        '
        Me.btnHelp.BackColor = System.Drawing.SystemColors.Control
        Me.btnHelp.Font = New System.Drawing.Font("Microsoft Sans Serif", 22.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnHelp.ForeColor = System.Drawing.SystemColors.Control
        Me.btnHelp.Image = CType(resources.GetObject("btnHelp.Image"), System.Drawing.Image)
        Me.btnHelp.Location = New System.Drawing.Point(860, 5)
        Me.btnHelp.Name = "btnHelp"
        Me.btnHelp.Size = New System.Drawing.Size(25, 25)
        Me.btnHelp.TabIndex = 236
        Me.btnHelp.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.ToolTip1.SetToolTip(Me.btnHelp, "Help: How to use this Search page.")
        Me.btnHelp.UseVisualStyleBackColor = False
        '
        'btnNew
        '
        Me.btnNew.BackColor = System.Drawing.Color.PaleGoldenrod
        Me.btnNew.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnNew.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnNew.Image = CType(resources.GetObject("btnNew.Image"), System.Drawing.Image)
        Me.btnNew.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnNew.Location = New System.Drawing.Point(769, 5)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(80, 40)
        Me.btnNew.TabIndex = 231
        Me.btnNew.Text = "New Conversation"
        Me.btnNew.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ToolTip1.SetToolTip(Me.btnNew, "Add New Conversation")
        Me.btnNew.UseVisualStyleBackColor = False
        '
        'GroupBox2
        '
        Me.GroupBox2.BackColor = System.Drawing.SystemColors.InactiveBorder
        Me.GroupBox2.Controls.Add(Me.Label12)
        Me.GroupBox2.Controls.Add(Me.Label13)
        Me.GroupBox2.Controls.Add(Me.RadioButton4)
        Me.GroupBox2.Controls.Add(Me.Label14)
        Me.GroupBox2.Controls.Add(Me.Label15)
        Me.GroupBox2.Controls.Add(Me.RadioButton5)
        Me.GroupBox2.Controls.Add(Me.RadioButton6)
        Me.GroupBox2.Location = New System.Drawing.Point(8, 403)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(173, 104)
        Me.GroupBox2.TabIndex = 4
        Me.GroupBox2.TabStop = False
        Me.ToolTip1.SetToolTip(Me.GroupBox2, "Uncheck this box to speed up the search.")
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.ForeColor = System.Drawing.Color.Maroon
        Me.Label12.Location = New System.Drawing.Point(2, 14)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(72, 13)
        Me.Label12.TabIndex = 214
        Me.Label12.Text = "Related Items"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(143, 82)
        Me.Label13.MinimumSize = New System.Drawing.Size(20, 0)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(20, 13)
        Me.Label13.TabIndex = 213
        '
        'RadioButton4
        '
        Me.RadioButton4.Location = New System.Drawing.Point(24, 78)
        Me.RadioButton4.Name = "RadioButton4"
        Me.RadioButton4.Size = New System.Drawing.Size(120, 20)
        Me.RadioButton4.TabIndex = 212
        Me.RadioButton4.Text = "Grants"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(143, 58)
        Me.Label14.MinimumSize = New System.Drawing.Size(20, 0)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(20, 13)
        Me.Label14.TabIndex = 211
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(143, 35)
        Me.Label15.MinimumSize = New System.Drawing.Size(20, 0)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(20, 13)
        Me.Label15.TabIndex = 9
        '
        'RadioButton5
        '
        Me.RadioButton5.Checked = True
        Me.RadioButton5.Location = New System.Drawing.Point(24, 30)
        Me.RadioButton5.Name = "RadioButton5"
        Me.RadioButton5.Size = New System.Drawing.Size(106, 20)
        Me.RadioButton5.TabIndex = 6
        Me.RadioButton5.TabStop = True
        Me.RadioButton5.Text = "Conversations"
        '
        'RadioButton6
        '
        Me.RadioButton6.Location = New System.Drawing.Point(24, 45)
        Me.RadioButton6.Name = "RadioButton6"
        Me.RadioButton6.Size = New System.Drawing.Size(120, 37)
        Me.RadioButton6.TabIndex = 7
        Me.RadioButton6.Text = "Resource Recommendations"
        '
        'ListBox1
        '
        Me.ListBox1.Location = New System.Drawing.Point(8, 80)
        Me.ListBox1.Name = "ListBox1"
        Me.ListBox1.Size = New System.Drawing.Size(173, 199)
        Me.ListBox1.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.ListBox1, "Case Status")
        '
        'grdvwMain
        '
        Me.grdvwMain.AllowUserToAddRows = False
        Me.grdvwMain.AllowUserToDeleteRows = False
        Me.grdvwMain.AllowUserToOrderColumns = True
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(204, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.grdvwMain.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.grdvwMain.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdvwMain.AutoGenerateColumns = False
        Me.grdvwMain.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.grdvwMain.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdvwMain.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.grdvwMain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdvwMain.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.CaseIDColumn, Me.OrgNumColumn, Me.OrgandCityColumn, Me.CaseNameColumn, Me.StatusDataGridViewTextBoxColumn, Me.StatusNumDataGridViewTextBoxColumn, Me.LastCallDataGridViewTextBoxColumn, Me.CaseMgrDataGridViewTextBoxColumn, Me.PhoneColumn, Me.ResourcesDataGridViewTextBoxColumn, Me.FromCRGColumn, Me.CRGConsultingOrgColumn})
        Me.grdvwMain.DataMember = "SrchCases"
        Me.grdvwMain.DataSource = Me.DsSrchCase1
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.grdvwMain.DefaultCellStyle = DataGridViewCellStyle4
        Me.grdvwMain.Location = New System.Drawing.Point(212, 67)
        Me.grdvwMain.Name = "grdvwMain"
        Me.grdvwMain.ReadOnly = True
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdvwMain.RowHeadersDefaultCellStyle = DataGridViewCellStyle5
        Me.grdvwMain.RowHeadersWidth = 20
        Me.grdvwMain.RowTemplate.Height = 21
        Me.grdvwMain.Size = New System.Drawing.Size(860, 271)
        Me.grdvwMain.TabIndex = 239
        Me.grdvwMain.Tag = "Main"
        Me.ToolTip1.SetToolTip(Me.grdvwMain, "use ctrl to select multiple rows in this grid")
        '
        'CaseIDColumn
        '
        Me.CaseIDColumn.DataPropertyName = "CaseID"
        Me.CaseIDColumn.HeaderText = "CaseID"
        Me.CaseIDColumn.Name = "CaseIDColumn"
        Me.CaseIDColumn.ReadOnly = True
        Me.CaseIDColumn.Visible = False
        Me.CaseIDColumn.Width = 5
        '
        'OrgNumColumn
        '
        Me.OrgNumColumn.DataPropertyName = "OrgNum"
        Me.OrgNumColumn.HeaderText = "OrgNum"
        Me.OrgNumColumn.Name = "OrgNumColumn"
        Me.OrgNumColumn.ReadOnly = True
        Me.OrgNumColumn.Visible = False
        '
        'OrgandCityColumn
        '
        Me.OrgandCityColumn.DataPropertyName = "OrgandCity"
        Me.OrgandCityColumn.HeaderText = "Organization"
        Me.OrgandCityColumn.Name = "OrgandCityColumn"
        Me.OrgandCityColumn.ReadOnly = True
        Me.OrgandCityColumn.Width = 165
        '
        'CaseNameColumn
        '
        Me.CaseNameColumn.DataPropertyName = "CaseName"
        Me.CaseNameColumn.HeaderText = "CaseName"
        Me.CaseNameColumn.Name = "CaseNameColumn"
        Me.CaseNameColumn.ReadOnly = True
        Me.CaseNameColumn.Width = 150
        '
        'StatusDataGridViewTextBoxColumn
        '
        Me.StatusDataGridViewTextBoxColumn.DataPropertyName = "Status"
        Me.StatusDataGridViewTextBoxColumn.HeaderText = "Status"
        Me.StatusDataGridViewTextBoxColumn.Name = "StatusDataGridViewTextBoxColumn"
        Me.StatusDataGridViewTextBoxColumn.ReadOnly = True
        Me.StatusDataGridViewTextBoxColumn.Width = 120
        '
        'StatusNumDataGridViewTextBoxColumn
        '
        Me.StatusNumDataGridViewTextBoxColumn.DataPropertyName = "StatusNum"
        Me.StatusNumDataGridViewTextBoxColumn.HeaderText = "StatusNum"
        Me.StatusNumDataGridViewTextBoxColumn.Name = "StatusNumDataGridViewTextBoxColumn"
        Me.StatusNumDataGridViewTextBoxColumn.ReadOnly = True
        Me.StatusNumDataGridViewTextBoxColumn.Visible = False
        '
        'LastCallDataGridViewTextBoxColumn
        '
        Me.LastCallDataGridViewTextBoxColumn.DataPropertyName = "LastCall"
        DataGridViewCellStyle3.Format = "d"
        DataGridViewCellStyle3.NullValue = Nothing
        Me.LastCallDataGridViewTextBoxColumn.DefaultCellStyle = DataGridViewCellStyle3
        Me.LastCallDataGridViewTextBoxColumn.HeaderText = "LastCall"
        Me.LastCallDataGridViewTextBoxColumn.Name = "LastCallDataGridViewTextBoxColumn"
        Me.LastCallDataGridViewTextBoxColumn.ReadOnly = True
        Me.LastCallDataGridViewTextBoxColumn.Width = 75
        '
        'CaseMgrDataGridViewTextBoxColumn
        '
        Me.CaseMgrDataGridViewTextBoxColumn.DataPropertyName = "CaseMgr"
        Me.CaseMgrDataGridViewTextBoxColumn.HeaderText = "CaseMgr"
        Me.CaseMgrDataGridViewTextBoxColumn.Name = "CaseMgrDataGridViewTextBoxColumn"
        Me.CaseMgrDataGridViewTextBoxColumn.ReadOnly = True
        Me.CaseMgrDataGridViewTextBoxColumn.Width = 125
        '
        'PhoneColumn
        '
        Me.PhoneColumn.DataPropertyName = "Phone"
        Me.PhoneColumn.HeaderText = "Phone"
        Me.PhoneColumn.Name = "PhoneColumn"
        Me.PhoneColumn.ReadOnly = True
        Me.PhoneColumn.Visible = False
        '
        'ResourcesDataGridViewTextBoxColumn
        '
        Me.ResourcesDataGridViewTextBoxColumn.DataPropertyName = "Resources"
        Me.ResourcesDataGridViewTextBoxColumn.HeaderText = "Resources Recommended"
        Me.ResourcesDataGridViewTextBoxColumn.Name = "ResourcesDataGridViewTextBoxColumn"
        Me.ResourcesDataGridViewTextBoxColumn.ReadOnly = True
        Me.ResourcesDataGridViewTextBoxColumn.Width = 60
        '
        'FromCRGColumn
        '
        Me.FromCRGColumn.DataPropertyName = "FromCRG"
        Me.FromCRGColumn.HeaderText = "From CRG"
        Me.FromCRGColumn.Name = "FromCRGColumn"
        Me.FromCRGColumn.ReadOnly = True
        Me.FromCRGColumn.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.FromCRGColumn.Width = 40
        '
        'CRGConsultingOrgColumn
        '
        Me.CRGConsultingOrgColumn.DataPropertyName = "CRGConsultingOrg"
        Me.CRGConsultingOrgColumn.HeaderText = "CRGConsultingOrg"
        Me.CRGConsultingOrgColumn.Name = "CRGConsultingOrgColumn"
        Me.CRGConsultingOrgColumn.ReadOnly = True
        '
        'cboCRG
        '
        Me.cboCRG.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboCRG.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboCRG.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCRG.DropDownWidth = 500
        Me.cboCRG.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cboCRG.ItemHeight = 13
        Me.cboCRG.Location = New System.Drawing.Point(3, 378)
        Me.cboCRG.Name = "cboCRG"
        Me.cboCRG.RestrictContentToListItems = True
        Me.cboCRG.Size = New System.Drawing.Size(177, 21)
        Me.cboCRG.TabIndex = 3
        Me.ToolTip1.SetToolTip(Me.cboCRG, "Right click to search.  Escape twice to select none.")
        '
        'cboStaff
        '
        Me.cboStaff.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboStaff.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboStaff.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cboStaff.FormattingEnabled = True
        Me.cboStaff.Location = New System.Drawing.Point(7, 8)
        Me.cboStaff.Name = "cboStaff"
        Me.cboStaff.RestrictContentToListItems = True
        Me.cboStaff.Size = New System.Drawing.Size(174, 21)
        Me.cboStaff.TabIndex = 0
        Me.cboStaff.Tag = "Staff Name"
        Me.ToolTip1.SetToolTip(Me.cboStaff, "Staff Name, default: Case Manager")
        '
        'cboCaller
        '
        Me.cboCaller.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboCaller.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboCaller.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cboCaller.ItemHeight = 13
        Me.cboCaller.Items.AddRange(New Object() {"Case Manager", "Caller", "Staff Conversations"})
        Me.cboCaller.Location = New System.Drawing.Point(8, 331)
        Me.cboCaller.Name = "cboCaller"
        Me.cboCaller.RestrictContentToListItems = True
        Me.cboCaller.Size = New System.Drawing.Size(173, 21)
        Me.cboCaller.TabIndex = 2
        Me.cboCaller.Tag = "Scope"
        Me.ToolTip1.SetToolTip(Me.cboCaller, "Was Staff Name the Case Manager or a Caller in someone else's case?")
        '
        'MainMenu1
        '
        Me.MainMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem1, Me.MenuItem2, Me.MenuItem3, Me.MenuItem4, Me.mmHelp})
        '
        'MenuItem1
        '
        Me.MenuItem1.Index = 0
        Me.MenuItem1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.miNew, Me.miCloseForm})
        Me.MenuItem1.Text = "File"
        '
        'miNew
        '
        Me.miNew.Index = 0
        Me.miNew.Text = "New Conversation"
        '
        'miCloseForm
        '
        Me.miCloseForm.Index = 1
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
        Me.MenuItem3.Index = 2
        Me.MenuItem3.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.miGotoCase, Me.miGotoConversation, Me.miGotoGrant, Me.miGotoRecommendation})
        Me.MenuItem3.Text = "Go to"
        Me.MenuItem3.Visible = False
        '
        'miGotoCase
        '
        Me.miGotoCase.Index = 0
        Me.miGotoCase.Text = "Case"
        '
        'miGotoConversation
        '
        Me.miGotoConversation.Index = 1
        Me.miGotoConversation.Text = "Conversation"
        '
        'miGotoGrant
        '
        Me.miGotoGrant.Enabled = False
        Me.miGotoGrant.Index = 2
        Me.miGotoGrant.Text = "Grant"
        '
        'miGotoRecommendation
        '
        Me.miGotoRecommendation.Index = 3
        Me.miGotoRecommendation.Text = "Recommendation"
        '
        'MenuItem4
        '
        Me.MenuItem4.Index = 3
        Me.MenuItem4.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.miPrintConv, Me.miPrintContact})
        Me.MenuItem4.Text = "Reports"
        '
        'miPrintConv
        '
        Me.miPrintConv.Index = 0
        Me.miPrintConv.Text = "Print Case Report"
        '
        'miPrintContact
        '
        Me.miPrintContact.Enabled = False
        Me.miPrintContact.Index = 1
        Me.miPrintContact.Text = "Print Contact Info"
        '
        'mmHelp
        '
        Me.mmHelp.Index = 4
        Me.mmHelp.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.miHelp, Me.miDefinitions})
        Me.mmHelp.Text = "&Help"
        '
        'miHelp
        '
        Me.miHelp.Index = 0
        Me.miHelp.Text = "Help"
        '
        'miDefinitions
        '
        Me.miDefinitions.Index = 1
        Me.miDefinitions.Text = "Status Definitions"
        '
        'sqlspSrchCases
        '
        Me.sqlspSrchCases.CommandText = "[SrchCases]"
        Me.sqlspSrchCases.CommandType = System.Data.CommandType.StoredProcedure
        Me.sqlspSrchCases.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, False, CType(0, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing), New System.Data.SqlClient.SqlParameter("@CaseMgrFld", System.Data.SqlDbType.NVarChar), New System.Data.SqlClient.SqlParameter("@CaseMgrVal", System.Data.SqlDbType.Int, 4, "CaseMgrNum"), New System.Data.SqlClient.SqlParameter("@CRG", System.Data.SqlDbType.VarChar, 100), New System.Data.SqlClient.SqlParameter("@CaseStatus", System.Data.SqlDbType.Int, 4, "StatusNum"), New System.Data.SqlClient.SqlParameter("@Today", System.Data.SqlDbType.DateTime)})
        '
        'sqlspGetConversations
        '
        Me.sqlspGetConversations.CommandText = "[GetConversations]"
        Me.sqlspGetConversations.CommandType = System.Data.CommandType.StoredProcedure
        Me.sqlspGetConversations.Connection = Me.SqlConnection1
        Me.sqlspGetConversations.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, False, CType(0, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing), New System.Data.SqlClient.SqlParameter("@IDVal", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, "2300"), New System.Data.SqlClient.SqlParameter("@IDFld", System.Data.SqlDbType.VarChar, 30, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, "Case")})
        '
        'SqlConnection1
        '
        Me.SqlConnection1.ConnectionString = "Data Source=SOLOMON2008\SOLOMON2008;Initial Catalog=InfoCtr_be;Integrated Securit" & _
    "y=True"
        Me.SqlConnection1.FireInfoMessageEventOnUserErrors = False
        '
        'sqlspGetRecommendations
        '
        Me.sqlspGetRecommendations.CommandText = "[GetResRecommendation]"
        Me.sqlspGetRecommendations.CommandType = System.Data.CommandType.StoredProcedure
        Me.sqlspGetRecommendations.Connection = Me.SqlConnection1
        Me.sqlspGetRecommendations.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, False, CType(0, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing), New System.Data.SqlClient.SqlParameter("@IDVal", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, "2300"), New System.Data.SqlClient.SqlParameter("@IDFld", System.Data.SqlDbType.VarChar, 30, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, "Case")})
        '
        'sqlspGetGrants
        '
        Me.sqlspGetGrants.CommandText = "[GetGrants]"
        Me.sqlspGetGrants.CommandType = System.Data.CommandType.StoredProcedure
        Me.sqlspGetGrants.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, False, CType(0, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing), New System.Data.SqlClient.SqlParameter("@IDVal", System.Data.SqlDbType.Int, 4), New System.Data.SqlClient.SqlParameter("@IDFld", System.Data.SqlDbType.VarChar, 30, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, "CaseNum")})
        '
        'fldOrgNum
        '
        Me.fldOrgNum.BackColor = System.Drawing.SystemColors.Control
        Me.fldOrgNum.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.fldOrgNum.Enabled = False
        Me.fldOrgNum.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fldOrgNum.ForeColor = System.Drawing.SystemColors.GrayText
        Me.fldOrgNum.Location = New System.Drawing.Point(1024, 345)
        Me.fldOrgNum.Name = "fldOrgNum"
        Me.fldOrgNum.Size = New System.Drawing.Size(35, 14)
        Me.fldOrgNum.TabIndex = 199
        Me.fldOrgNum.Text = "OrgID"
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlDark
        Me.Label4.Location = New System.Drawing.Point(970, 345)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(35, 14)
        Me.Label4.TabIndex = 227
        Me.Label4.Text = "Org #"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'pnlSecGrids
        '
        Me.pnlSecGrids.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.pnlSecGrids.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pnlSecGrids.Controls.Add(Me.grdConversation)
        Me.pnlSecGrids.Controls.Add(Me.grdRecommendation)
        Me.pnlSecGrids.Controls.Add(Me.grdGrant)
        Me.pnlSecGrids.Location = New System.Drawing.Point(241, 368)
        Me.pnlSecGrids.Name = "pnlSecGrids"
        Me.pnlSecGrids.Size = New System.Drawing.Size(791, 203)
        Me.pnlSecGrids.TabIndex = 233
        '
        'grdGrant
        '
        Me.grdGrant.AlternatingBackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.grdGrant.CaptionBackColor = System.Drawing.SystemColors.InactiveCaption
        Me.grdGrant.CaptionText = "Grant"
        Me.grdGrant.DataMember = "GetGrants"
        Me.grdGrant.DataSource = Me.DsCaseSecondary1
        Me.grdGrant.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdGrant.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.grdGrant.Location = New System.Drawing.Point(3, 3)
        Me.grdGrant.Name = "grdGrant"
        Me.grdGrant.ParentRowsVisible = False
        Me.grdGrant.ReadOnly = True
        Me.grdGrant.RowHeaderWidth = 15
        Me.grdGrant.Size = New System.Drawing.Size(770, 184)
        Me.grdGrant.TabIndex = 196
        Me.grdGrant.TableStyles.AddRange(New System.Windows.Forms.DataGridTableStyle() {Me.DataGridTableStyle3})
        Me.grdGrant.Tag = "Grant"
        Me.grdGrant.Visible = False
        '
        'DataGridTableStyle3
        '
        Me.DataGridTableStyle3.AlternatingBackColor = System.Drawing.Color.FromArgb(CType(CType(225, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.DataGridTableStyle3.DataGrid = Me.grdGrant
        Me.DataGridTableStyle3.GridColumnStyles.AddRange(New System.Windows.Forms.DataGridColumnStyle() {Me.DataGridTextBoxColumn22, Me.DataGridTextBoxColumn23, Me.DataGridTextBoxColumn24, Me.DataGridTextBoxColumn25, Me.DataGridTextBoxColumn29, Me.DataGridTextBoxColumn27})
        Me.DataGridTableStyle3.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.DataGridTableStyle3.MappingName = "GetGrants"
        Me.DataGridTableStyle3.RowHeaderWidth = 15
        '
        'DataGridTextBoxColumn22
        '
        Me.DataGridTextBoxColumn22.Format = ""
        Me.DataGridTextBoxColumn22.FormatInfo = Nothing
        Me.DataGridTextBoxColumn22.HeaderText = "GrantID"
        Me.DataGridTextBoxColumn22.MappingName = "GrantIDtxt"
        Me.DataGridTextBoxColumn22.Width = 0
        '
        'DataGridTextBoxColumn23
        '
        Me.DataGridTextBoxColumn23.Format = ""
        Me.DataGridTextBoxColumn23.FormatInfo = Nothing
        Me.DataGridTextBoxColumn23.HeaderText = "Type"
        Me.DataGridTextBoxColumn23.MappingName = "TypeofGrant"
        Me.DataGridTextBoxColumn23.Width = 200
        '
        'DataGridTextBoxColumn24
        '
        Me.DataGridTextBoxColumn24.Format = "$##,###"
        Me.DataGridTextBoxColumn24.FormatInfo = Nothing
        Me.DataGridTextBoxColumn24.HeaderText = "Amount"
        Me.DataGridTextBoxColumn24.MappingName = "Amount"
        Me.DataGridTextBoxColumn24.Width = 75
        '
        'DataGridTextBoxColumn25
        '
        Me.DataGridTextBoxColumn25.Format = ""
        Me.DataGridTextBoxColumn25.FormatInfo = Nothing
        Me.DataGridTextBoxColumn25.HeaderText = "Latest Activity"
        Me.DataGridTextBoxColumn25.MappingName = "GrantLatest"
        Me.DataGridTextBoxColumn25.Width = 200
        '
        'DataGridTextBoxColumn29
        '
        Me.DataGridTextBoxColumn29.Format = ""
        Me.DataGridTextBoxColumn29.FormatInfo = Nothing
        Me.DataGridTextBoxColumn29.HeaderText = "Next Report Due"
        Me.DataGridTextBoxColumn29.MappingName = "NextReportDue"
        Me.DataGridTextBoxColumn29.Width = 120
        '
        'DataGridTextBoxColumn27
        '
        Me.DataGridTextBoxColumn27.Format = ""
        Me.DataGridTextBoxColumn27.FormatInfo = Nothing
        Me.DataGridTextBoxColumn27.HeaderText = "Congr. Final Rpt"
        Me.DataGridTextBoxColumn27.MappingName = "CongregationFinalReport"
        Me.DataGridTextBoxColumn27.Width = 120
        '
        'SqlSelectCommand1
        '
        Me.SqlSelectCommand1.CommandText = "[GetResRecommendation]"
        Me.SqlSelectCommand1.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlSelectCommand1.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, False, CType(0, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing), New System.Data.SqlClient.SqlParameter("@IDVal", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "CaseNum", System.Data.DataRowVersion.Current, "2300"), New System.Data.SqlClient.SqlParameter("@IDFld", System.Data.SqlDbType.VarChar, 30, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, "Org")})
        '
        'lblOverdue
        '
        Me.lblOverdue.BackColor = System.Drawing.Color.Yellow
        Me.lblOverdue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblOverdue.CausesValidation = False
        Me.lblOverdue.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblOverdue.ForeColor = System.Drawing.Color.Red
        Me.lblOverdue.Location = New System.Drawing.Point(250, 2)
        Me.lblOverdue.Name = "lblOverdue"
        Me.lblOverdue.Size = New System.Drawing.Size(485, 21)
        Me.lblOverdue.TabIndex = 235
        Me.lblOverdue.Text = "overdue grants"
        Me.lblOverdue.Visible = False
        '
        'lblSelectionID
        '
        Me.lblSelectionID.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSelectionID.ForeColor = System.Drawing.SystemColors.ControlDark
        Me.lblSelectionID.Location = New System.Drawing.Point(717, 345)
        Me.lblSelectionID.Name = "lblSelectionID"
        Me.lblSelectionID.Size = New System.Drawing.Size(50, 14)
        Me.lblSelectionID.TabIndex = 238
        Me.lblSelectionID.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'fldSelectionNum
        '
        Me.fldSelectionNum.BackColor = System.Drawing.SystemColors.Control
        Me.fldSelectionNum.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.fldSelectionNum.Enabled = False
        Me.fldSelectionNum.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fldSelectionNum.ForeColor = System.Drawing.SystemColors.GrayText
        Me.fldSelectionNum.Location = New System.Drawing.Point(785, 345)
        Me.fldSelectionNum.MinimumSize = New System.Drawing.Size(100, 0)
        Me.fldSelectionNum.Name = "fldSelectionNum"
        Me.fldSelectionNum.Size = New System.Drawing.Size(100, 14)
        Me.fldSelectionNum.TabIndex = 237
        Me.fldSelectionNum.Text = "ID"
        '
        'lblMaino
        '
        Me.lblMaino.AutoSize = True
        Me.lblMaino.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.lblMaino.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMaino.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblMaino.Location = New System.Drawing.Point(143, 5)
        Me.lblMaino.MinimumSize = New System.Drawing.Size(0, 18)
        Me.lblMaino.Name = "lblMaino"
        Me.lblMaino.Size = New System.Drawing.Size(47, 18)
        Me.lblMaino.TabIndex = 240
        Me.lblMaino.Text = "lblMain"
        '
        'lblMainGrid
        '
        Me.lblMainGrid.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblMainGrid.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.lblMainGrid.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMainGrid.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblMainGrid.Location = New System.Drawing.Point(212, 49)
        Me.lblMainGrid.MinimumSize = New System.Drawing.Size(0, 18)
        Me.lblMainGrid.Name = "lblMainGrid"
        Me.lblMainGrid.Size = New System.Drawing.Size(860, 18)
        Me.lblMainGrid.TabIndex = 241
        Me.lblMainGrid.Text = "Cases"
        Me.lblMainGrid.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label8
        '
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(212, 28)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(95, 17)
        Me.Label8.TabIndex = 242
        Me.Label8.Text = "Results"
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.SystemColors.InactiveBorder
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Controls.Add(Me.cboFromCRG)
        Me.Panel2.Controls.Add(Me.Label7)
        Me.Panel2.Controls.Add(Me.Label6)
        Me.Panel2.Controls.Add(Me.Label5)
        Me.Panel2.Controls.Add(Me.cboCRG)
        Me.Panel2.Controls.Add(Me.cboStaff)
        Me.Panel2.Controls.Add(Me.GroupBox1)
        Me.Panel2.Controls.Add(Me.lstStatus)
        Me.Panel2.Controls.Add(Me.cboCaller)
        Me.Panel2.Controls.Add(Me.btnSearch)
        Me.Panel2.Controls.Add(Me.CheckBox1)
        Me.Panel2.Controls.Add(Me.Label9)
        Me.Panel2.Controls.Add(Me.Label10)
        Me.Panel2.Controls.Add(Me.Label11)
        Me.Panel2.Controls.Add(Me.GroupBox2)
        Me.Panel2.Controls.Add(Me.ListBox1)
        Me.Panel2.Location = New System.Drawing.Point(12, 49)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(192, 519)
        Me.Panel2.TabIndex = 243
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.Location = New System.Drawing.Point(19, 290)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(154, 17)
        Me.CheckBox1.TabIndex = 215
        Me.CheckBox1.Text = "Initiated from CRG Website"
        Me.CheckBox1.UseVisualStyleBackColor = True
        Me.CheckBox1.Visible = False
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.ForeColor = System.Drawing.Color.Maroon
        Me.Label9.Location = New System.Drawing.Point(6, 316)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(75, 13)
        Me.Label9.TabIndex = 214
        Me.Label9.Text = "Scope of Staff"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.ForeColor = System.Drawing.Color.Maroon
        Me.Label10.Location = New System.Drawing.Point(10, 65)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(64, 13)
        Me.Label10.TabIndex = 213
        Me.Label10.Text = "Case Status"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.ForeColor = System.Drawing.Color.Maroon
        Me.Label11.Location = New System.Drawing.Point(7, 363)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(58, 13)
        Me.Label11.TabIndex = 212
        Me.Label11.Text = "CRG Issue"
        '
        'frmSrchCase
        '
        Me.AcceptButton = Me.btnSearch
        Me.ClientSize = New System.Drawing.Size(1084, 619)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.lblMainGrid)
        Me.Controls.Add(Me.grdvwMain)
        Me.Controls.Add(Me.lblSelectionID)
        Me.Controls.Add(Me.chkDetail)
        Me.Controls.Add(Me.fldSelectionNum)
        Me.Controls.Add(Me.btnHelp)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.lblOverdue)
        Me.Controls.Add(Me.pnlSecGrids)
        Me.Controls.Add(Me.btnNew)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.fldOrgNum)
        Me.Controls.Add(Me.StatusBar1)
        Me.Controls.Add(Me.Label3)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Menu = Me.MainMenu1
        Me.Name = "frmSrchCase"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Tag = "SRCH CASE"
        Me.Text = "FIND CASE"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.StatusBarPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.StatusBarPanelID, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.StatusBarPanel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdConversation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsCaseSecondary1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdRecommendation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdMain, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsSrchCase1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.grdvwMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlSecGrids.ResumeLayout(False)
        CType(Me.grdGrant, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

#Region "Load"

    'LOAD
    Private Sub frmSrchCase_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles MyBase.Load

SET_DEFAULTS:
        usrRadio.Append("Conversations")
        strbActiveGrid.Append("Conversations")
DATA_ADAPTORS:
        daM.SelectCommand = Me.sqlspSrchCases
        daM.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "SrchCases", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("CaseID", "CaseID"), New System.Data.Common.DataColumnMapping("OrgNum", "OrgNum"), New System.Data.Common.DataColumnMapping("CaseName", "CaseName"), New System.Data.Common.DataColumnMapping("OrgandCity", "OrgandCity"), New System.Data.Common.DataColumnMapping("Status", "Status"), New System.Data.Common.DataColumnMapping("StatusNum", "StatusNum"), New System.Data.Common.DataColumnMapping("LastCall", "LastCall"), New System.Data.Common.DataColumnMapping("CaseMgr", "CaseMgr"), New System.Data.Common.DataColumnMapping("OrgPhone", "Phone"), New System.Data.Common.DataColumnMapping("OpenGrants", "OpenGrants"), New System.Data.Common.DataColumnMapping("NoResources", "NoResources"), New System.Data.Common.DataColumnMapping("Resources", "Resources")})})
        daM.SelectCommand.Connection = sc

        daSecond1.SelectCommand = Me.sqlspGetConversations
        daSecond1.SelectCommand.Connection = sc
        daSecond1.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "GetConversations", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("CaseNum", "CaseNum"), New System.Data.Common.DataColumnMapping("ContactNum", "ContactNum"), New System.Data.Common.DataColumnMapping("ConversDate", "ConversDate"), New System.Data.Common.DataColumnMapping("Contact", "Contact"), New System.Data.Common.DataColumnMapping("BriefSummary", "BriefSummary"), New System.Data.Common.DataColumnMapping("StaffName", "StaffName")})})

        daSecond2.SelectCommand = Me.SqlSelectCommand1
        daSecond2.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "GetRecommendations", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("RecommendID", "RecommendID"), New System.Data.Common.DataColumnMapping("ResourceNum", "ResourceNum"), New System.Data.Common.DataColumnMapping("CaseNum", "CaseNum"), New System.Data.Common.DataColumnMapping("ConversNum", "ConversNum"), New System.Data.Common.DataColumnMapping("GrantNum", "GrantNum"), New System.Data.Common.DataColumnMapping("ResourceName", "ResourceName"), New System.Data.Common.DataColumnMapping("Type", "Type"), New System.Data.Common.DataColumnMapping("Active", "Active"), New System.Data.Common.DataColumnMapping("RecommendDate", "RecommendDate"), New System.Data.Common.DataColumnMapping("Used", "Used"), New System.Data.Common.DataColumnMapping("OrgNum", "OrgNum"), New System.Data.Common.DataColumnMapping("RecommendStaffNum", "RecommendStaffNum"), New System.Data.Common.DataColumnMapping("OrgName", "OrgName"), New System.Data.Common.DataColumnMapping("CaseName", "CaseName"), New System.Data.Common.DataColumnMapping("WhoRecommended", "WhoRecommended")})})
        daSecond2.SelectCommand.Connection = sc

        daSecond3.SelectCommand = Me.sqlspGetGrants
        daSecond3.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "tblGrant", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("GrantIDtxt", "GrantIDtxt"), New System.Data.Common.DataColumnMapping("CaseNum", "CaseNum"), New System.Data.Common.DataColumnMapping("TypeofGrant", "TypeofGrant"), New System.Data.Common.DataColumnMapping("Amount", "Amount"), New System.Data.Common.DataColumnMapping("GrantLatest", "GrantLatest"), New System.Data.Common.DataColumnMapping("CongregationFinalReport", "CongregationFinalReport"), New System.Data.Common.DataColumnMapping("NextReportDue", "NextReportDue")})})
        daSecond3.SelectCommand.Connection = sc

FORM_SETUP:
        'LOAD COMBOs
        modGlobalVar.LoadStaffCombo(Me.cboStaff, True, StaffComboChoices.AllAndNo) 'preset to username
        modGlobalVar.LoadCRGCombo(Me.cboCRG)
        Me.cboCRG.SelectedIndex = -1
        Me.lstStatus.DataSource = tblCaseStatus
        Me.lstStatus.SelectedIndex = 0
        Me.cboCaller.SelectedIndex = 0

        'DATAVIEWS for filtering tables
        dvMain = New DataView(Me.DsSrchCase1.SrchCases)
        grdvwMain.DataSource = dvMain
        dvS1 = New DataView(Me.DsCaseSecondary1.GetConversations) '0conversations
        dvS2 = New DataView(Me.DsCaseSecondary1.GetRecommendations) '2recommendations 
        dvS3 = New DataView(Me.DsCaseSecondary1.GetGrants) '1grants

        'OVERDUE WARNING FLAG
        FillOverdueWarning(usr)

        isLoaded = True 'leave above cbocaller so load searches grid now
        btnSearch.PerformClick()
        Forms.Add(Me)

        Me.StatusBarPanel1.Text = "Done"

    End Sub 'load

    'CLOSE FORM
    Private Sub miCloseForm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles miCloseForm.Click
        Me.Close()
    End Sub

#End Region 'load

#Region "Search"

    'DO SEARCH
    Private Sub doSearch(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles miSrch.Click, btnSearch.Click ', lstStatus.SelectedIndexChanged, cboCRG.SelectedIndexChanged, cboCaller.SelectedIndexChanged

VALIDATION:
        If modGlobalVar.NewCheckList(lstStatus.Text, Me.lstStatus, "Case Status") Then   '6 hyphens
        Else
            Exit Sub
        End If
        If modGlobalVar.NewValidateStaff(cboStaff, True) Then ', conStaffHeading.FullList) Then
        Else
            Exit Sub
        End If
        If modGlobalVar.NewValidateCombo(Me.cboCaller, True) Then
        Else
            Exit Sub
        End If

        MouseWait()
RESET:
        ClearSecGrids()
        ClearMainGrid()
        Me.fldOrgNum.Text = ""
        Me.StatusBarPanelID.Text = "Case ID: "
        iGrid = 0 'row count
        Me.StatusBar1.Panels(0).Text = "Setting Params..."
        Me.btnSearch.BackColor = Color.FromKnownColor(KnownColor.InactiveCaption)

        '3 CLEAR EXISTING DATA, SET DEFAULT PARAMETERS
        daM.SelectCommand.Parameters("@CaseMgrFld").Value = Nothing
        daM.SelectCommand.Parameters("@CaseMgrVal").Value = Nothing
        daM.SelectCommand.Parameters("@CaseStatus").Value = Nothing
        '6001 = allstaff 6002 = nostaff

SET_PARAMS:
        'MGR vs CALLER
        If Me.cboStaff.SelectedValue = 6001 Then    'Text = "All Staff" Then     'search on all case managers
            daM.SelectCommand.Parameters("@CaseMgrVal").Value = Me.cboStaff.SelectedValue
            daM.SelectCommand.Parameters("@CaseMgrFld").Value = "Case Manager"
        ElseIf Me.cboStaff.SelectedValue = 6002 Then    'Text = "No Staff" Then  'search on no case manager
            daM.SelectCommand.Parameters("@CaseMgrVal").Value = 0
            daM.SelectCommand.Parameters("@CaseMgrFld").Value = "Case Manager"
        Else
            daM.SelectCommand.Parameters("@CaseMgrVal").Value = Me.cboStaff.SelectedValue
            daM.SelectCommand.Parameters("@CaseMgrFld").Value = Me.cboCaller.SelectedItem
        End If

        'STATUS
        daM.SelectCommand.Parameters("@CaseStatus").Value = Me.lstStatus.SelectedValue

        'CRG
        If Me.cboCRG.SelectedIndex = -1 Then
            daM.SelectCommand.Parameters("@CRG").Value = "%"
        Else
            daM.SelectCommand.Parameters("@CRG").Value = Me.cboCRG.Text & "%"
        End If

        'DATE
        daM.SelectCommand.Parameters("@Today").Value = Today

FILL_DATASET:
        Me.StatusBarPanel1.Text = "searching..."
        Me.DsSrchCase1.EnforceConstraints = False
        Try
            daM.Fill(Me.DsSrchCase1.SrchCases)
        Catch ex As System.FormatException
            msg("ATTENTION: Adjust your Search", "The field you selected to search cannot accept the search text you entered." & NextLine &
                   "Change your search text, or select a different field to search.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Me.StatusBarPanel1.Text = "case grid fill error"
            Exit Sub
        Catch ex As Exception
            msg("ERROR: can't fill main grid.", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

SET_HEADINGS:
        SrchTotal = CountCases() 'Me.DsSrchCase1.Tables(0).Rows.Count
        If SrchTotal > 0 Then
            ' Me.grdMain.Select(0)'don't select - it messes up multi-select
            Me.lblMainGrid.Text = Me.DsSrchCase1.SrchCases.Rows.Count.ToString & "  " & strDGM
            bFoundCase = True
            iGrid = Me.DsSrchCase1.Tables(0).Rows.Count

DO_NOT_SELECT_FIRST_ROW:
            'SET CASE ID and ORG FIELDS
            Me.grdvwMain.ClearSelection()
            SetIDFields(-1)
            'LOAD_OTHER_GRIDS:
            '  LoadSecondary()
        Else
            Me.lblMainGrid.Text = "NO MATCHING " & strDGM & " FOUND"
            modGlobalVar.msg("No Matches Found", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
            bFoundCase = False
        End If

CLEAR_ALL:
        Me.StatusBar1.Panels(0).Text = "Done"
        MouseDefault()
        btnSearch.BackColor = Color.FromKnownColor(KnownColor.Control)

    End Sub 'search

    'CALL SEARCH
    Private Sub cboCaller_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) _
    Handles cboCaller.SelectedIndexChanged, cboCRG.SelectedIndexChanged, lstStatus.SelectedIndexChanged
        If isLoaded Then
            Me.btnSearch.PerformClick()
        End If
    End Sub

    'CALL SEARCH and OVERDUE FLAG
    Private Sub cboStaff_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles cboStaff.SelectedIndexChanged

        If isLoaded Then
            Me.miSrch.PerformClick() 'search validate combo
            'allow headings in this search
            FillOverdueWarning(Me.cboStaff.SelectedValue)
        End If

    End Sub

#End Region 'search

#Region "Load Secondary"

    'LOAD SECONDARY GRIDS and CALL GET COUNTS
    Private Sub LoadSecondary()
        If isLoaded Then
        Else
            Exit Sub
        End If
        If Me.chkDetail.Checked Then
        Else
            Exit Sub
        End If
        If iCaseID > 0 Then 'nothing selected in main grid
        Else
            Exit Sub
        End If

        ClearSecGrids()
        SetStatusBarText("Retrieving data...")

        Me.DsCaseSecondary1.EnforceConstraints = False
        If sc.State = ConnectionState.Open Then
            sc.Close()
        End If

        If rb1Convers.Checked Then    'load first grid conversation
            '  Me.grdConversation.Visible = True
            daSecond1.SelectCommand.Parameters("@IDVal").Value = iCaseID
            daSecond1.SelectCommand.Parameters("@IDFld").Value = "Case"

            Try
                daSecond1.Fill(Me.DsCaseSecondary1.GetConversations)
                If Me.DsCaseSecondary1.GetConversations.Rows.Count > 0 Then
                    bFoundConversation = True
                    Me.grdConversation.CaptionText = Me.DsCaseSecondary1.GetConversations.Rows.Count.ToString & "  " & strDGS1
                Else
                    Me.grdConversation.CaptionText = "No " & strDGS1 & " found"
                    bFoundConversation = False
                End If
            Catch exc As Exception
                modGlobalVar.msg("ERROR: daConverse", exc.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try

        ElseIf rb2Recommend.Checked Then 'recommendations
            '  Me.grdRecommendation.Visible = True
            Try    'fill 3rd grid
                daSecond2.SelectCommand.Parameters("@IDFld").Value = "Case"
                daSecond2.SelectCommand.Parameters("@IDVal").Value = iCaseID
                daSecond2.Fill(Me.DsCaseSecondary1.GetRecommendations)

                If Me.DsCaseSecondary1.GetRecommendations.Rows.Count > 0 Then
                    bfoundResource = True
                    Me.grdRecommendation.CaptionText = Me.DsCaseSecondary1.GetRecommendations.Rows.Count.ToString & "  " & strDGS2
                Else
                    Me.grdRecommendation.CaptionText = "No " & strDGS2 & " found"
                    bfoundResource = False
                End If
            Catch exc As Exception
                modGlobalVar.msg("ERROR: load secondary daRec", exc.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try

        ElseIf rb3Grant.Checked Then 'grant
            '   Me.grdGrant.Visible = True
            Try    'fill third table of secondary grid 
                daSecond3.SelectCommand.Parameters("@IDFld").Value = "CaseNum"
                daSecond3.SelectCommand.Parameters("@IDVal").Value = iCaseID
                daSecond3.Fill(Me.DsCaseSecondary1.GetGrants)
                If Me.DsCaseSecondary1.GetGrants.Rows.Count > 0 Then
                    Me.grdGrant.CaptionText = Me.DsCaseSecondary1.GetGrants.Rows.Count.ToString & "  " & strDGS3
                    bFoundGrant = True
                Else
                    Me.grdGrant.CaptionText = "No " & strDGS3 & " found"
                    bFoundGrant = False
                End If
            Catch exc As Exception
                modGlobalVar.msg("ERROR: daGrant", exc.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        Else
            'MsgBox("Nothing checked")
        End If

        GetCounts()
        SetStatusBarText("Done")
    End Sub 'load secondary grids

    'COUNT SECONDARY ITEMS & MENU VISIBILITY
    Private Sub GetCounts()
        ' contacts, recommendations, grants
        Dim cmdCntID As New SqlCommand
        Dim SQLRecommend As String = "SELECT COUNT(RecommendID) FROM vwgetvalidrecommendations WHERE CaseNum = " & iCaseID 'Contact   'Me.txtCurrent.Text
        Dim SQLGrant As String = "SELECT COUNT(Grantnum) FROM vwCntGrants WHERE CaseNum = " & iCaseID 'Org 'Me.txtCurrent.Text
        Dim SQLConvers As String = "SELECT COUNT(ConversID) FROM vwgetvalidconversations WHERE CaseNum = " & iCaseID 'Contact   'Me.txtCurrent.Text

        cmdCntID.Connection = sc
        If Not SCConnect() Then
            Exit Sub
        End If
        SetStatusBarText("counting...")

        'GET RECOMMEND COUNT
        Dim i As Integer = 0
        cmdCntID.CommandText = SQLRecommend
        Try
            i = cmdCntID.ExecuteScalar
        Catch ex As Exception
            modGlobalVar.msg("ERROR: can't run getrecommend", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Me.cntRecommend.Text = i.ToString()

        'GET CONVERSATION COUNT
        cmdCntID.CommandText = SQLConvers
        i = 0
        Try
            i = cmdCntID.ExecuteScalar
        Catch ex As Exception
            modGlobalVar.msg("ERROR: can't run get Conversations", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Me.cntConvers.Text = i.ToString()

        'GET GRANT COUNT
        cmdCntID.CommandText = SQLGrant
        i = 0
        Try
            i = cmdCntID.ExecuteScalar
        Catch ex As Exception
            modGlobalVar.msg("ERROR: can't run get Grants", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Me.cntGrant.Text = i.ToString

        cmdCntID.Dispose()
        sc.Close()
        SetStatusBarText("Done")
    End Sub 'getCounts

#End Region 'load secondary grids

#Region "Datagrid"

    ' 'FILL DETAIL and txtcurrent
    Private Sub grdvwMain_SelectionChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles grdvwMain.SelectionChanged 'currentcellchanged
        If Not isLoaded Then
            Exit Sub
        End If

        If Me.DsSrchCase1.Tables(0).Rows.Count > 0 And Me.grdvwMain.SelectedCells.Count > 0 Then 'required for sorting click heading
SetVarsFromMainGrid:
            Dim row As DataGridViewRow
            Try
                row = Me.grdvwMain.CurrentRow
            Catch ex As Exception
                Exit Sub
            End Try

            If row.Cells("CaseIDColumn").Value = iCaseID Or row.Cells("CaseIDColumn").Value Is Nothing Then
                Exit Sub
            End If

            'SET CASE ID and ORG FIELDS
            SetIDFields(row.Index)
LoadOtherGrids:
            LoadSecondary()

        End If

    End Sub

    'SET ID FIELDS, call LOAD SECONDARY
    Private Sub SetIDFields(ByVal iRow As Integer)
        If iRow = -1 Then
            iCaseID = 0
            iOrgID = 0
            strGoTo = ""
            Me.StatusBarPanelID.Text = "Case ID: "
            Me.fldOrgNum.Text = String.Empty
            ClearSecGrids()
        Else
            iCaseID = Me.grdvwMain.Rows(iRow).Cells("CaseIDColumn").Value
            iOrgID = Me.grdvwMain.Rows(iRow).Cells("OrgNumColumn").Value
            strGoTo = Me.grdvwMain.Rows(iRow).Cells("OrgandCityColumn").Value & " : " & Me.grdvwMain.Rows(iRow).Cells("PhoneColumn").Value
            Me.StatusBarPanelID.Text = "Case ID: " & iCaseID
            Me.fldOrgNum.Text = iOrgID
        End If
        Me.lblSelectionID.Text = "" 'usrRadio.ToString
        Me.fldSelectionNum.Text = "" 'sender.item(0, sender.currentcell.rowindex).value
    End Sub

    'cCOUNT DISTINCT CASES in main grid re filter count display
    Private Function CountCases() As Integer
        Dim tDistinct As New DataTable("tDistinct")
        tDistinct = Me.DsSrchCase1.SrchCases.DefaultView.ToTable(True, "CaseID")
        CountCases = tDistinct.Rows.Count
        tDistinct = Nothing
    End Function

    'SEND SECONDARY GRID SELECTED ID TO LABELS
    Private Sub grd_CurrentCellChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
          Handles grdGrant.CurrentCellChanged, grdRecommendation.CurrentCellChanged, grdConversation.CurrentCellChanged
        If Not isLoaded Then
            Exit Sub
        End If

        If sender.CurrentRowIndex >= 0 Then
            Me.lblSelectionID.Text = usrRadio.ToString
            Me.fldSelectionNum.Text = sender.item(sender.currentrowindex, 0) 'urrentcell.rowindex).value
        End If
    End Sub

    'CLEAR SELECTION FROM DATAGRIDS AND LOSE FOCUS (note: selection stays with arrow)
    Private Sub grdUnselect_Click(ByVal sender As Object, ByVal e As System.EventArgs) _
         Handles MyBase.Click

        Me.grdvwMain.CurrentCell = Nothing
        ClearSecGrids()

    End Sub

    'SET SEARCH CRITERIA TO DEFAULTS AND CLEAR DATASETS
    Private Sub miClearSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles miClearSearch.Click
        isLoaded = False
        Me.cboStaff.SelectedIndex = cboStaff.FindString(usr)
        Me.lstStatus.SelectedIndex = 0
        '  Me.txtSearch.Text = "optional - enter search text"
        Me.cboCaller.SelectedItem = "Case Manager"
        ClearSecGrids()
        ClearMainGrid()

        Me.Refresh()
        ' EnableMenuItems(False)
        dv.RowFilter = ""
        isLoaded = True
    End Sub

    'CLEAR MAIN GRID
    Private Sub ClearMainGrid()

        Me.DsSrchCase1.Clear()
        Me.lblMainGrid.Text = ""
        ' ClearSecGrids()
        Me.fldOrgNum.Text = ""
        Me.StatusBarPanelID.Text = "Case ID:"
        'iCol = 0
    End Sub

    'CLEAR SECONDARY GRIDS and rb counts
    Private Sub ClearSecGrids()
        Try
            Me.DsCaseSecondary1.Clear()
        Catch e As Exception
        End Try
        Me.cntConvers.Text = 0
        Me.cntGrant.Text = 0
        Me.cntRecommend.Text = 0

        Me.grdConversation.CaptionText = ""
        Me.grdGrant.CaptionText = ""
        Me.grdRecommendation.CaptionText = ""

        statusS1 = ""
        statusS2 = ""
        statusS3 = ""

    End Sub

    'HIDE/SHOW SECONDARY GRIDS
    Private Sub chkDetail_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles chkDetail.CheckedChanged
        If Not isLoaded Then
            Exit Sub
        End If

        Me.pnlSecGrids.Visible = chkDetail.Checked
        LoadSecondary()

    End Sub

    'HIDE/SHOW SECONDARY GRIDS
    Private Sub RadioButton_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles rb1Convers.Click, rb2Recommend.Click, rb3Grant.Click 'CheckedChanged
        If Not isLoaded Then
            Exit Sub
        End If
        If chkDetail.Checked = False Then
            Exit Sub
        End If

        Me.grdConversation.Visible = rb1Convers.Checked
        Me.grdRecommendation.Visible = rb2Recommend.Checked
        Me.grdGrant.Visible = rb3Grant.Checked

        ''CHANGE VARIABLE for selectedlabel
        If sender.checked Then 'And Not sender.text = "     Show Related Items" Then
            usrRadio.Replace(usrRadio.ToString, sender.text)
        End If

        'LOAD GRID DATA
        LoadSecondary()

    End Sub

#End Region     'datagrid

#Region "Grid Filter"

    'MOUSE DOWN MAIN GRID - call FILTER
    Private Sub grdvwMain_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) _
            Handles grdvwMain.MouseDown    'grdSecond1.MouseDown, grdSecond3.MouseDown, grdvwMain.MouseDown, grdSecond2.MouseDown

        If e.Button = System.Windows.Forms.MouseButtons.Left Then  'wasn't loading secondary if click in same cell as RClick filter
            Exit Sub
        End If
CALL_FILTER:
        Dim str() As String = modGlobalVar.FilterDataGridView(sender, e, dvMain, False)
SET_HEADINGS:
        Select Case str(1)
            ' Case Is = "LEFT", "" 'no change

            Case Is = "CLEAR" 'restore heading
                Me.lblMainGrid.Text = CountCases().ToString & " " & strDGM
                Me.grdvwMain.ClearSelection()
                SetIDFields(-1)
                ClearSecGrids()
            Case Else 'filtered, reset id, clear sec grids
                lblMainGrid.Text = str(0) & "/" & SrchTotal.ToString & str(1)
                If Me.chkDetail.Checked Then
                    ClearSecGrids()    'clear child grids 
                End If
                If str(0) > 1 Then 'select no row
                    Me.grdvwMain.ClearSelection()
                    SetIDFields(-1)
                ElseIf str(0) = 1 Then 'process first row
                    SetIDFields(0)
                    LoadSecondary()
                End If
        End Select

    End Sub 'mouse down Main grid

    'REMOVE MAIN GRID FILTER 
    Private Sub lblMainGrid_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lblMainGrid.MouseClick
        If e.Button = System.Windows.Forms.MouseButtons.Right Then
            dvMain.RowFilter = ""
            Me.grdvwMain.Refresh()
            Me.lblMainGrid.Text = Me.DsSrchCase1.Tables(0).Rows.Count.ToString & "  " & strDGM
            iGrid = Me.DsSrchCase1.Tables(0).Rows.Count
            Me.grdvwMain.ClearSelection()
            SetIDFields(-1)
            'SetIDFields(0)'first row selected automatically anyway
LoadOtherGrids:
            ' LoadSecondary()'no rows selected
        End If
    End Sub

    'SECONDARY GRIDS MOUSE DOWN for FILTER
    Private Sub grdSec_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) _
             Handles grdConversation.MouseDown, grdGrant.MouseDown, grdRecommendation.MouseDown

        Dim tbl As Object
        Dim strHdr As String    'text for grid header

        strbActiveGrid.Replace(strbActiveGrid.ToString, sender.tag)    'use this for doubleclick code
        If e.Button = System.Windows.Forms.MouseButtons.Left Then
            Exit Sub
        End If

        hti = sender.HitTest(e.X, e.Y)
        Select Case sender.tag
            Case Is = "Conversation"
                tbl = Me.DsCaseSecondary1.GetConversations
                strHdr = strDGS1
            Case Is = "Recommendation"
                tbl = Me.DsCaseSecondary1.GetRecommendations
                strHdr = strDGS2
            Case Is = "Grant"
                tbl = Me.DsCaseSecondary1.GetGrants
                strHdr = strDGS3
            Case Else
                modGlobalVar.msg("ERROR: GRID TAG not found", sender.tag, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
        End Select
        If sender.CurrentRowIndex >= 0 Then
            Me.lblSelectionID.Text = usrRadio.ToString
            Me.fldSelectionNum.Text = sender.item(sender.currentrowindex, 0) 'urrentcell.rowindex).value
        End If
        '   strbActiveGrid.Replace(strbActiveGrid.ToString, sender.tag)    'use this for doubleclick code

        'SET VARs BASED ON GRID SELECTED
SetFilter:
        If hti.Type = DataGrid.HitTestType.Cell Then    'SET FILTER
            'CHECK FOR NULL
            If IsDBNull(sender.Item(hti.Row, hti.Column)) Then 'nulls cause filter error
                modGlobalVar.msg(MsgCodes.filterEmpty)
                Exit Sub
            End If

            Select Case strHdr
                Case Is = strDGS1
                    grdFilter(sender, Me.DsCaseSecondary1, tbl, strHdr, dvS1)
                Case Is = strDGS2
                    grdFilter(sender, Me.DsCaseSecondary1, tbl, strHdr, dvS2)
                Case Is = strDGS3
                    grdFilter(sender, Me.DsCaseSecondary1, tbl, strHdr, dvS3)

                Case Else
                    Exit Sub
            End Select
ClearFilter:
        Else            'not in cell,  'CLEAR FILTER
            sender.dataSource = tbl 'removes dv.rowfilter
            sender.CaptionText = tbl.Rows.Count.ToString & "  " & strHdr 'DsOrgSearch1.tblOrg.Rows.Count.ToString
            Me.StatusBarPanel1.Text = ""
            Select Case strHdr
                Case Is = strDGM
                    statusM = "Done"
                    If Me.chkDetail.Checked Then
                        ClearSecGrids()    'clear child grids 
                    End If
                    statusS1 = ""
                Case Is = strDGS1, strDGS2, strDGS3
                    statusS1 = ""
            End Select
        End If
    End Sub 'mouse down sec grids

    'FILTER SECONDARY GRIDS 
    Private Sub grdFilter(ByVal grd As Object, ByVal ds As Object, ByVal tbl As Object, ByVal strHdr As String, ByVal dv As DataView)
        Dim strFilter As String
        Dim myColumns As GridColumnStylesCollection

        myColumns = grd.TableStyles(0).GridColumnStyles
        strFilter = myColumns(hti.Column).MappingName
        strFilter = strFilter & " = '" & Replace(grd.Item(hti.Row, hti.Column), "'", "''") & " '"
        dv.RowFilter = strFilter
        grd.DataSource = dv
        grd.CaptionText = dv.Count.ToString & "/" & tbl.Rows.Count.ToString & "  " & " filtered on " & myColumns(hti.Column).HeaderText 'strHdr

        Select Case strHdr
            Case Is = strDGS1
                statusS1 = strHdr & " filtered on " & myColumns(hti.Column).HeaderText
            Case Is = strDGS2
                statusS2 = strHdr & " filtered on " & myColumns(hti.Column).HeaderText
            Case Is = strDGS3
                statusS3 = strHdr & " filtered on " & myColumns(hti.Column).HeaderText
        End Select

        SetStatusBarText("done")
    End Sub

#End Region 'filter grids

#Region "Open Forms"

    'OPEN CASE DETAIL
    Private Sub btnOpenMain_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles miGotoCase.Click, grdvwMain.DoubleClick
        Dim row As DataGridViewRow = Me.grdvwMain.CurrentRow

        If bFoundCase Then
            MouseWait()
            Me.StatusBarPanel1.Text = "Opening Case window"
            If Me.cboCaller.SelectedItem = "Staff Conversations" Then
                modGlobalVar.OpenMainStaffConversation(iCaseID)
            Else
                modGlobalVar.OpenMainCase(iCaseID, row.Cells("CaseNameColumn").Value, row.Cells("OrgandCityColumn").Value & " : " & row.Cells("PhoneColumn").Value, row.Cells("OrgNumColumn").Value)
            End If
            Me.StatusBarPanel1.Text = "Done"
            MouseDefault()
        End If
        row = Nothing
    End Sub

    'OPEN CONVERSATION DETAIL
    Private Sub OpenConversation(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles grdConversation.DoubleClick, miGotoConversation.Click
        Dim str As String

        If bFoundConversation Then 'true if rows were returned on fill
            'not necessarily true if item was deleted on opened detail form
            MouseWait()
            Try
                str = SubstrBriefSummary(IsNull(Me.grdConversation.Item(Me.grdConversation.CurrentRowIndex, 3), ""))
                modGlobalVar.OpenMainConversation(Me.grdConversation.Item(Me.grdConversation.CurrentRowIndex, 0), str, Me.grdvwMain.Item(2, Me.grdvwMain.CurrentRow.Index).Value & " : " & Me.grdvwMain.Item("PhoneColumn", Me.grdvwMain.CurrentRow.Index).Value, Me.grdvwMain.Item(1, Me.grdvwMain.CurrentRow.Index).Value)
            Catch ex As Exception
                MsgBox(ex.Message, , "error item not found ")
            End Try
            MouseDefault()
        End If

    End Sub

    'OPEN GRANT, RECOMMENDATION, MGI APP
    Private Sub OpenSecondary(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles grdGrant.DoubleClick, miGotoGrant.Click, miGotoRecommendation.Click, grdRecommendation.DoubleClick

        Select Case strbActiveGrid.ToString
            Case "Grant"
                If bFoundGrant Then
                    'Me.grdGrant.Item(Me.grdGrant.CurrentRowIndex, 0).ToString.Substring(4)
                    If Me.grdGrant.Item(Me.grdGrant.CurrentRowIndex, 3).ToString.Contains("Application") Then
                        modGlobalVar.OpenMGIForm(Me.grdGrant.Item(Me.grdGrant.CurrentRowIndex, 0).substring(4), Me.grdGrant.Item(Me.grdGrant.CurrentRowIndex, 0).ToString.Substring(0, 4), strGoTo, iOrgID)
                    Else
                        modGlobalVar.OpenMainGrant(Me.grdGrant.Item(Me.grdGrant.CurrentRowIndex, 0), Me.grdvwMain.Item(2, Me.grdvwMain.CurrentRow.Index).Value, Me.grdvwMain.Item(2, Me.grdvwMain.CurrentRow.Index).Value & " : " & Me.grdvwMain.Item(6, Me.grdvwMain.CurrentRow.Index).Value, Me.grdvwMain.Item(1, Me.grdvwMain.CurrentRow.Index).Value)
                    End If
                End If
            Case "Recommendation"
                If bfoundResource Then
                    Try
                        modGlobalVar.OpenMainRecommend(Me.grdRecommendation.Item(Me.grdRecommendation.CurrentRowIndex, 0), IsNull(Me.grdRecommendation.Item(Me.grdRecommendation.CurrentRowIndex, 2), ""), Me.grdRecommendation.Item(Me.grdRecommendation.CurrentRowIndex, 5))
                    Catch ex As Exception
                        modGlobalVar.msg("ERROR: srch open Recommend", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        '     modGlobalVar.Msg("RecommendID:" + Me.grdRecommendation.Item(Me.grdRecommendation.CurrentRowIndex, 0).ToString & NextLine & "OrgID: " + Me.grdRecommendation.Item(Me.grdRecommendation.CurrentRowIndex, 5).ToString, , Me.grdRecommendation.Item(Me.grdRecommendation.CurrentRowIndex, 2))
                    End Try
                End If
            Case Else
                modGlobalVar.msg("ERROR: active grid not found", strbActiveGrid.ToString, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Select
    End Sub 'secondary grid dblclick

#End Region 'open forms

#Region "General"

    'SET STATUS BAR LEFT TEXT re FILTERS
    Private Sub SetStatusBarText(ByVal str As String)
        Me.StatusBarPanel1.Text = str '& " " & statusS1 '& " " & statusS2
    End Sub

    'SET STATUS BAR LEFT TEXT - custom for this form
    Private Sub SetStatusBarText()
        Me.StatusBar1.Panels(0).Text = statusM & " " & statusS1 & " " & statusS2 & " " & statusS3
    End Sub

    'BTN HELP
    Private Sub miHelp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles miHelp.Click, btnHelp.Click

        modGlobalVar.msg("HELP: SEARCH for CASES", "* Asterisk after the Case Name indicates an Overdue Grant Report" & NextLine & NextLine & "HOW TO SEARCH. " & NextLine &
            "1. Using the dropdown box and list, select the staff name and case status." & NextLine &
                         "2. Click the Search button, or press the Enter key." & NextLine & NextLine &
                         "Change the Case Manager dropdown box to search for staff as the caller but not the Case Manager." & NextLine & NextLine &
                         "Conversations in the Case are listed below the main grid, or click the radio button to see Resources Recommended and Grants." & NextLine & NextLine &
            "To ADD NEW:" & NextLine & "CONVERSATION - Highlight a case then Click the yellow New button or use the menu: File/New Conversation." & NextLine & "CASE or GRANT - first go to the Organization Detail window." & NextLine & NextLine & "For Status Definitions use Help menu item.", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    'DISPLAY OVERDUE GRANT REPORT FLAG
    Private Sub FillOverdueWarning(ByVal istaff As Integer)

        Dim myCommand As New SqlCommand
        Dim o, s As Integer
        Dim strS As String = ""
        Dim strO As String = ""
        Dim drdr As SqlDataReader

        myCommand.Parameters.Add("@MgrID", SqlDbType.Int)
        myCommand.Parameters("@MgrID").Value = istaff
        myCommand.CommandType = System.Data.CommandType.StoredProcedure
        myCommand.Connection = sc
        If Not SCConnect() Then
            Exit Sub
        End If

        myCommand.CommandText = ("[CountGrantsDueSoon]")
        s = 0
        o = 0
        drdr = myCommand.ExecuteReader
        Try
            drdr.Read()
            s = drdr.GetValue(0)
            drdr.Read()
            o = drdr.GetValue(0)
        Catch ex As Exception
            modGlobalVar.msg("ERROR: grants due soon ", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            drdr.Close()
            sc.Close()
        End Try

        If s > 0 Then
            strS = ": " & s & " Due within 30 days."
        End If
        If o > 0 Then
            strO = o & " OVERDUE  "
        End If
        If s > 0 Or o > 0 Then
            Me.lblOverdue.Text = "WARNING -  " & strO & " Congregation Grant Final Reports " & strS '" OVERDUE Grant Final Reports and " & s & " coming due."
            Me.lblOverdue.Visible = True
            Try
                My.Computer.Audio.Play(My.Resources.MyBeep, AudioPlayMode.Background)
            Catch ex As Exception
            End Try
        Else
            Me.lblOverdue.Visible = False
        End If

    End Sub

    'FIND CRG TERM
    Private Sub cboCRG_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) _
         Handles cboCRG.MouseDown
        If e.Button = System.Windows.Forms.MouseButtons.Right Then
            modPopup.SearchCRG(Me, PointToClient(Control.MousePosition), Me.cboCRG)
        End If
    End Sub

    'CAPTURE ESCAPE FOR UNDO TEXTBOX
    Protected Overrides Function ProcessDialogKey(ByVal keyData As Keys) As Boolean
        If keyData = System.Windows.Forms.Keys.Escape And Me.ActiveControl.Name = "cboStaff" Or Me.ActiveControl.Name = "cboCRG" Then
            modPopup.UndoCtl(Me.ActiveControl)
            Return True  ' True means we've processed the key
        Else
            Return MyBase.ProcessDialogKey(keyData)
        End If
    End Function

    'OPEN DEFINITIONS IN NOTEPAD
    Private Sub miDefinitions_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles miDefinitions.Click
        modPopup.ShowDefinitions("Case")
    End Sub

#End Region 'general

#Region "Add Item"

    'INSERT NEW ITEM to BACKEND and OPEN DETAIL FORM
    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles btnNew.Click, miNew.Click
        'retrieve current case rating, case crb
        'insert new conversation
        Dim varRate, varCRG As Integer  'retrieve case defaults
        Dim strCase As New SqlCommand("SELECT CurrentCaseRating, CRGNum FROM tblCase WHERE CaseID = " & Me.grdvwMain.Item(0, Me.grdvwMain.CurrentRow.Index).Value, sc)
        Me.StatusBarPanel1.Text = "Adding new conversation"
        If modGlobalVar.msg("Are you sure?", "About to enter a new Conversation in Case: " & Me.grdvwMain.Item(3, Me.grdvwMain.CurrentRow.Index).Value.ToString, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then
            Exit Sub
        End If
        If Not iCaseID > 0 Then
            modGlobalVar.msg("ATTENTION: unable to enter new Conversation", "please select a case first", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        If Not SCConnect() Then
            Exit Sub
        End If

        Dim rdr As SqlDataReader = strCase.ExecuteReader(CommandBehavior.SingleRow)
        Try
            While rdr.Read()
                If IsDBNull(rdr.GetValue(0)) Then
                    varRate = ""
                Else
                    varRate = rdr.GetValue(0)
                End If
                If IsDBNull(rdr.GetValue(1)) Then
                    varCRG = ""
                Else
                    varCRG = rdr.GetValue(1)
                End If
            End While
        Catch ex As Exception
        Finally
            rdr.Close()
        End Try

InsertNewItem:
        Dim newID As Integer
        Dim strInsert As New SqlCommand("INSERT INTO tblConversation(CaseNum, OrgNum, StaffNum,  ConversDate, CaseRating, CRGNum) VALUES (" & Me.grdvwMain.Item(0, Me.grdvwMain.CurrentRow.Index).Value & ", " & Me.grdvwMain.Item(1, Me.grdvwMain.CurrentRow.Index).Value & ", " & usr & ",  GETDATE()," & varRate & ", " & varCRG & "); SELECT @@Identity", sc) ', myTrans)
        Try
            newID = strInsert.ExecuteScalar()
        Catch exce As Exception
            modGlobalVar.msg("ERROR: can't enter new conversation", exce.Message, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
        sc.Close()
OpenForm:
        modGlobalVar.OpenMainConversation(newID, "Entering new conversation with Case : " & Me.grdvwMain.Item(3, Me.grdvwMain.CurrentRow.Index).Value, Me.grdvwMain.Item(2, Me.grdvwMain.CurrentRow.Index).Value & ":" & Me.grdvwMain.Item("PhoneColumn", Me.grdvwMain.CurrentRow.Index).Value, Me.grdvwMain.Item(1, Me.grdvwMain.CurrentRow.Index).Value)
        Me.StatusBarPanel1.Text = "Done"
    End Sub

#End Region 'add item

#Region "Print List"

    'PRINT CASE & CONVERSATION REPORT
    Private Sub miPrintConv_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles miPrintConv.Click

        MouseWait()

        Dim bSelected As Boolean = False
        Dim iRow As Integer
        Dim strb As New StringBuilder
        strb.Append("SELECT  tblCase.CaseID, tblConversation.ConversDate, tblConversation.Notes, tblOrg.OrgName, " _
        & " tblCase.CaseName, tblContact.FirstName, tblContact.Lastname,  tblConversation.BriefSummary, luStaff.StaffName AS CaseMgr " _
 & " FROM         tblCase LEFT OUTER JOIN luStaff ON tblCase.CaseMgrNum = luStaff.StaffID LEFT OUTER JOIN tblConversation ON tblCase.CaseID = tblConversation.CaseNum LEFT OUTER JOIN tblOrg ON tblCase.OrgNum = tblOrg.OrgID LEFT OUTER JOIN tblContact ON tblConversation.ContactNum = tblContact.ContactID" _
 & " WHERE     (1 = 2)")

        'GET QUERY CRITERIA
        For iRow = 0 To iGrid - 1
            If Me.grdvwMain.Rows(iRow).Selected = True Then
                bSelected = True
                strb.Append(" OR tblCase.CaseID = " & Me.grdvwMain.Item(0, iRow).Value)
            End If
        Next iRow

        If bSelected Then
            strb.Append(" ORDER BY CaseID, tblConversation.ConversDate DESC")
            modPopup.PrintCaseConversation(strb.ToString)
        Else
            modGlobalVar.msg("ATTENTION: Cancelling Report", "No rows are selected." & vbNewLine & "Highlight a row and try again.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        MouseDefault()
    End Sub

    'print travel case contact phone list
    Private Sub miPrintContact_Click(sender As System.Object, e As System.EventArgs) Handles miPrintContact.Click
        MouseWait()

        Dim bSelected As Boolean = False
        Dim iRow As Integer
        Dim strb As New StringBuilder
        strb.Append("SELECT  tblCase.CaseName, tblOrg.OrgName, " _
        & " ContactName, tContact.Phone, tcontact.email as ContactEmail, tblorg.phone as OrgPhone " _
 & " FROM  tblCase LEFT OUTER JOIN tblOrg ON tblCase.OrgNum = tblOrg.OrgID LEFT OUTER JOIN " _
 & " (SELECT top 1 ConversID, CaseNum, ContactNum, ConversDate FROM tblconversation ORDER BY ConversDate DESC) as tConvers on caseid = casenum LEFT OUTER JOIN " _
 & " (SELECT ContactID, isnull(firstname,' ') + isnull(' ' + Lastname,'') as ContactName, Phone, Email FROM tblContact) as tContact ON tConvers.ContactNum = tContact.ContactID " _
 & " WHERE     (1 = 2) ") 'to allow concatenation based on rowS selected

        'GET QUERY CRITERIA
        For iRow = 0 To iGrid - 1
            If Me.grdvwMain.Rows(iRow).Selected = True Then
                bSelected = True
                strb.Append(" OR tblCase.CaseID = " & Me.grdvwMain.Item(0, iRow).Value)
            End If
        Next iRow

        If bSelected Then
            strb.Append(" ORDER BY CaseID")
            ClassLibrary1.ClassWriter2.PrintReport(strb.ToString, "Case Contacts" & NextLine & Now().ToShortDateString, True)
        Else
            modGlobalVar.msg(MsgCodes.noRowSelected)
            '"ATTENTION: Cancelling Report", "No rows are selected." & vbNewLine & "Highlight a row and try again.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        MouseDefault()
    End Sub

#End Region 'print

#Region "TESTING"
    'TODO why does form size change itself?
    Private Sub lblOverdue_Click(sender As System.Object, e As System.EventArgs) Handles lblOverdue.Click
        MsgBox("Form height" & Me.Height.ToString & NextLine &
            "Form Width" & Me.Width.ToString & NextLine, , "form dimensions")
    End Sub
#End Region 'testing

End Class



