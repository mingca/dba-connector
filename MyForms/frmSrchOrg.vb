Option Explicit On

Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Text    'stringbuilder
Imports System.IO   'getregion streamwriter'
Imports System.Threading    'sleep


Public Class frmSrchOrg
    Inherits System.Windows.Forms.Form
    'datagrid variables...
    Dim hti As System.Windows.Forms.DataGrid.HitTestInfo
    Dim dv, dvM, dvS, dvM2, dvS1, dvS2, dvS3, dvS4 As DataView   'filter for each datagrid
    Dim statusM, statusS1, statusS2, statusS3 As String 'filter text for status bar
    Dim ClickTime As DateTime
    Dim OrgCaption, ContactCaption As String    'for grid heading
    Dim gridMouseDownTime As DateTime   'to catch doubleclick within datagrid
    Dim strbActiveGrid As New StringBuilder 'for doubleclick any datagrid
    Dim iMainID As Integer 'selected ID column of main grid
    Dim iSelected As Integer 'current row so keep place in grid when change tab
    Dim itbl, iOrgtbl, iOrgtblStyle, iCasetbl, iCasetblStyle As Integer 'additional tables in dataset to hide grant columns depending on Region
    Dim tblRegistrations, tblConversations As DataTable
    Dim iRow As Integer = 0 'enforce single row selection
    Dim strCurTableStyle
    Dim tblSecGrid As DataTable
    Dim isLoaded As Boolean = False
    Dim isSearched As Boolean = False
    Dim usrSel, usrS As String
    Dim strbFormHdr As New StringBuilder 'for header on opening main form
    Dim usrRadio As New StringBuilder 'text from radio button
    Dim bFoundMainOrg As Boolean = False 'if no main items found, don't load secondary grids
    Dim bFoundMainContact As Boolean = False 'if neither org or contact is found, don't = icol
    Dim bFoundSec As Boolean = False 'for menu item enabling
    Dim WhatRField, WhatField As String
    Dim iCntCase As Integer 'count distinct cases
    Dim strMainDS As String = "Org"

    Const strDGM1 As String = "ORGANIZATIONS" 'header text on datagrids
    Const strDGM2 As String = "CONTACTS"
    Const strDGSContact As String = "CONTACTS"
    Const strDGSCase As String = "CASES"
    Const strDGSGrants As String = "GRANTS"
    Const strDGSStory As String = "STORIES"
    Const strDGSAlert As String = "ALERTS"
    Const strDGSReg As String = "REGISTRATIONS"
    Friend WithEvents DataGridTextBoxColumn68 As System.Windows.Forms.DataGridTextBoxColumn

    Const strDGSConvers As String = "CONVERSATIONS"


