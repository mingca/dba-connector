Option Explicit On
Imports System.Data.SqlClient
Imports System.Windows.Forms
Imports System.Text
Imports Microsoft.Office.Interop

Public Class frmMainConversation
    Inherits System.Windows.Forms.Form

    '  Dim bCancelClose As Boolean
    Public isLoaded As Boolean = False
    Dim tbl As DataTable
    Dim hti As System.Windows.Forms.DataGrid.HitTestInfo
    '  Dim gridMouseDownTime As DateTime
    Dim statusM, statusS1, statusS2 As String 'filter text for status bar
    Public bAskNoContact As Boolean = False
    Dim bOKNoContact As Boolean = False
    Dim bDate As Binding 'for deleting dates
    '===== for Detail form  closing routines
    Dim ctlIdentify As Control 'fields for delete and messages
    Dim ctlNeutral As Control 'will never be invalidated
    Dim objHowClose As Short ' structCloseMethod 'Object = Me.btnSaveExit 'identify object calling close
    Dim mainDS As DataSet 'for generic module calls like CloseDetailForm
    Dim mainTopic As String 'name of entity of this form - case, contact, etc
    Dim mainBSrce As System.Windows.Forms.BindingSource
    Dim mainTbl As DataTable
    Dim mainDAdapt As SqlDataAdapter
    Dim bDirty As Boolean 'crg combobox 

    Public ThisID, LocalOrgID As Integer

    '======
    ' Dim badfield As String

