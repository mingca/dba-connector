Public Class frmReportViewer

    Private Sub frmReportViewer_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'dsRptsReg.ReportsDrupal' table. You can move, or remove it, as needed.
        Me.dsRptsReg.EnforceConstraints = False
        Me.ReportsDrupalTableAdapter.Fill(Me.dsRptsReg.ReportsDrupal, "Refund")

        Me.ReportViewer1.RefreshReport()
    End Sub

   
End Class