<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMainWPayment
    Inherits System.Windows.Forms.Form



    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim PaymentIDLabel As System.Windows.Forms.Label
        Dim DtPaymentLabel As System.Windows.Forms.Label
        Dim PaymentTypeLabel As System.Windows.Forms.Label
        Dim AmountPaymentLabel As System.Windows.Forms.Label
        Dim PayerNameLabel As System.Windows.Forms.Label
        Dim DtDepositLabel As System.Windows.Forms.Label
        Dim PaymentStaffNumLabel As System.Windows.Forms.Label
        Dim PaymentNoteLabel As System.Windows.Forms.Label
        Dim RegistrarRegNumLabel As System.Windows.Forms.Label
        Dim Label5 As System.Windows.Forms.Label
        Dim Label4 As System.Windows.Forms.Label
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMainWPayment))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.RegistrarSIDLabel = New System.Windows.Forms.Label()
        Me.BindingNavigatorAddNewItem = New System.Windows.Forms.ToolStripButton()
        Me.BindingNavigatorCountItem = New System.Windows.Forms.ToolStripLabel()
        Me.BindingNavigatorDeleteItem = New System.Windows.Forms.ToolStripButton()
        Me.BindingNavigatorMoveFirstItem = New System.Windows.Forms.ToolStripButton()
        Me.BindingNavigatorMovePreviousItem = New System.Windows.Forms.ToolStripButton()
        Me.BindingNavigatorSeparator = New System.Windows.Forms.ToolStripSeparator()
        Me.BindingNavigatorPositionItem = New System.Windows.Forms.ToolStripTextBox()
        Me.BindingNavigatorSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.BindingNavigatorMoveNextItem = New System.Windows.Forms.ToolStripButton()
        Me.BindingNavigatorMoveLastItem = New System.Windows.Forms.ToolStripButton()
        Me.BindingNavigatorSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.MainEventPaymentBindingNavigatorSaveItem = New System.Windows.Forms.ToolStripButton()
        Me.fldSubmissionID = New System.Windows.Forms.TextBox()
        Me.cboPaymentType = New InfoCtr.ComboBoxRelaxed()
        Me.MainEventPaymentBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.DsMainWPayment = New InfoCtr.dsMainWPayment()
        Me.cboPaymentMethod = New InfoCtr.ComboBoxRelaxed()
        Me.AmountPaymentTextBox = New System.Windows.Forms.TextBox()
        Me.PayerNameTextBox = New System.Windows.Forms.TextBox()
        Me.TrackingNumTextBox = New System.Windows.Forms.TextBox()
        Me.PaymentStaffNumTextBox = New System.Windows.Forms.TextBox()
        Me.txtNotes = New System.Windows.Forms.TextBox()
        Me.RegistrationRegNumTextBox = New System.Windows.Forms.TextBox()
        Me.RegistrarSIDTextBox = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.pnlPaymentDetail = New System.Windows.Forms.Panel()
        Me.lblTrackingNum = New System.Windows.Forms.Label()
        Me.fldPaymentMethod = New System.Windows.Forms.TextBox()
        Me.DtPayment = New InfoCtr.DateTextBox()
        Me.fldPayID = New System.Windows.Forms.TextBox()
        Me.btnSaveExit = New System.Windows.Forms.Button()
        Me.MainMenu1 = New System.Windows.Forms.MainMenu(Me.components)
        Me.MenuItem1 = New System.Windows.Forms.MenuItem()
        Me.miNew = New System.Windows.Forms.MenuItem()
        Me.miClose = New System.Windows.Forms.MenuItem()
        Me.MenuItem3 = New System.Windows.Forms.MenuItem()
        Me.miSave = New System.Windows.Forms.MenuItem()
        Me.miCancel = New System.Windows.Forms.MenuItem()
        Me.miDelete = New System.Windows.Forms.MenuItem()
        Me.MenuItem7 = New System.Windows.Forms.MenuItem()
        Me.miStaffRpt = New System.Windows.Forms.MenuItem()
        Me.miHelp = New System.Windows.Forms.MenuItem()
        Me.fldOrderID = New System.Windows.Forms.TextBox()
        Me.txtSrchID = New System.Windows.Forms.TextBox()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.btnAddReg = New System.Windows.Forms.Button()
        Me.pnlGridRegs = New System.Windows.Forms.Panel()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.OrderNumDataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.LfNameDataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.OrgNameDataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewCheckBoxColumn1 = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.DataGridViewCheckBoxColumn2 = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.EventNameDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dsGridPaymentRegs = New InfoCtr.dsMainEventPaymentRegs()
        Me.RegIDDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.OrderNumDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.LfNameDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.OrgNameDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ErrorProvider1 = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.GridPaymentRegsTableAdapter = New InfoCtr.dsMainEventPaymentRegsTableAdapters.GridPaymentRegsTableAdapter()
        Me.MainEventPaymentTableAdapter = New InfoCtr.dsMainWPaymentTableAdapters.taMainRegistrPayment()
        Me.TableAdapterManager = New InfoCtr.dsMainWPaymentTableAdapters.TableAdapterManager()
        PaymentIDLabel = New System.Windows.Forms.Label()
        DtPaymentLabel = New System.Windows.Forms.Label()
        PaymentTypeLabel = New System.Windows.Forms.Label()
        AmountPaymentLabel = New System.Windows.Forms.Label()
        PayerNameLabel = New System.Windows.Forms.Label()
        DtDepositLabel = New System.Windows.Forms.Label()
        PaymentStaffNumLabel = New System.Windows.Forms.Label()
        PaymentNoteLabel = New System.Windows.Forms.Label()
        RegistrarRegNumLabel = New System.Windows.Forms.Label()
        Label5 = New System.Windows.Forms.Label()
        Label4 = New System.Windows.Forms.Label()
        CType(Me.MainEventPaymentBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsMainWPayment, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlPaymentDetail.SuspendLayout()
        Me.pnlGridRegs.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dsGridPaymentRegs, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PaymentIDLabel
        '
        PaymentIDLabel.AutoSize = True
        PaymentIDLabel.Location = New System.Drawing.Point(890, 514)
        PaymentIDLabel.Name = "PaymentIDLabel"
        PaymentIDLabel.Size = New System.Drawing.Size(73, 13)
        PaymentIDLabel.TabIndex = 2
        PaymentIDLabel.Text = "Submission #:"
        '
        'DtPaymentLabel
        '
        DtPaymentLabel.AutoSize = True
        DtPaymentLabel.Location = New System.Drawing.Point(87, 33)
        DtPaymentLabel.Name = "DtPaymentLabel"
        DtPaymentLabel.Size = New System.Drawing.Size(33, 13)
        DtPaymentLabel.TabIndex = 6
        DtPaymentLabel.Text = "Date:"
        '
        'PaymentTypeLabel
        '
        PaymentTypeLabel.AutoSize = True
        PaymentTypeLabel.Location = New System.Drawing.Point(30, 7)
        PaymentTypeLabel.Name = "PaymentTypeLabel"
        PaymentTypeLabel.Size = New System.Drawing.Size(93, 13)
        PaymentTypeLabel.TabIndex = 8
        PaymentTypeLabel.Text = "Transaction Type:"
        '
        'AmountPaymentLabel
        '
        AmountPaymentLabel.AutoSize = True
        AmountPaymentLabel.Location = New System.Drawing.Point(65, 55)
        AmountPaymentLabel.Name = "AmountPaymentLabel"
        AmountPaymentLabel.Size = New System.Drawing.Size(55, 13)
        AmountPaymentLabel.TabIndex = 12
        AmountPaymentLabel.Text = "Amount $:"
        '
        'PayerNameLabel
        '
        PayerNameLabel.AutoSize = True
        PayerNameLabel.Location = New System.Drawing.Point(378, 107)
        PayerNameLabel.Name = "PayerNameLabel"
        PayerNameLabel.Size = New System.Drawing.Size(132, 26)
        PayerNameLabel.TabIndex = 14
        PayerNameLabel.Text = "Optional: Name on Check:" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "or 'Billed To' if CreditCard"
        '
        'DtDepositLabel
        '
        DtDepositLabel.AutoSize = True
        DtDepositLabel.Location = New System.Drawing.Point(375, 12)
        DtDepositLabel.Name = "DtDepositLabel"
        DtDepositLabel.Size = New System.Drawing.Size(135, 13)
        DtDepositLabel.TabIndex = 18
        DtDepositLabel.Text = "Check/Cash Deposit Date:"
        '
        'PaymentStaffNumLabel
        '
        PaymentStaffNumLabel.AutoSize = True
        PaymentStaffNumLabel.Location = New System.Drawing.Point(400, 518)
        PaymentStaffNumLabel.Name = "PaymentStaffNumLabel"
        PaymentStaffNumLabel.Size = New System.Drawing.Size(101, 13)
        PaymentStaffNumLabel.TabIndex = 22
        PaymentStaffNumLabel.Text = "Payment Staff Num:"
        '
        'PaymentNoteLabel
        '
        PaymentNoteLabel.AutoSize = True
        PaymentNoteLabel.Location = New System.Drawing.Point(428, 39)
        PaymentNoteLabel.Name = "PaymentNoteLabel"
        PaymentNoteLabel.Size = New System.Drawing.Size(77, 13)
        PaymentNoteLabel.TabIndex = 24
        PaymentNoteLabel.Text = "Payment Note:"
        '
        'RegistrarRegNumLabel
        '
        RegistrarRegNumLabel.AutoSize = True
        RegistrarRegNumLabel.Location = New System.Drawing.Point(198, 518)
        RegistrarRegNumLabel.Name = "RegistrarRegNumLabel"
        RegistrarRegNumLabel.Size = New System.Drawing.Size(100, 13)
        RegistrarRegNumLabel.TabIndex = 26
        RegistrarRegNumLabel.Text = "Registrar Reg Num:"
        '
        'Label5
        '
        Label5.AutoSize = True
        Label5.Location = New System.Drawing.Point(738, 517)
        Label5.Name = "Label5"
        Label5.Size = New System.Drawing.Size(65, 13)
        Label5.TabIndex = 39
        Label5.Text = "Payment ID:"
        '
        'Label4
        '
        Label4.AutoSize = True
        Label4.Location = New System.Drawing.Point(589, 517)
        Label4.Name = "Label4"
        Label4.Size = New System.Drawing.Size(50, 13)
        Label4.TabIndex = 787
        Label4.Text = "Order ID:"
        '
        'RegistrarSIDLabel
        '
        Me.RegistrarSIDLabel.AutoSize = True
        Me.RegistrarSIDLabel.Location = New System.Drawing.Point(26, 517)
        Me.RegistrarSIDLabel.Name = "RegistrarSIDLabel"
        Me.RegistrarSIDLabel.Size = New System.Drawing.Size(73, 13)
        Me.RegistrarSIDLabel.TabIndex = 30
        Me.RegistrarSIDLabel.Text = "Registrar SID:"
        '
        'BindingNavigatorAddNewItem
        '
        Me.BindingNavigatorAddNewItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorAddNewItem.Image = CType(resources.GetObject("BindingNavigatorAddNewItem.Image"), System.Drawing.Image)
        Me.BindingNavigatorAddNewItem.Name = "BindingNavigatorAddNewItem"
        Me.BindingNavigatorAddNewItem.RightToLeftAutoMirrorImage = True
        Me.BindingNavigatorAddNewItem.Size = New System.Drawing.Size(23, 22)
        Me.BindingNavigatorAddNewItem.Text = "Add new"
        '
        'BindingNavigatorCountItem
        '
        Me.BindingNavigatorCountItem.Name = "BindingNavigatorCountItem"
        Me.BindingNavigatorCountItem.Size = New System.Drawing.Size(35, 22)
        Me.BindingNavigatorCountItem.Text = "of {0}"
        Me.BindingNavigatorCountItem.ToolTipText = "Total number of items"
        '
        'BindingNavigatorDeleteItem
        '
        Me.BindingNavigatorDeleteItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorDeleteItem.Image = CType(resources.GetObject("BindingNavigatorDeleteItem.Image"), System.Drawing.Image)
        Me.BindingNavigatorDeleteItem.Name = "BindingNavigatorDeleteItem"
        Me.BindingNavigatorDeleteItem.RightToLeftAutoMirrorImage = True
        Me.BindingNavigatorDeleteItem.Size = New System.Drawing.Size(23, 22)
        Me.BindingNavigatorDeleteItem.Text = "Delete"
        '
        'BindingNavigatorMoveFirstItem
        '
        Me.BindingNavigatorMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorMoveFirstItem.Image = CType(resources.GetObject("BindingNavigatorMoveFirstItem.Image"), System.Drawing.Image)
        Me.BindingNavigatorMoveFirstItem.Name = "BindingNavigatorMoveFirstItem"
        Me.BindingNavigatorMoveFirstItem.RightToLeftAutoMirrorImage = True
        Me.BindingNavigatorMoveFirstItem.Size = New System.Drawing.Size(23, 22)
        Me.BindingNavigatorMoveFirstItem.Text = "Move first"
        '
        'BindingNavigatorMovePreviousItem
        '
        Me.BindingNavigatorMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorMovePreviousItem.Image = CType(resources.GetObject("BindingNavigatorMovePreviousItem.Image"), System.Drawing.Image)
        Me.BindingNavigatorMovePreviousItem.Name = "BindingNavigatorMovePreviousItem"
        Me.BindingNavigatorMovePreviousItem.RightToLeftAutoMirrorImage = True
        Me.BindingNavigatorMovePreviousItem.Size = New System.Drawing.Size(23, 22)
        Me.BindingNavigatorMovePreviousItem.Text = "Move previous"
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
        Me.BindingNavigatorPositionItem.Size = New System.Drawing.Size(50, 23)
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
        'BindingNavigatorSeparator2
        '
        Me.BindingNavigatorSeparator2.Name = "BindingNavigatorSeparator2"
        Me.BindingNavigatorSeparator2.Size = New System.Drawing.Size(6, 25)
        '
        'MainEventPaymentBindingNavigatorSaveItem
        '
        Me.MainEventPaymentBindingNavigatorSaveItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.MainEventPaymentBindingNavigatorSaveItem.Image = CType(resources.GetObject("MainEventPaymentBindingNavigatorSaveItem.Image"), System.Drawing.Image)
        Me.MainEventPaymentBindingNavigatorSaveItem.Name = "MainEventPaymentBindingNavigatorSaveItem"
        Me.MainEventPaymentBindingNavigatorSaveItem.Size = New System.Drawing.Size(23, 22)
        Me.MainEventPaymentBindingNavigatorSaveItem.Text = "Save Data"
        '
        'fldSubmissionID
        '
        Me.fldSubmissionID.Location = New System.Drawing.Point(969, 514)
        Me.fldSubmissionID.Name = "fldSubmissionID"
        Me.fldSubmissionID.Size = New System.Drawing.Size(37, 20)
        Me.fldSubmissionID.TabIndex = 3
        '
        'cboPaymentType
        '
        Me.cboPaymentType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboPaymentType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboPaymentType.DataBindings.Add(New System.Windows.Forms.Binding("SelectedItem", Me.MainEventPaymentBindingSource, "PaymentType", True))
        Me.cboPaymentType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboPaymentType.FormattingEnabled = True
        Me.cboPaymentType.Items.AddRange(New Object() {"Discount", "Payment", "Refund"})
        Me.cboPaymentType.Location = New System.Drawing.Point(127, 4)
        Me.cboPaymentType.Name = "cboPaymentType"
        Me.cboPaymentType.RestrictContentToListItems = True
        Me.cboPaymentType.Size = New System.Drawing.Size(125, 21)
        Me.cboPaymentType.TabIndex = 1
        '
        'MainEventPaymentBindingSource
        '
        Me.MainEventPaymentBindingSource.DataMember = "MainEventPayment"
        Me.MainEventPaymentBindingSource.DataSource = Me.DsMainWPayment
        '
        'DsMainWPayment
        '
        Me.DsMainWPayment.DataSetName = "dsMainWPayment"
        Me.DsMainWPayment.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'cboPaymentMethod
        '
        Me.cboPaymentMethod.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboPaymentMethod.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboPaymentMethod.FormattingEnabled = True
        Me.cboPaymentMethod.Items.AddRange(New Object() {"Cash", "Check", "Paypal", "Coupon", "Discount", "Credit Card"})
        Me.cboPaymentMethod.Location = New System.Drawing.Point(3, 80)
        Me.cboPaymentMethod.Name = "cboPaymentMethod"
        Me.cboPaymentMethod.RestrictContentToListItems = True
        Me.cboPaymentMethod.Size = New System.Drawing.Size(118, 21)
        Me.cboPaymentMethod.TabIndex = 4
        Me.cboPaymentMethod.Text = "Payment Method:"
        '
        'AmountPaymentTextBox
        '
        Me.AmountPaymentTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainEventPaymentBindingSource, "AmountPayment", True, DataSourceUpdateMode.OnValidation, ""))
        Me.AmountPaymentTextBox.Location = New System.Drawing.Point(127, 55)
        Me.AmountPaymentTextBox.Name = "AmountPaymentTextBox"
        Me.AmountPaymentTextBox.Size = New System.Drawing.Size(70, 20)
        Me.AmountPaymentTextBox.TabIndex = 3
        '
        'PayerNameTextBox
        '
        Me.PayerNameTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainEventPaymentBindingSource, "PayerName", True))
        Me.PayerNameTextBox.Location = New System.Drawing.Point(511, 107)
        Me.PayerNameTextBox.Multiline = True
        Me.PayerNameTextBox.Name = "PayerNameTextBox"
        Me.PayerNameTextBox.Size = New System.Drawing.Size(248, 20)
        Me.PayerNameTextBox.TabIndex = 7
        '
        'TrackingNumTextBox
        '
        Me.TrackingNumTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainEventPaymentBindingSource, "TrackingNum", True))
        Me.TrackingNumTextBox.Location = New System.Drawing.Point(127, 108)
        Me.TrackingNumTextBox.Name = "TrackingNumTextBox"
        Me.TrackingNumTextBox.Size = New System.Drawing.Size(216, 20)
        Me.TrackingNumTextBox.TabIndex = 6
        '
        'PaymentStaffNumTextBox
        '
        Me.PaymentStaffNumTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainEventPaymentBindingSource, "PaymentStaffNum", True))
        Me.PaymentStaffNumTextBox.Location = New System.Drawing.Point(507, 515)
        Me.PaymentStaffNumTextBox.Name = "PaymentStaffNumTextBox"
        Me.PaymentStaffNumTextBox.ReadOnly = True
        Me.PaymentStaffNumTextBox.Size = New System.Drawing.Size(65, 20)
        Me.PaymentStaffNumTextBox.TabIndex = 23
        '
        'txtNotes
        '
        Me.txtNotes.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainEventPaymentBindingSource, "PaymentNote", True))
        Me.txtNotes.Location = New System.Drawing.Point(513, 39)
        Me.txtNotes.Multiline = True
        Me.txtNotes.Name = "txtNotes"
        Me.txtNotes.Size = New System.Drawing.Size(246, 62)
        Me.txtNotes.TabIndex = 9
        '
        'RegistrationRegNumTextBox
        '
        Me.RegistrationRegNumTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainEventPaymentBindingSource, "RegistrarRegNum", True))
        Me.RegistrationRegNumTextBox.Location = New System.Drawing.Point(304, 515)
        Me.RegistrationRegNumTextBox.Name = "RegistrationRegNumTextBox"
        Me.RegistrationRegNumTextBox.Size = New System.Drawing.Size(80, 20)
        Me.RegistrationRegNumTextBox.TabIndex = 27
        '
        'RegistrarSIDTextBox
        '
        Me.RegistrarSIDTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainEventPaymentBindingSource, "RegistrarSID", True))
        Me.RegistrarSIDTextBox.Location = New System.Drawing.Point(102, 515)
        Me.RegistrarSIDTextBox.Name = "RegistrarSIDTextBox"
        Me.RegistrarSIDTextBox.Size = New System.Drawing.Size(75, 20)
        Me.RegistrarSIDTextBox.TabIndex = 31
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.Highlight
        Me.Label1.Location = New System.Drawing.Point(7, 24)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(380, 15)
        Me.Label1.TabIndex = 34
        Me.Label1.Text = "This transaction was applied to the following registrations:"
        '
        'TextBox2
        '
        Me.TextBox2.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainEventPaymentBindingSource, "DtDeposit", True))
        Me.TextBox2.Location = New System.Drawing.Point(513, 9)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(70, 20)
        Me.TextBox2.TabIndex = 8
        '
        'pnlPaymentDetail
        '
        Me.pnlPaymentDetail.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pnlPaymentDetail.Controls.Add(Me.lblTrackingNum)
        Me.pnlPaymentDetail.Controls.Add(PayerNameLabel)
        Me.pnlPaymentDetail.Controls.Add(Me.TextBox2)
        Me.pnlPaymentDetail.Controls.Add(DtDepositLabel)
        Me.pnlPaymentDetail.Controls.Add(Me.fldPaymentMethod)
        Me.pnlPaymentDetail.Controls.Add(Me.DtPayment)
        Me.pnlPaymentDetail.Controls.Add(Me.txtNotes)
        Me.pnlPaymentDetail.Controls.Add(Me.TrackingNumTextBox)
        Me.pnlPaymentDetail.Controls.Add(PaymentNoteLabel)
        Me.pnlPaymentDetail.Controls.Add(Me.PayerNameTextBox)
        Me.pnlPaymentDetail.Controls.Add(Me.cboPaymentType)
        Me.pnlPaymentDetail.Controls.Add(Me.cboPaymentMethod)
        Me.pnlPaymentDetail.Controls.Add(PaymentTypeLabel)
        Me.pnlPaymentDetail.Controls.Add(Me.AmountPaymentTextBox)
        Me.pnlPaymentDetail.Controls.Add(AmountPaymentLabel)
        Me.pnlPaymentDetail.Controls.Add(DtPaymentLabel)
        Me.pnlPaymentDetail.Location = New System.Drawing.Point(10, 37)
        Me.pnlPaymentDetail.Name = "pnlPaymentDetail"
        Me.pnlPaymentDetail.Size = New System.Drawing.Size(766, 149)
        Me.pnlPaymentDetail.TabIndex = 0
        '
        'lblTrackingNum
        '
        Me.lblTrackingNum.AutoSize = True
        Me.lblTrackingNum.Location = New System.Drawing.Point(32, 111)
        Me.lblTrackingNum.MinimumSize = New System.Drawing.Size(90, 0)
        Me.lblTrackingNum.Name = "lblTrackingNum"
        Me.lblTrackingNum.Size = New System.Drawing.Size(90, 13)
        Me.lblTrackingNum.TabIndex = 19
        Me.lblTrackingNum.Text = "Reference #:"
        Me.lblTrackingNum.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'fldPaymentMethod
        '
        Me.fldPaymentMethod.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainEventPaymentBindingSource, "PaymentMethod", True))
        Me.fldPaymentMethod.Enabled = False
        Me.fldPaymentMethod.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fldPaymentMethod.Location = New System.Drawing.Point(127, 81)
        Me.fldPaymentMethod.Name = "fldPaymentMethod"
        Me.fldPaymentMethod.Size = New System.Drawing.Size(216, 20)
        Me.fldPaymentMethod.TabIndex = 43
        '
        'DtPayment
        '
        Me.DtPayment.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainEventPaymentBindingSource, "dtPayment", True))
        Me.DtPayment.Location = New System.Drawing.Point(127, 30)
        Me.DtPayment.Name = "DtPayment"
        Me.DtPayment.Size = New System.Drawing.Size(70, 20)
        Me.DtPayment.TabIndex = 2
        '
        'fldPayID
        '
        Me.fldPayID.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainEventPaymentBindingSource, "PaymentID", True))
        Me.fldPayID.Location = New System.Drawing.Point(811, 514)
        Me.fldPayID.Name = "fldPayID"
        Me.fldPayID.ReadOnly = True
        Me.fldPayID.Size = New System.Drawing.Size(83, 20)
        Me.fldPayID.TabIndex = 40
        '
        'btnSaveExit
        '
        Me.btnSaveExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSaveExit.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.btnSaveExit.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSaveExit.Image = CType(resources.GetObject("btnSaveExit.Image"), System.Drawing.Image)
        Me.btnSaveExit.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSaveExit.Location = New System.Drawing.Point(936, 0)
        Me.btnSaveExit.Name = "btnSaveExit"
        Me.btnSaveExit.Size = New System.Drawing.Size(40, 35)
        Me.btnSaveExit.TabIndex = 784
        Me.btnSaveExit.Text = "Close"
        Me.btnSaveExit.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSaveExit.UseVisualStyleBackColor = False
        '
        'MainMenu1
        '
        Me.MainMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem1, Me.MenuItem3, Me.MenuItem7, Me.miHelp})
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
        Me.miNew.Text = " New    xxx"
        '
        'miClose
        '
        Me.miClose.Index = 1
        Me.miClose.Text = "Close Window"
        '
        'MenuItem3
        '
        Me.MenuItem3.Index = 1
        Me.MenuItem3.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.miSave, Me.miCancel, Me.miDelete})
        Me.MenuItem3.Text = "Edit"
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
        Me.miDelete.Text = "Delete Payment"
        '
        'MenuItem7
        '
        Me.MenuItem7.Index = 2
        Me.MenuItem7.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.miStaffRpt})
        Me.MenuItem7.Text = "Reports"
        '
        'miStaffRpt
        '
        Me.miStaffRpt.Index = 0
        Me.miStaffRpt.Text = "xx"
        '
        'miHelp
        '
        Me.miHelp.Index = 3
        Me.miHelp.Text = "Help"
        '
        'fldOrderID
        '
        Me.fldOrderID.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainEventPaymentBindingSource, "OrderNum", True))
        Me.fldOrderID.Location = New System.Drawing.Point(642, 514)
        Me.fldOrderID.Name = "fldOrderID"
        Me.fldOrderID.ReadOnly = True
        Me.fldOrderID.Size = New System.Drawing.Size(83, 20)
        Me.fldOrderID.TabIndex = 788
        '
        'txtSrchID
        '
        Me.txtSrchID.Location = New System.Drawing.Point(242, 8)
        Me.txtSrchID.Name = "txtSrchID"
        Me.txtSrchID.Size = New System.Drawing.Size(112, 20)
        Me.txtSrchID.TabIndex = 789
        Me.txtSrchID.Text = "enter PaymentID"
        Me.txtSrchID.Visible = False
        '
        'btnSearch
        '
        Me.btnSearch.Location = New System.Drawing.Point(360, 8)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(127, 23)
        Me.btnSearch.TabIndex = 790
        Me.btnSearch.Text = "Search for Payments"
        Me.btnSearch.UseVisualStyleBackColor = True
        Me.btnSearch.Visible = False
        '
        'btnAddReg
        '
        Me.btnAddReg.ForeColor = System.Drawing.Color.Maroon
        Me.btnAddReg.Location = New System.Drawing.Point(513, 6)
        Me.btnAddReg.Name = "btnAddReg"
        Me.btnAddReg.Size = New System.Drawing.Size(216, 30)
        Me.btnAddReg.TabIndex = 791
        Me.btnAddReg.Text = "Select registrations this Payment Applies to"
        Me.btnAddReg.UseVisualStyleBackColor = True
        '
        'pnlGridRegs
        '
        Me.pnlGridRegs.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlGridRegs.Controls.Add(Me.DataGridView1)
        Me.pnlGridRegs.Controls.Add(Me.btnAddReg)
        Me.pnlGridRegs.Controls.Add(Me.Label1)
        Me.pnlGridRegs.Location = New System.Drawing.Point(12, 189)
        Me.pnlGridRegs.Name = "pnlGridRegs"
        Me.pnlGridRegs.Size = New System.Drawing.Size(987, 306)
        Me.pnlGridRegs.TabIndex = 792
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.AllowUserToOrderColumns = True
        Me.DataGridView1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridView1.AutoGenerateColumns = False
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridView1.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.OrderNumDataGridViewTextBoxColumn1, Me.LfNameDataGridViewTextBoxColumn1, Me.OrgNameDataGridViewTextBoxColumn1, Me.DataGridViewTextBoxColumn1, Me.DataGridViewCheckBoxColumn1, Me.DataGridViewCheckBoxColumn2, Me.EventNameDataGridViewTextBoxColumn, Me.DataGridViewTextBoxColumn2, Me.DataGridViewTextBoxColumn3})
        Me.DataGridView1.DataMember = "GridPaymentRegs"
        Me.DataGridView1.DataSource = Me.dsGridPaymentRegs
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DataGridView1.DefaultCellStyle = DataGridViewCellStyle2
        Me.DataGridView1.Location = New System.Drawing.Point(7, 42)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridView1.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.DataGridView1.RowHeadersWidth = 15
        Me.DataGridView1.Size = New System.Drawing.Size(969, 261)
        Me.DataGridView1.TabIndex = 35
        '
        'OrderNumDataGridViewTextBoxColumn1
        '
        Me.OrderNumDataGridViewTextBoxColumn1.DataPropertyName = "OrderNum"
        Me.OrderNumDataGridViewTextBoxColumn1.HeaderText = "OrderNum"
        Me.OrderNumDataGridViewTextBoxColumn1.Name = "OrderNumDataGridViewTextBoxColumn1"
        Me.OrderNumDataGridViewTextBoxColumn1.ReadOnly = True
        Me.OrderNumDataGridViewTextBoxColumn1.Width = 75
        '
        'LfNameDataGridViewTextBoxColumn1
        '
        Me.LfNameDataGridViewTextBoxColumn1.DataPropertyName = "lfName"
        Me.LfNameDataGridViewTextBoxColumn1.HeaderText = "Name"
        Me.LfNameDataGridViewTextBoxColumn1.Name = "LfNameDataGridViewTextBoxColumn1"
        Me.LfNameDataGridViewTextBoxColumn1.ReadOnly = True
        '
        'OrgNameDataGridViewTextBoxColumn1
        '
        Me.OrgNameDataGridViewTextBoxColumn1.DataPropertyName = "OrgName"
        Me.OrgNameDataGridViewTextBoxColumn1.HeaderText = "OrgName"
        Me.OrgNameDataGridViewTextBoxColumn1.Name = "OrgNameDataGridViewTextBoxColumn1"
        Me.OrgNameDataGridViewTextBoxColumn1.ReadOnly = True
        Me.OrgNameDataGridViewTextBoxColumn1.Width = 150
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.DataPropertyName = "AmountIndFee"
        Me.DataGridViewTextBoxColumn1.HeaderText = "IndFee"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.ReadOnly = True
        Me.DataGridViewTextBoxColumn1.Width = 50
        '
        'DataGridViewCheckBoxColumn1
        '
        Me.DataGridViewCheckBoxColumn1.DataPropertyName = "Cancelled"
        Me.DataGridViewCheckBoxColumn1.HeaderText = "Cancelled"
        Me.DataGridViewCheckBoxColumn1.Name = "DataGridViewCheckBoxColumn1"
        Me.DataGridViewCheckBoxColumn1.ReadOnly = True
        Me.DataGridViewCheckBoxColumn1.Width = 75
        '
        'DataGridViewCheckBoxColumn2
        '
        Me.DataGridViewCheckBoxColumn2.DataPropertyName = "RefundQfy"
        Me.DataGridViewCheckBoxColumn2.HeaderText = "RefundQfy"
        Me.DataGridViewCheckBoxColumn2.Name = "DataGridViewCheckBoxColumn2"
        Me.DataGridViewCheckBoxColumn2.ReadOnly = True
        Me.DataGridViewCheckBoxColumn2.Width = 75
        '
        'EventNameDataGridViewTextBoxColumn
        '
        Me.EventNameDataGridViewTextBoxColumn.DataPropertyName = "EventName"
        Me.EventNameDataGridViewTextBoxColumn.HeaderText = "EventName"
        Me.EventNameDataGridViewTextBoxColumn.Name = "EventNameDataGridViewTextBoxColumn"
        Me.EventNameDataGridViewTextBoxColumn.ReadOnly = True
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.DataPropertyName = "Notes"
        Me.DataGridViewTextBoxColumn2.HeaderText = "Notes"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.ReadOnly = True
        Me.DataGridViewTextBoxColumn2.Width = 200
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.DataPropertyName = "RegistrationID"
        Me.DataGridViewTextBoxColumn3.HeaderText = "RegistrationID"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.ReadOnly = True
        '
        'dsGridPaymentRegs
        '
        Me.dsGridPaymentRegs.DataSetName = "dsMainEventPaymentRegs"
        Me.dsGridPaymentRegs.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'RegIDDataGridViewTextBoxColumn
        '
        Me.RegIDDataGridViewTextBoxColumn.DataPropertyName = "RegistrationID"
        Me.RegIDDataGridViewTextBoxColumn.HeaderText = "RegistrationID"
        Me.RegIDDataGridViewTextBoxColumn.Name = "RegIDDataGridViewTextBoxColumn"
        Me.RegIDDataGridViewTextBoxColumn.Width = 5
        '
        'OrderNumDataGridViewTextBoxColumn
        '
        Me.OrderNumDataGridViewTextBoxColumn.DataPropertyName = "OrderNum"
        Me.OrderNumDataGridViewTextBoxColumn.HeaderText = "Order Num"
        Me.OrderNumDataGridViewTextBoxColumn.Name = "OrderNumDataGridViewTextBoxColumn"
        Me.OrderNumDataGridViewTextBoxColumn.Width = 50
        '
        'LfNameDataGridViewTextBoxColumn
        '
        Me.LfNameDataGridViewTextBoxColumn.DataPropertyName = "lfName"
        Me.LfNameDataGridViewTextBoxColumn.HeaderText = "Name"
        Me.LfNameDataGridViewTextBoxColumn.Name = "LfNameDataGridViewTextBoxColumn"
        '
        'OrgNameDataGridViewTextBoxColumn
        '
        Me.OrgNameDataGridViewTextBoxColumn.DataPropertyName = "OrgName"
        Me.OrgNameDataGridViewTextBoxColumn.HeaderText = "Congregation"
        Me.OrgNameDataGridViewTextBoxColumn.Name = "OrgNameDataGridViewTextBoxColumn"
        Me.OrgNameDataGridViewTextBoxColumn.Width = 150
        '
        'ErrorProvider1
        '
        Me.ErrorProvider1.ContainerControl = Me
        '
        'GridPaymentRegsTableAdapter
        '
        Me.GridPaymentRegsTableAdapter.ClearBeforeFill = True
        '
        'MainEventPaymentTableAdapter
        '
        Me.MainEventPaymentTableAdapter.ClearBeforeFill = True
        '
        'TableAdapterManager
        '
        Me.TableAdapterManager.BackupDataSetBeforeUpdate = False
        Me.TableAdapterManager.taMainRegistrPayment = Me.MainEventPaymentTableAdapter
        Me.TableAdapterManager.UpdateOrder = InfoCtr.dsMainWPaymentTableAdapters.TableAdapterManager.UpdateOrderOption.InsertUpdateDelete
        '
        'frmMainWPayment
        '
        Me.AcceptButton = Me.btnSearch
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1011, 563)
        Me.Controls.Add(Me.pnlGridRegs)
        Me.Controls.Add(Me.btnSearch)
        Me.Controls.Add(Me.txtSrchID)
        Me.Controls.Add(Me.fldOrderID)
        Me.Controls.Add(Label4)
        Me.Controls.Add(Me.btnSaveExit)
        Me.Controls.Add(Me.fldPayID)
        Me.Controls.Add(Me.fldSubmissionID)
        Me.Controls.Add(PaymentIDLabel)
        Me.Controls.Add(Label5)
        Me.Controls.Add(Me.pnlPaymentDetail)
        Me.Controls.Add(PaymentStaffNumLabel)
        Me.Controls.Add(Me.PaymentStaffNumTextBox)
        Me.Controls.Add(RegistrarRegNumLabel)
        Me.Controls.Add(Me.RegistrationRegNumTextBox)
        Me.Controls.Add(Me.RegistrarSIDLabel)
        Me.Controls.Add(Me.RegistrarSIDTextBox)
        Me.Menu = Me.MainMenu1
        Me.Name = "frmMainWPayment"
        Me.Tag = "FINANCIALS"
        Me.Text = "FINANCIALS DETAIL"
        CType(Me.MainEventPaymentBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsMainWPayment, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlPaymentDetail.ResumeLayout(False)
        Me.pnlPaymentDetail.PerformLayout()
        Me.pnlGridRegs.ResumeLayout(False)
        Me.pnlGridRegs.PerformLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dsGridPaymentRegs, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents DsMainWPayment As InfoCtr.dsMainWPayment
    Friend WithEvents MainEventPaymentBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents MainEventPaymentTableAdapter As InfoCtr.dsMainWPaymentTableAdapters.taMainRegistrPayment
    Friend WithEvents TableAdapterManager As InfoCtr.dsMainWPaymentTableAdapters.TableAdapterManager
    Friend WithEvents MainEventPaymentBindingNavigator As System.Windows.Forms.BindingNavigator
    Friend WithEvents BindingNavigatorAddNewItem As System.Windows.Forms.ToolStripButton
    Friend WithEvents BindingNavigatorCountItem As System.Windows.Forms.ToolStripLabel
    Friend WithEvents BindingNavigatorDeleteItem As System.Windows.Forms.ToolStripButton
    Friend WithEvents BindingNavigatorMoveFirstItem As System.Windows.Forms.ToolStripButton
    Friend WithEvents BindingNavigatorMovePreviousItem As System.Windows.Forms.ToolStripButton
    Friend WithEvents BindingNavigatorSeparator As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents BindingNavigatorPositionItem As System.Windows.Forms.ToolStripTextBox
    Friend WithEvents BindingNavigatorSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents BindingNavigatorMoveNextItem As System.Windows.Forms.ToolStripButton
    Friend WithEvents BindingNavigatorMoveLastItem As System.Windows.Forms.ToolStripButton
    Friend WithEvents BindingNavigatorSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents MainEventPaymentBindingNavigatorSaveItem As System.Windows.Forms.ToolStripButton
    Friend WithEvents fldSubmissionID As System.Windows.Forms.TextBox
    Friend WithEvents cboPaymentType As InfoCtr.ComboBoxRelaxed
    Friend WithEvents cboPaymentMethod As InfoCtr.ComboBoxRelaxed
    Friend WithEvents AmountPaymentTextBox As System.Windows.Forms.TextBox
    Friend WithEvents PayerNameTextBox As System.Windows.Forms.TextBox
    Friend WithEvents TrackingNumTextBox As System.Windows.Forms.TextBox
    Friend WithEvents PaymentStaffNumTextBox As System.Windows.Forms.TextBox
    Friend WithEvents txtNotes As System.Windows.Forms.TextBox
    Friend WithEvents RegistrationRegNumTextBox As System.Windows.Forms.TextBox
    Friend WithEvents RegistrarSIDTextBox As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents GridPaymentRegsBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents dsGridPaymentRegs As InfoCtr.dsMainEventPaymentRegs
    Friend WithEvents GridPaymentRegsTableAdapter As InfoCtr.dsMainEventPaymentRegsTableAdapters.GridPaymentRegsTableAdapter
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents pnlPaymentDetail As System.Windows.Forms.Panel
    Friend WithEvents fldPayID As System.Windows.Forms.TextBox
    Friend WithEvents TextBox2 As System.Windows.Forms.TextBox
    Friend WithEvents DtPayment As InfoCtr.DateTextBox
    Friend WithEvents RegistrationIDDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colPaymentNum As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colOrderNum As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cboThisPayment As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents NameDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents AmountIndFeeDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CancelledDataGridViewCheckBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents RefundQfyDataGridViewCheckBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents NotesDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents btnSaveExit As System.Windows.Forms.Button
    Friend WithEvents MainMenu1 As System.Windows.Forms.MainMenu
    Friend WithEvents MenuItem1 As System.Windows.Forms.MenuItem
    Friend WithEvents miNew As System.Windows.Forms.MenuItem
    Friend WithEvents miClose As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem3 As System.Windows.Forms.MenuItem
    Friend WithEvents miSave As System.Windows.Forms.MenuItem
    Friend WithEvents miCancel As System.Windows.Forms.MenuItem
    Friend WithEvents miDelete As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem7 As System.Windows.Forms.MenuItem
    Friend WithEvents miStaffRpt As System.Windows.Forms.MenuItem
    Friend WithEvents miHelp As System.Windows.Forms.MenuItem
    Friend WithEvents fldPaymentMethod As System.Windows.Forms.TextBox
    Friend WithEvents fldOrderID As System.Windows.Forms.TextBox
    Friend WithEvents txtSrchID As System.Windows.Forms.TextBox
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents btnAddReg As System.Windows.Forms.Button
    Friend WithEvents lblTrackingNum As System.Windows.Forms.Label
    Friend WithEvents pnlGridRegs As System.Windows.Forms.Panel
    Friend WithEvents RegistrarSIDLabel As System.Windows.Forms.Label
    Friend WithEvents RegIDDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents OrderNumDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents LfNameDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents OrgNameDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ErrorProvider1 As System.Windows.Forms.ErrorProvider
    Friend WithEvents OrderNumDataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents LfNameDataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents OrgNameDataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewCheckBoxColumn1 As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents DataGridViewCheckBoxColumn2 As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents EventNameDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    'Friend WithEvents GridPaymentRegsTableAdapter As InfoCtr.dsMainEventPaymentRegsTableAdapters.GridPaymentRegsTableAdapter
End Class
