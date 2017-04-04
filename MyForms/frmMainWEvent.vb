Option Explicit On
Imports System.Data.SqlClient
Imports System.Drawing.Printing
Imports System.Windows.Forms
Imports System.Text
Imports System.IO
Imports Excel = Microsoft.Office.interop.excel
Imports Office = Microsoft.Office.Core
Imports cExternalFile = ClassLibrary1.ExternalFiles

'---------------------------------
'TODO LATER change cbo to datasource rather than item list
'TODO Master Checklist not settin subsequent dates/regions correctly
'-------------------------------
Public Class frmMainWEvent
    Inherits System.Windows.Forms.Form

    Dim iOpeningCount As Integer
    Public hti As System.Windows.Forms.DataGrid.HitTestInfo
    Dim dv As DataView
    Dim iRowCount As Integer
    Dim strPhone As String
    Dim HostOverviewName As String
    Public isLoaded As Boolean = False
    Dim statusM As String
    Dim tblRegistrantList As New DataTable
    Dim bCancelClose As Boolean
    Public bChanged As Boolean = False
    Dim cmgr As CurrencyManager
    Public i As Integer = 0
    Dim ppFile As New ContextMenu
    Dim ehFile As EventHandler = AddressOf ehOpenFile
    Dim dirs As String()
    Dim LinkEventPath As String = LinkedPath & "Events\"
    Dim LinkOrgPath As String = LinkedPath & "Organizations\"
    Dim tblCaterer As New DataTable("tblCaterer")
    Dim dt As New DataTable("tblListNames")
    Dim strAttachedPathName, strOverviewPathName As String
    '===== for Detail form  closing routines
    Dim ctlIdentify As Control 'fields for delete and messages
    Dim ctlNeutral As Control 'will never be invalidated
    Dim objHowClose As Short 'identify object calling close
    Dim mainTopic As String 'name of entity of this form - case, contact, etc
    Dim mainDS As DataSet 'for generic module calls like CloseDetailForm
    Dim mainTbl As DataTable
    Dim bDirty As Boolean 'crg combobox 
    Public ThisID, LocalOrgID As Integer
    Dim mainBSrce As System.Windows.Forms.BindingSource
    Dim MstrChecklistFileExt As String

    '...........
    '
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

    Protected WithEvents grdMain As System.Windows.Forms.DataGrid
    ' Friend WithEvents grdMainReg As System.Windows.Forms.DataGrid
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents editInstructorPhone As System.Windows.Forms.TextBox
    Friend WithEvents pgRegistration As System.Windows.Forms.TabPage
    Friend WithEvents SqlDeleteCommand As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlInsertCommand As System.Data.SqlClient.SqlCommand
    Friend WithEvents dsMainWEvent As InfoCtr.dsMainWEvent
    '  Friend WithEvents dsGetEventRegList2a As InfoCtr.dsGetEventRegList2
    '  Friend WithEvents getEventRegistrantList2TableAdapter As InfoCtr.dsGetEventRegList2TableAdapters.getEventRegistrantList2TableAdapter
    Friend WithEvents MainEventSetupTableAdapter As InfoCtr.dsMainWEventTableAdapters.MainEventSetupTableAdapter
    Friend WithEvents lblTotRegistration As System.Windows.Forms.Label
    Friend WithEvents editTotRegistration As System.Windows.Forms.TextBox
    Friend WithEvents lblDescription As System.Windows.Forms.Label
    Friend WithEvents editDescription As System.Windows.Forms.TextBox
    Friend WithEvents editFollowUpDone As InfoCtr.DateTextBox
    Friend WithEvents lblFollowUpDone As System.Windows.Forms.Label
    Friend WithEvents lblFollowupStaffTxt As System.Windows.Forms.Label
    Friend WithEvents lblCRGNum As System.Windows.Forms.Label
    Friend WithEvents FldGotoCaseName As System.Windows.Forms.Label
    Friend WithEvents editMasterName As System.Windows.Forms.TextBox
    Friend WithEvents editNotes As System.Windows.Forms.TextBox
    Friend WithEvents lblNotes As System.Windows.Forms.Label
    Friend WithEvents lblBookGiveaway As System.Windows.Forms.Label
    Friend WithEvents pnlAddress As System.Windows.Forms.Panel
    Friend WithEvents lblCity As System.Windows.Forms.Label
    Friend WithEvents editCity As System.Windows.Forms.TextBox
    Friend WithEvents lblGotoOrg As System.Windows.Forms.Label
    Friend WithEvents lblAddress1 As System.Windows.Forms.Label
    Friend WithEvents editStreet As System.Windows.Forms.TextBox
    Friend WithEvents cboLocation As InfoCtr.ComboBoxRelaxed
    Friend WithEvents miNew As System.Windows.Forms.MenuItem
    Friend WithEvents btnNew As System.Windows.Forms.Button
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents btnHelp As System.Windows.Forms.Button
    Friend WithEvents MenuItem6 As System.Windows.Forms.MenuItem
    Friend WithEvents mmSend As System.Windows.Forms.MenuItem
    Friend WithEvents miMergeNametag As System.Windows.Forms.MenuItem
    Friend WithEvents miMergeInfoLttr As System.Windows.Forms.MenuItem
    Friend WithEvents btnUpdate As System.Windows.Forms.Button
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents miUpdateNametag As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mmiCheckNametag As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mmiUncheckNametag As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents miUpdateInfoLetter As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mmiCheckInformation As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mmiUncheckInformation As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents miUpdateAttendance As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mmiCheckAttended As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mmiUncheckAttended As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents miUpdate As System.Windows.Forms.MenuItem
    Friend WithEvents btnSaveExit As System.Windows.Forms.Button
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents btnReport As System.Windows.Forms.Button
    Friend WithEvents miHelp As System.Windows.Forms.MenuItem
    Friend WithEvents miDef As System.Windows.Forms.MenuItem
    Friend WithEvents dtpFirstDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents miViewAll As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem2 As System.Windows.Forms.MenuItem
    Friend WithEvents miSendEmail As System.Windows.Forms.MenuItem
    Friend WithEvents miEmailRegistered As System.Windows.Forms.MenuItem
    Friend WithEvents miEmailAttended As System.Windows.Forms.MenuItem
    Friend WithEvents TextBox3 As System.Windows.Forms.TextBox
    Friend WithEvents miRegistrant As System.Windows.Forms.MenuItem
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents ErrorProvider1 As System.Windows.Forms.ErrorProvider
    Friend WithEvents fldEventID As System.Windows.Forms.TextBox
    Friend WithEvents lblEventID As System.Windows.Forms.Label
    Friend WithEvents fldEventNID As System.Windows.Forms.TextBox
    Friend WithEvents miAttach As System.Windows.Forms.MenuItem
    Friend WithEvents miMergeTemplate As System.Windows.Forms.MenuItem
    Friend WithEvents miEmailDatafile As System.Windows.Forms.MenuItem
    Friend WithEvents miReport As System.Windows.Forms.MenuItem
    Friend WithEvents EditLocation As System.Windows.Forms.TextBox
    Friend WithEvents txtLocationURL As System.Windows.Forms.TextBox
    Friend WithEvents CheckBox1 As System.Windows.Forms.CheckBox
    Friend WithEvents cboCaterer As InfoCtr.ComboBoxRelaxed
    Friend WithEvents editCateringNotes As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents miFirstEmail As System.Windows.Forms.MenuItem
    Friend WithEvents miInfoLttrNoEmail As System.Windows.Forms.MenuItem
    Friend WithEvents pgChecklist As System.Windows.Forms.TabPage
    Friend WithEvents fldRegID As System.Windows.Forms.Label
    Friend WithEvents TextBox2 As System.Windows.Forms.TextBox
    Friend WithEvents btnRefreshChecklist As System.Windows.Forms.Button
    Friend WithEvents miMasterChecklist As System.Windows.Forms.MenuItem
    Friend WithEvents dgvChecklist As System.Windows.Forms.DataGridView
    Friend WithEvents pnlCaterer As System.Windows.Forms.Panel
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents miCancellationRpt As System.Windows.Forms.MenuItem
    Friend WithEvents miOpenFile As System.Windows.Forms.MenuItem
    Friend WithEvents lblChecklist As System.Windows.Forms.Label
    Friend WithEvents lblOrgNum As System.Windows.Forms.Label
    'Friend WithEvents dsGetEventRegList2a1 As InfoCtr.dsGetEventRegList2a
    Friend WithEvents DataGridTableStyle2 As System.Windows.Forms.DataGridTableStyle
    Friend WithEvents cboCRG As InfoCtr.ComboBoxRelaxed
    Friend WithEvents cboType As InfoCtr.ComboBoxRelaxed
    Friend WithEvents editTitle As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cboRegion As InfoCtr.ComboBoxRelaxed
    Friend WithEvents lblTypeofEvent As System.Windows.Forms.Label
    Friend WithEvents lblInstructor As System.Windows.Forms.Label
    Friend WithEvents editInstructor As System.Windows.Forms.TextBox
    Friend WithEvents cboTimeZone As InfoCtr.ComboBoxRelaxed
    Friend WithEvents editBookGiveaway As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents editDiscount As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents editTeamMin As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents fldSKU As System.Windows.Forms.TextBox
    Friend WithEvents DataGridTableStyle1 As System.Windows.Forms.DataGridTableStyle
    Friend WithEvents DataGridTextBoxColumn1 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn2 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn3 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn4 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn5 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn6 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn7 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn9 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn10 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn11 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn12 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn13 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn8 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn14 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents miPending As System.Windows.Forms.MenuItem
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents fldPending As System.Windows.Forms.TextBox
    Friend WithEvents DataGridTextBoxColumn15 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents miHost As System.Windows.Forms.MenuItem
    Friend WithEvents btnHostOverview As System.Windows.Forms.Button
    Friend WithEvents MenuItem4 As System.Windows.Forms.MenuItem
    Friend WithEvents miRptPayment As System.Windows.Forms.MenuItem
    Friend WithEvents miRptRefund As System.Windows.Forms.MenuItem
    Friend WithEvents StatusBarPanelID As System.Windows.Forms.StatusBarPanel
    Friend WithEvents MainEventSetupBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents btnOpenFile As System.Windows.Forms.Button
    Friend WithEvents miEmailNotAttend As System.Windows.Forms.MenuItem
    Friend WithEvents txtOnlineLink As System.Windows.Forms.TextBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents lblNametag As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents fldRegonlineID As System.Windows.Forms.TextBox
    Friend WithEvents TextBox4 As System.Windows.Forms.TextBox
    ' Friend WithEvents dsGetEventRegList2a As InfoCtr.dsGetEventRegList2
    Friend WithEvents btnCreateChecklist As System.Windows.Forms.Button
   
    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents lblInstructorPhone As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents lblAddress2 As System.Windows.Forms.Label
    Friend WithEvents lblState As System.Windows.Forms.Label
    Friend WithEvents lblZipCode As System.Windows.Forms.Label
    Friend WithEvents editRoom As System.Windows.Forms.TextBox
    Friend WithEvents editState As System.Windows.Forms.TextBox
    Friend WithEvents editZipCode As System.Windows.Forms.TextBox
    Friend WithEvents lblLocationPhone As System.Windows.Forms.Label
    Friend WithEvents lblLocationContact As System.Windows.Forms.Label
    Friend WithEvents editLocationPhone As System.Windows.Forms.TextBox
    Friend WithEvents editLocationContact As System.Windows.Forms.TextBox
    Friend WithEvents editCaterer As System.Windows.Forms.TextBox
    Friend WithEvents fldGotoCaterer As System.Windows.Forms.TextBox
    Friend WithEvents editCatererPhone As System.Windows.Forms.TextBox
    Friend WithEvents lblCatererPhone As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents lblRegion As System.Windows.Forms.Label
    Friend WithEvents lblDates As System.Windows.Forms.Label
    Friend WithEvents editDates As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Public WithEvents editTimeofEvent As System.Windows.Forms.TextBox
    Friend WithEvents lblTimeofEvent As System.Windows.Forms.Label
    Friend WithEvents editMaximum As System.Windows.Forms.TextBox
    Friend WithEvents lblFee As System.Windows.Forms.Label
    Friend WithEvents lblMaximum As System.Windows.Forms.Label
    Friend WithEvents editFee As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents pgSetup As System.Windows.Forms.TabPage
    ' Friend WithEvents daMainEdEvent As System.Data.SqlClient.SqlDataAdapter
    ' Friend WithEvents dsMainEd1 As WindowsApplication11.dsMainEdEvents
    '  Friend WithEvents ds1 As WindowsApplication11.dsMainReg
    Friend WithEvents MainMenu1 As System.Windows.Forms.MainMenu
    Friend WithEvents MenuItem1 As System.Windows.Forms.MenuItem
    Friend WithEvents miClose As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem3 As System.Windows.Forms.MenuItem
    Friend WithEvents miSave As System.Windows.Forms.MenuItem
    Friend WithEvents miDelete As System.Windows.Forms.MenuItem
    '  Friend WithEvents DsMainReg1 As WindowsApplication11.dsMainReg
    '  Friend WithEvents none As System.Data.SqlClient.SqlConnection
    'Friend WithEvents SqlConnection1 As System.Data.SqlClient.SqlConnection
    '  Friend WithEvents sqlEdEventSEL As System.Data.SqlClient.SqlCommand
    Friend WithEvents StatusBar1 As System.Windows.Forms.StatusBar
    Friend WithEvents mmReport As System.Windows.Forms.MenuItem
    Friend WithEvents miCancel As System.Windows.Forms.MenuItem
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents StatusBarPanel1 As System.Windows.Forms.StatusBarPanel
    Friend WithEvents StatusBarPanel2 As System.Windows.Forms.StatusBarPanel
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMainWEvent))
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.pgSetup = New System.Windows.Forms.TabPage()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.MainEventSetupBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.dsMainWEvent = New InfoCtr.dsMainWEvent()
        Me.lblNametag = New System.Windows.Forms.Label()
        Me.txtOnlineLink = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.fldPending = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.cboCRG = New InfoCtr.ComboBoxRelaxed()
        Me.btnCreateChecklist = New System.Windows.Forms.Button()
        Me.pnlCaterer = New System.Windows.Forms.Panel()
        Me.editCaterer = New System.Windows.Forms.TextBox()
        Me.fldGotoCaterer = New System.Windows.Forms.TextBox()
        Me.cboCaterer = New InfoCtr.ComboBoxRelaxed()
        Me.editCateringNotes = New System.Windows.Forms.TextBox()
        Me.editCatererPhone = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.lblCatererPhone = New System.Windows.Forms.Label()
        Me.CheckBox1 = New System.Windows.Forms.CheckBox()
        Me.TextBox3 = New System.Windows.Forms.TextBox()
        Me.lblTotRegistration = New System.Windows.Forms.Label()
        Me.editTotRegistration = New System.Windows.Forms.TextBox()
        Me.lblDescription = New System.Windows.Forms.Label()
        Me.editDescription = New System.Windows.Forms.TextBox()
        Me.editFollowUpDone = New InfoCtr.DateTextBox()
        Me.lblFollowUpDone = New System.Windows.Forms.Label()
        Me.lblFollowupStaffTxt = New System.Windows.Forms.Label()
        Me.lblCRGNum = New System.Windows.Forms.Label()
        Me.FldGotoCaseName = New System.Windows.Forms.Label()
        Me.editMasterName = New System.Windows.Forms.TextBox()
        Me.editNotes = New System.Windows.Forms.TextBox()
        Me.editMaximum = New System.Windows.Forms.TextBox()
        Me.lblNotes = New System.Windows.Forms.Label()
        Me.lblMaximum = New System.Windows.Forms.Label()
        Me.editBookGiveaway = New System.Windows.Forms.TextBox()
        Me.lblBookGiveaway = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.editInstructorPhone = New System.Windows.Forms.TextBox()
        Me.btnUpdate = New System.Windows.Forms.Button()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.miUpdateNametag = New System.Windows.Forms.ToolStripMenuItem()
        Me.mmiCheckNametag = New System.Windows.Forms.ToolStripMenuItem()
        Me.mmiUncheckNametag = New System.Windows.Forms.ToolStripMenuItem()
        Me.miUpdateInfoLetter = New System.Windows.Forms.ToolStripMenuItem()
        Me.mmiCheckInformation = New System.Windows.Forms.ToolStripMenuItem()
        Me.mmiUncheckInformation = New System.Windows.Forms.ToolStripMenuItem()
        Me.miUpdateAttendance = New System.Windows.Forms.ToolStripMenuItem()
        Me.mmiCheckAttended = New System.Windows.Forms.ToolStripMenuItem()
        Me.mmiUncheckAttended = New System.Windows.Forms.ToolStripMenuItem()
        Me.pgRegistration = New System.Windows.Forms.TabPage()
        Me.grdMain = New System.Windows.Forms.DataGrid()
        Me.DataGridTableStyle1 = New System.Windows.Forms.DataGridTableStyle()
        Me.DataGridTextBoxColumn1 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn2 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn3 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn5 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn6 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn4 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn14 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn7 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn9 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn10 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn11 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn13 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn8 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn12 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn15 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.pgChecklist = New System.Windows.Forms.TabPage()
        Me.lblChecklist = New System.Windows.Forms.Label()
        Me.dgvChecklist = New System.Windows.Forms.DataGridView()
        Me.btnRefreshChecklist = New System.Windows.Forms.Button()
        Me.fldRegID = New System.Windows.Forms.Label()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.DataGridTableStyle2 = New System.Windows.Forms.DataGridTableStyle()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lblAddress2 = New System.Windows.Forms.Label()
        Me.lblState = New System.Windows.Forms.Label()
        Me.lblZipCode = New System.Windows.Forms.Label()
        Me.editRoom = New System.Windows.Forms.TextBox()
        Me.editState = New System.Windows.Forms.TextBox()
        Me.editZipCode = New System.Windows.Forms.TextBox()
        Me.lblLocationPhone = New System.Windows.Forms.Label()
        Me.lblLocationContact = New System.Windows.Forms.Label()
        Me.editLocationPhone = New System.Windows.Forms.TextBox()
        Me.editLocationContact = New System.Windows.Forms.TextBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.editDiscount = New System.Windows.Forms.TextBox()
        Me.editTeamMin = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cboTimeZone = New InfoCtr.ComboBoxRelaxed()
        Me.cboType = New InfoCtr.ComboBoxRelaxed()
        Me.editTitle = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cboRegion = New InfoCtr.ComboBoxRelaxed()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.lblTypeofEvent = New System.Windows.Forms.Label()
        Me.dtpFirstDate = New System.Windows.Forms.DateTimePicker()
        Me.lblInstructor = New System.Windows.Forms.Label()
        Me.pnlAddress = New System.Windows.Forms.Panel()
        Me.btnHostOverview = New System.Windows.Forms.Button()
        Me.lblOrgNum = New System.Windows.Forms.Label()
        Me.txtLocationURL = New System.Windows.Forms.TextBox()
        Me.EditLocation = New System.Windows.Forms.TextBox()
        Me.cboLocation = New InfoCtr.ComboBoxRelaxed()
        Me.lblCity = New System.Windows.Forms.Label()
        Me.editCity = New System.Windows.Forms.TextBox()
        Me.lblGotoOrg = New System.Windows.Forms.Label()
        Me.lblAddress1 = New System.Windows.Forms.Label()
        Me.editStreet = New System.Windows.Forms.TextBox()
        Me.editInstructor = New System.Windows.Forms.TextBox()
        Me.lblDates = New System.Windows.Forms.Label()
        Me.editDates = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.editTimeofEvent = New System.Windows.Forms.TextBox()
        Me.lblTimeofEvent = New System.Windows.Forms.Label()
        Me.lblFee = New System.Windows.Forms.Label()
        Me.editFee = New System.Windows.Forms.TextBox()
        Me.lblRegion = New System.Windows.Forms.Label()
        Me.SqlDeleteCommand = New System.Data.SqlClient.SqlCommand()
        Me.SqlInsertCommand = New System.Data.SqlClient.SqlCommand()
        Me.MainMenu1 = New System.Windows.Forms.MainMenu(Me.components)
        Me.MenuItem1 = New System.Windows.Forms.MenuItem()
        Me.miNew = New System.Windows.Forms.MenuItem()
        Me.miClose = New System.Windows.Forms.MenuItem()
        Me.miAttach = New System.Windows.Forms.MenuItem()
        Me.miOpenFile = New System.Windows.Forms.MenuItem()
        Me.miMergeTemplate = New System.Windows.Forms.MenuItem()
        Me.MenuItem3 = New System.Windows.Forms.MenuItem()
        Me.miSave = New System.Windows.Forms.MenuItem()
        Me.miCancel = New System.Windows.Forms.MenuItem()
        Me.miDelete = New System.Windows.Forms.MenuItem()
        Me.MenuItem2 = New System.Windows.Forms.MenuItem()
        Me.miViewAll = New System.Windows.Forms.MenuItem()
        Me.miPending = New System.Windows.Forms.MenuItem()
        Me.mmSend = New System.Windows.Forms.MenuItem()
        Me.miMergeNametag = New System.Windows.Forms.MenuItem()
        Me.miMergeInfoLttr = New System.Windows.Forms.MenuItem()
        Me.miRegistrant = New System.Windows.Forms.MenuItem()
        Me.miInfoLttrNoEmail = New System.Windows.Forms.MenuItem()
        Me.miUpdate = New System.Windows.Forms.MenuItem()
        Me.mmReport = New System.Windows.Forms.MenuItem()
        Me.miReport = New System.Windows.Forms.MenuItem()
        Me.miHost = New System.Windows.Forms.MenuItem()
        Me.miMasterChecklist = New System.Windows.Forms.MenuItem()
        Me.miCancellationRpt = New System.Windows.Forms.MenuItem()
        Me.MenuItem4 = New System.Windows.Forms.MenuItem()
        Me.miRptPayment = New System.Windows.Forms.MenuItem()
        Me.miRptRefund = New System.Windows.Forms.MenuItem()
        Me.miSendEmail = New System.Windows.Forms.MenuItem()
        Me.miEmailAttended = New System.Windows.Forms.MenuItem()
        Me.miEmailRegistered = New System.Windows.Forms.MenuItem()
        Me.miEmailNotAttend = New System.Windows.Forms.MenuItem()
        Me.miEmailDatafile = New System.Windows.Forms.MenuItem()
        Me.miFirstEmail = New System.Windows.Forms.MenuItem()
        Me.MenuItem6 = New System.Windows.Forms.MenuItem()
        Me.miHelp = New System.Windows.Forms.MenuItem()
        Me.miDef = New System.Windows.Forms.MenuItem()
        Me.StatusBar1 = New System.Windows.Forms.StatusBar()
        Me.StatusBarPanel1 = New System.Windows.Forms.StatusBarPanel()
        Me.StatusBarPanelID = New System.Windows.Forms.StatusBarPanel()
        Me.StatusBarPanel2 = New System.Windows.Forms.StatusBarPanel()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.btnNew = New System.Windows.Forms.Button()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.btnSaveExit = New System.Windows.Forms.Button()
        Me.btnOpenFile = New System.Windows.Forms.Button()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.btnReport = New System.Windows.Forms.Button()
        Me.ErrorProvider1 = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.fldEventID = New System.Windows.Forms.TextBox()
        Me.lblEventID = New System.Windows.Forms.Label()
        Me.fldEventNID = New System.Windows.Forms.TextBox()
        Me.btnHelp = New System.Windows.Forms.Button()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.fldSKU = New System.Windows.Forms.TextBox()
        Me.MainEventSetupTableAdapter = New InfoCtr.dsMainWEventTableAdapters.MainEventSetupTableAdapter()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.fldRegonlineID = New System.Windows.Forms.TextBox()
        Me.TextBox4 = New System.Windows.Forms.TextBox()
        Me.TabControl1.SuspendLayout()
        Me.pgSetup.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.MainEventSetupBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dsMainWEvent, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlCaterer.SuspendLayout()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.pgRegistration.SuspendLayout()
        CType(Me.grdMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pgChecklist.SuspendLayout()
        CType(Me.dgvChecklist, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.pnlAddress.SuspendLayout()
        CType(Me.StatusBarPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.StatusBarPanelID, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.StatusBarPanel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel4.SuspendLayout()
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TabPage3
        '
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Size = New System.Drawing.Size(632, 502)
        Me.TabPage3.TabIndex = 1
        Me.TabPage3.Text = "Registrations"
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.pgSetup)
        Me.TabControl1.Controls.Add(Me.pgRegistration)
        Me.TabControl1.Controls.Add(Me.pgChecklist)
        Me.TabControl1.Location = New System.Drawing.Point(353, 62)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(795, 540)
        Me.TabControl1.SizeMode = System.Windows.Forms.TabSizeMode.FillToRight
        Me.TabControl1.TabIndex = 69
        Me.TabControl1.TabStop = False
        '
        'pgSetup
        '
        Me.pgSetup.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pgSetup.Controls.Add(Me.Panel2)
        Me.pgSetup.Controls.Add(Me.btnUpdate)
        Me.pgSetup.Location = New System.Drawing.Point(4, 22)
        Me.pgSetup.Name = "pgSetup"
        Me.pgSetup.Size = New System.Drawing.Size(787, 514)
        Me.pgSetup.TabIndex = 0
        Me.pgSetup.Tag = "SETUP"
        Me.pgSetup.Text = "EVENT SETUP"
        Me.pgSetup.UseVisualStyleBackColor = True
        '
        'Panel2
        '
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel2.Controls.Add(Me.TextBox1)
        Me.Panel2.Controls.Add(Me.lblNametag)
        Me.Panel2.Controls.Add(Me.txtOnlineLink)
        Me.Panel2.Controls.Add(Me.Label16)
        Me.Panel2.Controls.Add(Me.Label14)
        Me.Panel2.Controls.Add(Me.fldPending)
        Me.Panel2.Controls.Add(Me.Label7)
        Me.Panel2.Controls.Add(Me.cboCRG)
        Me.Panel2.Controls.Add(Me.btnCreateChecklist)
        Me.Panel2.Controls.Add(Me.pnlCaterer)
        Me.Panel2.Controls.Add(Me.CheckBox1)
        Me.Panel2.Controls.Add(Me.TextBox3)
        Me.Panel2.Controls.Add(Me.lblTotRegistration)
        Me.Panel2.Controls.Add(Me.editTotRegistration)
        Me.Panel2.Controls.Add(Me.lblDescription)
        Me.Panel2.Controls.Add(Me.editDescription)
        Me.Panel2.Controls.Add(Me.editFollowUpDone)
        Me.Panel2.Controls.Add(Me.lblFollowUpDone)
        Me.Panel2.Controls.Add(Me.lblFollowupStaffTxt)
        Me.Panel2.Controls.Add(Me.lblCRGNum)
        Me.Panel2.Controls.Add(Me.FldGotoCaseName)
        Me.Panel2.Controls.Add(Me.editMasterName)
        Me.Panel2.Controls.Add(Me.editNotes)
        Me.Panel2.Controls.Add(Me.editMaximum)
        Me.Panel2.Controls.Add(Me.lblNotes)
        Me.Panel2.Controls.Add(Me.lblMaximum)
        Me.Panel2.Controls.Add(Me.editBookGiveaway)
        Me.Panel2.Controls.Add(Me.lblBookGiveaway)
        Me.Panel2.Controls.Add(Me.Label4)
        Me.Panel2.Controls.Add(Me.editInstructorPhone)
        Me.Panel2.Location = New System.Drawing.Point(8, 6)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(779, 503)
        Me.Panel2.TabIndex = 20
        '
        'TextBox1
        '
        Me.TextBox1.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainEventSetupBindingSource, "NametagEventName", True))
        Me.TextBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox1.Location = New System.Drawing.Point(595, 249)
        Me.TextBox1.MaxLength = 200
        Me.TextBox1.Multiline = True
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(160, 41)
        Me.TextBox1.TabIndex = 234
        Me.ToolTip1.SetToolTip(Me.TextBox1, "Limit: 200 characters")
        '
        'MainEventSetupBindingSource
        '
        Me.MainEventSetupBindingSource.DataMember = "MainEventSetup"
        Me.MainEventSetupBindingSource.DataSource = Me.dsMainWEvent
        '
        'dsMainWEvent
        '
        Me.dsMainWEvent.DataSetName = "dsMainWEvent"
        Me.dsMainWEvent.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'lblNametag
        '
        Me.lblNametag.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNametag.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblNametag.Location = New System.Drawing.Point(503, 250)
        Me.lblNametag.Name = "lblNametag"
        Me.lblNametag.Size = New System.Drawing.Size(86, 37)
        Me.lblNametag.TabIndex = 235
        Me.lblNametag.Text = "Print on Nametag:"
        Me.lblNametag.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtOnlineLink
        '
        Me.txtOnlineLink.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainEventSetupBindingSource, "OnlineLearningLink", True))
        Me.txtOnlineLink.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOnlineLink.Location = New System.Drawing.Point(503, 145)
        Me.txtOnlineLink.MaxLength = 200
        Me.txtOnlineLink.Multiline = True
        Me.txtOnlineLink.Name = "txtOnlineLink"
        Me.txtOnlineLink.Size = New System.Drawing.Size(254, 19)
        Me.txtOnlineLink.TabIndex = 13
        Me.ToolTip1.SetToolTip(Me.txtOnlineLink, "Limit: 200 characters")
        '
        'Label16
        '
        Me.Label16.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Label16.Location = New System.Drawing.Point(412, 138)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(85, 30)
        Me.Label16.TabIndex = 233
        Me.Label16.Text = "Online Learning Link:"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label14
        '
        Me.Label14.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(379, 5)
        Me.Label14.Margin = New System.Windows.Forms.Padding(0)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(91, 35)
        Me.Label14.TabIndex = 231
        Me.Label14.Tag = ""
        Me.Label14.Text = "Pending" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Registrations:"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'fldPending
        '
        Me.fldPending.AcceptsReturn = True
        Me.fldPending.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainEventSetupBindingSource, "Pending", True))
        Me.fldPending.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fldPending.Location = New System.Drawing.Point(473, 12)
        Me.fldPending.Name = "fldPending"
        Me.fldPending.ReadOnly = True
        Me.fldPending.Size = New System.Drawing.Size(33, 21)
        Me.fldPending.TabIndex = 3
        Me.fldPending.TabStop = False
        Me.fldPending.Tag = ""
        Me.ToolTip1.SetToolTip(Me.fldPending, "Un-matched registrations from Download")
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.ForeColor = System.Drawing.SystemColors.Highlight
        Me.Label7.Location = New System.Drawing.Point(18, 295)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(76, 13)
        Me.Label7.TabIndex = 227
        Me.Label7.Text = "Caterer Details"
        '
        'cboCRG
        '
        Me.cboCRG.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboCRG.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboCRG.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.MainEventSetupBindingSource, "CRGNum", True))
        Me.cboCRG.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCRG.DropDownWidth = 500
        Me.cboCRG.FormattingEnabled = True
        Me.cboCRG.Location = New System.Drawing.Point(106, 143)
        Me.cboCRG.Name = "cboCRG"
        Me.cboCRG.RestrictContentToListItems = True
        Me.cboCRG.Size = New System.Drawing.Size(270, 21)
        Me.cboCRG.TabIndex = 10
        Me.cboCRG.Tag = "CRG"
        '
        'btnCreateChecklist
        '
        Me.btnCreateChecklist.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCreateChecklist.Location = New System.Drawing.Point(380, 250)
        Me.btnCreateChecklist.Margin = New System.Windows.Forms.Padding(0)
        Me.btnCreateChecklist.Name = "btnCreateChecklist"
        Me.btnCreateChecklist.Size = New System.Drawing.Size(107, 44)
        Me.btnCreateChecklist.TabIndex = 229
        Me.btnCreateChecklist.TabStop = False
        Me.btnCreateChecklist.Text = "Create Master Checklist"
        Me.btnCreateChecklist.UseVisualStyleBackColor = True
        Me.btnCreateChecklist.Visible = False
        '
        'pnlCaterer
        '
        Me.pnlCaterer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pnlCaterer.Controls.Add(Me.editCaterer)
        Me.pnlCaterer.Controls.Add(Me.fldGotoCaterer)
        Me.pnlCaterer.Controls.Add(Me.cboCaterer)
        Me.pnlCaterer.Controls.Add(Me.editCateringNotes)
        Me.pnlCaterer.Controls.Add(Me.editCatererPhone)
        Me.pnlCaterer.Controls.Add(Me.Label6)
        Me.pnlCaterer.Controls.Add(Me.lblCatererPhone)
        Me.pnlCaterer.Location = New System.Drawing.Point(9, 300)
        Me.pnlCaterer.Name = "pnlCaterer"
        Me.pnlCaterer.Size = New System.Drawing.Size(756, 129)
        Me.pnlCaterer.TabIndex = 100
        '
        'editCaterer
        '
        Me.editCaterer.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainEventSetupBindingSource, "Caterer", True))
        Me.editCaterer.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.editCaterer.Location = New System.Drawing.Point(90, 50)
        Me.editCaterer.MaxLength = 50
        Me.editCaterer.Name = "editCaterer"
        Me.editCaterer.Size = New System.Drawing.Size(168, 21)
        Me.editCaterer.TabIndex = 1
        '
        'fldGotoCaterer
        '
        Me.fldGotoCaterer.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.fldGotoCaterer.ForeColor = System.Drawing.Color.ForestGreen
        Me.fldGotoCaterer.Location = New System.Drawing.Point(10, 50)
        Me.fldGotoCaterer.Name = "fldGotoCaterer"
        Me.fldGotoCaterer.ReadOnly = True
        Me.fldGotoCaterer.Size = New System.Drawing.Size(74, 20)
        Me.fldGotoCaterer.TabIndex = 177
        Me.fldGotoCaterer.Tag = "Caterer"
        Me.fldGotoCaterer.Text = "Caterer Name:"
        Me.ToolTip1.SetToolTip(Me.fldGotoCaterer, "Doubleclick to Add or Edit Caterer info")
        '
        'cboCaterer
        '
        Me.cboCaterer.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboCaterer.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboCaterer.DisplayMember = "NameCity"
        Me.cboCaterer.DropDownWidth = 350
        Me.cboCaterer.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboCaterer.ForeColor = System.Drawing.Color.IndianRed
        Me.cboCaterer.FormattingEnabled = True
        Me.cboCaterer.Location = New System.Drawing.Point(28, 15)
        Me.cboCaterer.Name = "cboCaterer"
        Me.cboCaterer.RestrictContentToListItems = True
        Me.cboCaterer.Size = New System.Drawing.Size(230, 21)
        Me.cboCaterer.TabIndex = 0
        Me.cboCaterer.Text = "Select Caterer here"
        Me.ToolTip1.SetToolTip(Me.cboCaterer, "Caterer dropdown list is filtered by Region.")
        Me.cboCaterer.ValueMember = "CatererName"
        '
        'editCateringNotes
        '
        Me.editCateringNotes.AcceptsReturn = True
        Me.editCateringNotes.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainEventSetupBindingSource, "CatererFood", True))
        Me.editCateringNotes.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.editCateringNotes.Location = New System.Drawing.Point(334, 17)
        Me.editCateringNotes.MaxLength = 100
        Me.editCateringNotes.Multiline = True
        Me.editCateringNotes.Name = "editCateringNotes"
        Me.editCateringNotes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.editCateringNotes.Size = New System.Drawing.Size(404, 96)
        Me.editCateringNotes.TabIndex = 3
        '
        'editCatererPhone
        '
        Me.editCatererPhone.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainEventSetupBindingSource, "CatererPhone", True))
        Me.editCatererPhone.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.editCatererPhone.Location = New System.Drawing.Point(90, 82)
        Me.editCatererPhone.MaxLength = 20
        Me.editCatererPhone.Name = "editCatererPhone"
        Me.editCatererPhone.Size = New System.Drawing.Size(168, 21)
        Me.editCatererPhone.TabIndex = 2
        '
        'Label6
        '
        Me.Label6.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Label6.Location = New System.Drawing.Point(262, 15)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(66, 34)
        Me.Label6.TabIndex = 226
        Me.Label6.Text = "Catering Notes:"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblCatererPhone
        '
        Me.lblCatererPhone.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblCatererPhone.Location = New System.Drawing.Point(5, 80)
        Me.lblCatererPhone.Name = "lblCatererPhone"
        Me.lblCatererPhone.Size = New System.Drawing.Size(79, 23)
        Me.lblCatererPhone.TabIndex = 179
        Me.lblCatererPhone.Text = "Caterer Phone:"
        Me.lblCatererPhone.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.DataBindings.Add(New System.Windows.Forms.Binding("Checked", Me.MainEventSetupBindingSource, "EventCancelled", True))
        Me.CheckBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CheckBox1.Location = New System.Drawing.Point(558, 12)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(167, 19)
        Me.CheckBox1.TabIndex = 2
        Me.CheckBox1.Text = "This event was Cancelled."
        Me.CheckBox1.UseVisualStyleBackColor = True
        '
        'TextBox3
        '
        Me.TextBox3.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainEventSetupBindingSource, "FollowUpStaffTxt", True))
        Me.TextBox3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox3.Location = New System.Drawing.Point(106, 464)
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.Size = New System.Drawing.Size(169, 21)
        Me.TextBox3.TabIndex = 121
        '
        'lblTotRegistration
        '
        Me.lblTotRegistration.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotRegistration.Location = New System.Drawing.Point(153, 8)
        Me.lblTotRegistration.Margin = New System.Windows.Forms.Padding(0)
        Me.lblTotRegistration.Name = "lblTotRegistration"
        Me.lblTotRegistration.Size = New System.Drawing.Size(160, 35)
        Me.lblTotRegistration.TabIndex = 222
        Me.lblTotRegistration.Tag = ""
        Me.lblTotRegistration.Text = "Staff planning to attend, or count from Hosted event."
        Me.lblTotRegistration.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'editTotRegistration
        '
        Me.editTotRegistration.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainEventSetupBindingSource, "AdditionalPeople", True, DataSourceUpdateMode.OnValidation, ""))
        Me.editTotRegistration.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.editTotRegistration.Location = New System.Drawing.Point(316, 12)
        Me.editTotRegistration.Name = "editTotRegistration"
        Me.editTotRegistration.Size = New System.Drawing.Size(33, 21)
        Me.editTotRegistration.TabIndex = 1
        Me.editTotRegistration.Tag = "record attendance here if no registrations"
        Me.ToolTip1.SetToolTip(Me.editTotRegistration, "use for events with no registration required.")
        '
        'lblDescription
        '
        Me.lblDescription.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDescription.Location = New System.Drawing.Point(15, 176)
        Me.lblDescription.Name = "lblDescription"
        Me.lblDescription.Size = New System.Drawing.Size(86, 23)
        Me.lblDescription.TabIndex = 217
        Me.lblDescription.Text = "Description:"
        Me.lblDescription.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'editDescription
        '
        Me.editDescription.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainEventSetupBindingSource, "Description", True))
        Me.editDescription.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.editDescription.Location = New System.Drawing.Point(107, 183)
        Me.editDescription.MaxLength = 3000
        Me.editDescription.Multiline = True
        Me.editDescription.Name = "editDescription"
        Me.editDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.editDescription.Size = New System.Drawing.Size(648, 58)
        Me.editDescription.TabIndex = 20
        Me.ToolTip1.SetToolTip(Me.editDescription, "limit: 3,000 characters")
        '
        'editFollowUpDone
        '
        Me.editFollowUpDone.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainEventSetupBindingSource, "FollowUpDone", True))
        Me.editFollowUpDone.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.editFollowUpDone.Location = New System.Drawing.Point(446, 469)
        Me.editFollowUpDone.Name = "editFollowUpDone"
        Me.editFollowUpDone.Size = New System.Drawing.Size(119, 21)
        Me.editFollowUpDone.TabIndex = 122
        '
        'lblFollowUpDone
        '
        Me.lblFollowUpDone.Location = New System.Drawing.Point(298, 469)
        Me.lblFollowUpDone.Name = "lblFollowUpDone"
        Me.lblFollowUpDone.Size = New System.Drawing.Size(142, 20)
        Me.lblFollowUpDone.TabIndex = 215
        Me.lblFollowUpDone.Text = "Date Follow Up Complete:"
        Me.lblFollowUpDone.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblFollowupStaffTxt
        '
        Me.lblFollowupStaffTxt.Location = New System.Drawing.Point(24, 469)
        Me.lblFollowupStaffTxt.Name = "lblFollowupStaffTxt"
        Me.lblFollowupStaffTxt.Size = New System.Drawing.Size(76, 20)
        Me.lblFollowupStaffTxt.TabIndex = 214
        Me.lblFollowupStaffTxt.Text = "FollowupStaff:"
        '
        'lblCRGNum
        '
        Me.lblCRGNum.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCRGNum.Location = New System.Drawing.Point(40, 141)
        Me.lblCRGNum.Name = "lblCRGNum"
        Me.lblCRGNum.Size = New System.Drawing.Size(56, 23)
        Me.lblCRGNum.TabIndex = 208
        Me.lblCRGNum.Text = "CRG:"
        Me.lblCRGNum.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'FldGotoCaseName
        '
        Me.FldGotoCaseName.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FldGotoCaseName.Location = New System.Drawing.Point(5, 250)
        Me.FldGotoCaseName.Name = "FldGotoCaseName"
        Me.FldGotoCaseName.Size = New System.Drawing.Size(93, 33)
        Me.FldGotoCaseName.TabIndex = 209
        Me.FldGotoCaseName.Text = "Event " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Master Name:"
        Me.FldGotoCaseName.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'editMasterName
        '
        Me.editMasterName.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainEventSetupBindingSource, "MasterWorkshopName", True))
        Me.editMasterName.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.editMasterName.Location = New System.Drawing.Point(107, 250)
        Me.editMasterName.MaxLength = 50
        Me.editMasterName.Multiline = True
        Me.editMasterName.Name = "editMasterName"
        Me.editMasterName.Size = New System.Drawing.Size(269, 41)
        Me.editMasterName.TabIndex = 25
        Me.ToolTip1.SetToolTip(Me.editMasterName, "Do not change this name once the spreadsheet has been created or it will lose the" & _
        " connection to the spreadsheet.  All related events must have the identical name" & _
        " here.")
        '
        'editNotes
        '
        Me.editNotes.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainEventSetupBindingSource, "Notes", True))
        Me.editNotes.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.editNotes.Location = New System.Drawing.Point(106, 52)
        Me.editNotes.MaxLength = 300
        Me.editNotes.Multiline = True
        Me.editNotes.Name = "editNotes"
        Me.editNotes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.editNotes.Size = New System.Drawing.Size(404, 80)
        Me.editNotes.TabIndex = 5
        Me.editNotes.Tag = "limit: 300 characters"
        '
        'editMaximum
        '
        Me.editMaximum.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainEventSetupBindingSource, "MaximumSeating", True, DataSourceUpdateMode.OnValidation, ""))
        Me.editMaximum.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.editMaximum.Location = New System.Drawing.Point(105, 14)
        Me.editMaximum.Name = "editMaximum"
        Me.editMaximum.Size = New System.Drawing.Size(38, 21)
        Me.editMaximum.TabIndex = 0
        '
        'lblNotes
        '
        Me.lblNotes.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNotes.Location = New System.Drawing.Point(53, 52)
        Me.lblNotes.Name = "lblNotes"
        Me.lblNotes.Size = New System.Drawing.Size(48, 23)
        Me.lblNotes.TabIndex = 221
        Me.lblNotes.Text = "Notes:"
        Me.lblNotes.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblMaximum
        '
        Me.lblMaximum.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMaximum.Location = New System.Drawing.Point(31, 1)
        Me.lblMaximum.Name = "lblMaximum"
        Me.lblMaximum.Size = New System.Drawing.Size(67, 39)
        Me.lblMaximum.TabIndex = 197
        Me.lblMaximum.Text = "Maximum Seating:"
        Me.lblMaximum.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'editBookGiveaway
        '
        Me.editBookGiveaway.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainEventSetupBindingSource, "BookGiveaway", True))
        Me.editBookGiveaway.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.editBookGiveaway.Location = New System.Drawing.Point(588, 52)
        Me.editBookGiveaway.MaxLength = 400
        Me.editBookGiveaway.Multiline = True
        Me.editBookGiveaway.Name = "editBookGiveaway"
        Me.editBookGiveaway.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.editBookGiveaway.Size = New System.Drawing.Size(167, 84)
        Me.editBookGiveaway.TabIndex = 8
        Me.ToolTip1.SetToolTip(Me.editBookGiveaway, "limit: 400 characters")
        '
        'lblBookGiveaway
        '
        Me.lblBookGiveaway.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBookGiveaway.Location = New System.Drawing.Point(500, 39)
        Me.lblBookGiveaway.Name = "lblBookGiveaway"
        Me.lblBookGiveaway.Size = New System.Drawing.Size(80, 41)
        Me.lblBookGiveaway.TabIndex = 216
        Me.lblBookGiveaway.Text = "Book Giveaway:"
        Me.lblBookGiveaway.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(10, 434)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(91, 23)
        Me.Label4.TabIndex = 205
        Me.Label4.Text = "Instructor Phone:"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'editInstructorPhone
        '
        Me.editInstructorPhone.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainEventSetupBindingSource, "InstructorPhone", True))
        Me.editInstructorPhone.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.editInstructorPhone.Location = New System.Drawing.Point(107, 434)
        Me.editInstructorPhone.MaxLength = 20
        Me.editInstructorPhone.Name = "editInstructorPhone"
        Me.editInstructorPhone.Size = New System.Drawing.Size(166, 21)
        Me.editInstructorPhone.TabIndex = 120
        '
        'btnUpdate
        '
        Me.btnUpdate.ContextMenuStrip = Me.ContextMenuStrip1
        Me.btnUpdate.Location = New System.Drawing.Point(470, 90)
        Me.btnUpdate.Name = "btnUpdate"
        Me.btnUpdate.Size = New System.Drawing.Size(50, 28)
        Me.btnUpdate.TabIndex = 167
        Me.btnUpdate.Text = "Update"
        Me.ToolTip1.SetToolTip(Me.btnUpdate, "Update Checkboxes for Nametags, Info Letter Sent, or Attendance")
        Me.btnUpdate.UseVisualStyleBackColor = True
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.miUpdateNametag, Me.miUpdateInfoLetter, Me.miUpdateAttendance})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(176, 70)
        '
        'miUpdateNametag
        '
        Me.miUpdateNametag.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.miUpdateNametag.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mmiCheckNametag, Me.mmiUncheckNametag})
        Me.miUpdateNametag.Name = "miUpdateNametag"
        Me.miUpdateNametag.Size = New System.Drawing.Size(175, 22)
        Me.miUpdateNametag.Text = "Nametags"
        '
        'mmiCheckNametag
        '
        Me.mmiCheckNametag.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.mmiCheckNametag.Name = "mmiCheckNametag"
        Me.mmiCheckNametag.Size = New System.Drawing.Size(120, 22)
        Me.mmiCheckNametag.Text = "Check"
        '
        'mmiUncheckNametag
        '
        Me.mmiUncheckNametag.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.mmiUncheckNametag.Name = "mmiUncheckNametag"
        Me.mmiUncheckNametag.Size = New System.Drawing.Size(120, 22)
        Me.mmiUncheckNametag.Text = "Uncheck"
        '
        'miUpdateInfoLetter
        '
        Me.miUpdateInfoLetter.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.miUpdateInfoLetter.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mmiCheckInformation, Me.mmiUncheckInformation})
        Me.miUpdateInfoLetter.Name = "miUpdateInfoLetter"
        Me.miUpdateInfoLetter.Size = New System.Drawing.Size(175, 22)
        Me.miUpdateInfoLetter.Text = "Information Letters"
        '
        'mmiCheckInformation
        '
        Me.mmiCheckInformation.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.mmiCheckInformation.Name = "mmiCheckInformation"
        Me.mmiCheckInformation.Size = New System.Drawing.Size(120, 22)
        Me.mmiCheckInformation.Text = "Check"
        '
        'mmiUncheckInformation
        '
        Me.mmiUncheckInformation.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.mmiUncheckInformation.Name = "mmiUncheckInformation"
        Me.mmiUncheckInformation.Size = New System.Drawing.Size(120, 22)
        Me.mmiUncheckInformation.Text = "Uncheck"
        '
        'miUpdateAttendance
        '
        Me.miUpdateAttendance.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.miUpdateAttendance.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mmiCheckAttended, Me.mmiUncheckAttended})
        Me.miUpdateAttendance.Name = "miUpdateAttendance"
        Me.miUpdateAttendance.Size = New System.Drawing.Size(175, 22)
        Me.miUpdateAttendance.Text = "Attendance"
        '
        'mmiCheckAttended
        '
        Me.mmiCheckAttended.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.mmiCheckAttended.Name = "mmiCheckAttended"
        Me.mmiCheckAttended.Size = New System.Drawing.Size(120, 22)
        Me.mmiCheckAttended.Text = "Check"
        '
        'mmiUncheckAttended
        '
        Me.mmiUncheckAttended.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.mmiUncheckAttended.Name = "mmiUncheckAttended"
        Me.mmiUncheckAttended.Size = New System.Drawing.Size(120, 22)
        Me.mmiUncheckAttended.Text = "Uncheck"
        '
        'pgRegistration
        '
        Me.pgRegistration.BackColor = System.Drawing.Color.Wheat
        Me.pgRegistration.Controls.Add(Me.grdMain)
        Me.pgRegistration.Location = New System.Drawing.Point(4, 22)
        Me.pgRegistration.Name = "pgRegistration"
        Me.pgRegistration.Size = New System.Drawing.Size(787, 514)
        Me.pgRegistration.TabIndex = 4
        Me.pgRegistration.Tag = "REGISTRATIONS"
        Me.pgRegistration.Text = "REGISTRATIONS"
        Me.pgRegistration.UseVisualStyleBackColor = True
        '
        'grdMain
        '
        Me.grdMain.AlternatingBackColor = System.Drawing.Color.FromArgb(CType(CType(204, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.grdMain.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdMain.BackgroundColor = System.Drawing.SystemColors.Control
        Me.grdMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.grdMain.CaptionBackColor = System.Drawing.SystemColors.InactiveCaption
        Me.grdMain.DataMember = ""
        Me.grdMain.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.grdMain.Location = New System.Drawing.Point(3, 3)
        Me.grdMain.Name = "grdMain"
        Me.grdMain.ParentRowsBackColor = System.Drawing.SystemColors.Window
        Me.grdMain.ParentRowsVisible = False
        Me.grdMain.ReadOnly = True
        Me.grdMain.RowHeaderWidth = 20
        Me.grdMain.SelectionBackColor = System.Drawing.SystemColors.Highlight
        Me.grdMain.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        Me.grdMain.Size = New System.Drawing.Size(776, 500)
        Me.grdMain.TabIndex = 10
        Me.grdMain.TableStyles.AddRange(New System.Windows.Forms.DataGridTableStyle() {Me.DataGridTableStyle1})
        Me.grdMain.Tag = "Double-click for detail.  Right-click to filter; Right-click Border to clear filt" & _
    "er."
        Me.ToolTip1.SetToolTip(Me.grdMain, "Click in grid and press F1 for help")
        '
        'DataGridTableStyle1
        '
        Me.DataGridTableStyle1.DataGrid = Me.grdMain
        Me.DataGridTableStyle1.GridColumnStyles.AddRange(New System.Windows.Forms.DataGridColumnStyle() {Me.DataGridTextBoxColumn1, Me.DataGridTextBoxColumn2, Me.DataGridTextBoxColumn3, Me.DataGridTextBoxColumn5, Me.DataGridTextBoxColumn6, Me.DataGridTextBoxColumn4, Me.DataGridTextBoxColumn14, Me.DataGridTextBoxColumn7, Me.DataGridTextBoxColumn9, Me.DataGridTextBoxColumn10, Me.DataGridTextBoxColumn11, Me.DataGridTextBoxColumn13, Me.DataGridTextBoxColumn8, Me.DataGridTextBoxColumn12, Me.DataGridTextBoxColumn15})
        Me.DataGridTableStyle1.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.DataGridTableStyle1.ReadOnly = True
        Me.DataGridTableStyle1.RowHeaderWidth = 20
        '
        'DataGridTextBoxColumn1
        '
        Me.DataGridTextBoxColumn1.Format = ""
        Me.DataGridTextBoxColumn1.FormatInfo = Nothing
        Me.DataGridTextBoxColumn1.HeaderText = "RegID"
        Me.DataGridTextBoxColumn1.MappingName = "RegistrationID"
        Me.DataGridTextBoxColumn1.Width = 0
        '
        'DataGridTextBoxColumn2
        '
        Me.DataGridTextBoxColumn2.Format = ""
        Me.DataGridTextBoxColumn2.FormatInfo = Nothing
        Me.DataGridTextBoxColumn2.HeaderText = "EventNum"
        Me.DataGridTextBoxColumn2.MappingName = "EventNum"
        Me.DataGridTextBoxColumn2.Width = 0
        '
        'DataGridTextBoxColumn3
        '
        Me.DataGridTextBoxColumn3.Format = ""
        Me.DataGridTextBoxColumn3.FormatInfo = Nothing
        Me.DataGridTextBoxColumn3.HeaderText = "ContactNum"
        Me.DataGridTextBoxColumn3.MappingName = "ContactNum"
        Me.DataGridTextBoxColumn3.Width = 0
        '
        'DataGridTextBoxColumn5
        '
        Me.DataGridTextBoxColumn5.Format = ""
        Me.DataGridTextBoxColumn5.FormatInfo = Nothing
        Me.DataGridTextBoxColumn5.HeaderText = "Lastname"
        Me.DataGridTextBoxColumn5.MappingName = "LastName"
        Me.DataGridTextBoxColumn5.Width = 75
        '
        'DataGridTextBoxColumn6
        '
        Me.DataGridTextBoxColumn6.Format = ""
        Me.DataGridTextBoxColumn6.FormatInfo = Nothing
        Me.DataGridTextBoxColumn6.HeaderText = "Firstname"
        Me.DataGridTextBoxColumn6.MappingName = "FirstName"
        Me.DataGridTextBoxColumn6.Width = 75
        '
        'DataGridTextBoxColumn4
        '
        Me.DataGridTextBoxColumn4.Format = ""
        Me.DataGridTextBoxColumn4.FormatInfo = Nothing
        Me.DataGridTextBoxColumn4.HeaderText = "Congr"
        Me.DataGridTextBoxColumn4.MappingName = "Organization"
        Me.DataGridTextBoxColumn4.Width = 130
        '
        'DataGridTextBoxColumn14
        '
        Me.DataGridTextBoxColumn14.Format = ""
        Me.DataGridTextBoxColumn14.FormatInfo = Nothing
        Me.DataGridTextBoxColumn14.HeaderText = "Order"
        Me.DataGridTextBoxColumn14.MappingName = "OrderNum"
        Me.DataGridTextBoxColumn14.Width = 60
        '
        'DataGridTextBoxColumn7
        '
        Me.DataGridTextBoxColumn7.Format = ""
        Me.DataGridTextBoxColumn7.FormatInfo = Nothing
        Me.DataGridTextBoxColumn7.HeaderText = "Payment"
        Me.DataGridTextBoxColumn7.MappingName = "IndivDue"
        Me.DataGridTextBoxColumn7.Width = 45
        '
        'DataGridTextBoxColumn9
        '
        Me.DataGridTextBoxColumn9.Alignment = System.Windows.Forms.HorizontalAlignment.Center
        Me.DataGridTextBoxColumn9.Format = ""
        Me.DataGridTextBoxColumn9.FormatInfo = Nothing
        Me.DataGridTextBoxColumn9.HeaderText = "Confirm"
        Me.DataGridTextBoxColumn9.MappingName = "Confirmation"
        Me.DataGridTextBoxColumn9.Width = 45
        '
        'DataGridTextBoxColumn10
        '
        Me.DataGridTextBoxColumn10.Alignment = System.Windows.Forms.HorizontalAlignment.Center
        Me.DataGridTextBoxColumn10.Format = ""
        Me.DataGridTextBoxColumn10.FormatInfo = Nothing
        Me.DataGridTextBoxColumn10.HeaderText = "Info"
        Me.DataGridTextBoxColumn10.MappingName = "Information"
        Me.DataGridTextBoxColumn10.Width = 45
        '
        'DataGridTextBoxColumn11
        '
        Me.DataGridTextBoxColumn11.Alignment = System.Windows.Forms.HorizontalAlignment.Center
        Me.DataGridTextBoxColumn11.Format = ""
        Me.DataGridTextBoxColumn11.FormatInfo = Nothing
        Me.DataGridTextBoxColumn11.HeaderText = "Nametag"
        Me.DataGridTextBoxColumn11.MappingName = "Nametag"
        Me.DataGridTextBoxColumn11.Width = 50
        '
        'DataGridTextBoxColumn13
        '
        Me.DataGridTextBoxColumn13.Alignment = System.Windows.Forms.HorizontalAlignment.Center
        Me.DataGridTextBoxColumn13.Format = ""
        Me.DataGridTextBoxColumn13.FormatInfo = Nothing
        Me.DataGridTextBoxColumn13.HeaderText = "Attended"
        Me.DataGridTextBoxColumn13.MappingName = "Attended"
        Me.DataGridTextBoxColumn13.Width = 45
        '
        'DataGridTextBoxColumn8
        '
        Me.DataGridTextBoxColumn8.Format = ""
        Me.DataGridTextBoxColumn8.FormatInfo = Nothing
        Me.DataGridTextBoxColumn8.HeaderText = "Vegetarian"
        Me.DataGridTextBoxColumn8.MappingName = "Vegetarian"
        Me.DataGridTextBoxColumn8.Width = 45
        '
        'DataGridTextBoxColumn12
        '
        Me.DataGridTextBoxColumn12.Format = ""
        Me.DataGridTextBoxColumn12.FormatInfo = Nothing
        Me.DataGridTextBoxColumn12.HeaderText = "Notes"
        Me.DataGridTextBoxColumn12.MappingName = "Notes"
        Me.DataGridTextBoxColumn12.NullText = ""
        Me.DataGridTextBoxColumn12.ReadOnly = True
        Me.DataGridTextBoxColumn12.Width = 75
        '
        'DataGridTextBoxColumn15
        '
        Me.DataGridTextBoxColumn15.Format = ""
        Me.DataGridTextBoxColumn15.FormatInfo = Nothing
        Me.DataGridTextBoxColumn15.HeaderText = "Canceled"
        Me.DataGridTextBoxColumn15.MappingName = "Cancelled"
        Me.DataGridTextBoxColumn15.Width = 45
        '
        'pgChecklist
        '
        Me.pgChecklist.Controls.Add(Me.lblChecklist)
        Me.pgChecklist.Controls.Add(Me.dgvChecklist)
        Me.pgChecklist.Controls.Add(Me.btnRefreshChecklist)
        Me.pgChecklist.Location = New System.Drawing.Point(4, 22)
        Me.pgChecklist.Name = "pgChecklist"
        Me.pgChecklist.Size = New System.Drawing.Size(787, 514)
        Me.pgChecklist.TabIndex = 5
        Me.pgChecklist.Tag = "CHECKLIST"
        Me.pgChecklist.Text = "MASTER CHECKLIST"
        Me.pgChecklist.UseVisualStyleBackColor = True
        '
        'lblChecklist
        '
        Me.lblChecklist.AutoSize = True
        Me.lblChecklist.ForeColor = System.Drawing.Color.Maroon
        Me.lblChecklist.Location = New System.Drawing.Point(206, 16)
        Me.lblChecklist.Name = "lblChecklist"
        Me.lblChecklist.Size = New System.Drawing.Size(383, 13)
        Me.lblChecklist.TabIndex = 4
        Me.lblChecklist.Text = "(read only version of Checklist.  Double-click grid to open and edit spreadsheet." & _
    ")"
        '
        'dgvChecklist
        '
        Me.dgvChecklist.AllowUserToAddRows = False
        Me.dgvChecklist.AllowUserToDeleteRows = False
        Me.dgvChecklist.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvChecklist.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvChecklist.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvChecklist.DefaultCellStyle = DataGridViewCellStyle2
        Me.dgvChecklist.Location = New System.Drawing.Point(14, 42)
        Me.dgvChecklist.Name = "dgvChecklist"
        Me.dgvChecklist.ReadOnly = True
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvChecklist.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.dgvChecklist.RowTemplate.Height = 45
        Me.dgvChecklist.Size = New System.Drawing.Size(755, 460)
        Me.dgvChecklist.TabIndex = 3
        Me.ToolTip1.SetToolTip(Me.dgvChecklist, "Double click to edit checklist.")
        '
        'btnRefreshChecklist
        '
        Me.btnRefreshChecklist.Location = New System.Drawing.Point(14, 8)
        Me.btnRefreshChecklist.Name = "btnRefreshChecklist"
        Me.btnRefreshChecklist.Size = New System.Drawing.Size(175, 29)
        Me.btnRefreshChecklist.TabIndex = 2
        Me.btnRefreshChecklist.Text = "Refresh Master Checklist Display"
        Me.btnRefreshChecklist.UseVisualStyleBackColor = True
        Me.btnRefreshChecklist.Visible = False
        '
        'fldRegID
        '
        Me.fldRegID.AutoSize = True
        Me.fldRegID.BackColor = System.Drawing.Color.Transparent
        Me.fldRegID.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fldRegID.ForeColor = System.Drawing.SystemColors.ControlDark
        Me.fldRegID.Location = New System.Drawing.Point(1041, 48)
        Me.fldRegID.MinimumSize = New System.Drawing.Size(40, 0)
        Me.fldRegID.Name = "fldRegID"
        Me.fldRegID.Size = New System.Drawing.Size(41, 13)
        Me.fldRegID.TabIndex = 428
        Me.fldRegID.Text = "Reg ID"
        '
        'TextBox2
        '
        Me.TextBox2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBox2.BackColor = System.Drawing.SystemColors.Control
        Me.TextBox2.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBox2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox2.ForeColor = System.Drawing.SystemColors.ControlDark
        Me.TextBox2.Location = New System.Drawing.Point(910, 48)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.ReadOnly = True
        Me.TextBox2.Size = New System.Drawing.Size(110, 13)
        Me.TextBox2.TabIndex = 429
        Me.TextBox2.Text = "Selected Registration #"
        Me.TextBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'DataGridTableStyle2
        '
        Me.DataGridTableStyle2.DataGrid = Nothing
        Me.DataGridTableStyle2.HeaderForeColor = System.Drawing.SystemColors.ControlText
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Label3.Location = New System.Drawing.Point(5, 195)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(57, 30)
        Me.Label3.TabIndex = 182
        Me.Label3.Text = "Map Link:"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblAddress2
        '
        Me.lblAddress2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAddress2.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblAddress2.Location = New System.Drawing.Point(12, 53)
        Me.lblAddress2.Name = "lblAddress2"
        Me.lblAddress2.Size = New System.Drawing.Size(50, 15)
        Me.lblAddress2.TabIndex = 158
        Me.lblAddress2.Text = "Room:"
        Me.lblAddress2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblState
        '
        Me.lblState.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblState.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblState.Location = New System.Drawing.Point(22, 124)
        Me.lblState.Name = "lblState"
        Me.lblState.Size = New System.Drawing.Size(40, 15)
        Me.lblState.TabIndex = 166
        Me.lblState.Text = "State:"
        Me.lblState.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblZipCode
        '
        Me.lblZipCode.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblZipCode.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblZipCode.Location = New System.Drawing.Point(97, 120)
        Me.lblZipCode.Name = "lblZipCode"
        Me.lblZipCode.Size = New System.Drawing.Size(32, 23)
        Me.lblZipCode.TabIndex = 161
        Me.lblZipCode.Text = "Zip:"
        Me.lblZipCode.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'editRoom
        '
        Me.editRoom.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainEventSetupBindingSource, "Address2", True))
        Me.editRoom.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.editRoom.Location = New System.Drawing.Point(65, 50)
        Me.editRoom.MaxLength = 50
        Me.editRoom.Name = "editRoom"
        Me.editRoom.Size = New System.Drawing.Size(247, 21)
        Me.editRoom.TabIndex = 2
        '
        'editState
        '
        Me.editState.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainEventSetupBindingSource, "State", True))
        Me.editState.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.editState.Location = New System.Drawing.Point(65, 123)
        Me.editState.MaxLength = 2
        Me.editState.Name = "editState"
        Me.editState.Size = New System.Drawing.Size(32, 21)
        Me.editState.TabIndex = 5
        '
        'editZipCode
        '
        Me.editZipCode.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainEventSetupBindingSource, "Zip", True))
        Me.editZipCode.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.editZipCode.Location = New System.Drawing.Point(131, 122)
        Me.editZipCode.MaxLength = 10
        Me.editZipCode.Name = "editZipCode"
        Me.editZipCode.Size = New System.Drawing.Size(102, 21)
        Me.editZipCode.TabIndex = 6
        '
        'lblLocationPhone
        '
        Me.lblLocationPhone.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLocationPhone.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblLocationPhone.Location = New System.Drawing.Point(9, 170)
        Me.lblLocationPhone.Name = "lblLocationPhone"
        Me.lblLocationPhone.Size = New System.Drawing.Size(53, 23)
        Me.lblLocationPhone.TabIndex = 7
        Me.lblLocationPhone.Text = "Phone:"
        Me.lblLocationPhone.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblLocationContact
        '
        Me.lblLocationContact.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLocationContact.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblLocationContact.Location = New System.Drawing.Point(10, 150)
        Me.lblLocationContact.Margin = New System.Windows.Forms.Padding(0)
        Me.lblLocationContact.Name = "lblLocationContact"
        Me.lblLocationContact.Size = New System.Drawing.Size(52, 15)
        Me.lblLocationContact.TabIndex = 6
        Me.lblLocationContact.Text = "Contact:"
        Me.lblLocationContact.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'editLocationPhone
        '
        Me.editLocationPhone.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainEventSetupBindingSource, "LocationPhone", True))
        Me.editLocationPhone.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.editLocationPhone.Location = New System.Drawing.Point(65, 172)
        Me.editLocationPhone.MaxLength = 20
        Me.editLocationPhone.Name = "editLocationPhone"
        Me.editLocationPhone.Size = New System.Drawing.Size(111, 21)
        Me.editLocationPhone.TabIndex = 8
        Me.editLocationPhone.Tag = "(###) ###-####"
        '
        'editLocationContact
        '
        Me.editLocationContact.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainEventSetupBindingSource, "LocationContact", True))
        Me.editLocationContact.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.editLocationContact.Location = New System.Drawing.Point(65, 148)
        Me.editLocationContact.MaxLength = 50
        Me.editLocationContact.Name = "editLocationContact"
        Me.editLocationContact.Size = New System.Drawing.Size(168, 21)
        Me.editLocationContact.TabIndex = 7
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.InactiveBorder
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.editDiscount)
        Me.Panel1.Controls.Add(Me.editTeamMin)
        Me.Panel1.Controls.Add(Me.Label11)
        Me.Panel1.Controls.Add(Me.Label10)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.cboTimeZone)
        Me.Panel1.Controls.Add(Me.cboType)
        Me.Panel1.Controls.Add(Me.editTitle)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.cboRegion)
        Me.Panel1.Controls.Add(Me.Label9)
        Me.Panel1.Controls.Add(Me.lblTypeofEvent)
        Me.Panel1.Controls.Add(Me.dtpFirstDate)
        Me.Panel1.Controls.Add(Me.lblInstructor)
        Me.Panel1.Controls.Add(Me.pnlAddress)
        Me.Panel1.Controls.Add(Me.editInstructor)
        Me.Panel1.Controls.Add(Me.lblDates)
        Me.Panel1.Controls.Add(Me.editDates)
        Me.Panel1.Controls.Add(Me.Label15)
        Me.Panel1.Controls.Add(Me.editTimeofEvent)
        Me.Panel1.Controls.Add(Me.lblTimeofEvent)
        Me.Panel1.Controls.Add(Me.lblFee)
        Me.Panel1.Controls.Add(Me.editFee)
        Me.Panel1.Location = New System.Drawing.Point(5, 62)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(342, 565)
        Me.Panel1.TabIndex = 0
        Me.Panel1.Tag = "Event Detail"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.ForeColor = System.Drawing.SystemColors.Highlight
        Me.Label5.Location = New System.Drawing.Point(12, 303)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(83, 13)
        Me.Label5.TabIndex = 219
        Me.Label5.Text = "Location Details"
        '
        'editDiscount
        '
        Me.editDiscount.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainEventSetupBindingSource, "Discount", True, System.Windows.Forms.DataSourceUpdateMode.OnValidation, ""))
        Me.editDiscount.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.editDiscount.Location = New System.Drawing.Point(211, 276)
        Me.editDiscount.Name = "editDiscount"
        Me.editDiscount.Size = New System.Drawing.Size(22, 21)
        Me.editDiscount.TabIndex = 9
        '
        'editTeamMin
        '
        Me.editTeamMin.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainEventSetupBindingSource, "TeamMin", True, System.Windows.Forms.DataSourceUpdateMode.OnValidation, ""))
        Me.editTeamMin.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.editTeamMin.Location = New System.Drawing.Point(302, 275)
        Me.editTeamMin.Name = "editTeamMin"
        Me.editTeamMin.Size = New System.Drawing.Size(23, 21)
        Me.editTeamMin.TabIndex = 10
        '
        'Label11
        '
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(139, 272)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(71, 23)
        Me.Label11.TabIndex = 228
        Me.Label11.Text = "Discount $:"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label10
        '
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(235, 269)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(66, 35)
        Me.Label10.TabIndex = 226
        Me.Label10.Text = "for teams" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & " of at least:"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(6, 248)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(69, 20)
        Me.Label1.TabIndex = 224
        Me.Label1.Text = "Time Zone:"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cboTimeZone
        '
        Me.cboTimeZone.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboTimeZone.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboTimeZone.DataBindings.Add(New System.Windows.Forms.Binding("SelectedItem", Me.MainEventSetupBindingSource, "LocationTimeZone", True))
        Me.cboTimeZone.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboTimeZone.Items.AddRange(New Object() {"Central", "Eastern"})
        Me.cboTimeZone.Location = New System.Drawing.Point(84, 248)
        Me.cboTimeZone.Name = "cboTimeZone"
        Me.cboTimeZone.RestrictContentToListItems = True
        Me.cboTimeZone.Size = New System.Drawing.Size(110, 23)
        Me.cboTimeZone.TabIndex = 7
        Me.cboTimeZone.Tag = "Satellite Region"
        Me.cboTimeZone.Text = "Time Zone"
        Me.ToolTip1.SetToolTip(Me.cboTimeZone, "selecting a Location will select a TimeZone")
        '
        'cboType
        '
        Me.cboType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboType.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.MainEventSetupBindingSource, "TypeofEvent", True))
        Me.cboType.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboType.FormattingEnabled = True
        Me.cboType.Location = New System.Drawing.Point(84, 53)
        Me.cboType.Name = "cboType"
        Me.cboType.RestrictContentToListItems = True
        Me.cboType.Size = New System.Drawing.Size(239, 23)
        Me.cboType.TabIndex = 1
        Me.cboType.Tag = "Type of Event"
        '
        'editTitle
        '
        Me.editTitle.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainEventSetupBindingSource, "EventName", True))
        Me.editTitle.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.editTitle.ForeColor = System.Drawing.SystemColors.ControlText
        Me.editTitle.Location = New System.Drawing.Point(41, 3)
        Me.editTitle.MaxLength = 80
        Me.editTitle.Multiline = True
        Me.editTitle.Name = "editTitle"
        Me.editTitle.Size = New System.Drawing.Size(282, 46)
        Me.editTitle.TabIndex = 0
        Me.editTitle.Tag = "Event Name"
        Me.editTitle.Text = "***Required*** "
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(4, 11)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(38, 24)
        Me.Label2.TabIndex = 221
        Me.Label2.Text = "Title:"
        '
        'cboRegion
        '
        Me.cboRegion.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboRegion.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboRegion.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.MainEventSetupBindingSource, "SatelliteRegion", True))
        Me.cboRegion.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainEventSetupBindingSource, "SatelliteRegion", True))
        Me.cboRegion.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboRegion.Location = New System.Drawing.Point(83, 79)
        Me.cboRegion.Name = "cboRegion"
        Me.cboRegion.RestrictContentToListItems = True
        Me.cboRegion.Size = New System.Drawing.Size(111, 23)
        Me.cboRegion.TabIndex = 2
        Me.cboRegion.Tag = "Satellite Region"
        '
        'Label9
        '
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(9, 80)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(69, 20)
        Me.Label9.TabIndex = 220
        Me.Label9.Text = "Region:"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblTypeofEvent
        '
        Me.lblTypeofEvent.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTypeofEvent.Location = New System.Drawing.Point(2, 53)
        Me.lblTypeofEvent.Margin = New System.Windows.Forms.Padding(0)
        Me.lblTypeofEvent.Name = "lblTypeofEvent"
        Me.lblTypeofEvent.Size = New System.Drawing.Size(95, 23)
        Me.lblTypeofEvent.TabIndex = 218
        Me.lblTypeofEvent.Text = "Type of Event:"
        Me.lblTypeofEvent.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'dtpFirstDate
        '
        Me.dtpFirstDate.DataBindings.Add(New System.Windows.Forms.Binding("Value", Me.MainEventSetupBindingSource, "FirstDate", True))
        Me.dtpFirstDate.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainEventSetupBindingSource, "FirstDate", True))
        Me.dtpFirstDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpFirstDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpFirstDate.Location = New System.Drawing.Point(84, 144)
        Me.dtpFirstDate.Name = "dtpFirstDate"
        Me.dtpFirstDate.Size = New System.Drawing.Size(109, 21)
        Me.dtpFirstDate.TabIndex = 4
        Me.dtpFirstDate.Value = New Date(2012, 8, 2, 12, 59, 53, 0)
        '
        'lblInstructor
        '
        Me.lblInstructor.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblInstructor.Location = New System.Drawing.Point(4, 113)
        Me.lblInstructor.Name = "lblInstructor"
        Me.lblInstructor.Size = New System.Drawing.Size(74, 15)
        Me.lblInstructor.TabIndex = 219
        Me.lblInstructor.Text = "Instructor(s):"
        Me.lblInstructor.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'pnlAddress
        '
        Me.pnlAddress.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pnlAddress.Controls.Add(Me.btnHostOverview)
        Me.pnlAddress.Controls.Add(Me.lblOrgNum)
        Me.pnlAddress.Controls.Add(Me.txtLocationURL)
        Me.pnlAddress.Controls.Add(Me.EditLocation)
        Me.pnlAddress.Controls.Add(Me.cboLocation)
        Me.pnlAddress.Controls.Add(Me.lblCity)
        Me.pnlAddress.Controls.Add(Me.editCity)
        Me.pnlAddress.Controls.Add(Me.lblGotoOrg)
        Me.pnlAddress.Controls.Add(Me.lblAddress1)
        Me.pnlAddress.Controls.Add(Me.editStreet)
        Me.pnlAddress.Controls.Add(Me.lblAddress2)
        Me.pnlAddress.Controls.Add(Me.editState)
        Me.pnlAddress.Controls.Add(Me.editRoom)
        Me.pnlAddress.Controls.Add(Me.lblZipCode)
        Me.pnlAddress.Controls.Add(Me.lblLocationPhone)
        Me.pnlAddress.Controls.Add(Me.editLocationPhone)
        Me.pnlAddress.Controls.Add(Me.lblState)
        Me.pnlAddress.Controls.Add(Me.lblLocationContact)
        Me.pnlAddress.Controls.Add(Me.editZipCode)
        Me.pnlAddress.Controls.Add(Me.editLocationContact)
        Me.pnlAddress.Controls.Add(Me.Label3)
        Me.pnlAddress.Location = New System.Drawing.Point(9, 311)
        Me.pnlAddress.Name = "pnlAddress"
        Me.pnlAddress.Size = New System.Drawing.Size(326, 235)
        Me.pnlAddress.TabIndex = 11
        '
        'btnHostOverview
        '
        Me.btnHostOverview.Location = New System.Drawing.Point(240, 112)
        Me.btnHostOverview.Name = "btnHostOverview"
        Me.btnHostOverview.Size = New System.Drawing.Size(72, 50)
        Me.btnHostOverview.TabIndex = 229
        Me.btnHostOverview.Text = "Host Overview - Create"
        Me.btnHostOverview.UseVisualStyleBackColor = True
        '
        'lblOrgNum
        '
        Me.lblOrgNum.AutoSize = True
        Me.lblOrgNum.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainEventSetupBindingSource, "LocationOrgNum", True))
        Me.lblOrgNum.Enabled = False
        Me.lblOrgNum.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblOrgNum.ForeColor = System.Drawing.SystemColors.ButtonShadow
        Me.lblOrgNum.Location = New System.Drawing.Point(250, 177)
        Me.lblOrgNum.Name = "lblOrgNum"
        Me.lblOrgNum.Size = New System.Drawing.Size(45, 13)
        Me.lblOrgNum.TabIndex = 220
        Me.lblOrgNum.Text = "OrgNum"
        '
        'txtLocationURL
        '
        Me.txtLocationURL.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainEventSetupBindingSource, "LocationURL", True))
        Me.txtLocationURL.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLocationURL.Location = New System.Drawing.Point(65, 196)
        Me.txtLocationURL.Multiline = True
        Me.txtLocationURL.Name = "txtLocationURL"
        Me.txtLocationURL.ReadOnly = True
        Me.txtLocationURL.Size = New System.Drawing.Size(247, 37)
        Me.txtLocationURL.TabIndex = 9
        '
        'EditLocation
        '
        Me.EditLocation.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainEventSetupBindingSource, "LocationName", True))
        Me.EditLocation.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.EditLocation.Location = New System.Drawing.Point(65, 26)
        Me.EditLocation.MaxLength = 50
        Me.EditLocation.Name = "EditLocation"
        Me.EditLocation.Size = New System.Drawing.Size(247, 21)
        Me.EditLocation.TabIndex = 1
        '
        'cboLocation
        '
        Me.cboLocation.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboLocation.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboLocation.DropDownWidth = 350
        Me.cboLocation.ForeColor = System.Drawing.Color.IndianRed
        Me.cboLocation.FormattingEnabled = True
        Me.cboLocation.Location = New System.Drawing.Point(65, 3)
        Me.cboLocation.MaxLength = 50
        Me.cboLocation.Name = "cboLocation"
        Me.cboLocation.RestrictContentToListItems = True
        Me.cboLocation.Size = New System.Drawing.Size(247, 21)
        Me.cboLocation.TabIndex = 0
        Me.cboLocation.Text = "Select Location here"
        Me.ToolTip1.SetToolTip(Me.cboLocation, "dropdown Location list is filtered by Region")
        '
        'lblCity
        '
        Me.lblCity.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCity.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblCity.Location = New System.Drawing.Point(30, 101)
        Me.lblCity.Name = "lblCity"
        Me.lblCity.Size = New System.Drawing.Size(32, 15)
        Me.lblCity.TabIndex = 218
        Me.lblCity.Text = "City:"
        Me.lblCity.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'editCity
        '
        Me.editCity.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainEventSetupBindingSource, "City", True))
        Me.editCity.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.editCity.Location = New System.Drawing.Point(65, 98)
        Me.editCity.MaxLength = 20
        Me.editCity.Name = "editCity"
        Me.editCity.Size = New System.Drawing.Size(168, 21)
        Me.editCity.TabIndex = 4
        '
        'lblGotoOrg
        '
        Me.lblGotoOrg.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblGotoOrg.ForeColor = System.Drawing.Color.DarkGreen
        Me.lblGotoOrg.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblGotoOrg.Location = New System.Drawing.Point(2, 27)
        Me.lblGotoOrg.Margin = New System.Windows.Forms.Padding(0)
        Me.lblGotoOrg.Name = "lblGotoOrg"
        Me.lblGotoOrg.Size = New System.Drawing.Size(60, 15)
        Me.lblGotoOrg.TabIndex = 215
        Me.lblGotoOrg.Text = "Location:"
        Me.lblGotoOrg.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblAddress1
        '
        Me.lblAddress1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAddress1.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblAddress1.Location = New System.Drawing.Point(14, 77)
        Me.lblAddress1.Name = "lblAddress1"
        Me.lblAddress1.Size = New System.Drawing.Size(48, 15)
        Me.lblAddress1.TabIndex = 216
        Me.lblAddress1.Text = "Street:"
        Me.lblAddress1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'editStreet
        '
        Me.editStreet.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainEventSetupBindingSource, "Address1", True))
        Me.editStreet.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.editStreet.Location = New System.Drawing.Point(65, 74)
        Me.editStreet.MaxLength = 50
        Me.editStreet.Name = "editStreet"
        Me.editStreet.Size = New System.Drawing.Size(247, 21)
        Me.editStreet.TabIndex = 3
        '
        'editInstructor
        '
        Me.editInstructor.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainEventSetupBindingSource, "Instructor", True))
        Me.editInstructor.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.editInstructor.Location = New System.Drawing.Point(84, 105)
        Me.editInstructor.MaxLength = 75
        Me.editInstructor.Multiline = True
        Me.editInstructor.Name = "editInstructor"
        Me.editInstructor.Size = New System.Drawing.Size(239, 35)
        Me.editInstructor.TabIndex = 3
        '
        'lblDates
        '
        Me.lblDates.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDates.Location = New System.Drawing.Point(1, 167)
        Me.lblDates.Margin = New System.Windows.Forms.Padding(0)
        Me.lblDates.Name = "lblDates"
        Me.lblDates.Size = New System.Drawing.Size(75, 38)
        Me.lblDates.TabIndex = 203
        Me.lblDates.Text = "Date(s): (spelled out)"
        Me.lblDates.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'editDates
        '
        Me.editDates.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainEventSetupBindingSource, "DateTxt", True))
        Me.editDates.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.editDates.Location = New System.Drawing.Point(84, 168)
        Me.editDates.MaxLength = 75
        Me.editDates.MinimumSize = New System.Drawing.Size(0, 35)
        Me.editDates.Multiline = True
        Me.editDates.Name = "editDates"
        Me.editDates.Size = New System.Drawing.Size(239, 35)
        Me.editDates.TabIndex = 5
        '
        'Label15
        '
        Me.Label15.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(11, 146)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(65, 15)
        Me.Label15.TabIndex = 208
        Me.Label15.Text = "First Date:"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'editTimeofEvent
        '
        Me.editTimeofEvent.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainEventSetupBindingSource, "Timetxt", True))
        Me.editTimeofEvent.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.editTimeofEvent.Location = New System.Drawing.Point(84, 208)
        Me.editTimeofEvent.MinimumSize = New System.Drawing.Size(0, 35)
        Me.editTimeofEvent.Name = "editTimeofEvent"
        Me.editTimeofEvent.Size = New System.Drawing.Size(239, 35)
        Me.editTimeofEvent.TabIndex = 6
        '
        'lblTimeofEvent
        '
        Me.lblTimeofEvent.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTimeofEvent.Location = New System.Drawing.Point(15, 213)
        Me.lblTimeofEvent.Name = "lblTimeofEvent"
        Me.lblTimeofEvent.Size = New System.Drawing.Size(56, 23)
        Me.lblTimeofEvent.TabIndex = 195
        Me.lblTimeofEvent.Text = "Time:"
        Me.lblTimeofEvent.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblFee
        '
        Me.lblFee.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFee.Location = New System.Drawing.Point(25, 273)
        Me.lblFee.Name = "lblFee"
        Me.lblFee.Size = New System.Drawing.Size(44, 23)
        Me.lblFee.TabIndex = 196
        Me.lblFee.Text = "Fee $:"
        Me.lblFee.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'editFee
        '
        Me.editFee.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainEventSetupBindingSource, "Fee", True, System.Windows.Forms.DataSourceUpdateMode.OnValidation, ""))
        Me.editFee.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.editFee.Location = New System.Drawing.Point(84, 276)
        Me.editFee.Name = "editFee"
        Me.editFee.Size = New System.Drawing.Size(54, 21)
        Me.editFee.TabIndex = 8
        '
        'lblRegion
        '
        Me.lblRegion.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainEventSetupBindingSource, "SatelliteRegion", True))
        Me.lblRegion.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRegion.ForeColor = System.Drawing.Color.DarkBlue
        Me.lblRegion.Location = New System.Drawing.Point(10, 10)
        Me.lblRegion.Name = "lblRegion"
        Me.lblRegion.Size = New System.Drawing.Size(138, 18)
        Me.lblRegion.TabIndex = 210
        Me.lblRegion.Text = "EVENT DETAIL"
        Me.lblRegion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'SqlDeleteCommand
        '
        Me.SqlDeleteCommand.CommandText = "dbo.NewDeleteCommand"
        Me.SqlDeleteCommand.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlDeleteCommand.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, False, CType(0, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing), New System.Data.SqlClient.SqlParameter("@Original_EventID", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "EventID", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_Stamped", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "Stamped", System.Data.DataRowVersion.Current, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_Stamped", System.Data.SqlDbType.Timestamp, 8, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "Stamped", System.Data.DataRowVersion.Original, Nothing)})
        '
        'SqlInsertCommand
        '
        Me.SqlInsertCommand.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlInsertCommand.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, False, CType(0, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing), New System.Data.SqlClient.SqlParameter("@WorkshopNum", System.Data.SqlDbType.Int, 4, "WorkshopNum"), New System.Data.SqlClient.SqlParameter("@EventNID", System.Data.SqlDbType.Int, 4, "EventNID"), New System.Data.SqlClient.SqlParameter("@WorkshopNIDnum", System.Data.SqlDbType.Int, 4, "WorkshopNIDnum"), New System.Data.SqlClient.SqlParameter("@EventSku", System.Data.SqlDbType.VarChar, 100, "EventSku"), New System.Data.SqlClient.SqlParameter("@FirstDate", System.Data.SqlDbType.SmallDateTime, 4, "FirstDate"), New System.Data.SqlClient.SqlParameter("@SatelliteRegion", System.Data.SqlDbType.VarChar, 15, "SatelliteRegion"), New System.Data.SqlClient.SqlParameter("@Timetxt", System.Data.SqlDbType.VarChar, 100, "Timetxt"), New System.Data.SqlClient.SqlParameter("@Lastdate", System.Data.SqlDbType.SmallDateTime, 4, "Lastdate"), New System.Data.SqlClient.SqlParameter("@DateTxt", System.Data.SqlDbType.VarChar, 75, "DateTxt"), New System.Data.SqlClient.SqlParameter("@Fee", System.Data.SqlDbType.SmallInt, 4, "Fee"), New System.Data.SqlClient.SqlParameter("@Location", System.Data.SqlDbType.VarChar, 255, "Location"), New System.Data.SqlClient.SqlParameter("@LocationName", System.Data.SqlDbType.VarChar, 100, "LocationName"), New System.Data.SqlClient.SqlParameter("@Address1", System.Data.SqlDbType.VarChar, 50, "Address1"), New System.Data.SqlClient.SqlParameter("@Address2", System.Data.SqlDbType.VarChar, 50, "Address2"), New System.Data.SqlClient.SqlParameter("@City", System.Data.SqlDbType.VarChar, 20, "City"), New System.Data.SqlClient.SqlParameter("@State", System.Data.SqlDbType.VarChar, 2, "State"), New System.Data.SqlClient.SqlParameter("@Zip", System.Data.SqlDbType.VarChar, 10, "Zip"), New System.Data.SqlClient.SqlParameter("@Maximum", System.Data.SqlDbType.Int, 4, "Maximum"), New System.Data.SqlClient.SqlParameter("@CRGNum", System.Data.SqlDbType.Int, 4, "CRGNum"), New System.Data.SqlClient.SqlParameter("@Caterer", System.Data.SqlDbType.VarChar, 100, "Caterer"), New System.Data.SqlClient.SqlParameter("@Notes", System.Data.SqlDbType.Text, 2147483647, "Notes"), New System.Data.SqlClient.SqlParameter("@LocationOrgNum", System.Data.SqlDbType.Int, 4, "LocationOrgNum"), New System.Data.SqlClient.SqlParameter("@LocationPhone", System.Data.SqlDbType.VarChar, 20, "LocationPhone"), New System.Data.SqlClient.SqlParameter("@LocationContact", System.Data.SqlDbType.VarChar, 50, "LocationContact"), New System.Data.SqlClient.SqlParameter("@LocationURL", System.Data.SqlDbType.NVarChar, 600, "LocationURL"), New System.Data.SqlClient.SqlParameter("@CatererPhone", System.Data.SqlDbType.VarChar, 20, "CatererPhone"), New System.Data.SqlClient.SqlParameter("@CatererFood", System.Data.SqlDbType.Text, 2147483647, "CatererFood"), New System.Data.SqlClient.SqlParameter("@AdditionalPeople", System.Data.SqlDbType.SmallInt, 2, "AdditionalPeople"), New System.Data.SqlClient.SqlParameter("@BookGiveaway", System.Data.SqlDbType.VarChar, 200, "BookGiveaway"), New System.Data.SqlClient.SqlParameter("@EventCancelled", System.Data.SqlDbType.Bit, 1, "EventCancelled"), New System.Data.SqlClient.SqlParameter("@RegonlineNum", System.Data.SqlDbType.Int, 4, "RegonlineNum"), New System.Data.SqlClient.SqlParameter("@FollowUpStaffTxt", System.Data.SqlDbType.VarChar, 100, "FollowUpStaffTxt"), New System.Data.SqlClient.SqlParameter("@FollowupDone", System.Data.SqlDbType.SmallDateTime, 4, "FollowupDone")})
        '
        'MainMenu1
        '
        Me.MainMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem1, Me.MenuItem3, Me.MenuItem2, Me.mmSend, Me.miUpdate, Me.mmReport, Me.miSendEmail, Me.MenuItem6})
        '
        'MenuItem1
        '
        Me.MenuItem1.Index = 0
        Me.MenuItem1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.miNew, Me.miClose, Me.miAttach, Me.miOpenFile, Me.miMergeTemplate})
        Me.MenuItem1.Text = "File"
        '
        'miNew
        '
        Me.miNew.Index = 0
        Me.miNew.Text = "New"
        '
        'miClose
        '
        Me.miClose.Index = 1
        Me.miClose.Text = "Close Window"
        '
        'miAttach
        '
        Me.miAttach.Index = 2
        Me.miAttach.Text = "Attach File"
        '
        'miOpenFile
        '
        Me.miOpenFile.Enabled = False
        Me.miOpenFile.Index = 3
        Me.miOpenFile.Text = "Open File"
        '
        'miMergeTemplate
        '
        Me.miMergeTemplate.Index = 4
        Me.miMergeTemplate.Text = "Open default InfoLetter Merge template"
        '
        'MenuItem3
        '
        Me.MenuItem3.Index = 1
        Me.MenuItem3.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.miSave, Me.miCancel, Me.miDelete})
        Me.MenuItem3.Text = "Edit"
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
        Me.miDelete.Text = "Delete Event"
        '
        'MenuItem2
        '
        Me.MenuItem2.Index = 2
        Me.MenuItem2.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.miViewAll, Me.miPending})
        Me.MenuItem2.Text = "GoTo"
        '
        'miViewAll
        '
        Me.miViewAll.Index = 0
        Me.miViewAll.Text = "All Registrations Detail"
        '
        'miPending
        '
        Me.miPending.Index = 1
        Me.miPending.Text = "Pending Registrations"
        '
        'mmSend
        '
        Me.mmSend.Index = 3
        Me.mmSend.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.miMergeNametag, Me.miMergeInfoLttr, Me.miRegistrant, Me.miInfoLttrNoEmail})
        Me.mmSend.Text = "Merge"
        '
        'miMergeNametag
        '
        Me.miMergeNametag.Index = 0
        Me.miMergeNametag.Tag = "Nametag"
        Me.miMergeNametag.Text = "Nametags (if not already Printed)"
        '
        'miMergeInfoLttr
        '
        Me.miMergeInfoLttr.Index = 1
        Me.miMergeInfoLttr.Tag = "InfoLttr"
        Me.miMergeInfoLttr.Text = "Info Letter Data (if not already sent)"
        '
        'miRegistrant
        '
        Me.miRegistrant.Index = 2
        Me.miRegistrant.ShowShortcut = False
        Me.miRegistrant.Tag = "OpenDataFile"
        Me.miRegistrant.Text = "Registrant/Attendee Data"
        '
        'miInfoLttrNoEmail
        '
        Me.miInfoLttrNoEmail.Index = 3
        Me.miInfoLttrNoEmail.Tag = "InfoLttrNoEmail"
        Me.miInfoLttrNoEmail.Text = "Info Letter Data with No Email"
        '
        'miUpdate
        '
        Me.miUpdate.Index = 4
        Me.miUpdate.Text = "Update"
        '
        'mmReport
        '
        Me.mmReport.Index = 5
        Me.mmReport.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.miReport, Me.miHost, Me.miMasterChecklist, Me.miCancellationRpt, Me.MenuItem4})
        Me.mmReport.Text = "Reports"
        '
        'miReport
        '
        Me.miReport.Index = 0
        Me.miReport.Text = "Registration Reports"
        '
        'miHost
        '
        Me.miHost.Index = 1
        Me.miHost.Tag = "HostCongr"
        Me.miHost.Text = "Host Overview - Create"
        '
        'miMasterChecklist
        '
        Me.miMasterChecklist.Index = 2
        Me.miMasterChecklist.Text = "Open Master Checklist"
        '
        'miCancellationRpt
        '
        Me.miCancellationRpt.Index = 3
        Me.miCancellationRpt.Text = "Open Cancellation Phone List"
        '
        'MenuItem4
        '
        Me.MenuItem4.Enabled = False
        Me.MenuItem4.Index = 4
        Me.MenuItem4.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.miRptPayment, Me.miRptRefund})
        Me.MenuItem4.Text = "Financial Summary"
        '
        'miRptPayment
        '
        Me.miRptPayment.Enabled = False
        Me.miRptPayment.Index = 0
        Me.miRptPayment.Text = "Payments"
        '
        'miRptRefund
        '
        Me.miRptRefund.Enabled = False
        Me.miRptRefund.Index = 1
        Me.miRptRefund.Text = "Refunds"
        '
        'miSendEmail
        '
        Me.miSendEmail.Index = 6
        Me.miSendEmail.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.miEmailAttended, Me.miEmailRegistered, Me.miEmailNotAttend, Me.miEmailDatafile, Me.miFirstEmail})
        Me.miSendEmail.Text = "Send Email"
        '
        'miEmailAttended
        '
        Me.miEmailAttended.DefaultItem = True
        Me.miEmailAttended.Index = 0
        Me.miEmailAttended.Tag = "Attended"
        Me.miEmailAttended.Text = "All Who Attended"
        '
        'miEmailRegistered
        '
        Me.miEmailRegistered.Index = 1
        Me.miEmailRegistered.Tag = "Registered"
        Me.miEmailRegistered.Text = "All Who Registered"
        '
        'miEmailNotAttend
        '
        Me.miEmailNotAttend.Index = 2
        Me.miEmailNotAttend.Tag = "DidNotAttend"
        Me.miEmailNotAttend.Text = "All Who Did Not Attend"
        '
        'miEmailDatafile
        '
        Me.miEmailDatafile.Enabled = False
        Me.miEmailDatafile.Index = 3
        Me.miEmailDatafile.Tag = "OpenDatafile"
        Me.miEmailDatafile.Text = "Open Spreadsheet Datafile"
        '
        'miFirstEmail
        '
        Me.miFirstEmail.Enabled = False
        Me.miFirstEmail.Index = 4
        Me.miFirstEmail.Tag = "FirstEmail"
        Me.miFirstEmail.Text = "Not Rec'd InfoLetter && update"
        '
        'MenuItem6
        '
        Me.MenuItem6.Index = 7
        Me.MenuItem6.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.miHelp, Me.miDef})
        Me.MenuItem6.Text = "Help"
        '
        'miHelp
        '
        Me.miHelp.Index = 0
        Me.miHelp.Text = "Help"
        '
        'miDef
        '
        Me.miDef.Index = 1
        Me.miDef.Text = "Event Type Definitions"
        '
        'StatusBar1
        '
        Me.StatusBar1.Location = New System.Drawing.Point(0, 620)
        Me.StatusBar1.Name = "StatusBar1"
        Me.StatusBar1.Panels.AddRange(New System.Windows.Forms.StatusBarPanel() {Me.StatusBarPanel1, Me.StatusBarPanelID, Me.StatusBarPanel2})
        Me.StatusBar1.ShowPanels = True
        Me.StatusBar1.Size = New System.Drawing.Size(1148, 26)
        Me.StatusBar1.TabIndex = 195
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
        Me.StatusBarPanelID.Alignment = System.Windows.Forms.HorizontalAlignment.Center
        Me.StatusBarPanelID.Name = "StatusBarPanelID"
        Me.StatusBarPanelID.Text = "StatusBarPanel3"
        '
        'StatusBarPanel2
        '
        Me.StatusBarPanel2.Alignment = System.Windows.Forms.HorizontalAlignment.Right
        Me.StatusBarPanel2.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring
        Me.StatusBarPanel2.Name = "StatusBarPanel2"
        Me.StatusBarPanel2.Text = "Use this window to edit Events."
        Me.StatusBarPanel2.Width = 831
        '
        'btnNew
        '
        Me.btnNew.BackColor = System.Drawing.Color.PaleGoldenrod
        Me.btnNew.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnNew.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnNew.Image = Global.InfoCtr.My.Resources.Resources.Plus
        Me.btnNew.ImageAlign = System.Drawing.ContentAlignment.TopRight
        Me.btnNew.Location = New System.Drawing.Point(3, 2)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(64, 35)
        Me.btnNew.TabIndex = 208
        Me.btnNew.TabStop = False
        Me.btnNew.Text = "New Registration"
        Me.btnNew.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        Me.ToolTip1.SetToolTip(Me.btnNew, "Add New Registration")
        Me.btnNew.UseVisualStyleBackColor = False
        '
        'btnDelete
        '
        Me.btnDelete.BackColor = System.Drawing.SystemColors.Control
        Me.btnDelete.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Image = Global.InfoCtr.My.Resources.Resources.btnDelete
        Me.btnDelete.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.btnDelete.Location = New System.Drawing.Point(1109, 43)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(14, 14)
        Me.btnDelete.TabIndex = 308
        Me.btnDelete.Text = "Delete Event"
        Me.btnDelete.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.ToolTip1.SetToolTip(Me.btnDelete, "Delete this Event")
        Me.btnDelete.UseVisualStyleBackColor = False
        Me.btnDelete.Visible = False
        '
        'btnSaveExit
        '
        Me.btnSaveExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSaveExit.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.btnSaveExit.CausesValidation = False
        Me.btnSaveExit.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSaveExit.Image = Global.InfoCtr.My.Resources.Resources.btnSaveExit
        Me.btnSaveExit.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.btnSaveExit.Location = New System.Drawing.Point(155, 2)
        Me.btnSaveExit.Name = "btnSaveExit"
        Me.btnSaveExit.Size = New System.Drawing.Size(40, 35)
        Me.btnSaveExit.TabIndex = 415
        Me.btnSaveExit.Text = "Close"
        Me.btnSaveExit.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.ToolTip1.SetToolTip(Me.btnSaveExit, "Save &Close")
        Me.btnSaveExit.UseVisualStyleBackColor = False
        '
        'btnOpenFile
        '
        Me.btnOpenFile.BackColor = System.Drawing.SystemColors.Control
        Me.btnOpenFile.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnOpenFile.Image = Global.InfoCtr.My.Resources.Resources.btnAttached
        Me.btnOpenFile.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.btnOpenFile.Location = New System.Drawing.Point(67, 1)
        Me.btnOpenFile.Name = "btnOpenFile"
        Me.btnOpenFile.Size = New System.Drawing.Size(41, 35)
        Me.btnOpenFile.TabIndex = 418
        Me.btnOpenFile.Text = "Files"
        Me.btnOpenFile.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.btnOpenFile.UseVisualStyleBackColor = False
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.SystemColors.Highlight
        Me.Panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel4.Controls.Add(Me.btnOpenFile)
        Me.Panel4.Controls.Add(Me.btnReport)
        Me.Panel4.Controls.Add(Me.btnNew)
        Me.Panel4.Controls.Add(Me.btnSaveExit)
        Me.Panel4.Location = New System.Drawing.Point(897, 5)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(198, 39)
        Me.Panel4.TabIndex = 423
        '
        'btnReport
        '
        Me.btnReport.BackColor = System.Drawing.SystemColors.Control
        Me.btnReport.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReport.Image = Global.InfoCtr.My.Resources.Resources.Report
        Me.btnReport.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.btnReport.Location = New System.Drawing.Point(113, 2)
        Me.btnReport.Name = "btnReport"
        Me.btnReport.Size = New System.Drawing.Size(41, 35)
        Me.btnReport.TabIndex = 416
        Me.btnReport.Text = "Report"
        Me.btnReport.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.btnReport.UseVisualStyleBackColor = False
        '
        'ErrorProvider1
        '
        Me.ErrorProvider1.ContainerControl = Me
        '
        'fldEventID
        '
        Me.fldEventID.BackColor = System.Drawing.SystemColors.Control
        Me.fldEventID.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.fldEventID.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainEventSetupBindingSource, "EventID", True))
        Me.fldEventID.Enabled = False
        Me.fldEventID.ForeColor = System.Drawing.SystemColors.ControlDark
        Me.fldEventID.Location = New System.Drawing.Point(761, 3)
        Me.fldEventID.Name = "fldEventID"
        Me.fldEventID.ReadOnly = True
        Me.fldEventID.Size = New System.Drawing.Size(54, 13)
        Me.fldEventID.TabIndex = 428
        Me.fldEventID.Text = "EventID"
        '
        'lblEventID
        '
        Me.lblEventID.AutoSize = True
        Me.lblEventID.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEventID.ForeColor = System.Drawing.SystemColors.ControlDark
        Me.lblEventID.Location = New System.Drawing.Point(679, 1)
        Me.lblEventID.Name = "lblEventID"
        Me.lblEventID.Size = New System.Drawing.Size(70, 15)
        Me.lblEventID.TabIndex = 429
        Me.lblEventID.Text = "Our Event #"
        '
        'fldEventNID
        '
        Me.fldEventNID.BackColor = System.Drawing.SystemColors.Control
        Me.fldEventNID.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainEventSetupBindingSource, "NIDEvent", True))
        Me.fldEventNID.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fldEventNID.ForeColor = System.Drawing.Color.Gray
        Me.fldEventNID.Location = New System.Drawing.Point(761, 38)
        Me.fldEventNID.Name = "fldEventNID"
        Me.fldEventNID.ReadOnly = True
        Me.fldEventNID.Size = New System.Drawing.Size(85, 20)
        Me.fldEventNID.TabIndex = 432
        '
        'btnHelp
        '
        Me.btnHelp.Image = CType(resources.GetObject("btnHelp.Image"), System.Drawing.Image)
        Me.btnHelp.Location = New System.Drawing.Point(1101, 4)
        Me.btnHelp.Name = "btnHelp"
        Me.btnHelp.Size = New System.Drawing.Size(25, 25)
        Me.btnHelp.TabIndex = 309
        Me.btnHelp.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnHelp.UseVisualStyleBackColor = True
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.SystemColors.ControlDark
        Me.Label12.Location = New System.Drawing.Point(670, 41)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(79, 13)
        Me.Label12.TabIndex = 433
        Me.Label12.Text = "Drupal Event #"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.SystemColors.ControlDark
        Me.Label13.Location = New System.Drawing.Point(286, 13)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(60, 13)
        Me.Label13.TabIndex = 435
        Me.Label13.Text = "Event SKU"
        '
        'fldSKU
        '
        Me.fldSKU.BackColor = System.Drawing.SystemColors.Control
        Me.fldSKU.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainEventSetupBindingSource, "EventSKU", True))
        Me.fldSKU.Enabled = False
        Me.fldSKU.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fldSKU.ForeColor = System.Drawing.SystemColors.ControlDark
        Me.fldSKU.Location = New System.Drawing.Point(353, 10)
        Me.fldSKU.Name = "fldSKU"
        Me.fldSKU.Size = New System.Drawing.Size(249, 20)
        Me.fldSKU.TabIndex = 434
        '
        'MainEventSetupTableAdapter
        '
        Me.MainEventSetupTableAdapter.ClearBeforeFill = True
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlDark
        Me.Label8.Location = New System.Drawing.Point(653, 22)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(96, 13)
        Me.Label8.TabIndex = 437
        Me.Label8.Text = "Regonline Event #"
        '
        'fldRegonlineID
        '
        Me.fldRegonlineID.BackColor = System.Drawing.SystemColors.Control
        Me.fldRegonlineID.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainEventSetupBindingSource, "RegonlineNum", True))
        Me.fldRegonlineID.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fldRegonlineID.ForeColor = System.Drawing.Color.Gray
        Me.fldRegonlineID.Location = New System.Drawing.Point(761, 18)
        Me.fldRegonlineID.Margin = New System.Windows.Forms.Padding(0)
        Me.fldRegonlineID.Name = "fldRegonlineID"
        Me.fldRegonlineID.ReadOnly = True
        Me.fldRegonlineID.Size = New System.Drawing.Size(85, 20)
        Me.fldRegonlineID.TabIndex = 436
        Me.fldRegonlineID.Text = "RegonlineID"
        '
        'TextBox4
        '
        Me.TextBox4.BackColor = System.Drawing.SystemColors.Control
        Me.TextBox4.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainEventSetupBindingSource, "RegonlineNum", True))
        Me.TextBox4.Enabled = False
        Me.TextBox4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox4.ForeColor = System.Drawing.SystemColors.ControlDark
        Me.TextBox4.Location = New System.Drawing.Point(761, 18)
        Me.TextBox4.Margin = New System.Windows.Forms.Padding(0)
        Me.TextBox4.Name = "TextBox4"
        Me.TextBox4.Size = New System.Drawing.Size(85, 20)
        Me.TextBox4.TabIndex = 436
        '
        'frmMainWEvent
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.AutoSize = True
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.CausesValidation = False
        Me.ClientSize = New System.Drawing.Size(1148, 646)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.fldRegonlineID)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.btnDelete)
        Me.Controls.Add(Me.fldRegID)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.fldSKU)
        Me.Controls.Add(Me.TextBox2)
        Me.Controls.Add(Me.StatusBar1)
        Me.Controls.Add(Me.fldEventNID)
        Me.Controls.Add(Me.btnHelp)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.fldEventID)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.lblRegion)
        Me.Controls.Add(Me.lblEventID)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Menu = Me.MainMenu1
        Me.Name = "frmMainWEvent"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "W. EVENT DETAIL"
        Me.TabControl1.ResumeLayout(False)
        Me.pgSetup.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.MainEventSetupBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dsMainWEvent, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlCaterer.ResumeLayout(False)
        Me.pnlCaterer.PerformLayout()
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.pgRegistration.ResumeLayout(False)
        CType(Me.grdMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pgChecklist.ResumeLayout(False)
        Me.pgChecklist.PerformLayout()
        CType(Me.dgvChecklist, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.pnlAddress.ResumeLayout(False)
        Me.pnlAddress.PerformLayout()
        CType(Me.StatusBarPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.StatusBarPanelID, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.StatusBarPanel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel4.ResumeLayout(False)
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

#Region "Load"

    'LOAD
    Private Sub frmMainWEvent_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles MyBase.Load

        isLoaded = False
        Me.SuspendLayout()

SetConnections:
        Me.MainEventSetupTableAdapter.Connection = sc

SetDefaults:
        mainTopic = "Event"
        mainDS = Me.dsMainWEvent
        mainTbl = Me.dsMainWEvent.MainEventSetup
        mainBSrce = Me.MainEventSetupBindingSource

        ctlIdentify = Me.editTitle
        ctlNeutral = Me.btnHelp

        If usr = RegonlineAdmin.StaffID Or usr = DBAdmin.StaffID Then
            Me.fldEventNID.ReadOnly = False 'Enabled = True
            Me.fldSKU.ReadOnly = False 'Enabled = True
        End If

LoadCombos:
        SetStatusBarText("Loading Combos")
        modGlobalVar.LoadRegionCombo(Me.cboRegion, "Office")
        modGlobalVar.LoadCRGCombo(Me.cboCRG)
        modGlobalVar.LoadEventTypeCombo(Me.cboType, True)

FormSetup:
        modGlobalVar.EnableGridTextboxes(Me.grdMain)
        SetStatusBarText("Setting Captions")
        SetTabCaptions()

        Forms.Add(Me)
        isLoaded = True
        Me.ResumeLayout()
        SetStatusBarText("Done")

    End Sub 'load

    'LIST RELATED FILES
    Public Sub Reload()

        SetStatusBarText("loading org combos")
        MstrChecklistFileExt = String.Empty 'reset spreadsheet file name
LoadDDBasedOnRegion:
        LoadLocations() 'based on region dd
        LoadCatererCombo()
        SetStatusBarText("filling grid")
        FillSecondary()

GetAttachedFileList:
        SetStatusBarText("finding files")
        colEventDocPref.Clear()
        modPopup.FindFiles(ThisID, LinkEventPath, ppFile, ehFile, Me.miOpenFile, Me.btnOpenFile, My.Resources.btnAttached, Me.ToolTip1) ', Nothing, Nothing, colEventDocPref)

ResetVars:
        objHowClose = ObjClose.btnSaveExit
        Me.StatusBarPanelID.Text = mainTopic & " ID: " & ThisID.ToString
        Me.ToolTip1.SetToolTip(Me.cboCRG, cboCRG.Text)

MasterChecklistVisiblity:
        '  restore after testing 2/16
        SpreadsheetButtonVisible("reload") 'in Reload

HostChecklistVisiblity:
        SetStatusBarText("checking host spreadsheet")
        RefreshHostVisibility()

        SetStatusBarText("reload done")
    End Sub 'reload

#End Region    'load

#Region "Update Main"

    'SAVE & EXIT
    Private Sub btnSaveExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
          Handles btnSaveExit.Click
        objHowClose = ObjClose.btnSaveExit
        Me.Close()
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
                        If ctl Is ctlIdentify Then  'insert default data
                            ctl.Text = usrName & " " & Today.ToShortDateString
                            mainBSrce.EndEdit()
                            Me.MainEventSetupTableAdapter.Update(mainTbl) 'save default data
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
            ClassOpenForms.frmMainWEvent = Nothing 'reset global var
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

        If mainDS.HasChanges Or bDirty = True Or
            mainDS.Tables(0).Rows(0).HasVersion(DataRowVersion.Proposed) Then
            mainBSrce.EndEdit()
        Else
            DoUpdate = True
            GoTo CloseAll
        End If

        If mainDS.HasChanges = True Or bDirty = True Then 'this catches delete, crgcbo, save, asksave, save/exit, anyclose
UpdateBackend:
            SetStatusBarText("Updating server")
            Try
                'i =
                Me.MainEventSetupTableAdapter.Update(mainDS)
                DoUpdate = True
            Catch ex As Exception
                modGlobalVar.Msg("ERROR: updating " & mainTopic, ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
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
        'add the Neutral control to the array last to indicate rest are ok if its the first one
        Dim arCtls(0) As Control
        Dim ctl As Control
        Dim i As Integer = 0

        'EventName
        ctl = ctlIdentify
        If Replace(Replace(Replace(ctl.Text, " ", ""), Chr(10), ""), Chr(13), "") = String.Empty Then
            Me.ErrorProvider1.SetError(ctl, "please enter a " & IsNull(ctl.Tag, ctl.Name))
            arCtls(i) = ctl
            i = i + 1
            ReDim Preserve arCtls(arCtls.GetUpperBound(0) + 1)
        Else
            Me.ErrorProvider1.SetError(ctl, "")
        End If

        'TypeofEvent
        ctl = Me.cboType
        If modGlobalVar.ValidateBoundDD(ctl, True, Me.ErrorProvider1, ObjClose.btnSaveExit) = usrInput.OK Then
        Else
            arCtls(i) = ctl
            i = i + 1
            ReDim Preserve arCtls(arCtls.GetUpperBound(0) + 1)
        End If

        'Region
        If modGlobalVar.ValidateBoundDD(ctl, True, Me.ErrorProvider1, ObjClose.btnSaveExit) = usrInput.OK Then
        Else
            arCtls(i) = ctl
            i = i + 1
            ReDim Preserve arCtls(arCtls.GetUpperBound(0) + 1)
        End If

        arCtls(i) = ctlNeutral
        Return arCtls

    End Function

#End Region 'update main

#Region "Validation"

    'VALIDATE required, string
    Private Sub cboRegion_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) _
        Handles cboRegion.Validating, cboType.Validating
        Dim CheckInput As usrInput
        CheckInput = modGlobalVar.ValidateBoundDD(sender, True, Me.ErrorProvider1, ObjClose.CloseByControl)
        If CheckInput = usrInput.OK Then
            e.Cancel = False
        Else
            e.Cancel = True
            sender.droppeddown = True
        End If
    End Sub

    'CRG Filter
    Private Sub cboCRG_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) _
        Handles cboCRG.MouseDown
        If e.Button = System.Windows.Forms.MouseButtons.Right Then
            modPopup.SearchCRG(Me, PointToClient(Control.MousePosition), Me.cboCRG)
            bChanged = True
        End If
    End Sub

    'force cboCRG dirty
    Private Sub cboCRG_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cboCRG.SelectedIndexChanged
        If isLoaded Then
            bDirty = True 'else mystery won't save if is only change
        End If
    End Sub

    'CBO REGION RELOAD OTHER CBOs
    Private Sub cboRegion_Selectedindexchanged(ByVal sender As Object, ByVal e As System.EventArgs) _
        Handles cboRegion.SelectionChangeCommitted
        If Me.cboRegion.SelectedIndex > -1 Then
            LoadCatererCombo()
            LoadLocations()
        End If
    End Sub

#End Region 'validation

#Region "Edit Buttons"

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

    'SAVE
    Private Sub miSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
     Handles miSave.Click
        ctlNeutral.Focus()
        DoUpdate()
    End Sub

    'CANCEL CHANGES
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles miCancel.Click
        Try
            mainBSrce.CancelEdit()
            mainDS.RejectChanges()
        Catch ex As System.Exception
            modGlobalVar.Msg("ERROR: cancelling changes ", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        SetStatusBarText("Changes Cancelled")
    End Sub

    'DELETE
    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles miDelete.Click ', btnDelete.Click

        If modGlobalVar.Msg("CONFIRM DELETE", mainTopic & ": " + IsNull(ctlIdentify.Text, "") & NextLine & " will be marked for deletion and the window closed.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            ctlIdentify.Text = "DELETE: " & IsNull(ctlIdentify.Text, "")
            objHowClose = ObjClose.miDelete
            mainBSrce.EndEdit()
            Me.Close()
        End If
    End Sub

#End Region    'edit buttons

#Region "ADD ITEM"

    'INSERT NEW ITEM to BACKEND and OPEN DETAIL FORM
    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles btnNew.Click, miNew.Click

        AddNew("Registration")
        '1/16 menu not needed until reinstate multiple registration option
        'Dim pp As New ContextMenu
        'Dim eh As EventHandler = AddressOf AddNew

        ''ENHANCE set default as selected tab or conversation if none selected
        'pp.MenuItems.Add("ADD NEW ") 'FOR " + IsNull(Me.txtCaseName.Text, "no case name"))
        'pp.MenuItems.Add("---------------------------------------------")
        'pp.MenuItems.Add("Registration", eh)
        '' pp.MenuItems.Add("Single Registration", eh)
        '' pp.MenuItems.Add("Registration with Guest", eh)
        'pp.MenuItems(0).DefaultItem = True
        '' cm.MenuItems.Add("Grant", eh)
        'pp.Show(Me, PointToClient(Control.MousePosition))
    End Sub


    Private Sub AddNew(ByVal txt As String) '(ByVal obj As Object, ByVal ea As EventArgs)
        ' Dim newID As Integer
        Me.StatusBarPanel1.Text = "Adding new " & txt 'obj.text
        If modGlobalVar.msg("About to enter a new " & txt, "Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then
            Me.StatusBarPanel1.Text = "Ready"
            Exit Sub
        End If

        Select Case txt 'obj.text
            Case "Registration"
                Dim frm As New frmNewWReg2
                If frm.Loadcombos() Then
                    frm.SelectCombo(frm.cboEvent, ThisID)
                End If
                '  frm.cboRegistrant.SelectedIndex = -1
                frm.cboRegistrant.Focus()
                frm.ShowDialog()

                '  newID = modPopup.InsertRegistration(0, ThisID, 0)
                'GoTo OpenForm
                '--------------------
                '  InsertRegistration(0, Me.fldEventID.Text, 0)
                '  i = NextOrderNumber()
                '  gPayment.EventID = Me.fldEventID.Text
                '  gPayment.ContactID = 0

                '                ' str = " INSERT INTO tblEventRegOrder2(OID, DtOrder, Source, Regions) VALUES (" & i & ", N'" & DateTime.Now & "', N'" & usrFirst & "', ' " & usrRegion & "')"

                ''  LoadWEventDD(Today())

                'str = "INSERT INTO tblEventReg2 (OrderNum,EventNum, contactnum, RegDate, EnteredBy) VALUES (" & i & "," & Me.fldEventID.Text & ",0, N'" & Now & " ', N'" & usrFirst & "'); SELECT @@Identity"

                'Case "Single Registration"
                '    str = "INSERT INTO tblEventReg2 (EventNum, contactnum, RegDate, EnteredBy) VALUES (" & Me.fldEventID.Text & ",0, N'" & Now & " ', N'" & usrFirst & "'); SELECT @@Identity"
                'Case Is = "Registration with Guest"
                '    'so can generate unique Order#; Registrar = true
                '    str = "INSERT INTO tblEventReg2(EventNum, ContactNum, RegDate, EnteredBy, Registrar) VALUES (" & Me.fldEventID.Text & ",0, N'" & DateTime.Now & "', N'" & usrFirst & "',1); SELECT @@Identity" ', N'1/1/2001')"
            Case Else
                Exit Sub
        End Select

        '      ..................................
        'InsertNewItem:
        '        If Not SCConnect() Then
        '            Exit Sub
        '        End If

        '        '   Dim myTrans As SqlClient.SqlTransaction = sc.BeginTransaction()
        '        Dim cmd As New SqlClient.SqlCommand(str, sc) ', myTrans)

        '        Try
        '            newID = cmd.ExecuteScalar()
        '            '      myTrans.Commit()
        '        Catch exce As Exception
        '            modGlobalVar.Msg(exce.Message, MessageBoxIcon.Error, "ERROR execute nonquery")
        '            '    myTrans.Rollback()
        '            sc.Close()
        '            Exit Sub
        '        End Try

        '        sc.Close()

        'OPEN EVENT MAIN FORM TO EDIT NEW ED EVENT
        ' Select Case obj.text
        '   Case "Single Registration"
        '      LoadWEventDD(Today())
        ' mdGlobalVar.OpenMainWorkshopRegistration(newID, "entering New Registration for: " & Me.editTitle.Text, False, "Registrant")
        '     mdGlobalVar.OpenMainWReg2(newID, "entering New Registration for: " & Me.editTitle.Text, False, "Registrant")
        'Case Is = "Registration with Guest"
        'OpenForm:Now covered in insertNEw module
        '        Try
        '            '   mdGlobalVar.OpenMainWOrder(i, False)
        '            modGlobalVar.OpenMainWReg2(newID, "New registration", True, "Registrant")
        '        Catch ex As Exception
        '            modGlobalVar.Msg("ERROR: opening order detail", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        '        End Try
        'OpenNewWOrder2(newID, "Entering New Order", False, True)
        ' End Select

        'da = frmAdd.daMainEdEvent
        'da.SelectCommand.Connection = sc    '.ConnectionString = sqlCon
        'da.SelectCommand.Parameters("@ID").Value = cmdNewID.ExecuteScalar
        'da.Fill(frmAdd.DsMainEd1)
        'sc.Close()
        'frmAdd.ShowDialog()
        Me.StatusBarPanel1.Text = "Done"
        MouseDefault()

        '        '========================================
        '        'If modGlobalVar.Msg("Are you sure?", MessageBoxButtons.YesNo, "About to enter a new Registration for " & Me.editTitle.Text) = DialogBoxResult.No Then
        '        '    Exit Sub
        '        'End If
        '        If Me.fldEventID.Text = String.Empty Then
        '            modGlobalVar.Msg("please select an event first", MessageBoxIcon.Exclamation, "Unable to enter new Registration")
        '            Exit Sub
        '        End If
        '        Dim str As String
        '        str = "INSERT INTO tblEventReg2 (EventNum, contactnum, RegDate, EnteredBy) VALUES (" & Me.fldEventID.Text & ",0, N'" & Now & " ', N'" & usrFirst & "'); SELECT @@Identity"

        'InsertNewItem:
        '        If Not SCConnect() Then
        '            Exit Sub
        '        End If

        '        Dim myTrans As SqlClient.SqlTransaction = sc.BeginTransaction()
        '        Dim cmd As New SqlClient.SqlCommand(Str, sc, myTrans)
        '        Dim newID As Integer
        '        Try
        '            newID = cmd.ExecuteScalar()
        '            myTrans.Commit()
        '        Catch exce As Exception
        '            modGlobalVar.Msg(exce.Message)
        '            myTrans.Rollback()
        '            sc.Close()
        '            Exit Sub
        '        End Try
        '        'RetrieveNewID:
        '        '        Dim cmdNewID As New SqlCommand
        '        '        Dim newID As Integer
        '        '        cmdNewID.CommandText = "SELECT @@Identity"
        '        '        cmdNewID.Connection = sc
        '        '        Try
        '        '            newID = cmdNewID.ExecuteScalar
        '        '        Catch en As Exception
        '        '            modGlobalVar.Msg(en.Message)
        '        '        End Try
        '        '        ' modGlobalVar.Msg(newID)
        '        sc.Close()
        'OpenForm:

        '        LoadWEventDD(Today())
        '        mdGlobalVar.OpenMainWorkshopRegistration(newID, "entering New Registration for: " & Me.editTitle.Text, False, "Registrant")

        '        'TODO update count on tab
    End Sub


#End Region    'add item

#Region "Fill Datasets"

    'GET TAB CAPTION COUNT
    Public Sub SetTabCaptions()

SetTabCaptionsWCount:
        Dim cmdCntID As New SqlCommand
        Dim iAttended As Integer

        cmdCntID.Connection = sc
        If Not SCConnect() Then
            Exit Sub
        End If

        Try
            cmdCntID.CommandText = modGlobalVar.CountValidRegistrations(ThisID, "EventDetail")
            iOpeningCount = cmdCntID.ExecuteScalar()
            cmdCntID.CommandText = modGlobalVar.CountValidRegistrations(ThisID, "Attended")
            iAttended = cmdCntID.ExecuteScalar()
        Catch ex As Exception
            'modGlobalVar.Msg(ex.Message)
        End Try
        Me.TabControl1.TabPages("pgRegistration").Text = IsNull(iOpeningCount, 0).ToString() & "  Registrations, " & IsNull(iAttended, 0).ToString & " Attended"


        'TODO Add Warning section

        sc.Close()
        cmdCntID.Dispose()

    End Sub

#Region "LoadSecondary"

    'refresh registrations on tab change or reload
    Public Sub FillSecondary()
        SetStatusBarText("Refreshing registrations")

        Dim sql As New SqlCommand("[getEventRegistrantList2]", sc)
        sql.CommandType = CommandType.StoredProcedure
        sql.Parameters.AddWithValue("@EventID", ThisID)
        sql.Parameters.AddWithValue("@Which", "wEvents")
        Dim myReader As SqlDataReader

        tblRegistrantList.Clear()
        If Not SCConnect() Then
            Exit Sub
        End If
        myReader = sql.ExecuteReader(CommandBehavior.CloseConnection)
        Try
            tblRegistrantList.Load(myReader)
        Catch ex As Exception
            modGlobalVar.Msg("ERROR: Can't load Registrants", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            myReader.Close()
        End Try
        '  Dim strFilter As String = "ContactNum > 0 and Cancelled = 0"
        '  Dim dvRowCount As New DataView(tblRegistrantList)
        '  dvRowCount.RowFilter = strFilter

        iRowCount = tblRegistrantList.Rows.Count 'dvRowCount.Count
        'modGlobalVar.Msg("row count:" & iRowCount.ToString, , "thisID: " & ThisID.ToString)
        Me.grdMain.DataSource = tblRegistrantList
        dv = New DataView(tblRegistrantList)


        'Me.dsGetEventRegList2a.Clear()
        'Me.dsGetEventRegList2a.EnforceConstraints = False
        'Try
        '    Me.getEventRegistrantList2TableAdapter.Fill(Me.dsGetEventRegList2a.getEventRegistrantList2, ThisID, "wEvents")
        'Catch ex As Exception
        '    modGlobalVar.Msg(ex.Message, MessageBoxIcon.Error, "Can't load Registrants")
        'End Try
        '  Me.grdMain.CaptionText = IsNull(Me.DsGetRegistrantsMainWEvent.GetRegistrants.Rows.Count.ToString, 0) & "  Registrants"
        SetTabCaptions()
        ' HighlightCancelled()

        SetStatusBarText("Registrations Done")
    End Sub


#End Region


    Private Sub TabControl1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles TabControl1.SelectedIndexChanged

        If Not isLoaded Then    'SelctedIndexChanges occurs before load!!
            Exit Sub
        Else
            Me.grdMain.CaptionText = ""
            Select Case TabControl1.SelectedTab.Tag
                Case Is = "SETUP"
                Case Is = "REGISTRATIONS"
                    FillSecondary()
                Case Is = "BREAKOUT"
            End Select
        End If
    End Sub


#End Region   'fill datasets

#Region "Datagrid"

    'CELL CHANGE - HIGHLIGHT ROW
    Private Sub grdMain_CurrentCellChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles grdMain.CurrentCellChanged

        Me.fldRegID.Text = Me.grdMain.Item(grdMain.CurrentCell.RowNumber, 0)
HighlightSelectedRow:
        Me.grdMain.Select(grdMain.CurrentCell.RowNumber)

    End Sub

    '    'CAPTURE RIGHT MOUSE CLICK TO FILTER APPROPRIATE GRID
    Protected Sub grdAll_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) _
        Handles grdMain.MouseDown

        ' Me.btnDelete.Visible = False
        '  Dim tbl As Object
        Dim strHdr As String    'text for grid header
        ' Dim statusMbj As Object

        hti = sender.HitTest(e.X, e.Y)
IfRightMouseclick:
        If e.Button = System.Windows.Forms.MouseButtons.Right Then
            '            tbl = dt
            strHdr = Me.TabControl1.SelectedTab.Tag   'strDGM

SetClearFilter:
            If hti.Type = DataGrid.HitTestType.Cell Then    'SET FILTER
                If IsDBNull(sender.Item(hti.Row, hti.Column)) Then 'nulls cause filter error
                    modGlobalVar.Msg(MsgCodes.filterEmpty)
                    Exit Sub
                Else
                    grdFilter(sender, dt, strHdr, dv)
                End If
            Else    'not in cell, CLEAR FILTER
                Me.grdMain.DataSource = tblRegistrantList    'Me.DsGetRegistrants.GetRegistrants 'removes dv.rowfilter
                sender.CaptionText = iRowCount.ToString & "  " & strHdr 'Me.DsGetRegistrantsMainWEvent.GetRegistrants.Rows.Count.ToString & "  " & strHdr 'DsOrgSearch1.tblOrg.Rows.Count.ToString
                statusM = ""
                SetStatusBarText(statusM)
            End If
IfLeftMouseClick:
        Else    'left mouse
            '      strbActiveGrid.Replace(strbActiveGrid.ToString, Me.TabControl1.SelectedTab.Tag)
        End If

    End Sub


    'FILTER METHOD
    Private Sub grdFilter(ByVal grd As Object, ByVal tbl As Object, ByVal strHdr As String, ByVal dv As DataView)
        Dim strFilter As String
        Dim myColumns As GridColumnStylesCollection

        myColumns = grd.TableStyles(0).GridColumnStyles
        strFilter = myColumns(hti.Column).MappingName
        strFilter = strFilter & " = '" & grd.Item(hti.Row, hti.Column) & "'"
        dv.RowFilter = strFilter
        grd.DataSource = dv
        grd.CaptionText = dv.Count.ToString & "/" & iRowCount.ToString & "  " & strHdr 'Me.DsGetRegistrantsMainWEvent.GetRegistrants.Rows.Count.ToString & "  " & strHdr
        statusM = strHdr & " filtered on " & myColumns(hti.Column).HeaderText & " = " & Me.grdMain.Item(Me.grdMain.CurrentRowIndex, hti.Column)
        SetStatusBarText(statusM)

    End Sub


    'CLEAR SELECTION FROM DATAGRIDS AND LOSE FOCUS (note: selection stays with arrow)
    Private Sub grdUnselect_Click(ByVal sender As Object, ByVal e As System.EventArgs) _
            Handles MyBase.Click
        If grdMain.CurrentRowIndex > -1 Then
            Me.grdMain.UnSelect(grdMain.CurrentRowIndex)
            Me.grdMain.NavigateBack()
        End If
        '  Me.btnDelete.Visible = True
    End Sub

    'FILTER DATAGRID USING DATAVIEW ROWFILTER
    Protected Sub grdRegister_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        If e.Button = System.Windows.Forms.MouseButtons.Right Then
            hti = grdMain.HitTest(e.X, e.Y)
            If hti.Type = DataGrid.HitTestType.Cell Then
                If IsDBNull(grdMain.Item(hti.Row, hti.Column)) Then 'nulls cause filter error
                    Exit Sub
                Else
                    Dim strFilter As String
                    Dim myColumns As GridColumnStylesCollection
                    myColumns = grdMain.TableStyles(0).GridColumnStyles
                    strFilter = myColumns(hti.Column).MappingName
                    strFilter = strFilter & " = '" & grdMain.Item(hti.Row, hti.Column) & " '"
                    dv.RowFilter = strFilter
                    grdMain.DataSource = dv
                    Me.grdMain.CaptionText = dv.Count.ToString & "/" & iRowCount.ToString & " Registrations" 'Me.DsGetRegistrantsMainWEvent.GetRegistrants.Rows.Count.ToString & "  Registrations"
                    Me.StatusBar1.Text = "Registrations filtered on " & myColumns(hti.Column).HeaderText
                End If
            Else
                '    'CLEAR FILTER
                Me.grdMain.DataSource = tblRegistrantList 'Me.DsGetRegistrants.GetRegistrants
                '    'RESET CAPTION TOTAL

                Me.grdMain.CaptionText = iRowCount.ToString & " Registrations"
                'Me.grdMain.CaptionText = Me.DsGetRegistrantsMainWEvent.GetRegistrants.Rows.Count.ToString & "  Registrations" 'DsOrgSearch1.tblOrg.Rows.Count.ToString
                Me.StatusBar1.Text = ""
            End If

        End If
    End Sub




    ''highlight cancelled
    'Private Sub grdMain_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles grdMain.Paint
    '    '  If e..ColumnIndex = 4 Then   'do on first cell in case rest are out of sight and don't call format
    '    If IsNull(Me.grdMain.Item(5, grdMain.CurrentRowIndex).Value, "") = "Cancelled" Then
    '        Me.grdMain(5, grdMain.CurrentRowIndex).Style.BackColor = Color.LightGray
    '    End If
    '    'End If
    'End Sub


    ''CLEAR SELECTION FROM DATAGRIDS AND LOSE FOCUS (note: selection stays with arrow)
    'Private Sub pgRegistration_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    If grdMain.CurrentRowIndex > -1 Then
    '        Me.grdMain.UnSelect(grdMain.CurrentRowIndex)
    '        Me.grdMain.NavigateBack()
    '    End If

    'End Sub

#End Region     'datagrid

#Region "GridDoubleclick"

    'OPEN MAIN DETAIL FORMS FROM DATAGRID
    Private Sub DataGridDouble(ByVal sender As Object, ByVal e As MouseEventArgs)
        Try
            If (DateTime.Now < modGlobalVar.CheckDouble(sender, e).AddMilliseconds(SystemInformation.DoubleClickTime)) Then
                OpenForms()
            End If
        Catch
        End Try
    End Sub

    Private Sub grdMain_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) _
        Handles grdMain.DoubleClick
        OpenForms()
    End Sub

#End Region 'grid doubleclick

#Region "Open Forms"

    'FILTERS ON REGISTRANT
    Private Sub OpenForms()
        Dim cri As Integer
        cri = Me.grdMain.CurrentRowIndex
        MouseWait()

        modGlobalVar.OpenMainWReg2(Me.grdMain.Item(Me.grdMain.CurrentRowIndex, 0), "Registration for: " & Me.editTitle.Text, False, "Registrant")
        Me.StatusBarPanel1.Text = "Done"
        MouseDefault()
    End Sub

    'FILTERS ON EVENT so includes multiple registrants
    Private Sub miViewAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles miViewAll.Click
        MouseWait()

        modGlobalVar.OpenMainWReg2(ThisID, "All Registrations for: " & Me.editTitle.Text, False, mainTopic)

        Me.StatusBarPanel1.Text = "Done"
        MouseDefault()
    End Sub

    'OPEN PENDING
    Private Sub miPending_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles miPending.Click
        OpenPendingRegistrations(IsNull(Me.fldPending.Text, 0), ThisID, IsNull(Me.cboRegion.Text, usrRegion))
    End Sub

#End Region  'open forms

#Region "General"

    'SET DISCOUNT DEFAULTS
    Private Sub editFee_Leave(ByVal sender As Object, ByVal e As System.EventArgs) _
        Handles editFee.Leave
        If CType(Me.editFee.Text, Int16) = 30 Then
            Me.editDiscount.Text = "5"
            Me.editTeamMin.Text = "3"
        End If
    End Sub

    'UNDO
    Protected Overrides Function ProcessDialogKey(ByVal keyData As Keys) As Boolean
        If keyData = System.Windows.Forms.Keys.Escape Then
            ' If Me.ActiveControl Is Me.cboType Then  'required field
            ' Else
            modPopup.UndoCtl(Me.ActiveControl)
            bChanged = True
            ' End If
            Return True  ' True means we've processed the key
        Else
            Return MyBase.ProcessDialogKey(keyData)
        End If
    End Function

    'RIGHT CLICK MENU
    Private Sub textbox_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) _
        Handles editDates.MouseDown, editNotes.MouseDown, editDescription.MouseDown, editCateringNotes.MouseDown
        If e.Button = System.Windows.Forms.MouseButtons.Right Then
            Dim pp As New ClassRtbContextMenu(sender)
            pp.Show(Me, PointToClient(Control.MousePosition))
        End If
    End Sub

    'SET STATUS BAR LEFT TEXT
    Private Sub SetStatusBarText(ByVal str As String)
        Me.StatusBar1.Panels(0).Text = str
    End Sub

    ' COPY CURRENT ID
    Private Sub StatusBar1_DoubleClick(sender As System.Object, e As System.EventArgs) Handles StatusBar1.DoubleClick
        Clipboard.SetText(ThisID)
    End Sub

    'copy to clipboard
    Private Sub fldRegonlineID_TextChanged(sender As System.Object, e As System.EventArgs) Handles fldRegonlineID.DoubleClick
        Clipboard.SetText(IsNull(sender.Text, 0))
    End Sub

    'CALL REMOVE PARAGRAPHS
    Private Sub txtLostFocus(ByVal sender As Object, ByVal e As System.EventArgs) _
        Handles editTitle.Leave
        modGlobalVar.RemoveLineFeeds(sender)
    End Sub

    'HELP BUTTON
    Private Sub btnHelp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
         Handles btnHelp.Click, miHelp.Click
        modGlobalVar.Msg("HELP", "TO ADD NEW EVENT: " & NextLine & " Go To Event Search window and click New button" & NextLine & NextLine & "TO DELETE EVENT: click the Delete button" & NextLine & NextLine & "TO DELETE REGISTRATION: go to that registration detail, and click the Delete button found there", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    'show definitions
    Private Sub ShowDef(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles miDef.Click

        modPopup.ShowDefinitions("Workshop")
    End Sub

    'INDICATE HOW MANY CHARACTERS LEFT FOR NAMETAG
    Private Sub TextBox1_TextChanged(sender As System.Object, e As System.EventArgs) Handles TextBox1.TextChanged
        Me.lblNametag.Text = "(" & TextBox1.Text.Length.ToString & ") Print on Nametags:"
    End Sub

#End Region 'General

#Region "Format PhoneZipCity"

    'VERIFY PHONE NUMBER FORMATTED OK
    'note - this will run even if no change - 

    Private Sub editPhone_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles editLocationPhone.Leave, editCatererPhone.Leave
        If Len(sender.text) > 0 Then
            modGlobalVar.LeavePhone(sender, "USA")
        End If
    End Sub



    'VERIFY ZIP FORMATTED OK
    Private Sub editZipCode_Leave(ByVal sender As Object, ByVal e As System.EventArgs) _
        Handles editZipCode.Leave

        'Select Case Len(sender.text)
        '    Case 5 '
        '        If IsNumeric(sender.text) Then  'good
        '            Exit Sub
        '        End If
        '    Case 6
        '        If Mid(sender.text, 6, 1) = "-" Then
        '            sender.text = Mid(sender.text, 1, 5)
        '            Exit Sub
        '        End If
        '    Case 10 And Mid(sender.text, 6, 1) = "-" 'good
        '        Exit Sub
        'End Select
        If Me.editZipCode.Text = String.Empty Then
        Else
            Dim rtrn As String = modGlobalVar.FormatZip(sender, e, Me.editState.Text)
            Select Case rtrn
                Case usrInput.Ignore
                    Exit Sub 'out of state
                Case usrInput.OK    'proceed
                Case usrInput.Retry
                    Me.editZipCode.Focus()
                    Exit Sub
                Case Else
                    Me.editZipCode.Text = rtrn
            End Select
            rtrn = Nothing
        End If
    End Sub



    'CHECK KEYBOARD INPUT IS A NUMBER - why bother, why not just check when leave??
    'Private Sub editLocationPhone_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) _
    '    Handles editLocationPhone.KeyPress
    '    ValidatePhone(Asc(e.KeyChar), sender)
    'End Sub
#End Region 'format

#Region "Merge"

    'NAMETAG MERGE
    Private Sub miMergeNametag_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles miMergeNametag.Click

        Dim datafileName, templateName As String 'name for excel data file and template
        Dim strHeading As String 'nametag title
        Dim sheetLabel As String '= "NameTags"; name of tab on spreadsheet
        Dim sProc As String 'name of [stored procedure]
        Me.miSave.PerformClick()    'so when update nametag doesn't cause concurrency error on close
        MouseWait()

        Select Case sender.tag
            Case Is = "Nametag"
                datafileName = "NametagDataDoc"
                sheetLabel = "NameTags"
                sProc = "[MergeNametag]"
                GoTo Nametag
            Case Is = "InfoLttr"
                datafileName = "DataDoc"
                sheetLabel = sender.tag
                sProc = "[MergeNametag]"
                GoTo othermerge
            Case Else
                GoTo OtherMerge
        End Select

NAMETAG:
        strHeading = GetRegionTitle()
        If modPopup.StrmWriter(sProc, ThisID, datafileName, strHeading) Then
            If modPopup.DataToExcel(datafileName, sheetLabel) = String.Empty Then
                modGlobalVar.msg("ERROR: DataToExcel", "MergeNametag could not create datafile", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Else
                If i = 1653 Then 'flourishing 2015 has different nametag
                    MergePerform(SOPPath & "MergeInfoCtrtoWord\EventNameTagFlourishing.dot", datafileName, sheetLabel)
                Else
                    MergePerform(SOPPath & "MergeInfoCtrtoWord\EventNameTag.dot", datafileName, sheetLabel)
                End If
            End If
        Else
        End If
        GoTo CLoseAll

OTHERMERGE:  'not used????
        '        '1-GET DATA
        '        If modPopup.StrmWriter(sproc, thisid, datafileName, strHeading) Then
        '            '2-PUT IN EXCEL
        '            If modPopup.DataToExcel(datafileName, sheetLabel) = String.Empty Then
        '                modGlobalVar.msg("ERROR: DataToExcel", sender.tag & " could not create datafile", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '            Else
        '                '3-PERFORM MERGE INTO WORD
        '                MergePerform(SOPPath & templateName, datafileName, sheetLabel)
        '            End If
        '        End If
        GoTo CLoseAll

CLoseAll:
        datafileName = Nothing
        MouseDefault()
    End Sub

    'for nametags by region
    Private Function GetRegionTitle() As String
        Select Case Me.lblRegion.Text
            Case "Central"
                GetRegionTitle = "Indianapolis Center for Congregations"
            Case "NE", "NW", "SW", "SW", "South"
                GetRegionTitle = "Center for Congregations " & Me.lblRegion.Text
            Case Else
                GetRegionTitle = "Center for Congregations"
        End Select
    End Function

    'send emails
    Private Sub miEmailRegistered_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles miEmailRegistered.Click, miEmailAttended.Click, miEmailNotAttend.Click
        '*************************
        'originally:  'OPEN OUTLOOK WITH LIST OF NAMES FROM TEXT FILE
        'then changed to mailchimp
        'currently using Delivra 11/15
        ' modPopup.EmailEvent(sender.tag, ThisID, Me.editTitle.Text)
        '**************************
        modPopup.EmailDelivraEvent(Me.fldEventID.Text, sender.tag)

        'OPEN EXCEL  WITH THOSE W NO EMAIL
        Dim rows As Integer = StrmWriterEmailPrint(sender.tag) 'eventnums, attended
        If rows > 0 Then
            MsgBox("see '" & UserPath & "NoEmails.csv', open in the background", MsgBoxStyle.Information, rows.ToString & " " & sender.tag & " with no email")
        End If
    End Sub

    'word merge template to edit before merging
    Private Sub miInfoLetter_Click(ByVal sender As Object, ByVal e As System.EventArgs) _
        Handles miMergeTemplate.Click

        CountEmails()

        If DataToExcel("datadoc2", "EventIntro") = String.Empty Then
            modGlobalVar.msg("ERROR: DataToExcel", "EmailPrint1 could not create datafile", MessageBoxButtons.OK, MessageBoxIcon.Error)
            GoTo closeall
        Else

        End If

        Try
            modPopup.MergePerform(SOPPath & "MergeInfoCtrtoWord\MergeInfoLttr.dotx", "DataDoc2", False)
            MsgBox("merge done")
        Catch ex As Exception
            modGlobalVar.msg("ERROR: could not mergePerform", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
CloseAll:

    End Sub

    'counts emails
    Private Sub CountEmails()

        Select Case modPopup.StrmWriterEmailPrint("[MergeEvent]", ThisID, "DataDoc2", "InfoLetter")
            Case Is = 0 'no results
                modGlobalVar.msg(MsgCodes.noResult)
            Case Is = 1 'everyone had email so open only email template
                modGlobalVar.msg("Yes - everyone has an email address", "so you can merge to email and catch everyone." & NextLine & "To Merge to an Email: " & NextLine & "1) remove address information from the merge template, " & NextLine & "2) filter on email greater than nothing, " & NextLine & "3) merge to email.", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Case Else 'were leftovers w no email so open email and snailmail template
                modGlobalVar.msg("No - everyone does not have an email address", "you will have to print some hard copies to snail mail.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Select
 
    End Sub

#End Region ' merge/send

#Region "Checkboxes"

    'UPDATE BE ATTENDANCE
    Private Sub UpdateCheckboxes(ByVal YesNo As Integer, ByVal WhatField As String)
        Dim cmd As New SqlClient.SqlCommand
        cmd.Connection = sc
        cmd.Parameters.Add("@IDVal", SqlDbType.Int)
        cmd.Parameters.Add("@UpdateFld", SqlDbType.VarChar)
        cmd.Parameters.Add("@UpdateVal", SqlDbType.Int)
        cmd.Parameters("@IDVal").Value = ThisID
        cmd.Parameters("@UpdateVal").Value = YesNo
        cmd.Parameters("@UpdateFld").Value = WhatField
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = "[EventAttendanceUpdate]"

        If Not SCConnect() Then
            Exit Sub
        End If

        Try
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            modGlobalVar.msg("EROR: update", WhatField & " to " & YesNo & NextLine & ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            sc.Close()
        End Try
    End Sub

    'update nametag yes
    Private Sub mmi1Nametag_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
         Handles mmiCheckNametag.Click
        UpdateCheckboxes(1, "Nametag")
    End Sub

    'update nametag no
    Private Sub mmi0Nametag_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles mmiUncheckNametag.Click
        UpdateCheckboxes(0, "Nametag")
    End Sub

    'update infosent
    Private Sub CheckToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles mmiCheckInformation.Click
        UpdateCheckboxes(1, "Information")
    End Sub

    'update info not sent
    Private Sub UncheckToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles mmiUncheckInformation.Click
        UpdateCheckboxes(0, "Information")
    End Sub

    'update attendance yes
    Private Sub UncheckToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
         Handles mmiCheckAttended.Click
        UpdateCheckboxes(1, "Attendance")
    End Sub

    'update attendance no
    Private Sub UncheckToolStripMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles mmiUncheckAttended.Click

        UpdateCheckboxes(0, "Attendance")
    End Sub


    'show update choices
    Private Sub btnUpdate_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) _
       Handles miUpdate.Click
        Me.ContextMenuStrip1.Show(Me, PointToClient(Control.MousePosition))
    End Sub

#End Region

#Region "Reports"

    'VIEW REPORT VIEWER
    Private Sub miReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles miReport.Click, btnReport.Click
        If iOpeningCount = 0 Then
            modGlobalVar.msg(MsgCodes.noResultCancel)
        Else
            OpenReportRegistration(ThisID, Me.editTitle.Text)
        End If
    End Sub

    'CANCELLATION REPORT
    Private Sub miCancellationRpt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles miCancellationRpt.Click
        OpenReportCancellation(ThisID, Me.editTitle.Text)
    End Sub

    'PAYMENT REPORT
    Private Sub miRptPayment_Click(sender As System.Object, e As System.EventArgs) Handles miRptPayment.Click
        Dim fullDatafileName As String
        MouseWait()
        If modPopup.StrmWriter("[EventReports]", CType(ThisID, Integer), "DataDocPayments", "Payments") = True Then
            fullDatafileName = modPopup.DataToExcel("DataDocPayments", "Payments")
            If fullDatafileName = String.Empty Then
                modGlobalVar.msg("ERROR: DataToExcel", "EventReports could not create datafile", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Else
                OpenFile(fullDatafileName)
            End If
        End If
        fullDatafileName = Nothing
        MouseDefault()
    End Sub

#End Region 'reports

#Region "Attach Files"

    'COPY FILE to shared drive
    Private Sub btnAttach_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles miAttach.Click

        Try
            modPopup.AttachFiles(mainTopic, LinkEventPath, ThisID)
        Catch ex As Exception
        End Try
        'refresh list
        modPopup.FindFiles(ThisID, LinkEventPath, ppFile, ehFile, Me.miOpenFile, Me.btnOpenFile, My.Resources.btnAttached, Me.ToolTip1) ', colEventDocPref)
    End Sub

    'OPEN FILE SHOW POPUP
    Private Sub btnOpenFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnOpenFile.Click

        If sender.Tag.ToString = "Attach" Then
            Me.miAttach.PerformClick()
        Else
            ppFile.Show(Me, New Point(600, 10))
        End If
    End Sub

    'OPEN FILE POPUP HANDLER
    Private Sub ehOpenFile(ByVal obj As Object, ByVal ea As EventArgs)
        MouseWait()
        If obj.Text = "Attach File" Then
            Me.miAttach.PerformClick()
        Else

            If OpenFile(LinkEventPath & ThisID.ToString & " " & obj.text) = True Then
                SetStatusBarText("file opened")
            Else
                MsgBox(LinkEventPath & ThisID.ToString & " " & obj.text, , "nope")
                SetStatusBarText("network error")
            End If
            MouseDefault()
        End If

    End Sub

#End Region 'attach files

#Region "Location"

    'LOAD LOCATION DD
    Private Sub LoadLocations()

        MouseWait()
        Dim cmd As New SqlClient.SqlCommand("SELECT TOP (100) PERCENT vwGetOrgCity.OrgID, vwGetOrgCity.OrgandCity FROM vwGetOrgCity INNER JOIN vwGetValidOrgs AS vo ON vwGetOrgCity.OrgID = vo.OrgID " _
                                            & " AND vwGetOrgCity.SatelliteRegion = '" + IsNull(Me.cboRegion.Text, "%") + "' WHERE   (vo.Active = 1) OR (vwGetOrgCity.OrgandCity LIKE '%center for congregations%') ORDER BY vwGetOrgCity.OrgandCity", sc)
        Dim dtLocation As New DataTable
        If Not SCConnect() Then
            Exit Sub
        End If
        Dim dr As SqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
LoadLocationTable:
        Try
            dtLocation.Load(dr)
        Catch ex As Exception
            modGlobalVar.Msg("ERROR: loading location tbl", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            dr.Close()
            dr = Nothing
        End Try
        Me.cboLocation.DisplayMember = "OrgandCity"
        Me.cboLocation.ValueMember = "OrgID"
        Me.cboLocation.DataSource = dtLocation
        Me.cboLocation.SelectedIndex = -1
        Me.cboLocation.Text = "Select location here"
        MouseDefault()

    End Sub

    'Get LOCATION DETAILS
    Private Sub cboLocation_SelectionChangeCommitted(ByVal sender As Object, ByVal e As System.EventArgs) _
        Handles cboLocation.SelectionChangeCommitted

        Dim sql As New SqlClient.SqlCommand("SELECT * FROM dbo.fParseAddress( " & Me.cboLocation.SelectedValue & ")", sc)
        Dim dr As SqlDataReader
        If Not SCConnect() Then
            Exit Sub
        End If

        dr = sql.ExecuteReader()
        dr.Read()
        Dim x As Integer
        Me.lblOrgNum.Text = Me.cboLocation.SelectedValue.ToString

        x = dr.GetOrdinal("OrgName")
        If IsDBNull(dr(x)) Then
            Me.EditLocation.Text = String.Empty
            GoTo CloseAll
        Else
            Me.EditLocation.Text = dr.GetString(x)
        End If

        x = dr.GetOrdinal("Street1")
        If IsDBNull(dr(x)) Then
            Me.editStreet.Text = String.Empty
        Else
            Me.editStreet.Text = dr.GetString(x)
        End If
        x = dr.GetOrdinal("City")
        If IsDBNull(dr(x)) Then
            Me.editCity.Text = String.Empty
        Else
            Me.editCity.Text = dr.GetString(x)
        End If
        x = dr.GetOrdinal("State")
        If IsDBNull(dr(x)) Then
            Me.editState.Text = String.Empty
        Else
            Me.editState.Text = dr.GetString(x)
        End If
        x = dr.GetOrdinal("Zip")
        If IsDBNull(dr(x)) Then
            Me.editZipCode.Text = String.Empty
        Else
            Me.editZipCode.Text = dr.GetString(x)
        End If
        x = dr.GetOrdinal("Phone")
        If IsDBNull(dr(x)) Then
            Me.editLocationPhone.Text = String.Empty
        Else
            Me.editLocationPhone.Text = dr.GetString(x)
        End If
        x = dr.GetOrdinal("MapURL")
        If IsDBNull(dr(x)) Then
            Me.txtLocationURL.Text = String.Empty
        Else
            Me.txtLocationURL.Text = dr.GetString(x)
        End If
        dr.Close()

        If Me.cboTimeZone.SelectedIndex = -1 Then
            Dim cmdTime As New SqlCommand
            cmdTime.Connection = sc
            cmdTime.CommandText = "[luTimeZone]" 'SELECT TimeZone FROM luCountyZip WHERE County = '" & Me.lblCounty.Text & "'"
            cmdTime.CommandType = CommandType.StoredProcedure
            cmdTime.Parameters.Add("@OrgID", System.Data.SqlDbType.Int)
            cmdTime.Parameters("@OrgID").Value = cboLocation.SelectedValue
            Dim str As String = cmdTime.ExecuteScalar
            Me.cboTimeZone.SelectedIndex = Me.cboTimeZone.FindStringExact(str)
        Else
        End If

        mainBSrce.EndEdit()
HostOverview:
        RefreshHostVisibility()

CloseAll:
        Try
            sc.Close()
        Catch ex As Exception
        End Try
        dr = Nothing
        sql = Nothing
        bDirty = True
        modPopup.UndoCtl(Me.cboLocation)
    End Sub

    'CONCATENATE ADDRESS FOR WEB URL
    Private Sub pnlAddress_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles pnlAddress.Leave

        Dim strAddress As String
        If isLoaded = True Then
            If Me.editStreet.Text > "" And Me.editCity.Text > "" And Me.editState.Text > "" And Me.editZipCode.Text > "" Then
                strAddress = Replace(Me.editStreet.Text + "," + Me.editCity.Text + "," + Me.editState.Text + "," + Me.editZipCode.Text, " ", "%20")
            Else
                Exit Sub
            End If
SetWebAddress:
            If strAddress = String.Empty Then
            Else
                Me.txtLocationURL.Text = "http://maps.google.com/maps?q=" + strAddress 'Replace(Me.editStreet1.Text + "," + Me.editCity.Text + "," + Me.editStateAb.Text + "," + Me.editZip.Text, " ", "%20")
                bChanged = True
            End If
        End If

    End Sub

    'OPEN MAP
    Private Sub txtLocationURL_doubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles txtLocationURL.DoubleClick, txtOnlineLink.DoubleClick
        If sender.text > "" Then
            System.Diagnostics.Process.Start(sender.text)
        End If
    End Sub

    'GOTO ORG
    Private Sub lblGotoOrg_Click(sender As System.Object, e As System.EventArgs) _
        Handles lblGotoOrg.Click
        If Me.lblOrgNum.Text > 0 Then
            Me.WindowState = FormWindowState.Minimized
            modGlobalVar.OpenMainOrg(Me.lblOrgNum.Text, Me.EditLocation.Text)
        End If
    End Sub

#End Region 'location

#Region "Caterer"

    'GO TO CATERER
    Private Sub fldGotoCaterer_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) _
        Handles fldGotoCaterer.DoubleClick

        Dim frm As New frmAddNew
        modGlobalVar.HideTabPage("tbCaterer", frm.TabControl1)
        frm.LoadCaterer()
        If Me.editCaterer.Text > "" Then
            Try
                frm.TblCatererBindingSource.Position = frm.TblCatererBindingSource.Find("CatererName", Me.editCaterer.Text)
            Catch ex As Exception
            End Try

        End If
        frm.ShowDialog()
        LoadCatererCombo()

    End Sub

    'LOAD CATERER DD
    Public Sub LoadCatererCombo()

        Dim cmd As New SqlCommand("SELECT CatererName, CatererName + ' : ' + isnull(City,'')  as NameCity, Phone, Contact, SatelliteRegion FROM tblCaterer where satelliteregion ='" + IsNull(Me.cboRegion.SelectedValue, usrRegion) + "' ORDER BY NameCity", sc)
        cmd.CommandType = CommandType.Text

        If Not SCConnect() Then
            Exit Sub
        End If
        tblCaterer.Clear()

        Try
            tblCaterer.Load(cmd.ExecuteReader)
        Catch ex As Exception
            modGlobalVar.msg("Error: caterer run", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            sc.Close()
        End Try

        Me.cboCaterer.DataSource = tblCaterer
        Me.cboCaterer.SelectedIndex = -1
        Me.cboCaterer.Text = "Select Caterer here"

    End Sub

    'FILL IN CATERER FIELDS from user selection
    Private Sub cboCaterer_SelectionChangeCommitted(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles cboCaterer.SelectionChangeCommitted

        i = cboCaterer.SelectedIndex
        If i > -1 Then
            Me.editCaterer.Text = tblCaterer.Rows(i).Item("CatererName")
            Me.editCatererPhone.Text = tblCaterer.Rows(i).Item("Phone")
            Me.editCateringNotes.Text = tblCaterer.Rows(i).Item("Contact") & " - " & Me.editCateringNotes.Text
            bChanged = True
        End If

    End Sub

#End Region 'caterer

#Region "Master Checklist"

    ' REFRESH CHECKLIST DGV FROM EXCEL
    Private Sub btnRefreshChecklist_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnRefreshChecklist.Click

        '   verified 2/14
        'these 2 lines should never run as button should not be visible if checklist does not exist
        If Me.editMasterName.Text = String.Empty Then
            modGlobalVar.msg("ATTENTION: Missing Information", "Master Event Name required", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        If Me.cboRegion.SelectedIndex = -1 Then ' = String.Empty Then
            modGlobalVar.msg("ATTENTION: Missing Information", "Satellite region required", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        ' strFullName = modPopup.FindFile(LinkEventPath, Me.editMasterName.Text, ".xls*", True)
        Select Case MstrChecklistFileExt  ':IN btnRefreshChecklist

            Case Is = "error"
                SetStatusBarText("network error")
                GoTo closeall
            Case Is = "not found" '"not found"
                modGlobalVar.msg("cancelling request", "File does not exist." & NextLine & NextLine & "Click Create Master Checklist button first.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                miSave.PerformClick()
                SetStatusBarText("done")
                GoTo closeall
            Case Else
                MouseWait()

                Dim objExcel As New Excel.Application
                Dim objWorkbook As Excel.Workbook

                Try
                    Me.dgvChecklist.Columns.Clear()
                    objWorkbook = objExcel.Workbooks.Add(LinkEventPath & MstrChecklistFileExt) ':IN btnRefreshChecklist     LinkedFilePath & "\Events\" & Me.editMasterName.Text & ".xlsm")
                    Dim objSheet As Excel.Worksheet = objWorkbook.Worksheets(1) '"Sheet1")

                    'ADD COLUMNS
                    For x As Integer = 1 To 10
                        If objSheet.Cells(2, x).value > "" Then
                            '     modGlobalVar.Msg(objSheet.Cells(2, cl).value, , "row 2 col " & cl.ToString)
                            Me.dgvChecklist.Columns.Add("col" & x.ToString, objSheet.Cells(2, x).value)
                            Me.dgvChecklist.Columns(x - 1).SortMode = DataGridViewColumnSortMode.NotSortable
                        End If
                    Next x

                    'ADD ROWS 
                    Me.dgvChecklist.RowCount = 100
                    Me.dgvChecklist.RowHeadersWidth = 20

                    'FILL IN DATA
                    For rw As Integer = 3 To 100
                        For cl As Integer = 1 To Me.dgvChecklist.Columns.Count
                            Me.dgvChecklist.Item((cl - 1), (rw - 3)).Value = objSheet.Cells(rw, cl).value
                        Next cl
                    Next rw
                    Dim f As Integer
                    'COLUMN WIDTH
                    If Me.dgvChecklist.Columns(0).HeaderText = "Job" Then
                        Me.dgvChecklist.Columns(0).Width = 50
                        f = 2
                    Else
                        f = 1
                    End If
                    For g As Integer = f To Me.dgvChecklist.Columns.Count - 1
                        Me.dgvChecklist.Columns(g).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                    Next g

                    'SHADE SUBHEADINGS
                    For Each c As Microsoft.Office.Interop.Excel.Range In objSheet.Range("A3:A100")
                        '   Me.dgvChecklist.Item((c.Column - 1), c.Row - 3).Value = objSheet.Cells(c.Row, c.Column).value
                        If c.Interior.Color.ToString = "14211288" Then
                            Me.dgvChecklist.Rows(c.Row - 3).DefaultCellStyle.BackColor = Color.MediumAquamarine
                        End If
                    Next c

                    ReleaseComObject(objSheet)
                    objExcel.Quit()
                Catch ex As Exception
                    ' modGlobalVar.Msg(ex.Message, , "catch error")
                Finally
                    objExcel = Nothing
                    ReleaseComObject(objWorkbook)
                    ReleaseComObject(objExcel)
                End Try
                SetStatusBarText("done")
        End Select

closeall:
        ' strFullName = Nothing
        MouseDefault()
    End Sub

    ' OPEN MASTER CHECKLIST
    Private Sub dgvChecklist_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) _
        Handles dgvChecklist.DoubleClick, miMasterChecklist.Click

        If MstrChecklistFileExt > "" Then
            If OpenFile(LinkEventPath & MstrChecklistFileExt) Then ':IN dblclick grid
                Me.StatusBarPanel1.Text = "Master Checklist opened"
            End If
        End If

        '   verified 2/14
        If Me.editMasterName.Text = String.Empty Then
            modGlobalVar.msg("ATTENTION: missing information ", "Master Name is Required   ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        Else
        End If
        If IsNull(Me.dtpFirstDate.Text, "NOT") = "NOT" Then
            modGlobalVar.msg("ATTENTION: missing information ", "First Date is required   ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        'works, but replaced: OpenSpreadsheet("Events", Me.editMasterName.Text) ', Me.DateTimePicker1.Text)
        'If OpenFile(Me.editMasterName.Text) = True Then
        '  If OpenFile(modPopup.FindFile(LinkedFilePath & "Events\", Me.editMasterName.Text, ".xls*", True)) Then

    End Sub

    'CREATE & OPEN MASTER CHECKLIST SPREADSHEET WITH ALL RELATED EVENTS (based on "CaseName")
    Private Sub btnCreateChecklist_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnCreateChecklist.Click
        '-- 2/14 cg --
        'PURPOSE: Master Checklist may be created by any admin, and additional admins can add a column for their region with same 'case name'
        'note: this button only visible if already checked to see that 1) file exists and 2) this region not there
        '   Dim bOpen As Boolean = False

VerifyDataReady:
        If Me.editMasterName.Text = String.Empty Then
            modGlobalVar.Msg("ATTENTION: missing information ", "Master Name is Required   ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        If Me.dtpFirstDate.Value.ToString = String.Empty Then
            modGlobalVar.Msg("ATTENTION: missing information ", "First Date is required   ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        'TODO QUESTION why was this commented out?
        If cboRegion.Text = String.Empty Then
            modGlobalVar.Msg("ATTENTION: missing information ", "select the Region where this event will be held", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        MouseWait()
        '  miSave.PerformClick()

VerifyDoesNotExist:  'could have been recently created and left open by someone else since this window opeend
        '  Dim strFileName As String = FindFile(LinkEventPath, Me.editMasterName.Text, ".xls", False) 'works without wildcard in extensinop 
        '   Select Case FindFile(LinkEventPath, Me.editMasterName.Text, ".xls*", False) 'works without wildcard in extension
        MstrChecklistFileExt = GetFileName(LinkEventPath, Me.editMasterName.Text & ".xls*", False)
        Select Case MstrChecklistFileExt
            Case Is = "not found" 'DOES NOT EXIST
                SetStatusBarText("Creating MasterChecklist")
                MstrChecklistFileExt = Me.editMasterName.Text & ".xlsm" ':IN btnCreateChecklist
                'CALL CREATE CHECKLIST
                If CreateChecklist() = True Then
                    SetCreateChecklistVisible(False)
                    InsertRegionDateRow()
                Else
                    SetCreateChecklistVisible(True)
                End If

            Case "error"
                MstrChecklistFileExt = ""
                Exit Sub 'TODO what should happen here?
            Case Else 'spreadsheet exists, check for region date
INSERT_REGION:
                'DOES REGION ALREADY EXIST
                Dim b As Boolean
                b = isRegionDateCol(Me.editMasterName.Text, Me.lblRegion.Text, Me.dtpFirstDate.Value.ToShortDateString)

                Select Case b 'IsRegionDateCol(LinkEventPath, MstrChecklistFilePath, Me.lblRegion.Text, Me.dtpFirstDate.Value.ToShortDateString) 'Replace(Me.dtpFirstDate.Text, " ", ""))
                    Case Is = True 'SOMEONE ADDED SINCE 'probably will never happen -- yes happens frequently with Matt/Eunita
                        SetStatusBarText("MasterChecklist OK")
                        SetCreateChecklistVisible(False)

                    Case Is = False
                        'CALL INSERT REGION
                        SetStatusBarText("Inserting This Event column")
                        If InsertRegionDate(MstrChecklistFileExt, Me.lblRegion.Text, Me.dtpFirstDate.Value.ToShortDateString) = True Then 'Replace(Me.dtpFirstDate.Text, " ", "")) = True Then
                            'RESET VISIBILITY
                            SetStatusBarText("checklist done")
                            SetCreateChecklistVisible(False)

                            InsertRegionDateRow()

                        Else 'insert column failed
                            SetStatusBarText("ERROR inserting this event column")
                            SetCreateChecklistVisible(True)
                        End If
                End Select
        End Select

        MouseDefault()
    End Sub

    'ALSO INSERT_INTO_luTABLE:        '2/16
    Private Sub InsertRegionDateRow()
        Dim cmd As New SqlClient.SqlCommand("INSERT INTO luEventMasterSpreadsheet (EventNum, Mastername, SatelliteRegion, SatelliteDate) VALUES (" & _
                                            ThisID & ", '" & Me.editMasterName.Text & "', '" & Me.lblRegion.Text & "', '" & Me.dtpFirstDate.Value.ToShortDateString & "')", sc)
        If Not SCConnect() Then
            Exit Sub
        End If
        Try
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            modGlobalVar.msg("ERROR: Failed to save the new region/date", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        Finally
            sc.Close()
            cmd = Nothing
        End Try

    End Sub

    'CREATE CHECKLIST SPREADSHEET FROM TEMPLATE
    Private Function CreateChecklist() As Boolean
        Dim objExcel As New Excel.Application
        Dim objTemplate As Excel.Workbook
        Dim objSheet As Excel.Worksheet

        objTemplate = objExcel.Workbooks.Open(SharedPath & "Staff Forms\EventMasterChecklist.xltm") 'SOPPath & "\documenttemplates\EventMasterChecklist.xlt") 'c:\myworkbook.xlsx")
        objSheet = objTemplate.Worksheets(1)

        Try
            objSheet.Unprotect("event")
        Catch ex As Exception
            modGlobalVar.Msg("ERROR: opening M Checklist template", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
            CreateChecklist = False
            GoTo closeall
        End Try
        Try
            objTemplate.SaveAs(LinkEventPath & MstrChecklistFileExt, FileFormat:=52) ':IN CreateChecklist    'xlsm = 52  'xls = 56  ".xlsx": FileFormatNum = 51
        Catch ex As Exception
            modGlobalVar.Msg("ERROR: save 1", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
            CreateChecklist = False
            GoTo closeall
        End Try

        'STRAIGHT ENTER ALL REGIONS WITH SAME MASTER CHECKLIST NAME
        Dim i As Integer = 3
        Dim sql As New SqlCommand("SELECT EventID, SatelliteRegion, dbo.fformatDate(FirstDate,'NoTime') as strDate FROM tblEventSetup WHERE MasterWorkshopName = '" & Me.editMasterName.Text & "' ORDER BY FirstDate", sc)
        Dim dr As SqlDataReader

        'GetData:
        Try
            SCConnect()
            dr = sql.ExecuteReader
            dr.Read()
            objSheet.Cells(1, 2).value = (Me.editMasterName.Text) 'UCase
            objSheet.Cells(2, i).value = dr.GetString(dr.GetOrdinal("SatelliteRegion")) & NextLine & CType(dr.GetString(dr.GetOrdinal("strDate")), Date)
            i = i + 1
            'INSERT REGION HEADINGS
            While dr.Read
                objSheet.Cells(1, i).value = "Include date and your intitials"
                objSheet.Cells(2, i).value = dr.GetString(dr.GetOrdinal("SatelliteRegion")) & NextLine & CType(dr.GetString(dr.GetOrdinal("strDate")), Date)
                i = i + 1
            End While
            '   CreateChecklist = True
        Catch ex As Exception
            modGlobalVar.Msg("ERROR creating spreadsheet", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
            CreateChecklist = False
        Finally
            dr.Close()
            sc.Close()
            dr = Nothing
            sql = Nothing
        End Try

        Try
            objSheet.Cells(3, 3).select()
            objSheet.Protect("event")
            objTemplate.Save() 'as (LinkedFilePath & "\Events\" & strName)
            CreateChecklist = True
        Catch ex As Exception
            modGlobalVar.Msg("ERROR save 2", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
            CreateChecklist = True
        End Try

CloseAll:

        Try
            objTemplate.Close(False)
            objExcel.Quit()
            ReleaseComObject(objSheet)
            ReleaseComObject(objTemplate)
            ReleaseComObject(objExcel)

        Catch ex As Exception
            '  modGlobalVar.Msg(ex.Message, , "error closing")
        Finally
            GC.Collect()
            GC.WaitForPendingFinalizers()
            GC.Collect()
            GC.WaitForPendingFinalizers()
        End Try


        MouseDefault()
    End Function

    'DETERMINE MASTER CHECKLIST BUTTON & LABEL VISIBILITY
    Public Sub SpreadsheetButtonVisible(ByVal What As String) ', ByRef btnCreate As Button, ByRef btnRefresh As Button, ByRef lblYes As Label, ByRef lblNo As Label)
        ''--2/15 cg ---
        'sets MstrChecklistFilePath with reload
        ' --2/16 easier Find wtih wildcard
        ' Dim FileNameExt As String
        SetStatusBarText("setting Create checklist button visibility")
        '   MsgBox("in check visibility")
        If What = "edit" Or MstrChecklistFileExt > "" Then 'no need to check if scolling through existing fields
            GoTo closeall
        End If

        'IF FILE EXISTS, THEN CHECK FOR THIS REGION & DATE
        ' MstrChecklistFilePath = FindFile(LinkEventPath, Me.editMasterName.Text, ".xls*", True) ':IN Reload Spreadsheet btn visible

        MstrChecklistFileExt = GetFileName(LinkEventPath, Me.editMasterName.Text & ".xls*", False)
        Select Case MstrChecklistFileExt ':IN Spreadsheet btn visible
            Case Is = "not found" 'spreadsheet does not exist
                SetCreateChecklistVisible(True)
                MstrChecklistFileExt = ""
                '  Me.btnCreateChecklist.Visible = True
                '  Me.btnRefreshChecklist.Visible = False
                '  Me.miMasterChecklist.Enabled = False
            Case Is = "error"
                MstrChecklistFileExt = ""
                SetStatusBarText("checklist: directory error")
                SetCreateChecklistVisible(True)
            Case Else 'check for this regionDate
                SetStatusBarText("searching for Region/Date...")
                Select Case modPopup.isRegionDateCol(Me.editMasterName.Text, Me.lblRegion.Text, Me.dtpFirstDate.Text) 'MstrChecklistFilePath, Me.lblRegion.Text, Replace(Me.dtpFirstDate.Text, " ", "")) ':IN Spreadsheet btn visible
                    Case Is = True         'AOK
                        SetCreateChecklistVisible(False)
                    Case Is = False 'This regionDate not in spreadsheet, or error
                        Me.btnCreateChecklist.Visible = True
                        Me.miMasterChecklist.Enabled = False
                        Me.btnRefreshChecklist.Visible = True
                        lblChecklist.Text = "The Master Checklist exists but not for this Region && Date."
                End Select

        End Select
CloseAll:
        SetStatusBarText("done checklist")

    End Sub

    'SET SPREADSHEET BUTTON VISIBILITY
    Private Sub SetCreateChecklistVisible(ByVal b As Boolean)
        'true = make Create button visible; false = master checklist already exists for thisevent
        'create button , open menu item,  refresh button, and label over spreadsheet view grid
        SetStatusBarText("setting visibility")
        Me.btnCreateChecklist.Visible = b
        Me.miMasterChecklist.Enabled = Not b
        Me.btnRefreshChecklist.Visible = Not b
        If b = True Then
            lblChecklist.Text = "The Master Checklist for this event has not been created yet."
        Else
            lblChecklist.Text = "(Read-only version of the MasterChecklist below.  Double-click grid to open/edit in Excel.)"
        End If
        SetStatusBarText("done")
    End Sub

    'CALL MASTER CHECKLIST BUTTON VISIBILITY
    Private Sub editMasterName_leave(ByVal sender As Object, ByVal e As System.EventArgs) _
    Handles editMasterName.Leave ', DateTimePicker1.Leave
        If isLoaded Then
            SpreadsheetButtonVisible("edit")  'in EditMasterName
        End If
    End Sub

    'SPELL OUT DATE and CALL MASTER CHECKLIST BUTTON VISIBILITY
    Private Sub dtpFirstDate_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) _
        Handles dtpFirstDate.ValueChanged
        If isLoaded Then
            bChanged = True
            Me.editDates.Text = Me.dtpFirstDate.Value.ToLongDateString
            SpreadsheetButtonVisible("edit") 'in date change
        End If
    End Sub

#End Region 'Spreadsheet: MasterChecklist

#Region "HostOverview"    'spreadsheet: HostCongr
    'NOTE: link by orgID?  use orgID if is one of 'ours', else if name changes link will break...use location name for others not in our db

    'HIDE or SHOW CREATE BUTTON/MI
    Private Sub RefreshHostVisibility()
        'from reLoad and Location change
        Dim strShortName As String = GetShortName()
        If strShortName = "none" Then
            Me.miHost.Enabled = False
            Me.btnHostOverview.Enabled = False
        Else
            SetMenuItem("Host Overview", Me.miHost, Me.btnHostOverview, LinkOrgPath & "Host " & strShortName & ".xlsm")
        End If
        strShortName = Nothing
    End Sub

    ' CALLS CREATE and/or OPEN LOCATION OVERVIEW FROM EXCEL
    Private Sub btnHostOverview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnHostOverview.Click, miHost.Click
        '--  8/2013 --
        '--called by org and event detail; spreadsheet regarding parking, room size etc.
        '-- optimized
        Dim strShortName As String
        MouseWait()
        miSave.PerformClick()
        strShortName = GetShortName() 'returns "none", orgid, org abbrev location name
        If strShortName = "none" Then
            GoTo cleanup
        End If
        SetStatusBarText("Creating Host Overview")
        Select Case sender.text
            Case Is = "Host Overview - Create"
                GoTo CreateOverview
            Case Is = "Host Overview - Open", "Open Overview"
                GoTo OpenOverview
            Case Else
                modGlobalVar.Msg("Cancelling Request", "item not found: " & NextLine & sender.text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                GoTo Cleanup
        End Select

CreateOverview:
        If CreateSpreadsheet(SharedPath & "Staff Forms\EventHostTemplate.xltm", LinkOrgPath & "Host " & strShortName & ".xlsm", Me.EditLocation.Text, Me.editLocationPhone.Text) = True Then
            Me.btnHostOverview.Text = "Open Overview"
            Me.miHost.Text = "Host Overview - Open"
            Me.StatusBarPanel1.Text = "Host Overview created"

        End If

OpenOverview:  'note file name depends on whether or not is orgNum; could be by title unless policy becomes to enter other venues as orgs
        'works but replaced:        OpenSpreadsheet("Organizations", GetShortName) ', Me.DateTimePicker1.Text)
        '  If OpenFile(modPopup.FindFile(LinkOrgPath, "Host " & strShortName, ".xls*", True)) Then
        If OpenFile(modPopup.GetFileName(LinkOrgPath, "Host " & strShortName & ".xls*", True)) Then
            Me.StatusBarPanel1.Text = "Host Overview opened"
        Else
            SetStatusBarText("network error")
        End If
Cleanup:
        Try
            strShortName = Nothing
        Catch ex As Exception
        End Try
        SetStatusBarText("done Host Overview")
        MouseDefault()
    End Sub

    'get OrgId or if none, use abbreviated Location name
    Private Function GetShortName() As String
        '-- 8/13 cg --
        Dim i As Integer

        If Me.EditLocation.Text = String.Empty Then
            Return "none"
        End If

        If Me.lblOrgNum.Text = String.Empty Then    'no db org chose, use location name instead of id
            GetShortName = Replace(Replace(Replace(Me.EditLocation.Text, ".", ""), ",", ""), " ", "")
            i = Len(GetShortName)
            If i > 10 Then
                Return GetShortName.Substring(0, 10)
            Else
                Return Me.EditLocation.Text
            End If
        Else
            Return Me.lblOrgNum.Text
        End If

    End Function

#End Region 'Spreadsheet Host Congr

End Class

'