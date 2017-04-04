<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class rptViewerRegistration
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
        Me.components = New System.ComponentModel.Container()
        Dim ReportDataSource1 As Microsoft.Reporting.WinForms.ReportDataSource = New Microsoft.Reporting.WinForms.ReportDataSource()
        Me.RptRegistrationsBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.dsrptRegistration = New InfoCtr.dsrptRegistration()
        Me.vwrChecklist = New Microsoft.Reporting.WinForms.ReportViewer()
        Me.cboSelect = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.RptRegistrationsTableAdapter = New InfoCtr.dsrptRegistrationTableAdapters.RptRegistrationsTableAdapter()
        CType(Me.RptRegistrationsBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dsrptRegistration, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RptRegistrationsBindingSource
        '
        Me.RptRegistrationsBindingSource.DataMember = "RptRegistrations"
        Me.RptRegistrationsBindingSource.DataSource = Me.dsrptRegistration
        '
        'dsrptRegistration
        '
        Me.dsrptRegistration.DataSetName = "dsrptRegistration"
        Me.dsrptRegistration.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'vwrChecklist
        '
        Me.vwrChecklist.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        ReportDataSource1.Name = "dsrptRegistration_RptRegistrations"
        ReportDataSource1.Value = Me.RptRegistrationsBindingSource
        Me.vwrChecklist.LocalReport.DataSources.Add(ReportDataSource1)
        Me.vwrChecklist.LocalReport.ReportEmbeddedResource = "InfoCtr.rptRegistrationChecklist.rdlc"
        Me.vwrChecklist.Location = New System.Drawing.Point(0, 63)
        Me.vwrChecklist.Name = "vwrChecklist"
        Me.vwrChecklist.Size = New System.Drawing.Size(872, 447)
        Me.vwrChecklist.TabIndex = 0
        '
        'cboSelect
        '
        Me.cboSelect.FormattingEnabled = True
        Me.cboSelect.Items.AddRange(New Object() {"Check-In List", "Overview", "Roster Handout", "Cancellation Phone List"})
        Me.cboSelect.Location = New System.Drawing.Point(135, 24)
        Me.cboSelect.Name = "cboSelect"
        Me.cboSelect.Size = New System.Drawing.Size(232, 21)
        Me.cboSelect.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.Highlight
        Me.Label1.Location = New System.Drawing.Point(44, 27)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(85, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Select Report"
        '
        'RptRegistrationsTableAdapter
        '
        Me.RptRegistrationsTableAdapter.ClearBeforeFill = True
        '
        'rptViewerRegistration
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(872, 520)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cboSelect)
        Me.Controls.Add(Me.vwrChecklist)
        Me.Name = "rptViewerRegistration"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "rptViewerRegistration"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.RptRegistrationsBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dsrptRegistration, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents vwrChecklist As Microsoft.Reporting.WinForms.ReportViewer
    Friend WithEvents RptRegistrationsBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents dsrptRegistration As InfoCtr.dsrptRegistration
    Friend WithEvents RptRegistrationsTableAdapter As InfoCtr.dsrptRegistrationTableAdapters.RptRegistrationsTableAdapter
    Friend WithEvents cboSelect As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
End Class
