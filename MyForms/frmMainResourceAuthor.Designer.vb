<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMainResourceAuthor
    Inherits System.Windows.Forms.Form

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim ExtraIDLabel As System.Windows.Forms.Label
        Dim ResourceNumLabel As System.Windows.Forms.Label
        Dim NotesLabel As System.Windows.Forms.Label
        Dim Label7 As System.Windows.Forms.Label
        Dim SuffixLabel As System.Windows.Forms.Label
        Dim MiddleLabel As System.Windows.Forms.Label
        Dim PrefixLabel As System.Windows.Forms.Label
        Dim FirstNameLabel As System.Windows.Forms.Label
        Dim LastNameLabel As System.Windows.Forms.Label
        Dim PhoneLabel As System.Windows.Forms.Label
        Dim FaxLabel As System.Windows.Forms.Label
        Dim EMailLabel As System.Windows.Forms.Label
        Dim JobTitleLabel As System.Windows.Forms.Label
        Dim LocationLabel As System.Windows.Forms.Label
        Dim EventDateLabel As System.Windows.Forms.Label
        Dim EventAnnualLabel As System.Windows.Forms.Label
        Dim Phone2Label As System.Windows.Forms.Label
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMainResourceAuthor))
        Me.ExtraIDTextBox = New System.Windows.Forms.TextBox()
        Me.DsMainResourceExtra = New InfoCtr.dsMainResourceExtra()
        Me.txtResourceNum = New System.Windows.Forms.TextBox()
        Me.txtNotes = New System.Windows.Forms.TextBox()
        Me.cboAuthor = New InfoCtr.ComboBoxRelaxed()
        Me.daspMainResourceExtra = New System.Data.SqlClient.SqlDataAdapter()
        Me.SqlCommand1 = New System.Data.SqlClient.SqlCommand()
        Me.SqlInsertCommand = New System.Data.SqlClient.SqlCommand()
        Me.SqlSelectCommand = New System.Data.SqlClient.SqlCommand()
        Me.SqlUpdateCommand = New System.Data.SqlClient.SqlCommand()
        Me.MainMenu1 = New System.Windows.Forms.MainMenu(Me.components)
        Me.MenuItem1 = New System.Windows.Forms.MenuItem()
        Me.miClose = New System.Windows.Forms.MenuItem()
        Me.MenuItem2 = New System.Windows.Forms.MenuItem()
        Me.miSave = New System.Windows.Forms.MenuItem()
        Me.miCancel = New System.Windows.Forms.MenuItem()
        Me.miDelete = New System.Windows.Forms.MenuItem()
        Me.miHelp = New System.Windows.Forms.MenuItem()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.btnSaveExit = New System.Windows.Forms.Button()
        Me.pnlName = New System.Windows.Forms.Panel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.LastNameTextBox = New System.Windows.Forms.TextBox()
        Me.SuffixTextBox = New System.Windows.Forms.TextBox()
        Me.MiddleTextBox = New System.Windows.Forms.TextBox()
        Me.PrefixTextBox = New System.Windows.Forms.TextBox()
        Me.FirstNameTextBox = New System.Windows.Forms.TextBox()
        Me.pnlPhone = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.PhoneTextBox = New System.Windows.Forms.TextBox()
        Me.FaxTextBox = New System.Windows.Forms.TextBox()
        Me.EMailTextBox = New System.Windows.Forms.TextBox()
        Me.JobTitleTextBox = New System.Windows.Forms.TextBox()
        Me.pnlAuthor = New System.Windows.Forms.Panel()
        Me.lblPerson = New System.Windows.Forms.Label()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.pgName = New System.Windows.Forms.TabPage()
        Me.pgEvent = New System.Windows.Forms.TabPage()
        Me.ComboBox1 = New InfoCtr.ComboBoxRelaxed()
        Me.LocationTextBox = New System.Windows.Forms.TextBox()
        Me.Phone2TextBox = New System.Windows.Forms.TextBox()
        Me.EventDateTextBox = New System.Windows.Forms.TextBox()
        ExtraIDLabel = New System.Windows.Forms.Label()
        ResourceNumLabel = New System.Windows.Forms.Label()
        NotesLabel = New System.Windows.Forms.Label()
        Label7 = New System.Windows.Forms.Label()
        SuffixLabel = New System.Windows.Forms.Label()
        MiddleLabel = New System.Windows.Forms.Label()
        PrefixLabel = New System.Windows.Forms.Label()
        FirstNameLabel = New System.Windows.Forms.Label()
        LastNameLabel = New System.Windows.Forms.Label()
        PhoneLabel = New System.Windows.Forms.Label()
        FaxLabel = New System.Windows.Forms.Label()
        EMailLabel = New System.Windows.Forms.Label()
        JobTitleLabel = New System.Windows.Forms.Label()
        LocationLabel = New System.Windows.Forms.Label()
        EventDateLabel = New System.Windows.Forms.Label()
        EventAnnualLabel = New System.Windows.Forms.Label()
        Phone2Label = New System.Windows.Forms.Label()
        CType(Me.DsMainResourceExtra, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel3.SuspendLayout()
        Me.pnlName.SuspendLayout()
        Me.pnlPhone.SuspendLayout()
        Me.pnlAuthor.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.pgName.SuspendLayout()
        Me.pgEvent.SuspendLayout()
        Me.SuspendLayout()
        '
        'ExtraIDLabel
        '
        ExtraIDLabel.AutoSize = True
        ExtraIDLabel.ForeColor = System.Drawing.SystemColors.ControlDark
        ExtraIDLabel.Location = New System.Drawing.Point(538, 61)
        ExtraIDLabel.Name = "ExtraIDLabel"
        ExtraIDLabel.Size = New System.Drawing.Size(48, 13)
        ExtraIDLabel.TabIndex = 1
        ExtraIDLabel.Text = "Extra ID:"
        '
        'ResourceNumLabel
        '
        ResourceNumLabel.AutoSize = True
        ResourceNumLabel.ForeColor = System.Drawing.SystemColors.ControlDark
        ResourceNumLabel.Location = New System.Drawing.Point(513, 87)
        ResourceNumLabel.Name = "ResourceNumLabel"
        ResourceNumLabel.Size = New System.Drawing.Size(81, 13)
        ResourceNumLabel.TabIndex = 3
        ResourceNumLabel.Text = "Resource Num:"
        '
        'NotesLabel
        '
        NotesLabel.AutoSize = True
        NotesLabel.Location = New System.Drawing.Point(431, 162)
        NotesLabel.Name = "NotesLabel"
        NotesLabel.Size = New System.Drawing.Size(38, 13)
        NotesLabel.TabIndex = 33
        NotesLabel.Text = "Notes:"
        '
        'Label7
        '
        Label7.AutoSize = True
        Label7.Location = New System.Drawing.Point(3, 3)
        Label7.Name = "Label7"
        Label7.Size = New System.Drawing.Size(61, 13)
        Label7.TabIndex = 47
        Label7.Text = "Select one:"
        '
        'SuffixLabel
        '
        SuffixLabel.AutoSize = True
        SuffixLabel.Location = New System.Drawing.Point(93, 134)
        SuffixLabel.Name = "SuffixLabel"
        SuffixLabel.Size = New System.Drawing.Size(36, 13)
        SuffixLabel.TabIndex = 23
        SuffixLabel.Text = "Suffix:"
        '
        'MiddleLabel
        '
        MiddleLabel.AutoSize = True
        MiddleLabel.Location = New System.Drawing.Point(88, 108)
        MiddleLabel.Name = "MiddleLabel"
        MiddleLabel.Size = New System.Drawing.Size(41, 13)
        MiddleLabel.TabIndex = 21
        MiddleLabel.Text = "Middle:"
        '
        'PrefixLabel
        '
        PrefixLabel.AutoSize = True
        PrefixLabel.Location = New System.Drawing.Point(93, 82)
        PrefixLabel.Name = "PrefixLabel"
        PrefixLabel.Size = New System.Drawing.Size(36, 13)
        PrefixLabel.TabIndex = 19
        PrefixLabel.Text = "Prefix:"
        '
        'FirstNameLabel
        '
        FirstNameLabel.AutoSize = True
        FirstNameLabel.Location = New System.Drawing.Point(69, 56)
        FirstNameLabel.Name = "FirstNameLabel"
        FirstNameLabel.Size = New System.Drawing.Size(60, 13)
        FirstNameLabel.TabIndex = 17
        FirstNameLabel.Text = "First Name:"
        '
        'LastNameLabel
        '
        LastNameLabel.AutoSize = True
        LastNameLabel.Location = New System.Drawing.Point(68, 30)
        LastNameLabel.Name = "LastNameLabel"
        LastNameLabel.Size = New System.Drawing.Size(61, 13)
        LastNameLabel.TabIndex = 15
        LastNameLabel.Text = "Last Name:"
        '
        'PhoneLabel
        '
        PhoneLabel.AutoSize = True
        PhoneLabel.Location = New System.Drawing.Point(46, 30)
        PhoneLabel.Name = "PhoneLabel"
        PhoneLabel.Size = New System.Drawing.Size(41, 13)
        PhoneLabel.TabIndex = 33
        PhoneLabel.Text = "Phone:"
        '
        'FaxLabel
        '
        FaxLabel.AutoSize = True
        FaxLabel.Location = New System.Drawing.Point(46, 56)
        FaxLabel.Name = "FaxLabel"
        FaxLabel.Size = New System.Drawing.Size(27, 13)
        FaxLabel.TabIndex = 35
        FaxLabel.Text = "Fax:"
        '
        'EMailLabel
        '
        EMailLabel.AutoSize = True
        EMailLabel.Location = New System.Drawing.Point(46, 82)
        EMailLabel.Name = "EMailLabel"
        EMailLabel.Size = New System.Drawing.Size(36, 13)
        EMailLabel.TabIndex = 37
        EMailLabel.Text = "EMail:"
        '
        'JobTitleLabel
        '
        JobTitleLabel.AutoSize = True
        JobTitleLabel.Location = New System.Drawing.Point(46, 108)
        JobTitleLabel.Name = "JobTitleLabel"
        JobTitleLabel.Size = New System.Drawing.Size(50, 13)
        JobTitleLabel.TabIndex = 39
        JobTitleLabel.Text = "Job Title:"
        '
        'LocationLabel
        '
        LocationLabel.AutoSize = True
        LocationLabel.Location = New System.Drawing.Point(56, 94)
        LocationLabel.Name = "LocationLabel"
        LocationLabel.Size = New System.Drawing.Size(51, 13)
        LocationLabel.TabIndex = 38
        LocationLabel.Text = "Location:"
        '
        'EventDateLabel
        '
        EventDateLabel.AutoSize = True
        EventDateLabel.Location = New System.Drawing.Point(38, 35)
        EventDateLabel.Name = "EventDateLabel"
        EventDateLabel.Size = New System.Drawing.Size(69, 13)
        EventDateLabel.TabIndex = 36
        EventDateLabel.Text = "Event Dates:"
        '
        'EventAnnualLabel
        '
        EventAnnualLabel.AutoSize = True
        EventAnnualLabel.Location = New System.Drawing.Point(38, 65)
        EventAnnualLabel.Name = "EventAnnualLabel"
        EventAnnualLabel.Size = New System.Drawing.Size(74, 13)
        EventAnnualLabel.TabIndex = 37
        EventAnnualLabel.Text = "Annual Event:"
        '
        'Phone2Label
        '
        Phone2Label.AutoSize = True
        Phone2Label.Location = New System.Drawing.Point(32, 199)
        Phone2Label.Name = "Phone2Label"
        Phone2Label.Size = New System.Drawing.Size(85, 13)
        Phone2Label.TabIndex = 39
        Phone2Label.Text = "Location Phone:"
        '
        'ExtraIDTextBox
        '
        Me.ExtraIDTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.ExtraIDTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsMainResourceExtra, "tblResourceExtra.ResourceExtraID", True))
        Me.ExtraIDTextBox.Enabled = False
        Me.ExtraIDTextBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ExtraIDTextBox.Location = New System.Drawing.Point(600, 58)
        Me.ExtraIDTextBox.Name = "ExtraIDTextBox"
        Me.ExtraIDTextBox.ReadOnly = True
        Me.ExtraIDTextBox.Size = New System.Drawing.Size(77, 14)
        Me.ExtraIDTextBox.TabIndex = 2
        Me.ExtraIDTextBox.TabStop = False
        '
        'DsMainResourceExtra
        '
        Me.DsMainResourceExtra.DataSetName = "dsMainResourceExtra"
        Me.DsMainResourceExtra.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'txtResourceNum
        '
        Me.txtResourceNum.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtResourceNum.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsMainResourceExtra, "tblResourceExtra.ResourceNum", True))
        Me.txtResourceNum.Enabled = False
        Me.txtResourceNum.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtResourceNum.Location = New System.Drawing.Point(600, 84)
        Me.txtResourceNum.Name = "txtResourceNum"
        Me.txtResourceNum.ReadOnly = True
        Me.txtResourceNum.Size = New System.Drawing.Size(77, 14)
        Me.txtResourceNum.TabIndex = 4
        Me.txtResourceNum.TabStop = False
        '
        'txtNotes
        '
        Me.txtNotes.AcceptsReturn = True
        Me.txtNotes.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsMainResourceExtra, "tblResourceExtra.Notes", True))
        Me.txtNotes.Location = New System.Drawing.Point(475, 159)
        Me.txtNotes.Multiline = True
        Me.txtNotes.Name = "txtNotes"
        Me.txtNotes.Size = New System.Drawing.Size(193, 153)
        Me.txtNotes.TabIndex = 15
        '
        'cboAuthor
        '
        Me.cboAuthor.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboAuthor.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboAuthor.DataBindings.Add(New System.Windows.Forms.Binding("SelectedItem", Me.DsMainResourceExtra, "tblResourceExtra.AuthorEditor", True))
        Me.cboAuthor.FormattingEnabled = True
        Me.cboAuthor.Items.AddRange(New Object() {"Author", "Editor", "Presenter", "Resource", "Event"})
        Me.cboAuthor.Location = New System.Drawing.Point(84, 3)
        Me.cboAuthor.Name = "cboAuthor"
        Me.cboAuthor.RestrictContentToListItems = True
        Me.cboAuthor.Size = New System.Drawing.Size(115, 21)
        Me.cboAuthor.TabIndex = 0
        '
        'daspMainResourceExtra
        '
        Me.daspMainResourceExtra.DeleteCommand = Me.SqlCommand1
        Me.daspMainResourceExtra.InsertCommand = Me.SqlInsertCommand
        Me.daspMainResourceExtra.SelectCommand = Me.SqlSelectCommand
        Me.daspMainResourceExtra.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "tblResourceExtra", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("ResourceExtraID", "ResourceExtraID"), New System.Data.Common.DataColumnMapping("ResourceNum", "ResourceNum"), New System.Data.Common.DataColumnMapping("LastName", "LastName"), New System.Data.Common.DataColumnMapping("FirstName", "FirstName"), New System.Data.Common.DataColumnMapping("Prefix", "Prefix"), New System.Data.Common.DataColumnMapping("Middle", "Middle"), New System.Data.Common.DataColumnMapping("Suffix", "Suffix"), New System.Data.Common.DataColumnMapping("AuthorEditor", "AuthorEditor"), New System.Data.Common.DataColumnMapping("Phone", "Phone"), New System.Data.Common.DataColumnMapping("Fax", "Fax"), New System.Data.Common.DataColumnMapping("EMail", "EMail"), New System.Data.Common.DataColumnMapping("JobTitle", "JobTitle"), New System.Data.Common.DataColumnMapping("EventDate", "EventDate"), New System.Data.Common.DataColumnMapping("EventAnnual", "EventAnnual"), New System.Data.Common.DataColumnMapping("Location", "Location"), New System.Data.Common.DataColumnMapping("Phone2", "Phone2"), New System.Data.Common.DataColumnMapping("Notes", "Notes"), New System.Data.Common.DataColumnMapping("Stamped", "Stamped")})})
        Me.daspMainResourceExtra.UpdateCommand = Me.SqlUpdateCommand
        '
        'SqlCommand1
        '
        Me.SqlCommand1.CommandText = "dbo.MainResourceExtraDelete"
        Me.SqlCommand1.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlCommand1.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@ID", System.Data.SqlDbType.Int, 0, "ResourceExtraID")})
        '
        'SqlInsertCommand
        '
        Me.SqlInsertCommand.CommandType = System.Data.CommandType.StoredProcedure
        '
        'SqlSelectCommand
        '
        Me.SqlSelectCommand.CommandText = "dbo.MainResourceExtra"
        Me.SqlSelectCommand.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlSelectCommand.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, False, CType(0, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing), New System.Data.SqlClient.SqlParameter("@ID", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, "3962")})
        '
        'SqlUpdateCommand
        '
        Me.SqlUpdateCommand.CommandText = "dbo.MainResourceExtraUpdate"
        Me.SqlUpdateCommand.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlUpdateCommand.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, False, CType(0, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing), New System.Data.SqlClient.SqlParameter("@ResourceNum", System.Data.SqlDbType.Int, 4, "ResourceNum"), New System.Data.SqlClient.SqlParameter("@LastName", System.Data.SqlDbType.VarChar, 50, "LastName"), New System.Data.SqlClient.SqlParameter("@FirstName", System.Data.SqlDbType.VarChar, 50, "FirstName"), New System.Data.SqlClient.SqlParameter("@Prefix", System.Data.SqlDbType.VarChar, 50, "Prefix"), New System.Data.SqlClient.SqlParameter("@Middle", System.Data.SqlDbType.VarChar, 50, "Middle"), New System.Data.SqlClient.SqlParameter("@Suffix", System.Data.SqlDbType.VarChar, 50, "Suffix"), New System.Data.SqlClient.SqlParameter("@AuthorEditor", System.Data.SqlDbType.VarChar, 50, "AuthorEditor"), New System.Data.SqlClient.SqlParameter("@Phone", System.Data.SqlDbType.VarChar, 50, "Phone"), New System.Data.SqlClient.SqlParameter("@Fax", System.Data.SqlDbType.VarChar, 50, "Fax"), New System.Data.SqlClient.SqlParameter("@EMail", System.Data.SqlDbType.VarChar, 50, "EMail"), New System.Data.SqlClient.SqlParameter("@JobTitle", System.Data.SqlDbType.VarChar, 50, "JobTitle"), New System.Data.SqlClient.SqlParameter("@EventDate", System.Data.SqlDbType.VarChar, 50, "EventDate"), New System.Data.SqlClient.SqlParameter("@EventAnnual", System.Data.SqlDbType.VarChar, 50, "EventAnnual"), New System.Data.SqlClient.SqlParameter("@Location", System.Data.SqlDbType.VarChar, 50, "Location"), New System.Data.SqlClient.SqlParameter("@Phone2", System.Data.SqlDbType.VarChar, 50, "Phone2"), New System.Data.SqlClient.SqlParameter("@Notes", System.Data.SqlDbType.NText, 1073741823, "Notes"), New System.Data.SqlClient.SqlParameter("@ID", System.Data.SqlDbType.Int, 4, "ResourceExtraID"), New System.Data.SqlClient.SqlParameter("@Stamp", System.Data.SqlDbType.Timestamp, 8, "Stamped")})
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
        Me.miDelete.Text = "Delete this Entry"
        '
        'miHelp
        '
        Me.miHelp.Enabled = False
        Me.miHelp.Index = 2
        Me.miHelp.Text = "Help"
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.SystemColors.Highlight
        Me.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel3.Controls.Add(Me.btnSaveExit)
        Me.Panel3.Location = New System.Drawing.Point(631, 5)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(50, 40)
        Me.Panel3.TabIndex = 416
        '
        'btnSaveExit
        '
        Me.btnSaveExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSaveExit.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.btnSaveExit.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSaveExit.Image = CType(resources.GetObject("btnSaveExit.Image"), System.Drawing.Image)
        Me.btnSaveExit.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSaveExit.Location = New System.Drawing.Point(5, 1)
        Me.btnSaveExit.Name = "btnSaveExit"
        Me.btnSaveExit.Size = New System.Drawing.Size(40, 35)
        Me.btnSaveExit.TabIndex = 418
        Me.btnSaveExit.TabStop = False
        Me.btnSaveExit.Text = "Close"
        Me.btnSaveExit.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSaveExit.UseVisualStyleBackColor = False
        '
        'pnlName
        '
        Me.pnlName.BackColor = System.Drawing.SystemColors.Control
        Me.pnlName.Controls.Add(Me.Label2)
        Me.pnlName.Controls.Add(Me.TextBox1)
        Me.pnlName.Controls.Add(Me.LastNameTextBox)
        Me.pnlName.Controls.Add(Me.SuffixTextBox)
        Me.pnlName.Controls.Add(SuffixLabel)
        Me.pnlName.Controls.Add(Me.MiddleTextBox)
        Me.pnlName.Controls.Add(MiddleLabel)
        Me.pnlName.Controls.Add(Me.PrefixTextBox)
        Me.pnlName.Controls.Add(PrefixLabel)
        Me.pnlName.Controls.Add(Me.FirstNameTextBox)
        Me.pnlName.Controls.Add(FirstNameLabel)
        Me.pnlName.Controls.Add(LastNameLabel)
        Me.pnlName.Location = New System.Drawing.Point(24, 50)
        Me.pnlName.Name = "pnlName"
        Me.pnlName.Size = New System.Drawing.Size(285, 156)
        Me.pnlName.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.ForeColor = System.Drawing.SystemColors.InactiveCaption
        Me.Label2.Location = New System.Drawing.Point(18, 5)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(56, 13)
        Me.Label2.TabIndex = 41
        Me.Label2.Text = "Name Info"
        Me.Label2.Visible = False
        '
        'TextBox1
        '
        Me.TextBox1.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsMainResourceExtra, "tblResourceExtra.AuthorEditor", True))
        Me.TextBox1.Location = New System.Drawing.Point(136, 2)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.ReadOnly = True
        Me.TextBox1.Size = New System.Drawing.Size(100, 20)
        Me.TextBox1.TabIndex = 1
        Me.TextBox1.TabStop = False
        Me.TextBox1.Visible = False
        '
        'LastNameTextBox
        '
        Me.LastNameTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsMainResourceExtra, "tblResourceExtra.LastName", True))
        Me.LastNameTextBox.Location = New System.Drawing.Point(136, 27)
        Me.LastNameTextBox.Name = "LastNameTextBox"
        Me.LastNameTextBox.Size = New System.Drawing.Size(100, 20)
        Me.LastNameTextBox.TabIndex = 1
        '
        'SuffixTextBox
        '
        Me.SuffixTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsMainResourceExtra, "tblResourceExtra.Suffix", True))
        Me.SuffixTextBox.Location = New System.Drawing.Point(136, 131)
        Me.SuffixTextBox.Name = "SuffixTextBox"
        Me.SuffixTextBox.Size = New System.Drawing.Size(100, 20)
        Me.SuffixTextBox.TabIndex = 5
        '
        'MiddleTextBox
        '
        Me.MiddleTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsMainResourceExtra, "tblResourceExtra.Middle", True))
        Me.MiddleTextBox.Location = New System.Drawing.Point(136, 105)
        Me.MiddleTextBox.Name = "MiddleTextBox"
        Me.MiddleTextBox.Size = New System.Drawing.Size(100, 20)
        Me.MiddleTextBox.TabIndex = 4
        '
        'PrefixTextBox
        '
        Me.PrefixTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsMainResourceExtra, "tblResourceExtra.Prefix", True))
        Me.PrefixTextBox.Location = New System.Drawing.Point(136, 79)
        Me.PrefixTextBox.Name = "PrefixTextBox"
        Me.PrefixTextBox.Size = New System.Drawing.Size(100, 20)
        Me.PrefixTextBox.TabIndex = 3
        '
        'FirstNameTextBox
        '
        Me.FirstNameTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsMainResourceExtra, "tblResourceExtra.FirstName", True))
        Me.FirstNameTextBox.Location = New System.Drawing.Point(136, 53)
        Me.FirstNameTextBox.Name = "FirstNameTextBox"
        Me.FirstNameTextBox.Size = New System.Drawing.Size(100, 20)
        Me.FirstNameTextBox.TabIndex = 2
        '
        'pnlPhone
        '
        Me.pnlPhone.BackColor = System.Drawing.SystemColors.Control
        Me.pnlPhone.Controls.Add(Me.Label1)
        Me.pnlPhone.Controls.Add(PhoneLabel)
        Me.pnlPhone.Controls.Add(Me.PhoneTextBox)
        Me.pnlPhone.Controls.Add(FaxLabel)
        Me.pnlPhone.Controls.Add(Me.FaxTextBox)
        Me.pnlPhone.Controls.Add(EMailLabel)
        Me.pnlPhone.Controls.Add(Me.EMailTextBox)
        Me.pnlPhone.Controls.Add(JobTitleLabel)
        Me.pnlPhone.Controls.Add(Me.JobTitleTextBox)
        Me.pnlPhone.Location = New System.Drawing.Point(27, 214)
        Me.pnlPhone.Name = "pnlPhone"
        Me.pnlPhone.Size = New System.Drawing.Size(364, 136)
        Me.pnlPhone.TabIndex = 6
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.ForeColor = System.Drawing.SystemColors.InactiveCaption
        Me.Label1.Location = New System.Drawing.Point(15, 6)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(65, 13)
        Me.Label1.TabIndex = 40
        Me.Label1.Text = "Contact Info"
        '
        'PhoneTextBox
        '
        Me.PhoneTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsMainResourceExtra, "tblResourceExtra.Phone", True))
        Me.PhoneTextBox.Location = New System.Drawing.Point(133, 27)
        Me.PhoneTextBox.Name = "PhoneTextBox"
        Me.PhoneTextBox.Size = New System.Drawing.Size(100, 20)
        Me.PhoneTextBox.TabIndex = 6
        '
        'FaxTextBox
        '
        Me.FaxTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsMainResourceExtra, "tblResourceExtra.Fax", True))
        Me.FaxTextBox.Location = New System.Drawing.Point(133, 53)
        Me.FaxTextBox.Name = "FaxTextBox"
        Me.FaxTextBox.Size = New System.Drawing.Size(100, 20)
        Me.FaxTextBox.TabIndex = 7
        '
        'EMailTextBox
        '
        Me.EMailTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsMainResourceExtra, "tblResourceExtra.EMail", True))
        Me.EMailTextBox.Location = New System.Drawing.Point(133, 79)
        Me.EMailTextBox.Name = "EMailTextBox"
        Me.EMailTextBox.Size = New System.Drawing.Size(224, 20)
        Me.EMailTextBox.TabIndex = 8
        '
        'JobTitleTextBox
        '
        Me.JobTitleTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsMainResourceExtra, "tblResourceExtra.JobTitle", True))
        Me.JobTitleTextBox.Location = New System.Drawing.Point(133, 105)
        Me.JobTitleTextBox.Name = "JobTitleTextBox"
        Me.JobTitleTextBox.Size = New System.Drawing.Size(224, 20)
        Me.JobTitleTextBox.TabIndex = 9
        '
        'pnlAuthor
        '
        Me.pnlAuthor.BackColor = System.Drawing.SystemColors.Control
        Me.pnlAuthor.Controls.Add(Me.cboAuthor)
        Me.pnlAuthor.Controls.Add(Label7)
        Me.pnlAuthor.Location = New System.Drawing.Point(15, 13)
        Me.pnlAuthor.Name = "pnlAuthor"
        Me.pnlAuthor.Size = New System.Drawing.Size(212, 31)
        Me.pnlAuthor.TabIndex = 0
        '
        'lblPerson
        '
        Me.lblPerson.AutoSize = True
        Me.lblPerson.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPerson.Location = New System.Drawing.Point(52, 23)
        Me.lblPerson.Name = "lblPerson"
        Me.lblPerson.Size = New System.Drawing.Size(389, 13)
        Me.lblPerson.TabIndex = 422
        Me.lblPerson.Text = "This resource is a Person.  Fill in these fields to set resource name."
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.pgName)
        Me.TabControl1.Controls.Add(Me.pgEvent)
        Me.TabControl1.Location = New System.Drawing.Point(12, 84)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(416, 396)
        Me.TabControl1.TabIndex = 0
        Me.TabControl1.TabStop = False
        '
        'pgName
        '
        Me.pgName.Controls.Add(Me.pnlName)
        Me.pgName.Controls.Add(Me.pnlAuthor)
        Me.pgName.Controls.Add(Me.pnlPhone)
        Me.pgName.Location = New System.Drawing.Point(4, 22)
        Me.pgName.Name = "pgName"
        Me.pgName.Padding = New System.Windows.Forms.Padding(3)
        Me.pgName.Size = New System.Drawing.Size(408, 370)
        Me.pgName.TabIndex = 0
        Me.pgName.Text = "Name"
        Me.pgName.UseVisualStyleBackColor = True
        '
        'pgEvent
        '
        Me.pgEvent.Controls.Add(Me.ComboBox1)
        Me.pgEvent.Controls.Add(Me.LocationTextBox)
        Me.pgEvent.Controls.Add(Me.Phone2TextBox)
        Me.pgEvent.Controls.Add(LocationLabel)
        Me.pgEvent.Controls.Add(EventDateLabel)
        Me.pgEvent.Controls.Add(EventAnnualLabel)
        Me.pgEvent.Controls.Add(Phone2Label)
        Me.pgEvent.Controls.Add(Me.EventDateTextBox)
        Me.pgEvent.Location = New System.Drawing.Point(4, 22)
        Me.pgEvent.Name = "pgEvent"
        Me.pgEvent.Padding = New System.Windows.Forms.Padding(3)
        Me.pgEvent.Size = New System.Drawing.Size(408, 370)
        Me.pgEvent.TabIndex = 1
        Me.pgEvent.Text = "Event Details"
        Me.pgEvent.UseVisualStyleBackColor = True
        '
        'ComboBox1
        '
        Me.ComboBox1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.ComboBox1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.ComboBox1.DataBindings.Add(New System.Windows.Forms.Binding("SelectedItem", Me.DsMainResourceExtra, "tblResourceExtra.EventAnnual", True))
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Items.AddRange(New Object() {"Y", "N"})
        Me.ComboBox1.Location = New System.Drawing.Point(127, 62)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.RestrictContentToListItems = True
        Me.ComboBox1.Size = New System.Drawing.Size(81, 21)
        Me.ComboBox1.TabIndex = 33
        '
        'LocationTextBox
        '
        Me.LocationTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsMainResourceExtra, "tblResourceExtra.Location", True))
        Me.LocationTextBox.Location = New System.Drawing.Point(125, 91)
        Me.LocationTextBox.Multiline = True
        Me.LocationTextBox.Name = "LocationTextBox"
        Me.LocationTextBox.Size = New System.Drawing.Size(222, 88)
        Me.LocationTextBox.TabIndex = 35
        '
        'Phone2TextBox
        '
        Me.Phone2TextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsMainResourceExtra, "tblResourceExtra.Phone2", True))
        Me.Phone2TextBox.Location = New System.Drawing.Point(127, 196)
        Me.Phone2TextBox.Name = "Phone2TextBox"
        Me.Phone2TextBox.Size = New System.Drawing.Size(100, 20)
        Me.Phone2TextBox.TabIndex = 37
        '
        'EventDateTextBox
        '
        Me.EventDateTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsMainResourceExtra, "tblResourceExtra.EventDate", True))
        Me.EventDateTextBox.Location = New System.Drawing.Point(125, 32)
        Me.EventDateTextBox.Name = "EventDateTextBox"
        Me.EventDateTextBox.Size = New System.Drawing.Size(222, 20)
        Me.EventDateTextBox.TabIndex = 30
        '
        'frmMainResourceAuthor
        '
        Me.AcceptButton = Me.btnSaveExit
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(712, 492)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.lblPerson)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(ExtraIDLabel)
        Me.Controls.Add(Me.ExtraIDTextBox)
        Me.Controls.Add(ResourceNumLabel)
        Me.Controls.Add(Me.txtResourceNum)
        Me.Controls.Add(NotesLabel)
        Me.Controls.Add(Me.txtNotes)
        Me.Menu = Me.MainMenu1
        Me.Name = "frmMainResourceAuthor"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Resource Extra"
        CType(Me.DsMainResourceExtra, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel3.ResumeLayout(False)
        Me.pnlName.ResumeLayout(False)
        Me.pnlName.PerformLayout()
        Me.pnlPhone.ResumeLayout(False)
        Me.pnlPhone.PerformLayout()
        Me.pnlAuthor.ResumeLayout(False)
        Me.pnlAuthor.PerformLayout()
        Me.TabControl1.ResumeLayout(False)
        Me.pgName.ResumeLayout(False)
        Me.pgEvent.ResumeLayout(False)
        Me.pgEvent.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents DsMainResourceExtra As InfoCtr.dsMainResourceExtra
    Friend WithEvents ExtraIDTextBox As System.Windows.Forms.TextBox
    Friend WithEvents txtResourceNum As System.Windows.Forms.TextBox
    Friend WithEvents txtNotes As System.Windows.Forms.TextBox
    Friend WithEvents cboAuthor As InfoCtr.ComboBoxRelaxed
    Friend WithEvents daspMainResourceExtra As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents SqlInsertCommand As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlSelectCommand As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlUpdateCommand As System.Data.SqlClient.SqlCommand
    Friend WithEvents MainMenu1 As System.Windows.Forms.MainMenu
    Friend WithEvents MenuItem1 As System.Windows.Forms.MenuItem
    Friend WithEvents miClose As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem2 As System.Windows.Forms.MenuItem
    Friend WithEvents miSave As System.Windows.Forms.MenuItem
    Friend WithEvents miCancel As System.Windows.Forms.MenuItem
    Friend WithEvents miDelete As System.Windows.Forms.MenuItem
    Friend WithEvents miHelp As System.Windows.Forms.MenuItem
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents btnSaveExit As System.Windows.Forms.Button
    Friend WithEvents pnlName As System.Windows.Forms.Panel
    Friend WithEvents LastNameTextBox As System.Windows.Forms.TextBox
    Friend WithEvents SuffixTextBox As System.Windows.Forms.TextBox
    Friend WithEvents MiddleTextBox As System.Windows.Forms.TextBox
    Friend WithEvents PrefixTextBox As System.Windows.Forms.TextBox
    Friend WithEvents FirstNameTextBox As System.Windows.Forms.TextBox
    Friend WithEvents pnlPhone As System.Windows.Forms.Panel
    Friend WithEvents PhoneTextBox As System.Windows.Forms.TextBox
    Friend WithEvents FaxTextBox As System.Windows.Forms.TextBox
    Friend WithEvents EMailTextBox As System.Windows.Forms.TextBox
    Friend WithEvents JobTitleTextBox As System.Windows.Forms.TextBox
    Friend WithEvents pnlAuthor As System.Windows.Forms.Panel
    Friend WithEvents lblPerson As System.Windows.Forms.Label
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents pgName As System.Windows.Forms.TabPage
    Friend WithEvents pgEvent As System.Windows.Forms.TabPage
    Friend WithEvents ComboBox1 As InfoCtr.ComboBoxRelaxed
    Friend WithEvents LocationTextBox As System.Windows.Forms.TextBox
    Friend WithEvents Phone2TextBox As System.Windows.Forms.TextBox
    Friend WithEvents EventDateTextBox As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents SqlCommand1 As System.Data.SqlClient.SqlCommand

End Class
