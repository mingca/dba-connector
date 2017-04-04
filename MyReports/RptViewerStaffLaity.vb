Public Class RptViewerStaffLaity

    Public strOrgID As String

    Private Sub RptViewerStaffLaity_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' iOrgID = "14"
        'TODO: This line of code loads data into the 'dsStaffLaity.tbStaffLaity' table. You can move, or remove it, as needed.
        Me.dsStaffLaity.EnforceConstraints = False
        Me.taStaffLaity.Fill(Me.dsStaffLaity.tbStaffLaity, strOrgID)

        Me.ReportViewer1.RefreshReport()
    End Sub

    Private Sub ReportViewer1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ReportViewer1.Load

    End Sub
End Class