<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class rptViewerResource
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim ReportDataSource1 As Microsoft.Reporting.WinForms.ReportDataSource = New Microsoft.Reporting.WinForms.ReportDataSource
        Me.tblResourceQtrBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.DsResourceQtr = New InfoCtr.dsResourceQtr
        Me.BindingSource1 = New System.Windows.Forms.BindingSource(Me.components)
        Me.TblResourceQtrTableAdapter = New InfoCtr.dsResourceQtrTableAdapters.tblResourceQtrTableAdapter
        Me.ReportViewer1 = New Microsoft.Reporting.WinForms.ReportViewer
        CType(Me.tblResourceQtrBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsResourceQtr, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BindingSource1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'tblResourceQtrBindingSource
        '
        Me.tblResourceQtrBindingSource.DataMember = "tblResourceQtr"
        Me.tblResourceQtrBindingSource.DataSource = Me.DsResourceQtr
        '
        'DsResourceQtr
        '
        Me.DsResourceQtr.DataSetName = "dsResourceQtr"
        Me.DsResourceQtr.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'BindingSource1
        '
        Me.BindingSource1.DataMember = "tblResourceQtr"
        Me.BindingSource1.DataSource = Me.DsResourceQtr
        '
        'TblResourceQtrTableAdapter
        '
        Me.TblResourceQtrTableAdapter.ClearBeforeFill = True
        '
        'ReportViewer1
        '
        Me.ReportViewer1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        ReportDataSource1.Name = "dsResourceQtr_tblResourceQtr"
        ReportDataSource1.Value = Me.tblResourceQtrBindingSource
        Me.ReportViewer1.LocalReport.DataSources.Add(ReportDataSource1)
        Me.ReportViewer1.LocalReport.ReportEmbeddedResource = "InfoCtr.rptRecommendQtr.rdlc"
        Me.ReportViewer1.Location = New System.Drawing.Point(12, 32)
        Me.ReportViewer1.Name = "ReportViewer1"
        Me.ReportViewer1.Size = New System.Drawing.Size(482, 433)
        Me.ReportViewer1.TabIndex = 0
        '
        'rptViewerResource
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(506, 477)
        Me.Controls.Add(Me.ReportViewer1)
        Me.Name = "rptViewerResource"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "rptViewerResource"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.tblResourceQtrBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsResourceQtr, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BindingSource1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents BindingSource1 As System.Windows.Forms.BindingSource
    Friend WithEvents DsResourceQtr As InfoCtr.dsResourceQtr
    Friend WithEvents TblResourceQtrTableAdapter As InfoCtr.dsResourceQtrTableAdapters.tblResourceQtrTableAdapter
    Friend WithEvents ReportViewer1 As Microsoft.Reporting.WinForms.ReportViewer
    Friend WithEvents tblResourceQtrBindingSource As System.Windows.Forms.BindingSource
End Class
