Imports System.Windows.Forms

'MSG BOX THAT CLOSES ITSELF cg 2/16
'to call:    Dim frm As New DialogSelfClose("message goes here")
Public Class DialogSelfClose
    Private WithEvents tm As New Timer
    Public txt As String
    Dim x As Integer = 0

    Public Sub New(ByVal txt As String) 'DialogSelfClose_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        InitializeComponent()

        Me.lblMessage.Text = txt
        tm.Interval = 1000
        tm.Start()
        Me.ShowDialog()

    End Sub

    Private Sub tm_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles tm.Tick
        x = x + 1000
        If x >= 3000 Then
            Close()
        Else
            Me.lblCountdown.Text = "Closing in: " & (3000 - x) / 1000.ToString
        End If
    End Sub


End Class
