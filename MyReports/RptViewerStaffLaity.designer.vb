<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class RptViewerStaffLaity
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
        Dim ReportDataSource2 As Microsoft.Reporting.WinForms.ReportDataSource = New Microsoft.Reporting.WinForms.ReportDataSource
        Me.ReportViewer1 = New Microsoft.Reporting.WinForms.ReportViewer
        Me.dsStaffLaity = New InfoCtr.dsStaffLaity
        Me.tbStaffLaityBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.taStaffLaity = New InfoCtr.dsStaffLaityTableAdapters.taStaffLaity
        CType(Me.dsStaffLaity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbStaffLaityBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ReportViewer1
        '
        Me.ReportViewer1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        ReportDataSource2.Name = "dsStaffLaity_tbStaffLaity"
        ReportDataSource2.Value = Me.tbStaffLaityBindingSource
        Me.ReportViewer1.LocalReport.DataSources.Add(ReportDataSource2)
        Me.ReportViewer1.LocalReport.ReportEmbeddedResource = "InfoCtr.rptOrgStaffLaity.rdlc"
        Me.ReportViewer1.Location = New System.Drawing.Point(1, 4)
        Me.ReportViewer1.Name = "ReportViewer1"
        Me.ReportViewer1.Size = New System.Drawing.Size(906, 353)
        Me.ReportViewer1.TabIndex = 0
        '
        'dsStaffLaity
        '
        Me.dsStaffLaity.DataSetName = "dsStaffLaity"
        Me.dsStaffLaity.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'tbStaffLaityBindingSource
        '
        Me.tbStaffLaityBindingSource.DataMember = "tbStaffLaity"
        Me.tbStaffLaityBindingSource.DataSource = Me.dsStaffLaity
        '
        'taStaffLaity
        '
        Me.taStaffLaity.ClearBeforeFill = True
        '
        'RptViewerStaffLaity
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(919, 354)
        Me.Controls.Add(Me.ReportViewer1)
        Me.Name = "RptViewerStaffLaity"
        Me.Text = "Staff & Laity"
        CType(Me.dsStaffLaity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbStaffLaityBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ReportViewer1 As Microsoft.Reporting.WinForms.ReportViewer
    Friend WithEvents tbStaffLaityBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents dsStaffLaity As InfoCtr.dsStaffLaity
    Friend WithEvents taStaffLaity As InfoCtr.dsStaffLaityTableAdapters.taStaffLaity
End Class
