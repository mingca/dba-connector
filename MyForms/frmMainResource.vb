Imports System
Imports System.Data.SqlClient
Imports System.Text
Imports Microsoft.Office.Interop
Imports System.IO
'===================== 10/15 cg =============================
'only afew staff can fully edit a resource once it's been marked OnCRG.
'a read-only verions of this form opens for others; controlled by luStaff MemoDuties (newCRGFull, newCRGEdit)
'only newCRGFull staff can use CRG checkbox and dd; that plus LastChangeDate triggers upload to website
'NewCRGEdit staff can edit Annotation and URL (all fields) except CRG checkbox and dd 
'=============================================================
Public Class frmMainResource
    Inherits System.Windows.Forms.Form

    Dim Mystery As Boolean
    Public isLoaded As Boolean = False

    Dim GetResourceLocation As DataTable ', GetResourceExtra As DataTable
    Dim iExtraID As Integer
    Dim ds As DataSet, tbl As DataTable
    Dim daRFA As SqlDataAdapter = New SqlDataAdapter
    Dim cmd As New SqlCommand
    Dim itblStyle As Integer
    Dim frmExtra As frmMainResourceAuthor
    Dim ppFile As New ContextMenu
    Dim ppImage As New ContextMenu
    Dim ehFile As EventHandler = AddressOf ehOpenFile
    Dim ehImage As EventHandler = AddressOf ehOpenImage
    Dim bMatch As Boolean  'new index term already exists
    Dim bExtras As Boolean = False 'extras has changes
    Dim daPub As SqlDataAdapter
    Dim dsPub As DataSet
    Dim DocPath As String = LinkedPath & "Resources\"
    Dim ImagePath As String = LinkedPath & "ResourceImages\"
    Dim enumRecommend, enumFeedback, enumAlert, enumAuthor, emunLocation As structHeadings
    Dim usrIndexChoice As String

    '===== for Detail form  closing routines
    Dim ctlIdentify As Control 'fields for delete and messages
    Dim ctlNeutral As Control 'will never be invalidated
    Dim objHowClose As Short  ' structCloseMethod 'Object = Me.btnSaveExit 'identify object calling close
    Dim mainDS As DataSet 'for generic module calls like CloseDetailForm
    Dim mainTopic As String 'name of entity of this form - case, contact, etc
    Dim mainBSrce As System.Windows.Forms.BindingSource
    Dim mainTbl As DataTable
    Dim bDirty As Boolean 'crg combobox 
    Public ThisID, LocalOrgID As Integer


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

#End Region     'initialize

