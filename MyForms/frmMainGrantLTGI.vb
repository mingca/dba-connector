Imports System.Data.SqlClient
Imports System.Text
Imports System

Public Class frmMainGrantLTGI
    Inherits System.Windows.Forms.Form

    '..........needed
    Public isLoaded As Boolean = False  'used for combo boxes during maximize
    Dim cmgr As CurrencyManager
    Dim strDGM, strDGM2 As String
    Dim bDelete As Boolean
    Public iOrg As Integer 'orgid on load
    Dim dtContact As New DataTable("luContacts")
    Dim bCancelClose As Boolean
    Public bChanged As Boolean = False
    Friend WithEvents DsluCasesLTGI As InfoCtr.dsCaseNames
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents btnPlanningGrant As System.Windows.Forms.Button
    Friend WithEvents fldOrgID As System.Windows.Forms.Label
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents fldGINum As System.Windows.Forms.Label
    Friend WithEvents btnImplementationGrant As System.Windows.Forms.Button
    Dim DV As DataView
    '............copied

    'Dim hti As System.Windows.Forms.DataGrid.HitTestInfo
    'Dim gridMouseDownTime As DateTime
    'Dim strbActiveGrid As New StringBuilder
    'Dim dv, dvM, dvS1, dvS2 As DataView   'filter for each datagrid
    'Dim statusM, statusS1, statusS2 As String 'filter text for status bar
    'Dim strDGM, strDGM2, strDGS2 As String  'header text on datagrids
    'Dim frmOpen As frmMainCase 'mainCase - reference to existing form to allow bringtofront
    'Dim iCol As Integer 'selected ID column value
    'Dim ds As DataSet
    'Dim da2 As SqlDataAdapter = New SqlDataAdapter
    'Dim dsCalls As New DataSet("dsCalls"), dtC As New DataTable("dtCalls")
    'Dim dsRecommend As New DataSet, dtR As New DataTable("dtRecommend")
    'Dim dsGrants As New DataSet, dtG As New DataTable("dtGrants")
    'Dim itblStyle As Int16
    'Dim cmd As New SqlCommand
    'Dim strDT As DataTable  'original ds table




