<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmNewCase
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmNewCase))
        Me.btnHelp = New System.Windows.Forms.Button()
        Me.btnSaveExit = New System.Windows.Forms.Button()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.DsNewCase1 = New InfoCtr.dsNewCase()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cboCRG = New InfoCtr.ComboBoxRelaxed()
        Me.txtCaseName = New System.Windows.Forms.TextBox()
        Me.cboMgr = New InfoCtr.ComboBoxRelaxed()
        Me.fldOrgNum = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtDescription = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.cboStatus = New InfoCtr.ComboBoxRelaxed()
        Me.CheckBox1 = New System.Windows.Forms.CheckBox()
        Me.SqlSelectCommand1 = New System.Data.SqlClient.SqlCommand()
        Me.SqlUpdateCommand1 = New System.Data.SqlClient.SqlCommand()
        Me.daNewCase = New System.Data.SqlClient.SqlDataAdapter()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.fldCaseID = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.DtOpen = New InfoCtr.DateTextBox
        Me.txtCloseDate = New System.Windows.Forms.TextBox()
        CType(Me.DsNewCase1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnHelp
        '
        Me.btnHelp.Image = CType(resources.GetObject("btnHelp.Image"), System.Drawing.Image)
        Me.btnHelp.Location = New System.Drawing.Point(538, 10)
        Me.btnHelp.Name = "btnHelp"
        Me.btnHelp.Size = New System.Drawing.Size(25, 25)
        Me.btnHelp.TabIndex = 432
        Me.btnHelp.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnHelp.UseVisualStyleBackColor = True
        Me.btnHelp.Visible = False
        '
        'btnSaveExit
        '
        Me.btnSaveExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSaveExit.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.btnSaveExit.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSaveExit.Image = CType(resources.GetObject("btnSaveExit.Image"), System.Drawing.Image)
        Me.btnSaveExit.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSaveExit.Location = New System.Drawing.Point(492, 10)
        Me.btnSaveExit.Name = "btnSaveExit"
        Me.btnSaveExit.Size = New System.Drawing.Size(40, 35)
        Me.btnSaveExit.TabIndex = 430
        Me.btnSaveExit.Text = "Close"
        Me.btnSaveExit.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSaveExit.UseVisualStyleBackColor = False
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.Desktop
        Me.Label6.Location = New System.Drawing.Point(27, 26)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(228, 17)
        Me.Label6.TabIndex = 429
        Me.Label6.Text = "Required Information for New Case"
        '
        'DsNewCase1
        '
        Me.DsNewCase1.DataSetName = "dsNewCase"
        Me.DsNewCase1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(46, 170)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(58, 13)
        Me.Label5.TabIndex = 427
        Me.Label5.Text = "CRG Issue"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(46, 141)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(55, 13)
        Me.Label4.TabIndex = 426
        Me.Label4.Text = "Start Date"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(46, 276)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(60, 13)
        Me.Label3.TabIndex = 425
        Me.Label3.Text = "Description"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(33, 108)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(76, 13)
        Me.Label2.TabIndex = 424
        Me.Label2.Text = "Case Manager"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(39, 59)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(65, 13)
        Me.Label1.TabIndex = 423
        Me.Label1.Text = "Case  Name"
        '
        'cboCRG
        '
        Me.cboCRG.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboCRG.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboCRG.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.DsNewCase1, "MainNewCase.CRGNum", True))
        Me.cboCRG.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCRG.FormattingEnabled = True
        Me.cboCRG.Location = New System.Drawing.Point(113, 162)
        Me.cboCRG.Name = "cboCRG"
        Me.cboCRG.RestrictContentToListItems = True
        Me.cboCRG.Size = New System.Drawing.Size(354, 21)
        Me.cboCRG.TabIndex = 3
        '
        'txtCaseName
        '
        Me.txtCaseName.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsNewCase1, "MainNewCase.CaseName", True))
        Me.txtCaseName.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCaseName.ForeColor = System.Drawing.SystemColors.ControlText
        Me.txtCaseName.Location = New System.Drawing.Point(113, 51)
        Me.txtCaseName.MaxLength = 80
        Me.txtCaseName.Multiline = True
        Me.txtCaseName.Name = "txtCaseName"
        Me.txtCaseName.Size = New System.Drawing.Size(300, 46)
        Me.txtCaseName.TabIndex = 0
        '
        'cboMgr
        '
        Me.cboMgr.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboMgr.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboMgr.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.DsNewCase1, "MainNewCase.CaseMgrNum", True))
        Me.cboMgr.Items.AddRange(New Object() {"Center Hosted", "Center Sponsored", "Conference", "Congregational Connection", "Grant Workshop", "Invitational Gathering", "Long Term Learning", "Workshop"})
        Me.cboMgr.Location = New System.Drawing.Point(113, 105)
        Me.cboMgr.Name = "cboMgr"
        Me.cboMgr.RestrictContentToListItems = True
        Me.cboMgr.Size = New System.Drawing.Size(236, 21)
        Me.cboMgr.TabIndex = 1
        '
        'fldOrgNum
        '
        Me.fldOrgNum.BackColor = System.Drawing.SystemColors.Control
        Me.fldOrgNum.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsNewCase1, "MainNewCase.OrgNum", True))
        Me.fldOrgNum.Enabled = False
        Me.fldOrgNum.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fldOrgNum.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fldOrgNum.Location = New System.Drawing.Point(515, 378)
        Me.fldOrgNum.MaxLength = 80
        Me.fldOrgNum.Multiline = True
        Me.fldOrgNum.Name = "fldOrgNum"
        Me.fldOrgNum.ReadOnly = True
        Me.fldOrgNum.Size = New System.Drawing.Size(64, 25)
        Me.fldOrgNum.TabIndex = 433
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.SystemColors.Desktop
        Me.Label7.Location = New System.Drawing.Point(33, 240)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(135, 17)
        Me.Label7.TabIndex = 434
        Me.Label7.Text = "Optional Information"
        '
        'txtDescription
        '
        Me.txtDescription.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsNewCase1, "MainNewCase.Description", True))
        Me.txtDescription.Location = New System.Drawing.Point(113, 273)
        Me.txtDescription.Multiline = True
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.Size = New System.Drawing.Size(294, 63)
        Me.txtDescription.TabIndex = 5
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlDark
        Me.Label8.Location = New System.Drawing.Point(463, 378)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(34, 13)
        Me.Label8.TabIndex = 440
        Me.Label8.Text = "Org #"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(64, 194)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(37, 13)
        Me.Label9.TabIndex = 442
        Me.Label9.Text = "Status"
        '
        'cboStatus
        '
        Me.cboStatus.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboStatus.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboStatus.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.DsNewCase1, "MainNewCase.StatusNum", True))
        Me.cboStatus.FormattingEnabled = True
        Me.cboStatus.Location = New System.Drawing.Point(113, 191)
        Me.cboStatus.Name = "cboStatus"
        Me.cboStatus.RestrictContentToListItems = True
        Me.cboStatus.Size = New System.Drawing.Size(236, 21)
        Me.cboStatus.TabIndex = 4
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.DataBindings.Add(New System.Windows.Forms.Binding("Checked", Me.DsNewCase1, "MainNewCase.NoResources", True))
        Me.CheckBox1.Location = New System.Drawing.Point(113, 385)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(195, 17)
        Me.CheckBox1.TabIndex = 7
        Me.CheckBox1.Text = "No Resource Required by this Case"
        Me.CheckBox1.UseVisualStyleBackColor = True
        '
        'SqlSelectCommand1
        '
        Me.SqlSelectCommand1.CommandText = "dbo.MainNewCase"
        Me.SqlSelectCommand1.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlSelectCommand1.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, False, CType(0, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing), New System.Data.SqlClient.SqlParameter("@ID", System.Data.SqlDbType.Int, 4)})
        '
        'SqlUpdateCommand1
        '
        Me.SqlUpdateCommand1.CommandText = "dbo.MainNewCaseUpdate"
        Me.SqlUpdateCommand1.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlUpdateCommand1.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, False, CType(0, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing), New System.Data.SqlClient.SqlParameter("@CaseName", System.Data.SqlDbType.NVarChar, 50, "CaseName"), New System.Data.SqlClient.SqlParameter("@OrgNum", System.Data.SqlDbType.Int, 4, "OrgNum"), New System.Data.SqlClient.SqlParameter("@StatusNum", System.Data.SqlDbType.SmallInt, 2, "StatusNum"), New System.Data.SqlClient.SqlParameter("@CaseMgrNum", System.Data.SqlDbType.Int, 4, "CaseMgrNum"), New System.Data.SqlClient.SqlParameter("@StartDate", System.Data.SqlDbType.SmallDateTime, 4, "OpenDate"), New System.Data.SqlClient.SqlParameter("@CloseDate", System.Data.SqlDbType.SmallDateTime, 4, "CloseDate"), New System.Data.SqlClient.SqlParameter("@CRGNum", System.Data.SqlDbType.SmallInt, 2, "CRGNum"), New System.Data.SqlClient.SqlParameter("@Description", System.Data.SqlDbType.NVarChar, 255, "Description"), New System.Data.SqlClient.SqlParameter("@NoResources", System.Data.SqlDbType.Bit, 1, "NoResources"), New System.Data.SqlClient.SqlParameter("@ID", System.Data.SqlDbType.Int, 4, "CaseID")})
        '
        'daNewCase
        '
        Me.daNewCase.SelectCommand = Me.SqlSelectCommand1
        Me.daNewCase.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "MainNewCase", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("CaseID", "CaseID"), New System.Data.Common.DataColumnMapping("CaseName", "CaseName"), New System.Data.Common.DataColumnMapping("OrgNum", "OrgNum"), New System.Data.Common.DataColumnMapping("StatusNum", "StatusNum"), New System.Data.Common.DataColumnMapping("CaseMgrNum", "CaseMgrNum"), New System.Data.Common.DataColumnMapping("OpenDate", "OpenDate"), New System.Data.Common.DataColumnMapping("CloseDate", "CloseDate"), New System.Data.Common.DataColumnMapping("CRGNum", "CRGNum"), New System.Data.Common.DataColumnMapping("Description", "Description"), New System.Data.Common.DataColumnMapping("NoResources", "NoResources")})})
        Me.daNewCase.UpdateCommand = Me.SqlUpdateCommand1
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlDark
        Me.Label10.Location = New System.Drawing.Point(463, 345)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(41, 13)
        Me.Label10.TabIndex = 445
        Me.Label10.Text = "Case #"
        '
        'fldCaseID
        '
        Me.fldCaseID.BackColor = System.Drawing.SystemColors.Control
        Me.fldCaseID.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsNewCase1, "MainNewCase.CaseID", True))
        Me.fldCaseID.Enabled = False
        Me.fldCaseID.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fldCaseID.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fldCaseID.Location = New System.Drawing.Point(515, 345)
        Me.fldCaseID.MaxLength = 80
        Me.fldCaseID.Multiline = True
        Me.fldCaseID.Name = "fldCaseID"
        Me.fldCaseID.ReadOnly = True
        Me.fldCaseID.Size = New System.Drawing.Size(64, 25)
        Me.fldCaseID.TabIndex = 444
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(46, 356)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(59, 13)
        Me.Label11.TabIndex = 446
        Me.Label11.Text = "Close Date"
        '
        'DtOpen
        '
        Me.DtOpen.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsNewCase1, "MainNewCase.OpenDate", True))
        Me.DtOpen.Location = New System.Drawing.Point(115, 133)
        Me.DtOpen.Name = "DtOpen"
        Me.DtOpen.Size = New System.Drawing.Size(140, 20)
        Me.DtOpen.TabIndex = 2
        '
        'txtCloseDate
        '
        Me.txtCloseDate.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsNewCase1, "MainNewCase.CloseDate", True))
        Me.txtCloseDate.Location = New System.Drawing.Point(116, 351)
        Me.txtCloseDate.Name = "txtCloseDate"
        Me.txtCloseDate.ReadOnly = True
        Me.txtCloseDate.Size = New System.Drawing.Size(139, 20)
        Me.txtCloseDate.TabIndex = 6
        '
        'frmNewCase
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(591, 415)
        Me.Controls.Add(Me.txtCloseDate)
        Me.Controls.Add(Me.DtOpen)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.fldCaseID)
        Me.Controls.Add(Me.CheckBox1)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.cboStatus)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.txtDescription)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.fldOrgNum)
        Me.Controls.Add(Me.btnHelp)
        Me.Controls.Add(Me.btnSaveExit)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cboCRG)
        Me.Controls.Add(Me.txtCaseName)
        Me.Controls.Add(Me.cboMgr)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmNewCase"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "New Case"
        CType(Me.DsNewCase1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnHelp As System.Windows.Forms.Button
    Friend WithEvents btnSaveExit As System.Windows.Forms.Button
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cboCRG As InfoCtr.ComboBoxRelaxed
    Friend WithEvents txtCaseName As System.Windows.Forms.TextBox
    Friend WithEvents cboMgr As InfoCtr.ComboBoxRelaxed
    Friend WithEvents fldOrgNum As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtDescription As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents cboStatus As InfoCtr.ComboBoxRelaxed
    Friend WithEvents CheckBox1 As System.Windows.Forms.CheckBox
    Friend WithEvents SqlSelectCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlUpdateCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents daNewCase As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents DsNewCase1 As InfoCtr.dsNewCase
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents fldCaseID As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents DtOpen As InfoCtr.DateTextBox
    Friend WithEvents txtCloseDate As System.Windows.Forms.TextBox
End Class
