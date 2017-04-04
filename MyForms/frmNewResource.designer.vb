<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmNewResource
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmNewResource))
        Me.btnHelp = New System.Windows.Forms.Button()
        Me.btnSaveExit = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.editResourceName = New System.Windows.Forms.TextBox()
        Me.BindingSource1 = New System.Windows.Forms.BindingSource(Me.components)
        Me.DsNewResource1 = New InfoCtr.dsNewResource()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtDescription = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.editURL = New System.Windows.Forms.TextBox()
        Me.lblLink = New System.Windows.Forms.Label()
        Me.lblGotoPublisher = New System.Windows.Forms.Label()
        Me.Label36 = New System.Windows.Forms.Label()
        Me.editPubDate = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.editPhone800 = New System.Windows.Forms.TextBox()
        Me.editPhone = New System.Windows.Forms.TextBox()
        Me.editLast = New System.Windows.Forms.TextBox()
        Me.editStreet = New System.Windows.Forms.TextBox()
        Me.editCity = New System.Windows.Forms.TextBox()
        Me.editState = New System.Windows.Forms.TextBox()
        Me.editZip = New System.Windows.Forms.TextBox()
        Me.editCountry = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cboKey1 = New InfoCtr.ComboBoxRelaxed()
        Me.fldID = New System.Windows.Forms.Label()
        Me.pnlAddress = New System.Windows.Forms.Panel()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.pnlPublisher = New System.Windows.Forms.Panel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cboAuthor = New InfoCtr.ComboBoxRelaxed()
        Me.editPublisher = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.editFirst = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.DsType1 = New InfoCtr.dsType()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.editIndexTerm = New System.Windows.Forms.TextBox()
        Me.TblNewResourceTableAdapter = New InfoCtr.dsNewResourceTableAdapters.tblNewResourceTableAdapter()
        Me.cboSubtype = New InfoCtr.ComboBoxRelaxed()
        Me.cboType = New InfoCtr.ComboBoxRelaxed()
        Me.ErrorProvider1 = New System.Windows.Forms.ErrorProvider(Me.components)
        CType(Me.BindingSource1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsNewResource1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlAddress.SuspendLayout()
        Me.pnlPublisher.SuspendLayout()
        CType(Me.DsType1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnHelp
        '
        Me.btnHelp.Image = CType(resources.GetObject("btnHelp.Image"), System.Drawing.Image)
        Me.btnHelp.Location = New System.Drawing.Point(584, 31)
        Me.btnHelp.Name = "btnHelp"
        Me.btnHelp.Size = New System.Drawing.Size(25, 25)
        Me.btnHelp.TabIndex = 432
        Me.btnHelp.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnHelp.UseVisualStyleBackColor = True
        Me.btnHelp.Visible = False
        '
        'btnSaveExit
        '
        Me.btnSaveExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSaveExit.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.btnSaveExit.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSaveExit.Image = CType(resources.GetObject("btnSaveExit.Image"), System.Drawing.Image)
        Me.btnSaveExit.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSaveExit.Location = New System.Drawing.Point(628, 9)
        Me.btnSaveExit.Name = "btnSaveExit"
        Me.btnSaveExit.Size = New System.Drawing.Size(40, 35)
        Me.btnSaveExit.TabIndex = 430
        Me.btnSaveExit.Text = "Close"
        Me.btnSaveExit.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSaveExit.UseVisualStyleBackColor = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(87, 185)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(58, 13)
        Me.Label5.TabIndex = 427
        Me.Label5.Text = "CRG Issue"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(48, 213)
        Me.Label3.MaximumSize = New System.Drawing.Size(100, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(96, 13)
        Me.Label3.TabIndex = 425
        Me.Label3.Text = "Private Description"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(60, 81)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(84, 13)
        Me.Label1.TabIndex = 423
        Me.Label1.Text = "Resource Name"
        '
        'editResourceName
        '
        Me.editResourceName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.editResourceName.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.BindingSource1, "ResourceName", True))
        Me.editResourceName.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.editResourceName.ForeColor = System.Drawing.SystemColors.ControlText
        Me.editResourceName.Location = New System.Drawing.Point(156, 65)
        Me.editResourceName.MaxLength = 200
        Me.editResourceName.Multiline = True
        Me.editResourceName.Name = "editResourceName"
        Me.editResourceName.Size = New System.Drawing.Size(357, 44)
        Me.editResourceName.TabIndex = 0
        '
        'BindingSource1
        '
        Me.BindingSource1.DataMember = "tblNewResource"
        Me.BindingSource1.DataSource = Me.DsNewResource1
        '
        'DsNewResource1
        '
        Me.DsNewResource1.DataSetName = "dsNewResource"
        Me.DsNewResource1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.Maroon
        Me.Label7.Location = New System.Drawing.Point(12, 395)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(205, 19)
        Me.Label7.TabIndex = 434
        Me.Label7.Text = "At Least 1 means of contact"
        '
        'txtDescription
        '
        Me.txtDescription.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.BindingSource1, "PrivateDescription", True))
        Me.txtDescription.Location = New System.Drawing.Point(151, 213)
        Me.txtDescription.Multiline = True
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtDescription.Size = New System.Drawing.Size(354, 63)
        Me.txtDescription.TabIndex = 25
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(235, 10)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(57, 13)
        Me.Label11.TabIndex = 446
        Me.Label11.Text = "Area Code"
        '
        'Label21
        '
        Me.Label21.Location = New System.Drawing.Point(110, 127)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(34, 15)
        Me.Label21.TabIndex = 450
        Me.Label21.Text = "Type"
        Me.Label21.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label20
        '
        Me.Label20.Location = New System.Drawing.Point(99, 154)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(49, 18)
        Me.Label20.TabIndex = 449
        Me.Label20.Text = "Subtype"
        Me.Label20.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'editURL
        '
        Me.editURL.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.editURL.BackColor = System.Drawing.SystemColors.Window
        Me.editURL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.editURL.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.BindingSource1, "Website", True))
        Me.editURL.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.editURL.ForeColor = System.Drawing.SystemColors.Highlight
        Me.editURL.Location = New System.Drawing.Point(151, 417)
        Me.editURL.MaxLength = 255
        Me.editURL.Multiline = True
        Me.editURL.Name = "editURL"
        Me.editURL.Size = New System.Drawing.Size(419, 39)
        Me.editURL.TabIndex = 55
        Me.ToolTip1.SetToolTip(Me.editURL, "For a purchasable resource this url needs to link directly to the resource purcha" & _
        "se page.")
        '
        'lblLink
        '
        Me.lblLink.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLink.Location = New System.Drawing.Point(40, 417)
        Me.lblLink.Margin = New System.Windows.Forms.Padding(0)
        Me.lblLink.Name = "lblLink"
        Me.lblLink.Size = New System.Drawing.Size(108, 39)
        Me.lblLink.TabIndex = 454
        Me.lblLink.Text = "Website / " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Resource Link:"
        Me.lblLink.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblGotoPublisher
        '
        Me.lblGotoPublisher.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblGotoPublisher.ForeColor = System.Drawing.Color.DarkGreen
        Me.lblGotoPublisher.Location = New System.Drawing.Point(31, 27)
        Me.lblGotoPublisher.Name = "lblGotoPublisher"
        Me.lblGotoPublisher.Size = New System.Drawing.Size(102, 32)
        Me.lblGotoPublisher.TabIndex = 455
        Me.lblGotoPublisher.Text = "Publisher Name"
        Me.lblGotoPublisher.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label36
        '
        Me.Label36.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label36.Location = New System.Drawing.Point(55, 60)
        Me.Label36.Margin = New System.Windows.Forms.Padding(0)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(88, 29)
        Me.Label36.TabIndex = 457
        Me.Label36.Text = "Date Published"
        Me.Label36.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'editPubDate
        '
        Me.editPubDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.editPubDate.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.BindingSource1, "DatePublished", True))
        Me.editPubDate.Location = New System.Drawing.Point(151, 66)
        Me.editPubDate.MaxLength = 50
        Me.editPubDate.Name = "editPubDate"
        Me.editPubDate.Size = New System.Drawing.Size(139, 20)
        Me.editPubDate.TabIndex = 35
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.Color.Maroon
        Me.Label12.Location = New System.Drawing.Point(2, 6)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(198, 19)
        Me.Label12.TabIndex = 459
        Me.Label12.Text = "Published Resource Details"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.Color.Maroon
        Me.Label13.Location = New System.Drawing.Point(10, 13)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(592, 15)
        Me.Label13.TabIndex = 460
        Me.Label13.Text = "Fill in all these fields, then close, and edit Resource Detail window with additi" & _
    "onal information you may have."
        '
        'editPhone800
        '
        Me.editPhone800.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.editPhone800.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.BindingSource1, "Telephone", True))
        Me.editPhone800.Location = New System.Drawing.Point(133, 9)
        Me.editPhone800.MaxLength = 50
        Me.editPhone800.Name = "editPhone800"
        Me.editPhone800.Size = New System.Drawing.Size(85, 20)
        Me.editPhone800.TabIndex = 60
        Me.editPhone800.Text = "(800) 999-9999"
        '
        'editPhone
        '
        Me.editPhone.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.editPhone.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.BindingSource1, "Telephone2", True))
        Me.editPhone.Location = New System.Drawing.Point(298, 9)
        Me.editPhone.MaxLength = 50
        Me.editPhone.Name = "editPhone"
        Me.editPhone.Size = New System.Drawing.Size(85, 20)
        Me.editPhone.TabIndex = 65
        Me.editPhone.Text = "(    ) "
        '
        'editLast
        '
        Me.editLast.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.editLast.Location = New System.Drawing.Point(557, 66)
        Me.editLast.MaxLength = 50
        Me.editLast.Name = "editLast"
        Me.editLast.Size = New System.Drawing.Size(95, 20)
        Me.editLast.TabIndex = 50
        '
        'editStreet
        '
        Me.editStreet.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.editStreet.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.BindingSource1, "Address1", True))
        Me.editStreet.Location = New System.Drawing.Point(75, 45)
        Me.editStreet.MaxLength = 50
        Me.editStreet.Name = "editStreet"
        Me.editStreet.Size = New System.Drawing.Size(198, 20)
        Me.editStreet.TabIndex = 70
        Me.editStreet.Text = "Street"
        Me.ToolTip1.SetToolTip(Me.editStreet, "Street")
        '
        'editCity
        '
        Me.editCity.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.editCity.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.BindingSource1, "City", True))
        Me.editCity.Location = New System.Drawing.Point(279, 45)
        Me.editCity.MaxLength = 50
        Me.editCity.Name = "editCity"
        Me.editCity.Size = New System.Drawing.Size(104, 20)
        Me.editCity.TabIndex = 75
        Me.editCity.Text = "City"
        Me.ToolTip1.SetToolTip(Me.editCity, "City")
        '
        'editState
        '
        Me.editState.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.editState.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.BindingSource1, "State", True))
        Me.editState.Location = New System.Drawing.Point(392, 46)
        Me.editState.MaxLength = 50
        Me.editState.Name = "editState"
        Me.editState.Size = New System.Drawing.Size(76, 20)
        Me.editState.TabIndex = 80
        Me.editState.Text = "State"
        Me.ToolTip1.SetToolTip(Me.editState, "State")
        '
        'editZip
        '
        Me.editZip.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.editZip.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.BindingSource1, "Zip", True))
        Me.editZip.Location = New System.Drawing.Point(474, 46)
        Me.editZip.MaxLength = 50
        Me.editZip.Name = "editZip"
        Me.editZip.Size = New System.Drawing.Size(90, 20)
        Me.editZip.TabIndex = 85
        Me.editZip.Text = "Zip"
        Me.ToolTip1.SetToolTip(Me.editZip, "Zip")
        '
        'editCountry
        '
        Me.editCountry.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.editCountry.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.BindingSource1, "Country", True))
        Me.editCountry.Location = New System.Drawing.Point(570, 46)
        Me.editCountry.MaxLength = 50
        Me.editCountry.Name = "editCountry"
        Me.editCountry.Size = New System.Drawing.Size(69, 20)
        Me.editCountry.TabIndex = 90
        Me.editCountry.Text = "Country"
        Me.ToolTip1.SetToolTip(Me.editCountry, "Country")
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(28, 10)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(41, 13)
        Me.Label9.TabIndex = 471
        Me.Label9.Text = "Phone:"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(514, 216)
        Me.Label10.MaximumSize = New System.Drawing.Size(100, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(95, 39)
        Me.Label10.TabIndex = 472
        Me.Label10.Text = "How this resource is useful to congregations."
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ForeColor = System.Drawing.Color.Maroon
        Me.Label14.Location = New System.Drawing.Point(12, 43)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(175, 19)
        Me.Label14.TabIndex = 473
        Me.Label14.Text = "Resource Name && Type"
        '
        'cboKey1
        '
        Me.cboKey1.AllowDrop = True
        Me.cboKey1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboKey1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboKey1.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.BindingSource1, "Keyword1", True))
        Me.cboKey1.DisplayMember = "CRGID"
        Me.cboKey1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboKey1.DropDownWidth = 500
        Me.cboKey1.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboKey1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cboKey1.Location = New System.Drawing.Point(151, 182)
        Me.cboKey1.Name = "cboKey1"
        Me.cboKey1.RestrictContentToListItems = True
        Me.cboKey1.Size = New System.Drawing.Size(350, 20)
        Me.cboKey1.TabIndex = 20
        Me.cboKey1.Tag = "FIRST KEYWORD"
        Me.ToolTip1.SetToolTip(Me.cboKey1, "Right click for searching.")
        '
        'fldID
        '
        Me.fldID.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.BindingSource1, "ICCResourceID", True))
        Me.fldID.Enabled = False
        Me.fldID.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fldID.Location = New System.Drawing.Point(585, 65)
        Me.fldID.Margin = New System.Windows.Forms.Padding(0)
        Me.fldID.Name = "fldID"
        Me.fldID.Size = New System.Drawing.Size(76, 29)
        Me.fldID.TabIndex = 474
        Me.fldID.Text = "ResourceID"
        Me.fldID.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pnlAddress
        '
        Me.pnlAddress.Controls.Add(Me.Label17)
        Me.pnlAddress.Controls.Add(Me.Label9)
        Me.pnlAddress.Controls.Add(Me.editCountry)
        Me.pnlAddress.Controls.Add(Me.editZip)
        Me.pnlAddress.Controls.Add(Me.editState)
        Me.pnlAddress.Controls.Add(Me.editCity)
        Me.pnlAddress.Controls.Add(Me.editStreet)
        Me.pnlAddress.Controls.Add(Me.editPhone)
        Me.pnlAddress.Controls.Add(Me.editPhone800)
        Me.pnlAddress.Controls.Add(Me.Label11)
        Me.pnlAddress.Controls.Add(Me.Label4)
        Me.pnlAddress.Location = New System.Drawing.Point(18, 465)
        Me.pnlAddress.Name = "pnlAddress"
        Me.pnlAddress.Size = New System.Drawing.Size(650, 84)
        Me.pnlAddress.TabIndex = 56
        Me.pnlAddress.Visible = False
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(21, 48)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(48, 13)
        Me.Label17.TabIndex = 472
        Me.Label17.Text = "Address:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(82, 12)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(45, 13)
        Me.Label4.TabIndex = 426
        Me.Label4.Text = "Toll free"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'pnlPublisher
        '
        Me.pnlPublisher.Controls.Add(Me.Label2)
        Me.pnlPublisher.Controls.Add(Me.cboAuthor)
        Me.pnlPublisher.Controls.Add(Me.editPublisher)
        Me.pnlPublisher.Controls.Add(Me.Label16)
        Me.pnlPublisher.Controls.Add(Me.Label8)
        Me.pnlPublisher.Controls.Add(Me.editFirst)
        Me.pnlPublisher.Controls.Add(Me.editLast)
        Me.pnlPublisher.Controls.Add(Me.Label12)
        Me.pnlPublisher.Controls.Add(Me.Label36)
        Me.pnlPublisher.Controls.Add(Me.editPubDate)
        Me.pnlPublisher.Controls.Add(Me.lblGotoPublisher)
        Me.pnlPublisher.Location = New System.Drawing.Point(5, 284)
        Me.pnlPublisher.Name = "pnlPublisher"
        Me.pnlPublisher.Size = New System.Drawing.Size(663, 97)
        Me.pnlPublisher.TabIndex = 26
        Me.pnlPublisher.Visible = False
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(350, 22)
        Me.Label2.Margin = New System.Windows.Forms.Padding(0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(112, 29)
        Me.Label2.TabIndex = 480
        Me.Label2.Text = "First Author/Editor"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cboAuthor
        '
        Me.cboAuthor.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboAuthor.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboAuthor.FormattingEnabled = True
        Me.cboAuthor.Items.AddRange(New Object() {"Author", "Editor", "Presenter"})
        Me.cboAuthor.Location = New System.Drawing.Point(478, 30)
        Me.cboAuthor.Name = "cboAuthor"
        Me.cboAuthor.RestrictContentToListItems = True
        Me.cboAuthor.Size = New System.Drawing.Size(121, 21)
        Me.cboAuthor.TabIndex = 40
        Me.cboAuthor.Text = "Author"
        '
        'editPublisher
        '
        Me.editPublisher.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.editPublisher.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.BindingSource1, "Publisher", True))
        Me.editPublisher.Location = New System.Drawing.Point(151, 36)
        Me.editPublisher.MaxLength = 50
        Me.editPublisher.Name = "editPublisher"
        Me.editPublisher.Size = New System.Drawing.Size(139, 20)
        Me.editPublisher.TabIndex = 30
        '
        'Label16
        '
        Me.Label16.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(500, 60)
        Me.Label16.Margin = New System.Windows.Forms.Padding(0)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(56, 29)
        Me.Label16.TabIndex = 467
        Me.Label16.Text = "Last name"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label8
        '
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(334, 58)
        Me.Label8.Margin = New System.Windows.Forms.Padding(0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(59, 29)
        Me.Label8.TabIndex = 466
        Me.Label8.Text = "First name"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'editFirst
        '
        Me.editFirst.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.editFirst.Location = New System.Drawing.Point(396, 64)
        Me.editFirst.MaxLength = 50
        Me.editFirst.Name = "editFirst"
        Me.editFirst.Size = New System.Drawing.Size(95, 20)
        Me.editFirst.TabIndex = 45
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(576, 417)
        Me.Label15.MaximumSize = New System.Drawing.Size(100, 0)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(81, 39)
        Me.Label15.TabIndex = 478
        Me.Label15.Text = "Direct link to resource, not a search page."
        '
        'DsType1
        '
        Me.DsType1.DataSetName = "dsType"
        Me.DsType1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(336, 121)
        Me.Label6.Margin = New System.Windows.Forms.Padding(0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(62, 35)
        Me.Label6.TabIndex = 482
        Me.Label6.Text = "One Tag / Index Term"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'editIndexTerm
        '
        Me.editIndexTerm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.editIndexTerm.Location = New System.Drawing.Point(401, 125)
        Me.editIndexTerm.MaxLength = 50
        Me.editIndexTerm.Name = "editIndexTerm"
        Me.editIndexTerm.Size = New System.Drawing.Size(256, 20)
        Me.editIndexTerm.TabIndex = 15
        '
        'TblNewResourceTableAdapter
        '
        Me.TblNewResourceTableAdapter.ClearBeforeFill = True
        '
        'cboSubtype
        '
        Me.cboSubtype.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboSubtype.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboSubtype.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.BindingSource1, "Subtype", True))
        Me.cboSubtype.DataSource = Me.DsType1
        Me.cboSubtype.DisplayMember = "tType.RelTypeSubtype.ResourceSubType"
        Me.cboSubtype.FormattingEnabled = True
        Me.cboSubtype.Location = New System.Drawing.Point(151, 151)
        Me.cboSubtype.Name = "cboSubtype"
        Me.cboSubtype.RestrictContentToListItems = True
        Me.cboSubtype.Size = New System.Drawing.Size(144, 21)
        Me.cboSubtype.TabIndex = 10
        Me.cboSubtype.ValueMember = "tType.RelTypeSubtype.ResourceSubType"
        '
        'cboType
        '
        Me.cboType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboType.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.BindingSource1, "ResourceType", True))
        Me.cboType.DataSource = Me.DsType1
        Me.cboType.DisplayMember = "tType.ResourceType"
        Me.cboType.FormattingEnabled = True
        Me.cboType.Location = New System.Drawing.Point(151, 124)
        Me.cboType.Name = "cboType"
        Me.cboType.RestrictContentToListItems = True
        Me.cboType.Size = New System.Drawing.Size(144, 21)
        Me.cboType.TabIndex = 5
        Me.cboType.ValueMember = "tType.ResourceType"
        '
        'ErrorProvider1
        '
        Me.ErrorProvider1.ContainerControl = Me
        '
        'frmNewResource
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(680, 586)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.editIndexTerm)
        Me.Controls.Add(Me.cboKey1)
        Me.Controls.Add(Me.cboSubtype)
        Me.Controls.Add(Me.cboType)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.pnlPublisher)
        Me.Controls.Add(Me.pnlAddress)
        Me.Controls.Add(Me.fldID)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.editURL)
        Me.Controls.Add(Me.lblLink)
        Me.Controls.Add(Me.Label21)
        Me.Controls.Add(Me.Label20)
        Me.Controls.Add(Me.txtDescription)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.btnHelp)
        Me.Controls.Add(Me.btnSaveExit)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.editResourceName)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmNewResource"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "NEW RESOURCE required information."
        CType(Me.BindingSource1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsNewResource1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlAddress.ResumeLayout(False)
        Me.pnlAddress.PerformLayout()
        Me.pnlPublisher.ResumeLayout(False)
        Me.pnlPublisher.PerformLayout()
        CType(Me.DsType1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub


    Friend WithEvents btnHelp As System.Windows.Forms.Button
    Friend WithEvents btnSaveExit As System.Windows.Forms.Button
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    ' Friend WithEvents cboCRG As InfoCtr.ComboBoxRelaxed
    Friend WithEvents editResourceName As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtDescription As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label


    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents editURL As System.Windows.Forms.TextBox
    Friend WithEvents lblLink As System.Windows.Forms.Label
    Friend WithEvents lblGotoPublisher As System.Windows.Forms.Label
    Friend WithEvents cboPublisher As InfoCtr.ComboBoxRelaxed
    Friend WithEvents Label36 As System.Windows.Forms.Label
    Friend WithEvents editPubDate As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents editPhone800 As System.Windows.Forms.TextBox
    Friend WithEvents editPhone As System.Windows.Forms.TextBox
    Friend WithEvents editLast As System.Windows.Forms.TextBox
    Friend WithEvents editStreet As System.Windows.Forms.TextBox
    Friend WithEvents editCity As System.Windows.Forms.TextBox
    Friend WithEvents editState As System.Windows.Forms.TextBox
    Friend WithEvents editZip As System.Windows.Forms.TextBox
    Friend WithEvents editCountry As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents fldID As System.Windows.Forms.Label
    Friend WithEvents pnlAddress As System.Windows.Forms.Panel
    Friend WithEvents pnlPublisher As System.Windows.Forms.Panel
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents editPublisher As System.Windows.Forms.TextBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents editFirst As System.Windows.Forms.TextBox
    Friend WithEvents cboAuthor As InfoCtr.ComboBoxRelaxed

    Friend WithEvents cboType As InfoCtr.ComboBoxRelaxed
    Friend WithEvents cboSubtype As InfoCtr.ComboBoxRelaxed
    Friend WithEvents cboKey1 As InfoCtr.ComboBoxRelaxed
    Friend WithEvents DsType1 As InfoCtr.dsType
    Friend WithEvents BindingSource1 As System.Windows.Forms.BindingSource
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents editIndexTerm As System.Windows.Forms.TextBox
    Friend WithEvents DsNewResource1 As InfoCtr.dsNewResource
    Friend WithEvents TblNewResourceTableAdapter As InfoCtr.dsNewResourceTableAdapters.tblNewResourceTableAdapter
    Friend WithEvents ErrorProvider1 As System.Windows.Forms.ErrorProvider
End Class