#Region "INITIALIZE"
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
    Friend WithEvents SqlCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents btnNew As System.Windows.Forms.Button
    Friend WithEvents StatusBarPanelID As System.Windows.Forms.StatusBarPanel
    Friend WithEvents DsMainConversation1 As InfoCtr.dsMainConversation
    Friend WithEvents MainConversationBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents miNew As System.Windows.Forms.MenuItem
    Friend WithEvents btnSaveExit As System.Windows.Forms.Button
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents fldOrgNum As System.Windows.Forms.Label
    Friend WithEvents fldCaseNum As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents MenuItem7 As System.Windows.Forms.MenuItem
    Friend WithEvents miNoteCopy As System.Windows.Forms.MenuItem
    Friend WithEvents miNoteCut As System.Windows.Forms.MenuItem
    Friend WithEvents miNotePaste As System.Windows.Forms.MenuItem
    Friend WithEvents miSelectAll As System.Windows.Forms.MenuItem
    Friend WithEvents miUndo As System.Windows.Forms.MenuItem
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents pgNotes As System.Windows.Forms.TabPage
    Friend WithEvents txtNotes As System.Windows.Forms.TextBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents pgRecommend As System.Windows.Forms.TabPage
    Friend WithEvents grdMain As System.Windows.Forms.DataGrid
    Friend WithEvents DataGridTableStyle1 As System.Windows.Forms.DataGridTableStyle
    Friend WithEvents DataGridTextBoxColumn24 As System.Windows.Forms.DataGridTextBoxColumn
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
    Friend WithEvents miRefresh As System.Windows.Forms.MenuItem
    Friend WithEvents lblSelectedID As System.Windows.Forms.TextBox
    Friend WithEvents lblSelectedWhat As System.Windows.Forms.Label

    Friend WithEvents btnHelp As System.Windows.Forms.Button
    Friend WithEvents ErrorProvider1 As System.Windows.Forms.ErrorProvider
    Friend WithEvents dtpDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents rtbNotes As System.Windows.Forms.RichTextBox
    Friend WithEvents MenuItem8 As System.Windows.Forms.MenuItem
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents StatusBar1 As System.Windows.Forms.StatusBar
    Friend WithEvents MainMenu1 As System.Windows.Forms.MainMenu
    Friend WithEvents MenuItem1 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem3 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem4 As System.Windows.Forms.MenuItem
    Friend WithEvents txtConverseDate As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cboCRG As InfoCtr.ComboBoxRelaxed
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents StatusBarPanel1 As System.Windows.Forms.StatusBarPanel
    Friend WithEvents StatusBarPanel2 As System.Windows.Forms.StatusBarPanel
    Friend WithEvents daspMainConversation As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents SqlSelectCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlUpdateCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents cboCase As InfoCtr.ComboBoxRelaxed
    Friend WithEvents cboStaff As InfoCtr.ComboBoxRelaxed 'System.Windows.Forms.ComboBox
    Friend WithEvents cboContact As InfoCtr.ComboBoxRelaxed 'System.Windows.Forms.ComboBox
    Friend WithEvents miClose As System.Windows.Forms.MenuItem
    Friend WithEvents miSave As System.Windows.Forms.MenuItem
    Friend WithEvents miCancel As System.Windows.Forms.MenuItem
    Friend WithEvents miDelete As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem2 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem5 As System.Windows.Forms.MenuItem
    Friend WithEvents miGotoOrg As System.Windows.Forms.MenuItem
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents fldGotoOrg As System.Windows.Forms.TextBox
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents txtBriefSummary As System.Windows.Forms.TextBox
    Friend WithEvents fldGotoCase As System.Windows.Forms.TextBox
    Friend WithEvents fldGotoContact As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents cboMode As InfoCtr.ComboBoxRelaxed 'System.Windows.Forms.ComboBox
    Friend WithEvents SqlSelectCommand2 As System.Data.SqlClient.SqlCommand
    ' Friend WithEvents daspContactNames As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents SqlSelectCommand3 As System.Data.SqlClient.SqlCommand
    Friend WithEvents grpRate As System.Windows.Forms.GroupBox
    Friend WithEvents Rate0 As System.Windows.Forms.RadioButton
    Friend WithEvents Rate2 As System.Windows.Forms.RadioButton
    Friend WithEvents Rate4 As System.Windows.Forms.RadioButton
    Friend WithEvents Rate5 As System.Windows.Forms.RadioButton
    Friend WithEvents Rate1 As System.Windows.Forms.RadioButton
    Friend WithEvents Rate3 As System.Windows.Forms.RadioButton
    Friend WithEvents txtCaseRating As System.Windows.Forms.TextBox
    Friend WithEvents txtImpression As System.Windows.Forms.TextBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMainConversation))
        Me.Label2 = New System.Windows.Forms.Label()
        Me.MainConversationBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.DsMainConversation1 = New InfoCtr.dsMainConversation()
        Me.StatusBar1 = New System.Windows.Forms.StatusBar()
        Me.StatusBarPanel1 = New System.Windows.Forms.StatusBarPanel()
        Me.StatusBarPanelID = New System.Windows.Forms.StatusBarPanel()
        Me.StatusBarPanel2 = New System.Windows.Forms.StatusBarPanel()
        Me.MainMenu1 = New System.Windows.Forms.MainMenu(Me.components)
        Me.MenuItem1 = New System.Windows.Forms.MenuItem()
        Me.miNew = New System.Windows.Forms.MenuItem()
        Me.miClose = New System.Windows.Forms.MenuItem()
        Me.MenuItem4 = New System.Windows.Forms.MenuItem()
        Me.miSave = New System.Windows.Forms.MenuItem()
        Me.miCancel = New System.Windows.Forms.MenuItem()
        Me.miDelete = New System.Windows.Forms.MenuItem()
        Me.miRefresh = New System.Windows.Forms.MenuItem()
        Me.MenuItem3 = New System.Windows.Forms.MenuItem()
        Me.MenuItem2 = New System.Windows.Forms.MenuItem()
        Me.MenuItem8 = New System.Windows.Forms.MenuItem()
        Me.MenuItem5 = New System.Windows.Forms.MenuItem()
        Me.miGotoOrg = New System.Windows.Forms.MenuItem()
        Me.MenuItem7 = New System.Windows.Forms.MenuItem()
        Me.miNoteCopy = New System.Windows.Forms.MenuItem()
        Me.miNoteCut = New System.Windows.Forms.MenuItem()
        Me.miNotePaste = New System.Windows.Forms.MenuItem()
        Me.miSelectAll = New System.Windows.Forms.MenuItem()
        Me.miUndo = New System.Windows.Forms.MenuItem()
        Me.txtBriefSummary = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtImpression = New System.Windows.Forms.TextBox()
        Me.fldGotoCase = New System.Windows.Forms.TextBox()
        Me.txtConverseDate = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.fldGotoContact = New System.Windows.Forms.TextBox()
        Me.daspMainConversation = New System.Data.SqlClient.SqlDataAdapter()
        Me.SqlCommand1 = New System.Data.SqlClient.SqlCommand()
        Me.SqlSelectCommand1 = New System.Data.SqlClient.SqlCommand()
        Me.SqlUpdateCommand1 = New System.Data.SqlClient.SqlCommand()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.fldGotoOrg = New System.Windows.Forms.TextBox()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.btnNew = New System.Windows.Forms.Button()
        Me.btnSaveExit = New System.Windows.Forms.Button()
        Me.txtNotes = New System.Windows.Forms.TextBox()
        Me.dtpDate = New System.Windows.Forms.DateTimePicker()
        Me.cboCase = New InfoCtr.ComboBoxRelaxed()
        Me.cboCRG = New InfoCtr.ComboBoxRelaxed()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.pgNotes = New System.Windows.Forms.TabPage()
        Me.rtbNotes = New System.Windows.Forms.RichTextBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.pgRecommend = New System.Windows.Forms.TabPage()
        Me.grdMain = New System.Windows.Forms.DataGrid()
        Me.DataGridTableStyle1 = New System.Windows.Forms.DataGridTableStyle()
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
        Me.DataGridTextBoxColumn24 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.txtCaseRating = New System.Windows.Forms.TextBox()
        Me.grpRate = New System.Windows.Forms.GroupBox()
        Me.Rate3 = New System.Windows.Forms.RadioButton()
        Me.Rate1 = New System.Windows.Forms.RadioButton()
        Me.Rate5 = New System.Windows.Forms.RadioButton()
        Me.Rate4 = New System.Windows.Forms.RadioButton()
        Me.Rate2 = New System.Windows.Forms.RadioButton()
        Me.Rate0 = New System.Windows.Forms.RadioButton()
        Me.cboMode = New InfoCtr.ComboBoxRelaxed()
        Me.cboStaff = New InfoCtr.ComboBoxRelaxed()
        Me.cboContact = New InfoCtr.ComboBoxRelaxed()
        Me.SqlSelectCommand2 = New System.Data.SqlClient.SqlCommand()
        Me.SqlSelectCommand3 = New System.Data.SqlClient.SqlCommand()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.fldOrgNum = New System.Windows.Forms.Label()
        Me.fldCaseNum = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.lblSelectedID = New System.Windows.Forms.TextBox()
        Me.lblSelectedWhat = New System.Windows.Forms.Label()
        Me.btnHelp = New System.Windows.Forms.Button()
        Me.ErrorProvider1 = New System.Windows.Forms.ErrorProvider(Me.components)
        CType(Me.MainConversationBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsMainConversation1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.StatusBarPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.StatusBarPanelID, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.StatusBarPanel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.pgNotes.SuspendLayout()
        Me.pgRecommend.SuspendLayout()
        CType(Me.grdMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpRate.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label2.Location = New System.Drawing.Point(10, 10)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(214, 21)
        Me.Label2.TabIndex = 164
        Me.Label2.Text = "CONVERSATION DETAIL"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'MainConversationBindingSource
        '
        Me.MainConversationBindingSource.DataMember = "MainConversation"
        Me.MainConversationBindingSource.DataSource = Me.DsMainConversation1
        '
        'DsMainConversation1
        '
        Me.DsMainConversation1.DataSetName = "dsMainConversation"
        Me.DsMainConversation1.Locale = New System.Globalization.CultureInfo("en-US")
        Me.DsMainConversation1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'StatusBar1
        '
        Me.StatusBar1.Location = New System.Drawing.Point(0, 591)
        Me.StatusBar1.Name = "StatusBar1"
        Me.StatusBar1.Panels.AddRange(New System.Windows.Forms.StatusBarPanel() {Me.StatusBarPanel1, Me.StatusBarPanelID, Me.StatusBarPanel2})
        Me.StatusBar1.ShowPanels = True
        Me.StatusBar1.Size = New System.Drawing.Size(926, 22)
        Me.StatusBar1.TabIndex = 166
        Me.StatusBar1.Text = "StatusBar1"
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
        Me.StatusBarPanelID.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents
        Me.StatusBarPanelID.MinWidth = 200
        Me.StatusBarPanelID.Name = "StatusBarPanelID"
        Me.StatusBarPanelID.Text = "Conversation ID"
        Me.StatusBarPanelID.Width = 200
        '
        'StatusBarPanel2
        '
        Me.StatusBarPanel2.Alignment = System.Windows.Forms.HorizontalAlignment.Right
        Me.StatusBarPanel2.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring
        Me.StatusBarPanel2.BorderStyle = System.Windows.Forms.StatusBarPanelBorderStyle.None
        Me.StatusBarPanel2.Name = "StatusBarPanel2"
        Me.StatusBarPanel2.Text = "Use this window to edit Conversation details."
        Me.StatusBarPanel2.Width = 509
        '
        'MainMenu1
        '
        Me.MainMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem1, Me.MenuItem4, Me.MenuItem3, Me.MenuItem2})
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
        Me.miNew.Text = "New Recommendation"
        '
        'miClose
        '
        Me.miClose.Index = 1
        Me.miClose.Text = "Close Window"
        '
        'MenuItem4
        '
        Me.MenuItem4.Index = 1
        Me.MenuItem4.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.miSave, Me.miCancel, Me.miDelete, Me.miRefresh})
        Me.MenuItem4.Text = "Edit"
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
        Me.miDelete.Text = "Delete Conversation"
        '
        'miRefresh
        '
        Me.miRefresh.Index = 3
        Me.miRefresh.Text = "Refresh Grid"
        '
        'MenuItem3
        '
        Me.MenuItem3.Index = 2
        Me.MenuItem3.Text = "Reports"
        '
        'MenuItem2
        '
        Me.MenuItem2.Index = 3
        Me.MenuItem2.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem8, Me.MenuItem5, Me.miGotoOrg})
        Me.MenuItem2.Text = "Goto"
        '
        'MenuItem8
        '
        Me.MenuItem8.Index = 0
        Me.MenuItem8.Text = "Case"
        '
        'MenuItem5
        '
        Me.MenuItem5.Index = 1
        Me.MenuItem5.Text = "Contact"
        '
        'miGotoOrg
        '
        Me.miGotoOrg.Index = 2
        Me.miGotoOrg.Text = "Organization"
        '
        'MenuItem7
        '
        Me.MenuItem7.Index = -1
        Me.MenuItem7.Text = "---------------------"
        '
        'miNoteCopy
        '
        Me.miNoteCopy.Enabled = False
        Me.miNoteCopy.Index = -1
        Me.miNoteCopy.Text = "Copy          (Ctrl + c)"
        '
        'miNoteCut
        '
        Me.miNoteCut.Enabled = False
        Me.miNoteCut.Index = -1
        Me.miNoteCut.Text = "Cut            (Ctrl + x)"
        '
        'miNotePaste
        '
        Me.miNotePaste.Enabled = False
        Me.miNotePaste.Index = -1
        Me.miNotePaste.Text = "Paste         (Ctrl + v)"
        '
        'miSelectAll
        '
        Me.miSelectAll.Enabled = False
        Me.miSelectAll.Index = -1
        Me.miSelectAll.Text = "Select All    (Ctrl + a)"
        '
        'miUndo
        '
        Me.miUndo.Enabled = False
        Me.miUndo.Index = -1
        Me.miUndo.Text = "Undo          (Ctrl + z)"
        '
        'txtBriefSummary
        '
        Me.txtBriefSummary.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainConversationBindingSource, "BriefSummary", True))
        Me.txtBriefSummary.Location = New System.Drawing.Point(72, 216)
        Me.txtBriefSummary.MaxLength = 300
        Me.txtBriefSummary.Multiline = True
        Me.txtBriefSummary.Name = "txtBriefSummary"
        Me.txtBriefSummary.Size = New System.Drawing.Size(275, 40)
        Me.txtBriefSummary.TabIndex = 6
        Me.txtBriefSummary.Tag = "BRIEF SUMMARY"
        Me.txtBriefSummary.Text = "txtbriefsummary"
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Location = New System.Drawing.Point(15, 64)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(89, 16)
        Me.Label5.TabIndex = 169
        Me.Label5.Text = "Mode of contact"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Location = New System.Drawing.Point(17, 398)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(60, 14)
        Me.Label4.TabIndex = 166
        Me.Label4.Text = "Impression"
        '
        'txtImpression
        '
        Me.txtImpression.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainConversationBindingSource, "Impression", True))
        Me.txtImpression.Location = New System.Drawing.Point(79, 397)
        Me.txtImpression.Multiline = True
        Me.txtImpression.Name = "txtImpression"
        Me.txtImpression.Size = New System.Drawing.Size(265, 63)
        Me.txtImpression.TabIndex = 9
        '
        'fldGotoCase
        '
        Me.fldGotoCase.BackColor = System.Drawing.SystemColors.Control
        Me.fldGotoCase.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fldGotoCase.ForeColor = System.Drawing.Color.ForestGreen
        Me.fldGotoCase.Location = New System.Drawing.Point(9, 172)
        Me.fldGotoCase.Name = "fldGotoCase"
        Me.fldGotoCase.ReadOnly = True
        Me.fldGotoCase.Size = New System.Drawing.Size(54, 20)
        Me.fldGotoCase.TabIndex = 162
        Me.fldGotoCase.TabStop = False
        Me.fldGotoCase.Tag = "Case"
        Me.fldGotoCase.Text = "Case"
        Me.fldGotoCase.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ToolTip1.SetToolTip(Me.fldGotoCase, "Doubleclick to open Case Detail window")
        '
        'txtConverseDate
        '
        Me.txtConverseDate.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainConversationBindingSource, "ConversDate", True))
        Me.txtConverseDate.Location = New System.Drawing.Point(276, 61)
        Me.txtConverseDate.Name = "txtConverseDate"
        Me.txtConverseDate.Size = New System.Drawing.Size(71, 20)
        Me.txtConverseDate.TabIndex = 0
        Me.txtConverseDate.Visible = False
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(100, 362)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(244, 22)
        Me.Label1.TabIndex = 152
        Me.Label1.Text = "           Technical       »       »       »       Adaptive"
        '
        'Label17
        '
        Me.Label17.BackColor = System.Drawing.Color.Transparent
        Me.Label17.Location = New System.Drawing.Point(25, 325)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(92, 27)
        Me.Label17.TabIndex = 151
        Me.Label17.Text = "Rate Case as of this conversation"
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.Color.Transparent
        Me.Label16.Location = New System.Drawing.Point(10, 216)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(56, 34)
        Me.Label16.TabIndex = 150
        Me.Label16.Text = "Brief Summary"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.Transparent
        Me.Label13.Location = New System.Drawing.Point(26, 282)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(40, 20)
        Me.Label13.TabIndex = 149
        Me.Label13.Text = "CRG"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.Transparent
        Me.Label12.Location = New System.Drawing.Point(23, 100)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(40, 24)
        Me.Label12.TabIndex = 148
        Me.Label12.Text = "Staff Caller"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'fldGotoContact
        '
        Me.fldGotoContact.BackColor = System.Drawing.SystemColors.Control
        Me.fldGotoContact.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fldGotoContact.ForeColor = System.Drawing.Color.ForestGreen
        Me.fldGotoContact.Location = New System.Drawing.Point(4, 137)
        Me.fldGotoContact.Margin = New System.Windows.Forms.Padding(0)
        Me.fldGotoContact.Name = "fldGotoContact"
        Me.fldGotoContact.ReadOnly = True
        Me.fldGotoContact.Size = New System.Drawing.Size(59, 20)
        Me.fldGotoContact.TabIndex = 147
        Me.fldGotoContact.TabStop = False
        Me.fldGotoContact.Tag = "Contact"
        Me.fldGotoContact.Text = "Contact"
        Me.fldGotoContact.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ToolTip1.SetToolTip(Me.fldGotoContact, "Doubleclick to open Contact Detail window")
        '
        'daspMainConversation
        '
        Me.daspMainConversation.DeleteCommand = Me.SqlCommand1
        Me.daspMainConversation.SelectCommand = Me.SqlSelectCommand1
        Me.daspMainConversation.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "MainConversation", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("ConversID", "ConversID"), New System.Data.Common.DataColumnMapping("OrgNum", "OrgNum"), New System.Data.Common.DataColumnMapping("CaseNum", "CaseNum"), New System.Data.Common.DataColumnMapping("ContactNum", "ContactNum"), New System.Data.Common.DataColumnMapping("ConversDate", "ConversDate"), New System.Data.Common.DataColumnMapping("StaffNum", "StaffNum"), New System.Data.Common.DataColumnMapping("ModeofContact", "ModeofContact"), New System.Data.Common.DataColumnMapping("BriefSummary", "BriefSummary"), New System.Data.Common.DataColumnMapping("CRGNum", "CRGNum"), New System.Data.Common.DataColumnMapping("Notes", "Notes"), New System.Data.Common.DataColumnMapping("Impression", "Impression"), New System.Data.Common.DataColumnMapping("CaseRating", "CaseRating"), New System.Data.Common.DataColumnMapping("ConversType", "ConversType"), New System.Data.Common.DataColumnMapping("Stamped", "Stamped")})})
        Me.daspMainConversation.UpdateCommand = Me.SqlUpdateCommand1
        '
        'SqlCommand1
        '
        Me.SqlCommand1.CommandText = "dbo.MainConversationDelete"
        Me.SqlCommand1.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlCommand1.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "ConversID", System.Data.DataRowVersion.Original, Nothing)})
        '
        'SqlSelectCommand1
        '
        Me.SqlSelectCommand1.CommandText = "dbo.MainConversation"
        Me.SqlSelectCommand1.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlSelectCommand1.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, False, CType(0, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing), New System.Data.SqlClient.SqlParameter("@ID", System.Data.SqlDbType.Int, 4)})
        '
        'SqlUpdateCommand1
        '
        Me.SqlUpdateCommand1.CommandText = "dbo.MainConversationUpdate"
        Me.SqlUpdateCommand1.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlUpdateCommand1.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, False, CType(0, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing), New System.Data.SqlClient.SqlParameter("@ID", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "ConversID", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@CaseNum", System.Data.SqlDbType.Int, 4, "CaseNum"), New System.Data.SqlClient.SqlParameter("@OrgNum", System.Data.SqlDbType.Int, 4, "OrgNum"), New System.Data.SqlClient.SqlParameter("@ContactNum", System.Data.SqlDbType.Int, 4, "ContactNum"), New System.Data.SqlClient.SqlParameter("@ConversDate", System.Data.SqlDbType.DateTime2, 4, "ConversDate"), New System.Data.SqlClient.SqlParameter("@StaffNum", System.Data.SqlDbType.Int, 4, "StaffNum"), New System.Data.SqlClient.SqlParameter("@ModeofContact", System.Data.SqlDbType.VarChar, 20, "ModeofContact"), New System.Data.SqlClient.SqlParameter("@BriefSummary", System.Data.SqlDbType.VarChar, 100, "BriefSummary"), New System.Data.SqlClient.SqlParameter("@CRGNum", System.Data.SqlDbType.SmallInt, 2, "CRGNum"), New System.Data.SqlClient.SqlParameter("@Notes", System.Data.SqlDbType.VarChar, 0, "Notes"), New System.Data.SqlClient.SqlParameter("@Impression", System.Data.SqlDbType.VarChar, 1000, "Impression"), New System.Data.SqlClient.SqlParameter("@CaseRating", System.Data.SqlDbType.TinyInt, 1, "CaseRating"), New System.Data.SqlClient.SqlParameter("@ConversType", System.Data.SqlDbType.VarChar, 30, "ConversType"), New System.Data.SqlClient.SqlParameter("@Original_Stamp", System.Data.SqlDbType.Timestamp, 8, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "Stamped", System.Data.DataRowVersion.Original, Nothing)})
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
        Me.fldGotoOrg.TabIndex = 212
        Me.fldGotoOrg.Tag = "Org"
        Me.fldGotoOrg.Text = "should be org name"
        Me.ToolTip1.SetToolTip(Me.fldGotoOrg, "Doubleclick to open Organization Detail window.")
        '
        'btnDelete
        '
        Me.btnDelete.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.btnDelete.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Image = Global.InfoCtr.My.Resources.Resources.btnDelete
        Me.btnDelete.ImageAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnDelete.Location = New System.Drawing.Point(44, 1)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(18, 35)
        Me.btnDelete.TabIndex = 202
        Me.btnDelete.Text = "Delete"
        Me.btnDelete.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.ToolTip1.SetToolTip(Me.btnDelete, "Delete This Conversation")
        Me.btnDelete.UseVisualStyleBackColor = False
        Me.btnDelete.Visible = False
        '
        'btnNew
        '
        Me.btnNew.BackColor = System.Drawing.Color.PaleGoldenrod
        Me.btnNew.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnNew.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnNew.Image = Global.InfoCtr.My.Resources.Resources.btnAddNew
        Me.btnNew.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.btnNew.Location = New System.Drawing.Point(2, 1)
        Me.btnNew.Margin = New System.Windows.Forms.Padding(0)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(40, 35)
        Me.btnNew.TabIndex = 201
        Me.btnNew.Text = "New"
        Me.btnNew.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.ToolTip1.SetToolTip(Me.btnNew, "Add new Recommendation")
        Me.btnNew.UseVisualStyleBackColor = False
        '
        'btnSaveExit
        '
        Me.btnSaveExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSaveExit.BackColor = System.Drawing.SystemColors.Control
        Me.btnSaveExit.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSaveExit.Image = Global.InfoCtr.My.Resources.Resources.btnSaveExit
        Me.btnSaveExit.ImageAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSaveExit.Location = New System.Drawing.Point(68, 1)
        Me.btnSaveExit.Name = "btnSaveExit"
        Me.btnSaveExit.Size = New System.Drawing.Size(40, 35)
        Me.btnSaveExit.TabIndex = 203
        Me.btnSaveExit.Text = "Close"
        Me.btnSaveExit.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.ToolTip1.SetToolTip(Me.btnSaveExit, "Saves any changes and Closes this window")
        Me.btnSaveExit.UseVisualStyleBackColor = False
        '
        'txtNotes
        '
        Me.txtNotes.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainConversationBindingSource, "Notes", True))
        Me.txtNotes.Location = New System.Drawing.Point(412, 111)
        Me.txtNotes.Multiline = True
        Me.txtNotes.Name = "txtNotes"
        Me.txtNotes.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtNotes.Size = New System.Drawing.Size(81, 271)
        Me.txtNotes.TabIndex = 0
        Me.ToolTip1.SetToolTip(Me.txtNotes, "Rightclick for Copy/Cut/Paste options.")
        '
        'dtpDate
        '
        Me.dtpDate.CustomFormat = "Mm/dd/yyyy h:mm tt"
        Me.dtpDate.DataBindings.Add(New System.Windows.Forms.Binding("Value", Me.MainConversationBindingSource, "ConversDate", True))
        Me.dtpDate.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainConversationBindingSource, "ConversDate", True))
        Me.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDate.Location = New System.Drawing.Point(69, 21)
        Me.dtpDate.MinDate = New Date(1911, 1, 1, 0, 0, 0, 0)
        Me.dtpDate.Name = "dtpDate"
        Me.dtpDate.Size = New System.Drawing.Size(179, 20)
        Me.dtpDate.TabIndex = 0
        Me.dtpDate.Tag = "ConversationDate"
        Me.ToolTip1.SetToolTip(Me.dtpDate, "Date of Conversation")
        '
        'cboCase
        '
        Me.cboCase.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboCase.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboCase.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.MainConversationBindingSource, "CaseNum", True))
        Me.cboCase.DisplayMember = "CaseName"
        Me.cboCase.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboCase.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cboCase.Location = New System.Drawing.Point(72, 171)
        Me.cboCase.Name = "cboCase"
        Me.cboCase.RestrictContentToListItems = True
        Me.cboCase.Size = New System.Drawing.Size(275, 21)
        Me.cboCase.TabIndex = 5
        Me.cboCase.Tag = "CASE NAME"
        Me.ToolTip1.SetToolTip(Me.cboCase, "Click or use arrow keys +enter to select item from this list.")
        Me.cboCase.ValueMember = "CaseID"
        '
        'cboCRG
        '
        Me.cboCRG.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboCRG.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboCRG.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.MainConversationBindingSource, "CRGNum", True))
        Me.cboCRG.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCRG.DropDownWidth = 500
        Me.cboCRG.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboCRG.ItemHeight = 12
        Me.cboCRG.Location = New System.Drawing.Point(69, 284)
        Me.cboCRG.MaxDropDownItems = 20
        Me.cboCRG.Name = "cboCRG"
        Me.cboCRG.RestrictContentToListItems = True
        Me.cboCRG.Size = New System.Drawing.Size(275, 20)
        Me.cboCRG.TabIndex = 7
        Me.cboCRG.Tag = "CRG"
        Me.ToolTip1.SetToolTip(Me.cboCRG, "Right click and enter partial CRG term to filter the dropdown possibilities")
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Location = New System.Drawing.Point(32, 25)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(31, 16)
        Me.Label8.TabIndex = 217
        Me.Label8.Text = "Date"
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel1.Controls.Add(Me.dtpDate)
        Me.Panel1.Controls.Add(Me.TabControl1)
        Me.Panel1.Controls.Add(Me.txtCaseRating)
        Me.Panel1.Controls.Add(Me.grpRate)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.txtImpression)
        Me.Panel1.Controls.Add(Me.cboMode)
        Me.Panel1.Controls.Add(Me.txtConverseDate)
        Me.Panel1.Controls.Add(Me.Label8)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.cboCase)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.fldGotoCase)
        Me.Panel1.Controls.Add(Me.cboStaff)
        Me.Panel1.Controls.Add(Me.cboContact)
        Me.Panel1.Controls.Add(Me.cboCRG)
        Me.Panel1.Controls.Add(Me.Label17)
        Me.Panel1.Controls.Add(Me.Label16)
        Me.Panel1.Controls.Add(Me.Label13)
        Me.Panel1.Controls.Add(Me.Label12)
        Me.Panel1.Controls.Add(Me.fldGotoContact)
        Me.Panel1.Controls.Add(Me.txtBriefSummary)
        Me.Panel1.Location = New System.Drawing.Point(17, 57)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(901, 473)
        Me.Panel1.TabIndex = 0
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.pgNotes)
        Me.TabControl1.Controls.Add(Me.pgRecommend)
        Me.TabControl1.Location = New System.Drawing.Point(387, 25)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(507, 441)
        Me.TabControl1.TabIndex = 221
        '
        'pgNotes
        '
        Me.pgNotes.Controls.Add(Me.rtbNotes)
        Me.pgNotes.Controls.Add(Me.Button1)
        Me.pgNotes.Location = New System.Drawing.Point(4, 22)
        Me.pgNotes.Name = "pgNotes"
        Me.pgNotes.Padding = New System.Windows.Forms.Padding(3)
        Me.pgNotes.Size = New System.Drawing.Size(499, 415)
        Me.pgNotes.TabIndex = 0
        Me.pgNotes.Tag = "NOTES"
        Me.pgNotes.Text = "Notes"
        Me.pgNotes.UseVisualStyleBackColor = True
        '
        'rtbNotes
        '
        Me.rtbNotes.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rtbNotes.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainConversationBindingSource, "Notes", True))
        Me.rtbNotes.Location = New System.Drawing.Point(7, 6)
        Me.rtbNotes.Name = "rtbNotes"
        Me.rtbNotes.Size = New System.Drawing.Size(489, 376)
        Me.rtbNotes.TabIndex = 221
        Me.rtbNotes.Text = ""
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(412, 384)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(72, 25)
        Me.Button1.TabIndex = 220
        Me.Button1.Text = "Spellcheck"
        Me.Button1.UseVisualStyleBackColor = True
        Me.Button1.Visible = False
        '
        'pgRecommend
        '
        Me.pgRecommend.Controls.Add(Me.grdMain)
        Me.pgRecommend.Location = New System.Drawing.Point(4, 22)
        Me.pgRecommend.Name = "pgRecommend"
        Me.pgRecommend.Padding = New System.Windows.Forms.Padding(3)
        Me.pgRecommend.Size = New System.Drawing.Size(499, 415)
        Me.pgRecommend.TabIndex = 1
        Me.pgRecommend.Tag = "RECOMMENDATIONS"
        Me.pgRecommend.Text = "Resource Recommendations"
        Me.pgRecommend.UseVisualStyleBackColor = True
        '
        'grdMain
        '
        Me.grdMain.BackgroundColor = System.Drawing.SystemColors.Control
        Me.grdMain.DataMember = ""
        Me.grdMain.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.grdMain.Location = New System.Drawing.Point(6, 3)
        Me.grdMain.Name = "grdMain"
        Me.grdMain.ReadOnly = True
        Me.grdMain.RowHeaderWidth = 20
        Me.grdMain.Size = New System.Drawing.Size(487, 406)
        Me.grdMain.TabIndex = 0
        Me.grdMain.TableStyles.AddRange(New System.Windows.Forms.DataGridTableStyle() {Me.DataGridTableStyle1})
        '
        'DataGridTableStyle1
        '
        Me.DataGridTableStyle1.AlternatingBackColor = System.Drawing.Color.FromArgb(CType(CType(204, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.DataGridTableStyle1.DataGrid = Me.grdMain
        Me.DataGridTableStyle1.GridColumnStyles.AddRange(New System.Windows.Forms.DataGridColumnStyle() {Me.DataGridTextBoxColumn8, Me.DataGridTextBoxColumn9, Me.DataGridTextBoxColumn10, Me.DataGridTextBoxColumn11, Me.DataGridTextBoxColumn12, Me.DataGridTextBoxColumn13, Me.DataGridTextBoxColumn14, Me.DataGridTextBoxColumn15, Me.DataGridTextBoxColumn16, Me.DataGridTextBoxColumn17, Me.DataGridTextBoxColumn24})
        Me.DataGridTableStyle1.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.DataGridTableStyle1.MappingName = "tRecommend"
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
        Me.DataGridTextBoxColumn13.HeaderText = "RecommendDate"
        Me.DataGridTextBoxColumn13.MappingName = "RecommendDate"
        Me.DataGridTextBoxColumn13.Width = 75
        '
        'DataGridTextBoxColumn14
        '
        Me.DataGridTextBoxColumn14.Format = ""
        Me.DataGridTextBoxColumn14.FormatInfo = Nothing
        Me.DataGridTextBoxColumn14.HeaderText = "Name"
        Me.DataGridTextBoxColumn14.MappingName = "ResourceName"
        Me.DataGridTextBoxColumn14.Width = 150
        '
        'DataGridTextBoxColumn15
        '
        Me.DataGridTextBoxColumn15.Format = ""
        Me.DataGridTextBoxColumn15.FormatInfo = Nothing
        Me.DataGridTextBoxColumn15.HeaderText = "ResourceType"
        Me.DataGridTextBoxColumn15.MappingName = "ResourceType"
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
        'DataGridTextBoxColumn24
        '
        Me.DataGridTextBoxColumn24.Format = ""
        Me.DataGridTextBoxColumn24.FormatInfo = Nothing
        Me.DataGridTextBoxColumn24.HeaderText = "OrgID"
        Me.DataGridTextBoxColumn24.MappingName = "OrgID"
        Me.DataGridTextBoxColumn24.Width = 75
        '
        'txtCaseRating
        '
        Me.txtCaseRating.BackColor = System.Drawing.SystemColors.Control
        Me.txtCaseRating.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainConversationBindingSource, "CaseRating", True))
        Me.txtCaseRating.Location = New System.Drawing.Point(321, 337)
        Me.txtCaseRating.Margin = New System.Windows.Forms.Padding(0)
        Me.txtCaseRating.MaxLength = 2
        Me.txtCaseRating.Name = "txtCaseRating"
        Me.txtCaseRating.Size = New System.Drawing.Size(10, 20)
        Me.txtCaseRating.TabIndex = 219
        Me.txtCaseRating.TabStop = False
        '
        'grpRate
        '
        Me.grpRate.Controls.Add(Me.Rate3)
        Me.grpRate.Controls.Add(Me.Rate1)
        Me.grpRate.Controls.Add(Me.Rate5)
        Me.grpRate.Controls.Add(Me.Rate4)
        Me.grpRate.Controls.Add(Me.Rate2)
        Me.grpRate.Controls.Add(Me.Rate0)
        Me.grpRate.Location = New System.Drawing.Point(110, 326)
        Me.grpRate.Name = "grpRate"
        Me.grpRate.Size = New System.Drawing.Size(205, 33)
        Me.grpRate.TabIndex = 8
        Me.grpRate.TabStop = False
        Me.grpRate.Text = "None     1         2        3        4        5 "
        '
        'Rate3
        '
        Me.Rate3.Location = New System.Drawing.Point(111, 15)
        Me.Rate3.Name = "Rate3"
        Me.Rate3.Size = New System.Drawing.Size(16, 15)
        Me.Rate3.TabIndex = 3
        '
        'Rate1
        '
        Me.Rate1.Location = New System.Drawing.Point(49, 15)
        Me.Rate1.Name = "Rate1"
        Me.Rate1.Size = New System.Drawing.Size(16, 15)
        Me.Rate1.TabIndex = 1
        '
        'Rate5
        '
        Me.Rate5.Location = New System.Drawing.Point(173, 15)
        Me.Rate5.Name = "Rate5"
        Me.Rate5.Size = New System.Drawing.Size(16, 15)
        Me.Rate5.TabIndex = 5
        '
        'Rate4
        '
        Me.Rate4.Location = New System.Drawing.Point(142, 15)
        Me.Rate4.Name = "Rate4"
        Me.Rate4.Size = New System.Drawing.Size(15, 15)
        Me.Rate4.TabIndex = 4
        '
        'Rate2
        '
        Me.Rate2.Location = New System.Drawing.Point(80, 15)
        Me.Rate2.Name = "Rate2"
        Me.Rate2.Size = New System.Drawing.Size(15, 15)
        Me.Rate2.TabIndex = 2
        '
        'Rate0
        '
        Me.Rate0.Location = New System.Drawing.Point(18, 15)
        Me.Rate0.Name = "Rate0"
        Me.Rate0.Size = New System.Drawing.Size(14, 14)
        Me.Rate0.TabIndex = 0
        '
        'cboMode
        '
        Me.cboMode.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboMode.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboMode.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.MainConversationBindingSource, "ModeofContact", True))
        Me.cboMode.DisplayMember = "StatusName"
        Me.cboMode.Location = New System.Drawing.Point(107, 60)
        Me.cboMode.Name = "cboMode"
        Me.cboMode.RestrictContentToListItems = True
        Me.cboMode.Size = New System.Drawing.Size(141, 21)
        Me.cboMode.TabIndex = 2
        Me.cboMode.Tag = "Mode of Conversation"
        Me.cboMode.ValueMember = "StatusName"
        '
        'cboStaff
        '
        Me.cboStaff.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboStaff.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboStaff.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.MainConversationBindingSource, "StaffNum", True))
        Me.cboStaff.Location = New System.Drawing.Point(72, 100)
        Me.cboStaff.Name = "cboStaff"
        Me.cboStaff.RestrictContentToListItems = True
        Me.cboStaff.Size = New System.Drawing.Size(275, 21)
        Me.cboStaff.TabIndex = 3
        Me.cboStaff.Tag = "STAFF NAME"
        '
        'cboContact
        '
        Me.cboContact.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboContact.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboContact.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.MainConversationBindingSource, "ContactNum", True))
        Me.cboContact.DisplayMember = "ContactName"
        Me.cboContact.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboContact.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cboContact.Location = New System.Drawing.Point(72, 137)
        Me.cboContact.Name = "cboContact"
        Me.cboContact.RestrictContentToListItems = True
        Me.cboContact.Size = New System.Drawing.Size(275, 21)
        Me.cboContact.TabIndex = 4
        Me.cboContact.Tag = "CONTACT NAME"
        Me.cboContact.ValueMember = "ContactID"
        '
        'SqlSelectCommand2
        '
        Me.SqlSelectCommand2.CommandText = "dbo.luContactNames"
        Me.SqlSelectCommand2.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlSelectCommand2.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, False, CType(0, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing), New System.Data.SqlClient.SqlParameter("@IDFld", System.Data.SqlDbType.VarChar, 30, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, ""), New System.Data.SqlClient.SqlParameter("@IDVal", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, "2300")})
        '
        'SqlSelectCommand3
        '
        Me.SqlSelectCommand3.CommandText = "[luCaseNames]"
        Me.SqlSelectCommand3.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlSelectCommand3.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, False, CType(0, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing), New System.Data.SqlClient.SqlParameter("@OrgID", System.Data.SqlDbType.Int, 4)})
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.SystemColors.Highlight
        Me.Panel2.Controls.Add(Me.btnNew)
        Me.Panel2.Controls.Add(Me.btnSaveExit)
        Me.Panel2.Controls.Add(Me.btnDelete)
        Me.Panel2.Location = New System.Drawing.Point(798, 5)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(111, 40)
        Me.Panel2.TabIndex = 217
        '
        'fldOrgNum
        '
        Me.fldOrgNum.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.fldOrgNum.BackColor = System.Drawing.SystemColors.Control
        Me.fldOrgNum.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainConversationBindingSource, "OrgNum", True))
        Me.fldOrgNum.Enabled = False
        Me.fldOrgNum.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fldOrgNum.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.fldOrgNum.Location = New System.Drawing.Point(605, 580)
        Me.fldOrgNum.Name = "fldOrgNum"
        Me.fldOrgNum.Size = New System.Drawing.Size(100, 13)
        Me.fldOrgNum.TabIndex = 223
        Me.fldOrgNum.Text = "ID"
        Me.fldOrgNum.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'fldCaseNum
        '
        Me.fldCaseNum.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.fldCaseNum.BackColor = System.Drawing.SystemColors.Control
        Me.fldCaseNum.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainConversationBindingSource, "CaseNum", True))
        Me.fldCaseNum.Enabled = False
        Me.fldCaseNum.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fldCaseNum.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.fldCaseNum.Location = New System.Drawing.Point(711, 580)
        Me.fldCaseNum.Name = "fldCaseNum"
        Me.fldCaseNum.Size = New System.Drawing.Size(100, 13)
        Me.fldCaseNum.TabIndex = 219
        Me.fldCaseNum.Text = "ID"
        Me.fldCaseNum.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label9
        '
        Me.Label9.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.SystemColors.ControlDark
        Me.Label9.Location = New System.Drawing.Point(711, 561)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(100, 15)
        Me.Label9.TabIndex = 221
        Me.Label9.Text = "Case #"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label11
        '
        Me.Label11.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.SystemColors.ControlDark
        Me.Label11.Location = New System.Drawing.Point(605, 561)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(100, 15)
        Me.Label11.TabIndex = 222
        Me.Label11.Text = "Org #"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblSelectedID
        '
        Me.lblSelectedID.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblSelectedID.BackColor = System.Drawing.SystemColors.Control
        Me.lblSelectedID.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lblSelectedID.Enabled = False
        Me.lblSelectedID.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSelectedID.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.lblSelectedID.Location = New System.Drawing.Point(817, 580)
        Me.lblSelectedID.Name = "lblSelectedID"
        Me.lblSelectedID.Size = New System.Drawing.Size(100, 13)
        Me.lblSelectedID.TabIndex = 224
        Me.lblSelectedID.Text = "ID"
        Me.lblSelectedID.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblSelectedWhat
        '
        Me.lblSelectedWhat.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblSelectedWhat.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSelectedWhat.ForeColor = System.Drawing.SystemColors.ControlDark
        Me.lblSelectedWhat.Location = New System.Drawing.Point(817, 561)
        Me.lblSelectedWhat.Name = "lblSelectedWhat"
        Me.lblSelectedWhat.Size = New System.Drawing.Size(100, 15)
        Me.lblSelectedWhat.TabIndex = 225
        Me.lblSelectedWhat.Text = "Recommend #"
        Me.lblSelectedWhat.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'btnHelp
        '
        Me.btnHelp.Image = CType(resources.GetObject("btnHelp.Image"), System.Drawing.Image)
        Me.btnHelp.Location = New System.Drawing.Point(935, 6)
        Me.btnHelp.Name = "btnHelp"
        Me.btnHelp.Size = New System.Drawing.Size(25, 25)
        Me.btnHelp.TabIndex = 235
        Me.btnHelp.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnHelp.UseVisualStyleBackColor = True
        '
        'ErrorProvider1
        '
        Me.ErrorProvider1.ContainerControl = Me
        '
        'frmMainConversation
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.CausesValidation = False
        Me.ClientSize = New System.Drawing.Size(926, 613)
        Me.Controls.Add(Me.btnHelp)
        Me.Controls.Add(Me.lblSelectedID)
        Me.Controls.Add(Me.lblSelectedWhat)
        Me.Controls.Add(Me.fldOrgNum)
        Me.Controls.Add(Me.fldCaseNum)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.StatusBar1)
        Me.Controls.Add(Me.fldGotoOrg)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Panel1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Menu = Me.MainMenu1
        Me.Name = "frmMainConversation"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Tag = "CONVERSATION"
        Me.Text = "CONVERSATION DETAIL"
        Me.ToolTip1.SetToolTip(Me, "Add new Recommendation")
        CType(Me.MainConversationBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsMainConversation1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.StatusBarPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.StatusBarPanelID, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.StatusBarPanel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.TabControl1.ResumeLayout(False)
        Me.pgNotes.ResumeLayout(False)
        Me.pgRecommend.ResumeLayout(False)
        CType(Me.grdMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpRate.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region 'windows

#Region "Load"

    'ON LOAD
    Private Sub frmMainConversation_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles MyBase.Load
        'Note: leave time in date field for sorting purposes.
        Me.SuspendLayout()
        SetStatusBarText("loading")
SetMainDSConnection:
        Me.daspMainConversation.UpdateCommand.Connection = sc
        Me.daspMainConversation.SelectCommand.Connection = sc
        Me.daspMainConversation.DeleteCommand.Connection = sc

SetDefaults:
        ctlIdentify = Me.txtBriefSummary
        ctlNeutral = Me.btnHelp
        mainDS = Me.DsMainConversation1
        mainTbl = Me.DsMainConversation1.MainConversation
        mainTopic = "Conversation"
        mainBSrce = Me.MainConversationBindingSource
        mainDAdapt = Me.daspMainConversation

LoadCombos:
        modGlobalVar.LoadStaffCombo(Me.cboStaff, False, StaffComboChoices.Selectable)
        modGlobalVar.LoadCRGCombo(Me.cboCRG)
        Me.cboMode.DataSource = tblConversMode

FormSetup:
        modGlobalVar.EnableGridTextboxes(Me.grdMain)
        Me.dtpDate.CustomFormat = "M/d/yyyy hh:mm tt"

        Forms.Add(Me)
        isLoaded = True
        SetStatusBarText("Done ")
        Me.ResumeLayout()
    End Sub 'load

    ' 'REFRESH DATA, COMBOS, AND GRIDS
    Public Sub Reload() 'As Boolean
        Dim tblContacts As New DataTable
        Dim tblCases As New DataTable
        SetStatusBarText("Reloading")

ResetVars:
        objHowClose = ObjClose.btnSaveExit
        Me.StatusBarPanelID.Text = mainTopic & " ID: " & ThisID.ToString
OrgBasedCombos:
        modGlobalVar.LoadContactCombo(Me.cboContact, tblContacts, LocalOrgID)
        modGlobalVar.LoadCaseCombo(Me.cboCase, tblCases, LocalOrgID)
FormSetup:
        FillSecondary()
        SetStatusBarText("Done")
    End Sub 'reload

    'allow Read Only if [calling] item is Inactive   re Aaron 7/15
    Private Sub DoFormSetup(ByVal enabled_state As Boolean)
        'eg if contact is inactive, don't edit the conversation
ControlFormat:
        If enabled_state = True Then
            modGlobalVar.EnableGridTextboxes(Me.grdMain)
            Me.dtpDate.CustomFormat = "M/d/yyyy hh:mm tt"
        End If
        If enabled_state = False Then
EnableControls:            Dim ctl As Control            ' Examine every control.            For Each ctl In Me.Controls                ' Don't disable the form's CheckBox or the user won't be able to reenable it.                If Not (ctl Is miSave) Then                    On Error Resume Next                    ctl.Enabled = enabled_state                    On Error GoTo 0                End If            Next ctl            Me.Text = Me.Text + "~~~~~~ READ ONLY ~~~~ "        Else
            '  Me.Text = "hello"        End If        'From <http://www.vb-helper.com/howto_enable_all_controls.html>     End Sub

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
        Else
SkipUpdate:  'so LastChangeDate doesn't get updated
            If bChanges = False Then
                e.Cancel = False
                GoTo ReleaseForm
            End If
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
                    ''INSERT DEFAULT DATA -- default case?
                    'ctl = arCtls(0)
                    'If objHowClose = ObjClose.SaveClose Or e.CloseReason = System.Windows.Forms.CloseReason.UserClosing Then
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
            ClassOpenForms.frmMainConversation = Nothing 'reset global var
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

#End Region 'update

#Region "Validation"

    ''VALIDATE DATE dtpicker
    'Private Sub txtConverseDate_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) _
    '    Handles txtConverseDate.Validating

    '    '  e.Cancel = mdGlobalVar.ValidateDateA(sender.DataBindings.Item("Text").BindingMemberInfo.BindingField, mainDS.Tables(0).Rows(0), sender, Me.ErrorProvider1)
    '    e.Cancel = mdGlobalVar.ValidateDateA(sender, Me.ErrorProvider1)


    '    'Select Case ValidateDate(sender.DataBindings.Item("Text").BindingMemberInfo.BindingField, mainDS.Tables(0).Rows(0), sender)
    '    '    Case Is = msgboxResult.Ok
    '    '        Me.ErrorProvider1.SetError(sender, "")
    '    '        e.Cancel = False
    '    '    Case Is = msgboxResult.Retry
    '    '        Me.ErrorProvider1.SetError(sender, "please enter a valid date")
    '    '        e.Cancel = True
    '    '    Case Is = msgboxResult.Abort
    '    '        Me.ErrorProvider1.SetError(sender, "please check this date")
    '    '        e.Cancel = True
    '    'End Select

    'End Sub

    'CHECK REQUIRED FIELDS
    Private Function CheckRequired() As Control()

        Dim ctl As Control
        Dim arCtls(0) As Control
        Dim i As Integer = 0

        'case required
        ctl = Me.cboCase
        If modGlobalVar.ValidateBoundDD(ctl, True, Me.ErrorProvider1, ObjClose.btnSaveExit) = usrInput.OK Then
        Else
            arCtls(i) = ctl
            i = i + 1
            ReDim Preserve arCtls(arCtls.GetUpperBound(0) + 1)
        End If

        'mode -- item
        ctl = Me.cboMode
        If modGlobalVar.ValidateBoundDD(ctl, True, Me.ErrorProvider1, ObjClose.btnSaveExit) = usrInput.OK Then
        Else
            arCtls(i) = ctl
            i = i + 1
            ReDim Preserve arCtls(arCtls.GetUpperBound(0) + 1)
        End If

        'staff
        ctl = Me.cboStaff
        If modGlobalVar.ValidateBoundDD(ctl, True, Me.ErrorProvider1, ObjClose.btnSaveExit) = usrInput.OK Then
        Else
            arCtls(i) = ctl
            i = i + 1
            ReDim Preserve arCtls(arCtls.GetUpperBound(0) + 1)
        End If

        'contact - not required
        ctl = Me.cboContact
        If String.IsNullOrEmpty(ctl.Text) Then
            If modGlobalVar.Msg("ATTENTION: Contact Missing", "Do you know with whom you had this conversation?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                arCtls(i) = ctl
                i = i + 1
                Me.ErrorProvider1.SetError(ctl, "please enter a " & IsNull(ctl.Tag, ctl.Name))
                ReDim Preserve arCtls(arCtls.GetUpperBound(0) + 1)
            Else
                Me.ErrorProvider1.SetError(ctl, "")
            End If
        End If

        'CRG
        ctl = Me.cboCRG
        If modGlobalVar.ValidateBoundDD(ctl, True, Me.ErrorProvider1, ObjClose.btnSaveExit) = usrInput.OK Then
        Else
            arCtls(i) = ctl
            i = i + 1
            ReDim Preserve arCtls(arCtls.GetUpperBound(0) + 1)
        End If


        arCtls(i) = ctlNeutral
        Return arCtls

    End Function 'checkrequired

    ''VALIDATE CBO
    'Private Sub cboCRG_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) _
    '    Handles cboCRG.Validating
    '    Dim usr As usrInput
    '    Select Case sender.name
    '        Case Is = "cboCRG", "cboStaff"
    '            usr = mdGlobalVar.ValidateBoundDD(sender, True, Me.ErrorProvider1, ObjClose.CloseByControl)

    '        Case Is = "cboContact"
    '            usr = mdGlobalVar.ValidateBoundDD(sender, False, Me.ErrorProvider1, ObjClose.CloseByControl)
    '    End Select
    '    If usr = usrInput.OK Then
    '        e.Cancel = False
    '    Else
    '        e.Cancel = True
    '    End If
    'End Sub

    '' to force dropdown closed to occur
    'Private Sub cboCase_Enter(sender As System.Object, e As System.EventArgs) Handles cboCase.Enter
    '    Me.cboCase.DroppedDown = True
    'End Sub


    'update CRG, Case Rating
    Private Sub cboCase_selectedindexchanged(ByVal sender As Object, ByVal e As System.EventArgs) _
        Handles cboCase.SelectedIndexChanged 'ionChangeCommitted

        If Not isLoaded Then
            Exit Sub
        End If

        If Me.cboCase.SelectedIndex > -1 Then
        Else
            Exit Sub
        End If
        'GetCRG from CASE
        Dim Str As String = "SELECT CRGNum, CurrentCaseRating FROM tblCase WHERE (CaseID = " & Me.cboCase.SelectedValue & ")"
        Dim sqlCase As New SqlClient.SqlCommand(Str, sc)
        If Not SCConnect() Then
            Exit Sub
        End If
        Dim drd As SqlDataReader = sqlCase.ExecuteReader(CommandBehavior.CloseConnection)
        ' While drd.Read
        drd.Read()
        If IsDBNull(drd(0)) Then
            Me.cboCRG.SelectedIndex = -1
        Else
            Me.cboCRG.SelectedValue = drd.GetInt32(0)
        End If
        If IsDBNull(drd(1)) Then
        Else
            Me.txtCaseRating.Text = drd.GetValue(1)
        End If
        ' End While
        drd.Close()
    End Sub

    'CASE - required field, allow add new
    Private Sub cboCase_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) _
        Handles cboCase.Validating        ' SetError(Me.ErrorProvider1, sender, ValidateCBO(sender, IsNull(sender.tag, sender.name), True, CanAddNew.AddNew), True)

        Dim tblCases As New DataTable
        Dim i As Integer
        Dim strUsrEntry As String
        strUsrEntry = IsNull(Me.cboCase.Text, "")
        Select Case modGlobalVar.ValidateBoundDD(sender, True, Me.ErrorProvider1, ObjClose.CloseByControl)
            Case usrInput.Search
                i = modPopup.NewCase(LocalOrgID, strUsrEntry, IsNull(Me.cboStaff.SelectedValue, 0), "Case", Me, IsNull(Me.fldGotoOrg.Text, ""), IsNull(Me.cboCRG.SelectedValue, 0))
                If i > 0 Then
                    modGlobalVar.LoadCaseCombo(Me.cboCase, tblCases, LocalOrgID)
                    Me.ErrorProvider1.SetError(sender, "")
                    Me.cboCase.SelectedValue = i
                End If
            Case usrInput.OK
            Case Else
                'Me.ErrorProvider1.SetError(sender, "select a case name from the dropdown box, or type a new name and fill out the popup form.")
        End Select

    End Sub

    'validate optional contact - can add new, optional
    Private Sub cboContact_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) _
        Handles cboContact.Validating

        Select Case modGlobalVar.ValidateBoundDD(sender, False, Me.ErrorProvider1, ObjClose.CloseByControl)
            Case Is = usrInput.OK
                e.Cancel = False
            Case Is = usrInput.Retry 'invalid selection like heading
                e.Cancel = True
            Case Is = usrInput.Search  'typed invalid text  - note: add new only through org detail
                '  modGlobalVar.Msg("to enter a new contact, from the Organization Detail window, click the New Button", , cboContact.Text & " Not Found")
                e.Cancel = True

            Case Is = usrInput.Ignore  'empty - confirm none
                If modGlobalVar.Msg("ATTENTION: Contact Missing", "Do you know with whom you had this conversation?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    e.Cancel = True
                Else
                    e.Cancel = False
                End If
            Case Else
                e.Cancel = True
        End Select

    End Sub

    'validate Required CBO
    Private Sub CBOValidating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) _
        Handles cboMode.Validating, cboStaff.Validating, cboCRG.Validating
        Dim CheckInput As usrInput
        '  modGlobalVar.Msg(sender.name, , "form validating")
        CheckInput = modGlobalVar.ValidateBoundDD(sender, True, Me.ErrorProvider1, ObjClose.CloseByControl)
        If CheckInput = usrInput.OK Then
            e.Cancel = False
        Else
            e.Cancel = True
            sender.DroppedDown = True
        End If
    End Sub

#End Region 'validation

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
            Handles miDelete.Click, btnDelete.Click

        '2014 PERSON CAN ONLY DELETE THEIR OWN CONVERSATIONS
        If usr = Me.cboStaff.SelectedValue Or usr = DBAdmin.StaffID Then
            If modGlobalVar.msg("CONFIRM DELETE", mainTopic & ": " & IsNull(ctlIdentify.Text, "") &
     NextLine & " will be deleted and the window closed.", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) = DialogResult.Yes Then
                'ctlIdentify.Text = "DELETE: " & IsNull(ctlIdentify.Text, "")
                objHowClose = ObjClose.miDelete
                mainBSrce.RemoveCurrent() 'RemoveAt(0)
                Me.Close()
            End If
        Else
            Dim r As Integer = cboStaff.Text.IndexOf(",")
            modGlobalVar.Msg("Cancelling Request", "only " & cboStaff.Text.Substring(r + 1, cboStaff.Text.Length - r) & " can delete this conversation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

        End If

    End Sub

    'refill grid
    Private Sub miRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles miRefresh.Click
        FillSecondary()
    End Sub

#End Region 'menu items

#Region "Fill Datasets"

    'FILL GRID
    Private Sub FillSecondary()

        Dim cmd As New SqlCommand("[getResRecommendation]", sc)

        MouseWait()

        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add("@IDFld", SqlDbType.VarChar).Value = "Conversation"
        cmd.Parameters.Add("@IDVal", SqlDbType.Int).Value = ThisID

        tbl = New DataTable("tRecommend")

        Try
            modGlobalVar.LoadDataTable(tbl, cmd)
            ' dt.Load(cmd.ExecuteReader(CommandBehavior.CloseConnection))
        Catch ex As Exception
            modGlobalVar.Msg("ERROR: can't fill " & TabControl1.SelectedTab.Tag, ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
            GoTo CloseAll
        End Try

        Me.grdMain.DataSource = tbl
        Me.pgRecommend.Text = tbl.Rows.Count.ToString & "  Resource RECOMMENDATIONS" ' & TabControl1.SelectedTab.Tag
        dv = New DataView(tbl) 'ds.Tables(0))
CloseAll:
        SetStatusBarText("Done")
        MouseDefault()
    End Sub

    'TAB CONTROL CHANGE call load secondary
    Private Sub TabControl1_Click(ByVal sender As Object, ByVal e As System.EventArgs) _
        Handles TabControl1.Click
        FillSecondary()
    End Sub '"Combos"

#End Region 'load secondary

#Region "Datagrid"

    'CELL CHANGE - HIGHLIGHT ROW
    Private Sub grdMain_CurrentCellChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles grdMain.CurrentCellChanged
        Me.lblSelectedID.Text = Me.grdMain.Item(grdMain.CurrentCell.RowNumber, 0)
        Me.lblSelectedWhat.Text = Me.TabControl1.SelectedTab.Name.Substring(2) 'Me.grdMain.Item(Me.grdMain.CurrentRowIndex, 0)
HighlightSelectedRow:
        Me.grdMain.Select(grdMain.CurrentCell.RowNumber)
    End Sub

    '    'CAPTURE RIGHT MOUSE CLICK TO FILTER APPROPRIATE GRID
    Protected Sub grdAll_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) _
            Handles grdMain.MouseDown

        Dim strHdr As String    'text for grid header
        hti = sender.HitTest(e.X, e.Y)
IfRightMouseclick:
        If e.Button = System.Windows.Forms.MouseButtons.Right Then
            strHdr = Me.TabControl1.SelectedTab.Tag   'strDGM

SetClearFilter:
            If hti.Type = DataGrid.HitTestType.Cell Then    'SET FILTER
                If IsDBNull(sender.Item(hti.Row, hti.Column)) Then 'nulls cause filter error
                    modGlobalVar.msg(MsgCodes.filterEmpty)
                    Exit Sub
                Else
                    grdFilter(sender, strHdr)
                End If
            Else                                            'not in cell, CLEAR FILTER
                Me.grdMain.DataSource = tbl 'ds.Tables(0) 'removes dv.rowfilter
                Me.pgRecommend.Text = tbl.Rows.Count.ToString & "  Resource RECOMMENDATIONS"
                statusM = ""
                SetStatusBarText(statusM & " " & statusS1 & " " & statusS2)
            End If
IfLeftMouseClick:
        Else    'left mouse
        End If

    End Sub

    'FILTER METHOD
    Private Sub grdFilter(ByVal grd As Object, ByVal strHdr As String)
        Dim strFilter As String
        Dim myColumns As GridColumnStylesCollection

        myColumns = grd.TableStyles(0).GridColumnStyles
        strFilter = myColumns(hti.Column).MappingName
        strFilter = strFilter & " = '" & grd.Item(hti.Row, hti.Column) & "'"
        dv.RowFilter = strFilter
        grd.DataSource = dv
        grd.CaptionText = dv.Count.ToString & "/" & tbl.Rows.Count.ToString & "  " & strHdr
        statusM = strHdr & " filtered on " & myColumns(hti.Column).HeaderText & " = " & Me.grdMain.Item(Me.grdMain.CurrentRowIndex, hti.Column)
        SetStatusBarText(statusM & " " & statusS1 & " " & statusS2)

    End Sub

    'CLEAR SELECTION FROM DATAGRIDS AND LOSE FOCUS (note: selection stays with arrow)
    Private Sub grdUnselect_Click(ByVal sender As Object, ByVal e As System.EventArgs) _
            Handles MyBase.Click, grdMain.Leave
        If grdMain.CurrentRowIndex > -1 Then
            Me.grdMain.UnSelect(grdMain.CurrentRowIndex)
            Me.grdMain.NavigateBack()
        End If
    End Sub

#End Region     'datagrid

#Region "GridDoubleclick"

    'OPEN MAIN DETAIL FORMS FROM DATAGRID
    Private Sub grdMain_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdMain.DoubleClick
        OpenForms()
    End Sub

#End Region 'grid doubleclick

#Region "New"

    'btnNEW
    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles miNew.Click, btnNew.Click

        Dim str As String

        Me.StatusBarPanel1.Text = "Adding new Recommendation"
        If modGlobalVar.Msg("About to enter a new Recommendation", "Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then
            Exit Sub
        End If

        If Me.cboCase.SelectedIndex < 0 Then
            modGlobalVar.Msg("ATTENTION: MISSING INFORMATION", "Please select a Case first", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Me.cboCase.Focus()
            Exit Sub
        End If
        If Me.cboContact.SelectedIndex < 0 Then
            modGlobalVar.Msg("ATTENTION: MISSING INFORMATION", "Please select a Contact first", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Me.cboContact.Focus()
            Exit Sub
        End If
        Dim dt As Date
        dt = IsNull(CType(Me.dtpDate.Value, Date), Now())
        str = "INSERT INTO tblResourceRecommend(CaseNum, OrgNum, ConversNum, RecommendDate, RecommendStaffNum, ContactNum, Used) VALUES (" & Me.cboCase.SelectedValue & ", " & Me.fldOrgNum.Text & ", " & ThisID & ", '" & dt & "', " & usr & ", " & Me.cboContact.SelectedValue & ", N'unknown'); SELECT @@Identity"

InsertNewItem:
        If Not SCConnect() Then
            Exit Sub
        End If


        '   Dim myTrans As SqlClient.SqlTransaction = sc.BeginTransaction()
        Dim cmd As New SqlClient.SqlCommand(str, sc) ', myTrans)
        Dim newID As Integer
        Try
            newID = cmd.ExecuteScalar()
            '      myTrans.Commit()
        Catch exce As Exception
            modGlobalVar.Msg("ERROR: new recommend", exce.Message & NextLine & MessageBoxIcon.Error, MessageBoxButtons.OK, MessageBoxIcon.Error)
            '     myTrans.Rollback()
            sc.Close()
            Exit Sub
        End Try
        sc.Close()

OpenForm:
        modGlobalVar.OpenMainRecommend(newID, "New Recommendation", Me.fldOrgNum.Text)
        Me.StatusBarPanel1.Text = "Done"
    End Sub
#End Region

#Region "Open Forms"

    'GOTO ORG
    Private Sub fldGotoOrg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles fldGotoOrg.DoubleClick
        Me.StatusBarPanel1.Text = "Opening Organization Detail window"
        modGlobalVar.OpenMainOrg(Me.fldOrgNum.Text, Me.fldGotoOrg.Text)
        Me.StatusBarPanel1.Text = "Done"
    End Sub

    'GOTO Contact or Case
    Private Sub ComboBox_DblClick(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles fldGotoCase.DoubleClick, fldGotoContact.DoubleClick 'cboContact.Click, cboCase.Click

        Me.StatusBarPanel1.Text = "Opening " & sender.tag & " Detail window"

        Select Case sender.tag
            Case Is = "Contact"
                If cboContact.SelectedIndex.ToString = -1 Then
                    modGlobalVar.msg("ATTENTION: Incomplete Entry", "Select a contact", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    cboContact.Focus()
                Else
                    modGlobalVar.OpenMainContact(cboContact.SelectedValue, cboContact.Text, fldGotoOrg.Text, Me.fldOrgNum.Text)
                End If
            Case Is = "Case"
                If cboCase.SelectedIndex = -1 Then
                    modGlobalVar.msg("ATTENTION: Incomplete Entry", "Select a case", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    cboCase.Focus() 'makes this form come ontop of case form
                Else
                    modGlobalVar.OpenMainCase(cboCase.SelectedValue, cboCase.Text, Me.fldGotoOrg.Text, Me.fldOrgNum.Text)
                End If
        End Select

        Me.StatusBarPanel1.Text = "Done"

    End Sub

    'OPEN RECOMMENDATION
    Private Sub OpenForms()
        MouseWait()

        modGlobalVar.OpenMainRecommend(Me.grdMain.Item(Me.grdMain.CurrentRowIndex, 0), IsNull(Me.grdMain.Item(Me.grdMain.CurrentRowIndex, 1), "unknown resource"), Me.fldOrgNum.Text)    ' Me.grdMain.Item(Me.grdMain.CurrentRowIndex, 10))
        Me.StatusBarPanel1.Text = "Done"
        MouseDefault()
    End Sub

#End Region 'open forms

#Region "General"

    'UNDO
    Protected Overrides Function ProcessDialogKey(ByVal keyData As Keys) As Boolean
        If keyData = System.Windows.Forms.Keys.Escape Then
            modPopup.UndoCtl(Me.ActiveControl)
            Return True  ' True means we've processed the key
        Else
            Return MyBase.ProcessDialogKey(keyData)
        End If
    End Function

    'CRG Filter
    Private Sub cboCRG_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) _
            Handles cboCRG.MouseDown
        If e.Button = System.Windows.Forms.MouseButtons.Right Then
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
        Handles rtbNotes.MouseDown, txtImpression.MouseDown, txtBriefSummary.MouseDown
        sender.focus()
        If e.Button = System.Windows.Forms.MouseButtons.Right Then
            Dim pp As New ClassRtbContextMenu(sender)
            pp.Show(Me, PointToClient(Control.MousePosition))
        End If
    End Sub

    'SET STATUS BAR LEFT TEXT
    Private Sub SetStatusBarText(ByVal str As String)
        Me.StatusBarPanel1.Text = str
    End Sub

    ' COPY CURRENT ID
    Private Sub StatusBar1_DoubleClick(sender As System.Object, e As System.EventArgs) Handles StatusBar1.DoubleClick
        Clipboard.SetText(ThisID)
    End Sub

#End Region 'general

#Region "Get/Set CASE RATING"

    'SET RADIO BUTTON CHECK WHEN FORM OPENS
    Private Sub CaseRating_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles txtCaseRating.TextChanged
        'invisible bound contol used on load to set visible radioboxes
        Dim ctl As Control
        For Each ctl In Me.grpRate.Controls
            If TypeOf ctl Is RadioButton Then
                If ctl.Name = "Rate" & Me.txtCaseRating.Text.ToString Then
                    CType(ctl, RadioButton).Checked = True
                    Exit Sub
                End If
            End If
        Next

    End Sub

    'PUT RATE CHANGE IN TEXT BOX
    Private Sub Rate0_Click(ByVal sender As Object, ByVal e As System.EventArgs) _
        Handles Rate0.Click, Rate1.Click, Rate2.Click, Rate3.Click, Rate4.Click, Rate5.Click
        If isLoaded Then
            If sender.checked Then
                Me.txtCaseRating.Focus()
                Me.txtCaseRating.SelectAll()
                Me.txtCaseRating.Text = sender.name.substring(4, 1)
                Me.txtImpression.Focus()
            End If
        End If
    End Sub

#End Region     'get rating

End Class


