<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMainWOrder
    Inherits System.Windows.Forms.Form

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim OIDLabel As System.Windows.Forms.Label
        Dim DtOrderLabel As System.Windows.Forms.Label
        Dim OrderStatusLabel As System.Windows.Forms.Label
        Dim ProposedPaymentMethodLabel As System.Windows.Forms.Label
        Dim AmountOrderGrossLabel As System.Windows.Forms.Label
        Dim RegistrarNameLabel As System.Windows.Forms.Label
        Dim RegistrarContactNumLabel As System.Windows.Forms.Label
        Dim OrderNotesLabel As System.Windows.Forms.Label
        Dim RegistrarEmailLabel As System.Windows.Forms.Label
        Dim Label1 As System.Windows.Forms.Label
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMainWOrder))
        Me.qtyRegsLabel = New System.Windows.Forms.Label()
        Me.AmountOrderTotalLabel = New System.Windows.Forms.Label()
        Me.MainWOrder2BindingNavigator = New System.Windows.Forms.BindingNavigator(Me.components)
        Me.MainWOrder2BindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.dsMainWOrder1 = New InfoCtr.dsMainWOrder()
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
        Me.MainEventOrderBindingNavigatorSaveItem = New System.Windows.Forms.ToolStripButton()
        Me.fldOID = New System.Windows.Forms.TextBox()
        Me.txtAmountOrderGross = New System.Windows.Forms.TextBox()
        Me.RegistrarNameTextBox = New System.Windows.Forms.TextBox()
        Me.RegistrarContactNumTextBox = New System.Windows.Forms.TextBox()
        Me.txtAmountOrderTotal = New System.Windows.Forms.TextBox()
        Me.RegistrarEmailTextBox = New System.Windows.Forms.TextBox()
        Me.txtqtyRegs = New System.Windows.Forms.TextBox()
        Me.txtOrderNotes = New System.Windows.Forms.TextBox()
        Me.cboProposedPayment = New InfoCtr.ComboBoxRelaxed()
        Me.txtSID = New System.Windows.Forms.TextBox()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.miPayment = New System.Windows.Forms.ToolStripMenuItem()
        Me.miRegistration = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.DtOrder = New InfoCtr.DateTextBox()
        Me.cboOrderStatus = New InfoCtr.ComboBoxRelaxed()
        Me.btNewReg = New System.Windows.Forms.Button()
        Me.dgrd = New System.Windows.Forms.DataGrid()
        Me.DsNewWRegistration = New InfoCtr.dsNewWRegistration()
        Me.DataGridTableStyle1 = New System.Windows.Forms.DataGridTableStyle()
        Me.DataGridTextBoxColumn1 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn2 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn3 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn4 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn5 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn6 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.tblRegOrderTableAdapter = New InfoCtr.dsNewWRegistrationTableAdapters.tblRegOrderTableAdapter()
        Me.taMainRegistOrder = New InfoCtr.dsMainWOrderTableAdapters.taMainRegistOrder()
        Me.TableAdapterManager = New InfoCtr.dsMainWOrderTableAdapters.TableAdapterManager()
        OIDLabel = New System.Windows.Forms.Label()
        DtOrderLabel = New System.Windows.Forms.Label()
        OrderStatusLabel = New System.Windows.Forms.Label()
        ProposedPaymentMethodLabel = New System.Windows.Forms.Label()
        AmountOrderGrossLabel = New System.Windows.Forms.Label()
        RegistrarNameLabel = New System.Windows.Forms.Label()
        RegistrarContactNumLabel = New System.Windows.Forms.Label()
        OrderNotesLabel = New System.Windows.Forms.Label()
        RegistrarEmailLabel = New System.Windows.Forms.Label()
        Label1 = New System.Windows.Forms.Label()
        CType(Me.MainWOrder2BindingNavigator, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MainWOrder2BindingNavigator.SuspendLayout()
        CType(Me.MainWOrder2BindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dsMainWOrder1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.dgrd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsNewWRegistration, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'OIDLabel
        '
        OIDLabel.AutoSize = True
        OIDLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        OIDLabel.Location = New System.Drawing.Point(24, 10)
        OIDLabel.Name = "OIDLabel"
        OIDLabel.Size = New System.Drawing.Size(74, 20)
        OIDLabel.TabIndex = 2
        OIDLabel.Text = "Order #:"
        '
        'DtOrderLabel
        '
        DtOrderLabel.AutoSize = True
        DtOrderLabel.Location = New System.Drawing.Point(45, 55)
        DtOrderLabel.Name = "DtOrderLabel"
        DtOrderLabel.Size = New System.Drawing.Size(98, 13)
        DtOrderLabel.TabIndex = 4
        DtOrderLabel.Text = "Date Order Placed:"
        '
        'OrderStatusLabel
        '
        OrderStatusLabel.AutoSize = True
        OrderStatusLabel.Location = New System.Drawing.Point(74, 90)
        OrderStatusLabel.Name = "OrderStatusLabel"
        OrderStatusLabel.Size = New System.Drawing.Size(69, 13)
        OrderStatusLabel.TabIndex = 6
        OrderStatusLabel.Text = "Order Status:"
        '
        'ProposedPaymentMethodLabel
        '
        ProposedPaymentMethodLabel.AutoSize = True
        ProposedPaymentMethodLabel.Location = New System.Drawing.Point(5, 194)
        ProposedPaymentMethodLabel.Name = "ProposedPaymentMethodLabel"
        ProposedPaymentMethodLabel.Size = New System.Drawing.Size(138, 13)
        ProposedPaymentMethodLabel.TabIndex = 8
        ProposedPaymentMethodLabel.Text = "Proposed Payment Method:"
        '
        'AmountOrderGrossLabel
        '
        AmountOrderGrossLabel.AutoSize = True
        AmountOrderGrossLabel.Location = New System.Drawing.Point(25, 230)
        AmountOrderGrossLabel.Name = "AmountOrderGrossLabel"
        AmountOrderGrossLabel.Size = New System.Drawing.Size(118, 13)
        AmountOrderGrossLabel.TabIndex = 10
        AmountOrderGrossLabel.Text = "Total Before Discounts:"
        '
        'RegistrarNameLabel
        '
        RegistrarNameLabel.AutoSize = True
        RegistrarNameLabel.Location = New System.Drawing.Point(330, 399)
        RegistrarNameLabel.Name = "RegistrarNameLabel"
        RegistrarNameLabel.Size = New System.Drawing.Size(83, 13)
        RegistrarNameLabel.TabIndex = 12
        RegistrarNameLabel.Text = "Registrar Name:"
        RegistrarNameLabel.Visible = False
        '
        'RegistrarContactNumLabel
        '
        RegistrarContactNumLabel.AutoSize = True
        RegistrarContactNumLabel.Location = New System.Drawing.Point(587, 399)
        RegistrarContactNumLabel.Name = "RegistrarContactNumLabel"
        RegistrarContactNumLabel.Size = New System.Drawing.Size(117, 13)
        RegistrarContactNumLabel.TabIndex = 14
        RegistrarContactNumLabel.Text = "Registrar Contact Num:"
        '
        'OrderNotesLabel
        '
        OrderNotesLabel.AutoSize = True
        OrderNotesLabel.Location = New System.Drawing.Point(5, 300)
        OrderNotesLabel.Name = "OrderNotesLabel"
        OrderNotesLabel.Size = New System.Drawing.Size(62, 13)
        OrderNotesLabel.TabIndex = 16
        OrderNotesLabel.Text = "Order Note:"
        '
        'RegistrarEmailLabel
        '
        RegistrarEmailLabel.AutoSize = True
        RegistrarEmailLabel.Location = New System.Drawing.Point(329, 427)
        RegistrarEmailLabel.Name = "RegistrarEmailLabel"
        RegistrarEmailLabel.Size = New System.Drawing.Size(80, 13)
        RegistrarEmailLabel.TabIndex = 18
        RegistrarEmailLabel.Text = "Registrar Email:"
        '
        'Label1
        '
        Label1.AutoSize = True
        Label1.Location = New System.Drawing.Point(601, 430)
        Label1.Name = "Label1"
        Label1.Size = New System.Drawing.Size(107, 13)
        Label1.TabIndex = 27
        Label1.Text = "Drupal Registrar SID:"
        '
        'qtyRegsLabel
        '
        Me.qtyRegsLabel.AutoSize = True
        Me.qtyRegsLabel.Location = New System.Drawing.Point(46, 265)
        Me.qtyRegsLabel.Name = "qtyRegsLabel"
        Me.qtyRegsLabel.Size = New System.Drawing.Size(97, 13)
        Me.qtyRegsLabel.TabIndex = 20
        Me.qtyRegsLabel.Text = "Registration Count:"
        '
        'AmountOrderTotalLabel
        '
        Me.AmountOrderTotalLabel.AutoSize = True
        Me.AmountOrderTotalLabel.Location = New System.Drawing.Point(49, 125)
        Me.AmountOrderTotalLabel.Name = "AmountOrderTotalLabel"
        Me.AmountOrderTotalLabel.Size = New System.Drawing.Size(94, 13)
        Me.AmountOrderTotalLabel.TabIndex = 24
        Me.AmountOrderTotalLabel.Text = "Amount Tot Order:"
        '
        'MainWOrder2BindingNavigator
        '
        Me.MainWOrder2BindingNavigator.AddNewItem = Nothing
        Me.MainWOrder2BindingNavigator.BindingSource = Me.MainWOrder2BindingSource
        Me.MainWOrder2BindingNavigator.CountItem = Me.BindingNavigatorCountItem
        Me.MainWOrder2BindingNavigator.DeleteItem = Me.BindingNavigatorDeleteItem
        Me.MainWOrder2BindingNavigator.Dock = System.Windows.Forms.DockStyle.None
        Me.MainWOrder2BindingNavigator.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BindingNavigatorMoveFirstItem, Me.BindingNavigatorMovePreviousItem, Me.BindingNavigatorSeparator, Me.BindingNavigatorPositionItem, Me.BindingNavigatorCountItem, Me.BindingNavigatorSeparator1, Me.BindingNavigatorMoveNextItem, Me.BindingNavigatorMoveLastItem, Me.BindingNavigatorSeparator2, Me.BindingNavigatorDeleteItem, Me.MainEventOrderBindingNavigatorSaveItem})
        Me.MainWOrder2BindingNavigator.Location = New System.Drawing.Point(0, 0)
        Me.MainWOrder2BindingNavigator.MoveFirstItem = Me.BindingNavigatorMoveFirstItem
        Me.MainWOrder2BindingNavigator.MoveLastItem = Me.BindingNavigatorMoveLastItem
        Me.MainWOrder2BindingNavigator.MoveNextItem = Me.BindingNavigatorMoveNextItem
        Me.MainWOrder2BindingNavigator.MovePreviousItem = Me.BindingNavigatorMovePreviousItem
        Me.MainWOrder2BindingNavigator.Name = "MainWOrder2BindingNavigator"
        Me.MainWOrder2BindingNavigator.PositionItem = Me.BindingNavigatorPositionItem
        Me.MainWOrder2BindingNavigator.Size = New System.Drawing.Size(255, 25)
        Me.MainWOrder2BindingNavigator.TabIndex = 0
        Me.MainWOrder2BindingNavigator.Text = "BindingNavigator1"
        '
        'MainWOrder2BindingSource
        '
        Me.MainWOrder2BindingSource.DataMember = "MainEventOrder2"
        Me.MainWOrder2BindingSource.DataSource = Me.dsMainWOrder1
        '
        'dsMainWOrder1
        '
        Me.dsMainWOrder1.DataSetName = "dsMainWOrder"
        Me.dsMainWOrder1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
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
        'MainEventOrderBindingNavigatorSaveItem
        '
        Me.MainEventOrderBindingNavigatorSaveItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.MainEventOrderBindingNavigatorSaveItem.Image = CType(resources.GetObject("MainEventOrderBindingNavigatorSaveItem.Image"), System.Drawing.Image)
        Me.MainEventOrderBindingNavigatorSaveItem.Name = "MainEventOrderBindingNavigatorSaveItem"
        Me.MainEventOrderBindingNavigatorSaveItem.Size = New System.Drawing.Size(23, 22)
        Me.MainEventOrderBindingNavigatorSaveItem.Text = "Save Data"
        '
        'fldOID
        '
        Me.fldOID.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainWOrder2BindingSource, "OrderID", True))
        Me.fldOID.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fldOID.Location = New System.Drawing.Point(116, 7)
        Me.fldOID.Name = "fldOID"
        Me.fldOID.ReadOnly = True
        Me.fldOID.Size = New System.Drawing.Size(160, 26)
        Me.fldOID.TabIndex = 3
        '
        'txtAmountOrderGross
        '
        Me.txtAmountOrderGross.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainWOrder2BindingSource, "AmountOrderGross", True))
        Me.txtAmountOrderGross.Location = New System.Drawing.Point(145, 227)
        Me.txtAmountOrderGross.Name = "txtAmountOrderGross"
        Me.txtAmountOrderGross.ReadOnly = True
        Me.txtAmountOrderGross.Size = New System.Drawing.Size(50, 20)
        Me.txtAmountOrderGross.TabIndex = 15
        '
        'RegistrarNameTextBox
        '
        Me.RegistrarNameTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainWOrder2BindingSource, "RegistrarName", True))
        Me.RegistrarNameTextBox.Location = New System.Drawing.Point(419, 396)
        Me.RegistrarNameTextBox.Name = "RegistrarNameTextBox"
        Me.RegistrarNameTextBox.Size = New System.Drawing.Size(131, 20)
        Me.RegistrarNameTextBox.TabIndex = 22
        Me.RegistrarNameTextBox.Visible = False
        '
        'RegistrarContactNumTextBox
        '
        Me.RegistrarContactNumTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainWOrder2BindingSource, "RegistrarContactNum", True))
        Me.RegistrarContactNumTextBox.Location = New System.Drawing.Point(710, 396)
        Me.RegistrarContactNumTextBox.Name = "RegistrarContactNumTextBox"
        Me.RegistrarContactNumTextBox.ReadOnly = True
        Me.RegistrarContactNumTextBox.Size = New System.Drawing.Size(90, 20)
        Me.RegistrarContactNumTextBox.TabIndex = 26
        '
        'txtAmountOrderTotal
        '
        Me.txtAmountOrderTotal.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainWOrder2BindingSource, "AmountOrderTotal", True))
        Me.txtAmountOrderTotal.Location = New System.Drawing.Point(145, 124)
        Me.txtAmountOrderTotal.Name = "txtAmountOrderTotal"
        Me.txtAmountOrderTotal.ReadOnly = True
        Me.txtAmountOrderTotal.Size = New System.Drawing.Size(50, 20)
        Me.txtAmountOrderTotal.TabIndex = 9
        '
        'RegistrarEmailTextBox
        '
        Me.RegistrarEmailTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainWOrder2BindingSource, "RegistrarEmail", True))
        Me.RegistrarEmailTextBox.Location = New System.Drawing.Point(411, 424)
        Me.RegistrarEmailTextBox.Name = "RegistrarEmailTextBox"
        Me.RegistrarEmailTextBox.Size = New System.Drawing.Size(168, 20)
        Me.RegistrarEmailTextBox.TabIndex = 24
        '
        'txtqtyRegs
        '
        Me.txtqtyRegs.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainWOrder2BindingSource, "qtyRegs", True))
        Me.txtqtyRegs.Location = New System.Drawing.Point(145, 261)
        Me.txtqtyRegs.Name = "txtqtyRegs"
        Me.txtqtyRegs.ReadOnly = True
        Me.txtqtyRegs.Size = New System.Drawing.Size(50, 20)
        Me.txtqtyRegs.TabIndex = 17
        '
        'txtOrderNotes
        '
        Me.txtOrderNotes.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainWOrder2BindingSource, "OrderNotes", True))
        Me.txtOrderNotes.Location = New System.Drawing.Point(25, 316)
        Me.txtOrderNotes.Multiline = True
        Me.txtOrderNotes.Name = "txtOrderNotes"
        Me.txtOrderNotes.Size = New System.Drawing.Size(256, 88)
        Me.txtOrderNotes.TabIndex = 20
        '
        'cboProposedPayment
        '
        Me.cboProposedPayment.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboProposedPayment.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboProposedPayment.DataBindings.Add(New System.Windows.Forms.Binding("SelectedItem", Me.MainWOrder2BindingSource, "ProposedPaymentMethod", True))
        Me.cboProposedPayment.FormattingEnabled = True
        Me.cboProposedPayment.Items.AddRange(New Object() {"cash", "check", "credit", "free_order", "Not Applicable"})
        Me.cboProposedPayment.Location = New System.Drawing.Point(145, 192)
        Me.cboProposedPayment.Name = "cboProposedPayment"
        Me.cboProposedPayment.RestrictContentToListItems = True
        Me.cboProposedPayment.Size = New System.Drawing.Size(131, 21)
        Me.cboProposedPayment.TabIndex = 13
        '
        'txtSID
        '
        Me.txtSID.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainWOrder2BindingSource, "DesignatedSID", True))
        Me.txtSID.Location = New System.Drawing.Point(710, 427)
        Me.txtSID.Name = "txtSID"
        Me.txtSID.ReadOnly = True
        Me.txtSID.Size = New System.Drawing.Size(88, 20)
        Me.txtSID.TabIndex = 28
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.miPayment, Me.miRegistration})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(138, 48)
        '
        'miPayment
        '
        Me.miPayment.Name = "miPayment"
        Me.miPayment.Size = New System.Drawing.Size(137, 22)
        Me.miPayment.Text = "Payment"
        '
        'miRegistration
        '
        Me.miRegistration.Name = "miRegistration"
        Me.miRegistration.Size = New System.Drawing.Size(137, 22)
        Me.miRegistration.Text = "Registration"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.TextBox1)
        Me.Panel1.Controls.Add(Me.DtOrder)
        Me.Panel1.Controls.Add(Me.txtOrderNotes)
        Me.Panel1.Controls.Add(Me.cboOrderStatus)
        Me.Panel1.Controls.Add(Me.fldOID)
        Me.Panel1.Controls.Add(Me.AmountOrderTotalLabel)
        Me.Panel1.Controls.Add(Me.txtqtyRegs)
        Me.Panel1.Controls.Add(Me.cboProposedPayment)
        Me.Panel1.Controls.Add(Me.qtyRegsLabel)
        Me.Panel1.Controls.Add(OIDLabel)
        Me.Panel1.Controls.Add(OrderNotesLabel)
        Me.Panel1.Controls.Add(DtOrderLabel)
        Me.Panel1.Controls.Add(Me.txtAmountOrderTotal)
        Me.Panel1.Controls.Add(OrderStatusLabel)
        Me.Panel1.Controls.Add(ProposedPaymentMethodLabel)
        Me.Panel1.Controls.Add(AmountOrderGrossLabel)
        Me.Panel1.Controls.Add(Me.txtAmountOrderGross)
        Me.Panel1.Location = New System.Drawing.Point(12, 43)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(297, 416)
        Me.Panel1.TabIndex = 32
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(71, 159)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(72, 13)
        Me.Label2.TabIndex = 35
        Me.Label2.Text = "Balance Due:"
        '
        'TextBox1
        '
        Me.TextBox1.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainWOrder2BindingSource, "AmountOrderTotal", True))
        Me.TextBox1.Location = New System.Drawing.Point(145, 158)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.ReadOnly = True
        Me.TextBox1.Size = New System.Drawing.Size(50, 20)
        Me.TextBox1.TabIndex = 11
        '
        'DtOrder
        '
        Me.DtOrder.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainWOrder2BindingSource, "DtOrder", True))
        Me.DtOrder.Location = New System.Drawing.Point(145, 55)
        Me.DtOrder.Name = "DtOrder"
        Me.DtOrder.Size = New System.Drawing.Size(70, 20)
        Me.DtOrder.TabIndex = 5
        '
        'cboOrderStatus
        '
        Me.cboOrderStatus.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboOrderStatus.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboOrderStatus.DataBindings.Add(New System.Windows.Forms.Binding("SelectedItem", Me.MainWOrder2BindingSource, "OrderStatus", True))
        Me.cboOrderStatus.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboOrderStatus.FormattingEnabled = True
        Me.cboOrderStatus.Location = New System.Drawing.Point(145, 89)
        Me.cboOrderStatus.Name = "cboOrderStatus"
        Me.cboOrderStatus.RestrictContentToListItems = True
        Me.cboOrderStatus.Size = New System.Drawing.Size(131, 21)
        Me.cboOrderStatus.TabIndex = 7
        '
        'btNewReg
        '
        Me.btNewReg.Location = New System.Drawing.Point(543, 44)
        Me.btNewReg.Name = "btNewReg"
        Me.btNewReg.Size = New System.Drawing.Size(101, 41)
        Me.btNewReg.TabIndex = 33
        Me.btNewReg.Text = "Add Registration to this Order"
        Me.btNewReg.UseVisualStyleBackColor = True
        '
        'dgrd
        '
        Me.dgrd.CaptionFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.dgrd.CaptionText = "Registrations in this Order"
        Me.dgrd.DataMember = "tblRegOrder"
        Me.dgrd.DataSource = Me.DsNewWRegistration
        Me.dgrd.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.dgrd.Location = New System.Drawing.Point(332, 108)
        Me.dgrd.Name = "dgrd"
        Me.dgrd.ReadOnly = True
        Me.dgrd.RowHeaderWidth = 15
        Me.dgrd.Size = New System.Drawing.Size(645, 263)
        Me.dgrd.TabIndex = 500
        Me.dgrd.TableStyles.AddRange(New System.Windows.Forms.DataGridTableStyle() {Me.DataGridTableStyle1})
        '
        'DsNewWRegistration
        '
        Me.DsNewWRegistration.DataSetName = "dsNewWRegistration"
        Me.DsNewWRegistration.EnforceConstraints = False
        Me.DsNewWRegistration.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'DataGridTableStyle1
        '
        Me.DataGridTableStyle1.DataGrid = Me.dgrd
        Me.DataGridTableStyle1.GridColumnStyles.AddRange(New System.Windows.Forms.DataGridColumnStyle() {Me.DataGridTextBoxColumn1, Me.DataGridTextBoxColumn2, Me.DataGridTextBoxColumn3, Me.DataGridTextBoxColumn4, Me.DataGridTextBoxColumn5, Me.DataGridTextBoxColumn6})
        Me.DataGridTableStyle1.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.DataGridTableStyle1.MappingName = "tblRegOrder"
        Me.DataGridTableStyle1.RowHeaderWidth = 15
        '
        'DataGridTextBoxColumn1
        '
        Me.DataGridTextBoxColumn1.Format = ""
        Me.DataGridTextBoxColumn1.FormatInfo = Nothing
        Me.DataGridTextBoxColumn1.HeaderText = "Registrar"
        Me.DataGridTextBoxColumn1.MappingName = "Registrar"
        Me.DataGridTextBoxColumn1.Width = 75
        '
        'DataGridTextBoxColumn2
        '
        Me.DataGridTextBoxColumn2.Format = ""
        Me.DataGridTextBoxColumn2.FormatInfo = Nothing
        Me.DataGridTextBoxColumn2.HeaderText = "FirstName"
        Me.DataGridTextBoxColumn2.MappingName = "FirstName"
        Me.DataGridTextBoxColumn2.Width = 75
        '
        'DataGridTextBoxColumn3
        '
        Me.DataGridTextBoxColumn3.Format = ""
        Me.DataGridTextBoxColumn3.FormatInfo = Nothing
        Me.DataGridTextBoxColumn3.HeaderText = "LastName"
        Me.DataGridTextBoxColumn3.MappingName = "Lastname"
        Me.DataGridTextBoxColumn3.Width = 75
        '
        'DataGridTextBoxColumn4
        '
        Me.DataGridTextBoxColumn4.Format = ""
        Me.DataGridTextBoxColumn4.FormatInfo = Nothing
        Me.DataGridTextBoxColumn4.HeaderText = "Email"
        Me.DataGridTextBoxColumn4.MappingName = "Email"
        Me.DataGridTextBoxColumn4.Width = 75
        '
        'DataGridTextBoxColumn5
        '
        Me.DataGridTextBoxColumn5.Format = ""
        Me.DataGridTextBoxColumn5.FormatInfo = Nothing
        Me.DataGridTextBoxColumn5.HeaderText = "Congregation"
        Me.DataGridTextBoxColumn5.MappingName = "OrgName"
        Me.DataGridTextBoxColumn5.Width = 150
        '
        'DataGridTextBoxColumn6
        '
        Me.DataGridTextBoxColumn6.Format = ""
        Me.DataGridTextBoxColumn6.FormatInfo = Nothing
        Me.DataGridTextBoxColumn6.HeaderText = "Event"
        Me.DataGridTextBoxColumn6.MappingName = "EventName"
        Me.DataGridTextBoxColumn6.Width = 150
        '
        'tblRegOrderTableAdapter
        '
        Me.tblRegOrderTableAdapter.ClearBeforeFill = True
        '
        'taMainRegistOrder
        '
        Me.taMainRegistOrder.ClearBeforeFill = True
        '
        'TableAdapterManager
        '
        Me.TableAdapterManager.BackupDataSetBeforeUpdate = False
        Me.TableAdapterManager.Connection = Nothing
        Me.TableAdapterManager.taMainRegistOrder = Nothing
        Me.TableAdapterManager.UpdateOrder = InfoCtr.dsMainWOrderTableAdapters.TableAdapterManager.UpdateOrderOption.InsertUpdateDelete
        '
        'frmMainWOrder
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(989, 489)
        Me.Controls.Add(Me.dgrd)
        Me.Controls.Add(Me.MainWOrder2BindingNavigator)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.btNewReg)
        Me.Controls.Add(Me.RegistrarNameTextBox)
        Me.Controls.Add(RegistrarContactNumLabel)
        Me.Controls.Add(RegistrarNameLabel)
        Me.Controls.Add(Me.RegistrarContactNumTextBox)
        Me.Controls.Add(Me.RegistrarEmailTextBox)
        Me.Controls.Add(RegistrarEmailLabel)
        Me.Controls.Add(Label1)
        Me.Controls.Add(Me.txtSID)
        Me.Name = "frmMainWOrder"
        Me.Text = "ORDER DETAIL"
        CType(Me.MainWOrder2BindingNavigator, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MainWOrder2BindingNavigator.ResumeLayout(False)
        Me.MainWOrder2BindingNavigator.PerformLayout()
        CType(Me.MainWOrder2BindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dsMainWOrder1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.dgrd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsNewWRegistration, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents dsMainWOrder1 As InfoCtr.dsMainWOrder
    Friend WithEvents MainWOrder2BindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents taMainRegistOrder As InfoCtr.dsMainWOrderTableAdapters.taMainRegistOrder
    Friend WithEvents TableAdapterManager As InfoCtr.dsMainWOrderTableAdapters.TableAdapterManager
    Friend WithEvents MainWOrder2BindingNavigator As System.Windows.Forms.BindingNavigator
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
    Friend WithEvents MainEventOrderBindingNavigatorSaveItem As System.Windows.Forms.ToolStripButton
    Friend WithEvents fldOID As System.Windows.Forms.TextBox
    Friend WithEvents txtAmountOrderGross As System.Windows.Forms.TextBox
    Friend WithEvents RegistrarNameTextBox As System.Windows.Forms.TextBox
    Friend WithEvents RegistrarContactNumTextBox As System.Windows.Forms.TextBox
    Friend WithEvents txtAmountOrderTotal As System.Windows.Forms.TextBox
    Friend WithEvents RegistrarEmailTextBox As System.Windows.Forms.TextBox
    Friend WithEvents txtqtyRegs As System.Windows.Forms.TextBox
    Friend WithEvents txtOrderNotes As System.Windows.Forms.TextBox
    Friend WithEvents cboProposedPayment As InfoCtr.ComboBoxRelaxed
    Friend WithEvents txtSID As System.Windows.Forms.TextBox
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents miPayment As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents miRegistration As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents DtOrder As InfoCtr.DateTextBox
    Friend WithEvents cboOrderStatus As InfoCtr.ComboBoxRelaxed
    Friend WithEvents qtyRegsLabel As System.Windows.Forms.Label
    Friend WithEvents AmountOrderTotalLabel As System.Windows.Forms.Label
    Friend WithEvents btNewReg As System.Windows.Forms.Button
    Friend WithEvents dgrd As System.Windows.Forms.DataGrid
    Friend WithEvents DsNewWRegistration As InfoCtr.dsNewWRegistration
    Friend WithEvents DataGridTableStyle1 As System.Windows.Forms.DataGridTableStyle
    Friend WithEvents DataGridTextBoxColumn1 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn2 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn3 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn4 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn5 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn6 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents tblRegOrderTableAdapter As InfoCtr.dsNewWRegistrationTableAdapters.tblRegOrderTableAdapter
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
End Class
