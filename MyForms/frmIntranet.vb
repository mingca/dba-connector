Imports System.Data.SqlClient
Imports System.Text


Public Class frmIntranet
    Inherits System.Windows.Forms.Form

    'datagrid variables...
    Dim hti As System.Windows.Forms.DataGrid.HitTestInfo    'datagrid filter
    Dim statusM, statusS1, statusS2 As String 'filter text for status bar
    Dim strDGM As String  'header text on datagrids
    Dim dv, dvM As DataView 'for filtering search results
    Dim strbActiveGrid As New StringBuilder 'for doubleclick any datagrid
    Dim da As SqlDataAdapter
    Dim iCurrentRow, iRow, itblStyle As Integer
    Dim isLoaded As Boolean
    Const strEdit As String = "Editing"
    Const strView As String = "Document List"
    Dim cURL As DataGridColumnStyle
    Dim cmgr As CurrencyManager
    Dim iURL As Integer
    Friend WithEvents DataGridTextBoxColumn7 As System.Windows.Forms.DataGridTextBoxColumn
    Dim sourceRowCount As Integer 'row count re filter/total
    Dim strLastCategory As String = "Request"


#Region " Windows Form Designer generated code "

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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents miOpen As System.Windows.Forms.MenuItem
    Friend WithEvents miSend As System.Windows.Forms.MenuItem
    Friend WithEvents miAdd As System.Windows.Forms.MenuItem
    Friend WithEvents StatusBarPanel1 As System.Windows.Forms.StatusBarPanel
    Friend WithEvents StatusBarPanel2 As System.Windows.Forms.StatusBarPanel
    Friend WithEvents SqlDeleteCommand As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlConnection1 As System.Data.SqlClient.SqlConnection
    Friend WithEvents FormID As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents SqlInsertCommand As System.Data.SqlClient.SqlCommand
    Friend WithEvents sqlUpdateCommand As System.Data.SqlClient.SqlCommand
    Friend WithEvents DataGridTextBoxColumn9 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents FormName As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn11 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents miClose As System.Windows.Forms.MenuItem

    Friend WithEvents MenuEdit As System.Windows.Forms.MenuItem
    Friend WithEvents miSave As System.Windows.Forms.MenuItem
    Friend WithEvents miCancel As System.Windows.Forms.MenuItem
    Friend WithEvents miDelete As System.Windows.Forms.MenuItem
    Friend WithEvents lblIinstruction As System.Windows.Forms.Label
    Friend WithEvents btnAdd As System.Windows.Forms.Button
    Friend WithEvents cboType As System.Windows.Forms.ComboBox
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents HelpProvider1 As System.Windows.Forms.HelpProvider


    Friend WithEvents DataGrid1 As System.Windows.Forms.DataGrid
    Friend WithEvents daMainIntranet As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents DsIntranet1 As InfoCtr.dsIntranet
    Friend WithEvents TableStyleView As System.Windows.Forms.DataGridTableStyle
    Friend WithEvents DataGridTextBoxColumn1 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents URL1 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents TableStyleEdit As System.Windows.Forms.DataGridTableStyle
    Friend WithEvents DataGridTextBoxColumn3 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn4 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn5 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn6 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents URL2 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents MainMenu1 As System.Windows.Forms.MainMenu
    Friend WithEvents MenuItem1 As System.Windows.Forms.MenuItem
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents miView As System.Windows.Forms.MenuItem
    Friend WithEvents StatusBar1 As System.Windows.Forms.StatusBar
    Friend WithEvents SqlSelectCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents DataGridTextBoxColumn8 As System.Windows.Forms.DataGridTextBoxColumn
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmIntranet))
        Me.miAdd = New System.Windows.Forms.MenuItem()
        Me.daMainIntranet = New System.Data.SqlClient.SqlDataAdapter()
        Me.SqlDeleteCommand = New System.Data.SqlClient.SqlCommand()
        Me.SqlConnection1 = New System.Data.SqlClient.SqlConnection()
        Me.SqlInsertCommand = New System.Data.SqlClient.SqlCommand()
        Me.SqlSelectCommand1 = New System.Data.SqlClient.SqlCommand()
        Me.sqlUpdateCommand = New System.Data.SqlClient.SqlCommand()
        Me.MainMenu1 = New System.Windows.Forms.MainMenu(Me.components)
        Me.MenuItem1 = New System.Windows.Forms.MenuItem()
        Me.miOpen = New System.Windows.Forms.MenuItem()
        Me.miView = New System.Windows.Forms.MenuItem()
        Me.miSend = New System.Windows.Forms.MenuItem()
        Me.miClose = New System.Windows.Forms.MenuItem()
        Me.MenuEdit = New System.Windows.Forms.MenuItem()
        Me.miSave = New System.Windows.Forms.MenuItem()
        Me.miCancel = New System.Windows.Forms.MenuItem()
        Me.miDelete = New System.Windows.Forms.MenuItem()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.StatusBar1 = New System.Windows.Forms.StatusBar()
        Me.StatusBarPanel1 = New System.Windows.Forms.StatusBarPanel()
        Me.StatusBarPanel2 = New System.Windows.Forms.StatusBarPanel()
        Me.DataGrid1 = New System.Windows.Forms.DataGrid()
        Me.DsIntranet1 = New InfoCtr.dsIntranet()
        Me.TableStyleView = New System.Windows.Forms.DataGridTableStyle()
        Me.DataGridTextBoxColumn7 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.URL1 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn9 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn1 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn11 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.TableStyleEdit = New System.Windows.Forms.DataGridTableStyle()
        Me.FormID = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn4 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.FormName = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.URL2 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn3 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn5 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn6 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.lblIinstruction = New System.Windows.Forms.Label()
        Me.btnAdd = New System.Windows.Forms.Button()
        Me.cboType = New System.Windows.Forms.ComboBox()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.HelpProvider1 = New System.Windows.Forms.HelpProvider()
        CType(Me.StatusBarPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.StatusBarPanel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGrid1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsIntranet1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'miAdd
        '
        Me.miAdd.Index = 0
        Me.miAdd.Text = "Add Document or Edit List"
        '
        'daMainIntranet
        '
        Me.daMainIntranet.DeleteCommand = Me.SqlDeleteCommand
        Me.daMainIntranet.InsertCommand = Me.SqlInsertCommand
        Me.daMainIntranet.SelectCommand = Me.SqlSelectCommand1
        Me.daMainIntranet.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "MainIntranet", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("FormID", "FormID"), New System.Data.Common.DataColumnMapping("FormName", "FormName"), New System.Data.Common.DataColumnMapping("FormDescription", "FormDescription"), New System.Data.Common.DataColumnMapping("FormCategory", "FormCategory"), New System.Data.Common.DataColumnMapping("SatelliteRegion", "SatelliteRegion"), New System.Data.Common.DataColumnMapping("SortOrder", "SortOrder"), New System.Data.Common.DataColumnMapping("URL", "URL")})})
        Me.daMainIntranet.UpdateCommand = Me.sqlUpdateCommand
        '
        'SqlDeleteCommand
        '
        Me.SqlDeleteCommand.CommandText = "dbo.MainIntranetDelete"
        Me.SqlDeleteCommand.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlDeleteCommand.Connection = Me.SqlConnection1
        Me.SqlDeleteCommand.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, False, CType(0, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing), New System.Data.SqlClient.SqlParameter("@FormID", System.Data.SqlDbType.Int, 4, "FormID")})
        '
        'SqlConnection1
        '
        Me.SqlConnection1.ConnectionString = "Data Source=SOLOMON;Initial Catalog=InfoCtr_be;Integrated Security=True"
        Me.SqlConnection1.FireInfoMessageEventOnUserErrors = False
        '
        'SqlInsertCommand
        '
        Me.SqlInsertCommand.CommandText = "dbo.MainIntranetInsert"
        Me.SqlInsertCommand.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlInsertCommand.Connection = Me.SqlConnection1
        Me.SqlInsertCommand.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, False, CType(0, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing), New System.Data.SqlClient.SqlParameter("@FormName", System.Data.SqlDbType.VarChar, 200, "FormName"), New System.Data.SqlClient.SqlParameter("@FormDescription", System.Data.SqlDbType.VarChar, 200, "FormDescription"), New System.Data.SqlClient.SqlParameter("@FormCategory", System.Data.SqlDbType.VarChar, 50, "FormCategory"), New System.Data.SqlClient.SqlParameter("@SatelliteRegion", System.Data.SqlDbType.VarChar, 50, "SatelliteRegion"), New System.Data.SqlClient.SqlParameter("@SortOrder", System.Data.SqlDbType.TinyInt, 1, "SortOrder"), New System.Data.SqlClient.SqlParameter("@URL", System.Data.SqlDbType.NVarChar, 2147483647, "URL")})
        '
        'SqlSelectCommand1
        '
        Me.SqlSelectCommand1.CommandText = "dbo.MainIntranet"
        Me.SqlSelectCommand1.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlSelectCommand1.Connection = Me.SqlConnection1
        Me.SqlSelectCommand1.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, False, CType(0, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing), New System.Data.SqlClient.SqlParameter("@Category", System.Data.SqlDbType.VarChar, 50)})
        '
        'sqlUpdateCommand
        '
        Me.sqlUpdateCommand.CommandText = "dbo.MainIntranetUpdate"
        Me.sqlUpdateCommand.CommandType = System.Data.CommandType.StoredProcedure
        Me.sqlUpdateCommand.Connection = Me.SqlConnection1
        Me.sqlUpdateCommand.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, False, CType(0, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing), New System.Data.SqlClient.SqlParameter("@FormID", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "FormID", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@FormName", System.Data.SqlDbType.VarChar, 200, "FormName"), New System.Data.SqlClient.SqlParameter("@FormDescription", System.Data.SqlDbType.VarChar, 200, "FormDescription"), New System.Data.SqlClient.SqlParameter("@FormCategory", System.Data.SqlDbType.VarChar, 50, "FormCategory"), New System.Data.SqlClient.SqlParameter("@SatelliteRegion", System.Data.SqlDbType.VarChar, 50, "SatelliteRegion"), New System.Data.SqlClient.SqlParameter("@SortOrder", System.Data.SqlDbType.TinyInt, 1, "SortOrder"), New System.Data.SqlClient.SqlParameter("@URL", System.Data.SqlDbType.VarChar, 2147483647, "URL")})
        '
        'MainMenu1
        '
        Me.MainMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem1, Me.MenuEdit})
        '
        'MenuItem1
        '
        Me.MenuItem1.Index = 0
        Me.MenuItem1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.miAdd, Me.miOpen, Me.miView, Me.miSend, Me.miClose})
        Me.MenuItem1.Text = "File"
        '
        'miOpen
        '
        Me.miOpen.Index = 1
        Me.miOpen.Text = "Open Selected Document"
        '
        'miView
        '
        Me.miView.Enabled = False
        Me.miView.Index = 2
        Me.miView.Text = "Save Changes and Return to List"
        '
        'miSend
        '
        Me.miSend.Index = 3
        Me.miSend.Text = "Send Selected Document as Attachment"
        '
        'miClose
        '
        Me.miClose.Index = 4
        Me.miClose.Text = "Close Window"
        '
        'MenuEdit
        '
        Me.MenuEdit.Enabled = False
        Me.MenuEdit.Index = 1
        Me.MenuEdit.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.miSave, Me.miCancel, Me.miDelete})
        Me.MenuEdit.Text = "Edit"
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
        Me.miDelete.Text = "Remove item from list"
        '
        'StatusBar1
        '
        Me.StatusBar1.Location = New System.Drawing.Point(0, 616)
        Me.StatusBar1.Name = "StatusBar1"
        Me.StatusBar1.Panels.AddRange(New System.Windows.Forms.StatusBarPanel() {Me.StatusBarPanel1, Me.StatusBarPanel2})
        Me.StatusBar1.ShowPanels = True
        Me.StatusBar1.Size = New System.Drawing.Size(1018, 22)
        Me.StatusBar1.TabIndex = 4
        Me.StatusBar1.Text = "StatusBar1"
        '
        'StatusBarPanel1
        '
        Me.StatusBarPanel1.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring
        Me.StatusBarPanel1.MinWidth = 100
        Me.StatusBarPanel1.Name = "StatusBarPanel1"
        Me.StatusBarPanel1.Text = "StatusBarPanel1"
        Me.StatusBarPanel1.Width = 991
        '
        'StatusBarPanel2
        '
        Me.StatusBarPanel2.Alignment = System.Windows.Forms.HorizontalAlignment.Right
        Me.StatusBarPanel2.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents
        Me.StatusBarPanel2.Name = "StatusBarPanel2"
        Me.StatusBarPanel2.Width = 10
        '
        'DataGrid1
        '
        Me.DataGrid1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGrid1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.DataGrid1.DataMember = ""
        Me.DataGrid1.DataSource = Me.DsIntranet1.tblIntranet
        Me.DataGrid1.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.DataGrid1.Location = New System.Drawing.Point(15, 72)
        Me.DataGrid1.Name = "DataGrid1"
        Me.DataGrid1.PreferredRowHeight = 15
        Me.DataGrid1.ReadOnly = True
        Me.DataGrid1.RowHeaderWidth = 15
        Me.DataGrid1.Size = New System.Drawing.Size(991, 529)
        Me.DataGrid1.TabIndex = 10
        Me.DataGrid1.TableStyles.AddRange(New System.Windows.Forms.DataGridTableStyle() {Me.TableStyleView, Me.TableStyleEdit})
        '
        'DsIntranet1
        '
        Me.DsIntranet1.DataSetName = "dsIntranet"
        Me.DsIntranet1.Locale = New System.Globalization.CultureInfo("en-US")
        Me.DsIntranet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'TableStyleView
        '
        Me.TableStyleView.DataGrid = Me.DataGrid1
        Me.TableStyleView.GridColumnStyles.AddRange(New System.Windows.Forms.DataGridColumnStyle() {Me.DataGridTextBoxColumn7, Me.URL1, Me.DataGridTextBoxColumn9, Me.DataGridTextBoxColumn1, Me.DataGridTextBoxColumn11})
        Me.TableStyleView.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.TableStyleView.MappingName = "tblIntranet"
        Me.TableStyleView.PreferredRowHeight = 20
        Me.TableStyleView.ReadOnly = True
        Me.TableStyleView.RowHeaderWidth = 15
        '
        'DataGridTextBoxColumn7
        '
        Me.DataGridTextBoxColumn7.Format = ""
        Me.DataGridTextBoxColumn7.FormatInfo = Nothing
        Me.DataGridTextBoxColumn7.HeaderText = "Category"
        Me.DataGridTextBoxColumn7.MappingName = "FormCategory"
        Me.DataGridTextBoxColumn7.Width = 75
        '
        'URL1
        '
        Me.URL1.Format = ""
        Me.URL1.FormatInfo = Nothing
        Me.URL1.HeaderText = "URL"
        Me.URL1.MappingName = "URL"
        Me.URL1.ReadOnly = True
        Me.URL1.Width = 0
        '
        'DataGridTextBoxColumn9
        '
        Me.DataGridTextBoxColumn9.Format = ""
        Me.DataGridTextBoxColumn9.FormatInfo = Nothing
        Me.DataGridTextBoxColumn9.HeaderText = "Name"
        Me.DataGridTextBoxColumn9.MappingName = "FormName"
        Me.DataGridTextBoxColumn9.ReadOnly = True
        Me.DataGridTextBoxColumn9.Width = 200
        '
        'DataGridTextBoxColumn1
        '
        Me.DataGridTextBoxColumn1.Format = ""
        Me.DataGridTextBoxColumn1.FormatInfo = Nothing
        Me.DataGridTextBoxColumn1.HeaderText = "Description"
        Me.DataGridTextBoxColumn1.MappingName = "FormDescription"
        Me.DataGridTextBoxColumn1.ReadOnly = True
        Me.DataGridTextBoxColumn1.Width = 550
        '
        'DataGridTextBoxColumn11
        '
        Me.DataGridTextBoxColumn11.Format = ""
        Me.DataGridTextBoxColumn11.FormatInfo = Nothing
        Me.DataGridTextBoxColumn11.HeaderText = "SatelliteRegion"
        Me.DataGridTextBoxColumn11.MappingName = "SatelliteRegion"
        Me.DataGridTextBoxColumn11.ReadOnly = True
        Me.DataGridTextBoxColumn11.Width = 125
        '
        'TableStyleEdit
        '
        Me.TableStyleEdit.DataGrid = Me.DataGrid1
        Me.TableStyleEdit.GridColumnStyles.AddRange(New System.Windows.Forms.DataGridColumnStyle() {Me.FormID, Me.DataGridTextBoxColumn4, Me.FormName, Me.URL2, Me.DataGridTextBoxColumn3, Me.DataGridTextBoxColumn5, Me.DataGridTextBoxColumn6})
        Me.TableStyleEdit.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.TableStyleEdit.PreferredRowHeight = 20
        Me.TableStyleEdit.RowHeaderWidth = 15
        '
        'FormID
        '
        Me.FormID.Format = ""
        Me.FormID.FormatInfo = Nothing
        Me.FormID.HeaderText = "FormID"
        Me.FormID.MappingName = "FormID"
        Me.FormID.Width = 0
        '
        'DataGridTextBoxColumn4
        '
        Me.DataGridTextBoxColumn4.Format = ""
        Me.DataGridTextBoxColumn4.FormatInfo = Nothing
        Me.DataGridTextBoxColumn4.HeaderText = "Category"
        Me.DataGridTextBoxColumn4.MappingName = "FormCategory"
        Me.DataGridTextBoxColumn4.Width = 75
        '
        'FormName
        '
        Me.FormName.Format = ""
        Me.FormName.FormatInfo = Nothing
        Me.FormName.HeaderText = "Formname"
        Me.FormName.MappingName = "FormName"
        Me.FormName.Width = 150
        '
        'URL2
        '
        Me.URL2.Format = ""
        Me.URL2.FormatInfo = Nothing
        Me.URL2.HeaderText = "URL"
        Me.URL2.MappingName = "URL"
        Me.URL2.Width = 375
        '
        'DataGridTextBoxColumn3
        '
        Me.DataGridTextBoxColumn3.Format = ""
        Me.DataGridTextBoxColumn3.FormatInfo = Nothing
        Me.DataGridTextBoxColumn3.HeaderText = "Description"
        Me.DataGridTextBoxColumn3.MappingName = "FormDescription"
        Me.DataGridTextBoxColumn3.Width = 250
        '
        'DataGridTextBoxColumn5
        '
        Me.DataGridTextBoxColumn5.Format = ""
        Me.DataGridTextBoxColumn5.FormatInfo = Nothing
        Me.DataGridTextBoxColumn5.HeaderText = "Region"
        Me.DataGridTextBoxColumn5.MappingName = "SatelliteRegion"
        Me.DataGridTextBoxColumn5.Width = 75
        '
        'DataGridTextBoxColumn6
        '
        Me.DataGridTextBoxColumn6.Format = ""
        Me.DataGridTextBoxColumn6.FormatInfo = Nothing
        Me.DataGridTextBoxColumn6.HeaderText = "SortOrder"
        Me.DataGridTextBoxColumn6.MappingName = "SortOrder"
        Me.DataGridTextBoxColumn6.NullText = ""
        Me.DataGridTextBoxColumn6.Width = 65
        '
        'lblIinstruction
        '
        Me.lblIinstruction.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblIinstruction.ForeColor = System.Drawing.Color.Red
        Me.lblIinstruction.Location = New System.Drawing.Point(104, 5)
        Me.lblIinstruction.Name = "lblIinstruction"
        Me.lblIinstruction.Size = New System.Drawing.Size(689, 17)
        Me.lblIinstruction.TabIndex = 15
        Me.lblIinstruction.Text = "Right Click the URL field in a NEW ROW to import the path\name from a File Open D" & _
    "ialog box."
        Me.lblIinstruction.Visible = False
        '
        'btnAdd
        '
        Me.btnAdd.Location = New System.Drawing.Point(865, 2)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(107, 32)
        Me.btnAdd.TabIndex = 6
        Me.btnAdd.Text = "Add Document"
        Me.btnAdd.UseVisualStyleBackColor = True
        '
        'cboType
        '
        Me.cboType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboType.FormattingEnabled = True
        Me.HelpProvider1.SetHelpString(Me.cboType, "select a Category to narrow your search.")
        Me.cboType.Location = New System.Drawing.Point(15, 45)
        Me.cboType.Name = "cboType"
        Me.HelpProvider1.SetShowHelp(Me.cboType, True)
        Me.cboType.Size = New System.Drawing.Size(176, 21)
        Me.cboType.TabIndex = 0
        '
        'txtSearch
        '
        Me.txtSearch.AcceptsReturn = True
        Me.HelpProvider1.SetHelpString(Me.txtSearch, "enter exact search string; wildcards won't work here, except fore and aft which a" & _
        "re automatically added.")
        Me.txtSearch.Location = New System.Drawing.Point(324, 46)
        Me.txtSearch.Name = "txtSearch"
        Me.HelpProvider1.SetShowHelp(Me.txtSearch, True)
        Me.txtSearch.Size = New System.Drawing.Size(194, 20)
        Me.txtSearch.TabIndex = 1
        Me.txtSearch.Text = "enter exact search string, no wildcards"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(19, 26)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(49, 13)
        Me.Label1.TabIndex = 9
        Me.Label1.Text = "Category"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(321, 28)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(75, 13)
        Me.Label2.TabIndex = 10
        Me.Label2.Text = "Simple Search"
        '
        'frmIntranet
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(1018, 638)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtSearch)
        Me.Controls.Add(Me.cboType)
        Me.Controls.Add(Me.btnAdd)
        Me.Controls.Add(Me.lblIinstruction)
        Me.Controls.Add(Me.StatusBar1)
        Me.Controls.Add(Me.DataGrid1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Menu = Me.MainMenu1
        Me.Name = "frmIntranet"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "STAFF FORMS - double click row to open item."
        CType(Me.StatusBarPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.StatusBarPanel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGrid1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsIntranet1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    'TODO ENHANCE add region filter
#Region "Load"
    'LOAD
    Private Sub frmIntranet_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'use datareader to get combobox list
        Dim dr As SqlDataReader
        Dim cmd As New SqlCommand("SELECT DISTINCT FormCategory from tblIntranet ORDER BY FormCategory", sc)
        da = Me.daMainIntranet
        da.SelectCommand.Connection = sc
        SetStatusBarText("Loading...")
        cmgr = CType(Me.BindingContext(Me.DsIntranet1, "tblIntranet"), CurrencyManager)

        'load cboType
        Me.cboType.Items.Add("All Documents")
        If Not SCConnect() Then
            Exit Sub
        End If
        Try
            dr = cmd.ExecuteReader()
            While dr.Read()
                If IsDBNull(dr(0)) Then
                Else
                    Me.cboType.Items.Add(dr.GetString(0))
                End If
            End While
        Catch ex As Exception
            modGlobalVar.msg("ERROR: load cbotype", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        dr.Close()
        sc.Close()

        'load datatable
        Try
            Me.daMainIntranet.Fill(Me.DsIntranet1.tblIntranet)
        Catch ex As Exception
            modGlobalVar.msg("ERROR: fill dsintranet", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        'SET DATAView VARIABLE FOR FILTERING DATAGRIDS
        ' strDGM = " STAFF FORMS "
        dvM = New DataView(Me.DsIntranet1.tblIntranet)
        '  dvM.RowFilter = "FormCategory ='Request'"
        ' Me.DataGrid1.DataSource = dvM

        SetStatusBarText(strView)
        iRow = 0

FromSetup:
        Dim tbx As DataGridTextBoxColumn
        For Each tbx In DataGrid1.TableStyles(0).GridColumnStyles
            AddHandler tbx.TextBox.MouseDown, New MouseEventHandler(AddressOf DataGridDouble)
            tbx.TextBox.Enabled = False
        Next
        itblStyle = 0
        Me.sqlUpdateCommand.Connection = sc
        Me.SqlInsertCommand.Connection = sc
        Me.SqlDeleteCommand.Connection = sc

        Me.cboType.SelectedIndex = Me.cboType.FindStringExact("Request")
        LoadGrid()

        iURL = GetURLColID()

        Forms.Add(Me)
        SetStatusBarText("Done")

    End Sub

    'CLOSING
    Private Sub frmIntranet_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        SetStatusBarText("Closing")
        If Me.StatusBarPanel2.Text = strEdit Then
            If Me.DsIntranet1.HasChanges Or CType(cmgr.Current, DataRowView).Row.HasVersion(DataRowVersion.Proposed) Then
                Select Case modGlobalVar.msg("Closing window.  Save changes?", "to Discard Changes click No", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
                    Case Is = DialogResult.Yes
                        If DoUpdate() Then
                        Else
                            If modGlobalVar.msg("SAVE ERROR - OK to close anyway?", "your changes may not have been saved", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) = DialogResult.Yes Then
                            Else
                                e.Cancel = True
                                Exit Sub
                            End If
                        End If
                    Case Is = DialogResult.No
                    Case Is = MsgBoxResult.Cancel
                        e.Cancel = True
                End Select
            End If
        End If
        SetStatusBarText("done")
    End Sub

    'CALL CLOSE
    Private Sub miClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles miClose.Click
        Me.Close()
    End Sub

    'cboTYPE change
    Private Sub cboType_Selectedchangecommitted(ByVal sender As Object, ByVal e As System.EventArgs) _
        Handles cboType.SelectionChangeCommitted
        LoadGrid() '(sender, e)
    End Sub

#End Region

#Region "Grid"

    'GRID DOUBLECLICK
    Private Sub DataGrid1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) _
    Handles DataGrid1.DoubleClick, miOpen.Click
        MouseWait()
        If Me.StatusBarPanel1.Text = strEdit Then
            Exit Sub 'don't open form during dataentry
        End If

        If IsNull(Me.DataGrid1.CurrentRowIndex, -1) > -1 Then
        Else
            modGlobalVar.msg("ATTENTION: Invalid selection", "Select a document and try again", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        SetStatusBarText("Opening form")
        ' Dim dt As DataTable = Me.DsIntranet1.Tables(0)
        Dim strFile As String = DataGrid1.Item(DataGrid1.CurrentRowIndex, iURL)

        Try
            If strFile.Contains(".pptx") Then 'open power point in running mode
                System.Diagnostics.Process.Start("powerpnt", "/s """ & strFile & """") 'DataGrid1.Item(DataGrid1.CurrentRowIndex, iURL) & """")
            Else
                System.Diagnostics.Process.Start(strFile) 'DataGrid1.Item(DataGrid1.CurrentRowIndex, iURL)) 'dt.Columns("URL").Ordinal))
            End If
        Catch ex As Exception
            Me.StatusBarPanel1.Text = "second attempt:" + ex.Message
            Try
                System.Diagnostics.Process.Start(strFile) 'DataGrid1.Item(DataGrid1.CurrentRowIndex, iURL)) 'dt.Columns("URL").Ordinal))
            Catch exc As Exception
                modGlobalVar.msg("ERROR: opening document.", exc.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Try
        SetStatusBarText("Done")
        MouseDefault()
    End Sub

    'set double click
    Private Sub DataGridDouble(ByVal sender As Object, ByVal e As MouseEventArgs)
        If (DateTime.Now < modGlobalVar.CheckDouble(sender, e).AddMilliseconds(SystemInformation.DoubleClickTime)) Then
            Me.miOpen.PerformClick()
        End If
    End Sub

    'CLEAR SELECTION FROM dataGRIDS AND LOSE FOCUS (note: selection stays with arrow)
    Private Sub grdUnselect_Click(ByVal sender As Object, ByVal e As System.EventArgs) _
            Handles MyBase.Click
        If Me.DataGrid1.CurrentRowIndex > -1 Then
            Me.DataGrid1.UnSelect(DataGrid1.CurrentRowIndex)
            Me.DataGrid1.NavigateBack()
        End If
    End Sub

    'PREVENT MULTIPLE ROW SELECTION
    Private Sub DataGrid1_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) _
            Handles DataGrid1.MouseUp
        If iRow = sender.CurrentRowIndex Then
        Else
            For i As Integer = 0 To sender.VisibleRowCount() - 1
                sender.unselect(i)
            Next i
            iRow = sender.CurrentRowIndex
        End If
        '  sender.select(sender.currentrowindex)
    End Sub

    'RE-LOAD GRID
    Private Sub LoadGrid() '(ByVal obj As Object, ByVal ea As EventArgs)
        Me.txtSearch.Text = String.Empty
        dvM.RowFilter = Nothing
        Select Case cboType.Text
            Case Is = String.Empty
                cboType.SelectedIndex = cboType.FindStringExact(strLastCategory)
                dvM.RowFilter = "FormCategory ='" & strLastCategory & "'"
                Me.DataGrid1.DataSource = dvM
                sourceRowCount = dvM.Count
            Case Is = "All Documents"
                Me.DataGrid1.DataSource = Me.DsIntranet1.tblIntranet
                sourceRowCount = Me.DsIntranet1.tblIntranet.Rows.Count
            Case Else
                dvM.RowFilter = "FormCategory ='" & Me.cboType.Text & "'"
                Me.DataGrid1.DataSource = dvM
                sourceRowCount = dvM.Count
        End Select
        'If Me.cboType.Text = "All Documents" Then
        '    Me.DataGrid1.DataSource = Me.DsIntranet1.tblIntranet
        '    sourceRowCount = Me.DsIntranet1.tblIntranet.Rows.Count
        'Else
        '    dvM.RowFilter = "FormCategory ='" & Me.cboType.Text & "'"   'Me.ListBox1.SelectedItem & "'"
        '    Me.DataGrid1.DataSource = dvM
        '    sourceRowCount = dvM.Count
        'End If
        Me.DataGrid1.CaptionText = sourceRowCount.ToString
    End Sub

    'get colID of  url depending on current tablestyle
    Private Function GetURLColID() As Integer

        cURL = Me.DataGrid1.TableStyles(itblStyle).GridColumnStyles("URL")
        GetURLColID = DataGrid1.TableStyles(itblStyle).GridColumnStyles.IndexOf(cURL)

    End Function


#End Region 'grid; open doc

#Region "Filter Grid"

    'CAPTURE RIGHT MOUSE CLICK TO FILTER or CLEAR FILTER or SHOW URL
    Protected Sub grdAll_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles DataGrid1.MouseDown

        Dim tbl As Object
        ' Dim strHdr As String    'text for grid header

        MouseWait()
        hti = sender.HitTest(e.X, e.Y)
        tbl = Me.DsIntranet1.tblIntranet


        If hti.Type = DataGrid.HitTestType.Cell Then

            If e.Button.ToString = "Right" Then   '====Right: SET FILTER ====
                GoTo SetFilter
            Else '====Left: SHOW URL ====
                If usr = ITDir.StaffID Or usr = DBAdmin.StaffID Then
                    'If Me.DataGrid1.CaptionText.Contains("Add document") Then
                    '    cURL = Me.DataGrid1.TableStyles(1).GridColumnStyles("URL")
                    '    iURL = DataGrid1.TableStyles(1).GridColumnStyles.IndexOf(cURL)
                    'Else
                    '    cURL = Me.DataGrid1.TableStyles(0).GridColumnStyles("URL")
                    '    iURL = DataGrid1.TableStyles(0).GridColumnStyles.IndexOf(cURL)

                    'End If
                    SetStatusBarText(Me.DataGrid1(hti.Row, iURL))
                    ' Me.DataGrid1.TableStyles(itblStyle).GridColumnStyles(hti.Column).MappingName = "URL"
                End If
                GoTo CloseAll
            End If
        Else   '=====not in cell: CLEAR FILTER =====
            LoadGrid()
            sender.CaptionText = sourceRowCount.ToString & "  " & strDGM
            SetStatusBarText("")
            GoTo CloseAll
        End If


SetFilter:

        '  If e.Button.ToString = "Right" Then   'MouseButtons.Right Then
        'hti = sender.HitTest(e.X, e.Y)
        '  tbl = Me.DsIntranet1.tblIntranet
        'strHdr = strDGM

        'SET FILTER
        '  If hti.Type = DataGrid.HitTestType.Cell Then
        'OPEN FILE DIALOG
        If Me.DataGrid1.TableStyles(itblStyle).GridColumnStyles(hti.Column).MappingName.Contains("URL") Then '=== ADMIN has in Edit mode
            Dim fo As New OpenFileDialog
            fo.Title = "Select file"
            fo.InitialDirectory = "\\ICCNAS1\Users\" & Environment.UserName
            fo.Filter = "All files (*.*)|*.*"
            fo.FileName = ""
            If fo.ShowDialog = System.Windows.Forms.DialogResult.OK Then
                Me.DataGrid1.Item(hti.Row, hti.Column) = fo.FileName
            End If
            fo = Nothing
        Else
            'FILTER
            If IsDBNull(sender.Item(hti.Row, hti.Column)) Then 'nulls cause filter error
                modGlobalVar.msg(MsgCodes.filterEmpty)
            Else
                grdFilter(sender, Me.DataGrid1.DataSource, tbl, strDGM)
            End If
        End If

        'Else            'not in cell,  'CLEAR FILTER
        '    sender.DataSource = tbl 'removes dv.rowfilter
        '    sender.CaptionText = tbl.Rows.Count.ToString & "  " & strHdr 'DsOrgSearch1.tblOrg.Rows.Count.ToString
        '    SetStatusBarText("")
        '    'Select Case strHdr
        '    'Case Is = strDGM
        '    statusM = ""
        '    '        If Me.chkDetail.Checked Then
        '    '            grdFilterClear()    'clear child grids 
        '    '        End If
        '    '    Case Is = strDGS1
        '    '        statusS1 = ""
        '    '    Case Is = strDGS2
        '    '        statusS2 = ""
        '    'End Select
        '    SetStatusBarText(statusM)
        'End If
        '    Else    'is not right mouse button, capture doubleclick
        '    '    strbActiveGrid.Replace(strbActiveGrid.ToString, sender.name)    'use this for doubleclick code
        '    MouseDefault()
        '    End If

Closeall:
        MouseDefault()
    End Sub

    'FILTER METHOD
    Private Sub grdFilter(ByVal grd As Object, ByVal ds As Object, ByVal tbl As Object, ByVal strHdr As String)
        Dim strFilter As String
        Dim myColumns As GridColumnStylesCollection
        Dim iCnt As Integer

        If Me.DataGrid1.DataSource.GetType.ToString = "System.Data.DataView" Then
            myColumns = DataGrid1.TableStyles(itblStyle).GridColumnStyles
            strFilter = myColumns(hti.Column).MappingName
            strFilter = strFilter & " = '" & grd.Item(hti.Row, hti.Column) & " '"
            dvM.RowFilter = strFilter
            iCnt = dvM.Count
        Else
            Dim dv As New DataView(Me.DataGrid1.DataSource)
            myColumns = DataGrid1.TableStyles(itblStyle).GridColumnStyles
            strFilter = myColumns(hti.Column).MappingName
            strFilter = strFilter & " = '" & grd.Item(hti.Row, hti.Column) & " '"
            dv.RowFilter = strFilter
            grd.datasource = dv
            iCnt = dv.Count
        End If
        Try
            grd.CaptionText = iCnt.ToString & "/" & sourceRowCount & " " & strHdr 'tbl.Rows.Count.ToString & "  " & strHdr
            statusM = strHdr & " filtered on " & myColumns(hti.Column).HeaderText
        Catch ex As Exception
            modGlobalVar.msg("ERROR: filter error", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        SetStatusBarText(statusM)

    End Sub


#End Region 'filter grid

#Region "Search field"

    'SELECT ALL TEXT ON ENTER
    Private Sub txtSearch_Enter(sender As Object, e As System.EventArgs) Handles txtSearch.Enter
        Me.txtSearch.Text = String.Empty
        ' Me.txtSearch.SelectAll()
        strLastCategory = Me.cboType.Text
        '  Me.cboType.SelectedIndex = 0
    End Sub

    'ALLOW ENTER TO LEAVE FIELD
    Private Sub txtSearch_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles txtSearch.KeyDown
        Dim sUsr As String = Me.txtSearch.Text

        Select Case e.KeyData.ToString
            Case Is = "Return"  ' = System.Windows.Forms.Keys.Enter Then

                Me.DataGrid1.Focus()
                Me.txtSearch.Text = sUsr 'why does "return" clear txtsearch.text???
            Case Else
                ' MsgBox(e.KeyData.ToString, , "Not return")
        End Select
    End Sub

    'FILTER GRID via text field
    Private Sub txtSearch_Leave(sender As System.Object, e As System.EventArgs) Handles txtSearch.Leave
        If sender.text Is String.Empty Then
            Exit Sub
        End If
        Dim s As String = Replace(sender.text, "*", "")
        ' Me.DataGrid1.DataSource = Me.DsIntranet1.tblIntranet.Select("FormDescription like '%" & s & "%'")
        Me.cboType.SelectedIndex = -1

        dvM.RowFilter = Nothing
        dvM.RowFilter = "FormDescription like '%" & s & "%' or FormName like '%" & s & "%'"    'Me.ListBox1.SelectedItem & "'"
        Me.DataGrid1.DataSource = dvM
        Me.TableStyleView.MappingName = "tblIntranet"
        Me.TableStyleEdit.MappingName = Nothing
        itblStyle = 0
        sourceRowCount = dvM.Count
        Me.DataGrid1.CaptionText = sourceRowCount.ToString & " " & sender.text
        iURL = GetURLColID()
    End Sub

#End Region


#Region "Add/SAVE Document"

    'btn ADD/EDIT
    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles miView.Click, miAdd.Click, btnAdd.Click

        'VERIFY USER HAS PERMISSION
        SetStatusBarText("Verifying permission")
        Select Case usr
            Case Is = ITDir.StaffID, DBAdmin.StaffID
                SetStatusBarText(usrFirst & " editing or adding forms") 'Forms, Hints, Files
                Me.StatusBarPanel2.Text = strEdit
            Case Else
                modGlobalVar.msg("Unauthorized entry", "Sorry, contact " & ITDir.StaffName & " or " & DBAdmin.StaffName & "  to add a document.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '  Me.StatusBar1.Text = "Done"
                SetStatusBarText(usr.ToString & " Permission denied")
                Exit Sub
        End Select


        Select Case sender.Text
            Case Is = Me.miAdd.Text, "Add Document"
BeginEdit:      'VERIFY USER HAS PERMISSION

                'SHOW EXTRA FIELDS
                Me.DataGrid1.ReadOnly = False
                Me.TableStyleView.MappingName = Nothing
                Me.TableStyleEdit.MappingName = "tblIntranet"
                itblStyle = 1

                '    Me.DataGrid1.CaptionText = "Add document or edit existing grid entries."
                Me.miView.Enabled = True
                Me.miAdd.Enabled = False
                Me.MenuEdit.Enabled = True
                Me.lblIinstruction.Visible = True
                btnAdd.Text = "Save edits and return to list"
                iURL = GetURLColID()
            Case Is = miView.Text, "Save edits and return to list"
EndEdit:        'SAVE CHANGES
                If DoUpdate() Then
                Else
                    modGlobalVar.msg("Error: problem saving changes", "staying in edit mode", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If

                'HIDE EXTRA COLUMNS
                Me.TableStyleEdit.MappingName = Nothing
                Me.TableStyleView.MappingName = "tblIntranet"
                itblStyle = 0

                Me.DataGrid1.ReadOnly = True
                'RESET HEADINGS
                Me.miView.Enabled = False
                Me.miAdd.Enabled = True
                Me.MenuEdit.Enabled = False
                Me.StatusBarPanel2.Text = strView
                SetStatusBarText("")
                Me.lblIinstruction.Visible = False
                btnAdd.Text = "Add Document"
                iURL = GetURLColID()
            Case Else
                modGlobalVar.msg("ERROR: not found", sender.text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Select
        '    cURL = Me.DataGrid1.TableStyles("tblIntranet").GridColumnStyles("URL")
        '    iURL = DataGrid1.TableStyles("tblIntranet").GridColumnStyles.IndexOf(cURL)

    End Sub

    'SAVE CHANGES
    Private Function DoUpdate() As Boolean

        Try
            Me.DataGrid1.CurrentRowIndex = DataGrid1.CurrentRowIndex + 1    'captures changes w/out leaving row
        Catch ex2 As Exception
        End Try
        cmgr.EndCurrentEdit()

        Me.StatusBar1.Text = "updating"
        Try
            Me.daMainIntranet.Update(Me.DsIntranet1.tblIntranet)
            Return True
        Catch ex As Exception
            modGlobalVar.msg("ERROR intranet Update incomplete", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.StatusBar1.Text = "Not Saved"
            Return False
        End Try

    End Function



#End Region

#Region "Send Form"

    Private Sub btnEmail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles miSend.Click
        Dim SendEmail As New ClassEmail
        Dim iCol As Integer = DataGrid1.TableStyles(0).GridColumnStyles.IndexOf(Me.DataGrid1.TableStyles(0).GridColumnStyles("FormName"))

        If IsNull(Me.DataGrid1.CurrentRowIndex, -1) > -1 Then
            Try
                ' SendEmail.SendEmailMessage(usrFirst, usrFirst, Me.DsIntranet1.tblIntranet.Columns("FormName").ToString, "File is attached", DataGrid1.Item(DataGrid1.CurrentRowIndex, iURL).ToString)
                modPopup.EmailOutlookAttachment("", Me.DataGrid1(hti.Row, iCol), "File is attached", DataGrid1.Item(hti.Row, iURL).ToString)
            Catch ex As Exception
                modGlobalVar.msg("ERROR: email failed", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        Else
            modGlobalVar.msg("nothing selected", "Please select a document in the grid and try again", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
    End Sub

#End Region 'send form


#Region "General"

    'SET STATUS BAR LEFT TEXT re FILTERS
    Private Sub SetStatusBarText(ByVal s As String)
        Me.StatusBarPanel1.Text = s 'statusM '& " " & statusS1 & " " & statusS2
    End Sub

    'delete
    Private Sub miDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles miDelete.Click
        '    'default delete doesn't work

        Dim dr As DataRow = Me.DsIntranet1.tblIntranet.FindByFormID(Me.DataGrid1.Item(Me.DataGrid1.CurrentRowIndex, 0))
        dr.Item("FormName") = "Delete " & dr.Item("FormName")
        cmgr.EndCurrentEdit()
        '  Me.DsIntranet1.tblIntranet.AcceptChanges()
        '    dr = Nothing
        Try
            '    '    'does'nt work either: Me.DsIntranet1.tblIntranet.RemovetblIntranetRow(Me.DsIntranet1.tblIntranet.FindByFormID(Me.DataGrid1.Item(Me.DataGrid1.CurrentRowIndex, 0)))
            Me.daMainIntranet.Update(Me.DsIntranet1.tblIntranet)
            '    '     Me.DsIntranet1.tblIntranet.FindByFormID(Me.DataGrid1.Item(Me.DataGrid1.CurrentRowIndex, Me.DataGrid1.CurrentCell.ColumnNumber)).FormName = "Delete " & 
            '    '    me.DsIntranet1.tblIntranet.Rows(0).Item("FormName") = "Delete " & 
        Catch ex As Exception
            modGlobalVar.msg("ERROR: delete", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

#End Region 'general

End Class