#Region " Windows Form Designer generated code "
    Friend WithEvents miHelpGeneral As System.Windows.Forms.MenuItem
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents miCRG As System.Windows.Forms.MenuItem
    Friend WithEvents cboCRGStatus As InfoCtr.ComboBoxRelaxed 'System.Windows.Forms.ComboBox
    Friend WithEvents ppStrip As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents fldIndexCSV As System.Windows.Forms.TextBox
    Friend WithEvents DataGridTextBoxColumn69 As System.Windows.Forms.DataGridTextBoxColumn
    ' Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem2 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents fldIndexDE As System.Windows.Forms.TextBox
    Friend WithEvents btnIndexAdd As System.Windows.Forms.Button
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents Label40 As System.Windows.Forms.Label
    Friend WithEvents DtAccess As InfoCtr.DateTextBox
    Friend WithEvents Website As System.Windows.Forms.TextBox
    Friend WithEvents DsResourceExtra1 As InfoCtr.dsResourceExtra
    Friend WithEvents cboType As InfoCtr.ComboBoxRelaxed 'System.Windows.Forms.ComboBox
    Friend WithEvents cboSubtype As InfoCtr.ComboBoxRelaxed 'System.Windows.Forms.ComboBox
    Friend WithEvents DsMainResource1 As InfoCtr.dsMainResource
    Friend WithEvents txtSponsoringOrg As System.Windows.Forms.TextBox
    Friend WithEvents MainResourceTableAdapter As InfoCtr.dsMainResourceTableAdapters.MainResourceTableAdapter
    Friend WithEvents ResourceMainAddressBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents ResourceMainAddressTableAdapter As InfoCtr.dsMainResourceTableAdapters.ResourceMainAddressTableAdapter
    Friend WithEvents lblAuxOrgName As System.Windows.Forms.Label
    Friend WithEvents pgAddress As System.Windows.Forms.TabPage
    Friend WithEvents btnImage As System.Windows.Forms.Button
    Friend WithEvents Label50 As System.Windows.Forms.Label
    Friend WithEvents fldAddrChangedBy As System.Windows.Forms.TextBox
    Friend WithEvents fldAddrChangeDt As System.Windows.Forms.TextBox
    Friend WithEvents lblEntityType As System.Windows.Forms.Label
    Friend WithEvents lblAddressType As System.Windows.Forms.Label
    Friend WithEvents lblUseThisOne As System.Windows.Forms.Label
    Friend WithEvents Label51 As System.Windows.Forms.Label
    Friend WithEvents TextBox5 As System.Windows.Forms.TextBox
    Friend WithEvents lblEntityNum As System.Windows.Forms.Label
    Friend WithEvents MainResourceBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents StatusBarPanelID As System.Windows.Forms.StatusBarPanel
    Friend WithEvents pnlOrgAddress As System.Windows.Forms.Panel
    Friend WithEvents btnOrg As System.Windows.Forms.Button
    Friend WithEvents rtxtOrgAddress As System.Windows.Forms.RichTextBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Country As System.Windows.Forms.TextBox
    Friend WithEvents Zip As System.Windows.Forms.TextBox
    Friend WithEvents Address2 As System.Windows.Forms.TextBox
    Friend WithEvents City As System.Windows.Forms.TextBox
    Friend WithEvents State As System.Windows.Forms.TextBox
    Friend WithEvents Email As System.Windows.Forms.TextBox
    Friend WithEvents Address1 As System.Windows.Forms.TextBox
    Friend WithEvents Telephone2 As System.Windows.Forms.TextBox
    Friend WithEvents Telephone3 As System.Windows.Forms.TextBox

    Friend WithEvents btnHelp As System.Windows.Forms.Button
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents btnSaveExit As System.Windows.Forms.Button
    Friend WithEvents btnNew As System.Windows.Forms.Button
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents OrgNum As System.Windows.Forms.TextBox
    Friend WithEvents ICCResourceID As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents DsType1 As InfoCtr.dsType
    Friend WithEvents miPrintPublic As System.Windows.Forms.MenuItem
    Friend WithEvents grdFeedback As System.Windows.Forms.DataGrid
    Friend WithEvents grdAlert As System.Windows.Forms.DataGrid
    Friend WithEvents Label43 As System.Windows.Forms.Label
    Friend WithEvents txtReviewNote As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label36 As System.Windows.Forms.Label
    Friend WithEvents txtPubDate As System.Windows.Forms.TextBox
    Friend WithEvents lblGotoPublisher As System.Windows.Forms.Label
    Friend WithEvents cboPublisher As InfoCtr.ComboBoxRelaxed 'System.Windows.Forms.ComboBox
    Friend WithEvents tbPublishing As System.Windows.Forms.TabControl
    Friend WithEvents pgArticle As System.Windows.Forms.TabPage
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents TextBox8 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox9 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox6 As System.Windows.Forms.TextBox
    Friend WithEvents pgBook As System.Windows.Forms.TabPage
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents textbox1Source4 As System.Windows.Forms.TextBox
    Friend WithEvents textbox1Source5 As System.Windows.Forms.TextBox
    Friend WithEvents textbox1Source6 As System.Windows.Forms.TextBox
    Friend WithEvents textbox1Source7 As System.Windows.Forms.TextBox
    Friend WithEvents textbox1Source8 As System.Windows.Forms.TextBox
    Friend WithEvents pgMedia As System.Windows.Forms.TabPage
    Friend WithEvents textbox1Source0 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox3 As System.Windows.Forms.TextBox
    Friend WithEvents Label35 As System.Windows.Forms.Label
    Friend WithEvents Label34 As System.Windows.Forms.Label
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents TextBox24 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox25 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox26 As System.Windows.Forms.TextBox
    Friend WithEvents lblLink As System.Windows.Forms.Label
    Friend WithEvents txtURL As System.Windows.Forms.TextBox

    Friend WithEvents tsAlert As System.Windows.Forms.DataGridTableStyle
    Friend WithEvents DataGridTextBoxColumn12 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn13 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn14 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn15 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn16 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents tsRecommend As System.Windows.Forms.DataGridTableStyle
    Friend WithEvents DataGridTextBoxColumn17 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn18 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn19 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn20 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn21 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn22 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn23 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents tsFeedback As System.Windows.Forms.DataGridTableStyle
    Friend WithEvents DataGridTextBoxColumn24 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn25 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn26 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn27 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn28 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn29 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn56 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn2 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn4 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn57 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn58 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn59 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn60 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn61 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn62 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn63 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn64 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn65 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn66 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents miPrintDescription As System.Windows.Forms.MenuItem
    Friend WithEvents miPrintAllFields As System.Windows.Forms.MenuItem
    Friend WithEvents miRptFeedback As System.Windows.Forms.MenuItem
    Friend WithEvents ErrorProvider1 As System.Windows.Forms.ErrorProvider
    Friend WithEvents DataGridTextBoxColumn67 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents Label37 As System.Windows.Forms.Label
    Friend WithEvents PhoneExtension As System.Windows.Forms.TextBox
    Friend WithEvents cboWhyInactive As InfoCtr.ComboBoxRelaxed 'System.Windows.Forms.ComboBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label52 As System.Windows.Forms.Label
    Friend WithEvents miAttach As System.Windows.Forms.MenuItem
    Friend WithEvents miOpenFile As System.Windows.Forms.MenuItem
    Friend WithEvents btnOpenFile As System.Windows.Forms.Button
    Friend WithEvents miDefinitions As System.Windows.Forms.MenuItem
    Friend WithEvents Label55 As System.Windows.Forms.Label
    Friend WithEvents DataGridTextBoxColumn68 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents lblFlagPerson As System.Windows.Forms.Label
    Friend WithEvents SqlConnection1 As System.Data.SqlClient.SqlConnection
    Friend WithEvents pgNewCRG As System.Windows.Forms.TabPage
    Friend WithEvents lblStar As System.Windows.Forms.Label


    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.

    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents pnlResource As System.Windows.Forms.Panel
    ' Friend WithEvents cboType5 As System.Windows.Forms.ComboBox
    Friend WithEvents pgPrivate As System.Windows.Forms.TabPage
    Friend WithEvents pgPublic As System.Windows.Forms.TabPage
    Friend WithEvents rtbDescriptions As System.Windows.Forms.RichTextBox
    Friend WithEvents chkActive As System.Windows.Forms.CheckBox
    Friend WithEvents chkCRGWebsite As System.Windows.Forms.CheckBox
    Friend WithEvents chkLocal As System.Windows.Forms.CheckBox
    Friend WithEvents txtReferralSource As System.Windows.Forms.TextBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents grdLocation As System.Windows.Forms.DataGrid
    Friend WithEvents textbox1Source9 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox20 As System.Windows.Forms.TextBox
    Friend WithEvents tbSource As System.Windows.Forms.TabControl
    Friend WithEvents pgPublish As System.Windows.Forms.TabPage
    Friend WithEvents oldAddress As System.Windows.Forms.TabPage
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents Label38 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents DataGridTableStyle1 As System.Windows.Forms.DataGridTableStyle
    Friend WithEvents DataGridTextBoxColumn1 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn3 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn5 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn6 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn7 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn8 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn9 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn10 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn11 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents tbDescriptions As System.Windows.Forms.TabControl
    Friend WithEvents flagInactive As System.Windows.Forms.Label
    Friend WithEvents flagAlert As System.Windows.Forms.Label
    Friend WithEvents Label39 As System.Windows.Forms.Label
    Friend WithEvents MainMenu1 As System.Windows.Forms.MainMenu
    Friend WithEvents MenuItem1 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem3 As System.Windows.Forms.MenuItem
    Friend WithEvents miSave As System.Windows.Forms.MenuItem
    Friend WithEvents miCancel As System.Windows.Forms.MenuItem
    Friend WithEvents miDelete As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem7 As System.Windows.Forms.MenuItem
    Friend WithEvents miHelp As System.Windows.Forms.MenuItem
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents cboKey1 As InfoCtr.ComboBoxRelaxed
    Friend WithEvents cboKey2 As InfoCtr.ComboBoxRelaxed
    Friend WithEvents cboKey3 As InfoCtr.ComboBoxRelaxed
    Friend WithEvents cboKey4 As InfoCtr.ComboBoxRelaxed
    Friend WithEvents Label41 As System.Windows.Forms.Label
    Friend WithEvents Label42 As System.Windows.Forms.Label
    '  Friend WithEvents cboSubType As System.Windows.Forms.ComboBox
    Friend WithEvents miClose As System.Windows.Forms.MenuItem
    Friend WithEvents StatusBar1 As System.Windows.Forms.StatusBar
    Friend WithEvents StatusBarPanel1 As System.Windows.Forms.StatusBarPanel
    Friend WithEvents StatusBarPanel2 As System.Windows.Forms.StatusBarPanel
    Friend WithEvents txtResourceName As System.Windows.Forms.TextBox
    Friend WithEvents SqlInsertCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlDeleteCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents grdExtras As System.Windows.Forms.DataGrid
    Friend WithEvents grdRecommend As System.Windows.Forms.DataGrid
    Friend WithEvents pgExtra As System.Windows.Forms.TabPage
    Friend WithEvents pgRecommendation As System.Windows.Forms.TabPage
    Friend WithEvents pgFeedback As System.Windows.Forms.TabPage
    Friend WithEvents pgAlert As System.Windows.Forms.TabPage
    Friend WithEvents DsRFA As InfoCtr.dsMisc
    Friend WithEvents tsAuthors As System.Windows.Forms.DataGridTableStyle
    Friend WithEvents tsContacts As System.Windows.Forms.DataGridTableStyle
    Friend WithEvents tsEvents As System.Windows.Forms.DataGridTableStyle
    Friend WithEvents DataGridTextBoxColumn30 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn31 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn32 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn33 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn34 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn35 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn36 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn37 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn38 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn39 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn40 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn41 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn42 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn43 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn44 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn45 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn46 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn47 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn48 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn49 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn50 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn51 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn52 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn53 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn54 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn55 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents tsResourceName As System.Windows.Forms.DataGridTableStyle
    Friend WithEvents chkOrg As System.Windows.Forms.CheckBox
    Friend WithEvents lblAddressRequired As System.Windows.Forms.Label
    Friend WithEvents Telephone As System.Windows.Forms.TextBox
    Friend WithEvents pgOther As System.Windows.Forms.TabPage
    Friend WithEvents pnlReview As System.Windows.Forms.Panel
    Friend WithEvents btnReviewed As System.Windows.Forms.Button
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label44 As System.Windows.Forms.Label
    Friend WithEvents Label45 As System.Windows.Forms.Label
    Friend WithEvents Label47 As System.Windows.Forms.Label
    Friend WithEvents cboReview As InfoCtr.ComboBoxRelaxed 'System.Windows.Forms.ComboBox
    Friend WithEvents fldChangeBy As System.Windows.Forms.TextBox
    Friend WithEvents chkApproved As System.Windows.Forms.CheckBox
    Friend WithEvents fldLastChangeDt As System.Windows.Forms.TextBox
    Friend WithEvents fldCreatedBy As System.Windows.Forms.TextBox
    Friend WithEvents fldCreateDt As System.Windows.Forms.TextBox
    Friend WithEvents dtReview As InfoCtr.DateTextBox
    Friend WithEvents fldReviewStaff As System.Windows.Forms.TextBox
    Friend WithEvents tbMain As System.Windows.Forms.TabControl
    Friend WithEvents miNew As System.Windows.Forms.MenuItem
    Friend WithEvents txtPubState As System.Windows.Forms.TextBox
    Friend WithEvents txtPubCity As System.Windows.Forms.TextBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMainResource))
        Me.Label2 = New System.Windows.Forms.Label()
        Me.tbMain = New System.Windows.Forms.TabControl()
        Me.pgExtra = New System.Windows.Forms.TabPage()
        Me.DtAccess = New InfoCtr.DateTextBox()
        Me.MainResourceBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.DsMainResource1 = New InfoCtr.dsMainResource()
        Me.Label40 = New System.Windows.Forms.Label()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.fldIndexDE = New System.Windows.Forms.TextBox()
        Me.btnIndexAdd = New System.Windows.Forms.Button()
        Me.fldIndexCSV = New System.Windows.Forms.TextBox()
        Me.txtURL = New System.Windows.Forms.TextBox()
        Me.Label42 = New System.Windows.Forms.Label()
        Me.grdExtras = New System.Windows.Forms.DataGrid()
        Me.DsResourceExtra1 = New InfoCtr.dsResourceExtra()
        Me.tsAuthors = New System.Windows.Forms.DataGridTableStyle()
        Me.DataGridTextBoxColumn5 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn6 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn7 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn8 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn66 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn9 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn10 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn11 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn64 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.tsContacts = New System.Windows.Forms.DataGridTableStyle()
        Me.DataGridTextBoxColumn30 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn31 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn32 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn33 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn34 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn35 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn36 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn37 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn38 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn39 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn41 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn40 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.tsEvents = New System.Windows.Forms.DataGridTableStyle()
        Me.DataGridTextBoxColumn42 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn43 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn44 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn45 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn46 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn47 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn48 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn4 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn57 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn60 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn58 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn59 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn61 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn62 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn63 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.tsResourceName = New System.Windows.Forms.DataGridTableStyle()
        Me.DataGridTextBoxColumn49 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn50 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn51 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn52 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn53 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn54 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn55 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn65 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Label52 = New System.Windows.Forms.Label()
        Me.cboKey1 = New InfoCtr.ComboBoxRelaxed()
        Me.cboKey2 = New InfoCtr.ComboBoxRelaxed()
        Me.cboKey3 = New InfoCtr.ComboBoxRelaxed()
        Me.cboKey4 = New InfoCtr.ComboBoxRelaxed()
        Me.grdLocation = New System.Windows.Forms.DataGrid()
        Me.DataGridTableStyle1 = New System.Windows.Forms.DataGridTableStyle()
        Me.DataGridTextBoxColumn1 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn3 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn2 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn69 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.tbSource = New System.Windows.Forms.TabControl()
        Me.oldAddress = New System.Windows.Forms.TabPage()
        Me.pnlOrgAddress = New System.Windows.Forms.Panel()
        Me.btnOrg = New System.Windows.Forms.Button()
        Me.rtxtOrgAddress = New System.Windows.Forms.RichTextBox()
        Me.PhoneExtension = New System.Windows.Forms.TextBox()
        Me.Label37 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Country = New System.Windows.Forms.TextBox()
        Me.Zip = New System.Windows.Forms.TextBox()
        Me.Address2 = New System.Windows.Forms.TextBox()
        Me.City = New System.Windows.Forms.TextBox()
        Me.State = New System.Windows.Forms.TextBox()
        Me.Email = New System.Windows.Forms.TextBox()
        Me.Address1 = New System.Windows.Forms.TextBox()
        Me.Telephone2 = New System.Windows.Forms.TextBox()
        Me.Telephone3 = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.lblAddressRequired = New System.Windows.Forms.Label()
        Me.Telephone = New System.Windows.Forms.TextBox()
        Me.txtSponsoringOrg = New System.Windows.Forms.TextBox()
        Me.lblAuxOrgName = New System.Windows.Forms.Label()
        Me.pgPublish = New System.Windows.Forms.TabPage()
        Me.Label36 = New System.Windows.Forms.Label()
        Me.txtPubDate = New System.Windows.Forms.TextBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.txtPubCity = New System.Windows.Forms.TextBox()
        Me.lblGotoPublisher = New System.Windows.Forms.Label()
        Me.cboPublisher = New InfoCtr.ComboBoxRelaxed()
        Me.Label38 = New System.Windows.Forms.Label()
        Me.txtPubState = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.tbPublishing = New System.Windows.Forms.TabControl()
        Me.pgArticle = New System.Windows.Forms.TabPage()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.TextBox8 = New System.Windows.Forms.TextBox()
        Me.TextBox9 = New System.Windows.Forms.TextBox()
        Me.TextBox6 = New System.Windows.Forms.TextBox()
        Me.pgBook = New System.Windows.Forms.TabPage()
        Me.Label51 = New System.Windows.Forms.Label()
        Me.TextBox5 = New System.Windows.Forms.TextBox()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.textbox1Source4 = New System.Windows.Forms.TextBox()
        Me.textbox1Source5 = New System.Windows.Forms.TextBox()
        Me.textbox1Source6 = New System.Windows.Forms.TextBox()
        Me.textbox1Source7 = New System.Windows.Forms.TextBox()
        Me.textbox1Source8 = New System.Windows.Forms.TextBox()
        Me.pgMedia = New System.Windows.Forms.TabPage()
        Me.textbox1Source0 = New System.Windows.Forms.TextBox()
        Me.TextBox3 = New System.Windows.Forms.TextBox()
        Me.Label35 = New System.Windows.Forms.Label()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.TextBox24 = New System.Windows.Forms.TextBox()
        Me.TextBox25 = New System.Windows.Forms.TextBox()
        Me.TextBox26 = New System.Windows.Forms.TextBox()
        Me.lblLink = New System.Windows.Forms.Label()
        Me.Label41 = New System.Windows.Forms.Label()
        Me.pgRecommendation = New System.Windows.Forms.TabPage()
        Me.grdRecommend = New System.Windows.Forms.DataGrid()
        Me.DsRFA = New InfoCtr.dsMisc()
        Me.tsRecommend = New System.Windows.Forms.DataGridTableStyle()
        Me.DataGridTextBoxColumn17 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn18 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn23 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn19 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn20 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn21 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn22 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.pgFeedback = New System.Windows.Forms.TabPage()
        Me.grdFeedback = New System.Windows.Forms.DataGrid()
        Me.tsFeedback = New System.Windows.Forms.DataGridTableStyle()
        Me.DataGridTextBoxColumn24 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn25 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn68 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn26 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn27 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn28 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn29 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn56 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn67 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.pgAlert = New System.Windows.Forms.TabPage()
        Me.grdAlert = New System.Windows.Forms.DataGrid()
        Me.tsAlert = New System.Windows.Forms.DataGridTableStyle()
        Me.DataGridTextBoxColumn12 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn13 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn14 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn15 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn16 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.pgAddress = New System.Windows.Forms.TabPage()
        Me.pgOther = New System.Windows.Forms.TabPage()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.lblUseThisOne = New System.Windows.Forms.Label()
        Me.ResourceMainAddressBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.lblEntityNum = New System.Windows.Forms.Label()
        Me.lblEntityType = New System.Windows.Forms.Label()
        Me.lblAddressType = New System.Windows.Forms.Label()
        Me.Label50 = New System.Windows.Forms.Label()
        Me.fldAddrChangedBy = New System.Windows.Forms.TextBox()
        Me.fldAddrChangeDt = New System.Windows.Forms.TextBox()
        Me.pnlReview = New System.Windows.Forms.Panel()
        Me.Label55 = New System.Windows.Forms.Label()
        Me.Label43 = New System.Windows.Forms.Label()
        Me.txtReviewNote = New System.Windows.Forms.TextBox()
        Me.btnReviewed = New System.Windows.Forms.Button()
        Me.cboReview = New InfoCtr.ComboBoxRelaxed()
        Me.dtReview = New InfoCtr.DateTextBox()
        Me.Label44 = New System.Windows.Forms.Label()
        Me.fldReviewStaff = New System.Windows.Forms.TextBox()
        Me.Label45 = New System.Windows.Forms.Label()
        Me.fldCreateDt = New System.Windows.Forms.TextBox()
        Me.Label47 = New System.Windows.Forms.Label()
        Me.fldChangeBy = New System.Windows.Forms.TextBox()
        Me.fldCreatedBy = New System.Windows.Forms.TextBox()
        Me.fldLastChangeDt = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.chkApproved = New System.Windows.Forms.CheckBox()
        Me.Label39 = New System.Windows.Forms.Label()
        Me.textbox1Source9 = New System.Windows.Forms.TextBox()
        Me.TextBox20 = New System.Windows.Forms.TextBox()
        Me.pnlResource = New System.Windows.Forms.Panel()
        Me.lblFlagPerson = New System.Windows.Forms.Label()
        Me.txtResourceName = New System.Windows.Forms.TextBox()
        Me.lblStar = New System.Windows.Forms.Label()
        Me.cboCRGStatus = New InfoCtr.ComboBoxRelaxed()
        Me.cboWhyInactive = New InfoCtr.ComboBoxRelaxed()
        Me.cboSubtype = New InfoCtr.ComboBoxRelaxed()
        Me.DsType1 = New InfoCtr.dsType()
        Me.cboType = New InfoCtr.ComboBoxRelaxed()
        Me.chkActive = New System.Windows.Forms.CheckBox()
        Me.chkCRGWebsite = New System.Windows.Forms.CheckBox()
        Me.txtReferralSource = New System.Windows.Forms.TextBox()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.chkOrg = New System.Windows.Forms.CheckBox()
        Me.chkLocal = New System.Windows.Forms.CheckBox()
        Me.rtbDescriptions = New System.Windows.Forms.RichTextBox()
        Me.tbDescriptions = New System.Windows.Forms.TabControl()
        Me.pgPrivate = New System.Windows.Forms.TabPage()
        Me.pgPublic = New System.Windows.Forms.TabPage()
        Me.pgNewCRG = New System.Windows.Forms.TabPage()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.SqlConnection1 = New System.Data.SqlClient.SqlConnection()
        Me.SqlDeleteCommand1 = New System.Data.SqlClient.SqlCommand()
        Me.SqlInsertCommand1 = New System.Data.SqlClient.SqlCommand()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.btnHelp = New System.Windows.Forms.Button()
        Me.btnSaveExit = New System.Windows.Forms.Button()
        Me.btnNew = New System.Windows.Forms.Button()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.btnImage = New System.Windows.Forms.Button()
        Me.btnOpenFile = New System.Windows.Forms.Button()
        Me.flagInactive = New System.Windows.Forms.Label()
        Me.flagAlert = New System.Windows.Forms.Label()
        Me.MainMenu1 = New System.Windows.Forms.MainMenu(Me.components)
        Me.MenuItem1 = New System.Windows.Forms.MenuItem()
        Me.miNew = New System.Windows.Forms.MenuItem()
        Me.miAttach = New System.Windows.Forms.MenuItem()
        Me.miOpenFile = New System.Windows.Forms.MenuItem()
        Me.miClose = New System.Windows.Forms.MenuItem()
        Me.MenuItem3 = New System.Windows.Forms.MenuItem()
        Me.miSave = New System.Windows.Forms.MenuItem()
        Me.miCancel = New System.Windows.Forms.MenuItem()
        Me.miDelete = New System.Windows.Forms.MenuItem()
        Me.MenuItem7 = New System.Windows.Forms.MenuItem()
        Me.miPrintPublic = New System.Windows.Forms.MenuItem()
        Me.miPrintDescription = New System.Windows.Forms.MenuItem()
        Me.miPrintAllFields = New System.Windows.Forms.MenuItem()
        Me.miRptFeedback = New System.Windows.Forms.MenuItem()
        Me.miHelp = New System.Windows.Forms.MenuItem()
        Me.miHelpGeneral = New System.Windows.Forms.MenuItem()
        Me.miDefinitions = New System.Windows.Forms.MenuItem()
        Me.miCRG = New System.Windows.Forms.MenuItem()
        Me.StatusBar1 = New System.Windows.Forms.StatusBar()
        Me.StatusBarPanel1 = New System.Windows.Forms.StatusBarPanel()
        Me.StatusBarPanelID = New System.Windows.Forms.StatusBarPanel()
        Me.StatusBarPanel2 = New System.Windows.Forms.StatusBarPanel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.OrgNum = New System.Windows.Forms.TextBox()
        Me.ICCResourceID = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.ErrorProvider1 = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.ppStrip = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem2 = New System.Windows.Forms.ToolStripMenuItem()
        Me.Website = New System.Windows.Forms.TextBox()
        Me.MainResourceTableAdapter = New InfoCtr.dsMainResourceTableAdapters.MainResourceTableAdapter()
        Me.ResourceMainAddressTableAdapter = New InfoCtr.dsMainResourceTableAdapters.ResourceMainAddressTableAdapter()
        Me.tbMain.SuspendLayout()
        Me.pgExtra.SuspendLayout()
        CType(Me.MainResourceBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsMainResource1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel4.SuspendLayout()
        CType(Me.grdExtras, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsResourceExtra1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        CType(Me.grdLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tbSource.SuspendLayout()
        Me.oldAddress.SuspendLayout()
        Me.pnlOrgAddress.SuspendLayout()
        Me.pgPublish.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.tbPublishing.SuspendLayout()
        Me.pgArticle.SuspendLayout()
        Me.pgBook.SuspendLayout()
        Me.pgMedia.SuspendLayout()
        Me.pgRecommendation.SuspendLayout()
        CType(Me.grdRecommend, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsRFA, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pgFeedback.SuspendLayout()
        CType(Me.grdFeedback, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pgAlert.SuspendLayout()
        CType(Me.grdAlert, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pgOther.SuspendLayout()
        CType(Me.ResourceMainAddressBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlReview.SuspendLayout()
        Me.pnlResource.SuspendLayout()
        CType(Me.DsType1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tbDescriptions.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.StatusBarPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.StatusBarPanelID, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.StatusBarPanel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel3.SuspendLayout()
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label2.Location = New System.Drawing.Point(13, 17)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(179, 15)
        Me.Label2.TabIndex = 165
        Me.Label2.Text = "RESOURCE DETAIL"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'tbMain
        '
        Me.tbMain.Controls.Add(Me.pgExtra)
        Me.tbMain.Controls.Add(Me.pgRecommendation)
        Me.tbMain.Controls.Add(Me.pgFeedback)
        Me.tbMain.Controls.Add(Me.pgAlert)
        Me.tbMain.Controls.Add(Me.pgAddress)
        Me.tbMain.Controls.Add(Me.pgOther)
        Me.tbMain.Location = New System.Drawing.Point(453, 5)
        Me.tbMain.Name = "tbMain"
        Me.tbMain.SelectedIndex = 0
        Me.tbMain.Size = New System.Drawing.Size(691, 594)
        Me.tbMain.TabIndex = 219
        Me.tbMain.Tag = "tbMain"
        '
        'pgExtra
        '
        Me.pgExtra.Controls.Add(Me.DtAccess)
        Me.pgExtra.Controls.Add(Me.Label40)
        Me.pgExtra.Controls.Add(Me.Panel4)
        Me.pgExtra.Controls.Add(Me.txtURL)
        Me.pgExtra.Controls.Add(Me.Label42)
        Me.pgExtra.Controls.Add(Me.grdExtras)
        Me.pgExtra.Controls.Add(Me.Panel2)
        Me.pgExtra.Controls.Add(Me.grdLocation)
        Me.pgExtra.Controls.Add(Me.tbSource)
        Me.pgExtra.Controls.Add(Me.lblLink)
        Me.pgExtra.Controls.Add(Me.Label41)
        Me.pgExtra.Location = New System.Drawing.Point(4, 22)
        Me.pgExtra.Name = "pgExtra"
        Me.pgExtra.Size = New System.Drawing.Size(683, 568)
        Me.pgExtra.TabIndex = 0
        Me.pgExtra.Tag = "RESOURCE"
        Me.pgExtra.Text = "     RESOURCE     "
        Me.pgExtra.UseVisualStyleBackColor = True
        '
        'DtAccess
        '
        Me.DtAccess.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainResourceBindingSource, "AccessDate", True, System.Windows.Forms.DataSourceUpdateMode.OnValidation, Nothing, "d"))
        Me.DtAccess.Location = New System.Drawing.Point(505, 538)
        Me.DtAccess.Name = "DtAccess"
        Me.DtAccess.Size = New System.Drawing.Size(159, 20)
        Me.DtAccess.TabIndex = 179
        Me.DtAccess.Text = "enter numbers m/d/y."
        Me.ToolTip1.SetToolTip(Me.DtAccess, "enter numbers m/d/y; it will be formatted for you to Monthname d, yyyy.")
        '
        'MainResourceBindingSource
        '
        Me.MainResourceBindingSource.AllowNew = False
        Me.MainResourceBindingSource.DataMember = "MainResource"
        Me.MainResourceBindingSource.DataSource = Me.DsMainResource1
        '
        'DsMainResource1
        '
        Me.DsMainResource1.DataSetName = "dsMainResource"
        Me.DsMainResource1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'Label40
        '
        Me.Label40.AutoSize = True
        Me.Label40.Location = New System.Drawing.Point(419, 541)
        Me.Label40.Name = "Label40"
        Me.Label40.Size = New System.Drawing.Size(80, 13)
        Me.Label40.TabIndex = 178
        Me.Label40.Text = "Date Accessed"
        Me.ToolTip1.SetToolTip(Me.Label40, "enter numbers m/d/y; it will be formatted for you to Monthname d, yyyy.")
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.Panel4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel4.Controls.Add(Me.fldIndexDE)
        Me.Panel4.Controls.Add(Me.btnIndexAdd)
        Me.Panel4.Controls.Add(Me.fldIndexCSV)
        Me.Panel4.Location = New System.Drawing.Point(14, 311)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(375, 105)
        Me.Panel4.TabIndex = 10
        '
        'fldIndexDE
        '
        Me.fldIndexDE.Location = New System.Drawing.Point(9, 77)
        Me.fldIndexDE.Name = "fldIndexDE"
        Me.fldIndexDE.Size = New System.Drawing.Size(135, 20)
        Me.fldIndexDE.TabIndex = 2
        Me.fldIndexDE.Text = "Enter tag here..."
        '
        'btnIndexAdd
        '
        Me.btnIndexAdd.Location = New System.Drawing.Point(174, 77)
        Me.btnIndexAdd.Name = "btnIndexAdd"
        Me.btnIndexAdd.Size = New System.Drawing.Size(188, 20)
        Me.btnIndexAdd.TabIndex = 151
        Me.btnIndexAdd.Text = "Click to Insert or Remove from list."
        Me.btnIndexAdd.UseVisualStyleBackColor = True
        '
        'fldIndexCSV
        '
        Me.fldIndexCSV.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.fldIndexCSV.Location = New System.Drawing.Point(9, 5)
        Me.fldIndexCSV.MaxLength = 2147483647
        Me.fldIndexCSV.Multiline = True
        Me.fldIndexCSV.Name = "fldIndexCSV"
        Me.fldIndexCSV.ReadOnly = True
        Me.fldIndexCSV.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.fldIndexCSV.Size = New System.Drawing.Size(353, 67)
        Me.fldIndexCSV.TabIndex = 1
        Me.fldIndexCSV.TabStop = False
        Me.ToolTip1.SetToolTip(Me.fldIndexCSV, "Items are separated by semi-colon.")
        '
        'txtURL
        '
        Me.txtURL.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtURL.BackColor = System.Drawing.SystemColors.Window
        Me.txtURL.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainResourceBindingSource, "Website", True))
        Me.txtURL.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtURL.ForeColor = System.Drawing.SystemColors.Highlight
        Me.txtURL.Location = New System.Drawing.Point(469, 496)
        Me.txtURL.MaxLength = 255
        Me.txtURL.Multiline = True
        Me.txtURL.Name = "txtURL"
        Me.txtURL.Size = New System.Drawing.Size(203, 39)
        Me.txtURL.TabIndex = 169
        '
        'Label42
        '
        Me.Label42.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label42.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label42.Location = New System.Drawing.Point(11, 296)
        Me.Label42.Name = "Label42"
        Me.Label42.Size = New System.Drawing.Size(160, 18)
        Me.Label42.TabIndex = 148
        Me.Label42.Text = "Tags/Index Terms"
        '
        'grdExtras
        '
        Me.grdExtras.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdExtras.CaptionBackColor = System.Drawing.SystemColors.Control
        Me.grdExtras.CaptionForeColor = System.Drawing.SystemColors.ControlText
        Me.grdExtras.CaptionText = "Author/Contact Grid"
        Me.grdExtras.DataMember = "tblResourceExtra"
        Me.grdExtras.DataSource = Me.DsResourceExtra1
        Me.grdExtras.ForeColor = System.Drawing.Color.DarkGreen
        Me.grdExtras.GridLineStyle = System.Windows.Forms.DataGridLineStyle.None
        Me.grdExtras.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.grdExtras.Location = New System.Drawing.Point(5, 5)
        Me.grdExtras.MaximumSize = New System.Drawing.Size(670, 150)
        Me.grdExtras.Name = "grdExtras"
        Me.grdExtras.ReadOnly = True
        Me.grdExtras.RowHeadersVisible = False
        Me.grdExtras.RowHeaderWidth = 15
        Me.grdExtras.Size = New System.Drawing.Size(670, 150)
        Me.grdExtras.TabIndex = 133
        Me.grdExtras.TableStyles.AddRange(New System.Windows.Forms.DataGridTableStyle() {Me.tsAuthors, Me.tsContacts, Me.tsEvents, Me.tsResourceName})
        Me.grdExtras.Tag = ""
        Me.ToolTip1.SetToolTip(Me.grdExtras, "to Edit: doubleclick.  to Insert Author/Editor/Contact: click New button")
        '
        'DsResourceExtra1
        '
        Me.DsResourceExtra1.DataSetName = "dsResourceExtra"
        Me.DsResourceExtra1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'tsAuthors
        '
        Me.tsAuthors.DataGrid = Me.grdExtras
        Me.tsAuthors.GridColumnStyles.AddRange(New System.Windows.Forms.DataGridColumnStyle() {Me.DataGridTextBoxColumn5, Me.DataGridTextBoxColumn6, Me.DataGridTextBoxColumn7, Me.DataGridTextBoxColumn8, Me.DataGridTextBoxColumn66, Me.DataGridTextBoxColumn9, Me.DataGridTextBoxColumn10, Me.DataGridTextBoxColumn11, Me.DataGridTextBoxColumn64})
        Me.tsAuthors.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.tsAuthors.ReadOnly = True
        Me.tsAuthors.RowHeaderWidth = 30
        '
        'DataGridTextBoxColumn5
        '
        Me.DataGridTextBoxColumn5.Format = ""
        Me.DataGridTextBoxColumn5.FormatInfo = Nothing
        Me.DataGridTextBoxColumn5.HeaderText = "ExtraID"
        Me.DataGridTextBoxColumn5.MappingName = "ResourceExtraID"
        Me.DataGridTextBoxColumn5.Width = 0
        '
        'DataGridTextBoxColumn6
        '
        Me.DataGridTextBoxColumn6.Format = ""
        Me.DataGridTextBoxColumn6.FormatInfo = Nothing
        Me.DataGridTextBoxColumn6.HeaderText = "ResourceNum"
        Me.DataGridTextBoxColumn6.MappingName = "ResourceNum"
        Me.DataGridTextBoxColumn6.Width = 0
        '
        'DataGridTextBoxColumn7
        '
        Me.DataGridTextBoxColumn7.Format = ""
        Me.DataGridTextBoxColumn7.FormatInfo = Nothing
        Me.DataGridTextBoxColumn7.HeaderText = "Lastname"
        Me.DataGridTextBoxColumn7.MappingName = "LastName"
        Me.DataGridTextBoxColumn7.Width = 90
        '
        'DataGridTextBoxColumn8
        '
        Me.DataGridTextBoxColumn8.Format = ""
        Me.DataGridTextBoxColumn8.FormatInfo = Nothing
        Me.DataGridTextBoxColumn8.HeaderText = "Firstname"
        Me.DataGridTextBoxColumn8.MappingName = "FirstName"
        Me.DataGridTextBoxColumn8.Width = 90
        '
        'DataGridTextBoxColumn66
        '
        Me.DataGridTextBoxColumn66.Format = ""
        Me.DataGridTextBoxColumn66.FormatInfo = Nothing
        Me.DataGridTextBoxColumn66.HeaderText = "Prefix"
        Me.DataGridTextBoxColumn66.MappingName = "Prefix"
        Me.DataGridTextBoxColumn66.Width = 45
        '
        'DataGridTextBoxColumn9
        '
        Me.DataGridTextBoxColumn9.Format = ""
        Me.DataGridTextBoxColumn9.FormatInfo = Nothing
        Me.DataGridTextBoxColumn9.HeaderText = "Middle"
        Me.DataGridTextBoxColumn9.MappingName = "Middle"
        Me.DataGridTextBoxColumn9.Width = 45
        '
        'DataGridTextBoxColumn10
        '
        Me.DataGridTextBoxColumn10.Format = ""
        Me.DataGridTextBoxColumn10.FormatInfo = Nothing
        Me.DataGridTextBoxColumn10.HeaderText = "Suffix"
        Me.DataGridTextBoxColumn10.MappingName = "Suffix"
        Me.DataGridTextBoxColumn10.Width = 45
        '
        'DataGridTextBoxColumn11
        '
        Me.DataGridTextBoxColumn11.Format = ""
        Me.DataGridTextBoxColumn11.FormatInfo = Nothing
        Me.DataGridTextBoxColumn11.HeaderText = "Author/Ed"
        Me.DataGridTextBoxColumn11.MappingName = "AuthorEditor"
        Me.DataGridTextBoxColumn11.Width = 60
        '
        'DataGridTextBoxColumn64
        '
        Me.DataGridTextBoxColumn64.Format = ""
        Me.DataGridTextBoxColumn64.FormatInfo = Nothing
        Me.DataGridTextBoxColumn64.HeaderText = "Notes"
        Me.DataGridTextBoxColumn64.MappingName = "Notes"
        Me.DataGridTextBoxColumn64.Width = 175
        '
        'tsContacts
        '
        Me.tsContacts.DataGrid = Me.grdExtras
        Me.tsContacts.GridColumnStyles.AddRange(New System.Windows.Forms.DataGridColumnStyle() {Me.DataGridTextBoxColumn30, Me.DataGridTextBoxColumn31, Me.DataGridTextBoxColumn32, Me.DataGridTextBoxColumn33, Me.DataGridTextBoxColumn34, Me.DataGridTextBoxColumn35, Me.DataGridTextBoxColumn36, Me.DataGridTextBoxColumn37, Me.DataGridTextBoxColumn38, Me.DataGridTextBoxColumn39, Me.DataGridTextBoxColumn41, Me.DataGridTextBoxColumn40})
        Me.tsContacts.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.tsContacts.ReadOnly = True
        Me.tsContacts.RowHeaderWidth = 20
        '
        'DataGridTextBoxColumn30
        '
        Me.DataGridTextBoxColumn30.Format = ""
        Me.DataGridTextBoxColumn30.FormatInfo = Nothing
        Me.DataGridTextBoxColumn30.HeaderText = "ExtraID"
        Me.DataGridTextBoxColumn30.MappingName = "ResourceExtraID"
        Me.DataGridTextBoxColumn30.Width = 0
        '
        'DataGridTextBoxColumn31
        '
        Me.DataGridTextBoxColumn31.Format = ""
        Me.DataGridTextBoxColumn31.FormatInfo = Nothing
        Me.DataGridTextBoxColumn31.HeaderText = "ResourceNum"
        Me.DataGridTextBoxColumn31.MappingName = "ResourceNum"
        Me.DataGridTextBoxColumn31.Width = 0
        '
        'DataGridTextBoxColumn32
        '
        Me.DataGridTextBoxColumn32.Format = ""
        Me.DataGridTextBoxColumn32.FormatInfo = Nothing
        Me.DataGridTextBoxColumn32.HeaderText = "LastName"
        Me.DataGridTextBoxColumn32.MappingName = "LastName"
        Me.DataGridTextBoxColumn32.Width = 75
        '
        'DataGridTextBoxColumn33
        '
        Me.DataGridTextBoxColumn33.Format = ""
        Me.DataGridTextBoxColumn33.FormatInfo = Nothing
        Me.DataGridTextBoxColumn33.HeaderText = "FirstName"
        Me.DataGridTextBoxColumn33.MappingName = "FirstName"
        Me.DataGridTextBoxColumn33.Width = 75
        '
        'DataGridTextBoxColumn34
        '
        Me.DataGridTextBoxColumn34.Format = ""
        Me.DataGridTextBoxColumn34.FormatInfo = Nothing
        Me.DataGridTextBoxColumn34.HeaderText = "Prefix"
        Me.DataGridTextBoxColumn34.MappingName = "Prefix"
        Me.DataGridTextBoxColumn34.Width = 45
        '
        'DataGridTextBoxColumn35
        '
        Me.DataGridTextBoxColumn35.Format = ""
        Me.DataGridTextBoxColumn35.FormatInfo = Nothing
        Me.DataGridTextBoxColumn35.HeaderText = "Middle"
        Me.DataGridTextBoxColumn35.MappingName = "Middle"
        Me.DataGridTextBoxColumn35.Width = 45
        '
        'DataGridTextBoxColumn36
        '
        Me.DataGridTextBoxColumn36.Format = ""
        Me.DataGridTextBoxColumn36.FormatInfo = Nothing
        Me.DataGridTextBoxColumn36.HeaderText = "Suffix"
        Me.DataGridTextBoxColumn36.MappingName = "Suffix"
        Me.DataGridTextBoxColumn36.Width = 45
        '
        'DataGridTextBoxColumn37
        '
        Me.DataGridTextBoxColumn37.Format = ""
        Me.DataGridTextBoxColumn37.FormatInfo = Nothing
        Me.DataGridTextBoxColumn37.HeaderText = "JobTitle"
        Me.DataGridTextBoxColumn37.MappingName = "JobTitle"
        Me.DataGridTextBoxColumn37.Width = 75
        '
        'DataGridTextBoxColumn38
        '
        Me.DataGridTextBoxColumn38.Format = ""
        Me.DataGridTextBoxColumn38.FormatInfo = Nothing
        Me.DataGridTextBoxColumn38.HeaderText = "Phone"
        Me.DataGridTextBoxColumn38.MappingName = "Phone"
        Me.DataGridTextBoxColumn38.Width = 75
        '
        'DataGridTextBoxColumn39
        '
        Me.DataGridTextBoxColumn39.Format = ""
        Me.DataGridTextBoxColumn39.FormatInfo = Nothing
        Me.DataGridTextBoxColumn39.HeaderText = "Email"
        Me.DataGridTextBoxColumn39.MappingName = "EMail"
        Me.DataGridTextBoxColumn39.Width = 75
        '
        'DataGridTextBoxColumn41
        '
        Me.DataGridTextBoxColumn41.Format = ""
        Me.DataGridTextBoxColumn41.FormatInfo = Nothing
        Me.DataGridTextBoxColumn41.HeaderText = "Phone2"
        Me.DataGridTextBoxColumn41.MappingName = "Phone2"
        Me.DataGridTextBoxColumn41.Width = 75
        '
        'DataGridTextBoxColumn40
        '
        Me.DataGridTextBoxColumn40.Format = ""
        Me.DataGridTextBoxColumn40.FormatInfo = Nothing
        Me.DataGridTextBoxColumn40.HeaderText = "Notes"
        Me.DataGridTextBoxColumn40.MappingName = "Notes"
        Me.DataGridTextBoxColumn40.Width = 75
        '
        'tsEvents
        '
        Me.tsEvents.DataGrid = Me.grdExtras
        Me.tsEvents.GridColumnStyles.AddRange(New System.Windows.Forms.DataGridColumnStyle() {Me.DataGridTextBoxColumn42, Me.DataGridTextBoxColumn43, Me.DataGridTextBoxColumn44, Me.DataGridTextBoxColumn45, Me.DataGridTextBoxColumn46, Me.DataGridTextBoxColumn47, Me.DataGridTextBoxColumn48, Me.DataGridTextBoxColumn4, Me.DataGridTextBoxColumn57, Me.DataGridTextBoxColumn60, Me.DataGridTextBoxColumn58, Me.DataGridTextBoxColumn59, Me.DataGridTextBoxColumn61, Me.DataGridTextBoxColumn62, Me.DataGridTextBoxColumn63})
        Me.tsEvents.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.tsEvents.ReadOnly = True
        Me.tsEvents.RowHeaderWidth = 20
        '
        'DataGridTextBoxColumn42
        '
        Me.DataGridTextBoxColumn42.Format = ""
        Me.DataGridTextBoxColumn42.FormatInfo = Nothing
        Me.DataGridTextBoxColumn42.HeaderText = "ExtraID"
        Me.DataGridTextBoxColumn42.MappingName = "ResourceExtraID"
        Me.DataGridTextBoxColumn42.Width = 0
        '
        'DataGridTextBoxColumn43
        '
        Me.DataGridTextBoxColumn43.Format = ""
        Me.DataGridTextBoxColumn43.FormatInfo = Nothing
        Me.DataGridTextBoxColumn43.HeaderText = "ResourceNum"
        Me.DataGridTextBoxColumn43.MappingName = "ResourceNum"
        Me.DataGridTextBoxColumn43.Width = 0
        '
        'DataGridTextBoxColumn44
        '
        Me.DataGridTextBoxColumn44.Format = ""
        Me.DataGridTextBoxColumn44.FormatInfo = Nothing
        Me.DataGridTextBoxColumn44.HeaderText = "EventDate"
        Me.DataGridTextBoxColumn44.MappingName = "EventDate"
        Me.DataGridTextBoxColumn44.Width = 75
        '
        'DataGridTextBoxColumn45
        '
        Me.DataGridTextBoxColumn45.Format = ""
        Me.DataGridTextBoxColumn45.FormatInfo = Nothing
        Me.DataGridTextBoxColumn45.HeaderText = "Location"
        Me.DataGridTextBoxColumn45.MappingName = "Location"
        Me.DataGridTextBoxColumn45.Width = 120
        '
        'DataGridTextBoxColumn46
        '
        Me.DataGridTextBoxColumn46.Format = ""
        Me.DataGridTextBoxColumn46.FormatInfo = Nothing
        Me.DataGridTextBoxColumn46.HeaderText = "Annual?"
        Me.DataGridTextBoxColumn46.MappingName = "EventAnnual"
        Me.DataGridTextBoxColumn46.Width = 75
        '
        'DataGridTextBoxColumn47
        '
        Me.DataGridTextBoxColumn47.Format = ""
        Me.DataGridTextBoxColumn47.FormatInfo = Nothing
        Me.DataGridTextBoxColumn47.HeaderText = "Phone"
        Me.DataGridTextBoxColumn47.MappingName = "Phone"
        Me.DataGridTextBoxColumn47.Width = 75
        '
        'DataGridTextBoxColumn48
        '
        Me.DataGridTextBoxColumn48.Format = ""
        Me.DataGridTextBoxColumn48.FormatInfo = Nothing
        Me.DataGridTextBoxColumn48.HeaderText = "Notes"
        Me.DataGridTextBoxColumn48.MappingName = "Notes"
        Me.DataGridTextBoxColumn48.Width = 75
        '
        'DataGridTextBoxColumn4
        '
        Me.DataGridTextBoxColumn4.Format = ""
        Me.DataGridTextBoxColumn4.FormatInfo = Nothing
        Me.DataGridTextBoxColumn4.HeaderText = "LastName"
        Me.DataGridTextBoxColumn4.MappingName = "LastName"
        Me.DataGridTextBoxColumn4.Width = 50
        '
        'DataGridTextBoxColumn57
        '
        Me.DataGridTextBoxColumn57.Format = ""
        Me.DataGridTextBoxColumn57.FormatInfo = Nothing
        Me.DataGridTextBoxColumn57.HeaderText = "Firstname"
        Me.DataGridTextBoxColumn57.MappingName = "FirstName"
        Me.DataGridTextBoxColumn57.Width = 50
        '
        'DataGridTextBoxColumn60
        '
        Me.DataGridTextBoxColumn60.Format = ""
        Me.DataGridTextBoxColumn60.FormatInfo = Nothing
        Me.DataGridTextBoxColumn60.HeaderText = "Prefix"
        Me.DataGridTextBoxColumn60.MappingName = "Prefix"
        Me.DataGridTextBoxColumn60.Width = 75
        '
        'DataGridTextBoxColumn58
        '
        Me.DataGridTextBoxColumn58.Format = ""
        Me.DataGridTextBoxColumn58.FormatInfo = Nothing
        Me.DataGridTextBoxColumn58.HeaderText = "Middle"
        Me.DataGridTextBoxColumn58.MappingName = "Middle"
        Me.DataGridTextBoxColumn58.Width = 75
        '
        'DataGridTextBoxColumn59
        '
        Me.DataGridTextBoxColumn59.Format = ""
        Me.DataGridTextBoxColumn59.FormatInfo = Nothing
        Me.DataGridTextBoxColumn59.HeaderText = "Suffix"
        Me.DataGridTextBoxColumn59.MappingName = "Suffix"
        Me.DataGridTextBoxColumn59.Width = 75
        '
        'DataGridTextBoxColumn61
        '
        Me.DataGridTextBoxColumn61.Format = ""
        Me.DataGridTextBoxColumn61.FormatInfo = Nothing
        Me.DataGridTextBoxColumn61.HeaderText = "JobTitle"
        Me.DataGridTextBoxColumn61.MappingName = "JobTitle"
        Me.DataGridTextBoxColumn61.Width = 75
        '
        'DataGridTextBoxColumn62
        '
        Me.DataGridTextBoxColumn62.Format = ""
        Me.DataGridTextBoxColumn62.FormatInfo = Nothing
        Me.DataGridTextBoxColumn62.Width = 75
        '
        'DataGridTextBoxColumn63
        '
        Me.DataGridTextBoxColumn63.Format = ""
        Me.DataGridTextBoxColumn63.FormatInfo = Nothing
        Me.DataGridTextBoxColumn63.HeaderText = "Email"
        Me.DataGridTextBoxColumn63.MappingName = "EMail"
        Me.DataGridTextBoxColumn63.Width = 75
        '
        'tsResourceName
        '
        Me.tsResourceName.DataGrid = Me.grdExtras
        Me.tsResourceName.GridColumnStyles.AddRange(New System.Windows.Forms.DataGridColumnStyle() {Me.DataGridTextBoxColumn49, Me.DataGridTextBoxColumn50, Me.DataGridTextBoxColumn51, Me.DataGridTextBoxColumn52, Me.DataGridTextBoxColumn53, Me.DataGridTextBoxColumn54, Me.DataGridTextBoxColumn55, Me.DataGridTextBoxColumn65})
        Me.tsResourceName.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.tsResourceName.ReadOnly = True
        Me.tsResourceName.RowHeaderWidth = 20
        '
        'DataGridTextBoxColumn49
        '
        Me.DataGridTextBoxColumn49.Format = ""
        Me.DataGridTextBoxColumn49.FormatInfo = Nothing
        Me.DataGridTextBoxColumn49.HeaderText = "ExtraID"
        Me.DataGridTextBoxColumn49.MappingName = "ResourceExtraID"
        Me.DataGridTextBoxColumn49.Width = 0
        '
        'DataGridTextBoxColumn50
        '
        Me.DataGridTextBoxColumn50.Format = ""
        Me.DataGridTextBoxColumn50.FormatInfo = Nothing
        Me.DataGridTextBoxColumn50.HeaderText = "ResourceNum"
        Me.DataGridTextBoxColumn50.MappingName = "ResourceNum"
        Me.DataGridTextBoxColumn50.Width = 0
        '
        'DataGridTextBoxColumn51
        '
        Me.DataGridTextBoxColumn51.Format = ""
        Me.DataGridTextBoxColumn51.FormatInfo = Nothing
        Me.DataGridTextBoxColumn51.HeaderText = "LastName"
        Me.DataGridTextBoxColumn51.MappingName = "LastName"
        Me.DataGridTextBoxColumn51.Width = 90
        '
        'DataGridTextBoxColumn52
        '
        Me.DataGridTextBoxColumn52.Format = ""
        Me.DataGridTextBoxColumn52.FormatInfo = Nothing
        Me.DataGridTextBoxColumn52.HeaderText = "FirstName"
        Me.DataGridTextBoxColumn52.MappingName = "FirstName"
        Me.DataGridTextBoxColumn52.Width = 90
        '
        'DataGridTextBoxColumn53
        '
        Me.DataGridTextBoxColumn53.Format = ""
        Me.DataGridTextBoxColumn53.FormatInfo = Nothing
        Me.DataGridTextBoxColumn53.HeaderText = "Prefix"
        Me.DataGridTextBoxColumn53.MappingName = "Prefix"
        Me.DataGridTextBoxColumn53.Width = 45
        '
        'DataGridTextBoxColumn54
        '
        Me.DataGridTextBoxColumn54.Format = ""
        Me.DataGridTextBoxColumn54.FormatInfo = Nothing
        Me.DataGridTextBoxColumn54.HeaderText = "Middle"
        Me.DataGridTextBoxColumn54.MappingName = "Middle"
        Me.DataGridTextBoxColumn54.Width = 45
        '
        'DataGridTextBoxColumn55
        '
        Me.DataGridTextBoxColumn55.Format = ""
        Me.DataGridTextBoxColumn55.FormatInfo = Nothing
        Me.DataGridTextBoxColumn55.HeaderText = "Suffix"
        Me.DataGridTextBoxColumn55.MappingName = "Suffix"
        Me.DataGridTextBoxColumn55.Width = 45
        '
        'DataGridTextBoxColumn65
        '
        Me.DataGridTextBoxColumn65.Format = ""
        Me.DataGridTextBoxColumn65.FormatInfo = Nothing
        Me.DataGridTextBoxColumn65.HeaderText = "Notes"
        Me.DataGridTextBoxColumn65.MappingName = "Notes"
        Me.DataGridTextBoxColumn65.Width = 150
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.Label52)
        Me.Panel2.Controls.Add(Me.cboKey1)
        Me.Panel2.Controls.Add(Me.cboKey2)
        Me.Panel2.Controls.Add(Me.cboKey3)
        Me.Panel2.Controls.Add(Me.cboKey4)
        Me.Panel2.Location = New System.Drawing.Point(19, 181)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(375, 107)
        Me.Panel2.TabIndex = 8
        '
        'Label52
        '
        Me.Label52.AutoSize = True
        Me.Label52.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label52.Location = New System.Drawing.Point(3, 9)
        Me.Label52.MaximumSize = New System.Drawing.Size(8, 0)
        Me.Label52.MinimumSize = New System.Drawing.Size(0, 91)
        Me.Label52.Name = "Label52"
        Me.Label52.Size = New System.Drawing.Size(8, 91)
        Me.Label52.TabIndex = 16
        Me.Label52.Text = "1" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "2" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "3" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "4"
        '
        'cboKey1
        '
        Me.cboKey1.AllowDrop = True
        Me.cboKey1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboKey1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboKey1.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.MainResourceBindingSource, "Keyword1", True))
        Me.cboKey1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboKey1.DropDownWidth = 500
        Me.cboKey1.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboKey1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cboKey1.Location = New System.Drawing.Point(14, 6)
        Me.cboKey1.Name = "cboKey1"
        Me.cboKey1.RestrictContentToListItems = True
        Me.cboKey1.Size = New System.Drawing.Size(350, 20)
        Me.cboKey1.TabIndex = 5
        Me.cboKey1.Tag = "FIRST KEYWORD"
        Me.ToolTip1.SetToolTip(Me.cboKey1, "Right click for searching.")
        '
        'cboKey2
        '
        Me.cboKey2.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.cboKey2.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboKey2.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboKey2.BackColor = System.Drawing.SystemColors.Window
        Me.cboKey2.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.MainResourceBindingSource, "Keyword2", True))
        Me.cboKey2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboKey2.DropDownWidth = 500
        Me.cboKey2.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboKey2.Location = New System.Drawing.Point(15, 32)
        Me.cboKey2.Name = "cboKey2"
        Me.cboKey2.RestrictContentToListItems = True
        Me.cboKey2.Size = New System.Drawing.Size(350, 20)
        Me.cboKey2.TabIndex = 6
        Me.ToolTip1.SetToolTip(Me.cboKey2, "Right click for searching.")
        '
        'cboKey3
        '
        Me.cboKey3.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.cboKey3.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboKey3.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboKey3.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.MainResourceBindingSource, "Keyword3", True))
        Me.cboKey3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboKey3.DropDownWidth = 500
        Me.cboKey3.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboKey3.Location = New System.Drawing.Point(16, 57)
        Me.cboKey3.Name = "cboKey3"
        Me.cboKey3.RestrictContentToListItems = True
        Me.cboKey3.Size = New System.Drawing.Size(350, 20)
        Me.cboKey3.TabIndex = 7
        Me.ToolTip1.SetToolTip(Me.cboKey3, "Right click for searching.")
        '
        'cboKey4
        '
        Me.cboKey4.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboKey4.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboKey4.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.MainResourceBindingSource, "Keyword4", True))
        Me.cboKey4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboKey4.DropDownWidth = 500
        Me.cboKey4.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboKey4.Location = New System.Drawing.Point(15, 82)
        Me.cboKey4.Name = "cboKey4"
        Me.cboKey4.RestrictContentToListItems = True
        Me.cboKey4.Size = New System.Drawing.Size(350, 20)
        Me.cboKey4.TabIndex = 8
        Me.ToolTip1.SetToolTip(Me.cboKey4, "Right click for searching.")
        '
        'grdLocation
        '
        Me.grdLocation.CaptionBackColor = System.Drawing.SystemColors.Menu
        Me.grdLocation.CaptionForeColor = System.Drawing.SystemColors.ControlText
        Me.grdLocation.CaptionText = "Locations of Resource at Center offices"
        Me.grdLocation.DataMember = "GetResourceLocation"
        Me.grdLocation.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdLocation.GridLineStyle = System.Windows.Forms.DataGridLineStyle.None
        Me.grdLocation.HeaderBackColor = System.Drawing.SystemColors.ControlLightLight
        Me.grdLocation.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.grdLocation.Location = New System.Drawing.Point(14, 424)
        Me.grdLocation.Name = "grdLocation"
        Me.grdLocation.ReadOnly = True
        Me.grdLocation.RowHeadersVisible = False
        Me.grdLocation.RowHeaderWidth = 30
        Me.grdLocation.Size = New System.Drawing.Size(390, 134)
        Me.grdLocation.TabIndex = 125
        Me.grdLocation.TableStyles.AddRange(New System.Windows.Forms.DataGridTableStyle() {Me.DataGridTableStyle1})
        Me.grdLocation.Tag = ""
        '
        'DataGridTableStyle1
        '
        Me.DataGridTableStyle1.DataGrid = Me.grdLocation
        Me.DataGridTableStyle1.GridColumnStyles.AddRange(New System.Windows.Forms.DataGridColumnStyle() {Me.DataGridTextBoxColumn1, Me.DataGridTextBoxColumn3, Me.DataGridTextBoxColumn2, Me.DataGridTextBoxColumn69})
        Me.DataGridTableStyle1.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.DataGridTableStyle1.MappingName = "GetResourceLocation"
        Me.DataGridTableStyle1.RowHeaderWidth = 30
        '
        'DataGridTextBoxColumn1
        '
        Me.DataGridTextBoxColumn1.Format = ""
        Me.DataGridTextBoxColumn1.FormatInfo = Nothing
        Me.DataGridTextBoxColumn1.HeaderText = "ID"
        Me.DataGridTextBoxColumn1.MappingName = "ResourceLocationID"
        Me.DataGridTextBoxColumn1.Width = 0
        '
        'DataGridTextBoxColumn3
        '
        Me.DataGridTextBoxColumn3.Format = ""
        Me.DataGridTextBoxColumn3.FormatInfo = Nothing
        Me.DataGridTextBoxColumn3.HeaderText = "Region"
        Me.DataGridTextBoxColumn3.MappingName = "SatelliteRegion"
        Me.DataGridTextBoxColumn3.Width = 75
        '
        'DataGridTextBoxColumn2
        '
        Me.DataGridTextBoxColumn2.Format = ""
        Me.DataGridTextBoxColumn2.FormatInfo = Nothing
        Me.DataGridTextBoxColumn2.HeaderText = "Status/Location"
        Me.DataGridTextBoxColumn2.MappingName = "Location"
        Me.DataGridTextBoxColumn2.Width = 200
        '
        'DataGridTextBoxColumn69
        '
        Me.DataGridTextBoxColumn69.Format = ""
        Me.DataGridTextBoxColumn69.FormatInfo = Nothing
        Me.DataGridTextBoxColumn69.HeaderText = "Edition"
        Me.DataGridTextBoxColumn69.MappingName = "Edition"
        Me.DataGridTextBoxColumn69.Width = 75
        '
        'tbSource
        '
        Me.tbSource.Controls.Add(Me.oldAddress)
        Me.tbSource.Controls.Add(Me.pgPublish)
        Me.tbSource.Location = New System.Drawing.Point(408, 165)
        Me.tbSource.Margin = New System.Windows.Forms.Padding(0)
        Me.tbSource.Name = "tbSource"
        Me.tbSource.SelectedIndex = 0
        Me.tbSource.Size = New System.Drawing.Size(271, 328)
        Me.tbSource.TabIndex = 143
        Me.tbSource.TabStop = False
        Me.tbSource.Tag = ""
        Me.ToolTip1.SetToolTip(Me.tbSource, "use Address tab for Organizations; Publishing tab for Books, Articles, etc.)")
        '
        'oldAddress
        '
        Me.oldAddress.Controls.Add(Me.pnlOrgAddress)
        Me.oldAddress.Controls.Add(Me.PhoneExtension)
        Me.oldAddress.Controls.Add(Me.Label37)
        Me.oldAddress.Controls.Add(Me.Label19)
        Me.oldAddress.Controls.Add(Me.Label18)
        Me.oldAddress.Controls.Add(Me.Label17)
        Me.oldAddress.Controls.Add(Me.Label16)
        Me.oldAddress.Controls.Add(Me.Label15)
        Me.oldAddress.Controls.Add(Me.Label14)
        Me.oldAddress.Controls.Add(Me.Label12)
        Me.oldAddress.Controls.Add(Me.Label11)
        Me.oldAddress.Controls.Add(Me.Label10)
        Me.oldAddress.Controls.Add(Me.Country)
        Me.oldAddress.Controls.Add(Me.Zip)
        Me.oldAddress.Controls.Add(Me.Address2)
        Me.oldAddress.Controls.Add(Me.City)
        Me.oldAddress.Controls.Add(Me.State)
        Me.oldAddress.Controls.Add(Me.Email)
        Me.oldAddress.Controls.Add(Me.Address1)
        Me.oldAddress.Controls.Add(Me.Telephone2)
        Me.oldAddress.Controls.Add(Me.Telephone3)
        Me.oldAddress.Controls.Add(Me.Label8)
        Me.oldAddress.Controls.Add(Me.lblAddressRequired)
        Me.oldAddress.Controls.Add(Me.Telephone)
        Me.oldAddress.Controls.Add(Me.txtSponsoringOrg)
        Me.oldAddress.Controls.Add(Me.lblAuxOrgName)
        Me.oldAddress.Location = New System.Drawing.Point(4, 22)
        Me.oldAddress.Name = "oldAddress"
        Me.oldAddress.Size = New System.Drawing.Size(263, 302)
        Me.oldAddress.TabIndex = 1
        Me.oldAddress.Text = "Address of Resource"
        Me.oldAddress.Visible = False
        '
        'pnlOrgAddress
        '
        Me.pnlOrgAddress.BackColor = System.Drawing.Color.AliceBlue
        Me.pnlOrgAddress.Controls.Add(Me.btnOrg)
        Me.pnlOrgAddress.Controls.Add(Me.rtxtOrgAddress)
        Me.pnlOrgAddress.Location = New System.Drawing.Point(3, 19)
        Me.pnlOrgAddress.Name = "pnlOrgAddress"
        Me.pnlOrgAddress.Size = New System.Drawing.Size(260, 298)
        Me.pnlOrgAddress.TabIndex = 149
        '
        'btnOrg
        '
        Me.btnOrg.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnOrg.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnOrg.Location = New System.Drawing.Point(28, 253)
        Me.btnOrg.Name = "btnOrg"
        Me.btnOrg.Size = New System.Drawing.Size(204, 26)
        Me.btnOrg.TabIndex = 88
        Me.btnOrg.Text = "Edit this Address"
        Me.ToolTip1.SetToolTip(Me.btnOrg, "This Organization is also in our Mailing List database.")
        '
        'rtxtOrgAddress
        '
        Me.rtxtOrgAddress.Location = New System.Drawing.Point(4, 17)
        Me.rtxtOrgAddress.Name = "rtxtOrgAddress"
        Me.rtxtOrgAddress.ReadOnly = True
        Me.rtxtOrgAddress.Size = New System.Drawing.Size(253, 230)
        Me.rtxtOrgAddress.TabIndex = 13
        Me.rtxtOrgAddress.Text = ""
        Me.ToolTip1.SetToolTip(Me.rtxtOrgAddress, "This Organization is also in our Mailing List database.  To edit address, click E" & _
        "dit this Address button below.")
        '
        'PhoneExtension
        '
        Me.PhoneExtension.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainResourceBindingSource, "PhoneExtension", True))
        Me.PhoneExtension.Location = New System.Drawing.Point(207, 97)
        Me.PhoneExtension.Name = "PhoneExtension"
        Me.PhoneExtension.Size = New System.Drawing.Size(44, 20)
        Me.PhoneExtension.TabIndex = 152
        '
        'Label37
        '
        Me.Label37.Location = New System.Drawing.Point(180, 96)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(26, 19)
        Me.Label37.TabIndex = 151
        Me.Label37.Text = "Ext."
        Me.Label37.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label19
        '
        Me.Label19.Location = New System.Drawing.Point(29, 256)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(48, 16)
        Me.Label19.TabIndex = 97
        Me.Label19.Text = "Country"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label18
        '
        Me.Label18.Location = New System.Drawing.Point(119, 232)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(23, 16)
        Me.Label18.TabIndex = 96
        Me.Label18.Text = "Zip"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label17
        '
        Me.Label17.Location = New System.Drawing.Point(38, 228)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(40, 16)
        Me.Label17.TabIndex = 95
        Me.Label17.Text = "State"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label16
        '
        Me.Label16.Location = New System.Drawing.Point(22, 202)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(56, 16)
        Me.Label16.TabIndex = 94
        Me.Label16.Text = "City"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label15
        '
        Me.Label15.Location = New System.Drawing.Point(22, 179)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(56, 16)
        Me.Label15.TabIndex = 93
        Me.Label15.Text = "Address 2"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label14
        '
        Me.Label14.Location = New System.Drawing.Point(22, 156)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(56, 16)
        Me.Label14.TabIndex = 92
        Me.Label14.Text = "Address 1"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label12
        '
        Me.Label12.Location = New System.Drawing.Point(22, 132)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(56, 16)
        Me.Label12.TabIndex = 90
        Me.Label12.Text = "E-mail"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label11
        '
        Me.Label11.Location = New System.Drawing.Point(22, 109)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(56, 16)
        Me.Label11.TabIndex = 89
        Me.Label11.Text = "Fax"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label10
        '
        Me.Label10.Location = New System.Drawing.Point(22, 86)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(56, 16)
        Me.Label10.TabIndex = 88
        Me.Label10.Text = "Phone"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Country
        '
        Me.Country.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainResourceBindingSource, "Country", True))
        Me.Country.Location = New System.Drawing.Point(80, 254)
        Me.Country.MaxLength = 50
        Me.Country.Name = "Country"
        Me.Country.Size = New System.Drawing.Size(140, 20)
        Me.Country.TabIndex = 86
        '
        'Zip
        '
        Me.Zip.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainResourceBindingSource, "Zip", True))
        Me.Zip.Location = New System.Drawing.Point(144, 233)
        Me.Zip.MaxLength = 50
        Me.Zip.Name = "Zip"
        Me.Zip.Size = New System.Drawing.Size(76, 20)
        Me.Zip.TabIndex = 85
        '
        'Address2
        '
        Me.Address2.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainResourceBindingSource, "Address2", True))
        Me.Address2.Location = New System.Drawing.Point(80, 183)
        Me.Address2.MaxLength = 100
        Me.Address2.Name = "Address2"
        Me.Address2.Size = New System.Drawing.Size(165, 20)
        Me.Address2.TabIndex = 82
        '
        'City
        '
        Me.City.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainResourceBindingSource, "City", True))
        Me.City.Location = New System.Drawing.Point(80, 207)
        Me.City.MaxLength = 50
        Me.City.Name = "City"
        Me.City.Size = New System.Drawing.Size(140, 20)
        Me.City.TabIndex = 83
        '
        'State
        '
        Me.State.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainResourceBindingSource, "State", True))
        Me.State.Location = New System.Drawing.Point(80, 231)
        Me.State.MaxLength = 50
        Me.State.Name = "State"
        Me.State.Size = New System.Drawing.Size(34, 20)
        Me.State.TabIndex = 84
        '
        'Email
        '
        Me.Email.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainResourceBindingSource, "Email", True))
        Me.Email.Location = New System.Drawing.Point(80, 133)
        Me.Email.MaxLength = 100
        Me.Email.Name = "Email"
        Me.Email.Size = New System.Drawing.Size(163, 20)
        Me.Email.TabIndex = 79
        '
        'Address1
        '
        Me.Address1.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainResourceBindingSource, "Address1", True))
        Me.Address1.Location = New System.Drawing.Point(80, 159)
        Me.Address1.MaxLength = 100
        Me.Address1.Name = "Address1"
        Me.Address1.Size = New System.Drawing.Size(165, 20)
        Me.Address1.TabIndex = 81
        '
        'Telephone2
        '
        Me.Telephone2.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainResourceBindingSource, "Telephone2", True))
        Me.Telephone2.Location = New System.Drawing.Point(80, 85)
        Me.Telephone2.MaxLength = 30
        Me.Telephone2.Name = "Telephone2"
        Me.Telephone2.Size = New System.Drawing.Size(95, 20)
        Me.Telephone2.TabIndex = 77
        '
        'Telephone3
        '
        Me.Telephone3.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainResourceBindingSource, "Telephone3", True))
        Me.Telephone3.Location = New System.Drawing.Point(80, 109)
        Me.Telephone3.MaxLength = 30
        Me.Telephone3.Name = "Telephone3"
        Me.Telephone3.Size = New System.Drawing.Size(95, 20)
        Me.Telephone3.TabIndex = 78
        '
        'Label8
        '
        Me.Label8.Location = New System.Drawing.Point(10, 61)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(68, 16)
        Me.Label8.TabIndex = 65
        Me.Label8.Text = "Phone (800)"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblAddressRequired
        '
        Me.lblAddressRequired.ForeColor = System.Drawing.Color.Red
        Me.lblAddressRequired.Location = New System.Drawing.Point(4, 3)
        Me.lblAddressRequired.Name = "lblAddressRequired"
        Me.lblAddressRequired.Size = New System.Drawing.Size(247, 16)
        Me.lblAddressRequired.TabIndex = 64
        Me.lblAddressRequired.Text = "** REQUIRED at least one means of contact ***"
        '
        'Telephone
        '
        Me.Telephone.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainResourceBindingSource, "Telephone", True))
        Me.Telephone.Location = New System.Drawing.Point(80, 59)
        Me.Telephone.MaxLength = 50
        Me.Telephone.Name = "Telephone"
        Me.Telephone.Size = New System.Drawing.Size(95, 20)
        Me.Telephone.TabIndex = 52
        '
        'txtSponsoringOrg
        '
        Me.txtSponsoringOrg.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainResourceBindingSource, "SponsoringOrgName", True))
        Me.txtSponsoringOrg.Location = New System.Drawing.Point(80, 19)
        Me.txtSponsoringOrg.Multiline = True
        Me.txtSponsoringOrg.Name = "txtSponsoringOrg"
        Me.txtSponsoringOrg.Size = New System.Drawing.Size(167, 36)
        Me.txtSponsoringOrg.TabIndex = 153
        '
        'lblAuxOrgName
        '
        Me.lblAuxOrgName.Location = New System.Drawing.Point(10, 12)
        Me.lblAuxOrgName.Margin = New System.Windows.Forms.Padding(0)
        Me.lblAuxOrgName.Name = "lblAuxOrgName"
        Me.lblAuxOrgName.Size = New System.Drawing.Size(68, 47)
        Me.lblAuxOrgName.TabIndex = 154
        Me.lblAuxOrgName.Text = "Sponsoring " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Organization Name"
        Me.lblAuxOrgName.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'pgPublish
        '
        Me.pgPublish.Controls.Add(Me.Label36)
        Me.pgPublish.Controls.Add(Me.txtPubDate)
        Me.pgPublish.Controls.Add(Me.GroupBox1)
        Me.pgPublish.Controls.Add(Me.tbPublishing)
        Me.pgPublish.Location = New System.Drawing.Point(4, 22)
        Me.pgPublish.Name = "pgPublish"
        Me.pgPublish.Size = New System.Drawing.Size(263, 302)
        Me.pgPublish.TabIndex = 0
        Me.pgPublish.Text = " Publishing Information"
        '
        'Label36
        '
        Me.Label36.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label36.Location = New System.Drawing.Point(10, 76)
        Me.Label36.Margin = New System.Windows.Forms.Padding(0)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(88, 29)
        Me.Label36.TabIndex = 161
        Me.Label36.Text = "Date Published"
        Me.Label36.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtPubDate
        '
        Me.txtPubDate.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainResourceBindingSource, "DatePublished", True))
        Me.txtPubDate.Location = New System.Drawing.Point(101, 81)
        Me.txtPubDate.MaxLength = 50
        Me.txtPubDate.Name = "txtPubDate"
        Me.txtPubDate.Size = New System.Drawing.Size(144, 20)
        Me.txtPubDate.TabIndex = 168
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txtPubCity)
        Me.GroupBox1.Controls.Add(Me.lblGotoPublisher)
        Me.GroupBox1.Controls.Add(Me.cboPublisher)
        Me.GroupBox1.Controls.Add(Me.Label38)
        Me.GroupBox1.Controls.Add(Me.txtPubState)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(7, 6)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(0)
        Me.GroupBox1.Size = New System.Drawing.Size(248, 67)
        Me.GroupBox1.TabIndex = 160
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "   "
        '
        'txtPubCity
        '
        Me.txtPubCity.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainResourceBindingSource, "PubCity", True))
        Me.txtPubCity.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPubCity.Location = New System.Drawing.Point(60, 38)
        Me.txtPubCity.MaxLength = 50
        Me.txtPubCity.Name = "txtPubCity"
        Me.txtPubCity.Size = New System.Drawing.Size(106, 20)
        Me.txtPubCity.TabIndex = 164
        Me.ToolTip1.SetToolTip(Me.txtPubCity, "Publisher City")
        '
        'lblGotoPublisher
        '
        Me.lblGotoPublisher.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblGotoPublisher.ForeColor = System.Drawing.Color.DarkGreen
        Me.lblGotoPublisher.Location = New System.Drawing.Point(-4, 3)
        Me.lblGotoPublisher.Name = "lblGotoPublisher"
        Me.lblGotoPublisher.Size = New System.Drawing.Size(61, 32)
        Me.lblGotoPublisher.TabIndex = 157
        Me.lblGotoPublisher.Text = "Publisher Name"
        Me.lblGotoPublisher.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cboPublisher
        '
        Me.cboPublisher.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboPublisher.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboPublisher.DataBindings.Add(New System.Windows.Forms.Binding("SelectedItem", Me.MainResourceBindingSource, "Publisher", True))
        Me.cboPublisher.DropDownWidth = 250
        Me.cboPublisher.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboPublisher.Location = New System.Drawing.Point(60, 12)
        Me.cboPublisher.Name = "cboPublisher"
        Me.cboPublisher.RestrictContentToListItems = True
        Me.cboPublisher.Size = New System.Drawing.Size(182, 21)
        Me.cboPublisher.TabIndex = 162
        Me.ToolTip1.SetToolTip(Me.cboPublisher, "Publisher Name")
        '
        'Label38
        '
        Me.Label38.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label38.Location = New System.Drawing.Point(27, 31)
        Me.Label38.Margin = New System.Windows.Forms.Padding(0)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(30, 33)
        Me.Label38.TabIndex = 1
        Me.Label38.Text = "City"
        Me.Label38.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtPubState
        '
        Me.txtPubState.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainResourceBindingSource, "PubState", True))
        Me.txtPubState.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPubState.Location = New System.Drawing.Point(199, 38)
        Me.txtPubState.MaxLength = 50
        Me.txtPubState.Name = "txtPubState"
        Me.txtPubState.Size = New System.Drawing.Size(43, 20)
        Me.txtPubState.TabIndex = 165
        Me.ToolTip1.SetToolTip(Me.txtPubState, "Publisher State")
        '
        'Label9
        '
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(160, 38)
        Me.Label9.Margin = New System.Windows.Forms.Padding(0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(40, 19)
        Me.Label9.TabIndex = 166
        Me.Label9.Text = "State"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'tbPublishing
        '
        Me.tbPublishing.Controls.Add(Me.pgArticle)
        Me.tbPublishing.Controls.Add(Me.pgBook)
        Me.tbPublishing.Controls.Add(Me.pgMedia)
        Me.tbPublishing.Location = New System.Drawing.Point(9, 111)
        Me.tbPublishing.Name = "tbPublishing"
        Me.tbPublishing.SelectedIndex = 0
        Me.tbPublishing.Size = New System.Drawing.Size(240, 173)
        Me.tbPublishing.TabIndex = 170
        Me.tbPublishing.TabStop = False
        '
        'pgArticle
        '
        Me.pgArticle.Controls.Add(Me.Label25)
        Me.pgArticle.Controls.Add(Me.Label24)
        Me.pgArticle.Controls.Add(Me.Label23)
        Me.pgArticle.Controls.Add(Me.TextBox8)
        Me.pgArticle.Controls.Add(Me.TextBox9)
        Me.pgArticle.Controls.Add(Me.TextBox6)
        Me.pgArticle.Location = New System.Drawing.Point(4, 22)
        Me.pgArticle.Name = "pgArticle"
        Me.pgArticle.Size = New System.Drawing.Size(232, 147)
        Me.pgArticle.TabIndex = 0
        Me.pgArticle.Text = "Article"
        '
        'Label25
        '
        Me.Label25.Location = New System.Drawing.Point(5, 84)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(46, 18)
        Me.Label25.TabIndex = 1
        Me.Label25.Text = "Volume"
        '
        'Label24
        '
        Me.Label24.Location = New System.Drawing.Point(4, 46)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(51, 28)
        Me.Label24.TabIndex = 0
        Me.Label24.Text = "Page Numbers"
        '
        'Label23
        '
        Me.Label23.Location = New System.Drawing.Point(6, 17)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(41, 25)
        Me.Label23.TabIndex = 152
        Me.Label23.Text = "Source Title"
        '
        'TextBox8
        '
        Me.TextBox8.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainResourceBindingSource, "Volume", True))
        Me.TextBox8.Location = New System.Drawing.Point(54, 80)
        Me.TextBox8.Name = "TextBox8"
        Me.TextBox8.Size = New System.Drawing.Size(166, 20)
        Me.TextBox8.TabIndex = 176
        '
        'TextBox9
        '
        Me.TextBox9.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainResourceBindingSource, "PageNumbers", True))
        Me.TextBox9.Location = New System.Drawing.Point(54, 48)
        Me.TextBox9.MaxLength = 255
        Me.TextBox9.Name = "TextBox9"
        Me.TextBox9.Size = New System.Drawing.Size(166, 20)
        Me.TextBox9.TabIndex = 174
        '
        'TextBox6
        '
        Me.TextBox6.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainResourceBindingSource, "SourceTitle", True))
        Me.TextBox6.Location = New System.Drawing.Point(54, 16)
        Me.TextBox6.MaxLength = 255
        Me.TextBox6.Name = "TextBox6"
        Me.TextBox6.Size = New System.Drawing.Size(166, 20)
        Me.TextBox6.TabIndex = 172
        '
        'pgBook
        '
        Me.pgBook.Controls.Add(Me.Label51)
        Me.pgBook.Controls.Add(Me.TextBox5)
        Me.pgBook.Controls.Add(Me.Label30)
        Me.pgBook.Controls.Add(Me.Label29)
        Me.pgBook.Controls.Add(Me.Label28)
        Me.pgBook.Controls.Add(Me.Label27)
        Me.pgBook.Controls.Add(Me.Label26)
        Me.pgBook.Controls.Add(Me.textbox1Source4)
        Me.pgBook.Controls.Add(Me.textbox1Source5)
        Me.pgBook.Controls.Add(Me.textbox1Source6)
        Me.pgBook.Controls.Add(Me.textbox1Source7)
        Me.pgBook.Controls.Add(Me.textbox1Source8)
        Me.pgBook.Location = New System.Drawing.Point(4, 22)
        Me.pgBook.Name = "pgBook"
        Me.pgBook.Size = New System.Drawing.Size(232, 147)
        Me.pgBook.TabIndex = 1
        Me.pgBook.Text = "Book/Periodical"
        Me.pgBook.Visible = False
        '
        'Label51
        '
        Me.Label51.Location = New System.Drawing.Point(17, 120)
        Me.Label51.Name = "Label51"
        Me.Label51.Size = New System.Drawing.Size(48, 20)
        Me.Label51.TabIndex = 187
        Me.Label51.Text = "ASIN"
        Me.Label51.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'TextBox5
        '
        Me.TextBox5.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainResourceBindingSource, "ASINID", True))
        Me.TextBox5.Location = New System.Drawing.Point(79, 119)
        Me.TextBox5.MaxLength = 50
        Me.TextBox5.Name = "TextBox5"
        Me.TextBox5.Size = New System.Drawing.Size(144, 20)
        Me.TextBox5.TabIndex = 188
        Me.ToolTip1.SetToolTip(Me.TextBox5, "AmazonID")
        '
        'Label30
        '
        Me.Label30.Location = New System.Drawing.Point(4, 93)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(61, 15)
        Me.Label30.TabIndex = 39
        Me.Label30.Text = "Frequency"
        Me.Label30.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ToolTip1.SetToolTip(Me.Label30, "Quarterly, Annually, etc.")
        '
        'Label29
        '
        Me.Label29.Location = New System.Drawing.Point(17, 68)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(48, 20)
        Me.Label29.TabIndex = 38
        Me.Label29.Text = "Volume"
        Me.Label29.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label28
        '
        Me.Label28.Location = New System.Drawing.Point(17, 48)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(48, 20)
        Me.Label28.TabIndex = 37
        Me.Label28.Text = "Series"
        Me.Label28.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label27
        '
        Me.Label27.Location = New System.Drawing.Point(17, 29)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(48, 20)
        Me.Label27.TabIndex = 36
        Me.Label27.Text = "Edition"
        Me.Label27.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label26
        '
        Me.Label26.Location = New System.Drawing.Point(17, 7)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(48, 20)
        Me.Label26.TabIndex = 35
        Me.Label26.Text = "ISBN"
        Me.Label26.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'textbox1Source4
        '
        Me.textbox1Source4.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainResourceBindingSource, "PubNumber", True))
        Me.textbox1Source4.Location = New System.Drawing.Point(79, 6)
        Me.textbox1Source4.MaxLength = 50
        Me.textbox1Source4.Name = "textbox1Source4"
        Me.textbox1Source4.Size = New System.Drawing.Size(144, 20)
        Me.textbox1Source4.TabIndex = 178
        '
        'textbox1Source5
        '
        Me.textbox1Source5.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainResourceBindingSource, "Edition", True))
        Me.textbox1Source5.Location = New System.Drawing.Point(79, 28)
        Me.textbox1Source5.MaxLength = 255
        Me.textbox1Source5.Name = "textbox1Source5"
        Me.textbox1Source5.Size = New System.Drawing.Size(144, 20)
        Me.textbox1Source5.TabIndex = 180
        '
        'textbox1Source6
        '
        Me.textbox1Source6.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainResourceBindingSource, "Series", True))
        Me.textbox1Source6.Location = New System.Drawing.Point(79, 50)
        Me.textbox1Source6.Name = "textbox1Source6"
        Me.textbox1Source6.Size = New System.Drawing.Size(144, 20)
        Me.textbox1Source6.TabIndex = 182
        '
        'textbox1Source7
        '
        Me.textbox1Source7.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainResourceBindingSource, "Volume", True))
        Me.textbox1Source7.Location = New System.Drawing.Point(79, 72)
        Me.textbox1Source7.MaxLength = 50
        Me.textbox1Source7.Name = "textbox1Source7"
        Me.textbox1Source7.Size = New System.Drawing.Size(144, 20)
        Me.textbox1Source7.TabIndex = 184
        '
        'textbox1Source8
        '
        Me.textbox1Source8.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainResourceBindingSource, "PublishedPeriod", True))
        Me.textbox1Source8.Location = New System.Drawing.Point(79, 94)
        Me.textbox1Source8.MaxLength = 50
        Me.textbox1Source8.Name = "textbox1Source8"
        Me.textbox1Source8.Size = New System.Drawing.Size(144, 20)
        Me.textbox1Source8.TabIndex = 186
        '
        'pgMedia
        '
        Me.pgMedia.Controls.Add(Me.textbox1Source0)
        Me.pgMedia.Controls.Add(Me.TextBox3)
        Me.pgMedia.Controls.Add(Me.Label35)
        Me.pgMedia.Controls.Add(Me.Label34)
        Me.pgMedia.Controls.Add(Me.Label33)
        Me.pgMedia.Controls.Add(Me.Label32)
        Me.pgMedia.Controls.Add(Me.Label31)
        Me.pgMedia.Controls.Add(Me.TextBox24)
        Me.pgMedia.Controls.Add(Me.TextBox25)
        Me.pgMedia.Controls.Add(Me.TextBox26)
        Me.pgMedia.Location = New System.Drawing.Point(4, 22)
        Me.pgMedia.Name = "pgMedia"
        Me.pgMedia.Size = New System.Drawing.Size(232, 147)
        Me.pgMedia.TabIndex = 2
        Me.pgMedia.Text = "Media"
        Me.pgMedia.Visible = False
        '
        'textbox1Source0
        '
        Me.textbox1Source0.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainResourceBindingSource, "Series", True))
        Me.textbox1Source0.Location = New System.Drawing.Point(99, 76)
        Me.textbox1Source0.MaxLength = 100
        Me.textbox1Source0.Name = "textbox1Source0"
        Me.textbox1Source0.Size = New System.Drawing.Size(122, 20)
        Me.textbox1Source0.TabIndex = 196
        '
        'TextBox3
        '
        Me.TextBox3.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainResourceBindingSource, "PubNumber", True))
        Me.TextBox3.Location = New System.Drawing.Point(99, 7)
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.Size = New System.Drawing.Size(122, 20)
        Me.TextBox3.TabIndex = 190
        '
        'Label35
        '
        Me.Label35.Location = New System.Drawing.Point(15, 103)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(78, 14)
        Me.Label35.TabIndex = 40
        Me.Label35.Text = "Edition"
        Me.Label35.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label34
        '
        Me.Label34.Location = New System.Drawing.Point(13, 77)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(78, 17)
        Me.Label34.TabIndex = 39
        Me.Label34.Text = "Series"
        Me.Label34.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label33
        '
        Me.Label33.Location = New System.Drawing.Point(11, 5)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(78, 20)
        Me.Label33.TabIndex = 38
        Me.Label33.Text = "ISBN/Number"
        Me.Label33.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label32
        '
        Me.Label32.Location = New System.Drawing.Point(11, 51)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(78, 20)
        Me.Label32.TabIndex = 37
        Me.Label32.Text = "Format"
        Me.Label32.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label31
        '
        Me.Label31.Location = New System.Drawing.Point(9, 30)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(78, 20)
        Me.Label31.TabIndex = 36
        Me.Label31.Text = "Running Time"
        Me.Label31.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'TextBox24
        '
        Me.TextBox24.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainResourceBindingSource, "Edition", True))
        Me.TextBox24.Location = New System.Drawing.Point(99, 98)
        Me.TextBox24.Name = "TextBox24"
        Me.TextBox24.Size = New System.Drawing.Size(122, 20)
        Me.TextBox24.TabIndex = 198
        '
        'TextBox25
        '
        Me.TextBox25.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainResourceBindingSource, "PubFormat", True))
        Me.TextBox25.Location = New System.Drawing.Point(99, 53)
        Me.TextBox25.Name = "TextBox25"
        Me.TextBox25.Size = New System.Drawing.Size(122, 20)
        Me.TextBox25.TabIndex = 194
        '
        'TextBox26
        '
        Me.TextBox26.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainResourceBindingSource, "RunningTime", True))
        Me.TextBox26.Location = New System.Drawing.Point(99, 31)
        Me.TextBox26.MaxLength = 50
        Me.TextBox26.Name = "TextBox26"
        Me.TextBox26.Size = New System.Drawing.Size(122, 20)
        Me.TextBox26.TabIndex = 192
        '
        'lblLink
        '
        Me.lblLink.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLink.Location = New System.Drawing.Point(409, 496)
        Me.lblLink.Margin = New System.Windows.Forms.Padding(0)
        Me.lblLink.Name = "lblLink"
        Me.lblLink.Size = New System.Drawing.Size(57, 39)
        Me.lblLink.TabIndex = 172
        Me.lblLink.Text = "Resource Link"
        Me.lblLink.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label41
        '
        Me.Label41.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label41.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label41.Location = New System.Drawing.Point(16, 165)
        Me.Label41.Name = "Label41"
        Me.Label41.Size = New System.Drawing.Size(113, 18)
        Me.Label41.TabIndex = 147
        Me.Label41.Text = "CRG Categories"
        '
        'pgRecommendation
        '
        Me.pgRecommendation.Controls.Add(Me.grdRecommend)
        Me.pgRecommendation.Location = New System.Drawing.Point(4, 22)
        Me.pgRecommendation.Name = "pgRecommendation"
        Me.pgRecommendation.Size = New System.Drawing.Size(683, 568)
        Me.pgRecommendation.TabIndex = 1
        Me.pgRecommendation.Tag = "RECOMMENDATIONS"
        Me.pgRecommendation.Text = "RECOMMENDATIONS"
        Me.pgRecommendation.UseVisualStyleBackColor = True
        '
        'grdRecommend
        '
        Me.grdRecommend.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdRecommend.DataMember = "GetResRecommendation"
        Me.grdRecommend.DataSource = Me.DsRFA
        Me.grdRecommend.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.grdRecommend.Location = New System.Drawing.Point(9, 3)
        Me.grdRecommend.Name = "grdRecommend"
        Me.grdRecommend.ReadOnly = True
        Me.grdRecommend.RowHeaderWidth = 30
        Me.grdRecommend.Size = New System.Drawing.Size(670, 491)
        Me.grdRecommend.TabIndex = 0
        Me.grdRecommend.TableStyles.AddRange(New System.Windows.Forms.DataGridTableStyle() {Me.tsRecommend})
        '
        'DsRFA
        '
        Me.DsRFA.DataSetName = "dsMisc"
        Me.DsRFA.Locale = New System.Globalization.CultureInfo("en-US")
        Me.DsRFA.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'tsRecommend
        '
        Me.tsRecommend.DataGrid = Me.grdRecommend
        Me.tsRecommend.GridColumnStyles.AddRange(New System.Windows.Forms.DataGridColumnStyle() {Me.DataGridTextBoxColumn17, Me.DataGridTextBoxColumn18, Me.DataGridTextBoxColumn23, Me.DataGridTextBoxColumn19, Me.DataGridTextBoxColumn20, Me.DataGridTextBoxColumn21, Me.DataGridTextBoxColumn22})
        Me.tsRecommend.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.tsRecommend.MappingName = "GetResRecommendation"
        Me.tsRecommend.ReadOnly = True
        Me.tsRecommend.RowHeaderWidth = 30
        '
        'DataGridTextBoxColumn17
        '
        Me.DataGridTextBoxColumn17.Format = ""
        Me.DataGridTextBoxColumn17.FormatInfo = Nothing
        Me.DataGridTextBoxColumn17.HeaderText = "RecommendID"
        Me.DataGridTextBoxColumn17.MappingName = "RecommendID"
        Me.DataGridTextBoxColumn17.Width = 0
        '
        'DataGridTextBoxColumn18
        '
        Me.DataGridTextBoxColumn18.Format = ""
        Me.DataGridTextBoxColumn18.FormatInfo = Nothing
        Me.DataGridTextBoxColumn18.HeaderText = "ResourceNum"
        Me.DataGridTextBoxColumn18.MappingName = "ResourceNum"
        Me.DataGridTextBoxColumn18.Width = 0
        '
        'DataGridTextBoxColumn23
        '
        Me.DataGridTextBoxColumn23.Format = ""
        Me.DataGridTextBoxColumn23.FormatInfo = Nothing
        Me.DataGridTextBoxColumn23.HeaderText = "OrgNum"
        Me.DataGridTextBoxColumn23.MappingName = "OrgNum"
        Me.DataGridTextBoxColumn23.Width = 0
        '
        'DataGridTextBoxColumn19
        '
        Me.DataGridTextBoxColumn19.Format = ""
        Me.DataGridTextBoxColumn19.FormatInfo = Nothing
        Me.DataGridTextBoxColumn19.HeaderText = "Organization"
        Me.DataGridTextBoxColumn19.MappingName = "OrgName"
        Me.DataGridTextBoxColumn19.Width = 150
        '
        'DataGridTextBoxColumn20
        '
        Me.DataGridTextBoxColumn20.Format = ""
        Me.DataGridTextBoxColumn20.FormatInfo = Nothing
        Me.DataGridTextBoxColumn20.HeaderText = "Case"
        Me.DataGridTextBoxColumn20.MappingName = "CaseName"
        Me.DataGridTextBoxColumn20.Width = 200
        '
        'DataGridTextBoxColumn21
        '
        Me.DataGridTextBoxColumn21.Format = "d"
        Me.DataGridTextBoxColumn21.FormatInfo = Nothing
        Me.DataGridTextBoxColumn21.HeaderText = "Date"
        Me.DataGridTextBoxColumn21.MappingName = "RecommendDate"
        Me.DataGridTextBoxColumn21.Width = 75
        '
        'DataGridTextBoxColumn22
        '
        Me.DataGridTextBoxColumn22.Format = ""
        Me.DataGridTextBoxColumn22.FormatInfo = Nothing
        Me.DataGridTextBoxColumn22.HeaderText = "Recommended By"
        Me.DataGridTextBoxColumn22.MappingName = "WhoRecommended"
        Me.DataGridTextBoxColumn22.Width = 125
        '
        'pgFeedback
        '
        Me.pgFeedback.Controls.Add(Me.grdFeedback)
        Me.pgFeedback.Location = New System.Drawing.Point(4, 22)
        Me.pgFeedback.Name = "pgFeedback"
        Me.pgFeedback.Size = New System.Drawing.Size(683, 568)
        Me.pgFeedback.TabIndex = 2
        Me.pgFeedback.Tag = "FEEDBACK"
        Me.pgFeedback.Text = "      FEEDBACK      "
        Me.pgFeedback.UseVisualStyleBackColor = True
        '
        'grdFeedback
        '
        Me.grdFeedback.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdFeedback.DataMember = "GetResFeedback"
        Me.grdFeedback.DataSource = Me.DsRFA
        Me.grdFeedback.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.grdFeedback.Location = New System.Drawing.Point(9, 3)
        Me.grdFeedback.Name = "grdFeedback"
        Me.grdFeedback.ReadOnly = True
        Me.grdFeedback.RowHeaderWidth = 20
        Me.grdFeedback.Size = New System.Drawing.Size(670, 491)
        Me.grdFeedback.TabIndex = 1
        Me.grdFeedback.TableStyles.AddRange(New System.Windows.Forms.DataGridTableStyle() {Me.tsFeedback})
        '
        'tsFeedback
        '
        Me.tsFeedback.DataGrid = Me.grdFeedback
        Me.tsFeedback.GridColumnStyles.AddRange(New System.Windows.Forms.DataGridColumnStyle() {Me.DataGridTextBoxColumn24, Me.DataGridTextBoxColumn25, Me.DataGridTextBoxColumn68, Me.DataGridTextBoxColumn26, Me.DataGridTextBoxColumn27, Me.DataGridTextBoxColumn28, Me.DataGridTextBoxColumn29, Me.DataGridTextBoxColumn56, Me.DataGridTextBoxColumn67})
        Me.tsFeedback.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.tsFeedback.MappingName = "GetResFeedback"
        Me.tsFeedback.RowHeaderWidth = 20
        '
        'DataGridTextBoxColumn24
        '
        Me.DataGridTextBoxColumn24.Format = ""
        Me.DataGridTextBoxColumn24.FormatInfo = Nothing
        Me.DataGridTextBoxColumn24.HeaderText = "FeedbackID"
        Me.DataGridTextBoxColumn24.MappingName = "FeedbackID"
        Me.DataGridTextBoxColumn24.Width = 0
        '
        'DataGridTextBoxColumn25
        '
        Me.DataGridTextBoxColumn25.Format = ""
        Me.DataGridTextBoxColumn25.FormatInfo = Nothing
        Me.DataGridTextBoxColumn25.HeaderText = "ResourceNum"
        Me.DataGridTextBoxColumn25.MappingName = "ResourceNum"
        Me.DataGridTextBoxColumn25.Width = 0
        '
        'DataGridTextBoxColumn68
        '
        Me.DataGridTextBoxColumn68.Format = ""
        Me.DataGridTextBoxColumn68.FormatInfo = Nothing
        Me.DataGridTextBoxColumn68.HeaderText = "OrgNum"
        Me.DataGridTextBoxColumn68.MappingName = "OrgNum"
        Me.DataGridTextBoxColumn68.Width = 0
        '
        'DataGridTextBoxColumn26
        '
        Me.DataGridTextBoxColumn26.Format = ""
        Me.DataGridTextBoxColumn26.FormatInfo = Nothing
        Me.DataGridTextBoxColumn26.HeaderText = "Organization"
        Me.DataGridTextBoxColumn26.MappingName = "OrgName"
        Me.DataGridTextBoxColumn26.Width = 75
        '
        'DataGridTextBoxColumn27
        '
        Me.DataGridTextBoxColumn27.Format = ""
        Me.DataGridTextBoxColumn27.FormatInfo = Nothing
        Me.DataGridTextBoxColumn27.HeaderText = "Case"
        Me.DataGridTextBoxColumn27.MappingName = "CaseName"
        Me.DataGridTextBoxColumn27.Width = 150
        '
        'DataGridTextBoxColumn28
        '
        Me.DataGridTextBoxColumn28.Format = "d"
        Me.DataGridTextBoxColumn28.FormatInfo = Nothing
        Me.DataGridTextBoxColumn28.HeaderText = "Date"
        Me.DataGridTextBoxColumn28.MappingName = "FeedbackDate"
        Me.DataGridTextBoxColumn28.Width = 75
        '
        'DataGridTextBoxColumn29
        '
        Me.DataGridTextBoxColumn29.Format = ""
        Me.DataGridTextBoxColumn29.FormatInfo = Nothing
        Me.DataGridTextBoxColumn29.HeaderText = "Feedback By"
        Me.DataGridTextBoxColumn29.MappingName = "FeedbackBy"
        Me.DataGridTextBoxColumn29.Width = 125
        '
        'DataGridTextBoxColumn56
        '
        Me.DataGridTextBoxColumn56.Format = ""
        Me.DataGridTextBoxColumn56.FormatInfo = Nothing
        Me.DataGridTextBoxColumn56.HeaderText = "Approval"
        Me.DataGridTextBoxColumn56.MappingName = "ApprovalScale"
        Me.DataGridTextBoxColumn56.Width = 75
        '
        'DataGridTextBoxColumn67
        '
        Me.DataGridTextBoxColumn67.Format = ""
        Me.DataGridTextBoxColumn67.FormatInfo = Nothing
        Me.DataGridTextBoxColumn67.HeaderText = "What Did"
        Me.DataGridTextBoxColumn67.MappingName = "WhatResourceDid"
        Me.DataGridTextBoxColumn67.Width = 75
        '
        'pgAlert
        '
        Me.pgAlert.Controls.Add(Me.grdAlert)
        Me.pgAlert.Location = New System.Drawing.Point(4, 22)
        Me.pgAlert.Name = "pgAlert"
        Me.pgAlert.Size = New System.Drawing.Size(683, 568)
        Me.pgAlert.TabIndex = 3
        Me.pgAlert.Tag = "ALERTS"
        Me.pgAlert.Text = "          ALERTS           "
        Me.pgAlert.UseVisualStyleBackColor = True
        '
        'grdAlert
        '
        Me.grdAlert.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdAlert.DataMember = "GetResAlert"
        Me.grdAlert.DataSource = Me.DsRFA
        Me.grdAlert.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.grdAlert.Location = New System.Drawing.Point(9, 3)
        Me.grdAlert.Name = "grdAlert"
        Me.grdAlert.ReadOnly = True
        Me.grdAlert.RowHeaderWidth = 20
        Me.grdAlert.Size = New System.Drawing.Size(670, 491)
        Me.grdAlert.TabIndex = 1
        Me.grdAlert.TableStyles.AddRange(New System.Windows.Forms.DataGridTableStyle() {Me.tsAlert})
        '
        'tsAlert
        '
        Me.tsAlert.DataGrid = Me.grdAlert
        Me.tsAlert.GridColumnStyles.AddRange(New System.Windows.Forms.DataGridColumnStyle() {Me.DataGridTextBoxColumn12, Me.DataGridTextBoxColumn13, Me.DataGridTextBoxColumn14, Me.DataGridTextBoxColumn15, Me.DataGridTextBoxColumn16})
        Me.tsAlert.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.tsAlert.MappingName = "GetResAlert"
        Me.tsAlert.RowHeaderWidth = 20
        '
        'DataGridTextBoxColumn12
        '
        Me.DataGridTextBoxColumn12.Format = ""
        Me.DataGridTextBoxColumn12.FormatInfo = Nothing
        Me.DataGridTextBoxColumn12.MappingName = "WarningID"
        Me.DataGridTextBoxColumn12.Width = 0
        '
        'DataGridTextBoxColumn13
        '
        Me.DataGridTextBoxColumn13.Format = ""
        Me.DataGridTextBoxColumn13.FormatInfo = Nothing
        Me.DataGridTextBoxColumn13.MappingName = "ResourceNum"
        Me.DataGridTextBoxColumn13.Width = 0
        '
        'DataGridTextBoxColumn14
        '
        Me.DataGridTextBoxColumn14.Format = "d"
        Me.DataGridTextBoxColumn14.FormatInfo = Nothing
        Me.DataGridTextBoxColumn14.HeaderText = "Date"
        Me.DataGridTextBoxColumn14.MappingName = "WarningDate"
        Me.DataGridTextBoxColumn14.Width = 75
        '
        'DataGridTextBoxColumn15
        '
        Me.DataGridTextBoxColumn15.Format = ""
        Me.DataGridTextBoxColumn15.FormatInfo = Nothing
        Me.DataGridTextBoxColumn15.HeaderText = "Staff"
        Me.DataGridTextBoxColumn15.MappingName = "StaffName"
        Me.DataGridTextBoxColumn15.Width = 130
        '
        'DataGridTextBoxColumn16
        '
        Me.DataGridTextBoxColumn16.Format = ""
        Me.DataGridTextBoxColumn16.FormatInfo = Nothing
        Me.DataGridTextBoxColumn16.HeaderText = "Warning"
        Me.DataGridTextBoxColumn16.MappingName = "Warning"
        Me.DataGridTextBoxColumn16.Width = 200
        '
        'pgAddress
        '
        Me.pgAddress.Location = New System.Drawing.Point(4, 22)
        Me.pgAddress.Name = "pgAddress"
        Me.pgAddress.Size = New System.Drawing.Size(683, 568)
        Me.pgAddress.TabIndex = 5
        Me.pgAddress.Tag = "ADDRESSES"
        Me.pgAddress.Text = "ADDRESSES"
        Me.pgAddress.UseVisualStyleBackColor = True
        '
        'pgOther
        '
        Me.pgOther.Controls.Add(Me.Label3)
        Me.pgOther.Controls.Add(Me.TextBox1)
        Me.pgOther.Controls.Add(Me.lblUseThisOne)
        Me.pgOther.Controls.Add(Me.lblEntityNum)
        Me.pgOther.Controls.Add(Me.lblEntityType)
        Me.pgOther.Controls.Add(Me.lblAddressType)
        Me.pgOther.Controls.Add(Me.Label50)
        Me.pgOther.Controls.Add(Me.fldAddrChangedBy)
        Me.pgOther.Controls.Add(Me.fldAddrChangeDt)
        Me.pgOther.Controls.Add(Me.pnlReview)
        Me.pgOther.Controls.Add(Me.fldCreateDt)
        Me.pgOther.Controls.Add(Me.Label47)
        Me.pgOther.Controls.Add(Me.fldChangeBy)
        Me.pgOther.Controls.Add(Me.fldCreatedBy)
        Me.pgOther.Controls.Add(Me.fldLastChangeDt)
        Me.pgOther.Controls.Add(Me.Label5)
        Me.pgOther.Controls.Add(Me.chkApproved)
        Me.pgOther.Location = New System.Drawing.Point(4, 22)
        Me.pgOther.Name = "pgOther"
        Me.pgOther.Size = New System.Drawing.Size(683, 568)
        Me.pgOther.TabIndex = 4
        Me.pgOther.Tag = "OTHER"
        Me.pgOther.Text = "          OTHER          "
        Me.pgOther.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(58, 406)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(132, 26)
        Me.Label3.TabIndex = 24
        Me.Label3.Tag = ""
        Me.Label3.Text = "DateCRGFlagged"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'TextBox1
        '
        Me.TextBox1.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainResourceBindingSource, "NewCRGDate", True))
        Me.TextBox1.Enabled = False
        Me.TextBox1.Location = New System.Drawing.Point(200, 410)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.ReadOnly = True
        Me.TextBox1.Size = New System.Drawing.Size(134, 20)
        Me.TextBox1.TabIndex = 23
        Me.TextBox1.Text = "DateCRGFlagged"
        '
        'lblUseThisOne
        '
        Me.lblUseThisOne.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.ResourceMainAddressBindingSource, "UseThisOne", True))
        Me.lblUseThisOne.Location = New System.Drawing.Point(463, 492)
        Me.lblUseThisOne.Name = "lblUseThisOne"
        Me.lblUseThisOne.Size = New System.Drawing.Size(38, 26)
        Me.lblUseThisOne.TabIndex = 22
        Me.lblUseThisOne.Tag = ""
        Me.lblUseThisOne.Text = "1"
        Me.lblUseThisOne.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'ResourceMainAddressBindingSource
        '
        Me.ResourceMainAddressBindingSource.DataMember = "ResourceMainAddress"
        Me.ResourceMainAddressBindingSource.DataSource = Me.DsMainResource1
        '
        'lblEntityNum
        '
        Me.lblEntityNum.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.ResourceMainAddressBindingSource, "EntityNum", True))
        Me.lblEntityNum.Location = New System.Drawing.Point(280, 492)
        Me.lblEntityNum.Name = "lblEntityNum"
        Me.lblEntityNum.Size = New System.Drawing.Size(38, 26)
        Me.lblEntityNum.TabIndex = 21
        Me.lblEntityNum.Tag = ""
        Me.lblEntityNum.Text = "ThisID"
        Me.lblEntityNum.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblEntityType
        '
        Me.lblEntityType.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.ResourceMainAddressBindingSource, "EntityType", True))
        Me.lblEntityType.Location = New System.Drawing.Point(185, 492)
        Me.lblEntityType.Name = "lblEntityType"
        Me.lblEntityType.Size = New System.Drawing.Size(89, 26)
        Me.lblEntityType.TabIndex = 20
        Me.lblEntityType.Tag = ""
        Me.lblEntityType.Text = "Resource"
        Me.lblEntityType.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblAddressType
        '
        Me.lblAddressType.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.ResourceMainAddressBindingSource, "AddressType", True))
        Me.lblAddressType.Location = New System.Drawing.Point(363, 492)
        Me.lblAddressType.Name = "lblAddressType"
        Me.lblAddressType.Size = New System.Drawing.Size(94, 26)
        Me.lblAddressType.TabIndex = 19
        Me.lblAddressType.Tag = ""
        Me.lblAddressType.Text = "AddressType"
        Me.lblAddressType.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label50
        '
        Me.Label50.Location = New System.Drawing.Point(69, 465)
        Me.Label50.Name = "Label50"
        Me.Label50.Size = New System.Drawing.Size(132, 26)
        Me.Label50.TabIndex = 18
        Me.Label50.Tag = "Last Changed by"
        Me.Label50.Text = "Address Last Changed"
        Me.Label50.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Label50.Visible = False
        '
        'fldAddrChangedBy
        '
        Me.fldAddrChangedBy.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.ResourceMainAddressBindingSource, "LastChangeStaffNum", True))
        Me.fldAddrChangedBy.Location = New System.Drawing.Point(370, 469)
        Me.fldAddrChangedBy.Name = "fldAddrChangedBy"
        Me.fldAddrChangedBy.ReadOnly = True
        Me.fldAddrChangedBy.Size = New System.Drawing.Size(68, 20)
        Me.fldAddrChangedBy.TabIndex = 17
        Me.fldAddrChangedBy.Tag = "Changed by"
        Me.fldAddrChangedBy.Text = "fldAddrChangedBy"
        Me.fldAddrChangedBy.Visible = False
        '
        'fldAddrChangeDt
        '
        Me.fldAddrChangeDt.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.ResourceMainAddressBindingSource, "LastChangeDate", True))
        Me.fldAddrChangeDt.Enabled = False
        Me.fldAddrChangeDt.Location = New System.Drawing.Point(211, 469)
        Me.fldAddrChangeDt.Name = "fldAddrChangeDt"
        Me.fldAddrChangeDt.ReadOnly = True
        Me.fldAddrChangeDt.Size = New System.Drawing.Size(134, 20)
        Me.fldAddrChangeDt.TabIndex = 16
        Me.fldAddrChangeDt.Text = "fldAddrChangeDt"
        Me.fldAddrChangeDt.Visible = False
        '
        'pnlReview
        '
        Me.pnlReview.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.pnlReview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlReview.Controls.Add(Me.Label55)
        Me.pnlReview.Controls.Add(Me.Label43)
        Me.pnlReview.Controls.Add(Me.txtReviewNote)
        Me.pnlReview.Controls.Add(Me.btnReviewed)
        Me.pnlReview.Controls.Add(Me.cboReview)
        Me.pnlReview.Controls.Add(Me.dtReview)
        Me.pnlReview.Controls.Add(Me.Label44)
        Me.pnlReview.Controls.Add(Me.fldReviewStaff)
        Me.pnlReview.Controls.Add(Me.Label45)
        Me.pnlReview.Location = New System.Drawing.Point(43, 48)
        Me.pnlReview.Name = "pnlReview"
        Me.pnlReview.Size = New System.Drawing.Size(506, 176)
        Me.pnlReview.TabIndex = 1
        '
        'Label55
        '
        Me.Label55.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label55.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label55.Location = New System.Drawing.Point(199, 13)
        Me.Label55.Name = "Label55"
        Me.Label55.Size = New System.Drawing.Size(108, 13)
        Me.Label55.TabIndex = 154
        Me.Label55.Text = "Complete Review"
        '
        'Label43
        '
        Me.Label43.Location = New System.Drawing.Point(142, 110)
        Me.Label43.Name = "Label43"
        Me.Label43.Size = New System.Drawing.Size(73, 18)
        Me.Label43.TabIndex = 12
        Me.Label43.Text = "Review Note:"
        Me.Label43.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtReviewNote
        '
        Me.txtReviewNote.BackColor = System.Drawing.SystemColors.Window
        Me.txtReviewNote.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainResourceBindingSource, "ReviewNote", True))
        Me.txtReviewNote.Location = New System.Drawing.Point(216, 110)
        Me.txtReviewNote.MaxLength = 100
        Me.txtReviewNote.Multiline = True
        Me.txtReviewNote.Name = "txtReviewNote"
        Me.txtReviewNote.Size = New System.Drawing.Size(256, 41)
        Me.txtReviewNote.TabIndex = 11
        Me.txtReviewNote.Text = "Note"
        '
        'btnReviewed
        '
        Me.btnReviewed.Location = New System.Drawing.Point(26, 57)
        Me.btnReviewed.Name = "btnReviewed"
        Me.btnReviewed.Size = New System.Drawing.Size(104, 42)
        Me.btnReviewed.TabIndex = 0
        Me.btnReviewed.Text = "Update Full Review Complete"
        '
        'cboReview
        '
        Me.cboReview.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboReview.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboReview.DataBindings.Add(New System.Windows.Forms.Binding("SelectedItem", Me.MainResourceBindingSource, "ReviewHow", True))
        Me.cboReview.Items.AddRange(New Object() {"Personal Contact", "Print Materials", "Website", "Other"})
        Me.cboReview.Location = New System.Drawing.Point(271, 83)
        Me.cboReview.MaxLength = 50
        Me.cboReview.Name = "cboReview"
        Me.cboReview.RestrictContentToListItems = True
        Me.cboReview.Size = New System.Drawing.Size(201, 21)
        Me.cboReview.TabIndex = 10
        '
        'dtReview
        '
        Me.dtReview.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainResourceBindingSource, "ReviewDate", True))
        Me.dtReview.Location = New System.Drawing.Point(216, 61)
        Me.dtReview.Name = "dtReview"
        Me.dtReview.Size = New System.Drawing.Size(91, 20)
        Me.dtReview.TabIndex = 5
        Me.dtReview.Text = "fldReviewDt"
        '
        'Label44
        '
        Me.Label44.Location = New System.Drawing.Point(146, 61)
        Me.Label44.Name = "Label44"
        Me.Label44.Size = New System.Drawing.Size(69, 18)
        Me.Label44.TabIndex = 9
        Me.Label44.Tag = "Reviewed by"
        Me.Label44.Text = "Reviewed:"
        Me.Label44.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'fldReviewStaff
        '
        Me.fldReviewStaff.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainResourceBindingSource, "ReviewStaffNum", True))
        Me.fldReviewStaff.Location = New System.Drawing.Point(313, 61)
        Me.fldReviewStaff.Name = "fldReviewStaff"
        Me.fldReviewStaff.ReadOnly = True
        Me.fldReviewStaff.Size = New System.Drawing.Size(159, 20)
        Me.fldReviewStaff.TabIndex = 7
        Me.fldReviewStaff.Tag = "Reviewed by"
        Me.fldReviewStaff.Text = "fldReviewStaff"
        '
        'Label45
        '
        Me.Label45.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Label45.Location = New System.Drawing.Point(140, 79)
        Me.Label45.Name = "Label45"
        Me.Label45.Size = New System.Drawing.Size(121, 28)
        Me.Label45.TabIndex = 10
        Me.Label45.Text = "Information Source:"
        Me.Label45.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'fldCreateDt
        '
        Me.fldCreateDt.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainResourceBindingSource, "CreateDate", True))
        Me.fldCreateDt.Enabled = False
        Me.fldCreateDt.Location = New System.Drawing.Point(200, 358)
        Me.fldCreateDt.Name = "fldCreateDt"
        Me.fldCreateDt.ReadOnly = True
        Me.fldCreateDt.Size = New System.Drawing.Size(134, 20)
        Me.fldCreateDt.TabIndex = 0
        Me.fldCreateDt.Text = "fldCreateDt"
        '
        'Label47
        '
        Me.Label47.Location = New System.Drawing.Point(58, 380)
        Me.Label47.Name = "Label47"
        Me.Label47.Size = New System.Drawing.Size(132, 26)
        Me.Label47.TabIndex = 15
        Me.Label47.Tag = "Last Changed by"
        Me.Label47.Text = "Resource Last Changed"
        Me.Label47.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'fldChangeBy
        '
        Me.fldChangeBy.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainResourceBindingSource, "LastChangeStaffNum", True))
        Me.fldChangeBy.Location = New System.Drawing.Point(359, 386)
        Me.fldChangeBy.Name = "fldChangeBy"
        Me.fldChangeBy.ReadOnly = True
        Me.fldChangeBy.Size = New System.Drawing.Size(68, 20)
        Me.fldChangeBy.TabIndex = 3
        Me.fldChangeBy.Tag = "Changed by"
        Me.fldChangeBy.Text = "fldChangedBy"
        '
        'fldCreatedBy
        '
        Me.fldCreatedBy.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainResourceBindingSource, "CreateStaffNum", True))
        Me.fldCreatedBy.Location = New System.Drawing.Point(359, 359)
        Me.fldCreatedBy.Name = "fldCreatedBy"
        Me.fldCreatedBy.ReadOnly = True
        Me.fldCreatedBy.Size = New System.Drawing.Size(68, 20)
        Me.fldCreatedBy.TabIndex = 1
        Me.fldCreatedBy.Tag = "Created by"
        Me.fldCreatedBy.Text = "fldCreatedBy"
        '
        'fldLastChangeDt
        '
        Me.fldLastChangeDt.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainResourceBindingSource, "LastChangeDate", True))
        Me.fldLastChangeDt.Enabled = False
        Me.fldLastChangeDt.Location = New System.Drawing.Point(200, 384)
        Me.fldLastChangeDt.Name = "fldLastChangeDt"
        Me.fldLastChangeDt.ReadOnly = True
        Me.fldLastChangeDt.Size = New System.Drawing.Size(134, 20)
        Me.fldLastChangeDt.TabIndex = 2
        Me.fldLastChangeDt.Text = "fldLastChangedDt"
        '
        'Label5
        '
        Me.Label5.Location = New System.Drawing.Point(96, 358)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(98, 19)
        Me.Label5.TabIndex = 7
        Me.Label5.Tag = "Created by"
        Me.Label5.Text = "Resource Created"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'chkApproved
        '
        Me.chkApproved.DataBindings.Add(New System.Windows.Forms.Binding("Checked", Me.MainResourceBindingSource, "Approved", True))
        Me.chkApproved.Location = New System.Drawing.Point(213, 245)
        Me.chkApproved.Name = "chkApproved"
        Me.chkApproved.Size = New System.Drawing.Size(154, 22)
        Me.chkApproved.TabIndex = 14
        Me.chkApproved.TabStop = False
        Me.chkApproved.Text = "Resource Entry Approved"
        '
        'Label39
        '
        Me.Label39.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label39.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label39.ForeColor = System.Drawing.Color.SlateBlue
        Me.Label39.Location = New System.Drawing.Point(786, 16)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(136, 16)
        Me.Label39.TabIndex = 144
        Me.Label39.Text = "Source Information"
        Me.Label39.Visible = False
        '
        'textbox1Source9
        '
        Me.textbox1Source9.Location = New System.Drawing.Point(506, 156)
        Me.textbox1Source9.Name = "textbox1Source9"
        Me.textbox1Source9.Size = New System.Drawing.Size(100, 20)
        Me.textbox1Source9.TabIndex = 28
        Me.textbox1Source9.Text = "textbox1Source9"
        '
        'TextBox20
        '
        Me.TextBox20.Location = New System.Drawing.Point(506, 132)
        Me.TextBox20.Name = "TextBox20"
        Me.TextBox20.Size = New System.Drawing.Size(100, 20)
        Me.TextBox20.TabIndex = 27
        Me.TextBox20.Text = "TextBox20"
        '
        'pnlResource
        '
        Me.pnlResource.BackColor = System.Drawing.Color.Gainsboro
        Me.pnlResource.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pnlResource.Controls.Add(Me.lblFlagPerson)
        Me.pnlResource.Controls.Add(Me.txtResourceName)
        Me.pnlResource.Controls.Add(Me.lblStar)
        Me.pnlResource.Controls.Add(Me.cboCRGStatus)
        Me.pnlResource.Controls.Add(Me.cboWhyInactive)
        Me.pnlResource.Controls.Add(Me.cboSubtype)
        Me.pnlResource.Controls.Add(Me.cboType)
        Me.pnlResource.Controls.Add(Me.chkActive)
        Me.pnlResource.Controls.Add(Me.chkCRGWebsite)
        Me.pnlResource.Controls.Add(Me.txtReferralSource)
        Me.pnlResource.Controls.Add(Me.Label21)
        Me.pnlResource.Controls.Add(Me.Label20)
        Me.pnlResource.Controls.Add(Me.Label4)
        Me.pnlResource.Controls.Add(Me.chkOrg)
        Me.pnlResource.Controls.Add(Me.chkLocal)
        Me.pnlResource.Controls.Add(Me.rtbDescriptions)
        Me.pnlResource.Controls.Add(Me.tbDescriptions)
        Me.pnlResource.Controls.Add(Me.Label7)
        Me.pnlResource.Location = New System.Drawing.Point(8, 3)
        Me.pnlResource.Name = "pnlResource"
        Me.pnlResource.Size = New System.Drawing.Size(443, 596)
        Me.pnlResource.TabIndex = 0
        '
        'lblFlagPerson
        '
        Me.lblFlagPerson.BackColor = System.Drawing.Color.LemonChiffon
        Me.lblFlagPerson.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblFlagPerson.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFlagPerson.ForeColor = System.Drawing.SystemColors.Highlight
        Me.lblFlagPerson.Location = New System.Drawing.Point(206, 52)
        Me.lblFlagPerson.Name = "lblFlagPerson"
        Me.lblFlagPerson.Size = New System.Drawing.Size(225, 34)
        Me.lblFlagPerson.TabIndex = 782
        Me.lblFlagPerson.Text = "Use the Author/Contact GRID   ----------->" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "to ENTER or EDIT a Person resource na" & _
    "me."
        Me.lblFlagPerson.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblFlagPerson.Visible = False
        '
        'txtResourceName
        '
        Me.txtResourceName.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainResourceBindingSource, "ResourceName", True))
        Me.txtResourceName.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtResourceName.ForeColor = System.Drawing.SystemColors.ControlText
        Me.txtResourceName.Location = New System.Drawing.Point(52, 11)
        Me.txtResourceName.MaxLength = 200
        Me.txtResourceName.MinimumSize = New System.Drawing.Size(0, 70)
        Me.txtResourceName.Multiline = True
        Me.txtResourceName.Name = "txtResourceName"
        Me.txtResourceName.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtResourceName.Size = New System.Drawing.Size(371, 73)
        Me.txtResourceName.TabIndex = 0
        Me.txtResourceName.Tag = "RESOURCE NAME"
        '
        'lblStar
        '
        Me.lblStar.BackColor = System.Drawing.Color.Yellow
        Me.lblStar.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStar.ImageAlign = System.Drawing.ContentAlignment.TopRight
        Me.lblStar.Location = New System.Drawing.Point(402, 277)
        Me.lblStar.Margin = New System.Windows.Forms.Padding(0)
        Me.lblStar.MaximumSize = New System.Drawing.Size(16, 16)
        Me.lblStar.Name = "lblStar"
        Me.lblStar.Size = New System.Drawing.Size(16, 16)
        Me.lblStar.TabIndex = 158
        Me.lblStar.Text = "*"
        Me.lblStar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.ToolTip1.SetToolTip(Me.lblStar, "CRG Annotation")
        Me.lblStar.Visible = False
        '
        'cboCRGStatus
        '
        Me.cboCRGStatus.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboCRGStatus.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboCRGStatus.DataBindings.Add(New System.Windows.Forms.Binding("SelectedItem", Me.MainResourceBindingSource, "NewCRGStatus", True))
        Me.cboCRGStatus.FormattingEnabled = True
        Me.cboCRGStatus.ItemHeight = 13
        Me.cboCRGStatus.Location = New System.Drawing.Point(279, 119)
        Me.cboCRGStatus.Margin = New System.Windows.Forms.Padding(0)
        Me.cboCRGStatus.Name = "cboCRGStatus"
        Me.cboCRGStatus.RestrictContentToListItems = True
        Me.cboCRGStatus.Size = New System.Drawing.Size(147, 21)
        Me.cboCRGStatus.TabIndex = 6
        '
        'cboWhyInactive
        '
        Me.cboWhyInactive.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboWhyInactive.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboWhyInactive.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.MainResourceBindingSource, "ReasonInactive", True))
        Me.cboWhyInactive.DisplayMember = "Statusname"
        Me.cboWhyInactive.DropDownWidth = 200
        Me.cboWhyInactive.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cboWhyInactive.FormattingEnabled = True
        Me.cboWhyInactive.Location = New System.Drawing.Point(279, 185)
        Me.cboWhyInactive.Name = "cboWhyInactive"
        Me.cboWhyInactive.RestrictContentToListItems = True
        Me.cboWhyInactive.Size = New System.Drawing.Size(146, 21)
        Me.cboWhyInactive.TabIndex = 11
        Me.cboWhyInactive.TabStop = False
        Me.cboWhyInactive.Tag = "Reason Inactive"
        Me.ToolTip1.SetToolTip(Me.cboWhyInactive, "Reason why Inactive")
        Me.cboWhyInactive.ValueMember = "StatusName"
        '
        'cboSubtype
        '
        Me.cboSubtype.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboSubtype.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboSubtype.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.MainResourceBindingSource, "Subtype", True))
        Me.cboSubtype.DataSource = Me.DsType1
        Me.cboSubtype.DisplayMember = "tType.relTypeSubtype.ResourceSubType"
        Me.cboSubtype.FormattingEnabled = True
        Me.cboSubtype.Location = New System.Drawing.Point(52, 122)
        Me.cboSubtype.Name = "cboSubtype"
        Me.cboSubtype.RestrictContentToListItems = True
        Me.cboSubtype.Size = New System.Drawing.Size(196, 21)
        Me.cboSubtype.TabIndex = 2
        Me.cboSubtype.ValueMember = "tType.RelTypeSubtype.ResourceSubType"
        '
        'DsType1
        '
        Me.DsType1.DataSetName = "dsType"
        Me.DsType1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'cboType
        '
        Me.cboType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboType.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.MainResourceBindingSource, "ResourceType", True))
        Me.cboType.DataSource = Me.DsType1
        Me.cboType.DisplayMember = "tType.ResourceType"
        Me.cboType.FormattingEnabled = True
        Me.cboType.Location = New System.Drawing.Point(52, 95)
        Me.cboType.Name = "cboType"
        Me.cboType.RestrictContentToListItems = True
        Me.cboType.Size = New System.Drawing.Size(196, 21)
        Me.cboType.TabIndex = 1
        Me.cboType.ValueMember = "tType.ResourceType"
        '
        'chkActive
        '
        Me.chkActive.DataBindings.Add(New System.Windows.Forms.Binding("Checked", Me.MainResourceBindingSource, "Active", True))
        Me.chkActive.Location = New System.Drawing.Point(270, 156)
        Me.chkActive.Name = "chkActive"
        Me.chkActive.Size = New System.Drawing.Size(160, 31)
        Me.chkActive.TabIndex = 10
        Me.chkActive.TabStop = False
        Me.chkActive.Text = "Active, " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "   or Reason Inactive:"
        Me.ToolTip1.SetToolTip(Me.chkActive, "Uncheck Active if resource not available or not recommendable.")
        '
        'chkCRGWebsite
        '
        Me.chkCRGWebsite.DataBindings.Add(New System.Windows.Forms.Binding("Checked", Me.MainResourceBindingSource, "NewCRG", True))
        Me.chkCRGWebsite.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkCRGWebsite.Location = New System.Drawing.Point(270, 87)
        Me.chkCRGWebsite.Margin = New System.Windows.Forms.Padding(0)
        Me.chkCRGWebsite.Name = "chkCRGWebsite"
        Me.chkCRGWebsite.Size = New System.Drawing.Size(160, 35)
        Me.chkCRGWebsite.TabIndex = 5
        Me.chkCRGWebsite.TabStop = False
        Me.chkCRGWebsite.Text = "On CRG Website," & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "   or CRG Status:"
        '
        'txtReferralSource
        '
        Me.txtReferralSource.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainResourceBindingSource, "ReferralSource", True))
        Me.txtReferralSource.Location = New System.Drawing.Point(52, 153)
        Me.txtReferralSource.MaxLength = 2147483647
        Me.txtReferralSource.MinimumSize = New System.Drawing.Size(0, 40)
        Me.txtReferralSource.Multiline = True
        Me.txtReferralSource.Name = "txtReferralSource"
        Me.txtReferralSource.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtReferralSource.Size = New System.Drawing.Size(214, 118)
        Me.txtReferralSource.TabIndex = 3
        Me.ToolTip1.SetToolTip(Me.txtReferralSource, "Use semicolon to separate items.")
        '
        'Label21
        '
        Me.Label21.Location = New System.Drawing.Point(16, 96)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(34, 15)
        Me.Label21.TabIndex = 150
        Me.Label21.Text = "Type"
        Me.Label21.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label20
        '
        Me.Label20.Location = New System.Drawing.Point(1, 122)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(49, 18)
        Me.Label20.TabIndex = 149
        Me.Label20.Text = "Subtype"
        Me.Label20.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(3, 156)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(47, 31)
        Me.Label4.TabIndex = 148
        Me.Label4.Text = "Referral Source"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.ToolTip1.SetToolTip(Me.Label4, "Use semicolon to separate items.")
        '
        'chkOrg
        '
        Me.chkOrg.Enabled = False
        Me.chkOrg.ForeColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.chkOrg.Location = New System.Drawing.Point(272, 241)
        Me.chkOrg.Name = "chkOrg"
        Me.chkOrg.Size = New System.Drawing.Size(115, 19)
        Me.chkOrg.TabIndex = 124
        Me.chkOrg.TabStop = False
        Me.chkOrg.Text = "in Mailing List db"
        '
        'chkLocal
        '
        Me.chkLocal.DataBindings.Add(New System.Windows.Forms.Binding("Checked", Me.MainResourceBindingSource, "LocalUse", True))
        Me.chkLocal.Location = New System.Drawing.Point(272, 218)
        Me.chkLocal.Name = "chkLocal"
        Me.chkLocal.Size = New System.Drawing.Size(99, 17)
        Me.chkLocal.TabIndex = 12
        Me.chkLocal.TabStop = False
        Me.chkLocal.Text = "Local Scope"
        '
        'rtbDescriptions
        '
        Me.rtbDescriptions.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainResourceBindingSource, "PrivateDescription", True))
        Me.rtbDescriptions.ForeColor = System.Drawing.SystemColors.ControlText
        Me.rtbDescriptions.Location = New System.Drawing.Point(2, 295)
        Me.rtbDescriptions.Name = "rtbDescriptions"
        Me.rtbDescriptions.Size = New System.Drawing.Size(423, 294)
        Me.rtbDescriptions.TabIndex = 4
        Me.rtbDescriptions.Tag = "DESCRIPTION"
        Me.rtbDescriptions.Text = ""
        Me.ToolTip1.SetToolTip(Me.rtbDescriptions, "Right click for Copy/Cut/Paste options.")
        '
        'tbDescriptions
        '
        Me.tbDescriptions.Controls.Add(Me.pgPrivate)
        Me.tbDescriptions.Controls.Add(Me.pgPublic)
        Me.tbDescriptions.Controls.Add(Me.pgNewCRG)
        Me.tbDescriptions.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbDescriptions.Location = New System.Drawing.Point(2, 274)
        Me.tbDescriptions.Margin = New System.Windows.Forms.Padding(0)
        Me.tbDescriptions.Name = "tbDescriptions"
        Me.tbDescriptions.SelectedIndex = 0
        Me.tbDescriptions.Size = New System.Drawing.Size(420, 23)
        Me.tbDescriptions.TabIndex = 118
        Me.tbDescriptions.TabStop = False
        '
        'pgPrivate
        '
        Me.pgPrivate.Location = New System.Drawing.Point(4, 22)
        Me.pgPrivate.Margin = New System.Windows.Forms.Padding(0)
        Me.pgPrivate.Name = "pgPrivate"
        Me.pgPrivate.Size = New System.Drawing.Size(412, 0)
        Me.pgPrivate.TabIndex = 0
        Me.pgPrivate.Tag = "  PrivateDescription   "
        Me.pgPrivate.Text = "Private Description"
        '
        'pgPublic
        '
        Me.pgPublic.Location = New System.Drawing.Point(4, 22)
        Me.pgPublic.Margin = New System.Windows.Forms.Padding(0)
        Me.pgPublic.Name = "pgPublic"
        Me.pgPublic.Size = New System.Drawing.Size(412, 0)
        Me.pgPublic.TabIndex = 2
        Me.pgPublic.Tag = "  PublicDescription  "
        Me.pgPublic.Text = "Public Description"
        '
        'pgNewCRG
        '
        Me.pgNewCRG.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pgNewCRG.ForeColor = System.Drawing.SystemColors.ControlText
        Me.pgNewCRG.Location = New System.Drawing.Point(4, 22)
        Me.pgNewCRG.Margin = New System.Windows.Forms.Padding(0)
        Me.pgNewCRG.Name = "pgNewCRG"
        Me.pgNewCRG.Size = New System.Drawing.Size(412, 0)
        Me.pgNewCRG.TabIndex = 3
        Me.pgNewCRG.Tag = "NewCRGDescription"
        Me.pgNewCRG.Text = "CRG Annotation       "
        Me.pgNewCRG.UseVisualStyleBackColor = True
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Location = New System.Drawing.Point(3, 3)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(47, 46)
        Me.Label7.TabIndex = 117
        Me.Label7.Text = "Name"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.Controls.Add(Me.pnlResource)
        Me.Panel1.Controls.Add(Me.tbMain)
        Me.Panel1.Location = New System.Drawing.Point(8, 42)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1150, 613)
        Me.Panel1.TabIndex = 222
        '
        'SqlConnection1
        '
        Me.SqlConnection1.ConnectionString = "Data Source=SOLOMON2008\SOLOMON2008;Initial Catalog=InfoCtr_be;Integrated Securit" & _
    "y=True"
        Me.SqlConnection1.FireInfoMessageEventOnUserErrors = False
        '
        'btnHelp
        '
        Me.btnHelp.Image = Global.InfoCtr.My.Resources.Resources.btnHelp
        Me.btnHelp.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnHelp.Location = New System.Drawing.Point(1117, 3)
        Me.btnHelp.Name = "btnHelp"
        Me.btnHelp.Size = New System.Drawing.Size(25, 25)
        Me.btnHelp.TabIndex = 229
        Me.btnHelp.Tag = "ctlNeutral"
        Me.btnHelp.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ToolTip1.SetToolTip(Me.btnHelp, "Help")
        Me.btnHelp.UseVisualStyleBackColor = True
        Me.btnHelp.Visible = False
        '
        'btnSaveExit
        '
        Me.btnSaveExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSaveExit.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.btnSaveExit.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSaveExit.Image = Global.InfoCtr.My.Resources.Resources.btnSaveExit
        Me.btnSaveExit.ImageAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSaveExit.Location = New System.Drawing.Point(127, 1)
        Me.btnSaveExit.Name = "btnSaveExit"
        Me.btnSaveExit.Size = New System.Drawing.Size(40, 35)
        Me.btnSaveExit.TabIndex = 418
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
        Me.btnNew.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.btnNew.Location = New System.Drawing.Point(3, 1)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(40, 35)
        Me.btnNew.TabIndex = 417
        Me.btnNew.TabStop = False
        Me.btnNew.Text = "New"
        Me.btnNew.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.ToolTip1.SetToolTip(Me.btnNew, "Add New Recommendation, Feedback, or Alert")
        Me.btnNew.UseVisualStyleBackColor = False
        '
        'btnDelete
        '
        Me.btnDelete.BackColor = System.Drawing.SystemColors.Control
        Me.btnDelete.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Image = Global.InfoCtr.My.Resources.Resources.btnDelete
        Me.btnDelete.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnDelete.Location = New System.Drawing.Point(789, 7)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(16, 35)
        Me.btnDelete.TabIndex = 416
        Me.btnDelete.Text = "Delete"
        Me.btnDelete.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.btnDelete, "Delete this Resource")
        Me.btnDelete.UseVisualStyleBackColor = False
        Me.btnDelete.Visible = False
        '
        'btnImage
        '
        Me.btnImage.BackColor = System.Drawing.SystemColors.Control
        Me.btnImage.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ErrorProvider1.SetIconAlignment(Me.btnImage, System.Windows.Forms.ErrorIconAlignment.BottomLeft)
        Me.btnImage.Image = CType(resources.GetObject("btnImage.Image"), System.Drawing.Image)
        Me.btnImage.ImageAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnImage.Location = New System.Drawing.Point(86, 1)
        Me.btnImage.Name = "btnImage"
        Me.btnImage.Size = New System.Drawing.Size(41, 35)
        Me.btnImage.TabIndex = 783
        Me.btnImage.Tag = "AttachFile"
        Me.btnImage.Text = "Image"
        Me.btnImage.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.ToolTip1.SetToolTip(Me.btnImage, "for New CRG")
        Me.btnImage.UseVisualStyleBackColor = False
        '
        'btnOpenFile
        '
        Me.btnOpenFile.BackColor = System.Drawing.SystemColors.Control
        Me.btnOpenFile.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnOpenFile.Image = Global.InfoCtr.My.Resources.Resources.btnAttached
        Me.btnOpenFile.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.btnOpenFile.Location = New System.Drawing.Point(45, 1)
        Me.btnOpenFile.Name = "btnOpenFile"
        Me.btnOpenFile.Size = New System.Drawing.Size(41, 35)
        Me.btnOpenFile.TabIndex = 782
        Me.btnOpenFile.Text = "Files"
        Me.btnOpenFile.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.btnOpenFile.UseVisualStyleBackColor = False
        '
        'flagInactive
        '
        Me.flagInactive.BackColor = System.Drawing.Color.Yellow
        Me.flagInactive.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.flagInactive.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.flagInactive.Location = New System.Drawing.Point(389, 5)
        Me.flagInactive.Name = "flagInactive"
        Me.flagInactive.Size = New System.Drawing.Size(177, 34)
        Me.flagInactive.TabIndex = 223
        Me.flagInactive.Text = "Caution - this Resource is INACTIVE."
        Me.flagInactive.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.flagInactive.Visible = False
        '
        'flagAlert
        '
        Me.flagAlert.BackColor = System.Drawing.Color.Yellow
        Me.flagAlert.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.flagAlert.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.flagAlert.ForeColor = System.Drawing.Color.Red
        Me.flagAlert.Location = New System.Drawing.Point(572, 5)
        Me.flagAlert.Name = "flagAlert"
        Me.flagAlert.Size = New System.Drawing.Size(187, 34)
        Me.flagAlert.TabIndex = 224
        Me.flagAlert.Text = "Caution - this Resource has ALERTS."
        Me.flagAlert.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.flagAlert.Visible = False
        '
        'MainMenu1
        '
        Me.MainMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem1, Me.MenuItem3, Me.MenuItem7, Me.miHelp})
        '
        'MenuItem1
        '
        Me.MenuItem1.Index = 0
        Me.MenuItem1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.miNew, Me.miAttach, Me.miOpenFile, Me.miClose})
        Me.MenuItem1.Text = "File"
        '
        'miNew
        '
        Me.miNew.Index = 0
        Me.miNew.Text = "New"
        '
        'miAttach
        '
        Me.miAttach.Index = 1
        Me.miAttach.Text = "Attach File"
        '
        'miOpenFile
        '
        Me.miOpenFile.Index = 2
        Me.miOpenFile.Text = "Open File"
        '
        'miClose
        '
        Me.miClose.Index = 3
        Me.miClose.Text = "Close Window"
        '
        'MenuItem3
        '
        Me.MenuItem3.Index = 1
        Me.MenuItem3.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.miSave, Me.miCancel, Me.miDelete})
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
        'miDelete
        '
        Me.miDelete.Index = 2
        Me.miDelete.Text = "Delete Resource"
        '
        'MenuItem7
        '
        Me.MenuItem7.Index = 2
        Me.MenuItem7.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.miPrintPublic, Me.miPrintDescription, Me.miPrintAllFields, Me.miRptFeedback})
        Me.MenuItem7.Text = "Reports"
        '
        'miPrintPublic
        '
        Me.miPrintPublic.Index = 0
        Me.miPrintPublic.Text = "Public Report"
        '
        'miPrintDescription
        '
        Me.miPrintDescription.Index = 1
        Me.miPrintDescription.Text = "Basic Information and Descriptions"
        '
        'miPrintAllFields
        '
        Me.miPrintAllFields.Index = 2
        Me.miPrintAllFields.Text = "All Fields"
        '
        'miRptFeedback
        '
        Me.miRptFeedback.Index = 3
        Me.miRptFeedback.Text = "Feedback Report"
        '
        'miHelp
        '
        Me.miHelp.Index = 3
        Me.miHelp.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.miHelpGeneral, Me.miDefinitions, Me.miCRG})
        Me.miHelp.Text = "Help"
        '
        'miHelpGeneral
        '
        Me.miHelpGeneral.Index = 0
        Me.miHelpGeneral.Text = "General Help"
        '
        'miDefinitions
        '
        Me.miDefinitions.Index = 1
        Me.miDefinitions.Text = "Resource Type Definitions"
        '
        'miCRG
        '
        Me.miCRG.Index = 2
        Me.miCRG.Text = "CRG Website Resource Entry"
        '
        'StatusBar1
        '
        Me.StatusBar1.Location = New System.Drawing.Point(0, 701)
        Me.StatusBar1.Name = "StatusBar1"
        Me.StatusBar1.Panels.AddRange(New System.Windows.Forms.StatusBarPanel() {Me.StatusBarPanel1, Me.StatusBarPanelID, Me.StatusBarPanel2})
        Me.StatusBar1.ShowPanels = True
        Me.StatusBar1.Size = New System.Drawing.Size(1161, 20)
        Me.StatusBar1.TabIndex = 225
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
        Me.StatusBarPanelID.BorderStyle = System.Windows.Forms.StatusBarPanelBorderStyle.Raised
        Me.StatusBarPanelID.MinWidth = 200
        Me.StatusBarPanelID.Name = "StatusBarPanelID"
        Me.StatusBarPanelID.Text = "Resource ID"
        Me.StatusBarPanelID.Width = 200
        '
        'StatusBarPanel2
        '
        Me.StatusBarPanel2.Alignment = System.Windows.Forms.HorizontalAlignment.Right
        Me.StatusBarPanel2.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring
        Me.StatusBarPanel2.Name = "StatusBarPanel2"
        Me.StatusBarPanel2.Width = 744
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.SystemColors.Highlight
        Me.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel3.Controls.Add(Me.btnImage)
        Me.Panel3.Controls.Add(Me.btnOpenFile)
        Me.Panel3.Controls.Add(Me.btnSaveExit)
        Me.Panel3.Controls.Add(Me.btnNew)
        Me.Panel3.Location = New System.Drawing.Point(937, 0)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(171, 40)
        Me.Panel3.TabIndex = 781
        '
        'OrgNum
        '
        Me.OrgNum.BackColor = System.Drawing.SystemColors.Control
        Me.OrgNum.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.OrgNum.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainResourceBindingSource, "OrgNum", True))
        Me.OrgNum.Enabled = False
        Me.OrgNum.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OrgNum.ForeColor = System.Drawing.SystemColors.GrayText
        Me.OrgNum.Location = New System.Drawing.Point(668, 685)
        Me.OrgNum.Name = "OrgNum"
        Me.OrgNum.Size = New System.Drawing.Size(33, 14)
        Me.OrgNum.TabIndex = 234
        Me.OrgNum.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'ICCResourceID
        '
        Me.ICCResourceID.BackColor = System.Drawing.SystemColors.Control
        Me.ICCResourceID.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.ICCResourceID.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainResourceBindingSource, "ICCResourceID", True))
        Me.ICCResourceID.Enabled = False
        Me.ICCResourceID.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ICCResourceID.ForeColor = System.Drawing.SystemColors.GrayText
        Me.ICCResourceID.Location = New System.Drawing.Point(1058, 683)
        Me.ICCResourceID.Name = "ICCResourceID"
        Me.ICCResourceID.Size = New System.Drawing.Size(63, 16)
        Me.ICCResourceID.TabIndex = 230
        Me.ICCResourceID.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlDark
        Me.Label1.Location = New System.Drawing.Point(631, 681)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(31, 18)
        Me.Label1.TabIndex = 235
        Me.Label1.Text = "Org#"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlDark
        Me.Label6.Location = New System.Drawing.Point(998, 683)
        Me.Label6.Margin = New System.Windows.Forms.Padding(0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(61, 14)
        Me.Label6.TabIndex = 232
        Me.Label6.Text = "Resource"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ErrorProvider1
        '
        Me.ErrorProvider1.ContainerControl = Me
        '
        'ppStrip
        '
        Me.ppStrip.Name = "ppStrip"
        Me.ppStrip.Size = New System.Drawing.Size(61, 4)
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(32, 19)
        '
        'ToolStripMenuItem2
        '
        Me.ToolStripMenuItem2.Name = "ToolStripMenuItem2"
        Me.ToolStripMenuItem2.Size = New System.Drawing.Size(32, 19)
        '
        'Website
        '
        Me.Website.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Website.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainResourceBindingSource, "Website", True))
        Me.Website.Location = New System.Drawing.Point(76, 113)
        Me.Website.MaxLength = 255
        Me.Website.Name = "Website"
        Me.Website.Size = New System.Drawing.Size(165, 20)
        Me.Website.TabIndex = 80
        '
        'MainResourceTableAdapter
        '
        Me.MainResourceTableAdapter.ClearBeforeFill = True
        '
        'ResourceMainAddressTableAdapter
        '
        Me.ResourceMainAddressTableAdapter.ClearBeforeFill = True
        '
        'frmMainResource
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.AutoScroll = True
        Me.BackColor = System.Drawing.Color.WhiteSmoke
        Me.CausesValidation = False
        Me.ClientSize = New System.Drawing.Size(1161, 721)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.OrgNum)
        Me.Controls.Add(Me.btnDelete)
        Me.Controls.Add(Me.ICCResourceID)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.btnHelp)
        Me.Controls.Add(Me.Label39)
        Me.Controls.Add(Me.StatusBar1)
        Me.Controls.Add(Me.flagAlert)
        Me.Controls.Add(Me.flagInactive)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Label2)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Menu = Me.MainMenu1
        Me.Name = "frmMainResource"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Tag = "RESOURCE"
        Me.Text = "RESOURCE DETAIL"
        Me.tbMain.ResumeLayout(False)
        Me.pgExtra.ResumeLayout(False)
        Me.pgExtra.PerformLayout()
        CType(Me.MainResourceBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsMainResource1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        CType(Me.grdExtras, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsResourceExtra1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.grdLocation, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tbSource.ResumeLayout(False)
        Me.oldAddress.ResumeLayout(False)
        Me.oldAddress.PerformLayout()
        Me.pnlOrgAddress.ResumeLayout(False)
        Me.pgPublish.ResumeLayout(False)
        Me.pgPublish.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.tbPublishing.ResumeLayout(False)
        Me.pgArticle.ResumeLayout(False)
        Me.pgArticle.PerformLayout()
        Me.pgBook.ResumeLayout(False)
        Me.pgBook.PerformLayout()
        Me.pgMedia.ResumeLayout(False)
        Me.pgMedia.PerformLayout()
        Me.pgRecommendation.ResumeLayout(False)
        CType(Me.grdRecommend, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsRFA, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pgFeedback.ResumeLayout(False)
        CType(Me.grdFeedback, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pgAlert.ResumeLayout(False)
        CType(Me.grdAlert, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pgOther.ResumeLayout(False)
        Me.pgOther.PerformLayout()
        CType(Me.ResourceMainAddressBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlReview.ResumeLayout(False)
        Me.pnlReview.PerformLayout()
        Me.pnlResource.ResumeLayout(False)
        Me.pnlResource.PerformLayout()
        CType(Me.DsType1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tbDescriptions.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        CType(Me.StatusBarPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.StatusBarPanelID, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.StatusBarPanel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel3.ResumeLayout(False)
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

#Region "Load"

    'LOAD
    Private Sub frmMainResource_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Dim i As Integer

        Me.SuspendLayout()
        SetStatusBarText("loading")

SetMainDSConnection:
        Me.MainResourceTableAdapter.Connection = sc
        '  Me.ResourceMainAddressTableAdapter.Connection = sc
        Dim tResourceTypeTableAdapter As New dsTypeTableAdapters.tTypeTableAdapter
        Dim tResourceSubtypeTableAdapter As New dsTypeTableAdapters.tSubtypeTableAdapter
        tResourceSubtypeTableAdapter.Connection = sc
        tResourceTypeTableAdapter.Connection = sc

setDefaults:
        enumRecommend = New structHeadings("Recommendation", "RECOMMENDATIONS")
        enumFeedback = New structHeadings("Feedback", "FEEDBACK")
        enumAlert = New structHeadings("Alert", "ALERTS")

        ctlIdentify = Me.txtResourceName
        ctlNeutral = Me.btnNew
        mainTopic = "Resource"
        mainDS = Me.DsMainResource1
        mainTbl = Me.DsMainResource1.MainResource
        mainBSrce = Me.MainResourceBindingSource
        GetResourceLocation = New DataTable("GetResourceLocation")

LoadComboboxes:
        'KEYWORDS
        SetStatusBarText("Loading Keyword Combos")

        Dim cboar() As ComboBox = {cboKey1, cboKey2, cboKey3, cboKey4}
        For i = 0 To 3
            modGlobalVar.LoadCRGCombo(cboar(i))
        Next i

        'RESOURCE TYPE
        SetStatusBarText("Loading Type combo")
        tResourceSubtypeTableAdapter.Fill(Me.DsType1.tSubtype)
        tResourceTypeTableAdapter.Fill(Me.DsType1.tType)

        Me.cboType.DataSource = Me.DsType1
        Me.cboSubtype.DataSource = Me.DsType1
        Me.cboWhyInactive.DataSource = tblResourceInactive

        'PUBLISHER
        LoadPubCombo()
        cmd.Parameters.Add("@IDVal", System.Data.SqlDbType.Int, 4)
        cmd.Parameters.Add("@IDFld", System.Data.SqlDbType.VarChar, 30)

FormSetup:
        'grid field initialize
        modGlobalVar.EnableGridTextboxes(Me.grdRecommend)
        modGlobalVar.EnableGridTextboxes(Me.grdFeedback)
        modGlobalVar.EnableGridTextboxes(Me.grdExtras)
        modGlobalVar.EnableGridTextboxes(Me.grdAlert)
        modGlobalVar.EnableGridTextboxes(Me.grdLocation)

        Forms.Add(Me)
        Me.ResumeLayout()

        isLoaded = True
        SetStatusBarText("Done")

    End Sub 'load

    'RESET VISIBILITY, PERMISSIONS, FLAGS
    Public Sub Reload() ' after form loads and fields are bound

        SetStatusBarText("Reloading")
        Me.SuspendLayout()
        '...............................
ResetVars:
        objHowClose = ObjClose.CloseByControl '(=5: form X)
        Me.StatusBarPanelID.Text = mainTopic & " ID: " & ThisID.ToString
        Me.lblEntityType.Text = mainTopic
        Me.lblEntityNum.Text = ThisID.ToString
        Me.lblAddressType.Text = mainTopic & IsNull(cboType.SelectedValue, 0).ToString
        Me.ToolTip1.SetToolTip(Me.lblLink, txtURL.Text)

FillGrids:
        'AUTHORS/CONTACTS
        ReloadExtras()
        'LOCATIONS
        Dim sqlLocate As New SqlCommand("[GetResLocation]", sc)
        sqlLocate.CommandType = CommandType.StoredProcedure
        sqlLocate.Parameters.Add("@IDVal", SqlDbType.Int).Value = ThisID

        Try
            modGlobalVar.LoadDataTable(GetResourceLocation, sqlLocate)
            '  Me.grdLocation.Datamember = GetResourceLocation
        Catch ex As System.Exception
            modGlobalVar.Msg("ERROR: loading locations  ", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            '   sc.Close()
        End Try
        '     dvLocations = New DataView(GetResourceLocation) 'dv needed for filtering
        Me.grdLocation.DataSource = GetResourceLocation

DescriptionTab:
        ' CHECK FOR DESCRIPTION FIELD CONTENTS TO DISPLAY CHECKMARK IN HEADING
        Dim pg As TabPage
        ' Dim drw As DataRow = CType(Me.DsMainResource1.MainResource, DataTable).Rows(0)
        Dim drw As DataRow = CType(mainTbl, DataTable).Rows(0)
        Dim l As Integer
        'ADD >> to TAB HEADINGS

        lblStar.Visible = False

        For Each pg In Me.tbDescriptions.Controls
            l = Len(drw(mainTbl.Columns(Trim(pg.Tag)), DataRowVersion.Current).ToString)
            If l > 2 Then
                pg.Text = Replace(pg.Text, ">>", "") & " >>" '" »"   'ChrW(251)  '"v"  U+221A  0xFB    'won't take because code is in courier
                If pg.Tag = "NewCRGDescription" Then
                    lblStar.Visible = True
                End If
            End If
        Next pg

        'SET RTB DEFAULT BINDING - why?
        Me.rtbDescriptions.DataBindings.Clear()
        Me.rtbDescriptions.DataBindings.Add(New Binding("Text", mainTbl, "PrivateDescription"))
        Me.tbDescriptions.Enabled = True

        '..................................
        'CHECK INACTIVE FLAG
        SetInactiveFlag()

        'GET INDEX TERMS
        RefreshIndexTerms()
        '..................................

SetCRGPermissions:
        'CRG field restrictions
        If StaffCRGFull.Contains(usr) Then
            Me.chkCRGWebsite.Enabled = True
            Me.cboCRGStatus.Enabled = True
        Else
            Me.chkCRGWebsite.Enabled = False
            Me.cboCRGStatus.Enabled = False
        End If

CongrAsResource:
        'IF RESOURCE IS ALSO in tblORGS
        If Me.OrgNum.Text = String.Empty Then
            Me.chkOrg.Checked = False
            Me.pnlOrgAddress.Visible = False
            Me.lblAddressRequired.Visible = True
        Else

            Me.chkOrg.Checked = True
            Me.pnlOrgAddress.Visible = True
            Dim sql As New SqlCommand("SELECT Street1 as Street, City, State, Zip, Phone, Fax, Email, Website FROM tblORG WHERE orgID = " & Me.OrgNum.Text, sc)
            Dim rdr As SqlDataReader
            If Not SCConnect() Then
                Exit Sub
            End If

            Try
                rdr = sql.ExecuteReader
                rdr.Read()
                Me.rtxtOrgAddress.Text = " ADDRESS INFORMATION" & NextLine & NextLine
                For i As Integer = 0 To 7
                    Me.rtxtOrgAddress.Text = Me.rtxtOrgAddress.Text & rdr.GetName(i) & ": " & rdr.GetValue(i)
                    If IsDBNull(rdr.GetValue(i)) Then
                        Me.rtxtOrgAddress.Text = Me.rtxtOrgAddress.Text & rdr.GetValue(i) & NextLine
                    Else
                        Me.rtxtOrgAddress.Text = Me.rtxtOrgAddress.Text & NextLine
                    End If
                Next i

            Catch
            Finally
                rdr.Close()
            End Try
            sc.Close()
            Me.lblAddressRequired.Visible = False 'Text = "Edit address from Organization main screen"

        End If

findfiles:
        modPopup.FindFiles(ThisID, DocPath, ppFile, ehFile, Me.miOpenFile, Me.btnOpenFile, My.Resources.btnAttached, Me.ToolTip1)

        FindImages()

SelectDefaultTabs:
        Me.tbDescriptions.SelectTab(0)

        SelectAddressTab()
        Me.txtResourceName.Focus()
        Me.ResumeLayout()

        SetStatusBarText("Done reload")

    End Sub 'reload

    Private Sub FindImages()
        'IMAGES
        modPopup.FindFiles(ThisID, ImagePath, ppImage, ehImage, Nothing, Me.btnImage, Me.btnImage.Image, Me.ToolTip1)
    End Sub

    'LOAD AUTHOR/CONTACT GRID
    Public Sub ReloadExtras()
        '  modGlobalVar.Msg("in reload extras")
        Dim sql As New SqlCommand("[GetResourceExtra]", sc)
        sql.CommandType = CommandType.StoredProcedure
        sql.Parameters.Add("@ID", SqlDbType.Int).Value = ThisID

        Try
            modGlobalVar.LoadDataTable(Me.DsResourceExtra1.tblResourceExtra, sql)
        Catch ex As System.Exception
            modGlobalVar.msg("ERROR: loading extras 1  ", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally

        End Try

    End Sub

    'LOAD PUB and CRG Status DD
    Private Sub LoadPubCombo()
        Dim dr As SqlDataReader
        'Publishers
        Dim cmd As New SqlCommand("SELECT distinct PublisherName, PublisherName + ': ' + City  as PubCity FROM tblPublisher", sc)
        Me.cboPublisher.Items.Clear()
        If Not SCConnect() Then
            Exit Sub
        End If

        dr = cmd.ExecuteReader
        While dr.Read()
            If Not IsDBNull(dr.GetString(0)) Then
                Me.cboPublisher.Items.Add(dr.GetString(0))
            End If
        End While
        dr.Close()
        'CRG Status
        cmd.CommandText = "Select StatusName from luStatus where Topic = 'CRGStatus' ORDER BY SortNum"
        dr = cmd.ExecuteReader
        While dr.Read()
            If Not IsDBNull(dr.GetString(0)) Then
                Me.cboCRGStatus.Items.Add(dr.GetString(0))

            End If
        End While

        dr.Close()
        sc.Close()

    End Sub

    'VISIBILITY OF ADDRESS TAB CONTENTS
    Private Sub SelectAddressTab()

        Select Case IsNull(Me.cboType.Text, "")
            Case Is = "Organization", "Person", "Event"
                Me.lblAddressRequired.Visible = True
                Me.tbSource.SelectedTab = Me.oldAddress

            Case Is = "Equipment" 'retired
                '  Me.lblAddressRequired.Visible = True
                Me.tbSource.SelectedTab = Me.oldAddress

            Case "Article"
                Me.tbSource.SelectedTab = Me.pgPublish
                Me.tbPublishing.SelectedTab = Me.pgArticle
                Me.lblAddressRequired.Visible = False

            Case "Book", "Software", "Periodical"
                Me.tbSource.SelectedTab = Me.pgPublish
                Me.tbPublishing.SelectedTab = Me.pgBook
                Me.lblAddressRequired.Visible = False

            Case "Media"
                Me.tbSource.SelectedTab = Me.pgPublish
                Me.tbPublishing.SelectedTab = Me.pgMedia
                Me.lblAddressRequired.Visible = False

            Case Else 'WebResource
                Me.tbSource.SelectedTab = Me.oldAddress
        End Select

        'SPONSORING ORG FIELD
        Select Case IsNull(Me.cboType.Text, "")
            Case Is = "Event"
                Me.lblAuxOrgName.Text = "Sponsoring Organization Name"
                Me.txtSponsoringOrg.Visible = True
                '  Case Is = "Person"
                '   Me.lblAuxOrgName.Text = "optional Company Name"
                '   Me.txtSponsoringOrg.Visible = True
            Case Else
                Me.lblAuxOrgName.Text = ""
                Me.txtSponsoringOrg.Visible = False
        End Select
    End Sub

#End Region 'load

#Region "Update Close"

    'SAVE & EXIT (prevent exit if required field missing)
    Private Sub btnSaveExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
      Handles btnSaveExit.Click
        objHowClose = ObjClose.btnSaveExit
        Me.Close()
    End Sub

    'for forms with LastChangeDate
    Public Function LookForChanges() As Boolean
        'need to prevent LastChangeDate from updating with no changes when minimized
        If bDirty = True Then
            LookForChanges = True
        Else
            LookForChanges = modGlobalVar.AnyChanges(ctlNeutral, mainBSrce, mainTbl)
        End If
    End Function

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
        bChanges = LookForChanges()

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
                    ctl = arCtls(0)
                    'INSERT DEFAULT DATA
                    If objHowClose = ObjClose.SaveClose Or e.CloseReason = Windows.Forms.CloseReason.UserClosing Then
                        If ctl Is ctlIdentify Then
                            ctl.Text = usrName & " " & Today.ToShortDateString
                            mainBSrce.EndEdit()
                            Me.MainResourceTableAdapter.Update(mainDS) 'save default data
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
            ClassOpenForms.frmMainResource = Nothing 'reset global var
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
            '  i =
            Me.MainResourceTableAdapter.Update(mainTbl)
            '=====FUTURE EXTRA ADDRESS UPDATE=====
            'f = Me.ResourceMainAddressTableAdapter.Update(Me.DsMainResource1.ResourceMainAddress)
            'Me.ResourceMainAddressTableAdapter.Insert(ThisID, @street1, @street2, @city, @state, @zip,'',  @country, @OrgName, @AddressNote,1)
            ' Me.ResourceMainAddressTableAdapter.Insertc()
            'above would try to add a new address each time?

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

    'CHECK REQUIRED FIELDS w Error Provider
    Private Function CheckRequired() As Control()
        'add the Neutral control to the array last to indicate rest are ok if its the first one
        Dim arCtls(0) As Control
        Dim ctl As Control
        Dim i As Integer = 0

        'ResourceName
        ctl = ctlIdentify
        If Replace(Replace(Replace(ctl.Text, " ", ""), Chr(10), ""), Chr(13), "") = String.Empty Then
            Me.ErrorProvider1.SetError(ctl, "please enter a " & IsNull(ctl.Tag, ctl.Name))
            arCtls(i) = ctl
            i = i + 1
            ReDim Preserve arCtls(arCtls.GetUpperBound(0) + 1)
        Else
            Me.ErrorProvider1.SetError(ctl, "")
        End If

        'Descriptions
        ctl = Me.rtbDescriptions
        If Me.rtbDescriptions.Text > "  " Or Len(mainTbl.Rows(0).Item("PublicDescription").ToString) > 2 Or Len(mainTbl.Rows(0).Item("PrivateDescription").ToString) > 2 Or Len(mainTbl.Rows(0).Item("NewCRGDescription").ToString) > 2 Then
            Me.ErrorProvider1.SetError(ctl, "")
        Else
            Me.ErrorProvider1.SetError(ctl, "Private or Public Description is required")
            arCtls(i) = ctl
            i = i + 1
            ReDim Preserve arCtls(arCtls.GetUpperBound(0) + 1)

        End If

        'Keyword
        ctl = Me.cboKey1
        If Me.cboKey1.SelectedIndex > -1 Then
            Me.ErrorProvider1.SetError(ctl, "")
        Else
            Me.ErrorProvider1.SetError(ctl, "The first Keyword is required.")
            arCtls(i) = ctl
            i = i + 1
            ReDim Preserve arCtls(arCtls.GetUpperBound(0) + 1)

        End If

        'inactive
        ctl = Me.cboWhyInactive
        If Me.chkActive.CheckState = CheckState.Checked Then
            Me.ErrorProvider1.SetError(ctl, "")
        Else

            If Me.cboWhyInactive.SelectedIndex > 0 Then
                Me.ErrorProvider1.SetError(ctl, "")
            Else
                Me.ErrorProvider1.SetError(ctl, "select a reason why the resource is marked Inactive.")
                arCtls(i) = ctl
                i = i + 1
                ReDim Preserve arCtls(arCtls.GetUpperBound(0) + 1)

            End If
        End If

        arCtls(i) = ctlNeutral
        Return arCtls

    End Function

    'SET LASTCHANGED DATE if TAGS or AUTHORS is edited.; 'underlying data'
    Public Sub SetLastChanged()
        MainResourceBindingSource.Current("LastChangeDate") = Now()
        MainResourceBindingSource.Current("LastChangeStaffNum") = usr
    End Sub

#End Region 'update

#Region "Menu Items"

    'ALLOW CLOSE WITHOUT SAVING
    Private Sub miCloseForm_Click(ByVal sender As Object, ByVal e As System.EventArgs) _
        Handles miClose.Click
        objHowClose = ObjClose.miAskSave
        Me.Close()
    End Sub

    'mi SAVE
    Private Sub miSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles miSave.Click
        objHowClose = ObjClose.miSave
        DoUpdate()
        bDirty = False
    End Sub

    'mi CANCEL CHANGES
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles miCancel.Click
        Try
            modPopup.UndoCtl(Me.ActiveControl)
            mainBSrce.CancelEdit()
            mainDS.RejectChanges()
        Catch ex As System.Exception
            modGlobalVar.msg("ERROR: cancelling changes ", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        SetStatusBarText("Changes Cancelled")
    End Sub

    'mi DELETE CURRENT RECORD
    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles miDelete.Click ', btnDelete.Click

        If modGlobalVar.msg("CONFIRM DELETE", mainTopic & ": " + IsNull(ctlIdentify.Text, "") & NextLine & " will be marked for deletion and the window closed.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            ctlIdentify.Text = "DELETE: " & IsNull(ctlIdentify.Text, "")
            Me.chkActive.CheckState = CheckState.Unchecked 'org, resource
            'Me.chkActive.Checked = False
            objHowClose = ObjClose.miDelete
            Me.Close()
        End If

    End Sub

#End Region 'edit buttons

#Region "Validation"

    'CALL REMOVE LINE FEEDS
    Private Sub txtLostFocus(ByVal sender As Object, ByVal e As System.EventArgs) _
       Handles txtResourceName.LostFocus, Email.LostFocus, Address1.LostFocus, txtURL.LostFocus, Website.LostFocus

        If isLoaded Then
            modGlobalVar.RemoveLineFeeds(sender)
        End If

    End Sub

    'VALIDATE DATE
    Private Sub DateValidation(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) _
          Handles dtReview.Validating, DtAccess.Validating
        'e.Cancel = mdGlobalVar.ValidateDateA(sender.DataBindings.Item("Text").BindingMemberInfo.BindingField, mainDS.Tables(0).Rows(0), sender, Me.ErrorProvider1)

        Dim b As Boolean
        '  Dim d As Date
        b = modGlobalVar.ValidateDateA(sender, Me.ErrorProvider1)
        e.Cancel = b

    End Sub

    'FORMAT PHONE
    Private Sub editPhone_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles Telephone.Leave, Telephone2.Leave, Telephone3.Leave
        Dim strCntry As String
        If Me.Country.Text = String.Empty Then
            strCntry = "USA"
        Else
            strCntry = Me.Country.Text
        End If
        If Len(sender.text) > 0 Then
            If modGlobalVar.LeavePhone(sender, strCntry) = True Then
                Me.ErrorProvider1.SetError(sender, "")
            Else
                Me.ErrorProvider1.SetError(sender, "invalid phone number")
            End If
        End If
    End Sub

#End Region 'validation

#Region "Fill Datasets"

    'FILL GRID
    Private Sub FillSecondary()
        SetStatusBarText("Retrieving data")

        cmd.CommandType = CommandType.StoredProcedure
        cmd.Connection = sc

        cmd.Parameters("@IDFld").Value = "ResourceNum"
        cmd.Parameters("@IDVal").Value = ThisID ' Me.ICCResourceID.Text
        daRFA.SelectCommand = cmd

        Select Case tbMain.SelectedTab.Tag
            Case Is = enumRecommend.PluralName   '
                cmd.CommandText = "[GetResRecommendation]"
                ds = DsRFA
                tbl = New DataTable("GetResRecommendation")
            Case Is = enumFeedback.PluralName 'strData2  '
                cmd.CommandText = "[GetResFeedback]"
                ds = DsRFA
                tbl = New DataTable("GetResFeedback")
            Case Is = enumAlert.PluralName 'strData3  '
                cmd.CommandText = "[GetResAlert]"
                ds = DsRFA
                tbl = New DataTable("GetResAlert")

            Case Else
                GoTo closeall
                Exit Sub
        End Select
        ds.Clear()
        Try
            daRFA.Fill(ds, tbl.TableName)
        Catch ex As Exception
            modGlobalVar.msg("ERROR - can't fill " + tbMain.SelectedTab.Tag, ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
CloseAll:
        StatusBar1.Panels(0).Text = "Done"
    End Sub

#End Region

#Region "ADD ITEM"

    'POPUP ADD NEW Case, Contact, Grant
    Private Sub PopupNew(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles miNew.Click, btnNew.Click

        'popup menu asking for usr input
        ' Dim p As PointConverter
        Dim cm As New ContextMenu
        Dim eh As EventHandler = AddressOf AddNew

        cm.MenuItems.Add("SELECT ITEM TO ADD TO: " + ctlIdentify.Text)
        cm.MenuItems.Add("_______________________________________")
        Select Case cboType.SelectedValue
            Case Is = "Book", "Article"
                cm.MenuItems.Add("Author/Editor", eh)
            Case Is = "Media"
                cm.MenuItems.Add("Author/Editor/Presenter", eh)
            Case Is = "Periodical"
                cm.MenuItems.Add("Author/Editor/Contact", eh)
            Case Is = "Event", "Conference"
                cm.MenuItems.Add("Event Info", eh)

            Case Else   'Is = "Organization", "Business"
                cm.MenuItems.Add("Resource Contact", eh)
        End Select
        cm.MenuItems.Add("Recommendation", eh)
        cm.MenuItems.Add("Feedback", eh)
        cm.MenuItems.Add("Alert", eh)
        cm.MenuItems.Add("Location", eh)
        cm.MenuItems(2).DefaultItem = True
        cm.Show(Me, New Point(500, 10))

    End Sub

    'INSERT NEW ITEM to BACKEND and OPEN DETAIL FORM
    ' Protected Sub miAddOrg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) ' _
    ' Handles btnNew.Click
    Private Sub AddNew(ByVal obj As Object, ByVal ea As EventArgs)
        Dim str As String
        Dim iType As Integer 'consultant vw admin
        ' Dim frm As New frmMainResourceAuthor
        'Dim strE As String
        ' strE = UCase(obj.text) & "S"
        ' Select Case UCase(TabControl1.SelectedTab.Tag)
        SetStatusBarText("Adding new " & obj.text.ToString)
        If modGlobalVar.msg("Are you sure?", "About to enter a new " & obj.text.ToString, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then
            Exit Sub
        End If

        If usrType = "Consultant" Then
            iType = usr
        Else
            iType = 0   'could be admin entering from grant app so don't default to their name
        End If

        Select Case obj.text.ToString

            '  str = "INSERT INTO tblResourceTag(ResourceNum, StaffNum, TagName) VALUES (" & Me.ICCResourceID.Text & ", " & usr & InputBox("", "Entering new Index Term") & ");SELECT @@Identity"
            Case Is = enumRecommend.SingleName 'strData1 'recommendation
                str = "INSERT INTO tblResourceRecommend(ResourceNum, RecommendStaffNum, RecommendDate, Used) VALUES (" & ThisID & ", " & iType & ", GETDATE(), 'unknown'); SELECT @@Identity"

            Case Is = enumFeedback.SingleName 'feedback
                str = "INSERT INTO tblResourceFeedback(ResourceNum, FeedbackStaffNum, FeedbackDate) VALUES (" & ThisID & ", " & usr & ", GETDATE()); SELECT @@Identity"

            Case Is = enumAlert.SingleName 'warning
                str = "INSERT INTO tblResourceWarning(ResourceNum, WarningStaffNum, WarningDate) VALUES (" & ThisID & ", " & usr & ", GETDATE()); SELECT @@Identity"

            Case Is = "Author/Editor" 'author
                str = "INSERT INTO tblResourceExtra (ResourceNum, AuthorEditor) VALUES (" & ThisID & ", 'Author'); SELECT @@Identity"

            Case Is = "Author/Editor/Presenter" 'presenter
                str = "INSERT INTO tblResourceExtra (ResourceNum, AuthorEditor) VALUES (" & ThisID & ", 'Presenter'); SELECT @@Identity"

            Case Is = "Author/Editor/Contact" 'periodical
                str = "INSERT INTO tblResourceExtra (ResourceNum, AuthorEditor) VALUES (" & ThisID & ", 'Editor'); SELECT @@Identity"

            Case Is = "Event Info" 'Event
                str = "INSERT INTO tblResourceExtra (ResourceNum, AuthorEditor) VALUES (" & ThisID & ", 'Event'); SELECT @@Identity"

            Case Is = "Resource Contact" 'contact
                str = "INSERT INTO tblResourceExtra (ResourceNum, AuthorEditor) VALUES (" & ThisID & ",'Contact'); SELECT @@Identity"

            Case Is = "Location"
                'TODO Why isn't OrderWhen a date field?'location
                str = "INSERT INTO tblResourceLocation (ResourceNum, SatelliteRegion, OrderWhen, orderstaffnum) VALUES (" & ThisID & ", N'" & usrRegion & "', N'" & Now.ToString & "', " & usr & " ); SELECT @@Identity"

            Case Else
                modGlobalVar.msg("ERROR: not found", obj.text.ToString, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
        End Select

InsertNewItem:
        If Not SCConnect() Then
            Exit Sub
        End If


        Dim myTrans As SqlClient.SqlTransaction = sc.BeginTransaction()
        Dim cmd As New SqlClient.SqlCommand(str, sc, myTrans)
        Dim newID As Integer
        Try
            newID = cmd.ExecuteScalar()
            myTrans.Commit()
        Catch exce As Exception
            modGlobalVar.msg("ERROR: new id", exce.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
            myTrans.Rollback()
            sc.Close()
            Exit Sub
        End Try

        sc.Close()
OpenForm:
        Select Case obj.text.ToString    'UCase(TabControl1.SelectedTab.Tag)
            Case Is = enumRecommend.SingleName 'recommend
                modGlobalVar.OpenMainRecommend(newID, ctlIdentify.Text, 0)
            Case Is = enumFeedback.SingleName 'feedback
                modGlobalVar.OpenMainFeedback(newID, ThisID, ctlIdentify.Text, 0)
                'mdGlobalVar.OpenMainCase(newID, "Entering New Case", Me.editOrgName.Text & " : " & Me.editPhone.Text, Me.txtOrgID.Text)
            Case Is = enumAlert.SingleName 'warning
                modGlobalVar.OpenMainResourceWarning(newID, ThisID, ctlIdentify.Text)
            Case Is = "Author/Editor", "Author/Editor/Contact", "Author/Editor/Presenter", "Resource Contact", "Event Info" 'strData4
                ' modGlobalVar.Msg(Me.ICCResourceID.Text)
                Try
                    frmExtra = New frmMainResourceAuthor
                    frmExtra.iResourceExtraID = newID
                    frmExtra.Text = UCase(obj.text.ToString) & " DETAIL" 'cboType.SelectedItem & " DETAIL"
                    SetExtraVisibility(Me.cboType.SelectedValue)
                    OpenExtraForm()
                    'frmExtra.ShowDialog()
                    'ResetPersonName()
                Catch ex As Exception
                    modGlobalVar.msg("ERROR:", "Can't open new author/contact form." & NextLine & ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            Case Is = "Location"
                modGlobalVar.OpenMainResourceLocation(newID, ThisID, ctlIdentify.Text, usrRegion)
        End Select
        SetStatusBarText("Done")
        'TODO update count on tab

    End Sub

#End Region 'add item

#Region "General"

    'SET STATUS BAR LEFT TEXT
    Private Sub SetStatusBarText(ByVal str As String)
        Me.StatusBarPanel1.Text = str
    End Sub

    'FILTER cboSUBTYPE ON cboTYPE SELECTION; address vs publisher visibility
    Private Sub cboType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles cboType.SelectedIndexChanged

        If sender.selectedindex = -1 Then 'Or isLoaded = False Then
            Exit Sub
        Else
            Me.cboSubtype.SelectedIndex = -1
        End If

resetAddressInsert:
        Me.lblAddressType.Text = "Resource" & cboType.SelectedValue.ToString

SetDatagridTablestyle:
        'Reset defaults
        Me.tsContacts.MappingName = ""
        Me.tsEvents.MappingName = ""
        Me.tsResourceName.MappingName = ""
        Me.tsAuthors.MappingName = ""
        Me.grdExtras.Height = 150
        Me.grdExtras.Visible = True
        Me.txtResourceName.ReadOnly = False
        Me.grdExtras.ReadOnly = True
        Me.lblFlagPerson.Visible = False
        Me.lblLink.Text = "Website"

        'set tablestyle
        Select Case cboType.SelectedValue.ToString

            Case Is = "Article", "Book", "Web Resource", "Periodical"
                Me.tsAuthors.MappingName = "tblResourceExtra"
                Me.grdExtras.CaptionText = "                    Authors/Editors"
                Me.lblLink.Text = "Resource Link"
                'ChildPublish!TabPublishing.Pages!([pgArticle].Visible = True)
                'ChildPublish.Visible = True
                ''  lblChildPublish.Caption = "SOURCE INFORMATION"

                'Case Is = "Book", "Web Resource"
                '    Me.tsAuthors.MappingName = "tblResourceExtra"
                '    Me.grdExtras.CaptionText = "                    Authors/Editors"
                '    'ChildAuthor.Visible = True
                '    'ChildPublish.Visible = True
                '    'ChildPublish!TabPublishing.Pages!([pgBook].Visible = True)
                '    '' lblChildPublish.Caption = "SOURCE INFORMATION"

                'Case Is = "Equipment" the type discarded 2013
                '    Me.grdExtras.Visible = False
                '    'ShowPhone()
                '    'ShowAddress()
                '    '' lblChildPublish.Caption = "ADDRESS INFORMATION"

            Case Is = "Event"
                Me.tsEvents.MappingName = "tblResourceExtra"
                Me.grdExtras.CaptionText = "                    Event Dates/Locations"

            Case Is = "Media"
                Me.tsAuthors.MappingName = "tblResourceExtra"
                Me.grdExtras.CaptionText = "                    Presenters"
                'TODO Match presenter field to author grid fields
                'ChildPublish.Visible = True
                'ChildPublish!TabPublishing.Pages!([pgMedia].Visible = True)
                ''  lblChildPublish.Caption = "SOURCE INFORMATION"

            Case Is = "Organization"
                Me.tsContacts.MappingName = "tblResourceExtra"
                Me.grdExtras.CaptionText = "                    Contacts"

                'ShowPhone()
                'ShowAddress()
                '' lblChildPublish.Caption = "ADDRESS INFORMATION"

            Case Is = "Periodical"
                Me.grdExtras.Visible = False
                'Keyword1.SetFocus()
                'ChildPublish.Visible = True
                'ChildPublish!TabPublishing.Pages!([pgBook].Visible = True)
                ''lblChildPublish.Caption = "SOURCE INFORMATION"

            Case Is = "Person"
                Me.tsResourceName.MappingName = "tblResourceExtra"
                Me.grdExtras.CaptionText = "              Name Grid   -  Edit Resource Name here"
                Me.grdExtras.Height = 90
                Me.txtResourceName.ReadOnly = True
                Me.lblFlagPerson.Visible = True
                If isLoaded Then
                    'INSERT NEW Row in tblResourceExtras for uniform name entry
                    If Me.DsResourceExtra1.tblResourceExtra.Rows.Count > 0 Then
                    Else
                        Dim str As String
                        str = "INSERT INTO tblResourceExtra (ResourceNum, AuthorEditor, Lastname) VALUES (" & ThisID & ",'Resource','" & IsNull(ctlIdentify.Text, "lastname") & "')"
InsertNewItem:
                        If Not SCConnect() Then
                            Exit Sub
                        End If

                        Dim cmd As New SqlClient.SqlCommand(str, sc)
                        Try
                            cmd.ExecuteNonQuery()
                        Catch exce As Exception
                            modGlobalVar.msg("ERROR: new person not inserted", exce.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Finally
                            sc.Close()
                            ' Exit Sub
                        End Try
                        ReloadExtras()
                    End If
                End If
                ' ReloadExtras()


                ' Me.grdExtras.DataMember = "MainResourceExtra"
                ' Me.grdExtras.Refresh()

                'ShowPhone()
                'ShowAddress()
                '' lblChildPublish.Caption = "ADDRESS INFORMATION"
                'Me.ResourceName.Locked = True

            Case Is = "Software"
                '  Me.grdExtras.Visible = False
                'ShowPhone()
                'ShowAddress()
                ''  lblChildPublish.Caption = "ADDRESS INFORMATION"

            Case Is = "Topic" 'retired
                Me.grdExtras.Visible = False
                'Keyword1.SetFocus()
                ''  lblChildPublish.Caption = "ADDRESS INFORMATION"

                'Case Is = "Web Resource"
                '  Me.grdExtras.Visible = True
                'ShowPhone()
                ''  lblChildPublish.Caption = "ADDRESS INFORMATION"
            Case Else
                Exit Sub
        End Select

        Try
            SelectAddressTab()
        Catch ex As System.Exception
            ' modGlobalVar.Msg(ex.Message, , "ERROR:   ")
        End Try

        If isLoaded Then
            ActiveControl = cboSubtype
            '  Me.cboSubtype.DroppedDown = True
        End If

        '  modGlobalVar.Msg(Me.cboType.SelectedItem(1), , Me.cboType.SelectedValue)
        '     Try
        '    Me.cboSubtype.SelectedIndex = -1
        'Catch ex As System.Exception
        '    modGlobalVar.Msg(ex.Message, , "ERROR:   ")
        'End Try
        'Dim OldVal As String = IsNull(Me.cboSubType.SelectedValue, "") '.Text, "")
        'If isLoaded Then 'without this it changes LastChangedDate
        '    cboSubType.Text = ""
        'End If

        'RefreshSubtype:
        ' Me.cboSubType.ResetText()
        '        Try
        '            Me.cboSubType.SelectedItem = Me.cboSubType.FindStringExact(OldVal)
        '        Catch ex As Exception
        '            'modGlobalVar.Msg(ex.Message, , "ERROR: setting subtype ")
        '        End Try

        'Dim dv As New DataView(dtResourceType) 'dsResourceSubtype.Tables("luResourceSubtype"))
        'dv.RowFilter = Nothing
        'dv.RowFilter = "MasterType = '" & cboType.SelectedItem & "'"
        'Dim i As Integer
        'Me.cboSubType.Items.Clear()
        'For i = 0 To dv.Count - 1
        '    Me.cboSubType.Items.Add(dv(i)("TypeName"))
        'Next

    End Sub

    'BIND rtbDescription to appropriate description field
    Private Sub tbDescriptions_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles tbDescriptions.SelectedIndexChanged
        Dim str As String
        MouseWait()
        If isLoaded Then
SetFieldBinding:
            str = Trim(tbDescriptions.SelectedTab.Tag)
            Me.rtbDescriptions.DataBindings.Clear()
            Me.rtbDescriptions.DataBindings.Add(New Binding("Text", Me.MainResourceBindingSource, str))
SetPermission:
            EnforceNewCRGPermission(str)
        End If

        str = Nothing
        MouseDefault()
    End Sub

    'ONLY SELECT FEW CAN EDIT 'NEW CRG ANNOTATION' see tblStaff
    Private Sub EnforceNewCRGPermission(ByVal str As String)
        If StaffCRGFull.Contains(usr) Or StaffCRGEdit.Contains(usr) Then
            Me.rtbDescriptions.ReadOnly = False
        Else
            If str = "NewCRGDescription" Then
                Me.rtbDescriptions.ReadOnly = True
            Else
                Me.rtbDescriptions.ReadOnly = False
            End If
        End If
    End Sub

    'CHECK INACTIVE FLAG
    Private Sub chkActive_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles chkActive.Click

        If chkActive.CheckState = CheckState.Unchecked Then
            If modGlobalVar.msg("CONFIRM: making resource InActive", "You have made this resource Inactive.  If that is what you meant to do, click Yes and select something in the Reason Inactive dropdown box.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                cboWhyInactive.SelectedIndex = cboWhyInactive.FindStringExact("Unavailable or unable to locate")

                Me.cboWhyInactive.Focus()
                cboWhyInactive.DroppedDown = True
            Else
                chkActive.Checked = CheckState.Checked
            End If
        End If

        If chkActive.CheckState = CheckState.Checked Then
            Me.cboWhyInactive.Text = ""
            Me.ErrorProvider1.SetError(Me.cboWhyInactive, "")
            cboWhyInactive.FlatStyle = FlatStyle.Flat
        Else
            cboWhyInactive.FlatStyle = FlatStyle.Standard
        End If

        SetInactiveFlag()

    End Sub

    'MAKE INACTIVE FLAG VISIBLE/INVISIBLE
    Private Sub SetInactiveFlag()
        Me.flagInactive.Visible = Not Me.chkActive.Checked
    End Sub

    'FIND CRG TERM
    Private Sub cboCRG_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) _
            Handles cboKey1.MouseDown, cboKey2.MouseDown, cboKey3.MouseDown, cboKey4.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Right Then
            modPopup.SearchCRG(Me, PointToClient(Control.MousePosition), sender)

        End If
    End Sub

    'GET TAB CAPTION COUNT
    Public Sub SetTabCaptions(ByVal ID As Integer)
SetTabCaptionsWCount:
        Dim cmdCntID As New SqlCommand
        Dim i As Integer = 0

        cmdCntID.Connection = sc
        If Not SCConnect() Then
            Exit Sub
        End If

        'RECOMMENDATIONS
        cmdCntID.CommandText = "SELECT COUNT(RecommendID) FROM tblResourceRecommend WHERE ResourceNum = " & ID
        i = 0
        Try
            i = cmdCntID.ExecuteScalar()
        Catch ex As Exception
            '      modGlobalVar.Msg(ex.Message)
        End Try
        Me.tbMain.TabPages(1).Text = i.ToString() & "  " & Me.tbMain.TabPages(1).Tag

        'FEEDBACK
        cmdCntID.CommandText = "SELECT COUNT(FeedbackID) FROM tblResourceFeedback WHERE ResourceNum = " & ID
        i = 0
        Try
            i = cmdCntID.ExecuteScalar()
        Catch ex As Exception
            '      modGlobalVar.Msg(ex.Message)
        End Try
        Me.tbMain.TabPages(2).Text = "   " & i.ToString() & "  " & Me.tbMain.TabPages(2).Tag & "     "

        'ALERTS
        cmdCntID.CommandText = "SELECT COUNT(WarningID) FROM tblResourceWarning WHERE ResourceNum = " & ID
        Try
            i = cmdCntID.ExecuteScalar()
        Catch ex As Exception
            '      modGlobalVar.Msg(ex.Message)
        End Try
        Me.tbMain.TabPages(3).Text = "   " & i.ToString() & "  " & Me.tbMain.TabPages(3).Tag & "   "
        If i > 0 Then Me.flagAlert.Visible = True

        sc.Close()
        cmdCntID.Dispose()

    End Sub

    'tab control CALL FILL SECONDARY
    Private Sub TabControl1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbMain.SelectedIndexChanged
        If isLoaded Then
            FillSecondary()
        End If
    End Sub

    'HELP BUTTON
    Private Sub btnHelp_Click(ByVal sender As Object, ByVal e As System.EventArgs) _
            Handles btnHelp.Click, miHelpGeneral.Click
        modGlobalVar.msg("HELP", "TO ADD NEW RESOURCE:" & NextLine & NextLine & "Go to Resource Search window and Click New button" + NextLine _
               & NextLine & "TO ADD CONTACT or AUTHOR or LOCATION or RECOMMENDATION etc.:" & NextLine & "Click the New button" + NextLine _
               + "TO EDIT CONTACT OR AUTHOR, double-click the name in the grid" + NextLine _
               + "TO EDIT RESOURCE NAME of a person type resource, use the Author/Contact grid to make changes. They will be reflected in the resource name when you exit the grid." + NextLine _
               & NextLine & "CRG WEBSITE FIELDS are restricted; you may not have authority to make changes.", MessageBoxButtons.OK, MessageBoxIcon.Information)

    End Sub

    'HELP: Resource Definitions
    Private Sub miDefinitions_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles miDefinitions.Click
        modPopup.ShowDefinitions("ResourceType")
    End Sub

    'HELP: CRG WEBSITE
    Private Sub miCRG_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles miCRG.Click
        modGlobalVar.msg("CRG WEBSITE HELP", "Only certain staff can edit CRG Resources.  Contact " & CRGAdmin.StaffName & "." & NextLine & NextLine &
               "The On CRG checkbox is what signals a resource is ready for uploading to the CRG website" & NextLine & NextLine &
               "TO EMBED URL in Description field for the CRG Website: " & NextLine &
               "  • anywhere within the description, type the descriptive label followed by a space then the url; EXAMPLE: " & NextLine & "   Click Here url" & NextLine &
               "  • select the label and url with the mouse, and right click " & NextLine & "  • select 'Enclose in {{ }}'.", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    'UNDO
    Protected Overrides Function ProcessDialogKey(ByVal keyData As Keys) As Boolean
        If keyData = System.Windows.Forms.Keys.Escape Then
            ' If Me.ActiveControl Is Me.cboType Then  'required field
            ' Else
            modPopup.UndoCtl(Me.ActiveControl)
            ' bChanged = True
            'End If
            Return True  ' True means we've processed the key
        Else
            Return MyBase.ProcessDialogKey(keyData)
        End If
    End Function

    'COPY CURRENT ID
    Private Sub StatusBar1_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StatusBar1.DoubleClick
        Clipboard.SetText(ThisID)
    End Sub

    'REVIEW BUTTON
    Private Sub btnReviewed_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReviewed.Click
        '    Dim d As New ClassDialog("Finished Review for now.", "Yes, the information is current and up-do-date.", "No, the information could not be confirmed.", "If you attempted but have not been able to get current information for this resource, click the No button." & NextLine & "Your name and today's date will be placed in the Resource reviewed fields.", False)

        'set underlying datasource or save won't trigger
        mainBSrce.Current("ReviewDate") = Today.ToShortDateString
        mainBSrce.Current("ReviewStaffNum") = usr
        Me.dtReview.Text = Today.ToShortDateString
        Me.txtReviewNote.Text = " Verified " & Today.ToShortDateString
        Me.cboReview.Focus()

    End Sub

    'DISPLAY STAFF NAME FROM ID
    Private Sub TextBox_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) _
             Handles fldReviewStaff.MouseHover, fldCreatedBy.MouseHover, fldChangeBy.MouseHover

        Me.ToolTip1.SetToolTip(sender, sender.tag & ": " & modPopup.ShowStaff(sender.text))

    End Sub

    'VALIDATE PUBLISHER
    Private Sub cboPublisher_validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) _
            Handles cboPublisher.Validating

        Dim strName As String = "Publisher Name"
        Dim strCity As String = "City"
        Dim strState As String = "ST"

        If Not SCConnect() Then
            Exit Sub
        End If

        Select Case modGlobalVar.ValidateCBO(sender, "Publisher", False, CanAddNew.AddNew)
            Case Is = usrInput.Yes
                strName = InputBox("", "Enter Publisher's Name", Me.cboPublisher.Text)
                strCity = InputBox("", "Enter publisher's City", strCity)
                strState = IsNull(InputBox("only 2 letters will be accepted", "Enter publisher's State abbreviation", strState), "--")
                Dim cmd As New SqlCommand("INSERT INTO tblPublisher (PublisherName, City, State) VALUES (N'" & strName.Replace("'", "''") & "', N'" & strCity & "', N'" & IsNull(strState.Substring(0, 2), "") & "')", sc)
                Try
                    cmd.ExecuteNonQuery()
                Catch ex As Exception
                    modGlobalVar.msg("ERROR: insert publisher failed", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    GoTo CloseOut
                End Try
                cmd = Nothing
                LoadPubCombo()
                Me.cboPublisher.Refresh()
                Me.cboPublisher.SelectedIndex = cboPublisher.FindString(strName)
            Case Is = usrInput.OK
                ''TODO look up city and state from existing publisher
                ' modGlobalVar.Msg(Me.cboPublisher.Text, , "Publisher")

                Dim cmd As New SqlCommand("SELECT City FROM tblPublisher WHERE PublisherName = '" & Me.cboPublisher.Text.Replace("'", "''") & "'", sc)
                strCity = IsNull(cmd.ExecuteScalar, "")
                Dim cmd2 As New SqlCommand("SELECT State FROM tblPublisher WHERE PublisherName = '" & Me.cboPublisher.Text.Replace("'", "''") & "'", sc)
                strState = IsNull(cmd2.ExecuteScalar, "")
                cmd = Nothing
                cmd2 = Nothing
            Case Is = usrInput.Retry
                '  Dim frm As New frmAddNew
                '  frm.ShowDialog()
                Me.cboPublisher.Focus() 'can leave blank
                sc.Close()
                Exit Sub
        End Select
CloseOut:
        sc.Close()
        Me.txtPubCity.Text = strCity
        Me.txtPubState.Text = strState
        Me.txtPubDate.Focus()
    End Sub

    'TAB VISIBILITY on Resource Extra Detail form
    Private Sub SetExtraVisibility(ByVal what As String)
        'for resources with multiple contacts/authors/editors/ or event occurrences, or resource type Person to set name
        frmExtra.lblPerson.Visible = False
        frmExtra.pgEvent.Visible = False
        Select Case what
            Case Is = "Organization", "Business"
                frmExtra.pnlName.Visible = True
                frmExtra.pnlPhone.Visible = True
            Case Is = "Person"
                frmExtra.pnlName.Visible = True
                frmExtra.pnlPhone.Visible = True
                frmExtra.lblPerson.Visible = True
            Case Is = "Book", "Article", "Media", "Periodical"
                frmExtra.pnlName.Visible = True
            Case Is = "Event"
                frmExtra.pgEvent.Visible = True
                Try
                    frmExtra.pgEvent.Select()
                Catch ex As Exception
                    modGlobalVar.msg("ERROR: can't select page", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            Case Is = "Web Resource"
                frmExtra.pnlName.Visible = True
                frmExtra.pnlPhone.Visible = True
            Case Else 'show all tabs
                SetStatusBarText("resource type not found")
        End Select
    End Sub

    'CONFIRM PERMISION to APPROVE RESOURCE
    Private Sub chkApproved_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkApproved.Click
        If usr = ResourceReviewer.StaffID Or usr = ResourceDir.StaffID Or usr = DBAdmin.StaffID Then  'ClassStaff.StaffNum.ResourceAdmin Or usr = ClassStaff.StaffNum.ResourceReviewer Then
        Else
            modGlobalVar.msg("Unauthorized edit", "Only " & ResourceDir.StaffName & " or " & ResourceReviewer.StaffName & " may approve resources", MessageBoxButtons.OK, MessageBoxIcon.Information)
            chkApproved.Checked = Not chkApproved.Checked
        End If
    End Sub

    'REMOVE LINE FEEDS - referral source used in xml and other reports
    Private Sub txtReferralSource_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtReferralSource.Leave
        modGlobalVar.ReplaceLineFeeds(txtReferralSource)
    End Sub

    'URL MOUSE SHAPE 
    Private Sub txtURL_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles txtURL.MouseHover ', txtWebsite.MouseHover
        MouseWeb()
    End Sub

    'CANCEL URL MOUSE SHAPE
    Private Sub txturl_mouseleave(ByVal sender As System.Object, ByVal e As System.EventArgs) _
          Handles txtURL.MouseLeave ', txtWebsite.MouseLeave
        MouseDefault()
    End Sub

    'CRG Checkbox clears CRG combo
    Private Sub chkCRGWebsite_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkCRGWebsite.CheckedChanged
        If isLoaded Then
        Else
            Exit Sub
        End If

        Dim cb As CheckBox = DirectCast(sender, CheckBox)
        If cb.Checked = True Then
            If modGlobalVar.msg("Please Confirm", "This resource is ready to go ONLINE.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then
                RemoveHandler cb.CheckedChanged, AddressOf chkCRGWebsite_CheckedChanged
                cb.Checked = False 'Not cb.Checked
                AddHandler cb.CheckedChanged, AddressOf chkCRGWebsite_CheckedChanged
            End If
        End If

        If sender.checked = True Then
            Me.cboCRGStatus.SelectedIndex = 0
            Me.cboCRGStatus.FlatStyle = FlatStyle.Flat
        Else
            Me.cboCRGStatus.FlatStyle = FlatStyle.Standard
        End If

    End Sub

    'CRB Combo clears CRGCheckbox
    Private Sub cboCRGStatus_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) _
        Handles cboCRGStatus.SelectedIndexChanged
        If isLoaded Then
        Else
            Exit Sub
        End If
        If sender.text > "" Then
            Me.chkCRGWebsite.Checked = False
        End If
    End Sub

    'INACTIVE MESSAGE
    Private Sub cboWhyInactive_MouseHover(sender As System.Object, e As System.EventArgs) Handles cboWhyInactive.MouseHover
        Me.ToolTip1.SetToolTip(sender, sender.text)
    End Sub

#End Region     'general

#Region "TEXTBOX EDIT"

    'CALL RIGHT CLICK EDIT MENU
    Private Sub rtbDescriptions_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) _
        Handles rtbDescriptions.MouseDown ', txtResourceName.MouseDown

        If e.Button = Windows.Forms.MouseButtons.Right Then
            Dim pp As New ClassRTBContextMenu(sender)
            pp.Show(Me, PointToClient(Control.MousePosition))
        Else
        End If
    End Sub

#End Region 'view full text

#Region "AuthorGrid"

    'reset name of Person type resource based on first row in grid
    Public Sub ResetPersonName() 'ByVal sender As Object, ByVal e As System.EventArgs)
        Dim tbl As DataTable = Me.DsResourceExtra1.tblResourceExtra
        Dim str As String   ' to concatenate name parts
        Dim m As String
        Dim S As String
        Dim p As String

        If cboType.SelectedValue = "Person" And Me.DsResourceExtra1.tblResourceExtra.Rows.Count > 0 Then
            p = ""
            S = ""
            m = ""
            If IsNull(Me.grdExtras.Item(0, tbl.Columns("Prefix").Ordinal), "") > " " Then
                p = " (" & IsNull(Me.grdExtras.Item(0, tbl.Columns("Prefix").Ordinal), "") & ")"
            End If
            If IsNull(Me.grdExtras.Item(0, tbl.Columns("Suffix").Ordinal), "") > " " Then
                S = " ," & IsNull(Me.grdExtras.Item(0, tbl.Columns("Suffix").Ordinal), "")
            End If
            If IsNull(Me.grdExtras.Item(0, tbl.Columns("Middle").Ordinal), "") > " " Then
                m = " " & IsNull(Me.grdExtras.Item(0, tbl.Columns("Middle").Ordinal), "")
            End If
            str = Me.grdExtras.Item(0, tbl.Columns("LastName").Ordinal) & ", " & Me.grdExtras.Item(0, tbl.Columns("FirstName").Ordinal) & m & p & S
            mainTbl.Rows(0)("ResourceName") = IsNull(str, "add a Contact") 'triggers save
            Me.txtResourceName.Text = IsNull(str, "add a Contact") 'visible to user
        Else
            'do nothing; there is no data to work with
            'modGlobalVar.Msg(cboType.SelectedValue, , cboType.Text)
        End If
    End Sub

#End Region

#Region "OpenDetailForms"

    'OPEN LINK
    Private Sub txtURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles txtURL.Click ', txtWebsite.Click
        modPopup.OpenWebsite(sender.text)
    End Sub

    'open website from descriptiop field
    Private Sub rtbDescriptions_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkClickedEventArgs) _
    Handles rtbDescriptions.LinkClicked
        modPopup.OpenWebsite(e.LinkText)
    End Sub

    'open publisher detail
    Private Sub lblGotoPublisher_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblGotoPublisher.DoubleClick

        Dim frm As New frmAddNew
        modGlobalVar.HideTabPage("tbPublisher", frm.TabControl1)
        frm.LoadPublishers()

        If Me.cboPublisher.Text > "" Then
            Try
                frm.BindingSourcePublisher.Position = frm.BindingSourcePublisher.Find("PublisherName", Me.cboPublisher.Text)
            Catch ex As Exception
                modGlobalVar.msg("ERROR: ", "can't navigate to " & Me.cboPublisher.Text & "." & NextLine & ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try

        End If
        frm.ShowDialog()
        LoadPubCombo()

    End Sub

    'TODO check combobox behaviour re type-in; lower ones work web-wise, top one works properly
    Private Sub OpenForms(ByVal obj As String)

        MouseWait()
        SetStatusBarText("opening " & Me.tbMain.SelectedTab.Tag)
        If obj = "grdLocation" Then
            Try
                modGlobalVar.OpenMainResourceLocation(Me.grdLocation.Item(Me.grdLocation.CurrentRowIndex, 0), ThisID, ctlIdentify.Text, Me.grdLocation.Item(Me.grdLocation.CurrentRowIndex, 1))
            Catch ex As Exception
            End Try
        Else

            Select Case Me.tbMain.SelectedTab.Tag
                Case Is = enumRecommend.PluralName '"Recommendation"
                    modGlobalVar.OpenMainRecommend(Me.grdRecommend.Item(Me.grdRecommend.CurrentRowIndex, 0), ctlIdentify.Text, IsNull(Me.grdRecommend.Item(Me.grdRecommend.CurrentRowIndex, 2), 0))
                Case Is = enumFeedback.PluralName '"Feedback"
                    Dim i As Integer = IsNull(Me.grdFeedback.Item(Me.grdFeedback.CurrentRowIndex, Me.grdFeedback.TableStyles(0).GridColumnStyles.IndexOf(Me.grdFeedback.TableStyles(0).GridColumnStyles("OrgNum"))), 0)
                    modGlobalVar.OpenMainFeedback(Me.grdFeedback.Item(Me.grdFeedback.CurrentRowIndex, 0), ThisID, ctlIdentify.Text, i)
                Case Is = enumAlert.PluralName '"Alert"
                    modGlobalVar.OpenMainResourceWarning(Me.grdAlert.Item(Me.grdAlert.CurrentRowIndex, 0), ThisID, ctlIdentify.Text)
                Case Is = "RESOURCE"
                    frmExtra = New frmMainResourceAuthor
                    frmExtra.iResourceExtraID = Me.grdExtras.Item(Me.grdExtras.CurrentRowIndex, 0)
                    frmExtra.Text = cboType.SelectedValue & " DETAIL"
                    SetExtraVisibility(Me.cboType.SelectedValue)
                    OpenExtraForm()
            End Select
        End If

        SetStatusBarText("Done")
        MouseDefault()
    End Sub

    'OPEN EXTRAS on close reload author grid, reform resource name if person
    Private Sub OpenExtraForm()

        frmExtra.ShowDialog()

        'REFRESH GRID - call from Extra form only if changes
        '  ReloadExtras()

        'CHECK RESOURCE NAME
        'ResetPersonName()

    End Sub

    'OPEN FORMS from GRID DBL CLICK
    Private Sub grd_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) _
            Handles grdRecommend.DoubleClick, grdExtras.DoubleClick, grdFeedback.DoubleClick, grdLocation.DoubleClick, grdAlert.DoubleClick

        If sender.CurrentRowIndex >= 0 Then
            If sender.Item(sender.CurrentRowIndex, 0).ToString > "" Then
                OpenForms(sender.name)
            Else 'data issue
            End If
        Else
            modGlobalVar.msg("INSUFFICIENT DATA", "Select a row in the grid, or " & NextLine & "to enter new " + sender.name.ToString.Substring(3, Len(sender.name.ToString) - 3) + ": click the New button", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
    End Sub

    'OPEN MAIN ORG
    Private Sub btnOrg_Click(ByVal sender As Object, ByVal e As System.EventArgs) _
            Handles btnOrg.Click
        modGlobalVar.OpenMainOrg(Me.OrgNum.Text, ctlIdentify.Text + ", " + Me.Telephone.Text)
    End Sub

    'CLEAR SELECTION FROM DATAGRIDS AND LOSE FOCUS (note: selection stays with arrow)
    Private Sub grdUnselect_Click(ByVal sender As Object, ByVal e As System.EventArgs) _
            Handles MyBase.Click, grdRecommend.LostFocus, grdExtras.LostFocus, grdFeedback.LostFocus, grdLocation.LostFocus

        If sender.GetType.ToString = "Windows.Forms.DataGrid" Then
            If sender.CurrentRowIndex > -1 Then
                sender.UnSelect(sender.CurrentRowIndex)
                sender.NavigateBack()
            End If
        Else
        End If
    End Sub

#End Region     'open forms

#Region "Print Reports"
    '------------------------------------------------------------
    'SPECIALTY REPORTS IN WORD FORMATTED BY NANCY DEMOTT SPECS
    '-------------------------------------------------------------
    'PUBLIC RESOURCE REPORT
    Private Sub miPrintPublic_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles miPrintPublic.Click
        MouseWait()
        Dim strb As New StringBuilder
        strb.Append("SELECT * From vwRptResourcePublic WHERE ICCResourceID =  " & ThisID)
        modPopup.PrintResourcePublic(strb.ToString, "for selected resource") ', IsNull(ctlIdentify.Text, ""))
        MouseDefault()
    End Sub

    'DESCRIPTION REPORT
    Private Sub miPrintDescription_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles miPrintDescription.Click
        MouseWait()
        Dim strb As New StringBuilder
        strb.Append("SELECT   *  FROM vwRptResourceDescription  WHERE ICCResourceID  =   " & ThisID)
        modPopup.PrintResourceDescriptions(strb.ToString, "for selected resource") 'IsNull(ctlIdentify.Text, ""))
        MouseDefault()
    End Sub

    'ALL FIELDS REPORT
    Private Sub miPrintAllFields_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles miPrintAllFields.Click
        MouseWait()
        modPopup.PrintResourceFull(ThisID, Nothing)
        MouseDefault()
    End Sub

    'FEEDBACK REPORT
    Private Sub miRptFeedback_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles miRptFeedback.Click

        Dim sp As New SqlClient.SqlCommand("[RptsResources]", sc)
        sp.CommandType = CommandType.StoredProcedure
        sp.Parameters.Add("@Num", SqlDbType.VarChar).Value = ThisID 'Me.ICCResourceID.Text
        sp.Parameters.Add("Which", SqlDbType.VarChar).Value = "FeedbackIndividual"
        modPopup.PrintResourceReport(sp, "Resource Feedback", "Selected Resources", True, "ResourceName", "Feedback Report")
        MouseDefault()
    End Sub

    'RECOMMEND/FEEDBACK/ALERT REPORT
    Private Sub miRptRFA_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        MouseWait()
        modPopup.OpenReport(ThisID, "ResourceFullRFA")
        MouseDefault()
    End Sub

#End Region

#Region "Attach Files"
    '=== NOTES ==='
    '2 buttons on form, one for files, one for iamges; each has 2 menu items: Attach and Open
    '----------------------------------------------------------------------------------------

    'COPY FILE to shared drive
    Private Sub miAttach_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles miAttach.Click
        MouseWait()
AttachFile:
        Try
            modPopup.AttachFiles("Resource", DocPath, ThisID)
        Catch ex As Exception
            modGlobalVar.msg("ERROR: attach resource files", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
ReloadList:
        modPopup.FindFiles(ThisID, DocPath, ppFile, ehFile, Me.miOpenFile, Me.btnOpenFile, My.Resources.btnAttached, Me.ToolTip1)

        SetStatusBarText("Done")
        MouseDefault()

    End Sub

    'ATTACH, OR POPUP OPEN FILE
    Private Sub btnOpenFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnOpenFile.Click
        If sender.Tag.ToString = "Attach" Then
            Me.miAttach.PerformClick()
        Else
            ppFile.Show(Me, New Point(600, 10))
        End If
    End Sub

    'POPUP MENU HANDLER
    Private Sub ehOpenFile(ByVal obj As Object, ByVal ea As EventArgs)
        MouseWait()
        If obj.text = "Attach File" Then
            Me.miAttach.PerformClick()
        Else
            If OpenFile(DocPath & ThisID.ToString & " " & obj.text) = True Then
                SetStatusBarText("file opened")
            Else
                SetStatusBarText("network error")
            End If
        End If
        Exit Sub
        MouseDefault()

    End Sub

    'ATTACH IMAGE OR OPEN POPUP
    Private Sub btnImage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
      Handles btnImage.Click
        MouseWait()
        If sender.Tag.ToString = "Attach" Then
AttachFile:
            Try
                modPopup.AttachFiles("ResourceImage", ImagePath, ThisID)
            Catch ex As Exception
                modGlobalVar.msg("ERROR: attach image files", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
ReloadList:
            FindImages()
            'modPopup.FindFiles(ThisID, strImagePath, ppImage, ehFile, Nothing, Me.btnImage, Me.btnImage.Image, Me.ToolTip1) ', Nothing)
        Else
            ppImage.Show(Me, New Point(600, 10))
        End If

        SetStatusBarText("Done")
        MouseDefault()

    End Sub

    'IMAGE POPUP HANDLER
    Private Sub ehOpenImage(ByVal obj As Object, ByVal ea As EventArgs)
        MouseWait()
        If obj.text = "Attach File" Then
            Try
                modPopup.AttachFiles("ResourceImage", ImagePath, ThisID)
            Catch ex As Exception
                modGlobalVar.msg("ERROR: attach image files 2", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
ReloadList:
            FindImages()
            'modPopup.FindFiles(ThisID, strImagePath, ppImage, ehImage, Nothing, Me.btnImage, Me.btnImage.Image, Me.ToolTip1)

        Else
            If OpenFile(ImagePath & ThisID.ToString & " " & obj.text) = True Then
                SetStatusBarText("file opened")
            End If
        End If
        MouseDefault()

    End Sub

#End Region 'attach files

#Region "IndexTerms"

    '    'REFRESH INDEX LIST
    Private Sub RefreshIndexTerms()
        Dim str As String = Me.fldIndexCSV.Text
        Dim sql As New SqlCommand("SELECT dbo.fDynamicString('ResourceIndexTerms', DEFAULT, " & ThisID & ") AS IndexTerms", sc)
        If Not SCConnect() Then
            Exit Sub
        End If
        Try
            Me.fldIndexCSV.Text = sql.ExecuteScalar.ToString
        Catch ex As System.Exception
            modGlobalVar.msg("ERROR: term reload  ", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        sc.Close()
SetLastChangeDate:
        If isLoaded Then
            If fldIndexCSV.Text = str Then
            Else
                SetLastChanged()
            End If
        End If
        Me.fldIndexDE.Text = "Enter text here"
    End Sub

    'SIMPLE ADD USER INPUT
    Private Sub btnAddIndex_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnIndexAdd.Click

        Dim i As Integer
        'IS DATA
        If String.IsNullOrEmpty(Me.fldIndexDE.Text) Or Me.fldIndexDE.Text = "Enter text here" Then
            Exit Sub
        End If

        'CallInsertIndex
        i = CreateIndexterm(Me.fldIndexDE.Text)
        If i = 0 Then
            modGlobalVar.msg("ERROR: getting index id", Me.fldIndexDE.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If
        'call Associate Index with Resource
        AssignResourceIndex(i)

        'CALL REFRESH INDEX TEXTBOX
        RefreshIndexTerms()

    End Sub

    'INSERT or DELETE TERM in RESOURCE INDEX tbl
    Private Sub AssignResourceIndex(ByVal i As Integer)
        Dim sql As SqlCommand
        sql = New SqlCommand("INSERT INTO tblResourceIndexTerm (ResourceNum, IndexTermNum) VALUES(" & ThisID & ", " & i & ")", sc)
        If Not SCConnect() Then
            Exit Sub
        End If
        Try
            'INSERT
            sql.ExecuteScalar()
        Catch exD As System.Data.SqlClient.SqlException
            'DELETE
            If modGlobalVar.msg("CONFIRM - DELETE index term?", UCase(Me.fldIndexDE.Text) & " is already listed with this resource. " & NextLine _
                      & "If you click Yes, this index will be REMOVED." & NextLine & Me.fldIndexDE.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                sql = New SqlCommand("DELETE FROM tblResourceIndexTerm WHERE (ResourceNum = " & ThisID & ") AND (IndexTermNum = " & i & ")", sc)
                sql.ExecuteScalar()
            End If
        Catch ex As System.Exception
            modGlobalVar.msg("ERROR: processing index ", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally

            sc.Close()
        End Try
    End Sub

    'SELECT ALL TEXT when enter fld
    Private Sub fldIndexDE_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles fldIndexDE.Enter
        sender.selectall()
    End Sub

    'SELECT ALL TEXT from mouse click
    Private Sub fldIndexDE_MouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles fldIndexDE.MouseClick
        sender.selectall()
    End Sub

#End Region 'index


End Class

