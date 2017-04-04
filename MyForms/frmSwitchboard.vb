Imports System.Data.SqlClient
Imports System.Text
'Imports System.Threading
Imports oApp = Microsoft.Office.Interop.Outlook
Imports System.IO


Public Class frmSwitchboard
    Inherits System.Windows.Forms.Form

    Dim frmRTB As New frmRTB
    Dim foundRow() As Data.DataRow
    Dim rptSubTitle As String    'sub title on Word report listing selected params
    Dim rptGroupOn As String    'which field in bold, restarts group in Word
    Dim bdate, bRegion, bType, bCRG, bStaff, bSize As Boolean

    Dim mCharFrom As Integer
    Dim tnSel As TreeNode
    Dim dtEndOne As Date 'add 1 to date to compensate for time of midnight for between date criteria
    Dim tblDesc As New DataTable("tDescription")
    Dim tblGen As New DataTable("tLoadCBO")
    Dim KeepDates As Boolean = False   'so dates don't reset between reports
    'selected treenode
    Dim isLoaded As Boolean = False
    Dim iStart As Integer = 0 'find starting point
    Dim ExpHeading As String

#Region "Initialize"

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()
        Me.ClientSize = New System.Drawing.Size(1111, 675)
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

#End Region

#Region " Windows Form Designer generated code "

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents rbGQI As System.Windows.Forms.RadioButton
    Friend WithEvents rbYellow As System.Windows.Forms.RadioButton

    Friend WithEvents pnlRptSelect As System.Windows.Forms.Panel
    Friend WithEvents pnlDate As System.Windows.Forms.Panel
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents dtEnd As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents dtStart As System.Windows.Forms.DateTimePicker
    Friend WithEvents btnGo As System.Windows.Forms.Button
    Friend WithEvents cboReport As InfoCtr.ComboBoxRelaxed
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents cboYear As InfoCtr.ComboBoxRelaxed
    Friend WithEvents grpQuarter As System.Windows.Forms.GroupBox
    Friend WithEvents rbQtr4 As System.Windows.Forms.RadioButton
    Friend WithEvents rbQtr3 As System.Windows.Forms.RadioButton
    Friend WithEvents rbQtr2 As System.Windows.Forms.RadioButton
    Friend WithEvents rbQtr1 As System.Windows.Forms.RadioButton
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents cboCycle As InfoCtr.ComboBoxRelaxed
    Friend WithEvents Label17 As System.Windows.Forms.Label

    Friend WithEvents PrintPreviewDialog1 As System.Windows.Forms.PrintPreviewDialog
    Friend WithEvents PrintDocument1 As System.Drawing.Printing.PrintDocument
    Friend WithEvents PrintDialog1 As System.Windows.Forms.PrintDialog
    Friend WithEvents MainMenu1 As System.Windows.Forms.MainMenu
    Friend WithEvents MenuItem1 As System.Windows.Forms.MenuItem
    Friend WithEvents miExport As System.Windows.Forms.MenuItem
    Friend WithEvents miClose As System.Windows.Forms.MenuItem
    Friend WithEvents miPageSetup As System.Windows.Forms.MenuItem
    Friend WithEvents miPreview As System.Windows.Forms.MenuItem
    Friend WithEvents PageSetupDialog1 As System.Windows.Forms.PageSetupDialog
    Friend WithEvents miPrint As System.Windows.Forms.MenuItem
    Friend WithEvents oldpnlCRG As System.Windows.Forms.Panel
    Friend WithEvents chkIndex As System.Windows.Forms.CheckBox
    Friend WithEvents chkCRG As System.Windows.Forms.CheckBox
    Friend WithEvents chkTitle As System.Windows.Forms.CheckBox
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents txtTitle As System.Windows.Forms.TextBox
    Friend WithEvents ComboBox1 As InfoCtr.ComboBoxRelaxed
    Friend WithEvents txtCRG As System.Windows.Forms.TextBox
    Friend WithEvents txtIndex As System.Windows.Forms.TextBox
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents rbQtrAll As System.Windows.Forms.RadioButton
    Friend WithEvents pnlBookList As System.Windows.Forms.Panel
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents cboShelf As InfoCtr.ComboBoxRelaxed
    Friend WithEvents Label34 As System.Windows.Forms.Label
    Friend WithEvents trvType As System.Windows.Forms.TreeView
    Friend WithEvents Label35 As System.Windows.Forms.Label
    Friend WithEvents Label36 As System.Windows.Forms.Label
    Friend WithEvents lblDescription As System.Windows.Forms.Label
    Friend WithEvents pnlGrantCycle As System.Windows.Forms.Panel
    Friend WithEvents pnlDateRange As System.Windows.Forms.Panel
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents lblTop10 As System.Windows.Forms.Label
    Friend WithEvents cboTop10 As InfoCtr.ComboBoxRelaxed
    Friend WithEvents lblDatePertainsTo As System.Windows.Forms.Label
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents grpReports As System.Windows.Forms.GroupBox
    Friend WithEvents rbWorkshop As System.Windows.Forms.RadioButton
    Friend WithEvents rbResource As System.Windows.Forms.RadioButton
    Friend WithEvents rbGrant As System.Windows.Forms.RadioButton
    Friend WithEvents rbOrg As System.Windows.Forms.RadioButton
    Friend WithEvents rbCase As System.Windows.Forms.RadioButton
    Friend WithEvents grpAdmin As System.Windows.Forms.GroupBox
    Friend WithEvents rbDuplResource As System.Windows.Forms.RadioButton
    Friend WithEvents rbDuplOrg As System.Windows.Forms.RadioButton
    Friend WithEvents rbDelivra As System.Windows.Forms.RadioButton
    Friend WithEvents rbInvalidEmail As System.Windows.Forms.RadioButton
    Friend WithEvents rbLib As System.Windows.Forms.RadioButton
    Friend WithEvents rbMailing As System.Windows.Forms.RadioButton
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents grpQuick As System.Windows.Forms.GroupBox
    Friend WithEvents rbActiveCongr As System.Windows.Forms.RadioButton
    Friend WithEvents rbCurrentCases As System.Windows.Forms.RadioButton
    Friend WithEvents rbConvToDate As System.Windows.Forms.RadioButton
    Friend WithEvents rbMapZip As System.Windows.Forms.RadioButton
    Friend WithEvents TabControl2 As System.Windows.Forms.TabControl
    Friend WithEvents tbDate As System.Windows.Forms.TabPage
    Friend WithEvents tbRegion As System.Windows.Forms.TabPage
    Friend WithEvents tbCRG As System.Windows.Forms.TabPage
    Friend WithEvents trvStaff As System.Windows.Forms.TreeView
    Friend WithEvents trvRegion As System.Windows.Forms.TreeView
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label39 As System.Windows.Forms.Label
    Friend WithEvents Label40 As System.Windows.Forms.Label
    Friend WithEvents Label43 As System.Windows.Forms.Label
    Friend WithEvents Label42 As System.Windows.Forms.Label
    Friend WithEvents Label41 As System.Windows.Forms.Label
    Friend WithEvents Label44 As System.Windows.Forms.Label
    Friend WithEvents trvSize As System.Windows.Forms.TreeView
    Friend WithEvents rbNeverReviewed As System.Windows.Forms.RadioButton
    Friend WithEvents Label45 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents StatusLabel As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents StatusProgressBar As System.Windows.Forms.ToolStripProgressBar
    Friend WithEvents Label37 As System.Windows.Forms.Label
    Friend WithEvents Label38 As System.Windows.Forms.Label
    Friend WithEvents btnEmailTest As System.Windows.Forms.Button
    Friend WithEvents rbListCRG As System.Windows.Forms.RadioButton
    Friend WithEvents rbCntResourceType As System.Windows.Forms.RadioButton
    Friend WithEvents Label46 As System.Windows.Forms.Label
    Friend WithEvents trvCRG As System.Windows.Forms.TreeView
    Friend WithEvents rbResourceGuide As System.Windows.Forms.RadioButton
    Friend WithEvents rbMGI As System.Windows.Forms.RadioButton
    Friend WithEvents Label47 As System.Windows.Forms.Label
    Friend WithEvents rbResourceType As System.Windows.Forms.RadioButton
    Friend WithEvents rbBoard As System.Windows.Forms.RadioButton
    Friend WithEvents lblQuarter As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents grpOnCRG As System.Windows.Forms.GroupBox
    Friend WithEvents rbCRGNo As System.Windows.Forms.RadioButton
    Friend WithEvents rbCRGEither As System.Windows.Forms.RadioButton
    Friend WithEvents rbCRGYes As System.Windows.Forms.RadioButton
    Friend WithEvents Label48 As System.Windows.Forms.Label

    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents rbCRG As System.Windows.Forms.RadioButton
    Friend WithEvents rbCurrent As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButton3 As System.Windows.Forms.RadioButton
    Friend WithEvents rbResources As System.Windows.Forms.RadioButton
    Friend WithEvents rbMail As System.Windows.Forms.RadioButton
    Friend WithEvents rbLibrary As System.Windows.Forms.RadioButton
    Friend WithEvents rbStats As System.Windows.Forms.RadioButton
    Friend WithEvents rbEmail As System.Windows.Forms.RadioButton
    Friend WithEvents rbGeography As System.Windows.Forms.RadioButton
    Friend WithEvents rbAdmin As System.Windows.Forms.RadioButton
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim TreeNode1 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("1-49  (Family)")
        Dim TreeNode2 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("50-149  (Pastoral)")
        Dim TreeNode3 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("150-349  (Program)")
        Dim TreeNode4 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("350-999  (Corporate)")
        Dim TreeNode5 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("1000 or more  (Mega)")
        Dim TreeNode6 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("AllSizes", New System.Windows.Forms.TreeNode() {TreeNode1, TreeNode2, TreeNode3, TreeNode4, TreeNode5})
        Dim TreeNode7 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Admin Asst")
        Dim TreeNode8 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Consultants")
        Dim TreeNode9 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Support")
        Dim TreeNode10 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("All Active Staff", New System.Windows.Forms.TreeNode() {TreeNode7, TreeNode8, TreeNode9})
        Dim TreeNode11 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Central")
        Dim TreeNode12 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("NE")
        Dim TreeNode13 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("NW")
        Dim TreeNode14 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("SE")
        Dim TreeNode15 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("SW")
        Dim TreeNode16 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("All Regions", New System.Windows.Forms.TreeNode() {TreeNode11, TreeNode12, TreeNode13, TreeNode14, TreeNode15})
        Dim TreeNode17 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Inactive")
        Dim TreeNode18 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("AllStaff", New System.Windows.Forms.TreeNode() {TreeNode10, TreeNode16, TreeNode17})
        Dim TreeNode19 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Central")
        Dim TreeNode20 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("NE")
        Dim TreeNode21 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("NW")
        Dim TreeNode22 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("SE")
        Dim TreeNode23 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("SW")
        Dim TreeNode24 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Satellite Regions", New System.Windows.Forms.TreeNode() {TreeNode19, TreeNode20, TreeNode21, TreeNode22, TreeNode23})
        Dim TreeNode25 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("All Areas", New System.Windows.Forms.TreeNode() {TreeNode24})
        Dim TreeNode26 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Outside Indiana")
        Dim TreeNode27 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("International")
        Dim TreeNode28 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Future Use", New System.Windows.Forms.TreeNode() {TreeNode26, TreeNode27})
        Dim TreeNode29 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("All Administration")
        Dim TreeNode30 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("All Building Issues")
        Dim TreeNode31 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("All Congregational Vitality")
        Dim TreeNode32 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("All Leadership")
        Dim TreeNode33 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("All Miscellaneous")
        Dim TreeNode34 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("All Public Ministry")
        Dim TreeNode35 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("All Religion in America")
        Dim TreeNode36 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("All Specialized Ministry")
        Dim TreeNode37 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("All Spirituality")
        Dim TreeNode38 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("All Worship")
        Dim TreeNode39 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("All Center Services")
        Dim TreeNode40 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("All CRG Terms", New System.Windows.Forms.TreeNode() {TreeNode29, TreeNode30, TreeNode31, TreeNode32, TreeNode33, TreeNode34, TreeNode35, TreeNode36, TreeNode37, TreeNode38, TreeNode39})
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSwitchboard))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.rbGQI = New System.Windows.Forms.RadioButton()
        Me.rbYellow = New System.Windows.Forms.RadioButton()
        Me.rbAdmin = New System.Windows.Forms.RadioButton()
        Me.rbGeography = New System.Windows.Forms.RadioButton()
        Me.rbStats = New System.Windows.Forms.RadioButton()
        Me.rbEmail = New System.Windows.Forms.RadioButton()
        Me.rbResources = New System.Windows.Forms.RadioButton()
        Me.rbMail = New System.Windows.Forms.RadioButton()
        Me.rbLibrary = New System.Windows.Forms.RadioButton()
        Me.RadioButton3 = New System.Windows.Forms.RadioButton()
        Me.rbCurrent = New System.Windows.Forms.RadioButton()
        Me.rbCRG = New System.Windows.Forms.RadioButton()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.rbMapZip = New System.Windows.Forms.RadioButton()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.grpReports = New System.Windows.Forms.GroupBox()
        Me.rbBoard = New System.Windows.Forms.RadioButton()
        Me.Label38 = New System.Windows.Forms.Label()
        Me.rbWorkshop = New System.Windows.Forms.RadioButton()
        Me.rbResource = New System.Windows.Forms.RadioButton()
        Me.rbGrant = New System.Windows.Forms.RadioButton()
        Me.rbOrg = New System.Windows.Forms.RadioButton()
        Me.rbCase = New System.Windows.Forms.RadioButton()
        Me.rbMailing = New System.Windows.Forms.RadioButton()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.Label39 = New System.Windows.Forms.Label()
        Me.grpQuick = New System.Windows.Forms.GroupBox()
        Me.rbResourceType = New System.Windows.Forms.RadioButton()
        Me.rbMGI = New System.Windows.Forms.RadioButton()
        Me.rbCntResourceType = New System.Windows.Forms.RadioButton()
        Me.rbListCRG = New System.Windows.Forms.RadioButton()
        Me.rbNeverReviewed = New System.Windows.Forms.RadioButton()
        Me.rbActiveCongr = New System.Windows.Forms.RadioButton()
        Me.rbCurrentCases = New System.Windows.Forms.RadioButton()
        Me.rbConvToDate = New System.Windows.Forms.RadioButton()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.Label40 = New System.Windows.Forms.Label()
        Me.grpAdmin = New System.Windows.Forms.GroupBox()
        Me.Label47 = New System.Windows.Forms.Label()
        Me.rbResourceGuide = New System.Windows.Forms.RadioButton()
        Me.rbDuplResource = New System.Windows.Forms.RadioButton()
        Me.rbDuplOrg = New System.Windows.Forms.RadioButton()
        Me.rbDelivra = New System.Windows.Forms.RadioButton()
        Me.rbInvalidEmail = New System.Windows.Forms.RadioButton()
        Me.rbLib = New System.Windows.Forms.RadioButton()
        Me.grpQuarter = New System.Windows.Forms.GroupBox()
        Me.rbQtrAll = New System.Windows.Forms.RadioButton()
        Me.rbQtr4 = New System.Windows.Forms.RadioButton()
        Me.rbQtr3 = New System.Windows.Forms.RadioButton()
        Me.rbQtr2 = New System.Windows.Forms.RadioButton()
        Me.rbQtr1 = New System.Windows.Forms.RadioButton()
        Me.cboYear = New InfoCtr.ComboBoxRelaxed()
        Me.pnlRptSelect = New System.Windows.Forms.Panel()
        Me.Label37 = New System.Windows.Forms.Label()
        Me.lblDescription = New System.Windows.Forms.Label()
        Me.TabControl2 = New System.Windows.Forms.TabControl()
        Me.tbDate = New System.Windows.Forms.TabPage()
        Me.Label45 = New System.Windows.Forms.Label()
        Me.lblDatePertainsTo = New System.Windows.Forms.Label()
        Me.pnlDate = New System.Windows.Forms.Panel()
        Me.cboCycle = New InfoCtr.ComboBoxRelaxed()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label44 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.pnlDateRange = New System.Windows.Forms.Panel()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.dtEnd = New System.Windows.Forms.DateTimePicker()
        Me.dtStart = New System.Windows.Forms.DateTimePicker()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cboTop10 = New InfoCtr.ComboBoxRelaxed()
        Me.lblTop10 = New System.Windows.Forms.Label()
        Me.pnlGrantCycle = New System.Windows.Forms.Panel()
        Me.tbRegion = New System.Windows.Forms.TabPage()
        Me.trvSize = New System.Windows.Forms.TreeView()
        Me.Label43 = New System.Windows.Forms.Label()
        Me.Label42 = New System.Windows.Forms.Label()
        Me.Label41 = New System.Windows.Forms.Label()
        Me.trvStaff = New System.Windows.Forms.TreeView()
        Me.trvRegion = New System.Windows.Forms.TreeView()
        Me.tbCRG = New System.Windows.Forms.TabPage()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.trvCRG = New System.Windows.Forms.TreeView()
        Me.trvType = New System.Windows.Forms.TreeView()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.Label36 = New System.Windows.Forms.Label()
        Me.Label35 = New System.Windows.Forms.Label()
        Me.pnlBookList = New System.Windows.Forms.Panel()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.cboShelf = New InfoCtr.ComboBoxRelaxed()
        Me.grpOnCRG = New System.Windows.Forms.GroupBox()
        Me.rbCRGNo = New System.Windows.Forms.RadioButton()
        Me.rbCRGEither = New System.Windows.Forms.RadioButton()
        Me.rbCRGYes = New System.Windows.Forms.RadioButton()
        Me.oldpnlCRG = New System.Windows.Forms.Panel()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.txtCRG = New System.Windows.Forms.TextBox()
        Me.txtIndex = New System.Windows.Forms.TextBox()
        Me.ComboBox1 = New InfoCtr.ComboBoxRelaxed()
        Me.txtTitle = New System.Windows.Forms.TextBox()
        Me.chkIndex = New System.Windows.Forms.CheckBox()
        Me.chkCRG = New System.Windows.Forms.CheckBox()
        Me.chkTitle = New System.Windows.Forms.CheckBox()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.btnGo = New System.Windows.Forms.Button()
        Me.cboReport = New InfoCtr.ComboBoxRelaxed()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.PrintPreviewDialog1 = New System.Windows.Forms.PrintPreviewDialog()
        Me.PrintDocument1 = New System.Drawing.Printing.PrintDocument()
        Me.PrintDialog1 = New System.Windows.Forms.PrintDialog()
        Me.MainMenu1 = New System.Windows.Forms.MainMenu(Me.components)
        Me.MenuItem1 = New System.Windows.Forms.MenuItem()
        Me.miExport = New System.Windows.Forms.MenuItem()
        Me.miPrint = New System.Windows.Forms.MenuItem()
        Me.miPageSetup = New System.Windows.Forms.MenuItem()
        Me.miPreview = New System.Windows.Forms.MenuItem()
        Me.miClose = New System.Windows.Forms.MenuItem()
        Me.PageSetupDialog1 = New System.Windows.Forms.PageSetupDialog()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.StatusLabel = New System.Windows.Forms.ToolStripStatusLabel()
        Me.StatusProgressBar = New System.Windows.Forms.ToolStripProgressBar()
        Me.btnEmailTest = New System.Windows.Forms.Button()
        Me.Label46 = New System.Windows.Forms.Label()
        Me.Label48 = New System.Windows.Forms.Label()
        Me.lblQuarter = New System.Windows.Forms.Label()
        Me.GroupBox1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.grpReports.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.grpQuick.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        Me.grpAdmin.SuspendLayout()
        Me.grpQuarter.SuspendLayout()
        Me.pnlRptSelect.SuspendLayout()
        Me.TabControl2.SuspendLayout()
        Me.tbDate.SuspendLayout()
        Me.pnlDate.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.pnlDateRange.SuspendLayout()
        Me.tbRegion.SuspendLayout()
        Me.tbCRG.SuspendLayout()
        Me.pnlBookList.SuspendLayout()
        Me.grpOnCRG.SuspendLayout()
        Me.oldpnlCRG.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox1.Controls.Add(Me.rbGQI)
        Me.GroupBox1.Controls.Add(Me.rbYellow)
        Me.GroupBox1.Controls.Add(Me.rbAdmin)
        Me.GroupBox1.Controls.Add(Me.rbGeography)
        Me.GroupBox1.Controls.Add(Me.rbStats)
        Me.GroupBox1.Controls.Add(Me.rbEmail)
        Me.GroupBox1.Controls.Add(Me.rbResources)
        Me.GroupBox1.Controls.Add(Me.rbMail)
        Me.GroupBox1.Controls.Add(Me.rbLibrary)
        Me.GroupBox1.Controls.Add(Me.RadioButton3)
        Me.GroupBox1.Controls.Add(Me.rbCurrent)
        Me.GroupBox1.Location = New System.Drawing.Point(619, 23)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(152, 47)
        Me.GroupBox1.TabIndex = 3
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Reports and Utilities"
        Me.GroupBox1.Visible = False
        '
        'rbGQI
        '
        Me.rbGQI.Appearance = System.Windows.Forms.Appearance.Button
        Me.rbGQI.BackColor = System.Drawing.Color.LightSteelBlue
        Me.rbGQI.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbGQI.Location = New System.Drawing.Point(8, 338)
        Me.rbGQI.Name = "rbGQI"
        Me.rbGQI.Size = New System.Drawing.Size(163, 32)
        Me.rbGQI.TabIndex = 10
        Me.rbGQI.Text = "Build Your Own Query"
        Me.rbGQI.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.rbGQI.UseVisualStyleBackColor = False
        '
        'rbYellow
        '
        Me.rbYellow.Appearance = System.Windows.Forms.Appearance.Button
        Me.rbYellow.BackColor = System.Drawing.Color.PaleGoldenrod
        Me.rbYellow.ForeColor = System.Drawing.SystemColors.ControlDark
        Me.rbYellow.Location = New System.Drawing.Point(8, 307)
        Me.rbYellow.Name = "rbYellow"
        Me.rbYellow.Size = New System.Drawing.Size(163, 32)
        Me.rbYellow.TabIndex = 9
        Me.rbYellow.Tag = "Request"
        Me.rbYellow.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.rbYellow.UseVisualStyleBackColor = False
        '
        'rbAdmin
        '
        Me.rbAdmin.Appearance = System.Windows.Forms.Appearance.Button
        Me.rbAdmin.ForeColor = System.Drawing.SystemColors.ControlText
        Me.rbAdmin.Location = New System.Drawing.Point(8, 396)
        Me.rbAdmin.Name = "rbAdmin"
        Me.rbAdmin.Size = New System.Drawing.Size(161, 32)
        Me.rbAdmin.TabIndex = 9
        Me.rbAdmin.Text = "System &Administration"
        Me.rbAdmin.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.ToolTip1.SetToolTip(Me.rbAdmin, "Reset grant numbers, move Contacts")
        Me.rbAdmin.Visible = False
        '
        'rbGeography
        '
        Me.rbGeography.Appearance = System.Windows.Forms.Appearance.Button
        Me.rbGeography.BackColor = System.Drawing.Color.PaleVioletRed
        Me.rbGeography.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbGeography.Location = New System.Drawing.Point(8, 121)
        Me.rbGeography.Name = "rbGeography"
        Me.rbGeography.Size = New System.Drawing.Size(163, 32)
        Me.rbGeography.TabIndex = 3
        Me.rbGeography.Tag = "Indiana"
        Me.rbGeography.Text = "&Indiana/Satellite Regions"
        Me.rbGeography.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.rbGeography.UseVisualStyleBackColor = False
        '
        'rbStats
        '
        Me.rbStats.Appearance = System.Windows.Forms.Appearance.Button
        Me.rbStats.BackColor = System.Drawing.Color.PaleVioletRed
        Me.rbStats.ForeColor = System.Drawing.SystemColors.ControlText
        Me.rbStats.Location = New System.Drawing.Point(8, 276)
        Me.rbStats.Name = "rbStats"
        Me.rbStats.Size = New System.Drawing.Size(163, 32)
        Me.rbStats.TabIndex = 8
        Me.rbStats.Tag = "Statistics"
        Me.rbStats.Text = "&Statistical Reports"
        Me.rbStats.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.rbStats.UseVisualStyleBackColor = False
        '
        'rbEmail
        '
        Me.rbEmail.Appearance = System.Windows.Forms.Appearance.Button
        Me.rbEmail.BackColor = System.Drawing.Color.DarkSeaGreen
        Me.rbEmail.Enabled = False
        Me.rbEmail.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.rbEmail.Location = New System.Drawing.Point(8, 90)
        Me.rbEmail.Name = "rbEmail"
        Me.rbEmail.Size = New System.Drawing.Size(163, 32)
        Me.rbEmail.TabIndex = 2
        Me.rbEmail.Tag = "Email"
        Me.rbEmail.Text = "&E-mail Groups"
        Me.rbEmail.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.rbEmail.UseVisualStyleBackColor = False
        '
        'rbResources
        '
        Me.rbResources.Appearance = System.Windows.Forms.Appearance.Button
        Me.rbResources.BackColor = System.Drawing.Color.DarkSeaGreen
        Me.rbResources.ForeColor = System.Drawing.SystemColors.ControlText
        Me.rbResources.Location = New System.Drawing.Point(8, 245)
        Me.rbResources.Name = "rbResources"
        Me.rbResources.Size = New System.Drawing.Size(163, 32)
        Me.rbResources.TabIndex = 7
        Me.rbResources.Tag = "Resource"
        Me.rbResources.Text = "&Resource Reports"
        Me.rbResources.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.ToolTip1.SetToolTip(Me.rbResources, "New Resources, ")
        Me.rbResources.UseVisualStyleBackColor = False
        '
        'rbMail
        '
        Me.rbMail.Appearance = System.Windows.Forms.Appearance.Button
        Me.rbMail.BackColor = System.Drawing.Color.LightSteelBlue
        Me.rbMail.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbMail.ForeColor = System.Drawing.SystemColors.ControlText
        Me.rbMail.Location = New System.Drawing.Point(8, 183)
        Me.rbMail.Name = "rbMail"
        Me.rbMail.Size = New System.Drawing.Size(163, 32)
        Me.rbMail.TabIndex = 5
        Me.rbMail.Tag = "Labels"
        Me.rbMail.Text = "&Mailing Labels"
        Me.rbMail.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.rbMail.UseVisualStyleBackColor = False
        '
        'rbLibrary
        '
        Me.rbLibrary.Appearance = System.Windows.Forms.Appearance.Button
        Me.rbLibrary.BackColor = System.Drawing.Color.PaleGoldenrod
        Me.rbLibrary.ForeColor = System.Drawing.SystemColors.ControlText
        Me.rbLibrary.Location = New System.Drawing.Point(8, 152)
        Me.rbLibrary.Name = "rbLibrary"
        Me.rbLibrary.Size = New System.Drawing.Size(163, 32)
        Me.rbLibrary.TabIndex = 4
        Me.rbLibrary.Tag = "Library"
        Me.rbLibrary.Text = "&Library Labels and Reports"
        Me.rbLibrary.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.rbLibrary.UseVisualStyleBackColor = False
        '
        'RadioButton3
        '
        Me.RadioButton3.Appearance = System.Windows.Forms.Appearance.Button
        Me.RadioButton3.BackColor = System.Drawing.Color.Thistle
        Me.RadioButton3.Enabled = False
        Me.RadioButton3.ForeColor = System.Drawing.SystemColors.ControlDark
        Me.RadioButton3.Location = New System.Drawing.Point(8, 214)
        Me.RadioButton3.Name = "RadioButton3"
        Me.RadioButton3.Size = New System.Drawing.Size(163, 32)
        Me.RadioButton3.TabIndex = 6
        Me.RadioButton3.Tag = "MGI"
        Me.RadioButton3.Text = "Major %Grant Initiative Apps"
        Me.RadioButton3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.RadioButton3.UseVisualStyleBackColor = False
        '
        'rbCurrent
        '
        Me.rbCurrent.Appearance = System.Windows.Forms.Appearance.Button
        Me.rbCurrent.BackColor = System.Drawing.Color.Thistle
        Me.rbCurrent.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbCurrent.Location = New System.Drawing.Point(8, 59)
        Me.rbCurrent.Name = "rbCurrent"
        Me.rbCurrent.Size = New System.Drawing.Size(163, 32)
        Me.rbCurrent.TabIndex = 1
        Me.rbCurrent.Tag = "Current"
        Me.rbCurrent.Text = "&Current Activity"
        Me.rbCurrent.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.rbCurrent.UseVisualStyleBackColor = False
        '
        'rbCRG
        '
        Me.rbCRG.Appearance = System.Windows.Forms.Appearance.Button
        Me.rbCRG.BackColor = System.Drawing.Color.LightSteelBlue
        Me.rbCRG.ForeColor = System.Drawing.SystemColors.ControlText
        Me.rbCRG.Location = New System.Drawing.Point(743, 12)
        Me.rbCRG.Name = "rbCRG"
        Me.rbCRG.Size = New System.Drawing.Size(163, 32)
        Me.rbCRG.TabIndex = 0
        Me.rbCRG.Tag = "CRG"
        Me.rbCRG.Text = "CRG &Terminology"
        Me.rbCRG.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.rbCRG.UseVisualStyleBackColor = False
        '
        'rbMapZip
        '
        Me.rbMapZip.Appearance = System.Windows.Forms.Appearance.Button
        Me.rbMapZip.AutoSize = True
        Me.rbMapZip.FlatAppearance.BorderColor = System.Drawing.Color.Fuchsia
        Me.rbMapZip.Location = New System.Drawing.Point(17, 359)
        Me.rbMapZip.MinimumSize = New System.Drawing.Size(0, 30)
        Me.rbMapZip.Name = "rbMapZip"
        Me.rbMapZip.Size = New System.Drawing.Size(139, 30)
        Me.rbMapZip.TabIndex = 1
        Me.rbMapZip.TabStop = True
        Me.rbMapZip.Tag = "MapZips"
        Me.rbMapZip.Text = "Satellite Region Detail"
        Me.ToolTip1.SetToolTip(Me.rbMapZip, "list cities/zips in a region, map it online")
        Me.rbMapZip.UseVisualStyleBackColor = True
        '
        'TabPage1
        '
        Me.TabPage1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.TabPage1.Controls.Add(Me.Label8)
        Me.TabPage1.Controls.Add(Me.grpReports)
        Me.TabPage1.Location = New System.Drawing.Point(4, 4)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(202, 467)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "   REPORTS   "
        Me.ToolTip1.SetToolTip(Me.TabPage1, "REPORTS with Date and other variables.")
        Me.TabPage1.ToolTipText = "REPORTS with Date and other variables."
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Comic Sans MS", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(4, 3)
        Me.Label8.MinimumSize = New System.Drawing.Size(192, 35)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(192, 35)
        Me.Label8.TabIndex = 3
        Me.Label8.Text = "Reports with variable choices"
        '
        'grpReports
        '
        Me.grpReports.Controls.Add(Me.rbBoard)
        Me.grpReports.Controls.Add(Me.Label38)
        Me.grpReports.Controls.Add(Me.rbWorkshop)
        Me.grpReports.Controls.Add(Me.rbResource)
        Me.grpReports.Controls.Add(Me.rbGrant)
        Me.grpReports.Controls.Add(Me.rbOrg)
        Me.grpReports.Controls.Add(Me.rbCase)
        Me.grpReports.Controls.Add(Me.rbMailing)
        Me.grpReports.Controls.Add(Me.rbMapZip)
        Me.grpReports.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpReports.Location = New System.Drawing.Point(4, 36)
        Me.grpReports.Name = "grpReports"
        Me.grpReports.Padding = New System.Windows.Forms.Padding(0)
        Me.grpReports.Size = New System.Drawing.Size(192, 430)
        Me.grpReports.TabIndex = 2
        Me.grpReports.TabStop = False
        '
        'rbBoard
        '
        Me.rbBoard.AutoSize = True
        Me.rbBoard.ForeColor = System.Drawing.SystemColors.ControlText
        Me.rbBoard.Location = New System.Drawing.Point(5, 204)
        Me.rbBoard.Name = "rbBoard"
        Me.rbBoard.Size = New System.Drawing.Size(104, 19)
        Me.rbBoard.TabIndex = 9
        Me.rbBoard.TabStop = True
        Me.rbBoard.Tag = "Board"
        Me.rbBoard.Text = "Board Reports"
        Me.rbBoard.UseVisualStyleBackColor = True
        '
        'Label38
        '
        Me.Label38.AutoSize = True
        Me.Label38.Font = New System.Drawing.Font("Comic Sans MS", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label38.Location = New System.Drawing.Point(13, 256)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(136, 32)
        Me.Label38.TabIndex = 8
        Me.Label38.Text = "Advanced Reports" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "opening in new window:"
        '
        'rbWorkshop
        '
        Me.rbWorkshop.AutoSize = True
        Me.rbWorkshop.ForeColor = System.Drawing.SystemColors.ControlText
        Me.rbWorkshop.Location = New System.Drawing.Point(5, 169)
        Me.rbWorkshop.Name = "rbWorkshop"
        Me.rbWorkshop.Size = New System.Drawing.Size(61, 19)
        Me.rbWorkshop.TabIndex = 5
        Me.rbWorkshop.TabStop = True
        Me.rbWorkshop.Tag = "Events"
        Me.rbWorkshop.Text = "Events"
        Me.rbWorkshop.UseVisualStyleBackColor = True
        '
        'rbResource
        '
        Me.rbResource.AutoSize = True
        Me.rbResource.ForeColor = System.Drawing.SystemColors.ControlText
        Me.rbResource.Location = New System.Drawing.Point(4, 131)
        Me.rbResource.Name = "rbResource"
        Me.rbResource.Size = New System.Drawing.Size(84, 19)
        Me.rbResource.TabIndex = 4
        Me.rbResource.TabStop = True
        Me.rbResource.Tag = "Resources"
        Me.rbResource.Text = "Resources"
        Me.rbResource.UseVisualStyleBackColor = True
        '
        'rbGrant
        '
        Me.rbGrant.AutoSize = True
        Me.rbGrant.ForeColor = System.Drawing.SystemColors.ControlText
        Me.rbGrant.Location = New System.Drawing.Point(4, 93)
        Me.rbGrant.Name = "rbGrant"
        Me.rbGrant.Size = New System.Drawing.Size(61, 19)
        Me.rbGrant.TabIndex = 3
        Me.rbGrant.TabStop = True
        Me.rbGrant.Tag = "Grants"
        Me.rbGrant.Text = "Grants"
        Me.rbGrant.UseVisualStyleBackColor = True
        '
        'rbOrg
        '
        Me.rbOrg.AutoSize = True
        Me.rbOrg.ForeColor = System.Drawing.SystemColors.ControlText
        Me.rbOrg.Location = New System.Drawing.Point(4, 59)
        Me.rbOrg.Name = "rbOrg"
        Me.rbOrg.Size = New System.Drawing.Size(162, 19)
        Me.rbOrg.TabIndex = 2
        Me.rbOrg.TabStop = True
        Me.rbOrg.Tag = "ContactsOrgs"
        Me.rbOrg.Text = "Contacts && Organizations"
        Me.rbOrg.UseVisualStyleBackColor = True
        '
        'rbCase
        '
        Me.rbCase.AutoSize = True
        Me.rbCase.ForeColor = System.Drawing.SystemColors.ControlText
        Me.rbCase.Location = New System.Drawing.Point(6, 25)
        Me.rbCase.Name = "rbCase"
        Me.rbCase.Size = New System.Drawing.Size(59, 19)
        Me.rbCase.TabIndex = 1
        Me.rbCase.TabStop = True
        Me.rbCase.Tag = "Cases"
        Me.rbCase.Text = "Cases"
        Me.rbCase.UseVisualStyleBackColor = True
        '
        'rbMailing
        '
        Me.rbMailing.Appearance = System.Windows.Forms.Appearance.Button
        Me.rbMailing.AutoSize = True
        Me.rbMailing.Location = New System.Drawing.Point(17, 304)
        Me.rbMailing.MaximumSize = New System.Drawing.Size(156, 0)
        Me.rbMailing.MinimumSize = New System.Drawing.Size(100, 40)
        Me.rbMailing.Name = "rbMailing"
        Me.rbMailing.Size = New System.Drawing.Size(100, 40)
        Me.rbMailing.TabIndex = 6
        Me.rbMailing.TabStop = True
        Me.rbMailing.Tag = "Mailing"
        Me.rbMailing.Text = "Mailing Lists"
        Me.rbMailing.UseVisualStyleBackColor = True
        '
        'TabPage2
        '
        Me.TabPage2.BackColor = System.Drawing.Color.DarkSeaGreen
        Me.TabPage2.Controls.Add(Me.Label39)
        Me.TabPage2.Controls.Add(Me.grpQuick)
        Me.TabPage2.Location = New System.Drawing.Point(4, 4)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(202, 467)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "   QUICK VIEW   "
        Me.ToolTip1.SetToolTip(Me.TabPage2, "QUICK VIEW reports with no variables.")
        Me.TabPage2.ToolTipText = "QUICK VIEW reports with no variables."
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'Label39
        '
        Me.Label39.AutoSize = True
        Me.Label39.Font = New System.Drawing.Font("Comic Sans MS", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label39.Location = New System.Drawing.Point(4, 3)
        Me.Label39.MinimumSize = New System.Drawing.Size(192, 35)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(192, 35)
        Me.Label39.TabIndex = 4
        Me.Label39.Text = "Quick View reports " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "with no variable choices"
        '
        'grpQuick
        '
        Me.grpQuick.BackColor = System.Drawing.Color.DarkSeaGreen
        Me.grpQuick.Controls.Add(Me.rbResourceType)
        Me.grpQuick.Controls.Add(Me.rbMGI)
        Me.grpQuick.Controls.Add(Me.rbCntResourceType)
        Me.grpQuick.Controls.Add(Me.rbListCRG)
        Me.grpQuick.Controls.Add(Me.rbNeverReviewed)
        Me.grpQuick.Controls.Add(Me.rbActiveCongr)
        Me.grpQuick.Controls.Add(Me.rbCurrentCases)
        Me.grpQuick.Controls.Add(Me.rbConvToDate)
        Me.grpQuick.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpQuick.Location = New System.Drawing.Point(4, 36)
        Me.grpQuick.Name = "grpQuick"
        Me.grpQuick.Size = New System.Drawing.Size(192, 430)
        Me.grpQuick.TabIndex = 0
        Me.grpQuick.TabStop = False
        '
        'rbResourceType
        '
        Me.rbResourceType.AutoSize = True
        Me.rbResourceType.Location = New System.Drawing.Point(6, 158)
        Me.rbResourceType.Name = "rbResourceType"
        Me.rbResourceType.Size = New System.Drawing.Size(125, 17)
        Me.rbResourceType.TabIndex = 11
        Me.rbResourceType.TabStop = True
        Me.rbResourceType.Tag = "ResourcesTypes"
        Me.rbResourceType.Text = "List: Resource Types"
        Me.rbResourceType.UseVisualStyleBackColor = True
        '
        'rbMGI
        '
        Me.rbMGI.AutoSize = True
        Me.rbMGI.Location = New System.Drawing.Point(6, 235)
        Me.rbMGI.Name = "rbMGI"
        Me.rbMGI.Size = New System.Drawing.Size(164, 17)
        Me.rbMGI.TabIndex = 10
        Me.rbMGI.TabStop = True
        Me.rbMGI.Tag = "MGI"
        Me.rbMGI.Text = "List: Current MGI Applications"
        Me.rbMGI.UseVisualStyleBackColor = True
        '
        'rbCntResourceType
        '
        Me.rbCntResourceType.AutoSize = True
        Me.rbCntResourceType.Location = New System.Drawing.Point(6, 191)
        Me.rbCntResourceType.Name = "rbCntResourceType"
        Me.rbCntResourceType.Size = New System.Drawing.Size(127, 17)
        Me.rbCntResourceType.TabIndex = 9
        Me.rbCntResourceType.TabStop = True
        Me.rbCntResourceType.Tag = "cntResources"
        Me.rbCntResourceType.Text = "# Resources by Type"
        Me.rbCntResourceType.UseVisualStyleBackColor = True
        '
        'rbListCRG
        '
        Me.rbListCRG.AutoSize = True
        Me.rbListCRG.Location = New System.Drawing.Point(6, 127)
        Me.rbListCRG.Name = "rbListCRG"
        Me.rbListCRG.Size = New System.Drawing.Size(102, 17)
        Me.rbListCRG.TabIndex = 8
        Me.rbListCRG.TabStop = True
        Me.rbListCRG.Tag = "CRGTerms"
        Me.rbListCRG.Text = "List: CRG Terms"
        Me.rbListCRG.UseVisualStyleBackColor = True
        '
        'rbNeverReviewed
        '
        Me.rbNeverReviewed.AutoSize = True
        Me.rbNeverReviewed.Location = New System.Drawing.Point(6, 334)
        Me.rbNeverReviewed.Name = "rbNeverReviewed"
        Me.rbNeverReviewed.Size = New System.Drawing.Size(106, 30)
        Me.rbNeverReviewed.TabIndex = 5
        Me.rbNeverReviewed.TabStop = True
        Me.rbNeverReviewed.Tag = "CongrNeverReviewed"
        Me.rbNeverReviewed.Text = "# Congregations " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Never Reviewed"
        Me.rbNeverReviewed.UseVisualStyleBackColor = True
        '
        'rbActiveCongr
        '
        Me.rbActiveCongr.AutoSize = True
        Me.rbActiveCongr.Location = New System.Drawing.Point(6, 20)
        Me.rbActiveCongr.Name = "rbActiveCongr"
        Me.rbActiveCongr.Size = New System.Drawing.Size(174, 17)
        Me.rbActiveCongr.TabIndex = 4
        Me.rbActiveCongr.TabStop = True
        Me.rbActiveCongr.Tag = "IndianaCongr"
        Me.rbActiveCongr.Text = "# Active Indiana Congregations"
        Me.rbActiveCongr.UseVisualStyleBackColor = True
        '
        'rbCurrentCases
        '
        Me.rbCurrentCases.AutoSize = True
        Me.rbCurrentCases.Location = New System.Drawing.Point(6, 92)
        Me.rbCurrentCases.Name = "rbCurrentCases"
        Me.rbCurrentCases.Size = New System.Drawing.Size(101, 17)
        Me.rbCurrentCases.TabIndex = 3
        Me.rbCurrentCases.TabStop = True
        Me.rbCurrentCases.Tag = "CurrentCases"
        Me.rbCurrentCases.Text = "# Current Cases"
        Me.rbCurrentCases.UseVisualStyleBackColor = True
        '
        'rbConvToDate
        '
        Me.rbConvToDate.AutoSize = True
        Me.rbConvToDate.Location = New System.Drawing.Point(6, 54)
        Me.rbConvToDate.Name = "rbConvToDate"
        Me.rbConvToDate.Size = New System.Drawing.Size(144, 17)
        Me.rbConvToDate.TabIndex = 2
        Me.rbConvToDate.TabStop = True
        Me.rbConvToDate.Tag = "ConvToDate"
        Me.rbConvToDate.Text = "# Conversations To-Date"
        Me.rbConvToDate.UseVisualStyleBackColor = True
        '
        'TabPage3
        '
        Me.TabPage3.BackColor = System.Drawing.Color.Thistle
        Me.TabPage3.Controls.Add(Me.Label40)
        Me.TabPage3.Controls.Add(Me.grpAdmin)
        Me.TabPage3.Location = New System.Drawing.Point(4, 4)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Size = New System.Drawing.Size(202, 467)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "  ADMIN UTILITIES   "
        Me.ToolTip1.SetToolTip(Me.TabPage3, "Admin Utilities for trained staff.")
        Me.TabPage3.ToolTipText = "Admin Utilities for trained staff."
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'Label40
        '
        Me.Label40.AutoSize = True
        Me.Label40.Font = New System.Drawing.Font("Comic Sans MS", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label40.Location = New System.Drawing.Point(4, 3)
        Me.Label40.MinimumSize = New System.Drawing.Size(192, 35)
        Me.Label40.Name = "Label40"
        Me.Label40.Size = New System.Drawing.Size(192, 35)
        Me.Label40.TabIndex = 6
        Me.Label40.Text = "Administrative Utilities" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & " for trained staff"
        '
        'grpAdmin
        '
        Me.grpAdmin.Controls.Add(Me.Label47)
        Me.grpAdmin.Controls.Add(Me.rbResourceGuide)
        Me.grpAdmin.Controls.Add(Me.rbDuplResource)
        Me.grpAdmin.Controls.Add(Me.rbDuplOrg)
        Me.grpAdmin.Controls.Add(Me.rbDelivra)
        Me.grpAdmin.Controls.Add(Me.rbInvalidEmail)
        Me.grpAdmin.Controls.Add(Me.rbLib)
        Me.grpAdmin.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpAdmin.Location = New System.Drawing.Point(4, 36)
        Me.grpAdmin.MinimumSize = New System.Drawing.Size(192, 430)
        Me.grpAdmin.Name = "grpAdmin"
        Me.grpAdmin.Size = New System.Drawing.Size(192, 430)
        Me.grpAdmin.TabIndex = 1
        Me.grpAdmin.TabStop = False
        '
        'Label47
        '
        Me.Label47.AutoSize = True
        Me.Label47.Location = New System.Drawing.Point(23, 259)
        Me.Label47.Name = "Label47"
        Me.Label47.Size = New System.Drawing.Size(154, 15)
        Me.Label47.TabIndex = 7
        Me.Label47.Text = "_____________________"
        '
        'rbResourceGuide
        '
        Me.rbResourceGuide.AutoSize = True
        Me.rbResourceGuide.ForeColor = System.Drawing.SystemColors.ControlText
        Me.rbResourceGuide.Location = New System.Drawing.Point(10, 363)
        Me.rbResourceGuide.Name = "rbResourceGuide"
        Me.rbResourceGuide.Size = New System.Drawing.Size(167, 19)
        Me.rbResourceGuide.TabIndex = 6
        Me.rbResourceGuide.TabStop = True
        Me.rbResourceGuide.Tag = "ResourceGuide"
        Me.rbResourceGuide.Text = "Process Resource Guides"
        Me.rbResourceGuide.UseVisualStyleBackColor = True
        '
        'rbDuplResource
        '
        Me.rbDuplResource.AutoSize = True
        Me.rbDuplResource.Enabled = False
        Me.rbDuplResource.ForeColor = System.Drawing.SystemColors.ControlText
        Me.rbDuplResource.Location = New System.Drawing.Point(9, 338)
        Me.rbDuplResource.Name = "rbDuplResource"
        Me.rbDuplResource.Size = New System.Drawing.Size(158, 19)
        Me.rbDuplResource.TabIndex = 5
        Me.rbDuplResource.TabStop = True
        Me.rbDuplResource.Tag = "DuplResources"
        Me.rbDuplResource.Text = "Fix Duplicate Resources"
        Me.rbDuplResource.UseVisualStyleBackColor = True
        '
        'rbDuplOrg
        '
        Me.rbDuplOrg.AutoSize = True
        Me.rbDuplOrg.Enabled = False
        Me.rbDuplOrg.ForeColor = System.Drawing.SystemColors.ControlText
        Me.rbDuplOrg.Location = New System.Drawing.Point(9, 315)
        Me.rbDuplOrg.Name = "rbDuplOrg"
        Me.rbDuplOrg.Size = New System.Drawing.Size(175, 19)
        Me.rbDuplOrg.TabIndex = 4
        Me.rbDuplOrg.TabStop = True
        Me.rbDuplOrg.Tag = "DuplOrgs"
        Me.rbDuplOrg.Text = "Fix Duplicate Orgs/Contacts"
        Me.rbDuplOrg.UseVisualStyleBackColor = True
        '
        'rbDelivra
        '
        Me.rbDelivra.AutoSize = True
        Me.rbDelivra.ForeColor = System.Drawing.SystemColors.ControlText
        Me.rbDelivra.Location = New System.Drawing.Point(8, 291)
        Me.rbDelivra.Name = "rbDelivra"
        Me.rbDelivra.Size = New System.Drawing.Size(164, 19)
        Me.rbDelivra.TabIndex = 3
        Me.rbDelivra.TabStop = True
        Me.rbDelivra.Tag = "Delivra"
        Me.rbDelivra.Text = "Upload Delivra Email File"
        Me.rbDelivra.UseVisualStyleBackColor = True
        '
        'rbInvalidEmail
        '
        Me.rbInvalidEmail.AutoSize = True
        Me.rbInvalidEmail.ForeColor = System.Drawing.SystemColors.ControlText
        Me.rbInvalidEmail.Location = New System.Drawing.Point(20, 29)
        Me.rbInvalidEmail.Name = "rbInvalidEmail"
        Me.rbInvalidEmail.Size = New System.Drawing.Size(128, 19)
        Me.rbInvalidEmail.TabIndex = 2
        Me.rbInvalidEmail.TabStop = True
        Me.rbInvalidEmail.Tag = "InvalidEmail"
        Me.rbInvalidEmail.Text = "Find Invalid Emails"
        Me.rbInvalidEmail.UseVisualStyleBackColor = True
        '
        'rbLib
        '
        Me.rbLib.AutoSize = True
        Me.rbLib.ForeColor = System.Drawing.SystemColors.ControlText
        Me.rbLib.Location = New System.Drawing.Point(20, 65)
        Me.rbLib.Name = "rbLib"
        Me.rbLib.Size = New System.Drawing.Size(133, 19)
        Me.rbLib.TabIndex = 1
        Me.rbLib.TabStop = True
        Me.rbLib.Tag = "Library"
        Me.rbLib.Text = "Label Library Books"
        Me.rbLib.UseVisualStyleBackColor = True
        '
        'grpQuarter
        '
        Me.grpQuarter.Controls.Add(Me.rbQtrAll)
        Me.grpQuarter.Controls.Add(Me.rbQtr4)
        Me.grpQuarter.Controls.Add(Me.rbQtr3)
        Me.grpQuarter.Controls.Add(Me.rbQtr2)
        Me.grpQuarter.Controls.Add(Me.rbQtr1)
        Me.grpQuarter.Location = New System.Drawing.Point(6, 66)
        Me.grpQuarter.Name = "grpQuarter"
        Me.grpQuarter.Padding = New System.Windows.Forms.Padding(0)
        Me.grpQuarter.Size = New System.Drawing.Size(184, 49)
        Me.grpQuarter.TabIndex = 8
        Me.grpQuarter.TabStop = False
        Me.grpQuarter.Tag = "allyear"
        Me.ToolTip1.SetToolTip(Me.grpQuarter, "based on Year dropdown above")
        '
        'rbQtrAll
        '
        Me.rbQtrAll.AutoSize = True
        Me.rbQtrAll.Checked = True
        Me.rbQtrAll.Location = New System.Drawing.Point(104, 16)
        Me.rbQtrAll.Name = "rbQtrAll"
        Me.rbQtrAll.Size = New System.Drawing.Size(66, 17)
        Me.rbQtrAll.TabIndex = 4
        Me.rbQtrAll.TabStop = True
        Me.rbQtrAll.Tag = "5"
        Me.rbQtrAll.Text = "Full Year"
        Me.ToolTip1.SetToolTip(Me.rbQtrAll, "resets to full year")
        Me.rbQtrAll.UseVisualStyleBackColor = True
        '
        'rbQtr4
        '
        Me.rbQtr4.AutoSize = True
        Me.rbQtr4.Location = New System.Drawing.Point(52, 25)
        Me.rbQtr4.Name = "rbQtr4"
        Me.rbQtr4.Size = New System.Drawing.Size(40, 17)
        Me.rbQtr4.TabIndex = 3
        Me.rbQtr4.TabStop = True
        Me.rbQtr4.Tag = "4"
        Me.rbQtr4.Text = "4th"
        Me.rbQtr4.UseVisualStyleBackColor = True
        '
        'rbQtr3
        '
        Me.rbQtr3.AutoSize = True
        Me.rbQtr3.Location = New System.Drawing.Point(3, 25)
        Me.rbQtr3.Name = "rbQtr3"
        Me.rbQtr3.Size = New System.Drawing.Size(40, 17)
        Me.rbQtr3.TabIndex = 2
        Me.rbQtr3.TabStop = True
        Me.rbQtr3.Tag = "3"
        Me.rbQtr3.Text = "3rd"
        Me.rbQtr3.UseVisualStyleBackColor = True
        '
        'rbQtr2
        '
        Me.rbQtr2.AutoSize = True
        Me.rbQtr2.Location = New System.Drawing.Point(50, 6)
        Me.rbQtr2.Name = "rbQtr2"
        Me.rbQtr2.Size = New System.Drawing.Size(43, 17)
        Me.rbQtr2.TabIndex = 1
        Me.rbQtr2.TabStop = True
        Me.rbQtr2.Tag = "2"
        Me.rbQtr2.Text = "2nd"
        Me.rbQtr2.UseVisualStyleBackColor = True
        '
        'rbQtr1
        '
        Me.rbQtr1.AutoSize = True
        Me.rbQtr1.Location = New System.Drawing.Point(3, 6)
        Me.rbQtr1.Name = "rbQtr1"
        Me.rbQtr1.Size = New System.Drawing.Size(39, 17)
        Me.rbQtr1.TabIndex = 0
        Me.rbQtr1.TabStop = True
        Me.rbQtr1.Tag = "1"
        Me.rbQtr1.Text = "1st"
        Me.rbQtr1.UseVisualStyleBackColor = True
        '
        'cboYear
        '
        Me.cboYear.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboYear.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboYear.FormattingEnabled = True
        Me.cboYear.Location = New System.Drawing.Point(6, 21)
        Me.cboYear.Name = "cboYear"
        Me.cboYear.RestrictContentToListItems = True
        Me.cboYear.Size = New System.Drawing.Size(97, 21)
        Me.cboYear.TabIndex = 10
        Me.ToolTip1.SetToolTip(Me.cboYear, "sets Start date to Jan 1 and End Date to Dec 31.")
        '
        'pnlRptSelect
        '
        Me.pnlRptSelect.BackColor = System.Drawing.Color.LightSteelBlue
        Me.pnlRptSelect.Controls.Add(Me.Label37)
        Me.pnlRptSelect.Controls.Add(Me.lblDescription)
        Me.pnlRptSelect.Controls.Add(Me.TabControl2)
        Me.pnlRptSelect.Controls.Add(Me.oldpnlCRG)
        Me.pnlRptSelect.Controls.Add(Me.Label5)
        Me.pnlRptSelect.Controls.Add(Me.Label10)
        Me.pnlRptSelect.Controls.Add(Me.btnGo)
        Me.pnlRptSelect.Controls.Add(Me.cboReport)
        Me.pnlRptSelect.Location = New System.Drawing.Point(279, 76)
        Me.pnlRptSelect.Name = "pnlRptSelect"
        Me.pnlRptSelect.Size = New System.Drawing.Size(775, 471)
        Me.pnlRptSelect.TabIndex = 227
        '
        'Label37
        '
        Me.Label37.AutoSize = True
        Me.Label37.ForeColor = System.Drawing.Color.Maroon
        Me.Label37.Location = New System.Drawing.Point(370, 17)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(95, 13)
        Me.Label37.TabIndex = 241
        Me.Label37.Text = "Report Description"
        '
        'lblDescription
        '
        Me.lblDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblDescription.Font = New System.Drawing.Font("Comic Sans MS", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDescription.Location = New System.Drawing.Point(365, 33)
        Me.lblDescription.MinimumSize = New System.Drawing.Size(350, 50)
        Me.lblDescription.Name = "lblDescription"
        Me.lblDescription.Size = New System.Drawing.Size(374, 146)
        Me.lblDescription.TabIndex = 234
        Me.lblDescription.Text = "Report Description:"
        '
        'TabControl2
        '
        Me.TabControl2.Controls.Add(Me.tbDate)
        Me.TabControl2.Controls.Add(Me.tbRegion)
        Me.TabControl2.Controls.Add(Me.tbCRG)
        Me.TabControl2.Location = New System.Drawing.Point(16, 186)
        Me.TabControl2.Name = "TabControl2"
        Me.TabControl2.SelectedIndex = 0
        Me.TabControl2.Size = New System.Drawing.Size(723, 274)
        Me.TabControl2.TabIndex = 240
        '
        'tbDate
        '
        Me.tbDate.Controls.Add(Me.Label45)
        Me.tbDate.Controls.Add(Me.lblDatePertainsTo)
        Me.tbDate.Controls.Add(Me.pnlDate)
        Me.tbDate.Controls.Add(Me.cboTop10)
        Me.tbDate.Controls.Add(Me.lblTop10)
        Me.tbDate.Controls.Add(Me.pnlGrantCycle)
        Me.tbDate.Location = New System.Drawing.Point(4, 22)
        Me.tbDate.Name = "tbDate"
        Me.tbDate.Size = New System.Drawing.Size(715, 248)
        Me.tbDate.TabIndex = 2
        Me.tbDate.Text = "Dates              "
        Me.tbDate.UseVisualStyleBackColor = True
        '
        'Label45
        '
        Me.Label45.AutoSize = True
        Me.Label45.ForeColor = System.Drawing.SystemColors.Highlight
        Me.Label45.Location = New System.Drawing.Point(25, 199)
        Me.Label45.Name = "Label45"
        Me.Label45.Size = New System.Drawing.Size(121, 13)
        Me.Label45.TabIndex = 35
        Me.Label45.Text = "HOW MANY RESULTS"
        Me.Label45.Visible = False
        '
        'lblDatePertainsTo
        '
        Me.lblDatePertainsTo.AutoSize = True
        Me.lblDatePertainsTo.Location = New System.Drawing.Point(533, 187)
        Me.lblDatePertainsTo.Name = "lblDatePertainsTo"
        Me.lblDatePertainsTo.Size = New System.Drawing.Size(10, 13)
        Me.lblDatePertainsTo.TabIndex = 43
        Me.lblDatePertainsTo.Text = " "
        '
        'pnlDate
        '
        Me.pnlDate.Controls.Add(Me.cboCycle)
        Me.pnlDate.Controls.Add(Me.Label19)
        Me.pnlDate.Controls.Add(Me.Label44)
        Me.pnlDate.Controls.Add(Me.Label17)
        Me.pnlDate.Controls.Add(Me.Label13)
        Me.pnlDate.Controls.Add(Me.Panel1)
        Me.pnlDate.Controls.Add(Me.pnlDateRange)
        Me.pnlDate.Location = New System.Drawing.Point(16, 10)
        Me.pnlDate.Name = "pnlDate"
        Me.pnlDate.Size = New System.Drawing.Size(683, 158)
        Me.pnlDate.TabIndex = 15
        '
        'cboCycle
        '
        Me.cboCycle.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboCycle.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboCycle.DisplayMember = "StatusName"
        Me.cboCycle.FormattingEnabled = True
        Me.cboCycle.Location = New System.Drawing.Point(468, 46)
        Me.cboCycle.Name = "cboCycle"
        Me.cboCycle.RestrictContentToListItems = True
        Me.cboCycle.Size = New System.Drawing.Size(181, 21)
        Me.cboCycle.TabIndex = 23
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.ForeColor = System.Drawing.Color.IndianRed
        Me.Label19.Location = New System.Drawing.Point(474, 97)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(194, 26)
        Me.Label19.TabIndex = 40
        Me.Label19.Text = "Enter the Start and End dates manually," & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "or set them using these options."
        Me.Label19.Visible = False
        '
        'Label44
        '
        Me.Label44.AutoSize = True
        Me.Label44.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label44.ForeColor = System.Drawing.SystemColors.Highlight
        Me.Label44.Location = New System.Drawing.Point(43, 6)
        Me.Label44.Name = "Label44"
        Me.Label44.Size = New System.Drawing.Size(87, 13)
        Me.Label44.TabIndex = 34
        Me.Label44.Text = "DATE RANGE"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(465, 32)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(62, 13)
        Me.Label17.TabIndex = 24
        Me.Label17.Text = "Grant Cycle"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.SystemColors.Highlight
        Me.Label13.Location = New System.Drawing.Point(267, 6)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(114, 13)
        Me.Label13.TabIndex = 14
        Me.Label13.Text = "Date Setting Shortcuts"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Label7)
        Me.Panel1.Controls.Add(Me.grpQuarter)
        Me.Panel1.Controls.Add(Me.Label12)
        Me.Panel1.Controls.Add(Me.cboYear)
        Me.Panel1.Location = New System.Drawing.Point(266, 25)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(196, 121)
        Me.Panel1.TabIndex = 21
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(7, 52)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(42, 13)
        Me.Label7.TabIndex = 15
        Me.Label7.Text = "Quarter"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(7, 7)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(29, 13)
        Me.Label12.TabIndex = 14
        Me.Label12.Text = "Year"
        '
        'pnlDateRange
        '
        Me.pnlDateRange.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pnlDateRange.Controls.Add(Me.Panel4)
        Me.pnlDateRange.Controls.Add(Me.dtEnd)
        Me.pnlDateRange.Controls.Add(Me.dtStart)
        Me.pnlDateRange.Controls.Add(Me.Label4)
        Me.pnlDateRange.Controls.Add(Me.Label2)
        Me.pnlDateRange.Location = New System.Drawing.Point(13, 25)
        Me.pnlDateRange.Name = "pnlDateRange"
        Me.pnlDateRange.Size = New System.Drawing.Size(197, 63)
        Me.pnlDateRange.TabIndex = 42
        '
        'Panel4
        '
        Me.Panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel4.Location = New System.Drawing.Point(246, 12)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(141, 38)
        Me.Panel4.TabIndex = 42
        '
        'dtEnd
        '
        Me.dtEnd.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtEnd.Location = New System.Drawing.Point(65, 33)
        Me.dtEnd.Name = "dtEnd"
        Me.dtEnd.Size = New System.Drawing.Size(119, 20)
        Me.dtEnd.TabIndex = 6
        '
        'dtStart
        '
        Me.dtStart.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtStart.Location = New System.Drawing.Point(64, 5)
        Me.dtStart.Name = "dtStart"
        Me.dtStart.Size = New System.Drawing.Size(121, 20)
        Me.dtStart.TabIndex = 2
        Me.dtStart.Value = New Date(2008, 2, 25, 0, 0, 0, 0)
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(3, 9)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(65, 13)
        Me.Label4.TabIndex = 5
        Me.Label4.Text = "Start Date"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(4, 37)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(60, 13)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "End Date"
        '
        'cboTop10
        '
        Me.cboTop10.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboTop10.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboTop10.FormattingEnabled = True
        Me.cboTop10.Items.AddRange(New Object() {"All", "5", "10", "25", "50"})
        Me.cboTop10.Location = New System.Drawing.Point(29, 217)
        Me.cboTop10.Name = "cboTop10"
        Me.cboTop10.RestrictContentToListItems = True
        Me.cboTop10.Size = New System.Drawing.Size(117, 21)
        Me.cboTop10.TabIndex = 0
        Me.cboTop10.Text = "All"
        Me.cboTop10.Visible = False
        '
        'lblTop10
        '
        Me.lblTop10.AutoSize = True
        Me.lblTop10.ForeColor = System.Drawing.SystemColors.Highlight
        Me.lblTop10.Location = New System.Drawing.Point(299, 199)
        Me.lblTop10.Name = "lblTop10"
        Me.lblTop10.Size = New System.Drawing.Size(98, 13)
        Me.lblTop10.TabIndex = 1
        Me.lblTop10.Text = " Recommendations"
        Me.lblTop10.Visible = False
        '
        'pnlGrantCycle
        '
        Me.pnlGrantCycle.Location = New System.Drawing.Point(602, 183)
        Me.pnlGrantCycle.Name = "pnlGrantCycle"
        Me.pnlGrantCycle.Size = New System.Drawing.Size(34, 17)
        Me.pnlGrantCycle.TabIndex = 41
        '
        'tbRegion
        '
        Me.tbRegion.Controls.Add(Me.trvSize)
        Me.tbRegion.Controls.Add(Me.Label43)
        Me.tbRegion.Controls.Add(Me.Label42)
        Me.tbRegion.Controls.Add(Me.Label41)
        Me.tbRegion.Controls.Add(Me.trvStaff)
        Me.tbRegion.Controls.Add(Me.trvRegion)
        Me.tbRegion.Location = New System.Drawing.Point(4, 22)
        Me.tbRegion.Name = "tbRegion"
        Me.tbRegion.Padding = New System.Windows.Forms.Padding(3)
        Me.tbRegion.Size = New System.Drawing.Size(715, 248)
        Me.tbRegion.TabIndex = 1
        Me.tbRegion.Text = "Center Staff, Region, Size of Congregation     "
        Me.tbRegion.UseVisualStyleBackColor = True
        '
        'trvSize
        '
        Me.trvSize.CheckBoxes = True
        Me.trvSize.Location = New System.Drawing.Point(497, 29)
        Me.trvSize.Name = "trvSize"
        TreeNode1.Name = "ndFamily"
        TreeNode1.Text = "1-49  (Family)"
        TreeNode2.Name = "ndPastoral"
        TreeNode2.Text = "50-149  (Pastoral)"
        TreeNode3.Name = "ndProgram"
        TreeNode3.Text = "150-349  (Program)"
        TreeNode4.Name = "ndCorporate"
        TreeNode4.Text = "350-999  (Corporate)"
        TreeNode5.Name = "ndMega"
        TreeNode5.Text = "1000 or more  (Mega)"
        TreeNode6.Name = "ndSizeAll"
        TreeNode6.Text = "AllSizes"
        Me.trvSize.Nodes.AddRange(New System.Windows.Forms.TreeNode() {TreeNode6})
        Me.trvSize.Size = New System.Drawing.Size(189, 207)
        Me.trvSize.TabIndex = 246
        '
        'Label43
        '
        Me.Label43.AutoSize = True
        Me.Label43.ForeColor = System.Drawing.SystemColors.Highlight
        Me.Label43.Location = New System.Drawing.Point(494, 11)
        Me.Label43.Name = "Label43"
        Me.Label43.Size = New System.Drawing.Size(105, 13)
        Me.Label43.TabIndex = 245
        Me.Label43.Text = "AVG ATTENDANCE"
        '
        'Label42
        '
        Me.Label42.AutoSize = True
        Me.Label42.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label42.ForeColor = System.Drawing.SystemColors.Highlight
        Me.Label42.Location = New System.Drawing.Point(257, 11)
        Me.Label42.Name = "Label42"
        Me.Label42.Size = New System.Drawing.Size(99, 13)
        Me.Label42.TabIndex = 244
        Me.Label42.Text = "CENTER STAFF"
        '
        'Label41
        '
        Me.Label41.AutoSize = True
        Me.Label41.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label41.ForeColor = System.Drawing.SystemColors.Highlight
        Me.Label41.Location = New System.Drawing.Point(14, 11)
        Me.Label41.Name = "Label41"
        Me.Label41.Size = New System.Drawing.Size(63, 13)
        Me.Label41.TabIndex = 243
        Me.Label41.Text = "REGIONS"
        '
        'trvStaff
        '
        Me.trvStaff.CheckBoxes = True
        Me.trvStaff.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.trvStaff.ForeColor = System.Drawing.SystemColors.WindowText
        Me.trvStaff.Location = New System.Drawing.Point(260, 29)
        Me.trvStaff.Name = "trvStaff"
        TreeNode7.Name = "ndAdmin"
        TreeNode7.Text = "Admin Asst"
        TreeNode8.Name = "ndConsultant"
        TreeNode8.Text = "Consultants"
        TreeNode9.Name = "ndSupport"
        TreeNode9.Text = "Support"
        TreeNode10.Name = "ndStaffActive"
        TreeNode10.Text = "All Active Staff"
        TreeNode11.Name = "ndStaffCentral"
        TreeNode11.Text = "Central"
        TreeNode12.Name = "ndStaffNE"
        TreeNode12.Text = "NE"
        TreeNode13.Name = "ndStaffNW"
        TreeNode13.Text = "NW"
        TreeNode14.Name = "ndStaffSE"
        TreeNode14.Text = "SE"
        TreeNode15.Name = "ndStaffSW"
        TreeNode15.Text = "SW"
        TreeNode16.Name = "ndStaffRegions"
        TreeNode16.Text = "All Regions"
        TreeNode17.Name = "ndStaffInactive"
        TreeNode17.Text = "Inactive"
        TreeNode18.Name = "ndAllStaff"
        TreeNode18.Text = "AllStaff"
        Me.trvStaff.Nodes.AddRange(New System.Windows.Forms.TreeNode() {TreeNode18})
        Me.trvStaff.Size = New System.Drawing.Size(222, 208)
        Me.trvStaff.TabIndex = 240
        '
        'trvRegion
        '
        Me.trvRegion.CheckBoxes = True
        Me.trvRegion.Location = New System.Drawing.Point(17, 29)
        Me.trvRegion.Name = "trvRegion"
        TreeNode19.Checked = True
        TreeNode19.Name = "Node1"
        TreeNode19.Text = "Central"
        TreeNode20.Checked = True
        TreeNode20.Name = "Node2"
        TreeNode20.Text = "NE"
        TreeNode21.Checked = True
        TreeNode21.Name = "Node3"
        TreeNode21.Text = "NW"
        TreeNode22.Checked = True
        TreeNode22.Name = "Node4"
        TreeNode22.Text = "SE"
        TreeNode23.Checked = True
        TreeNode23.Name = "Node5"
        TreeNode23.Text = "SW"
        TreeNode24.Checked = True
        TreeNode24.Name = "ndRegions"
        TreeNode24.Text = "Satellite Regions"
        TreeNode25.Name = "ndAllAreas"
        TreeNode25.Text = "All Areas"
        TreeNode26.Name = "ndUSA"
        TreeNode26.Text = "Outside Indiana"
        TreeNode27.Name = "ndIntl"
        TreeNode27.Text = "International"
        TreeNode28.Name = "ndFuture"
        TreeNode28.Text = "Future Use"
        Me.trvRegion.Nodes.AddRange(New System.Windows.Forms.TreeNode() {TreeNode25, TreeNode28})
        Me.trvRegion.Size = New System.Drawing.Size(228, 208)
        Me.trvRegion.TabIndex = 239
        '
        'tbCRG
        '
        Me.tbCRG.Controls.Add(Me.Label9)
        Me.tbCRG.Controls.Add(Me.Label1)
        Me.tbCRG.Controls.Add(Me.trvCRG)
        Me.tbCRG.Controls.Add(Me.trvType)
        Me.tbCRG.Controls.Add(Me.Label34)
        Me.tbCRG.Controls.Add(Me.Label36)
        Me.tbCRG.Controls.Add(Me.Label35)
        Me.tbCRG.Controls.Add(Me.pnlBookList)
        Me.tbCRG.Controls.Add(Me.grpOnCRG)
        Me.tbCRG.Location = New System.Drawing.Point(4, 22)
        Me.tbCRG.Name = "tbCRG"
        Me.tbCRG.Padding = New System.Windows.Forms.Padding(3)
        Me.tbCRG.Size = New System.Drawing.Size(715, 248)
        Me.tbCRG.TabIndex = 0
        Me.tbCRG.Text = "CRG, Type of Resource     "
        Me.tbCRG.UseVisualStyleBackColor = True
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.SystemColors.Highlight
        Me.Label9.Location = New System.Drawing.Point(573, 23)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(103, 13)
        Me.Label9.TabIndex = 236
        Me.Label9.Text = "On CRG Website"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Maroon
        Me.Label1.Location = New System.Drawing.Point(320, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(228, 13)
        Me.Label1.TabIndex = 235
        Me.Label1.Text = "Clear all checks, then Right-Click to search list."
        '
        'trvCRG
        '
        Me.trvCRG.CheckBoxes = True
        Me.trvCRG.Location = New System.Drawing.Point(221, 31)
        Me.trvCRG.Name = "trvCRG"
        TreeNode29.Name = "Node1"
        TreeNode29.Text = "All Administration"
        TreeNode30.Name = "Node2"
        TreeNode30.Text = "All Building Issues"
        TreeNode31.Name = "Node3"
        TreeNode31.Text = "All Congregational Vitality"
        TreeNode32.Name = "Node4"
        TreeNode32.Text = "All Leadership"
        TreeNode33.Name = "Node5"
        TreeNode33.Text = "All Miscellaneous"
        TreeNode34.Name = "Node6"
        TreeNode34.Text = "All Public Ministry"
        TreeNode35.Name = "Node7"
        TreeNode35.Text = "All Religion in America"
        TreeNode36.Name = "Node8"
        TreeNode36.Text = "All Specialized Ministry"
        TreeNode37.Name = "Node9"
        TreeNode37.Text = "All Spirituality"
        TreeNode38.Name = "Node10"
        TreeNode38.Text = "All Worship"
        TreeNode39.Name = "ndServices"
        TreeNode39.Text = "All Center Services"
        TreeNode40.Name = "ndAllCRG"
        TreeNode40.Text = "All CRG Terms"
        Me.trvCRG.Nodes.AddRange(New System.Windows.Forms.TreeNode() {TreeNode40})
        Me.trvCRG.Size = New System.Drawing.Size(327, 192)
        Me.trvCRG.TabIndex = 234
        '
        'trvType
        '
        Me.trvType.CheckBoxes = True
        Me.trvType.Location = New System.Drawing.Point(28, 32)
        Me.trvType.Name = "trvType"
        Me.trvType.Size = New System.Drawing.Size(157, 192)
        Me.trvType.TabIndex = 34
        '
        'Label34
        '
        Me.Label34.AutoSize = True
        Me.Label34.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label34.ForeColor = System.Drawing.SystemColors.Highlight
        Me.Label34.Location = New System.Drawing.Point(25, 15)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(39, 13)
        Me.Label34.TabIndex = 33
        Me.Label34.Text = "TYPE"
        '
        'Label36
        '
        Me.Label36.AutoSize = True
        Me.Label36.ForeColor = System.Drawing.SystemColors.Highlight
        Me.Label36.Location = New System.Drawing.Point(55, 232)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(200, 13)
        Me.Label36.TabIndex = 39
        Me.Label36.Text = "By default ALL items in list are searched. "
        Me.Label36.Visible = False
        '
        'Label35
        '
        Me.Label35.AutoSize = True
        Me.Label35.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label35.ForeColor = System.Drawing.SystemColors.Highlight
        Me.Label35.Location = New System.Drawing.Point(218, 16)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(72, 13)
        Me.Label35.TabIndex = 35
        Me.Label35.Text = "CRG TERM"
        '
        'pnlBookList
        '
        Me.pnlBookList.Controls.Add(Me.Label33)
        Me.pnlBookList.Controls.Add(Me.cboShelf)
        Me.pnlBookList.Location = New System.Drawing.Point(360, 308)
        Me.pnlBookList.Name = "pnlBookList"
        Me.pnlBookList.Size = New System.Drawing.Size(302, 27)
        Me.pnlBookList.TabIndex = 232
        Me.pnlBookList.Visible = False
        '
        'Label33
        '
        Me.Label33.AutoSize = True
        Me.Label33.ForeColor = System.Drawing.SystemColors.Highlight
        Me.Label33.Location = New System.Drawing.Point(8, 18)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(245, 13)
        Me.Label33.TabIndex = 33
        Me.Label33.Text = "Select a Shelf (Library, Collection, File Space, etc.)"
        '
        'cboShelf
        '
        Me.cboShelf.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboShelf.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboShelf.DisplayMember = "Locationname"
        Me.cboShelf.FormattingEnabled = True
        Me.cboShelf.Items.AddRange(New Object() {"Public Library", "Jewish Collection", "SSGI Collection"})
        Me.cboShelf.Location = New System.Drawing.Point(28, 43)
        Me.cboShelf.Name = "cboShelf"
        Me.cboShelf.RestrictContentToListItems = True
        Me.cboShelf.Size = New System.Drawing.Size(257, 21)
        Me.cboShelf.TabIndex = 32
        '
        'grpOnCRG
        '
        Me.grpOnCRG.Controls.Add(Me.rbCRGNo)
        Me.grpOnCRG.Controls.Add(Me.rbCRGEither)
        Me.grpOnCRG.Controls.Add(Me.rbCRGYes)
        Me.grpOnCRG.Location = New System.Drawing.Point(568, 7)
        Me.grpOnCRG.Name = "grpOnCRG"
        Me.grpOnCRG.Size = New System.Drawing.Size(117, 106)
        Me.grpOnCRG.TabIndex = 237
        Me.grpOnCRG.TabStop = False
        Me.grpOnCRG.Text = "  "
        Me.grpOnCRG.Visible = False
        '
        'rbCRGNo
        '
        Me.rbCRGNo.AutoSize = True
        Me.rbCRGNo.Location = New System.Drawing.Point(9, 60)
        Me.rbCRGNo.Name = "rbCRGNo"
        Me.rbCRGNo.Size = New System.Drawing.Size(102, 17)
        Me.rbCRGNo.TabIndex = 2
        Me.rbCRGNo.Text = "Not on CRG site"
        Me.rbCRGNo.UseVisualStyleBackColor = True
        '
        'rbCRGEither
        '
        Me.rbCRGEither.AutoSize = True
        Me.rbCRGEither.Checked = True
        Me.rbCRGEither.Location = New System.Drawing.Point(9, 82)
        Me.rbCRGEither.Name = "rbCRGEither"
        Me.rbCRGEither.Size = New System.Drawing.Size(94, 17)
        Me.rbCRGEither.TabIndex = 1
        Me.rbCRGEither.TabStop = True
        Me.rbCRGEither.Text = "Doesn't Matter"
        Me.rbCRGEither.UseVisualStyleBackColor = True
        '
        'rbCRGYes
        '
        Me.rbCRGYes.AutoSize = True
        Me.rbCRGYes.Location = New System.Drawing.Point(9, 37)
        Me.rbCRGYes.Name = "rbCRGYes"
        Me.rbCRGYes.Size = New System.Drawing.Size(103, 17)
        Me.rbCRGYes.TabIndex = 0
        Me.rbCRGYes.Text = "Yes on CRG site"
        Me.rbCRGYes.UseVisualStyleBackColor = True
        '
        'oldpnlCRG
        '
        Me.oldpnlCRG.Controls.Add(Me.Label32)
        Me.oldpnlCRG.Controls.Add(Me.txtCRG)
        Me.oldpnlCRG.Controls.Add(Me.txtIndex)
        Me.oldpnlCRG.Controls.Add(Me.ComboBox1)
        Me.oldpnlCRG.Controls.Add(Me.txtTitle)
        Me.oldpnlCRG.Controls.Add(Me.chkIndex)
        Me.oldpnlCRG.Controls.Add(Me.chkCRG)
        Me.oldpnlCRG.Controls.Add(Me.chkTitle)
        Me.oldpnlCRG.Controls.Add(Me.Label28)
        Me.oldpnlCRG.Location = New System.Drawing.Point(583, 160)
        Me.oldpnlCRG.Name = "oldpnlCRG"
        Me.oldpnlCRG.Size = New System.Drawing.Size(113, 37)
        Me.oldpnlCRG.TabIndex = 232
        Me.oldpnlCRG.Visible = False
        '
        'Label32
        '
        Me.Label32.AutoSize = True
        Me.Label32.ForeColor = System.Drawing.SystemColors.Highlight
        Me.Label32.Location = New System.Drawing.Point(3, 138)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(204, 13)
        Me.Label32.TabIndex = 50
        Me.Label32.Text = " Select CRG website ShowOnWeb option"
        '
        'txtCRG
        '
        Me.txtCRG.Location = New System.Drawing.Point(162, 56)
        Me.txtCRG.Name = "txtCRG"
        Me.txtCRG.Size = New System.Drawing.Size(166, 20)
        Me.txtCRG.TabIndex = 49
        Me.txtCRG.Text = "enter CRG search text here"
        '
        'txtIndex
        '
        Me.txtIndex.Location = New System.Drawing.Point(162, 79)
        Me.txtIndex.Name = "txtIndex"
        Me.txtIndex.Size = New System.Drawing.Size(166, 20)
        Me.txtIndex.TabIndex = 48
        Me.txtIndex.Text = "enter Index search text here"
        '
        'ComboBox1
        '
        Me.ComboBox1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.ComboBox1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Items.AddRange(New Object() {"Not ShowOnWeb", "ShowOnWeb", "Doesn't Matter"})
        Me.ComboBox1.Location = New System.Drawing.Point(31, 163)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.RestrictContentToListItems = True
        Me.ComboBox1.Size = New System.Drawing.Size(231, 21)
        Me.ComboBox1.TabIndex = 47
        Me.ComboBox1.Text = "Not ShowOn CRG Website"
        '
        'txtTitle
        '
        Me.txtTitle.Location = New System.Drawing.Point(162, 32)
        Me.txtTitle.Name = "txtTitle"
        Me.txtTitle.Size = New System.Drawing.Size(166, 20)
        Me.txtTitle.TabIndex = 46
        Me.txtTitle.Text = "enter Title search text here"
        '
        'chkIndex
        '
        Me.chkIndex.AutoSize = True
        Me.chkIndex.Checked = True
        Me.chkIndex.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkIndex.Location = New System.Drawing.Point(24, 79)
        Me.chkIndex.Name = "chkIndex"
        Me.chkIndex.Size = New System.Drawing.Size(60, 17)
        Me.chkIndex.TabIndex = 45
        Me.chkIndex.Text = "Indices"
        Me.chkIndex.UseVisualStyleBackColor = True
        '
        'chkCRG
        '
        Me.chkCRG.AutoSize = True
        Me.chkCRG.Checked = True
        Me.chkCRG.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkCRG.Enabled = False
        Me.chkCRG.Location = New System.Drawing.Point(24, 32)
        Me.chkCRG.Name = "chkCRG"
        Me.chkCRG.Size = New System.Drawing.Size(98, 17)
        Me.chkCRG.TabIndex = 44
        Me.chkCRG.Text = "CRG Keywords"
        Me.chkCRG.UseVisualStyleBackColor = True
        '
        'chkTitle
        '
        Me.chkTitle.AutoSize = True
        Me.chkTitle.Checked = True
        Me.chkTitle.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkTitle.Location = New System.Drawing.Point(24, 56)
        Me.chkTitle.Name = "chkTitle"
        Me.chkTitle.Size = New System.Drawing.Size(49, 17)
        Me.chkTitle.TabIndex = 43
        Me.chkTitle.Text = " Title"
        Me.chkTitle.UseVisualStyleBackColor = True
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.ForeColor = System.Drawing.SystemColors.Highlight
        Me.Label28.Location = New System.Drawing.Point(3, 31)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(372, 13)
        Me.Label28.TabIndex = 42
        Me.Label28.Text = "Select fields to search, and enter text to search on, using manual wildcards (*)"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.ForeColor = System.Drawing.Color.Maroon
        Me.Label5.Location = New System.Drawing.Point(17, 166)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(72, 13)
        Me.Label5.TabIndex = 28
        Me.Label5.Tag = "@End as datetime = '1/1/1911',"
        Me.Label5.Text = "Select Criteria"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.ForeColor = System.Drawing.Color.Maroon
        Me.Label10.Location = New System.Drawing.Point(13, 17)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(84, 13)
        Me.Label10.TabIndex = 25
        Me.Label10.Text = "Select a Report."
        '
        'btnGo
        '
        Me.btnGo.Location = New System.Drawing.Point(123, 81)
        Me.btnGo.Name = "btnGo"
        Me.btnGo.Size = New System.Drawing.Size(68, 29)
        Me.btnGo.TabIndex = 12
        Me.btnGo.Text = "Go"
        Me.btnGo.UseVisualStyleBackColor = True
        '
        'cboReport
        '
        Me.cboReport.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboReport.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboReport.DropDownWidth = 375
        Me.cboReport.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboReport.FormattingEnabled = True
        Me.cboReport.Location = New System.Drawing.Point(16, 35)
        Me.cboReport.Name = "cboReport"
        Me.cboReport.RestrictContentToListItems = True
        Me.cboReport.Size = New System.Drawing.Size(343, 23)
        Me.cboReport.TabIndex = 11
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Comic Sans MS", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(14, 7)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(448, 57)
        Me.Label6.TabIndex = 29
        Me.Label6.Text = "1. Select an option from the tabs on the left." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "2. Select a Report from the dropd" & _
    "own.   [optional: change variables]." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "3. Click Go."
        '
        'PrintPreviewDialog1
        '
        Me.PrintPreviewDialog1.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.PrintPreviewDialog1.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.PrintPreviewDialog1.ClientSize = New System.Drawing.Size(400, 300)
        Me.PrintPreviewDialog1.Enabled = True
        Me.PrintPreviewDialog1.Icon = CType(resources.GetObject("PrintPreviewDialog1.Icon"), System.Drawing.Icon)
        Me.PrintPreviewDialog1.Name = "PrintPreviewDialog1"
        Me.PrintPreviewDialog1.Visible = False
        '
        'PrintDocument1
        '
        '
        'PrintDialog1
        '
        Me.PrintDialog1.UseEXDialog = True
        '
        'MainMenu1
        '
        Me.MainMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem1})
        '
        'MenuItem1
        '
        Me.MenuItem1.Index = 0
        Me.MenuItem1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.miExport, Me.miPrint, Me.miClose})
        Me.MenuItem1.Text = "File"
        '
        'miExport
        '
        Me.miExport.Index = 0
        Me.miExport.Text = "Export to Excel"
        Me.miExport.Visible = False
        '
        'miPrint
        '
        Me.miPrint.Index = 1
        Me.miPrint.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.miPageSetup, Me.miPreview})
        Me.miPrint.Text = "Print"
        '
        'miPageSetup
        '
        Me.miPageSetup.Index = 0
        Me.miPageSetup.Text = "Page Setup"
        '
        'miPreview
        '
        Me.miPreview.Index = 1
        Me.miPreview.Text = "PrintPreview"
        '
        'miClose
        '
        Me.miClose.Index = 2
        Me.miClose.Text = "Close Window"
        '
        'TabControl1
        '
        Me.TabControl1.Alignment = System.Windows.Forms.TabAlignment.Right
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabControl1.ItemSize = New System.Drawing.Size(130, 35)
        Me.TabControl1.Location = New System.Drawing.Point(12, 72)
        Me.TabControl1.Multiline = True
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.ShowToolTips = True
        Me.TabControl1.Size = New System.Drawing.Size(245, 475)
        Me.TabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed
        Me.TabControl1.TabIndex = 231
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Comic Sans MS", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(546, 558)
        Me.Label3.MinimumSize = New System.Drawing.Size(300, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(452, 50)
        Me.Label3.TabIndex = 241
        Me.Label3.Text = "* ""CONGREGATION"" = church, synagogue, temple, mosque, or denomination." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "**Active " & _
    "organizations in Indiana unless otherwise noted." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "***Center 'Services' cases are" & _
    " included." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.StatusLabel, Me.StatusProgressBar})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 641)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(1148, 22)
        Me.StatusStrip1.TabIndex = 242
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'StatusLabel
        '
        Me.StatusLabel.AutoSize = False
        Me.StatusLabel.Name = "StatusLabel"
        Me.StatusLabel.Size = New System.Drawing.Size(300, 17)
        Me.StatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'StatusProgressBar
        '
        Me.StatusProgressBar.AutoSize = False
        Me.StatusProgressBar.Margin = New System.Windows.Forms.Padding(0)
        Me.StatusProgressBar.Name = "StatusProgressBar"
        Me.StatusProgressBar.Size = New System.Drawing.Size(300, 22)
        '
        'btnEmailTest
        '
        Me.btnEmailTest.Enabled = False
        Me.btnEmailTest.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnEmailTest.Location = New System.Drawing.Point(957, 38)
        Me.btnEmailTest.Name = "btnEmailTest"
        Me.btnEmailTest.Size = New System.Drawing.Size(83, 23)
        Me.btnEmailTest.TabIndex = 243
        Me.btnEmailTest.Text = "do not click"
        Me.btnEmailTest.UseVisualStyleBackColor = True
        Me.btnEmailTest.Visible = False
        '
        'Label46
        '
        Me.Label46.Font = New System.Drawing.Font("Comic Sans MS", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label46.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label46.Location = New System.Drawing.Point(223, 558)
        Me.Label46.MaximumSize = New System.Drawing.Size(400, 60)
        Me.Label46.MinimumSize = New System.Drawing.Size(100, 50)
        Me.Label46.Name = "Label46"
        Me.Label46.Size = New System.Drawing.Size(309, 50)
        Me.Label46.TabIndex = 244
        Me.Label46.Text = "# OR $: opens in a popup grid with totals." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "LIST: open in a popup grid with names" & _
    " [and totals]." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "REPORT: opens in Word with detailed information." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        '
        'Label48
        '
        Me.Label48.AutoSize = True
        Me.Label48.Location = New System.Drawing.Point(139, 560)
        Me.Label48.Name = "Label48"
        Me.Label48.Size = New System.Drawing.Size(67, 13)
        Me.Label48.TabIndex = 245
        Me.Label48.Text = "Terminology:"
        '
        'lblQuarter
        '
        Me.lblQuarter.AutoSize = True
        Me.lblQuarter.ForeColor = System.Drawing.Color.DarkRed
        Me.lblQuarter.Location = New System.Drawing.Point(500, 10)
        Me.lblQuarter.Name = "lblQuarter"
        Me.lblQuarter.Size = New System.Drawing.Size(115, 13)
        Me.lblQuarter.TabIndex = 26
        Me.lblQuarter.Tag = "receives radiobutton user selection"
        Me.lblQuarter.Text = "    keep                        "
        Me.lblQuarter.Visible = False
        '
        'frmSwitchboard
        '
        Me.AcceptButton = Me.btnGo
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(1148, 663)
        Me.Controls.Add(Me.Label48)
        Me.Controls.Add(Me.Label46)
        Me.Controls.Add(Me.btnEmailTest)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.pnlRptSelect)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.lblQuarter)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.rbCRG)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Menu = Me.MainMenu1
        Me.Name = "frmSwitchboard"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Tag = "Switchboard"
        Me.Text = "Reports & Utilities"
        Me.GroupBox1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.grpReports.ResumeLayout(False)
        Me.grpReports.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        Me.grpQuick.ResumeLayout(False)
        Me.grpQuick.PerformLayout()
        Me.TabPage3.ResumeLayout(False)
        Me.TabPage3.PerformLayout()
        Me.grpAdmin.ResumeLayout(False)
        Me.grpAdmin.PerformLayout()
        Me.grpQuarter.ResumeLayout(False)
        Me.grpQuarter.PerformLayout()
        Me.pnlRptSelect.ResumeLayout(False)
        Me.pnlRptSelect.PerformLayout()
        Me.TabControl2.ResumeLayout(False)
        Me.tbDate.ResumeLayout(False)
        Me.tbDate.PerformLayout()
        Me.pnlDate.ResumeLayout(False)
        Me.pnlDate.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.pnlDateRange.ResumeLayout(False)
        Me.pnlDateRange.PerformLayout()
        Me.tbRegion.ResumeLayout(False)
        Me.tbRegion.PerformLayout()
        Me.tbCRG.ResumeLayout(False)
        Me.tbCRG.PerformLayout()
        Me.pnlBookList.ResumeLayout(False)
        Me.pnlBookList.PerformLayout()
        Me.grpOnCRG.ResumeLayout(False)
        Me.grpOnCRG.PerformLayout()
        Me.oldpnlCRG.ResumeLayout(False)
        Me.oldpnlCRG.PerformLayout()
        Me.TabControl1.ResumeLayout(False)
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

