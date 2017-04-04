<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMailLabels
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        Forms.Remove(Me)
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim TreeNode1 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Central")
        Dim TreeNode2 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("NE")
        Dim TreeNode3 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("NW")
        Dim TreeNode4 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("SE")
        Dim TreeNode5 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("SW")
        Dim TreeNode6 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Not In Region")
        Dim TreeNode7 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("SATELLITE REGIONS", New System.Windows.Forms.TreeNode() {TreeNode1, TreeNode2, TreeNode3, TreeNode4, TreeNode5, TreeNode6})
        Dim TreeNode8 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("CENTRAL MAILING AREA")
        Dim TreeNode9 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("NORTHERN MAILING AREA")
        Dim TreeNode10 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("SOUTHERN MAILING AREA")
        Dim TreeNode11 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("MAILING AREAS", New System.Windows.Forms.TreeNode() {TreeNode8, TreeNode9, TreeNode10})
        Dim TreeNode12 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("ALL OF INDIANA")
        Dim TreeNode13 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Other States")
        Dim TreeNode14 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("International")
        Dim TreeNode15 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("OUTSIDE INDIANA", New System.Windows.Forms.TreeNode() {TreeNode13, TreeNode14})
        Dim TreeNode16 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Church")
        Dim TreeNode17 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Denomination")
        Dim TreeNode18 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Mosque")
        Dim TreeNode19 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Synagogue")
        Dim TreeNode20 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Temple")
        Dim TreeNode21 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Religious Organizations", New System.Windows.Forms.TreeNode() {TreeNode16, TreeNode17, TreeNode18, TreeNode19, TreeNode20})
        Dim TreeNode22 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Type of Organization", New System.Windows.Forms.TreeNode() {TreeNode21})
        Dim TreeNode23 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("PrimaryContact")
        Dim TreeNode24 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Pastoral Staff")
        Dim TreeNode25 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("General Staff")
        Dim TreeNode26 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Staff", New System.Windows.Forms.TreeNode() {TreeNode23, TreeNode24, TreeNode25})
        Dim TreeNode27 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Lay Leadership")
        Dim TreeNode28 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Member")
        Dim TreeNode29 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Laity (home address only)", New System.Windows.Forms.TreeNode() {TreeNode27, TreeNode28})
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMailLabels))
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.TextBox3 = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.trvArea = New System.Windows.Forms.TreeView()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.ComboBox2 = New System.Windows.Forms.ComboBox()
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.trvType = New System.Windows.Forms.TreeView()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.btnGo = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.txtJobTitle = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.trvPeople = New System.Windows.Forms.TreeView()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.btnHelp = New System.Windows.Forms.Button()
        Me.btnMap = New System.Windows.Forms.Button()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.rbAddress = New System.Windows.Forms.RadioButton()
        Me.rbEmail = New System.Windows.Forms.RadioButton()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.btnCreateList = New System.Windows.Forms.Button()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.chkShowMembers = New System.Windows.Forms.CheckBox()
        Me.btnAddMember = New System.Windows.Forms.Button()
        Me.btnNewList = New System.Windows.Forms.Button()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.cboPrintFlag = New System.Windows.Forms.ComboBox()
        Me.dgrdMember = New System.Windows.Forms.DataGrid()
        Me.DataGridTableStyle1 = New System.Windows.Forms.DataGridTableStyle()
        Me.DataGridTextBoxColumn7 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn1 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn2 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn3 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn4 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn5 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn8 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn9 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn6 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.pnlMailListDetail = New System.Windows.Forms.Panel()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.fldMailListID = New System.Windows.Forms.Label()
        Me.fldExpireDate = New System.Windows.Forms.TextBox()
        Me.fldCreateDate = New System.Windows.Forms.Label()
        Me.fldNumMembers = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.fldDescription = New System.Windows.Forms.TextBox()
        Me.fldCreatedBy = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        CType(Me.dgrdMember, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlMailListDetail.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(150, Byte), Integer), CType(CType(175, Byte), Integer), CType(CType(150, Byte), Integer))
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.TextBox3)
        Me.Panel1.Controls.Add(Me.Label9)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.trvArea)
        Me.Panel1.Location = New System.Drawing.Point(15, 9)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(269, 586)
        Me.Panel1.TabIndex = 0
        '
        'TextBox3
        '
        Me.TextBox3.Enabled = False
        Me.TextBox3.Location = New System.Drawing.Point(16, 531)
        Me.TextBox3.Multiline = True
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.ReadOnly = True
        Me.TextBox3.Size = New System.Drawing.Size(230, 40)
        Me.TextBox3.TabIndex = 435
        Me.TextBox3.Visible = False
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Comic Sans MS", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(23, 492)
        Me.Label9.MaximumSize = New System.Drawing.Size(160, 0)
        Me.Label9.MinimumSize = New System.Drawing.Size(0, 30)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(158, 30)
        Me.Label9.TabIndex = 434
        Me.Label9.Text = "OR have Zipcode in this list:  (separate by commas)"
        Me.Label9.Visible = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Comic Sans MS", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(66, 7)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(139, 19)
        Me.Label2.TabIndex = 427
        Me.Label2.Text = "Geographic Coverage"
        '
        'trvArea
        '
        Me.trvArea.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.trvArea.CheckBoxes = True
        Me.trvArea.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.trvArea.FullRowSelect = True
        Me.trvArea.Location = New System.Drawing.Point(9, 37)
        Me.trvArea.Name = "trvArea"
        TreeNode1.Name = "SatelliteCentral"
        TreeNode1.Tag = "Central"
        TreeNode1.Text = "Central"
        TreeNode1.ToolTipText = "Indianapolis area"
        TreeNode2.Name = "SatelliteNE"
        TreeNode2.Tag = "NE"
        TreeNode2.Text = "NE"
        TreeNode2.ToolTipText = "Fort Wayne area"
        TreeNode3.Name = "SatelliteNW"
        TreeNode3.Tag = "NW"
        TreeNode3.Text = "NW"
        TreeNode4.Name = "SatelliteSE"
        TreeNode4.Tag = "SE"
        TreeNode4.Text = "SE"
        TreeNode5.Name = "SatelliteSW"
        TreeNode5.Tag = "SW"
        TreeNode5.Text = "SW"
        TreeNode5.ToolTipText = "Evansville area"
        TreeNode6.Name = "SatelliteNotInRegion"
        TreeNode6.Tag = "Not In Region"
        TreeNode6.Text = "Not In Region"
        TreeNode6.ToolTipText = "in Indiana, but outside a Satellite region"
        TreeNode7.Name = "SatelliteRegions"
        TreeNode7.Tag = "@Region"
        TreeNode7.Text = "SATELLITE REGIONS"
        TreeNode8.Name = "Central"
        TreeNode8.Tag = "Central"
        TreeNode8.Text = "CENTRAL MAILING AREA"
        TreeNode9.Name = "Northern"
        TreeNode9.Tag = "Northern"
        TreeNode9.Text = "NORTHERN MAILING AREA"
        TreeNode10.Name = "Southern"
        TreeNode10.Tag = "Southern"
        TreeNode10.Text = "SOUTHERN MAILING AREA"
        TreeNode11.Name = "MailingAreas"
        TreeNode11.Tag = "@MailArea"
        TreeNode11.Text = "MAILING AREAS"
        TreeNode12.Name = "AllIndiana"
        TreeNode12.Tag = "@State"
        TreeNode12.Text = "ALL OF INDIANA"
        TreeNode13.Name = "OtherStates"
        TreeNode13.Tag = "@State"
        TreeNode13.Text = "Other States"
        TreeNode14.Name = "International"
        TreeNode14.Tag = "@Country"
        TreeNode14.Text = "International"
        TreeNode15.Name = "OutsideIndiana"
        TreeNode15.Tag = ""
        TreeNode15.Text = "OUTSIDE INDIANA"
        Me.trvArea.Nodes.AddRange(New System.Windows.Forms.TreeNode() {TreeNode7, TreeNode11, TreeNode12, TreeNode15})
        Me.trvArea.ShowNodeToolTips = True
        Me.trvArea.Size = New System.Drawing.Size(247, 447)
        Me.trvArea.TabIndex = 0
        Me.ToolTip1.SetToolTip(Me.trvArea, "Number of active organizations shown in brackets.")
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Tan
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Controls.Add(Me.ComboBox2)
        Me.Panel2.Controls.Add(Me.ComboBox1)
        Me.Panel2.Controls.Add(Me.TextBox2)
        Me.Panel2.Controls.Add(Me.Label8)
        Me.Panel2.Controls.Add(Me.TextBox1)
        Me.Panel2.Controls.Add(Me.Label7)
        Me.Panel2.Controls.Add(Me.trvType)
        Me.Panel2.Controls.Add(Me.Label3)
        Me.Panel2.Location = New System.Drawing.Point(299, 9)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(240, 586)
        Me.Panel2.TabIndex = 2
        '
        'ComboBox2
        '
        Me.ComboBox2.Enabled = False
        Me.ComboBox2.FormattingEnabled = True
        Me.ComboBox2.Items.AddRange(New Object() {"And", "Or"})
        Me.ComboBox2.Location = New System.Drawing.Point(54, 490)
        Me.ComboBox2.Name = "ComboBox2"
        Me.ComboBox2.Size = New System.Drawing.Size(104, 21)
        Me.ComboBox2.TabIndex = 437
        Me.ComboBox2.Visible = False
        '
        'ComboBox1
        '
        Me.ComboBox1.Enabled = False
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Items.AddRange(New Object() {"= equal to", "< less than", "> greater than", "between (x and y)"})
        Me.ComboBox1.Location = New System.Drawing.Point(31, 541)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(83, 21)
        Me.ComboBox1.TabIndex = 436
        Me.ComboBox1.Visible = False
        '
        'TextBox2
        '
        Me.TextBox2.Enabled = False
        Me.TextBox2.Location = New System.Drawing.Point(128, 539)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.ReadOnly = True
        Me.TextBox2.Size = New System.Drawing.Size(68, 20)
        Me.TextBox2.TabIndex = 435
        Me.TextBox2.Visible = False
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Comic Sans MS", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(30, 523)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(118, 15)
        Me.Label8.TabIndex = 434
        Me.Label8.Text = "have Avg Attendance "
        Me.Label8.Visible = False
        '
        'TextBox1
        '
        Me.TextBox1.Enabled = False
        Me.TextBox1.Location = New System.Drawing.Point(21, 464)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.ReadOnly = True
        Me.TextBox1.Size = New System.Drawing.Size(163, 20)
        Me.TextBox1.TabIndex = 433
        Me.TextBox1.Visible = False
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Comic Sans MS", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(30, 448)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(148, 15)
        Me.Label7.TabIndex = 432
        Me.Label7.Text = "OR have Denomination like:"
        Me.Label7.Visible = False
        '
        'trvType
        '
        Me.trvType.CheckBoxes = True
        Me.trvType.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.trvType.Location = New System.Drawing.Point(14, 37)
        Me.trvType.Name = "trvType"
        TreeNode16.Name = "Church"
        TreeNode16.Tag = "Church"
        TreeNode16.Text = "Church"
        TreeNode17.Name = "Denomination"
        TreeNode17.Tag = "Denomination"
        TreeNode17.Text = "Denomination"
        TreeNode18.Name = "Mosque"
        TreeNode18.Tag = "Mosque"
        TreeNode18.Text = "Mosque"
        TreeNode19.Name = "Synagogue"
        TreeNode19.Tag = "Synagogue"
        TreeNode19.Text = "Synagogue"
        TreeNode20.Name = "Temple"
        TreeNode20.Tag = "Temple"
        TreeNode20.Text = "Temple"
        TreeNode21.Name = "Religious"
        TreeNode21.Text = "Religious Organizations"
        TreeNode21.ToolTipText = "Congregation, Denomination, Mosque, Synagogue, Temple"
        TreeNode22.ForeColor = System.Drawing.Color.DarkGray
        TreeNode22.Name = "OrgType"
        TreeNode22.NodeFont = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        TreeNode22.Tag = "@Type"
        TreeNode22.Text = "Type of Organization"
        Me.trvType.Nodes.AddRange(New System.Windows.Forms.TreeNode() {TreeNode22})
        Me.trvType.Size = New System.Drawing.Size(214, 351)
        Me.trvType.TabIndex = 0
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Comic Sans MS", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(40, 8)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(156, 19)
        Me.Label3.TabIndex = 428
        Me.Label3.Text = "Types of Organizations"
        '
        'btnGo
        '
        Me.btnGo.Font = New System.Drawing.Font("Comic Sans MS", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGo.Location = New System.Drawing.Point(20, 107)
        Me.btnGo.Name = "btnGo"
        Me.btnGo.Size = New System.Drawing.Size(64, 37)
        Me.btnGo.TabIndex = 3
        Me.btnGo.Text = "Go"
        Me.btnGo.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(190, 10)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(0, 13)
        Me.Label1.TabIndex = 4
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.LightSteelBlue
        Me.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel3.Controls.Add(Me.txtJobTitle)
        Me.Panel3.Controls.Add(Me.Label6)
        Me.Panel3.Controls.Add(Me.trvPeople)
        Me.Panel3.Controls.Add(Me.Label4)
        Me.Panel3.Location = New System.Drawing.Point(555, 9)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(207, 349)
        Me.Panel3.TabIndex = 5
        '
        'txtJobTitle
        '
        Me.txtJobTitle.Enabled = False
        Me.txtJobTitle.Location = New System.Drawing.Point(23, 311)
        Me.txtJobTitle.Name = "txtJobTitle"
        Me.txtJobTitle.ReadOnly = True
        Me.txtJobTitle.Size = New System.Drawing.Size(163, 20)
        Me.txtJobTitle.TabIndex = 431
        Me.txtJobTitle.Visible = False
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Comic Sans MS", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(20, 283)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(155, 15)
        Me.Label6.TabIndex = 430
        Me.Label6.Text = "AND/OR have Job Title like:"
        Me.Label6.Visible = False
        '
        'trvPeople
        '
        Me.trvPeople.CheckBoxes = True
        Me.trvPeople.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.trvPeople.Location = New System.Drawing.Point(8, 37)
        Me.trvPeople.Name = "trvPeople"
        TreeNode23.Name = "PrimaryContact"
        TreeNode23.Tag = "@Primary"
        TreeNode23.Text = "PrimaryContact"
        TreeNode23.ToolTipText = "includes Organizations with unknown Primary contact"
        TreeNode24.Name = "PastoralStaff"
        TreeNode24.Tag = "@Pastoral"
        TreeNode24.Text = "Pastoral Staff"
        TreeNode25.Name = "GeneralStaff"
        TreeNode25.Tag = "@General"
        TreeNode25.Text = "General Staff"
        TreeNode25.ToolTipText = "from Secretary to Custodian to Treasurer"
        TreeNode26.ForeColor = System.Drawing.Color.DarkGray
        TreeNode26.Name = "Staff"
        TreeNode26.Text = "Staff"
        TreeNode26.ToolTipText = "Will go to organization address unless home address is listed."
        TreeNode27.Name = "LayLeadership"
        TreeNode27.Tag = "@LayLeader"
        TreeNode27.Text = "Lay Leadership"
        TreeNode27.ToolTipText = "have Job Title but are not Staff"
        TreeNode28.Name = "Member"
        TreeNode28.Tag = "@Member"
        TreeNode28.Text = "Member"
        TreeNode29.Name = "Laity"
        TreeNode29.Text = "Laity (home address only)"
        Me.trvPeople.Nodes.AddRange(New System.Windows.Forms.TreeNode() {TreeNode26, TreeNode29})
        Me.trvPeople.Size = New System.Drawing.Size(188, 220)
        Me.trvPeople.TabIndex = 0
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Comic Sans MS", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(73, 7)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(63, 19)
        Me.Label4.TabIndex = 429
        Me.Label4.Text = "Contacts"
        '
        'btnHelp
        '
        Me.btnHelp.BackColor = System.Drawing.SystemColors.Control
        Me.btnHelp.Font = New System.Drawing.Font("Microsoft Sans Serif", 22.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnHelp.ForeColor = System.Drawing.SystemColors.Control
        Me.btnHelp.Image = Global.InfoCtr.My.Resources.Resources.btnHelp
        Me.btnHelp.Location = New System.Drawing.Point(40, 340)
        Me.btnHelp.Name = "btnHelp"
        Me.btnHelp.Size = New System.Drawing.Size(25, 25)
        Me.btnHelp.TabIndex = 426
        Me.btnHelp.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnHelp.UseVisualStyleBackColor = False
        '
        'btnMap
        '
        Me.btnMap.Location = New System.Drawing.Point(10, 286)
        Me.btnMap.Name = "btnMap"
        Me.btnMap.Size = New System.Drawing.Size(76, 36)
        Me.btnMap.TabIndex = 425
        Me.btnMap.Text = "Indiana County Map"
        Me.btnMap.UseVisualStyleBackColor = True
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.SystemColors.ControlLight
        Me.Panel4.Controls.Add(Me.GroupBox1)
        Me.Panel4.Controls.Add(Me.btnMap)
        Me.Panel4.Controls.Add(Me.btnGo)
        Me.Panel4.Controls.Add(Me.btnHelp)
        Me.Panel4.Location = New System.Drawing.Point(11, 35)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(102, 469)
        Me.Panel4.TabIndex = 430
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.rbAddress)
        Me.GroupBox1.Controls.Add(Me.rbEmail)
        Me.GroupBox1.Location = New System.Drawing.Point(3, 8)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(96, 84)
        Me.GroupBox1.TabIndex = 4
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Get"
        Me.ToolTip1.SetToolTip(Me.GroupBox1, "'Addresses' also includes emails, but only for organizations with an address.  'E" & _
        "mails' returns fewer columns. ")
        '
        'rbAddress
        '
        Me.rbAddress.AutoSize = True
        Me.rbAddress.Checked = True
        Me.rbAddress.Location = New System.Drawing.Point(7, 20)
        Me.rbAddress.Name = "rbAddress"
        Me.rbAddress.Size = New System.Drawing.Size(74, 17)
        Me.rbAddress.TabIndex = 1
        Me.rbAddress.TabStop = True
        Me.rbAddress.Text = "Addresses"
        Me.ToolTip1.SetToolTip(Me.rbAddress, "opens data file to merge for example Word mailing labels.")
        Me.rbAddress.UseVisualStyleBackColor = True
        '
        'rbEmail
        '
        Me.rbEmail.AutoSize = True
        Me.rbEmail.Location = New System.Drawing.Point(7, 44)
        Me.rbEmail.Name = "rbEmail"
        Me.rbEmail.Size = New System.Drawing.Size(55, 17)
        Me.rbEmail.TabIndex = 0
        Me.rbEmail.Text = "Emails"
        Me.ToolTip1.SetToolTip(Me.rbEmail, "opens email grid to email multiple recipients.")
        Me.rbEmail.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.ForeColor = System.Drawing.SystemColors.MenuHighlight
        Me.Label5.Location = New System.Drawing.Point(62, 9)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(538, 13)
        Me.Label5.TabIndex = 426
        Me.Label5.Text = "Have at least one box checked from each upper panel, then click Go button.  Sprea" & _
    "dsheet will open with results."
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(130, 37)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(842, 630)
        Me.TabControl1.TabIndex = 432
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.btnCreateList)
        Me.TabPage1.Controls.Add(Me.Panel1)
        Me.TabPage1.Controls.Add(Me.Panel2)
        Me.TabPage1.Controls.Add(Me.Panel3)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(834, 604)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Tag = "Geography"
        Me.TabPage1.Text = "Region + Type + Relationship"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'btnCreateList
        '
        Me.btnCreateList.Location = New System.Drawing.Point(588, 375)
        Me.btnCreateList.Name = "btnCreateList"
        Me.btnCreateList.Size = New System.Drawing.Size(153, 47)
        Me.btnCreateList.TabIndex = 6
        Me.btnCreateList.Text = "Save as Mailing List"
        Me.btnCreateList.UseVisualStyleBackColor = True
        Me.btnCreateList.Visible = False
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.Label22)
        Me.TabPage2.Controls.Add(Me.Label21)
        Me.TabPage2.Controls.Add(Me.chkShowMembers)
        Me.TabPage2.Controls.Add(Me.btnAddMember)
        Me.TabPage2.Controls.Add(Me.btnNewList)
        Me.TabPage2.Controls.Add(Me.Label13)
        Me.TabPage2.Controls.Add(Me.Label11)
        Me.TabPage2.Controls.Add(Me.Label12)
        Me.TabPage2.Controls.Add(Me.cboPrintFlag)
        Me.TabPage2.Controls.Add(Me.dgrdMember)
        Me.TabPage2.Controls.Add(Me.Label10)
        Me.TabPage2.Controls.Add(Me.pnlMailListDetail)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(834, 604)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Tag = "SavedMailList"
        Me.TabPage2.Text = "Saved Mail Lists"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'chkShowMembers
        '
        Me.chkShowMembers.AutoSize = True
        Me.chkShowMembers.Checked = True
        Me.chkShowMembers.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkShowMembers.Font = New System.Drawing.Font("Comic Sans MS", 9.0!)
        Me.chkShowMembers.Location = New System.Drawing.Point(39, 542)
        Me.chkShowMembers.Name = "chkShowMembers"
        Me.chkShowMembers.Size = New System.Drawing.Size(296, 21)
        Me.chkShowMembers.TabIndex = 437
        Me.chkShowMembers.Text = "Show/Hide Members (uncheck for faster loading)"
        Me.chkShowMembers.UseVisualStyleBackColor = True
        '
        'btnAddMember
        '
        Me.btnAddMember.Font = New System.Drawing.Font("Comic Sans MS", 9.0!)
        Me.btnAddMember.Location = New System.Drawing.Point(202, 202)
        Me.btnAddMember.Name = "btnAddMember"
        Me.btnAddMember.Size = New System.Drawing.Size(122, 26)
        Me.btnAddMember.TabIndex = 436
        Me.btnAddMember.Text = "Add Members"
        Me.btnAddMember.UseVisualStyleBackColor = True
        '
        'btnNewList
        '
        Me.btnNewList.Font = New System.Drawing.Font("Comic Sans MS", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnNewList.Location = New System.Drawing.Point(691, 6)
        Me.btnNewList.Name = "btnNewList"
        Me.btnNewList.Size = New System.Drawing.Size(122, 26)
        Me.btnNewList.TabIndex = 435
        Me.btnNewList.Text = "Create New List"
        Me.btnNewList.UseVisualStyleBackColor = True
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Palatino Linotype", 9.75!)
        Me.Label13.ForeColor = System.Drawing.Color.Maroon
        Me.Label13.Location = New System.Drawing.Point(23, 15)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(130, 18)
        Me.Label13.TabIndex = 434
        Me.Label13.Text = "1. Select Mailing List"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Palatino Linotype", 9.75!)
        Me.Label11.Location = New System.Drawing.Point(268, 36)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(80, 18)
        Me.Label11.TabIndex = 427
        Me.Label11.Text = "Description:"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Palatino Linotype", 9.75!)
        Me.Label12.Location = New System.Drawing.Point(26, 213)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(67, 18)
        Me.Label12.TabIndex = 433
        Me.Label12.Text = "Members:"
        '
        'cboPrintFlag
        '
        Me.cboPrintFlag.DisplayMember = "MailListName"
        Me.cboPrintFlag.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboPrintFlag.DropDownWidth = 250
        Me.cboPrintFlag.FormattingEnabled = True
        Me.cboPrintFlag.Location = New System.Drawing.Point(59, 37)
        Me.cboPrintFlag.Name = "cboPrintFlag"
        Me.cboPrintFlag.Size = New System.Drawing.Size(168, 21)
        Me.cboPrintFlag.TabIndex = 429
        Me.cboPrintFlag.ValueMember = "MailListID"
        '
        'dgrdMember
        '
        Me.dgrdMember.DataMember = ""
        Me.dgrdMember.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.dgrdMember.Location = New System.Drawing.Point(26, 234)
        Me.dgrdMember.Name = "dgrdMember"
        Me.dgrdMember.ReadOnly = True
        Me.dgrdMember.Size = New System.Drawing.Size(793, 289)
        Me.dgrdMember.TabIndex = 438
        Me.dgrdMember.TableStyles.AddRange(New System.Windows.Forms.DataGridTableStyle() {Me.DataGridTableStyle1})
        '
        'DataGridTableStyle1
        '
        Me.DataGridTableStyle1.DataGrid = Me.dgrdMember
        Me.DataGridTableStyle1.GridColumnStyles.AddRange(New System.Windows.Forms.DataGridColumnStyle() {Me.DataGridTextBoxColumn7, Me.DataGridTextBoxColumn1, Me.DataGridTextBoxColumn2, Me.DataGridTextBoxColumn3, Me.DataGridTextBoxColumn4, Me.DataGridTextBoxColumn5, Me.DataGridTextBoxColumn9, Me.DataGridTextBoxColumn8, Me.DataGridTextBoxColumn6})
        Me.DataGridTableStyle1.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.DataGridTableStyle1.MappingName = "tMailMembers"
        '
        'DataGridTextBoxColumn7
        '
        Me.DataGridTextBoxColumn7.Format = ""
        Me.DataGridTextBoxColumn7.FormatInfo = Nothing
        Me.DataGridTextBoxColumn7.HeaderText = "MailListNum"
        Me.DataGridTextBoxColumn7.MappingName = "MailListNum"
        Me.DataGridTextBoxColumn7.Width = 0
        '
        'DataGridTextBoxColumn1
        '
        Me.DataGridTextBoxColumn1.Format = ""
        Me.DataGridTextBoxColumn1.FormatInfo = Nothing
        Me.DataGridTextBoxColumn1.HeaderText = "ContactNum"
        Me.DataGridTextBoxColumn1.MappingName = "ContactNum"
        Me.DataGridTextBoxColumn1.Width = 0
        '
        'DataGridTextBoxColumn2
        '
        Me.DataGridTextBoxColumn2.Format = ""
        Me.DataGridTextBoxColumn2.FormatInfo = Nothing
        Me.DataGridTextBoxColumn2.HeaderText = "OrgName"
        Me.DataGridTextBoxColumn2.MappingName = "OrgName"
        Me.DataGridTextBoxColumn2.Width = 200
        '
        'DataGridTextBoxColumn3
        '
        Me.DataGridTextBoxColumn3.Format = ""
        Me.DataGridTextBoxColumn3.FormatInfo = Nothing
        Me.DataGridTextBoxColumn3.HeaderText = "Last Name"
        Me.DataGridTextBoxColumn3.MappingName = "LastName"
        Me.DataGridTextBoxColumn3.Width = 75
        '
        'DataGridTextBoxColumn4
        '
        Me.DataGridTextBoxColumn4.Format = ""
        Me.DataGridTextBoxColumn4.FormatInfo = Nothing
        Me.DataGridTextBoxColumn4.HeaderText = "First Name"
        Me.DataGridTextBoxColumn4.MappingName = "FirstName"
        Me.DataGridTextBoxColumn4.Width = 75
        '
        'DataGridTextBoxColumn5
        '
        Me.DataGridTextBoxColumn5.Format = ""
        Me.DataGridTextBoxColumn5.FormatInfo = Nothing
        Me.DataGridTextBoxColumn5.HeaderText = "Job Title"
        Me.DataGridTextBoxColumn5.MappingName = "JobTitle"
        '
        'DataGridTextBoxColumn8
        '
        Me.DataGridTextBoxColumn8.Format = ""
        Me.DataGridTextBoxColumn8.FormatInfo = Nothing
        Me.DataGridTextBoxColumn8.HeaderText = "OKEmail"
        Me.DataGridTextBoxColumn8.MappingName = "OKEmail"
        Me.DataGridTextBoxColumn8.Width = 55
        '
        'DataGridTextBoxColumn9
        '
        Me.DataGridTextBoxColumn9.Format = ""
        Me.DataGridTextBoxColumn9.FormatInfo = Nothing
        Me.DataGridTextBoxColumn9.HeaderText = "OKPostal"
        Me.DataGridTextBoxColumn9.MappingName = "OKPostal"
        Me.DataGridTextBoxColumn9.Width = 55
        '
        'DataGridTextBoxColumn6
        '
        Me.DataGridTextBoxColumn6.Format = ""
        Me.DataGridTextBoxColumn6.FormatInfo = Nothing
        Me.DataGridTextBoxColumn6.HeaderText = "Email"
        Me.DataGridTextBoxColumn6.MappingName = "Email"
        Me.DataGridTextBoxColumn6.Width = 150
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Comic Sans MS", 9.0!)
        Me.Label10.ForeColor = System.Drawing.Color.Maroon
        Me.Label10.Location = New System.Drawing.Point(114, 577)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(480, 17)
        Me.Label10.TabIndex = 428
        Me.Label10.Text = "NOTE: Clicking Go from this tab ignores selections on Region/Type/Relationship ta" & _
    "b." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        '
        'pnlMailListDetail
        '
        Me.pnlMailListDetail.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.pnlMailListDetail.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pnlMailListDetail.Controls.Add(Me.Label20)
        Me.pnlMailListDetail.Controls.Add(Me.fldMailListID)
        Me.pnlMailListDetail.Controls.Add(Me.fldExpireDate)
        Me.pnlMailListDetail.Controls.Add(Me.fldCreateDate)
        Me.pnlMailListDetail.Controls.Add(Me.fldNumMembers)
        Me.pnlMailListDetail.Controls.Add(Me.Label18)
        Me.pnlMailListDetail.Controls.Add(Me.fldDescription)
        Me.pnlMailListDetail.Controls.Add(Me.fldCreatedBy)
        Me.pnlMailListDetail.Controls.Add(Me.Label14)
        Me.pnlMailListDetail.Controls.Add(Me.Label19)
        Me.pnlMailListDetail.Controls.Add(Me.Label17)
        Me.pnlMailListDetail.Controls.Add(Me.Label16)
        Me.pnlMailListDetail.Controls.Add(Me.Label15)
        Me.pnlMailListDetail.Location = New System.Drawing.Point(377, 36)
        Me.pnlMailListDetail.Name = "pnlMailListDetail"
        Me.pnlMailListDetail.Size = New System.Drawing.Size(436, 161)
        Me.pnlMailListDetail.TabIndex = 431
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(70, 137)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(249, 13)
        Me.Label20.TabIndex = 18
        Me.Label20.Text = "*changes are saved as soon as you leave this field."
        '
        'fldMailListID
        '
        Me.fldMailListID.AutoSize = True
        Me.fldMailListID.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.fldMailListID.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fldMailListID.Location = New System.Drawing.Point(285, 71)
        Me.fldMailListID.MinimumSize = New System.Drawing.Size(100, 0)
        Me.fldMailListID.Name = "fldMailListID"
        Me.fldMailListID.Size = New System.Drawing.Size(100, 17)
        Me.fldMailListID.TabIndex = 17
        '
        'fldExpireDate
        '
        Me.fldExpireDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fldExpireDate.Location = New System.Drawing.Point(285, 42)
        Me.fldExpireDate.MinimumSize = New System.Drawing.Size(100, 4)
        Me.fldExpireDate.Name = "fldExpireDate"
        Me.fldExpireDate.Size = New System.Drawing.Size(100, 21)
        Me.fldExpireDate.TabIndex = 16
        '
        'fldCreateDate
        '
        Me.fldCreateDate.AutoSize = True
        Me.fldCreateDate.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.fldCreateDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fldCreateDate.Location = New System.Drawing.Point(83, 46)
        Me.fldCreateDate.MinimumSize = New System.Drawing.Size(100, 0)
        Me.fldCreateDate.Name = "fldCreateDate"
        Me.fldCreateDate.Size = New System.Drawing.Size(100, 17)
        Me.fldCreateDate.TabIndex = 15
        '
        'fldNumMembers
        '
        Me.fldNumMembers.AutoSize = True
        Me.fldNumMembers.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.fldNumMembers.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fldNumMembers.Location = New System.Drawing.Point(285, 13)
        Me.fldNumMembers.MinimumSize = New System.Drawing.Size(100, 0)
        Me.fldNumMembers.Name = "fldNumMembers"
        Me.fldNumMembers.Size = New System.Drawing.Size(100, 17)
        Me.fldNumMembers.TabIndex = 14
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Font = New System.Drawing.Font("Comic Sans MS", 8.0!)
        Me.Label18.Location = New System.Drawing.Point(216, 72)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(69, 15)
        Me.Label18.TabIndex = 4
        Me.Label18.Text = "Mail list ID:"
        '
        'fldDescription
        '
        Me.fldDescription.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fldDescription.Location = New System.Drawing.Point(15, 92)
        Me.fldDescription.MinimumSize = New System.Drawing.Size(370, 30)
        Me.fldDescription.Multiline = True
        Me.fldDescription.Name = "fldDescription"
        Me.fldDescription.Size = New System.Drawing.Size(414, 40)
        Me.fldDescription.TabIndex = 11
        '
        'fldCreatedBy
        '
        Me.fldCreatedBy.AutoSize = True
        Me.fldCreatedBy.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.fldCreatedBy.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fldCreatedBy.Location = New System.Drawing.Point(72, 13)
        Me.fldCreatedBy.MinimumSize = New System.Drawing.Size(110, 0)
        Me.fldCreatedBy.Name = "fldCreatedBy"
        Me.fldCreatedBy.Size = New System.Drawing.Size(110, 17)
        Me.fldCreatedBy.TabIndex = 7
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Comic Sans MS", 8.0!)
        Me.Label14.Location = New System.Drawing.Point(216, 14)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(68, 15)
        Me.Label14.TabIndex = 6
        Me.Label14.Text = "# Members:"
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Font = New System.Drawing.Font("Comic Sans MS", 8.0!)
        Me.Label19.Location = New System.Drawing.Point(197, 43)
        Me.Label19.Margin = New System.Windows.Forms.Padding(0)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(96, 15)
        Me.Label19.TabIndex = 5
        Me.Label19.Text = "Expiration date*:"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Font = New System.Drawing.Font("Comic Sans MS", 8.0!)
        Me.Label17.Location = New System.Drawing.Point(7, 46)
        Me.Label17.Margin = New System.Windows.Forms.Padding(0)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(77, 15)
        Me.Label17.TabIndex = 3
        Me.Label17.Text = "Date Created:"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Comic Sans MS", 8.0!)
        Me.Label16.Location = New System.Drawing.Point(7, 72)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(76, 15)
        Me.Label16.TabIndex = 2
        Me.Label16.Text = "Description*:"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Comic Sans MS", 8.0!)
        Me.Label15.Location = New System.Drawing.Point(7, 14)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(65, 15)
        Me.Label15.TabIndex = 1
        Me.Label15.Text = "Created by:"
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Font = New System.Drawing.Font("Palatino Linotype", 9.75!)
        Me.Label21.ForeColor = System.Drawing.Color.Maroon
        Me.Label21.Location = New System.Drawing.Point(26, 71)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(190, 36)
        Me.Label21.TabIndex = 439
        Me.Label21.Text = "2. <-- Select Address or Emails." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "3. <-- Click Go."
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.ForeColor = System.Drawing.Color.Maroon
        Me.Label22.Location = New System.Drawing.Point(374, 218)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(444, 13)
        Me.Label22.TabIndex = 440
        Me.Label22.Text = "Note: NO in the OK Email or OK Postal column means they have opted out of that ma" & _
    "iling list."
        '
        'frmMailLabels
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(984, 662)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Label1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmMailLabels"
        Me.Text = "Mailing Label Data"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.Panel4.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        CType(Me.dgrdMember, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlMailListDetail.ResumeLayout(False)
        Me.pnlMailListDetail.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents trvArea As System.Windows.Forms.TreeView
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents btnGo As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents trvType As System.Windows.Forms.TreeView
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents trvPeople As System.Windows.Forms.TreeView
    Friend WithEvents btnHelp As System.Windows.Forms.Button
    Friend WithEvents btnMap As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents TextBox2 As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtJobTitle As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents ComboBox1 As System.Windows.Forms.ComboBox
    Friend WithEvents TextBox3 As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents ComboBox2 As System.Windows.Forms.ComboBox
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents rbAddress As System.Windows.Forms.RadioButton
    Friend WithEvents rbEmail As System.Windows.Forms.RadioButton
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents btnCreateList As System.Windows.Forms.Button
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents btnAddMember As System.Windows.Forms.Button
    Friend WithEvents btnNewList As System.Windows.Forms.Button
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents cboPrintFlag As System.Windows.Forms.ComboBox
    Friend WithEvents dgrdMember As System.Windows.Forms.DataGrid
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents pnlMailListDetail As System.Windows.Forms.Panel
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents fldMailListID As System.Windows.Forms.Label
    Friend WithEvents fldExpireDate As System.Windows.Forms.TextBox
    Friend WithEvents fldCreateDate As System.Windows.Forms.Label
    Friend WithEvents fldNumMembers As System.Windows.Forms.Label
    Friend WithEvents fldDescription As System.Windows.Forms.TextBox
    Friend WithEvents fldCreatedBy As System.Windows.Forms.Label
    Friend WithEvents chkShowMembers As System.Windows.Forms.CheckBox
    Friend WithEvents DataGridTableStyle1 As System.Windows.Forms.DataGridTableStyle
    Friend WithEvents DataGridTextBoxColumn1 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn2 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn7 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn3 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn4 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn5 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn6 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn8 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn9 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
End Class
