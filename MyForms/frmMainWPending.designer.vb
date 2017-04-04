<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMainWPending
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
        Dim NotesLabel As System.Windows.Forms.Label
        Dim Label1 As System.Windows.Forms.Label
        Dim Label3 As System.Windows.Forms.Label
        Dim Label4 As System.Windows.Forms.Label
        Dim Label6 As System.Windows.Forms.Label
        Dim Label25 As System.Windows.Forms.Label
        Dim Label27 As System.Windows.Forms.Label
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMainWPending))
        Me.lblordergoto = New System.Windows.Forms.Label()
        Me.lblevent = New System.Windows.Forms.Label()
        Me.tblRegPendingBindingNavigator = New System.Windows.Forms.BindingNavigator(Me.components)
        Me.tblRegPendingBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.DsRegPending = New InfoCtr.dsRegPending()
        Me.BindingNavigatorCountItem = New System.Windows.Forms.ToolStripLabel()
        Me.BindingNavigatorMoveFirstItem = New System.Windows.Forms.ToolStripButton()
        Me.BindingNavigatorMovePreviousItem = New System.Windows.Forms.ToolStripButton()
        Me.BindingNavigatorSeparator = New System.Windows.Forms.ToolStripSeparator()
        Me.BindingNavigatorPositionItem = New System.Windows.Forms.ToolStripTextBox()
        Me.BindingNavigatorSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.BindingNavigatorMoveNextItem = New System.Windows.Forms.ToolStripButton()
        Me.BindingNavigatorMoveLastItem = New System.Windows.Forms.ToolStripButton()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.btnReject = New System.Windows.Forms.Button()
        Me.btnAccept = New System.Windows.Forms.Button()
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
        Me.miGotoOrder = New System.Windows.Forms.MenuItem()
        Me.miHelp = New System.Windows.Forms.MenuItem()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btnGotoSrchOrg = New System.Windows.Forms.Button()
        Me.txtSrchOrg = New System.Windows.Forms.TextBox()
        Me.txtSrchContact = New System.Windows.Forms.TextBox()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.chkWildcard = New System.Windows.Forms.CheckBox()
        Me.btnCopyEmail = New System.Windows.Forms.Button()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.cboSrchOrg = New InfoCtr.ComboBoxRelaxed()
        Me.cboSrchContact = New InfoCtr.ComboBoxRelaxed()
        Me.btnHelp = New System.Windows.Forms.Button()
        Me.ErrorProvider1 = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.SqlConnection1 = New System.Data.SqlClient.SqlConnection()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.fldEventID = New System.Windows.Forms.Label()
        Me.fldUID = New System.Windows.Forms.Label()
        Me.fldOID = New System.Windows.Forms.Label()
        Me.fldSID = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.fldName = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.txtMatched = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.dgOrg = New System.Windows.Forms.DataGrid()
        Me.DataGridTableStyle1 = New System.Windows.Forms.DataGridTableStyle()
        Me.DataGridTextBoxColumn1 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn2 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn3 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn9 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn4 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn5 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn6 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn7 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.OrgID = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.dgContact = New System.Windows.Forms.DataGrid()
        Me.DataGridTableStyle2 = New System.Windows.Forms.DataGridTableStyle()
        Me.btnOrgGo = New System.Windows.Forms.Button()
        Me.btnContactGo = New System.Windows.Forms.Button()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.lblWarning = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.fldEmail = New System.Windows.Forms.TextBox()
        Me.tblRegPendingTableAdapter = New InfoCtr.dsRegPendingTableAdapters.taRegistrPending()
        Me.cboRegion = New InfoCtr.ComboBoxRelaxed()
        RegDateLabel = New System.Windows.Forms.Label()
        NotesLabel = New System.Windows.Forms.Label()
        Label1 = New System.Windows.Forms.Label()
        Label3 = New System.Windows.Forms.Label()
        Label4 = New System.Windows.Forms.Label()
        Label6 = New System.Windows.Forms.Label()
        Label25 = New System.Windows.Forms.Label()
        Label27 = New System.Windows.Forms.Label()
        CType(Me.tblRegPendingBindingNavigator,System.ComponentModel.ISupportInitialize).BeginInit
        Me.tblRegPendingBindingNavigator.SuspendLayout
        CType(Me.tblRegPendingBindingSource,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.DsRegPending,System.ComponentModel.ISupportInitialize).BeginInit
        Me.Panel2.SuspendLayout
        CType(Me.ErrorProvider1,System.ComponentModel.ISupportInitialize).BeginInit
        Me.Panel1.SuspendLayout
        CType(Me.dgOrg,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.dgContact,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.SplitContainer1,System.ComponentModel.ISupportInitialize).BeginInit
        Me.SplitContainer1.Panel1.SuspendLayout
        Me.SplitContainer1.Panel2.SuspendLayout
        Me.SplitContainer1.SuspendLayout
        Me.SuspendLayout
        '
        'RegDateLabel
        '
        RegDateLabel.AutoSize = true
        RegDateLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 7!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        RegDateLabel.Location = New System.Drawing.Point(30, 304)
        RegDateLabel.Name = "RegDateLabel"
        RegDateLabel.Size = New System.Drawing.Size(91, 13)
        RegDateLabel.TabIndex = 11
        RegDateLabel.Text = "Registration Date:"
        RegDateLabel.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'NotesLabel
        '
        NotesLabel.AutoSize = true
        NotesLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 7!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        NotesLabel.Location = New System.Drawing.Point(10, 452)
        NotesLabel.Name = "NotesLabel"
        NotesLabel.Size = New System.Drawing.Size(62, 26)
        NotesLabel.TabIndex = 27
        NotesLabel.Text = "Registration"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"Comment:"
        NotesLabel.TextAlign = System.Drawing.ContentAlignment.TopRight
        NotesLabel.Visible = false
        '
        'Label1
        '
        Label1.AutoSize = true
        Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 7!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Label1.Location = New System.Drawing.Point(34, 14)
        Label1.Name = "Label1"
        Label1.Size = New System.Drawing.Size(37, 13)
        Label1.TabIndex = 526
        Label1.Text = "Name:"
        '
        'Label3
        '
        Label3.AutoSize = true
        Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 7!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Label3.Location = New System.Drawing.Point(37, 144)
        Label3.Name = "Label3"
        Label3.Size = New System.Drawing.Size(35, 13)
        Label3.TabIndex = 527
        Label3.Text = "Email:"
        '
        'Label4
        '
        Label4.AutoSize = true
        Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 7!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Label4.Location = New System.Drawing.Point(27, 91)
        Label4.Name = "Label4"
        Label4.Size = New System.Drawing.Size(48, 13)
        Label4.TabIndex = 528
        Label4.Text = "Address:"
        '
        'Label6
        '
        Label6.AutoSize = true
        Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 7!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Label6.Location = New System.Drawing.Point(6, 50)
        Label6.Name = "Label6"
        Label6.Size = New System.Drawing.Size(69, 13)
        Label6.TabIndex = 529
        Label6.Text = "Organization:"
        '
        'Label25
        '
        Label25.AutoSize = true
        Label25.Font = New System.Drawing.Font("Microsoft Sans Serif", 7!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Label25.Location = New System.Drawing.Point(12, 173)
        Label25.Name = "Label25"
        Label25.Size = New System.Drawing.Size(78, 13)
        Label25.TabIndex = 550
        Label25.Text = "Primary Phone:"
        '
        'Label27
        '
        Label27.AutoSize = true
        Label27.Font = New System.Drawing.Font("Microsoft Sans Serif", 7!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Label27.Location = New System.Drawing.Point(12, 197)
        Label27.Name = "Label27"
        Label27.Size = New System.Drawing.Size(94, 13)
        Label27.TabIndex = 552
        Label27.Text = "Secondary Phone:"
        '
        'lblordergoto
        '
        Me.lblordergoto.AutoSize = true
        Me.lblordergoto.Font = New System.Drawing.Font("Microsoft Sans Serif", 7!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblordergoto.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblordergoto.Location = New System.Drawing.Point(73, 328)
        Me.lblordergoto.Name = "lblordergoto"
        Me.lblordergoto.Size = New System.Drawing.Size(45, 13)
        Me.lblordergoto.TabIndex = 486
        Me.lblordergoto.Text = "Order #:"
        Me.ToolTip1.SetToolTip(Me.lblordergoto, "Required if payment covers more than one person/event.")
        '
        'lblevent
        '
        Me.lblevent.AutoSize = true
        Me.lblevent.Font = New System.Drawing.Font("Microsoft Sans Serif", 7!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblevent.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblevent.Location = New System.Drawing.Point(3, 262)
        Me.lblevent.Name = "lblevent"
        Me.lblevent.Size = New System.Drawing.Size(62, 13)
        Me.lblevent.TabIndex = 5
        Me.lblevent.Text = "Event SKU:"
        '
        'tblRegPendingBindingNavigator
        '
        Me.tblRegPendingBindingNavigator.AddNewItem = Nothing
        Me.tblRegPendingBindingNavigator.BindingSource = Me.tblRegPendingBindingSource
        Me.tblRegPendingBindingNavigator.CountItem = Me.BindingNavigatorCountItem
        Me.tblRegPendingBindingNavigator.DeleteItem = Nothing
        Me.tblRegPendingBindingNavigator.Dock = System.Windows.Forms.DockStyle.None
        Me.tblRegPendingBindingNavigator.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BindingNavigatorMoveFirstItem, Me.BindingNavigatorMovePreviousItem, Me.BindingNavigatorSeparator, Me.BindingNavigatorPositionItem, Me.BindingNavigatorCountItem, Me.BindingNavigatorSeparator1, Me.BindingNavigatorMoveNextItem, Me.BindingNavigatorMoveLastItem})
        Me.tblRegPendingBindingNavigator.Location = New System.Drawing.Point(15, 492)
        Me.tblRegPendingBindingNavigator.MoveFirstItem = Me.BindingNavigatorMoveFirstItem
        Me.tblRegPendingBindingNavigator.MoveLastItem = Me.BindingNavigatorMoveLastItem
        Me.tblRegPendingBindingNavigator.MoveNextItem = Me.BindingNavigatorMoveNextItem
        Me.tblRegPendingBindingNavigator.MovePreviousItem = Me.BindingNavigatorMovePreviousItem
        Me.tblRegPendingBindingNavigator.Name = "tblRegPendingBindingNavigator"
        Me.tblRegPendingBindingNavigator.PositionItem = Me.BindingNavigatorPositionItem
        Me.tblRegPendingBindingNavigator.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
        Me.tblRegPendingBindingNavigator.Size = New System.Drawing.Size(203, 25)
        Me.tblRegPendingBindingNavigator.TabIndex = 0
        Me.tblRegPendingBindingNavigator.Text = "BindingNavigator1"
        '
        'tblRegPendingBindingSource
        '
        Me.tblRegPendingBindingSource.DataMember = "tblEventRegPending2"
        Me.tblRegPendingBindingSource.DataSource = Me.DsRegPending
        '
        'DsRegPending
        '
        Me.DsRegPending.DataSetName = "dsRegPending"
        Me.DsRegPending.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
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
        Me.BindingNavigatorMoveFirstItem.Image = CType(resources.GetObject("BindingNavigatorMoveFirstItem.Image"),System.Drawing.Image)
        Me.BindingNavigatorMoveFirstItem.Name = "BindingNavigatorMoveFirstItem"
        Me.BindingNavigatorMoveFirstItem.RightToLeftAutoMirrorImage = true
        Me.BindingNavigatorMoveFirstItem.Size = New System.Drawing.Size(23, 22)
        Me.BindingNavigatorMoveFirstItem.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        '
        'BindingNavigatorMovePreviousItem
        '
        Me.BindingNavigatorMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorMovePreviousItem.Image = CType(resources.GetObject("BindingNavigatorMovePreviousItem.Image"),System.Drawing.Image)
        Me.BindingNavigatorMovePreviousItem.Name = "BindingNavigatorMovePreviousItem"
        Me.BindingNavigatorMovePreviousItem.RightToLeftAutoMirrorImage = true
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
        Me.BindingNavigatorPositionItem.AutoSize = false
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
        Me.BindingNavigatorMoveNextItem.Image = CType(resources.GetObject("BindingNavigatorMoveNextItem.Image"),System.Drawing.Image)
        Me.BindingNavigatorMoveNextItem.Name = "BindingNavigatorMoveNextItem"
        Me.BindingNavigatorMoveNextItem.RightToLeftAutoMirrorImage = true
        Me.BindingNavigatorMoveNextItem.Size = New System.Drawing.Size(23, 22)
        Me.BindingNavigatorMoveNextItem.Text = "Move next"
        '
        'BindingNavigatorMoveLastItem
        '
        Me.BindingNavigatorMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorMoveLastItem.Image = CType(resources.GetObject("BindingNavigatorMoveLastItem.Image"),System.Drawing.Image)
        Me.BindingNavigatorMoveLastItem.Name = "BindingNavigatorMoveLastItem"
        Me.BindingNavigatorMoveLastItem.RightToLeftAutoMirrorImage = true
        Me.BindingNavigatorMoveLastItem.Size = New System.Drawing.Size(23, 22)
        Me.BindingNavigatorMoveLastItem.Text = "Move last"
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.DarkBlue
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Controls.Add(Me.btnReject)
        Me.Panel2.Controls.Add(Me.btnAccept)
        Me.Panel2.ForeColor = System.Drawing.Color.DarkBlue
        Me.Panel2.Location = New System.Drawing.Point(754, 93)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(232, 54)
        Me.Panel2.TabIndex = 424
        '
        'btnReject
        '
        Me.btnReject.Enabled = false
        Me.btnReject.Location = New System.Drawing.Point(132, 5)
        Me.btnReject.Name = "btnReject"
        Me.btnReject.Size = New System.Drawing.Size(95, 40)
        Me.btnReject.TabIndex = 248
        Me.btnReject.Text = "REJECT Registration"
        Me.btnReject.UseVisualStyleBackColor = true
        '
        'btnAccept
        '
        Me.btnAccept.BackColor = System.Drawing.SystemColors.Control
        Me.btnAccept.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.btnAccept.Location = New System.Drawing.Point(8, 4)
        Me.btnAccept.Name = "btnAccept"
        Me.btnAccept.Size = New System.Drawing.Size(109, 42)
        Me.btnAccept.TabIndex = 247
        Me.btnAccept.TabStop = false
        Me.btnAccept.Text = "ACCEPT Registration"
        Me.btnAccept.UseVisualStyleBackColor = true
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
        Me.miSave.Enabled = false
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
        Me.miCopy.Enabled = false
        Me.miCopy.Index = 4
        Me.miCopy.Text = "Copy Registration to Another Event"
        '
        'MenuItem3
        '
        Me.MenuItem3.Index = 2
        Me.MenuItem3.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.miMultiple, Me.miGotoOrder})
        Me.MenuItem3.Text = "View"
        '
        'miMultiple
        '
        Me.miMultiple.Enabled = false
        Me.miMultiple.Index = 0
        Me.miMultiple.Text = "Series Registrations"
        '
        'miGotoOrder
        '
        Me.miGotoOrder.Enabled = false
        Me.miGotoOrder.Index = 1
        Me.miGotoOrder.Text = "Go to Order Detail"
        '
        'miHelp
        '
        Me.miHelp.Index = 3
        Me.miHelp.Text = "Help"
        '
        'Label2
        '
        Me.Label2.AutoSize = true
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 7!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(8, 352)
        Me.Label2.Margin = New System.Windows.Forms.Padding(0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(110, 26)
        Me.Label2.TabIndex = 525
        Me.Label2.Text = "Online Confirmation #:"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"(SID)"
        Me.ToolTip1.SetToolTip(Me.Label2, "from Website registration")
        '
        'btnGotoSrchOrg
        '
        Me.btnGotoSrchOrg.BackColor = System.Drawing.SystemColors.InactiveBorder
        Me.btnGotoSrchOrg.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.btnGotoSrchOrg.Image = CType(resources.GetObject("btnGotoSrchOrg.Image"),System.Drawing.Image)
        Me.btnGotoSrchOrg.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnGotoSrchOrg.Location = New System.Drawing.Point(969, 7)
        Me.btnGotoSrchOrg.Name = "btnGotoSrchOrg"
        Me.btnGotoSrchOrg.Size = New System.Drawing.Size(128, 49)
        Me.btnGotoSrchOrg.TabIndex = 540
        Me.btnGotoSrchOrg.Text = "Search Contacts"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"  &Organizations"
        Me.btnGotoSrchOrg.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ToolTip1.SetToolTip(Me.btnGotoSrchOrg, "Find Congregations and People, or add new ones.")
        Me.btnGotoSrchOrg.UseVisualStyleBackColor = false
        '
        'txtSrchOrg
        '
        Me.txtSrchOrg.Location = New System.Drawing.Point(303, 2)
        Me.txtSrchOrg.Name = "txtSrchOrg"
        Me.txtSrchOrg.Size = New System.Drawing.Size(213, 20)
        Me.txtSrchOrg.TabIndex = 5
        Me.txtSrchOrg.Text = "enter search text here then press Enter or Tab"
        Me.ToolTip1.SetToolTip(Me.txtSrchOrg, "Tab to exit.  Use * for wildcard.")
        Me.txtSrchOrg.WordWrap = false
        '
        'txtSrchContact
        '
        Me.txtSrchContact.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtSrchContact.Location = New System.Drawing.Point(303, 5)
        Me.txtSrchContact.Name = "txtSrchContact"
        Me.txtSrchContact.Size = New System.Drawing.Size(213, 20)
        Me.txtSrchContact.TabIndex = 15
        Me.txtSrchContact.Text = "enter search text here then press Enter or Tab"
        Me.ToolTip1.SetToolTip(Me.txtSrchContact, "Tab to exit.  Use * for wildcard.")
        Me.txtSrchContact.WordWrap = false
        '
        'Label21
        '
        Me.Label21.AutoSize = true
        Me.Label21.Font = New System.Drawing.Font("Microsoft Sans Serif", 7!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label21.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label21.Location = New System.Drawing.Point(33, 378)
        Me.Label21.Margin = New System.Windows.Forms.Padding(0)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(85, 26)
        Me.Label21.TabIndex = 539
        Me.Label21.Text = "Online Person #:"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"(UID)"
        Me.ToolTip1.SetToolTip(Me.Label21, "Required if payment covers more than one person/event.")
        '
        'Label20
        '
        Me.Label20.AutoSize = true
        Me.Label20.Font = New System.Drawing.Font("Microsoft Sans Serif", 7!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label20.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label20.Location = New System.Drawing.Point(52, 404)
        Me.Label20.Margin = New System.Windows.Forms.Padding(0)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(65, 13)
        Me.Label20.TabIndex = 543
        Me.Label20.Text = "our Event #:"
        Me.ToolTip1.SetToolTip(Me.Label20, "Required if payment covers more than one person/event.")
        '
        'chkWildcard
        '
        Me.chkWildcard.AutoSize = true
        Me.chkWildcard.Checked = true
        Me.chkWildcard.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkWildcard.Location = New System.Drawing.Point(1028, 166)
        Me.chkWildcard.Name = "chkWildcard"
        Me.chkWildcard.Size = New System.Drawing.Size(81, 17)
        Me.chkWildcard.TabIndex = 542
        Me.chkWildcard.Text = "Wildcard (*)"
        Me.ToolTip1.SetToolTip(Me.chkWildcard, "automatic wildcard before and after what you type to search on.")
        Me.chkWildcard.UseVisualStyleBackColor = true
        '
        'btnCopyEmail
        '
        Me.btnCopyEmail.Location = New System.Drawing.Point(777, 160)
        Me.btnCopyEmail.Name = "btnCopyEmail"
        Me.btnCopyEmail.Size = New System.Drawing.Size(75, 23)
        Me.btnCopyEmail.TabIndex = 548
        Me.btnCopyEmail.TabStop = false
        Me.btnCopyEmail.Text = "Copy Email"
        Me.ToolTip1.SetToolTip(Me.btnCopyEmail, "Copy email registrant entered online.")
        Me.btnCopyEmail.UseVisualStyleBackColor = true
        '
        'Label29
        '
        Me.Label29.AutoSize = true
        Me.Label29.Font = New System.Drawing.Font("Microsoft Sans Serif", 7!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label29.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label29.Location = New System.Drawing.Point(43, 430)
        Me.Label29.Margin = New System.Windows.Forms.Padding(0)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(74, 13)
        Me.Label29.TabIndex = 554
        Me.Label29.Text = "Event Region:"
        Me.ToolTip1.SetToolTip(Me.Label29, "Required if payment covers more than one person/event.")
        '
        'cboSrchOrg
        '
        Me.cboSrchOrg.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboSrchOrg.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboSrchOrg.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSrchOrg.FormattingEnabled = true
        Me.cboSrchOrg.Items.AddRange(New Object() {"OrgName", "City", "Email", "Street", "Zip"})
        Me.cboSrchOrg.Location = New System.Drawing.Point(172, 3)
        Me.cboSrchOrg.Name = "cboSrchOrg"
        Me.cboSrchOrg.RestrictContentToListItems = true
        Me.cboSrchOrg.Size = New System.Drawing.Size(125, 21)
        Me.cboSrchOrg.TabIndex = 3
        Me.cboSrchOrg.TabStop = false
        Me.ToolTip1.SetToolTip(Me.cboSrchOrg, "select field to search")
        '
        'cboSrchContact
        '
        Me.cboSrchContact.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboSrchContact.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboSrchContact.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSrchContact.FormattingEnabled = true
        Me.cboSrchContact.Items.AddRange(New Object() {"LastName", "FirstName", "City", "Email", "Street", "Zip"})
        Me.cboSrchContact.Location = New System.Drawing.Point(138, 5)
        Me.cboSrchContact.Name = "cboSrchContact"
        Me.cboSrchContact.RestrictContentToListItems = true
        Me.cboSrchContact.Size = New System.Drawing.Size(125, 21)
        Me.cboSrchContact.TabIndex = 7
        Me.cboSrchContact.TabStop = false
        Me.ToolTip1.SetToolTip(Me.cboSrchContact, "select field to search")
        '
        'btnHelp
        '
        Me.btnHelp.BackColor = System.Drawing.SystemColors.Control
        Me.btnHelp.Font = New System.Drawing.Font("Microsoft Sans Serif", 22!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.btnHelp.ForeColor = System.Drawing.SystemColors.Control
        Me.btnHelp.Image = CType(resources.GetObject("btnHelp.Image"),System.Drawing.Image)
        Me.btnHelp.Location = New System.Drawing.Point(919, 7)
        Me.btnHelp.Name = "btnHelp"
        Me.btnHelp.Size = New System.Drawing.Size(31, 30)
        Me.btnHelp.TabIndex = 423
        Me.btnHelp.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnHelp.UseVisualStyleBackColor = false
        '
        'ErrorProvider1
        '
        Me.ErrorProvider1.ContainerControl = Me
        '
        'SqlConnection1
        '
        Me.SqlConnection1.ConnectionString = "Data Source=SOLOMON;Initial Catalog=InfoCtr_be;Integrated Security=True"
        Me.SqlConnection1.FireInfoMessageEventOnUserErrors = false
        '
        'Label5
        '
        Me.Label5.AutoSize = true
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label5.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label5.Location = New System.Drawing.Point(5, 5)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(161, 13)
        Me.Label5.TabIndex = 504
        Me.Label5.Text = "Search for ORGANIZATION by: "
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.SystemColors.Control
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 10!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label8.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label8.Location = New System.Drawing.Point(40, 29)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(229, 16)
        Me.Label8.TabIndex = 429
        Me.Label8.Text = "PENDING REGISTRATIONS"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(250,Byte),Integer), CType(CType(255,Byte),Integer), CType(CType(230,Byte),Integer))
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel1.Controls.Add(Me.Label28)
        Me.Panel1.Controls.Add(Me.Label29)
        Me.Panel1.Controls.Add(Me.Label26)
        Me.Panel1.Controls.Add(Label27)
        Me.Panel1.Controls.Add(Me.Label22)
        Me.Panel1.Controls.Add(Label25)
        Me.Panel1.Controls.Add(Me.Label23)
        Me.Panel1.Controls.Add(Me.Label17)
        Me.Panel1.Controls.Add(Me.Label18)
        Me.Panel1.Controls.Add(Me.Label16)
        Me.Panel1.Controls.Add(Me.fldEventID)
        Me.Panel1.Controls.Add(Me.Label20)
        Me.Panel1.Controls.Add(Me.fldUID)
        Me.Panel1.Controls.Add(Me.Label21)
        Me.Panel1.Controls.Add(Me.fldOID)
        Me.Panel1.Controls.Add(Me.fldSID)
        Me.Panel1.Controls.Add(Me.Label15)
        Me.Panel1.Controls.Add(Me.Label13)
        Me.Panel1.Controls.Add(Me.Label12)
        Me.Panel1.Controls.Add(Me.Label11)
        Me.Panel1.Controls.Add(Me.Label10)
        Me.Panel1.Controls.Add(Me.fldName)
        Me.Panel1.Controls.Add(Me.Label7)
        Me.Panel1.Controls.Add(Label6)
        Me.Panel1.Controls.Add(Label4)
        Me.Panel1.Controls.Add(Label3)
        Me.Panel1.Controls.Add(Label1)
        Me.Panel1.Controls.Add(Me.lblevent)
        Me.Panel1.Controls.Add(RegDateLabel)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.lblordergoto)
        Me.Panel1.Controls.Add(NotesLabel)
        Me.Panel1.Controls.Add(Me.tblRegPendingBindingNavigator)
        Me.Panel1.Location = New System.Drawing.Point(12, 93)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(266, 539)
        Me.Panel1.TabIndex = 526
        '
        'Label28
        '
        Me.Label28.AutoSize = true
        Me.Label28.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.tblRegPendingBindingSource, "SatelliteRegion", true))
        Me.Label28.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label28.Location = New System.Drawing.Point(127, 430)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(78, 13)
        Me.Label28.TabIndex = 555
        Me.Label28.Text = "SatelliteRegion"
        Me.Label28.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label26
        '
        Me.Label26.AutoSize = true
        Me.Label26.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.tblRegPendingBindingSource, "SecondaryPhone", true))
        Me.Label26.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label26.Location = New System.Drawing.Point(110, 195)
        Me.Label26.MaximumSize = New System.Drawing.Size(120, 0)
        Me.Label26.MinimumSize = New System.Drawing.Size(120, 0)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(120, 15)
        Me.Label26.TabIndex = 553
        '
        'Label22
        '
        Me.Label22.AutoSize = true
        Me.Label22.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.tblRegPendingBindingSource, "PrimaryPhone", true))
        Me.Label22.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label22.Location = New System.Drawing.Point(110, 171)
        Me.Label22.MaximumSize = New System.Drawing.Size(120, 0)
        Me.Label22.MinimumSize = New System.Drawing.Size(120, 0)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(120, 15)
        Me.Label22.TabIndex = 551
        '
        'Label23
        '
        Me.Label23.AutoSize = true
        Me.Label23.Font = New System.Drawing.Font("Microsoft Sans Serif", 7!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label23.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label23.Location = New System.Drawing.Point(0, 242)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(64, 13)
        Me.Label23.TabIndex = 549
        Me.Label23.Text = "Event Date:"
        '
        'Label17
        '
        Me.Label17.AutoSize = true
        Me.Label17.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label17.Location = New System.Drawing.Point(78, 222)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(82, 13)
        Me.Label17.TabIndex = 548
        Me.Label17.Text = "-------------------------"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label18
        '
        Me.Label18.AutoSize = true
        Me.Label18.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.tblRegPendingBindingSource, "FirstDate", true))
        Me.Label18.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label18.Location = New System.Drawing.Point(64, 243)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(30, 13)
        Me.Label18.TabIndex = 547
        Me.Label18.Text = "Date"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label16
        '
        Me.Label16.AutoSize = true
        Me.Label16.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.tblRegPendingBindingSource, "GoesBy", true))
        Me.Label16.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label16.Location = New System.Drawing.Point(127, 29)
        Me.Label16.MaximumSize = New System.Drawing.Size(175, 0)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(51, 15)
        Me.Label16.TabIndex = 545
        Me.Label16.Text = "Goes by"
        '
        'fldEventID
        '
        Me.fldEventID.AutoSize = true
        Me.fldEventID.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.tblRegPendingBindingSource, "MatchEventID", true))
        Me.fldEventID.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.fldEventID.Location = New System.Drawing.Point(127, 404)
        Me.fldEventID.Name = "fldEventID"
        Me.fldEventID.Size = New System.Drawing.Size(46, 13)
        Me.fldEventID.TabIndex = 544
        Me.fldEventID.Text = "EventID"
        Me.fldEventID.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'fldUID
        '
        Me.fldUID.AutoSize = true
        Me.fldUID.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.tblRegPendingBindingSource, "UID", true))
        Me.fldUID.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.fldUID.Location = New System.Drawing.Point(128, 379)
        Me.fldUID.Name = "fldUID"
        Me.fldUID.Size = New System.Drawing.Size(26, 13)
        Me.fldUID.TabIndex = 540
        Me.fldUID.Text = "UID"
        Me.fldUID.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'fldOID
        '
        Me.fldOID.AutoSize = true
        Me.fldOID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.fldOID.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.tblRegPendingBindingSource, "OID", true))
        Me.fldOID.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.fldOID.Location = New System.Drawing.Point(128, 329)
        Me.fldOID.Name = "fldOID"
        Me.fldOID.Padding = New System.Windows.Forms.Padding(2)
        Me.fldOID.Size = New System.Drawing.Size(43, 19)
        Me.fldOID.TabIndex = 538
        Me.fldOID.Text = "00000"
        Me.fldOID.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'fldSID
        '
        Me.fldSID.AutoSize = true
        Me.fldSID.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.tblRegPendingBindingSource, "SID", true))
        Me.fldSID.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.fldSID.Location = New System.Drawing.Point(128, 354)
        Me.fldSID.Name = "fldSID"
        Me.fldSID.Size = New System.Drawing.Size(25, 13)
        Me.fldSID.TabIndex = 537
        Me.fldSID.Text = "SID"
        Me.fldSID.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label15
        '
        Me.Label15.AutoSize = true
        Me.Label15.BackColor = System.Drawing.Color.Transparent
        Me.Label15.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.tblRegPendingBindingSource, "DateRegister", true))
        Me.Label15.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label15.Location = New System.Drawing.Point(127, 304)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(0, 13)
        Me.Label15.TabIndex = 536
        '
        'Label13
        '
        Me.Label13.AutoSize = true
        Me.Label13.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.tblRegPendingBindingSource, "Address", true))
        Me.Label13.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label13.Location = New System.Drawing.Point(74, 90)
        Me.Label13.MaximumSize = New System.Drawing.Size(175, 0)
        Me.Label13.MinimumSize = New System.Drawing.Size(165, 50)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(165, 50)
        Me.Label13.TabIndex = 535
        '
        'Label12
        '
        Me.Label12.AutoSize = true
        Me.Label12.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.tblRegPendingBindingSource, "Company", true))
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label12.Location = New System.Drawing.Point(74, 49)
        Me.Label12.MaximumSize = New System.Drawing.Size(175, 0)
        Me.Label12.MinimumSize = New System.Drawing.Size(165, 26)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(165, 26)
        Me.Label12.TabIndex = 534
        '
        'Label11
        '
        Me.Label11.AutoSize = true
        Me.Label11.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.tblRegPendingBindingSource, "Email", true))
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label11.Location = New System.Drawing.Point(74, 143)
        Me.Label11.MaximumSize = New System.Drawing.Size(185, 0)
        Me.Label11.MinimumSize = New System.Drawing.Size(175, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(175, 15)
        Me.Label11.TabIndex = 533
        '
        'Label10
        '
        Me.Label10.AutoSize = true
        Me.Label10.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.tblRegPendingBindingSource, "Notes", true))
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label10.Location = New System.Drawing.Point(93, 453)
        Me.Label10.MaximumSize = New System.Drawing.Size(175, 25)
        Me.Label10.MinimumSize = New System.Drawing.Size(100, 25)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(100, 25)
        Me.Label10.TabIndex = 532
        Me.Label10.Text = "Label10"
        Me.Label10.Visible = false
        '
        'fldName
        '
        Me.fldName.AutoSize = true
        Me.fldName.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.tblRegPendingBindingSource, "Name", true))
        Me.fldName.Font = New System.Drawing.Font("Microsoft Sans Serif", 10!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.fldName.Location = New System.Drawing.Point(74, 12)
        Me.fldName.MaximumSize = New System.Drawing.Size(175, 0)
        Me.fldName.MinimumSize = New System.Drawing.Size(165, 0)
        Me.fldName.Name = "fldName"
        Me.fldName.Size = New System.Drawing.Size(165, 17)
        Me.fldName.TabIndex = 531
        '
        'Label7
        '
        Me.Label7.AutoSize = true
        Me.Label7.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.tblRegPendingBindingSource, "EventSKU", true))
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label7.Location = New System.Drawing.Point(64, 262)
        Me.Label7.MaximumSize = New System.Drawing.Size(185, 30)
        Me.Label7.MinimumSize = New System.Drawing.Size(185, 20)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(185, 20)
        Me.Label7.TabIndex = 530
        Me.Label7.Text = "SKU"
        '
        'Label19
        '
        Me.Label19.AutoSize = true
        Me.Label19.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label19.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label19.Location = New System.Drawing.Point(5, 8)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(127, 13)
        Me.Label19.TabIndex = 533
        Me.Label19.Text = "Search for CONTACT by:"
        '
        'txtMatched
        '
        Me.txtMatched.BackColor = System.Drawing.Color.FromArgb(CType(CType(250,Byte),Integer), CType(CType(255,Byte),Integer), CType(CType(230,Byte),Integer))
        Me.txtMatched.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtMatched.ForeColor = System.Drawing.Color.SaddleBrown
        Me.txtMatched.Location = New System.Drawing.Point(282, 93)
        Me.txtMatched.Multiline = true
        Me.txtMatched.Name = "txtMatched"
        Me.txtMatched.ReadOnly = true
        Me.txtMatched.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtMatched.Size = New System.Drawing.Size(450, 115)
        Me.txtMatched.TabIndex = 534
        Me.txtMatched.TabStop = false
        '
        'Label14
        '
        Me.Label14.AutoSize = true
        Me.Label14.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label14.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label14.Location = New System.Drawing.Point(449, 74)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(71, 13)
        Me.Label14.TabIndex = 535
        Me.Label14.Text = "Your match"
        '
        'dgOrg
        '
        Me.dgOrg.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
            Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.dgOrg.DataMember = ""
        Me.dgOrg.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.dgOrg.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.dgOrg.Location = New System.Drawing.Point(8, 29)
        Me.dgOrg.Name = "dgOrg"
        Me.dgOrg.ReadOnly = true
        Me.dgOrg.RowHeaderWidth = 10
        Me.dgOrg.Size = New System.Drawing.Size(838, 116)
        Me.dgOrg.TabIndex = 10
        Me.dgOrg.TableStyles.AddRange(New System.Windows.Forms.DataGridTableStyle() {Me.DataGridTableStyle1})
        Me.dgOrg.TabStop = false
        '
        'DataGridTableStyle1
        '
        Me.DataGridTableStyle1.DataGrid = Me.dgOrg
        Me.DataGridTableStyle1.GridColumnStyles.AddRange(New System.Windows.Forms.DataGridColumnStyle() {Me.DataGridTextBoxColumn1, Me.DataGridTextBoxColumn2, Me.DataGridTextBoxColumn3, Me.DataGridTextBoxColumn9, Me.DataGridTextBoxColumn4, Me.DataGridTextBoxColumn5, Me.DataGridTextBoxColumn6, Me.DataGridTextBoxColumn7, Me.OrgID})
        Me.DataGridTableStyle1.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.DataGridTableStyle1.MappingName = "tblOrgs"
        '
        'DataGridTextBoxColumn1
        '
        Me.DataGridTextBoxColumn1.Format = ""
        Me.DataGridTextBoxColumn1.FormatInfo = Nothing
        Me.DataGridTextBoxColumn1.HeaderText = "OrgName"
        Me.DataGridTextBoxColumn1.MappingName = "OrgName"
        Me.DataGridTextBoxColumn1.Width = 180
        '
        'DataGridTextBoxColumn2
        '
        Me.DataGridTextBoxColumn2.Format = ""
        Me.DataGridTextBoxColumn2.FormatInfo = Nothing
        Me.DataGridTextBoxColumn2.HeaderText = "MailingAddress"
        Me.DataGridTextBoxColumn2.MappingName = "Street1"
        Me.DataGridTextBoxColumn2.Width = 150
        '
        'DataGridTextBoxColumn3
        '
        Me.DataGridTextBoxColumn3.Format = ""
        Me.DataGridTextBoxColumn3.FormatInfo = Nothing
        Me.DataGridTextBoxColumn3.HeaderText = "City"
        Me.DataGridTextBoxColumn3.MappingName = "City"
        Me.DataGridTextBoxColumn3.Width = 50
        '
        'DataGridTextBoxColumn9
        '
        Me.DataGridTextBoxColumn9.Format = ""
        Me.DataGridTextBoxColumn9.FormatInfo = Nothing
        Me.DataGridTextBoxColumn9.HeaderText = "State"
        Me.DataGridTextBoxColumn9.MappingName = "State"
        Me.DataGridTextBoxColumn9.Width = 0
        '
        'DataGridTextBoxColumn4
        '
        Me.DataGridTextBoxColumn4.Format = ""
        Me.DataGridTextBoxColumn4.FormatInfo = Nothing
        Me.DataGridTextBoxColumn4.HeaderText = "Zip"
        Me.DataGridTextBoxColumn4.MappingName = "Zip"
        Me.DataGridTextBoxColumn4.Width = 75
        '
        'DataGridTextBoxColumn5
        '
        Me.DataGridTextBoxColumn5.Format = ""
        Me.DataGridTextBoxColumn5.FormatInfo = Nothing
        Me.DataGridTextBoxColumn5.HeaderText = "Email"
        Me.DataGridTextBoxColumn5.MappingName = "Email"
        Me.DataGridTextBoxColumn5.Width = 75
        '
        'DataGridTextBoxColumn6
        '
        Me.DataGridTextBoxColumn6.Format = ""
        Me.DataGridTextBoxColumn6.FormatInfo = Nothing
        Me.DataGridTextBoxColumn6.HeaderText = "Phone"
        Me.DataGridTextBoxColumn6.MappingName = "Phone"
        Me.DataGridTextBoxColumn6.Width = 75
        '
        'DataGridTextBoxColumn7
        '
        Me.DataGridTextBoxColumn7.Format = ""
        Me.DataGridTextBoxColumn7.FormatInfo = Nothing
        Me.DataGridTextBoxColumn7.HeaderText = "Physical"
        Me.DataGridTextBoxColumn7.MappingName = "StreetAddress"
        Me.DataGridTextBoxColumn7.Width = 75
        '
        'OrgID
        '
        Me.OrgID.Format = ""
        Me.OrgID.FormatInfo = Nothing
        Me.OrgID.HeaderText = "OrgID"
        Me.OrgID.MappingName = "OrgID"
        Me.OrgID.Width = 75
        '
        'dgContact
        '
        Me.dgContact.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
            Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.dgContact.DataMember = ""
        Me.dgContact.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.dgContact.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.dgContact.Location = New System.Drawing.Point(8, 33)
        Me.dgContact.Name = "dgContact"
        Me.dgContact.ReadOnly = true
        Me.dgContact.RowHeaderWidth = 10
        Me.dgContact.Size = New System.Drawing.Size(838, 227)
        Me.dgContact.TabIndex = 20
        Me.dgContact.TableStyles.AddRange(New System.Windows.Forms.DataGridTableStyle() {Me.DataGridTableStyle2})
        Me.dgContact.TabStop = false
        '
        'DataGridTableStyle2
        '
        Me.DataGridTableStyle2.DataGrid = Me.dgContact
        Me.DataGridTableStyle2.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.DataGridTableStyle2.MappingName = "tblContacts"
        Me.DataGridTableStyle2.ReadOnly = true
        '
        'btnOrgGo
        '
        Me.btnOrgGo.Location = New System.Drawing.Point(456, 19)
        Me.btnOrgGo.Name = "btnOrgGo"
        Me.btnOrgGo.Size = New System.Drawing.Size(3, 3)
        Me.btnOrgGo.TabIndex = 6
        Me.btnOrgGo.Text = "Button1"
        Me.btnOrgGo.UseVisualStyleBackColor = true
        '
        'btnContactGo
        '
        Me.btnContactGo.Location = New System.Drawing.Point(456, 15)
        Me.btnContactGo.Name = "btnContactGo"
        Me.btnContactGo.Size = New System.Drawing.Size(3, 3)
        Me.btnContactGo.TabIndex = 16
        Me.btnContactGo.Text = "Button1"
        Me.btnContactGo.UseVisualStyleBackColor = true
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.SplitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.SplitContainer1.Location = New System.Drawing.Point(284, 214)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.dgOrg)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Label5)
        Me.SplitContainer1.Panel1.Controls.Add(Me.cboSrchOrg)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnOrgGo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtSrchOrg)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.dgContact)
        Me.SplitContainer1.Panel2.Controls.Add(Me.cboSrchContact)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnContactGo)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Label19)
        Me.SplitContainer1.Panel2.Controls.Add(Me.txtSrchContact)
        Me.SplitContainer1.Size = New System.Drawing.Size(864, 423)
        Me.SplitContainer1.SplitterDistance = 152
        Me.SplitContainer1.TabIndex = 1
        Me.SplitContainer1.TabStop = false
        '
        'Label9
        '
        Me.Label9.AutoSize = true
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label9.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label9.Location = New System.Drawing.Point(24, 74)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(217, 13)
        Me.Label9.TabIndex = 543
        Me.Label9.Text = "Pending Registrations from download"
        '
        'lblWarning
        '
        Me.lblWarning.AutoSize = true
        Me.lblWarning.BackColor = System.Drawing.Color.Yellow
        Me.lblWarning.Font = New System.Drawing.Font("Microsoft Sans Serif", 10!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblWarning.Location = New System.Drawing.Point(162, 23)
        Me.lblWarning.Name = "lblWarning"
        Me.lblWarning.Size = New System.Drawing.Size(387, 17)
        Me.lblWarning.TabIndex = 544
        Me.lblWarning.Text = "WARNING:  This registration already assigned to contact # """
        Me.lblWarning.Visible = false
        '
        'Label24
        '
        Me.Label24.AutoSize = true
        Me.Label24.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label24.ForeColor = System.Drawing.Color.Maroon
        Me.Label24.Location = New System.Drawing.Point(555, 7)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(334, 75)
        Me.Label24.TabIndex = 546
        Me.Label24.Text = resources.GetString("Label24.Text")
        '
        'fldEmail
        '
        Me.fldEmail.BackColor = System.Drawing.SystemColors.Control
        Me.fldEmail.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.fldEmail.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.tblRegPendingBindingSource, "Email", true))
        Me.fldEmail.ForeColor = System.Drawing.SystemColors.Control
        Me.fldEmail.Location = New System.Drawing.Point(754, 189)
        Me.fldEmail.Name = "fldEmail"
        Me.fldEmail.Size = New System.Drawing.Size(232, 13)
        Me.fldEmail.TabIndex = 547
        '
        'tblRegPendingTableAdapter
        '
        Me.tblRegPendingTableAdapter.ClearBeforeFill = true
        '
        'cboRegion
        '
        Me.cboRegion.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboRegion.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboRegion.FormattingEnabled = true
        Me.cboRegion.Items.AddRange(New Object() {"Central", "NE", "NW", "SE", "SW", "All Indiana"})
        Me.cboRegion.Location = New System.Drawing.Point(29, 26)
        Me.cboRegion.Name = "cboRegion"
        Me.cboRegion.RestrictContentToListItems = true
        Me.cboRegion.Size = New System.Drawing.Size(124, 21)
        Me.cboRegion.TabIndex = 0
        '
        'frmMainWPending
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoScroll = true
        Me.ClientSize = New System.Drawing.Size(1275, 657)
        Me.Controls.Add(Me.btnCopyEmail)
        Me.Controls.Add(Me.fldEmail)
        Me.Controls.Add(Me.Label24)
        Me.Controls.Add(Me.lblWarning)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.chkWildcard)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.btnGotoSrchOrg)
        Me.Controls.Add(Me.cboRegion)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.btnHelp)
        Me.Controls.Add(Me.txtMatched)
        Me.Menu = Me.MainMenu1
        Me.Name = "frmMainWPending"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Tag = "PENDING"
        Me.Text = "W. PENDING REGISTRATIONS"
        CType(Me.tblRegPendingBindingNavigator,System.ComponentModel.ISupportInitialize).EndInit
        Me.tblRegPendingBindingNavigator.ResumeLayout(false)
        Me.tblRegPendingBindingNavigator.PerformLayout
        CType(Me.tblRegPendingBindingSource,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.DsRegPending,System.ComponentModel.ISupportInitialize).EndInit
        Me.Panel2.ResumeLayout(false)
        CType(Me.ErrorProvider1,System.ComponentModel.ISupportInitialize).EndInit
        Me.Panel1.ResumeLayout(false)
        Me.Panel1.PerformLayout
        CType(Me.dgOrg,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.dgContact,System.ComponentModel.ISupportInitialize).EndInit
        Me.SplitContainer1.Panel1.ResumeLayout(false)
        Me.SplitContainer1.Panel1.PerformLayout
        Me.SplitContainer1.Panel2.ResumeLayout(false)
        Me.SplitContainer1.Panel2.PerformLayout
        CType(Me.SplitContainer1,System.ComponentModel.ISupportInitialize).EndInit
        Me.SplitContainer1.ResumeLayout(false)
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub




    Friend WithEvents tblRegPendingBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents tblRegPendingBindingNavigator As System.Windows.Forms.BindingNavigator
    Friend WithEvents BindingNavigatorCountItem As System.Windows.Forms.ToolStripLabel
    Friend WithEvents BindingNavigatorMoveFirstItem As System.Windows.Forms.ToolStripButton
    Friend WithEvents BindingNavigatorMovePreviousItem As System.Windows.Forms.ToolStripButton
    Friend WithEvents BindingNavigatorSeparator As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents BindingNavigatorPositionItem As System.Windows.Forms.ToolStripTextBox
    Friend WithEvents BindingNavigatorSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents BindingNavigatorMoveNextItem As System.Windows.Forms.ToolStripButton
    Friend WithEvents BindingNavigatorMoveLastItem As System.Windows.Forms.ToolStripButton
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents btnAccept As System.Windows.Forms.Button
    Friend WithEvents btnHelp As System.Windows.Forms.Button
    Friend WithEvents DsRegPending As InfoCtr.dsRegPending
    Friend WithEvents tblRegPendingTableAdapter As InfoCtr.dsRegPendingTableAdapters.taRegistrPending
    Friend WithEvents MainMenu1 As System.Windows.Forms.MainMenu
    Friend WithEvents MenuItem1 As System.Windows.Forms.MenuItem
    Friend WithEvents miNew As System.Windows.Forms.MenuItem
    Friend WithEvents miClose As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem2 As System.Windows.Forms.MenuItem
    Friend WithEvents miSave As System.Windows.Forms.MenuItem
    Friend WithEvents miCancel As System.Windows.Forms.MenuItem
    Friend WithEvents miDelete As System.Windows.Forms.MenuItem
    Friend WithEvents lblevent As System.Windows.Forms.Label
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents MenuItem3 As System.Windows.Forms.MenuItem
    Friend WithEvents miMultiple As System.Windows.Forms.MenuItem
    Friend WithEvents miRefreshContacts As System.Windows.Forms.MenuItem
    Friend WithEvents miHelp As System.Windows.Forms.MenuItem
    Friend WithEvents miCopy As System.Windows.Forms.MenuItem
    Friend WithEvents ErrorProvider1 As System.Windows.Forms.ErrorProvider
    ' Friend WithEvents RelRegOrderBindingSource As System.Windows.Forms.BindingSource
    '  Friend WithEvents tblRegOrderGridTableAdapter As InfoCtr.dsMainWPendingTableAdapters.tblRegOrderGridTableAdapter
    ' Friend WithEvents tblEventDDTableAdapter As InfoCtr.dsMainWPendingTableAdapters.tblEventDDTableAdapter
    '  Friend WithEvents RelRegOrderBindingSource1 As System.Windows.Forms.BindingSource
    Friend WithEvents miGotoOrder As System.Windows.Forms.MenuItem
    Friend WithEvents lblordergoto As System.Windows.Forms.Label
    Friend WithEvents SqlConnection1 As System.Data.SqlClient.SqlConnection
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents cboSrchOrg As InfoCtr.ComboBoxRelaxed
    Friend WithEvents fldOID As System.Windows.Forms.Label
    Friend WithEvents fldSID As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents fldName As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents txtMatched As System.Windows.Forms.TextBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents txtSrchContact As System.Windows.Forms.TextBox
    Friend WithEvents cboSrchContact As InfoCtr.ComboBoxRelaxed
    Friend WithEvents txtSrchOrg As System.Windows.Forms.TextBox
    Friend WithEvents cboRegion As InfoCtr.ComboBoxRelaxed
    Friend WithEvents dgContact As System.Windows.Forms.DataGrid
    Friend WithEvents dgOrg As System.Windows.Forms.DataGrid
    Friend WithEvents btnGotoSrchOrg As System.Windows.Forms.Button
    Friend WithEvents btnContactGo As System.Windows.Forms.Button
    Friend WithEvents btnOrgGo As System.Windows.Forms.Button
    Friend WithEvents fldUID As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents fldEventID As System.Windows.Forms.Label
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents DataGridTableStyle1 As System.Windows.Forms.DataGridTableStyle
    Friend WithEvents DataGridTextBoxColumn1 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn2 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn3 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn4 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn5 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn6 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn7 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents OrgID As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents chkWildcard As System.Windows.Forms.CheckBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents lblWarning As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents DataGridTextBoxColumn9 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents DataGridTableStyle2 As System.Windows.Forms.DataGridTableStyle
    Friend WithEvents fldEmail As System.Windows.Forms.TextBox
    Friend WithEvents btnCopyEmail As System.Windows.Forms.Button
    Friend WithEvents btnReject As System.Windows.Forms.Button
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents Label29 As System.Windows.Forms.Label
    '  Friend WithEvents DsContacts As WindowsApplication11.dsContacts
End Class
