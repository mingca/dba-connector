Option Explicit On
Imports System.Data.SqlClient
Imports System

Public Class frmMainResourceLocation
    Inherits System.Windows.Forms.Form

    Public isLoaded As Boolean = False
    Dim ctlIdentify As Control 'fields for delete and messages
    Dim ctlNeutral As Control 'will never be invalidated
    Dim objHowClose As Short ' structCloseMethod 'Object = Me.btnSaveExit 'identify object calling close
    Dim enumConvers, enumGrant, enumRecommend As structHeadings
    Dim mainDS As DataSet 'for generic module calls like CloseDetailForm
    Dim mainTopic As String 'name of entity of this form - case, contact, etc
    Dim mainDAdapt As SqlDataAdapter
    Public ThisID, LocalResourceID As Integer
    Dim mainBSrce As System.Windows.Forms.BindingSource

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

    Friend WithEvents SqlSelectCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlUpdateCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlDeleteCommand1 As System.Data.SqlClient.SqlCommand

    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents cboStatus As InfoCtr.ComboBoxRelaxed
    Friend WithEvents cboOverride As InfoCtr.ComboBoxRelaxed
    Friend WithEvents DsResourceLocationDD1 As InfoCtr.dsResourceLocationDD
    Friend WithEvents luLocationStatusTableAdapter As InfoCtr.dsResourceLocationDDTableAdapters.luLocationStatusTableAdapter
    Friend WithEvents ErrorProvider1 As System.Windows.Forms.ErrorProvider
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents TextBox2 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents StatusBarPanelID As System.Windows.Forms.StatusBarPanel
    Friend WithEvents luLocationOverrideTableAdapter As InfoCtr.dsResourceLocationDDTableAdapters.luLocationOverrideTableAdapter
    Friend WithEvents daMainResourceLocation As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents MainMenu1 As System.Windows.Forms.MainMenu
    Friend WithEvents MenuItem1 As System.Windows.Forms.MenuItem
    Friend WithEvents miClose As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem2 As System.Windows.Forms.MenuItem
    Friend WithEvents miSave As System.Windows.Forms.MenuItem
    Friend WithEvents miCancel As System.Windows.Forms.MenuItem
    Friend WithEvents miDelete As System.Windows.Forms.MenuItem
    Friend WithEvents lblHeading As System.Windows.Forms.Label
    Friend WithEvents DsMainResourceLocation1 As InfoCtr.dsMainResourceLocation

    'Friend WithEvents MainResourceLocationTableAdapter As InfoCtr.dsMainResourceLocationTableAdapters.MainResourceLocationTableAdapter
    Friend WithEvents fldResourceNum As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents MainResourceLocationBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents lblGiveTo As System.Windows.Forms.Label
    Friend WithEvents cboRegion As InfoCtr.ComboBoxRelaxed
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents cboStaff As InfoCtr.ComboBoxRelaxed
    Friend WithEvents cboPayment As InfoCtr.ComboBoxRelaxed
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtWhen As System.Windows.Forms.TextBox
    Friend WithEvents txtWhere As System.Windows.Forms.TextBox
    Friend WithEvents txtNote As System.Windows.Forms.TextBox
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents btnSaveExit As System.Windows.Forms.Button
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents btnHelp As System.Windows.Forms.Button
    Friend WithEvents StatusBar1 As System.Windows.Forms.StatusBar
    Friend WithEvents StatusBarPanel1 As System.Windows.Forms.StatusBarPanel
    Friend WithEvents StatusBarPanel2 As System.Windows.Forms.StatusBarPanel
    'Friend WithEvents MainResourceLocationTableAdapter As WindowsApplication11.dsMainResourceLocationTableAdapters.MainResourceLocationTableAdapter'
    Friend WithEvents SqlConnection1 As System.Data.SqlClient.SqlConnection

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.SqlConnection1 = New System.Data.SqlClient.SqlConnection()
        Me.SqlSelectCommand1 = New System.Data.SqlClient.SqlCommand()
        Me.SqlUpdateCommand1 = New System.Data.SqlClient.SqlCommand()
        Me.SqlDeleteCommand1 = New System.Data.SqlClient.SqlCommand()
        Me.lblGiveTo = New System.Windows.Forms.Label()
        Me.cboRegion = New InfoCtr.ComboBoxRelaxed()
        Me.MainResourceLocationBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.DsMainResourceLocation1 = New InfoCtr.dsMainResourceLocation()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.btnSaveExit = New System.Windows.Forms.Button()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.btnHelp = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.cboStaff = New InfoCtr.ComboBoxRelaxed()
        Me.cboPayment = New InfoCtr.ComboBoxRelaxed()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtWhen = New System.Windows.Forms.TextBox()
        Me.txtWhere = New System.Windows.Forms.TextBox()
        Me.txtNote = New System.Windows.Forms.TextBox()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.StatusBar1 = New System.Windows.Forms.StatusBar()
        Me.StatusBarPanel1 = New System.Windows.Forms.StatusBarPanel()
        Me.StatusBarPanelID = New System.Windows.Forms.StatusBarPanel()
        Me.StatusBarPanel2 = New System.Windows.Forms.StatusBarPanel()
        Me.MainMenu1 = New System.Windows.Forms.MainMenu(Me.components)
        Me.MenuItem1 = New System.Windows.Forms.MenuItem()
        Me.miClose = New System.Windows.Forms.MenuItem()
        Me.MenuItem2 = New System.Windows.Forms.MenuItem()
        Me.miSave = New System.Windows.Forms.MenuItem()
        Me.miCancel = New System.Windows.Forms.MenuItem()
        Me.miDelete = New System.Windows.Forms.MenuItem()
        Me.lblHeading = New System.Windows.Forms.Label()
        Me.fldResourceNum = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.luLocationOverrideTableAdapter = New InfoCtr.dsResourceLocationDDTableAdapters.luLocationOverrideTableAdapter()
        Me.luLocationStatusTableAdapter = New InfoCtr.dsResourceLocationDDTableAdapters.luLocationStatusTableAdapter()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.cboStatus = New InfoCtr.ComboBoxRelaxed()
        Me.DsResourceLocationDD1 = New InfoCtr.dsResourceLocationDD()
        Me.cboOverride = New InfoCtr.ComboBoxRelaxed()
        Me.ErrorProvider1 = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.Label13 = New System.Windows.Forms.Label()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.daMainResourceLocation = New System.Data.SqlClient.SqlDataAdapter()
        CType(Me.MainResourceLocationBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsMainResourceLocation1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel3.SuspendLayout()
        CType(Me.StatusBarPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.StatusBarPanelID, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.StatusBarPanel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsResourceLocationDD1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SqlConnection1
        '
        Me.SqlConnection1.ConnectionString = "Data Source=SOLOMON2008\SOLOMON2008;Initial Catalog=InfoCtr_be;Integrated Securit" & _
    "y=True"
        Me.SqlConnection1.FireInfoMessageEventOnUserErrors = False
        '
        'SqlSelectCommand1
        '
        Me.SqlSelectCommand1.CommandText = "dbo.MainResourceLocation"
        Me.SqlSelectCommand1.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlSelectCommand1.Connection = Me.SqlConnection1
        Me.SqlSelectCommand1.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, False, CType(0, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing), New System.Data.SqlClient.SqlParameter("@ID", System.Data.SqlDbType.Int, 4)})
        '
        'SqlUpdateCommand1
        '
        Me.SqlUpdateCommand1.CommandText = "dbo.MainResourceLocationUpdate"
        Me.SqlUpdateCommand1.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlUpdateCommand1.Connection = Me.SqlConnection1
        Me.SqlUpdateCommand1.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, False, CType(0, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing), New System.Data.SqlClient.SqlParameter("@ResourceNum", System.Data.SqlDbType.Int, 4, "ResourceNum"), New System.Data.SqlClient.SqlParameter("@SatelliteRegion", System.Data.SqlDbType.VarChar, 15, "SatelliteRegion"), New System.Data.SqlClient.SqlParameter("@OverrideNum", System.Data.SqlDbType.Int, 4, "OverrideLocationNum"), New System.Data.SqlClient.SqlParameter("@StatusNum", System.Data.SqlDbType.Int, 4, "LocationStatusNum"), New System.Data.SqlClient.SqlParameter("@OrderStaffNum", System.Data.SqlDbType.Int, 4, "OrderStaffNum"), New System.Data.SqlClient.SqlParameter("@OrderWhen", System.Data.SqlDbType.VarChar, 50, "OrderWhen"), New System.Data.SqlClient.SqlParameter("@OrderWhere", System.Data.SqlDbType.VarChar, 50, "OrderWhere"), New System.Data.SqlClient.SqlParameter("@OrderWhy", System.Data.SqlDbType.VarChar, 50, "OrderWhy"), New System.Data.SqlClient.SqlParameter("@OrderHow", System.Data.SqlDbType.VarChar, 50, "OrderHow"), New System.Data.SqlClient.SqlParameter("@OrderNotes", System.Data.SqlDbType.VarChar, 2147483647, "OrderNotes"), New System.Data.SqlClient.SqlParameter("@Edition", System.Data.SqlDbType.VarChar, 100, "Edition"), New System.Data.SqlClient.SqlParameter("@ISBN", System.Data.SqlDbType.VarChar, 30, "ISBN"), New System.Data.SqlClient.SqlParameter("@ID", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "ResourceLocationID", System.Data.DataRowVersion.Original, Nothing)})
        '
        'SqlDeleteCommand1
        '
        Me.SqlDeleteCommand1.CommandText = "dbo.MainResourceLocationDelete"
        Me.SqlDeleteCommand1.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlDeleteCommand1.Connection = Me.SqlConnection1
        Me.SqlDeleteCommand1.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, False, CType(0, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing), New System.Data.SqlClient.SqlParameter("@ID", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "ResourceLocationID", System.Data.DataRowVersion.Original, Nothing)})
        '
        'lblGiveTo
        '
        Me.lblGiveTo.AutoSize = True
        Me.lblGiveTo.ForeColor = System.Drawing.SystemColors.AppWorkspace
        Me.lblGiveTo.Location = New System.Drawing.Point(314, 181)
        Me.lblGiveTo.MaximumSize = New System.Drawing.Size(250, 0)
        Me.lblGiveTo.Name = "lblGiveTo"
        Me.lblGiveTo.Size = New System.Drawing.Size(13, 13)
        Me.lblGiveTo.TabIndex = 0
        Me.lblGiveTo.Text = "+"
        '
        'cboRegion
        '
        Me.cboRegion.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboRegion.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboRegion.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.MainResourceLocationBindingSource, "SatelliteRegion", True))
        Me.cboRegion.FormattingEnabled = True
        Me.cboRegion.Location = New System.Drawing.Point(124, 80)
        Me.cboRegion.Name = "cboRegion"
        Me.cboRegion.RestrictContentToListItems = True
        Me.cboRegion.Size = New System.Drawing.Size(184, 21)
        Me.cboRegion.TabIndex = 0
        Me.ToolTip1.SetToolTip(Me.cboRegion, "Office location of the resource or file for the resource.")
        '
        'MainResourceLocationBindingSource
        '
        Me.MainResourceLocationBindingSource.DataMember = "MainResourceLocation"
        Me.MainResourceLocationBindingSource.DataSource = Me.DsMainResourceLocation1
        '
        'DsMainResourceLocation1
        '
        Me.DsMainResourceLocation1.DataSetName = "dsMainResourceLocation"
        Me.DsMainResourceLocation1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(73, 83)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(41, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Region"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(29, 192)
        Me.Label3.MaximumSize = New System.Drawing.Size(100, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(84, 26)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Override Default Location"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(53, 114)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(64, 13)
        Me.Label4.TabIndex = 5
        Me.Label4.Text = "Entry Status"
        '
        'btnSaveExit
        '
        Me.btnSaveExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSaveExit.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.btnSaveExit.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSaveExit.Image = Global.InfoCtr.My.Resources.Resources.btnSaveExit
        Me.btnSaveExit.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSaveExit.Location = New System.Drawing.Point(45, 1)
        Me.btnSaveExit.Name = "btnSaveExit"
        Me.btnSaveExit.Size = New System.Drawing.Size(40, 35)
        Me.btnSaveExit.TabIndex = 514
        Me.btnSaveExit.Text = "Close"
        Me.btnSaveExit.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.btnSaveExit, "Saves any changes and Closes this window")
        Me.btnSaveExit.UseVisualStyleBackColor = False
        '
        'btnDelete
        '
        Me.btnDelete.BackColor = System.Drawing.SystemColors.Control
        Me.btnDelete.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Image = Global.InfoCtr.My.Resources.Resources.btnDelete
        Me.btnDelete.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnDelete.Location = New System.Drawing.Point(3, 1)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(40, 35)
        Me.btnDelete.TabIndex = 512
        Me.btnDelete.Text = "Delete"
        Me.btnDelete.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.btnDelete, "Delete this Location")
        Me.btnDelete.UseVisualStyleBackColor = False
        Me.btnDelete.Visible = False
        '
        'btnHelp
        '
        Me.btnHelp.Image = Global.InfoCtr.My.Resources.Resources.btnHelp
        Me.btnHelp.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnHelp.Location = New System.Drawing.Point(591, 12)
        Me.btnHelp.Name = "btnHelp"
        Me.btnHelp.Size = New System.Drawing.Size(25, 25)
        Me.btnHelp.TabIndex = 782
        Me.btnHelp.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ToolTip1.SetToolTip(Me.btnHelp, "Help")
        Me.btnHelp.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.ForeColor = System.Drawing.SystemColors.Highlight
        Me.Label5.Location = New System.Drawing.Point(35, 166)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(101, 13)
        Me.Label5.TabIndex = 7
        Me.Label5.Text = "Optional Information"
        '
        'cboStaff
        '
        Me.cboStaff.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboStaff.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboStaff.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.MainResourceLocationBindingSource, "OrderStaffNum", True))
        Me.cboStaff.FormattingEnabled = True
        Me.cboStaff.Location = New System.Drawing.Point(125, 230)
        Me.cboStaff.Name = "cboStaff"
        Me.cboStaff.RestrictContentToListItems = True
        Me.cboStaff.Size = New System.Drawing.Size(184, 21)
        Me.cboStaff.TabIndex = 3
        '
        'cboPayment
        '
        Me.cboPayment.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboPayment.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboPayment.DataBindings.Add(New System.Windows.Forms.Binding("SelectedItem", Me.MainResourceLocationBindingSource, "OrderHow", True))
        Me.cboPayment.FormattingEnabled = True
        Me.cboPayment.Items.AddRange(New Object() {"Check", "Credit Card", "Requested Invoice"})
        Me.cboPayment.Location = New System.Drawing.Point(125, 324)
        Me.cboPayment.Name = "cboPayment"
        Me.cboPayment.RestrictContentToListItems = True
        Me.cboPayment.Size = New System.Drawing.Size(184, 21)
        Me.cboPayment.TabIndex = 6
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(86, 234)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(29, 13)
        Me.Label6.TabIndex = 11
        Me.Label6.Text = "Staff"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(79, 265)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(36, 13)
        Me.Label7.TabIndex = 12
        Me.Label7.Text = "When"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(35, 296)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(82, 13)
        Me.Label8.TabIndex = 13
        Me.Label8.Text = "Where/Supplier"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(29, 328)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(87, 13)
        Me.Label10.TabIndex = 15
        Me.Label10.Text = "Payment Method"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(83, 361)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(30, 13)
        Me.Label11.TabIndex = 16
        Me.Label11.Text = "Note"
        '
        'txtWhen
        '
        Me.txtWhen.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainResourceLocationBindingSource, "OrderWhen", True))
        Me.txtWhen.Location = New System.Drawing.Point(125, 262)
        Me.txtWhen.Name = "txtWhen"
        Me.txtWhen.Size = New System.Drawing.Size(184, 20)
        Me.txtWhen.TabIndex = 4
        '
        'txtWhere
        '
        Me.txtWhere.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainResourceLocationBindingSource, "OrderWhere", True))
        Me.txtWhere.Location = New System.Drawing.Point(125, 293)
        Me.txtWhere.Name = "txtWhere"
        Me.txtWhere.Size = New System.Drawing.Size(184, 20)
        Me.txtWhere.TabIndex = 5
        '
        'txtNote
        '
        Me.txtNote.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainResourceLocationBindingSource, "OrderNotes", True))
        Me.txtNote.Location = New System.Drawing.Point(125, 359)
        Me.txtNote.Multiline = True
        Me.txtNote.Name = "txtNote"
        Me.txtNote.Size = New System.Drawing.Size(315, 72)
        Me.txtNote.TabIndex = 7
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.SystemColors.Highlight
        Me.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel3.Controls.Add(Me.btnSaveExit)
        Me.Panel3.Controls.Add(Me.btnDelete)
        Me.Panel3.Location = New System.Drawing.Point(496, 10)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(89, 40)
        Me.Panel3.TabIndex = 510
        '
        'StatusBar1
        '
        Me.StatusBar1.Location = New System.Drawing.Point(0, 444)
        Me.StatusBar1.Name = "StatusBar1"
        Me.StatusBar1.Panels.AddRange(New System.Windows.Forms.StatusBarPanel() {Me.StatusBarPanel1, Me.StatusBarPanelID, Me.StatusBarPanel2})
        Me.StatusBar1.ShowPanels = True
        Me.StatusBar1.Size = New System.Drawing.Size(650, 22)
        Me.StatusBar1.TabIndex = 784
        Me.StatusBar1.Text = "Done"
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
        Me.StatusBarPanelID.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents
        Me.StatusBarPanelID.Name = "StatusBarPanelID"
        Me.StatusBarPanelID.Text = "Resource Location ID:"
        Me.StatusBarPanelID.Width = 126
        '
        'StatusBarPanel2
        '
        Me.StatusBarPanel2.Alignment = System.Windows.Forms.HorizontalAlignment.Right
        Me.StatusBarPanel2.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring
        Me.StatusBarPanel2.Name = "StatusBarPanel2"
        Me.StatusBarPanel2.Text = "Physical Location of Article, Book, Organization File."
        Me.StatusBarPanel2.Width = 307
        '
        'MainMenu1
        '
        Me.MainMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem1, Me.MenuItem2})
        '
        'MenuItem1
        '
        Me.MenuItem1.Index = 0
        Me.MenuItem1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.miClose})
        Me.MenuItem1.Text = "File"
        '
        'miClose
        '
        Me.miClose.Index = 0
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
        Me.miSave.Enabled = False
        '
        'miCancel
        '
        Me.miCancel.Index = 1
        Me.miCancel.Text = "Cancel Changes"
        '
        'miDelete
        '
        Me.miDelete.Index = 2
        Me.miDelete.Text = "Delete Location"
        '
        'lblHeading
        '
        Me.lblHeading.AutoSize = True
        Me.lblHeading.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeading.ForeColor = System.Drawing.Color.DarkBlue
        Me.lblHeading.Location = New System.Drawing.Point(34, 17)
        Me.lblHeading.Name = "lblHeading"
        Me.lblHeading.Size = New System.Drawing.Size(267, 20)
        Me.lblHeading.TabIndex = 786
        Me.lblHeading.Text = "Location of resource at Center office"
        '
        'fldResourceNum
        '
        Me.fldResourceNum.BackColor = System.Drawing.SystemColors.Control
        Me.fldResourceNum.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.fldResourceNum.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainResourceLocationBindingSource, "ResourceNum", True))
        Me.fldResourceNum.Enabled = False
        Me.fldResourceNum.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fldResourceNum.ForeColor = System.Drawing.Color.DarkGray
        Me.fldResourceNum.Location = New System.Drawing.Point(542, 417)
        Me.fldResourceNum.Name = "fldResourceNum"
        Me.fldResourceNum.Size = New System.Drawing.Size(57, 14)
        Me.fldResourceNum.TabIndex = 789
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlDark
        Me.Label1.Location = New System.Drawing.Point(461, 417)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(75, 13)
        Me.Label1.TabIndex = 790
        Me.Label1.Text = "ResourceNum"
        '
        'luLocationOverrideTableAdapter
        '
        Me.luLocationOverrideTableAdapter.ClearBeforeFill = True
        '
        'luLocationStatusTableAdapter
        '
        Me.luLocationStatusTableAdapter.ClearBeforeFill = True
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainResourceLocationBindingSource, "ActualLocationtxt", True))
        Me.Label12.ForeColor = System.Drawing.SystemColors.InactiveCaption
        Me.Label12.Location = New System.Drawing.Point(150, 42)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(76, 13)
        Me.Label12.TabIndex = 796
        Me.Label12.Text = "actual location"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainResourceLocationBindingSource, "Placement", True))
        Me.Label15.ForeColor = System.Drawing.SystemColors.InactiveCaption
        Me.Label15.Location = New System.Drawing.Point(88, 42)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(56, 13)
        Me.Label15.TabIndex = 799
        Me.Label15.Text = "placement"
        '
        'cboStatus
        '
        Me.cboStatus.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboStatus.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboStatus.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.MainResourceLocationBindingSource, "LocationStatusNum", True))
        Me.cboStatus.DataSource = Me.DsResourceLocationDD1
        Me.cboStatus.DisplayMember = "luLocationStatus.LocationName"
        Me.cboStatus.FormattingEnabled = True
        Me.cboStatus.Location = New System.Drawing.Point(125, 111)
        Me.cboStatus.MaxDropDownItems = 20
        Me.cboStatus.Name = "cboStatus"
        Me.cboStatus.RestrictContentToListItems = True
        Me.cboStatus.Size = New System.Drawing.Size(183, 21)
        Me.cboStatus.TabIndex = 1
        Me.cboStatus.ValueMember = "LocationSetupID"
        '
        'DsResourceLocationDD1
        '
        Me.DsResourceLocationDD1.DataSetName = "dsResourceLocationDD"
        Me.DsResourceLocationDD1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'cboOverride
        '
        Me.cboOverride.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboOverride.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboOverride.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.MainResourceLocationBindingSource, "OverrideLocationNum", True))
        Me.cboOverride.DataSource = Me.DsResourceLocationDD1
        Me.cboOverride.DisplayMember = "luLocationOverride.LocationName"
        Me.cboOverride.FormattingEnabled = True
        Me.cboOverride.Location = New System.Drawing.Point(125, 191)
        Me.cboOverride.MaxDropDownItems = 20
        Me.cboOverride.Name = "cboOverride"
        Me.cboOverride.RestrictContentToListItems = True
        Me.cboOverride.Size = New System.Drawing.Size(183, 21)
        Me.cboOverride.TabIndex = 2
        Me.cboOverride.ValueMember = "LocationSetupID"
        '
        'ErrorProvider1
        '
        Me.ErrorProvider1.ContainerControl = Me
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.ForeColor = System.Drawing.SystemColors.Highlight
        Me.Label13.Location = New System.Drawing.Point(371, 166)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(245, 13)
        Me.Label13.TabIndex = 800
        Me.Label13.Text = "Editionl Information if different from reosurce Detail."
        '
        'TextBox1
        '
        Me.TextBox1.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainResourceLocationBindingSource, "ISBN", True))
        Me.TextBox1.Location = New System.Drawing.Point(410, 191)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(183, 20)
        Me.TextBox1.TabIndex = 801
        '
        'TextBox2
        '
        Me.TextBox2.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainResourceLocationBindingSource, "Edition", True))
        Me.TextBox2.Location = New System.Drawing.Point(410, 230)
        Me.TextBox2.Multiline = True
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(183, 61)
        Me.TextBox2.TabIndex = 802
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(356, 238)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(39, 13)
        Me.Label14.TabIndex = 803
        Me.Label14.Text = "Edition"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(356, 194)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(32, 13)
        Me.Label16.TabIndex = 804
        Me.Label16.Text = "ISBN"
        '
        'daMainResourceLocation
        '
        Me.daMainResourceLocation.DeleteCommand = Me.SqlDeleteCommand1
        Me.daMainResourceLocation.SelectCommand = Me.SqlSelectCommand1
        Me.daMainResourceLocation.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "MainResourceLocation", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("ResourceLocationID", "ResourceLocationID"), New System.Data.Common.DataColumnMapping("ResourceNum", "ResourceNum"), New System.Data.Common.DataColumnMapping("SatelliteRegion", "SatelliteRegion"), New System.Data.Common.DataColumnMapping("OrderStaffNum", "OrderStaffNum"), New System.Data.Common.DataColumnMapping("OrderWhen", "OrderWhen"), New System.Data.Common.DataColumnMapping("OrderWhere", "OrderWhere"), New System.Data.Common.DataColumnMapping("OrderWhy", "OrderWhy"), New System.Data.Common.DataColumnMapping("OrderHow", "OrderHow"), New System.Data.Common.DataColumnMapping("OrderNotes", "OrderNotes"), New System.Data.Common.DataColumnMapping("LocationStatusNum", "LocationStatusNum"), New System.Data.Common.DataColumnMapping("ActualLocationNum", "ActualLocationNum"), New System.Data.Common.DataColumnMapping("OverrideLocationNum", "OverrideLocationNum"), New System.Data.Common.DataColumnMapping("Placement", "Placement"), New System.Data.Common.DataColumnMapping("ActualLocationtxt", "ActualLocationtxt"), New System.Data.Common.DataColumnMapping("Edition", "Edition"), New System.Data.Common.DataColumnMapping("ISBN", "ISBN")})})
        Me.daMainResourceLocation.UpdateCommand = Me.SqlUpdateCommand1
        '
        'frmMainResourceLocation
        '
        Me.ClientSize = New System.Drawing.Size(650, 466)
        Me.Controls.Add(Me.Label16)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.TextBox2)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.cboOverride)
        Me.Controls.Add(Me.cboStatus)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.fldResourceNum)
        Me.Controls.Add(Me.lblHeading)
        Me.Controls.Add(Me.StatusBar1)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.btnHelp)
        Me.Controls.Add(Me.txtNote)
        Me.Controls.Add(Me.txtWhere)
        Me.Controls.Add(Me.txtWhen)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.cboPayment)
        Me.Controls.Add(Me.cboStaff)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.cboRegion)
        Me.Controls.Add(Me.lblGiveTo)
        Me.Menu = Me.MainMenu1
        Me.Name = "frmMainResourceLocation"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Resource Location"
        CType(Me.MainResourceLocationBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsMainResourceLocation1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel3.ResumeLayout(False)
        CType(Me.StatusBarPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.StatusBarPanelID, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.StatusBarPanel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsResourceLocationDD1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

#Region "Load"
    'LOAD
    Private Sub frmMainResourceLocation_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles MyBase.Load
        Me.SuspendLayout()
        modPopup.SetStatusBarText("loading", Me.StatusBar1, 0)

SetMainDSConnection:
        Me.daMainResourceLocation.SelectCommand.Connection = sc
        Me.daMainResourceLocation.UpdateCommand.Connection = sc
        Me.daMainResourceLocation.DeleteCommand.Connection = sc
SetDefaults:
        mainDAdapt = Me.daMainResourceLocation
        mainDS = Me.DsMainResourceLocation1
        mainBSrce = Me.MainResourceLocationBindingSource
        'ctlIdentify = Me.txtCaseName
        ctlNeutral = Me.btnHelp
        mainTopic = "Resource Location"
LoadCombos:
        modGlobalVar.LoadRegionCombo(Me.cboRegion, "Library")
        modGlobalVar.LoadStaffCombo(Me.cboStaff, False, StaffComboChoices.Selectable)
FormSetup:
        Me.cboRegion.SelectedIndex = Me.cboRegion.FindStringExact(gRegion)
        Me.luLocationStatusTableAdapter.FillUserStatus(Me.DsResourceLocationDD1.luLocationStatus)

        Me.ResumeLayout()
        isLoaded = True
        Forms.Add(Me)
        modPopup.SetStatusBarText("Done", Me.StatusBar1, 0)

    End Sub

    'RELOAD
    Public Sub Reload()
ResetVars:
        objHowClose = ObjClose.btnSaveExit
        Me.StatusBarPanelID.Text = mainTopic & " ID: " & ThisID.ToString
        RefreshCombos()
    End Sub
    '  TODO QUESTION  Does Load: cboRegionSelected index to staff region work if is data in field?

#End Region    'load

    'TODO Complete Updata Change
#Region "Update Main"

    'SAVE & EXIT
    Private Sub btnSaveExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnSaveExit.Click
        objHowClose = ObjClose.btnSaveExit
        Me.Close()
    End Sub

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

    'CLOSING
    Private Sub frm_Closing(ByVal sender As Object, ByVal e As FormClosingEventArgs) _
        Handles MyBase.FormClosing  ''ByVal e As System.ComponentModel.CancelEventArgs) _

        '   Dim arCtls(0) As Control
        '  Dim ctl As Control 'no required info here
        'modGlobalVar.Msg("DS has changes: " & mainDS.HasChanges.ToString& NextLine & 
        '      "RowState: " & mainDS.Tables(0).Rows(0).RowState.ToString& NextLine & 
        '      "Original Version: " & mainDS.Tables(0).Rows(0).Item("OpenDate", DataRowVersion.Original).ToString& NextLine & 
        '      "Current Version: " & mainDS.Tables(0).Rows(0).Item("OpenDate", DataRowVersion.Current).ToString, , "closing: " & CType(objHowClose, ObjClose).ToString

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

CheckRequiredFields:  ' allow user to leave anyway if used menu
        Select Case objHowClose
            Case Is = ObjClose.DontSaveClose, ObjClose.miDelete
                e.Cancel = False
                GoTo ReleaseForm
            Case Is = ObjClose.cancelClose
                e.Cancel = True
                GoTo ReleaseForm
            Case Else 'btnSaveExit, SaveClose,, ObjClose.noChanges
                'arCtls = CheckRequired()
                'If arCtls.GetLength(0) > 1 Then 'required info missing
                '    ctl = arCtls(0)
                '    ''INSERT DEFAULT DATA
                '    'If objHowClose = ObjClose.SaveClose Or e.CloseReason = Windows.Forms.CloseReason.UserClosing Then
                '    '    If ctl Is ctlIdentify Then
                '    '        ctl.Text = usrName & " " & Today.ToShortDateString
                '    '        mainBSrce.EndEdit()
                '    '        mainDAdapt.Update(mainDS) 'save default data
                '    '    End If
                '    'End If
                '    Dim strbListFields As New StringBuilder
                '    For x As Integer = 0 To arCtls.GetLength(0) - 2
                '        strbListFields.Append(", " & arCtls(x).Tag)
                '    Next
                '    e.Cancel = Not (mdGlobalVar.AskCloseWithMissingInfo(objHowClose, ctl, strbListFields.ToString.Substring(2)))
                'Else
                '    e.Cancel = False
                'End If
        End Select

ReleaseForm:  'TODO ? is this still necessary?
        If e.Cancel = False Then   'user OKs close form
            ClassOpenForms.frmMainResourceLocation = Nothing 'reset global var
            objHowClose = Nothing
        Else
        End If
        '   arCtls = Nothing
        MouseDefault()

    End Sub

    'UPDATE BACK END, return number of records updated, return false if error updating
    Public Function DoUpdate() As Boolean
        Dim i As Integer
        MouseWait()
        modPopup.SetStatusBarText("Updating server", Me.StatusBar1, 0)

        If mainDS.Tables(0).Rows(0).HasVersion(DataRowVersion.Proposed) Or mainDS.HasChanges Then
            mainBSrce.EndEdit()
        Else
            DoUpdate = True
            GoTo CloseAll
        End If

        If mainDS.HasChanges = True Then 'this catches delete, save, asksave, save/exit, anyclose
UpdateBackend:
            Try
                i = mainDAdapt.Update(mainDS)
                DoUpdate = True
            Catch ex As Exception
                modGlobalVar.msg("ERROR: updating " & mainTopic, ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
                DoUpdate = False
            End Try
        Else
            DoUpdate = True 'completed action though no updates to be made
        End If

CloseAll:
        modPopup.SetStatusBarText("Update routine complete", Me.StatusBar1, 0)
        MouseDefault()

    End Function 'update

#End Region 'update main

#Region "Edit Buttons"

    'SAVE
    Private Sub miSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
     Handles miSave.Click
        mainBSrce.EndEdit()
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
        modPopup.SetStatusBarText("Changes Cancelled", Me.StatusBar1, 0)
    End Sub

    'DELETE
    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles miDelete.Click
        'only user FROM THIS REGION can delete a location
        If usr = DBAdmin.StaffID Or usrRegion = Me.cboRegion.Text Then
            If modGlobalVar.Msg("CONFIRM DELETE", "This " & mainTopic & " WILL BE DELETED and the window closed.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                '  ctlIdentify.Text = "DELETE: " & IsNull(ctlIdentify.Text, "")
                objHowClose = ObjClose.miDelete
                mainBSrce.RemoveCurrent() 'RemoveAt(0)
                Me.Close()
            End If
        Else
            modGlobalVar.Msg("Cancelling request", "only staff at " & Me.cboRegion.Text & "region can delete their location", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

#End Region

#Region "RefreshCombos"

    'UPDATE LOCATION and STATUS COMBOs
    Private Sub RefreshCombos()
        Dim str As String = Me.cboOverride.Text
        Try
            Me.luLocationOverrideTableAdapter.FillLocation(Me.DsResourceLocationDD1.luLocationOverride, cboRegion.SelectedValue)
        Catch ex As Exception
            modGlobalVar.Msg("ERROR: Fill", "could not fill status/location combobox" & NextLine & ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        If str = String.Empty Then
            Me.cboOverride.SelectedIndex = -1
            Exit Sub
        End If
    End Sub

    'CALL CHANGE DD
    Private Sub cboRegion_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) _
            Handles cboRegion.SelectedIndexChanged, cboRegion.DropDownClosed ',cboRegion.TextChanged, cboRegion.TextUpdate ',cboRegion.SelectionChangeCommitted ',cboRegion.SelectedValueChanged ',cboRegion.SelectedIndexChanged

        gRegion = Me.cboRegion.SelectedValue
        RefreshCombos()

    End Sub

    'UPDATE STAFF AND DATE
    Private Sub cboStatus_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) _
        Handles cboRegion.SelectionChangeCommitted, cboStatus.DropDownClosed
        Me.cboStaff.SelectedIndex = cboStaff.FindStringExact(usrName)
        Me.txtWhen.Text = Now '.ToShortDateString
    End Sub

#End Region

#Region "General"

    'COPY CURRENT ID
    Private Sub StatusBar1_DoubleClick(sender As System.Object, e As System.EventArgs) Handles StatusBar1.DoubleClick
        Clipboard.SetText(ThisID)
    End Sub

    'RIGHT CLICK MENU
    Private Sub textbox_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) _
        Handles txtNote.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Right Then
            Dim pp As New ClassRtbContextMenu(sender)
            pp.Show(Me, PointToClient(Control.MousePosition))
        End If
        miSave.PerformClick()
    End Sub

    'UNDO
    Protected Overrides Function ProcessDialogKey(ByVal keyData As Keys) As Boolean
        If keyData = System.Windows.Forms.Keys.Escape Then
            modPopup.UndoCtl(Me.ActiveControl)
            Return True  ' True means we've processed the key
        Else
            Return MyBase.ProcessDialogKey(keyData)
        End If
    End Function

    'HELP
    Private Sub btnHelp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHelp.Click
        modGlobalVar.Msg("HELP: Location of Resource", "Location is to indicate where each office houses a copy of a resource. " & NextLine & "The Order/Label Status is used to enable labels to be run." & NextLine & NextLine & "Recording information about the purchase of a resource may be recorded here.  This is not a permanent record; it will be overwritten if the resource is ordered additional times." & NextLine & NextLine & "Each Location can have multiple copies of a resource.", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

#End Region

#Region "Validation"

    'Optional, heading
    Private Sub cboStatus_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cboStatus.Validating
        modGlobalVar.NewIsHeading(sender)
    End Sub

    'required
    Private Sub cboRegion_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) _
    Handles cboRegion.Validating
        If modGlobalVar.ValidateBoundDD(sender, True, Me.ErrorProvider1, ObjClose.CloseByControl) = usrInput.OK Then
            e.Cancel = False
        Else
            e.Cancel = True
        End If
        '   e.Cancel = Not newValidateUnboundCBOString(sender, True, Me.ErrorProvider1, ObjClose.CloseByControl)
    End Sub

    'staff not required?
    Private Sub cboStaff_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cboStaff.Validating
        If modGlobalVar.ValidateBoundDD(sender, True, Me.ErrorProvider1, ObjClose.CloseByControl) = usrInput.OK Then
            e.Cancel = False
        Else
            e.Cancel = True
            sender.DroppedDown = True
        End If

    End Sub

#End Region 'validating

End Class
