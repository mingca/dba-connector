Option Explicit On
Imports System
Imports System.Data.SqlClient
Imports System.Text


Public Class frmMainGrant
    Inherits System.Windows.Forms.Form

    Public isLoaded As Boolean = False  'used for combo boxes during maximize
    Dim strDGM, strDGM2, strDGM3 As String
    '  Public bChanged As Boolean = False
    Dim bCancelClose As Boolean
    Dim m As Double ' number of months from 1900 to month after determination date
    Dim tblContacts As New DataTable("tContacts")
    Dim tblCases As New DataTable("tCases")
    Dim enumProcess, enumResource, enumProgress As structHeadings
    Const LinkGrantPath As String = LinkedPath & "Grants\"
    Dim ppFile As New ContextMenu   'File goto
    Dim ehFile As EventHandler = AddressOf ehOpenFile
    Public GrantID As Integer 'attach file 
    '===== for Detail form  closing routines
    Dim ctlIdentify As Control 'fields for delete and messages
    Dim ctlNeutral As Control 'will never be invalidated
    Dim objHowClose As Short  'identify object calling close
    Dim mainTopic As String 'name of entity of this form - case, contact, etc
    Dim mainDS As DataSet 'for generic module calls like CloseDetailForm
    Dim maintbl As DataTable
    Dim mainDAdapt As SqlDataAdapter
    Dim mainBSrce As System.Windows.Forms.BindingSource
    Dim bDirty As Boolean 'grant due dates
    Public ThisID As String 'note change in type
    Public localOrgID As Integer
    Private tConcurrency As New dsMainGrant.MainGrantDataTable

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

#End Region 'Initialize

