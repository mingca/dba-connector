Option Explicit On 

Imports System.Data.SqlClient
Imports System.text    'stringbuilder
Imports System.IO
Imports Excel = Microsoft.Office.Interop.Excel
Imports System.Linq


Public Class frmSrchEvent
    Inherits System.Windows.Forms.Form

    'datagrid variables...
    Dim hti As System.Windows.Forms.DataGrid.HitTestInfo   'datagrid filter
    Dim dvM, dvS1, dvM2 As DataView   'filter for each datagrid
    Dim statusM, statusS1, statusS2 As String 'filter text for status bar
    Dim strDGM, strDGS1, strDGS2 As String  'header text on datagrids
    Dim dv As DataView 'for filtering search results

    Dim isLoaded As Boolean = False
    Dim iValidReg As Object 'now includes text, registered and cancelled 'Integer
    Dim iColCase, iColFirstDt As Int16
    Dim iCurrRow As Integer = -1 'temp updating via grid
    Dim icolEventSKU, icolEventNID, icolRegistered, iColOrderNum, iColPaymentNum As Integer
    Dim bindingClick As New BindingSource
    Dim bindingRadioBtn As New BindingSource

    Dim b1stOrder As Boolean = True 'click invisible button
    Dim b1stLocation As Boolean = True
    Dim bFirstTime As Boolean = True
    Dim ThisID As Integer = 0
    Dim source1 As New BindingSource()
    Dim SrchTotal As Integer 'keep count for display
    Dim iGrid As Integer    'count rows of [filtered] grid

    Dim frmAdd As New frmMainWEvent    'leave here or won't reload if minimized
    Dim strForm, strKey, strOpenKey As String
    Dim WhatType As String = "All Types" 'to get which radio button checked
    Dim WhatField As String = "Upcoming"

    Dim tblOrder As New DataTable
    Dim tblPay As New DataTable
    Dim tblReg As New DataTable
    Dim rO, rP, rR As DataRow
    Dim dvPay, dvReg, dvPending As DataView
    Dim frmOpen As frmMainWEvent 'mainEdEvent - reference to existing form to allow bringtofront

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
    Friend WithEvents miRecentRegs As System.Windows.Forms.MenuItem
    Friend WithEvents rbLiveOnline As System.Windows.Forms.RadioButton
    Friend WithEvents colRegID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn35 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colRegLFName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn38 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colRegEventName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn36 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Fee As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewCheckBoxColumn3 As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents OnlineSID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colPaymentID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn29 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn30 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn31 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn33 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn34 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents NumRegs As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colPendingSID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colPendingOID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn46 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn43 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn44 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn45 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewCheckBoxColumn4 As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents SID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chkWild As System.Windows.Forms.CheckBox
    Friend WithEvents DataGridTextBoxColumn15 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn16 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn17 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn18 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn19 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents miReportRegistration As System.Windows.Forms.MenuItem
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents btnReport As System.Windows.Forms.Button
    Friend WithEvents MenuItem3 As System.Windows.Forms.MenuItem
    Friend WithEvents miHelp As System.Windows.Forms.MenuItem
    Friend WithEvents miDef As System.Windows.Forms.MenuItem
    Friend WithEvents miViewAll As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem4 As System.Windows.Forms.MenuItem
    Friend WithEvents miEmailAttend As System.Windows.Forms.MenuItem
    Friend WithEvents miEmailRegister As System.Windows.Forms.MenuItem
    Friend WithEvents DataGridTextBoxColumn21 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents miEmailDatafile As System.Windows.Forms.MenuItem
    Protected Friend WithEvents fldEventNID As System.Windows.Forms.TextBox
    Protected Friend WithEvents Label2 As System.Windows.Forms.Label
    Protected WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents miFirstEmail As System.Windows.Forms.MenuItem
    Friend WithEvents cboRetired As InfoCtr.ComboBoxRelaxed
    Protected WithEvents Label4 As System.Windows.Forms.Label
    Protected WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents rbEvaluation As System.Windows.Forms.RadioButton
    Friend WithEvents rbRenewal As System.Windows.Forms.RadioButton
    Friend WithEvents rbAllTypes As System.Windows.Forms.RadioButton
    Friend WithEvents cboDates As InfoCtr.ComboBoxRelaxed
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents miChecklist As System.Windows.Forms.MenuItem
    Friend WithEvents miCancellationRpt As System.Windows.Forms.MenuItem
    'Friend WithEvents taGetWorkshopRegistrants As InfoCtr.dsGetEventRegList2TableAdapters.taGetWRegistrantList
    '  Protected Friend WithEvents daSrchWEvent As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents SqlCommand1 As System.Data.SqlClient.SqlCommand
    'Friend WithEvents dsGetEventRegList21 As InfoCtr.dsGetEventRegList2
    Friend WithEvents DsSrchWEvent1 As InfoCtr.dsSrchWEvent
    ' Friend WithEvents dsGetEventRegList2 As InfoCtr.dsGetEventRegList2
    Friend WithEvents getEventRegistrantList2TableAdapter As InfoCtr.dsGetEventRegList2TableAdapters.getEventRegistrantList2TableAdapter
    ' Protected WithEvents grdMain As System.Windows.Forms.DataGrid
    Protected Friend WithEvents styleSrchWEvents As System.Windows.Forms.DataGridTableStyle
    Friend WithEvents DataGridTextBoxColumn23 As System.Windows.Forms.DataGridTextBoxColumn
    Protected Friend WithEvents DataGridTextBoxColumn6 As System.Windows.Forms.DataGridTextBoxColumn
    Protected Friend WithEvents DataGridTextBoxColumn7 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn20 As System.Windows.Forms.DataGridTextBoxColumn
    Protected Friend WithEvents DataGridTextBoxColumn5 As System.Windows.Forms.DataGridTextBoxColumn
    Protected Friend WithEvents colReg As System.Windows.Forms.DataGridTextBoxColumn
    Protected Friend WithEvents colFDate As System.Windows.Forms.DataGridTextBoxColumn
    Protected Friend WithEvents colPresenter As System.Windows.Forms.DataGridTextBoxColumn
    Protected Friend WithEvents colWName As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn25 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn24 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn11 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents miGotoEvent As System.Windows.Forms.MenuItem
    Friend WithEvents colMasterName As System.Windows.Forms.DataGridTextBoxColumn
    Protected Friend WithEvents fldRegID As System.Windows.Forms.TextBox
    Protected Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents DataGridTextBoxColumn3 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridBoolColumn1 As System.Windows.Forms.DataGridBoolColumn
    Protected Friend WithEvents fldEventSKU As System.Windows.Forms.TextBox
    Protected Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents DataGridTextBoxColumn4 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents miGotoPending As System.Windows.Forms.MenuItem
    Friend WithEvents colPending As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents colEventSKU As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents colNIDEvent As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents pnlRegion As System.Windows.Forms.Panel
    Friend WithEvents miFinancial As System.Windows.Forms.MenuItem
    Friend WithEvents miGotoOrder As System.Windows.Forms.MenuItem
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents pgEvent As System.Windows.Forms.TabPage
    Friend WithEvents pgOrder As System.Windows.Forms.TabPage
    Friend WithEvents SplitContainer As System.Windows.Forms.SplitContainer
    Friend WithEvents treevOrders As System.Windows.Forms.TreeView
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents tOrderLstTableAdapter As InfoCtr.dsEventRegOrderTVTableAdapters.tOrderLstTableAdapter
    Friend WithEvents TOrderLstPendingTableAdapter As InfoCtr.dsEventRegOrderTVTableAdapters.tOrderLstPendingsTableAdapter
    Friend WithEvents DsEventRegOrderTV2 As InfoCtr.dsEventRegOrderTV
    Friend WithEvents TOrderLstRegistrationsTableAdapter As InfoCtr.dsEventRegOrderTVTableAdapters.tOrderLstRegistrationsTableAdapter
    Friend WithEvents TOrderLstPaymentsTableAdapter As InfoCtr.dsEventRegOrderTVTableAdapters.tOrderLstPaymentsTableAdapter
    Friend WithEvents lblMax As System.Windows.Forms.Label
    Friend WithEvents btnSrchOrder As System.Windows.Forms.Button
    Friend WithEvents txtSrchOrder As System.Windows.Forms.TextBox
    Friend WithEvents OIDDataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SIDDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CompanyDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents MatchFirstDateDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents RegistrarDataGridViewCheckBoxColumn1 As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents LFNameDataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents OrderNumDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents EventDtDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents RegNumDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents RegistrarDataGridViewCheckBoxColumn As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents OrgNameDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents LFNameDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    '  Friend WithEvents SSISDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents OIDDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents AmountPdDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PaymentMethodDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DtPaymentDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PaymentIDDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PayerNameDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PaymentTypeDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PaymentID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents OID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PaymentType As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PaymentMethod As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents AmountPd As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DtPayment As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PayerName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgvPending2 As System.Windows.Forms.DataGridView
    Friend WithEvents dgvReg2 As System.Windows.Forms.DataGridView
    Friend WithEvents dgvFinance2 As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridViewTextBoxColumn5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn7 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn6 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn25 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn21 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn27 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn23 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn22 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn26 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn24 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents DsGetEventRegList2 As InfoCtr.dsGetEventRegList2
    Friend WithEvents DsGetEventRegList2BindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents colPd As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn2 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents miGotoPayment As System.Windows.Forms.MenuItem
    Friend WithEvents colRegistrationID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn8 As System.Windows.Forms.DataGridViewTextBoxColumn
    ' Friend WithEvents colRegLFName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn11 As System.Windows.Forms.DataGridViewTextBoxColumn
    '  Friend WithEvents colRegEventName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn9 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewCheckBoxColumn1 As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn20 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn17 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn18 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn19 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewCheckBoxColumn2 As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn10 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn28 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn12 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn13 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn14 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn16 As System.Windows.Forms.DataGridViewTextBoxColumn
    '  Friend WithEvents colPndgSID As System.Windows.Forms.DataGridViewTextBoxColumn
    '  Friend WithEvents colPndgOID As System.Windows.Forms.DataGridViewTextBoxColumn
    ' Friend WithEvents colPamentID As System.Windows.Forms.DataGridViewTextBoxColumn
    Protected WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents txtdtOrder As System.Windows.Forms.TextBox
    Friend WithEvents cboOrderStatus As InfoCtr.ComboBoxRelaxed
    Friend WithEvents txtOrderNotes As System.Windows.Forms.TextBox
    Friend WithEvents AmountOrderTotalLabel As System.Windows.Forms.Label
    Friend WithEvents txtAmountOrderTotal As System.Windows.Forms.TextBox
    Friend WithEvents cboProposedPayment As InfoCtr.ComboBoxRelaxed
    Friend WithEvents btnAddReg As System.Windows.Forms.Button
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents MainWOrder2BindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents dsMainWOrder1 As InfoCtr.dsMainWOrder
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents btNewReg As System.Windows.Forms.Button
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents btnRegion As System.Windows.Forms.Button
    Friend WithEvents DataGridTextBoxColumn1 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents grdvwMain As System.Windows.Forms.DataGridView
    Friend WithEvents lblMainGrid As System.Windows.Forms.Label
    ' Friend WithEvents WorkshopNameDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn37 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents StatusBarPanelID As System.Windows.Forms.StatusBarPanel
    Friend WithEvents EventIDDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents FirstDateDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SatelliteRegionDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents EventNameDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents InstructorDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TypeofEventDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents RegisteredDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents MaximumSeatingDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TotRegisteredDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents VegDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents MasterWorkshopNameDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PendingDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents NIDEventDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents WorkshopNumDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents EventSKUDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents RegonlineNumDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents miEmailNotAttend As System.Windows.Forms.MenuItem
    ' Protected Friend WithEvents miGotoEvent As System.Windows.Forms.MenuItem

    Protected Friend WithEvents StatusBar1 As System.Windows.Forms.StatusBar
    Protected Friend WithEvents DataGridTableStyle2 As System.Windows.Forms.DataGridTableStyle
    Protected Friend WithEvents colSecLastname As System.Windows.Forms.DataGridTextBoxColumn
    Protected Friend WithEvents colSecFirstname As System.Windows.Forms.DataGridTextBoxColumn
    Protected Friend WithEvents DataGridTextBoxColumn10 As System.Windows.Forms.DataGridTextBoxColumn
    Protected Friend WithEvents colSecOrg As System.Windows.Forms.DataGridTextBoxColumn
    Protected Friend WithEvents fldEventID As System.Windows.Forms.TextBox
    Protected Friend WithEvents Label1 As System.Windows.Forms.Label
    Protected Friend WithEvents DataGridTextBoxColumn14 As System.Windows.Forms.DataGridTextBoxColumn
    Protected Friend WithEvents btnNew As System.Windows.Forms.Button
    Protected Friend WithEvents miGotoRegistration As System.Windows.Forms.MenuItem 'for title/presenter/year search
    Protected Friend WithEvents MenuItem1 As System.Windows.Forms.MenuItem
    Protected Friend WithEvents MenuItem2 As System.Windows.Forms.MenuItem
    Protected Friend WithEvents mmReport As System.Windows.Forms.MenuItem
    Protected Friend WithEvents miClear As System.Windows.Forms.MenuItem
    Protected Friend WithEvents miSearch As System.Windows.Forms.MenuItem
    ' Protected Friend WithEvents dsGetReg As InfoCtr.dsGetReg
    Protected Friend WithEvents miAddEvent As System.Windows.Forms.MenuItem
    Protected Friend WithEvents miCloseForm As System.Windows.Forms.MenuItem
    Protected Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Protected Friend WithEvents mmiGoto As System.Windows.Forms.MenuItem
    Protected Friend WithEvents mmiSearch As System.Windows.Forms.MenuItem
    Protected WithEvents cboRegion As InfoCtr.ComboBoxRelaxed
    Protected WithEvents cboSpecific As InfoCtr.ComboBoxRelaxed
    Protected WithEvents txtSearch As System.Windows.Forms.TextBox
    Protected WithEvents MainMenu1 As System.Windows.Forms.MainMenu
    Friend WithEvents cmd1 As System.Data.SqlClient.SqlCommand
    '   Protected Friend WithEvents daspSrchEdEvents As System.Data.SqlClient.SqlDataAdapter
    ' Friend WithEvents dsSrchEdEvents1 As InfoCtr.dsSrchEdEvents
    'Friend WithEvents taGetWorkshopRegistrant As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents miPrintList As System.Windows.Forms.MenuItem
    Friend WithEvents chkDetail As System.Windows.Forms.CheckBox
    Protected WithEvents grdSecond1 As System.Windows.Forms.DataGrid
    Friend WithEvents HelpProvider1 As System.Windows.Forms.HelpProvider
    Friend WithEvents StatusBarPanel1 As System.Windows.Forms.StatusBarPanel
    Friend WithEvents StatusBarPanel2 As System.Windows.Forms.StatusBarPanel
    Friend WithEvents grpType As System.Windows.Forms.GroupBox
    Friend WithEvents rbWorkshop As System.Windows.Forms.RadioButton
    Friend WithEvents rbLongterm As System.Windows.Forms.RadioButton
    Friend WithEvents rbGrant As System.Windows.Forms.RadioButton
    Friend WithEvents rbConference As System.Windows.Forms.RadioButton
    Friend WithEvents rbSponsored As System.Windows.Forms.RadioButton
    Friend WithEvents rbHosted As System.Windows.Forms.RadioButton
    Protected WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents btnHelp As System.Windows.Forms.Button

    <System.Diagnostics.DebuggerStepThrough()> Protected Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim DtOrderLabel As System.Windows.Forms.Label
        Dim OrderStatusLabel As System.Windows.Forms.Label
        Dim OrderNotesLabel As System.Windows.Forms.Label
        Dim ProposedPaymentMethodLabel As System.Windows.Forms.Label
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSrchEvent))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle11 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle12 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle13 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.StatusBar1 = New System.Windows.Forms.StatusBar()
        Me.StatusBarPanel1 = New System.Windows.Forms.StatusBarPanel()
        Me.StatusBarPanelID = New System.Windows.Forms.StatusBarPanel()
        Me.StatusBarPanel2 = New System.Windows.Forms.StatusBarPanel()
        Me.MainMenu1 = New System.Windows.Forms.MainMenu(Me.components)
        Me.MenuItem1 = New System.Windows.Forms.MenuItem()
        Me.MenuItem2 = New System.Windows.Forms.MenuItem()
        Me.miAddEvent = New System.Windows.Forms.MenuItem()
        Me.miCloseForm = New System.Windows.Forms.MenuItem()
        Me.mmiSearch = New System.Windows.Forms.MenuItem()
        Me.miSearch = New System.Windows.Forms.MenuItem()
        Me.miClear = New System.Windows.Forms.MenuItem()
        Me.mmiGoto = New System.Windows.Forms.MenuItem()
        Me.miGotoEvent = New System.Windows.Forms.MenuItem()
        Me.miGotoOrder = New System.Windows.Forms.MenuItem()
        Me.miGotoPayment = New System.Windows.Forms.MenuItem()
        Me.miGotoRegistration = New System.Windows.Forms.MenuItem()
        Me.miViewAll = New System.Windows.Forms.MenuItem()
        Me.miGotoPending = New System.Windows.Forms.MenuItem()
        Me.mmReport = New System.Windows.Forms.MenuItem()
        Me.miPrintList = New System.Windows.Forms.MenuItem()
        Me.miReportRegistration = New System.Windows.Forms.MenuItem()
        Me.miChecklist = New System.Windows.Forms.MenuItem()
        Me.miCancellationRpt = New System.Windows.Forms.MenuItem()
        Me.miEmailDatafile = New System.Windows.Forms.MenuItem()
        Me.miFinancial = New System.Windows.Forms.MenuItem()
        Me.miRecentRegs = New System.Windows.Forms.MenuItem()
        Me.MenuItem4 = New System.Windows.Forms.MenuItem()
        Me.miEmailAttend = New System.Windows.Forms.MenuItem()
        Me.miEmailRegister = New System.Windows.Forms.MenuItem()
        Me.miEmailNotAttend = New System.Windows.Forms.MenuItem()
        Me.miFirstEmail = New System.Windows.Forms.MenuItem()
        Me.MenuItem3 = New System.Windows.Forms.MenuItem()
        Me.miHelp = New System.Windows.Forms.MenuItem()
        Me.miDef = New System.Windows.Forms.MenuItem()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.btnReport = New System.Windows.Forms.Button()
        Me.btnNew = New System.Windows.Forms.Button()
        Me.btnHelp = New System.Windows.Forms.Button()
        Me.btnAddReg = New System.Windows.Forms.Button()
        Me.btnRegion = New System.Windows.Forms.Button()
        Me.cmd1 = New System.Data.SqlClient.SqlCommand()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.pnlRegion = New System.Windows.Forms.Panel()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.chkWild = New System.Windows.Forms.CheckBox()
        Me.grpType = New System.Windows.Forms.GroupBox()
        Me.rbLiveOnline = New System.Windows.Forms.RadioButton()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.rbAllTypes = New System.Windows.Forms.RadioButton()
        Me.rbRenewal = New System.Windows.Forms.RadioButton()
        Me.rbEvaluation = New System.Windows.Forms.RadioButton()
        Me.rbGrant = New System.Windows.Forms.RadioButton()
        Me.rbWorkshop = New System.Windows.Forms.RadioButton()
        Me.rbLongterm = New System.Windows.Forms.RadioButton()
        Me.rbConference = New System.Windows.Forms.RadioButton()
        Me.rbSponsored = New System.Windows.Forms.RadioButton()
        Me.rbHosted = New System.Windows.Forms.RadioButton()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.chkDetail = New System.Windows.Forms.CheckBox()
        Me.HelpProvider1 = New System.Windows.Forms.HelpProvider()
        Me.DsSrchWEvent1 = New InfoCtr.dsSrchWEvent()
        Me.styleSrchWEvents = New System.Windows.Forms.DataGridTableStyle()
        Me.DataGridTextBoxColumn11 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.colFDate = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.colReg = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.colWName = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.colPresenter = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn5 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn20 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn7 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn6 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn23 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.colMasterName = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.colPending = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.colEventSKU = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.colNIDEvent = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.fldEventID = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.fldEventNID = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.lblMainGrid = New System.Windows.Forms.Label()
        Me.grdvwMain = New System.Windows.Forms.DataGridView()
        Me.EventIDDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.FirstDateDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SatelliteRegionDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.EventNameDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.InstructorDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TypeofEventDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.RegisteredDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MaximumSeatingDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TotRegisteredDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.VegDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MasterWorkshopNameDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PendingDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.NIDEventDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.WorkshopNumDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.EventSKUDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.RegonlineNumDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.grdSecond1 = New System.Windows.Forms.DataGrid()
        Me.DsGetEventRegList2 = New InfoCtr.dsGetEventRegList2()
        Me.DataGridTableStyle2 = New System.Windows.Forms.DataGridTableStyle()
        Me.DataGridTextBoxColumn14 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn10 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.colSecLastname = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.colSecFirstname = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.colSecOrg = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn21 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn3 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.colPd = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridBoolColumn1 = New System.Windows.Forms.DataGridBoolColumn()
        Me.DataGridTextBoxColumn18 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn15 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn16 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn17 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn19 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn4 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn2 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn1 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.SqlCommand1 = New System.Data.SqlClient.SqlCommand()
        Me.DataGridTextBoxColumn25 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn24 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.fldRegID = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.fldEventSKU = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.pgEvent = New System.Windows.Forms.TabPage()
        Me.pgOrder = New System.Windows.Forms.TabPage()
        Me.SplitContainer = New System.Windows.Forms.SplitContainer()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.txtdtOrder = New System.Windows.Forms.TextBox()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.txtOrderNotes = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.AmountOrderTotalLabel = New System.Windows.Forms.Label()
        Me.txtAmountOrderTotal = New System.Windows.Forms.TextBox()
        Me.lblMax = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.btnSrchOrder = New System.Windows.Forms.Button()
        Me.txtSrchOrder = New System.Windows.Forms.TextBox()
        Me.treevOrders = New System.Windows.Forms.TreeView()
        Me.dgvPending2 = New System.Windows.Forms.DataGridView()
        Me.colPendingSID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colPendingOID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn46 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn43 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn44 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn45 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewCheckBoxColumn4 = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.SID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DsEventRegOrderTV2 = New InfoCtr.dsEventRegOrderTV()
        Me.dgvReg2 = New System.Windows.Forms.DataGridView()
        Me.colRegID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn35 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colRegLFName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn38 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colRegEventName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn36 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Fee = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewCheckBoxColumn3 = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.OnlineSID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgvFinance2 = New System.Windows.Forms.DataGridView()
        Me.colPaymentID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn29 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn30 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn31 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn33 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn34 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.NumRegs = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.PaymentID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.OID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PaymentType = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PaymentMethod = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.AmountPd = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DtPayment = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PayerName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.btNewReg = New System.Windows.Forms.Button()
        Me.getEventRegistrantList2TableAdapter = New InfoCtr.dsGetEventRegList2TableAdapters.getEventRegistrantList2TableAdapter()
        Me.tOrderLstTableAdapter = New InfoCtr.dsEventRegOrderTVTableAdapters.tOrderLstTableAdapter()
        Me.TOrderLstPendingTableAdapter = New InfoCtr.dsEventRegOrderTVTableAdapters.tOrderLstPendingsTableAdapter()
        Me.TOrderLstRegistrationsTableAdapter = New InfoCtr.dsEventRegOrderTVTableAdapters.tOrderLstRegistrationsTableAdapter()
        Me.TOrderLstPaymentsTableAdapter = New InfoCtr.dsEventRegOrderTVTableAdapters.tOrderLstPaymentsTableAdapter()
        Me.cboDates = New InfoCtr.ComboBoxRelaxed()
        Me.cboRegion = New InfoCtr.ComboBoxRelaxed()
        Me.cboSpecific = New InfoCtr.ComboBoxRelaxed()
        Me.cboRetired = New InfoCtr.ComboBoxRelaxed()
        Me.cboProposedPayment = New InfoCtr.ComboBoxRelaxed()
        Me.cboOrderStatus = New InfoCtr.ComboBoxRelaxed()
        DtOrderLabel = New System.Windows.Forms.Label()
        OrderStatusLabel = New System.Windows.Forms.Label()
        OrderNotesLabel = New System.Windows.Forms.Label()
        ProposedPaymentMethodLabel = New System.Windows.Forms.Label()
        CType(Me.StatusBarPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.StatusBarPanelID, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.StatusBarPanel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        Me.pnlRegion.SuspendLayout()
        Me.grpType.SuspendLayout()
        CType(Me.DsSrchWEvent1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel4.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.grdvwMain, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdSecond1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsGetEventRegList2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabControl1.SuspendLayout()
        Me.pgEvent.SuspendLayout()
        Me.pgOrder.SuspendLayout()
        CType(Me.SplitContainer, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer.Panel1.SuspendLayout()
        Me.SplitContainer.Panel2.SuspendLayout()
        Me.SplitContainer.SuspendLayout()
        CType(Me.dgvPending2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsEventRegOrderTV2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvReg2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvFinance2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DtOrderLabel
        '
        DtOrderLabel.AutoSize = True
        DtOrderLabel.Location = New System.Drawing.Point(55, 428)
        DtOrderLabel.Name = "DtOrderLabel"
        DtOrderLabel.Size = New System.Drawing.Size(98, 13)
        DtOrderLabel.TabIndex = 34
        DtOrderLabel.Text = "Date Order Placed:"
        DtOrderLabel.Visible = False
        '
        'OrderStatusLabel
        '
        OrderStatusLabel.AutoSize = True
        OrderStatusLabel.Location = New System.Drawing.Point(80, 485)
        OrderStatusLabel.Name = "OrderStatusLabel"
        OrderStatusLabel.Size = New System.Drawing.Size(69, 13)
        OrderStatusLabel.TabIndex = 35
        OrderStatusLabel.Text = "Order Status:"
        OrderStatusLabel.Visible = False
        '
        'OrderNotesLabel
        '
        OrderNotesLabel.AutoSize = True
        OrderNotesLabel.Location = New System.Drawing.Point(4, 512)
        OrderNotesLabel.Name = "OrderNotesLabel"
        OrderNotesLabel.Size = New System.Drawing.Size(62, 13)
        OrderNotesLabel.TabIndex = 36
        OrderNotesLabel.Text = "Order Note:"
        OrderNotesLabel.Visible = False
        '
        'ProposedPaymentMethodLabel
        '
        ProposedPaymentMethodLabel.AutoSize = True
        ProposedPaymentMethodLabel.Location = New System.Drawing.Point(15, 563)
        ProposedPaymentMethodLabel.Name = "ProposedPaymentMethodLabel"
        ProposedPaymentMethodLabel.Size = New System.Drawing.Size(138, 13)
        ProposedPaymentMethodLabel.TabIndex = 42
        ProposedPaymentMethodLabel.Text = "Proposed Payment Method:"
        ProposedPaymentMethodLabel.Visible = False
        '
        'StatusBar1
        '
        Me.StatusBar1.Location = New System.Drawing.Point(0, 559)
        Me.StatusBar1.Name = "StatusBar1"
        Me.StatusBar1.Panels.AddRange(New System.Windows.Forms.StatusBarPanel() {Me.StatusBarPanel1, Me.StatusBarPanelID, Me.StatusBarPanel2})
        Me.StatusBar1.ShowPanels = True
        Me.StatusBar1.Size = New System.Drawing.Size(1094, 21)
        Me.StatusBar1.TabIndex = 8
        Me.StatusBar1.Text = "Use the form to search for Events.  Detail section lists Registrants."
        '
        'StatusBarPanel1
        '
        Me.StatusBarPanel1.MinWidth = 200
        Me.StatusBarPanel1.Name = "StatusBarPanel1"
        Me.StatusBarPanel1.Width = 200
        '
        'StatusBarPanelID
        '
        Me.StatusBarPanelID.Alignment = System.Windows.Forms.HorizontalAlignment.Center
        Me.StatusBarPanelID.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents
        Me.StatusBarPanelID.BorderStyle = System.Windows.Forms.StatusBarPanelBorderStyle.Raised
        Me.StatusBarPanelID.MinWidth = 200
        Me.StatusBarPanelID.Name = "StatusBarPanelID"
        Me.StatusBarPanelID.Text = "Event ID"
        Me.StatusBarPanelID.Width = 200
        '
        'StatusBarPanel2
        '
        Me.StatusBarPanel2.Alignment = System.Windows.Forms.HorizontalAlignment.Right
        Me.StatusBarPanel2.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring
        Me.StatusBarPanel2.Name = "StatusBarPanel2"
        Me.StatusBarPanel2.Text = "Use this window to search for Events.  Click ShowRegistrations to show or hide li" & _
    "st of registrants in lower grid."
        Me.StatusBarPanel2.Width = 677
        '
        'MainMenu1
        '
        Me.MainMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem1, Me.mmiSearch, Me.mmiGoto, Me.mmReport, Me.MenuItem4, Me.MenuItem3})
        '
        'MenuItem1
        '
        Me.MenuItem1.Index = 0
        Me.MenuItem1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem2, Me.miCloseForm})
        Me.MenuItem1.Text = "File"
        '
        'MenuItem2
        '
        Me.MenuItem2.Index = 0
        Me.MenuItem2.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.miAddEvent})
        Me.MenuItem2.Text = "New"
        '
        'miAddEvent
        '
        Me.miAddEvent.Index = 0
        Me.miAddEvent.Text = "Event"
        '
        'miCloseForm
        '
        Me.miCloseForm.Index = 1
        Me.miCloseForm.Text = "Close Window"
        '
        'mmiSearch
        '
        Me.mmiSearch.Index = 1
        Me.mmiSearch.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.miSearch, Me.miClear})
        Me.mmiSearch.Text = "Search"
        '
        'miSearch
        '
        Me.miSearch.Index = 0
        Me.miSearch.Text = "Begin Search"
        '
        'miClear
        '
        Me.miClear.Index = 1
        Me.miClear.Text = "Clear Criteria"
        '
        'mmiGoto
        '
        Me.mmiGoto.Index = 2
        Me.mmiGoto.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.miGotoEvent, Me.miGotoOrder, Me.miGotoPayment, Me.miGotoRegistration, Me.miViewAll, Me.miGotoPending})
        Me.mmiGoto.Text = "Go to"
        '
        'miGotoEvent
        '
        Me.miGotoEvent.Index = 0
        Me.miGotoEvent.Text = "Event Detail"
        '
        'miGotoOrder
        '
        Me.miGotoOrder.Enabled = False
        Me.miGotoOrder.Index = 1
        Me.miGotoOrder.Text = "Order Detail"
        '
        'miGotoPayment
        '
        Me.miGotoPayment.Enabled = False
        Me.miGotoPayment.Index = 2
        Me.miGotoPayment.Text = "Payment Detail"
        '
        'miGotoRegistration
        '
        Me.miGotoRegistration.Index = 3
        Me.miGotoRegistration.Text = "This Registration Detail"
        '
        'miViewAll
        '
        Me.miViewAll.Index = 4
        Me.miViewAll.Text = "All Registrations at this Event"
        '
        'miGotoPending
        '
        Me.miGotoPending.Index = 5
        Me.miGotoPending.Text = "Pending Registrations"
        '
        'mmReport
        '
        Me.mmReport.Index = 3
        Me.mmReport.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.miPrintList, Me.miReportRegistration, Me.miChecklist, Me.miCancellationRpt, Me.miEmailDatafile, Me.miFinancial, Me.miRecentRegs})
        Me.mmReport.Text = "Reports"
        '
        'miPrintList
        '
        Me.miPrintList.Enabled = False
        Me.miPrintList.Index = 0
        Me.miPrintList.Text = "Print This List"
        '
        'miReportRegistration
        '
        Me.miReportRegistration.Index = 1
        Me.miReportRegistration.Text = "Registration Reports"
        '
        'miChecklist
        '
        Me.miChecklist.Enabled = False
        Me.miChecklist.Index = 2
        Me.miChecklist.Text = "Open Master Checklist"
        '
        'miCancellationRpt
        '
        Me.miCancellationRpt.Index = 3
        Me.miCancellationRpt.Text = "Open Cancellation Phone List"
        '
        'miEmailDatafile
        '
        Me.miEmailDatafile.Enabled = False
        Me.miEmailDatafile.Index = 4
        Me.miEmailDatafile.Tag = "OpenDataFile"
        Me.miEmailDatafile.Text = "Open Spreadsheet Datafile"
        '
        'miFinancial
        '
        Me.miFinancial.Enabled = False
        Me.miFinancial.Index = 5
        Me.miFinancial.Text = "Financial Reports"
        '
        'miRecentRegs
        '
        Me.miRecentRegs.Index = 6
        Me.miRecentRegs.Text = "Recent Downloaded Registrations"
        '
        'MenuItem4
        '
        Me.MenuItem4.Index = 4
        Me.MenuItem4.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.miEmailAttend, Me.miEmailRegister, Me.miEmailNotAttend, Me.miFirstEmail})
        Me.MenuItem4.Text = "Send Email"
        '
        'miEmailAttend
        '
        Me.miEmailAttend.DefaultItem = True
        Me.miEmailAttend.Index = 0
        Me.miEmailAttend.Tag = "Attended"
        Me.miEmailAttend.Text = "All Who Attended"
        '
        'miEmailRegister
        '
        Me.miEmailRegister.Index = 1
        Me.miEmailRegister.Tag = "Registered"
        Me.miEmailRegister.Text = "All Who Registered"
        '
        'miEmailNotAttend
        '
        Me.miEmailNotAttend.Index = 2
        Me.miEmailNotAttend.Tag = "DidNotAttend"
        Me.miEmailNotAttend.Text = "All Who Did Not Attend"
        '
        'miFirstEmail
        '
        Me.miFirstEmail.Enabled = False
        Me.miFirstEmail.Index = 3
        Me.miFirstEmail.Tag = "FirstEmail"
        Me.miFirstEmail.Text = "Not Rec'd InfoLetter"
        '
        'MenuItem3
        '
        Me.MenuItem3.Index = 5
        Me.MenuItem3.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.miHelp, Me.miDef})
        Me.MenuItem3.Text = "Help"
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
        'txtSearch
        '
        Me.txtSearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.txtSearch.Location = New System.Drawing.Point(13, 407)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(119, 20)
        Me.txtSearch.TabIndex = 5
        Me.txtSearch.Text = "Type search text here."
        Me.ToolTip1.SetToolTip(Me.txtSearch, "Type search text here.  Use * for wildcard.")
        '
        'btnReport
        '
        Me.btnReport.BackColor = System.Drawing.SystemColors.Control
        Me.btnReport.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReport.Image = CType(resources.GetObject("btnReport.Image"), System.Drawing.Image)
        Me.btnReport.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnReport.Location = New System.Drawing.Point(87, 1)
        Me.btnReport.Name = "btnReport"
        Me.btnReport.Size = New System.Drawing.Size(41, 35)
        Me.btnReport.TabIndex = 416
        Me.btnReport.Text = "Report"
        Me.btnReport.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.btnReport, "Registration Reports")
        Me.btnReport.UseVisualStyleBackColor = False
        '
        'btnNew
        '
        Me.btnNew.BackColor = System.Drawing.Color.PaleGoldenrod
        Me.btnNew.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnNew.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnNew.Image = CType(resources.GetObject("btnNew.Image"), System.Drawing.Image)
        Me.btnNew.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnNew.Location = New System.Drawing.Point(3, 1)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(83, 35)
        Me.btnNew.TabIndex = 241
        Me.btnNew.Text = "New"
        Me.btnNew.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnNew.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ToolTip1.SetToolTip(Me.btnNew, "Add New Event")
        Me.btnNew.UseVisualStyleBackColor = False
        '
        'btnHelp
        '
        Me.btnHelp.BackColor = System.Drawing.SystemColors.Control
        Me.btnHelp.Font = New System.Drawing.Font("Microsoft Sans Serif", 22.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnHelp.ForeColor = System.Drawing.SystemColors.Control
        Me.btnHelp.Image = CType(resources.GetObject("btnHelp.Image"), System.Drawing.Image)
        Me.btnHelp.ImageAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnHelp.Location = New System.Drawing.Point(1032, 4)
        Me.btnHelp.Name = "btnHelp"
        Me.btnHelp.Size = New System.Drawing.Size(40, 25)
        Me.btnHelp.TabIndex = 236
        Me.btnHelp.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.ToolTip1.SetToolTip(Me.btnHelp, "Help: How to use this Search page.")
        Me.btnHelp.UseVisualStyleBackColor = False
        '
        'btnAddReg
        '
        Me.btnAddReg.Enabled = False
        Me.btnAddReg.Location = New System.Drawing.Point(319, 418)
        Me.btnAddReg.Name = "btnAddReg"
        Me.btnAddReg.Size = New System.Drawing.Size(46, 32)
        Me.btnAddReg.TabIndex = 44
        Me.btnAddReg.Text = "Add"
        Me.ToolTip1.SetToolTip(Me.btnAddReg, "add Registration or Payment to this Order")
        Me.btnAddReg.UseVisualStyleBackColor = True
        Me.btnAddReg.Visible = False
        '
        'btnRegion
        '
        Me.btnRegion.Location = New System.Drawing.Point(159, 6)
        Me.btnRegion.Name = "btnRegion"
        Me.btnRegion.Size = New System.Drawing.Size(75, 23)
        Me.btnRegion.TabIndex = 46
        Me.btnRegion.Text = "All Regions"
        Me.ToolTip1.SetToolTip(Me.btnRegion, "View Orders from All Regions")
        Me.btnRegion.UseVisualStyleBackColor = True
        '
        'cmd1
        '
        Me.cmd1.CommandText = "dbo.SrchEvents2"
        Me.cmd1.CommandType = System.Data.CommandType.StoredProcedure
        Me.cmd1.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, False, CType(0, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing), New System.Data.SqlClient.SqlParameter("@Region", System.Data.SqlDbType.VarChar, 20), New System.Data.SqlClient.SqlParameter("@dte", System.Data.SqlDbType.DateTime, 8), New System.Data.SqlClient.SqlParameter("@EventType", System.Data.SqlDbType.VarChar, 50), New System.Data.SqlClient.SqlParameter("@Title", System.Data.SqlDbType.VarChar, 100), New System.Data.SqlClient.SqlParameter("@Instructor", System.Data.SqlDbType.VarChar, 100), New System.Data.SqlClient.SqlParameter("@Case", System.Data.SqlDbType.VarChar, 100), New System.Data.SqlClient.SqlParameter("@Yr", System.Data.SqlDbType.Int, 4), New System.Data.SqlClient.SqlParameter("@Fld", System.Data.SqlDbType.VarChar, 15), New System.Data.SqlClient.SqlParameter("@IDNum", System.Data.SqlDbType.Int, 4)})
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.SystemColors.InactiveBorder
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Controls.Add(Me.pnlRegion)
        Me.Panel2.Controls.Add(Me.Label6)
        Me.Panel2.Controls.Add(Me.cboSpecific)
        Me.Panel2.Controls.Add(Me.btnSearch)
        Me.Panel2.Controls.Add(Me.txtSearch)
        Me.Panel2.Controls.Add(Me.chkWild)
        Me.Panel2.Controls.Add(Me.grpType)
        Me.Panel2.Controls.Add(Me.cboRetired)
        Me.Panel2.Controls.Add(Me.Label5)
        Me.Panel2.Location = New System.Drawing.Point(6, 6)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(187, 520)
        Me.Panel2.TabIndex = 183
        '
        'pnlRegion
        '
        Me.pnlRegion.Controls.Add(Me.cboDates)
        Me.pnlRegion.Controls.Add(Me.cboRegion)
        Me.pnlRegion.Location = New System.Drawing.Point(10, 7)
        Me.pnlRegion.Name = "pnlRegion"
        Me.pnlRegion.Size = New System.Drawing.Size(168, 60)
        Me.pnlRegion.TabIndex = 247
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Maroon
        Me.Label6.Location = New System.Drawing.Point(8, 361)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(188, 16)
        Me.Label6.TabIndex = 245
        Me.Label6.Text = "Search for Specific Event or Order"
        '
        'btnSearch
        '
        Me.btnSearch.BackColor = System.Drawing.SystemColors.Control
        Me.btnSearch.Location = New System.Drawing.Point(67, 73)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(75, 25)
        Me.btnSearch.TabIndex = 2
        Me.btnSearch.Text = "Search"
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'chkWild
        '
        Me.chkWild.AutoSize = True
        Me.chkWild.Checked = True
        Me.chkWild.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkWild.Location = New System.Drawing.Point(17, 433)
        Me.chkWild.Name = "chkWild"
        Me.chkWild.Size = New System.Drawing.Size(68, 17)
        Me.chkWild.TabIndex = 242
        Me.chkWild.Text = "Wildcard"
        Me.chkWild.UseVisualStyleBackColor = True
        Me.chkWild.Visible = False
        '
        'grpType
        '
        Me.grpType.Controls.Add(Me.rbLiveOnline)
        Me.grpType.Controls.Add(Me.Label4)
        Me.grpType.Controls.Add(Me.rbAllTypes)
        Me.grpType.Controls.Add(Me.rbRenewal)
        Me.grpType.Controls.Add(Me.rbEvaluation)
        Me.grpType.Controls.Add(Me.rbGrant)
        Me.grpType.Controls.Add(Me.rbWorkshop)
        Me.grpType.Controls.Add(Me.rbLongterm)
        Me.grpType.Controls.Add(Me.rbConference)
        Me.grpType.Controls.Add(Me.rbSponsored)
        Me.grpType.Controls.Add(Me.rbHosted)
        Me.grpType.Location = New System.Drawing.Point(2, 104)
        Me.grpType.Name = "grpType"
        Me.grpType.Size = New System.Drawing.Size(176, 250)
        Me.grpType.TabIndex = 3
        Me.grpType.TabStop = False
        Me.grpType.Text = "  "
        '
        'rbLiveOnline
        '
        Me.rbLiveOnline.AutoSize = True
        Me.rbLiveOnline.Location = New System.Drawing.Point(14, 99)
        Me.rbLiveOnline.Name = "rbLiveOnline"
        Me.rbLiveOnline.Size = New System.Drawing.Size(78, 17)
        Me.rbLiveOnline.TabIndex = 7
        Me.rbLiveOnline.Text = "Live Online"
        Me.rbLiveOnline.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Maroon
        Me.Label4.Location = New System.Drawing.Point(6, 23)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(128, 16)
        Me.Label4.TabIndex = 246
        Me.Label4.Text = "Filter by Type of Event"
        '
        'rbAllTypes
        '
        Me.rbAllTypes.AutoSize = True
        Me.rbAllTypes.Checked = True
        Me.rbAllTypes.Location = New System.Drawing.Point(6, 0)
        Me.rbAllTypes.Name = "rbAllTypes"
        Me.rbAllTypes.Size = New System.Drawing.Size(68, 17)
        Me.rbAllTypes.TabIndex = 11
        Me.rbAllTypes.TabStop = True
        Me.rbAllTypes.Text = "All Types"
        Me.rbAllTypes.UseVisualStyleBackColor = True
        '
        'rbRenewal
        '
        Me.rbRenewal.AutoSize = True
        Me.rbRenewal.Location = New System.Drawing.Point(14, 208)
        Me.rbRenewal.Name = "rbRenewal"
        Me.rbRenewal.Size = New System.Drawing.Size(156, 17)
        Me.rbRenewal.TabIndex = 19
        Me.rbRenewal.Text = "Indiana Clergy Ec. Renewal"
        Me.rbRenewal.UseVisualStyleBackColor = True
        '
        'rbEvaluation
        '
        Me.rbEvaluation.AutoSize = True
        Me.rbEvaluation.Location = New System.Drawing.Point(14, 188)
        Me.rbEvaluation.Name = "rbEvaluation"
        Me.rbEvaluation.Size = New System.Drawing.Size(111, 17)
        Me.rbEvaluation.TabIndex = 17
        Me.rbEvaluation.Text = "Evaluation Project"
        Me.rbEvaluation.UseVisualStyleBackColor = True
        '
        'rbGrant
        '
        Me.rbGrant.AutoSize = True
        Me.rbGrant.Location = New System.Drawing.Point(14, 137)
        Me.rbGrant.Name = "rbGrant"
        Me.rbGrant.Size = New System.Drawing.Size(122, 17)
        Me.rbGrant.TabIndex = 11
        Me.rbGrant.Text = "Major Grant Initiative"
        Me.rbGrant.UseVisualStyleBackColor = True
        '
        'rbWorkshop
        '
        Me.rbWorkshop.AutoSize = True
        Me.rbWorkshop.Location = New System.Drawing.Point(14, 156)
        Me.rbWorkshop.Name = "rbWorkshop"
        Me.rbWorkshop.Size = New System.Drawing.Size(74, 17)
        Me.rbWorkshop.TabIndex = 15
        Me.rbWorkshop.Text = "Workshop"
        Me.rbWorkshop.UseVisualStyleBackColor = True
        '
        'rbLongterm
        '
        Me.rbLongterm.AutoSize = True
        Me.rbLongterm.Location = New System.Drawing.Point(14, 118)
        Me.rbLongterm.Name = "rbLongterm"
        Me.rbLongterm.Size = New System.Drawing.Size(120, 17)
        Me.rbLongterm.TabIndex = 9
        Me.rbLongterm.Text = "Long Term Learning"
        Me.rbLongterm.UseVisualStyleBackColor = True
        '
        'rbConference
        '
        Me.rbConference.AutoSize = True
        Me.rbConference.Location = New System.Drawing.Point(14, 61)
        Me.rbConference.Name = "rbConference"
        Me.rbConference.Size = New System.Drawing.Size(80, 17)
        Me.rbConference.TabIndex = 3
        Me.rbConference.Text = "Conference"
        Me.rbConference.UseVisualStyleBackColor = True
        '
        'rbSponsored
        '
        Me.rbSponsored.AutoSize = True
        Me.rbSponsored.Location = New System.Drawing.Point(14, 42)
        Me.rbSponsored.Name = "rbSponsored"
        Me.rbSponsored.Size = New System.Drawing.Size(110, 17)
        Me.rbSponsored.TabIndex = 1
        Me.rbSponsored.Text = "Center Sponsored"
        Me.rbSponsored.UseVisualStyleBackColor = True
        '
        'rbHosted
        '
        Me.rbHosted.AutoSize = True
        Me.rbHosted.Location = New System.Drawing.Point(14, 80)
        Me.rbHosted.Name = "rbHosted"
        Me.rbHosted.Size = New System.Drawing.Size(73, 17)
        Me.rbHosted.TabIndex = 5
        Me.rbHosted.Text = "Hospitality"
        Me.rbHosted.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Maroon
        Me.Label5.Location = New System.Drawing.Point(14, 469)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(170, 16)
        Me.Label5.TabIndex = 182
        Me.Label5.Text = "Filter by Old Topics/Categories"
        '
        'chkDetail
        '
        Me.chkDetail.Checked = True
        Me.chkDetail.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkDetail.Location = New System.Drawing.Point(15, 5)
        Me.chkDetail.Margin = New System.Windows.Forms.Padding(0)
        Me.chkDetail.Name = "chkDetail"
        Me.chkDetail.Size = New System.Drawing.Size(525, 18)
        Me.chkDetail.TabIndex = 199
        Me.chkDetail.Text = "Check to list registrations below.  Uncheck to hide registrations for faster scro" & _
    "lling in upper grid."
        '
        'DsSrchWEvent1
        '
        Me.DsSrchWEvent1.DataSetName = "dsSrchWEvent"
        Me.DsSrchWEvent1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'styleSrchWEvents
        '
        Me.styleSrchWEvents.DataGrid = Nothing
        Me.styleSrchWEvents.HeaderForeColor = System.Drawing.SystemColors.ControlText
        '
        'DataGridTextBoxColumn11
        '
        Me.DataGridTextBoxColumn11.Format = ""
        Me.DataGridTextBoxColumn11.FormatInfo = Nothing
        Me.DataGridTextBoxColumn11.HeaderText = "EventID"
        Me.DataGridTextBoxColumn11.MappingName = "EventID"
        Me.DataGridTextBoxColumn11.NullText = ""
        Me.DataGridTextBoxColumn11.Width = 0
        '
        'colFDate
        '
        Me.colFDate.Format = "d"
        Me.colFDate.FormatInfo = Nothing
        Me.colFDate.HeaderText = "First Date"
        Me.colFDate.MappingName = "FirstDate"
        Me.colFDate.NullText = ""
        Me.colFDate.Width = 70
        '
        'colReg
        '
        Me.colReg.Format = ""
        Me.colReg.FormatInfo = Nothing
        Me.colReg.HeaderText = "Region"
        Me.colReg.MappingName = "SatelliteRegion"
        Me.colReg.NullText = ""
        Me.colReg.Width = 50
        '
        'colWName
        '
        Me.colWName.Format = ""
        Me.colWName.FormatInfo = Nothing
        Me.colWName.HeaderText = "Event Name"
        Me.colWName.MappingName = "EventName"
        Me.colWName.NullText = ""
        Me.colWName.ReadOnly = True
        Me.colWName.Width = 180
        '
        'colPresenter
        '
        Me.colPresenter.Format = ""
        Me.colPresenter.FormatInfo = Nothing
        Me.colPresenter.HeaderText = "Presenter"
        Me.colPresenter.MappingName = "Instructor"
        Me.colPresenter.NullText = ""
        Me.colPresenter.Width = 125
        '
        'DataGridTextBoxColumn5
        '
        Me.DataGridTextBoxColumn5.Format = ""
        Me.DataGridTextBoxColumn5.FormatInfo = Nothing
        Me.DataGridTextBoxColumn5.HeaderText = "Type"
        Me.DataGridTextBoxColumn5.MappingName = "TypeofEvent"
        Me.DataGridTextBoxColumn5.NullText = ""
        Me.DataGridTextBoxColumn5.Width = 75
        '
        'DataGridTextBoxColumn20
        '
        Me.DataGridTextBoxColumn20.Format = ""
        Me.DataGridTextBoxColumn20.FormatInfo = Nothing
        Me.DataGridTextBoxColumn20.HeaderText = "Reg"
        Me.DataGridTextBoxColumn20.MappingName = "Registered"
        Me.DataGridTextBoxColumn20.Width = 45
        '
        'DataGridTextBoxColumn7
        '
        Me.DataGridTextBoxColumn7.Format = ""
        Me.DataGridTextBoxColumn7.FormatInfo = Nothing
        Me.DataGridTextBoxColumn7.HeaderText = "Max"
        Me.DataGridTextBoxColumn7.MappingName = "MaximumSeating"
        Me.DataGridTextBoxColumn7.NullText = ""
        Me.DataGridTextBoxColumn7.Width = 45
        '
        'DataGridTextBoxColumn6
        '
        Me.DataGridTextBoxColumn6.Format = ""
        Me.DataGridTextBoxColumn6.FormatInfo = Nothing
        Me.DataGridTextBoxColumn6.HeaderText = "Total"
        Me.DataGridTextBoxColumn6.MappingName = "TotRegistered"
        Me.DataGridTextBoxColumn6.NullText = ""
        Me.DataGridTextBoxColumn6.Width = 45
        '
        'DataGridTextBoxColumn23
        '
        Me.DataGridTextBoxColumn23.Format = ""
        Me.DataGridTextBoxColumn23.FormatInfo = Nothing
        Me.DataGridTextBoxColumn23.HeaderText = "Veg"
        Me.DataGridTextBoxColumn23.MappingName = "Veg"
        Me.DataGridTextBoxColumn23.Width = 45
        '
        'colMasterName
        '
        Me.colMasterName.Format = ""
        Me.colMasterName.FormatInfo = Nothing
        Me.colMasterName.HeaderText = "Master Name"
        Me.colMasterName.MappingName = "MasterWorkshopName"
        Me.colMasterName.Width = 90
        '
        'colPending
        '
        Me.colPending.Alignment = System.Windows.Forms.HorizontalAlignment.Center
        Me.colPending.Format = ""
        Me.colPending.FormatInfo = Nothing
        Me.colPending.HeaderText = "Pending   "
        Me.colPending.MappingName = "Pending"
        Me.colPending.Width = 55
        '
        'colEventSKU
        '
        Me.colEventSKU.Format = ""
        Me.colEventSKU.FormatInfo = Nothing
        Me.colEventSKU.MappingName = "EventSKU"
        Me.colEventSKU.Width = 0
        '
        'colNIDEvent
        '
        Me.colNIDEvent.Format = ""
        Me.colNIDEvent.FormatInfo = Nothing
        Me.colNIDEvent.MappingName = "NIDEvent"
        Me.colNIDEvent.Width = 0
        '
        'fldEventID
        '
        Me.fldEventID.BackColor = System.Drawing.SystemColors.Control
        Me.fldEventID.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.fldEventID.Enabled = False
        Me.fldEventID.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fldEventID.ForeColor = System.Drawing.SystemColors.GrayText
        Me.fldEventID.Location = New System.Drawing.Point(955, 4)
        Me.fldEventID.Name = "fldEventID"
        Me.fldEventID.Size = New System.Drawing.Size(71, 14)
        Me.fldEventID.TabIndex = 237
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlDark
        Me.Label1.Location = New System.Drawing.Point(899, 4)
        Me.Label1.Margin = New System.Windows.Forms.Padding(0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(53, 18)
        Me.Label1.TabIndex = 239
        Me.Label1.Text = "Event #"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.SystemColors.Highlight
        Me.Panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel4.Controls.Add(Me.btnReport)
        Me.Panel4.Controls.Add(Me.btnNew)
        Me.Panel4.Location = New System.Drawing.Point(759, 4)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(133, 39)
        Me.Panel4.TabIndex = 424
        '
        'fldEventNID
        '
        Me.fldEventNID.BackColor = System.Drawing.SystemColors.Control
        Me.fldEventNID.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.fldEventNID.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fldEventNID.ForeColor = System.Drawing.SystemColors.GrayText
        Me.fldEventNID.Location = New System.Drawing.Point(566, 4)
        Me.fldEventNID.Name = "fldEventNID"
        Me.fldEventNID.ReadOnly = True
        Me.fldEventNID.Size = New System.Drawing.Size(71, 14)
        Me.fldEventNID.TabIndex = 431
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlDark
        Me.Label2.Location = New System.Drawing.Point(469, 4)
        Me.Label2.Margin = New System.Windows.Forms.Padding(0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(94, 18)
        Me.Label2.TabIndex = 432
        Me.Label2.Text = "Drupal Event #"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SplitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.SplitContainer1.Location = New System.Drawing.Point(199, 6)
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.chkDetail)
        Me.SplitContainer1.Panel2.Controls.Add(Me.grdSecond1)
        Me.SplitContainer1.Size = New System.Drawing.Size(881, 479)
        Me.SplitContainer1.SplitterDistance = 210
        Me.SplitContainer1.TabIndex = 434
        '
        'lblMainGrid
        '
        Me.lblMainGrid.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.lblMainGrid.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMainGrid.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblMainGrid.Location = New System.Drawing.Point(3, 0)
        Me.lblMainGrid.MinimumSize = New System.Drawing.Size(0, 18)
        Me.lblMainGrid.Name = "lblMainGrid"
        Me.lblMainGrid.Size = New System.Drawing.Size(882, 18)
        Me.lblMainGrid.TabIndex = 242
        Me.lblMainGrid.Text = "Events"
        Me.lblMainGrid.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'grdvwMain
        '
        Me.grdvwMain.AllowUserToAddRows = False
        Me.grdvwMain.AllowUserToDeleteRows = False
        Me.grdvwMain.AllowUserToOrderColumns = True
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(225, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.grdvwMain.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.grdvwMain.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
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
        Me.grdvwMain.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.EventIDDataGridViewTextBoxColumn, Me.FirstDateDataGridViewTextBoxColumn, Me.SatelliteRegionDataGridViewTextBoxColumn, Me.EventNameDataGridViewTextBoxColumn, Me.InstructorDataGridViewTextBoxColumn, Me.TypeofEventDataGridViewTextBoxColumn, Me.RegisteredDataGridViewTextBoxColumn, Me.MaximumSeatingDataGridViewTextBoxColumn, Me.TotRegisteredDataGridViewTextBoxColumn, Me.VegDataGridViewTextBoxColumn, Me.MasterWorkshopNameDataGridViewTextBoxColumn, Me.PendingDataGridViewTextBoxColumn, Me.NIDEventDataGridViewTextBoxColumn, Me.WorkshopNumDataGridViewTextBoxColumn, Me.EventSKUDataGridViewTextBoxColumn, Me.RegonlineNumDataGridViewTextBoxColumn})
        Me.grdvwMain.DataMember = "SrchWEvents"
        Me.grdvwMain.DataSource = Me.DsSrchWEvent1
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.grdvwMain.DefaultCellStyle = DataGridViewCellStyle3
        Me.grdvwMain.Location = New System.Drawing.Point(4, 17)
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
        Me.grdvwMain.RowHeadersWidth = 15
        Me.grdvwMain.Size = New System.Drawing.Size(870, 186)
        Me.grdvwMain.TabIndex = 10
        '
        'EventIDDataGridViewTextBoxColumn
        '
        Me.EventIDDataGridViewTextBoxColumn.DataPropertyName = "EventID"
        Me.EventIDDataGridViewTextBoxColumn.HeaderText = "EventID"
        Me.EventIDDataGridViewTextBoxColumn.Name = "EventIDDataGridViewTextBoxColumn"
        Me.EventIDDataGridViewTextBoxColumn.ReadOnly = True
        Me.EventIDDataGridViewTextBoxColumn.Visible = False
        Me.EventIDDataGridViewTextBoxColumn.Width = 5
        '
        'FirstDateDataGridViewTextBoxColumn
        '
        Me.FirstDateDataGridViewTextBoxColumn.DataPropertyName = "FirstDate"
        Me.FirstDateDataGridViewTextBoxColumn.HeaderText = "FirstDate"
        Me.FirstDateDataGridViewTextBoxColumn.Name = "FirstDateDataGridViewTextBoxColumn"
        Me.FirstDateDataGridViewTextBoxColumn.ReadOnly = True
        Me.FirstDateDataGridViewTextBoxColumn.Width = 70
        '
        'SatelliteRegionDataGridViewTextBoxColumn
        '
        Me.SatelliteRegionDataGridViewTextBoxColumn.DataPropertyName = "SatelliteRegion"
        Me.SatelliteRegionDataGridViewTextBoxColumn.HeaderText = "Region"
        Me.SatelliteRegionDataGridViewTextBoxColumn.Name = "SatelliteRegionDataGridViewTextBoxColumn"
        Me.SatelliteRegionDataGridViewTextBoxColumn.ReadOnly = True
        Me.SatelliteRegionDataGridViewTextBoxColumn.Width = 50
        '
        'EventNameDataGridViewTextBoxColumn
        '
        Me.EventNameDataGridViewTextBoxColumn.DataPropertyName = "EventName"
        Me.EventNameDataGridViewTextBoxColumn.HeaderText = "EventName"
        Me.EventNameDataGridViewTextBoxColumn.Name = "EventNameDataGridViewTextBoxColumn"
        Me.EventNameDataGridViewTextBoxColumn.ReadOnly = True
        Me.EventNameDataGridViewTextBoxColumn.Width = 180
        '
        'InstructorDataGridViewTextBoxColumn
        '
        Me.InstructorDataGridViewTextBoxColumn.DataPropertyName = "Instructor"
        Me.InstructorDataGridViewTextBoxColumn.HeaderText = "Presenter"
        Me.InstructorDataGridViewTextBoxColumn.Name = "InstructorDataGridViewTextBoxColumn"
        Me.InstructorDataGridViewTextBoxColumn.ReadOnly = True
        Me.InstructorDataGridViewTextBoxColumn.Width = 130
        '
        'TypeofEventDataGridViewTextBoxColumn
        '
        Me.TypeofEventDataGridViewTextBoxColumn.DataPropertyName = "TypeofEvent"
        Me.TypeofEventDataGridViewTextBoxColumn.HeaderText = "Type"
        Me.TypeofEventDataGridViewTextBoxColumn.Name = "TypeofEventDataGridViewTextBoxColumn"
        Me.TypeofEventDataGridViewTextBoxColumn.ReadOnly = True
        Me.TypeofEventDataGridViewTextBoxColumn.Width = 75
        '
        'RegisteredDataGridViewTextBoxColumn
        '
        Me.RegisteredDataGridViewTextBoxColumn.DataPropertyName = "Registered"
        Me.RegisteredDataGridViewTextBoxColumn.HeaderText = "Reg"
        Me.RegisteredDataGridViewTextBoxColumn.Name = "RegisteredDataGridViewTextBoxColumn"
        Me.RegisteredDataGridViewTextBoxColumn.ReadOnly = True
        Me.RegisteredDataGridViewTextBoxColumn.Width = 45
        '
        'MaximumSeatingDataGridViewTextBoxColumn
        '
        Me.MaximumSeatingDataGridViewTextBoxColumn.DataPropertyName = "MaximumSeating"
        Me.MaximumSeatingDataGridViewTextBoxColumn.HeaderText = "Max"
        Me.MaximumSeatingDataGridViewTextBoxColumn.Name = "MaximumSeatingDataGridViewTextBoxColumn"
        Me.MaximumSeatingDataGridViewTextBoxColumn.ReadOnly = True
        Me.MaximumSeatingDataGridViewTextBoxColumn.Width = 45
        '
        'TotRegisteredDataGridViewTextBoxColumn
        '
        Me.TotRegisteredDataGridViewTextBoxColumn.DataPropertyName = "TotRegistered"
        Me.TotRegisteredDataGridViewTextBoxColumn.HeaderText = "Total"
        Me.TotRegisteredDataGridViewTextBoxColumn.Name = "TotRegisteredDataGridViewTextBoxColumn"
        Me.TotRegisteredDataGridViewTextBoxColumn.ReadOnly = True
        Me.TotRegisteredDataGridViewTextBoxColumn.Width = 45
        '
        'VegDataGridViewTextBoxColumn
        '
        Me.VegDataGridViewTextBoxColumn.DataPropertyName = "Veg"
        Me.VegDataGridViewTextBoxColumn.HeaderText = "Veg"
        Me.VegDataGridViewTextBoxColumn.Name = "VegDataGridViewTextBoxColumn"
        Me.VegDataGridViewTextBoxColumn.ReadOnly = True
        Me.VegDataGridViewTextBoxColumn.Width = 45
        '
        'MasterWorkshopNameDataGridViewTextBoxColumn
        '
        Me.MasterWorkshopNameDataGridViewTextBoxColumn.DataPropertyName = "MasterWorkshopName"
        Me.MasterWorkshopNameDataGridViewTextBoxColumn.HeaderText = "MasterChecklist"
        Me.MasterWorkshopNameDataGridViewTextBoxColumn.Name = "MasterWorkshopNameDataGridViewTextBoxColumn"
        Me.MasterWorkshopNameDataGridViewTextBoxColumn.ReadOnly = True
        Me.MasterWorkshopNameDataGridViewTextBoxColumn.Width = 90
        '
        'PendingDataGridViewTextBoxColumn
        '
        Me.PendingDataGridViewTextBoxColumn.DataPropertyName = "Pending"
        Me.PendingDataGridViewTextBoxColumn.HeaderText = "Pending"
        Me.PendingDataGridViewTextBoxColumn.Name = "PendingDataGridViewTextBoxColumn"
        Me.PendingDataGridViewTextBoxColumn.ReadOnly = True
        Me.PendingDataGridViewTextBoxColumn.Width = 50
        '
        'NIDEventDataGridViewTextBoxColumn
        '
        Me.NIDEventDataGridViewTextBoxColumn.DataPropertyName = "NIDEvent"
        Me.NIDEventDataGridViewTextBoxColumn.HeaderText = "NIDEvent"
        Me.NIDEventDataGridViewTextBoxColumn.Name = "NIDEventDataGridViewTextBoxColumn"
        Me.NIDEventDataGridViewTextBoxColumn.ReadOnly = True
        Me.NIDEventDataGridViewTextBoxColumn.Visible = False
        '
        'WorkshopNumDataGridViewTextBoxColumn
        '
        Me.WorkshopNumDataGridViewTextBoxColumn.DataPropertyName = "WorkshopNum"
        Me.WorkshopNumDataGridViewTextBoxColumn.HeaderText = "WorkshopNum"
        Me.WorkshopNumDataGridViewTextBoxColumn.Name = "WorkshopNumDataGridViewTextBoxColumn"
        Me.WorkshopNumDataGridViewTextBoxColumn.ReadOnly = True
        Me.WorkshopNumDataGridViewTextBoxColumn.Visible = False
        '
        'EventSKUDataGridViewTextBoxColumn
        '
        Me.EventSKUDataGridViewTextBoxColumn.DataPropertyName = "EventSKU"
        Me.EventSKUDataGridViewTextBoxColumn.HeaderText = "EventSKU"
        Me.EventSKUDataGridViewTextBoxColumn.Name = "EventSKUDataGridViewTextBoxColumn"
        Me.EventSKUDataGridViewTextBoxColumn.ReadOnly = True
        Me.EventSKUDataGridViewTextBoxColumn.Visible = False
        '
        'RegonlineNumDataGridViewTextBoxColumn
        '
        Me.RegonlineNumDataGridViewTextBoxColumn.DataPropertyName = "RegonlineNum"
        Me.RegonlineNumDataGridViewTextBoxColumn.HeaderText = "RegonlineNum"
        Me.RegonlineNumDataGridViewTextBoxColumn.Name = "RegonlineNumDataGridViewTextBoxColumn"
        Me.RegonlineNumDataGridViewTextBoxColumn.ReadOnly = True
        Me.RegonlineNumDataGridViewTextBoxColumn.Visible = False
        Me.RegonlineNumDataGridViewTextBoxColumn.Width = 5
        '
        'grdSecond1
        '
        Me.grdSecond1.AlternatingBackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.grdSecond1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.grdSecond1.CaptionBackColor = System.Drawing.SystemColors.InactiveCaption
        Me.grdSecond1.DataMember = "getEventRegistrantList2"
        Me.grdSecond1.DataSource = Me.DsGetEventRegList2
        Me.grdSecond1.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.grdSecond1.Location = New System.Drawing.Point(4, 24)
        Me.grdSecond1.Name = "grdSecond1"
        Me.grdSecond1.ParentRowsVisible = False
        Me.grdSecond1.ReadOnly = True
        Me.grdSecond1.RowHeaderWidth = 15
        Me.grdSecond1.Size = New System.Drawing.Size(870, 234)
        Me.grdSecond1.TabIndex = 9
        Me.grdSecond1.TableStyles.AddRange(New System.Windows.Forms.DataGridTableStyle() {Me.DataGridTableStyle2})
        '
        'DsGetEventRegList2
        '
        Me.DsGetEventRegList2.DataSetName = "dsGetEventRegList2"
        Me.DsGetEventRegList2.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'DataGridTableStyle2
        '
        Me.DataGridTableStyle2.AlternatingBackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.DataGridTableStyle2.DataGrid = Me.grdSecond1
        Me.DataGridTableStyle2.GridColumnStyles.AddRange(New System.Windows.Forms.DataGridColumnStyle() {Me.DataGridTextBoxColumn14, Me.DataGridTextBoxColumn10, Me.colSecLastname, Me.colSecFirstname, Me.colSecOrg, Me.DataGridTextBoxColumn21, Me.DataGridTextBoxColumn3, Me.colPd, Me.DataGridBoolColumn1, Me.DataGridTextBoxColumn18, Me.DataGridTextBoxColumn15, Me.DataGridTextBoxColumn16, Me.DataGridTextBoxColumn17, Me.DataGridTextBoxColumn19, Me.DataGridTextBoxColumn4, Me.DataGridTextBoxColumn2, Me.DataGridTextBoxColumn1})
        Me.DataGridTableStyle2.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.DataGridTableStyle2.MappingName = "getEventRegistrantList2"
        Me.DataGridTableStyle2.RowHeaderWidth = 15
        '
        'DataGridTextBoxColumn14
        '
        Me.DataGridTextBoxColumn14.Format = ""
        Me.DataGridTextBoxColumn14.FormatInfo = Nothing
        Me.DataGridTextBoxColumn14.HeaderText = "RegID"
        Me.DataGridTextBoxColumn14.MappingName = "RegistrationID"
        Me.DataGridTextBoxColumn14.ReadOnly = True
        Me.DataGridTextBoxColumn14.Width = 0
        '
        'DataGridTextBoxColumn10
        '
        Me.DataGridTextBoxColumn10.Format = ""
        Me.DataGridTextBoxColumn10.FormatInfo = Nothing
        Me.DataGridTextBoxColumn10.HeaderText = "EventNum"
        Me.DataGridTextBoxColumn10.MappingName = "EventNum"
        Me.DataGridTextBoxColumn10.ReadOnly = True
        Me.DataGridTextBoxColumn10.Width = 0
        '
        'colSecLastname
        '
        Me.colSecLastname.Format = ""
        Me.colSecLastname.FormatInfo = Nothing
        Me.colSecLastname.HeaderText = "LastName"
        Me.colSecLastname.MappingName = "LastName"
        Me.colSecLastname.NullText = ""
        Me.colSecLastname.ReadOnly = True
        Me.colSecLastname.Width = 75
        '
        'colSecFirstname
        '
        Me.colSecFirstname.Format = ""
        Me.colSecFirstname.FormatInfo = Nothing
        Me.colSecFirstname.HeaderText = "First Name"
        Me.colSecFirstname.MappingName = "FirstName"
        Me.colSecFirstname.NullText = ""
        Me.colSecFirstname.ReadOnly = True
        Me.colSecFirstname.Width = 70
        '
        'colSecOrg
        '
        Me.colSecOrg.Format = ""
        Me.colSecOrg.FormatInfo = Nothing
        Me.colSecOrg.HeaderText = "Organization"
        Me.colSecOrg.MappingName = "Organization"
        Me.colSecOrg.ReadOnly = True
        Me.colSecOrg.Width = 140
        '
        'DataGridTextBoxColumn21
        '
        Me.DataGridTextBoxColumn21.Format = "d"
        Me.DataGridTextBoxColumn21.FormatInfo = Nothing
        Me.DataGridTextBoxColumn21.HeaderText = "Date"
        Me.DataGridTextBoxColumn21.MappingName = "RegDate"
        Me.DataGridTextBoxColumn21.ReadOnly = True
        Me.DataGridTextBoxColumn21.Width = 70
        '
        'DataGridTextBoxColumn3
        '
        Me.DataGridTextBoxColumn3.Format = ""
        Me.DataGridTextBoxColumn3.FormatInfo = Nothing
        Me.DataGridTextBoxColumn3.HeaderText = "Order #"
        Me.DataGridTextBoxColumn3.MappingName = "OrderNum"
        Me.DataGridTextBoxColumn3.ReadOnly = True
        Me.DataGridTextBoxColumn3.Width = 70
        '
        'colPd
        '
        Me.colPd.Format = ""
        Me.colPd.FormatInfo = Nothing
        Me.colPd.HeaderText = "Pd"
        Me.colPd.MappingName = "IndivDue"
        Me.colPd.ReadOnly = True
        Me.colPd.Width = 60
        '
        'DataGridBoolColumn1
        '
        Me.DataGridBoolColumn1.HeaderText = "Registrar"
        Me.DataGridBoolColumn1.MappingName = "Registrar"
        Me.DataGridBoolColumn1.Width = 0
        '
        'DataGridTextBoxColumn18
        '
        Me.DataGridTextBoxColumn18.Format = ""
        Me.DataGridTextBoxColumn18.FormatInfo = Nothing
        Me.DataGridTextBoxColumn18.HeaderText = "Notes"
        Me.DataGridTextBoxColumn18.MappingName = "Notes"
        Me.DataGridTextBoxColumn18.ReadOnly = True
        Me.DataGridTextBoxColumn18.Width = 75
        '
        'DataGridTextBoxColumn15
        '
        Me.DataGridTextBoxColumn15.Alignment = System.Windows.Forms.HorizontalAlignment.Center
        Me.DataGridTextBoxColumn15.Format = ""
        Me.DataGridTextBoxColumn15.FormatInfo = Nothing
        Me.DataGridTextBoxColumn15.HeaderText = "Confirmation"
        Me.DataGridTextBoxColumn15.MappingName = "Confirmation"
        Me.DataGridTextBoxColumn15.ReadOnly = True
        Me.DataGridTextBoxColumn15.Width = 38
        '
        'DataGridTextBoxColumn16
        '
        Me.DataGridTextBoxColumn16.Alignment = System.Windows.Forms.HorizontalAlignment.Center
        Me.DataGridTextBoxColumn16.Format = ""
        Me.DataGridTextBoxColumn16.FormatInfo = Nothing
        Me.DataGridTextBoxColumn16.HeaderText = "Information"
        Me.DataGridTextBoxColumn16.MappingName = "Information"
        Me.DataGridTextBoxColumn16.ReadOnly = True
        Me.DataGridTextBoxColumn16.Width = 38
        '
        'DataGridTextBoxColumn17
        '
        Me.DataGridTextBoxColumn17.Alignment = System.Windows.Forms.HorizontalAlignment.Center
        Me.DataGridTextBoxColumn17.Format = ""
        Me.DataGridTextBoxColumn17.FormatInfo = Nothing
        Me.DataGridTextBoxColumn17.HeaderText = "Nametag"
        Me.DataGridTextBoxColumn17.MappingName = "Nametag"
        Me.DataGridTextBoxColumn17.ReadOnly = True
        Me.DataGridTextBoxColumn17.Width = 38
        '
        'DataGridTextBoxColumn19
        '
        Me.DataGridTextBoxColumn19.Alignment = System.Windows.Forms.HorizontalAlignment.Center
        Me.DataGridTextBoxColumn19.Format = ""
        Me.DataGridTextBoxColumn19.FormatInfo = Nothing
        Me.DataGridTextBoxColumn19.HeaderText = "Attended"
        Me.DataGridTextBoxColumn19.MappingName = "Attended"
        Me.DataGridTextBoxColumn19.ReadOnly = True
        Me.DataGridTextBoxColumn19.Width = 38
        '
        'DataGridTextBoxColumn4
        '
        Me.DataGridTextBoxColumn4.Format = ""
        Me.DataGridTextBoxColumn4.FormatInfo = Nothing
        Me.DataGridTextBoxColumn4.HeaderText = "Vegetarian"
        Me.DataGridTextBoxColumn4.MappingName = "Vegetarian"
        Me.DataGridTextBoxColumn4.ReadOnly = True
        Me.DataGridTextBoxColumn4.Width = 38
        '
        'DataGridTextBoxColumn2
        '
        Me.DataGridTextBoxColumn2.Format = ""
        Me.DataGridTextBoxColumn2.FormatInfo = Nothing
        Me.DataGridTextBoxColumn2.HeaderText = "Cancelled"
        Me.DataGridTextBoxColumn2.MappingName = "Cancelled"
        Me.DataGridTextBoxColumn2.Width = 38
        '
        'DataGridTextBoxColumn1
        '
        Me.DataGridTextBoxColumn1.Format = ""
        Me.DataGridTextBoxColumn1.FormatInfo = Nothing
        Me.DataGridTextBoxColumn1.HeaderText = "Payment#"
        Me.DataGridTextBoxColumn1.MappingName = "PaymentNum"
        Me.DataGridTextBoxColumn1.Width = 50
        '
        'DataGridTextBoxColumn25
        '
        Me.DataGridTextBoxColumn25.Format = ""
        Me.DataGridTextBoxColumn25.FormatInfo = Nothing
        Me.DataGridTextBoxColumn25.HeaderText = "EventNID"
        Me.DataGridTextBoxColumn25.MappingName = "EventNID"
        Me.DataGridTextBoxColumn25.Width = 25
        '
        'DataGridTextBoxColumn24
        '
        Me.DataGridTextBoxColumn24.Format = ""
        Me.DataGridTextBoxColumn24.FormatInfo = Nothing
        Me.DataGridTextBoxColumn24.HeaderText = "WorkshopNum"
        Me.DataGridTextBoxColumn24.MappingName = "WorkshopNum"
        Me.DataGridTextBoxColumn24.Width = 25
        '
        'fldRegID
        '
        Me.fldRegID.BackColor = System.Drawing.SystemColors.Control
        Me.fldRegID.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.fldRegID.Enabled = False
        Me.fldRegID.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fldRegID.ForeColor = System.Drawing.SystemColors.GrayText
        Me.fldRegID.Location = New System.Drawing.Point(955, 29)
        Me.fldRegID.Name = "fldRegID"
        Me.fldRegID.Size = New System.Drawing.Size(71, 13)
        Me.fldRegID.TabIndex = 435
        '
        'Label9
        '
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.SystemColors.ControlDark
        Me.Label9.Location = New System.Drawing.Point(910, 27)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(38, 16)
        Me.Label9.TabIndex = 436
        Me.Label9.Text = "Reg. #"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'fldEventSKU
        '
        Me.fldEventSKU.BackColor = System.Drawing.SystemColors.Control
        Me.fldEventSKU.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.fldEventSKU.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fldEventSKU.ForeColor = System.Drawing.SystemColors.GrayText
        Me.fldEventSKU.Location = New System.Drawing.Point(566, 24)
        Me.fldEventSKU.Name = "fldEventSKU"
        Me.fldEventSKU.ReadOnly = True
        Me.fldEventSKU.Size = New System.Drawing.Size(175, 14)
        Me.fldEventSKU.TabIndex = 437
        '
        'Label8
        '
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlDark
        Me.Label8.Location = New System.Drawing.Point(436, 24)
        Me.Label8.Margin = New System.Windows.Forms.Padding(0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(127, 18)
        Me.Label8.TabIndex = 438
        Me.Label8.Text = "RegOnline Event #"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.pgEvent)
        Me.TabControl1.Controls.Add(Me.pgOrder)
        Me.TabControl1.Location = New System.Drawing.Point(0, 30)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(1096, 523)
        Me.TabControl1.TabIndex = 0
        Me.TabControl1.Tag = "tab"
        '
        'pgEvent
        '
        Me.pgEvent.Controls.Add(Me.Panel2)
        Me.pgEvent.Controls.Add(Me.SplitContainer1)
        Me.pgEvent.Location = New System.Drawing.Point(4, 22)
        Me.pgEvent.Name = "pgEvent"
        Me.pgEvent.Padding = New System.Windows.Forms.Padding(3)
        Me.pgEvent.Size = New System.Drawing.Size(1088, 497)
        Me.pgEvent.TabIndex = 0
        Me.pgEvent.Tag = "EVENTS"
        Me.pgEvent.Text = "EVENTS  "
        Me.pgEvent.UseVisualStyleBackColor = True
        '
        'pgOrder
        '
        Me.pgOrder.Controls.Add(Me.SplitContainer)
        Me.pgOrder.Location = New System.Drawing.Point(4, 22)
        Me.pgOrder.Name = "pgOrder"
        Me.pgOrder.Padding = New System.Windows.Forms.Padding(3)
        Me.pgOrder.Size = New System.Drawing.Size(1088, 518)
        Me.pgOrder.TabIndex = 1
        Me.pgOrder.Tag = "ORDERS"
        Me.pgOrder.Text = "ORDERS  "
        Me.pgOrder.UseVisualStyleBackColor = True
        '
        'SplitContainer
        '
        Me.SplitContainer.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.SplitContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.SplitContainer.Location = New System.Drawing.Point(6, 6)
        Me.SplitContainer.Name = "SplitContainer"
        '
        'SplitContainer.Panel1
        '
        Me.SplitContainer.Panel1.Controls.Add(Me.btnRegion)
        Me.SplitContainer.Panel1.Controls.Add(Me.Label13)
        Me.SplitContainer.Panel1.Controls.Add(Me.btnAddReg)
        Me.SplitContainer.Panel1.Controls.Add(Me.cboProposedPayment)
        Me.SplitContainer.Panel1.Controls.Add(ProposedPaymentMethodLabel)
        Me.SplitContainer.Panel1.Controls.Add(Me.txtdtOrder)
        Me.SplitContainer.Panel1.Controls.Add(Me.cboOrderStatus)
        Me.SplitContainer.Panel1.Controls.Add(Me.TextBox1)
        Me.SplitContainer.Panel1.Controls.Add(Me.txtOrderNotes)
        Me.SplitContainer.Panel1.Controls.Add(Me.Label12)
        Me.SplitContainer.Panel1.Controls.Add(Me.AmountOrderTotalLabel)
        Me.SplitContainer.Panel1.Controls.Add(DtOrderLabel)
        Me.SplitContainer.Panel1.Controls.Add(Me.txtAmountOrderTotal)
        Me.SplitContainer.Panel1.Controls.Add(OrderStatusLabel)
        Me.SplitContainer.Panel1.Controls.Add(OrderNotesLabel)
        Me.SplitContainer.Panel1.Controls.Add(Me.lblMax)
        Me.SplitContainer.Panel1.Controls.Add(Me.Label3)
        Me.SplitContainer.Panel1.Controls.Add(Me.btnSrchOrder)
        Me.SplitContainer.Panel1.Controls.Add(Me.txtSrchOrder)
        Me.SplitContainer.Panel1.Controls.Add(Me.treevOrders)
        '
        'SplitContainer.Panel2
        '
        Me.SplitContainer.Panel2.Controls.Add(Me.dgvPending2)
        Me.SplitContainer.Panel2.Controls.Add(Me.dgvReg2)
        Me.SplitContainer.Panel2.Controls.Add(Me.dgvFinance2)
        Me.SplitContainer.Panel2.Controls.Add(Me.Label11)
        Me.SplitContainer.Panel2.Controls.Add(Me.Label10)
        Me.SplitContainer.Panel2.Controls.Add(Me.Label7)
        Me.SplitContainer.Size = New System.Drawing.Size(1076, 631)
        Me.SplitContainer.SplitterDistance = 395
        Me.SplitContainer.SplitterWidth = 3
        Me.SplitContainer.TabIndex = 1
        Me.SplitContainer.Text = "SplitContainer1"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.Color.MistyRose
        Me.Label13.Location = New System.Drawing.Point(224, 428)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(71, 156)
        Me.Label13.TabIndex = 45
        Me.Label13.Text = "" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "WAITING " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "FOR" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & " FEEDBACK" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "is this a " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "convenient" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "place to edit " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Order and/or " & _
    "" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "add a " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "registrant?" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "  "
        Me.Label13.Visible = False
        '
        'txtdtOrder
        '
        Me.txtdtOrder.Enabled = False
        Me.txtdtOrder.Location = New System.Drawing.Point(159, 425)
        Me.txtdtOrder.Name = "txtdtOrder"
        Me.txtdtOrder.Size = New System.Drawing.Size(60, 20)
        Me.txtdtOrder.TabIndex = 41
        Me.txtdtOrder.Visible = False
        '
        'TextBox1
        '
        Me.TextBox1.Enabled = False
        Me.TextBox1.Location = New System.Drawing.Point(159, 585)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(131, 20)
        Me.TextBox1.TabIndex = 13
        Me.TextBox1.Visible = False
        '
        'txtOrderNotes
        '
        Me.txtOrderNotes.Enabled = False
        Me.txtOrderNotes.Location = New System.Drawing.Point(72, 509)
        Me.txtOrderNotes.Multiline = True
        Me.txtOrderNotes.Name = "txtOrderNotes"
        Me.txtOrderNotes.Size = New System.Drawing.Size(283, 40)
        Me.txtOrderNotes.TabIndex = 39
        Me.txtOrderNotes.Visible = False
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(50, 588)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(83, 13)
        Me.Label12.TabIndex = 12
        Me.Label12.Text = "Registrar Name:"
        Me.Label12.Visible = False
        '
        'AmountOrderTotalLabel
        '
        Me.AmountOrderTotalLabel.AutoSize = True
        Me.AmountOrderTotalLabel.Location = New System.Drawing.Point(55, 459)
        Me.AmountOrderTotalLabel.Name = "AmountOrderTotalLabel"
        Me.AmountOrderTotalLabel.Size = New System.Drawing.Size(94, 13)
        Me.AmountOrderTotalLabel.TabIndex = 38
        Me.AmountOrderTotalLabel.Text = "Amount Tot Order:"
        Me.AmountOrderTotalLabel.Visible = False
        '
        'txtAmountOrderTotal
        '
        Me.txtAmountOrderTotal.Enabled = False
        Me.txtAmountOrderTotal.Location = New System.Drawing.Point(159, 456)
        Me.txtAmountOrderTotal.Name = "txtAmountOrderTotal"
        Me.txtAmountOrderTotal.Size = New System.Drawing.Size(60, 20)
        Me.txtAmountOrderTotal.TabIndex = 37
        Me.txtAmountOrderTotal.Visible = False
        '
        'lblMax
        '
        Me.lblMax.AutoSize = True
        Me.lblMax.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMax.Location = New System.Drawing.Point(326, 12)
        Me.lblMax.Name = "lblMax"
        Me.lblMax.Size = New System.Drawing.Size(39, 13)
        Me.lblMax.TabIndex = 6
        Me.lblMax.Text = "Label4"
        Me.lblMax.Visible = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(242, 2)
        Me.Label3.MaximumSize = New System.Drawing.Size(100, 0)
        Me.Label3.MinimumSize = New System.Drawing.Size(0, 30)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(80, 30)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "Highest Order#:"
        Me.Label3.Visible = False
        '
        'btnSrchOrder
        '
        Me.btnSrchOrder.Location = New System.Drawing.Point(71, 6)
        Me.btnSrchOrder.Name = "btnSrchOrder"
        Me.btnSrchOrder.Size = New System.Drawing.Size(59, 23)
        Me.btnSrchOrder.TabIndex = 5
        Me.btnSrchOrder.Tag = "  Search for Specific Order#"
        Me.btnSrchOrder.Text = "Search"
        Me.btnSrchOrder.UseVisualStyleBackColor = True
        '
        'txtSrchOrder
        '
        Me.txtSrchOrder.Location = New System.Drawing.Point(7, 6)
        Me.txtSrchOrder.Name = "txtSrchOrder"
        Me.txtSrchOrder.Size = New System.Drawing.Size(59, 20)
        Me.txtSrchOrder.TabIndex = 4
        Me.txtSrchOrder.Text = "*"
        Me.txtSrchOrder.WordWrap = False
        '
        'treevOrders
        '
        Me.treevOrders.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.treevOrders.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.treevOrders.FullRowSelect = True
        Me.treevOrders.HideSelection = False
        Me.treevOrders.Location = New System.Drawing.Point(4, 35)
        Me.treevOrders.Name = "treevOrders"
        Me.treevOrders.ShowLines = False
        Me.treevOrders.Size = New System.Drawing.Size(386, 370)
        Me.treevOrders.TabIndex = 0
        '
        'dgvPending2
        '
        Me.dgvPending2.AllowUserToAddRows = False
        Me.dgvPending2.AllowUserToDeleteRows = False
        Me.dgvPending2.AllowUserToOrderColumns = True
        Me.dgvPending2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvPending2.AutoGenerateColumns = False
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvPending2.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle5
        Me.dgvPending2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvPending2.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colPendingSID, Me.colPendingOID, Me.DataGridViewTextBoxColumn46, Me.DataGridViewTextBoxColumn43, Me.DataGridViewTextBoxColumn44, Me.DataGridViewTextBoxColumn45, Me.DataGridViewCheckBoxColumn4, Me.SID})
        Me.dgvPending2.DataMember = "tOrderLstPendings"
        Me.dgvPending2.DataSource = Me.DsEventRegOrderTV2
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvPending2.DefaultCellStyle = DataGridViewCellStyle6
        Me.dgvPending2.Location = New System.Drawing.Point(5, 379)
        Me.dgvPending2.Name = "dgvPending2"
        Me.dgvPending2.ReadOnly = True
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvPending2.RowHeadersDefaultCellStyle = DataGridViewCellStyle7
        Me.dgvPending2.RowHeadersWidth = 15
        Me.dgvPending2.Size = New System.Drawing.Size(679, 162)
        Me.dgvPending2.TabIndex = 8
        '
        'colPendingSID
        '
        Me.colPendingSID.DataPropertyName = "SID"
        Me.colPendingSID.HeaderText = "SID"
        Me.colPendingSID.Name = "colPendingSID"
        Me.colPendingSID.ReadOnly = True
        Me.colPendingSID.Width = 5
        '
        'colPendingOID
        '
        Me.colPendingOID.DataPropertyName = "OID"
        Me.colPendingOID.HeaderText = "Order#"
        Me.colPendingOID.Name = "colPendingOID"
        Me.colPendingOID.ReadOnly = True
        Me.colPendingOID.Width = 65
        '
        'DataGridViewTextBoxColumn46
        '
        Me.DataGridViewTextBoxColumn46.DataPropertyName = "LFName"
        Me.DataGridViewTextBoxColumn46.HeaderText = "LFName"
        Me.DataGridViewTextBoxColumn46.Name = "DataGridViewTextBoxColumn46"
        Me.DataGridViewTextBoxColumn46.ReadOnly = True
        '
        'DataGridViewTextBoxColumn43
        '
        Me.DataGridViewTextBoxColumn43.DataPropertyName = "Company"
        Me.DataGridViewTextBoxColumn43.HeaderText = "Congregation"
        Me.DataGridViewTextBoxColumn43.Name = "DataGridViewTextBoxColumn43"
        Me.DataGridViewTextBoxColumn43.ReadOnly = True
        Me.DataGridViewTextBoxColumn43.Width = 130
        '
        'DataGridViewTextBoxColumn44
        '
        Me.DataGridViewTextBoxColumn44.DataPropertyName = "EventSKU"
        Me.DataGridViewTextBoxColumn44.HeaderText = "Event"
        Me.DataGridViewTextBoxColumn44.Name = "DataGridViewTextBoxColumn44"
        Me.DataGridViewTextBoxColumn44.ReadOnly = True
        Me.DataGridViewTextBoxColumn44.Width = 150
        '
        'DataGridViewTextBoxColumn45
        '
        Me.DataGridViewTextBoxColumn45.DataPropertyName = "MatchFirstDate"
        Me.DataGridViewTextBoxColumn45.HeaderText = "Event Date"
        Me.DataGridViewTextBoxColumn45.Name = "DataGridViewTextBoxColumn45"
        Me.DataGridViewTextBoxColumn45.ReadOnly = True
        Me.DataGridViewTextBoxColumn45.Width = 75
        '
        'DataGridViewCheckBoxColumn4
        '
        Me.DataGridViewCheckBoxColumn4.DataPropertyName = "Registrar"
        Me.DataGridViewCheckBoxColumn4.HeaderText = "Registrar"
        Me.DataGridViewCheckBoxColumn4.Name = "DataGridViewCheckBoxColumn4"
        Me.DataGridViewCheckBoxColumn4.ReadOnly = True
        Me.DataGridViewCheckBoxColumn4.Width = 60
        '
        'SID
        '
        Me.SID.DataPropertyName = "SID"
        Me.SID.HeaderText = "Confirm#"
        Me.SID.Name = "SID"
        Me.SID.ReadOnly = True
        '
        'DsEventRegOrderTV2
        '
        Me.DsEventRegOrderTV2.DataSetName = "dsEventRegOrderTV"
        Me.DsEventRegOrderTV2.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'dgvReg2
        '
        Me.dgvReg2.AllowUserToAddRows = False
        Me.dgvReg2.AllowUserToDeleteRows = False
        Me.dgvReg2.AllowUserToOrderColumns = True
        Me.dgvReg2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvReg2.AutoGenerateColumns = False
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvReg2.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle8
        Me.dgvReg2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvReg2.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colRegID, Me.DataGridViewTextBoxColumn35, Me.colRegLFName, Me.DataGridViewTextBoxColumn38, Me.colRegEventName, Me.DataGridViewTextBoxColumn36, Me.Fee, Me.DataGridViewCheckBoxColumn3, Me.OnlineSID})
        Me.dgvReg2.DataMember = "tOrderLstRegistrations"
        Me.dgvReg2.DataSource = Me.DsEventRegOrderTV2
        DataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle9.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvReg2.DefaultCellStyle = DataGridViewCellStyle9
        Me.dgvReg2.Location = New System.Drawing.Point(5, 170)
        Me.dgvReg2.Name = "dgvReg2"
        Me.dgvReg2.ReadOnly = True
        DataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle10.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvReg2.RowHeadersDefaultCellStyle = DataGridViewCellStyle10
        Me.dgvReg2.RowHeadersWidth = 15
        Me.dgvReg2.Size = New System.Drawing.Size(679, 180)
        Me.dgvReg2.TabIndex = 7
        '
        'colRegID
        '
        Me.colRegID.DataPropertyName = "RegNum"
        Me.colRegID.HeaderText = "RegNum"
        Me.colRegID.Name = "colRegID"
        Me.colRegID.ReadOnly = True
        Me.colRegID.Visible = False
        '
        'DataGridViewTextBoxColumn35
        '
        Me.DataGridViewTextBoxColumn35.DataPropertyName = "OrderNum"
        Me.DataGridViewTextBoxColumn35.HeaderText = "Order#"
        Me.DataGridViewTextBoxColumn35.Name = "DataGridViewTextBoxColumn35"
        Me.DataGridViewTextBoxColumn35.ReadOnly = True
        Me.DataGridViewTextBoxColumn35.Width = 65
        '
        'colRegLFName
        '
        Me.colRegLFName.DataPropertyName = "LFName"
        Me.colRegLFName.HeaderText = "LFName"
        Me.colRegLFName.Name = "colRegLFName"
        Me.colRegLFName.ReadOnly = True
        '
        'DataGridViewTextBoxColumn38
        '
        Me.DataGridViewTextBoxColumn38.DataPropertyName = "OrgName"
        Me.DataGridViewTextBoxColumn38.HeaderText = "Congregation"
        Me.DataGridViewTextBoxColumn38.Name = "DataGridViewTextBoxColumn38"
        Me.DataGridViewTextBoxColumn38.ReadOnly = True
        Me.DataGridViewTextBoxColumn38.Width = 130
        '
        'colRegEventName
        '
        Me.colRegEventName.DataPropertyName = "EventName"
        Me.colRegEventName.HeaderText = "Event"
        Me.colRegEventName.Name = "colRegEventName"
        Me.colRegEventName.ReadOnly = True
        Me.colRegEventName.Width = 160
        '
        'DataGridViewTextBoxColumn36
        '
        Me.DataGridViewTextBoxColumn36.DataPropertyName = "EventDt"
        Me.DataGridViewTextBoxColumn36.HeaderText = "Event Date"
        Me.DataGridViewTextBoxColumn36.Name = "DataGridViewTextBoxColumn36"
        Me.DataGridViewTextBoxColumn36.ReadOnly = True
        Me.DataGridViewTextBoxColumn36.Width = 70
        '
        'Fee
        '
        Me.Fee.DataPropertyName = "Fee"
        Me.Fee.HeaderText = "Fee"
        Me.Fee.Name = "Fee"
        Me.Fee.ReadOnly = True
        Me.Fee.Width = 30
        '
        'DataGridViewCheckBoxColumn3
        '
        Me.DataGridViewCheckBoxColumn3.DataPropertyName = "Registrar"
        Me.DataGridViewCheckBoxColumn3.HeaderText = "Regis- trar"
        Me.DataGridViewCheckBoxColumn3.Name = "DataGridViewCheckBoxColumn3"
        Me.DataGridViewCheckBoxColumn3.ReadOnly = True
        Me.DataGridViewCheckBoxColumn3.Width = 40
        '
        'OnlineSID
        '
        Me.OnlineSID.DataPropertyName = "OnlineSID"
        Me.OnlineSID.HeaderText = "Confirm#"
        Me.OnlineSID.Name = "OnlineSID"
        Me.OnlineSID.ReadOnly = True
        Me.OnlineSID.Width = 55
        '
        'dgvFinance2
        '
        Me.dgvFinance2.AllowUserToAddRows = False
        Me.dgvFinance2.AllowUserToDeleteRows = False
        Me.dgvFinance2.AllowUserToOrderColumns = True
        Me.dgvFinance2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvFinance2.AutoGenerateColumns = False
        DataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle11.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvFinance2.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle11
        Me.dgvFinance2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvFinance2.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colPaymentID, Me.DataGridViewTextBoxColumn29, Me.DataGridViewTextBoxColumn30, Me.DataGridViewTextBoxColumn31, Me.DataGridViewTextBoxColumn33, Me.DataGridViewTextBoxColumn34, Me.NumRegs})
        Me.dgvFinance2.DataMember = "tOrderLstPayments"
        Me.dgvFinance2.DataSource = Me.DsEventRegOrderTV2
        DataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle12.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvFinance2.DefaultCellStyle = DataGridViewCellStyle12
        Me.dgvFinance2.Location = New System.Drawing.Point(5, 35)
        Me.dgvFinance2.Name = "dgvFinance2"
        Me.dgvFinance2.ReadOnly = True
        DataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle13.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle13.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle13.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle13.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle13.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle13.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvFinance2.RowHeadersDefaultCellStyle = DataGridViewCellStyle13
        Me.dgvFinance2.RowHeadersWidth = 15
        Me.dgvFinance2.Size = New System.Drawing.Size(679, 108)
        Me.dgvFinance2.TabIndex = 6
        '
        'colPaymentID
        '
        Me.colPaymentID.DataPropertyName = "PaymentID"
        Me.colPaymentID.HeaderText = "PaymentID"
        Me.colPaymentID.Name = "colPaymentID"
        Me.colPaymentID.ReadOnly = True
        Me.colPaymentID.Visible = False
        '
        'DataGridViewTextBoxColumn29
        '
        Me.DataGridViewTextBoxColumn29.DataPropertyName = "AmountPd"
        Me.DataGridViewTextBoxColumn29.HeaderText = "Amount"
        Me.DataGridViewTextBoxColumn29.Name = "DataGridViewTextBoxColumn29"
        Me.DataGridViewTextBoxColumn29.ReadOnly = True
        Me.DataGridViewTextBoxColumn29.Width = 50
        '
        'DataGridViewTextBoxColumn30
        '
        Me.DataGridViewTextBoxColumn30.DataPropertyName = "PaymentMethod"
        Me.DataGridViewTextBoxColumn30.HeaderText = "Method"
        Me.DataGridViewTextBoxColumn30.Name = "DataGridViewTextBoxColumn30"
        Me.DataGridViewTextBoxColumn30.ReadOnly = True
        Me.DataGridViewTextBoxColumn30.Width = 130
        '
        'DataGridViewTextBoxColumn31
        '
        Me.DataGridViewTextBoxColumn31.DataPropertyName = "DtPayment"
        Me.DataGridViewTextBoxColumn31.HeaderText = "Date"
        Me.DataGridViewTextBoxColumn31.Name = "DataGridViewTextBoxColumn31"
        Me.DataGridViewTextBoxColumn31.ReadOnly = True
        Me.DataGridViewTextBoxColumn31.Width = 75
        '
        'DataGridViewTextBoxColumn33
        '
        Me.DataGridViewTextBoxColumn33.DataPropertyName = "PayerName"
        Me.DataGridViewTextBoxColumn33.HeaderText = "Payer Name"
        Me.DataGridViewTextBoxColumn33.Name = "DataGridViewTextBoxColumn33"
        Me.DataGridViewTextBoxColumn33.ReadOnly = True
        '
        'DataGridViewTextBoxColumn34
        '
        Me.DataGridViewTextBoxColumn34.DataPropertyName = "PaymentType"
        Me.DataGridViewTextBoxColumn34.HeaderText = "Type"
        Me.DataGridViewTextBoxColumn34.Name = "DataGridViewTextBoxColumn34"
        Me.DataGridViewTextBoxColumn34.ReadOnly = True
        '
        'NumRegs
        '
        Me.NumRegs.DataPropertyName = "NumRegs"
        Me.NumRegs.HeaderText = "#Regs"
        Me.NumRegs.Name = "NumRegs"
        Me.NumRegs.ReadOnly = True
        Me.NumRegs.Width = 50
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Comic Sans MS", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.Color.RosyBrown
        Me.Label11.Location = New System.Drawing.Point(16, 16)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(89, 16)
        Me.Label11.TabIndex = 2
        Me.Label11.Text = "FINANCIALS"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Comic Sans MS", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.RosyBrown
        Me.Label10.Location = New System.Drawing.Point(13, 152)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(88, 16)
        Me.Label10.TabIndex = 3
        Me.Label10.Text = "REGISTERED"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Comic Sans MS", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.RosyBrown
        Me.Label7.Location = New System.Drawing.Point(13, 360)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(66, 16)
        Me.Label7.TabIndex = 5
        Me.Label7.Text = "PENDING"
        '
        'PaymentID
        '
        Me.PaymentID.DataPropertyName = "PaymentID"
        Me.PaymentID.HeaderText = "PaymentID"
        Me.PaymentID.Name = "PaymentID"
        Me.PaymentID.ReadOnly = True
        Me.PaymentID.Visible = False
        '
        'OID
        '
        Me.OID.DataPropertyName = "OID"
        Me.OID.HeaderText = "OID"
        Me.OID.Name = "OID"
        Me.OID.ReadOnly = True
        Me.OID.Visible = False
        '
        'PaymentType
        '
        Me.PaymentType.DataPropertyName = "PaymentType"
        Me.PaymentType.HeaderText = "PaymentType"
        Me.PaymentType.Name = "PaymentType"
        Me.PaymentType.ReadOnly = True
        '
        'PaymentMethod
        '
        Me.PaymentMethod.DataPropertyName = "PaymentMethod"
        Me.PaymentMethod.HeaderText = "PaymentMethod"
        Me.PaymentMethod.Name = "PaymentMethod"
        Me.PaymentMethod.ReadOnly = True
        '
        'AmountPd
        '
        Me.AmountPd.DataPropertyName = "AmountPd"
        Me.AmountPd.HeaderText = "AmountPd"
        Me.AmountPd.Name = "AmountPd"
        Me.AmountPd.ReadOnly = True
        Me.AmountPd.Width = 75
        '
        'DtPayment
        '
        Me.DtPayment.DataPropertyName = "DtPayment"
        Me.DtPayment.HeaderText = "DtPayment"
        Me.DtPayment.Name = "DtPayment"
        Me.DtPayment.ReadOnly = True
        Me.DtPayment.Width = 75
        '
        'PayerName
        '
        Me.PayerName.DataPropertyName = "PayerName"
        Me.PayerName.HeaderText = "PayerName"
        Me.PayerName.Name = "PayerName"
        Me.PayerName.ReadOnly = True
        '
        'btNewReg
        '
        Me.btNewReg.Location = New System.Drawing.Point(264, 414)
        Me.btNewReg.Name = "btNewReg"
        Me.btNewReg.Size = New System.Drawing.Size(101, 41)
        Me.btNewReg.TabIndex = 44
        Me.btNewReg.Text = "Add Registration to this Order"
        Me.btNewReg.UseVisualStyleBackColor = True
        '
        'getEventRegistrantList2TableAdapter
        '
        Me.getEventRegistrantList2TableAdapter.ClearBeforeFill = True
        '
        'tOrderLstTableAdapter
        '
        Me.tOrderLstTableAdapter.ClearBeforeFill = True
        '
        'TOrderLstPendingTableAdapter
        '
        Me.TOrderLstPendingTableAdapter.ClearBeforeFill = True
        '
        'TOrderLstRegistrationsTableAdapter
        '
        Me.TOrderLstRegistrationsTableAdapter.ClearBeforeFill = True
        '
        'TOrderLstPaymentsTableAdapter
        '
        Me.TOrderLstPaymentsTableAdapter.ClearBeforeFill = True
        '
        'cboDates
        '
        Me.cboDates.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboDates.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboDates.FormattingEnabled = True
        Me.cboDates.Items.AddRange(New Object() {"Upcoming Events", "All Events"})
        Me.cboDates.Location = New System.Drawing.Point(3, 6)
        Me.cboDates.Name = "cboDates"
        Me.cboDates.RestrictContentToListItems = True
        Me.cboDates.Size = New System.Drawing.Size(146, 21)
        Me.cboDates.TabIndex = 0
        Me.cboDates.Tag = "Upcoming/All Dates"
        Me.cboDates.Text = "Time span"
        '
        'cboRegion
        '
        Me.cboRegion.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboRegion.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboRegion.ItemHeight = 13
        Me.cboRegion.Location = New System.Drawing.Point(3, 33)
        Me.cboRegion.Name = "cboRegion"
        Me.cboRegion.RestrictContentToListItems = True
        Me.cboRegion.Size = New System.Drawing.Size(146, 21)
        Me.cboRegion.TabIndex = 1
        Me.cboRegion.Tag = "SatelliteRegion"
        Me.cboRegion.Text = "Regions"
        Me.ToolTip1.SetToolTip(Me.cboRegion, "SatelliteRegion where Event will be held.")
        '
        'cboSpecific
        '
        Me.cboSpecific.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboSpecific.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboSpecific.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cboSpecific.ItemHeight = 13
        Me.cboSpecific.Items.AddRange(New Object() {"", "-----EVENTS-----", "Event Name", "Presenter Name", "Year", "-----SINGLE EVENT-----", "Event #", "Online NID #", "Online SKU"})
        Me.cboSpecific.Location = New System.Drawing.Point(13, 380)
        Me.cboSpecific.Name = "cboSpecific"
        Me.cboSpecific.RestrictContentToListItems = True
        Me.cboSpecific.Size = New System.Drawing.Size(151, 21)
        Me.cboSpecific.TabIndex = 4
        Me.cboSpecific.Tag = "Search Specific Event"
        Me.cboSpecific.Text = "Select field to search..."
        '
        'cboRetired
        '
        Me.cboRetired.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboRetired.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboRetired.FormattingEnabled = True
        Me.cboRetired.Items.AddRange(New Object() {"", "CMGI", "SSGI", "LTGI", "TMGI", "Center PR", "Congregational Connection", "GoodWords/GoodFlicks", "Invitational Gathering"})
        Me.cboRetired.Location = New System.Drawing.Point(17, 488)
        Me.cboRetired.Name = "cboRetired"
        Me.cboRetired.RestrictContentToListItems = True
        Me.cboRetired.Size = New System.Drawing.Size(142, 21)
        Me.cboRetired.TabIndex = 6
        Me.cboRetired.Tag = "Old Topics"
        '
        'cboProposedPayment
        '
        Me.cboProposedPayment.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboProposedPayment.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboProposedPayment.Enabled = False
        Me.cboProposedPayment.FormattingEnabled = True
        Me.cboProposedPayment.Items.AddRange(New Object() {"cash", "check", "credit", "free_order"})
        Me.cboProposedPayment.Location = New System.Drawing.Point(159, 555)
        Me.cboProposedPayment.Name = "cboProposedPayment"
        Me.cboProposedPayment.RestrictContentToListItems = True
        Me.cboProposedPayment.Size = New System.Drawing.Size(131, 21)
        Me.cboProposedPayment.TabIndex = 43
        Me.cboProposedPayment.Visible = False
        '
        'cboOrderStatus
        '
        Me.cboOrderStatus.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboOrderStatus.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboOrderStatus.Enabled = False
        Me.cboOrderStatus.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboOrderStatus.FormattingEnabled = True
        Me.cboOrderStatus.Location = New System.Drawing.Point(159, 482)
        Me.cboOrderStatus.Name = "cboOrderStatus"
        Me.cboOrderStatus.RestrictContentToListItems = True
        Me.cboOrderStatus.Size = New System.Drawing.Size(131, 21)
        Me.cboOrderStatus.TabIndex = 40
        Me.cboOrderStatus.Visible = False
        '
        'frmSrchEvent
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(1094, 580)
        Me.Controls.Add(Me.fldEventSKU)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.fldRegID)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.fldEventNID)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.fldEventID)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnHelp)
        Me.Controls.Add(Me.StatusBar1)
        Me.Controls.Add(Me.TabControl1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Menu = Me.MainMenu1
        Me.Name = "frmSrchEvent"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Tag = "SrchEvent"
        Me.Text = "W. FIND EVENTS"
        CType(Me.StatusBarPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.StatusBarPanelID, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.StatusBarPanel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.pnlRegion.ResumeLayout(False)
        Me.grpType.ResumeLayout(False)
        Me.grpType.PerformLayout()
        CType(Me.DsSrchWEvent1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel4.ResumeLayout(False)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.grdvwMain, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdSecond1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsGetEventRegList2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabControl1.ResumeLayout(False)
        Me.pgEvent.ResumeLayout(False)
        Me.pgOrder.ResumeLayout(False)
        Me.SplitContainer.Panel1.ResumeLayout(False)
        Me.SplitContainer.Panel1.PerformLayout()
        Me.SplitContainer.Panel2.ResumeLayout(False)
        Me.SplitContainer.Panel2.PerformLayout()
        CType(Me.SplitContainer, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer.ResumeLayout(False)
        CType(Me.dgvPending2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsEventRegOrderTV2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvReg2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvFinance2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

#Region "Load"

    'LOAD DEFAULTS
    Protected Sub frmSrchEvent_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles Me.Load

        MouseWait()
        If usr = DBAdmin.StaffID Then   '213 Then 'catharine can edit in grid
            Me.grdSecond1.ReadOnly = False
        End If
        cmd1.Connection = sc

        If usr = RegonlineAdmin.StaffID Or usr = DBAdmin.StaffID Then
            Me.fldEventNID.ReadOnly = False 'Enabled = True
            Me.fldEventSKU.ReadOnly = False
        End If

        strDGM = " EVENTS "
        strDGS1 = " REGISTRATIONS "
        strForm = "frmSrchEvent"
        strKey = "SrchEvent"
        strOpenKey = "SrchEvent"

        Me.StatusBarPanel1.Text = "loading regions"
        modGlobalVar.LoadRegionCombo(Me.cboRegion, "All")
        Me.cboSpecific.SelectedIndex = -1  '.Text = "Other Search..."

        Dim qryFill As New SqlCommand("SELECT StatusName   FROM luStatus WHERE  (Topic = 'order') AND (Selectable = 1) AND (Ball IS NULL) ORDER BY SortNum", sc)
        qryFill.CommandType = System.Data.CommandType.Text
        modGlobalVar.LoadDataTable(tblOrderStatus, qryFill)

        Me.StatusBarPanel1.Text = "setting dataview"
GetColIndex:
        iColOrderNum = Me.grdSecond1.TableStyles("GetEventRegistrantList2").GridColumnStyles.IndexOf(CType(Me.grdSecond1.TableStyles("GetEventRegistrantList2").GridColumnStyles("OrderNum"), DataGridColumnStyle))
        iColPaymentNum = Me.grdSecond1.TableStyles("GetEventRegistrantList2").GridColumnStyles.IndexOf(CType(Me.grdSecond1.TableStyles("GetEventRegistrantList2").GridColumnStyles("PaymentNum"), DataGridColumnStyle))

        '  SET DATATABLE VARIABLE
        dv = New DataView(Me.DsSrchWEvent1.SrchWEvents) 'dsSrchEdEvents1.SrchEdEvents)
        '  SET DATAView VARIABLE FOR FILTERING DATAGRIDS
        dvM = New DataView(Me.DsSrchWEvent1.SrchWEvents) 'Me.dsSrchEdEvents1.SrchEdEvents)
        dvS1 = New DataView(Me.DsGetEventRegList2.getEventRegistrantList2) 'Me.dsGetEventRegList21.tblGetWEventRegistrants)
        dvS1.RowFilter = "Cancelled = 0"

        Dim tbx As DataGridTextBoxColumn
        Dim obj As Object
        For Each obj In Me.grdSecond1.TableStyles(0).GridColumnStyles
            If TypeOf obj Is DataGridTextBoxColumn Then
                tbx = DirectCast(obj, DataGridTextBoxColumn)
                tbx.TextBox.Enabled = False
                tbx.NullText = ""
            Else
            End If
        Next obj

        Try 'needs delay?
            dvPay = New DataView(Me.DsEventRegOrderTV2.tOrderLstPayments) '.DefaultView)
            dvReg = New DataView(Me.DsEventRegOrderTV2.tOrderLstRegistrations)
            dvPending = New DataView(Me.DsEventRegOrderTV2.tOrderLstPendings)
        Catch ex As Exception
            modGlobalVar.msg("ERROR: setting dataviews", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        isLoaded = True
        qryFill = Nothing
        Me.cboDates.SelectedIndex = 0

        Me.StatusBarPanel1.Text = "Done"
        Forms.Add(Me)
        MouseDefault()
    End Sub 'load

#End Region 'load

#Region "WORKSHOP-TAB EVENTS" '****************************

#Region "SearchEvents"

    'DO SEARCH: fill result grid based on user choices or defaults
    Protected Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles miSearch.Click, btnSearch.Click
        Dim par As String 'SqlClient.SqlParameter
        Dim r As Integer

        '1 SET STATUS BAR & CURSOR HOURGLASS
        Me.StatusBar1.Panels(0).Text = "Searching..."
        MouseWait()

A_Reset:
        Me.grdvwMain.CurrentCell = Nothing 'why does this do nothing?
        ClearDefaults() 'why now causing fatal error? 7/16
        ClearGrids()
        '2_CLEAR EXISTING DATA, SET DEFAULT PARAMETERS
        Me.DsSrchWEvent1.Clear()
        Me.grdvwMain.Refresh()
        cmd1.Parameters("@Title").Value = Nothing
        cmd1.Parameters("@Instructor").Value = Nothing
        cmd1.Parameters("@Yr").Value = 0
        cmd1.Parameters("@Case").Value = Nothing

B_SingleSearches:
        Select Case WhatField   '   returns single event or order; ignore other params
            Case "Event #"
                Me.cmd1.Parameters("@Fld").Value = "Event"
            Case "Online NID #"
                Me.cmd1.Parameters("@Fld").Value = "NID"
            Case "Confirmation #"
                Me.cmd1.Parameters("@Fld").Value = "SID"
            Case "Order #"
                Me.cmd1.Parameters("@Fld").Value = "Order"
            Case "Online SKU"
                Me.cmd1.Parameters("@Fld").Value = "SKU"
                Me.cmd1.Parameters("@Title").Value = modGlobalVar.GetWild(Me.txtSearch.Text, Me.chkWild.CheckState)
                GoTo FillDataset
            Case Else
                Me.cmd1.Parameters("@Fld").Value = DBNull.Value
        End Select
        Select Case WhatField
            Case "Event #", "Online NID #", "Confirmation #", "Order #"
                cmd1.Parameters("@IDNum").Value = Me.txtSearch.Text
                GoTo FillDataset
        End Select

C_SetParamsforMultipleResultSets:
        'SET DATE Range
        If Me.cboDates.SelectedItem = "All Events" Then
            cmd1.Parameters("@dte").Value = "1/1/1991"
        Else
            cmd1.Parameters("@dte").Value = DateTime.Today.AddDays(-10)
        End If
        cmd1.Parameters("@Region").Value = Me.cboRegion.SelectedItem
        'SET TYPE
        cmd1.Parameters("@case").Value = Nothing

SetParams:
        If Me.cboSpecific.SelectedIndex > 0 Then
            If WhatField = "Year" Then
                cmd1.Parameters("@Yr").Value = Me.txtSearch.Text
            Else
                'ADD WILDCARD
                Select Case WhatField
                    Case "Event Name" : par = "@Title"
                    Case "Presenter Name" : par = "@Instructor"
                    Case "Series/Case Name" : par = "@Case"
                    Case Else
                        modGlobalVar.msg("ERROR: Whatfield", WhatField, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Exit Sub
                End Select
                cmd1.Parameters(par).Value = modGlobalVar.GetWild(Me.txtSearch.Text, Me.chkWild.CheckState)
            End If
        Else
        End If

FillDataset:
        '5 FILL DATASET
        RemoveHandler Me.grdvwMain.CurrentCellChanged, AddressOf grdvwMain_currentcellchanged
        Try
            If Not SCConnect() Then
                Exit Sub
            End If
            Me.DsSrchWEvent1.EnforceConstraints = False
            Me.DsSrchWEvent1.SrchWEvents.Load(cmd1.ExecuteReader(CommandBehavior.CloseConnection))
            r = Me.DsSrchWEvent1.SrchWEvents.Rows.Count

        Catch exc As System.FormatException
            modGlobalVar.msg(MsgCodes.invalidSearch) ', "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Me.StatusBarPanel1.Text = "event grid fill error"
            ' sc.Close()
            Exit Sub
        Catch exc As Exception
            modGlobalVar.msg("ERROR: filling Event Grid", exc.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
            '    sc.Close()
            Exit Sub
        Finally
            sc.Close()
        End Try

        Me.grdvwMain.DataSource = dvM
        '    STATUS BAR; INCLUDE COUNT OF FOUND ITEMS; RESET CURSOR; RESET MENU ITEMS
        If r > 0 Then
        Else
            Me.lblMainGrid.Text = "NO MATCHING " & strDGM & " FOUND"
        End If
        AddHandler Me.grdvwMain.CurrentCellChanged, AddressOf Me.grdvwMain_currentcellchanged
        SrchTotal = GetCount()
        Me.StatusBar1.Panels(0).Text = "Done"
        Me.btnSearch.BackColor = Color.FromKnownColor(KnownColor.Control)
        MouseDefault()
    End Sub

    Private Sub ClearDefaults()
        resetRBAllTypes()
        WhatType = "Upcoming"
    End Sub

    'select cboRetired GOTO DO SEARCH
    Private Sub cboRetired_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles cboRetired.SelectedIndexChanged
        If isLoaded Then
            If sender.selectedindex > 0 Then
                WhatType = sender.text 'sender.name.substring(2, Len(sender.name) - 2)
                grdFilterByType() 'Me.DsSrchWEvent1.SrchWEvents, dvM)
            End If
        End If
    End Sub

    'Changed search cbo GOTO DO SEARCH
    Private Sub cboDates_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) _
    Handles cboDates.SelectedIndexChanged, cboRegion.SelectedIndexChanged
        If isLoaded Then
            miSearch.PerformClick()
        End If
    End Sub

    'VISIBLE/INVISIBLE ITEMS based on cboSPECIFIC 
    Protected Sub cboSpecific_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles cboSpecific.SelectedIndexChanged
        '  Dim isValid As Boolean = modGlobalVar.NewValidateCombo(sender, False)

        If isLoaded = True Then 'And isValid = True Then
        Else
            Me.grpType.Visible = True
            Me.pnlRegion.Visible = True
            Exit Sub
        End If
        Try
            Select Case cboSpecific.SelectedIndex
                Case Is < 1    'no selection, filters ok
                    Me.grpType.Visible = True
                    Me.pnlRegion.Visible = True
                    Me.txtSearch.Visible = False ' .text = "Type search text here."
                    dvM.RowFilter = ""
                    miClear.PerformClick()
                    WhatField = "Upcoming"
                    Me.btnSearch.PerformClick()
                    Exit Sub
                Case Is < 5    'user chose potential multiple return, filters ok
                    WhatField = cboSpecific.SelectedItem
                    Me.grpType.Visible = True
                    Me.pnlRegion.Visible = True
                Case Else 'user chose single result set, hide filters
                    WhatField = cboSpecific.SelectedItem
                    Me.grpType.Visible = False
                    Me.pnlRegion.Visible = False
            End Select

        Catch ex As Exception
            modGlobalVar.msg("ERROR: cboSpecific", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        If Me.cboSpecific.Text.Contains("#") Then
            Me.chkWild.Visible = False
        End If
        Me.txtSearch.Visible = True
        Me.txtSearch.Focus()
    End Sub

    'call DO SEARCH
    Private Sub txtSearch_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles txtSearch.KeyDown
        If e.KeyData.ToString = "Return" Then
            Me.btnSearch_Click(Me, Nothing)
        End If
    End Sub

    'Leave txtSearch GOTO DO SEARCH
    Private Sub txtSearch_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSearch.Leave
        If Me.txtSearch.Text = "Type search text here." Or Me.txtSearch.Text = String.Empty Or Me.cboSpecific.Text = String.Empty Then
        Else
            Me.btnSearch_Click(Me, Nothing)
        End If

    End Sub

    'grid error
    Private Sub grdvwMain_DataError(sender As System.Object, e As System.Windows.Forms.DataGridViewDataErrorEventArgs) Handles grdvwMain.DataError
        MsgBox(e.Exception.Message)
    End Sub

#End Region 'search Events

    'TODO ADD flag for registration full
#Region "Open Secondary event Forms"

    'OPEN EVENT DETAIL
    Protected Sub btnOpenMain_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles mmiGoto.Click, miGotoEvent.Click, grdvwMain.DoubleClick
        MouseWait()
        Try
            Dim row As DataGridViewRow = Me.grdvwMain.CurrentRow
            modGlobalVar.OpenMainWEvent(row.Cells("EventID" & grdvwColName).Value, UCase(row.Cells("EventName" & grdvwColName).Value) & " " & row.Cells("Instructor" & grdvwColName).Value, False)
            'old datagrid:     modGlobalVar.OpenMainWEvent(Me.grdMain.Item(Me.grdMain.CurrentRowIndex, 0), UCase(Me.grdMain.Item(Me.grdMain.CurrentRowIndex, 2) & " " & Me.grdMain.Item(Me.grdMain.CurrentRowIndex, 3)), False)
        Catch ex As Exception
            modGlobalVar.msg("ERROR: open EventDetail", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Me.StatusBar1.Panels(0).Text = "Done."
        MouseDefault()
    End Sub

    'OPEN REGISTRATION DETAIL from SECONDARY GRID
    Protected Sub OpenSecondary(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles grdSecond1.DoubleClick, miGotoRegistration.Click
        If Me.grdSecond1.CurrentRowIndex > -1 Then
            Me.StatusBarPanel1.Text = "Opening Registration Detail Window"
            modGlobalVar.OpenMainWReg2(Me.grdSecond1.Item(Me.grdSecond1.CurrentRowIndex, 0), "Registration for: " & Me.grdSecond1.Item(Me.grdSecond1.CurrentRowIndex, 3).ToString, False, "Registrant")
            Me.StatusBarPanel1.Text = "Done"
        End If
    End Sub

    'OPEN REGISTRATION DETAIL for all registrations at that event
    Private Sub miViewAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles miViewAll.Click

        If ThisID > 0 Then
        Else
            modGlobalVar.msg(MsgCodes.noRowSelected) '"ATTENTION - no event selected", "Select an event and try again", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            GoTo CloseAll
        End If

        MouseWait()
        If Me.grdvwMain.CurrentRow.Cells("TotRegistered" & grdvwColName).Value > 0 Then
            modGlobalVar.OpenMainWReg2(Me.grdvwMain.CurrentRow.Cells("EventID" & grdvwColName).Value, "All Registrations for: " & Me.grdvwMain.CurrentRow.Cells("EventName" & grdvwColName).Value, False, "Event")
        Else
            modGlobalVar.msg(MsgCodes.noResultCancel) ', "", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
CloseAll:
        Me.StatusBarPanel1.Text = "Done"
        MouseDefault()
    End Sub

#End Region  'open secondary event forms

#Region "Event Registration Grid"

    'CAPTURE RIGHT MOUSE CLICK TO FILTER MAIN DATAGRIDVIEW
    Protected Sub grdvwMain_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) _
            Handles grdvwMain.MouseDown

        Dim oldCnt As Integer = Me.grdvwMain.Rows.Count
        Dim str() As String = modGlobalVar.FilterDataGridView(sender, e, Me.DsSrchWEvent1.SrchWEvents, False)

        If str(1) = String.Empty Or str(1) = "LEFT" Then
            lblMainGrid.Text = GetCount() & " EVENTS" 'str(0)
            Exit Sub
        Else
            lblMainGrid.Text = str(0) & "/" & SrchTotal.ToString & str(1)

            If Me.chkDetail.Checked Then
                ClearSecGrids()    'clear child grids 
            End If
        End If
        bindingClick.DataSource = Me.grdvwMain.DataSource
        SetDGVHeading(str(0), oldCnt, str(1))

        'clearing filter so reset radio buttons
        If e.Button = System.Windows.Forms.MouseButtons.Right Then
            resetRBAllTypes()
        End If
        Me.grdvwMain.ClearSelection()
    End Sub

    'Changed RB Event Type 
    Private Sub rb_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles rbConference.CheckedChanged, rbGrant.CheckedChanged, rbHosted.CheckedChanged, rbLongterm.CheckedChanged, rbSponsored.CheckedChanged, rbWorkshop.CheckedChanged, rbEvaluation.CheckedChanged, rbRenewal.CheckedChanged, rbLiveOnline.CheckedChanged
        If Not isLoaded Or sender.checked = False Then
            Exit Sub
        End If
        Dim oldCnt As Integer

        If IsNothing(bindingClick.Filter) Then
            oldCnt = Me.DsSrchWEvent1.SrchWEvents.Rows.Count 'Me.grdvwMain.Rows.Count
        Else
            oldCnt = bindingClick.Count
        End If

        WhatType = sender.text
        grdFilterByType() 'Me.DsSrchWEvent1.SrchWEvents, dvM)

        SetDGVHeading(Me.grdvwMain.Rows.Count, oldCnt, String.Empty) 'bindingRadioBtn.Count, oldCnt)

    End Sub

    'FILTER GRID METHOD maingridvw called by event type rb buttons
    Private Sub grdFilterByType() 'ByVal tbl As DataTable, ByVal dv As DataView)
        'filter on main table or on already filtered results from rclick
        Dim strFilter As String

        bindingRadioBtn.DataSource = dvM
        strFilter = "TypeofEvent = '" & WhatType & "'"
        bindingRadioBtn.Filter = strFilter

        Me.grdvwMain.DataSource = bindingRadioBtn
        strFilter = String.Empty

    End Sub

    'MAIN GRID HEADING
    Private Sub SetDGVHeading(ByVal iNew As Integer, ByVal iOld As Integer, ByVal str As String)

        If str = String.Empty Then
            Me.lblMainGrid.Text = iNew.ToString & " / " & iOld.ToString & " Filtered on " & IsNull(WhatType.Replace("Upcoming", ""), " ")
        Else
            Me.lblMainGrid.Text = iNew.ToString & " / " & iOld.ToString & " " & str & "  " & IsNull(WhatType, " ")
        End If
    End Sub

    'Changed RB Event Type GOTO DO SEARCH
    Private Sub rbAllTypes_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles rbAllTypes.CheckedChanged
        If Not isLoaded Or sender.checked = False Then
            Exit Sub
        End If
        Me.btnSearch.PerformClick()
    End Sub

    'temporarily remove handler
    Private Sub resetRBAllTypes()
        RemoveHandler Me.rbAllTypes.CheckedChanged, AddressOf Me.rbAllTypes_CheckedChanged
        Me.rbAllTypes.Checked = True
        AddHandler Me.rbAllTypes.CheckedChanged, AddressOf Me.rbAllTypes_CheckedChanged
    End Sub

    'FILTER GRID METHOD secondary grid
    Private Sub grdFilter(ByVal grd As Object, ByVal ds As Object, ByVal tbl As Object, ByVal strHdr As String, ByVal dv As DataView)
        Dim strFilter As String
        Dim strHeading As String
        Dim myColumns As GridColumnStylesCollection
        Dim strHTIValue, strHTIColumn As String   'for filter

        'CLEAR EXISTING FILTERS
        grdFilterClear()

        'SET FILTER
        If hti Is Nothing Then  '.ToString = String.Empty Then 'If IsNull(hti.Row, -2) < 0 Then
        ElseIf hti.Row > -1 Then
            myColumns = grd.TableStyles(0).GridColumnStyles
            strHTIValue = grd.Item(hti.Row, hti.Column)
            strHTIColumn = myColumns(hti.Column).MappingName
        End If
        If strHTIValue = Nothing Then
        Else
            strFilter = strHTIColumn & " = '" & strHTIValue & "'"
            strHeading = strHTIColumn
        End If

        dv.RowFilter = strFilter

        'DISPLAY HEADINGS
        grd.DataSource = dv
        grd.CaptionText = dv.Count.ToString & " / " & iValidReg.ToString & "  " & strHdr
        SetStatusBarText(strHdr & " filtered on " & strHeading) 'myColumns(hti.Column).HeaderText)
        strHeading = Nothing
        strFilter = ""
        hti = Nothing
    End Sub

    'REMOVE MAIN GRID FILTER
    Private Sub lblMainGrid_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) _
        Handles lblMainGrid.MouseClick

        If e.Button = System.Windows.Forms.MouseButtons.Right Then
            Me.DsSrchWEvent1.SrchWEvents.DefaultView.RowFilter = ""
            Me.grdvwMain.Refresh()
            Me.lblMainGrid.Text = Me.DsSrchWEvent1.SrchWEvents.Rows.Count.ToString & "  " & strDGM
            iGrid = Me.DsSrchWEvent1.SrchWEvents.Rows.Count
            resetRBAllTypes()
            WhatType = String.Empty
            Me.grdvwMain.ClearSelection()
        End If
    End Sub

    'count distinct events in main grid re filter count display
    Private Function GetCount() As Integer
        Dim tDistinct As New DataTable("tDistinct")
        tDistinct = Me.DsSrchWEvent1.SrchWEvents.DefaultView.ToTable(True, "EventID")
        GetCount = tDistinct.Rows.Count
        tDistinct = Nothing
    End Function

    'SECONDARY GRID FILTER CAPTURE RIGHT MOUSE CLICK 
    Protected Sub grdAll_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) _
            Handles grdSecond1.MouseDown
        Dim tbl As Object
        Dim strHdr As String    'text for grid header
        Dim pp As New ContextMenu
        Dim eh As EventHandler = AddressOf ppRightClickMenu

        'set variables
        Select Case sender.name
            Case Is = "grdSecond1"
                tbl = Me.DsGetEventRegList2.getEventRegistrantList2 'GetWEventRegistrants
                strHdr = strDGS1
            Case Else
                tbl = Me.DsSrchWEvent1.SrchWEvents
                strHdr = strDGM
        End Select

        If e.Button = System.Windows.Forms.MouseButtons.Right Then
            hti = sender.HitTest(e.X, e.Y)

            'call filter method
            If hti.Type = DataGrid.HitTestType.Cell Then
                If IsDBNull(sender.Item(hti.Row, hti.Column)) Then 'nulls cause filter error
                    Exit Sub
                Else
                    Select Case sender.name
                        Case Is = "grdSecond1"
                            pp.MenuItems.Add("Filter", eh)
                            If Me.grdSecond1.Item(Me.grdSecond1.CurrentRowIndex, iColOrderNum).ToString > "" Then
                                pp.MenuItems.Add("Order Detail", eh)
                            End If
                            If Me.grdSecond1.Item(Me.grdSecond1.CurrentRowIndex, iColPaymentNum).ToString > "" Then
                                pp.MenuItems.Add("Payment Detail", eh)
                            End If
                            pp.MenuItems.Add("Registration Detail", eh)
                            pp.Show(Me, PointToClient(Control.MousePosition))
                    End Select

                End If
            Else
                '    'RESET CAPTION TOTAL
                If sender.name = "grdvwMain" Then
                    sender.DataSource = tbl
                    Me.lblMainGrid.Text = Me.DsSrchWEvent1.SrchWEvents.Count.ToString & "  " & strHdr 'DsOrgSearch1.tblOrg.Rows.Count.ToString
                Else
                    dvS1.RowFilter = "Cancelled = 0"
                    ' sender.datasource = dvS1
                    sender.captiontext = iValidReg.ToString & "  " & strHdr
                End If
                Me.StatusBar1.Text = ""
                Select Case strHdr
                    Case Is = strDGS1
                        statusS1 = ""
                End Select
                SetStatusBarText(statusM & " " & statusS1)
            End If
        Else
        End If
        pp = Nothing
    End Sub

    'go directly to order or payment detail, or filter as usual
    Private Sub ppRightClickMenu(ByVal obj As Object, ByVal ea As EventArgs)

        Select Case obj.text
            Case Is = "Filter"
                grdFilter(Me.grdSecond1, Me.DsGetEventRegList2, Me.DsGetEventRegList2.getEventRegistrantList2, strDGS1, dvS1)

            Case Is = "Order Detail"
                modGlobalVar.OpenMainWOrder(Me.grdSecond1.Item(Me.grdSecond1.CurrentRowIndex, iColOrderNum), False)

            Case Is = "Payment Detail" 'payment id not a column
                modGlobalVar.OpenMainWPayment(Me.grdSecond1.Item(Me.grdSecond1.CurrentRowIndex, iColPaymentNum))

            Case Is = "Registration Detail"
                modGlobalVar.OpenMainWReg2(Me.grdSecond1.Item(Me.grdSecond1.CurrentRowIndex, 0), "Registration for: " & Me.grdSecond1.Item(Me.grdSecond1.CurrentRowIndex, 3).ToString, False, "Registrant")
        End Select

    End Sub

    'CLEAR CHILD FILTER METHOD when main grid changes
    Private Sub grdFilterClear()
        dvM.RowFilter = ""
        Me.grdSecond1.DataSource = Me.DsGetEventRegList2.getEventRegistrantList2 '.tblGetWEventRegistrants

        statusS1 = ""
        statusS2 = ""
        SetStatusBarText(statusM)

    End Sub

    'CALL DISPLAY IDs and FILL SECONDARY GRIDS
    Protected Sub grdvwMain_currentcellchanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles grdvwMain.CurrentCellChanged 'SelectionChanged = gets old id first'currentcellchanged
        If Not isLoaded Then
            Exit Sub
        End If
        MouseWait()

        If grdvwMain.CurrentCell Is Nothing Then
            ThisID = 0
        Else
            'RESET ThisID
            ThisID = grdvwMain.CurrentRow.Cells("EventID" & grdvwColName).Value
            'RELOAD SECONDARY GRIDS
        End If

        If ThisID > 0 Then
            If Me.chkDetail.Checked = True Then
                LoadSecondary()
            Else
            End If
        End If

        'LOAD DISPLAY IDs
        DisplayIDs()
        MouseDefault()
    End Sub

    'DISPLAY CURRENT REGISTRATION ID
    Protected Sub grdSecond1_CurrentCellChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles grdSecond1.CurrentCellChanged
        If Not isLoaded Then
            Exit Sub
        End If
        MouseWait()
        'LOAD DISPLAY IDs
        If Me.grdSecond1.CurrentRowIndex >= 0 Then
            Me.fldRegID.Text = Me.grdSecond1.Item(IsNull(Me.grdSecond1.CurrentRowIndex, 0), 0)
        End If
        MouseDefault()
    End Sub

    'Set ID Indicator fields
    Private Sub DisplayIDs()

        SetStatusBarText("Setting SKU")
        If ThisID > 0 Then
            Me.fldEventID.Text = ThisID 'Me.grdvwMain.CurrentRow.Cells("EventID" & grdvwColName).Value ''Me.grdMain.CurrentRowIndex, 0)
            Me.fldEventSKU.Text = IsNull(Me.grdvwMain.CurrentRow.Cells("RegonlineNum" & grdvwColName).Value, "")
            Me.fldEventNID.Text = IsNull(Me.grdvwMain.CurrentRow.Cells("NIDEvent" & grdvwColName).Value, 0)
            Me.StatusBarPanelID.Text = "Event ID: " & ThisID
        Else
            Me.fldEventID.Text = String.Empty
            Me.fldEventSKU.Text = String.Empty
            Me.fldEventNID.Text = String.Empty
            Me.StatusBarPanelID.Text = "Event ID"
        End If

        SetStatusBarText("Done")
    End Sub

    'LOAD SECONDARY GRIDS
    Protected Sub LoadSecondary()
        If ThisID > 0 Then
        Else
            Exit Sub
        End If

        Dim sql As New SqlCommand("[getEventRegistrantList2]", sc)
        Me.DsGetEventRegList2.Clear() 'DsGetRegistrantsSrchEvent.Clear()
        Me.DsGetEventRegList2.EnforceConstraints = False
        sql.CommandType = CommandType.StoredProcedure
        sql.Parameters.Add("@EventID", SqlDbType.Int).Value = grdvwMain.CurrentRow.Cells("EventID" & grdvwColName).Value

        If Me.cboSpecific.Text = "Order #" Then
            Me.getEventRegistrantList2TableAdapter.Fill(DsGetEventRegList2.getEventRegistrantList2, Me.txtSearch.Text, "Orders")
        Else
            Me.getEventRegistrantList2TableAdapter.Fill(DsGetEventRegList2.getEventRegistrantList2, ThisID, "wEvents")
        End If

        'EXCLUDE CANCELLATIONS IN COUNT
        Dim cmdCount As New SqlCommand(modGlobalVar.CountValidRegistrations(ThisID, "Event"), sc) '("SELECT COUNT(registrationid) FROM (SELECT RegistrationID as RegNum FROM vwgetvalidwRegistrations  inner join tblEventReg2 on vvr.regnum = tblEventReg2.RegistrationID WHERE (EventNum = " & Me.grdMain.Item(grdMain.CurrentCell.RowNumber, 0) & " ) and (cancelled <>1) AND (Notes is null or notes not like 'Delete%')", sc)
        If Not SCConnect() Then
            GoTo CloseAll
        End If
        Try
            iValidReg = cmdCount.ExecuteScalar
        Catch ex As Exception
            modGlobalVar.msg("ERROR: executing mdGlobalVar.CountValidRegistrations", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            cmdCount = Nothing
        End Try
        If iValidReg = String.Empty Then
            iValidReg = "0 Registrations"
        End If
        Me.grdSecond1.CaptionText = iValidReg.ToString ' & "  " & strDGS1
        Me.grdSecond1.DataSource = dvS1

CloseAll:
        Try
            sc.Close()
        Catch ex As Exception
        End Try

    End Sub

    'HIDE SECONDARY GRIDS WHEN NOT REQUIRED
    Private Sub chkDetail_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles chkDetail.CheckedChanged

        If isLoaded And ThisID > 0 Then
            Me.grdSecond1.Visible = chkDetail.Checked
            If chkDetail.Checked = True Then
                Me.chkDetail.Text = "Hide Registrants for faster scrolling in Event grid."
                LoadSecondary()
            Else
                Me.chkDetail.Text = "Show Registrants for Event selected in Event grid."
            End If
        End If

    End Sub

    'CLEAR MAIN GRID
    Private Sub ClearGrids()
        Me.DsSrchWEvent1.Clear()
        Me.lblMainGrid.Text = "EVENTS"
        dvM.RowFilter = ""
        ClearSecGrids()
    End Sub

    'CLEAR SECONDARY GRIDS
    Private Sub ClearSecGrids()
        Me.DsGetEventRegList2.Clear()
        Me.grdSecond1.CaptionText = ""
        statusS1 = ""
        statusS2 = ""
        SetStatusBarText(statusM)
    End Sub

    'SET SEARCH CRITERIA TO DEFAULTS AND CLEAR DATASETS
    Protected Sub miClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles miClear.Click

        Dim ctl As Control
        Me.cboRegion.SelectedIndex = 0
        Me.cboSpecific.SelectedIndex = 0
        Me.txtSearch.Text = "Type search text here."
        Me.txtSearch.Visible = False
        For Each ctl In Me.grpType.Controls
            If TypeOf ctl Is RadioButton Then
                Dim rb As RadioButton = DirectCast(ctl, RadioButton)
                rb.Checked = False
            End If
        Next
        WhatField = "All Events"
        WhatType = "All Types"
        Me.rbAllTypes.Checked = True
        ClearGrids()
        Me.Refresh()
        dv.RowFilter = ""
        Me.StatusBarPanel1.Text = "Ready"
    End Sub

    'UNSELECT FROM DATAGRIDS AND LOSE FOCUS (note: selection stays with arrow)
    Protected Sub grdUnselect_Click(ByVal sender As Object, ByVal e As System.EventArgs) _
            Handles MyBase.Click
        Me.grdvwMain.ClearSelection()
        Me.grdvwMain.CurrentCell = Nothing
        ThisID = Nothing
        Me.StatusBarPanelID.Text = "Event ID"
        If grdSecond1.CurrentRowIndex > -1 Then
            Me.grdSecond1.UnSelect(grdSecond1.CurrentRowIndex)
            Me.grdSecond1.NavigateBack()
        End If
    End Sub

#End Region 'event registration grid

#End Region 'TAb EVENTS '********************

#Region "ORDER-TAB EVENTS" '********************

    'REFRESH PAYMENT GRID
    Private Sub RefreshPayments()
        Try
            Me.StatusBarPanel1.Text = "loading payments"
            Me.TOrderLstPaymentsTableAdapter.Fill(Me.DsEventRegOrderTV2.tOrderLstPayments)
        Catch ex As System.Exception
            modGlobalVar.msg("ERROR: loading payments  ", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#Region "Order Treeview"

    'BUILD TREE VIEW
    Public Sub LoadOrderTree(ByVal i As Integer, ByVal srchRegion As String) 'load order tree by User Region or all regions

        '9/13 change to load only user region
        Dim nd0Region, nd1Mnth As TreeNode
        Dim nd2Order As TreeNode
        Dim strHdg As String = "Start"
        Dim strRegion As String = "firstRegion"

        MouseWait()
        Me.StatusBarPanel1.Text = "Loading tree"
        Me.treevOrders.Nodes.Clear()
        Me.DsEventRegOrderTV2.EnforceConstraints = False
        Me.tOrderLstTableAdapter.Fill(Me.DsEventRegOrderTV2.tOrderLst, i, srchRegion)

BuildTreeView:
        For Each rO In Me.DsEventRegOrderTV2.tOrderLst.Rows
            Try
                If rO("Satelliteregion") = strRegion Then   'same region as last entry
                Else 'add region node
                    nd0Region = Me.treevOrders.Nodes.Add(rO("SatelliteRegion"), rO("SatelliteRegion"))
                    strRegion = rO("SatelliteRegion")
                    strHdg = "restart"
                End If
                If rO("mnth") = strHdg Then 'same month as last entry
                Else 'add group heading
                    nd1Mnth = nd0Region.Nodes.Add(rO("mnth"))
                    strHdg = rO("Mnth")
                End If
                nd2Order = nd1Mnth.Nodes.Add(rO("OrderID"), rO("CombFld"))

            Catch ex As System.Exception
            End Try
        Next rO


        strHdg = Nothing
        strRegion = Nothing
        MouseDefault()
        Me.StatusBarPanel1.Text = "Done"

    End Sub 'load order tree

    'LOAD  GRIDS from treeview node selection
    Public Sub FilterGrids(ByVal i As Integer)

        If isLoaded Then   'why doesnt this work if not loaded or from Load method?
            'why filter instead of table relation - because treeview cannot be bound
            Dim sReg As String = "OrderNum = " & CType(i, Integer)

            Try
                dvPay.RowFilter = sReg
                Me.dgvFinance2.DataSource = dvPay
            Catch ex As Exception
                modGlobalVar.msg("ERROR: filtering dvPay", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
            Try
                dvReg.RowFilter = sReg
                Me.dgvReg2.DataSource = dvReg

            Catch ex As Exception
                modGlobalVar.msg("ERROR: filtering dvReg", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
            Try
                dvPending.RowFilter = "OID = " & CType(i, Integer)
                Me.dgvPending2.DataSource = dvPending
            Catch ex As Exception
                modGlobalVar.msg("ERROR: filtering dvPay", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                '  sPay = Nothing
                sReg = Nothing
            End Try
        End If

    End Sub

    ' CALL LOAD GRIDS
    Private Sub TreeVorders_AfterSelect(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) _
        Handles treevOrders.AfterSelect

        If isLoaded = True Then
            If e.Node.Parent Is Nothing Then
            Else
                If e.Node.Parent.Parent Is Nothing Then
                Else
                    Me.txtSrchOrder.Text = e.Node.Name.ToString
                    FilterGrids(Me.txtSrchOrder.Text) 'e.Node.Name)

                End If
            End If
        End If

    End Sub  'treev after select

    'CALL LOAD TREE for Specific Order
    Private Sub btnSrchOrder_click(sender As System.Object, e As System.EventArgs) _
        Handles btnSrchOrder.Click

        Try
            LoadOrderTree(CType(Me.txtSrchOrder.Text.Replace("*", 0), Integer), "Show All")
        Catch ex As Exception
            modGlobalVar.msg("ERROR: LOADING TREE", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Select Case treevOrders.Nodes.Count
            Case Is > 0
                SelectFirstNode("First")
            Case Else
                FilterGrids(50) 'non-existant order to blank grids
        End Select

    End Sub   'click btn search

    'CALL LOAD TREE for ALL REGIONS
    Private Sub btnRegion_Click(sender As System.Object, e As System.EventArgs) Handles btnRegion.Click
        LoadOrderTree(0, "Show All")
        SelectFirstNode("none")
    End Sub

    'SELECT FIRST NODE SO GRIDS LOAD
    Private Sub SelectFirstNode(ByVal strExpandWhich As String)

        Select Case strExpandWhich  'on first load, or specific order
            Case Is = "First"
                Try
                    Me.treevOrders.Nodes(0).FirstNode.Expand()
                    Me.treevOrders.Nodes(0).Nodes(0).FirstNode.Expand()
                Catch ex As Exception
                Finally
                    Try
                        Me.treevOrders.SelectedNode = Me.treevOrders.Nodes(0).Nodes(0).FirstNode
                    Catch exc As Exception
                    End Try
                End Try
            Case Is = "none"  'onLoad All button - don't expand any region
        End Select

    End Sub

    'CALL LOAD TREEVIEWs ON LOAD -- also loads grids first time 
    Private Sub TabControl1_SelectedIndexChanged(sender As Object, e As System.EventArgs) _
        Handles TabControl1.SelectedIndexChanged

        'TODO look into moving refresh grids
        Select Case sender.selectedtab.name
            Case Is = "pgOrder"
                If b1stOrder = True Then
                    MouseWait()
                    Me.StatusBarPanel1.Text = "loading order tree"

                    'FILL DATATABLES
                    RefreshPayments()

                    Try
                        Me.StatusBarPanel1.Text = "loading registrations"
                        Me.TOrderLstRegistrationsTableAdapter.Fill(Me.DsEventRegOrderTV2.tOrderLstRegistrations)
                    Catch ex As System.Exception
                        modGlobalVar.msg("ERROR: loading registrations  ", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try
                    Try
                        Me.StatusBarPanel1.Text = "loading pendings"
                        Me.TOrderLstPendingTableAdapter.Fill(Me.DsEventRegOrderTV2.tOrderLstPendings)
                    Catch ex As System.Exception
                        modGlobalVar.msg("ERROR: loading pendings  ", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try

                    'LOAD TREE TO USERS REGION ON FIRST TIME OPEN TAB
                    If bFirstTime = True Then
                        LoadOrderTree(0, usrRegion)
                        SelectFirstNode("First")
                        bFirstTime = False
                    End If
                    b1stOrder = False
                    Me.StatusBarPanel1.Text = "done"
                    MouseDefault()
                End If
                Me.miGotoOrder.Enabled = True
                Me.miGotoPayment.Enabled = True
            Case Else
                Me.miGotoOrder.Enabled = False
                Me.miGotoPayment.Enabled = False
        End Select
        '   End If
    End Sub

#End Region 'ORDER Load treeview

#Region "ORDER Open Secondary Order Forms"

    ''OPEN ORDERS
    Private Sub TreeView_NodeMouseDoubleClick(sender As Object, e As System.Windows.Forms.TreeNodeMouseClickEventArgs) _
        Handles treevOrders.NodeMouseDoubleClick
        'only lowest nodes contain order number as node name
        Dim i As Integer
        If Integer.TryParse(e.Node.Name, i) = False Then
            Exit Sub
        End If

        Dim qryFill As New SqlCommand("SELECT StatusName FROM luStatus WHERE  (Topic = 'order') AND (Selectable = 1) AND (Ball IS NULL) ORDER BY SortNum", sc)
        qryFill.CommandType = System.Data.CommandType.Text

        modGlobalVar.LoadDataTable(tblOrderStatus, qryFill)
        modGlobalVar.OpenMainWOrder(e.Node.Name, True)

    End Sub

    'OPEN PAYMENTS
    Private Sub dgvPay_DoubleClick(sender As Object, e As System.EventArgs) _
     Handles dgvFinance2.DoubleClick
        Dim iRow As Integer = Me.dgvFinance2.CurrentRow.Index
        Dim iColName As Integer = Me.dgvFinance2.Columns("colPaymentID").Index

        modGlobalVar.OpenMainWPayment(Me.dgvFinance2.Item(iColName, iRow).Value)
    End Sub

    'OPEN REGISTRATION
    Private Sub dgvReg2_DoubleClick(sender As Object, e As System.EventArgs) _
        Handles dgvReg2.DoubleClick
        Dim iRow As Integer = Me.dgvReg2.CurrentRow.Index
        Dim iColName As Integer = Me.dgvReg2.Columns("colRegLFName").Index
        Dim iColEvent As Integer = Me.dgvReg2.Columns("colRegEventName").Index
        Dim str As String = Me.dgvReg2.Item(iColName, iRow).Value & " @ " & Me.dgvReg2.Item(iColEvent, iRow).Value

        modGlobalVar.OpenMainWReg2(dgvReg2.Item(0, iRow).Value, str, False, "Registrant")
        str = Nothing
    End Sub

    'OPEN PENDINGS
    Private Sub dgvPending2_DoubleClick(sender As Object, e As System.EventArgs) _
     Handles dgvPending2.DoubleClick
        Dim iRow As Integer = Me.dgvPending2.CurrentRow.Index
        Dim iColName As Integer = Me.dgvPending2.Columns("colPendingOID").Index

        OpenPendingRegistrations(Me.dgvPending2.Item(iColName, iRow).Value)
    End Sub

#End Region  'open secondary order forms

#End Region 'ORDERS  '********************

#Region "MENU ITEMS"

    'SEND EMAIL TO GROUP
    Private Sub miEmailRegister_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
             Handles miEmailRegister.Click, miEmailAttend.Click, miEmailNotAttend.Click
        Dim EventNums As New StringBuilder
         If Me.grdvwMain.SelectedRows.Count > 0 Then
            For Each r As DataGridViewRow In Me.grdvwMain.SelectedRows
                EventNums.Append(r.Cells("EventID" & grdvwColName).Value & ",")
            Next
            modPopup.EmailDelivraEvent(EventNums.ToString, sender.tag)
        Else
            msg(MsgCodes.noRowSelected)
        End If
    End Sub

    'OPEN NEW ORDER
    Private Sub miOrderSetup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles miGotoOrder.Click
        Try
            modGlobalVar.OpenMainWOrder(Me.txtSrchOrder.Text, True)
        Catch ex As Exception
            modGlobalVar.msg("ERROR: opening order detail", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    'OPEN PENDING FORM
    Private Sub miPending_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles miGotoPending.Click

        If ThisID > 0 Then
            OpenPendingRegistrations(IsNull(Me.grdvwMain.CurrentRow.Cells("Pending" & grdvwColName).Value, 0), ThisID, Me.grdvwMain.CurrentRow.Cells("SatelliteRegion" & grdvwColName).Value)
        Else 'open all pendings for region
            OpenPendingRegistrations(0, 0, IsNull(Me.cboRegion.Text, usrRegion))
        End If
    End Sub

#End Region 'MenuItems

#Region "Reports"

    'OPEN REPORT: MASTER CHECKLIST SPREADSHEET 
    'deactivated as takes too long when scrolling
    Private Sub miChecklist_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles miChecklist.Click

        If ThisID > 0 Then
        Else
            modGlobalVar.msg(MsgCodes.noRowSelected) '"ATTENTION: no event selected.", "Highlight a row in the grid.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        MouseWait()
        Dim CaseNam As String = IsNull(Me.grdvwMain.CurrentRow.Cells("MasterWorkshopName" & grdvwColName).Value, String.Empty)
        If CaseNam = String.Empty Then
            msg("CANCELLING REQUEST", "the master checklist hasn't been created yet", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
        'works but replaced: OpenSpreadsheet("Events", CaseNm) ', Me.grdMain.Item(Me.grdMain.CurrentRowIndex)) ', iColFirstDt))
        If OpenFile(modPopup.GetFileName(LinkedPath & "Events\", CaseNam & ".xls*", True)) Then
            Me.StatusBarPanel1.Text = "file opened"
        Else
            Me.StatusBarPanel1.Text = "network errror"
        End If
        MouseDefault()
    End Sub

    'OPEN REPORT: CANCELLATION PHONELIST
    Private Sub miCancellationRpt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles miCancellationRpt.Click
        If ThisID > 0 Then
        Else
            modGlobalVar.msg(MsgCodes.noRowSelected)
            Exit Sub
        End If

        If Me.grdvwMain.CurrentRow.Cells("TotRegistered" & grdvwColName).Value > 0 Then   'no actual registrations to report
            OpenReportCancellation(ThisID, Me.grdvwMain.CurrentRow.Cells("EventName" & grdvwColName).Value)
        Else
            modGlobalVar.msg(MsgCodes.noResultCancel) '"Cancelling Report", "no registrations found", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If
    End Sub

    'HELP BUTTON
    Private Sub btnHelp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnHelp.Click, miHelp.Click
        modGlobalVar.msg("HELP: SEARCH for ED EVENT:", "HOW TO SEARCH:" & NextLine & "This window opens showing any upcoming Events.  " & NextLine &
               "1. Select other search criteria using the radio buttons and drop down boxes. " & NextLine &
               "2. Click the Search button, or press the Enter key." & NextLine & NextLine & "Note: * is a wildcard character.  " & NextLine & NextLine &
               "To ADD NEW:" & NextLine & "EVENT - Click the yellow New button or use the menu: File/New Event." & NextLine &
               "TO Add NEW: REGISTRATION, use the Event Detail or Contact Detail new button" & NextLine & NextLine &
               "DEFINITIONS of Type of Event: use Help menu item", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    'SHOW WORKSHOP TYPE DEFINITIONS
    Private Sub ShowDef(ByVal sender As System.Object, ByVal e As System.EventArgs) _
Handles miDef.Click
        modPopup.ShowDefinitions("Workshop")
    End Sub

    'OPEN REPORTS: REPORT VIEWER
    Private Sub miReportRegistration_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles miReportRegistration.Click, btnReport.Click
        If ThisID > 0 Then
        Else
            modGlobalVar.msg(MsgCodes.noRowSelected)
            Exit Sub
        End If

        If Me.grdvwMain.CurrentRow.Cells("TotRegistered" & grdvwColName).Value > 0 Then
            OpenReportRegistration(ThisID, Me.grdvwMain.CurrentRow.Cells("EventName" & grdvwColName).Value)
        Else
            modGlobalVar.msg(MsgCodes.noResultCancel) ', "", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If
    End Sub

    'POpup report
    Private Sub miOutstanding_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Dim sql As New SqlCommand("[RptsNewPayments]", sc)
        sql.Parameters.Add("@What", SqlDbType.VarChar).Value = sender.tag
        '  sql.Parameters.Add("@Region", SqlDbType.VarChar).Value = Me.cboRegion.Text
        Try
            sql.Parameters.Add("@Scope", SqlDbType.Int)
        Catch ex As Exception
            modGlobalVar.msg("ERROR: @Scope", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        If ThisID > 0 Then
            sql.Parameters("@Scope").Value = ThisID
        Else
            sql.Parameters("@Scope").Value = 1
        End If
         modPopup.OpenPopupDGV(sender.text, sql)
    End Sub

    'show last 10 downloaded registrations 3/16
    Private Sub miRecentRegs_Click(sender As System.Object, e As System.EventArgs) Handles miRecentRegs.Click
        Dim sql As New SqlCommand("SELECT TOP 10 OrderID as OrderNum, QtyRegs, cast(DtOrder as varchar)  as OrderDate, OrderStatus, Regions, ProposedPaymentMethod as PaymentBy, RegistrarEmail FROM tblEventRegOrder2 WHERE OrderID < 20000 ORDER BY OrderID Desc", sc)
        modPopup.OpenPopupDGV("Latest Registrations Downloaded", sql)
    End Sub

#End Region 'reports

#Region "ADD ITEM"

    'SHOW ADD NEW POPUP MENU
    Protected Sub miAddEvent_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles miAddEvent.Click, btnNew.Click
        Dim pp As New ContextMenu
        Dim eh As EventHandler = AddressOf ppAddNew

        'ENHANCE set default as selected tab or conversation if none selected
        pp.MenuItems.Add("ADD NEW ") 'FOR " + IsNull(Me.txtCaseName.Text, "no case name"))
        pp.MenuItems.Add("---------------------------------------------")
        pp.MenuItems.Add("Event", eh)
        Select Case Me.TabControl1.SelectedTab.Tag
            Case Is = "EVENTS"

            Case Is = "ORDERS"
                If Me.txtSrchOrder.Text = String.Empty Or txtSrchOrder.Text = "*" Then
                Else
                    pp.MenuItems.Add("Payment (for Order# " & Me.txtSrchOrder.Text & ")", eh)
                End If
        End Select
        pp.MenuItems(2).DefaultItem = True
        pp.Show(Me, PointToClient(Control.MousePosition)) ' New Point(200, 10)) started causing error for invisible control??
    End Sub

    'ADD NEW EVENT or ORDER
    Private Sub ppAddNew(ByVal obj As Object, ByVal ea As EventArgs)
        Dim str As String
        Dim i As Integer 'for new order number
        Me.StatusBarPanel1.Text = "Adding new " + obj.text
        If modGlobalVar.msg("Are you sure?", "About to enter a new " & obj.text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then
            Me.StatusBarPanel1.Text = "Ready"
            Exit Sub
        End If

        Select Case obj.text.substring(0, 5)
            Case Is = "Event" 'INSERT NEW ED EVENT TO BACKEND
                str = "INSERT INTO tblEventSetup (EventName, TypeofEvent, SatelliteRegion ) VALUES (N'Event Name 2',N'Workshop','" & usrRegion & "' ); SELECT @@Identity" 'otherwise causes conflict if another partial event is entered
                Me.StatusBarPanel1.Text = "Entering new Workshop"
                modGlobalVar.LoadWEventDD("upcoming") 'REFRESH Event LIST
            Case Is = "Order"
                gPayment.EventID = Me.fldEventID.Text
                gPayment.ContactID = 0
                i = modPopup.NextOrderNumber()

            Case Is = "Payme"
                gPayment.PaymentSearch = Me.TabControl1.SelectedTab.Tag
                gPayment.OrderID = Me.txtSrchOrder.Text
                str = "INSERT INTO tblEventRegPayment2 (PaymentType, OrderNum, DtPayment, PaymentStaffNum, PaymentMethod) VALUES('Payment', " & Me.txtSrchOrder.Text & ", N'" & DateTime.Now & "', " & usr & ", 'Check'); SELECT @@Identity"
                '    str = "INSERT INTO tblEventRegPayment2 (PaymentType, DtPayment, PaymentStaffNum, PaymentMethod) VALUES('Payment', N'" & DateTime.Now & "', " & usr & ", 'Check'); SELECT @@Identity"
            Case Is = "Regis"
                gPayment.OrderID = Me.txtSrchOrder.Text
                str = "INSERT INTO tblEventReg2 (OrderNum, RegDate,EnteredBy) VALUES (" + Me.txtSrchOrder.Text & ", " + DateTime.Now + "," + usrName + "); SELECT @@Identity"
            Case Else
                Exit Sub
        End Select

        '      ..................................
InsertNewItem:
        If Not SCConnect() Then
            Exit Sub
        End If
        Dim cmd As New SqlClient.SqlCommand(str, sc) ', myTrans)
        Dim newID As Integer
        Try
            newID = cmd.ExecuteScalar()
            '      myTrans.Commit()
        Catch ex As Exception
            modGlobalVar.msg("ERROR: execute addNew", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
            '    myTrans.Rollback()
            sc.Close()
            Exit Sub
        End Try
        sc.Close()

        'OPEN EVENT MAIN FORM TO EDIT NEW ED EVENT
        Select Case obj.text.substring(0, 5)
            Case "Event"
                modGlobalVar.OpenMainWEvent(newID, "Entering New Event", True)
            Case Is = "Order"
                modGlobalVar.OpenMainWOrder(i, True)
                'OpenNewWOrder2(i, "Entering New Order", True, True)
            Case Is = "Payme"
                modGlobalVar.OpenMainWPayment(newID)
        End Select

        Select Case obj.text.substring(0, 5)
            Case "Event"
            Case "Order"
            Case "Payme"
                RefreshPayments()
        End Select
        Me.StatusBarPanel1.Text = "Done"
        MouseDefault()
    End Sub

#End Region 'addItem

#Region "General"

    'COPY CURRENT ID
    Private Sub StatusBar1_DoubleClick(sender As System.Object, e As System.EventArgs) Handles StatusBar1.DoubleClick
        Clipboard.SetText(ThisID)
    End Sub

    Private Sub fldEventSKU_TextChanged(sender As System.Object, e As System.EventArgs) Handles fldEventSKU.DoubleClick
        Clipboard.SetText(IsNull(sender.Text, 0))
    End Sub

    'SET STATUS BAR LEFT TEXT
    Private Sub SetStatusBarText(ByVal str As String)
        Me.StatusBar1.Panels(0).Text = str
    End Sub

    'SELECT ALL TEXT from enter
    Private Sub txtSearch_Enter(ByVal sender As Object, ByVal e As System.EventArgs) _
          Handles txtSearch.Enter, txtSrchOrder.Enter
        sender.SelectAll()
    End Sub

    Private Sub txtSrchOrder_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) _
        Handles txtSrchOrder.KeyDown
        'modGlobalVar.Msg(e.KeyData.ToString, , e.KeyValue.ToString) ' Return, 13
        If e.KeyData.ToString = "Return" Then
            Me.btnSrchOrder_click(Me, Nothing)
        End If
    End Sub

    'CALL SEARCH ON LEAVE FLD tab
    Private Sub txtSrchOrder_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSrchOrder.Leave
        Me.btnSrchOrder_click(Me, Nothing)
    End Sub

    'VALIDATE cboRETIRED
    Private Sub cboretired_Leave(ByVal sender As Object, ByVal e As System.EventArgs) _
     Handles cboRetired.Leave
        '      modGlobalVar.Msg("Before validation: " & cboDates.SelectedIndex.ToString, , cboDates.SelectedItem)
        If modGlobalVar.NewValidateCombo(sender, False) Then
        Else
            sender.Focus()
            Exit Sub
        End If
    End Sub

#End Region


End Class
