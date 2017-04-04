Imports System
Imports System.Data.SqlClient
Imports System.text

Public Class frmMainResourceFeedback
    Inherits System.Windows.Forms.Form

    Public isLoaded As Boolean = False
    Dim bCancelClose As Boolean
    Dim ctlIdentify, ctlNeutral As Control
    Dim mainTbl As DataTable

    Dim objHowClose As Short 'identify object calling close
    Dim mainDS As DataSet 'for generic module calls like CloseDetailForm
    Dim mainTopic As String 'name of entity of this form - case, contact, etc
    Dim mainBSrce As System.Windows.Forms.BindingSource
    Public ThisOrgID As Integer
    Public bChanged As Boolean = False

    Dim oldValues As New Microsoft.VisualBasic.Collection


#Region "Initialize"

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()
    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
        Forms.Remove(Me)
    End Sub

#End Region 'INITIALIZE

#Region " Windows Form Designer generated code "
    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents cboOrg As InfoCtr.ComboBoxRelaxed
    Friend WithEvents cboCase As InfoCtr.ComboBoxRelaxed
    Friend WithEvents cboStaff As InfoCtr.ComboBoxRelaxed
    Friend WithEvents cboContact As InfoCtr.ComboBoxRelaxed
    Friend WithEvents StatusBar1 As System.Windows.Forms.StatusBar
    Friend WithEvents StatusBarPanel1 As System.Windows.Forms.StatusBarPanel
    Friend WithEvents StatusBarPanel2 As System.Windows.Forms.StatusBarPanel
    Friend WithEvents daspGetOrgName As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents SqlSelectCommand4 As System.Data.SqlClient.SqlCommand
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents daspContactNames As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents SqlSelectCommand2 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlSelectCommand3 As System.Data.SqlClient.SqlCommand
    Friend WithEvents daspCaseNames As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents MainMenu1 As System.Windows.Forms.MainMenu
    Friend WithEvents MenuItem1 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem2 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem3 As System.Windows.Forms.MenuItem
    Friend WithEvents miClose As System.Windows.Forms.MenuItem
    Friend WithEvents miSave As System.Windows.Forms.MenuItem
    Friend WithEvents miCancel As System.Windows.Forms.MenuItem
    Friend WithEvents miDelete As System.Windows.Forms.MenuItem
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents cboApproval As InfoCtr.ComboBoxRelaxed
    Friend WithEvents rtbDid As System.Windows.Forms.RichTextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents rtbComment As System.Windows.Forms.RichTextBox
    Friend WithEvents chkPleased As System.Windows.Forms.CheckBox
    Friend WithEvents chkRecommend As System.Windows.Forms.CheckBox
    Friend WithEvents DsFeedbackCombos As InfoCtr.dsFeedbackOrgs
    Friend WithEvents btnSaveExit As System.Windows.Forms.Button
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents DtFeedback As InfoCtr.DateTextBox
    Friend WithEvents RecommendID As System.Windows.Forms.TextBox
    Friend WithEvents fldGotoRecommend As System.Windows.Forms.TextBox
    Friend WithEvents fldResourceNum As System.Windows.Forms.TextBox
    Friend WithEvents FeedbackID As System.Windows.Forms.TextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents cboResource As InfoCtr.ComboBoxRelaxed
    Friend WithEvents fldGotoResource As System.Windows.Forms.TextBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents MenuItem4 As System.Windows.Forms.MenuItem
    Friend WithEvents miGotoRecommend As System.Windows.Forms.MenuItem
    Friend WithEvents ErrorProvider1 As System.Windows.Forms.ErrorProvider
    Friend WithEvents btnHelp As System.Windows.Forms.Button

    Friend WithEvents miGotoResource As System.Windows.Forms.MenuItem
    Friend WithEvents SqlConnection1 As System.Data.SqlClient.SqlConnection
    Friend WithEvents MainResourceFeedbackBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents DsMainFeedback1 As InfoCtr.dsMainFeedback
    Friend WithEvents MainResourceFeedbackTableAdapter As InfoCtr.dsMainFeedbackTableAdapters.MainResourceFeedbackTableAdapter


    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMainResourceFeedback))
        Me.SqlConnection1 = New System.Data.SqlClient.SqlConnection()
        Me.cboOrg = New InfoCtr.ComboBoxRelaxed()
        Me.MainResourceFeedbackBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.DsMainFeedback1 = New InfoCtr.dsMainFeedback()
        Me.DsFeedbackCombos = New InfoCtr.dsFeedbackOrgs()
        Me.cboCase = New InfoCtr.ComboBoxRelaxed()
        Me.cboStaff = New InfoCtr.ComboBoxRelaxed()
        Me.cboContact = New InfoCtr.ComboBoxRelaxed()
        Me.StatusBar1 = New System.Windows.Forms.StatusBar()
        Me.StatusBarPanel1 = New System.Windows.Forms.StatusBarPanel()
        Me.StatusBarPanel2 = New System.Windows.Forms.StatusBarPanel()
        Me.daspGetOrgName = New System.Data.SqlClient.SqlDataAdapter()
        Me.SqlSelectCommand4 = New System.Data.SqlClient.SqlCommand()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.cboApproval = New InfoCtr.ComboBoxRelaxed()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.rtbDid = New System.Windows.Forms.RichTextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.daspContactNames = New System.Data.SqlClient.SqlDataAdapter()
        Me.SqlSelectCommand2 = New System.Data.SqlClient.SqlCommand()
        Me.daspCaseNames = New System.Data.SqlClient.SqlDataAdapter()
        Me.SqlSelectCommand3 = New System.Data.SqlClient.SqlCommand()
        Me.MainMenu1 = New System.Windows.Forms.MainMenu(Me.components)
        Me.MenuItem1 = New System.Windows.Forms.MenuItem()
        Me.miClose = New System.Windows.Forms.MenuItem()
        Me.MenuItem2 = New System.Windows.Forms.MenuItem()
        Me.miSave = New System.Windows.Forms.MenuItem()
        Me.miCancel = New System.Windows.Forms.MenuItem()
        Me.miDelete = New System.Windows.Forms.MenuItem()
        Me.MenuItem4 = New System.Windows.Forms.MenuItem()
        Me.miGotoRecommend = New System.Windows.Forms.MenuItem()
        Me.miGotoResource = New System.Windows.Forms.MenuItem()
        Me.MenuItem3 = New System.Windows.Forms.MenuItem()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.btnSaveExit = New System.Windows.Forms.Button()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.rtbComment = New System.Windows.Forms.RichTextBox()
        Me.chkPleased = New System.Windows.Forms.CheckBox()
        Me.chkRecommend = New System.Windows.Forms.CheckBox()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.DtFeedback = New InfoCtr.DateTextBox()
        Me.RecommendID = New System.Windows.Forms.TextBox()
        Me.fldGotoRecommend = New System.Windows.Forms.TextBox()
        Me.fldResourceNum = New System.Windows.Forms.TextBox()
        Me.FeedbackID = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.cboResource = New InfoCtr.ComboBoxRelaxed()
        Me.fldGotoResource = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.ErrorProvider1 = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.btnHelp = New System.Windows.Forms.Button()
        Me.MainResourceFeedbackTableAdapter = New InfoCtr.dsMainFeedbackTableAdapters.MainResourceFeedbackTableAdapter()
        CType(Me.MainResourceFeedbackBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsMainFeedback1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsFeedbackCombos, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.StatusBarPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.StatusBarPanel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SqlConnection1
        '
        Me.SqlConnection1.ConnectionString = "Data Source=SOLOMON2008\SOLOMON2008;Initial Catalog=InfoCtr_be;Integrated Securit" & _
    "y=True"
        Me.SqlConnection1.FireInfoMessageEventOnUserErrors = False
        '
        'cboOrg
        '
        Me.cboOrg.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboOrg.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboOrg.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.MainResourceFeedbackBindingSource, "OrgNum", True))
        Me.cboOrg.DataSource = Me.DsFeedbackCombos
        Me.cboOrg.DisplayMember = "GetOrgs.OrgandCity"
        Me.cboOrg.Location = New System.Drawing.Point(114, 126)
        Me.cboOrg.Name = "cboOrg"
        Me.cboOrg.RestrictContentToListItems = True
        Me.cboOrg.Size = New System.Drawing.Size(288, 21)
        Me.cboOrg.TabIndex = 1
        Me.cboOrg.Tag = "Congregation supplying feedback"
        Me.ToolTip1.SetToolTip(Me.cboOrg, "Leave blank if Feedback is not from congregation.l")
        Me.cboOrg.ValueMember = "GetOrgs.OrgID"
        '
        'MainResourceFeedbackBindingSource
        '
        Me.MainResourceFeedbackBindingSource.DataMember = "MainResourceFeedback"
        Me.MainResourceFeedbackBindingSource.DataSource = Me.DsMainFeedback1
        '
        'DsMainFeedback1
        '
        Me.DsMainFeedback1.DataSetName = "dsMainFeedback"
        Me.DsMainFeedback1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'DsFeedbackCombos
        '
        Me.DsFeedbackCombos.CaseSensitive = True
        Me.DsFeedbackCombos.DataSetName = "dsFeedbackOrgs"
        Me.DsFeedbackCombos.Locale = New System.Globalization.CultureInfo("en-US")
        Me.DsFeedbackCombos.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'cboCase
        '
        Me.cboCase.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboCase.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboCase.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.MainResourceFeedbackBindingSource, "CaseNum", True))
        Me.cboCase.DataSource = Me.DsFeedbackCombos
        Me.cboCase.DisplayMember = "luCaseNames.CaseName"
        Me.cboCase.Location = New System.Drawing.Point(114, 155)
        Me.cboCase.Name = "cboCase"
        Me.cboCase.RestrictContentToListItems = True
        Me.cboCase.Size = New System.Drawing.Size(288, 21)
        Me.cboCase.TabIndex = 2
        Me.cboCase.Tag = "Case feedback is related to"
        Me.ToolTip1.SetToolTip(Me.cboCase, "Case feedback is related to if applicable")
        Me.cboCase.ValueMember = "luCaseNames.CaseID"
        '
        'cboStaff
        '
        Me.cboStaff.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboStaff.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboStaff.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.MainResourceFeedbackBindingSource, "FeedbackStaffNum", True))
        Me.cboStaff.DisplayMember = "luContactNames.ContactName"
        Me.cboStaff.Location = New System.Drawing.Point(159, 241)
        Me.cboStaff.Name = "cboStaff"
        Me.cboStaff.RestrictContentToListItems = True
        Me.cboStaff.Size = New System.Drawing.Size(218, 21)
        Me.cboStaff.TabIndex = 4
        Me.cboStaff.Tag = "Center Staff entering feedback"
        Me.ToolTip1.SetToolTip(Me.cboStaff, "Center Staff entering feedback")
        Me.cboStaff.ValueMember = "luContactNames.ContactName"
        '
        'cboContact
        '
        Me.cboContact.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboContact.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboContact.DataBindings.Add(New System.Windows.Forms.Binding("SelectedItem", Me.MainResourceFeedbackBindingSource, "FeedbackBy", True))
        Me.cboContact.Location = New System.Drawing.Point(159, 268)
        Me.cboContact.Name = "cboContact"
        Me.cboContact.RestrictContentToListItems = True
        Me.cboContact.Size = New System.Drawing.Size(220, 21)
        Me.cboContact.TabIndex = 5
        Me.ToolTip1.SetToolTip(Me.cboContact, "Select congregation contact or type other source of feedback.")
        '
        'StatusBar1
        '
        Me.StatusBar1.Location = New System.Drawing.Point(0, 619)
        Me.StatusBar1.Name = "StatusBar1"
        Me.StatusBar1.Panels.AddRange(New System.Windows.Forms.StatusBarPanel() {Me.StatusBarPanel1, Me.StatusBarPanel2})
        Me.StatusBar1.ShowPanels = True
        Me.StatusBar1.Size = New System.Drawing.Size(984, 22)
        Me.StatusBar1.TabIndex = 9
        Me.StatusBar1.Text = "StatusBar1"
        '
        'StatusBarPanel1
        '
        Me.StatusBarPanel1.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents
        Me.StatusBarPanel1.MinWidth = 200
        Me.StatusBarPanel1.Name = "StatusBarPanel1"
        Me.StatusBarPanel1.Width = 200
        '
        'StatusBarPanel2
        '
        Me.StatusBarPanel2.Alignment = System.Windows.Forms.HorizontalAlignment.Right
        Me.StatusBarPanel2.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring
        Me.StatusBarPanel2.Name = "StatusBarPanel2"
        Me.StatusBarPanel2.Text = "Use this window to edit Feedback details."
        Me.StatusBarPanel2.Width = 767
        '
        'daspGetOrgName
        '
        Me.daspGetOrgName.SelectCommand = Me.SqlSelectCommand4
        Me.daspGetOrgName.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "getOrgs", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("OrgID", "OrgID"), New System.Data.Common.DataColumnMapping("OrgandCity", "OrgandCity")})})
        '
        'SqlSelectCommand4
        '
        Me.SqlSelectCommand4.CommandText = "SELECT OrgID, OrgandCity FROM vwGetOrgCity ORDER BY OrgandCity"
        Me.SqlSelectCommand4.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, False, CType(0, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing)})
        '
        'Label1
        '
        Me.Label1.ForeColor = System.Drawing.SystemColors.Highlight
        Me.Label1.Location = New System.Drawing.Point(24, 75)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(378, 48)
        Me.Label1.TabIndex = 10
        Me.Label1.Text = "1.  Select the Congregation from which the Feedback was received.        Leave Co" & _
    "ngregation and Case dropdowns blank if Feedback is from CRG/CTS or other non-con" & _
    "gregational source."
        '
        'Label2
        '
        Me.Label2.ForeColor = System.Drawing.SystemColors.Highlight
        Me.Label2.Location = New System.Drawing.Point(24, 195)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(120, 24)
        Me.Label2.TabIndex = 11
        Me.Label2.Text = "2.  Fill in the details"
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(57, 215)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(96, 24)
        Me.Label3.TabIndex = 12
        Me.Label3.Text = "Date of Feedback"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(65, 155)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(42, 24)
        Me.Label4.TabIndex = 13
        Me.Label4.Text = "Case"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label5
        '
        Me.Label5.Location = New System.Drawing.Point(1, 233)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(152, 40)
        Me.Label5.TabIndex = 14
        Me.Label5.Text = "Staff Entering Feedback"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label6
        '
        Me.Label6.Location = New System.Drawing.Point(12, 268)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(141, 28)
        Me.Label6.TabIndex = 15
        Me.Label6.Text = "Feedback Received From"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label9
        '
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label9.Location = New System.Drawing.Point(10, 10)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(144, 14)
        Me.Label9.TabIndex = 165
        Me.Label9.Text = "FEEDBACK DETAIL"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cboApproval
        '
        Me.cboApproval.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboApproval.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboApproval.DataBindings.Add(New System.Windows.Forms.Binding("SelectedItem", Me.MainResourceFeedbackBindingSource, "ApprovalScale", True))
        Me.cboApproval.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainResourceFeedbackBindingSource, "ApprovalScale", True))
        Me.cboApproval.Items.AddRange(New Object() {"1", "2", "3", "4", "5"})
        Me.cboApproval.Location = New System.Drawing.Point(159, 350)
        Me.cboApproval.Margin = New System.Windows.Forms.Padding(50, 3, 3, 3)
        Me.cboApproval.Name = "cboApproval"
        Me.cboApproval.RestrictContentToListItems = True
        Me.cboApproval.Size = New System.Drawing.Size(99, 21)
        Me.cboApproval.TabIndex = 8
        '
        'Label11
        '
        Me.Label11.Location = New System.Drawing.Point(65, 349)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(88, 24)
        Me.Label11.TabIndex = 171
        Me.Label11.Text = "Approval Scale (1 low to 5 high)"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'rtbDid
        '
        Me.rtbDid.DataBindings.Add(New System.Windows.Forms.Binding("text", Me.MainResourceFeedbackBindingSource, "WhatResourceDid", True))
        Me.rtbDid.Location = New System.Drawing.Point(159, 388)
        Me.rtbDid.MaxLength = 0
        Me.rtbDid.Name = "rtbDid"
        Me.rtbDid.Size = New System.Drawing.Size(390, 212)
        Me.rtbDid.TabIndex = 9
        Me.rtbDid.Text = ""
        '
        'Label12
        '
        Me.Label12.Location = New System.Drawing.Point(39, 388)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(114, 24)
        Me.Label12.TabIndex = 173
        Me.Label12.Text = "What Resource Did"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'daspContactNames
        '
        Me.daspContactNames.SelectCommand = Me.SqlSelectCommand2
        Me.daspContactNames.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "luContactNames", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("ContactID", "ContactID"), New System.Data.Common.DataColumnMapping("ContactName", "ContactName"), New System.Data.Common.DataColumnMapping("ContactStaff", "ContactStaff"), New System.Data.Common.DataColumnMapping("ContactOrgCity", "ContactOrgCity"), New System.Data.Common.DataColumnMapping("OrgID", "OrgID"), New System.Data.Common.DataColumnMapping("Staff", "Staff"), New System.Data.Common.DataColumnMapping("FirstName", "FirstName"), New System.Data.Common.DataColumnMapping("Lastname", "Lastname"), New System.Data.Common.DataColumnMapping("Active", "Active"), New System.Data.Common.DataColumnMapping("PrimaryContact", "PrimaryContact")})})
        '
        'SqlSelectCommand2
        '
        Me.SqlSelectCommand2.CommandText = "dbo.luContactNames"
        Me.SqlSelectCommand2.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlSelectCommand2.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, False, CType(0, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing), New System.Data.SqlClient.SqlParameter("@IDFld", System.Data.SqlDbType.VarChar, 30), New System.Data.SqlClient.SqlParameter("@IDVal", System.Data.SqlDbType.Int, 4)})
        '
        'daspCaseNames
        '
        Me.daspCaseNames.SelectCommand = Me.SqlSelectCommand3
        Me.daspCaseNames.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "luCaseNames", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("CaseID", "CaseID"), New System.Data.Common.DataColumnMapping("CaseName", "CaseName"), New System.Data.Common.DataColumnMapping("OrgNum", "OrgNum"), New System.Data.Common.DataColumnMapping("StatusName", "StatusName")})})
        '
        'SqlSelectCommand3
        '
        Me.SqlSelectCommand3.CommandText = "dbo.luCaseNames"
        Me.SqlSelectCommand3.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlSelectCommand3.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, False, CType(0, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing), New System.Data.SqlClient.SqlParameter("@OrgID", System.Data.SqlDbType.Int, 4)})
        '
        'MainMenu1
        '
        Me.MainMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem1, Me.MenuItem2, Me.MenuItem4, Me.MenuItem3})
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
        Me.miDelete.Text = "Delete"
        '
        'MenuItem4
        '
        Me.MenuItem4.Index = 2
        Me.MenuItem4.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.miGotoRecommend, Me.miGotoResource})
        Me.MenuItem4.Text = "GoTo"
        '
        'miGotoRecommend
        '
        Me.miGotoRecommend.Index = 0
        Me.miGotoRecommend.Text = "Recomendation"
        '
        'miGotoResource
        '
        Me.miGotoResource.Index = 1
        Me.miGotoResource.Text = "Resource"
        '
        'MenuItem3
        '
        Me.MenuItem3.Index = 3
        Me.MenuItem3.Text = "Report"
        '
        'btnDelete
        '
        Me.btnDelete.BackColor = System.Drawing.SystemColors.Control
        Me.btnDelete.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Image = CType(resources.GetObject("btnDelete.Image"), System.Drawing.Image)
        Me.btnDelete.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnDelete.Location = New System.Drawing.Point(3, 1)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(40, 35)
        Me.btnDelete.TabIndex = 212
        Me.btnDelete.Text = "Delete"
        Me.btnDelete.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.btnDelete, "Delete this Feedback")
        Me.btnDelete.UseVisualStyleBackColor = False
        Me.btnDelete.Visible = False
        '
        'btnSaveExit
        '
        Me.btnSaveExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSaveExit.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.btnSaveExit.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSaveExit.Image = CType(resources.GetObject("btnSaveExit.Image"), System.Drawing.Image)
        Me.btnSaveExit.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSaveExit.Location = New System.Drawing.Point(47, 1)
        Me.btnSaveExit.Name = "btnSaveExit"
        Me.btnSaveExit.Size = New System.Drawing.Size(40, 35)
        Me.btnSaveExit.TabIndex = 415
        Me.btnSaveExit.Text = "Close"
        Me.btnSaveExit.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.btnSaveExit, "Saves any changes and Closes this window")
        Me.btnSaveExit.UseVisualStyleBackColor = False
        '
        'Label8
        '
        Me.Label8.Location = New System.Drawing.Point(554, 38)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(112, 24)
        Me.Label8.TabIndex = 220
        Me.Label8.Text = "Feedback Comments"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'rtbComment
        '
        Me.rtbComment.DataBindings.Add(New System.Windows.Forms.Binding("text", Me.MainResourceFeedbackBindingSource, "FeedbackComments", True))
        Me.rtbComment.Location = New System.Drawing.Point(575, 65)
        Me.rtbComment.MaxLength = 0
        Me.rtbComment.Name = "rtbComment"
        Me.rtbComment.Size = New System.Drawing.Size(371, 471)
        Me.rtbComment.TabIndex = 10
        Me.rtbComment.Text = ""
        '
        'chkPleased
        '
        Me.chkPleased.DataBindings.Add(New System.Windows.Forms.Binding("Checked", Me.MainResourceFeedbackBindingSource, "WereYouPleased", True))
        Me.chkPleased.Location = New System.Drawing.Point(159, 295)
        Me.chkPleased.Name = "chkPleased"
        Me.chkPleased.Size = New System.Drawing.Size(255, 24)
        Me.chkPleased.TabIndex = 6
        Me.chkPleased.Text = "Congregation was Pleased with this Resource"
        '
        'chkRecommend
        '
        Me.chkRecommend.DataBindings.Add(New System.Windows.Forms.Binding("Checked", Me.MainResourceFeedbackBindingSource, "WouldYouRecommend", True))
        Me.chkRecommend.Location = New System.Drawing.Point(159, 320)
        Me.chkRecommend.Name = "chkRecommend"
        Me.chkRecommend.Size = New System.Drawing.Size(255, 24)
        Me.chkRecommend.TabIndex = 7
        Me.chkRecommend.Text = "Congregation would Recommend this Resource"
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.SystemColors.Highlight
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Controls.Add(Me.btnSaveExit)
        Me.Panel2.Controls.Add(Me.btnDelete)
        Me.Panel2.Location = New System.Drawing.Point(835, -1)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(88, 40)
        Me.Panel2.TabIndex = 418
        '
        'DtFeedback
        '
        Me.DtFeedback.DataBindings.Add(New System.Windows.Forms.Binding("text", Me.MainResourceFeedbackBindingSource, "FeedbackDate", True))
        Me.DtFeedback.Location = New System.Drawing.Point(159, 215)
        Me.DtFeedback.Name = "DtFeedback"
        Me.DtFeedback.Size = New System.Drawing.Size(99, 20)
        Me.DtFeedback.TabIndex = 3
        '
        'RecommendID
        '
        Me.RecommendID.BackColor = System.Drawing.SystemColors.Control
        Me.RecommendID.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.RecommendID.DataBindings.Add(New System.Windows.Forms.Binding("text", Me.MainResourceFeedbackBindingSource, "RecommendNum", True))
        Me.RecommendID.Enabled = False
        Me.RecommendID.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RecommendID.ForeColor = System.Drawing.SystemColors.GrayText
        Me.RecommendID.Location = New System.Drawing.Point(903, 546)
        Me.RecommendID.Name = "RecommendID"
        Me.RecommendID.ReadOnly = True
        Me.RecommendID.Size = New System.Drawing.Size(43, 11)
        Me.RecommendID.TabIndex = 437
        '
        'fldGotoRecommend
        '
        Me.fldGotoRecommend.BackColor = System.Drawing.SystemColors.Control
        Me.fldGotoRecommend.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fldGotoRecommend.ForeColor = System.Drawing.Color.DarkGray
        Me.fldGotoRecommend.Location = New System.Drawing.Point(805, 542)
        Me.fldGotoRecommend.Name = "fldGotoRecommend"
        Me.fldGotoRecommend.ReadOnly = True
        Me.fldGotoRecommend.Size = New System.Drawing.Size(84, 20)
        Me.fldGotoRecommend.TabIndex = 438
        Me.fldGotoRecommend.Text = "Recommend #"
        '
        'fldResourceNum
        '
        Me.fldResourceNum.BackColor = System.Drawing.SystemColors.Control
        Me.fldResourceNum.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.fldResourceNum.DataBindings.Add(New System.Windows.Forms.Binding("text", Me.MainResourceFeedbackBindingSource, "ResourceNum", True))
        Me.fldResourceNum.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fldResourceNum.ForeColor = System.Drawing.SystemColors.GrayText
        Me.fldResourceNum.Location = New System.Drawing.Point(903, 589)
        Me.fldResourceNum.Name = "fldResourceNum"
        Me.fldResourceNum.ReadOnly = True
        Me.fldResourceNum.Size = New System.Drawing.Size(43, 11)
        Me.fldResourceNum.TabIndex = 434
        '
        'FeedbackID
        '
        Me.FeedbackID.BackColor = System.Drawing.SystemColors.Control
        Me.FeedbackID.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.FeedbackID.DataBindings.Add(New System.Windows.Forms.Binding("text", Me.MainResourceFeedbackBindingSource, "FeedbackID", True))
        Me.FeedbackID.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FeedbackID.ForeColor = System.Drawing.SystemColors.GrayText
        Me.FeedbackID.Location = New System.Drawing.Point(903, 572)
        Me.FeedbackID.Name = "FeedbackID"
        Me.FeedbackID.ReadOnly = True
        Me.FeedbackID.Size = New System.Drawing.Size(43, 11)
        Me.FeedbackID.TabIndex = 433
        '
        'Label14
        '
        Me.Label14.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ForeColor = System.Drawing.SystemColors.ControlDark
        Me.Label14.Location = New System.Drawing.Point(825, 588)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(72, 12)
        Me.Label14.TabIndex = 436
        Me.Label14.Text = "Resource #"
        '
        'Label13
        '
        Me.Label13.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.SystemColors.ControlDark
        Me.Label13.Location = New System.Drawing.Point(825, 568)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(73, 12)
        Me.Label13.TabIndex = 435
        Me.Label13.Text = "Feedback #"
        '
        'cboResource
        '
        Me.cboResource.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboResource.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboResource.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.MainResourceFeedbackBindingSource, "ResourceNum", True))
        Me.cboResource.FormattingEnabled = True
        Me.cboResource.Location = New System.Drawing.Point(114, 38)
        Me.cboResource.Name = "cboResource"
        Me.cboResource.RestrictContentToListItems = True
        Me.cboResource.Size = New System.Drawing.Size(288, 21)
        Me.cboResource.TabIndex = 0
        Me.cboResource.Tag = "Resource"
        '
        'fldGotoResource
        '
        Me.fldGotoResource.BackColor = System.Drawing.SystemColors.Control
        Me.fldGotoResource.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fldGotoResource.ForeColor = System.Drawing.Color.DarkGreen
        Me.fldGotoResource.Location = New System.Drawing.Point(27, 37)
        Me.fldGotoResource.Name = "fldGotoResource"
        Me.fldGotoResource.ReadOnly = True
        Me.fldGotoResource.Size = New System.Drawing.Size(81, 20)
        Me.fldGotoResource.TabIndex = 440
        Me.fldGotoResource.Tag = "Resource"
        Me.fldGotoResource.Text = "Resource"
        '
        'Label16
        '
        Me.Label16.Location = New System.Drawing.Point(12, 123)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(96, 24)
        Me.Label16.TabIndex = 441
        Me.Label16.Text = "Congregation"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'ErrorProvider1
        '
        Me.ErrorProvider1.ContainerControl = Me
        '
        'btnHelp
        '
        Me.btnHelp.Image = CType(resources.GetObject("btnHelp.Image"), System.Drawing.Image)
        Me.btnHelp.Location = New System.Drawing.Point(947, -1)
        Me.btnHelp.Name = "btnHelp"
        Me.btnHelp.Size = New System.Drawing.Size(25, 25)
        Me.btnHelp.TabIndex = 442
        Me.btnHelp.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnHelp.UseVisualStyleBackColor = True
        '
        'MainResourceFeedbackTableAdapter
        '
        Me.MainResourceFeedbackTableAdapter.ClearBeforeFill = True
        '
        'frmMainResourceFeedback
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.CausesValidation = False
        Me.ClientSize = New System.Drawing.Size(984, 641)
        Me.Controls.Add(Me.btnHelp)
        Me.Controls.Add(Me.Label16)
        Me.Controls.Add(Me.fldGotoResource)
        Me.Controls.Add(Me.cboResource)
        Me.Controls.Add(Me.RecommendID)
        Me.Controls.Add(Me.fldGotoRecommend)
        Me.Controls.Add(Me.fldResourceNum)
        Me.Controls.Add(Me.FeedbackID)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.DtFeedback)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.cboContact)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.StatusBar1)
        Me.Controls.Add(Me.chkRecommend)
        Me.Controls.Add(Me.chkPleased)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.rtbComment)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.rtbDid)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.cboApproval)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cboStaff)
        Me.Controls.Add(Me.cboCase)
        Me.Controls.Add(Me.cboOrg)
        Me.Controls.Add(Me.Label5)
        Me.Menu = Me.MainMenu1
        Me.Name = "frmMainResourceFeedback"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Tag = "FEEDBACK"
        Me.Text = "FEEDBACK DETAIL"
        CType(Me.MainResourceFeedbackBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsMainFeedback1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsFeedbackCombos, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.StatusBarPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.StatusBarPanel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    'ENHANCE add Goto for Org, Case, Resource

#Region "Load"

    'LOAD
    Private Sub frmMainFeedback_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        bChanged = False

        Me.StatusBarPanel1.Text = "loading"

        MainResourceFeedbackTableAdapter.Connection = sc
        Me.daspCaseNames.SelectCommand.Connection = sc
        Me.daspContactNames.SelectCommand.Connection = sc
        Me.daspGetOrgName.SelectCommand.Connection = sc

        Me.daspCaseNames.SelectCommand.Parameters("@OrgID").Value = 0
        Me.daspContactNames.SelectCommand.Parameters("@IDVal").Value = 0

        mainTbl = Me.DsMainFeedback1.MainResourceFeedback
        mainDS = Me.DsMainFeedback1
        ctlIdentify = Me.cboResource
        ctlNeutral = Me.btnHelp
        mainTopic = "Resource FeedbacK"
        mainBSrce = Me.MainResourceFeedbackBindingSource

        'LOAD COMBOS
        Me.DsFeedbackCombos.EnforceConstraints = False
        Me.daspGetOrgName.Fill(Me.DsFeedbackCombos.Tables(0))
        Try
            modGlobalVar.LoadStaffCombo(Me.cboStaff, False, StaffComboChoices.CMGI)
        Catch ex As Exception
            modGlobalVar.msg("ERROR: staff combo", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        LoadResourceCombo()

        Forms.Add(Me)
        Me.StatusBarPanel1.Text = "Done"

    End Sub

    'LOAD RESOURCE COMBO
    Public Sub LoadResourceCombo()
        Dim daR As SqlDataAdapter = New SqlDataAdapter
        Dim dsResource As New DataSet
        Dim cmd As New SqlCommand("SELECT ICCResourceID, CASE WHEN Active = 1 then  ResourceName ELSE '~' + ResourceName  END as ResourceName FROM tblResource ORDER by Active Desc, ResourceName", sc)
        daR.SelectCommand = cmd
        If sc.State = ConnectionState.Open Then
            sc.Close()
        End If
        Try
            daR.Fill(dsResource, "luResource")
        Catch ex As Exception
            modGlobalVar.msg("ERROR: can't fill resource combo", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        cboResource.DisplayMember = "ResourceName"
        cboResource.ValueMember = "ICCResourceID"
        cboResource.DataSource = dsResource.Tables("luResource")
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
     Handles MyBase.FormClosing
        Dim arCtls(0) As Control
        Dim ctl As Control
        ctlNeutral.Focus()  'safely calls endedit if was in edit mode

CallUpdate:
        If objHowClose = ObjClose.DontSaveClose Or objHowClose = ObjClose.cancelClose Then
            GoTo CheckRequiredFields
        End If

        If DoUpdate() Then
            e.Cancel = False
        Else
            e.Cancel = True 'don't close form
            GoTo ReleaseForm
        End If

CheckRequiredFields:  'check required fields; allow user to leave anyway if used menu
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
                            bCancelClose = True
                            modGlobalVar.msg("Cancelling Close", "No resource was selected." & NextLine & NextLine & "If this is an unwanted entry, " & NextLine & "select any resource in the dropdown, then click Edit, Delete from the menu.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            GoTo ReleaseForm
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
            ClassOpenForms.frmMainResourceFeedback = Nothing 'reset global var
            objHowClose = Nothing
        Else
        End If
        arCtls = Nothing
        MouseDefault()
    End Sub

    'UPDATE BACK END, return number of records updated, return false if error updating
    Public Function DoUpdate() As Boolean
        Dim i As Integer
        MouseWait()

        If mainDS.HasChanges Or
            mainDS.Tables(0).Rows(0).HasVersion(DataRowVersion.Proposed) Then
            mainBSrce.EndEdit()
        Else
            DoUpdate = True
            GoTo CloseAll
        End If

        If mainDS.HasChanges = True Then 'this catches delete, save, asksave, save/exit, anyclose
            'ASSIGN CHANGE DATE/STAFF
            'mainDAdapt.UpdateCommand.Parameters("@LastChangeStaffNum").Value = usr
            'mainDAdapt.UpdateCommand.Parameters("@LastChangeDate").Value = Now()

UpdateBackend:
            SetStatusBarText("Updating server")
            Try
                ' i =
                Me.MainResourceFeedbackTableAdapter.Update(mainDS)
                DoUpdate = True
            Catch ex As Exception
                modGlobalVar.msg("ERROR: updating " & mainTopic, ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
                DoUpdate = False
            End Try
        Else
            DoUpdate = True 'completed action though no updates to be made
        End If

CloseAll:
        SetStatusBarText("Update routine complete")
        MouseDefault()
    End Function

    'CHECK REQUIRED FIELDS w Error Provider
    Private Function CheckRequired() As Control()
        Dim arCtls(0) As Control
        Dim ctl As Control
        Dim i As Integer = 0

        'Resource
        ctl = ctlIdentify
        If modGlobalVar.ValidateBoundDD(ctl, True, Me.ErrorProvider1, ObjClose.btnSaveExit) = usrInput.OK Then
        Else
            arCtls(i) = ctl
            i = i + 1
            ReDim Preserve arCtls(arCtls.GetUpperBound(0) + 1)
        End If

        'Staff - required
        ctl = Me.cboStaff
        If modGlobalVar.ValidateBoundDD(ctl, True, Me.ErrorProvider1, ObjClose.btnSaveExit) = usrInput.OK Then
        Else
            arCtls(i) = ctl
            i = i + 1
            ReDim Preserve arCtls(arCtls.GetUpperBound(0) + 1)
        End If

        arCtls(i) = ctlNeutral
        Return arCtls
    End Function

#End Region

#Region "Edit buttons"

    'SAVE
    Private Sub miSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles miSave.Click
        mainBSrce.EndEdit()
        DoUpdate()
    End Sub

    'CANCEL CHANGES
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles miCancel.Click
        mainBSrce.CancelEdit()
        bChanged = False
        Me.StatusBarPanel1.Text = "Changes cancelled"
    End Sub

    'DELETE CASE
    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles miDelete.Click, btnDelete.Click

        If modGlobalVar.msg("CONFIRM DELETE", mainTopic & ": " + IsNull(ctlIdentify.Text, "") & NextLine & " WILL BE DELETED and the window closed.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            objHowClose = ObjClose.miDelete
            mainBSrce.RemoveCurrent() 'RemoveAt(0)
            Me.Close()

            'if delete here, reload Orgs is triggered causing error
            'so delete is in close method
            ' Me.Close()
        End If
    End Sub

#End Region

#Region "Validation"

    'RESOURCE - required field, allow search
    Private Sub cboResource_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) _
         Handles cboResource.Validating
        Dim usrSel As New usrInput
        usrSel = modGlobalVar.ValidateCBO(sender, IsNull(sender.tag, sender.name), True, CanAddNew.Search)

        Select Case usrSel
            Case Is = usrInput.Yes
                Dim frm As New frmSrchResource
                frm.ShowDialog()
                LoadResourceCombo()
                Me.cboResource.SelectedIndex = -1
                Me.cboResource.Focus()
            Case Else
                ' SetError(Me.ErrorProvider1, sender, usrSel, True)
        End Select
        usrSel = Nothing

    End Sub

    'RELOAD ORG BASED COMBOS
    Private Sub cboOrg_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboOrg.SelectedIndexChanged
        If isLoaded Then
            If cboOrg.SelectedValue > 0 Then
                LoadOrgBasedCombos(sender.selectedvalue)
            End If
        End If
        ThisOrgID = Me.cboOrg.SelectedValue
    End Sub

    'VALIDATE STAFF - required
    Private Sub cboStaff_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) _
        Handles cboStaff.Validating
        If modGlobalVar.ValidateBoundDD(sender, True, Me.ErrorProvider1, ObjClose.CloseByControl) = usrInput.OK Then
            e.Cancel = False
        Else
            e.Cancel = True
            sender.droppeddown = True
        End If
    End Sub

    'VALIDATE DATE
    Private Sub dtFeedback_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles DtFeedback.Validating
        e.Cancel = modGlobalVar.ValidateDateA(sender, Me.ErrorProvider1)
    End Sub

    'VALIDATE CASE CBO - optional, add new --OR should CASE ALREADY EXIST for FEEDBACK? No
    Private Sub cboCase_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cboCase.Validating
        If modGlobalVar.ValidateCBO(sender, IsNull(sender.tag, sender.name), False, CanAddNew.AddNew) = usrInput.Yes Then
            modPopup.NewCase(Me.cboOrg.SelectedValue, Me.cboCase.Text, usr, "Case", Me, Me.cboOrg.Text, 0)
            LoadCases(Me.cboOrg.SelectedValue)
            cboCase.Focus()
            cboCase.DroppedDown = True
        End If
    End Sub

#End Region 'validation

#Region "General"

    'UNDO
    Protected Overrides Function ProcessDialogKey(ByVal keyData As Keys) As Boolean
        If keyData = System.Windows.Forms.Keys.Escape Then
            modPopup.UndoCtl(Me.ActiveControl)
            bChanged = True
            Return True  ' True means we've processed the escape key
        Else
            Return MyBase.ProcessDialogKey(keyData)
        End If
    End Function

    'SET STATUS BAR LEFT TEXT
    Private Sub SetStatusBarText(ByVal str As String)
        Me.StatusBar1.Panels(0).Text = str
    End Sub

    ' COPY CURRENT ID
    Private Sub StatusBar1_DoubleClick(sender As System.Object, e As System.EventArgs) Handles StatusBar1.DoubleClick
        Clipboard.SetText(ThisOrgID)
    End Sub

    'FORMAT RECOMMEND GOTO
    Private Sub RecommendID_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RecommendID.TextChanged
        If CType(IsNull(Me.RecommendID.Text, 0), Int16) > 0 Then
            Me.fldGotoRecommend.ForeColor = Color.ForestGreen
            Me.miGotoRecommend.Enabled = True
        Else
            Me.fldGotoRecommend.ForeColor = Color.DarkGray
            Me.miGotoRecommend.Enabled = False
        End If
    End Sub

    'SHOW RTB MENU
    Private Sub rtbComment_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) _
        Handles rtbComment.MouseDown, rtbDid.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Right Then
            Dim pp As New ClassRTBContextMenu(sender)
            pp.Show(Me, PointToClient(Control.MousePosition))
        End If
    End Sub

#End Region

#Region "Load OrgBased Combos"

    'CALL REFRESH COMBOS
    Public Sub LoadOrgBasedCombos(ByVal id As Integer) 'orgid, caseid

        If objHowClose = ObjClose.miDelete Then
            Exit Sub 'else errors out
        Else
            objHowClose = ObjClose.btnSaveExit
        End If

GetOldValues:
        oldValues.Clear()
        oldValues.Add(IsNull(Me.cboCase.Text, ""), "Case")
        oldValues.Add(IsNull(Me.cboContact.Text, ""), "Contact")
LoadCombos:

        LoadCases(id)
        LoadContacts(id)

    End Sub

    'LOAD CASES
    Private Sub LoadCases(ByVal id As Integer)

        Me.DsFeedbackCombos.Tables(1).Clear()
        Me.DsFeedbackCombos.EnforceConstraints = False
        Try
            Me.daspCaseNames.SelectCommand.Parameters("@OrgID").Value = id   ' Me.dsMainFeedback1.tblResourceFeedback.Rows(0).Item("OrgNum")
            daspCaseNames.Fill(Me.DsFeedbackCombos.luCaseNames) ', "luCaseNames")
        Catch ex As Exception
            modGlobalVar.msg("ERROR: can't fill case combo", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
        End Try

    End Sub

    'LOAD CONTACTS
    Private Sub LoadContacts(ByVal id As Integer)
        Dim strExistingContact As String

        strExistingContact = IsNull(Me.cboContact.Text, "")

        Me.cboContact.Items.Clear()
        If strExistingContact = String.Empty Then
        Else
            Me.cboContact.Items.Add(Me.cboContact.Text)
        End If
        Me.DsFeedbackCombos.Tables(2).Clear()
        Me.daspContactNames.SelectCommand.Parameters("@IDVal").Value = id
        Try
            daspContactNames.Fill(Me.DsFeedbackCombos.luContactNames)
            For Each r As DataRow In Me.DsFeedbackCombos.luContactNames.Rows
                Me.cboContact.Items.Add(r.Item("ContactName"))
            Next
        Catch ex As Exception
            modGlobalVar.msg("ERROR: can't fill contact combo", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
        End Try
        strExistingContact = Nothing
    End Sub

#End Region

#Region "Open Other Forms"

    'OPEN RELATED Recommendation
    Private Sub fldGotoRecommend_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) _
            Handles fldGotoRecommend.DoubleClick, miGotoRecommend.Click
        If CType(IsNull(Me.RecommendID.Text, 0), Int16) > 0 Then
            modGlobalVar.OpenMainRecommend(Me.RecommendID.Text, Me.cboResource.Text, ThisOrgID)
        End If
    End Sub

    'OPEN RESOURCE FORM
    Private Sub fldGotoResource_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles fldGotoResource.DoubleClick, miGotoResource.Click
        If Me.cboResource.SelectedIndex > -1 Then
            modGlobalVar.OpenResourceChoice(Me.cboResource.SelectedValue, Me.cboResource.Text)
        End If
    End Sub

#End Region

End Class


