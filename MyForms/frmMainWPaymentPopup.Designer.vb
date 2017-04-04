<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMainWPaymentPopup
    Inherits System.Windows.Forms.Form



    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer
    Friend WithEvents lvSource As System.Windows.Forms.ListView
    Friend WithEvents txtSrch As System.Windows.Forms.TextBox
    Friend WithEvents btnSrch As System.Windows.Forms.Button
    Friend WithEvents btnApply As System.Windows.Forms.Button
    Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader2 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader3 As System.Windows.Forms.ColumnHeader
    Friend WithEvents fldPaymentID As System.Windows.Forms.TextBox
    Friend WithEvents ColumnHeader4 As System.Windows.Forms.ColumnHeader
    Friend WithEvents cboResult As InfoCtr.ComboBoxRelaxed
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents rbOrder As System.Windows.Forms.RadioButton
    Friend WithEvents rbOrg As System.Windows.Forms.RadioButton
    Friend WithEvents rbLastname As System.Windows.Forms.RadioButton
    Friend WithEvents rbEvent As System.Windows.Forms.RadioButton
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents ColumnHeader7 As System.Windows.Forms.ColumnHeader
    Friend WithEvents Label3 As System.Windows.Forms.Label
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim ListViewItem1 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("Line1")
        Dim ListViewItem2 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("Line2")
        Dim ListViewItem3 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("Line3")
        Dim ListViewItem4 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("Line4")
        Dim ListViewItem5 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("Line5")
        Me.lvSource = New System.Windows.Forms.ListView()
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader3 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader4 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader5 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader6 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader7 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.txtSrch = New System.Windows.Forms.TextBox()
        Me.btnSrch = New System.Windows.Forms.Button()
        Me.btnApply = New System.Windows.Forms.Button()
        Me.fldPaymentID = New System.Windows.Forms.TextBox()
        Me.cboResult = New InfoCtr.ComboBoxRelaxed()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.rbOrder = New System.Windows.Forms.RadioButton()
        Me.rbOrg = New System.Windows.Forms.RadioButton()
        Me.rbLastname = New System.Windows.Forms.RadioButton()
        Me.rbEvent = New System.Windows.Forms.RadioButton()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'lvSource
        '
        Me.lvSource.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lvSource.CheckBoxes = True
        Me.lvSource.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2, Me.ColumnHeader3, Me.ColumnHeader4, Me.ColumnHeader5, Me.ColumnHeader6, Me.ColumnHeader7})
        Me.lvSource.FullRowSelect = True
        ListViewItem1.StateImageIndex = 0
        ListViewItem2.StateImageIndex = 0
        ListViewItem3.StateImageIndex = 0
        ListViewItem4.StateImageIndex = 0
        ListViewItem5.StateImageIndex = 0
        Me.lvSource.Items.AddRange(New System.Windows.Forms.ListViewItem() {ListViewItem1, ListViewItem2, ListViewItem3, ListViewItem4, ListViewItem5})
        Me.lvSource.Location = New System.Drawing.Point(239, 87)
        Me.lvSource.Name = "lvSource"
        Me.lvSource.Size = New System.Drawing.Size(809, 220)
        Me.lvSource.TabIndex = 0
        Me.lvSource.UseCompatibleStateImageBehavior = False
        Me.lvSource.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = " "
        Me.ColumnHeader1.Width = 20
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "Name"
        Me.ColumnHeader2.Width = 150
        '
        'ColumnHeader3
        '
        Me.ColumnHeader3.Text = "Congr"
        Me.ColumnHeader3.Width = 150
        '
        'ColumnHeader4
        '
        Me.ColumnHeader4.Text = "EventName"
        Me.ColumnHeader4.Width = 200
        '
        'ColumnHeader5
        '
        Me.ColumnHeader5.Text = "Fee"
        Me.ColumnHeader5.Width = 50
        '
        'ColumnHeader6
        '
        Me.ColumnHeader6.Text = "OrderID"
        Me.ColumnHeader6.Width = 70
        '
        'ColumnHeader7
        '
        Me.ColumnHeader7.Text = "RegistrationID"
        Me.ColumnHeader7.Width = 70
        '
        'txtSrch
        '
        Me.txtSrch.Location = New System.Drawing.Point(20, 13)
        Me.txtSrch.Name = "txtSrch"
        Me.txtSrch.Size = New System.Drawing.Size(153, 20)
        Me.txtSrch.TabIndex = 26
        Me.txtSrch.Text = "enter search text"
        '
        'btnSrch
        '
        Me.btnSrch.Location = New System.Drawing.Point(35, 49)
        Me.btnSrch.Name = "btnSrch"
        Me.btnSrch.Size = New System.Drawing.Size(103, 23)
        Me.btnSrch.TabIndex = 27
        Me.btnSrch.Text = "Reload List"
        Me.btnSrch.UseVisualStyleBackColor = True
        '
        'btnApply
        '
        Me.btnApply.Location = New System.Drawing.Point(365, 31)
        Me.btnApply.Name = "btnApply"
        Me.btnApply.Size = New System.Drawing.Size(171, 23)
        Me.btnApply.TabIndex = 28
        Me.btnApply.Text = "Apply Payment"
        Me.btnApply.UseVisualStyleBackColor = True
        '
        'fldPaymentID
        '
        Me.fldPaymentID.Location = New System.Drawing.Point(455, 331)
        Me.fldPaymentID.Name = "fldPaymentID"
        Me.fldPaymentID.Size = New System.Drawing.Size(100, 20)
        Me.fldPaymentID.TabIndex = 29
        Me.fldPaymentID.Text = "PaymentID"
        '
        'cboResult
        '
        Me.cboResult.FormattingEnabled = True
        Me.cboResult.Location = New System.Drawing.Point(94, 33)
        Me.cboResult.Name = "cboResult"
        Me.cboResult.Size = New System.Drawing.Size(171, 21)
        Me.cboResult.TabIndex = 30
        Me.cboResult.Visible = False
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.rbOrder)
        Me.Panel1.Controls.Add(Me.rbOrg)
        Me.Panel1.Controls.Add(Me.rbLastname)
        Me.Panel1.Controls.Add(Me.rbEvent)
        Me.Panel1.Controls.Add(Me.txtSrch)
        Me.Panel1.Controls.Add(Me.btnSrch)
        Me.Panel1.Location = New System.Drawing.Point(12, 87)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(193, 220)
        Me.Panel1.TabIndex = 31
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Maroon
        Me.Label1.Location = New System.Drawing.Point(17, 172)
        Me.Label1.MaximumSize = New System.Drawing.Size(175, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(165, 39)
        Me.Label1.TabIndex = 32
        Me.Label1.Text = "Use this section to find additional registrations to include in this payment."
        '
        'rbOrder
        '
        Me.rbOrder.AutoSize = True
        Me.rbOrder.Checked = True
        Me.rbOrder.Location = New System.Drawing.Point(50, 99)
        Me.rbOrder.Name = "rbOrder"
        Me.rbOrder.Size = New System.Drawing.Size(76, 17)
        Me.rbOrder.TabIndex = 31
        Me.rbOrder.TabStop = True
        Me.rbOrder.Text = "Order Num"
        Me.rbOrder.UseVisualStyleBackColor = True
        '
        'rbOrg
        '
        Me.rbOrg.AutoSize = True
        Me.rbOrg.Location = New System.Drawing.Point(50, 140)
        Me.rbOrg.Name = "rbOrg"
        Me.rbOrg.Size = New System.Drawing.Size(88, 17)
        Me.rbOrg.TabIndex = 30
        Me.rbOrg.Text = "Congregation"
        Me.rbOrg.UseVisualStyleBackColor = True
        '
        'rbLastname
        '
        Me.rbLastname.AutoSize = True
        Me.rbLastname.Location = New System.Drawing.Point(50, 120)
        Me.rbLastname.Name = "rbLastname"
        Me.rbLastname.Size = New System.Drawing.Size(76, 17)
        Me.rbLastname.TabIndex = 29
        Me.rbLastname.Text = "Last Name"
        Me.rbLastname.UseVisualStyleBackColor = True
        '
        'rbEvent
        '
        Me.rbEvent.AutoSize = True
        Me.rbEvent.Location = New System.Drawing.Point(50, 78)
        Me.rbEvent.Name = "rbEvent"
        Me.rbEvent.Size = New System.Drawing.Size(53, 17)
        Me.rbEvent.TabIndex = 28
        Me.rbEvent.Text = "Event"
        Me.rbEvent.UseVisualStyleBackColor = True
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(736, 31)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(75, 23)
        Me.btnClose.TabIndex = 32
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Maroon
        Me.Label2.Location = New System.Drawing.Point(236, 71)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(400, 13)
        Me.Label2.TabIndex = 33
        Me.Label2.Text = "Click the Checkbox for everyone this payment covers.  Then click the Apply button" & _
    "."
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Maroon
        Me.Label3.Location = New System.Drawing.Point(12, 36)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(76, 13)
        Me.Label3.TabIndex = 34
        Me.Label3.Text = "Grid based on:"
        Me.Label3.Visible = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(390, 334)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(59, 13)
        Me.Label4.TabIndex = 35
        Me.Label4.Text = "PaymentID"
        '
        'frmMainWPaymentPopup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1060, 363)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.cboResult)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.fldPaymentID)
        Me.Controls.Add(Me.btnApply)
        Me.Controls.Add(Me.lvSource)
        Me.MaximizeBox = False
        Me.Name = "frmMainWPaymentPopup"
        Me.Text = "APPLYING PAYMENT"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ColumnHeader5 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader6 As System.Windows.Forms.ColumnHeader
    Friend WithEvents Label4 As System.Windows.Forms.Label

End Class
