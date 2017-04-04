Option Explicit On
Imports System
Imports System.Data.SqlClient
Imports System.Text
Imports System.IO


Public Class frmMainCase
    Inherits System.Windows.Forms.Form

    Public isLoaded As Boolean = False  'used for combo boxes during maximize
    Dim tbl As DataTable 'flexible datagrid
    Dim hti As System.Windows.Forms.DataGrid.HitTestInfo
    Dim dv, dvM, dvS1, dvS2 As DataView   'filter for each datagrid
    Dim statusM, statusS1, statusS2 As String 'filter text for status bar
    Dim ppFile As New ContextMenu   'File goto
    Dim ehFile As EventHandler = AddressOf ehOpenFile
    Dim ppMGI As New ContextMenu    'MGI goto
    Dim ehMGI As EventHandler = AddressOf ehGotoMGI
    Dim iPrevStatus, iPrevCaseMgr, iPrevCRG, iPrevStaff As Integer 'old value of dd
    Const LinkCasePath As String = LinkedPath & "Cases\"
    '  Dim bdDate, bdDate2, bdDate3, bdDate4 As Binding 'for deleting dates
    Dim enumConvers, enumGrant, enumRecommend As structHeadings
    '===== for Detail form  closing routines
    Dim ctlIdentify As Control 'fields for delete and messages
    Dim ctlNeutral As Control 'will never be invalidated
    Dim objHowClose As Short ' structCloseMethod 'Object = Me.btnSaveExit 'identify object calling close
    Dim mainDS As DataSet 'for generic module calls like CloseDetailForm
    Dim mainTopic As String 'name of entity of this form - case, contact, etc
    Dim mainBSrce As System.Windows.Forms.BindingSource
    Dim mainTbl As DataTable
    Dim mainDAdapt As SqlDataAdapter
    Dim bDirty As Boolean = False 'crg combobox 
    Public ThisID, LocalOrgID As Integer
    Dim strbTest As New StringBuilder
    Dim tblConsultant As DataTable


#Region "INITIALIZE"
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

#End Region 'Initialize

