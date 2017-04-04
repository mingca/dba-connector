<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMainOrgAlert
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
        Dim StoryIDLabel As System.Windows.Forms.Label
        Dim OrgNumLabel As System.Windows.Forms.Label
        Dim CaseNumLabel As System.Windows.Forms.Label
        Dim CreateDateLabel As System.Windows.Forms.Label
        Dim CreateStaffNumLabel As System.Windows.Forms.Label
        Dim StoryTextLabel As System.Windows.Forms.Label
        Dim Label3 As System.Windows.Forms.Label
        Dim Label4 As System.Windows.Forms.Label
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMainOrgAlert))
        Me.SqlSelectCommand1 = New System.Data.SqlClient.SqlCommand()
        Me.SqlConnection1 = New System.Data.SqlClient.SqlConnection()
        Me.SqlUpdateCommand1 = New System.Data.SqlClient.SqlCommand()
        Me.daMainOrgAlert = New System.Data.SqlClient.SqlDataAdapter()
        Me.SqlDeleteCommand = New System.Data.SqlClient.SqlCommand()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.fldAlertID = New System.Windows.Forms.TextBox()
        Me.MainOrgAlertBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.DsMainOrgAlert1 = New InfoCtr.dsMainOrgAlert()
        Me.fldOrgNum = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtHeadline = New System.Windows.Forms.TextBox()
        Me.cboType = New System.Windows.Forms.ComboBox()
        Me.rtbDescription = New System.Windows.Forms.RichTextBox()
        Me.btnSpellcheck = New System.Windows.Forms.Button()
        Me.fldGotoOrg = New System.Windows.Forms.TextBox()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.btnSaveExit = New System.Windows.Forms.Button()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.MainMenu1 = New System.Windows.Forms.MainMenu(Me.components)
        Me.MenuItem1 = New System.Windows.Forms.MenuItem()
        Me.miClose = New System.Windows.Forms.MenuItem()
        Me.MenuItem2 = New System.Windows.Forms.MenuItem()
        Me.miSave = New System.Windows.Forms.MenuItem()
        Me.miCancel = New System.Windows.Forms.MenuItem()
        Me.miDelete = New System.Windows.Forms.MenuItem()
        Me.cboStaff = New System.Windows.Forms.ComboBox()
        Me.ErrorProvider1 = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cboCancelStaff = New System.Windows.Forms.ComboBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.DtCancel = New InfoCtr.DateTextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.DtCreate = New InfoCtr.DateTextBox()
        Me.StatusBar1 = New System.Windows.Forms.StatusBar()
        Me.StatusBarPanel1 = New System.Windows.Forms.StatusBarPanel()
        Me.StatusBarPanelID = New System.Windows.Forms.StatusBarPanel()
        Me.StatusBarPanel2 = New System.Windows.Forms.StatusBarPanel()
        StoryIDLabel = New System.Windows.Forms.Label()
        OrgNumLabel = New System.Windows.Forms.Label()
        CaseNumLabel = New System.Windows.Forms.Label()
        CreateDateLabel = New System.Windows.Forms.Label()
        CreateStaffNumLabel = New System.Windows.Forms.Label()
        StoryTextLabel = New System.Windows.Forms.Label()
        Label3 = New System.Windows.Forms.Label()
        Label4 = New System.Windows.Forms.Label()
        CType(Me.MainOrgAlertBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsMainOrgAlert1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.StatusBarPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.StatusBarPanelID, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.StatusBarPanel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'StoryIDLabel
        '
        StoryIDLabel.AutoSize = True
        StoryIDLabel.Enabled = False
        StoryIDLabel.ForeColor = System.Drawing.SystemColors.ControlDark
        StoryIDLabel.Location = New System.Drawing.Point(697, 465)
        StoryIDLabel.Name = "StoryIDLabel"
        StoryIDLabel.Size = New System.Drawing.Size(45, 13)
        StoryIDLabel.TabIndex = 261
        StoryIDLabel.Text = "Alert ID:"
        '
        'OrgNumLabel
        '
        OrgNumLabel.AutoSize = True
        OrgNumLabel.Enabled = False
        OrgNumLabel.ForeColor = System.Drawing.SystemColors.ControlDark
        OrgNumLabel.Location = New System.Drawing.Point(697, 491)
        OrgNumLabel.Name = "OrgNumLabel"
        OrgNumLabel.Size = New System.Drawing.Size(52, 13)
        OrgNumLabel.TabIndex = 263
        OrgNumLabel.Text = "Org Num:"
        '
        'CaseNumLabel
        '
        CaseNumLabel.AutoSize = True
        CaseNumLabel.ForeColor = System.Drawing.SystemColors.ControlText
        CaseNumLabel.Location = New System.Drawing.Point(41, 139)
        CaseNumLabel.Name = "CaseNumLabel"
        CaseNumLabel.Size = New System.Drawing.Size(31, 13)
        CaseNumLabel.TabIndex = 256
        CaseNumLabel.Text = "Type"
        '
        'CreateDateLabel
        '
        CreateDateLabel.AutoSize = True
        CreateDateLabel.Location = New System.Drawing.Point(43, 163)
        CreateDateLabel.Name = "CreateDateLabel"
        CreateDateLabel.Size = New System.Drawing.Size(30, 13)
        CreateDateLabel.TabIndex = 257
        CreateDateLabel.Text = "Date"
        '
        'CreateStaffNumLabel
        '
        CreateStaffNumLabel.AutoSize = True
        CreateStaffNumLabel.Location = New System.Drawing.Point(44, 192)
        CreateStaffNumLabel.Name = "CreateStaffNumLabel"
        CreateStaffNumLabel.Size = New System.Drawing.Size(29, 13)
        CreateStaffNumLabel.TabIndex = 258
        CreateStaffNumLabel.Text = "Staff"
        '
        'StoryTextLabel
        '
        StoryTextLabel.AutoSize = True
        StoryTextLabel.Location = New System.Drawing.Point(371, 79)
        StoryTextLabel.Name = "StoryTextLabel"
        StoryTextLabel.Size = New System.Drawing.Size(60, 13)
        StoryTextLabel.TabIndex = 259
        StoryTextLabel.Text = "Description"
        '
        'Label3
        '
        Label3.AutoSize = True
        Label3.Location = New System.Drawing.Point(9, 52)
        Label3.Name = "Label3"
        Label3.Size = New System.Drawing.Size(66, 13)
        Label3.TabIndex = 269
        Label3.Text = "Cancel Date"
        '
        'Label4
        '
        Label4.AutoSize = True
        Label4.Location = New System.Drawing.Point(10, 81)
        Label4.Name = "Label4"
        Label4.Size = New System.Drawing.Size(65, 13)
        Label4.TabIndex = 270
        Label4.Text = "Cancel Staff"
        '
        'SqlSelectCommand1
        '
        Me.SqlSelectCommand1.CommandText = "dbo.MainOrgAlert"
        Me.SqlSelectCommand1.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlSelectCommand1.Connection = Me.SqlConnection1
        Me.SqlSelectCommand1.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, False, CType(0, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing), New System.Data.SqlClient.SqlParameter("@ID", System.Data.SqlDbType.Int, 4)})
        '
        'SqlConnection1
        '
        Me.SqlConnection1.ConnectionString = "Data Source=SOLOMON2008\SOLOMON2008;Initial Catalog=InfoCtr_be;Integrated Securit" & _
    "y=True"
        Me.SqlConnection1.FireInfoMessageEventOnUserErrors = False
        '
        'SqlUpdateCommand1
        '
        Me.SqlUpdateCommand1.CommandText = "dbo.MainOrgAlertUpdate"
        Me.SqlUpdateCommand1.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlUpdateCommand1.Connection = Me.SqlConnection1
        Me.SqlUpdateCommand1.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, False, CType(0, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing), New System.Data.SqlClient.SqlParameter("@ID", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "AlertID", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@OrgNum", System.Data.SqlDbType.Int, 4, "OrgNum"), New System.Data.SqlClient.SqlParameter("@Type", System.Data.SqlDbType.VarChar, 50, "Type"), New System.Data.SqlClient.SqlParameter("@Headline", System.Data.SqlDbType.VarChar, 100, "Headline"), New System.Data.SqlClient.SqlParameter("@Description", System.Data.SqlDbType.VarChar, 2147483647, "Description"), New System.Data.SqlClient.SqlParameter("@CreateDate", System.Data.SqlDbType.[Date], 3, "CreateDate"), New System.Data.SqlClient.SqlParameter("@StaffNum", System.Data.SqlDbType.SmallInt, 2, "StaffNum"), New System.Data.SqlClient.SqlParameter("@CancelStaffNum", System.Data.SqlDbType.SmallInt, 2, "CancelStaffNum"), New System.Data.SqlClient.SqlParameter("@CancelDate", System.Data.SqlDbType.[Date], 3, "CancelDate"), New System.Data.SqlClient.SqlParameter("@CancelWhy", System.Data.SqlDbType.Text, 2147483647, "CancelExplanation")})
        '
        'daMainOrgAlert
        '
        Me.daMainOrgAlert.DeleteCommand = Me.SqlDeleteCommand
        Me.daMainOrgAlert.SelectCommand = Me.SqlSelectCommand1
        Me.daMainOrgAlert.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "MainOrgAlert", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("AlertID", "AlertID"), New System.Data.Common.DataColumnMapping("OrgNum", "OrgNum"), New System.Data.Common.DataColumnMapping("Type", "Type"), New System.Data.Common.DataColumnMapping("Headline", "Headline"), New System.Data.Common.DataColumnMapping("Description", "Description"), New System.Data.Common.DataColumnMapping("CreateDate", "CreateDate"), New System.Data.Common.DataColumnMapping("StaffNum", "StaffNum"), New System.Data.Common.DataColumnMapping("Stamped", "Stamped"), New System.Data.Common.DataColumnMapping("CancelDate", "CancelDate"), New System.Data.Common.DataColumnMapping("CancelStaffNum", "CancelStaffNum"), New System.Data.Common.DataColumnMapping("CancelExplanation", "CancelExplanation")})})
        Me.daMainOrgAlert.UpdateCommand = Me.SqlUpdateCommand1
        '
        'SqlDeleteCommand
        '
        Me.SqlDeleteCommand.CommandText = "dbo.MainOrgAlertDelete"
        Me.SqlDeleteCommand.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlDeleteCommand.Connection = Me.SqlConnection1
        Me.SqlDeleteCommand.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, False, CType(0, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing), New System.Data.SqlClient.SqlParameter("@ID", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "AlertID", System.Data.DataRowVersion.Original, Nothing)})
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label2.Location = New System.Drawing.Point(10, 10)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(228, 16)
        Me.Label2.TabIndex = 265
        Me.Label2.Text = "Organization ALERT DETAIL"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'fldAlertID
        '
        Me.fldAlertID.BackColor = System.Drawing.SystemColors.Control
        Me.fldAlertID.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainOrgAlertBindingSource, "AlertID", True))
        Me.fldAlertID.Enabled = False
        Me.fldAlertID.Location = New System.Drawing.Point(755, 462)
        Me.fldAlertID.Name = "fldAlertID"
        Me.fldAlertID.Size = New System.Drawing.Size(58, 20)
        Me.fldAlertID.TabIndex = 262
        '
        'MainOrgAlertBindingSource
        '
        Me.MainOrgAlertBindingSource.DataMember = "MainOrgAlert"
        Me.MainOrgAlertBindingSource.DataSource = Me.DsMainOrgAlert1
        '
        'DsMainOrgAlert1
        '
        Me.DsMainOrgAlert1.DataSetName = "dsMainOrgAlert"
        Me.DsMainOrgAlert1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'fldOrgNum
        '
        Me.fldOrgNum.BackColor = System.Drawing.SystemColors.Control
        Me.fldOrgNum.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainOrgAlertBindingSource, "OrgNum", True))
        Me.fldOrgNum.Enabled = False
        Me.fldOrgNum.Location = New System.Drawing.Point(755, 488)
        Me.fldOrgNum.Name = "fldOrgNum"
        Me.fldOrgNum.Size = New System.Drawing.Size(58, 20)
        Me.fldOrgNum.TabIndex = 264
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(23, 98)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(49, 13)
        Me.Label1.TabIndex = 260
        Me.Label1.Text = "Headline"
        '
        'txtHeadline
        '
        Me.txtHeadline.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainOrgAlertBindingSource, "Headline", True))
        Me.txtHeadline.Location = New System.Drawing.Point(78, 95)
        Me.txtHeadline.MinimumSize = New System.Drawing.Size(4, 35)
        Me.txtHeadline.Multiline = True
        Me.txtHeadline.Name = "txtHeadline"
        Me.txtHeadline.Size = New System.Drawing.Size(242, 35)
        Me.txtHeadline.TabIndex = 0
        Me.txtHeadline.Tag = "Headline"
        '
        'cboType
        '
        Me.cboType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cboType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboType.DataBindings.Add(New System.Windows.Forms.Binding("SelectedItem", Me.MainOrgAlertBindingSource, "Type", True))
        Me.cboType.FormattingEnabled = True
        Me.cboType.Items.AddRange(New Object() {"Center issue", "Other issue"})
        Me.cboType.Location = New System.Drawing.Point(78, 136)
        Me.cboType.Name = "cboType"
        Me.cboType.Size = New System.Drawing.Size(183, 21)
        Me.cboType.TabIndex = 1
        Me.cboType.Tag = "Type"
        '
        'rtbDescription
        '
        Me.rtbDescription.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainOrgAlertBindingSource, "Description", True))
        Me.rtbDescription.Location = New System.Drawing.Point(374, 95)
        Me.rtbDescription.Name = "rtbDescription"
        Me.rtbDescription.Size = New System.Drawing.Size(455, 312)
        Me.rtbDescription.TabIndex = 4
        Me.rtbDescription.Tag = "Description"
        Me.rtbDescription.Text = ""
        '
        'btnSpellcheck
        '
        Me.btnSpellcheck.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnSpellcheck.Location = New System.Drawing.Point(641, 64)
        Me.btnSpellcheck.Name = "btnSpellcheck"
        Me.btnSpellcheck.Size = New System.Drawing.Size(72, 25)
        Me.btnSpellcheck.TabIndex = 255
        Me.btnSpellcheck.Text = "Spellcheck"
        Me.btnSpellcheck.UseVisualStyleBackColor = True
        Me.btnSpellcheck.Visible = False
        '
        'fldGotoOrg
        '
        Me.fldGotoOrg.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fldGotoOrg.ForeColor = System.Drawing.Color.ForestGreen
        Me.fldGotoOrg.Location = New System.Drawing.Point(374, 5)
        Me.fldGotoOrg.Multiline = True
        Me.fldGotoOrg.Name = "fldGotoOrg"
        Me.fldGotoOrg.Size = New System.Drawing.Size(330, 45)
        Me.fldGotoOrg.TabIndex = 254
        Me.fldGotoOrg.Text = "should be org name"
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.SystemColors.Highlight
        Me.Panel2.Controls.Add(Me.btnSaveExit)
        Me.Panel2.Controls.Add(Me.btnDelete)
        Me.Panel2.Location = New System.Drawing.Point(770, 5)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(88, 40)
        Me.Panel2.TabIndex = 266
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
        'btnDelete
        '
        Me.btnDelete.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.btnDelete.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Image = Global.InfoCtr.My.Resources.Resources.btnDelete
        Me.btnDelete.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnDelete.Location = New System.Drawing.Point(3, 1)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(40, 35)
        Me.btnDelete.TabIndex = 202
        Me.btnDelete.Text = "Delete"
        Me.btnDelete.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnDelete.UseVisualStyleBackColor = False
        Me.btnDelete.Visible = False
        '
        'MainMenu1
        '
        Me.MainMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem1, Me.MenuItem2})
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
        Me.miDelete.Text = "Delete Alert"
        '
        'cboStaff
        '
        Me.cboStaff.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboStaff.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboStaff.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.MainOrgAlertBindingSource, "StaffNum", True))
        Me.cboStaff.FormattingEnabled = True
        Me.cboStaff.Location = New System.Drawing.Point(78, 189)
        Me.cboStaff.Name = "cboStaff"
        Me.cboStaff.Size = New System.Drawing.Size(180, 21)
        Me.cboStaff.TabIndex = 3
        Me.cboStaff.Tag = "Staff"
        '
        'ErrorProvider1
        '
        Me.ErrorProvider1.ContainerControl = Me
        '
        'ToolTip1
        '
        Me.ToolTip1.ToolTipTitle = "Brief summary of the content of the Alert/Warning"
        '
        'cboCancelStaff
        '
        Me.cboCancelStaff.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboCancelStaff.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboCancelStaff.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.MainOrgAlertBindingSource, "CancelStaffNum", True))
        Me.cboCancelStaff.FormattingEnabled = True
        Me.cboCancelStaff.Location = New System.Drawing.Point(81, 78)
        Me.cboCancelStaff.Name = "cboCancelStaff"
        Me.cboCancelStaff.Size = New System.Drawing.Size(180, 21)
        Me.cboCancelStaff.TabIndex = 268
        Me.cboCancelStaff.Tag = "Staff"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.DtCancel)
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.TextBox1)
        Me.Panel1.Controls.Add(Me.cboCancelStaff)
        Me.Panel1.Controls.Add(Label3)
        Me.Panel1.Controls.Add(Label4)
        Me.Panel1.Location = New System.Drawing.Point(14, 234)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(305, 274)
        Me.Panel1.TabIndex = 271
        '
        'DtCancel
        '
        Me.DtCancel.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainOrgAlertBindingSource, "CancelDate", True))
        Me.DtCancel.Location = New System.Drawing.Point(81, 49)
        Me.DtCancel.Name = "DtCancel"
        Me.DtCancel.Size = New System.Drawing.Size(180, 20)
        Me.DtCancel.TabIndex = 274
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Red
        Me.Label6.Location = New System.Drawing.Point(46, 9)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(178, 14)
        Me.Label6.TabIndex = 273
        Me.Label6.Text = "ALERT CANCELLED"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.Label6.Visible = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(3, 115)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(98, 13)
        Me.Label5.TabIndex = 272
        Me.Label5.Text = "Cancel Explanation"
        '
        'TextBox1
        '
        Me.TextBox1.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainOrgAlertBindingSource, "CancelExplanation", True))
        Me.TextBox1.Location = New System.Drawing.Point(19, 131)
        Me.TextBox1.MinimumSize = New System.Drawing.Size(4, 35)
        Me.TextBox1.Multiline = True
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(242, 112)
        Me.TextBox1.TabIndex = 271
        Me.TextBox1.Tag = "Headline"
        '
        'DtCreate
        '
        Me.DtCreate.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainOrgAlertBindingSource, "CreateDate", True))
        Me.DtCreate.Location = New System.Drawing.Point(78, 163)
        Me.DtCreate.Name = "DtCreate"
        Me.DtCreate.Size = New System.Drawing.Size(182, 20)
        Me.DtCreate.TabIndex = 272
        '
        'StatusBar1
        '
        Me.StatusBar1.Location = New System.Drawing.Point(0, 531)
        Me.StatusBar1.Name = "StatusBar1"
        Me.StatusBar1.Panels.AddRange(New System.Windows.Forms.StatusBarPanel() {Me.StatusBarPanel1, Me.StatusBarPanelID, Me.StatusBarPanel2})
        Me.StatusBar1.ShowPanels = True
        Me.StatusBar1.Size = New System.Drawing.Size(984, 22)
        Me.StatusBar1.TabIndex = 273
        Me.StatusBar1.Text = "StatusBar1"
        '
        'StatusBarPanel1
        '
        Me.StatusBarPanel1.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents
        Me.StatusBarPanel1.MinWidth = 200
        Me.StatusBarPanel1.Name = "StatusBarPanel1"
        Me.StatusBarPanel1.Text = "StatusBarPanel1"
        Me.StatusBarPanel1.Width = 200
        '
        'StatusBarPanelID
        '
        Me.StatusBarPanelID.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents
        Me.StatusBarPanelID.Name = "StatusBarPanelID"
        Me.StatusBarPanelID.Text = "Organization Alert: "
        Me.StatusBarPanelID.Width = 108
        '
        'StatusBarPanel2
        '
        Me.StatusBarPanel2.Alignment = System.Windows.Forms.HorizontalAlignment.Right
        Me.StatusBarPanel2.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring
        Me.StatusBarPanel2.Name = "StatusBarPanel2"
        Me.StatusBarPanel2.Text = "Use this window to edit Organization Alerts."
        Me.StatusBarPanel2.Width = 659
        '
        'frmMainOrgAlert
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(984, 553)
        Me.Controls.Add(Me.StatusBar1)
        Me.Controls.Add(Me.DtCreate)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.cboStaff)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(StoryIDLabel)
        Me.Controls.Add(Me.fldAlertID)
        Me.Controls.Add(OrgNumLabel)
        Me.Controls.Add(Me.fldOrgNum)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtHeadline)
        Me.Controls.Add(Me.cboType)
        Me.Controls.Add(CaseNumLabel)
        Me.Controls.Add(CreateDateLabel)
        Me.Controls.Add(CreateStaffNumLabel)
        Me.Controls.Add(StoryTextLabel)
        Me.Controls.Add(Me.rtbDescription)
        Me.Controls.Add(Me.btnSpellcheck)
        Me.Controls.Add(Me.fldGotoOrg)
        Me.Menu = Me.MainMenu1
        Me.Name = "frmMainOrgAlert"
        Me.Tag = "ORGANIZATION ALERT"
        Me.Text = "ORGANIZATION ALERT"
        CType(Me.MainOrgAlertBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsMainOrgAlert1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.StatusBarPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.StatusBarPanelID, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.StatusBarPanel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents SqlSelectCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlUpdateCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents daMainOrgAlert As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents DsMainOrgAlert1 As InfoCtr.dsMainOrgAlert
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents fldAlertID As System.Windows.Forms.TextBox
    Friend WithEvents fldOrgNum As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtHeadline As System.Windows.Forms.TextBox
    Friend WithEvents cboType As System.Windows.Forms.ComboBox
    Friend WithEvents rtbDescription As System.Windows.Forms.RichTextBox
    Friend WithEvents btnSpellcheck As System.Windows.Forms.Button
    Friend WithEvents fldGotoOrg As System.Windows.Forms.TextBox
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents btnSaveExit As System.Windows.Forms.Button
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents MainMenu1 As System.Windows.Forms.MainMenu
    Friend WithEvents MenuItem1 As System.Windows.Forms.MenuItem
    Friend WithEvents miClose As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem2 As System.Windows.Forms.MenuItem
    Friend WithEvents miSave As System.Windows.Forms.MenuItem
    Friend WithEvents miCancel As System.Windows.Forms.MenuItem
    Friend WithEvents miDelete As System.Windows.Forms.MenuItem
    Friend WithEvents cboStaff As System.Windows.Forms.ComboBox
    Friend WithEvents ErrorProvider1 As System.Windows.Forms.ErrorProvider
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents SqlConnection1 As System.Data.SqlClient.SqlConnection
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents cboCancelStaff As System.Windows.Forms.ComboBox
    Friend WithEvents StatusBar1 As System.Windows.Forms.StatusBar
    Friend WithEvents StatusBarPanel1 As System.Windows.Forms.StatusBarPanel
    Friend WithEvents StatusBarPanel2 As System.Windows.Forms.StatusBarPanel
    Friend WithEvents MainOrgAlertBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents StatusBarPanelID As System.Windows.Forms.StatusBarPanel
    Friend WithEvents SqlDeleteCommand As System.Data.SqlClient.SqlCommand
    Friend WithEvents DtCreate As InfoCtr.DateTextBox
    Friend WithEvents DtCancel As InfoCtr.DateTextBox
End Class