#Region " Windows Form Designer generated code "

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents StatusBarPanelID As System.Windows.Forms.StatusBarPanel
    Friend WithEvents lblAwardKey As System.Windows.Forms.Label
    Friend WithEvents Label53 As System.Windows.Forms.Label
    Friend WithEvents txtTypeofGrant As System.Windows.Forms.TextBox
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents Label47 As System.Windows.Forms.Label
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents CheckBox10 As System.Windows.Forms.CheckBox
    Friend WithEvents Label51 As System.Windows.Forms.Label
    Friend WithEvents miAttach As System.Windows.Forms.MenuItem
    Friend WithEvents miOpenFile As System.Windows.Forms.MenuItem
    Friend WithEvents btnViewFile As System.Windows.Forms.Button
    Friend WithEvents pnlJerri1 As System.Windows.Forms.Panel
    Friend WithEvents cboAgreementRecdStaffNum As InfoCtr.ComboBoxRelaxed
    Friend WithEvents DtNoticeMailed As InfoCtr.DateTextBox
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents cboNoticeMailedStaffNum As InfoCtr.ComboBoxRelaxed
    Friend WithEvents DsMainGrant1 As InfoCtr.dsMainGrant
    Friend WithEvents pgProgress As System.Windows.Forms.TabPage
    Friend WithEvents fldQtr3 As System.Windows.Forms.TextBox
    Friend WithEvents Label40 As System.Windows.Forms.Label
    Friend WithEvents Midpoint As System.Windows.Forms.Label
    Friend WithEvents fldQtr4 As System.Windows.Forms.TextBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label43 As System.Windows.Forms.Label
    Friend WithEvents btnExtension As System.Windows.Forms.Button
    Friend WithEvents DateTimePicker2 As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label44 As System.Windows.Forms.Label
    Friend WithEvents fldOrigDueDate As System.Windows.Forms.Label
    Friend WithEvents fldExtensions As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    '   Friend WithEvents btnExtend As System.Windows.Forms.Button
    ' Friend WithEvents DateTimePicker1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents OrigDeadline As System.Windows.Forms.Label
    Friend WithEvents Extensions As System.Windows.Forms.Label
    Friend WithEvents DtFinalRptReceived As InfoCtr.DateTextBox
    Friend WithEvents Label48 As System.Windows.Forms.Label
    Friend WithEvents cboRptRecdStaffNum As InfoCtr.ComboBoxRelaxed
    Friend WithEvents Label35 As System.Windows.Forms.Label
    Friend WithEvents txtCheckNum As System.Windows.Forms.TextBox
    Friend WithEvents cboChkMailedStaffNum As InfoCtr.ComboBoxRelaxed
    Friend WithEvents DtCheckMailed As InfoCtr.DateTextBox
    Friend WithEvents DtCheckRequested As InfoCtr.DateTextBox
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents DtRequestFinalReport As InfoCtr.DateTextBox
    Friend WithEvents DtRemindFinalReport As InfoCtr.DateTextBox
    Friend WithEvents DtSixMonthCall As InfoCtr.DateTextBox
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents cboMidpointCallStaffNum As InfoCtr.ComboBoxRelaxed
    Friend WithEvents cboRequestFinalStaffNum As InfoCtr.ComboBoxRelaxed
    Friend WithEvents cboRemindFinalStaffNum As InfoCtr.ComboBoxRelaxed
    Friend WithEvents cboFinalRecdStaffNum2 As InfoCtr.ComboBoxRelaxed
    Friend WithEvents fldQtr2 As System.Windows.Forms.TextBox
    Friend WithEvents Label38 As System.Windows.Forms.Label
    Friend WithEvents fldQtr1 As System.Windows.Forms.TextBox
    Friend WithEvents Label36 As System.Windows.Forms.Label
    Friend WithEvents btnHelp As System.Windows.Forms.Button
    Friend WithEvents fldOrgNum As System.Windows.Forms.TextBox
    Friend WithEvents CaseNum As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents DataGridTextBoxColumn5 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn6 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn7 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents fldMGI As System.Windows.Forms.TextBox
    Friend WithEvents Label37 As System.Windows.Forms.Label
    Friend WithEvents miMGI As System.Windows.Forms.MenuItem
    Friend WithEvents Label39 As System.Windows.Forms.Label
    Friend WithEvents CheckBox6 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox5 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox4 As System.Windows.Forms.CheckBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label41 As System.Windows.Forms.Label
    Friend WithEvents CheckBox7 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox8 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox9 As System.Windows.Forms.CheckBox
    Friend WithEvents lblQ2 As System.Windows.Forms.Label
    Friend WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents lblQ1 As System.Windows.Forms.Label
    Friend WithEvents Label46 As System.Windows.Forms.Label
    Friend WithEvents lblQ3 As System.Windows.Forms.Label
    Friend WithEvents Label50 As System.Windows.Forms.Label
    Friend WithEvents cboCloseStaffNum As InfoCtr.ComboBoxRelaxed
    Friend WithEvents lblQFinal As System.Windows.Forms.Label
    Friend WithEvents Label42 As System.Windows.Forms.Label
    Friend WithEvents chkEIN As System.Windows.Forms.CheckBox
    Friend WithEvents chkStory As System.Windows.Forms.CheckBox
    Friend WithEvents flagEIN As System.Windows.Forms.Label
    Friend WithEvents menuitem8 As System.Windows.Forms.MenuItem
    Friend WithEvents miDefinition As System.Windows.Forms.MenuItem
    Friend WithEvents miHelp As System.Windows.Forms.MenuItem
    Friend WithEvents ErrorProvider1 As System.Windows.Forms.ErrorProvider
    Friend WithEvents pnlJerri3 As System.Windows.Forms.Panel
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents miGotoOrg As System.Windows.Forms.MenuItem
    Friend WithEvents Label45 As System.Windows.Forms.Label
    Friend WithEvents cboClosure As InfoCtr.ComboBoxRelaxed
    Friend WithEvents DtClosed As InfoCtr.DateTextBox
    Friend WithEvents Label52 As System.Windows.Forms.Label
    Friend WithEvents Label49 As System.Windows.Forms.Label
    Friend WithEvents SqlDeleteCommand As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlInsertCommand As System.Data.SqlClient.SqlCommand
    Friend WithEvents MainGrantBindingSource As System.Windows.Forms.BindingSource

    Friend WithEvents fldGotoOrg As System.Windows.Forms.TextBox
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents cboCase As InfoCtr.ComboBoxRelaxed
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents fldGotoCase As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Private WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents pgProcess As System.Windows.Forms.TabPage
    Friend WithEvents pgResources As System.Windows.Forms.TabPage
    Friend WithEvents StatusBar1 As System.Windows.Forms.StatusBar
    Friend WithEvents StatusBarPanel1 As System.Windows.Forms.StatusBarPanel
    Friend WithEvents StatusBarPanel2 As System.Windows.Forms.StatusBarPanel
    Friend WithEvents cboFollowupStaffNum As InfoCtr.ComboBoxRelaxed
    Friend WithEvents cboProjDir As InfoCtr.ComboBoxRelaxed
    Friend WithEvents txtCongrAmnt As System.Windows.Forms.TextBox
    Friend WithEvents txtNotes As System.Windows.Forms.TextBox
    Friend WithEvents cboType As InfoCtr.ComboBoxRelaxed 'InfoCtr.ComboBoxRelaxed
    Friend WithEvents txtAmount As System.Windows.Forms.TextBox
    Friend WithEvents cboGrantStaffNum As InfoCtr.ComboBoxRelaxed
    Friend WithEvents cboSrClergy As InfoCtr.ComboBoxRelaxed
    Friend WithEvents txtBriefIssue As System.Windows.Forms.TextBox
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents cboNotifiedCallStaffNum As InfoCtr.ComboBoxRelaxed
    Friend WithEvents cboDeterminationStaffNum As InfoCtr.ComboBoxRelaxed
    Friend WithEvents cboFinalDraftRecdStaffNum As InfoCtr.ComboBoxRelaxed
    Friend WithEvents cboFollowupComplStaffNum As InfoCtr.ComboBoxRelaxed
    Friend WithEvents cboFirstDraftRecdStaffNum As InfoCtr.ComboBoxRelaxed
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Label34 As System.Windows.Forms.Label
    Friend WithEvents CheckBox1 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox3 As System.Windows.Forms.CheckBox
    Friend WithEvents DtFollowupReportComplete As InfoCtr.DateTextBox
    ' Friend WithEvents dtReminderSummaryReport As System.Windows.Forms.TextBox
    Friend WithEvents DtSignedAgreementRecd As InfoCtr.DateTextBox
    Friend WithEvents DtNotifyCall As InfoCtr.DateTextBox
    Friend WithEvents DtDetermination As InfoCtr.DateTextBox
    Friend WithEvents DtFinalDraftReceived As InfoCtr.DateTextBox
    Friend WithEvents DtFirstDraftReceived As InfoCtr.DateTextBox
    Friend WithEvents pnlApplication As System.Windows.Forms.Panel
    Friend WithEvents SummaryReportRecd As System.Windows.Forms.TextBox
    Friend WithEvents cboDetermination As InfoCtr.ComboBoxRelaxed
    Friend WithEvents DataView1 As System.Data.DataView
    Friend WithEvents MainMenu1 As System.Windows.Forms.MainMenu
    Friend WithEvents MenuItem1 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem2 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem3 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem4 As System.Windows.Forms.MenuItem
    Friend WithEvents miSave As System.Windows.Forms.MenuItem
    Friend WithEvents miCancel As System.Windows.Forms.MenuItem
    Friend WithEvents miDelete As System.Windows.Forms.MenuItem
    Friend WithEvents miClose As System.Windows.Forms.MenuItem
    Friend WithEvents miGotoCase As System.Windows.Forms.MenuItem
    Friend WithEvents miGotoResource As System.Windows.Forms.MenuItem
    Friend WithEvents grdMain As System.Windows.Forms.DataGrid
    Friend WithEvents daspGetRecommendations As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents SqlSelectCommand4 As System.Data.SqlClient.SqlCommand
    Friend WithEvents DsRecommend1 As InfoCtr.dsRecommend
    Friend WithEvents DataGridTableStyle1 As System.Windows.Forms.DataGridTableStyle
    Friend WithEvents DataGridTextBoxColumn1 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn2 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn3 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn4 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents daspMainGrant As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents SqlSelectCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlUpdateCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents btnNew As System.Windows.Forms.Button
    Friend WithEvents btnSaveExit As System.Windows.Forms.Button
    Friend WithEvents btnSend As System.Windows.Forms.Button
    Friend WithEvents Label6 As System.Windows.Forms.Label


    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMainGrant))
        Me.fldGotoOrg = New System.Windows.Forms.TextBox()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.DsMainGrant1 = New InfoCtr.dsMainGrant()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.txtTypeofGrant = New System.Windows.Forms.TextBox()
        Me.MainGrantBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.Label52 = New System.Windows.Forms.Label()
        Me.Label49 = New System.Windows.Forms.Label()
        Me.DtClosed = New InfoCtr.DateTextBox()
        Me.Label45 = New System.Windows.Forms.Label()
        Me.cboClosure = New InfoCtr.ComboBoxRelaxed()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.cboCloseStaffNum = New InfoCtr.ComboBoxRelaxed()
        Me.cboFollowupStaffNum = New InfoCtr.ComboBoxRelaxed()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.cboProjDir = New InfoCtr.ComboBoxRelaxed()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtCongrAmnt = New System.Windows.Forms.TextBox()
        Me.txtNotes = New System.Windows.Forms.TextBox()
        Me.txtAmount = New System.Windows.Forms.TextBox()
        Me.txtBriefIssue = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.cboCase = New InfoCtr.ComboBoxRelaxed()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.fldGotoCase = New System.Windows.Forms.Label()
        Me.cboGrantStaffNum = New InfoCtr.ComboBoxRelaxed()
        Me.cboSrClergy = New InfoCtr.ComboBoxRelaxed()
        Me.DataView1 = New System.Data.DataView()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.cboType = New InfoCtr.ComboBoxRelaxed()
        Me.pnlApplication = New System.Windows.Forms.Panel()
        Me.pnlJerri1 = New System.Windows.Forms.Panel()
        Me.cboChkMailedStaffNum = New InfoCtr.ComboBoxRelaxed()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.DtCheckMailed = New InfoCtr.DateTextBox()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.DtSignedAgreementRecd = New InfoCtr.DateTextBox()
        Me.cboNoticeMailedStaffNum = New InfoCtr.ComboBoxRelaxed()
        Me.Label35 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.DtNoticeMailed = New InfoCtr.DateTextBox()
        Me.txtCheckNum = New System.Windows.Forms.TextBox()
        Me.cboAgreementRecdStaffNum = New InfoCtr.ComboBoxRelaxed()
        Me.DtCheckRequested = New InfoCtr.DateTextBox()
        Me.cboDetermination = New InfoCtr.ComboBoxRelaxed()
        Me.CheckBox3 = New System.Windows.Forms.CheckBox()
        Me.CheckBox1 = New System.Windows.Forms.CheckBox()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.DtFollowupReportComplete = New InfoCtr.DateTextBox()
        Me.DtNotifyCall = New InfoCtr.DateTextBox()
        Me.DtDetermination = New InfoCtr.DateTextBox()
        Me.DtFinalDraftReceived = New InfoCtr.DateTextBox()
        Me.DtFirstDraftReceived = New InfoCtr.DateTextBox()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.cboFirstDraftRecdStaffNum = New InfoCtr.ComboBoxRelaxed()
        Me.cboFollowupComplStaffNum = New InfoCtr.ComboBoxRelaxed()
        Me.cboFinalDraftRecdStaffNum = New InfoCtr.ComboBoxRelaxed()
        Me.cboDeterminationStaffNum = New InfoCtr.ComboBoxRelaxed()
        Me.cboNotifiedCallStaffNum = New InfoCtr.ComboBoxRelaxed()
        Me.grdMain = New System.Windows.Forms.DataGrid()
        Me.DsRecommend1 = New InfoCtr.dsRecommend()
        Me.DataGridTableStyle1 = New System.Windows.Forms.DataGridTableStyle()
        Me.DataGridTextBoxColumn7 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn1 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn2 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn3 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn4 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn5 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn6 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.pgProcess = New System.Windows.Forms.TabPage()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Label47 = New System.Windows.Forms.Label()
        Me.CheckBox7 = New System.Windows.Forms.CheckBox()
        Me.CheckBox8 = New System.Windows.Forms.CheckBox()
        Me.CheckBox9 = New System.Windows.Forms.CheckBox()
        Me.lblQ2 = New System.Windows.Forms.Label()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.lblQ1 = New System.Windows.Forms.Label()
        Me.lblQ3 = New System.Windows.Forms.Label()
        Me.Label50 = New System.Windows.Forms.Label()
        Me.Label46 = New System.Windows.Forms.Label()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.CheckBox10 = New System.Windows.Forms.CheckBox()
        Me.pnlJerri3 = New System.Windows.Forms.Panel()
        Me.flagEIN = New System.Windows.Forms.Label()
        Me.chkStory = New System.Windows.Forms.CheckBox()
        Me.chkEIN = New System.Windows.Forms.CheckBox()
        Me.lblQFinal = New System.Windows.Forms.Label()
        Me.fldQtr4 = New System.Windows.Forms.TextBox()
        Me.Label42 = New System.Windows.Forms.Label()
        Me.DtFinalRptReceived = New InfoCtr.DateTextBox()
        Me.Label48 = New System.Windows.Forms.Label()
        Me.cboRptRecdStaffNum = New InfoCtr.ComboBoxRelaxed()
        Me.Label51 = New System.Windows.Forms.Label()
        Me.pgResources = New System.Windows.Forms.TabPage()
        Me.pgProgress = New System.Windows.Forms.TabPage()
        Me.CheckBox6 = New System.Windows.Forms.CheckBox()
        Me.CheckBox5 = New System.Windows.Forms.CheckBox()
        Me.CheckBox4 = New System.Windows.Forms.CheckBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label41 = New System.Windows.Forms.Label()
        Me.Label39 = New System.Windows.Forms.Label()
        Me.fldQtr2 = New System.Windows.Forms.TextBox()
        Me.Label38 = New System.Windows.Forms.Label()
        Me.fldQtr1 = New System.Windows.Forms.TextBox()
        Me.Label36 = New System.Windows.Forms.Label()
        Me.DtRequestFinalReport = New InfoCtr.DateTextBox()
        Me.fldQtr3 = New System.Windows.Forms.TextBox()
        Me.Label40 = New System.Windows.Forms.Label()
        Me.DtFinalReportRecd = New InfoCtr.DateTextBox()
        Me.DtRemindFinalReport = New InfoCtr.DateTextBox()
        Me.DtSixMonthCall = New InfoCtr.DateTextBox()
        Me.Midpoint = New System.Windows.Forms.Label()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label43 = New System.Windows.Forms.Label()
        Me.btnExtension = New System.Windows.Forms.Button()
        Me.Label44 = New System.Windows.Forms.Label()
        Me.fldOrigDueDate = New System.Windows.Forms.Label()
        Me.fldExtensions = New System.Windows.Forms.Label()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.cboMidpointCallStaffNum = New InfoCtr.ComboBoxRelaxed()
        Me.cboRequestFinalStaffNum = New InfoCtr.ComboBoxRelaxed()
        Me.cboRemindFinalStaffNum = New InfoCtr.ComboBoxRelaxed()
        Me.cboFinalRecdStaffNum2 = New InfoCtr.ComboBoxRelaxed()
        Me.StatusBar1 = New System.Windows.Forms.StatusBar()
        Me.StatusBarPanel1 = New System.Windows.Forms.StatusBarPanel()
        Me.StatusBarPanelID = New System.Windows.Forms.StatusBarPanel()
        Me.StatusBarPanel2 = New System.Windows.Forms.StatusBarPanel()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.fldMGI = New System.Windows.Forms.TextBox()
        Me.btnSaveExit = New System.Windows.Forms.Button()
        Me.btnNew = New System.Windows.Forms.Button()
        Me.btnSend = New System.Windows.Forms.Button()
        Me.btnViewFile = New System.Windows.Forms.Button()
        Me.MainMenu1 = New System.Windows.Forms.MainMenu(Me.components)
        Me.MenuItem1 = New System.Windows.Forms.MenuItem()
        Me.miAttach = New System.Windows.Forms.MenuItem()
        Me.miOpenFile = New System.Windows.Forms.MenuItem()
        Me.miClose = New System.Windows.Forms.MenuItem()
        Me.MenuItem2 = New System.Windows.Forms.MenuItem()
        Me.miSave = New System.Windows.Forms.MenuItem()
        Me.miCancel = New System.Windows.Forms.MenuItem()
        Me.miDelete = New System.Windows.Forms.MenuItem()
        Me.MenuItem3 = New System.Windows.Forms.MenuItem()
        Me.MenuItem4 = New System.Windows.Forms.MenuItem()
        Me.miGotoCase = New System.Windows.Forms.MenuItem()
        Me.miGotoResource = New System.Windows.Forms.MenuItem()
        Me.miMGI = New System.Windows.Forms.MenuItem()
        Me.miGotoOrg = New System.Windows.Forms.MenuItem()
        Me.menuitem8 = New System.Windows.Forms.MenuItem()
        Me.miDefinition = New System.Windows.Forms.MenuItem()
        Me.miHelp = New System.Windows.Forms.MenuItem()
        Me.daspGetRecommendations = New System.Data.SqlClient.SqlDataAdapter()
        Me.SqlSelectCommand4 = New System.Data.SqlClient.SqlCommand()
        Me.daspMainGrant = New System.Data.SqlClient.SqlDataAdapter()
        Me.SqlDeleteCommand = New System.Data.SqlClient.SqlCommand()
        Me.SqlInsertCommand = New System.Data.SqlClient.SqlCommand()
        Me.SqlSelectCommand1 = New System.Data.SqlClient.SqlCommand()
        Me.SqlUpdateCommand1 = New System.Data.SqlClient.SqlCommand()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.OrigDeadline = New System.Windows.Forms.Label()
        Me.Extensions = New System.Windows.Forms.Label()
        Me.btnHelp = New System.Windows.Forms.Button()
        Me.fldOrgNum = New System.Windows.Forms.TextBox()
        Me.CaseNum = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Label37 = New System.Windows.Forms.Label()
        Me.ErrorProvider1 = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.Label6 = New System.Windows.Forms.Label()
        Me.lblAwardKey = New System.Windows.Forms.Label()
        Me.Label53 = New System.Windows.Forms.Label()
        Me.miDelay = New System.Windows.Forms.MenuItem()
        CType(Me.DsMainGrant1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.MainGrantBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlJerri1.SuspendLayout()
        CType(Me.grdMain, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsRecommend1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabControl1.SuspendLayout()
        Me.pgProcess.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.pnlJerri3.SuspendLayout()
        Me.pgResources.SuspendLayout()
        Me.pgProgress.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.StatusBarPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.StatusBarPanelID, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.StatusBarPanel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'fldGotoOrg
        '
        Me.fldGotoOrg.BackColor = System.Drawing.SystemColors.Control
        Me.fldGotoOrg.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fldGotoOrg.ForeColor = System.Drawing.Color.ForestGreen
        Me.fldGotoOrg.Location = New System.Drawing.Point(384, 5)
        Me.fldGotoOrg.Multiline = True
        Me.fldGotoOrg.Name = "fldGotoOrg"
        Me.fldGotoOrg.ReadOnly = True
        Me.fldGotoOrg.Size = New System.Drawing.Size(325, 45)
        Me.fldGotoOrg.TabIndex = 221
        Me.fldGotoOrg.Text = "should be org name"
        Me.ToolTip1.SetToolTip(Me.fldGotoOrg, "Doubleclick to go to Organization Window")
        '
        'btnDelete
        '
        Me.btnDelete.BackColor = System.Drawing.SystemColors.Control
        Me.btnDelete.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Image = CType(resources.GetObject("btnDelete.Image"), System.Drawing.Image)
        Me.btnDelete.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnDelete.Location = New System.Drawing.Point(930, 542)
        Me.btnDelete.Margin = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(40, 35)
        Me.btnDelete.TabIndex = 220
        Me.btnDelete.TabStop = False
        Me.btnDelete.Text = "Delete"
        Me.btnDelete.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.btnDelete, "Delete this Grant")
        Me.btnDelete.UseVisualStyleBackColor = False
        Me.btnDelete.Visible = False
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label2.Location = New System.Drawing.Point(10, 10)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(122, 14)
        Me.Label2.TabIndex = 219
        Me.Label2.Text = "GRANT DETAIL"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'DsMainGrant1
        '
        Me.DsMainGrant1.DataSetName = "dsMainGrant"
        Me.DsMainGrant1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(250, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel1.Controls.Add(Me.txtTypeofGrant)
        Me.Panel1.Controls.Add(Me.Label52)
        Me.Panel1.Controls.Add(Me.Label49)
        Me.Panel1.Controls.Add(Me.DtClosed)
        Me.Panel1.Controls.Add(Me.Label45)
        Me.Panel1.Controls.Add(Me.cboClosure)
        Me.Panel1.Controls.Add(Me.Label13)
        Me.Panel1.Controls.Add(Me.cboCloseStaffNum)
        Me.Panel1.Controls.Add(Me.cboFollowupStaffNum)
        Me.Panel1.Controls.Add(Me.Label7)
        Me.Panel1.Controls.Add(Me.cboProjDir)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.txtCongrAmnt)
        Me.Panel1.Controls.Add(Me.txtNotes)
        Me.Panel1.Controls.Add(Me.txtAmount)
        Me.Panel1.Controls.Add(Me.txtBriefIssue)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.Label8)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.cboCase)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.fldGotoCase)
        Me.Panel1.Controls.Add(Me.cboGrantStaffNum)
        Me.Panel1.Controls.Add(Me.cboSrClergy)
        Me.Panel1.Controls.Add(Me.Label16)
        Me.Panel1.Controls.Add(Me.Label12)
        Me.Panel1.Location = New System.Drawing.Point(12, 57)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(360, 530)
        Me.Panel1.TabIndex = 0
        '
        'txtTypeofGrant
        '
        Me.txtTypeofGrant.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainGrantBindingSource, "TypeofGrant", True))
        Me.txtTypeofGrant.Location = New System.Drawing.Point(98, 8)
        Me.txtTypeofGrant.Name = "txtTypeofGrant"
        Me.txtTypeofGrant.ReadOnly = True
        Me.txtTypeofGrant.Size = New System.Drawing.Size(252, 20)
        Me.txtTypeofGrant.TabIndex = 303
        Me.txtTypeofGrant.TabStop = False
        '
        'MainGrantBindingSource
        '
        Me.MainGrantBindingSource.DataMember = "MainGrant"
        Me.MainGrantBindingSource.DataSource = Me.DsMainGrant1
        '
        'Label52
        '
        Me.Label52.BackColor = System.Drawing.Color.Transparent
        Me.Label52.Location = New System.Drawing.Point(222, 476)
        Me.Label52.Name = "Label52"
        Me.Label52.Size = New System.Drawing.Size(128, 19)
        Me.Label52.TabIndex = 302
        Me.Label52.Text = "Staff Closing Grant:"
        Me.Label52.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label49
        '
        Me.Label49.BackColor = System.Drawing.Color.Transparent
        Me.Label49.Location = New System.Drawing.Point(117, 477)
        Me.Label49.Name = "Label49"
        Me.Label49.Size = New System.Drawing.Size(72, 19)
        Me.Label49.TabIndex = 301
        Me.Label49.Text = "Close Date:"
        Me.Label49.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'DtClosed
        '
        Me.DtClosed.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainGrantBindingSource, "GrantCompletionDate", True))
        Me.DtClosed.Location = New System.Drawing.Point(117, 499)
        Me.DtClosed.Name = "DtClosed"
        Me.DtClosed.Size = New System.Drawing.Size(72, 20)
        Me.DtClosed.TabIndex = 81
        Me.DtClosed.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label45
        '
        Me.Label45.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label45.Location = New System.Drawing.Point(14, 473)
        Me.Label45.Name = "Label45"
        Me.Label45.Size = New System.Drawing.Size(80, 22)
        Me.Label45.TabIndex = 299
        Me.Label45.Text = "Grant Status:"
        Me.Label45.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cboClosure
        '
        Me.cboClosure.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboClosure.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboClosure.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainGrantBindingSource, "GrantCompletion", True))
        Me.cboClosure.Items.AddRange(New Object() {"Completed", "Withdrawn", "Abandoned", "Open"})
        Me.cboClosure.Location = New System.Drawing.Point(14, 498)
        Me.cboClosure.Name = "cboClosure"
        Me.cboClosure.RestrictContentToListItems = True
        Me.cboClosure.Size = New System.Drawing.Size(97, 21)
        Me.cboClosure.TabIndex = 80
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.Transparent
        Me.Label13.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label13.Location = New System.Drawing.Point(2, 128)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(89, 29)
        Me.Label13.TabIndex = 237
        Me.Label13.Tag = ""
        Me.Label13.Text = " Clergy Leader:"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cboCloseStaffNum
        '
        Me.cboCloseStaffNum.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboCloseStaffNum.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboCloseStaffNum.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.MainGrantBindingSource, "GrantCompleteWhoNum", True))
        Me.cboCloseStaffNum.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboCloseStaffNum.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cboCloseStaffNum.Location = New System.Drawing.Point(206, 498)
        Me.cboCloseStaffNum.Name = "cboCloseStaffNum"
        Me.cboCloseStaffNum.RestrictContentToListItems = True
        Me.cboCloseStaffNum.Size = New System.Drawing.Size(144, 21)
        Me.cboCloseStaffNum.TabIndex = 82
        Me.cboCloseStaffNum.Tag = "Complete Staff"
        '
        'cboFollowupStaffNum
        '
        Me.cboFollowupStaffNum.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboFollowupStaffNum.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboFollowupStaffNum.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.MainGrantBindingSource, "FollowupStaffNum", True))
        Me.cboFollowupStaffNum.Location = New System.Drawing.Point(96, 316)
        Me.cboFollowupStaffNum.Name = "cboFollowupStaffNum"
        Me.cboFollowupStaffNum.RestrictContentToListItems = True
        Me.cboFollowupStaffNum.Size = New System.Drawing.Size(254, 21)
        Me.cboFollowupStaffNum.TabIndex = 8
        Me.cboFollowupStaffNum.Tag = "Assigned Staff"
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Location = New System.Drawing.Point(12, 313)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(82, 31)
        Me.Label7.TabIndex = 223
        Me.Label7.Text = "Followup Staff Assigned:"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cboProjDir
        '
        Me.cboProjDir.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboProjDir.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboProjDir.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.MainGrantBindingSource, "PDNum", True))
        Me.cboProjDir.DisplayMember = "ContactStaff"
        Me.cboProjDir.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboProjDir.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cboProjDir.Location = New System.Drawing.Point(96, 160)
        Me.cboProjDir.Name = "cboProjDir"
        Me.cboProjDir.RestrictContentToListItems = True
        Me.cboProjDir.Size = New System.Drawing.Size(254, 21)
        Me.cboProjDir.TabIndex = 6
        Me.cboProjDir.Tag = "Project Dir"
        Me.cboProjDir.ValueMember = "ContactID"
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(11, 160)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(77, 20)
        Me.Label3.TabIndex = 221
        Me.Label3.Tag = ""
        Me.Label3.Text = "Project Dir:"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtCongrAmnt
        '
        Me.txtCongrAmnt.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainGrantBindingSource, "TotalAmount", True, System.Windows.Forms.DataSourceUpdateMode.OnValidation, Nothing, "N0"))
        Me.txtCongrAmnt.Location = New System.Drawing.Point(278, 38)
        Me.txtCongrAmnt.Name = "txtCongrAmnt"
        Me.txtCongrAmnt.Size = New System.Drawing.Size(72, 20)
        Me.txtCongrAmnt.TabIndex = 2
        Me.txtCongrAmnt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ToolTip1.SetToolTip(Me.txtCongrAmnt, "This field will only accept numbers and a period.")
        '
        'txtNotes
        '
        Me.txtNotes.AcceptsReturn = True
        Me.txtNotes.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainGrantBindingSource, "Notes", True))
        Me.txtNotes.Location = New System.Drawing.Point(96, 348)
        Me.txtNotes.Multiline = True
        Me.txtNotes.Name = "txtNotes"
        Me.txtNotes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtNotes.Size = New System.Drawing.Size(254, 108)
        Me.txtNotes.TabIndex = 10
        '
        'txtAmount
        '
        Me.txtAmount.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainGrantBindingSource, "Amount", True, System.Windows.Forms.DataSourceUpdateMode.OnValidation, Nothing, "N0"))
        Me.txtAmount.Location = New System.Drawing.Point(96, 38)
        Me.txtAmount.Name = "txtAmount"
        Me.txtAmount.Size = New System.Drawing.Size(64, 20)
        Me.txtAmount.TabIndex = 1
        Me.txtAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ToolTip1.SetToolTip(Me.txtAmount, "This field will only accept numbers and a period.")
        '
        'txtBriefIssue
        '
        Me.txtBriefIssue.AcceptsReturn = True
        Me.txtBriefIssue.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainGrantBindingSource, "Issue", True))
        Me.txtBriefIssue.Location = New System.Drawing.Point(96, 187)
        Me.txtBriefIssue.Multiline = True
        Me.txtBriefIssue.Name = "txtBriefIssue"
        Me.txtBriefIssue.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtBriefIssue.Size = New System.Drawing.Size(254, 120)
        Me.txtBriefIssue.TabIndex = 7
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Location = New System.Drawing.Point(157, 39)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(123, 20)
        Me.Label1.TabIndex = 219
        Me.Label1.Text = "Total Project Amount:"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Location = New System.Drawing.Point(11, 40)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(83, 19)
        Me.Label8.TabIndex = 217
        Me.Label8.Text = "Grant Amount:"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Location = New System.Drawing.Point(18, 8)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(80, 16)
        Me.Label5.TabIndex = 169
        Me.Label5.Text = "Type of Grant:"
        '
        'cboCase
        '
        Me.cboCase.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboCase.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboCase.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.MainGrantBindingSource, "CaseNum", True))
        Me.cboCase.DisplayMember = "CaseName"
        Me.cboCase.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboCase.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cboCase.Location = New System.Drawing.Point(96, 69)
        Me.cboCase.Name = "cboCase"
        Me.cboCase.RestrictContentToListItems = True
        Me.cboCase.Size = New System.Drawing.Size(254, 21)
        Me.cboCase.TabIndex = 3
        Me.cboCase.Tag = "Case"
        Me.cboCase.ValueMember = "CaseID"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Location = New System.Drawing.Point(46, 359)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(45, 14)
        Me.Label4.TabIndex = 166
        Me.Label4.Text = "Notes:"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'fldGotoCase
        '
        Me.fldGotoCase.BackColor = System.Drawing.Color.FromArgb(CType(CType(250, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.fldGotoCase.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.fldGotoCase.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fldGotoCase.ForeColor = System.Drawing.Color.ForestGreen
        Me.fldGotoCase.Location = New System.Drawing.Point(27, 69)
        Me.fldGotoCase.Name = "fldGotoCase"
        Me.fldGotoCase.Size = New System.Drawing.Size(61, 21)
        Me.fldGotoCase.TabIndex = 162
        Me.fldGotoCase.Tag = "Case"
        Me.fldGotoCase.Text = "Case:"
        Me.fldGotoCase.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ToolTip1.SetToolTip(Me.fldGotoCase, "Doubleclick to Goto Case Window")
        '
        'cboGrantStaffNum
        '
        Me.cboGrantStaffNum.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboGrantStaffNum.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboGrantStaffNum.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.MainGrantBindingSource, "GrantStaffNum", True))
        Me.cboGrantStaffNum.DisplayMember = "StaffName"
        Me.cboGrantStaffNum.Location = New System.Drawing.Point(96, 99)
        Me.cboGrantStaffNum.Name = "cboGrantStaffNum"
        Me.cboGrantStaffNum.RestrictContentToListItems = True
        Me.cboGrantStaffNum.Size = New System.Drawing.Size(254, 21)
        Me.cboGrantStaffNum.TabIndex = 4
        Me.cboGrantStaffNum.Tag = "Grant Staff"
        Me.cboGrantStaffNum.ValueMember = "StaffID"
        '
        'cboSrClergy
        '
        Me.cboSrClergy.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboSrClergy.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboSrClergy.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.MainGrantBindingSource, "SrClergyNum", True))
        Me.cboSrClergy.DataSource = Me.DataView1
        Me.cboSrClergy.DisplayMember = "ContactStaff"
        Me.cboSrClergy.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboSrClergy.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cboSrClergy.Location = New System.Drawing.Point(96, 131)
        Me.cboSrClergy.Name = "cboSrClergy"
        Me.cboSrClergy.RestrictContentToListItems = True
        Me.cboSrClergy.Size = New System.Drawing.Size(254, 21)
        Me.cboSrClergy.TabIndex = 5
        Me.cboSrClergy.Tag = "Clergy Leader"
        Me.cboSrClergy.ValueMember = "ContactID"
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.Color.Transparent
        Me.Label16.Location = New System.Drawing.Point(24, 186)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(64, 50)
        Me.Label16.TabIndex = 150
        Me.Label16.Text = "Brief Grant Issue:"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.Transparent
        Me.Label12.Location = New System.Drawing.Point(8, 90)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(83, 38)
        Me.Label12.TabIndex = 148
        Me.Label12.Text = "Staff in charge of Grant:"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cboType
        '
        Me.cboType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboType.BackColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.cboType.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainGrantBindingSource, "TypeofGrant", True))
        Me.cboType.Location = New System.Drawing.Point(318, 8)
        Me.cboType.Name = "cboType"
        Me.cboType.RestrictContentToListItems = True
        Me.cboType.Size = New System.Drawing.Size(30, 21)
        Me.cboType.TabIndex = 0
        Me.cboType.TabStop = False
        Me.cboType.Tag = "Type of Grant"
        Me.cboType.Visible = False
        '
        'pnlApplication
        '
        Me.pnlApplication.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.pnlApplication.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pnlApplication.Location = New System.Drawing.Point(3, 3)
        Me.pnlApplication.Name = "pnlApplication"
        Me.pnlApplication.Size = New System.Drawing.Size(131, 427)
        Me.pnlApplication.TabIndex = 229
        '
        'pnlJerri1
        '
        Me.pnlJerri1.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.pnlJerri1.Controls.Add(Me.cboChkMailedStaffNum)
        Me.pnlJerri1.Controls.Add(Me.Label23)
        Me.pnlJerri1.Controls.Add(Me.DtCheckMailed)
        Me.pnlJerri1.Controls.Add(Me.Label24)
        Me.pnlJerri1.Controls.Add(Me.DtSignedAgreementRecd)
        Me.pnlJerri1.Controls.Add(Me.cboNoticeMailedStaffNum)
        Me.pnlJerri1.Controls.Add(Me.Label35)
        Me.pnlJerri1.Controls.Add(Me.Label22)
        Me.pnlJerri1.Controls.Add(Me.Label26)
        Me.pnlJerri1.Controls.Add(Me.DtNoticeMailed)
        Me.pnlJerri1.Controls.Add(Me.txtCheckNum)
        Me.pnlJerri1.Controls.Add(Me.cboAgreementRecdStaffNum)
        Me.pnlJerri1.Controls.Add(Me.DtCheckRequested)
        Me.pnlJerri1.Cursor = System.Windows.Forms.Cursors.Default
        Me.pnlJerri1.Location = New System.Drawing.Point(33, 230)
        Me.pnlJerri1.Name = "pnlJerri1"
        Me.pnlJerri1.Size = New System.Drawing.Size(464, 122)
        Me.pnlJerri1.TabIndex = 40
        '
        'cboChkMailedStaffNum
        '
        Me.cboChkMailedStaffNum.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboChkMailedStaffNum.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboChkMailedStaffNum.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.MainGrantBindingSource, "CheckMailedWhoNum", True))
        Me.cboChkMailedStaffNum.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboChkMailedStaffNum.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cboChkMailedStaffNum.Location = New System.Drawing.Point(282, 92)
        Me.cboChkMailedStaffNum.Name = "cboChkMailedStaffNum"
        Me.cboChkMailedStaffNum.RestrictContentToListItems = True
        Me.cboChkMailedStaffNum.Size = New System.Drawing.Size(171, 21)
        Me.cboChkMailedStaffNum.TabIndex = 7
        Me.cboChkMailedStaffNum.Tag = "Check Staff"
        '
        'Label23
        '
        Me.Label23.BackColor = System.Drawing.Color.Transparent
        Me.Label23.Location = New System.Drawing.Point(11, 36)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(169, 19)
        Me.Label23.TabIndex = 229
        Me.Label23.Text = "Signed Agreement Received:"
        Me.Label23.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'DtCheckMailed
        '
        Me.DtCheckMailed.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainGrantBindingSource, "CheckMailedDate", True))
        Me.DtCheckMailed.Location = New System.Drawing.Point(188, 93)
        Me.DtCheckMailed.Name = "DtCheckMailed"
        Me.DtCheckMailed.Size = New System.Drawing.Size(72, 20)
        Me.DtCheckMailed.TabIndex = 6
        Me.DtCheckMailed.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label24
        '
        Me.Label24.BackColor = System.Drawing.Color.Transparent
        Me.Label24.Location = New System.Drawing.Point(66, 94)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(114, 19)
        Me.Label24.TabIndex = 264
        Me.Label24.Text = "Check Forwarded:"
        Me.Label24.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'DtSignedAgreementRecd
        '
        Me.DtSignedAgreementRecd.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainGrantBindingSource, "SignedAgreementRecdDate", True))
        Me.DtSignedAgreementRecd.Location = New System.Drawing.Point(188, 35)
        Me.DtSignedAgreementRecd.Name = "DtSignedAgreementRecd"
        Me.DtSignedAgreementRecd.Size = New System.Drawing.Size(72, 20)
        Me.DtSignedAgreementRecd.TabIndex = 2
        Me.DtSignedAgreementRecd.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'cboNoticeMailedStaffNum
        '
        Me.cboNoticeMailedStaffNum.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboNoticeMailedStaffNum.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboNoticeMailedStaffNum.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboNoticeMailedStaffNum.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.MainGrantBindingSource, "NoticeMailedWhoNum", True))
        Me.cboNoticeMailedStaffNum.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboNoticeMailedStaffNum.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cboNoticeMailedStaffNum.Location = New System.Drawing.Point(282, 6)
        Me.cboNoticeMailedStaffNum.Name = "cboNoticeMailedStaffNum"
        Me.cboNoticeMailedStaffNum.RestrictContentToListItems = True
        Me.cboNoticeMailedStaffNum.Size = New System.Drawing.Size(171, 21)
        Me.cboNoticeMailedStaffNum.TabIndex = 1
        Me.cboNoticeMailedStaffNum.Tag = "Notice Staff"
        '
        'Label35
        '
        Me.Label35.BackColor = System.Drawing.Color.Transparent
        Me.Label35.Location = New System.Drawing.Point(285, 66)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(56, 16)
        Me.Label35.TabIndex = 269
        Me.Label35.Text = "Check #"
        '
        'Label22
        '
        Me.Label22.BackColor = System.Drawing.Color.Transparent
        Me.Label22.Location = New System.Drawing.Point(76, 10)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(104, 16)
        Me.Label22.TabIndex = 238
        Me.Label22.Text = "Notice Mailed:"
        Me.Label22.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label26
        '
        Me.Label26.BackColor = System.Drawing.Color.Transparent
        Me.Label26.Location = New System.Drawing.Point(36, 70)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(144, 18)
        Me.Label26.TabIndex = 266
        Me.Label26.Text = "Check Requisitioned:"
        Me.Label26.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'DtNoticeMailed
        '
        Me.DtNoticeMailed.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainGrantBindingSource, "NoticeMailedDate", True))
        Me.DtNoticeMailed.Location = New System.Drawing.Point(188, 7)
        Me.DtNoticeMailed.Name = "DtNoticeMailed"
        Me.DtNoticeMailed.Size = New System.Drawing.Size(72, 20)
        Me.DtNoticeMailed.TabIndex = 0
        Me.DtNoticeMailed.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtCheckNum
        '
        Me.txtCheckNum.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainGrantBindingSource, "CheckNum", True))
        Me.txtCheckNum.Location = New System.Drawing.Point(347, 63)
        Me.txtCheckNum.Name = "txtCheckNum"
        Me.txtCheckNum.Size = New System.Drawing.Size(109, 20)
        Me.txtCheckNum.TabIndex = 5
        Me.txtCheckNum.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ToolTip1.SetToolTip(Me.txtCheckNum, "This field will only accept numbers and a period.")
        '
        'cboAgreementRecdStaffNum
        '
        Me.cboAgreementRecdStaffNum.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboAgreementRecdStaffNum.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboAgreementRecdStaffNum.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboAgreementRecdStaffNum.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.MainGrantBindingSource, "SignedAgreementRecdWhoNum", True))
        Me.cboAgreementRecdStaffNum.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboAgreementRecdStaffNum.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cboAgreementRecdStaffNum.Location = New System.Drawing.Point(282, 35)
        Me.cboAgreementRecdStaffNum.Name = "cboAgreementRecdStaffNum"
        Me.cboAgreementRecdStaffNum.RestrictContentToListItems = True
        Me.cboAgreementRecdStaffNum.Size = New System.Drawing.Size(171, 21)
        Me.cboAgreementRecdStaffNum.TabIndex = 3
        Me.cboAgreementRecdStaffNum.Tag = "Agreement Staff"
        '
        'DtCheckRequested
        '
        Me.DtCheckRequested.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainGrantBindingSource, "CheckRequestedDate", True))
        Me.DtCheckRequested.Location = New System.Drawing.Point(188, 64)
        Me.DtCheckRequested.Name = "DtCheckRequested"
        Me.DtCheckRequested.Size = New System.Drawing.Size(72, 20)
        Me.DtCheckRequested.TabIndex = 4
        Me.DtCheckRequested.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'cboDetermination
        '
        Me.cboDetermination.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboDetermination.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboDetermination.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainGrantBindingSource, "Determination", True))
        Me.cboDetermination.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboDetermination.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cboDetermination.Items.AddRange(New Object() {"Approved", "Denied", "Withdrawn", "Rescinded"})
        Me.cboDetermination.Location = New System.Drawing.Point(79, 7)
        Me.cboDetermination.Name = "cboDetermination"
        Me.cboDetermination.RestrictContentToListItems = True
        Me.cboDetermination.Size = New System.Drawing.Size(105, 21)
        Me.cboDetermination.TabIndex = 31
        '
        'CheckBox3
        '
        Me.CheckBox3.DataBindings.Add(New System.Windows.Forms.Binding("Checked", Me.MainGrantBindingSource, "Received501c3", True))
        Me.CheckBox3.Location = New System.Drawing.Point(395, 4)
        Me.CheckBox3.Name = "CheckBox3"
        Me.CheckBox3.Size = New System.Drawing.Size(57, 17)
        Me.CheckBox3.TabIndex = 22
        Me.CheckBox3.Text = "501c3"
        '
        'CheckBox1
        '
        Me.CheckBox1.DataBindings.Add(New System.Windows.Forms.Binding("Checked", Me.MainGrantBindingSource, "ReceivedBudget", True))
        Me.CheckBox1.Location = New System.Drawing.Point(190, 4)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(164, 17)
        Me.CheckBox1.TabIndex = 20
        Me.CheckBox1.Text = "Current Year's Budget"
        '
        'Label34
        '
        Me.Label34.BackColor = System.Drawing.Color.Transparent
        Me.Label34.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label34.Location = New System.Drawing.Point(376, 7)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(49, 11)
        Me.Label34.TabIndex = 247
        Me.Label34.Text = "Staff"
        '
        'Label33
        '
        Me.Label33.BackColor = System.Drawing.Color.Transparent
        Me.Label33.Location = New System.Drawing.Point(103, 52)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(112, 16)
        Me.Label33.TabIndex = 246
        Me.Label33.Text = "Final Draft Received:"
        Me.Label33.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label32
        '
        Me.Label32.BackColor = System.Drawing.Color.Transparent
        Me.Label32.Location = New System.Drawing.Point(80, 23)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(135, 17)
        Me.Label32.TabIndex = 245
        Me.Label32.Text = "First Draft Received:"
        Me.Label32.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'DtFollowupReportComplete
        '
        Me.DtFollowupReportComplete.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainGrantBindingSource, "FollowupReportCompleteDate", True))
        Me.DtFollowupReportComplete.Location = New System.Drawing.Point(223, 471)
        Me.DtFollowupReportComplete.Name = "DtFollowupReportComplete"
        Me.DtFollowupReportComplete.Size = New System.Drawing.Size(72, 20)
        Me.DtFollowupReportComplete.TabIndex = 77
        Me.DtFollowupReportComplete.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'DtNotifyCall
        '
        Me.DtNotifyCall.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainGrantBindingSource, "NotifyCallDate", True))
        Me.DtNotifyCall.Location = New System.Drawing.Point(221, 202)
        Me.DtNotifyCall.Name = "DtNotifyCall"
        Me.DtNotifyCall.Size = New System.Drawing.Size(72, 20)
        Me.DtNotifyCall.TabIndex = 37
        Me.DtNotifyCall.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'DtDetermination
        '
        Me.DtDetermination.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.DtDetermination.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainGrantBindingSource, "DeterminationDate", True))
        Me.DtDetermination.Location = New System.Drawing.Point(188, 7)
        Me.DtDetermination.Name = "DtDetermination"
        Me.DtDetermination.Size = New System.Drawing.Size(72, 20)
        Me.DtDetermination.TabIndex = 32
        Me.DtDetermination.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ToolTip1.SetToolTip(Me.DtDetermination, "This date generates the quarterly report due dates.")
        '
        'DtFinalDraftReceived
        '
        Me.DtFinalDraftReceived.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainGrantBindingSource, "FinalDraftReceivedDate", True))
        Me.DtFinalDraftReceived.Location = New System.Drawing.Point(223, 51)
        Me.DtFinalDraftReceived.Name = "DtFinalDraftReceived"
        Me.DtFinalDraftReceived.Size = New System.Drawing.Size(72, 20)
        Me.DtFinalDraftReceived.TabIndex = 19
        Me.DtFinalDraftReceived.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'DtFirstDraftReceived
        '
        Me.DtFirstDraftReceived.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainGrantBindingSource, "FirstDraftReceivedDate", True))
        Me.DtFirstDraftReceived.Location = New System.Drawing.Point(223, 21)
        Me.DtFirstDraftReceived.Name = "DtFirstDraftReceived"
        Me.DtFirstDraftReceived.Size = New System.Drawing.Size(72, 20)
        Me.DtFirstDraftReceived.TabIndex = 15
        Me.DtFirstDraftReceived.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label29
        '
        Me.Label29.BackColor = System.Drawing.Color.Transparent
        Me.Label29.Location = New System.Drawing.Point(63, 471)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(152, 16)
        Me.Label29.TabIndex = 241
        Me.Label29.Text = "ICC Followup Rpt. Complete:"
        Me.Label29.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label21
        '
        Me.Label21.BackColor = System.Drawing.Color.Transparent
        Me.Label21.Location = New System.Drawing.Point(57, 204)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(156, 16)
        Me.Label21.TabIndex = 225
        Me.Label21.Text = "Phone Notification Made:"
        Me.Label21.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label20
        '
        Me.Label20.BackColor = System.Drawing.Color.Transparent
        Me.Label20.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.Location = New System.Drawing.Point(237, 7)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(45, 16)
        Me.Label20.TabIndex = 223
        Me.Label20.Text = "Date"
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.Color.Transparent
        Me.Label15.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(3, 12)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(79, 23)
        Me.Label15.TabIndex = 221
        Me.Label15.Text = "Determination"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Location = New System.Drawing.Point(6, 9)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(173, 25)
        Me.Label10.TabIndex = 219
        Me.Label10.Text = "Supporting Documents Received:"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cboFirstDraftRecdStaffNum
        '
        Me.cboFirstDraftRecdStaffNum.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboFirstDraftRecdStaffNum.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboFirstDraftRecdStaffNum.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.MainGrantBindingSource, "FirstDraftReceivedWhoNum", True))
        Me.cboFirstDraftRecdStaffNum.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboFirstDraftRecdStaffNum.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cboFirstDraftRecdStaffNum.Location = New System.Drawing.Point(317, 19)
        Me.cboFirstDraftRecdStaffNum.Name = "cboFirstDraftRecdStaffNum"
        Me.cboFirstDraftRecdStaffNum.RestrictContentToListItems = True
        Me.cboFirstDraftRecdStaffNum.Size = New System.Drawing.Size(171, 21)
        Me.cboFirstDraftRecdStaffNum.TabIndex = 16
        Me.cboFirstDraftRecdStaffNum.Tag = "FirstDraft Staff"
        '
        'cboFollowupComplStaffNum
        '
        Me.cboFollowupComplStaffNum.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboFollowupComplStaffNum.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboFollowupComplStaffNum.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.MainGrantBindingSource, "FollowupReportCompleteWhoNum", True))
        Me.cboFollowupComplStaffNum.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboFollowupComplStaffNum.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cboFollowupComplStaffNum.Location = New System.Drawing.Point(309, 470)
        Me.cboFollowupComplStaffNum.Name = "cboFollowupComplStaffNum"
        Me.cboFollowupComplStaffNum.RestrictContentToListItems = True
        Me.cboFollowupComplStaffNum.Size = New System.Drawing.Size(171, 21)
        Me.cboFollowupComplStaffNum.TabIndex = 78
        Me.cboFollowupComplStaffNum.Tag = "Followup Staff"
        '
        'cboFinalDraftRecdStaffNum
        '
        Me.cboFinalDraftRecdStaffNum.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboFinalDraftRecdStaffNum.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboFinalDraftRecdStaffNum.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.MainGrantBindingSource, "FinalDraftReceivedWhoNum", True))
        Me.cboFinalDraftRecdStaffNum.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboFinalDraftRecdStaffNum.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cboFinalDraftRecdStaffNum.Location = New System.Drawing.Point(317, 51)
        Me.cboFinalDraftRecdStaffNum.Name = "cboFinalDraftRecdStaffNum"
        Me.cboFinalDraftRecdStaffNum.RestrictContentToListItems = True
        Me.cboFinalDraftRecdStaffNum.Size = New System.Drawing.Size(171, 21)
        Me.cboFinalDraftRecdStaffNum.TabIndex = 20
        Me.cboFinalDraftRecdStaffNum.Tag = "Final Draft Staff"
        '
        'cboDeterminationStaffNum
        '
        Me.cboDeterminationStaffNum.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboDeterminationStaffNum.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboDeterminationStaffNum.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.MainGrantBindingSource, "DeterminationWhoNum", True))
        Me.cboDeterminationStaffNum.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboDeterminationStaffNum.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cboDeterminationStaffNum.Location = New System.Drawing.Point(282, 7)
        Me.cboDeterminationStaffNum.Name = "cboDeterminationStaffNum"
        Me.cboDeterminationStaffNum.RestrictContentToListItems = True
        Me.cboDeterminationStaffNum.Size = New System.Drawing.Size(171, 21)
        Me.cboDeterminationStaffNum.TabIndex = 33
        Me.cboDeterminationStaffNum.Tag = "Determination Staff"
        '
        'cboNotifiedCallStaffNum
        '
        Me.cboNotifiedCallStaffNum.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboNotifiedCallStaffNum.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboNotifiedCallStaffNum.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.MainGrantBindingSource, "NotifyCallWhoNum", True))
        Me.cboNotifiedCallStaffNum.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboNotifiedCallStaffNum.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cboNotifiedCallStaffNum.Location = New System.Drawing.Point(315, 201)
        Me.cboNotifiedCallStaffNum.Name = "cboNotifiedCallStaffNum"
        Me.cboNotifiedCallStaffNum.RestrictContentToListItems = True
        Me.cboNotifiedCallStaffNum.Size = New System.Drawing.Size(171, 21)
        Me.cboNotifiedCallStaffNum.TabIndex = 38
        Me.cboNotifiedCallStaffNum.Tag = "Phone Staff"
        '
        'grdMain
        '
        Me.grdMain.DataMember = "tblResourceRecommend"
        Me.grdMain.DataSource = Me.DsRecommend1
        Me.grdMain.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.grdMain.Location = New System.Drawing.Point(3, 3)
        Me.grdMain.Name = "grdMain"
        Me.grdMain.ReadOnly = True
        Me.grdMain.RowHeaderWidth = 15
        Me.grdMain.Size = New System.Drawing.Size(521, 498)
        Me.grdMain.TabIndex = 0
        Me.grdMain.TableStyles.AddRange(New System.Windows.Forms.DataGridTableStyle() {Me.DataGridTableStyle1})
        '
        'DsRecommend1
        '
        Me.DsRecommend1.DataSetName = "dsRecommend"
        Me.DsRecommend1.Locale = New System.Globalization.CultureInfo("en-US")
        Me.DsRecommend1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'DataGridTableStyle1
        '
        Me.DataGridTableStyle1.DataGrid = Me.grdMain
        Me.DataGridTableStyle1.ForeColor = System.Drawing.Color.DarkGreen
        Me.DataGridTableStyle1.GridColumnStyles.AddRange(New System.Windows.Forms.DataGridColumnStyle() {Me.DataGridTextBoxColumn7, Me.DataGridTextBoxColumn1, Me.DataGridTextBoxColumn2, Me.DataGridTextBoxColumn3, Me.DataGridTextBoxColumn4, Me.DataGridTextBoxColumn5, Me.DataGridTextBoxColumn6})
        Me.DataGridTableStyle1.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.DataGridTableStyle1.MappingName = "tblResourceRecommend"
        Me.DataGridTableStyle1.RowHeaderWidth = 15
        '
        'DataGridTextBoxColumn7
        '
        Me.DataGridTextBoxColumn7.Format = ""
        Me.DataGridTextBoxColumn7.FormatInfo = Nothing
        Me.DataGridTextBoxColumn7.HeaderText = "RecommendID"
        Me.DataGridTextBoxColumn7.MappingName = "RecommendID"
        Me.DataGridTextBoxColumn7.Width = 0
        '
        'DataGridTextBoxColumn1
        '
        Me.DataGridTextBoxColumn1.Format = ""
        Me.DataGridTextBoxColumn1.FormatInfo = Nothing
        Me.DataGridTextBoxColumn1.HeaderText = "ResourceNum"
        Me.DataGridTextBoxColumn1.MappingName = "ResourceNum"
        Me.DataGridTextBoxColumn1.Width = 0
        '
        'DataGridTextBoxColumn2
        '
        Me.DataGridTextBoxColumn2.Format = ""
        Me.DataGridTextBoxColumn2.FormatInfo = Nothing
        Me.DataGridTextBoxColumn2.HeaderText = "Resource"
        Me.DataGridTextBoxColumn2.MappingName = "ResourceName"
        Me.DataGridTextBoxColumn2.Width = 175
        '
        'DataGridTextBoxColumn3
        '
        Me.DataGridTextBoxColumn3.Format = ""
        Me.DataGridTextBoxColumn3.FormatInfo = Nothing
        Me.DataGridTextBoxColumn3.HeaderText = "Type"
        Me.DataGridTextBoxColumn3.MappingName = "Type"
        Me.DataGridTextBoxColumn3.Width = 75
        '
        'DataGridTextBoxColumn4
        '
        Me.DataGridTextBoxColumn4.Format = ""
        Me.DataGridTextBoxColumn4.FormatInfo = Nothing
        Me.DataGridTextBoxColumn4.HeaderText = "Active"
        Me.DataGridTextBoxColumn4.MappingName = "Active"
        Me.DataGridTextBoxColumn4.Width = 75
        '
        'DataGridTextBoxColumn5
        '
        Me.DataGridTextBoxColumn5.Format = ""
        Me.DataGridTextBoxColumn5.FormatInfo = Nothing
        Me.DataGridTextBoxColumn5.HeaderText = "Funded"
        Me.DataGridTextBoxColumn5.MappingName = "GrantNum"
        Me.DataGridTextBoxColumn5.Width = 75
        '
        'DataGridTextBoxColumn6
        '
        Me.DataGridTextBoxColumn6.Format = ""
        Me.DataGridTextBoxColumn6.FormatInfo = Nothing
        Me.DataGridTextBoxColumn6.HeaderText = "Used"
        Me.DataGridTextBoxColumn6.MappingName = "Used"
        Me.DataGridTextBoxColumn6.Width = 50
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.pgProcess)
        Me.TabControl1.Controls.Add(Me.pgResources)
        Me.TabControl1.Controls.Add(Me.pgProgress)
        Me.TabControl1.Location = New System.Drawing.Point(384, 56)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(535, 530)
        Me.TabControl1.TabIndex = 75
        '
        'pgProcess
        '
        Me.pgProcess.Controls.Add(Me.Panel4)
        Me.pgProcess.Controls.Add(Me.Panel3)
        Me.pgProcess.Controls.Add(Me.pnlJerri3)
        Me.pgProcess.Controls.Add(Me.flagEIN)
        Me.pgProcess.Controls.Add(Me.chkStory)
        Me.pgProcess.Controls.Add(Me.chkEIN)
        Me.pgProcess.Controls.Add(Me.lblQFinal)
        Me.pgProcess.Controls.Add(Me.Label42)
        Me.pgProcess.Controls.Add(Me.DtFinalRptReceived)
        Me.pgProcess.Controls.Add(Me.Label48)
        Me.pgProcess.Controls.Add(Me.cboRptRecdStaffNum)
        Me.pgProcess.Controls.Add(Me.pnlJerri1)
        Me.pgProcess.Controls.Add(Me.Label34)
        Me.pgProcess.Controls.Add(Me.Label33)
        Me.pgProcess.Controls.Add(Me.Label32)
        Me.pgProcess.Controls.Add(Me.DtFollowupReportComplete)
        Me.pgProcess.Controls.Add(Me.DtNotifyCall)
        Me.pgProcess.Controls.Add(Me.DtFinalDraftReceived)
        Me.pgProcess.Controls.Add(Me.DtFirstDraftReceived)
        Me.pgProcess.Controls.Add(Me.Label29)
        Me.pgProcess.Controls.Add(Me.Label21)
        Me.pgProcess.Controls.Add(Me.Label20)
        Me.pgProcess.Controls.Add(Me.cboFirstDraftRecdStaffNum)
        Me.pgProcess.Controls.Add(Me.cboFollowupComplStaffNum)
        Me.pgProcess.Controls.Add(Me.cboFinalDraftRecdStaffNum)
        Me.pgProcess.Controls.Add(Me.cboNotifiedCallStaffNum)
        Me.pgProcess.Controls.Add(Me.Label51)
        Me.pgProcess.Location = New System.Drawing.Point(4, 22)
        Me.pgProcess.Name = "pgProcess"
        Me.pgProcess.Size = New System.Drawing.Size(527, 504)
        Me.pgProcess.TabIndex = 0
        Me.pgProcess.Tag = "PROCESS"
        Me.pgProcess.Text = "   PROCESS   "
        Me.pgProcess.UseVisualStyleBackColor = True
        '
        'Panel4
        '
        Me.Panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel4.Controls.Add(Me.Label47)
        Me.Panel4.Controls.Add(Me.CheckBox7)
        Me.Panel4.Controls.Add(Me.CheckBox8)
        Me.Panel4.Controls.Add(Me.CheckBox9)
        Me.Panel4.Controls.Add(Me.lblQ2)
        Me.Panel4.Controls.Add(Me.Label31)
        Me.Panel4.Controls.Add(Me.lblQ1)
        Me.Panel4.Controls.Add(Me.lblQ3)
        Me.Panel4.Controls.Add(Me.Label50)
        Me.Panel4.Controls.Add(Me.Label46)
        Me.Panel4.Location = New System.Drawing.Point(33, 360)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(464, 73)
        Me.Panel4.TabIndex = 50
        '
        'Label47
        '
        Me.Label47.BackColor = System.Drawing.Color.Transparent
        Me.Label47.Location = New System.Drawing.Point(4, 17)
        Me.Label47.Name = "Label47"
        Me.Label47.Size = New System.Drawing.Size(77, 40)
        Me.Label47.TabIndex = 294
        Me.Label47.Text = "Quarterly Reports"
        Me.Label47.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'CheckBox7
        '
        Me.CheckBox7.AutoSize = True
        Me.CheckBox7.DataBindings.Add(New System.Windows.Forms.Binding("Checked", Me.MainGrantBindingSource, "Quarter3Recd", True))
        Me.CheckBox7.Location = New System.Drawing.Point(299, 49)
        Me.CheckBox7.Name = "CheckBox7"
        Me.CheckBox7.Size = New System.Drawing.Size(72, 17)
        Me.CheckBox7.TabIndex = 56
        Me.CheckBox7.Text = "Received"
        Me.CheckBox7.UseVisualStyleBackColor = True
        '
        'CheckBox8
        '
        Me.CheckBox8.AutoSize = True
        Me.CheckBox8.DataBindings.Add(New System.Windows.Forms.Binding("Checked", Me.MainGrantBindingSource, "Quarter2Recd", True))
        Me.CheckBox8.Location = New System.Drawing.Point(299, 28)
        Me.CheckBox8.Name = "CheckBox8"
        Me.CheckBox8.Size = New System.Drawing.Size(72, 17)
        Me.CheckBox8.TabIndex = 55
        Me.CheckBox8.Text = "Received"
        Me.CheckBox8.UseVisualStyleBackColor = True
        '
        'CheckBox9
        '
        Me.CheckBox9.AutoSize = True
        Me.CheckBox9.DataBindings.Add(New System.Windows.Forms.Binding("Checked", Me.MainGrantBindingSource, "Quarter1Recd", True))
        Me.CheckBox9.Location = New System.Drawing.Point(299, 7)
        Me.CheckBox9.Name = "CheckBox9"
        Me.CheckBox9.Size = New System.Drawing.Size(72, 17)
        Me.CheckBox9.TabIndex = 54
        Me.CheckBox9.Text = "Received"
        Me.CheckBox9.UseVisualStyleBackColor = True
        '
        'lblQ2
        '
        Me.lblQ2.BackColor = System.Drawing.SystemColors.Window
        Me.lblQ2.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainGrantBindingSource, "Quarter2DueDate", True))
        Me.lblQ2.Location = New System.Drawing.Point(207, 29)
        Me.lblQ2.Name = "lblQ2"
        Me.lblQ2.Size = New System.Drawing.Size(72, 16)
        Me.lblQ2.TabIndex = 52
        '
        'Label31
        '
        Me.Label31.Location = New System.Drawing.Point(102, 25)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(92, 22)
        Me.Label31.TabIndex = 292
        Me.Label31.Text = "2nd Quarter Due:"
        Me.Label31.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblQ1
        '
        Me.lblQ1.BackColor = System.Drawing.SystemColors.Window
        Me.lblQ1.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainGrantBindingSource, "Quarter1DueDate", True))
        Me.lblQ1.Location = New System.Drawing.Point(207, 9)
        Me.lblQ1.Name = "lblQ1"
        Me.lblQ1.Size = New System.Drawing.Size(72, 16)
        Me.lblQ1.TabIndex = 51
        '
        'lblQ3
        '
        Me.lblQ3.BackColor = System.Drawing.SystemColors.Window
        Me.lblQ3.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainGrantBindingSource, "Quarter3DueDate", True))
        Me.lblQ3.Location = New System.Drawing.Point(207, 49)
        Me.lblQ3.Name = "lblQ3"
        Me.lblQ3.Size = New System.Drawing.Size(72, 16)
        Me.lblQ3.TabIndex = 53
        '
        'Label50
        '
        Me.Label50.Location = New System.Drawing.Point(102, 45)
        Me.Label50.Name = "Label50"
        Me.Label50.Size = New System.Drawing.Size(93, 22)
        Me.Label50.TabIndex = 288
        Me.Label50.Text = "3rd Quarter Due:"
        Me.Label50.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label46
        '
        Me.Label46.Location = New System.Drawing.Point(99, 4)
        Me.Label46.Name = "Label46"
        Me.Label46.Size = New System.Drawing.Size(93, 23)
        Me.Label46.TabIndex = 290
        Me.Label46.Text = "1st Quarter Due:"
        Me.Label46.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Panel3
        '
        Me.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel3.Controls.Add(Me.CheckBox10)
        Me.Panel3.Controls.Add(Me.CheckBox3)
        Me.Panel3.Controls.Add(Me.CheckBox1)
        Me.Panel3.Controls.Add(Me.Label10)
        Me.Panel3.Location = New System.Drawing.Point(33, 77)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(464, 46)
        Me.Panel3.TabIndex = 22
        '
        'CheckBox10
        '
        Me.CheckBox10.DataBindings.Add(New System.Windows.Forms.Binding("Checked", Me.MainGrantBindingSource, "ReceivedPrevFinancials", True))
        Me.CheckBox10.Location = New System.Drawing.Point(190, 24)
        Me.CheckBox10.Name = "CheckBox10"
        Me.CheckBox10.Size = New System.Drawing.Size(203, 17)
        Me.CheckBox10.TabIndex = 21
        Me.CheckBox10.Text = "Previous Year's Financial Summary"
        '
        'pnlJerri3
        '
        Me.pnlJerri3.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.pnlJerri3.Controls.Add(Me.cboDetermination)
        Me.pnlJerri3.Controls.Add(Me.Label15)
        Me.pnlJerri3.Controls.Add(Me.DtDetermination)
        Me.pnlJerri3.Controls.Add(Me.cboDeterminationStaffNum)
        Me.pnlJerri3.Location = New System.Drawing.Point(33, 129)
        Me.pnlJerri3.Name = "pnlJerri3"
        Me.pnlJerri3.Size = New System.Drawing.Size(464, 38)
        Me.pnlJerri3.TabIndex = 30
        '
        'flagEIN
        '
        Me.flagEIN.AutoSize = True
        Me.flagEIN.BackColor = System.Drawing.Color.Yellow
        Me.flagEIN.Font = New System.Drawing.Font("Microsoft Sans Serif", 13.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.flagEIN.Location = New System.Drawing.Point(69, 173)
        Me.flagEIN.Name = "flagEIN"
        Me.flagEIN.Size = New System.Drawing.Size(120, 25)
        Me.flagEIN.TabIndex = 427
        Me.flagEIN.Text = "EIN MISSING"
        Me.ToolTip1.SetToolTip(Me.flagEIN, "Enter EIN on congregation's detail window.")
        Me.flagEIN.UseCompatibleTextRendering = True
        '
        'chkStory
        '
        Me.chkStory.AutoSize = True
        Me.chkStory.DataBindings.Add(New System.Windows.Forms.Binding("Checked", Me.MainGrantBindingSource, "WorthStory", True))
        Me.chkStory.Location = New System.Drawing.Point(224, 177)
        Me.chkStory.Name = "chkStory"
        Me.chkStory.Size = New System.Drawing.Size(119, 17)
        Me.chkStory.TabIndex = 35
        Me.chkStory.Text = "Worth Following Up"
        Me.ToolTip1.SetToolTip(Me.chkStory, "This check box allows us to generate a list of grants we felt were interesting.")
        Me.chkStory.UseVisualStyleBackColor = True
        '
        'chkEIN
        '
        Me.chkEIN.AutoCheck = False
        Me.chkEIN.DataBindings.Add(New System.Windows.Forms.Binding("Checked", Me.MainGrantBindingSource, "EIN", True))
        Me.chkEIN.ForeColor = System.Drawing.SystemColors.ControlDark
        Me.chkEIN.Location = New System.Drawing.Point(429, 173)
        Me.chkEIN.Name = "chkEIN"
        Me.chkEIN.Size = New System.Drawing.Size(48, 24)
        Me.chkEIN.TabIndex = 23
        Me.chkEIN.TabStop = False
        Me.chkEIN.Text = "EIN"
        Me.ToolTip1.SetToolTip(Me.chkEIN, "Enter number on Congregation Detail window.")
        '
        'lblQFinal
        '
        Me.lblQFinal.BackColor = System.Drawing.SystemColors.Window
        Me.lblQFinal.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainGrantBindingSource, "Quarter4DueDate", True))
        Me.lblQFinal.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblQFinal.Location = New System.Drawing.Point(101, 447)
        Me.lblQFinal.Name = "lblQFinal"
        Me.lblQFinal.Size = New System.Drawing.Size(72, 20)
        Me.lblQFinal.TabIndex = 60
        Me.lblQFinal.Text = Me.fldQtr4.Text
        '
        'fldQtr4
        '
        Me.fldQtr4.BackColor = System.Drawing.SystemColors.Window
        Me.fldQtr4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.fldQtr4.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainGrantBindingSource, "Quarter4DueDate", True))
        Me.fldQtr4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fldQtr4.Location = New System.Drawing.Point(109, 218)
        Me.fldQtr4.Name = "fldQtr4"
        Me.fldQtr4.ReadOnly = True
        Me.fldQtr4.Size = New System.Drawing.Size(83, 21)
        Me.fldQtr4.TabIndex = 259
        '
        'Label42
        '
        Me.Label42.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label42.Location = New System.Drawing.Point(4, 440)
        Me.Label42.Name = "Label42"
        Me.Label42.Size = New System.Drawing.Size(63, 26)
        Me.Label42.TabIndex = 298
        Me.Label42.Text = "Final Rpt"
        Me.Label42.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'DtFinalRptReceived
        '
        Me.DtFinalRptReceived.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainGrantBindingSource, "FinalReportRecdDate", True))
        Me.DtFinalRptReceived.Location = New System.Drawing.Point(225, 442)
        Me.DtFinalRptReceived.Name = "DtFinalRptReceived"
        Me.DtFinalRptReceived.Size = New System.Drawing.Size(72, 20)
        Me.DtFinalRptReceived.TabIndex = 65
        Me.DtFinalRptReceived.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label48
        '
        Me.Label48.BackColor = System.Drawing.Color.Transparent
        Me.Label48.Location = New System.Drawing.Point(178, 443)
        Me.Label48.Name = "Label48"
        Me.Label48.Size = New System.Drawing.Size(41, 19)
        Me.Label48.TabIndex = 278
        Me.Label48.Text = " Rec'd:"
        Me.Label48.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cboRptRecdStaffNum
        '
        Me.cboRptRecdStaffNum.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboRptRecdStaffNum.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboRptRecdStaffNum.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.MainGrantBindingSource, "FinalReportRecdWhoNum", True))
        Me.cboRptRecdStaffNum.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboRptRecdStaffNum.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cboRptRecdStaffNum.Location = New System.Drawing.Point(311, 442)
        Me.cboRptRecdStaffNum.Name = "cboRptRecdStaffNum"
        Me.cboRptRecdStaffNum.RestrictContentToListItems = True
        Me.cboRptRecdStaffNum.Size = New System.Drawing.Size(171, 21)
        Me.cboRptRecdStaffNum.TabIndex = 66
        Me.cboRptRecdStaffNum.Tag = "Final Rpt Staff"
        '
        'Label51
        '
        Me.Label51.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label51.Location = New System.Drawing.Point(67, 443)
        Me.Label51.Margin = New System.Windows.Forms.Padding(0)
        Me.Label51.Name = "Label51"
        Me.Label51.Size = New System.Drawing.Size(34, 19)
        Me.Label51.TabIndex = 431
        Me.Label51.Text = "Due:"
        Me.Label51.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'pgResources
        '
        Me.pgResources.Controls.Add(Me.grdMain)
        Me.pgResources.Location = New System.Drawing.Point(4, 22)
        Me.pgResources.Name = "pgResources"
        Me.pgResources.Size = New System.Drawing.Size(527, 504)
        Me.pgResources.TabIndex = 1
        Me.pgResources.Tag = "RESOURCES"
        Me.pgResources.Text = "   RESOURCES   "
        Me.pgResources.UseVisualStyleBackColor = True
        '
        'pgProgress
        '
        Me.pgProgress.Controls.Add(Me.CheckBox6)
        Me.pgProgress.Controls.Add(Me.CheckBox5)
        Me.pgProgress.Controls.Add(Me.CheckBox4)
        Me.pgProgress.Controls.Add(Me.Label18)
        Me.pgProgress.Controls.Add(Me.Label41)
        Me.pgProgress.Controls.Add(Me.Label39)
        Me.pgProgress.Controls.Add(Me.fldQtr2)
        Me.pgProgress.Controls.Add(Me.Label38)
        Me.pgProgress.Controls.Add(Me.fldQtr1)
        Me.pgProgress.Controls.Add(Me.Label36)
        Me.pgProgress.Controls.Add(Me.DtRequestFinalReport)
        Me.pgProgress.Controls.Add(Me.fldQtr3)
        Me.pgProgress.Controls.Add(Me.Label40)
        Me.pgProgress.Controls.Add(Me.DtFinalReportRecd)
        Me.pgProgress.Controls.Add(Me.DtRemindFinalReport)
        Me.pgProgress.Controls.Add(Me.DtSixMonthCall)
        Me.pgProgress.Controls.Add(Me.Midpoint)
        Me.pgProgress.Controls.Add(Me.fldQtr4)
        Me.pgProgress.Controls.Add(Me.Label30)
        Me.pgProgress.Controls.Add(Me.Label19)
        Me.pgProgress.Controls.Add(Me.Label28)
        Me.pgProgress.Controls.Add(Me.GroupBox1)
        Me.pgProgress.Controls.Add(Me.Label27)
        Me.pgProgress.Controls.Add(Me.Label25)
        Me.pgProgress.Controls.Add(Me.cboMidpointCallStaffNum)
        Me.pgProgress.Controls.Add(Me.cboRequestFinalStaffNum)
        Me.pgProgress.Controls.Add(Me.cboRemindFinalStaffNum)
        Me.pgProgress.Controls.Add(Me.cboFinalRecdStaffNum2)
        Me.pgProgress.Location = New System.Drawing.Point(4, 22)
        Me.pgProgress.Name = "pgProgress"
        Me.pgProgress.Size = New System.Drawing.Size(527, 504)
        Me.pgProgress.TabIndex = 2
        Me.pgProgress.Tag = "PROGRESS"
        Me.pgProgress.Text = "REPORTS && EXTENSIONS"
        Me.pgProgress.UseVisualStyleBackColor = True
        '
        'CheckBox6
        '
        Me.CheckBox6.AutoSize = True
        Me.CheckBox6.DataBindings.Add(New System.Windows.Forms.Binding("Checked", Me.MainGrantBindingSource, "Quarter3Recd", True))
        Me.CheckBox6.Location = New System.Drawing.Point(318, 82)
        Me.CheckBox6.Name = "CheckBox6"
        Me.CheckBox6.Size = New System.Drawing.Size(72, 17)
        Me.CheckBox6.TabIndex = 302
        Me.CheckBox6.Text = "Received"
        Me.CheckBox6.UseVisualStyleBackColor = True
        '
        'CheckBox5
        '
        Me.CheckBox5.AutoSize = True
        Me.CheckBox5.DataBindings.Add(New System.Windows.Forms.Binding("Checked", Me.MainGrantBindingSource, "Quarter2Recd", True))
        Me.CheckBox5.Location = New System.Drawing.Point(318, 59)
        Me.CheckBox5.Name = "CheckBox5"
        Me.CheckBox5.Size = New System.Drawing.Size(72, 17)
        Me.CheckBox5.TabIndex = 301
        Me.CheckBox5.Text = "Received"
        Me.CheckBox5.UseVisualStyleBackColor = True
        '
        'CheckBox4
        '
        Me.CheckBox4.AutoSize = True
        Me.CheckBox4.DataBindings.Add(New System.Windows.Forms.Binding("Checked", Me.MainGrantBindingSource, "Quarter1Recd", True))
        Me.CheckBox4.Location = New System.Drawing.Point(318, 36)
        Me.CheckBox4.Name = "CheckBox4"
        Me.CheckBox4.Size = New System.Drawing.Size(72, 17)
        Me.CheckBox4.TabIndex = 300
        Me.CheckBox4.Text = "Received"
        Me.CheckBox4.UseVisualStyleBackColor = True
        '
        'Label18
        '
        Me.Label18.BackColor = System.Drawing.Color.Transparent
        Me.Label18.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(379, 109)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(49, 11)
        Me.Label18.TabIndex = 284
        Me.Label18.Text = "Staff"
        '
        'Label41
        '
        Me.Label41.BackColor = System.Drawing.Color.Transparent
        Me.Label41.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label41.Location = New System.Drawing.Point(240, 109)
        Me.Label41.Name = "Label41"
        Me.Label41.Size = New System.Drawing.Size(45, 16)
        Me.Label41.TabIndex = 283
        Me.Label41.Text = "Date"
        '
        'Label39
        '
        Me.Label39.BackColor = System.Drawing.Color.Transparent
        Me.Label39.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label39.Location = New System.Drawing.Point(3, 218)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(108, 18)
        Me.Label39.TabIndex = 282
        Me.Label39.Text = "Final Report Due:"
        Me.Label39.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'fldQtr2
        '
        Me.fldQtr2.BackColor = System.Drawing.SystemColors.Window
        Me.fldQtr2.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.fldQtr2.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainGrantBindingSource, "Quarter2DueDate", True))
        Me.fldQtr2.Location = New System.Drawing.Point(234, 59)
        Me.fldQtr2.Name = "fldQtr2"
        Me.fldQtr2.ReadOnly = True
        Me.fldQtr2.Size = New System.Drawing.Size(72, 13)
        Me.fldQtr2.TabIndex = 276
        '
        'Label38
        '
        Me.Label38.Location = New System.Drawing.Point(41, 55)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(180, 23)
        Me.Label38.TabIndex = 280
        Me.Label38.Text = "2nd Quarter Rpt Due"
        Me.Label38.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'fldQtr1
        '
        Me.fldQtr1.BackColor = System.Drawing.SystemColors.Window
        Me.fldQtr1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.fldQtr1.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainGrantBindingSource, "Quarter1DueDate", True))
        Me.fldQtr1.Location = New System.Drawing.Point(234, 36)
        Me.fldQtr1.Name = "fldQtr1"
        Me.fldQtr1.ReadOnly = True
        Me.fldQtr1.Size = New System.Drawing.Size(72, 13)
        Me.fldQtr1.TabIndex = 275
        '
        'Label36
        '
        Me.Label36.Location = New System.Drawing.Point(114, 27)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(106, 32)
        Me.Label36.TabIndex = 278
        Me.Label36.Text = "1st Quarter Rpt Due"
        Me.Label36.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'DtRequestFinalReport
        '
        Me.DtRequestFinalReport.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainGrantBindingSource, "RequestFinalReportDate", True))
        Me.DtRequestFinalReport.Location = New System.Drawing.Point(234, 163)
        Me.DtRequestFinalReport.Name = "DtRequestFinalReport"
        Me.DtRequestFinalReport.Size = New System.Drawing.Size(72, 20)
        Me.DtRequestFinalReport.TabIndex = 307
        Me.DtRequestFinalReport.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'fldQtr3
        '
        Me.fldQtr3.BackColor = System.Drawing.SystemColors.Window
        Me.fldQtr3.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.fldQtr3.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainGrantBindingSource, "Quarter3DueDate", True))
        Me.fldQtr3.Location = New System.Drawing.Point(234, 82)
        Me.fldQtr3.Name = "fldQtr3"
        Me.fldQtr3.ReadOnly = True
        Me.fldQtr3.Size = New System.Drawing.Size(72, 13)
        Me.fldQtr3.TabIndex = 277
        '
        'Label40
        '
        Me.Label40.Location = New System.Drawing.Point(115, 72)
        Me.Label40.Name = "Label40"
        Me.Label40.Size = New System.Drawing.Size(107, 32)
        Me.Label40.TabIndex = 264
        Me.Label40.Text = "3rd Quarter Rpt Due"
        Me.Label40.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'DtFinalReportRecd
        '
        Me.DtFinalReportRecd.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainGrantBindingSource, "FinalReportRecdDate", True))
        Me.DtFinalReportRecd.Location = New System.Drawing.Point(234, 215)
        Me.DtFinalReportRecd.Name = "DtFinalReportRecd"
        Me.DtFinalReportRecd.Size = New System.Drawing.Size(72, 20)
        Me.DtFinalReportRecd.TabIndex = 311
        Me.DtFinalReportRecd.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'DtRemindFinalReport
        '
        Me.DtRemindFinalReport.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainGrantBindingSource, "RemindFinalReportDate", True))
        Me.DtRemindFinalReport.Location = New System.Drawing.Point(234, 189)
        Me.DtRemindFinalReport.Name = "DtRemindFinalReport"
        Me.DtRemindFinalReport.Size = New System.Drawing.Size(72, 20)
        Me.DtRemindFinalReport.TabIndex = 309
        Me.DtRemindFinalReport.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'DtSixMonthCall
        '
        Me.DtSixMonthCall.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainGrantBindingSource, "SixMonthCallDate", True))
        Me.DtSixMonthCall.Location = New System.Drawing.Point(234, 137)
        Me.DtSixMonthCall.Name = "DtSixMonthCall"
        Me.DtSixMonthCall.Size = New System.Drawing.Size(72, 20)
        Me.DtSixMonthCall.TabIndex = 305
        Me.DtSixMonthCall.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.DtSixMonthCall.Visible = False
        '
        'Midpoint
        '
        Me.Midpoint.BackColor = System.Drawing.SystemColors.Window
        Me.Midpoint.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainGrantBindingSource, "SixMonthDueDate", True))
        Me.Midpoint.Location = New System.Drawing.Point(435, 384)
        Me.Midpoint.Name = "Midpoint"
        Me.Midpoint.Size = New System.Drawing.Size(72, 16)
        Me.Midpoint.TabIndex = 320
        Me.Midpoint.Text = "Midpoint"
        Me.Midpoint.Visible = False
        '
        'Label30
        '
        Me.Label30.BackColor = System.Drawing.Color.Transparent
        Me.Label30.Location = New System.Drawing.Point(80, 159)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(146, 27)
        Me.Label30.TabIndex = 277
        Me.Label30.Text = "1st Request for Final Rpt."
        Me.Label30.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label19
        '
        Me.Label19.Location = New System.Drawing.Point(349, 382)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(72, 16)
        Me.Label19.TabIndex = 257
        Me.Label19.Text = "MidPoint"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Label19.Visible = False
        '
        'Label28
        '
        Me.Label28.BackColor = System.Drawing.Color.Transparent
        Me.Label28.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label28.Location = New System.Drawing.Point(189, 219)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(44, 17)
        Me.Label28.TabIndex = 275
        Me.Label28.Text = " Rec'd"
        Me.Label28.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label43)
        Me.GroupBox1.Controls.Add(Me.btnExtension)
        Me.GroupBox1.Controls.Add(Me.Label44)
        Me.GroupBox1.Controls.Add(Me.fldOrigDueDate)
        Me.GroupBox1.Controls.Add(Me.fldExtensions)
        Me.GroupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.GroupBox1.Location = New System.Drawing.Point(100, 253)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(230, 160)
        Me.GroupBox1.TabIndex = 315
        Me.GroupBox1.TabStop = False
        '
        'Label43
        '
        Me.Label43.Location = New System.Drawing.Point(14, 89)
        Me.Label43.Name = "Label43"
        Me.Label43.Size = New System.Drawing.Size(105, 28)
        Me.Label43.TabIndex = 225
        Me.Label43.Text = "Original Due Date:"
        Me.Label43.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnExtension
        '
        Me.btnExtension.BackColor = System.Drawing.SystemColors.Control
        Me.btnExtension.Location = New System.Drawing.Point(46, 14)
        Me.btnExtension.Name = "btnExtension"
        Me.btnExtension.Size = New System.Drawing.Size(99, 24)
        Me.btnExtension.TabIndex = 95
        Me.btnExtension.TabStop = False
        Me.btnExtension.Text = "Extend Deadline"
        Me.btnExtension.UseVisualStyleBackColor = False
        '
        'Label44
        '
        Me.Label44.Location = New System.Drawing.Point(59, 130)
        Me.Label44.Name = "Label44"
        Me.Label44.Size = New System.Drawing.Size(72, 16)
        Me.Label44.TabIndex = 3
        Me.Label44.Text = "# extensions:"
        Me.Label44.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'fldOrigDueDate
        '
        Me.fldOrigDueDate.BackColor = System.Drawing.SystemColors.Window
        Me.fldOrigDueDate.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainGrantBindingSource, "ExtensionOriginalDate", True))
        Me.fldOrigDueDate.Location = New System.Drawing.Point(125, 97)
        Me.fldOrigDueDate.Name = "fldOrigDueDate"
        Me.fldOrigDueDate.Size = New System.Drawing.Size(81, 20)
        Me.fldOrigDueDate.TabIndex = 97
        Me.fldOrigDueDate.Text = "fldOrigDueDate"
        '
        'fldExtensions
        '
        Me.fldExtensions.BackColor = System.Drawing.SystemColors.Window
        Me.fldExtensions.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainGrantBindingSource, "ExtensionCnt", True))
        Me.fldExtensions.Location = New System.Drawing.Point(133, 129)
        Me.fldExtensions.Name = "fldExtensions"
        Me.fldExtensions.Size = New System.Drawing.Size(46, 16)
        Me.fldExtensions.TabIndex = 98
        Me.fldExtensions.Text = "Label46"
        '
        'Label27
        '
        Me.Label27.BackColor = System.Drawing.Color.Transparent
        Me.Label27.Location = New System.Drawing.Point(52, 192)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(174, 17)
        Me.Label27.TabIndex = 273
        Me.Label27.Text = "2nd Request for Final Rpt."
        Me.Label27.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label25
        '
        Me.Label25.BackColor = System.Drawing.Color.Transparent
        Me.Label25.Location = New System.Drawing.Point(114, 138)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(112, 16)
        Me.Label25.TabIndex = 270
        Me.Label25.Text = "Midpoint Call Made"
        Me.Label25.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Label25.Visible = False
        '
        'cboMidpointCallStaffNum
        '
        Me.cboMidpointCallStaffNum.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboMidpointCallStaffNum.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboMidpointCallStaffNum.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.MainGrantBindingSource, "SixMonthCallWhoNum", True))
        Me.cboMidpointCallStaffNum.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboMidpointCallStaffNum.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cboMidpointCallStaffNum.Location = New System.Drawing.Point(314, 137)
        Me.cboMidpointCallStaffNum.Name = "cboMidpointCallStaffNum"
        Me.cboMidpointCallStaffNum.RestrictContentToListItems = True
        Me.cboMidpointCallStaffNum.Size = New System.Drawing.Size(171, 21)
        Me.cboMidpointCallStaffNum.TabIndex = 306
        Me.cboMidpointCallStaffNum.Visible = False
        '
        'cboRequestFinalStaffNum
        '
        Me.cboRequestFinalStaffNum.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboRequestFinalStaffNum.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboRequestFinalStaffNum.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.MainGrantBindingSource, "RequestFinalReportWhoNum", True))
        Me.cboRequestFinalStaffNum.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboRequestFinalStaffNum.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cboRequestFinalStaffNum.Location = New System.Drawing.Point(314, 163)
        Me.cboRequestFinalStaffNum.Name = "cboRequestFinalStaffNum"
        Me.cboRequestFinalStaffNum.RestrictContentToListItems = True
        Me.cboRequestFinalStaffNum.Size = New System.Drawing.Size(171, 21)
        Me.cboRequestFinalStaffNum.TabIndex = 308
        '
        'cboRemindFinalStaffNum
        '
        Me.cboRemindFinalStaffNum.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboRemindFinalStaffNum.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboRemindFinalStaffNum.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.MainGrantBindingSource, "RemindFinalReportWhoNum", True))
        Me.cboRemindFinalStaffNum.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboRemindFinalStaffNum.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cboRemindFinalStaffNum.Location = New System.Drawing.Point(314, 189)
        Me.cboRemindFinalStaffNum.Name = "cboRemindFinalStaffNum"
        Me.cboRemindFinalStaffNum.RestrictContentToListItems = True
        Me.cboRemindFinalStaffNum.Size = New System.Drawing.Size(171, 21)
        Me.cboRemindFinalStaffNum.TabIndex = 310
        '
        'cboFinalRecdStaffNum2
        '
        Me.cboFinalRecdStaffNum2.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboFinalRecdStaffNum2.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboFinalRecdStaffNum2.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.MainGrantBindingSource, "FinalReportRecdWhoNum", True))
        Me.cboFinalRecdStaffNum2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboFinalRecdStaffNum2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cboFinalRecdStaffNum2.Location = New System.Drawing.Point(314, 215)
        Me.cboFinalRecdStaffNum2.Name = "cboFinalRecdStaffNum2"
        Me.cboFinalRecdStaffNum2.RestrictContentToListItems = True
        Me.cboFinalRecdStaffNum2.Size = New System.Drawing.Size(171, 21)
        Me.cboFinalRecdStaffNum2.TabIndex = 312
        '
        'StatusBar1
        '
        Me.StatusBar1.Location = New System.Drawing.Point(0, 619)
        Me.StatusBar1.Name = "StatusBar1"
        Me.StatusBar1.Panels.AddRange(New System.Windows.Forms.StatusBarPanel() {Me.StatusBarPanel1, Me.StatusBarPanelID, Me.StatusBarPanel2})
        Me.StatusBar1.ShowPanels = True
        Me.StatusBar1.Size = New System.Drawing.Size(984, 22)
        Me.StatusBar1.TabIndex = 231
        Me.StatusBar1.Text = "StatusBar1"
        '
        'StatusBarPanel1
        '
        Me.StatusBarPanel1.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents
        Me.StatusBarPanel1.MinWidth = 200
        Me.StatusBarPanel1.Name = "StatusBarPanel1"
        Me.StatusBarPanel1.Width = 200
        '
        'StatusBarPanelID
        '
        Me.StatusBarPanelID.Alignment = System.Windows.Forms.HorizontalAlignment.Center
        Me.StatusBarPanelID.MinWidth = 200
        Me.StatusBarPanelID.Name = "StatusBarPanelID"
        Me.StatusBarPanelID.Text = "Grant Request ID here"
        Me.StatusBarPanelID.Width = 200
        '
        'StatusBarPanel2
        '
        Me.StatusBarPanel2.Alignment = System.Windows.Forms.HorizontalAlignment.Right
        Me.StatusBarPanel2.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring
        Me.StatusBarPanel2.Name = "StatusBarPanel2"
        Me.StatusBarPanel2.Text = "Use this window to change Grant information."
        Me.StatusBarPanel2.Width = 567
        '
        'fldMGI
        '
        Me.fldMGI.BackColor = System.Drawing.SystemColors.Control
        Me.fldMGI.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.fldMGI.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainGrantBindingSource, "GINum", True))
        Me.fldMGI.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fldMGI.ForeColor = System.Drawing.Color.DarkGreen
        Me.fldMGI.Location = New System.Drawing.Point(869, 597)
        Me.fldMGI.Multiline = True
        Me.fldMGI.Name = "fldMGI"
        Me.fldMGI.ReadOnly = True
        Me.fldMGI.Size = New System.Drawing.Size(40, 13)
        Me.fldMGI.TabIndex = 502
        Me.fldMGI.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ToolTip1.SetToolTip(Me.fldMGI, "Doubleclick to go to MGI Application")
        '
        'btnSaveExit
        '
        Me.btnSaveExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSaveExit.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.btnSaveExit.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSaveExit.Image = Global.InfoCtr.My.Resources.Resources.btnSaveExit
        Me.btnSaveExit.ImageAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSaveExit.Location = New System.Drawing.Point(138, 1)
        Me.btnSaveExit.Name = "btnSaveExit"
        Me.btnSaveExit.Size = New System.Drawing.Size(40, 35)
        Me.btnSaveExit.TabIndex = 505
        Me.btnSaveExit.Text = "Close"
        Me.btnSaveExit.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.ToolTip1.SetToolTip(Me.btnSaveExit, "Saves any changes and Closes this window")
        Me.btnSaveExit.UseVisualStyleBackColor = False
        '
        'btnNew
        '
        Me.btnNew.BackColor = System.Drawing.Color.PaleGoldenrod
        Me.btnNew.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnNew.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnNew.Image = Global.InfoCtr.My.Resources.Resources.btnAddNew
        Me.btnNew.ImageAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnNew.Location = New System.Drawing.Point(4, 1)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(40, 35)
        Me.btnNew.TabIndex = 503
        Me.btnNew.Text = "New"
        Me.btnNew.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.ToolTip1.SetToolTip(Me.btnNew, "Add new Resource Recommendation")
        Me.btnNew.UseVisualStyleBackColor = False
        '
        'btnSend
        '
        Me.btnSend.BackColor = System.Drawing.SystemColors.Control
        Me.btnSend.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSend.Image = CType(resources.GetObject("btnSend.Image"), System.Drawing.Image)
        Me.btnSend.ImageAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSend.Location = New System.Drawing.Point(95, 1)
        Me.btnSend.Name = "btnSend"
        Me.btnSend.Size = New System.Drawing.Size(40, 35)
        Me.btnSend.TabIndex = 503
        Me.btnSend.Text = "Send"
        Me.btnSend.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.ToolTip1.SetToolTip(Me.btnSend, "Grant Proposal, request Final Report (merge into Word doc)")
        Me.btnSend.UseVisualStyleBackColor = False
        '
        'btnViewFile
        '
        Me.btnViewFile.BackColor = System.Drawing.SystemColors.Control
        Me.btnViewFile.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnViewFile.Image = Global.InfoCtr.My.Resources.Resources.btnAttached
        Me.btnViewFile.ImageAlign = System.Drawing.ContentAlignment.BottomLeft
        Me.btnViewFile.Location = New System.Drawing.Point(49, 1)
        Me.btnViewFile.Name = "btnViewFile"
        Me.btnViewFile.Size = New System.Drawing.Size(41, 35)
        Me.btnViewFile.TabIndex = 506
        Me.btnViewFile.Text = "Files"
        Me.btnViewFile.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.ToolTip1.SetToolTip(Me.btnViewFile, "Attach or open a document related to this case.")
        Me.btnViewFile.UseVisualStyleBackColor = False
        '
        'MainMenu1
        '
        Me.MainMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem1, Me.MenuItem2, Me.MenuItem3, Me.MenuItem4, Me.menuitem8})
        '
        'MenuItem1
        '
        Me.MenuItem1.Index = 0
        Me.MenuItem1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.miAttach, Me.miOpenFile, Me.miClose})
        Me.MenuItem1.Text = "File"
        '
        'miAttach
        '
        Me.miAttach.Index = 0
        Me.miAttach.Text = "Attach File"
        '
        'miOpenFile
        '
        Me.miOpenFile.Index = 1
        Me.miOpenFile.Text = "Open File"
        '
        'miClose
        '
        Me.miClose.Index = 2
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
        Me.miDelete.Text = "Delete Grant"
        '
        'MenuItem3
        '
        Me.MenuItem3.Enabled = False
        Me.MenuItem3.Index = 2
        Me.MenuItem3.Text = "Reports"
        '
        'MenuItem4
        '
        Me.MenuItem4.Index = 3
        Me.MenuItem4.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.miGotoCase, Me.miGotoResource, Me.miMGI, Me.miGotoOrg})
        Me.MenuItem4.Text = "Goto"
        Me.MenuItem4.Visible = False
        '
        'miGotoCase
        '
        Me.miGotoCase.Index = 0
        Me.miGotoCase.Text = "Case"
        '
        'miGotoResource
        '
        Me.miGotoResource.Index = 1
        Me.miGotoResource.Text = "Resource"
        '
        'miMGI
        '
        Me.miMGI.Index = 2
        Me.miMGI.Text = "MGI Application"
        '
        'miGotoOrg
        '
        Me.miGotoOrg.Index = 3
        Me.miGotoOrg.Text = "Organization"
        '
        'menuitem8
        '
        Me.menuitem8.Index = 4
        Me.menuitem8.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.miDefinition, Me.miHelp, Me.miDelay})
        Me.menuitem8.Text = "Help"
        '
        'miDefinition
        '
        Me.miDefinition.Index = 0
        Me.miDefinition.Text = "Definition of Determination terms"
        '
        'miHelp
        '
        Me.miHelp.Index = 1
        Me.miHelp.Text = "Help"
        '
        'daspGetRecommendations
        '
        Me.daspGetRecommendations.SelectCommand = Me.SqlSelectCommand4
        Me.daspGetRecommendations.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "tblResourceRecommend", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("RecommendID", "RecommendID"), New System.Data.Common.DataColumnMapping("ResourceNum", "ResourceNum"), New System.Data.Common.DataColumnMapping("OrgNum", "OrgNum"), New System.Data.Common.DataColumnMapping("CaseNum", "CaseNum"), New System.Data.Common.DataColumnMapping("CallNum", "CallNum"), New System.Data.Common.DataColumnMapping("RecommendDate", "RecommendDate"), New System.Data.Common.DataColumnMapping("Used", "Used"), New System.Data.Common.DataColumnMapping("GrantNum", "GrantNum"), New System.Data.Common.DataColumnMapping("RecommendStaffNum", "RecommendStaffNum"), New System.Data.Common.DataColumnMapping("Type", "Type"), New System.Data.Common.DataColumnMapping("ResourceName", "ResourceName")})})
        '
        'SqlSelectCommand4
        '
        Me.SqlSelectCommand4.CommandText = "[GetResFunded]"
        Me.SqlSelectCommand4.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlSelectCommand4.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@IDStr", System.Data.SqlDbType.VarChar, 30), New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, False, CType(0, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing), New System.Data.SqlClient.SqlParameter("@IDCase", System.Data.SqlDbType.Int)})
        '
        'daspMainGrant
        '
        Me.daspMainGrant.DeleteCommand = Me.SqlDeleteCommand
        Me.daspMainGrant.InsertCommand = Me.SqlInsertCommand
        Me.daspMainGrant.SelectCommand = Me.SqlSelectCommand1
        Me.daspMainGrant.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "MainGrant", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("GrantIDTxt", "GrantIDTxt"), New System.Data.Common.DataColumnMapping("OrgNum", "OrgNum"), New System.Data.Common.DataColumnMapping("CaseNum", "CaseNum"), New System.Data.Common.DataColumnMapping("GINum", "GINum"), New System.Data.Common.DataColumnMapping("TypeofGrant", "TypeofGrant"), New System.Data.Common.DataColumnMapping("AwardDate", "AwardDate"), New System.Data.Common.DataColumnMapping("AwardKey", "AwardKey"), New System.Data.Common.DataColumnMapping("Issue", "Issue"), New System.Data.Common.DataColumnMapping("GrantStaffTxt", "GrantStaffTxt"), New System.Data.Common.DataColumnMapping("GrantStaffNum", "GrantStaffNum"), New System.Data.Common.DataColumnMapping("FollowupStaffNum", "FollowupStaffNum"), New System.Data.Common.DataColumnMapping("PDNum", "PDNum"), New System.Data.Common.DataColumnMapping("SrClergyNum", "SrClergyNum"), New System.Data.Common.DataColumnMapping("Amount", "Amount"), New System.Data.Common.DataColumnMapping("TotalAmount", "TotalAmount"), New System.Data.Common.DataColumnMapping("Notes", "Notes"), New System.Data.Common.DataColumnMapping("FirstDraftReceivedDate", "FirstDraftReceivedDate"), New System.Data.Common.DataColumnMapping("FirstDraftReceivedWhoNum", "FirstDraftReceivedWhoNum"), New System.Data.Common.DataColumnMapping("ChangesRecommendedDate", "ChangesRecommendedDate"), New System.Data.Common.DataColumnMapping("ChangesRecommendedWhoNum", "ChangesRecommendedWhoNum"), New System.Data.Common.DataColumnMapping("FinalDraftReceivedDate", "FinalDraftReceivedDate"), New System.Data.Common.DataColumnMapping("FinalDraftReceivedWhoNum", "FinalDraftReceivedWhoNum"), New System.Data.Common.DataColumnMapping("Determination", "Determination"), New System.Data.Common.DataColumnMapping("DeterminationDate", "DeterminationDate"), New System.Data.Common.DataColumnMapping("DeterminationWhoNum", "DeterminationWhoNum"), New System.Data.Common.DataColumnMapping("NotifyCallDate", "NotifyCallDate"), New System.Data.Common.DataColumnMapping("NotifyCallWhoNum", "NotifyCallWhoNum"), New System.Data.Common.DataColumnMapping("NoticeMailedDate", "NoticeMailedDate"), New System.Data.Common.DataColumnMapping("NoticeMailedWhoNum", "NoticeMailedWhoNum"), New System.Data.Common.DataColumnMapping("SignedAgreementRecdDate", "SignedAgreementRecdDate"), New System.Data.Common.DataColumnMapping("SignedAgreementRecdWhoNum", "SignedAgreementRecdWhoNum"), New System.Data.Common.DataColumnMapping("ReceivedBudget", "ReceivedBudget"), New System.Data.Common.DataColumnMapping("ReceivedSignatures", "ReceivedSignatures"), New System.Data.Common.DataColumnMapping("Received501c3", "Received501c3"), New System.Data.Common.DataColumnMapping("ReceivedPreviousFinancials", "ReceivedPrevFinancials"), New System.Data.Common.DataColumnMapping("CheckMailedDate", "CheckMailedDate"), New System.Data.Common.DataColumnMapping("CheckMailedWhoNum", "CheckMailedWhoNum"), New System.Data.Common.DataColumnMapping("CheckNum", "CheckNum"), New System.Data.Common.DataColumnMapping("Quarter4DueDate", "Quarter4DueDate"), New System.Data.Common.DataColumnMapping("SixMonthDueDate", "SixMonthDueDate"), New System.Data.Common.DataColumnMapping("SixMonthCallDate", "SixMonthCallDate"), New System.Data.Common.DataColumnMapping("SixMonthCallWhoNum", "SixMonthCallWhoNum"), New System.Data.Common.DataColumnMapping("ProjectCompleteDate", "ProjectCompleteDate"), New System.Data.Common.DataColumnMapping("ProjectCompleteWhoNum", "ProjectCompleteWhoNum"), New System.Data.Common.DataColumnMapping("RequestFinalReportDate", "RequestFinalReportDate"), New System.Data.Common.DataColumnMapping("RequestFinalReportWhoNum", "RequestFinalReportWhoNum"), New System.Data.Common.DataColumnMapping("RemindFinalReportDate", "RemindFinalReportDate"), New System.Data.Common.DataColumnMapping("RemindFinalReportWhoNum", "RemindFinalReportWhoNum"), New System.Data.Common.DataColumnMapping("FinalReportRecdDate", "FinalReportRecdDate"), New System.Data.Common.DataColumnMapping("FinalReportRecdWhoNum", "FinalReportRecdWhoNum"), New System.Data.Common.DataColumnMapping("FollowupInProgressDate", "FollowupInProgressDate"), New System.Data.Common.DataColumnMapping("FollowupReportCompleteWhoNum", "FollowupReportCompleteWhoNum"), New System.Data.Common.DataColumnMapping("FollowupReportCompleteDate", "FollowupReportCompleteDate"), New System.Data.Common.DataColumnMapping("GrantComplete", "GrantComplete"), New System.Data.Common.DataColumnMapping("GrantCompleteWhoNum", "GrantCompleteWhoNum"), New System.Data.Common.DataColumnMapping("ExtensionCnt", "ExtensionCnt"), New System.Data.Common.DataColumnMapping("ExtensionOriginalDate", "ExtensionOriginalDate"), New System.Data.Common.DataColumnMapping("ResourceUsed", "ResourceUsed"), New System.Data.Common.DataColumnMapping("CheckRequestedDate", "CheckRequestedDate"), New System.Data.Common.DataColumnMapping("CheckRequestedWhoNum", "CheckRequestedWhoNum"), New System.Data.Common.DataColumnMapping("CheckArrivedDate", "CheckArrivedDate"), New System.Data.Common.DataColumnMapping("CheckArrivedWhoNum", "CheckArrivedWhoNum"), New System.Data.Common.DataColumnMapping("RegisteredOnline", "RegisteredOnline"), New System.Data.Common.DataColumnMapping("RegisteredOnlineDate", "RegisteredOnlineDate"), New System.Data.Common.DataColumnMapping("Quarter1DueDate", "Quarter1DueDate"), New System.Data.Common.DataColumnMapping("Quarter1Recd", "Quarter1Recd"), New System.Data.Common.DataColumnMapping("Quarter2DueDate", "Quarter2DueDate"), New System.Data.Common.DataColumnMapping("Quarter2Recd", "Quarter2Recd"), New System.Data.Common.DataColumnMapping("Quarter3DueDate", "Quarter3DueDate"), New System.Data.Common.DataColumnMapping("Quarter3Recd", "Quarter3Recd"), New System.Data.Common.DataColumnMapping("WorthStory", "WorthStory"), New System.Data.Common.DataColumnMapping("GrantCompletion", "GrantCompletion"), New System.Data.Common.DataColumnMapping("GrantCompletionDate", "GrantCompletionDate"), New System.Data.Common.DataColumnMapping("Stamped", "Stamped"), New System.Data.Common.DataColumnMapping("EIN", "EIN"), New System.Data.Common.DataColumnMapping("GrantID", "GrantID")})})
        Me.daspMainGrant.UpdateCommand = Me.SqlUpdateCommand1
        '
        'SqlSelectCommand1
        '
        Me.SqlSelectCommand1.CommandText = "dbo.MainGrant"
        Me.SqlSelectCommand1.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlSelectCommand1.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, False, CType(0, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing), New System.Data.SqlClient.SqlParameter("@Original_GrantIDTxt", System.Data.SqlDbType.VarChar, 15)})
        '
        'SqlUpdateCommand1
        '
        Me.SqlUpdateCommand1.CommandText = "dbo.MainGrantUpdate"
        Me.SqlUpdateCommand1.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlUpdateCommand1.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, False, CType(0, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing), New System.Data.SqlClient.SqlParameter("@OrgNum", System.Data.SqlDbType.Int, 4, "OrgNum"), New System.Data.SqlClient.SqlParameter("@CaseNum", System.Data.SqlDbType.Int, 4, "CaseNum"), New System.Data.SqlClient.SqlParameter("@GINum", System.Data.SqlDbType.Int, 4, "GINum"), New System.Data.SqlClient.SqlParameter("@TypeofGrant", System.Data.SqlDbType.VarChar, 50, "TypeofGrant"), New System.Data.SqlClient.SqlParameter("@AwardDate", System.Data.SqlDbType.[Date], 4, "AwardDate"), New System.Data.SqlClient.SqlParameter("@AwardKey", System.Data.SqlDbType.VarChar, 15, "AwardKey"), New System.Data.SqlClient.SqlParameter("@Issue", System.Data.SqlDbType.VarChar, -1, "Issue"), New System.Data.SqlClient.SqlParameter("@GrantStaffTxt", System.Data.SqlDbType.VarChar, 50, "GrantStaffTxt"), New System.Data.SqlClient.SqlParameter("@GrantStaffNum", System.Data.SqlDbType.Int, 4, "GrantStaffNum"), New System.Data.SqlClient.SqlParameter("@FollowupStaffNum", System.Data.SqlDbType.Int, 4, "FollowupStaffNum"), New System.Data.SqlClient.SqlParameter("@PDNum", System.Data.SqlDbType.Int, 4, "PDNum"), New System.Data.SqlClient.SqlParameter("@SrClergyNum", System.Data.SqlDbType.Int, 4, "SrClergyNum"), New System.Data.SqlClient.SqlParameter("@Amount", System.Data.SqlDbType.SmallMoney, 4, "Amount"), New System.Data.SqlClient.SqlParameter("@TotalAmount", System.Data.SqlDbType.Money, 8, "TotalAmount"), New System.Data.SqlClient.SqlParameter("@Notes", System.Data.SqlDbType.VarChar, 2147483647, "Notes"), New System.Data.SqlClient.SqlParameter("@FirstDraftReceivedDate", System.Data.SqlDbType.[Date], 4, "FirstDraftReceivedDate"), New System.Data.SqlClient.SqlParameter("@FirstDraftReceivedWhoNum", System.Data.SqlDbType.Int, 4, "FirstDraftReceivedWhoNum"), New System.Data.SqlClient.SqlParameter("@ChangesRecommendedDate", System.Data.SqlDbType.[Date], 4, "ChangesRecommendedDate"), New System.Data.SqlClient.SqlParameter("@ChangesRecommendedWhoNum", System.Data.SqlDbType.Int, 4, "ChangesRecommendedWhoNum"), New System.Data.SqlClient.SqlParameter("@FinalDraftReceivedDate", System.Data.SqlDbType.[Date], 4, "FinalDraftReceivedDate"), New System.Data.SqlClient.SqlParameter("@FinalDraftReceivedWhoNum", System.Data.SqlDbType.Int, 4, "FinalDraftReceivedWhoNum"), New System.Data.SqlClient.SqlParameter("@Determination", System.Data.SqlDbType.VarChar, 50, "Determination"), New System.Data.SqlClient.SqlParameter("@DeterminationDate", System.Data.SqlDbType.[Date], 4, "DeterminationDate"), New System.Data.SqlClient.SqlParameter("@DeterminationWhoNum", System.Data.SqlDbType.Int, 4, "DeterminationWhoNum"), New System.Data.SqlClient.SqlParameter("@NotifyCallDate", System.Data.SqlDbType.[Date], 4, "NotifyCallDate"), New System.Data.SqlClient.SqlParameter("@NotifyCallWhoNum", System.Data.SqlDbType.Int, 4, "NotifyCallWhoNum"), New System.Data.SqlClient.SqlParameter("@NoticeMailedDate", System.Data.SqlDbType.[Date], 4, "NoticeMailedDate"), New System.Data.SqlClient.SqlParameter("@NoticeMailedWhoNum", System.Data.SqlDbType.Int, 4, "NoticeMailedWhoNum"), New System.Data.SqlClient.SqlParameter("@SignedAgreementRecdDate", System.Data.SqlDbType.[Date], 4, "SignedAgreementRecdDate"), New System.Data.SqlClient.SqlParameter("@SignedAgreementRecdWhoNum", System.Data.SqlDbType.Int, 4, "SignedAgreementRecdWhoNum"), New System.Data.SqlClient.SqlParameter("@ReceivedBudget", System.Data.SqlDbType.Bit, 1, "ReceivedBudget"), New System.Data.SqlClient.SqlParameter("@Received501c3", System.Data.SqlDbType.Bit, 1, "Received501c3"), New System.Data.SqlClient.SqlParameter("@ReceivedPrevFinancials", System.Data.SqlDbType.Bit, 1, "ReceivedPrevFinancials"), New System.Data.SqlClient.SqlParameter("@CheckMailedDate", System.Data.SqlDbType.[Date], 4, "CheckMailedDate"), New System.Data.SqlClient.SqlParameter("@CheckMailedWhoNum", System.Data.SqlDbType.Int, 4, "CheckMailedWhoNum"), New System.Data.SqlClient.SqlParameter("@CheckNum", System.Data.SqlDbType.Int, 4, "CheckNum"), New System.Data.SqlClient.SqlParameter("@Quarter4DueDate", System.Data.SqlDbType.[Date], 4, "Quarter4DueDate"), New System.Data.SqlClient.SqlParameter("@SixMonthDueDate", System.Data.SqlDbType.[Date], 4, "SixMonthDueDate"), New System.Data.SqlClient.SqlParameter("@SixMonthCallDate", System.Data.SqlDbType.[Date], 4, "SixMonthCallDate"), New System.Data.SqlClient.SqlParameter("@SixMonthCallWhoNum", System.Data.SqlDbType.Int, 4, "SixMonthCallWhoNum"), New System.Data.SqlClient.SqlParameter("@ProjectCompleteDate", System.Data.SqlDbType.[Date], 4, "ProjectCompleteDate"), New System.Data.SqlClient.SqlParameter("@ProjectCompleteWhoNum", System.Data.SqlDbType.Int, 4, "ProjectCompleteWhoNum"), New System.Data.SqlClient.SqlParameter("@RequestFinalReportDate", System.Data.SqlDbType.[Date], 4, "RequestFinalReportDate"), New System.Data.SqlClient.SqlParameter("@RequestFinalReportWhoNum", System.Data.SqlDbType.Int, 4, "RequestFinalReportWhoNum"), New System.Data.SqlClient.SqlParameter("@RemindFinalReportDate", System.Data.SqlDbType.[Date], 4, "RemindFinalReportDate"), New System.Data.SqlClient.SqlParameter("@RemindFinalReportWhoNum", System.Data.SqlDbType.Int, 4, "RemindFinalReportWhoNum"), New System.Data.SqlClient.SqlParameter("@FinalReportRecdDate", System.Data.SqlDbType.[Date], 4, "FinalReportRecdDate"), New System.Data.SqlClient.SqlParameter("@FinalReportRecdWhoNum", System.Data.SqlDbType.Int, 4, "FinalReportRecdWhoNum"), New System.Data.SqlClient.SqlParameter("@FollowupInProgressDate", System.Data.SqlDbType.[Date], 4, "FollowupInProgressDate"), New System.Data.SqlClient.SqlParameter("@FollowupReportCompleteWhoNum", System.Data.SqlDbType.Int, 4, "FollowupReportCompleteWhoNum"), New System.Data.SqlClient.SqlParameter("@FollowupReportCompleteDate", System.Data.SqlDbType.[Date], 4, "FollowupReportCompleteDate"), New System.Data.SqlClient.SqlParameter("@GrantComplete", System.Data.SqlDbType.Bit, 1, "GrantComplete"), New System.Data.SqlClient.SqlParameter("@GrantCompleteWhoNum", System.Data.SqlDbType.Int, 4, "GrantCompleteWhoNum"), New System.Data.SqlClient.SqlParameter("@ExtensionCnt", System.Data.SqlDbType.Int, 4, "ExtensionCnt"), New System.Data.SqlClient.SqlParameter("@ExtensionOriginalDate", System.Data.SqlDbType.[Date], 4, "ExtensionOriginalDate"), New System.Data.SqlClient.SqlParameter("@CheckRequestedDate", System.Data.SqlDbType.[Date], 4, "CheckRequestedDate"), New System.Data.SqlClient.SqlParameter("@CheckRequestedWhoNum", System.Data.SqlDbType.Int, 4, "CheckRequestedWhoNum"), New System.Data.SqlClient.SqlParameter("@CheckArrivedDate", System.Data.SqlDbType.[Date], 4, "CheckArrivedDate"), New System.Data.SqlClient.SqlParameter("@CheckArrivedWhoNum", System.Data.SqlDbType.Int, 4, "CheckArrivedWhoNum"), New System.Data.SqlClient.SqlParameter("@Quarter1DueDate", System.Data.SqlDbType.[Date], 4, "Quarter1DueDate"), New System.Data.SqlClient.SqlParameter("@Quarter1Recd", System.Data.SqlDbType.Bit, 1, "Quarter1Recd"), New System.Data.SqlClient.SqlParameter("@Quarter2DueDate", System.Data.SqlDbType.[Date], 4, "Quarter2DueDate"), New System.Data.SqlClient.SqlParameter("@Quarter2Recd", System.Data.SqlDbType.Bit, 1, "Quarter2Recd"), New System.Data.SqlClient.SqlParameter("@Quarter3DueDate", System.Data.SqlDbType.[Date], 4, "Quarter3DueDate"), New System.Data.SqlClient.SqlParameter("@Quarter3Recd", System.Data.SqlDbType.Bit, 1, "Quarter3Recd"), New System.Data.SqlClient.SqlParameter("@WorthStory", System.Data.SqlDbType.Bit, 1, "WorthStory"), New System.Data.SqlClient.SqlParameter("@GrantCompletion", System.Data.SqlDbType.VarChar, 10, "GrantCompletion"), New System.Data.SqlClient.SqlParameter("@GrantCompletionDate", System.Data.SqlDbType.[Date], 4, "GrantCompletionDate"), New System.Data.SqlClient.SqlParameter("@Original_GrantIDTxt", System.Data.SqlDbType.VarChar, 15, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "GrantIDTxt", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@Original_Stamped", System.Data.SqlDbType.Timestamp, 8, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "Stamped", System.Data.DataRowVersion.Original, Nothing)})
        '
        'Label17
        '
        Me.Label17.Location = New System.Drawing.Point(14, 89)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(105, 28)
        Me.Label17.TabIndex = 225
        Me.Label17.Text = "Original Due Date"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label14
        '
        Me.Label14.Location = New System.Drawing.Point(59, 130)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(72, 16)
        Me.Label14.TabIndex = 3
        Me.Label14.Text = "# extensions"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'OrigDeadline
        '
        Me.OrigDeadline.BackColor = System.Drawing.SystemColors.Window
        Me.OrigDeadline.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.OrigDeadline.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainGrantBindingSource, "ExtensionOriginalDate", True))
        Me.OrigDeadline.Location = New System.Drawing.Point(131, 96)
        Me.OrigDeadline.Name = "OrigDeadline"
        Me.OrigDeadline.Size = New System.Drawing.Size(72, 16)
        Me.OrigDeadline.TabIndex = 9
        Me.OrigDeadline.Text = "OrigDeadline"
        '
        'Extensions
        '
        Me.Extensions.BackColor = System.Drawing.SystemColors.Window
        Me.Extensions.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Extensions.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainGrantBindingSource, "ExtensionCnt", True))
        Me.Extensions.Location = New System.Drawing.Point(133, 129)
        Me.Extensions.Name = "Extensions"
        Me.Extensions.Size = New System.Drawing.Size(46, 16)
        Me.Extensions.TabIndex = 12
        Me.Extensions.Text = "Extensions"
        '
        'btnHelp
        '
        Me.btnHelp.Location = New System.Drawing.Point(945, 5)
        Me.btnHelp.Name = "btnHelp"
        Me.btnHelp.Size = New System.Drawing.Size(25, 25)
        Me.btnHelp.TabIndex = 234
        Me.btnHelp.UseVisualStyleBackColor = True
        Me.btnHelp.Visible = False
        '
        'fldOrgNum
        '
        Me.fldOrgNum.BackColor = System.Drawing.SystemColors.Control
        Me.fldOrgNum.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.fldOrgNum.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainGrantBindingSource, "OrgNum", True))
        Me.fldOrgNum.Enabled = False
        Me.fldOrgNum.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fldOrgNum.ForeColor = System.Drawing.SystemColors.GrayText
        Me.fldOrgNum.Location = New System.Drawing.Point(642, 597)
        Me.fldOrgNum.Name = "fldOrgNum"
        Me.fldOrgNum.Size = New System.Drawing.Size(41, 13)
        Me.fldOrgNum.TabIndex = 500
        Me.fldOrgNum.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'CaseNum
        '
        Me.CaseNum.BackColor = System.Drawing.SystemColors.Control
        Me.CaseNum.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.CaseNum.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainGrantBindingSource, "CaseNum", True))
        Me.CaseNum.Enabled = False
        Me.CaseNum.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CaseNum.ForeColor = System.Drawing.SystemColors.GrayText
        Me.CaseNum.Location = New System.Drawing.Point(749, 597)
        Me.CaseNum.Name = "CaseNum"
        Me.CaseNum.Size = New System.Drawing.Size(41, 13)
        Me.CaseNum.TabIndex = 501
        Me.CaseNum.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label9
        '
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.SystemColors.ControlDark
        Me.Label9.Location = New System.Drawing.Point(694, 597)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(47, 14)
        Me.Label9.TabIndex = 419
        Me.Label9.Text = "Case #"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label11
        '
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.SystemColors.ControlDark
        Me.Label11.Location = New System.Drawing.Point(587, 597)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(47, 14)
        Me.Label11.TabIndex = 420
        Me.Label11.Text = "Org #"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.SystemColors.Highlight
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Controls.Add(Me.btnViewFile)
        Me.Panel2.Controls.Add(Me.btnSend)
        Me.Panel2.Controls.Add(Me.btnSaveExit)
        Me.Panel2.Controls.Add(Me.btnNew)
        Me.Panel2.Location = New System.Drawing.Point(732, 1)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(183, 39)
        Me.Panel2.TabIndex = 424
        '
        'Label37
        '
        Me.Label37.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label37.ForeColor = System.Drawing.SystemColors.ControlDark
        Me.Label37.Location = New System.Drawing.Point(808, 597)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(51, 14)
        Me.Label37.TabIndex = 426
        Me.Label37.Text = "MGI #"
        Me.Label37.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'ErrorProvider1
        '
        Me.ErrorProvider1.ContainerControl = Me
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.ForeColor = System.Drawing.Color.Maroon
        Me.Label6.Location = New System.Drawing.Point(13, 33)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(354, 13)
        Me.Label6.TabIndex = 503
        Me.Label6.Text = "The Case Manager is responsible for everything outside the blue sections."
        '
        'lblAwardKey
        '
        Me.lblAwardKey.AutoSize = True
        Me.lblAwardKey.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainGrantBindingSource, "AwardKey", True))
        Me.lblAwardKey.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAwardKey.ForeColor = System.Drawing.Color.DarkBlue
        Me.lblAwardKey.Location = New System.Drawing.Point(235, 11)
        Me.lblAwardKey.MinimumSize = New System.Drawing.Size(20, 0)
        Me.lblAwardKey.Name = "lblAwardKey"
        Me.lblAwardKey.Size = New System.Drawing.Size(20, 13)
        Me.lblAwardKey.TabIndex = 504
        '
        'Label53
        '
        Me.Label53.AutoSize = True
        Me.Label53.Location = New System.Drawing.Point(138, 11)
        Me.Label53.Name = "Label53"
        Me.Label53.Size = New System.Drawing.Size(95, 13)
        Me.Label53.TabIndex = 505
        Me.Label53.Text = "Grant Awarded ID:"
        '
        'miDelay
        '
        Me.miDelay.Index = 2
        Me.miDelay.Text = "Delay Start of Grant"
        '
        'frmMainGrant
        '
        Me.AcceptButton = Me.btnHelp
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.CausesValidation = False
        Me.ClientSize = New System.Drawing.Size(984, 641)
        Me.Controls.Add(Me.Label53)
        Me.Controls.Add(Me.lblAwardKey)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.fldMGI)
        Me.Controls.Add(Me.Label37)
        Me.Controls.Add(Me.btnDelete)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.fldOrgNum)
        Me.Controls.Add(Me.CaseNum)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.btnHelp)
        Me.Controls.Add(Me.StatusBar1)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.fldGotoOrg)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TabControl1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Menu = Me.MainMenu1
        Me.Name = "frmMainGrant"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Tag = "GRANT"
        Me.Text = "GRANT DETAIL"
        CType(Me.DsMainGrant1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.MainGrantBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlJerri1.ResumeLayout(False)
        Me.pnlJerri1.PerformLayout()
        CType(Me.grdMain, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsRecommend1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabControl1.ResumeLayout(False)
        Me.pgProcess.ResumeLayout(False)
        Me.pgProcess.PerformLayout()
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.pnlJerri3.ResumeLayout(False)
        Me.pnlJerri3.PerformLayout()
        Me.pgResources.ResumeLayout(False)
        Me.pgProgress.ResumeLayout(False)
        Me.pgProgress.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        CType(Me.StatusBarPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.StatusBarPanelID, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.StatusBarPanel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region 'windows

    'TODO CHECK RIGHT CLICK GRID LINE NOT GENERATE ERROR
#Region "Load"

    'ON LOAD
    Private Sub frmMainGrant_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles MyBase.Load

        Me.SuspendLayout()
        SetStatusBarText("loading")

SetMainDSConnection:
        Me.daspMainGrant.UpdateCommand.Connection = sc
        Me.daspMainGrant.SelectCommand.Connection = sc
        Me.daspGetRecommendations.SelectCommand.Connection = sc

SetDefaults:
        enumProcess = New structHeadings("Process", "PROCESS")
        enumResource = New structHeadings("Resouce", "RESOURCES")
        enumProgress = New structHeadings("Progress", "PROGRESS")

        ctlIdentify = Me.txtTypeofGrant
        ctlNeutral = Me.btnNew
        mainTopic = "Grant"
        mainDS = Me.DsMainGrant1
        maintbl = Me.DsMainGrant1.MainGrant
        mainBSrce = Me.MainGrantBindingSource
        mainDAdapt = Me.daspMainGrant

LoadCombos:
        'LOAD ALL STAFF COMBOs
        Dim ctl As Control, strType As String
        For Each ctl In Me.Controls
            strType = ctl.GetType.ToString
            Select Case strType
                Case Is = "InfoCtr.ComboBoxRelaxed"
                    isStaffCombo(ctl)
                Case Is = "System.Windows.Forms.Panel"
                    For Each cbo As Control In ctl.Controls
                        isStaffCombo(cbo)
                    Next cbo
                Case Is = "System.Windows.Forms.TabControl"
                    For Each pg As TabPage In ctl.Controls
                        For Each cbo As Control In pg.Controls
                            If cbo.GetType.ToString = "System.Windows.Forms.Panel" Then
                                For Each c As Control In cbo.Controls
                                    isStaffCombo(c)
                                Next c
                            Else
                                isStaffCombo(cbo)
                            End If
                        Next cbo
                    Next pg
                Case Else
            End Select
        Next ctl

FormSetup:
        'GRID TEXT BOXES ALLOW CLICK
        Dim tbx As DataGridTextBoxColumn
        For Each tbx In grdMain.TableStyles(0).GridColumnStyles
            AddHandler tbx.TextBox.MouseDown, New MouseEventHandler(AddressOf DataGridDouble)
        Next

        SetTabCaptions()
        Me.ResumeLayout()
        isLoaded = True
        Forms.Add(Me)
        SetStatusBarText("Done")

    End Sub

    'Load Staff Combox
    Private Sub isStaffCombo(ByRef ctl As Control)
        If ctl.GetType.ToString = "InfoCtr.ComboBoxRelaxed" And ctl.Name.ToString.Contains("StaffNum") Then ' <> "cboDetermination" Then
            modGlobalVar.LoadStaffCombo(ctl, False, StaffComboChoices.Selectable)
        End If
    End Sub

    'REFRESH DATA, COMBOS, AND GRIDS
    Public Sub Reload()
        Dim cmd As New SqlCommand("[luCaseNames]", sc)

        SetStatusBarText("Reloading")
ResetVars:
        Me.StatusBarPanelID.Text = "Grant Request ID: " & ThisID.ToString
        objHowClose = ObjClose.btnSaveExit

LoadCombos:
        tblCases.Clear()
        tblContacts.Clear()

        'LOAD CASES
        Try
            modGlobalVar.LoadCaseCombo(Me.cboCase, tblCases, localOrgID)
            tblCases.PrimaryKey = New DataColumn() {tblCases.Columns("CaseID")}
        Catch ex As Exception
            modGlobalVar.msg("ERROR: fill case combo", ex.Message & NextLine & "param: " & localOrgID.ToString, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        'LOAD SR CLERGY and PROJ DIR
        modGlobalVar.LoadContactCombo(Me.cboProjDir, tblContacts, localOrgID)
        Me.cboProjDir.DisplayMember = "ContactStaff"
        Me.cboProjDir.DataSource = tblContacts

        Me.DataView1.Table = tblContacts
        Me.DataView1.Sort = "PrimaryContact DESC, Active DESC, Staff DESC, Lastname, FirstName"
        Me.cboSrClergy.ValueMember = "ContactID"
        Me.cboSrClergy.DisplayMember = "ContactStaff"
        Me.cboSrClergy.DataSource = DataView1

FillGrid:
        FillSecondary()
        bDirty = False

closeall:
        Try
            sc.Close()
        Catch ex As System.Exception
        End Try

        SetStatusBarText("Done")

    End Sub

    'call Find Files after dataset is loaded
    Public Sub FindFiles()

        'ADD RELATED FILES TO MENU
        modPopup.FindFiles(GrantID, LinkGrantPath, ppFile, ehFile, Me.miOpenFile, Me.btnViewFile, My.Resources.btnAttached, Me.ToolTip1) ',nothing)

    End Sub

#End Region 'load

#Region "Update Main"

    'SAVE & EXIT
    Private Sub btnSaveExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnSaveExit.Click

        objHowClose = ObjClose.btnSaveExit
        Me.Close()
    End Sub

    'CLOSING & ask user & data validation & Release Form
    Private Sub frm_Closing(ByVal sender As Object, ByVal e As FormClosingEventArgs) _
               Handles MyBase.FormClosing

        Dim arCtls(0) As Control
        Dim ctl As Control
        Dim bChanges As Boolean

        If objHowClose = ObjClose.miDelete Then
            GoTo callupdate
        End If

LookForChanges:
        If bDirty = True Then
            bChanges = True
        Else
            bChanges = modGlobalVar.AnyChanges(ctlNeutral, mainBSrce, maintbl)
        End If

CheckCallingMethod:
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
                    'ctl = arCtls(0)
                    ''INSERT DEFAULT DATA - no, could be blank last name
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
            ClassOpenForms.frmMainGrant = Nothing 'reset global var
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
        Catch dbcx As Data.DBConcurrencyException
            modGlobalVar.msg("ERROR someone else changed the record", "Your changes will NOT be saved" & NextLine & dbcx.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
            DoUpdate = False
            '    Dim response As Windows.Forms.DialogResult
            '    response = MessageBox.Show(CreateMessage_1NoLastChanged(dbcx.Row, mainDAdapt, tConcurrency, maintbl), "Concurrency Exception BEWARE", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
            '    Select Case ProcessDialogResult_4(response, maintbl, mainDAdapt, tConcurrency)
            '        'Case Is = usrInput.Overwrite
            '        '    DoUpdate = True
            '        Case Is = usrInput.Reset
            '            Return False
            '        Case Is = usrInput.Cancel
            '            bDirty = True
            '            Return False
            '    End Select
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

    'CHECK REQUIRED FIELDS w Error Provider
    Private Function CheckRequired() As Control()
        'add the Neutral control to the array last to indicate rest are ok if its the first one
        Dim arCtls(0) As Control
        Dim i As Integer = 0
        arCtls(i) = ctlNeutral
        Return arCtls

    End Function

#End Region 'update main

#Region "MenuItems"

    'mi ALLOW CLOSE WITHOUT SAVING
    Private Sub miCloseForm_Click(ByVal sender As Object, ByVal e As System.EventArgs) _
        Handles miClose.Click
        objHowClose = ObjClose.miAskSave
        Me.Close()
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
            modGlobalVar.msg("ERROR: cancelling changes  ", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        SetStatusBarText("Changes Cancelled")
    End Sub

    'DELETE
    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
           Handles miDelete.Click ', btnDelete.Click

        If modGlobalVar.msg("CONFIRM DELETE", mainTopic & ": " + ctlIdentify.Text & NextLine & " will be marked for deletion and the window closed.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            ' mainDS.Tables(0).Rows(0).BeginEdit()
            Me.txtBriefIssue.Text = "DELETE: " & IsNull(Me.txtBriefIssue.Text, "")
            objHowClose = ObjClose.miDelete
            '  mainBSrce.EndEdit()
            Me.Close()
        End If
    End Sub

    'SEND POPUP
    Private Sub btnSend_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnSend.Click

        If Me.cboSrClergy.SelectedIndex = -1 Then
            modGlobalVar.msg("Cancelling Request", "you must enter a Sr Clergy first", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        Dim pp As ContextMenu = New ContextMenu
        pp.MenuItems.Clear()
        'LOAD POPUPMENU
        pp.MenuItems.Add("Create Grant Proposal Document", AddressOf MergeLetters)
        pp.MenuItems.Add("Request Final Report Letter", AddressOf MergeLetters)
        pp.MenuItems.Add("Reminder for Final Report Letter", AddressOf MergeLetters)
        pp.MenuItems.Add("Thankyou for Final Report Letter", AddressOf MergeLetters)

        pp.MenuItems(0).DefaultItem = True
        pp.Show(Me, PointToClient(Control.MousePosition))
    End Sub

    'SEND MERGE general
    Private Sub MergeLetters(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim datafileName As String = "DataDoc"
        Dim mergeTemplate As String
        Dim sheetLabel As String

        If sender.text = "Create Grant Proposal Document" Then
            If Me.DtDetermination.Text > "1/1/2006" And Me.cboDetermination.SelectedItem = "Approved" Then
                GrantProposalLetter()
            Else
                modGlobalVar.msg("Cancelling Request", "the grant must be approved and dated before creating a Grant Proposal document", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Me.cboDetermination.Focus()
            End If
            GoTo closeall
        End If

        'TODO peg the menu items enabled on if report has been received
        Select Case sender.text
            Case Is = "Request Final Report Letter"
                If Me.DtFinalReportRecd.Text.ToString = String.Empty Then

                Else
                    modGlobalVar.msg("cancelling your request", "report has already been received", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    GoTo CloseAll
                End If
                If Me.DtRemindFinalReport.Text.ToString = String.Empty Then
                Else
                    If modGlobalVar.msg("The Reminder Letter was already sent", Me.DtRemindFinalReport.Text.ToString & NextLine & "Are you sure you want to send the letter again" & NextLine &
                               "Click OK to overwrite the existing date.", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) = DialogResult.Cancel Then
                        GoTo closeall
                    End If
                End If
                mergeTemplate = "MergeInfoCtrToWord\MergeGrantRequestFinalRpt.dotx"
                sheetLabel = "RequestFinal"

            Case Is = "Reminder for Final Report Letter"
                If Me.DtFinalReportRecd.Text.ToString = String.Empty Then
                Else
                    modGlobalVar.msg("cancelling your request", "report has already been received", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    GoTo CloseAll
                End If
                mergeTemplate = "MergeInfoCtrToWord\MergeGrantRemindReturnFinalRpt.dotx"
                sheetLabel = "GrantReminder"

            Case Is = "Thankyou for Final Report Letter"
                If Me.DtFinalReportRecd.Text.ToString = String.Empty Then
                    modGlobalVar.msg("cancelling your request", "report has NOT been been received", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    GoTo CloseAll
                End If
                mergeTemplate = "MergeInfoCtrToWord\MergeGrantThankyouforFinalRpt.dotx"
                sheetLabel = "GrantThankyou"
                GoTo DoMerge
        End Select


DoMerge:
        If modPopup.StrmWriter("[MergeGrantLetter]", ThisID, datafileName, sheetLabel) Then
            SetStatusBarText("done streamwriter")
            MouseWait()
            If modPopup.DataToExcel(datafileName, sheetLabel) = String.Empty Then '"GrantLetter") = String.Empty Then
                modGlobalVar.msg("ERROR: grant DataToExcel", "MergeGrantLetter could not create datafile", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Else
                SetStatusBarText("done convert to excel")

                Try
                    modPopup.MergePerform(SOPPath & mergeTemplate, datafileName, sheetLabel)
                    SetStatusBarText("done merge")
                    'reset dates
                    If sheetLabel = "RequestFinal" Then
                        Me.DtRequestFinalReport.Text = Now()
                        Me.cboRequestFinalStaffNum.SelectedValue = usr
                        Me.miSave.PerformClick()
                    Else
                        If sheetLabel = "GrantReminder" Then
                            Me.DtRemindFinalReport.Text = Now()
                            Me.cboRemindFinalStaffNum.SelectedValue = usr
                            Me.miSave.PerformClick()
                        End If
                    End If
                Catch ex As Exception
                End Try
            End If
        End If
        '        modGlobalVar.Msg("final report has already been received", , "cancelling request")
        'Dim d As String = String.Empty
        'If Me.dtCheckMailed.Text = String.Empty Then
        'Else
        '    d = CType(DateDiff(DateInterval.Month, Now(), CType(Me.dtCheckMailed.Text, Date)) * -1, String)
        'End If
closeall:
        datafileName = Nothing
        mergeTemplate = Nothing
        sheetLabel = Nothing
        MouseDefault()
    End Sub

    'SEND MERGE GrantProposalLetter
    Private Sub GrantProposalLetter()
        Dim datafileName As String = "GrantDataDoc"
        Dim sheetLabel As String = "GrantProposal"
        Me.miSave.PerformClick()
        MouseWait()
        'write datafile
        If modPopup.StrmWriter("[MergeGrantLetter]", Me.lblAwardKey.Text, datafileName, "Proposal") Then
            SetStatusBarText("done streamwriter")
            'convert to excel
            If modPopup.DataToExcel(datafileName, sheetLabel) = String.Empty Then
                modGlobalVar.msg("ERROR: grant DataToExcel 2", "Grant Merge could not create datafile", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Else    'perform merge
                modPopup.MergePerform(SOPPath & "MergeInfoCtrToWord\MergeGrantProposal.dotx", datafileName, sheetLabel)
                SetStatusBarText("done merge")
            End If
        Else
            modGlobalVar.msg("ERROR: streamwriter", "merge grant", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
        SetStatusBarText("done Grant Proposal")
        MouseDefault()
    End Sub

    'NEW RECOMMENDATION
    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnNew.Click

        Dim iorg, icase As Integer
        Dim str As String
        If Me.fldOrgNum.Text = String.Empty Then
            iorg = 0
        Else
            iorg = localOrgID
        End If
        If Me.cboCase.Text = String.Empty Then
            icase = 0
        Else
            icase = CType(Me.cboCase.SelectedValue, Integer)
        End If

        str = "INSERT INTO tblResourceRecommend( RecommendDate, GrantNum, OrgNum, CaseNum, RecommendedBy, Used) VALUES (GETDATE(), '" & ThisID & "', " & localOrgID & ", " & icase & ", 'Grant App', 'Yes'); SELECT @@Identity"

        If Not SCConnect() Then
            Exit Sub
        End If

        '        Dim myTrans As SqlClient.SqlTransaction = sc.BeginTransaction()
        Dim cmd As New SqlClient.SqlCommand(str, sc) ', myTrans)
        Dim newID As Integer
        Try
            newID = cmd.ExecuteScalar()
            ''   myTrans.Commit()
        Catch exce As Exception
            modGlobalVar.msg("ERROR: new recommendation", exce.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
            '    myTrans.Rollback()
            sc.Close()
            Exit Sub
        End Try

        sc.Close()
OpenForm:
        modGlobalVar.OpenMainRecommend(newID, "new recommendation ", localOrgID)

    End Sub

#End Region 'edit buttons

#Region "GridDoubleclick"

    'OPEN MAIN DETAIL FORMS FROM DATAGRID
    Private Sub DataGridDouble(ByVal sender As Object, ByVal e As MouseEventArgs)

        If (DateTime.Now < modGlobalVar.CheckDouble(sender, e).AddMilliseconds(SystemInformation.DoubleClickTime)) Then
            Select Case UCase(TabControl1.SelectedTab.Tag.ToString)
                Case Is = "RESOURCES"
                    Me.StatusBarPanel1.Text = "Searching for Resources"
                    '  openmainconversation(me.grdMain.Item(grdmain.CurrentRowIndex,0),grdmain.Item....
                    Me.miGotoResource.PerformClick()
                    'Case Is = strDGM2
                Case Else
                    modGlobalVar.msg("ERROR: tab not found", TabControl1.SelectedTab.Tag, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Select
        End If
        Me.StatusBarPanel1.Text = "Done"
        '    Catch
        '  End Try
    End Sub

    'CLEAR SELECTION FROM DATAGRIDS AND LOSE FOCUS (note: selection stays with arrow)
    Private Sub grdUnselect_Click(ByVal sender As Object, ByVal e As System.EventArgs) _
            Handles MyBase.Click, grdMain.LostFocus
        If grdMain.CurrentRowIndex > -1 Then
            Me.grdMain.UnSelect(grdMain.CurrentRowIndex)
            Me.grdMain.NavigateBack()
        End If
        '   Me.btnDelete.Visible = True
    End Sub

    'call open RESOURCE
    Private Sub grdMain_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdMain.DoubleClick
        miGotoResource.PerformClick()
    End Sub

#End Region 'grid dblclick

#Region "General"

    'COPY CURRENT ID
    Private Sub StatusBar1_DoubleClick(sender As System.Object, e As System.EventArgs) Handles StatusBar1.DoubleClick
        Clipboard.SetText(ThisID)
    End Sub

    'CALL FILL SECONDARY from TAB 
    Private Sub TabControl1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TabControl1.SelectedIndexChanged

        Select Case TabControl1.SelectedTab.Tag
            Case "RESOURCES"
                FillSecondary()
                SetTabCaptions()
        End Select

    End Sub

    'UNDO
    Protected Overrides Function ProcessDialogKey(ByVal keyData As Keys) As Boolean
        '   MsgBox("grant form dialogkey")
        If keyData = System.Windows.Forms.Keys.Escape Then
            modPopup.UndoCtl(Me.ActiveControl)
            '  bChanged = True
            Return True  ' True means we've processed the escape key
        Else
            Return MyBase.ProcessDialogKey(keyData)
        End If
    End Function

    'RIGHT CLICK MENU
    Private Sub textbox_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) _
        Handles txtNotes.MouseDown, txtBriefIssue.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Right Then
            Dim pp As New ClassRTBContextMenu(sender)
            pp.Show(Me, PointToClient(Control.MousePosition))
        End If
    End Sub

    'SET STATUS BAR LEFT TEXT
    Private Sub SetStatusBarText(ByVal str As String)
        Me.StatusBarPanel1.Text = str
    End Sub

    'HELP - general
    Private Sub miHelp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles miHelp.Click 'btnHelp.Click,
        modGlobalVar.msg("GRANT HELP", "TO ADD NEW GRANT" & NextLine & NextLine & "Go to Organization Edit window and click New button" & NextLine & NextLine & _
        "The Case Manager is responsible for all the grant fields except the ones in blue boxes." & NextLine & NextLine & _
        "The Determination Date triggers the Quarterly Report Due Dates." & NextLine & NextLine & _
         "EIN: the checkbox indicates if we have the congregation's EIN, which is entered on the Organization Detail window", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    'Help - DELAY
    Private Sub miDelay_Click(sender As System.Object, e As System.EventArgs) Handles miDelay.Click
        MsgBox("to DELAY START OF GRANT and set later Quarterly Due dates:" & NextLine &
                "Use the Determination Date field; the Final Report Due Date will be one year after the date you set here." & NextLine &
                "Then be sure to SET THE DATE BACK to the actual approval date so our reports will be accurate, and say NO to changing the dates." & NextLine &
                 NextLine & "to EXTEND the Final Due Date of an active grant, use the Extension button on the Dates & Extension tab.", , "Grant Help")
    End Sub

    'Help - DEFINITIONS
    Private Sub miDefinition_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles miDefinition.Click
        modPopup.ShowDefinitions("GrantDetail")
    End Sub

    'IS FINAL REPORT RECEIVED
    Private Function CheckRptRecd() As Boolean
        If Me.DtFinalReportRecd.Text.ToString = String.Empty Then
            Return False
        Else
            Return True
        End If
    End Function

    'EIN VISIBILITY from CBO DETERMINATION
    Private Sub cboDetermination_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboDetermination.SelectedIndexChanged

SetEINFlag:
        If sender.text = "Approved" And Me.chkEIN.Checked = False Then
            Me.flagEIN.Visible = True
        Else
            Me.flagEIN.Visible = False
        End If
DisableLegacyRescinded:
        If sender.text = "Rescinded" And isLoaded = True Then
            'sender.selectedindex = -1
            Me.ErrorProvider1.SetError(sender, "please select another Determination ")
            msg(MsgCodes.InvalidSelection) ' MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            sender.focus()
        Else
            Me.ErrorProvider1.SetError(sender, "")
        End If

    End Sub

    'validate Determination
    Private Sub cboDetermination_Leave(sender As Object, e As System.EventArgs) Handles cboDetermination.Leave
        If Me.ErrorProvider1.GetError(sender) = String.Empty Then
        Else
            sender.focus()
            msg("'Rescinded' is no longer a valid option.", " See " & GrantDir.StaffName & " if you have questions.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
    End Sub

#End Region 'general

#Region "FIll Secondary"

    'FILL GRID
    Private Sub FillSecondary()
        Dim i As Integer = 0
        Me.DsRecommend1.Clear()
        '  Me.daspGetRecommendations.SelectCommand.Parameters("@IDFld").Value = "Grant"
        Me.daspGetRecommendations.SelectCommand.Parameters("@IDStr").Value = ThisID
        If Me.cboCase.Text = String.Empty Then
        Else
            i = Me.cboCase.SelectedValue
        End If
        Me.daspGetRecommendations.SelectCommand.Parameters("@IDCase").Value = i
        Try
            Me.daspGetRecommendations.Fill(Me.DsRecommend1)
        Catch ex As Exception
            modGlobalVar.msg("ERROR: Fill Recommendations", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    'RESET TAB CAPTIONS w  COUNT
    Public Sub SetTabCaptions()
SetTabCaptionsWCount:
        Dim cmdCntID As New SqlCommand
        Dim i As Integer = 0

        cmdCntID.Connection = sc
        If Not SCConnect() Then
            Exit Sub
        End If

        'RECOMMENDATIONS
        cmdCntID.CommandText = "SELECT COUNT(RecommendID) FROM tblResourceRecommend WHERE GrantNum = '" & ThisID & "' AND (recommendedby is null OR not (RecommendedBy LIKE 'delete%'))"
        Try
            i = cmdCntID.ExecuteScalar()
        Catch ex As Exception
            modGlobalVar.msg("ERROR: count Recommend", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Me.TabControl1.TabPages(1).Text = i.ToString() & "  FUNDED RESOURCES" ' & strData1

        sc.Close()
        cmdCntID.Dispose()

    End Sub

#End Region  'fill secondary

#Region "Open Forms"

    'OPEN MAIN ORG FORM - see module
    Private Sub OpenOrg(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles fldGotoOrg.DoubleClick, miGotoOrg.Click ', btnGotoOrg.Click
        Dim i As Integer
        i = CType(Me.fldOrgNum.Text, Integer)
        modGlobalVar.OpenMainOrg(i, Me.fldGotoOrg.Text) ', ClassOpenForms.frmMainOrg.DsMainOrg1, ClassOpenForms.frmMainOrg.daMainOrg, ClassOpenForms.frmMainOrg.miSave, "MainOrg", ClassOpenForms.frmMainOrg.daMainOrg.SelectCommand.Parameters("@OrgID"))
    End Sub

    'OPEN CASE Detail
    Private Sub fldGotoCase_DblClick(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles fldGotoCase.DoubleClick 'cboContact.Click, cboCase.Click

        Me.StatusBarPanel1.Text = "Opening " & sender.tag & " Detail window"
        Select Case sender.name.ToString
            '            Case Is = "lblContact"
            '               OpenMainContact(cboContact.SelectedValue, cboContact.DisplayMember, fldGotoOrg.Text, Me.OrgNum.Text)
            '          'TODO CONSISTENCIZE sometimes firstname first, othertimes lastname first
            Case Is = "fldGotoCase"
                modGlobalVar.OpenMainCase(cboCase.SelectedValue, cboCase.DisplayMember, Me.fldGotoOrg.Text, Me.fldOrgNum.Text)
            Case Else
                ' modGlobalVar.Msg(sender.name.ToString)
        End Select
        Me.StatusBarPanel1.Text = "Done"
    End Sub

    'OPEN RESOURE Detail
    Private Sub miGotoResource_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles miGotoResource.Click
          modGlobalVar.OpenMainRecommend(Me.grdMain.Item(Me.grdMain.CurrentRowIndex, 0), Me.grdMain.Item(Me.grdMain.CurrentRowIndex, 2), Me.fldOrgNum.Text)
    End Sub

    'OPEN MGI APPLICATION Detail
    Private Sub miMGI_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles miMGI.Click, fldMGI.Click
        OpenMGIForm(fldMGI.Text, Me.txtTypeofGrant.Text, Me.fldGotoOrg.Text, Me.fldOrgNum.Text)
    End Sub

#End Region  'open forms

#Region "Validation"
    'Validate form??
    Private Sub frmMainGrant_Validating(sender As System.Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.Validating
        '   modGlobalVar.Msg("in validating")
        Select Case modGlobalVar.ValidateBoundDD(sender, False, Me.ErrorProvider1, ObjClose.CloseByControl)
            Case Is = usrInput.OK
                e.Cancel = False
            Case Is = usrInput.Retry 'invalid selection like heading
                e.Cancel = True
                'Case Is = usrInput.Search  'typed invalid text  - note: add new only through org detail
                '    modGlobalVar.Msg("to enter a new contact, from the Organization Detail window, click the New Button", , cboContact.Text & " Not Found")
                '    e.Cancel = True

                'Case Is = usrInput.Ignore  'empty - confirm none
                '    If modGlobalVar.Msg("Do you know with whom you had this conversation?", MessageBoxButtons.YesNo, "Missing Contact") = DialogResult.Yes Then
                '        e.Cancel = True
                '    Else
                '        e.Cancel = False
                '    End If
            Case Else
                e.Cancel = True
        End Select

    End Sub

    'VALIDATE DATE
    Private Sub DateValidation(sender As Object, e As System.ComponentModel.CancelEventArgs) _
          Handles DtCheckRequested.Validating, DtCheckMailed.Validating, DtFinalDraftReceived.Validating, DtFirstDraftReceived.Validating, DtFollowupReportComplete.Validating, DtNoticeMailed.Validating, DtNotifyCall.Validating, DtRemindFinalReport.Validating, DtRequestFinalReport.Validating, DtSignedAgreementRecd.Validating, DtSixMonthCall.Validating, DtClosed.Validating, DtFinalRptReceived.Validating, DtDetermination.Validating, DtFinalReportRecd.Validating
        'DateTimePicker1.Validating, DateTimePicker2.Validating,
        e.Cancel = modGlobalVar.ValidateDateA(sender, Me.ErrorProvider1)

    End Sub

    'VALIDATE NOT REQUIRED CBO StaffNum
    Private Sub cboCompleteStaffNum_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) _
            Handles cboGrantStaffNum.Validating, cboFollowupStaffNum.Validating, cboChkMailedStaffNum.Validating, cboFinalDraftRecdStaffNum.Validating, cboFollowupComplStaffNum.Validating, cboFirstDraftRecdStaffNum.Validating, cboRptRecdStaffNum.Validating, cboMidpointCallStaffNum.Validating, cboRequestFinalStaffNum.Validating, cboRemindFinalStaffNum.Validating, cboFinalRecdStaffNum2.Validating, cboAgreementRecdStaffNum.Validating, cboNoticeMailedStaffNum.Validating, cboNotifiedCallStaffNum.Validating, cboDeterminationStaffNum.Validating

        If modGlobalVar.ValidateBoundDD(sender, False, Me.ErrorProvider1, ObjClose.CloseByControl) = usrInput.OK Then
            e.Cancel = False
        Else
            e.Cancel = True
        End If

    End Sub

#End Region 'validation

#Region "Generate Dates"

    'ASSIGN DUEDATE, QUARTER DATES CALL
    Private Sub dtDetermination_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles DtDetermination.Validated 'leave = is called twice with modGlobalVar.Msg!   'dtCheckMailed.Leave
        Dim iExtraDays, iMonths As Integer

        If DtDetermination.Text = String.Empty Or DtDetermination.Text = "1/1/1911" Then
            GoTo CloseAll
        End If
        If Me.cboDetermination.Text <> "Approved" Then
            GoTo Closeall
        End If
        ' Dim SendEmail As New ClassEmail
        Dim strb As New StringBuilder
        Dim strbStaff As New StringBuilder
        Dim drw As DataRow
        'this one works
        'If DeleteDate(sender, Me.DsMainGrant1.tblGrant.Rows(0).Item("CheckMailedDt")) Then
        m = 1 + DateDiff(DateInterval.Month, #1/1/1900#, CType(DtDetermination.Text, Date)) ' = number of months from 1900 to month after determination date

SetGrantAwardKeyID:
        GetAwardKey() 'needed for Jerri's acceptance merge letter

SetQuarterlyDates:
        Select Case Me.txtTypeofGrant.Text
            Case Is = "Youth Ministry Grant", "YMGI Grant"  '18 months for grant
                iExtraDays = 45
                iMonths = 18
            Case Else   '1 yr for grant
                iExtraDays = 0
                iMonths = 12
        End Select
        If Me.lblQ1.Text = DateAdd(DateInterval.Month, m + 3, DateAdd(DateInterval.Day, iExtraDays, #1/1/1900#)).ToShortDateString Then
            GoTo EmailCaseMgr
        Else
            If Me.lblQ1.Text = String.Empty Then
            Else
                If modGlobalVar.msg("WAIT - report due dates already set.", "This grant already has report due dates set. " & NextLine & "Are you sure you want to overwrite the existing dates?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                Else
                    GoTo CloseAll
                End If
            End If
        End If
        MouseWait()
        Me.StatusBarPanel1.Text = "Getting Quarterly Due Dates"

        If GetQuarterlyDates(iExtraDays, iMonths) Then
            '  bChanged = True
            bDirty = True
        Else
            modGlobalVar.msg("ERROR: get Qtry Dates", "dates not assigned", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            GoTo CloseAll
        End If

EmailCaseMgr:
        'REMOVE AFTER TESTING 4/15
        Me.StatusBarPanel1.Text = "Preparing email to Case Mgr"
        ' For x As Integer = 1 To 3
        ' strb.AppendLine("Quarter " & x.ToString & " Report Due: " & arqtrDates(x).ToString & "%0D%0A") ' & Chr(10) & Chr(13)) 'NewLine) 'System.enviroment.newline)
        strb.AppendFormat("{0} {1}", "  Quarter 1 Report Due: ", Me.lblQ1.Text) ', Environment.NewLine)
        strb.AppendLine("%0A")
        strb.AppendFormat("{0} {1}", "  Quarter 2 Report Due: ", Me.lblQ2.Text) ', Environment.NewLine)
        strb.AppendLine("%0A")
        strb.AppendFormat("{0} {1}", "  Quarter 3 Report Due: ", Me.lblQ3.Text) ', Environment.NewLine)
        strb.AppendLine("%0A")
        strb.AppendFormat("{0} {1}", "  Final Report Due: ", Me.lblQFinal.Text) ', Environment.NewLine)

        'select names
        Dim strCaseMgr As String
        Dim strGrantStaff As String

        Try
            If Me.cboCase.SelectedIndex > -1 Then
                drw = tblStaff.Rows.Find(tblCases.Rows.Find(Me.cboCase.SelectedValue)("CaseMgrNum"))
                 strCaseMgr = drw(tblStaff.Columns("StaffFirstNameFirst"), DataRowVersion.Current).ToString
            Else : strCaseMgr = "no case attached to this grant"
            End If
        Catch ex As Exception
            modGlobalVar.msg("ERROR: getting case mgr  ", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Try
            If Me.cboGrantStaffNum.SelectedIndex > -1 Then
                drw = tblStaff.Rows.Find(Me.cboGrantStaffNum.SelectedValue)
                strGrantStaff = drw(tblStaff.Columns("StaffFirstNameFirst"), DataRowVersion.Current).ToString
             Else
                strGrantStaff = "None"
            End If

        Catch ex As Exception
            modGlobalVar.msg("ERROR: getting grant staff ", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        strbStaff.Append(strCaseMgr)
        If strCaseMgr = strGrantStaff Or strGrantStaff = "None" Then
        Else
            strbStaff.Append(";" & strGrantStaff.ToString)
        End If

        Me.StatusBarPanel1.Text = "sending email"
        modPopup.EmailOutlook(strbStaff.ToString, "Quarterly Report Due Dates for: " & Me.fldGotoOrg.Text.Substring(0, 20), strb.ToString, "")

        '...........................................
        'SummaryReportDue = CheckMailed + 367
        'SixMonthDue = CheckMailed + 182
        ''RequestReport = CheckMailed + 335
        ''SummaryReminder = CheckMailed + 395
        '......................................

OpenProposalLetter:  'jerri only
        If usr = GrantAdmin.StaffID And Me.DtDetermination.Text > "1/1/2006" And Me.cboDetermination.SelectedItem = "Approved" Then
            Me.StatusBarPanel1.Text = "Opening Grant Proposal Document"
            GrantProposalLetter()
            Me.StatusBarPanel1.Text = "Done"
        End If

CloseAll:
        Try
            m = Nothing
            '   SendEmail = Nothing
            strb = Nothing
            strbStaff = Nothing
            drw = Nothing
        Catch ex As Exception
            'modGlobalVar.Msg(ex.Message, , "ERROR:  ")
        End Try
        ' Array.Clear(arQtrDates, 0, 5)
        Me.StatusBarPanel1.Text = "Done"
        MouseDefault()
    End Sub

    'QUARTER DATES DO
    Private Function GetQuarterlyDates(ByVal iExtraDays As Integer, ByVal iMonths As Integer) As Boolean
        '==========NEW DATES 2011 ===================
        'first of month after grant approved, + 1st of months quarterly after that.
        'due date is next year last day of month previous to 1st of first month this year = last day of approved month next year
        '  On Error GoTo err
        'FIRST OF NEXT MONTH
        '  Dim m As Integer = 1 + DateDiff(DateInterval.Month, CType("1/1/1900", Date), dt) ' = number of months from 1900 to month after determination date
        '  Dim dtStart As Date = CType(DateAdd(DateInterval.Month, m + 1, CType("1/1/1900", Date)), Date)    'will this be first of month?

        Try
            mainBSrce.Current("Quarter1DueDate") = DateAdd(DateInterval.Month, m + 3, DateAdd(DateInterval.Day, iExtraDays, #1/1/1900#)).ToShortDateString
            mainBSrce.Current("Quarter2DueDate") = DateAdd(DateInterval.Month, m + 6, DateAdd(DateInterval.Day, iExtraDays * 2, #1/1/1900#)).ToShortDateString
            mainBSrce.Current("Quarter3DueDate") = DateAdd(DateInterval.Month, m + 9, DateAdd(DateInterval.Day, iExtraDays * 3, #1/1/1900#)).ToShortDateString
            mainBSrce.Current("Quarter4DueDate") = DateAdd(DateInterval.Day, -1, DateAdd(DateInterval.Month, m + iMonths, #1/1/1900#)).ToShortDateString    '1 year less a day
            mainBSrce.EndEdit()
        Catch ex As Exception
            modGlobalVar.msg("date assignment error", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
            GoTo err
        End Try
        Return True
err:
        Return False
    End Function

    'EXTEND DUE DATE
    Private Sub btnExtension_Click(ByVal sender As Object, ByVal e As System.EventArgs) _
            Handles btnExtension.Click
        Dim dt, newDt As Date
        'allow user to change date; default 6 mo; retain old date and count how many times extended
        'doesn't flag change
        dt = Me.lblQFinal.Text
        '  Me.fldOrigDueDate.Text = Me.fldQtr4.Text
        ' Me.txtNotes.Text = 
        mainDS.Tables(0).Rows(0)("Notes") = IsNull(Me.txtNotes.Text, "") & " Extended: " & InputBox("Why was this extension required", "Add to Note field...", "Congregation needed more time because ")

        Try
            'Me.fldExtensions.Text =
            mainDS.Tables(0).Rows(0)("Extensioncnt") = (CType(IsNull(Me.fldExtensions.Text, 0), Integer) + 1).ToString
            If Me.fldOrigDueDate.Text > " " Then
            Else
                Me.fldOrigDueDate.Text = dt.ToString
                '  mainDS.Tables(0).Rows(0)("ExtensionOriginalDate") = dt
            End If
        Catch ex As Exception
            modGlobalVar.msg("extension error", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Try
            newDt = CType(InputBox("default is 60 days from last due date", "enter new Congregation Final Report Due Date", DateAdd(DateInterval.Day, 60, dt)), Date)
            Me.fldQtr4.Text = newDt
            Me.lblQFinal.Text = newDt
        Catch ex As Exception
            modGlobalVar.msg("ERROR: Final date", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        bDirty = True
    End Sub

    'ASSIGN AWARD Key ie second Grant ID
    Private Sub GetAwardKey()
        '    'TODO Generate Award#
        Dim cmd As New SqlCommand

        Dim newNum As Integer = 0
        Dim strShort As String

        If DtDetermination.Text = String.Empty Then
            Exit Sub
        End If
        If Me.lblAwardKey.Text > " " Then
            Exit Sub
        End If

        'If modGlobalVar.Msg("Has the grant been Approved?", MessageBoxButtons.YesNo, "Incomplete data") = DialogResult.No Then
        '    Exit Sub
        'Else
        'Me.cboDetermination.SelectedIndex = Me.cboDetermination.FindString("Approved")
CheckDateReYearEndOverlap:
        If Year(Me.DtDetermination.Text) = Year(Today) Then
            cmd.CommandText = ("SELECT GrantApprovedNumber, GrantTypeSuffix FROM luGrantTokentbl WHERE GrantTypeName = '" & Me.txtTypeofGrant.Text & "'")
            cmd.Connection = sc
            If Not SCConnect() Then
                Exit Sub
            End If

            Dim drdr = cmd.ExecuteReader
            drdr.Read()
            newNum = drdr.GetValue(0) + 1
            strShort = drdr.GetString(1)
            drdr.Close()
            'reset token table number
            cmd.CommandText = "UPDATE luGrantTokenTbl SET GrantApprovedNumber = " & newNum & " FROM luGrantTokentbl WHERE GrantTypeName = '" & Me.txtTypeofGrant.Text & "'"
            cmd.ExecuteNonQuery()
            sc.Close()
        Else 'is from last year don't update token table
            cmd.CommandText = ("SELECT MAX(PARSENAME(REPLACE(AwardKey, '-', '.'), 2)) + 1 AS Mid,  luGrantTokenTbl.GrantTypeSuffix   FROM tblGrant LEFT OUTER JOIN luGrantTokenTbl ON luGrantTokenTbl.GrantTypeName = tblGrant.TypeofGrant" _
                         & " WHERE (LEFT(AwardKey, 4) = '" & Year(Me.DtDetermination.Text) & "') AND (TypeofGrant = '" & Me.txtTypeofGrant.Text & "') GROUP BY luGrantTokenTbl.GrantTypeSuffix")
            cmd.Connection = sc
            If Not SCConnect() Then
                Exit Sub
            End If
            Dim drdr = cmd.ExecuteReader
            drdr.Read()
            newNum = drdr.GetValue(0)
            strShort = drdr.GetString(1)
            drdr.Close()
            sc.Close()
        End If

        'set grant num
        ' Me.lblAwardKey.Text = Year(CType(dtDetermination.Text, Date)) & "-" & Format(newNum, "000") & "-" & strShort
        mainBSrce.Current("AwardKey") = Year(CType(DtDetermination.Text, Date)) & "-" & Format(newNum, "000") & "-" & strShort
        mainBSrce.ResetCurrentItem()
        bDirty = True
    End Sub

#End Region 'generate dates

#Region "Attach Files"

    'COPY FILE to shared drive
    Private Sub btnAttach_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles miAttach.Click

        Try
            modPopup.AttachFiles("Grant", LinkGrantPath, GrantID)
        Catch ex As Exception
            modGlobalVar.msg("error attach Grant file", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        modPopup.FindFiles(GrantID, LinkGrantPath, ppFile, ehFile, Me.miOpenFile, Me.btnViewFile, My.Resources.btnAttached, Me.ToolTip1, Nothing)

        SetStatusBarText("Done")

    End Sub

    'OPEN FILE
    Private Sub ehOpenFile(ByVal obj As Object, ByVal ea As EventArgs)

        If obj.Text = "Attach File" Then
            Me.miAttach.PerformClick()
            Exit Sub
        End If

         If OpenFile(modPopup.GetFileName(LinkGrantPath, GrantID.ToString & " " & obj.text & ".*", True)) Then
            SetStatusBarText("file opened")
        Else
            SetStatusBarText("network error")
        End If

    End Sub

    'SHOW FILE POPUP
    Private Sub btnViewFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnViewFile.Click
        If sender.Tag = "0" Then
            ehOpenFile(sender, Nothing)
        End If
        ppFile.Show(Me, New Point(600, 10))
    End Sub

#End Region 'attach files

End Class