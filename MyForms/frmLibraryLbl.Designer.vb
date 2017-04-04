<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmLibraryLbl
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmLibraryLbl))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cboWhat = New System.Windows.Forms.ComboBox()
        Me.cboRegion = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.fldNumSkip = New System.Windows.Forms.TextBox()
        Me.cboLabels = New System.Windows.Forms.ComboBox()
        Me.cboOverride = New System.Windows.Forms.ComboBox()
        Me.btnSort = New System.Windows.Forms.Button()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.cboFiled = New System.Windows.Forms.ComboBox()
        Me.btnClearSort = New System.Windows.Forms.Button()
        Me.btnUpdate = New System.Windows.Forms.Button()
        Me.btnShelfLabel = New System.Windows.Forms.Button()
        Me.cboStatus = New System.Windows.Forms.ComboBox()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.pgResources = New System.Windows.Forms.TabPage()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.GrpType = New System.Windows.Forms.GroupBox()
        Me.RadioButton10 = New System.Windows.Forms.CheckBox()
        Me.RadioButton9 = New System.Windows.Forms.CheckBox()
        Me.RadioButton8 = New System.Windows.Forms.CheckBox()
        Me.RadioButton7 = New System.Windows.Forms.CheckBox()
        Me.RadioButton6 = New System.Windows.Forms.CheckBox()
        Me.RadioButton5 = New System.Windows.Forms.CheckBox()
        Me.RadioButton4 = New System.Windows.Forms.CheckBox()
        Me.RadioButton3 = New System.Windows.Forms.CheckBox()
        Me.RadioButton2 = New System.Windows.Forms.CheckBox()
        Me.RadioButton1 = New System.Windows.Forms.CheckBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.pgPrint = New System.Windows.Forms.TabPage()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.fldNumRepeat = New System.Windows.Forms.TextBox()
        Me.btnPrint = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.pgUpdate = New System.Windows.Forms.TabPage()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.lstSortChoice = New System.Windows.Forms.ListBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.lblHeading = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.lblWhy = New System.Windows.Forms.Label()
        Me.StatusBar1 = New System.Windows.Forms.StatusBar()
        Me.StatusBarPanel1 = New System.Windows.Forms.StatusBarPanel()
        Me.StatusBarPanel3 = New System.Windows.Forms.StatusBarPanel()
        Me.StatusBarPanel2 = New System.Windows.Forms.StatusBarPanel()
        Me.DsResourceLocationDD1 = New InfoCtr.dsResourceLocationDD()
        Me.LuLocationStatusTableAdapter = New InfoCtr.dsResourceLocationDDTableAdapters.luLocationStatusTableAdapter()
        Me.LuLocationOverrideTableAdapter = New InfoCtr.dsResourceLocationDDTableAdapters.luLocationOverrideTableAdapter()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.ActualLocation = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.btnHelp = New System.Windows.Forms.Button()
        Me.grdvwMain = New System.Windows.Forms.DataGridView()
        Me.ResourceLocationID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ResourceNum = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Status = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Placement = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ResourceName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Author = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CRGMain = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ResourceType = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Why = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PrintLocation = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Publisher = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Active = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Staff = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.LocationEditDt = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CurrentLocation = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.OverrideLocation = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PrintSatellite = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.StatusNum = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ActualLocationNum = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.NewLocationNum = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.OverrideLocationNum = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.LibraryName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SatelliteRegion = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CRGRest = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.LocationStaffnum = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DsLibrary1 = New InfoCtr.dsLibrary()
        Me.LibLabelsTableAdapter = New InfoCtr.dsLibraryTableAdapters.LibLabelsNewTableAdapter()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.TabControl1.SuspendLayout()
        Me.pgResources.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.GrpType.SuspendLayout()
        Me.pgPrint.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.pgUpdate.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.StatusBarPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.StatusBarPanel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.StatusBarPanel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsResourceLocationDD1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel4.SuspendLayout()
        CType(Me.grdvwMain, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsLibrary1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cboWhat
        '
        Me.cboWhat.DropDownWidth = 250
        Me.cboWhat.FormattingEnabled = True
        Me.cboWhat.Location = New System.Drawing.Point(8, 121)
        Me.cboWhat.MaxDropDownItems = 25
        Me.cboWhat.Name = "cboWhat"
        Me.cboWhat.Size = New System.Drawing.Size(144, 21)
        Me.cboWhat.TabIndex = 9
        Me.ToolTip1.SetToolTip(Me.cboWhat, "Status and Type of Resource")
        '
        'cboRegion
        '
        Me.cboRegion.FormattingEnabled = True
        Me.cboRegion.Location = New System.Drawing.Point(8, 72)
        Me.cboRegion.Name = "cboRegion"
        Me.cboRegion.Size = New System.Drawing.Size(144, 21)
        Me.cboRegion.TabIndex = 1
        Me.cboRegion.Text = "Region"
        Me.ToolTip1.SetToolTip(Me.cboRegion, "Office Location of the Resource")
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(10, 114)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(113, 13)
        Me.Label5.TabIndex = 14
        Me.Label5.Text = "Print How Many Times"
        Me.ToolTip1.SetToolTip(Me.Label5, "How many of each label to print (media often takes more than 1 label).")
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(38, 88)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(82, 13)
        Me.Label4.TabIndex = 13
        Me.Label4.Text = "Skip How Many"
        Me.ToolTip1.SetToolTip(Me.Label4, "For when you are re-using a partial label sheet.")
        '
        'fldNumSkip
        '
        Me.fldNumSkip.Location = New System.Drawing.Point(126, 85)
        Me.fldNumSkip.Name = "fldNumSkip"
        Me.fldNumSkip.Size = New System.Drawing.Size(31, 20)
        Me.fldNumSkip.TabIndex = 11
        Me.fldNumSkip.Text = "0"
        Me.ToolTip1.SetToolTip(Me.fldNumSkip, "0 to 15.  More than that, start the label sheet from the bottom.")
        '
        'cboLabels
        '
        Me.cboLabels.DropDownWidth = 250
        Me.cboLabels.FormattingEnabled = True
        Me.cboLabels.Items.AddRange(New Object() {"Book Labels (30 up)", "Manilla File Labels  (30 up)", "Hanging File Labels  (10 up)", "", "None - Data file only (\\ICCNAS1\Users\greenc\DataLibLbl.xls)", "", "-------------------------", "future development.  for now select 'None - Data file only' and run your own merg" & _
                "e.", "", "Printout (Org address summary on regular paper)"})
        Me.cboLabels.Location = New System.Drawing.Point(3, 57)
        Me.cboLabels.Name = "cboLabels"
        Me.cboLabels.Size = New System.Drawing.Size(168, 21)
        Me.cboLabels.TabIndex = 3
        Me.ToolTip1.SetToolTip(Me.cboLabels, "Word will use this template.")
        '
        'cboOverride
        '
        Me.cboOverride.DisplayMember = "LocationName"
        Me.cboOverride.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboOverride.FormattingEnabled = True
        Me.cboOverride.Location = New System.Drawing.Point(11, 301)
        Me.cboOverride.Name = "cboOverride"
        Me.cboOverride.Size = New System.Drawing.Size(147, 21)
        Me.cboOverride.TabIndex = 16
        Me.ToolTip1.SetToolTip(Me.cboOverride, "Select new status, then click Run Update")
        Me.cboOverride.ValueMember = "LocationSetupID"
        '
        'btnSort
        '
        Me.btnSort.Location = New System.Drawing.Point(114, 47)
        Me.btnSort.Name = "btnSort"
        Me.btnSort.Size = New System.Drawing.Size(59, 42)
        Me.btnSort.TabIndex = 20
        Me.btnSort.Text = "Do Sort"
        Me.ToolTip1.SetToolTip(Me.btnSort, "To sort by multiple columns: select items from list, then click Sort button.")
        Me.btnSort.UseVisualStyleBackColor = True
        '
        'btnClear
        '
        Me.btnClear.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClear.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte), True)
        Me.btnClear.Location = New System.Drawing.Point(164, 241)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(13, 81)
        Me.btnClear.TabIndex = 19
        Me.btnClear.Text = "Clear"
        Me.ToolTip1.SetToolTip(Me.btnClear, "Clear combo boxes")
        Me.btnClear.UseVisualStyleBackColor = True
        Me.btnClear.Visible = False
        '
        'cboFiled
        '
        Me.cboFiled.DisplayMember = "LocationName"
        Me.cboFiled.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboFiled.FormattingEnabled = True
        Me.cboFiled.Location = New System.Drawing.Point(11, 107)
        Me.cboFiled.Name = "cboFiled"
        Me.cboFiled.Size = New System.Drawing.Size(147, 21)
        Me.cboFiled.TabIndex = 20
        Me.ToolTip1.SetToolTip(Me.cboFiled, "Placement is the Actual Location of the Resource")
        Me.cboFiled.ValueMember = "LocationSetupID"
        '
        'btnClearSort
        '
        Me.btnClearSort.Location = New System.Drawing.Point(114, 108)
        Me.btnClearSort.Name = "btnClearSort"
        Me.btnClearSort.Size = New System.Drawing.Size(59, 34)
        Me.btnClearSort.TabIndex = 24
        Me.btnClearSort.Text = "Clear Sort Order"
        Me.ToolTip1.SetToolTip(Me.btnClearSort, "To sort by multiple columns: select items from list, then click Sort button.")
        Me.btnClearSort.UseVisualStyleBackColor = True
        '
        'btnUpdate
        '
        Me.btnUpdate.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnUpdate.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnUpdate.Location = New System.Drawing.Point(48, 51)
        Me.btnUpdate.Name = "btnUpdate"
        Me.btnUpdate.Size = New System.Drawing.Size(98, 34)
        Me.btnUpdate.TabIndex = 6
        Me.btnUpdate.Text = "Do Update"
        Me.ToolTip1.SetToolTip(Me.btnUpdate, "Select Placement and/or Status and click Update.")
        Me.btnUpdate.UseVisualStyleBackColor = True
        '
        'btnShelfLabel
        '
        Me.btnShelfLabel.Location = New System.Drawing.Point(21, 293)
        Me.btnShelfLabel.Name = "btnShelfLabel"
        Me.btnShelfLabel.Size = New System.Drawing.Size(158, 23)
        Me.btnShelfLabel.TabIndex = 18
        Me.btnShelfLabel.Text = "Llibray Shelf Label Inserts"
        Me.ToolTip1.SetToolTip(Me.btnShelfLabel, "Administration, etc.  Print and cut these to place in plastic holders stuck on sh" & _
        "elves.")
        Me.btnShelfLabel.UseVisualStyleBackColor = True
        '
        'cboStatus
        '
        Me.cboStatus.DisplayMember = "LocationName"
        Me.cboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboStatus.FormattingEnabled = True
        Me.cboStatus.Location = New System.Drawing.Point(11, 238)
        Me.cboStatus.Name = "cboStatus"
        Me.cboStatus.Size = New System.Drawing.Size(147, 21)
        Me.cboStatus.TabIndex = 7
        Me.cboStatus.ValueMember = "LocationSetupID"
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.pgResources)
        Me.TabControl1.Controls.Add(Me.pgPrint)
        Me.TabControl1.Controls.Add(Me.pgUpdate)
        Me.TabControl1.Location = New System.Drawing.Point(12, 12)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(203, 366)
        Me.TabControl1.TabIndex = 17
        '
        'pgResources
        '
        Me.pgResources.BackColor = System.Drawing.Color.Transparent
        Me.pgResources.Controls.Add(Me.Panel2)
        Me.pgResources.Location = New System.Drawing.Point(4, 22)
        Me.pgResources.Name = "pgResources"
        Me.pgResources.Padding = New System.Windows.Forms.Padding(3)
        Me.pgResources.Size = New System.Drawing.Size(195, 340)
        Me.pgResources.TabIndex = 0
        Me.pgResources.Text = "List Resources"
        '
        'Panel2
        '
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel2.Controls.Add(Me.btnSearch)
        Me.Panel2.Controls.Add(Me.GrpType)
        Me.Panel2.Controls.Add(Me.Label8)
        Me.Panel2.Controls.Add(Me.Label3)
        Me.Panel2.Controls.Add(Me.cboWhat)
        Me.Panel2.Controls.Add(Me.Label1)
        Me.Panel2.Controls.Add(Me.cboRegion)
        Me.Panel2.Location = New System.Drawing.Point(6, 6)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(164, 333)
        Me.Panel2.TabIndex = 16
        '
        'btnSearch
        '
        Me.btnSearch.Location = New System.Drawing.Point(78, 30)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(74, 23)
        Me.btnSearch.TabIndex = 13
        Me.btnSearch.Text = "Search"
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'GrpType
        '
        Me.GrpType.Controls.Add(Me.RadioButton10)
        Me.GrpType.Controls.Add(Me.RadioButton9)
        Me.GrpType.Controls.Add(Me.RadioButton8)
        Me.GrpType.Controls.Add(Me.RadioButton7)
        Me.GrpType.Controls.Add(Me.RadioButton6)
        Me.GrpType.Controls.Add(Me.RadioButton5)
        Me.GrpType.Controls.Add(Me.RadioButton4)
        Me.GrpType.Controls.Add(Me.RadioButton3)
        Me.GrpType.Controls.Add(Me.RadioButton2)
        Me.GrpType.Controls.Add(Me.RadioButton1)
        Me.GrpType.Location = New System.Drawing.Point(3, 160)
        Me.GrpType.Name = "GrpType"
        Me.GrpType.Size = New System.Drawing.Size(154, 166)
        Me.GrpType.TabIndex = 12
        Me.GrpType.TabStop = False
        Me.GrpType.Text = "Type of Resource"
        '
        'RadioButton10
        '
        Me.RadioButton10.AutoSize = True
        Me.RadioButton10.Font = New System.Drawing.Font("Comic Sans MS", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadioButton10.Location = New System.Drawing.Point(6, 91)
        Me.RadioButton10.Name = "RadioButton10"
        Me.RadioButton10.Size = New System.Drawing.Size(50, 17)
        Me.RadioButton10.TabIndex = 9
        Me.RadioButton10.Tag = "2"
        Me.RadioButton10.Text = "Topic"
        Me.RadioButton10.UseVisualStyleBackColor = True
        '
        'RadioButton9
        '
        Me.RadioButton9.AutoSize = True
        Me.RadioButton9.Font = New System.Drawing.Font("Comic Sans MS", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadioButton9.Location = New System.Drawing.Point(5, 119)
        Me.RadioButton9.Name = "RadioButton9"
        Me.RadioButton9.Size = New System.Drawing.Size(70, 17)
        Me.RadioButton9.TabIndex = 8
        Me.RadioButton9.Text = "Equipment"
        Me.RadioButton9.UseVisualStyleBackColor = True
        '
        'RadioButton8
        '
        Me.RadioButton8.AutoSize = True
        Me.RadioButton8.Font = New System.Drawing.Font("Comic Sans MS", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadioButton8.Location = New System.Drawing.Point(85, 119)
        Me.RadioButton8.Name = "RadioButton8"
        Me.RadioButton8.Size = New System.Drawing.Size(50, 17)
        Me.RadioButton8.TabIndex = 7
        Me.RadioButton8.Text = "Event"
        Me.RadioButton8.UseVisualStyleBackColor = True
        '
        'RadioButton7
        '
        Me.RadioButton7.AutoSize = True
        Me.RadioButton7.Font = New System.Drawing.Font("Comic Sans MS", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadioButton7.Location = New System.Drawing.Point(85, 142)
        Me.RadioButton7.Name = "RadioButton7"
        Me.RadioButton7.Size = New System.Drawing.Size(65, 17)
        Me.RadioButton7.TabIndex = 6
        Me.RadioButton7.Text = "Software"
        Me.RadioButton7.UseVisualStyleBackColor = True
        '
        'RadioButton6
        '
        Me.RadioButton6.AutoSize = True
        Me.RadioButton6.Font = New System.Drawing.Font("Comic Sans MS", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadioButton6.Location = New System.Drawing.Point(85, 70)
        Me.RadioButton6.Name = "RadioButton6"
        Me.RadioButton6.Size = New System.Drawing.Size(54, 17)
        Me.RadioButton6.TabIndex = 5
        Me.RadioButton6.Tag = "2"
        Me.RadioButton6.Text = "Person"
        Me.RadioButton6.UseVisualStyleBackColor = True
        '
        'RadioButton5
        '
        Me.RadioButton5.AutoSize = True
        Me.RadioButton5.Font = New System.Drawing.Font("Comic Sans MS", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadioButton5.Location = New System.Drawing.Point(6, 70)
        Me.RadioButton5.Name = "RadioButton5"
        Me.RadioButton5.Size = New System.Drawing.Size(81, 17)
        Me.RadioButton5.TabIndex = 4
        Me.RadioButton5.Tag = "2"
        Me.RadioButton5.Text = "Organization"
        Me.RadioButton5.UseVisualStyleBackColor = True
        '
        'RadioButton4
        '
        Me.RadioButton4.AutoSize = True
        Me.RadioButton4.Font = New System.Drawing.Font("Comic Sans MS", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadioButton4.Location = New System.Drawing.Point(5, 142)
        Me.RadioButton4.Name = "RadioButton4"
        Me.RadioButton4.Size = New System.Drawing.Size(68, 17)
        Me.RadioButton4.TabIndex = 3
        Me.RadioButton4.Text = "Periodical"
        Me.RadioButton4.UseVisualStyleBackColor = True
        '
        'RadioButton3
        '
        Me.RadioButton3.AutoSize = True
        Me.RadioButton3.Checked = True
        Me.RadioButton3.CheckState = System.Windows.Forms.CheckState.Checked
        Me.RadioButton3.Font = New System.Drawing.Font("Comic Sans MS", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadioButton3.Location = New System.Drawing.Point(6, 40)
        Me.RadioButton3.Name = "RadioButton3"
        Me.RadioButton3.Size = New System.Drawing.Size(52, 17)
        Me.RadioButton3.TabIndex = 2
        Me.RadioButton3.Tag = "1"
        Me.RadioButton3.Text = "Media"
        Me.RadioButton3.UseVisualStyleBackColor = True
        '
        'RadioButton2
        '
        Me.RadioButton2.AutoSize = True
        Me.RadioButton2.Checked = True
        Me.RadioButton2.CheckState = System.Windows.Forms.CheckState.Checked
        Me.RadioButton2.Font = New System.Drawing.Font("Comic Sans MS", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadioButton2.Location = New System.Drawing.Point(85, 19)
        Me.RadioButton2.Name = "RadioButton2"
        Me.RadioButton2.Size = New System.Drawing.Size(47, 17)
        Me.RadioButton2.TabIndex = 1
        Me.RadioButton2.Tag = "1"
        Me.RadioButton2.Text = "Book"
        Me.RadioButton2.UseVisualStyleBackColor = True
        '
        'RadioButton1
        '
        Me.RadioButton1.AutoSize = True
        Me.RadioButton1.Checked = True
        Me.RadioButton1.CheckState = System.Windows.Forms.CheckState.Checked
        Me.RadioButton1.Font = New System.Drawing.Font("Comic Sans MS", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadioButton1.Location = New System.Drawing.Point(6, 19)
        Me.RadioButton1.Name = "RadioButton1"
        Me.RadioButton1.Size = New System.Drawing.Size(55, 17)
        Me.RadioButton1.TabIndex = 0
        Me.RadioButton1.Tag = "1"
        Me.RadioButton1.Text = "Article"
        Me.RadioButton1.UseVisualStyleBackColor = True
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.SystemColors.InactiveCaption
        Me.Label8.Location = New System.Drawing.Point(5, 6)
        Me.Label8.MaximumSize = New System.Drawing.Size(200, 0)
        Me.Label8.MinimumSize = New System.Drawing.Size(0, 30)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(161, 30)
        Me.Label8.TabIndex = 11
        Me.Label8.Text = "Fill Grid by selecting these options"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(7, 105)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(92, 13)
        Me.Label3.TabIndex = 10
        Me.Label3.Text = "Which Resources"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(5, 56)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(81, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Satellite Region"
        '
        'pgPrint
        '
        Me.pgPrint.Controls.Add(Me.btnShelfLabel)
        Me.pgPrint.Controls.Add(Me.Panel3)
        Me.pgPrint.Location = New System.Drawing.Point(4, 22)
        Me.pgPrint.Name = "pgPrint"
        Me.pgPrint.Padding = New System.Windows.Forms.Padding(3)
        Me.pgPrint.Size = New System.Drawing.Size(195, 340)
        Me.pgPrint.TabIndex = 1
        Me.pgPrint.Text = "Print"
        Me.pgPrint.UseVisualStyleBackColor = True
        '
        'Panel3
        '
        Me.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel3.Controls.Add(Me.Label9)
        Me.Panel3.Controls.Add(Me.Label5)
        Me.Panel3.Controls.Add(Me.Label4)
        Me.Panel3.Controls.Add(Me.fldNumRepeat)
        Me.Panel3.Controls.Add(Me.fldNumSkip)
        Me.Panel3.Controls.Add(Me.btnPrint)
        Me.Panel3.Controls.Add(Me.Label2)
        Me.Panel3.Controls.Add(Me.cboLabels)
        Me.Panel3.Location = New System.Drawing.Point(6, 6)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(183, 190)
        Me.Panel3.TabIndex = 17
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.SystemColors.InactiveCaption
        Me.Label9.Location = New System.Drawing.Point(3, 6)
        Me.Label9.MaximumSize = New System.Drawing.Size(115, 50)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(110, 26)
        Me.Label9.TabIndex = 15
        Me.Label9.Text = "Export Data file and Create Labels"
        '
        'fldNumRepeat
        '
        Me.fldNumRepeat.Location = New System.Drawing.Point(126, 111)
        Me.fldNumRepeat.Name = "fldNumRepeat"
        Me.fldNumRepeat.Size = New System.Drawing.Size(31, 20)
        Me.fldNumRepeat.TabIndex = 12
        Me.fldNumRepeat.Text = "1"
        '
        'btnPrint
        '
        Me.btnPrint.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrint.Location = New System.Drawing.Point(66, 142)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(91, 30)
        Me.btnPrint.TabIndex = 5
        Me.btnPrint.Text = "Run Labels"
        Me.btnPrint.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(7, 41)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(80, 13)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Label Template"
        '
        'pgUpdate
        '
        Me.pgUpdate.Controls.Add(Me.Panel1)
        Me.pgUpdate.Location = New System.Drawing.Point(4, 22)
        Me.pgUpdate.Name = "pgUpdate"
        Me.pgUpdate.Padding = New System.Windows.Forms.Padding(3)
        Me.pgUpdate.Size = New System.Drawing.Size(195, 340)
        Me.pgUpdate.TabIndex = 2
        Me.pgUpdate.Text = "Update"
        Me.pgUpdate.ToolTipText = "Run update after Printing labels."
        Me.pgUpdate.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel1.Controls.Add(Me.Label13)
        Me.Panel1.Controls.Add(Me.Label12)
        Me.Panel1.Controls.Add(Me.cboFiled)
        Me.Panel1.Controls.Add(Me.Label10)
        Me.Panel1.Controls.Add(Me.btnUpdate)
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Controls.Add(Me.btnClear)
        Me.Panel1.Controls.Add(Me.cboOverride)
        Me.Panel1.Controls.Add(Me.Label7)
        Me.Panel1.Controls.Add(Me.cboStatus)
        Me.Panel1.Location = New System.Drawing.Point(6, 3)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(184, 337)
        Me.Panel1.TabIndex = 9
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.SystemColors.InactiveCaption
        Me.Label13.Location = New System.Drawing.Point(7, 9)
        Me.Label13.MaximumSize = New System.Drawing.Size(160, 50)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(152, 39)
        Me.Label13.TabIndex = 22
        Me.Label13.Text = "After printing labels, click Run update with Placement as 'Filed'."
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(8, 91)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(153, 13)
        Me.Label12.TabIndex = 21
        Me.Label12.Text = "Placement    (physical location)"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.SystemColors.InactiveCaption
        Me.Label10.Location = New System.Drawing.Point(7, 147)
        Me.Label10.MaximumSize = New System.Drawing.Size(160, 50)
        Me.Label10.MinimumSize = New System.Drawing.Size(0, 70)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(157, 70)
        Me.Label10.TabIndex = 17
        Me.Label10.Text = "Or during library inventory, you can change the Placement, or Status of selected " & _
    "resources if they need relabelling."
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(10, 222)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(160, 13)
        Me.Label6.TabIndex = 14
        Me.Label6.Text = "Status    (process/determination)"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(8, 285)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(137, 13)
        Me.Label7.TabIndex = 15
        Me.Label7.Text = "Override Location (for label)"
        '
        'lstSortChoice
        '
        Me.lstSortChoice.AllowDrop = True
        Me.lstSortChoice.FormattingEnabled = True
        Me.lstSortChoice.Location = New System.Drawing.Point(12, 47)
        Me.lstSortChoice.Name = "lstSortChoice"
        Me.lstSortChoice.Size = New System.Drawing.Size(96, 95)
        Me.lstSortChoice.TabIndex = 18
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(221, 10)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(0, 13)
        Me.Label11.TabIndex = 21
        '
        'lblHeading
        '
        Me.lblHeading.AutoSize = True
        Me.lblHeading.ForeColor = System.Drawing.SystemColors.Highlight
        Me.lblHeading.Location = New System.Drawing.Point(227, 18)
        Me.lblHeading.MinimumSize = New System.Drawing.Size(300, 0)
        Me.lblHeading.Name = "lblHeading"
        Me.lblHeading.Size = New System.Drawing.Size(300, 13)
        Me.lblHeading.TabIndex = 22
        Me.lblHeading.Text = "Resources by Region Location"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.ForeColor = System.Drawing.SystemColors.Highlight
        Me.Label14.Location = New System.Drawing.Point(11, 5)
        Me.Label14.MaximumSize = New System.Drawing.Size(150, 0)
        Me.Label14.MinimumSize = New System.Drawing.Size(0, 30)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(150, 39)
        Me.Label14.TabIndex = 23
        Me.Label14.Text = "to sort by multiple columns, select them in order here then click SortGo."
        '
        'lblWhy
        '
        Me.lblWhy.AutoSize = True
        Me.lblWhy.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblWhy.ForeColor = System.Drawing.SystemColors.Highlight
        Me.lblWhy.Location = New System.Drawing.Point(9, 553)
        Me.lblWhy.MaximumSize = New System.Drawing.Size(150, 0)
        Me.lblWhy.MinimumSize = New System.Drawing.Size(200, 30)
        Me.lblWhy.Name = "lblWhy"
        Me.lblWhy.Size = New System.Drawing.Size(200, 104)
        Me.lblWhy.TabIndex = 25
        Me.lblWhy.Text = resources.GetString("lblWhy.Text")
        '
        'StatusBar1
        '
        Me.StatusBar1.Location = New System.Drawing.Point(0, 765)
        Me.StatusBar1.Name = "StatusBar1"
        Me.StatusBar1.Panels.AddRange(New System.Windows.Forms.StatusBarPanel() {Me.StatusBarPanel1, Me.StatusBarPanel3, Me.StatusBarPanel2})
        Me.StatusBar1.ShowPanels = True
        Me.StatusBar1.Size = New System.Drawing.Size(1184, 22)
        Me.StatusBar1.TabIndex = 224
        '
        'StatusBarPanel1
        '
        Me.StatusBarPanel1.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents
        Me.StatusBarPanel1.MinWidth = 300
        Me.StatusBarPanel1.Name = "StatusBarPanel1"
        Me.StatusBarPanel1.Text = "Ready"
        Me.StatusBarPanel1.Width = 300
        '
        'StatusBarPanel3
        '
        Me.StatusBarPanel3.Alignment = System.Windows.Forms.HorizontalAlignment.Center
        Me.StatusBarPanel3.Name = "StatusBarPanel3"
        '
        'StatusBarPanel2
        '
        Me.StatusBarPanel2.Alignment = System.Windows.Forms.HorizontalAlignment.Right
        Me.StatusBarPanel2.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring
        Me.StatusBarPanel2.BorderStyle = System.Windows.Forms.StatusBarPanelBorderStyle.None
        Me.StatusBarPanel2.Name = "StatusBarPanel2"
        Me.StatusBarPanel2.Text = "Click button to select report or utility."
        Me.StatusBarPanel2.Width = 767
        '
        'DsResourceLocationDD1
        '
        Me.DsResourceLocationDD1.DataSetName = "dsResourceLocationDD"
        Me.DsResourceLocationDD1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'LuLocationStatusTableAdapter
        '
        Me.LuLocationStatusTableAdapter.ClearBeforeFill = True
        '
        'LuLocationOverrideTableAdapter
        '
        Me.LuLocationOverrideTableAdapter.ClearBeforeFill = True
        '
        'Panel4
        '
        Me.Panel4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel4.Controls.Add(Me.btnClearSort)
        Me.Panel4.Controls.Add(Me.Label14)
        Me.Panel4.Controls.Add(Me.btnSort)
        Me.Panel4.Controls.Add(Me.lstSortChoice)
        Me.Panel4.Location = New System.Drawing.Point(22, 384)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(185, 155)
        Me.Panel4.TabIndex = 225
        '
        'ActualLocation
        '
        Me.ActualLocation.DataPropertyName = "ActualLocation"
        Me.ActualLocation.HeaderText = "Location"
        Me.ActualLocation.Name = "ActualLocation"
        Me.ActualLocation.ReadOnly = True
        '
        'btnHelp
        '
        Me.btnHelp.Image = CType(resources.GetObject("btnHelp.Image"), System.Drawing.Image)
        Me.btnHelp.Location = New System.Drawing.Point(868, 3)
        Me.btnHelp.Name = "btnHelp"
        Me.btnHelp.Size = New System.Drawing.Size(25, 25)
        Me.btnHelp.TabIndex = 421
        Me.btnHelp.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnHelp.UseVisualStyleBackColor = True
        '
        'grdvwMain
        '
        Me.grdvwMain.AllowUserToAddRows = False
        Me.grdvwMain.AllowUserToDeleteRows = False
        Me.grdvwMain.AllowUserToOrderColumns = True
        Me.grdvwMain.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdvwMain.AutoGenerateColumns = False
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdvwMain.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.grdvwMain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdvwMain.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ResourceLocationID, Me.ResourceNum, Me.Status, Me.Placement, Me.ResourceName, Me.Author, Me.CRGMain, Me.ResourceType, Me.Why, Me.PrintLocation, Me.Publisher, Me.Active, Me.Staff, Me.LocationEditDt, Me.CurrentLocation, Me.OverrideLocation, Me.PrintSatellite, Me.StatusNum, Me.ActualLocationNum, Me.NewLocationNum, Me.OverrideLocationNum, Me.LibraryName, Me.SatelliteRegion, Me.CRGRest, Me.LocationStaffnum})
        Me.grdvwMain.DataMember = "LibLabelsNew"
        Me.grdvwMain.DataSource = Me.DsLibrary1
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.Color.DarkGreen
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.grdvwMain.DefaultCellStyle = DataGridViewCellStyle2
        Me.grdvwMain.Location = New System.Drawing.Point(227, 34)
        Me.grdvwMain.Name = "grdvwMain"
        Me.grdvwMain.ReadOnly = True
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdvwMain.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.grdvwMain.RowHeadersWidth = 20
        Me.grdvwMain.Size = New System.Drawing.Size(928, 641)
        Me.grdvwMain.TabIndex = 0
        '
        'ResourceLocationID
        '
        Me.ResourceLocationID.DataPropertyName = "ResourceLocationID"
        Me.ResourceLocationID.HeaderText = "ResourceLocationID"
        Me.ResourceLocationID.Name = "ResourceLocationID"
        Me.ResourceLocationID.ReadOnly = True
        Me.ResourceLocationID.Visible = False
        Me.ResourceLocationID.Width = 5
        '
        'ResourceNum
        '
        Me.ResourceNum.DataPropertyName = "ICCResourceID"
        Me.ResourceNum.HeaderText = "Resource Num"
        Me.ResourceNum.Name = "ResourceNum"
        Me.ResourceNum.ReadOnly = True
        Me.ResourceNum.Width = 40
        '
        'Status
        '
        Me.Status.DataPropertyName = "Status"
        Me.Status.HeaderText = "Status"
        Me.Status.Name = "Status"
        Me.Status.ReadOnly = True
        Me.Status.Width = 50
        '
        'Placement
        '
        Me.Placement.DataPropertyName = "Placement"
        Me.Placement.HeaderText = "Current Placement"
        Me.Placement.Name = "Placement"
        Me.Placement.ReadOnly = True
        Me.Placement.Width = 65
        '
        'ResourceName
        '
        Me.ResourceName.DataPropertyName = "ResourceName"
        Me.ResourceName.HeaderText = "ResourceName"
        Me.ResourceName.Name = "ResourceName"
        Me.ResourceName.ReadOnly = True
        Me.ResourceName.Width = 150
        '
        'Author
        '
        Me.Author.DataPropertyName = "Author"
        Me.Author.HeaderText = "Author"
        Me.Author.Name = "Author"
        Me.Author.ReadOnly = True
        '
        'CRGMain
        '
        Me.CRGMain.DataPropertyName = "CRGMain"
        Me.CRGMain.HeaderText = "CRGMain"
        Me.CRGMain.Name = "CRGMain"
        Me.CRGMain.ReadOnly = True
        '
        'ResourceType
        '
        Me.ResourceType.DataPropertyName = "ResourceType"
        Me.ResourceType.HeaderText = "ResourceType"
        Me.ResourceType.Name = "ResourceType"
        Me.ResourceType.ReadOnly = True
        '
        'Why
        '
        Me.Why.DataPropertyName = "Why"
        Me.Why.HeaderText = "Why"
        Me.Why.Name = "Why"
        Me.Why.ReadOnly = True
        Me.Why.ToolTipText = "Shaded = should be in Public Library"
        Me.Why.Width = 50
        '
        'PrintLocation
        '
        Me.PrintLocation.DataPropertyName = "NewLocation"
        Me.PrintLocation.HeaderText = "Label will print"
        Me.PrintLocation.Name = "PrintLocation"
        Me.PrintLocation.ReadOnly = True
        Me.PrintLocation.Width = 75
        '
        'Publisher
        '
        Me.Publisher.DataPropertyName = "Publisher"
        Me.Publisher.HeaderText = "Publisher"
        Me.Publisher.Name = "Publisher"
        Me.Publisher.ReadOnly = True
        '
        'Active
        '
        Me.Active.DataPropertyName = "ActiveStr"
        Me.Active.HeaderText = "Active"
        Me.Active.Name = "Active"
        Me.Active.ReadOnly = True
        Me.Active.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Active.Width = 25
        '
        'Staff
        '
        Me.Staff.DataPropertyName = "LocationStaff"
        Me.Staff.HeaderText = "Staff"
        Me.Staff.Name = "Staff"
        Me.Staff.ReadOnly = True
        '
        'LocationEditDt
        '
        Me.LocationEditDt.DataPropertyName = "LocationEditDt"
        Me.LocationEditDt.HeaderText = "LocationEditDt"
        Me.LocationEditDt.Name = "LocationEditDt"
        Me.LocationEditDt.ReadOnly = True
        '
        'CurrentLocation
        '
        Me.CurrentLocation.DataPropertyName = "CurrentLocation"
        Me.CurrentLocation.HeaderText = "CurrentLocation"
        Me.CurrentLocation.Name = "CurrentLocation"
        Me.CurrentLocation.ReadOnly = True
        '
        'OverrideLocation
        '
        Me.OverrideLocation.DataPropertyName = "OverrideLocation"
        Me.OverrideLocation.HeaderText = "OverrideLocation"
        Me.OverrideLocation.Name = "OverrideLocation"
        Me.OverrideLocation.ReadOnly = True
        '
        'PrintSatellite
        '
        Me.PrintSatellite.DataPropertyName = "LibraryName"
        Me.PrintSatellite.HeaderText = "PrintSatellite"
        Me.PrintSatellite.Name = "PrintSatellite"
        Me.PrintSatellite.ReadOnly = True
        Me.PrintSatellite.Visible = False
        '
        'StatusNum
        '
        Me.StatusNum.DataPropertyName = "StatusNum"
        Me.StatusNum.HeaderText = "StatusNum"
        Me.StatusNum.Name = "StatusNum"
        Me.StatusNum.ReadOnly = True
        Me.StatusNum.Visible = False
        Me.StatusNum.Width = 5
        '
        'ActualLocationNum
        '
        Me.ActualLocationNum.DataPropertyName = "ActualLocationNum"
        Me.ActualLocationNum.HeaderText = "ActualLocationNum"
        Me.ActualLocationNum.Name = "ActualLocationNum"
        Me.ActualLocationNum.ReadOnly = True
        Me.ActualLocationNum.Width = 5
        '
        'NewLocationNum
        '
        Me.NewLocationNum.DataPropertyName = "NewLocationNum"
        Me.NewLocationNum.HeaderText = "NewLocationNum"
        Me.NewLocationNum.Name = "NewLocationNum"
        Me.NewLocationNum.ReadOnly = True
        Me.NewLocationNum.Visible = False
        Me.NewLocationNum.Width = 5
        '
        'OverrideLocationNum
        '
        Me.OverrideLocationNum.DataPropertyName = "OverrideLocationNum"
        Me.OverrideLocationNum.HeaderText = "OverrideLocationNum"
        Me.OverrideLocationNum.Name = "OverrideLocationNum"
        Me.OverrideLocationNum.ReadOnly = True
        '
        'LibraryName
        '
        Me.LibraryName.DataPropertyName = "LibraryName"
        Me.LibraryName.HeaderText = "LibraryName"
        Me.LibraryName.Name = "LibraryName"
        Me.LibraryName.ReadOnly = True
        Me.LibraryName.Visible = False
        '
        'SatelliteRegion
        '
        Me.SatelliteRegion.DataPropertyName = "SatelliteRegion"
        Me.SatelliteRegion.HeaderText = "SatelliteRegion"
        Me.SatelliteRegion.Name = "SatelliteRegion"
        Me.SatelliteRegion.ReadOnly = True
        Me.SatelliteRegion.Visible = False
        '
        'CRGRest
        '
        Me.CRGRest.DataPropertyName = "CRGRest"
        Me.CRGRest.HeaderText = "CRGRest"
        Me.CRGRest.Name = "CRGRest"
        Me.CRGRest.ReadOnly = True
        Me.CRGRest.Visible = False
        '
        'LocationStaffnum
        '
        Me.LocationStaffnum.DataPropertyName = "LocationStaffnum"
        Me.LocationStaffnum.HeaderText = "LocationStaffnum"
        Me.LocationStaffnum.Name = "LocationStaffnum"
        Me.LocationStaffnum.ReadOnly = True
        Me.LocationStaffnum.Visible = False
        '
        'DsLibrary1
        '
        Me.DsLibrary1.DataSetName = "dsLibrary"
        Me.DsLibrary1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'LibLabelsTableAdapter
        '
        Me.LibLabelsTableAdapter.ClearBeforeFill = True
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(13, 678)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(444, 65)
        Me.Label15.TabIndex = 422
        Me.Label15.Text = resources.GetString("Label15.Text")
        '
        'frmLibraryLbl
        '
        Me.AcceptButton = Me.btnSearch
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1184, 787)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.btnHelp)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.StatusBar1)
        Me.Controls.Add(Me.lblWhy)
        Me.Controls.Add(Me.lblHeading)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.grdvwMain)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmLibraryLbl"
        Me.Text = "frmLibraryLbl"
        Me.TabControl1.ResumeLayout(False)
        Me.pgResources.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.GrpType.ResumeLayout(False)
        Me.GrpType.PerformLayout()
        Me.pgPrint.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.pgUpdate.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.StatusBarPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.StatusBarPanel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.StatusBarPanel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsResourceLocationDD1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        CType(Me.grdvwMain, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsLibrary1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents grdvwMain As System.Windows.Forms.DataGridView
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents DsLibrary1 As InfoCtr.dsLibrary
    Friend WithEvents ResourceLocationID1DataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents LocationNum1DataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents pgResources As System.Windows.Forms.TabPage
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cboWhat As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cboRegion As System.Windows.Forms.ComboBox
    Friend WithEvents pgPrint As System.Windows.Forms.TabPage
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents fldNumRepeat As System.Windows.Forms.TextBox
    Friend WithEvents fldNumSkip As System.Windows.Forms.TextBox
    Friend WithEvents btnPrint As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cboLabels As System.Windows.Forms.ComboBox
    Friend WithEvents pgUpdate As System.Windows.Forms.TabPage
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents btnUpdate As System.Windows.Forms.Button
    Friend WithEvents cboOverride As System.Windows.Forms.ComboBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents cboStatus As System.Windows.Forms.ComboBox
    Friend WithEvents lstSortChoice As System.Windows.Forms.ListBox
    Friend WithEvents btnSort As System.Windows.Forms.Button
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents LocationNameDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents LocationNumDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents ResourceIDDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents LocationRegionDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CRGDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SatelliteNameDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents cboFiled As System.Windows.Forms.ComboBox
    Friend WithEvents lblHeading As System.Windows.Forms.Label
    Friend WithEvents DsResourceLocationDD1 As InfoCtr.dsResourceLocationDD
    Friend WithEvents LuLocationStatusTableAdapter As InfoCtr.dsResourceLocationDDTableAdapters.luLocationStatusTableAdapter

    Friend WithEvents LuLocationOverrideTableAdapter As InfoCtr.dsResourceLocationDDTableAdapters.luLocationOverrideTableAdapter
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents btnClearSort As System.Windows.Forms.Button
    Friend WithEvents lblWhy As System.Windows.Forms.Label
    Friend WithEvents StatusBar1 As System.Windows.Forms.StatusBar
    Friend WithEvents StatusBarPanel1 As System.Windows.Forms.StatusBarPanel
    Friend WithEvents StatusBarPanel3 As System.Windows.Forms.StatusBarPanel
    Friend WithEvents StatusBarPanel2 As System.Windows.Forms.StatusBarPanel
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents btnHelp As System.Windows.Forms.Button
    Friend WithEvents btnShelfLabel As System.Windows.Forms.Button
    Friend WithEvents ActualLocation As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SubtypeDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents GrpType As System.Windows.Forms.GroupBox
    Friend WithEvents RadioButton1 As System.Windows.Forms.CheckBox
    Friend WithEvents RadioButton10 As System.Windows.Forms.CheckBox
    Friend WithEvents RadioButton9 As System.Windows.Forms.CheckBox
    Friend WithEvents RadioButton8 As System.Windows.Forms.CheckBox
    Friend WithEvents RadioButton7 As System.Windows.Forms.CheckBox
    Friend WithEvents RadioButton6 As System.Windows.Forms.CheckBox
    Friend WithEvents RadioButton5 As System.Windows.Forms.CheckBox
    Friend WithEvents RadioButton4 As System.Windows.Forms.CheckBox
    Friend WithEvents RadioButton3 As System.Windows.Forms.CheckBox
    Friend WithEvents RadioButton2 As System.Windows.Forms.CheckBox
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents LibLabelsTableAdapter As InfoCtr.dsLibraryTableAdapters.LibLabelsNewTableAdapter
    Friend WithEvents Type As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents OverrideNum As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents ResourceLocationID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ResourceNum As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Status As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Placement As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ResourceName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Author As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CRGMain As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ResourceType As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Why As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PrintLocation As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Publisher As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Active As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Staff As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents LocationEditDt As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CurrentLocation As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents OverrideLocation As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PrintSatellite As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents StatusNum As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ActualLocationNum As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents NewLocationNum As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents OverrideLocationNum As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents LibraryName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SatelliteRegion As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CRGRest As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents LocationStaffnum As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
