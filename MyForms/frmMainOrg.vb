
Option Explicit On
Imports System
Imports System.Data.SqlClient
'Imports System.Drawing.Bitmap
Imports System.Text
Imports System.IO



Public Class frmMainOrg
    Inherits System.Windows.Forms.Form

    Dim isloaded As Boolean = False
    Dim tbl As DataTable  'flexible datagrid
    Dim dv As DataView 'filter for each datagrid
    Dim statusM, statusS1, statusS2 As String 'filter text for status bar
    Dim hti As System.Windows.Forms.DataGrid.HitTestInfo
    Dim arAddress As String() 'user selected city or zip from menu, don't ask about other
    Dim FullHostName As String
    Dim enumContact, enumCase, enumGrant As structHeadings
    Dim cntContact, cntCase As Integer 'to retain count to restore grid heading after filter 
    '===== for Detail form  closing routines
    Dim ctlIdentify As Control 'fields for delete and messages
    Dim ctlNeutral As Control 'will never be invalidated
    Dim objHowClose As Short  'identify object calling close
    Dim mainTopic As String 'name of entity of this form - case, contact, etc
    Dim mainDS As DataSet 'for generic module calls like CloseDetailForm
    Dim maintbl As DataTable
    Dim mainDAdapt As SqlDataAdapter
    Dim mainBSrce As System.Windows.Forms.BindingSource
    Dim bDirty As Boolean = False 'denomination cbo
    Public ThisID As Integer 'id of this item
    Const LinkOrgPath As String = LinkedPath & "Organizations\"
    Private tConcurrency As New dsMainOrgWAddress.tblOrg1DataTable

    '====
    'to open resource form
    Dim t As Int16 = 0  'to limit timer

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

#End Region

