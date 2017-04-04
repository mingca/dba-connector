Option Explicit On
'Option Strict On

Imports System.Reflection
Imports System.Data.SqlClient
Imports System.Text
Imports System.IO '.IsolatedStorage
Imports System.Drawing
Imports System.Deployment
Imports Microsoft.Office.Interop
Imports Microsoft.Office.Interop.Word
Imports Microsoft.Office.Interop.Word.WdParagraphAlignment
Imports Microsoft.Office.Interop.Excel
Imports System.Security.Permissions
Imports System.Threading
Imports System.Runtime.InteropServices
Imports System.Deployment.Application

Imports System.Net.Mail
'Imports EnvDTE
'Imports Microsoft.Office.Interop.Outlook


Public Class frmStart
    Inherits System.Windows.Forms.Form

    Dim ExlApp As Microsoft.Office.Interop.Excel.Application
    ' Declaration
    Public Event UnhandledException(ByVal sender As Object, ByVal e As UnhandledExceptionEventArgs)
    Private Declare Sub Sleep Lib "kernel32" (ByVal dwMilliseconds As Long)
    '  Public bOpenMultiple As Boolean = False  'TODO so can open multiple instances only when requested.
    Dim SoundInst As New SoundClass
    ' Public Shared Timer1 As System.Windows.Forms.Timer
    ' Private Thrd As Thread
    Public Const GW_HWNDPREV = 3
    Declare Function OpenIcon Lib "user32" (ByVal hwnd As Long) As Long
     Declare Function SetForegroundWindow Lib "user32" (ByVal hwnd As Long) As Long

#Region "Initialize"

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()
        System.Windows.Forms.Application.EnableVisualStyles()

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)

        Forms.Remove(Me)
    End Sub
#End Region 'initialize

