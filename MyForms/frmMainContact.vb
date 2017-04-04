Option Explicit On
Imports System
Imports System.Data.SqlClient
Imports System.Text
Imports cEmail = ClassLibrary1.EmailConversation

Public Class frmMainContact
    Inherits System.Windows.Forms.Form

    Public isLoaded As Boolean = False
    Dim bSpecialMail As Boolean = False 'so load popup only first time required
    Dim tbl As DataTable  'flexible datagrid
    Dim dv As DataView 'filter for each datagrid
    Dim statusM, statusS1, statusS2 As String 'filter text for status bar
    Dim hti As System.Windows.Forms.DataGrid.HitTestInfo
    Dim Who As String
    Dim pref, suff As String
    Dim enumConvers, enumRegistr, EnumRecommend As structHeadings
    '===== for Detail form  closing routines
    Dim ctlIdentify As Control 'fields for delete and messages
    Dim ctlNeutral As Control 'will never be invalidated
    Dim objHowClose As Short 'Object = Me.btnSaveExit 'identify object calling close
    Dim mainTopic As String 'name of entity of this form - case, contact, etc
    Dim mainDS As DataSet 'for generic module calls like CloseDetailForm
    Dim maintbl As DataTable
    Dim mainDAdapt As SqlDataAdapter
    Dim mainBSrce As System.Windows.Forms.BindingSource
    Dim bMailingExists As Boolean 'if mailing exists, offer to delete
    Public ThisID, LocalOrgID As Integer
  
    '======

#Region "Initialize"
    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
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

#End Region 'initialize

