Option Explicit On
Imports System.Net.Mail
Imports System.Text
Imports System.Diagnostics



Public Class FormColors
    Inherits System.Windows.Forms.Form
    Dim isLoaded As Boolean = False

#Region "Initialize"

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub
#End Region 'initialize


#Region " Windows Form Designer generated code "
    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.

    '  Friend WithEvents dgMain As DGFilter
    Friend WithEvents SqlSelectCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents lblNW As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents lblNE As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents lblSouth As System.Windows.Forms.Label
    Friend WithEvents lblSW As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents lblCentral As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents lblSE As System.Windows.Forms.Label
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents Process1 As System.Diagnostics.Process
    Friend WithEvents txtBlue As System.Windows.Forms.TextBox
    Friend WithEvents txtGreen As System.Windows.Forms.TextBox
    Friend WithEvents txtRed As System.Windows.Forms.TextBox
    Friend WithEvents TestResult As System.Windows.Forms.TextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents NumericUpDown1 As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label28 As System.Windows.Forms.Label


    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.SqlSelectCommand1 = New System.Data.SqlClient.SqlCommand()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lblNW = New System.Windows.Forms.Label()
        Me.lblCentral = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.lblNE = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.lblSouth = New System.Windows.Forms.Label()
        Me.lblSW = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.lblSE = New System.Windows.Forms.Label()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.Process1 = New System.Diagnostics.Process()
        Me.txtRed = New System.Windows.Forms.TextBox()
        Me.txtGreen = New System.Windows.Forms.TextBox()
        Me.txtBlue = New System.Windows.Forms.TextBox()
        Me.TestResult = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.NumericUpDown1 = New System.Windows.Forms.NumericUpDown()
        Me.Label15 = New System.Windows.Forms.Label()
        CType(Me.NumericUpDown1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.SteelBlue
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(472, 328)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(160, 32)
        Me.Label1.TabIndex = 6
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(155, Byte), Integer), CType(CType(155, Byte), Integer))
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(472, 360)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(160, 32)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "old se"
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.Salmon
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(232, 272)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(160, 32)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "salmon"
        '
        'lblNW
        '
        Me.lblNW.BackColor = System.Drawing.Color.Cornsilk
        Me.lblNW.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNW.Location = New System.Drawing.Point(704, 256)
        Me.lblNW.Name = "lblNW"
        Me.lblNW.Size = New System.Drawing.Size(160, 32)
        Me.lblNW.TabIndex = 27
        Me.lblNW.Text = "NW"
        '
        'lblCentral
        '
        Me.lblCentral.BackColor = System.Drawing.Color.FromArgb(CType(CType(195, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.lblCentral.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCentral.Location = New System.Drawing.Point(704, 160)
        Me.lblCentral.Name = "lblCentral"
        Me.lblCentral.Size = New System.Drawing.Size(160, 32)
        Me.lblCentral.TabIndex = 24
        Me.lblCentral.Text = "Central"
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(155, Byte), Integer), CType(CType(155, Byte), Integer))
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(232, 240)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(160, 32)
        Me.Label6.TabIndex = 9
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(230, Byte), Integer), CType(CType(200, Byte), Integer), CType(CType(170, Byte), Integer))
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(232, 368)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(160, 32)
        Me.Label7.TabIndex = 11
        Me.Label7.Text = "old sw"
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(180, Byte), Integer))
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(232, 208)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(160, 32)
        Me.Label8.TabIndex = 10
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(160, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(232, 112)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(160, 32)
        Me.Label9.TabIndex = 12
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.DarkSalmon
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(232, 304)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(160, 32)
        Me.Label10.TabIndex = 13
        Me.Label10.Text = "dark salmon"
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.FromArgb(CType(CType(250, Byte), Integer), CType(CType(110, Byte), Integer), CType(CType(105, Byte), Integer))
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(472, 260)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(160, 32)
        Me.Label11.TabIndex = 20
        '
        'lblNE
        '
        Me.lblNE.BackColor = System.Drawing.Color.FromArgb(CType(CType(200, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.lblNE.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNE.Location = New System.Drawing.Point(704, 192)
        Me.lblNE.Name = "lblNE"
        Me.lblNE.Size = New System.Drawing.Size(160, 32)
        Me.lblNE.TabIndex = 25
        Me.lblNE.Text = "NE"
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.Teal
        Me.Label13.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(472, 188)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(160, 32)
        Me.Label13.TabIndex = 18
        '
        'lblSouth
        '
        Me.lblSouth.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(170, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.lblSouth.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSouth.Location = New System.Drawing.Point(704, 336)
        Me.lblSouth.Name = "lblSouth"
        Me.lblSouth.Size = New System.Drawing.Size(160, 32)
        Me.lblSouth.TabIndex = 29
        Me.lblSouth.Text = "South"
        '
        'lblSW
        '
        Me.lblSW.BackColor = System.Drawing.Color.FromArgb(CType(CType(230, Byte), Integer), CType(CType(200, Byte), Integer), CType(CType(170, Byte), Integer))
        Me.lblSW.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSW.Location = New System.Drawing.Point(704, 224)
        Me.lblSW.Name = "lblSW"
        Me.lblSW.Size = New System.Drawing.Size(160, 32)
        Me.lblSW.TabIndex = 26
        Me.lblSW.Text = "SW"
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.Color.FromArgb(CType(CType(204, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Label16.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(704, 80)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(160, 32)
        Me.Label16.TabIndex = 22
        Me.Label16.Text = "MainGrid Alternate"
        '
        'Label17
        '
        Me.Label17.BackColor = System.Drawing.Color.FromArgb(CType(CType(225, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.Label17.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(704, 112)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(160, 32)
        Me.Label17.TabIndex = 23
        Me.Label17.Text = "Sec. Grid Alternate"
        '
        'Label18
        '
        Me.Label18.BackColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.Label18.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(472, 296)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(160, 32)
        Me.Label18.TabIndex = 21
        Me.Label18.Text = "nice blue"
        '
        'Label19
        '
        Me.Label19.BackColor = System.Drawing.Color.FromArgb(CType(CType(175, Byte), Integer), CType(CType(210, Byte), Integer), CType(CType(175, Byte), Integer))
        Me.Label19.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.Location = New System.Drawing.Point(472, 112)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(160, 32)
        Me.Label19.TabIndex = 16
        '
        'Label20
        '
        Me.Label20.BackColor = System.Drawing.Color.FromArgb(CType(CType(190, Byte), Integer), CType(CType(230, Byte), Integer), CType(CType(190, Byte), Integer))
        Me.Label20.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.Location = New System.Drawing.Point(472, 80)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(160, 32)
        Me.Label20.TabIndex = 15
        '
        'Label21
        '
        Me.Label21.BackColor = System.Drawing.SystemColors.ControlLight
        Me.Label21.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label21.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.Location = New System.Drawing.Point(232, 8)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(160, 48)
        Me.Label21.TabIndex = 22
        Me.Label21.Text = "NAMED"
        '
        'Label22
        '
        Me.Label22.BackColor = System.Drawing.SystemColors.Control
        Me.Label22.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label22.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.Location = New System.Drawing.Point(472, 8)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(160, 48)
        Me.Label22.TabIndex = 23
        Me.Label22.Text = "CREATED"
        '
        'Label23
        '
        Me.Label23.BackColor = System.Drawing.SystemColors.ControlDark
        Me.Label23.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label23.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.Location = New System.Drawing.Point(696, 8)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(160, 48)
        Me.Label23.TabIndex = 24
        Me.Label23.Text = "CHOSEN"
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.Bisque
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(472, 224)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(160, 32)
        Me.Label5.TabIndex = 19
        Me.Label5.Text = "oldSouthern"
        '
        'Label24
        '
        Me.Label24.BackColor = System.Drawing.Color.FromArgb(CType(CType(221, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(221, Byte), Integer))
        Me.Label24.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.Location = New System.Drawing.Point(472, 152)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(160, 32)
        Me.Label24.TabIndex = 17
        Me.Label24.Text = "oldNE"
        '
        'lblSE
        '
        Me.lblSE.BackColor = System.Drawing.Color.FromArgb(CType(CType(125, Byte), Integer), CType(CType(150, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.lblSE.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSE.Location = New System.Drawing.Point(704, 288)
        Me.lblSE.Name = "lblSE"
        Me.lblSE.Size = New System.Drawing.Size(160, 32)
        Me.lblSE.TabIndex = 28
        Me.lblSE.Text = "SE"
        '
        'Label27
        '
        Me.Label27.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(175, Byte), Integer))
        Me.Label27.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label27.Location = New System.Drawing.Point(232, 336)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(160, 32)
        Me.Label27.TabIndex = 14
        '
        'Label28
        '
        Me.Label28.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label28.Location = New System.Drawing.Point(704, 368)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(160, 40)
        Me.Label28.TabIndex = 30
        Me.Label28.Text = "238, 178, 148;  220, 160, 122; 245,222,179"
        '
        'Process1
        '
        Me.Process1.StartInfo.Domain = ""
        Me.Process1.StartInfo.LoadUserProfile = False
        Me.Process1.StartInfo.Password = Nothing
        Me.Process1.StartInfo.StandardErrorEncoding = Nothing
        Me.Process1.StartInfo.StandardOutputEncoding = Nothing
        Me.Process1.StartInfo.UserName = ""
        Me.Process1.SynchronizingObject = Me
        '
        'txtRed
        '
        Me.txtRed.Location = New System.Drawing.Point(51, 327)
        Me.txtRed.Name = "txtRed"
        Me.txtRed.Size = New System.Drawing.Size(37, 20)
        Me.txtRed.TabIndex = 2
        Me.txtRed.Text = "0"
        '
        'txtGreen
        '
        Me.txtGreen.Location = New System.Drawing.Point(94, 327)
        Me.txtGreen.Name = "txtGreen"
        Me.txtGreen.Size = New System.Drawing.Size(35, 20)
        Me.txtGreen.TabIndex = 3
        Me.txtGreen.Text = "0"
        '
        'txtBlue
        '
        Me.txtBlue.Location = New System.Drawing.Point(140, 327)
        Me.txtBlue.Name = "txtBlue"
        Me.txtBlue.Size = New System.Drawing.Size(35, 20)
        Me.txtBlue.TabIndex = 4
        Me.txtBlue.Text = "0"
        '
        'TestResult
        '
        Me.TestResult.Location = New System.Drawing.Point(51, 190)
        Me.TestResult.MinimumSize = New System.Drawing.Size(0, 50)
        Me.TestResult.Name = "TestResult"
        Me.TestResult.Size = New System.Drawing.Size(158, 50)
        Me.TestResult.TabIndex = 5
        Me.TestResult.Text = "RGB result"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.PaleTurquoise
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(232, 80)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(160, 32)
        Me.Label4.TabIndex = 31
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.Turquoise
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(232, 144)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(160, 32)
        Me.Label12.TabIndex = 32
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.MediumTurquoise
        Me.Label14.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(232, 176)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(160, 32)
        Me.Label14.TabIndex = 33
        '
        'NumericUpDown1
        '
        Me.NumericUpDown1.Location = New System.Drawing.Point(55, 376)
        Me.NumericUpDown1.Maximum = New Decimal(New Integer() {50, 0, 0, 0})
        Me.NumericUpDown1.Minimum = New Decimal(New Integer() {50, 0, 0, -2147483648})
        Me.NumericUpDown1.Name = "NumericUpDown1"
        Me.NumericUpDown1.Size = New System.Drawing.Size(120, 20)
        Me.NumericUpDown1.TabIndex = 34
        Me.NumericUpDown1.Value = New Decimal(New Integer() {10, 0, 0, 0})
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(82, 275)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(63, 13)
        Me.Label15.TabIndex = 35
        Me.Label15.Text = "R     G      B"
        '
        'FormColors
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(923, 498)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.NumericUpDown1)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TestResult)
        Me.Controls.Add(Me.txtBlue)
        Me.Controls.Add(Me.txtGreen)
        Me.Controls.Add(Me.txtRed)
        Me.Controls.Add(Me.Label28)
        Me.Controls.Add(Me.Label27)
        Me.Controls.Add(Me.lblSouth)
        Me.Controls.Add(Me.lblSE)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.Label24)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label23)
        Me.Controls.Add(Me.Label22)
        Me.Controls.Add(Me.Label21)
        Me.Controls.Add(Me.Label20)
        Me.Controls.Add(Me.Label19)
        Me.Controls.Add(Me.Label18)
        Me.Controls.Add(Me.Label17)
        Me.Controls.Add(Me.Label16)
        Me.Controls.Add(Me.lblSW)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.lblNE)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.lblCentral)
        Me.Controls.Add(Me.lblNW)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label1)
        Me.Name = "FormColors"
        Me.Text = "EXPERIMENT: Colours"
        CType(Me.NumericUpDown1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Private Sub Form5_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '
        '' dgMain.CaptionText = "Double-click to open this lengthy  document."
        'dgMain.ReadOnly = True
        'dgMain.statusM = "hello"

        'dgMain.hdrM = "main datagrid"
        'dgMain.CaptionText = dgMain.hdrM
        'Me.SqlDataAdapter1.Fill(Me.DataSet11)
        '.....................................
        Dim ctl As Control
        'GET REGION COLOURS FROM mdGlobalVar
        For Each ctl In Me.Controls
            If ctl.Name.Substring(0, 3) = "lbl" Then
                '   modGlobalVar.Msg(ctl.Name.Substring(3, Len(ctl.Name) - 3))
                ctl.BackColor = modGlobalVar.GetRegionColor(ctl.Name.Substring(3, Len(ctl.Name) - 3))
            End If
        Next
        Dim bDone As Boolean
        Dim c As Color
        'LABEL COLOURS w NAME or RGB
        For Each ctl In Me.Controls
            If ctl.GetType.ToString = "System.Windows.Forms.Label" Then

                For Each kc As KnownColor In [Enum].GetValues(GetType(KnownColor))
                    bDone = False
                    c = Color.FromKnownColor(kc)
                    If c.ToArgb = ctl.BackColor.ToArgb Then
                        bDone = True
                        Exit For
                    End If
                Next kc
                If bDone = True Then
                    ctl.Text = Replace(c.Name, "0", "") & " " & ctl.Text & " " & Replace(Replace(Replace(Color.FromArgb(ctl.BackColor.ToArgb()).ToString, "[A=255, ", NextLine), "]", ""), "Color", "")
                Else
                    ctl.Text = Replace(ctl.BackColor.ToKnownColor.ToString, "0", "") & " " & ctl.Text & " " & Replace(Replace(Replace(Color.FromArgb(ctl.BackColor.ToArgb()).ToString, "[A=255, ", NextLine), "]", ""), "Color", "")
                End If
            End If
        Next
        isloaded = True
        'note .toknowncolor doesn't work from argb!
    End Sub


    Private Sub txtRed_Enter(ByVal sender As Object, ByVal e As System.EventArgs) _
            Handles txtRed.Enter, txtGreen.Enter, txtBlue.Enter
        sender.SelectAll()
    End Sub

    Private Sub TestResult_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) _
            Handles TestResult.GotFocus
        GetColor()
    End Sub
    Private Sub GetColor()
        Try
            Me.TestResult.BackColor = Color.FromArgb(255, CType(txtRed.Text, Integer), CType(txtGreen.Text, Integer), CType(txtBlue.Text, Integer))
        Catch ex As Exception
            modGlobalVar.Msg("color error", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
            'Me.txtRed.Focus()
        End Try
    End Sub

    Private Sub TestResult_leave(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles txtRed.Leave, txtGreen.Leave, txtBlue.Leave
        CheckMax(sender)
    End Sub

    Private Sub CheckMax(ByVal x As Object)
        If CType(x.text, Integer) > 255 Then
            x.text = 255
        End If
    End Sub
    Private Sub NumericUpDown1_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown1.ValueChanged
        If isloaded Then
            Me.txtRed.Text = CType(Me.txtRed.Text, Integer) + CType(Me.txtRed.Text, Integer) * CType(sender.value, Int16) / 100
            CheckMax(txtRed)
            Me.txtGreen.Text = CType(Me.txtGreen.Text, Integer) + CType(Me.txtGreen.Text, Integer) * CType(sender.value, Int16) / 100
            CheckMax(Me.txtGreen)
            Me.txtBlue.Text = CType(Me.txtBlue.Text, Integer) + CType(Me.txtBlue.Text, Integer) * CType(sender.value, Int16) / 100
            CheckMax(txtBlue)

            GetColor()
        End If
    End Sub
End Class
