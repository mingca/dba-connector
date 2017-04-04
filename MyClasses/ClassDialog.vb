'<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>_

Imports System.Windows.Forms

Partial Class ClassDialog
    Inherits System.Windows.Forms.Form

#Region "SetUp"


    Friend WithEvents btnNo As System.Windows.Forms.Button
    Friend WithEvents btnYes As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents lblDescription As System.Windows.Forms.Label
    Friend lblTopic As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox

    ''Form overrides dispose to clean up the component list.
    '<System.Diagnostics.DebuggerNonUserCode()> _
    'Protected Overrides Sub Dispose(ByVal disposing As Boolean)
    '    Try
    '        If disposing AndAlso components IsNot Nothing Then
    '            components.Dispose()
    '        End If
    '    Finally
    '        MyBase.Dispose(disposing)
    '    End Try
    'End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.btnNo = New System.Windows.Forms.Button
        Me.btnYes = New System.Windows.Forms.Button
        Me.btnCancel = New System.Windows.Forms.Button
        Me.lblDescription = New System.Windows.Forms.Label
        Me.lblTopic = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PictureBox1
        '
        Me.PictureBox1.BackColor = System.Drawing.Color.Transparent
        Me.PictureBox1.Image = Global.InfoCtr.My.Resources.Resources.question
        Me.PictureBox1.Location = New System.Drawing.Point(12, 41)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(112, 82)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox1.TabIndex = 7
        Me.PictureBox1.TabStop = False
        '
        'btnNo
        '
        Me.btnNo.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.btnNo.DialogResult = System.Windows.Forms.DialogResult.No
        Me.btnNo.Location = New System.Drawing.Point(360, 358)
        Me.btnNo.Margin = New System.Windows.Forms.Padding(0)
        Me.btnNo.Name = "btnNo"
        Me.btnNo.Size = New System.Drawing.Size(207, 102)
        Me.btnNo.TabIndex = 5
        Me.btnNo.Text = "No"
        '
        'btnYes
        '
        Me.btnYes.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.btnYes.DialogResult = System.Windows.Forms.DialogResult.Yes
        Me.btnYes.Location = New System.Drawing.Point(42, 358)
        Me.btnYes.Name = "btnYes"
        Me.btnYes.Size = New System.Drawing.Size(207, 102)
        Me.btnYes.TabIndex = 3
        Me.btnYes.Text = "Yes"
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Location = New System.Drawing.Point(568, 12)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(20, 21)
        Me.btnCancel.TabIndex = 4
        Me.btnCancel.Text = "Cancel"
        '
        'lblDescription
        '
        Me.lblDescription.AutoSize = True
        Me.lblDescription.BackColor = System.Drawing.Color.Transparent
        Me.lblDescription.Font = New System.Drawing.Font("Comic Sans MS", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDescription.Location = New System.Drawing.Point(39, 146)
        Me.lblDescription.Margin = New System.Windows.Forms.Padding(0)
        Me.lblDescription.MaximumSize = New System.Drawing.Size(500, 235)
        Me.lblDescription.MinimumSize = New System.Drawing.Size(500, 200)
        Me.lblDescription.Name = "lblDescription"
        Me.lblDescription.Size = New System.Drawing.Size(500, 200)
        Me.lblDescription.TabIndex = 8
        Me.lblDescription.Text = "Label1"
        '
        'lblTopic
        '
        Me.lblTopic.AutoSize = True
        Me.lblTopic.BackColor = System.Drawing.Color.Transparent
        Me.lblTopic.Font = New System.Drawing.Font("Comic Sans MS", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTopic.Location = New System.Drawing.Point(129, 57)
        Me.lblTopic.MaximumSize = New System.Drawing.Size(450, 70)
        Me.lblTopic.MinimumSize = New System.Drawing.Size(375, 0)
        Me.lblTopic.Name = "lblTopic"
        Me.lblTopic.Size = New System.Drawing.Size(375, 23)
        Me.lblTopic.TabIndex = 9
        Me.lblTopic.Text = "Label2"
        Me.lblTopic.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(12, 360)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(36, 26)
        Me.Label3.TabIndex = 10
        Me.Label3.Text = "A)"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(333, 360)
        Me.Label4.Margin = New System.Windows.Forms.Padding(0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(36, 26)
        Me.Label4.TabIndex = 11
        Me.Label4.Text = "B)"
        '
        'ClassDialog
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = Global.InfoCtr.My.Resources.Resources.Win7_Yellow_1920x1200
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(600, 500)
        Me.Controls.Add(Me.lblTopic)
        Me.Controls.Add(Me.lblDescription)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.btnNo)
        Me.Controls.Add(Me.btnYes)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "ClassDialog"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "IT'S YOUR CHOICE..."
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub


#End Region

    Public Sub New(ByVal strHeading As String, ByVal strBtnYes As String, ByVal strBtnNo As String, ByVal strInstructions As String, ByVal bCancel As Boolean)
        ' Dim f As Form = DialogMaster
        'f.Text = strHeading
        'f.Controls("BtnYes").Text = strBtnYes
        'f.Controls("BtnNo").Text = strBtnNo
        'f.Controls("btnCancel").Visible = bCancel
        'f.ShowDialog()
        InitializeComponent()
        Me.lblTopic.Text = UCase(strHeading)
        Me.lblDescription.Text = strInstructions
        Me.btnYes.Text = strBtnYes
        Me.btnNo.Text = strBtnNo
        Me.btnCancel.Visible = bCancel

        Me.ShowDialog()

    End Sub

    Private Sub btnYes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.DialogResult = System.Windows.Forms.DialogResult.Yes
        Me.Close()
    End Sub

    Private Sub btnNo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.DialogResult = System.Windows.Forms.DialogResult.No
        Me.Close()
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub Label3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label3.Click
        Me.btnYes.PerformClick()
    End Sub
    Private Sub Label4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label4.Click
        Me.btnNo.PerformClick()
    End Sub


End Class