#Region " Windows Form Designer generated code "
    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents SqlInsertCommand As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlDeleteCommand As System.Data.SqlClient.SqlCommand
    Friend WithEvents sqlUpdateAddress As System.Data.SqlClient.SqlCommand

    Friend WithEvents btnTest As System.Windows.Forms.Button
    Friend WithEvents btnTest2 As System.Windows.Forms.Button
    Friend WithEvents editStateAB As System.Windows.Forms.TextBox
    Friend WithEvents lblDenomination As System.Windows.Forms.Label
    Friend WithEvents MainOrgBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents fldLastChangeStaff As System.Windows.Forms.TextBox
    Friend WithEvents fldCreateStaff As System.Windows.Forms.TextBox
    Friend WithEvents lblExtra As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents cboEthnicity As InfoCtr.ComboBoxRelaxed
    Friend WithEvents tbAddress As System.Windows.Forms.TabControl
    Friend WithEvents pgMailing As System.Windows.Forms.TabPage
    Friend WithEvents pgPhysical As System.Windows.Forms.TabPage
    Friend WithEvents editPhysicalZip As System.Windows.Forms.TextBox
    Friend WithEvents fldPhysicalAddress As System.Windows.Forms.TextBox
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents EditPhysicalCity As System.Windows.Forms.TextBox
    Friend WithEvents editPhysicalState As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents editPhysicalCountry As System.Windows.Forms.TextBox
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents editPhysicalStreet As System.Windows.Forms.TextBox
    Friend WithEvents btnNewPhysical As System.Windows.Forms.Button
    Friend WithEvents sqlSelectOrg As System.Data.SqlClient.SqlCommand
    Friend WithEvents DsMainOrgWAddress1 As InfoCtr.dsMainOrgWAddress
    Friend WithEvents sqlUpdateOrg As System.Data.SqlClient.SqlCommand
    Friend WithEvents sqlSelectAddress As System.Data.SqlClient.SqlCommand
    Friend WithEvents AddressBindingSource As System.Windows.Forms.BindingSource

    Friend WithEvents fldLastChangeDate As System.Windows.Forms.TextBox
    Friend WithEvents fldCreateDate As System.Windows.Forms.TextBox
    Friend WithEvents SqlCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents pnlAddress As System.Windows.Forms.Panel
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents lblZip As System.Windows.Forms.Label
    Friend WithEvents lblCity As System.Windows.Forms.Label
    Friend WithEvents DataGridTextBoxColumn19 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents btnHelp As System.Windows.Forms.Button
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents btnNew As System.Windows.Forms.Button
    Friend WithEvents btnSaveExit As System.Windows.Forms.Button
    Friend WithEvents btnReport As System.Windows.Forms.Button
    Friend WithEvents txtOrgResource As System.Windows.Forms.TextBox
    Friend WithEvents lblSelectedID As System.Windows.Forms.Label
    Friend WithEvents DataGridTableStyle4 As System.Windows.Forms.DataGridTableStyle
    Friend WithEvents DataGridTextBoxColumn20 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn21 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents miGotoResource As System.Windows.Forms.MenuItem
    Friend WithEvents lblResource As System.Windows.Forms.Label
    Friend WithEvents DataGridTableStyle5 As System.Windows.Forms.DataGridTableStyle
    Friend WithEvents DataGridTextBoxColumn22 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn24 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn25 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cboEmail As InfoCtr.ComboBoxRelaxed
    Friend WithEvents cboPostal As InfoCtr.ComboBoxRelaxed
    Friend WithEvents miStaffRpt As System.Windows.Forms.MenuItem
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents fldReviewed As System.Windows.Forms.TextBox
    Friend WithEvents fldReviewedBy As System.Windows.Forms.TextBox
    Friend WithEvents btnReview As System.Windows.Forms.Button
    Friend WithEvents fldReviewDate As System.Windows.Forms.TextBox
    Friend WithEvents flgOutstanding As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents editHouseholds As System.Windows.Forms.TextBox
    Friend WithEvents DataGridTextBoxColumn26 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn27 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn28 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents txtEIN As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents DataGridTextBoxColumn23 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents ErrorProvider1 As System.Windows.Forms.ErrorProvider
    ' Friend WithEvents txtOrgID As System.Windows.Forms.TextBox
    Friend WithEvents lblSelectedWhat As System.Windows.Forms.Label
    Friend WithEvents DataGridTextBoxColumn29 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents lblPredominant As System.Windows.Forms.Label
    Friend WithEvents cboCulture As InfoCtr.ComboBoxRelaxed
    Friend WithEvents editCountry As System.Windows.Forms.TextBox
    Friend WithEvents Country As System.Windows.Forms.Label
    Friend WithEvents editLayLeadershipChanges As System.Windows.Forms.TextBox
    Friend WithEvents lblLayLeadershipChanges As System.Windows.Forms.Label
    Friend WithEvents txtDatasource As System.Windows.Forms.TextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents pnlEthnicity As System.Windows.Forms.Panel
    Friend WithEvents editDateFounded As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents SqlConnection1 As System.Data.SqlClient.SqlConnection
    Friend WithEvents PgHosted As System.Windows.Forms.TabPage
    Friend WithEvents PnlMail As System.Windows.Forms.Panel
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents pnlRegion As System.Windows.Forms.Panel
    Friend WithEvents lblRegion As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents fldRegion As System.Windows.Forms.Label
    Friend WithEvents fldCounty As System.Windows.Forms.Label
    Friend WithEvents miHostOverview As System.Windows.Forms.MenuItem



    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    ' Friend WithEvents SqlConnection1 As System.Data.SqlClient.SqlConnection
    'Friend WithEvents DsMainOrg1 As InfoCtr.dsMainOrg
    Friend WithEvents HelpProvider1 As System.Windows.Forms.HelpProvider
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents MainMenu1 As System.Windows.Forms.MainMenu
    Friend WithEvents MenuItem1 As System.Windows.Forms.MenuItem
    Friend WithEvents miClose As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem3 As System.Windows.Forms.MenuItem
    Friend WithEvents miSave As System.Windows.Forms.MenuItem
    Friend WithEvents miCancel As System.Windows.Forms.MenuItem
    Friend WithEvents miDeleteOrg As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem7 As System.Windows.Forms.MenuItem
    Friend WithEvents StatusBar1 As System.Windows.Forms.StatusBar
    Friend WithEvents StatusBarPanel1 As System.Windows.Forms.StatusBarPanel
    Friend WithEvents StatusBarPanel2 As System.Windows.Forms.StatusBarPanel
    Friend WithEvents StatusBarPanelID As System.Windows.Forms.StatusBarPanel
    'Friend WithEvents daMainOrg As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents daOrg2 As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents daOrg2Address As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents SqlSelectCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlInsertCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlDeleteCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents ContextMenu1 As System.Windows.Forms.ContextMenu
    Friend WithEvents miNew As System.Windows.Forms.MenuItem
    Friend WithEvents pgProfile As System.Windows.Forms.TabPage
    Friend WithEvents pgContact As System.Windows.Forms.TabPage
    Friend WithEvents pgCase As System.Windows.Forms.TabPage
    Friend WithEvents pgGrant As System.Windows.Forms.TabPage
    Friend WithEvents pgStory As System.Windows.Forms.TabPage
    Friend WithEvents pgExtra As System.Windows.Forms.TabPage
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Friend WithEvents lblName As System.Windows.Forms.Label
    Friend WithEvents editOrgName As System.Windows.Forms.TextBox
    Friend WithEvents editNotes As System.Windows.Forms.TextBox
    Friend WithEvents lblNotes As System.Windows.Forms.Label
    Friend WithEvents editWebsite As System.Windows.Forms.TextBox
    Friend WithEvents editZip As System.Windows.Forms.TextBox
    Friend WithEvents editStreet1 As System.Windows.Forms.TextBox
    Friend WithEvents editMapURL As System.Windows.Forms.Label
    Friend WithEvents editCity As System.Windows.Forms.TextBox
    Friend WithEvents lblWebsite As System.Windows.Forms.Label
    Friend WithEvents lblStateAb As System.Windows.Forms.Label
    Friend WithEvents lblMapURL As System.Windows.Forms.Label
    Friend WithEvents editEmail As System.Windows.Forms.TextBox
    Friend WithEvents lblEmail As System.Windows.Forms.Label
    Friend WithEvents editPhone As System.Windows.Forms.TextBox
    Friend WithEvents lblFax As System.Windows.Forms.Label
    Friend WithEvents editFax As System.Windows.Forms.TextBox
    Friend WithEvents lblPhone As System.Windows.Forms.Label
    Friend WithEvents grdMain As System.Windows.Forms.DataGrid
    Friend WithEvents DataGridTableStyle1 As System.Windows.Forms.DataGridTableStyle
    Friend WithEvents DataGridTableStyle2 As System.Windows.Forms.DataGridTableStyle
    Friend WithEvents DataGridTableStyle3 As System.Windows.Forms.DataGridTableStyle
    Friend WithEvents DataGridTextBoxColumn1 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn2 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn3 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn4 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn5 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn6 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn7 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn8 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn9 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn10 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn11 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn12 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn13 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn14 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents pnlExtra As System.Windows.Forms.Panel
    Friend WithEvents pnlStats As System.Windows.Forms.Panel
    Friend WithEvents fldDenomination As System.Windows.Forms.TextBox
    Friend WithEvents editMembership As System.Windows.Forms.TextBox
    Friend WithEvents editPrograms As System.Windows.Forms.TextBox
    Friend WithEvents editAnnual_Budget As System.Windows.Forms.TextBox
    Friend WithEvents editAttendance As System.Windows.Forms.TextBox
    Friend WithEvents lblGotoDenomination As System.Windows.Forms.Label
    Friend WithEvents lblMembership As System.Windows.Forms.Label
    Friend WithEvents lblPrograms As System.Windows.Forms.Label
    Friend WithEvents lblAnnual_Budget As System.Windows.Forms.Label
    Friend WithEvents lblAttendance As System.Windows.Forms.Label
    Friend WithEvents cboType As InfoCtr.ComboBoxRelaxed
    Friend WithEvents chkActive As System.Windows.Forms.CheckBox
    Friend WithEvents lblOrgType As System.Windows.Forms.Label
    ' Friend WithEvents Test1 As WindowsApplication11.test
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents mmGoto As System.Windows.Forms.MenuItem
    Friend WithEvents miHelp As System.Windows.Forms.MenuItem
    Friend WithEvents miGotoCase As System.Windows.Forms.MenuItem
    Friend WithEvents miGotoContact As System.Windows.Forms.MenuItem
    Friend WithEvents miGotoGrant As System.Windows.Forms.MenuItem
    Friend WithEvents lblTimeZone As System.Windows.Forms.Label
    Friend WithEvents pnlProfile As System.Windows.Forms.Panel
    Friend WithEvents flagOutofRegion As System.Windows.Forms.Label
    Friend WithEvents flagWarning As System.Windows.Forms.Label
    Friend WithEvents pgAlert As System.Windows.Forms.TabPage
    Friend WithEvents DataGridTextBoxColumn15 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn16 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn17 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn18 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents TimerFlag As System.Windows.Forms.Timer
    Friend WithEvents miMakeResource As System.Windows.Forms.MenuItem
    Friend WithEvents chkResource As System.Windows.Forms.CheckBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMainOrg))
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.pgProfile = New System.Windows.Forms.TabPage()
        Me.pgContact = New System.Windows.Forms.TabPage()
        Me.pgCase = New System.Windows.Forms.TabPage()
        Me.pgGrant = New System.Windows.Forms.TabPage()
        Me.pgStory = New System.Windows.Forms.TabPage()
        Me.PgHosted = New System.Windows.Forms.TabPage()
        Me.pgAlert = New System.Windows.Forms.TabPage()
        Me.pgExtra = New System.Windows.Forms.TabPage()
        Me.HelpProvider1 = New System.Windows.Forms.HelpProvider()
        Me.fldDenomination = New System.Windows.Forms.TextBox()
        Me.MainOrgBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.DsMainOrgWAddress1 = New InfoCtr.dsMainOrgWAddress()
        Me.editAttendance = New System.Windows.Forms.TextBox()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.chkResource = New System.Windows.Forms.CheckBox()
        Me.editWebsite = New System.Windows.Forms.TextBox()
        Me.editMapURL = New System.Windows.Forms.Label()
        Me.btnHelp = New System.Windows.Forms.Button()
        Me.btnSaveExit = New System.Windows.Forms.Button()
        Me.btnReport = New System.Windows.Forms.Button()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.btnNew = New System.Windows.Forms.Button()
        Me.lblResource = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblLayLeadershipChanges = New System.Windows.Forms.Label()
        Me.lblStateAb = New System.Windows.Forms.Label()
        Me.pnlRegion = New System.Windows.Forms.Panel()
        Me.lblRegion = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.fldRegion = New System.Windows.Forms.Label()
        Me.fldCounty = New System.Windows.Forms.Label()
        Me.lblMapURL = New System.Windows.Forms.Label()
        Me.StatusBar1 = New System.Windows.Forms.StatusBar()
        Me.StatusBarPanel1 = New System.Windows.Forms.StatusBarPanel()
        Me.StatusBarPanelID = New System.Windows.Forms.StatusBarPanel()
        Me.StatusBarPanel2 = New System.Windows.Forms.StatusBarPanel()
        Me.editPhysicalZip = New System.Windows.Forms.TextBox()
        Me.AddressBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.fldPhysicalAddress = New System.Windows.Forms.TextBox()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.btnNewPhysical = New System.Windows.Forms.Button()
        Me.btnTest = New System.Windows.Forms.Button()
        Me.btnTest2 = New System.Windows.Forms.Button()
        Me.pnlStats = New System.Windows.Forms.Panel()
        Me.editHouseholds = New System.Windows.Forms.TextBox()
        Me.editMembership = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.editAnnual_Budget = New System.Windows.Forms.TextBox()
        Me.lblMembership = New System.Windows.Forms.Label()
        Me.lblAnnual_Budget = New System.Windows.Forms.Label()
        Me.lblAttendance = New System.Windows.Forms.Label()
        Me.editEmail = New System.Windows.Forms.TextBox()
        Me.cboPostal = New InfoCtr.ComboBoxRelaxed()
        Me.cboEmail = New InfoCtr.ComboBoxRelaxed()
        Me.cboEthnicity = New InfoCtr.ComboBoxRelaxed()
        Me.cboCulture = New InfoCtr.ComboBoxRelaxed()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.lblGotoDenomination = New System.Windows.Forms.Label()
        Me.MainMenu1 = New System.Windows.Forms.MainMenu(Me.components)
        Me.MenuItem1 = New System.Windows.Forms.MenuItem()
        Me.miNew = New System.Windows.Forms.MenuItem()
        Me.miClose = New System.Windows.Forms.MenuItem()
        Me.MenuItem3 = New System.Windows.Forms.MenuItem()
        Me.miSave = New System.Windows.Forms.MenuItem()
        Me.miCancel = New System.Windows.Forms.MenuItem()
        Me.miDeleteOrg = New System.Windows.Forms.MenuItem()
        Me.miMakeResource = New System.Windows.Forms.MenuItem()
        Me.MenuItem7 = New System.Windows.Forms.MenuItem()
        Me.miStaffRpt = New System.Windows.Forms.MenuItem()
        Me.mmGoto = New System.Windows.Forms.MenuItem()
        Me.miGotoCase = New System.Windows.Forms.MenuItem()
        Me.miGotoContact = New System.Windows.Forms.MenuItem()
        Me.miGotoGrant = New System.Windows.Forms.MenuItem()
        Me.miGotoResource = New System.Windows.Forms.MenuItem()
        Me.miHostOverview = New System.Windows.Forms.MenuItem()
        Me.miHelp = New System.Windows.Forms.MenuItem()
        Me.SqlSelectCommand1 = New System.Data.SqlClient.SqlCommand()
        Me.SqlConnection1 = New System.Data.SqlClient.SqlConnection()
        Me.SqlCommand1 = New System.Data.SqlClient.SqlCommand()
        Me.daOrg2 = New System.Data.SqlClient.SqlDataAdapter()
        Me.sqlSelectOrg = New System.Data.SqlClient.SqlCommand()
        Me.sqlUpdateOrg = New System.Data.SqlClient.SqlCommand()
        Me.daOrg2Address = New System.Data.SqlClient.SqlDataAdapter()
        Me.SqlDeleteCommand = New System.Data.SqlClient.SqlCommand()
        Me.SqlInsertCommand = New System.Data.SqlClient.SqlCommand()
        Me.sqlSelectAddress = New System.Data.SqlClient.SqlCommand()
        Me.sqlUpdateAddress = New System.Data.SqlClient.SqlCommand()
        Me.SqlInsertCommand1 = New System.Data.SqlClient.SqlCommand()
        Me.SqlDeleteCommand1 = New System.Data.SqlClient.SqlCommand()
        Me.ContextMenu1 = New System.Windows.Forms.ContextMenu()
        Me.pnlProfile = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.PnlMail = New System.Windows.Forms.Panel()
        Me.txtOrgResource = New System.Windows.Forms.TextBox()
        Me.editPrograms = New System.Windows.Forms.TextBox()
        Me.lblPrograms = New System.Windows.Forms.Label()
        Me.cboType = New InfoCtr.ComboBoxRelaxed()
        Me.lblOrgType = New System.Windows.Forms.Label()
        Me.chkActive = New System.Windows.Forms.CheckBox()
        Me.lblPredominant = New System.Windows.Forms.Label()
        Me.pnlEthnicity = New System.Windows.Forms.Panel()
        Me.editDateFounded = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.pnlExtra = New System.Windows.Forms.Panel()
        Me.txtDatasource = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.editLayLeadershipChanges = New System.Windows.Forms.TextBox()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.fldReviewed = New System.Windows.Forms.TextBox()
        Me.fldReviewedBy = New System.Windows.Forms.TextBox()
        Me.btnReview = New System.Windows.Forms.Button()
        Me.fldReviewDate = New System.Windows.Forms.TextBox()
        Me.fldLastChangeStaff = New System.Windows.Forms.TextBox()
        Me.fldCreateStaff = New System.Windows.Forms.TextBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.lblExtra = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.fldLastChangeDate = New System.Windows.Forms.TextBox()
        Me.fldCreateDate = New System.Windows.Forms.TextBox()
        Me.editCountry = New System.Windows.Forms.TextBox()
        Me.Country = New System.Windows.Forms.Label()
        Me.grdMain = New System.Windows.Forms.DataGrid()
        Me.DataGridTableStyle1 = New System.Windows.Forms.DataGridTableStyle()
        Me.DataGridTextBoxColumn5 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn1 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn2 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn3 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn4 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn6 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn26 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTableStyle2 = New System.Windows.Forms.DataGridTableStyle()
        Me.DataGridTextBoxColumn7 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn8 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn9 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn10 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn14 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn11 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn12 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn13 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTableStyle3 = New System.Windows.Forms.DataGridTableStyle()
        Me.DataGridTextBoxColumn15 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn19 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn16 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn17 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn18 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn23 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTableStyle4 = New System.Windows.Forms.DataGridTableStyle()
        Me.DataGridTextBoxColumn20 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn27 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn21 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTableStyle5 = New System.Windows.Forms.DataGridTableStyle()
        Me.DataGridTextBoxColumn22 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn25 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn24 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn28 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn29 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.lblSelectedID = New System.Windows.Forms.Label()
        Me.flagOutofRegion = New System.Windows.Forms.Label()
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.txtEIN = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.pnlAddress = New System.Windows.Forms.Panel()
        Me.tbAddress = New System.Windows.Forms.TabControl()
        Me.pgMailing = New System.Windows.Forms.TabPage()
        Me.editCity = New System.Windows.Forms.TextBox()
        Me.editZip = New System.Windows.Forms.TextBox()
        Me.editStateAB = New System.Windows.Forms.TextBox()
        Me.lblCity = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.lblZip = New System.Windows.Forms.Label()
        Me.editStreet1 = New System.Windows.Forms.TextBox()
        Me.pgPhysical = New System.Windows.Forms.TabPage()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.EditPhysicalCity = New System.Windows.Forms.TextBox()
        Me.editPhysicalState = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.editPhysicalCountry = New System.Windows.Forms.TextBox()
        Me.editPhysicalStreet = New System.Windows.Forms.TextBox()
        Me.lblTimeZone = New System.Windows.Forms.Label()
        Me.lblName = New System.Windows.Forms.Label()
        Me.editOrgName = New System.Windows.Forms.TextBox()
        Me.editNotes = New System.Windows.Forms.TextBox()
        Me.lblNotes = New System.Windows.Forms.Label()
        Me.lblWebsite = New System.Windows.Forms.Label()
        Me.lblEmail = New System.Windows.Forms.Label()
        Me.editPhone = New System.Windows.Forms.TextBox()
        Me.lblFax = New System.Windows.Forms.Label()
        Me.editFax = New System.Windows.Forms.TextBox()
        Me.lblPhone = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.flagWarning = New System.Windows.Forms.Label()
        Me.TimerFlag = New System.Windows.Forms.Timer(Me.components)
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.flgOutstanding = New System.Windows.Forms.Label()
        Me.ErrorProvider1 = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.lblSelectedWhat = New System.Windows.Forms.Label()
        Me.lblDenomination = New System.Windows.Forms.Label()
        Me.TabControl1.SuspendLayout()
        CType(Me.MainOrgBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsMainOrgWAddress1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlRegion.SuspendLayout()
        CType(Me.StatusBarPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.StatusBarPanelID, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.StatusBarPanel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.AddressBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlStats.SuspendLayout()
        Me.pnlProfile.SuspendLayout()
        Me.PnlMail.SuspendLayout()
        Me.pnlEthnicity.SuspendLayout()
        Me.pnlExtra.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.grdMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlMain.SuspendLayout()
        Me.pnlAddress.SuspendLayout()
        Me.tbAddress.SuspendLayout()
        Me.pgMailing.SuspendLayout()
        Me.pgPhysical.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.pgProfile)
        Me.TabControl1.Controls.Add(Me.pgContact)
        Me.TabControl1.Controls.Add(Me.pgCase)
        Me.TabControl1.Controls.Add(Me.pgGrant)
        Me.TabControl1.Controls.Add(Me.pgStory)
        Me.TabControl1.Controls.Add(Me.PgHosted)
        Me.TabControl1.Controls.Add(Me.pgAlert)
        Me.TabControl1.Controls.Add(Me.pgExtra)
        Me.TabControl1.Location = New System.Drawing.Point(328, 8)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(607, 25)
        Me.TabControl1.TabIndex = 687
        '
        'pgProfile
        '
        Me.pgProfile.Location = New System.Drawing.Point(4, 22)
        Me.pgProfile.Name = "pgProfile"
        Me.pgProfile.Size = New System.Drawing.Size(599, 0)
        Me.pgProfile.TabIndex = 0
        Me.pgProfile.Tag = "PROFILE"
        Me.pgProfile.Text = "   PROFILE   "
        Me.pgProfile.UseVisualStyleBackColor = True
        '
        'pgContact
        '
        Me.pgContact.Location = New System.Drawing.Point(4, 22)
        Me.pgContact.Name = "pgContact"
        Me.pgContact.Size = New System.Drawing.Size(599, 0)
        Me.pgContact.TabIndex = 1
        Me.pgContact.Tag = "CONTACTS"
        Me.pgContact.Text = "   CONTACTS   "
        Me.pgContact.UseVisualStyleBackColor = True
        '
        'pgCase
        '
        Me.pgCase.Location = New System.Drawing.Point(4, 22)
        Me.pgCase.Name = "pgCase"
        Me.pgCase.Size = New System.Drawing.Size(599, 0)
        Me.pgCase.TabIndex = 2
        Me.pgCase.Tag = "CASES"
        Me.pgCase.Text = "    CASES   "
        Me.pgCase.UseVisualStyleBackColor = True
        '
        'pgGrant
        '
        Me.pgGrant.Location = New System.Drawing.Point(4, 22)
        Me.pgGrant.Name = "pgGrant"
        Me.pgGrant.Size = New System.Drawing.Size(599, 0)
        Me.pgGrant.TabIndex = 3
        Me.pgGrant.Tag = "GRANTS"
        Me.pgGrant.Text = "   GRANTS   "
        Me.pgGrant.UseVisualStyleBackColor = True
        '
        'pgStory
        '
        Me.pgStory.Location = New System.Drawing.Point(4, 22)
        Me.pgStory.Name = "pgStory"
        Me.pgStory.Size = New System.Drawing.Size(599, 0)
        Me.pgStory.TabIndex = 4
        Me.pgStory.Tag = "STORIES"
        Me.pgStory.Text = "   STORIES   "
        Me.pgStory.UseVisualStyleBackColor = True
        '
        'PgHosted
        '
        Me.PgHosted.Location = New System.Drawing.Point(4, 22)
        Me.PgHosted.Name = "PgHosted"
        Me.PgHosted.Size = New System.Drawing.Size(599, 0)
        Me.PgHosted.TabIndex = 7
        Me.PgHosted.Tag = "EVENTS"
        Me.PgHosted.Text = "   EVENTS"
        Me.PgHosted.ToolTipText = "future"
        Me.PgHosted.UseVisualStyleBackColor = True
        '
        'pgAlert
        '
        Me.pgAlert.Location = New System.Drawing.Point(4, 22)
        Me.pgAlert.Name = "pgAlert"
        Me.pgAlert.Size = New System.Drawing.Size(599, 0)
        Me.pgAlert.TabIndex = 6
        Me.pgAlert.Tag = "ALERTS"
        Me.pgAlert.Text = "   ALERTS   "
        Me.pgAlert.UseVisualStyleBackColor = True
        '
        'pgExtra
        '
        Me.pgExtra.Location = New System.Drawing.Point(4, 22)
        Me.pgExtra.Name = "pgExtra"
        Me.pgExtra.Size = New System.Drawing.Size(599, 0)
        Me.pgExtra.TabIndex = 5
        Me.pgExtra.Tag = "EXTRAS"
        Me.pgExtra.Text = "ADDITIONAL"
        Me.pgExtra.UseVisualStyleBackColor = True
        '
        'fldDenomination
        '
        Me.fldDenomination.BackColor = System.Drawing.SystemColors.Window
        Me.fldDenomination.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.fldDenomination.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainOrgBindingSource, "Denomination", True))
        Me.HelpProvider1.SetHelpString(Me.fldDenomination, "Right click for choices.  ")
        Me.fldDenomination.Location = New System.Drawing.Point(97, 71)
        Me.fldDenomination.MaxLength = 75
        Me.fldDenomination.Name = "fldDenomination"
        Me.fldDenomination.ReadOnly = True
        Me.HelpProvider1.SetShowHelp(Me.fldDenomination, True)
        Me.fldDenomination.Size = New System.Drawing.Size(196, 20)
        Me.fldDenomination.TabIndex = 22
        Me.ToolTip1.SetToolTip(Me.fldDenomination, "Right click for choices. " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "To enter new denomination, doubleclick green ""Denomina" & _
        "tion"" and enter on popup form.")
        '
        'MainOrgBindingSource
        '
        Me.MainOrgBindingSource.DataMember = "tblOrg1"
        Me.MainOrgBindingSource.DataSource = Me.DsMainOrgWAddress1
        '
        'DsMainOrgWAddress1
        '
        Me.DsMainOrgWAddress1.DataSetName = "dsMainOrgWAddress"
        Me.DsMainOrgWAddress1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'editAttendance
        '
        Me.editAttendance.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainOrgBindingSource, "Attendance", True, System.Windows.Forms.DataSourceUpdateMode.OnValidation, ""))
        Me.HelpProvider1.SetHelpString(Me.editAttendance, "must be a valid number")
        Me.editAttendance.Location = New System.Drawing.Point(107, 15)
        Me.editAttendance.Name = "editAttendance"
        Me.HelpProvider1.SetShowHelp(Me.editAttendance, True)
        Me.editAttendance.Size = New System.Drawing.Size(87, 20)
        Me.editAttendance.TabIndex = 51
        '
        'chkResource
        '
        Me.chkResource.Enabled = False
        Me.chkResource.ForeColor = System.Drawing.Color.ForestGreen
        Me.chkResource.Location = New System.Drawing.Point(334, 267)
        Me.chkResource.Name = "chkResource"
        Me.chkResource.Size = New System.Drawing.Size(23, 24)
        Me.chkResource.TabIndex = 65
        Me.chkResource.Text = "Is Resource"
        Me.ToolTip1.SetToolTip(Me.chkResource, "Use the New button to make this organization also a resource.")
        '
        'editWebsite
        '
        Me.editWebsite.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainOrgBindingSource, "Website", True))
        Me.editWebsite.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.editWebsite.ForeColor = System.Drawing.SystemColors.ActiveCaption
        Me.editWebsite.Location = New System.Drawing.Point(80, 277)
        Me.editWebsite.Multiline = True
        Me.editWebsite.Name = "editWebsite"
        Me.editWebsite.Size = New System.Drawing.Size(223, 40)
        Me.editWebsite.TabIndex = 10
        Me.ToolTip1.SetToolTip(Me.editWebsite, "Click to open website.")
        '
        'editMapURL
        '
        Me.editMapURL.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainOrgBindingSource, "MapURL", True))
        Me.editMapURL.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.editMapURL.ForeColor = System.Drawing.SystemColors.ActiveCaption
        Me.editMapURL.Location = New System.Drawing.Point(60, 38)
        Me.editMapURL.Name = "editMapURL"
        Me.editMapURL.Size = New System.Drawing.Size(212, 28)
        Me.editMapURL.TabIndex = 14
        Me.ToolTip1.SetToolTip(Me.editMapURL, "Click to open map of Physical Address.")
        '
        'btnHelp
        '
        Me.btnHelp.BackColor = System.Drawing.SystemColors.Control
        Me.btnHelp.Font = New System.Drawing.Font("Microsoft Sans Serif", 22.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnHelp.ForeColor = System.Drawing.SystemColors.Control
        Me.btnHelp.Image = CType(resources.GetObject("btnHelp.Image"), System.Drawing.Image)
        Me.btnHelp.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnHelp.Location = New System.Drawing.Point(945, 5)
        Me.btnHelp.Name = "btnHelp"
        Me.btnHelp.Size = New System.Drawing.Size(25, 25)
        Me.btnHelp.TabIndex = 778
        Me.btnHelp.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ToolTip1.SetToolTip(Me.btnHelp, "Help")
        Me.btnHelp.UseVisualStyleBackColor = False
        '
        'btnSaveExit
        '
        Me.btnSaveExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSaveExit.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.btnSaveExit.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSaveExit.Image = CType(resources.GetObject("btnSaveExit.Image"), System.Drawing.Image)
        Me.btnSaveExit.ImageAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSaveExit.Location = New System.Drawing.Point(85, 1)
        Me.btnSaveExit.Name = "btnSaveExit"
        Me.btnSaveExit.Size = New System.Drawing.Size(40, 35)
        Me.btnSaveExit.TabIndex = 783
        Me.btnSaveExit.Text = "Close"
        Me.btnSaveExit.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.ToolTip1.SetToolTip(Me.btnSaveExit, "Saves any changes and Closes this window")
        Me.btnSaveExit.UseVisualStyleBackColor = False
        '
        'btnReport
        '
        Me.btnReport.BackColor = System.Drawing.SystemColors.Control
        Me.btnReport.Image = CType(resources.GetObject("btnReport.Image"), System.Drawing.Image)
        Me.btnReport.Location = New System.Drawing.Point(746, 7)
        Me.btnReport.Name = "btnReport"
        Me.btnReport.Size = New System.Drawing.Size(40, 35)
        Me.btnReport.TabIndex = 782
        Me.ToolTip1.SetToolTip(Me.btnReport, "Preview Reports")
        Me.btnReport.UseVisualStyleBackColor = False
        '
        'btnDelete
        '
        Me.btnDelete.BackColor = System.Drawing.SystemColors.Control
        Me.btnDelete.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Image = CType(resources.GetObject("btnDelete.Image"), System.Drawing.Image)
        Me.btnDelete.ImageAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnDelete.Location = New System.Drawing.Point(43, 1)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(40, 35)
        Me.btnDelete.TabIndex = 784
        Me.btnDelete.TabStop = False
        Me.btnDelete.Text = "Delete"
        Me.btnDelete.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.ToolTip1.SetToolTip(Me.btnDelete, "Delete this Organization")
        Me.btnDelete.UseVisualStyleBackColor = False
        Me.btnDelete.Visible = False
        '
        'btnNew
        '
        Me.btnNew.BackColor = System.Drawing.Color.PaleGoldenrod
        Me.btnNew.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnNew.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnNew.Image = CType(resources.GetObject("btnNew.Image"), System.Drawing.Image)
        Me.btnNew.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.btnNew.Location = New System.Drawing.Point(2, 1)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(40, 35)
        Me.btnNew.TabIndex = 785
        Me.btnNew.TabStop = False
        Me.btnNew.Text = "New"
        Me.btnNew.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.ToolTip1.SetToolTip(Me.btnNew, "Add New Case, Contact or Grant.")
        Me.btnNew.UseVisualStyleBackColor = False
        '
        'lblResource
        '
        Me.lblResource.AutoSize = True
        Me.lblResource.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblResource.ForeColor = System.Drawing.Color.DarkGreen
        Me.lblResource.Location = New System.Drawing.Point(351, 270)
        Me.lblResource.Name = "lblResource"
        Me.lblResource.Size = New System.Drawing.Size(212, 15)
        Me.lblResource.TabIndex = 132
        Me.lblResource.Text = "This congregation is also a Resource."
        Me.ToolTip1.SetToolTip(Me.lblResource, "Doubleclick to open Resource window, or use the New button to make this Organizat" & _
        "ion a Resource.")
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Label2.Location = New System.Drawing.Point(91, 6)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(68, 23)
        Me.Label2.TabIndex = 242
        Me.Label2.Tag = ""
        Me.Label2.Text = "Postal Mail"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ToolTip1.SetToolTip(Me.Label2, "sent to organizaiton address.")
        '
        'lblLayLeadershipChanges
        '
        Me.lblLayLeadershipChanges.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLayLeadershipChanges.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLayLeadershipChanges.Location = New System.Drawing.Point(317, 47)
        Me.lblLayLeadershipChanges.Name = "lblLayLeadershipChanges"
        Me.lblLayLeadershipChanges.Size = New System.Drawing.Size(94, 32)
        Me.lblLayLeadershipChanges.TabIndex = 247
        Me.lblLayLeadershipChanges.Text = "LayLeadership Changes:"
        Me.lblLayLeadershipChanges.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ToolTip1.SetToolTip(Me.lblLayLeadershipChanges, "month; season of year; etc.")
        '
        'lblStateAb
        '
        Me.lblStateAb.BackColor = System.Drawing.Color.Transparent
        Me.lblStateAb.Location = New System.Drawing.Point(169, 35)
        Me.lblStateAb.Margin = New System.Windows.Forms.Padding(0)
        Me.lblStateAb.Name = "lblStateAb"
        Me.lblStateAb.Size = New System.Drawing.Size(41, 23)
        Me.lblStateAb.TabIndex = 116
        Me.lblStateAb.Text = "State:"
        Me.lblStateAb.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ToolTip1.SetToolTip(Me.lblStateAb, "2 characters")
        '
        'pnlRegion
        '
        Me.pnlRegion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlRegion.Controls.Add(Me.lblRegion)
        Me.pnlRegion.Controls.Add(Me.Label7)
        Me.pnlRegion.Controls.Add(Me.fldRegion)
        Me.pnlRegion.Controls.Add(Me.fldCounty)
        Me.pnlRegion.Controls.Add(Me.editMapURL)
        Me.pnlRegion.Controls.Add(Me.lblMapURL)
        Me.pnlRegion.Location = New System.Drawing.Point(12, 236)
        Me.pnlRegion.Name = "pnlRegion"
        Me.pnlRegion.Size = New System.Drawing.Size(287, 74)
        Me.pnlRegion.TabIndex = 25
        Me.ToolTip1.SetToolTip(Me.pnlRegion, "This dropdown does not affect mail sent to a home address.")
        '
        'lblRegion
        '
        Me.lblRegion.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRegion.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblRegion.Location = New System.Drawing.Point(142, 13)
        Me.lblRegion.Name = "lblRegion"
        Me.lblRegion.Size = New System.Drawing.Size(51, 21)
        Me.lblRegion.TabIndex = 784
        Me.lblRegion.Tag = ""
        Me.lblRegion.Text = "Region:"
        Me.lblRegion.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label7
        '
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(4, 13)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(50, 21)
        Me.Label7.TabIndex = 786
        Me.Label7.Tag = ""
        Me.Label7.Text = "County:"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'fldRegion
        '
        Me.fldRegion.BackColor = System.Drawing.Color.Transparent
        Me.fldRegion.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainOrgBindingSource, "SatelliteRegion", True))
        Me.fldRegion.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.fldRegion.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fldRegion.Location = New System.Drawing.Point(193, 14)
        Me.fldRegion.Name = "fldRegion"
        Me.fldRegion.Size = New System.Drawing.Size(83, 20)
        Me.fldRegion.TabIndex = 46
        Me.fldRegion.Text = "Not In Region"
        Me.fldRegion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'fldCounty
        '
        Me.fldCounty.BackColor = System.Drawing.Color.Transparent
        Me.fldCounty.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainOrgBindingSource, "County", True))
        Me.fldCounty.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.fldCounty.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fldCounty.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fldCounty.Location = New System.Drawing.Point(52, 14)
        Me.fldCounty.Name = "fldCounty"
        Me.fldCounty.Size = New System.Drawing.Size(83, 20)
        Me.fldCounty.TabIndex = 45
        Me.fldCounty.Text = "County"
        Me.fldCounty.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblMapURL
        '
        Me.lblMapURL.BackColor = System.Drawing.Color.Transparent
        Me.lblMapURL.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMapURL.Location = New System.Drawing.Point(6, 34)
        Me.lblMapURL.Name = "lblMapURL"
        Me.lblMapURL.Size = New System.Drawing.Size(48, 23)
        Me.lblMapURL.TabIndex = 114
        Me.lblMapURL.Text = "Map:"
        Me.lblMapURL.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'StatusBar1
        '
        Me.StatusBar1.Location = New System.Drawing.Point(0, 623)
        Me.StatusBar1.Name = "StatusBar1"
        Me.StatusBar1.Panels.AddRange(New System.Windows.Forms.StatusBarPanel() {Me.StatusBarPanel1, Me.StatusBarPanelID, Me.StatusBarPanel2})
        Me.StatusBar1.ShowPanels = True
        Me.StatusBar1.Size = New System.Drawing.Size(1000, 18)
        Me.StatusBar1.TabIndex = 197
        Me.StatusBar1.Text = "StatusBar1"
        Me.ToolTip1.SetToolTip(Me.StatusBar1, "Doubleclick to copy ID.")
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
        Me.StatusBarPanelID.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents
        Me.StatusBarPanelID.BorderStyle = System.Windows.Forms.StatusBarPanelBorderStyle.Raised
        Me.StatusBarPanelID.Name = "StatusBarPanelID"
        Me.StatusBarPanelID.Text = "OrgID"
        Me.StatusBarPanelID.Width = 44
        '
        'StatusBarPanel2
        '
        Me.StatusBarPanel2.Alignment = System.Windows.Forms.HorizontalAlignment.Right
        Me.StatusBarPanel2.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring
        Me.StatusBarPanel2.Name = "StatusBarPanel2"
        Me.StatusBarPanel2.Text = "Use this window to change Organization details, and go to related Cases, Contacts" & _
    " and Grants."
        Me.StatusBarPanel2.Width = 739
        '
        'editPhysicalZip
        '
        Me.editPhysicalZip.BackColor = System.Drawing.SystemColors.Window
        Me.editPhysicalZip.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.editPhysicalZip.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.AddressBindingSource, "Zip", True))
        Me.editPhysicalZip.ForeColor = System.Drawing.SystemColors.WindowText
        Me.editPhysicalZip.Location = New System.Drawing.Point(50, 64)
        Me.editPhysicalZip.MaxLength = 20
        Me.editPhysicalZip.Name = "editPhysicalZip"
        Me.editPhysicalZip.Size = New System.Drawing.Size(106, 20)
        Me.editPhysicalZip.TabIndex = 8
        Me.editPhysicalZip.Text = "ZIP"
        Me.ToolTip1.SetToolTip(Me.editPhysicalZip, "Zip. Determines Satellite Region.")
        '
        'AddressBindingSource
        '
        Me.AddressBindingSource.DataMember = "tblAddress1"
        Me.AddressBindingSource.DataSource = Me.DsMainOrgWAddress1
        '
        'fldPhysicalAddress
        '
        Me.fldPhysicalAddress.BackColor = System.Drawing.Color.PeachPuff
        Me.fldPhysicalAddress.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.AddressBindingSource, "AddressID", True))
        Me.fldPhysicalAddress.Location = New System.Drawing.Point(158, 34)
        Me.fldPhysicalAddress.MaxLength = 75
        Me.fldPhysicalAddress.Name = "fldPhysicalAddress"
        Me.fldPhysicalAddress.Size = New System.Drawing.Size(20, 20)
        Me.fldPhysicalAddress.TabIndex = 11
        Me.fldPhysicalAddress.Text = "AddressID"
        Me.fldPhysicalAddress.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ToolTip1.SetToolTip(Me.fldPhysicalAddress, "Show building location if different from mailing address.  Include Street comma C" & _
        "ity comma State. Doubleclick to automate.")
        Me.fldPhysicalAddress.Visible = False
        '
        'Label22
        '
        Me.Label22.BackColor = System.Drawing.Color.Transparent
        Me.Label22.Location = New System.Drawing.Point(170, 35)
        Me.Label22.Margin = New System.Windows.Forms.Padding(0)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(41, 23)
        Me.Label22.TabIndex = 257
        Me.Label22.Text = "State:"
        Me.Label22.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ToolTip1.SetToolTip(Me.Label22, "2 characters")
        '
        'btnNewPhysical
        '
        Me.btnNewPhysical.Location = New System.Drawing.Point(258, 7)
        Me.btnNewPhysical.Name = "btnNewPhysical"
        Me.btnNewPhysical.Size = New System.Drawing.Size(37, 24)
        Me.btnNewPhysical.TabIndex = 264
        Me.btnNewPhysical.Text = "Add"
        Me.ToolTip1.SetToolTip(Me.btnNewPhysical, "click to create address different from Mailing Address.")
        Me.btnNewPhysical.UseVisualStyleBackColor = True
        '
        'btnTest
        '
        Me.btnTest.Image = CType(resources.GetObject("btnTest.Image"), System.Drawing.Image)
        Me.btnTest.Location = New System.Drawing.Point(236, 5)
        Me.btnTest.Name = "btnTest"
        Me.btnTest.Size = New System.Drawing.Size(43, 38)
        Me.btnTest.TabIndex = 787
        Me.ToolTip1.SetToolTip(Me.btnTest, "TEST")
        Me.btnTest.UseVisualStyleBackColor = True
        Me.btnTest.Visible = False
        '
        'btnTest2
        '
        Me.btnTest2.Image = CType(resources.GetObject("btnTest2.Image"), System.Drawing.Image)
        Me.btnTest2.Location = New System.Drawing.Point(285, 12)
        Me.btnTest2.Name = "btnTest2"
        Me.btnTest2.Size = New System.Drawing.Size(34, 30)
        Me.btnTest2.TabIndex = 788
        Me.btnTest2.Text = "EndEdit"
        Me.ToolTip1.SetToolTip(Me.btnTest2, "EndEdit")
        Me.btnTest2.UseVisualStyleBackColor = True
        Me.btnTest2.Visible = False
        '
        'pnlStats
        '
        Me.pnlStats.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlStats.Controls.Add(Me.editHouseholds)
        Me.pnlStats.Controls.Add(Me.editMembership)
        Me.pnlStats.Controls.Add(Me.Label12)
        Me.pnlStats.Controls.Add(Me.Label4)
        Me.pnlStats.Controls.Add(Me.editAnnual_Budget)
        Me.pnlStats.Controls.Add(Me.editAttendance)
        Me.pnlStats.Controls.Add(Me.lblMembership)
        Me.pnlStats.Controls.Add(Me.lblAnnual_Budget)
        Me.pnlStats.Controls.Add(Me.lblAttendance)
        Me.pnlStats.Location = New System.Drawing.Point(12, 332)
        Me.pnlStats.Name = "pnlStats"
        Me.pnlStats.Size = New System.Drawing.Size(287, 133)
        Me.pnlStats.TabIndex = 30
        Me.ToolTip1.SetToolTip(Me.pnlStats, "tool tip message")
        '
        'editHouseholds
        '
        Me.editHouseholds.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainOrgBindingSource, "Households", True, System.Windows.Forms.DataSourceUpdateMode.OnValidation, ""))
        Me.editHouseholds.Location = New System.Drawing.Point(199, 62)
        Me.editHouseholds.Name = "editHouseholds"
        Me.editHouseholds.Size = New System.Drawing.Size(73, 20)
        Me.editHouseholds.TabIndex = 53
        '
        'editMembership
        '
        Me.editMembership.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainOrgBindingSource, "Membership", True, System.Windows.Forms.DataSourceUpdateMode.OnValidation, ""))
        Me.editMembership.Location = New System.Drawing.Point(107, 62)
        Me.editMembership.Name = "editMembership"
        Me.editMembership.Size = New System.Drawing.Size(86, 20)
        Me.editMembership.TabIndex = 52
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.Transparent
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label12.Location = New System.Drawing.Point(186, 41)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(90, 23)
        Me.Label12.TabIndex = 248
        Me.Label12.Text = "# Households"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(93, 41)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(77, 23)
        Me.Label4.TabIndex = 2
        Me.Label4.Text = "# Individuals"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'editAnnual_Budget
        '
        Me.editAnnual_Budget.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainOrgBindingSource, "AnnualBudget", True, System.Windows.Forms.DataSourceUpdateMode.OnValidation, ""))
        Me.editAnnual_Budget.Location = New System.Drawing.Point(107, 99)
        Me.editAnnual_Budget.Name = "editAnnual_Budget"
        Me.editAnnual_Budget.Size = New System.Drawing.Size(86, 20)
        Me.editAnnual_Budget.TabIndex = 54
        '
        'lblMembership
        '
        Me.lblMembership.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMembership.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblMembership.Location = New System.Drawing.Point(21, 59)
        Me.lblMembership.Name = "lblMembership"
        Me.lblMembership.Size = New System.Drawing.Size(81, 23)
        Me.lblMembership.TabIndex = 116
        Me.lblMembership.Text = "Membership:"
        Me.lblMembership.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblAnnual_Budget
        '
        Me.lblAnnual_Budget.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAnnual_Budget.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblAnnual_Budget.Location = New System.Drawing.Point(8, 104)
        Me.lblAnnual_Budget.Margin = New System.Windows.Forms.Padding(0)
        Me.lblAnnual_Budget.Name = "lblAnnual_Budget"
        Me.lblAnnual_Budget.Size = New System.Drawing.Size(99, 15)
        Me.lblAnnual_Budget.TabIndex = 110
        Me.lblAnnual_Budget.Text = "Annual Budget:"
        Me.lblAnnual_Budget.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblAttendance
        '
        Me.lblAttendance.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAttendance.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblAttendance.Location = New System.Drawing.Point(6, 16)
        Me.lblAttendance.Name = "lblAttendance"
        Me.lblAttendance.Size = New System.Drawing.Size(99, 15)
        Me.lblAttendance.TabIndex = 111
        Me.lblAttendance.Text = "Avg Attendance:"
        Me.lblAttendance.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'editEmail
        '
        Me.editEmail.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainOrgBindingSource, "Email", True))
        Me.editEmail.Location = New System.Drawing.Point(80, 249)
        Me.editEmail.MaxLength = 100
        Me.editEmail.Name = "editEmail"
        Me.editEmail.Size = New System.Drawing.Size(222, 20)
        Me.editEmail.TabIndex = 9
        Me.ToolTip1.SetToolTip(Me.editEmail, "default office email, not the pastor's email")
        '
        'cboPostal
        '
        Me.cboPostal.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboPostal.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboPostal.BackColor = System.Drawing.SystemColors.Window
        Me.cboPostal.DataBindings.Add(New System.Windows.Forms.Binding("SelectedItem", Me.MainOrgBindingSource, "MailPreference", True))
        Me.cboPostal.FormattingEnabled = True
        Me.cboPostal.Items.AddRange(New Object() {"Yes", "One Only", "No"})
        Me.cboPostal.Location = New System.Drawing.Point(90, 29)
        Me.cboPostal.Name = "cboPostal"
        Me.cboPostal.RestrictContentToListItems = True
        Me.cboPostal.Size = New System.Drawing.Size(83, 21)
        Me.cboPostal.TabIndex = 35
        Me.ToolTip1.SetToolTip(Me.cboPostal, "controls mail sent to organizaton address.")
        '
        'cboEmail
        '
        Me.cboEmail.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboEmail.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboEmail.Enabled = False
        Me.cboEmail.FormattingEnabled = True
        Me.cboEmail.Items.AddRange(New Object() {"Yes", "No"})
        Me.cboEmail.Location = New System.Drawing.Point(183, 29)
        Me.cboEmail.Name = "cboEmail"
        Me.cboEmail.RestrictContentToListItems = True
        Me.cboEmail.Size = New System.Drawing.Size(83, 21)
        Me.cboEmail.TabIndex = 36
        Me.ToolTip1.SetToolTip(Me.cboEmail, "sent to organization email")
        '
        'cboEthnicity
        '
        Me.cboEthnicity.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboEthnicity.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboEthnicity.DataBindings.Add(New System.Windows.Forms.Binding("SelectedItem", Me.MainOrgBindingSource, "CongrEthnicity", True))
        Me.cboEthnicity.DropDownWidth = 250
        Me.cboEthnicity.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboEthnicity.FormattingEnabled = True
        Me.cboEthnicity.Items.AddRange(New Object() {"African American", "Asian", "Hispanic", "White & English Speaking", "Other"})
        Me.cboEthnicity.Location = New System.Drawing.Point(103, 16)
        Me.cboEthnicity.Name = "cboEthnicity"
        Me.cboEthnicity.RestrictContentToListItems = True
        Me.cboEthnicity.Size = New System.Drawing.Size(155, 21)
        Me.cboEthnicity.TabIndex = 75
        Me.cboEthnicity.TabStop = False
        Me.cboEthnicity.Tag = "Ethnicity"
        Me.ToolTip1.SetToolTip(Me.cboEthnicity, "Census terminology.")
        '
        'cboCulture
        '
        Me.cboCulture.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboCulture.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboCulture.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainOrgBindingSource, "CongrCulture", True))
        Me.cboCulture.DropDownWidth = 250
        Me.cboCulture.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.cboCulture.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboCulture.FormattingEnabled = True
        Me.cboCulture.Location = New System.Drawing.Point(44, 60)
        Me.cboCulture.Name = "cboCulture"
        Me.cboCulture.RestrictContentToListItems = False
        Me.cboCulture.Size = New System.Drawing.Size(213, 21)
        Me.cboCulture.TabIndex = 76
        Me.cboCulture.TabStop = False
        Me.cboCulture.Tag = "OtherCombo"
        Me.ToolTip1.SetToolTip(Me.cboCulture, "Optional; select or type new entry.  Use for language or more specific ethnicity." & _
        "")
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.Transparent
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label11.Location = New System.Drawing.Point(7, 29)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(84, 20)
        Me.Label11.TabIndex = 239
        Me.Label11.Text = "Mailing Lists:"
        '
        'Label8
        '
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(206, 6)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(44, 23)
        Me.Label8.TabIndex = 243
        Me.Label8.Tag = ""
        Me.Label8.Text = "E-Mail"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblGotoDenomination
        '
        Me.lblGotoDenomination.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblGotoDenomination.ForeColor = System.Drawing.Color.ForestGreen
        Me.lblGotoDenomination.Location = New System.Drawing.Point(3, 69)
        Me.lblGotoDenomination.Name = "lblGotoDenomination"
        Me.lblGotoDenomination.Size = New System.Drawing.Size(89, 23)
        Me.lblGotoDenomination.TabIndex = 122
        Me.lblGotoDenomination.Text = "Denomination:"
        Me.lblGotoDenomination.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'MainMenu1
        '
        Me.MainMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem1, Me.MenuItem3, Me.MenuItem7, Me.mmGoto, Me.miHelp})
        '
        'MenuItem1
        '
        Me.MenuItem1.Index = 0
        Me.MenuItem1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.miNew, Me.miClose})
        Me.MenuItem1.Text = "File"
        '
        'miNew
        '
        Me.miNew.Index = 0
        Me.miNew.Text = " New     >"
        '
        'miClose
        '
        Me.miClose.Index = 1
        Me.miClose.Text = "Close Window"
        '
        'MenuItem3
        '
        Me.MenuItem3.Index = 1
        Me.MenuItem3.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.miSave, Me.miCancel, Me.miDeleteOrg, Me.miMakeResource})
        Me.MenuItem3.Text = "Edit"
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
        'miDeleteOrg
        '
        Me.miDeleteOrg.Index = 2
        Me.miDeleteOrg.Text = "Delete Organization"
        '
        'miMakeResource
        '
        Me.miMakeResource.Enabled = False
        Me.miMakeResource.Index = 3
        Me.miMakeResource.Text = "Make this Organization a Resource"
        Me.miMakeResource.Visible = False
        '
        'MenuItem7
        '
        Me.MenuItem7.Index = 2
        Me.MenuItem7.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.miStaffRpt})
        Me.MenuItem7.Text = "Reports"
        '
        'miStaffRpt
        '
        Me.miStaffRpt.Index = 0
        Me.miStaffRpt.Text = "Staff && Laity"
        '
        'mmGoto
        '
        Me.mmGoto.Index = 3
        Me.mmGoto.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.miGotoCase, Me.miGotoContact, Me.miGotoGrant, Me.miGotoResource, Me.miHostOverview})
        Me.mmGoto.Text = "Goto"
        '
        'miGotoCase
        '
        Me.miGotoCase.Enabled = False
        Me.miGotoCase.Index = 0
        Me.miGotoCase.Text = "Case"
        '
        'miGotoContact
        '
        Me.miGotoContact.Enabled = False
        Me.miGotoContact.Index = 1
        Me.miGotoContact.Text = "Contact"
        '
        'miGotoGrant
        '
        Me.miGotoGrant.Enabled = False
        Me.miGotoGrant.Index = 2
        Me.miGotoGrant.Text = "Grant"
        '
        'miGotoResource
        '
        Me.miGotoResource.Enabled = False
        Me.miGotoResource.Index = 3
        Me.miGotoResource.Text = "Resource Entry"
        '
        'miHostOverview
        '
        Me.miHostOverview.Index = 4
        Me.miHostOverview.Text = "Host Overview - Create"
        '
        'miHelp
        '
        Me.miHelp.Index = 4
        Me.miHelp.Text = "Help"
        '
        'SqlConnection1
        '
        Me.SqlConnection1.ConnectionString = "Data Source=SOLOMON2008\SOLOMON2008;Initial Catalog=InfoCtr_be;Integrated Securit" & _
    "y=True"
        Me.SqlConnection1.FireInfoMessageEventOnUserErrors = False
        '
        'SqlCommand1
        '
        Me.SqlCommand1.CommandText = "dbo.MainOrgUpdate"
        Me.SqlCommand1.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlCommand1.Connection = Me.SqlConnection1
        Me.SqlCommand1.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, False, CType(0, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing), New System.Data.SqlClient.SqlParameter("@OrgType", System.Data.SqlDbType.VarChar, 50, "OrgType"), New System.Data.SqlClient.SqlParameter("@OrgName", System.Data.SqlDbType.VarChar, 100, "OrgName"), New System.Data.SqlClient.SqlParameter("@Street1", System.Data.SqlDbType.VarChar, 50, "Street1"), New System.Data.SqlClient.SqlParameter("@Street2", System.Data.SqlDbType.VarChar, 50, "Street2"), New System.Data.SqlClient.SqlParameter("@City", System.Data.SqlDbType.VarChar, 30, "City"), New System.Data.SqlClient.SqlParameter("@State", System.Data.SqlDbType.VarChar, 20, "State"), New System.Data.SqlClient.SqlParameter("@Zip", System.Data.SqlDbType.VarChar, 10, "Zip"), New System.Data.SqlClient.SqlParameter("@Phone", System.Data.SqlDbType.VarChar, 20, "Phone"), New System.Data.SqlClient.SqlParameter("@Fax", System.Data.SqlDbType.VarChar, 20, "Fax"), New System.Data.SqlClient.SqlParameter("@Website", System.Data.SqlDbType.VarChar, 200, "Website"), New System.Data.SqlClient.SqlParameter("@Email", System.Data.SqlDbType.VarChar, 100, "Email"), New System.Data.SqlClient.SqlParameter("@Notes", System.Data.SqlDbType.VarChar, 2000, "Notes"), New System.Data.SqlClient.SqlParameter("@StreetAddress", System.Data.SqlDbType.VarChar, 75, "StreetAddress"), New System.Data.SqlClient.SqlParameter("@PhysicalAddress", System.Data.SqlDbType.VarChar, 75, "PhysicalAddress"), New System.Data.SqlClient.SqlParameter("@PhysicalZip", System.Data.SqlDbType.VarChar, 10, "PhysicalZip"), New System.Data.SqlClient.SqlParameter("@Active", System.Data.SqlDbType.Bit, 1, "Active"), New System.Data.SqlClient.SqlParameter("@NewsOnly", System.Data.SqlDbType.Bit, 1, "NewsOnly"), New System.Data.SqlClient.SqlParameter("@MailPref", System.Data.SqlDbType.VarChar, 30, "MailPreference"), New System.Data.SqlClient.SqlParameter("@EmailPref", System.Data.SqlDbType.VarChar, 30, "EmailPreference"), New System.Data.SqlClient.SqlParameter("@Attendance", System.Data.SqlDbType.Int, 4, "Attendance"), New System.Data.SqlClient.SqlParameter("@Membership", System.Data.SqlDbType.Int, 4, "Membership"), New System.Data.SqlClient.SqlParameter("@Household", System.Data.SqlDbType.Int, 4, "Households"), New System.Data.SqlClient.SqlParameter("@AnnualBudget", System.Data.SqlDbType.Int, 4, "AnnualBudget"), New System.Data.SqlClient.SqlParameter("@Denomination", System.Data.SqlDbType.VarChar, 75, "Denomination"), New System.Data.SqlClient.SqlParameter("@Region", System.Data.SqlDbType.VarChar, 15, "SatelliteRegion"), New System.Data.SqlClient.SqlParameter("@County", System.Data.SqlDbType.VarChar, 15, "County"), New System.Data.SqlClient.SqlParameter("@MapURL", System.Data.SqlDbType.VarChar, 600, "MapURL"), New System.Data.SqlClient.SqlParameter("@Programs", System.Data.SqlDbType.VarChar, 2000, "Programs"), New System.Data.SqlClient.SqlParameter("@LayLeadershipChanges", System.Data.SqlDbType.VarChar, 50, "LayLeadershipChanges"), New System.Data.SqlClient.SqlParameter("@CongrEthnicity", System.Data.SqlDbType.VarChar, 50, "CongrEthnicity"), New System.Data.SqlClient.SqlParameter("@CongrCulture", System.Data.SqlDbType.VarChar, 50, "CongrCulture"), New System.Data.SqlClient.SqlParameter("@DateFounded", System.Data.SqlDbType.VarChar, 20, "DateFounded"), New System.Data.SqlClient.SqlParameter("@InfoSource", System.Data.SqlDbType.VarChar, 75, "InfoSource"), New System.Data.SqlClient.SqlParameter("@Country", System.Data.SqlDbType.VarChar, 255, "Country"), New System.Data.SqlClient.SqlParameter("@FullAddress", System.Data.SqlDbType.VarChar, 75, "FullAddress"), New System.Data.SqlClient.SqlParameter("@StaffLiaisonNum", System.Data.SqlDbType.Int, 4, "StaffLiaisonNum"), New System.Data.SqlClient.SqlParameter("@ReviewDate", System.Data.SqlDbType.DateTime2, 4, "ReviewDate"), New System.Data.SqlClient.SqlParameter("@ReviewStaffNum", System.Data.SqlDbType.Int, 4, "ReviewStaffNum"), New System.Data.SqlClient.SqlParameter("@Review", System.Data.SqlDbType.VarChar, 50, "ReviewResult"), New System.Data.SqlClient.SqlParameter("@EIN", System.Data.SqlDbType.VarChar, 10, "EIN"), New System.Data.SqlClient.SqlParameter("@Original_OrgID", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "OrgID", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@ID", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "OrgID", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@Original_Stamp", System.Data.SqlDbType.Timestamp, 8, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "Stamped", System.Data.DataRowVersion.Original, Nothing)})
        '
        'daOrg2
        '
        Me.daOrg2.SelectCommand = Me.sqlSelectOrg
        Me.daOrg2.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "MainOrg", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("OrgID", "OrgID"), New System.Data.Common.DataColumnMapping("OrgType", "OrgType"), New System.Data.Common.DataColumnMapping("OrgName", "OrgName"), New System.Data.Common.DataColumnMapping("Street1", "Street1"), New System.Data.Common.DataColumnMapping("Street2", "Street2"), New System.Data.Common.DataColumnMapping("City", "City"), New System.Data.Common.DataColumnMapping("State", "State"), New System.Data.Common.DataColumnMapping("Zip", "Zip"), New System.Data.Common.DataColumnMapping("Phone", "Phone"), New System.Data.Common.DataColumnMapping("Website", "Website"), New System.Data.Common.DataColumnMapping("Email", "Email"), New System.Data.Common.DataColumnMapping("Notes", "Notes"), New System.Data.Common.DataColumnMapping("Active", "Active"), New System.Data.Common.DataColumnMapping("MailPreference", "MailPreference"), New System.Data.Common.DataColumnMapping("EmailPreference", "EmailPreference"), New System.Data.Common.DataColumnMapping("Attendance", "Attendance"), New System.Data.Common.DataColumnMapping("Membership", "Membership"), New System.Data.Common.DataColumnMapping("Households", "Households"), New System.Data.Common.DataColumnMapping("AnnualBudget", "AnnualBudget"), New System.Data.Common.DataColumnMapping("Denomination", "Denomination"), New System.Data.Common.DataColumnMapping("SatelliteRegion", "SatelliteRegion"), New System.Data.Common.DataColumnMapping("County", "County"), New System.Data.Common.DataColumnMapping("MapURL", "MapURL"), New System.Data.Common.DataColumnMapping("Programs", "Programs"), New System.Data.Common.DataColumnMapping("CongrEthnicity", "CongrEthnicity"), New System.Data.Common.DataColumnMapping("CongrCulture", "CongrCulture"), New System.Data.Common.DataColumnMapping("CreateDate", "CreateDate"), New System.Data.Common.DataColumnMapping("CreateStaffNum", "CreateStaffNum"), New System.Data.Common.DataColumnMapping("LastChangeDate", "LastChangeDate"), New System.Data.Common.DataColumnMapping("LastChangeStaffNum", "LastChangeStaffNum"), New System.Data.Common.DataColumnMapping("Country", "Country"), New System.Data.Common.DataColumnMapping("ReviewDate", "ReviewDate"), New System.Data.Common.DataColumnMapping("ReviewStaffNum", "ReviewStaffNum"), New System.Data.Common.DataColumnMapping("ReviewResult", "ReviewResult"), New System.Data.Common.DataColumnMapping("EIN", "EIN"), New System.Data.Common.DataColumnMapping("Fax", "Fax"), New System.Data.Common.DataColumnMapping("DateFounded", "DateFounded"), New System.Data.Common.DataColumnMapping("InfoSource", "InfoSource"), New System.Data.Common.DataColumnMapping("LayLeadershipChanges", "LayLeadershipChanges"), New System.Data.Common.DataColumnMapping("Stamped", "Stamped"), New System.Data.Common.DataColumnMapping("lblCounts", "lblCounts")})})
        Me.daOrg2.UpdateCommand = Me.sqlUpdateOrg
        '
        'sqlSelectOrg
        '
        Me.sqlSelectOrg.CommandText = "dbo.MainOrg"
        Me.sqlSelectOrg.CommandType = System.Data.CommandType.StoredProcedure
        Me.sqlSelectOrg.Connection = Me.SqlConnection1
        Me.sqlSelectOrg.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, False, CType(0, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing), New System.Data.SqlClient.SqlParameter("@ID", System.Data.SqlDbType.Int, 4)})
        '
        'sqlUpdateOrg
        '
        Me.sqlUpdateOrg.CommandText = "dbo.MainOrgUpdate"
        Me.sqlUpdateOrg.CommandType = System.Data.CommandType.StoredProcedure
        Me.sqlUpdateOrg.Connection = Me.SqlConnection1
        Me.sqlUpdateOrg.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, False, CType(0, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing), New System.Data.SqlClient.SqlParameter("@OrgType", System.Data.SqlDbType.VarChar, 50, "OrgType"), New System.Data.SqlClient.SqlParameter("@OrgName", System.Data.SqlDbType.VarChar, 100, "OrgName"), New System.Data.SqlClient.SqlParameter("@Street1", System.Data.SqlDbType.VarChar, 50, "Street1"), New System.Data.SqlClient.SqlParameter("@Street2", System.Data.SqlDbType.VarChar, 50, "Street2"), New System.Data.SqlClient.SqlParameter("@City", System.Data.SqlDbType.VarChar, 30, "City"), New System.Data.SqlClient.SqlParameter("@State", System.Data.SqlDbType.VarChar, 20, "State"), New System.Data.SqlClient.SqlParameter("@Zip", System.Data.SqlDbType.VarChar, 10, "Zip"), New System.Data.SqlClient.SqlParameter("@Phone", System.Data.SqlDbType.VarChar, 20, "Phone"), New System.Data.SqlClient.SqlParameter("@Fax", System.Data.SqlDbType.VarChar, 20, "Fax"), New System.Data.SqlClient.SqlParameter("@Website", System.Data.SqlDbType.VarChar, 300, "Website"), New System.Data.SqlClient.SqlParameter("@Email", System.Data.SqlDbType.VarChar, 100, "Email"), New System.Data.SqlClient.SqlParameter("@Notes", System.Data.SqlDbType.VarChar, 2000, "Notes"), New System.Data.SqlClient.SqlParameter("@Active", System.Data.SqlDbType.Bit, 1, "Active"), New System.Data.SqlClient.SqlParameter("@MailPref", System.Data.SqlDbType.VarChar, 30, "MailPreference"), New System.Data.SqlClient.SqlParameter("@EmailPref", System.Data.SqlDbType.VarChar, 30, "Email"), New System.Data.SqlClient.SqlParameter("@Attendance", System.Data.SqlDbType.Int, 4, "Attendance"), New System.Data.SqlClient.SqlParameter("@Membership", System.Data.SqlDbType.Int, 4, "Membership"), New System.Data.SqlClient.SqlParameter("@Household", System.Data.SqlDbType.Int, 4, "Households"), New System.Data.SqlClient.SqlParameter("@AnnualBudget", System.Data.SqlDbType.Int, 4, "AnnualBudget"), New System.Data.SqlClient.SqlParameter("@Denomination", System.Data.SqlDbType.VarChar, 75, "Denomination"), New System.Data.SqlClient.SqlParameter("@Region", System.Data.SqlDbType.VarChar, 15, "SatelliteRegion"), New System.Data.SqlClient.SqlParameter("@County", System.Data.SqlDbType.VarChar, 15, "County"), New System.Data.SqlClient.SqlParameter("@MapURL", System.Data.SqlDbType.VarChar, 600, "MapURL"), New System.Data.SqlClient.SqlParameter("@Programs", System.Data.SqlDbType.VarChar, 2000, "Programs"), New System.Data.SqlClient.SqlParameter("@LayLeadershipChanges", System.Data.SqlDbType.VarChar, 50, "LayLeadershipChanges"), New System.Data.SqlClient.SqlParameter("@CongrEthnicity", System.Data.SqlDbType.VarChar, 50, "CongrEthnicity"), New System.Data.SqlClient.SqlParameter("@CongrCulture", System.Data.SqlDbType.VarChar, 50, "CongrCulture"), New System.Data.SqlClient.SqlParameter("@DateFounded", System.Data.SqlDbType.VarChar, 20, "DateFounded"), New System.Data.SqlClient.SqlParameter("@InfoSource", System.Data.SqlDbType.VarChar, 75, "InfoSource"), New System.Data.SqlClient.SqlParameter("@Country", System.Data.SqlDbType.VarChar, 255, "Country"), New System.Data.SqlClient.SqlParameter("@CreateDate", System.Data.SqlDbType.DateTime2, 6, "CreateDate"), New System.Data.SqlClient.SqlParameter("@CreateStaffNum", System.Data.SqlDbType.SmallInt, 2, "CreateStaffNum"), New System.Data.SqlClient.SqlParameter("@LastChangeDate", System.Data.SqlDbType.DateTime2, 6, "LastChangeDate"), New System.Data.SqlClient.SqlParameter("@LastChangeStaffNum", System.Data.SqlDbType.SmallInt, 2, "LastChangeStaffNum"), New System.Data.SqlClient.SqlParameter("@ReviewDate", System.Data.SqlDbType.DateTime2, 6, "ReviewDate"), New System.Data.SqlClient.SqlParameter("@ReviewStaffNum", System.Data.SqlDbType.SmallInt, 2, "ReviewStaffNum"), New System.Data.SqlClient.SqlParameter("@Review", System.Data.SqlDbType.VarChar, 50, "ReviewResult"), New System.Data.SqlClient.SqlParameter("@EIN", System.Data.SqlDbType.VarChar, 10, "EIN"), New System.Data.SqlClient.SqlParameter("@ID", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "OrgID", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@Original_Stamp", System.Data.SqlDbType.Timestamp, 8, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "Stamped", System.Data.DataRowVersion.Original, Nothing)})
        '
        'daOrg2Address
        '
        Me.daOrg2Address.DeleteCommand = Me.SqlDeleteCommand
        Me.daOrg2Address.InsertCommand = Me.SqlInsertCommand
        Me.daOrg2Address.SelectCommand = Me.sqlSelectAddress
        Me.daOrg2Address.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "tblAddress", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("AddressID", "AddressID"), New System.Data.Common.DataColumnMapping("EntityType", "EntityType"), New System.Data.Common.DataColumnMapping("EntityNum", "EntityNum"), New System.Data.Common.DataColumnMapping("AddressType", "AddressType"), New System.Data.Common.DataColumnMapping("Street1", "Street1"), New System.Data.Common.DataColumnMapping("Street2", "Street2"), New System.Data.Common.DataColumnMapping("City", "City"), New System.Data.Common.DataColumnMapping("State", "State"), New System.Data.Common.DataColumnMapping("Zip", "Zip"), New System.Data.Common.DataColumnMapping("County", "County"), New System.Data.Common.DataColumnMapping("Country", "Country"), New System.Data.Common.DataColumnMapping("AddressNote", "AddressNote"), New System.Data.Common.DataColumnMapping("UseThisOne", "UseThisOne"), New System.Data.Common.DataColumnMapping("CreateDate", "CreateDate"), New System.Data.Common.DataColumnMapping("CreateStaffNum", "CreateStaffNum"), New System.Data.Common.DataColumnMapping("LastChangeDate", "LastChangeDate"), New System.Data.Common.DataColumnMapping("LastChangeStaffNum", "LastChangeStaffNum"), New System.Data.Common.DataColumnMapping("OrgName", "OrgName")})})
        Me.daOrg2Address.UpdateCommand = Me.sqlUpdateAddress
        '
        'SqlDeleteCommand
        '
        Me.SqlDeleteCommand.CommandText = resources.GetString("SqlDeleteCommand.CommandText")
        Me.SqlDeleteCommand.Connection = Me.SqlConnection1
        Me.SqlDeleteCommand.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@Original_AddressID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "AddressID", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@Original_EntityType", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "EntityType", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@Original_EntityNum", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "EntityNum", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@Original_AddressType", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "AddressType", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_Street1", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "Street1", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_Street1", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "Street1", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_Street2", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "Street2", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_Street2", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "Street2", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_City", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "City", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_City", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "City", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_State", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "State", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_State", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "State", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_Zip", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "Zip", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_Zip", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "Zip", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_County", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "County", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_County", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "County", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_Country", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "Country", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_Country", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "Country", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_AddressNote", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "AddressNote", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_AddressNote", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "AddressNote", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_UseThisOne", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "UseThisOne", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_UseThisOne", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "UseThisOne", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@Original_CreateDate", System.Data.SqlDbType.[Date], 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "CreateDate", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@Original_CreateStaffNum", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "CreateStaffNum", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_LastChangeDate", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "LastChangeDate", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_LastChangeDate", System.Data.SqlDbType.[Date], 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "LastChangeDate", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_LastChangeStaffNum", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "LastChangeStaffNum", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_LastChangeStaffNum", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "LastChangeStaffNum", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_OrgName", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "OrgName", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_OrgName", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "OrgName", System.Data.DataRowVersion.Original, Nothing)})
        '
        'SqlInsertCommand
        '
        Me.SqlInsertCommand.CommandText = resources.GetString("SqlInsertCommand.CommandText")
        Me.SqlInsertCommand.Connection = Me.SqlConnection1
        Me.SqlInsertCommand.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@EntityType", System.Data.SqlDbType.VarChar, 0, "EntityType"), New System.Data.SqlClient.SqlParameter("@EntityNum", System.Data.SqlDbType.Int, 0, "EntityNum"), New System.Data.SqlClient.SqlParameter("@AddressType", System.Data.SqlDbType.VarChar, 0, "AddressType"), New System.Data.SqlClient.SqlParameter("@Street1", System.Data.SqlDbType.VarChar, 0, "Street1"), New System.Data.SqlClient.SqlParameter("@Street2", System.Data.SqlDbType.VarChar, 0, "Street2"), New System.Data.SqlClient.SqlParameter("@City", System.Data.SqlDbType.VarChar, 0, "City"), New System.Data.SqlClient.SqlParameter("@State", System.Data.SqlDbType.VarChar, 0, "State"), New System.Data.SqlClient.SqlParameter("@Zip", System.Data.SqlDbType.VarChar, 0, "Zip"), New System.Data.SqlClient.SqlParameter("@County", System.Data.SqlDbType.VarChar, 0, "County"), New System.Data.SqlClient.SqlParameter("@Country", System.Data.SqlDbType.VarChar, 0, "Country"), New System.Data.SqlClient.SqlParameter("@AddressNote", System.Data.SqlDbType.VarChar, 0, "AddressNote"), New System.Data.SqlClient.SqlParameter("@UseThisOne", System.Data.SqlDbType.Bit, 0, "UseThisOne"), New System.Data.SqlClient.SqlParameter("@CreateDate", System.Data.SqlDbType.[Date], 0, "CreateDate"), New System.Data.SqlClient.SqlParameter("@CreateStaffNum", System.Data.SqlDbType.Int, 0, "CreateStaffNum"), New System.Data.SqlClient.SqlParameter("@LastChangeDate", System.Data.SqlDbType.[Date], 0, "LastChangeDate"), New System.Data.SqlClient.SqlParameter("@LastChangeStaffNum", System.Data.SqlDbType.Int, 0, "LastChangeStaffNum"), New System.Data.SqlClient.SqlParameter("@OrgName", System.Data.SqlDbType.VarChar, 0, "OrgName")})
        '
        'sqlSelectAddress
        '
        Me.sqlSelectAddress.CommandText = resources.GetString("sqlSelectAddress.CommandText")
        Me.sqlSelectAddress.Connection = Me.SqlConnection1
        Me.sqlSelectAddress.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@OrgID", System.Data.SqlDbType.Int, 4, "EntityNum")})
        '
        'sqlUpdateAddress
        '
        Me.sqlUpdateAddress.CommandText = resources.GetString("sqlUpdateAddress.CommandText")
        Me.sqlUpdateAddress.Connection = Me.SqlConnection1
        Me.sqlUpdateAddress.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@EntityType", System.Data.SqlDbType.VarChar, 10, "EntityType"), New System.Data.SqlClient.SqlParameter("@EntityNum", System.Data.SqlDbType.Int, 4, "EntityNum"), New System.Data.SqlClient.SqlParameter("@AddressType", System.Data.SqlDbType.VarChar, 20, "AddressType"), New System.Data.SqlClient.SqlParameter("@Street1", System.Data.SqlDbType.VarChar, 100, "Street1"), New System.Data.SqlClient.SqlParameter("@Street2", System.Data.SqlDbType.VarChar, 100, "Street2"), New System.Data.SqlClient.SqlParameter("@City", System.Data.SqlDbType.VarChar, 30, "City"), New System.Data.SqlClient.SqlParameter("@State", System.Data.SqlDbType.VarChar, 20, "State"), New System.Data.SqlClient.SqlParameter("@Zip", System.Data.SqlDbType.VarChar, 15, "Zip"), New System.Data.SqlClient.SqlParameter("@County", System.Data.SqlDbType.VarChar, 20, "County"), New System.Data.SqlClient.SqlParameter("@Country", System.Data.SqlDbType.VarChar, 50, "Country"), New System.Data.SqlClient.SqlParameter("@AddressNote", System.Data.SqlDbType.VarChar, 300, "AddressNote"), New System.Data.SqlClient.SqlParameter("@UseThisOne", System.Data.SqlDbType.Bit, 1, "UseThisOne"), New System.Data.SqlClient.SqlParameter("@CreateDate", System.Data.SqlDbType.[Date], 3, "CreateDate"), New System.Data.SqlClient.SqlParameter("@CreateStaffNum", System.Data.SqlDbType.Int, 4, "CreateStaffNum"), New System.Data.SqlClient.SqlParameter("@LastChangeDate", System.Data.SqlDbType.[Date], 3, "LastChangeDate"), New System.Data.SqlClient.SqlParameter("@LastChangeStaffNum", System.Data.SqlDbType.Int, 4, "LastChangeStaffNum"), New System.Data.SqlClient.SqlParameter("@OrgName", System.Data.SqlDbType.VarChar, 100, "OrgName"), New System.Data.SqlClient.SqlParameter("@ID", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "AddressID", System.Data.DataRowVersion.Original, Nothing)})
        '
        'pnlProfile
        '
        Me.pnlProfile.BackColor = System.Drawing.SystemColors.Control
        Me.pnlProfile.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pnlProfile.Controls.Add(Me.Label1)
        Me.pnlProfile.Controls.Add(Me.Label3)
        Me.pnlProfile.Controls.Add(Me.lblResource)
        Me.pnlProfile.Controls.Add(Me.chkResource)
        Me.pnlProfile.Controls.Add(Me.Label15)
        Me.pnlProfile.Controls.Add(Me.PnlMail)
        Me.pnlProfile.Controls.Add(Me.txtOrgResource)
        Me.pnlProfile.Controls.Add(Me.editPrograms)
        Me.pnlProfile.Controls.Add(Me.pnlStats)
        Me.pnlProfile.Controls.Add(Me.fldDenomination)
        Me.pnlProfile.Controls.Add(Me.lblPrograms)
        Me.pnlProfile.Controls.Add(Me.lblGotoDenomination)
        Me.pnlProfile.Controls.Add(Me.cboType)
        Me.pnlProfile.Controls.Add(Me.lblOrgType)
        Me.pnlProfile.Controls.Add(Me.chkActive)
        Me.pnlProfile.Controls.Add(Me.pnlRegion)
        Me.pnlProfile.Controls.Add(Me.lblPredominant)
        Me.pnlProfile.Controls.Add(Me.pnlEthnicity)
        Me.pnlProfile.Location = New System.Drawing.Point(328, 30)
        Me.pnlProfile.Name = "pnlProfile"
        Me.pnlProfile.Size = New System.Drawing.Size(607, 470)
        Me.pnlProfile.TabIndex = 20
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.Label1.ForeColor = System.Drawing.SystemColors.Highlight
        Me.Label1.Location = New System.Drawing.Point(21, 223)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(161, 19)
        Me.Label1.TabIndex = 785
        Me.Label1.Tag = "lbl"
        Me.Label1.Text = "Region of Physical Address"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.Label3.ForeColor = System.Drawing.SystemColors.Highlight
        Me.Label3.Location = New System.Drawing.Point(27, 318)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(97, 21)
        Me.Label3.TabIndex = 786
        Me.Label3.Tag = "lbl"
        Me.Label3.Text = "Size Indicators"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label15
        '
        Me.Label15.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.Label15.ForeColor = System.Drawing.SystemColors.Highlight
        Me.Label15.Location = New System.Drawing.Point(19, 122)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(120, 19)
        Me.Label15.TabIndex = 787
        Me.Label15.Tag = "lbl"
        Me.Label15.Text = "Mailing Preferences"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'PnlMail
        '
        Me.PnlMail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PnlMail.Controls.Add(Me.Label11)
        Me.PnlMail.Controls.Add(Me.cboPostal)
        Me.PnlMail.Controls.Add(Me.Label8)
        Me.PnlMail.Controls.Add(Me.cboEmail)
        Me.PnlMail.Controls.Add(Me.Label2)
        Me.PnlMail.Location = New System.Drawing.Point(12, 134)
        Me.PnlMail.Name = "PnlMail"
        Me.PnlMail.Size = New System.Drawing.Size(287, 63)
        Me.PnlMail.TabIndex = 23
        '
        'txtOrgResource
        '
        Me.txtOrgResource.BackColor = System.Drawing.SystemColors.Control
        Me.txtOrgResource.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtOrgResource.Enabled = False
        Me.txtOrgResource.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOrgResource.ForeColor = System.Drawing.SystemColors.GrayText
        Me.txtOrgResource.Location = New System.Drawing.Point(522, 466)
        Me.txtOrgResource.Name = "txtOrgResource"
        Me.txtOrgResource.Size = New System.Drawing.Size(41, 11)
        Me.txtOrgResource.TabIndex = 782
        Me.txtOrgResource.TabStop = False
        Me.txtOrgResource.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'editPrograms
        '
        Me.editPrograms.AcceptsReturn = True
        Me.editPrograms.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainOrgBindingSource, "Programs", True))
        Me.editPrograms.Location = New System.Drawing.Point(323, 34)
        Me.editPrograms.Multiline = True
        Me.editPrograms.Name = "editPrograms"
        Me.editPrograms.Size = New System.Drawing.Size(266, 210)
        Me.editPrograms.TabIndex = 60
        '
        'lblPrograms
        '
        Me.lblPrograms.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPrograms.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblPrograms.Location = New System.Drawing.Point(313, 12)
        Me.lblPrograms.Name = "lblPrograms"
        Me.lblPrograms.Size = New System.Drawing.Size(56, 15)
        Me.lblPrograms.TabIndex = 117
        Me.lblPrograms.Text = "Programs:"
        Me.lblPrograms.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cboType
        '
        Me.cboType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboType.DataBindings.Add(New System.Windows.Forms.Binding("SelectedItem", Me.MainOrgBindingSource, "OrgType", True))
        Me.cboType.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainOrgBindingSource, "OrgType", True))
        Me.cboType.Location = New System.Drawing.Point(136, 37)
        Me.cboType.Name = "cboType"
        Me.cboType.RestrictContentToListItems = True
        Me.cboType.Size = New System.Drawing.Size(157, 21)
        Me.cboType.TabIndex = 21
        Me.cboType.Tag = "Type of Organization"
        '
        'lblOrgType
        '
        Me.lblOrgType.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblOrgType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblOrgType.Location = New System.Drawing.Point(4, 23)
        Me.lblOrgType.Margin = New System.Windows.Forms.Padding(0)
        Me.lblOrgType.Name = "lblOrgType"
        Me.lblOrgType.Size = New System.Drawing.Size(129, 38)
        Me.lblOrgType.TabIndex = 76
        Me.lblOrgType.Text = "Type of Organzation:"
        Me.lblOrgType.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'chkActive
        '
        Me.chkActive.DataBindings.Add(New System.Windows.Forms.Binding("Checked", Me.MainOrgBindingSource, "Active", True))
        Me.chkActive.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkActive.Location = New System.Drawing.Point(136, 8)
        Me.chkActive.Name = "chkActive"
        Me.chkActive.Size = New System.Drawing.Size(89, 19)
        Me.chkActive.TabIndex = 16
        Me.chkActive.Text = "Active"
        '
        'lblPredominant
        '
        Me.lblPredominant.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.lblPredominant.ForeColor = System.Drawing.SystemColors.Highlight
        Me.lblPredominant.Location = New System.Drawing.Point(334, 316)
        Me.lblPredominant.Name = "lblPredominant"
        Me.lblPredominant.Size = New System.Drawing.Size(131, 25)
        Me.lblPredominant.TabIndex = 244
        Me.lblPredominant.Tag = "lbl"
        Me.lblPredominant.Text = "Predominant Culture"
        Me.lblPredominant.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pnlEthnicity
        '
        Me.pnlEthnicity.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlEthnicity.Controls.Add(Me.editDateFounded)
        Me.pnlEthnicity.Controls.Add(Me.Label10)
        Me.pnlEthnicity.Controls.Add(Me.Label17)
        Me.pnlEthnicity.Controls.Add(Me.cboEthnicity)
        Me.pnlEthnicity.Controls.Add(Me.Label16)
        Me.pnlEthnicity.Controls.Add(Me.cboCulture)
        Me.pnlEthnicity.Location = New System.Drawing.Point(322, 332)
        Me.pnlEthnicity.Name = "pnlEthnicity"
        Me.pnlEthnicity.Size = New System.Drawing.Size(268, 133)
        Me.pnlEthnicity.TabIndex = 70
        '
        'editDateFounded
        '
        Me.editDateFounded.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainOrgBindingSource, "DateFounded", True))
        Me.editDateFounded.Location = New System.Drawing.Point(118, 99)
        Me.editDateFounded.MaxLength = 75
        Me.editDateFounded.Name = "editDateFounded"
        Me.editDateFounded.Size = New System.Drawing.Size(137, 20)
        Me.editDateFounded.TabIndex = 80
        '
        'Label10
        '
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label10.Location = New System.Drawing.Point(9, 96)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(102, 23)
        Me.Label10.TabIndex = 257
        Me.Label10.Text = "Date Founded:"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label17
        '
        Me.Label17.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.Label17.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label17.Location = New System.Drawing.Point(6, 15)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(100, 19)
        Me.Label17.TabIndex = 253
        Me.Label17.Tag = "lbl"
        Me.Label17.Text = "General Ethnicity"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label16
        '
        Me.Label16.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.Label16.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label16.Location = New System.Drawing.Point(6, 38)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(186, 22)
        Me.Label16.TabIndex = 251
        Me.Label16.Tag = "lbl"
        Me.Label16.Text = "Language or 'Other' Specifics"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pnlExtra
        '
        Me.pnlExtra.BackColor = System.Drawing.SystemColors.InactiveBorder
        Me.pnlExtra.Controls.Add(Me.txtDatasource)
        Me.pnlExtra.Controls.Add(Me.Label14)
        Me.pnlExtra.Controls.Add(Me.editLayLeadershipChanges)
        Me.pnlExtra.Controls.Add(Me.lblLayLeadershipChanges)
        Me.pnlExtra.Controls.Add(Me.Panel3)
        Me.pnlExtra.Controls.Add(Me.fldLastChangeStaff)
        Me.pnlExtra.Controls.Add(Me.fldCreateStaff)
        Me.pnlExtra.Controls.Add(Me.Label19)
        Me.pnlExtra.Controls.Add(Me.lblExtra)
        Me.pnlExtra.Controls.Add(Me.Label18)
        Me.pnlExtra.Controls.Add(Me.fldLastChangeDate)
        Me.pnlExtra.Controls.Add(Me.fldCreateDate)
        Me.pnlExtra.Location = New System.Drawing.Point(328, 33)
        Me.pnlExtra.Name = "pnlExtra"
        Me.pnlExtra.Size = New System.Drawing.Size(602, 467)
        Me.pnlExtra.TabIndex = 230
        Me.pnlExtra.Visible = False
        '
        'txtDatasource
        '
        Me.txtDatasource.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainOrgBindingSource, "InfoSource", True))
        Me.txtDatasource.Location = New System.Drawing.Point(428, 331)
        Me.txtDatasource.MaxLength = 75
        Me.txtDatasource.Name = "txtDatasource"
        Me.txtDatasource.Size = New System.Drawing.Size(152, 20)
        Me.txtDatasource.TabIndex = 258
        Me.txtDatasource.Text = "USA"
        '
        'Label14
        '
        Me.Label14.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label14.Location = New System.Drawing.Point(299, 321)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(127, 35)
        Me.Label14.TabIndex = 255
        Me.Label14.Text = "Original datasource:"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'editLayLeadershipChanges
        '
        Me.editLayLeadershipChanges.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainOrgBindingSource, "LayLeadershipChanges", True))
        Me.editLayLeadershipChanges.Location = New System.Drawing.Point(416, 55)
        Me.editLayLeadershipChanges.MaxLength = 50
        Me.editLayLeadershipChanges.Name = "editLayLeadershipChanges"
        Me.editLayLeadershipChanges.Size = New System.Drawing.Size(152, 20)
        Me.editLayLeadershipChanges.TabIndex = 252
        '
        'Panel3
        '
        Me.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel3.Controls.Add(Me.fldReviewed)
        Me.Panel3.Controls.Add(Me.fldReviewedBy)
        Me.Panel3.Controls.Add(Me.btnReview)
        Me.Panel3.Controls.Add(Me.fldReviewDate)
        Me.Panel3.Location = New System.Drawing.Point(72, 143)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(218, 78)
        Me.Panel3.TabIndex = 245
        '
        'fldReviewed
        '
        Me.fldReviewed.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainOrgBindingSource, "ReviewResult", True))
        Me.fldReviewed.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fldReviewed.ForeColor = System.Drawing.SystemColors.ButtonShadow
        Me.fldReviewed.Location = New System.Drawing.Point(118, 3)
        Me.fldReviewed.Name = "fldReviewed"
        Me.fldReviewed.ReadOnly = True
        Me.fldReviewed.Size = New System.Drawing.Size(86, 20)
        Me.fldReviewed.TabIndex = 245
        '
        'fldReviewedBy
        '
        Me.fldReviewedBy.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainOrgBindingSource, "ReviewStaffNum", True))
        Me.fldReviewedBy.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fldReviewedBy.ForeColor = System.Drawing.SystemColors.ButtonShadow
        Me.fldReviewedBy.Location = New System.Drawing.Point(122, 39)
        Me.fldReviewedBy.Name = "fldReviewedBy"
        Me.fldReviewedBy.ReadOnly = True
        Me.fldReviewedBy.Size = New System.Drawing.Size(86, 20)
        Me.fldReviewedBy.TabIndex = 249
        Me.fldReviewedBy.Tag = "Reviewed by"
        '
        'btnReview
        '
        Me.btnReview.Location = New System.Drawing.Point(6, 3)
        Me.btnReview.Name = "btnReview"
        Me.btnReview.Size = New System.Drawing.Size(104, 21)
        Me.btnReview.TabIndex = 246
        Me.btnReview.Text = "Review Complete"
        Me.btnReview.UseVisualStyleBackColor = True
        '
        'fldReviewDate
        '
        Me.fldReviewDate.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainOrgBindingSource, "ReviewDate", True))
        Me.fldReviewDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fldReviewDate.ForeColor = System.Drawing.SystemColors.ButtonShadow
        Me.fldReviewDate.Location = New System.Drawing.Point(21, 40)
        Me.fldReviewDate.Name = "fldReviewDate"
        Me.fldReviewDate.ReadOnly = True
        Me.fldReviewDate.Size = New System.Drawing.Size(90, 20)
        Me.fldReviewDate.TabIndex = 247
        '
        'fldLastChangeStaff
        '
        Me.fldLastChangeStaff.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainOrgBindingSource, "LastChangeStaffNum", True))
        Me.fldLastChangeStaff.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fldLastChangeStaff.ForeColor = System.Drawing.SystemColors.ButtonShadow
        Me.fldLastChangeStaff.Location = New System.Drawing.Point(487, 186)
        Me.fldLastChangeStaff.Name = "fldLastChangeStaff"
        Me.fldLastChangeStaff.ReadOnly = True
        Me.fldLastChangeStaff.Size = New System.Drawing.Size(86, 20)
        Me.fldLastChangeStaff.TabIndex = 231
        Me.fldLastChangeStaff.TabStop = False
        Me.fldLastChangeStaff.Tag = "Last Changed by"
        '
        'fldCreateStaff
        '
        Me.fldCreateStaff.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainOrgBindingSource, "CreateStaffNum", True))
        Me.fldCreateStaff.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fldCreateStaff.ForeColor = System.Drawing.SystemColors.ButtonShadow
        Me.fldCreateStaff.Location = New System.Drawing.Point(486, 153)
        Me.fldCreateStaff.Name = "fldCreateStaff"
        Me.fldCreateStaff.ReadOnly = True
        Me.fldCreateStaff.Size = New System.Drawing.Size(86, 20)
        Me.fldCreateStaff.TabIndex = 236
        Me.fldCreateStaff.TabStop = False
        Me.fldCreateStaff.Tag = "Created By"
        '
        'Label19
        '
        Me.Label19.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.Label19.Location = New System.Drawing.Point(326, 182)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(56, 27)
        Me.Label19.TabIndex = 235
        Me.Label19.Text = "Last Changed"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblExtra
        '
        Me.lblExtra.BackColor = System.Drawing.Color.Transparent
        Me.lblExtra.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.lblExtra.ForeColor = System.Drawing.SystemColors.Highlight
        Me.lblExtra.Location = New System.Drawing.Point(409, 131)
        Me.lblExtra.Name = "lblExtra"
        Me.lblExtra.Size = New System.Drawing.Size(136, 16)
        Me.lblExtra.TabIndex = 243
        Me.lblExtra.Text = "Update Information"
        Me.lblExtra.Visible = False
        '
        'Label18
        '
        Me.Label18.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.Label18.Location = New System.Drawing.Point(326, 153)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(56, 20)
        Me.Label18.TabIndex = 234
        Me.Label18.Text = "Created"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'fldLastChangeDate
        '
        Me.fldLastChangeDate.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainOrgBindingSource, "LastChangeDate", True))
        Me.fldLastChangeDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fldLastChangeDate.ForeColor = System.Drawing.SystemColors.ButtonShadow
        Me.fldLastChangeDate.Location = New System.Drawing.Point(388, 186)
        Me.fldLastChangeDate.Name = "fldLastChangeDate"
        Me.fldLastChangeDate.ReadOnly = True
        Me.fldLastChangeDate.Size = New System.Drawing.Size(90, 20)
        Me.fldLastChangeDate.TabIndex = 233
        Me.fldLastChangeDate.TabStop = False
        '
        'fldCreateDate
        '
        Me.fldCreateDate.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainOrgBindingSource, "CreateDate", True))
        Me.fldCreateDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fldCreateDate.ForeColor = System.Drawing.SystemColors.ButtonShadow
        Me.fldCreateDate.Location = New System.Drawing.Point(388, 153)
        Me.fldCreateDate.Name = "fldCreateDate"
        Me.fldCreateDate.ReadOnly = True
        Me.fldCreateDate.Size = New System.Drawing.Size(90, 20)
        Me.fldCreateDate.TabIndex = 232
        Me.fldCreateDate.TabStop = False
        '
        'editCountry
        '
        Me.editCountry.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.editCountry.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainOrgBindingSource, "Country", True))
        Me.editCountry.Location = New System.Drawing.Point(211, 64)
        Me.editCountry.MaxLength = 75
        Me.editCountry.Name = "editCountry"
        Me.editCountry.Size = New System.Drawing.Size(89, 20)
        Me.editCountry.TabIndex = 9
        Me.editCountry.Text = "USA"
        '
        'Country
        '
        Me.Country.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Country.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Country.Location = New System.Drawing.Point(157, 60)
        Me.Country.Margin = New System.Windows.Forms.Padding(0)
        Me.Country.Name = "Country"
        Me.Country.Size = New System.Drawing.Size(52, 23)
        Me.Country.TabIndex = 253
        Me.Country.Text = "Country:"
        Me.Country.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'grdMain
        '
        Me.grdMain.DataMember = ""
        Me.grdMain.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.grdMain.Location = New System.Drawing.Point(328, 30)
        Me.grdMain.Name = "grdMain"
        Me.grdMain.ReadOnly = True
        Me.grdMain.RowHeaderWidth = 20
        Me.grdMain.Size = New System.Drawing.Size(605, 469)
        Me.grdMain.TabIndex = 777
        Me.grdMain.TableStyles.AddRange(New System.Windows.Forms.DataGridTableStyle() {Me.DataGridTableStyle1, Me.DataGridTableStyle2, Me.DataGridTableStyle3, Me.DataGridTableStyle4, Me.DataGridTableStyle5})
        Me.grdMain.Visible = False
        '
        'DataGridTableStyle1
        '
        Me.DataGridTableStyle1.DataGrid = Me.grdMain
        Me.DataGridTableStyle1.ForeColor = System.Drawing.Color.DarkGreen
        Me.DataGridTableStyle1.GridColumnStyles.AddRange(New System.Windows.Forms.DataGridColumnStyle() {Me.DataGridTextBoxColumn5, Me.DataGridTextBoxColumn1, Me.DataGridTextBoxColumn2, Me.DataGridTextBoxColumn3, Me.DataGridTextBoxColumn4, Me.DataGridTextBoxColumn6, Me.DataGridTextBoxColumn26})
        Me.DataGridTableStyle1.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.DataGridTableStyle1.MappingName = "GetContacts"
        Me.DataGridTableStyle1.RowHeaderWidth = 15
        '
        'DataGridTextBoxColumn5
        '
        Me.DataGridTextBoxColumn5.Format = ""
        Me.DataGridTextBoxColumn5.FormatInfo = Nothing
        Me.DataGridTextBoxColumn5.HeaderText = "ContactID"
        Me.DataGridTextBoxColumn5.MappingName = "ContactID"
        Me.DataGridTextBoxColumn5.Width = 0
        '
        'DataGridTextBoxColumn1
        '
        Me.DataGridTextBoxColumn1.Format = ""
        Me.DataGridTextBoxColumn1.FormatInfo = Nothing
        Me.DataGridTextBoxColumn1.HeaderText = "Last Name"
        Me.DataGridTextBoxColumn1.MappingName = "LastName"
        Me.DataGridTextBoxColumn1.Width = 75
        '
        'DataGridTextBoxColumn2
        '
        Me.DataGridTextBoxColumn2.Format = ""
        Me.DataGridTextBoxColumn2.FormatInfo = Nothing
        Me.DataGridTextBoxColumn2.HeaderText = "FirstName"
        Me.DataGridTextBoxColumn2.MappingName = "FirstName"
        Me.DataGridTextBoxColumn2.Width = 75
        '
        'DataGridTextBoxColumn3
        '
        Me.DataGridTextBoxColumn3.Format = ""
        Me.DataGridTextBoxColumn3.FormatInfo = Nothing
        Me.DataGridTextBoxColumn3.HeaderText = "JobTitle"
        Me.DataGridTextBoxColumn3.MappingName = "JobTitle"
        Me.DataGridTextBoxColumn3.Width = 150
        '
        'DataGridTextBoxColumn4
        '
        Me.DataGridTextBoxColumn4.Format = ""
        Me.DataGridTextBoxColumn4.FormatInfo = Nothing
        Me.DataGridTextBoxColumn4.HeaderText = "Email"
        Me.DataGridTextBoxColumn4.MappingName = "Email"
        Me.DataGridTextBoxColumn4.Width = 150
        '
        'DataGridTextBoxColumn6
        '
        Me.DataGridTextBoxColumn6.Format = ""
        Me.DataGridTextBoxColumn6.FormatInfo = Nothing
        Me.DataGridTextBoxColumn6.HeaderText = "Staff"
        Me.DataGridTextBoxColumn6.MappingName = "Staff"
        Me.DataGridTextBoxColumn6.Width = 75
        '
        'DataGridTextBoxColumn26
        '
        Me.DataGridTextBoxColumn26.Format = ""
        Me.DataGridTextBoxColumn26.FormatInfo = Nothing
        Me.DataGridTextBoxColumn26.HeaderText = "Note"
        Me.DataGridTextBoxColumn26.MappingName = "Notes"
        Me.DataGridTextBoxColumn26.Width = 75
        '
        'DataGridTableStyle2
        '
        Me.DataGridTableStyle2.DataGrid = Me.grdMain
        Me.DataGridTableStyle2.ForeColor = System.Drawing.Color.DarkGreen
        Me.DataGridTableStyle2.GridColumnStyles.AddRange(New System.Windows.Forms.DataGridColumnStyle() {Me.DataGridTextBoxColumn7, Me.DataGridTextBoxColumn8, Me.DataGridTextBoxColumn9, Me.DataGridTextBoxColumn10, Me.DataGridTextBoxColumn14, Me.DataGridTextBoxColumn11, Me.DataGridTextBoxColumn12, Me.DataGridTextBoxColumn13})
        Me.DataGridTableStyle2.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.DataGridTableStyle2.MappingName = "GetCases"
        Me.DataGridTableStyle2.RowHeaderWidth = 15
        '
        'DataGridTextBoxColumn7
        '
        Me.DataGridTextBoxColumn7.Format = ""
        Me.DataGridTextBoxColumn7.FormatInfo = Nothing
        Me.DataGridTextBoxColumn7.HeaderText = "CaseID"
        Me.DataGridTextBoxColumn7.MappingName = "CaseID"
        Me.DataGridTextBoxColumn7.Width = 0
        '
        'DataGridTextBoxColumn8
        '
        Me.DataGridTextBoxColumn8.Format = ""
        Me.DataGridTextBoxColumn8.FormatInfo = Nothing
        Me.DataGridTextBoxColumn8.HeaderText = "Case Name"
        Me.DataGridTextBoxColumn8.MappingName = "CaseName"
        Me.DataGridTextBoxColumn8.Width = 150
        '
        'DataGridTextBoxColumn9
        '
        Me.DataGridTextBoxColumn9.Format = ""
        Me.DataGridTextBoxColumn9.FormatInfo = Nothing
        Me.DataGridTextBoxColumn9.HeaderText = "Status"
        Me.DataGridTextBoxColumn9.MappingName = "CaseStatus"
        Me.DataGridTextBoxColumn9.Width = 125
        '
        'DataGridTextBoxColumn10
        '
        Me.DataGridTextBoxColumn10.Format = ""
        Me.DataGridTextBoxColumn10.FormatInfo = Nothing
        Me.DataGridTextBoxColumn10.HeaderText = "Manager"
        Me.DataGridTextBoxColumn10.MappingName = "CaseMgr"
        Me.DataGridTextBoxColumn10.Width = 75
        '
        'DataGridTextBoxColumn14
        '
        Me.DataGridTextBoxColumn14.Format = ""
        Me.DataGridTextBoxColumn14.FormatInfo = Nothing
        Me.DataGridTextBoxColumn14.HeaderText = "Grant"
        Me.DataGridTextBoxColumn14.MappingName = "Grant"
        Me.DataGridTextBoxColumn14.Width = 75
        '
        'DataGridTextBoxColumn11
        '
        Me.DataGridTextBoxColumn11.Format = ""
        Me.DataGridTextBoxColumn11.FormatInfo = Nothing
        Me.DataGridTextBoxColumn11.HeaderText = "CRG"
        Me.DataGridTextBoxColumn11.MappingName = "CRGName"
        Me.DataGridTextBoxColumn11.Width = 75
        '
        'DataGridTextBoxColumn12
        '
        Me.DataGridTextBoxColumn12.Format = "d"
        Me.DataGridTextBoxColumn12.FormatInfo = Nothing
        Me.DataGridTextBoxColumn12.HeaderText = "DateOpen"
        Me.DataGridTextBoxColumn12.MappingName = "DateOpen"
        Me.DataGridTextBoxColumn12.Width = 75
        '
        'DataGridTextBoxColumn13
        '
        Me.DataGridTextBoxColumn13.Format = ""
        Me.DataGridTextBoxColumn13.FormatInfo = Nothing
        Me.DataGridTextBoxColumn13.HeaderText = "Description"
        Me.DataGridTextBoxColumn13.MappingName = "Description"
        Me.DataGridTextBoxColumn13.Width = 150
        '
        'DataGridTableStyle3
        '
        Me.DataGridTableStyle3.DataGrid = Me.grdMain
        Me.DataGridTableStyle3.ForeColor = System.Drawing.Color.DarkGreen
        Me.DataGridTableStyle3.GridColumnStyles.AddRange(New System.Windows.Forms.DataGridColumnStyle() {Me.DataGridTextBoxColumn15, Me.DataGridTextBoxColumn19, Me.DataGridTextBoxColumn16, Me.DataGridTextBoxColumn17, Me.DataGridTextBoxColumn18, Me.DataGridTextBoxColumn23})
        Me.DataGridTableStyle3.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.DataGridTableStyle3.MappingName = "GetGrants"
        Me.DataGridTableStyle3.RowHeaderWidth = 15
        '
        'DataGridTextBoxColumn15
        '
        Me.DataGridTextBoxColumn15.Format = ""
        Me.DataGridTextBoxColumn15.FormatInfo = Nothing
        Me.DataGridTextBoxColumn15.HeaderText = "GrantID"
        Me.DataGridTextBoxColumn15.MappingName = "GrantIDtxt"
        Me.DataGridTextBoxColumn15.Width = 0
        '
        'DataGridTextBoxColumn19
        '
        Me.DataGridTextBoxColumn19.Format = ""
        Me.DataGridTextBoxColumn19.FormatInfo = Nothing
        Me.DataGridTextBoxColumn19.HeaderText = "GIID"
        Me.DataGridTextBoxColumn19.MappingName = "GIID"
        Me.DataGridTextBoxColumn19.Width = 0
        '
        'DataGridTextBoxColumn16
        '
        Me.DataGridTextBoxColumn16.Format = "c"
        Me.DataGridTextBoxColumn16.FormatInfo = Nothing
        Me.DataGridTextBoxColumn16.HeaderText = "Amount"
        Me.DataGridTextBoxColumn16.MappingName = "Amount"
        Me.DataGridTextBoxColumn16.Width = 75
        '
        'DataGridTextBoxColumn17
        '
        Me.DataGridTextBoxColumn17.Format = ""
        Me.DataGridTextBoxColumn17.FormatInfo = Nothing
        Me.DataGridTextBoxColumn17.HeaderText = "Type"
        Me.DataGridTextBoxColumn17.MappingName = "TypeofGrant"
        Me.DataGridTextBoxColumn17.Width = 150
        '
        'DataGridTextBoxColumn18
        '
        Me.DataGridTextBoxColumn18.Format = ""
        Me.DataGridTextBoxColumn18.FormatInfo = Nothing
        Me.DataGridTextBoxColumn18.HeaderText = "Latest Activity"
        Me.DataGridTextBoxColumn18.MappingName = "GrantLatest"
        Me.DataGridTextBoxColumn18.Width = 150
        '
        'DataGridTextBoxColumn23
        '
        Me.DataGridTextBoxColumn23.Format = ""
        Me.DataGridTextBoxColumn23.FormatInfo = Nothing
        Me.DataGridTextBoxColumn23.HeaderText = "Case Name"
        Me.DataGridTextBoxColumn23.MappingName = "CaseName"
        Me.DataGridTextBoxColumn23.Width = 175
        '
        'DataGridTableStyle4
        '
        Me.DataGridTableStyle4.DataGrid = Me.grdMain
        Me.DataGridTableStyle4.GridColumnStyles.AddRange(New System.Windows.Forms.DataGridColumnStyle() {Me.DataGridTextBoxColumn20, Me.DataGridTextBoxColumn27, Me.DataGridTextBoxColumn21})
        Me.DataGridTableStyle4.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.DataGridTableStyle4.MappingName = "GetOrgStories"
        '
        'DataGridTextBoxColumn20
        '
        Me.DataGridTextBoxColumn20.Format = ""
        Me.DataGridTextBoxColumn20.FormatInfo = Nothing
        Me.DataGridTextBoxColumn20.HeaderText = "StoryID"
        Me.DataGridTextBoxColumn20.MappingName = "StoryID"
        Me.DataGridTextBoxColumn20.Width = 0
        '
        'DataGridTextBoxColumn27
        '
        Me.DataGridTextBoxColumn27.Format = "d"
        Me.DataGridTextBoxColumn27.FormatInfo = Nothing
        Me.DataGridTextBoxColumn27.HeaderText = "Date"
        Me.DataGridTextBoxColumn27.MappingName = "CreateDate"
        Me.DataGridTextBoxColumn27.Width = 75
        '
        'DataGridTextBoxColumn21
        '
        Me.DataGridTextBoxColumn21.Format = ""
        Me.DataGridTextBoxColumn21.FormatInfo = Nothing
        Me.DataGridTextBoxColumn21.HeaderText = "Headline"
        Me.DataGridTextBoxColumn21.MappingName = "Headline"
        Me.DataGridTextBoxColumn21.Width = 250
        '
        'DataGridTableStyle5
        '
        Me.DataGridTableStyle5.DataGrid = Me.grdMain
        Me.DataGridTableStyle5.GridColumnStyles.AddRange(New System.Windows.Forms.DataGridColumnStyle() {Me.DataGridTextBoxColumn22, Me.DataGridTextBoxColumn25, Me.DataGridTextBoxColumn24, Me.DataGridTextBoxColumn28, Me.DataGridTextBoxColumn29})
        Me.DataGridTableStyle5.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.DataGridTableStyle5.MappingName = "GetOrgAlerts"
        '
        'DataGridTextBoxColumn22
        '
        Me.DataGridTextBoxColumn22.Format = ""
        Me.DataGridTextBoxColumn22.FormatInfo = Nothing
        Me.DataGridTextBoxColumn22.HeaderText = "AlertID"
        Me.DataGridTextBoxColumn22.MappingName = "AlertID"
        Me.DataGridTextBoxColumn22.Width = 0
        '
        'DataGridTextBoxColumn25
        '
        Me.DataGridTextBoxColumn25.Format = "d"
        Me.DataGridTextBoxColumn25.FormatInfo = Nothing
        Me.DataGridTextBoxColumn25.HeaderText = "Date"
        Me.DataGridTextBoxColumn25.MappingName = "CreateDate"
        Me.DataGridTextBoxColumn25.Width = 75
        '
        'DataGridTextBoxColumn24
        '
        Me.DataGridTextBoxColumn24.Format = ""
        Me.DataGridTextBoxColumn24.FormatInfo = Nothing
        Me.DataGridTextBoxColumn24.HeaderText = "Type"
        Me.DataGridTextBoxColumn24.MappingName = "Type"
        Me.DataGridTextBoxColumn24.Width = 75
        '
        'DataGridTextBoxColumn28
        '
        Me.DataGridTextBoxColumn28.Format = ""
        Me.DataGridTextBoxColumn28.FormatInfo = Nothing
        Me.DataGridTextBoxColumn28.HeaderText = "Headline"
        Me.DataGridTextBoxColumn28.MappingName = "Headline"
        Me.DataGridTextBoxColumn28.Width = 250
        '
        'DataGridTextBoxColumn29
        '
        Me.DataGridTextBoxColumn29.Format = "d"
        Me.DataGridTextBoxColumn29.FormatInfo = Nothing
        Me.DataGridTextBoxColumn29.HeaderText = "Cancelled"
        Me.DataGridTextBoxColumn29.MappingName = "CancelDate"
        Me.DataGridTextBoxColumn29.Width = 75
        '
        'lblSelectedID
        '
        Me.lblSelectedID.BackColor = System.Drawing.SystemColors.Control
        Me.lblSelectedID.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSelectedID.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.lblSelectedID.Location = New System.Drawing.Point(912, 579)
        Me.lblSelectedID.Name = "lblSelectedID"
        Me.lblSelectedID.Size = New System.Drawing.Size(50, 14)
        Me.lblSelectedID.TabIndex = 781
        Me.lblSelectedID.Text = "ID"
        '
        'flagOutofRegion
        '
        Me.flagOutofRegion.BackColor = System.Drawing.Color.Yellow
        Me.flagOutofRegion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.flagOutofRegion.Enabled = False
        Me.flagOutofRegion.Location = New System.Drawing.Point(610, 10)
        Me.flagOutofRegion.Name = "flagOutofRegion"
        Me.flagOutofRegion.Size = New System.Drawing.Size(110, 17)
        Me.flagOutofRegion.TabIndex = 234
        Me.flagOutofRegion.Text = "Out of Service Area"
        Me.flagOutofRegion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.flagOutofRegion.Visible = False
        '
        'pnlMain
        '
        Me.pnlMain.BackColor = System.Drawing.SystemColors.InactiveBorder
        Me.pnlMain.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pnlMain.Controls.Add(Me.TabControl1)
        Me.pnlMain.Controls.Add(Me.pnlExtra)
        Me.pnlMain.Controls.Add(Me.grdMain)
        Me.pnlMain.Controls.Add(Me.txtEIN)
        Me.pnlMain.Controls.Add(Me.Label13)
        Me.pnlMain.Controls.Add(Me.pnlAddress)
        Me.pnlMain.Controls.Add(Me.lblTimeZone)
        Me.pnlMain.Controls.Add(Me.lblName)
        Me.pnlMain.Controls.Add(Me.editOrgName)
        Me.pnlMain.Controls.Add(Me.editNotes)
        Me.pnlMain.Controls.Add(Me.lblNotes)
        Me.pnlMain.Controls.Add(Me.editWebsite)
        Me.pnlMain.Controls.Add(Me.lblWebsite)
        Me.pnlMain.Controls.Add(Me.editEmail)
        Me.pnlMain.Controls.Add(Me.lblEmail)
        Me.pnlMain.Controls.Add(Me.editPhone)
        Me.pnlMain.Controls.Add(Me.lblFax)
        Me.pnlMain.Controls.Add(Me.editFax)
        Me.pnlMain.Controls.Add(Me.lblPhone)
        Me.pnlMain.Controls.Add(Me.pnlProfile)
        Me.pnlMain.Location = New System.Drawing.Point(15, 61)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(955, 518)
        Me.pnlMain.TabIndex = 0
        '
        'txtEIN
        '
        Me.txtEIN.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainOrgBindingSource, "EIN", True))
        Me.txtEIN.Location = New System.Drawing.Point(80, 480)
        Me.txtEIN.MaxLength = 100
        Me.txtEIN.Name = "txtEIN"
        Me.txtEIN.Size = New System.Drawing.Size(98, 20)
        Me.txtEIN.TabIndex = 15
        Me.txtEIN.Text = "_ _-_ _ _ _ _ _ _"
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.Transparent
        Me.Label13.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(29, 473)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(40, 23)
        Me.Label13.TabIndex = 134
        Me.Label13.Text = "EIN:"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'pnlAddress
        '
        Me.pnlAddress.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pnlAddress.Controls.Add(Me.tbAddress)
        Me.pnlAddress.Location = New System.Drawing.Point(3, 54)
        Me.pnlAddress.Name = "pnlAddress"
        Me.pnlAddress.Size = New System.Drawing.Size(319, 131)
        Me.pnlAddress.TabIndex = 1
        '
        'tbAddress
        '
        Me.tbAddress.Controls.Add(Me.pgMailing)
        Me.tbAddress.Controls.Add(Me.pgPhysical)
        Me.tbAddress.Location = New System.Drawing.Point(-1, 3)
        Me.tbAddress.Name = "tbAddress"
        Me.tbAddress.SelectedIndex = 0
        Me.tbAddress.Size = New System.Drawing.Size(314, 121)
        Me.tbAddress.TabIndex = 1
        '
        'pgMailing
        '
        Me.pgMailing.Controls.Add(Me.editCity)
        Me.pgMailing.Controls.Add(Me.editZip)
        Me.pgMailing.Controls.Add(Me.editStateAB)
        Me.pgMailing.Controls.Add(Me.lblCity)
        Me.pgMailing.Controls.Add(Me.Country)
        Me.pgMailing.Controls.Add(Me.Label9)
        Me.pgMailing.Controls.Add(Me.editCountry)
        Me.pgMailing.Controls.Add(Me.lblStateAb)
        Me.pgMailing.Controls.Add(Me.lblZip)
        Me.pgMailing.Controls.Add(Me.editStreet1)
        Me.pgMailing.Location = New System.Drawing.Point(4, 22)
        Me.pgMailing.Name = "pgMailing"
        Me.pgMailing.Padding = New System.Windows.Forms.Padding(3)
        Me.pgMailing.Size = New System.Drawing.Size(306, 95)
        Me.pgMailing.TabIndex = 0
        Me.pgMailing.Text = " (Mailing) Address"
        Me.pgMailing.UseVisualStyleBackColor = True
        '
        'editCity
        '
        Me.editCity.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainOrgBindingSource, "City", True))
        Me.editCity.Location = New System.Drawing.Point(50, 36)
        Me.editCity.MaxLength = 20
        Me.editCity.Name = "editCity"
        Me.editCity.Size = New System.Drawing.Size(100, 20)
        Me.editCity.TabIndex = 3
        '
        'editZip
        '
        Me.editZip.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainOrgBindingSource, "Zip", True))
        Me.editZip.Location = New System.Drawing.Point(50, 64)
        Me.editZip.MaxLength = 10
        Me.editZip.Name = "editZip"
        Me.editZip.Size = New System.Drawing.Size(100, 20)
        Me.editZip.TabIndex = 7
        '
        'editStateAB
        '
        Me.editStateAB.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainOrgBindingSource, "State", True))
        Me.editStateAB.Location = New System.Drawing.Point(211, 36)
        Me.editStateAB.Name = "editStateAB"
        Me.editStateAB.Size = New System.Drawing.Size(89, 20)
        Me.editStateAB.TabIndex = 5
        '
        'lblCity
        '
        Me.lblCity.BackColor = System.Drawing.Color.Transparent
        Me.lblCity.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCity.Location = New System.Drawing.Point(7, 31)
        Me.lblCity.Name = "lblCity"
        Me.lblCity.Size = New System.Drawing.Size(40, 23)
        Me.lblCity.TabIndex = 133
        Me.lblCity.Text = "City:"
        Me.lblCity.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(1, 7)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(46, 21)
        Me.Label9.TabIndex = 135
        Me.Label9.Text = "Street:"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblZip
        '
        Me.lblZip.BackColor = System.Drawing.Color.Transparent
        Me.lblZip.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblZip.Location = New System.Drawing.Point(6, 61)
        Me.lblZip.Name = "lblZip"
        Me.lblZip.Size = New System.Drawing.Size(40, 23)
        Me.lblZip.TabIndex = 134
        Me.lblZip.Text = "Zip:"
        Me.lblZip.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'editStreet1
        '
        Me.editStreet1.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainOrgBindingSource, "Street1", True))
        Me.editStreet1.Location = New System.Drawing.Point(50, 9)
        Me.editStreet1.MaxLength = 50
        Me.editStreet1.Name = "editStreet1"
        Me.editStreet1.Size = New System.Drawing.Size(250, 20)
        Me.editStreet1.TabIndex = 1
        '
        'pgPhysical
        '
        Me.pgPhysical.Controls.Add(Me.btnNewPhysical)
        Me.pgPhysical.Controls.Add(Me.Label23)
        Me.pgPhysical.Controls.Add(Me.EditPhysicalCity)
        Me.pgPhysical.Controls.Add(Me.editPhysicalState)
        Me.pgPhysical.Controls.Add(Me.Label5)
        Me.pgPhysical.Controls.Add(Me.Label20)
        Me.pgPhysical.Controls.Add(Me.Label21)
        Me.pgPhysical.Controls.Add(Me.editPhysicalCountry)
        Me.pgPhysical.Controls.Add(Me.Label22)
        Me.pgPhysical.Controls.Add(Me.editPhysicalStreet)
        Me.pgPhysical.Controls.Add(Me.editPhysicalZip)
        Me.pgPhysical.Controls.Add(Me.fldPhysicalAddress)
        Me.pgPhysical.Location = New System.Drawing.Point(4, 22)
        Me.pgPhysical.Name = "pgPhysical"
        Me.pgPhysical.Padding = New System.Windows.Forms.Padding(3)
        Me.pgPhysical.Size = New System.Drawing.Size(306, 95)
        Me.pgPhysical.TabIndex = 1
        Me.pgPhysical.Text = "Physical Location (if different)"
        Me.pgPhysical.UseVisualStyleBackColor = True
        '
        'Label23
        '
        Me.Label23.BackColor = System.Drawing.Color.Transparent
        Me.Label23.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.Location = New System.Drawing.Point(6, 61)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(40, 23)
        Me.Label23.TabIndex = 263
        Me.Label23.Text = "Zip:"
        Me.Label23.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'EditPhysicalCity
        '
        Me.EditPhysicalCity.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.AddressBindingSource, "City", True))
        Me.EditPhysicalCity.Location = New System.Drawing.Point(50, 36)
        Me.EditPhysicalCity.MaxLength = 20
        Me.EditPhysicalCity.Name = "EditPhysicalCity"
        Me.EditPhysicalCity.Size = New System.Drawing.Size(106, 20)
        Me.EditPhysicalCity.TabIndex = 4
        '
        'editPhysicalState
        '
        Me.editPhysicalState.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.AddressBindingSource, "State", True))
        Me.editPhysicalState.Location = New System.Drawing.Point(212, 37)
        Me.editPhysicalState.Name = "editPhysicalState"
        Me.editPhysicalState.Size = New System.Drawing.Size(84, 20)
        Me.editPhysicalState.TabIndex = 6
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(7, 31)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(40, 23)
        Me.Label5.TabIndex = 258
        Me.Label5.Text = "City:"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label20
        '
        Me.Label20.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label20.Location = New System.Drawing.Point(157, 60)
        Me.Label20.Margin = New System.Windows.Forms.Padding(0)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(52, 23)
        Me.Label20.TabIndex = 261
        Me.Label20.Text = "Country:"
        Me.Label20.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label21
        '
        Me.Label21.BackColor = System.Drawing.Color.Transparent
        Me.Label21.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.Location = New System.Drawing.Point(1, 7)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(46, 21)
        Me.Label21.TabIndex = 259
        Me.Label21.Text = "Street:"
        Me.Label21.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'editPhysicalCountry
        '
        Me.editPhysicalCountry.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.AddressBindingSource, "Country", True))
        Me.editPhysicalCountry.Location = New System.Drawing.Point(209, 64)
        Me.editPhysicalCountry.MaxLength = 75
        Me.editPhysicalCountry.Name = "editPhysicalCountry"
        Me.editPhysicalCountry.Size = New System.Drawing.Size(86, 20)
        Me.editPhysicalCountry.TabIndex = 10
        Me.editPhysicalCountry.Text = "USA"
        '
        'editPhysicalStreet
        '
        Me.editPhysicalStreet.BackColor = System.Drawing.SystemColors.Window
        Me.editPhysicalStreet.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.AddressBindingSource, "Street1", True))
        Me.editPhysicalStreet.Location = New System.Drawing.Point(50, 9)
        Me.editPhysicalStreet.MaxLength = 50
        Me.editPhysicalStreet.Name = "editPhysicalStreet"
        Me.editPhysicalStreet.Size = New System.Drawing.Size(185, 20)
        Me.editPhysicalStreet.TabIndex = 2
        '
        'lblTimeZone
        '
        Me.lblTimeZone.BackColor = System.Drawing.Color.Transparent
        Me.lblTimeZone.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTimeZone.Location = New System.Drawing.Point(199, 216)
        Me.lblTimeZone.Name = "lblTimeZone"
        Me.lblTimeZone.Size = New System.Drawing.Size(104, 20)
        Me.lblTimeZone.TabIndex = 130
        Me.lblTimeZone.Text = "Time Zone"
        Me.lblTimeZone.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblName
        '
        Me.lblName.BackColor = System.Drawing.Color.Transparent
        Me.lblName.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblName.Location = New System.Drawing.Point(1, 12)
        Me.lblName.Name = "lblName"
        Me.lblName.Size = New System.Drawing.Size(47, 23)
        Me.lblName.TabIndex = 129
        Me.lblName.Text = "Name:"
        '
        'editOrgName
        '
        Me.editOrgName.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainOrgBindingSource, "OrgName", True))
        Me.editOrgName.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.editOrgName.ForeColor = System.Drawing.SystemColors.ControlText
        Me.editOrgName.Location = New System.Drawing.Point(49, 8)
        Me.editOrgName.MaxLength = 100
        Me.editOrgName.Multiline = True
        Me.editOrgName.Name = "editOrgName"
        Me.editOrgName.Size = New System.Drawing.Size(261, 40)
        Me.editOrgName.TabIndex = 0
        Me.editOrgName.Tag = "ORGANIZATION NAME"
        Me.editOrgName.Text = "OrgName"
        '
        'editNotes
        '
        Me.editNotes.AcceptsReturn = True
        Me.editNotes.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainOrgBindingSource, "Notes", True))
        Me.editNotes.Location = New System.Drawing.Point(79, 323)
        Me.editNotes.Multiline = True
        Me.editNotes.Name = "editNotes"
        Me.editNotes.Size = New System.Drawing.Size(223, 124)
        Me.editNotes.TabIndex = 13
        '
        'lblNotes
        '
        Me.lblNotes.BackColor = System.Drawing.Color.Transparent
        Me.lblNotes.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNotes.Location = New System.Drawing.Point(31, 319)
        Me.lblNotes.Name = "lblNotes"
        Me.lblNotes.Size = New System.Drawing.Size(43, 23)
        Me.lblNotes.TabIndex = 126
        Me.lblNotes.Text = "Notes:"
        Me.lblNotes.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblWebsite
        '
        Me.lblWebsite.BackColor = System.Drawing.Color.Transparent
        Me.lblWebsite.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblWebsite.Location = New System.Drawing.Point(5, 279)
        Me.lblWebsite.Name = "lblWebsite"
        Me.lblWebsite.Size = New System.Drawing.Size(70, 23)
        Me.lblWebsite.TabIndex = 122
        Me.lblWebsite.Text = "Website:"
        Me.lblWebsite.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblEmail
        '
        Me.lblEmail.BackColor = System.Drawing.Color.Transparent
        Me.lblEmail.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEmail.Location = New System.Drawing.Point(5, 245)
        Me.lblEmail.Name = "lblEmail"
        Me.lblEmail.Size = New System.Drawing.Size(70, 32)
        Me.lblEmail.TabIndex = 105
        Me.lblEmail.Text = "General Email:"
        Me.lblEmail.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'editPhone
        '
        Me.editPhone.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainOrgBindingSource, "Phone", True))
        Me.editPhone.Location = New System.Drawing.Point(80, 191)
        Me.editPhone.MaxLength = 15
        Me.editPhone.Name = "editPhone"
        Me.editPhone.Size = New System.Drawing.Size(106, 20)
        Me.editPhone.TabIndex = 7
        '
        'lblFax
        '
        Me.lblFax.BackColor = System.Drawing.Color.Transparent
        Me.lblFax.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFax.Location = New System.Drawing.Point(5, 217)
        Me.lblFax.Name = "lblFax"
        Me.lblFax.Size = New System.Drawing.Size(70, 23)
        Me.lblFax.TabIndex = 106
        Me.lblFax.Text = "Fax:"
        Me.lblFax.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'editFax
        '
        Me.editFax.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainOrgBindingSource, "Fax", True))
        Me.editFax.Location = New System.Drawing.Point(80, 220)
        Me.editFax.MaxLength = 15
        Me.editFax.Name = "editFax"
        Me.editFax.Size = New System.Drawing.Size(106, 20)
        Me.editFax.TabIndex = 8
        '
        'lblPhone
        '
        Me.lblPhone.BackColor = System.Drawing.Color.Transparent
        Me.lblPhone.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPhone.Location = New System.Drawing.Point(5, 188)
        Me.lblPhone.Name = "lblPhone"
        Me.lblPhone.Size = New System.Drawing.Size(70, 23)
        Me.lblPhone.TabIndex = 77
        Me.lblPhone.Text = "Phone:"
        Me.lblPhone.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.SystemColors.Control
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label6.Location = New System.Drawing.Point(10, 10)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(205, 27)
        Me.Label6.TabIndex = 231
        Me.Label6.Text = "ORGANIZATION DETAIL"
        '
        'flagWarning
        '
        Me.flagWarning.BackColor = System.Drawing.Color.Yellow
        Me.flagWarning.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.flagWarning.ForeColor = System.Drawing.Color.Red
        Me.flagWarning.Location = New System.Drawing.Point(348, 5)
        Me.flagWarning.Name = "flagWarning"
        Me.flagWarning.Size = New System.Drawing.Size(256, 19)
        Me.flagWarning.TabIndex = 232
        Me.flagWarning.Text = "see ALERTS tab"
        Me.flagWarning.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.flagWarning.Visible = False
        '
        'TimerFlag
        '
        Me.TimerFlag.Interval = 300
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.SystemColors.Highlight
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Controls.Add(Me.btnDelete)
        Me.Panel2.Controls.Add(Me.btnNew)
        Me.Panel2.Controls.Add(Me.btnSaveExit)
        Me.Panel2.Location = New System.Drawing.Point(808, 5)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(131, 40)
        Me.Panel2.TabIndex = 780
        '
        'flgOutstanding
        '
        Me.flgOutstanding.BackColor = System.Drawing.Color.Yellow
        Me.flgOutstanding.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.flgOutstanding.ForeColor = System.Drawing.Color.Red
        Me.flgOutstanding.Location = New System.Drawing.Point(348, 28)
        Me.flgOutstanding.Name = "flgOutstanding"
        Me.flgOutstanding.Size = New System.Drawing.Size(256, 19)
        Me.flgOutstanding.TabIndex = 783
        Me.flgOutstanding.Text = "Outstanding Grant Report"
        Me.flgOutstanding.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.flgOutstanding.Visible = False
        '
        'ErrorProvider1
        '
        Me.ErrorProvider1.ContainerControl = Me
        '
        'lblSelectedWhat
        '
        Me.lblSelectedWhat.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSelectedWhat.ForeColor = System.Drawing.SystemColors.ControlDark
        Me.lblSelectedWhat.Location = New System.Drawing.Point(805, 581)
        Me.lblSelectedWhat.Name = "lblSelectedWhat"
        Me.lblSelectedWhat.Size = New System.Drawing.Size(100, 15)
        Me.lblSelectedWhat.TabIndex = 786
        Me.lblSelectedWhat.Text = "Selection #"
        Me.lblSelectedWhat.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblDenomination
        '
        Me.lblDenomination.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDenomination.ForeColor = System.Drawing.Color.ForestGreen
        Me.lblDenomination.Location = New System.Drawing.Point(3, 58)
        Me.lblDenomination.Name = "lblDenomination"
        Me.lblDenomination.Size = New System.Drawing.Size(89, 23)
        Me.lblDenomination.TabIndex = 122
        Me.lblDenomination.Text = "Denomination:"
        Me.lblDenomination.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'frmMainOrg
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.CausesValidation = False
        Me.ClientSize = New System.Drawing.Size(1000, 641)
        Me.Controls.Add(Me.btnTest2)
        Me.Controls.Add(Me.btnTest)
        Me.Controls.Add(Me.flagOutofRegion)
        Me.Controls.Add(Me.lblSelectedWhat)
        Me.Controls.Add(Me.flgOutstanding)
        Me.Controls.Add(Me.lblSelectedID)
        Me.Controls.Add(Me.btnHelp)
        Me.Controls.Add(Me.StatusBar1)
        Me.Controls.Add(Me.btnReport)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.flagWarning)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.pnlMain)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Menu = Me.MainMenu1
        Me.Name = "frmMainOrg"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Tag = "ORGANIZATION"
        Me.Text = "ORGANIZATION DETAIL"
        Me.TabControl1.ResumeLayout(False)
        CType(Me.MainOrgBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsMainOrgWAddress1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlRegion.ResumeLayout(False)
        CType(Me.StatusBarPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.StatusBarPanelID, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.StatusBarPanel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.AddressBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlStats.ResumeLayout(False)
        Me.pnlStats.PerformLayout()
        Me.pnlProfile.ResumeLayout(False)
        Me.pnlProfile.PerformLayout()
        Me.PnlMail.ResumeLayout(False)
        Me.pnlEthnicity.ResumeLayout(False)
        Me.pnlEthnicity.PerformLayout()
        Me.pnlExtra.ResumeLayout(False)
        Me.pnlExtra.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        CType(Me.grdMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlMain.ResumeLayout(False)
        Me.pnlMain.PerformLayout()
        Me.pnlAddress.ResumeLayout(False)
        Me.tbAddress.ResumeLayout(False)
        Me.pgMailing.ResumeLayout(False)
        Me.pgMailing.PerformLayout()
        Me.pgPhysical.ResumeLayout(False)
        Me.pgPhysical.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    'ToDo ENHANCE add zipcode/city code
    'TODO FUTURE allow open form to all orgs w navigation buttons; then make duplicate/inactive flag

#Region "Testing"

    'get status
    Private Sub btnTest_Click(sender As System.Object, e As System.EventArgs) Handles btnTest.Click

        MsgBox("TBL Name: " & Me.maintbl.TableName & NextLine &
               "NameSpace: " & Me.maintbl.Namespace.ToString & NextLine &
               "Rows: " & Me.maintbl.Rows.Count.ToString & NextLine &
                "DS HasChanges: " & mainDS.HasChanges.ToString & NextLine &
               "TBL RowState: " & Me.maintbl.Rows(0).RowState.ToString & NextLine &
            "TBL RowVersion 'Proposed': " & maintbl.Rows(0).HasVersion(DataRowVersion.Proposed).ToString, , "Main Table")


    End Sub

    'force end edit
    Private Sub btnTest2_Click(sender As System.Object, e As System.EventArgs) Handles btnTest2.Click
        mainBSrce.EndEdit()
    End Sub

#End Region 'testing

#Region "Load"

    'ON LOAD
    Private Sub frmMainOrg_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles MyBase.Load

        SetStatusBarText("loading")
SetMainDSConnection:
        Me.daOrg2.SelectCommand.Connection = sc
        Me.daOrg2Address.SelectCommand.Connection = sc
        Me.daOrg2.UpdateCommand.Connection = sc
        Me.daOrg2Address.UpdateCommand.Connection = sc

SetDefaults:
        enumContact = New structHeadings("Contact", "CONTACTS")
        enumCase = New structHeadings("Case", "CASES")
        enumGrant = New structHeadings("Grant", "GRANTS")

        ctlIdentify = Me.editOrgName
        ctlNeutral = Me.btnHelp
        mainTopic = "Organization"
        mainDS = Me.DsMainOrgWAddress1 'Me.DsMainOrg1
        maintbl = Me.DsMainOrgWAddress1.tblOrg1 'Me.DsMainOrg1.MainOrg

        mainBSrce = Me.MainOrgBindingSource
        mainDAdapt = Me.daOrg2 'Me.daMainOrg

        ' AddHandler maintbl.RowChanging, New DataRowChangeEventHandler(AddressOf Row_Changing)
        Me.SuspendLayout()
LoadCombos:
        Dim dr As SqlDataReader
        Dim cmdType As New SqlCommand("SELECT DISTINCT OrgType FROM tblOrg WHERE OrgType not in('pending','notfound','duplicate','duplicate?','Duplicate ?') ORDER BY OrgType")
        cmdType.Connection = sc
        Me.StatusBar1.Panels(0).Text = "Loading..."
        If Not SCConnect() Then
            Exit Sub
        End If

        Try
            dr = cmdType.ExecuteReader()
            While dr.Read()
                Me.cboType.Items.Add(dr.GetString(0))
            End While
            dr.Close()
        Catch ex As Exception
            modGlobalVar.msg("ERROR", "cmdtype execute reader error" & NextLine & ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Try
            cmdType.CommandText = "SELECT Distinct CongrCulture from tblorg where CongrCulture is not null order by CongrCulture "
            dr = cmdType.ExecuteReader()
            While dr.Read()
                Me.cboCulture.Items.Add(dr.GetString(0))
            End While
        Catch ex As Exception
            modGlobalVar.msg("ERROR: cboCulture execute  reader", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        dr.Close()
        cmdType.Dispose()
        sc.Close()

FormSetup:
        modGlobalVar.EnableGridTextboxes(Me.grdMain)
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        isloaded = True
        Forms.Add(Me)
        Me.ResumeLayout()
        SetStatusBarText("Done")
    End Sub

    'REFRESH BASED ON GLOBAL ID
    Public Sub ReLoad()
        SetStatusBarText("Reloading")

ResetVars:
        objHowClose = ObjClose.CloseByControl '(=5: form X)
        Me.StatusBarPanelID.Text = mainTopic & " ID: " & ThisID.ToString

FillGrid:
        FillSecondary()

        'CHECK IF ORG IS ALSO RESOURCE
        Dim i As Integer = 0
        Dim cmdR As New SqlCommand("SELECT ICCResourceID FROM tblResource WHERE OrgNum = " & ThisID, sc)
        If Not SCConnect() Then
            Exit Sub
        End If
        i = cmdR.ExecuteScalar()
        If i > 0 Then
            Me.chkResource.CheckState = CheckState.Checked
            Me.lblResource.Tag = i
            Me.lblResource.ForeColor = Color.ForestGreen
            Me.miGotoResource.Enabled = True
            Me.miMakeResource.Enabled = False
        Else
            Me.lblResource.ForeColor = Color.Black
            Me.miGotoResource.Enabled = False
            Me.miMakeResource.Enabled = True
        End If

        'TIME ZONE
        Dim cmdTime As New SqlCommand
        cmdTime.Connection = sc
        cmdTime.CommandText = "[luTimeZone]"
        cmdTime.CommandType = CommandType.StoredProcedure
        cmdTime.Parameters.Add("@OrgID", System.Data.SqlDbType.Int).Value = ThisID
        Me.lblTimeZone.Text = cmdTime.ExecuteScalar & " Time Zone"
        sc.Close()

        'CAN ENTER DATA BUT NOT EDIT InfoSource
        If Me.txtDatasource.Text = String.Empty Then
        Else
            Me.txtDatasource.Enabled = False
        End If

FindFiles:
        'TODO 2/16 bad assumption that Host spreadsheet will be only attached file
        'FullHostName = modPopup.FindFile(LinkedPath + "Organizations\", "Host " & ThisID.ToString, ".xls*", True)
        FullHostName = modPopup.GetFileName(LinkOrgPath, "Host " & ThisID.ToString & ".xls*", True)
        'multiple files:  modPopup.FindFiles(ThisID, LinkOrgPath, ppFile, ehFile, Me.miOpenFile, Me.btnViewFile, My.Resources.btnAttached, Me.ToolTip1, Nothing)

        If FullHostName = "error" Then
            SetStatusBarText("network error")
        Else
            SetMenuItem("Host Overview", Me.miHostOverview, Nothing, FullHostName)
            SetStatusBarText("Done")
        End If
    End Sub

    'SET HIDDEN STAFF HELP MESSAGE
    Public Sub GetHiddenStaffNames()
        Try
            Me.HelpProvider1.SetHelpString(fldCreateStaff, fldCreateStaff.Tag & ": " & modPopup.ShowStaff(fldCreateStaff.Text))
            Me.HelpProvider1.SetHelpString(fldLastChangeStaff, fldLastChangeStaff.Tag & ": " & modPopup.ShowStaff(fldLastChangeStaff.Text))
            Me.HelpProvider1.SetHelpString(fldReviewedBy, fldReviewedBy.Tag & ": " & modPopup.ShowStaff(fldReviewedBy.Text))
        Catch ex As Exception
        End Try
    End Sub

#End Region    'load

#Region "Update Main"

    'SAVE & EXIT
    Private Sub btnSaveExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnSaveExit.Click
        objHowClose = ObjClose.btnSaveExit
        Me.Close()
    End Sub

    'Changes?
    Public Function LookForChanges() As Boolean

        If bDirty = True Then
            '  MsgBox("true", , "bDirty")
            Return True
        Else
            Return modGlobalVar.AnyChanges(ctlNeutral, mainBSrce, maintbl) ', maintbl, bDirty)
        End If

    End Function

    'CLOSING & ask user & data validation & Release Form
    Private Sub frm_Closing(ByVal sender As Object, ByVal e As FormClosingEventArgs) _
               Handles MyBase.FormClosing
        'force valid email
        If Me.ErrorProvider1.GetError(Me.editEmail) = String.Empty Then
        Else
            If objHowClose = ObjClose.miAskSave Then '
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
        bChanges = LookForChanges()

AllowCloseWoutSaving:
        If objHowClose = ObjClose.miAskSave Then 'allow close without saving
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
        'If objHowClose = ObjClose.ReloadForm Then   'ask save, then open with new record
        '    If bChanges = True Then
        '        'ask if want to save changes
        '    End If
        'End If
        'SkipUpdate:  'TESTING - should not be required -- so LastChangeDate doesn't get updated
        '        If bChanges = False Then
        '            e.Cancel = False
        '            GoTo ReleaseForm
        '        End If

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
                    'INSERT DEFAULT DATA - no, could be blank last name
                    If objHowClose = ObjClose.SaveClose Or e.CloseReason = Windows.Forms.CloseReason.UserClosing Then
                        If ctl Is ctlIdentify Then
                            ctl.Text = usrName & " " & Today.ToShortDateString
                            mainBSrce.EndEdit()
                            mainDAdapt.Update(mainDS) 'save default data
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
            ClassOpenForms.frmMainOrg = Nothing 'reset global var
        Else
        End If
    End Sub

    'UPDATE
    Public Function DoUpdate() As Boolean
        MouseWait()
        mainBSrce.EndEdit() 'gets rid of proposed version
        AddressBindingSource.EndEdit()
UpdateBackend:
        'MAIN TABLE
        SetStatusBarText("Updating server 1")
        Try
            'i =
            mainDAdapt.Update(maintbl)
            '    Me.daOrg2Address.Update(Me.DsMainOrgWAddress1.tblAddress1)
            DoUpdate = True
        Catch dbcx As Data.DBConcurrencyException
            ' Dim RowError As DataRow = dbcx.Row()
            ' Declare an array variable for DataColumn objects.
            'Dim colArr() As DataColumn
            'Dim strb As New StringBuilder
            ' If the Row has errors, check use GetColumnsInError.
            'RETURNS BLANK MESSAGE
            'If RowError.HasErrors Then
            '    ' Get the array of columns in error.
            '    colArr = RowError.GetColumnsInError()
            '    For i As Integer = 0 To colArr.GetUpperBound(0)
            '        ' Insert code to fix errors on each column.
            '        strb.Append(colArr(i).ColumnName)
            '    Next i

            '    ' Clear errors after reconciling.
            '    RowError.ClearErrors()
            '    MsgBox(strb.tostring, , "concurrency error fields")
            'End If

            'modGlobalVar.msg("ERROR someone else changed the record", "Your changes will NOT be saved" & NextLine & dbcx.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
            ' DoUpdate = False
            Dim response As Windows.Forms.DialogResult
            'response = MessageBox.Show(CreateMessage_1(CType(dbcx.Row, dsMainOrgWAddress.tblOrg1Row)), "Concurrency Exception BEWARE", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning)
            'DoUpdate = ProcessDialogResult_4(response)

            response = MessageBox.Show(CreateMessage_1(maintbl.Rows(0), mainDAdapt, tConcurrency, maintbl), "     Concurrency Exception BEWARE                                             . ", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
            Select Case ProcessDialogResult_4(response, maintbl, mainDAdapt, tConcurrency)
                'Case Is = usrInput.Overwrite
                '    DoUpdate = True
                Case Is = usrInput.Reset
                    Return False

                Case Is = usrInput.Cancel
                    bDirty = True
                    Return False

            End Select

        Catch ex As Exception
            modGlobalVar.msg("Error updating " & mainTopic, ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
            DoUpdate = False
        Finally
            '  modGlobalVar.Msg("main: " & i.ToString& NextLine &  "addresses: " & f.ToString, , "updated")
        End Try

        'ADDRESS TABLE
        If Me.DsMainOrgWAddress1.tblAddress1.Rows.Count = 0 Then
            GoTo closeall
        Else

        End If
        SetStatusBarText("Updating server 2")
        Try
            ' Me.daOrg2Address.SelectCommand.Parameters("@OrgID").Value = ThisID
            Me.daOrg2Address.Update(Me.DsMainOrgWAddress1.tblAddress1)
            '    Me.daOrg2Address.Update(Me.DsMainOrgWAddress1.tblAddress1)
            DoUpdate = True
        Catch dbcx As Data.DBConcurrencyException
            tConcurrency.Clear()
            Dim response As Windows.Forms.DialogResult
            response = MessageBox.Show(CreateMessage_1(Me.DsMainOrgWAddress1.tblAddress1.Rows(0), Me.daOrg2Address, tConcurrency, Me.DsMainOrgWAddress1.tblAddress1), "     Concurrency Exception BEWARE                                             . ", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
            Select Case ProcessDialogResult_4(response, Me.DsMainOrgWAddress1.tblAddress1, Me.daOrg2Address, tConcurrency)
                'Case Is = usrInput.Overwrite
                '    DoUpdate = True
                Case Is = usrInput.Reset
                    Return False
                Case Is = usrInput.Cancel
                    bDirty = True
                    Return False
            End Select

        Catch ex As Exception
            modGlobalVar.msg("Error updating  address", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
            DoUpdate = False
        Finally
            '  modGlobalVar.Msg("main: " & i.ToString& NextLine &  "addresses: " & f.ToString, , "updated")
        End Try

CloseAll:
        SetStatusBarText("Update routine complete")
        MouseDefault()
    End Function

#End Region 'update

#Region "MenuItems"

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
        objHowClose = ObjClose.miSave
        DoUpdate()
        bDirty = False
    End Sub

    'CANCEL CHANGES
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles miCancel.Click
        Try
            mainBSrce.CancelEdit()
            mainDS.RejectChanges()
        Catch ex As System.Exception
            modGlobalVar.msg("ERROR: cancelling changes", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        SetStatusBarText("Changes Cancelled")
    End Sub

    'DELETE 
    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles miDeleteOrg.Click, btnDelete.Click

        Select Case modGlobalVar.msg("CONFIRM DELETE", mainTopic & ": " + IsNull(ctlIdentify.Text, "") & NextLine & " will be marked for deletion and the window closed.", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            Case Is = DialogResult.Yes
                Me.chkActive.CheckState = CheckState.Unchecked 'org, resource
                ctlIdentify.Text = "DELETE: " & IsNull(ctlIdentify.Text, "")
                '   Me.chkActive.Checked = False
                '  mainBSrce.EndEdit()
                objHowClose = ObjClose.miDelete
                Me.Close()
            Case Else   'do nothing
        End Select

    End Sub

    'ENABLE MENU ITEMS based on TabControl selected
    Private Sub mmGoTo_Popup(sender As System.Object, e As System.EventArgs) Handles mmGoto.Popup
        Me.miGotoCase.Enabled = False
        Me.miGotoContact.Enabled = False
        Me.miGotoGrant.Enabled = False
        Me.miGotoResource.Enabled = Me.chkResource.Checked

        Select Case Me.TabControl1.SelectedTab.Tag
            Case Is = enumCase.PluralName
                miGotoCase.Enabled = True
            Case Is = enumContact.PluralName
                Me.miGotoContact.Enabled = True
            Case Is = enumGrant.PluralName
                Me.miGotoGrant.Enabled = True
            Case Else
                'modGlobalVar.Msg(Me.TabControl1.SelectedTab.Name.ToString, , "clicked")
        End Select

    End Sub

    'BTN HELP
    Private Sub btnHelp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnHelp.Click, miHelp.Click
        modGlobalVar.msg("HELP: ORGANIZATION WINDOW", "TO ADD NEW ORGANIZATION:" & NextLine & _
        "  Go to the Organization Search Window and click the New button." & NextLine & NextLine & _
        "TO ADD NEW Contact, Case, Grant, Story, or Alert use the New button or File ..> New." & NextLine & NextLine & _
        "DENOMINATION: RightClick the Denomination field for choices." & NextLine & NextLine & _
        "PREDOMINANTLY & CULTURE/ETHINICITY fields: useful for our outreach and statistics. " & NextLine & _
        "   (Hispanic = Spanish speaking, may be of any race, from area colonized by Spain (like Mexico).  " + _
        "   Latino = broader term than Hispanic, Spanish or Portuguese speaking, from south of US border (incl Brazil)." & NextLine & NextLine & _
        "   A predominantly 'black' congregation is not necessarily African American; they may be more African and speak French," & NextLine & NextLine & _
        " so would go under 'Other' in the Ethnicity dropdown  with Ethiopian or French in the Language Specific dropdown).", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

#End Region 'menuitems

#Region "Validation"

    'CHECK REQUIRED FIELDS
    Private Function CheckRequired() As Control()
        Dim arCtls(0) As Control
        Dim ctl As Control
        Dim i As Integer = 0

        'Org Name
        ctl = ctlIdentify
        If Replace(Replace(Replace(ctl.Text, " ", ""), Chr(10), ""), Chr(13), "") = String.Empty Then
            Me.ErrorProvider1.SetError(ctl, "please enter a " & IsNull(ctl.Tag, ctl.Name) & " or 'Delete' if it is an unwanted entry")
            arCtls(i) = ctl
            i = i + 1
            ReDim Preserve arCtls(arCtls.GetUpperBound(0) + 1)
        Else
            Me.ErrorProvider1.SetError(ctl, "")
        End If

        'org Type
        ctl = Me.cboType
        If modGlobalVar.ValidateBoundDD(ctl, True, Me.ErrorProvider1, ObjClose.btnSaveExit) = usrInput.OK Then
        Else
            Me.ErrorProvider1.SetError(ctl, "please enter a " & IsNull(ctl.Tag, ctl.Name))
            arCtls(i) = ctl
            i = i + 1
            ReDim Preserve arCtls(arCtls.GetUpperBound(0) + 1)
        End If

        arCtls(i) = ctlNeutral
        Return arCtls

    End Function

    'VALIDATE COUNTRY - SET REGION/COUNTY
    Private Sub editCountry_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) ' Handles editCountry.Validating
        'is this ever called??
        GetCountry(sender.text)
        '--note: pre crg international:
        'Me.fldRegion.Text = "Outside USA"
        'Me.fldCounty.Text = "Out of State"
    End Sub

    'VALIDATE CBO TYPE
    Private Sub cboType_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) _
        Handles cboType.Validating

        If modGlobalVar.ValidateBoundDD(sender, True, Me.ErrorProvider1, ObjClose.CloseByControl) = usrInput.OK Then
            e.Cancel = False
        Else
            e.Cancel = True
            sender.DroppedDown = True
        End If
        ' e.Cancel = Not newValidateUnboundCBOString(sender, True, Me.ErrorProvider1, ObjClose.CloseByControl)
    End Sub

#End Region

#Region "ADD ITEM"

    'POPUP ADD NEW Case, Contact, Grant
    Private Sub PopupNew(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles miNew.Click, btnNew.Click
        Dim pp As New ContextMenu
        Dim eh As EventHandler = AddressOf AddNew

        'TODO ENHANCE set default as selected tab or contact if none selected
        pp.MenuItems.Add("SELECT ITEM TO ADD: ")
        pp.MenuItems.Add("-------------------------------------------------------")
        pp.MenuItems.Add("Case", eh)
        pp.MenuItems.Add("Contact", eh)
        pp.MenuItems.Add("Grant", eh)
        pp.MenuItems.Add("Story", eh)
        pp.MenuItems.Add("Alert", eh)
        pp.MenuItems.Add("Denomination", eh)

        '  pp.MenuItems(0).DefaultItem = True
        pp.Show(Me, New Point(200, 10))
    End Sub

    'INSERT NEW ITEM to BACKEND and OPEN DETAIL FORM
    Private Sub AddNew(ByVal obj As Object, ByVal ea As EventArgs)
        Dim str As String

        Dim ehg As EventHandler = AddressOf GetGrantType
        If obj.text = "Make this organization a Resource" Then
            Me.miMakeResource.PerformClick()
            Exit Sub
        End If
        SetStatusBarText("Adding new " + obj.text)
        If modGlobalVar.msg("About to enter a new " + obj.text, "Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then
            SetStatusBarText("Ready")
            Exit Sub
        End If
        MouseWait()

        Select Case obj.text
            Case Is = enumContact.SingleName 'strData1 'contact
                str = "INSERT INTO tblContact(FirstName,OrgNum, CreateStaffNum, CreateDate) VALUES (N'new person', " & ThisID & ", " & usr & ", GETDATE()); SELECT @@IDENTITY"
            Case Is = enumCase.SingleName 'strData2 'case
                'Dim strCase As String
                If modPopup.NewCase(ThisID, "New Case", usr, "Case", Me, Me.editOrgName.Text + ": " + Me.editPhone.Text, 0) Then
                End If
                GoTo exitnew
            Case Is = enumGrant.SingleName 'strData3 'grant
                Dim pp As New ContextMenu

                'TODO change this to table datasource
                'GET TYPE OF GRANT AND NEXT NUMBER
                pp.MenuItems.Add("Select Type of Grant")
                pp.MenuItems.Add("--------------------")

                Dim QryGrantType As New SqlCommand("SELECT GrantTypeName as GrantType FROM luGrantTokenTbl where GrantTypeClosed = 0 ORDER BY SortFld", sc)
                If Not SCConnect() Then
                    Exit Sub
                End If
                Dim dr As SqlDataReader = QryGrantType.ExecuteReader()
                While dr.Read
                    pp.MenuItems.Add(dr(0), ehg)
                End While
                sc.Close()
                dr = Nothing
                QryGrantType = Nothing

                pp.MenuItems(2).DefaultItem = True
                pp.Show(Me, New Point(200, 10))
                Exit Sub
            Case Is = "Story"
                str = "INSERT INTO tblOrgStory(OrgNum, CreateStaffNum, CreateDate) VALUES (" & ThisID & ", " & usr & ", GETDATE()); SELECT @@Identity"
            Case Is = "Alert"
                str = "INSERT INTO tblOrgAlert(OrgNum, StaffNum, CreateDate) VALUES (" & ThisID & "," & usr & ", GETDATE()); SELECT @@Identity"
            Case Is = "Denomination"
                GoTo OpenForm
            Case Else
                modGlobalVar.msg("ERROR: not found", obj.text, MessageBoxButtons.OK, MessageBoxIcon.Error)
                GoTo ExitNew
        End Select

InsertNewItem:
        If Not SCConnect() Then
            Exit Sub
        End If

        '   Dim myTrans As SqlClient.SqlTransaction = sc.BeginTransaction()
        Dim cmd As New SqlClient.SqlCommand(str, sc) ', myTrans)
        Dim newID As Integer
        Try
            newID = cmd.ExecuteScalar()
            '       myTrans.Commit()
        Catch exce As Exception
            modGlobalVar.msg("ERROR: insert", exce.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
            '      myTrans.Rollback()
            sc.Close()
            GoTo exitnew
        End Try

        sc.Close()
OpenForm:
        Select Case obj.text   'UCase(TabControl1.SelectedTab.Tag)
            Case Is = enumContact.SingleName 'contact
                modGlobalVar.OpenMainContact(newID, "Entering new contact", Me.editOrgName.Text & " : " & Me.editPhone.Text, ThisID)
                bNewContact = True
            Case Is = enumCase.SingleName 'case
                modGlobalVar.OpenMainCase(newID, "Entering New Case", Me.editOrgName.Text & " : " & Me.editPhone.Text, ThisID)
            Case Is = enumGrant.SingleName 'grant
                'see getgranttype
            Case Is = "Story"
                modGlobalVar.OpenMainStory(newID, Me.editOrgName.Text & " : " & Me.editPhone.Text, ThisID)
            Case Is = "Alert"
                modGlobalVar.OpenMainAlert(newID, Me.editOrgName.Text & " : " & Me.editPhone.Text, ThisID)
            Case Is = "Denomination"
                OpenDenomForm(True)
            Case Is = "Make this organization a Resource"
                modGlobalVar.OpenMainResource(newID, "Entering " & Me.editOrgName.Text & " as a Resource")
                Me.chkResource.CheckState = CheckState.Checked
        End Select
ExitNew:
        SetTabCaptions()
        MouseDefault()

    End Sub

    'EDIT DENOM MASTER
    Private Sub OpenDenomForm(ByRef FillGrid As Boolean)
        Dim frm As New frmAddNew
        modGlobalVar.HideTabPage("tbDenomination", frm.TabControl1)

        If FillGrid Then
            frm.FillDenomGrid()
        End If

        If Me.fldDenomination.Text > " " Then
            Try
                frm.BindingSourceDenomination.Position = frm.BindingSourceDenomination.Find("Denomination", Me.fldDenomination.Text)
            Catch ex As Exception
                modGlobalVar.msg("ERROR: ", "can't navigate to " & Me.fldDenomination.Text & "." & NextLine & ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        Else
            'MsgBox("denom not found", , fldDenomination.Text)
        End If
        frm.LoadDenomType()
        frm.ShowDialog()
    End Sub

    'EHG NEW GRANT POPUP HANDLER
    Private Sub GetGrantType(ByVal obj As Object, ByVal ea As EventArgs)
        modPopup.NewGrant(ThisID, obj.text, False, Me.editOrgName.Text, Me.editOrgName.Text & " : " & Me.editPhone.Text, 0, 0, "NULL", 0, 0, usr)
    End Sub

    'MAKE THIS ORGANIZATION A RESOURCE or GOTO  
    Private Sub btnResource_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles miMakeResource.Click, lblResource.DoubleClick
        'remove apostrophe
        Dim strName As String
        strName = Me.editOrgName.Text.Replace("'", "''")

        'OPEN EXISTING RESOURCE ENTRY
        If Me.chkResource.Checked = True Then
            SetStatusBarText("Opening Resource window")
            modGlobalVar.OpenResourceChoice(Me.lblResource.Tag, Me.editOrgName.Text)
            SetStatusBarText("Done")
            Exit Sub
        End If
        'CREATE NEW RESOURCE ENTRY
        SetStatusBarText("Making this congregation a Resource")
        If modGlobalVar.msg("Are you sure?", "About to enter a new Resource", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then
            Exit Sub
        End If
        MouseWait()
InsertNewRecord:
        Dim str As String = "INSERT INTO tblResource(ResourceName, ResourceType, Subtype, OrgNum, ReferralSource, CreateStaffNum) VALUES (N'" & strName & "', 'Organization', 'Congregation', " & ThisID & ", N'" & usrFirst & "', " & usr & "); SELECT @@Identity"
        If Not SCConnect() Then
            Exit Sub
        End If

        '   Dim myTrans As SqlClient.SqlTransaction = sc.BeginTransaction()
        Dim cmd As New SqlClient.SqlCommand(str, sc)
        Dim newID As Integer
        Try
            newID = cmd.ExecuteScalar()
        Catch ex As Exception
            modGlobalVar.msg("ERROR: creating resource", "execute scalar unsuccssful", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        Finally
            sc.Close()
        End Try

OpenForm:
        modGlobalVar.OpenMainResource(newID, "Entering new resource")
        SetStatusBarText("Done")
        MouseDefault()
    End Sub

#End Region    'add item

#Region "Fill Datasets"

    'FILL GRID
    Public Sub FillSecondary()
        Dim cmd As New SqlCommand(" ", sc)
        SetStatusBarText("Retrieving data...")
        MouseWait()

        SetTabCaptions()

        cmd.Parameters.Add("@IDVal", System.Data.SqlDbType.Int).Value = ThisID
        cmd.Parameters.Add("@IDFld", System.Data.SqlDbType.VarChar, 30).Value = "Org"
        cmd.CommandType = System.Data.CommandType.StoredProcedure

        Try
            tbl.Columns.Clear()
        Catch ex As System.Exception
            'modGlobalVar.Msg(ex.Message, , "ERROR: clearing dt  ")
        End Try
        Select Case TabControl1.SelectedTab.Tag
            Case Is = enumContact.PluralName
                cmd.CommandText = "[GetContacts]"
                tbl = New DataTable("GetContacts")
            Case Is = enumCase.PluralName
                cmd.CommandText = "[GetCases]"
                tbl = New DataTable("GetCases")
            Case Is = enumGrant.PluralName
                cmd.CommandText = "[GetGrants]"
                cmd.Parameters("@IDFld").Value = "OrgNum"
                tbl = New DataTable("GetGrants")
            Case Is = "STORIES"
                cmd.CommandText = "[GetOrgStories]"
                tbl = New DataTable("GetOrgStories")
            Case Is = "ALERTS"
                cmd.CommandText = "[GetOrgAlerts]"
                tbl = New DataTable("GetOrgAlerts")

            Case Else
                ' modGlobalVar.Msg(TabControl1.SelectedTab.Tag, , "not found")
                GoTo CloseAll
        End Select
        If Not SCConnect() Then
            GoTo CloseAll
        End If
        Try
            tbl.Load(cmd.ExecuteReader(CommandBehavior.CloseConnection))
        Catch ex As Exception
            modGlobalVar.msg("ERROR - can't fill " + TabControl1.SelectedTab.Tag, ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
            GoTo CloseAll
        End Try

        'ASSIGN DATAVIEW
        '  strbActiveGrid.Append(TabControl1.SelectedTab.Text)    'use this for doubleclick code
        dv = New DataView(tbl)
        Me.grdMain.DataSource = tbl
        ' grdMain.CaptionText = dt.Rows.Count.ToString & " " & TabControl1.SelectedTab.Tag

CloseAll:
        SetStatusBarText("Done")
        MouseDefault()
    End Sub 'fill secondary

    'GET TAB CAPTION COUNT
    Public Sub SetTabCaptions()

        Dim cmdCntID As New SqlCommand("", sc)
        Dim i As Integer = 0

        'RESET SELECTED ITEM INDICATOR
        modGlobalVar.ClearIDLbls(Me.lblSelectedID, Me.lblSelectedWhat)

        If Not SCConnect() Then
            Exit Sub
        End If

        'COUNT CONTACT
        cmdCntID.CommandText = "SELECT COUNT(ContactID) FROM vwgetvalidcontacts WHERE OrgNum = " & ThisID & " AND Active = 1"
        cntContact = 0
        Try
            cntContact = cmdCntID.ExecuteScalar()
        Catch ex As Exception
            '      modGlobalVar.Msg(ex.Message)
        End Try
        Me.TabControl1.TabPages("Pg" & enumContact.SingleName).Text = cntContact.ToString() & "  " & enumContact.PluralName

        'COUNT CASES
        cmdCntID.CommandText = "SELECT COUNT(CASEID) FROM vwgetvalidcases WHERE OrgNum = " & ThisID
        cntCase = 0
        Try
            cntCase = cmdCntID.ExecuteScalar()
        Catch ex As Exception
            '      modGlobalVar.Msg(ex.Message)
        End Try
        Me.TabControl1.TabPages("Pg" & enumCase.SingleName).Text = cntCase.ToString() & "  " & enumCase.PluralName

        'COUNT STORIES
        cmdCntID.CommandText = "SELECT COUNT(STORYID) FROM tblOrgStory WHERE OrgNum = " & ThisID
        i = 0
        Try
            i = cmdCntID.ExecuteScalar()
        Catch ex As Exception
            '      modGlobalVar.Msg(ex.Message)
        End Try
        Me.TabControl1.TabPages("PgStory").Text = i.ToString() & "  " & Me.TabControl1.TabPages("PgStory").Tag

        '-----------------------
        'COUNT ALERTS & SET WARNING FLAG
        cmdCntID.CommandText = "SELECT COUNT(AlertID) FROM tblOrgAlert WHERE OrgNum = " & ThisID
        i = 0
        Try
            i = cmdCntID.ExecuteScalar
        Catch ex As Exception
        End Try
        Me.TabControl1.TabPages("PgAlert").Text = i.ToString() & "  " & Me.TabControl1.TabPages("PgAlert").Tag
        cmdCntID.CommandText = "SELECT COUNT(AlertID) FROM tblOrgAlert WHERE (canceldate is null or canceldate = '1/1/1911') and OrgNum = " & ThisID
        i = 0
        Try
            i = cmdCntID.ExecuteScalar
        Catch ex As Exception
        End Try
        If i > 0 Then
            Me.flagWarning.Visible = True
            Me.TimerFlag.Start()
        Else
            Me.flagWarning.Visible = False
        End If

        '-------------------------------
        'GRANTS  'count grants & grantapps
        Dim r As SqlDataReader
        i = 0
        Dim myCommand As New SqlCommand("[GetGrants]", sc)
        myCommand.Parameters.Add("@IDFld", SqlDbType.VarChar).Value = "OrgNum"
        myCommand.Parameters.Add("@IDVal", SqlDbType.Int).Value = ThisID
        myCommand.CommandType = System.Data.CommandType.StoredProcedure
        Try
            r = myCommand.ExecuteReader
            While r.Read
                i += 1
                ' If IsDBNull(r("GrantLatest")) Then
                ' Else
                If r.GetString(r.GetOrdinal("CongregationFinalReport")).Contains("late") Then
                    Me.flgOutstanding.Visible = True
                End If
                ' End If
            End While
            r.Close()
        Catch ex As Exception
            modGlobalVar.msg("ERROR: grant count datareader", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Me.TabControl1.TabPages("Pg" & enumGrant.SingleName).Text = i.ToString & "  " & enumGrant.PluralName
        sc.Close()

    End Sub

    'SET VISIBLITY based on TAB USER SELECTED
    Private Sub TabControl1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
     Handles TabControl1.SelectedIndexChanged

        If isloaded Then    'SelectedIndexChanges occurs before load!!
            modGlobalVar.ClearIDLbls(Me.lblSelectedID, Me.lblSelectedWhat)
            Me.grdMain.CaptionText = ""
            Select Case TabControl1.SelectedTab.Tag
                Case Is = "PROFILE"
                    Me.grdMain.Visible = False
                    Me.pnlExtra.Visible = False
                    Me.lblExtra.Visible = False
                Case Is = enumContact.PluralName, enumCase.PluralName, enumGrant.PluralName, "STORIES", "ALERTS"
                    Me.grdMain.Visible = True
                    Me.pnlExtra.Visible = False
                    Me.lblExtra.Visible = False
                    FillSecondary()
                Case Is = "EVENTS"
                    Me.grdMain.Visible = True
                    Me.pnlExtra.Visible = False
                    Me.lblExtra.Visible = False
                    Me.grdMain.CaptionText = "Future Development - Will show list of event held at this location"
                Case Is = "EXTRAS"
                    Me.grdMain.Visible = False
                    Me.pnlExtra.Visible = True
                    Me.lblExtra.Visible = True
                    '   modGlobalVar.Msg("This page will contain a list of events held at this location, and a link to a document containing site-specific information", MessageBoxIcon.Information, "coming soon")
            End Select
        End If
    End Sub 'tab sel index changed

#End Region   'fill datasets

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

    'CAPTURE RIGHT MOUSE CLICK TO FILTER APPROPRIATE GRID
    Protected Sub grdAll_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) _
          Handles grdMain.MouseDown

        Dim strHdr As String    'text for grid header
        hti = sender.HitTest(e.X, e.Y)

IfRightMouseclick:
        If e.Button = System.Windows.Forms.MouseButtons.Right Then
            strHdr = Me.TabControl1.SelectedTab.Tag
SetOrClearFilter:
            If hti.Type = DataGrid.HitTestType.Cell Then    'SET FILTER
                If IsDBNull(sender.Item(hti.Row, hti.Column)) Then 'nulls cause filter error
                    modGlobalVar.msg(MsgCodes.filterEmpty)
                    Exit Sub
                Else
                    grdFilter(sender, strHdr)
                End If
            Else                                            'not in cell, CLEAR FILTER
                Me.grdMain.DataSource = tbl ' remove dv rowfilter
                Me.lblSelectedID.Text = ""

                Select Case TabControl1.SelectedTab.Tag
                    Case Is = enumContact.PluralName
                        sender.CaptionText = cntContact & "  " & strHdr
                    Case Is = enumCase.PluralName
                        sender.CaptionText = cntCase & "  " & strHdr
                    Case Else

                        sender.CaptionText = tbl.Rows.Count.ToString & "  " & strHdr 'DsOrgSearch1.tblOrg.Rows.Count.ToString
                End Select

                statusM = ""
                SetStatusBarText(statusM & " " & statusS1 & " " & statusS2)
            End If
IfLeftMouseClick:
        Else    'left mouse
            '   strbActiveGrid.Replace(strbActiveGrid.ToString, Me.TabControl1.SelectedTab.Tag)
        End If
    End Sub

    'FILTER METHOD
    Private Sub grdFilter(ByVal grd As Object, ByVal strHdr As String)
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

#Region "Open Forms"

    'OPEN CASE/CONTACT/GRANT FORMS
    Private Sub OpenForms()

        If Me.lblSelectedID.Text = String.Empty Then
            modGlobalVar.msg(MsgCodes.noRowSelected)
            Exit Sub
        End If

        Dim gotoOrgName As String = Me.editOrgName.Text & " : " & Me.editPhone.Text
        Dim cri As Integer = Me.grdMain.CurrentRowIndex
        MouseWait()
        SetStatusBarText("opening " & TabControl1.SelectedTab.Tag & "...")

        Select Case Me.TabControl1.SelectedTab.Tag '.lblSelectedWhat.Text.Replace(" ID:", String.Empty)
            Case Is = enumContact.PluralName
                Try
                    modGlobalVar.OpenMainContact(Me.lblSelectedID.Text, Me.grdMain.Item(cri, 1) & ", " & Me.grdMain.Item(cri, 2), gotoOrgName, ThisID)
                Catch ex As Exception
                    modGlobalVar.msg("ERROR:  opening contact from org", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
                'TODO remove hardcode grant types
            Case Is = enumCase.PluralName '"Cases"
                modGlobalVar.OpenMainCase(Me.lblSelectedID.Text, Me.grdMain.Item(cri, 1), gotoOrgName, ThisID)
            Case Is = enumGrant.PluralName ' "Grants"
                If Me.grdMain.Item(cri, 3).ToString.Contains("Application") Then
                    If Me.grdMain.Item(cri, 0).ToString.Contains("CMG ") Then
                        modGlobalVar.OpenMainCMG(Me.grdMain.Item(cri, 1), gotoOrgName, ThisID)
                    Else
                        If Me.grdMain.Item(cri, 0).ToString.Contains("YMGI") Then
                            modGlobalVar.OpenMainYMGI(Me.grdMain.Item(cri, 1), gotoOrgName, ThisID)
                        Else
                            If Me.grdMain.Item(cri, 0).ToString.Contains("LTGI") Then
                                modGlobalVar.OpenMainLTGI(Me.grdMain.Item(cri, 1), gotoOrgName, ThisID)
                            Else
                                If Me.grdMain.Item(cri, 0).ToString.Contains("TMGI") Then
                                    modGlobalVar.OpenMainTMGI(Me.grdMain.Item(cri, 1), gotoOrgName, ThisID)
                                Else
                                    If Me.grdMain.Item(cri, 0).ToString.Contains("TGI") Then
                                        modGlobalVar.OpenMainTGI(Me.grdMain.Item(cri, 1), gotoOrgName, ThisID)
                                    Else
                                        modGlobalVar.msg("ARCHIVED DATA", "see " & DBAdmin.StaffName & " for SSGI and CMGI details", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                    End If
                                End If
                            End If
                        End If
                    End If
                Else
                    '  modGlobalVar.Msg(Me.grdMain.Item(cri, 0), , "first column")
                    modGlobalVar.OpenMainGrant(Me.grdMain.Item(cri, 0), Me.editOrgName.Text, gotoOrgName, ThisID)
                End If
            Case Is = "STORIES"
                modGlobalVar.OpenMainStory(Me.lblSelectedID.Text, gotoOrgName, ThisID)
            Case Is = "ALERTS"
                modGlobalVar.OpenMainAlert(Me.lblSelectedID.Text, gotoOrgName, ThisID)
            Case Else
                ' modGlobalVar.Msg(Me.TabControl1.SelectedTab.Tag)
                'modGlobalVar.Msg(Me.lblSelectedWhat.Text.Replace(" ID:", String.Empty), , ThisID.ToString)
        End Select
        SetStatusBarText("Done")
        MouseDefault()
    End Sub

    'OPEN SECONDARY FORMS from MENU ITEMS
    Private Sub miGotoCase_Click(sender As Object, e As System.EventArgs) _
        Handles miGotoCase.Click, miGotoContact.Click, miGotoGrant.Click
        OpenForms()
    End Sub

    'OPEN RESOURCE DETAIL FORM
    Private Sub miGotoResource_Click(sender As Object, e As System.EventArgs) _
           Handles miGotoResource.Click
        modGlobalVar.OpenResourceChoice(Me.lblResource.Tag, "This congregation as a resource")
    End Sub

    'OPEN DENOMINATION EDIT FORM
    Private Sub lblDenomination_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) _
        Handles lblGotoDenomination.DoubleClick, lblDenomination.DoubleClick

        OpenDenomForm(True)
    End Sub

    'OPEN WEBSITE
    Private Sub editWebsite_Click(ByVal sender As Object, ByVal e As System.EventArgs) _
        Handles editWebsite.Click, editMapURL.Click

        modPopup.OpenWebsite(sender.text)
    End Sub

#End Region  'open forms

#Region "Address"

    'TODO REDO address section

    'create new physical address
    Private Sub btnNewPhysical_Click(sender As System.Object, e As System.EventArgs) Handles btnNewPhysical.Click
        Dim Str As String = "INSERT INTO tblAddress (EntityType, EntityNum, AddressType, City, State, Zip, Country, County, CreateDate, CreateStaffNum) " & _
            "VALUES ('Org', " & ThisID & ",'Physical', '" & Me.editCity.Text & "', '" & Me.editStateAB.Text & "', '" & Me.editZip.Text.Substring(0, 5) & "', '" & _
                Me.editCountry.Text & "', '" & Me.fldCounty.Text & "', GETDATE(), " & usr & "); SELECT @@IDENTITY"

        If Not SCConnect() Then
            Exit Sub
        End If

        Dim cmd As New SqlClient.SqlCommand(Str, sc) ', myTrans)
        Dim newID As Integer
        Try
            newID = cmd.ExecuteScalar()
        Catch exce As Exception
            modGlobalVar.msg("ERROR: insert address", exce.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            sc.Close()
        End Try

        Me.daOrg2Address.Fill(Me.DsMainOrgWAddress1.tblAddress1)
        sender.visible = False
        Me.editPhysicalStreet.Focus()
    End Sub

    'Leave State GET ZIP
    Private Sub editStateAb_Leave(ByVal sender As Object, ByVal e As System.EventArgs) _
            Handles editStateAB.Leave
        If isloaded = False Then
            Exit Sub
        End If
        Dim pp As New ContextMenu
        Dim eh As EventHandler = AddressOf userChooseZip
        If Me.editStateAB.Text = String.Empty Then
            Exit Sub
        End If

FormatState:
        Me.editStateAB.Text = UCase(Me.editStateAB.Text)

        If Me.editStateAB.Text.StartsWith("IN") Then 'And Me.editCity.Text > "" Then
        Else    'not indiana
            Me.fldCounty.Text = "Out of State"
            Me.fldRegion.Text = "Outside Indiana"
            Exit Sub
        End If

C_GETZIP:  'if zip blank
        If Me.editZip.Text = String.Empty Then

            Dim sqlZip As New SqlCommand("SELECT COUNT(Zip) As NumZips FROM luCountyZip WHERE City = '" + Me.editCity.Text + "' GROUP BY City ; Select Distinct Zip FROM luCountyZip WHERE City = '" + Me.editCity.Text + "' ORDER BY Zip ", sc)
            Dim rdr As SqlDataReader
            If Not SCConnect() Then
                Exit Sub
            End If

            Try
                rdr = sqlZip.ExecuteReader(CommandBehavior.CloseConnection)
            Catch ex As Exception
                modGlobalVar.msg("ERROR: Query get Zips", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
            If rdr.HasRows Then
                rdr.Read()
                Select Case rdr.GetValue(0)
                    Case Is = 1    'match
                        rdr.NextResult()
                        rdr.Read()
                        Me.editZip.Text = rdr.GetString(0)
                    Case Is > 1    'popup menu
                        pp.MenuItems.Add("Multiple Zips for this City - select one")
                        pp.MenuItems.Add("----------------------------------------")
                        rdr.NextResult()
                        While rdr.Read
                            pp.MenuItems.Add(rdr.GetString(0), eh)
                        End While
                End Select
            Else 'not found
            End If
            rdr.Close()
            sc.Close()
            sqlZip = Nothing
            pp.Show(Me, New Point(200, 10))
            pp = Nothing
        Else
        End If

    End Sub

    'Leave ZIP FORMAT, GET CITY
    Private Sub editZip_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles editZip.Leave

        Dim strC, strR As String
        strC = Me.fldCounty.Text
        strR = Me.fldRegion.Text
        Dim strbConcat As New StringBuilder
        Dim pp As New ContextMenu
        Dim eh As EventHandler = AddressOf userChooseCity
        Dim n As Integer

B_ZipFormat:
        modGlobalVar.RemoveLineFeeds(sender)
        If Me.editZip.Text = String.Empty Then
            Exit Sub
        Else
            Dim rtrn As String = modGlobalVar.FormatZip(sender, e, Me.editStateAB.Text)

            Select Case rtrn
                Case usrInput.Ignore
                    Exit Sub 'out of state
                Case usrInput.OK    'proceed
                Case usrInput.Retry
                    sender.Focus()
                    Exit Sub
                Case Else
                    sender.Text = rtrn
            End Select
            rtrn = Nothing
        End If

D_GetCity:  'if empty

        If Me.editCity.Text = String.Empty Then

            Dim sqlCity As New SqlCommand("SELECT COUNT(City) As NumCities FROM luCountyZip WHERE Zip = " & Me.editZip.Text.Substring(0, 5) & " GROUP BY Zip ; Select Distinct City FROM luCountyZip WHERE Zip = " & Me.editZip.Text.Substring(0, 5) & " ORDER BY City", sc)
            Dim rdr As SqlDataReader
            If Not SCConnect() Then
                Exit Sub
            End If

            Try
                rdr = sqlCity.ExecuteReader()
            Catch ex As Exception
                modGlobalVar.msg("ERROR: Query get city", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
            If rdr.HasRows Then
                rdr.Read()
                n = rdr.GetValue(0) '?why does this fail on getInt32?
                rdr.NextResult()
                rdr.Read()

                Select Case n
                    Case Is = 1    'match
                        '  modGlobalVar.Msg(rdr.GetValue(0).ToString, , "city")
                        Me.editCity.Text = rdr.GetString(0)
                    Case Is > 1    'popup menu
                        pp.MenuItems.Add("Multiple Cities at this Zip - select one")
                        pp.MenuItems.Add("----------------------------------------")
                        pp.MenuItems.Add(rdr.GetString(0), eh)
                        While rdr.Read
                            pp.MenuItems.Add(rdr.GetString(0), eh)
                        End While
                End Select
            Else 'not found
                modGlobalVar.msg("ERROR: zip code", "Please check zipcode", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
            rdr.Close()
            sc.Close()
            sqlCity = Nothing
            pp.Show(Me, New Point(200, 10))
            pp = Nothing
        Else    'already is a city

        End If
    End Sub   'leave zip

    'Multiple Cities
    Private Sub userChooseCity(ByVal obj As Object, ByVal ea As EventArgs)
        Me.editCity.Text = obj.text
        ' bChanged = True
    End Sub

    'Leave Address Panel: set Physical address, county, region, url
    Private Sub pnlAddress_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) _
    Handles pnlAddress.Leave

        If UCase(IsNull(Me.editPhysicalCountry.Text, Me.editCountry.Text)) <> "USA" Then
            GetCountry(IsNull(Me.editPhysicalCountry.Text, Me.editCountry.Text))
            Exit Sub
        End If
        If IsNull(Me.editPhysicalStreet.Text, Me.editStreet1.Text) = String.Empty And IsNull(Me.editPhysicalZip.Text, Me.editZip.Text) = String.Empty Then
            Exit Sub
        End If

E_CompareCityZip:  'VERIFY City=Zip  
        CompareCityZip(Me.editZip)
        'changed 12/16 - only include physical if is different from Mailing Address

G_GetCountyRegion:
        GetCountyRegion("Mailing")

H_GetURL:
        GetWebURL()

    End Sub 'leave pnladdress

    'Compare City/Zip
    Private Sub CompareCityZip(ByRef fld As TextBox)
        Dim z As String
        Dim sqlGetZip As New SqlCommand
        Dim rdr As SqlDataReader

        If Me.editCity.Text = String.Empty Or Me.editZip.Text = String.Empty Then
            Exit Sub
        End If

        sqlGetZip.Connection = sc

        z = Me.editZip.Text.Substring(0, 5)

        If Not SCConnect() Then
            Exit Sub
        End If
        If z = String.Empty Then
        Else
            sqlGetZip.CommandText = "SELECT  Zip FROM luCountyZip WHERE zip = " & z & " AND (City = '" & Me.editCity.Text & "')"
            If sqlGetZip.ExecuteScalar = z Then
                GoTo closeall
            End If
        End If
        '     Dim sqlCntCity As New SqlCommand("SELECT COUNT(City) FROM luCountyZip GROUP BY City HAVING (City = '" & Me.editCity.Text & "')", sc)
        sqlGetZip.CommandText = "SELECT Count (distinct(Zip)) FROM  luCountyZip WHERE zip = " & z & " OR City = '" & Me.editCity.Text & "'; SELECT City, Zip, County, SatelliteRegion FROM luCountyZip WHERE zip = " & z & " OR (City = '" & Me.editCity.Text & "')"

C_Getzips:
        Dim strbConcat As New StringBuilder
        strbConcat.Append(" ")
        Dim pp As New ContextMenu
        Dim eh As EventHandler = AddressOf SetZip
        pp.MenuItems.Add("Matching Zip and City")
        pp.MenuItems.Add("-------------------------------------")

        If UCase(Me.editStateAB.Text) = "IN" Then
            'zip or null
            Try
                rdr = sqlGetZip.ExecuteReader()
            Catch ex As Exception
                modGlobalVar.msg("ERROR: matching city/zip", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try

            If rdr.HasRows Then
                rdr.Read()
                Dim x As Integer = rdr.GetInt32(0)
                rdr.NextResult()
                Select Case x
                    Case Is = 0 'none found
                        GoTo CloseAll
                    Case Is > 1 'let user choose from popupmenu
                        While rdr.Read
                            strbConcat.Remove(0, Len(strbConcat.ToString))
                            If z = rdr.GetString(1) And Me.editCity.Text = rdr.GetString(0) Then    'no change to zip or city
                                GoTo CloseAll
                            End If
                            For y As Integer = 0 To 3   'multiple matches
                                If IsDBNull(rdr(y)) Then
                                Else
                                    strbConcat.Append(rdr.GetString(y) & "-")
                                End If
                            Next y
                            pp.MenuItems.Add(strbConcat.ToString, eh)
                        End While
                        rdr.Close()
                        sc.Close()
                        GoTo RunPopup

                    Case Is = 1 'confirm change zip
                        rdr.Read()
                        If Me.editZip.Text > "" Then
                            If Me.editZip.Text.Substring(0, 5) <> rdr.GetString(1) Then
                                If modGlobalVar.msg("CONFIRM CHANGE", "Zip does not match USPS suggestions" & NextLine & "To change from " & Me.editZip.Text & " to " & rdr.GetString(1) & " click Yes. ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                                    Me.editZip.Text = rdr.GetString(1)
                                    '  bChanged = True
                                End If
                            End If
                        Else
                            Me.editZip.Text = rdr.GetString(1)
                            '  bChanged = True
                        End If
                        GoTo Closeall
                End Select
                End
RunPopup:       'show popup whether full or populated from query
                pp.MenuItems(0).DefaultItem = True
                pp.Show(Me, New Point(200, 10))
                pp = Nothing

            Else 'rdr has no rows
                Exit Sub
            End If
        Else    'not indiana
            Exit Sub

        End If 'not indiana
CloseAll:
        Try
            rdr.Close()
            sc.Close()

            sqlGetZip = Nothing
        Catch ex As Exception

        End Try
        ' End If 'not indiana

    End Sub  'compare city/zip 

    'leave Physical Zip, set county, region, url
    Private Sub editPhysicalZip_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles editPhysicalZip.Leave

B_ZipFormat:
        modGlobalVar.RemoveLineFeeds(sender)
        If sender.Text = String.Empty Then
            Exit Sub
        Else
            Dim rtrn As String = modGlobalVar.FormatZip(sender, e, Me.editStateAB.Text)
            Select Case rtrn
                Case usrInput.Ignore
                    Exit Sub 'out of state
                Case usrInput.OK    'proceed
                Case usrInput.Retry
                    sender.Focus()
                    Exit Sub
                Case Else
                    sender.Text = rtrn
            End Select
            rtrn = Nothing
        End If

G_GetCountyRegion:
        GetCountyRegion("Physical")

H_GetURL:
        GetWebURL()

    End Sub     'leave FldPhysicalZip

    'Multiple Zips
    Private Sub userChooseZip(ByVal obj As Object, ByVal ByValea As EventArgs)
        Me.editZip.Text = obj.text
        '  bChanged = True
    End Sub

    'SET Zip
    Private Sub SetZip(ByVal obj As Object, ByVal ea As EventArgs)
        ' Dim i As Integer
        Dim arMenu As String() = Split(obj.text, "-")
        '  i = InStr(obj.text, " -")
        Try
            Me.editCity.Text = arMenu(0)
            Me.editZip.Text = arMenu(1)
            '  Me.fldCounty.Text = arMenu(2)
            ' Me.fldRegion.Text = arMenu(3)
            '  obj.text.substring(0, i - 1)
            '  bChanged = True

        Catch ex As Exception
            modGlobalVar.msg("ERROR: couldn't get county/zip", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

    End Sub

    'GET COUNTY, REGION - based whether called by leaving address or by dlb-clicking Physical address; {changes getting confused w older data in other field}
    Private Sub GetCountyRegion(ByVal what As String) 'mailing or physical
        'TODO -- make this on  change, not every time leave field/panel ===============
        Dim strbConcat As New StringBuilder
        Dim strZip, strState, strCountry As String
        strState = IsNull(Me.editPhysicalState.Text, Me.editStateAB.Text)
        strCountry = IsNull(Me.editPhysicalCountry.Text, Me.editCountry.Text)
        strZip = IsNull(Me.editPhysicalZip.Text, Me.editZip.Text).Substring(0, 5)
International:
        If UCase(strCountry) <> "USA" Then
            GetCountry(strCountry)
            Exit Sub
        End If

NotIndiana:  'not stae only captured by mailing address
        If UCase(strState).StartsWith("IN") Then     'nd Me.editStateAb.Text > "") Then 'another state, skip this routine
            GoTo Indiana
        Else
            If IsNull(Me.fldCounty.Text, "") = "out of state" Then
            Else
                Me.fldCounty.Text = "out of state"
                ' bChanged = True
            End If
            If fldRegion.Text = "Outside Indiana" Then
            Else
                Me.fldRegion.Text = "Outside Indiana"   'formerly Not in Region; not used since went state-wide
                ' bChanged = True
            End If
            Exit Sub
        End If

Indiana:  'base county and map url on Physical not Mailing address
        ''Dim strZip As String    'get physical zip if is one, else use mailing zip
        'zipMail = Me.editZip.Text.ToString.Substring(0, 5)
        'zipPhysical = Me.fldPhysicalZip.Text.ToString.Substring(0, 5)
        ''If zipMail = zipPhysical Then 'ok to proceed
        ''    strZip = zipMail
        '' Else
        ''    'If what = "Physical" Then
        ''    ' strZip = zipMail
        ''    ' Else  'ziphysical may have old data; ask user to select one
        ''    If modGlobalVar.Msg("ATTENTION: Zip code discrepancy", "To assign County and Region, choose which address to use." & NextLine &
        ''               "Mailing Address: " & zipMail.ToString & NextLine & "Physical: " & zipPhysical.ToString & NextLine & "click Yes for Mailing address.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
        ''        strZip = zipMail
        ''    Else
        ''        strZip = zipPhysical
        ''    End If
        ''    'End If
        ''End If
        'If what = "physical" Then
        '    strZip = Me.editPhysicalZip.Text.ToString.Substring(0, 5)
        'Else
        '    strZip = Me.editZip.Text.ToString.Substring(0, 5)
        'End If


        Dim pp As New ContextMenu
        Dim eh As EventHandler = AddressOf UserChooseRegion
        pp.MenuItems.Add("County undetermined - select from list")
        pp.MenuItems.Add("-------------------------------------")
        'get region, county
        Dim sqlCounty As New SqlCommand("Select Distinct County, SatelliteRegion, dense_rank() over (order by  county desc) as RowCnt FROM luCountyZip WHERE  (Zip = " & strZip & ")  ORDER BY County", sc)
        strZip = Nothing
        Dim rdr As SqlDataReader
        If Not SCConnect() Then
            Exit Sub
        End If

        Try
            rdr = sqlCounty.ExecuteReader()
        Catch ex As Exception
            modGlobalVar.msg("ERROR: County Query not run", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
            GoTo FullList
        End Try

        If rdr.HasRows Then
            Dim colCounty As Integer = rdr.GetOrdinal("County")
            Dim colRegion As Integer = rdr.GetOrdinal("SatelliteRegion")
            rdr.Read()

            For x As Integer = 0 To 1
                If IsDBNull(rdr(x)) Then
                Else
                    strbConcat.Append(rdr.GetString(x) & "-")
                End If
            Next x
            pp.MenuItems.Add(strbConcat.ToString, eh)

            Select Case rdr.GetValue(rdr.GetOrdinal("RowCnt")) 'GetInt32(2) - why does this fail on datatype?
                Case Is = 1 'single answer
                    If IsDBNull(colCounty) Then    'county
                    Else
                        If Me.fldCounty.Text = rdr.GetString(colCounty) Then
                        Else
                            Me.fldCounty.Text = rdr.GetString(colCounty)
                            '  bChanged = True
                        End If
                    End If
                    If IsDBNull(colRegion) Then    'region
                    Else
                        If Me.fldRegion.Text = rdr.GetString(colRegion) Then
                        Else
                            Me.fldRegion.Text = rdr.GetString(colRegion)
                            ' bChanged = True
                        End If
                    End If
                    rdr.Close()
                    sc.Close()
                    Exit Sub

                Case Is > 1
                    While rdr.Read
                        strbConcat.Remove(0, Len(strbConcat.ToString))
                        For x As Integer = 0 To 1
                            If IsDBNull(rdr(x)) Then
                            Else
                                strbConcat.Append(rdr.GetString(x) & "-")
                            End If
                        Next x
                        pp.MenuItems.Add(strbConcat.ToString, eh)
                    End While
                    rdr.Close()
                    sc.Close()
                    GoTo RunPopup
                Case Else    '??

                    If IsDBNull(rdr(colCounty)) Then    'county
                    Else
                        If Me.fldCounty.Text = rdr.GetString(colCounty) Then
                        Else
                            Me.fldCounty.Text = rdr.GetString(colCounty)
                            'bChanged = True
                        End If
                    End If
                    If IsDBNull(rdr(colRegion)) Then    'region
                    Else
                        If Me.fldRegion.Text = rdr.GetString(colRegion) Then
                        Else
                            Me.fldRegion.Text = rdr.GetString(colRegion)
                            ' bChanged = True
                        End If
                    End If
                    rdr.Close()
                    sc.Close()
                    Exit Sub
            End Select

        Else    'zip not found in datatable
            If modGlobalVar.msg("ERROR: Zip not found", strZip & NextLine & "is this a valid Indiana zipcode?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                rdr.Close()
                sc.Close()
                GoTo FullList
            Else
                rdr.Close()
                sc.Close()
                Exit Sub
            End If
        End If

FullList:  'TODO load list w all counties
        For i As Integer = 1 To colRegion5lu.Count
            pp.MenuItems.Add(" -" + colRegion5lu(i), eh) '
        Next i
RunPopup:  'show popup whether full or populated from query
        pp.MenuItems(0).DefaultItem = True
        pp.Show(Me, New Point(200, 10))
        pp = Nothing

    End Sub 'get county/Region

    'MULTIPLE REGIONS
    Private Sub UserChooseRegion(ByVal obj As Object, ByVal ea As EventArgs)

        Dim arMenu As String() = Split(obj.text, "-")

        Try
            Me.fldCounty.Text = arMenu(0)
            Me.fldRegion.Text = arMenu(1)
        Catch ex As Exception
            modGlobalVar.msg("couldn't get county/zip", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

    End Sub

    'get country
    Private Sub GetCountry(ByVal usrCountry As String)
        If UCase(usrCountry) = "USA" Or usrCountry = "U.S." Or usrCountry = String.Empty Then
        Else
            SetStatusBarText("getting region")
            Dim sql As New SqlCommand("SELECT  TOP (1) Continent FROM luStateAbbrev WHERE Country = '" & usrCountry & "'", sc)
            If Not SCConnect() Then
                Exit Sub
            End If
            Dim reslt As String = sql.ExecuteScalar
            If reslt = String.Empty Then

                '  sb.Append(usrName & NextLine & "INSERT luStateAbbrev: Country, Continent." & NextLine & "EDIT tblOrg: SatRegion = Continent, State usually NULL." & NextLine & " OrgID: " & ThisID.ToString)
                'email cg
                Dim SendEmail As New ClassEmail
                SendEmail.SendHTMLEmail(DBAdmin.StaffEmail, "unknown country: " & usrCountry,
                "Entered by: " & usrFirst & " <br> <br> #1. INSERT luStateAbbrev: Country, Continent. <br> #2. EDIT tblOrg: SatRegion = Continent, State usually NULL. <br><br>  OrgID: " & ThisID.ToString) '"App: InfoCtr&#xaConnection String:\n" & sc.ConnectionString.ToString & "&#x0aComputer Name: " & IsNull(Environ("ComputerName"), "na") & "%OD%OAError: " & exc.Message)
                SendEmail = Nothing
                Me.fldRegion.Text = String.Empty
                Me.fldCounty.Text = String.Empty
            Else
                Me.fldRegion.Text = reslt
                Me.fldCounty.Text = String.Empty
            End If

            sc.Close()
            SetStatusBarText("done")

        End If
    End Sub

    'GET MAP URL - 12/16 changed to Indiana only
    Private Sub GetWebURL()
        Dim strAddress As String
        If Me.editStateAB.Text.StartsWith("IN") Then
        Else
            Exit Sub
        End If
        'PHYSICAL ADDRESS FIELD even with no address, city state zip works for url of center of town
GetAddressString:
        If Me.editPhysicalZip.Text = String.Empty Then
            strAddress = Me.editStreet1.Text + "," + Me.editCity.Text + "," + Me.editStateAB.Text + "," + Me.editZip.Text
        Else
            strAddress = Replace(Me.fldPhysicalAddress.Text, ", ", ",") + "," + Me.editPhysicalZip.Text
        End If

        'ADD MAP URL
WebAddress:
        If Me.editMapURL.Text = "http://maps.google.com/maps?q=" + Replace(strAddress, " ", "%20") Then
        Else
            Me.editMapURL.Text = "http://maps.google.com/maps?q=" + Replace(strAddress, " ", "%20") 'Replace(Me.editStreet1.Text + "," + Me.editCity.Text + "," + Me.editStateAb.Text + "," + Me.editZip.Text, " ", "%20")
            'bChanged = True
        End If

    End Sub 'Get map URL

#End Region 'zip, county, region

#Region "General"

    'SET STATUS BAR LEFT TEXT
    Private Sub SetStatusBarText(ByVal str As String)
        Me.StatusBarPanel1.Text = str
    End Sub

    'COPY ID
    Private Sub StatusBar1_DoubleClick(sender As System.Object, e As System.EventArgs) Handles StatusBar1.DoubleClick
        Clipboard.SetText(ThisID)
    End Sub

    'UNDO
    Protected Overrides Function ProcessDialogKey(ByVal keyData As Keys) As Boolean
        '  MsgBox(keyData.ToString, , "process dialog key")
        If keyData = System.Windows.Forms.Keys.Escape Then
            modPopup.UndoCtl(Me.ActiveControl)
            Return True  ' True means we've processed the key
            'Else
        End If
        Return MyBase.ProcessDialogKey(keyData)
        ' End If
    End Function

    'RIGHT CLICK MENU
    Private Sub textbox_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) _
        Handles editNotes.MouseDown, editOrgName.MouseDown, editPrograms.MouseDown

        If e.Button = System.Windows.Forms.MouseButtons.Right Then
            Dim pp As New ClassRTBContextMenu(sender)
            pp.Show(Me, PointToClient(Control.MousePosition))
        End If
    End Sub

    'TOGGLE FLAG BLINKING
    Private Sub TimerFlag_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles TimerFlag.Tick

        If t = 6 Then
            Me.TimerFlag.Stop()
            Exit Sub
        End If
        Me.flagWarning.Visible = Not Me.flagWarning.Visible
        t = t + 1
    End Sub

    'STOP BLINKING
    Private Sub flagWarning_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles flagWarning.Click

        Me.TimerFlag.Stop()
    End Sub

    'URL - MOUSE POINTER
    Private Sub editWebsite_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) _
        Handles editWebsite.MouseEnter

        MousePointer()
    End Sub

    'URL - MOUSE DEFAULT
    Private Sub editWebsite_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) _
        Handles editWebsite.MouseLeave

        MouseDefault()
    End Sub

    'DISPLAY STAFF NAME
    Private Sub TextBox5_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) _
        Handles fldCreateStaff.MouseHover, fldLastChangeStaff.MouseHover, fldReviewedBy.MouseHover
        Me.ToolTip1.SetToolTip(sender, sender.tag & ": " & modPopup.ShowStaff(sender.text))
    End Sub

#End Region 'general

#Region "Format Phone, City, EIN"

    'VALIDATE SPELLING INDY, FT WAYNE
    Private Sub editCity_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles editCity.Leave
A_FormatCity:
        Me.editCity.Text = modGlobalVar.CheckCity(Me.editCity, Me.editCity.Text)

    End Sub

    'VERIFY PHONE NUMBER FORMATTED OK     'note - this will run even if no change - 
    Private Sub editPhone_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles editPhone.Leave, editFax.Leave
        If Len(sender.text) > 0 Then
            If modGlobalVar.LeavePhone(sender, IsNull(Me.editCountry.Text, "USA")) = False Then
                Me.ErrorProvider1.SetError(sender, "enter valid phone number")
                sender.focus()
            Else
                Me.ErrorProvider1.SetError(sender, "")
            End If
        End If
    End Sub

    'FORMAT EIN FIELD
    Private Sub txtEIN_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtEIN.Leave
        '2 num hypen 7 num
        Select Case Len(sender.Text)
            Case 0
            Case 9 And IsNumeric(sender.text)
                sender.text = Mid(sender.text, 1, 2) + "-" + Mid(sender.text, 3)
            Case 10 And Mid(sender.text, 3, 1) = "-" And IsNumeric(Replace(sender.text, "-", ""))
            Case Else
                modGlobalVar.msg("EIN invalid", "please enter 9 numbers, with or without hypen in 3rd position" & NextLine & txtEIN.Text.ToString, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtEIN.Focus()
        End Select
    End Sub


    'REMOVE LINE ENDINGS from critical merge fields
    Private Sub LineFeeds_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) _
        Handles editStreet1.Validating, editCity.Validating, editZip.Validating, editWebsite.Validating, editOrgName.Validating
        modGlobalVar.RemoveLineFeeds(sender)
    End Sub



    'VALIDATE EMAIL and REMOVE LINE FEEDS
    Private Sub editEmail_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) _
Handles editEmail.Validating

        If sender.text = String.Empty Then
            Exit Sub
        End If

        modGlobalVar.RemoveLineFeeds(sender)
        e.Cancel = Not (modGlobalVar.ValidateEmail(sender, Me.ErrorProvider1))

        'modGlobalVar.ValidateEmail(sender)
    End Sub


#End Region 'format

#Region "Denomination"

    'MSG USER INFO when attempting to type in protected denomination dd
    Private Sub editDenomination_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) _
        Handles fldDenomination.KeyPress
        '     MsgBox(e.KeyChar.ToString) 'didn't trigger on escape
        ' If e.KeyChar = escape Then
        modGlobalVar.msg("Invalid entry", "Right-click to find existing Denominations; Use the New button to add a missing denomination", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

    End Sub

    'FIND DENOMINATION - CALL POPUP
    Private Sub editdenom_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) _
        Handles fldDenomination.MouseDown

        If e.Button = System.Windows.Forms.MouseButtons.Right Then
            SearchDenom(Me, PointToClient(Control.MousePosition), Me.fldDenomination)
        Else
            ' modGlobalVar.Msg("Right click for list of denominations; double-click to enter a new one", modGlobalVar.MsgStyle.DefaultButton1 Or MessageBoxIcon.Information, "Select a denomination")
        End If
    End Sub

    'POPUP FILTERED DENOMINATION
    Public Sub SearchDenom(ByRef frm As Form, ByVal p As System.Drawing.Point, ByRef ctl As Control)
        Dim dr As SqlDataReader
        Dim pp As ContextMenu = New ContextMenu 'crg filter
        Dim i As Integer = 0

        Dim myValue As Object
        Dim eh As EventHandler = AddressOf SelectDenom
        'GET USER INPUT; remove sinqle quote
        myValue = Replace(InputBox("type denomination search string like bapt or presb", "Search for Denomination"), "'", "%")
        If CType(myValue, String) > "" Then '0-length string returned on cancel
        Else
            Exit Sub
        End If

        'LOAD POPUP MENU USING QUERY
        pp.MenuItems.Clear()
        Dim cmd As New SqlCommand("SELECT distinct   Denomination from luDenomType WHERE GeneralType not like 'z%' AND (generalType like '%" & myValue & "%' or CommonName LIKE '%" & myValue & "%' or Denomination LIKE '%" & myValue & "%') ORDER BY Denomination", sc)
        If Not SCConnect() Then
            Exit Sub
        End If
        Try
            dr = cmd.ExecuteReader()
        Catch ex As Exception
            modGlobalVar.msg("ERROR: reading exception", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Dim c As Integer = 0
        Dim x As Integer = maintbl.Columns("Denomination").Ordinal

        If dr.HasRows Then
            'LOAD MENU
            While dr.Read()
                c = c + 1
                pp.MenuItems.Add(dr.GetString(0), eh)
            End While
            'DISPLAY CONTEXT MENU
            If c = 1 Then
                maintbl.Rows(0)("Denomination") = pp.MenuItems(0).Text
                Me.fldDenomination.Text = pp.MenuItems(0).Text
                Me.editAttendance.Focus()
            Else
                pp.Show(frm, p)
            End If
        Else
            ' modGlobalVar.Msg(myValue, , "no results found for")
        End If
        dr.Close()
        sc.Close()
        ''............................
    End Sub

    'PROCESS POPUP DENOMINATION
    Private Sub SelectDenom(ByVal obj As Object, ByVal ea As EventArgs)

        Me.fldDenomination.Text = obj.text
        maintbl.Rows(0)("Denomination") = obj.text
        bDirty = True


    End Sub

#End Region     'denomination

#Region "Reports"

    'OPEN HOST of EVENTS OVERVIEW REPORT
    Private Sub miHostOverviewRpt_Click(sender As System.Object, e As System.EventArgs) _
        Handles miHostOverview.Click

        Dim strFileName As String = LinkedPath & "Organizations\Host " & ThisID & ".xlsm"
        If sender.text = "Host Overview - Create" Then
            If modGlobalVar.msg("CONFIRM Create Host Overview", "Click OK to create a spreadsheet" & NextLine & "to record features of this organization related to hosting an event.", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) = Windows.Forms.DialogResult.OK Then
                If CreateSpreadsheet(SharedPath & "Staff Forms\EventHostTemplate.xltm", strFileName, Me.editOrgName.Text, Me.editPhone.Text) = True Then
                    FullHostName = strFileName
                    Me.miHostOverview.Text = "Host Overview - Open"
                    GoTo OpenFile
                Else
                    GoTo Cleanup
                End If
            Else
                GoTo Cleanup
            End If
        End If
OpenFile:
        Try
            'do this way so can check if is open or not
            If OpenFile(FullHostName) = True Then
                SetStatusBarText("file opened")
            End If
        Catch ex As Exception
            modGlobalVar.msg("ERROR: opening file", FullHostName & NextLine & ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
Cleanup:
        strFileName = Nothing
        FullHostName = Nothing
    End Sub

    'OPEN Staff/Laity REPORT 
    Private Sub miStaffRpt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
     Handles miStaffRpt.Click
        MouseWait()
        Try
            modPopup.StaffLaityRpt(ThisID)
        Catch ex As Exception
            modGlobalVar.msg("ERROR: with StaffLaity report ", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            MouseDefault()
        End Try
    End Sub

#End Region 'reports

#Region "DataReview"

    'POPUP DATA VERIFIED
    Private Sub btnReview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnReview.Click

        Dim pp As New ContextMenu
        Dim eh As EventHandler = AddressOf Verified

        pp.MenuItems.Add("Verified", eh)
        pp.MenuItems.Add("Unable to verify", eh)
        pp.Show(Me, PointToClient(Control.MousePosition))
    End Sub

    'PROCESS POPUP VERIFIED
    Private Sub Verified(ByVal obj As Object, ByVal ea As EventArgs)

        Me.fldReviewed.Text = obj.text
        Me.fldReviewDate.Text = Now()
        Me.fldReviewedBy.Text = usr
        'set underlying datasource or does not trigger save
        mainBSrce.Current("ReviewDate") = Now()
        mainBSrce.Current("ReviewStaffNum") = usr

        '  bChanged = True
    End Sub

#End Region 'data review

 
End Class
