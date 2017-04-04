<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmReportViewer
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim ReportDataSource1 As Microsoft.Reporting.WinForms.ReportDataSource = New Microsoft.Reporting.WinForms.ReportDataSource()
        Me.ReportsDrupalBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.dsRptsReg = New InfoCtr.dsRptsReg()
        Me.ReportViewer1 = New Microsoft.Reporting.WinForms.ReportViewer()
        Me.ReportsDrupalTableAdapter = New InfoCtr.dsRptsRegTableAdapters.ReportsDrupalTableAdapter()
        CType(Me.ReportsDrupalBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dsRptsReg, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ReportsDrupalBindingSource
        '
        Me.ReportsDrupalBindingSource.DataMember = "ReportsDrupal"
        Me.ReportsDrupalBindingSource.DataSource = Me.dsRptsReg
        '
        'dsRptsReg
        '
        Me.dsRptsReg.DataSetName = "dsRptsReg"
        Me.dsRptsReg.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'ReportViewer1
        '
        Me.ReportViewer1.Dock = System.Windows.Forms.DockStyle.Fill
        ReportDataSource1.Name = "DataSet1"
        ReportDataSource1.Value = Me.ReportsDrupalBindingSource
        Me.ReportViewer1.LocalReport.DataSources.Add(ReportDataSource1)
        Me.ReportViewer1.LocalReport.ReportEmbeddedResource = "InfoCtr.rptRegRefund.rdlc"
        Me.ReportViewer1.Location = New System.Drawing.Point(0, 0)
        Me.ReportViewer1.Name = "ReportViewer1"
        Me.ReportViewer1.Size = New System.Drawing.Size(831, 412)
        Me.ReportViewer1.TabIndex = 0
        '
        'ReportsDrupalTableAdapter
        '
        Me.ReportsDrupalTableAdapter.ClearBeforeFill = True
        '
        'frmReportViewer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(831, 412)
        Me.Controls.Add(Me.ReportViewer1)
        Me.Name = "frmReportViewer"
        Me.Text = "frmReportViewer"
        CType(Me.ReportsDrupalBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dsRptsReg, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ReportViewer1 As Microsoft.Reporting.WinForms.ReportViewer
    Friend WithEvents ReportsDrupalBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents dsRptsReg As InfoCtr.dsRptsReg
    Friend WithEvents ReportsDrupalTableAdapter As InfoCtr.dsRptsRegTableAdapters.ReportsDrupalTableAdapter
End Class
