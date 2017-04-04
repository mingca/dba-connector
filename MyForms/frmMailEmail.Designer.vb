<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMailEmail
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.daSrch = New System.Data.SqlClient.SqlDataAdapter()
        Me.SqlSelectCommand = New System.Data.SqlClient.SqlCommand()
        Me.btnSend = New System.Windows.Forms.Button()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.btnExclude = New System.Windows.Forms.Button()
        Me.btnHelp = New System.Windows.Forms.Button()
        Me.StatusBar1 = New System.Windows.Forms.StatusBar()
        Me.StatusBarPanel1 = New System.Windows.Forms.StatusBarPanel()
        Me.StatusBarPanel3 = New System.Windows.Forms.StatusBarPanel()
        Me.StatusBarPanel2 = New System.Windows.Forms.StatusBarPanel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblResultCount = New System.Windows.Forms.Label()
        Me.rtbBody = New System.Windows.Forms.RichTextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtSubject = New System.Windows.Forms.TextBox()
        Me.grdvwMain = New System.Windows.Forms.DataGridView()
        Me.colEmail = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colLastName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colFirstName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colOrgName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column6 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column7 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column8 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colContactID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colOrgNum = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.BindingSource1 = New System.Windows.Forms.BindingSource(Me.components)
        Me.btnShowReject = New System.Windows.Forms.Button()
        Me.HelpProvider1 = New System.Windows.Forms.HelpProvider()
        CType(Me.StatusBarPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.StatusBarPanel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.StatusBarPanel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdvwMain, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BindingSource1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'daSrch
        '
        Me.daSrch.SelectCommand = Me.SqlSelectCommand
        Me.daSrch.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "EmailZipcodes", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("ZipCode", "ZipCode"), New System.Data.Common.DataColumnMapping("Email", "Email")})})
        '
        'SqlSelectCommand
        '
        Me.SqlSelectCommand.CommandText = "SELECT * FROM  vwMailEmails"
        '
        'btnSend
        '
        Me.btnSend.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSend.ForeColor = System.Drawing.Color.Maroon
        Me.btnSend.Location = New System.Drawing.Point(1062, 19)
        Me.btnSend.Name = "btnSend"
        Me.btnSend.Size = New System.Drawing.Size(125, 25)
        Me.btnSend.TabIndex = 30
        Me.btnSend.Text = "3) Send Email"
        Me.btnSend.UseVisualStyleBackColor = True
        '
        'btnExclude
        '
        Me.HelpProvider1.SetHelpString(Me.btnExclude, "")
        Me.btnExclude.Location = New System.Drawing.Point(525, 55)
        Me.btnExclude.Name = "btnExclude"
        Me.HelpProvider1.SetShowHelp(Me.btnExclude, True)
        Me.btnExclude.Size = New System.Drawing.Size(86, 21)
        Me.btnExclude.TabIndex = 222
        Me.btnExclude.TabStop = False
        Me.btnExclude.Text = "Exclude"
        Me.ToolTip1.SetToolTip(Me.btnExclude, "select a row and click Exclude to remove email from send list.  These can be view" & _
        "ed via the Show Excluded button under the grid.")
        Me.btnExclude.UseVisualStyleBackColor = True
        '
        'btnHelp
        '
        Me.btnHelp.Image = Global.InfoCtr.My.Resources.Resources.btnHelp
        Me.btnHelp.Location = New System.Drawing.Point(1251, 15)
        Me.btnHelp.Name = "btnHelp"
        Me.btnHelp.Size = New System.Drawing.Size(30, 32)
        Me.btnHelp.TabIndex = 230
        Me.btnHelp.Tag = "ctlNeutral"
        Me.btnHelp.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ToolTip1.SetToolTip(Me.btnHelp, "Help")
        Me.btnHelp.UseVisualStyleBackColor = True
        '
        'StatusBar1
        '
        Me.StatusBar1.Location = New System.Drawing.Point(0, 595)
        Me.StatusBar1.Name = "StatusBar1"
        Me.StatusBar1.Panels.AddRange(New System.Windows.Forms.StatusBarPanel() {Me.StatusBarPanel1, Me.StatusBarPanel3, Me.StatusBarPanel2})
        Me.StatusBar1.ShowPanels = True
        Me.StatusBar1.Size = New System.Drawing.Size(1330, 22)
        Me.StatusBar1.TabIndex = 211
        '
        'StatusBarPanel1
        '
        Me.StatusBarPanel1.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents
        Me.StatusBarPanel1.MinWidth = 200
        Me.StatusBarPanel1.Name = "StatusBarPanel1"
        Me.StatusBarPanel1.Text = "Ready"
        Me.StatusBarPanel1.Width = 200
        '
        'StatusBarPanel3
        '
        Me.StatusBarPanel3.Alignment = System.Windows.Forms.HorizontalAlignment.Center
        Me.StatusBarPanel3.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents
        Me.StatusBarPanel3.Name = "StatusBarPanel3"
        Me.StatusBarPanel3.Width = 10
        '
        'StatusBarPanel2
        '
        Me.StatusBarPanel2.Alignment = System.Windows.Forms.HorizontalAlignment.Right
        Me.StatusBarPanel2.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring
        Me.StatusBarPanel2.BorderStyle = System.Windows.Forms.StatusBarPanelBorderStyle.None
        Me.StatusBarPanel2.Name = "StatusBarPanel2"
        Me.StatusBarPanel2.Text = "Use this window generate an address list to copy to excel for mailing purposes."
        Me.StatusBarPanel2.Width = 1103
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Maroon
        Me.Label2.Location = New System.Drawing.Point(12, 40)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(298, 13)
        Me.Label2.TabIndex = 214
        Me.Label2.Text = "1) Confirm email should go to all these listed below."
        '
        'lblResultCount
        '
        Me.lblResultCount.AutoSize = True
        Me.lblResultCount.BackColor = System.Drawing.SystemColors.HighlightText
        Me.lblResultCount.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblResultCount.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblResultCount.Location = New System.Drawing.Point(30, 59)
        Me.lblResultCount.MinimumSize = New System.Drawing.Size(100, 20)
        Me.lblResultCount.Name = "lblResultCount"
        Me.lblResultCount.Size = New System.Drawing.Size(100, 20)
        Me.lblResultCount.TabIndex = 216
        Me.lblResultCount.Text = "Results:"
        '
        'rtbBody
        '
        Me.rtbBody.Location = New System.Drawing.Point(662, 82)
        Me.rtbBody.Name = "rtbBody"
        Me.rtbBody.Size = New System.Drawing.Size(619, 445)
        Me.rtbBody.TabIndex = 20
        Me.rtbBody.Text = "type body of message here"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Maroon
        Me.Label3.Location = New System.Drawing.Point(659, 25)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(252, 13)
        Me.Label3.TabIndex = 218
        Me.Label3.Text = "2. Enter Subject line and Body of message."
        '
        'txtSubject
        '
        Me.txtSubject.Location = New System.Drawing.Point(662, 56)
        Me.txtSubject.Name = "txtSubject"
        Me.txtSubject.Size = New System.Drawing.Size(335, 20)
        Me.txtSubject.TabIndex = 10
        Me.txtSubject.Text = "type Subject line here"
        '
        'grdvwMain
        '
        Me.grdvwMain.AllowUserToDeleteRows = False
        Me.grdvwMain.AllowUserToOrderColumns = True
        Me.grdvwMain.AutoGenerateColumns = False
        Me.grdvwMain.BackgroundColor = System.Drawing.Color.LightGray
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdvwMain.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.grdvwMain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdvwMain.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colEmail, Me.colLastName, Me.colFirstName, Me.Column1, Me.Column2, Me.colOrgName, Me.Column3, Me.Column4, Me.Column5, Me.Column6, Me.Column7, Me.Column8, Me.colContactID, Me.colOrgNum})
        Me.grdvwMain.DataSource = Me.BindingSource1
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.grdvwMain.DefaultCellStyle = DataGridViewCellStyle2
        Me.grdvwMain.Location = New System.Drawing.Point(12, 82)
        Me.grdvwMain.Name = "grdvwMain"
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdvwMain.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.grdvwMain.RowHeadersWidth = 20
        Me.grdvwMain.Size = New System.Drawing.Size(634, 460)
        Me.grdvwMain.TabIndex = 5
        '
        'colEmail
        '
        Me.colEmail.DataPropertyName = "Email"
        Me.colEmail.HeaderText = "Email"
        Me.colEmail.Name = "colEmail"
        Me.colEmail.Width = 150
        '
        'colLastName
        '
        Me.colLastName.DataPropertyName = "LastName"
        Me.colLastName.HeaderText = "LastName"
        Me.colLastName.Name = "colLastName"
        Me.colLastName.ReadOnly = True
        '
        'colFirstName
        '
        Me.colFirstName.DataPropertyName = "FirstName"
        Me.colFirstName.HeaderText = "FirstName"
        Me.colFirstName.Name = "colFirstName"
        Me.colFirstName.ReadOnly = True
        '
        'Column1
        '
        Me.Column1.DataPropertyName = "StaffType"
        Me.Column1.HeaderText = "StaffType"
        Me.Column1.Name = "Column1"
        Me.Column1.ReadOnly = True
        '
        'Column2
        '
        Me.Column2.DataPropertyName = "JobTitle"
        Me.Column2.HeaderText = "JobTitle"
        Me.Column2.Name = "Column2"
        Me.Column2.ReadOnly = True
        '
        'colOrgName
        '
        Me.colOrgName.DataPropertyName = "OrgName"
        Me.colOrgName.HeaderText = "OrgName"
        Me.colOrgName.Name = "colOrgName"
        Me.colOrgName.ReadOnly = True
        Me.colOrgName.Width = 150
        '
        'Column3
        '
        Me.Column3.DataPropertyName = "OrgType"
        Me.Column3.HeaderText = "OrgType"
        Me.Column3.Name = "Column3"
        Me.Column3.ReadOnly = True
        '
        'Column4
        '
        Me.Column4.DataPropertyName = "County"
        Me.Column4.HeaderText = "County"
        Me.Column4.Name = "Column4"
        Me.Column4.ReadOnly = True
        Me.Column4.Width = 75
        '
        'Column5
        '
        Me.Column5.DataPropertyName = "SatelliteRegion"
        Me.Column5.HeaderText = "SatelliteRegion"
        Me.Column5.Name = "Column5"
        Me.Column5.ReadOnly = True
        Me.Column5.Width = 75
        '
        'Column6
        '
        Me.Column6.DataPropertyName = "#Attendance"
        Me.Column6.HeaderText = "#Attendance"
        Me.Column6.Name = "Column6"
        Me.Column6.ReadOnly = True
        Me.Column6.Width = 40
        '
        'Column7
        '
        Me.Column7.DataPropertyName = "#Members"
        Me.Column7.HeaderText = "#Members"
        Me.Column7.Name = "Column7"
        Me.Column7.ReadOnly = True
        Me.Column7.Width = 40
        '
        'Column8
        '
        Me.Column8.DataPropertyName = "#Cases"
        Me.Column8.HeaderText = "#Cases"
        Me.Column8.Name = "Column8"
        Me.Column8.ReadOnly = True
        Me.Column8.Width = 40
        '
        'colContactID
        '
        Me.colContactID.DataPropertyName = "ContactID"
        Me.colContactID.HeaderText = "ContactNum"
        Me.colContactID.Name = "colContactID"
        Me.colContactID.ReadOnly = True
        Me.colContactID.Width = 50
        '
        'colOrgNum
        '
        Me.colOrgNum.DataPropertyName = "OrgNum"
        Me.colOrgNum.HeaderText = "OrgNum"
        Me.colOrgNum.Name = "colOrgNum"
        Me.colOrgNum.ReadOnly = True
        Me.colOrgNum.Width = 50
        '
        'btnShowReject
        '
        Me.btnShowReject.Location = New System.Drawing.Point(128, 549)
        Me.btnShowReject.Name = "btnShowReject"
        Me.btnShowReject.Size = New System.Drawing.Size(117, 23)
        Me.btnShowReject.TabIndex = 50
        Me.btnShowReject.TabStop = False
        Me.btnShowReject.Text = "Show Excluded"
        Me.btnShowReject.UseVisualStyleBackColor = True
        '
        'frmMailEmail
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1330, 617)
        Me.Controls.Add(Me.btnHelp)
        Me.Controls.Add(Me.btnShowReject)
        Me.Controls.Add(Me.btnExclude)
        Me.Controls.Add(Me.grdvwMain)
        Me.Controls.Add(Me.txtSubject)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.rtbBody)
        Me.Controls.Add(Me.lblResultCount)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.StatusBar1)
        Me.Controls.Add(Me.btnSend)
        Me.Name = "frmMailEmail"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Send Emails"
        CType(Me.StatusBarPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.StatusBarPanel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.StatusBarPanel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdvwMain, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BindingSource1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnSend As System.Windows.Forms.Button
    Friend WithEvents daSrch As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents SqlSelectCommand As System.Data.SqlClient.SqlCommand
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents StatusBar1 As System.Windows.Forms.StatusBar
    Friend WithEvents StatusBarPanel1 As System.Windows.Forms.StatusBarPanel
    Friend WithEvents StatusBarPanel2 As System.Windows.Forms.StatusBarPanel
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lblResultCount As System.Windows.Forms.Label
    Friend WithEvents rtbBody As System.Windows.Forms.RichTextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtSubject As System.Windows.Forms.TextBox
    Friend WithEvents grdvwMain As System.Windows.Forms.DataGridView
    Friend WithEvents btnExclude As System.Windows.Forms.Button
    Friend WithEvents btnShowReject As System.Windows.Forms.Button
    Friend WithEvents BindingSource1 As System.Windows.Forms.BindingSource
    Friend WithEvents btnHelp As System.Windows.Forms.Button
    Friend WithEvents StatusBarPanel3 As System.Windows.Forms.StatusBarPanel
    Friend WithEvents colEmail As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colLastName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colFirstName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colOrgName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column6 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column7 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column8 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colContactID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colOrgNum As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents HelpProvider1 As System.Windows.Forms.HelpProvider

End Class
