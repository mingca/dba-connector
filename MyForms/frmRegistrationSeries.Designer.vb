<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRegistrationSeries
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmRegistrationSeries))
        Me.SqlSelectCommand1 = New System.Data.SqlClient.SqlCommand
        Me.SqlDataAdapter1 = New System.Data.SqlClient.SqlDataAdapter
        Me.SqlInsertCommand = New System.Data.SqlClient.SqlCommand
        Me.DsEventSeries1 = New InfoCtr.dsEventSeries
        Me.ListBox1 = New System.Windows.Forms.ListBox
        Me.Button1 = New System.Windows.Forms.Button
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.btnSaveExit = New System.Windows.Forms.Button
        Me.btnHelp = New System.Windows.Forms.Button
        CType(Me.DsEventSeries1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel3.SuspendLayout()
        Me.SuspendLayout()
        '
        'SqlSelectCommand1
        '
        Me.SqlSelectCommand1.CommandText = "dbo.GetWEventSeries"
        Me.SqlSelectCommand1.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlSelectCommand1.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, False, CType(0, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing), New System.Data.SqlClient.SqlParameter("@CaseName", System.Data.SqlDbType.VarChar, 30), New System.Data.SqlClient.SqlParameter("@ContactNum", System.Data.SqlDbType.Int, 4)})
        '
        'SqlDataAdapter1
        '
        Me.SqlDataAdapter1.InsertCommand = Me.SqlInsertCommand
        Me.SqlDataAdapter1.SelectCommand = Me.SqlSelectCommand1
        Me.SqlDataAdapter1.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "GetEventSeries", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("EventID", "EventID"), New System.Data.Common.DataColumnMapping("Registered", "Registered")})})
        '
        'SqlInsertCommand
        '
        Me.SqlInsertCommand.CommandText = "dbo.InsertRegistrationSeries"
        Me.SqlInsertCommand.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlInsertCommand.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, False, CType(0, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing), New System.Data.SqlClient.SqlParameter("@EventID", System.Data.SqlDbType.Int, 4, "EventID"), New System.Data.SqlClient.SqlParameter("@ContactID", System.Data.SqlDbType.Int, 4), New System.Data.SqlClient.SqlParameter("@RegDate", System.Data.SqlDbType.DateTime, 8), New System.Data.SqlClient.SqlParameter("@Fee", System.Data.SqlDbType.VarChar, 20), New System.Data.SqlClient.SqlParameter("@PayDate", System.Data.SqlDbType.DateTime, 8), New System.Data.SqlClient.SqlParameter("@Clergy", System.Data.SqlDbType.VarChar, 5), New System.Data.SqlClient.SqlParameter("@Gender", System.Data.SqlDbType.VarChar, 5), New System.Data.SqlClient.SqlParameter("@PayMethod", System.Data.SqlDbType.VarChar, 20), New System.Data.SqlClient.SqlParameter("@Multiple", System.Data.SqlDbType.Bit, 1), New System.Data.SqlClient.SqlParameter("@Group", System.Data.SqlDbType.Bit, 1), New System.Data.SqlClient.SqlParameter("@Heard", System.Data.SqlDbType.VarChar, 20), New System.Data.SqlClient.SqlParameter("@Registered", System.Data.SqlDbType.VarChar, 20, "Registered"), New System.Data.SqlClient.SqlParameter("@Veg", System.Data.SqlDbType.Bit, 1), New System.Data.SqlClient.SqlParameter("@Notes", System.Data.SqlDbType.VarChar, 100)})
        '
        'DsEventSeries1
        '
        Me.DsEventSeries1.DataSetName = "dsEventSeries"
        Me.DsEventSeries1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'ListBox1
        '
        Me.ListBox1.DataSource = Me.DsEventSeries1
        Me.ListBox1.DisplayMember = "GetEventSeries.Registered"
        Me.ListBox1.FormattingEnabled = True
        Me.ListBox1.Location = New System.Drawing.Point(31, 22)
        Me.ListBox1.Name = "ListBox1"
        Me.ListBox1.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.ListBox1.Size = New System.Drawing.Size(291, 186)
        Me.ListBox1.TabIndex = 3
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(362, 84)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(69, 26)
        Me.Button1.TabIndex = 4
        Me.Button1.Text = "Button1"
        Me.Button1.UseVisualStyleBackColor = True
        Me.Button1.Visible = False
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.SystemColors.Highlight
        Me.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel3.Controls.Add(Me.btnSaveExit)
        Me.Panel3.Location = New System.Drawing.Point(384, 12)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(47, 40)
        Me.Panel3.TabIndex = 783
        '
        'btnSaveExit
        '
        Me.btnSaveExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSaveExit.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.btnSaveExit.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSaveExit.Image = CType(resources.GetObject("btnSaveExit.Image"), System.Drawing.Image)
        Me.btnSaveExit.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSaveExit.Location = New System.Drawing.Point(3, 1)
        Me.btnSaveExit.Name = "btnSaveExit"
        Me.btnSaveExit.Size = New System.Drawing.Size(40, 35)
        Me.btnSaveExit.TabIndex = 418
        Me.btnSaveExit.Text = "Close"
        Me.btnSaveExit.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSaveExit.UseVisualStyleBackColor = False
        '
        'btnHelp
        '
        Me.btnHelp.Image = CType(resources.GetObject("btnHelp.Image"), System.Drawing.Image)
        Me.btnHelp.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnHelp.Location = New System.Drawing.Point(437, 12)
        Me.btnHelp.Name = "btnHelp"
        Me.btnHelp.Size = New System.Drawing.Size(25, 25)
        Me.btnHelp.TabIndex = 782
        Me.btnHelp.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnHelp.UseVisualStyleBackColor = True
        '
        'frmRegistrationSeries
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(473, 266)
        Me.ControlBox = False
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.btnHelp)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.ListBox1)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmRegistrationSeries"
        Me.Text = "frmRegistrationSeries"
        CType(Me.DsEventSeries1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel3.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SqlSelectCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlDataAdapter1 As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents DsEventSeries1 As InfoCtr.dsEventSeries
    Friend WithEvents ListBox1 As System.Windows.Forms.ListBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents btnSaveExit As System.Windows.Forms.Button
    Friend WithEvents btnHelp As System.Windows.Forms.Button
    Friend WithEvents SqlInsertCommand As System.Data.SqlClient.SqlCommand
End Class