#Region "Windows Form Designer generated code "

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents btnHelp As System.Windows.Forms.Button
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents lblCase1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents pbHeading As System.Windows.Forms.PictureBox
    Friend WithEvents lblContact2 As System.Windows.Forms.Label
    Friend WithEvents lblCase2 As System.Windows.Forms.Label
    Friend WithEvents lblContact3 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents lblStatus As System.Windows.Forms.Label
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents ConstructionImage As System.Windows.Forms.PictureBox
    Friend WithEvents ConstructionLbl As System.Windows.Forms.Label
    Friend WithEvents pb As System.Windows.Forms.ProgressBar
    Friend WithEvents Process1 As System.Diagnostics.Process
    Friend WithEvents Label1 As System.Windows.Forms.Label

    Friend WithEvents testConnection As System.Data.SqlClient.SqlDataAdapter
    Public WithEvents SqlConnection2 As System.Data.SqlClient.SqlConnection
    Friend WithEvents btnSrchEdEvent As System.Windows.Forms.Button
    Friend WithEvents btnMyCases As System.Windows.Forms.Button
    Friend WithEvents btnReports As System.Windows.Forms.Button
    Friend WithEvents btnSrchOrg As System.Windows.Forms.Button
    '   Friend WithEvents sqlUsrInfo As System.Data.SqlClient.SqlCommand
    Friend WithEvents pnlRedBar As System.Windows.Forms.Panel
    Friend WithEvents btnSrchGrant As System.Windows.Forms.Button
    Friend WithEvents btnSrchResource As System.Windows.Forms.Button
    Friend WithEvents pbLogo As System.Windows.Forms.PictureBox
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents HelpProvider1 As System.Windows.Forms.HelpProvider
    Friend WithEvents lblWelcome As System.Windows.Forms.Label
    ' Friend WithEvents SqlConnection1 As System.Data.SqlClient.SqlConnection
    Friend WithEvents SqlSelectCommand1 As System.Data.SqlClient.SqlCommand
    ' Friend WithEvents daspGetStaff As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents SqlSelectCommand2 As System.Data.SqlClient.SqlCommand
    '  Friend WithEvents daspGetStatus As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents lblVersion As System.Windows.Forms.Label
    Friend WithEvents btnIntranet As System.Windows.Forms.Button
    '  Shared WithEvents Timer1 As System.Windows.Forms.Timer

    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmStart))
        Me.pnlRedBar = New System.Windows.Forms.Panel()
        Me.lblWelcome = New System.Windows.Forms.Label()
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.lblVersion = New System.Windows.Forms.Label()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.btnIntranet = New System.Windows.Forms.Button()
        Me.btnReports = New System.Windows.Forms.Button()
        Me.btnSrchGrant = New System.Windows.Forms.Button()
        Me.btnSrchOrg = New System.Windows.Forms.Button()
        Me.btnMyCases = New System.Windows.Forms.Button()
        Me.btnSrchEdEvent = New System.Windows.Forms.Button()
        Me.btnSrchResource = New System.Windows.Forms.Button()
        Me.btnHelp = New System.Windows.Forms.Button()
        Me.pbLogo = New System.Windows.Forms.PictureBox()
        Me.HelpProvider1 = New System.Windows.Forms.HelpProvider()
        Me.SqlSelectCommand1 = New System.Data.SqlClient.SqlCommand()
        Me.SqlSelectCommand2 = New System.Data.SqlClient.SqlCommand()
        Me.pb = New System.Windows.Forms.ProgressBar()
        Me.Process1 = New System.Diagnostics.Process()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblCase1 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.pbHeading = New System.Windows.Forms.PictureBox()
        Me.lblCase2 = New System.Windows.Forms.Label()
        Me.lblContact2 = New System.Windows.Forms.Label()
        Me.lblContact3 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.ConstructionLbl = New System.Windows.Forms.Label()
        Me.ConstructionImage = New System.Windows.Forms.PictureBox()
        Me.pnlRedBar.SuspendLayout()
        CType(Me.pbLogo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbHeading, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ConstructionImage, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlRedBar
        '
        Me.pnlRedBar.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlRedBar.BackColor = System.Drawing.Color.Brown
        Me.pnlRedBar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlRedBar.Controls.Add(Me.lblWelcome)
        Me.pnlRedBar.Controls.Add(Me.lblStatus)
        Me.pnlRedBar.Controls.Add(Me.lblVersion)
        Me.pnlRedBar.Location = New System.Drawing.Point(1, 598)
        Me.pnlRedBar.Name = "pnlRedBar"
        Me.pnlRedBar.Size = New System.Drawing.Size(1090, 29)
        Me.pnlRedBar.TabIndex = 17
        '
        'lblWelcome
        '
        Me.lblWelcome.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblWelcome.BackColor = System.Drawing.Color.Transparent
        Me.lblWelcome.Font = New System.Drawing.Font("Times New Roman", 9.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblWelcome.ForeColor = System.Drawing.Color.DarkGray
        Me.lblWelcome.Location = New System.Drawing.Point(426, 4)
        Me.lblWelcome.Margin = New System.Windows.Forms.Padding(0)
        Me.lblWelcome.Name = "lblWelcome"
        Me.lblWelcome.Size = New System.Drawing.Size(276, 19)
        Me.lblWelcome.TabIndex = 26
        Me.lblWelcome.Text = "LabelWelcome"
        Me.lblWelcome.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.ToolTip1.SetToolTip(Me.lblWelcome, "Welcome")
        '
        'lblStatus
        '
        Me.lblStatus.AutoSize = True
        Me.lblStatus.ForeColor = System.Drawing.Color.DarkGray
        Me.lblStatus.Location = New System.Drawing.Point(12, 7)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(22, 13)
        Me.lblStatus.TabIndex = 60
        Me.lblStatus.Text = "     "
        '
        'lblVersion
        '
        Me.lblVersion.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblVersion.ForeColor = System.Drawing.Color.White
        Me.lblVersion.Location = New System.Drawing.Point(917, 4)
        Me.lblVersion.Name = "lblVersion"
        Me.lblVersion.Size = New System.Drawing.Size(154, 22)
        Me.lblVersion.TabIndex = 36
        Me.lblVersion.Text = "&version "
        Me.lblVersion.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnIntranet
        '
        Me.btnIntranet.BackColor = System.Drawing.SystemColors.InactiveBorder
        Me.btnIntranet.BackgroundImage = CType(resources.GetObject("btnIntranet.BackgroundImage"), System.Drawing.Image)
        Me.btnIntranet.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnIntranet.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnIntranet.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnIntranet.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnIntranet.Location = New System.Drawing.Point(485, 459)
        Me.btnIntranet.Name = "btnIntranet"
        Me.btnIntranet.Size = New System.Drawing.Size(145, 95)
        Me.btnIntranet.TabIndex = 14
        Me.btnIntranet.Text = "         "
        Me.ToolTip1.SetToolTip(Me.btnIntranet, "Payroll Timesheets, Travel Expense, Request IT Assistance, etc.")
        Me.btnIntranet.UseVisualStyleBackColor = False
        '
        'btnReports
        '
        Me.btnReports.BackColor = System.Drawing.SystemColors.InactiveBorder
        Me.btnReports.BackgroundImage = CType(resources.GetObject("btnReports.BackgroundImage"), System.Drawing.Image)
        Me.btnReports.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnReports.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReports.ForeColor = System.Drawing.SystemColors.ControlText
        Me.HelpProvider1.SetHelpString(Me.btnReports, "this includes Lilly stats, current Staff load, Recommendation summaries, CRG term" & _
        "inology ")
        Me.btnReports.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnReports.Location = New System.Drawing.Point(736, 300)
        Me.btnReports.Name = "btnReports"
        Me.HelpProvider1.SetShowHelp(Me.btnReports, True)
        Me.btnReports.Size = New System.Drawing.Size(145, 95)
        Me.btnReports.TabIndex = 12
        Me.btnReports.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ToolTip1.SetToolTip(Me.btnReports, "Statistical Reports, Mailing labels, Library labels, Maps." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Right click to open M" & _
        "ailing Label window directly.")
        Me.btnReports.UseVisualStyleBackColor = False
        '
        'btnSrchGrant
        '
        Me.btnSrchGrant.BackColor = System.Drawing.SystemColors.InactiveBorder
        Me.btnSrchGrant.BackgroundImage = CType(resources.GetObject("btnSrchGrant.BackgroundImage"), System.Drawing.Image)
        Me.btnSrchGrant.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnSrchGrant.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSrchGrant.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnSrchGrant.Location = New System.Drawing.Point(736, 119)
        Me.btnSrchGrant.Name = "btnSrchGrant"
        Me.btnSrchGrant.Size = New System.Drawing.Size(145, 95)
        Me.btnSrchGrant.TabIndex = 6
        Me.btnSrchGrant.Text = "            "
        Me.ToolTip1.SetToolTip(Me.btnSrchGrant, "Find Grants grouped by Status.")
        Me.btnSrchGrant.UseVisualStyleBackColor = False
        '
        'btnSrchOrg
        '
        Me.btnSrchOrg.BackColor = System.Drawing.SystemColors.InactiveBorder
        Me.btnSrchOrg.BackgroundImage = CType(resources.GetObject("btnSrchOrg.BackgroundImage"), System.Drawing.Image)
        Me.btnSrchOrg.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnSrchOrg.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSrchOrg.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSrchOrg.Location = New System.Drawing.Point(485, 119)
        Me.btnSrchOrg.Name = "btnSrchOrg"
        Me.btnSrchOrg.Size = New System.Drawing.Size(145, 95)
        Me.btnSrchOrg.TabIndex = 4
        Me.btnSrchOrg.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        Me.ToolTip1.SetToolTip(Me.btnSrchOrg, "Find our Congregations and People, or add new ones.")
        Me.btnSrchOrg.UseVisualStyleBackColor = False
        '
        'btnMyCases
        '
        Me.btnMyCases.BackColor = System.Drawing.SystemColors.InactiveBorder
        Me.btnMyCases.BackgroundImage = CType(resources.GetObject("btnMyCases.BackgroundImage"), System.Drawing.Image)
        Me.btnMyCases.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnMyCases.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnMyCases.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.btnMyCases.Location = New System.Drawing.Point(226, 119)
        Me.btnMyCases.Name = "btnMyCases"
        Me.btnMyCases.Size = New System.Drawing.Size(145, 95)
        Me.btnMyCases.TabIndex = 2
        Me.btnMyCases.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        Me.ToolTip1.SetToolTip(Me.btnMyCases, "Find Cases grouped by Case Manager and Case Status.")
        Me.btnMyCases.UseVisualStyleBackColor = False
        '
        'btnSrchEdEvent
        '
        Me.btnSrchEdEvent.BackColor = System.Drawing.SystemColors.InactiveBorder
        Me.btnSrchEdEvent.BackgroundImage = CType(resources.GetObject("btnSrchEdEvent.BackgroundImage"), System.Drawing.Image)
        Me.btnSrchEdEvent.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnSrchEdEvent.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSrchEdEvent.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.btnSrchEdEvent.Location = New System.Drawing.Point(485, 300)
        Me.btnSrchEdEvent.Name = "btnSrchEdEvent"
        Me.btnSrchEdEvent.Size = New System.Drawing.Size(145, 95)
        Me.btnSrchEdEvent.TabIndex = 10
        Me.ToolTip1.SetToolTip(Me.btnSrchEdEvent, "Find or add EdEvents and Registrations." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Right click for latest Registration down" & _
        "load information.")
        Me.btnSrchEdEvent.UseVisualStyleBackColor = False
        '
        'btnSrchResource
        '
        Me.btnSrchResource.BackColor = System.Drawing.SystemColors.InactiveBorder
        Me.btnSrchResource.BackgroundImage = CType(resources.GetObject("btnSrchResource.BackgroundImage"), System.Drawing.Image)
        Me.btnSrchResource.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnSrchResource.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSrchResource.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnSrchResource.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSrchResource.Location = New System.Drawing.Point(226, 300)
        Me.btnSrchResource.Name = "btnSrchResource"
        Me.btnSrchResource.Size = New System.Drawing.Size(145, 95)
        Me.btnSrchResource.TabIndex = 8
        Me.ToolTip1.SetToolTip(Me.btnSrchResource, "Find or add Resources (and Recommendations, Feedback)")
        Me.btnSrchResource.UseVisualStyleBackColor = False
        '
        'btnHelp
        '
        Me.btnHelp.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnHelp.BackColor = System.Drawing.Color.OldLace
        Me.btnHelp.BackgroundImage = CType(resources.GetObject("btnHelp.BackgroundImage"), System.Drawing.Image)
        Me.btnHelp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnHelp.FlatAppearance.BorderColor = System.Drawing.Color.White
        Me.btnHelp.FlatAppearance.BorderSize = 0
        Me.btnHelp.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnHelp.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnHelp.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnHelp.Location = New System.Drawing.Point(1017, 12)
        Me.btnHelp.Name = "btnHelp"
        Me.btnHelp.Size = New System.Drawing.Size(43, 57)
        Me.btnHelp.TabIndex = 0
        Me.btnHelp.TabStop = False
        Me.btnHelp.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnHelp.UseVisualStyleBackColor = False
        '
        'pbLogo
        '
        Me.pbLogo.BackColor = System.Drawing.Color.Transparent
        Me.pbLogo.BackgroundImage = CType(resources.GetObject("pbLogo.BackgroundImage"), System.Drawing.Image)
        Me.pbLogo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.pbLogo.Location = New System.Drawing.Point(15, 11)
        Me.pbLogo.Name = "pbLogo"
        Me.pbLogo.Size = New System.Drawing.Size(88, 87)
        Me.pbLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pbLogo.TabIndex = 22
        Me.pbLogo.TabStop = False
        '
        'SqlSelectCommand1
        '
        Me.SqlSelectCommand1.CommandText = "[GetStaff]"
        Me.SqlSelectCommand1.CommandType = System.Data.CommandType.StoredProcedure
        '
        'SqlSelectCommand2
        '
        Me.SqlSelectCommand2.CommandText = "[GetStatus]"
        Me.SqlSelectCommand2.CommandType = System.Data.CommandType.StoredProcedure
        '
        'pb
        '
        Me.pb.Location = New System.Drawing.Point(915, 653)
        Me.pb.Name = "pb"
        Me.pb.Size = New System.Drawing.Size(101, 22)
        Me.pb.TabIndex = 40
        '
        'Process1
        '
        Me.Process1.StartInfo.Domain = ""
        Me.Process1.StartInfo.FileName = "Outlook.exe"
        Me.Process1.StartInfo.LoadUserProfile = False
        Me.Process1.StartInfo.Password = Nothing
        Me.Process1.StartInfo.StandardErrorEncoding = Nothing
        Me.Process1.StartInfo.StandardOutputEncoding = Nothing
        Me.Process1.StartInfo.UserName = ""
        Me.Process1.SynchronizingObject = Me
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(812, 229)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(0, 13)
        Me.Label1.TabIndex = 40
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Verdana", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(788, 219)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(50, 18)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "rants"
        '
        'lblCase1
        '
        Me.lblCase1.AutoSize = True
        Me.lblCase1.BackColor = System.Drawing.Color.Transparent
        Me.lblCase1.Font = New System.Drawing.Font("Verdana", 12.0!, System.Drawing.FontStyle.Underline)
        Me.lblCase1.Location = New System.Drawing.Point(273, 217)
        Me.lblCase1.Margin = New System.Windows.Forms.Padding(0)
        Me.lblCase1.MaximumSize = New System.Drawing.Size(13, 0)
        Me.lblCase1.Name = "lblCase1"
        Me.lblCase1.Size = New System.Drawing.Size(13, 18)
        Me.lblCase1.TabIndex = 1
        Me.lblCase1.Text = "C"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Verdana", 12.0!)
        Me.Label4.Location = New System.Drawing.Point(266, 399)
        Me.Label4.Margin = New System.Windows.Forms.Padding(0, 0, 3, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(80, 18)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "esources"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Verdana", 12.0!)
        Me.Label5.Location = New System.Drawing.Point(542, 398)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(53, 18)
        Me.Label5.TabIndex = 9
        Me.Label5.Text = "vents"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Verdana", 12.0!)
        Me.Label6.Location = New System.Drawing.Point(739, 399)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(143, 18)
        Me.Label6.TabIndex = 11
        Me.Label6.Text = "Reports/  tilities"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Verdana", 12.0!)
        Me.Label7.Location = New System.Drawing.Point(532, 558)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(69, 18)
        Me.Label7.TabIndex = 13
        Me.Label7.Text = "or Staff"
        '
        'pbHeading
        '
        Me.pbHeading.BackgroundImage = CType(resources.GetObject("pbHeading.BackgroundImage"), System.Drawing.Image)
        Me.pbHeading.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.pbHeading.ErrorImage = Nothing
        Me.pbHeading.InitialImage = Nothing
        Me.pbHeading.Location = New System.Drawing.Point(154, 11)
        Me.pbHeading.Name = "pbHeading"
        Me.pbHeading.Size = New System.Drawing.Size(806, 77)
        Me.pbHeading.TabIndex = 49
        Me.pbHeading.TabStop = False
        '
        'lblCase2
        '
        Me.lblCase2.AutoSize = True
        Me.lblCase2.BackColor = System.Drawing.Color.Transparent
        Me.lblCase2.Font = New System.Drawing.Font("Verdana", 12.0!)
        Me.lblCase2.Location = New System.Drawing.Point(284, 217)
        Me.lblCase2.Margin = New System.Windows.Forms.Padding(0)
        Me.lblCase2.Name = "lblCase2"
        Me.lblCase2.Size = New System.Drawing.Size(46, 18)
        Me.lblCase2.TabIndex = 50
        Me.lblCase2.Text = "ases"
        '
        'lblContact2
        '
        Me.lblContact2.AutoSize = True
        Me.lblContact2.BackColor = System.Drawing.Color.Transparent
        Me.lblContact2.Font = New System.Drawing.Font("Verdana", 12.0!, System.Drawing.FontStyle.Underline)
        Me.lblContact2.Location = New System.Drawing.Point(532, 219)
        Me.lblContact2.Margin = New System.Windows.Forms.Padding(0)
        Me.lblContact2.MaximumSize = New System.Drawing.Size(12, 0)
        Me.lblContact2.Name = "lblContact2"
        Me.lblContact2.Size = New System.Drawing.Size(12, 18)
        Me.lblContact2.TabIndex = 52
        Me.lblContact2.Text = "o"
        '
        'lblContact3
        '
        Me.lblContact3.AutoSize = True
        Me.lblContact3.BackColor = System.Drawing.Color.Transparent
        Me.lblContact3.Font = New System.Drawing.Font("Verdana", 12.0!)
        Me.lblContact3.Location = New System.Drawing.Point(542, 219)
        Me.lblContact3.Margin = New System.Windows.Forms.Padding(0)
        Me.lblContact3.Name = "lblContact3"
        Me.lblContact3.Size = New System.Drawing.Size(59, 18)
        Me.lblContact3.TabIndex = 53
        Me.lblContact3.Text = "ntacts"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Verdana", 12.0!, System.Drawing.FontStyle.Underline)
        Me.Label3.Location = New System.Drawing.Point(810, 399)
        Me.Label3.Margin = New System.Windows.Forms.Padding(0)
        Me.Label3.MaximumSize = New System.Drawing.Size(14, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(14, 18)
        Me.Label3.TabIndex = 54
        Me.Label3.Text = "U"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Font = New System.Drawing.Font("Verdana", 12.0!, System.Drawing.FontStyle.Underline)
        Me.Label8.Location = New System.Drawing.Point(522, 558)
        Me.Label8.Margin = New System.Windows.Forms.Padding(0)
        Me.Label8.MaximumSize = New System.Drawing.Size(12, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(12, 18)
        Me.Label8.TabIndex = 55
        Me.Label8.Text = "F"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Font = New System.Drawing.Font("Verdana", 12.0!, System.Drawing.FontStyle.Underline)
        Me.Label9.Location = New System.Drawing.Point(255, 399)
        Me.Label9.Margin = New System.Windows.Forms.Padding(0)
        Me.Label9.MaximumSize = New System.Drawing.Size(13, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(13, 18)
        Me.Label9.TabIndex = 56
        Me.Label9.Text = "R"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Font = New System.Drawing.Font("Verdana", 12.0!, System.Drawing.FontStyle.Underline)
        Me.Label10.Location = New System.Drawing.Point(532, 397)
        Me.Label10.Margin = New System.Windows.Forms.Padding(0)
        Me.Label10.MaximumSize = New System.Drawing.Size(12, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(12, 18)
        Me.Label10.TabIndex = 57
        Me.Label10.Text = "E"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.Color.Transparent
        Me.Label11.Font = New System.Drawing.Font("Verdana", 12.0!, System.Drawing.FontStyle.Underline)
        Me.Label11.Location = New System.Drawing.Point(775, 218)
        Me.Label11.Margin = New System.Windows.Forms.Padding(0)
        Me.Label11.MaximumSize = New System.Drawing.Size(14, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(14, 18)
        Me.Label11.TabIndex = 58
        Me.Label11.Text = "G"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.Color.Transparent
        Me.Label12.Font = New System.Drawing.Font("Verdana", 12.0!)
        Me.Label12.Location = New System.Drawing.Point(522, 219)
        Me.Label12.Margin = New System.Windows.Forms.Padding(0)
        Me.Label12.MaximumSize = New System.Drawing.Size(13, 0)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(13, 18)
        Me.Label12.TabIndex = 59
        Me.Label12.Text = "C"
        '
        'PictureBox1
        '
        Me.PictureBox1.BackgroundImage = CType(resources.GetObject("PictureBox1.BackgroundImage"), System.Drawing.Image)
        Me.PictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.PictureBox1.ErrorImage = Nothing
        Me.PictureBox1.InitialImage = Nothing
        Me.PictureBox1.Location = New System.Drawing.Point(154, 11)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(806, 77)
        Me.PictureBox1.TabIndex = 49
        Me.PictureBox1.TabStop = False
        '
        'ConstructionLbl
        '
        Me.ConstructionLbl.AutoSize = True
        Me.ConstructionLbl.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ConstructionLbl.Location = New System.Drawing.Point(696, 545)
        Me.ConstructionLbl.MaximumSize = New System.Drawing.Size(400, 0)
        Me.ConstructionLbl.MinimumSize = New System.Drawing.Size(300, 50)
        Me.ConstructionLbl.Name = "ConstructionLbl"
        Me.ConstructionLbl.Size = New System.Drawing.Size(300, 50)
        Me.ConstructionLbl.TabIndex = 68
        Me.ConstructionLbl.Text = "FYI - Currently Under Construction:"
        Me.ConstructionLbl.Visible = False
        '
        'ConstructionImage
        '
        Me.ConstructionImage.Image = CType(resources.GetObject("ConstructionImage.Image"), System.Drawing.Image)
        Me.ConstructionImage.Location = New System.Drawing.Point(778, 459)
        Me.ConstructionImage.Name = "ConstructionImage"
        Me.ConstructionImage.Size = New System.Drawing.Size(83, 75)
        Me.ConstructionImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.ConstructionImage.TabIndex = 69
        Me.ConstructionImage.TabStop = False
        Me.ConstructionImage.Visible = False
        '
        'frmStart
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1084, 627)
        Me.Controls.Add(Me.ConstructionImage)
        Me.Controls.Add(Me.ConstructionLbl)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.lblContact2)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.lblCase1)
        Me.Controls.Add(Me.pbHeading)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.btnHelp)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnIntranet)
        Me.Controls.Add(Me.btnReports)
        Me.Controls.Add(Me.pbLogo)
        Me.Controls.Add(Me.btnSrchGrant)
        Me.Controls.Add(Me.btnSrchOrg)
        Me.Controls.Add(Me.btnMyCases)
        Me.Controls.Add(Me.btnSrchEdEvent)
        Me.Controls.Add(Me.btnSrchResource)
        Me.Controls.Add(Me.lblCase2)
        Me.Controls.Add(Me.lblContact3)
        Me.Controls.Add(Me.pnlRedBar)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(30, 30)
        Me.Name = "frmStart"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Tag = "STARTUP"
        Me.Text = "InfoCtr"
        Me.pnlRedBar.ResumeLayout(False)
        Me.pnlRedBar.PerformLayout()
        CType(Me.pbLogo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbHeading, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ConstructionImage, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region  'windows

#Region "LOAD/CLOSE"

    'LOAD - LOAD GLOBAL DATA, LOGIN, Set up this form
    Protected Sub frmStart_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        modGlobalVar.MouseWait()

SecondInstance:
        'CHANGED 2015  Do not ask about multiple instances, simply open the running instance
        If (Environ("Computername")).Contains("RDP") Or System.Diagnostics.Debugger.IsAttached = True Then  'don't ask if are using terminal server, as someone else's instance is counted '08RDP
        Else
            If UBound(Diagnostics.Process.GetProcessesByName(Diagnostics.Process.GetCurrentProcess.ProcessName)) > 0 Then
                ActivatePrevInstance(Me.Text) 'TEXT_PROPERTY_OF_OPENING_FORM)
            End If
        End If

        strVersionDt = File.GetLastWriteTime(DeployLocation & "publish.htm").ToShortDateString 'gets date from .exe on cg machine
SetTitle:
        If System.Diagnostics.Debugger.IsAttached = False Then
            ' strVersionDt = File.GetLastWriteTime(System.Deployment.Application.ApplicationDeployment.CurrentDeployment.ActivationUri.AbsolutePath.ToString()).ToShortDateString never tested
            Dim CD As ApplicationDeployment = ApplicationDeployment.CurrentDeployment 'doesn't work in debug mode
            '  strVersionDt = File.GetLastWriteTime(System.Reflection.Assembly.GetExecutingAssembly.Location).ToShortDateString 'gets date from .exe on cg machine
            strVersion = IsNull(CD.CurrentVersion.Major.ToString, "debug") & "." & IsNull(CD.CurrentVersion.Build.ToString, "") & "." & IsNull(CD.CurrentVersion.Revision.ToString, "")
            Me.lblVersion.Text = "Version : " & IsNull(strVersionDt, "no date") & " " & IsNull(strVersion, "no version") 'CD.CurrentVersion.Major.ToString & "." & CD.CurrentVersion.Revision.ToString 'My.Application.Deployment.CurrentVersion.ToString()
        Else 'is debug
            Me.lblVersion.Text = "TESTING " & System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString & " " &
                "last deployed: " & strVersionDt
        End If

SetUserVariables:
        'SET USER VARIABLES, DO LOGIN
        Me.lblStatus.Text = "checking login"
        If modGlobalVar.GetUser() Then   'sets region, staffID, Staffname and inserts into LoginTable

        Else
            sc.Close()
            Me.Close()
            Exit Sub
        End If

        'Check for Previous Instance else some data congruency issues (see Nancy D conversations not saved)
        '.....................................................
        'AARON - revove these 7 lines if multiple users can't open InfoCtr
        ' modGlobalVar.Msg(Environ("ComputerName"), , Environ("UserName"))
        '    Dim style As modGlobalVar.MsgStyle
        '    style = modGlobalVar.MsgStyle.DefaultButton2 Or modGlobalVar.MsgStyle.Question Or MessageBoxButtons.YesNo

        '  modGlobalVar.Msg("not found", , Diagnostics.Process.GetCurrentProcess.ProcessName)
        'End If
        '.....................................................................
SetGlobals:

        'NOTE: thread doesn't work fast enough. FOrms will open with no CRG or no Case Status
        'Thrd = New Thread(AddressOf ThreadTask)
        'Thrd.IsBackground = True
        'Thrd.Start()

        'load collections and datasets
        Me.lblStatus.Text = "loading datasets"
        LoadDatasets()
        Me.lblStatus.Text = "setting region"
        cmdRegion.CommandType = CommandType.StoredProcedure
        cmdRegion.Parameters.Add("@ID", SqlDbType.Int)
        Me.lblStatus.Text = "region params done"
        'check for 'under construction, then check again every hour
        UnderConstruction_Tick()
        StartUnderConstructionTimer()
        Me.lblStatus.Text = "timer done"
Personalize:
        Me.lblWelcome.Text = "Welcome " & usrFirst
        '....................................................
        modGlobalVar.CreateShortcut()
        modGlobalVar.MouseDefault()
        Me.lblStatus.Text = "Ready"
        Forms.Add(Me)
    End Sub

    'START TIMER
    Private Sub StartUnderConstructionTimer()
        Timer1 = New System.Windows.Forms.Timer
        Timer1.Interval = 3600000 ' = 1 hr
        'Timer1.Enabled = False
        Timer1.Start()
        AddHandler Timer1.Tick, AddressOf UnderConstruction_Tick
        '  1000 = 1 sec
        '60000 = 1 min

    End Sub

    'SET MESSAGE VISIBLITY
    Public Sub UnderConstruction_Tick() ' Handles timer1.Tick

        Dim sql As New SqlCommand("SELECT LastNumUsed, Description FROM luTokenTbl WHERE What = 'UnderConstruction'", sc)
        Dim tblMsg As New System.Data.DataTable ' = New DataTable

        Dim b As Boolean
        modGlobalVar.LoadDataTable(tblMsg, sql)
        b = tblMsg.Rows(0)(0)
        Me.ConstructionImage.Visible = b
        Me.ConstructionLbl.Visible = b
        If b = True Then
            Me.ConstructionLbl.Text = "FYI - Currently Under Construction:" & NextLine & tblMsg.Rows(0)("Description").ToString
        End If
        tblMsg = Nothing
    End Sub

    'PREVENT USER FROM OPENING MULTIPLE INSTANCES
    'TODO what if want multiple instances open?
    Private Function ActivatePrevInstance(ByVal argStrAppToFind As String) As Boolean
        Dim PrevHndl As Long
        Dim result As Long
        Dim objProcess As New Process 'Variable to hold individual Process
        Dim objProcesses() As Process 'Collection of all the Processes running on local machine
        objProcesses = Process.GetProcesses() ''Get all processes into the  collection()

        For Each objProcess In objProcesses
            ' modGlobalVar.Msg(UCase(objProcess.MainWindowTitle), , UCase(argStrAppToFind))
            ''Check and exit if we have SMS running already
            If UCase(objProcess.MainWindowTitle) = UCase(argStrAppToFind) Then
                '  modGlobalVar.Msg("Another instance of " & argStrAppToFind & " is already running on this machine. Opening other instance.")
                PrevHndl = objProcess.MainWindowHandle.ToInt32()
                Exit For
            End If
        Next

        If PrevHndl = 0 Then
            Exit Function 'if No previous instance found exit the application.
        Else            ''If found
            Try
                result = OpenIcon(PrevHndl) 'Restore the program.
                result = SetForegroundWindow(PrevHndl) 'Activate the application.
                MouseDefault()
                End 'End the current instance of the application.
            Catch ex As ExternalException
                ' p = Diagnostics.Process.GetProcessesByName(Diagnostics.Process.GetCurrentProcess.ProcessName)(1) 'while testing
                objProcess.Kill()
                'old one won't open, so either is stuck in Task Mgr
                'or this is RDP machine and is ok to open another instance as previous isntance might be from other use
            End Try
        End If
        'http://www.devx.com/tips/Tip/20044
    End Function

    'LOADING DATASETS for comboboxes
    Private Sub LoadDatasets()

CaseStatusLookupTable:
        modGlobalVar.LoadCaseStatusDS()
        '   UpdateProgressBar(10, "Cases Status Loaded")

CommonCollections:
        'LOAD COLLECTIONS
        modGlobalVar.CreateRegionCol()
        modGlobalVar.CreateCountyCol()
        '  CreateModeCol()
        modGlobalVar.CreateStaffCol()
        ' UpdateProgressBar(30, "Collections Loaded")
        modGlobalVar.LoadConversMode()

CreateCRGDataset:
        modGlobalVar.LoadCRGtbl()

        ' UpdateProgressBar(20, "CRG Loaded")
Resources:
        'IndexCollections
        modGlobalVar.CreateIndexArl()
        'Resource Types
        modGlobalVar.CreateResourceTypeCol()
        modGlobalVar.LoadResourceInactiveDS()

    End Sub 'load datasets

    'TODO ASK USER TO SAVE
    'Exiting program bypasses onclosing for forms thereby discarding changes without asking user
    Private Sub frmStart_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Dim x, y As Integer
        Dim f As Form

CloseOpenForms:  'and ask if data needs saving
        y = System.Windows.Forms.Application.OpenForms.Count
        For x = y - 1 To 1 Step -1
            f = System.Windows.Forms.Application.OpenForms(x)
            '  If f.Name.Substring(3, 4) = "Main" Then
            Try
                f.Close()
            Catch ex As System.Exception
            End Try
            ' End If
        Next x
        y = System.Windows.Forms.Application.OpenForms.Count
        '  Sleep(1000)
        If y > 1 Then
            System.Windows.Forms.Application.OpenForms(1).Focus()
            msg("Delay in Closing", "Please check " & System.Windows.Forms.Application.OpenForms(1).Tag & " DETAIL window for errors or missing information before closing the Information Center", MessageBoxButtons.OK, MessageBoxIcon.Information)
            e.Cancel = True
        Else
            e.Cancel = False
        End If

LogOut:
        If e.Cancel = False Then
            Try
                modGlobalVar.LogOut()
            Catch ex As System.Exception
            End Try
        End If
        'System.Windows.Forms.Application.Exit()'should not be necessary

    End Sub

#End Region 'Load

#Region "Open Form Buttons"

    'OPEN SINGLE INSTANCE OF FORM
    Private Sub OpenSingleForm(ByVal frm As Form, ByVal txt As String)
        MouseWait()
        Me.lblStatus.Text = "Opening: " & txt & " form."
        Try
            Forms.find(frm, True)
        Catch ex As System.Exception
            modGlobalVar.msg("ERROR: opening form :" & frm.Name, ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
Finish:
        Me.lblStatus.Text = "Done"
        MouseDefault()
    End Sub

    'OPEN SEARCH FOR ORGANIZATIONS/PEOPLE
    Private Sub btnSrchOrg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSrchOrg.Click
        OpenSingleForm(frmSrchOrg, "Organization/People Search")
    End Sub

    'OPEN SEARCH FOR USER'S CASES
    Private Sub btnMyCases_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnMyCases.Click, lblCase1.Click
        OpenSingleForm(frmSrchCase, "Case Search")
    End Sub

    'OPEN STAFF FORMS FORM
    Private Sub btnIntranet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnIntranet.Click
        OpenSingleForm(frmIntranet, "Staff ")
    End Sub

    'OPEN GRANT SEARCH
    Private Sub btnSrchGrant_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSrchGrant.Click
        OpenSingleForm(frmSrchGrant, "Grant Search ")
    End Sub

    'OPEN RESOURCE SEARCH
    Private Sub btnSrchResource_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSrchResource.Click
        OpenSingleForm(frmSrchResource, "Resource Search ")
    End Sub

    'RIGHT Click open Mail List form
    Private Sub btnReports_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) _
        Handles btnReports.MouseDown
        If e.Button = System.Windows.Forms.MouseButtons.Right Then
            OpenSingleForm(frmMailLabels, "Mailing ")
        End If
    End Sub

    'OPEN SWITCHBOARD for REPORTS & UTILITIES like LIB LABELS
    Private Sub btnReports_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReports.Click
        OpenSingleForm(frmSwitchboard, "Reports & Utilities ")
    End Sub

    'OPEN SEARCH FOR ED EVENT  FORM
    Private Sub btnSrchEdEvent_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSrchEdEvent.Click
        OpenSrchWEvent(0, False)
        Me.lblStatus.Text = "Done"
    End Sub

    'RIGHT Click vew Last Download Date/time
    Private Sub btnSrchEdEvent_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles btnSrchEdEvent.MouseDown

        If e.Button = System.Windows.Forms.MouseButtons.Right Then
        Else
            Exit Sub
        End If

        Dim j As Integer
        Dim k As Date
        Dim sql As New SqlCommand()
        sql.Connection = sc

        If Not SCConnect() Then
            Exit Sub
        End If
        sql.CommandText = "SELECT MAX(OrderID) AS Expr1 FROM tblEventRegOrder2 WHERE (Source LIKE 'Regonline%')"
        j = sql.ExecuteScalar
        sql.CommandText = "Select DateLastSuccess FROM internalMisc_be.dbo.SSISTokens WHERE (PackageName = 'RegOnline')"
        k = sql.ExecuteScalar
        sc.Close()
        modGlobalVar.msg("Latest Registrations:",
          "Last RegOnline Download: " & File.GetLastWriteTime("\\ICCNAS1\users\shared\website db\CustomCrossEventReport2016725.xls") & NextLine &
        "Last RegOnline Import: " & k.ToString & NextLine &
        "Last RegOnline Order #: " & j.ToString & NextLine, MessageBoxButtons.OK, MessageBoxIcon.Information)

        sql = Nothing
    End Sub

    'SHORTCUT to BUTTONS; since removed text on buttons 12/2014, alt+ no longer worked
    Private Sub frmStart_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown

        Select Case e.KeyCode
            Case Is = Keys.C ', Keys.C AndAlso e.Modifiers = Keys.Alt
                Me.btnMyCases.PerformClick()
            Case Is = Keys.O
                Me.btnSrchOrg.PerformClick()
            Case Is = Keys.E
                Me.btnSrchEdEvent.PerformClick()
            Case Is = Keys.R
                Me.btnSrchResource.PerformClick()
            Case Is = Keys.U
                Me.btnReports.PerformClick()
            Case Is = Keys.G
                Me.btnSrchGrant.PerformClick()
            Case Is = Keys.F
                Me.btnIntranet.PerformClick()
            Case Is = Keys.Alt, Keys.Menu 'menu = alt
            Case Else
                ' modGlobalVar.Msg(e.KeyCode.ToString, , "not an option")
        End Select

    End Sub

#End Region 'open form buttons

#Region "General"

    'UNHANDLED EXCEPTION EMAIL
    Public Sub Me_UnhandledException(ByVal sender As Object, ByVal e As UnhandledExceptionEventArgs) _
        Handles Me.UnhandledException
        Dim SendEmail As New ClassEmail
        SendEmail.SendHTMLEmail(DBAdmin.StaffEmail, "unhandled exception", "<b>who: </b>" + IsNull(usrName, "error before got usrname") + "<br><b>What:</b> " + e.ExceptionObject.ToString + "<br><b>Msg: </b>" + e.ExceptionObject.exception.message())
        e.ExceptionObject.exception.message()
    End Sub

    'btn Help
    Private Sub btnHelp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHelp.Click

        Dim pp As ContextMenu = New ContextMenu

        pp.MenuItems.Clear()
        'LOAD POPUPMENU
        pp.MenuItems.Add("Install Upgrade to Information Center if one is available.", AddressOf HelpPopup)
        pp.MenuItems.Add("Request Tech Support Ticket", AddressOf HelpPopup)
        pp.MenuItems.Add("SEND BULK EMAIL, preset with " & DBAdmin.StaffName, AddressOf HelpPopup)
        pp.MenuItems.Add("For additional Help", AddressOf HelpPopup)
        'pp.MenuItems(0).DefaultItem = True
        pp.Show(Me, PointToClient(Control.MousePosition))
    End Sub

    'process help menu
    Private Sub HelpPopup(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Select Case sender.text
            Case Is = "Install Upgrade to Information Center if one is available."
                RunUpgrade()
            Case Is = "Request Tech Support Ticket"
                Dim GetURL As String
                Dim cmd As New SqlCommand("SELECT url from tblIntranet WHERE FormName = 'Support Requests'", sc)
                If Not SCConnect() Then
                    Exit Sub
                End If
                Try
                    GetURL = cmd.ExecuteScalar
                    System.Diagnostics.Process.Start(GetURL)
                Catch ex As ExternalException
                Finally
                    sc.Close()
                End Try
            Case Is = "For additional Help"
                msg("For Additional Help:", "click 'For Staff'  button" + NextLine +
                    "   and change the dropdown box to 'How to...' or 'Information Center Help'", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Case Is = "SEND BULK EMAIL, preset with " & DBAdmin.StaffName
                Dim strsql As New SqlCommand("SELECT * FROM tmpBulkEmail", sc)
                strsql.CommandType = CommandType.Text
                OpenEmailForm(strsql, "SEND EMAIL Test data", True)
        End Select
    End Sub

    'force upgrade
    Private Sub RunUpgrade()
        Try
            System.Diagnostics.Process.Start(SharedPath & "VisualStudioApps\InformationCenter\publish.htm")
        Catch ex As System.Exception
            msg("Error - Upgrade", ex.Message & NextLine & SharedPath, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region    'general

#Region "Testing"

    'use for testing lblWelcome
    Private Sub lblWelcome_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles lblWelcome.Click

    End Sub

    'use for testing - lbltitle
    Private Sub lblTitle_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim frm As New frmReviewOrgSimple
        frm.Show()
    End Sub

    'use for testing - Logo
    Private Sub pbLogo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles pbLogo.Click
        '---TEST DELIVRA EMAIL ===========
        Dim strsql As New SqlCommand("dbo.MailUserFlagList", sc)
        strsql.CommandType = CommandType.StoredProcedure
        strsql.Parameters.Add("@FileType", SqlDbType.VarChar).Value = "email"
        strsql.Parameters.Add("@MailFlag", SqlDbType.VarChar).Value = "DelivraTest"
        OpenEmailForm(strsql, "SEND EMAIL Test data")
    End Sub

    ''use for testing - Logo
    'Private Sub pbLogo_doubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) _
    '        Handles pbLogo.DoubleClick
    '    '---TEST DELIVRA EMAIL ===========
    '    Dim strsql As New SqlCommand("SELECT * FROM tmpBulkEmail", sc)
    '    strsql.CommandType = CommandType.Text
    '    OpenEmailForm(strsql, "SEND EMAIL Test data", True)
    'End Sub

    'pbheading
    Private Sub pbHeading_Click(sender As System.Object, e As System.EventArgs) Handles pbHeading.Click
        '======='WORKS !! USES ~$FileName =============
        'returns owner of ~$file even if ~$file not found by dir
        Dim secUtil As Object
        Dim secDesc As Object
        Dim strFileName As String
        strFileName = InputBox("", "enter file name", "~$TestFileAccess.xlsm")
        Try
            secUtil = CreateObject("ADsSecurityUtility")
            secDesc = secUtil.GetSecurityDescriptor(LinkedPath & "Events\" & strFileName, 1, 1)
            MsgBox(secDesc.owner, , "~$ open")
        Catch ex As System.Exception
            secDesc = secUtil.GetSecurityDescriptor(LinkedPath & "Events\" & strFileName.Substring(2), 1, 1)
            MsgBox(secDesc.owner, , "creator")
        End Try
        '========================
    End Sub

#End Region 'testing

End Class