#Region " Windows Form Designer generated code "

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents Label27 As System.Windows.Forms.Label

    Friend WithEvents SqlSelectCommand As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlUpdateCommand As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents miAddConversation As System.Windows.Forms.MenuItem
    Friend WithEvents miAddRegistration As System.Windows.Forms.MenuItem
    Friend WithEvents miIntro As System.Windows.Forms.MenuItem
    Friend WithEvents MainContactBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents fldOrgNum As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents lblSelectedID As System.Windows.Forms.Label
    Friend WithEvents lblSelectedWhat As System.Windows.Forms.Label
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents btnSaveExit As System.Windows.Forms.Button
    Friend WithEvents btnSend As System.Windows.Forms.Button
    Friend WithEvents btnNew As System.Windows.Forms.Button
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents fldMailListCSV As System.Windows.Forms.TextBox
    Friend WithEvents btnMailListAdd As System.Windows.Forms.Button
    Friend WithEvents btnHelp As System.Windows.Forms.Button
    Friend WithEvents ErrorProvider1 As System.Windows.Forms.ErrorProvider
    Friend WithEvents txtCountry As System.Windows.Forms.TextBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents chkEmail As System.Windows.Forms.CheckBox
    Friend WithEvents chkPostal As System.Windows.Forms.CheckBox
    Friend WithEvents txtSecondPhone As System.Windows.Forms.TextBox
    Friend WithEvents Label34 As System.Windows.Forms.Label
    Friend WithEvents Label35 As System.Windows.Forms.Label
    Friend WithEvents DataGridTextBoxColumn5 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents TextBox3 As System.Windows.Forms.TextBox
    Friend WithEvents Label36 As System.Windows.Forms.Label
    Friend WithEvents fldGoToOrg As System.Windows.Forms.TextBox
    Friend WithEvents miGotoOrg As System.Windows.Forms.MenuItem
    Friend WithEvents DataGridTextBoxColumn6 As System.Windows.Forms.DataGridTextBoxColumn

    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TextBox2 As System.Windows.Forms.TextBox
    '  Friend WithEvents DsFullContact1 As InfoCtr.dsFullContact
    Friend WithEvents MainMenu1 As System.Windows.Forms.MainMenu
    Friend WithEvents MenuItem1 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem2 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem3 As System.Windows.Forms.MenuItem
    Friend WithEvents miAdd As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem5 As System.Windows.Forms.MenuItem
    Friend WithEvents miLetter As System.Windows.Forms.MenuItem
    Friend WithEvents miEmail As System.Windows.Forms.MenuItem
    Friend WithEvents pgConversation As System.Windows.Forms.TabPage
    Friend WithEvents pgExtras As System.Windows.Forms.TabPage
    Friend WithEvents pgAddress As System.Windows.Forms.TabPage
    Friend WithEvents fldCreateStaff As System.Windows.Forms.TextBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents fldLastChangeDate As System.Windows.Forms.TextBox
    Friend WithEvents fldCreateDate As System.Windows.Forms.TextBox
    Friend WithEvents fldLastChangeStaff As System.Windows.Forms.TextBox
    Friend WithEvents DsMainContact1 As InfoCtr.dsMainContact
    Friend WithEvents daMainContact As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents miClose As System.Windows.Forms.MenuItem
    Friend WithEvents miSave As System.Windows.Forms.MenuItem
    Friend WithEvents miCancel As System.Windows.Forms.MenuItem
    Friend WithEvents miDelete As System.Windows.Forms.MenuItem
    Friend WithEvents mmGoto As System.Windows.Forms.MenuItem
    Friend WithEvents StatusBar1 As System.Windows.Forms.StatusBar
    Friend WithEvents StatusBarPanel1 As System.Windows.Forms.StatusBarPanel
    Friend WithEvents StatusBarPanelID As System.Windows.Forms.StatusBarPanel
    Friend WithEvents StatusBarPanel2 As System.Windows.Forms.StatusBarPanel
    Friend WithEvents miGotoRegistration As System.Windows.Forms.MenuItem
    Friend WithEvents miGoToConversation As System.Windows.Forms.MenuItem
    Friend WithEvents miGoToRecommend As System.Windows.Forms.MenuItem
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents txtGoesBy As System.Windows.Forms.TextBox
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents txtSuffix As System.Windows.Forms.TextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents txtMI As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtLastName As System.Windows.Forms.TextBox
    Friend WithEvents txtFirstName As System.Windows.Forms.TextBox
    Friend WithEvents RichTextBox2 As System.Windows.Forms.RichTextBox
    Friend WithEvents txtJobTitle As System.Windows.Forms.TextBox
    Friend WithEvents cboClergy As InfoCtr.ComboBoxRelaxed
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents txtPrefix As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents pgRegistration As System.Windows.Forms.TabPage
    Friend WithEvents pgRecommendation As System.Windows.Forms.TabPage
    Friend WithEvents pnlGrid As System.Windows.Forms.Panel
    Friend WithEvents grdMain As System.Windows.Forms.DataGrid
    Friend WithEvents DataGridTableStyle2 As System.Windows.Forms.DataGridTableStyle
    Friend WithEvents DataGridTableStyle1 As System.Windows.Forms.DataGridTableStyle
    Friend WithEvents DataGridTextBoxColumn12 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn1 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn2 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn3 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn4 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents lblExtra As System.Windows.Forms.Label
    Friend WithEvents pnlExtras As System.Windows.Forms.Panel
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents pnlMailingList As System.Windows.Forms.Panel
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents chkRemoveEmail As System.Windows.Forms.CheckBox
    Friend WithEvents chkUseHome As System.Windows.Forms.CheckBox
    Friend WithEvents chkRemovePostal As System.Windows.Forms.CheckBox
    Friend WithEvents chkActive As System.Windows.Forms.CheckBox
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents txtCellPhone As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtHomePhone As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtEmail As System.Windows.Forms.TextBox
    Friend WithEvents txtHomeFax As System.Windows.Forms.TextBox
    Friend WithEvents txtZip As System.Windows.Forms.TextBox
    Friend WithEvents txtState As System.Windows.Forms.TextBox
    Friend WithEvents txtStreet As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents DataGridTextBoxColumn13 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn15 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn14 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn16 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn17 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTableStyle3 As System.Windows.Forms.DataGridTableStyle
    Friend WithEvents DataGridTextBoxColumn18 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn19 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn20 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn21 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents miHelp As System.Windows.Forms.MenuItem
    Friend WithEvents txtCity As System.Windows.Forms.TextBox
    Friend WithEvents CheckBox5 As System.Windows.Forms.CheckBox
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents cboGender As InfoCtr.ComboBoxRelaxed
    Friend WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents TextBox9 As System.Windows.Forms.TextBox
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents txtWorkPhone As System.Windows.Forms.TextBox
    Friend WithEvents txtPrimPhone As System.Windows.Forms.TextBox
    Friend WithEvents pnlTabs As System.Windows.Forms.Panel
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMainContact))
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.pgAddress = New System.Windows.Forms.TabPage()
        Me.pgConversation = New System.Windows.Forms.TabPage()
        Me.pgRegistration = New System.Windows.Forms.TabPage()
        Me.pgRecommendation = New System.Windows.Forms.TabPage()
        Me.pgExtras = New System.Windows.Forms.TabPage()
        Me.fldCreateStaff = New System.Windows.Forms.TextBox()
        Me.MainContactBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.DsMainContact1 = New InfoCtr.dsMainContact()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.fldLastChangeDate = New System.Windows.Forms.TextBox()
        Me.fldCreateDate = New System.Windows.Forms.TextBox()
        Me.fldLastChangeStaff = New System.Windows.Forms.TextBox()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.daMainContact = New System.Data.SqlClient.SqlDataAdapter()
        Me.SqlSelectCommand = New System.Data.SqlClient.SqlCommand()
        Me.SqlUpdateCommand = New System.Data.SqlClient.SqlCommand()
        Me.MainMenu1 = New System.Windows.Forms.MainMenu(Me.components)
        Me.MenuItem1 = New System.Windows.Forms.MenuItem()
        Me.miAdd = New System.Windows.Forms.MenuItem()
        Me.miAddConversation = New System.Windows.Forms.MenuItem()
        Me.miAddRegistration = New System.Windows.Forms.MenuItem()
        Me.miClose = New System.Windows.Forms.MenuItem()
        Me.MenuItem2 = New System.Windows.Forms.MenuItem()
        Me.miSave = New System.Windows.Forms.MenuItem()
        Me.miCancel = New System.Windows.Forms.MenuItem()
        Me.miDelete = New System.Windows.Forms.MenuItem()
        Me.MenuItem3 = New System.Windows.Forms.MenuItem()
        Me.miLetter = New System.Windows.Forms.MenuItem()
        Me.miEmail = New System.Windows.Forms.MenuItem()
        Me.miIntro = New System.Windows.Forms.MenuItem()
        Me.MenuItem5 = New System.Windows.Forms.MenuItem()
        Me.mmGoto = New System.Windows.Forms.MenuItem()
        Me.miGoToConversation = New System.Windows.Forms.MenuItem()
        Me.miGotoOrg = New System.Windows.Forms.MenuItem()
        Me.miGotoRegistration = New System.Windows.Forms.MenuItem()
        Me.miGoToRecommend = New System.Windows.Forms.MenuItem()
        Me.miHelp = New System.Windows.Forms.MenuItem()
        Me.StatusBar1 = New System.Windows.Forms.StatusBar()
        Me.StatusBarPanel1 = New System.Windows.Forms.StatusBarPanel()
        Me.StatusBarPanelID = New System.Windows.Forms.StatusBarPanel()
        Me.StatusBarPanel2 = New System.Windows.Forms.StatusBarPanel()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.fldGoToOrg = New System.Windows.Forms.TextBox()
        Me.CheckBox5 = New System.Windows.Forms.CheckBox()
        Me.txtPrefix = New System.Windows.Forms.TextBox()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.chkRemoveEmail = New System.Windows.Forms.CheckBox()
        Me.chkRemovePostal = New System.Windows.Forms.CheckBox()
        Me.chkActive = New System.Windows.Forms.CheckBox()
        Me.btnSaveExit = New System.Windows.Forms.Button()
        Me.btnSend = New System.Windows.Forms.Button()
        Me.btnNew = New System.Windows.Forms.Button()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.txtEmail = New System.Windows.Forms.TextBox()
        Me.btnMailListAdd = New System.Windows.Forms.Button()
        Me.cboClergy = New InfoCtr.ComboBoxRelaxed()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.lblSelectedID = New System.Windows.Forms.Label()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.lblSelectedWhat = New System.Windows.Forms.Label()
        Me.cboGender = New InfoCtr.ComboBoxRelaxed()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.fldOrgNum = New System.Windows.Forms.Label()
        Me.txtGoesBy = New System.Windows.Forms.TextBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.txtSuffix = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.txtMI = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtLastName = New System.Windows.Forms.TextBox()
        Me.txtFirstName = New System.Windows.Forms.TextBox()
        Me.RichTextBox2 = New System.Windows.Forms.RichTextBox()
        Me.txtJobTitle = New System.Windows.Forms.TextBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.pnlTabs = New System.Windows.Forms.Panel()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.fldMailListCSV = New System.Windows.Forms.TextBox()
        Me.pnlMailingList = New System.Windows.Forms.Panel()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.chkEmail = New System.Windows.Forms.CheckBox()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.chkUseHome = New System.Windows.Forms.CheckBox()
        Me.chkPostal = New System.Windows.Forms.CheckBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.pnlExtras = New System.Windows.Forms.Panel()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.lblExtra = New System.Windows.Forms.Label()
        Me.TextBox3 = New System.Windows.Forms.TextBox()
        Me.Label36 = New System.Windows.Forms.Label()
        Me.pnlGrid = New System.Windows.Forms.Panel()
        Me.grdMain = New System.Windows.Forms.DataGrid()
        Me.DataGridTableStyle3 = New System.Windows.Forms.DataGridTableStyle()
        Me.DataGridTextBoxColumn18 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn19 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn20 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn21 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTableStyle2 = New System.Windows.Forms.DataGridTableStyle()
        Me.DataGridTextBoxColumn13 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn15 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn14 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn16 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn17 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTableStyle1 = New System.Windows.Forms.DataGridTableStyle()
        Me.DataGridTextBoxColumn12 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn1 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn2 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn3 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn4 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn5 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn6 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.txtSecondPhone = New System.Windows.Forms.TextBox()
        Me.txtCountry = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.TextBox9 = New System.Windows.Forms.TextBox()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.txtPrimPhone = New System.Windows.Forms.TextBox()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.txtWorkPhone = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.txtCellPhone = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtHomePhone = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtHomeFax = New System.Windows.Forms.TextBox()
        Me.txtZip = New System.Windows.Forms.TextBox()
        Me.txtState = New System.Windows.Forms.TextBox()
        Me.txtCity = New System.Windows.Forms.TextBox()
        Me.txtStreet = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.SqlCommand1 = New System.Data.SqlClient.SqlCommand()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.btnHelp = New System.Windows.Forms.Button()
        Me.ErrorProvider1 = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.TabControl1.SuspendLayout()
        CType(Me.MainContactBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsMainContact1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.StatusBarPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.StatusBarPanelID, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.StatusBarPanel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.pnlTabs.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.pnlMailingList.SuspendLayout()
        Me.pnlExtras.SuspendLayout()
        Me.pnlGrid.SuspendLayout()
        CType(Me.grdMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.pgAddress)
        Me.TabControl1.Controls.Add(Me.pgConversation)
        Me.TabControl1.Controls.Add(Me.pgRegistration)
        Me.TabControl1.Controls.Add(Me.pgRecommendation)
        Me.TabControl1.Controls.Add(Me.pgExtras)
        Me.TabControl1.Location = New System.Drawing.Point(293, 70)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(624, 23)
        Me.TabControl1.TabIndex = 18
        '
        'pgAddress
        '
        Me.pgAddress.Location = New System.Drawing.Point(4, 22)
        Me.pgAddress.Name = "pgAddress"
        Me.pgAddress.Size = New System.Drawing.Size(616, 0)
        Me.pgAddress.TabIndex = 0
        Me.pgAddress.Tag = "HOME ADDRESS"
        Me.pgAddress.Text = "  HOME ADDRESS  "
        Me.pgAddress.UseVisualStyleBackColor = True
        '
        'pgConversation
        '
        Me.pgConversation.Location = New System.Drawing.Point(4, 22)
        Me.pgConversation.Name = "pgConversation"
        Me.pgConversation.Size = New System.Drawing.Size(616, 0)
        Me.pgConversation.TabIndex = 1
        Me.pgConversation.Tag = "CONVERSATIONS"
        Me.pgConversation.Text = "  CONVERSATIONS  "
        Me.pgConversation.UseVisualStyleBackColor = True
        Me.pgConversation.Visible = False
        '
        'pgRegistration
        '
        Me.pgRegistration.Location = New System.Drawing.Point(4, 22)
        Me.pgRegistration.Name = "pgRegistration"
        Me.pgRegistration.Size = New System.Drawing.Size(616, 0)
        Me.pgRegistration.TabIndex = 3
        Me.pgRegistration.Tag = "REGISTRATIONS"
        Me.pgRegistration.Text = "  REGISTRATIONS"
        Me.pgRegistration.UseVisualStyleBackColor = True
        '
        'pgRecommendation
        '
        Me.pgRecommendation.Location = New System.Drawing.Point(4, 22)
        Me.pgRecommendation.Name = "pgRecommendation"
        Me.pgRecommendation.Size = New System.Drawing.Size(616, 0)
        Me.pgRecommendation.TabIndex = 5
        Me.pgRecommendation.Tag = "RECOMMENDATIONS"
        Me.pgRecommendation.Text = " RECOMMENDATIONS"
        Me.pgRecommendation.UseVisualStyleBackColor = True
        '
        'pgExtras
        '
        Me.pgExtras.Location = New System.Drawing.Point(4, 22)
        Me.pgExtras.Name = "pgExtras"
        Me.pgExtras.Size = New System.Drawing.Size(616, 0)
        Me.pgExtras.TabIndex = 4
        Me.pgExtras.Tag = "EXTRAS"
        Me.pgExtras.Text = "  EXTRAS  "
        Me.pgExtras.UseVisualStyleBackColor = True
        '
        'fldCreateStaff
        '
        Me.fldCreateStaff.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainContactBindingSource, "CreateStaffNum", True))
        Me.fldCreateStaff.Location = New System.Drawing.Point(199, 1)
        Me.fldCreateStaff.Name = "fldCreateStaff"
        Me.fldCreateStaff.ReadOnly = True
        Me.fldCreateStaff.Size = New System.Drawing.Size(152, 20)
        Me.fldCreateStaff.TabIndex = 170
        Me.fldCreateStaff.Tag = "Created by"
        '
        'MainContactBindingSource
        '
        Me.MainContactBindingSource.DataMember = "MainContact"
        Me.MainContactBindingSource.DataSource = Me.DsMainContact1
        '
        'DsMainContact1
        '
        Me.DsMainContact1.DataSetName = "dsMainContact"
        Me.DsMainContact1.Locale = New System.Globalization.CultureInfo("en-US")
        Me.DsMainContact1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'Label19
        '
        Me.Label19.Location = New System.Drawing.Point(7, 28)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(88, 23)
        Me.Label19.TabIndex = 169
        Me.Label19.Text = "Last Changed"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label18
        '
        Me.Label18.Location = New System.Drawing.Point(47, 9)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(47, 14)
        Me.Label18.TabIndex = 168
        Me.Label18.Text = "Created"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'fldLastChangeDate
        '
        Me.fldLastChangeDate.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.fldLastChangeDate.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainContactBindingSource, "LastChangeDate", True))
        Me.fldLastChangeDate.ForeColor = System.Drawing.SystemColors.WindowText
        Me.fldLastChangeDate.Location = New System.Drawing.Point(103, 28)
        Me.fldLastChangeDate.Name = "fldLastChangeDate"
        Me.fldLastChangeDate.ReadOnly = True
        Me.fldLastChangeDate.Size = New System.Drawing.Size(86, 20)
        Me.fldLastChangeDate.TabIndex = 166
        '
        'fldCreateDate
        '
        Me.fldCreateDate.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainContactBindingSource, "CreateDate", True))
        Me.fldCreateDate.Location = New System.Drawing.Point(103, 1)
        Me.fldCreateDate.Name = "fldCreateDate"
        Me.fldCreateDate.ReadOnly = True
        Me.fldCreateDate.Size = New System.Drawing.Size(86, 20)
        Me.fldCreateDate.TabIndex = 165
        '
        'fldLastChangeStaff
        '
        Me.fldLastChangeStaff.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainContactBindingSource, "LastChangeStaffNum", True))
        Me.fldLastChangeStaff.Location = New System.Drawing.Point(199, 28)
        Me.fldLastChangeStaff.Name = "fldLastChangeStaff"
        Me.fldLastChangeStaff.ReadOnly = True
        Me.fldLastChangeStaff.Size = New System.Drawing.Size(152, 20)
        Me.fldLastChangeStaff.TabIndex = 163
        Me.fldLastChangeStaff.Tag = "Last Changed by"
        '
        'TextBox2
        '
        Me.TextBox2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.TextBox2.BackColor = System.Drawing.SystemColors.ControlLight
        Me.TextBox2.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBox2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox2.Location = New System.Drawing.Point(424, 680)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(65, 13)
        Me.TextBox2.TabIndex = 83
        '
        'daMainContact
        '
        Me.daMainContact.SelectCommand = Me.SqlSelectCommand
        Me.daMainContact.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "MainContact", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("ContactID", "ContactID"), New System.Data.Common.DataColumnMapping("OrgNum", "OrgNum"), New System.Data.Common.DataColumnMapping("Prefix", "Prefix"), New System.Data.Common.DataColumnMapping("FirstName", "FirstName"), New System.Data.Common.DataColumnMapping("MI", "MI"), New System.Data.Common.DataColumnMapping("Lastname", "Lastname"), New System.Data.Common.DataColumnMapping("Suffix", "Suffix"), New System.Data.Common.DataColumnMapping("Goesby", "Goesby"), New System.Data.Common.DataColumnMapping("Staff", "Staff"), New System.Data.Common.DataColumnMapping("JobTitle", "JobTitle"), New System.Data.Common.DataColumnMapping("Responsibilities", "Responsibilities"), New System.Data.Common.DataColumnMapping("PrimaryContact", "PrimaryContact"), New System.Data.Common.DataColumnMapping("Street1", "Street1"), New System.Data.Common.DataColumnMapping("Street2", "Street2"), New System.Data.Common.DataColumnMapping("City", "City"), New System.Data.Common.DataColumnMapping("State", "State"), New System.Data.Common.DataColumnMapping("Zip", "Zip"), New System.Data.Common.DataColumnMapping("UseHome", "UseHome"), New System.Data.Common.DataColumnMapping("Phone", "Phone"), New System.Data.Common.DataColumnMapping("Fax", "Fax"), New System.Data.Common.DataColumnMapping("Email", "Email"), New System.Data.Common.DataColumnMapping("Notes", "Notes"), New System.Data.Common.DataColumnMapping("Active", "Active"), New System.Data.Common.DataColumnMapping("WorkPhone", "WorkPhone"), New System.Data.Common.DataColumnMapping("WorkExtension", "WorkExtension"), New System.Data.Common.DataColumnMapping("PagerNumber", "PagerNumber"), New System.Data.Common.DataColumnMapping("CellPhone", "CellPhone"), New System.Data.Common.DataColumnMapping("ReferredBy", "ReferredBy"), New System.Data.Common.DataColumnMapping("PrintFlag", "PrintFlag"), New System.Data.Common.DataColumnMapping("Gender", "Gender"), New System.Data.Common.DataColumnMapping("Ethnicity", "Ethnicity"), New System.Data.Common.DataColumnMapping("SpouseName", "SpouseName"), New System.Data.Common.DataColumnMapping("CreateDate", "CreateDate"), New System.Data.Common.DataColumnMapping("CreateStaffNum", "CreateStaffNum"), New System.Data.Common.DataColumnMapping("LastChangeDate", "LastChangeDate"), New System.Data.Common.DataColumnMapping("LastChangeStaffNum", "LastChangeStaffNum"), New System.Data.Common.DataColumnMapping("WarningFlag", "WarningFlag"), New System.Data.Common.DataColumnMapping("MailListEmail", "MailListEmail"), New System.Data.Common.DataColumnMapping("MailListPostal", "MailListPostal"), New System.Data.Common.DataColumnMapping("Country", "Country"), New System.Data.Common.DataColumnMapping("PrimaryPhone", "PrimaryPhone"), New System.Data.Common.DataColumnMapping("SecondaryPhone", "SecondaryPhone"), New System.Data.Common.DataColumnMapping("OnlineUID", "OnlineUID"), New System.Data.Common.DataColumnMapping("Stamped", "Stamped"), New System.Data.Common.DataColumnMapping("lblCounts", "lblCounts")})})
        Me.daMainContact.UpdateCommand = Me.SqlUpdateCommand
        '
        'SqlSelectCommand
        '
        Me.SqlSelectCommand.CommandText = "dbo.MainContact"
        Me.SqlSelectCommand.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlSelectCommand.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, False, CType(0, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing), New System.Data.SqlClient.SqlParameter("@ID", System.Data.SqlDbType.Int, 4)})
        '
        'SqlUpdateCommand
        '
        Me.SqlUpdateCommand.CommandText = "dbo.MainContactUpdate"
        Me.SqlUpdateCommand.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlUpdateCommand.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, False, CType(0, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing), New System.Data.SqlClient.SqlParameter("@Prefix", System.Data.SqlDbType.VarChar, 20, "Prefix"), New System.Data.SqlClient.SqlParameter("@FirstName", System.Data.SqlDbType.VarChar, 30, "FirstName"), New System.Data.SqlClient.SqlParameter("@Lastname", System.Data.SqlDbType.VarChar, 30, "Lastname"), New System.Data.SqlClient.SqlParameter("@MI", System.Data.SqlDbType.VarChar, 10, "MI"), New System.Data.SqlClient.SqlParameter("@Suffix", System.Data.SqlDbType.VarChar, 10, "Suffix"), New System.Data.SqlClient.SqlParameter("@Goesby", System.Data.SqlDbType.VarChar, 20, "Goesby"), New System.Data.SqlClient.SqlParameter("@Staff", System.Data.SqlDbType.VarChar, 10, "Staff"), New System.Data.SqlClient.SqlParameter("@JobTitle", System.Data.SqlDbType.VarChar, 50, "JobTitle"), New System.Data.SqlClient.SqlParameter("@Street1", System.Data.SqlDbType.VarChar, 50, "Street1"), New System.Data.SqlClient.SqlParameter("@City", System.Data.SqlDbType.VarChar, 30, "City"), New System.Data.SqlClient.SqlParameter("@State", System.Data.SqlDbType.VarChar, 20, "State"), New System.Data.SqlClient.SqlParameter("@Zip", System.Data.SqlDbType.VarChar, 10, "Zip"), New System.Data.SqlClient.SqlParameter("@Phone", System.Data.SqlDbType.VarChar, 30, "Phone"), New System.Data.SqlClient.SqlParameter("@CellPhone", System.Data.SqlDbType.VarChar, 30, "CellPhone"), New System.Data.SqlClient.SqlParameter("@PagerNumber", System.Data.SqlDbType.VarChar, 30, "PagerNumber"), New System.Data.SqlClient.SqlParameter("@PrimaryPhone", System.Data.SqlDbType.VarChar, 30, "PrimaryPhone"), New System.Data.SqlClient.SqlParameter("@SecondaryPhone", System.Data.SqlDbType.VarChar, 30, "SecondaryPhone"), New System.Data.SqlClient.SqlParameter("@Fax", System.Data.SqlDbType.VarChar, 20, "Fax"), New System.Data.SqlClient.SqlParameter("@Email", System.Data.SqlDbType.VarChar, 50, "Email"), New System.Data.SqlClient.SqlParameter("@Notes", System.Data.SqlDbType.VarChar, 2000, "Notes"), New System.Data.SqlClient.SqlParameter("@UseHome", System.Data.SqlDbType.Bit, 1, "UseHome"), New System.Data.SqlClient.SqlParameter("@WarningFlag", System.Data.SqlDbType.Bit, 1, "WarningFlag"), New System.Data.SqlClient.SqlParameter("@PrintFlag", System.Data.SqlDbType.VarChar, 200, "PrintFlag"), New System.Data.SqlClient.SqlParameter("@Gender", System.Data.SqlDbType.VarChar, 10, "Gender"), New System.Data.SqlClient.SqlParameter("@Ethnicity", System.Data.SqlDbType.VarChar, 10, "Ethnicity"), New System.Data.SqlClient.SqlParameter("@OrgNum", System.Data.SqlDbType.Int, 4, "OrgNum"), New System.Data.SqlClient.SqlParameter("@PrimaryContact", System.Data.SqlDbType.Bit, 1, "PrimaryContact"), New System.Data.SqlClient.SqlParameter("@WorkPhone", System.Data.SqlDbType.VarChar, 30, "WorkPhone"), New System.Data.SqlClient.SqlParameter("@WorkExtension", System.Data.SqlDbType.VarChar, 20, "WorkExtension"), New System.Data.SqlClient.SqlParameter("@MailListEmail", System.Data.SqlDbType.Bit, 1, "MailListEmail"), New System.Data.SqlClient.SqlParameter("@MailListPostal", System.Data.SqlDbType.Bit, 1, "MailListPostal"), New System.Data.SqlClient.SqlParameter("@Active", System.Data.SqlDbType.Bit, 1, "Active"), New System.Data.SqlClient.SqlParameter("@Cntry", System.Data.SqlDbType.VarChar, 50, "Country"), New System.Data.SqlClient.SqlParameter("@UID", System.Data.SqlDbType.Int, 4, "OnlineUID"), New System.Data.SqlClient.SqlParameter("@Original_ContactID", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "ContactID", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@ID", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "ContactID", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@Original_Stamp", System.Data.SqlDbType.Timestamp, 8, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "Stamped", System.Data.DataRowVersion.Original, Nothing)})
        '
        'MainMenu1
        '
        Me.MainMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem1, Me.MenuItem2, Me.MenuItem3, Me.MenuItem5, Me.mmGoto, Me.miHelp})
        '
        'MenuItem1
        '
        Me.MenuItem1.Index = 0
        Me.MenuItem1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.miAdd, Me.miClose})
        Me.MenuItem1.Text = "File"
        '
        'miAdd
        '
        Me.miAdd.Index = 0
        Me.miAdd.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.miAddConversation, Me.miAddRegistration})
        Me.miAdd.Text = "Add"
        '
        'miAddConversation
        '
        Me.miAddConversation.Index = 0
        Me.miAddConversation.Text = "Conversation"
        '
        'miAddRegistration
        '
        Me.miAddRegistration.Index = 1
        Me.miAddRegistration.Text = "Registration"
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
        Me.miDelete.Text = "Delete Contact"
        '
        'MenuItem3
        '
        Me.MenuItem3.Index = 2
        Me.MenuItem3.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.miLetter, Me.miEmail, Me.miIntro})
        Me.MenuItem3.Text = "Send"
        '
        'miLetter
        '
        Me.miLetter.Index = 0
        Me.miLetter.Text = "Letter"
        '
        'miEmail
        '
        Me.miEmail.Index = 1
        Me.miEmail.Text = "Email"
        '
        'miIntro
        '
        Me.miIntro.Index = 2
        Me.miIntro.Text = "Intro Letter"
        '
        'MenuItem5
        '
        Me.MenuItem5.Index = 3
        Me.MenuItem5.Text = "Report"
        '
        'mmGoto
        '
        Me.mmGoto.Index = 4
        Me.mmGoto.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.miGoToConversation, Me.miGotoOrg, Me.miGotoRegistration, Me.miGoToRecommend})
        Me.mmGoto.Text = "Goto"
        '
        'miGoToConversation
        '
        Me.miGoToConversation.Index = 0
        Me.miGoToConversation.Tag = "CONVERSATION"
        Me.miGoToConversation.Text = "Conversation"
        '
        'miGotoOrg
        '
        Me.miGotoOrg.Index = 1
        Me.miGotoOrg.Tag = "ORGANIZATION"
        Me.miGotoOrg.Text = "Organization"
        '
        'miGotoRegistration
        '
        Me.miGotoRegistration.Index = 2
        Me.miGotoRegistration.Tag = "REGISTRATION"
        Me.miGotoRegistration.Text = "Registration"
        '
        'miGoToRecommend
        '
        Me.miGoToRecommend.Index = 3
        Me.miGoToRecommend.Tag = "RECOMMENDATION"
        Me.miGoToRecommend.Text = "Recommendation"
        '
        'miHelp
        '
        Me.miHelp.Index = 5
        Me.miHelp.Text = "Help"
        '
        'StatusBar1
        '
        Me.StatusBar1.Location = New System.Drawing.Point(0, 569)
        Me.StatusBar1.Name = "StatusBar1"
        Me.StatusBar1.Panels.AddRange(New System.Windows.Forms.StatusBarPanel() {Me.StatusBarPanel1, Me.StatusBarPanelID, Me.StatusBarPanel2})
        Me.StatusBar1.ShowPanels = True
        Me.StatusBar1.Size = New System.Drawing.Size(984, 22)
        Me.StatusBar1.TabIndex = 202
        Me.StatusBar1.Text = "StatusBar1"
        Me.ToolTip1.SetToolTip(Me.StatusBar1, "doubleclick to copy ID")
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
        'StatusBarPanelID
        '
        Me.StatusBarPanelID.Alignment = System.Windows.Forms.HorizontalAlignment.Center
        Me.StatusBarPanelID.BorderStyle = System.Windows.Forms.StatusBarPanelBorderStyle.Raised
        Me.StatusBarPanelID.Name = "StatusBarPanelID"
        Me.StatusBarPanelID.Text = "Contact ID"
        Me.StatusBarPanelID.Width = 200
        '
        'StatusBarPanel2
        '
        Me.StatusBarPanel2.Alignment = System.Windows.Forms.HorizontalAlignment.Right
        Me.StatusBarPanel2.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring
        Me.StatusBarPanel2.BorderStyle = System.Windows.Forms.StatusBarPanelBorderStyle.None
        Me.StatusBarPanel2.Name = "StatusBarPanel2"
        Me.StatusBarPanel2.Text = "Use this window to change Contact detail, and go to related Conversations or Regi" & _
    "strations."
        Me.StatusBarPanel2.Width = 567
        '
        'fldGoToOrg
        '
        Me.fldGoToOrg.BackColor = System.Drawing.SystemColors.Control
        Me.fldGoToOrg.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fldGoToOrg.ForeColor = System.Drawing.Color.ForestGreen
        Me.fldGoToOrg.Location = New System.Drawing.Point(275, 5)
        Me.fldGoToOrg.Multiline = True
        Me.fldGoToOrg.Name = "fldGoToOrg"
        Me.fldGoToOrg.ReadOnly = True
        Me.fldGoToOrg.Size = New System.Drawing.Size(370, 45)
        Me.fldGoToOrg.TabIndex = 421
        Me.fldGoToOrg.Text = "should be org name"
        Me.ToolTip1.SetToolTip(Me.fldGoToOrg, "Doubleclick to open Organization Detail window.")
        '
        'CheckBox5
        '
        Me.CheckBox5.DataBindings.Add(New System.Windows.Forms.Binding("Checked", Me.MainContactBindingSource, "PrimaryContact", True))
        Me.CheckBox5.Location = New System.Drawing.Point(82, 168)
        Me.CheckBox5.Name = "CheckBox5"
        Me.CheckBox5.Size = New System.Drawing.Size(131, 24)
        Me.CheckBox5.TabIndex = 6
        Me.CheckBox5.Text = "Primary Contact"
        Me.ToolTip1.SetToolTip(Me.CheckBox5, "only one Primary contact per organization")
        '
        'txtPrefix
        '
        Me.txtPrefix.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainContactBindingSource, "Prefix", True))
        Me.txtPrefix.Location = New System.Drawing.Point(80, 24)
        Me.txtPrefix.MaxLength = 20
        Me.txtPrefix.Name = "txtPrefix"
        Me.txtPrefix.Size = New System.Drawing.Size(160, 20)
        Me.txtPrefix.TabIndex = 0
        Me.ToolTip1.SetToolTip(Me.txtPrefix, "Prefix is used for mailing salutations")
        '
        'Label24
        '
        Me.Label24.Location = New System.Drawing.Point(34, 84)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(141, 23)
        Me.Label24.TabIndex = 3
        Me.Label24.Text = "Remove from Email List"
        Me.ToolTip1.SetToolTip(Me.Label24, "on our Mailing List via Email")
        '
        'Label23
        '
        Me.Label23.Location = New System.Drawing.Point(33, 58)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(139, 22)
        Me.Label23.TabIndex = 2
        Me.Label23.Text = "Remove from Postal Mail"
        Me.ToolTip1.SetToolTip(Me.Label23, "on our Mailing List via the Post Office")
        '
        'chkRemoveEmail
        '
        Me.chkRemoveEmail.Location = New System.Drawing.Point(16, 80)
        Me.chkRemoveEmail.Name = "chkRemoveEmail"
        Me.chkRemoveEmail.Size = New System.Drawing.Size(17, 21)
        Me.chkRemoveEmail.TabIndex = 14
        Me.chkRemoveEmail.Tag = "Email"
        Me.ToolTip1.SetToolTip(Me.chkRemoveEmail, "on our Mailing List via Email")
        '
        'chkRemovePostal
        '
        Me.chkRemovePostal.Location = New System.Drawing.Point(16, 54)
        Me.chkRemovePostal.Name = "chkRemovePostal"
        Me.chkRemovePostal.Size = New System.Drawing.Size(17, 21)
        Me.chkRemovePostal.TabIndex = 13
        Me.chkRemovePostal.Tag = "Postal"
        Me.ToolTip1.SetToolTip(Me.chkRemovePostal, "on our Mailing List via the Post Office")
        '
        'chkActive
        '
        Me.chkActive.DataBindings.Add(New System.Windows.Forms.Binding("Checked", Me.MainContactBindingSource, "Active", True))
        Me.chkActive.Location = New System.Drawing.Point(15, 3)
        Me.chkActive.Name = "chkActive"
        Me.chkActive.Size = New System.Drawing.Size(80, 21)
        Me.chkActive.TabIndex = 11
        Me.ToolTip1.SetToolTip(Me.chkActive, "Active at this Organization")
        '
        'btnSaveExit
        '
        Me.btnSaveExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSaveExit.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.btnSaveExit.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSaveExit.Image = Global.InfoCtr.My.Resources.Resources.btnSaveExit
        Me.btnSaveExit.ImageAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSaveExit.Location = New System.Drawing.Point(111, 1)
        Me.btnSaveExit.Name = "btnSaveExit"
        Me.btnSaveExit.Size = New System.Drawing.Size(40, 35)
        Me.btnSaveExit.TabIndex = 418
        Me.btnSaveExit.Text = "Close"
        Me.btnSaveExit.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.ToolTip1.SetToolTip(Me.btnSaveExit, "Saves any changes and Closes this window")
        Me.btnSaveExit.UseVisualStyleBackColor = False
        '
        'btnSend
        '
        Me.btnSend.BackColor = System.Drawing.SystemColors.Control
        Me.btnSend.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSend.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnSend.Image = CType(resources.GetObject("btnSend.Image"), System.Drawing.Image)
        Me.btnSend.ImageAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSend.Location = New System.Drawing.Point(65, 1)
        Me.btnSend.Name = "btnSend"
        Me.btnSend.Size = New System.Drawing.Size(40, 35)
        Me.btnSend.TabIndex = 417
        Me.btnSend.Text = "Send"
        Me.btnSend.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.ToolTip1.SetToolTip(Me.btnSend, "Start Email or Letter or send Introductory Letter merged into Word doc.")
        Me.btnSend.UseVisualStyleBackColor = False
        '
        'btnNew
        '
        Me.btnNew.BackColor = System.Drawing.Color.PaleGoldenrod
        Me.btnNew.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnNew.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnNew.Image = Global.InfoCtr.My.Resources.Resources.btnAddNew
        Me.btnNew.ImageAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnNew.Location = New System.Drawing.Point(3, 1)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(40, 35)
        Me.btnNew.TabIndex = 415
        Me.btnNew.Text = "New"
        Me.btnNew.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.ToolTip1.SetToolTip(Me.btnNew, "Add new Conversation or Registration")
        Me.btnNew.UseVisualStyleBackColor = False
        '
        'btnDelete
        '
        Me.btnDelete.BackColor = System.Drawing.SystemColors.Control
        Me.btnDelete.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Image = Global.InfoCtr.My.Resources.Resources.btnDelete
        Me.btnDelete.ImageAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnDelete.Location = New System.Drawing.Point(46, 1)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(14, 35)
        Me.btnDelete.TabIndex = 416
        Me.btnDelete.Text = "Delete"
        Me.btnDelete.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.ToolTip1.SetToolTip(Me.btnDelete, "Delete this Contact")
        Me.btnDelete.UseVisualStyleBackColor = False
        Me.btnDelete.Visible = False
        '
        'txtEmail
        '
        Me.txtEmail.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainContactBindingSource, "Email", True))
        Me.txtEmail.Location = New System.Drawing.Point(97, 142)
        Me.txtEmail.MaxLength = 50
        Me.txtEmail.Name = "txtEmail"
        Me.txtEmail.Size = New System.Drawing.Size(293, 20)
        Me.txtEmail.TabIndex = 26
        Me.ToolTip1.SetToolTip(Me.txtEmail, "only single legitimate email goes here. Use Notes for additional information.")
        '
        'btnMailListAdd
        '
        Me.btnMailListAdd.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnMailListAdd.Location = New System.Drawing.Point(225, 4)
        Me.btnMailListAdd.Name = "btnMailListAdd"
        Me.btnMailListAdd.Size = New System.Drawing.Size(146, 25)
        Me.btnMailListAdd.TabIndex = 173
        Me.btnMailListAdd.Text = "Click to Add or Remove"
        Me.btnMailListAdd.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.ToolTip1.SetToolTip(Me.btnMailListAdd, "Mailing List")
        Me.btnMailListAdd.UseVisualStyleBackColor = True
        '
        'cboClergy
        '
        Me.cboClergy.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboClergy.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboClergy.DataBindings.Add(New System.Windows.Forms.Binding("SelectedItem", Me.MainContactBindingSource, "Staff", True))
        Me.cboClergy.Items.AddRange(New Object() {"", "General", "Pastoral"})
        Me.cboClergy.Location = New System.Drawing.Point(80, 198)
        Me.cboClergy.Name = "cboClergy"
        Me.cboClergy.RestrictContentToListItems = True
        Me.cboClergy.Size = New System.Drawing.Size(160, 21)
        Me.cboClergy.TabIndex = 7
        Me.ToolTip1.SetToolTip(Me.cboClergy, "leave blank if not official staff")
        '
        'Label26
        '
        Me.Label26.BackColor = System.Drawing.SystemColors.Control
        Me.Label26.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label26.Location = New System.Drawing.Point(10, 10)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(156, 16)
        Me.Label26.TabIndex = 211
        Me.Label26.Text = "CONTACT DETAIL"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.ControlLight
        Me.Panel1.Controls.Add(Me.lblSelectedID)
        Me.Panel1.Controls.Add(Me.Label30)
        Me.Panel1.Controls.Add(Me.lblSelectedWhat)
        Me.Panel1.Controls.Add(Me.cboGender)
        Me.Panel1.Controls.Add(Me.Label28)
        Me.Panel1.Controls.Add(Me.fldOrgNum)
        Me.Panel1.Controls.Add(Me.txtGoesBy)
        Me.Panel1.Controls.Add(Me.Label17)
        Me.Panel1.Controls.Add(Me.Label21)
        Me.Panel1.Controls.Add(Me.txtSuffix)
        Me.Panel1.Controls.Add(Me.Label14)
        Me.Panel1.Controls.Add(Me.txtMI)
        Me.Panel1.Controls.Add(Me.Label13)
        Me.Panel1.Controls.Add(Me.Label8)
        Me.Panel1.Controls.Add(Me.Label7)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.CheckBox5)
        Me.Panel1.Controls.Add(Me.txtLastName)
        Me.Panel1.Controls.Add(Me.txtFirstName)
        Me.Panel1.Controls.Add(Me.RichTextBox2)
        Me.Panel1.Controls.Add(Me.txtJobTitle)
        Me.Panel1.Controls.Add(Me.cboClergy)
        Me.Panel1.Controls.Add(Me.Label20)
        Me.Panel1.Controls.Add(Me.txtPrefix)
        Me.Panel1.Controls.Add(Me.Label10)
        Me.Panel1.Controls.Add(Me.pnlTabs)
        Me.Panel1.Location = New System.Drawing.Point(15, 62)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(906, 478)
        Me.Panel1.TabIndex = 210
        '
        'lblSelectedID
        '
        Me.lblSelectedID.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblSelectedID.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSelectedID.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.lblSelectedID.Location = New System.Drawing.Point(837, 450)
        Me.lblSelectedID.Name = "lblSelectedID"
        Me.lblSelectedID.Size = New System.Drawing.Size(50, 16)
        Me.lblSelectedID.TabIndex = 427
        Me.lblSelectedID.Text = "ID"
        '
        'Label30
        '
        Me.Label30.Location = New System.Drawing.Point(26, 273)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(48, 16)
        Me.Label30.TabIndex = 129
        Me.Label30.Text = "Gender"
        Me.Label30.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblSelectedWhat
        '
        Me.lblSelectedWhat.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblSelectedWhat.AutoSize = True
        Me.lblSelectedWhat.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSelectedWhat.ForeColor = System.Drawing.SystemColors.ControlDark
        Me.lblSelectedWhat.Location = New System.Drawing.Point(731, 453)
        Me.lblSelectedWhat.MinimumSize = New System.Drawing.Size(100, 0)
        Me.lblSelectedWhat.Name = "lblSelectedWhat"
        Me.lblSelectedWhat.Size = New System.Drawing.Size(100, 13)
        Me.lblSelectedWhat.TabIndex = 426
        Me.lblSelectedWhat.Text = "Selection #"
        Me.lblSelectedWhat.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'cboGender
        '
        Me.cboGender.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboGender.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboGender.DataBindings.Add(New System.Windows.Forms.Binding("SelectedItem", Me.MainContactBindingSource, "Gender", True))
        Me.cboGender.Items.AddRange(New Object() {"", "Female", "Male"})
        Me.cboGender.Location = New System.Drawing.Point(82, 271)
        Me.cboGender.Name = "cboGender"
        Me.cboGender.RestrictContentToListItems = True
        Me.cboGender.Size = New System.Drawing.Size(160, 21)
        Me.cboGender.TabIndex = 9
        '
        'Label28
        '
        Me.Label28.Location = New System.Drawing.Point(18, 142)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(56, 24)
        Me.Label28.TabIndex = 124
        Me.Label28.Text = "Goes by"
        Me.Label28.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'fldOrgNum
        '
        Me.fldOrgNum.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.fldOrgNum.BackColor = System.Drawing.SystemColors.Control
        Me.fldOrgNum.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainContactBindingSource, "OrgNum", True))
        Me.fldOrgNum.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fldOrgNum.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.fldOrgNum.Location = New System.Drawing.Point(651, 449)
        Me.fldOrgNum.Name = "fldOrgNum"
        Me.fldOrgNum.Size = New System.Drawing.Size(50, 14)
        Me.fldOrgNum.TabIndex = 419
        '
        'txtGoesBy
        '
        Me.txtGoesBy.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainContactBindingSource, "Goesby", True))
        Me.txtGoesBy.Location = New System.Drawing.Point(80, 144)
        Me.txtGoesBy.MaxLength = 20
        Me.txtGoesBy.Name = "txtGoesBy"
        Me.txtGoesBy.Size = New System.Drawing.Size(160, 20)
        Me.txtGoesBy.TabIndex = 5
        '
        'Label17
        '
        Me.Label17.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label17.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.ForeColor = System.Drawing.SystemColors.ControlDark
        Me.Label17.Location = New System.Drawing.Point(595, 452)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(50, 14)
        Me.Label17.TabIndex = 416
        Me.Label17.Text = "Org #"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label21
        '
        Me.Label21.Location = New System.Drawing.Point(136, 111)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(40, 24)
        Me.Label21.TabIndex = 122
        Me.Label21.Text = "Suffix"
        Me.Label21.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtSuffix
        '
        Me.txtSuffix.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainContactBindingSource, "Suffix", True))
        Me.txtSuffix.Location = New System.Drawing.Point(184, 114)
        Me.txtSuffix.MaxLength = 20
        Me.txtSuffix.Name = "txtSuffix"
        Me.txtSuffix.Size = New System.Drawing.Size(48, 20)
        Me.txtSuffix.TabIndex = 4
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.Transparent
        Me.Label14.Location = New System.Drawing.Point(25, 113)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(49, 24)
        Me.Label14.TabIndex = 120
        Me.Label14.Text = "Middle"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtMI
        '
        Me.txtMI.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainContactBindingSource, "MI", True))
        Me.txtMI.Location = New System.Drawing.Point(80, 114)
        Me.txtMI.MaxLength = 20
        Me.txtMI.Name = "txtMI"
        Me.txtMI.Size = New System.Drawing.Size(48, 20)
        Me.txtMI.TabIndex = 3
        '
        'Label13
        '
        Me.Label13.Location = New System.Drawing.Point(34, 200)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(40, 16)
        Me.Label13.TabIndex = 117
        Me.Label13.Text = "If Staff"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label8
        '
        Me.Label8.Location = New System.Drawing.Point(26, 227)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(48, 24)
        Me.Label8.TabIndex = 116
        Me.Label8.Text = "Job Title"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Location = New System.Drawing.Point(10, 56)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(64, 16)
        Me.Label7.TabIndex = 115
        Me.Label7.Text = "First Name"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Location = New System.Drawing.Point(14, 88)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(60, 16)
        Me.Label3.TabIndex = 114
        Me.Label3.Text = "Last Name"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtLastName
        '
        Me.txtLastName.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainContactBindingSource, "Lastname", True))
        Me.txtLastName.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLastName.ForeColor = System.Drawing.SystemColors.ControlText
        Me.txtLastName.Location = New System.Drawing.Point(80, 83)
        Me.txtLastName.MaxLength = 30
        Me.txtLastName.Name = "txtLastName"
        Me.txtLastName.Size = New System.Drawing.Size(160, 23)
        Me.txtLastName.TabIndex = 2
        Me.txtLastName.Tag = "LAST NAME"
        Me.txtLastName.Text = "LastName"
        '
        'txtFirstName
        '
        Me.txtFirstName.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainContactBindingSource, "FirstName", True))
        Me.txtFirstName.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFirstName.ForeColor = System.Drawing.SystemColors.ControlText
        Me.txtFirstName.Location = New System.Drawing.Point(80, 52)
        Me.txtFirstName.MaxLength = 30
        Me.txtFirstName.Name = "txtFirstName"
        Me.txtFirstName.Size = New System.Drawing.Size(160, 23)
        Me.txtFirstName.TabIndex = 1
        Me.txtFirstName.Tag = "FIRST NAME"
        Me.txtFirstName.Text = "FirstName"
        '
        'RichTextBox2
        '
        Me.RichTextBox2.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainContactBindingSource, "Notes", True))
        Me.RichTextBox2.Location = New System.Drawing.Point(48, 304)
        Me.RichTextBox2.Name = "RichTextBox2"
        Me.RichTextBox2.Size = New System.Drawing.Size(200, 136)
        Me.RichTextBox2.TabIndex = 10
        Me.RichTextBox2.Text = ""
        '
        'txtJobTitle
        '
        Me.txtJobTitle.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainContactBindingSource, "JobTitle", True))
        Me.txtJobTitle.Location = New System.Drawing.Point(80, 227)
        Me.txtJobTitle.MaxLength = 50
        Me.txtJobTitle.Multiline = True
        Me.txtJobTitle.Name = "txtJobTitle"
        Me.txtJobTitle.Size = New System.Drawing.Size(160, 37)
        Me.txtJobTitle.TabIndex = 8
        '
        'Label20
        '
        Me.Label20.Location = New System.Drawing.Point(16, 304)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(32, 23)
        Me.Label20.TabIndex = 110
        Me.Label20.Text = "Note"
        '
        'Label10
        '
        Me.Label10.Location = New System.Drawing.Point(25, 24)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(49, 16)
        Me.Label10.TabIndex = 106
        Me.Label10.Text = "Prefix"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'pnlTabs
        '
        Me.pnlTabs.BackColor = System.Drawing.SystemColors.Control
        Me.pnlTabs.Controls.Add(Me.Panel4)
        Me.pnlTabs.Controls.Add(Me.pnlMailingList)
        Me.pnlTabs.Controls.Add(Me.Label12)
        Me.pnlTabs.Controls.Add(Me.pnlExtras)
        Me.pnlTabs.Controls.Add(Me.Label29)
        Me.pnlTabs.Controls.Add(Me.lblExtra)
        Me.pnlTabs.Location = New System.Drawing.Point(278, 29)
        Me.pnlTabs.Name = "pnlTabs"
        Me.pnlTabs.Size = New System.Drawing.Size(613, 417)
        Me.pnlTabs.TabIndex = 20
        '
        'Panel4
        '
        Me.Panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel4.Controls.Add(Me.Label27)
        Me.Panel4.Controls.Add(Me.btnMailListAdd)
        Me.Panel4.Controls.Add(Me.fldMailListCSV)
        Me.Panel4.Location = New System.Drawing.Point(208, 26)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(386, 112)
        Me.Panel4.TabIndex = 230
        '
        'Label27
        '
        Me.Label27.Location = New System.Drawing.Point(3, 4)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(110, 14)
        Me.Label27.TabIndex = 174
        Me.Label27.Text = "Named Mailing Lists:"
        Me.Label27.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'fldMailListCSV
        '
        Me.fldMailListCSV.Location = New System.Drawing.Point(10, 35)
        Me.fldMailListCSV.Multiline = True
        Me.fldMailListCSV.Name = "fldMailListCSV"
        Me.fldMailListCSV.ReadOnly = True
        Me.fldMailListCSV.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.fldMailListCSV.Size = New System.Drawing.Size(356, 64)
        Me.fldMailListCSV.TabIndex = 171
        '
        'pnlMailingList
        '
        Me.pnlMailingList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pnlMailingList.Controls.Add(Me.Label24)
        Me.pnlMailingList.Controls.Add(Me.Label23)
        Me.pnlMailingList.Controls.Add(Me.Label25)
        Me.pnlMailingList.Controls.Add(Me.chkEmail)
        Me.pnlMailingList.Controls.Add(Me.chkRemoveEmail)
        Me.pnlMailingList.Controls.Add(Me.Label22)
        Me.pnlMailingList.Controls.Add(Me.chkUseHome)
        Me.pnlMailingList.Controls.Add(Me.chkActive)
        Me.pnlMailingList.Controls.Add(Me.chkRemovePostal)
        Me.pnlMailingList.Controls.Add(Me.chkPostal)
        Me.pnlMailingList.Location = New System.Drawing.Point(22, 23)
        Me.pnlMailingList.Name = "pnlMailingList"
        Me.pnlMailingList.Size = New System.Drawing.Size(192, 127)
        Me.pnlMailingList.TabIndex = 1
        '
        'Label25
        '
        Me.Label25.Location = New System.Drawing.Point(31, 32)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(104, 15)
        Me.Label25.TabIndex = 1
        Me.Label25.Text = "Use Home Address"
        '
        'chkEmail
        '
        Me.chkEmail.AutoSize = True
        Me.chkEmail.DataBindings.Add(New System.Windows.Forms.Binding("Checked", Me.MainContactBindingSource, "MailListEmail", True))
        Me.chkEmail.Location = New System.Drawing.Point(106, 85)
        Me.chkEmail.Name = "chkEmail"
        Me.chkEmail.Size = New System.Drawing.Size(51, 17)
        Me.chkEmail.TabIndex = 230
        Me.chkEmail.TabStop = False
        Me.chkEmail.Text = "Email"
        Me.chkEmail.UseVisualStyleBackColor = True
        '
        'Label22
        '
        Me.Label22.Location = New System.Drawing.Point(31, 6)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(64, 15)
        Me.Label22.TabIndex = 0
        Me.Label22.Text = "Active"
        '
        'chkUseHome
        '
        Me.chkUseHome.DataBindings.Add(New System.Windows.Forms.Binding("Checked", Me.MainContactBindingSource, "UseHome", True))
        Me.chkUseHome.Location = New System.Drawing.Point(15, 29)
        Me.chkUseHome.Name = "chkUseHome"
        Me.chkUseHome.Size = New System.Drawing.Size(17, 21)
        Me.chkUseHome.TabIndex = 12
        '
        'chkPostal
        '
        Me.chkPostal.AutoSize = True
        Me.chkPostal.DataBindings.Add(New System.Windows.Forms.Binding("Checked", Me.MainContactBindingSource, "MailListPostal", True))
        Me.chkPostal.Location = New System.Drawing.Point(94, 57)
        Me.chkPostal.Name = "chkPostal"
        Me.chkPostal.Size = New System.Drawing.Size(55, 17)
        Me.chkPostal.TabIndex = 229
        Me.chkPostal.TabStop = False
        Me.chkPostal.Text = "Postal"
        Me.chkPostal.UseVisualStyleBackColor = True
        '
        'Label12
        '
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.SystemColors.Highlight
        Me.Label12.Location = New System.Drawing.Point(234, 149)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(107, 13)
        Me.Label12.TabIndex = 226
        Me.Label12.Text = "Home Address"
        '
        'pnlExtras
        '
        Me.pnlExtras.BackColor = System.Drawing.SystemColors.Control
        Me.pnlExtras.Controls.Add(Me.fldCreateStaff)
        Me.pnlExtras.Controls.Add(Me.Label19)
        Me.pnlExtras.Controls.Add(Me.Label18)
        Me.pnlExtras.Controls.Add(Me.fldLastChangeDate)
        Me.pnlExtras.Controls.Add(Me.fldCreateDate)
        Me.pnlExtras.Controls.Add(Me.fldLastChangeStaff)
        Me.pnlExtras.Location = New System.Drawing.Point(54, 350)
        Me.pnlExtras.Name = "pnlExtras"
        Me.pnlExtras.Size = New System.Drawing.Size(388, 57)
        Me.pnlExtras.TabIndex = 228
        Me.pnlExtras.Visible = False
        '
        'Label29
        '
        Me.Label29.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label29.ForeColor = System.Drawing.SystemColors.Highlight
        Me.Label29.Location = New System.Drawing.Point(51, 9)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(102, 14)
        Me.Label29.TabIndex = 227
        Me.Label29.Text = "Mailing List"
        '
        'lblExtra
        '
        Me.lblExtra.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblExtra.ForeColor = System.Drawing.SystemColors.Highlight
        Me.lblExtra.Location = New System.Drawing.Point(53, 350)
        Me.lblExtra.Name = "lblExtra"
        Me.lblExtra.Size = New System.Drawing.Size(93, 13)
        Me.lblExtra.TabIndex = 229
        Me.lblExtra.Text = "Extra Information"
        Me.lblExtra.Visible = False
        '
        'TextBox3
        '
        Me.TextBox3.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainContactBindingSource, "OnlineUID", True, DataSourceUpdateMode.OnValidation, ""))
        Me.TextBox3.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox3.Location = New System.Drawing.Point(527, 144)
        Me.TextBox3.MaxLength = 10
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.Size = New System.Drawing.Size(60, 18)
        Me.TextBox3.TabIndex = 159
        Me.TextBox3.TabStop = False
        '
        'Label36
        '
        Me.Label36.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label36.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label36.Location = New System.Drawing.Point(407, 144)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(119, 13)
        Me.Label36.TabIndex = 160
        Me.Label36.Text = "Online Registration UID"
        '
        'pnlGrid
        '
        Me.pnlGrid.BackColor = System.Drawing.SystemColors.Control
        Me.pnlGrid.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pnlGrid.Controls.Add(Me.grdMain)
        Me.pnlGrid.Location = New System.Drawing.Point(293, 93)
        Me.pnlGrid.Name = "pnlGrid"
        Me.pnlGrid.Size = New System.Drawing.Size(624, 412)
        Me.pnlGrid.TabIndex = 222
        Me.pnlGrid.Visible = False
        '
        'grdMain
        '
        Me.grdMain.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdMain.DataMember = ""
        Me.grdMain.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.grdMain.Location = New System.Drawing.Point(1, -5)
        Me.grdMain.Name = "grdMain"
        Me.grdMain.ReadOnly = True
        Me.grdMain.RowHeaderWidth = 20
        Me.grdMain.Size = New System.Drawing.Size(621, 411)
        Me.grdMain.TabIndex = 170
        Me.grdMain.TableStyles.AddRange(New System.Windows.Forms.DataGridTableStyle() {Me.DataGridTableStyle3, Me.DataGridTableStyle2, Me.DataGridTableStyle1})
        '
        'DataGridTableStyle3
        '
        Me.DataGridTableStyle3.DataGrid = Me.grdMain
        Me.DataGridTableStyle3.ForeColor = System.Drawing.Color.DarkGreen
        Me.DataGridTableStyle3.GridColumnStyles.AddRange(New System.Windows.Forms.DataGridColumnStyle() {Me.DataGridTextBoxColumn18, Me.DataGridTextBoxColumn19, Me.DataGridTextBoxColumn20, Me.DataGridTextBoxColumn21})
        Me.DataGridTableStyle3.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.DataGridTableStyle3.MappingName = "GetRecommendations"
        Me.DataGridTableStyle3.RowHeaderWidth = 15
        '
        'DataGridTextBoxColumn18
        '
        Me.DataGridTextBoxColumn18.Format = ""
        Me.DataGridTextBoxColumn18.FormatInfo = Nothing
        Me.DataGridTextBoxColumn18.HeaderText = "RecommendID"
        Me.DataGridTextBoxColumn18.MappingName = "RecommendID"
        Me.DataGridTextBoxColumn18.Width = 0
        '
        'DataGridTextBoxColumn19
        '
        Me.DataGridTextBoxColumn19.Format = ""
        Me.DataGridTextBoxColumn19.FormatInfo = Nothing
        Me.DataGridTextBoxColumn19.HeaderText = "Resource"
        Me.DataGridTextBoxColumn19.MappingName = "ResourceName"
        Me.DataGridTextBoxColumn19.Width = 250
        '
        'DataGridTextBoxColumn20
        '
        Me.DataGridTextBoxColumn20.Format = "d"
        Me.DataGridTextBoxColumn20.FormatInfo = Nothing
        Me.DataGridTextBoxColumn20.HeaderText = "Date"
        Me.DataGridTextBoxColumn20.MappingName = "RecommendDate"
        Me.DataGridTextBoxColumn20.Width = 75
        '
        'DataGridTextBoxColumn21
        '
        Me.DataGridTextBoxColumn21.Format = ""
        Me.DataGridTextBoxColumn21.FormatInfo = Nothing
        Me.DataGridTextBoxColumn21.HeaderText = "Case"
        Me.DataGridTextBoxColumn21.MappingName = "CaseName"
        Me.DataGridTextBoxColumn21.Width = 230
        '
        'DataGridTableStyle2
        '
        Me.DataGridTableStyle2.DataGrid = Me.grdMain
        Me.DataGridTableStyle2.ForeColor = System.Drawing.Color.DarkGreen
        Me.DataGridTableStyle2.GridColumnStyles.AddRange(New System.Windows.Forms.DataGridColumnStyle() {Me.DataGridTextBoxColumn13, Me.DataGridTextBoxColumn15, Me.DataGridTextBoxColumn14, Me.DataGridTextBoxColumn16, Me.DataGridTextBoxColumn17})
        Me.DataGridTableStyle2.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.DataGridTableStyle2.MappingName = "GetConversations"
        Me.DataGridTableStyle2.RowHeaderWidth = 15
        '
        'DataGridTextBoxColumn13
        '
        Me.DataGridTextBoxColumn13.Format = ""
        Me.DataGridTextBoxColumn13.FormatInfo = Nothing
        Me.DataGridTextBoxColumn13.HeaderText = "ConversID"
        Me.DataGridTextBoxColumn13.MappingName = "ConversID"
        Me.DataGridTextBoxColumn13.Width = 0
        '
        'DataGridTextBoxColumn15
        '
        Me.DataGridTextBoxColumn15.Format = "d"
        Me.DataGridTextBoxColumn15.FormatInfo = Nothing
        Me.DataGridTextBoxColumn15.HeaderText = "Date"
        Me.DataGridTextBoxColumn15.MappingName = "ConversDate"
        Me.DataGridTextBoxColumn15.Width = 75
        '
        'DataGridTextBoxColumn14
        '
        Me.DataGridTextBoxColumn14.Format = ""
        Me.DataGridTextBoxColumn14.FormatInfo = Nothing
        Me.DataGridTextBoxColumn14.HeaderText = "Case"
        Me.DataGridTextBoxColumn14.MappingName = "CaseName"
        Me.DataGridTextBoxColumn14.Width = 150
        '
        'DataGridTextBoxColumn16
        '
        Me.DataGridTextBoxColumn16.Format = ""
        Me.DataGridTextBoxColumn16.FormatInfo = Nothing
        Me.DataGridTextBoxColumn16.HeaderText = "Staff"
        Me.DataGridTextBoxColumn16.MappingName = "StaffName"
        Me.DataGridTextBoxColumn16.Width = 75
        '
        'DataGridTextBoxColumn17
        '
        Me.DataGridTextBoxColumn17.Format = ""
        Me.DataGridTextBoxColumn17.FormatInfo = Nothing
        Me.DataGridTextBoxColumn17.HeaderText = "Brief Summary"
        Me.DataGridTextBoxColumn17.MappingName = "BriefSummary"
        Me.DataGridTextBoxColumn17.Width = 200
        '
        'DataGridTableStyle1
        '
        Me.DataGridTableStyle1.DataGrid = Me.grdMain
        Me.DataGridTableStyle1.ForeColor = System.Drawing.Color.DarkGreen
        Me.DataGridTableStyle1.GridColumnStyles.AddRange(New System.Windows.Forms.DataGridColumnStyle() {Me.DataGridTextBoxColumn12, Me.DataGridTextBoxColumn1, Me.DataGridTextBoxColumn2, Me.DataGridTextBoxColumn3, Me.DataGridTextBoxColumn4, Me.DataGridTextBoxColumn5, Me.DataGridTextBoxColumn6})
        Me.DataGridTableStyle1.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.DataGridTableStyle1.MappingName = "GetWRegistrations"
        Me.DataGridTableStyle1.RowHeaderWidth = 15
        '
        'DataGridTextBoxColumn12
        '
        Me.DataGridTextBoxColumn12.Format = ""
        Me.DataGridTextBoxColumn12.FormatInfo = Nothing
        Me.DataGridTextBoxColumn12.HeaderText = "RegistrationID"
        Me.DataGridTextBoxColumn12.MappingName = "RegistrationID"
        Me.DataGridTextBoxColumn12.Width = 0
        '
        'DataGridTextBoxColumn1
        '
        Me.DataGridTextBoxColumn1.Format = ""
        Me.DataGridTextBoxColumn1.FormatInfo = Nothing
        Me.DataGridTextBoxColumn1.HeaderText = "ContactNum"
        Me.DataGridTextBoxColumn1.MappingName = "ContactNum"
        Me.DataGridTextBoxColumn1.Width = 0
        '
        'DataGridTextBoxColumn2
        '
        Me.DataGridTextBoxColumn2.Format = "d"
        Me.DataGridTextBoxColumn2.FormatInfo = Nothing
        Me.DataGridTextBoxColumn2.HeaderText = "Event Date"
        Me.DataGridTextBoxColumn2.MappingName = "EventDate"
        Me.DataGridTextBoxColumn2.Width = 75
        '
        'DataGridTextBoxColumn3
        '
        Me.DataGridTextBoxColumn3.Format = ""
        Me.DataGridTextBoxColumn3.FormatInfo = Nothing
        Me.DataGridTextBoxColumn3.HeaderText = "EventName"
        Me.DataGridTextBoxColumn3.MappingName = "EventName"
        Me.DataGridTextBoxColumn3.Width = 250
        '
        'DataGridTextBoxColumn4
        '
        Me.DataGridTextBoxColumn4.Format = ""
        Me.DataGridTextBoxColumn4.FormatInfo = Nothing
        Me.DataGridTextBoxColumn4.HeaderText = "Event Region"
        Me.DataGridTextBoxColumn4.MappingName = "SatelliteRegion"
        Me.DataGridTextBoxColumn4.Width = 75
        '
        'DataGridTextBoxColumn5
        '
        Me.DataGridTextBoxColumn5.Format = ""
        Me.DataGridTextBoxColumn5.FormatInfo = Nothing
        Me.DataGridTextBoxColumn5.HeaderText = "Attended"
        Me.DataGridTextBoxColumn5.MappingName = "Attended"
        Me.DataGridTextBoxColumn5.Width = 50
        '
        'DataGridTextBoxColumn6
        '
        Me.DataGridTextBoxColumn6.Format = ""
        Me.DataGridTextBoxColumn6.FormatInfo = Nothing
        Me.DataGridTextBoxColumn6.HeaderText = "OrderNum"
        Me.DataGridTextBoxColumn6.MappingName = "OrderNum"
        Me.DataGridTextBoxColumn6.Width = 75
        '
        'Panel2
        '
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel2.Controls.Add(Me.TextBox3)
        Me.Panel2.Controls.Add(Me.Label36)
        Me.Panel2.Controls.Add(Me.txtSecondPhone)
        Me.Panel2.Controls.Add(Me.txtCountry)
        Me.Panel2.Controls.Add(Me.Label16)
        Me.Panel2.Controls.Add(Me.Label33)
        Me.Panel2.Controls.Add(Me.TextBox9)
        Me.Panel2.Controls.Add(Me.Label32)
        Me.Panel2.Controls.Add(Me.txtPrimPhone)
        Me.Panel2.Controls.Add(Me.Label31)
        Me.Panel2.Controls.Add(Me.txtWorkPhone)
        Me.Panel2.Controls.Add(Me.Label15)
        Me.Panel2.Controls.Add(Me.txtCellPhone)
        Me.Panel2.Controls.Add(Me.Label11)
        Me.Panel2.Controls.Add(Me.txtHomePhone)
        Me.Panel2.Controls.Add(Me.Label9)
        Me.Panel2.Controls.Add(Me.Label1)
        Me.Panel2.Controls.Add(Me.txtEmail)
        Me.Panel2.Controls.Add(Me.txtHomeFax)
        Me.Panel2.Controls.Add(Me.txtZip)
        Me.Panel2.Controls.Add(Me.txtState)
        Me.Panel2.Controls.Add(Me.txtCity)
        Me.Panel2.Controls.Add(Me.txtStreet)
        Me.Panel2.Controls.Add(Me.Label6)
        Me.Panel2.Controls.Add(Me.Label5)
        Me.Panel2.Controls.Add(Me.Label4)
        Me.Panel2.Controls.Add(Me.Label2)
        Me.Panel2.Controls.Add(Me.Label34)
        Me.Panel2.Location = New System.Drawing.Point(302, 255)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(595, 174)
        Me.Panel2.TabIndex = 2
        '
        'txtSecondPhone
        '
        Me.txtSecondPhone.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainContactBindingSource, "SecondaryPHone", True))
        Me.txtSecondPhone.Location = New System.Drawing.Point(295, 62)
        Me.txtSecondPhone.MaxLength = 30
        Me.txtSecondPhone.Name = "txtSecondPhone"
        Me.txtSecondPhone.Size = New System.Drawing.Size(94, 20)
        Me.txtSecondPhone.TabIndex = 20
        '
        'txtCountry
        '
        Me.txtCountry.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainContactBindingSource, "Country", True))
        Me.txtCountry.Location = New System.Drawing.Point(445, 33)
        Me.txtCountry.MaxLength = 10
        Me.txtCountry.Name = "txtCountry"
        Me.txtCountry.Size = New System.Drawing.Size(60, 20)
        Me.txtCountry.TabIndex = 153
        '
        'Label16
        '
        Me.Label16.Location = New System.Drawing.Point(402, 33)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(44, 16)
        Me.Label16.TabIndex = 154
        Me.Label16.Text = "Country"
        '
        'Label33
        '
        Me.Label33.Location = New System.Drawing.Point(193, 116)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(26, 20)
        Me.Label33.TabIndex = 152
        Me.Label33.Text = "ext."
        Me.Label33.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'TextBox9
        '
        Me.TextBox9.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainContactBindingSource, "WorkExtension", True))
        Me.TextBox9.Location = New System.Drawing.Point(217, 117)
        Me.TextBox9.MaxLength = 20
        Me.TextBox9.Name = "TextBox9"
        Me.TextBox9.Size = New System.Drawing.Size(79, 20)
        Me.TextBox9.TabIndex = 25
        '
        'Label32
        '
        Me.Label32.Location = New System.Drawing.Point(3, 58)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(83, 20)
        Me.Label32.TabIndex = 150
        Me.Label32.Text = "Primary Phone"
        Me.Label32.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtPrimPhone
        '
        Me.txtPrimPhone.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainContactBindingSource, "PrimaryPhone", True))
        Me.txtPrimPhone.Location = New System.Drawing.Point(94, 60)
        Me.txtPrimPhone.MaxLength = 30
        Me.txtPrimPhone.Name = "txtPrimPhone"
        Me.txtPrimPhone.Size = New System.Drawing.Size(96, 20)
        Me.txtPrimPhone.TabIndex = 19
        '
        'Label31
        '
        Me.Label31.Location = New System.Drawing.Point(15, 117)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(74, 20)
        Me.Label31.TabIndex = 148
        Me.Label31.Text = "Work Phone"
        Me.Label31.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtWorkPhone
        '
        Me.txtWorkPhone.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainContactBindingSource, "WorkPhone", True))
        Me.txtWorkPhone.Location = New System.Drawing.Point(97, 115)
        Me.txtWorkPhone.MaxLength = 30
        Me.txtWorkPhone.Name = "txtWorkPhone"
        Me.txtWorkPhone.Size = New System.Drawing.Size(94, 20)
        Me.txtWorkPhone.TabIndex = 24
        '
        'Label15
        '
        Me.Label15.Location = New System.Drawing.Point(224, 91)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(64, 20)
        Me.Label15.TabIndex = 146
        Me.Label15.Text = "Cell Phone"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtCellPhone
        '
        Me.txtCellPhone.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainContactBindingSource, "CellPhone", True))
        Me.txtCellPhone.Location = New System.Drawing.Point(296, 93)
        Me.txtCellPhone.MaxLength = 30
        Me.txtCellPhone.Name = "txtCellPhone"
        Me.txtCellPhone.Size = New System.Drawing.Size(94, 20)
        Me.txtCellPhone.TabIndex = 22
        '
        'Label11
        '
        Me.Label11.Location = New System.Drawing.Point(16, 90)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(72, 20)
        Me.Label11.TabIndex = 144
        Me.Label11.Text = "Home Phone"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtHomePhone
        '
        Me.txtHomePhone.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainContactBindingSource, "Phone", True))
        Me.txtHomePhone.Location = New System.Drawing.Point(96, 87)
        Me.txtHomePhone.MaxLength = 20
        Me.txtHomePhone.Name = "txtHomePhone"
        Me.txtHomePhone.Size = New System.Drawing.Size(94, 20)
        Me.txtHomePhone.TabIndex = 21
        '
        'Label9
        '
        Me.Label9.Location = New System.Drawing.Point(45, 142)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(47, 24)
        Me.Label9.TabIndex = 133
        Me.Label9.Text = "Email"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(403, 93)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(64, 20)
        Me.Label1.TabIndex = 132
        Me.Label1.Text = "Home Fax"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtHomeFax
        '
        Me.txtHomeFax.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainContactBindingSource, "Fax", True))
        Me.txtHomeFax.Location = New System.Drawing.Point(475, 91)
        Me.txtHomeFax.MaxLength = 25
        Me.txtHomeFax.Name = "txtHomeFax"
        Me.txtHomeFax.Size = New System.Drawing.Size(94, 20)
        Me.txtHomeFax.TabIndex = 23
        '
        'txtZip
        '
        Me.txtZip.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainContactBindingSource, "Zip", True))
        Me.txtZip.Location = New System.Drawing.Point(309, 33)
        Me.txtZip.MaxLength = 10
        Me.txtZip.Name = "txtZip"
        Me.txtZip.Size = New System.Drawing.Size(80, 20)
        Me.txtZip.TabIndex = 18
        '
        'txtState
        '
        Me.txtState.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainContactBindingSource, "State", True))
        Me.txtState.Location = New System.Drawing.Point(230, 33)
        Me.txtState.MaxLength = 20
        Me.txtState.Name = "txtState"
        Me.txtState.Size = New System.Drawing.Size(42, 20)
        Me.txtState.TabIndex = 17
        '
        'txtCity
        '
        Me.txtCity.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainContactBindingSource, "City", True))
        Me.txtCity.Location = New System.Drawing.Point(94, 33)
        Me.txtCity.MaxLength = 25
        Me.txtCity.Name = "txtCity"
        Me.txtCity.Size = New System.Drawing.Size(96, 20)
        Me.txtCity.TabIndex = 16
        '
        'txtStreet
        '
        Me.txtStreet.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainContactBindingSource, "Street1", True))
        Me.txtStreet.Location = New System.Drawing.Point(94, 6)
        Me.txtStreet.MaxLength = 50
        Me.txtStreet.Name = "txtStreet"
        Me.txtStreet.Size = New System.Drawing.Size(296, 20)
        Me.txtStreet.TabIndex = 15
        '
        'Label6
        '
        Me.Label6.Location = New System.Drawing.Point(286, 33)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(24, 16)
        Me.Label6.TabIndex = 130
        Me.Label6.Text = "Zip"
        '
        'Label5
        '
        Me.Label5.Location = New System.Drawing.Point(190, 33)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(32, 16)
        Me.Label5.TabIndex = 129
        Me.Label5.Text = "State"
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(62, 33)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(24, 16)
        Me.Label4.TabIndex = 128
        Me.Label4.Text = "City"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(46, 6)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(40, 16)
        Me.Label2.TabIndex = 127
        Me.Label2.Text = "Street"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label34
        '
        Me.Label34.Location = New System.Drawing.Point(204, 60)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(98, 20)
        Me.Label34.TabIndex = 156
        Me.Label34.Text = "Secondary Phone"
        Me.Label34.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'SqlCommand1
        '
        Me.SqlCommand1.CommandText = "[GetRegistrants2]"
        Me.SqlCommand1.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlCommand1.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, False, CType(0, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing), New System.Data.SqlClient.SqlParameter("@IDFld", System.Data.SqlDbType.VarChar, 30), New System.Data.SqlClient.SqlParameter("@IDVal", System.Data.SqlDbType.Int, 4)})
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.SystemColors.Highlight
        Me.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel3.Controls.Add(Me.btnSaveExit)
        Me.Panel3.Controls.Add(Me.btnSend)
        Me.Panel3.Controls.Add(Me.btnNew)
        Me.Panel3.Controls.Add(Me.btnDelete)
        Me.Panel3.Location = New System.Drawing.Point(765, 5)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(156, 40)
        Me.Panel3.TabIndex = 415
        '
        'btnHelp
        '
        Me.btnHelp.Image = Global.InfoCtr.My.Resources.Resources.btnHelp
        Me.btnHelp.Location = New System.Drawing.Point(933, 5)
        Me.btnHelp.Name = "btnHelp"
        Me.btnHelp.Size = New System.Drawing.Size(25, 25)
        Me.btnHelp.TabIndex = 420
        Me.btnHelp.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnHelp.UseVisualStyleBackColor = True
        '
        'ErrorProvider1
        '
        Me.ErrorProvider1.ContainerControl = Me
        '
        'frmMainContact
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.CausesValidation = False
        Me.ClientSize = New System.Drawing.Size(984, 591)
        Me.Controls.Add(Me.btnHelp)
        Me.Controls.Add(Me.pnlGrid)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.StatusBar1)
        Me.Controls.Add(Me.TextBox2)
        Me.Controls.Add(Me.Label26)
        Me.Controls.Add(Me.fldGoToOrg)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Panel3)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Menu = Me.MainMenu1
        Me.Name = "frmMainContact"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Tag = "CONTACT"
        Me.Text = "CONTACT DETAIL"
        Me.TabControl1.ResumeLayout(False)
        CType(Me.MainContactBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsMainContact1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.StatusBarPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.StatusBarPanelID, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.StatusBarPanel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.pnlTabs.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.pnlMailingList.ResumeLayout(False)
        Me.pnlMailingList.PerformLayout()
        Me.pnlExtras.ResumeLayout(False)
        Me.pnlExtras.PerformLayout()
        Me.pnlGrid.ResumeLayout(False)
        CType(Me.grdMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region 'windows

#Region "Load"

    'LOAD
    Private Sub frmMainContact_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles MyBase.Load
        Me.SuspendLayout()
        SetStatusBarText("loading")
SetMainDSConnection:
        Me.daMainContact.SelectCommand.Connection = sc
        Me.daMainContact.UpdateCommand.Connection = sc

SetDefaults:
        enumConvers = New structHeadings("Conversation", "CONVERSATIONS")
        enumRegistr = New structHeadings("Registration", "REGISTRATIONS")
        EnumRecommend = New structHeadings("Recommendation", "RECOMMENDATIONS")

        ctlIdentify = Me.txtLastName
        ctlNeutral = Me.btnHelp
        mainTopic = "Contact"
        mainDS = Me.DsMainContact1
        maintbl = Me.DsMainContact1.MainContact
        mainBSrce = Me.MainContactBindingSource
        mainDAdapt = Me.daMainContact

LoadCombos:
        'none- simple lists
FormSetup:
        modGlobalVar.EnableGridTextboxes(Me.grdMain)

        Forms.Add(Me)
        Me.ResumeLayout()
        isLoaded = True
        SetStatusBarText("Done")
    End Sub

    'RELOAD
    Public Sub Reload()
        SetStatusBarText("Reloading")

ResetVars:
        objHowClose = ObjClose.CloseByControl '(=5: form X)
        Me.StatusBarPanelID.Text = mainTopic & " ID: " & ThisID.ToString

FillGrid:
        FillSecondary()
        SetStatusBarText("Done")
        RefreshMailList()
    End Sub

#End Region 'load

#Region "Update Main"

    'SAVE & EXIT
    Private Sub btnSaveExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnSaveExit.Click
        objHowClose = ObjClose.btnSaveExit
        Me.Close()
    End Sub

    'Changes?
    Public Function LookForChanges() As Boolean
        Return modGlobalVar.AnyChanges(ctlNeutral, mainBSrce, maintbl)
    End Function

    'CLOSING & ask user & data validation & Release Form
    Private Sub frm_Closing(ByVal sender As Object, ByVal e As FormClosingEventArgs) _
               Handles MyBase.FormClosing
        'force valid email
        If Me.ErrorProvider1.GetError(Me.txtEmail) = String.Empty Then
        Else
            If objHowClose = ObjClose.miAskSave Then
            Else
                Exit Sub
            End If
        End If
        Dim arCtls(0) As Control
        Dim ctl As Control
        Dim bChanges As Boolean

        If objHowClose = ObjClose.miDelete Then
            GoTo callupdate
        End If

LookForChanges:
        bChanges = LookForChanges() 'modGlobalVar.AnyChanges(ctlNeutral, mainBSrce)

AllowCloseWoutSaving:
        If objHowClose = ObjClose.miAskSave Then
            Select Case AskAcceptChanges(bChanges, mainTopic)
                Case Is = ObjClose.cancelClose
                    e.Cancel = True
                    GoTo ReleaseForm
                Case Is = ObjClose.DontSaveClose
                    ' GoTo validate ' notify of missing fields??
                    e.Cancel = False
                    GoTo ReleaseForm
                Case Is = ObjClose.SaveClose
                    e.Cancel = False
                    GoTo callupdate
            End Select
        End If
SkipUpdate:  'so LastChangeDate doesn't get updated
        If bChanges = False Then
            e.Cancel = False
            GoTo ReleaseForm
        End If

CallUpdate:
        If DoUpdate() Then
            e.Cancel = False
        Else                'update didn't work
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
            Case Else 'btnSaveExit, SaveClose,, ObjClose.noChanges
                arCtls = CheckRequired()
                If arCtls.GetLength(0) > 1 Then 'required info missing
                    ctl = arCtls(0)
                    ''INSERT DEFAULT DATA - no, could be blank last name??
                    'If objHowClose = ObjClose.SaveClose Or e.CloseReason = Windows.Forms.CloseReason.UserClosing Then
                    '    If ctl Is ctlIdentify Then
                    '        ctl.Text = usrName & " " & Today.ToShortDateString
                    '        mainBSrce.EndEdit()
                    '        mainDAdapt.Update(mainDS) 'save default data
                    '    End If
                    'End If
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
            ClassOpenForms.frmMainContact = Nothing 'reset global var
        Else
        End If
    End Sub

    'UPDATE
    Public Function DoUpdate() As Boolean
        Dim i As Integer

        MouseWait()
        mainBSrce.EndEdit() 'gets rid of proposed version

UpdateBackend:
        SetStatusBarText("Updating server 1")
        Try
            i = mainDAdapt.Update(maintbl)
            DoUpdate = True
        Catch ex As Exception
            modGlobalVar.msg("ERROR: updating " & mainTopic, ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
            DoUpdate = False
        Finally
            '  modGlobalVar.Msg("main: " & i.ToString& NextLine &  "addresses: " & f.ToString, , "updated")
        End Try

CloseAll:
        SetStatusBarText("Update routine complete")
        MouseDefault()
    End Function

#End Region 'update

#Region "Menu Items"

    'mi ALLOW CLOSE WITHOUT SAVING
    Private Sub miCloseForm_Click(ByVal sender As Object, ByVal e As System.EventArgs) _
        Handles miClose.Click
        '   If Me.Validate = True Then 'force email validation
        objHowClose = ObjClose.miAskSave
        Me.Close()
        'End If
    End Sub

    'mi SAVE
    Private Sub miSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
     Handles miSave.Click
        DoUpdate()
    End Sub

    'mi CANCEL CHANGES
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles miCancel.Click
        Try
            mainBSrce.CancelEdit()
            mainDS.RejectChanges()
        Catch ex As System.Exception
            modGlobalVar.msg("ERROR: cancelling changes ", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        SetStatusBarText("Changes Cancelled")
    End Sub


    'DELETE 
    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles btnDelete.Click, miDelete.Click

        If modGlobalVar.msg("CONFIRM DELETE", mainTopic & ": " & IsNull(ctlIdentify.Text, ""), MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) = DialogResult.Yes Then
            Me.chkActive.CheckState = CheckState.Unchecked
            Try
                Me.txtLastName.Text = "DELETE: " & IsNull(Me.txtLastName.Text, "")
            Catch ex As Exception
                Me.txtLastName.Text = "DELETE"
            Finally
            End Try
            ' Me.chkActive.Checked = False
            'mainBSrce.EndEdit()
            objHowClose = ObjClose.miDelete
            Me.Close()
        End If

    End Sub

    'ENABLE MENU ITEMS based on TabControl selected
    Private Sub mmGoTo_Popup(sender As System.Object, e As System.EventArgs) Handles mmGoto.Popup
        Me.miGoToConversation.Enabled = False
        Me.miGotoRegistration.Enabled = False
        Me.miGoToRecommend.Enabled = False
        Select Case Me.TabControl1.SelectedTab.Tag
            Case Is = enumConvers.PluralName
                miGoToConversation.Enabled = True
            Case Is = enumRegistr.PluralName
                Me.miGotoRegistration.Enabled = True
            Case Is = EnumRecommend.PluralName
                Me.miGoToRecommend.Enabled = True
            Case Else
                '  modGlobalVar.Msg(Me.TabControl1.SelectedTab.Name.ToString, , "clicked")
        End Select
    End Sub

    'HELP BUTTON
    Private Sub btnHelp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles btnHelp.Click, miHelp.Click
        modGlobalVar.msg("CONTACT HELP", "TO ADD NEW CONTACT" & NextLine & NextLine & "Go to Organization Edit window and click New button" & NextLine & _
        "'Prefix' refers to terms like 'Reverend, Mr./Mrs. etc." & NextLine & "'JobTitle' refers to terms like 'Pastor, Chair of Board, etc.'" & NextLine & _
        "'Active' means attends this congregation whether or not in a leadership role" & NextLine & _
        "MAILING LIST: To remove from mailing list, uncheck 'Postal Mail List' and/or 'Email List'" & NextLine & _
        "Home address will not be used even if filled in unless the 'Use Home Address' box is checked" & NextLine & _
        "SPECIAL MAIL FLAG: create a new one with the New button, assign one or more with the Flag button.  Doubleclick field to delete one." & NextLine & _
        "Go to Report & Utilities --> Mailing Labels to generate datafile.", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

#End Region 'menu

#Region "Validating"

    'CHECK REQUIRED FIELDS w Error Provider
    Private Function CheckRequired() As Control()

        Dim arCtls(0) As Control
        '  Dim ctl As Control
        Dim i As Integer = 0

        'NOTE only name required here.  If add another required field, and 'or else if ' to  WarningClose message
        If Me.txtLastName.Text = String.Empty And Me.txtFirstName.Text = String.Empty Then
            Me.ErrorProvider1.SetError(ctlIdentify, "or select menu item Edit, Delete")
            Me.ErrorProvider1.SetError(txtFirstName, "a First and/or Last Name is required")
            arCtls(i) = Me.txtLastName
            i = i + 1
            ReDim Preserve arCtls(arCtls.GetUpperBound(0) + 1)
            Me.ErrorProvider1.SetError(ctlIdentify, "or select menu item Edit, Delete")
            Me.ErrorProvider1.SetError(txtFirstName, "a First and/or Last Name is required")
            arCtls(i) = Me.txtFirstName
            i = i + 1
            ReDim Preserve arCtls(arCtls.GetUpperBound(0) + 1)
        Else
            Me.ErrorProvider1.SetError(Me.txtLastName, "")
            Me.ErrorProvider1.SetError(Me.txtFirstName, "")
        End If

        arCtls(i) = ctlNeutral
        Return arCtls

    End Function

    'REMOVE LINE FEEDS
    Private Sub TextBox_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) _
Handles txtLastName.Validating, txtFirstName.Validating, txtStreet.Validating
        modGlobalVar.RemoveLineFeeds(sender)

    End Sub

    'REMOVE LINE FEEDS
    Private Sub txtEmail_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) _
Handles txtEmail.Validating
        If sender.text = String.Empty Then
            Exit Sub
        End If
        modGlobalVar.RemoveLineFeeds(sender)
        e.Cancel = Not (modGlobalVar.ValidateEmail(sender, Me.ErrorProvider1))
    End Sub

#End Region 'validating

#Region "Fill Datasets"

    'FILL GRID
    Public Sub FillSecondary()
        Dim cmd As New SqlCommand(" ", sc)
        SetStatusBarText("Retrieving data...")
        MouseWait()
        SetTabCaptions()

        cmd.Parameters.Add("@IDVal", System.Data.SqlDbType.Int).Value = ThisID
        cmd.Parameters.Add("@IDFld", System.Data.SqlDbType.VarChar, 30).Value = "Contact"
        cmd.CommandType = System.Data.CommandType.StoredProcedure
        Try
            tbl.Columns.Clear()
        Catch ex As System.Exception
        End Try

        Select Case TabControl1.SelectedTab.Tag
            Case Is = enumConvers.PluralName
                cmd.CommandText = "[GetConversations]"
                tbl = New DataTable("GetConversations")
            Case Is = enumRegistr.PluralName
                cmd.CommandText = "[GetRegistrants2]"
                tbl = New DataTable("GetWRegistrations")
            Case Is = EnumRecommend.PluralName
                cmd.CommandText = "[GetResRecommendation]"
                tbl = New DataTable("GetRecommendations")
            Case Is = "EXTRAS"
                'make update fields visible
                GoTo CloseAll
            Case Else
                GoTo CloseAll
        End Select
        If Not SCConnect() Then
            GoTo CloseAll
        End If
        Try
            tbl.Load(cmd.ExecuteReader(CommandBehavior.CloseConnection))
        Catch ex As Exception
            modGlobalVar.msg("ERROR - filling " + TabControl1.SelectedTab.Tag, ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
            GoTo CloseAll
        End Try

        'ASSIGN DATAVIEW
        dv = New DataView(tbl)
        Me.grdMain.DataSource = tbl

CloseAll:
        SetStatusBarText("Done")
        MouseDefault()
    End Sub

    'RESET TAB CAPTION COUNT
    Public Sub SetTabCaptions()

        Dim cmdCntID As New SqlCommand("", sc)
        Dim i As Integer = 0


        'RESET SELECTED ITEM INDICATOR
        modGlobalVar.ClearIDLbls(Me.lblSelectedID, Me.lblSelectedWhat)

        If Not SCConnect() Then
            Exit Sub
        End If

        'CONVERSATIONS
        cmdCntID.CommandText = "SELECT COUNT(ConversID) FROM tblConversation WHERE ContactNum = " & ThisID
        Try
            i = cmdCntID.ExecuteScalar()
        Catch ex As Exception
        End Try
        Me.TabControl1.TabPages("pg" & enumConvers.SingleName).Text = i.ToString() & "  " & enumConvers.PluralName

        'REGISTRATIONS
        cmdCntID.CommandText = modGlobalVar.CountValidRegistrations(ThisID, "Contact")
        i = 0
        Try
            i = cmdCntID.ExecuteScalar()
        Catch ex As Exception
        End Try
        Me.TabControl1.TabPages("pg" & enumRegistr.SingleName).Text = i.ToString() & "  " & enumRegistr.PluralName

        'RECOMMENDATIONS
        cmdCntID.CommandText = "SELECT COUNT(RecommendID) FROM tblResourceRecommend WHERE ContactNum = " & ThisID
        i = 0
        Try
            i = cmdCntID.ExecuteScalar()
        Catch ex As Exception
        End Try
        Me.TabControl1.TabPages("pg" & EnumRecommend.SingleName).Text = i.ToString() & "  " & EnumRecommend.PluralName

        sc.Close()

    End Sub

    'CALL FILL SECONDARY & SET VISIBLITY based on TAB USER SELECTED
    Private Sub TabControl1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles TabControl1.SelectedIndexChanged

        If Not isLoaded Then
            Exit Sub
        End If
        '  mdGlobalVar.ClearIDLbls(Me.lblSelectedID, Me.lblSelectedWhat)
        ' Me.grdMain.CaptionText = ""
        Select Case TabControl1.SelectedTab.Tag
            Case Is = "HOME ADDRESS"
                Me.pnlGrid.Visible = False
                Me.pnlExtras.Visible = False
                Me.lblExtra.Visible = False
            Case Is = enumConvers.PluralName, enumRegistr.PluralName, EnumRecommend.PluralName
                Me.pnlGrid.Visible = True
                Me.pnlExtras.Visible = False
                Me.lblExtra.Visible = False
                FillSecondary() 'refresh
            Case Is = "EXTRAS"
                Me.pnlGrid.Visible = False
                Me.pnlExtras.Visible = True
                Me.lblExtra.Visible = True
        End Select
    End Sub

#End Region

#Region "Datagrid"

    'CELL CHANGE - HIGHLIGHT ROW
    Private Sub grdMain_CurrentCellChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles grdMain.CurrentCellChanged

        'DISPLAY SELECTED ITEM ID LABEL
        Me.lblSelectedID.Text = Me.grdMain.Item(grdMain.CurrentCell.RowNumber, 0)
        Me.lblSelectedWhat.Text = Me.TabControl1.SelectedTab.Name.Substring(2) & " ID:"

        'HIGHLIGHT SELECTED ROW
        Me.grdMain.Select(grdMain.CurrentCell.RowNumber)

    End Sub

    '    'CAPTURE RIGHT MOUSE CLICK TO FILTER APPROPRIATE GRID
    Protected Sub grdAll_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) _
Handles grdMain.MouseDown

        Dim strHdr As String    'text for grid header
        hti = sender.HitTest(e.X, e.Y)

IfRightMouseclick:
        If e.Button = Windows.Forms.MouseButtons.Right Then
            strHdr = Me.TabControl1.SelectedTab.Tag
SetOrClearFilter:
            If hti.Type = DataGrid.HitTestType.Cell Then    'SET FILTER
                If IsDBNull(sender.Item(hti.Row, hti.Column)) Then 'nulls cause filter error
                    modGlobalVar.msg(MsgCodes.filterEmpty)
                    Exit Sub
                Else
                    grdFilter(sender, strHdr, dv)
                End If
            Else                                            'not in cell, CLEAR FILTER
                Me.grdMain.DataSource = tbl 'removes dv.rowfilter
                Me.lblSelectedID.Text = ""
                sender.CaptionText = tbl.Rows.Count.ToString & "  " & strHdr 'DsOrgSearch1.tblOrg.Rows.Count.ToString
                statusM = ""
                SetStatusBarText(statusM & " " & statusS1 & " " & statusS2)
            End If
IfLeftMouseClick:
        Else    'left mouse
            'strbActiveGrid.Replace(strbActiveGrid.ToString, Me.TabControl1.SelectedTab.Tag)
        End If

    End Sub

    'FILTER METHOD
    Private Sub grdFilter(ByVal grd As Object, ByVal strHdr As String, ByVal dv As DataView)
        Dim strFilter As String
        Dim myColumns As GridColumnStylesCollection

        myColumns = grd.TableStyles(tbl.TableName).GridColumnStyles
        strFilter = myColumns(hti.Column).MappingName
        strFilter = strFilter & " = '" & grd.Item(hti.Row, hti.Column) & "'"
        Try
            dv.RowFilter = strFilter
            grd.DataSource = dv
            grd.CaptionText = dv.Count.ToString & "/" & tbl.Rows.Count.ToString & "  " & strHdr
        Catch ex As Exception
        End Try
        statusM = strHdr & " filtered on " & myColumns(hti.Column).HeaderText & " = " & Me.grdMain.Item(Me.grdMain.CurrentRowIndex, hti.Column)
        SetStatusBarText(statusM & " " & statusS1 & " " & statusS2)

    End Sub

    'CLEAR SELECTION FROM DATAGRIDS AND LOSE FOCUS (note: selection stays with arrow)
    Private Sub grdUnselect_Click(ByVal sender As Object, ByVal e As System.EventArgs) _
  Handles MyBase.Click, grdMain.LostFocus
        If grdMain.CurrentRowIndex > -1 Then
            Me.grdMain.UnSelect(grdMain.CurrentRowIndex)
            Me.grdMain.NavigateBack()
        End If
    End Sub

    'CALL OPEN MAIN DETAIL FORMS FROM DATAGRID
    Private Sub grdMain_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdMain.DoubleClick
        OpenForms()
    End Sub

#End Region     'datagrid

#Region "Open Secondary Forms"

    'OPEN SECONDARY FORMS
    Private Sub OpenForms()
        If Me.lblSelectedID.Text = String.Empty Then
            '   modGlobalVar.msg("Cancelling Request", "select a row in the grid", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            modGlobalVar.msg(MsgCodes.noRowSelected)
            Exit Sub
        End If
        Dim gotoOrgName As String = Me.fldGoToOrg.Text
        Dim cri As Integer = Me.grdMain.CurrentRowIndex
        Dim str As String   'item name for conversation
        MouseWait()
        SetStatusBarText("opening " & TabControl1.SelectedTab.Tag & "...")

        'TODO add select statement for conversation or registration
        Select Case Me.lblSelectedWhat.Text.Replace(" ID:", String.Empty)
            Case Is = enumConvers.SingleName
                If IsDBNull(Me.grdMain.Item(cri, 4)) Then
                    str = ""
                Else
                    str = SubstrBriefSummary(Me.grdMain.Item(cri, 4))
                End If
                modGlobalVar.OpenMainConversation(Me.lblSelectedID.Text, str, Me.fldGoToOrg.Text, Me.fldOrgNum.Text)

            Case Is = enumRegistr.SingleName  'registration
                'don't like this loading everytime, but may need to refresh
                ' try open event search and do it there.
                'LoadWEventDD("1/1/1997")
                'LoadRegistrantDD(False)
                ' LoadSrchWEvent(False)
                If tblRegistrant.Rows.Count = 0 Then
                    modGlobalVar.LoadRegistrantDD(False)
                End If
                modGlobalVar.OpenMainWReg2(Me.lblSelectedID.Text, "Registration for: " & IsNull(Me.txtFirstName.Text, ""), True, "Registrant")

            Case Is = EnumRecommend.SingleName   'recommend
                modGlobalVar.OpenMainRecommend(Me.lblSelectedID.Text, str, Me.fldOrgNum.Text)

                'Case Is = "REGORDER" 'order 
                'i = Me.grdMain.TableStyles("GetWRegistrations").GridColumnStyles.IndexOf(Me.grdMain.TableStyles("GetWRegistrations").GridColumnStyles("OrderNum"))
                '    OpenSrchWEvent(Me.grdMain.Item(cri, i), True)
            Case Else
                ' modGlobalVar.Msg(Me.TabControl1.SelectedTab.Tag, , "tab tag not found")
        End Select
        SetStatusBarText("Done")
        MouseDefault()
    End Sub

    ' CALL OPEN SECONDARY FORMS from MENU ITEMS
    Private Sub miGoTo_Click(ByVal sender As Object, ByVal e As System.EventArgs) _
        Handles miGoToRecommend.Click, miGoToConversation.Click, miGotoRegistration.Click
        OpenForms()
    End Sub

    'OPEN MAIN ORG FORM
    Private Sub fldGoToOrg_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) _
      Handles fldGoToOrg.Click, miGotoOrg.Click
        SetStatusBarText("Opening Organization Detail window")
        modGlobalVar.OpenMainOrg(Me.fldOrgNum.Text, Me.fldGoToOrg.Text)
        SetStatusBarText("Done")
    End Sub

#End Region

#Region "ADD ITEM"

    'INSERT NEW ITEM to BACKEND and OPEN DETAIL FORM
    Protected Sub miAddNew(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles miAddConversation.Click, miAddRegistration.Click, btnNew.Click
        Dim pp As New ContextMenu
        Dim eh As EventHandler = AddressOf AddNew

        'ENHANCE set default as selected tab or conversation if none selected
        pp.MenuItems.Add("SELECT NEW ITEM FOR CONTACT: " + IsNull(Me.txtFirstName.Text, "") & " " & IsNull(Me.txtLastName.Text, ""))
        pp.MenuItems.Add("---------------------------------------------")
        pp.MenuItems.Add(enumConvers.SingleName, eh) 'conversation
        pp.MenuItems.Add(enumRegistr.SingleName, eh) 'registration
        '  pp.MenuItems.Add("Special Mail Flag", eh)
        pp.MenuItems(2).DefaultItem = True
        ' cm.MenuItems.Add("Grant", eh)
        pp.Show(Me, New Point(200, 10))
    End Sub

    'INSERT NEW ITEM to BACKEND and OPEN DETAIL FORM
    Private Sub AddNew(ByVal obj As Object, ByVal ea As EventArgs)
        Dim str As String
        Dim newID As Integer

        If modGlobalVar.msg("About to enter a new " & UCase(obj.text), "Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then
            Exit Sub
        End If
        Me.miSave.PerformClick()

        Select Case obj.text 'UCase(strE)
            Case Is = enumConvers.SingleName 'strData1 'conversation
                str = "INSERT INTO tblConversation(ContactNum, OrgNum, StaffNum,ConversDate) VALUES (" & ThisID & ", " & Me.fldOrgNum.Text & ", " & usr & ", N'" & Now & "'); SELECT @@Identity"
            Case Is = enumRegistr.SingleName '"REGISTRATIONS" 'registration
                ' newID = modPopup.InsertRegistration(0, 0, ThisID)
                Dim frm As New frmNewWReg2
                If frm.Loadcombos() Then
                    frm.SelectCombo(frm.cboRegistrant, ThisID)
                End If
                ' frm.cboEvent.SelectedIndex = -1
                frm.cboEvent.Focus()
                frm.ShowDialog()
                Exit Sub
            Case Is = "Special Mail Flag"
                GetNewMailFlag()
            Case Else
                msg("ATTENTION: Invalid Search", obj.text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Select

InsertNewItem:
        If Not SCConnect() Then
            Exit Sub
        End If

        Dim cmd As New SqlCommand(str, sc)
        Try
            newID = cmd.ExecuteScalar() 'CommandBehavior.CloseConnection)
        Catch ex As Exception
            modGlobalVar.msg("ERROR: insert from Contact detail", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            sc.Close()
        End Try

OpenForm:
        Try
            Select Case obj.text 'UCase(strE)
                Case Is = enumConvers.SingleName 'strData1 '
                    modGlobalVar.OpenMainConversation(newID, "Entering new conversation with: " & Me.txtFirstName.Text & " " & Me.txtLastName.Text, Me.fldGoToOrg.Text, Me.fldOrgNum.Text)
                    '  Case Is = enumRegistr.SingleName '"REGISTRATIONS"
                    '      modGlobalVar.OpenMainWReg2(newID, "Entering new registration with: " & IsNull(Me.txtFirstName.Text, ""), True, "Registrant")
                Case Else
                    ' modGlobalVar.Msg("ERROR: not found", obj.text, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Select

        Catch ex As System.Exception

        End Try
    End Sub

#End Region

#Region "General"

    'SET STATUS BAR LEFT TEXT
    Private Sub SetStatusBarText(ByVal str As String)
        Me.StatusBarPanel1.Text = str
        ' Me.StatusBar1.Panels(0).Text = statusM & " " & statusS1 & " " & statusS2
    End Sub

    'COPY ID
    Private Sub StatusBar1_DoubleClick(sender As System.Object, e As System.EventArgs) Handles StatusBar1.DoubleClick
        Clipboard.SetText(ThisID)
    End Sub

    'VERIFY PHONE NUMBER FORMATTED OK
    Private Sub editPhone_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles txtCellPhone.Leave, txtHomeFax.Leave, txtHomePhone.Leave, txtWorkPhone.Leave, txtPrimPhone.Leave, txtSecondPhone.Leave

        If Len(sender.text) > 0 Then
            If modGlobalVar.LeavePhone(sender, IsNull(Me.txtCountry, "USA")) = True Then
                Me.ErrorProvider1.SetError(sender, "")
            Else
                Me.ErrorProvider1.SetError(sender, "invalid phone number")
            End If
        End If
    End Sub

    'RIGHT CLICK MENU   
    Private Sub textbox_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) _
        Handles RichTextBox2.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Right Then
            Dim pp As New ClassRTBContextMenu(sender)
            pp.Show(Me, PointToClient(Control.MousePosition))
        End If
    End Sub

    'VALIDATE SPELLING INDY/FT WAYNE
    Private Sub txtCity_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles txtCity.Leave
        Me.txtCity.Text = modGlobalVar.CheckCity(CType(Me.txtCity, System.Windows.Forms.TextBox), Me.txtCity.Text)
        modGlobalVar.RemoveLineFeeds(sender)

    End Sub

    'UNDO
    Protected Overrides Function ProcessDialogKey(ByVal keyData As Keys) As Boolean
        If keyData = System.Windows.Forms.Keys.Escape Then
            modPopup.UndoCtl(Me.ActiveControl)

            Return True  ' True means we've processed the key
        Else
            Return MyBase.ProcessDialogKey(keyData)
        End If
    End Function

    'Display StaffName
    Private Sub TextBox_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) _
            Handles fldCreateStaff.MouseHover, fldLastChangeStaff.MouseHover

        Me.ToolTip1.SetToolTip(sender, sender.tag & ": " & modPopup.ShowStaff(sender.text))

    End Sub

#End Region

#Region "Send Mail"

    'SEND EMAIL
    Private Sub miEmail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles miEmail.Click
        Dim str As String
        MouseWait()
        If Me.txtEmail.Text = String.Empty Then    'no personal email address
            If Me.cboClergy.SelectedIndex > -1 Then
                'TODO Get Org email address if is staff
                Dim cmd As New SqlCommand("SELECT isnull(Email,'none') as Email FROM dbo.tblOrg WHERE OrgID = " & Me.fldOrgNum.Text, sc)
                If Not SCConnect() Then
                    GoTo CloseAll
                End If

                str = cmd.ExecuteScalar(CommandBehavior.CloseConnection).ToString
                modGlobalVar.msg("FYI No personal email address", "This person is on staff, so the email will be sent to the organization's email address: " & str, MessageBoxButtons.OK, MessageBoxIcon.Information)
                If str = "none" Then
                    modGlobalVar.msg("MISSING EMAIL ADDRESS", "email address is required", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Else
                    modPopup.EmailOutlook(str)
                End If
            Else
                modGlobalVar.msg("MISSING EMAIL ADDRESS", "This person is not indicated as Staff, so a personal email address is required.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If
        Else
            ' SendEmail.SendEmailMessage(Me.txtcontactID.Text, Me.txtOrgNum.Text, 0, usr, Me.txtEmail.Text, "")
            modPopup.EmailOutlook(Me.txtEmail.Text)
        End If
CloseAll:
        ' str = Nothing
        MouseDefault()
    End Sub

    'OPEN WORD DOCUMENT
    Private Sub miLetter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles miLetter.Click
        MouseWait()
        pref = modPopup.GetPrefix(Me.txtPrefix.Text)
        suff = modPopup.GetSuffix(Me.txtSuffix.Text)
        '([tblcontact]![Prefix]+" ") & [tblcontact]![FirstName] & " " & [tblcontact]![Lastname] & (+", "+[tblContact!Suffix]) AS FullName,
        Who = pref & IsNull(Me.txtFirstName.Text, "") & " " & IsNull(Me.txtLastName.Text, "") & suff
        If Me.chkUseHome.Checked Then
            modPopup.MailLetter("new", False, Me.fldOrgNum.Text, Who, "", Me.txtStreet.Text, Me.txtCity.Text, Me.txtState.Text, Me.txtZip.Text, Me.txtFirstName.Text, Me.txtLastName.Text, "", "", "", "", "", "")
        Else
            modPopup.MailLetter("new", False, Me.fldOrgNum.Text, Who, "", "", "", "", "", Me.txtFirstName.Text, Me.txtLastName.Text, "", "", "", "", "", "")
        End If
        MouseDefault()
    End Sub

    'MI INTRO LETTER
    Private Sub miIntro_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles miIntro.Click
        Dim datafileName As String = "DataDoc"
        Dim i As Integer = ThisID
        Dim sheetLabel As String = "intro"
        MouseWait()
        Try
            If StrmWriter("[MergeLetter]", i, datafileName, "ContactIntro") Then
                If modPopup.DataToExcel(datafileName, sheetLabel) = String.Empty Then
                    modGlobalVar.msg("ERROR merge letter DataToExcel", "MergeLetter could not create datafile", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Else
                    modPopup.MergePerform(SOPPath & "MergeInfoCtrtoWord\ICCFolderIntroLttrVS.dotx", datafileName, sheetLabel)
                End If
            End If
        Catch ex As Exception
            modGlobalVar.msg("ERROR: streamwriterMergeletter", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        MouseDefault()
        ' strDataDoc = Nothing
    End Sub

    'BTN SEND
    Private Sub btnSend_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnSend.Click
        Dim pp As ContextMenu = New ContextMenu

        pp.MenuItems.Clear()
        'LOAD POPUPMENU
        pp.MenuItems.Add("Letter", AddressOf Me.miLetter_Click)
        pp.MenuItems.Add("Email", AddressOf Me.miEmail_Click)
        pp.MenuItems.Add("Intro Letter", AddressOf Me.miIntro_Click)

        pp.MenuItems(0).DefaultItem = True
        pp.Show(Me, PointToClient(Control.MousePosition))
    End Sub

    'REVERSE MAIL LIST CHECKBOXES by staff request
    Private Sub chkRemovePostal_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) _
            Handles chkRemovePostal.Click, chkRemoveEmail.Click 'chkRemovePostal.CheckedChanged, chkRemoveEmail.CheckedChanged
        Dim b As Boolean
        b = sender.checked
        ' If isLoaded Then
        Select Case sender.tag
            Case Is = "Postal"
                Me.DsMainContact1.MainContact.Rows(0)("MailListPostal") = Not b
                ' Me.chkPostal.Checked = Not sender.checked
            Case Is = "Email"
                Me.DsMainContact1.MainContact.Rows(0)("MailListEmail") = Not b
                'Me.chkEmail.Checked = Not sender.checked
        End Select
    End Sub

#End Region 'send mail

#Region "Zip"

    'GET Indiana ZIP
    Private Sub textState_Leave(ByVal sender As Object, ByVal e As System.EventArgs) _
            Handles txtState.Leave
        'find zip if city only in table once
        'force upper case
        Me.txtState.Text = UCase(Me.txtState.Text)

        'TODO add City lookup from Zip
        If IsDBNull(Me.txtCity.Text) Then
            Exit Sub
        End If

        'only have indiana zip data table
        If UCase(Me.txtState.Text).StartsWith("IN") Then
        Else
            Exit Sub
        End If

         Dim sqlGetZip As New SqlCommand("SELECT Count (distinct(Zip)) FROM  luCountyZip WHERE City = '" & Me.txtCity.Text & "'; SELECT Zip, County, SatelliteRegion, City FROM luCountyZip WHERE (Zip > 0) AND (City = '" & Me.txtCity.Text & "')", sc)
        Dim strbConcat As New StringBuilder
        strbConcat.Append(" ")
        Dim pp As New ContextMenu
        Dim eh As EventHandler = AddressOf SetZip
        pp.MenuItems.Add("Select Zipcode, County, Region")
        pp.MenuItems.Add("-------------------------------------")

        If UCase(Me.txtState.Text) = "IN" Then
            If Not SCConnect() Then
                Exit Sub
            End If
            Dim rdr As SqlDataReader
            Try
                rdr = sqlGetZip.ExecuteReader(CommandBehavior.CloseConnection)
            Catch ex As Exception
                modGlobalVar.msg("ERROR: can't get zips", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try

            rdr.Read()
            Dim x As Integer = rdr.GetInt32(0)
            Select Case x
                Case Is = 0 'none found
                    rdr.Close()
                    Exit Sub
                Case Is > 1 'let user choose from popupmenu
                    rdr.NextResult()
                    While rdr.Read
                        If Me.txtZip.Text = rdr.GetString(0) Then
                            rdr.Close()
                            Exit Sub
                        End If
                        strbConcat.Remove(0, Len(strbConcat.ToString))
                        For y As Integer = 0 To 2
                            If IsDBNull(rdr(y)) Then
                            Else
                                strbConcat.Append(rdr.GetString(y) & " - ")
                            End If
                        Next y
                        pp.MenuItems.Add(strbConcat.ToString, eh)
                    End While
                    rdr.Close()
                    GoTo RunPopup

                Case Is = 1 'confirm change zip

                    rdr.NextResult()
                    rdr.Read()

                    If Me.txtZip.Text > "" Then
                        If Me.txtZip.Text.Substring(0, 5) <> rdr.GetString(0) Then
                            If modGlobalVar.msg("CONFIRM: changing zip", "from " & Me.txtZip.Text & " to " & rdr.GetString(0) & NextLine & "based on post office data", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                                Me.txtZip.Text = rdr.GetString(0)

                            End If
                        End If
                    Else
                        Me.txtZip.Text = rdr.GetString(0)

                    End If
                    rdr.Close()
                    Exit Sub
            End Select
            End
        Else    'not indiana
            Exit Sub
        End If

RunPopup:  'show popup whether full or populated from query
        pp.MenuItems(0).DefaultItem = True
        pp.Show(Me, New Point(200, 10))
 
    End Sub

    'ZIP FORMAT, GET COUNTY, REGION
    Private Sub txtZip_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles txtZip.Leave
IsIndiana:
        'If (UCase(Me.txtState.Text) <> "IN" And Me.txtState.Text > "") Then 'another state, skip this routine
        '    Exit Sub
        'End If
FormatZip:
        If Me.txtZip.Text = String.Empty Then
            Exit Sub
        Else
            Dim rtrn As String = modGlobalVar.FormatZip(sender, e, Me.txtState.Text)
            Select Case rtrn
                Case usrInput.Ignore
                    Exit Sub 'out of state
                Case usrInput.OK    'proceed
                Case usrInput.Retry
                    Me.txtZip.Focus()
                    Exit Sub
                Case Else
                    Me.txtZip.Text = rtrn
            End Select
            ' rtrn = Nothing

        End If

GetCity:
        Dim strbConcat As New StringBuilder
        strbConcat.Append(" ")
        Dim pp As New ContextMenu
        Dim eh As EventHandler = AddressOf SetCity
        pp.MenuItems.Add("City undetermined - select from list")
        pp.MenuItems.Add("-------------------------------------")
        'get region, county
        Dim sqlCounty As New SqlCommand("SELECT Count(City)as #Cities FROM luCountyZip WHERE Zip = '" & Me.txtZip.Text.Substring(0, 5) & "' GROUP BY Zip; Select Distinct City, County, SatelliteRegion FROM luCountyZip WHERE Zip = '" & Me.txtZip.Text.Substring(0, 5) & "' ORDER BY City, SatelliteRegion, County", sc)
        Dim rdr As SqlDataReader
        If Not SCConnect() Then
            Exit Sub
        End If

        Try
            rdr = sqlCounty.ExecuteReader(CommandBehavior.CloseConnection)
        Catch ex As Exception
            modGlobalVar.msg("ERROR: can't get city", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        If rdr.HasRows Then
            rdr.Read()  'count of rows returned
            '  modGlobalVar.Msg(rdr.GetInt32(0).ToString, , "how many rows")
            If rdr.GetInt32(0) > 1 Then       'multiple counties to choose from
                rdr.NextResult()
                While rdr.Read
                    strbConcat.Remove(0, Len(strbConcat.ToString))
                    If Me.txtCity.Text = rdr.GetString(0) Then
                        rdr.Close()
                        Exit Sub
                    End If
                    For x As Integer = 0 To 2
                        If IsDBNull(rdr(x)) Then
                        Else
                            strbConcat.Append(rdr.GetString(x) & " - ")
                        End If
                    Next x
                    pp.MenuItems.Add(strbConcat.ToString, eh)
                End While
                rdr.Close()
                GoTo RunPopup
            Else    'single city
                rdr.NextResult()
                While rdr.Read
                    If IsDBNull(rdr(0)) Then    'city
                    Else
                        If Me.txtCity.Text = rdr.GetString(0) Then
                        Else
                            If Me.txtCity.Text > "" Then
                                Dim dl As New ClassDialog("City doesn't match zip", "Change city to " & rdr.GetString(0), "No, leave city as " & Me.txtCity.Text, "According to the post office tables, " & rdr.GetString(0) & " is the city for the zip listed here: " & Me.txtZip.Text & ".", False)
                                If dl.DialogResult.ToString = "Yes" Then  '   If modGlobalVar.Msg("Change City from " & Me.txtCity.Text & " to " & rdr.GetString(0) & "?", MessageBoxButtons.YesNo, "City does not match this zip code") = DialogResult.Yes Then
                                    Me.txtCity.Text = rdr.GetString(0)

                                End If
                            Else
                                Me.txtCity.Text = rdr.GetString(0)

                            End If
                        End If
                    End If
                End While
                rdr.Close()
                Exit Sub
            End If
        Else    'zip not found in datatable
            rdr.Close()
            Exit Sub
        End If

RunPopup:  'show popup whether full or populated from query
        pp.MenuItems(0).DefaultItem = True
        pp.Show(Me, New Point(200, 10))
    End Sub

    'SET CITY
    Private Sub SetCity(ByVal obj As Object, ByVal ea As EventArgs)
        Dim i As Integer

        If obj.text.contains(" - ") Then
            i = InStr(obj.text, " -")
            Try
                Me.txtCity.Text = obj.text.substring(0, i - 1)
            Catch ex As Exception
                modGlobalVar.msg("ERROR: get county/zip", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try

        Else

        End If

    End Sub

    'SET Zip
    Private Sub SetZip(ByVal obj As Object, ByVal ea As EventArgs)
        Dim i As Integer

        i = InStr(obj.text, " -")
        Try
            Me.txtZip.Text = obj.text.substring(0, i - 1)

        Catch ex As Exception
            modGlobalVar.msg("ERROR: set zip", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

    End Sub

#End Region 'zipcode

#Region "Mailing Lists"

    'REFRESH Maillist textbox
    Private Sub RefreshMailList()
        Dim str As String = Me.fldMailListCSV.Text
        Dim sql As New SqlCommand("SELECT dbo.fDynamicString('MailListContact', DEFAULT, " & ThisID & ") AS Lst", sc)
        If Not SCConnect() Then
            Exit Sub
        End If
        Try
            Me.fldMailListCSV.Text = sql.ExecuteScalar.ToString
        Catch ex As System.Exception
            modGlobalVar.msg("ERROR: term reload  ", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        sc.Close()
SetLastChangeDate:
        If isLoaded Then
            If fldMailListCSV.Text = str Then
            Else
                SetLastChanged()
            End If
        End If

    End Sub

    'append SpecialMail flag from popup
    Private Sub btnSpecialMail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnMailListAdd.Click
        Dim ppup As New ContextMenu
        Dim eh As EventHandler = AddressOf PopupMailList

        modGlobalVar.CreateSpecialMailDS()

        Try
            ppup.MenuItems.Add("SELECT a MAILING LIST")
            ppup.MenuItems.Item(0).Enabled = False
            ppup.MenuItems.Add("Create new Mailing List", eh)

            For Each row As DataRow In tblSpecialMail.Rows '.Tables("dtSpecialMail").Rows
                ppup.MenuItems.Add(row("MailListName"), eh)
            Next
            ' ppup.MenuItems(0).DefaultItem = True
        Catch ex As Exception
        End Try
        ppup.Show(Me, PointToClient(Control.MousePosition)) 'New Point(200, 10))

    End Sub

    Private Function GetNewMailFlag() As String
        Dim frm As New frmAddNew
        modGlobalVar.HideTabPage("tbSavedMailList", frm.TabControl1)
        frm.TabControl1.SelectedTab = frm.tbSavedMailList
        frm.Text = "Entering New Mailing List"
        frm.LoadMailFlag()
        frm.ShowDialog()
        GetNewMailFlag = frm.returnval
        frm = Nothing

    End Function

    'POPUP HANDLES SPECIAL MAIL
    Private Sub PopupMailList(ByVal obj As Object, ByVal ea As EventArgs)
        Dim i As Integer
        Dim usrChoice As String
        '  Me.txtMailFlag.Text = Me.txtMailFlag.Text + ",  " + obj.Text.Substring(0, InStr(obj.text, " ~") - 1)

        If obj.text = "Create new Mailing List" Then
            ' usrChoice = InputBox("enter new mailing list name", "CREATING NEW MAILING LIST", "")
            usrChoice = GetNewMailFlag
        Else
            usrChoice = obj.text
        End If

        If usrChoice > "" Then 'process
            'Call create new list
            i = CreateMailList(usrChoice)
            If i = 0 Then
                modGlobalVar.msg("ERROR: getting mailing id", obj.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If

            'call Associate MailList with Contact
            AssignMailList(i, usrChoice)

            'CALL REFRESH csv TEXTBOX
            RefreshMailList()
        Else 'empty entry; skip
        End If

    End Sub

    'SET LASTCHANGED DATE if TAGS or AUTHORS is edited.; 'underlying data'
    Public Sub SetLastChanged()
        mainBSrce.Current("LastChangeDate") = Now()
        mainBSrce.Current("LastChangeStaffNum") = usr
    End Sub

    'INSERT TERM in LU Table
    Private Function CreateMailList(ByVal str As String) As Integer
        Dim sql As SqlCommand
        Dim i As Integer
        If Not SCConnect() Then
            Exit Function
        End If
        'reset var
        bMailingExists = 0
        'SEE IF ALREADY EXISTS
        sql = New SqlCommand("SELECT MailListID FROM luMailList WHERE MailListName = '" & str.Replace("'", "''") & "'", sc)
        Try
            i = sql.ExecuteScalar()
        Catch ex As System.Exception
            modGlobalVar.msg("ERROR: search luMailList: '" & str & "'", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        'INSERT AS NEW
        If i > 0 Then
            bMailingExists = 1
        Else
            sql = New SqlCommand("INSERT INTO luMailList (MailListName) VALUES ('" & str.Replace("'", "''") & "'); select @@Identity", sc)
            Try
                i = sql.ExecuteScalar()
            Catch ex As System.Exception
                modGlobalVar.msg("ERROR: insert MailingList ", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If

        sc.Close()

        Return i

    End Function

    'call ADD or REMOVE this Contact from selected mailing List
    Private Sub AssignMailList(ByVal i As Integer, ByVal usrInput As String)
        modPopup.MailListContact(ThisID, IsNull(Me.txtFirstName.Text, "") & " " & IsNull(Me.txtLastName.Text, ""), i, usrInput)
    End Sub

#End Region 'special mailing lists


End Class




