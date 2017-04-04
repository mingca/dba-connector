Public Class rptViewerRegistration
    Public ID As Integer

    Private Sub rptViewerRegistration_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'dsrptRegistration.RptRegistrations' table. You can move, or remove it, as needed.
        Me.dsrptRegistration.EnforceConstraints = False
        Me.RptRegistrationsTableAdapter.Fill(Me.dsrptRegistration.RptRegistrations, ID)

        'Me.ReportViewer1.RefreshReport()
        ' Me.vwrbyOrg.RefreshReport()
    End Sub

    Private Sub cboSelect_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) _
        Handles cboSelect.SelectedIndexChanged

        'if don't use it, can't change forms
        'Dim rptSource1 As New Microsoft.Reporting.WinForms.ReportDataSource
        'rptSource1.name = "myProj_EsnItems"
        'rptSource1.value = Me.BindingSource1
        'Me.ReportViewer1.LocalReport.DataSources.Add(rptSource1)
        MouseWait()

        Me.vwrChecklist.Reset() 'if use this need to reset datasource instance
        Dim rptSource1 As New Microsoft.Reporting.WinForms.ReportDataSource
        rptSource1.Name = "dsrptRegistration_RptRegistrations"
        rptSource1.Value = Me.RptRegistrationsBindingSource
        Me.vwrChecklist.LocalReport.DataSources.Add(rptSource1)
        'Me.RptRegistrationsBindingSource.DataSource = "InfoCtr.dsrptRegistration"
        'modGlobalVar.Msg(Me.RptRegistrationsBindingSource.DataSource.ToString)


        Select Case cboSelect.SelectedItem
            Case "Check-In List"
                Me.vwrChecklist.LocalReport.ReportEmbeddedResource = "InfoCtr.rptRegistrationChecklist.rdlc"
                ' Me.vwrChecklist.BringToFront()

                ' Me.vwrChecklist.Visible = True
                ' Me.vwrbyOrg.Visible = False
                'Me.vwrOverview.Visible = True
                '  Case "ChecklistNew"
                '      Me.vwrChecklist.LocalReport.ReportEmbeddedResource = "InfoCtr.rptRegistrationCheck2008.rdlc"
            Case "Roster Handout"
                Me.vwrChecklist.LocalReport.ReportEmbeddedResource = "InfoCtr.rptRegistrationRoster.rdlc"

                'Me.vwrChecklist.Visible = False
                ' Me.vwrbyOrg.Visible = True
                'Me.vwrOverview.Visible = False
                'Me.vwrbyOrg.RefreshReport()
            Case "Overview"
                Me.vwrChecklist.LocalReport.ReportEmbeddedResource = "InfoCtr.rptRegistrationOverview.rdlc"
                ' Me.vwrChecklist.BringToFront()
                'Me.vwrOverview.Visible = True
                'Me.vwrOverview.RefreshReport()
                'Me.vwrbyOrg.Visible = False
                'Me.vwrChecklist.Visible = True
                'Me.vwrChecklist.RefreshReport()
            Case "Cancellation Phone List"
                Me.vwrChecklist.LocalReport.ReportEmbeddedResource = "InfoCtr.rptRegistrationCancellation.rdlc"

        End Select

        Me.vwrChecklist.RefreshReport()
        ''add these 2 lines as workaround for 'known bug' not printing first time
        'Try
        '    Me.vwrChecklist.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout)
        '    Me.vwrChecklist.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.PageWidth
        '    'another suggested workaround: manually enter page numbers to print
        'Catch ex As Exception
        'End Try
        MouseDefault()
    End Sub

End Class