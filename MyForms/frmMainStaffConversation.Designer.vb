<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMainStaffConversation
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
        Forms.Remove(Me)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMainStaffConversation))
        Me.dtpTime = New System.Windows.Forms.DateTimePicker
        Me.BindingSource1 = New System.Windows.Forms.BindingSource(Me.components)
        Me.DsStaffConv1 = New InfoCtr.dsStaffConv
        Me.dtpDate = New System.Windows.Forms.DateTimePicker
        Me.cboMode = New System.Windows.Forms.ComboBox
        Me.txtConverseDate = New System.Windows.Forms.TextBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.cboCaller = New System.Windows.Forms.ComboBox
        Me.Label12 = New System.Windows.Forms.Label
        Me.btnHelp = New System.Windows.Forms.Button
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.btnNew = New System.Windows.Forms.Button
        Me.btnSaveExit = New System.Windows.Forms.Button
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtNotes = New System.Windows.Forms.TextBox
        Me.Label16 = New System.Windows.Forms.Label
        Me.txtBriefSummary = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.cboCase = New System.Windows.Forms.ComboBox
        Me.fldGotoCase = New System.Windows.Forms.TextBox
        Me.cboOrg = New System.Windows.Forms.ComboBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.cboEvent = New System.Windows.Forms.ComboBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.MainMenu1 = New System.Windows.Forms.MainMenu(Me.components)
        Me.MenuItem1 = New System.Windows.Forms.MenuItem
        Me.miNew = New System.Windows.Forms.MenuItem
        Me.miClose = New System.Windows.Forms.MenuItem
        Me.MenuItem4 = New System.Windows.Forms.MenuItem
        Me.miSave = New System.Windows.Forms.MenuItem
        Me.miCancel = New System.Windows.Forms.MenuItem
        Me.miDelete = New System.Windows.Forms.MenuItem
        Me.MenuItem7 = New System.Windows.Forms.MenuItem
        Me.miNoteCopy = New System.Windows.Forms.MenuItem
        Me.miNoteCut = New System.Windows.Forms.MenuItem
        Me.miNotePaste = New System.Windows.Forms.MenuItem
        Me.miSelectAll = New System.Windows.Forms.MenuItem
        Me.miUndo = New System.Windows.Forms.MenuItem
        Me.TextBox1 = New System.Windows.Forms.TextBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.StatusBar1 = New System.Windows.Forms.StatusBar
        Me.StatusBarPanel1 = New System.Windows.Forms.StatusBarPanel
        Me.StatusBarPanel2 = New System.Windows.Forms.StatusBarPanel
        Me.DsCaseNames1 = New InfoCtr.dsCaseNames
        Me.TblStaffConversationTableAdapter = New InfoCtr.dsStaffConvTableAdapters.MainStaffConversationTableAdapter
        Me.fldID = New System.Windows.Forms.Label
        CType(Me.BindingSource1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsStaffConv1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        CType(Me.StatusBarPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.StatusBarPanel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsCaseNames1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dtpTime
        '
        Me.dtpTime.DataBindings.Add(New System.Windows.Forms.Binding("Value", Me.BindingSource1, "ConversDate", True))
        Me.dtpTime.Format = System.Windows.Forms.DateTimePickerFormat.Time
        Me.dtpTime.Location = New System.Drawing.Point(164, 115)
        Me.dtpTime.Name = "dtpTime"
        Me.dtpTime.ShowUpDown = True
        Me.dtpTime.Size = New System.Drawing.Size(116, 20)
        Me.dtpTime.TabIndex = 220
        Me.dtpTime.Tag = "Time"
        '
        'BindingSource1
        '
        Me.BindingSource1.DataMember = "MainStaffConversation"
        Me.BindingSource1.DataSource = Me.DsStaffConv1
        '
        'DsStaffConv1
        '
        Me.DsStaffConv1.DataSetName = "dsStaffConv"
        Me.DsStaffConv1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'dtpDate
        '
        Me.dtpDate.DataBindings.Add(New System.Windows.Forms.Binding("Value", Me.BindingSource1, "ConversDate", True))
        Me.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpDate.Location = New System.Drawing.Point(56, 115)
        Me.dtpDate.Name = "dtpDate"
        Me.dtpDate.Size = New System.Drawing.Size(102, 20)
        Me.dtpDate.TabIndex = 218
        Me.dtpDate.Tag = "ConversationDate"
        '
        'cboMode
        '
        Me.cboMode.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboMode.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboMode.DataBindings.Add(New System.Windows.Forms.Binding("SelectedItem", Me.BindingSource1, "ModeofContact", True))
        Me.cboMode.Location = New System.Drawing.Point(202, 290)
        Me.cboMode.Name = "cboMode"
        Me.cboMode.Size = New System.Drawing.Size(141, 21)
        Me.cboMode.TabIndex = 221
        Me.cboMode.Tag = "MODE of CONTACT"
        '
        'txtConverseDate
        '
        Me.txtConverseDate.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.BindingSource1, "ConversDate", True))
        Me.txtConverseDate.Location = New System.Drawing.Point(286, 115)
        Me.txtConverseDate.Name = "txtConverseDate"
        Me.txtConverseDate.Size = New System.Drawing.Size(50, 20)
        Me.txtConverseDate.TabIndex = 219
        Me.txtConverseDate.Visible = False
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Location = New System.Drawing.Point(13, 119)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(31, 16)
        Me.Label8.TabIndex = 225
        Me.Label8.Text = "Date"
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Location = New System.Drawing.Point(107, 290)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(89, 16)
        Me.Label5.TabIndex = 224
        Me.Label5.Text = "Mode of contact"
        '
        'cboCaller
        '
        Me.cboCaller.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboCaller.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboCaller.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.BindingSource1, "StaffNum", True))
        Me.cboCaller.Location = New System.Drawing.Point(89, 147)
        Me.cboCaller.Name = "cboCaller"
        Me.cboCaller.Size = New System.Drawing.Size(254, 21)
        Me.cboCaller.TabIndex = 222
        Me.cboCaller.Tag = "STAFF NAME"
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.Transparent
        Me.Label12.Location = New System.Drawing.Point(43, 147)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(40, 24)
        Me.Label12.TabIndex = 223
        Me.Label12.Text = "Staff Caller"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnHelp
        '
        Me.btnHelp.Image = CType(resources.GetObject("btnHelp.Image"), System.Drawing.Image)
        Me.btnHelp.Location = New System.Drawing.Point(713, 11)
        Me.btnHelp.Name = "btnHelp"
        Me.btnHelp.Size = New System.Drawing.Size(25, 25)
        Me.btnHelp.TabIndex = 237
        Me.btnHelp.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnHelp.UseVisualStyleBackColor = True
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.SystemColors.Highlight
        Me.Panel2.Controls.Add(Me.btnNew)
        Me.Panel2.Controls.Add(Me.btnSaveExit)
        Me.Panel2.Location = New System.Drawing.Point(622, 10)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(88, 40)
        Me.Panel2.TabIndex = 236
        '
        'btnNew
        '
        Me.btnNew.BackColor = System.Drawing.Color.PaleGoldenrod
        Me.btnNew.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnNew.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnNew.Image = CType(resources.GetObject("btnNew.Image"), System.Drawing.Image)
        Me.btnNew.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnNew.Location = New System.Drawing.Point(2, 1)
        Me.btnNew.Margin = New System.Windows.Forms.Padding(0)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(40, 35)
        Me.btnNew.TabIndex = 201
        Me.btnNew.Text = "New"
        Me.btnNew.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnNew.UseVisualStyleBackColor = False
        '
        'btnSaveExit
        '
        Me.btnSaveExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSaveExit.BackColor = System.Drawing.SystemColors.Control
        Me.btnSaveExit.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSaveExit.Image = CType(resources.GetObject("btnSaveExit.Image"), System.Drawing.Image)
        Me.btnSaveExit.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSaveExit.Location = New System.Drawing.Point(45, 1)
        Me.btnSaveExit.Name = "btnSaveExit"
        Me.btnSaveExit.Size = New System.Drawing.Size(40, 35)
        Me.btnSaveExit.TabIndex = 203
        Me.btnSaveExit.Text = "Close"
        Me.btnSaveExit.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSaveExit.UseVisualStyleBackColor = False
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.Desktop
        Me.Label2.Location = New System.Drawing.Point(9, 13)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(255, 24)
        Me.Label2.TabIndex = 238
        Me.Label2.Text = "STAFF CONVERSATION DETAIL"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtNotes
        '
        Me.txtNotes.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.BindingSource1, "Notes", True))
        Me.txtNotes.Location = New System.Drawing.Point(349, 116)
        Me.txtNotes.Multiline = True
        Me.txtNotes.Name = "txtNotes"
        Me.txtNotes.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtNotes.Size = New System.Drawing.Size(484, 339)
        Me.txtNotes.TabIndex = 239
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.Color.Transparent
        Me.Label16.Location = New System.Drawing.Point(9, 177)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(74, 34)
        Me.Label16.TabIndex = 241
        Me.Label16.Text = "Brief Summary"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtBriefSummary
        '
        Me.txtBriefSummary.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.BindingSource1, "BriefSummary", True))
        Me.txtBriefSummary.Location = New System.Drawing.Point(89, 177)
        Me.txtBriefSummary.MaxLength = 300
        Me.txtBriefSummary.Multiline = True
        Me.txtBriefSummary.Name = "txtBriefSummary"
        Me.txtBriefSummary.Size = New System.Drawing.Size(254, 40)
        Me.txtBriefSummary.TabIndex = 240
        Me.txtBriefSummary.Text = "txtbriefsummary"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(16, 35)
        Me.Label1.MaximumSize = New System.Drawing.Size(600, 0)
        Me.Label1.MinimumSize = New System.Drawing.Size(0, 50)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(568, 50)
        Me.Label1.TabIndex = 242
        Me.Label1.Text = resources.GetString("Label1.Text")
        '
        'cboCase
        '
        Me.cboCase.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboCase.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboCase.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.BindingSource1, "CaseNum", True))
        Me.cboCase.DisplayMember = "CaseName"
        Me.cboCase.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboCase.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cboCase.Location = New System.Drawing.Point(102, 347)
        Me.cboCase.Name = "cboCase"
        Me.cboCase.Size = New System.Drawing.Size(241, 21)
        Me.cboCase.TabIndex = 243
        Me.cboCase.Tag = "CASE NAME"
        Me.cboCase.ValueMember = "CaseID"
        '
        'FldGotoCase
        '
        Me.fldGotoCase.BackColor = System.Drawing.SystemColors.Control
        Me.FldGotoCase.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FldGotoCase.ForeColor = System.Drawing.Color.Black
        Me.FldGotoCase.Location = New System.Drawing.Point(19, 337)
        Me.FldGotoCase.Name = "FldGotoCase"
        Me.FldGotoCase.Size = New System.Drawing.Size(64, 31)
        Me.FldGotoCase.TabIndex = 244
        Me.FldGotoCase.Tag = "Case"
        Me.FldGotoCase.Text = "Case Name"
        Me.FldGotoCase.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cboOrg
        '
        Me.cboOrg.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboOrg.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboOrg.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.BindingSource1, "OrgNum", True))
        Me.cboOrg.DisplayMember = "CaseID"
        Me.cboOrg.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboOrg.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cboOrg.Location = New System.Drawing.Point(102, 320)
        Me.cboOrg.Name = "cboOrg"
        Me.cboOrg.Size = New System.Drawing.Size(241, 21)
        Me.cboOrg.TabIndex = 245
        Me.cboOrg.Tag = "ORG NAME"
        Me.cboOrg.ValueMember = "CaseID"
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.ForestGreen
        Me.Label3.Location = New System.Drawing.Point(12, 310)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(84, 31)
        Me.Label3.TabIndex = 246
        Me.Label3.Tag = "Org"
        Me.Label3.Text = "Organization Name"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cboEvent
        '
        Me.cboEvent.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboEvent.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboEvent.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.BindingSource1, "EventNum", True))
        Me.cboEvent.DisplayMember = "CaseName"
        Me.cboEvent.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboEvent.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cboEvent.Location = New System.Drawing.Point(68, 374)
        Me.cboEvent.Name = "cboEvent"
        Me.cboEvent.Size = New System.Drawing.Size(275, 21)
        Me.cboEvent.TabIndex = 247
        Me.cboEvent.Tag = "EVENT NAME"
        Me.cboEvent.ValueMember = "CaseID"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(19, 370)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(43, 31)
        Me.Label4.TabIndex = 248
        Me.Label4.Tag = "Event"
        Me.Label4.Text = "Event Name"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Black
        Me.Label6.Location = New System.Drawing.Point(346, 85)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(43, 31)
        Me.Label6.TabIndex = 249
        Me.Label6.Tag = "Note"
        Me.Label6.Text = "Notes"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'MainMenu1
        '
        Me.MainMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem1, Me.MenuItem4})
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
        Me.miNew.Text = "New Conversation"
        '
        'miClose
        '
        Me.miClose.Index = 1
        Me.miClose.Text = "Close Window"
        '
        'MenuItem4
        '
        Me.MenuItem4.Index = 1
        Me.MenuItem4.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.miSave, Me.miCancel, Me.miDelete, Me.MenuItem7, Me.miNoteCopy, Me.miNoteCut, Me.miNotePaste, Me.miSelectAll, Me.miUndo})
        Me.MenuItem4.Text = "Edit"
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
        Me.miDelete.Text = "Delete Conversation"
        '
        'MenuItem7
        '
        Me.MenuItem7.Index = 3
        Me.MenuItem7.Text = "---------------------"
        '
        'miNoteCopy
        '
        Me.miNoteCopy.Enabled = False
        Me.miNoteCopy.Index = 4
        Me.miNoteCopy.Text = "Copy          (Ctrl + c)"
        '
        'miNoteCut
        '
        Me.miNoteCut.Enabled = False
        Me.miNoteCut.Index = 5
        Me.miNoteCut.Text = "Cut            (Ctrl + x)"
        '
        'miNotePaste
        '
        Me.miNotePaste.Enabled = False
        Me.miNotePaste.Index = 6
        Me.miNotePaste.Text = "Paste         (Ctrl + v)"
        '
        'miSelectAll
        '
        Me.miSelectAll.Enabled = False
        Me.miSelectAll.Index = 7
        Me.miSelectAll.Text = "Select All    (Ctrl + a)"
        '
        'miUndo
        '
        Me.miUndo.Enabled = False
        Me.miUndo.Index = 8
        Me.miUndo.Text = "Undo          (Ctrl + z)"
        '
        'TextBox1
        '
        Me.TextBox1.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsStaffConv1, "MainStaffConversation.Contact", True))
        Me.TextBox1.Location = New System.Drawing.Point(102, 224)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(241, 20)
        Me.TextBox1.TabIndex = 250
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Location = New System.Drawing.Point(6, 216)
        Me.Label7.Margin = New System.Windows.Forms.Padding(0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(93, 34)
        Me.Label7.TabIndex = 251
        Me.Label7.Text = "Conversation with"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'StatusBar1
        '
        Me.StatusBar1.Location = New System.Drawing.Point(0, 239)
        Me.StatusBar1.Name = "StatusBar1"
        Me.StatusBar1.Panels.AddRange(New System.Windows.Forms.StatusBarPanel() {Me.StatusBarPanel1, Me.StatusBarPanel2})
        Me.StatusBar1.ShowPanels = True
        Me.StatusBar1.Size = New System.Drawing.Size(750, 22)
        Me.StatusBar1.TabIndex = 252
        Me.StatusBar1.Text = "StatusBar1"
        '
        'StatusBarPanel1
        '
        Me.StatusBarPanel1.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents
        Me.StatusBarPanel1.BorderStyle = System.Windows.Forms.StatusBarPanelBorderStyle.Raised
        Me.StatusBarPanel1.MinWidth = 200
        Me.StatusBarPanel1.Name = "StatusBarPanel1"
        Me.StatusBarPanel1.Width = 200
        '
        'StatusBarPanel2
        '
        Me.StatusBarPanel2.Alignment = System.Windows.Forms.HorizontalAlignment.Right
        Me.StatusBarPanel2.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring
        Me.StatusBarPanel2.BorderStyle = System.Windows.Forms.StatusBarPanelBorderStyle.None
        Me.StatusBarPanel2.Name = "StatusBarPanel2"
        Me.StatusBarPanel2.Text = "Use this window to edit Conversation details."
        Me.StatusBarPanel2.Width = 533
        '
        'DsCaseNames1
        '
        Me.DsCaseNames1.DataSetName = "dsCaseNames"
        Me.DsCaseNames1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'TblStaffConversationTableAdapter
        '
        Me.TblStaffConversationTableAdapter.ClearBeforeFill = True
        '
        'fldID
        '
        Me.fldID.BackColor = System.Drawing.Color.Transparent
        Me.fldID.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.BindingSource1, "StaffConversID", True))
        Me.fldID.Location = New System.Drawing.Point(636, 67)
        Me.fldID.Name = "fldID"
        Me.fldID.Size = New System.Drawing.Size(74, 34)
        Me.fldID.TabIndex = 253
        Me.fldID.Text = "IDFld"
        Me.fldID.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'frmMainStaffConversation
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(750, 261)
        Me.Controls.Add(Me.fldID)
        Me.Controls.Add(Me.StatusBar1)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.cboEvent)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.cboOrg)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.cboCase)
        Me.Controls.Add(Me.FldGotoCase)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label16)
        Me.Controls.Add(Me.txtBriefSummary)
        Me.Controls.Add(Me.txtNotes)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.btnHelp)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.dtpTime)
        Me.Controls.Add(Me.dtpDate)
        Me.Controls.Add(Me.cboMode)
        Me.Controls.Add(Me.txtConverseDate)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.cboCaller)
        Me.Controls.Add(Me.Label12)
        Me.Menu = Me.MainMenu1
        Me.Name = "frmMainStaffConversation"
        Me.Text = "Staff Conversation Detail"
        CType(Me.BindingSource1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsStaffConv1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        CType(Me.StatusBarPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.StatusBarPanel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsCaseNames1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents dtpTime As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents cboMode As System.Windows.Forms.ComboBox
    Friend WithEvents txtConverseDate As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents cboCaller As System.Windows.Forms.ComboBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents btnHelp As System.Windows.Forms.Button
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents btnSaveExit As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtNotes As System.Windows.Forms.TextBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents txtBriefSummary As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cboCase As System.Windows.Forms.ComboBox
    Friend WithEvents fldGotoCase As System.Windows.Forms.TextBox
    Friend WithEvents cboOrg As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cboEvent As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents btnNew As System.Windows.Forms.Button
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents BindingSource1 As System.Windows.Forms.BindingSource
    Friend WithEvents DsStaffConv1 As InfoCtr.dsStaffConv
    Friend WithEvents TblStaffConversationTableAdapter As InfoCtr.dsStaffConvTableAdapters.MainStaffConversationTableAdapter
    Friend WithEvents MainMenu1 As System.Windows.Forms.MainMenu
    Friend WithEvents MenuItem1 As System.Windows.Forms.MenuItem
    Friend WithEvents miNew As System.Windows.Forms.MenuItem
    Friend WithEvents miClose As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem4 As System.Windows.Forms.MenuItem
    Friend WithEvents miSave As System.Windows.Forms.MenuItem
    Friend WithEvents miCancel As System.Windows.Forms.MenuItem
    Friend WithEvents miDelete As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem7 As System.Windows.Forms.MenuItem
    Friend WithEvents miNoteCopy As System.Windows.Forms.MenuItem
    Friend WithEvents miNoteCut As System.Windows.Forms.MenuItem
    Friend WithEvents miNotePaste As System.Windows.Forms.MenuItem
    Friend WithEvents miSelectAll As System.Windows.Forms.MenuItem
    Friend WithEvents miUndo As System.Windows.Forms.MenuItem
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents StatusBar1 As System.Windows.Forms.StatusBar
    Friend WithEvents StatusBarPanel1 As System.Windows.Forms.StatusBarPanel
    Friend WithEvents StatusBarPanel2 As System.Windows.Forms.StatusBarPanel
    Friend WithEvents DsCaseNames1 As InfoCtr.dsCaseNames
    Friend WithEvents fldID As System.Windows.Forms.Label
End Class
