<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmNewWReg2
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
        Me.components = New System.ComponentModel.Container()
        Dim RegDateLabel As System.Windows.Forms.Label
        Dim Label15 As System.Windows.Forms.Label
        Dim Label1 As System.Windows.Forms.Label
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmNewWReg2))
        Me.ErrorProvider1 = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.btnSaveExit = New System.Windows.Forms.Button()
        Me.MainMenu1 = New System.Windows.Forms.MainMenu(Me.components)
        Me.MenuItem1 = New System.Windows.Forms.MenuItem()
        Me.miClose = New System.Windows.Forms.MenuItem()
        Me.MenuItem2 = New System.Windows.Forms.MenuItem()
        Me.miDelete = New System.Windows.Forms.MenuItem()
        Me.miRefreshContacts = New System.Windows.Forms.MenuItem()
        Me.miCopy = New System.Windows.Forms.MenuItem()
        Me.miHelp = New System.Windows.Forms.MenuItem()
        Me.cboEvent = New InfoCtr.ComboBoxRelaxed()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.fldRegisterDate = New System.Windows.Forms.TextBox()
        Me.btnRefreshContacts = New System.Windows.Forms.Button()
        Me.fldOrderNum = New System.Windows.Forms.TextBox()
        Me.btnHelp = New System.Windows.Forms.Button()
        Me.cboRegistrant = New InfoCtr.ComboBoxRelaxed()
        Me.fldGotoOrder = New System.Windows.Forms.TextBox()
        Me.ToolTip2 = New System.Windows.Forms.ToolTip(Me.components)
        Me.btnRefreshEvents = New System.Windows.Forms.Button()
        Me.StatusBarPanel2 = New System.Windows.Forms.StatusBarPanel()
        Me.StatusBarPanelID = New System.Windows.Forms.StatusBarPanel()
        Me.StatusBarPanel1 = New System.Windows.Forms.StatusBarPanel()
        Me.StatusBar1 = New System.Windows.Forms.StatusBar()
        Me.btnNewOrder = New System.Windows.Forms.Button()
        RegDateLabel = New System.Windows.Forms.Label()
        Label15 = New System.Windows.Forms.Label()
        Label1 = New System.Windows.Forms.Label()
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        CType(Me.StatusBarPanel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.StatusBarPanelID, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.StatusBarPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RegDateLabel
        '
        RegDateLabel.AutoSize = True
        RegDateLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        RegDateLabel.Location = New System.Drawing.Point(5, 183)
        RegDateLabel.Margin = New System.Windows.Forms.Padding(0)
        RegDateLabel.Name = "RegDateLabel"
        RegDateLabel.Size = New System.Drawing.Size(105, 15)
        RegDateLabel.TabIndex = 11
        RegDateLabel.Text = "Registration Date:"
        '
        'Label15
        '
        Label15.AutoSize = True
        Label15.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Label15.Location = New System.Drawing.Point(70, 62)
        Label15.Margin = New System.Windows.Forms.Padding(0)
        Label15.Name = "Label15"
        Label15.Size = New System.Drawing.Size(40, 15)
        Label15.TabIndex = 612
        Label15.Text = "Event:"
        '
        'Label1
        '
        Label1.AutoSize = True
        Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Label1.Location = New System.Drawing.Point(44, 97)
        Label1.Margin = New System.Windows.Forms.Padding(0)
        Label1.Name = "Label1"
        Label1.Size = New System.Drawing.Size(66, 15)
        Label1.TabIndex = 616
        Label1.Text = "Registrant:"
        '
        'ErrorProvider1
        '
        Me.ErrorProvider1.ContainerControl = Me
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.SystemColors.Highlight
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Controls.Add(Me.btnSaveExit)
        Me.Panel2.Location = New System.Drawing.Point(572, 1)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(59, 40)
        Me.Panel2.TabIndex = 424
        '
        'btnSaveExit
        '
        Me.btnSaveExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSaveExit.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.btnSaveExit.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSaveExit.Image = CType(resources.GetObject("btnSaveExit.Image"), System.Drawing.Image)
        Me.btnSaveExit.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSaveExit.Location = New System.Drawing.Point(3, 1)
        Me.btnSaveExit.Name = "btnSaveExit"
        Me.btnSaveExit.Size = New System.Drawing.Size(50, 35)
        Me.btnSaveExit.TabIndex = 416
        Me.btnSaveExit.TabStop = False
        Me.btnSaveExit.Text = "Accept"
        Me.btnSaveExit.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSaveExit.UseVisualStyleBackColor = False
        '
        'MainMenu1
        '
        Me.MainMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem1, Me.MenuItem2, Me.miHelp})
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
        Me.MenuItem2.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.miDelete, Me.miRefreshContacts, Me.miCopy})
        Me.MenuItem2.Text = "Edit"
        '
        'miDelete
        '
        Me.miDelete.Index = 0
        Me.miDelete.Text = "Delete Registration"
        '
        'miRefreshContacts
        '
        Me.miRefreshContacts.Enabled = False
        Me.miRefreshContacts.Index = 1
        Me.miRefreshContacts.Text = "Refresh Contact DropDown"
        '
        'miCopy
        '
        Me.miCopy.Enabled = False
        Me.miCopy.Index = 2
        Me.miCopy.Text = "Copy Registration to Another Event"
        '
        'miHelp
        '
        Me.miHelp.Index = 2
        Me.miHelp.Text = "Help"
        '
        'cboEvent
        '
        Me.cboEvent.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboEvent.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboEvent.DisplayMember = "EventName"
        Me.cboEvent.DropDownWidth = 500
        Me.cboEvent.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboEvent.FormattingEnabled = True
        Me.cboEvent.Location = New System.Drawing.Point(108, 61)
        Me.cboEvent.Name = "cboEvent"
        Me.cboEvent.RestrictContentToListItems = True
        Me.cboEvent.Size = New System.Drawing.Size(420, 23)
        Me.cboEvent.TabIndex = 0
        Me.cboEvent.TabStop = False
        Me.cboEvent.Tag = "Event"
        Me.cboEvent.ValueMember = "EventID"
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
        Me.Label8.Text = "NEW REGISTRATION"
        '
        'fldRegisterDate
        '
        Me.fldRegisterDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fldRegisterDate.Location = New System.Drawing.Point(108, 182)
        Me.fldRegisterDate.Name = "fldRegisterDate"
        Me.fldRegisterDate.Size = New System.Drawing.Size(110, 21)
        Me.fldRegisterDate.TabIndex = 2
        Me.fldRegisterDate.Tag = "Registration Date"
        '
        'btnRefreshContacts
        '
        Me.btnRefreshContacts.Location = New System.Drawing.Point(543, 94)
        Me.btnRefreshContacts.Name = "btnRefreshContacts"
        Me.btnRefreshContacts.Size = New System.Drawing.Size(106, 19)
        Me.btnRefreshContacts.TabIndex = 477
        Me.btnRefreshContacts.TabStop = False
        Me.btnRefreshContacts.Tag = "C:\VisualStudioDevelopment\AppInfoCtr\MyForms\frmMainWReg2.vb"
        Me.btnRefreshContacts.Text = "refresh Contacts"
        Me.btnRefreshContacts.UseVisualStyleBackColor = True
        Me.btnRefreshContacts.Visible = False
        '
        'fldOrderNum
        '
        Me.fldOrderNum.BackColor = System.Drawing.SystemColors.Window
        Me.fldOrderNum.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fldOrderNum.Location = New System.Drawing.Point(108, 136)
        Me.fldOrderNum.Name = "fldOrderNum"
        Me.fldOrderNum.Size = New System.Drawing.Size(110, 21)
        Me.fldOrderNum.TabIndex = 3
        Me.fldOrderNum.TabStop = False
        Me.fldOrderNum.Tag = "Order Number"
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
        'cboRegistrant
        '
        Me.cboRegistrant.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboRegistrant.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboRegistrant.DisplayMember = "ContactOrgCity"
        Me.cboRegistrant.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboRegistrant.FormattingEnabled = True
        Me.cboRegistrant.Location = New System.Drawing.Point(108, 95)
        Me.cboRegistrant.Name = "cboRegistrant"
        Me.cboRegistrant.RestrictContentToListItems = True
        Me.cboRegistrant.Size = New System.Drawing.Size(420, 23)
        Me.cboRegistrant.TabIndex = 1
        Me.cboRegistrant.Tag = "Contact Name"
        Me.cboRegistrant.ValueMember = "ContactID"
        '
        'fldGotoOrder
        '
        Me.fldGotoOrder.BackColor = System.Drawing.SystemColors.Control
        Me.fldGotoOrder.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.fldGotoOrder.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fldGotoOrder.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fldGotoOrder.Location = New System.Drawing.Point(31, 139)
        Me.fldGotoOrder.Margin = New System.Windows.Forms.Padding(0)
        Me.fldGotoOrder.Name = "fldGotoOrder"
        Me.fldGotoOrder.ReadOnly = True
        Me.fldGotoOrder.Size = New System.Drawing.Size(77, 16)
        Me.fldGotoOrder.TabIndex = 611
        Me.fldGotoOrder.TabStop = False
        Me.fldGotoOrder.Tag = ""
        Me.fldGotoOrder.Text = "Order #:"
        Me.fldGotoOrder.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ToolTip2.SetToolTip(Me.fldGotoOrder, "Doubleclick to open Order summary.")
        '
        'btnRefreshEvents
        '
        Me.btnRefreshEvents.Location = New System.Drawing.Point(543, 61)
        Me.btnRefreshEvents.Name = "btnRefreshEvents"
        Me.btnRefreshEvents.Size = New System.Drawing.Size(106, 19)
        Me.btnRefreshEvents.TabIndex = 614
        Me.btnRefreshEvents.TabStop = False
        Me.btnRefreshEvents.Tag = ""
        Me.btnRefreshEvents.Text = "refresh Events"
        Me.ToolTip2.SetToolTip(Me.btnRefreshEvents, "fills dropdown with all events.")
        Me.btnRefreshEvents.UseVisualStyleBackColor = True
        '
        'StatusBarPanel2
        '
        Me.StatusBarPanel2.Alignment = System.Windows.Forms.HorizontalAlignment.Right
        Me.StatusBarPanel2.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring
        Me.StatusBarPanel2.BorderStyle = System.Windows.Forms.StatusBarPanelBorderStyle.None
        Me.StatusBarPanel2.Name = "StatusBarPanel2"
        Me.StatusBarPanel2.Text = "Use this window to enter new Registration."
        Me.StatusBarPanel2.Width = 247
        '
        'StatusBarPanelID
        '
        Me.StatusBarPanelID.Alignment = System.Windows.Forms.HorizontalAlignment.Center
        Me.StatusBarPanelID.BorderStyle = System.Windows.Forms.StatusBarPanelBorderStyle.Raised
        Me.StatusBarPanelID.Name = "StatusBarPanelID"
        Me.StatusBarPanelID.Text = "New Registration"
        Me.StatusBarPanelID.Width = 200
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
        'StatusBar1
        '
        Me.StatusBar1.Location = New System.Drawing.Point(0, 251)
        Me.StatusBar1.Name = "StatusBar1"
        Me.StatusBar1.Panels.AddRange(New System.Windows.Forms.StatusBarPanel() {Me.StatusBarPanel1, Me.StatusBarPanelID, Me.StatusBarPanel2})
        Me.StatusBar1.ShowPanels = True
        Me.StatusBar1.Size = New System.Drawing.Size(664, 22)
        Me.StatusBar1.TabIndex = 613
        Me.StatusBar1.Text = "StatusBar1"
        '
        'btnNewOrder
        '
        Me.btnNewOrder.Location = New System.Drawing.Point(231, 135)
        Me.btnNewOrder.Name = "btnNewOrder"
        Me.btnNewOrder.Size = New System.Drawing.Size(106, 23)
        Me.btnNewOrder.TabIndex = 615
        Me.btnNewOrder.TabStop = False
        Me.btnNewOrder.Tag = "C:\VisualStudioDevelopment\AppInfoCtr\MyForms\frmMainWReg2.vb"
        Me.btnNewOrder.Text = "get New Order #"
        Me.btnNewOrder.UseVisualStyleBackColor = True
        '
        'frmNewWReg2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(664, 273)
        Me.Controls.Add(Label1)
        Me.Controls.Add(Me.btnNewOrder)
        Me.Controls.Add(Me.btnRefreshEvents)
        Me.Controls.Add(Me.StatusBar1)
        Me.Controls.Add(Label15)
        Me.Controls.Add(Me.fldGotoOrder)
        Me.Controls.Add(RegDateLabel)
        Me.Controls.Add(Me.fldRegisterDate)
        Me.Controls.Add(Me.fldOrderNum)
        Me.Controls.Add(Me.btnRefreshContacts)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.cboEvent)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.btnHelp)
        Me.Controls.Add(Me.cboRegistrant)
        Me.Menu = Me.MainMenu1
        Me.Name = "frmNewWReg2"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Tag = "NewREGISTRATION"
        Me.Text = "NEW REGISTRATION"
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        CType(Me.StatusBarPanel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.StatusBarPanelID, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.StatusBarPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub




    Friend WithEvents cboRegistrant As InfoCtr.ComboBoxRelaxed
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents btnSaveExit As System.Windows.Forms.Button
    Friend WithEvents btnHelp As System.Windows.Forms.Button
    ' Friend WithEvents MainEventReg2TableAdapter As InfoCtr.dsMainEventReg2TableAdapters.MainEventReg2TableAdapter
    Friend WithEvents MainMenu1 As System.Windows.Forms.MainMenu
    Friend WithEvents MenuItem1 As System.Windows.Forms.MenuItem
    Friend WithEvents miClose As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem2 As System.Windows.Forms.MenuItem
    Friend WithEvents miDelete As System.Windows.Forms.MenuItem
    Friend WithEvents cboEvent As InfoCtr.ComboBoxRelaxed
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents fldRegisterDate As System.Windows.Forms.TextBox
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents miRefreshContacts As System.Windows.Forms.MenuItem
    Friend WithEvents miHelp As System.Windows.Forms.MenuItem
    Friend WithEvents btnRefreshContacts As System.Windows.Forms.Button
    Friend WithEvents miCopy As System.Windows.Forms.MenuItem
    '  Friend WithEvents ErrorProvider1 As System.Windows.Forms.ErrorProvider
    ' Friend WithEvents RelRegOrderBindingSource As System.Windows.Forms.BindingSource
    'Friend WithEvents tPaymentTableAdapter As InfoCtr.dsMainEventReg2TableAdapters.tPaymentTableAdapter
    ' Friend WithEvents tblEventDDTableAdapter As InfoCtr.dsMainEventReg2TableAdapters.tblEventDDTableAdapter
    '  Friend WithEvents RelRegOrderBindingSource1 As System.Windows.Forms.BindingSource
    Friend WithEvents fldOrderNum As System.Windows.Forms.TextBox

    Friend WithEvents ErrorProvider1 As System.Windows.Forms.ErrorProvider
    Friend WithEvents fldGotoOrder As System.Windows.Forms.TextBox
    Friend WithEvents ToolTip2 As System.Windows.Forms.ToolTip
    Friend WithEvents btnRefreshEvents As System.Windows.Forms.Button
    Friend WithEvents StatusBar1 As System.Windows.Forms.StatusBar
    Friend WithEvents StatusBarPanel1 As System.Windows.Forms.StatusBarPanel
    Friend WithEvents StatusBarPanelID As System.Windows.Forms.StatusBarPanel
    Friend WithEvents StatusBarPanel2 As System.Windows.Forms.StatusBarPanel
    Friend WithEvents btnNewOrder As System.Windows.Forms.Button
    '  Friend WithEvents DataGridTableStyle1 As System.Windows.Forms.DataGridTableStyle

    '  Friend WithEvents DsContacts As WindowsApplication11.dsContacts
End Class
