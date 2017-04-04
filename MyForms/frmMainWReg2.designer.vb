<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMainWReg2
    Inherits System.Windows.Forms.Form


    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim RegDateLabel As System.Windows.Forms.Label
        Dim EventNumLabel As System.Windows.Forms.Label
        Dim OrgNumLabel As System.Windows.Forms.Label
        Dim Label13 As System.Windows.Forms.Label
        Dim Label17 As System.Windows.Forms.Label
        Dim Label1 As System.Windows.Forms.Label
        Dim Label14 As System.Windows.Forms.Label
        Dim NotesLabel As System.Windows.Forms.Label
        Dim QuestionsLabel As System.Windows.Forms.Label
        Dim ClergyLayLabel As System.Windows.Forms.Label
        Dim MaleFemaleLabel As System.Windows.Forms.Label
        Dim HowRegisteredLabel As System.Windows.Forms.Label
        Dim HowHeardLabel As System.Windows.Forms.Label
        Dim Label6 As System.Windows.Forms.Label
        Dim Label7 As System.Windows.Forms.Label
        Dim Label9 As System.Windows.Forms.Label
        Dim Label20 As System.Windows.Forms.Label
        Dim Label12 As System.Windows.Forms.Label
        Dim Label11 As System.Windows.Forms.Label
        Dim Label15 As System.Windows.Forms.Label
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMainWReg2))
        Me.lblGotoEvent = New System.Windows.Forms.Label()
        Me.ErrorProvider1 = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.MainEventReg2BindingNavigator = New System.Windows.Forms.BindingNavigator(Me.components)
        Me.MainEventReg2BindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.dsMainEventReg2 = New InfoCtr.dsMainEventReg2()
        Me.BindingNavigatorCountItem = New System.Windows.Forms.ToolStripLabel()
        Me.BindingNavigatorMoveFirstItem = New System.Windows.Forms.ToolStripButton()
        Me.BindingNavigatorMovePreviousItem = New System.Windows.Forms.ToolStripButton()
        Me.BindingNavigatorSeparator = New System.Windows.Forms.ToolStripSeparator()
        Me.BindingNavigatorPositionItem = New System.Windows.Forms.ToolStripTextBox()
        Me.BindingNavigatorSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.BindingNavigatorMoveNextItem = New System.Windows.Forms.ToolStripButton()
        Me.BindingNavigatorMoveLastItem = New System.Windows.Forms.ToolStripButton()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.btnSaveExit = New System.Windows.Forms.Button()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.MainMenu1 = New System.Windows.Forms.MainMenu(Me.components)
        Me.MenuItem1 = New System.Windows.Forms.MenuItem()
        Me.miNewReg = New System.Windows.Forms.MenuItem()
        Me.miClose = New System.Windows.Forms.MenuItem()
        Me.MenuItem2 = New System.Windows.Forms.MenuItem()
        Me.miSave = New System.Windows.Forms.MenuItem()
        Me.miCancel = New System.Windows.Forms.MenuItem()
        Me.miDelete = New System.Windows.Forms.MenuItem()
        Me.miRefreshContacts = New System.Windows.Forms.MenuItem()
        Me.miCopy = New System.Windows.Forms.MenuItem()
        Me.MenuItem3 = New System.Windows.Forms.MenuItem()
        Me.miMultiple = New System.Windows.Forms.MenuItem()
        Me.miGotoOrder = New System.Windows.Forms.MenuItem()
        Me.miGotoEvent = New System.Windows.Forms.MenuItem()
        Me.miHelp = New System.Windows.Forms.MenuItem()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.fldEventNum = New System.Windows.Forms.TextBox()
        Me.fldContactNum = New System.Windows.Forms.Label()
        Me.btnRefreshContacts = New System.Windows.Forms.Button()
        Me.editIndFee = New System.Windows.Forms.TextBox()
        Me.TextBox7 = New System.Windows.Forms.TextBox()
        Me.fldOrderNum = New System.Windows.Forms.TextBox()
        Me.TextBox8 = New System.Windows.Forms.TextBox()
        Me.chkCancelled = New System.Windows.Forms.CheckBox()
        Me.chkRefund = New System.Windows.Forms.CheckBox()
        Me.fldRefundAmount = New System.Windows.Forms.TextBox()
        Me.fldRefundCheck = New System.Windows.Forms.TextBox()
        Me.btnRequestRefund = New System.Windows.Forms.Button()
        Me.btnHelp = New System.Windows.Forms.Button()
        Me.lblGotoContact = New System.Windows.Forms.TextBox()
        Me.TextBox4 = New System.Windows.Forms.TextBox()
        Me.fldOrgNum = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.btnLoadOrderGrid = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.AttendedCheckBox = New System.Windows.Forms.CheckBox()
        Me.NametagCheckBox = New System.Windows.Forms.CheckBox()
        Me.InformationCheckBox = New System.Windows.Forms.CheckBox()
        Me.ConfirmationCheckBox = New System.Windows.Forms.CheckBox()
        Me.tctExisting = New System.Windows.Forms.TextBox()
        Me.txtPersonal = New System.Windows.Forms.TextBox()
        Me.rtbReason = New System.Windows.Forms.RichTextBox()
        Me.chkVeg = New System.Windows.Forms.CheckBox()
        Me.rtbNotes = New System.Windows.Forms.RichTextBox()
        Me.pnlFinance = New System.Windows.Forms.Panel()
        Me.TextBox5 = New System.Windows.Forms.TextBox()
        Me.lblGotoRefund = New System.Windows.Forms.Label()
        Me.dtRefundRequest = New System.Windows.Forms.TextBox()
        Me.TextBox9 = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.fldGotoOrder = New System.Windows.Forms.TextBox()
        Me.ToolTip2 = New System.Windows.Forms.ToolTip(Me.components)
        Me.fldPaymentNum = New System.Windows.Forms.TextBox()
        Me.StatusBar1 = New System.Windows.Forms.StatusBar()
        Me.StatusBarPanel1 = New System.Windows.Forms.StatusBarPanel()
        Me.StatusBarPanelID = New System.Windows.Forms.StatusBarPanel()
        Me.StatusBarPanel2 = New System.Windows.Forms.StatusBarPanel()
        Me.grdOrder = New System.Windows.Forms.DataGrid()
        Me.datagridtablestyle1 = New System.Windows.Forms.DataGridTableStyle()
        Me.DataGridTextBoxColumn1 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn2 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn6 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn4 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn3 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn5 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn7 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn8 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn9 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.MainEventReg2TableAdapter1 = New InfoCtr.dsMainEventReg2TableAdapters.MainEventReg2TableAdapter()
        Me.GetEventOrderList2TableAdapter = New InfoCtr.dsMainEventReg2TableAdapters.GetEventOrderList2TableAdapter()
        Me.DtRegistration = New InfoCtr.DateTextBox()
        Me.cboGender = New InfoCtr.ComboBoxRelaxed()
        Me.cboClergy = New InfoCtr.ComboBoxRelaxed()
        Me.cboRegistered = New InfoCtr.ComboBoxRelaxed()
        Me.cboHeard = New InfoCtr.ComboBoxRelaxed()
        Me.cboEvent = New InfoCtr.ComboBoxRelaxed()
        Me.cboRegistrant = New InfoCtr.ComboBoxRelaxed()
        Me.Label2 = New System.Windows.Forms.Label()
        RegDateLabel = New System.Windows.Forms.Label()
        EventNumLabel = New System.Windows.Forms.Label()
        OrgNumLabel = New System.Windows.Forms.Label()
        Label13 = New System.Windows.Forms.Label()
        Label17 = New System.Windows.Forms.Label()
        Label1 = New System.Windows.Forms.Label()
        Label14 = New System.Windows.Forms.Label()
        NotesLabel = New System.Windows.Forms.Label()
        QuestionsLabel = New System.Windows.Forms.Label()
        ClergyLayLabel = New System.Windows.Forms.Label()
        MaleFemaleLabel = New System.Windows.Forms.Label()
        HowRegisteredLabel = New System.Windows.Forms.Label()
        HowHeardLabel = New System.Windows.Forms.Label()
        Label6 = New System.Windows.Forms.Label()
        Label7 = New System.Windows.Forms.Label()
        Label9 = New System.Windows.Forms.Label()
        Label20 = New System.Windows.Forms.Label()
        Label12 = New System.Windows.Forms.Label()
        Label11 = New System.Windows.Forms.Label()
        Label15 = New System.Windows.Forms.Label()
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MainEventReg2BindingNavigator, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MainEventReg2BindingNavigator.SuspendLayout()
        CType(Me.MainEventReg2BindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dsMainEventReg2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        Me.pnlFinance.SuspendLayout()
        CType(Me.StatusBarPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.StatusBarPanelID, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.StatusBarPanel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdOrder, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RegDateLabel
        '
        RegDateLabel.AutoSize = True
        RegDateLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        RegDateLabel.Location = New System.Drawing.Point(44, 126)
        RegDateLabel.Name = "RegDateLabel"
        RegDateLabel.Size = New System.Drawing.Size(105, 15)
        RegDateLabel.TabIndex = 11
        RegDateLabel.Text = "Registration Date:"
        '
        'EventNumLabel
        '
        EventNumLabel.AutoSize = True
        EventNumLabel.Enabled = False
        EventNumLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        EventNumLabel.ForeColor = System.Drawing.SystemColors.ControlDark
        EventNumLabel.Location = New System.Drawing.Point(870, 78)
        EventNumLabel.Name = "EventNumLabel"
        EventNumLabel.Size = New System.Drawing.Size(44, 13)
        EventNumLabel.TabIndex = 443
        EventNumLabel.Text = "Event#:"
        '
        'OrgNumLabel
        '
        OrgNumLabel.AutoSize = True
        OrgNumLabel.Enabled = False
        OrgNumLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        OrgNumLabel.ForeColor = System.Drawing.SystemColors.ControlDark
        OrgNumLabel.Location = New System.Drawing.Point(862, 58)
        OrgNumLabel.Name = "OrgNumLabel"
        OrgNumLabel.Size = New System.Drawing.Size(52, 13)
        OrgNumLabel.TabIndex = 445
        OrgNumLabel.Text = "Contact#:"
        '
        'Label13
        '
        Label13.AutoSize = True
        Label13.Enabled = False
        Label13.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Label13.ForeColor = System.Drawing.SystemColors.ControlDark
        Label13.Location = New System.Drawing.Point(853, 99)
        Label13.Name = "Label13"
        Label13.Size = New System.Drawing.Size(61, 13)
        Label13.TabIndex = 465
        Label13.Text = "Entered by:"
        '
        'Label17
        '
        Label17.AutoSize = True
        Label17.Enabled = False
        Label17.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Label17.ForeColor = System.Drawing.SystemColors.ControlDark
        Label17.Location = New System.Drawing.Point(992, 696)
        Label17.Name = "Label17"
        Label17.Size = New System.Drawing.Size(51, 13)
        Label17.TabIndex = 479
        Label17.Text = "Org Num:"
        '
        'Label1
        '
        Label1.AutoSize = True
        Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Label1.Location = New System.Drawing.Point(70, 200)
        Label1.Name = "Label1"
        Label1.Size = New System.Drawing.Size(76, 13)
        Label1.TabIndex = 512
        Label1.Text = "Individual Fee:"
        '
        'Label14
        '
        Label14.AutoSize = True
        Label14.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Label14.Location = New System.Drawing.Point(41, 695)
        Label14.Name = "Label14"
        Label14.Size = New System.Drawing.Size(90, 13)
        Label14.TabIndex = 521
        Label14.Text = "Discount Applied:"
        '
        'NotesLabel
        '
        NotesLabel.AutoSize = True
        NotesLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        NotesLabel.ForeColor = System.Drawing.SystemColors.ControlText
        NotesLabel.Location = New System.Drawing.Point(46, 265)
        NotesLabel.Name = "NotesLabel"
        NotesLabel.Size = New System.Drawing.Size(105, 15)
        NotesLabel.TabIndex = 543
        NotesLabel.Text = "Registration Note:"
        AddHandler NotesLabel.Click, AddressOf Me.NotesLabel_Click
        '
        'QuestionsLabel
        '
        QuestionsLabel.AutoSize = True
        QuestionsLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        QuestionsLabel.Location = New System.Drawing.Point(559, 264)
        QuestionsLabel.Name = "QuestionsLabel"
        QuestionsLabel.Size = New System.Drawing.Size(169, 15)
        QuestionsLabel.TabIndex = 546
        QuestionsLabel.Text = "Primary Reason for Attending:"
        QuestionsLabel.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'ClergyLayLabel
        '
        ClergyLayLabel.AutoSize = True
        ClergyLayLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        ClergyLayLabel.Location = New System.Drawing.Point(573, 148)
        ClergyLayLabel.Name = "ClergyLayLabel"
        ClergyLayLabel.Size = New System.Drawing.Size(86, 15)
        ClergyLayLabel.TabIndex = 544
        ClergyLayLabel.Text = "Clergy or Laity:"
        '
        'MaleFemaleLabel
        '
        MaleFemaleLabel.AutoSize = True
        MaleFemaleLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        MaleFemaleLabel.Location = New System.Drawing.Point(562, 175)
        MaleFemaleLabel.Name = "MaleFemaleLabel"
        MaleFemaleLabel.Size = New System.Drawing.Size(97, 15)
        MaleFemaleLabel.TabIndex = 545
        MaleFemaleLabel.Text = "Male or Female:"
        '
        'HowRegisteredLabel
        '
        HowRegisteredLabel.AutoSize = True
        HowRegisteredLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        HowRegisteredLabel.Location = New System.Drawing.Point(561, 233)
        HowRegisteredLabel.Name = "HowRegisteredLabel"
        HowRegisteredLabel.Size = New System.Drawing.Size(98, 15)
        HowRegisteredLabel.TabIndex = 548
        HowRegisteredLabel.Text = "How Registered:"
        '
        'HowHeardLabel
        '
        HowHeardLabel.AutoSize = True
        HowHeardLabel.Enabled = False
        HowHeardLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        HowHeardLabel.Location = New System.Drawing.Point(553, 203)
        HowHeardLabel.Name = "HowHeardLabel"
        HowHeardLabel.Size = New System.Drawing.Size(106, 15)
        HowHeardLabel.TabIndex = 547
        HowHeardLabel.Text = "How Heard About:"
        '
        'Label6
        '
        Label6.AutoSize = True
        Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Label6.Location = New System.Drawing.Point(32, 34)
        Label6.Name = "Label6"
        Label6.Size = New System.Drawing.Size(60, 15)
        Label6.TabIndex = 518
        Label6.Text = "Refund $:"
        '
        'Label7
        '
        Label7.AutoSize = True
        Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Label7.Location = New System.Drawing.Point(7, 9)
        Label7.Name = "Label7"
        Label7.Size = New System.Drawing.Size(85, 15)
        Label7.TabIndex = 513
        Label7.Text = "Request Date:"
        '
        'Label9
        '
        Label9.AutoSize = True
        Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Label9.ForeColor = System.Drawing.SystemColors.ControlText
        Label9.Location = New System.Drawing.Point(907, 391)
        Label9.MinimumSize = New System.Drawing.Size(0, 25)
        Label9.Name = "Label9"
        Label9.Size = New System.Drawing.Size(93, 25)
        Label9.TabIndex = 517
        Label9.Text = "Center Check #:"
        Label9.Visible = False
        '
        'Label20
        '
        Label20.AutoSize = True
        Label20.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Label20.ForeColor = System.Drawing.SystemColors.ControlText
        Label20.Location = New System.Drawing.Point(7, 60)
        Label20.Margin = New System.Windows.Forms.Padding(0)
        Label20.Name = "Label20"
        Label20.Size = New System.Drawing.Size(85, 15)
        Label20.TabIndex = 525
        Label20.Text = "Requested by:"
        '
        'Label12
        '
        Label12.AutoSize = True
        Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Label12.Location = New System.Drawing.Point(330, 688)
        Label12.Name = "Label12"
        Label12.Size = New System.Drawing.Size(51, 13)
        Label12.TabIndex = 527
        Label12.Text = "Billed To:"
        Label12.Visible = False
        '
        'Label11
        '
        Label11.AutoSize = True
        Label11.Enabled = False
        Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Label11.ForeColor = System.Drawing.Color.Black
        Label11.Location = New System.Drawing.Point(78, 230)
        Label11.Name = "Label11"
        Label11.Size = New System.Drawing.Size(68, 15)
        Label11.TabIndex = 610
        Label11.Text = "Payment #:"
        '
        'Label15
        '
        Label15.AutoSize = True
        Label15.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Label15.ForeColor = System.Drawing.SystemColors.ControlText
        Label15.Location = New System.Drawing.Point(24, 85)
        Label15.Margin = New System.Windows.Forms.Padding(0)
        Label15.Name = "Label15"
        Label15.Size = New System.Drawing.Size(68, 15)
        Label15.TabIndex = 611
        Label15.Text = "Processed:"
        '
        'lblGotoEvent
        '
        Me.lblGotoEvent.AutoSize = True
        Me.lblGotoEvent.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblGotoEvent.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblGotoEvent.ForeColor = System.Drawing.Color.DarkGreen
        Me.lblGotoEvent.Location = New System.Drawing.Point(231, 474)
        Me.lblGotoEvent.Name = "lblGotoEvent"
        Me.lblGotoEvent.Size = New System.Drawing.Size(42, 17)
        Me.lblGotoEvent.TabIndex = 612
        Me.lblGotoEvent.Text = "Event:"
        Me.ToolTip2.SetToolTip(Me.lblGotoEvent, "Doubleclick to open Event Detail.")
        '
        'ErrorProvider1
        '
        Me.ErrorProvider1.ContainerControl = Me
        '
        'MainEventReg2BindingNavigator
        '
        Me.MainEventReg2BindingNavigator.AddNewItem = Nothing
        Me.MainEventReg2BindingNavigator.BindingSource = Me.MainEventReg2BindingSource
        Me.MainEventReg2BindingNavigator.CountItem = Me.BindingNavigatorCountItem
        Me.MainEventReg2BindingNavigator.DeleteItem = Nothing
        Me.MainEventReg2BindingNavigator.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.MainEventReg2BindingNavigator.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BindingNavigatorMoveFirstItem, Me.BindingNavigatorMovePreviousItem, Me.BindingNavigatorSeparator, Me.BindingNavigatorPositionItem, Me.BindingNavigatorCountItem, Me.BindingNavigatorSeparator1, Me.BindingNavigatorMoveNextItem, Me.BindingNavigatorMoveLastItem})
        Me.MainEventReg2BindingNavigator.Location = New System.Drawing.Point(0, 735)
        Me.MainEventReg2BindingNavigator.MoveFirstItem = Me.BindingNavigatorMoveFirstItem
        Me.MainEventReg2BindingNavigator.MoveLastItem = Me.BindingNavigatorMoveLastItem
        Me.MainEventReg2BindingNavigator.MoveNextItem = Me.BindingNavigatorMoveNextItem
        Me.MainEventReg2BindingNavigator.MovePreviousItem = Me.BindingNavigatorMovePreviousItem
        Me.MainEventReg2BindingNavigator.Name = "MainEventReg2BindingNavigator"
        Me.MainEventReg2BindingNavigator.PositionItem = Me.BindingNavigatorPositionItem
        Me.MainEventReg2BindingNavigator.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
        Me.MainEventReg2BindingNavigator.Size = New System.Drawing.Size(1095, 25)
        Me.MainEventReg2BindingNavigator.TabIndex = 0
        Me.MainEventReg2BindingNavigator.Text = "BindingNavigator1"
        '
        'MainEventReg2BindingSource
        '
        Me.MainEventReg2BindingSource.DataMember = "MainEventReg2"
        Me.MainEventReg2BindingSource.DataSource = Me.dsMainEventReg2
        '
        'dsMainEventReg2
        '
        Me.dsMainEventReg2.DataSetName = "dsMainEventReg2"
        Me.dsMainEventReg2.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'BindingNavigatorCountItem
        '
        Me.BindingNavigatorCountItem.Name = "BindingNavigatorCountItem"
        Me.BindingNavigatorCountItem.Size = New System.Drawing.Size(35, 22)
        Me.BindingNavigatorCountItem.Text = "of {0}"
        Me.BindingNavigatorCountItem.ToolTipText = "Total number of items"
        '
        'BindingNavigatorMoveFirstItem
        '
        Me.BindingNavigatorMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorMoveFirstItem.Image = CType(resources.GetObject("BindingNavigatorMoveFirstItem.Image"), System.Drawing.Image)
        Me.BindingNavigatorMoveFirstItem.Name = "BindingNavigatorMoveFirstItem"
        Me.BindingNavigatorMoveFirstItem.RightToLeftAutoMirrorImage = True
        Me.BindingNavigatorMoveFirstItem.Size = New System.Drawing.Size(23, 22)
        Me.BindingNavigatorMoveFirstItem.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        '
        'BindingNavigatorMovePreviousItem
        '
        Me.BindingNavigatorMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorMovePreviousItem.Image = CType(resources.GetObject("BindingNavigatorMovePreviousItem.Image"), System.Drawing.Image)
        Me.BindingNavigatorMovePreviousItem.Name = "BindingNavigatorMovePreviousItem"
        Me.BindingNavigatorMovePreviousItem.RightToLeftAutoMirrorImage = True
        Me.BindingNavigatorMovePreviousItem.Size = New System.Drawing.Size(23, 22)
        Me.BindingNavigatorMovePreviousItem.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        '
        'BindingNavigatorSeparator
        '
        Me.BindingNavigatorSeparator.Name = "BindingNavigatorSeparator"
        Me.BindingNavigatorSeparator.Size = New System.Drawing.Size(6, 25)
        '
        'BindingNavigatorPositionItem
        '
        Me.BindingNavigatorPositionItem.AccessibleName = "Position"
        Me.BindingNavigatorPositionItem.AutoSize = False
        Me.BindingNavigatorPositionItem.Name = "BindingNavigatorPositionItem"
        Me.BindingNavigatorPositionItem.Size = New System.Drawing.Size(50, 21)
        Me.BindingNavigatorPositionItem.Text = "0"
        Me.BindingNavigatorPositionItem.ToolTipText = "Current position"
        '
        'BindingNavigatorSeparator1
        '
        Me.BindingNavigatorSeparator1.Name = "BindingNavigatorSeparator1"
        Me.BindingNavigatorSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'BindingNavigatorMoveNextItem
        '
        Me.BindingNavigatorMoveNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorMoveNextItem.Image = CType(resources.GetObject("BindingNavigatorMoveNextItem.Image"), System.Drawing.Image)
        Me.BindingNavigatorMoveNextItem.Name = "BindingNavigatorMoveNextItem"
        Me.BindingNavigatorMoveNextItem.RightToLeftAutoMirrorImage = True
        Me.BindingNavigatorMoveNextItem.Size = New System.Drawing.Size(23, 22)
        Me.BindingNavigatorMoveNextItem.Text = "Move next"
        '
        'BindingNavigatorMoveLastItem
        '
        Me.BindingNavigatorMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorMoveLastItem.Image = CType(resources.GetObject("BindingNavigatorMoveLastItem.Image"), System.Drawing.Image)
        Me.BindingNavigatorMoveLastItem.Name = "BindingNavigatorMoveLastItem"
        Me.BindingNavigatorMoveLastItem.RightToLeftAutoMirrorImage = True
        Me.BindingNavigatorMoveLastItem.Size = New System.Drawing.Size(23, 22)
        Me.BindingNavigatorMoveLastItem.Text = "Move last"
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.SystemColors.Highlight
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Controls.Add(Me.btnSaveExit)
        Me.Panel2.Controls.Add(Me.btnDelete)
        Me.Panel2.Location = New System.Drawing.Point(854, 2)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(103, 40)
        Me.Panel2.TabIndex = 424
        '
        'btnSaveExit
        '
        Me.btnSaveExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSaveExit.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.btnSaveExit.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSaveExit.Image = CType(resources.GetObject("btnSaveExit.Image"), System.Drawing.Image)
        Me.btnSaveExit.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSaveExit.Location = New System.Drawing.Point(60, 1)
        Me.btnSaveExit.Name = "btnSaveExit"
        Me.btnSaveExit.Size = New System.Drawing.Size(40, 35)
        Me.btnSaveExit.TabIndex = 416
        Me.btnSaveExit.TabStop = False
        Me.btnSaveExit.Text = "Close"
        Me.btnSaveExit.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSaveExit.UseVisualStyleBackColor = False
        '
        'btnDelete
        '
        Me.btnDelete.BackColor = System.Drawing.SystemColors.Control
        Me.btnDelete.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Image = CType(resources.GetObject("btnDelete.Image"), System.Drawing.Image)
        Me.btnDelete.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnDelete.Location = New System.Drawing.Point(3, 1)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(40, 35)
        Me.btnDelete.TabIndex = 247
        Me.btnDelete.TabStop = False
        Me.btnDelete.Text = "Delete"
        Me.btnDelete.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnDelete.UseVisualStyleBackColor = True
        Me.btnDelete.Visible = False
        '
        'MainMenu1
        '
        Me.MainMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem1, Me.MenuItem2, Me.MenuItem3, Me.miHelp})
        '
        'MenuItem1
        '
        Me.MenuItem1.Index = 0
        Me.MenuItem1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.miNewReg, Me.miClose})
        Me.MenuItem1.Text = "File"
        '
        'miNewReg
        '
        Me.miNewReg.Index = 0
        Me.miNewReg.Text = "New Registration"
        '
        'miClose
        '
        Me.miClose.Index = 1
        Me.miClose.Text = "Close Window"
        '
        'MenuItem2
        '
        Me.MenuItem2.Index = 1
        Me.MenuItem2.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.miSave, Me.miCancel, Me.miDelete, Me.miRefreshContacts, Me.miCopy})
        Me.MenuItem2.Text = "Edit"
        '
        'miSave
        '
        Me.miSave.Enabled = False
        Me.miSave.Index = 0
        Me.miSave.Text = "Save Changes"
        '
        'miCancel
        '
        Me.miCancel.Index = 1
        Me.miCancel.Text = "Cancel Changes"
        '
        'miDelete
        '
        Me.miDelete.Index = 2
        Me.miDelete.Text = "Delete Registration"
        '
        'miRefreshContacts
        '
        Me.miRefreshContacts.Index = 3
        Me.miRefreshContacts.Text = "Refresh Contact DropDown"
        '
        'miCopy
        '
        Me.miCopy.Index = 4
        Me.miCopy.Text = "Copy Registration to Another Event"
        '
        'MenuItem3
        '
        Me.MenuItem3.Index = 2
        Me.MenuItem3.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.miMultiple, Me.miGotoOrder, Me.miGotoEvent})
        Me.MenuItem3.Text = "View"
        '
        'miMultiple
        '
        Me.miMultiple.Enabled = False
        Me.miMultiple.Index = 0
        Me.miMultiple.Text = "Series Registrations"
        '
        'miGotoOrder
        '
        Me.miGotoOrder.Index = 1
        Me.miGotoOrder.Text = "Go to Order Detail"
        '
        'miGotoEvent
        '
        Me.miGotoEvent.Index = 2
        Me.miGotoEvent.Text = "Go to Event Detail"
        '
        'miHelp
        '
        Me.miHelp.Index = 3
        Me.miHelp.Text = "Help"
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.SystemColors.Control
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label8.Location = New System.Drawing.Point(12, 11)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(229, 16)
        Me.Label8.TabIndex = 429
        Me.Label8.Text = "REGISTRATION DETAIL"
        '
        'fldEventNum
        '
        Me.fldEventNum.BackColor = System.Drawing.SystemColors.Control
        Me.fldEventNum.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.fldEventNum.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainEventReg2BindingSource, "EventNum", True))
        Me.fldEventNum.Enabled = False
        Me.fldEventNum.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fldEventNum.Location = New System.Drawing.Point(920, 78)
        Me.fldEventNum.Name = "fldEventNum"
        Me.fldEventNum.ReadOnly = True
        Me.fldEventNum.Size = New System.Drawing.Size(73, 13)
        Me.fldEventNum.TabIndex = 444
        Me.fldEventNum.TabStop = False
        '
        'fldContactNum
        '
        Me.fldContactNum.BackColor = System.Drawing.SystemColors.Control
        Me.fldContactNum.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainEventReg2BindingSource, "ContactNum", True))
        Me.fldContactNum.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fldContactNum.Location = New System.Drawing.Point(920, 59)
        Me.fldContactNum.Name = "fldContactNum"
        Me.fldContactNum.Size = New System.Drawing.Size(73, 13)
        Me.fldContactNum.TabIndex = 614
        '
        'btnRefreshContacts
        '
        Me.btnRefreshContacts.Location = New System.Drawing.Point(570, 50)
        Me.btnRefreshContacts.Name = "btnRefreshContacts"
        Me.btnRefreshContacts.Size = New System.Drawing.Size(70, 19)
        Me.btnRefreshContacts.TabIndex = 477
        Me.btnRefreshContacts.TabStop = False
        Me.btnRefreshContacts.Text = "refresh"
        Me.btnRefreshContacts.UseVisualStyleBackColor = True
        '
        'editIndFee
        '
        Me.editIndFee.BackColor = System.Drawing.SystemColors.Window
        Me.editIndFee.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainEventReg2BindingSource, "AmountIndFee", True, System.Windows.Forms.DataSourceUpdateMode.OnValidation, ""))
        Me.editIndFee.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.editIndFee.Location = New System.Drawing.Point(149, 195)
        Me.editIndFee.Name = "editIndFee"
        Me.editIndFee.Size = New System.Drawing.Size(50, 21)
        Me.editIndFee.TabIndex = 5
        '
        'TextBox7
        '
        Me.TextBox7.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox7.Location = New System.Drawing.Point(135, 690)
        Me.TextBox7.Name = "TextBox7"
        Me.TextBox7.ReadOnly = True
        Me.TextBox7.Size = New System.Drawing.Size(43, 23)
        Me.TextBox7.TabIndex = 520
        Me.TextBox7.TabStop = False
        '
        'fldOrderNum
        '
        Me.fldOrderNum.BackColor = System.Drawing.SystemColors.Window
        Me.fldOrderNum.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainEventReg2BindingSource, "OrderNum", True))
        Me.fldOrderNum.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fldOrderNum.Location = New System.Drawing.Point(149, 160)
        Me.fldOrderNum.Name = "fldOrderNum"
        Me.fldOrderNum.Size = New System.Drawing.Size(110, 21)
        Me.fldOrderNum.TabIndex = 3
        Me.fldOrderNum.TabStop = False
        '
        'TextBox8
        '
        Me.TextBox8.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainEventReg2BindingSource, "OnlineSID", True))
        Me.TextBox8.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox8.Location = New System.Drawing.Point(402, 275)
        Me.TextBox8.Name = "TextBox8"
        Me.TextBox8.ReadOnly = True
        Me.TextBox8.Size = New System.Drawing.Size(104, 18)
        Me.TextBox8.TabIndex = 30
        Me.TextBox8.TabStop = False
        Me.ToolTip2.SetToolTip(Me.TextBox8, "for online registrations")
        '
        'chkCancelled
        '
        Me.chkCancelled.DataBindings.Add(New System.Windows.Forms.Binding("CheckState", Me.MainEventReg2BindingSource, "Cancelled", True))
        Me.chkCancelled.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCancelled.Location = New System.Drawing.Point(344, 170)
        Me.chkCancelled.Name = "chkCancelled"
        Me.chkCancelled.Size = New System.Drawing.Size(96, 24)
        Me.chkCancelled.TabIndex = 24
        Me.chkCancelled.Text = "Cancelled"
        '
        'chkRefund
        '
        Me.chkRefund.DataBindings.Add(New System.Windows.Forms.Binding("CheckState", Me.MainEventReg2BindingSource, "RefundQfy", True))
        Me.chkRefund.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.chkRefund.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.chkRefund.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.chkRefund.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkRefund.Location = New System.Drawing.Point(344, 194)
        Me.chkRefund.Name = "chkRefund"
        Me.chkRefund.Size = New System.Drawing.Size(124, 24)
        Me.chkRefund.TabIndex = 26
        Me.chkRefund.Text = "Qualifies for Refund"
        Me.ToolTip2.SetToolTip(Me.chkRefund, "click here to REQUEST Refund.")
        '
        'fldRefundAmount
        '
        Me.fldRefundAmount.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainEventReg2BindingSource, "RefundRequestAmount", True))
        Me.fldRefundAmount.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fldRefundAmount.ForeColor = System.Drawing.SystemColors.WindowText
        Me.fldRefundAmount.Location = New System.Drawing.Point(95, 29)
        Me.fldRefundAmount.Name = "fldRefundAmount"
        Me.fldRefundAmount.ReadOnly = True
        Me.fldRefundAmount.Size = New System.Drawing.Size(103, 21)
        Me.fldRefundAmount.TabIndex = 54
        '
        'fldRefundCheck
        '
        Me.fldRefundCheck.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fldRefundCheck.ForeColor = System.Drawing.SystemColors.WindowText
        Me.fldRefundCheck.Location = New System.Drawing.Point(920, 419)
        Me.fldRefundCheck.Name = "fldRefundCheck"
        Me.fldRefundCheck.Size = New System.Drawing.Size(80, 21)
        Me.fldRefundCheck.TabIndex = 1
        Me.fldRefundCheck.Visible = False
        '
        'btnRequestRefund
        '
        Me.btnRequestRefund.Enabled = False
        Me.btnRequestRefund.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRequestRefund.Location = New System.Drawing.Point(1008, 6)
        Me.btnRequestRefund.Margin = New System.Windows.Forms.Padding(0)
        Me.btnRequestRefund.Name = "btnRequestRefund"
        Me.btnRequestRefund.Size = New System.Drawing.Size(54, 36)
        Me.btnRequestRefund.TabIndex = 601
        Me.btnRequestRefund.TabStop = False
        Me.btnRequestRefund.Text = "Request Refund"
        Me.btnRequestRefund.UseVisualStyleBackColor = True
        Me.btnRequestRefund.Visible = False
        '
        'btnHelp
        '
        Me.btnHelp.BackColor = System.Drawing.SystemColors.Control
        Me.btnHelp.Enabled = False
        Me.btnHelp.Font = New System.Drawing.Font("Microsoft Sans Serif", 22.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnHelp.ForeColor = System.Drawing.SystemColors.Control
        Me.btnHelp.Image = CType(resources.GetObject("btnHelp.Image"), System.Drawing.Image)
        Me.btnHelp.Location = New System.Drawing.Point(128, -1210)
        Me.btnHelp.Name = "btnHelp"
        Me.btnHelp.Size = New System.Drawing.Size(25, 25)
        Me.btnHelp.TabIndex = 423
        Me.btnHelp.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnHelp.UseVisualStyleBackColor = False
        Me.btnHelp.Visible = False
        '
        'lblGotoContact
        '
        Me.lblGotoContact.BackColor = System.Drawing.SystemColors.Control
        Me.lblGotoContact.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblGotoContact.ForeColor = System.Drawing.Color.DarkGreen
        Me.lblGotoContact.Location = New System.Drawing.Point(17, 45)
        Me.lblGotoContact.Name = "lblGotoContact"
        Me.lblGotoContact.ReadOnly = True
        Me.lblGotoContact.Size = New System.Drawing.Size(77, 23)
        Me.lblGotoContact.TabIndex = 452
        Me.lblGotoContact.TabStop = False
        Me.lblGotoContact.Tag = "Contact"
        Me.lblGotoContact.Text = "Registrant:"
        Me.ToolTip2.SetToolTip(Me.lblGotoContact, "Double click to open Contact Detail.")
        '
        'TextBox4
        '
        Me.TextBox4.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBox4.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainEventReg2BindingSource, "EnteredBy", True))
        Me.TextBox4.Enabled = False
        Me.TextBox4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox4.Location = New System.Drawing.Point(920, 99)
        Me.TextBox4.Name = "TextBox4"
        Me.TextBox4.ReadOnly = True
        Me.TextBox4.Size = New System.Drawing.Size(97, 13)
        Me.TextBox4.TabIndex = 463
        Me.TextBox4.TabStop = False
        '
        'fldOrgNum
        '
        Me.fldOrgNum.BackColor = System.Drawing.SystemColors.Control
        Me.fldOrgNum.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.fldOrgNum.Enabled = False
        Me.fldOrgNum.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fldOrgNum.Location = New System.Drawing.Point(1050, 696)
        Me.fldOrgNum.Name = "fldOrgNum"
        Me.fldOrgNum.ReadOnly = True
        Me.fldOrgNum.Size = New System.Drawing.Size(43, 13)
        Me.fldOrgNum.TabIndex = 478
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label5.Location = New System.Drawing.Point(132, 96)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(100, 13)
        Me.Label5.TabIndex = 504
        Me.Label5.Text = "REGISTRATION"
        '
        'btnLoadOrderGrid
        '
        Me.btnLoadOrderGrid.Location = New System.Drawing.Point(22, 522)
        Me.btnLoadOrderGrid.Name = "btnLoadOrderGrid"
        Me.btnLoadOrderGrid.Size = New System.Drawing.Size(210, 23)
        Me.btnLoadOrderGrid.TabIndex = 551
        Me.btnLoadOrderGrid.TabStop = False
        Me.btnLoadOrderGrid.Text = "Click to View Everyone In This Order"
        Me.btnLoadOrderGrid.UseVisualStyleBackColor = True
        Me.btnLoadOrderGrid.Visible = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label4.Location = New System.Drawing.Point(381, 96)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(65, 13)
        Me.Label4.TabIndex = 553
        Me.Label4.Text = "PROCESS"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label3.Location = New System.Drawing.Point(616, 98)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(59, 13)
        Me.Label3.TabIndex = 552
        Me.Label3.Text = "PROFILE"
        '
        'AttendedCheckBox
        '
        Me.AttendedCheckBox.DataBindings.Add(New System.Windows.Forms.Binding("Checked", Me.MainEventReg2BindingSource, "Attended", True))
        Me.AttendedCheckBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AttendedCheckBox.Location = New System.Drawing.Point(344, 146)
        Me.AttendedCheckBox.Name = "AttendedCheckBox"
        Me.AttendedCheckBox.Size = New System.Drawing.Size(96, 24)
        Me.AttendedCheckBox.TabIndex = 22
        Me.AttendedCheckBox.Text = "Attended"
        '
        'NametagCheckBox
        '
        Me.NametagCheckBox.DataBindings.Add(New System.Windows.Forms.Binding("Checked", Me.MainEventReg2BindingSource, "Nametag", True))
        Me.NametagCheckBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NametagCheckBox.Location = New System.Drawing.Point(344, 122)
        Me.NametagCheckBox.Name = "NametagCheckBox"
        Me.NametagCheckBox.Size = New System.Drawing.Size(150, 24)
        Me.NametagCheckBox.TabIndex = 20
        Me.NametagCheckBox.Text = "Nametag Printed"
        '
        'InformationCheckBox
        '
        Me.InformationCheckBox.DataBindings.Add(New System.Windows.Forms.Binding("Checked", Me.MainEventReg2BindingSource, "Information", True))
        Me.InformationCheckBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.InformationCheckBox.Location = New System.Drawing.Point(344, 233)
        Me.InformationCheckBox.Margin = New System.Windows.Forms.Padding(0)
        Me.InformationCheckBox.Name = "InformationCheckBox"
        Me.InformationCheckBox.Size = New System.Drawing.Size(141, 20)
        Me.InformationCheckBox.TabIndex = 28
        Me.InformationCheckBox.Text = "Information Sent"
        '
        'ConfirmationCheckBox
        '
        Me.ConfirmationCheckBox.DataBindings.Add(New System.Windows.Forms.Binding("Checked", Me.MainEventReg2BindingSource, "Confirmation", True))
        Me.ConfirmationCheckBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ConfirmationCheckBox.Location = New System.Drawing.Point(344, 253)
        Me.ConfirmationCheckBox.Margin = New System.Windows.Forms.Padding(0)
        Me.ConfirmationCheckBox.Name = "ConfirmationCheckBox"
        Me.ConfirmationCheckBox.Size = New System.Drawing.Size(141, 27)
        Me.ConfirmationCheckBox.TabIndex = 30
        Me.ConfirmationCheckBox.Text = "Confirmation Sent"
        '
        'tctExisting
        '
        Me.tctExisting.Location = New System.Drawing.Point(920, 448)
        Me.tctExisting.Name = "tctExisting"
        Me.tctExisting.Size = New System.Drawing.Size(74, 20)
        Me.tctExisting.TabIndex = 550
        Me.tctExisting.Visible = False
        '
        'txtPersonal
        '
        Me.txtPersonal.Location = New System.Drawing.Point(920, 474)
        Me.txtPersonal.Name = "txtPersonal"
        Me.txtPersonal.Size = New System.Drawing.Size(74, 20)
        Me.txtPersonal.TabIndex = 549
        Me.txtPersonal.Visible = False
        '
        'rtbReason
        '
        Me.rtbReason.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainEventReg2BindingSource, "Questions", True))
        Me.rtbReason.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rtbReason.Location = New System.Drawing.Point(559, 282)
        Me.rtbReason.MaxLength = 4000
        Me.rtbReason.Name = "rtbReason"
        Me.rtbReason.Size = New System.Drawing.Size(249, 156)
        Me.rtbReason.TabIndex = 42
        Me.rtbReason.Text = ""
        '
        'chkVeg
        '
        Me.chkVeg.DataBindings.Add(New System.Windows.Forms.Binding("Checked", Me.MainEventReg2BindingSource, "Vegetarian", True))
        Me.chkVeg.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkVeg.Location = New System.Drawing.Point(619, 119)
        Me.chkVeg.Name = "chkVeg"
        Me.chkVeg.Size = New System.Drawing.Size(146, 22)
        Me.chkVeg.TabIndex = 32
        Me.chkVeg.Text = "Vegetarian Meal"
        '
        'rtbNotes
        '
        Me.rtbNotes.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainEventReg2BindingSource, "Notes", True))
        Me.rtbNotes.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rtbNotes.Location = New System.Drawing.Point(47, 282)
        Me.rtbNotes.Name = "rtbNotes"
        Me.rtbNotes.Size = New System.Drawing.Size(249, 156)
        Me.rtbNotes.TabIndex = 10
        Me.rtbNotes.Text = ""
        '
        'pnlFinance
        '
        Me.pnlFinance.Controls.Add(Label15)
        Me.pnlFinance.Controls.Add(Me.TextBox5)
        Me.pnlFinance.Controls.Add(Label20)
        Me.pnlFinance.Controls.Add(Me.lblGotoRefund)
        Me.pnlFinance.Controls.Add(Label6)
        Me.pnlFinance.Controls.Add(Label7)
        Me.pnlFinance.Controls.Add(Me.fldRefundAmount)
        Me.pnlFinance.Controls.Add(Me.dtRefundRequest)
        Me.pnlFinance.Location = New System.Drawing.Point(321, 327)
        Me.pnlFinance.Name = "pnlFinance"
        Me.pnlFinance.Size = New System.Drawing.Size(212, 111)
        Me.pnlFinance.TabIndex = 50
        '
        'TextBox5
        '
        Me.TextBox5.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainEventReg2BindingSource, "RefundRequestStaffNum", True))
        Me.TextBox5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox5.ForeColor = System.Drawing.SystemColors.WindowText
        Me.TextBox5.Location = New System.Drawing.Point(95, 54)
        Me.TextBox5.Name = "TextBox5"
        Me.TextBox5.ReadOnly = True
        Me.TextBox5.Size = New System.Drawing.Size(103, 21)
        Me.TextBox5.TabIndex = 56
        '
        'lblGotoRefund
        '
        Me.lblGotoRefund.AutoSize = True
        Me.lblGotoRefund.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblGotoRefund.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainEventReg2BindingSource, "RefundNum", True))
        Me.lblGotoRefund.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblGotoRefund.ForeColor = System.Drawing.Color.DarkGreen
        Me.lblGotoRefund.Location = New System.Drawing.Point(95, 83)
        Me.lblGotoRefund.MinimumSize = New System.Drawing.Size(100, 20)
        Me.lblGotoRefund.Name = "lblGotoRefund"
        Me.lblGotoRefund.Size = New System.Drawing.Size(100, 20)
        Me.lblGotoRefund.TabIndex = 58
        Me.lblGotoRefund.Text = "Refund#"
        Me.ToolTip2.SetToolTip(Me.lblGotoRefund, "doubleclick to open Refund Detail.")
        '
        'dtRefundRequest
        '
        Me.dtRefundRequest.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainEventReg2BindingSource, "RefundRequestDate", True))
        Me.dtRefundRequest.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtRefundRequest.ForeColor = System.Drawing.SystemColors.WindowText
        Me.dtRefundRequest.Location = New System.Drawing.Point(95, 10)
        Me.dtRefundRequest.Name = "dtRefundRequest"
        Me.dtRefundRequest.ReadOnly = True
        Me.dtRefundRequest.Size = New System.Drawing.Size(103, 21)
        Me.dtRefundRequest.TabIndex = 52
        '
        'TextBox9
        '
        Me.TextBox9.BackColor = System.Drawing.SystemColors.Control
        Me.TextBox9.Enabled = False
        Me.TextBox9.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox9.Location = New System.Drawing.Point(408, 688)
        Me.TextBox9.Name = "TextBox9"
        Me.TextBox9.ReadOnly = True
        Me.TextBox9.Size = New System.Drawing.Size(206, 20)
        Me.TextBox9.TabIndex = 526
        Me.TextBox9.TabStop = False
        Me.TextBox9.Visible = False
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label10.Location = New System.Drawing.Point(388, 311)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(58, 13)
        Me.Label10.TabIndex = 609
        Me.Label10.Text = "REFUND"
        '
        'fldGotoOrder
        '
        Me.fldGotoOrder.BackColor = System.Drawing.SystemColors.Control
        Me.fldGotoOrder.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fldGotoOrder.ForeColor = System.Drawing.Color.DarkGreen
        Me.fldGotoOrder.Location = New System.Drawing.Point(69, 159)
        Me.fldGotoOrder.Name = "fldGotoOrder"
        Me.fldGotoOrder.ReadOnly = True
        Me.fldGotoOrder.Size = New System.Drawing.Size(77, 23)
        Me.fldGotoOrder.TabIndex = 611
        Me.fldGotoOrder.TabStop = False
        Me.fldGotoOrder.Tag = ""
        Me.fldGotoOrder.Text = "Order #:"
        Me.fldGotoOrder.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ToolTip2.SetToolTip(Me.fldGotoOrder, "Doubleclick to open Order summary.")
        '
        'fldPaymentNum
        '
        Me.fldPaymentNum.BackColor = System.Drawing.SystemColors.Control
        Me.fldPaymentNum.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainEventReg2BindingSource, "PaymentNum", True))
        Me.fldPaymentNum.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fldPaymentNum.ForeColor = System.Drawing.Color.DarkGreen
        Me.fldPaymentNum.Location = New System.Drawing.Point(149, 230)
        Me.fldPaymentNum.Name = "fldPaymentNum"
        Me.fldPaymentNum.ReadOnly = True
        Me.fldPaymentNum.Size = New System.Drawing.Size(77, 20)
        Me.fldPaymentNum.TabIndex = 8
        Me.fldPaymentNum.TabStop = False
        Me.fldPaymentNum.Tag = ""
        Me.ToolTip2.SetToolTip(Me.fldPaymentNum, "Doubleclick to open Payment Detail.")
        '
        'StatusBar1
        '
        Me.StatusBar1.Location = New System.Drawing.Point(0, 713)
        Me.StatusBar1.Name = "StatusBar1"
        Me.StatusBar1.Panels.AddRange(New System.Windows.Forms.StatusBarPanel() {Me.StatusBarPanel1, Me.StatusBarPanelID, Me.StatusBarPanel2})
        Me.StatusBar1.ShowPanels = True
        Me.StatusBar1.Size = New System.Drawing.Size(1095, 22)
        Me.StatusBar1.TabIndex = 613
        Me.StatusBar1.Text = "StatusBar1"
        '
        'StatusBarPanel1
        '
        Me.StatusBarPanel1.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents
        Me.StatusBarPanel1.BorderStyle = System.Windows.Forms.StatusBarPanelBorderStyle.None
        Me.StatusBarPanel1.MinWidth = 200
        Me.StatusBarPanel1.Name = "StatusBarPanel1"
        Me.StatusBarPanel1.Text = "StatusBarPanel1"
        Me.StatusBarPanel1.Width = 200
        '
        'StatusBarPanelID
        '
        Me.StatusBarPanelID.Alignment = System.Windows.Forms.HorizontalAlignment.Center
        Me.StatusBarPanelID.BorderStyle = System.Windows.Forms.StatusBarPanelBorderStyle.Raised
        Me.StatusBarPanelID.Name = "StatusBarPanelID"
        Me.StatusBarPanelID.Text = "RegistrationID"
        Me.StatusBarPanelID.Width = 200
        '
        'StatusBarPanel2
        '
        Me.StatusBarPanel2.Alignment = System.Windows.Forms.HorizontalAlignment.Right
        Me.StatusBarPanel2.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring
        Me.StatusBarPanel2.BorderStyle = System.Windows.Forms.StatusBarPanelBorderStyle.None
        Me.StatusBarPanel2.Name = "StatusBarPanel2"
        Me.StatusBarPanel2.Text = "Use this window to change Registration Detail."
        Me.StatusBarPanel2.Width = 678
        '
        'grdOrder
        '
        Me.grdOrder.CaptionFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.grdOrder.CaptionText = "Everyone with this Order #."
        Me.grdOrder.DataMember = "GetEventOrderList2"
        Me.grdOrder.DataSource = Me.dsMainEventReg2
        Me.grdOrder.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.grdOrder.Location = New System.Drawing.Point(17, 551)
        Me.grdOrder.Name = "grdOrder"
        Me.grdOrder.ReadOnly = True
        Me.grdOrder.RowHeaderWidth = 20
        Me.grdOrder.Size = New System.Drawing.Size(964, 116)
        Me.grdOrder.TabIndex = 554
        Me.grdOrder.TableStyles.AddRange(New System.Windows.Forms.DataGridTableStyle() {Me.datagridtablestyle1})
        Me.grdOrder.TabStop = False
        Me.grdOrder.Visible = False
        '
        'datagridtablestyle1
        '
        Me.datagridtablestyle1.DataGrid = Me.grdOrder
        Me.datagridtablestyle1.GridColumnStyles.AddRange(New System.Windows.Forms.DataGridColumnStyle() {Me.DataGridTextBoxColumn1, Me.DataGridTextBoxColumn2, Me.DataGridTextBoxColumn6, Me.DataGridTextBoxColumn4, Me.DataGridTextBoxColumn3, Me.DataGridTextBoxColumn5, Me.DataGridTextBoxColumn7, Me.DataGridTextBoxColumn8, Me.DataGridTextBoxColumn9})
        Me.datagridtablestyle1.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.datagridtablestyle1.MappingName = "GetEventOrderList2"
        Me.datagridtablestyle1.ReadOnly = True
        Me.datagridtablestyle1.RowHeaderWidth = 20
        '
        'DataGridTextBoxColumn1
        '
        Me.DataGridTextBoxColumn1.Format = ""
        Me.DataGridTextBoxColumn1.FormatInfo = Nothing
        Me.DataGridTextBoxColumn1.HeaderText = "RegistrationID"
        Me.DataGridTextBoxColumn1.MappingName = "RegistrationID"
        Me.DataGridTextBoxColumn1.Width = 0
        '
        'DataGridTextBoxColumn2
        '
        Me.DataGridTextBoxColumn2.Format = ""
        Me.DataGridTextBoxColumn2.FormatInfo = Nothing
        Me.DataGridTextBoxColumn2.HeaderText = "OrderNum"
        Me.DataGridTextBoxColumn2.MappingName = "OrderNum"
        Me.DataGridTextBoxColumn2.Width = 0
        '
        'DataGridTextBoxColumn6
        '
        Me.DataGridTextBoxColumn6.Format = ""
        Me.DataGridTextBoxColumn6.FormatInfo = Nothing
        Me.DataGridTextBoxColumn6.HeaderText = "Registrar"
        Me.DataGridTextBoxColumn6.MappingName = "Registrar"
        Me.DataGridTextBoxColumn6.Width = 200
        '
        'DataGridTextBoxColumn4
        '
        Me.DataGridTextBoxColumn4.Format = ""
        Me.DataGridTextBoxColumn4.FormatInfo = Nothing
        Me.DataGridTextBoxColumn4.HeaderText = "Lastname"
        Me.DataGridTextBoxColumn4.MappingName = "Lastname"
        Me.DataGridTextBoxColumn4.Width = 75
        '
        'DataGridTextBoxColumn3
        '
        Me.DataGridTextBoxColumn3.Format = ""
        Me.DataGridTextBoxColumn3.FormatInfo = Nothing
        Me.DataGridTextBoxColumn3.HeaderText = "Firstname"
        Me.DataGridTextBoxColumn3.MappingName = "FirstName"
        Me.DataGridTextBoxColumn3.Width = 75
        '
        'DataGridTextBoxColumn5
        '
        Me.DataGridTextBoxColumn5.Format = ""
        Me.DataGridTextBoxColumn5.FormatInfo = Nothing
        Me.DataGridTextBoxColumn5.HeaderText = "Organization"
        Me.DataGridTextBoxColumn5.MappingName = "OrgName"
        Me.DataGridTextBoxColumn5.Width = 150
        '
        'DataGridTextBoxColumn7
        '
        Me.DataGridTextBoxColumn7.Format = ""
        Me.DataGridTextBoxColumn7.FormatInfo = Nothing
        Me.DataGridTextBoxColumn7.HeaderText = "Event"
        Me.DataGridTextBoxColumn7.MappingName = "EventSKU"
        Me.DataGridTextBoxColumn7.Width = 175
        '
        'DataGridTextBoxColumn8
        '
        Me.DataGridTextBoxColumn8.Format = ""
        Me.DataGridTextBoxColumn8.FormatInfo = Nothing
        Me.DataGridTextBoxColumn8.HeaderText = "Congr"
        Me.DataGridTextBoxColumn8.MappingName = "Orgname"
        Me.DataGridTextBoxColumn8.Width = 75
        '
        'DataGridTextBoxColumn9
        '
        Me.DataGridTextBoxColumn9.Format = ""
        Me.DataGridTextBoxColumn9.FormatInfo = Nothing
        Me.DataGridTextBoxColumn9.HeaderText = "OrderNotes"
        Me.DataGridTextBoxColumn9.MappingName = "OrderNotes"
        Me.DataGridTextBoxColumn9.Width = 250
        '
        'MainEventReg2TableAdapter1
        '
        Me.MainEventReg2TableAdapter1.ClearBeforeFill = True
        '
        'GetEventOrderList2TableAdapter
        '
        Me.GetEventOrderList2TableAdapter.ClearBeforeFill = True
        '
        'DtRegistration
        '
        Me.DtRegistration.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainEventReg2BindingSource, "RegDate", True))
        Me.DtRegistration.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DtRegistration.Location = New System.Drawing.Point(149, 125)
        Me.DtRegistration.Name = "DtRegistration"
        Me.DtRegistration.Size = New System.Drawing.Size(110, 21)
        Me.DtRegistration.TabIndex = 2
        '
        'cboGender
        '
        Me.cboGender.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboGender.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboGender.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.cboGender.DataBindings.Add(New System.Windows.Forms.Binding("SelectedItem", Me.MainEventReg2BindingSource, "MaleFemale", True))
        Me.cboGender.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboGender.FormattingEnabled = True
        Me.cboGender.Items.AddRange(New Object() {"F", "M"})
        Me.cboGender.Location = New System.Drawing.Point(662, 173)
        Me.cboGender.Name = "cboGender"
        Me.cboGender.RestrictContentToListItems = True
        Me.cboGender.Size = New System.Drawing.Size(66, 23)
        Me.cboGender.TabIndex = 36
        '
        'cboClergy
        '
        Me.cboClergy.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboClergy.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboClergy.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.cboClergy.DataBindings.Add(New System.Windows.Forms.Binding("SelectedItem", Me.MainEventReg2BindingSource, "ClergyLay", True))
        Me.cboClergy.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboClergy.FormattingEnabled = True
        Me.cboClergy.Items.AddRange(New Object() {"C", "L"})
        Me.cboClergy.Location = New System.Drawing.Point(662, 145)
        Me.cboClergy.Name = "cboClergy"
        Me.cboClergy.RestrictContentToListItems = True
        Me.cboClergy.Size = New System.Drawing.Size(66, 23)
        Me.cboClergy.TabIndex = 34
        '
        'cboRegistered
        '
        Me.cboRegistered.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboRegistered.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboRegistered.BackColor = System.Drawing.SystemColors.Window
        Me.cboRegistered.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainEventReg2BindingSource, "HowRegistered", True))
        Me.cboRegistered.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboRegistered.FormattingEnabled = True
        Me.cboRegistered.Items.AddRange(New Object() {"By Someone Else", "Email", "In Person", "Phone", "Fax ", "Mail", "Website"})
        Me.cboRegistered.Location = New System.Drawing.Point(662, 230)
        Me.cboRegistered.Name = "cboRegistered"
        Me.cboRegistered.RestrictContentToListItems = True
        Me.cboRegistered.Size = New System.Drawing.Size(146, 24)
        Me.cboRegistered.TabIndex = 40
        '
        'cboHeard
        '
        Me.cboHeard.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboHeard.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboHeard.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainEventReg2BindingSource, "HowHeard", True))
        Me.cboHeard.Enabled = False
        Me.cboHeard.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboHeard.FormattingEnabled = True
        Me.cboHeard.Items.AddRange(New Object() {"Email", "Mailing", "Newspaper", "ICC Website", "Other Event", "Person", "Other"})
        Me.cboHeard.Location = New System.Drawing.Point(662, 200)
        Me.cboHeard.Name = "cboHeard"
        Me.cboHeard.RestrictContentToListItems = True
        Me.cboHeard.Size = New System.Drawing.Size(146, 24)
        Me.cboHeard.TabIndex = 38
        '
        'cboEvent
        '
        Me.cboEvent.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboEvent.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboEvent.DisplayMember = "EventName"
        Me.cboEvent.DropDownWidth = 500
        Me.cboEvent.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboEvent.FormattingEnabled = True
        Me.cboEvent.Location = New System.Drawing.Point(283, 474)
        Me.cboEvent.Name = "cboEvent"
        Me.cboEvent.RestrictContentToListItems = True
        Me.cboEvent.Size = New System.Drawing.Size(432, 23)
        Me.cboEvent.TabIndex = 0
        Me.cboEvent.TabStop = False
        Me.cboEvent.ValueMember = "EventID"
        '
        'cboRegistrant
        '
        Me.cboRegistrant.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboRegistrant.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboRegistrant.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.MainEventReg2BindingSource, "ContactNum", True))
        Me.cboRegistrant.DisplayMember = "ContactOrgCity"
        Me.cboRegistrant.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboRegistrant.FormattingEnabled = True
        Me.cboRegistrant.Location = New System.Drawing.Point(97, 46)
        Me.cboRegistrant.Name = "cboRegistrant"
        Me.cboRegistrant.RestrictContentToListItems = True
        Me.cboRegistrant.Size = New System.Drawing.Size(431, 23)
        Me.cboRegistrant.TabIndex = 1
        Me.cboRegistrant.TabStop = False
        Me.cboRegistrant.Tag = "Registrant"
        Me.cboRegistrant.ValueMember = "ContactID"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(388, 275)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(14, 13)
        Me.Label2.TabIndex = 615
        Me.Label2.Text = "#"
        '
        'frmMainWReg2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(1112, 691)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.fldPaymentNum)
        Me.Controls.Add(Me.StatusBar1)
        Me.Controls.Add(Me.lblGotoEvent)
        Me.Controls.Add(Me.fldGotoOrder)
        Me.Controls.Add(Label11)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.btnRequestRefund)
        Me.Controls.Add(Me.rtbNotes)
        Me.Controls.Add(NotesLabel)
        Me.Controls.Add(Label9)
        Me.Controls.Add(Me.chkRefund)
        Me.Controls.Add(Label14)
        Me.Controls.Add(Me.TextBox7)
        Me.Controls.Add(Me.fldRefundCheck)
        Me.Controls.Add(Label12)
        Me.Controls.Add(Me.TextBox9)
        Me.Controls.Add(Me.editIndFee)
        Me.Controls.Add(Label1)
        Me.Controls.Add(Me.TextBox8)
        Me.Controls.Add(RegDateLabel)
        Me.Controls.Add(Me.DtRegistration)
        Me.Controls.Add(Me.fldOrderNum)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.btnLoadOrderGrid)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.chkCancelled)
        Me.Controls.Add(Me.AttendedCheckBox)
        Me.Controls.Add(Me.NametagCheckBox)
        Me.Controls.Add(Me.InformationCheckBox)
        Me.Controls.Add(Me.ConfirmationCheckBox)
        Me.Controls.Add(Me.tctExisting)
        Me.Controls.Add(Me.grdOrder)
        Me.Controls.Add(Me.txtPersonal)
        Me.Controls.Add(QuestionsLabel)
        Me.Controls.Add(Me.rtbReason)
        Me.Controls.Add(Me.cboGender)
        Me.Controls.Add(Me.chkVeg)
        Me.Controls.Add(Me.cboClergy)
        Me.Controls.Add(ClergyLayLabel)
        Me.Controls.Add(MaleFemaleLabel)
        Me.Controls.Add(Me.cboRegistered)
        Me.Controls.Add(HowRegisteredLabel)
        Me.Controls.Add(Me.cboHeard)
        Me.Controls.Add(HowHeardLabel)
        Me.Controls.Add(Me.fldOrgNum)
        Me.Controls.Add(Label17)
        Me.Controls.Add(Me.btnRefreshContacts)
        Me.Controls.Add(Label13)
        Me.Controls.Add(Me.TextBox4)
        Me.Controls.Add(Me.lblGotoContact)
        Me.Controls.Add(EventNumLabel)
        Me.Controls.Add(Me.fldEventNum)
        Me.Controls.Add(OrgNumLabel)
        Me.Controls.Add(Me.fldContactNum)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.cboEvent)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.btnHelp)
        Me.Controls.Add(Me.cboRegistrant)
        Me.Controls.Add(Me.pnlFinance)
        Me.Controls.Add(Me.MainEventReg2BindingNavigator)
        Me.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainEventReg2BindingSource, "TitleBar", True))
        Me.Menu = Me.MainMenu1
        Me.Name = "frmMainWReg2"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Tag = "REGISTRATION"
        Me.Text = "REGISTRATION DETAIL"
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MainEventReg2BindingNavigator, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MainEventReg2BindingNavigator.ResumeLayout(False)
        Me.MainEventReg2BindingNavigator.PerformLayout()
        CType(Me.MainEventReg2BindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dsMainEventReg2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.pnlFinance.ResumeLayout(False)
        Me.pnlFinance.PerformLayout()
        CType(Me.StatusBarPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.StatusBarPanelID, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.StatusBarPanel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdOrder, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub




    Friend WithEvents MainEventReg2BindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents MainEventReg2BindingNavigator As System.Windows.Forms.BindingNavigator
    Friend WithEvents BindingNavigatorCountItem As System.Windows.Forms.ToolStripLabel
    Friend WithEvents BindingNavigatorMoveFirstItem As System.Windows.Forms.ToolStripButton
    Friend WithEvents BindingNavigatorMovePreviousItem As System.Windows.Forms.ToolStripButton
    Friend WithEvents BindingNavigatorSeparator As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents BindingNavigatorPositionItem As System.Windows.Forms.ToolStripTextBox
    Friend WithEvents BindingNavigatorSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents BindingNavigatorMoveNextItem As System.Windows.Forms.ToolStripButton
    Friend WithEvents BindingNavigatorMoveLastItem As System.Windows.Forms.ToolStripButton
    Friend WithEvents cboRegistrant As InfoCtr.ComboBoxRelaxed
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents btnSaveExit As System.Windows.Forms.Button
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents btnHelp As System.Windows.Forms.Button
    Friend WithEvents dsMainEventReg2 As InfoCtr.dsMainEventReg2
    ' Friend WithEvents MainEventReg2TableAdapter As InfoCtr.dsMainEventReg2TableAdapters.MainEventReg2TableAdapter
    Friend WithEvents MainMenu1 As System.Windows.Forms.MainMenu
    Friend WithEvents MenuItem1 As System.Windows.Forms.MenuItem
    Friend WithEvents miNewReg As System.Windows.Forms.MenuItem
    Friend WithEvents miClose As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem2 As System.Windows.Forms.MenuItem
    Friend WithEvents miSave As System.Windows.Forms.MenuItem
    Friend WithEvents miCancel As System.Windows.Forms.MenuItem
    Friend WithEvents miDelete As System.Windows.Forms.MenuItem
    Friend WithEvents cboEvent As InfoCtr.ComboBoxRelaxed
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents fldEventNum As System.Windows.Forms.TextBox
    Friend WithEvents fldContactNum As System.Windows.Forms.Label
    Friend WithEvents DtRegistration As InfoCtr.DateTextBox
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents MenuItem3 As System.Windows.Forms.MenuItem
    Friend WithEvents miMultiple As System.Windows.Forms.MenuItem
    Friend WithEvents miRefreshContacts As System.Windows.Forms.MenuItem
    Friend WithEvents miHelp As System.Windows.Forms.MenuItem
    Friend WithEvents TextBox4 As System.Windows.Forms.TextBox
    Friend WithEvents btnRefreshContacts As System.Windows.Forms.Button
    Friend WithEvents fldOrgNum As System.Windows.Forms.TextBox
    Friend WithEvents miCopy As System.Windows.Forms.MenuItem
    '  Friend WithEvents ErrorProvider1 As System.Windows.Forms.ErrorProvider
    ' Friend WithEvents RelRegOrderBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents GetEventOrderList2TableAdapter As InfoCtr.dsMainEventReg2TableAdapters.GetEventOrderList2TableAdapter
    'Friend WithEvents tPaymentTableAdapter As InfoCtr.dsMainEventReg2TableAdapters.tPaymentTableAdapter
    ' Friend WithEvents tblEventDDTableAdapter As InfoCtr.dsMainEventReg2TableAdapters.tblEventDDTableAdapter
    '  Friend WithEvents RelRegOrderBindingSource1 As System.Windows.Forms.BindingSource
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents miGotoOrder As System.Windows.Forms.MenuItem
    Friend WithEvents editIndFee As System.Windows.Forms.TextBox
    Friend WithEvents fldOrderNum As System.Windows.Forms.TextBox
    Friend WithEvents TextBox7 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox8 As System.Windows.Forms.TextBox
    Friend WithEvents btnLoadOrderGrid As System.Windows.Forms.Button
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents chkCancelled As System.Windows.Forms.CheckBox
    Friend WithEvents AttendedCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents chkRefund As System.Windows.Forms.CheckBox
    Friend WithEvents NametagCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents InformationCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents ConfirmationCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents tctExisting As System.Windows.Forms.TextBox
    Friend WithEvents datagridtablestyle1 As System.Windows.Forms.DataGridTableStyle
    Friend WithEvents grdOrder As System.Windows.Forms.DataGrid
    Friend WithEvents DataGridTextBoxColumn1 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn3 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn4 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn5 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn7 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn8 As System.Windows.Forms.DataGridTextBoxColumn

    Friend WithEvents txtPersonal As System.Windows.Forms.TextBox
    Friend WithEvents rtbReason As System.Windows.Forms.RichTextBox
    Friend WithEvents cboGender As InfoCtr.ComboBoxRelaxed
    Friend WithEvents chkVeg As System.Windows.Forms.CheckBox
    Friend WithEvents cboClergy As InfoCtr.ComboBoxRelaxed
    Friend WithEvents cboRegistered As InfoCtr.ComboBoxRelaxed
    Friend WithEvents cboHeard As InfoCtr.ComboBoxRelaxed
    Friend WithEvents rtbNotes As System.Windows.Forms.RichTextBox
    Friend WithEvents pnlFinance As System.Windows.Forms.Panel
    Friend WithEvents fldRefundAmount As System.Windows.Forms.TextBox
    Friend WithEvents fldRefundCheck As System.Windows.Forms.TextBox
    Friend WithEvents btnRequestRefund As System.Windows.Forms.Button
    Friend WithEvents ErrorProvider1 As System.Windows.Forms.ErrorProvider
    Friend WithEvents lblGotoRefund As System.Windows.Forms.Label

    Friend WithEvents DataGridTextBoxColumn2 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn6 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn9 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents TextBox9 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox5 As System.Windows.Forms.TextBox
    Friend WithEvents dtRefundRequest As System.Windows.Forms.TextBox
    Friend WithEvents MainEventReg2TableAdapter1 As InfoCtr.dsMainEventReg2TableAdapters.MainEventReg2TableAdapter
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents fldGotoOrder As System.Windows.Forms.TextBox
    Friend WithEvents ToolTip2 As System.Windows.Forms.ToolTip
    Friend WithEvents miGotoEvent As System.Windows.Forms.MenuItem
    Friend WithEvents StatusBar1 As System.Windows.Forms.StatusBar
    Friend WithEvents StatusBarPanel1 As System.Windows.Forms.StatusBarPanel
    Friend WithEvents StatusBarPanelID As System.Windows.Forms.StatusBarPanel
    Friend WithEvents StatusBarPanel2 As System.Windows.Forms.StatusBarPanel
    Friend WithEvents lblGotoContact As System.Windows.Forms.TextBox
    Friend WithEvents lblGotoEvent As System.Windows.Forms.Label
    Friend WithEvents fldPaymentNum As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label


End Class
