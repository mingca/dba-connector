<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class rptViewerStatsGrant
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
        Me.vwrGrant = New Microsoft.Reporting.WinForms.ReportViewer
        Me.dsStatsGrant = New InfoCtr.dsStatsGrant
        Me.StatsGrantsBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.StatsGrantsTableAdapter = New InfoCtr.dsStatsGrantTableAdapters.StatsGrantsTableAdapter
        CType(Me.dsStatsGrant, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.StatsGrantsBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'vwrGrant
        '
        Me.vwrGrant.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        ReportDataSource1.Name = "dsStatsGrant_StatsGrants"
        ReportDataSource1.Value = Me.StatsGrantsBindingSource
        Me.vwrGrant.LocalReport.DataSources.Add(ReportDataSource1)
        Me.vwrGrant.LocalReport.ReportEmbeddedResource = "InfoCtr.rptStatsGrant.rdlc"
        Me.vwrGrant.Location = New System.Drawing.Point(44, 53)
        Me.vwrGrant.Name = "vwrGrant"
        Me.vwrGrant.Size = New System.Drawing.Size(476, 292)
        Me.vwrGrant.TabIndex = 0
        '
        'dsStatsGrant
        '
        Me.dsStatsGrant.DataSetName = "dsStatsGrant"
        Me.dsStatsGrant.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'StatsGrantsBindingSource
        '
        Me.StatsGrantsBindingSource.DataMember = "StatsGrants"
        Me.StatsGrantsBindingSource.DataSource = Me.dsStatsGrant
        '
        'StatsGrantsTableAdapter
        '
        Me.StatsGrantsTableAdapter.ClearBeforeFill = True
        '
        'rptViewerStatsGrant
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(545, 364)
        Me.Controls.Add(Me.vwrGrant)
        Me.Name = "rptViewerStatsGrant"
        Me.Text = "Grant Statistics"
        CType(Me.dsStatsGrant, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.StatsGrantsBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents vwrGrant As Microsoft.Reporting.WinForms.ReportViewer
    Friend WithEvents StatsGrantsBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents dsStatsGrant As InfoCtr.dsStatsGrant
    Friend WithEvents StatsGrantsTableAdapter As InfoCtr.dsStatsGrantTableAdapters.StatsGrantsTableAdapter
End Class
