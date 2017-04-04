<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmNewWOrder2
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
        Dim FeePaidLabel As System.Windows.Forms.Label
        Dim PaymentMethodLabel As System.Windows.Forms.Label
        Dim RegistrationIDLabel As System.Windows.Forms.Label
        Dim EventNumLabel As System.Windows.Forms.Label
        Dim OrgNumLabel As System.Windows.Forms.Label
        Dim Label13 As System.Windows.Forms.Label
        Dim Label17 As System.Windows.Forms.Label
        Dim Label6 As System.Windows.Forms.Label
        Dim NotesLabel As System.Windows.Forms.Label
        Dim Label11 As System.Windows.Forms.Label
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmNewWOrder2))
        Me.lblevent = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.btnSaveExit = New System.Windows.Forms.Button()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.btnHelp = New System.Windows.Forms.Button()
        Me.btnCalc = New System.Windows.Forms.Button()
        Me.MainMenu1 = New System.Windows.Forms.MainMenu(Me.components)
        Me.MenuItem1 = New System.Windows.Forms.MenuItem()
        Me.miNew = New System.Windows.Forms.MenuItem()
        Me.miClose = New System.Windows.Forms.MenuItem()
        Me.MenuItem2 = New System.Windows.Forms.MenuItem()
        Me.miSave = New System.Windows.Forms.MenuItem()
        Me.miCancel = New System.Windows.Forms.MenuItem()
        Me.miDelete = New System.Windows.Forms.MenuItem()
        Me.miRefreshContacts = New System.Windows.Forms.MenuItem()
        Me.miCopy = New System.Windows.Forms.MenuItem()
        Me.MenuItem3 = New System.Windows.Forms.MenuItem()
        Me.miMultiple = New System.Windows.Forms.MenuItem()
        Me.miHelp = New System.Windows.Forms.MenuItem()
        Me.cboMethod = New InfoCtr.ComboBoxRelaxed()
        Me.cboEvent = New InfoCtr.ComboBoxRelaxed()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.fldRegistrationID = New System.Windows.Forms.TextBox()
        Me.fldEventNum = New System.Windows.Forms.TextBox()
        Me.fldContactNum = New System.Windows.Forms.TextBox()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.btnPopupGroup = New System.Windows.Forms.Button()
        Me.btnPopupPayee = New System.Windows.Forms.Button()
        Me.btnOrderNum = New System.Windows.Forms.Button()
        Me.FindOrder = New System.Windows.Forms.Button()
        Me.fldCalcAmount = New System.Windows.Forms.Label()
        Me.SqlConnection1 = New System.Data.SqlClient.SqlConnection()
        Me.fldEnteredBy = New System.Windows.Forms.TextBox()
        Me.fldOrgNum = New System.Windows.Forms.TextBox()
        Me.ErrorProvider1 = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.fldOrderNum = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.fldFee = New System.Windows.Forms.Label()
        Me.fldDiscount = New System.Windows.Forms.Label()
        Me.fldMin = New System.Windows.Forms.Label()
        Me.fldMax = New System.Windows.Forms.Label()
        Me.txtRegDate = New System.Windows.Forms.TextBox()
        Me.rtbNotes = New System.Windows.Forms.RichTextBox()
        Me.lblRegion = New System.Windows.Forms.Label()
        Me.txtAmount = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.StatusLabel1 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.txtDiscount = New System.Windows.Forms.TextBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.DsNewWRegistration = New InfoCtr.dsNewWRegistration()
        Me.TblNewWRegistrationBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.dgrd = New System.Windows.Forms.DataGrid()
        Me.DataGridTableStyle1 = New System.Windows.Forms.DataGridTableStyle()
        Me.DataGridTextBoxColumn6 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn8 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn9 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn11 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn12 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn1 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn2 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn3 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn4 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn5 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn10 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.TblNewWRegistrationTableAdapter = New InfoCtr.dsNewWRegistrationTableAdapters.tblNewWRegistrationTableAdapter()
        Me.tblEventDDTableAdapter = New InfoCtr.dsNewWRegistrationTableAdapters.tblEventDDTableAdapter()
        Me.tblRegOrderTableAdapter = New InfoCtr.dsNewWRegistrationTableAdapters.tblRegOrderTableAdapter()
        RegDateLabel = New System.Windows.Forms.Label()
        FeePaidLabel = New System.Windows.Forms.Label()
        PaymentMethodLabel = New System.Windows.Forms.Label()
        RegistrationIDLabel = New System.Windows.Forms.Label()
        EventNumLabel = New System.Windows.Forms.Label()
        OrgNumLabel = New System.Windows.Forms.Label()
        Label13 = New System.Windows.Forms.Label()
        Label17 = New System.Windows.Forms.Label()
        Label6 = New System.Windows.Forms.Label()
        NotesLabel = New System.Windows.Forms.Label()
        Label11 = New System.Windows.Forms.Label()
        Me.Panel2.SuspendLayout()
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.StatusStrip1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.DsNewWRegistration, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TblNewWRegistrationBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgrd, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RegDateLabel
        '
        RegDateLabel.AutoSize = True
        RegDateLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        RegDateLabel.Location = New System.Drawing.Point(54, 135)
        RegDateLabel.Name = "RegDateLabel"
        RegDateLabel.Size = New System.Drawing.Size(105, 15)
        RegDateLabel.TabIndex = 11
        RegDateLabel.Text = "Registration Date:"
        '
        'FeePaidLabel
        '
        FeePaidLabel.AutoSize = True
        FeePaidLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        FeePaidLabel.Location = New System.Drawing.Point(78, 164)
        FeePaidLabel.Name = "FeePaidLabel"
        FeePaidLabel.Size = New System.Drawing.Size(81, 15)
        FeePaidLabel.TabIndex = 13
        FeePaidLabel.Text = "Total Order $:"
        Me.ToolTip1.SetToolTip(FeePaidLabel, "this field will update when registrants are added.")
        '
        'PaymentMethodLabel
        '
        PaymentMethodLabel.AutoSize = True
        PaymentMethodLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        PaymentMethodLabel.Location = New System.Drawing.Point(56, 192)
        PaymentMethodLabel.Name = "PaymentMethodLabel"
        PaymentMethodLabel.Size = New System.Drawing.Size(103, 15)
        PaymentMethodLabel.TabIndex = 39
        PaymentMethodLabel.Text = "Payment Method:"
        '
        'RegistrationIDLabel
        '
        RegistrationIDLabel.AutoSize = True
        RegistrationIDLabel.Enabled = False
        RegistrationIDLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        RegistrationIDLabel.ForeColor = System.Drawing.SystemColors.ControlDark
        RegistrationIDLabel.Location = New System.Drawing.Point(819, 90)
        RegistrationIDLabel.Name = "RegistrationIDLabel"
        RegistrationIDLabel.Size = New System.Drawing.Size(79, 13)
        RegistrationIDLabel.TabIndex = 441
        RegistrationIDLabel.Text = "Registration ID:"
        '
        'EventNumLabel
        '
        EventNumLabel.AutoSize = True
        EventNumLabel.Enabled = False
        EventNumLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        EventNumLabel.ForeColor = System.Drawing.SystemColors.ControlDark
        EventNumLabel.Location = New System.Drawing.Point(836, 112)
        EventNumLabel.Name = "EventNumLabel"
        EventNumLabel.Size = New System.Drawing.Size(62, 13)
        EventNumLabel.TabIndex = 443
        EventNumLabel.Text = "Event Num:"
        '
        'OrgNumLabel
        '
        OrgNumLabel.AutoSize = True
        OrgNumLabel.Enabled = False
        OrgNumLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        OrgNumLabel.ForeColor = System.Drawing.SystemColors.ControlDark
        OrgNumLabel.Location = New System.Drawing.Point(847, 178)
        OrgNumLabel.Name = "OrgNumLabel"
        OrgNumLabel.Size = New System.Drawing.Size(51, 13)
        OrgNumLabel.TabIndex = 445
        OrgNumLabel.Text = "Org Num:"
        '
        'Label13
        '
        Label13.AutoSize = True
        Label13.Enabled = False
        Label13.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Label13.ForeColor = System.Drawing.SystemColors.ControlDark
        Label13.Location = New System.Drawing.Point(837, 156)
        Label13.Name = "Label13"
        Label13.Size = New System.Drawing.Size(61, 13)
        Label13.TabIndex = 465
        Label13.Text = "Entered by:"
        '
        'Label17
        '
        Label17.AutoSize = True
        Label17.Enabled = False
        Label17.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Label17.ForeColor = System.Drawing.SystemColors.ControlText
        Label17.Location = New System.Drawing.Point(108, 105)
        Label17.Name = "Label17"
        Label17.Size = New System.Drawing.Size(51, 15)
        Label17.TabIndex = 479
        Label17.Text = "Order #:"
        '
        'Label6
        '
        Label6.AutoSize = True
        Label6.Enabled = False
        Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Label6.ForeColor = System.Drawing.SystemColors.ControlDark
        Label6.Location = New System.Drawing.Point(828, 134)
        Label6.Name = "Label6"
        Label6.Size = New System.Drawing.Size(70, 13)
        Label6.TabIndex = 527
        Label6.Text = "Contact Num:"
        '
        'NotesLabel
        '
        NotesLabel.AutoSize = True
        NotesLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        NotesLabel.Location = New System.Drawing.Point(15, 388)
        NotesLabel.Name = "NotesLabel"
        NotesLabel.Size = New System.Drawing.Size(42, 15)
        NotesLabel.TabIndex = 27
        NotesLabel.Text = "Notes:"
        NotesLabel.Visible = False
        '
        'Label11
        '
        Label11.AutoSize = True
        Label11.Enabled = False
        Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Label11.Location = New System.Drawing.Point(338, 185)
        Label11.Name = "Label11"
        Label11.Size = New System.Drawing.Size(57, 26)
        Label11.TabIndex = 550
        Label11.Text = "Discount" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & " Applied $:"
        '
        'lblevent
        '
        Me.lblevent.AutoSize = True
        Me.lblevent.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblevent.ForeColor = System.Drawing.Color.DarkGreen
        Me.lblevent.Location = New System.Drawing.Point(12, 41)
        Me.lblevent.Name = "lblevent"
        Me.lblevent.Size = New System.Drawing.Size(48, 17)
        Me.lblevent.TabIndex = 5
        Me.lblevent.Text = "Event:"
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.SystemColors.Highlight
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Controls.Add(Me.btnSaveExit)
        Me.Panel2.Controls.Add(Me.btnDelete)
        Me.Panel2.Controls.Add(Me.btnHelp)
        Me.Panel2.Controls.Add(Me.btnCalc)
        Me.Panel2.Location = New System.Drawing.Point(840, 12)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(222, 42)
        Me.Panel2.TabIndex = 424
        '
        'btnSaveExit
        '
        Me.btnSaveExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSaveExit.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.btnSaveExit.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSaveExit.Image = CType(resources.GetObject("btnSaveExit.Image"), System.Drawing.Image)
        Me.btnSaveExit.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSaveExit.Location = New System.Drawing.Point(168, 1)
        Me.btnSaveExit.Name = "btnSaveExit"
        Me.btnSaveExit.Size = New System.Drawing.Size(51, 35)
        Me.btnSaveExit.TabIndex = 416
        Me.btnSaveExit.Text = "ACCEPT"
        Me.btnSaveExit.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSaveExit.UseVisualStyleBackColor = False
        '
        'btnDelete
        '
        Me.btnDelete.BackColor = System.Drawing.SystemColors.Control
        Me.btnDelete.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Image = CType(resources.GetObject("btnDelete.Image"), System.Drawing.Image)
        Me.btnDelete.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnDelete.Location = New System.Drawing.Point(2, 1)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(40, 35)
        Me.btnDelete.TabIndex = 247
        Me.btnDelete.TabStop = False
        Me.btnDelete.Text = "Delete"
        Me.btnDelete.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnDelete.UseVisualStyleBackColor = True
        '
        'btnHelp
        '
        Me.btnHelp.BackColor = System.Drawing.SystemColors.Control
        Me.btnHelp.Enabled = False
        Me.btnHelp.Font = New System.Drawing.Font("Microsoft Sans Serif", 22.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnHelp.ForeColor = System.Drawing.SystemColors.Control
        Me.btnHelp.Image = CType(resources.GetObject("btnHelp.Image"), System.Drawing.Image)
        Me.btnHelp.Location = New System.Drawing.Point(44, 1)
        Me.btnHelp.Name = "btnHelp"
        Me.btnHelp.Size = New System.Drawing.Size(25, 25)
        Me.btnHelp.TabIndex = 423
        Me.btnHelp.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnHelp.UseVisualStyleBackColor = False
        Me.btnHelp.Visible = False
        '
        'btnCalc
        '
        Me.btnCalc.Location = New System.Drawing.Point(101, 2)
        Me.btnCalc.Name = "btnCalc"
        Me.btnCalc.Size = New System.Drawing.Size(50, 35)
        Me.btnCalc.TabIndex = 541
        Me.btnCalc.Text = "Re- calc"
        Me.btnCalc.UseVisualStyleBackColor = True
        '
        'MainMenu1
        '
        Me.MainMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem1, Me.MenuItem2, Me.MenuItem3, Me.miHelp})
        '
        'MenuItem1
        '
        Me.MenuItem1.Index = 0
        Me.MenuItem1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.miNew, Me.miClose})
        Me.MenuItem1.Text = "File"
        '
        'miNew
        '
        Me.miNew.Index = 0
        Me.miNew.Text = "New Registration"
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
        Me.MenuItem3.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.miMultiple})
        Me.MenuItem3.Text = "View"
        '
        'miMultiple
        '
        Me.miMultiple.Index = 0
        Me.miMultiple.Text = "Series Registrations"
        '
        'miHelp
        '
        Me.miHelp.Index = 3
        Me.miHelp.Text = "Help"
        '
        'cboMethod
        '
        '  Me.cboMethod.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        '  Me.cboMethod.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboMethod.DataBindings.Add(New System.Windows.Forms.Binding("SelectedItem", Me.TblNewWRegistrationBindingSource, "PaymentMethod", True))
        Me.cboMethod.DropDownWidth = 300
        Me.cboMethod.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboMethod.FormattingEnabled = True
        Me.cboMethod.Items.AddRange(New Object() {"Payment for this order arranged by this Registrar via:", "-------------------------------------------------------", "Cash", "Check", "Credit Card", "Will pay at event", "Unpaid"})
        Me.cboMethod.Location = New System.Drawing.Point(164, 189)
        Me.cboMethod.Name = "cboMethod"
        Me.cboMethod.Size = New System.Drawing.Size(137, 24)
        Me.cboMethod.TabIndex = 3
        '
        'cboEvent
        '
        Me.cboEvent.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.TblNewWRegistrationBindingSource, "EventNum", True))
        Me.cboEvent.DisplayMember = "EventName"
        Me.cboEvent.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboEvent.FormattingEnabled = True
        Me.cboEvent.Location = New System.Drawing.Point(66, 38)
        Me.cboEvent.Name = "cboEvent"
        Me.cboEvent.Size = New System.Drawing.Size(582, 23)
        Me.cboEvent.TabIndex = 0
        Me.cboEvent.TabStop = False
        Me.cboEvent.ValueMember = "tblEventDD.EventID"
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.SystemColors.Control
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label8.Location = New System.Drawing.Point(12, 8)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(225, 15)
        Me.Label8.TabIndex = 429
        Me.Label8.Text = "ORDER DETAIL"
        '
        'fldRegistrationID
        '
        Me.fldRegistrationID.BackColor = System.Drawing.SystemColors.Control
        Me.fldRegistrationID.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.fldRegistrationID.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.TblNewWRegistrationBindingSource, "RegistrationID", True))
        Me.fldRegistrationID.Enabled = False
        Me.fldRegistrationID.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fldRegistrationID.Location = New System.Drawing.Point(904, 90)
        Me.fldRegistrationID.MinimumSize = New System.Drawing.Size(50, 0)
        Me.fldRegistrationID.Name = "fldRegistrationID"
        Me.fldRegistrationID.ReadOnly = True
        Me.fldRegistrationID.Size = New System.Drawing.Size(50, 13)
        Me.fldRegistrationID.TabIndex = 442
        '
        'fldEventNum
        '
        Me.fldEventNum.BackColor = System.Drawing.SystemColors.Control
        Me.fldEventNum.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.fldEventNum.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.TblNewWRegistrationBindingSource, "EventNum", True))
        Me.fldEventNum.Enabled = False
        Me.fldEventNum.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fldEventNum.Location = New System.Drawing.Point(904, 111)
        Me.fldEventNum.MinimumSize = New System.Drawing.Size(50, 0)
        Me.fldEventNum.Name = "fldEventNum"
        Me.fldEventNum.ReadOnly = True
        Me.fldEventNum.Size = New System.Drawing.Size(50, 13)
        Me.fldEventNum.TabIndex = 444
        '
        'fldContactNum
        '
        Me.fldContactNum.BackColor = System.Drawing.SystemColors.Control
        Me.fldContactNum.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.fldContactNum.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.TblNewWRegistrationBindingSource, "ContactNum", True))
        Me.fldContactNum.Enabled = False
        Me.fldContactNum.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fldContactNum.Location = New System.Drawing.Point(904, 132)
        Me.fldContactNum.Name = "fldContactNum"
        Me.fldContactNum.ReadOnly = True
        Me.fldContactNum.Size = New System.Drawing.Size(50, 13)
        Me.fldContactNum.TabIndex = 59
        '
        'btnPopupGroup
        '
        Me.btnPopupGroup.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPopupGroup.Location = New System.Drawing.Point(35, 333)
        Me.btnPopupGroup.Name = "btnPopupGroup"
        Me.btnPopupGroup.Size = New System.Drawing.Size(188, 39)
        Me.btnPopupGroup.TabIndex = 538
        Me.btnPopupGroup.Text = "Register Additional People"
        Me.ToolTip1.SetToolTip(Me.btnPopupGroup, "include people covered by this payment")
        Me.btnPopupGroup.UseVisualStyleBackColor = True
        Me.btnPopupGroup.Visible = False
        '
        'btnPopupPayee
        '
        Me.btnPopupPayee.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPopupPayee.Location = New System.Drawing.Point(35, 266)
        Me.btnPopupPayee.Name = "btnPopupPayee"
        Me.btnPopupPayee.Size = New System.Drawing.Size(138, 32)
        Me.btnPopupPayee.TabIndex = 537
        Me.btnPopupPayee.Text = "Register Payee"
        Me.ToolTip1.SetToolTip(Me.btnPopupPayee, "Payment information will be attached to this person.")
        Me.btnPopupPayee.UseVisualStyleBackColor = True
        '
        'btnOrderNum
        '
        Me.btnOrderNum.Location = New System.Drawing.Point(262, 104)
        Me.btnOrderNum.Margin = New System.Windows.Forms.Padding(0)
        Me.btnOrderNum.Name = "btnOrderNum"
        Me.btnOrderNum.Size = New System.Drawing.Size(40, 23)
        Me.btnOrderNum.TabIndex = 551
        Me.btnOrderNum.Text = "New"
        Me.ToolTip1.SetToolTip(Me.btnOrderNum, "Generate next order number not online.")
        Me.btnOrderNum.UseVisualStyleBackColor = True
        Me.btnOrderNum.Visible = False
        '
        'FindOrder
        '
        Me.FindOrder.Location = New System.Drawing.Point(719, 15)
        Me.FindOrder.Name = "FindOrder"
        Me.FindOrder.Size = New System.Drawing.Size(89, 33)
        Me.FindOrder.TabIndex = 554
        Me.FindOrder.Text = "Find Order"
        Me.FindOrder.UseVisualStyleBackColor = True
        Me.FindOrder.Visible = False
        '
        'fldCalcAmount
        '
        Me.fldCalcAmount.AutoSize = True
        Me.fldCalcAmount.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fldCalcAmount.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.fldCalcAmount.Location = New System.Drawing.Point(323, 142)
        Me.fldCalcAmount.MinimumSize = New System.Drawing.Size(150, 0)
        Me.fldCalcAmount.Name = "fldCalcAmount"
        Me.fldCalcAmount.Size = New System.Drawing.Size(150, 13)
        Me.fldCalcAmount.TabIndex = 488
        Me.fldCalcAmount.Text = "Calculated Amnt:     "
        Me.fldCalcAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'SqlConnection1
        '
        Me.SqlConnection1.ConnectionString = "Data Source=SOLOMON;Initial Catalog=InfoCtr_be;Integrated Security=True"
        Me.SqlConnection1.FireInfoMessageEventOnUserErrors = False
        '
        'fldEnteredBy
        '
        Me.fldEnteredBy.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.fldEnteredBy.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.TblNewWRegistrationBindingSource, "EnteredBy", True))
        Me.fldEnteredBy.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fldEnteredBy.Location = New System.Drawing.Point(904, 156)
        Me.fldEnteredBy.Name = "fldEnteredBy"
        Me.fldEnteredBy.ReadOnly = True
        Me.fldEnteredBy.Size = New System.Drawing.Size(50, 13)
        Me.fldEnteredBy.TabIndex = 463
        '
        'fldOrgNum
        '
        Me.fldOrgNum.BackColor = System.Drawing.SystemColors.Control
        Me.fldOrgNum.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.fldOrgNum.Enabled = False
        Me.fldOrgNum.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fldOrgNum.Location = New System.Drawing.Point(904, 175)
        Me.fldOrgNum.Name = "fldOrgNum"
        Me.fldOrgNum.ReadOnly = True
        Me.fldOrgNum.Size = New System.Drawing.Size(50, 13)
        Me.fldOrgNum.TabIndex = 478
        '
        'ErrorProvider1
        '
        Me.ErrorProvider1.ContainerControl = Me
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.Highlight
        Me.Label5.Location = New System.Drawing.Point(5, 315)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(316, 15)
        Me.Label5.TabIndex = 493
        Me.Label5.Text = "3.  ADDITIONAL REGISTRATIONS in this ORDER"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.Highlight
        Me.Label2.Location = New System.Drawing.Point(5, 248)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(199, 15)
        Me.Label2.TabIndex = 497
        Me.Label2.Text = "2.  PERSON PLACING ORDER"
        '
        'fldOrderNum
        '
        Me.fldOrderNum.BackColor = System.Drawing.SystemColors.Window
        Me.fldOrderNum.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.TblNewWRegistrationBindingSource, "OrderNum", True))
        Me.fldOrderNum.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fldOrderNum.Location = New System.Drawing.Point(164, 105)
        Me.fldOrderNum.Name = "fldOrderNum"
        Me.fldOrderNum.Size = New System.Drawing.Size(101, 22)
        Me.fldOrderNum.TabIndex = 0
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.SystemColors.Highlight
        Me.Label7.Location = New System.Drawing.Point(8, 77)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(223, 15)
        Me.Label7.TabIndex = 529
        Me.Label7.Text = "1.  ORDER SETUP INFORMATION"
        '
        'fldFee
        '
        Me.fldFee.AutoSize = True
        Me.fldFee.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsNewWRegistration, "tblEventDD.Fee", True))
        Me.fldFee.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.fldFee.Location = New System.Drawing.Point(92, 40)
        Me.fldFee.Name = "fldFee"
        Me.fldFee.Size = New System.Drawing.Size(19, 13)
        Me.fldFee.TabIndex = 530
        Me.fldFee.Text = "30"
        Me.fldFee.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'fldDiscount
        '
        Me.fldDiscount.AutoSize = True
        Me.fldDiscount.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.fldDiscount.Location = New System.Drawing.Point(92, 57)
        Me.fldDiscount.Name = "fldDiscount"
        Me.fldDiscount.Size = New System.Drawing.Size(13, 13)
        Me.fldDiscount.TabIndex = 531
        Me.fldDiscount.Text = "5"
        Me.fldDiscount.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'fldMin
        '
        Me.fldMin.AutoSize = True
        Me.fldMin.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsNewWRegistration, "tblEventDD.TeamMin", True))
        Me.fldMin.ForeColor = System.Drawing.SystemColors.ControlDark
        Me.fldMin.Location = New System.Drawing.Point(1000, 175)
        Me.fldMin.Name = "fldMin"
        Me.fldMin.Size = New System.Drawing.Size(102, 13)
        Me.fldMin.TabIndex = 532
        Me.fldMin.Text = "DiscountMinimum: 3"
        '
        'fldMax
        '
        Me.fldMax.AutoSize = True
        Me.fldMax.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsNewWRegistration, "tblEventDD.MaximumSeating", True))
        Me.fldMax.ForeColor = System.Drawing.SystemColors.ControlDark
        Me.fldMax.Location = New System.Drawing.Point(1006, 195)
        Me.fldMax.Name = "fldMax"
        Me.fldMax.Size = New System.Drawing.Size(63, 13)
        Me.fldMax.TabIndex = 533
        Me.fldMax.Text = "MaxSeating"
        '
        'txtRegDate
        '
        Me.txtRegDate.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.TblNewWRegistrationBindingSource, "RegDate", True, DataSourceUpdateMode.OnValidation, ""))
        Me.txtRegDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRegDate.Location = New System.Drawing.Point(164, 132)
        Me.txtRegDate.Name = "txtRegDate"
        Me.txtRegDate.Size = New System.Drawing.Size(137, 23)
        Me.txtRegDate.TabIndex = 1
        '
        'rtbNotes
        '
        Me.rtbNotes.Location = New System.Drawing.Point(15, 406)
        Me.rtbNotes.Name = "rtbNotes"
        Me.rtbNotes.Size = New System.Drawing.Size(309, 124)
        Me.rtbNotes.TabIndex = 7
        Me.rtbNotes.Text = ""
        Me.rtbNotes.Visible = False
        '
        'lblRegion
        '
        Me.lblRegion.AutoSize = True
        Me.lblRegion.ForeColor = System.Drawing.SystemColors.ControlDark
        Me.lblRegion.Location = New System.Drawing.Point(836, 71)
        Me.lblRegion.Name = "lblRegion"
        Me.lblRegion.Size = New System.Drawing.Size(41, 13)
        Me.lblRegion.TabIndex = 544
        Me.lblRegion.Text = "Region"
        '
        'txtAmount
        '
        Me.txtAmount.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.TblNewWRegistrationBindingSource, "AmountTotOrder", True, DataSourceUpdateMode.OnValidation, ""))
        Me.txtAmount.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAmount.Location = New System.Drawing.Point(164, 161)
        Me.txtAmount.Name = "txtAmount"
        Me.txtAmount.Size = New System.Drawing.Size(136, 23)
        Me.txtAmount.TabIndex = 2
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.Label3.Location = New System.Drawing.Point(41, 39)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(28, 13)
        Me.Label3.TabIndex = 546
        Me.Label3.Text = "Fee:"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.Label9.Location = New System.Drawing.Point(18, 57)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(52, 13)
        Me.Label9.TabIndex = 547
        Me.Label9.Text = "Discount:"
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.StatusLabel1})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 541)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(1120, 22)
        Me.StatusStrip1.TabIndex = 548
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'StatusLabel1
        '
        Me.StatusLabel1.Name = "StatusLabel1"
        Me.StatusLabel1.Size = New System.Drawing.Size(121, 17)
        Me.StatusLabel1.Text = "ToolStripStatusLabel1"
        '
        'txtDiscount
        '
        Me.txtDiscount.AcceptsReturn = True
        Me.txtDiscount.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.TblNewWRegistrationBindingSource, "AmountDiscount", True))
        Me.txtDiscount.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDiscount.Location = New System.Drawing.Point(410, 188)
        Me.txtDiscount.Name = "txtDiscount"
        Me.txtDiscount.ReadOnly = True
        Me.txtDiscount.Size = New System.Drawing.Size(42, 23)
        Me.txtDiscount.TabIndex = 549
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Label12)
        Me.Panel1.Controls.Add(Me.Label9)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.fldDiscount)
        Me.Panel1.Controls.Add(Me.fldFee)
        Me.Panel1.Location = New System.Drawing.Point(339, 78)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(149, 77)
        Me.Panel1.TabIndex = 555
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.Label12.Location = New System.Drawing.Point(15, 2)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(119, 13)
        Me.Label12.TabIndex = 548
        Me.Label12.Text = "FYI Calculated amounts"
        '
        'DsNewWRegistration
        '
        Me.DsNewWRegistration.DataSetName = "dsNewWRegistration"
        Me.DsNewWRegistration.EnforceConstraints = False
        Me.DsNewWRegistration.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'TblNewWRegistrationBindingSource
        '
        Me.TblNewWRegistrationBindingSource.DataMember = "tblNewWRegistration"
        Me.TblNewWRegistrationBindingSource.DataSource = Me.DsNewWRegistration
        '
        'dgrd
        '
        Me.dgrd.CaptionFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.dgrd.CaptionText = "Registrations in this Payment"
        Me.dgrd.DataMember = "tblRegOrder"
        Me.dgrd.DataSource = Me.DsNewWRegistration
        Me.dgrd.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.dgrd.Location = New System.Drawing.Point(410, 261)
        Me.dgrd.Name = "dgrd"
        Me.dgrd.ReadOnly = True
        Me.dgrd.RowHeaderWidth = 15
        Me.dgrd.Size = New System.Drawing.Size(625, 311)
        Me.dgrd.TabIndex = 499
        Me.dgrd.TableStyles.AddRange(New System.Windows.Forms.DataGridTableStyle() {Me.DataGridTableStyle1})
        '
        'DataGridTableStyle1
        '
        Me.DataGridTableStyle1.DataGrid = Me.dgrd
        Me.DataGridTableStyle1.GridColumnStyles.AddRange(New System.Windows.Forms.DataGridColumnStyle() {Me.DataGridTextBoxColumn6, Me.DataGridTextBoxColumn8, Me.DataGridTextBoxColumn9, Me.DataGridTextBoxColumn11, Me.DataGridTextBoxColumn12, Me.DataGridTextBoxColumn1, Me.DataGridTextBoxColumn2, Me.DataGridTextBoxColumn3, Me.DataGridTextBoxColumn4, Me.DataGridTextBoxColumn5, Me.DataGridTextBoxColumn10})
        Me.DataGridTableStyle1.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.DataGridTableStyle1.MappingName = "tblRegOrder"
        Me.DataGridTableStyle1.RowHeaderWidth = 15
        '
        'DataGridTextBoxColumn6
        '
        Me.DataGridTextBoxColumn6.Format = ""
        Me.DataGridTextBoxColumn6.FormatInfo = Nothing
        Me.DataGridTextBoxColumn6.HeaderText = "RegID"
        Me.DataGridTextBoxColumn6.MappingName = "RegistrationID"
        Me.DataGridTextBoxColumn6.Width = 0
        '
        'DataGridTextBoxColumn8
        '
        Me.DataGridTextBoxColumn8.Format = ""
        Me.DataGridTextBoxColumn8.FormatInfo = Nothing
        Me.DataGridTextBoxColumn8.HeaderText = "OrderNum"
        Me.DataGridTextBoxColumn8.MappingName = "OrderNum"
        Me.DataGridTextBoxColumn8.Width = 0
        '
        'DataGridTextBoxColumn9
        '
        Me.DataGridTextBoxColumn9.Format = ""
        Me.DataGridTextBoxColumn9.FormatInfo = Nothing
        Me.DataGridTextBoxColumn9.HeaderText = "ContactNum"
        Me.DataGridTextBoxColumn9.MappingName = "ContactNum"
        Me.DataGridTextBoxColumn9.Width = 0
        '
        'DataGridTextBoxColumn11
        '
        Me.DataGridTextBoxColumn11.Format = ""
        Me.DataGridTextBoxColumn11.FormatInfo = Nothing
        Me.DataGridTextBoxColumn11.HeaderText = "Status"
        Me.DataGridTextBoxColumn11.MappingName = "Registrar"
        Me.DataGridTextBoxColumn11.Width = 75
        '
        'DataGridTextBoxColumn12
        '
        Me.DataGridTextBoxColumn12.Format = ""
        Me.DataGridTextBoxColumn12.FormatInfo = Nothing
        Me.DataGridTextBoxColumn12.HeaderText = "Order Total"
        Me.DataGridTextBoxColumn12.MappingName = "OrderTotal"
        Me.DataGridTextBoxColumn12.Width = 75
        '
        'DataGridTextBoxColumn1
        '
        Me.DataGridTextBoxColumn1.Format = ""
        Me.DataGridTextBoxColumn1.FormatInfo = Nothing
        Me.DataGridTextBoxColumn1.HeaderText = "FirstName"
        Me.DataGridTextBoxColumn1.MappingName = "FirstName"
        Me.DataGridTextBoxColumn1.ReadOnly = True
        Me.DataGridTextBoxColumn1.Width = 75
        '
        'DataGridTextBoxColumn2
        '
        Me.DataGridTextBoxColumn2.Format = ""
        Me.DataGridTextBoxColumn2.FormatInfo = Nothing
        Me.DataGridTextBoxColumn2.HeaderText = "Lastname"
        Me.DataGridTextBoxColumn2.MappingName = "Lastname"
        Me.DataGridTextBoxColumn2.Width = 75
        '
        'DataGridTextBoxColumn3
        '
        Me.DataGridTextBoxColumn3.Format = ""
        Me.DataGridTextBoxColumn3.FormatInfo = Nothing
        Me.DataGridTextBoxColumn3.HeaderText = "Email"
        Me.DataGridTextBoxColumn3.MappingName = "Email"
        Me.DataGridTextBoxColumn3.Width = 75
        '
        'DataGridTextBoxColumn4
        '
        Me.DataGridTextBoxColumn4.Format = ""
        Me.DataGridTextBoxColumn4.FormatInfo = Nothing
        Me.DataGridTextBoxColumn4.HeaderText = "MethodPay"
        Me.DataGridTextBoxColumn4.MappingName = "PaymentMethod"
        Me.DataGridTextBoxColumn4.Width = 75
        '
        'DataGridTextBoxColumn5
        '
        Me.DataGridTextBoxColumn5.Format = ""
        Me.DataGridTextBoxColumn5.FormatInfo = Nothing
        Me.DataGridTextBoxColumn5.HeaderText = "Organization"
        Me.DataGridTextBoxColumn5.MappingName = "OrgName"
        Me.DataGridTextBoxColumn5.Width = 150
        '
        'DataGridTextBoxColumn10
        '
        Me.DataGridTextBoxColumn10.Format = ""
        Me.DataGridTextBoxColumn10.FormatInfo = Nothing
        Me.DataGridTextBoxColumn10.HeaderText = "EventName"
        Me.DataGridTextBoxColumn10.MappingName = "EventName"
        Me.DataGridTextBoxColumn10.Width = 150
        '
        'TblNewWRegistrationTableAdapter
        '
        Me.TblNewWRegistrationTableAdapter.ClearBeforeFill = True
        '
        'tblEventDDTableAdapter
        '
        Me.tblEventDDTableAdapter.ClearBeforeFill = True
        '
        'tblRegOrderTableAdapter
        '
        Me.tblRegOrderTableAdapter.ClearBeforeFill = True
        '
        'frmNewWOrder2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1120, 563)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.FindOrder)
        Me.Controls.Add(Me.btnOrderNum)
        Me.Controls.Add(Label11)
        Me.Controls.Add(Me.txtDiscount)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.txtAmount)
        Me.Controls.Add(Me.lblRegion)
        Me.Controls.Add(Me.btnPopupGroup)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.btnPopupPayee)
        Me.Controls.Add(Me.txtRegDate)
        Me.Controls.Add(NotesLabel)
        Me.Controls.Add(Me.fldMax)
        Me.Controls.Add(Me.fldMin)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.fldOrderNum)
        Me.Controls.Add(Label6)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.fldCalcAmount)
        Me.Controls.Add(Label17)
        Me.Controls.Add(Me.fldOrgNum)
        Me.Controls.Add(Label13)
        Me.Controls.Add(Me.fldEnteredBy)
        Me.Controls.Add(Me.lblevent)
        Me.Controls.Add(RegistrationIDLabel)
        Me.Controls.Add(Me.fldRegistrationID)
        Me.Controls.Add(EventNumLabel)
        Me.Controls.Add(Me.fldEventNum)
        Me.Controls.Add(OrgNumLabel)
        Me.Controls.Add(Me.fldContactNum)
        Me.Controls.Add(Me.rtbNotes)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.cboEvent)
        Me.Controls.Add(Me.cboMethod)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(RegDateLabel)
        Me.Controls.Add(FeePaidLabel)
        Me.Controls.Add(PaymentMethodLabel)
        Me.Controls.Add(Me.dgrd)
        Me.Menu = Me.MainMenu1
        Me.Name = "frmNewWOrder2"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Tag = "NEWORDER"
        Me.Text = "ORDER DETAIL"
        Me.ToolTip1.SetToolTip(Me, "generate a new order number")
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.Panel2.ResumeLayout(False)
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.DsNewWRegistration, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TblNewWRegistrationBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgrd, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents dgrd As System.Windows.Forms.DataGrid
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents btnSaveExit As System.Windows.Forms.Button
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents btnHelp As System.Windows.Forms.Button
    Friend WithEvents MainMenu1 As System.Windows.Forms.MainMenu
    Friend WithEvents MenuItem1 As System.Windows.Forms.MenuItem
    Friend WithEvents miNew As System.Windows.Forms.MenuItem
    Friend WithEvents miClose As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem2 As System.Windows.Forms.MenuItem
    Friend WithEvents miSave As System.Windows.Forms.MenuItem
    Friend WithEvents miCancel As System.Windows.Forms.MenuItem
    Friend WithEvents miDelete As System.Windows.Forms.MenuItem
    Friend WithEvents cboMethod As InfoCtr.ComboBoxRelaxed
    Friend WithEvents cboEvent As InfoCtr.ComboBoxRelaxed
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents lblevent As System.Windows.Forms.Label
    Friend WithEvents fldRegistrationID As System.Windows.Forms.TextBox
    Friend WithEvents fldEventNum As System.Windows.Forms.TextBox
    Friend WithEvents fldContactNum As System.Windows.Forms.TextBox
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents MenuItem3 As System.Windows.Forms.MenuItem
    Friend WithEvents miMultiple As System.Windows.Forms.MenuItem
    Friend WithEvents SqlConnection1 As System.Data.SqlClient.SqlConnection
    Friend WithEvents miRefreshContacts As System.Windows.Forms.MenuItem
    Friend WithEvents miHelp As System.Windows.Forms.MenuItem
    Friend WithEvents fldEnteredBy As System.Windows.Forms.TextBox
    Friend WithEvents fldOrgNum As System.Windows.Forms.TextBox
    Friend WithEvents miCopy As System.Windows.Forms.MenuItem
    Friend WithEvents ErrorProvider1 As System.Windows.Forms.ErrorProvider
    Friend WithEvents fldCalcAmount As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents DataGridTableStyle1 As System.Windows.Forms.DataGridTableStyle
    Friend WithEvents DataGridTextBoxColumn1 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents TblNewWRegistrationBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents DsNewWRegistration As InfoCtr.dsNewWRegistration
    Friend WithEvents TblNewWRegistrationTableAdapter As InfoCtr.dsNewWRegistrationTableAdapters.tblNewWRegistrationTableAdapter
    Friend WithEvents fldOrderNum As System.Windows.Forms.TextBox
    Friend WithEvents tblEventDDTableAdapter As InfoCtr.dsNewWRegistrationTableAdapters.tblEventDDTableAdapter
    Friend WithEvents tblRegOrderTableAdapter As InfoCtr.dsNewWRegistrationTableAdapters.tblRegOrderTableAdapter

    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents fldMax As System.Windows.Forms.Label
    Friend WithEvents fldMin As System.Windows.Forms.Label
    Friend WithEvents fldDiscount As System.Windows.Forms.Label
    Friend WithEvents fldFee As System.Windows.Forms.Label
    Friend WithEvents DataGridTextBoxColumn2 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn3 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn4 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn5 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents txtRegDate As System.Windows.Forms.TextBox
    Friend WithEvents btnPopupGroup As System.Windows.Forms.Button
    Friend WithEvents btnPopupPayee As System.Windows.Forms.Button
    Friend WithEvents btnCalc As System.Windows.Forms.Button
    Friend WithEvents DataGridTextBoxColumn6 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn8 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn9 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents rtbNotes As System.Windows.Forms.RichTextBox
    Friend WithEvents lblRegion As System.Windows.Forms.Label
    Friend WithEvents txtAmount As System.Windows.Forms.TextBox
    Friend WithEvents DataGridTextBoxColumn10 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents DataGridTextBoxColumn11 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn12 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents StatusLabel1 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents txtDiscount As System.Windows.Forms.TextBox
    Friend WithEvents btnOrderNum As System.Windows.Forms.Button
    Friend WithEvents FindOrder As System.Windows.Forms.Button
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label12 As System.Windows.Forms.Label
    '  Friend WithEvents DsContacts As WindowsApplication11.dsContacts
End Class