#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        '  This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Me.ClientSize = New System.Drawing.Size(990, 650)


        ' Add any initialization after the InitializeComponent() call


    End Sub

    ' Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
        Forms.Remove(Me)
    End Sub

    ' Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents SqlSelectCommand As System.Data.SqlClient.SqlCommand
    Friend WithEvents dsMainGrantLTGI1 As InfoCtr.dsMainGrantLTGI
    Friend WithEvents DsluContactsLTGI As InfoCtr.dsluContacts  'so can close form on delete without modGlobalVar.Msg
    ' Friend WithEvents DsCaseNamesLTGI As WindowsApplication11.dsCaseNames
    Friend WithEvents TextBox6 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox5 As System.Windows.Forms.TextBox
    Friend WithEvents cboClergyLeader As System.Windows.Forms.ComboBox
    Friend WithEvents cboPD As System.Windows.Forms.ComboBox
    Friend WithEvents btnHelp As System.Windows.Forms.Button


    Friend WithEvents fldGotoOrg As System.Windows.Forms.TextBox
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents OrgNum As System.Windows.Forms.TextBox
    Friend WithEvents CaseNum As System.Windows.Forms.TextBox
    Friend WithEvents GrantID As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents cboCase As System.Windows.Forms.ComboBox
    Friend WithEvents lblStaff As System.Windows.Forms.Label
    Friend WithEvents fldGotoCase As System.Windows.Forms.TextBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents fldContact As System.Windows.Forms.Textbox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Private WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents OrigDeadline As System.Windows.Forms.Label
    Friend WithEvents Extensions As System.Windows.Forms.Label
    Friend WithEvents StatusBar1 As System.Windows.Forms.StatusBar
    Friend WithEvents StatusBarPanel1 As System.Windows.Forms.StatusBarPanel
    Friend WithEvents StatusBarPanel2 As System.Windows.Forms.StatusBarPanel
    ' Friend WithEvents dsMainGI1 As WindowsApplication11.dsMainGI
    Friend WithEvents cboCallNotified As System.Windows.Forms.ComboBox
    Friend WithEvents txtClassGroup As System.Windows.Forms.TextBox
    ' Friend WithEvents txtNotes As System.Windows.Forms.TextBox
    Friend WithEvents txtPlanningDeadline As System.Windows.Forms.TextBox
    Friend WithEvents cboOnsiteConsultant As System.Windows.Forms.ComboBox
    Friend WithEvents txtTentative As System.Windows.Forms.TextBox
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents dtReminderSummaryReport As System.Windows.Forms.TextBox
    Friend WithEvents dtNotifyCallDate As System.Windows.Forms.TextBox
    Friend WithEvents dtDeterminationDate As System.Windows.Forms.TextBox
    Friend WithEvents SummaryReportRecd As System.Windows.Forms.TextBox
    Friend WithEvents cboDetermination As System.Windows.Forms.ComboBox
    Friend WithEvents daspCaseNames As System.Data.SqlClient.SqlDataAdapter
    '   Friend WithEvents DsContactNames1 As WindowsApplication11.dsContactName
    Friend WithEvents daspContactNames As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents MainMenu1 As System.Windows.Forms.MainMenu
    Friend WithEvents MenuItem1 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem2 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem3 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem4 As System.Windows.Forms.MenuItem
    Friend WithEvents miSave As System.Windows.Forms.MenuItem
    Friend WithEvents miCancel As System.Windows.Forms.MenuItem
    Friend WithEvents miDelete As System.Windows.Forms.MenuItem
    Friend WithEvents miClose As System.Windows.Forms.MenuItem
    Friend WithEvents miGotoCase As System.Windows.Forms.MenuItem
    Friend WithEvents miGotoResource As System.Windows.Forms.MenuItem
    Friend WithEvents pnlApplication As System.Windows.Forms.Panel
    Friend WithEvents pnlImplement As System.Windows.Forms.Panel
    Friend WithEvents pnlPlan As System.Windows.Forms.Panel
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label36 As System.Windows.Forms.Label
    Friend WithEvents Label37 As System.Windows.Forms.Label
    Friend WithEvents daspMainLTGI As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents CheckBox4 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox5 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox6 As System.Windows.Forms.CheckBox
    Friend WithEvents Label38 As System.Windows.Forms.Label
    Friend WithEvents CheckBox8 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox9 As System.Windows.Forms.CheckBox
    Friend WithEvents txtRemarks As System.Windows.Forms.TextBox
    Friend WithEvents pnlProcess As System.Windows.Forms.Panel
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents TextBox2 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox3 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox4 As System.Windows.Forms.TextBox
    Friend WithEvents Label39 As System.Windows.Forms.Label
    '   Friend WithEvents SqlDataAdapter1 As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents Label41 As System.Windows.Forms.Label
    Friend WithEvents Label42 As System.Windows.Forms.Label
    Friend WithEvents txtClassConfirmed As System.Windows.Forms.TextBox
    Friend WithEvents Label43 As System.Windows.Forms.Label
    Friend WithEvents txtPlanningRecd As System.Windows.Forms.TextBox
    Friend WithEvents CheckBox1 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox2 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox3 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox10 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox11 As System.Windows.Forms.CheckBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents txtImplementationRecd As System.Windows.Forms.TextBox
    Friend WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents TextBox8 As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents btnSaveExit As Windows.Forms.Button
    ' Friend WithEvents SqlSelectCommand2 As System.Data.SqlClient.SqlCommand
    Friend WithEvents TextBox9 As System.Windows.Forms.TextBox
    Friend WithEvents txtNotes As System.Windows.Forms.TextBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents dtCallNotifiedDate As System.Windows.Forms.TextBox
    Friend WithEvents txtAppRecd As System.Windows.Forms.TextBox
    Friend WithEvents SqlSelectCommand3 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlSelectCommand4 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlSelectCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlUpdateCommand1 As System.Data.SqlClient.SqlCommand

    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMainGrantLTGI))
        Me.fldGotoOrg = New System.Windows.Forms.TextBox()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.OrgNum = New System.Windows.Forms.TextBox()
        Me.dsMainGrantLTGI1 = New InfoCtr.dsMainGrantLTGI()
        Me.CaseNum = New System.Windows.Forms.TextBox()
        Me.GrantID = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.pnlApplication = New System.Windows.Forms.Panel()
        Me.TextBox6 = New System.Windows.Forms.TextBox()
        Me.TextBox5 = New System.Windows.Forms.TextBox()
        Me.cboClergyLeader = New System.Windows.Forms.ComboBox()
        Me.cboPD = New System.Windows.Forms.ComboBox()
        Me.DsluContactsLTGI = New InfoCtr.dsluContacts()
        Me.txtAppRecd = New System.Windows.Forms.TextBox()
        Me.txtRemarks = New System.Windows.Forms.TextBox()
        Me.CheckBox8 = New System.Windows.Forms.CheckBox()
        Me.CheckBox9 = New System.Windows.Forms.CheckBox()
        Me.CheckBox4 = New System.Windows.Forms.CheckBox()
        Me.CheckBox5 = New System.Windows.Forms.CheckBox()
        Me.CheckBox6 = New System.Windows.Forms.CheckBox()
        Me.Label38 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtTentative = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.fldContact = New System.Windows.Forms.TextBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label39 = New System.Windows.Forms.Label()
        Me.TextBox4 = New System.Windows.Forms.TextBox()
        Me.cboCallNotified = New System.Windows.Forms.ComboBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtClassGroup = New System.Windows.Forms.TextBox()
        Me.txtPlanningDeadline = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.cboCase = New System.Windows.Forms.ComboBox()
        Me.DsluCasesLTGI = New InfoCtr.dsCaseNames()
        Me.fldGotoCase = New System.Windows.Forms.TextBox()
        Me.cboOnsiteConsultant = New System.Windows.Forms.ComboBox()
        Me.pnlImplement = New System.Windows.Forms.Panel()
        Me.btnImplementationGrant = New System.Windows.Forms.Button()
        Me.CheckBox10 = New System.Windows.Forms.CheckBox()
        Me.CheckBox11 = New System.Windows.Forms.CheckBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.txtImplementationRecd = New System.Windows.Forms.TextBox()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.TextBox8 = New System.Windows.Forms.TextBox()
        Me.cboDetermination = New System.Windows.Forms.ComboBox()
        Me.dtCallNotifiedDate = New System.Windows.Forms.TextBox()
        Me.dtDeterminationDate = New System.Windows.Forms.TextBox()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.pnlPlan = New System.Windows.Forms.Panel()
        Me.btnPlanningGrant = New System.Windows.Forms.Button()
        Me.lblStaff = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.txtNotes = New System.Windows.Forms.TextBox()
        Me.TextBox9 = New System.Windows.Forms.TextBox()
        Me.CheckBox3 = New System.Windows.Forms.CheckBox()
        Me.CheckBox2 = New System.Windows.Forms.CheckBox()
        Me.CheckBox1 = New System.Windows.Forms.CheckBox()
        Me.Label43 = New System.Windows.Forms.Label()
        Me.txtPlanningRecd = New System.Windows.Forms.TextBox()
        Me.Label42 = New System.Windows.Forms.Label()
        Me.txtClassConfirmed = New System.Windows.Forms.TextBox()
        Me.Label41 = New System.Windows.Forms.Label()
        Me.StatusBar1 = New System.Windows.Forms.StatusBar()
        Me.StatusBarPanel1 = New System.Windows.Forms.StatusBarPanel()
        Me.StatusBarPanel2 = New System.Windows.Forms.StatusBarPanel()
        Me.daspCaseNames = New System.Data.SqlClient.SqlDataAdapter()
        Me.SqlSelectCommand3 = New System.Data.SqlClient.SqlCommand()
        Me.daspContactNames = New System.Data.SqlClient.SqlDataAdapter()
        Me.SqlSelectCommand = New System.Data.SqlClient.SqlCommand()
        Me.SqlSelectCommand4 = New System.Data.SqlClient.SqlCommand()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.btnSaveExit = New System.Windows.Forms.Button()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.TextBox3 = New System.Windows.Forms.TextBox()
        Me.MainMenu1 = New System.Windows.Forms.MainMenu(Me.components)
        Me.MenuItem1 = New System.Windows.Forms.MenuItem()
        Me.miClose = New System.Windows.Forms.MenuItem()
        Me.MenuItem2 = New System.Windows.Forms.MenuItem()
        Me.miSave = New System.Windows.Forms.MenuItem()
        Me.miCancel = New System.Windows.Forms.MenuItem()
        Me.miDelete = New System.Windows.Forms.MenuItem()
        Me.MenuItem3 = New System.Windows.Forms.MenuItem()
        Me.MenuItem4 = New System.Windows.Forms.MenuItem()
        Me.miGotoCase = New System.Windows.Forms.MenuItem()
        Me.miGotoResource = New System.Windows.Forms.MenuItem()
        Me.daspMainLTGI = New System.Data.SqlClient.SqlDataAdapter()
        Me.SqlSelectCommand1 = New System.Data.SqlClient.SqlCommand()
        Me.SqlUpdateCommand1 = New System.Data.SqlClient.SqlCommand()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label36 = New System.Windows.Forms.Label()
        Me.Label37 = New System.Windows.Forms.Label()
        Me.pnlProcess = New System.Windows.Forms.Panel()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.btnHelp = New System.Windows.Forms.Button()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.fldOrgID = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.fldGINum = New System.Windows.Forms.Label()
        CType(Me.dsMainGrantLTGI1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlApplication.SuspendLayout()
        CType(Me.DsluContactsLTGI, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsluCasesLTGI, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlImplement.SuspendLayout()
        Me.pnlPlan.SuspendLayout()
        CType(Me.StatusBarPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.StatusBarPanel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlProcess.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'fldGotoOrg
        '
        Me.fldGotoOrg.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fldGotoOrg.ForeColor = System.Drawing.Color.ForestGreen
        Me.fldGotoOrg.Location = New System.Drawing.Point(400, 5)
        Me.fldGotoOrg.Name = "fldGotoOrg"
        Me.fldGotoOrg.Size = New System.Drawing.Size(328, 20)
        Me.fldGotoOrg.TabIndex = 221
        Me.fldGotoOrg.Text = "should be org name"
        '
        'btnDelete
        '
        Me.btnDelete.BackColor = System.Drawing.SystemColors.Control
        Me.btnDelete.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Image = CType(resources.GetObject("btnDelete.Image"), System.Drawing.Image)
        Me.btnDelete.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnDelete.Location = New System.Drawing.Point(3, 2)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(40, 35)
        Me.btnDelete.TabIndex = 220
        Me.btnDelete.Text = "Delete"
        Me.btnDelete.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.btnDelete, "Delete this LTGI Application")
        Me.btnDelete.UseVisualStyleBackColor = False
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.Desktop
        Me.Label2.Location = New System.Drawing.Point(20, 20)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(180, 14)
        Me.Label2.TabIndex = 219
        Me.Label2.Text = "LIFE TOGETHER (LTGI)"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'OrgNum
        '
        Me.OrgNum.BackColor = System.Drawing.SystemColors.Control
        Me.OrgNum.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.OrgNum.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.dsMainGrantLTGI1, "MainGrantLTGI.OrgNum", True))
        Me.OrgNum.Enabled = False
        Me.OrgNum.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OrgNum.ForeColor = System.Drawing.SystemColors.GrayText
        Me.OrgNum.Location = New System.Drawing.Point(888, 48)
        Me.OrgNum.Name = "OrgNum"
        Me.OrgNum.Size = New System.Drawing.Size(41, 11)
        Me.OrgNum.TabIndex = 227
        Me.OrgNum.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'dsMainGrantLTGI1
        '
        Me.dsMainGrantLTGI1.DataSetName = "dsMainGrantLTGI"
        Me.dsMainGrantLTGI1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'CaseNum
        '
        Me.CaseNum.BackColor = System.Drawing.SystemColors.Control
        Me.CaseNum.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.CaseNum.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.dsMainGrantLTGI1, "MainGrantLTGI.CaseNum", True))
        Me.CaseNum.Enabled = False
        Me.CaseNum.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CaseNum.ForeColor = System.Drawing.SystemColors.GrayText
        Me.CaseNum.Location = New System.Drawing.Point(325, 145)
        Me.CaseNum.Name = "CaseNum"
        Me.CaseNum.ReadOnly = True
        Me.CaseNum.Size = New System.Drawing.Size(41, 11)
        Me.CaseNum.TabIndex = 223
        Me.CaseNum.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'GrantID
        '
        Me.GrantID.BackColor = System.Drawing.SystemColors.Control
        Me.GrantID.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.GrantID.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.dsMainGrantLTGI1, "MainGrantLTGI.GIID", True))
        Me.GrantID.Enabled = False
        Me.GrantID.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GrantID.ForeColor = System.Drawing.SystemColors.GrayText
        Me.GrantID.Location = New System.Drawing.Point(888, 2)
        Me.GrantID.Multiline = True
        Me.GrantID.Name = "GrantID"
        Me.GrantID.Size = New System.Drawing.Size(41, 26)
        Me.GrantID.TabIndex = 222
        Me.GrantID.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label9
        '
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.SystemColors.ControlDark
        Me.Label9.Location = New System.Drawing.Point(840, 32)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(47, 14)
        Me.Label9.TabIndex = 225
        Me.Label9.Text = "Case #"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlDark
        Me.Label6.Location = New System.Drawing.Point(848, 8)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(32, 14)
        Me.Label6.TabIndex = 224
        Me.Label6.Text = "Grant #"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label11
        '
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.SystemColors.ControlDark
        Me.Label11.Location = New System.Drawing.Point(840, 48)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(47, 14)
        Me.Label11.TabIndex = 226
        Me.Label11.Text = "Org #"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'pnlApplication
        '
        Me.pnlApplication.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.pnlApplication.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pnlApplication.Controls.Add(Me.TextBox6)
        Me.pnlApplication.Controls.Add(Me.TextBox5)
        Me.pnlApplication.Controls.Add(Me.cboClergyLeader)
        Me.pnlApplication.Controls.Add(Me.cboPD)
        Me.pnlApplication.Controls.Add(Me.txtAppRecd)
        Me.pnlApplication.Controls.Add(Me.txtRemarks)
        Me.pnlApplication.Controls.Add(Me.CheckBox8)
        Me.pnlApplication.Controls.Add(Me.CheckBox9)
        Me.pnlApplication.Controls.Add(Me.CheckBox4)
        Me.pnlApplication.Controls.Add(Me.CheckBox5)
        Me.pnlApplication.Controls.Add(Me.CheckBox6)
        Me.pnlApplication.Controls.Add(Me.Label38)
        Me.pnlApplication.Controls.Add(Me.Label3)
        Me.pnlApplication.Controls.Add(Me.txtTentative)
        Me.pnlApplication.Controls.Add(Me.Label16)
        Me.pnlApplication.Controls.Add(Me.fldContact)
        Me.pnlApplication.Controls.Add(Me.Label19)
        Me.pnlApplication.Controls.Add(Me.Label39)
        Me.pnlApplication.Controls.Add(Me.TextBox4)
        Me.pnlApplication.Location = New System.Drawing.Point(16, 72)
        Me.pnlApplication.Name = "pnlApplication"
        Me.pnlApplication.Size = New System.Drawing.Size(392, 280)
        Me.pnlApplication.TabIndex = 0
        '
        'TextBox6
        '
        Me.TextBox6.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.dsMainGrantLTGI1, "MainGrantLTGI.ProjectDirectorNum", True))
        Me.TextBox6.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox6.Location = New System.Drawing.Point(336, 171)
        Me.TextBox6.Name = "TextBox6"
        Me.TextBox6.ReadOnly = True
        Me.TextBox6.Size = New System.Drawing.Size(41, 18)
        Me.TextBox6.TabIndex = 255
        '
        'TextBox5
        '
        Me.TextBox5.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.dsMainGrantLTGI1, "MainGrantLTGI.ClergyLeaderNum", True))
        Me.TextBox5.Enabled = False
        Me.TextBox5.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox5.Location = New System.Drawing.Point(335, 147)
        Me.TextBox5.Name = "TextBox5"
        Me.TextBox5.ReadOnly = True
        Me.TextBox5.Size = New System.Drawing.Size(42, 18)
        Me.TextBox5.TabIndex = 254
        '
        'cboClergyLeader
        '
        Me.cboClergyLeader.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.dsMainGrantLTGI1, "MainGrantLTGI.ClergyLeaderNum", True))
        Me.cboClergyLeader.FormattingEnabled = True
        Me.cboClergyLeader.Location = New System.Drawing.Point(95, 146)
        Me.cboClergyLeader.Name = "cboClergyLeader"
        Me.cboClergyLeader.Size = New System.Drawing.Size(222, 21)
        Me.cboClergyLeader.TabIndex = 7
        '
        'cboPD
        '
        Me.cboPD.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.dsMainGrantLTGI1, "MainGrantLTGI.ProjectDirectorNum", True))
        Me.cboPD.DataSource = Me.DsluContactsLTGI
        Me.cboPD.DisplayMember = "luContactNames.ContactStaff"
        Me.cboPD.FormattingEnabled = True
        Me.cboPD.Location = New System.Drawing.Point(95, 173)
        Me.cboPD.Name = "cboPD"
        Me.cboPD.Size = New System.Drawing.Size(222, 21)
        Me.cboPD.TabIndex = 8
        Me.cboPD.ValueMember = "luContactNames.ContactID"
        '
        'DsluContactsLTGI
        '
        Me.DsluContactsLTGI.DataSetName = "dsluContacts"
        Me.DsluContactsLTGI.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'txtAppRecd
        '
        Me.txtAppRecd.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.dsMainGrantLTGI1, "MainGrantLTGI.AppRecdDt", True))
        Me.txtAppRecd.Location = New System.Drawing.Point(172, 11)
        Me.txtAppRecd.Name = "txtAppRecd"
        Me.txtAppRecd.Size = New System.Drawing.Size(64, 20)
        Me.txtAppRecd.TabIndex = 0
        Me.txtAppRecd.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ToolTip1.SetToolTip(Me.txtAppRecd, "Date")
        '
        'txtRemarks
        '
        Me.txtRemarks.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.dsMainGrantLTGI1, "MainGrantLTGI.Remarks", True))
        Me.txtRemarks.Location = New System.Drawing.Point(15, 50)
        Me.txtRemarks.Multiline = True
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.ReadOnly = True
        Me.txtRemarks.Size = New System.Drawing.Size(142, 89)
        Me.txtRemarks.TabIndex = 1
        Me.txtRemarks.Text = "Enclosure Remarks"
        '
        'CheckBox8
        '
        Me.CheckBox8.DataBindings.Add(New System.Windows.Forms.Binding("Checked", Me.dsMainGrantLTGI1, "MainGrantLTGI.InclPostmark", True))
        Me.CheckBox8.Location = New System.Drawing.Point(168, 125)
        Me.CheckBox8.Name = "CheckBox8"
        Me.CheckBox8.Size = New System.Drawing.Size(173, 19)
        Me.CheckBox8.TabIndex = 6
        Me.CheckBox8.Text = "Postmarked by 1/11/2007"
        '
        'CheckBox9
        '
        Me.CheckBox9.DataBindings.Add(New System.Windows.Forms.Binding("Checked", Me.dsMainGrantLTGI1, "MainGrantLTGI.InclCongProfile", True))
        Me.CheckBox9.Location = New System.Drawing.Point(167, 103)
        Me.CheckBox9.Name = "CheckBox9"
        Me.CheckBox9.Size = New System.Drawing.Size(210, 24)
        Me.CheckBox9.TabIndex = 5
        Me.CheckBox9.Text = "Application Complete (Congr. Profile)"
        '
        'CheckBox4
        '
        Me.CheckBox4.DataBindings.Add(New System.Windows.Forms.Binding("Checked", Me.dsMainGrantLTGI1, "MainGrantLTGI.InclAbility501c3", True))
        Me.CheckBox4.Location = New System.Drawing.Point(168, 88)
        Me.CheckBox4.Name = "CheckBox4"
        Me.CheckBox4.Size = New System.Drawing.Size(195, 16)
        Me.CheckBox4.TabIndex = 4
        Me.CheckBox4.Text = "Ability to Provide 501c3 included"
        '
        'CheckBox5
        '
        Me.CheckBox5.DataBindings.Add(New System.Windows.Forms.Binding("Checked", Me.dsMainGrantLTGI1, "MainGrantLTGI.InclSignatures", True))
        Me.CheckBox5.Location = New System.Drawing.Point(167, 68)
        Me.CheckBox5.Name = "CheckBox5"
        Me.CheckBox5.Size = New System.Drawing.Size(150, 19)
        Me.CheckBox5.TabIndex = 3
        Me.CheckBox5.Text = "Both Signatures included"
        '
        'CheckBox6
        '
        Me.CheckBox6.DataBindings.Add(New System.Windows.Forms.Binding("Checked", Me.dsMainGrantLTGI1, "MainGrantLTGI.InServiceArea", True))
        Me.CheckBox6.Location = New System.Drawing.Point(167, 48)
        Me.CheckBox6.Name = "CheckBox6"
        Me.CheckBox6.Size = New System.Drawing.Size(111, 21)
        Me.CheckBox6.TabIndex = 2
        Me.CheckBox6.Text = "In Service Area"
        '
        'Label38
        '
        Me.Label38.BackColor = System.Drawing.Color.Transparent
        Me.Label38.Location = New System.Drawing.Point(87, 34)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(89, 16)
        Me.Label38.TabIndex = 251
        Me.Label38.Text = "Enclosures:"
        Me.Label38.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(26, 171)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(64, 20)
        Me.Label3.TabIndex = 221
        Me.Label3.Tag = ""
        Me.Label3.Text = "Project Dir."
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtTentative
        '
        Me.txtTentative.AcceptsReturn = True
        Me.txtTentative.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.dsMainGrantLTGI1, "MainGrantLTGI.TentativePlan", True))
        Me.txtTentative.Location = New System.Drawing.Point(71, 199)
        Me.txtTentative.Multiline = True
        Me.txtTentative.Name = "txtTentative"
        Me.txtTentative.Size = New System.Drawing.Size(313, 42)
        Me.txtTentative.TabIndex = 9
        Me.txtTentative.Text = "TextBox4"
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.Color.Transparent
        Me.Label16.Location = New System.Drawing.Point(5, 204)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(56, 37)
        Me.Label16.TabIndex = 150
        Me.Label16.Text = "Tentative Plan"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'fldContact
        '
        Me.fldContact.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fldContact.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fldContact.Location = New System.Drawing.Point(16, 145)
        Me.fldContact.Name = "fldContact"
        Me.fldContact.Size = New System.Drawing.Size(77, 20)
        Me.fldContact.TabIndex = 147
        Me.fldContact.Tag = ""
        Me.fldContact.Text = "Clergy Leader"
        '
        'Label19
        '
        Me.Label19.Location = New System.Drawing.Point(8, 8)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(147, 24)
        Me.Label19.TabIndex = 7
        Me.Label19.Text = "Date Application Received"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label39
        '
        Me.Label39.BackColor = System.Drawing.Color.Transparent
        Me.Label39.Location = New System.Drawing.Point(0, 250)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(148, 24)
        Me.Label39.TabIndex = 229
        Me.Label39.Text = "Date Mailed Receipt of Appl."
        Me.Label39.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'TextBox4
        '
        Me.TextBox4.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.dsMainGrantLTGI1, "MainGrantLTGI.MailedNoticeReceipt", True))
        Me.TextBox4.Location = New System.Drawing.Point(154, 252)
        Me.TextBox4.Name = "TextBox4"
        Me.TextBox4.Size = New System.Drawing.Size(72, 20)
        Me.TextBox4.TabIndex = 10
        Me.TextBox4.Text = "Date"
        Me.TextBox4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ToolTip1.SetToolTip(Me.TextBox4, "Date")
        '
        'cboCallNotified
        '
        Me.cboCallNotified.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.dsMainGrantLTGI1, "MainGrantLTGI.CallNotifiedStaffNum", True))
        Me.cboCallNotified.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCallNotified.Location = New System.Drawing.Point(234, 55)
        Me.cboCallNotified.Name = "cboCallNotified"
        Me.cboCallNotified.Size = New System.Drawing.Size(152, 21)
        Me.cboCallNotified.TabIndex = 3
        Me.ToolTip1.SetToolTip(Me.cboCallNotified, "Staff name")
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Location = New System.Drawing.Point(0, 103)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(148, 24)
        Me.Label7.TabIndex = 223
        Me.Label7.Text = "Mailed Participation Agreemnt"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtClassGroup
        '
        Me.txtClassGroup.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.dsMainGrantLTGI1, "MainGrantLTGI.ClassGroup", True))
        Me.txtClassGroup.Location = New System.Drawing.Point(145, 37)
        Me.txtClassGroup.Name = "txtClassGroup"
        Me.txtClassGroup.Size = New System.Drawing.Size(102, 20)
        Me.txtClassGroup.TabIndex = 1
        Me.txtClassGroup.Tag = ""
        Me.txtClassGroup.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ToolTip1.SetToolTip(Me.txtClassGroup, "??")
        '
        'txtPlanningDeadline
        '
        Me.txtPlanningDeadline.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.dsMainGrantLTGI1, "MainGrantLTGI.PlanAppDeadline", True))
        Me.txtPlanningDeadline.Location = New System.Drawing.Point(214, 204)
        Me.txtPlanningDeadline.Name = "txtPlanningDeadline"
        Me.txtPlanningDeadline.Size = New System.Drawing.Size(72, 20)
        Me.txtPlanningDeadline.TabIndex = 6
        Me.txtPlanningDeadline.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ToolTip1.SetToolTip(Me.txtPlanningDeadline, "Date")
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Location = New System.Drawing.Point(51, 37)
        Me.Label1.Margin = New System.Windows.Forms.Padding(0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(88, 23)
        Me.Label1.TabIndex = 219
        Me.Label1.Text = "Class Group"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Location = New System.Drawing.Point(6, 207)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(200, 16)
        Me.Label8.TabIndex = 6
        Me.Label8.Text = "Planning Grant Application Deadline"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cboCase
        '
        Me.cboCase.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.dsMainGrantLTGI1, "MainGrantLTGI.CaseNum", True))
        'Me.cboCase.DataSource = Me.DsGetCases 'dsluCasesLTGI
        Me.cboCase.DisplayMember = "luCaseName.CaseName"
        Me.cboCase.Location = New System.Drawing.Point(95, 141)
        Me.cboCase.Name = "cboCase"
        Me.cboCase.Size = New System.Drawing.Size(224, 21)
        Me.cboCase.TabIndex = 7
        Me.cboCase.ValueMember = "luCaseName.CaseID"
        ''
        ''DsluCasesLTGI
        ''
        'Me.DsluCasesLTGI.DataSetName = "dsluCases"
        'Me.DsluCasesLTGI.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'fldGotoCase
        '
        Me.fldGotoCase.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fldGotoCase.ForeColor = System.Drawing.Color.ForestGreen
        Me.fldGotoCase.Location = New System.Drawing.Point(15, 142)
        Me.fldGotoCase.Name = "fldGotoCase"
        Me.fldGotoCase.Size = New System.Drawing.Size(72, 20)
        Me.fldGotoCase.TabIndex = 162
        Me.fldGotoCase.Tag = "Case"
        Me.fldGotoCase.Text = "Case Name"
        Me.ToolTip1.SetToolTip(Me.fldGotoCase, "Goto Case")
        '
        'cboOnsiteConsultant
        '
        Me.cboOnsiteConsultant.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.dsMainGrantLTGI1, "MainGrantLTGI.OnSiteConsultantTxt", True))
        Me.cboOnsiteConsultant.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboOnsiteConsultant.Location = New System.Drawing.Point(99, 101)
        Me.cboOnsiteConsultant.Name = "cboOnsiteConsultant"
        Me.cboOnsiteConsultant.Size = New System.Drawing.Size(187, 21)
        Me.cboOnsiteConsultant.TabIndex = 6
        Me.cboOnsiteConsultant.Visible = False
        '
        'pnlImplement
        '
        Me.pnlImplement.BackColor = System.Drawing.Color.FromArgb(CType(CType(250, Byte), Integer), CType(CType(180, Byte), Integer), CType(CType(145, Byte), Integer))
        Me.pnlImplement.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pnlImplement.Controls.Add(Me.btnImplementationGrant)
        Me.pnlImplement.Controls.Add(Me.CheckBox10)
        Me.pnlImplement.Controls.Add(Me.CheckBox11)
        Me.pnlImplement.Controls.Add(Me.Label20)
        Me.pnlImplement.Controls.Add(Me.txtImplementationRecd)
        Me.pnlImplement.Controls.Add(Me.Label31)
        Me.pnlImplement.Controls.Add(Me.TextBox8)
        Me.pnlImplement.Location = New System.Drawing.Point(432, 384)
        Me.pnlImplement.Name = "pnlImplement"
        Me.pnlImplement.Size = New System.Drawing.Size(488, 184)
        Me.pnlImplement.TabIndex = 3
        '
        'btnImplementationGrant
        '
        Me.btnImplementationGrant.Location = New System.Drawing.Point(321, 155)
        Me.btnImplementationGrant.Name = "btnImplementationGrant"
        Me.btnImplementationGrant.Size = New System.Drawing.Size(151, 22)
        Me.btnImplementationGrant.TabIndex = 263
        Me.btnImplementationGrant.Tag = "Implementation"
        Me.btnImplementationGrant.Text = "Go to Implementation Grant"
        Me.btnImplementationGrant.UseVisualStyleBackColor = True
        '
        'CheckBox10
        '
        Me.CheckBox10.DataBindings.Add(New System.Windows.Forms.Binding("Checked", Me.dsMainGrantLTGI1, "MainGrantLTGI.Attended5", True))
        Me.CheckBox10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CheckBox10.Location = New System.Drawing.Point(31, 55)
        Me.CheckBox10.Name = "CheckBox10"
        Me.CheckBox10.Size = New System.Drawing.Size(175, 21)
        Me.CheckBox10.TabIndex = 1
        Me.CheckBox10.Text = "Attended Session 1/2"
        '
        'CheckBox11
        '
        Me.CheckBox11.DataBindings.Add(New System.Windows.Forms.Binding("Checked", Me.dsMainGrantLTGI1, "MainGrantLTGI.Attended4", True))
        Me.CheckBox11.Location = New System.Drawing.Point(31, 31)
        Me.CheckBox11.Name = "CheckBox11"
        Me.CheckBox11.Size = New System.Drawing.Size(175, 21)
        Me.CheckBox11.TabIndex = 0
        Me.CheckBox11.Text = "Attended Session 4"
        '
        'Label20
        '
        Me.Label20.BackColor = System.Drawing.Color.Transparent
        Me.Label20.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label20.Location = New System.Drawing.Point(23, 124)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(232, 16)
        Me.Label20.TabIndex = 261
        Me.Label20.Text = "Implementation Grant Application Received"
        '
        'txtImplementationRecd
        '
        Me.txtImplementationRecd.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.dsMainGrantLTGI1, "MainGrantLTGI.ImplementAppRecd", True))
        Me.txtImplementationRecd.Location = New System.Drawing.Point(253, 122)
        Me.txtImplementationRecd.Name = "txtImplementationRecd"
        Me.txtImplementationRecd.Size = New System.Drawing.Size(90, 20)
        Me.txtImplementationRecd.TabIndex = 3
        Me.txtImplementationRecd.Tag = "Date"
        Me.txtImplementationRecd.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ToolTip1.SetToolTip(Me.txtImplementationRecd, "Filling in this date will generate a new grant.")
        '
        'Label31
        '
        Me.Label31.BackColor = System.Drawing.Color.Transparent
        Me.Label31.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label31.Location = New System.Drawing.Point(15, 95)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(232, 16)
        Me.Label31.TabIndex = 259
        Me.Label31.Text = "Implementation Grant Application Deadline"
        '
        'TextBox8
        '
        Me.TextBox8.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.dsMainGrantLTGI1, "MainGrantLTGI.ImplementAppDeadline", True))
        Me.TextBox8.Location = New System.Drawing.Point(253, 95)
        Me.TextBox8.Name = "TextBox8"
        Me.TextBox8.Size = New System.Drawing.Size(90, 20)
        Me.TextBox8.TabIndex = 2
        Me.TextBox8.Tag = "Date"
        Me.TextBox8.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ToolTip1.SetToolTip(Me.TextBox8, "Date")
        '
        'cboDetermination
        '
        Me.cboDetermination.DataBindings.Add(New System.Windows.Forms.Binding("SelectedItem", Me.dsMainGrantLTGI1, "MainGrantLTGI.Determination", True))
        Me.cboDetermination.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDetermination.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboDetermination.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cboDetermination.Items.AddRange(New Object() {"Approved", "Denied", "Waiting List"})
        Me.cboDetermination.Location = New System.Drawing.Point(234, 31)
        Me.cboDetermination.Name = "cboDetermination"
        Me.cboDetermination.Size = New System.Drawing.Size(120, 21)
        Me.cboDetermination.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.cboDetermination, "Approved, Denied")
        '
        'dtCallNotifiedDate
        '
        Me.dtCallNotifiedDate.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.dsMainGrantLTGI1, "MainGrantLTGI.CallNotifiedDate", True))
        Me.dtCallNotifiedDate.Location = New System.Drawing.Point(154, 55)
        Me.dtCallNotifiedDate.Name = "dtCallNotifiedDate"
        Me.dtCallNotifiedDate.Size = New System.Drawing.Size(72, 20)
        Me.dtCallNotifiedDate.TabIndex = 2
        Me.dtCallNotifiedDate.Text = "Date"
        Me.dtCallNotifiedDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ToolTip1.SetToolTip(Me.dtCallNotifiedDate, "Date")
        '
        'dtDeterminationDate
        '
        Me.dtDeterminationDate.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.dsMainGrantLTGI1, "MainGrantLTGI.DeterminationDate", True))
        Me.dtDeterminationDate.Location = New System.Drawing.Point(154, 31)
        Me.dtDeterminationDate.Name = "dtDeterminationDate"
        Me.dtDeterminationDate.Size = New System.Drawing.Size(72, 20)
        Me.dtDeterminationDate.TabIndex = 0
        Me.dtDeterminationDate.Text = "Date"
        Me.dtDeterminationDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ToolTip1.SetToolTip(Me.dtDeterminationDate, "Date")
        '
        'Label21
        '
        Me.Label21.BackColor = System.Drawing.Color.Transparent
        Me.Label21.Location = New System.Drawing.Point(18, 55)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(128, 16)
        Me.Label21.TabIndex = 225
        Me.Label21.Text = "Phone Notification Made"
        Me.Label21.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.Color.Transparent
        Me.Label15.Location = New System.Drawing.Point(66, 31)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(80, 16)
        Me.Label15.TabIndex = 221
        Me.Label15.Text = "Determination"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'pnlPlan
        '
        Me.pnlPlan.BackColor = System.Drawing.Color.FromArgb(CType(CType(250, Byte), Integer), CType(CType(210, Byte), Integer), CType(CType(180, Byte), Integer))
        Me.pnlPlan.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pnlPlan.Controls.Add(Me.btnPlanningGrant)
        Me.pnlPlan.Controls.Add(Me.lblStaff)
        Me.pnlPlan.Controls.Add(Me.Label18)
        Me.pnlPlan.Controls.Add(Me.txtNotes)
        Me.pnlPlan.Controls.Add(Me.TextBox9)
        Me.pnlPlan.Controls.Add(Me.cboOnsiteConsultant)
        Me.pnlPlan.Controls.Add(Me.CheckBox3)
        Me.pnlPlan.Controls.Add(Me.CheckBox2)
        Me.pnlPlan.Controls.Add(Me.CheckBox1)
        Me.pnlPlan.Controls.Add(Me.Label43)
        Me.pnlPlan.Controls.Add(Me.txtPlanningRecd)
        Me.pnlPlan.Controls.Add(Me.Label42)
        Me.pnlPlan.Controls.Add(Me.txtClassConfirmed)
        Me.pnlPlan.Controls.Add(Me.Label41)
        Me.pnlPlan.Controls.Add(Me.Label1)
        Me.pnlPlan.Controls.Add(Me.txtClassGroup)
        Me.pnlPlan.Controls.Add(Me.Label8)
        Me.pnlPlan.Controls.Add(Me.txtPlanningDeadline)
        Me.pnlPlan.Location = New System.Drawing.Point(432, 72)
        Me.pnlPlan.Name = "pnlPlan"
        Me.pnlPlan.Size = New System.Drawing.Size(488, 280)
        Me.pnlPlan.TabIndex = 2
        '
        'btnPlanningGrant
        '
        Me.btnPlanningGrant.Location = New System.Drawing.Point(316, 247)
        Me.btnPlanningGrant.Name = "btnPlanningGrant"
        Me.btnPlanningGrant.Size = New System.Drawing.Size(151, 22)
        Me.btnPlanningGrant.TabIndex = 262
        Me.btnPlanningGrant.Tag = "Planning"
        Me.btnPlanningGrant.Text = "Go to Planning Grant"
        Me.btnPlanningGrant.UseVisualStyleBackColor = True
        '
        'lblStaff
        '
        Me.lblStaff.BackColor = System.Drawing.Color.Transparent
        Me.lblStaff.Location = New System.Drawing.Point(7, 101)
        Me.lblStaff.Name = "lblStaff"
        Me.lblStaff.Size = New System.Drawing.Size(90, 21)
        Me.lblStaff.TabIndex = 231
        Me.lblStaff.Text = "Onsite Consultant"
        Me.lblStaff.Visible = False
        '
        'Label18
        '
        Me.Label18.BackColor = System.Drawing.Color.Transparent
        Me.Label18.Location = New System.Drawing.Point(305, 13)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(80, 16)
        Me.Label18.TabIndex = 261
        Me.Label18.Text = "Notes"
        '
        'txtNotes
        '
        Me.txtNotes.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.dsMainGrantLTGI1, "MainGrantLTGI.Notes", True))
        Me.txtNotes.Location = New System.Drawing.Point(303, 32)
        Me.txtNotes.Multiline = True
        Me.txtNotes.Name = "txtNotes"
        Me.txtNotes.Size = New System.Drawing.Size(169, 207)
        Me.txtNotes.TabIndex = 8
        Me.txtNotes.Text = "Notes"
        '
        'TextBox9
        '
        Me.TextBox9.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.dsMainGrantLTGI1, "MainGrantLTGI.AgreementRecd", True))
        Me.TextBox9.Location = New System.Drawing.Point(145, 8)
        Me.TextBox9.Name = "TextBox9"
        Me.TextBox9.Size = New System.Drawing.Size(102, 20)
        Me.TextBox9.TabIndex = 0
        Me.TextBox9.Tag = ""
        Me.TextBox9.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ToolTip1.SetToolTip(Me.TextBox9, "Date")
        '
        'CheckBox3
        '
        Me.CheckBox3.DataBindings.Add(New System.Windows.Forms.Binding("Checked", Me.dsMainGrantLTGI1, "MainGrantLTGI.Attended3", True))
        Me.CheckBox3.Location = New System.Drawing.Point(99, 171)
        Me.CheckBox3.Name = "CheckBox3"
        Me.CheckBox3.Size = New System.Drawing.Size(142, 21)
        Me.CheckBox3.TabIndex = 5
        Me.CheckBox3.Text = "Attended Session 3"
        '
        'CheckBox2
        '
        Me.CheckBox2.DataBindings.Add(New System.Windows.Forms.Binding("Checked", Me.dsMainGrantLTGI1, "MainGrantLTGI.Attended2", True))
        Me.CheckBox2.Location = New System.Drawing.Point(99, 151)
        Me.CheckBox2.Name = "CheckBox2"
        Me.CheckBox2.Size = New System.Drawing.Size(144, 21)
        Me.CheckBox2.TabIndex = 4
        Me.CheckBox2.Text = "Attended Session 2"
        '
        'CheckBox1
        '
        Me.CheckBox1.DataBindings.Add(New System.Windows.Forms.Binding("Checked", Me.dsMainGrantLTGI1, "MainGrantLTGI.Attended1", True))
        Me.CheckBox1.Location = New System.Drawing.Point(99, 132)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(142, 21)
        Me.CheckBox1.TabIndex = 3
        Me.CheckBox1.Text = "Attended Session 1"
        '
        'Label43
        '
        Me.Label43.BackColor = System.Drawing.Color.Transparent
        Me.Label43.Location = New System.Drawing.Point(31, 245)
        Me.Label43.Name = "Label43"
        Me.Label43.Size = New System.Drawing.Size(175, 16)
        Me.Label43.TabIndex = 8
        Me.Label43.Text = "Planning Grant Draft Received"
        Me.Label43.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtPlanningRecd
        '
        Me.txtPlanningRecd.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.dsMainGrantLTGI1, "MainGrantLTGI.PlanAppRecd", True))
        Me.txtPlanningRecd.Location = New System.Drawing.Point(214, 245)
        Me.txtPlanningRecd.Name = "txtPlanningRecd"
        Me.txtPlanningRecd.Size = New System.Drawing.Size(72, 20)
        Me.txtPlanningRecd.TabIndex = 7
        Me.txtPlanningRecd.Tag = ""
        Me.txtPlanningRecd.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ToolTip1.SetToolTip(Me.txtPlanningRecd, "Filling in this date will generate a new grant.")
        Me.txtPlanningRecd.UseSystemPasswordChar = True
        '
        'Label42
        '
        Me.Label42.BackColor = System.Drawing.Color.Transparent
        Me.Label42.Location = New System.Drawing.Point(10, 65)
        Me.Label42.Margin = New System.Windows.Forms.Padding(0)
        Me.Label42.Name = "Label42"
        Me.Label42.Size = New System.Drawing.Size(129, 21)
        Me.Label42.TabIndex = 221
        Me.Label42.Text = "Date Class Confirmed"
        Me.Label42.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtClassConfirmed
        '
        Me.txtClassConfirmed.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.dsMainGrantLTGI1, "MainGrantLTGI.DateConfirmClass", True))
        Me.txtClassConfirmed.Location = New System.Drawing.Point(145, 65)
        Me.txtClassConfirmed.Name = "txtClassConfirmed"
        Me.txtClassConfirmed.Size = New System.Drawing.Size(103, 20)
        Me.txtClassConfirmed.TabIndex = 2
        Me.txtClassConfirmed.Tag = ""
        Me.txtClassConfirmed.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ToolTip1.SetToolTip(Me.txtClassConfirmed, "Date")
        '
        'Label41
        '
        Me.Label41.Location = New System.Drawing.Point(3, 5)
        Me.Label41.Name = "Label41"
        Me.Label41.Size = New System.Drawing.Size(140, 24)
        Me.Label41.TabIndex = 11
        Me.Label41.Text = "Date Agreement Received"
        Me.Label41.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'StatusBar1
        '
        Me.StatusBar1.Location = New System.Drawing.Point(0, 626)
        Me.StatusBar1.Name = "StatusBar1"
        Me.StatusBar1.Panels.AddRange(New System.Windows.Forms.StatusBarPanel() {Me.StatusBarPanel1, Me.StatusBarPanel2})
        Me.StatusBar1.ShowPanels = True
        Me.StatusBar1.Size = New System.Drawing.Size(990, 24)
        Me.StatusBar1.TabIndex = 231
        Me.StatusBar1.Text = "StatusBar1"
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
        Me.StatusBarPanel2.Text = "Use this window to change MGI App information."
        Me.StatusBarPanel2.Width = 773
        '
        'daspCaseNames
        '
        Me.daspCaseNames.SelectCommand = Me.SqlSelectCommand3
        Me.daspCaseNames.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "luCaseName", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("CaseID", "CaseID"), New System.Data.Common.DataColumnMapping("CaseName", "CaseName"), New System.Data.Common.DataColumnMapping("OrgNum", "OrgNum"), New System.Data.Common.DataColumnMapping("StatusName", "StatusName")})})
        '
        'SqlSelectCommand3
        '
        Me.SqlSelectCommand3.CommandText = "[luCaseNames]"
        Me.SqlSelectCommand3.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlSelectCommand3.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, False, CType(0, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing), New System.Data.SqlClient.SqlParameter("@OrgID", System.Data.SqlDbType.Int, 4)})
        '
        'daspContactNames
        '
        Me.daspContactNames.SelectCommand = Me.SqlSelectCommand
        Me.daspContactNames.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "luContactNames", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("ContactID", "ContactID"), New System.Data.Common.DataColumnMapping("ContactName", "ContactName"), New System.Data.Common.DataColumnMapping("ContactStaff", "ContactStaff"), New System.Data.Common.DataColumnMapping("ContactOrgCity", "ContactOrgCity"), New System.Data.Common.DataColumnMapping("OrgID", "OrgID"), New System.Data.Common.DataColumnMapping("Staff", "Staff"), New System.Data.Common.DataColumnMapping("FirstName", "FirstName"), New System.Data.Common.DataColumnMapping("Lastname", "Lastname"), New System.Data.Common.DataColumnMapping("Active", "Active"), New System.Data.Common.DataColumnMapping("PrimaryContact", "PrimaryContact")})})
        '
        'SqlSelectCommand
        '
        Me.SqlSelectCommand.CommandText = "dbo.luContactNames"
        Me.SqlSelectCommand.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlSelectCommand.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, False, CType(0, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing), New System.Data.SqlClient.SqlParameter("@IDFld", System.Data.SqlDbType.VarChar, 30, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, "Orgnum"), New System.Data.SqlClient.SqlParameter("@IDVal", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, "2300")})
        '
        'btnSaveExit
        '
        Me.btnSaveExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSaveExit.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.btnSaveExit.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSaveExit.Image = CType(resources.GetObject("btnSaveExit.Image"), System.Drawing.Image)
        Me.btnSaveExit.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSaveExit.Location = New System.Drawing.Point(48, 2)
        Me.btnSaveExit.Name = "btnSaveExit"
        Me.btnSaveExit.Size = New System.Drawing.Size(40, 35)
        Me.btnSaveExit.TabIndex = 418
        Me.btnSaveExit.Text = "Close"
        Me.btnSaveExit.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.btnSaveExit, "Saves any changes and Closes this window")
        Me.btnSaveExit.UseVisualStyleBackColor = False
        '
        'TextBox2
        '
        Me.TextBox2.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.dsMainGrantLTGI1, "MainGrantLTGI.DecisionMailed", True))
        Me.TextBox2.Location = New System.Drawing.Point(154, 79)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(72, 20)
        Me.TextBox2.TabIndex = 4
        Me.TextBox2.Text = "Date"
        Me.TextBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ToolTip1.SetToolTip(Me.TextBox2, "Date")
        '
        'TextBox3
        '
        Me.TextBox3.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.dsMainGrantLTGI1, "MainGrantLTGI.AgreementMailed", True))
        Me.TextBox3.Location = New System.Drawing.Point(154, 103)
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.Size = New System.Drawing.Size(72, 20)
        Me.TextBox3.TabIndex = 5
        Me.TextBox3.Text = "Date"
        Me.TextBox3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ToolTip1.SetToolTip(Me.TextBox3, "Date")
        '
        'MainMenu1
        '
        Me.MainMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem1, Me.MenuItem2, Me.MenuItem3, Me.MenuItem4})
        '
        'MenuItem1
        '
        Me.MenuItem1.Index = 0
        Me.MenuItem1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.miClose})
        Me.MenuItem1.Text = "File"
        '
        'miClose
        '
        Me.miClose.Index = 0
        Me.miClose.Text = "Close Window"
        '
        'MenuItem2
        '
        Me.MenuItem2.Index = 1
        Me.MenuItem2.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.miSave, Me.miCancel, Me.miDelete})
        Me.MenuItem2.Text = "Edit"
        '
        'miSave
        '
        Me.miSave.Index = 0
        Me.miSave.Text = "Save Changes"
        Me.miSave.Enabled = False
        '
        'miCancel
        '
        Me.miCancel.Index = 1
        Me.miCancel.Text = "Cancel Changes"
        '
        'miDelete
        '
        Me.miDelete.Index = 2
        Me.miDelete.Text = "Delete Grant"
        '
        'MenuItem3
        '
        Me.MenuItem3.Index = 2
        Me.MenuItem3.Text = "Reports"
        '
        'MenuItem4
        '
        Me.MenuItem4.Index = 3
        Me.MenuItem4.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.miGotoCase, Me.miGotoResource})
        Me.MenuItem4.Text = "Goto"
        Me.MenuItem4.Visible = False
        '
        'miGotoCase
        '
        Me.miGotoCase.Index = 0
        Me.miGotoCase.Text = "Case"
        '
        'miGotoResource
        '
        Me.miGotoResource.Index = 1
        Me.miGotoResource.Text = "Resource"
        '
        'daspMainLTGI
        '
        Me.daspMainLTGI.SelectCommand = Me.SqlSelectCommand1
        Me.daspMainLTGI.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "MainGrantLTGI", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("GIID", "GIID"), New System.Data.Common.DataColumnMapping("OrgNum", "OrgNum"), New System.Data.Common.DataColumnMapping("CaseNum", "CaseNum"), New System.Data.Common.DataColumnMapping("AppRecdDt", "AppRecDt"), New System.Data.Common.DataColumnMapping("Remarks", "Remarks"), New System.Data.Common.DataColumnMapping("InServiceArea", "InServiceArea"), New System.Data.Common.DataColumnMapping("InclSignatures", "InclSignatures"), New System.Data.Common.DataColumnMapping("InclAbility501c3", "InclAbility501c3"), New System.Data.Common.DataColumnMapping("InclCongProfile", "InclCongProfile"), New System.Data.Common.DataColumnMapping("InclPostmark", "InclPostmark"), New System.Data.Common.DataColumnMapping("MailedNoticeReceipt", "MailedNoticeReceipt"), New System.Data.Common.DataColumnMapping("Determination", "Determination"), New System.Data.Common.DataColumnMapping("DeterminationDate", "DeterminationDate"), New System.Data.Common.DataColumnMapping("CallNotifiedDate", "CallNotifiedDate"), New System.Data.Common.DataColumnMapping("CallNotifiedStaffNum", "CallNotifiedStaffNum"), New System.Data.Common.DataColumnMapping("DecisionMailed", "DecisionMailed"), New System.Data.Common.DataColumnMapping("AgreementMailed", "AgreementMailed"), New System.Data.Common.DataColumnMapping("AgreementRecd", "AgreementRecd"), New System.Data.Common.DataColumnMapping("ClassGroup", "ClassGroup"), New System.Data.Common.DataColumnMapping("DateConfirmClass", "DateConfirmClass"), New System.Data.Common.DataColumnMapping("OnSiteConsultantTxt", "OnSiteConsultantTxt"), New System.Data.Common.DataColumnMapping("Attended1", "Attended1"), New System.Data.Common.DataColumnMapping("Attended2", "Attended2"), New System.Data.Common.DataColumnMapping("Attended3", "Attended3"), New System.Data.Common.DataColumnMapping("Attended4", "Attended4"), New System.Data.Common.DataColumnMapping("Attended5", "Attended5"), New System.Data.Common.DataColumnMapping("PlanAppDeadline", "PlanAppDeadline"), New System.Data.Common.DataColumnMapping("PlanAppRecd", "PlanAppRecd"), New System.Data.Common.DataColumnMapping("ImplementAppDeadline", "ImplementAppDeadline"), New System.Data.Common.DataColumnMapping("ImplementAppRecd", "ImplementAppRecd"), New System.Data.Common.DataColumnMapping("ProjectDirectorNum", "ProjectDirectorNum"), New System.Data.Common.DataColumnMapping("ClergyLeaderNum", "ClergyLeaderNum"), New System.Data.Common.DataColumnMapping("TentativePlan", "TentativePlan"), New System.Data.Common.DataColumnMapping("AppStaffNum", "AppStaffNum"), New System.Data.Common.DataColumnMapping("OnSiteVisitOne", "OnSiteVisitOne"), New System.Data.Common.DataColumnMapping("OnSiteVisitTwo", "OnSiteVisitTwo"), New System.Data.Common.DataColumnMapping("OnSiteVisitThree", "OnSiteVisitThree"), New System.Data.Common.DataColumnMapping("OnSiteVisitFour", "OnSiteVisitFour"), New System.Data.Common.DataColumnMapping("ArchitectVisit", "ArchitectVisit"), New System.Data.Common.DataColumnMapping("ArchitectVisitWhoTxt", "ArchitectVisitWhoTxt"), New System.Data.Common.DataColumnMapping("Notes", "Notes"), New System.Data.Common.DataColumnMapping("PDPhone", "PDPhone"), New System.Data.Common.DataColumnMapping("PDFax", "PDFax"), New System.Data.Common.DataColumnMapping("CLPhone", "CLPhone"), New System.Data.Common.DataColumnMapping("CLFax", "CLFax"), New System.Data.Common.DataColumnMapping("CallNotifiedStaffTxt", "CallNotifiedStaffTxt"), New System.Data.Common.DataColumnMapping("ReviewStaffTxt", "ReviewStaffTxt"), New System.Data.Common.DataColumnMapping("Stamped", "Stamped")})})
        Me.daspMainLTGI.UpdateCommand = Me.SqlUpdateCommand1
        '
        'SqlSelectCommand1
        '
        Me.SqlSelectCommand1.CommandText = "dbo.MainGrantLTGI"
        Me.SqlSelectCommand1.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlSelectCommand1.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, False, CType(0, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing), New System.Data.SqlClient.SqlParameter("@ID", System.Data.SqlDbType.Int, 4)})
        '
        'SqlUpdateCommand1
        '
        Me.SqlUpdateCommand1.CommandText = "dbo.MainGrantLTGIUpdate"
        Me.SqlUpdateCommand1.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlUpdateCommand1.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, False, CType(0, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing), New System.Data.SqlClient.SqlParameter("@ID", System.Data.SqlDbType.Int, 4, "GIID"), New System.Data.SqlClient.SqlParameter("@OrgNum", System.Data.SqlDbType.Int, 4, "OrgNum"), New System.Data.SqlClient.SqlParameter("@CaseNum", System.Data.SqlDbType.Int, 4, "CaseNum"), New System.Data.SqlClient.SqlParameter("@AppRecdDt", System.Data.SqlDbType.Date, 4, "AppRecdDt"), New System.Data.SqlClient.SqlParameter("@Remarks", System.Data.SqlDbType.VarChar, 255, "Remarks"), New System.Data.SqlClient.SqlParameter("@InServiceArea", System.Data.SqlDbType.Bit, 1, "InServiceArea"), New System.Data.SqlClient.SqlParameter("@InclSignatures", System.Data.SqlDbType.Bit, 1, "InclSignatures"), New System.Data.SqlClient.SqlParameter("@InclAbility501c3", System.Data.SqlDbType.Bit, 1, "InclAbility501c3"), New System.Data.SqlClient.SqlParameter("@InclCongProfile", System.Data.SqlDbType.Bit, 1, "InclCongProfile"), New System.Data.SqlClient.SqlParameter("@InclPostmark", System.Data.SqlDbType.Bit, 1, "InclPostmark"), New System.Data.SqlClient.SqlParameter("@MailedNoticeReceipt", System.Data.SqlDbType.Date, 4, "MailedNoticeReceipt"), New System.Data.SqlClient.SqlParameter("@Determination", System.Data.SqlDbType.VarChar, 50, "Determination"), New System.Data.SqlClient.SqlParameter("@DeterminationDate", System.Data.SqlDbType.Date, 4, "DeterminationDate"), New System.Data.SqlClient.SqlParameter("@CallNotifiedDate", System.Data.SqlDbType.Date, 4, "CallNotifiedDate"), New System.Data.SqlClient.SqlParameter("@CallNotifiedStaffNum", System.Data.SqlDbType.Int, 4, "CallNotifiedStaffNum"), New System.Data.SqlClient.SqlParameter("@DecisionMailed", System.Data.SqlDbType.Date, 4, "DecisionMailed"), New System.Data.SqlClient.SqlParameter("@AgreementMailed", System.Data.SqlDbType.Date, 4, "AgreementMailed"), New System.Data.SqlClient.SqlParameter("@AgreementRecd", System.Data.SqlDbType.Date, 4, "AgreementRecd"), New System.Data.SqlClient.SqlParameter("@ClassGroup", System.Data.SqlDbType.VarChar, 75, "ClassGroup"), New System.Data.SqlClient.SqlParameter("@DateConfirmClass", System.Data.SqlDbType.Date, 4, "DateConfirmClass"), New System.Data.SqlClient.SqlParameter("@OnSiteConsultantTxt", System.Data.SqlDbType.VarChar, 50, "OnSiteConsultantTxt"), New System.Data.SqlClient.SqlParameter("@Attended1", System.Data.SqlDbType.Bit, 1, "Attended1"), New System.Data.SqlClient.SqlParameter("@Attended2", System.Data.SqlDbType.Bit, 1, "Attended2"), New System.Data.SqlClient.SqlParameter("@Attended3", System.Data.SqlDbType.Bit, 1, "Attended3"), New System.Data.SqlClient.SqlParameter("@Attended4", System.Data.SqlDbType.Bit, 1, "Attended4"), New System.Data.SqlClient.SqlParameter("@Attended5", System.Data.SqlDbType.Bit, 1, "Attended5"), New System.Data.SqlClient.SqlParameter("@PlanAppDeadline", System.Data.SqlDbType.Date, 4, "PlanAppDeadline"), New System.Data.SqlClient.SqlParameter("@PlanAppRecd", System.Data.SqlDbType.Date, 4, "PlanAppRecd"), New System.Data.SqlClient.SqlParameter("@ImplementAppDeadline", System.Data.SqlDbType.Date, 4, "ImplementAppDeadline"), New System.Data.SqlClient.SqlParameter("@ImplementAppRecd", System.Data.SqlDbType.Date, 4, "ImplementAppRecd"), New System.Data.SqlClient.SqlParameter("@ProjectDirectorNum", System.Data.SqlDbType.Int, 4, "ProjectDirectorNum"), New System.Data.SqlClient.SqlParameter("@ClergyLeaderNum", System.Data.SqlDbType.Int, 4, "ClergyLeaderNum"), New System.Data.SqlClient.SqlParameter("@TentativePlan", System.Data.SqlDbType.VarChar, 2147483647, "TentativePlan"), New System.Data.SqlClient.SqlParameter("@AppStaffNum", System.Data.SqlDbType.Int, 4, "AppStaffNum"), New System.Data.SqlClient.SqlParameter("@OnSiteVisitOne", System.Data.SqlDbType.Date, 4, "OnSiteVisitOne"), New System.Data.SqlClient.SqlParameter("@OnSiteVisitTwo", System.Data.SqlDbType.Date, 4, "OnSiteVisitTwo"), New System.Data.SqlClient.SqlParameter("@OnSiteVisitThree", System.Data.SqlDbType.Date, 4, "OnSiteVisitThree"), New System.Data.SqlClient.SqlParameter("@OnSiteVisitFour", System.Data.SqlDbType.Date, 4, "OnSiteVisitFour"), New System.Data.SqlClient.SqlParameter("@ArchitectVisit", System.Data.SqlDbType.Date, 4, "ArchitectVisit"), New System.Data.SqlClient.SqlParameter("@ArchitectVisitWhoTxt", System.Data.SqlDbType.VarChar, 50, "ArchitectVisitWhoTxt"), New System.Data.SqlClient.SqlParameter("@Notes", System.Data.SqlDbType.Text, 2147483647, "Notes"), New System.Data.SqlClient.SqlParameter("@PDPhone", System.Data.SqlDbType.VarChar, 50, "PDPhone"), New System.Data.SqlClient.SqlParameter("@PDFax", System.Data.SqlDbType.VarChar, 50, "PDFax"), New System.Data.SqlClient.SqlParameter("@CLPhone", System.Data.SqlDbType.VarChar, 50, "CLPhone"), New System.Data.SqlClient.SqlParameter("@CLFax", System.Data.SqlDbType.VarChar, 50, "CLFax"), New System.Data.SqlClient.SqlParameter("@CallNotifiedStaffTxt", System.Data.SqlDbType.VarChar, 50, "CallNotifiedStaffTxt"), New System.Data.SqlClient.SqlParameter("@ReviewStaffTxt", System.Data.SqlDbType.VarChar, 50, "ReviewStaffTxt"), New System.Data.SqlClient.SqlParameter("@Stamp", System.Data.SqlDbType.Timestamp, 8, "Stamped")})
        '
        'Label13
        '
        Me.Label13.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.SystemColors.Highlight
        Me.Label13.Location = New System.Drawing.Point(160, 56)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(136, 14)
        Me.Label13.TabIndex = 253
        Me.Label13.Text = "APPLICATION"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label36
        '
        Me.Label36.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label36.ForeColor = System.Drawing.SystemColors.Highlight
        Me.Label36.Location = New System.Drawing.Point(576, 56)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(184, 14)
        Me.Label36.TabIndex = 254
        Me.Label36.Text = "PHASE 1 - PLANNING"
        Me.Label36.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label37
        '
        Me.Label37.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label37.ForeColor = System.Drawing.SystemColors.Highlight
        Me.Label37.Location = New System.Drawing.Point(568, 368)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(255, 14)
        Me.Label37.TabIndex = 255
        Me.Label37.Text = "PHASE 2 - IMPLEMENTATION"
        Me.Label37.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pnlProcess
        '
        Me.pnlProcess.BackColor = System.Drawing.Color.FromArgb(CType(CType(250, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.pnlProcess.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pnlProcess.Controls.Add(Me.Label4)
        Me.pnlProcess.Controls.Add(Me.TextBox3)
        Me.pnlProcess.Controls.Add(Me.Label10)
        Me.pnlProcess.Controls.Add(Me.TextBox2)
        Me.pnlProcess.Controls.Add(Me.cboCase)
        Me.pnlProcess.Controls.Add(Me.cboCallNotified)
        Me.pnlProcess.Controls.Add(Me.Label7)
        Me.pnlProcess.Controls.Add(Me.CaseNum)
        Me.pnlProcess.Controls.Add(Me.fldGotoCase)
        Me.pnlProcess.Controls.Add(Me.Label15)
        Me.pnlProcess.Controls.Add(Me.dtDeterminationDate)
        Me.pnlProcess.Controls.Add(Me.cboDetermination)
        Me.pnlProcess.Controls.Add(Me.Label21)
        Me.pnlProcess.Controls.Add(Me.dtCallNotifiedDate)
        Me.pnlProcess.Location = New System.Drawing.Point(16, 384)
        Me.pnlProcess.Name = "pnlProcess"
        Me.pnlProcess.Size = New System.Drawing.Size(392, 184)
        Me.pnlProcess.TabIndex = 1
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.ForeColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Label4.Location = New System.Drawing.Point(169, 15)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(35, 13)
        Me.Label4.TabIndex = 228
        Me.Label4.Text = "Dates"
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Location = New System.Drawing.Point(18, 79)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(128, 16)
        Me.Label10.TabIndex = 227
        Me.Label10.Text = "Mailed Notification"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.Highlight
        Me.Label5.Location = New System.Drawing.Point(152, 368)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(160, 14)
        Me.Label5.TabIndex = 256
        Me.Label5.Text = "DETERMINATION"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnHelp
        '
        Me.btnHelp.Image = CType(resources.GetObject("btnHelp.Image"), System.Drawing.Image)
        Me.btnHelp.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnHelp.Location = New System.Drawing.Point(937, 6)
        Me.btnHelp.Name = "btnHelp"
        Me.btnHelp.Size = New System.Drawing.Size(25, 25)
        Me.btnHelp.TabIndex = 257
        Me.btnHelp.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnHelp.UseVisualStyleBackColor = True
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Location = New System.Drawing.Point(255, 12)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(45, 13)
        Me.Label22.TabIndex = 0
        Me.Label22.Text = "Label22"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.Highlight
        Me.Panel1.Controls.Add(Me.btnSaveExit)
        Me.Panel1.Controls.Add(Me.btnDelete)
        Me.Panel1.Location = New System.Drawing.Point(840, 5)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(91, 40)
        Me.Panel1.TabIndex = 419
        '
        'fldOrgID
        '
        Me.fldOrgID.AutoSize = True
        Me.fldOrgID.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.dsMainGrantLTGI1, "MainGrantLTGI.OrgNum", True))
        Me.fldOrgID.ForeColor = System.Drawing.SystemColors.ControlDark
        Me.fldOrgID.Location = New System.Drawing.Point(801, 591)
        Me.fldOrgID.Name = "fldOrgID"
        Me.fldOrgID.Size = New System.Drawing.Size(45, 13)
        Me.fldOrgID.TabIndex = 420
        Me.fldOrgID.Text = "Label23"
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.ForeColor = System.Drawing.SystemColors.ControlDark
        Me.Label24.Location = New System.Drawing.Point(750, 591)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(35, 13)
        Me.Label24.TabIndex = 421
        Me.Label24.Text = "OrgID"
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.ForeColor = System.Drawing.SystemColors.ControlDark
        Me.Label23.Location = New System.Drawing.Point(750, 613)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(41, 13)
        Me.Label23.TabIndex = 423
        Me.Label23.Text = "LTGI #"
        '
        'fldGINum
        '
        Me.fldGINum.AutoSize = True
        Me.fldGINum.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.dsMainGrantLTGI1, "MainGrantLTGI.GIID", True))
        Me.fldGINum.ForeColor = System.Drawing.SystemColors.ControlDark
        Me.fldGINum.Location = New System.Drawing.Point(801, 613)
        Me.fldGINum.Name = "fldGINum"
        Me.fldGINum.Size = New System.Drawing.Size(45, 13)
        Me.fldGINum.TabIndex = 422
        Me.fldGINum.Text = "Label23"
        '
        'frmMainGrantLTGI
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(984, 641)
        Me.Controls.Add(Me.Label23)
        Me.Controls.Add(Me.fldGINum)
        Me.Controls.Add(Me.Label24)
        Me.Controls.Add(Me.fldOrgID)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.pnlApplication)
        Me.Controls.Add(Me.pnlImplement)
        Me.Controls.Add(Me.pnlPlan)
        Me.Controls.Add(Me.pnlProcess)
        Me.Controls.Add(Me.btnHelp)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.Label36)
        Me.Controls.Add(Me.Label37)
        Me.Controls.Add(Me.Label5)
        Me.Menu = Me.MainMenu1
        Me.Name = "frmMainGrantLTGI"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Tag = "LTGI"
        Me.Text = "LIFE TOGETHER GRANT INITIATIVE APPLICATION"
        CType(Me.dsMainGrantLTGI1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlApplication.ResumeLayout(False)
        Me.pnlApplication.PerformLayout()
        CType(Me.DsluContactsLTGI, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsluCasesLTGI, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlImplement.ResumeLayout(False)
        Me.pnlImplement.PerformLayout()
        Me.pnlPlan.ResumeLayout(False)
        Me.pnlPlan.PerformLayout()
        CType(Me.StatusBarPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.StatusBarPanel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlProcess.ResumeLayout(False)
        Me.pnlProcess.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

#Region "Load"
    Private Sub frmMainGI_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ' Dim dv As DataView

        isLoaded = False
        bChanged = False
        Me.SuspendLayout()

        bDelete = False
        Me.daspMainLTGI.SelectCommand.Connection = sc
        Me.daspCaseNames.SelectCommand.Connection = sc
        Me.daspContactNames.SelectCommand.Connection = sc
        Me.daspMainLTGI.UpdateCommand.Connection = sc


SetDefaults:
        strDGM = "RESOURCES"
        strDGM2 = "GRANT" 'move to text box not grid

LoadCombos:
        modGlobalVar.LoadStaffCombo(Me.cboCallNotified, False, StaffComboChoices.Selectable)
        ' Me.DataView1.RowFilter = "Staff = 'Pastoral'"

        '        DV.RowFilter = "Staff = 'Pastoral'"
        '  mdGlobalVar.LoadStaffCombo(Me.cboStaffFollowup, False, "Selectable")

        cmgr = CType(Me.BindingContext(Me.dsMainGrantLTGI1, "MainGrantLTGI"), CurrencyManager)
        ' AddHandler cmgr.CurrentChanged, AddressOf CurrencyManager_ItemChanged





        'ENABLE DELETE DATE
        Dim ctl As Control
        For Each ctl In Me.pnlApplication.Controls
            If ctl.Name.Substring(0, 2) = "dt" Then
                '    FormatDateFld(ctl, "text", Me.dsMainGrantLTGI1, "MainGrantLTGI." & ctl.Name.Substring(2, Len(ctl.Name) - 2))
                AddHandler ctl.DataBindings(0).Parse, AddressOf modGlobalVar.DateParse
            Else
                ''   modGlobalVar.Msg(ctl.GetType.ToString, , ctl.Name) '= "System.Windows.Forms.TextBox"
                'If ctl.GetType.ToString = "System.Windows.Forms.ComboBox" And ctl.Name <> "cboDetermination" Then
                '    mdGlobalVar.LoadStaffCombo(ctl, False, "Selectable")
                'End If
            End If
        Next

        'pm = Me.BindingContext(dv)
        ' pm = CType(Me.BindingContext(ds, "Customers"), PropertyManager)
        '  pm = CType(bmbCase, PropertyManager)
        'cmd.Connection = sc
        ' cmd.Parameters.Add(New System.Data.SqlClient.SqlParameter("@IDVal", System.Data.SqlDbType.Int, 4))
        ' cmd.Parameters.Add(New System.Data.SqlClient.SqlParameter("@IDFld", System.Data.SqlDbType.VarChar, 30))
        'Try
        '    Me.daspMainGI.SelectCommand.Parameters("@ID").Value = 1
        '    Me.daspMainGI.Fill(Me.DsMainGI1)
        'Catch ex As Exception
        '    modGlobalVar.Msg(ex.Message)

        'End Try

        '   LoadOrgBasedCombos(iOrg)
        Me.ResumeLayout()
        ' fillsecondary()
        Forms.Add(Me)
        CloseButton.Disable(Me)

        isLoaded = True
        StatusBar1.Panels(0).Text = "Done"

    End Sub

    Public Sub LoadOrgBasedCombos(ByVal orgid As Integer)

        Dim strsql As New SqlCommand("SELECT ContactID, ContactStaff FROM vwgetContacts where orgid = " & orgid, sc)
        Dim dtCase As New DataTable("dtCase")
        If SCConnect() Then
            Dim dr As SqlDataReader = strsql.ExecuteReader()
            dtContact.Load(dr)

            'LOAD CASES
            strsql.CommandText = "[luCaseNames]"
            strsql.CommandType = CommandType.StoredProcedure
            strsql.Parameters.Add("OrgID", SqlDbType.Int)
            strsql.Parameters("OrgID").Value = orgid
            dr = strsql.ExecuteReader(CommandBehavior.CloseConnection)
            dtCase.Load(dr)
            dr.Close()
            dr = Nothing
        End If

DropDowns:
        Me.cboClergyLeader.DisplayMember = "ContactStaff"
        Me.cboClergyLeader.ValueMember = "ContactID"
        Me.cboClergyLeader.DataSource = dtContact.DefaultView

        Me.cboPD.DisplayMember = "ContactStaff"
        Me.cboPD.ValueMember = "ContactID"
        Me.cboPD.DataSource = dtContact

        '  Me.cboCase.DisplayMember = "CaseName"
        '  Me.cboCase.ValueMember = "CaseID"
        Me.cboCase.DataSource = dtCase.DefaultView

    End Sub



    ''TODO CONCURRENCY change these datasets to be form specific not application wide
    'Public Sub LoadOrgBasedCombos(ByVal ID As Integer)


    '    'LOAD CASES
    '    Me.daspCaseNames.SelectCommand.Parameters("@OrgID").Value = ID
    '    Me.DsluCasesLTGI.Clear()
    '    Try
    '        daspCaseNames.Fill(DsluCasesLTGI)
    '    Catch ex As Exception
    '        modGlobalVar.Msg(ex.Message, , "can't fill case combo")
    '    End Try

    '    'LOAD CONTACT NAMES
    '    Me.daspContactNames.SelectCommand.Parameters("@IDVal").Value = ID
    '    Me.DsluContactsLTGI.Clear()
    '    Try
    '        daspContactNames.Fill(Me.DsluContactsLTGI)
    '    Catch ex As Exception
    '        modGlobalVar.Msg(ex.Message, , "can't fill contact combo")
    '    End Try
    '    DV = New DataView(Me.DsluContactsLTGI.luContactNames)
    '    Me.cboClergyLeader.ValueMember = "ContactID"
    '    Me.cboClergyLeader.DisplayMember = "ContactStaff"
    '    Me.cboClergyLeader.DataSource = DV
    '    '  modGlobalVar.Msg("out of load orgbased combos", , ID.ToString)
    'End Sub


    Protected Sub miCloseForm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Me.Close()
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

#End Region 'Load

#Region "Update Main"

    Private Sub frm_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) _
        Handles MyBase.FormClosing

        'If CheckStaff(cboCaller.SelectedValue, "regular") Then
        'Else
        '    cboCaller.Focus()
        '    bCancelClose = True
        '    GoTo Finish
        'End If
        If bDelete Then
        Else
            CloseSave()
        End If

        If bCancelClose = False Then   'user OKs close form
            ClassOpenForms.frmMainGrantLTGI = Nothing 'reset global var
        Else
        End If
Finish:
        e.Cancel = bCancelClose
    End Sub

    Private Sub CloseSave()
        If bChanged Or CType(cmgr.Current, DataRowView).Row.HasVersion(DataRowVersion.Proposed) Then
            Select Case modGlobalVar.Msg("CONFIRM: Closing - Save Changes?", Me.Text & NextLine &
                                     "click Yes to save and close; No to discard changes, Cancel to remain open", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
                Case Is = DialogResult.Yes
                    UpdateDB("close")
                    bCancelClose = False
                Case Is = DialogResult.No 'discard changes, but close
                    bCancelClose = False
                Case Is = MsgBoxResult.Cancel
                    bCancelClose = True
            End Select
        Else    'data has not changed
            Select Case modGlobalVar.Msg("CONFIRM: Closing with No Changes", Me.Text & NextLine &
                                "Click OK to continue, Cancel to remain open", MessageBoxButtons.OKCancel, MessageBoxIcon.Question)
                Case MsgBoxResult.Cancel
                    bCancelClose = True
                Case Else
                    bCancelClose = False
            End Select
        End If
    End Sub

    Private Sub miSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles miSave.Click
        '  mainBSrce.EndEdit()
        UpdateDB("mi")
        bCancelClose = False

    End Sub

    Private Sub btnSaveExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnSaveExit.Click
        Dim bUpdated As Boolean = False
        MouseWait()
Update:
        ' bDelete = True??
        Try
            If bChanged Or CType(cmgr.Current, DataRowView).Row.HasVersion(DataRowVersion.Proposed) Then
                UpdateDB("CloseExit")
                miClose.PerformClick()
            Else
                miClose.PerformClick()
            End If
        Catch ex As Exception
            modGlobalVar.Msg("ERROR: Save/Close", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        MouseDefault()

        '--Other MGI forms have this--
        'If AnyChanges(ctlNeutral, cmgr, mainTbl, bChanged) = True Then
        '    UpdateDB("SaveExit")
        '    bUpdated = True
        'End If
        'Me.Close()
        'MouseDefault()

    End Sub


    Public Sub UpdateDB(ByVal How As String)

        Me.dsMainGrantLTGI1.EnforceConstraints = False
        'ERROR HANDLING FOR END EDIT LIKE VALIDATION
        Try
            cmgr.EndCurrentEdit()
        Catch ECONSTRAINT As ConstraintException
            modGlobalVar.Msg("CONSTRAINT ERROR", ECONSTRAINT.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ENULL As NoNullAllowedException
            modGlobalVar.Msg("NULL ERROR", ENULL.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch EDATA As DataException
            modGlobalVar.Msg("ADO.NET DATA ERROR", EDATA.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ESYSTEM As Exception
            modGlobalVar.Msg("ESYSTEM ERROR", ESYSTEM.Message & NextLine & ESYSTEM.GetType.ToString, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

BeginTransaction:
        Dim myTransaction As SqlClient.SqlTransaction
        If Not SCConnect() Then
            Exit Sub
        End If

        myTransaction = sc.BeginTransaction
        Me.daspMainLTGI.UpdateCommand.Transaction = myTransaction
        Try
            Me.daspMainLTGI.UpdateCommand.Parameters("@ID").Value = Me.GrantID.Text
            Me.daspMainLTGI.Update(dsMainGrantLTGI1, "MainGrantLTGI")
            myTransaction.Commit()
            bCancelClose = False
        Catch dbcex As DBConcurrencyException
            modGlobalVar.Msg("ERROR: Update Failed concurrency", "someone else has changed this LTGI", MessageBoxButtons.OK, MessageBoxIcon.Error)
            'TODO put code here to capture changes and/or override
            myTransaction.Rollback()
            bCancelClose = True
        Catch eUpdate As System.Exception
            modGlobalVar.Msg("ERROR: Update Failed eUpdate", eUpdate.Message & NextLine & eUpdate.GetType.ToString, MessageBoxButtons.OK, MessageBoxIcon.Error)
            bCancelClose = True
            myTransaction.Rollback()
        Finally
            sc.Close()
        End Try
        bChanged = False
    End Sub
#End Region

#Region "EditButtons"
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles miCancel.Click
        ', btnCancel.Click
        Me.StatusBarPanel1.Text = "Cancelling changes."
        Me.dsMainGrantLTGI1.EnforceConstraints = False
        cmgr.CancelCurrentEdit()
        Me.StatusBarPanel1.Text = "Done."
    End Sub


    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
           Handles miDelete.Click, btnDelete.Click
        Dim strOldv As String
        Dim ctl As Control = Me.txtTentative
        strOldv = ctl.Text

        If modGlobalVar.Msg("CONFIRM DELETE", strOldv & NextLine & " will be marked for deletion and the window closed.", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) = DialogResult.Yes Then
            ctl.Text = "DELETE: " & IsNull(ctl.Text, "")
            UpdateDB("delete")
            bDelete = True
            Me.btnSaveExit.PerformClick()
        End If

    End Sub


#End Region

#Region "General"
    'UNDO
    Protected Overrides Function ProcessDialogKey(ByVal keyData As Keys) As Boolean
        If keyData = System.Windows.Forms.Keys.Escape Then
            modPopup.UndoCtl(Me.ActiveControl)
            bChanged = True
            Return True  ' True means we've processed the escape key
        Else
            Return MyBase.ProcessDialogKey(keyData)
        End If
    End Function

    'RIGHT CLICK MENU
    Private Sub textbox_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) _
         Handles txtRemarks.MouseDown, txtTentative.MouseDown, txtNotes.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Right Then
            Dim pp As New ClassRtbContextMenu(sender)
            pp.Show(Me, PointToClient(Control.MousePosition))
        End If
    End Sub


#End Region

#Region "FIll Secondary"

#End Region

#Region "Open Forms"

    'OPEN MAIN ORG FORM - see module
    Private Sub OpenOrg(ByVal sender As System.Object, ByVal e As System.EventArgs) _
             ', btnGotoOrg.Click
        Dim i As Integer
        i = CType(Me.OrgNum.Text, Integer)
        modGlobalVar.OpenMainOrg(i, Me.fldGotoOrg.Text) ', ClassOpenForms.frmMainOrg.DsMainOrg1, ClassOpenForms.frmMainOrg.daMainOrg, ClassOpenForms.frmMainOrg.miSave, "MainOrg", ClassOpenForms.frmMainOrg.daMainOrg.SelectCommand.Parameters("@OrgID"))
    End Sub

    'GOTO Case
    Private Sub ComboBox_DblClick(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles fldGotoCase.DoubleClick, fldContact.DoubleClick 'cboContact.Click, cboCase.Click

        Me.StatusBarPanel1.Text = "Opening " & sender.tag & " Detail window"
        Select Case sender.name.ToString
            '            Case Is = "lblContact"
            '               OpenMainContact(cboContact.SelectedValue, cboContact.DisplayMember, fldGotoOrg.Text, Me.OrgNum.Text)
            '          'TODO CONSISTENCIZE sometimes firstname first, othertimes lastname first
            Case Is = "FldGotoCase"
                modGlobalVar.OpenMainCase(cboCase.SelectedValue, cboCase.DisplayMember, Me.fldGotoOrg.Text, Me.OrgNum.Text)
        End Select
        Me.StatusBarPanel1.Text = "Done"
    End Sub




#End Region


    Private Sub btnHelp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHelp.Click
        modGlobalVar.Msg("LTGI HELP", "TO ADD NEW LTGI APPLICATION" & NextLine & NextLine & "Go to Grant Search Window and click New button", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    'GENERATE NEW PLANNING GRANT
    Private Sub txtPlanningRecd_leave(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles txtPlanningRecd.Leave

        If Not isLoaded Or IsNull(txtPlanningRecd.Text, "") = String.Empty Then
            Exit Sub
        End If
        If CType(txtPlanningRecd.Text, Date) > CType("1/1/1911", Date) Then
            modPopup.NewGrant(fldOrgID.Text, "Life Together Planning Grant", True, "LTGI PLANNING GRANT", "LTGI", Me.fldGINum.Text, IsNull(Me.cboCase.SelectedValue, 0), Me.txtPlanningRecd.Text, IsNull(Me.cboPD.SelectedValue, 0), IsNull(Me.cboClergyLeader.SelectedValue, 0), usr)
        End If
    End Sub

    'GENERATE NEW IMPLEMENTATION GRANT
    Private Sub txtImplementationRecd_leave(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles txtImplementationRecd.Leave

        If Not isLoaded Or IsNull(txtImplementationRecd.Text, "") = String.Empty Then
            Exit Sub
        End If
        If CType(txtImplementationRecd.Text, Date) > CType("1/1/1911", Date) Then
            modPopup.NewGrant(fldOrgID.Text, "Life Together Implementation Grant", True, "LTGI IMPLEMENTATION GRANT", "LTGI", Me.fldGINum.Text, IsNull(Me.cboCase.SelectedValue, 0), Me.txtImplementationRecd.Text, IsNull(Me.cboPD.SelectedValue, 0), IsNull(Me.cboClergyLeader.SelectedValue, 0), usr)
        End If
    End Sub

    Private Sub btnGotoGrant_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnPlanningGrant.Click, btnImplementationGrant.Click
        Dim cmdCntID As New SqlCommand
        Dim i As String

        cmdCntID.Connection = sc
        If Not SCConnect() Then
            Exit Sub
        End If

        Dim str As String = "Life Together " + sender.tag + " Grant"
        cmdCntID.CommandText = "SELECT grantidtxt FROM tblGrant WHERE GINum = " & Me.fldGINum.Text & " and TypeofGrant = '" + str + "'"
        Try
            i = cmdCntID.ExecuteScalar()
        Catch ex As Exception
            modGlobalVar.Msg("ERROR: getting grant id", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        sc.Close()

        If i > "0" Then
            modGlobalVar.OpenMainGrant(i, "LTGI", Me.fldGotoOrg.Text, Me.fldOrgID.Text)
        Else
            modGlobalVar.Msg("ERROR: Grant not found", "Either the grant hasn't been started yet (fill in the LastDraftReceived date and then Tab or click somewhere else), or the grant does not include the LTGI number.  see " & DBAdmin.StaffName, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
        str = Nothing

    End Sub


End Class
