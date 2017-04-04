<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMainOrgStory
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
        Dim CreateDateLabel As System.Windows.Forms.Label
        Dim CreateStaffNumLabel As System.Windows.Forms.Label
        Dim StoryTextLabel As System.Windows.Forms.Label
        Dim CaseNumLabel As System.Windows.Forms.Label
        Dim StoryIDLabel As System.Windows.Forms.Label
        Dim OrgNumLabel As System.Windows.Forms.Label
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMainOrgStory))
        Me.btnSpellcheck = New System.Windows.Forms.Button()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.btnSaveExit = New System.Windows.Forms.Button()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.fldGotoOrg = New System.Windows.Forms.TextBox()
        Me.rtbStory = New System.Windows.Forms.RichTextBox()
        Me.MainOrgStoryBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.DsMainStory = New InfoCtr.dsMainStory()
        Me.cboCase = New InfoCtr.ComboBoxRelaxed()
        Me.DsStoryCase1 = New InfoCtr.dsStoryCase()
        Me.cboStaff = New InfoCtr.ComboBoxRelaxed()
        Me.DtCreate = New InfoCtr.DateTextBox()
        Me.txtHeadline = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.MainMenu1 = New System.Windows.Forms.MainMenu(Me.components)
        Me.MenuItem1 = New System.Windows.Forms.MenuItem()
        Me.miClose = New System.Windows.Forms.MenuItem()
        Me.miEdit = New System.Windows.Forms.MenuItem()
        Me.miSave = New System.Windows.Forms.MenuItem()
        Me.miCancel = New System.Windows.Forms.MenuItem()
        Me.miDelete = New System.Windows.Forms.MenuItem()
        Me.StoryIDTextBox = New System.Windows.Forms.TextBox()
        Me.OrgNumTextBox = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.sqlCaseNames = New System.Data.SqlClient.SqlCommand()
        Me.daspStoryCases = New System.Data.SqlClient.SqlDataAdapter()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.Button1 = New System.Windows.Forms.Button()
        Me.ErrorProvider1 = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.daMainOrgStory = New System.Data.SqlClient.SqlDataAdapter()
        Me.SqlDeleteCommand = New System.Data.SqlClient.SqlCommand()
        Me.SqlConnection1 = New System.Data.SqlClient.SqlConnection()
        Me.SqlSelectCommand1 = New System.Data.SqlClient.SqlCommand()
        Me.SqlUpdateCommand1 = New System.Data.SqlClient.SqlCommand()
        Me.StatusBar1 = New System.Windows.Forms.StatusBar()
        Me.StatusBarPanel1 = New System.Windows.Forms.StatusBarPanel()
        Me.StatusBarPanelID = New System.Windows.Forms.StatusBarPanel()
        Me.StatusBarPanel2 = New System.Windows.Forms.StatusBarPanel()
        CreateDateLabel = New System.Windows.Forms.Label()
        CreateStaffNumLabel = New System.Windows.Forms.Label()
        StoryTextLabel = New System.Windows.Forms.Label()
        CaseNumLabel = New System.Windows.Forms.Label()
        StoryIDLabel = New System.Windows.Forms.Label()
        OrgNumLabel = New System.Windows.Forms.Label()
        Me.Panel2.SuspendLayout()
        CType(Me.MainOrgStoryBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsMainStory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsStoryCase1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.StatusBarPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.StatusBarPanelID, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.StatusBarPanel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'CreateDateLabel
        '
        CreateDateLabel.AutoSize = True
        CreateDateLabel.Location = New System.Drawing.Point(34, 150)
        CreateDateLabel.Name = "CreateDateLabel"
        CreateDateLabel.Size = New System.Drawing.Size(67, 13)
        CreateDateLabel.TabIndex = 232
        CreateDateLabel.Text = "Create Date:"
        '
        'CreateStaffNumLabel
        '
        CreateStaffNumLabel.AutoSize = True
        CreateStaffNumLabel.Location = New System.Drawing.Point(27, 187)
        CreateStaffNumLabel.Name = "CreateStaffNumLabel"
        CreateStaffNumLabel.Size = New System.Drawing.Size(74, 13)
        CreateStaffNumLabel.TabIndex = 234
        CreateStaffNumLabel.Text = "Staff Entering:"
        '
        'StoryTextLabel
        '
        StoryTextLabel.AutoSize = True
        StoryTextLabel.Location = New System.Drawing.Point(332, 85)
        StoryTextLabel.Name = "StoryTextLabel"
        StoryTextLabel.Size = New System.Drawing.Size(58, 13)
        StoryTextLabel.TabIndex = 236
        StoryTextLabel.Text = "Story Text:"
        '
        'CaseNumLabel
        '
        CaseNumLabel.AutoSize = True
        CaseNumLabel.ForeColor = System.Drawing.SystemColors.ControlText
        CaseNumLabel.Location = New System.Drawing.Point(27, 225)
        CaseNumLabel.Name = "CaseNumLabel"
        CaseNumLabel.Size = New System.Drawing.Size(79, 13)
        CaseNumLabel.TabIndex = 230
        CaseNumLabel.Text = "Case (Optional)"
        '
        'StoryIDLabel
        '
        StoryIDLabel.AutoSize = True
        StoryIDLabel.Enabled = False
        StoryIDLabel.ForeColor = System.Drawing.SystemColors.ControlDark
        StoryIDLabel.Location = New System.Drawing.Point(862, 15)
        StoryIDLabel.Name = "StoryIDLabel"
        StoryIDLabel.Size = New System.Drawing.Size(48, 13)
        StoryIDLabel.TabIndex = 242
        StoryIDLabel.Text = "Story ID:"
        '
        'OrgNumLabel
        '
        OrgNumLabel.AutoSize = True
        OrgNumLabel.Enabled = False
        OrgNumLabel.ForeColor = System.Drawing.SystemColors.ControlDark
        OrgNumLabel.Location = New System.Drawing.Point(862, 41)
        OrgNumLabel.Name = "OrgNumLabel"
        OrgNumLabel.Size = New System.Drawing.Size(52, 13)
        OrgNumLabel.TabIndex = 244
        OrgNumLabel.Text = "Org Num:"
        '
        'btnSpellcheck
        '
        Me.btnSpellcheck.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnSpellcheck.Location = New System.Drawing.Point(764, 70)
        Me.btnSpellcheck.Name = "btnSpellcheck"
        Me.btnSpellcheck.Size = New System.Drawing.Size(72, 25)
        Me.btnSpellcheck.TabIndex = 224
        Me.btnSpellcheck.Text = "Spellcheck"
        Me.btnSpellcheck.UseVisualStyleBackColor = True
        Me.btnSpellcheck.Visible = False
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.SystemColors.Highlight
        Me.Panel2.Controls.Add(Me.btnSaveExit)
        Me.Panel2.Controls.Add(Me.btnDelete)
        Me.Panel2.Location = New System.Drawing.Point(768, 5)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(88, 40)
        Me.Panel2.TabIndex = 223
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
        'fldGotoOrg
        '
        Me.fldGotoOrg.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fldGotoOrg.ForeColor = System.Drawing.Color.ForestGreen
        Me.fldGotoOrg.Location = New System.Drawing.Point(335, 5)
        Me.fldGotoOrg.Multiline = True
        Me.fldGotoOrg.Name = "fldGotoOrg"
        Me.fldGotoOrg.Size = New System.Drawing.Size(330, 45)
        Me.fldGotoOrg.TabIndex = 222
        Me.fldGotoOrg.Text = "should be org name"
        '
        'rtbStory
        '
        Me.rtbStory.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainOrgStoryBindingSource, "StoryText", True))
        Me.rtbStory.Location = New System.Drawing.Point(335, 101)
        Me.rtbStory.Name = "rtbStory"
        Me.rtbStory.Size = New System.Drawing.Size(501, 429)
        Me.rtbStory.TabIndex = 4
        Me.rtbStory.Text = ""
        Me.ToolTip1.SetToolTip(Me.rtbStory, "Right-click for text editing options.")
        '
        'MainOrgStoryBindingSource
        '
        Me.MainOrgStoryBindingSource.DataMember = "MainOrgStory"
        Me.MainOrgStoryBindingSource.DataSource = Me.DsMainStory
        '
        'DsMainStory
        '
        Me.DsMainStory.DataSetName = "dsMainStory"
        Me.DsMainStory.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'cboCase
        '
        Me.cboCase.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboCase.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboCase.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.MainOrgStoryBindingSource, "CaseNum", True))
        Me.cboCase.DataSource = Me.DsStoryCase1.luCaseNames
        Me.cboCase.DisplayMember = "CaseName"
        Me.cboCase.FormattingEnabled = True
        Me.cboCase.Location = New System.Drawing.Point(113, 225)
        Me.cboCase.Name = "cboCase"
        Me.cboCase.RestrictContentToListItems = True
        Me.cboCase.Size = New System.Drawing.Size(196, 21)
        Me.cboCase.TabIndex = 3
        Me.cboCase.ValueMember = "CaseID"
        '
        'DsStoryCase1
        '
        Me.DsStoryCase1.DataSetName = "dsStoryCase"
        Me.DsStoryCase1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'cboStaff
        '
        Me.cboStaff.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboStaff.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboStaff.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.MainOrgStoryBindingSource, "CreateStaffNum", True))
        Me.cboStaff.FormattingEnabled = True
        Me.cboStaff.Location = New System.Drawing.Point(127, 184)
        Me.cboStaff.Name = "cboStaff"
        Me.cboStaff.RestrictContentToListItems = True
        Me.cboStaff.Size = New System.Drawing.Size(183, 21)
        Me.cboStaff.TabIndex = 2
        Me.cboStaff.Tag = "Staff"
        '
        'DtCreate
        '
        Me.DtCreate.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainOrgStoryBindingSource, "CreateDate", True))
        Me.DtCreate.Location = New System.Drawing.Point(126, 153)
        Me.DtCreate.Name = "DtCreate"
        Me.DtCreate.Size = New System.Drawing.Size(183, 20)
        Me.DtCreate.TabIndex = 1
        '
        'txtHeadline
        '
        Me.txtHeadline.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainOrgStoryBindingSource, "Headline", True))
        Me.txtHeadline.Location = New System.Drawing.Point(67, 101)
        Me.txtHeadline.MinimumSize = New System.Drawing.Size(4, 35)
        Me.txtHeadline.Multiline = True
        Me.txtHeadline.Name = "txtHeadline"
        Me.txtHeadline.Size = New System.Drawing.Size(242, 35)
        Me.txtHeadline.TabIndex = 0
        Me.txtHeadline.Tag = "Story Headline"
        Me.ToolTip1.SetToolTip(Me.txtHeadline, "Brief summary of the story")
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 104)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(49, 13)
        Me.Label1.TabIndex = 241
        Me.Label1.Text = "Headline"
        '
        'MainMenu1
        '
        Me.MainMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem1, Me.miEdit})
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
        'miEdit
        '
        Me.miEdit.Index = 1
        Me.miEdit.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.miSave, Me.miCancel, Me.miDelete})
        Me.miEdit.Text = "Edit"
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
        Me.miDelete.Text = "Delete Story"
        '
        'StoryIDTextBox
        '
        Me.StoryIDTextBox.BackColor = System.Drawing.SystemColors.Control
        Me.StoryIDTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainOrgStoryBindingSource, "StoryID", True))
        Me.StoryIDTextBox.Enabled = False
        Me.StoryIDTextBox.Location = New System.Drawing.Point(920, 12)
        Me.StoryIDTextBox.Name = "StoryIDTextBox"
        Me.StoryIDTextBox.Size = New System.Drawing.Size(58, 20)
        Me.StoryIDTextBox.TabIndex = 243
        '
        'OrgNumTextBox
        '
        Me.OrgNumTextBox.BackColor = System.Drawing.SystemColors.Control
        Me.OrgNumTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainOrgStoryBindingSource, "OrgNum", True))
        Me.OrgNumTextBox.Enabled = False
        Me.OrgNumTextBox.Location = New System.Drawing.Point(920, 38)
        Me.OrgNumTextBox.Name = "OrgNumTextBox"
        Me.OrgNumTextBox.Size = New System.Drawing.Size(58, 20)
        Me.OrgNumTextBox.TabIndex = 245
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label2.Location = New System.Drawing.Point(10, 10)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(238, 18)
        Me.Label2.TabIndex = 246
        Me.Label2.Text = "Organization STORY DETAIL"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'sqlCaseNames
        '
        Me.sqlCaseNames.CommandText = "dbo.luCaseNames"
        Me.sqlCaseNames.CommandType = System.Data.CommandType.StoredProcedure
        Me.sqlCaseNames.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, False, CType(0, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing), New System.Data.SqlClient.SqlParameter("@OrgID", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, "2300")})
        '
        'daspStoryCases
        '
        Me.daspStoryCases.SelectCommand = Me.sqlCaseNames
        Me.daspStoryCases.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "luCaseNames", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("CaseID", "CaseID"), New System.Data.Common.DataColumnMapping("CaseName", "CaseName"), New System.Data.Common.DataColumnMapping("OrgNum", "OrgNum"), New System.Data.Common.DataColumnMapping("StatusName", "StatusName")})})
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(105, 363)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(97, 24)
        Me.Button1.TabIndex = 247
        Me.Button1.Text = "Button1"
        Me.Button1.UseVisualStyleBackColor = True
        Me.Button1.Visible = False
        '
        'ErrorProvider1
        '
        Me.ErrorProvider1.ContainerControl = Me
        '
        'daMainOrgStory
        '
        Me.daMainOrgStory.DeleteCommand = Me.SqlDeleteCommand
        Me.daMainOrgStory.SelectCommand = Me.SqlSelectCommand1
        Me.daMainOrgStory.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "MainOrgStory", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("StoryID", "StoryID"), New System.Data.Common.DataColumnMapping("OrgNum", "OrgNum"), New System.Data.Common.DataColumnMapping("CaseNum", "CaseNum"), New System.Data.Common.DataColumnMapping("CreateDate", "CreateDate"), New System.Data.Common.DataColumnMapping("CreateStaffNum", "CreateStaffNum"), New System.Data.Common.DataColumnMapping("Headline", "Headline"), New System.Data.Common.DataColumnMapping("StoryText", "StoryText"), New System.Data.Common.DataColumnMapping("EnteredBy", "EnteredBy"), New System.Data.Common.DataColumnMapping("Stamped", "Stamped")})})
        Me.daMainOrgStory.UpdateCommand = Me.SqlUpdateCommand1
        '
        'SqlDeleteCommand
        '
        Me.SqlDeleteCommand.CommandText = "dbo.MainOrgStoryDelete"
        Me.SqlDeleteCommand.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlDeleteCommand.Connection = Me.SqlConnection1
        Me.SqlDeleteCommand.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, False, CType(0, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing), New System.Data.SqlClient.SqlParameter("@ID", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "StoryID", System.Data.DataRowVersion.Original, Nothing)})
        '
        'SqlConnection1
        '
        Me.SqlConnection1.ConnectionString = "Data Source=SOLOMON2008\SOLOMON2008;Initial Catalog=InfoCtr_be;Integrated Securit" & _
    "y=True"
        Me.SqlConnection1.FireInfoMessageEventOnUserErrors = False
        '
        'SqlSelectCommand1
        '
        Me.SqlSelectCommand1.CommandText = "dbo.MainOrgStory"
        Me.SqlSelectCommand1.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlSelectCommand1.Connection = Me.SqlConnection1
        Me.SqlSelectCommand1.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, False, CType(0, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing), New System.Data.SqlClient.SqlParameter("@ID", System.Data.SqlDbType.Int, 4)})
        '
        'SqlUpdateCommand1
        '
        Me.SqlUpdateCommand1.CommandText = "dbo.MainOrgStoryUpdate"
        Me.SqlUpdateCommand1.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlUpdateCommand1.Connection = Me.SqlConnection1
        Me.SqlUpdateCommand1.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, False, CType(0, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing), New System.Data.SqlClient.SqlParameter("@ID", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "StoryID", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@OrgNum", System.Data.SqlDbType.Int, 4, "OrgNum"), New System.Data.SqlClient.SqlParameter("@CaseNum", System.Data.SqlDbType.Int, 4, "CaseNum"), New System.Data.SqlClient.SqlParameter("@Headline", System.Data.SqlDbType.VarChar, 100, "Headline"), New System.Data.SqlClient.SqlParameter("@CreateDate", System.Data.SqlDbType.[Date], 3, "CreateDate"), New System.Data.SqlClient.SqlParameter("@StaffNum", System.Data.SqlDbType.SmallInt, 2, "CreateStaffNum"), New System.Data.SqlClient.SqlParameter("@storyText", System.Data.SqlDbType.Text, 2147483647, "StoryText")})
        '
        'StatusBar1
        '
        Me.StatusBar1.Location = New System.Drawing.Point(0, 619)
        Me.StatusBar1.Name = "StatusBar1"
        Me.StatusBar1.Panels.AddRange(New System.Windows.Forms.StatusBarPanel() {Me.StatusBarPanel1, Me.StatusBarPanelID, Me.StatusBarPanel2})
        Me.StatusBar1.ShowPanels = True
        Me.StatusBar1.Size = New System.Drawing.Size(984, 22)
        Me.StatusBar1.TabIndex = 274
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
        Me.StatusBarPanelID.Text = "Organization Story:"
        Me.StatusBarPanelID.Width = 111
        '
        'StatusBarPanel2
        '
        Me.StatusBarPanel2.Alignment = System.Windows.Forms.HorizontalAlignment.Right
        Me.StatusBarPanel2.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring
        Me.StatusBarPanel2.Name = "StatusBarPanel2"
        Me.StatusBarPanel2.Text = "Use this window to edit Organization Stories."
        Me.StatusBarPanel2.Width = 656
        '
        'frmMainOrgStory
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(984, 641)
        Me.Controls.Add(Me.StatusBar1)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(StoryIDLabel)
        Me.Controls.Add(Me.StoryIDTextBox)
        Me.Controls.Add(OrgNumLabel)
        Me.Controls.Add(Me.OrgNumTextBox)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtHeadline)
        Me.Controls.Add(Me.DtCreate)
        Me.Controls.Add(Me.cboStaff)
        Me.Controls.Add(Me.cboCase)
        Me.Controls.Add(CaseNumLabel)
        Me.Controls.Add(CreateDateLabel)
        Me.Controls.Add(CreateStaffNumLabel)
        Me.Controls.Add(StoryTextLabel)
        Me.Controls.Add(Me.rtbStory)
        Me.Controls.Add(Me.btnSpellcheck)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.fldGotoOrg)
        Me.Menu = Me.MainMenu1
        Me.Name = "frmMainOrgStory"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Tag = "ORGANIZATION STORY"
        Me.Text = "ORGANIZATION STORY"
        Me.Panel2.ResumeLayout(False)
        CType(Me.MainOrgStoryBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsMainStory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsStoryCase1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.StatusBarPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.StatusBarPanelID, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.StatusBarPanel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnSpellcheck As System.Windows.Forms.Button
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents btnSaveExit As System.Windows.Forms.Button
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents fldGotoOrg As System.Windows.Forms.TextBox
    Friend WithEvents rtbStory As System.Windows.Forms.RichTextBox
    Friend WithEvents DsMainStory As InfoCtr.dsMainStory
    Friend WithEvents cboCase As InfoCtr.ComboBoxRelaxed
    Friend WithEvents cboStaff As InfoCtr.ComboBoxRelaxed
    Friend WithEvents DtCreate As InfoCtr.DateTextBox
    Friend WithEvents txtHeadline As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents MainMenu1 As System.Windows.Forms.MainMenu
    Friend WithEvents MenuItem1 As System.Windows.Forms.MenuItem
    Friend WithEvents miClose As System.Windows.Forms.MenuItem
    Friend WithEvents miEdit As System.Windows.Forms.MenuItem
    Friend WithEvents miSave As System.Windows.Forms.MenuItem
    Friend WithEvents miCancel As System.Windows.Forms.MenuItem
    Friend WithEvents miDelete As System.Windows.Forms.MenuItem
    Friend WithEvents StoryIDTextBox As System.Windows.Forms.TextBox
    Friend WithEvents OrgNumTextBox As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents DsStoryCase1 As InfoCtr.dsStoryCase
    Friend WithEvents sqlCaseNames As System.Data.SqlClient.SqlCommand
    Friend WithEvents daspStoryCases As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents ErrorProvider1 As System.Windows.Forms.ErrorProvider
    Friend WithEvents daMainOrgStory As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents SqlSelectCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlConnection1 As System.Data.SqlClient.SqlConnection
    Friend WithEvents SqlUpdateCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents MainOrgStoryBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents SqlDeleteCommand As System.Data.SqlClient.SqlCommand
    Friend WithEvents StatusBar1 As System.Windows.Forms.StatusBar
    Friend WithEvents StatusBarPanel1 As System.Windows.Forms.StatusBarPanel
    Friend WithEvents StatusBarPanelID As System.Windows.Forms.StatusBarPanel
    Friend WithEvents StatusBarPanel2 As System.Windows.Forms.StatusBarPanel
End Class
