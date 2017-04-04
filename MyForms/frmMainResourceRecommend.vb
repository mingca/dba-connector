Imports System.Data.SqlClient
Imports System.Windows.Forms
Imports System.Text

Public Class frmMainResourceRecommend

    Inherits System.Windows.Forms.Form
    Public isLoaded As Boolean = False

    '===== for Detail form  closing routines
    Dim ctlIdentify As Control 'fields for delete and messages
    Dim ctlNeutral As Control 'will never be invalidated
    Dim objHowClose As Short
    Dim mainDS As DataSet 'for generic module calls like CloseDetailForm
    Dim mainTopic As String 'name of entity of this form - case, contact, etc
    Dim mainDAdapt As SqlDataAdapter
    Public ThisID, LocalOrgID As Integer

    Dim mainBSrce As System.Windows.Forms.BindingSource

#Region "Initialize"
    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
    End Sub


    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
        Forms.Remove(Me)
    End Sub

#End Region  'initialize

#Region " Windows Form Designer generated code "
    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMainResourceRecommend))
        Me.MainMenu1 = New System.Windows.Forms.MainMenu(Me.components)
        Me.MenuItem1 = New System.Windows.Forms.MenuItem()
        Me.miAddFeedback = New System.Windows.Forms.MenuItem()
        Me.miClose = New System.Windows.Forms.MenuItem()
        Me.MenuItem2 = New System.Windows.Forms.MenuItem()
        Me.miSave = New System.Windows.Forms.MenuItem()
        Me.miCancel = New System.Windows.Forms.MenuItem()
        Me.miDelete = New System.Windows.Forms.MenuItem()
        Me.MenuItem3 = New System.Windows.Forms.MenuItem()
        Me.miGotoFeedback = New System.Windows.Forms.MenuItem()
        Me.miGotoOrg = New System.Windows.Forms.MenuItem()
        Me.miGotoCase = New System.Windows.Forms.MenuItem()
        Me.miHelp = New System.Windows.Forms.MenuItem()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.daMainRecommendation = New System.Data.SqlClient.SqlDataAdapter()
        Me.SqlDeleteCommand = New System.Data.SqlClient.SqlCommand()
        Me.SqlSelectCommand = New System.Data.SqlClient.SqlCommand()
        Me.SqlUpdateCommand = New System.Data.SqlClient.SqlCommand()
        Me.cboOrg = New InfoCtr.ComboBoxRelaxed()
        Me.MainResRecommendBindingSource1 = New System.Windows.Forms.BindingSource(Me.components)
        Me.DsMainResourceRecommend1 = New InfoCtr.dsMainResourceRecommend()
        Me.cboResource = New InfoCtr.ComboBoxRelaxed()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.cboStaff = New InfoCtr.ComboBoxRelaxed()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.cboContact = New InfoCtr.ComboBoxRelaxed()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtOther = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.cboFunded = New InfoCtr.ComboBoxRelaxed()
        Me.fldGotoCase = New System.Windows.Forms.TextBox()
        Me.fldGotoOrg = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.DtRecommend = New InfoCtr.DateTextBox()
        Me.fldGotoResource = New System.Windows.Forms.TextBox()
        Me.StatusBar1 = New System.Windows.Forms.StatusBar()
        Me.StatusBarPanel1 = New System.Windows.Forms.StatusBarPanel()
        Me.StatusBarPanelID = New System.Windows.Forms.StatusBarPanel()
        Me.StatusBarPanel2 = New System.Windows.Forms.StatusBarPanel()
        Me.cboCase = New InfoCtr.ComboBoxRelaxed()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.btnNew = New System.Windows.Forms.Button()
        Me.fldFeedbackID = New System.Windows.Forms.Label()
        Me.btnSaveExit = New System.Windows.Forms.Button()
        Me.fldGotoFeedback = New System.Windows.Forms.TextBox()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.fldCaseNum = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.fldOrgNum = New System.Windows.Forms.Label()
        Me.fldID = New System.Windows.Forms.Label()
        Me.cboUsed = New InfoCtr.ComboBoxRelaxed()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.fldContactNum = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.rtbNotes = New System.Windows.Forms.RichTextBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.btnHelp = New System.Windows.Forms.Button()
        Me.ErrorProvider1 = New System.Windows.Forms.ErrorProvider(Me.components)
        CType(Me.MainResRecommendBindingSource1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsMainResourceRecommend1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        CType(Me.StatusBarPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.StatusBarPanelID, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.StatusBarPanel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel3.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'MainMenu1
        '
        Me.MainMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem1, Me.MenuItem2, Me.MenuItem3, Me.miHelp})
        '
        'MenuItem1
        '
        Me.MenuItem1.Index = 0
        Me.MenuItem1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.miAddFeedback, Me.miClose})
        Me.MenuItem1.Text = "File"
        '
        'miAddFeedback
        '
        Me.miAddFeedback.Index = 0
        Me.miAddFeedback.Text = "Add Feedback"
        '
        'miClose
        '
        Me.miClose.Index = 1
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
        Me.miDelete.Text = "Delete this Recommendation"
        '
        'MenuItem3
        '
        Me.MenuItem3.Index = 2
        Me.MenuItem3.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.miGotoFeedback, Me.miGotoOrg, Me.miGotoCase})
        Me.MenuItem3.Text = "Goto"
        '
        'miGotoFeedback
        '
        Me.miGotoFeedback.Index = 0
        Me.miGotoFeedback.Text = "Feedback from this Recommendation"
        '
        'miGotoOrg
        '
        Me.miGotoOrg.Index = 1
        Me.miGotoOrg.Text = "Organization"
        '
        'miGotoCase
        '
        Me.miGotoCase.Index = 2
        Me.miGotoCase.Text = "Case"
        '
        'miHelp
        '
        Me.miHelp.Index = 3
        Me.miHelp.Text = "Help"
        '
        'Label26
        '
        Me.Label26.BackColor = System.Drawing.SystemColors.Control
        Me.Label26.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.ForeColor = System.Drawing.SystemColors.Desktop
        Me.Label26.Location = New System.Drawing.Point(5, 9)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(330, 27)
        Me.Label26.TabIndex = 212
        Me.Label26.Text = "RECOMMENDATION/FUNDED DETAIL"
        '
        'daMainRecommendation
        '
        Me.daMainRecommendation.DeleteCommand = Me.SqlDeleteCommand
        Me.daMainRecommendation.SelectCommand = Me.SqlSelectCommand
        Me.daMainRecommendation.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "MainResRecommend", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("RecommendID", "RecommendID"), New System.Data.Common.DataColumnMapping("ResourceNum", "ResourceNum"), New System.Data.Common.DataColumnMapping("OrgNum", "OrgNum"), New System.Data.Common.DataColumnMapping("CaseNum", "CaseNum"), New System.Data.Common.DataColumnMapping("ConversNum", "ConversNum"), New System.Data.Common.DataColumnMapping("RecommendDate", "RecommendDate"), New System.Data.Common.DataColumnMapping("RecommendStaffNum", "RecommendStaffNum"), New System.Data.Common.DataColumnMapping("ContactNum", "ContactNum"), New System.Data.Common.DataColumnMapping("RecommendedBy", "RecommendedBy"), New System.Data.Common.DataColumnMapping("Used", "Used"), New System.Data.Common.DataColumnMapping("GrantNum", "GrantNum"), New System.Data.Common.DataColumnMapping("AlbanNum", "AlbanNum"), New System.Data.Common.DataColumnMapping("Notes", "Notes"), New System.Data.Common.DataColumnMapping("Stamped", "Stamped"), New System.Data.Common.DataColumnMapping("Feedbacknum", "Feedbacknum")})})
        Me.daMainRecommendation.UpdateCommand = Me.SqlUpdateCommand
        '
        'SqlDeleteCommand
        '
        Me.SqlDeleteCommand.CommandText = "MainResourceRecommendDelete"
        Me.SqlDeleteCommand.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlDeleteCommand.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "RecommendID", System.Data.DataRowVersion.Original, Nothing)})
        '
        'SqlSelectCommand
        '
        Me.SqlSelectCommand.CommandText = "dbo.MainResourceRecommend"
        Me.SqlSelectCommand.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlSelectCommand.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, False, CType(0, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing), New System.Data.SqlClient.SqlParameter("@ID", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, "15")})
        '
        'SqlUpdateCommand
        '
        Me.SqlUpdateCommand.CommandText = "dbo.MainResourceRecommendUpdate"
        Me.SqlUpdateCommand.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlUpdateCommand.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, False, CType(0, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing), New System.Data.SqlClient.SqlParameter("@ResourceNum", System.Data.SqlDbType.Int, 4, "ResourceNum"), New System.Data.SqlClient.SqlParameter("@OrgNum", System.Data.SqlDbType.Int, 4, "OrgNum"), New System.Data.SqlClient.SqlParameter("@CaseNum", System.Data.SqlDbType.Int, 4, "CaseNum"), New System.Data.SqlClient.SqlParameter("@ConversNum", System.Data.SqlDbType.Int, 4, "ConversNum"), New System.Data.SqlClient.SqlParameter("@RecommendDate", System.Data.SqlDbType.[Date], 4, "RecommendDate"), New System.Data.SqlClient.SqlParameter("@RecommendStaffNum", System.Data.SqlDbType.Int, 4, "RecommendStaffNum"), New System.Data.SqlClient.SqlParameter("@ContactNum", System.Data.SqlDbType.Int, 4, "ContactNum"), New System.Data.SqlClient.SqlParameter("@RecommendedBy", System.Data.SqlDbType.VarChar, 50, "RecommendedBy"), New System.Data.SqlClient.SqlParameter("@Used", System.Data.SqlDbType.VarChar, 50, "Used"), New System.Data.SqlClient.SqlParameter("@GrantNum", System.Data.SqlDbType.VarChar, 50, "GrantNum"), New System.Data.SqlClient.SqlParameter("@AlbanNum", System.Data.SqlDbType.Int, 4, "AlbanNum"), New System.Data.SqlClient.SqlParameter("@Notes", System.Data.SqlDbType.Text, 2147483647, "Notes"), New System.Data.SqlClient.SqlParameter("@Stamp", System.Data.SqlDbType.Timestamp, 8, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "Stamped", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@ID", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "RecommendID", System.Data.DataRowVersion.Original, Nothing)})
        '
        'cboOrg
        '
        Me.cboOrg.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboOrg.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboOrg.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.MainResRecommendBindingSource1, "OrgNum", True))
        Me.cboOrg.FormattingEnabled = True
        Me.cboOrg.Location = New System.Drawing.Point(167, 93)
        Me.cboOrg.Name = "cboOrg"
        Me.cboOrg.RestrictContentToListItems = True
        Me.cboOrg.Size = New System.Drawing.Size(338, 21)
        Me.cboOrg.TabIndex = 1
        Me.cboOrg.Tag = "ORGANIZATION"
        '
        'MainResRecommendBindingSource1
        '
        Me.MainResRecommendBindingSource1.DataMember = "MainResRecommend"
        Me.MainResRecommendBindingSource1.DataSource = Me.DsMainResourceRecommend1
        '
        'DsMainResourceRecommend1
        '
        Me.DsMainResourceRecommend1.DataSetName = "dsMainResourceRecommend"
        Me.DsMainResourceRecommend1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'cboResource
        '
        Me.cboResource.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboResource.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboResource.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.MainResRecommendBindingSource1, "ResourceNum", True))
        Me.cboResource.FormattingEnabled = True
        Me.cboResource.Location = New System.Drawing.Point(167, 64)
        Me.cboResource.Name = "cboResource"
        Me.cboResource.RestrictContentToListItems = True
        Me.cboResource.Size = New System.Drawing.Size(338, 21)
        Me.cboResource.TabIndex = 0
        Me.cboResource.Tag = "RESOURCE"
        Me.ToolTip1.SetToolTip(Me.cboResource, "Click or use arrow keys and Enter to select.  Tab moves to next field.")
        '
        'Panel2
        '
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel2.Controls.Add(Me.cboStaff)
        Me.Panel2.Controls.Add(Me.Label13)
        Me.Panel2.Controls.Add(Me.cboContact)
        Me.Panel2.Controls.Add(Me.Label28)
        Me.Panel2.Location = New System.Drawing.Point(45, 216)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(315, 70)
        Me.Panel2.TabIndex = 4
        '
        'cboStaff
        '
        Me.cboStaff.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboStaff.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboStaff.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.MainResRecommendBindingSource1, "RecommendStaffNum", True))
        Me.cboStaff.Location = New System.Drawing.Point(98, 7)
        Me.cboStaff.Name = "cboStaff"
        Me.cboStaff.RestrictContentToListItems = True
        Me.cboStaff.Size = New System.Drawing.Size(187, 21)
        Me.cboStaff.TabIndex = 1
        Me.cboStaff.Tag = "CENTER STAFF"
        '
        'Label13
        '
        Me.Label13.Location = New System.Drawing.Point(20, 7)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(72, 18)
        Me.Label13.TabIndex = 117
        Me.Label13.Text = "Center Staff"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cboContact
        '
        Me.cboContact.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboContact.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboContact.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.MainResRecommendBindingSource1, "ContactNum", True))
        Me.cboContact.DisplayMember = "ContactName"
        Me.cboContact.FormattingEnabled = True
        Me.cboContact.Location = New System.Drawing.Point(98, 34)
        Me.cboContact.Name = "cboContact"
        Me.cboContact.RestrictContentToListItems = True
        Me.cboContact.Size = New System.Drawing.Size(187, 21)
        Me.cboContact.TabIndex = 2
        Me.cboContact.Tag = "CONTACT"
        Me.cboContact.ValueMember = "ContactID"
        '
        'Label28
        '
        Me.Label28.Location = New System.Drawing.Point(-10, 31)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(102, 24)
        Me.Label28.TabIndex = 219
        Me.Label28.Text = "Recommended to"
        Me.Label28.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label8
        '
        Me.Label8.Location = New System.Drawing.Point(3, -2)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(81, 55)
        Me.Label8.TabIndex = 116
        Me.Label8.Text = "This resource was discovered by: "
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtOther
        '
        Me.txtOther.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainResRecommendBindingSource1, "RecommendedBy", True))
        Me.txtOther.Location = New System.Drawing.Point(90, 7)
        Me.txtOther.MaxLength = 50
        Me.txtOther.Multiline = True
        Me.txtOther.Name = "txtOther"
        Me.txtOther.Size = New System.Drawing.Size(160, 43)
        Me.txtOther.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(40, 185)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(273, 28)
        Me.Label1.TabIndex = 221
        Me.Label1.Text = "Resource Recommended by Center Staff"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label30
        '
        Me.Label30.Location = New System.Drawing.Point(128, 305)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(150, 19)
        Me.Label30.TabIndex = 220
        Me.Label30.Text = "Grant Funding this Resource"
        Me.Label30.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cboFunded
        '
        Me.cboFunded.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboFunded.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboFunded.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.MainResRecommendBindingSource1, "GrantNum", True))
        Me.cboFunded.Location = New System.Drawing.Point(284, 301)
        Me.cboFunded.Name = "cboFunded"
        Me.cboFunded.RestrictContentToListItems = True
        Me.cboFunded.Size = New System.Drawing.Size(168, 21)
        Me.cboFunded.TabIndex = 6
        Me.cboFunded.Tag = "GRANT NUM"
        '
        'fldGotoCase
        '
        Me.fldGotoCase.BackColor = System.Drawing.SystemColors.Control
        Me.fldGotoCase.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fldGotoCase.ForeColor = System.Drawing.Color.ForestGreen
        Me.fldGotoCase.Location = New System.Drawing.Point(75, 126)
        Me.fldGotoCase.Name = "fldGotoCase"
        Me.fldGotoCase.ReadOnly = True
        Me.fldGotoCase.Size = New System.Drawing.Size(83, 20)
        Me.fldGotoCase.TabIndex = 218
        Me.fldGotoCase.Tag = "Case"
        Me.fldGotoCase.Text = "Case"
        Me.fldGotoCase.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'fldGotoOrg
        '
        Me.fldGotoOrg.BackColor = System.Drawing.SystemColors.Control
        Me.fldGotoOrg.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fldGotoOrg.ForeColor = System.Drawing.Color.ForestGreen
        Me.fldGotoOrg.Location = New System.Drawing.Point(75, 94)
        Me.fldGotoOrg.Name = "fldGotoOrg"
        Me.fldGotoOrg.ReadOnly = True
        Me.fldGotoOrg.Size = New System.Drawing.Size(83, 20)
        Me.fldGotoOrg.TabIndex = 217
        Me.fldGotoOrg.Tag = "Org"
        Me.fldGotoOrg.Text = "Organization"
        Me.fldGotoOrg.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ToolTip1.SetToolTip(Me.fldGotoOrg, "Select the Organization to which this Recommendation or Funding belonsgs.")
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Location = New System.Drawing.Point(17, 153)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(141, 21)
        Me.Label3.TabIndex = 216
        Me.Label3.Text = "Date of Recommendation"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'DtRecommend
        '
        Me.DtRecommend.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainResRecommendBindingSource1, "RecommendDate", True))
        Me.DtRecommend.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DtRecommend.ForeColor = System.Drawing.SystemColors.ControlText
        Me.DtRecommend.Location = New System.Drawing.Point(167, 151)
        Me.DtRecommend.Name = "DtRecommend"
        Me.DtRecommend.Size = New System.Drawing.Size(116, 23)
        Me.DtRecommend.TabIndex = 3
        Me.DtRecommend.Tag = "Date"
        '
        'fldGotoResource
        '
        Me.fldGotoResource.BackColor = System.Drawing.SystemColors.Control
        Me.fldGotoResource.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fldGotoResource.ForeColor = System.Drawing.Color.ForestGreen
        Me.fldGotoResource.Location = New System.Drawing.Point(75, 66)
        Me.fldGotoResource.Name = "fldGotoResource"
        Me.fldGotoResource.ReadOnly = True
        Me.fldGotoResource.Size = New System.Drawing.Size(83, 20)
        Me.fldGotoResource.TabIndex = 215
        Me.fldGotoResource.Tag = "Resource"
        Me.fldGotoResource.Text = "Resource"
        Me.fldGotoResource.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'StatusBar1
        '
        Me.StatusBar1.Location = New System.Drawing.Point(0, 523)
        Me.StatusBar1.Name = "StatusBar1"
        Me.StatusBar1.Panels.AddRange(New System.Windows.Forms.StatusBarPanel() {Me.StatusBarPanel1, Me.StatusBarPanelID, Me.StatusBarPanel2})
        Me.StatusBar1.ShowPanels = True
        Me.StatusBar1.Size = New System.Drawing.Size(705, 22)
        Me.StatusBar1.TabIndex = 228
        '
        'StatusBarPanel1
        '
        Me.StatusBarPanel1.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents
        Me.StatusBarPanel1.BorderStyle = System.Windows.Forms.StatusBarPanelBorderStyle.Raised
        Me.StatusBarPanel1.MinWidth = 200
        Me.StatusBarPanel1.Name = "StatusBarPanel1"
        Me.StatusBarPanel1.Text = "StatusBarPanel1"
        Me.StatusBarPanel1.Width = 200
        '
        'StatusBarPanelID
        '
        Me.StatusBarPanelID.Alignment = System.Windows.Forms.HorizontalAlignment.Center
        Me.StatusBarPanelID.MinWidth = 200
        Me.StatusBarPanelID.Name = "StatusBarPanelID"
        Me.StatusBarPanelID.Text = "Recommendation ID"
        Me.StatusBarPanelID.Width = 200
        '
        'StatusBarPanel2
        '
        Me.StatusBarPanel2.Alignment = System.Windows.Forms.HorizontalAlignment.Right
        Me.StatusBarPanel2.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring
        Me.StatusBarPanel2.BorderStyle = System.Windows.Forms.StatusBarPanelBorderStyle.None
        Me.StatusBarPanel2.Name = "StatusBarPanel2"
        Me.StatusBarPanel2.Text = "Use this window to change Recommendation detail."
        Me.StatusBarPanel2.Width = 288
        '
        'cboCase
        '
        Me.cboCase.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboCase.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboCase.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.MainResRecommendBindingSource1, "CaseNum", True))
        Me.cboCase.DisplayMember = "CaseName"
        Me.cboCase.FormattingEnabled = True
        Me.cboCase.Location = New System.Drawing.Point(167, 122)
        Me.cboCase.Name = "cboCase"
        Me.cboCase.RestrictContentToListItems = True
        Me.cboCase.Size = New System.Drawing.Size(338, 21)
        Me.cboCase.TabIndex = 2
        Me.cboCase.Tag = "CASE"
        Me.cboCase.ValueMember = "CaseID"
        '
        'btnDelete
        '
        Me.btnDelete.BackColor = System.Drawing.SystemColors.Control
        Me.btnDelete.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Image = Global.InfoCtr.My.Resources.Resources.btnDelete
        Me.btnDelete.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnDelete.Location = New System.Drawing.Point(56, 1)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(40, 35)
        Me.btnDelete.TabIndex = 418
        Me.btnDelete.Text = "Delete"
        Me.btnDelete.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.btnDelete, "Delete this Recommendation")
        Me.btnDelete.UseVisualStyleBackColor = False
        Me.btnDelete.Visible = False
        '
        'btnNew
        '
        Me.btnNew.BackColor = System.Drawing.Color.PaleGoldenrod
        Me.btnNew.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnNew.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnNew.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnNew.Location = New System.Drawing.Point(-1, 1)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(57, 35)
        Me.btnNew.TabIndex = 420
        Me.btnNew.Text = "New Feedback"
        Me.btnNew.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.btnNew, "Add new Feedback to this Recommendation")
        Me.btnNew.UseVisualStyleBackColor = False
        '
        'fldFeedbackID
        '
        Me.fldFeedbackID.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainResRecommendBindingSource1, "FeedbackNum", True))
        Me.fldFeedbackID.ForeColor = System.Drawing.Color.DarkGray
        Me.fldFeedbackID.Location = New System.Drawing.Point(581, 368)
        Me.fldFeedbackID.Name = "fldFeedbackID"
        Me.fldFeedbackID.Size = New System.Drawing.Size(78, 17)
        Me.fldFeedbackID.TabIndex = 440
        Me.fldFeedbackID.Text = "1st feedback"
        Me.fldFeedbackID.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ToolTip1.SetToolTip(Me.fldFeedbackID, "(only 1 feedback displays here)")
        '
        'btnSaveExit
        '
        Me.btnSaveExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSaveExit.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.btnSaveExit.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSaveExit.Image = CType(resources.GetObject("btnSaveExit.Image"), System.Drawing.Image)
        Me.btnSaveExit.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSaveExit.Location = New System.Drawing.Point(97, 1)
        Me.btnSaveExit.Name = "btnSaveExit"
        Me.btnSaveExit.Size = New System.Drawing.Size(40, 35)
        Me.btnSaveExit.TabIndex = 415
        Me.btnSaveExit.Text = "Close"
        Me.btnSaveExit.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.btnSaveExit, "Saves any changes and Closes this window")
        Me.btnSaveExit.UseVisualStyleBackColor = False
        '
        'fldGotoFeedback
        '
        Me.fldGotoFeedback.BackColor = System.Drawing.SystemColors.Control
        Me.fldGotoFeedback.ForeColor = System.Drawing.Color.ForestGreen
        Me.fldGotoFeedback.Location = New System.Drawing.Point(499, 365)
        Me.fldGotoFeedback.Name = "fldGotoFeedback"
        Me.fldGotoFeedback.ReadOnly = True
        Me.fldGotoFeedback.Size = New System.Drawing.Size(67, 20)
        Me.fldGotoFeedback.TabIndex = 439
        Me.fldGotoFeedback.Tag = "Feedback"
        Me.fldGotoFeedback.Text = "Feedback #"
        Me.ToolTip1.SetToolTip(Me.fldGotoFeedback, "open First Feedback associated with this Recommendation")
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.SystemColors.Highlight
        Me.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel3.Controls.Add(Me.btnSaveExit)
        Me.Panel3.Controls.Add(Me.btnNew)
        Me.Panel3.Controls.Add(Me.btnDelete)
        Me.Panel3.Location = New System.Drawing.Point(488, 5)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(142, 40)
        Me.Panel3.TabIndex = 418
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlDark
        Me.Label5.Location = New System.Drawing.Point(496, 440)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(79, 15)
        Me.Label5.TabIndex = 424
        Me.Label5.Text = "Case#"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'fldCaseNum
        '
        Me.fldCaseNum.ForeColor = System.Drawing.Color.DarkGray
        Me.fldCaseNum.Location = New System.Drawing.Point(581, 440)
        Me.fldCaseNum.Name = "fldCaseNum"
        Me.fldCaseNum.Size = New System.Drawing.Size(64, 15)
        Me.fldCaseNum.TabIndex = 423
        Me.fldCaseNum.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlDark
        Me.Label4.Location = New System.Drawing.Point(496, 421)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(79, 15)
        Me.Label4.TabIndex = 422
        Me.Label4.Text = "Org#"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlDark
        Me.Label2.Location = New System.Drawing.Point(496, 402)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(79, 15)
        Me.Label2.TabIndex = 421
        Me.Label2.Text = "Recommend#"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'fldOrgNum
        '
        Me.fldOrgNum.ForeColor = System.Drawing.Color.DarkGray
        Me.fldOrgNum.Location = New System.Drawing.Point(581, 421)
        Me.fldOrgNum.Name = "fldOrgNum"
        Me.fldOrgNum.Size = New System.Drawing.Size(64, 15)
        Me.fldOrgNum.TabIndex = 420
        Me.fldOrgNum.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'fldID
        '
        Me.fldID.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainResRecommendBindingSource1, "RecommendID", True))
        Me.fldID.ForeColor = System.Drawing.Color.DarkGray
        Me.fldID.Location = New System.Drawing.Point(581, 402)
        Me.fldID.Name = "fldID"
        Me.fldID.Size = New System.Drawing.Size(64, 15)
        Me.fldID.TabIndex = 419
        Me.fldID.Text = "Recommend"
        Me.fldID.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cboUsed
        '
        Me.cboUsed.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboUsed.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboUsed.DataBindings.Add(New System.Windows.Forms.Binding("SelectedItem", Me.MainResRecommendBindingSource1, "Used", True))
        Me.cboUsed.FormattingEnabled = True
        Me.cboUsed.Items.AddRange(New Object() {"Yes", "No", "unknown"})
        Me.cboUsed.Location = New System.Drawing.Point(284, 332)
        Me.cboUsed.Name = "cboUsed"
        Me.cboUsed.RestrictContentToListItems = True
        Me.cboUsed.Size = New System.Drawing.Size(166, 21)
        Me.cboUsed.TabIndex = 7
        Me.cboUsed.Tag = "RESOURCE was USED"
        '
        'Label6
        '
        Me.Label6.Location = New System.Drawing.Point(137, 332)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(141, 29)
        Me.Label6.TabIndex = 426
        Me.Label6.Text = "The resource was used by the Congregation"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'fldContactNum
        '
        Me.fldContactNum.ForeColor = System.Drawing.Color.DarkGray
        Me.fldContactNum.Location = New System.Drawing.Point(581, 455)
        Me.fldContactNum.Name = "fldContactNum"
        Me.fldContactNum.Size = New System.Drawing.Size(64, 15)
        Me.fldContactNum.TabIndex = 427
        Me.fldContactNum.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel1.Controls.Add(Me.txtOther)
        Me.Panel1.Controls.Add(Me.Label8)
        Me.Panel1.Location = New System.Drawing.Point(366, 216)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(266, 70)
        Me.Panel1.TabIndex = 5
        '
        'Label11
        '
        Me.Label11.Location = New System.Drawing.Point(319, 185)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(276, 28)
        Me.Label11.TabIndex = 429
        Me.Label11.Text = " -- OR --               Congregation Found this Resource"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label12
        '
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.SystemColors.ControlDark
        Me.Label12.Location = New System.Drawing.Point(496, 455)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(79, 15)
        Me.Label12.TabIndex = 430
        Me.Label12.Text = "Contact#"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label15
        '
        Me.Label15.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.ForeColor = System.Drawing.SystemColors.ControlDark
        Me.Label15.Location = New System.Drawing.Point(496, 386)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(79, 16)
        Me.Label15.TabIndex = 431
        Me.Label15.Text = "Resource#"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label17
        '
        Me.Label17.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainResRecommendBindingSource1, "ResourceNum", True))
        Me.Label17.ForeColor = System.Drawing.Color.DarkGray
        Me.Label17.Location = New System.Drawing.Point(581, 386)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(64, 15)
        Me.Label17.TabIndex = 433
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'rtbNotes
        '
        Me.rtbNotes.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainResRecommendBindingSource1, "Notes", True))
        Me.rtbNotes.Location = New System.Drawing.Point(43, 399)
        Me.rtbNotes.Name = "rtbNotes"
        Me.rtbNotes.Size = New System.Drawing.Size(315, 98)
        Me.rtbNotes.TabIndex = 8
        Me.rtbNotes.Text = ""
        '
        'Label18
        '
        Me.Label18.BackColor = System.Drawing.Color.Transparent
        Me.Label18.Location = New System.Drawing.Point(5, 396)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(35, 21)
        Me.Label18.TabIndex = 436
        Me.Label18.Text = "Note"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnHelp
        '
        Me.btnHelp.Image = CType(resources.GetObject("btnHelp.Image"), System.Drawing.Image)
        Me.btnHelp.Location = New System.Drawing.Point(634, 9)
        Me.btnHelp.Name = "btnHelp"
        Me.btnHelp.Size = New System.Drawing.Size(25, 25)
        Me.btnHelp.TabIndex = 441
        Me.btnHelp.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnHelp.UseVisualStyleBackColor = True
        '
        'ErrorProvider1
        '
        Me.ErrorProvider1.ContainerControl = Me
        '
        'frmMainResourceRecommend
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CausesValidation = False
        Me.ClientSize = New System.Drawing.Size(705, 545)
        Me.Controls.Add(Me.btnHelp)
        Me.Controls.Add(Me.fldFeedbackID)
        Me.Controls.Add(Me.fldGotoFeedback)
        Me.Controls.Add(Me.Label18)
        Me.Controls.Add(Me.rtbNotes)
        Me.Controls.Add(Me.Label17)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.fldContactNum)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.cboUsed)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.fldCaseNum)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.fldOrgNum)
        Me.Controls.Add(Me.fldID)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.cboCase)
        Me.Controls.Add(Me.StatusBar1)
        Me.Controls.Add(Me.cboOrg)
        Me.Controls.Add(Me.cboResource)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label30)
        Me.Controls.Add(Me.cboFunded)
        Me.Controls.Add(Me.fldGotoCase)
        Me.Controls.Add(Me.fldGotoOrg)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.DtRecommend)
        Me.Controls.Add(Me.fldGotoResource)
        Me.Controls.Add(Me.Label26)
        Me.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Menu = Me.MainMenu1
        Me.Name = "frmMainResourceRecommend"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Tag = "RECOMMENDATION"
        Me.Text = "Recommendation:"
        CType(Me.MainResRecommendBindingSource1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsMainResourceRecommend1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        CType(Me.StatusBarPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.StatusBarPanelID, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.StatusBarPanel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel3.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MainResRecommendBindingSource1 As System.Windows.Forms.BindingSource
    Friend WithEvents miGotoCase As System.Windows.Forms.MenuItem
    Friend WithEvents StatusBarPanelID As System.Windows.Forms.StatusBarPanel
    Friend WithEvents btnSaveExit As System.Windows.Forms.Button
    Friend WithEvents btnHelp As System.Windows.Forms.Button
    Friend WithEvents ErrorProvider1 As System.Windows.Forms.ErrorProvider
    Friend WithEvents MainMenu1 As System.Windows.Forms.MainMenu
    Friend WithEvents MenuItem1 As System.Windows.Forms.MenuItem
    Friend WithEvents miAddFeedback As System.Windows.Forms.MenuItem
    Friend WithEvents miClose As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem2 As System.Windows.Forms.MenuItem
    Friend WithEvents miSave As System.Windows.Forms.MenuItem
    Friend WithEvents miCancel As System.Windows.Forms.MenuItem
    Friend WithEvents miDelete As System.Windows.Forms.MenuItem
    Friend WithEvents miHelp As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem3 As System.Windows.Forms.MenuItem
    Friend WithEvents miGotoFeedback As System.Windows.Forms.MenuItem
    Friend WithEvents miGotoOrg As System.Windows.Forms.MenuItem
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents daMainRecommendation As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents SqlSelectCommand As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlUpdateCommand As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlDeleteCommand As System.Data.SqlClient.SqlCommand
    Friend WithEvents DsMainResourceRecommend1 As InfoCtr.dsMainResourceRecommend
    Friend WithEvents cboOrg As InfoCtr.ComboBoxRelaxed
    Friend WithEvents cboResource As InfoCtr.ComboBoxRelaxed
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents cboStaff As InfoCtr.ComboBoxRelaxed
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents txtOther As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents cboFunded As InfoCtr.ComboBoxRelaxed
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents fldGotoCase As System.Windows.Forms.TextBox
    Friend WithEvents fldGotoOrg As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents DtRecommend As InfoCtr.DateTextBox
    Private WithEvents fldGotoResource As System.Windows.Forms.TextBox
    Friend WithEvents StatusBar1 As System.Windows.Forms.StatusBar
    Friend WithEvents StatusBarPanel1 As System.Windows.Forms.StatusBarPanel
    Friend WithEvents StatusBarPanel2 As System.Windows.Forms.StatusBarPanel
    Friend WithEvents cboCase As InfoCtr.ComboBoxRelaxed
    Friend WithEvents cboContact As InfoCtr.ComboBoxRelaxed
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    '  Friend WithEvents btnSaveExit As System.Windows.Forms.Button
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Private WithEvents Label5 As System.Windows.Forms.Label
    Private WithEvents fldCaseNum As System.Windows.Forms.Label
    Private WithEvents Label4 As System.Windows.Forms.Label
    Private WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents fldOrgNum As System.Windows.Forms.Label
    Private WithEvents fldID As System.Windows.Forms.Label
    Friend WithEvents cboUsed As InfoCtr.ComboBoxRelaxed
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Private WithEvents fldContactNum As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents btnNew As System.Windows.Forms.Button
    Private WithEvents Label12 As System.Windows.Forms.Label
    Private WithEvents Label15 As System.Windows.Forms.Label
    Private WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents rtbNotes As System.Windows.Forms.RichTextBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents fldGotoFeedback As System.Windows.Forms.TextBox
    Private WithEvents fldFeedbackID As System.Windows.Forms.Label

#End Region 'windows

#Region "Load"

    'LOAD
    Private Sub frmMainResourceRecommend_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles MyBase.Load

        Dim dtOrgs As New DataTable
        Me.SuspendLayout()
        Me.StatusBarPanel1.Text = "Loading"
SetMainDSConnection:
        Me.daMainRecommendation.SelectCommand.Connection = sc
        Me.daMainRecommendation.UpdateCommand.Connection = sc
        Me.daMainRecommendation.DeleteCommand.Connection = sc

SetDefaults:
        ctlIdentify = Me.cboResource
        ctlNeutral = Me.Panel2
        mainDS = Me.DsMainResourceRecommend1
        mainDAdapt = Me.daMainRecommendation
        mainBSrce = Me.MainResRecommendBindingSource1
        mainTopic = "Recommendation"

LoadCombos:
        modGlobalVar.LoadStaffCombo(Me.cboStaff, False, StaffComboChoices.CMGI)
        LoadResourceCombo()

        'LOAD ORG COMBO
        Dim cmd As New SqlCommand("SELECT OrgID, OrgandCity FROM vwGetOrgCity ORDER BY OrgandCity ", sc)
        If SCConnect() Then
        Else
            Exit Sub
        End If
        Try
            dtOrgs.Load(cmd.ExecuteReader) ', CommandBehavior.CloseConnection)
        Catch ex As Exception
            modGlobalVar.msg("ERROR: can't fill org combo", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            sc.Close()
        End Try
        cboOrg.DisplayMember = "OrgandCity"
        cboOrg.ValueMember = "OrgID"
        cboOrg.DataSource = dtOrgs

FormSetup:
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.ResumeLayout()
        isLoaded = True
        Forms.Add(Me)
        modPopup.SetStatusBarText("Done", Me.StatusBar1, 0)
    End Sub

    'LOAD RESOURCE COMBO
    Public Sub LoadResourceCombo()
        Dim dtResources As New DataTable
        Dim cmd As New SqlCommand("SELECT ICCResourceID, CASE WHEN Active = 1 then  ResourceName ELSE '~' + ResourceName  END as ResourceName FROM tblResource WHERE ResourceName > ''  ORDER by Active desc, ResourceName", sc)
        If SCConnect() Then
        Else
            Exit Sub
        End If
        Try
            dtResources.Load(cmd.ExecuteReader) '.Fill(dsResource, "luResource")
        Catch ex As Exception
            modGlobalVar.msg("can't fill resource combo", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            sc.Close()
        End Try
        cboResource.DisplayMember = "ResourceName"
        cboResource.ValueMember = "ICCResourceID"
        cboResource.DataSource = dtResources

    End Sub

    'RELOAD
    Public Sub Reload() 'orgid, caseid
        modPopup.SetStatusBarText("Reloading", Me.StatusBar1, 0)
ResetVars:
        objHowClose = ObjClose.btnSaveExit
        Me.StatusBarPanelID.Text = mainTopic & " ID: " & ThisID.ToString

        LoadOrgBasedCombos()

        modPopup.SetStatusBarText("Done", Me.StatusBar1, 0)
    End Sub

    'load comboboxes
    Private Sub LoadOrgBasedCombos()
        Dim oldValues As New Microsoft.VisualBasic.Collection
        Dim dtGrants As New DataTable
        Dim dtContacts As New DataTable
        Dim dtCases As New DataTable

        'clear old  from underlying table case and contact
        Me.fldCaseNum.Text = String.Empty
        Me.fldContactNum.Text = String.Empty
        Me.fldOrgNum.Text = LocalOrgID

        'refill combo boxes
        modGlobalVar.LoadContactCombo(Me.cboContact, dtContacts, LocalOrgID)
        modGlobalVar.LoadCaseCombo(Me.cboCase, dtCases, LocalOrgID)
        Me.cboCase.SelectedIndex = -1
        Me.cboContact.SelectedIndex = -1

        'GRANTS
        Dim cmd As New SqlCommand("SELECT GrantIDTxt FROM tblGrant WHERE orgNum = " & LocalOrgID, sc)
        If SCConnect() Then
        Else
            Exit Sub
        End If
        Try
            dtGrants.Load(cmd.ExecuteReader)
        Catch ex As Exception
            modGlobalVar.msg("ERROR: can't load grant combo", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            sc.Close()
        End Try
        Me.cboFunded.ValueMember = "GrantIDTxt"
        Me.cboFunded.DisplayMember = "GrantIDTxt"
        Me.cboFunded.DataSource = dtGrants

    End Sub

#End Region 'load

#Region "Update Main"

    'SAVE & EXIT
    Private Sub btnSaveExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnSaveExit.Click
        objHowClose = ObjClose.btnSaveExit
        Me.Close()
    End Sub

    'ALLOW CLOSE WITHOUT SAVING
    Protected Sub miCloseForm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
     Handles miClose.Click

        MouseWait()
        ctlNeutral.Focus()
        If mainDS.Tables(0).Rows(0).HasVersion(DataRowVersion.Proposed) Or mainDS.HasChanges Then
            mainBSrce.EndEdit()
        End If

        objHowClose = AskAcceptChanges(mainDS, mainTopic)
        Me.Close()
CloseAll:
        MouseDefault()
    End Sub

    'CLOSING
    Private Sub frm_Closing(ByVal sender As Object, ByVal e As FormClosingEventArgs) _
               Handles MyBase.FormClosing        'ComponentModel.CancelEventArgs
        Dim arCtls(0) As Control
        Dim ctl As Control

        ctlNeutral.Focus()  'safely calls endedit if was in edit mode

CallUpdate:
        If objHowClose = ObjClose.DontSaveClose Or objHowClose = ObjClose.cancelClose Then
            GoTo validate
        End If
        If DoUpdate() Then
            e.Cancel = False
        Else
            e.Cancel = True 'don't close form
            GoTo ReleaseForm
        End If

VALIDATE:  'check required fields; allow user to leave anyway if used menu
        Select Case objHowClose
            Case Is = ObjClose.DontSaveClose, ObjClose.miDelete
                e.Cancel = False
                GoTo ReleaseForm
            Case Is = ObjClose.cancelClose
                e.Cancel = True
                GoTo ReleaseForm
            Case Else
                arCtls = CheckRequired()
                If arCtls.GetLength(0) > 1 Then 'required info missing
                    ctl = arCtls(0)
                    If objHowClose = ObjClose.SaveClose Or e.CloseReason = System.Windows.Forms.CloseReason.UserClosing Then
                        If ctl Is ctlIdentify Then   'delete record

                            e.Cancel = True
                        End If

                    End If
                    Dim strbListFields As New StringBuilder
                    For x As Integer = 0 To arCtls.GetLength(0) - 2
                        strbListFields.Append(", " & arCtls(x).Tag)
                    Next
                    e.Cancel = Not (modGlobalVar.AskCloseWithMissingInfo(objHowClose, ctl, strbListFields.ToString.Substring(2)))
                Else
                    e.Cancel = False
                End If
        End Select

ReleaseForm:
        If e.Cancel = False Then   'user OKs close form
            ClassOpenForms.frmMainResourceRecommend = Nothing 'reset global var
        Else
        End If
    End Sub

    'UPDATE BACK END, return number of records updated, return false if error updating
    Public Function DoUpdate() As Boolean
        Dim i As Integer
        MouseWait()
        modPopup.SetStatusBarText("Updating server", Me.StatusBar1, 0)

        If mainDS.Tables(0).Rows(0).HasVersion(DataRowVersion.Proposed) Or mainDS.HasChanges Then
            mainBSrce.EndEdit()
        Else
            DoUpdate = True
            GoTo CloseAll
        End If

        If mainDS.HasChanges = True Then 'this catches delete, save, asksave, save/exit, anyclose
UpdateBackend:
            Try
                i = mainDAdapt.Update(mainDS)
                DoUpdate = True
            Catch ex As Exception
                modGlobalVar.msg("ERROR: updating " & mainTopic, ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
                DoUpdate = False
            End Try
        Else
            'no changes to update
            DoUpdate = True 'completed action though no updates to be made
        End If
CloseAll:
        modPopup.SetStatusBarText("Update routine complete", Me.StatusBar1, 0)
        MouseDefault()

    End Function

    'CHECK REQUIRED FIELDS w Error Provider
    Private Function CheckRequired() As Control()
        Dim arCtls(0) As Control
        Dim ctl As Control
        Dim i As Integer = 0

        'Resource
        ctl = ctlIdentify
        If CType(ctl, ComboBox).SelectedIndex = -1 Then
            Me.ErrorProvider1.SetError(ctl, "please enter a " & IsNull(ctl.Tag, ctl.Name)) ' & " or 'Delete' this recommendation")
            arCtls(i) = ctl
            i = i + 1
            ReDim Preserve arCtls(arCtls.GetUpperBound(0) + 1)
        Else
            Me.ErrorProvider1.SetError(ctl, "")
        End If

        'Organization
        ctl = Me.cboOrg
        If CType(ctl, ComboBox).SelectedIndex = -1 Then
            Me.ErrorProvider1.SetError(ctl, "please enter a " & IsNull(ctl.Tag, ctl.Name)) ' & " or 'Delete' this recommendation")
            arCtls(i) = ctl
            i = i + 1
            ReDim Preserve arCtls(arCtls.GetUpperBound(0) + 1)
        Else
            Me.ErrorProvider1.SetError(ctl, "")
        End If

        'Case
        ctl = Me.cboCase
        If CType(ctl, ComboBox).SelectedIndex = -1 Then
            Me.ErrorProvider1.SetError(ctl, "please enter a " & IsNull(ctl.Tag, ctl.Name)) ' & " or 'Delete' this recommendation")
            arCtls(i) = ctl
            i = i + 1
            ReDim Preserve arCtls(arCtls.GetUpperBound(0) + 1)
        Else
            Me.ErrorProvider1.SetError(ctl, "")
        End If

        'RecommendDate
        ctl = Me.DtRecommend
        If Replace(Replace(Replace(ctl.Text, " ", ""), Chr(10), ""), Chr(13), "") = String.Empty Then
            Me.ErrorProvider1.SetError(ctl, "please enter a " & IsNull(ctl.Tag, ctl.Name)) ' & " or 'Delete' this recommendation")
            arCtls(i) = ctl
            i = i + 1
            ReDim Preserve arCtls(arCtls.GetUpperBound(0) + 1)
        Else
            Me.ErrorProvider1.SetError(ctl, "")
        End If

        'Recommended to
        ctl = Me.cboContact
        If Me.cboStaff.Text > "" And CType(ctl, ComboBox).SelectedIndex = -1 And Me.cboFunded.SelectedIndex = -1 Then 'If grant, skip this.  
            Me.ErrorProvider1.SetError(ctl, "please enter a " & IsNull(ctl.Tag, ctl.Name)) ' & " or 'Delete' this recommendation")
            arCtls(i) = ctl
            i = i + 1
            ReDim Preserve arCtls(arCtls.GetUpperBound(0) + 1)
        Else
            Me.ErrorProvider1.SetError(ctl, "")
        End If

        'Recommended by either staff or congregation
        ctl = Me.cboStaff
        If Me.cboStaff.SelectedIndex = -1 And Me.txtOther.Text = String.Empty Then
            Me.ErrorProvider1.SetError(ctl, "select a staff name or enter something in the 'Congregation Found This Resource box.") ' & " or 'Delete' if it is an unwanted entry")
            arCtls(i) = ctl
            i = i + 1
            ReDim Preserve arCtls(arCtls.GetUpperBound(0) + 1)
        End If

        'Add Neutral Control as finale
        arCtls(i) = ctlNeutral
        Return arCtls

    End Function

#End Region 'update main

#Region "Menu Items"

    '    SAVE
    Private Sub miSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles miSave.Click
        ctlNeutral.Focus()  'safely calls endedit if was in edit mode
        DoUpdate()
    End Sub

    'CANCEL CHANGES
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles miCancel.Click
        Try
            mainBSrce.CancelEdit()
            mainDS.RejectChanges()
        Catch ex As System.Exception
            modGlobalVar.msg("ERROR: cancelling ", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        modPopup.SetStatusBarText("Changes Cancelled", Me.StatusBar1, 0)

    End Sub

    'DELETE RECOMMENDATION
    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles miDelete.Click, btnDelete.Click

        If modGlobalVar.msg("CONFIRM DELETE", mainTopic & ": " + IsNull(ctlIdentify.Text, "") & NextLine & " WILL BE DELETED and the window closed.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            '    ctl.Text = "DELETE: " & IsNull(ctl.Text, "")
            objHowClose = ObjClose.miDelete
            mainBSrce.RemoveCurrent()
            Me.Close()
        End If
    End Sub

#End Region 'menu items

#Region "VALIDATE w ErrorProvider"

    'RESOURCE - required field, allow search
    Private Sub cboResource_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) _
        Handles cboResource.Validating

        Dim usrSel As New usrInput

        usrSel = modGlobalVar.ValidateBoundDD(sender, True, Me.ErrorProvider1, ObjClose.CloseByControl)

        Select Case usrSel
            Case Is = usrInput.Search
                Dim frm As New frmSrchResource
                frm.ShowDialog()
                LoadResourceCombo()
                e.Cancel = True
                sender.DroppedDown = True

            Case Is = usrInput.OK
                e.Cancel = False
            Case Else
                e.Cancel = True
                sender.DroppedDown = True
        End Select
        usrSel = Nothing

    End Sub

    'VALIDATE DATE
    Private Sub DateValidation(sender As Object, e As System.ComponentModel.CancelEventArgs) _
        Handles DtRecommend.Validating
        e.Cancel = modGlobalVar.ValidateDateA(sender, Me.ErrorProvider1)
    End Sub

    'STAFF - optional because could be from Grant
    Private Sub cboStaff_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) _
            Handles cboStaff.Validating
        If modGlobalVar.ValidateBoundDD(sender, False, Me.ErrorProvider1, ObjClose.CloseByControl) = usrInput.OK Then
            e.Cancel = False
        Else
            e.Cancel = True
        End If
    End Sub

    'cboOrg
    Private Sub cboOrg_SelectionChangeCommitted(ByVal sender As Object, ByVal e As System.EventArgs) _
        Handles cboOrg.SelectionChangeCommitted, cboOrg.SelectedIndexChanged
        If isLoaded Then
            If Me.cboOrg.SelectedIndex > -1 Then
                Me.ErrorProvider1.SetError(Me.cboOrg, "")
                LocalOrgID = Me.cboOrg.SelectedValue
                '  Me.fldOrgNum.Text = LocalOrgID 'works but is nto captured on load
                LoadOrgBasedCombos()
            Else
            End If
        End If
    End Sub

    'CASE - required field, allow add new - allow to get out of field to change org dd
    Private Sub cboCase_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) _
        Handles cboCase.Validating        ' SetError(Me.ErrorProvider1, sender, ValidateCBO(sender, IsNull(sender.tag, sender.name), True, CanAddNew.AddNew), True)
        Dim dtCases As New DataTable
        Dim i As Integer
        Dim strUsrEntry As String
        If cboOrg.SelectedIndex = -1 Then
            Exit Sub
        End If

        strUsrEntry = IsNull(Me.cboCase.Text, "")
        Select Case modGlobalVar.ValidateBoundDD(sender, False, Me.ErrorProvider1, ObjClose.CloseByControl)
            Case usrInput.Search
                i = modPopup.NewCase(LocalOrgID, strUsrEntry, IsNull(Me.cboStaff.SelectedValue, 0), "Case", Me, IsNull(Me.fldGotoOrg.Text, ""), 0)
                If i > 0 Then
                    modGlobalVar.LoadCaseCombo(Me.cboCase, dtCases, LocalOrgID)
                    Me.ErrorProvider1.SetError(sender, "")
                    Me.cboCase.SelectedValue = i
                End If
            Case usrInput.OK
                e.Cancel = False
            Case Else
                e.Cancel = True
                'Me.ErrorProvider1.SetError(sender, "select a case name from the dropdown box, or type a new name and fill out the popup form.")
        End Select

    End Sub

#End Region 'validate

#Region "Open Secondary Forms"

    'OPEN RESOURCE FORM
    Private Sub fldGotoResource_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles fldGotoResource.DoubleClick
        If Me.cboResource.SelectedIndex > -1 Then
            modGlobalVar.OpenResourceChoice(Me.cboResource.SelectedValue, Me.cboResource.Text)
        End If
    End Sub

    'OPEN Org FORM
    Private Sub fldGotoOrg_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles miGotoOrg.Click
        If Me.cboOrg.SelectedIndex > -1 Then
            modGlobalVar.OpenMainOrg(LocalOrgID, Me.cboOrg.Text)
        End If
    End Sub

    'OPEN CASE FORM
    Private Sub fldGotoCase_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles fldGotoCase.DoubleClick, miGotoCase.Click
        If Me.cboCase.SelectedIndex > -1 Then
            modGlobalVar.OpenMainCase(Me.cboCase.SelectedValue, Me.cboCase.Text, Me.cboOrg.Text, LocalOrgID) 'cboOrg.SelectedValue)
        Else
            modGlobalVar.msg("ATTENTION: no case selected", "select a case and try again" & NextLine & "If no cases are listed, create a new case from the Organization Detail window.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Me.cboCase.Focus()
        End If
    End Sub

#End Region 'open secondary

#Region "General"

    'BTN NEW
    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles btnNew.Click, miAddFeedback.Click

        Dim iCase As Integer
        If Me.cboCase.Text = String.Empty Then
            iCase = 0
        Else
            iCase = CType(Me.cboCase.SelectedValue, Integer)
        End If

        If Not SCConnect() Then
            Exit Sub
        End If

        Dim cmd As New SqlClient.SqlCommand("INSERT INTO tblResourceFeedback(ResourceNum, RecommendNum,OrgNum, CaseNum, FeedbackStaffNum, FeedbackDate) VALUES (" & Me.cboResource.SelectedValue & ", " & Me.fldID.Text & ", " & LocalOrgID & ", " & iCase & ", " & usr & ", N'" & Today & "'); SELECT @@Identity", sc)
        Dim newID As Integer
        Try
            newID = cmd.ExecuteScalar()
        Catch exce As Exception
            modGlobalVar.msg("ERRORL: inserting Feedback", exce.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        Finally
            sc.Close()
        End Try

OpenForm:
        modGlobalVar.OpenMainFeedback(newID, Me.cboResource.SelectedValue, Me.Text, LocalOrgID) 'Me.cboOrg.SelectedValue)
    End Sub

    'BTN HELP
    Private Sub miHelp_Click(ByVal sender As Object, ByVal e As System.EventArgs) _
        Handles miHelp.Click, btnHelp.Click
        modGlobalVar.msg("HELP: MAKING A RECOMMENDATION", "STEPS TO ENTER A RECOMMENDATION" & NextLine _
    & "1) SELECT A RESOURCE" & NextLine & "Use the dropdown box to select a resource.  If you can't find it in the dropdown, answer Yes to do a search on the Resource Search window using a slightly different search string.  Please be sure when you leave the field the correct resource is showing." & NextLine _
    & "2) Select the congregation name from the next dropdown box." & NextLine _
    & "3) Next select the Case name form the next dropdown box and enter the date of the recommendation.  If the case does not exist, enter the new case name and complete the information in the popup box that opens." & NextLine _
    & "4) If Center staff made the initial recommendation, select a staff name, and the person to whom the recommendation was made.  " & NextLine _
   & NextLine & "The Notes field is available for other information pertinant to this recommendation, for instance, which aspect of a resource was recommended." & NextLine _
   & NextLine & "If the resource is being funded, select the grant number.  If the congregation discovered the resource without benefit of a Center staff recommendation, fill in the 'This resource was discovered by' field." & NextLine & NextLine _
    & "FEEDBACK" & NextLine _
    & "To Add Feedback to this Recommendation, use the New Feedback button or File --> Add Feedback" & NextLine _
    & "To View All Feedback on this resource, open the Resource Detail window by clicking the green word 'Resource'" & NextLine _
    & "To View the First Feedback associated with this Recommendation, click the green word 'Feedback', or use the Goto menu item.", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    ' COPY CURRENT ID
    Private Sub StatusBar1_DoubleClick(sender As System.Object, e As System.EventArgs) Handles StatusBar1.DoubleClick
        Clipboard.SetText(ThisID)
    End Sub

    'UNDO
    Protected Overrides Function ProcessDialogKey(ByVal keyData As Keys) As Boolean

        If (keyData = System.Windows.Forms.Keys.Escape) Then
            '   modGlobalVar.Msg("undoing", , "form process dialog key")
            modPopup.UndoCtl(Me.ActiveControl)

            Return True  ' True means we've processed the escape key
        Else
            Return MyBase.ProcessDialogKey(keyData)
        End If
    End Function

    'OPEN FIRST RELATED FEEDBACK
    Private Sub fldGotoFeedback_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) _
            Handles fldGotoFeedback.DoubleClick, miGotoFeedback.Click
        'Dim frm As New frmMainResourceFeedback
        If CType(IsNull(Me.fldFeedbackID.Text, 0), Int16) > 0 Then
            modGlobalVar.OpenMainFeedback(Me.fldFeedbackID.Text, Me.fldFeedbackID.Text, Me.cboResource.Text, LocalOrgID) ' Me.cboOrg.SelectedValue)
        End If
    End Sub

    'why does paint still work here and not on frmFeedback????
    Private Sub FeedbackID_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles fldFeedbackID.Paint
        If CType(IsNull(Me.fldFeedbackID.Text, 0), Int16) > 0 Then
            Me.fldGotoFeedback.ForeColor = Color.ForestGreen
            Me.miGotoFeedback.Enabled = True
        Else
            Me.fldGotoFeedback.ForeColor = Color.DarkGray
            Me.miGotoFeedback.Enabled = False
        End If
    End Sub

    'RIGHT CLICK MENU
    Private Sub textbox_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) _
        Handles txtOther.MouseDown, rtbNotes.MouseDown
        If e.Button = System.Windows.Forms.MouseButtons.Right Then
            Dim pp As New ClassRTBContextMenu(sender)
            pp.Show(Me, PointToClient(Control.MousePosition))
        End If
    End Sub

#End Region 'general

End Class

' 