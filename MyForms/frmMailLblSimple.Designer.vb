<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMailLblSimple
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMailLblSimple))
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
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.cboPrintFlag = New System.Windows.Forms.ComboBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.Panel5.SuspendLayout()
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
        Me.Panel1.Location = New System.Drawing.Point(121, 47)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(269, 593)
        Me.Panel1.TabIndex = 0
        '
        'TextBox3
        '
        Me.TextBox3.Enabled = False
        Me.TextBox3.Location = New System.Drawing.Point(26, 559)
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.ReadOnly = True
        Me.TextBox3.Size = New System.Drawing.Size(163, 20)
        Me.TextBox3.TabIndex = 435
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Comic Sans MS", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(23, 523)
        Me.Label9.MaximumSize = New System.Drawing.Size(160, 0)
        Me.Label9.MinimumSize = New System.Drawing.Size(0, 30)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(158, 30)
        Me.Label9.TabIndex = 434
        Me.Label9.Text = "OR have Zipcode in this list:  (separate by commas)"
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
        Me.trvArea.Size = New System.Drawing.Size(247, 474)
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
        Me.Panel2.Location = New System.Drawing.Point(411, 47)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(240, 593)
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
        '
        'TextBox2
        '
        Me.TextBox2.Enabled = False
        Me.TextBox2.Location = New System.Drawing.Point(128, 539)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.ReadOnly = True
        Me.TextBox2.Size = New System.Drawing.Size(68, 20)
        Me.TextBox2.TabIndex = 435
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
        '
        'TextBox1
        '
        Me.TextBox1.Enabled = False
        Me.TextBox1.Location = New System.Drawing.Point(21, 464)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.ReadOnly = True
        Me.TextBox1.Size = New System.Drawing.Size(163, 20)
        Me.TextBox1.TabIndex = 433
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
        Me.trvType.Size = New System.Drawing.Size(214, 380)
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
        Me.btnGo.Location = New System.Drawing.Point(15, 10)
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
        Me.Panel3.Location = New System.Drawing.Point(673, 47)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(207, 326)
        Me.Panel3.TabIndex = 5
        '
        'txtJobTitle
        '
        Me.txtJobTitle.Enabled = False
        Me.txtJobTitle.Location = New System.Drawing.Point(19, 283)
        Me.txtJobTitle.Name = "txtJobTitle"
        Me.txtJobTitle.ReadOnly = True
        Me.txtJobTitle.Size = New System.Drawing.Size(163, 20)
        Me.txtJobTitle.TabIndex = 431
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Comic Sans MS", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(20, 267)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(155, 15)
        Me.Label6.TabIndex = 430
        Me.Label6.Text = "AND/OR have Job Title like:"
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
        Me.trvPeople.Size = New System.Drawing.Size(188, 196)
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
        Me.btnHelp.Location = New System.Drawing.Point(855, 10)
        Me.btnHelp.Name = "btnHelp"
        Me.btnHelp.Size = New System.Drawing.Size(25, 25)
        Me.btnHelp.TabIndex = 426
        Me.btnHelp.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnHelp.UseVisualStyleBackColor = False
        '
        'btnMap
        '
        Me.btnMap.Location = New System.Drawing.Point(767, 5)
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
        Me.Panel4.Controls.Add(Me.btnGo)
        Me.Panel4.Location = New System.Drawing.Point(11, 35)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(102, 469)
        Me.Panel4.TabIndex = 430
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.rbAddress)
        Me.GroupBox1.Controls.Add(Me.rbEmail)
        Me.GroupBox1.Location = New System.Drawing.Point(3, 73)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(96, 95)
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
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(140, Byte), Integer))
        Me.Panel5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel5.Controls.Add(Me.cboPrintFlag)
        Me.Panel5.Controls.Add(Me.Label10)
        Me.Panel5.Controls.Add(Me.Label11)
        Me.Panel5.Location = New System.Drawing.Point(673, 395)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(207, 137)
        Me.Panel5.TabIndex = 431
        '
        'cboPrintFlag
        '
        Me.cboPrintFlag.DisplayMember = "PrintFlag"
        Me.cboPrintFlag.FormattingEnabled = True
        Me.cboPrintFlag.Location = New System.Drawing.Point(18, 96)
        Me.cboPrintFlag.Name = "cboPrintFlag"
        Me.cboPrintFlag.Size = New System.Drawing.Size(158, 21)
        Me.cboPrintFlag.TabIndex = 429
        Me.cboPrintFlag.ValueMember = "PrintFlag"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(17, 37)
        Me.Label10.MaximumSize = New System.Drawing.Size(175, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(151, 39)
        Me.Label10.TabIndex = 428
        Me.Label10.Text = "If you select an item from this dropdown, none of the other selections will have " & _
    "any effect."
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Comic Sans MS", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(32, 9)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(136, 19)
        Me.Label11.TabIndex = 427
        Me.Label11.Text = "Special Mailing Flag"
        '
        'frmMailLblSimple
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(984, 662)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.btnMap)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.btnHelp)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmMailLblSimple"
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
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
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
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents cboPrintFlag As System.Windows.Forms.ComboBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents rbAddress As System.Windows.Forms.RadioButton
    Friend WithEvents rbEmail As System.Windows.Forms.RadioButton
End Class
