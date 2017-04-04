
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Text    'stringbuilder
Imports System.IO   'getregion streamwriter'
Imports System.Threading    'sleep
'Imports Microsoft.Office.Interop.Word
Imports Microsoft.Office.Interop
Imports System.Windows.Forms


Public Class frmSrchResource
    Inherits System.Windows.Forms.Form
    'datagrid variables...
    Dim hti As DataGrid.HitTestInfo
    Dim htivw As DataGridView.HitTestInfo
    Dim dv, dvM, dvM2, dvS1, dvS2, dvS3, dvS4 As DataView   'filter for each datagrid
    Dim statusM, statusS1, statusS2 As String 'filter text for status bar
    Dim strDGM, strDGM2, strDGS1, strDGS2, strDGS3, strDGS4 As String  'header text on datagrids
    Dim source1 As New BindingSource()

    Private gridMouseDownTime As DateTime   'to catch doubleclick within datagrid
    Dim strbActiveGrid As New StringBuilder 'for doubleclick any datagrid
    Dim ThisID As Integer 'selected id
    Dim dr As SqlDataReader
    Dim cmdChoice As New SqlCommand
    Dim SrchTotal As Integer 'keep count for display 

    Dim isLoaded As Boolean = False
    Dim isSearched As Boolean = False

    Dim usrSel, usrS As String
    Dim strbFormHdr As New StringBuilder 'for header on opening main form
    Dim usrRadio As New StringBuilder 'text from radio button
    Dim bFoundMain As Boolean = False 'if no main items found, don't load secondary grids
    Dim bFoundSec As Boolean = False 'for menu item enabling
    Dim WhatField, whatFieldtxt As String
    Dim strMainDS As String = "Resource"

#Region "Initialize"

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()
        'Add any initialization after the InitializeComponent() call
        ''DO this for each grid so doubleclick will work
        Dim tbx As DataGridTextBoxColumn
        Dim tbs As DataGridTableStyle

        For Each tbs In Me.grdSecond1.TableStyles
            For Each tbx In tbs.GridColumnStyles
                tbx.TextBox.Enabled = False
                tbx.NullText = ""
            Next
        Next
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

#End Region

