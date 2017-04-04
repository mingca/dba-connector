<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMainGrantTGI
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMainGrantTGI))
        Me.pnlApplication = New System.Windows.Forms.Panel()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.BindingSource1 = New System.Windows.Forms.BindingSource(Me.components)
        Me.DsMainGrantTGI = New InfoCtr.dsMainGrantTGI()
        Me.cboCase = New System.Windows.Forms.ComboBox()
        Me.fldGotoCase = New System.Windows.Forms.TextBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.cboClergyLeader = New System.Windows.Forms.ComboBox()
        Me.txtNotes = New System.Windows.Forms.TextBox()
        Me.cboPD = New System.Windows.Forms.ComboBox()
        Me.dtAppRecd = New System.Windows.Forms.TextBox()
        Me.txtRemarks = New System.Windows.Forms.TextBox()
        Me.CheckBox8 = New System.Windows.Forms.CheckBox()
        Me.CheckBox9 = New System.Windows.Forms.CheckBox()
        Me.CheckBox4 = New System.Windows.Forms.CheckBox()
        Me.CheckBox5 = New System.Windows.Forms.CheckBox()
        Me.CheckBox6 = New System.Windows.Forms.CheckBox()
        Me.Label38 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtTentative = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.fldContact = New System.Windows.Forms.TextBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label39 = New System.Windows.Forms.Label()
        Me.dtAppReceiptMail = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.pnlProcess = New System.Windows.Forms.Panel()
        Me.cboDetermination = New System.Windows.Forms.ComboBox()
        Me.CheckBox7 = New System.Windows.Forms.CheckBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.dtAgreementRecd = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.dtAgreementMail = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.dtNotifyMail = New System.Windows.Forms.TextBox()
        Me.Label41 = New System.Windows.Forms.Label()
        Me.cboCallNotified = New System.Windows.Forms.ComboBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.dtDetermination = New System.Windows.Forms.TextBox()
        Me.cboDeterminationStaff = New System.Windows.Forms.ComboBox()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.dtNotifyCall = New System.Windows.Forms.TextBox()
        Me.CheckBox3 = New System.Windows.Forms.CheckBox()
        Me.cboClassGroup = New System.Windows.Forms.ComboBox()
        Me.CheckBox2 = New System.Windows.Forms.CheckBox()
        Me.Label42 = New System.Windows.Forms.Label()
        Me.dtClassConfirm = New System.Windows.Forms.TextBox()
        Me.CheckBox1 = New System.Windows.Forms.CheckBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.pnlGrant = New System.Windows.Forms.Panel()
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label43 = New System.Windows.Forms.Label()
        Me.dtGrantAppRecd = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.dtGrantAppDeadline = New System.Windows.Forms.TextBox()
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
        Me.MainGrantTGITableAdapter = New InfoCtr.dsMainGrantTGITableAdapters.MainGrantTGITableAdapter()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.pnlApplication.SuspendLayout()
        CType(Me.BindingSource1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsMainGrantTGI, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlProcess.SuspendLayout()
        Me.pnlGrant.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.StatusBarPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.StatusBarPanel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlApplication
        '
        Me.pnlApplication.BackColor = System.Drawing.Color.PaleTurquoise
        Me.pnlApplication.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pnlApplication.Controls.Add(Me.Label6)
        Me.pnlApplication.Controls.Add(Me.cboCase)
        Me.pnlApplication.Controls.Add(Me.fldGotoCase)
        Me.pnlApplication.Controls.Add(Me.Label18)
        Me.pnlApplication.Controls.Add(Me.cboClergyLeader)
        Me.pnlApplication.Controls.Add(Me.txtNotes)
        Me.pnlApplication.Controls.Add(Me.cboPD)
        Me.pnlApplication.Controls.Add(Me.dtAppRecd)
        Me.pnlApplication.Controls.Add(Me.txtRemarks)
        Me.pnlApplication.Controls.Add(Me.CheckBox8)
        Me.pnlApplication.Controls.Add(Me.CheckBox9)
        Me.pnlApplication.Controls.Add(Me.CheckBox4)
        Me.pnlApplication.Controls.Add(Me.CheckBox5)
        Me.pnlApplication.Controls.Add(Me.CheckBox6)
        Me.pnlApplication.Controls.Add(Me.Label38)
        Me.pnlApplication.Controls.Add(Me.Label3)
        Me.pnlApplication.Controls.Add(Me.txtTentative)
        Me.pnlApplication.Controls.Add(Me.Label16)
        Me.pnlApplication.Controls.Add(Me.fldContact)
        Me.pnlApplication.Controls.Add(Me.Label19)
        Me.pnlApplication.Controls.Add(Me.Label39)
        Me.pnlApplication.Controls.Add(Me.dtAppReceiptMail)
        Me.pnlApplication.Location = New System.Drawing.Point(58, 86)
        Me.pnlApplication.Name = "pnlApplication"
        Me.pnlApplication.Size = New System.Drawing.Size(366, 441)
        Me.pnlApplication.TabIndex = 254
        Me.pnlApplication.TabStop = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.BindingSource1, "CaseNum", True))
        Me.Label6.Location = New System.Drawing.Point(324, 223)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(39, 13)
        Me.Label6.TabIndex = 265
        Me.Label6.Text = "Label6"
        '
        'BindingSource1
        '
        Me.BindingSource1.DataMember = "MainGrantTGI"
        Me.BindingSource1.DataSource = Me.DsMainGrantTGI
        '
        'DsMainGrantTGI
        '
        Me.DsMainGrantTGI.DataSetName = "dsMainGrantTGI"
        Me.DsMainGrantTGI.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'cboCase
        '
        Me.cboCase.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.BindingSource1, "CaseNum", True))
        Me.cboCase.Location = New System.Drawing.Point(90, 221)
        Me.cboCase.Name = "cboCase"
        Me.cboCase.Size = New System.Drawing.Size(224, 21)
        Me.cboCase.TabIndex = 9
        '
        'fldGotoCase
        '
        Me.fldGotoCase.BackColor = System.Drawing.SystemColors.Control
        Me.fldGotoCase.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fldGotoCase.ForeColor = System.Drawing.Color.ForestGreen
        Me.fldGotoCase.Location = New System.Drawing.Point(10, 222)
        Me.fldGotoCase.Name = "fldGotoCase"
        Me.fldGotoCase.Size = New System.Drawing.Size(72, 20)
        Me.fldGotoCase.TabIndex = 264
        Me.fldGotoCase.Tag = "Case"
        Me.fldGotoCase.Text = "Case Name"
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
        'cboClergyLeader
        '
        Me.cboClergyLeader.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.BindingSource1, "ClergyLeaderNum", True))
        Me.cboClergyLeader.FormattingEnabled = True
        Me.cboClergyLeader.Location = New System.Drawing.Point(90, 167)
        Me.cboClergyLeader.Name = "cboClergyLeader"
        Me.cboClergyLeader.Size = New System.Drawing.Size(222, 21)
        Me.cboClergyLeader.TabIndex = 7
        '
        'txtNotes
        '
        Me.txtNotes.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.BindingSource1, "Notes", True))
        Me.txtNotes.Location = New System.Drawing.Point(51, 348)
        Me.txtNotes.Multiline = True
        Me.txtNotes.Name = "txtNotes"
        Me.txtNotes.Size = New System.Drawing.Size(308, 80)
        Me.txtNotes.TabIndex = 12
        Me.txtNotes.Text = "Notes"
        '
        'cboPD
        '
        Me.cboPD.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.BindingSource1, "ProjectDirectorNum", True))
        Me.cboPD.FormattingEnabled = True
        Me.cboPD.Location = New System.Drawing.Point(90, 194)
        Me.cboPD.Name = "cboPD"
        Me.cboPD.Size = New System.Drawing.Size(222, 21)
        Me.cboPD.TabIndex = 8
        '
        'dtAppRecd
        '
        Me.dtAppRecd.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.BindingSource1, "AppRecdDt", True))
        Me.dtAppRecd.Location = New System.Drawing.Point(172, 11)
        Me.dtAppRecd.Name = "dtAppRecd"
        Me.dtAppRecd.Size = New System.Drawing.Size(60, 20)
        Me.dtAppRecd.TabIndex = 0
        Me.dtAppRecd.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtRemarks
        '
        Me.txtRemarks.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsMainGrantTGI, "MainGrantTGI.InclRemarks", True))
        Me.txtRemarks.Location = New System.Drawing.Point(7, 69)
        Me.txtRemarks.Multiline = True
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.Size = New System.Drawing.Size(142, 86)
        Me.txtRemarks.TabIndex = 1
        Me.txtRemarks.Text = "Enclosure Remarks"
        '
        'CheckBox8
        '
        Me.CheckBox8.DataBindings.Add(New System.Windows.Forms.Binding("Checked", Me.BindingSource1, "InclPostmark", True))
        Me.CheckBox8.Location = New System.Drawing.Point(159, 141)
        Me.CheckBox8.Name = "CheckBox8"
        Me.CheckBox8.Size = New System.Drawing.Size(173, 19)
        Me.CheckBox8.TabIndex = 6
        Me.CheckBox8.Text = "Postmarked by 4/30/2010"
        '
        'CheckBox9
        '
        Me.CheckBox9.DataBindings.Add(New System.Windows.Forms.Binding("Checked", Me.BindingSource1, "InclCongProfile", True))
        Me.CheckBox9.Location = New System.Drawing.Point(159, 119)
        Me.CheckBox9.Name = "CheckBox9"
        Me.CheckBox9.Size = New System.Drawing.Size(200, 24)
        Me.CheckBox9.TabIndex = 5
        Me.CheckBox9.Text = "Application Complete (Congr. Profile)"
        '
        'CheckBox4
        '
        Me.CheckBox4.DataBindings.Add(New System.Windows.Forms.Binding("Checked", Me.BindingSource1, "InclAbility501c3", True))
        Me.CheckBox4.Location = New System.Drawing.Point(159, 104)
        Me.CheckBox4.Name = "CheckBox4"
        Me.CheckBox4.Size = New System.Drawing.Size(195, 16)
        Me.CheckBox4.TabIndex = 4
        Me.CheckBox4.Text = "Ability to Provide 501c3 included"
        '
        'CheckBox5
        '
        Me.CheckBox5.DataBindings.Add(New System.Windows.Forms.Binding("Checked", Me.BindingSource1, "InclSignatures", True))
        Me.CheckBox5.Location = New System.Drawing.Point(159, 84)
        Me.CheckBox5.Name = "CheckBox5"
        Me.CheckBox5.Size = New System.Drawing.Size(150, 19)
        Me.CheckBox5.TabIndex = 3
        Me.CheckBox5.Text = "Both Signatures included"
        '
        'CheckBox6
        '
        Me.CheckBox6.DataBindings.Add(New System.Windows.Forms.Binding("Checked", Me.BindingSource1, "InServiceArea", True))
        Me.CheckBox6.Location = New System.Drawing.Point(159, 64)
        Me.CheckBox6.Name = "CheckBox6"
        Me.CheckBox6.Size = New System.Drawing.Size(196, 21)
        Me.CheckBox6.TabIndex = 2
        Me.CheckBox6.Text = "In Service Area (Marion && contig.)"
        '
        'Label38
        '
        Me.Label38.BackColor = System.Drawing.Color.Transparent
        Me.Label38.Location = New System.Drawing.Point(79, 50)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(89, 16)
        Me.Label38.TabIndex = 251
        Me.Label38.Text = "Enclosures:"
        Me.Label38.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(21, 192)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(64, 20)
        Me.Label3.TabIndex = 221
        Me.Label3.Tag = ""
        Me.Label3.Text = "Project Dir."
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtTentative
        '
        Me.txtTentative.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.BindingSource1, "TentativePlan", True))
        Me.txtTentative.Location = New System.Drawing.Point(72, 250)
        Me.txtTentative.Multiline = True
        Me.txtTentative.Name = "txtTentative"
        Me.txtTentative.Size = New System.Drawing.Size(282, 54)
        Me.txtTentative.TabIndex = 10
        Me.txtTentative.Text = "TextBox4"
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
        'fldContact
        '
        Me.fldContact.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fldContact.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fldContact.Location = New System.Drawing.Point(11, 166)
        Me.fldContact.Name = "fldContact"
        Me.fldContact.Size = New System.Drawing.Size(77, 20)
        Me.fldContact.TabIndex = 147
        Me.fldContact.Tag = ""
        Me.fldContact.Text = "Clergy Leader"
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
        Me.dtAppReceiptMail.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.BindingSource1, "AppReceiptMailDt", True))
        Me.dtAppReceiptMail.Location = New System.Drawing.Point(172, 319)
        Me.dtAppReceiptMail.Name = "dtAppReceiptMail"
        Me.dtAppReceiptMail.Size = New System.Drawing.Size(60, 20)
        Me.dtAppReceiptMail.TabIndex = 11
        Me.dtAppReceiptMail.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label13
        '
        Me.Label13.Font = New System.Drawing.Font("Comic Sans MS", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.Color.CadetBlue
        Me.Label13.Location = New System.Drawing.Point(148, 62)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(224, 21)
        Me.Label13.TabIndex = 255
        Me.Label13.Text = "MGI APPLICATION"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pnlProcess
        '
        Me.pnlProcess.BackColor = System.Drawing.Color.FromArgb(CType(CType(175, Byte), Integer), CType(CType(238, Byte), Integer), CType(CType(230, Byte), Integer))
        Me.pnlProcess.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
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
        Me.pnlProcess.Location = New System.Drawing.Point(435, 86)
        Me.pnlProcess.Name = "pnlProcess"
        Me.pnlProcess.Size = New System.Drawing.Size(432, 214)
        Me.pnlProcess.TabIndex = 257
        '
        'cboDetermination
        '
        Me.cboDetermination.DataBindings.Add(New System.Windows.Forms.Binding("SelectedItem", Me.BindingSource1, "Determination", True))
        Me.cboDetermination.FormattingEnabled = True
        Me.cboDetermination.Items.AddRange(New Object() {"Approved", "Denied", "Rescinded", "Returned", "Withdrawn"})
        Me.cboDetermination.Location = New System.Drawing.Point(78, 26)
        Me.cboDetermination.Name = "cboDetermination"
        Me.cboDetermination.Size = New System.Drawing.Size(94, 21)
        Me.cboDetermination.TabIndex = 0
        '
        'CheckBox7
        '
        Me.CheckBox7.DataBindings.Add(New System.Windows.Forms.Binding("Checked", Me.BindingSource1, "FeePaid", True))
        Me.CheckBox7.Location = New System.Drawing.Point(90, 167)
        Me.CheckBox7.Name = "CheckBox7"
        Me.CheckBox7.Size = New System.Drawing.Size(173, 21)
        Me.CheckBox7.TabIndex = 8
        Me.CheckBox7.Text = "paid $75 Registration fee"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.ForeColor = System.Drawing.Color.Teal
        Me.Label2.Location = New System.Drawing.Point(326, 10)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(29, 13)
        Me.Label2.TabIndex = 232
        Me.Label2.Text = "Staff"
        '
        'dtAgreementRecd
        '
        Me.dtAgreementRecd.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.BindingSource1, "AgreementRecdDt", True))
        Me.dtAgreementRecd.Location = New System.Drawing.Point(189, 134)
        Me.dtAgreementRecd.Name = "dtAgreementRecd"
        Me.dtAgreementRecd.Size = New System.Drawing.Size(60, 20)
        Me.dtAgreementRecd.TabIndex = 7
        Me.dtAgreementRecd.Tag = ""
        Me.dtAgreementRecd.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.ForeColor = System.Drawing.Color.Teal
        Me.Label4.Location = New System.Drawing.Point(194, 10)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(35, 13)
        Me.Label4.TabIndex = 228
        Me.Label4.Text = "Dates"
        '
        'dtAgreementMail
        '
        Me.dtAgreementMail.AcceptsReturn = True
        Me.dtAgreementMail.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.BindingSource1, "AgreementMailDt", True))
        Me.dtAgreementMail.Location = New System.Drawing.Point(189, 107)
        Me.dtAgreementMail.Name = "dtAgreementMail"
        Me.dtAgreementMail.Size = New System.Drawing.Size(60, 20)
        Me.dtAgreementMail.TabIndex = 6
        Me.dtAgreementMail.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Location = New System.Drawing.Point(41, 81)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(128, 16)
        Me.Label10.TabIndex = 227
        Me.Label10.Text = "Mailed Notification"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dtNotifyMail
        '
        Me.dtNotifyMail.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.BindingSource1, "NotifyMailDt", True))
        Me.dtNotifyMail.Location = New System.Drawing.Point(189, 80)
        Me.dtNotifyMail.Name = "dtNotifyMail"
        Me.dtNotifyMail.Size = New System.Drawing.Size(60, 20)
        Me.dtNotifyMail.TabIndex = 5
        Me.dtNotifyMail.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label41
        '
        Me.Label41.Location = New System.Drawing.Point(31, 131)
        Me.Label41.Name = "Label41"
        Me.Label41.Size = New System.Drawing.Size(140, 24)
        Me.Label41.TabIndex = 11
        Me.Label41.Text = "Date Agreement Received"
        Me.Label41.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cboCallNotified
        '
        Me.cboCallNotified.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.BindingSource1, "NotifyStaffNum", True))
        Me.cboCallNotified.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCallNotified.Location = New System.Drawing.Point(286, 53)
        Me.cboCallNotified.Name = "cboCallNotified"
        Me.cboCallNotified.Size = New System.Drawing.Size(136, 21)
        Me.cboCallNotified.TabIndex = 4
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Location = New System.Drawing.Point(23, 104)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(148, 24)
        Me.Label7.TabIndex = 223
        Me.Label7.Text = "Mailed Participation Agreement"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.Color.Transparent
        Me.Label15.Location = New System.Drawing.Point(3, 26)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(78, 17)
        Me.Label15.TabIndex = 221
        Me.Label15.Text = "Determination"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'dtDetermination
        '
        Me.dtDetermination.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.BindingSource1, "DeterminationDt", True))
        Me.dtDetermination.Location = New System.Drawing.Point(189, 26)
        Me.dtDetermination.Name = "dtDetermination"
        Me.dtDetermination.Size = New System.Drawing.Size(60, 20)
        Me.dtDetermination.TabIndex = 1
        Me.dtDetermination.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'cboDeterminationStaff
        '
        Me.cboDeterminationStaff.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.BindingSource1, "DeterminationStaffNum", True))
        Me.cboDeterminationStaff.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDeterminationStaff.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboDeterminationStaff.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cboDeterminationStaff.Items.AddRange(New Object() {"Approved", "Denied", "Waiting List"})
        Me.cboDeterminationStaff.Location = New System.Drawing.Point(286, 26)
        Me.cboDeterminationStaff.Name = "cboDeterminationStaff"
        Me.cboDeterminationStaff.Size = New System.Drawing.Size(136, 21)
        Me.cboDeterminationStaff.TabIndex = 2
        '
        'Label21
        '
        Me.Label21.BackColor = System.Drawing.Color.Transparent
        Me.Label21.Location = New System.Drawing.Point(41, 50)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(128, 16)
        Me.Label21.TabIndex = 225
        Me.Label21.Text = "Phone Notification Made"
        Me.Label21.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dtNotifyCall
        '
        Me.dtNotifyCall.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.BindingSource1, "NotifyCallDt", True))
        Me.dtNotifyCall.Location = New System.Drawing.Point(189, 53)
        Me.dtNotifyCall.Name = "dtNotifyCall"
        Me.dtNotifyCall.Size = New System.Drawing.Size(60, 20)
        Me.dtNotifyCall.TabIndex = 3
        Me.dtNotifyCall.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'CheckBox3
        '
        Me.CheckBox3.DataBindings.Add(New System.Windows.Forms.Binding("Checked", Me.BindingSource1, "Class3attend", True))
        Me.CheckBox3.Location = New System.Drawing.Point(392, 51)
        Me.CheckBox3.Name = "CheckBox3"
        Me.CheckBox3.Size = New System.Drawing.Size(33, 21)
        Me.CheckBox3.TabIndex = 4
        Me.CheckBox3.Text = "3"
        '
        'cboClassGroup
        '
        Me.cboClassGroup.DataBindings.Add(New System.Windows.Forms.Binding("SelectedItem", Me.BindingSource1, "ClassGroup", True))
        Me.cboClassGroup.FormattingEnabled = True
        Me.cboClassGroup.Items.AddRange(New Object() {"Class I: Aug - Sept 2010", "Class II: Feb - Mar 2011"})
        Me.cboClassGroup.Location = New System.Drawing.Point(73, 51)
        Me.cboClassGroup.Name = "cboClassGroup"
        Me.cboClassGroup.Size = New System.Drawing.Size(168, 21)
        Me.cboClassGroup.TabIndex = 1
        '
        'CheckBox2
        '
        Me.CheckBox2.DataBindings.Add(New System.Windows.Forms.Binding("Checked", Me.BindingSource1, "Class2Attend", True))
        Me.CheckBox2.Location = New System.Drawing.Point(352, 51)
        Me.CheckBox2.Name = "CheckBox2"
        Me.CheckBox2.Size = New System.Drawing.Size(34, 21)
        Me.CheckBox2.TabIndex = 3
        Me.CheckBox2.Text = "2"
        '
        'Label42
        '
        Me.Label42.BackColor = System.Drawing.Color.Transparent
        Me.Label42.Location = New System.Drawing.Point(121, 24)
        Me.Label42.Margin = New System.Windows.Forms.Padding(0)
        Me.Label42.Name = "Label42"
        Me.Label42.Size = New System.Drawing.Size(129, 21)
        Me.Label42.TabIndex = 221
        Me.Label42.Text = "Date Class Confirmed"
        Me.Label42.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dtClassConfirm
        '
        Me.dtClassConfirm.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.BindingSource1, "ClassConfirmDt", True))
        Me.dtClassConfirm.Location = New System.Drawing.Point(258, 25)
        Me.dtClassConfirm.Name = "dtClassConfirm"
        Me.dtClassConfirm.Size = New System.Drawing.Size(60, 20)
        Me.dtClassConfirm.TabIndex = 0
        Me.dtClassConfirm.Tag = ""
        Me.dtClassConfirm.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'CheckBox1
        '
        Me.CheckBox1.DataBindings.Add(New System.Windows.Forms.Binding("Checked", Me.BindingSource1, "Class1Attend", True))
        Me.CheckBox1.Location = New System.Drawing.Point(311, 51)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(38, 21)
        Me.CheckBox1.TabIndex = 2
        Me.CheckBox1.Text = "1"
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Location = New System.Drawing.Point(2, 48)
        Me.Label1.Margin = New System.Windows.Forms.Padding(0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(68, 24)
        Me.Label1.TabIndex = 219
        Me.Label1.Text = "Session"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Comic Sans MS", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.CadetBlue
        Me.Label5.Location = New System.Drawing.Point(591, 62)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(154, 21)
        Me.Label5.TabIndex = 258
        Me.Label5.Text = "DETERMINATION"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pnlGrant
        '
        Me.pnlGrant.BackColor = System.Drawing.Color.Turquoise
        Me.pnlGrant.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pnlGrant.Controls.Add(Me.ComboBox1)
        Me.pnlGrant.Controls.Add(Me.Label12)
        Me.pnlGrant.Controls.Add(Me.Label9)
        Me.pnlGrant.Controls.Add(Me.Label11)
        Me.pnlGrant.Controls.Add(Me.Label43)
        Me.pnlGrant.Controls.Add(Me.dtGrantAppRecd)
        Me.pnlGrant.Controls.Add(Me.CheckBox3)
        Me.pnlGrant.Controls.Add(Me.cboClassGroup)
        Me.pnlGrant.Controls.Add(Me.Label8)
        Me.pnlGrant.Controls.Add(Me.CheckBox2)
        Me.pnlGrant.Controls.Add(Me.dtGrantAppDeadline)
        Me.pnlGrant.Controls.Add(Me.Label42)
        Me.pnlGrant.Controls.Add(Me.Label1)
        Me.pnlGrant.Controls.Add(Me.dtClassConfirm)
        Me.pnlGrant.Controls.Add(Me.CheckBox1)
        Me.pnlGrant.Location = New System.Drawing.Point(435, 338)
        Me.pnlGrant.Name = "pnlGrant"
        Me.pnlGrant.Size = New System.Drawing.Size(432, 189)
        Me.pnlGrant.TabIndex = 259
        '
        'ComboBox1
        '
        Me.ComboBox1.DataBindings.Add(New System.Windows.Forms.Binding("SelectedItem", Me.BindingSource1, "Topic", True))
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Items.AddRange(New Object() {"Presentation Technologies", "Web Strategy"})
        Me.ComboBox1.Location = New System.Drawing.Point(73, 86)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(168, 21)
        Me.ComboBox1.TabIndex = 237
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.Transparent
        Me.Label12.Location = New System.Drawing.Point(2, 83)
        Me.Label12.Margin = New System.Windows.Forms.Padding(0)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(68, 24)
        Me.Label12.TabIndex = 238
        Me.Label12.Text = "Topic"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.ForeColor = System.Drawing.Color.Teal
        Me.Label9.Location = New System.Drawing.Point(287, 5)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(35, 13)
        Me.Label9.TabIndex = 236
        Me.Label9.Text = "Dates"
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.Transparent
        Me.Label11.Location = New System.Drawing.Point(244, 50)
        Me.Label11.Margin = New System.Windows.Forms.Padding(0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(64, 29)
        Me.Label11.TabIndex = 235
        Me.Label11.Text = "Attended Classes:"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label43
        '
        Me.Label43.BackColor = System.Drawing.Color.Transparent
        Me.Label43.Location = New System.Drawing.Point(114, 156)
        Me.Label43.Name = "Label43"
        Me.Label43.Size = New System.Drawing.Size(136, 20)
        Me.Label43.TabIndex = 8
        Me.Label43.Text = "Date Grant Draft Received"
        Me.Label43.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dtGrantAppRecd
        '
        Me.dtGrantAppRecd.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.BindingSource1, "GrantAppRecdDt", True))
        Me.dtGrantAppRecd.Location = New System.Drawing.Point(258, 156)
        Me.dtGrantAppRecd.Name = "dtGrantAppRecd"
        Me.dtGrantAppRecd.Size = New System.Drawing.Size(73, 20)
        Me.dtGrantAppRecd.TabIndex = 5
        Me.dtGrantAppRecd.Tag = ""
        Me.dtGrantAppRecd.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Location = New System.Drawing.Point(16, 130)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(234, 14)
        Me.Label8.TabIndex = 6
        Me.Label8.Text = "Grant Application Deadline (90 days after class)"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dtGrantAppDeadline
        '
        Me.dtGrantAppDeadline.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.BindingSource1, "GrantAppDeadlineDt", True))
        Me.dtGrantAppDeadline.Location = New System.Drawing.Point(258, 128)
        Me.dtGrantAppDeadline.Name = "dtGrantAppDeadline"
        Me.dtGrantAppDeadline.Size = New System.Drawing.Size(73, 20)
        Me.dtGrantAppDeadline.TabIndex = 6
        Me.dtGrantAppDeadline.TabStop = False
        Me.dtGrantAppDeadline.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label36
        '
        Me.Label36.Font = New System.Drawing.Font("Comic Sans MS", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label36.ForeColor = System.Drawing.Color.CadetBlue
        Me.Label36.Location = New System.Drawing.Point(501, 311)
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
        Me.Panel1.Location = New System.Drawing.Point(783, 13)
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
        Me.btnGrant.Text = "Goto Grant"
        Me.btnGrant.UseVisualStyleBackColor = False
        Me.btnGrant.Visible = False
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
        Me.btnHelp.Location = New System.Drawing.Point(894, 13)
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
        Me.Label23.Location = New System.Drawing.Point(738, 563)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(41, 13)
        Me.Label23.TabIndex = 427
        Me.Label23.Text = "LTGI #"
        '
        'fldGINum
        '
        Me.fldGINum.AutoSize = True
        Me.fldGINum.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.BindingSource1, "GIID", True))
        Me.fldGINum.ForeColor = System.Drawing.SystemColors.ControlDark
        Me.fldGINum.Location = New System.Drawing.Point(789, 563)
        Me.fldGINum.Name = "fldGINum"
        Me.fldGINum.Size = New System.Drawing.Size(45, 13)
        Me.fldGINum.TabIndex = 426
        Me.fldGINum.Text = "Label23"
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.ForeColor = System.Drawing.SystemColors.ControlDark
        Me.Label24.Location = New System.Drawing.Point(738, 541)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(35, 13)
        Me.Label24.TabIndex = 425
        Me.Label24.Text = "OrgID"
        '
        'fldOrgID
        '
        Me.fldOrgID.AutoSize = True
        Me.fldOrgID.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.BindingSource1, "OrgNum", True))
        Me.fldOrgID.ForeColor = System.Drawing.SystemColors.ControlDark
        Me.fldOrgID.Location = New System.Drawing.Point(789, 541)
        Me.fldOrgID.Name = "fldOrgID"
        Me.fldOrgID.Size = New System.Drawing.Size(45, 13)
        Me.fldOrgID.TabIndex = 424
        Me.fldOrgID.Text = "Label23"
        '
        'StatusBar1
        '
        Me.StatusBar1.Location = New System.Drawing.Point(0, 510)
        Me.StatusBar1.Name = "StatusBar1"
        Me.StatusBar1.Panels.AddRange(New System.Windows.Forms.StatusBarPanel() {Me.StatusBarPanel1, Me.StatusBarPanel2})
        Me.StatusBar1.ShowPanels = True
        Me.StatusBar1.Size = New System.Drawing.Size(984, 22)
        Me.StatusBar1.TabIndex = 429
        Me.StatusBar1.Text = "StatusBar1"
        '
        'StatusBarPanel1
        '
        Me.StatusBarPanel1.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents
        Me.StatusBarPanel1.Name = "StatusBarPanel1"
        Me.StatusBarPanel1.Width = 10
        '
        'StatusBarPanel2
        '
        Me.StatusBarPanel2.Alignment = System.Windows.Forms.HorizontalAlignment.Right
        Me.StatusBarPanel2.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring
        Me.StatusBarPanel2.Name = "StatusBarPanel2"
        Me.StatusBarPanel2.Text = "Use this window to change MGI App information."
        Me.StatusBarPanel2.Width = 957
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
        Me.miDelete.Text = "Delete Technology Grant"
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
        Me.fldGotoOrg.Location = New System.Drawing.Point(456, 5)
        Me.fldGotoOrg.Name = "fldGotoOrg"
        Me.fldGotoOrg.Size = New System.Drawing.Size(330, 20)
        Me.fldGotoOrg.TabIndex = 430
        Me.fldGotoOrg.Text = "goto Org"
        '
        'MainGrantTGITableAdapter
        '
        Me.MainGrantTGITableAdapter.ClearBeforeFill = True
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Font = New System.Drawing.Font("Cooper Black", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(18, 18)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(190, 24)
        Me.Label17.TabIndex = 436
        Me.Label17.Text = "Technology 2010"
        '
        'frmMainGrantTGI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(984, 532)
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
        Me.Name = "frmMainGrantTGI"
        Me.Text = "TGI APPLICATION DETAIL"
        Me.pnlApplication.ResumeLayout(False)
        Me.pnlApplication.PerformLayout()
        CType(Me.BindingSource1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsMainGrantTGI, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlProcess.ResumeLayout(False)
        Me.pnlProcess.PerformLayout()
        Me.pnlGrant.ResumeLayout(False)
        Me.pnlGrant.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        CType(Me.StatusBarPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.StatusBarPanel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents BindingSource1 As System.Windows.Forms.BindingSource
    Friend WithEvents pnlApplication As System.Windows.Forms.Panel
    Friend WithEvents cboClergyLeader As System.Windows.Forms.ComboBox
    Friend WithEvents cboPD As System.Windows.Forms.ComboBox
    Friend WithEvents dtAppRecd As System.Windows.Forms.TextBox
    Friend WithEvents txtRemarks As System.Windows.Forms.TextBox
    Friend WithEvents CheckBox8 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox9 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox4 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox5 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox6 As System.Windows.Forms.CheckBox
    Friend WithEvents Label38 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtTentative As System.Windows.Forms.TextBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents fldContact As System.Windows.Forms.Textbox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Label39 As System.Windows.Forms.Label
    Friend WithEvents dtAppReceiptMail As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents pnlProcess As System.Windows.Forms.Panel
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents dtAgreementMail As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents dtNotifyMail As System.Windows.Forms.TextBox
    Friend WithEvents cboCallNotified As System.Windows.Forms.ComboBox
    Private WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents dtDetermination As System.Windows.Forms.TextBox
    Friend WithEvents cboDeterminationStaff As System.Windows.Forms.ComboBox
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents dtNotifyCall As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents pnlGrant As System.Windows.Forms.Panel
    Friend WithEvents txtNotes As System.Windows.Forms.TextBox
    Friend WithEvents dtAgreementRecd As System.Windows.Forms.TextBox
    Friend WithEvents CheckBox3 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox2 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox1 As System.Windows.Forms.CheckBox
    Friend WithEvents Label43 As System.Windows.Forms.Label
    Friend WithEvents dtGrantAppRecd As System.Windows.Forms.TextBox
    Friend WithEvents Label42 As System.Windows.Forms.Label
    Friend WithEvents dtClassConfirm As System.Windows.Forms.TextBox
    Friend WithEvents Label41 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents dtGrantAppDeadline As System.Windows.Forms.TextBox
    Friend WithEvents Label36 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents btnSaveExit As System.Windows.Forms.Button
    Friend WithEvents btnHelp As System.Windows.Forms.Button
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents cboClassGroup As System.Windows.Forms.ComboBox
    Friend WithEvents CheckBox7 As System.Windows.Forms.CheckBox
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents fldGINum As System.Windows.Forms.Label
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents fldOrgID As System.Windows.Forms.Label
    Friend WithEvents DsMainGrantTGI As InfoCtr.dsMainGrantTGI
    Friend WithEvents MainGrantTGITableAdapter As InfoCtr.dsMainGrantTGITableAdapters.MainGrantTGITableAdapter
    Friend WithEvents cboDetermination As System.Windows.Forms.ComboBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents StatusBar1 As System.Windows.Forms.StatusBar
    Friend WithEvents StatusBarPanel1 As System.Windows.Forms.StatusBarPanel
    Friend WithEvents StatusBarPanel2 As System.Windows.Forms.StatusBarPanel
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
    Friend WithEvents cboCase As System.Windows.Forms.ComboBox
    Friend WithEvents fldGotoCase As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents ComboBox1 As System.Windows.Forms.ComboBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
End Class