#Region "Load"

    'LOAD
    Private Sub frmSwitchboard_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load

        For x As Integer = DatePart(DateInterval.Year, Today()) + 1 To 1997 Step -1
            Me.cboYear.Items.Add(x.ToString)
        Next

        LoadTrees()
        LoadGrantCycleDD("Central")
        LoadReports()

        Me.PrintPreviewDialog1.Document = Me.PrintDocument1
        Me.dtStart.Value = DateAdd(DateInterval.Day, -14, Today())
        GetDtEndAdd(Today())

        If usr = DBAdmin.StaffID Then
            Me.rbDuplResource.Enabled = True
            Me.rbDuplOrg.Enabled = True
            Me.btnEmailTest.Enabled = True
        End If
        isLoaded = True
        Forms.Add(Me)
    End Sub

    'LOAD REPORT LIST & Descriptions
    Private Sub LoadReports()
        Dim sqld As New SqlCommand("SELECT   Topic, Report, ISNULL(Description, 'n/a') + '    ' + ISNULL('Variables: ' + Variables, '') AS Description, CommandString, CommandWhich, Commandvariables  FROM luReport WHERE  (Development = 'Requests') AND (Topic IS NOT NULL) ORDER BY Topic, SortOrder, Report", sc)
        modGlobalVar.LoadDataTable(tblDesc, sqld)
        sqld = Nothing

    End Sub

    'LOAD GRANT CYCLES based on region dropdown
    Private Sub LoadGrantCycleDD(ByVal strRegion As String)
        'TODO add strRegion = first node selected in region tree
        'GET DATA
        tblGen.Clear()

        Dim sql3 As New SqlCommand("SELECT  StatusName + isnull('      ' + ball,'') as GrantCycle FROM   luStatus WHERE   (Topic = 'grantcycle')  ORDER BY StatusName DESC", sc)
        'and (Ball = '" + strRegion + "' or Ball = 'AllRegions')
        ' dtGen.Clear()
        modGlobalVar.LoadDataTable(tblGen, sql3)
        sql3 = Nothing

        'LOAD DD
        For Each r As DataRow In tblGen.Rows
            Me.cboCycle.Items.Add(r("GrantCycle"))
        Next
        tblGen = Nothing
    End Sub