#Region " Windows Form Designer generated code "

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.

    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents chkFromCRG As System.Windows.Forms.CheckBox
    Friend WithEvents cboCRGConsultant As InfoCtr.ComboBoxRelaxed
    Friend WithEvents Label4 As System.Windows.Forms.Label

    Friend WithEvents StatusBar1 As System.Windows.Forms.StatusBar
    Friend WithEvents StatusBarPanelID As System.Windows.Forms.StatusBarPanel
    Protected Friend WithEvents lblSelectedID As System.Windows.Forms.Label
    Protected Friend WithEvents lblSelectedWhat As System.Windows.Forms.Label
    ' Friend WithEvents fldCaseID As System.Windows.Forms.TextBox
    Friend WithEvents MainCaseBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents pgGrant As System.Windows.Forms.TabPage
    Friend WithEvents tsGrant As System.Windows.Forms.DataGridTableStyle
    Friend WithEvents tsConversation As System.Windows.Forms.DataGridTableStyle
    Friend WithEvents tsRecommend As System.Windows.Forms.DataGridTableStyle
    Friend WithEvents DataGridTextBoxColumn18 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn19 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn20 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn21 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn22 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn23 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents MenuItem4 As System.Windows.Forms.MenuItem
    Friend WithEvents miRequestFeedback As System.Windows.Forms.MenuItem
    Friend WithEvents btnFeedback As System.Windows.Forms.Button
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents DtFeedbackRecd As InfoCtr.DateTextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents DtFeedbackRequested As InfoCtr.DateTextBox
    Friend WithEvents btnHelp As System.Windows.Forms.Button
    Friend WithEvents DataGridTextBoxColumn24 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents btnSaveExit As System.Windows.Forms.Button
    Friend WithEvents fldOrgNum As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents miPrintConversation As System.Windows.Forms.MenuItem
    Friend WithEvents btnViewFile As System.Windows.Forms.Button
    Friend WithEvents DataGridTextBoxColumn25 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents miOpenFile As System.Windows.Forms.MenuItem
    Friend WithEvents miAttach As System.Windows.Forms.MenuItem
    Friend WithEvents ErrorProvider1 As System.Windows.Forms.ErrorProvider
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents btnMGI As System.Windows.Forms.Button
    Friend WithEvents miHelp As System.Windows.Forms.MenuItem
    Friend WithEvents miDefinitions As System.Windows.Forms.MenuItem
    Friend WithEvents miGotoOrg As System.Windows.Forms.MenuItem
    Friend WithEvents SqlConnection1 As System.Data.SqlClient.SqlConnection
    Friend WithEvents DsMainCase1 As InfoCtr.dsMainCase
    Friend WithEvents MainMenu1 As System.Windows.Forms.MainMenu
    Friend WithEvents MenuItem1 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem2 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem7 As System.Windows.Forms.MenuItem
    Friend WithEvents SqlSelectCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlUpdateCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents grdMain As System.Windows.Forms.DataGrid
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents DtCaseClose As System.Windows.Forms.TextBox
    Friend WithEvents DtCaseOpen As InfoCtr.DateTextBox
    Friend WithEvents cboMgr As InfoCtr.ComboBoxRelaxed
    Friend WithEvents cboStatus As InfoCtr.ComboBoxRelaxed
    Friend WithEvents cboCRG As InfoCtr.ComboBoxRelaxed 'System.Windows.Forms.ComboBox '
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents txtDescription As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents pgConversation As System.Windows.Forms.TabPage
    Friend WithEvents pgRecommendation As System.Windows.Forms.TabPage
    Friend WithEvents DataGridTextBoxColumn1 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn2 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn3 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn4 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn5 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn6 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn7 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents StatusBarPanel1 As System.Windows.Forms.StatusBarPanel
    Friend WithEvents StatusBarPanel2 As System.Windows.Forms.StatusBarPanel
    Friend WithEvents DataGridTextBoxColumn8 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn9 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn10 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn11 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn12 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn13 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn14 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn15 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn16 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn17 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents daspMainCase As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents miCancel As System.Windows.Forms.MenuItem
    Friend WithEvents miDelete As System.Windows.Forms.MenuItem
    Friend WithEvents miClose As System.Windows.Forms.MenuItem
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents TxtCaseName As System.Windows.Forms.TextBox
    Friend WithEvents mmGoTo As System.Windows.Forms.MenuItem
    Friend WithEvents miGotoConversation As System.Windows.Forms.MenuItem
    Friend WithEvents miGotoGrant As System.Windows.Forms.MenuItem
    Friend WithEvents miGotoRecommendation As System.Windows.Forms.MenuItem
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents fldGotoOrg As System.Windows.Forms.TextBox
    Friend WithEvents lblCurrentCaseRating As System.Windows.Forms.Label
    Friend WithEvents miUndo As System.Windows.Forms.MenuItem
    Friend WithEvents ContextMenu1 As System.Windows.Forms.ContextMenu
    Friend WithEvents txtSelection As System.Windows.Forms.TextBox
    Friend WithEvents miSave As System.Windows.Forms.MenuItem
    Friend WithEvents btnNew As System.Windows.Forms.Button
    Friend WithEvents mmHelp As System.Windows.Forms.MenuItem
    Friend WithEvents miNew As System.Windows.Forms.MenuItem
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents chkResourceReqd As System.Windows.Forms.CheckBox

    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMainCase))
        Me.daspMainCase = New System.Data.SqlClient.SqlDataAdapter()
        Me.SqlSelectCommand1 = New System.Data.SqlClient.SqlCommand()
        Me.SqlConnection1 = New System.Data.SqlClient.SqlConnection()
        Me.SqlUpdateCommand1 = New System.Data.SqlClient.SqlCommand()
        Me.MainMenu1 = New System.Windows.Forms.MainMenu(Me.components)
        Me.MenuItem1 = New System.Windows.Forms.MenuItem()
        Me.miNew = New System.Windows.Forms.MenuItem()
        Me.miAttach = New System.Windows.Forms.MenuItem()
        Me.miOpenFile = New System.Windows.Forms.MenuItem()
        Me.miClose = New System.Windows.Forms.MenuItem()
        Me.MenuItem2 = New System.Windows.Forms.MenuItem()
        Me.miSave = New System.Windows.Forms.MenuItem()
        Me.miCancel = New System.Windows.Forms.MenuItem()
        Me.miDelete = New System.Windows.Forms.MenuItem()
        Me.MenuItem4 = New System.Windows.Forms.MenuItem()
        Me.miRequestFeedback = New System.Windows.Forms.MenuItem()
        Me.MenuItem7 = New System.Windows.Forms.MenuItem()
        Me.miPrintConversation = New System.Windows.Forms.MenuItem()
        Me.mmGoTo = New System.Windows.Forms.MenuItem()
        Me.miGotoConversation = New System.Windows.Forms.MenuItem()
        Me.miGotoRecommendation = New System.Windows.Forms.MenuItem()
        Me.miGotoGrant = New System.Windows.Forms.MenuItem()
        Me.miGotoOrg = New System.Windows.Forms.MenuItem()
        Me.mmHelp = New System.Windows.Forms.MenuItem()
        Me.miHelp = New System.Windows.Forms.MenuItem()
        Me.miDefinitions = New System.Windows.Forms.MenuItem()
        Me.miUndo = New System.Windows.Forms.MenuItem()
        Me.grdMain = New System.Windows.Forms.DataGrid()
        Me.tsConversation = New System.Windows.Forms.DataGridTableStyle()
        Me.DataGridTextBoxColumn1 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn2 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn3 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn4 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn5 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn6 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn7 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.tsGrant = New System.Windows.Forms.DataGridTableStyle()
        Me.DataGridTextBoxColumn18 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn23 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn19 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn22 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn20 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn21 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.tsRecommend = New System.Windows.Forms.DataGridTableStyle()
        Me.DataGridTextBoxColumn8 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn9 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn10 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn11 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn12 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn13 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn14 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn15 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn16 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn17 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn25 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn24 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.chkFromCRG = New System.Windows.Forms.CheckBox()
        Me.MainCaseBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.DsMainCase1 = New InfoCtr.dsMainCase()
        Me.cboCRGConsultant = New InfoCtr.ComboBoxRelaxed()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.DtFeedbackRecd = New InfoCtr.DateTextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.DtFeedbackRequested = New InfoCtr.DateTextBox()
        Me.TxtCaseName = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.lblCurrentCaseRating = New System.Windows.Forms.Label()
        Me.chkResourceReqd = New System.Windows.Forms.CheckBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.pgConversation = New System.Windows.Forms.TabPage()
        Me.pgGrant = New System.Windows.Forms.TabPage()
        Me.pgRecommendation = New System.Windows.Forms.TabPage()
        Me.DtCaseClose = New System.Windows.Forms.TextBox()
        Me.DtCaseOpen = New InfoCtr.DateTextBox()
        Me.cboMgr = New InfoCtr.ComboBoxRelaxed()
        Me.cboStatus = New InfoCtr.ComboBoxRelaxed()
        Me.cboCRG = New InfoCtr.ComboBoxRelaxed()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.txtDescription = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.fldGotoOrg = New System.Windows.Forms.TextBox()
        Me.btnSaveExit = New System.Windows.Forms.Button()
        Me.btnFeedback = New System.Windows.Forms.Button()
        Me.btnNew = New System.Windows.Forms.Button()
        Me.btnViewFile = New System.Windows.Forms.Button()
        Me.StatusBar1 = New System.Windows.Forms.StatusBar()
        Me.StatusBarPanel1 = New System.Windows.Forms.StatusBarPanel()
        Me.StatusBarPanelID = New System.Windows.Forms.StatusBarPanel()
        Me.StatusBarPanel2 = New System.Windows.Forms.StatusBarPanel()
        Me.btnMGI = New System.Windows.Forms.Button()
        Me.ContextMenu1 = New System.Windows.Forms.ContextMenu()
        Me.txtSelection = New System.Windows.Forms.TextBox()
        Me.fldOrgNum = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.btnHelp = New System.Windows.Forms.Button()
        Me.ErrorProvider1 = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.Label11 = New System.Windows.Forms.Label()
        Me.lblSelectedWhat = New System.Windows.Forms.Label()
        Me.lblSelectedID = New System.Windows.Forms.Label()
        CType(Me.grdMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.MainCaseBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsMainCase1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabControl1.SuspendLayout()
        CType(Me.StatusBarPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.StatusBarPanelID, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.StatusBarPanel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'daspMainCase
        '
        Me.daspMainCase.SelectCommand = Me.SqlSelectCommand1
        Me.daspMainCase.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "MainCase", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("CaseID", "CaseID"), New System.Data.Common.DataColumnMapping("CaseName", "CaseName"), New System.Data.Common.DataColumnMapping("OrgNum", "OrgNum"), New System.Data.Common.DataColumnMapping("StatusNum", "StatusNum"), New System.Data.Common.DataColumnMapping("CaseMgrNum", "CaseMgrNum"), New System.Data.Common.DataColumnMapping("OpenDate", "OpenDate"), New System.Data.Common.DataColumnMapping("CloseDate", "CloseDate"), New System.Data.Common.DataColumnMapping("CRGNum", "CRGNum"), New System.Data.Common.DataColumnMapping("Description", "Description"), New System.Data.Common.DataColumnMapping("NoResources", "NoResources"), New System.Data.Common.DataColumnMapping("FeedbackRequestedDate", "FeedbackRequestedDate"), New System.Data.Common.DataColumnMapping("FeedbackReceivedDate", "FeedbackReceivedDate"), New System.Data.Common.DataColumnMapping("CRGConsultingOrgNum", "CRGConsultingOrgNum"), New System.Data.Common.DataColumnMapping("Stamped", "Stamped")})})
        Me.daspMainCase.UpdateCommand = Me.SqlUpdateCommand1
        '
        'SqlSelectCommand1
        '
        Me.SqlSelectCommand1.CommandText = "dbo.MainCase"
        Me.SqlSelectCommand1.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlSelectCommand1.Connection = Me.SqlConnection1
        Me.SqlSelectCommand1.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, False, CType(0, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing), New System.Data.SqlClient.SqlParameter("@ID", System.Data.SqlDbType.Int, 4)})
        '
        'SqlConnection1
        '
        Me.SqlConnection1.ConnectionString = "Data Source=SOLOMON2008\SOLOMON2008;Initial Catalog=InfoCtr_be;Integrated Securit" & _
    "y=True"
        Me.SqlConnection1.FireInfoMessageEventOnUserErrors = False
        '
        'SqlUpdateCommand1
        '
        Me.SqlUpdateCommand1.CommandText = "dbo.MainCaseUpdate"
        Me.SqlUpdateCommand1.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlUpdateCommand1.Connection = Me.SqlConnection1
        Me.SqlUpdateCommand1.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, False, CType(0, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing), New System.Data.SqlClient.SqlParameter("@CaseName", System.Data.SqlDbType.NVarChar, 50, "CaseName"), New System.Data.SqlClient.SqlParameter("@OrgNum", System.Data.SqlDbType.Int, 4, "OrgNum"), New System.Data.SqlClient.SqlParameter("@StatusNum", System.Data.SqlDbType.SmallInt, 2, "StatusNum"), New System.Data.SqlClient.SqlParameter("@CaseMgrNum", System.Data.SqlDbType.Int, 4, "CaseMgrNum"), New System.Data.SqlClient.SqlParameter("@StartDate", System.Data.SqlDbType.[Date], 4, "OpenDate"), New System.Data.SqlClient.SqlParameter("@CloseDate", System.Data.SqlDbType.[Date], 4, "CloseDate"), New System.Data.SqlClient.SqlParameter("@CRGNum", System.Data.SqlDbType.SmallInt, 2, "CRGNum"), New System.Data.SqlClient.SqlParameter("@Description", System.Data.SqlDbType.VarChar, 500, "Description"), New System.Data.SqlClient.SqlParameter("@NoResources", System.Data.SqlDbType.Bit, 1, "NoResources"), New System.Data.SqlClient.SqlParameter("@FeedbackMailed", System.Data.SqlDbType.[Date], 4, "FeedbackRequestedDate"), New System.Data.SqlClient.SqlParameter("@FeedbackReceived", System.Data.SqlDbType.[Date], 4, "FeedbackReceivedDate"), New System.Data.SqlClient.SqlParameter("@FromCRG", System.Data.SqlDbType.Bit, 0, "FromCRG"), New System.Data.SqlClient.SqlParameter("@CRGConsultingOrgNum", System.Data.SqlDbType.Int, 4, "CRGConsultingOrgNum"), New System.Data.SqlClient.SqlParameter("@ID", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "CaseID", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@Original_Stamp", System.Data.SqlDbType.Timestamp, 8, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "Stamped", System.Data.DataRowVersion.Original, Nothing)})
        '
        'MainMenu1
        '
        Me.MainMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem1, Me.MenuItem2, Me.MenuItem4, Me.MenuItem7, Me.mmGoTo, Me.mmHelp})
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
        Me.miDelete.Text = "Delete Case"
        '
        'MenuItem4
        '
        Me.MenuItem4.Index = 2
        Me.MenuItem4.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.miRequestFeedback})
        Me.MenuItem4.Text = "Send"
        '
        'miRequestFeedback
        '
        Me.miRequestFeedback.Index = 0
        Me.miRequestFeedback.Text = "Request Resource Feedback"
        '
        'MenuItem7
        '
        Me.MenuItem7.Index = 3
        Me.MenuItem7.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.miPrintConversation})
        Me.MenuItem7.Text = "Reports"
        '
        'miPrintConversation
        '
        Me.miPrintConversation.Index = 0
        Me.miPrintConversation.Text = "Print Case Conversations"
        '
        'mmGoTo
        '
        Me.mmGoTo.Index = 4
        Me.mmGoTo.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.miGotoConversation, Me.miGotoRecommendation, Me.miGotoGrant, Me.miGotoOrg})
        Me.mmGoTo.Text = "Go to"
        '
        'miGotoConversation
        '
        Me.miGotoConversation.Enabled = False
        Me.miGotoConversation.Index = 0
        Me.miGotoConversation.Text = "Conversation"
        '
        'miGotoRecommendation
        '
        Me.miGotoRecommendation.Enabled = False
        Me.miGotoRecommendation.Index = 1
        Me.miGotoRecommendation.Text = "Recommendation"
        '
        'miGotoGrant
        '
        Me.miGotoGrant.Enabled = False
        Me.miGotoGrant.Index = 2
        Me.miGotoGrant.Text = "Grant"
        '
        'miGotoOrg
        '
        Me.miGotoOrg.Index = 3
        Me.miGotoOrg.Text = "Organization"
        '
        'mmHelp
        '
        Me.mmHelp.Index = 5
        Me.mmHelp.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.miHelp, Me.miDefinitions})
        Me.mmHelp.Text = "&Help"
        '
        'miHelp
        '
        Me.miHelp.Index = 0
        Me.miHelp.Text = "Help"
        '
        'miDefinitions
        '
        Me.miDefinitions.Index = 1
        Me.miDefinitions.Text = "Status Definitions"
        '
        'miUndo
        '
        Me.miUndo.Index = -1
        Me.miUndo.Text = "Undo this field"
        '
        'grdMain
        '
        Me.grdMain.AlternatingBackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.grdMain.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdMain.DataMember = ""
        Me.grdMain.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.grdMain.Location = New System.Drawing.Point(352, 29)
        Me.grdMain.Name = "grdMain"
        Me.grdMain.ReadOnly = True
        Me.grdMain.RowHeaderWidth = 20
        Me.grdMain.Size = New System.Drawing.Size(623, 421)
        Me.grdMain.TabIndex = 13
        Me.grdMain.TableStyles.AddRange(New System.Windows.Forms.DataGridTableStyle() {Me.tsConversation, Me.tsGrant, Me.tsRecommend})
        Me.grdMain.TabStop = False
        '
        'tsConversation
        '
        Me.tsConversation.AlternatingBackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.tsConversation.DataGrid = Me.grdMain
        Me.tsConversation.ForeColor = System.Drawing.Color.DarkGreen
        Me.tsConversation.GridColumnStyles.AddRange(New System.Windows.Forms.DataGridColumnStyle() {Me.DataGridTextBoxColumn1, Me.DataGridTextBoxColumn2, Me.DataGridTextBoxColumn3, Me.DataGridTextBoxColumn4, Me.DataGridTextBoxColumn5, Me.DataGridTextBoxColumn6, Me.DataGridTextBoxColumn7})
        Me.tsConversation.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.tsConversation.MappingName = "tConversation"
        Me.tsConversation.RowHeaderWidth = 15
        Me.tsConversation.SelectionBackColor = System.Drawing.SystemColors.Highlight
        '
        'DataGridTextBoxColumn1
        '
        Me.DataGridTextBoxColumn1.Format = ""
        Me.DataGridTextBoxColumn1.FormatInfo = Nothing
        Me.DataGridTextBoxColumn1.HeaderText = "ConversID"
        Me.DataGridTextBoxColumn1.MappingName = "ConversID"
        Me.DataGridTextBoxColumn1.Width = 0
        '
        'DataGridTextBoxColumn2
        '
        Me.DataGridTextBoxColumn2.Format = ""
        Me.DataGridTextBoxColumn2.FormatInfo = Nothing
        Me.DataGridTextBoxColumn2.HeaderText = "CaseNum"
        Me.DataGridTextBoxColumn2.MappingName = "CaseNum"
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
        'DataGridTextBoxColumn4
        '
        Me.DataGridTextBoxColumn4.Format = "d"
        Me.DataGridTextBoxColumn4.FormatInfo = Nothing
        Me.DataGridTextBoxColumn4.HeaderText = "Date "
        Me.DataGridTextBoxColumn4.MappingName = "ConversDate"
        Me.DataGridTextBoxColumn4.Width = 75
        '
        'DataGridTextBoxColumn5
        '
        Me.DataGridTextBoxColumn5.Format = ""
        Me.DataGridTextBoxColumn5.FormatInfo = Nothing
        Me.DataGridTextBoxColumn5.HeaderText = "Contact"
        Me.DataGridTextBoxColumn5.MappingName = "ContactName"
        Me.DataGridTextBoxColumn5.Width = 75
        '
        'DataGridTextBoxColumn6
        '
        Me.DataGridTextBoxColumn6.Format = ""
        Me.DataGridTextBoxColumn6.FormatInfo = Nothing
        Me.DataGridTextBoxColumn6.HeaderText = "Brief Summary"
        Me.DataGridTextBoxColumn6.MappingName = "BriefSummary"
        Me.DataGridTextBoxColumn6.Width = 250
        '
        'DataGridTextBoxColumn7
        '
        Me.DataGridTextBoxColumn7.Format = ""
        Me.DataGridTextBoxColumn7.FormatInfo = Nothing
        Me.DataGridTextBoxColumn7.HeaderText = "Caller"
        Me.DataGridTextBoxColumn7.MappingName = "StaffName"
        Me.DataGridTextBoxColumn7.NullText = "0"
        Me.DataGridTextBoxColumn7.Width = 125
        '
        'tsGrant
        '
        Me.tsGrant.DataGrid = Me.grdMain
        Me.tsGrant.GridColumnStyles.AddRange(New System.Windows.Forms.DataGridColumnStyle() {Me.DataGridTextBoxColumn18, Me.DataGridTextBoxColumn23, Me.DataGridTextBoxColumn19, Me.DataGridTextBoxColumn22, Me.DataGridTextBoxColumn20, Me.DataGridTextBoxColumn21})
        Me.tsGrant.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.tsGrant.MappingName = "tGrant"
        '
        'DataGridTextBoxColumn18
        '
        Me.DataGridTextBoxColumn18.Format = ""
        Me.DataGridTextBoxColumn18.FormatInfo = Nothing
        Me.DataGridTextBoxColumn18.HeaderText = "GrantIDtxt"
        Me.DataGridTextBoxColumn18.MappingName = "GrantIDtxt"
        Me.DataGridTextBoxColumn18.Width = 0
        '
        'DataGridTextBoxColumn23
        '
        Me.DataGridTextBoxColumn23.Format = ""
        Me.DataGridTextBoxColumn23.FormatInfo = Nothing
        Me.DataGridTextBoxColumn23.HeaderText = "GIID"
        Me.DataGridTextBoxColumn23.MappingName = "GIID"
        Me.DataGridTextBoxColumn23.Width = 0
        '
        'DataGridTextBoxColumn19
        '
        Me.DataGridTextBoxColumn19.Format = "c"
        Me.DataGridTextBoxColumn19.FormatInfo = Nothing
        Me.DataGridTextBoxColumn19.HeaderText = "Amount"
        Me.DataGridTextBoxColumn19.MappingName = "Amount"
        Me.DataGridTextBoxColumn19.Width = 75
        '
        'DataGridTextBoxColumn22
        '
        Me.DataGridTextBoxColumn22.Format = ""
        Me.DataGridTextBoxColumn22.FormatInfo = Nothing
        Me.DataGridTextBoxColumn22.HeaderText = "Type of Grant"
        Me.DataGridTextBoxColumn22.MappingName = "TypeofGrant"
        Me.DataGridTextBoxColumn22.Width = 125
        '
        'DataGridTextBoxColumn20
        '
        Me.DataGridTextBoxColumn20.Format = ""
        Me.DataGridTextBoxColumn20.FormatInfo = Nothing
        Me.DataGridTextBoxColumn20.HeaderText = "Latest Activity"
        Me.DataGridTextBoxColumn20.MappingName = "GrantLatest"
        Me.DataGridTextBoxColumn20.Width = 150
        '
        'DataGridTextBoxColumn21
        '
        Me.DataGridTextBoxColumn21.Format = ""
        Me.DataGridTextBoxColumn21.FormatInfo = Nothing
        Me.DataGridTextBoxColumn21.HeaderText = "Congr. Final Report"
        Me.DataGridTextBoxColumn21.MappingName = "CongregationFinalReport"
        Me.DataGridTextBoxColumn21.Width = 150
        '
        'tsRecommend
        '
        Me.tsRecommend.AlternatingBackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.tsRecommend.DataGrid = Me.grdMain
        Me.tsRecommend.ForeColor = System.Drawing.Color.DarkGreen
        Me.tsRecommend.GridColumnStyles.AddRange(New System.Windows.Forms.DataGridColumnStyle() {Me.DataGridTextBoxColumn8, Me.DataGridTextBoxColumn9, Me.DataGridTextBoxColumn10, Me.DataGridTextBoxColumn11, Me.DataGridTextBoxColumn12, Me.DataGridTextBoxColumn13, Me.DataGridTextBoxColumn14, Me.DataGridTextBoxColumn15, Me.DataGridTextBoxColumn16, Me.DataGridTextBoxColumn17, Me.DataGridTextBoxColumn25, Me.DataGridTextBoxColumn24})
        Me.tsRecommend.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.tsRecommend.MappingName = "tRecommend"
        Me.tsRecommend.RowHeaderWidth = 15
        Me.tsRecommend.SelectionBackColor = System.Drawing.SystemColors.Highlight
        '
        'DataGridTextBoxColumn8
        '
        Me.DataGridTextBoxColumn8.Format = ""
        Me.DataGridTextBoxColumn8.FormatInfo = Nothing
        Me.DataGridTextBoxColumn8.MappingName = "RecommendID"
        Me.DataGridTextBoxColumn8.Width = 0
        '
        'DataGridTextBoxColumn9
        '
        Me.DataGridTextBoxColumn9.Format = ""
        Me.DataGridTextBoxColumn9.FormatInfo = Nothing
        Me.DataGridTextBoxColumn9.HeaderText = "ResourceNum"
        Me.DataGridTextBoxColumn9.MappingName = "ResourceNum"
        Me.DataGridTextBoxColumn9.Width = 0
        '
        'DataGridTextBoxColumn10
        '
        Me.DataGridTextBoxColumn10.Format = ""
        Me.DataGridTextBoxColumn10.FormatInfo = Nothing
        Me.DataGridTextBoxColumn10.HeaderText = "CaseNum"
        Me.DataGridTextBoxColumn10.MappingName = "CaseNum"
        Me.DataGridTextBoxColumn10.Width = 0
        '
        'DataGridTextBoxColumn11
        '
        Me.DataGridTextBoxColumn11.Format = ""
        Me.DataGridTextBoxColumn11.FormatInfo = Nothing
        Me.DataGridTextBoxColumn11.HeaderText = "CallNum"
        Me.DataGridTextBoxColumn11.MappingName = "CallNum"
        Me.DataGridTextBoxColumn11.Width = 0
        '
        'DataGridTextBoxColumn12
        '
        Me.DataGridTextBoxColumn12.Format = ""
        Me.DataGridTextBoxColumn12.FormatInfo = Nothing
        Me.DataGridTextBoxColumn12.HeaderText = "GrantNum"
        Me.DataGridTextBoxColumn12.MappingName = "GrantNum"
        Me.DataGridTextBoxColumn12.Width = 0
        '
        'DataGridTextBoxColumn13
        '
        Me.DataGridTextBoxColumn13.Format = "d"
        Me.DataGridTextBoxColumn13.FormatInfo = Nothing
        Me.DataGridTextBoxColumn13.HeaderText = "Recommend Date"
        Me.DataGridTextBoxColumn13.MappingName = "RecommendDate"
        Me.DataGridTextBoxColumn13.Width = 75
        '
        'DataGridTextBoxColumn14
        '
        Me.DataGridTextBoxColumn14.Format = ""
        Me.DataGridTextBoxColumn14.FormatInfo = Nothing
        Me.DataGridTextBoxColumn14.HeaderText = "Resource Name"
        Me.DataGridTextBoxColumn14.MappingName = "ResourceName"
        Me.DataGridTextBoxColumn14.Width = 200
        '
        'DataGridTextBoxColumn15
        '
        Me.DataGridTextBoxColumn15.Format = ""
        Me.DataGridTextBoxColumn15.FormatInfo = Nothing
        Me.DataGridTextBoxColumn15.HeaderText = "Type of Resource"
        Me.DataGridTextBoxColumn15.MappingName = "Type"
        Me.DataGridTextBoxColumn15.Width = 75
        '
        'DataGridTextBoxColumn16
        '
        Me.DataGridTextBoxColumn16.Format = ""
        Me.DataGridTextBoxColumn16.FormatInfo = Nothing
        Me.DataGridTextBoxColumn16.HeaderText = "Active"
        Me.DataGridTextBoxColumn16.MappingName = "Active"
        Me.DataGridTextBoxColumn16.Width = 75
        '
        'DataGridTextBoxColumn17
        '
        Me.DataGridTextBoxColumn17.Format = ""
        Me.DataGridTextBoxColumn17.FormatInfo = Nothing
        Me.DataGridTextBoxColumn17.HeaderText = "Used"
        Me.DataGridTextBoxColumn17.MappingName = "Used"
        Me.DataGridTextBoxColumn17.Width = 75
        '
        'DataGridTextBoxColumn25
        '
        Me.DataGridTextBoxColumn25.Format = ""
        Me.DataGridTextBoxColumn25.FormatInfo = Nothing
        Me.DataGridTextBoxColumn25.HeaderText = "OrgNum"
        Me.DataGridTextBoxColumn25.MappingName = "OrgNum"
        Me.DataGridTextBoxColumn25.Width = 0
        '
        'DataGridTextBoxColumn24
        '
        Me.DataGridTextBoxColumn24.Format = ""
        Me.DataGridTextBoxColumn24.FormatInfo = Nothing
        Me.DataGridTextBoxColumn24.HeaderText = "Discovered By"
        Me.DataGridTextBoxColumn24.MappingName = "WhoRecommended"
        Me.DataGridTextBoxColumn24.Width = 75
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.Panel1.Controls.Add(Me.grdMain)
        Me.Panel1.Controls.Add(Me.Panel3)
        Me.Panel1.Controls.Add(Me.Label9)
        Me.Panel1.Controls.Add(Me.DtFeedbackRecd)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Controls.Add(Me.Label8)
        Me.Panel1.Controls.Add(Me.DtFeedbackRequested)
        Me.Panel1.Controls.Add(Me.TxtCaseName)
        Me.Panel1.Controls.Add(Me.Label7)
        Me.Panel1.Controls.Add(Me.lblCurrentCaseRating)
        Me.Panel1.Controls.Add(Me.chkResourceReqd)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.TabControl1)
        Me.Panel1.Controls.Add(Me.DtCaseClose)
        Me.Panel1.Controls.Add(Me.DtCaseOpen)
        Me.Panel1.Controls.Add(Me.cboMgr)
        Me.Panel1.Controls.Add(Me.cboStatus)
        Me.Panel1.Controls.Add(Me.cboCRG)
        Me.Panel1.Controls.Add(Me.Label17)
        Me.Panel1.Controls.Add(Me.Label16)
        Me.Panel1.Controls.Add(Me.txtDescription)
        Me.Panel1.Controls.Add(Me.Label13)
        Me.Panel1.Controls.Add(Me.Label12)
        Me.Panel1.Controls.Add(Me.Label10)
        Me.Panel1.Location = New System.Drawing.Point(10, 50)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(978, 458)
        Me.Panel1.TabIndex = 0
        '
        'Panel3
        '
        Me.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel3.Controls.Add(Me.chkFromCRG)
        Me.Panel3.Controls.Add(Me.cboCRGConsultant)
        Me.Panel3.Controls.Add(Me.Label4)
        Me.Panel3.Location = New System.Drawing.Point(5, 238)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(339, 68)
        Me.Panel3.TabIndex = 169
        '
        'chkFromCRG
        '
        Me.chkFromCRG.DataBindings.Add(New System.Windows.Forms.Binding("Checked", Me.MainCaseBindingSource, "FromCRG", True))
        Me.chkFromCRG.Location = New System.Drawing.Point(77, 4)
        Me.chkFromCRG.Name = "chkFromCRG"
        Me.chkFromCRG.Size = New System.Drawing.Size(167, 27)
        Me.chkFromCRG.TabIndex = 151
        Me.chkFromCRG.Text = "Case From CRG Website"
        Me.chkFromCRG.UseVisualStyleBackColor = False
        '
        'MainCaseBindingSource
        '
        Me.MainCaseBindingSource.DataMember = "MainCase"
        Me.MainCaseBindingSource.DataSource = Me.DsMainCase1
        '
        'DsMainCase1
        '
        Me.DsMainCase1.DataSetName = "dsMainCase"
        Me.DsMainCase1.Locale = New System.Globalization.CultureInfo("en-US")
        Me.DsMainCase1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'cboCRGConsultant
        '
        Me.cboCRGConsultant.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboCRGConsultant.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboCRGConsultant.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.MainCaseBindingSource, "CRGConsultingOrgNum", True))
        Me.cboCRGConsultant.DisplayMember = "ConsultantName"
        Me.cboCRGConsultant.DropDownWidth = 250
        Me.cboCRGConsultant.Location = New System.Drawing.Point(76, 37)
        Me.cboCRGConsultant.Name = "cboCRGConsultant"
        Me.cboCRGConsultant.RestrictContentToListItems = True
        Me.cboCRGConsultant.Size = New System.Drawing.Size(258, 21)
        Me.cboCRGConsultant.TabIndex = 149
        Me.cboCRGConsultant.Tag = "ConsultingOrg"
        Me.cboCRGConsultant.ValueMember = "ConsultantID"
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(3, 24)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(69, 38)
        Me.Label4.TabIndex = 150
        Me.Label4.Text = "Resource" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Consulting" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Organization"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label9
        '
        Me.Label9.Location = New System.Drawing.Point(31, 433)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(131, 20)
        Me.Label9.TabIndex = 168
        Me.Label9.Text = "Feedback Received Date"
        '
        'DtFeedbackRecd
        '
        Me.DtFeedbackRecd.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainCaseBindingSource, "FeedbackReceivedDate", True))
        Me.DtFeedbackRecd.Location = New System.Drawing.Point(182, 430)
        Me.DtFeedbackRecd.Name = "DtFeedbackRecd"
        Me.DtFeedbackRecd.Size = New System.Drawing.Size(105, 20)
        Me.DtFeedbackRecd.TabIndex = 10
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(161, 212)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(153, 20)
        Me.Label5.TabIndex = 6
        Me.Label5.Text = "(1 Technical   »  5 Adaptive)"
        Me.ToolTip1.SetToolTip(Me.Label5, "Adjust Case Rating from Conversation Detail window.")
        '
        'Label6
        '
        Me.Label6.Location = New System.Drawing.Point(31, 408)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(145, 20)
        Me.Label6.TabIndex = 166
        Me.Label6.Text = "Feedback Requested Date"
        '
        'Label8
        '
        Me.Label8.Location = New System.Drawing.Point(19, 16)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(39, 31)
        Me.Label8.TabIndex = 162
        Me.Label8.Text = "Case Name"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'DtFeedbackRequested
        '
        Me.DtFeedbackRequested.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainCaseBindingSource, "FeedbackRequestedDate", True))
        Me.DtFeedbackRequested.Location = New System.Drawing.Point(182, 404)
        Me.DtFeedbackRequested.Name = "DtFeedbackRequested"
        Me.DtFeedbackRequested.Size = New System.Drawing.Size(105, 20)
        Me.DtFeedbackRequested.TabIndex = 9
        '
        'TxtCaseName
        '
        Me.TxtCaseName.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainCaseBindingSource, "CaseName", True))
        Me.TxtCaseName.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtCaseName.ForeColor = System.Drawing.SystemColors.Highlight
        Me.TxtCaseName.Location = New System.Drawing.Point(62, 14)
        Me.TxtCaseName.MaxLength = 50
        Me.TxtCaseName.Multiline = True
        Me.TxtCaseName.Name = "TxtCaseName"
        Me.TxtCaseName.Size = New System.Drawing.Size(277, 45)
        Me.TxtCaseName.TabIndex = 0
        Me.TxtCaseName.Tag = "Case NAME"
        '
        'Label7
        '
        Me.Label7.Location = New System.Drawing.Point(16, 154)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(64, 16)
        Me.Label7.TabIndex = 154
        Me.Label7.Text = "Open Date"
        '
        'lblCurrentCaseRating
        '
        Me.lblCurrentCaseRating.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblCurrentCaseRating.Enabled = False
        Me.lblCurrentCaseRating.Location = New System.Drawing.Point(124, 207)
        Me.lblCurrentCaseRating.Name = "lblCurrentCaseRating"
        Me.lblCurrentCaseRating.Size = New System.Drawing.Size(32, 20)
        Me.lblCurrentCaseRating.TabIndex = 7
        Me.lblCurrentCaseRating.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ToolTip1.SetToolTip(Me.lblCurrentCaseRating, "Case Rating is from last Conversation.")
        '
        'chkResourceReqd
        '
        Me.chkResourceReqd.DataBindings.Add(New System.Windows.Forms.Binding("Checked", Me.MainCaseBindingSource, "NoResources", True))
        Me.chkResourceReqd.Location = New System.Drawing.Point(211, 162)
        Me.chkResourceReqd.Name = "chkResourceReqd"
        Me.chkResourceReqd.Size = New System.Drawing.Size(128, 27)
        Me.chkResourceReqd.TabIndex = 7
        Me.chkResourceReqd.Text = "No Resource Req'd"
        Me.chkResourceReqd.UseVisualStyleBackColor = False
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(16, 181)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(64, 14)
        Me.Label3.TabIndex = 153
        Me.Label3.Text = "Close Date"
        Me.ToolTip1.SetToolTip(Me.Label3, "Change status to Closed  to enter this date. To change the date, choose Closed ag" & _
        "ain.")
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.pgConversation)
        Me.TabControl1.Controls.Add(Me.pgGrant)
        Me.TabControl1.Controls.Add(Me.pgRecommendation)
        Me.TabControl1.Location = New System.Drawing.Point(350, 3)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(627, 28)
        Me.TabControl1.TabIndex = 12
        '
        'pgConversation
        '
        Me.pgConversation.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pgConversation.ForeColor = System.Drawing.SystemColors.ControlText
        Me.pgConversation.Location = New System.Drawing.Point(4, 22)
        Me.pgConversation.Name = "pgConversation"
        Me.pgConversation.Size = New System.Drawing.Size(619, 2)
        Me.pgConversation.TabIndex = 0
        Me.pgConversation.Tag = "CONVERSATIONS"
        Me.pgConversation.Text = "     CONVERSATIONS     "
        Me.pgConversation.UseVisualStyleBackColor = True
        '
        'pgGrant
        '
        Me.pgGrant.Location = New System.Drawing.Point(4, 22)
        Me.pgGrant.Name = "pgGrant"
        Me.pgGrant.Size = New System.Drawing.Size(619, 2)
        Me.pgGrant.TabIndex = 2
        Me.pgGrant.Tag = "GRANTS or MGI Applications"
        Me.pgGrant.Text = "  GRANTS  or MGI Applications  "
        Me.pgGrant.UseVisualStyleBackColor = True
        '
        'pgRecommendation
        '
        Me.pgRecommendation.ForeColor = System.Drawing.SystemColors.ControlText
        Me.pgRecommendation.Location = New System.Drawing.Point(4, 22)
        Me.pgRecommendation.Name = "pgRecommendation"
        Me.pgRecommendation.Size = New System.Drawing.Size(619, 2)
        Me.pgRecommendation.TabIndex = 1
        Me.pgRecommendation.Tag = "Resource RECOMMENDATIONS"
        Me.pgRecommendation.Text = "   Resource RECOMMENDATIONS"
        Me.pgRecommendation.UseVisualStyleBackColor = True
        '
        'DtCaseClose
        '
        Me.DtCaseClose.BackColor = System.Drawing.SystemColors.ControlLight
        Me.DtCaseClose.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainCaseBindingSource, "CloseDate", True))
        Me.DtCaseClose.Location = New System.Drawing.Point(81, 176)
        Me.DtCaseClose.Name = "DtCaseClose"
        Me.DtCaseClose.ReadOnly = True
        Me.DtCaseClose.Size = New System.Drawing.Size(94, 20)
        Me.DtCaseClose.TabIndex = 6
        Me.DtCaseClose.TabStop = False
        Me.ToolTip1.SetToolTip(Me.DtCaseClose, "Change status to Closed  to enter this date. To change the date, choose Closed ag" & _
        "ain.")
        '
        'DtCaseOpen
        '
        Me.DtCaseOpen.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainCaseBindingSource, "OpenDate", True))
        Me.DtCaseOpen.Location = New System.Drawing.Point(82, 149)
        Me.DtCaseOpen.Name = "DtCaseOpen"
        Me.DtCaseOpen.Size = New System.Drawing.Size(94, 20)
        Me.DtCaseOpen.TabIndex = 4
        '
        'cboMgr
        '
        Me.cboMgr.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboMgr.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboMgr.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.MainCaseBindingSource, "CaseMgrNum", True))
        Me.cboMgr.DropDownWidth = 250
        Me.cboMgr.Location = New System.Drawing.Point(62, 93)
        Me.cboMgr.Name = "cboMgr"
        Me.cboMgr.RestrictContentToListItems = True
        Me.cboMgr.Size = New System.Drawing.Size(277, 21)
        Me.cboMgr.TabIndex = 2
        Me.cboMgr.Tag = "Case MANAGER"
        '
        'cboStatus
        '
        Me.cboStatus.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboStatus.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboStatus.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.MainCaseBindingSource, "StatusNum", True))
        Me.cboStatus.Location = New System.Drawing.Point(62, 65)
        Me.cboStatus.Name = "cboStatus"
        Me.cboStatus.RestrictContentToListItems = True
        Me.cboStatus.Size = New System.Drawing.Size(277, 21)
        Me.cboStatus.TabIndex = 1
        Me.cboStatus.Tag = "Case STATUS"
        '
        'cboCRG
        '
        Me.cboCRG.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboCRG.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboCRG.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.MainCaseBindingSource, "CRGNum", True))
        Me.cboCRG.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCRG.DropDownWidth = 500
        Me.cboCRG.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboCRG.ItemHeight = 12
        Me.cboCRG.Location = New System.Drawing.Point(62, 122)
        Me.cboCRG.MaxDropDownItems = 20
        Me.cboCRG.Name = "cboCRG"
        Me.cboCRG.RestrictContentToListItems = True
        Me.cboCRG.Size = New System.Drawing.Size(277, 20)
        Me.cboCRG.TabIndex = 3
        Me.cboCRG.Tag = "Case CRG"
        Me.ToolTip1.SetToolTip(Me.cboCRG, "Right click and enter partial CRG term to filter the dropdown possibilities")
        '
        'Label17
        '
        Me.Label17.Location = New System.Drawing.Point(16, 212)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(109, 20)
        Me.Label17.TabIndex = 151
        Me.Label17.Text = "Current Case Rating"
        Me.ToolTip1.SetToolTip(Me.Label17, "Adjust Case Rating from Conversation Detail window.")
        '
        'Label16
        '
        Me.Label16.Location = New System.Drawing.Point(11, 318)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(64, 37)
        Me.Label16.TabIndex = 150
        Me.Label16.Text = "Case Description"
        '
        'txtDescription
        '
        Me.txtDescription.AcceptsReturn = True
        Me.txtDescription.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainCaseBindingSource, "Description", True))
        Me.txtDescription.Location = New System.Drawing.Point(81, 312)
        Me.txtDescription.MaxLength = 300
        Me.txtDescription.Multiline = True
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.Size = New System.Drawing.Size(260, 83)
        Me.txtDescription.TabIndex = 8
        '
        'Label13
        '
        Me.Label13.Location = New System.Drawing.Point(28, 122)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(33, 20)
        Me.Label13.TabIndex = 149
        Me.Label13.Text = "CRG"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label12
        '
        Me.Label12.Location = New System.Drawing.Point(12, 94)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(49, 20)
        Me.Label12.TabIndex = 148
        Me.Label12.Text = "Manager"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label10
        '
        Me.Label10.Location = New System.Drawing.Point(21, 65)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(40, 20)
        Me.Label10.TabIndex = 147
        Me.Label10.Text = "Status"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label2.Location = New System.Drawing.Point(10, 10)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(150, 20)
        Me.Label2.TabIndex = 155
        Me.Label2.Text = "CASE DETAIL"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'fldGotoOrg
        '
        Me.fldGotoOrg.BackColor = System.Drawing.SystemColors.Control
        Me.fldGotoOrg.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fldGotoOrg.ForeColor = System.Drawing.Color.ForestGreen
        Me.fldGotoOrg.Location = New System.Drawing.Point(275, 5)
        Me.fldGotoOrg.Multiline = True
        Me.fldGotoOrg.Name = "fldGotoOrg"
        Me.fldGotoOrg.ReadOnly = True
        Me.fldGotoOrg.Size = New System.Drawing.Size(370, 40)
        Me.fldGotoOrg.TabIndex = 159
        Me.fldGotoOrg.Tag = "Org"
        Me.fldGotoOrg.Text = "go to Org"
        Me.ToolTip1.SetToolTip(Me.fldGotoOrg, "Doubleclick to open Organization Detail window.")
        '
        'btnSaveExit
        '
        Me.btnSaveExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSaveExit.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.btnSaveExit.CausesValidation = False
        Me.btnSaveExit.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSaveExit.Image = Global.InfoCtr.My.Resources.Resources.btnSaveExit
        Me.btnSaveExit.ImageAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSaveExit.Location = New System.Drawing.Point(155, 1)
        Me.btnSaveExit.Name = "btnSaveExit"
        Me.btnSaveExit.Size = New System.Drawing.Size(40, 35)
        Me.btnSaveExit.TabIndex = 415
        Me.btnSaveExit.Text = "Close"
        Me.btnSaveExit.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.ToolTip1.SetToolTip(Me.btnSaveExit, "Saves any changes and Closes this window")
        Me.btnSaveExit.UseVisualStyleBackColor = False
        '
        'btnFeedback
        '
        Me.btnFeedback.BackColor = System.Drawing.SystemColors.Control
        Me.btnFeedback.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnFeedback.ImageAlign = System.Drawing.ContentAlignment.TopRight
        Me.btnFeedback.Location = New System.Drawing.Point(47, 0)
        Me.btnFeedback.Name = "btnFeedback"
        Me.btnFeedback.Size = New System.Drawing.Size(55, 35)
        Me.btnFeedback.TabIndex = 233
        Me.btnFeedback.Text = "Request Feedback"
        Me.btnFeedback.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.ToolTip1.SetToolTip(Me.btnFeedback, "Request Feedback letter (merge into Word doc)")
        Me.btnFeedback.UseVisualStyleBackColor = False
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
        Me.btnNew.TabIndex = 232
        Me.btnNew.Text = "New"
        Me.btnNew.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.ToolTip1.SetToolTip(Me.btnNew, "Add new Conversation")
        Me.btnNew.UseVisualStyleBackColor = False
        '
        'btnViewFile
        '
        Me.btnViewFile.BackColor = System.Drawing.SystemColors.Control
        Me.btnViewFile.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnViewFile.Image = Global.InfoCtr.My.Resources.Resources.btnAttached
        Me.btnViewFile.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.btnViewFile.Location = New System.Drawing.Point(108, 1)
        Me.btnViewFile.Name = "btnViewFile"
        Me.btnViewFile.Size = New System.Drawing.Size(41, 35)
        Me.btnViewFile.TabIndex = 417
        Me.btnViewFile.Text = "Files"
        Me.btnViewFile.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.ToolTip1.SetToolTip(Me.btnViewFile, "Attach or open a document related to this case.")
        Me.btnViewFile.UseVisualStyleBackColor = False
        '
        'StatusBar1
        '
        Me.StatusBar1.Location = New System.Drawing.Point(0, 577)
        Me.StatusBar1.Name = "StatusBar1"
        Me.StatusBar1.Panels.AddRange(New System.Windows.Forms.StatusBarPanel() {Me.StatusBarPanel1, Me.StatusBarPanelID, Me.StatusBarPanel2})
        Me.StatusBar1.ShowPanels = True
        Me.StatusBar1.Size = New System.Drawing.Size(1000, 18)
        Me.StatusBar1.TabIndex = 426
        Me.StatusBar1.Text = "StatusBar1"
        Me.ToolTip1.SetToolTip(Me.StatusBar1, "Doubleclick to copy ID.")
        '
        'StatusBarPanel1
        '
        Me.StatusBarPanel1.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents
        Me.StatusBarPanel1.BorderStyle = System.Windows.Forms.StatusBarPanelBorderStyle.Raised
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
        Me.StatusBarPanelID.Text = "Case ID"
        Me.StatusBarPanelID.Width = 200
        '
        'StatusBarPanel2
        '
        Me.StatusBarPanel2.Alignment = System.Windows.Forms.HorizontalAlignment.Right
        Me.StatusBarPanel2.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring
        Me.StatusBarPanel2.BorderStyle = System.Windows.Forms.StatusBarPanelBorderStyle.None
        Me.StatusBarPanel2.Name = "StatusBarPanel2"
        Me.StatusBarPanel2.Text = "Use this window to change Case details, and go to related Conversations and Grant" & _
    "."
        Me.StatusBarPanel2.Width = 583
        '
        'btnMGI
        '
        Me.btnMGI.Location = New System.Drawing.Point(664, 2)
        Me.btnMGI.Name = "btnMGI"
        Me.btnMGI.Size = New System.Drawing.Size(67, 43)
        Me.btnMGI.TabIndex = 421
        Me.btnMGI.Text = "Major Grant Init Appl..."
        Me.btnMGI.UseVisualStyleBackColor = False
        '
        'txtSelection
        '
        Me.txtSelection.BackColor = System.Drawing.SystemColors.Control
        Me.txtSelection.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtSelection.Enabled = False
        Me.txtSelection.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSelection.ForeColor = System.Drawing.SystemColors.GrayText
        Me.txtSelection.Location = New System.Drawing.Point(642, 29)
        Me.txtSelection.Name = "txtSelection"
        Me.txtSelection.Size = New System.Drawing.Size(49, 13)
        Me.txtSelection.TabIndex = 213
        Me.txtSelection.TabStop = False
        Me.txtSelection.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'fldOrgNum
        '
        Me.fldOrgNum.BackColor = System.Drawing.SystemColors.Control
        Me.fldOrgNum.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainCaseBindingSource, "OrgNum", True))
        Me.fldOrgNum.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fldOrgNum.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.fldOrgNum.Location = New System.Drawing.Point(713, 523)
        Me.fldOrgNum.Name = "fldOrgNum"
        Me.fldOrgNum.Size = New System.Drawing.Size(75, 15)
        Me.fldOrgNum.TabIndex = 417
        Me.fldOrgNum.Text = "OrgID"
        Me.fldOrgNum.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlDark
        Me.Label1.Location = New System.Drawing.Point(657, 523)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(50, 14)
        Me.Label1.TabIndex = 418
        Me.Label1.Text = "Org ID:"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.SystemColors.Highlight
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Controls.Add(Me.btnViewFile)
        Me.Panel2.Controls.Add(Me.btnSaveExit)
        Me.Panel2.Controls.Add(Me.btnFeedback)
        Me.Panel2.Controls.Add(Me.btnNew)
        Me.Panel2.Location = New System.Drawing.Point(737, 5)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(200, 40)
        Me.Panel2.TabIndex = 420
        '
        'btnHelp
        '
        Me.btnHelp.Image = Global.InfoCtr.My.Resources.Resources.btnHelp
        Me.btnHelp.Location = New System.Drawing.Point(943, 5)
        Me.btnHelp.Name = "btnHelp"
        Me.btnHelp.Size = New System.Drawing.Size(30, 25)
        Me.btnHelp.TabIndex = 234
        Me.btnHelp.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnHelp.UseVisualStyleBackColor = True
        '
        'ErrorProvider1
        '
        Me.ErrorProvider1.ContainerControl = Me
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(203, 17)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(45, 13)
        Me.Label11.TabIndex = 169
        Me.Label11.Text = "Label11"
        Me.Label11.Visible = False
        '
        'lblSelectedWhat
        '
        Me.lblSelectedWhat.AutoSize = True
        Me.lblSelectedWhat.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSelectedWhat.ForeColor = System.Drawing.SystemColors.ControlDark
        Me.lblSelectedWhat.Location = New System.Drawing.Point(812, 523)
        Me.lblSelectedWhat.MinimumSize = New System.Drawing.Size(75, 0)
        Me.lblSelectedWhat.Name = "lblSelectedWhat"
        Me.lblSelectedWhat.Size = New System.Drawing.Size(75, 13)
        Me.lblSelectedWhat.TabIndex = 424
        Me.lblSelectedWhat.Text = "Selection #"
        Me.lblSelectedWhat.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblSelectedID
        '
        Me.lblSelectedID.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSelectedID.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.lblSelectedID.Location = New System.Drawing.Point(886, 522)
        Me.lblSelectedID.Name = "lblSelectedID"
        Me.lblSelectedID.Size = New System.Drawing.Size(75, 15)
        Me.lblSelectedID.TabIndex = 425
        Me.lblSelectedID.Text = "ID"
        Me.lblSelectedID.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'frmMainCase
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.CausesValidation = False
        Me.ClientSize = New System.Drawing.Size(1000, 595)
        Me.Controls.Add(Me.StatusBar1)
        Me.Controls.Add(Me.lblSelectedID)
        Me.Controls.Add(Me.lblSelectedWhat)
        Me.Controls.Add(Me.btnMGI)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.fldOrgNum)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnHelp)
        Me.Controls.Add(Me.txtSelection)
        Me.Controls.Add(Me.fldGotoOrg)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Panel1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Menu = Me.MainMenu1
        Me.Name = "frmMainCase"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Tag = "CASE"
        Me.Text = "CASE DETAIL"
        CType(Me.grdMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        CType(Me.MainCaseBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsMainCase1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabControl1.ResumeLayout(False)
        CType(Me.StatusBarPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.StatusBarPanelID, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.StatusBarPanel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region  'windows

#Region "LOAD"

    'ON LOAD
    Private Sub frmMainCase_Load(ByVal sender As Object, ByVal e As System.EventArgs) _
            Handles MyBase.Load

        Me.SuspendLayout()
        SetStatusBarText("loading")

SetMainDSConnection:
        Me.daspMainCase.SelectCommand.Connection = sc
        Me.daspMainCase.UpdateCommand.Connection = sc

SetDefaults:
        ctlIdentify = Me.TxtCaseName
        ctlNeutral = Me.btnHelp
        mainTopic = "Case"
        mainDS = Me.DsMainCase1
        mainTbl = Me.DsMainCase1.MainCase
        mainBSrce = Me.MainCaseBindingSource
        mainDAdapt = Me.daspMainCase

        'single name = pg name, plural name = heading text
        enumConvers = New structHeadings("Conversation", "CONVERSATIONS")
        enumGrant = New structHeadings("Grant", "GRANTS or MGI Applications")
        enumRecommend = New structHeadings("Recommendation", "Resource RECOMMENDATIONS")

LoadCombos:
        modGlobalVar.LoadCRGCombo(Me.cboCRG)
        modGlobalVar.LoadCaseStatusCombo(Me.cboStatus, "Selectable") ', "Case")
        modGlobalVar.LoadStaffCombo(Me.cboMgr, False, StaffComboChoices.Selectable)
        tblConsultant = New DataTable("tCRGConsultant")
        modGlobalVar.LoadDataTable(tblConsultant, "SELECT ConsultantID, ConsultantName FROM luConsultant WHERE Type = 'CRG Resource Consultant'  ORDER BY ConsultantName")

FormSetup:
        'ENABLE DOUBLECLICK IN GRID
        modGlobalVar.EnableGridTextboxes(Me.grdMain)

        Me.cboCRGConsultant.DataSource = tblConsultant

        Forms.Add(Me)
        Me.ResumeLayout()
        isLoaded = True
        SetStatusBarText("Done ")

    End Sub

    'REFRESH DATA, COMBOS, AND GRIDS
    Public Sub ReLoad()  '
        SetStatusBarText("Reloading")

ResetVars:
        objHowClose = ObjClose.btnSaveExit
        Me.StatusBarPanelID.Text = mainTopic & " ID: " & ThisID.ToString
        Try 'for user undoing cbo - NOTE does not work here as dataset not filled yet
            '      iPrevStatus = Me.DsMainCase1.MainCase.FindByCaseID(ThisID).Item("StatusNum")
            ' iPrevStaff = Me.DsMainCase1.MainCase.FindByCaseID(ThisID).Item("CaseMgrNum")
        Catch ex As Exception
        End Try

FillGrid:
        Me.btnMGI.Visible = Not HideBtnMGI()
        FillSecondary()

FindFiles:
        'ADD RELATED FILES TO MENU
        modPopup.FindFiles(ThisID, LinkCasePath, ppFile, ehFile, Me.miOpenFile, Me.btnViewFile, My.Resources.btnAttached, Me.ToolTip1) ',nothing)
        ' modGlobalVar.Msg("out of reload")
        bDirty = False
        SetStatusBarText("Done")
    End Sub 'reload

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

        'miDELETE must be handled here for forms where row has already been removed so AnyChanges would fail
        If objHowClose = ObjClose.miDelete Then
            GoTo callupdate
        End If

LookForChanges:
        If bDirty = True Then
            bChanges = True
        Else
            bChanges = modGlobalVar.AnyChanges(ctlNeutral, mainBSrce, mainTbl)
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
                    ctl = arCtls(0)
                    'INSERT DEFAULT DATA
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
            ClassOpenForms.frmMainCase = Nothing 'reset global var
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
            i = mainDAdapt.Update(mainTbl)
            DoUpdate = True
        Catch ex As Exception
            modGlobalVar.Msg("ERROR: updating " & mainTopic, ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
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

        'CASE NAME
        ctl = ctlIdentify
        If Replace(Replace(Replace(ctl.Text, " ", ""), Chr(10), ""), Chr(13), "") = String.Empty Then
            Me.ErrorProvider1.SetError(ctl, "please enter a " & IsNull(ctl.Tag, ctl.Name) & " or 'Delete' if it is an unwanted entry")
            arCtls(i) = ctl
            i = i + 1
            ReDim Preserve arCtls(arCtls.GetUpperBound(0) + 1)
        Else
            Me.ErrorProvider1.SetError(ctl, "")
        End If

        'CASE MANAGER
        ctl = Me.cboMgr
        If modGlobalVar.ValidateBoundDD(ctl, True, Me.ErrorProvider1, ObjClose.btnSaveExit) = usrInput.OK Then
        Else
            'Me.ErrorProvider1.SetError(ctl, "please enter a " & IsNull(ctl.Tag, ctl.Name))
            arCtls(i) = ctl
            i = i + 1
            ReDim Preserve arCtls(arCtls.GetUpperBound(0) + 1)
        End If

        'CASE STATUS
        ctl = Me.cboStatus
        If modGlobalVar.ValidateBoundDD(ctl, True, Me.ErrorProvider1, ObjClose.btnSaveExit) = usrInput.OK Then
        Else
            '  Me.ErrorProvider1.SetError(ctl, "please enter a " & IsNull(ctl.Tag, ctl.Name))
            arCtls(i) = ctl
            i = i + 1
            ReDim Preserve arCtls(arCtls.GetUpperBound(0) + 1)
        End If

        'CRG
        ctl = Me.cboCRG
        If modGlobalVar.ValidateBoundDD(ctl, True, Me.ErrorProvider1, ObjClose.btnSaveExit) = usrInput.OK Then
        Else
            '  If ctl.Text = String.Empty Then
            ' Me.ErrorProvider1.SetError(ctl, "please enter a " & IsNull(ctl.Tag, ctl.Name))
            arCtls(i) = ctl
            i = i + 1
            ReDim Preserve arCtls(arCtls.GetUpperBound(0) + 1)
            ' Else
            '  Me.ErrorProvider1.SetError(ctl, "")
        End If

        arCtls(i) = ctlNeutral
        Return arCtls

    End Function 'reqd fields

#End Region 'update

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
            modGlobalVar.Msg("ERROR: cancelling changes ", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        SetStatusBarText("Changes Cancelled")
    End Sub

    'DELETE
    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles miDelete.Click

        If modGlobalVar.msg("CONFIRM DELETE", mainTopic & ": " & IsNull(ctlIdentify.Text, "") & NextLine & " will be marked for deletion and the window closed.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = MsgBoxResult.Yes Then
            ctlIdentify.Text = "DELETE: " & IsNull(ctlIdentify.Text, "")
            objHowClose = ObjClose.miDelete
            '  mainBSrce.EndEdit()
            Me.Close()
        End If
    End Sub

    'ENABLE MENU ITEMS based on TabControl selected
    Private Sub mmGoTo_Popup(sender As System.Object, e As System.EventArgs) Handles mmGoTo.Popup
        Me.miGotoConversation.Enabled = False
        Me.miGotoGrant.Enabled = False
        Me.miGotoRecommendation.Enabled = False

        Select Case Me.TabControl1.SelectedTab.Tag
            Case Is = enumConvers.PluralName
                miGotoConversation.Enabled = True
            Case Is = enumGrant.PluralName
                Me.miGotoGrant.Enabled = True
            Case Is = enumRecommend.PluralName
                Me.miGotoRecommendation.Enabled = True
            Case Else
                '  modGlobalVar.Msg(Me.TabControl1.SelectedTab.Name.ToString, , "clicked")
        End Select
    End Sub

    'SHOW HELP
    Private Sub miHelp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
    Handles miHelp.Click
        modGlobalVar.Msg("CASE HELP", "TO ADD NEW CASE:" & NextLine & "Go to Organization Edit window and click New button" _
        & NextLine & NextLine & " TO ATTACH A DOCUMENT RELATED TO THIS CASE: " & NextLine & "Click the Files button, or the menu items File --> Attach File" _
         & NextLine & NextLine & " TO OPEN A DOCUMENT RELATED TO THIS CASE: " & NextLine & "Click the Files button, or the menu items File --> View File.  If any documents have been attached, they will be listed.  Click the one you would like to open, and wait for the File Is Open message." _
         & NextLine & NextLine & " TO ADD NEW GRANT: go to Organization Detail window and click New" _
         & NextLine & NextLine & "Close date is based on Status; to change the date or remove it, use the status dropdown box.", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    'SHOW HELP SUBMENU
    Private Sub btnHelp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
  Handles btnHelp.Click
        SendKeys.Send("%(h)")
        'Me.mmHelp.PerformClick()'performselect do nothing!
    End Sub

    'CASE REPORT
    Private Sub miPrintConversation_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles miPrintConversation.Click

        MouseWait()
        Dim strb As New StringBuilder
        strb.Append("SELECT  tblCase.CaseID, tblConversation.ConversDate, tblConversation.Notes, tblOrg.OrgName, " _
        & " tblCase.CaseName, tblContact.FirstName, tblContact.Lastname,  tblConversation.BriefSummary, luStaff.StaffName AS CaseMgr " _
 & " FROM         tblCase LEFT OUTER JOIN luStaff ON tblCase.CaseMgrNum = luStaff.StaffID LEFT OUTER JOIN tblConversation ON tblCase.CaseID = tblConversation.CaseNum LEFT OUTER JOIN tblOrg ON tblCase.OrgNum = tblOrg.OrgID LEFT OUTER JOIN tblContact ON tblConversation.ContactNum = tblContact.ContactID" _
 & " WHERE    tblCase.CaseID = " & ThisID)

        strb.Append(" ORDER BY CaseID, tblConversation.ConversDate DESC")
        modPopup.PrintCaseConversation(strb.ToString)

        MouseDefault()
    End Sub

    'DISPLAY CASE STATUSes
    Private Sub miDefinitions_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles miDefinitions.Click
        modPopup.ShowDefinitions("Case")
    End Sub


#End Region    'menu items

#Region "Validation"

    'VALIDATE DATE
    Private Sub DateValidation(sender As Object, e As System.ComponentModel.CancelEventArgs) _
        Handles DtCaseOpen.Validating, DtCaseClose.Validating

        e.Cancel = modGlobalVar.ValidateDateA(sender, Me.ErrorProvider1)

    End Sub

    ' SET CASE CLOSED DATE
    Private Sub cboStatus_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cboStatus.SelectedIndexChanged
        If Not isLoaded Then
            GoTo GetPrev
        End If

        RemoveHandler cboStatus.SelectedIndexChanged, AddressOf cboStatus_SelectedIndexChanged
        '        strbTest.AppendLine(iPrevStatus & " " & cboStatus.Text & " " & cboStatus.SelectedIndex.ToString + "  selected Index Changed")

        If sender.Text = "Closed" Then
            If modGlobalVar.Msg("WARNING - Closing Case", "Are you sure you want to close this case?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                Try
                    Me.DtCaseClose.Text = CType(InputBox("Enter date case was closed, m/d/yyyy", "Close Date", Today), Date)
                Catch ex As Exception
                    Me.DtCaseClose.Text = Today
                End Try
            Else '--why does this trigger dirty?
                Me.cboStatus.SelectedIndex = iPrevStatus
            End If
        Else 'IS CASE BEING RE-OPENED?
            If CType(Me.DtCaseClose.Text, String) > "1/1/1911" Then

                If modGlobalVar.Msg("CONFIRM", "Are you sure you want to remove the Case Closed date?" & NextLine & "WARNING - This action will re-open the case.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    iPrevStatus = Me.cboStatus.SelectedIndex
                    mainDS.Tables(0).Rows(0)("CloseDate") = DBNull.Value
                    Me.cboStatus.SelectedIndex = iPrevStatus
                Else
                    Me.cboStatus.SelectedIndex = iPrevStatus
                End If
            End If
        End If

        AddHandler cboStatus.SelectedIndexChanged, AddressOf cboStatus_SelectedIndexChanged
GetPrev:
        iPrevStatus = Me.cboStatus.SelectedIndex 'Me.DsMainCase1.MainCase.FindByCaseID(ThisID).Item("StatusNum")
    End Sub

    'reset PrevStaff
    Private Sub cboMgr_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cboMgr.SelectedIndexChanged
        iPrevStaff = Me.cboMgr.SelectedIndex
    End Sub

    'VALIDATE CBO
    Private Sub CBO_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) _
        Handles cboStatus.Validating, cboCRG.Validating, cboMgr.Validating
        'NOTE: cancel here does not prevent user from leaving form ie. saving invalid data.
        Dim CheckInput As usrInput
        CheckInput = modGlobalVar.ValidateBoundDD(sender, True, Me.ErrorProvider1, ObjClose.CloseByControl)
        If CheckInput = usrInput.OK Then
            e.Cancel = False
        Else
            e.Cancel = True
            sender.DroppedDown = True
        End If

    End Sub

    'CALL REMOVE LINE FEEDS
    Private Sub txtCaseName_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) _
     Handles TxtCaseName.LostFocus
        modGlobalVar.RemoveLineFeeds(sender)
    End Sub

#End Region  'validation

#Region "ADD NEW ITEM"

    'POPUP ADD NEW Case, Contact, Grant
    Private Sub PopupNew(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles miNew.Click, btnNew.Click

        ''INSERT NEW ITEM to BACKEND and OPEN DETAIL FORM
        Dim str As String
        SetStatusBarText("Adding new Conversation") ' & obj.text
        If modGlobalVar.Msg("About to enter a new Conversation", "Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then ' & obj.text) = DialogBoxResult.No Then
            Exit Sub
        End If
        Dim c As Integer

        If (Me.cboCRG.SelectedIndex > -1) Then
            c = Me.cboCRG.SelectedValue
        Else
            c = 0
        End If
        str = "INSERT INTO tblConversation(CaseNum, OrgNum, StaffNum,  ConversDate, CaseRating, CRGNum) VALUES (" & ThisID & ", " & LocalOrgID & ", " & usr & ",'" & Now & "', " & IsNull(Me.lblCurrentCaseRating.Text, 0) & ", " & c & "); SELECT @@Identity"

InsertNewItem:
        If Not SCConnect() Then
            Exit Sub
        End If

        '  Dim myTrans As SqlClient.SqlTransaction = sc.BeginTransaction()
        Dim cmdInsert As New SqlCommand(str, sc)   '(str, sc, myTrans)
        Dim newID As Integer
        Try
            newID = cmdInsert.ExecuteScalar() 'CommandBehavior.CloseConnection)
            '     myTrans.Commit()
        Catch ex As Exception
            modGlobalVar.Msg("ERROR: insert conversation from case", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
            '    myTrans.Rollback()
            ' Exit Sub
        Finally
            sc.Close()
        End Try

OpenForm:
        Try
            modGlobalVar.OpenMainConversation(newID, "Entering new conversation in Case: " & Me.TxtCaseName.Text, Me.fldGotoOrg.Text, LocalOrgID)
        Catch ex As System.Exception
            modGlobalVar.Msg("ERROR: opening conversation ", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        SetStatusBarText("Done")
    End Sub

    'CREATE NEW MGI
    Private Sub btnMGI_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMGI.Click
        ppMGI.Show(Me, Me.PointToClient(Control.MousePosition))
    End Sub

    'MGI BUtton visibility
    Private Function HideBtnMGI() As Boolean
        ' FIND MGI APPLs and CURRENT MGI
        ' 1) BUTTON VISIBLE if there are either
        ' 2) LOAD POPUP MENU
        '    a) Goto mgi appl
        '    b) current MGI: flag if none for create option

        Dim bFound As Boolean = False 'there is a current MGI and it has already been started
        Dim dr As SqlDataReader

        Dim cmdFindMGIs As New SqlCommand("[GetGrants2]", sc)
        cmdFindMGIs.CommandType = CommandType.StoredProcedure
        cmdFindMGIs.Parameters.Add("@What", SqlDbType.VarChar).Value = "MGI"
        cmdFindMGIs.Parameters.Add("@IDFld", SqlDbType.VarChar).Value = "CaseNum"
        cmdFindMGIs.Parameters.Add("@IDVal", SqlDbType.Int).Value = ThisID

        ppMGI.MenuItems.Clear()
        If Not SCConnect() Then
            GoTo CloseAll
        End If

        'CURRENT APPLICATIONS
        Try
            dr = cmdFindMGIs.ExecuteReader
            If dr.HasRows Then
                'load popup and set Create option
                While dr.Read() 'GIID, TypeofGrant, IsCurrentMGI
                    If IsDBNull(dr("IsCurrentMGI")) Then
                    Else
                        If dr.GetInt16(dr.GetOrdinal("IsCurrentMGI")) > 0 Then
                            bFound = True
                        End If
                    End If
                    ppMGI.MenuItems.Add(dr.GetString(dr.GetOrdinal("TypeofGrant")) & " # " & CType(dr.GetInt32(dr.GetOrdinal("GIID")), String), ehMGI)
                    HideBtnMGI = False
                End While
            Else
                HideBtnMGI = True
            End If

            'LOOK FOR ACTIVE MGI Grant Type IF ONE DOESN'T EXIST
            If bFound = True Then 'MGI has already been started
            Else
                dr.NextResult()
                If dr.HasRows Then
                    '  ppMGI.MenuItems.Add("CREATE NEW MGI APPLICATION TRACKER")
                    While dr.Read()
                        ppMGI.MenuItems.Add(dr.GetString(dr.GetOrdinal("MGIName")) & " - CREATE new tracking form", ehMGI)
                    End While
                    HideBtnMGI = False
                Else
                    HideBtnMGI = True
                End If
            End If

        Catch ex As Exception
            modGlobalVar.msg("QUERY ERROR", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
            HideBtnMGI = True
        End Try

closeall:
        sc.Close()
    End Function

    'POPUP menu for MGI open or create
    Private Sub ehGotoMGI(ByVal obj As Object, ByVal ea As EventArgs)

        Dim i As Integer
        Dim str As String = obj.text.substring(0, 4) 'Replace(Replace(obj.text, "Go to ", ""), "CREATE new ", "")
        'CREATE NEW MGI GrantApp
        If obj.text.Contains("CREATE new") Then
            SetStatusBarText("Adding new MGI Application Tracker") ' & obj.text
            If modGlobalVar.msg("About to enter a new " & str & " Application", "Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then
                Exit Sub
            End If
            'create new

            If Not SCConnect() Then
                Exit Sub
            End If
            'TODO REMOVE hard coding of MGI type
            Dim cmdInsert As New SqlClient.SqlCommand("[InsertCMGGrant]", sc)
            cmdInsert.CommandType = CommandType.StoredProcedure
            cmdInsert.Parameters.Add("@CaseNum", SqlDbType.Int).Value = ThisID

            '  Dim newID As Integer
            Try
                i = cmdInsert.ExecuteScalar() 'CommandBehavior.CloseConnection)
                '   modGlobalVar.OpenMainCMG(i, Me.fldGotoOrg.Text, LocalOrgID)
            Catch ex As Exception
                modGlobalVar.msg("ERROR: new CMG application", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                sc.Close()
            End Try
        Else
            i = CType(obj.text.ToString.Substring(obj.text.ToString.LastIndexOf(" ")), Integer)
        End If

        ' OPEN FORM
        If i > 0 Then
            modGlobalVar.OpenMGIForm(i, str, Me.fldGotoOrg.Text, Me.fldOrgNum.Text)
        Else
        End If

        i = Nothing
        str = Nothing

    End Sub

#End Region 'add new item

#Region "Fill Datasets"

    'FILL GRID
    Public Sub FillSecondary()
        Dim cmd As New SqlCommand(" ", sc)
        SetStatusBarText("Retrieving data...")
        MouseWait()

        Me.lblCurrentCaseRating.Text = modPopup.GetLatestCaseRating(ThisID)
        ctlIdentify.Focus()

        cmd.Parameters.Add("@IDVal", System.Data.SqlDbType.Int).Value = ThisID
        cmd.Parameters.Add("@IDFld", System.Data.SqlDbType.VarChar, 30)
        cmd.CommandType = System.Data.CommandType.StoredProcedure

        Try
            tbl.Clear()
            tbl.Columns.Clear()
        Catch ex As System.Exception
        End Try
        'modGlobalVar.Msg("T" & Me.TabControl1.SelectedTab.Tag& NextLine & 
        '             "E" & enumConvers.PluralName& NextLine & 
        '             "X" & enumGrant.PluralName, , "in fill secondary")

        Select Case Me.TabControl1.SelectedTab.Tag
            Case Is = enumConvers.PluralName    'conversation
                cmd.Parameters("@IDFld").Value = "Case"
                cmd.CommandText = "[GetConversations]"
                tbl = New DataTable("tConversation")
            Case Is = enumGrant.PluralName  'grant
                cmd.Parameters("@IDFld").Value = "CaseNum"
                cmd.CommandText = "[GetGrants]"
                tbl = New DataTable("tGrant")
            Case Is = enumRecommend.PluralName   'recommendation
                cmd.Parameters("@IDFld").Value = "Case"
                cmd.CommandText = "[getResRecommendation]"
                tbl = New DataTable("tRecommend")
            Case Else
                MsgBox("selected Tag " & Me.TabControl1.SelectedTab.Tag & NextLine &
                      "Convers.plural" & enumConvers.PluralName & NextLine &
                      "Grant.plural" & enumGrant.PluralName, , "not found here")
                GoTo CloseAll
        End Select

        If Not SCConnect() Then
            GoTo CloseAll
        End If
        Try
            tbl.Load(cmd.ExecuteReader(CommandBehavior.CloseConnection))
            'ASSIGN DATAVIEW
            '  strbActiveGrid.Append(TabControl1.SelectedTab.Text)    'use this for doubleclick code
            dv = New DataView(tbl) 'dv needed for filtering
            Me.grdMain.DataSource = tbl
        Catch ex As Exception
            modGlobalVar.Msg("ERROR - can't fill " + TabControl1.SelectedTab.Tag, ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
            GoTo CloseAll
        End Try

        SetTabCaptions()

CloseAll:
        SetStatusBarText("Done")
        MouseDefault()
    End Sub 'fill secondary

    'RESET TAB CAPTIONS w  COUNT
    Public Sub SetTabCaptions()

        Dim cmdCntID As New SqlCommand("", sc)
        Dim i As Integer = 0

        'RESET SELECTED ITEM INDICATOR
        modGlobalVar.ClearIDLbls(Me.lblSelectedID, Me.lblSelectedWhat)

        If Not SCConnect() Then
            Exit Sub
        End If

        'COUNT CONVERSATIONS
        cmdCntID.CommandText = "SELECT COUNT(ConversID) as NumConvers FROM tblConversation WHERE CaseNum = " & ThisID
        Try
            i = cmdCntID.ExecuteScalar()
        Catch ex As Exception
        End Try
        Me.TabControl1.TabPages("pg" & enumConvers.SingleName).Text = i.ToString() & "  " & enumConvers.PluralName

        'COUNT GRANTS & GRANT APPS
        i = 0
        cmdCntID.CommandText = " SELECT        SUM(NumGrants) AS Expr1 FROM     (SELECT        COUNT(GrantIDTxt) AS NumGrants   FROM            tblGrant" _
          & " WHERE        (CaseNum = " & ThisID & ")  UNION  SELECT   COUNT(GrantIDtxt) AS Expr1 FROM vwGetMGIApps" _
          & " WHERE        (CaseNum = " & ThisID & ")) AS t1"
        Try
            i = cmdCntID.ExecuteScalar()
        Catch ex As Exception
            modGlobalVar.Msg("QUERY ERROR", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Me.TabControl1.TabPages("pg" & enumGrant.SingleName).Text = i.ToString() & "  " & enumGrant.PluralName

        'COUNT RECOMMENDATIONS
        cmdCntID.CommandText = "SELECT COUNT(RecommendID) FROM tblResourceRecommend WHERE CaseNum = " & ThisID
        i = 0
        Try
            i = cmdCntID.ExecuteScalar()
        Catch ex As Exception
        End Try
        '  Me.TabControl1.TabPages(2).Text = i.ToString() & "  " & enumRecommend.PluralName
        Me.TabControl1.TabPages("pg" & enumRecommend.SingleName).Text = i.ToString() & "  " & enumRecommend.PluralName

        sc.Close()

    End Sub 'set tab captions

    'CALL FILL SECONDARY from TAB 
    Private Sub TabControl1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles TabControl1.SelectedIndexChanged
        '  If isLoaded Then    'SelectedIndexChange occurs before load!!
        Me.grdMain.Text = ""
        FillSecondary()
        ' End If
    End Sub

#End Region   'fill datasets

#Region "Datagrid"

    'CELL CHANGE - GET NEW ID, HIGHLIGHT ROW
    Private Sub grdMain_CurrentCellChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles grdMain.CurrentCellChanged

        'DISPLAY SELECTED ITEM ID LABEL
        Me.lblSelectedID.Text = Me.grdMain.Item(grdMain.CurrentCell.RowNumber, 0)
        Me.lblSelectedWhat.Text = Me.TabControl1.SelectedTab.Name.Substring(2) & " ID:"
        '   TODO should be this instead of assume 1st col holds id?
        'i = Me.grdMain.TableStyles("GetWRegistrations").GridColumnStyles.IndexOf(Me.grdMain.TableStyles("GetWRegistrations").GridColumnStyles("RegistrationID"))
        'HIGHLIGHT SELECTED ROW
        Me.grdMain.Select(grdMain.CurrentCell.RowNumber)

    End Sub

    'CAPTURE RIGHT MOUSE CLICK TO FILTER APPROPRIATE GRID
    Protected Sub grdAll_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) _
            Handles grdMain.MouseDown

        Dim strHdr As String    'text for grid header
        hti = sender.HitTest(e.X, e.Y)

IfRightMouseclick:
        If e.Button = Windows.Forms.MouseButtons.Right Then
            strHdr = Me.TabControl1.SelectedTab.Tag   'strDGM
SetOrClearFilter:
            If hti.Type = DataGrid.HitTestType.Cell Then    'SET FILTER
                If IsDBNull(sender.Item(hti.Row, hti.Column)) Then 'nulls cause filter error
                    modGlobalVar.msg(MsgCodes.filterEmpty)
                    Exit Sub
                Else
                    grdFilter(sender, strHdr)
                End If
            Else    'not in cell, CLEAR FILTER
                Me.grdMain.DataSource = tbl ''removes dv.rowfilter
                Me.lblSelectedID.Text = ""
                sender.CaptionText = tbl.Rows.Count.ToString & "  " & strHdr 'DsOrgSearch1.tblOrg.Rows.Count.ToString
                statusM = ""
                SetStatusBarText(statusM & " " & statusS1 & " " & statusS2)
            End If
IfLeftMouseClick:
        Else    'left mouse
            '  strbActiveGrid.Replace(strbActiveGrid.ToString, Me.TabControl1.SelectedTab.Tag)
        End If

    End Sub

    'FILTER METHOD
    Private Sub grdFilter(ByVal grd As Object, ByVal strHdr As String) ', ByVal dv As DataView)
        '  Private Sub grdFilter(ByVal grd As Object, ByVal tbl As DataTable, ByVal strHdr As String, ByVal dv As DataView)
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
            ' modGlobalVar.Msg("Cancelling Request", "select a row in the grid", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            modGlobalVar.msg(MsgCodes.noRowSelected)
            Exit Sub
        End If
        Dim gotoOrgName As String = Me.fldGotoOrg.Text
        Dim cri As Integer = Me.grdMain.CurrentRowIndex
        MouseWait()
        SetStatusBarText("opening " & TabControl1.SelectedTab.Tag & "...")

        Select Case Me.lblSelectedWhat.Text.Replace(" ID:", String.Empty)
            Case Is = enumConvers.SingleName
                Dim str As String
                If IsDBNull(Me.grdMain.Item(cri, 5)) Then
                    str = ""
                Else
                    str = SubstrBriefSummary(Me.grdMain.Item(cri, 5))
                End If
                modGlobalVar.OpenMainConversation(Me.lblSelectedID.Text, str, gotoOrgName, LocalOrgID)
            Case Is = enumGrant.SingleName 'grant or MGI
                'TODO remove hard coded MGI
                If Me.grdMain.Item(cri, 3).ToString.Contains("Application") Then

                    If Me.grdMain.Item(cri, 0).ToString.Contains("LTGI") Then
                        modGlobalVar.OpenMainLTGI(Me.grdMain.Item(cri, 1), gotoOrgName, LocalOrgID)
                    ElseIf Me.grdMain.Item(cri, 0).ToString.Contains("TGI") Then
                        modGlobalVar.OpenMainTGI(Me.grdMain.Item(cri, 1), gotoOrgName, LocalOrgID)
                    ElseIf Me.grdMain.Item(cri, 0).ToString.Contains("TMGI") Then
                        modGlobalVar.OpenMainTMGI(Me.grdMain.Item(cri, 1), gotoOrgName, LocalOrgID)
                    ElseIf Me.grdMain.Item(cri, 0).ToString.Contains("YMGI") Then
                        modGlobalVar.OpenMainYMGI(Me.grdMain.Item(cri, 1), gotoOrgName, LocalOrgID)
                    ElseIf Me.grdMain.Item(cri, 0).ToString.Contains("CMG ") Then 'note space for cmg vs older cmgi
                        modGlobalVar.OpenMainCMG(Me.grdMain.Item(cri, 1), gotoOrgName, LocalOrgID)

                    Else
                        modGlobalVar.msg("Archived Information", "Sacred Space and Computers & Ministry Application forms outdated" & NextLine & "see" & DBAdmin.StaffName & " if you need that information.", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                Else
                    Try
                        modGlobalVar.OpenMainGrant(Me.lblSelectedID.Text, "Grant", gotoOrgName, LocalOrgID)

                    Catch ex As System.Exception
                        modGlobalVar.msg("ERROR: opening grant form ", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try
                End If
            Case Is = enumRecommend.SingleName
                modGlobalVar.OpenMainRecommend(Me.lblSelectedID.Text, IsNull(Me.grdMain.Item(cri, 6), "unknown resource"), Me.grdMain.Item(cri, 10))
            Case Else
                ' modGlobalVar.Msg(Me.lblSelectedWhat.Text.Replace(" ID:", String.Empty), , "Unable to comply")
        End Select

        SetStatusBarText("Done")
        MouseDefault()
    End Sub

    'CALL OPEN SECONDARY FORMS from MENU ITEMS
    Private Sub miGoto_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles miGotoConversation.Click, miGotoGrant.Click, miGotoRecommendation.Click
        OpenForms()
    End Sub

    'OPEN ORG DETAIL FORM - see module
    Private Sub OpenOrg(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles fldGotoOrg.DoubleClick, miGotoOrg.Click ', btnGotoOrg.Click

        SetStatusBarText("Opening Organization Detail window")
        modGlobalVar.OpenMainOrg(LocalOrgID, Me.fldGotoOrg.Text)
        SetStatusBarText("Done")
    End Sub

#End Region    'open secondary forms

#Region "General"

    'SET STATUS BAR LEFT TEXT
    Private Sub SetStatusBarText(ByVal str As String)
        Me.StatusBarPanel1.Text = str
    End Sub

    'COPY CURRENT ID
    Private Sub StatusBar1_DoubleClick(sender As System.Object, e As System.EventArgs) Handles StatusBar1.DoubleClick
        Clipboard.SetText(ThisID)
    End Sub

    'UNDO
    Protected Overrides Function ProcessDialogKey(ByVal keyData As Keys) As Boolean
        If keyData = System.Windows.Forms.Keys.Escape Then
            modPopup.UndoCtl(Me.ActiveControl)
            Return True  ' True means we've processed the key
        Else
            Return MyBase.ProcessDialogKey(keyData) 'fails on Tab when have validation on Leave method of combobox
        End If
    End Function

    'CRG Filter
    Private Sub cboCRG_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) _
            Handles cboCRG.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Right Then
            modPopup.SearchCRG(Me, PointToClient(Control.MousePosition), Me.cboCRG)
        End If

    End Sub

    'force cboCRG dirty
    Private Sub cboCRG_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cboCRG.SelectedIndexChanged
        If isLoaded Then
            bDirty = True 'else mystery won't save if is only change
        End If
    End Sub

    'RIGHT CLICK MENU
    Private Sub textbox_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) _
        Handles TxtCaseName.MouseDown, txtDescription.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Right Then
            Dim pp As New ClassRtbContextMenu(sender)
            pp.Show(Me, PointToClient(Control.MousePosition))
        End If
    End Sub

#End Region 'general

#Region "Attach Files"

    'COPY FILE to shared drive
    Private Sub btnAttach_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles miAttach.Click

        Try
            modPopup.AttachFiles("Case", LinkCasePath, ThisID)
        Catch ex As Exception
            modGlobalVar.Msg("error attach case files", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        modPopup.FindFiles(ThisID, LinkCasePath, ppFile, ehFile, Me.miOpenFile, Me.btnViewFile, My.Resources.btnAttached, Me.ToolTip1, Nothing)

        SetStatusBarText("Done")

    End Sub

    'OPEN FILE
    Private Sub ehOpenFile(ByVal obj As Object, ByVal ea As EventArgs)

        If obj.Text = "Attach File" Then
            Me.miAttach.PerformClick()
            Exit Sub
        End If

        If OpenFile(modPopup.GetFileName(LinkCasePath, ThisID.ToString & " " & obj.text & ".*", True)) Then
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

#Region "MERGE Feedback"

    Private Sub miRequestFeedback_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles miRequestFeedback.Click, btnFeedback.Click

        Dim datafileName As String = "DataDoc"
        Dim sheetLabel As String = "FeedbackRequest"

        If Me.chkResourceReqd.Checked = True Then
            modGlobalVar.Msg("cancelling your request", "no resources were required is checked", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
        MouseWait()
        Dim i As Integer = ThisID
        If StrmWriter("[MergeLetter]", i, datafileName, "CaseRequestFeedback") Then
            If modPopup.DataToExcel(datafileName, sheetLabel) = String.Empty Then
                modGlobalVar.Msg("ERROR DataToExcel", "MergeLetter could not create datafile", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Else
                modPopup.MergePerform(SOPPath & "MergeInfoCtrtoWord\MergeRequestResourceEvaluation.dotx", datafileName, sheetLabel)
            End If
        End If
        MouseDefault()
        '   strDataDoc = Nothing
    End Sub

#End Region 'feedback

End Class

