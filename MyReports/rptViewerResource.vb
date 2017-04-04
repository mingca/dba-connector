Public Class rptViewerResource

    'these 3 public variables allow to print parameters on report.  Is there a better way?  These do not show up if no records are found; it would be better if they did show up.
    Public dtStart As Date
    Public dtEnd As Date
    Public strRegion As String
    Public rptResourceName As String
    Private dsName As DataSet



    Private Sub rptViewerResource_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Select Case rptResourceName
            Case Is = "QuarterlyRecommendations"
                'QuarterlyRecommendations:
                Me.ReportViewer1.LocalReport.ReportEmbeddedResource = "InfoCtr.rptRecommendQtr.rdlc"
                Me.DsResourceQtr.EnforceConstraints = False
                Try
                    Me.TblResourceQtrTableAdapter.Fill(Me.DsResourceQtr.tblResourceQtr, dtStart, dtEnd, strRegion)
                Catch ex As Exception
                    modGlobalVar.Msg("ERROR", "can't fill qtr adapter" & NextLine & ex.Message(), MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try

            Case Is = "Full Resource Report"
                '     Me.ReportViewer1.LocalReport.ReportEmbeddedResource = "InfoCtr.rptRecommendQtr.rdlc"
                '   Me.DsResourceQtr.EnforceConstraints = False
                '    Me.TblResourceFullTableAdapter.Fill(Me.DsResourceQtr.tblResourceQtr, dtStart, dtEnd, strRegion)
        End Select


FinishSetup:
        Me.ReportViewer1.RefreshReport()
        'add these 2 lines as  workaround for 'known bug' not printing first time
        Me.ReportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout)
        Me.ReportViewer1.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.PageWidth
        'another suggested workaround: manually enter page numbers to print

        rptResourceName = Nothing
    End Sub


End Class