#End Region 'load

#Region "OPEN OTHER FORMS"

    'OPEN SEARCH FOR REGION,City,County,ZIp
    Private Sub rbGeography_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbGeography.Click

        Dim frm As New frmGeography
        Me.StatusLabel.Text = "Opening search for region... "
        MouseWait()
        OpenNonDataForm(frm)
        Me.StatusLabel.Text = "Done"
    End Sub

    'EMAIL - opens form
    Private Sub rbEmail_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbEmail.Click
        Dim frm As New frmMailEmail
        Me.StatusLabel.Text = "opening email window"
        frm.Show()
        Me.StatusLabel.Text = "Done"
    End Sub

    'GET DATA FOR MAILING LABELS - opens form
    Private Sub rbMail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbMail.Click
        Dim frm As New frmMailLblSimple
        Me.StatusLabel.Text = "Opening Labels"
        frm.Show()
        Me.StatusLabel.Text = "Done"
    End Sub

    'RUN: OPEN FORMS
    Private Sub OpenForms(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles rbMapZip.Click, rbMailing.Click, rbLib.Click, rbDelivra.Click, rbInvalidEmail.Click, rbDuplOrg.Click, rbDuplResource.Click, rbResourceGuide.Click
        Dim frm As Form
        Dim r As Integer = 0

        If sender.checked = True Then
        Else
            Exit Sub
        End If

        MouseWait()
        Me.cboReport.Items.Clear()
        Me.StatusLabel.Text = "Opening  " & sender.text
        Select Case sender.tag
            Case Is = "MapZips"
                frm = frmGeography
            Case Is = "Mailing"
                frm = frmMailLblSimple
            Case Is = "Library"
                frm = frmLibraryLbl
            Case Is = "Delivra"
                r = modPopup.StrmWriter("[DelivraEmails]", "@Why", "NewList", UserPath & "Delivra", ".csv", False)
                modPopup.OpenWebsite("www.delivra.com")
                GoTo closeall
            Case Is = "DuplOrgs"
                If StaffDuplCongr.Contains(usr) Or usr = DBAdmin.StaffID Then
                    frm = frmReviewOrgSimple
                Else
                    modGlobalVar.msg("PERMISSION DENIED", "Special training is required to use this form.  See " & DBAdmin.StaffName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            Case Is = "DuplResources"
                If StaffDuplResource.Contains(usr) Or usr = DBAdmin.StaffID Then
                    frm = frmReviewResources
                Else
                    modGlobalVar.msg("PERMISSION DENIED", "Special training is required to use this form.  See " & DBAdmin.StaffName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            Case Is = "ResourceGuide"
                frm = frmAddNew
                '  HideTabPage("tbResourceGuide", frm.ttabcontrol1)
                '======================
            Case Is = "InvalidEmail"
                r = modPopup.StrmWriter("[DelivraEmails]", "@Why", "Validate", UserPath & "DelivraRepair", ".csv", True)
                If r > 0 Then
                    modGlobalVar.msg("Switchboard Done. See Excel document.", UserPath & "DelivraRepair.csv" & NextLine & r.ToString & " bad emails found.", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    GoTo closeall
                Else
                End If
        End Select

        If frm Is Nothing Then
        Else
            Try
                Forms.find(frm, True)
                frm = Nothing
            Catch ex As Exception
                modGlobalVar.msg("ERROR: opening form", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
CloseAll:
        Me.StatusLabel.Text = "Done"
        MouseDefault()
    End Sub

#End Region   'open forms

#Region "RUN ADMIN UTILITIES"

    'POPUP MENU
    Private Sub rbAdmin_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbAdmin.Click
        Dim pp As New ContextMenu
        Dim eh As EventHandler = AddressOf ContextHandler
        pp.MenuItems.Add("Delivra Emails", eh)
        pp.MenuItems.Add("Review Duplicate Organizations && Contacts", eh)
        pp.MenuItems.Add("Review Duplicate Resources", eh)
        pp.MenuItems.Add("Staff && Laity Multi-Mailing", eh)
        pp.MenuItems.Add("")
        pp.Show(Me, PointToClient(Control.MousePosition))
    End Sub

    'MENU ACTION
    Private Sub ContextHandler(ByVal obj As Object, ByVal ea As EventArgs)
        Select Case obj.text
            Case "Review Duplicate Organizations && Contacts"
                If StaffDuplCongr.Contains(usr) Or usr = DBAdmin.StaffID Then
                    Dim frm As New frmReviewOrgSimple
                    frm.Show()
                Else
                    modGlobalVar.msg("PERMISSION DENIED", "Special training is required to use this form.  See " & DBAdmin.StaffName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            Case "Delivra Emails"
                Dim pp As New ContextMenu
                Dim eh As EventHandler = AddressOf DelivraHandler
                pp.MenuItems.Add("Validate Existing Emails", eh)
                pp.MenuItems.Add("Generate New Email .csv", eh)
                pp.MenuItems.Add("Update Information Center", eh)
                pp.Show(Me, PointToClient(Control.MousePosition))
            Case "Review Duplicate Resources"
                If StaffDuplResource.Contains(usr) Or usr = DBAdmin.StaffID Then
                    Dim frm As New frmReviewResources
                    frm.Show()
                Else
                    modGlobalVar.msg("PERMISSION DENIED", "Special training is required to use this form.  See " & DBAdmin.StaffName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If

            Case "Staff && Laity Multi-Mailing"
                Dim frm As New frmReviewOrgChild
                frm.Show()
        End Select
    End Sub

    'DELIVRA HANDLER
    Private Sub DelivraHandler(ByVal obj As Object, ByVal ea As EventArgs)
        Dim r As Integer = 0
        MouseWait()
        Select Case obj.text
            Case "Validate Existing Emails"
                r = modPopup.StrmWriter("[DelivraEmails]", "@Why", "Validate", UserPath & "DelivraRepair", ".csv", True)
                ' OpenExcel("C:\DelivraRepair", True)
            Case "Generate New Email .csv"
                r = modPopup.StrmWriter("[DelivraEmails]", "@Why", "NewList", UserPath & "Delivra", ".csv", False)
                modPopup.OpenWebsite("www.delivra.com")
            Case "Update Information Center"
            Case Else
                modGlobalVar.msg("ERROR: not found", obj.text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Select
        If r > 0 Then
            modGlobalVar.msg("DONE", r.ToString & " emails found.", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
        MouseDefault()
    End Sub

#End Region 'grant default numbers, org review, move contacts

#Region "SET DEFAULT DATES"

    'CHANGE DATES BASED ON USER SELECTING QUARTER 
    Private Sub ChangeDateQtr(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles rbQtr1.CheckedChanged, rbQtr2.CheckedChanged, rbQtr3.CheckedChanged, rbQtr4.CheckedChanged

        If isLoaded And sender.checked Then
            Me.lblQuarter.Text = sender.tag
            If Me.cboYear.SelectedIndex > -1 Then
                SetDate(sender)
            End If
            Me.cboCycle.SelectedIndex = -1
        Else
        End If
    End Sub

    'CHANGE DATE BASED ON USER SELECTING GRANT CYCLE
    Private Sub ChangeDateCycle(ByVal sender As Object, ByVal e As System.EventArgs) _
            Handles cboCycle.SelectionChangeCommitted
        If isLoaded And cboCycle.SelectedIndex > -1 Then
            SetDate(sender)
            Me.cboYear.SelectedIndex = -1
            Me.rbQtrAll.Checked = True
        End If
    End Sub

    'CHANGE DATE BASED ON USER SELECTING YEAR
    Private Sub ChangeDateYear(ByVal sender As Object, ByVal e As System.EventArgs) _
            Handles cboYear.SelectionChangeCommitted
        If isLoaded And Me.cboYear.SelectedIndex > -1 Then    'And lblQuarter.Text <> "receives radiobutton user selection" Then
            SetDate(sender)
            Me.cboCycle.SelectedIndex = -1
        End If

    End Sub

    'GENERATE FIRST AND LAST DATE
    Private Sub SetDate(ByRef ctl As Control)
        Dim FirstDay, LastDay As Date

        If Not isLoaded Then
            Exit Sub
        End If

        'GRANT CYCLE
        If ctl.Name = "cboCycle" Then
            If cboCycle.SelectedIndex = 0 Then 'all years
                Me.dtStart.Text = CType("1/1/1997", Date)
                Me.dtEnd.Text = Today
                Exit Sub
            End If
            Dim yr As Int16 = CType(cboCycle.SelectedItem.substring(0, 4), Int16)
            dtStart.Text = CType("1/1/" & yr, Date)
            dtEnd.Text = CType("12/31/" & CType(cboCycle.SelectedItem.substring(5, 4), Int16), Date)
            GetDtEndAdd(dtEnd.Text)
            Exit Sub
        End If

        If ctl.Name.ToString = "cboYear" Then
            If Me.rbQtrAll.Checked = True Then
                Me.dtStart.Text = CType("1/1/" & cboYear.SelectedItem, Date)
                Me.dtEnd.Text = CType("12/31/" & cboYear.SelectedItem, Date)
                GetDtEndAdd(dtEnd.Text)
                Exit Sub
            Else    'set quarter dates

            End If
        End If

        'QUARTER
        FirstDay = CType(CType(Me.lblQuarter.Text, Integer) * 3 - 2, String) & "/" & Me.cboYear.SelectedItem

        If Me.lblQuarter.Text = 4 Then
            LastDay = CType("12/31/" & Me.cboYear.SelectedItem, Date)
        Else
            LastDay = DateAdd(DateInterval.Day, -1, CType(CType(CType(Me.lblQuarter.Text, Integer) * 3 + 1, String) & "/" & Me.cboYear.SelectedItem, Date))
        End If
        Me.dtStart.Text = FirstDay
        Me.dtEnd.Text = LastDay
        GetDtEndAdd(dtEnd.Text)

    End Sub

    'ADD ONE TO END DATE TO COMPENSATE FOR MIDNIGHT IN SQL 'BETWEEN' PARAMETERS
    Private Sub GetDtEndAdd(ByVal dt As Date)
        dtEndOne = CType(DateAdd(DateInterval.Day, 1, dt), Date)
    End Sub

    'GET NEW ENDDATE IF USER CHANGES DATE
    Private Sub dtEnd_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtEnd.LostFocus
        GetDtEndAdd(Me.dtEnd.Text)
    End Sub

    'set dates per QUARTER
    Private Sub rbQtrAll_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbQtrAll.CheckedChanged
        If Not isLoaded Then
            Exit Sub
        End If
        If rbQtrAll.Checked = True Then
            If Me.cboYear.SelectedIndex > -1 Then
                SetDate(Me.cboYear)
                Me.cboCycle.SelectedIndex = -1
            End If
        End If
    End Sub

#End Region 'dates  

#Region "Print Preview RTB"

    'PRINT
    Private Sub PrintDocument1_BeginPrint(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles PrintDocument1.BeginPrint
        mCharFrom = 0
    End Sub

    Private Sub btnPageSetup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles miPageSetup.Click
        PageSetupDialog1.Document = PrintDocument1

        'using the following breaks the connection to printpreview 
        '' Initialize the dialog's PrinterSettings property to hold user
        '' defined printer settings.
        'PageSetupDialog1.PageSettings = _
        '    New System.Drawing.Printing.PageSettings

        '' Initialize dialog's PrinterSettings property to hold user
        '' set printer settings.
        'PageSetupDialog1.PrinterSettings = _
        '    New System.Drawing.Printing.PrinterSettings

        PageSetupDialog1.ShowDialog()

    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles miPrint.Click
        'Allow the user to choose the page range to print.
        PrintDialog1.AllowSomePages = True
        If PrintDialog1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            PrintDocument1.Print()

        End If
    End Sub

    Private Sub btnPrintPreview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles miPreview.Click
        ' PrintPreviewDialog1.Document = PrintDocument1
        PrintPreviewDialog1.PrintPreviewControl.Zoom = 1.0
        PrintPreviewDialog1.ShowDialog()
    End Sub
#End Region

#Region "MISC"

    'change search field text on user changing first one
    Private Sub txtTitle_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles txtTitle.TextChanged
        If txtCRG.Text = "enter CRG search text here" Then
            txtCRG.Text = txtTitle.Text
        End If
    End Sub

    'make your own query  - discontinued??
    Private Sub rbGQI_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbGQI.Click
        Try
            System.Diagnostics.Process.Start("\\ICCNAS1\Users\Shared\VisualStudioApps\GQI\setup.exe") 'publish.htm") 
        Catch ex As System.Exception
            modGlobalVar.msg("ERROR open DIY Query", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region 'misc

#Region "NEW ROUTINES"

    'SET PARAMS, GO
    Private Sub NewSearch(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnGo.Click

        If Me.cboReport.SelectedIndex = -1 Then
            modGlobalVar.msg("Cancelling Request", "no report selected in the dropdown box", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If
        Me.StatusProgressBar.Value = 10
        Dim strbType As New StringBuilder    'type
        Dim strbCRGNum As New StringBuilder    'crg
        Dim strbCRGText As New StringBuilder   'crg text for heading
        Dim cmd As New SqlClient.SqlCommand
        Dim tblTmp As New DataTable
        Dim strStaff, strDate As String 'for rpt heading

        MouseWait()
        Me.StatusLabel.Text = "Selecting Data for : " & Me.cboReport.Text
        Me.StatusProgressBar.Value = 20
        Me.StatusLabel.Text = "getting data"

        If foundRow(0)("CommandString").substring(0, 1) = "[" Then 'Contains("[") Then
            cmd.CommandType = CommandType.StoredProcedure
        Else
            cmd.CommandType = CommandType.Text
        End If
        cmd.Connection = sc

AddDefiningParam:
        Select Case foundRow(0)("CommandWhich").ToString
            Case Is = String.Empty
                modGlobalVar.msg("ERROR: missing commandWhich", Me.cboReport.SelectedItem.ToString, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            Case Else
                cmd.CommandText = foundRow(0)("CommandString")
                cmd.Parameters.Add("@Which", SqlDbType.VarChar).Value = foundRow(0)("CommandWhich")
        End Select

SetCommonParams:
Dt:
        If bdate = True Then
            cmd.Parameters.Add("@Start", SqlDbType.DateTime).Value = Me.dtStart.Text
            cmd.Parameters.Add("@End", SqlDbType.DateTime).Value = CType(dtEndOne, Date)
            strDate = NextLine & " from " & Me.dtStart.Value.ToShortDateString & " to " & Me.dtEnd.Value.ToShortDateString ' = cboReport.SelectedItem & " from " & Me.dtStart.Value.ToShortDateString & " to " & Me.dtEnd.Value.ToShortDateString
        Else
            strDate = "" '  strTitle = cboReport.SelectedItem
        End If
Region:
        If bRegion = True Then
            SetRegionParam(cmd)
        End If
Staff:
        If bStaff = True Then
            Dim str As String
            If foundRow(0)("CommandVariables").ToString.Contains("X") Then 'staff privacy
                str = SetStaffParam(True)
            Else
                str = SetStaffParam(False)
            End If

            If str = "%" Then
            Else
                cmd.Parameters.Add("@StaffNum", SqlDbType.VarChar).Value = str
                strStaff = NextLine & "Staff: Selected staff"
            End If
            str = Nothing
        Else
        End If
Type:
        If bType = True Then
            SetTypeParam(cmd, strbType)
        End If
CRGName:
        If bCRG = True Then
            Dim str As String = SetCRGParam(strbCRGNum, strbCRGText)
            If str = "%" Then
            Else
                cmd.Parameters.Add("@CRGNum", SqlDbType.VarChar).Value = strbCRGNum.ToString
            End If
            str = Nothing
        End If
OnCRGWebsite:
        If Me.grpOnCRG.Visible = True Then 'don't add this param unless is needed by resource reports
            cmd.Parameters.Add("@OnCRG", SqlDbType.TinyInt)
            If rbCRGEither.Checked = True Then
                cmd.Parameters("@OnCRG").Value = 2
            Else
                If Me.rbCRGYes.Checked = True Then
                    cmd.Parameters("@OnCRG").Value = 1
                Else
                    cmd.Parameters("@OnCRG").Value = 0
                End If
            End If
        End If
GetData:
        Me.StatusProgressBar.Value = 30
RunReport:
        Me.StatusLabel.Text = "opening results"
        If Me.cboReport.SelectedItem.contains("Report:") Then 'OPEN IN Word
            Try
                If strbType.ToString.Contains("%") Or strbType.ToString = String.Empty Then
                    rptSubTitle = "" '"For All Resource Types" '& NextLine & strbCRGText.ToString& NextLine &  strStaff& NextLine &  Me.dtStart.Value.ToShortDateString & " and " & Me.dtEnd.Value.ToShortDateString
                Else
                    rptSubTitle = "For Resource Type:  " & strbType.ToString() '& NextLine &  strbCRGText.ToString& NextLine &  strStaff& NextLine &  Me.dtStart.Value.ToShortDateString & " and " & Me.dtEnd.Value.ToShortDateString
                End If
                rptSubTitle = rptSubTitle & strbCRGText.ToString & strStaff & strDate

                Select Case UCase(foundRow(0)("CommandString"))
                    Case Is = "[RPTSRESOURCESFULL]"   'specialized layout of resource detail
                        Try
                            modGlobalVar.LoadDataTable(tblTmp, cmd)
                        Catch ex As Exception
                            modGlobalVar.msg("ERROR: loading rpt table", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Finally
                            sc.Close()
                        End Try
                        modPopup.PrintResourceFull(tblTmp, Me.cboReport.SelectedItem, rptSubTitle, Me.cboReport.SelectedItem, Me.StatusProgressBar)
                    Case Is = "[RPTSRESOURCESSHARE]"
                        Dim strb As New StringBuilder
                        strb.Append("SELECT * FROM vwRptResourceBasicActive  WHERE ICCResourceID IN (SELECT distinct ICCResourceID FROM vwGetResourceKeywords WHERE (Keyword IN ('" + strbCRGNum.ToString + "'))) ORDER BY ResourceType, Sortfld")
                        modGlobalVar.LoadDataTable(tblTmp, cmd)
                        sc.Close()
                        If modGlobalVar.msg("CONFIRM:", "X", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                            modPopup.PrintResourceDescriptions(strb.ToString, strbCRGText.ToString, True)
                        End If
                    Case Is = "[RPTSSTAFFBENCHMARKS]" ' = Tim  open in Word with red captions
                        ClassLibrary1.ClassWriter2.PrintTimReport(cmd.CommandText, "Report: " & Me.cboReport.SelectedItem & NextLine &
                                                 "To-Date: " & cmd.Parameters("@End").Value.ToString)
                    Case Else 'GENERIC resource REPORTS w grouping

                        modPopup.PrintResourceReport(cmd, Me.cboReport.SelectedItem, rptSubTitle, True, rptGroupOn, "Report: " & Me.cboReport.SelectedItem)

                End Select
            Catch ex As Exception
            modGlobalVar.msg("ERROR: publishing to Word ", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Else    'List or #; OPEN IN GRID
        Select Case foundRow(0)("CommandWhich")
            Case Is = "AnnualReportStats"
                OpenPopupDGV(Me.cboReport.SelectedItem, cmd) ' foundRow(0)("CommandString"))
            Case Else
                Try
                    modGlobalVar.LoadDataTable(tblTmp, cmd)
                    sc.Close()
                Catch ex As Exception
                    modGlobalVar.msg("ERROR: loading table ", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    GoTo CloseAll
                End Try
                If tblTmp.Rows.Count > 0 Then
                    OpenPopupDGV(Me.cboReport.SelectedItem, tblTmp, True)
                Else
                    modGlobalVar.msg(MsgCodes.noResultCancel) '"cancelling request", "no results found", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
        End Select
        End If
CloseAll:
        cmd = Nothing
        tblTmp = Nothing
        Try
            sc.Close()
        Catch ex As Exception
            modGlobalVar.msg("ERROR: closing  ", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        MouseDefault()
        Me.StatusLabel.Text = "Done "
        Exit Sub

    End Sub

    'TBL VERSION: SET PARAM VISIBILITY
    Private Sub cboReport_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles cboReport.SelectedIndexChanged
        HidePanels()
        If cboReport.SelectedIndex > -1 Then    'doesn't work!
        Else
            Exit Sub
        End If
        Me.StatusProgressBar.Value = 0

DESCRIPTION:
        If GetDescription(Me.cboReport.SelectedItem) = False Then
            Exit Sub
        End If

VISIBILITY:  'based on multiple options
        If foundRow(0)("CommandVariables").ToString.Contains("D") Then 'date
            Me.pnlDate.Visible = True
            Me.dtStart.Visible = True
            Me.dtEnd.Visible = True
            bdate = True
        End If
        If foundRow(0)("CommandVariables").ToString.Contains("S") Then 'staff
            Me.trvStaff.Visible = True
            bStaff = True
        End If
        If foundRow(0)("CommandVariables").ToString.Contains("R") Then 'region
            Me.trvRegion.Visible = True
            bRegion = True
        End If
        If foundRow(0)("CommandVariables").ToString.Contains("C") Then 'CRG term
            Me.trvCRG.Visible = True
            bCRG = True
        End If
        If foundRow(0)("CommandVariables").ToString.Contains("T") Then 'resource type
            Me.trvType.Visible = True
            bType = True
        End If
        If foundRow(0)("CommandVariables").ToString.Contains("A") Then 'attendance
            bSize = True
        End If
        If foundRow(0)("CommandVariables").ToString.Contains("Y") Then 'year
            Me.pnlDate.Visible = True
            Me.dtStart.Visible = True
            Me.dtEnd.Visible = False
            bdate = True
        End If
        If foundRow(0)("CommandVariables").ToString.Contains("O") Then 'on CRG website
            Me.grpOnCRG.Visible = True
        End If
        If foundRow(0)("CommandVariables").ToString.Contains("I") Then 'international
            Me.trvRegion.Visible = True
        End If
    End Sub

    'TBL VERSION: LOAD REPORT CBO
    Private Sub LoadReports(ByVal strTopic As String)
        Dim GetRows() As Data.DataRow
        Dim i As Integer = 0

        If strTopic = String.Empty Then
            Exit Sub
        End If

ClearOld:
        cboReport.Items.Clear()
        cboReport.Text = ""
        HidePanels()

        Me.dtStart.Value = DateAdd(DateInterval.Day, -DatePart(DateInterval.Day, Today()) + 1, Today()) 'first of current month
LoadCBO:
        GetRows = tblDesc.Select("Topic = '" & strTopic & "'")
        If GetRows.Length = 0 Then
            Exit Sub
        Else
            For Each r As DataRow In GetRows
                If i = 0 Then   'add blank rows between headings
                Else
                    If r("Report").ToString.Contains("- - -") Then
                        Me.cboReport.Items.Add(" ")
                    End If
                End If
                Me.cboReport.Items.Add(r("Report"))
                i = i + 1
            Next
        End If
        GetRows = Nothing
        i = Nothing
        cboReport.DroppedDown = True
    End Sub

    'TBL VERSION: LOAD DESCRIPTION TEXT BOX
    Private Function GetDescription(ByVal RptName As String) As Boolean
        If RptName = String.Empty Then
            Me.lblDescription.Text = "Report Description: n/a"
            Return False
            Exit Function
        End If

AssignFoundRow:
        foundRow = tblDesc.Select("Report = '" & Replace(RptName, NextLine, "") & "'")
        If foundRow.Length = 0 Then
            '  Me.lblDescription.Text = "Report Description: n/a"
            Return False
        Else
            Me.lblDescription.Text = IsNull(Replace(foundRow(0)("Description"), "  ", Chr(10)), "Report Description: tbd")
            Return True
        End If

    End Function

    'TBL VERSION: RUN QUICK RESULT: SINGLE TABLE
    Private Sub QuickView(ByVal sender As Object, ByVal e As System.EventArgs) _
        Handles rbConvToDate.Click, rbNeverReviewed.Click, rbCntResourceType.Click, rbMGI.Click, rbResourceType.Click
        If sender.checked = False Then
            Exit Sub
        End If
        Dim bSortable As Boolean = True
        MouseWait()

        HidePanels()
        Try
            Me.cboReport.Items.Clear()
        Catch ex As Exception
            modGlobalVar.msg("ERROR: clearing cboReport", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Me.pnlDate.Enabled = False
        Me.StatusLabel.Text = "getting data"
        Dim tb As New DataTable(sender.tag)
        Dim cmd As New SqlClient.SqlCommand
        cmd.Connection = sc

        If GetDescription(sender.text) = False Then
            GoTo CloseAll
        End If

        cmd.CommandText = foundRow(0)("CommandString")
        If cmd.CommandText.ToString.Substring(0, 1) = "[" Then
            cmd.CommandType = CommandType.StoredProcedure
        Else
            cmd.CommandType = CommandType.Text
        End If
        If foundRow(0)("CommandWhich").ToString = String.Empty Then
        Else
            cmd.Parameters.Add("@Which", SqlDbType.VarChar).Value = foundRow(0)("CommandWhich")
        End If

        Try
            modGlobalVar.LoadDataTable(tb, cmd)
        Catch ex As Exception
            modGlobalVar.msg("ERROR: loading table ", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
            GoTo CloseAll
        End Try
        Try
            If tb.Rows.Count > 0 Then
                Me.StatusLabel.Text = "opening results"
                OpenPopupDGV(sender.text, tb, bSortable)
            Else
                modGlobalVar.msg(MsgCodes.noResultCancel) '"cancelling request", "no results found", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Catch ex As Exception
            modGlobalVar.msg("ERROR: populating result grid ", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

CloseAll:
        cmd = Nothing
        tb = Nothing
        Try
            sc.Close()
        Catch ex As Exception
            modGlobalVar.msg("ERROR: closing  ", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        MouseDefault()
        Me.StatusLabel.Text = "Done "
    End Sub

    'TBL VERSION: RUN QUICK RESULT: MULTIPLE TABLES
    Private Sub QVMultiple(ByVal sender As Object, ByVal e As System.EventArgs) _
        Handles rbCurrentCases.Click

        Dim bSortable As Boolean

        MouseWait()
        If sender.name = "rbCurrentCases" Then
            Me.dtStart.Value = DateAdd(DateInterval.Day, -DatePart(DateInterval.Day, Today()) + 1, Today()) 'first of current month
            bSortable = True
        Else
            bSortable = False
        End If

        GetDescription(sender.text)
        Dim cmd As New SqlClient.SqlCommand
        cmd.Connection = sc

        cmd.CommandText = foundRow(0)("CommandString")  '"[CTCurrentCasesbyRegion]" '
        If cmd.CommandText.ToString.Substring(0, 1) = "[" Then
            cmd.CommandType = CommandType.StoredProcedure
        End If
        If foundRow(0)("CommandWhich").ToString = String.Empty Then
        Else
            cmd.Parameters.Add("@Which", SqlDbType.VarChar).Value = foundRow(0)("CommandWhich")
        End If

        'If Not SCConnect() Then
        '    GoTo CloseAll
        'End If
        Try
            Me.StatusProgressBar.Value = 30
            Me.StatusLabel.Text = "opening results"
            OpenPopupDGV(sender.text, cmd, bSortable)
        Catch ex As Exception
            modGlobalVar.msg("ERROR: populating result grid ", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
CloseAll:
        cmd = Nothing
        'Try
        '    sc.Close()
        'Catch ex As Exception
        'End Try

        MouseDefault()
        Me.StatusLabel.Text = "Done"
    End Sub

    'TBL VERSION: QUICK VIEW w REPORT OPTIONS w NO DATE
    Private Sub RptOptionsNoDate(ByVal sender As System.Object, ByVal e As System.EventArgs) _
       Handles rbActiveCongr.Click, rbListCRG.Click

        If sender.checked = True Then
            LoadReports(sender.tag)
            Me.pnlDate.Enabled = False
        End If
    End Sub

    'TBL VERSION: QUICK VIEW w REPORT OPTIONS w  DATE
    Private Sub RptOptionsWDate(ByVal sender As System.Object, ByVal e As System.EventArgs) _
       Handles rbCase.Click, rbGrant.Click, rbOrg.Click, rbResource.Click, rbWorkshop.Click, rbBoard.Click

        If sender.checked = True Then
            LoadReports(sender.tag)
            Me.pnlDate.Enabled = True
        End If
        'bold heading in word
        Select Case sender.tag
            Case Is = "Resources"
                rptGroupOn = "ResourceName"
            Case Is = "ContactsOrgs"
                rptGroupOn = "OrgName"
            Case Else
                rptGroupOn = String.Empty
        End Select

    End Sub

    'OPEN DGV w TABLE  Query answer in popup DatagridView; ds = table; uses progress bar
    Private Sub OpenPopupDGV(ByVal strTitle As String, ByRef tb As DataTable, ByVal bAllowSort As Boolean)
        Dim newFrm As New frmPopupDatagrid(strTitle, Me.StatusProgressBar)
        Dim grd As DataGridView = newFrm.dgvResult
        Dim Cnt As Integer

        Me.StatusLabel.Text = "Retrieving data"
        If Forms.find(ClassOpenForms.frmPopupDatagrid) Then
            Try
                ClassOpenForms.frmPopupDatagrid.Close()
            Catch ex As Exception
            End Try
            ' Else
        End If
        newFrm.Show()
        ClassOpenForms.frmPopupDatagrid = newFrm
        modPopup.FixColumnSize(newFrm.dgvResult)

LoadGrid:
        'ASSIGN DATASOURCE
        grd.DataSource = tb
        Me.StatusProgressBar.Value = 80

        Cnt = modPopup.FormatGrid(grd, bAllowSort)
        Me.StatusLabel.Text = "Done"
        Me.StatusProgressBar.Value = 100
        ClassOpenForms.frmPopupDatagrid.Text = strTitle & "  " & Cnt.ToString & " rows"
    End Sub

    'OPEN DGV w MULTIPLE TABLES w diff columnspup Query answer in popup DatagridView; ds = rows w repeat query by region
    'eg CurrentCases
    Public Sub OpenPopupDGV(ByVal strTitle As String, ByVal cmd As SqlCommand, ByVal bAllowSort As Boolean)
        Dim newFrm As New frmPopupDatagrid(strTitle, Me.StatusProgressBar)
        Dim grd As DataGridView = newFrm.dgvResult
        Dim iNewRow As Integer = 0

        If Forms.find(ClassOpenForms.frmPopupDatagrid) Then
            ClassOpenForms.frmPopupDatagrid.Close()
        End If
        newFrm.Show()
        ClassOpenForms.frmPopupDatagrid = newFrm
        ClassOpenForms.frmPopupDatagrid.Text = strTitle
        cmd.Parameters.Add("@Region", SqlDbType.VarChar)

AddColumns:
        For i As Integer = 1 To 25 'dtStaff.Rows.Count - 15 '30 'number of potential staff igrd To igrd + (itbl - igrd)
            grd.Columns.Add(i.ToString, "") ', i.ToString)
        Next i
        Me.StatusProgressBar.Value = 10

LoadGrid:  'runs regions individually
        For iRegion As Integer = 1 To colRegionOffice.Count
            Me.StatusLabel.Text = colRegionOffice.Item(iRegion).ToString
            Me.StatusProgressBar.Value = Me.StatusProgressBar.Value + 10

            cmd.Parameters("@Region").Value = colRegionOffice.Item(iRegion).ToString
            Dim tb As New DataTable
            Try
                modGlobalVar.LoadDataTable(tb, cmd, False)
            Catch ex As Exception
                modGlobalVar.msg("ERROR: loading table ", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
                GoTo CloseAll
            End Try

            If tb.Rows.Count > 0 Then
                Dim itbl As Integer = tb.Columns.Count
                Dim cnt As Integer = 1
ColumnHeadings:
                grd.Rows.Add()
                grd.Rows(iNewRow).DefaultCellStyle.Font = New Font(grd.DefaultCellStyle.Font, FontStyle.Bold)
                For h As Integer = 0 To itbl - 1
                    grd(h, iNewRow).Value = tb.Columns(h).ColumnName
                Next h
DataRows:
                For Each r As DataRow In tb.Rows
                    grd.Rows.Add()
                    iNewRow = iNewRow + 1 ' iNewRow = grd.NewRowIndex + 1 'grd.Rows.Count - 1
                    For c As Integer = 0 To itbl - 1
                        grd(c, iNewRow).Value = r.Item(c).ToString
                    Next
                    If r.Item(0).ToString.Contains("Total") Or r.Item(1).ToString.Contains("Total") Then
                        grd.Rows(iNewRow).DefaultCellStyle.Font = New Font(grd.DefaultCellStyle.Font, FontStyle.Bold)
                    End If
                    cnt = cnt + 1
                Next r
                grd.Rows.Add(2)    'blank row between runs 
                iNewRow = iNewRow + 2
            End If 'tb has rows
            tb = Nothing
        Next iRegion

        '---====================================
        'NEW RUN AGAIN FOR CRG OUT OF STATE CASES 3/16
        Me.StatusLabel.Text = "OutofState CRG cases"
        Me.StatusProgressBar.Value = Me.StatusProgressBar.Value + 10

        cmd.Parameters("@Region").Value = "OutoFState"
        Dim tb2 As New DataTable
        Try
            modGlobalVar.LoadDataTable(tb2, cmd, False)
        Catch ex As Exception
            modGlobalVar.msg("ERROR: loading table ", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
            GoTo CloseAll
        End Try

        If tb2.Rows.Count > 0 Then
            Dim itbl As Integer = tb2.Columns.Count
            Dim cnt As Integer = 1

            'ColumnHeadings:
            grd.Rows.Add()
            grd.Rows(iNewRow).DefaultCellStyle.Font = New Font(grd.DefaultCellStyle.Font, FontStyle.Bold)
            For h As Integer = 0 To itbl - 1
                grd(h, iNewRow).Value = tb2.Columns(h).ColumnName
            Next h
            'DataRows:
            For Each r As DataRow In tb2.Rows
                '    Me.StatusLabel.Text = " adding rows " & colRegion5lu.Item(iRegion).ToString & " : " & cnt.ToString
                grd.Rows.Add()
                iNewRow = iNewRow + 1 ' iNewRow = grd.NewRowIndex + 1 'grd.Rows.Count - 1
                For c As Integer = 0 To itbl - 1
                    grd(c, iNewRow).Value = r.Item(c).ToString
                Next
                If r.Item(0).ToString.Contains("Total") Or r.Item(1).ToString.Contains("Total") Then
                    grd.Rows(iNewRow).DefaultCellStyle.Font = New Font(grd.DefaultCellStyle.Font, FontStyle.Bold)
                End If
                cnt = cnt + 1
            Next r
            grd.Rows.Add(2)    'blank row between runs 
            iNewRow = iNewRow + 2
        End If 'tb has rows

        tb2 = Nothing

        Me.StatusProgressBar.Value = 90
        Me.StatusLabel.Text = "getting sub totals"
GetTotalRow:  'runs regions all at once
        Dim tbt As New DataTable
        cmd.CommandText = "[ctCurrentCasesTotal]"
        cmd.Parameters("@Region").Value = "%"
        Try
            modGlobalVar.LoadDataTable(tbt, cmd)
            sc.Close()
        Catch ex As Exception
            modGlobalVar.msg("ERROR: loading table 2", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
            GoTo CloseAll
        End Try

        Dim itblt As Integer = tbt.Columns.Count
        Dim igrdt As Int16 = grd.Columns.Count
        Dim cntt As Integer = 1

AddCols:
        If itblt - igrdt > 0 Then
            For i As Integer = igrdt To igrdt + (itblt - igrdt)
                grd.Columns.Add(i.ToString, "") ', i.ToString)
            Next i
        End If
ColumnHead:
        grd.Rows.Add()
        grd.Rows(iNewRow).DefaultCellStyle.Font = New Font(grd.DefaultCellStyle.Font, FontStyle.Bold)
        For h As Integer = 0 To itblt - 1
            grd(h, iNewRow).Value = tbt.Columns(h).ColumnName
        Next h
Data:
        Me.StatusLabel.Text = " adding total row "

        For Each r As DataRow In tbt.Rows
            grd.Rows.Add()
            iNewRow = iNewRow + 1 ' iNewRow = grd.NewRowIndex + 1 'grd.Rows.Count - 1
            If r.Item(0).ToString.Contains("GRAND TOTAL") Then
                grd.Rows(iNewRow).DefaultCellStyle.Font = New Font(grd.DefaultCellStyle.Font, FontStyle.Bold)
            End If
            For c As Integer = 0 To itblt - 1
                grd(c, iNewRow).Value = r.Item(c).ToString
            Next
            cntt = cntt + 1
        Next r

        tbt = Nothing

FormatGrid:
        Me.StatusProgressBar.Value = 95
        Me.StatusLabel.Text = "formatting grid"
        If bAllowSort = False Then
            For Each c As DataGridViewColumn In ClassOpenForms.frmPopupDatagrid.dgvResult.Columns
                c.SortMode = DataGridViewColumnSortMode.NotSortable
            Next c
        End If
        modPopup.FormatGrid(grd, iNewRow)
        modPopup.FixColumnSize(grd)
        Me.StatusLabel.Text = "Done"
        Me.StatusProgressBar.Value = 100
CloseAll:
        Try
            cmd = Nothing
            tbt = Nothing
        Catch ex As Exception
        End Try

    End Sub

    'OPEN DGV with MULTIPLE QUERIES in sproc and VARIOUS COLUMN COUNT but single run of sproc
    ' eg Jane Annual Stats; by year not start and end dates
    Public Sub OpenPopupDGV(ByVal strTitle As String, ByVal cmd As SqlCommand)
        ' modGlobalVar.Msg(strTitle, , "here")
        Dim newFrm As New frmPopupDatagrid(strTitle, Me.StatusProgressBar)
        Dim grd As DataGridView = newFrm.dgvResult
        '  Dim cmd As New SqlCommand(cmdtxt, sc)
        Dim iNewRow As Integer = 0
        Dim dr As SqlDataReader
        Dim fNextResult As Boolean = True
        Dim cntCols As Integer = 0 'number of columns in datagridview
        MouseWait()
        If Forms.find(ClassOpenForms.frmPopupDatagrid) Then
            ClassOpenForms.frmPopupDatagrid.Close()
        End If
        newFrm.Show()
        ClassOpenForms.frmPopupDatagrid = newFrm
        ClassOpenForms.frmPopupDatagrid.Text = strTitle
        'must have column before can add row; added to popup grid
        'TODO test other reports see if this is ok

        If Not SCConnect() Then
            MouseDefault()
            Exit Sub
        End If
        dr = cmd.ExecuteReader()

LoopDataReader:
        Do Until Not fNextResult
            '  modGlobalVar.Msg(fNextResult.ToString, , cntCol.ToString)
AddColumns:
            Dim cntFields As Integer = dr.FieldCount
            If cntFields > cntCols Then
                For i As Integer = grd.Columns.Count To grd.Columns.Count + (cntFields - 1 - grd.Columns.Count)
                    grd.Columns.Add("col" + i.ToString, "col" + i.ToString)
                    grd.Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
                Next i
                cntCols = grd.Columns.Count
            End If
            grd.Columns(0).HeaderText = Year(Me.dtStart.Text).ToString

GetColumnTitle:
            grd.Rows.Add() 'for next result set headings
            ' iNewRow += iNewRow
            grd.Rows(iNewRow).DefaultCellStyle.Font = New Font(grd.DefaultCellStyle.Font, FontStyle.Bold)
            For i As Integer = 0 To cntFields - 1
                grd(i, iNewRow).Value = dr.GetName(i)
                grd.Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
            Next i

            If dr.HasRows Then
GetData:
                Do While dr.Read()
                    grd.Rows.Add()
                    iNewRow = iNewRow + 1
                    For i As Integer = 0 To cntFields - 1
                        grd(i, iNewRow).Value = dr.GetValue(i)
                    Next
                Loop
            Else
                grd.Rows.Add()
                iNewRow = iNewRow + 1
                grd(0, grd.CurrentRow.Index).Value = "no results"
            End If
            grd.Rows.Add(2)
            iNewRow = iNewRow + 2
GetNextResult:
            Try
                fNextResult = dr.NextResult()
                ' modGlobalVar.Msg(dr.FieldCount.ToString, , "field count")
            Catch ex As Exception
                modGlobalVar.msg("error Nextresult", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        Loop

        dr.Close()
        sc.Close()
        MouseDefault()
        'PreventUserSort:  'PREVENT SORTING since ARE UNRELATED ROWS from multiple queries
        '        For Each c As DataGridViewColumn In ClassOpenForms.frmPopupDatagrid.dgvResult.Columns
        '            c.SortMode = DataGridViewColumnSortMode.NotSortable
        '        Next c

FormatGrid:
        Me.StatusProgressBar.Value = 95
        Me.StatusLabel.Text = "formatting grid"

        'bold, shortdates, etc
        modPopup.FormatGrid(grd, iNewRow)
        'ALLOW USER TO ADJUST COLUMN SIZE
        modPopup.FixColumnSize(grd)

CloseAll:
        Me.StatusLabel.Text = "Done"
        Me.StatusProgressBar.Value = 100
        Try
            cmd = Nothing
            sc.Close()
        Catch ex As Exception
            ' modGlobalVar.Msg(ex.Message, , "ERROR:  ")
        End Try
    End Sub

#End Region 'new routines

#Region "Setup"

    'TREEVIEW load
    Private Sub LoadTrees()
Staff:
        Dim sql As New SqlCommand("SELECT StaffID, StaffName, SatelliteRegion, StaffFirstNameFirst, TypeofStaff, Active FROM luStaff WHERE (Selectable = 1) AND  (TypeofStaff IS NOT NULL) AND (StaffID < 1000) ORDER by Staffname", sc)
        Dim tbl As New DataTable("tmptb")
        Me.trvStaff.SuspendLayout()

        Try
            modGlobalVar.LoadDataTable(tbl, sql)
        Catch ex As Exception
            modGlobalVar.msg("ERROR: load trees table ", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        For rw As Int16 = 0 To tbl.Rows.Count - 1
            If tbl.Rows(rw)("Active") = True Then
                Select Case tbl.Rows(rw)("TypeofStaff")
                    Case Is = "Admin"
                        trvStaff.Nodes("ndAllStaff").Nodes("ndStaffActive").Nodes("ndAdmin").Nodes.Add(tbl.Rows(rw)("StaffID"), tbl.Rows(rw)("StaffName"))
                    Case Is = "Consultant", "offsite"
                        trvStaff.Nodes("ndAllStaff").Nodes("ndStaffActive").Nodes("ndConsultant").Nodes.Add(tbl.Rows(rw)("StaffID"), tbl.Rows(rw)("StaffName"))
                    Case Is = "Support"
                        trvStaff.Nodes("ndAllStaff").Nodes("ndStaffActive").Nodes("ndSupport").Nodes.Add(tbl.Rows(rw)("StaffID"), tbl.Rows(rw)("StaffName"))
                End Select
            Else
                trvStaff.Nodes("ndAllStaff").Nodes("ndStaffInactive").Nodes.Add(tbl.Rows(rw)("StaffID"), tbl.Rows(rw)("StaffName"))
            End If
        Next rw
        For rw As Int16 = 0 To tbl.Rows.Count - 1
            If tbl.Rows(rw)("Active") = True Then
                If tbl.Rows(rw)("TypeofStaff") = "offsite" Then
                Else
                    For Each nd As TreeNode In Me.trvStaff.Nodes("ndAllStaff").Nodes("ndStaffRegions").Nodes
                        If tbl.Rows(rw)("SatelliteRegion") = nd.Name.Substring(7) Then
                            nd.Nodes.Add(tbl.Rows(rw)("StaffID"), tbl.Rows(rw)("StaffName"))
                            Exit For
                        End If
                    Next
                End If
            End If
        Next rw
        Me.trvStaff.Nodes("ndAllStaff").Checked = True   '.Nodes("ndConsultant")
        Me.trvStaff.ResumeLayout()
Region:  'in designer
        trvLoadAreas()

        Me.trvRegion.Nodes(0).Expand()
        Me.trvRegion.Nodes(0).Nodes(0).Expand() 'satelliteRegions
ResourceType:
        trvLoadResourceType()
CRG:
        Me.trvCRG.SuspendLayout()
        For Each r As DataRow In tblCRG.Rows
            If r.Item("CRGName").ToString.Contains("SERVICES") Then
                Me.trvCRG.Nodes(0).Nodes("ndServices").Nodes.Add(r.Item("CRGID"), r.Item("CRGName"))
            Else
                For Each nd As TreeNode In Me.trvCRG.Nodes(0).Nodes
                    If UCase(nd.Text.Substring(4, 5)).Contains(r.Item("CRGName").ToString.Substring(0, 5)) Then
                        nd.Nodes.Add(r.Item("CRGID"), r.Item("CRGName"))
                        Exit For
                    End If
                Next nd
            End If
        Next r
        Me.trvCRG.Nodes("ndAllCRG").Checked = True
        Me.trvCRG.Nodes(0).Expand()
        Me.trvCRG.ResumeLayout()

    End Sub

    'LOAD AREA NODES
    Private Sub trvLoadAreas()
        Dim tbl1 As New DataTable
        Dim tbl2 As New DataTable
        'INTERNATIONAL
        Dim sqld As New SqlCommand("SELECT  * FROM vwGetTreeViewCountryList", sc)
        modGlobalVar.LoadDataTable(tbl1, sqld)

        For Each r As DataRow In tbl1.Rows
            trvRegion.Nodes("ndFuture").Nodes("ndIntl").Nodes.Add(r(0).ToString)
        Next r
        'US STATES
        Try
            sqld.CommandText = "SELECT * FROM vwGetTreeViewStateList"
            modGlobalVar.LoadDataTable(tbl2, sqld)
            For Each r As DataRow In tbl2.Rows
                trvRegion.Nodes("ndFuture").Nodes("ndUSA").Nodes.Add(r(0).ToString)
            Next r

            sqld = Nothing
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    'LOAD RESOURCE TYPE NODES
    Private Sub trvLoadResourceType()
        Me.trvType.SuspendLayout()
        Me.trvType.Nodes.Clear()
        Me.trvType.Nodes.Add("AllTypes", "All Resource Types")
        For i As Int16 = 1 To colResourceType.Count
            Me.trvType.Nodes("AllTypes").Nodes.Add(colResourceType(i))
        Next i
        Me.trvType.Nodes("AllTypes").Checked = True
        Me.trvType.ResumeLayout()
    End Sub

    'TREEVIEW Tick Untick
    Private Sub TreeView_AfterCheck(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) _
        Handles trvStaff.AfterCheck, trvRegion.AfterCheck, trvType.AfterCheck, trvCRG.AfterCheck

        RemoveHandler CType(sender, TreeView).AfterCheck, AddressOf TreeView_AfterCheck
        iTrvChecked = 0
        CheckChildNode(e.Node)
        CheckParentNode(e.Node)
        AddHandler CType(sender, TreeView).AfterCheck, AddressOf TreeView_AfterCheck

    End Sub

    'VARIABLE VISIBILITY
    Private Sub HidePanels()
        Me.lblDescription.Text = ""
        Me.pnlDate.Visible = False
        bRegion = False
        bdate = False
        bCRG = False
        bStaff = False
        bType = False
        bSize = False
        Me.rbCRGEither.Checked = True
        Me.grpOnCRG.Visible = False
        Me.trvCRG.Visible = False
        Me.trvType.Visible = False
        Me.trvStaff.Visible = False
        Me.trvRegion.Visible = False
        Me.trvSize.Visible = False
        Me.cboTop10.Visible = False

    End Sub

    'Reset Visibility
    Private Sub TabControl1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TabControl1.SelectedIndexChanged
        HidePanels()
        Me.cboReport.Items.Clear()
        Me.cboReport.Text = String.Empty
    End Sub

    'FILTER CRG COMBOs
    Private Sub trvCRG_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles trvCRG.MouseDown
        If e.Button = System.Windows.Forms.MouseButtons.Right Then

            Dim pp As ContextMenu = New ContextMenu 'crg filter
            Dim i As Integer = 0
            Dim myValue As Object
            Dim eh As EventHandler = AddressOf SelectNode

            'GET USER INPUT
            myValue = InputBox("Enter part of CRG term.  (* to restore full list.)", "Search for CRG category")
            If CType(myValue, String) > "" Then '0-length string returned on cancel
            Else
                Exit Sub
            End If
            pp.MenuItems.Clear()
            'LOAD POPUPMENU
            pp.MenuItems.Add("Select a category")
            pp.MenuItems.Add("------------------")
            Dim copyRows() As DataRow = tblCRG.Select("CRGName LIKE '%" & myValue & "%'")    'dsCRG.Tables("luCRG").Select("CRGName LIKE '%" & myValue & "%'")
            Dim dar As DataRow
            For Each dar In copyRows
                pp.MenuItems.Add(dar.Item(1), eh)
            Next
            pp.MenuItems(0).DefaultItem = True
            pp.Show(Me.trvCRG, New System.Drawing.Point(200, 200)) 'Control.MousePosition))
        End If
    End Sub

    'SELECT NODE  TO USER SELECTION
    Private Sub SelectNode(ByVal obj As Object, ByVal ea As EventArgs)
        Dim bFound As Boolean = False

        For Each nd As TreeNode In Me.trvCRG.Nodes(0).Nodes
            If bFound = True Then
                Exit For
            End If
            For Each n As TreeNode In nd.Nodes
                If UCase(n.Text) = UCase(obj.text) Then
                    n.Checked = True
                    n.Parent.BackColor = Color.LightGray
                    bFound = True
                    Exit For
                Else
                    '  n.Checked = False
                End If
            Next
        Next
        If bFound = True Then
        Else
            modGlobalVar.msg("ERROR: node not found", UCase(obj.text), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If

    End Sub

#End Region 'load treelists

#Region "Set Variables"

    'STAFF ARRAY build staff array from treeview selections
    Private Function SetStaffParam(ByVal bRestricted As Boolean) As String
        Dim strbStaff As New StringBuilder
        Dim bAny As Boolean = False
        'PARENTS: active, inactive, region
        'CHILDREN: Active - admin,consultant, support; Inactive/Region - staffnames
        'GRANDCHILDREN: Active - Staffnames

        If bRestricted = True Then 'only Brent or Catharine can see everyone's numbers re competition
            If usr = DBAdmin.StaffID Or usr = DefaultCaseMgr.StaffID Then '
                ' Return "%"
            Else
                SetStaffParam = usr
                Exit Function
            End If
        End If

        If Me.trvStaff.Nodes("ndAllStaff").Checked = True Then 'ndstaffRegions").Checked = True Or Me.trvStaff.Nodes("ndStaffActive").Checked = True Then
            SetStaffParam = "%"
        Else
            For Each ndChild As TreeNode In Me.trvStaff.Nodes("ndAllStaff").Nodes("ndStaffActive").Nodes    'consultant, admin,support
                For Each ndGrandchild As TreeNode In ndChild.Nodes
                    If ndGrandchild.Checked = True Then
                        bAny = True
                        strbStaff.Append("," & ndGrandchild.Name)
                    End If
                Next ndGrandchild
            Next ndChild
            For Each ndChild As TreeNode In Me.trvStaff.Nodes("ndAllStaff").Nodes("ndStaffRegions").Nodes
                For Each ndGrandchild As TreeNode In ndChild.Nodes
                    If ndGrandchild.Checked = True Then
                        bAny = True
                        strbStaff.Append("," & ndGrandchild.Name)
                    End If
                Next ndGrandchild
            Next ndChild

            For Each ndChild As TreeNode In Me.trvStaff.Nodes("ndAllStaff").Nodes("ndStaffInactive").Nodes
                For Each ndGrandchild As TreeNode In ndChild.Nodes
                    If ndGrandchild.Checked = True Then
                        bAny = True
                        strbStaff.Append("," & ndGrandchild.Name)
                    End If
                Next ndGrandchild
            Next ndChild

            If bAny = True Then
                SetStaffParam = strbStaff.ToString.Substring(1)
            Else
                SetStaffParam = "%"
            End If
        End If
        strbStaff = Nothing
    End Function

    'REGION ARRAY build region array from treeview selections
    Private Function SetRegionParam(ByRef cmd As SqlCommand) As String
        Dim strbRegion As New StringBuilder
        Dim bAny As Boolean = False

        If Me.trvRegion.Nodes(0).Nodes("ndRegions").Checked = True Then
            Return ""
        Else
            For Each ndChild As TreeNode In Me.trvRegion.Nodes(0).Nodes("ndRegions").Nodes
                If ndChild.Checked = True Then
                    bAny = True
                    '  strbRegion.Append(""",""" & ndChild.Text)
                    strbRegion.Append("','" & ndChild.Text)
                End If
            Next ndChild
        End If
        If bAny = True Then
            SetRegionParam = strbRegion.ToString.Substring(3)
        Else                'use deselected all, so use all as default 
            SetRegionParam = "'Central','NE','NW','SE','SW'"
        End If

        If cmd Is Nothing Then  'return plain region string
            Exit Function
        Else                    'return @Region parameter string
            If bAny = True Then
                cmd.Parameters.Add("@Region", SqlDbType.VarChar).Value = strbRegion.ToString.Substring(2) & "'"
            Else
            End If
        End If

        strbRegion = Nothing
    End Function

    'TYPE ARRAY from treeview
    Private Sub SetTypeParam(ByRef cmd As SqlCommand, ByRef strb As StringBuilder)
        Dim bfound As Boolean = False
        Dim tv As TreeView = Me.trvType
Type:
        If tv.Nodes("AllTypes").Checked = True Then
        Else
            For Each n As TreeNode In tv.Nodes("AllTypes").Nodes
                If n.Checked = True Then
                    bfound = True
                    strb.Append(",'" + n.Text + "'")
                End If
            Next
            If bfound = True Then
                strb.Replace(strb.ToString, strb.ToString.Substring(1))
            End If
        End If
        If bfound = False Then
            strb.Append("%")
        Else
            Try
                cmd.Parameters.Add("@Type", SqlDbType.VarChar).Value = strb.ToString
            Catch ex As Exception
                modGlobalVar.msg("ERROR: adding type param ", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    'CRG TREEVIEW - get ID for query, text for Report subheading
    Private Function SetCRGParam(ByRef strbCRGID As StringBuilder, ByRef strbCRGText As StringBuilder) As String 'ByRef cmd As SqlCommand, ByRef strbCRGID As StringBuilder, ByRef strbCRGText As StringBuilder) As String
        Dim bfound As Boolean = False
        Dim tv As TreeView = Me.trvCRG

        If tv.Nodes("ndAllCRG").Checked = True Then
            Return "%"
        Else
            'replace single quotes
            If Me.trvCRG.Nodes(0).Checked = True Then
            Else
                For Each nd As TreeNode In Me.trvCRG.Nodes(0).Nodes
                    If nd.Checked = True Then   'don't list children in text
                        strbCRGText.Append(",'" + Replace(nd.Text, "", "") + "' ")
                        For Each n As TreeNode In nd.Nodes
                            If n.Checked = True Then
                                bfound = True
                                strbCRGID.Append("," + n.Name + "")
                            End If
                        Next n
                    Else
                        For Each n As TreeNode In nd.Nodes
                            If n.Checked = True Then
                                bfound = True
                                strbCRGID.Append("," + n.Name + "")
                                strbCRGText.Append(",'" + Replace(n.Text, "", "") + "' ")
                            End If
                        Next n
                    End If
                Next nd
            End If

            If bfound = True Then
                strbCRGID.Replace(strbCRGID.ToString, strbCRGID.ToString.Substring(1))
                strbCRGText.Replace(strbCRGText.ToString, NextLine & "For CRG terms: " & strbCRGText.ToString.Substring(1))
                SetCRGParam = "yes" 'strbCRGID '.ToString.Substring(1)
            Else
                SetCRGParam = "%"
            End If
        End If

    End Function

    'Get Description for RB single reports and clear DD
    Private Sub rbResourceType_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) _
        Handles rbActiveCongr.MouseDown, rbConvToDate.MouseDown, rbNeverReviewed.MouseDown, rbCurrentCases.MouseDown, rbListCRG.MouseDown, rbCntResourceType.MouseDown
        Me.cboReport.Items.Clear()
        Me.cboReport.Text = String.Empty
        GetDescription(sender.tag)
    End Sub

#End Region 'set variables

End Class

