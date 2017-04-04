Public Class rptViewerStatsGrant
    Public dtStart As Date
    Public dtEnd As Date
    Public strRegion As String
    Public strQuery As String


    Private Sub rptViewerStatsGrant_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.dsStatsGrant.EnforceConstraints = False
        Me.StatsGrantsTableAdapter.Fill(Me.dsStatsGrant.StatsGrants, dtStart, dtEnd, strRegion, strQuery)
        Me.vwrGrant.RefreshReport()
        'add these 2 lines as workaround for 'known bug' not printing first time
        Me.vwrGrant.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout)
        Me.vwrGrant.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.PageWidth
        'another suggested workaround: manually enter page numbers to print
    End Sub

End Class