#Region " Windows Form Designer generated code "

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents DataGridTextBoxColumn22 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents rbKeyIndex As System.Windows.Forms.RadioButton
    Friend WithEvents miPrintPublic As System.Windows.Forms.MenuItem
    Friend WithEvents DataGridBoolColumn1 As System.Windows.Forms.DataGridBoolColumn
    Friend WithEvents grdvwMain As System.Windows.Forms.DataGridView
    Friend WithEvents lblMainGrid As System.Windows.Forms.Label
    Friend WithEvents DataGridTextBoxColumn23 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn26 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents SqlConnection1 As System.Data.SqlClient.SqlConnection
    Friend WithEvents DsGetLocation1 As InfoCtr.dsGetLocation
    Friend WithEvents miPrintFull As System.Windows.Forms.MenuItem
    Friend WithEvents chkRecommend As System.Windows.Forms.CheckBox
    Friend WithEvents rbWebsite As System.Windows.Forms.RadioButton
    Friend WithEvents miPrintFeedbackRpt As System.Windows.Forms.MenuItem
    Friend WithEvents miPrintDescriptions As System.Windows.Forms.MenuItem
    Friend WithEvents chkFeedback As System.Windows.Forms.CheckBox
    Protected WithEvents Label2 As System.Windows.Forms.Label
    Protected WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents cboField As InfoCtr.ComboBoxRelaxed
    Friend WithEvents miDefinitions As System.Windows.Forms.MenuItem
    Friend WithEvents FdbckOrgNum As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents DataGridTextBoxColumn15 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn16 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn17 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents SqlConnection2 As System.Data.SqlClient.SqlConnection
    Friend WithEvents miCRGShare As System.Windows.Forms.MenuItem
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Protected WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents TypeDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chkActive As System.Windows.Forms.CheckBox
    Friend WithEvents StatusBarPanelID As System.Windows.Forms.StatusBarPanel
    Friend WithEvents DataGridTextBoxColumn10 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents chkNewCRG As System.Windows.Forms.CheckBox
    Friend WithEvents DataGridTextBoxColumn19 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents MenuItem6 As System.Windows.Forms.MenuItem
    Friend WithEvents miExcel As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem7 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem3 As System.Windows.Forms.MenuItem
    Friend WithEvents ICCResourceIDDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ResourceNameDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents AuthorContactDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ResourceTypeDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ActiveDataGridViewCheckBoxColumn As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents OnCRGWebsite As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents NumRecommend As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents NumFeedback As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cntFunded As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Keyword As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents SqlSelectCommand3 As System.Data.SqlClient.SqlCommand
    Friend WithEvents btnHelp As System.Windows.Forms.Button
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents SqlDeleteCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents HelpProvider1 As System.Windows.Forms.HelpProvider
    Friend WithEvents StatusBar1 As System.Windows.Forms.StatusBar
    Friend WithEvents StatusBarPanel3 As System.Windows.Forms.StatusBarPanel
    Friend WithEvents StatusBarPanel4 As System.Windows.Forms.StatusBarPanel
    Friend WithEvents miHelp As System.Windows.Forms.MenuItem
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents btnNew As System.Windows.Forms.Button
    Friend WithEvents MMOrgContact As System.Windows.Forms.MainMenu
    Friend WithEvents MenuItem1 As System.Windows.Forms.MenuItem
    Friend WithEvents miCloseForm As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem2 As System.Windows.Forms.MenuItem
    Friend WithEvents miSearch As System.Windows.Forms.MenuItem
    Friend WithEvents miClearSearch As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem5 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem4 As System.Windows.Forms.MenuItem
    Friend WithEvents SqlUpdateCommand2 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlDeleteCommand2 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlInsertCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlUpdateCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlSelectCommand2 As System.Data.SqlClient.SqlCommand
    Friend WithEvents DataGridTableStyle1 As System.Windows.Forms.DataGridTableStyle
    Friend WithEvents DataGridTextBoxColumn1 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn2 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn3 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents tsFeedback As System.Windows.Forms.DataGridTableStyle
    Friend WithEvents FdbckID As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents FdbckResourceNum As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents FdbckDate As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTableStyle5 As System.Windows.Forms.DataGridTableStyle
    Friend WithEvents DataGridTextBoxColumn24 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn28 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn30 As System.Windows.Forms.DataGridTextBoxColumn
    Protected WithEvents grdSecond1 As System.Windows.Forms.DataGrid
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents chkDetail As System.Windows.Forms.CheckBox
    Friend WithEvents RadioButton2 As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButton1 As System.Windows.Forms.RadioButton
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Protected WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents pnlMainGrid As System.Windows.Forms.Panel
    Friend WithEvents DataGridTableStyle2 As System.Windows.Forms.DataGridTableStyle
    Friend WithEvents DataGridTextBoxColumn4 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn5 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn6 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn7 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn8 As System.Windows.Forms.DataGridTextBoxColumn
    Protected WithEvents oldgrdMain As System.Windows.Forms.DataGrid
    Protected WithEvents PanelOrg As System.Windows.Forms.Panel
    Friend WithEvents grpOrg As System.Windows.Forms.GroupBox
    Friend WithEvents rbName As System.Windows.Forms.RadioButton
    Protected WithEvents cboRegion As InfoCtr.ComboBoxRelaxed
    Protected WithEvents cboType As InfoCtr.ComboBoxRelaxed
    Protected WithEvents txtSearch As System.Windows.Forms.TextBox
    Protected WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents chkWild As System.Windows.Forms.CheckBox
    Friend WithEvents SqlInsertCommand2 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SecID As System.Windows.Forms.TextBox
    Friend WithEvents lblMainID As System.Windows.Forms.Label
    Friend WithEvents miGotoResource As System.Windows.Forms.MenuItem
    Friend WithEvents miGotoFeedback As System.Windows.Forms.MenuItem
    Friend WithEvents miGotoAlert As System.Windows.Forms.MenuItem
    Friend WithEvents miGotoRecommendation As System.Windows.Forms.MenuItem
    Friend WithEvents cntFeedback As System.Windows.Forms.Label
    Friend WithEvents cntAlert As System.Windows.Forms.Label
    Friend WithEvents daspSrchResources As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents SqlSelectCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents DsSrchResources1 As InfoCtr.dsSrchResources
    Friend WithEvents daspGetRecommendations As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents daspGetFeedback As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents DsSecResource1 As InfoCtr.dsSecResource
    Friend WithEvents daspGetAlert As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents SqlSelectCommand9 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlSelectCommand7 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlSelectCommand8 As System.Data.SqlClient.SqlCommand
    Friend WithEvents DataGridTextBoxColumn11 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn12 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn14 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents cntRecommendation As System.Windows.Forms.Label
    Friend WithEvents RadioButton3 As System.Windows.Forms.RadioButton
    Friend WithEvents rbID As System.Windows.Forms.RadioButton
    Friend WithEvents rbISBN As System.Windows.Forms.RadioButton
    Friend WithEvents rbIndex As System.Windows.Forms.RadioButton
    Friend WithEvents rbSeries As System.Windows.Forms.RadioButton
    Friend WithEvents rbCollection As System.Windows.Forms.RadioButton
    Friend WithEvents rbBy As System.Windows.Forms.RadioButton
    Friend WithEvents rbResourceGuide As System.Windows.Forms.RadioButton
    Friend WithEvents rbCRG As System.Windows.Forms.RadioButton
    Friend WithEvents daGetLocation As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents SqlGetLocation As System.Data.SqlClient.SqlCommand
    Friend WithEvents daGetAuthorContact As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents sqlGetAuthorContact As System.Data.SqlClient.SqlCommand
    Friend WithEvents RadioButton4 As System.Windows.Forms.RadioButton
    Friend WithEvents DataGridTableStyle4 As System.Windows.Forms.DataGridTableStyle
    Friend WithEvents DataGridTextBoxColumn13 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn18 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents cntLocation As System.Windows.Forms.Label
    Friend WithEvents FdbckOrg As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents FdbckCase As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn21 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents cboChoices As InfoCtr.ComboBoxRelaxed
    Friend WithEvents cboCRG As InfoCtr.ComboBoxRelaxed
    Friend WithEvents DataGridTextBoxColumn9 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents miGoto As System.Windows.Forms.MenuItem
    Friend WithEvents miAddResource As System.Windows.Forms.MenuItem
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSrchResource))
        Me.SqlSelectCommand3 = New System.Data.SqlClient.SqlCommand()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.SqlDeleteCommand1 = New System.Data.SqlClient.SqlCommand()
        Me.HelpProvider1 = New System.Windows.Forms.HelpProvider()
        Me.StatusBar1 = New System.Windows.Forms.StatusBar()
        Me.StatusBarPanel3 = New System.Windows.Forms.StatusBarPanel()
        Me.StatusBarPanelID = New System.Windows.Forms.StatusBarPanel()
        Me.StatusBarPanel4 = New System.Windows.Forms.StatusBarPanel()
        Me.grdSecond1 = New System.Windows.Forms.DataGrid()
        Me.DsGetLocation1 = New InfoCtr.dsGetLocation()
        Me.DataGridTableStyle1 = New System.Windows.Forms.DataGridTableStyle()
        Me.DataGridTextBoxColumn1 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn2 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn12 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn14 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn3 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn21 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn22 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn15 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn19 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.tsFeedback = New System.Windows.Forms.DataGridTableStyle()
        Me.FdbckID = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.FdbckResourceNum = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.FdbckOrgNum = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.FdbckDate = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.FdbckOrg = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.FdbckCase = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTableStyle5 = New System.Windows.Forms.DataGridTableStyle()
        Me.DataGridTextBoxColumn24 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn28 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn30 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn11 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn26 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTableStyle4 = New System.Windows.Forms.DataGridTableStyle()
        Me.DataGridTextBoxColumn23 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn13 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn18 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn10 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn16 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn17 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.oldgrdMain = New System.Windows.Forms.DataGrid()
        Me.DsSrchResources1 = New InfoCtr.dsSrchResources()
        Me.DataGridTableStyle2 = New System.Windows.Forms.DataGridTableStyle()
        Me.DataGridTextBoxColumn4 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn5 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn6 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn7 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn8 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn9 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridBoolColumn1 = New System.Windows.Forms.DataGridBoolColumn()
        Me.cboRegion = New InfoCtr.ComboBoxRelaxed()
        Me.miHelp = New System.Windows.Forms.MenuItem()
        Me.miDefinitions = New System.Windows.Forms.MenuItem()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.cntRecommendation = New System.Windows.Forms.Label()
        Me.cntLocation = New System.Windows.Forms.Label()
        Me.RadioButton4 = New System.Windows.Forms.RadioButton()
        Me.cntFeedback = New System.Windows.Forms.Label()
        Me.cntAlert = New System.Windows.Forms.Label()
        Me.RadioButton3 = New System.Windows.Forms.RadioButton()
        Me.RadioButton2 = New System.Windows.Forms.RadioButton()
        Me.RadioButton1 = New System.Windows.Forms.RadioButton()
        Me.chkDetail = New System.Windows.Forms.CheckBox()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.lblMainID = New System.Windows.Forms.Label()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.chkWild = New System.Windows.Forms.CheckBox()
        Me.rbIndex = New System.Windows.Forms.RadioButton()
        Me.rbResourceGuide = New System.Windows.Forms.RadioButton()
        Me.rbCRG = New System.Windows.Forms.RadioButton()
        Me.rbKeyIndex = New System.Windows.Forms.RadioButton()
        Me.rbName = New System.Windows.Forms.RadioButton()
        Me.btnHelp = New System.Windows.Forms.Button()
        Me.btnNew = New System.Windows.Forms.Button()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.cboType = New InfoCtr.ComboBoxRelaxed()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.chkRecommend = New System.Windows.Forms.CheckBox()
        Me.miAddResource = New System.Windows.Forms.MenuItem()
        Me.MMOrgContact = New System.Windows.Forms.MainMenu(Me.components)
        Me.MenuItem1 = New System.Windows.Forms.MenuItem()
        Me.miCloseForm = New System.Windows.Forms.MenuItem()
        Me.MenuItem2 = New System.Windows.Forms.MenuItem()
        Me.miSearch = New System.Windows.Forms.MenuItem()
        Me.miClearSearch = New System.Windows.Forms.MenuItem()
        Me.MenuItem5 = New System.Windows.Forms.MenuItem()
        Me.MenuItem4 = New System.Windows.Forms.MenuItem()
        Me.MenuItem7 = New System.Windows.Forms.MenuItem()
        Me.miPrintFull = New System.Windows.Forms.MenuItem()
        Me.miPrintDescriptions = New System.Windows.Forms.MenuItem()
        Me.miPrintFeedbackRpt = New System.Windows.Forms.MenuItem()
        Me.miPrintPublic = New System.Windows.Forms.MenuItem()
        Me.MenuItem3 = New System.Windows.Forms.MenuItem()
        Me.miCRGShare = New System.Windows.Forms.MenuItem()
        Me.MenuItem6 = New System.Windows.Forms.MenuItem()
        Me.miExcel = New System.Windows.Forms.MenuItem()
        Me.miGoto = New System.Windows.Forms.MenuItem()
        Me.miGotoResource = New System.Windows.Forms.MenuItem()
        Me.miGotoRecommendation = New System.Windows.Forms.MenuItem()
        Me.miGotoFeedback = New System.Windows.Forms.MenuItem()
        Me.miGotoAlert = New System.Windows.Forms.MenuItem()
        Me.SqlUpdateCommand2 = New System.Data.SqlClient.SqlCommand()
        Me.SqlDeleteCommand2 = New System.Data.SqlClient.SqlCommand()
        Me.SqlInsertCommand1 = New System.Data.SqlClient.SqlCommand()
        Me.SqlUpdateCommand1 = New System.Data.SqlClient.SqlCommand()
        Me.SqlSelectCommand2 = New System.Data.SqlClient.SqlCommand()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.PanelOrg = New System.Windows.Forms.Panel()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.chkNewCRG = New System.Windows.Forms.CheckBox()
        Me.chkActive = New System.Windows.Forms.CheckBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.chkFeedback = New System.Windows.Forms.CheckBox()
        Me.cboField = New InfoCtr.ComboBoxRelaxed()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.grpOrg = New System.Windows.Forms.GroupBox()
        Me.rbWebsite = New System.Windows.Forms.RadioButton()
        Me.rbID = New System.Windows.Forms.RadioButton()
        Me.rbISBN = New System.Windows.Forms.RadioButton()
        Me.rbSeries = New System.Windows.Forms.RadioButton()
        Me.rbCollection = New System.Windows.Forms.RadioButton()
        Me.rbBy = New System.Windows.Forms.RadioButton()
        Me.cboCRG = New InfoCtr.ComboBoxRelaxed()
        Me.cboChoices = New InfoCtr.ComboBoxRelaxed()
        Me.pnlMainGrid = New System.Windows.Forms.Panel()
        Me.grdvwMain = New System.Windows.Forms.DataGridView()
        Me.ICCResourceIDDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ResourceNameDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.AuthorContactDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ResourceTypeDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ActiveDataGridViewCheckBoxColumn = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.OnCRGWebsite = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.NumRecommend = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.NumFeedback = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cntFunded = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Keyword = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SecID = New System.Windows.Forms.TextBox()
        Me.lblMainGrid = New System.Windows.Forms.Label()
        Me.SqlInsertCommand2 = New System.Data.SqlClient.SqlCommand()
        Me.daspSrchResources = New System.Data.SqlClient.SqlDataAdapter()
        Me.SqlSelectCommand1 = New System.Data.SqlClient.SqlCommand()
        Me.SqlConnection1 = New System.Data.SqlClient.SqlConnection()
        Me.daspGetRecommendations = New System.Data.SqlClient.SqlDataAdapter()
        Me.SqlSelectCommand7 = New System.Data.SqlClient.SqlCommand()
        Me.SqlConnection2 = New System.Data.SqlClient.SqlConnection()
        Me.daspGetFeedback = New System.Data.SqlClient.SqlDataAdapter()
        Me.SqlSelectCommand8 = New System.Data.SqlClient.SqlCommand()
        Me.daspGetAlert = New System.Data.SqlClient.SqlDataAdapter()
        Me.SqlSelectCommand9 = New System.Data.SqlClient.SqlCommand()
        Me.daGetLocation = New System.Data.SqlClient.SqlDataAdapter()
        Me.SqlGetLocation = New System.Data.SqlClient.SqlCommand()
        Me.daGetAuthorContact = New System.Data.SqlClient.SqlDataAdapter()
        Me.sqlGetAuthorContact = New System.Data.SqlClient.SqlCommand()
        Me.DsSecResource1 = New InfoCtr.dsSecResource()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        CType(Me.StatusBarPanel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.StatusBarPanelID, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.StatusBarPanel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdSecond1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsGetLocation1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.oldgrdMain, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsSrchResources1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.PanelOrg.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.grpOrg.SuspendLayout()
        CType(Me.grdvwMain, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsSecResource1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlDark
        Me.Label5.Location = New System.Drawing.Point(772, 5)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(42, 20)
        Me.Label5.TabIndex = 228
        Me.Label5.Text = "Item #"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'StatusBar1
        '
        Me.StatusBar1.Location = New System.Drawing.Point(0, 619)
        Me.StatusBar1.Name = "StatusBar1"
        Me.StatusBar1.Panels.AddRange(New System.Windows.Forms.StatusBarPanel() {Me.StatusBarPanel3, Me.StatusBarPanelID, Me.StatusBarPanel4})
        Me.HelpProvider1.SetShowHelp(Me.StatusBar1, False)
        Me.StatusBar1.ShowPanels = True
        Me.StatusBar1.Size = New System.Drawing.Size(1148, 22)
        Me.StatusBar1.TabIndex = 223
        '
        'StatusBarPanel3
        '
        Me.StatusBarPanel3.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents
        Me.StatusBarPanel3.BorderStyle = System.Windows.Forms.StatusBarPanelBorderStyle.None
        Me.StatusBarPanel3.MinWidth = 200
        Me.StatusBarPanel3.Name = "StatusBarPanel3"
        Me.StatusBarPanel3.Text = "Ready"
        Me.StatusBarPanel3.Width = 200
        '
        'StatusBarPanelID
        '
        Me.StatusBarPanelID.Alignment = System.Windows.Forms.HorizontalAlignment.Center
        Me.StatusBarPanelID.MinWidth = 200
        Me.StatusBarPanelID.Name = "StatusBarPanelID"
        Me.StatusBarPanelID.Text = "Resource ID"
        Me.StatusBarPanelID.Width = 200
        '
        'StatusBarPanel4
        '
        Me.StatusBarPanel4.Alignment = System.Windows.Forms.HorizontalAlignment.Right
        Me.StatusBarPanel4.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring
        Me.StatusBarPanel4.BorderStyle = System.Windows.Forms.StatusBarPanelBorderStyle.None
        Me.StatusBarPanel4.Name = "StatusBarPanel4"
        Me.StatusBarPanel4.Text = "Use this window to find a Resource.  Click Show Related Items to show or hide rel" & _
    "ated information in the lower grid."
        Me.StatusBarPanel4.Width = 731
        '
        'grdSecond1
        '
        Me.grdSecond1.AlternatingBackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.grdSecond1.CaptionBackColor = System.Drawing.SystemColors.ControlDark
        Me.grdSecond1.CaptionText = "Related Information Grid"
        Me.grdSecond1.DataMember = "GetResourceLocation"
        Me.grdSecond1.DataSource = Me.DsGetLocation1
        Me.grdSecond1.GridLineStyle = System.Windows.Forms.DataGridLineStyle.None
        Me.grdSecond1.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.HelpProvider1.SetHelpString(Me.grdSecond1, "")
        Me.grdSecond1.Location = New System.Drawing.Point(9, 28)
        Me.grdSecond1.Name = "grdSecond1"
        Me.grdSecond1.ParentRowsVisible = False
        Me.grdSecond1.ReadOnly = True
        Me.grdSecond1.RowHeaderWidth = 15
        Me.HelpProvider1.SetShowHelp(Me.grdSecond1, True)
        Me.grdSecond1.Size = New System.Drawing.Size(883, 206)
        Me.grdSecond1.TabIndex = 203
        Me.grdSecond1.TableStyles.AddRange(New System.Windows.Forms.DataGridTableStyle() {Me.DataGridTableStyle1, Me.tsFeedback, Me.DataGridTableStyle5, Me.DataGridTableStyle4})
        Me.grdSecond1.TabStop = False
        Me.ToolTip1.SetToolTip(Me.grdSecond1, "Double-click to go to detail window.")
        '
        'DsGetLocation1
        '
        Me.DsGetLocation1.DataSetName = "dsGetLocation"
        Me.DsGetLocation1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'DataGridTableStyle1
        '
        Me.DataGridTableStyle1.AlternatingBackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.DataGridTableStyle1.DataGrid = Me.grdSecond1
        Me.DataGridTableStyle1.GridColumnStyles.AddRange(New System.Windows.Forms.DataGridColumnStyle() {Me.DataGridTextBoxColumn1, Me.DataGridTextBoxColumn2, Me.DataGridTextBoxColumn12, Me.DataGridTextBoxColumn14, Me.DataGridTextBoxColumn3, Me.DataGridTextBoxColumn21, Me.DataGridTextBoxColumn22, Me.DataGridTextBoxColumn15, Me.DataGridTextBoxColumn19})
        Me.DataGridTableStyle1.GridLineStyle = System.Windows.Forms.DataGridLineStyle.None
        Me.DataGridTableStyle1.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.DataGridTableStyle1.MappingName = "getResRecommendation"
        Me.DataGridTableStyle1.RowHeaderWidth = 15
        Me.DataGridTableStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        '
        'DataGridTextBoxColumn1
        '
        Me.DataGridTextBoxColumn1.Format = ""
        Me.DataGridTextBoxColumn1.FormatInfo = Nothing
        Me.DataGridTextBoxColumn1.MappingName = "RecommendID"
        Me.DataGridTextBoxColumn1.Width = 0
        '
        'DataGridTextBoxColumn2
        '
        Me.DataGridTextBoxColumn2.Format = ""
        Me.DataGridTextBoxColumn2.FormatInfo = Nothing
        Me.DataGridTextBoxColumn2.HeaderText = "Date"
        Me.DataGridTextBoxColumn2.MappingName = "RecommendDate"
        Me.DataGridTextBoxColumn2.Width = 75
        '
        'DataGridTextBoxColumn12
        '
        Me.DataGridTextBoxColumn12.Format = ""
        Me.DataGridTextBoxColumn12.FormatInfo = Nothing
        Me.DataGridTextBoxColumn12.HeaderText = "Org."
        Me.DataGridTextBoxColumn12.MappingName = "OrgName"
        Me.DataGridTextBoxColumn12.Width = 200
        '
        'DataGridTextBoxColumn14
        '
        Me.DataGridTextBoxColumn14.Format = ""
        Me.DataGridTextBoxColumn14.FormatInfo = Nothing
        Me.DataGridTextBoxColumn14.HeaderText = "Case"
        Me.DataGridTextBoxColumn14.MappingName = "CaseName"
        Me.DataGridTextBoxColumn14.Width = 150
        '
        'DataGridTextBoxColumn3
        '
        Me.DataGridTextBoxColumn3.Format = ""
        Me.DataGridTextBoxColumn3.FormatInfo = Nothing
        Me.DataGridTextBoxColumn3.HeaderText = "Used"
        Me.DataGridTextBoxColumn3.MappingName = "Used"
        Me.DataGridTextBoxColumn3.Width = 50
        '
        'DataGridTextBoxColumn21
        '
        Me.DataGridTextBoxColumn21.Format = ""
        Me.DataGridTextBoxColumn21.FormatInfo = Nothing
        Me.DataGridTextBoxColumn21.HeaderText = "Grant"
        Me.DataGridTextBoxColumn21.MappingName = "GrantNum"
        Me.DataGridTextBoxColumn21.Width = 80
        '
        'DataGridTextBoxColumn22
        '
        Me.DataGridTextBoxColumn22.Format = ""
        Me.DataGridTextBoxColumn22.FormatInfo = Nothing
        Me.DataGridTextBoxColumn22.HeaderText = "OrgNum"
        Me.DataGridTextBoxColumn22.MappingName = "OrgNum"
        Me.DataGridTextBoxColumn22.Width = 0
        '
        'DataGridTextBoxColumn15
        '
        Me.DataGridTextBoxColumn15.Format = ""
        Me.DataGridTextBoxColumn15.FormatInfo = Nothing
        Me.DataGridTextBoxColumn15.HeaderText = "Center Staff"
        Me.DataGridTextBoxColumn15.MappingName = "WhoRecommended"
        Me.DataGridTextBoxColumn15.ReadOnly = True
        Me.DataGridTextBoxColumn15.Width = 150
        '
        'DataGridTextBoxColumn19
        '
        Me.DataGridTextBoxColumn19.Format = ""
        Me.DataGridTextBoxColumn19.FormatInfo = Nothing
        Me.DataGridTextBoxColumn19.HeaderText = "#Feedback"
        Me.DataGridTextBoxColumn19.MappingName = "cntFeedback"
        Me.DataGridTextBoxColumn19.Width = 50
        '
        'tsFeedback
        '
        Me.tsFeedback.AlternatingBackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.tsFeedback.DataGrid = Me.grdSecond1
        Me.tsFeedback.GridColumnStyles.AddRange(New System.Windows.Forms.DataGridColumnStyle() {Me.FdbckID, Me.FdbckResourceNum, Me.FdbckOrgNum, Me.FdbckDate, Me.FdbckOrg, Me.FdbckCase})
        Me.tsFeedback.GridLineStyle = System.Windows.Forms.DataGridLineStyle.None
        Me.tsFeedback.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.tsFeedback.MappingName = "GetResFeedback"
        Me.tsFeedback.RowHeaderWidth = 15
        Me.tsFeedback.SelectionBackColor = System.Drawing.SystemColors.Highlight
        '
        'FdbckID
        '
        Me.FdbckID.Format = ""
        Me.FdbckID.FormatInfo = Nothing
        Me.FdbckID.HeaderText = "FeedbackID"
        Me.FdbckID.MappingName = "FeedbackID"
        Me.FdbckID.Width = 0
        '
        'FdbckResourceNum
        '
        Me.FdbckResourceNum.Format = ""
        Me.FdbckResourceNum.FormatInfo = Nothing
        Me.FdbckResourceNum.HeaderText = "ResourceNum"
        Me.FdbckResourceNum.MappingName = "ResourceNum"
        Me.FdbckResourceNum.NullText = ""
        Me.FdbckResourceNum.Width = 0
        '
        'FdbckOrgNum
        '
        Me.FdbckOrgNum.Format = ""
        Me.FdbckOrgNum.FormatInfo = Nothing
        Me.FdbckOrgNum.HeaderText = "OrgNum"
        Me.FdbckOrgNum.MappingName = "OrgNum"
        Me.FdbckOrgNum.Width = 0
        '
        'FdbckDate
        '
        Me.FdbckDate.Format = ""
        Me.FdbckDate.FormatInfo = Nothing
        Me.FdbckDate.HeaderText = "Date"
        Me.FdbckDate.MappingName = "FeedbackDate"
        Me.FdbckDate.NullText = ""
        Me.FdbckDate.Width = 75
        '
        'FdbckOrg
        '
        Me.FdbckOrg.Format = ""
        Me.FdbckOrg.FormatInfo = Nothing
        Me.FdbckOrg.HeaderText = "Org"
        Me.FdbckOrg.MappingName = "OrgName"
        Me.FdbckOrg.Width = 250
        '
        'FdbckCase
        '
        Me.FdbckCase.Format = ""
        Me.FdbckCase.FormatInfo = Nothing
        Me.FdbckCase.HeaderText = "Case"
        Me.FdbckCase.MappingName = "CaseName"
        Me.FdbckCase.Width = 175
        '
        'DataGridTableStyle5
        '
        Me.DataGridTableStyle5.AlternatingBackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.DataGridTableStyle5.DataGrid = Me.grdSecond1
        Me.DataGridTableStyle5.GridColumnStyles.AddRange(New System.Windows.Forms.DataGridColumnStyle() {Me.DataGridTextBoxColumn24, Me.DataGridTextBoxColumn28, Me.DataGridTextBoxColumn30, Me.DataGridTextBoxColumn11, Me.DataGridTextBoxColumn26})
        Me.DataGridTableStyle5.GridLineStyle = System.Windows.Forms.DataGridLineStyle.None
        Me.DataGridTableStyle5.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.DataGridTableStyle5.MappingName = "tblResourceWarning"
        Me.DataGridTableStyle5.RowHeaderWidth = 15
        Me.DataGridTableStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight
        '
        'DataGridTextBoxColumn24
        '
        Me.DataGridTextBoxColumn24.Format = ""
        Me.DataGridTextBoxColumn24.FormatInfo = Nothing
        Me.DataGridTextBoxColumn24.HeaderText = "WarningID"
        Me.DataGridTextBoxColumn24.MappingName = "WarningID"
        Me.DataGridTextBoxColumn24.Width = 0
        '
        'DataGridTextBoxColumn28
        '
        Me.DataGridTextBoxColumn28.Format = ""
        Me.DataGridTextBoxColumn28.FormatInfo = Nothing
        Me.DataGridTextBoxColumn28.HeaderText = "ResourceNum"
        Me.DataGridTextBoxColumn28.MappingName = "ResourceNum"
        Me.DataGridTextBoxColumn28.Width = 0
        '
        'DataGridTextBoxColumn30
        '
        Me.DataGridTextBoxColumn30.Alignment = System.Windows.Forms.HorizontalAlignment.Right
        Me.DataGridTextBoxColumn30.Format = "d"
        Me.DataGridTextBoxColumn30.FormatInfo = Nothing
        Me.DataGridTextBoxColumn30.HeaderText = "Date    ."
        Me.DataGridTextBoxColumn30.MappingName = "WarningDate"
        Me.DataGridTextBoxColumn30.Width = 80
        '
        'DataGridTextBoxColumn11
        '
        Me.DataGridTextBoxColumn11.Format = ""
        Me.DataGridTextBoxColumn11.FormatInfo = Nothing
        Me.DataGridTextBoxColumn11.HeaderText = "Staff"
        Me.DataGridTextBoxColumn11.MappingName = "StaffName"
        Me.DataGridTextBoxColumn11.Width = 175
        '
        'DataGridTextBoxColumn26
        '
        Me.DataGridTextBoxColumn26.Format = ""
        Me.DataGridTextBoxColumn26.FormatInfo = Nothing
        Me.DataGridTextBoxColumn26.HeaderText = "Warning"
        Me.DataGridTextBoxColumn26.MappingName = "Warning"
        Me.DataGridTextBoxColumn26.Width = 250
        '
        'DataGridTableStyle4
        '
        Me.DataGridTableStyle4.DataGrid = Me.grdSecond1
        Me.DataGridTableStyle4.GridColumnStyles.AddRange(New System.Windows.Forms.DataGridColumnStyle() {Me.DataGridTextBoxColumn23, Me.DataGridTextBoxColumn13, Me.DataGridTextBoxColumn18, Me.DataGridTextBoxColumn10, Me.DataGridTextBoxColumn16, Me.DataGridTextBoxColumn17})
        Me.DataGridTableStyle4.GridLineStyle = System.Windows.Forms.DataGridLineStyle.None
        Me.DataGridTableStyle4.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.DataGridTableStyle4.MappingName = "GetResourceLocation"
        Me.DataGridTableStyle4.RowHeaderWidth = 15
        '
        'DataGridTextBoxColumn23
        '
        Me.DataGridTextBoxColumn23.Format = ""
        Me.DataGridTextBoxColumn23.FormatInfo = Nothing
        Me.DataGridTextBoxColumn23.HeaderText = "ResourceLocationID"
        Me.DataGridTextBoxColumn23.MappingName = "ResourceLocationID"
        Me.DataGridTextBoxColumn23.Width = 0
        '
        'DataGridTextBoxColumn13
        '
        Me.DataGridTextBoxColumn13.Format = ""
        Me.DataGridTextBoxColumn13.FormatInfo = Nothing
        Me.DataGridTextBoxColumn13.HeaderText = "Region"
        Me.DataGridTextBoxColumn13.MappingName = "SatelliteRegion"
        Me.DataGridTextBoxColumn13.Width = 75
        '
        'DataGridTextBoxColumn18
        '
        Me.DataGridTextBoxColumn18.Format = ""
        Me.DataGridTextBoxColumn18.FormatInfo = Nothing
        Me.DataGridTextBoxColumn18.HeaderText = "Location"
        Me.DataGridTextBoxColumn18.MappingName = "Location"
        Me.DataGridTextBoxColumn18.Width = 275
        '
        'DataGridTextBoxColumn10
        '
        Me.DataGridTextBoxColumn10.Format = ""
        Me.DataGridTextBoxColumn10.FormatInfo = Nothing
        Me.DataGridTextBoxColumn10.HeaderText = "Edition"
        Me.DataGridTextBoxColumn10.MappingName = "Edition"
        Me.DataGridTextBoxColumn10.Width = 125
        '
        'DataGridTextBoxColumn16
        '
        Me.DataGridTextBoxColumn16.Format = ""
        Me.DataGridTextBoxColumn16.FormatInfo = Nothing
        Me.DataGridTextBoxColumn16.HeaderText = "Date"
        Me.DataGridTextBoxColumn16.MappingName = "LocationEdit"
        Me.DataGridTextBoxColumn16.Width = 75
        '
        'DataGridTextBoxColumn17
        '
        Me.DataGridTextBoxColumn17.Format = ""
        Me.DataGridTextBoxColumn17.FormatInfo = Nothing
        Me.DataGridTextBoxColumn17.HeaderText = "Location Staff"
        Me.DataGridTextBoxColumn17.MappingName = "LocationStaff"
        Me.DataGridTextBoxColumn17.Width = 300
        '
        'oldgrdMain
        '
        Me.oldgrdMain.BackgroundColor = System.Drawing.SystemColors.Control
        Me.oldgrdMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.oldgrdMain.CaptionBackColor = System.Drawing.SystemColors.InactiveCaption
        Me.oldgrdMain.CaptionText = "Search Result Grid"
        Me.oldgrdMain.DataMember = "SrchResource"
        Me.oldgrdMain.DataSource = Me.DsSrchResources1
        Me.oldgrdMain.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.oldgrdMain.Location = New System.Drawing.Point(3, 210)
        Me.oldgrdMain.Name = "oldgrdMain"
        Me.oldgrdMain.ParentRowsBackColor = System.Drawing.SystemColors.Window
        Me.oldgrdMain.ParentRowsVisible = False
        Me.oldgrdMain.RowHeaderWidth = 15
        Me.oldgrdMain.SelectionBackColor = System.Drawing.SystemColors.Highlight
        Me.oldgrdMain.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        Me.HelpProvider1.SetShowHelp(Me.oldgrdMain, True)
        Me.oldgrdMain.Size = New System.Drawing.Size(723, 77)
        Me.oldgrdMain.TabIndex = 202
        Me.oldgrdMain.TableStyles.AddRange(New System.Windows.Forms.DataGridTableStyle() {Me.DataGridTableStyle2})
        Me.oldgrdMain.TabStop = False
        Me.oldgrdMain.Tag = "Double-click for detail.  Right-click to filter; right-click border to clear filt" & _
    "er."
        Me.ToolTip1.SetToolTip(Me.oldgrdMain, "Click in grid, then press F1 for grid help")
        Me.oldgrdMain.Visible = False
        '
        'DsSrchResources1
        '
        Me.DsSrchResources1.DataSetName = "dsSrchResources"
        Me.DsSrchResources1.Locale = New System.Globalization.CultureInfo("en-US")
        Me.DsSrchResources1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'DataGridTableStyle2
        '
        Me.DataGridTableStyle2.AlternatingBackColor = System.Drawing.Color.FromArgb(CType(CType(204, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.DataGridTableStyle2.DataGrid = Me.oldgrdMain
        Me.DataGridTableStyle2.ForeColor = System.Drawing.Color.DarkGreen
        Me.DataGridTableStyle2.GridColumnStyles.AddRange(New System.Windows.Forms.DataGridColumnStyle() {Me.DataGridTextBoxColumn4, Me.DataGridTextBoxColumn5, Me.DataGridTextBoxColumn6, Me.DataGridTextBoxColumn7, Me.DataGridTextBoxColumn8, Me.DataGridTextBoxColumn9, Me.DataGridBoolColumn1})
        Me.DataGridTableStyle2.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.DataGridTableStyle2.MappingName = "SrchResource"
        Me.DataGridTableStyle2.RowHeaderWidth = 15
        Me.DataGridTableStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        '
        'DataGridTextBoxColumn4
        '
        Me.DataGridTextBoxColumn4.Format = ""
        Me.DataGridTextBoxColumn4.FormatInfo = Nothing
        Me.DataGridTextBoxColumn4.HeaderText = "ID"
        Me.DataGridTextBoxColumn4.MappingName = "ICCResourceID"
        Me.DataGridTextBoxColumn4.ReadOnly = True
        Me.DataGridTextBoxColumn4.Width = 0
        '
        'DataGridTextBoxColumn5
        '
        Me.DataGridTextBoxColumn5.Format = ""
        Me.DataGridTextBoxColumn5.FormatInfo = Nothing
        Me.DataGridTextBoxColumn5.HeaderText = "Name"
        Me.DataGridTextBoxColumn5.MappingName = "ResourceName"
        Me.DataGridTextBoxColumn5.ReadOnly = True
        Me.DataGridTextBoxColumn5.Width = 250
        '
        'DataGridTextBoxColumn6
        '
        Me.DataGridTextBoxColumn6.Format = ""
        Me.DataGridTextBoxColumn6.FormatInfo = Nothing
        Me.DataGridTextBoxColumn6.HeaderText = "Author/Contact"
        Me.DataGridTextBoxColumn6.MappingName = "AuthorContact"
        Me.DataGridTextBoxColumn6.ReadOnly = True
        Me.DataGridTextBoxColumn6.Width = 125
        '
        'DataGridTextBoxColumn7
        '
        Me.DataGridTextBoxColumn7.Format = ""
        Me.DataGridTextBoxColumn7.FormatInfo = Nothing
        Me.DataGridTextBoxColumn7.HeaderText = "ResourceType"
        Me.DataGridTextBoxColumn7.MappingName = "ResourceType"
        Me.DataGridTextBoxColumn7.ReadOnly = True
        Me.DataGridTextBoxColumn7.Width = 75
        '
        'DataGridTextBoxColumn8
        '
        Me.DataGridTextBoxColumn8.Format = ""
        Me.DataGridTextBoxColumn8.FormatInfo = Nothing
        Me.DataGridTextBoxColumn8.HeaderText = "Active"
        Me.DataGridTextBoxColumn8.MappingName = "Active"
        Me.DataGridTextBoxColumn8.ReadOnly = True
        Me.DataGridTextBoxColumn8.Width = 45
        '
        'DataGridTextBoxColumn9
        '
        Me.DataGridTextBoxColumn9.Format = ""
        Me.DataGridTextBoxColumn9.FormatInfo = Nothing
        Me.DataGridTextBoxColumn9.HeaderText = "Found"
        Me.DataGridTextBoxColumn9.MappingName = "Found"
        Me.DataGridTextBoxColumn9.ReadOnly = True
        Me.DataGridTextBoxColumn9.Width = 150
        '
        'DataGridBoolColumn1
        '
        Me.DataGridBoolColumn1.HeaderText = "Select"
        Me.DataGridBoolColumn1.MappingName = "Select"
        Me.DataGridBoolColumn1.Width = 50
        '
        'cboRegion
        '
        Me.cboRegion.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboRegion.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboRegion.ItemHeight = 13
        Me.cboRegion.Location = New System.Drawing.Point(100, 124)
        Me.cboRegion.Name = "cboRegion"
        Me.cboRegion.RestrictContentToListItems = True
        Me.HelpProvider1.SetShowHelp(Me.cboRegion, True)
        Me.cboRegion.Size = New System.Drawing.Size(97, 21)
        Me.cboRegion.TabIndex = 4
        Me.cboRegion.Tag = "Region"
        Me.ToolTip1.SetToolTip(Me.cboRegion, "Region of Resource Location")
        '
        'miHelp
        '
        Me.miHelp.Index = 4
        Me.miHelp.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.miDefinitions})
        Me.miHelp.Text = "Help"
        '
        'miDefinitions
        '
        Me.miDefinitions.Index = 0
        Me.miDefinitions.Text = "Resource Type Definitions"
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.SystemColors.InactiveBorder
        Me.GroupBox1.Controls.Add(Me.cntRecommendation)
        Me.GroupBox1.Controls.Add(Me.cntLocation)
        Me.GroupBox1.Controls.Add(Me.RadioButton4)
        Me.GroupBox1.Controls.Add(Me.cntFeedback)
        Me.GroupBox1.Controls.Add(Me.cntAlert)
        Me.GroupBox1.Controls.Add(Me.RadioButton3)
        Me.GroupBox1.Controls.Add(Me.RadioButton2)
        Me.GroupBox1.Controls.Add(Me.RadioButton1)
        Me.GroupBox1.Location = New System.Drawing.Point(3, 4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(172, 95)
        Me.GroupBox1.TabIndex = 208
        Me.GroupBox1.TabStop = False
        Me.ToolTip1.SetToolTip(Me.GroupBox1, "Uncheck this box to speed up the search.")
        '
        'cntRecommendation
        '
        Me.cntRecommendation.Location = New System.Drawing.Point(128, 34)
        Me.cntRecommendation.Name = "cntRecommendation"
        Me.cntRecommendation.Size = New System.Drawing.Size(35, 14)
        Me.cntRecommendation.TabIndex = 11
        Me.cntRecommendation.Text = "0"
        Me.cntRecommendation.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cntLocation
        '
        Me.cntLocation.Location = New System.Drawing.Point(128, 14)
        Me.cntLocation.Name = "cntLocation"
        Me.cntLocation.Size = New System.Drawing.Size(35, 16)
        Me.cntLocation.TabIndex = 13
        Me.cntLocation.Text = "0"
        Me.cntLocation.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'RadioButton4
        '
        Me.RadioButton4.Checked = True
        Me.RadioButton4.Location = New System.Drawing.Point(9, 10)
        Me.RadioButton4.Name = "RadioButton4"
        Me.RadioButton4.Size = New System.Drawing.Size(117, 20)
        Me.RadioButton4.TabIndex = 12
        Me.RadioButton4.TabStop = True
        Me.RadioButton4.Text = "Locations"
        '
        'cntFeedback
        '
        Me.cntFeedback.Location = New System.Drawing.Point(128, 52)
        Me.cntFeedback.Name = "cntFeedback"
        Me.cntFeedback.Size = New System.Drawing.Size(35, 14)
        Me.cntFeedback.TabIndex = 10
        Me.cntFeedback.Text = "0"
        Me.cntFeedback.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cntAlert
        '
        Me.cntAlert.Location = New System.Drawing.Point(128, 70)
        Me.cntAlert.Name = "cntAlert"
        Me.cntAlert.Size = New System.Drawing.Size(35, 14)
        Me.cntAlert.TabIndex = 9
        Me.cntAlert.Text = "0"
        Me.cntAlert.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'RadioButton3
        '
        Me.RadioButton3.Location = New System.Drawing.Point(9, 68)
        Me.RadioButton3.Name = "RadioButton3"
        Me.RadioButton3.Size = New System.Drawing.Size(109, 18)
        Me.RadioButton3.TabIndex = 8
        Me.RadioButton3.Text = "Alerts"
        '
        'RadioButton2
        '
        Me.RadioButton2.Location = New System.Drawing.Point(9, 50)
        Me.RadioButton2.Name = "RadioButton2"
        Me.RadioButton2.Size = New System.Drawing.Size(109, 18)
        Me.RadioButton2.TabIndex = 7
        Me.RadioButton2.Text = "Feedback"
        '
        'RadioButton1
        '
        Me.RadioButton1.Location = New System.Drawing.Point(9, 30)
        Me.RadioButton1.Name = "RadioButton1"
        Me.RadioButton1.Size = New System.Drawing.Size(118, 20)
        Me.RadioButton1.TabIndex = 6
        Me.RadioButton1.Text = "Recommendations"
        '
        'chkDetail
        '
        Me.chkDetail.Checked = True
        Me.chkDetail.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkDetail.Location = New System.Drawing.Point(9, 7)
        Me.chkDetail.Name = "chkDetail"
        Me.chkDetail.Size = New System.Drawing.Size(16, 16)
        Me.chkDetail.TabIndex = 2
        Me.ToolTip1.SetToolTip(Me.chkDetail, "Uncheck this box to speed up the search.")
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.SystemColors.InactiveBorder
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Controls.Add(Me.GroupBox1)
        Me.Panel2.Controls.Add(Me.lblMainID)
        Me.Panel2.Location = New System.Drawing.Point(14, 358)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(183, 109)
        Me.Panel2.TabIndex = 222
        Me.ToolTip1.SetToolTip(Me.Panel2, "Change the related information topic shown in the lower grid.")
        '
        'lblMainID
        '
        Me.lblMainID.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMainID.ForeColor = System.Drawing.SystemColors.ControlDark
        Me.lblMainID.Location = New System.Drawing.Point(846, 2)
        Me.lblMainID.Name = "lblMainID"
        Me.lblMainID.Size = New System.Drawing.Size(35, 14)
        Me.lblMainID.TabIndex = 213
        Me.lblMainID.Text = "Case #"
        Me.lblMainID.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtSearch
        '
        Me.txtSearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.txtSearch.Location = New System.Drawing.Point(10, 7)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(187, 20)
        Me.txtSearch.TabIndex = 0
        Me.txtSearch.Text = "enter search text"
        Me.ToolTip1.SetToolTip(Me.txtSearch, "enter search text. Wildcard: *")
        '
        'chkWild
        '
        Me.chkWild.Checked = True
        Me.chkWild.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkWild.Location = New System.Drawing.Point(23, 142)
        Me.chkWild.Name = "chkWild"
        Me.chkWild.Size = New System.Drawing.Size(96, 16)
        Me.chkWild.TabIndex = 8
        Me.chkWild.Text = "Wildcards On"
        Me.ToolTip1.SetToolTip(Me.chkWild, "Places a wildcard before and after your search text. ")
        '
        'rbIndex
        '
        Me.rbIndex.Location = New System.Drawing.Point(20, 49)
        Me.rbIndex.Name = "rbIndex"
        Me.rbIndex.Size = New System.Drawing.Size(88, 18)
        Me.rbIndex.TabIndex = 3
        Me.rbIndex.Tag = "IndexList"
        Me.rbIndex.Text = "Index Terms"
        Me.ToolTip1.SetToolTip(Me.rbIndex, "Searches index fields.  Select from dropdown box.")
        Me.rbIndex.Visible = False
        '
        'rbResourceGuide
        '
        Me.rbResourceGuide.Location = New System.Drawing.Point(30, 38)
        Me.rbResourceGuide.Name = "rbResourceGuide"
        Me.rbResourceGuide.Size = New System.Drawing.Size(125, 18)
        Me.rbResourceGuide.TabIndex = 4
        Me.rbResourceGuide.Tag = "ResourceGuide"
        Me.rbResourceGuide.Text = "on Resource Guide"
        Me.ToolTip1.SetToolTip(Me.rbResourceGuide, "Finds resources listed on a EdEvent Resource Guide.  Select from dropdown.")
        Me.rbResourceGuide.Visible = False
        '
        'rbCRG
        '
        Me.rbCRG.Location = New System.Drawing.Point(47, 73)
        Me.rbCRG.Name = "rbCRG"
        Me.rbCRG.Size = New System.Drawing.Size(108, 18)
        Me.rbCRG.TabIndex = 2
        Me.rbCRG.Tag = "CRG"
        Me.rbCRG.Text = "CRG Category"
        Me.ToolTip1.SetToolTip(Me.rbCRG, "Searches keywords.  Select term from dropdown box.")
        Me.rbCRG.Visible = False
        '
        'rbKeyIndex
        '
        Me.rbKeyIndex.Location = New System.Drawing.Point(20, 58)
        Me.rbKeyIndex.Name = "rbKeyIndex"
        Me.rbKeyIndex.Size = New System.Drawing.Size(145, 18)
        Me.rbKeyIndex.TabIndex = 10
        Me.rbKeyIndex.Tag = "KeyIndex"
        Me.rbKeyIndex.Text = "all Keywords and Indices"
        Me.ToolTip1.SetToolTip(Me.rbKeyIndex, "Searches all 4 keywords and all 4 index terms.  Enter your own word.")
        Me.rbKeyIndex.Visible = False
        '
        'rbName
        '
        Me.rbName.Checked = True
        Me.rbName.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbName.Location = New System.Drawing.Point(53, 14)
        Me.rbName.Name = "rbName"
        Me.rbName.Size = New System.Drawing.Size(83, 18)
        Me.rbName.TabIndex = 0
        Me.rbName.TabStop = True
        Me.rbName.Tag = "Name"
        Me.rbName.Text = "Name"
        Me.ToolTip1.SetToolTip(Me.rbName, "Name of Resource, Author, Editor, Contact etc.")
        Me.rbName.Visible = False
        '
        'btnHelp
        '
        Me.btnHelp.BackColor = System.Drawing.SystemColors.Control
        Me.btnHelp.Font = New System.Drawing.Font("Microsoft Sans Serif", 22.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnHelp.ForeColor = System.Drawing.SystemColors.Control
        Me.btnHelp.Image = Global.InfoCtr.My.Resources.Resources.btnHelp
        Me.btnHelp.Location = New System.Drawing.Point(1106, 12)
        Me.btnHelp.Name = "btnHelp"
        Me.btnHelp.Size = New System.Drawing.Size(25, 25)
        Me.btnHelp.TabIndex = 227
        Me.btnHelp.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.ToolTip1.SetToolTip(Me.btnHelp, "Help: How to use this Search page.")
        Me.btnHelp.UseVisualStyleBackColor = False
        '
        'btnNew
        '
        Me.btnNew.BackColor = System.Drawing.Color.PaleGoldenrod
        Me.btnNew.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnNew.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnNew.Image = Global.InfoCtr.My.Resources.Resources.btnAddNew
        Me.btnNew.ImageAlign = System.Drawing.ContentAlignment.TopRight
        Me.btnNew.Location = New System.Drawing.Point(1017, 12)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(67, 35)
        Me.btnNew.TabIndex = 226
        Me.btnNew.Text = "New Resource"
        Me.btnNew.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        Me.ToolTip1.SetToolTip(Me.btnNew, "Add New Resource ")
        Me.btnNew.UseVisualStyleBackColor = False
        '
        'btnSearch
        '
        Me.btnSearch.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.btnSearch.Location = New System.Drawing.Point(63, 89)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(75, 25)
        Me.btnSearch.TabIndex = 1
        Me.btnSearch.Text = "Search"
        Me.ToolTip1.SetToolTip(Me.btnSearch, "Begin Search")
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'cboType
        '
        Me.cboType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboType.Location = New System.Drawing.Point(100, 98)
        Me.cboType.MaxDropDownItems = 20
        Me.cboType.Name = "cboType"
        Me.cboType.RestrictContentToListItems = True
        Me.cboType.Size = New System.Drawing.Size(94, 21)
        Me.cboType.TabIndex = 3
        Me.cboType.Tag = "ResourceType"
        Me.ToolTip1.SetToolTip(Me.cboType, "Type of Resource")
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.SystemColors.InactiveBorder
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Maroon
        Me.Label2.Location = New System.Drawing.Point(17, 348)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(138, 21)
        Me.Label2.TabIndex = 222
        Me.Label2.Text = "Related items in Lower Grid"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Location = New System.Drawing.Point(6, 4)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(124, 20)
        Me.Label4.TabIndex = 5
        Me.Label4.Text = "     Show Related Items"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'chkRecommend
        '
        Me.chkRecommend.Location = New System.Drawing.Point(20, 28)
        Me.chkRecommend.Name = "chkRecommend"
        Me.chkRecommend.Size = New System.Drawing.Size(164, 16)
        Me.chkRecommend.TabIndex = 193
        Me.chkRecommend.Text = "Have Been Recommended"
        '
        'miAddResource
        '
        Me.miAddResource.Index = 0
        Me.miAddResource.Text = "New Resource"
        '
        'MMOrgContact
        '
        Me.MMOrgContact.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem1, Me.MenuItem2, Me.MenuItem4, Me.miGoto, Me.miHelp})
        '
        'MenuItem1
        '
        Me.MenuItem1.Index = 0
        Me.MenuItem1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.miAddResource, Me.miCloseForm})
        Me.MenuItem1.Text = "File"
        '
        'miCloseForm
        '
        Me.miCloseForm.Index = 1
        Me.miCloseForm.Text = "Close Window"
        '
        'MenuItem2
        '
        Me.MenuItem2.Index = 1
        Me.MenuItem2.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.miSearch, Me.miClearSearch, Me.MenuItem5})
        Me.MenuItem2.Text = "Search"
        '
        'miSearch
        '
        Me.miSearch.Index = 0
        Me.miSearch.Text = "Begin Search"
        '
        'miClearSearch
        '
        Me.miClearSearch.Index = 1
        Me.miClearSearch.Text = "Clear Criteria"
        '
        'MenuItem5
        '
        Me.MenuItem5.Enabled = False
        Me.MenuItem5.Index = 2
        Me.MenuItem5.Text = "Include Inactives"
        '
        'MenuItem4
        '
        Me.MenuItem4.Index = 2
        Me.MenuItem4.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem7, Me.miPrintFull, Me.miPrintDescriptions, Me.miPrintFeedbackRpt, Me.miPrintPublic, Me.MenuItem3, Me.miCRGShare, Me.MenuItem6, Me.miExcel})
        Me.MenuItem4.Text = "Reports"
        '
        'MenuItem7
        '
        Me.MenuItem7.Enabled = False
        Me.MenuItem7.Index = 0
        Me.MenuItem7.Text = "---Word Reports ---"
        '
        'miPrintFull
        '
        Me.miPrintFull.Index = 1
        Me.miPrintFull.Tag = "All"
        Me.miPrintFull.Text = "All Fields"
        '
        'miPrintDescriptions
        '
        Me.miPrintDescriptions.Index = 2
        Me.miPrintDescriptions.Tag = "Descriptions"
        Me.miPrintDescriptions.Text = "Basic Information and Descriptions"
        '
        'miPrintFeedbackRpt
        '
        Me.miPrintFeedbackRpt.Index = 3
        Me.miPrintFeedbackRpt.Tag = "Feedback"
        Me.miPrintFeedbackRpt.Text = "Feedback"
        '
        'miPrintPublic
        '
        Me.miPrintPublic.Index = 4
        Me.miPrintPublic.Tag = "Public"
        Me.miPrintPublic.Text = "Public Information"
        '
        'MenuItem3
        '
        Me.MenuItem3.Enabled = False
        Me.MenuItem3.Index = 5
        Me.MenuItem3.Text = "----------"
        '
        'miCRGShare
        '
        Me.miCRGShare.Index = 6
        Me.miCRGShare.Text = "CRG Share Spreadsheet"
        '
        'MenuItem6
        '
        Me.MenuItem6.Enabled = False
        Me.MenuItem6.Index = 7
        Me.MenuItem6.Text = "----------"
        '
        'miExcel
        '
        Me.miExcel.Index = 8
        Me.miExcel.Text = "Export grid fields to Excel"
        '
        'miGoto
        '
        Me.miGoto.Index = 3
        Me.miGoto.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.miGotoResource, Me.miGotoRecommendation, Me.miGotoFeedback, Me.miGotoAlert})
        Me.miGoto.Text = "Go to"
        '
        'miGotoResource
        '
        Me.miGotoResource.Index = 0
        Me.miGotoResource.Text = "Resource"
        '
        'miGotoRecommendation
        '
        Me.miGotoRecommendation.Enabled = False
        Me.miGotoRecommendation.Index = 1
        Me.miGotoRecommendation.MdiList = True
        Me.miGotoRecommendation.Text = "Recommendation"
        '
        'miGotoFeedback
        '
        Me.miGotoFeedback.Enabled = False
        Me.miGotoFeedback.Index = 2
        Me.miGotoFeedback.Text = "Feedback"
        '
        'miGotoAlert
        '
        Me.miGotoAlert.Enabled = False
        Me.miGotoAlert.Index = 3
        Me.miGotoAlert.Text = "Alert"
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Maroon
        Me.Label3.Location = New System.Drawing.Point(28, 20)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(89, 17)
        Me.Label3.TabIndex = 220
        Me.Label3.Text = "Search Criteria"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'PanelOrg
        '
        Me.PanelOrg.BackColor = System.Drawing.SystemColors.InactiveBorder
        Me.PanelOrg.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.PanelOrg.Controls.Add(Me.Label10)
        Me.PanelOrg.Controls.Add(Me.Label2)
        Me.PanelOrg.Controls.Add(Me.Panel1)
        Me.PanelOrg.Controls.Add(Me.cboField)
        Me.PanelOrg.Controls.Add(Me.Panel2)
        Me.PanelOrg.Controls.Add(Me.chkWild)
        Me.PanelOrg.Controls.Add(Me.Label7)
        Me.PanelOrg.Controls.Add(Me.grpOrg)
        Me.PanelOrg.Controls.Add(Me.btnSearch)
        Me.PanelOrg.Controls.Add(Me.cboCRG)
        Me.PanelOrg.Controls.Add(Me.cboChoices)
        Me.PanelOrg.Controls.Add(Me.txtSearch)
        Me.PanelOrg.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PanelOrg.Location = New System.Drawing.Point(12, 56)
        Me.PanelOrg.Name = "PanelOrg"
        Me.PanelOrg.Size = New System.Drawing.Size(214, 520)
        Me.PanelOrg.TabIndex = 1
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.SystemColors.InactiveBorder
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.Maroon
        Me.Label10.Location = New System.Drawing.Point(11, 171)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(106, 23)
        Me.Label10.TabIndex = 234
        Me.Label10.Text = "Filter Your Results"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.chkNewCRG)
        Me.Panel1.Controls.Add(Me.chkActive)
        Me.Panel1.Controls.Add(Me.Label9)
        Me.Panel1.Controls.Add(Me.Label8)
        Me.Panel1.Controls.Add(Me.chkFeedback)
        Me.Panel1.Controls.Add(Me.chkRecommend)
        Me.Panel1.Controls.Add(Me.cboRegion)
        Me.Panel1.Controls.Add(Me.cboType)
        Me.Panel1.Location = New System.Drawing.Point(3, 185)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(201, 149)
        Me.Panel1.TabIndex = 235
        '
        'chkNewCRG
        '
        Me.chkNewCRG.Location = New System.Drawing.Point(20, 68)
        Me.chkNewCRG.Name = "chkNewCRG"
        Me.chkNewCRG.Size = New System.Drawing.Size(164, 16)
        Me.chkNewCRG.TabIndex = 237
        Me.chkNewCRG.Text = """New CRG"""
        '
        'chkActive
        '
        Me.chkActive.AutoSize = True
        Me.chkActive.Checked = True
        Me.chkActive.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkActive.Location = New System.Drawing.Point(20, 8)
        Me.chkActive.Name = "chkActive"
        Me.chkActive.Size = New System.Drawing.Size(56, 17)
        Me.chkActive.TabIndex = 236
        Me.chkActive.Text = "Active"
        Me.chkActive.UseVisualStyleBackColor = True
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(4, 124)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(90, 13)
        Me.Label9.TabIndex = 233
        Me.Label9.Tag = "(note: for books this would be each office that has a copy.)"
        Me.Label9.Text = "Resource Region"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(4, 100)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(92, 13)
        Me.Label8.TabIndex = 232
        Me.Label8.Text = "Type of Resource"
        '
        'chkFeedback
        '
        Me.chkFeedback.Location = New System.Drawing.Point(20, 48)
        Me.chkFeedback.Name = "chkFeedback"
        Me.chkFeedback.Size = New System.Drawing.Size(164, 16)
        Me.chkFeedback.TabIndex = 194
        Me.chkFeedback.Text = "Have Feedback"
        '
        'cboField
        '
        Me.cboField.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboField.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboField.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.cboField.FormattingEnabled = True
        Me.cboField.Items.AddRange(New Object() {"Name, Author/Contact", "-------------------", "all Keywords/Index Terms, Name", "CRG Category", "Index Terms", "ISBN", "Referral Source", "Resource Guide", "Series", "Website", "--------------------", "City", "Satellite Region", "--------------------", "Resource ID #"})
        Me.cboField.Location = New System.Drawing.Point(7, 59)
        Me.cboField.Name = "cboField"
        Me.cboField.RestrictContentToListItems = True
        Me.cboField.Size = New System.Drawing.Size(198, 21)
        Me.cboField.TabIndex = 195
        Me.cboField.Tag = "Field to Search"
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.SystemColors.InactiveBorder
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.Maroon
        Me.Label7.Location = New System.Drawing.Point(6, 39)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(138, 17)
        Me.Label7.TabIndex = 222
        Me.Label7.Text = "Field to Search"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'grpOrg
        '
        Me.grpOrg.Controls.Add(Me.rbWebsite)
        Me.grpOrg.Controls.Add(Me.rbKeyIndex)
        Me.grpOrg.Controls.Add(Me.rbID)
        Me.grpOrg.Controls.Add(Me.rbISBN)
        Me.grpOrg.Controls.Add(Me.rbIndex)
        Me.grpOrg.Controls.Add(Me.rbSeries)
        Me.grpOrg.Controls.Add(Me.rbCollection)
        Me.grpOrg.Controls.Add(Me.rbBy)
        Me.grpOrg.Controls.Add(Me.rbResourceGuide)
        Me.grpOrg.Controls.Add(Me.rbCRG)
        Me.grpOrg.Controls.Add(Me.rbName)
        Me.grpOrg.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.grpOrg.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpOrg.Location = New System.Drawing.Point(17, 473)
        Me.grpOrg.Name = "grpOrg"
        Me.grpOrg.Size = New System.Drawing.Size(183, 38)
        Me.grpOrg.TabIndex = 191
        Me.grpOrg.TabStop = False
        Me.grpOrg.Tag = ""
        Me.grpOrg.Visible = False
        '
        'rbWebsite
        '
        Me.rbWebsite.Location = New System.Drawing.Point(33, 62)
        Me.rbWebsite.Name = "rbWebsite"
        Me.rbWebsite.Size = New System.Drawing.Size(122, 18)
        Me.rbWebsite.TabIndex = 11
        Me.rbWebsite.Tag = "Website"
        Me.rbWebsite.Text = "Website"
        Me.rbWebsite.Visible = False
        '
        'rbID
        '
        Me.rbID.Location = New System.Drawing.Point(53, 82)
        Me.rbID.Name = "rbID"
        Me.rbID.Size = New System.Drawing.Size(122, 18)
        Me.rbID.TabIndex = 9
        Me.rbID.Tag = "ID"
        Me.rbID.Text = "Resource Number"
        Me.rbID.Visible = False
        '
        'rbISBN
        '
        Me.rbISBN.Location = New System.Drawing.Point(6, 82)
        Me.rbISBN.Name = "rbISBN"
        Me.rbISBN.Size = New System.Drawing.Size(86, 18)
        Me.rbISBN.TabIndex = 8
        Me.rbISBN.Tag = "ISBN"
        Me.rbISBN.Text = "ISBN"
        Me.rbISBN.Visible = False
        '
        'rbSeries
        '
        Me.rbSeries.Location = New System.Drawing.Point(17, 73)
        Me.rbSeries.Name = "rbSeries"
        Me.rbSeries.Size = New System.Drawing.Size(86, 18)
        Me.rbSeries.TabIndex = 7
        Me.rbSeries.Tag = "Series"
        Me.rbSeries.Text = "Series"
        Me.rbSeries.Visible = False
        '
        'rbCollection
        '
        Me.rbCollection.Location = New System.Drawing.Point(6, 61)
        Me.rbCollection.Name = "rbCollection"
        Me.rbCollection.Size = New System.Drawing.Size(88, 18)
        Me.rbCollection.TabIndex = 6
        Me.rbCollection.Tag = "Collection"
        Me.rbCollection.Text = "Collection"
        Me.rbCollection.Visible = False
        '
        'rbBy
        '
        Me.rbBy.Location = New System.Drawing.Point(19, 49)
        Me.rbBy.Name = "rbBy"
        Me.rbBy.Size = New System.Drawing.Size(119, 18)
        Me.rbBy.TabIndex = 5
        Me.rbBy.Tag = "ReferralSource"
        Me.rbBy.Text = "Recommended By"
        Me.rbBy.Visible = False
        '
        'cboCRG
        '
        Me.cboCRG.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboCRG.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboCRG.BackColor = System.Drawing.SystemColors.Window
        Me.cboCRG.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCRG.DropDownWidth = 500
        Me.cboCRG.Location = New System.Drawing.Point(7, 9)
        Me.cboCRG.MaxDropDownItems = 20
        Me.cboCRG.Name = "cboCRG"
        Me.cboCRG.RestrictContentToListItems = True
        Me.cboCRG.Size = New System.Drawing.Size(193, 21)
        Me.cboCRG.TabIndex = 231
        Me.cboCRG.Tag = "Dropdown"
        Me.cboCRG.Visible = False
        '
        'cboChoices
        '
        Me.cboChoices.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboChoices.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboChoices.DropDownWidth = 500
        Me.cboChoices.Location = New System.Drawing.Point(5, 7)
        Me.cboChoices.MaxDropDownItems = 20
        Me.cboChoices.Name = "cboChoices"
        Me.cboChoices.RestrictContentToListItems = True
        Me.cboChoices.Size = New System.Drawing.Size(198, 21)
        Me.cboChoices.TabIndex = 192
        Me.cboChoices.Tag = "Dropdown"
        Me.cboChoices.Visible = False
        '
        'pnlMainGrid
        '
        Me.pnlMainGrid.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlMainGrid.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pnlMainGrid.Location = New System.Drawing.Point(1106, 56)
        Me.pnlMainGrid.Name = "pnlMainGrid"
        Me.pnlMainGrid.Size = New System.Drawing.Size(31, 520)
        Me.pnlMainGrid.TabIndex = 219
        '
        'grdvwMain
        '
        Me.grdvwMain.AllowUserToAddRows = False
        Me.grdvwMain.AllowUserToDeleteRows = False
        Me.grdvwMain.AllowUserToOrderColumns = True
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(204, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.grdvwMain.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.grdvwMain.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdvwMain.AutoGenerateColumns = False
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdvwMain.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.grdvwMain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdvwMain.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ICCResourceIDDataGridViewTextBoxColumn, Me.ResourceNameDataGridViewTextBoxColumn, Me.AuthorContactDataGridViewTextBoxColumn, Me.ResourceTypeDataGridViewTextBoxColumn, Me.ActiveDataGridViewCheckBoxColumn, Me.OnCRGWebsite, Me.NumRecommend, Me.NumFeedback, Me.cntFunded, Me.Keyword})
        Me.grdvwMain.DataMember = "SrchResource"
        Me.grdvwMain.DataSource = Me.DsSrchResources1
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.Color.DarkGreen
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.grdvwMain.DefaultCellStyle = DataGridViewCellStyle3
        Me.grdvwMain.GridColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.grdvwMain.Location = New System.Drawing.Point(9, 25)
        Me.grdvwMain.Name = "grdvwMain"
        Me.grdvwMain.ReadOnly = True
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdvwMain.RowHeadersDefaultCellStyle = DataGridViewCellStyle4
        Me.grdvwMain.RowHeadersWidth = 20
        Me.grdvwMain.RowTemplate.Height = 20
        Me.grdvwMain.Size = New System.Drawing.Size(883, 321)
        Me.grdvwMain.TabIndex = 232
        '
        'ICCResourceIDDataGridViewTextBoxColumn
        '
        Me.ICCResourceIDDataGridViewTextBoxColumn.DataPropertyName = "ICCResourceID"
        Me.ICCResourceIDDataGridViewTextBoxColumn.HeaderText = "ICCResourceID"
        Me.ICCResourceIDDataGridViewTextBoxColumn.Name = "ICCResourceIDDataGridViewTextBoxColumn"
        Me.ICCResourceIDDataGridViewTextBoxColumn.ReadOnly = True
        Me.ICCResourceIDDataGridViewTextBoxColumn.Visible = False
        Me.ICCResourceIDDataGridViewTextBoxColumn.Width = 5
        '
        'ResourceNameDataGridViewTextBoxColumn
        '
        Me.ResourceNameDataGridViewTextBoxColumn.DataPropertyName = "ResourceName"
        Me.ResourceNameDataGridViewTextBoxColumn.HeaderText = "ResourceName"
        Me.ResourceNameDataGridViewTextBoxColumn.Name = "ResourceNameDataGridViewTextBoxColumn"
        Me.ResourceNameDataGridViewTextBoxColumn.ReadOnly = True
        Me.ResourceNameDataGridViewTextBoxColumn.Width = 300
        '
        'AuthorContactDataGridViewTextBoxColumn
        '
        Me.AuthorContactDataGridViewTextBoxColumn.DataPropertyName = "AuthorContact"
        Me.AuthorContactDataGridViewTextBoxColumn.HeaderText = "AuthorContact"
        Me.AuthorContactDataGridViewTextBoxColumn.Name = "AuthorContactDataGridViewTextBoxColumn"
        Me.AuthorContactDataGridViewTextBoxColumn.ReadOnly = True
        Me.AuthorContactDataGridViewTextBoxColumn.Width = 160
        '
        'ResourceTypeDataGridViewTextBoxColumn
        '
        Me.ResourceTypeDataGridViewTextBoxColumn.DataPropertyName = "ResourceType"
        Me.ResourceTypeDataGridViewTextBoxColumn.HeaderText = "ResourceType"
        Me.ResourceTypeDataGridViewTextBoxColumn.Name = "ResourceTypeDataGridViewTextBoxColumn"
        Me.ResourceTypeDataGridViewTextBoxColumn.ReadOnly = True
        '
        'ActiveDataGridViewCheckBoxColumn
        '
        Me.ActiveDataGridViewCheckBoxColumn.DataPropertyName = "Active"
        Me.ActiveDataGridViewCheckBoxColumn.HeaderText = "Active"
        Me.ActiveDataGridViewCheckBoxColumn.Name = "ActiveDataGridViewCheckBoxColumn"
        Me.ActiveDataGridViewCheckBoxColumn.ReadOnly = True
        Me.ActiveDataGridViewCheckBoxColumn.Width = 50
        '
        'OnCRGWebsite
        '
        Me.OnCRGWebsite.DataPropertyName = "OnCRGWebsite"
        Me.OnCRGWebsite.HeaderText = "RG/NewCRG"
        Me.OnCRGWebsite.Name = "OnCRGWebsite"
        Me.OnCRGWebsite.ReadOnly = True
        Me.OnCRGWebsite.Width = 75
        '
        'NumRecommend
        '
        Me.NumRecommend.DataPropertyName = "cntRecbyCenter"
        Me.NumRecommend.HeaderText = "#CtrRecom"
        Me.NumRecommend.Name = "NumRecommend"
        Me.NumRecommend.ReadOnly = True
        Me.NumRecommend.Width = 50
        '
        'NumFeedback
        '
        Me.NumFeedback.DataPropertyName = "CntFeedback"
        Me.NumFeedback.HeaderText = "#Feedback"
        Me.NumFeedback.Name = "NumFeedback"
        Me.NumFeedback.ReadOnly = True
        Me.NumFeedback.Width = 50
        '
        'cntFunded
        '
        Me.cntFunded.DataPropertyName = "cntFunded"
        Me.cntFunded.HeaderText = "#Funded"
        Me.cntFunded.Name = "cntFunded"
        Me.cntFunded.ReadOnly = True
        Me.cntFunded.Width = 50
        '
        'Keyword
        '
        Me.Keyword.DataPropertyName = "Keyword"
        Me.Keyword.HeaderText = "Keyword"
        Me.Keyword.Name = "Keyword"
        Me.Keyword.ReadOnly = True
        Me.Keyword.Width = 25
        '
        'SecID
        '
        Me.SecID.AcceptsReturn = True
        Me.SecID.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SecID.BackColor = System.Drawing.SystemColors.Control
        Me.SecID.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.SecID.Enabled = False
        Me.SecID.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SecID.ForeColor = System.Drawing.SystemColors.GrayText
        Me.SecID.Location = New System.Drawing.Point(827, 8)
        Me.SecID.Name = "SecID"
        Me.SecID.Size = New System.Drawing.Size(59, 14)
        Me.SecID.TabIndex = 224
        Me.SecID.Text = "ID#"
        Me.SecID.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblMainGrid
        '
        Me.lblMainGrid.AutoSize = True
        Me.lblMainGrid.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.lblMainGrid.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMainGrid.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblMainGrid.Location = New System.Drawing.Point(11, 5)
        Me.lblMainGrid.MinimumSize = New System.Drawing.Size(875, 19)
        Me.lblMainGrid.Name = "lblMainGrid"
        Me.lblMainGrid.Size = New System.Drawing.Size(875, 19)
        Me.lblMainGrid.TabIndex = 232
        Me.lblMainGrid.Text = "Search Results"
        Me.lblMainGrid.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'daspSrchResources
        '
        Me.daspSrchResources.SelectCommand = Me.SqlSelectCommand1
        Me.daspSrchResources.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "SrchResource", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("ICCResourceID", "ICCResourceID"), New System.Data.Common.DataColumnMapping("ResourceName", "ResourceName"), New System.Data.Common.DataColumnMapping("AuthorContact", "AuthorContact"), New System.Data.Common.DataColumnMapping("ResourceType", "ResourceType"), New System.Data.Common.DataColumnMapping("Subtype", "Subtype"), New System.Data.Common.DataColumnMapping("Active", "Active"), New System.Data.Common.DataColumnMapping("Keyword", "Keyword"), New System.Data.Common.DataColumnMapping("OnCRGWebsite", "OnCRGWebsite"), New System.Data.Common.DataColumnMapping("NumRecommend", "NumRecommend"), New System.Data.Common.DataColumnMapping("NumFeedback", "NumFeedback")})})
        '
        'SqlSelectCommand1
        '
        Me.SqlSelectCommand1.CommandText = "dbo.SrchResource"
        Me.SqlSelectCommand1.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlSelectCommand1.Connection = Me.SqlConnection1
        Me.SqlSelectCommand1.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, False, CType(0, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing), New System.Data.SqlClient.SqlParameter("@IDFld", System.Data.SqlDbType.VarChar, 30), New System.Data.SqlClient.SqlParameter("@IDstr", System.Data.SqlDbType.VarChar, 255), New System.Data.SqlClient.SqlParameter("@IDVal", System.Data.SqlDbType.Int, 4), New System.Data.SqlClient.SqlParameter("@Region", System.Data.SqlDbType.VarChar, 30), New System.Data.SqlClient.SqlParameter("@Type", System.Data.SqlDbType.VarChar, 30), New System.Data.SqlClient.SqlParameter("@Fld", System.Data.SqlDbType.VarChar, 100), New System.Data.SqlClient.SqlParameter("@Recommend", System.Data.SqlDbType.Bit), New System.Data.SqlClient.SqlParameter("@Active", System.Data.SqlDbType.Bit), New System.Data.SqlClient.SqlParameter("@Feedback", System.Data.SqlDbType.Bit), New System.Data.SqlClient.SqlParameter("@NewCRG", System.Data.SqlDbType.Bit)})
        '
        'SqlConnection1
        '
        Me.SqlConnection1.ConnectionString = "Data Source=SOLOMON;Initial Catalog=InfoCtr_be;Integrated Security=True"
        Me.SqlConnection1.FireInfoMessageEventOnUserErrors = False
        '
        'daspGetRecommendations
        '
        Me.daspGetRecommendations.SelectCommand = Me.SqlSelectCommand7
        Me.daspGetRecommendations.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "GetResRecommendation", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("RecommendID", "RecommendID"), New System.Data.Common.DataColumnMapping("ResourceNum", "ResourceNum"), New System.Data.Common.DataColumnMapping("CaseNum", "CaseNum"), New System.Data.Common.DataColumnMapping("OrgNum", "OrgNum"), New System.Data.Common.DataColumnMapping("ConversNum", "ConversNum"), New System.Data.Common.DataColumnMapping("GrantNum", "GrantNum"), New System.Data.Common.DataColumnMapping("ResourceName", "ResourceName"), New System.Data.Common.DataColumnMapping("ResourceType", "ResourceType"), New System.Data.Common.DataColumnMapping("Active", "Active"), New System.Data.Common.DataColumnMapping("RecommendDate", "RecommendDate"), New System.Data.Common.DataColumnMapping("Used", "Used"), New System.Data.Common.DataColumnMapping("RecommendStaffNum", "RecommendStaffNum"), New System.Data.Common.DataColumnMapping("OrgName", "OrgName"), New System.Data.Common.DataColumnMapping("CaseName", "CaseName"), New System.Data.Common.DataColumnMapping("WhoRecommended", "WhoRecommended"), New System.Data.Common.DataColumnMapping("cntFeedback", "cntFeedback")})})
        '
        'SqlSelectCommand7
        '
        Me.SqlSelectCommand7.CommandText = "dbo.GetResRecommendation"
        Me.SqlSelectCommand7.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlSelectCommand7.Connection = Me.SqlConnection2
        Me.SqlSelectCommand7.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, False, CType(0, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing), New System.Data.SqlClient.SqlParameter("@IDFld", System.Data.SqlDbType.VarChar, 30), New System.Data.SqlClient.SqlParameter("@IDVal", System.Data.SqlDbType.Int, 4), New System.Data.SqlClient.SqlParameter("@IDStr", System.Data.SqlDbType.VarChar, 30)})
        '
        'SqlConnection2
        '
        Me.SqlConnection2.ConnectionString = "Data Source=SOLOMON2008\SOLOMON2008;Initial Catalog=InfoCtr_be;Integrated Securit" & _
    "y=True"
        Me.SqlConnection2.FireInfoMessageEventOnUserErrors = False
        '
        'daspGetFeedback
        '
        Me.daspGetFeedback.SelectCommand = Me.SqlSelectCommand8
        Me.daspGetFeedback.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "GetResFeedback", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("FeedbackID", "FeedbackID"), New System.Data.Common.DataColumnMapping("ResourceNum", "ResourceNum"), New System.Data.Common.DataColumnMapping("RecommendNum", "RecommendNum"), New System.Data.Common.DataColumnMapping("OrgNum", "OrgNum"), New System.Data.Common.DataColumnMapping("CaseNum", "CaseNum"), New System.Data.Common.DataColumnMapping("FeedbackDate", "FeedbackDate"), New System.Data.Common.DataColumnMapping("FeedbackBy", "FeedbackBy"), New System.Data.Common.DataColumnMapping("WouldYouRecommend", "WouldYouRecommend"), New System.Data.Common.DataColumnMapping("ApprovalScale", "ApprovalScale")})})
        '
        'SqlSelectCommand8
        '
        Me.SqlSelectCommand8.CommandText = "[GetResFeedback]"
        Me.SqlSelectCommand8.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlSelectCommand8.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, False, CType(0, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing), New System.Data.SqlClient.SqlParameter("@IDFld", System.Data.SqlDbType.VarChar, 30), New System.Data.SqlClient.SqlParameter("@IDVal", System.Data.SqlDbType.Int, 4)})
        '
        'daspGetAlert
        '
        Me.daspGetAlert.SelectCommand = Me.SqlSelectCommand9
        Me.daspGetAlert.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "tblResourceWarning", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("WarningID", "WarningID"), New System.Data.Common.DataColumnMapping("ResourceNum", "ResourceNum"), New System.Data.Common.DataColumnMapping("WarningDescription", "Warning"), New System.Data.Common.DataColumnMapping("StaffName", "StaffName"), New System.Data.Common.DataColumnMapping("WarningDate", "WarningDate")})})
        '
        'SqlSelectCommand9
        '
        Me.SqlSelectCommand9.CommandText = "[GetResAlert]"
        Me.SqlSelectCommand9.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlSelectCommand9.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, False, CType(0, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing), New System.Data.SqlClient.SqlParameter("@IDVal", System.Data.SqlDbType.Int, 4), New System.Data.SqlClient.SqlParameter("@IDFld", System.Data.SqlDbType.NVarChar, 30)})
        '
        'daGetLocation
        '
        Me.daGetLocation.SelectCommand = Me.SqlGetLocation
        Me.daGetLocation.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "GetResourceLocation", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("ResourceLocationID", "ResourceLocationID"), New System.Data.Common.DataColumnMapping("ResourceNum", "ResourceNum"), New System.Data.Common.DataColumnMapping("SatelliteRegion", "SatelliteRegion"), New System.Data.Common.DataColumnMapping("Location", "Location"), New System.Data.Common.DataColumnMapping("Keyword1", "Keyword1"), New System.Data.Common.DataColumnMapping("LocationStaff", "LocationStaff"), New System.Data.Common.DataColumnMapping("LocationEdit", "LocationEdit"), New System.Data.Common.DataColumnMapping("edition", "edition"), New System.Data.Common.DataColumnMapping("isbn", "isbn")})})
        '
        'SqlGetLocation
        '
        Me.SqlGetLocation.CommandText = "dbo.GetResLocation"
        Me.SqlGetLocation.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlGetLocation.Connection = Me.SqlConnection2
        Me.SqlGetLocation.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, False, CType(0, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing), New System.Data.SqlClient.SqlParameter("@IDVal", System.Data.SqlDbType.Int, 4)})
        '
        'daGetAuthorContact
        '
        Me.daGetAuthorContact.SelectCommand = Me.sqlGetAuthorContact
        Me.daGetAuthorContact.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "tblResourceExtra", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("ResourceNum", "ResourceNum"), New System.Data.Common.DataColumnMapping("LastName", "LastName"), New System.Data.Common.DataColumnMapping("FirstName", "FirstName"), New System.Data.Common.DataColumnMapping("AuthorEditor", "AuthorEditor")})})
        '
        'sqlGetAuthorContact
        '
        Me.sqlGetAuthorContact.CommandText = "SELECT ResourceNum, LastName, FirstName, AuthorEditor FROM tblResourceExtra WHERE" & _
    " (ResourceNum = @ID)"
        Me.sqlGetAuthorContact.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@ID", System.Data.SqlDbType.Int, 4, "ResourceNum")})
        '
        'DsSecResource1
        '
        Me.DsSecResource1.DataSetName = "dsSecResource"
        Me.DsSecResource1.Locale = New System.Globalization.CultureInfo("en-US")
        Me.DsSecResource1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SplitContainer1.Location = New System.Drawing.Point(232, 56)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblMainGrid)
        Me.SplitContainer1.Panel1.Controls.Add(Me.grdvwMain)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.grdSecond1)
        Me.SplitContainer1.Panel2.Controls.Add(Me.SecID)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Label5)
        Me.SplitContainer1.Panel2.Controls.Add(Me.chkDetail)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Label4)
        Me.SplitContainer1.Size = New System.Drawing.Size(904, 556)
        Me.SplitContainer1.SplitterDistance = 349
        Me.SplitContainer1.TabIndex = 228
        '
        'frmSrchResource
        '
        Me.ClientSize = New System.Drawing.Size(1148, 641)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.PanelOrg)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.btnHelp)
        Me.Controls.Add(Me.StatusBar1)
        Me.Controls.Add(Me.btnNew)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Menu = Me.MMOrgContact
        Me.Name = "frmSrchResource"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "FIND RESOURCE"
        CType(Me.StatusBarPanel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.StatusBarPanelID, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.StatusBarPanel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdSecond1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsGetLocation1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.oldgrdMain, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsSrchResources1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.PanelOrg.ResumeLayout(False)
        Me.PanelOrg.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.grpOrg.ResumeLayout(False)
        CType(Me.grdvwMain, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsSecResource1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.Panel2.PerformLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    'TODO ENHANCEMENT allow to search by wildcards in DropDowns for CRG, Index, Series, etc.

#Region "Load"
    'LOAD
    Private Sub frmSrchOrg_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles MyBase.Load
        Dim i As Byte
        'SET CAPTION STRINGS
        strDGM = "RESOURCES"
        strDGS1 = "RECOMMENDATIONS"
        strDGS2 = "FEEDBACK"
        strDGS3 = "ALERTS"
        strDGS4 = "LOCATIONS"

        usrRadio.Append("Locations")

        Me.daspSrchResources.SelectCommand.Connection = sc
        Me.daspGetRecommendations.SelectCommand.Connection = sc
        Me.daspGetFeedback.SelectCommand.Connection = sc
        Me.daspGetAlert.SelectCommand.Connection = sc

        'SET ComboBox DEFAULTS
        Me.cboRegion.Items.Add("All")
        For i = 0 To colRegionlu.Count - 2    '0 means include All Regions, - 2 not not in region

            Me.cboRegion.Items.Add(colRegionlu(i + 1))
            'End If
        Next
        Me.cboRegion.SelectedIndex = 0 'cboRegion.FindString(usrRegion)

        'RESOURCE TYPE
        Me.StatusBarPanel3.Text = "Loading Type combo"
        cboType.Items.Add("All Types")
        For i = 1 To colResourceType.Count
            cboType.Items.Add(colResourceType(i))
        Next
        Me.cboType.SelectedIndex = 0

        'SET dataView VARIABLE FOR FILTERING dataGRIDS
        dvM = New DataView(Me.DsSrchResources1.Tables(0))
        dv = dvM
        dvS1 = New DataView(Me.DsSecResource1.GetResRecommendation)
        dvS2 = New DataView(Me.DsSecResource1.GetResFeedback)
        dvS3 = New DataView(Me.DsSecResource1.tblResourceWarning)
        dvS4 = New DataView(Me.DsGetLocation1.GetResourceLocation)
        cmdChoice.CommandType = CommandType.StoredProcedure
        cmdChoice.Connection = sc

        strbActiveGrid.Append("grdvwMain")    'use this for doubleclick code

        Me.AcceptButton = Me.btnSearch
        Me.StatusBarPanel3.Text = "Ready"
        WhatField = "KeyIndex" '"Name"
        Me.cboField.SelectedIndex = 2 'search all terms; 0 = name
        isLoaded = True
        Forms.Add(Me)

    End Sub

    'CLOSE
    Private Sub miCloseForm_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles miCloseForm.Click
        Me.Close()
    End Sub

#End Region    'load

#Region "Search"

    'FILL MAIN DATAGRID
    Protected Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles btnSearch.Click, miSearch.Click 'edIndexChanged, cboType.SelectedIndexChanged ', txtSearch.Leave ', cboSearch.SelectedIndexChanged
        'LOAD SEARCH dataGRID BASED ON USER CHOICES OR DEFAULTS

        If isLoaded Then
            If modGlobalVar.NewValidateCombo(Me.cboType, True) Then
            Else
                Exit Sub
            End If
            If modGlobalVar.NewValidateCombo(Me.cboField, True) Then
            Else
                Exit Sub
            End If
            If modGlobalVar.NewValidateCombo(Me.cboRegion, False) Then
            Else
                Exit Sub
            End If
        Else
            Exit Sub
        End If
        MouseWait()
        ClearGrids()
        bFoundMain = False
        Me.grdvwMain.Refresh()

        If sender.name = "cboRegion" Then
            'refresh collection list
            If Me.rbCollection.Checked = True Then
                Me.rbCollection.PerformClick()
            End If
            Exit Sub
        End If

        ''1 CHECK FOR USER ENTRY:
        Select Case WhatField
            Case Is = "CRG"
                ''TODO ENHANCEMENT: allow data entry as well as selection here.  Note: substring does not work if entry length is less than "Select a" 
                If Me.cboCRG.SelectedIndex < 0 Then
                    'modGlobalVar.Msg("Please make a selection from the dropdown box", MessageBoxIcon.Exclamation, "incomplete information")
                    Me.cboCRG.DroppedDown = True
                    Exit Sub
                End If
            Case Is = "IndexList", "Collection", "Series", "ResourceGuide", "ReferralSource"
                'If cboChoices.SelectedIndex < 1 Then
                If cboChoices.SelectedItem = Nothing Then
                    cboChoices.DroppedDown = True '  modGlobalVar.Msg("Please make a selection from the dropdown box", MessageBoxIcon.Exclamation, "Incomplete Information for " & WhatField)
                    Exit Sub
                End If
            Case Else
                If Me.txtSearch.Text = "enter search text" Then
                    modGlobalVar.msg("ATTENTION: incomplete information", "please enter some text to search for " & WhatField & ".", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
        End Select

        '2 SET STATUS BAR & CURSOR HOURGLASS
        Me.StatusBarPanel3.Text = "Searching... " '& SrchWhat
        Me.daspSrchResources.SelectCommand.Parameters("@Region").Value = Nothing
        Me.daspSrchResources.SelectCommand.Parameters("@IDFld").Value = Nothing
        Me.daspSrchResources.SelectCommand.Parameters("@IDVal").Value = Nothing
        Me.daspSrchResources.SelectCommand.Parameters("@IDstr").Value = Nothing
        Me.daspSrchResources.SelectCommand.Parameters("@Type").Value = Nothing
        Me.daspSrchResources.SelectCommand.Parameters("@Active").Value = Me.chkActive.Checked
        Me.daspSrchResources.SelectCommand.Parameters("@Recommend").Value = Me.chkRecommend.Checked
        Me.daspSrchResources.SelectCommand.Parameters("@Feedback").Value = Me.chkFeedback.Checked
        Me.daspSrchResources.SelectCommand.Parameters("@NewCRG").Value = Me.chkNewCRG.Checked

        If Me.cboRegion.SelectedIndex <> 0 And WhatField <> "ID" Then   'And WhatField <> "ISBN" Then
            Me.daspSrchResources.SelectCommand.Parameters("@Region").Value = Me.cboRegion.SelectedItem
        End If

        If Me.cboType.SelectedIndex <> 0 Then
            Me.daspSrchResources.SelectCommand.Parameters("@Type").Value = Me.cboType.SelectedItem
        End If

        If WhatField > Nothing Then
            ' Me.daspSrchResources.SelectCommand.Parameters("@Fld").Value = WhatField 'column to append
            Me.daspSrchResources.SelectCommand.Parameters("@IDFld").Value = WhatField 'column to search
            ' modGlobalVar.Msg(WhatField, , Me.txtSearch.Text)
            Select Case WhatField
                'string from combo
                Case "IndexList", "Series"
                    Me.daspSrchResources.SelectCommand.Parameters("@IDstr").Value = modGlobalVar.GetWild(Me.cboChoices.Text, Me.chkWild.CheckState)
                    'string from textfield
                Case "Name", "KeyIndex"
                    Me.daspSrchResources.SelectCommand.Parameters("@IDstr").Value = modGlobalVar.GetWild(Me.txtSearch.Text, Me.chkWild.CheckState)
                    'string from textfield
                Case "ISBN"
                    Me.daspSrchResources.SelectCommand.Parameters("@IDstr").Value = modGlobalVar.GetWild(Me.txtSearch.Text, Me.chkWild.CheckState)
                    'string from crg combo
                Case "CRG"
                    Me.daspSrchResources.SelectCommand.Parameters("@IDVal").Value = Me.cboCRG.SelectedValue
                        Me.daspSrchResources.SelectCommand.Parameters("@IDstr").Value = modGlobalVar.GetWild(Me.cboCRG.Text, Me.chkWild.CheckState)
                    'string from choice combo
                Case "ResourceGuide", "ReferralSource"
                    Me.daspSrchResources.SelectCommand.Parameters("@IDstr").Value = modGlobalVar.GetWild(Me.cboChoices.Text, True)
                    'integer from textbox
                Case Is = "Website"
                    Me.daspSrchResources.SelectCommand.Parameters("@IDstr").Value = modGlobalVar.GetWild(Me.txtSearch.Text, Me.chkWild.CheckState)
                Case "ID"
                    Me.daspSrchResources.SelectCommand.Parameters("@IDval").Value = Me.txtSearch.Text
                Case "Collection"   'all collections retired 2011
                    Me.daspSrchResources.SelectCommand.Parameters("@IDstr").Value = modGlobalVar.GetWild(Me.cboChoices.Text, Me.chkWild.CheckState)
                Case Is = "City"
                    Me.daspSrchResources.SelectCommand.Parameters("@IDstr").Value = modGlobalVar.GetWild(Me.txtSearch.Text, Me.chkWild.CheckState)
                Case Is = "Satellite Region"
                    Me.daspSrchResources.SelectCommand.Parameters("@IDstr").Value = modGlobalVar.GetWild(Me.txtSearch.Text, Me.chkWild.CheckState)
                    Me.daspSrchResources.SelectCommand.Parameters("@IDFld").Value = "SatelliteRegion"
                Case Else
                    modGlobalVar.msg("ERROR: not found", WhatField, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
            End Select
        End If
        Me.DsSrchResources1.EnforceConstraints = False

PerformSearch:
        '5 FILL dataSET
        Me.DsSrchResources1.Clear()
        Me.DsSrchResources1.EnforceConstraints = False
        Try
            Me.daspSrchResources.Fill(Me.DsSrchResources1.SrchResource)
        Catch exc As System.FormatException
            modGlobalVar.msg(MsgCodes.invalidSearch)
            Me.StatusBarPanel3.Text = "main grid fill error"
            Exit Sub            ' Me.daGetLocation.Fill(Me.DsGetLocation1.tblResourceLocation)
        Catch exc As Exception
            modGlobalVar.msg("ERROR: filling main grid", exc.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.StatusBarPanel3.Text = "main grid fill error"
            Exit Sub
        End Try

        ' 6 SET HEADINGS & MENUITEMS
        '    STATUS BAR; INCLUDE COUNT OF FOUND ITEMS; RESET CURSOR; RESET MENU ITEMS
        If Me.DsSrchResources1.Tables(0).Rows.Count > 0 Then 'Or dsSrchContact.Tables(0).Rows.Count > 0 Then
            'DONE: count the rows without the extra authors
            Me.lblMainGrid.Text = GetCount.ToString & "  " & strDGM
            bFoundMain = True
            Me.grdvwMain.Rows(0).Selected = True
            GetCounts()
        Else
            modGlobalVar.msg("NO MATCHES FOUND", "Hint: " & NextLine & "Verify the Type of Resource and Region are correct in the dropdown boxes." & NextLine & "If the Wildcard checkbox is on, wildcards are added at the beginning and end of your search text.", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.lblMainGrid.Text = "NO MATCHING RESOURCES FOUND"
        End If
        isSearched = True
        SrchTotal = GetCount()
        MouseDefault()
        Me.StatusBarPanel3.Text = "Done"

    End Sub

    'SET SEARCH COMBOS PARAMETERS FROM RADIO BUTTONS
    Private Sub cboField_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboField.SelectedIndexChanged
        Dim instruction As String
        If modGlobalVar.NewIsHeading(sender) = True Then
            Exit Sub
        End If
        WhatField = sender.text '***
        ClearGrids()

        Me.cboChoices.Visible = False
        Me.cboChoices.DataSource = Nothing
        Me.cboChoices.Items.Clear()
        Me.cboCRG.Visible = False
        Me.chkWild.Checked = False

        Select Case sender.text
            Case Is = "Name, Author/Contact"
                WhatField = "Name"
            Case Is = "all Keywords/Index Terms, Name"
                WhatField = "KeyIndex"
            Case Is = "CRG Category"
                WhatField = "CRG"
            Case Is = "Index Terms"
                WhatField = "IndexList"
            Case Is = "Resource ID #"
                WhatField = "ID"
            Case Is = "Resource Guide"
                WhatField = "ResourceGuide"
            Case Is = "Referral Source"
                WhatField = "ReferralSource"
            Case Else
                WhatField = sender.text
        End Select

        Select Case WhatField
            Case "Name", "KeyIndex", "Website"
                Me.chkWild.Checked = True
                Me.txtSearch.Focus()
                Me.txtSearch.SelectAll()
            Case "IndexList"
                cmdChoice.CommandText = ("[luResourceIndex]")
                instruction = "--- Select an Index term ---"
                LoadcboChoices(Me.cboChoices, instruction)

            Case "Series"
                cmdChoice.CommandText = ("[luResourceSeries]")
                instruction = "--- Select a Series ---"
                LoadcboChoices(Me.cboChoices, instruction)
            Case "ReferralSource"
                cmdChoice.CommandText = ("[luReferralBy]")
                instruction = "--- Select a Referral Source ---"
                LoadcboChoices(Me.cboChoices, instruction)
            Case "ResourceGuide"
                cmdChoice.CommandText = ("[luResourceGuide]")
                instruction = "--- Select a Resource Guide ---"
                LoadcboChoices(Me.cboChoices, instruction)
                '    Me.cboCRG.Visible = True
            Case "Collection"
                cmdChoice.CommandText = ("[luResourceCollection]")
                cmdChoice.Parameters.Add("@Region", System.Data.SqlDbType.VarChar)
                If Me.cboRegion.SelectedItem = "All Regions" Then
                    cmdChoice.Parameters("@Region").Value = "%"
                Else
                    cmdChoice.Parameters("@Region").Value = Me.cboRegion.Text
                End If
                instruction = "--- Select a Collection ---"
                LoadcboChoices(Me.cboChoices, instruction)
                cmdChoice.Parameters.Remove(cmdChoice.Parameters("@Region"))
            Case "CRG"
                'LOAD POPUPMENU
                Me.chkWild.Checked = True
                modGlobalVar.LoadCRGCombo(Me.cboCRG) 'don't use this so can have title
                Me.cboCRG.Visible = True
                Me.cboCRG.DroppedDown = True
            Case Else
                Me.txtSearch.Focus()
                Me.txtSearch.SelectAll()
        End Select
        Me.btnSearch.PerformClick()
        Me.StatusBar1.Panels(0).Text = "Done"
    End Sub

    'LOAD SELECTION COMBOs BASED ON USER SELECTED RADIO BUTTON
    Private Sub LoadcboChoices(ByRef cbo As ComboBox, ByVal instruction As String)

        Me.StatusBar1.Panels(0).Text = "Loading index choices"
        If Not SCConnect() Then
            Exit Sub
        End If
        Me.cboCRG.DataSource = Nothing
        Me.cboCRG.Items.Clear()

        Try
            dr = cmdChoice.ExecuteReader()
            cbo.BeginUpdate()
            While dr.Read()
                cbo.Items.Add(dr.GetString(0))
            End While
            cbo.EndUpdate()
        Catch ex As Exception
            modGlobalVar.msg("ERROR: loading combobox", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        dr.Close()
        sc.Close()
        cbo.Visible = True
        cbo.DroppedDown = True
        cbo.Text = instruction
        Me.StatusBar1.Panels(0).Text = "Done"

    End Sub

#End Region  'search

#Region "datagrid"

    'FILL DETAIL DS IF USER REQUESTS
    Private Sub grdMain_SelectionChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles grdvwMain.SelectionChanged
        If Not isLoaded Then
            Exit Sub
        End If

        If Me.DsSrchResources1.Tables(0).Rows.Count > 0 And Me.grdvwMain.SelectedCells.Count > 0 Then
            '    modGlobalVar.Msg(Me.grdvwMain.Item(0, Me.grdvwMain.CurrentRow.Index).Value.ToString, , "currentcellchanged")
            ThisID = grdvwMain.CurrentRow.Cells("ICCResourceID" & grdvwColName).Value
        Else
            Exit Sub
        End If
        ClearSecGrids() 'clear filters and status bar text
        If Me.chkDetail.Checked Then
            LoadSecondary()
        Else
        End If

        Me.StatusBarPanelID.Text = "Resource ID: " & ThisID
        Me.StatusBarPanel4.Text = "CRG/Keyword 1: " & Me.grdvwMain.CurrentRow.Cells("Keyword").Value
        Me.SecID.Text = ""
        GetCounts()

    End Sub

    'count secondary table rows
    Private Sub GetCounts()

        Dim cmdCntID As New SqlCommand
        cmdCntID.CommandText = "SELECT COUNT(RecommendID) FROM tblResourceRecommend WHERE ResourceNum = " & ThisID
        cmdCntID.Connection = sc
        If Not SCConnect() Then
            Exit Sub
        End If

        Dim i As Integer = 0
        Try
            i = cmdCntID.ExecuteScalar()
        Catch ex As Exception
            '      modGlobalVar.Msg(ex.Message)
        End Try
        Me.cntRecommendation.Text = i.ToString()

        cmdCntID.CommandText = "SELECT COUNT(FeedbackID) FROM tblResourceFeedback WHERE ResourceNum = " & ThisID
        i = 0
        Try
            i = cmdCntID.ExecuteScalar()
        Catch ex As Exception
            '      modGlobalVar.Msg(ex.Message)
        End Try
        Me.cntFeedback.Text = i.ToString()
        cmdCntID.CommandText = "SELECT COUNT(WarningID) FROM tblResourceWarning WHERE ResourceNum = " & ThisID
        i = 0
        Try
            i = cmdCntID.ExecuteScalar()
        Catch ex As Exception
            '      modGlobalVar.Msg(ex.Message)
        End Try
        Me.cntAlert.Text = i.ToString()

        cmdCntID.CommandText = "SELECT COUNT(ResourceLocationID) FROM tblResourceLocation WHERE ResourceNum = " & ThisID
        i = 0
        Try
            i = cmdCntID.ExecuteScalar()
        Catch ex As Exception
            '      modGlobalVar.Msg(ex.Message)
        End Try
        Me.cntLocation.Text = i.ToString()

        sc.Close()
    End Sub

    'CLEAR SELECTION FROM dataGRIDS AND LOSE FOCUS (note: selection stays with arrow)
    Private Sub grdUnselect_Click(ByVal sender As Object, ByVal e As System.EventArgs) _
            Handles MyBase.Click
        Try
            If IsNull(Me.grdvwMain.CurrentRow.Index, -1) > -1 Then
                Me.grdvwMain.ClearSelection()
                ThisID = 0
                Me.StatusBarPanelID.Text = "Resource ID:"
                '  Me.grdvwMain.NavigateBack()
            End If
            If grdSecond1.CurrentRowIndex > -1 Then
                Me.grdSecond1.UnSelect(grdSecond1.CurrentRowIndex)
                Me.grdSecond1.NavigateBack()
            End If
        Catch ex As Exception
        End Try

    End Sub

    'SET SEARCH CRITERIA TO DEFAULTS AND CLEAR dataSETS
    Protected Sub miClearSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles miClearSearch.Click

        Dim rb As RadioButton
        Me.cboRegion.SelectedIndex = 0
        Me.cboType.SelectedIndex = 0
        Me.txtSearch.Text = "enter search text"
        For Each rb In Me.grpOrg.Controls
            rb.Checked = False
        Next
        WhatField = Nothing
        ClearGrids()
        Me.Refresh()
        dv.RowFilter = ""
        SetStatusBarText("Ready")
    End Sub

    'grid DOUBLE CLICK
    Private Sub dataGridDouble(ByVal sender As Object, ByVal e As MouseEventArgs)

        If (DateTime.Now < modGlobalVar.CheckDouble(sender, e).AddMilliseconds(SystemInformation.DoubleClickTime)) Then
            Dim iorg As Integer = 0

            Select Case strbActiveGrid.ToString
                Case Is = "grdvwMain"
                    modGlobalVar.OpenResourceChoice(ThisID, Me.grdvwMain.Item(1, Me.grdvwMain.CurrentRow.Index).Value)
                Case Is = "grdSecond1"
                    SetStatusBarText("Opening " & usrRadio.ToString & " Detail Window")
                    Select Case Me.usrRadio.ToString
                        Case "Recommendations"
                            If IsDBNull(Me.grdSecond1.Item(Me.grdSecond1.CurrentRowIndex, 6)) Then
                            Else
                                iorg = Me.grdSecond1.Item(Me.grdSecond1.CurrentRowIndex, 6)
                            End If
                            modGlobalVar.OpenMainRecommend(Me.grdSecond1.Item(Me.grdSecond1.CurrentRowIndex, 0), IsNull(Me.grdvwMain.Item(1, Me.grdvwMain.CurrentRow.Index).Value, "unknown"), iorg)
                            'TODO
                            'modGlobalVar.Msg("open recommendation")
                        Case "Locations"
                            If IsNull(Me.grdSecond1.Item(Me.grdSecond1.CurrentRowIndex, 0), 0) = 0 Then
                            Else
                                modGlobalVar.OpenMainResourceLocation(Me.grdSecond1.Item(Me.grdSecond1.CurrentRowIndex, 0), Me.grdvwMain.Item(0, Me.grdvwMain.CurrentRow.Index).Value, IsNull(Me.grdvwMain.Item(1, Me.grdvwMain.CurrentRow.Index).Value, "unknown"), Me.grdSecond1.Item(Me.grdSecond1.CurrentRowIndex, 1))
                            End If
                        Case "Feedback"
                            'TODO
                            modGlobalVar.OpenMainFeedback(Me.grdSecond1.Item(Me.grdSecond1.CurrentRowIndex, 0), Me.grdSecond1.Item(Me.grdSecond1.CurrentRowIndex, 1), IsNull(Me.grdvwMain.Item(1, Me.grdvwMain.CurrentRow.Index).Value, "unknown"), Me.grdSecond1.Item(Me.grdSecond1.CurrentRowIndex, Me.grdSecond1.TableStyles("GetResFeedback").GridColumnStyles.IndexOf(Me.grdSecond1.TableStyles("GetResFeedback").GridColumnStyles("OrgNum"))))
                        Case "Alerts"
                            modGlobalVar.OpenMainResourceWarning(Me.grdSecond1.Item(Me.grdSecond1.CurrentRowIndex, 0), Me.grdSecond1.Item(Me.grdSecond1.CurrentRowIndex, 1), IsNull(Me.grdvwMain.Item(1, Me.grdvwMain.CurrentRow.Index).Value, "unknown"))
                    End Select
                Case Else
                    modGlobalVar.msg("ERROR: dblclick sender name not matched", sender.name, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Select
        Else 'single click
            If strbActiveGrid.ToString = "grdSecond1" Then
                Me.SecID.Text = grdSecond1.Item(grdSecond1.CurrentRowIndex, 0)
            End If
        End If
    End Sub

#End Region 'datagrid

#Region "Filter Grid"

    'CAPTURE RIGHT MOUSE CLICK TO FILTER MAIN DATAGRID VIEW
    Protected Sub grdMain_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) _
            Handles grdvwMain.MouseDown
        Dim str() As String = modGlobalVar.FilterDataGridView(sender, e, Me.DsSrchResources1.SrchResource, False)

        If str(1) = String.Empty Or str(1) = "LEFT" Then
            lblMainGrid.Text = GetCount.ToString
        Else
            lblMainGrid.Text = str(0) & "/" & SrchTotal & str(1) 'dont count duplicate resources in filtered number
            If Me.chkDetail.Checked Then
                ClearSecGrids()    'clear child grids 
            End If
        End If
    End Sub

    'count distinct resources in main grid re duplicates re multiple authors
    Private Function GetCount() As Integer
        Dim tDistinct As New DataTable("tDistinct")
        tDistinct = Me.DsSrchResources1.Tables(0).DefaultView.ToTable(True, "ICCResourceID")
        GetCount = tDistinct.Rows.Count
        tDistinct = Nothing
    End Function

    'SET ID
    Private Sub grdSecond1_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdSecond1.CurrentCellChanged
        If IsDBNull(sender.item(sender.CurrentRowIndex, 0)) Then
            Me.SecID.Text = "none"
        Else
            Me.SecID.Text = sender.item(sender.CurrentRowIndex, 0)
        End If
    End Sub 'main grid

    'secondary grids
    Protected Sub grdSec_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) _
             Handles grdSecond1.MouseDown
        Dim tbl As Object
        Dim strHdr As String    'text for grid header
        Dim intTS As Integer   'table style of grid for case select 
        strbActiveGrid.Replace(strbActiveGrid.ToString, sender.name)    'use this for doubleclick code
RightClick:
        If e.Button = System.Windows.Forms.MouseButtons.Right Then
            'SET VARs BASED ON GRID SELECTED
            hti = sender.HitTest(e.X, e.Y)
            Select Case sender.name.ToString
                Case Is = "grdSecond1"
                    If RadioButton1.Checked Then
                        tbl = Me.DsSecResource1.GetResRecommendation
                        intTS = 0
                        strHdr = UCase(RadioButton1.Text) 'strDGS1
                    ElseIf RadioButton2.Checked Then
                        tbl = Me.DsSecResource1.GetResFeedback
                        intTS = 1
                        strHdr = UCase(RadioButton2.Text) 'strDGS2
                    ElseIf RadioButton3.Checked Then
                        tbl = Me.DsSecResource1.tblResourceWarning
                        intTS = 2
                        strHdr = UCase(RadioButton3.Text) 'strDGS2
                    ElseIf RadioButton4.Checked Then
                        tbl = Me.DsGetLocation1.GetResourceLocation
                        intTS = 0
                        strHdr = UCase(RadioButton4.Text)
                    Else
                        modGlobalVar.msg("ERROR: GRID NOT FOUND", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Exit Sub
                    End If
            End Select
SetFilter:
            If hti.Type = DataGrid.HitTestType.Cell Then    'SET FILTER
                'CHECK FOR NULL
                If IsDBNull(sender.Item(hti.Row, hti.Column)) Then 'nulls cause filter error
                    modGlobalVar.msg(MsgCodes.filterEmpty)
                    Exit Sub
                End If
                'CHECK FOR APOSTROPHE
                If Object.ReferenceEquals(sender.item(hti.Row, hti.Column).GetType, Now.GetType) Or Object.ReferenceEquals(sender.item(hti.Row, hti.Column).GetType, GetType(Integer)) Then
                Else    'or throws an error on date fields
                    If sender.item(hti.Row, hti.Column).indexof("'") > 0 Then
                        modGlobalVar.msg(MsgCodes.filterApostrophe)
                        Exit Sub
                    End If
                End If

                Select Case strHdr
                    Case Is = strDGS1   'recommend
                        grdFilter(sender, Me.DsSecResource1, tbl, strHdr, dvS1, 0)
                    Case Is = strDGS2   'feedback
                        grdFilter(sender, Me.DsSecResource1, tbl, strHdr, dvS2, 1)
                    Case Is = strDGS3   'warning
                        grdFilter(sender, Me.DsSecResource1, tbl, strHdr, dvS3, 2)
                    Case Else
                        Exit Sub
                End Select
ClearFilter:
            Else            'not in cell,  'CLEAR FILTER
                sender.dataSource = tbl 'removes dv.rowfilter
                sender.CaptionText = tbl.Rows.Count.ToString & "  " & strHdr 'DsOrgSearch1.tblOrg.Rows.Count.ToString
                Me.StatusBarPanel3.Text = ""
                Select Case strHdr
                    Case Is = strDGM
                        statusM = "Done"
                        If Me.chkDetail.Checked Then
                            ClearSecGrids()    'clear child grids 
                        End If
                        statusS1 = ""
                    Case Is = strDGS1, strDGS3, strDGS4
                        statusS1 = ""
                End Select
                SetStatusBarText("")
            End If
LeftClick:
        Else    'is not right mouse button, capture doubleclick
        End If
    End Sub

    'FILTER METHOD
    Private Sub grdFilter(ByVal grd As Object, ByVal ds As Object, ByVal tbl As Object, ByVal strHdr As String, ByVal dv As DataView, ByVal inTS As Integer)
        Dim strFilter As String
        Dim myColumns As GridColumnStylesCollection

        If grd.name = "grdvwMain" Then
            ' Create a BindingSource and set its DataSource property to the DataView.
            source1.DataSource = Me.DsSrchResources1.Tables(0)
            ' Set the data source for the DataGridView.
            grdvwMain.DataSource = source1
            'set filter
            source1.Filter = grd.columns(htivw.ColumnIndex).datapropertyname & " = '" & grd.Item(htivw.ColumnIndex, htivw.RowIndex).value & " '"
            statusM = strHdr & " filtered on " & grd.columns(htivw.ColumnIndex).HeaderText '& " = " & grd.Item(hti.Row, hti.Column)
            ClearSecGrids()
        Else    'secondary grid.  Use tabletyle properties
            myColumns = grd.TableStyles(inTS).GridColumnStyles
            strFilter = myColumns(hti.Column).MappingName
            strFilter = strFilter & " = '" & grd.Item(hti.Row, hti.Column) & " '"
            dv.RowFilter = strFilter
            grd.dataSource = dv
            grd.CaptionText = dv.Count.ToString & "/" & tbl.Rows.Count.ToString & "  " & strHdr
            statusS1 = strHdr & " filtered on " & myColumns(hti.Column).HeaderText
        End If
        SetStatusBarText("Done")

    End Sub

    'REMOVE MAIN GRID FILTER
    Private Sub lblMainGrid_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lblMainGrid.MouseClick
        If e.Button = System.Windows.Forms.MouseButtons.Right Then
            source1.RemoveFilter()
        End If
    End Sub

    'CLEAR MAIN GRID
    Private Sub ClearGrids()
        Me.DsSrchResources1.Clear()
        Me.lblMainGrid.Text = ""
        ClearSecGrids()
    End Sub

    'CLEAR SECONDARY GRIDS
    Private Sub ClearSecGrids()
        Me.DsGetLocation1.Clear()
        Me.DsSecResource1.Clear()
        Me.grdSecond1.CaptionText = ""
        statusS1 = ""
        statusS2 = ""
        Me.cntAlert.Text = 0
        Me.cntFeedback.Text = 0
        Me.cntLocation.Text = 0
        Me.cntRecommendation.Text = 0
        SetStatusBarText(statusM)
    End Sub

#End Region 'filter grid

#Region "DetailGrid"

    'HIDE SECONdaSrchRY GRIDS WHEN NOT REQUIRED
    Private Sub chkDetail_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles chkDetail.CheckedChanged, RadioButton1.CheckedChanged, RadioButton2.CheckedChanged, RadioButton3.CheckedChanged, RadioButton4.CheckedChanged
        ' Dim x As Byte
        If isLoaded Then
            Me.grdSecond1.Visible = chkDetail.Checked
            If sender.name = "chkDetail" Then   'do nothing else
                Exit Sub
            End If

            'CHANGE VARIABLE
            If sender.checked Then
                usrRadio.Replace(usrRadio.ToString, sender.text)
                LoadSecondary()
                GetCounts()
            End If

            'SET MENU ITEMS
            '  SetMenuItems(sender.text)

        End If

    End Sub

    '    'modGlobalVar.Msg(SrchWhat)
    'SetMenuItemsandSecondaryDataset:
    '            If SrchWhat = "Organizations" Then
    '                If RadioButton1.Checked Then    'org cases
    '    'SrchWhat = RadioButton1.Text
    '                    Me.grdSecond1.DataSource = Me.DsSec1.GetCase
    '    '  Me.grdSecond1.DataMember = "GetCase"  'Me.DsSec1.Tables(0).TableName
    '    'Me.DsSec1.GetCase
    '                    Me.grdSecond1.CaptionText = Me.DsSec1.GetCase.Rows.Count.ToString & strDGS1
    '                    Me.miGotoCase.Enabled = True

    '                Else
    '                    If RadioButton2.Checked Then    'org contacts
    '    '   SrchWhat = RadioButton2.Text
    '                        Me.grdSecond1.DataSource = Me.DsSec2.GetContact
    '                        Me.grdSecond1.DataMember = Me.DsSec2.Tables(0).TableName
    '                        Me.grdSecond1.CaptionText = Me.DsSec2.Tables(0).Rows.Count.ToString & strDGS2
    '                        Me.miGotoContact.Enabled = True

    '                    Else
    '    '  Me.grdSecond1.Visible = False
    '                        Exit Sub
    '                    End If
    '                End If
    '            Else
    '                If RadioButton1.Checked Then    'contact conversations
    '    'SrchWhat = RadioButton1.Text
    '                    Me.grdSecond1.DataSource = Me.DsGetConversations1.GetConversations
    '                    Me.grdSecond1.CaptionText = Me.DsGetConversations1.Tables(0).Rows.Count.ToString & strDGS3
    '                    Me.miGotoConversation.Enabled = True
    '    'SrchWhat = RadioButton1.Text
    '                Else
    '                    If RadioButton2.Checked Then    'contact registrations
    '    '  SrchWhat = RadioButton2.Text
    '                        Me.grdSecond1.DataSource = Me.DsGetRegistrations1.GetRegistrations
    '                        Me.grdSecond1.CaptionText = Me.DsGetRegistrations1.Tables(0).Rows.Count.ToString & strDGS4
    '                        Me.miGotoEdEvent.Enabled = True
    '    ' SrchWhat = RadioButton2.Text
    '                    Else
    '    '  Me.grdSecond1.Visible = False
    '                        Exit Sub
    '                    End If
    '                End If
    '            End If

    '        End If
    '    End Sub


    'LOAD SECONdaSrchRY GRIDS
#End Region  'detail grid

#Region "Load Secondary"

    'LOAD SEC
    Protected Sub LoadSecondary()
        Dim ds As DataSet, da As SqlDataAdapter, tbl As DataTable, idt As Integer
        Dim strCaption As String
        Dim miName As String

        If Not bFoundMain Or Me.chkDetail.Checked = False Then 'main grid returned no records
            Exit Sub
        End If

        SetStatusBarText("Retrieving data")
        ClearSecGrids()
        DisableMenuItems()

        If RadioButton1.Checked Then
            da = Me.daspGetRecommendations
            ds = Me.DsSecResource1
            tbl = Me.DsSecResource1.Tables(0)
            strCaption = strDGS1
            da.SelectCommand.Parameters("@IDFld").Value = "ResourceNum"
            idt = 0
            miName = RadioButton1.Text
        ElseIf RadioButton2.Checked Then
            da = Me.daspGetFeedback
            ds = Me.DsSecResource1
            tbl = Me.DsSecResource1.Tables(1)
            strCaption = strDGS2
            da.SelectCommand.Parameters("@IDFld").Value = "ResourceNum"
            idt = 1
            miName = RadioButton2.Text
        ElseIf RadioButton3.Checked Then
            da = Me.daspGetAlert
            ds = Me.DsSecResource1
            tbl = Me.DsSecResource1.Tables(2)
            strCaption = strDGS3
            da.SelectCommand.Parameters("@IDFld").Value = "ResourceNum"
            idt = 2
            miName = RadioButton3.Text
        ElseIf RadioButton4.Checked Then
            da = Me.daGetLocation
            ds = Me.DsGetLocation1
            tbl = Me.DsGetLocation1.GetResourceLocation
            strCaption = strDGS4
            idt = 0
            miName = RadioButton4.Text
        Else    'no radio button checked
            Exit Sub
        End If
        da.SelectCommand.Parameters("@IDVal").Value = ThisID
        da.SelectCommand.Connection = sc

fillSecondaryDataset:
        ds.Clear()
        Try
            ds.EnforceConstraints = False
            da.Fill(ds)
        Catch ex As Exception
            modGlobalVar.msg("ERROR: daSrch2", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        If tbl.Rows.Count > 0 Then
            Me.grdSecond1.CaptionText = tbl.Rows.Count.ToString & "  " & strCaption
            Me.miGoto.MenuItems(idt + 1).Enabled = True
            dvS1 = New DataView(ds.Tables(idt))
            Me.grdSecond1.DataSource = dvS1
            Me.grdSecond1.Refresh()
            bFoundSec = True
        Else
            bFoundSec = False
            Me.grdSecond1.CaptionText = "NO  " & strCaption & " FOUND"
        End If
        SetStatusBarText("Done")
    End Sub 'load secondary

#End Region 'load sec

#Region "Open Main Form"

    'OPEN FORM FROM MAIN GRID
    Protected Sub OpenMain(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles grdvwMain.DoubleClick, miGotoResource.Click
        If Not bFoundMain Then
            SetStatusBarText("can't open from empty grid")
            Exit Sub
        End If
        SetStatusBarText("Opening " & strMainDS & " Detail window")
        modGlobalVar.OpenResourceChoice(ThisID, Me.grdvwMain.Item(1, Me.grdvwMain.CurrentRow.Index).Value) ', ClassOpenForms.frmMainOrg.DsMainOrg1, ClassOpenForms.frmMainOrg.daMainOrg, ClassOpenForms.frmMainOrg.miSave, "MainOrg", ClassOpenForms.frmMainOrg.daMainOrg.SelectCommand.Parameters("@OrgID"))
        SetStatusBarText("Done")
    End Sub

#End Region   'open main form

#Region "Open Secondary Forms"

    'OPEN FORM from SECONDARY GRID
    Protected Sub OpenSecondary(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles grdSecond1.DoubleClick, miGotoRecommendation.Click, miGotoFeedback.Click, miGotoAlert.Click

        If Not bFoundSec Then
            Exit Sub
        End If

        MouseWait()
        SetStatusBarText("Opening " & usrRadio.ToString & " Detail Window")
        Select Case Me.usrRadio.ToString
            Case "Recommendations"
                modGlobalVar.OpenMainRecommend(Me.grdSecond1.Item(Me.grdSecond1.CurrentRowIndex, 0), IsNull(Me.grdvwMain.Item(1, Me.grdvwMain.CurrentRow.Index).Value, "unknown"), IsNull(Me.grdSecond1.Item(Me.grdSecond1.CurrentRowIndex, 6), 0))
                'TODO
                'modGlobalVar.Msg("open recommendation")
            Case "Feedback"
                modGlobalVar.OpenMainFeedback(Me.grdSecond1.Item(Me.grdSecond1.CurrentRowIndex, 0), IsNull(Me.grdSecond1.Item(Me.grdSecond1.CurrentRowIndex, 1), 0), IsNull(Me.grdvwMain.Item(1, Me.grdvwMain.CurrentRow.Index).Value, "unknown"), IsNull(Me.grdSecond1.Item(Me.grdSecond1.CurrentRowIndex, Me.grdSecond1.TableStyles("GetResFeedback").GridColumnStyles.IndexOf(Me.grdSecond1.TableStyles("GetResFeedback").GridColumnStyles("OrgNum"))), 0))
            Case "Locations"
                If IsNull(Me.grdSecond1.Item(Me.grdSecond1.CurrentRowIndex, 1), "") = "" Then     'no location, only CRG listed
                Else
                    modGlobalVar.OpenMainResourceLocation(Me.grdSecond1.Item(Me.grdSecond1.CurrentRowIndex, 0), Me.grdvwMain.Item(0, Me.grdvwMain.CurrentRow.Index).Value, IsNull(Me.grdvwMain.Item(1, Me.grdvwMain.CurrentRow.Index).Value, "unknown"), Me.grdSecond1.Item(Me.grdSecond1.CurrentRowIndex, 1))
                End If
            Case "Alerts"
                'TODO
                modGlobalVar.OpenMainResourceWarning(Me.grdSecond1.Item(Me.grdSecond1.CurrentRowIndex, 0), Me.grdSecond1.Item(Me.grdSecond1.CurrentRowIndex, 1), "Resource Alert")
        End Select
        MouseDefault()
        SetStatusBarText("Done")
    End Sub

#End Region  'open scondary form

#Region "Menu"

    'HELP
    Private Sub btnHelpOrg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnHelp.Click, miHelp.Click
        modGlobalVar.Msg("HELP: SEARCH for RESOURCE", "HOW TO SEARCH:" & NextLine & "1. Select search criteria using the radio buttons and drop down boxes. " & NextLine & "2. Click the Search button, or press the Enter key." & NextLine & NextLine & "Note: * is a wildcard character.  * is automatically utilized at the beginning and end " & NextLine & "    of your search string when applicable. To remove these wildcards uncheck the Wildcards checkbox.  " & NextLine & "    (Wildcards are not available for certain criteria like Resource ID)." & NextLine & NextLine & "To ADD NEW:" & NextLine & "RESOURCE - Click the yellow New button or use the menu: File/New Resource." & NextLine & "AUTHOR, CONTACT, LOCATION, RECOMMENDATION, FEEDBACK , OR ALERT - first go to the Resource Detail window, then click the New button.", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    'HELP - definitions
    Private Sub miDefinitions_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles miDefinitions.Click
        modPopup.ShowDefinitions("ResourceType")
    End Sub

    'disable menu items
    Private Sub DisableMenuItems()
        Me.miGotoAlert.Enabled = False
        Me.miGotoFeedback.Enabled = False
        Me.miGotoRecommendation.Enabled = False
    End Sub

#End Region    'menu

#Region "ADD ITEM"

    'INSERT NEW ITEM to BACKEND and OPEN DETAIL FORM
    Protected Sub miAddResource_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click, miAddResource.Click

        Me.StatusBarPanel3.Text = "Adding new Resource"
        If modGlobalVar.msg("Are you sure?", "About to enter a new Resource", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then
            Exit Sub
        End If
        MouseWait()
InsertNewRecord:
        Dim str As String = "INSERT INTO tblResource(ResourceName, ResourceType, CreateStaffNum) VALUES (N'" & StrConv(IsNull(Me.txtSearch.Text.Replace("'", "''"), "entering New Resource"), VbStrConv.ProperCase) & "', 'Book', " & usr & "); SELECT @@Identity"
        If Not SCConnect() Then
            Exit Sub
        End If

        Dim cmd As New SqlClient.SqlCommand(str, sc) ', myTrans)
        Dim newID As Integer
        Try
            newID = cmd.ExecuteScalar()
        Catch ex As Exception
            sc.Close()
            modGlobalVar.msg("ERROR: inserting resource", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        Finally
        End Try

        sc.Close()
OpenForm:
        modGlobalVar.OpenNewResource(newID)
        Me.StatusBarPanel3.Text = "Done"
        MouseDefault()
    End Sub

#End Region    'add item

#Region "General"

    'SET STATUS BAR LEFT TEXT re FILTERS
    Private Sub SetStatusBarText(ByVal str As String)
        Me.StatusBarPanel3.Text = str & " " & statusS1 '& " " & statusS2
    End Sub

    'COPY CURRENT ID
    Private Sub StatusBar1_DoubleClick(sender As System.Object, e As System.EventArgs) Handles StatusBar1.DoubleClick
        Clipboard.SetText(ThisID)
    End Sub

    'call Clear Grids
    Private Sub txtSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles txtSearch.TextChanged
        ClearGrids()
    End Sub

    'SELECT ALL TEXT
    Private Sub txtSearch_Enter(ByVal sender As Object, ByVal e As System.EventArgs) _
        Handles txtSearch.Enter
        Me.txtSearch.SelectAll()
    End Sub

    'call search
    Private Sub cboRegion_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) _
        Handles cboRegion.SelectedIndexChanged, cboType.SelectedIndexChanged
        If isLoaded Then
            Me.btnSearch.PerformClick()
        End If
    End Sub

#End Region 'general

#Region "Color Button"
    'BUTTON HIGHLIGHT ON ENTER
    Private Sub btnSearch_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnSearch.Enter
        sender.BackColor = Color.SandyBrown
    End Sub

    'BUTTON RESTORE COLOR
    Private Sub btnSearch_LostFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnSearch.Leave ', btnSearch.Click
        sender.BackColor = System.Drawing.SystemColors.InactiveCaption
    End Sub
#End Region    'color button

#Region "CRG COMBO"
    'RESIZE CRG Combo to normal
    Private Sub cboCRG_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles cboCRG.SelectionChangeCommitted 'IndexChanged
        If Me.cboCRG.SelectedIndex >= 0 Then
            Me.btnSearch.PerformClick()
        End If
    End Sub

    'FIND CRG TERM
    Private Sub cboCRG_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) _
            Handles cboCRG.MouseDown
        If e.Button = System.Windows.Forms.MouseButtons.Right Then
            modPopup.SearchCRG(Me, PointToClient(Control.MousePosition), Me.cboCRG)

        End If
    End Sub

    'call Search
    Private Sub cboChoices_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) _
            Handles cboChoices.SelectedIndexChanged
        If isLoaded = False Or modGlobalVar.NewIsHeading(sender) = True Then
        Else
            Me.btnSearch.PerformClick()
        End If
    End Sub

#End Region   'crg combo

#Region "Print"

    'PRINT PUBLIC RESOURCE REPORT
    Private Sub miPrintPublic_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles miPrintPublic.Click, miPrintDescriptions.Click, miPrintFeedbackRpt.Click, miPrintFull.Click
        Dim bSelected As Boolean = False
        Dim strb As New StringBuilder
        Dim strbID As New StringBuilder
        Dim cnt As Integer = 0    ''count unique results
        If Me.grdvwMain.SelectedRows.Count > 0 Then
        Else
            modGlobalVar.msg("Cancelling Report", "no Rows were selected in grid", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            GoTo CloseAll
        End If
        MouseWait()

        'GET QUERY CRITERIA
        Select Case sender.tag
            Case Is = "Public"
                strb.Append("SELECT * FROM vwRptResourcePublic WHERE ICCResourceID IN (" & modPopup.GetIDArray(Me.grdvwMain) & ") ")
                strb.Append(" ORDER BY ResourceType, SortFld, ResourceName")
                modPopup.PrintResourcePublic(strb.ToString, "RESOURCE REPORT: PUBLIC " & NextLine & "Searched " & WhatField & " for: '" & Me.txtSearch.Text & "'")
            Case Is = "Descriptions"
                strb.Append("SELECT * FROM vwRptResourceDescription  WHERE ICCResourceID IN (" & modPopup.GetIDArray(Me.grdvwMain) & ") ")
                modPopup.PrintResourceDescriptions(strb.ToString, "Searched " & WhatField & " for: '" & Me.txtSearch.Text & "'")
            Case Is = "All"
                modPopup.PrintResourceFull(modPopup.GetIDArray(Me.grdvwMain), "- Searched " & WhatField & " for: '" & Me.txtSearch.Text & "'")
            Case Is = "Feedback"
                Dim sp As New SqlClient.SqlCommand
                sp.CommandType = CommandType.StoredProcedure
                sp.CommandText = "[RptsResources]"
                sp.Connection = sc
                sp.Parameters.Add("@Which", SqlDbType.VarChar).Value = "FeedbackIndividual"
                sp.Parameters.Add("@Num", SqlDbType.VarChar).Value = modPopup.GetIDArray(Me.grdvwMain)
                modPopup.PrintResourceReport(sp, "RESOURCE REPORT: FEEDBACK", "", True, "ResourceName", "Feedback Report")
            Case Is = "XML" 'No Xml any more 10/14
                '========== write xml query to .csv file =====================
                '====this works with both text and sproc
                '====as long as original query results in 1 row
                '===== as 'data()' for column name of text fields in separate SELECT statement ending with FOR XML Path(''), Type 
                '=== and query itself ends with FOR XML path('RowName'), type, root) AS RowName
                '============================================================
                Dim strXMLFile As String = "C:\XMLSharetest.xml" 'result document
                Dim strExcelFile As String = "C:\XMLSharetest.xlsx"
                Dim wkbkXML As Excel.Workbook
                Dim wkbkXLS As Excel.Workbook
                Dim sql As New SqlClient.SqlCommand
                If File.Exists(strExcelFile) Then
                    File.Delete(strExcelFile)
                End If
                sql.Connection = sc
CreateXMLFile:
                sql.CommandType = CommandType.StoredProcedure   ' CommandType.Text
                sql.CommandText = "[RptsResourcesShare]"      '"SELECT * FROM zvwXMLTest"
                sql.Parameters.Add("@Which", SqlDbType.VarChar).Value = "Capital"
                sql.Parameters.Add("@Num", SqlDbType.VarChar).Value = modPopup.GetIDArray(Me.grdvwMain)

                sc.Open()
                Try
                    File.WriteAllText(strXMLFile, "<?xml version='1.0' encoding='UTF-8'?>")
                    File.AppendAllText(strXMLFile, sql.ExecuteScalar.ToString)
                Catch ex As System.Exception
                    File.AppendAllText(strXMLFile, sql.ExecuteScalar.ToString)
                    modGlobalVar.msg("ERROR: writing  ", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
                sc.Close()

SaveAsSpreadsheet:
                Dim oExcel As Object = CreateObject("Excel.Application")
                oExcel.DisplayAlerts = False
                Try
                    wkbkXML = oExcel.workbooks.openxml(strXMLFile)
                    wkbkXML.SaveAs(strExcelFile, 51) 'FileFormat:=51) '.xlsx
                Catch ex As System.Exception
                    modGlobalVar.msg("ERROR: saving .xlsx", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Finally
                    wkbkXML.Close()
                    ReleaseComObject(wkbkXML)
                End Try

                wkbkXLS = oExcel.workbooks.open(strExcelFile)
                Dim objSheet As Excel.Worksheet = wkbkXLS.Worksheets(1) '("XMLShareTest")
                Dim range1 As Excel.Range = objSheet.UsedRange
FormatSpreadsheet:
                Try
                    'VERTICAL ALIGNMENT
                    Try
                        range1.RowHeight = 50
                        range1.VerticalAlignment = Excel.XlVAlign.xlVAlignTop
                        range1 = Nothing
                    Catch ex As Exception
                        modGlobalVar.msg("ERROR: vertical alignment", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try
                    'REMOVE SLASHES FROM XML HEADINGS
                    Try
                        For c As Integer = 1 To objSheet.UsedRange.Columns.Count
                            Dim x As Integer = InStr(objSheet.Cells(2, c).value.ToString.Substring(1), "_") 'undscore in query to force column order
                            'InStr(objSheet.Cells(2, c).value.ToString.Substring(1), "/") 'find 2nd / in heading
                            If x > 0 Then
                                objSheet.Cells(2, c).value = objSheet.Cells(2, c).value.substring(x + 1)
                            End If
                        Next c
                    Catch ex As System.Exception
                        modGlobalVar.msg("ERROR: column names", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try

                    Try
                        For c As Integer = objSheet.UsedRange.Columns.Count To 1 Step -1
                            If objSheet.Cells(2, c).value.ToString.Contains("#agg") Then
                                objSheet.Columns(c).delete(Shift:=Excel.XlDeleteShiftDirection.xlShiftToLeft)
                            End If
                        Next c
                    Catch ex As System.Exception
                        modGlobalVar.msg("ERROR: agg columns", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try
                    'DELETE ROW 1 (XML root) 
                    Try
                        'DELETE ROW (XML ROOT)
                        objSheet.Rows(1).delete()
                        'SAVE CHANGES
                        oExcel.activeworkbook.Save()
                    Catch exc As System.Exception
                        modGlobalVar.msg("ERROR: delete ", exc.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try

                Catch ex As Exception
                Finally
                    oExcel.DisplayAlerts = True
                    ReleaseComObject(objSheet)
                    ReleaseComObject(wkbkXML)
                    wkbkXLS.Close()
                    ReleaseComObject(wkbkXLS)
                    oExcel.quit()
                    ReleaseComObject(oExcel)
                    oExcel = Nothing
                End Try
                ''============================
                System.Diagnostics.Process.Start(strExcelFile)
        End Select

CloseAll:
        Try
            strb = Nothing
            strbID = Nothing
        Catch ex As Exception
        End Try
        MouseDefault()

    End Sub

    'OPENS IN EXCEL
    Private Sub miCRGShare_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles miCRGShare.Click
        Dim tmpTable As New DataTable("tmpTable")
        Dim sql As New SqlClient.SqlCommand
        sql.Connection = sc
        sql.CommandType = CommandType.StoredProcedure   ' CommandType.Text
        sql.CommandText = "[RptsResourcesShare]"      '"SELECT * FROM zvwXMLTest"
        sql.Parameters.Add("@Which", SqlDbType.VarChar).Value = "Capital"
        sql.Parameters.Add("@Num", SqlDbType.VarChar).Value = modPopup.GetIDArray(Me.grdvwMain)

        If Not SCConnect() Then
            Exit Sub
        End If
        tmpTable.Load(sql.ExecuteReader)
        sc.Close()
        modPopup.DataToExcel(tmpTable)

    End Sub

    'DATA TO EXCEL
    Private Sub miExcel_Click(sender As System.Object, e As System.EventArgs) Handles miExcel.Click
        modPopup.DataToExcel(Me.grdvwMain)
    End Sub

#End Region 'reports

End Class

'