#Region "Initialize"

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        ''Add any initialization after the InitializeComponent() call
        ''TODO this for each grid so doubleclick will work
        Dim tbx As DataGridTextBoxColumn
        Dim tbs As DataGridTableStyle
        'TODO FIX there has to be a better way than mousedown. This calls the doubleclick twice.
        'neither click nor doubleclick works any better than mousedown.
        For Each tbs In grdMain.TableStyles
            '  tbs.AlternatingBackColor = colorMainGrid
            For Each tbx In tbs.GridColumnStyles
                'AddHandler tbx.TextBox.DoubleClick, New EventHandler(AddressOf dataGridDouble)
                '  AddHandler tbx.TextBox.MouseDown, New MouseEventHandler(AddressOf dataGridDouble)
                tbx.TextBox.Enabled = False
                tbx.NullText = ""
            Next
        Next
        For Each tbs In Me.grdSecond1.TableStyles
            '  tbs.AlternatingBackColor = colorSecGrid
            For Each tbx In tbs.GridColumnStyles
                ' AddHandler tbx.TextBox.MouseDown, New MouseEventHandler(AddressOf dataGridDouble)
                tbx.TextBox.Enabled = False
                tbx.NullText = ""
            Next
        Next

        strbActiveGrid.Append("grdMain")
        Me.HelpProvider1.SetHelpString(Me.grdMain, strHelpGrid)

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
    ' Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents StatusBarPanelID As System.Windows.Forms.StatusBarPanel
    Friend WithEvents DataGridTextBoxColumn66 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn67 As System.Windows.Forms.DataGridTextBoxColumn

    Friend WithEvents sqlGetCases As System.Data.SqlClient.SqlCommand
    Friend WithEvents sqlGetContacts As System.Data.SqlClient.SqlCommand
    Friend WithEvents HelpProvider1 As System.Windows.Forms.HelpProvider
    Friend WithEvents DataGridTextBoxColumn42 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents cboRegion As InfoCtr.ComboBoxRelaxed
    Friend WithEvents cboType As InfoCtr.ComboBoxRelaxed
    Friend WithEvents DataGridTextBoxColumn43 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn44 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn45 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTableStyle7 As System.Windows.Forms.DataGridTableStyle
    Friend WithEvents DataGridTextBoxColumn46 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents chkActive As System.Windows.Forms.CheckBox
    Friend WithEvents cntStories As System.Windows.Forms.Label
    Friend WithEvents rbStories As System.Windows.Forms.RadioButton
    Friend WithEvents sqlGetOrgStories As System.Data.SqlClient.SqlCommand
    Friend WithEvents daContact As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents DsSrchContacts1 As InfoCtr.dsSrchContacts
    Friend WithEvents DataGridTableStyle9 As System.Windows.Forms.DataGridTableStyle
    Friend WithEvents DataGridTextBoxColumn47 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn48 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents miGotoStory As System.Windows.Forms.MenuItem
    Friend WithEvents cntAlerts As System.Windows.Forms.Label
    Friend WithEvents rbALerts As System.Windows.Forms.RadioButton
    Friend WithEvents sqlGetOrgAlerts As System.Data.SqlClient.SqlCommand
    Friend WithEvents DataGridTableStyle10 As System.Windows.Forms.DataGridTableStyle
    Friend WithEvents DataGridTextBoxColumn49 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn50 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents SqlConnection1 As System.Data.SqlClient.SqlConnection
    Friend WithEvents DataGridTextBoxColumn51 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents miGotoAlert As System.Windows.Forms.MenuItem
    Friend WithEvents miStaffRpt As System.Windows.Forms.MenuItem
    Friend WithEvents DataGridTextBoxColumn52 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn53 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn54 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn55 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn56 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn57 As System.Windows.Forms.DataGridTextBoxColumn
    Protected WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents cboField As InfoCtr.ComboBoxRelaxed
    Friend WithEvents DataGridTextBoxColumn58 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents miCaseReport As System.Windows.Forms.MenuItem
    Friend WithEvents DataGridTextBoxColumn59 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn60 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents chkWebsite As System.Windows.Forms.CheckBox
    Friend WithEvents cntGrants As System.Windows.Forms.Label
    Friend WithEvents rbGrants As System.Windows.Forms.RadioButton
    Friend WithEvents sqlGetGrants As System.Data.SqlClient.SqlCommand
    Friend WithEvents TableStyleGrants As System.Windows.Forms.DataGridTableStyle
    Friend WithEvents DataGridTextBoxColumn61 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn62 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn63 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn64 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn65 As System.Windows.Forms.DataGridTextBoxColumn

    Friend WithEvents MenuItem4 As System.Windows.Forms.MenuItem
    ' Friend WithEvents dsSec1 As WindowsApplication11.dsSecondary
    Friend WithEvents miClearSearch As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem3 As System.Windows.Forms.MenuItem
    Friend WithEvents miGotoOrg As System.Windows.Forms.MenuItem
    Friend WithEvents dataGridTableStyle1 As System.Windows.Forms.DataGridTableStyle
    Friend WithEvents dataGridTextBoxColumn1 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents dataGridTextBoxColumn2 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents dataGridTextBoxColumn3 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents grdSecond1 As System.Windows.Forms.DataGrid
    Friend WithEvents dataGridTextBoxColumn12 As System.Windows.Forms.DataGridTextBoxColumn
    '  Friend WithEvents daSec1 As System.data.SqlClient.SqlDataAdapter
    Friend WithEvents sqlspSrchOrgs As System.Data.SqlClient.SqlCommand
    'Protected WithEvents ocboRegion As InfoCtr.ComboBoxRelaxed
    'Protected WithEvents ocboType As InfoCtr.ComboBoxRelaxed
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents MenuItem2 As System.Windows.Forms.MenuItem
    Friend WithEvents miSrchOrg As System.Windows.Forms.MenuItem
    ' Friend WithEvents daSec1 As System.data.SqlClient.SqlDataAdapter
    ' Friend WithEvents daSec2 As System.data.SqlClient.SqlDataAdapter
    Friend WithEvents dataGridTextBoxColumn4 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents dataGridTextBoxColumn5 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents dataGridTextBoxColumn6 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents dataGridTextBoxColumn7 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents dataGridTextBoxColumn8 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents dataGridTextBoxColumn9 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents dataGridTextBoxColumn10 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents grdMain As System.Windows.Forms.DataGrid
    Friend WithEvents dataGridTableStyle2 As System.Windows.Forms.DataGridTableStyle
    Friend WithEvents dataGridTextBoxColumn11 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents MenuItem1 As System.Windows.Forms.MenuItem
    Friend WithEvents miCloseForm As System.Windows.Forms.MenuItem
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents StatusBar1 As System.Windows.Forms.StatusBar
    Friend WithEvents pgLURegion As System.Windows.Forms.TabPage
    Friend WithEvents StatusBarPanel1 As System.Windows.Forms.StatusBarPanel
    Friend WithEvents StatusBarPanel2 As System.Windows.Forms.StatusBarPanel
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents rbContact1Conver2 As System.Windows.Forms.RadioButton
    Friend WithEvents rbCase1Reg2 As System.Windows.Forms.RadioButton
    Friend WithEvents SqlInsertCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlUpdateCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlDeleteCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlInsertCommand2 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlUpdateCommand2 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlDeleteCommand2 As System.Data.SqlClient.SqlCommand
    Friend WithEvents daSrch As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents SqlSelectCommand2 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlSelectCommand3 As System.Data.SqlClient.SqlCommand
    Friend WithEvents DataGridTableStyle3 As System.Windows.Forms.DataGridTableStyle
    Friend WithEvents DataGridTableStyle8 As System.Windows.Forms.DataGridTableStyle
    Friend WithEvents DataGridTextBoxColumn13 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn14 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn15 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn16 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn17 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn18 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents PanelOrg As System.Windows.Forms.Panel
    Friend WithEvents pgOrg As System.Windows.Forms.TabPage
    Friend WithEvents DataGridTableStyle4 As System.Windows.Forms.DataGridTableStyle
    Friend WithEvents DataGridTextBoxColumn19 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn20 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn21 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn22 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn23 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn25 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn26 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn27 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents sqlGetConversations As System.Data.SqlClient.SqlCommand
    Friend WithEvents sqlGetRegistrations As System.Data.SqlClient.SqlCommand
    Friend WithEvents DataGridTableStyle5 As System.Windows.Forms.DataGridTableStyle
    Friend WithEvents DataGridTableStyle6 As System.Windows.Forms.DataGridTableStyle
    Friend WithEvents DataGridTextBoxColumn28 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn29 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn30 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn31 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn32 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn33 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn34 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn35 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn36 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn37 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents miGotoCase As System.Windows.Forms.MenuItem
    Friend WithEvents miGotoContact As System.Windows.Forms.MenuItem
    Friend WithEvents miGotoConversation As System.Windows.Forms.MenuItem
    Friend WithEvents lblSelectionID As System.Windows.Forms.TextBox
    Friend WithEvents MenuItem5 As System.Windows.Forms.MenuItem
    Friend WithEvents miAddOrg As System.Windows.Forms.MenuItem
    Friend WithEvents DataGridTextBoxColumn38 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn39 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn24 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn40 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn41 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents lblCurrentMain As System.Windows.Forms.Label
    Friend WithEvents fldMainCurrentID As System.Windows.Forms.TextBox
    Friend WithEvents btnNew As System.Windows.Forms.Button
    Friend WithEvents btnHelpOrg As System.Windows.Forms.Button
    Friend WithEvents tbMainGrid As System.Windows.Forms.TabControl
    Friend WithEvents pnlMainGrid As System.Windows.Forms.Panel
    Friend WithEvents pgGridOrg As System.Windows.Forms.TabPage
    Friend WithEvents pgGridContact As System.Windows.Forms.TabPage
    Friend WithEvents chkWild As System.Windows.Forms.CheckBox
    Friend WithEvents dsSrchOrgs As InfoCtr.dsSrchOrg
    Friend WithEvents chkDetail As System.Windows.Forms.CheckBox
    Friend WithEvents sqlspSrchContacts As System.Data.SqlClient.SqlCommand
    Friend WithEvents pnlLuOrgContact As System.Windows.Forms.Panel
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents lblSelection As System.Windows.Forms.Label
    Friend WithEvents miGotoRegistration As System.Windows.Forms.MenuItem
    Friend WithEvents miHelp As System.Windows.Forms.MenuItem
    Friend WithEvents MMOrgContact As System.Windows.Forms.MainMenu
    Friend WithEvents MenuItem8 As System.Windows.Forms.MenuItem
    Friend WithEvents CntContacts As System.Windows.Forms.Label
    Friend WithEvents cntCases As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSrchOrg))
        Me.pgOrg = New System.Windows.Forms.TabPage()
        Me.btnHelpOrg = New System.Windows.Forms.Button()
        Me.pnlMainGrid = New System.Windows.Forms.Panel()
        Me.chkWebsite = New System.Windows.Forms.CheckBox()
        Me.dsSrchOrgs = New InfoCtr.dsSrchOrg()
        Me.lblSelection = New System.Windows.Forms.Label()
        Me.grdMain = New System.Windows.Forms.DataGrid()
        Me.dataGridTableStyle2 = New System.Windows.Forms.DataGridTableStyle()
        Me.dataGridTextBoxColumn11 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.dataGridTextBoxColumn4 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.dataGridTextBoxColumn5 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.dataGridTextBoxColumn6 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.dataGridTextBoxColumn7 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.dataGridTextBoxColumn8 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.dataGridTextBoxColumn9 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.dataGridTextBoxColumn10 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn43 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn44 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn45 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTableStyle4 = New System.Windows.Forms.DataGridTableStyle()
        Me.DataGridTextBoxColumn19 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn20 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn21 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn22 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn23 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn25 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn26 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn27 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn39 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn38 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn40 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn55 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn56 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTableStyle7 = New System.Windows.Forms.DataGridTableStyle()
        Me.grdSecond1 = New System.Windows.Forms.DataGrid()
        Me.DataGridTableStyle3 = New System.Windows.Forms.DataGridTableStyle()
        Me.DataGridTextBoxColumn13 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn18 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn14 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn15 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn16 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn66 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn17 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn46 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.dataGridTableStyle1 = New System.Windows.Forms.DataGridTableStyle()
        Me.dataGridTextBoxColumn1 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.dataGridTextBoxColumn2 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.dataGridTextBoxColumn3 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn42 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.dataGridTextBoxColumn12 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn58 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn60 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTableStyle5 = New System.Windows.Forms.DataGridTableStyle()
        Me.DataGridTextBoxColumn32 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn33 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn34 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn35 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn36 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn37 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn24 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTableStyle6 = New System.Windows.Forms.DataGridTableStyle()
        Me.DataGridTextBoxColumn41 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn28 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn29 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn30 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn31 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn54 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn59 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn68 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTableStyle8 = New System.Windows.Forms.DataGridTableStyle()
        Me.DataGridTableStyle9 = New System.Windows.Forms.DataGridTableStyle()
        Me.DataGridTextBoxColumn47 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn53 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn48 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTableStyle10 = New System.Windows.Forms.DataGridTableStyle()
        Me.DataGridTextBoxColumn49 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn51 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn50 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn52 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn57 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.TableStyleGrants = New System.Windows.Forms.DataGridTableStyle()
        Me.DataGridTextBoxColumn61 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn62 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn63 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn64 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn65 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn67 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.tbMainGrid = New System.Windows.Forms.TabControl()
        Me.pgGridOrg = New System.Windows.Forms.TabPage()
        Me.pgGridContact = New System.Windows.Forms.TabPage()
        Me.lblSelectionID = New System.Windows.Forms.TextBox()
        Me.chkDetail = New System.Windows.Forms.CheckBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.cntGrants = New System.Windows.Forms.Label()
        Me.rbGrants = New System.Windows.Forms.RadioButton()
        Me.cntAlerts = New System.Windows.Forms.Label()
        Me.rbALerts = New System.Windows.Forms.RadioButton()
        Me.cntStories = New System.Windows.Forms.Label()
        Me.rbStories = New System.Windows.Forms.RadioButton()
        Me.cntCases = New System.Windows.Forms.Label()
        Me.CntContacts = New System.Windows.Forms.Label()
        Me.rbCase1Reg2 = New System.Windows.Forms.RadioButton()
        Me.rbContact1Conver2 = New System.Windows.Forms.RadioButton()
        Me.StatusBar1 = New System.Windows.Forms.StatusBar()
        Me.StatusBarPanel1 = New System.Windows.Forms.StatusBarPanel()
        Me.StatusBarPanelID = New System.Windows.Forms.StatusBarPanel()
        Me.StatusBarPanel2 = New System.Windows.Forms.StatusBarPanel()
        Me.PanelOrg = New System.Windows.Forms.Panel()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.cboField = New InfoCtr.ComboBoxRelaxed()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.chkActive = New System.Windows.Forms.CheckBox()
        Me.cboType = New InfoCtr.ComboBoxRelaxed()
        Me.cboRegion = New InfoCtr.ComboBoxRelaxed()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.chkWild = New System.Windows.Forms.CheckBox()
        Me.pgLURegion = New System.Windows.Forms.TabPage()
        Me.MenuItem4 = New System.Windows.Forms.MenuItem()
        Me.miCaseReport = New System.Windows.Forms.MenuItem()
        Me.miStaffRpt = New System.Windows.Forms.MenuItem()
        Me.miGotoContact = New System.Windows.Forms.MenuItem()
        Me.miClearSearch = New System.Windows.Forms.MenuItem()
        Me.MenuItem3 = New System.Windows.Forms.MenuItem()
        Me.miGotoOrg = New System.Windows.Forms.MenuItem()
        Me.miGotoCase = New System.Windows.Forms.MenuItem()
        Me.miGotoConversation = New System.Windows.Forms.MenuItem()
        Me.miGotoRegistration = New System.Windows.Forms.MenuItem()
        Me.miGotoStory = New System.Windows.Forms.MenuItem()
        Me.miGotoAlert = New System.Windows.Forms.MenuItem()
        Me.sqlspSrchOrgs = New System.Data.SqlClient.SqlCommand()
        Me.MenuItem2 = New System.Windows.Forms.MenuItem()
        Me.miSrchOrg = New System.Windows.Forms.MenuItem()
        Me.MenuItem5 = New System.Windows.Forms.MenuItem()
        Me.MenuItem1 = New System.Windows.Forms.MenuItem()
        Me.miAddOrg = New System.Windows.Forms.MenuItem()
        Me.MenuItem8 = New System.Windows.Forms.MenuItem()
        Me.miCloseForm = New System.Windows.Forms.MenuItem()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.btnNew = New System.Windows.Forms.Button()
        Me.MMOrgContact = New System.Windows.Forms.MainMenu(Me.components)
        Me.miHelp = New System.Windows.Forms.MenuItem()
        Me.SqlInsertCommand1 = New System.Data.SqlClient.SqlCommand()
        Me.SqlUpdateCommand1 = New System.Data.SqlClient.SqlCommand()
        Me.SqlDeleteCommand1 = New System.Data.SqlClient.SqlCommand()
        Me.sqlGetCases = New System.Data.SqlClient.SqlCommand()
        Me.SqlSelectCommand2 = New System.Data.SqlClient.SqlCommand()
        Me.SqlInsertCommand2 = New System.Data.SqlClient.SqlCommand()
        Me.SqlUpdateCommand2 = New System.Data.SqlClient.SqlCommand()
        Me.SqlDeleteCommand2 = New System.Data.SqlClient.SqlCommand()
        Me.sqlGetContacts = New System.Data.SqlClient.SqlCommand()
        Me.SqlSelectCommand3 = New System.Data.SqlClient.SqlCommand()
        Me.daSrch = New System.Data.SqlClient.SqlDataAdapter()
        Me.sqlGetConversations = New System.Data.SqlClient.SqlCommand()
        Me.daContact = New System.Data.SqlClient.SqlDataAdapter()
        Me.sqlspSrchContacts = New System.Data.SqlClient.SqlCommand()
        Me.SqlConnection1 = New System.Data.SqlClient.SqlConnection()
        Me.sqlGetRegistrations = New System.Data.SqlClient.SqlCommand()
        Me.lblCurrentMain = New System.Windows.Forms.Label()
        Me.fldMainCurrentID = New System.Windows.Forms.TextBox()
        Me.pnlLuOrgContact = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.HelpProvider1 = New System.Windows.Forms.HelpProvider()
        Me.sqlGetOrgStories = New System.Data.SqlClient.SqlCommand()
        Me.sqlGetOrgAlerts = New System.Data.SqlClient.SqlCommand()
        Me.DsSrchContacts1 = New InfoCtr.dsSrchContacts()
        Me.sqlGetGrants = New System.Data.SqlClient.SqlCommand()
        Me.pnlMainGrid.SuspendLayout()
        CType(Me.dsSrchOrgs, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdMain, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdSecond1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tbMainGrid.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.StatusBarPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.StatusBarPanelID, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.StatusBarPanel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelOrg.SuspendLayout()
        Me.pnlLuOrgContact.SuspendLayout()
        CType(Me.DsSrchContacts1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pgOrg
        '
        Me.pgOrg.Location = New System.Drawing.Point(4, 22)
        Me.pgOrg.Name = "pgOrg"
        Me.pgOrg.Size = New System.Drawing.Size(865, 547)
        Me.pgOrg.TabIndex = 0
        Me.pgOrg.Text = "FIND ORGANIZATION or CONTACT"
        '
        'btnHelpOrg
        '
        Me.btnHelpOrg.BackColor = System.Drawing.SystemColors.Control
        Me.btnHelpOrg.Font = New System.Drawing.Font("Microsoft Sans Serif", 22.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnHelpOrg.ForeColor = System.Drawing.SystemColors.Control
        Me.btnHelpOrg.Image = CType(resources.GetObject("btnHelpOrg.Image"), System.Drawing.Image)
        Me.btnHelpOrg.Location = New System.Drawing.Point(860, 5)
        Me.btnHelpOrg.Name = "btnHelpOrg"
        Me.btnHelpOrg.Size = New System.Drawing.Size(25, 25)
        Me.btnHelpOrg.TabIndex = 217
        Me.btnHelpOrg.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ToolTip1.SetToolTip(Me.btnHelpOrg, "Help: How to use this Search page.")
        Me.btnHelpOrg.UseVisualStyleBackColor = False
        '
        'pnlMainGrid
        '
        Me.pnlMainGrid.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlMainGrid.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pnlMainGrid.Controls.Add(Me.chkWebsite)
        Me.pnlMainGrid.Controls.Add(Me.lblSelection)
        Me.pnlMainGrid.Controls.Add(Me.grdMain)
        Me.pnlMainGrid.Controls.Add(Me.grdSecond1)
        Me.pnlMainGrid.Controls.Add(Me.tbMainGrid)
        Me.pnlMainGrid.Controls.Add(Me.lblSelectionID)
        Me.pnlMainGrid.Controls.Add(Me.chkDetail)
        Me.pnlMainGrid.Controls.Add(Me.Label4)
        Me.pnlMainGrid.Location = New System.Drawing.Point(182, 22)
        Me.pnlMainGrid.Name = "pnlMainGrid"
        Me.pnlMainGrid.Size = New System.Drawing.Size(887, 504)
        Me.pnlMainGrid.TabIndex = 219
        '
        'chkWebsite
        '
        Me.chkWebsite.AutoSize = True
        Me.chkWebsite.DataBindings.Add(New System.Windows.Forms.Binding("Checked", Me.dsSrchOrgs, "SrchOrg.HasWebsite", True))
        Me.chkWebsite.Location = New System.Drawing.Point(782, 7)
        Me.chkWebsite.Name = "chkWebsite"
        Me.chkWebsite.Size = New System.Drawing.Size(87, 17)
        Me.chkWebsite.TabIndex = 219
        Me.chkWebsite.Text = "Has Website"
        Me.chkWebsite.UseVisualStyleBackColor = True
        Me.chkWebsite.Visible = False
        '
        'dsSrchOrgs
        '
        Me.dsSrchOrgs.DataSetName = "dsSrchOrg"
        Me.dsSrchOrgs.Locale = New System.Globalization.CultureInfo("en-US")
        Me.dsSrchOrgs.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'lblSelection
        '
        Me.lblSelection.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSelection.ForeColor = System.Drawing.SystemColors.ControlDark
        Me.lblSelection.Location = New System.Drawing.Point(666, 298)
        Me.lblSelection.Name = "lblSelection"
        Me.lblSelection.Size = New System.Drawing.Size(38, 14)
        Me.lblSelection.TabIndex = 220
        Me.lblSelection.Text = "Item #"
        Me.lblSelection.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'grdMain
        '
        Me.grdMain.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdMain.BackgroundColor = System.Drawing.SystemColors.Control
        Me.grdMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.grdMain.CaptionBackColor = System.Drawing.SystemColors.InactiveCaption
        Me.grdMain.CaptionText = "Search Result Grid"
        Me.grdMain.DataMember = "SrchOrg"
        Me.grdMain.DataSource = Me.dsSrchOrgs
        Me.grdMain.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.grdMain.Location = New System.Drawing.Point(4, 30)
        Me.grdMain.Name = "grdMain"
        Me.grdMain.ParentRowsBackColor = System.Drawing.SystemColors.Window
        Me.grdMain.ParentRowsVisible = False
        Me.grdMain.ReadOnly = True
        Me.grdMain.RowHeaderWidth = 20
        Me.grdMain.SelectionBackColor = System.Drawing.SystemColors.Highlight
        Me.grdMain.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        Me.grdMain.Size = New System.Drawing.Size(876, 263)
        Me.grdMain.TabIndex = 202
        Me.grdMain.TableStyles.AddRange(New System.Windows.Forms.DataGridTableStyle() {Me.dataGridTableStyle2, Me.DataGridTableStyle4, Me.DataGridTableStyle7})
        Me.grdMain.Tag = "Main"
        Me.ToolTip1.SetToolTip(Me.grdMain, "Click in grid, then press F1 for grid help")
        '
        'dataGridTableStyle2
        '
        Me.dataGridTableStyle2.AlternatingBackColor = System.Drawing.Color.FromArgb(CType(CType(204, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.dataGridTableStyle2.DataGrid = Me.grdMain
        Me.dataGridTableStyle2.ForeColor = System.Drawing.Color.DarkGreen
        Me.dataGridTableStyle2.GridColumnStyles.AddRange(New System.Windows.Forms.DataGridColumnStyle() {Me.dataGridTextBoxColumn11, Me.dataGridTextBoxColumn4, Me.dataGridTextBoxColumn5, Me.dataGridTextBoxColumn6, Me.dataGridTextBoxColumn7, Me.dataGridTextBoxColumn8, Me.dataGridTextBoxColumn9, Me.dataGridTextBoxColumn10, Me.DataGridTextBoxColumn43, Me.DataGridTextBoxColumn44, Me.DataGridTextBoxColumn45})
        Me.dataGridTableStyle2.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.dataGridTableStyle2.MappingName = "SrchOrg"
        Me.dataGridTableStyle2.RowHeaderWidth = 20
        Me.dataGridTableStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        '
        'dataGridTextBoxColumn11
        '
        Me.dataGridTextBoxColumn11.Format = ""
        Me.dataGridTextBoxColumn11.FormatInfo = Nothing
        Me.dataGridTextBoxColumn11.HeaderText = "OrgID"
        Me.dataGridTextBoxColumn11.MappingName = "OrgID"
        Me.dataGridTextBoxColumn11.Width = 0
        '
        'dataGridTextBoxColumn4
        '
        Me.dataGridTextBoxColumn4.Format = ""
        Me.dataGridTextBoxColumn4.FormatInfo = Nothing
        Me.dataGridTextBoxColumn4.HeaderText = "Organization"
        Me.dataGridTextBoxColumn4.MappingName = "OrgName"
        Me.dataGridTextBoxColumn4.Width = 175
        '
        'dataGridTextBoxColumn5
        '
        Me.dataGridTextBoxColumn5.Format = ""
        Me.dataGridTextBoxColumn5.FormatInfo = Nothing
        Me.dataGridTextBoxColumn5.HeaderText = "City"
        Me.dataGridTextBoxColumn5.MappingName = "City"
        Me.dataGridTextBoxColumn5.Width = 75
        '
        'dataGridTextBoxColumn6
        '
        Me.dataGridTextBoxColumn6.Format = ""
        Me.dataGridTextBoxColumn6.FormatInfo = Nothing
        Me.dataGridTextBoxColumn6.HeaderText = "Street"
        Me.dataGridTextBoxColumn6.MappingName = "Street"
        Me.dataGridTextBoxColumn6.Width = 75
        '
        'dataGridTextBoxColumn7
        '
        Me.dataGridTextBoxColumn7.Format = ""
        Me.dataGridTextBoxColumn7.FormatInfo = Nothing
        Me.dataGridTextBoxColumn7.HeaderText = "Phone"
        Me.dataGridTextBoxColumn7.MappingName = "Phone"
        Me.dataGridTextBoxColumn7.Width = 90
        '
        'dataGridTextBoxColumn8
        '
        Me.dataGridTextBoxColumn8.Format = ""
        Me.dataGridTextBoxColumn8.FormatInfo = Nothing
        Me.dataGridTextBoxColumn8.HeaderText = "Denom"
        Me.dataGridTextBoxColumn8.MappingName = "Denomination"
        Me.dataGridTextBoxColumn8.Width = 75
        '
        'dataGridTextBoxColumn9
        '
        Me.dataGridTextBoxColumn9.Format = ""
        Me.dataGridTextBoxColumn9.FormatInfo = Nothing
        Me.dataGridTextBoxColumn9.HeaderText = "Type"
        Me.dataGridTextBoxColumn9.MappingName = "OrgType"
        Me.dataGridTextBoxColumn9.Width = 75
        '
        'dataGridTextBoxColumn10
        '
        Me.dataGridTextBoxColumn10.Format = ""
        Me.dataGridTextBoxColumn10.FormatInfo = Nothing
        Me.dataGridTextBoxColumn10.HeaderText = "Region"
        Me.dataGridTextBoxColumn10.MappingName = "SatelliteRegion"
        Me.dataGridTextBoxColumn10.Width = 60
        '
        'DataGridTextBoxColumn43
        '
        Me.DataGridTextBoxColumn43.Format = ""
        Me.DataGridTextBoxColumn43.FormatInfo = Nothing
        Me.DataGridTextBoxColumn43.HeaderText = "Open Grants"
        Me.DataGridTextBoxColumn43.MappingName = "OpenGrants"
        Me.DataGridTextBoxColumn43.Width = 75
        '
        'DataGridTextBoxColumn44
        '
        Me.DataGridTextBoxColumn44.Format = ""
        Me.DataGridTextBoxColumn44.FormatInfo = Nothing
        Me.DataGridTextBoxColumn44.HeaderText = "Email"
        Me.DataGridTextBoxColumn44.MappingName = "Email"
        Me.DataGridTextBoxColumn44.Width = 75
        '
        'DataGridTextBoxColumn45
        '
        Me.DataGridTextBoxColumn45.Format = ""
        Me.DataGridTextBoxColumn45.FormatInfo = Nothing
        Me.DataGridTextBoxColumn45.HeaderText = "Zip"
        Me.DataGridTextBoxColumn45.MappingName = "Zip"
        Me.DataGridTextBoxColumn45.Width = 75
        '
        'DataGridTableStyle4
        '
        Me.DataGridTableStyle4.AlternatingBackColor = System.Drawing.Color.FromArgb(CType(CType(204, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.DataGridTableStyle4.DataGrid = Me.grdMain
        Me.DataGridTableStyle4.ForeColor = System.Drawing.Color.DarkGreen
        Me.DataGridTableStyle4.GridColumnStyles.AddRange(New System.Windows.Forms.DataGridColumnStyle() {Me.DataGridTextBoxColumn19, Me.DataGridTextBoxColumn20, Me.DataGridTextBoxColumn21, Me.DataGridTextBoxColumn22, Me.DataGridTextBoxColumn23, Me.DataGridTextBoxColumn25, Me.DataGridTextBoxColumn26, Me.DataGridTextBoxColumn27, Me.DataGridTextBoxColumn39, Me.DataGridTextBoxColumn38, Me.DataGridTextBoxColumn40, Me.DataGridTextBoxColumn55, Me.DataGridTextBoxColumn56})
        Me.DataGridTableStyle4.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.DataGridTableStyle4.MappingName = "SrchContact"
        Me.DataGridTableStyle4.RowHeaderWidth = 20
        Me.DataGridTableStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
        '
        'DataGridTextBoxColumn19
        '
        Me.DataGridTextBoxColumn19.Format = ""
        Me.DataGridTextBoxColumn19.FormatInfo = Nothing
        Me.DataGridTextBoxColumn19.HeaderText = "ContactID"
        Me.DataGridTextBoxColumn19.MappingName = "ContactID"
        Me.DataGridTextBoxColumn19.Width = 0
        '
        'DataGridTextBoxColumn20
        '
        Me.DataGridTextBoxColumn20.Format = ""
        Me.DataGridTextBoxColumn20.FormatInfo = Nothing
        Me.DataGridTextBoxColumn20.HeaderText = "OrgNum"
        Me.DataGridTextBoxColumn20.MappingName = "OrgNum"
        Me.DataGridTextBoxColumn20.Width = 0
        '
        'DataGridTextBoxColumn21
        '
        Me.DataGridTextBoxColumn21.Format = ""
        Me.DataGridTextBoxColumn21.FormatInfo = Nothing
        Me.DataGridTextBoxColumn21.HeaderText = "Prefix"
        Me.DataGridTextBoxColumn21.MappingName = "Prefix"
        Me.DataGridTextBoxColumn21.Width = 40
        '
        'DataGridTextBoxColumn22
        '
        Me.DataGridTextBoxColumn22.Format = ""
        Me.DataGridTextBoxColumn22.FormatInfo = Nothing
        Me.DataGridTextBoxColumn22.HeaderText = "FirstName"
        Me.DataGridTextBoxColumn22.MappingName = "FirstName"
        Me.DataGridTextBoxColumn22.Width = 75
        '
        'DataGridTextBoxColumn23
        '
        Me.DataGridTextBoxColumn23.Format = ""
        Me.DataGridTextBoxColumn23.FormatInfo = Nothing
        Me.DataGridTextBoxColumn23.HeaderText = "LastName"
        Me.DataGridTextBoxColumn23.MappingName = "LastName"
        Me.DataGridTextBoxColumn23.Width = 75
        '
        'DataGridTextBoxColumn25
        '
        Me.DataGridTextBoxColumn25.Format = ""
        Me.DataGridTextBoxColumn25.FormatInfo = Nothing
        Me.DataGridTextBoxColumn25.HeaderText = "Staff"
        Me.DataGridTextBoxColumn25.MappingName = "Staff"
        Me.DataGridTextBoxColumn25.Width = 65
        '
        'DataGridTextBoxColumn26
        '
        Me.DataGridTextBoxColumn26.Format = ""
        Me.DataGridTextBoxColumn26.FormatInfo = Nothing
        Me.DataGridTextBoxColumn26.HeaderText = "JobTitle"
        Me.DataGridTextBoxColumn26.MappingName = "JobTitle"
        Me.DataGridTextBoxColumn26.Width = 150
        '
        'DataGridTextBoxColumn27
        '
        Me.DataGridTextBoxColumn27.Format = ""
        Me.DataGridTextBoxColumn27.FormatInfo = Nothing
        Me.DataGridTextBoxColumn27.HeaderText = "Organization"
        Me.DataGridTextBoxColumn27.MappingName = "Org"
        Me.DataGridTextBoxColumn27.Width = 150
        '
        'DataGridTextBoxColumn39
        '
        Me.DataGridTextBoxColumn39.Format = ""
        Me.DataGridTextBoxColumn39.FormatInfo = Nothing
        Me.DataGridTextBoxColumn39.HeaderText = "Type"
        Me.DataGridTextBoxColumn39.MappingName = "OrgType"
        Me.DataGridTextBoxColumn39.Width = 50
        '
        'DataGridTextBoxColumn38
        '
        Me.DataGridTextBoxColumn38.Format = ""
        Me.DataGridTextBoxColumn38.FormatInfo = Nothing
        Me.DataGridTextBoxColumn38.HeaderText = "Region"
        Me.DataGridTextBoxColumn38.MappingName = "SatelliteRegion"
        Me.DataGridTextBoxColumn38.Width = 75
        '
        'DataGridTextBoxColumn40
        '
        Me.DataGridTextBoxColumn40.Format = ""
        Me.DataGridTextBoxColumn40.FormatInfo = Nothing
        Me.DataGridTextBoxColumn40.HeaderText = "Org. Phone"
        Me.DataGridTextBoxColumn40.MappingName = "OrgPhone"
        Me.DataGridTextBoxColumn40.Width = 90
        '
        'DataGridTextBoxColumn55
        '
        Me.DataGridTextBoxColumn55.Format = ""
        Me.DataGridTextBoxColumn55.FormatInfo = Nothing
        Me.DataGridTextBoxColumn55.HeaderText = "Cell Phone"
        Me.DataGridTextBoxColumn55.MappingName = "CellPhone"
        Me.DataGridTextBoxColumn55.Width = 75
        '
        'DataGridTextBoxColumn56
        '
        Me.DataGridTextBoxColumn56.Format = ""
        Me.DataGridTextBoxColumn56.FormatInfo = Nothing
        Me.DataGridTextBoxColumn56.HeaderText = "HomePhone"
        Me.DataGridTextBoxColumn56.MappingName = "HomePhone"
        Me.DataGridTextBoxColumn56.Width = 75
        '
        'DataGridTableStyle7
        '
        Me.DataGridTableStyle7.AlternatingBackColor = System.Drawing.Color.FromArgb(CType(CType(204, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.DataGridTableStyle7.DataGrid = Me.grdMain
        Me.DataGridTableStyle7.ForeColor = System.Drawing.Color.DarkGreen
        Me.DataGridTableStyle7.GridColumnStyles.AddRange(New System.Windows.Forms.DataGridColumnStyle() {Me.dataGridTextBoxColumn11, Me.dataGridTextBoxColumn4, Me.dataGridTextBoxColumn5, Me.dataGridTextBoxColumn6, Me.dataGridTextBoxColumn7, Me.dataGridTextBoxColumn8, Me.dataGridTextBoxColumn9, Me.dataGridTextBoxColumn10, Me.DataGridTextBoxColumn44, Me.DataGridTextBoxColumn45})
        Me.DataGridTableStyle7.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.DataGridTableStyle7.MappingName = "SrchOrgNonGrantable"
        Me.DataGridTableStyle7.RowHeaderWidth = 20
        Me.DataGridTableStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight
        '
        'grdSecond1
        '
        Me.grdSecond1.AlternatingBackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.grdSecond1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdSecond1.CaptionBackColor = System.Drawing.SystemColors.InactiveCaption
        Me.grdSecond1.CaptionText = "Related Information Grid"
        Me.grdSecond1.DataMember = ""
        Me.grdSecond1.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.grdSecond1.Location = New System.Drawing.Point(7, 315)
        Me.grdSecond1.Name = "grdSecond1"
        Me.grdSecond1.ParentRowsVisible = False
        Me.grdSecond1.ReadOnly = True
        Me.grdSecond1.RowHeaderWidth = 30
        Me.grdSecond1.Size = New System.Drawing.Size(862, 185)
        Me.grdSecond1.TabIndex = 203
        Me.grdSecond1.TableStyles.AddRange(New System.Windows.Forms.DataGridTableStyle() {Me.DataGridTableStyle3, Me.dataGridTableStyle1, Me.DataGridTableStyle5, Me.DataGridTableStyle6, Me.DataGridTableStyle8, Me.DataGridTableStyle9, Me.DataGridTableStyle10, Me.TableStyleGrants})
        Me.grdSecond1.Tag = "Secondary"
        Me.ToolTip1.SetToolTip(Me.grdSecond1, "Double-click to go to detail window.")
        Me.grdSecond1.Visible = False
        '
        'DataGridTableStyle3
        '
        Me.DataGridTableStyle3.AlternatingBackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.DataGridTableStyle3.DataGrid = Me.grdSecond1
        Me.DataGridTableStyle3.ForeColor = System.Drawing.Color.DarkGreen
        Me.DataGridTableStyle3.GridColumnStyles.AddRange(New System.Windows.Forms.DataGridColumnStyle() {Me.DataGridTextBoxColumn13, Me.DataGridTextBoxColumn18, Me.DataGridTextBoxColumn14, Me.DataGridTextBoxColumn15, Me.DataGridTextBoxColumn16, Me.DataGridTextBoxColumn66, Me.DataGridTextBoxColumn17, Me.DataGridTextBoxColumn46})
        Me.DataGridTableStyle3.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.DataGridTableStyle3.MappingName = "FoundOrgCases"
        Me.DataGridTableStyle3.RowHeaderWidth = 15
        Me.DataGridTableStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        '
        'DataGridTextBoxColumn13
        '
        Me.DataGridTextBoxColumn13.Format = ""
        Me.DataGridTextBoxColumn13.FormatInfo = Nothing
        Me.DataGridTextBoxColumn13.HeaderText = "CaseID"
        Me.DataGridTextBoxColumn13.MappingName = "CaseID"
        Me.DataGridTextBoxColumn13.Width = 0
        '
        'DataGridTextBoxColumn18
        '
        Me.DataGridTextBoxColumn18.Format = ""
        Me.DataGridTextBoxColumn18.FormatInfo = Nothing
        Me.DataGridTextBoxColumn18.HeaderText = "OrgNum"
        Me.DataGridTextBoxColumn18.MappingName = "OrgNum"
        Me.DataGridTextBoxColumn18.Width = 0
        '
        'DataGridTextBoxColumn14
        '
        Me.DataGridTextBoxColumn14.Format = ""
        Me.DataGridTextBoxColumn14.FormatInfo = Nothing
        Me.DataGridTextBoxColumn14.HeaderText = "Case Name"
        Me.DataGridTextBoxColumn14.MappingName = "CaseName"
        Me.DataGridTextBoxColumn14.Width = 200
        '
        'DataGridTextBoxColumn15
        '
        Me.DataGridTextBoxColumn15.Format = ""
        Me.DataGridTextBoxColumn15.FormatInfo = Nothing
        Me.DataGridTextBoxColumn15.HeaderText = "CaseStatus"
        Me.DataGridTextBoxColumn15.MappingName = "CaseStatus"
        Me.DataGridTextBoxColumn15.Width = 120
        '
        'DataGridTextBoxColumn16
        '
        Me.DataGridTextBoxColumn16.Format = ""
        Me.DataGridTextBoxColumn16.FormatInfo = Nothing
        Me.DataGridTextBoxColumn16.HeaderText = "Case Manager"
        Me.DataGridTextBoxColumn16.MappingName = "CaseMgr"
        Me.DataGridTextBoxColumn16.Width = 120
        '
        'DataGridTextBoxColumn66
        '
        Me.DataGridTextBoxColumn66.Format = "d"
        Me.DataGridTextBoxColumn66.FormatInfo = Nothing
        Me.DataGridTextBoxColumn66.HeaderText = "Last Call"
        Me.DataGridTextBoxColumn66.MappingName = "LastCall"
        Me.DataGridTextBoxColumn66.Width = 75
        '
        'DataGridTextBoxColumn17
        '
        Me.DataGridTextBoxColumn17.Format = ""
        Me.DataGridTextBoxColumn17.FormatInfo = Nothing
        Me.DataGridTextBoxColumn17.HeaderText = "Latest Grant Activity"
        Me.DataGridTextBoxColumn17.MappingName = "GrantLatest"
        Me.DataGridTextBoxColumn17.NullText = ""
        Me.DataGridTextBoxColumn17.Width = 150
        '
        'DataGridTextBoxColumn46
        '
        Me.DataGridTextBoxColumn46.Format = ""
        Me.DataGridTextBoxColumn46.FormatInfo = Nothing
        Me.DataGridTextBoxColumn46.HeaderText = "Congr. Final Rpt"
        Me.DataGridTextBoxColumn46.MappingName = "CongregationFinalReport"
        Me.DataGridTextBoxColumn46.Width = 140
        '
        'dataGridTableStyle1
        '
        Me.dataGridTableStyle1.AlternatingBackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.dataGridTableStyle1.DataGrid = Me.grdSecond1
        Me.dataGridTableStyle1.ForeColor = System.Drawing.Color.DarkGreen
        Me.dataGridTableStyle1.GridColumnStyles.AddRange(New System.Windows.Forms.DataGridColumnStyle() {Me.dataGridTextBoxColumn1, Me.dataGridTextBoxColumn2, Me.dataGridTextBoxColumn3, Me.DataGridTextBoxColumn42, Me.dataGridTextBoxColumn12, Me.DataGridTextBoxColumn58, Me.DataGridTextBoxColumn60})
        Me.dataGridTableStyle1.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.dataGridTableStyle1.MappingName = "FoundOrgContacts"
        Me.dataGridTableStyle1.RowHeaderWidth = 20
        Me.dataGridTableStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        '
        'dataGridTextBoxColumn1
        '
        Me.dataGridTextBoxColumn1.Format = ""
        Me.dataGridTextBoxColumn1.FormatInfo = Nothing
        Me.dataGridTextBoxColumn1.MappingName = "ContactID"
        Me.dataGridTextBoxColumn1.Width = 0
        '
        'dataGridTextBoxColumn2
        '
        Me.dataGridTextBoxColumn2.Format = ""
        Me.dataGridTextBoxColumn2.FormatInfo = Nothing
        Me.dataGridTextBoxColumn2.HeaderText = "Last Name"
        Me.dataGridTextBoxColumn2.MappingName = "Lastname"
        Me.dataGridTextBoxColumn2.NullText = ""
        Me.dataGridTextBoxColumn2.Width = 120
        '
        'dataGridTextBoxColumn3
        '
        Me.dataGridTextBoxColumn3.Format = ""
        Me.dataGridTextBoxColumn3.FormatInfo = Nothing
        Me.dataGridTextBoxColumn3.HeaderText = "First Name"
        Me.dataGridTextBoxColumn3.MappingName = "FirstName"
        Me.dataGridTextBoxColumn3.NullText = ""
        Me.dataGridTextBoxColumn3.Width = 120
        '
        'DataGridTextBoxColumn42
        '
        Me.DataGridTextBoxColumn42.Format = ""
        Me.DataGridTextBoxColumn42.FormatInfo = Nothing
        Me.DataGridTextBoxColumn42.HeaderText = "Staff"
        Me.DataGridTextBoxColumn42.MappingName = "Staff"
        Me.DataGridTextBoxColumn42.Width = 75
        '
        'dataGridTextBoxColumn12
        '
        Me.dataGridTextBoxColumn12.Format = ""
        Me.dataGridTextBoxColumn12.FormatInfo = Nothing
        Me.dataGridTextBoxColumn12.HeaderText = "Job Title"
        Me.dataGridTextBoxColumn12.MappingName = "JobTitle"
        Me.dataGridTextBoxColumn12.NullText = ""
        Me.dataGridTextBoxColumn12.Width = 175
        '
        'DataGridTextBoxColumn58
        '
        Me.DataGridTextBoxColumn58.Format = ""
        Me.DataGridTextBoxColumn58.FormatInfo = Nothing
        Me.DataGridTextBoxColumn58.HeaderText = "Email"
        Me.DataGridTextBoxColumn58.MappingName = "Email"
        Me.DataGridTextBoxColumn58.Width = 175
        '
        'DataGridTextBoxColumn60
        '
        Me.DataGridTextBoxColumn60.Format = ""
        Me.DataGridTextBoxColumn60.FormatInfo = Nothing
        Me.DataGridTextBoxColumn60.HeaderText = "AmountDue"
        Me.DataGridTextBoxColumn60.MappingName = "AmountDue"
        Me.DataGridTextBoxColumn60.Width = 75
        '
        'DataGridTableStyle5
        '
        Me.DataGridTableStyle5.AlternatingBackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.DataGridTableStyle5.DataGrid = Me.grdSecond1
        Me.DataGridTableStyle5.ForeColor = System.Drawing.Color.DarkGreen
        Me.DataGridTableStyle5.GridColumnStyles.AddRange(New System.Windows.Forms.DataGridColumnStyle() {Me.DataGridTextBoxColumn32, Me.DataGridTextBoxColumn33, Me.DataGridTextBoxColumn34, Me.DataGridTextBoxColumn35, Me.DataGridTextBoxColumn36, Me.DataGridTextBoxColumn37, Me.DataGridTextBoxColumn24})
        Me.DataGridTableStyle5.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.DataGridTableStyle5.MappingName = "GetConversations"
        Me.DataGridTableStyle5.RowHeaderWidth = 15
        Me.DataGridTableStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight
        '
        'DataGridTextBoxColumn32
        '
        Me.DataGridTextBoxColumn32.Format = ""
        Me.DataGridTextBoxColumn32.FormatInfo = Nothing
        Me.DataGridTextBoxColumn32.HeaderText = "ConversID"
        Me.DataGridTextBoxColumn32.MappingName = "ConversID"
        Me.DataGridTextBoxColumn32.Width = 0
        '
        'DataGridTextBoxColumn33
        '
        Me.DataGridTextBoxColumn33.Format = ""
        Me.DataGridTextBoxColumn33.FormatInfo = Nothing
        Me.DataGridTextBoxColumn33.HeaderText = "Case#"
        Me.DataGridTextBoxColumn33.MappingName = "CaseNum"
        Me.DataGridTextBoxColumn33.Width = 0
        '
        'DataGridTextBoxColumn34
        '
        Me.DataGridTextBoxColumn34.Format = ""
        Me.DataGridTextBoxColumn34.FormatInfo = Nothing
        Me.DataGridTextBoxColumn34.HeaderText = "Contact#"
        Me.DataGridTextBoxColumn34.MappingName = "ContactID"
        Me.DataGridTextBoxColumn34.Width = 0
        '
        'DataGridTextBoxColumn35
        '
        Me.DataGridTextBoxColumn35.Format = ""
        Me.DataGridTextBoxColumn35.FormatInfo = Nothing
        Me.DataGridTextBoxColumn35.HeaderText = "Date"
        Me.DataGridTextBoxColumn35.MappingName = "ConversDate"
        Me.DataGridTextBoxColumn35.Width = 80
        '
        'DataGridTextBoxColumn36
        '
        Me.DataGridTextBoxColumn36.Format = ""
        Me.DataGridTextBoxColumn36.FormatInfo = Nothing
        Me.DataGridTextBoxColumn36.HeaderText = "BriefSummary"
        Me.DataGridTextBoxColumn36.MappingName = "BriefSummary"
        Me.DataGridTextBoxColumn36.Width = 250
        '
        'DataGridTextBoxColumn37
        '
        Me.DataGridTextBoxColumn37.Format = ""
        Me.DataGridTextBoxColumn37.FormatInfo = Nothing
        Me.DataGridTextBoxColumn37.HeaderText = "Staff"
        Me.DataGridTextBoxColumn37.MappingName = "StaffName"
        Me.DataGridTextBoxColumn37.Width = 120
        '
        'DataGridTextBoxColumn24
        '
        Me.DataGridTextBoxColumn24.Format = ""
        Me.DataGridTextBoxColumn24.FormatInfo = Nothing
        Me.DataGridTextBoxColumn24.HeaderText = "Case"
        Me.DataGridTextBoxColumn24.MappingName = "CaseName"
        Me.DataGridTextBoxColumn24.Width = 120
        '
        'DataGridTableStyle6
        '
        Me.DataGridTableStyle6.AlternatingBackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.DataGridTableStyle6.DataGrid = Me.grdSecond1
        Me.DataGridTableStyle6.ForeColor = System.Drawing.Color.DarkGreen
        Me.DataGridTableStyle6.GridColumnStyles.AddRange(New System.Windows.Forms.DataGridColumnStyle() {Me.DataGridTextBoxColumn41, Me.DataGridTextBoxColumn28, Me.DataGridTextBoxColumn29, Me.DataGridTextBoxColumn30, Me.DataGridTextBoxColumn31, Me.DataGridTextBoxColumn54, Me.DataGridTextBoxColumn59, Me.DataGridTextBoxColumn68})
        Me.DataGridTableStyle6.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.DataGridTableStyle6.MappingName = "GetRegistrants2"
        Me.DataGridTableStyle6.RowHeaderWidth = 15
        Me.DataGridTableStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight
        '
        'DataGridTextBoxColumn41
        '
        Me.DataGridTextBoxColumn41.Format = ""
        Me.DataGridTextBoxColumn41.FormatInfo = Nothing
        Me.DataGridTextBoxColumn41.HeaderText = "RegistrationID"
        Me.DataGridTextBoxColumn41.MappingName = "RegistrationID"
        Me.DataGridTextBoxColumn41.Width = 0
        '
        'DataGridTextBoxColumn28
        '
        Me.DataGridTextBoxColumn28.Format = ""
        Me.DataGridTextBoxColumn28.FormatInfo = Nothing
        Me.DataGridTextBoxColumn28.HeaderText = "ContactNum"
        Me.DataGridTextBoxColumn28.MappingName = "ContactNum"
        Me.DataGridTextBoxColumn28.Width = 0
        '
        'DataGridTextBoxColumn29
        '
        Me.DataGridTextBoxColumn29.Format = "d"
        Me.DataGridTextBoxColumn29.FormatInfo = Nothing
        Me.DataGridTextBoxColumn29.HeaderText = "EventDate"
        Me.DataGridTextBoxColumn29.MappingName = "EventDate"
        Me.DataGridTextBoxColumn29.Width = 75
        '
        'DataGridTextBoxColumn30
        '
        Me.DataGridTextBoxColumn30.Format = ""
        Me.DataGridTextBoxColumn30.FormatInfo = Nothing
        Me.DataGridTextBoxColumn30.HeaderText = "Event Name"
        Me.DataGridTextBoxColumn30.MappingName = "EventName"
        Me.DataGridTextBoxColumn30.Width = 275
        '
        'DataGridTextBoxColumn31
        '
        Me.DataGridTextBoxColumn31.Format = ""
        Me.DataGridTextBoxColumn31.FormatInfo = Nothing
        Me.DataGridTextBoxColumn31.HeaderText = "Region"
        Me.DataGridTextBoxColumn31.MappingName = "SatelliteRegion"
        Me.DataGridTextBoxColumn31.Width = 75
        '
        'DataGridTextBoxColumn54
        '
        Me.DataGridTextBoxColumn54.Format = ""
        Me.DataGridTextBoxColumn54.FormatInfo = Nothing
        Me.DataGridTextBoxColumn54.HeaderText = "Attended"
        Me.DataGridTextBoxColumn54.MappingName = "Attended"
        Me.DataGridTextBoxColumn54.Width = 75
        '
        'DataGridTextBoxColumn59
        '
        Me.DataGridTextBoxColumn59.Format = ""
        Me.DataGridTextBoxColumn59.FormatInfo = Nothing
        Me.DataGridTextBoxColumn59.HeaderText = "AmountDue"
        Me.DataGridTextBoxColumn59.MappingName = "AmountDue"
        Me.DataGridTextBoxColumn59.Width = 75
        '
        'DataGridTextBoxColumn68
        '
        Me.DataGridTextBoxColumn68.Format = ""
        Me.DataGridTextBoxColumn68.FormatInfo = Nothing
        Me.DataGridTextBoxColumn68.HeaderText = "Order#"
        Me.DataGridTextBoxColumn68.MappingName = "OrderNum"
        Me.DataGridTextBoxColumn68.Width = 75
        '
        'DataGridTableStyle8
        '
        Me.DataGridTableStyle8.AlternatingBackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.DataGridTableStyle8.DataGrid = Me.grdSecond1
        Me.DataGridTableStyle8.ForeColor = System.Drawing.Color.DarkGreen
        Me.DataGridTableStyle8.GridColumnStyles.AddRange(New System.Windows.Forms.DataGridColumnStyle() {Me.DataGridTextBoxColumn13, Me.DataGridTextBoxColumn18, Me.DataGridTextBoxColumn14, Me.DataGridTextBoxColumn15, Me.DataGridTextBoxColumn16})
        Me.DataGridTableStyle8.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.DataGridTableStyle8.MappingName = "FoundOrgCasesNonGrant"
        Me.DataGridTableStyle8.RowHeaderWidth = 15
        Me.DataGridTableStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight
        '
        'DataGridTableStyle9
        '
        Me.DataGridTableStyle9.DataGrid = Me.grdSecond1
        Me.DataGridTableStyle9.GridColumnStyles.AddRange(New System.Windows.Forms.DataGridColumnStyle() {Me.DataGridTextBoxColumn47, Me.DataGridTextBoxColumn53, Me.DataGridTextBoxColumn48})
        Me.DataGridTableStyle9.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.DataGridTableStyle9.MappingName = "GetOrgStories"
        Me.DataGridTableStyle9.RowHeaderWidth = 15
        '
        'DataGridTextBoxColumn47
        '
        Me.DataGridTextBoxColumn47.Format = ""
        Me.DataGridTextBoxColumn47.FormatInfo = Nothing
        Me.DataGridTextBoxColumn47.HeaderText = "StoryID"
        Me.DataGridTextBoxColumn47.MappingName = "StoryID"
        Me.DataGridTextBoxColumn47.Width = 0
        '
        'DataGridTextBoxColumn53
        '
        Me.DataGridTextBoxColumn53.Format = "d"
        Me.DataGridTextBoxColumn53.FormatInfo = Nothing
        Me.DataGridTextBoxColumn53.HeaderText = "Date"
        Me.DataGridTextBoxColumn53.MappingName = "CreateDate"
        Me.DataGridTextBoxColumn53.Width = 75
        '
        'DataGridTextBoxColumn48
        '
        Me.DataGridTextBoxColumn48.Format = ""
        Me.DataGridTextBoxColumn48.FormatInfo = Nothing
        Me.DataGridTextBoxColumn48.HeaderText = "Headline"
        Me.DataGridTextBoxColumn48.MappingName = "Headline"
        Me.DataGridTextBoxColumn48.Width = 250
        '
        'DataGridTableStyle10
        '
        Me.DataGridTableStyle10.DataGrid = Me.grdSecond1
        Me.DataGridTableStyle10.GridColumnStyles.AddRange(New System.Windows.Forms.DataGridColumnStyle() {Me.DataGridTextBoxColumn49, Me.DataGridTextBoxColumn51, Me.DataGridTextBoxColumn50, Me.DataGridTextBoxColumn52, Me.DataGridTextBoxColumn57})
        Me.DataGridTableStyle10.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.DataGridTableStyle10.MappingName = "GetOrgAlerts"
        Me.DataGridTableStyle10.RowHeaderWidth = 15
        '
        'DataGridTextBoxColumn49
        '
        Me.DataGridTextBoxColumn49.Format = ""
        Me.DataGridTextBoxColumn49.FormatInfo = Nothing
        Me.DataGridTextBoxColumn49.HeaderText = "ID"
        Me.DataGridTextBoxColumn49.MappingName = "AlertID"
        Me.DataGridTextBoxColumn49.Width = 0
        '
        'DataGridTextBoxColumn51
        '
        Me.DataGridTextBoxColumn51.Format = "d"
        Me.DataGridTextBoxColumn51.FormatInfo = Nothing
        Me.DataGridTextBoxColumn51.HeaderText = "Date"
        Me.DataGridTextBoxColumn51.MappingName = "CreateDate"
        Me.DataGridTextBoxColumn51.Width = 75
        '
        'DataGridTextBoxColumn50
        '
        Me.DataGridTextBoxColumn50.Format = ""
        Me.DataGridTextBoxColumn50.FormatInfo = Nothing
        Me.DataGridTextBoxColumn50.HeaderText = "Type of Alert"
        Me.DataGridTextBoxColumn50.MappingName = "Type"
        Me.DataGridTextBoxColumn50.Width = 75
        '
        'DataGridTextBoxColumn52
        '
        Me.DataGridTextBoxColumn52.Format = ""
        Me.DataGridTextBoxColumn52.FormatInfo = Nothing
        Me.DataGridTextBoxColumn52.HeaderText = "Headline"
        Me.DataGridTextBoxColumn52.MappingName = "Headline"
        Me.DataGridTextBoxColumn52.Width = 200
        '
        'DataGridTextBoxColumn57
        '
        Me.DataGridTextBoxColumn57.Format = "d"
        Me.DataGridTextBoxColumn57.FormatInfo = Nothing
        Me.DataGridTextBoxColumn57.HeaderText = "Cancelled"
        Me.DataGridTextBoxColumn57.MappingName = "CancelDate"
        Me.DataGridTextBoxColumn57.Width = 75
        '
        'TableStyleGrants
        '
        Me.TableStyleGrants.DataGrid = Me.grdSecond1
        Me.TableStyleGrants.GridColumnStyles.AddRange(New System.Windows.Forms.DataGridColumnStyle() {Me.DataGridTextBoxColumn61, Me.DataGridTextBoxColumn62, Me.DataGridTextBoxColumn63, Me.DataGridTextBoxColumn64, Me.DataGridTextBoxColumn65, Me.DataGridTextBoxColumn67})
        Me.TableStyleGrants.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.TableStyleGrants.MappingName = "FoundOrgGrants"
        Me.TableStyleGrants.RowHeaderWidth = 30
        '
        'DataGridTextBoxColumn61
        '
        Me.DataGridTextBoxColumn61.Format = ""
        Me.DataGridTextBoxColumn61.FormatInfo = Nothing
        Me.DataGridTextBoxColumn61.HeaderText = "GrantID"
        Me.DataGridTextBoxColumn61.MappingName = "GrantIDTxt"
        Me.DataGridTextBoxColumn61.Width = 0
        '
        'DataGridTextBoxColumn62
        '
        Me.DataGridTextBoxColumn62.Format = ""
        Me.DataGridTextBoxColumn62.FormatInfo = Nothing
        Me.DataGridTextBoxColumn62.HeaderText = "Type Of Grant"
        Me.DataGridTextBoxColumn62.MappingName = "TypeofGrant"
        Me.DataGridTextBoxColumn62.Width = 150
        '
        'DataGridTextBoxColumn63
        '
        Me.DataGridTextBoxColumn63.Format = ""
        Me.DataGridTextBoxColumn63.FormatInfo = Nothing
        Me.DataGridTextBoxColumn63.HeaderText = "Amount"
        Me.DataGridTextBoxColumn63.MappingName = "Amount"
        Me.DataGridTextBoxColumn63.Width = 75
        '
        'DataGridTextBoxColumn64
        '
        Me.DataGridTextBoxColumn64.Format = ""
        Me.DataGridTextBoxColumn64.FormatInfo = Nothing
        Me.DataGridTextBoxColumn64.HeaderText = "Case Name"
        Me.DataGridTextBoxColumn64.MappingName = "CaseName"
        Me.DataGridTextBoxColumn64.Width = 200
        '
        'DataGridTextBoxColumn65
        '
        Me.DataGridTextBoxColumn65.Format = ""
        Me.DataGridTextBoxColumn65.FormatInfo = Nothing
        Me.DataGridTextBoxColumn65.HeaderText = "Latest Activity"
        Me.DataGridTextBoxColumn65.MappingName = "GrantLatest"
        Me.DataGridTextBoxColumn65.Width = 200
        '
        'DataGridTextBoxColumn67
        '
        Me.DataGridTextBoxColumn67.Format = ""
        Me.DataGridTextBoxColumn67.FormatInfo = Nothing
        Me.DataGridTextBoxColumn67.HeaderText = "Next Report Due"
        Me.DataGridTextBoxColumn67.MappingName = "NextReportDue"
        Me.DataGridTextBoxColumn67.Width = 120
        '
        'tbMainGrid
        '
        Me.tbMainGrid.Controls.Add(Me.pgGridOrg)
        Me.tbMainGrid.Controls.Add(Me.pgGridContact)
        Me.tbMainGrid.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbMainGrid.Location = New System.Drawing.Point(9, 3)
        Me.tbMainGrid.Name = "tbMainGrid"
        Me.tbMainGrid.SelectedIndex = 0
        Me.tbMainGrid.Size = New System.Drawing.Size(708, 24)
        Me.tbMainGrid.TabIndex = 218
        Me.tbMainGrid.Tag = "Contact"
        '
        'pgGridOrg
        '
        Me.pgGridOrg.Location = New System.Drawing.Point(4, 22)
        Me.pgGridOrg.Name = "pgGridOrg"
        Me.pgGridOrg.Size = New System.Drawing.Size(700, 0)
        Me.pgGridOrg.TabIndex = 0
        Me.pgGridOrg.Tag = "Org"
        Me.pgGridOrg.Text = "Organizations Found"
        '
        'pgGridContact
        '
        Me.pgGridContact.Location = New System.Drawing.Point(4, 22)
        Me.pgGridContact.Name = "pgGridContact"
        Me.pgGridContact.Size = New System.Drawing.Size(700, 0)
        Me.pgGridContact.TabIndex = 1
        Me.pgGridContact.Tag = "Contact"
        Me.pgGridContact.Text = "Contacts Found"
        '
        'lblSelectionID
        '
        Me.lblSelectionID.AcceptsReturn = True
        Me.lblSelectionID.BackColor = System.Drawing.SystemColors.Control
        Me.lblSelectionID.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lblSelectionID.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSelectionID.ForeColor = System.Drawing.SystemColors.GrayText
        Me.lblSelectionID.Location = New System.Drawing.Point(707, 298)
        Me.lblSelectionID.Name = "lblSelectionID"
        Me.lblSelectionID.ReadOnly = True
        Me.lblSelectionID.Size = New System.Drawing.Size(116, 14)
        Me.lblSelectionID.TabIndex = 209
        Me.lblSelectionID.Text = "ID#"
        '
        'chkDetail
        '
        Me.chkDetail.Location = New System.Drawing.Point(16, 298)
        Me.chkDetail.Name = "chkDetail"
        Me.chkDetail.Size = New System.Drawing.Size(16, 16)
        Me.chkDetail.TabIndex = 2
        Me.ToolTip1.SetToolTip(Me.chkDetail, "Uncheck this box to speed up the search.")
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Location = New System.Drawing.Point(12, 295)
        Me.Label4.MinimumSize = New System.Drawing.Size(300, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(300, 20)
        Me.Label4.TabIndex = 5
        Me.Label4.Text = " Show items related to selected Organization or Contact"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.SystemColors.InactiveBorder
        Me.GroupBox1.Controls.Add(Me.cntGrants)
        Me.GroupBox1.Controls.Add(Me.rbGrants)
        Me.GroupBox1.Controls.Add(Me.cntAlerts)
        Me.GroupBox1.Controls.Add(Me.rbALerts)
        Me.GroupBox1.Controls.Add(Me.cntStories)
        Me.GroupBox1.Controls.Add(Me.rbStories)
        Me.GroupBox1.Controls.Add(Me.cntCases)
        Me.GroupBox1.Controls.Add(Me.CntContacts)
        Me.GroupBox1.Controls.Add(Me.rbCase1Reg2)
        Me.GroupBox1.Controls.Add(Me.rbContact1Conver2)
        Me.GroupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GroupBox1.Location = New System.Drawing.Point(10, 379)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(163, 115)
        Me.GroupBox1.TabIndex = 208
        Me.GroupBox1.TabStop = False
        Me.ToolTip1.SetToolTip(Me.GroupBox1, "Uncheck this box to speed up the search.")
        '
        'cntGrants
        '
        Me.cntGrants.Location = New System.Drawing.Point(132, 45)
        Me.cntGrants.Name = "cntGrants"
        Me.cntGrants.Size = New System.Drawing.Size(25, 14)
        Me.cntGrants.TabIndex = 19
        Me.cntGrants.Text = "0"
        Me.cntGrants.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'rbGrants
        '
        Me.rbGrants.Location = New System.Drawing.Point(8, 41)
        Me.rbGrants.Name = "rbGrants"
        Me.rbGrants.Size = New System.Drawing.Size(93, 18)
        Me.rbGrants.TabIndex = 18
        Me.rbGrants.Tag = "FoundOrgGrants"
        Me.rbGrants.Text = "Grants"
        '
        'cntAlerts
        '
        Me.cntAlerts.Location = New System.Drawing.Point(132, 83)
        Me.cntAlerts.Name = "cntAlerts"
        Me.cntAlerts.Size = New System.Drawing.Size(25, 14)
        Me.cntAlerts.TabIndex = 17
        Me.cntAlerts.Text = "0"
        Me.cntAlerts.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'rbALerts
        '
        Me.rbALerts.Location = New System.Drawing.Point(8, 79)
        Me.rbALerts.Name = "rbALerts"
        Me.rbALerts.Size = New System.Drawing.Size(88, 18)
        Me.rbALerts.TabIndex = 16
        Me.rbALerts.Tag = "GetOrgAlerts"
        Me.rbALerts.Text = "Alerts"
        '
        'cntStories
        '
        Me.cntStories.Location = New System.Drawing.Point(132, 62)
        Me.cntStories.Name = "cntStories"
        Me.cntStories.Size = New System.Drawing.Size(25, 14)
        Me.cntStories.TabIndex = 15
        Me.cntStories.Text = "0"
        Me.cntStories.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'rbStories
        '
        Me.rbStories.Location = New System.Drawing.Point(8, 60)
        Me.rbStories.Name = "rbStories"
        Me.rbStories.Size = New System.Drawing.Size(93, 18)
        Me.rbStories.TabIndex = 14
        Me.rbStories.Tag = "GetOrgStories"
        Me.rbStories.Text = "Stories"
        '
        'cntCases
        '
        Me.cntCases.Location = New System.Drawing.Point(132, 25)
        Me.cntCases.Name = "cntCases"
        Me.cntCases.Size = New System.Drawing.Size(25, 14)
        Me.cntCases.TabIndex = 13
        Me.cntCases.Text = "0"
        Me.cntCases.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'CntContacts
        '
        Me.CntContacts.Location = New System.Drawing.Point(132, 4)
        Me.CntContacts.Name = "CntContacts"
        Me.CntContacts.Size = New System.Drawing.Size(25, 14)
        Me.CntContacts.TabIndex = 12
        Me.CntContacts.Text = "0"
        Me.CntContacts.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'rbCase1Reg2
        '
        Me.rbCase1Reg2.Location = New System.Drawing.Point(8, 22)
        Me.rbCase1Reg2.Name = "rbCase1Reg2"
        Me.rbCase1Reg2.Size = New System.Drawing.Size(125, 18)
        Me.rbCase1Reg2.TabIndex = 13
        Me.rbCase1Reg2.Tag = "GetRegistrants2"
        Me.rbCase1Reg2.Text = "Cases"
        '
        'rbContact1Conver2
        '
        Me.rbContact1Conver2.Checked = True
        Me.rbContact1Conver2.Location = New System.Drawing.Point(8, 1)
        Me.rbContact1Conver2.Name = "rbContact1Conver2"
        Me.rbContact1Conver2.Size = New System.Drawing.Size(130, 20)
        Me.rbContact1Conver2.TabIndex = 12
        Me.rbContact1Conver2.TabStop = True
        Me.rbContact1Conver2.Tag = "GetConversations"
        Me.rbContact1Conver2.Text = "Contacts"
        '
        'StatusBar1
        '
        Me.StatusBar1.Location = New System.Drawing.Point(0, 597)
        Me.StatusBar1.Name = "StatusBar1"
        Me.StatusBar1.Panels.AddRange(New System.Windows.Forms.StatusBarPanel() {Me.StatusBarPanel1, Me.StatusBarPanelID, Me.StatusBarPanel2})
        Me.StatusBar1.ShowPanels = True
        Me.StatusBar1.Size = New System.Drawing.Size(1148, 22)
        Me.StatusBar1.TabIndex = 207
        '
        'StatusBarPanel1
        '
        Me.StatusBarPanel1.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents
        Me.StatusBarPanel1.BorderStyle = System.Windows.Forms.StatusBarPanelBorderStyle.None
        Me.StatusBarPanel1.MinWidth = 200
        Me.StatusBarPanel1.Name = "StatusBarPanel1"
        Me.StatusBarPanel1.Text = "Ready"
        Me.StatusBarPanel1.Width = 200
        '
        'StatusBarPanelID
        '
        Me.StatusBarPanelID.Alignment = System.Windows.Forms.HorizontalAlignment.Center
        Me.StatusBarPanelID.MinWidth = 200
        Me.StatusBarPanelID.Name = "StatusBarPanelID"
        Me.StatusBarPanelID.Text = "Org ID:"
        Me.StatusBarPanelID.Width = 200
        '
        'StatusBarPanel2
        '
        Me.StatusBarPanel2.Alignment = System.Windows.Forms.HorizontalAlignment.Right
        Me.StatusBarPanel2.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring
        Me.StatusBarPanel2.BorderStyle = System.Windows.Forms.StatusBarPanelBorderStyle.None
        Me.StatusBarPanel2.Name = "StatusBarPanel2"
        Me.StatusBarPanel2.Text = "Use this window to find an Organization or Contact.  Click Show Related Items to " & _
    "hide or show related information in the lower grid."
        Me.StatusBarPanel2.Width = 731
        '
        'PanelOrg
        '
        Me.PanelOrg.BackColor = System.Drawing.SystemColors.InactiveBorder
        Me.PanelOrg.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.PanelOrg.Controls.Add(Me.Label7)
        Me.PanelOrg.Controls.Add(Me.Label6)
        Me.PanelOrg.Controls.Add(Me.cboField)
        Me.PanelOrg.Controls.Add(Me.Label5)
        Me.PanelOrg.Controls.Add(Me.Label2)
        Me.PanelOrg.Controls.Add(Me.chkActive)
        Me.PanelOrg.Controls.Add(Me.cboType)
        Me.PanelOrg.Controls.Add(Me.GroupBox1)
        Me.PanelOrg.Controls.Add(Me.cboRegion)
        Me.PanelOrg.Controls.Add(Me.btnSearch)
        Me.PanelOrg.Controls.Add(Me.txtSearch)
        Me.PanelOrg.Controls.Add(Me.chkWild)
        Me.PanelOrg.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PanelOrg.Location = New System.Drawing.Point(3, 26)
        Me.PanelOrg.Name = "PanelOrg"
        Me.PanelOrg.Size = New System.Drawing.Size(173, 502)
        Me.PanelOrg.TabIndex = 1
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.SystemColors.InactiveBorder
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.Maroon
        Me.Label7.Location = New System.Drawing.Point(10, 137)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(138, 17)
        Me.Label7.TabIndex = 225
        Me.Label7.Text = "Type of Organization"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.SystemColors.InactiveBorder
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Maroon
        Me.Label6.Location = New System.Drawing.Point(10, 182)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(138, 17)
        Me.Label6.TabIndex = 224
        Me.Label6.Text = "Satellite Region"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cboField
        '
        Me.cboField.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboField.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboField.FormattingEnabled = True
        Me.cboField.Items.AddRange(New Object() {"Name", "City", "County", "Culture", "Denomination", "Email", "Phone", "Program", "Street", "Zip", "----------", "EIN", "ID#"})
        Me.cboField.Location = New System.Drawing.Point(11, 67)
        Me.cboField.Name = "cboField"
        Me.cboField.RestrictContentToListItems = True
        Me.cboField.Size = New System.Drawing.Size(142, 21)
        Me.cboField.TabIndex = 2
        Me.cboField.Tag = "Field to Search"
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.SystemColors.InactiveBorder
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Maroon
        Me.Label5.Location = New System.Drawing.Point(3, 47)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(138, 17)
        Me.Label5.TabIndex = 222
        Me.Label5.Text = "Field to Search"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.SystemColors.InactiveBorder
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Maroon
        Me.Label2.Location = New System.Drawing.Point(5, 359)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(138, 17)
        Me.Label2.TabIndex = 221
        Me.Label2.Text = "Related items"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'chkActive
        '
        Me.chkActive.Checked = True
        Me.chkActive.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkActive.Location = New System.Drawing.Point(31, 295)
        Me.chkActive.Name = "chkActive"
        Me.chkActive.Size = New System.Drawing.Size(95, 18)
        Me.chkActive.TabIndex = 10
        Me.chkActive.Text = "Active"
        Me.ToolTip1.SetToolTip(Me.chkActive, "Unchecked will search only Inactive Organizations and Contacts")
        '
        'cboType
        '
        Me.cboType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboType.FormattingEnabled = True
        Me.cboType.Items.AddRange(New Object() {"All Congregations", "------", "Church", "Synagogue", "Mosque", "Temple", "Denomination", "------", "Business", "Community", "Government", "Individuals", "Office", "Organization", "Other", "Press", "School", "------", "All Types of Organizations", "------", "National Learning"})
        Me.cboType.Location = New System.Drawing.Point(10, 154)
        Me.cboType.Name = "cboType"
        Me.cboType.RestrictContentToListItems = True
        Me.cboType.Size = New System.Drawing.Size(145, 21)
        Me.cboType.TabIndex = 4
        Me.cboType.Tag = "Type of Organization"
        Me.cboType.Text = "Type"
        Me.ToolTip1.SetToolTip(Me.cboType, "Type of Organization.  Note: 'Congregations' includes Churches, Synagogues, Mosqu" & _
        "es, Temples, and Denominations.")
        '
        'cboRegion
        '
        Me.cboRegion.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboRegion.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboRegion.ForeColor = System.Drawing.Color.Black
        Me.cboRegion.FormattingEnabled = True
        Me.cboRegion.Location = New System.Drawing.Point(10, 199)
        Me.cboRegion.Name = "cboRegion"
        Me.cboRegion.RestrictContentToListItems = True
        Me.cboRegion.Size = New System.Drawing.Size(142, 21)
        Me.cboRegion.TabIndex = 5
        Me.cboRegion.Tag = "Satellite Region"
        Me.cboRegion.Text = "Satellite Region"
        Me.ToolTip1.SetToolTip(Me.cboRegion, "Satellite Region where Organization is located.")
        '
        'btnSearch
        '
        Me.btnSearch.BackColor = System.Drawing.SystemColors.Control
        Me.btnSearch.Location = New System.Drawing.Point(36, 101)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(75, 25)
        Me.btnSearch.TabIndex = 3
        Me.btnSearch.Text = "Search"
        Me.ToolTip1.SetToolTip(Me.btnSearch, "Begin Search")
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'txtSearch
        '
        Me.txtSearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.txtSearch.Location = New System.Drawing.Point(11, 14)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(142, 20)
        Me.txtSearch.TabIndex = 0
        Me.txtSearch.Text = "enter search text"
        Me.ToolTip1.SetToolTip(Me.txtSearch, "use * as wildcard")
        '
        'chkWild
        '
        Me.chkWild.Checked = True
        Me.chkWild.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkWild.Location = New System.Drawing.Point(31, 273)
        Me.chkWild.Name = "chkWild"
        Me.chkWild.Size = New System.Drawing.Size(112, 16)
        Me.chkWild.TabIndex = 8
        Me.chkWild.Text = "Wildcards On"
        Me.ToolTip1.SetToolTip(Me.chkWild, "Places a wildcard before and after your search text. ")
        '
        'pgLURegion
        '
        Me.pgLURegion.Location = New System.Drawing.Point(4, 22)
        Me.pgLURegion.Name = "pgLURegion"
        Me.pgLURegion.Size = New System.Drawing.Size(865, 547)
        Me.pgLURegion.TabIndex = 2
        Me.pgLURegion.Text = "     CHECK SERVICE AREA     "
        Me.pgLURegion.ToolTipText = "Find out which City, County or Zip is in what Region."
        '
        'MenuItem4
        '
        Me.MenuItem4.Index = 2
        Me.MenuItem4.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.miCaseReport, Me.miStaffRpt})
        Me.MenuItem4.Text = "Reports"
        '
        'miCaseReport
        '
        Me.miCaseReport.Checked = True
        Me.miCaseReport.Enabled = False
        Me.miCaseReport.Index = 0
        Me.miCaseReport.Text = "Case Report"
        '
        'miStaffRpt
        '
        Me.miStaffRpt.Index = 1
        Me.miStaffRpt.Text = "Staff && Laity"
        '
        'miGotoContact
        '
        Me.miGotoContact.Index = 1
        Me.miGotoContact.Text = "Contact"
        '
        'miClearSearch
        '
        Me.miClearSearch.Index = 1
        Me.miClearSearch.Text = "Clear Criteria"
        '
        'MenuItem3
        '
        Me.MenuItem3.Index = 3
        Me.MenuItem3.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.miGotoOrg, Me.miGotoContact, Me.miGotoCase, Me.miGotoConversation, Me.miGotoRegistration, Me.miGotoStory, Me.miGotoAlert})
        Me.MenuItem3.Text = "Go to"
        '
        'miGotoOrg
        '
        Me.miGotoOrg.Index = 0
        Me.miGotoOrg.Text = "Organization"
        '
        'miGotoCase
        '
        Me.miGotoCase.Enabled = False
        Me.miGotoCase.Index = 2
        Me.miGotoCase.Text = "Case"
        '
        'miGotoConversation
        '
        Me.miGotoConversation.Enabled = False
        Me.miGotoConversation.Index = 3
        Me.miGotoConversation.Text = "Conversation"
        '
        'miGotoRegistration
        '
        Me.miGotoRegistration.Enabled = False
        Me.miGotoRegistration.Index = 4
        Me.miGotoRegistration.Text = "Registration"
        '
        'miGotoStory
        '
        Me.miGotoStory.Enabled = False
        Me.miGotoStory.Index = 5
        Me.miGotoStory.Text = "Story"
        '
        'miGotoAlert
        '
        Me.miGotoAlert.Enabled = False
        Me.miGotoAlert.Index = 6
        Me.miGotoAlert.Text = "Alert"
        '
        'sqlspSrchOrgs
        '
        Me.sqlspSrchOrgs.CommandText = "[SrchOrgs]"
        Me.sqlspSrchOrgs.CommandType = System.Data.CommandType.StoredProcedure
        Me.sqlspSrchOrgs.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, False, CType(0, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing), New System.Data.SqlClient.SqlParameter("@Name", System.Data.SqlDbType.VarChar, 100), New System.Data.SqlClient.SqlParameter("@City", System.Data.SqlDbType.VarChar, 30), New System.Data.SqlClient.SqlParameter("@Street", System.Data.SqlDbType.VarChar, 50), New System.Data.SqlClient.SqlParameter("@Denom", System.Data.SqlDbType.VarChar, 75), New System.Data.SqlClient.SqlParameter("@Type", System.Data.SqlDbType.VarChar, 50), New System.Data.SqlClient.SqlParameter("@Region", System.Data.SqlDbType.VarChar, 15), New System.Data.SqlClient.SqlParameter("@Program", System.Data.SqlDbType.NVarChar, 1073741823), New System.Data.SqlClient.SqlParameter("@Email", System.Data.SqlDbType.VarChar, 100), New System.Data.SqlClient.SqlParameter("@OrgID", System.Data.SqlDbType.Int, 4), New System.Data.SqlClient.SqlParameter("@Zip", System.Data.SqlDbType.VarChar, 10), New System.Data.SqlClient.SqlParameter("@Active", System.Data.SqlDbType.VarChar), New System.Data.SqlClient.SqlParameter("@Sort", System.Data.SqlDbType.VarChar, 50), New System.Data.SqlClient.SqlParameter("@County", System.Data.SqlDbType.VarChar, 20), New System.Data.SqlClient.SqlParameter("@Culture", System.Data.SqlDbType.VarChar, 30), New System.Data.SqlClient.SqlParameter("@Phone", System.Data.SqlDbType.VarChar, 15)})
        '
        'MenuItem2
        '
        Me.MenuItem2.Index = 1
        Me.MenuItem2.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.miSrchOrg, Me.miClearSearch, Me.MenuItem5})
        Me.MenuItem2.Text = "Search"
        '
        'miSrchOrg
        '
        Me.miSrchOrg.Index = 0
        Me.miSrchOrg.Text = "Begin Search"
        '
        'MenuItem5
        '
        Me.MenuItem5.Enabled = False
        Me.MenuItem5.Index = 2
        Me.MenuItem5.Text = "Include Inactives"
        '
        'MenuItem1
        '
        Me.MenuItem1.Index = 0
        Me.MenuItem1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.miAddOrg, Me.MenuItem8, Me.miCloseForm})
        Me.MenuItem1.Text = "File"
        '
        'miAddOrg
        '
        Me.miAddOrg.Index = 0
        Me.miAddOrg.Text = "&New Organization"
        '
        'MenuItem8
        '
        Me.MenuItem8.Index = 1
        Me.MenuItem8.Text = "Open Regional Search"
        '
        'miCloseForm
        '
        Me.miCloseForm.Index = 2
        Me.miCloseForm.Text = "Close Window"
        '
        'btnNew
        '
        Me.btnNew.BackColor = System.Drawing.Color.PaleGoldenrod
        Me.btnNew.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnNew.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnNew.Image = CType(resources.GetObject("btnNew.Image"), System.Drawing.Image)
        Me.btnNew.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnNew.Location = New System.Drawing.Point(789, 5)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(65, 40)
        Me.btnNew.TabIndex = 214
        Me.btnNew.Text = "New Organization"
        Me.btnNew.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ToolTip1.SetToolTip(Me.btnNew, "Add New Organization ")
        Me.btnNew.UseVisualStyleBackColor = False
        '
        'MMOrgContact
        '
        Me.MMOrgContact.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem1, Me.MenuItem2, Me.MenuItem4, Me.MenuItem3, Me.miHelp})
        '
        'miHelp
        '
        Me.miHelp.Index = 4
        Me.miHelp.Text = "Help"
        '
        'sqlGetCases
        '
        Me.sqlGetCases.CommandText = "dbo.GetCases"
        Me.sqlGetCases.CommandType = System.Data.CommandType.StoredProcedure
        Me.sqlGetCases.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, False, CType(0, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing), New System.Data.SqlClient.SqlParameter("@IDFld", System.Data.SqlDbType.VarChar, 30), New System.Data.SqlClient.SqlParameter("@IDVal", System.Data.SqlDbType.Int, 4), New System.Data.SqlClient.SqlParameter("@Region", System.Data.SqlDbType.VarChar, 30)})
        '
        'sqlGetContacts
        '
        Me.sqlGetContacts.CommandText = "dbo.GetContacts"
        Me.sqlGetContacts.CommandType = System.Data.CommandType.StoredProcedure
        Me.sqlGetContacts.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, False, CType(0, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing), New System.Data.SqlClient.SqlParameter("@IDFld", System.Data.SqlDbType.VarChar, 30, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, ""), New System.Data.SqlClient.SqlParameter("@IDVal", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, "3")})
        '
        'daSrch
        '
        Me.daSrch.SelectCommand = Me.sqlspSrchOrgs
        Me.daSrch.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "SrchOrgs", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("OrgID", "OrgID"), New System.Data.Common.DataColumnMapping("OrgName", "OrgName"), New System.Data.Common.DataColumnMapping("City", "City"), New System.Data.Common.DataColumnMapping("Street", "Street"), New System.Data.Common.DataColumnMapping("Phone", "Phone"), New System.Data.Common.DataColumnMapping("Denomination", "Denomination"), New System.Data.Common.DataColumnMapping("OrgType", "OrgType"), New System.Data.Common.DataColumnMapping("SatelliteRegion", "SatelliteRegion"), New System.Data.Common.DataColumnMapping("Programs", "Programs"), New System.Data.Common.DataColumnMapping("Email", "Email"), New System.Data.Common.DataColumnMapping("Zip", "Zip"), New System.Data.Common.DataColumnMapping("OpenGrants", "OpenGrants")})})
        '
        'sqlGetConversations
        '
        Me.sqlGetConversations.CommandText = "[GetConversations]"
        Me.sqlGetConversations.CommandType = System.Data.CommandType.StoredProcedure
        Me.sqlGetConversations.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, False, CType(0, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing), New System.Data.SqlClient.SqlParameter("@IDFld", System.Data.SqlDbType.VarChar, 30), New System.Data.SqlClient.SqlParameter("@IDVal", System.Data.SqlDbType.Int, 4)})
        '
        'daContact
        '
        Me.daContact.SelectCommand = Me.sqlspSrchContacts
        Me.daContact.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "SrchContact", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("ContactID", "ContactID"), New System.Data.Common.DataColumnMapping("OrgNum", "OrgNum"), New System.Data.Common.DataColumnMapping("Prefix", "Prefix"), New System.Data.Common.DataColumnMapping("FirstName", "FirstName"), New System.Data.Common.DataColumnMapping("Lastname", "Lastname"), New System.Data.Common.DataColumnMapping("Org", "Org"), New System.Data.Common.DataColumnMapping("Staff", "Staff"), New System.Data.Common.DataColumnMapping("PrimaryContact", "PrimaryContact"), New System.Data.Common.DataColumnMapping("JobTitle", "JobTitle"), New System.Data.Common.DataColumnMapping("Street1", "Street1"), New System.Data.Common.DataColumnMapping("City", "City"), New System.Data.Common.DataColumnMapping("Zip", "Zip"), New System.Data.Common.DataColumnMapping("email", "email"), New System.Data.Common.DataColumnMapping("SatelliteRegion", "SatelliteRegion"), New System.Data.Common.DataColumnMapping("county", "county"), New System.Data.Common.DataColumnMapping("orgtype", "orgtype"), New System.Data.Common.DataColumnMapping("OrgPhone", "OrgPhone"), New System.Data.Common.DataColumnMapping("HomePhone", "HomePhone"), New System.Data.Common.DataColumnMapping("CellPhone", "CellPhone")})})
        '
        'sqlspSrchContacts
        '
        Me.sqlspSrchContacts.CommandText = "dbo.SrchContacts"
        Me.sqlspSrchContacts.CommandType = System.Data.CommandType.StoredProcedure
        Me.sqlspSrchContacts.Connection = Me.SqlConnection1
        Me.sqlspSrchContacts.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, False, CType(0, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing), New System.Data.SqlClient.SqlParameter("@FldName", System.Data.SqlDbType.VarChar, 30, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, ""), New System.Data.SqlClient.SqlParameter("@FldVal", System.Data.SqlDbType.VarChar, 30, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, ""), New System.Data.SqlClient.SqlParameter("@Region", System.Data.SqlDbType.VarChar, 15, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, ""), New System.Data.SqlClient.SqlParameter("@Phone", System.Data.SqlDbType.VarChar, 15, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, ""), New System.Data.SqlClient.SqlParameter("@Type", System.Data.SqlDbType.VarChar, 50, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, ""), New System.Data.SqlClient.SqlParameter("@Sort", System.Data.SqlDbType.VarChar, 100, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, ""), New System.Data.SqlClient.SqlParameter("@colon", System.Data.SqlDbType.VarChar, 3, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, ""), New System.Data.SqlClient.SqlParameter("@church", System.Data.SqlDbType.VarChar, 12, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, ""), New System.Data.SqlClient.SqlParameter("@syn", System.Data.SqlDbType.VarChar, 12, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, ""), New System.Data.SqlClient.SqlParameter("@mosque", System.Data.SqlDbType.VarChar, 12, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, ""), New System.Data.SqlClient.SqlParameter("@temple", System.Data.SqlDbType.VarChar, 12, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, ""), New System.Data.SqlClient.SqlParameter("@denom", System.Data.SqlDbType.VarChar, 12, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, ""), New System.Data.SqlClient.SqlParameter("@Active", System.Data.SqlDbType.VarChar, 10, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, "")})
        '
        'SqlConnection1
        '
        Me.SqlConnection1.ConnectionString = "Data Source=SOLOMON2008\SOLOMON2008;Initial Catalog=InfoCtr_be;Integrated Securit" & _
    "y=True"
        Me.SqlConnection1.FireInfoMessageEventOnUserErrors = False
        '
        'sqlGetRegistrations
        '
        Me.sqlGetRegistrations.CommandText = "dbo.GetRegistrants2"
        Me.sqlGetRegistrations.CommandType = System.Data.CommandType.StoredProcedure
        Me.sqlGetRegistrations.Connection = Me.SqlConnection1
        Me.sqlGetRegistrations.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, False, CType(0, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing), New System.Data.SqlClient.SqlParameter("@IDFld", System.Data.SqlDbType.VarChar, 30), New System.Data.SqlClient.SqlParameter("@IDVal", System.Data.SqlDbType.Int, 4)})
        '
        'lblCurrentMain
        '
        Me.lblCurrentMain.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCurrentMain.ForeColor = System.Drawing.SystemColors.GrayText
        Me.lblCurrentMain.Location = New System.Drawing.Point(920, 9)
        Me.lblCurrentMain.Name = "lblCurrentMain"
        Me.lblCurrentMain.Size = New System.Drawing.Size(75, 21)
        Me.lblCurrentMain.TabIndex = 212
        Me.lblCurrentMain.Text = "Org ID #"
        Me.lblCurrentMain.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'fldMainCurrentID
        '
        Me.fldMainCurrentID.AcceptsReturn = True
        Me.fldMainCurrentID.BackColor = System.Drawing.SystemColors.Control
        Me.fldMainCurrentID.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.fldMainCurrentID.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fldMainCurrentID.ForeColor = System.Drawing.SystemColors.GrayText
        Me.fldMainCurrentID.Location = New System.Drawing.Point(1007, 9)
        Me.fldMainCurrentID.Name = "fldMainCurrentID"
        Me.fldMainCurrentID.ReadOnly = True
        Me.fldMainCurrentID.Size = New System.Drawing.Size(55, 14)
        Me.fldMainCurrentID.TabIndex = 213
        Me.fldMainCurrentID.Text = "ID#"
        Me.fldMainCurrentID.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'pnlLuOrgContact
        '
        Me.pnlLuOrgContact.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlLuOrgContact.BackColor = System.Drawing.SystemColors.Control
        Me.pnlLuOrgContact.Controls.Add(Me.Label1)
        Me.pnlLuOrgContact.Controls.Add(Me.Label3)
        Me.pnlLuOrgContact.Controls.Add(Me.pnlMainGrid)
        Me.pnlLuOrgContact.Controls.Add(Me.PanelOrg)
        Me.pnlLuOrgContact.Location = New System.Drawing.Point(9, 50)
        Me.pnlLuOrgContact.Name = "pnlLuOrgContact"
        Me.pnlLuOrgContact.Size = New System.Drawing.Size(1078, 569)
        Me.pnlLuOrgContact.TabIndex = 0
        Me.pnlLuOrgContact.Tag = "OrgContact"
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(185, 4)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(138, 17)
        Me.Label1.TabIndex = 221
        Me.Label1.Text = "Results"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(10, 4)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(138, 17)
        Me.Label3.TabIndex = 220
        Me.Label3.Text = "Search Criteria"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'sqlGetOrgStories
        '
        Me.sqlGetOrgStories.CommandText = "dbo.GetOrgStories"
        Me.sqlGetOrgStories.CommandType = System.Data.CommandType.StoredProcedure
        Me.sqlGetOrgStories.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, False, CType(0, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing), New System.Data.SqlClient.SqlParameter("@IDVal", System.Data.SqlDbType.Int, 4), New System.Data.SqlClient.SqlParameter("@IDFld", System.Data.SqlDbType.VarChar, 15)})
        '
        'sqlGetOrgAlerts
        '
        Me.sqlGetOrgAlerts.CommandText = "dbo.GetOrgAlerts"
        Me.sqlGetOrgAlerts.CommandType = System.Data.CommandType.StoredProcedure
        Me.sqlGetOrgAlerts.Connection = Me.SqlConnection1
        Me.sqlGetOrgAlerts.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, False, CType(0, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing), New System.Data.SqlClient.SqlParameter("@IDVal", System.Data.SqlDbType.Int, 4), New System.Data.SqlClient.SqlParameter("@IDFld", System.Data.SqlDbType.VarChar, 15)})
        '
        'DsSrchContacts1
        '
        Me.DsSrchContacts1.DataSetName = "dsSrchContacts"
        Me.DsSrchContacts1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'sqlGetGrants
        '
        Me.sqlGetGrants.CommandText = "dbo.GetGrants"
        Me.sqlGetGrants.CommandType = System.Data.CommandType.StoredProcedure
        Me.sqlGetGrants.Connection = Me.SqlConnection1
        Me.sqlGetGrants.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@IDVal", System.Data.SqlDbType.Int, 4), New System.Data.SqlClient.SqlParameter("@IDFld", System.Data.SqlDbType.VarChar, 6, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, "OrgNum")})
        '
        'frmSrchOrg
        '
        Me.AcceptButton = Me.btnSearch
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(1148, 619)
        Me.Controls.Add(Me.StatusBar1)
        Me.Controls.Add(Me.btnNew)
        Me.Controls.Add(Me.lblCurrentMain)
        Me.Controls.Add(Me.fldMainCurrentID)
        Me.Controls.Add(Me.btnHelpOrg)
        Me.Controls.Add(Me.pnlLuOrgContact)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Menu = Me.MMOrgContact
        Me.Name = "frmSrchOrg"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Tag = "SrchOrg"
        Me.Text = "FIND ORGANIZATION or CONTACT"
        Me.pnlMainGrid.ResumeLayout(False)
        Me.pnlMainGrid.PerformLayout()
        CType(Me.dsSrchOrgs, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdMain, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdSecond1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tbMainGrid.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        CType(Me.StatusBarPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.StatusBarPanelID, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.StatusBarPanel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelOrg.ResumeLayout(False)
        Me.PanelOrg.PerformLayout()
        Me.pnlLuOrgContact.ResumeLayout(False)
        CType(Me.DsSrchContacts1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region 'windows

#Region "Load"

    'LOAD
    Private Sub frmSrchOrg_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles MyBase.Load

        'SET CAPTION STRINGS
        statusM = ""
        usrRadio.Append("Contacts")
        strCurTableStyle = "FoundOrgContacts"
        WhatField = "Name"
        strMainDS = "Org"
        WhatRField = "City"

        'SET QUERY CONNECTION
        Me.daSrch.SelectCommand.Connection = sc
        Me.daContact.SelectCommand.Connection = sc
        Me.sqlGetGrants.Connection = sc
        Me.sqlGetCases.Connection = sc
        Me.sqlGetContacts.Connection = sc
        Me.sqlGetConversations.Connection = sc
        Me.sqlGetRegistrations.Connection = sc
        Me.sqlGetOrgStories.Connection = sc
        Me.sqlGetOrgAlerts.Connection = sc

        'LOAD COMBO BOXES
        modGlobalVar.LoadRegionCombo(Me.cboRegion, "All", usrRegion)
        Me.cboType.SelectedIndex = Me.cboType.FindStringExact("All Congregations") 'Types of Organizations")

        'SET DATAVIEW VARIABLE FOR FILTERING dataGRIDS
        dvM = New DataView(Me.dsSrchOrgs.Tables(0))
        dvM2 = New DataView(Me.DsSrchContacts1.Tables(0))

        dv = dvM
        strbActiveGrid.Append("grdMain")    'use this for doubleclick code

        'FORM SETUP
        Me.AcceptButton = Me.btnSearch
        Me.cboField.SelectedIndex = 0
        isLoaded = True
        Me.chkDetail.Checked = True
         Forms.Add(Me)
        Me.StatusBarPanel1.Text = "Done"

    End Sub 'load

    'CLOSE FORM
    Private Sub miCloseForm_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles miCloseForm.Click
        Me.Close()
    End Sub

#End Region 'Load

#Region "Search"

    'DO SEARCH
    Protected Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles btnSearch.Click, miSrchOrg.Click ', cboType.SelectedIndexChanged ', cboType.SelectionChangeCommitted 'edIndexChanged ', txtSearch.Leave ', cboSearch.SelectedIndexChanged
VALIDATION:
        If isLoaded Then
            If modGlobalVar.NewValidateCombo(Me.cboField, True) Then
            Else
                Exit Sub
            End If
            If modGlobalVar.NewValidateCombo(Me.cboType, True) Then
            Else
                Exit Sub
            End If
            If modGlobalVar.NewValidateCombo(Me.cboRegion, True) Then
            Else
                Exit Sub
            End If
        Else
            Exit Sub
        End If
        If txtSearch.Text = "enter search text" And WhatField > Nothing Then  'need txtsearch
            txtSearch.Focus()
            txtSearch.SelectAll()
            Exit Sub
        Else 'If txtSearch.Text <> "enter search text" And WhatField = Nothing Then
            'search on region and type only
        End If

        MouseWait()
RESET:
        Dim par As String = "" 'SqlClient.SqlParameter
        Me.btnSearch.BackColor = Color.FromKnownColor(KnownColor.InactiveCaption)

        ClearGrid()
        ClearSecGrids()
        bFoundMainOrg = False
        bFoundMainContact = False
        Me.StatusBarPanel1.Text = "Searching... " '& SrchWhat

        'CLEAR EXISTING DATA, SET DEFAULT PARAMETERS
        daSrch.SelectCommand.Parameters("@Name").Value = Nothing
        daSrch.SelectCommand.Parameters("@City").Value = Nothing
        daSrch.SelectCommand.Parameters("@Street").Value = Nothing
        daSrch.SelectCommand.Parameters("@Phone").Value = Nothing
        daSrch.SelectCommand.Parameters("@Program").Value = Nothing
        daSrch.SelectCommand.Parameters("@Denom").Value = Nothing
        daSrch.SelectCommand.Parameters("@OrgID").Value = Nothing
        daSrch.SelectCommand.Parameters("@Zip").Value = Nothing
        daSrch.SelectCommand.Parameters("@County").Value = Nothing
        daSrch.SelectCommand.Parameters("@Culture").Value = Nothing
        daSrch.SelectCommand.Parameters("@Email").Value = Nothing
        daSrch.SelectCommand.Parameters("@Active").Value = Nothing        '.........................
        daSrch.SelectCommand.Parameters("@Type").Value = Nothing
        daSrch.SelectCommand.Parameters("@Region").Value = "%"
        daContact.SelectCommand.Parameters("@Region").Value = "%"
        daSrch.SelectCommand.Parameters("@Active").Value = Me.chkActive.Checked.ToString
        '.....................................
        daContact.SelectCommand.Parameters("@FldName").Value = Nothing
        daContact.SelectCommand.Parameters("@FldVal").Value = Nothing
        daContact.SelectCommand.Parameters("@Active").Value = Me.chkActive.Checked.ToString

        'REGION
        If Me.cboRegion.SelectedIndex > -1 Then
            daSrch.SelectCommand.Parameters("@Region").Value = Me.cboRegion.SelectedItem
            daContact.SelectCommand.Parameters("@Region").Value = Me.cboRegion.SelectedItem
        End If

        'TYPE
        Select Case cboType.SelectedItem.ToString
            Case Is = "All Congregations" : daSrch.SelectCommand.Parameters("@Type").Value = "All Congregations" 'programmed in sgtored proc
                daContact.SelectCommand.Parameters("@Type").Value = "All Congregations"
            Case Is = "All Types of Organizations" : daSrch.SelectCommand.Parameters("@Type").Value = "%"
                daContact.SelectCommand.Parameters("@Type").Value = "%"
            Case Else : daSrch.SelectCommand.Parameters("@Type").Value = Me.cboType.Text
                daContact.SelectCommand.Parameters("@Type").Value = Me.cboType.Text
        End Select

        'NAME
        If Me.txtSearch.Text = "enter search text" Then 'no user input
            'search is on region and type only
            daSrch.SelectCommand.Parameters("@Sort").Value = "tblOrg.OrgName"
            daContact.SelectCommand.Parameters("@Sort").Value = "tblContact.LastName, tblContact.FirstName"
            GoTo FILL_DATASET
            'OTHER FIELD
        Else
            If WhatField > Nothing Then
                Try
                    Select Case WhatField
                        Case "Name" : par = "@Name"
                            daSrch.SelectCommand.Parameters("@Sort").Value = "tblOrg.OrgName"
                            daContact.SelectCommand.Parameters("@Sort").Value = "tblContact.LastName, tblContact.FirstName"
                        Case "City" : par = "@City"
                            daSrch.SelectCommand.Parameters("@Sort").Value = "tblOrg.City"
                            daContact.SelectCommand.Parameters("@Sort").Value = "tblContact.City"
                        Case "Street" : par = "@Street"
                            daSrch.SelectCommand.Parameters("@Sort").Value = "Street1"
                            daContact.SelectCommand.Parameters("@Sort").Value = "tblContact.Street1"
                        Case "Phone" : par = "@Phone"
                            daSrch.SelectCommand.Parameters("@Sort").Value = "Phone"
                            daContact.SelectCommand.Parameters("@Sort").Value = "tblContact.Phone"
                        Case "Email" : par = "@Email"
                            daSrch.SelectCommand.Parameters("@Sort").Value = "tblOrg.Email"
                            daContact.SelectCommand.Parameters("@Sort").Value = "tblContact.Email"
                        Case "Zip" : par = "@zip"
                            daSrch.SelectCommand.Parameters("@Sort").Value = "tblOrg.Zip"
                            daContact.SelectCommand.Parameters("@Sort").Value = "tblContact.Zip"
                            daSrch.SelectCommand.Parameters("@Region").Value = "%"
                            daContact.SelectCommand.Parameters("@Region").Value = "%"
                        Case "County" : par = "@County"
                            daSrch.SelectCommand.Parameters("@Sort").Value = "tblOrg.Zip" '"tblOrg.County" --county not in results
                        Case "Culture", "EIN" : par = "@Culture"
                            daSrch.SelectCommand.Parameters("@Culture").Value = Me.txtSearch.Text
                            daSrch.SelectCommand.Parameters("@Sort").Value = "tblOrg.OrgName"
                        Case "ID#" : par = "@OrgID"
                            daSrch.SelectCommand.Parameters("@Sort").Value = "tblOrg.OrgName"
                            daSrch.SelectCommand.Parameters("@Region").Value = "%"
                            daContact.SelectCommand.Parameters("@Region").Value = "%"
                            daContact.SelectCommand.Parameters("@Sort").Value = "tblContact.ContactID"
                        Case "Program" : par = "@Program"
                        Case "Denomination" : par = "@Denom"
                        Case Else   'default to name
                            '  modGlobalVar.Msg("ERROR: selection not found", WhatField, MessageBoxButtons.OK, MessageBoxIcon.Error)                     'Exit Sub
                    End Select
                Catch ex As System.Exception
                    modGlobalVar.Msg(MsgCodes.invalidSearch)
                End Try
                'GET OPTIONAL WILDCARDS' SET CONTACT SEARCH PARAMS
                Select Case WhatField
                    Case Is = "ID#"
                        daSrch.SelectCommand.Parameters("@OrgID").Value = Me.txtSearch.Text
                        daContact.SelectCommand.Parameters("@FldName").Value = WhatField
                        daContact.SelectCommand.Parameters("@Fldval").Value = Me.txtSearch.Text
                    Case Is = "Denomination", "Program"     'assign fake params to srchContact
                        daSrch.SelectCommand.Parameters("@Sort").Value = "Denomination, OrgName"
                        daSrch.SelectCommand.Parameters(par).Value = modGlobalVar.GetWild(Me.txtSearch.Text, Me.chkWild.CheckState)
                        daContact.SelectCommand.Parameters("@Sort").Value = "tblContact.ContactID"
                        daContact.SelectCommand.Parameters("@FldName").Value = "Name"
                        daContact.SelectCommand.Parameters("@Fldval").Value = "xyzzzz"
                    Case Is = "EIN"

                        daContact.SelectCommand.Parameters("@FldName").Value = "Name"
                        daContact.SelectCommand.Parameters("@Fldval").Value = "xyzzzz"
                    Case Else
                        daSrch.SelectCommand.Parameters(par).Value = modGlobalVar.GetWild(Me.txtSearch.Text, Me.chkWild.CheckState)
                        daContact.SelectCommand.Parameters("@FldName").Value = WhatField
                        daContact.SelectCommand.Parameters("@Fldval").Value = modGlobalVar.GetWild(Me.txtSearch.Text, Me.chkWild.CheckState)
                End Select            '   modGlobalVar.Msg(daOrg.SelectCommand.Parameters(par).Value, , par)
            Else    'user has entered text but not selected a radiobutton
                modGlobalVar.msg("ATTENTION: incomplete information", "please select a field to search", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If
        End If


FILL_DATASET:
        Me.dsSrchOrgs.Clear()
        Me.DsSrchContacts1.Clear()
        Me.dsSrchOrgs.EnforceConstraints = False
        Me.DsSrchContacts1.EnforceConstraints = False

        Try
            daSrch.Fill(Me.dsSrchOrgs, "SrchOrg")
            Me.grdMain.DataSource = Me.dsSrchOrgs.SrchOrg
            iOrgtbl = 0 '2 = non-grantable region
            iOrgtblStyle = 0
        Catch exc As System.FormatException
            modGlobalVar.msg(MsgCodes.invalidSearch)
            Me.StatusBarPanel1.Text = "org fill FORMAT error"
            Exit Sub
        Catch exc As Exception
            modGlobalVar.msg("ERROR: daSrch", exc.Message & NextLine, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.StatusBarPanel1.Text = "org fill unk error"
            Exit Sub
        End Try

        Try
            daContact.Fill(Me.DsSrchContacts1, "SrchContact")
        Catch exc As System.FormatException
            modGlobalVar.msg(MsgCodes.invalidSearch)
            Me.StatusBarPanel1.Text = "contact fill error"
            Exit Sub
        Catch exc As Exception
            modGlobalVar.Msg("ERROR: filling daContact", exc.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.StatusBarPanel1.Text = "contact fill error"
            Exit Sub
        End Try
        '...................................................

SET_HEADINGS:
        If Me.dsSrchOrgs.Tables(iOrgtbl).Rows.Count > 0 Then
            bFoundMainOrg = True
            Me.tbMainGrid.TabPages(0).Text = Me.dsSrchOrgs.Tables(iOrgtbl).Rows.Count.ToString & "  Organizations "
        Else
            Me.tbMainGrid.TabPages(0).Text = "0  Organizations "
        End If
        OrgCaption = Me.tbMainGrid.TabPages(0).Text
        If Me.DsSrchContacts1.SrchContact.Rows.Count > 0 Then
            bFoundMainContact = True
            Me.tbMainGrid.TabPages(1).Text = Me.DsSrchContacts1.Tables(0).Rows.Count.ToString & "  Contacts"
        Else
            Me.tbMainGrid.TabPages(1).Text = "0  Contacts"
        End If
        ContactCaption = Me.tbMainGrid.TabPages(1).Text

        'SET MAIN GRID DATASOURCE BASED ON WHICH TAB IS SELECTED
        AssignGridDatasource(Me.grdMain)

DO_NOT_SELECT_FIRST_ROW:
        Select Case Me.tbMainGrid.SelectedTab.Tag
            Case Is = "Org"
                InitialRowSelection(Me.grdMain, Me.dsSrchOrgs.Tables(iOrgtbl).Rows.Count, tbMainGrid.SelectedTab.Tag)
                'DO NOT SELECT FIRST ROW
                If bFoundMainOrg = True > 0 Then
                    Me.grdMain.UnSelect(0)
                End If
            Case Else
                InitialRowSelection(Me.grdMain, Me.DsSrchContacts1.Tables(0).Rows.Count, tbMainGrid.SelectedTab.Tag)
                'DO NOT SELECT FIRST ROW
                If bFoundMainContact = True > 0 Then
                    Me.grdMain.UnSelect(0)
                End If
        End Select

        MouseDefault()
        Me.btnSearch.BackColor = Color.FromKnownColor(KnownColor.Control)
        Me.StatusBarPanel1.Text = "Done"
    End Sub 'search

    'SET ID FIELDS
    Private Sub SetIDFields(ByVal WhichTab As String)
        ' 5/16 always show orgid in upper label; main item ID is in status bar
        iMainID = grdMain.Item(grdMain.CurrentRowIndex, 0)
        Select Case WhichTab
            Case Is = "Org"
                Me.StatusBarPanelID.Text = "Org ID:" & iMainID.ToString
                Me.fldMainCurrentID.Text = iMainID.ToString
            Case Is = "Contact"
                Me.StatusBarPanelID.Text = "Contact ID: " & iMainID.ToString
                Me.fldMainCurrentID.Text = grdMain.Item(grdMain.CurrentRowIndex, 1).ToString
        End Select

        Me.lblSelection.Text = ""
        Me.lblSelectionID.Text = ""

    End Sub

    'CALL SEARCH and SET SEARCH FIELD FROM RADIO BUTTONS
    Private Sub cboField_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
       Handles cboField.SelectedIndexChanged
        If isLoaded = False Or modGlobalVar.NewIsHeading(sender) = True Then
            Exit Sub
        End If

        WhatField = Me.cboField.Text
        'TODO add code here to hide Type and Region combos
        Select Case WhatField
            Case "ID#"
                Me.cboRegion.Visible = False
                Me.cboType.Visible = False
                Me.chkActive.Visible = False
            Case Is = "Zip"
                Me.cboRegion.Visible = False
                Me.cboType.Visible = False
            Case Else
                Me.cboRegion.Visible = True
                Me.cboType.Visible = True
                Me.chkActive.Visible = True
        End Select
        Me.txtSearch.Focus()
        Me.txtSearch.SelectAll()
        Me.btnSearch.PerformClick()
    End Sub

    'CALL SEARCH
    Private Sub cboType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) _
     Handles cboType.SelectedIndexChanged
        If isLoaded = False Or modGlobalVar.NewIsHeading(sender) = True Then
        Else
            Me.btnSearch.PerformClick()
        End If
    End Sub

    'CALL SEARCH
    Private Sub cboRegion_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) _
        Handles cboRegion.SelectedIndexChanged ', cboType.SelectedIndexChanged
        If isLoaded Then
            Me.btnSearch.PerformClick()
        End If
    End Sub

#End Region 'Search

#Region "LoadSecondary"

    'LOAD SECONDARY GRIDS
    Protected Sub LoadSecondary()
        If isLoaded Then
        Else
            SetStatusBarText("Done")
            Exit Sub
        End If
        If Not Me.chkDetail.Checked Then
            SetStatusBarText("Done")
            Exit Sub
        End If
        If iMainID > 0 Then 'nothing selected in main grid
        Else
            Exit Sub
        End If

        Dim ds As DataSet, da As SqlDataAdapter
        Dim strCaption As String
        itbl = 0

        ClearSecGrids()
        '............................
        SetStatusBarText("Retrieving data...")

        Select Case strMainDS
            'USE ORGS
            Case "Org"
                If bFoundMainOrg Then
                    If rbContact1Conver2.Checked Then    'org contacts
                        tblSecGrid = New DataTable("FoundOrgContacts") ' = tableStyle mapping name
                        Me.sqlGetContacts.Parameters("@IDVal").Value = iMainID
                        '  Me.sqlGetGrants.Connection = sc
                        modGlobalVar.LoadDataTable(tblSecGrid, Me.sqlGetContacts)
                        strCaption = strDGSContact
                        GoTo SetCaptions
                    ElseIf rbCase1Reg2.Checked Then    'org cases
                        tblSecGrid = New DataTable("FoundOrgCases") ' = tableStyle mapping name
                        Me.sqlGetCases.Parameters("@IDVal").Value = iMainID
                        Me.sqlGetCases.Parameters("@Region").Value = "Grantable"
                        modGlobalVar.LoadDataTable(tblSecGrid, Me.sqlGetCases)
                        strCaption = strDGSCase
                        GoTo SetCaptions
                    ElseIf rbStories.Checked = True Then
                        tblSecGrid = New DataTable("GetOrgStories") ' = tableStyle mapping name
                        Me.sqlGetOrgStories.Parameters("@IDVal").Value = iMainID
                        modGlobalVar.LoadDataTable(tblSecGrid, Me.sqlGetOrgStories)
                        strCaption = strDGSStory
                        GoTo SetCaptions
                    ElseIf rbALerts.Checked = True Then
                        tblSecGrid = New DataTable("GetOrgAlerts") ' = tableStyle mapping name
                        Me.sqlGetOrgAlerts.Parameters("@IDVal").Value = iMainID
                        modGlobalVar.LoadDataTable(tblSecGrid, Me.sqlGetOrgAlerts)
                        strCaption = strDGSAlert
                        GoTo SetCaptions
                    ElseIf rbGrants.Checked = True Then
                        tblSecGrid = New DataTable("FoundOrgGrants") ' = tableStyle mapping name
                        Me.sqlGetGrants.Parameters("@IDVal").Value = iMainID
                        strCaption = strDGSGrants
                        modGlobalVar.LoadDataTable(tblSecGrid, Me.sqlGetGrants)
                        GoTo SetCaptions
                    End If
                Else
                    Exit Sub
                End If
            Case "Contact" '"FoundContact"
                If bFoundMainContact Then
                    If rbContact1Conver2.Checked Then    'contact conversations
                        tblSecGrid = New DataTable("GetConversations") ' = tableStyle mapping name
                        Me.sqlGetConversations.Parameters("@IDVal").Value = iMainID
                        Me.sqlGetConversations.Parameters("@IDFld").Value = "Contact"
                        strCaption = strDGSConvers
                        modGlobalVar.LoadDataTable(tblSecGrid, Me.sqlGetConversations)
                        GoTo SetCaptions

                    ElseIf rbCase1Reg2.Checked Then                          'contact registrations
                        tblSecGrid = New DataTable("GetRegistrants2") ' = tableStyle mapping name
                        Me.sqlGetRegistrations.Parameters("@IDVal").Value = iMainID
                        Me.sqlGetRegistrations.Parameters("@IDFld").Value = "Contact"
                        strCaption = strDGSReg
                        modGlobalVar.LoadDataTable(tblSecGrid, Me.sqlGetRegistrations)
                        GoTo SetCaptions
                    Else
                        Exit Sub
                    End If
                Else
                    Exit Sub
                End If
            Case Else
                modGlobalVar.msg("ERROR: tab not found", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
        End Select

FillSecondaryDataset:
        ds.Clear()
        ds.EnforceConstraints = False
        Try
            da.Fill(ds.Tables(itbl))
        Catch exc As Exception
            modGlobalVar.msg("ERROR: daSrch2", exc.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

SetCaptions:
        Me.grdSecond1.DataSource = tblSecGrid
        If strCaption = strDGSCase Then
            Me.grdSecond1.CaptionText = iCntCase & "  " & strCaption
        Else
            Me.grdSecond1.CaptionText = tblSecGrid.Rows.Count.ToString & "  " & strCaption
        End If
        dvS = New DataView(tblSecGrid)
        If tblSecGrid.Rows.Count > 0 Then
            bFoundSec = True
        Else
            bFoundSec = False
        End If

        GetCounts()

        If Len(statusM) > 5 Then
            SetStatusBarText(statusM)
        Else
            SetStatusBarText("Done")
        End If

    End Sub 'load secondary

    'GET RADIO BUTTON COUNTS
    Private Sub GetCounts()
        Dim cmdCntID As New SqlCommand
        Dim SQLReg As String = modGlobalVar.CountValidRegistrations(iMainID, "Contact") '"SELECT COUNT(RegistrationID) FROM vwgetValidWRegistrations WHERE (ContactNum = " & iCol & ") and {Cancelled <> 1) AND ((Notes is null) or (Notes not like 'Delete%'))" 'icontactid'))'Contact   'Me.txtCurrent.Text
        Dim SQLContact As String = "SELECT COUNT(ContactID) FROM vwgetValidContacts WHERE OrgNum = " & iMainID 'Org 'Me.txtCurrent.Text
        Dim SQLConvers As String = "SELECT COUNT(ContactNum) FROM vwgetValidConversations WHERE ContactNum = " & iMainID 'Contact   'Me.txtCurrent.Text
        Dim SQLCase As String = "SELECT COUNT(CaseID) FROM vwGetValidCases WHERE OrgNum = " & iMainID 'Org  'Me.txtCurrent.Text
        Dim SQLStory As String = "SELECT COUNT(StoryID) FROM tblOrgStory WHERE OrgNum = " & iMainID
        Dim SQLAlert As String = "SELECT COUNT(AlertID) FROM tblOrgAlert WHERE OrgNum = " & iMainID
        Dim SQLGrant As String = "SELECT COUNT(GrantNum) FROM vwCntGrants WHERE OrgNum = " & iMainID
        Dim i As Integer = 0

        cmdCntID.Connection = sc
        If Not SCConnect() Then
            Exit Sub
        End If
        SetStatusBarText("counting...")
        Select Case strMainDS
            Case "Org"
                cmdCntID.CommandText = SQLContact
                Try
                    i = cmdCntID.ExecuteScalar()
                Catch ex As Exception
                End Try
                Me.CntContacts.Text = i.ToString()

                cmdCntID.CommandText = SQLCase
                i = 0
                Try
                    i = cmdCntID.ExecuteScalar()
                Catch ex As Exception
                End Try
                Me.cntCases.Text = i.ToString()
                iCntCase = i.ToString

                cmdCntID.CommandText = SQLStory
                i = 0
                Try
                    i = cmdCntID.ExecuteScalar()
                Catch ex As Exception
                End Try
                Me.cntStories.Text = i.ToString()

                cmdCntID.CommandText = SQLAlert
                i = 0
                Try
                    i = cmdCntID.ExecuteScalar()
                Catch ex As Exception
                End Try
                Me.cntAlerts.Text = i.ToString()

                cmdCntID.CommandText = SQLGrant
                i = 0
                Try
                    i = cmdCntID.ExecuteScalar()
                Catch ex As Exception
                End Try
                Me.cntGrants.Text = i.ToString()

            Case "Contact"
                cmdCntID.CommandText = SQLConvers
                i = 0
                Try
                    i = cmdCntID.ExecuteScalar()
                Catch ex As Exception
                End Try
                Me.CntContacts.Text = i.ToString()

                cmdCntID.CommandText = SQLReg
                i = 0
                Try
                    i = cmdCntID.ExecuteScalar()
                Catch ex As Exception
                    modGlobalVar.msg("ERROR: getting reg count", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
                Me.cntCases.Text = i.ToString()
            Case Else
                modGlobalVar.msg("ERROR: strMainDS not found", strMainDS, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Select
        cmdCntID.Dispose()
        sc.Close()
        SetStatusBarText("done")
    End Sub

#End Region 'load sec

#Region "datagrid"

    'FILL DETAIL and txtcurrent
    Private Sub grdMain_CurrentCellChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles grdMain.CurrentCellChanged ', grdSecond1.CurrentCellChanged

        If Not isLoaded Then
            Exit Sub
        End If

        Dim iCurrRow As Integer
        Try
            iCurrRow = grdMain.CurrentRowIndex > -1
        Catch ex As Exception 'no rows
            Exit Sub
        End Try
        If iMainID = Me.grdMain.Item(Me.grdMain.CurrentRowIndex, 0) Then
            Exit Sub 'same row
        End If

        'SET IDs
        SetIDFields(Me.tbMainGrid.SelectedTab.Tag)

        'FILL SECONDARY GRIDS
        LoadSecondary()
    End Sub

    'SEND SECONDARY GRID SELECTED ID TO LABELS
    Private Sub grd_CurrentCellChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
          Handles grdSecond1.CurrentCellChanged
        If Not isLoaded Then
            Exit Sub
        End If

        If sender.CurrentRowIndex >= 0 Then
            Me.lblSelectionID.Text = sender.item(sender.currentrowindex, 0) 'urrentcell.rowindex).value
            Me.lblSelection.Text = usrRadio.ToString
        End If
    End Sub

    'ASSIGN GRID DATASOURCE, SET GRID CAPTIONS
    Private Sub AssignGridDatasource(ByRef grd As DataGrid)
        If bFoundMainOrg = False And bFoundMainContact = False Then
            modGlobalVar.msg("NO MATCHES FOUND", "Hint: " & NextLine & "change the Type of Organization and Region dropdown boxes." & NextLine & "If the Wildcard checkbox is on, wildcards are added at the beginning and end of your search text.", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.grdMain.CaptionText = "NO MATCHING ORGANIZATIONS or CONTACTS FOUND"
        Else
            'ASSIGN GRID DATASOURCE DEPENDING ON TAB SELECTED
            Select Case strMainDS
                Case "Org"
                    dvM = New DataView(Me.dsSrchOrgs.Tables(iOrgtbl))
                    Me.grdMain.DataSource = dvM
                    If bFoundMainOrg Then    ' Me.grdMain.DataSource = Me.dsFoundOrg1.Tables(0)
                        '  HighlightRow(grd, 0)
                    End If
                    Me.grdMain.CaptionText = OrgCaption
                Case "Contact"
                    dvM = New DataView(Me.DsSrchContacts1.Tables(0))
                    Me.grdMain.DataSource = dvM
                    If bFoundMainContact Then
                        '  HighlightRow(grd, 0)
                    End If
                    Me.grdMain.CaptionText = ContactCaption
            End Select
        End If 'main dataset is empty

    End Sub

    'CLEAR SELECTION FROM dataGRIDS AND LOSE FOCUS (note: selection stays with arrow)
    Private Sub grdUnselect_Click(ByVal sender As Object, ByVal e As System.EventArgs) _
            Handles MyBase.Click
        If grdMain.CurrentRowIndex > -1 Then
            Me.grdMain.UnSelect(grdMain.CurrentRowIndex)
        End If
        ClearSecGrids()
    End Sub

    'SET SEARCH CRITERIA TO DEFAULTS AND CLEAR dataSETS
    Protected Sub miClearSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles miClearSearch.Click
        isLoaded = False
        Me.cboRegion.SelectedIndex = 0
        Me.cboType.SelectedIndex = 0
        Me.txtSearch.Text = "enter search text"
        ClearGrid()
        ClearSecGrids()
        Me.Refresh()
        dv.RowFilter = ""
        SetStatusBarText("done")
        isLoaded = True
    End Sub

    'CHANGE MAIN GRID DS ON TAB CHANGE
    Private Sub tbMainGrid_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles tbMainGrid.SelectedIndexChanged

        strMainDS = tbMainGrid.SelectedTab.Tag
        Me.StatusBarPanelID.Text = tbMainGrid.SelectedTab.Tag & " ID: "
        Me.lblSelectionID.Text = ""
        Me.lblSelection.Text = ""

        ClearSecGrids()
        AssignGridDatasource(Me.grdMain)

        SetRadioText()
        GetCounts()
    End Sub

#End Region 'datagrid

#Region "Filter Grid"

    'MOUSE DOWN MAIN GRID - call FILTER
    Protected Sub grdall_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) _
            Handles grdMain.MouseDown ', grdSecond1.MouseDown

        Dim tbl As Object
        Dim strHdr As String    'text for grid header
        Dim iTblStyle As Integer   'table style of grid for filter 

IS_RIGHT_CLICK:
        If e.Button = System.Windows.Forms.MouseButtons.Right Then
            hti = sender.HitTest(e.X, e.Y)
            'ORG vs CONTACT
            dv = dvM
            If strMainDS = "Org" Then
                tbl = Me.dsSrchOrgs.Tables(iOrgtbl) 'Me.DsMain.greenc_SrchOrgs
                strHdr = strDGM1
                iTblStyle = iOrgtblStyle '2 = non-grantable region
            Else
                tbl = Me.DsSrchContacts1.Tables(0)
                strHdr = strDGM2
                iTblStyle = 1
            End If

            'IS LEGITIMATE CELL - SET FILTER
            If hti.Type = DataGrid.HitTestType.Cell Then    'SET FILTER
                'CHECK FOR NULL
                If IsDBNull(sender.Item(hti.Row, hti.Column)) Then 'nulls cause filter error
                    modGlobalVar.msg(MsgCodes.filterEmpty)
                    Exit Sub
                End If
                If Me.chkDetail.Checked Then
                    ClearSecGrids()    'clear child grids 
                End If
CALL_FILTER:
                Select Case strHdr
                    Case Is = strDGM1 'main grid orgs
                        grdFilter(Me.dsSrchOrgs, tbl, strHdr, dvM, iTblStyle)
                   
                    Case Is = strDGM2 ' contacts
                        If strMainDS = "Org" Then 'contacts in sec grid
                            grdFilter(strHdr)
                        Else    'contacts in main grid
                            grdFilter(Me.DsSrchContacts1, tbl, strHdr, dvM2, iTblStyle)
                        End If
                    Case Else 'moved to other sub
                        grdFilter(strHdr)
                        Exit Sub
                End Select
ClearFilter:
            Else            'not in cell,  'CLEAR FILTER
                sender.dataSource = tbl 'removes dv.rowfilter
                sender.CaptionText = tbl.Rows.Count.ToString & "  " & strHdr 'DsOrgSearch1.tblOrg.Rows.Count.ToString
                Me.StatusBarPanel1.Text = ""
                If Me.chkDetail.Checked Then
                    ClearSecGrids()    'clear child grids 
                End If
                Select Case strHdr
                    Case Is = strDGM1
                        statusM = "Done"
                        statusS1 = ""
                    Case Is = strDGM2   'contacts
                        If strMainDS = "Org" Then
                        Else
                            statusM = ""
                        End If
                        statusS1 = ""
                End Select
                SetStatusBarText(statusM)
            End If
IS_LEFT_CLICK:
        ElseIf e.Button = System.Windows.Forms.MouseButtons.Left Then
            strbActiveGrid.Replace(strbActiveGrid.ToString, sender.name)    'use this for doubleclick code

        Else 'future some other kind of click
        End If
    End Sub 'main grid mouse down

    'SECONDARY GRIDS MOUSE DOWN for FILTER
    Protected Sub grdSecond1_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) _
            Handles grdSecond1.MouseDown
        Dim strHdr As String    'text for grid header

RightClick:
        If e.Button = System.Windows.Forms.MouseButtons.Right Then
            'SET VARs BASED ON GRID SELECTED
            hti = sender.HitTest(e.X, e.Y)

            Select Case strMainDS
                Case Is = "Org"     'first 2 rb do double duty with upper grid
                    If rbContact1Conver2.Checked = True Then 'contacts
                        strHdr = strDGSContact
                    ElseIf rbCase1Reg2.Checked = True Then 'cases
                        strHdr = strDGSCase
                    Else
                        For Each rd As Control In Me.GroupBox1.Controls
                            Try
                                If CType(rd, RadioButton).Checked = True Then
                                    strHdr = rd.Text
                                End If
                            Catch ex As Exception
                            End Try
                        Next
                    End If
                Case Is = "Contacts"
                    If rbContact1Conver2.Checked = True Then ' first 2 rb do double duty with upper grid
                        strHdr = strDGSConvers
                    End If
                    If rbCase1Reg2.Checked = True Then
                        strHdr = strDGSReg
                    End If
            End Select 'which tab in main grid
            Me.grdMain.UnSelect(Me.grdMain.CurrentRowIndex)
            'SetFilter:
            If hti.Type = DataGrid.HitTestType.Cell Then    'SET FILTER
                'CHECK FOR NULL
                If IsDBNull(sender.Item(hti.Row, hti.Column)) Then 'nulls cause filter error
                    modGlobalVar.msg(MsgCodes.filterEmpty)
                    Exit Sub
                End If
                grdFilter(strHdr)
ClearFilter:
            Else            'not in cell,  'CLEAR FILTER
                dvS.RowFilter = ""
                SetStatusBarText(statusM)
            End If
IS_LEFT_CLICK:
        ElseIf e.Button = System.Windows.Forms.MouseButtons.Left Then
            strbActiveGrid.Replace(strbActiveGrid.ToString, sender.name)    'use this for doubleclick code
            If sender.tag = "Secondary" And Me.rbCase1Reg2.Checked = True And strMainDS = "Org" Then    'case
                Me.miCaseReport.Enabled = True
            Else
                Me.miCaseReport.Enabled = False
            End If
         Else 'some other kind of click
        End If
    End Sub

    'FILTER METHOD for main grid
    Private Sub grdFilter(ByVal ds As Object, ByVal tbl As Object, ByVal strHdr As String, ByVal dv As DataView, ByVal iTblStyle As Integer)
        Dim strFilter As String
        Dim myColumns As GridColumnStylesCollection

SET_FILTER:
        myColumns = Me.grdMain.TableStyles(iTblStyle).GridColumnStyles
        strFilter = myColumns(hti.Column).MappingName
        strFilter = strFilter & " = '" & Replace(Me.grdMain.Item(hti.Row, hti.Column), "'", "''") & " '"
        statusS1 = ""
        dv.RowFilter = strFilter
        Me.grdMain.DataSource = dv

SET_HEADINGS:
        Me.grdMain.CaptionText = dv.Count.ToString & "/" & tbl.Rows.Count.ToString & "  " & statusM 'strHdr
        Select Case strHdr
            Case Is = strDGM1
                statusM = strHdr & " filtered on " & myColumns(hti.Column).HeaderText '& " = " & grd.Item(hti.Row, hti.Column)
                ClearSecGrids()
                Me.StatusBarPanelID.Text = "Org ID: "
            Case Is = strDGM2
                If strMainDS = "Org" Then
                    statusS1 = strHdr & " filtered on " & myColumns(hti.Column).HeaderText '& " = " & grd.Item(hti.Row, hti.Column)
                Else
                    statusM = strHdr & " filtered on " & myColumns(hti.Column).HeaderText '& " = " & grd.Item(hti.Row, hti.Column)
                    ClearSecGrids()
                End If
                Me.StatusBarPanelID.Text = " Contact ID:"
            Case Else ' = strDGSCase
                statusS1 = strHdr & " filtered on " & myColumns(hti.Column).HeaderText '& " = " & grd.Item(hti.Row, hti.Column)
        End Select
DO_NOT_SELECT_FIRST_ROW:  'unless is only row
        InitialRowSelection(Me.grdMain, dv.Count, strMainDS)
        SetStatusBarText(statusM + statusS1)

    End Sub 'filter main grid

    'FILTER METHOD for secondary datagrid with multiple table styles
    Private Sub grdFilter(ByVal strHdr As String)
        Dim grd As DataGrid = Me.grdSecond1
        Dim strFilter As String
        Dim myColumns As GridColumnStylesCollection
SET_FITLER:
        myColumns = grd.TableStyles(strCurTableStyle).GridColumnStyles
        strFilter = myColumns(hti.Column).MappingName
        strFilter = strFilter & " = '" & grd.Item(hti.Row, hti.Column) & " '"
        statusS1 = ""
        dvS.RowFilter = strFilter
        grd.DataSource = dvS
SET_HEADINGS:
        grd.CaptionText = dvS.Count.ToString & "/" & tblSecGrid.Rows.Count.ToString & "  " & strHdr
        Select Case strHdr
            Case Is = strDGM1
                statusM = strHdr & " filtered on " & myColumns(hti.Column).HeaderText '& " = " & grd.Item(hti.Row, hti.Column)
                ClearSecGrids()
            Case Is = strDGM2
                If strMainDS = "Org" Then
                    statusS1 = strHdr & " filtered on " & myColumns(hti.Column).HeaderText '& " = " & grd.Item(hti.Row, hti.Column)
                Else
                    statusM = strHdr & " filtered on " & myColumns(hti.Column).HeaderText '& " = " & grd.Item(hti.Row, hti.Column)
                    ClearSecGrids()
                End If
            Case Else ' = strDGSCase
                statusS1 = strHdr & " filtered on " & myColumns(hti.Column).HeaderText '& " = " & grd.Item(hti.Row, hti.Column)
        End Select
        grd.CaptionText = dvS.Count.ToString & "grd caption text"
        InitialRowSelection(Me.grdSecond1, dvS.Count, "Other")
        SetStatusBarText(statusM + statusS1)
    End Sub

    'call LOAD SEC
    Private Sub InitialRowSelection(ByRef grd As DataGrid, ByVal numRows As Integer, ByVal What As String)
        If numRows = 1 Then
            grd.Select(0)
            Select Case What
                Case Is = "Org", "Contact"
                    SetIDFields(What)
                    LoadSecondary()
                Case Else
            End Select
            grd.UnSelect(0) 'remove row hightlight
        Else
            'TODO how get rid of arrow in unselected row header?
        End If
    End Sub

    'CLEAR MAIN GRID
    Private Sub ClearGrid()
        Me.dsSrchOrgs.Clear()
        Me.DsSrchContacts1.Clear()
        Me.grdMain.CaptionText = ""
        Me.fldMainCurrentID.Text = ""
        ClearSecGrids()
    End Sub

    'CLEAR SECONDARY GRIDS
    Private Sub ClearSecGrids()
        If Not isLoaded Then
            Exit Sub
        End If
        Me.grdSecond1.CaptionText = ""
        statusS1 = ""
        statusS2 = ""
        Me.CntContacts.Text = 0
        Me.cntCases.Text = 0
        Me.cntGrants.Text = 0
        Me.cntStories.Text = 0
        Me.cntAlerts.Text = 0
        Try
            dvS.RowFilter = ""
            CType(Me.grdSecond1.DataSource, DataTable).Clear()
        Catch ex As Exception
        End Try
        SetStatusBarText(statusM)
    End Sub

#End Region 'Filter

#Region "DetailGrid"

    'HIDE/SHOW SECONDARY GRIDS
    Private Sub chkDetail_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles chkDetail.CheckedChanged
        If Not isLoaded Then
            Exit Sub
        End If

        Me.grdSecond1.Visible = chkDetail.Checked
        LoadSecondary()

    End Sub

    'HIDE SECONDARY GRIDS WHEN NOT REQUIRED
    Private Sub rb_click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles rbContact1Conver2.Click, rbCase1Reg2.Click, rbStories.Click, rbALerts.Click, rbGrants.Click
        '  Dim x As Byte
        If Not isLoaded Then
            Exit Sub
        End If
        '  Me.grdSecond1.Visible = chkDetail.Checked
        If chkDetail.Checked = False Then
            Exit Sub
        End If

        'CHANGE VARIABLES heading, TableStyle
        If sender.checked Then
            usrRadio.Replace(usrRadio.ToString, sender.text)
            '  strCurTableStyle = sender.tag
            If strMainDS = "Org" Then 'double duty
                If rbContact1Conver2.Checked = True Then
                    strCurTableStyle = "FoundOrgContacts"
                End If
                If rbCase1Reg2.Checked = True Then
                    strCurTableStyle = "FoundOrgCases"
                End If
            End If
        End If

        'SET MENU ITEMS
        SetMenuItems(sender.text)
        LoadSecondary()
    End Sub

#End Region 'DetailGrid

#Region "Open Main Form"

    'OPEN FORM FROM MAIN GRID
    Protected Sub OpenMain(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles miGotoOrg.Click, grdMain.DoubleClick
        If Not bFoundMainOrg And Not bFoundMainContact Then
            SetStatusBarText("can't open from empty grid")
            Exit Sub
        End If

        SetStatusBarText("Opening " & strMainDS & " Detail window")
        Select Case strMainDS
            Case Is = "Org"
                If bFoundMainOrg Then
                    iMainID = Me.grdMain.Item(Me.grdMain.CurrentRowIndex, 0)
                    modGlobalVar.OpenMainOrg(iMainID, Me.grdMain.Item(Me.grdMain.CurrentRowIndex, 1)) ', ClassOpenForms.frmMainOrg.DsMainOrg1, ClassOpenForms.frmMainOrg.daMainOrg, ClassOpenForms.frmMainOrg.miSave, "MainOrg", ClassOpenForms.frmMainOrg.daMainOrg.SelectCommand.Parameters("@OrgID"))
                End If
            Case Is = "Contact"
                If bFoundMainContact Then
                    iMainID = Me.grdMain.Item(Me.grdMain.CurrentRowIndex, 0)
                    modGlobalVar.OpenMainContact(iMainID, Me.grdMain.Item(Me.grdMain.CurrentRowIndex, 4) + ", " + Me.grdMain.Item(Me.grdMain.CurrentRowIndex, 3), Me.grdMain.Item(Me.grdMain.CurrentRowIndex, 7) + " : " & NextLine & Me.grdMain.Item(Me.grdMain.CurrentRowIndex, 10), Me.grdMain.Item(Me.grdMain.CurrentRowIndex, 1))
                End If
        End Select

        SetStatusBarText("Done")
    End Sub

#End Region 'OpenMainForm

#Region "Open Secondary Forms"
    'OPEN FORM from SECONDARY GRID
    Protected Sub OpenSecondary(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles grdSecond1.DoubleClick, miGotoCase.Click, miGotoConversation.Click, miGotoRegistration.Click, miGotoStory.Click, miGotoAlert.Click
        Dim str As String
        If Not bFoundSec Then
            Exit Sub
        End If
        SetStatusBarText("Opening " & usrRadio.ToString & " Detail Window")
        Select Case Me.usrRadio.ToString
            Case "Contacts"
                Me.miGotoContact.PerformClick()
            Case "Cases"
                modGlobalVar.OpenMainCase(Me.grdSecond1.Item(Me.grdSecond1.CurrentRowIndex, 0), Me.grdSecond1.Item(Me.grdSecond1.CurrentRowIndex, 2), Me.grdMain.Item(Me.grdMain.CurrentRowIndex, 1) + " : " & NextLine & Me.grdMain.Item(Me.grdMain.CurrentRowIndex, 4), Me.grdMain.Item(Me.grdMain.CurrentRowIndex, 0))
            Case "Conversations"
                str = SubstrBriefSummary(IsNull(Me.grdSecond1.Item(Me.grdSecond1.CurrentRowIndex, 4), ""))
                     modGlobalVar.OpenMainConversation(Me.grdSecond1.Item(Me.grdSecond1.CurrentRowIndex, 0), str, Me.grdMain.Item(Me.grdMain.CurrentRowIndex, 7) + " : " & NextLine & Me.grdMain.Item(Me.grdMain.CurrentRowIndex, 10), Me.grdMain.Item(Me.grdMain.CurrentRowIndex, 1))
            Case "Registrations"
                If tblRegistrant.Rows.Count = 0 Then
                    modGlobalVar.LoadRegistrantDD(False)
                End If
                modGlobalVar.OpenMainWReg2(Me.grdSecond1.Item(Me.grdSecond1.CurrentRowIndex, 0), str, True, "Registrant")
                'TODO add contact param to registration??
            Case Is = "Grants"
                If Me.grdSecond1.Item(Me.grdSecond1.CurrentRowIndex, 1).ToString.Contains("Application") Then
                    modGlobalVar.OpenMGIForm(Me.grdSecond1.Item(Me.grdSecond1.CurrentRowIndex, 0).ToString.Substring(4), Me.grdSecond1.Item(Me.grdSecond1.CurrentRowIndex, 0).ToString.Substring(0, 4), Me.grdMain.Item(Me.grdMain.CurrentRowIndex, 1) + " : " & NextLine & Me.grdMain.Item(Me.grdMain.CurrentRowIndex, 4), Me.grdMain.Item(Me.grdMain.CurrentRowIndex, 0))
                Else
                    modGlobalVar.OpenMainGrant(Me.grdSecond1.Item(Me.grdSecond1.CurrentRowIndex, 0), "Grant", Me.grdMain.Item(Me.grdMain.CurrentRowIndex, 1) + " : " & NextLine & Me.grdMain.Item(Me.grdMain.CurrentRowIndex, 4), Me.grdMain.Item(Me.grdMain.CurrentRowIndex, 0))
                End If
            Case "Stories"
                modGlobalVar.OpenMainStory(Me.grdSecond1.Item(Me.grdSecond1.CurrentRowIndex, 0), Me.grdMain.Item(Me.grdMain.CurrentRowIndex, 1) + " : " & NextLine & Me.grdMain.Item(Me.grdMain.CurrentRowIndex, 4), Me.grdMain.Item(Me.grdMain.CurrentRowIndex, 0))
            Case "Alerts"
                modGlobalVar.OpenMainAlert(Me.grdSecond1.Item(Me.grdSecond1.CurrentRowIndex, 0), Me.grdMain.Item(Me.grdMain.CurrentRowIndex, 1) + " : " & NextLine & Me.grdMain.Item(Me.grdMain.CurrentRowIndex, 4), Me.grdMain.Item(Me.grdMain.CurrentRowIndex, 0))
            Case Else
                modGlobalVar.msg("ERROR: usrRadio not found", usrRadio.ToString, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Select
        SetStatusBarText("Done")

    End Sub 'grdsecond dblclick

    'OPEN CONTACT DETAIL
    Private Sub miGotoContact_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles miGotoContact.Click

        Select Case strMainDS
            Case Is = "Org"   'get ID from secondary grid
                modGlobalVar.OpenMainContact(Me.grdSecond1.Item(Me.grdSecond1.CurrentRowIndex, 0), Me.grdSecond1.Item(Me.grdSecond1.CurrentRowIndex, 2) + " " + Me.grdSecond1.Item(Me.grdSecond1.CurrentRowIndex, 1), Me.grdMain.Item(Me.grdMain.CurrentRowIndex, 1) + " : " + Me.grdMain.Item(Me.grdMain.CurrentRowIndex, 4), Me.grdMain.Item(Me.grdMain.CurrentRowIndex, 0))
            Case Is = "Contact"    'get ID from main grid
                modGlobalVar.OpenMainContact(Me.grdMain.Item(Me.grdMain.CurrentRowIndex, 0), Me.grdMain.Item(Me.grdMain.CurrentRowIndex, 3) + " " + Me.grdMain.Item(Me.grdMain.CurrentRowIndex, 4), Me.grdMain.Item(Me.grdMain.CurrentRowIndex, 7) + " : " + Me.grdMain.Item(Me.grdMain.CurrentRowIndex, 10), Me.grdMain.Item(Me.grdMain.CurrentRowIndex, 1))
        End Select
    End Sub

    'OPEN CASE REPORT
    Private Sub miCaseReport_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        MouseWait()
        Dim strb As New StringBuilder
        strb.Append("SELECT  tblCase.CaseID, tblConversation.ConversDate, tblConversation.Notes, tblOrg.OrgName, " _
        & " tblCase.CaseName, tblContact.FirstName, tblContact.Lastname,  tblConversation.BriefSummary, luStaff.StaffName AS CaseMgr " _
 & " FROM         tblCase LEFT OUTER JOIN luStaff ON tblCase.CaseMgrNum = luStaff.StaffID LEFT OUTER JOIN tblConversation ON tblCase.CaseID = tblConversation.CaseNum LEFT OUTER JOIN tblOrg ON tblCase.OrgNum = tblOrg.OrgID LEFT OUTER JOIN tblContact ON tblConversation.ContactNum = tblContact.ContactID" _
 & " WHERE    tblCase.CaseID = " & Me.grdSecond1.Item(grdSecond1.CurrentRowIndex, 0))

        strb.Append(" ORDER BY CaseID, tblConversation.ConversDate DESC")
        modPopup.PrintCaseConversation(strb.ToString)
        MouseDefault()
    End Sub

#End Region  'open main form

#Region "Set Menu"

    'ENABLE GOTO MENU ITEMS depending on radio button selected
    Private Sub SetMenuItems(ByVal str As String)
        Me.miGotoConversation.Enabled = False
        Me.miGotoRegistration.Enabled = False
        Me.miGotoCase.Enabled = False

        Select Case str
            Case "Cases"
                If bFoundSec Then Me.miGotoCase.Enabled = True
            Case "Conversations"
                If bFoundSec Then Me.miGotoConversation.Enabled = True
            Case "Registrations"
                If bFoundSec Then Me.miGotoRegistration.Enabled = True
            Case "Stories"
                If bFoundSec Then Me.miGotoStory.Enabled = True
            Case "Alerts"
                If bFoundSec Then Me.miGotoAlert.Enabled = True
            Case Else
        End Select
    End Sub

#End Region 'menu

#Region "ADD ITEM"

    'INSERT NEW ITEM to BACKEND and OPEN DETAIL FORM
    Protected Sub miAddOrg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles miAddOrg.Click, btnNew.Click

        If modGlobalVar.msg("Are you sure?", "About to enter a new Organization", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then
            Exit Sub
        End If
        MouseWait()
        Me.StatusBarPanel1.Text = "Entering New Organization"
InsertNewRecord:
        Dim str As String = "INSERT INTO tblOrg(OrgName, OrgType, SatelliteRegion, CreateStaffNum, CreateDate) VALUES (N'entering New Organization', N'Church', N'" & usrRegion & "', " & usr & ", GETDATE()); SELECT @@Identity"
        If Not SCConnect() Then
            Exit Sub
        End If

        Dim cmd As New SqlClient.SqlCommand(str, sc) ', myTrans)
        Dim newID As Integer
        Try
            newID = cmd.ExecuteScalar()
        Catch ex As Exception
            sc.Close()
            Exit Sub
        End Try
        sc.Close()
        Me.StatusBarPanel1.Text = "Done"
OpenForm:
        modGlobalVar.OpenMainOrg(newID, "Entering new organization")
        MouseDefault()
    End Sub
#End Region 'Add Item

    'TODO ALL FORMS on grid filter/clear filter, refill secondary grid

#Region "General"

    'SET STATUS BAR LEFT TEXT re FILTERS
    Private Sub SetStatusBarText(ByVal str As String)
        Me.StatusBarPanel1.Text = str '& " " & statusS1 '& " " & statusS2
    End Sub

    'ENTER SRCH TEXT BOX SELECT ALL
    Private Sub txtSearch_Enter(ByVal sender As Object, ByVal e As System.EventArgs) _
        Handles txtSearch.Enter
        Me.txtSearch.SelectAll()
    End Sub

    'BTN HELP
    Private Sub btnHelpOrg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles btnHelpOrg.Click, miHelp.Click
        modGlobalVar.msg("HELP: SEARCH for CONGREGATION or CONTACT:", "HOW TO SEARCH:" & NextLine & "1. Select search criteria using the radio buttons and drop down boxes. " & NextLine & "    If applicable, enter any text you wish to find." & NextLine & "2. Click the Search button, or press the Enter key." & NextLine & NextLine & "Note: A search on Name, Email, Street, City, or Zip etc.,  will search both Organizations and Contacts with the results showing on the " & NextLine & "     corresponding tabs.  Contacts at an Organization are listed below the main grid, not on the Contact tab." & NextLine & NextLine & "Note: ~ after Lastname indicates they are no longer Active." & NextLine & "         ^ after Lastname indicates the Primary contact." & NextLine & NextLine & "Note: * is a wildcard character for searching.  * is automatically utilized at the beginning and end " & NextLine & "    of your search string when applicable. To remove these wildcards uncheck the Wildcards checkbox." & NextLine & NextLine & "To ADD NEW:" & NextLine & "ORGANIZATION - Click the yellow New button or use the menu: File/New Organization." & NextLine & "CASE or CONTACT - first go to the Organization Detail window;" & NextLine & "CONVERSATION - first go to the Case Detail or Contact Detail window;" & NextLine & "REGISTRATION - first go to the Event or the Contact Detail window." & NextLine & NextLine & "* Asterisk after a Case Name in the lower grid indicates Grant Report is Overdue from congregation.", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    'REPORT SERVER STAFF LAITY REPORT
    Private Sub miStaffRpt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles miStaffRpt.Click
        Dim strb As New StringBuilder
        'NOTE is datagrid, so cannot select multiple rows
        If iMainID > 0 Then
            MouseWait()
            Try
                modPopup.StaffLaityRpt(iMainID.ToString)
            Catch ex As Exception
                modGlobalVar.msg("ERROR: StaffLaity report ", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                MouseDefault()
            End Try
            'OpenStaffLaityRpt(iCol.ToString, Me.grdMain.Item(grdMain.CurrentRowIndex, 1))
        Else
            modGlobalVar.msg(MsgCodes.noRowSelected)
        End If
    End Sub

    'SET RADIO BUTTON TEXT org vs contact
    Private Sub SetRadioText()
        Select Case strMainDS
            Case "Org"
                Me.rbContact1Conver2.Text = "Contacts"
                Me.rbCase1Reg2.Text = "Cases"
                Me.rbStories.Visible = True
                Me.cntStories.Visible = True
                Me.rbALerts.Visible = True
                Me.cntAlerts.Visible = True
                Me.rbGrants.Visible = True
                Me.cntGrants.Visible = True
            Case "Contact"
                Me.rbContact1Conver2.Text = "Conversations"
                Me.rbCase1Reg2.Text = "Registrations"
                Me.rbStories.Visible = False
                Me.cntStories.Visible = False
                Me.rbALerts.Visible = False
                Me.cntAlerts.Visible = False
                Me.rbGrants.Visible = False
                Me.cntGrants.Visible = False
            Case Else
                modGlobalVar.msg("ERROR: strMainDS not found", strMainDS, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Select
        Me.rbContact1Conver2.Checked = True
        usrRadio.Replace(usrRadio.ToString, rbContact1Conver2.Text)
        SetMenuItems(rbContact1Conver2.Text)
    End Sub

#End Region 'general

End Class




