<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmNewWOrderPopup2
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
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
        Dim NotesLabel As System.Windows.Forms.Label
        Dim ClergyLayLabel As System.Windows.Forms.Label
        Dim PersonalCongregationalLabel As System.Windows.Forms.Label
        Dim MaleFemaleLabel As System.Windows.Forms.Label
        Dim QuestionsLabel As System.Windows.Forms.Label
        Dim HowHeardLabel As System.Windows.Forms.Label
        Dim HowRegisteredLabel As System.Windows.Forms.Label
        Dim RegistrationIDLabel As System.Windows.Forms.Label
        Dim Label2 As System.Windows.Forms.Label
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmNewWOrderPopup2))
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.btnSaveExit = New System.Windows.Forms.Button()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.btnHelp = New System.Windows.Forms.Button()
        Me.MainMenu1 = New System.Windows.Forms.MainMenu(Me.components)
        Me.MenuItem1 = New System.Windows.Forms.MenuItem()
        Me.miClose = New System.Windows.Forms.MenuItem()
        Me.MenuItem2 = New System.Windows.Forms.MenuItem()
        Me.miSave = New System.Windows.Forms.MenuItem()
        Me.miCancel = New System.Windows.Forms.MenuItem()
        Me.miDelete = New System.Windows.Forms.MenuItem()
        Me.miHelp = New System.Windows.Forms.MenuItem()
        Me.TblWRegPOpupBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.DsWRegPopup = New InfoCtr.dsWRegPopup()
        Me.chkVeg = New System.Windows.Forms.CheckBox()
        Me.cboHeard = New InfoCtr.ComboBoxRelaxed()
        Me.cboRegistered = New InfoCtr.ComboBoxRelaxed()
        Me.rtbNotes = New System.Windows.Forms.RichTextBox()
        Me.rtbReason = New System.Windows.Forms.RichTextBox()
        Me.cboClergy = New InfoCtr.ComboBoxRelaxed()
        Me.cboPersonal = New InfoCtr.ComboBoxRelaxed()
        Me.cboGender = New InfoCtr.ComboBoxRelaxed()
        Me.txtCommittee = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.btnNewContact = New System.Windows.Forms.Button()
        Me.btnRefreshContacts = New System.Windows.Forms.Button()
        Me.SqlConnection1 = New System.Data.SqlClient.SqlConnection()
        Me.ErrorProvider1 = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.fldContact = New System.Windows.Forms.TextBox()
        Me.lblRegistrantName = New System.Windows.Forms.Label()
        Me.fldRegistrationID = New System.Windows.Forms.TextBox()
        Me.fldOrderID = New System.Windows.Forms.TextBox()
        Me.cboContact = New InfoCtr.ComboBoxRelaxed()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cboEvent = New InfoCtr.ComboBoxRelaxed()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cboOrg = New InfoCtr.ComboBoxRelaxed()
        Me.cboRegion = New InfoCtr.ComboBoxRelaxed()
        Me.TblWRegPOpupTableAdapter = New InfoCtr.dsWRegPopupTableAdapters.tblWRegPopupTableAdapter()
        Me.Label5 = New System.Windows.Forms.Label()
        NotesLabel = New System.Windows.Forms.Label()
        ClergyLayLabel = New System.Windows.Forms.Label()
        PersonalCongregationalLabel = New System.Windows.Forms.Label()
        MaleFemaleLabel = New System.Windows.Forms.Label()
        QuestionsLabel = New System.Windows.Forms.Label()
        HowHeardLabel = New System.Windows.Forms.Label()
        HowRegisteredLabel = New System.Windows.Forms.Label()
        RegistrationIDLabel = New System.Windows.Forms.Label()
        Label2 = New System.Windows.Forms.Label()
        Me.Panel2.SuspendLayout()
        CType(Me.TblWRegPOpupBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsWRegPopup, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'NotesLabel
        '
        NotesLabel.AutoSize = True
        NotesLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        NotesLabel.Location = New System.Drawing.Point(35, 311)
        NotesLabel.Name = "NotesLabel"
        NotesLabel.Size = New System.Drawing.Size(36, 15)
        NotesLabel.TabIndex = 27
        NotesLabel.Text = "Note:"
        '
        'ClergyLayLabel
        '
        ClergyLayLabel.AutoSize = True
        ClergyLayLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        ClergyLayLabel.Location = New System.Drawing.Point(47, 167)
        ClergyLayLabel.Name = "ClergyLayLabel"
        ClergyLayLabel.Size = New System.Drawing.Size(86, 15)
        ClergyLayLabel.TabIndex = 29
        ClergyLayLabel.Text = "Clergy or Laity:"
        '
        'PersonalCongregationalLabel
        '
        PersonalCongregationalLabel.AutoSize = True
        PersonalCongregationalLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        PersonalCongregationalLabel.Location = New System.Drawing.Point(385, 472)
        PersonalCongregationalLabel.MaximumSize = New System.Drawing.Size(125, 0)
        PersonalCongregationalLabel.MinimumSize = New System.Drawing.Size(0, 30)
        PersonalCongregationalLabel.Name = "PersonalCongregationalLabel"
        PersonalCongregationalLabel.Size = New System.Drawing.Size(91, 45)
        PersonalCongregationalLabel.TabIndex = 31
        PersonalCongregationalLabel.Text = "for Personal or Congregational Growth: "
        PersonalCongregationalLabel.TextAlign = System.Drawing.ContentAlignment.TopRight
        PersonalCongregationalLabel.Visible = False
        '
        'MaleFemaleLabel
        '
        MaleFemaleLabel.AutoSize = True
        MaleFemaleLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        MaleFemaleLabel.Location = New System.Drawing.Point(49, 196)
        MaleFemaleLabel.Name = "MaleFemaleLabel"
        MaleFemaleLabel.Size = New System.Drawing.Size(83, 15)
        MaleFemaleLabel.TabIndex = 35
        MaleFemaleLabel.Text = "Male/Female:"
        '
        'QuestionsLabel
        '
        QuestionsLabel.AutoSize = True
        QuestionsLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        QuestionsLabel.Location = New System.Drawing.Point(402, 211)
        QuestionsLabel.Name = "QuestionsLabel"
        QuestionsLabel.Size = New System.Drawing.Size(233, 15)
        QuestionsLabel.TabIndex = 37
        QuestionsLabel.Text = "Questions / Primary Reason for Attending:"
        '
        'HowHeardLabel
        '
        HowHeardLabel.AutoSize = True
        HowHeardLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        HowHeardLabel.ForeColor = System.Drawing.SystemColors.ControlDark
        HowHeardLabel.Location = New System.Drawing.Point(29, 226)
        HowHeardLabel.Name = "HowHeardLabel"
        HowHeardLabel.Size = New System.Drawing.Size(106, 15)
        HowHeardLabel.TabIndex = 43
        HowHeardLabel.Text = "How Heard About:"
        '
        'HowRegisteredLabel
        '
        HowRegisteredLabel.AutoSize = True
        HowRegisteredLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        HowRegisteredLabel.Location = New System.Drawing.Point(35, 253)
        HowRegisteredLabel.Name = "HowRegisteredLabel"
        HowRegisteredLabel.Size = New System.Drawing.Size(98, 15)
        HowRegisteredLabel.TabIndex = 45
        HowRegisteredLabel.Text = "How Registered:"
        '
        'RegistrationIDLabel
        '
        RegistrationIDLabel.AutoSize = True
        RegistrationIDLabel.Enabled = False
        RegistrationIDLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        RegistrationIDLabel.ForeColor = System.Drawing.SystemColors.ControlDark
        RegistrationIDLabel.Location = New System.Drawing.Point(791, 91)
        RegistrationIDLabel.Name = "RegistrationIDLabel"
        RegistrationIDLabel.Size = New System.Drawing.Size(79, 13)
        RegistrationIDLabel.TabIndex = 441
        RegistrationIDLabel.Text = "Registration ID:"
        '
        'Label2
        '
        Label2.AutoSize = True
        Label2.Enabled = False
        Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Label2.ForeColor = System.Drawing.SystemColors.ControlDark
        Label2.Location = New System.Drawing.Point(669, 10)
        Label2.Name = "Label2"
        Label2.Size = New System.Drawing.Size(56, 15)
        Label2.TabIndex = 533
        Label2.Text = "Order ID:"
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.SystemColors.Highlight
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Controls.Add(Me.btnSaveExit)
        Me.Panel2.Controls.Add(Me.btnDelete)
        Me.Panel2.Controls.Add(Me.btnHelp)
        Me.Panel2.Location = New System.Drawing.Point(865, 12)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(148, 40)
        Me.Panel2.TabIndex = 424
        '
        'btnSaveExit
        '
        Me.btnSaveExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSaveExit.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.btnSaveExit.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSaveExit.Image = CType(resources.GetObject("btnSaveExit.Image"), System.Drawing.Image)
        Me.btnSaveExit.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSaveExit.Location = New System.Drawing.Point(105, 1)
        Me.btnSaveExit.Name = "btnSaveExit"
        Me.btnSaveExit.Size = New System.Drawing.Size(40, 35)
        Me.btnSaveExit.TabIndex = 416
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
        Me.btnHelp.Location = New System.Drawing.Point(45, 7)
        Me.btnHelp.Name = "btnHelp"
        Me.btnHelp.Size = New System.Drawing.Size(25, 25)
        Me.btnHelp.TabIndex = 423
        Me.btnHelp.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnHelp.UseVisualStyleBackColor = False
        Me.btnHelp.Visible = False
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
        Me.miDelete.Text = "Delete Registration"
        '
        'miHelp
        '
        Me.miHelp.Index = 2
        Me.miHelp.Text = "Help"
        '
        'TblWRegPOpupBindingSource
        '
        Me.TblWRegPOpupBindingSource.DataMember = "tblWRegPopup"
        Me.TblWRegPOpupBindingSource.DataSource = Me.DsWRegPopup
        '
        'DsWRegPopup
        '
        Me.DsWRegPopup.DataSetName = "dsWRegPopup"
        Me.DsWRegPopup.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'chkVeg
        '
        Me.chkVeg.DataBindings.Add(New System.Windows.Forms.Binding("Checked", Me.TblWRegPOpupBindingSource, "Vegetarian", True))
        Me.chkVeg.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.chkVeg.Location = New System.Drawing.Point(139, 136)
        Me.chkVeg.Name = "chkVeg"
        Me.chkVeg.Size = New System.Drawing.Size(108, 24)
        Me.chkVeg.TabIndex = 6
        Me.chkVeg.Text = "Vegetarian"
        '
        'cboHeard
        '
        '  Me.cboHeard.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        '  Me.cboHeard.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboHeard.DataBindings.Add(New System.Windows.Forms.Binding("SelectedItem", Me.TblWRegPOpupBindingSource, "HowHeard", True))
        Me.cboHeard.Enabled = False
        Me.cboHeard.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboHeard.FormattingEnabled = True
        Me.cboHeard.Items.AddRange(New Object() {"Email", "Center Event", "Mailing", "Newspaper", "Person", "Website", "Other"})
        Me.cboHeard.Location = New System.Drawing.Point(139, 223)
        Me.cboHeard.Name = "cboHeard"
        Me.cboHeard.Size = New System.Drawing.Size(180, 24)
        Me.cboHeard.TabIndex = 9
        Me.ToolTip1.SetToolTip(Me.cboHeard, "temporarily disabled")
        Me.cboHeard.Visible = False
        '
        'cboRegistered
        '
        '   Me.cboRegistered.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        '   Me.cboRegistered.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboRegistered.DataBindings.Add(New System.Windows.Forms.Binding("SelectedItem", Me.TblWRegPOpupBindingSource, "HowRegistered", True))
        Me.cboRegistered.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboRegistered.FormattingEnabled = True
        Me.cboRegistered.Items.AddRange(New Object() {"Email", "Fax", "In Person", "Mail", "Phone", "Website"})
        Me.cboRegistered.Location = New System.Drawing.Point(139, 253)
        Me.cboRegistered.Name = "cboRegistered"
        Me.cboRegistered.Size = New System.Drawing.Size(180, 24)
        Me.cboRegistered.TabIndex = 10
        '
        'rtbNotes
        '
        Me.rtbNotes.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.TblWRegPOpupBindingSource, "Notes", True))
        Me.rtbNotes.Location = New System.Drawing.Point(38, 329)
        Me.rtbNotes.Name = "rtbNotes"
        Me.rtbNotes.Size = New System.Drawing.Size(305, 86)
        Me.rtbNotes.TabIndex = 11
        Me.rtbNotes.Text = ""
        '
        'rtbReason
        '
        Me.rtbReason.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.TblWRegPOpupBindingSource, "Questions", True))
        Me.rtbReason.Location = New System.Drawing.Point(405, 229)
        Me.rtbReason.MaxLength = 4000
        Me.rtbReason.Name = "rtbReason"
        Me.rtbReason.Size = New System.Drawing.Size(333, 186)
        Me.rtbReason.TabIndex = 12
        Me.rtbReason.Text = ""
        '
        'cboClergy
        '
        '    Me.cboClergy.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        '    Me.cboClergy.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboClergy.DataBindings.Add(New System.Windows.Forms.Binding("SelectedItem", Me.TblWRegPOpupBindingSource, "ClergyLay", True))
        Me.cboClergy.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.TblWRegPOpupBindingSource, "ClergyLay", True))
        '   Me.cboClergy.DropDownStyle = InfoCtr.ComboBoxRelaxedStyle.DropDownList
        Me.cboClergy.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboClergy.FormattingEnabled = True
        Me.cboClergy.Items.AddRange(New Object() {"C", "L"})
        Me.cboClergy.Location = New System.Drawing.Point(138, 164)
        Me.cboClergy.Name = "cboClergy"
        Me.cboClergy.Size = New System.Drawing.Size(66, 24)
        Me.cboClergy.TabIndex = 7
        '
        'cboPersonal
        '
        '  Me.cboPersonal.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        '  Me.cboPersonal.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboPersonal.FormattingEnabled = True
        Me.cboPersonal.Items.AddRange(New Object() {"Apply to Congregation", "Personal Growth", "Both"})
        Me.cboPersonal.Location = New System.Drawing.Point(516, 486)
        Me.cboPersonal.Name = "cboPersonal"
        Me.cboPersonal.Size = New System.Drawing.Size(110, 21)
        Me.cboPersonal.TabIndex = 30
        Me.cboPersonal.Visible = False
        '
        'cboGender
        '
        '  Me.cboGender.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        '  Me.cboGender.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboGender.DataBindings.Add(New System.Windows.Forms.Binding("SelectedItem", Me.TblWRegPOpupBindingSource, "MaleFemale", True))
        Me.cboGender.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.TblWRegPOpupBindingSource, "MaleFemale", True))
        '    Me.cboGender.DropDownStyle = InfoCtr.ComboBoxRelaxedStyle.DropDownList
        Me.cboGender.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboGender.FormattingEnabled = True
        Me.cboGender.Items.AddRange(New Object() {"F", "M"})
        Me.cboGender.Location = New System.Drawing.Point(138, 193)
        Me.cboGender.Name = "cboGender"
        Me.cboGender.Size = New System.Drawing.Size(66, 24)
        Me.cboGender.TabIndex = 8
        '
        'txtCommittee
        '
        Me.txtCommittee.Location = New System.Drawing.Point(588, 525)
        Me.txtCommittee.Name = "txtCommittee"
        Me.txtCommittee.Size = New System.Drawing.Size(38, 20)
        Me.txtCommittee.TabIndex = 40
        Me.txtCommittee.Visible = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(385, 530)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(116, 15)
        Me.Label4.TabIndex = 440
        Me.Label4.Text = "Existing Committee:"
        Me.Label4.Visible = False
        '
        'btnNewContact
        '
        Me.btnNewContact.Font = New System.Drawing.Font("Webdings", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.btnNewContact.ForeColor = System.Drawing.Color.Goldenrod
        Me.btnNewContact.Location = New System.Drawing.Point(23, 94)
        Me.btnNewContact.Margin = New System.Windows.Forms.Padding(0)
        Me.btnNewContact.Name = "btnNewContact"
        Me.btnNewContact.Size = New System.Drawing.Size(22, 25)
        Me.btnNewContact.TabIndex = 541
        Me.btnNewContact.Text = "˜"
        Me.btnNewContact.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage
        Me.ToolTip1.SetToolTip(Me.btnNewContact, "Add new contact to this organization")
        Me.btnNewContact.UseVisualStyleBackColor = True
        '
        'btnRefreshContacts
        '
        Me.btnRefreshContacts.Location = New System.Drawing.Point(614, 105)
        Me.btnRefreshContacts.Name = "btnRefreshContacts"
        Me.btnRefreshContacts.Size = New System.Drawing.Size(56, 19)
        Me.btnRefreshContacts.TabIndex = 546
        Me.btnRefreshContacts.TabStop = False
        Me.btnRefreshContacts.Text = "refresh"
        Me.ToolTip1.SetToolTip(Me.btnRefreshContacts, "click here to reload Registration dropdown")
        Me.btnRefreshContacts.UseVisualStyleBackColor = True
        '
        'SqlConnection1
        '
        Me.SqlConnection1.ConnectionString = "Data Source=SOLOMON;Initial Catalog=InfoCtr_be;Integrated Security=True"
        Me.SqlConnection1.FireInfoMessageEventOnUserErrors = False
        '
        'ErrorProvider1
        '
        Me.ErrorProvider1.ContainerControl = Me
        '
        'fldContact
        '
        Me.fldContact.BackColor = System.Drawing.SystemColors.Control
        Me.fldContact.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fldContact.ForeColor = System.Drawing.Color.DarkGreen
        Me.fldContact.Location = New System.Drawing.Point(58, 95)
        Me.fldContact.Name = "fldContact"
        Me.fldContact.Size = New System.Drawing.Size(77, 21)
        Me.fldContact.TabIndex = 498
        Me.fldContact.Text = "Registrant:"
        Me.fldContact.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblRegistrantName
        '
        Me.lblRegistrantName.AutoSize = True
        Me.lblRegistrantName.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.lblRegistrantName.Location = New System.Drawing.Point(778, 139)
        Me.lblRegistrantName.MinimumSize = New System.Drawing.Size(200, 0)
        Me.lblRegistrantName.Name = "lblRegistrantName"
        Me.lblRegistrantName.Size = New System.Drawing.Size(200, 13)
        Me.lblRegistrantName.TabIndex = 532
        Me.lblRegistrantName.Text = "lblRegistrantName"
        '
        'fldRegistrationID
        '
        Me.fldRegistrationID.BackColor = System.Drawing.SystemColors.Control
        Me.fldRegistrationID.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.fldRegistrationID.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.TblWRegPOpupBindingSource, "RegistrationID", True))
        Me.fldRegistrationID.Enabled = False
        Me.fldRegistrationID.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fldRegistrationID.Location = New System.Drawing.Point(876, 91)
        Me.fldRegistrationID.Name = "fldRegistrationID"
        Me.fldRegistrationID.ReadOnly = True
        Me.fldRegistrationID.Size = New System.Drawing.Size(44, 13)
        Me.fldRegistrationID.TabIndex = 442
        '
        'fldOrderID
        '
        Me.fldOrderID.BackColor = System.Drawing.SystemColors.Control
        Me.fldOrderID.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.fldOrderID.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.TblWRegPOpupBindingSource, "OrderNum", True))
        Me.fldOrderID.Enabled = False
        Me.fldOrderID.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fldOrderID.Location = New System.Drawing.Point(731, 10)
        Me.fldOrderID.Name = "fldOrderID"
        Me.fldOrderID.ReadOnly = True
        Me.fldOrderID.Size = New System.Drawing.Size(44, 14)
        Me.fldOrderID.TabIndex = 534
        '
        'cboContact
        '
        '  Me.cboContact.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append
        '  Me.cboContact.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboContact.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.TblWRegPOpupBindingSource, "ContactNum", True))
        Me.cboContact.DisplayMember = "ContactName"
        Me.cboContact.DropDownWidth = 600
        Me.cboContact.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboContact.FormattingEnabled = True
        Me.cboContact.Location = New System.Drawing.Point(139, 95)
        Me.cboContact.MaxDropDownItems = 15
        Me.cboContact.Name = "cboContact"
        Me.cboContact.Size = New System.Drawing.Size(461, 24)
        Me.cboContact.TabIndex = 2
        Me.cboContact.ValueMember = "ContactID"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.DarkGreen
        Me.Label3.Location = New System.Drawing.Point(97, 39)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(40, 15)
        Me.Label3.TabIndex = 544
        Me.Label3.Text = "Event:"
        '
        'cboEvent
        '
        '  Me.cboEvent.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        '  Me.cboEvent.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboEvent.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.TblWRegPOpupBindingSource, "EventNum", True))
        Me.cboEvent.DisplayMember = "EventName"
        Me.cboEvent.DropDownWidth = 600
        Me.cboEvent.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboEvent.FormattingEnabled = True
        Me.cboEvent.Location = New System.Drawing.Point(139, 36)
        Me.cboEvent.MaxDropDownItems = 15
        Me.cboEvent.Name = "cboEvent"
        Me.cboEvent.Size = New System.Drawing.Size(461, 24)
        Me.cboEvent.TabIndex = 0
        Me.cboEvent.TabStop = False
        Me.cboEvent.ValueMember = "EventID"
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.SystemColors.Control
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label8.Location = New System.Drawing.Point(18, 9)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(323, 21)
        Me.Label8.TabIndex = 547
        Me.Label8.Text = "ENTERING NEW REGISTRATION"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.DarkGreen
        Me.Label1.Location = New System.Drawing.Point(57, 68)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(80, 15)
        Me.Label1.TabIndex = 549
        Me.Label1.Text = "Organization:"
        '
        'cboOrg
        '
        '   Me.cboOrg.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        '   Me.cboOrg.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboOrg.DisplayMember = "Org"
        Me.cboOrg.DropDownWidth = 600
        Me.cboOrg.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboOrg.FormattingEnabled = True
        Me.cboOrg.Location = New System.Drawing.Point(139, 65)
        Me.cboOrg.MaxDropDownItems = 15
        Me.cboOrg.Name = "cboOrg"
        Me.cboOrg.Size = New System.Drawing.Size(461, 24)
        Me.cboOrg.TabIndex = 0
        Me.cboOrg.ValueMember = "OrgID"
        '
        'cboRegion
        '
        '   Me.cboRegion.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        '   Me.cboRegion.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboRegion.DropDownWidth = 600
        Me.cboRegion.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboRegion.FormattingEnabled = True
        Me.cboRegion.Location = New System.Drawing.Point(606, 65)
        Me.cboRegion.MaxDropDownItems = 15
        Me.cboRegion.Name = "cboRegion"
        Me.cboRegion.Size = New System.Drawing.Size(132, 24)
        Me.cboRegion.TabIndex = 550
        Me.cboRegion.TabStop = False
        '
        'TblWRegPOpupTableAdapter
        '
        Me.TblWRegPOpupTableAdapter.ClearBeforeFill = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.TblWRegPOpupBindingSource, "EventNum", True))
        Me.Label5.ForeColor = System.Drawing.SystemColors.GrayText
        Me.Label5.Location = New System.Drawing.Point(611, 41)
        Me.Label5.MinimumSize = New System.Drawing.Size(75, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(75, 13)
        Me.Label5.TabIndex = 551
        Me.Label5.Text = "Label5"
        '
        'frmNewWOrderPopup2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(1020, 700)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.cboRegion)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cboOrg)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.btnRefreshContacts)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.cboEvent)
        Me.Controls.Add(Me.btnNewContact)
        Me.Controls.Add(Me.cboContact)
        Me.Controls.Add(Label2)
        Me.Controls.Add(Me.fldOrderID)
        Me.Controls.Add(Me.lblRegistrantName)
        Me.Controls.Add(Me.fldContact)
        Me.Controls.Add(RegistrationIDLabel)
        Me.Controls.Add(Me.fldRegistrationID)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txtCommittee)
        Me.Controls.Add(Me.cboGender)
        Me.Controls.Add(Me.cboPersonal)
        Me.Controls.Add(Me.cboClergy)
        Me.Controls.Add(Me.rtbReason)
        Me.Controls.Add(Me.rtbNotes)
        Me.Controls.Add(Me.cboRegistered)
        Me.Controls.Add(Me.cboHeard)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(NotesLabel)
        Me.Controls.Add(ClergyLayLabel)
        Me.Controls.Add(PersonalCongregationalLabel)
        Me.Controls.Add(MaleFemaleLabel)
        Me.Controls.Add(QuestionsLabel)
        Me.Controls.Add(HowHeardLabel)
        Me.Controls.Add(HowRegisteredLabel)
        Me.Controls.Add(Me.chkVeg)
        Me.Menu = Me.MainMenu1
        Me.Name = "frmNewWOrderPopup2"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Tag = "PROFILE"
        Me.Text = "ADDING NEW REGISTRATION"
        Me.TopMost = True
        Me.Panel2.ResumeLayout(False)
        CType(Me.TblWRegPOpupBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsWRegPopup, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents btnSaveExit As System.Windows.Forms.Button
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents btnHelp As System.Windows.Forms.Button
    Friend WithEvents MainMenu1 As System.Windows.Forms.MainMenu
    Friend WithEvents MenuItem1 As System.Windows.Forms.MenuItem
    Friend WithEvents miClose As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem2 As System.Windows.Forms.MenuItem
    Friend WithEvents miSave As System.Windows.Forms.MenuItem
    Friend WithEvents miCancel As System.Windows.Forms.MenuItem
    Friend WithEvents miDelete As System.Windows.Forms.MenuItem
    Friend WithEvents chkVeg As System.Windows.Forms.CheckBox
    Friend WithEvents cboHeard As InfoCtr.ComboBoxRelaxed
    Friend WithEvents cboRegistered As InfoCtr.ComboBoxRelaxed
    Friend WithEvents rtbNotes As System.Windows.Forms.RichTextBox
    Friend WithEvents rtbReason As System.Windows.Forms.RichTextBox
    Friend WithEvents cboClergy As InfoCtr.ComboBoxRelaxed
    Friend WithEvents cboPersonal As InfoCtr.ComboBoxRelaxed
    Friend WithEvents cboGender As InfoCtr.ComboBoxRelaxed
    Friend WithEvents txtCommittee As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents SqlConnection1 As System.Data.SqlClient.SqlConnection
    Friend WithEvents miHelp As System.Windows.Forms.MenuItem
    Friend WithEvents ErrorProvider1 As System.Windows.Forms.ErrorProvider
    Friend WithEvents lblRegistrantName As System.Windows.Forms.Label
    Friend WithEvents fldContact As System.Windows.Forms.TextBox
    Friend WithEvents TblWRegPOpupBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents DsWRegPopup As InfoCtr.dsWRegPopup
    Friend WithEvents TblWRegPOpupTableAdapter As InfoCtr.dsWRegPopupTableAdapters.tblWRegPopupTableAdapter
    Friend WithEvents fldRegistrationID As System.Windows.Forms.TextBox
    Friend WithEvents fldOrderID As System.Windows.Forms.TextBox
    Friend WithEvents btnNewContact As System.Windows.Forms.Button
    Friend WithEvents cboContact As InfoCtr.ComboBoxRelaxed
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cboEvent As InfoCtr.ComboBoxRelaxed
    Friend WithEvents btnRefreshContacts As System.Windows.Forms.Button
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents cboRegion As InfoCtr.ComboBoxRelaxed
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cboOrg As InfoCtr.ComboBoxRelaxed
    Friend WithEvents Label5 As System.Windows.Forms.Label
    '  Friend WithEvents DsContacts As WindowsApplication11.dsContacts
End Class
