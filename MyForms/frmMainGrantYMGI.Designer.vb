<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMainGrantYMGI
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMainGrantYMGI))
        Me.pnlApplication = New System.Windows.Forms.Panel()
        Me.fldCaseID = New System.Windows.Forms.Label()
        Me.MainYMGIBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.DsMainGrantYMGI = New InfoCtr.dsMainGrantYMGI()
        Me.cboCase = New InfoCtr.ComboBoxRelaxed()
        Me.lblGotoCase = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.cboPD = New InfoCtr.ComboBoxRelaxed()
        Me.txtNotes = New System.Windows.Forms.TextBox()
        Me.cboCongRep = New InfoCtr.ComboBoxRelaxed()
        Me.dtAppRecd = New InfoCtr.DateTextBox()
        Me.txtRemarks = New System.Windows.Forms.TextBox()
        Me.CheckBox8 = New System.Windows.Forms.CheckBox()
        Me.CheckBox9 = New System.Windows.Forms.CheckBox()
        Me.CheckBox4 = New System.Windows.Forms.CheckBox()
        Me.CheckBox5 = New System.Windows.Forms.CheckBox()
        Me.CheckBox6 = New System.Windows.Forms.CheckBox()
        Me.Label38 = New System.Windows.Forms.Label()
        Me.lblGotoCongRep = New System.Windows.Forms.Label()
        Me.txtTentative = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.lblGotoPD = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label39 = New System.Windows.Forms.Label()
        Me.dtAppReceiptMail = New InfoCtr.DateTextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.pnlProcess = New System.Windows.Forms.Panel()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.cboDetermination = New InfoCtr.ComboBoxRelaxed()
        Me.CheckBox7 = New System.Windows.Forms.CheckBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.dtAgreementRecd = New InfoCtr.DateTextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.dtAgreementMail = New InfoCtr.DateTextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.dtNotifyMail = New InfoCtr.DateTextBox()
        Me.Label41 = New System.Windows.Forms.Label()
        Me.cboCallNotified = New InfoCtr.ComboBoxRelaxed()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.dtDetermination = New InfoCtr.DateTextBox()
        Me.cboDeterminationStaff = New InfoCtr.ComboBoxRelaxed()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.dtNotifyCall = New InfoCtr.DateTextBox()
        Me.CheckBox3 = New System.Windows.Forms.CheckBox()
        Me.CheckBox2 = New System.Windows.Forms.CheckBox()
        Me.Label42 = New System.Windows.Forms.Label()
        Me.dtClassConfirm = New InfoCtr.DateTextBox()
        Me.CheckBox1 = New System.Windows.Forms.CheckBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.pnlGrant = New System.Windows.Forms.Panel()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.rbMarch = New System.Windows.Forms.RadioButton()
        Me.rbJan = New System.Windows.Forms.RadioButton()
        Me.dtGrantDeadline = New InfoCtr.DateTextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label43 = New System.Windows.Forms.Label()
        Me.dtGrantAppRecd = New InfoCtr.DateTextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label36 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.btnGrant = New System.Windows.Forms.Button()
        Me.btnSaveExit = New System.Windows.Forms.Button()
        Me.btnHelp = New System.Windows.Forms.Button()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.fldGINum = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.fldOrgID = New System.Windows.Forms.Label()
        Me.StatusBar1 = New System.Windows.Forms.StatusBar()
        Me.StatusBarPanel1 = New System.Windows.Forms.StatusBarPanel()
        Me.StatusBarPanelID = New System.Windows.Forms.StatusBarPanel()
        Me.StatusBarPanel2 = New System.Windows.Forms.StatusBarPanel()
        Me.MainMenu1 = New System.Windows.Forms.MainMenu(Me.components)
        Me.MenuItem1 = New System.Windows.Forms.MenuItem()
        Me.miClose = New System.Windows.Forms.MenuItem()
        Me.MenuItem2 = New System.Windows.Forms.MenuItem()
        Me.miSave = New System.Windows.Forms.MenuItem()
        Me.miCancel = New System.Windows.Forms.MenuItem()
        Me.miDelete = New System.Windows.Forms.MenuItem()
        Me.MenuItem3 = New System.Windows.Forms.MenuItem()
        Me.MenuItem4 = New System.Windows.Forms.MenuItem()
        Me.miGotoGrant = New System.Windows.Forms.MenuItem()
        Me.fldGotoOrg = New System.Windows.Forms.TextBox()
        Me.tblGrantYMGITableAdapter = New InfoCtr.dsMainGrantYMGITableAdapters.tblGrantYMGITableAdapter()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.Label17 = New System.Windows.Forms.Label()
        Me.pnlApplication.SuspendLayout()
        CType(Me.MainYMGIBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsMainGrantYMGI, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlProcess.SuspendLayout()
        Me.pnlGrant.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.StatusBarPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.StatusBarPanelID, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.StatusBarPanel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlApplication
        '
        Me.pnlApplication.BackColor = System.Drawing.Color.MintCream
        Me.pnlApplication.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pnlApplication.Controls.Add(Me.fldCaseID)
        Me.pnlApplication.Controls.Add(Me.cboCase)
        Me.pnlApplication.Controls.Add(Me.lblGotoCase)
        Me.pnlApplication.Controls.Add(Me.Label18)
        Me.pnlApplication.Controls.Add(Me.cboPD)
        Me.pnlApplication.Controls.Add(Me.txtNotes)
        Me.pnlApplication.Controls.Add(Me.cboCongRep)
        Me.pnlApplication.Controls.Add(Me.dtAppRecd)
        Me.pnlApplication.Controls.Add(Me.txtRemarks)
        Me.pnlApplication.Controls.Add(Me.CheckBox8)
        Me.pnlApplication.Controls.Add(Me.CheckBox9)
        Me.pnlApplication.Controls.Add(Me.CheckBox4)
        Me.pnlApplication.Controls.Add(Me.CheckBox5)
        Me.pnlApplication.Controls.Add(Me.CheckBox6)
        Me.pnlApplication.Controls.Add(Me.Label38)
        Me.pnlApplication.Controls.Add(Me.lblGotoCongRep)
        Me.pnlApplication.Controls.Add(Me.txtTentative)
        Me.pnlApplication.Controls.Add(Me.Label16)
        Me.pnlApplication.Controls.Add(Me.lblGotoPD)
        Me.pnlApplication.Controls.Add(Me.Label19)
        Me.pnlApplication.Controls.Add(Me.Label39)
        Me.pnlApplication.Controls.Add(Me.dtAppReceiptMail)
        Me.pnlApplication.Location = New System.Drawing.Point(51, 92)
        Me.pnlApplication.Name = "pnlApplication"
        Me.pnlApplication.Size = New System.Drawing.Size(366, 441)
        Me.pnlApplication.TabIndex = 254
        Me.pnlApplication.TabStop = True
        '
        'fldCaseID
        '
        Me.fldCaseID.AutoSize = True
        Me.fldCaseID.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainYMGIBindingSource, "CaseNum", True))
        Me.fldCaseID.Enabled = False
        Me.fldCaseID.Location = New System.Drawing.Point(316, 225)
        Me.fldCaseID.Name = "fldCaseID"
        Me.fldCaseID.Size = New System.Drawing.Size(41, 13)
        Me.fldCaseID.TabIndex = 265
        Me.fldCaseID.Text = "caseID"
        '
        'MainYMGIBindingSource
        '
        Me.MainYMGIBindingSource.DataMember = "tblGrantYMGI"
        Me.MainYMGIBindingSource.DataSource = Me.DsMainGrantYMGI
        '
        'DsMainGrantYMGI
        '
        Me.DsMainGrantYMGI.DataSetName = "dsMainGrantYMGI"
        Me.DsMainGrantYMGI.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'cboCase
        '
        Me.cboCase.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboCase.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboCase.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.MainYMGIBindingSource, "CaseNum", True))
        Me.cboCase.DisplayMember = "CaseName"
        Me.cboCase.Location = New System.Drawing.Point(90, 221)
        Me.cboCase.Name = "cboCase"
        Me.cboCase.RestrictContentToListItems = True
        Me.cboCase.Size = New System.Drawing.Size(224, 21)
        Me.cboCase.TabIndex = 9
        Me.cboCase.ValueMember = "CaseID"
        '
        'lblGotoCase
        '
        Me.lblGotoCase.BackColor = System.Drawing.Color.Transparent
        Me.lblGotoCase.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblGotoCase.ForeColor = System.Drawing.Color.ForestGreen
        Me.lblGotoCase.Location = New System.Drawing.Point(10, 222)
        Me.lblGotoCase.Name = "lblGotoCase"
        Me.lblGotoCase.Size = New System.Drawing.Size(72, 20)
        Me.lblGotoCase.TabIndex = 264
        Me.lblGotoCase.Tag = "Case"
        Me.lblGotoCase.Text = "Case Name"
        '
        'Label18
        '
        Me.Label18.BackColor = System.Drawing.Color.Transparent
        Me.Label18.Location = New System.Drawing.Point(8, 351)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(37, 16)
        Me.Label18.TabIndex = 262
        Me.Label18.Text = "Notes"
        '
        'cboPD
        '
        Me.cboPD.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboPD.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboPD.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.MainYMGIBindingSource, "ProjectDirectorNum", True))
        Me.cboPD.FormattingEnabled = True
        Me.cboPD.Location = New System.Drawing.Point(90, 167)
        Me.cboPD.Name = "cboPD"
        Me.cboPD.RestrictContentToListItems = True
        Me.cboPD.Size = New System.Drawing.Size(222, 21)
        Me.cboPD.TabIndex = 7
        '
        'txtNotes
        '
        Me.txtNotes.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainYMGIBindingSource, "Notes", True))
        Me.txtNotes.Location = New System.Drawing.Point(51, 348)
        Me.txtNotes.Multiline = True
        Me.txtNotes.Name = "txtNotes"
        Me.txtNotes.Size = New System.Drawing.Size(308, 80)
        Me.txtNotes.TabIndex = 12
        Me.txtNotes.Text = "Notes"
        '
        'cboCongRep
        '
        Me.cboCongRep.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboCongRep.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboCongRep.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.MainYMGIBindingSource, "CongRepNum", True))
        Me.cboCongRep.FormattingEnabled = True
        Me.cboCongRep.Location = New System.Drawing.Point(90, 194)
        Me.cboCongRep.Name = "cboCongRep"
        Me.cboCongRep.RestrictContentToListItems = True
        Me.cboCongRep.Size = New System.Drawing.Size(222, 21)
        Me.cboCongRep.TabIndex = 8
        '
        'dtAppRecd
        '
        Me.dtAppRecd.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainYMGIBindingSource, "AppRecdDt", True))
        Me.dtAppRecd.Location = New System.Drawing.Point(172, 11)
        Me.dtAppRecd.Name = "dtAppRecd"
        Me.dtAppRecd.Size = New System.Drawing.Size(70, 20)
        Me.dtAppRecd.TabIndex = 0
        '
        'txtRemarks
        '
        Me.txtRemarks.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainYMGIBindingSource, "InclRemarks", True))
        Me.txtRemarks.Location = New System.Drawing.Point(7, 63)
        Me.txtRemarks.Multiline = True
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.Size = New System.Drawing.Size(142, 86)
        Me.txtRemarks.TabIndex = 1
        Me.txtRemarks.Text = "Enclosure Remarks"
        '
        'CheckBox8
        '
        Me.CheckBox8.DataBindings.Add(New System.Windows.Forms.Binding("Checked", Me.MainYMGIBindingSource, "InclPostmark", True))
        Me.CheckBox8.Location = New System.Drawing.Point(159, 135)
        Me.CheckBox8.Name = "CheckBox8"
        Me.CheckBox8.Size = New System.Drawing.Size(173, 19)
        Me.CheckBox8.TabIndex = 6
        Me.CheckBox8.Text = "Postmarked by 5/1/2015"
        '
        'CheckBox9
        '
        Me.CheckBox9.DataBindings.Add(New System.Windows.Forms.Binding("Checked", Me.MainYMGIBindingSource, "InclCongProfile", True))
        Me.CheckBox9.Location = New System.Drawing.Point(159, 113)
        Me.CheckBox9.Name = "CheckBox9"
        Me.CheckBox9.Size = New System.Drawing.Size(200, 24)
        Me.CheckBox9.TabIndex = 5
        Me.CheckBox9.Text = "Application Complete (Congr. Profile)"
        '
        'CheckBox4
        '
        Me.CheckBox4.DataBindings.Add(New System.Windows.Forms.Binding("Checked", Me.MainYMGIBindingSource, "InclAbility501c3", True))
        Me.CheckBox4.Location = New System.Drawing.Point(159, 98)
        Me.CheckBox4.Name = "CheckBox4"
        Me.CheckBox4.Size = New System.Drawing.Size(195, 16)
        Me.CheckBox4.TabIndex = 4
        Me.CheckBox4.Text = "Ability to Provide 501c3 included"
        '
        'CheckBox5
        '
        Me.CheckBox5.DataBindings.Add(New System.Windows.Forms.Binding("Checked", Me.MainYMGIBindingSource, "InclSignatures", True))
        Me.CheckBox5.Location = New System.Drawing.Point(159, 78)
        Me.CheckBox5.Name = "CheckBox5"
        Me.CheckBox5.Size = New System.Drawing.Size(150, 19)
        Me.CheckBox5.TabIndex = 3
        Me.CheckBox5.Text = "Both Signatures included"
        '
        'CheckBox6
        '
        Me.CheckBox6.DataBindings.Add(New System.Windows.Forms.Binding("Checked", Me.MainYMGIBindingSource, "InServiceArea", True))
        Me.CheckBox6.Location = New System.Drawing.Point(159, 58)
        Me.CheckBox6.Name = "CheckBox6"
        Me.CheckBox6.Size = New System.Drawing.Size(196, 21)
        Me.CheckBox6.TabIndex = 2
        Me.CheckBox6.Text = "In Service Area (Marion && contig.)"
        Me.CheckBox6.ThreeState = True
        Me.CheckBox6.Visible = False
        '
        'Label38
        '
        Me.Label38.BackColor = System.Drawing.Color.Transparent
        Me.Label38.Location = New System.Drawing.Point(69, 39)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(89, 16)
        Me.Label38.TabIndex = 251
        Me.Label38.Text = "Enclosures:"
        Me.Label38.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblGotoCongRep
        '
        Me.lblGotoCongRep.BackColor = System.Drawing.Color.Transparent
        Me.lblGotoCongRep.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblGotoCongRep.ForeColor = System.Drawing.Color.ForestGreen
        Me.lblGotoCongRep.Location = New System.Drawing.Point(21, 192)
        Me.lblGotoCongRep.Name = "lblGotoCongRep"
        Me.lblGotoCongRep.Size = New System.Drawing.Size(64, 20)
        Me.lblGotoCongRep.TabIndex = 221
        Me.lblGotoCongRep.Tag = "CongrRep"
        Me.lblGotoCongRep.Text = "Congr Rep."
        Me.lblGotoCongRep.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtTentative
        '
        Me.txtTentative.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainYMGIBindingSource, "TentativePlan", True))
        Me.txtTentative.Location = New System.Drawing.Point(72, 250)
        Me.txtTentative.Multiline = True
        Me.txtTentative.Name = "txtTentative"
        Me.txtTentative.Size = New System.Drawing.Size(282, 54)
        Me.txtTentative.TabIndex = 10
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.Color.Transparent
        Me.Label16.Location = New System.Drawing.Point(4, 250)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(62, 37)
        Me.Label16.TabIndex = 150
        Me.Label16.Text = "Tentative Plan"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblGotoPD
        '
        Me.lblGotoPD.BackColor = System.Drawing.Color.Transparent
        Me.lblGotoPD.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblGotoPD.ForeColor = System.Drawing.Color.ForestGreen
        Me.lblGotoPD.Location = New System.Drawing.Point(3, 166)
        Me.lblGotoPD.Name = "lblGotoPD"
        Me.lblGotoPD.Size = New System.Drawing.Size(85, 20)
        Me.lblGotoPD.TabIndex = 147
        Me.lblGotoPD.Tag = "ProjDir"
        Me.lblGotoPD.Text = "Project Director"
        Me.lblGotoPD.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label19
        '
        Me.Label19.ForeColor = System.Drawing.Color.Black
        Me.Label19.Location = New System.Drawing.Point(8, 8)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(147, 24)
        Me.Label19.TabIndex = 7
        Me.Label19.Text = "Date Application Received"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label39
        '
        Me.Label39.BackColor = System.Drawing.Color.Transparent
        Me.Label39.Location = New System.Drawing.Point(14, 317)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(152, 24)
        Me.Label39.TabIndex = 229
        Me.Label39.Text = "Date Receipt of Appl. Mailed"
        Me.Label39.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dtAppReceiptMail
        '
        Me.dtAppReceiptMail.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainYMGIBindingSource, "AppReceiptMailDt", True))
        Me.dtAppReceiptMail.Location = New System.Drawing.Point(172, 319)
        Me.dtAppReceiptMail.Name = "dtAppReceiptMail"
        Me.dtAppReceiptMail.Size = New System.Drawing.Size(70, 20)
        Me.dtAppReceiptMail.TabIndex = 11
        Me.dtAppReceiptMail.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label13
        '
        Me.Label13.Font = New System.Drawing.Font("Comic Sans MS", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.Color.DeepSkyBlue
        Me.Label13.Location = New System.Drawing.Point(141, 68)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(224, 21)
        Me.Label13.TabIndex = 255
        Me.Label13.Text = "MGI PROGRAM APPLICATION"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pnlProcess
        '
        Me.pnlProcess.BackColor = System.Drawing.Color.MintCream
        Me.pnlProcess.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pnlProcess.Controls.Add(Me.Label12)
        Me.pnlProcess.Controls.Add(Me.TextBox2)
        Me.pnlProcess.Controls.Add(Me.TextBox1)
        Me.pnlProcess.Controls.Add(Me.cboDetermination)
        Me.pnlProcess.Controls.Add(Me.CheckBox7)
        Me.pnlProcess.Controls.Add(Me.Label2)
        Me.pnlProcess.Controls.Add(Me.dtAgreementRecd)
        Me.pnlProcess.Controls.Add(Me.Label4)
        Me.pnlProcess.Controls.Add(Me.dtAgreementMail)
        Me.pnlProcess.Controls.Add(Me.Label10)
        Me.pnlProcess.Controls.Add(Me.dtNotifyMail)
        Me.pnlProcess.Controls.Add(Me.Label41)
        Me.pnlProcess.Controls.Add(Me.cboCallNotified)
        Me.pnlProcess.Controls.Add(Me.Label7)
        Me.pnlProcess.Controls.Add(Me.Label15)
        Me.pnlProcess.Controls.Add(Me.dtDetermination)
        Me.pnlProcess.Controls.Add(Me.cboDeterminationStaff)
        Me.pnlProcess.Controls.Add(Me.Label21)
        Me.pnlProcess.Controls.Add(Me.dtNotifyCall)
        Me.pnlProcess.Location = New System.Drawing.Point(428, 92)
        Me.pnlProcess.Name = "pnlProcess"
        Me.pnlProcess.Size = New System.Drawing.Size(432, 190)
        Me.pnlProcess.TabIndex = 257
        '
        'Label12
        '
        Me.Label12.Location = New System.Drawing.Point(255, 156)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(60, 24)
        Me.Label12.TabIndex = 235
        Me.Label12.Text = "Check #:"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'TextBox2
        '
        Me.TextBox2.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainYMGIBindingSource, "FeeCheckNum", True))
        Me.TextBox2.Location = New System.Drawing.Point(321, 161)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(60, 20)
        Me.TextBox2.TabIndex = 11
        Me.TextBox2.Tag = ""
        Me.TextBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TextBox1
        '
        Me.TextBox1.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainYMGIBindingSource, "DepositDt", True))
        Me.TextBox1.Location = New System.Drawing.Point(189, 160)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(70, 20)
        Me.TextBox1.TabIndex = 10
        Me.TextBox1.Tag = ""
        Me.TextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'cboDetermination
        '
        Me.cboDetermination.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboDetermination.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboDetermination.DataBindings.Add(New System.Windows.Forms.Binding("SelectedItem", Me.MainYMGIBindingSource, "Determination", True))
        Me.cboDetermination.FormattingEnabled = True
        Me.cboDetermination.Items.AddRange(New Object() {"Approved", "Denied", "Rescinded", "Returned", "Waiting List", "Withdrawn"})
        Me.cboDetermination.Location = New System.Drawing.Point(86, 26)
        Me.cboDetermination.Name = "cboDetermination"
        Me.cboDetermination.RestrictContentToListItems = True
        Me.cboDetermination.Size = New System.Drawing.Size(94, 21)
        Me.cboDetermination.TabIndex = 0
        '
        'CheckBox7
        '
        Me.CheckBox7.DataBindings.Add(New System.Windows.Forms.Binding("Checked", Me.MainYMGIBindingSource, "FeePaid", True))
        Me.CheckBox7.Location = New System.Drawing.Point(16, 160)
        Me.CheckBox7.Margin = New System.Windows.Forms.Padding(0)
        Me.CheckBox7.Name = "CheckBox7"
        Me.CheckBox7.Size = New System.Drawing.Size(176, 21)
        Me.CheckBox7.TabIndex = 8
        Me.CheckBox7.Text = "$75 Registration fee.  Deposit:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Teal
        Me.Label2.Location = New System.Drawing.Point(326, 8)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(29, 13)
        Me.Label2.TabIndex = 232
        Me.Label2.Text = "Staff"
        '
        'dtAgreementRecd
        '
        Me.dtAgreementRecd.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainYMGIBindingSource, "AgreementRecdDt", True))
        Me.dtAgreementRecd.Location = New System.Drawing.Point(189, 134)
        Me.dtAgreementRecd.Name = "dtAgreementRecd"
        Me.dtAgreementRecd.Size = New System.Drawing.Size(70, 20)
        Me.dtAgreementRecd.TabIndex = 7
        Me.dtAgreementRecd.Tag = ""
        Me.dtAgreementRecd.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Teal
        Me.Label4.Location = New System.Drawing.Point(194, 8)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(30, 13)
        Me.Label4.TabIndex = 228
        Me.Label4.Text = "Date"
        '
        'dtAgreementMail
        '
        Me.dtAgreementMail.AcceptsReturn = True
        Me.dtAgreementMail.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainYMGIBindingSource, "AgreementMailDt", True))
        Me.dtAgreementMail.Location = New System.Drawing.Point(189, 107)
        Me.dtAgreementMail.Name = "dtAgreementMail"
        Me.dtAgreementMail.Size = New System.Drawing.Size(70, 20)
        Me.dtAgreementMail.TabIndex = 6
        Me.dtAgreementMail.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Location = New System.Drawing.Point(53, 84)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(128, 16)
        Me.Label10.TabIndex = 227
        Me.Label10.Text = "Mailed Notification:"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dtNotifyMail
        '
        Me.dtNotifyMail.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainYMGIBindingSource, "NotifyMailDt", True))
        Me.dtNotifyMail.Location = New System.Drawing.Point(189, 80)
        Me.dtNotifyMail.Name = "dtNotifyMail"
        Me.dtNotifyMail.Size = New System.Drawing.Size(70, 20)
        Me.dtNotifyMail.TabIndex = 5
        Me.dtNotifyMail.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label41
        '
        Me.Label41.Location = New System.Drawing.Point(39, 131)
        Me.Label41.Name = "Label41"
        Me.Label41.Size = New System.Drawing.Size(142, 24)
        Me.Label41.TabIndex = 11
        Me.Label41.Text = "Date Agreement Received:"
        Me.Label41.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cboCallNotified
        '
        Me.cboCallNotified.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboCallNotified.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboCallNotified.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.MainYMGIBindingSource, "NotifyStaffNum", True))
        Me.cboCallNotified.Location = New System.Drawing.Point(286, 53)
        Me.cboCallNotified.Name = "cboCallNotified"
        Me.cboCallNotified.RestrictContentToListItems = True
        Me.cboCallNotified.Size = New System.Drawing.Size(136, 21)
        Me.cboCallNotified.TabIndex = 4
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Location = New System.Drawing.Point(19, 112)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(161, 19)
        Me.Label7.TabIndex = 223
        Me.Label7.Text = "Mailed Participation Agreement:"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.Color.Transparent
        Me.Label15.Location = New System.Drawing.Point(3, 27)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(78, 17)
        Me.Label15.TabIndex = 221
        Me.Label15.Text = "Determination:"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'dtDetermination
        '
        Me.dtDetermination.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainYMGIBindingSource, "DeterminationDt", True))
        Me.dtDetermination.Location = New System.Drawing.Point(189, 26)
        Me.dtDetermination.Name = "dtDetermination"
        Me.dtDetermination.Size = New System.Drawing.Size(70, 20)
        Me.dtDetermination.TabIndex = 1
        Me.dtDetermination.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'cboDeterminationStaff
        '
        Me.cboDeterminationStaff.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboDeterminationStaff.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboDeterminationStaff.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.MainYMGIBindingSource, "DeterminationStaffNum", True))
        Me.cboDeterminationStaff.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboDeterminationStaff.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cboDeterminationStaff.Items.AddRange(New Object() {"Approved", "Denied", "Waiting List"})
        Me.cboDeterminationStaff.Location = New System.Drawing.Point(286, 26)
        Me.cboDeterminationStaff.Name = "cboDeterminationStaff"
        Me.cboDeterminationStaff.RestrictContentToListItems = True
        Me.cboDeterminationStaff.Size = New System.Drawing.Size(136, 21)
        Me.cboDeterminationStaff.TabIndex = 2
        '
        'Label21
        '
        Me.Label21.BackColor = System.Drawing.Color.Transparent
        Me.Label21.Location = New System.Drawing.Point(54, 57)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(128, 16)
        Me.Label21.TabIndex = 225
        Me.Label21.Text = "Phone Notification Made:"
        Me.Label21.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dtNotifyCall
        '
        Me.dtNotifyCall.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainYMGIBindingSource, "NotifyCallDt", True))
        Me.dtNotifyCall.Location = New System.Drawing.Point(189, 53)
        Me.dtNotifyCall.Name = "dtNotifyCall"
        Me.dtNotifyCall.Size = New System.Drawing.Size(70, 20)
        Me.dtNotifyCall.TabIndex = 3
        Me.dtNotifyCall.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'CheckBox3
        '
        Me.CheckBox3.DataBindings.Add(New System.Windows.Forms.Binding("Checked", Me.MainYMGIBindingSource, "Class3attend", True))
        Me.CheckBox3.Location = New System.Drawing.Point(334, 53)
        Me.CheckBox3.Name = "CheckBox3"
        Me.CheckBox3.Size = New System.Drawing.Size(76, 21)
        Me.CheckBox3.TabIndex = 4
        Me.CheckBox3.Text = "Nov 18"
        '
        'CheckBox2
        '
        Me.CheckBox2.DataBindings.Add(New System.Windows.Forms.Binding("Checked", Me.MainYMGIBindingSource, "Class2Attend", True))
        Me.CheckBox2.Location = New System.Drawing.Point(260, 53)
        Me.CheckBox2.Name = "CheckBox2"
        Me.CheckBox2.Size = New System.Drawing.Size(68, 21)
        Me.CheckBox2.TabIndex = 3
        Me.CheckBox2.Text = "Oct 28"
        '
        'Label42
        '
        Me.Label42.BackColor = System.Drawing.Color.Transparent
        Me.Label42.Location = New System.Drawing.Point(55, 19)
        Me.Label42.Margin = New System.Windows.Forms.Padding(0)
        Me.Label42.Name = "Label42"
        Me.Label42.Size = New System.Drawing.Size(129, 21)
        Me.Label42.TabIndex = 221
        Me.Label42.Text = "Date Class Confirmed:"
        Me.Label42.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dtClassConfirm
        '
        Me.dtClassConfirm.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainYMGIBindingSource, "ClassConfirmDt", True))
        Me.dtClassConfirm.Location = New System.Drawing.Point(189, 20)
        Me.dtClassConfirm.Name = "dtClassConfirm"
        Me.dtClassConfirm.Size = New System.Drawing.Size(70, 20)
        Me.dtClassConfirm.TabIndex = 0
        Me.dtClassConfirm.Tag = ""
        Me.dtClassConfirm.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'CheckBox1
        '
        Me.CheckBox1.DataBindings.Add(New System.Windows.Forms.Binding("Checked", Me.MainYMGIBindingSource, "Class1Attend", True))
        Me.CheckBox1.Location = New System.Drawing.Point(189, 53)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(60, 21)
        Me.CheckBox1.TabIndex = 2
        Me.CheckBox1.Text = "Oct 15"
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Location = New System.Drawing.Point(259, 19)
        Me.Label1.Margin = New System.Windows.Forms.Padding(0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(143, 24)
        Me.Label1.TabIndex = 219
        Me.Label1.Text = "1 Session:  Oct-Nov 2016"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Comic Sans MS", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.DeepSkyBlue
        Me.Label5.Location = New System.Drawing.Point(477, 68)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(308, 21)
        Me.Label5.TabIndex = 258
        Me.Label5.Text = "DETERMINATION of ACCEPTANCE into MGI"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pnlGrant
        '
        Me.pnlGrant.BackColor = System.Drawing.Color.LightCyan
        Me.pnlGrant.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pnlGrant.Controls.Add(Me.Label14)
        Me.pnlGrant.Controls.Add(Me.rbMarch)
        Me.pnlGrant.Controls.Add(Me.rbJan)
        Me.pnlGrant.Controls.Add(Me.dtGrantDeadline)
        Me.pnlGrant.Controls.Add(Me.Label6)
        Me.pnlGrant.Controls.Add(Me.Label9)
        Me.pnlGrant.Controls.Add(Me.Label11)
        Me.pnlGrant.Controls.Add(Me.Label43)
        Me.pnlGrant.Controls.Add(Me.dtGrantAppRecd)
        Me.pnlGrant.Controls.Add(Me.CheckBox3)
        Me.pnlGrant.Controls.Add(Me.Label8)
        Me.pnlGrant.Controls.Add(Me.CheckBox2)
        Me.pnlGrant.Controls.Add(Me.Label42)
        Me.pnlGrant.Controls.Add(Me.Label1)
        Me.pnlGrant.Controls.Add(Me.dtClassConfirm)
        Me.pnlGrant.Controls.Add(Me.CheckBox1)
        Me.pnlGrant.Location = New System.Drawing.Point(428, 322)
        Me.pnlGrant.Name = "pnlGrant"
        Me.pnlGrant.Size = New System.Drawing.Size(432, 211)
        Me.pnlGrant.TabIndex = 259
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.ForeColor = System.Drawing.Color.Maroon
        Me.Label14.Location = New System.Drawing.Point(268, 169)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(134, 13)
        Me.Label14.TabIndex = 243
        Me.Label14.Text = "<--- generates regular grant"
        '
        'rbMarch
        '
        Me.rbMarch.AutoSize = True
        Me.rbMarch.Location = New System.Drawing.Point(288, 129)
        Me.rbMarch.Name = "rbMarch"
        Me.rbMarch.Size = New System.Drawing.Size(77, 17)
        Me.rbMarch.TabIndex = 7
        Me.rbMarch.TabStop = True
        Me.rbMarch.Text = "4/30/2016"
        Me.rbMarch.UseVisualStyleBackColor = True
        '
        'rbJan
        '
        Me.rbJan.AutoSize = True
        Me.rbJan.Location = New System.Drawing.Point(288, 107)
        Me.rbJan.Name = "rbJan"
        Me.rbJan.Size = New System.Drawing.Size(77, 17)
        Me.rbJan.TabIndex = 5
        Me.rbJan.TabStop = True
        Me.rbJan.Text = "1/30/2016"
        Me.rbJan.UseVisualStyleBackColor = True
        '
        'dtGrantDeadline
        '
        Me.dtGrantDeadline.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainYMGIBindingSource, "GrantAppDeadlineDt", True))
        Me.dtGrantDeadline.Location = New System.Drawing.Point(189, 117)
        Me.dtGrantDeadline.Name = "dtGrantDeadline"
        Me.dtGrantDeadline.ReadOnly = True
        Me.dtGrantDeadline.Size = New System.Drawing.Size(70, 20)
        Me.dtGrantDeadline.TabIndex = 8
        Me.dtGrantDeadline.TabStop = False
        Me.dtGrantDeadline.Tag = ""
        Me.dtGrantDeadline.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ToolTip1.SetToolTip(Me.dtGrantDeadline, "double-click to clear")
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(106, 83)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(235, 13)
        Me.Label6.TabIndex = 239
        Me.Label6.Text = "----------------------------------------------------------------------------"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.Teal
        Me.Label9.Location = New System.Drawing.Point(207, 3)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(30, 13)
        Me.Label9.TabIndex = 236
        Me.Label9.Text = "Date"
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.Transparent
        Me.Label11.Location = New System.Drawing.Point(78, 48)
        Me.Label11.Margin = New System.Windows.Forms.Padding(0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(93, 29)
        Me.Label11.TabIndex = 235
        Me.Label11.Text = "Attended Classes:"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label43
        '
        Me.Label43.BackColor = System.Drawing.Color.Transparent
        Me.Label43.Location = New System.Drawing.Point(21, 165)
        Me.Label43.Name = "Label43"
        Me.Label43.Size = New System.Drawing.Size(163, 20)
        Me.Label43.TabIndex = 8
        Me.Label43.Text = "Date Grant First Draft Received:"
        Me.Label43.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dtGrantAppRecd
        '
        Me.dtGrantAppRecd.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainYMGIBindingSource, "GrantAppRecdDt", True))
        Me.dtGrantAppRecd.Location = New System.Drawing.Point(189, 165)
        Me.dtGrantAppRecd.Name = "dtGrantAppRecd"
        Me.dtGrantAppRecd.Size = New System.Drawing.Size(73, 20)
        Me.dtGrantAppRecd.TabIndex = 10
        Me.dtGrantAppRecd.Tag = ""
        Me.dtGrantAppRecd.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Location = New System.Drawing.Point(13, 118)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(175, 18)
        Me.Label8.TabIndex = 6
        Me.Label8.Text = "Grant Application Deadline:"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label36
        '
        Me.Label36.Font = New System.Drawing.Font("Comic Sans MS", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label36.ForeColor = System.Drawing.Color.DeepSkyBlue
        Me.Label36.Location = New System.Drawing.Point(494, 299)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(291, 20)
        Me.Label36.TabIndex = 260
        Me.Label36.Text = "CLASS && GRANT APPLICATION"
        Me.Label36.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.Highlight
        Me.Panel1.Controls.Add(Me.btnGrant)
        Me.Panel1.Controls.Add(Me.btnSaveExit)
        Me.Panel1.Location = New System.Drawing.Point(776, 19)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(105, 40)
        Me.Panel1.TabIndex = 421
        '
        'btnGrant
        '
        Me.btnGrant.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.btnGrant.Location = New System.Drawing.Point(6, 1)
        Me.btnGrant.Name = "btnGrant"
        Me.btnGrant.Size = New System.Drawing.Size(50, 36)
        Me.btnGrant.TabIndex = 419
        Me.btnGrant.Text = "Go to Grant"
        Me.btnGrant.UseVisualStyleBackColor = False
        '
        'btnSaveExit
        '
        Me.btnSaveExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSaveExit.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.btnSaveExit.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSaveExit.Image = CType(resources.GetObject("btnSaveExit.Image"), System.Drawing.Image)
        Me.btnSaveExit.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSaveExit.Location = New System.Drawing.Point(62, 1)
        Me.btnSaveExit.Name = "btnSaveExit"
        Me.btnSaveExit.Size = New System.Drawing.Size(40, 35)
        Me.btnSaveExit.TabIndex = 418
        Me.btnSaveExit.Text = "Close"
        Me.btnSaveExit.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSaveExit.UseVisualStyleBackColor = False
        '
        'btnHelp
        '
        Me.btnHelp.Image = CType(resources.GetObject("btnHelp.Image"), System.Drawing.Image)
        Me.btnHelp.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnHelp.Location = New System.Drawing.Point(887, 19)
        Me.btnHelp.Name = "btnHelp"
        Me.btnHelp.Size = New System.Drawing.Size(25, 25)
        Me.btnHelp.TabIndex = 420
        Me.btnHelp.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnHelp.UseVisualStyleBackColor = True
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.ForeColor = System.Drawing.SystemColors.ControlDark
        Me.Label23.Location = New System.Drawing.Point(698, 569)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(68, 13)
        Me.Label23.TabIndex = 427
        Me.Label23.Text = "YMGI Appl #"
        '
        'fldGINum
        '
        Me.fldGINum.AutoSize = True
        Me.fldGINum.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainYMGIBindingSource, "GIID", True))
        Me.fldGINum.ForeColor = System.Drawing.SystemColors.ControlDark
        Me.fldGINum.Location = New System.Drawing.Point(782, 569)
        Me.fldGINum.Name = "fldGINum"
        Me.fldGINum.Size = New System.Drawing.Size(45, 13)
        Me.fldGINum.TabIndex = 426
        Me.fldGINum.Text = "Label23"
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.ForeColor = System.Drawing.SystemColors.ControlDark
        Me.Label24.Location = New System.Drawing.Point(731, 547)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(35, 13)
        Me.Label24.TabIndex = 425
        Me.Label24.Text = "OrgID"
        '
        'fldOrgID
        '
        Me.fldOrgID.AutoSize = True
        Me.fldOrgID.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainYMGIBindingSource, "OrgNum", True))
        Me.fldOrgID.ForeColor = System.Drawing.SystemColors.ControlDark
        Me.fldOrgID.Location = New System.Drawing.Point(782, 547)
        Me.fldOrgID.Name = "fldOrgID"
        Me.fldOrgID.Size = New System.Drawing.Size(45, 13)
        Me.fldOrgID.TabIndex = 424
        Me.fldOrgID.Text = "Label23"
        '
        'StatusBar1
        '
        Me.StatusBar1.Location = New System.Drawing.Point(0, 612)
        Me.StatusBar1.Name = "StatusBar1"
        Me.StatusBar1.Panels.AddRange(New System.Windows.Forms.StatusBarPanel() {Me.StatusBarPanel1, Me.StatusBarPanelID, Me.StatusBarPanel2})
        Me.StatusBar1.ShowPanels = True
        Me.StatusBar1.Size = New System.Drawing.Size(1005, 22)
        Me.StatusBar1.TabIndex = 429
        Me.StatusBar1.Text = "StatusBar1"
        '
        'StatusBarPanel1
        '
        Me.StatusBarPanel1.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents
        Me.StatusBarPanel1.MinWidth = 300
        Me.StatusBarPanel1.Name = "StatusBarPanel1"
        Me.StatusBarPanel1.Text = "StatusBarPanel1"
        Me.StatusBarPanel1.Width = 300
        '
        'StatusBarPanelID
        '
        Me.StatusBarPanelID.Alignment = System.Windows.Forms.HorizontalAlignment.Center
        Me.StatusBarPanelID.MinWidth = 400
        Me.StatusBarPanelID.Name = "StatusBarPanelID"
        Me.StatusBarPanelID.Text = "StatusBarPanel3"
        Me.StatusBarPanelID.Width = 400
        '
        'StatusBarPanel2
        '
        Me.StatusBarPanel2.Alignment = System.Windows.Forms.HorizontalAlignment.Right
        Me.StatusBarPanel2.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring
        Me.StatusBarPanel2.Name = "StatusBarPanel2"
        Me.StatusBarPanel2.Text = "Use this window to edit MGI app."
        Me.StatusBarPanel2.Width = 288
        '
        'MainMenu1
        '
        Me.MainMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem1, Me.MenuItem2, Me.MenuItem3, Me.MenuItem4})
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
        Me.miDelete.Text = "Delete grant Application"
        '
        'MenuItem3
        '
        Me.MenuItem3.Index = 2
        Me.MenuItem3.Text = "Reports"
        '
        'MenuItem4
        '
        Me.MenuItem4.Index = 3
        Me.MenuItem4.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.miGotoGrant})
        Me.MenuItem4.Text = "Goto"
        Me.MenuItem4.Visible = False
        '
        'miGotoGrant
        '
        Me.miGotoGrant.Index = 0
        Me.miGotoGrant.Text = "Grant"
        '
        'fldGotoOrg
        '
        Me.fldGotoOrg.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fldGotoOrg.ForeColor = System.Drawing.Color.ForestGreen
        Me.fldGotoOrg.Location = New System.Drawing.Point(322, 14)
        Me.fldGotoOrg.Multiline = True
        Me.fldGotoOrg.Name = "fldGotoOrg"
        Me.fldGotoOrg.Size = New System.Drawing.Size(397, 41)
        Me.fldGotoOrg.TabIndex = 430
        Me.fldGotoOrg.Text = "goto Org"
        '
        'tblGrantYMGITableAdapter
        '
        Me.tblGrantYMGITableAdapter.ClearBeforeFill = True
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Font = New System.Drawing.Font("Cooper Black", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(18, 18)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(173, 24)
        Me.Label17.TabIndex = 435
        Me.Label17.Text = "Youth Ministry"
        '
        'frmMainGrantYMGI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.LightYellow
        Me.ClientSize = New System.Drawing.Size(1005, 634)
        Me.Controls.Add(Me.Label17)
        Me.Controls.Add(Me.fldGotoOrg)
        Me.Controls.Add(Me.StatusBar1)
        Me.Controls.Add(Me.Label23)
        Me.Controls.Add(Me.fldGINum)
        Me.Controls.Add(Me.Label24)
        Me.Controls.Add(Me.fldOrgID)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.btnHelp)
        Me.Controls.Add(Me.pnlGrant)
        Me.Controls.Add(Me.Label36)
        Me.Controls.Add(Me.pnlProcess)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.pnlApplication)
        Me.Controls.Add(Me.Label13)
        Me.Menu = Me.MainMenu1
        Me.Name = "frmMainGrantYMGI"
        Me.Text = "YMGI APPLICATION DETAIL"
        Me.pnlApplication.ResumeLayout(False)
        Me.pnlApplication.PerformLayout()
        CType(Me.MainYMGIBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsMainGrantYMGI, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlProcess.ResumeLayout(False)
        Me.pnlProcess.PerformLayout()
        Me.pnlGrant.ResumeLayout(False)
        Me.pnlGrant.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        CType(Me.StatusBarPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.StatusBarPanelID, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.StatusBarPanel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MainYMGIBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents pnlApplication As System.Windows.Forms.Panel
    Friend WithEvents dtAppRecd As InfoCtr.DateTextBox
    Friend WithEvents txtRemarks As System.Windows.Forms.TextBox
    Friend WithEvents CheckBox8 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox9 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox4 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox5 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox6 As System.Windows.Forms.CheckBox
    Friend WithEvents Label38 As System.Windows.Forms.Label
    Friend WithEvents lblGotoCongRep As System.Windows.Forms.Label
    Friend WithEvents txtTentative As System.Windows.Forms.TextBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents lblGotoPD As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Label39 As System.Windows.Forms.Label
    Friend WithEvents dtAppReceiptMail As InfoCtr.DateTextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents pnlProcess As System.Windows.Forms.Panel
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents dtAgreementMail As InfoCtr.DateTextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents dtNotifyMail As InfoCtr.DateTextBox
    Private WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents dtDetermination As InfoCtr.DateTextBox
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents dtNotifyCall As InfoCtr.DateTextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents pnlGrant As System.Windows.Forms.Panel
    Friend WithEvents txtNotes As System.Windows.Forms.TextBox
    Friend WithEvents dtAgreementRecd As InfoCtr.DateTextBox
    Friend WithEvents CheckBox3 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox2 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox1 As System.Windows.Forms.CheckBox
    Friend WithEvents Label43 As System.Windows.Forms.Label
    Friend WithEvents dtGrantAppRecd As InfoCtr.DateTextBox
    Friend WithEvents Label42 As System.Windows.Forms.Label
    Friend WithEvents dtClassConfirm As InfoCtr.DateTextBox
    Friend WithEvents Label41 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label36 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents btnSaveExit As System.Windows.Forms.Button
    Friend WithEvents btnHelp As System.Windows.Forms.Button
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents CheckBox7 As System.Windows.Forms.CheckBox
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents fldGINum As System.Windows.Forms.Label
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents fldOrgID As System.Windows.Forms.Label
    Friend WithEvents DsMainGrantYMGI As InfoCtr.dsMainGrantYMGI
    Friend WithEvents tblGrantYMGITableAdapter As InfoCtr.dsMainGrantYMGITableAdapters.tblGrantYMGITableAdapter
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents StatusBar1 As System.Windows.Forms.StatusBar
    Friend WithEvents StatusBarPanel1 As System.Windows.Forms.StatusBarPanel
    Friend WithEvents StatusBarPanel2 As System.Windows.Forms.StatusBarPanel
    Friend WithEvents StatusBarPanelID As System.Windows.Forms.StatusBarPanel
    Friend WithEvents MainMenu1 As System.Windows.Forms.MainMenu
    Friend WithEvents MenuItem1 As System.Windows.Forms.MenuItem
    Friend WithEvents miClose As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem2 As System.Windows.Forms.MenuItem
    Friend WithEvents miSave As System.Windows.Forms.MenuItem
    Friend WithEvents miCancel As System.Windows.Forms.MenuItem
    Friend WithEvents miDelete As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem3 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem4 As System.Windows.Forms.MenuItem
    Friend WithEvents miGotoGrant As System.Windows.Forms.MenuItem
    Friend WithEvents fldGotoOrg As System.Windows.Forms.TextBox
    Friend WithEvents btnGrant As System.Windows.Forms.Button
    Friend WithEvents lblGotoCase As System.Windows.Forms.Label
    Friend WithEvents fldCaseID As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents TextBox2 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents rbMarch As System.Windows.Forms.RadioButton
    Friend WithEvents rbJan As System.Windows.Forms.RadioButton
    Friend WithEvents dtGrantDeadline As InfoCtr.DateTextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents cboPD As InfoCtr.ComboBoxRelaxed
    Friend WithEvents cboCongRep As InfoCtr.ComboBoxRelaxed
    Friend WithEvents cboCallNotified As InfoCtr.ComboBoxRelaxed
    Friend WithEvents cboDeterminationStaff As InfoCtr.ComboBoxRelaxed
    Friend WithEvents cboDetermination As InfoCtr.ComboBoxRelaxed
    Friend WithEvents cboCase As InfoCtr.ComboBoxRelaxed
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents Label17 As System.Windows.Forms.Label
End Class
