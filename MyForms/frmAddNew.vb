Imports System.Data.SqlClient

Public Class frmAddNew
    Inherits System.Windows.Forms.Form
    Public returnval As String


#Region "Initialize"

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()
        '  AddHandler Me.txtMailExpire.DataBindings(0).Parse, AddressOf modGlobalVar.DateParse
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
    '  Dim da As New SqlClient.SqlDataAdapter
    Friend WithEvents dgvPublisher As System.Windows.Forms.DataGridView
    Friend WithEvents lblHeading As System.Windows.Forms.Label
    Friend WithEvents DsPublisher1 As InfoCtr.dsPublisher
    Friend WithEvents DsMainMailList1 As InfoCtr.dsMainMailList
    ' Friend WithEvents tblpublisherTableAdapter1 As InfoCtr.dsPublisherTableAdapters.tblPublisherTableAdapter
    ' Friend WithEvents MainMailListTableAdapter1 As InfoCtr.dsMainMailListTableAdapters.MainMailListTableAdapter
    Friend WithEvents BindingSourcePublisher As System.Windows.Forms.BindingSource
    Friend WithEvents BindingNavigatorPublisher As System.Windows.Forms.BindingNavigator
    Friend WithEvents BindingNavigatorCountItem As System.Windows.Forms.ToolStripLabel
    Friend WithEvents BindingNavigatorMoveFirstItem As System.Windows.Forms.ToolStripButton
    Friend WithEvents BindingNavigatorMovePreviousItem As System.Windows.Forms.ToolStripButton
    Friend WithEvents BindingNavigatorSeparator As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents BindingNavigatorPositionItem As System.Windows.Forms.ToolStripTextBox
    Friend WithEvents BindingNavigatorSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents BindingNavigatorMoveNextItem As System.Windows.Forms.ToolStripButton
    Friend WithEvents BindingNavigatorMoveLastItem As System.Windows.Forms.ToolStripButton
    Friend WithEvents BindingNavigatorSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents TblPublisherTableAdapter As InfoCtr.dsPublisherTableAdapters.tblPublisherTableAdapter
    Friend WithEvents MainMailListTableAdapter As InfoCtr.dsMainMailListTableAdapters.MainMailListTableAdapter
    Friend WithEvents PublisherNameDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Address1DataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Address2DataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CityDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents StateDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CountryDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ZipDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TelephoneDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Telephone2DataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Telephone3DataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents EmailDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents WebsiteDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents tbPublisher As System.Windows.Forms.TabPage
    Friend WithEvents tbSavedMailList As System.Windows.Forms.TabPage
    Friend WithEvents tbDenomination As System.Windows.Forms.TabPage
    Friend WithEvents txtDenomWeb As System.Windows.Forms.TextBox
    Friend WithEvents cboDenomCPO As System.Windows.Forms.ComboBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents btnOKDenom As System.Windows.Forms.Button
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cboDenomGeneralType As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtDenomName As System.Windows.Forms.TextBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents txtMailDescription As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents txtMailName As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents ComboBox1 As System.Windows.Forms.ComboBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents ComboBox2 As System.Windows.Forms.ComboBox
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents TextBox2 As System.Windows.Forms.TextBox
    Friend WithEvents btnOKPublisher As System.Windows.Forms.Button
    Friend WithEvents btnOKMailFlag As System.Windows.Forms.Button
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents btnDenomGrid As System.Windows.Forms.Button
    Friend WithEvents dgvDenominations As System.Windows.Forms.DataGridView
    Friend WithEvents DsDenom As InfoCtr.dsDenom
    Friend WithEvents LuDenomTypeBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents LuDenomTypeTableAdapter As InfoCtr.dsDenomTableAdapters.luDenomTypeTableAdapter
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents txtDenomCommon As System.Windows.Forms.TextBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents tbCaterer As System.Windows.Forms.TabPage
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents DsCaterer As InfoCtr.dsCaterer
    Friend WithEvents TblCatererBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents TblCatererTableAdapter As InfoCtr.dsCatererTableAdapters.tblCatererTableAdapter
    ' Friend WithEvents MainMailListTableAdapter As InfoCtr.dsMainMailListTableAdapters.MainMailListTableAdapter
    Friend WithEvents btnOKCaterer As System.Windows.Forms.Button
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents AddressDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents txtStreet As System.Windows.Forms.TextBox
    Friend WithEvents txtWebsite As System.Windows.Forms.TextBox
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents Label34 As System.Windows.Forms.Label
    Friend WithEvents txtName As System.Windows.Forms.TextBox
    Friend WithEvents txtNotes As System.Windows.Forms.TextBox
    Friend WithEvents txtCellPhone As System.Windows.Forms.TextBox
    Friend WithEvents txtSpecialty As System.Windows.Forms.TextBox
    Friend WithEvents txtContact As System.Windows.Forms.TextBox
    Friend WithEvents Label36 As System.Windows.Forms.Label
    Friend WithEvents Label35 As System.Windows.Forms.Label
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents txtCity As System.Windows.Forms.TextBox
    Friend WithEvents txtZip As System.Windows.Forms.TextBox
    Friend WithEvents txtState As System.Windows.Forms.TextBox
    Friend WithEvents txtPhone As System.Windows.Forms.TextBox
    Friend WithEvents txtEmail As System.Windows.Forms.TextBox
    Friend WithEvents Label37 As System.Windows.Forms.Label
    Friend WithEvents CatererID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SatelliteRegionDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CatererNameDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PhoneDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ContactDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CellPhoneDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SpecialtyDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents NotesDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CityDataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents StateDataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ZipDataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents WebsiteDataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents EmailDataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents StaffNumDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Label38 As System.Windows.Forms.Label
    Friend WithEvents txtSatellite As System.Windows.Forms.TextBox
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents tbResourceGuide As System.Windows.Forms.TabPage
    Friend WithEvents dgvRG As System.Windows.Forms.DataGridView
    Friend WithEvents DsResourceGuideProcess As InfoCtr.dsResourceGuideProcess
    Friend WithEvents ProcessRGBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents ProcessRGTableAdapter As InfoCtr.dsResourceGuideProcessTableAdapters.ProcessRGTableAdapter
    Friend WithEvents RGNameDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ReceivedDtDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ResDirReportDtDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents OrderDtDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ForwardDtDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ShortNameDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents btnRG As System.Windows.Forms.Button
    Friend WithEvents btnOKRG As System.Windows.Forms.Button
    Friend WithEvents dgvMailings As System.Windows.Forms.DataGridView
    Friend WithEvents MailListIDDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CreateStaffNumDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents MailListNameDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DescriptionDataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CreateDateDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ExpirationDateDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents NumMembersDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ListOwnerDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Label39 As System.Windows.Forms.Label
    Friend WithEvents txtMailExpire As System.Windows.Forms.TextBox
    Friend WithEvents DenominationDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents GeneralTypeDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CommonNameDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DescriptionDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents WebsiteDataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CathProtOtherDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents WorkshopGroupingDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents EthnicityDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents BindingSourceDenomination As System.Windows.Forms.BindingSource
    Friend WithEvents tbRegistrationCopy As System.Windows.Forms.TabPage
    Friend WithEvents btnRefreshEvents As System.Windows.Forms.Button
    Friend WithEvents cboEvent As InfoCtr.ComboBoxRelaxed
    Friend WithEvents btnRegCopyDone As System.Windows.Forms.Button
    Friend WithEvents lblRegistrationDetail As System.Windows.Forms.Label
    Friend WithEvents Label42 As System.Windows.Forms.Label
    Friend WithEvents lblRegIDtoCopy As System.Windows.Forms.Label


    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAddNew))
        Dim Label40 As System.Windows.Forms.Label
        Me.dgvPublisher = New System.Windows.Forms.DataGridView()
        Me.PublisherNameDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Address1DataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Address2DataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CityDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.StateDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CountryDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ZipDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TelephoneDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Telephone2DataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Telephone3DataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.EmailDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.WebsiteDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.BindingSourcePublisher = New System.Windows.Forms.BindingSource(Me.components)
        Me.DsPublisher1 = New InfoCtr.dsPublisher()
        Me.lblHeading = New System.Windows.Forms.Label()
        Me.BindingNavigatorPublisher = New System.Windows.Forms.BindingNavigator(Me.components)
        Me.BindingNavigatorCountItem = New System.Windows.Forms.ToolStripLabel()
        Me.BindingNavigatorMoveFirstItem = New System.Windows.Forms.ToolStripButton()
        Me.BindingNavigatorMovePreviousItem = New System.Windows.Forms.ToolStripButton()
        Me.BindingNavigatorSeparator = New System.Windows.Forms.ToolStripSeparator()
        Me.BindingNavigatorPositionItem = New System.Windows.Forms.ToolStripTextBox()
        Me.BindingNavigatorSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.BindingNavigatorMoveNextItem = New System.Windows.Forms.ToolStripButton()
        Me.BindingNavigatorMoveLastItem = New System.Windows.Forms.ToolStripButton()
        Me.BindingNavigatorSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.TblPublisherTableAdapter = New InfoCtr.dsPublisherTableAdapters.tblPublisherTableAdapter()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.tbPublisher = New System.Windows.Forms.TabPage()
        Me.btnOKPublisher = New System.Windows.Forms.Button()
        Me.tbSavedMailList = New System.Windows.Forms.TabPage()
        Me.Label39 = New System.Windows.Forms.Label()
        Me.dgvMailings = New System.Windows.Forms.DataGridView()
        Me.MailListIDDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CreateStaffNumDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MailListNameDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DescriptionDataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CreateDateDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ExpirationDateDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.NumMembersDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ListOwnerDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DsMainMailList1 = New InfoCtr.dsMainMailList()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.btnOKMailFlag = New System.Windows.Forms.Button()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.txtMailDescription = New System.Windows.Forms.TextBox()
        Me.txtMailExpire = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.txtMailName = New System.Windows.Forms.TextBox()
        Me.tbDenomination = New System.Windows.Forms.TabPage()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.txtDenomCommon = New System.Windows.Forms.TextBox()
        Me.btnDenomGrid = New System.Windows.Forms.Button()
        Me.dgvDenominations = New System.Windows.Forms.DataGridView()
        Me.DenominationDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GeneralTypeDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CommonNameDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DescriptionDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.WebsiteDataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CathProtOtherDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.WorkshopGroupingDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.EthnicityDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.BindingSourceDenomination = New System.Windows.Forms.BindingSource(Me.components)
        Me.DsDenom = New InfoCtr.dsDenom()
        Me.txtDenomWeb = New System.Windows.Forms.TextBox()
        Me.cboDenomCPO = New System.Windows.Forms.ComboBox()
        Me.btnOKDenom = New System.Windows.Forms.Button()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cboDenomGeneralType = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtDenomName = New System.Windows.Forms.TextBox()
        Me.tbCaterer = New System.Windows.Forms.TabPage()
        Me.btnOKCaterer = New System.Windows.Forms.Button()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.txtContact = New System.Windows.Forms.TextBox()
        Me.Label38 = New System.Windows.Forms.Label()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.txtSatellite = New System.Windows.Forms.TextBox()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.txtEmail = New System.Windows.Forms.TextBox()
        Me.txtCity = New System.Windows.Forms.TextBox()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.txtPhone = New System.Windows.Forms.TextBox()
        Me.txtZip = New System.Windows.Forms.TextBox()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.Label37 = New System.Windows.Forms.Label()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.txtWebsite = New System.Windows.Forms.TextBox()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.txtNotes = New System.Windows.Forms.TextBox()
        Me.txtName = New System.Windows.Forms.TextBox()
        Me.txtStreet = New System.Windows.Forms.TextBox()
        Me.txtCellPhone = New System.Windows.Forms.TextBox()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.txtSpecialty = New System.Windows.Forms.TextBox()
        Me.txtState = New System.Windows.Forms.TextBox()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.Label36 = New System.Windows.Forms.Label()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.Label35 = New System.Windows.Forms.Label()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.CatererID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SatelliteRegionDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CatererNameDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PhoneDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ContactDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CellPhoneDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SpecialtyDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.NotesDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CityDataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.StateDataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ZipDataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.WebsiteDataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.EmailDataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.StaffNumDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TblCatererBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.DsCaterer = New InfoCtr.dsCaterer()
        Me.tbResourceGuide = New System.Windows.Forms.TabPage()
        Me.btnOKRG = New System.Windows.Forms.Button()
        Me.btnRG = New System.Windows.Forms.Button()
        Me.dgvRG = New System.Windows.Forms.DataGridView()
        Me.RGNameDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ReceivedDtDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ResDirReportDtDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.OrderDtDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ForwardDtDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ShortNameDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ProcessRGBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.DsResourceGuideProcess = New InfoCtr.dsResourceGuideProcess()
        Me.LuDenomTypeBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.ComboBox2 = New System.Windows.Forms.ComboBox()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.LuDenomTypeTableAdapter = New InfoCtr.dsDenomTableAdapters.luDenomTypeTableAdapter()
        Me.TblCatererTableAdapter = New InfoCtr.dsCatererTableAdapters.tblCatererTableAdapter()
        Me.ProcessRGTableAdapter = New InfoCtr.dsResourceGuideProcessTableAdapters.ProcessRGTableAdapter()
        Me.MainMailListTableAdapter = New InfoCtr.dsMainMailListTableAdapters.MainMailListTableAdapter()
        Me.tbRegistrationCopy = New System.Windows.Forms.TabPage()
        Me.btnRefreshEvents = New System.Windows.Forms.Button()
        Me.cboEvent = New InfoCtr.ComboBoxRelaxed()
        Me.btnRegCopyDone = New System.Windows.Forms.Button()
        Me.Label42 = New System.Windows.Forms.Label()
        Me.lblRegistrationDetail = New System.Windows.Forms.Label()
        Me.lblRegIDtoCopy = New System.Windows.Forms.Label()
        Label40 = New System.Windows.Forms.Label()
        CType(Me.dgvPublisher, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BindingSourcePublisher, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsPublisher1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BindingNavigatorPublisher, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.BindingNavigatorPublisher.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.tbPublisher.SuspendLayout()
        Me.tbSavedMailList.SuspendLayout()
        CType(Me.dgvMailings, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsMainMailList1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tbDenomination.SuspendLayout()
        CType(Me.dgvDenominations, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BindingSourceDenomination, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsDenom, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tbCaterer.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TblCatererBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsCaterer, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tbResourceGuide.SuspendLayout()
        CType(Me.dgvRG, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ProcessRGBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsResourceGuideProcess, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LuDenomTypeBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tbRegistrationCopy.SuspendLayout()
        Me.SuspendLayout()
        '
        'dgvPublisher
        '
        Me.dgvPublisher.AllowUserToAddRows = False
        Me.dgvPublisher.AllowUserToDeleteRows = False
        Me.dgvPublisher.AllowUserToOrderColumns = True
        Me.dgvPublisher.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.dgvPublisher.AutoGenerateColumns = False
        Me.dgvPublisher.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvPublisher.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.PublisherNameDataGridViewTextBoxColumn, Me.Address1DataGridViewTextBoxColumn, Me.Address2DataGridViewTextBoxColumn, Me.CityDataGridViewTextBoxColumn, Me.StateDataGridViewTextBoxColumn, Me.CountryDataGridViewTextBoxColumn, Me.ZipDataGridViewTextBoxColumn, Me.TelephoneDataGridViewTextBoxColumn, Me.Telephone2DataGridViewTextBoxColumn, Me.Telephone3DataGridViewTextBoxColumn, Me.EmailDataGridViewTextBoxColumn, Me.WebsiteDataGridViewTextBoxColumn})
        Me.dgvPublisher.DataSource = Me.BindingSourcePublisher
        Me.dgvPublisher.Location = New System.Drawing.Point(21, 93)
        Me.dgvPublisher.Name = "dgvPublisher"
        Me.dgvPublisher.RowHeadersWidth = 21
        Me.dgvPublisher.Size = New System.Drawing.Size(859, 468)
        Me.dgvPublisher.TabIndex = 0
        '
        'PublisherNameDataGridViewTextBoxColumn
        '
        Me.PublisherNameDataGridViewTextBoxColumn.DataPropertyName = "PublisherName"
        Me.PublisherNameDataGridViewTextBoxColumn.HeaderText = "PublisherName"
        Me.PublisherNameDataGridViewTextBoxColumn.Name = "PublisherNameDataGridViewTextBoxColumn"
        '
        'Address1DataGridViewTextBoxColumn
        '
        Me.Address1DataGridViewTextBoxColumn.DataPropertyName = "Address1"
        Me.Address1DataGridViewTextBoxColumn.HeaderText = "Address1"
        Me.Address1DataGridViewTextBoxColumn.Name = "Address1DataGridViewTextBoxColumn"
        '
        'Address2DataGridViewTextBoxColumn
        '
        Me.Address2DataGridViewTextBoxColumn.DataPropertyName = "Address2"
        Me.Address2DataGridViewTextBoxColumn.HeaderText = "Address2"
        Me.Address2DataGridViewTextBoxColumn.Name = "Address2DataGridViewTextBoxColumn"
        '
        'CityDataGridViewTextBoxColumn
        '
        Me.CityDataGridViewTextBoxColumn.DataPropertyName = "City"
        Me.CityDataGridViewTextBoxColumn.HeaderText = "City"
        Me.CityDataGridViewTextBoxColumn.Name = "CityDataGridViewTextBoxColumn"
        '
        'StateDataGridViewTextBoxColumn
        '
        Me.StateDataGridViewTextBoxColumn.DataPropertyName = "State"
        Me.StateDataGridViewTextBoxColumn.HeaderText = "State"
        Me.StateDataGridViewTextBoxColumn.Name = "StateDataGridViewTextBoxColumn"
        '
        'CountryDataGridViewTextBoxColumn
        '
        Me.CountryDataGridViewTextBoxColumn.DataPropertyName = "Country"
        Me.CountryDataGridViewTextBoxColumn.HeaderText = "Country"
        Me.CountryDataGridViewTextBoxColumn.Name = "CountryDataGridViewTextBoxColumn"
        '
        'ZipDataGridViewTextBoxColumn
        '
        Me.ZipDataGridViewTextBoxColumn.DataPropertyName = "Zip"
        Me.ZipDataGridViewTextBoxColumn.HeaderText = "Zip"
        Me.ZipDataGridViewTextBoxColumn.Name = "ZipDataGridViewTextBoxColumn"
        '
        'TelephoneDataGridViewTextBoxColumn
        '
        Me.TelephoneDataGridViewTextBoxColumn.DataPropertyName = "Telephone"
        Me.TelephoneDataGridViewTextBoxColumn.HeaderText = "Telephone"
        Me.TelephoneDataGridViewTextBoxColumn.Name = "TelephoneDataGridViewTextBoxColumn"
        '
        'Telephone2DataGridViewTextBoxColumn
        '
        Me.Telephone2DataGridViewTextBoxColumn.DataPropertyName = "Telephone2"
        Me.Telephone2DataGridViewTextBoxColumn.HeaderText = "Telephone2"
        Me.Telephone2DataGridViewTextBoxColumn.Name = "Telephone2DataGridViewTextBoxColumn"
        '
        'Telephone3DataGridViewTextBoxColumn
        '
        Me.Telephone3DataGridViewTextBoxColumn.DataPropertyName = "Telephone3"
        Me.Telephone3DataGridViewTextBoxColumn.HeaderText = "Telephone3"
        Me.Telephone3DataGridViewTextBoxColumn.Name = "Telephone3DataGridViewTextBoxColumn"
        '
        'EmailDataGridViewTextBoxColumn
        '
        Me.EmailDataGridViewTextBoxColumn.DataPropertyName = "Email"
        Me.EmailDataGridViewTextBoxColumn.HeaderText = "Email"
        Me.EmailDataGridViewTextBoxColumn.Name = "EmailDataGridViewTextBoxColumn"
        '
        'WebsiteDataGridViewTextBoxColumn
        '
        Me.WebsiteDataGridViewTextBoxColumn.DataPropertyName = "Website"
        Me.WebsiteDataGridViewTextBoxColumn.HeaderText = "Website"
        Me.WebsiteDataGridViewTextBoxColumn.Name = "WebsiteDataGridViewTextBoxColumn"
        '
        'BindingSourcePublisher
        '
        Me.BindingSourcePublisher.DataMember = "tblPublisher"
        Me.BindingSourcePublisher.DataSource = Me.DsPublisher1
        '
        'DsPublisher1
        '
        Me.DsPublisher1.DataSetName = "dsPublisher"
        Me.DsPublisher1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'lblHeading
        '
        Me.lblHeading.AutoSize = True
        Me.lblHeading.Font = New System.Drawing.Font("Comic Sans MS", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeading.ForeColor = System.Drawing.Color.Red
        Me.lblHeading.Location = New System.Drawing.Point(18, 35)
        Me.lblHeading.Name = "lblHeading"
        Me.lblHeading.Size = New System.Drawing.Size(221, 15)
        Me.lblHeading.TabIndex = 1
        Me.lblHeading.Text = "Edit Publisher name and information here."
        '
        'BindingNavigatorPublisher
        '
        Me.BindingNavigatorPublisher.AddNewItem = Nothing
        Me.BindingNavigatorPublisher.BindingSource = Me.BindingSourcePublisher
        Me.BindingNavigatorPublisher.CountItem = Me.BindingNavigatorCountItem
        Me.BindingNavigatorPublisher.DeleteItem = Nothing
        Me.BindingNavigatorPublisher.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BindingNavigatorMoveFirstItem, Me.BindingNavigatorMovePreviousItem, Me.BindingNavigatorSeparator, Me.BindingNavigatorPositionItem, Me.BindingNavigatorCountItem, Me.BindingNavigatorSeparator1, Me.BindingNavigatorMoveNextItem, Me.BindingNavigatorMoveLastItem, Me.BindingNavigatorSeparator2})
        Me.BindingNavigatorPublisher.Location = New System.Drawing.Point(0, 0)
        Me.BindingNavigatorPublisher.MoveFirstItem = Me.BindingNavigatorMoveFirstItem
        Me.BindingNavigatorPublisher.MoveLastItem = Me.BindingNavigatorMoveLastItem
        Me.BindingNavigatorPublisher.MoveNextItem = Me.BindingNavigatorMoveNextItem
        Me.BindingNavigatorPublisher.MovePreviousItem = Me.BindingNavigatorMovePreviousItem
        Me.BindingNavigatorPublisher.Name = "BindingNavigatorPublisher"
        Me.BindingNavigatorPublisher.PositionItem = Me.BindingNavigatorPositionItem
        Me.BindingNavigatorPublisher.Size = New System.Drawing.Size(977, 25)
        Me.BindingNavigatorPublisher.TabIndex = 2
        Me.BindingNavigatorPublisher.Text = "BindingNavigator1"
        '
        'BindingNavigatorCountItem
        '
        Me.BindingNavigatorCountItem.Name = "BindingNavigatorCountItem"
        Me.BindingNavigatorCountItem.Size = New System.Drawing.Size(35, 22)
        Me.BindingNavigatorCountItem.Text = "of {0}"
        Me.BindingNavigatorCountItem.ToolTipText = "Total number of items"
        '
        'BindingNavigatorMoveFirstItem
        '
        Me.BindingNavigatorMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorMoveFirstItem.Image = CType(resources.GetObject("BindingNavigatorMoveFirstItem.Image"), System.Drawing.Image)
        Me.BindingNavigatorMoveFirstItem.Name = "BindingNavigatorMoveFirstItem"
        Me.BindingNavigatorMoveFirstItem.RightToLeftAutoMirrorImage = True
        Me.BindingNavigatorMoveFirstItem.Size = New System.Drawing.Size(23, 22)
        Me.BindingNavigatorMoveFirstItem.Text = "Move first"
        '
        'BindingNavigatorMovePreviousItem
        '
        Me.BindingNavigatorMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorMovePreviousItem.Image = CType(resources.GetObject("BindingNavigatorMovePreviousItem.Image"), System.Drawing.Image)
        Me.BindingNavigatorMovePreviousItem.Name = "BindingNavigatorMovePreviousItem"
        Me.BindingNavigatorMovePreviousItem.RightToLeftAutoMirrorImage = True
        Me.BindingNavigatorMovePreviousItem.Size = New System.Drawing.Size(23, 22)
        Me.BindingNavigatorMovePreviousItem.Text = "Move previous"
        '
        'BindingNavigatorSeparator
        '
        Me.BindingNavigatorSeparator.Name = "BindingNavigatorSeparator"
        Me.BindingNavigatorSeparator.Size = New System.Drawing.Size(6, 25)
        '
        'BindingNavigatorPositionItem
        '
        Me.BindingNavigatorPositionItem.AccessibleName = "Position"
        Me.BindingNavigatorPositionItem.AutoSize = False
        Me.BindingNavigatorPositionItem.Name = "BindingNavigatorPositionItem"
        Me.BindingNavigatorPositionItem.Size = New System.Drawing.Size(50, 21)
        Me.BindingNavigatorPositionItem.Text = "0"
        Me.BindingNavigatorPositionItem.ToolTipText = "Current position"
        '
        'BindingNavigatorSeparator1
        '
        Me.BindingNavigatorSeparator1.Name = "BindingNavigatorSeparator1"
        Me.BindingNavigatorSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'BindingNavigatorMoveNextItem
        '
        Me.BindingNavigatorMoveNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorMoveNextItem.Image = CType(resources.GetObject("BindingNavigatorMoveNextItem.Image"), System.Drawing.Image)
        Me.BindingNavigatorMoveNextItem.Name = "BindingNavigatorMoveNextItem"
        Me.BindingNavigatorMoveNextItem.RightToLeftAutoMirrorImage = True
        Me.BindingNavigatorMoveNextItem.Size = New System.Drawing.Size(23, 22)
        Me.BindingNavigatorMoveNextItem.Text = "Move next"
        '
        'BindingNavigatorMoveLastItem
        '
        Me.BindingNavigatorMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorMoveLastItem.Image = CType(resources.GetObject("BindingNavigatorMoveLastItem.Image"), System.Drawing.Image)
        Me.BindingNavigatorMoveLastItem.Name = "BindingNavigatorMoveLastItem"
        Me.BindingNavigatorMoveLastItem.RightToLeftAutoMirrorImage = True
        Me.BindingNavigatorMoveLastItem.Size = New System.Drawing.Size(23, 22)
        Me.BindingNavigatorMoveLastItem.Text = "Move last"
        '
        'BindingNavigatorSeparator2
        '
        Me.BindingNavigatorSeparator2.Name = "BindingNavigatorSeparator2"
        Me.BindingNavigatorSeparator2.Size = New System.Drawing.Size(6, 25)
        '
        'TblPublisherTableAdapter
        '
        Me.TblPublisherTableAdapter.ClearBeforeFill = True
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.tbPublisher)
        Me.TabControl1.Controls.Add(Me.tbSavedMailList)
        Me.TabControl1.Controls.Add(Me.tbDenomination)
        Me.TabControl1.Controls.Add(Me.tbCaterer)
        Me.TabControl1.Controls.Add(Me.tbResourceGuide)
        Me.TabControl1.Controls.Add(Me.tbRegistrationCopy)
        Me.TabControl1.Location = New System.Drawing.Point(12, 37)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(928, 594)
        Me.TabControl1.TabIndex = 3
        '
        'tbPublisher
        '
        Me.tbPublisher.Controls.Add(Me.btnOKPublisher)
        Me.tbPublisher.Controls.Add(Me.dgvPublisher)
        Me.tbPublisher.Controls.Add(Me.lblHeading)
        Me.tbPublisher.Location = New System.Drawing.Point(4, 22)
        Me.tbPublisher.Name = "tbPublisher"
        Me.tbPublisher.Padding = New System.Windows.Forms.Padding(3)
        Me.tbPublisher.Size = New System.Drawing.Size(920, 568)
        Me.tbPublisher.TabIndex = 0
        Me.tbPublisher.Tag = "Publisher"
        Me.tbPublisher.Text = "Publisher"
        Me.tbPublisher.UseVisualStyleBackColor = True
        '
        'btnOKPublisher
        '
        Me.btnOKPublisher.Font = New System.Drawing.Font("Comic Sans MS", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnOKPublisher.ForeColor = System.Drawing.Color.Crimson
        Me.btnOKPublisher.Location = New System.Drawing.Point(783, 15)
        Me.btnOKPublisher.Name = "btnOKPublisher"
        Me.btnOKPublisher.Size = New System.Drawing.Size(80, 35)
        Me.btnOKPublisher.TabIndex = 7
        Me.btnOKPublisher.Text = "OK"
        Me.btnOKPublisher.UseVisualStyleBackColor = True
        '
        'tbSavedMailList
        '
        Me.tbSavedMailList.Controls.Add(Me.Label39)
        Me.tbSavedMailList.Controls.Add(Me.dgvMailings)
        Me.tbSavedMailList.Controls.Add(Me.Label17)
        Me.tbSavedMailList.Controls.Add(Me.btnOKMailFlag)
        Me.tbSavedMailList.Controls.Add(Me.Label16)
        Me.tbSavedMailList.Controls.Add(Me.txtMailDescription)
        Me.tbSavedMailList.Controls.Add(Me.txtMailExpire)
        Me.tbSavedMailList.Controls.Add(Me.Label13)
        Me.tbSavedMailList.Controls.Add(Me.Label14)
        Me.tbSavedMailList.Controls.Add(Me.Label15)
        Me.tbSavedMailList.Controls.Add(Me.txtMailName)
        Me.tbSavedMailList.Location = New System.Drawing.Point(4, 22)
        Me.tbSavedMailList.Name = "tbSavedMailList"
        Me.tbSavedMailList.Padding = New System.Windows.Forms.Padding(3)
        Me.tbSavedMailList.Size = New System.Drawing.Size(920, 568)
        Me.tbSavedMailList.TabIndex = 1
        Me.tbSavedMailList.Tag = "SavedMailList"
        Me.tbSavedMailList.Text = "Saved Mailing Lists"
        Me.tbSavedMailList.UseVisualStyleBackColor = True
        '
        'Label39
        '
        Me.Label39.AutoSize = True
        Me.Label39.Location = New System.Drawing.Point(24, 249)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(113, 13)
        Me.Label39.TabIndex = 179
        Me.Label39.Text = "My Other Mailing Lists:"
        '
        'dgvMailings
        '
        Me.dgvMailings.AllowUserToDeleteRows = False
        Me.dgvMailings.AllowUserToOrderColumns = True
        Me.dgvMailings.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.dgvMailings.AutoGenerateColumns = False
        Me.dgvMailings.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvMailings.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.MailListIDDataGridViewTextBoxColumn, Me.CreateStaffNumDataGridViewTextBoxColumn, Me.MailListNameDataGridViewTextBoxColumn, Me.DescriptionDataGridViewTextBoxColumn1, Me.CreateDateDataGridViewTextBoxColumn, Me.ExpirationDateDataGridViewTextBoxColumn, Me.NumMembersDataGridViewTextBoxColumn, Me.ListOwnerDataGridViewTextBoxColumn})
        Me.dgvMailings.DataMember = "MainMailList"
        Me.dgvMailings.DataSource = Me.DsMainMailList1
        Me.dgvMailings.Location = New System.Drawing.Point(23, 274)
        Me.dgvMailings.MultiSelect = False
        Me.dgvMailings.Name = "dgvMailings"
        Me.dgvMailings.RowHeadersWidth = 21
        Me.dgvMailings.Size = New System.Drawing.Size(881, 238)
        Me.dgvMailings.TabIndex = 178
        '
        'MailListIDDataGridViewTextBoxColumn
        '
        Me.MailListIDDataGridViewTextBoxColumn.DataPropertyName = "MailListID"
        Me.MailListIDDataGridViewTextBoxColumn.HeaderText = "MailListID"
        Me.MailListIDDataGridViewTextBoxColumn.Name = "MailListIDDataGridViewTextBoxColumn"
        Me.MailListIDDataGridViewTextBoxColumn.ReadOnly = True
        Me.MailListIDDataGridViewTextBoxColumn.Visible = False
        Me.MailListIDDataGridViewTextBoxColumn.Width = 200
        '
        'CreateStaffNumDataGridViewTextBoxColumn
        '
        Me.CreateStaffNumDataGridViewTextBoxColumn.DataPropertyName = "CreateStaffNum"
        Me.CreateStaffNumDataGridViewTextBoxColumn.HeaderText = "CreateStaffNum"
        Me.CreateStaffNumDataGridViewTextBoxColumn.Name = "CreateStaffNumDataGridViewTextBoxColumn"
        Me.CreateStaffNumDataGridViewTextBoxColumn.Visible = False
        '
        'MailListNameDataGridViewTextBoxColumn
        '
        Me.MailListNameDataGridViewTextBoxColumn.DataPropertyName = "MailListName"
        Me.MailListNameDataGridViewTextBoxColumn.HeaderText = "MailListName"
        Me.MailListNameDataGridViewTextBoxColumn.Name = "MailListNameDataGridViewTextBoxColumn"
        Me.MailListNameDataGridViewTextBoxColumn.Width = 200
        '
        'DescriptionDataGridViewTextBoxColumn1
        '
        Me.DescriptionDataGridViewTextBoxColumn1.DataPropertyName = "Description"
        Me.DescriptionDataGridViewTextBoxColumn1.HeaderText = "Description"
        Me.DescriptionDataGridViewTextBoxColumn1.Name = "DescriptionDataGridViewTextBoxColumn1"
        Me.DescriptionDataGridViewTextBoxColumn1.Width = 350
        '
        'CreateDateDataGridViewTextBoxColumn
        '
        Me.CreateDateDataGridViewTextBoxColumn.DataPropertyName = "CreateDate"
        Me.CreateDateDataGridViewTextBoxColumn.HeaderText = "CreateDate"
        Me.CreateDateDataGridViewTextBoxColumn.Name = "CreateDateDataGridViewTextBoxColumn"
        Me.CreateDateDataGridViewTextBoxColumn.Width = 75
        '
        'ExpirationDateDataGridViewTextBoxColumn
        '
        Me.ExpirationDateDataGridViewTextBoxColumn.DataPropertyName = "ExpirationDate"
        Me.ExpirationDateDataGridViewTextBoxColumn.HeaderText = "ExpirationDate"
        Me.ExpirationDateDataGridViewTextBoxColumn.Name = "ExpirationDateDataGridViewTextBoxColumn"
        Me.ExpirationDateDataGridViewTextBoxColumn.Width = 75
        '
        'NumMembersDataGridViewTextBoxColumn
        '
        Me.NumMembersDataGridViewTextBoxColumn.DataPropertyName = "NumMembers"
        Me.NumMembersDataGridViewTextBoxColumn.HeaderText = "# Members"
        Me.NumMembersDataGridViewTextBoxColumn.Name = "NumMembersDataGridViewTextBoxColumn"
        Me.NumMembersDataGridViewTextBoxColumn.ReadOnly = True
        Me.NumMembersDataGridViewTextBoxColumn.Width = 50
        '
        'ListOwnerDataGridViewTextBoxColumn
        '
        Me.ListOwnerDataGridViewTextBoxColumn.DataPropertyName = "ListOwner"
        Me.ListOwnerDataGridViewTextBoxColumn.HeaderText = "ListOwner"
        Me.ListOwnerDataGridViewTextBoxColumn.Name = "ListOwnerDataGridViewTextBoxColumn"
        '
        'DsMainMailList1
        '
        Me.DsMainMailList1.DataSetName = "dsMainMailList"
        Me.DsMainMailList1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.BackColor = System.Drawing.Color.Transparent
        Me.Label17.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label17.Font = New System.Drawing.Font("Comic Sans MS", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.ForeColor = System.Drawing.Color.Maroon
        Me.Label17.Location = New System.Drawing.Point(494, 89)
        Me.Label17.MaximumSize = New System.Drawing.Size(500, 0)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(400, 152)
        Me.Label17.TabIndex = 177
        Me.Label17.Text = resources.GetString("Label17.Text")
        '
        'btnOKMailFlag
        '
        Me.btnOKMailFlag.Font = New System.Drawing.Font("Comic Sans MS", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnOKMailFlag.ForeColor = System.Drawing.Color.Crimson
        Me.btnOKMailFlag.Location = New System.Drawing.Point(769, 28)
        Me.btnOKMailFlag.Name = "btnOKMailFlag"
        Me.btnOKMailFlag.Size = New System.Drawing.Size(123, 35)
        Me.btnOKMailFlag.TabIndex = 176
        Me.btnOKMailFlag.Text = "Save && Close"
        Me.btnOKMailFlag.UseVisualStyleBackColor = True
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(43, 173)
        Me.Label16.MaximumSize = New System.Drawing.Size(150, 0)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(140, 39)
        Me.Label16.TabIndex = 175
        Me.Label16.Text = "Optional - Expiration Date after which time the flag can be deleted:"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtMailDescription
        '
        Me.txtMailDescription.Location = New System.Drawing.Point(205, 89)
        Me.txtMailDescription.MaxLength = 200
        Me.txtMailDescription.Multiline = True
        Me.txtMailDescription.Name = "txtMailDescription"
        Me.txtMailDescription.Size = New System.Drawing.Size(274, 84)
        Me.txtMailDescription.TabIndex = 170
        '
        'txtMailExpire
        '
        Me.txtMailExpire.Location = New System.Drawing.Point(205, 192)
        Me.txtMailExpire.Name = "txtMailExpire"
        Me.txtMailExpire.Size = New System.Drawing.Size(98, 20)
        Me.txtMailExpire.TabIndex = 173
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Comic Sans MS", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.Color.Crimson
        Me.Label13.Location = New System.Drawing.Point(16, 15)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(180, 23)
        Me.Label13.TabIndex = 172
        Me.Label13.Text = "Enter New Mailing List"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(24, 92)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(165, 13)
        Me.Label14.TabIndex = 170
        Me.Label14.Text = "Optional - Description or Purpose:"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(103, 50)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(86, 13)
        Me.Label15.TabIndex = 169
        Me.Label15.Text = "Name of Mailing:"
        '
        'txtMailName
        '
        Me.txtMailName.Location = New System.Drawing.Point(205, 50)
        Me.txtMailName.MaxLength = 50
        Me.txtMailName.Name = "txtMailName"
        Me.txtMailName.Size = New System.Drawing.Size(274, 20)
        Me.txtMailName.TabIndex = 168
        '
        'tbDenomination
        '
        Me.tbDenomination.Controls.Add(Me.Label22)
        Me.tbDenomination.Controls.Add(Me.Label21)
        Me.tbDenomination.Controls.Add(Me.Label20)
        Me.tbDenomination.Controls.Add(Me.Label19)
        Me.tbDenomination.Controls.Add(Me.Label18)
        Me.tbDenomination.Controls.Add(Me.txtDenomCommon)
        Me.tbDenomination.Controls.Add(Me.btnDenomGrid)
        Me.tbDenomination.Controls.Add(Me.dgvDenominations)
        Me.tbDenomination.Controls.Add(Me.txtDenomWeb)
        Me.tbDenomination.Controls.Add(Me.cboDenomCPO)
        Me.tbDenomination.Controls.Add(Me.btnOKDenom)
        Me.tbDenomination.Controls.Add(Me.Label6)
        Me.tbDenomination.Controls.Add(Me.Label5)
        Me.tbDenomination.Controls.Add(Me.Label4)
        Me.tbDenomination.Controls.Add(Me.Label3)
        Me.tbDenomination.Controls.Add(Me.cboDenomGeneralType)
        Me.tbDenomination.Controls.Add(Me.Label2)
        Me.tbDenomination.Controls.Add(Me.Label1)
        Me.tbDenomination.Controls.Add(Me.txtDenomName)
        Me.tbDenomination.Location = New System.Drawing.Point(4, 22)
        Me.tbDenomination.Name = "tbDenomination"
        Me.tbDenomination.Size = New System.Drawing.Size(920, 568)
        Me.tbDenomination.TabIndex = 2
        Me.tbDenomination.Tag = "Denomination"
        Me.tbDenomination.Text = "Denomination"
        Me.tbDenomination.UseVisualStyleBackColor = True
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Location = New System.Drawing.Point(350, 164)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(75, 13)
        Me.Label22.TabIndex = 20
        Me.Label22.Text = "example: DOC"
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Location = New System.Drawing.Point(350, 64)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(221, 13)
        Me.Label21.TabIndex = 19
        Me.Label21.Text = "example: Christian Church (Disciples of Christ)"
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(350, 90)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(129, 13)
        Me.Label20.TabIndex = 18
        Me.Label20.Text = "example: Christian Church"
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Font = New System.Drawing.Font("Comic Sans MS", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.ForeColor = System.Drawing.Color.Red
        Me.Label19.Location = New System.Drawing.Point(619, 268)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(220, 15)
        Me.Label19.TabIndex = 17
        Me.Label19.Text = "To delete a denomination, send request to "
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(29, 164)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(121, 13)
        Me.Label18.TabIndex = 16
        Me.Label18.Text = "Alternate common name"
        '
        'txtDenomCommon
        '
        Me.txtDenomCommon.Location = New System.Drawing.Point(156, 164)
        Me.txtDenomCommon.Name = "txtDenomCommon"
        Me.txtDenomCommon.Size = New System.Drawing.Size(172, 20)
        Me.txtDenomCommon.TabIndex = 5
        '
        'btnDenomGrid
        '
        Me.btnDenomGrid.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDenomGrid.ForeColor = System.Drawing.Color.Red
        Me.btnDenomGrid.Location = New System.Drawing.Point(20, 268)
        Me.btnDenomGrid.Name = "btnDenomGrid"
        Me.btnDenomGrid.Size = New System.Drawing.Size(202, 22)
        Me.btnDenomGrid.TabIndex = 14
        Me.btnDenomGrid.Text = "View Existing Denominations"
        Me.btnDenomGrid.UseVisualStyleBackColor = True
        Me.btnDenomGrid.Visible = False
        '
        'dgvDenominations
        '
        Me.dgvDenominations.AllowUserToAddRows = False
        Me.dgvDenominations.AllowUserToDeleteRows = False
        Me.dgvDenominations.AllowUserToOrderColumns = True
        Me.dgvDenominations.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.dgvDenominations.AutoGenerateColumns = False
        Me.dgvDenominations.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvDenominations.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DenominationDataGridViewTextBoxColumn, Me.GeneralTypeDataGridViewTextBoxColumn, Me.CommonNameDataGridViewTextBoxColumn, Me.DescriptionDataGridViewTextBoxColumn, Me.WebsiteDataGridViewTextBoxColumn1, Me.CathProtOtherDataGridViewTextBoxColumn, Me.WorkshopGroupingDataGridViewTextBoxColumn, Me.EthnicityDataGridViewTextBoxColumn})
        Me.dgvDenominations.DataSource = Me.BindingSourceDenomination
        Me.dgvDenominations.Location = New System.Drawing.Point(20, 296)
        Me.dgvDenominations.Name = "dgvDenominations"
        Me.dgvDenominations.RowHeadersWidth = 21
        Me.dgvDenominations.Size = New System.Drawing.Size(876, 266)
        Me.dgvDenominations.TabIndex = 13
        '
        'DenominationDataGridViewTextBoxColumn
        '
        Me.DenominationDataGridViewTextBoxColumn.DataPropertyName = "Denomination"
        Me.DenominationDataGridViewTextBoxColumn.HeaderText = "Denomination"
        Me.DenominationDataGridViewTextBoxColumn.Name = "DenominationDataGridViewTextBoxColumn"
        Me.DenominationDataGridViewTextBoxColumn.Width = 150
        '
        'GeneralTypeDataGridViewTextBoxColumn
        '
        Me.GeneralTypeDataGridViewTextBoxColumn.DataPropertyName = "GeneralType"
        Me.GeneralTypeDataGridViewTextBoxColumn.HeaderText = "GeneralType"
        Me.GeneralTypeDataGridViewTextBoxColumn.Name = "GeneralTypeDataGridViewTextBoxColumn"
        Me.GeneralTypeDataGridViewTextBoxColumn.Width = 150
        '
        'CommonNameDataGridViewTextBoxColumn
        '
        Me.CommonNameDataGridViewTextBoxColumn.DataPropertyName = "CommonName"
        Me.CommonNameDataGridViewTextBoxColumn.HeaderText = "CommonName"
        Me.CommonNameDataGridViewTextBoxColumn.Name = "CommonNameDataGridViewTextBoxColumn"
        '
        'DescriptionDataGridViewTextBoxColumn
        '
        Me.DescriptionDataGridViewTextBoxColumn.DataPropertyName = "Description"
        Me.DescriptionDataGridViewTextBoxColumn.HeaderText = "Description"
        Me.DescriptionDataGridViewTextBoxColumn.Name = "DescriptionDataGridViewTextBoxColumn"
        '
        'WebsiteDataGridViewTextBoxColumn1
        '
        Me.WebsiteDataGridViewTextBoxColumn1.DataPropertyName = "Website"
        Me.WebsiteDataGridViewTextBoxColumn1.HeaderText = "Website"
        Me.WebsiteDataGridViewTextBoxColumn1.Name = "WebsiteDataGridViewTextBoxColumn1"
        '
        'CathProtOtherDataGridViewTextBoxColumn
        '
        Me.CathProtOtherDataGridViewTextBoxColumn.DataPropertyName = "CathProtOther"
        Me.CathProtOtherDataGridViewTextBoxColumn.HeaderText = "CathProtOther"
        Me.CathProtOtherDataGridViewTextBoxColumn.Name = "CathProtOtherDataGridViewTextBoxColumn"
        '
        'WorkshopGroupingDataGridViewTextBoxColumn
        '
        Me.WorkshopGroupingDataGridViewTextBoxColumn.DataPropertyName = "WorkshopGrouping"
        Me.WorkshopGroupingDataGridViewTextBoxColumn.HeaderText = "WorkshopGrouping"
        Me.WorkshopGroupingDataGridViewTextBoxColumn.Name = "WorkshopGroupingDataGridViewTextBoxColumn"
        '
        'EthnicityDataGridViewTextBoxColumn
        '
        Me.EthnicityDataGridViewTextBoxColumn.DataPropertyName = "Ethnicity"
        Me.EthnicityDataGridViewTextBoxColumn.HeaderText = "Ethnicity"
        Me.EthnicityDataGridViewTextBoxColumn.Name = "EthnicityDataGridViewTextBoxColumn"
        '
        'BindingSourceDenomination
        '
        Me.BindingSourceDenomination.DataMember = "luDenomType"
        Me.BindingSourceDenomination.DataSource = Me.DsDenom
        '
        'DsDenom
        '
        Me.DsDenom.DataSetName = "dsDenom"
        Me.DsDenom.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'txtDenomWeb
        '
        Me.txtDenomWeb.Location = New System.Drawing.Point(156, 217)
        Me.txtDenomWeb.Name = "txtDenomWeb"
        Me.txtDenomWeb.Size = New System.Drawing.Size(599, 20)
        Me.txtDenomWeb.TabIndex = 9
        '
        'cboDenomCPO
        '
        Me.cboDenomCPO.FormattingEnabled = True
        Me.cboDenomCPO.Items.AddRange(New Object() {"Catholic", "Protestant Affiliated", "Protestant Independent", "Other"})
        Me.cboDenomCPO.Location = New System.Drawing.Point(156, 190)
        Me.cboDenomCPO.Name = "cboDenomCPO"
        Me.cboDenomCPO.Size = New System.Drawing.Size(172, 21)
        Me.cboDenomCPO.TabIndex = 7
        '
        'btnOKDenom
        '
        Me.btnOKDenom.Font = New System.Drawing.Font("Comic Sans MS", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnOKDenom.ForeColor = System.Drawing.Color.Crimson
        Me.btnOKDenom.Location = New System.Drawing.Point(817, 15)
        Me.btnOKDenom.Name = "btnOKDenom"
        Me.btnOKDenom.Size = New System.Drawing.Size(79, 35)
        Me.btnOKDenom.TabIndex = 11
        Me.btnOKDenom.Text = "OK"
        Me.btnOKDenom.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(32, 220)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(118, 13)
        Me.Label6.TabIndex = 9
        Me.Label6.Text = "Denomination's website"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(21, 190)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(129, 13)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "Catholic/Protestant/Other"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Comic Sans MS", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Crimson
        Me.Label4.Location = New System.Drawing.Point(16, 15)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(189, 23)
        Me.Label4.TabIndex = 5
        Me.Label4.Text = "Enter New Denomination"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Comic Sans MS", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Crimson
        Me.Label3.Location = New System.Drawing.Point(16, 124)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(121, 20)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Optional Details"
        '
        'cboDenomGeneralType
        '
        Me.cboDenomGeneralType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboDenomGeneralType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboDenomGeneralType.DisplayMember = "GeneralType"
        Me.cboDenomGeneralType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDenomGeneralType.FormattingEnabled = True
        Me.cboDenomGeneralType.Location = New System.Drawing.Point(81, 87)
        Me.cboDenomGeneralType.Name = "cboDenomGeneralType"
        Me.cboDenomGeneralType.Size = New System.Drawing.Size(247, 21)
        Me.cboDenomGeneralType.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(4, 87)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(71, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "General Type"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(21, 64)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(54, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Full Name"
        '
        'txtDenomName
        '
        Me.txtDenomName.Location = New System.Drawing.Point(81, 61)
        Me.txtDenomName.Name = "txtDenomName"
        Me.txtDenomName.Size = New System.Drawing.Size(247, 20)
        Me.txtDenomName.TabIndex = 0
        '
        'tbCaterer
        '
        Me.tbCaterer.Controls.Add(Me.btnOKCaterer)
        Me.tbCaterer.Controls.Add(Me.SplitContainer1)
        Me.tbCaterer.Location = New System.Drawing.Point(4, 22)
        Me.tbCaterer.Name = "tbCaterer"
        Me.tbCaterer.Size = New System.Drawing.Size(920, 568)
        Me.tbCaterer.TabIndex = 3
        Me.tbCaterer.Tag = "Caterer"
        Me.tbCaterer.Text = "Caterer"
        Me.tbCaterer.UseVisualStyleBackColor = True
        '
        'btnOKCaterer
        '
        Me.btnOKCaterer.Font = New System.Drawing.Font("Comic Sans MS", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnOKCaterer.ForeColor = System.Drawing.Color.Red
        Me.btnOKCaterer.Location = New System.Drawing.Point(837, 12)
        Me.btnOKCaterer.Name = "btnOKCaterer"
        Me.btnOKCaterer.Size = New System.Drawing.Size(80, 35)
        Me.btnOKCaterer.TabIndex = 13
        Me.btnOKCaterer.Text = "OK"
        Me.btnOKCaterer.UseVisualStyleBackColor = True
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SplitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.SplitContainer1.Location = New System.Drawing.Point(3, 12)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtContact)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Label38)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Label32)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtSatellite)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Label30)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Label24)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtEmail)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtCity)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Label29)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtPhone)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtZip)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Label31)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Label37)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Label33)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtWebsite)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Label34)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtNotes)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtStreet)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtCellPhone)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Label28)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtSpecialty)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtState)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Label25)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Label36)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Label26)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Label35)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Label27)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.Label23)
        Me.SplitContainer1.Panel2.Controls.Add(Me.DataGridView1)
        Me.SplitContainer1.Size = New System.Drawing.Size(893, 553)
        Me.SplitContainer1.SplitterDistance = 207
        Me.SplitContainer1.TabIndex = 51
        '
        'txtContact
        '
        Me.txtContact.Location = New System.Drawing.Point(81, 92)
        Me.txtContact.Name = "txtContact"
        Me.txtContact.Size = New System.Drawing.Size(145, 20)
        Me.txtContact.TabIndex = 3
        '
        'Label38
        '
        Me.Label38.AutoSize = True
        Me.Label38.Location = New System.Drawing.Point(647, 43)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(78, 13)
        Me.Label38.TabIndex = 50
        Me.Label38.Text = "SatelliteRegion"
        '
        'Label32
        '
        Me.Label32.AutoSize = True
        Me.Label32.Font = New System.Drawing.Font("Comic Sans MS", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label32.ForeColor = System.Drawing.Color.Crimson
        Me.Label32.Location = New System.Drawing.Point(7, 63)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(121, 20)
        Me.Label32.TabIndex = 25
        Me.Label32.Text = "Optional Details"
        '
        'txtSatellite
        '
        Me.txtSatellite.Location = New System.Drawing.Point(727, 40)
        Me.txtSatellite.Name = "txtSatellite"
        Me.txtSatellite.Size = New System.Drawing.Size(100, 20)
        Me.txtSatellite.TabIndex = 49
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.Location = New System.Drawing.Point(31, 95)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(44, 13)
        Me.Label30.TabIndex = 29
        Me.Label30.Text = "Contact"
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Location = New System.Drawing.Point(508, 43)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(24, 13)
        Me.Label24.TabIndex = 37
        Me.Label24.Text = "City"
        '
        'txtEmail
        '
        Me.txtEmail.Location = New System.Drawing.Point(505, 134)
        Me.txtEmail.Name = "txtEmail"
        Me.txtEmail.Size = New System.Drawing.Size(223, 20)
        Me.txtEmail.TabIndex = 10
        '
        'txtCity
        '
        Me.txtCity.Location = New System.Drawing.Point(532, 40)
        Me.txtCity.Name = "txtCity"
        Me.txtCity.Size = New System.Drawing.Size(93, 20)
        Me.txtCity.TabIndex = 2
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.Location = New System.Drawing.Point(456, 176)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(46, 13)
        Me.Label29.TabIndex = 31
        Me.Label29.Text = "Website"
        '
        'txtPhone
        '
        Me.txtPhone.Location = New System.Drawing.Point(384, 40)
        Me.txtPhone.Name = "txtPhone"
        Me.txtPhone.Size = New System.Drawing.Size(108, 20)
        Me.txtPhone.TabIndex = 1
        '
        'txtZip
        '
        Me.txtZip.Location = New System.Drawing.Point(795, 95)
        Me.txtZip.Name = "txtZip"
        Me.txtZip.Size = New System.Drawing.Size(89, 20)
        Me.txtZip.TabIndex = 7
        '
        'Label31
        '
        Me.Label31.AutoSize = True
        Me.Label31.Font = New System.Drawing.Font("Comic Sans MS", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label31.ForeColor = System.Drawing.Color.Crimson
        Me.Label31.Location = New System.Drawing.Point(16, 5)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(150, 23)
        Me.Label31.TabIndex = 26
        Me.Label31.Text = "Enter New Caterer"
        '
        'Label37
        '
        Me.Label37.AutoSize = True
        Me.Label37.Location = New System.Drawing.Point(467, 134)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(32, 13)
        Me.Label37.TabIndex = 48
        Me.Label37.Text = "Email"
        '
        'Label33
        '
        Me.Label33.AutoSize = True
        Me.Label33.Location = New System.Drawing.Point(345, 43)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(38, 13)
        Me.Label33.TabIndex = 23
        Me.Label33.Text = "Phone"
        '
        'txtWebsite
        '
        Me.txtWebsite.Location = New System.Drawing.Point(505, 176)
        Me.txtWebsite.Name = "txtWebsite"
        Me.txtWebsite.Size = New System.Drawing.Size(223, 20)
        Me.txtWebsite.TabIndex = 11
        '
        'Label34
        '
        Me.Label34.AutoSize = True
        Me.Label34.Location = New System.Drawing.Point(21, 43)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(54, 13)
        Me.Label34.TabIndex = 22
        Me.Label34.Text = "Full Name"
        '
        'txtNotes
        '
        Me.txtNotes.Location = New System.Drawing.Point(301, 120)
        Me.txtNotes.Multiline = True
        Me.txtNotes.Name = "txtNotes"
        Me.txtNotes.Size = New System.Drawing.Size(149, 76)
        Me.txtNotes.TabIndex = 9
        '
        'txtName
        '
        Me.txtName.Location = New System.Drawing.Point(81, 40)
        Me.txtName.Name = "txtName"
        Me.txtName.Size = New System.Drawing.Size(247, 20)
        Me.txtName.TabIndex = 0
        '
        'txtStreet
        '
        Me.txtStreet.Location = New System.Drawing.Point(505, 95)
        Me.txtStreet.Name = "txtStreet"
        Me.txtStreet.Size = New System.Drawing.Size(172, 20)
        Me.txtStreet.TabIndex = 5
        '
        'txtCellPhone
        '
        Me.txtCellPhone.Location = New System.Drawing.Point(301, 92)
        Me.txtCellPhone.Name = "txtCellPhone"
        Me.txtCellPhone.Size = New System.Drawing.Size(149, 20)
        Me.txtCellPhone.TabIndex = 4
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.Location = New System.Drawing.Point(464, 98)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(35, 13)
        Me.Label28.TabIndex = 32
        Me.Label28.Text = "Street"
        '
        'txtSpecialty
        '
        Me.txtSpecialty.AcceptsReturn = True
        Me.txtSpecialty.Location = New System.Drawing.Point(81, 123)
        Me.txtSpecialty.MinimumSize = New System.Drawing.Size(0, 75)
        Me.txtSpecialty.Multiline = True
        Me.txtSpecialty.Name = "txtSpecialty"
        Me.txtSpecialty.Size = New System.Drawing.Size(145, 75)
        Me.txtSpecialty.TabIndex = 8
        '
        'txtState
        '
        Me.txtState.Location = New System.Drawing.Point(718, 95)
        Me.txtState.Name = "txtState"
        Me.txtState.Size = New System.Drawing.Size(37, 20)
        Me.txtState.TabIndex = 6
        Me.txtState.Text = "IN"
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Location = New System.Drawing.Point(767, 98)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(22, 13)
        Me.Label25.TabIndex = 38
        Me.Label25.Text = "Zip"
        '
        'Label36
        '
        Me.Label36.AutoSize = True
        Me.Label36.Location = New System.Drawing.Point(251, 123)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(35, 13)
        Me.Label36.TabIndex = 42
        Me.Label36.Text = "Notes"
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Location = New System.Drawing.Point(684, 98)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(32, 13)
        Me.Label26.TabIndex = 39
        Me.Label26.Text = "State"
        '
        'Label35
        '
        Me.Label35.AutoSize = True
        Me.Label35.Location = New System.Drawing.Point(31, 123)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(50, 13)
        Me.Label35.TabIndex = 41
        Me.Label35.Text = "Specialty"
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.Location = New System.Drawing.Point(237, 95)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(58, 13)
        Me.Label27.TabIndex = 40
        Me.Label27.Text = "Cell Phone"
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Font = New System.Drawing.Font("Comic Sans MS", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.ForeColor = System.Drawing.Color.Red
        Me.Label23.Location = New System.Drawing.Point(8, 6)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(243, 15)
        Me.Label23.TabIndex = 3
        Me.Label23.Text = "Edit Caterer name and information in the grid."
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.AllowUserToOrderColumns = True
        Me.DataGridView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridView1.AutoGenerateColumns = False
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.CatererID, Me.SatelliteRegionDataGridViewTextBoxColumn, Me.CatererNameDataGridViewTextBoxColumn, Me.PhoneDataGridViewTextBoxColumn, Me.ContactDataGridViewTextBoxColumn, Me.CellPhoneDataGridViewTextBoxColumn, Me.SpecialtyDataGridViewTextBoxColumn, Me.NotesDataGridViewTextBoxColumn, Me.CityDataGridViewTextBoxColumn1, Me.StateDataGridViewTextBoxColumn1, Me.ZipDataGridViewTextBoxColumn1, Me.WebsiteDataGridViewTextBoxColumn2, Me.EmailDataGridViewTextBoxColumn1, Me.StaffNumDataGridViewTextBoxColumn})
        Me.DataGridView1.DataSource = Me.TblCatererBindingSource
        Me.DataGridView1.Location = New System.Drawing.Point(16, 29)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.RowHeadersWidth = 20
        Me.DataGridView1.Size = New System.Drawing.Size(883, 328)
        Me.DataGridView1.TabIndex = 12
        '
        'CatererID
        '
        Me.CatererID.DataPropertyName = "CatererID"
        Me.CatererID.HeaderText = "Column1"
        Me.CatererID.Name = "CatererID"
        Me.CatererID.ReadOnly = True
        Me.CatererID.Visible = False
        '
        'SatelliteRegionDataGridViewTextBoxColumn
        '
        Me.SatelliteRegionDataGridViewTextBoxColumn.DataPropertyName = "SatelliteRegion"
        Me.SatelliteRegionDataGridViewTextBoxColumn.HeaderText = "SatelliteRegion"
        Me.SatelliteRegionDataGridViewTextBoxColumn.Name = "SatelliteRegionDataGridViewTextBoxColumn"
        Me.SatelliteRegionDataGridViewTextBoxColumn.Width = 40
        '
        'CatererNameDataGridViewTextBoxColumn
        '
        Me.CatererNameDataGridViewTextBoxColumn.DataPropertyName = "CatererName"
        Me.CatererNameDataGridViewTextBoxColumn.HeaderText = "CatererName"
        Me.CatererNameDataGridViewTextBoxColumn.Name = "CatererNameDataGridViewTextBoxColumn"
        Me.CatererNameDataGridViewTextBoxColumn.Width = 110
        '
        'PhoneDataGridViewTextBoxColumn
        '
        Me.PhoneDataGridViewTextBoxColumn.DataPropertyName = "Phone"
        Me.PhoneDataGridViewTextBoxColumn.HeaderText = "Phone"
        Me.PhoneDataGridViewTextBoxColumn.Name = "PhoneDataGridViewTextBoxColumn"
        Me.PhoneDataGridViewTextBoxColumn.Width = 75
        '
        'ContactDataGridViewTextBoxColumn
        '
        Me.ContactDataGridViewTextBoxColumn.DataPropertyName = "Contact"
        Me.ContactDataGridViewTextBoxColumn.HeaderText = "Contact"
        Me.ContactDataGridViewTextBoxColumn.Name = "ContactDataGridViewTextBoxColumn"
        '
        'CellPhoneDataGridViewTextBoxColumn
        '
        Me.CellPhoneDataGridViewTextBoxColumn.DataPropertyName = "CellPhone"
        Me.CellPhoneDataGridViewTextBoxColumn.HeaderText = "CellPhone"
        Me.CellPhoneDataGridViewTextBoxColumn.Name = "CellPhoneDataGridViewTextBoxColumn"
        Me.CellPhoneDataGridViewTextBoxColumn.Width = 75
        '
        'SpecialtyDataGridViewTextBoxColumn
        '
        Me.SpecialtyDataGridViewTextBoxColumn.DataPropertyName = "Specialty"
        Me.SpecialtyDataGridViewTextBoxColumn.HeaderText = "Specialty"
        Me.SpecialtyDataGridViewTextBoxColumn.Name = "SpecialtyDataGridViewTextBoxColumn"
        '
        'NotesDataGridViewTextBoxColumn
        '
        Me.NotesDataGridViewTextBoxColumn.DataPropertyName = "Notes"
        Me.NotesDataGridViewTextBoxColumn.HeaderText = "Notes"
        Me.NotesDataGridViewTextBoxColumn.Name = "NotesDataGridViewTextBoxColumn"
        '
        'CityDataGridViewTextBoxColumn1
        '
        Me.CityDataGridViewTextBoxColumn1.DataPropertyName = "City"
        Me.CityDataGridViewTextBoxColumn1.HeaderText = "City"
        Me.CityDataGridViewTextBoxColumn1.Name = "CityDataGridViewTextBoxColumn1"
        Me.CityDataGridViewTextBoxColumn1.Width = 60
        '
        'StateDataGridViewTextBoxColumn1
        '
        Me.StateDataGridViewTextBoxColumn1.DataPropertyName = "State"
        Me.StateDataGridViewTextBoxColumn1.HeaderText = "State"
        Me.StateDataGridViewTextBoxColumn1.Name = "StateDataGridViewTextBoxColumn1"
        Me.StateDataGridViewTextBoxColumn1.Width = 20
        '
        'ZipDataGridViewTextBoxColumn1
        '
        Me.ZipDataGridViewTextBoxColumn1.DataPropertyName = "Zip"
        Me.ZipDataGridViewTextBoxColumn1.HeaderText = "Zip"
        Me.ZipDataGridViewTextBoxColumn1.Name = "ZipDataGridViewTextBoxColumn1"
        Me.ZipDataGridViewTextBoxColumn1.Width = 50
        '
        'WebsiteDataGridViewTextBoxColumn2
        '
        Me.WebsiteDataGridViewTextBoxColumn2.DataPropertyName = "Website"
        Me.WebsiteDataGridViewTextBoxColumn2.HeaderText = "Website"
        Me.WebsiteDataGridViewTextBoxColumn2.Name = "WebsiteDataGridViewTextBoxColumn2"
        '
        'EmailDataGridViewTextBoxColumn1
        '
        Me.EmailDataGridViewTextBoxColumn1.DataPropertyName = "Email"
        Me.EmailDataGridViewTextBoxColumn1.HeaderText = "Email"
        Me.EmailDataGridViewTextBoxColumn1.Name = "EmailDataGridViewTextBoxColumn1"
        '
        'StaffNumDataGridViewTextBoxColumn
        '
        Me.StaffNumDataGridViewTextBoxColumn.DataPropertyName = "StaffNum"
        Me.StaffNumDataGridViewTextBoxColumn.HeaderText = "StaffNum"
        Me.StaffNumDataGridViewTextBoxColumn.Name = "StaffNumDataGridViewTextBoxColumn"
        Me.StaffNumDataGridViewTextBoxColumn.ReadOnly = True
        '
        'TblCatererBindingSource
        '
        Me.TblCatererBindingSource.DataMember = "tblCaterer"
        Me.TblCatererBindingSource.DataSource = Me.DsCaterer
        '
        'DsCaterer
        '
        Me.DsCaterer.DataSetName = "dsCaterer"
        Me.DsCaterer.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'tbResourceGuide
        '
        Me.tbResourceGuide.Controls.Add(Me.btnOKRG)
        Me.tbResourceGuide.Controls.Add(Me.btnRG)
        Me.tbResourceGuide.Controls.Add(Me.dgvRG)
        Me.tbResourceGuide.Location = New System.Drawing.Point(4, 22)
        Me.tbResourceGuide.Name = "tbResourceGuide"
        Me.tbResourceGuide.Padding = New System.Windows.Forms.Padding(3)
        Me.tbResourceGuide.Size = New System.Drawing.Size(920, 568)
        Me.tbResourceGuide.TabIndex = 4
        Me.tbResourceGuide.Tag = "ResourceGuide"
        Me.tbResourceGuide.Text = "Resource Guides"
        Me.tbResourceGuide.UseVisualStyleBackColor = True
        '
        'btnOKRG
        '
        Me.btnOKRG.Font = New System.Drawing.Font("Comic Sans MS", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnOKRG.ForeColor = System.Drawing.Color.Crimson
        Me.btnOKRG.Location = New System.Drawing.Point(813, 15)
        Me.btnOKRG.Name = "btnOKRG"
        Me.btnOKRG.Size = New System.Drawing.Size(80, 35)
        Me.btnOKRG.TabIndex = 177
        Me.btnOKRG.Text = "OK"
        Me.btnOKRG.UseVisualStyleBackColor = True
        '
        'btnRG
        '
        Me.btnRG.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRG.ForeColor = System.Drawing.Color.Red
        Me.btnRG.Location = New System.Drawing.Point(6, 38)
        Me.btnRG.Name = "btnRG"
        Me.btnRG.Size = New System.Drawing.Size(202, 22)
        Me.btnRG.TabIndex = 15
        Me.btnRG.Text = "View Resource Guides"
        Me.btnRG.UseVisualStyleBackColor = True
        '
        'dgvRG
        '
        Me.dgvRG.AllowUserToDeleteRows = False
        Me.dgvRG.AutoGenerateColumns = False
        Me.dgvRG.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvRG.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.RGNameDataGridViewTextBoxColumn, Me.ReceivedDtDataGridViewTextBoxColumn, Me.ResDirReportDtDataGridViewTextBoxColumn, Me.OrderDtDataGridViewTextBoxColumn, Me.ForwardDtDataGridViewTextBoxColumn, Me.ShortNameDataGridViewTextBoxColumn})
        Me.dgvRG.DataSource = Me.ProcessRGBindingSource
        Me.dgvRG.Location = New System.Drawing.Point(6, 86)
        Me.dgvRG.Name = "dgvRG"
        Me.dgvRG.RowHeadersWidth = 15
        Me.dgvRG.Size = New System.Drawing.Size(908, 243)
        Me.dgvRG.TabIndex = 0
        '
        'RGNameDataGridViewTextBoxColumn
        '
        Me.RGNameDataGridViewTextBoxColumn.DataPropertyName = "RGName"
        Me.RGNameDataGridViewTextBoxColumn.HeaderText = "RGName"
        Me.RGNameDataGridViewTextBoxColumn.Name = "RGNameDataGridViewTextBoxColumn"
        Me.RGNameDataGridViewTextBoxColumn.Width = 300
        '
        'ReceivedDtDataGridViewTextBoxColumn
        '
        Me.ReceivedDtDataGridViewTextBoxColumn.DataPropertyName = "ReceivedDt"
        Me.ReceivedDtDataGridViewTextBoxColumn.HeaderText = "ReceivedDt"
        Me.ReceivedDtDataGridViewTextBoxColumn.Name = "ReceivedDtDataGridViewTextBoxColumn"
        '
        'ResDirReportDtDataGridViewTextBoxColumn
        '
        Me.ResDirReportDtDataGridViewTextBoxColumn.DataPropertyName = "ResDirReportDt"
        Me.ResDirReportDtDataGridViewTextBoxColumn.HeaderText = "ResDirReportDt"
        Me.ResDirReportDtDataGridViewTextBoxColumn.Name = "ResDirReportDtDataGridViewTextBoxColumn"
        '
        'OrderDtDataGridViewTextBoxColumn
        '
        Me.OrderDtDataGridViewTextBoxColumn.DataPropertyName = "OrderDt"
        Me.OrderDtDataGridViewTextBoxColumn.HeaderText = "OrderDt"
        Me.OrderDtDataGridViewTextBoxColumn.Name = "OrderDtDataGridViewTextBoxColumn"
        '
        'ForwardDtDataGridViewTextBoxColumn
        '
        Me.ForwardDtDataGridViewTextBoxColumn.DataPropertyName = "ForwardDt"
        Me.ForwardDtDataGridViewTextBoxColumn.HeaderText = "ForwardDt"
        Me.ForwardDtDataGridViewTextBoxColumn.Name = "ForwardDtDataGridViewTextBoxColumn"
        '
        'ShortNameDataGridViewTextBoxColumn
        '
        Me.ShortNameDataGridViewTextBoxColumn.DataPropertyName = "ShortName"
        Me.ShortNameDataGridViewTextBoxColumn.HeaderText = "ShortName"
        Me.ShortNameDataGridViewTextBoxColumn.Name = "ShortNameDataGridViewTextBoxColumn"
        Me.ShortNameDataGridViewTextBoxColumn.Width = 175
        '
        'ProcessRGBindingSource
        '
        Me.ProcessRGBindingSource.DataMember = "ProcessRG"
        Me.ProcessRGBindingSource.DataSource = Me.DsResourceGuideProcess
        '
        'DsResourceGuideProcess
        '
        Me.DsResourceGuideProcess.DataSetName = "dsResourceGuideProcess"
        Me.DsResourceGuideProcess.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'LuDenomTypeBindingSource
        '
        Me.LuDenomTypeBindingSource.DataMember = "luDenomType"
        Me.LuDenomTypeBindingSource.DataSource = Me.DsDenom
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(68, 249)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(129, 13)
        Me.Label7.TabIndex = 8
        Me.Label7.Text = "Catholic/Protestant/Other"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Comic Sans MS", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.Crimson
        Me.Label8.Location = New System.Drawing.Point(22, 31)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(198, 23)
        Me.Label8.TabIndex = 5
        Me.Label8.Text = "Enter New Denomination"
        '
        'ComboBox1
        '
        Me.ComboBox1.DisplayMember = "GeneralType"
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Location = New System.Drawing.Point(219, 126)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(274, 21)
        Me.ComboBox1.TabIndex = 3
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(115, 126)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(71, 13)
        Me.Label9.TabIndex = 2
        Me.Label9.Text = "General Type"
        '
        'ComboBox2
        '
        Me.ComboBox2.FormattingEnabled = True
        Me.ComboBox2.Items.AddRange(New Object() {"Catholic", "Protestant Affiliated", "Protestant Independent", "Other"})
        Me.ComboBox2.Location = New System.Drawing.Point(219, 249)
        Me.ComboBox2.Name = "ComboBox2"
        Me.ComboBox2.Size = New System.Drawing.Size(274, 21)
        Me.ComboBox2.TabIndex = 11
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(219, 288)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(585, 20)
        Me.TextBox1.TabIndex = 12
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(93, 288)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(118, 13)
        Me.Label10.TabIndex = 9
        Me.Label10.Text = "Denomination's website"
        '
        'Button2
        '
        Me.Button2.Font = New System.Drawing.Font("Comic Sans MS", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button2.ForeColor = System.Drawing.Color.Crimson
        Me.Button2.Location = New System.Drawing.Point(738, 19)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(114, 46)
        Me.Button2.TabIndex = 6
        Me.Button2.Text = "OK"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Comic Sans MS", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.Color.Crimson
        Me.Label11.Location = New System.Drawing.Point(22, 196)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(134, 23)
        Me.Label11.TabIndex = 4
        Me.Label11.Text = "Optional Details"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(68, 87)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(128, 13)
        Me.Label12.TabIndex = 1
        Me.Label12.Text = "New Denomination Name"
        '
        'TextBox2
        '
        Me.TextBox2.Location = New System.Drawing.Point(219, 87)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(274, 20)
        Me.TextBox2.TabIndex = 0
        '
        'LuDenomTypeTableAdapter
        '
        Me.LuDenomTypeTableAdapter.ClearBeforeFill = True
        '
        'TblCatererTableAdapter
        '
        Me.TblCatererTableAdapter.ClearBeforeFill = True
        '
        'ProcessRGTableAdapter
        '
        Me.ProcessRGTableAdapter.ClearBeforeFill = True
        '
        'MainMailListTableAdapter
        '
        Me.MainMailListTableAdapter.ClearBeforeFill = True
        '
        'tbRegistrationCopy
        '
        Me.tbRegistrationCopy.Controls.Add(Me.lblRegIDtoCopy)
        Me.tbRegistrationCopy.Controls.Add(Me.lblRegistrationDetail)
        Me.tbRegistrationCopy.Controls.Add(Me.Label42)
        Me.tbRegistrationCopy.Controls.Add(Me.btnRegCopyDone)
        Me.tbRegistrationCopy.Controls.Add(Me.btnRefreshEvents)
        Me.tbRegistrationCopy.Controls.Add(Label40)
        Me.tbRegistrationCopy.Controls.Add(Me.cboEvent)
        Me.tbRegistrationCopy.Location = New System.Drawing.Point(4, 22)
        Me.tbRegistrationCopy.Name = "tbRegistrationCopy"
        Me.tbRegistrationCopy.Size = New System.Drawing.Size(920, 568)
        Me.tbRegistrationCopy.TabIndex = 5
        Me.tbRegistrationCopy.Tag = "RegistrationCopy"
        Me.tbRegistrationCopy.Text = "Copy Registration"
        Me.tbRegistrationCopy.UseVisualStyleBackColor = True
        '
        'btnRefreshEvents
        '
        Me.btnRefreshEvents.Location = New System.Drawing.Point(506, 96)
        Me.btnRefreshEvents.Name = "btnRefreshEvents"
        Me.btnRefreshEvents.Size = New System.Drawing.Size(106, 23)
        Me.btnRefreshEvents.TabIndex = 617
        Me.btnRefreshEvents.TabStop = False
        Me.btnRefreshEvents.Tag = ""
        Me.btnRefreshEvents.Text = "refresh Events"
        Me.btnRefreshEvents.UseVisualStyleBackColor = True
        '
        'Label40
        '
        Label40.AutoSize = True
        Label40.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Label40.Location = New System.Drawing.Point(18, 96)
        Label40.Margin = New System.Windows.Forms.Padding(0)
        Label40.Name = "Label40"
        Label40.Size = New System.Drawing.Size(40, 15)
        Label40.TabIndex = 616
        Label40.Text = "Event:"
        '
        'cboEvent
        '
        Me.cboEvent.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboEvent.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboEvent.DisplayMember = "EventName"
        Me.cboEvent.DropDownWidth = 500
        Me.cboEvent.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboEvent.FormattingEnabled = True
        Me.cboEvent.Location = New System.Drawing.Point(71, 96)
        Me.cboEvent.Name = "cboEvent"
        Me.cboEvent.RestrictContentToListItems = True
        Me.cboEvent.Size = New System.Drawing.Size(420, 23)
        Me.cboEvent.TabIndex = 615
        Me.cboEvent.TabStop = False
        Me.cboEvent.Tag = "Event"
        Me.cboEvent.ValueMember = "EventID"
        '
        'btnRegCopyDone
        '
        Me.btnRegCopyDone.Location = New System.Drawing.Point(797, 33)
        Me.btnRegCopyDone.Name = "btnRegCopyDone"
        Me.btnRegCopyDone.Size = New System.Drawing.Size(84, 39)
        Me.btnRegCopyDone.TabIndex = 618
        Me.btnRegCopyDone.Text = "Done"
        Me.btnRegCopyDone.UseVisualStyleBackColor = True
        '
        'Label42
        '
        Me.Label42.AutoSize = True
        Me.Label42.Font = New System.Drawing.Font("Comic Sans MS", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label42.ForeColor = System.Drawing.Color.Crimson
        Me.Label42.Location = New System.Drawing.Point(43, 33)
        Me.Label42.Name = "Label42"
        Me.Label42.Size = New System.Drawing.Size(256, 23)
        Me.Label42.TabIndex = 620
        Me.Label42.Text = "Select new Event then click Done."
        '
        'lblRegistrationDetail
        '
        Me.lblRegistrationDetail.AutoSize = True
        Me.lblRegistrationDetail.Location = New System.Drawing.Point(114, 176)
        Me.lblRegistrationDetail.Name = "lblRegistrationDetail"
        Me.lblRegistrationDetail.Size = New System.Drawing.Size(45, 13)
        Me.lblRegistrationDetail.TabIndex = 621
        Me.lblRegistrationDetail.Text = "Label41"
        '
        'lblRegIDtoCopy
        '
        Me.lblRegIDtoCopy.AutoSize = True
        Me.lblRegIDtoCopy.Location = New System.Drawing.Point(117, 206)
        Me.lblRegIDtoCopy.Name = "lblRegIDtoCopy"
        Me.lblRegIDtoCopy.Size = New System.Drawing.Size(45, 13)
        Me.lblRegIDtoCopy.TabIndex = 622
        Me.lblRegIDtoCopy.Text = "Label41"
        '
        'frmAddNew
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(977, 666)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.BindingNavigatorPublisher)
        Me.Name = "frmAddNew"
        Me.Text = "ADD ITEM"
        CType(Me.dgvPublisher, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BindingSourcePublisher, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsPublisher1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BindingNavigatorPublisher, System.ComponentModel.ISupportInitialize).EndInit()
        Me.BindingNavigatorPublisher.ResumeLayout(False)
        Me.BindingNavigatorPublisher.PerformLayout()
        Me.TabControl1.ResumeLayout(False)
        Me.tbPublisher.ResumeLayout(False)
        Me.tbPublisher.PerformLayout()
        Me.tbSavedMailList.ResumeLayout(False)
        Me.tbSavedMailList.PerformLayout()
        CType(Me.dgvMailings, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsMainMailList1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tbDenomination.ResumeLayout(False)
        Me.tbDenomination.PerformLayout()
        CType(Me.dgvDenominations, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BindingSourceDenomination, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsDenom, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tbCaterer.ResumeLayout(False)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.Panel2.PerformLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TblCatererBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsCaterer, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tbResourceGuide.ResumeLayout(False)
        CType(Me.dgvRG, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ProcessRGBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsResourceGuideProcess, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LuDenomTypeBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tbRegistrationCopy.ResumeLayout(False)
        Me.tbRegistrationCopy.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

#Region "Load"
    Private Sub frmAddNew_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Forms.Add(Me)
    End Sub

    'DISABLES X CLOSE BUTTON IN CONTROL BOX
    Protected Overrides ReadOnly Property CreateParams() As CreateParams
        Get
            Dim cp As CreateParams = MyBase.CreateParams
            Const CS_NOCLOSE As Integer = &H200
            cp.ClassStyle = cp.ClassStyle Or CS_NOCLOSE
            Return cp
        End Get
    End Property

    Private Sub frm_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) _
            Handles MyBase.FormClosing
        Me.Dispose()
        ' Return Nothing
    End Sub

#End Region 'load

#Region "Publisher"
    Public Sub LoadPublishers()
        Me.TblPublisherTableAdapter.Fill(Me.DsPublisher1.tblPublisher)
    End Sub

    Private Sub btnOKPublisher_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOKPublisher.Click
        If Me.DsPublisher1.HasChanges Then
            Try
                Me.TblPublisherTableAdapter.Update(Me.DsPublisher1.tblPublisher)
                '     Me.StatusBarPanel1.Text = "Publisher Updated"
            Catch ex As Exception
                '    Me.StatusBarPanel1.Text = "Publisher update FAILED"
                modGlobalVar.msg("ERROR: Publisher update", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
        Me.Close()
    End Sub
#End Region 'publisher

#Region "Resource Guide"
    Private Sub btnRGClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRG.Click
        'TODO: This line of code loads data into the 'DsResourceGuideProcess.ProcessRG' table. You can move, or remove it, as needed.
        If usr = ResourceAdmin.StaffID Or usr = DBAdmin.StaffID Then
            Me.ProcessRGTableAdapter.Fill(Me.DsResourceGuideProcess.ProcessRG)
            Me.dgvRG.Visible = True
            CloseButton.Disable(Me)
        Else
            modGlobalVar.msg("Permission Denied", "This task reserved for Resource Admin" & NextLine & ResourceAdmin.StaffName, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Sub btnOKRG_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOKRG.Click
        If Me.DsResourceGuideProcess.HasChanges Then
            Try
                Me.ProcessRGTableAdapter.Update(Me.DsResourceGuideProcess.Tables(0))
                '     Me.StatusBarPanel1.Text = "Publisher Updated"
            Catch ex As Exception
                '    Me.StatusBarPanel1.Text = "Publisher update FAILED"
                modGlobalVar.msg("ERROR: Resource Guide update", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
        Me.Close()
    End Sub

#End Region 'resource guide

#Region "Denomination"

    Private Sub btnDenomGrid_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDenomGrid.Click
        FillDenomGrid()
    End Sub

    Public Sub FillDenomGrid()
        Try
            Me.LuDenomTypeTableAdapter.Fill(Me.DsDenom.luDenomType)
            ' Me.dgvDenominations.Visible = True
        Catch ex As Exception
            modGlobalVar.msg("can't fill denomin grid", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub LoadDenomType()
        Dim sql As New SqlCommand("SELECT distinct GeneralType from luDenomType order by generaltype", sc)
        Dim dt As New DataTable("dtDenom")
        Dim dr As SqlDataReader
        SCConnect()
        dr = sql.ExecuteReader(System.Data.CommandBehavior.CloseConnection)
        dt.Load(dr)
        Me.cboDenomGeneralType.DataSource = dt
        dr.Close()
        dr = Nothing
        sql = Nothing
    End Sub

    Private Sub btnOKDenom_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnOKDenom.Click
        'If Me.DataGridView1.Visible = True Then
        If Me.DsDenom.HasChanges Then
            Try 'null errors without catch
                Me.LuDenomTypeTableAdapter.Update(Me.DsDenom.luDenomType)
            Catch ex As Exception
            End Try
        End If
        If Me.txtDenomName.Text > "" Then
            If modPopup.InsertNewRecord("INSERT INTO ludenomtype(Denomination, GeneralType, CommonName, CathProtOTher, Website, ImportGrouping) VALUES (N'" & Me.txtDenomName.Text & "', N'" & Me.cboDenomGeneralType.Text & "',N'" & Me.txtDenomCommon.Text & "',N'" & Me.cboDenomCPO.Text & "',N'" & Me.txtDenomWeb.Text & "'," & usr & ")") = True Then
                ClassOpenForms.frmMainOrg.fldDenomination.Text = Me.txtDenomName.Text
                '  ClassOpenForms.frmMainOrg.bChanged = True
            End If
        Else
            If Me.dgvDenominations.SelectedRows.Count > 0 Then
                ClassOpenForms.frmMainOrg.fldDenomination.Text = Me.dgvDenominations.Item("DenominationDataGridViewTextBoxColumn", Me.dgvDenominations.SelectedRows(0).Index).Value
            Else
                msg(MsgCodes.noRowSelected)
            End If
        End If
        Me.Close()
    End Sub

#End Region 'Denomination

#Region "MailFlag"

    Public Sub LoadMailFlag()
        'TODO now that columns are created, tableadapter not needed
        Try
            Me.MainMailListTableAdapter.Fill(Me.DsMainMailList1.MainMailList, usr)
        Catch ex As Exception
            modGlobalVar.msg("can't fill mail list grid", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
    End Sub

    Private Sub btnOKMailFlag_Click(sender As System.Object, e As System.EventArgs) Handles btnOKMailFlag.Click
        If Me.txtMailName.Text > "" Then
            Dim sql As New SqlCommand
            sql.Connection = sc
            'prevent duplicate names

            If Me.txtMailExpire.Text > " " Then
                sql.CommandText = " INSERT INTO luMailList (MailListName, CreateStaffNum, Description, CreateDate, ExpirationDate) VALUES ('" _
                                     & Me.txtMailName.Text & "'," & usr & ",' " & Me.txtMailDescription.Text & "', N'" & Today & "', N'" & CType(Me.txtMailExpire.Text, Date).ToShortDateString & "')"
            Else
                sql.CommandText = " INSERT INTO luMailList (MailListName, CreateStaffNum, Description, CreateDate) VALUES ('" _
                                      & Me.txtMailName.Text & "'," & usr & ",' " & Me.txtMailDescription.Text & "', N'" & Today & "')" 'N'"
            End If
            '  MsgBox(sql.CommandText, , "qry string")
            Try
                If SCConnect() Then
                    sql.ExecuteNonQuery()
                    sc.Close()
                End If
                returnval = Me.txtMailName.Text
                ' Catch ex As System.Data.ConstraintException 'why doesn't work if is constraint error??
            Catch ex As SqlException
                modGlobalVar.msg("ERROR creating: " & Me.txtMailName.Text, ex.Message & NextLine & "This mailing list name may already exist. " & NextLine & "Change the name a little, or add your initials.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End Try
            'no need to reload as form closes' LoadMailFlag()
        End If
        Me.Close()
    End Sub

#End Region 'special mail flag

#Region "Caterer"

    Public Sub LoadCaterer()
        Me.TblCatererTableAdapter.Fill(Me.DsCaterer.tblCaterer)
        Me.txtSatellite.Text = usrRegion
    End Sub

    Private Sub btnOKCaterer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOKCaterer.Click
        If Me.DsCaterer.HasChanges Then
            Try
                Me.TblCatererTableAdapter.Update(Me.DsCaterer.tblCaterer)
            Catch ex As Exception
                modGlobalVar.msg("ERROR: Caterer update", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If

        'SAVE NEW
        If Me.txtName.Text > "" Then
            Dim sql As New SqlCommand("INSERT INTO tblCaterer (SatelliteRegion, CatererName, Street, City, State, Zip, Phone, CellPhone, Website, Contact, Email, Specialty, Notes, StaffNum)" _
            & " VALUES     ('" & IsNull(Me.txtSatellite.Text, usrRegion) & "','" & Me.txtName.Text & "','" & Me.txtStreet.Text & "','" & Me.txtCity.Text & "','" & Me.txtState.Text & "','" & Me.txtZip.Text & "','" & Me.txtPhone.Text & "','" & Me.txtCellPhone.Text & "','" & Me.txtWebsite.Text & "','" & Me.txtContact.Text & "','" & Me.txtEmail.Text & "','" & Me.txtSpecialty.Text & "','" & Me.txtNotes.Text & "'," & usr & ")", sc)

            If Me.txtCity.Text > "" Then
                SCConnect()
                sql.ExecuteScalar()
            Else
                modGlobalVar.msg("Cancelling action - incomplete information", "Please enter a City", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If
        Else
            If Me.txtContact.Text > "" Or Me.txtPhone.Text > "" Then
                'If modGlobalVar.Msg("ATTENTION: missing information", "new Caterer Name missing." & NextLine & "Are you trying to enter a new caterer?", MessageBoxIcon.Exclamation) = DialogResult.Yes Then
                '    Exit Sub
                'End If
            End If
        End If
        Me.Close()

    End Sub
#End Region 'caterer

#Region "Registration"

    'upcoming events dd
    Public Sub LoadEventDD()
        modGlobalVar.LoadWEventDD("upcoming")
        Me.cboEvent.DataSource = tblWEvents
        Me.cboEvent.SelectedIndex = -1
    End Sub

    'specific events dd
    Public Sub LoadEventDD(ByVal strChecklistName As String)
        modGlobalVar.LoadWEventDD("upcoming")
        Me.cboEvent.DataSource = tblWEvents
        Me.cboEvent.SelectedIndex = -1
    End Sub


    'Reload all EVENTS dd
    Private Sub btnRefreshEvents_Click(sender As System.Object, e As System.EventArgs) Handles btnRefreshEvents.Click
        modGlobalVar.LoadWEventDD("all")
        Me.cboEvent.DataSource = tblWEvents
        Me.cboEvent.SelectedIndex = -1
    End Sub

    Private Sub btnRegCopyDone_Click(sender As System.Object, e As System.EventArgs) Handles btnRegCopyDone.Click
        Dim sql As New SqlCommand("[RegistrationCopy]")
        sql.CommandType = CommandType.StoredProcedure
        sql.Connection = sc
        Try
            sql.Parameters.Add("@EventID", SqlDbType.Int).Value = Me.cboEvent.SelectedValue
            sql.Parameters.Add("@Num", SqlDbType.Int).Value = Me.lblRegIDtoCopy.Text
            If Not SCConnect() Then
                Exit Sub
            End If

            sql.ExecuteNonQuery()

        Catch ex As Exception
            MsgBox(ex.Message & NextLine & sql.Parameters("@EventID").Value.ToString, , "Error Copy REGISTRATION")
        Finally
            sc.Close()

        End Try

        sql = Nothing
        Me.Close()
    End Sub


#End Region 'registration

  
End Class
