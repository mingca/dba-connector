<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmReviewOrgSimple
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
        Forms.Remove(Me)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmReviewOrgSimple))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.TblOrgBindingNavigator = New System.Windows.Forms.BindingNavigator(Me.components)
        Me.tblOrgBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.dsReviewOrgIntern = New InfoCtr.dsReviewOrgIntern()
        Me.BindingNavigatorCountItem = New System.Windows.Forms.ToolStripLabel()
        Me.toolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.tscboWhat = New System.Windows.Forms.ToolStripComboBox()
        Me.tscboRegion = New System.Windows.Forms.ToolStripComboBox()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsSaveItem = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.BindingNavigatorMoveFirstItem = New System.Windows.Forms.ToolStripButton()
        Me.BindingNavigatorMovePreviousItem = New System.Windows.Forms.ToolStripButton()
        Me.BindingNavigatorSeparator = New System.Windows.Forms.ToolStripSeparator()
        Me.BindingNavigatorPositionItem = New System.Windows.Forms.ToolStripTextBox()
        Me.BindingNavigatorSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.BindingNavigatorMoveNextItem = New System.Windows.Forms.ToolStripButton()
        Me.BindingNavigatorMoveLastItem = New System.Windows.Forms.ToolStripButton()
        Me.BindingNavigatorSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsTxtSearch = New System.Windows.Forms.ToolStripTextBox()
        Me.tsBtnSearch = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsBtnViewAll = New System.Windows.Forms.ToolStripButton()
        Me.tslblFilter = New System.Windows.Forms.ToolStripLabel()
        Me.toolStripSeparator = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsBtnHelp = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton1 = New System.Windows.Forms.ToolStripButton()
        Me.tsReload = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton2 = New System.Windows.Forms.ToolStripButton()
        Me.lblMainGrid = New System.Windows.Forms.Label()
        Me.StatusBar1 = New System.Windows.Forms.StatusBar()
        Me.StatusBarPanel1 = New System.Windows.Forms.StatusBarPanel()
        Me.StatusBarPanel2 = New System.Windows.Forms.StatusBarPanel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.grdvwSec = New System.Windows.Forms.DataGridView()
        Me.ContactID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.OrgNum = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ActiveDataGridViewCheckBoxColumn = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.DataGridViewTextBoxColumn28 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn14 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.LastName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn19 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn20 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewCheckBoxColumn6 = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.NotesDataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Street1DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CityDataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.StateDataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ZipDataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewCheckBoxColumn7 = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.PhoneDataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.FaxDataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.EmailDataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn25 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn21 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn22 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GoesbyDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn15 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn17 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewCheckBoxColumn8 = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.DataGridViewCheckBoxColumn9 = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.OrgName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn23 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn26 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn27 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CreateDateDataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CreateStaffNumDataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.LastChangeDateDataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.LastChangeStaffNumDataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn29 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SortFldDataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.BS3 = New System.Windows.Forms.BindingSource(Me.components)
        Me.RelOrgContactBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.pnlOrg = New System.Windows.Forms.Panel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btnMovePerson = New System.Windows.Forms.Button()
        Me.btnGoodBadFilter = New System.Windows.Forms.Button()
        Me.fldBadID = New System.Windows.Forms.TextBox()
        Me.fldGoodID = New System.Windows.Forms.TextBox()
        Me.btnOKDeleteOrg = New System.Windows.Forms.Button()
        Me.grdvwMain = New System.Windows.Forms.DataGridView()
        Me.OrgID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Active = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.OrgType = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.OrgNameDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Street1DataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CityDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.StateDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ZipDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PhysicalAddress = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PhysicalZip = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PhoneDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.FaxDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.WebsiteDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.EmailDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.NotesDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.AttendanceDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MembershipDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.HouseholdsDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.AnnualBudgetDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DenominationDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CountyDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SatelliteRegionDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.EINDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MapURLDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.LayLeadershipChangesDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MailPreferenceDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.EmailPreferenceDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CountryDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SortFldDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Programs = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DateFounded = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.InfoSource = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CongrEthnicity = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CongrCulture = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CreateDateDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CreateStaffNumDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.LastChangeDateDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.LastChangeStaffNumDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ReviewDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ReviewStaffNum = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ReviewResult = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.OrgIDDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Contact = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ContactIDDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ContactIDDataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.FillByZipToolStrip = New System.Windows.Forms.ToolStrip()
        Me.ZipToolStripLabel = New System.Windows.Forms.ToolStripLabel()
        Me.ZipToolStripTextBox = New System.Windows.Forms.ToolStripTextBox()
        Me.FillByZipToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.BindingNavigatorAddNewItem = New System.Windows.Forms.ToolStripButton()
        Me.BindingNavigatorDeleteItem = New System.Windows.Forms.ToolStripButton()
        Me.TblContactBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.OrgIDDataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TblOrgTableAdapter = New InfoCtr.dsReviewOrgInternTableAdapters.tblOrgTableAdapter()
        Me.TblContactTableAdapter = New InfoCtr.dsReviewOrgInternTableAdapters.tblContactTableAdapter()
        CType(Me.TblOrgBindingNavigator, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TblOrgBindingNavigator.SuspendLayout()
        CType(Me.tblOrgBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dsReviewOrgIntern, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.StatusBarPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.StatusBarPanel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdvwSec, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BS3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RelOrgContactBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlOrg.SuspendLayout()
        CType(Me.grdvwMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FillByZipToolStrip.SuspendLayout()
        CType(Me.TblContactBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TblOrgBindingNavigator
        '
        Me.TblOrgBindingNavigator.AddNewItem = Nothing
        Me.TblOrgBindingNavigator.BindingSource = Me.tblOrgBindingSource
        Me.TblOrgBindingNavigator.CountItem = Me.BindingNavigatorCountItem
        Me.TblOrgBindingNavigator.DeleteItem = Nothing
        Me.TblOrgBindingNavigator.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.toolStripSeparator3, Me.tscboWhat, Me.tscboRegion, Me.ToolStripSeparator2, Me.tsSaveItem, Me.ToolStripSeparator4, Me.BindingNavigatorMoveFirstItem, Me.BindingNavigatorMovePreviousItem, Me.BindingNavigatorSeparator, Me.BindingNavigatorPositionItem, Me.BindingNavigatorCountItem, Me.BindingNavigatorSeparator1, Me.BindingNavigatorMoveNextItem, Me.BindingNavigatorMoveLastItem, Me.BindingNavigatorSeparator2, Me.tsTxtSearch, Me.tsBtnSearch, Me.ToolStripSeparator1, Me.tsBtnViewAll, Me.tslblFilter, Me.toolStripSeparator, Me.ToolStripSeparator5, Me.tsBtnHelp, Me.ToolStripButton1, Me.tsReload, Me.ToolStripButton2})
        Me.TblOrgBindingNavigator.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.TblOrgBindingNavigator.Location = New System.Drawing.Point(0, 0)
        Me.TblOrgBindingNavigator.MoveFirstItem = Me.BindingNavigatorMoveFirstItem
        Me.TblOrgBindingNavigator.MoveLastItem = Me.BindingNavigatorMoveLastItem
        Me.TblOrgBindingNavigator.MoveNextItem = Me.BindingNavigatorMoveNextItem
        Me.TblOrgBindingNavigator.MovePreviousItem = Me.BindingNavigatorMovePreviousItem
        Me.TblOrgBindingNavigator.Name = "TblOrgBindingNavigator"
        Me.TblOrgBindingNavigator.PositionItem = Me.BindingNavigatorPositionItem
        Me.TblOrgBindingNavigator.Size = New System.Drawing.Size(1148, 23)
        Me.TblOrgBindingNavigator.Stretch = True
        Me.TblOrgBindingNavigator.TabIndex = 0
        Me.TblOrgBindingNavigator.Text = "BindingNavigator1"
        '
        'tblOrgBindingSource
        '
        Me.tblOrgBindingSource.DataMember = "tblOrg"
        Me.tblOrgBindingSource.DataSource = Me.dsReviewOrgIntern
        '
        'dsReviewOrgIntern
        '
        Me.dsReviewOrgIntern.DataSetName = "dsReviewOrgIntern"
        Me.dsReviewOrgIntern.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'BindingNavigatorCountItem
        '
        Me.BindingNavigatorCountItem.Name = "BindingNavigatorCountItem"
        Me.BindingNavigatorCountItem.Size = New System.Drawing.Size(35, 15)
        Me.BindingNavigatorCountItem.Text = "of {0}"
        Me.BindingNavigatorCountItem.ToolTipText = "Total number of items"
        '
        'toolStripSeparator3
        '
        Me.toolStripSeparator3.Name = "toolStripSeparator3"
        Me.toolStripSeparator3.Size = New System.Drawing.Size(6, 23)
        '
        'tscboWhat
        '
        Me.tscboWhat.Items.AddRange(New Object() {"Organizations", "Orgs to Delete"})
        Me.tscboWhat.Name = "tscboWhat"
        Me.tscboWhat.Size = New System.Drawing.Size(121, 23)
        Me.tscboWhat.Text = "Organizations"
        '
        'tscboRegion
        '
        Me.tscboRegion.Items.AddRange(New Object() {"All Regions", "Central", "NE", "NW", "SE", "SW", "South", "Not in Region"})
        Me.tscboRegion.Name = "tscboRegion"
        Me.tscboRegion.Size = New System.Drawing.Size(121, 23)
        Me.tscboRegion.Text = "select Region"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 23)
        '
        'tsSaveItem
        '
        Me.tsSaveItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsSaveItem.Image = CType(resources.GetObject("tsSaveItem.Image"), System.Drawing.Image)
        Me.tsSaveItem.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.tsSaveItem.Name = "tsSaveItem"
        Me.tsSaveItem.Size = New System.Drawing.Size(23, 20)
        Me.tsSaveItem.Text = "Save Data"
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(6, 23)
        '
        'BindingNavigatorMoveFirstItem
        '
        Me.BindingNavigatorMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorMoveFirstItem.Image = CType(resources.GetObject("BindingNavigatorMoveFirstItem.Image"), System.Drawing.Image)
        Me.BindingNavigatorMoveFirstItem.Name = "BindingNavigatorMoveFirstItem"
        Me.BindingNavigatorMoveFirstItem.RightToLeftAutoMirrorImage = True
        Me.BindingNavigatorMoveFirstItem.Size = New System.Drawing.Size(23, 20)
        Me.BindingNavigatorMoveFirstItem.Text = "Move first"
        '
        'BindingNavigatorMovePreviousItem
        '
        Me.BindingNavigatorMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorMovePreviousItem.Image = CType(resources.GetObject("BindingNavigatorMovePreviousItem.Image"), System.Drawing.Image)
        Me.BindingNavigatorMovePreviousItem.Name = "BindingNavigatorMovePreviousItem"
        Me.BindingNavigatorMovePreviousItem.RightToLeftAutoMirrorImage = True
        Me.BindingNavigatorMovePreviousItem.Size = New System.Drawing.Size(23, 20)
        Me.BindingNavigatorMovePreviousItem.Text = "Move previous"
        '
        'BindingNavigatorSeparator
        '
        Me.BindingNavigatorSeparator.Name = "BindingNavigatorSeparator"
        Me.BindingNavigatorSeparator.Size = New System.Drawing.Size(6, 23)
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
        Me.BindingNavigatorSeparator1.Size = New System.Drawing.Size(6, 23)
        '
        'BindingNavigatorMoveNextItem
        '
        Me.BindingNavigatorMoveNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorMoveNextItem.Image = CType(resources.GetObject("BindingNavigatorMoveNextItem.Image"), System.Drawing.Image)
        Me.BindingNavigatorMoveNextItem.Name = "BindingNavigatorMoveNextItem"
        Me.BindingNavigatorMoveNextItem.RightToLeftAutoMirrorImage = True
        Me.BindingNavigatorMoveNextItem.Size = New System.Drawing.Size(23, 20)
        Me.BindingNavigatorMoveNextItem.Text = "Move next"
        '
        'BindingNavigatorMoveLastItem
        '
        Me.BindingNavigatorMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorMoveLastItem.Image = CType(resources.GetObject("BindingNavigatorMoveLastItem.Image"), System.Drawing.Image)
        Me.BindingNavigatorMoveLastItem.Name = "BindingNavigatorMoveLastItem"
        Me.BindingNavigatorMoveLastItem.RightToLeftAutoMirrorImage = True
        Me.BindingNavigatorMoveLastItem.Size = New System.Drawing.Size(23, 20)
        Me.BindingNavigatorMoveLastItem.Text = "Move last"
        '
        'BindingNavigatorSeparator2
        '
        Me.BindingNavigatorSeparator2.Name = "BindingNavigatorSeparator2"
        Me.BindingNavigatorSeparator2.Size = New System.Drawing.Size(6, 23)
        '
        'tsTxtSearch
        '
        Me.tsTxtSearch.Name = "tsTxtSearch"
        Me.tsTxtSearch.Size = New System.Drawing.Size(100, 23)
        Me.tsTxtSearch.Text = "type search text here"
        Me.tsTxtSearch.ToolTipText = "use wildcard"
        '
        'tsBtnSearch
        '
        Me.tsBtnSearch.Image = CType(resources.GetObject("tsBtnSearch.Image"), System.Drawing.Image)
        Me.tsBtnSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.tsBtnSearch.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsBtnSearch.Name = "tsBtnSearch"
        Me.tsBtnSearch.Size = New System.Drawing.Size(87, 20)
        Me.tsBtnSearch.Tag = "Search selected column"
        Me.tsBtnSearch.Text = "Find Org ID"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 23)
        '
        'tsBtnViewAll
        '
        Me.tsBtnViewAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.tsBtnViewAll.Image = CType(resources.GetObject("tsBtnViewAll.Image"), System.Drawing.Image)
        Me.tsBtnViewAll.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsBtnViewAll.Name = "tsBtnViewAll"
        Me.tsBtnViewAll.Size = New System.Drawing.Size(85, 19)
        Me.tsBtnViewAll.Text = "Cancel Search"
        Me.tsBtnViewAll.Visible = False
        '
        'tslblFilter
        '
        Me.tslblFilter.Name = "tslblFilter"
        Me.tslblFilter.Size = New System.Drawing.Size(10, 15)
        Me.tslblFilter.Text = " "
        '
        'toolStripSeparator
        '
        Me.toolStripSeparator.Name = "toolStripSeparator"
        Me.toolStripSeparator.Size = New System.Drawing.Size(6, 23)
        '
        'ToolStripSeparator5
        '
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        Me.ToolStripSeparator5.Size = New System.Drawing.Size(6, 23)
        '
        'tsBtnHelp
        '
        Me.tsBtnHelp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsBtnHelp.Image = CType(resources.GetObject("tsBtnHelp.Image"), System.Drawing.Image)
        Me.tsBtnHelp.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsBtnHelp.Name = "tsBtnHelp"
        Me.tsBtnHelp.Size = New System.Drawing.Size(23, 20)
        Me.tsBtnHelp.Tag = "Help"
        '
        'ToolStripButton1
        '
        Me.ToolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton1.Name = "ToolStripButton1"
        Me.ToolStripButton1.Size = New System.Drawing.Size(23, 4)
        Me.ToolStripButton1.Text = "Reload Form"
        Me.ToolStripButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage
        '
        'tsReload
        '
        Me.tsReload.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.tsReload.Image = CType(resources.GetObject("tsReload.Image"), System.Drawing.Image)
        Me.tsReload.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsReload.Margin = New System.Windows.Forms.Padding(0)
        Me.tsReload.Name = "tsReload"
        Me.tsReload.Size = New System.Drawing.Size(66, 19)
        Me.tsReload.Text = "ReFreshAll"
        '
        'ToolStripButton2
        '
        Me.ToolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton2.Image = CType(resources.GetObject("ToolStripButton2.Image"), System.Drawing.Image)
        Me.ToolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton2.Name = "ToolStripButton2"
        Me.ToolStripButton2.Size = New System.Drawing.Size(23, 20)
        Me.ToolStripButton2.Text = "ToolStripButton2"
        Me.ToolStripButton2.Visible = False
        '
        'lblMainGrid
        '
        Me.lblMainGrid.AutoSize = True
        Me.lblMainGrid.BackColor = System.Drawing.Color.Sienna
        Me.lblMainGrid.Font = New System.Drawing.Font("Comic Sans MS", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMainGrid.ForeColor = System.Drawing.Color.NavajoWhite
        Me.lblMainGrid.Location = New System.Drawing.Point(13, 25)
        Me.lblMainGrid.Name = "lblMainGrid"
        Me.lblMainGrid.Padding = New System.Windows.Forms.Padding(20, 0, 0, 0)
        Me.lblMainGrid.Size = New System.Drawing.Size(140, 16)
        Me.lblMainGrid.TabIndex = 3
        Me.lblMainGrid.Text = "ORGANIZATIONS"
        '
        'StatusBar1
        '
        Me.StatusBar1.Location = New System.Drawing.Point(0, 778)
        Me.StatusBar1.Name = "StatusBar1"
        Me.StatusBar1.Panels.AddRange(New System.Windows.Forms.StatusBarPanel() {Me.StatusBarPanel1, Me.StatusBarPanel2})
        Me.StatusBar1.ShowPanels = True
        Me.StatusBar1.Size = New System.Drawing.Size(1148, 22)
        Me.StatusBar1.TabIndex = 193
        '
        'StatusBarPanel1
        '
        Me.StatusBarPanel1.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents
        Me.StatusBarPanel1.MinWidth = 150
        Me.StatusBarPanel1.Name = "StatusBarPanel1"
        Me.StatusBarPanel1.Width = 150
        '
        'StatusBarPanel2
        '
        Me.StatusBarPanel2.Alignment = System.Windows.Forms.HorizontalAlignment.Right
        Me.StatusBarPanel2.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring
        Me.StatusBarPanel2.Name = "StatusBarPanel2"
        Me.StatusBarPanel2.Text = "Use this window to compare and update Organization and Contact address informatio" & _
    "n."
        Me.StatusBarPanel2.Width = 981
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.ForeColor = System.Drawing.Color.Maroon
        Me.Label1.Location = New System.Drawing.Point(250, 24)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(446, 13)
        Me.Label1.TabIndex = 194
        Me.Label1.Text = "Note: Changes should save automatically.  There is no 'close without saving chang" & _
    "es' option."
        '
        'grdvwSec
        '
        Me.grdvwSec.AllowUserToAddRows = False
        Me.grdvwSec.AllowUserToDeleteRows = False
        Me.grdvwSec.AllowUserToOrderColumns = True
        Me.grdvwSec.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdvwSec.AutoGenerateColumns = False
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdvwSec.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.grdvwSec.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ContactID, Me.OrgNum, Me.ActiveDataGridViewCheckBoxColumn, Me.DataGridViewTextBoxColumn28, Me.DataGridViewTextBoxColumn14, Me.LastName, Me.DataGridViewTextBoxColumn19, Me.DataGridViewTextBoxColumn20, Me.DataGridViewCheckBoxColumn6, Me.NotesDataGridViewTextBoxColumn2, Me.Street1DataGridViewTextBoxColumn2, Me.CityDataGridViewTextBoxColumn2, Me.StateDataGridViewTextBoxColumn2, Me.ZipDataGridViewTextBoxColumn2, Me.DataGridViewCheckBoxColumn7, Me.PhoneDataGridViewTextBoxColumn2, Me.FaxDataGridViewTextBoxColumn2, Me.EmailDataGridViewTextBoxColumn2, Me.DataGridViewTextBoxColumn25, Me.DataGridViewTextBoxColumn21, Me.DataGridViewTextBoxColumn22, Me.GoesbyDataGridViewTextBoxColumn, Me.DataGridViewTextBoxColumn15, Me.DataGridViewTextBoxColumn17, Me.DataGridViewCheckBoxColumn8, Me.DataGridViewCheckBoxColumn9, Me.OrgName, Me.DataGridViewTextBoxColumn23, Me.DataGridViewTextBoxColumn26, Me.DataGridViewTextBoxColumn27, Me.CreateDateDataGridViewTextBoxColumn1, Me.CreateStaffNumDataGridViewTextBoxColumn1, Me.LastChangeDateDataGridViewTextBoxColumn1, Me.LastChangeStaffNumDataGridViewTextBoxColumn1, Me.DataGridViewTextBoxColumn29, Me.SortFldDataGridViewTextBoxColumn2})
        Me.grdvwSec.DataSource = Me.BS3
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.grdvwSec.DefaultCellStyle = DataGridViewCellStyle2
        Me.grdvwSec.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter
        Me.grdvwSec.Location = New System.Drawing.Point(23, 431)
        Me.grdvwSec.MultiSelect = False
        Me.grdvwSec.Name = "grdvwSec"
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdvwSec.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.grdvwSec.RowHeadersWidth = 15
        Me.grdvwSec.Size = New System.Drawing.Size(1088, 244)
        Me.grdvwSec.TabIndex = 195
        Me.grdvwSec.Tag = "Secondary"
        '
        'ContactID
        '
        Me.ContactID.DataPropertyName = "ContactID"
        Me.ContactID.HeaderText = "ContactID"
        Me.ContactID.Name = "ContactID"
        Me.ContactID.ReadOnly = True
        Me.ContactID.Width = 80
        '
        'OrgNum
        '
        Me.OrgNum.DataPropertyName = "OrgNum"
        Me.OrgNum.HeaderText = "OrgNum"
        Me.OrgNum.Name = "OrgNum"
        Me.OrgNum.ReadOnly = True
        Me.OrgNum.Width = 75
        '
        'ActiveDataGridViewCheckBoxColumn
        '
        Me.ActiveDataGridViewCheckBoxColumn.DataPropertyName = "Active"
        Me.ActiveDataGridViewCheckBoxColumn.HeaderText = "Active"
        Me.ActiveDataGridViewCheckBoxColumn.Name = "ActiveDataGridViewCheckBoxColumn"
        Me.ActiveDataGridViewCheckBoxColumn.Width = 30
        '
        'DataGridViewTextBoxColumn28
        '
        Me.DataGridViewTextBoxColumn28.DataPropertyName = "Prefix"
        Me.DataGridViewTextBoxColumn28.HeaderText = "Prefix"
        Me.DataGridViewTextBoxColumn28.Name = "DataGridViewTextBoxColumn28"
        Me.DataGridViewTextBoxColumn28.Width = 50
        '
        'DataGridViewTextBoxColumn14
        '
        Me.DataGridViewTextBoxColumn14.DataPropertyName = "FirstName"
        Me.DataGridViewTextBoxColumn14.HeaderText = "FirstName"
        Me.DataGridViewTextBoxColumn14.Name = "DataGridViewTextBoxColumn14"
        '
        'LastName
        '
        Me.LastName.DataPropertyName = "Lastname"
        Me.LastName.HeaderText = "Lastname"
        Me.LastName.Name = "LastName"
        '
        'DataGridViewTextBoxColumn19
        '
        Me.DataGridViewTextBoxColumn19.DataPropertyName = "Staff"
        Me.DataGridViewTextBoxColumn19.HeaderText = "Staff"
        Me.DataGridViewTextBoxColumn19.Name = "DataGridViewTextBoxColumn19"
        Me.DataGridViewTextBoxColumn19.Width = 50
        '
        'DataGridViewTextBoxColumn20
        '
        Me.DataGridViewTextBoxColumn20.DataPropertyName = "JobTitle"
        Me.DataGridViewTextBoxColumn20.HeaderText = "JobTitle"
        Me.DataGridViewTextBoxColumn20.Name = "DataGridViewTextBoxColumn20"
        '
        'DataGridViewCheckBoxColumn6
        '
        Me.DataGridViewCheckBoxColumn6.DataPropertyName = "PrimaryContact"
        Me.DataGridViewCheckBoxColumn6.HeaderText = "PrimaryContact"
        Me.DataGridViewCheckBoxColumn6.Name = "DataGridViewCheckBoxColumn6"
        Me.DataGridViewCheckBoxColumn6.Width = 30
        '
        'NotesDataGridViewTextBoxColumn2
        '
        Me.NotesDataGridViewTextBoxColumn2.DataPropertyName = "Notes"
        Me.NotesDataGridViewTextBoxColumn2.HeaderText = "Notes"
        Me.NotesDataGridViewTextBoxColumn2.Name = "NotesDataGridViewTextBoxColumn2"
        '
        'Street1DataGridViewTextBoxColumn2
        '
        Me.Street1DataGridViewTextBoxColumn2.DataPropertyName = "Street1"
        Me.Street1DataGridViewTextBoxColumn2.HeaderText = "Street1"
        Me.Street1DataGridViewTextBoxColumn2.Name = "Street1DataGridViewTextBoxColumn2"
        '
        'CityDataGridViewTextBoxColumn2
        '
        Me.CityDataGridViewTextBoxColumn2.DataPropertyName = "City"
        Me.CityDataGridViewTextBoxColumn2.HeaderText = "City"
        Me.CityDataGridViewTextBoxColumn2.Name = "CityDataGridViewTextBoxColumn2"
        Me.CityDataGridViewTextBoxColumn2.Width = 50
        '
        'StateDataGridViewTextBoxColumn2
        '
        Me.StateDataGridViewTextBoxColumn2.DataPropertyName = "State"
        Me.StateDataGridViewTextBoxColumn2.HeaderText = "State"
        Me.StateDataGridViewTextBoxColumn2.Name = "StateDataGridViewTextBoxColumn2"
        Me.StateDataGridViewTextBoxColumn2.Width = 30
        '
        'ZipDataGridViewTextBoxColumn2
        '
        Me.ZipDataGridViewTextBoxColumn2.DataPropertyName = "Zip"
        Me.ZipDataGridViewTextBoxColumn2.HeaderText = "Zip"
        Me.ZipDataGridViewTextBoxColumn2.Name = "ZipDataGridViewTextBoxColumn2"
        Me.ZipDataGridViewTextBoxColumn2.Width = 30
        '
        'DataGridViewCheckBoxColumn7
        '
        Me.DataGridViewCheckBoxColumn7.DataPropertyName = "UseHome"
        Me.DataGridViewCheckBoxColumn7.HeaderText = "UseHome"
        Me.DataGridViewCheckBoxColumn7.Name = "DataGridViewCheckBoxColumn7"
        Me.DataGridViewCheckBoxColumn7.Width = 30
        '
        'PhoneDataGridViewTextBoxColumn2
        '
        Me.PhoneDataGridViewTextBoxColumn2.DataPropertyName = "Phone"
        Me.PhoneDataGridViewTextBoxColumn2.HeaderText = "Phone"
        Me.PhoneDataGridViewTextBoxColumn2.Name = "PhoneDataGridViewTextBoxColumn2"
        Me.PhoneDataGridViewTextBoxColumn2.Width = 75
        '
        'FaxDataGridViewTextBoxColumn2
        '
        Me.FaxDataGridViewTextBoxColumn2.DataPropertyName = "Fax"
        Me.FaxDataGridViewTextBoxColumn2.HeaderText = "Fax"
        Me.FaxDataGridViewTextBoxColumn2.Name = "FaxDataGridViewTextBoxColumn2"
        Me.FaxDataGridViewTextBoxColumn2.Width = 75
        '
        'EmailDataGridViewTextBoxColumn2
        '
        Me.EmailDataGridViewTextBoxColumn2.DataPropertyName = "Email"
        Me.EmailDataGridViewTextBoxColumn2.HeaderText = "Email"
        Me.EmailDataGridViewTextBoxColumn2.Name = "EmailDataGridViewTextBoxColumn2"
        '
        'DataGridViewTextBoxColumn25
        '
        Me.DataGridViewTextBoxColumn25.DataPropertyName = "CellPhone"
        Me.DataGridViewTextBoxColumn25.HeaderText = "CellPhone"
        Me.DataGridViewTextBoxColumn25.Name = "DataGridViewTextBoxColumn25"
        Me.DataGridViewTextBoxColumn25.Width = 75
        '
        'DataGridViewTextBoxColumn21
        '
        Me.DataGridViewTextBoxColumn21.DataPropertyName = "WorkPhone"
        Me.DataGridViewTextBoxColumn21.HeaderText = "WorkPhone"
        Me.DataGridViewTextBoxColumn21.Name = "DataGridViewTextBoxColumn21"
        Me.DataGridViewTextBoxColumn21.Width = 75
        '
        'DataGridViewTextBoxColumn22
        '
        Me.DataGridViewTextBoxColumn22.DataPropertyName = "WorkExtension"
        Me.DataGridViewTextBoxColumn22.HeaderText = "WorkExtension"
        Me.DataGridViewTextBoxColumn22.Name = "DataGridViewTextBoxColumn22"
        Me.DataGridViewTextBoxColumn22.Width = 30
        '
        'GoesbyDataGridViewTextBoxColumn
        '
        Me.GoesbyDataGridViewTextBoxColumn.DataPropertyName = "Goesby"
        Me.GoesbyDataGridViewTextBoxColumn.HeaderText = "Goesby"
        Me.GoesbyDataGridViewTextBoxColumn.Name = "GoesbyDataGridViewTextBoxColumn"
        Me.GoesbyDataGridViewTextBoxColumn.Width = 50
        '
        'DataGridViewTextBoxColumn15
        '
        Me.DataGridViewTextBoxColumn15.DataPropertyName = "MI"
        Me.DataGridViewTextBoxColumn15.HeaderText = "MI"
        Me.DataGridViewTextBoxColumn15.Name = "DataGridViewTextBoxColumn15"
        Me.DataGridViewTextBoxColumn15.Width = 30
        '
        'DataGridViewTextBoxColumn17
        '
        Me.DataGridViewTextBoxColumn17.DataPropertyName = "Suffix"
        Me.DataGridViewTextBoxColumn17.HeaderText = "Suffix"
        Me.DataGridViewTextBoxColumn17.Name = "DataGridViewTextBoxColumn17"
        Me.DataGridViewTextBoxColumn17.Width = 30
        '
        'DataGridViewCheckBoxColumn8
        '
        Me.DataGridViewCheckBoxColumn8.DataPropertyName = "MailListEmail"
        Me.DataGridViewCheckBoxColumn8.HeaderText = "MailListEmail"
        Me.DataGridViewCheckBoxColumn8.Name = "DataGridViewCheckBoxColumn8"
        Me.DataGridViewCheckBoxColumn8.Width = 30
        '
        'DataGridViewCheckBoxColumn9
        '
        Me.DataGridViewCheckBoxColumn9.DataPropertyName = "MailListPostal"
        Me.DataGridViewCheckBoxColumn9.HeaderText = "MailListPostal"
        Me.DataGridViewCheckBoxColumn9.Name = "DataGridViewCheckBoxColumn9"
        Me.DataGridViewCheckBoxColumn9.Width = 30
        '
        'OrgName
        '
        Me.OrgName.DataPropertyName = "OrgName"
        Me.OrgName.HeaderText = "OrgName"
        Me.OrgName.Name = "OrgName"
        Me.OrgName.ReadOnly = True
        '
        'DataGridViewTextBoxColumn23
        '
        Me.DataGridViewTextBoxColumn23.DataPropertyName = "PagerNumber"
        Me.DataGridViewTextBoxColumn23.HeaderText = "PagerNumber"
        Me.DataGridViewTextBoxColumn23.Name = "DataGridViewTextBoxColumn23"
        Me.DataGridViewTextBoxColumn23.Width = 75
        '
        'DataGridViewTextBoxColumn26
        '
        Me.DataGridViewTextBoxColumn26.DataPropertyName = "Gender"
        Me.DataGridViewTextBoxColumn26.HeaderText = "Gender"
        Me.DataGridViewTextBoxColumn26.Name = "DataGridViewTextBoxColumn26"
        Me.DataGridViewTextBoxColumn26.Width = 30
        '
        'DataGridViewTextBoxColumn27
        '
        Me.DataGridViewTextBoxColumn27.DataPropertyName = "Ethnicity"
        Me.DataGridViewTextBoxColumn27.HeaderText = "Ethnicity"
        Me.DataGridViewTextBoxColumn27.Name = "DataGridViewTextBoxColumn27"
        Me.DataGridViewTextBoxColumn27.Width = 50
        '
        'CreateDateDataGridViewTextBoxColumn1
        '
        Me.CreateDateDataGridViewTextBoxColumn1.DataPropertyName = "CreateDate"
        Me.CreateDateDataGridViewTextBoxColumn1.HeaderText = "CreateDate"
        Me.CreateDateDataGridViewTextBoxColumn1.Name = "CreateDateDataGridViewTextBoxColumn1"
        Me.CreateDateDataGridViewTextBoxColumn1.ReadOnly = True
        Me.CreateDateDataGridViewTextBoxColumn1.Width = 75
        '
        'CreateStaffNumDataGridViewTextBoxColumn1
        '
        Me.CreateStaffNumDataGridViewTextBoxColumn1.DataPropertyName = "CreateStaffNum"
        Me.CreateStaffNumDataGridViewTextBoxColumn1.HeaderText = "CreateStaffNum"
        Me.CreateStaffNumDataGridViewTextBoxColumn1.Name = "CreateStaffNumDataGridViewTextBoxColumn1"
        Me.CreateStaffNumDataGridViewTextBoxColumn1.ReadOnly = True
        Me.CreateStaffNumDataGridViewTextBoxColumn1.Width = 30
        '
        'LastChangeDateDataGridViewTextBoxColumn1
        '
        Me.LastChangeDateDataGridViewTextBoxColumn1.DataPropertyName = "LastChangeDate"
        Me.LastChangeDateDataGridViewTextBoxColumn1.HeaderText = "LastChangeDate"
        Me.LastChangeDateDataGridViewTextBoxColumn1.Name = "LastChangeDateDataGridViewTextBoxColumn1"
        Me.LastChangeDateDataGridViewTextBoxColumn1.ReadOnly = True
        Me.LastChangeDateDataGridViewTextBoxColumn1.Width = 75
        '
        'LastChangeStaffNumDataGridViewTextBoxColumn1
        '
        Me.LastChangeStaffNumDataGridViewTextBoxColumn1.DataPropertyName = "LastChangeStaffNum"
        Me.LastChangeStaffNumDataGridViewTextBoxColumn1.HeaderText = "LastChangeStaffNum"
        Me.LastChangeStaffNumDataGridViewTextBoxColumn1.Name = "LastChangeStaffNumDataGridViewTextBoxColumn1"
        Me.LastChangeStaffNumDataGridViewTextBoxColumn1.ReadOnly = True
        Me.LastChangeStaffNumDataGridViewTextBoxColumn1.Width = 30
        '
        'DataGridViewTextBoxColumn29
        '
        Me.DataGridViewTextBoxColumn29.DataPropertyName = "PrintFlag"
        Me.DataGridViewTextBoxColumn29.HeaderText = "PrintFlag"
        Me.DataGridViewTextBoxColumn29.Name = "DataGridViewTextBoxColumn29"
        Me.DataGridViewTextBoxColumn29.Width = 30
        '
        'SortFldDataGridViewTextBoxColumn2
        '
        Me.SortFldDataGridViewTextBoxColumn2.DataPropertyName = "SortFld"
        Me.SortFldDataGridViewTextBoxColumn2.HeaderText = "SortFld"
        Me.SortFldDataGridViewTextBoxColumn2.Name = "SortFldDataGridViewTextBoxColumn2"
        Me.SortFldDataGridViewTextBoxColumn2.ReadOnly = True
        '
        'BS3
        '
        Me.BS3.DataSource = Me.RelOrgContactBindingSource
        '
        'RelOrgContactBindingSource
        '
        Me.RelOrgContactBindingSource.DataMember = "relOrgContact"
        Me.RelOrgContactBindingSource.DataSource = Me.tblOrgBindingSource
        '
        'pnlOrg
        '
        Me.pnlOrg.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlOrg.BackColor = System.Drawing.Color.Sienna
        Me.pnlOrg.Controls.Add(Me.Label2)
        Me.pnlOrg.Controls.Add(Me.btnMovePerson)
        Me.pnlOrg.Controls.Add(Me.grdvwSec)
        Me.pnlOrg.Controls.Add(Me.btnGoodBadFilter)
        Me.pnlOrg.Controls.Add(Me.fldBadID)
        Me.pnlOrg.Controls.Add(Me.fldGoodID)
        Me.pnlOrg.Controls.Add(Me.btnOKDeleteOrg)
        Me.pnlOrg.Controls.Add(Me.grdvwMain)
        Me.pnlOrg.Location = New System.Drawing.Point(12, 52)
        Me.pnlOrg.Name = "pnlOrg"
        Me.pnlOrg.Size = New System.Drawing.Size(1124, 689)
        Me.pnlOrg.TabIndex = 196
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(501, 30)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(82, 13)
        Me.Label2.TabIndex = 212
        Me.Label2.Text = "KEEPER -->>"
        Me.Label2.Visible = False
        '
        'btnMovePerson
        '
        Me.btnMovePerson.Location = New System.Drawing.Point(4, 388)
        Me.btnMovePerson.Name = "btnMovePerson"
        Me.btnMovePerson.Size = New System.Drawing.Size(127, 37)
        Me.btnMovePerson.TabIndex = 211
        Me.btnMovePerson.Text = "Move person " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "from Bad to Good"
        Me.btnMovePerson.UseVisualStyleBackColor = True
        Me.btnMovePerson.Visible = False
        '
        'btnGoodBadFilter
        '
        Me.btnGoodBadFilter.Location = New System.Drawing.Point(87, 16)
        Me.btnGoodBadFilter.Name = "btnGoodBadFilter"
        Me.btnGoodBadFilter.Size = New System.Drawing.Size(67, 40)
        Me.btnGoodBadFilter.TabIndex = 210
        Me.btnGoodBadFilter.Text = "View only  1 && 2"
        Me.btnGoodBadFilter.UseVisualStyleBackColor = True
        '
        'fldBadID
        '
        Me.fldBadID.Location = New System.Drawing.Point(13, 40)
        Me.fldBadID.Name = "fldBadID"
        Me.fldBadID.Size = New System.Drawing.Size(69, 20)
        Me.fldBadID.TabIndex = 209
        Me.fldBadID.Text = "Org2"
        '
        'fldGoodID
        '
        Me.fldGoodID.Location = New System.Drawing.Point(13, 14)
        Me.fldGoodID.Name = "fldGoodID"
        Me.fldGoodID.Size = New System.Drawing.Size(68, 20)
        Me.fldGoodID.TabIndex = 208
        Me.fldGoodID.Text = "Org1"
        '
        'btnOKDeleteOrg
        '
        Me.btnOKDeleteOrg.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.btnOKDeleteOrg.Location = New System.Drawing.Point(184, 17)
        Me.btnOKDeleteOrg.Name = "btnOKDeleteOrg"
        Me.btnOKDeleteOrg.Size = New System.Drawing.Size(215, 39)
        Me.btnOKDeleteOrg.TabIndex = 207
        Me.btnOKDeleteOrg.Text = "√ Updated Keeper; OK to  Delete Bad"
        Me.btnOKDeleteOrg.UseVisualStyleBackColor = True
        '
        'grdvwMain
        '
        Me.grdvwMain.AllowUserToAddRows = False
        Me.grdvwMain.AllowUserToDeleteRows = False
        Me.grdvwMain.AllowUserToOrderColumns = True
        Me.grdvwMain.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdvwMain.AutoGenerateColumns = False
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdvwMain.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle4
        Me.grdvwMain.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.OrgID, Me.Active, Me.OrgType, Me.OrgNameDataGridViewTextBoxColumn, Me.Street1DataGridViewTextBoxColumn, Me.CityDataGridViewTextBoxColumn, Me.StateDataGridViewTextBoxColumn, Me.ZipDataGridViewTextBoxColumn, Me.PhysicalAddress, Me.PhysicalZip, Me.PhoneDataGridViewTextBoxColumn, Me.FaxDataGridViewTextBoxColumn, Me.WebsiteDataGridViewTextBoxColumn, Me.EmailDataGridViewTextBoxColumn, Me.NotesDataGridViewTextBoxColumn, Me.AttendanceDataGridViewTextBoxColumn, Me.MembershipDataGridViewTextBoxColumn, Me.HouseholdsDataGridViewTextBoxColumn, Me.AnnualBudgetDataGridViewTextBoxColumn, Me.DenominationDataGridViewTextBoxColumn, Me.CountyDataGridViewTextBoxColumn, Me.SatelliteRegionDataGridViewTextBoxColumn, Me.EINDataGridViewTextBoxColumn, Me.MapURLDataGridViewTextBoxColumn, Me.LayLeadershipChangesDataGridViewTextBoxColumn, Me.MailPreferenceDataGridViewTextBoxColumn, Me.EmailPreferenceDataGridViewTextBoxColumn, Me.CountryDataGridViewTextBoxColumn, Me.SortFldDataGridViewTextBoxColumn, Me.Programs, Me.DateFounded, Me.InfoSource, Me.CongrEthnicity, Me.CongrCulture, Me.CreateDateDataGridViewTextBoxColumn, Me.CreateStaffNumDataGridViewTextBoxColumn, Me.LastChangeDateDataGridViewTextBoxColumn, Me.LastChangeStaffNumDataGridViewTextBoxColumn, Me.ReviewDate, Me.ReviewStaffNum, Me.ReviewResult})
        Me.grdvwMain.DataSource = Me.tblOrgBindingSource
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.grdvwMain.DefaultCellStyle = DataGridViewCellStyle5
        Me.grdvwMain.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter
        Me.grdvwMain.Location = New System.Drawing.Point(13, 69)
        Me.grdvwMain.Name = "grdvwMain"
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdvwMain.RowHeadersDefaultCellStyle = DataGridViewCellStyle6
        Me.grdvwMain.RowHeadersWidth = 15
        Me.grdvwMain.Size = New System.Drawing.Size(1098, 301)
        Me.grdvwMain.TabIndex = 1
        Me.grdvwMain.Tag = "Main"
        '
        'OrgID
        '
        Me.OrgID.DataPropertyName = "OrgID"
        Me.OrgID.HeaderText = "OrgID"
        Me.OrgID.Name = "OrgID"
        Me.OrgID.ReadOnly = True
        Me.OrgID.Width = 60
        '
        'Active
        '
        Me.Active.DataPropertyName = "Active"
        Me.Active.HeaderText = "Active"
        Me.Active.Name = "Active"
        Me.Active.Width = 25
        '
        'OrgType
        '
        Me.OrgType.DataPropertyName = "OrgType"
        Me.OrgType.HeaderText = "OrgType"
        Me.OrgType.Name = "OrgType"
        Me.OrgType.Width = 30
        '
        'OrgNameDataGridViewTextBoxColumn
        '
        Me.OrgNameDataGridViewTextBoxColumn.DataPropertyName = "OrgName"
        Me.OrgNameDataGridViewTextBoxColumn.HeaderText = "OrgName"
        Me.OrgNameDataGridViewTextBoxColumn.Name = "OrgNameDataGridViewTextBoxColumn"
        Me.OrgNameDataGridViewTextBoxColumn.Width = 120
        '
        'Street1DataGridViewTextBoxColumn
        '
        Me.Street1DataGridViewTextBoxColumn.DataPropertyName = "Street1"
        Me.Street1DataGridViewTextBoxColumn.HeaderText = "Street1"
        Me.Street1DataGridViewTextBoxColumn.Name = "Street1DataGridViewTextBoxColumn"
        Me.Street1DataGridViewTextBoxColumn.Width = 110
        '
        'CityDataGridViewTextBoxColumn
        '
        Me.CityDataGridViewTextBoxColumn.DataPropertyName = "City"
        Me.CityDataGridViewTextBoxColumn.HeaderText = "City"
        Me.CityDataGridViewTextBoxColumn.Name = "CityDataGridViewTextBoxColumn"
        Me.CityDataGridViewTextBoxColumn.Width = 50
        '
        'StateDataGridViewTextBoxColumn
        '
        Me.StateDataGridViewTextBoxColumn.DataPropertyName = "State"
        Me.StateDataGridViewTextBoxColumn.HeaderText = "State"
        Me.StateDataGridViewTextBoxColumn.Name = "StateDataGridViewTextBoxColumn"
        Me.StateDataGridViewTextBoxColumn.Width = 30
        '
        'ZipDataGridViewTextBoxColumn
        '
        Me.ZipDataGridViewTextBoxColumn.DataPropertyName = "Zip"
        Me.ZipDataGridViewTextBoxColumn.HeaderText = "Zip"
        Me.ZipDataGridViewTextBoxColumn.Name = "ZipDataGridViewTextBoxColumn"
        Me.ZipDataGridViewTextBoxColumn.Width = 30
        '
        'PhysicalAddress
        '
        Me.PhysicalAddress.DataPropertyName = "PhysicalAddress"
        Me.PhysicalAddress.HeaderText = "PhysicalAddress"
        Me.PhysicalAddress.Name = "PhysicalAddress"
        Me.PhysicalAddress.Width = 150
        '
        'PhysicalZip
        '
        Me.PhysicalZip.DataPropertyName = "PhysicalZip"
        Me.PhysicalZip.HeaderText = "PhysicalZip"
        Me.PhysicalZip.Name = "PhysicalZip"
        '
        'PhoneDataGridViewTextBoxColumn
        '
        Me.PhoneDataGridViewTextBoxColumn.DataPropertyName = "Phone"
        Me.PhoneDataGridViewTextBoxColumn.HeaderText = "Phone"
        Me.PhoneDataGridViewTextBoxColumn.Name = "PhoneDataGridViewTextBoxColumn"
        '
        'FaxDataGridViewTextBoxColumn
        '
        Me.FaxDataGridViewTextBoxColumn.DataPropertyName = "Fax"
        Me.FaxDataGridViewTextBoxColumn.HeaderText = "Fax"
        Me.FaxDataGridViewTextBoxColumn.Name = "FaxDataGridViewTextBoxColumn"
        Me.FaxDataGridViewTextBoxColumn.Width = 75
        '
        'WebsiteDataGridViewTextBoxColumn
        '
        Me.WebsiteDataGridViewTextBoxColumn.DataPropertyName = "Website"
        Me.WebsiteDataGridViewTextBoxColumn.HeaderText = "Website"
        Me.WebsiteDataGridViewTextBoxColumn.Name = "WebsiteDataGridViewTextBoxColumn"
        '
        'EmailDataGridViewTextBoxColumn
        '
        Me.EmailDataGridViewTextBoxColumn.DataPropertyName = "Email"
        Me.EmailDataGridViewTextBoxColumn.HeaderText = "Email"
        Me.EmailDataGridViewTextBoxColumn.Name = "EmailDataGridViewTextBoxColumn"
        '
        'NotesDataGridViewTextBoxColumn
        '
        Me.NotesDataGridViewTextBoxColumn.DataPropertyName = "Notes"
        Me.NotesDataGridViewTextBoxColumn.HeaderText = "Notes"
        Me.NotesDataGridViewTextBoxColumn.Name = "NotesDataGridViewTextBoxColumn"
        '
        'AttendanceDataGridViewTextBoxColumn
        '
        Me.AttendanceDataGridViewTextBoxColumn.DataPropertyName = "Attendance"
        Me.AttendanceDataGridViewTextBoxColumn.HeaderText = "Attendance"
        Me.AttendanceDataGridViewTextBoxColumn.Name = "AttendanceDataGridViewTextBoxColumn"
        Me.AttendanceDataGridViewTextBoxColumn.Width = 30
        '
        'MembershipDataGridViewTextBoxColumn
        '
        Me.MembershipDataGridViewTextBoxColumn.DataPropertyName = "Membership"
        Me.MembershipDataGridViewTextBoxColumn.HeaderText = "Membership"
        Me.MembershipDataGridViewTextBoxColumn.Name = "MembershipDataGridViewTextBoxColumn"
        Me.MembershipDataGridViewTextBoxColumn.Width = 30
        '
        'HouseholdsDataGridViewTextBoxColumn
        '
        Me.HouseholdsDataGridViewTextBoxColumn.DataPropertyName = "Households"
        Me.HouseholdsDataGridViewTextBoxColumn.HeaderText = "Households"
        Me.HouseholdsDataGridViewTextBoxColumn.Name = "HouseholdsDataGridViewTextBoxColumn"
        Me.HouseholdsDataGridViewTextBoxColumn.Width = 30
        '
        'AnnualBudgetDataGridViewTextBoxColumn
        '
        Me.AnnualBudgetDataGridViewTextBoxColumn.DataPropertyName = "AnnualBudget"
        Me.AnnualBudgetDataGridViewTextBoxColumn.HeaderText = "AnnualBudget"
        Me.AnnualBudgetDataGridViewTextBoxColumn.Name = "AnnualBudgetDataGridViewTextBoxColumn"
        Me.AnnualBudgetDataGridViewTextBoxColumn.Width = 30
        '
        'DenominationDataGridViewTextBoxColumn
        '
        Me.DenominationDataGridViewTextBoxColumn.DataPropertyName = "Denomination"
        Me.DenominationDataGridViewTextBoxColumn.HeaderText = "Denomination"
        Me.DenominationDataGridViewTextBoxColumn.Name = "DenominationDataGridViewTextBoxColumn"
        '
        'CountyDataGridViewTextBoxColumn
        '
        Me.CountyDataGridViewTextBoxColumn.DataPropertyName = "County"
        Me.CountyDataGridViewTextBoxColumn.HeaderText = "County"
        Me.CountyDataGridViewTextBoxColumn.Name = "CountyDataGridViewTextBoxColumn"
        Me.CountyDataGridViewTextBoxColumn.Width = 30
        '
        'SatelliteRegionDataGridViewTextBoxColumn
        '
        Me.SatelliteRegionDataGridViewTextBoxColumn.DataPropertyName = "SatelliteRegion"
        Me.SatelliteRegionDataGridViewTextBoxColumn.HeaderText = "SatelliteRegion"
        Me.SatelliteRegionDataGridViewTextBoxColumn.Name = "SatelliteRegionDataGridViewTextBoxColumn"
        Me.SatelliteRegionDataGridViewTextBoxColumn.Width = 30
        '
        'EINDataGridViewTextBoxColumn
        '
        Me.EINDataGridViewTextBoxColumn.DataPropertyName = "EIN"
        Me.EINDataGridViewTextBoxColumn.HeaderText = "EIN"
        Me.EINDataGridViewTextBoxColumn.Name = "EINDataGridViewTextBoxColumn"
        Me.EINDataGridViewTextBoxColumn.Width = 30
        '
        'MapURLDataGridViewTextBoxColumn
        '
        Me.MapURLDataGridViewTextBoxColumn.DataPropertyName = "MapURL"
        Me.MapURLDataGridViewTextBoxColumn.HeaderText = "MapURL"
        Me.MapURLDataGridViewTextBoxColumn.Name = "MapURLDataGridViewTextBoxColumn"
        '
        'LayLeadershipChangesDataGridViewTextBoxColumn
        '
        Me.LayLeadershipChangesDataGridViewTextBoxColumn.DataPropertyName = "LayLeadershipChanges"
        Me.LayLeadershipChangesDataGridViewTextBoxColumn.HeaderText = "LayLeadershipChanges"
        Me.LayLeadershipChangesDataGridViewTextBoxColumn.Name = "LayLeadershipChangesDataGridViewTextBoxColumn"
        Me.LayLeadershipChangesDataGridViewTextBoxColumn.Width = 30
        '
        'MailPreferenceDataGridViewTextBoxColumn
        '
        Me.MailPreferenceDataGridViewTextBoxColumn.DataPropertyName = "MailPreference"
        Me.MailPreferenceDataGridViewTextBoxColumn.HeaderText = "MailPreference"
        Me.MailPreferenceDataGridViewTextBoxColumn.Name = "MailPreferenceDataGridViewTextBoxColumn"
        Me.MailPreferenceDataGridViewTextBoxColumn.Width = 30
        '
        'EmailPreferenceDataGridViewTextBoxColumn
        '
        Me.EmailPreferenceDataGridViewTextBoxColumn.DataPropertyName = "EmailPreference"
        Me.EmailPreferenceDataGridViewTextBoxColumn.HeaderText = "EmailPreference"
        Me.EmailPreferenceDataGridViewTextBoxColumn.Name = "EmailPreferenceDataGridViewTextBoxColumn"
        Me.EmailPreferenceDataGridViewTextBoxColumn.Width = 30
        '
        'CountryDataGridViewTextBoxColumn
        '
        Me.CountryDataGridViewTextBoxColumn.DataPropertyName = "Country"
        Me.CountryDataGridViewTextBoxColumn.HeaderText = "Country"
        Me.CountryDataGridViewTextBoxColumn.Name = "CountryDataGridViewTextBoxColumn"
        Me.CountryDataGridViewTextBoxColumn.Width = 30
        '
        'SortFldDataGridViewTextBoxColumn
        '
        Me.SortFldDataGridViewTextBoxColumn.DataPropertyName = "SortFld"
        Me.SortFldDataGridViewTextBoxColumn.HeaderText = "SortFld"
        Me.SortFldDataGridViewTextBoxColumn.Name = "SortFldDataGridViewTextBoxColumn"
        Me.SortFldDataGridViewTextBoxColumn.ReadOnly = True
        Me.SortFldDataGridViewTextBoxColumn.Width = 30
        '
        'Programs
        '
        Me.Programs.DataPropertyName = "Programs"
        Me.Programs.HeaderText = "Programs"
        Me.Programs.Name = "Programs"
        '
        'DateFounded
        '
        Me.DateFounded.DataPropertyName = "DateFounded"
        Me.DateFounded.HeaderText = "DateFounded"
        Me.DateFounded.Name = "DateFounded"
        '
        'InfoSource
        '
        Me.InfoSource.DataPropertyName = "InfoSource"
        Me.InfoSource.HeaderText = "InfoSource"
        Me.InfoSource.Name = "InfoSource"
        '
        'CongrEthnicity
        '
        Me.CongrEthnicity.DataPropertyName = "CongrEthnicity"
        Me.CongrEthnicity.HeaderText = "CongrEthnicity"
        Me.CongrEthnicity.Name = "CongrEthnicity"
        '
        'CongrCulture
        '
        Me.CongrCulture.DataPropertyName = "CongrCulture"
        Me.CongrCulture.HeaderText = "CongrCulture"
        Me.CongrCulture.Name = "CongrCulture"
        '
        'CreateDateDataGridViewTextBoxColumn
        '
        Me.CreateDateDataGridViewTextBoxColumn.DataPropertyName = "CreateDate"
        Me.CreateDateDataGridViewTextBoxColumn.HeaderText = "CreateDate"
        Me.CreateDateDataGridViewTextBoxColumn.Name = "CreateDateDataGridViewTextBoxColumn"
        Me.CreateDateDataGridViewTextBoxColumn.ReadOnly = True
        Me.CreateDateDataGridViewTextBoxColumn.Width = 75
        '
        'CreateStaffNumDataGridViewTextBoxColumn
        '
        Me.CreateStaffNumDataGridViewTextBoxColumn.DataPropertyName = "CreateStaffNum"
        Me.CreateStaffNumDataGridViewTextBoxColumn.HeaderText = "CreateStaffNum"
        Me.CreateStaffNumDataGridViewTextBoxColumn.Name = "CreateStaffNumDataGridViewTextBoxColumn"
        Me.CreateStaffNumDataGridViewTextBoxColumn.ReadOnly = True
        Me.CreateStaffNumDataGridViewTextBoxColumn.Width = 30
        '
        'LastChangeDateDataGridViewTextBoxColumn
        '
        Me.LastChangeDateDataGridViewTextBoxColumn.DataPropertyName = "LastChangeDate"
        Me.LastChangeDateDataGridViewTextBoxColumn.HeaderText = "LastChangeDate"
        Me.LastChangeDateDataGridViewTextBoxColumn.Name = "LastChangeDateDataGridViewTextBoxColumn"
        Me.LastChangeDateDataGridViewTextBoxColumn.ReadOnly = True
        Me.LastChangeDateDataGridViewTextBoxColumn.Width = 75
        '
        'LastChangeStaffNumDataGridViewTextBoxColumn
        '
        Me.LastChangeStaffNumDataGridViewTextBoxColumn.DataPropertyName = "LastChangeStaffNum"
        Me.LastChangeStaffNumDataGridViewTextBoxColumn.HeaderText = "LastChangeStaffNum"
        Me.LastChangeStaffNumDataGridViewTextBoxColumn.Name = "LastChangeStaffNumDataGridViewTextBoxColumn"
        Me.LastChangeStaffNumDataGridViewTextBoxColumn.ReadOnly = True
        Me.LastChangeStaffNumDataGridViewTextBoxColumn.Width = 30
        '
        'ReviewDate
        '
        Me.ReviewDate.DataPropertyName = "ReviewDate"
        Me.ReviewDate.HeaderText = "ReviewDate"
        Me.ReviewDate.Name = "ReviewDate"
        '
        'ReviewStaffNum
        '
        Me.ReviewStaffNum.DataPropertyName = "ReviewStaffNum"
        Me.ReviewStaffNum.HeaderText = "ReviewStaffNum"
        Me.ReviewStaffNum.Name = "ReviewStaffNum"
        '
        'ReviewResult
        '
        Me.ReviewResult.DataPropertyName = "ReviewResult"
        Me.ReviewResult.HeaderText = "ReviewResult"
        Me.ReviewResult.Name = "ReviewResult"
        '
        'OrgIDDataGridViewTextBoxColumn
        '
        Me.OrgIDDataGridViewTextBoxColumn.DataPropertyName = "OrgID"
        Me.OrgIDDataGridViewTextBoxColumn.HeaderText = "OrgID"
        Me.OrgIDDataGridViewTextBoxColumn.Name = "OrgIDDataGridViewTextBoxColumn"
        Me.OrgIDDataGridViewTextBoxColumn.Width = 5
        '
        'Contact
        '
        Me.Contact.DataPropertyName = "Contact"
        Me.Contact.HeaderText = "Contact"
        Me.Contact.Name = "Contact"
        '
        'ContactIDDataGridViewTextBoxColumn
        '
        Me.ContactIDDataGridViewTextBoxColumn.DataPropertyName = "ContactID"
        Me.ContactIDDataGridViewTextBoxColumn.HeaderText = "ContactID"
        Me.ContactIDDataGridViewTextBoxColumn.Name = "ContactIDDataGridViewTextBoxColumn"
        Me.ContactIDDataGridViewTextBoxColumn.ReadOnly = True
        '
        'ContactIDDataGridViewTextBoxColumn1
        '
        Me.ContactIDDataGridViewTextBoxColumn1.DataPropertyName = "ContactID"
        Me.ContactIDDataGridViewTextBoxColumn1.HeaderText = "ContactID"
        Me.ContactIDDataGridViewTextBoxColumn1.Name = "ContactIDDataGridViewTextBoxColumn1"
        Me.ContactIDDataGridViewTextBoxColumn1.ReadOnly = True
        '
        'FillByZipToolStrip
        '
        Me.FillByZipToolStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ZipToolStripLabel, Me.ZipToolStripTextBox, Me.FillByZipToolStripButton})
        Me.FillByZipToolStrip.Location = New System.Drawing.Point(0, 23)
        Me.FillByZipToolStrip.Name = "FillByZipToolStrip"
        Me.FillByZipToolStrip.Size = New System.Drawing.Size(1148, 25)
        Me.FillByZipToolStrip.TabIndex = 198
        Me.FillByZipToolStrip.Text = "FillByZipToolStrip"
        '
        'ZipToolStripLabel
        '
        Me.ZipToolStripLabel.Name = "ZipToolStripLabel"
        Me.ZipToolStripLabel.Size = New System.Drawing.Size(27, 22)
        Me.ZipToolStripLabel.Text = "Zip:"
        '
        'ZipToolStripTextBox
        '
        Me.ZipToolStripTextBox.Name = "ZipToolStripTextBox"
        Me.ZipToolStripTextBox.Size = New System.Drawing.Size(100, 25)
        '
        'FillByZipToolStripButton
        '
        Me.FillByZipToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.FillByZipToolStripButton.Name = "FillByZipToolStripButton"
        Me.FillByZipToolStripButton.Size = New System.Drawing.Size(56, 22)
        Me.FillByZipToolStripButton.Text = "FillByZip"
        '
        'BindingNavigatorAddNewItem
        '
        Me.BindingNavigatorAddNewItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorAddNewItem.Image = CType(resources.GetObject("BindingNavigatorAddNewItem.Image"), System.Drawing.Image)
        Me.BindingNavigatorAddNewItem.Name = "BindingNavigatorAddNewItem"
        Me.BindingNavigatorAddNewItem.RightToLeftAutoMirrorImage = True
        Me.BindingNavigatorAddNewItem.Size = New System.Drawing.Size(23, 22)
        Me.BindingNavigatorAddNewItem.Text = "Add new"
        '
        'BindingNavigatorDeleteItem
        '
        Me.BindingNavigatorDeleteItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorDeleteItem.Image = CType(resources.GetObject("BindingNavigatorDeleteItem.Image"), System.Drawing.Image)
        Me.BindingNavigatorDeleteItem.Name = "BindingNavigatorDeleteItem"
        Me.BindingNavigatorDeleteItem.RightToLeftAutoMirrorImage = True
        Me.BindingNavigatorDeleteItem.Size = New System.Drawing.Size(23, 22)
        Me.BindingNavigatorDeleteItem.Text = "Delete"
        '
        'TblContactBindingSource
        '
        Me.TblContactBindingSource.DataMember = "tblContact"
        Me.TblContactBindingSource.DataSource = Me.dsReviewOrgIntern
        '
        'OrgIDDataGridViewTextBoxColumn2
        '
        Me.OrgIDDataGridViewTextBoxColumn2.DataPropertyName = "OrgID"
        Me.OrgIDDataGridViewTextBoxColumn2.HeaderText = "OrgID"
        Me.OrgIDDataGridViewTextBoxColumn2.Name = "OrgIDDataGridViewTextBoxColumn2"
        Me.OrgIDDataGridViewTextBoxColumn2.Width = 75
        '
        'TblOrgTableAdapter
        '
        Me.TblOrgTableAdapter.ClearBeforeFill = True
        '
        'TblContactTableAdapter
        '
        Me.TblContactTableAdapter.ClearBeforeFill = True
        '
        'frmReviewOrgSimple
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoScroll = True
        Me.BackColor = System.Drawing.Color.Bisque
        Me.ClientSize = New System.Drawing.Size(1148, 800)
        Me.Controls.Add(Me.FillByZipToolStrip)
        Me.Controls.Add(Me.lblMainGrid)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.StatusBar1)
        Me.Controls.Add(Me.TblOrgBindingNavigator)
        Me.Controls.Add(Me.pnlOrg)
        Me.Name = "frmReviewOrgSimple"
        Me.Text = "REVIEW ACTIVE ORGANIZATIONS and CONTACTS"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.TblOrgBindingNavigator, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TblOrgBindingNavigator.ResumeLayout(False)
        Me.TblOrgBindingNavigator.PerformLayout()
        CType(Me.tblOrgBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dsReviewOrgIntern, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.StatusBarPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.StatusBarPanel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdvwSec, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BS3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RelOrgContactBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlOrg.ResumeLayout(False)
        Me.pnlOrg.PerformLayout()
        CType(Me.grdvwMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FillByZipToolStrip.ResumeLayout(False)
        Me.FillByZipToolStrip.PerformLayout()
        CType(Me.TblContactBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents dsReviewOrgIntern As InfoCtr.dsReviewOrgIntern
    Friend WithEvents TblOrgBindingNavigator As System.Windows.Forms.BindingNavigator
    Friend WithEvents BindingNavigatorAddNewItem As System.Windows.Forms.ToolStripButton
    Friend WithEvents BindingNavigatorCountItem As System.Windows.Forms.ToolStripLabel
    Friend WithEvents BindingNavigatorDeleteItem As System.Windows.Forms.ToolStripButton
    Friend WithEvents BindingNavigatorMoveFirstItem As System.Windows.Forms.ToolStripButton
    Friend WithEvents BindingNavigatorMovePreviousItem As System.Windows.Forms.ToolStripButton
    Friend WithEvents BindingNavigatorSeparator As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents BindingNavigatorPositionItem As System.Windows.Forms.ToolStripTextBox
    Friend WithEvents BindingNavigatorSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents BindingNavigatorMoveNextItem As System.Windows.Forms.ToolStripButton
    Friend WithEvents BindingNavigatorMoveLastItem As System.Windows.Forms.ToolStripButton
    Friend WithEvents BindingNavigatorSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tsSaveItem As System.Windows.Forms.ToolStripButton
    Friend WithEvents lblMainGrid As System.Windows.Forms.Label
    Friend WithEvents StatusBar1 As System.Windows.Forms.StatusBar
    Friend WithEvents StatusBarPanel1 As System.Windows.Forms.StatusBarPanel
    Friend WithEvents StatusBarPanel2 As System.Windows.Forms.StatusBarPanel
    Friend WithEvents tsTxtSearch As System.Windows.Forms.ToolStripTextBox
    Friend WithEvents tsBtnSearch As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents toolStripSeparator As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents toolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tsBtnHelp As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsBtnViewAll As System.Windows.Forms.ToolStripButton
    Friend WithEvents tscboRegion As System.Windows.Forms.ToolStripComboBox
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tslblFilter As System.Windows.Forms.ToolStripLabel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents DataGridViewTextBoxColumn61 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn65 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn62 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn63 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn66 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn68 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents tscboWhat As System.Windows.Forms.ToolStripComboBox
    Friend WithEvents DataGridViewTextBoxColumn33 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn34 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewCheckBoxColumn1 As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn37 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn35 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn39 As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn40 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn47 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn48 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn45 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn52 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn41 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn42 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn43 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn44 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewCheckBoxColumn2 As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn36 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn38 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn46 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewCheckBoxColumn3 As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn49 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn50 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn51 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn53 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn54 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn55 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn56 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn57 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn58 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewCheckBoxColumn4 As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents DataGridViewCheckBoxColumn5 As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents TblContactBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents grdvwSec As System.Windows.Forms.DataGridView
    Friend WithEvents TblRegistrationBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents grdvwMain As System.Windows.Forms.DataGridView
    Friend WithEvents pnlOrg As System.Windows.Forms.Panel
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator5 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripButton1 As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsReload As System.Windows.Forms.ToolStripButton
    '  Friend WithEvents RelOrgGrantBindingSource As System.Windows.Forms.BindingSource
    ' Friend WithEvents TblGrantTableAdapter As WindowsApplication11.dsReviewOrgInternTableAdapters.tblGrantTableAdapter
    Friend WithEvents RelContactRefBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents RegDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents GrantDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents RecDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents FeedbackDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents AlertDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ProfileDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents StoryDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CMGIDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents LTGIDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SSGIDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ResourceDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ToolStripButton2 As System.Windows.Forms.ToolStripButton
    Friend WithEvents OrgIDDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Contact As System.Windows.Forms.DataGridViewTextBoxColumn
    '  Friend WithEvents RelOrgRefBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents ContactIDDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ContactIDDataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents tblOrgBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ConversIDDataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CaseNumDataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents FillByZipToolStrip As System.Windows.Forms.ToolStrip
    Friend WithEvents ZipToolStripLabel As System.Windows.Forms.ToolStripLabel
    Friend WithEvents ZipToolStripTextBox As System.Windows.Forms.ToolStripTextBox
    Friend WithEvents FillByZipToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents ContactIDDataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents OrgNumDataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents FirstNameDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents LastnameDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PrimaryContactDataGridViewCheckBoxColumn As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents StaffDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents JobTitleDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents EmailDataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents UseHomeDataGridViewCheckBoxColumn As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents PhoneDataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CellPhoneDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents MailListPostalDataGridViewCheckBoxColumn As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents MailListEmailDataGridViewCheckBoxColumn As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents Street1DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CityDataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents StateDataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ZipDataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PrefixDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents MIDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SuffixDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents NotesDataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents FaxDataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ActiveDataGridViewCheckBoxColumn1 As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents WorkPhoneDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents WorkExtensionDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PagerNumberDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents GenderDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents EthnicityDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ContactCreateDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ContactCreateStaffNum As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ContactLastChangeDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ContactLastChangeStaffNum As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PrintFlagDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SortFldDataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents RelOrgContactBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents BS3 As System.Windows.Forms.BindingSource
    Friend WithEvents BlackHispanicDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CultureDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents OrgNameDataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Street1DataGridViewTextBoxColumn5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CityDataGridViewTextBoxColumn5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents StateDataGridViewTextBoxColumn5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ZipDataGridViewTextBoxColumn5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PhoneDataGridViewTextBoxColumn5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents FaxDataGridViewTextBoxColumn5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents EmailDataGridViewTextBoxColumn5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents NotesDataGridViewTextBoxColumn5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CreateDateDataGridViewTextBoxColumn4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CreateStaffNumDataGridViewTextBoxColumn4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents LastChangeDateDataGridViewTextBoxColumn4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents LastChangeStaffNumDataGridViewTextBoxColumn4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ActiveDataGridViewCheckBoxColumn4 As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents SortFldDataGridViewTextBoxColumn5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents OrgNumDataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents OrgIDDataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ConversDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents EventNumDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents RegonlineParticipantDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents btnGoodBadFilter As System.Windows.Forms.Button
    Friend WithEvents fldBadID As System.Windows.Forms.TextBox
    Friend WithEvents fldGoodID As System.Windows.Forms.TextBox
    Friend WithEvents btnOKDeleteOrg As System.Windows.Forms.Button
    Friend WithEvents btnMovePerson As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TblOrgTableAdapter As InfoCtr.dsReviewOrgInternTableAdapters.tblOrgTableAdapter
    'Friend WithEvents TblCaseTableAdapter As InfoCtr.dsReviewOrgInternTableAdapters.tblCaseTableAdapter
    'Friend WithEvents TblCaseConversationTableAdapter As InfoCtr.dsReviewOrgInternTableAdapters.tblCaseConversationTableAdapter
    Friend WithEvents TblContactTableAdapter As InfoCtr.dsReviewOrgInternTableAdapters.tblContactTableAdapter
    'Friend WithEvents TblRegistrationTableAdapter As InfoCtr.dsReviewOrgInternTableAdapters.tblRegistrationTableAdapter
    '  'Friend WithEvents TblConversationTableAdapter As InfoCtr.dsReviewOrgInternTableAdapters.tblConversationTableAdapter
    '  Friend WithEvents VwCntRefOrgTableAdapter As InfoCtr.dsReviewOrgInternTableAdapters.vwCntRefOrgTableAdapter
    '  Friend WithEvents VwCntRefContactTableAdapter As InfoCtr.dsReviewOrgInternTableAdapters.vwCntRefContactTableAdapter
    Friend WithEvents ContactID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents OrgNum As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ActiveDataGridViewCheckBoxColumn As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn28 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn14 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents LastName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn19 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn20 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewCheckBoxColumn6 As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents NotesDataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Street1DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CityDataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents StateDataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ZipDataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewCheckBoxColumn7 As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents PhoneDataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents FaxDataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents EmailDataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn25 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn21 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn22 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents GoesbyDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn15 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn17 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewCheckBoxColumn8 As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents DataGridViewCheckBoxColumn9 As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents OrgName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn23 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn26 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn27 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CreateDateDataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CreateStaffNumDataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents LastChangeDateDataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents LastChangeStaffNumDataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn29 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SortFldDataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents OrgID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Active As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents OrgType As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents OrgNameDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Street1DataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CityDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents StateDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ZipDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PhysicalAddress As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PhysicalZip As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PhoneDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents FaxDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents WebsiteDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents EmailDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents NotesDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents AttendanceDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents MembershipDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents HouseholdsDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents AnnualBudgetDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DenominationDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CountyDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SatelliteRegionDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents EINDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents MapURLDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents LayLeadershipChangesDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents MailPreferenceDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents EmailPreferenceDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CountryDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SortFldDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Programs As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DateFounded As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents InfoSource As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CongrEthnicity As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CongrCulture As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CreateDateDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CreateStaffNumDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents LastChangeDateDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents LastChangeStaffNumDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ReviewDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ReviewStaffNum As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ReviewResult As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
