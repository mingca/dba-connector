Imports Microsoft.Office.Interop
'Imports Microsoft.Office.Interop.Excel
Imports System.Text
Imports System.IO

Public Class frmPopupDatagrid

    Dim strbData As New StringBuilder
    Dim progBar As System.Windows.Forms.ToolStripProgressBar
    Dim strTitle As String

#Region "Initialize"
    Public Sub New(ByVal str As String, ByRef PBar As System.Windows.Forms.ToolStripProgressBar)
        MyBase.New()
        progBar = PBar
        strTitle = str

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try

        Try
            Forms.Remove(Me)
        Catch ex As Exception

        End Try
    End Sub

#End Region 'initialize

#Region "Load"
    'LOAD
    Private Sub frmPopupDatagrid_Load(ByVal sender As Object, ByVal e As System.EventArgs) _
            Handles MyBase.Load
        Forms.Add(Me)
    End Sub

#End Region 'load

#Region "Form Specific"

    'get selected rows of result grid and convert to string
    Private Function TableToString() As Boolean

        If Me.dgvResult.Rows.Count > 0 Then
            If Not Me.dgvResult.SelectedRows.Count > 0 Then
                Me.miSelectResult.PerformClick()
            End If
            If strbData.ToString = String.Empty Then
            Else
                strbData.Replace(strbData.ToString, "")
            End If

            Dim tb As Char = "\t"
ConvertTabletoString:
            Dim i As Integer = 0
            If (Me.dgvResult.SelectedRows.Count > 0) Then
                '       For Each c As DataColumn In dt.Columns  'HEADINGS
                ' For Each c As DataGridViewTextBoxColumn In Me.dgvResult.Columns 'hangs up if a checkbox
                For Each c As DataGridViewColumn In Me.dgvResult.Columns
                    If i = 0 Then
                        strbData.Append(c.Name) 'c.columnname
                    Else
                        strbData.Append(Chr(9) & c.Name)
                    End If
                    i += 1
                Next c
                strbData.Append(NextLine)
            Else
            End If
            i = 0
           
            For Each r As DataGridViewRow In Me.dgvResult.Rows
                If r.Selected = True Then '.SelectedRows() 'dt.Rows  'DATA
                    For Each rc As DataGridViewColumn In Me.dgvResult.Columns
                        If i = 0 Then
                            strbData.Append(IsNull(r.Cells(rc.Index).Value, ""))
                        Else
                            strbData.Append(Chr(9) & IsNull(r.Cells(rc.Index).Value, ""))
                        End If
                        i += 1
                    Next rc
                    strbData.Append(NextLine)
                    i = 0
                End If
            Next r
            Return True
        Else
            modGlobalVar.msg(MsgCodes.noResult)
            Return False
        End If
    End Function

    'select all
    Private Sub SelectAllToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles miSelectResult.Click
        Me.dgvResult.SelectAll()
    End Sub

    'COPY SELECTED ROWS 
    Private Sub miCopy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
    Handles miCopy.Click

        If TableToString() Then
            Try
                Clipboard.SetDataObject(strbData.ToString, True)
            Catch ex As Exception
                modGlobalVar.msg("ERROR: copying to clipboard ", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    'EXPORT DataGRIDVIEW to EXCEL accounting for odd characters like smart quotes
    Private Sub miExportExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
             Handles miExportExcel.Click
        modPopup.DataToExcel(Me.dgvResult)
    End Sub

    'ADVANCE PROGRESS BAR ON CALLING FORM
    Private Sub setProgressBar(ByVal i As Integer)
        If progBar Is Nothing Then
        Else
            progBar.Value = i
        End If
    End Sub

    'potential error handler
    Private Sub dgvResult_DataError(sender As Object, e As System.Windows.Forms.DataGridViewDataErrorEventArgs) Handles dgvResult.DataError
        ' MsgBox(e.Exception.Message, , "dataerror")'parameter is not valid
    End Sub

#End Region 'form specific

End Class

