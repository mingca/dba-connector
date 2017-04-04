Imports System.Data.SqlClient
Imports system.Text

'Imports System.Threading    'required for sleep command
Public Class frmReviewOrgChild
    Inherits System.Windows.Forms.Form

    'Implements IFormattable


    Dim dt As DataTable 'for datagrid filter
    Dim colName As DataColumn 'name of column
    Public hti As System.Windows.Forms.DataGrid.HitTestInfo
    Dim dv As DataView 'for filtering search results
    '  Dim frm As New frmMainEdEvent
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents btnGo As System.Windows.Forms.Button
    Friend WithEvents DataSet1 As System.Data.DataSet
    Friend WithEvents DataTable1 As System.Data.DataTable
    Friend WithEvents DataColumn1 As System.Data.DataColumn
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents OrgID As System.Windows.Forms.DataGridViewTextBoxColumn
    '  Dim da As New SqlDataAdapter    'on another form

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

    Private components As System.ComponentModel.IContainer

    'Required by the Windows Form Designer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Protected Friend WithEvents DataGridTableStyle1 As System.Windows.Forms.DataGridTableStyle
    Protected Friend WithEvents DataGridTextBoxColumn1 As System.Windows.Forms.DataGridTextBoxColumn
    Protected Friend WithEvents DataGridTextBoxColumn2 As System.Windows.Forms.DataGridTextBoxColumn
    Protected Friend WithEvents DataGridTextBoxColumn3 As System.Windows.Forms.DataGridTextBoxColumn
    Protected Friend WithEvents DataGridTextBoxColumn4 As System.Windows.Forms.DataGridTextBoxColumn
    Protected Friend WithEvents DataGridTextBoxColumn5 As System.Windows.Forms.DataGridTextBoxColumn
    Protected Friend WithEvents DataGridTextBoxColumn6 As System.Windows.Forms.DataGridTextBoxColumn
    Protected Friend WithEvents DataGridTextBoxColumn7 As System.Windows.Forms.DataGridTextBoxColumn
    Protected Friend WithEvents StatusBar1 As System.Windows.Forms.StatusBar
    Protected Friend WithEvents DataGridTableStyle2 As System.Windows.Forms.DataGridTableStyle
    Protected Friend WithEvents DataGridTextBoxColumn8 As System.Windows.Forms.DataGridTextBoxColumn
    Protected Friend WithEvents DataGridTextBoxColumn9 As System.Windows.Forms.DataGridTextBoxColumn
    Protected Friend WithEvents DataGridTextBoxColumn10 As System.Windows.Forms.DataGridTextBoxColumn
    Protected Friend WithEvents MenuItem1 As System.Windows.Forms.MenuItem
    Protected Friend WithEvents MenuItem2 As System.Windows.Forms.MenuItem
    Protected Friend WithEvents MenuItem4 As System.Windows.Forms.MenuItem
    Protected Friend WithEvents miClear As System.Windows.Forms.MenuItem
    Protected Friend WithEvents miSearch As System.Windows.Forms.MenuItem
    Protected Friend WithEvents dsGetReg As InfoCtr.dsGetReg
    Protected Friend WithEvents miAddEvent As System.Windows.Forms.MenuItem
    Protected Friend WithEvents miAddRegistration As System.Windows.Forms.MenuItem
    Protected Friend WithEvents miCloseForm As System.Windows.Forms.MenuItem
    Protected Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Protected Friend WithEvents mmiGoto As System.Windows.Forms.MenuItem
    Protected Friend WithEvents miGotoEvent As System.Windows.Forms.MenuItem
    Protected Friend WithEvents miGotoRegistration As System.Windows.Forms.MenuItem
    Protected Friend WithEvents miGotoBreakout As System.Windows.Forms.MenuItem
    Protected Friend WithEvents miGotoExtra As System.Windows.Forms.MenuItem
    Protected Friend WithEvents mmiSearch As System.Windows.Forms.MenuItem
    Protected WithEvents cboType As System.Windows.Forms.ComboBox
    Protected WithEvents btnSearch As System.Windows.Forms.Button
    Protected WithEvents btnOpenEdEvent As System.Windows.Forms.Button
    Protected WithEvents grdEvent As System.Windows.Forms.DataGrid
    Protected WithEvents grdPeople As System.Windows.Forms.DataGrid
    Protected WithEvents cboRegion As System.Windows.Forms.ComboBox
    Protected WithEvents cboSearch As System.Windows.Forms.ComboBox
    Protected WithEvents txtSearch As System.Windows.Forms.TextBox
    Protected WithEvents Panel2 As System.Windows.Forms.Panel
    Protected WithEvents Label3 As System.Windows.Forms.Label
    Protected WithEvents Label2 As System.Windows.Forms.Label
    Protected WithEvents Label1 As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Protected Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.DataGridTableStyle1 = New System.Windows.Forms.DataGridTableStyle()
        Me.grdEvent = New System.Windows.Forms.DataGrid()
        Me.DataGridTextBoxColumn1 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn2 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn3 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn4 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn5 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn6 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn7 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.btnOpenEdEvent = New System.Windows.Forms.Button()
        Me.cboType = New System.Windows.Forms.ComboBox()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.StatusBar1 = New System.Windows.Forms.StatusBar()
        Me.MenuItem1 = New System.Windows.Forms.MenuItem()
        Me.MenuItem2 = New System.Windows.Forms.MenuItem()
        Me.miAddEvent = New System.Windows.Forms.MenuItem()
        Me.miAddRegistration = New System.Windows.Forms.MenuItem()
        Me.miCloseForm = New System.Windows.Forms.MenuItem()
        Me.mmiSearch = New System.Windows.Forms.MenuItem()
        Me.miSearch = New System.Windows.Forms.MenuItem()
        Me.miClear = New System.Windows.Forms.MenuItem()
        Me.mmiGoto = New System.Windows.Forms.MenuItem()
        Me.miGotoEvent = New System.Windows.Forms.MenuItem()
        Me.miGotoRegistration = New System.Windows.Forms.MenuItem()
        Me.miGotoBreakout = New System.Windows.Forms.MenuItem()
        Me.miGotoExtra = New System.Windows.Forms.MenuItem()
        Me.MenuItem4 = New System.Windows.Forms.MenuItem()
        Me.grdPeople = New System.Windows.Forms.DataGrid()
        Me.DataGridTableStyle2 = New System.Windows.Forms.DataGridTableStyle()
        Me.DataGridTextBoxColumn8 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn9 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn10 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cboSearch = New System.Windows.Forms.ComboBox()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.cboRegion = New System.Windows.Forms.ComboBox()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.OrgID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.btnGo = New System.Windows.Forms.Button()
        Me.DataSet1 = New System.Data.DataSet()
        Me.DataTable1 = New System.Data.DataTable()
        Me.DataColumn1 = New System.Data.DataColumn()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        CType(Me.grdEvent, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdPeople, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataSet1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataTable1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DataGridTableStyle1
        '
        Me.DataGridTableStyle1.DataGrid = Nothing
        Me.DataGridTableStyle1.HeaderForeColor = System.Drawing.SystemColors.ControlText
        '
        'grdEvent
        '
        Me.grdEvent.DataMember = ""
        Me.grdEvent.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.grdEvent.Location = New System.Drawing.Point(0, 0)
        Me.grdEvent.Name = "grdEvent"
        Me.grdEvent.Size = New System.Drawing.Size(130, 80)
        Me.grdEvent.TabIndex = 0
        '
        'DataGridTextBoxColumn1
        '
        Me.DataGridTextBoxColumn1.Format = ""
        Me.DataGridTextBoxColumn1.FormatInfo = Nothing
        Me.DataGridTextBoxColumn1.Width = -1
        '
        'DataGridTextBoxColumn2
        '
        Me.DataGridTextBoxColumn2.Format = ""
        Me.DataGridTextBoxColumn2.FormatInfo = Nothing
        Me.DataGridTextBoxColumn2.Width = -1
        '
        'DataGridTextBoxColumn3
        '
        Me.DataGridTextBoxColumn3.Format = ""
        Me.DataGridTextBoxColumn3.FormatInfo = Nothing
        Me.DataGridTextBoxColumn3.Width = -1
        '
        'DataGridTextBoxColumn4
        '
        Me.DataGridTextBoxColumn4.Format = ""
        Me.DataGridTextBoxColumn4.FormatInfo = Nothing
        Me.DataGridTextBoxColumn4.Width = -1
        '
        'DataGridTextBoxColumn5
        '
        Me.DataGridTextBoxColumn5.Format = ""
        Me.DataGridTextBoxColumn5.FormatInfo = Nothing
        Me.DataGridTextBoxColumn5.Width = -1
        '
        'DataGridTextBoxColumn6
        '
        Me.DataGridTextBoxColumn6.Format = ""
        Me.DataGridTextBoxColumn6.FormatInfo = Nothing
        Me.DataGridTextBoxColumn6.Width = -1
        '
        'DataGridTextBoxColumn7
        '
        Me.DataGridTextBoxColumn7.Format = ""
        Me.DataGridTextBoxColumn7.FormatInfo = Nothing
        Me.DataGridTextBoxColumn7.Width = -1
        '
        'btnOpenEdEvent
        '
        Me.btnOpenEdEvent.Location = New System.Drawing.Point(0, 0)
        Me.btnOpenEdEvent.Name = "btnOpenEdEvent"
        Me.btnOpenEdEvent.Size = New System.Drawing.Size(75, 23)
        Me.btnOpenEdEvent.TabIndex = 0
        '
        'cboType
        '
        Me.cboType.Location = New System.Drawing.Point(0, 0)
        Me.cboType.Name = "cboType"
        Me.cboType.Size = New System.Drawing.Size(121, 21)
        Me.cboType.TabIndex = 0
        '
        'btnSearch
        '
        Me.btnSearch.Location = New System.Drawing.Point(0, 0)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(75, 23)
        Me.btnSearch.TabIndex = 0
        '
        'StatusBar1
        '
        Me.StatusBar1.Location = New System.Drawing.Point(0, 0)
        Me.StatusBar1.Name = "StatusBar1"
        Me.StatusBar1.Size = New System.Drawing.Size(100, 22)
        Me.StatusBar1.TabIndex = 0
        '
        'MenuItem1
        '
        Me.MenuItem1.Index = -1
        Me.MenuItem1.Text = ""
        '
        'MenuItem2
        '
        Me.MenuItem2.Index = -1
        Me.MenuItem2.Text = ""
        '
        'miAddEvent
        '
        Me.miAddEvent.Index = -1
        Me.miAddEvent.Text = ""
        '
        'miAddRegistration
        '
        Me.miAddRegistration.Index = -1
        Me.miAddRegistration.Text = ""
        '
        'miCloseForm
        '
        Me.miCloseForm.Index = -1
        Me.miCloseForm.Text = ""
        '
        'mmiSearch
        '
        Me.mmiSearch.Index = -1
        Me.mmiSearch.Text = ""
        '
        'miSearch
        '
        Me.miSearch.Index = -1
        Me.miSearch.Text = ""
        '
        'miClear
        '
        Me.miClear.Index = -1
        Me.miClear.Text = ""
        '
        'mmiGoto
        '
        Me.mmiGoto.Index = -1
        Me.mmiGoto.Text = ""
        '
        'miGotoEvent
        '
        Me.miGotoEvent.Index = -1
        Me.miGotoEvent.Text = ""
        '
        'miGotoRegistration
        '
        Me.miGotoRegistration.Index = -1
        Me.miGotoRegistration.Text = ""
        '
        'miGotoBreakout
        '
        Me.miGotoBreakout.Index = -1
        Me.miGotoBreakout.Text = ""
        '
        'miGotoExtra
        '
        Me.miGotoExtra.Index = -1
        Me.miGotoExtra.Text = ""
        '
        'MenuItem4
        '
        Me.MenuItem4.Index = -1
        Me.MenuItem4.Text = ""
        '
        'grdPeople
        '
        Me.grdPeople.DataMember = ""
        Me.grdPeople.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.grdPeople.Location = New System.Drawing.Point(0, 0)
        Me.grdPeople.Name = "grdPeople"
        Me.grdPeople.Size = New System.Drawing.Size(130, 80)
        Me.grdPeople.TabIndex = 0
        '
        'DataGridTableStyle2
        '
        Me.DataGridTableStyle2.DataGrid = Nothing
        Me.DataGridTableStyle2.HeaderForeColor = System.Drawing.SystemColors.ControlText
        '
        'DataGridTextBoxColumn8
        '
        Me.DataGridTextBoxColumn8.Format = ""
        Me.DataGridTextBoxColumn8.FormatInfo = Nothing
        Me.DataGridTextBoxColumn8.Width = -1
        '
        'DataGridTextBoxColumn9
        '
        Me.DataGridTextBoxColumn9.Format = ""
        Me.DataGridTextBoxColumn9.FormatInfo = Nothing
        Me.DataGridTextBoxColumn9.Width = -1
        '
        'DataGridTextBoxColumn10
        '
        Me.DataGridTextBoxColumn10.Format = ""
        Me.DataGridTextBoxColumn10.FormatInfo = Nothing
        Me.DataGridTextBoxColumn10.Width = -1
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(0, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(100, 23)
        Me.Label2.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(0, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(100, 23)
        Me.Label1.TabIndex = 0
        '
        'cboSearch
        '
        Me.cboSearch.Location = New System.Drawing.Point(0, 0)
        Me.cboSearch.Name = "cboSearch"
        Me.cboSearch.Size = New System.Drawing.Size(121, 21)
        Me.cboSearch.TabIndex = 0
        '
        'txtSearch
        '
        Me.txtSearch.Location = New System.Drawing.Point(0, 0)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(100, 20)
        Me.txtSearch.TabIndex = 0
        '
        'cboRegion
        '
        Me.cboRegion.Location = New System.Drawing.Point(0, 0)
        Me.cboRegion.Name = "cboRegion"
        Me.cboRegion.Size = New System.Drawing.Size(121, 21)
        Me.cboRegion.TabIndex = 0
        '
        'Panel2
        '
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(200, 100)
        Me.Panel2.TabIndex = 0
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(0, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(100, 23)
        Me.Label3.TabIndex = 0
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToResizeRows = False
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.OrgID})
        Me.DataGridView1.Location = New System.Drawing.Point(33, 80)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.Size = New System.Drawing.Size(246, 254)
        Me.DataGridView1.TabIndex = 0
        '
        'OrgID
        '
        Me.OrgID.HeaderText = "OrgID"
        Me.OrgID.Name = "OrgID"
        Me.OrgID.Width = 200
        '
        'btnGo
        '
        Me.btnGo.Location = New System.Drawing.Point(285, 80)
        Me.btnGo.Name = "btnGo"
        Me.btnGo.Size = New System.Drawing.Size(151, 24)
        Me.btnGo.TabIndex = 1
        Me.btnGo.Text = "Generate Report"
        Me.btnGo.UseVisualStyleBackColor = True
        '
        'DataSet1
        '
        Me.DataSet1.DataSetName = "NewDataSet"
        Me.DataSet1.Tables.AddRange(New System.Data.DataTable() {Me.DataTable1})
        '
        'DataTable1
        '
        Me.DataTable1.Columns.AddRange(New System.Data.DataColumn() {Me.DataColumn1})
        Me.DataTable1.TableName = "Table1"
        '
        'DataColumn1
        '
        Me.DataColumn1.ColumnName = "IDFld"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.Desktop
        Me.Label4.Location = New System.Drawing.Point(37, 11)
        Me.Label4.MaximumSize = New System.Drawing.Size(400, 0)
        Me.Label4.MinimumSize = New System.Drawing.Size(0, 35)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(390, 35)
        Me.Label4.TabIndex = 2
        Me.Label4.Text = "Enter OrgID numbers, either in separate rows, or in one cell separated by a comma" & _
    "."
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.Desktop
        Me.Label5.Location = New System.Drawing.Point(289, 144)
        Me.Label5.MaximumSize = New System.Drawing.Size(175, 0)
        Me.Label5.MinimumSize = New System.Drawing.Size(0, 35)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(175, 75)
        Me.Label5.TabIndex = 3
        Me.Label5.Text = "Generate Report button will open 1) Congr Report in Explorer.  Export to .pdf; 2)" & _
    " Cover letter addressed to congr; (3) envelopes or labels.)"
        '
        'frmReviewOrgChild
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(476, 355)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.btnGo)
        Me.Controls.Add(Me.DataGridView1)
        Me.Name = "frmReviewOrgChild"
        CType(Me.grdEvent, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdPeople, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataSet1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataTable1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region





    Private Sub btnGo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGo.Click
        Dim strb As New StringBuilder
        For Each row As DataGridViewRow In Me.DataGridView1.Rows
            '  If row.Selected = True Then
            strb.Append(Me.DataGridView1.Item(0, row.Index).Value)
            'End If
            strb.Append(",")
        Next row
        modPopup.OpenStaffLaityRpt(strb.ToString.Substring(0, Len(strb.ToString) - 2), "")

BOpenWordCoverLetter:
        'CREATE DATAFILE
        ' StrmWriter("[MailStaffLaity]", "@IDArray", strb.ToString.Substring(0, Len(strb.ToString) - 2), "C:\DataDoc.csv", "csv", False)
        'OPEN LETTER
        ' MergePerform("\\iccnas\shared\standard operating procedures\mergeinfoctrtoWord\StaffLaityIntro.docx", "C:\datadoc", ".csv")

        'OPEN ENVELOPE
    End Sub


End Class
