Public Class frmReviewResources


    Private Sub btnFind_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFind.Click
        miSave.PerformClick()
        Me.DsReviewResource1.EnforceConstraints = False
        If IsNull(Me.txtSrchID.Text, 0) > 0 Then
            Me.MainResourceTableAdapter.FillByID(Me.DsReviewResource1.MainResource, Me.txtSrchID.Text)
        Else
            Me.MainResourceTableAdapter.FillResource(Me.DsReviewResource1.MainResource, Me.txtSrch.Text)
        End If
        Me.MainResourceExtraTableAdapter.FillResourceExtra(Me.DsReviewResource1.MainResourceExtra)
        Me.MainResourceLocationTableAdapter.FillResourceLocation(Me.DsReviewResource1.MainResourceLocation)
    End Sub


    Private Sub btnUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles btnUpdate.Click, miSave.Click
        'TODO add transaction?
        Try
            RelResourcetoResourceExtraBindingSource.EndEdit()
            LuLocationBindingSource.EndEdit()
            RelResourceFeedbackBindingSource.EndEdit()
            RelResourceRecommendBindingSource.EndEdit()
            RelResourcetoResourceLocationBindingSource.EndEdit()
            RelResourceWarningBindingSource.EndEdit()
            MainResourceBindingSource.EndEdit()

        Catch ex As Exception
            modGlobalVar.Msg("ERROR: Ending Edit", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Try
            Me.MainResourceTableAdapter.Update(Me.DsReviewResource1.MainResource)
            Me.MainResourceExtraTableAdapter.Update(Me.DsReviewResource1.MainResourceExtra)
            Me.MainResourceLocationTableAdapter.Update(Me.DsReviewResource1.MainResourceLocation)
            Me.TblResourceFeedbackTableAdapter.Update(Me.DsReviewResource1.tblResourceFeedback)
            Me.TblResourceRecommendTableAdapter.Update(Me.DsReviewResource1.tblResourceRecommend)
            Me.TblResourceWarningTableAdapter.Update(Me.DsReviewResource1.tblResourceWarning)
        Catch ex As Exception
            modGlobalVar.Msg("ERROR: multiple ta update", "probably concurrency error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        'modGlobalVar.Msg("saved")
    End Sub


    Private Sub frmReviewResources_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        ' modGlobalVar.Msg("closing")
        miSave.PerformClick()
    End Sub

    Private Sub frmReviewResources_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.DsReviewResource1.EnforceConstraints = False
        'TODO: This line of code loads data into the 'DsReviewResource1.tblResourceWarning' table. You can move, or remove it, as needed.
        Me.TblResourceWarningTableAdapter.Fill(Me.DsReviewResource1.tblResourceWarning)
        'TODO: This line of code loads data into the 'DsReviewResource1.tblResourceRecommend' table. You can move, or remove it, as needed.
        Me.TblResourceRecommendTableAdapter.Fill(Me.DsReviewResource1.tblResourceRecommend)
        'TODO: This line of code loads data into the 'DsReviewResource1.tblResourceFeedback' table. You can move, or remove it, as needed.
        Me.TblResourceFeedbackTableAdapter.Fill(Me.DsReviewResource1.tblResourceFeedback)
        Me.LuLocationTableAdapter.Fill(Me.dsLuLocation.luLocation)

    End Sub

 

    'COPY FROM CELL ABOVE
    Private Sub dgvExtra_CellMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles dgvExtra.CellMouseDoubleClick
        If Me.dgvExtra.CurrentRow.Index = 0 Then
            Exit Sub
        End If
        If e.Button = System.Windows.Forms.MouseButtons.Right Then
            Me.dgvExtra.CurrentCell.Value = Me.dgvExtra.Item(Me.dgvExtra.CurrentCell.ColumnIndex, Me.dgvExtra.CurrentCell.RowIndex - 1).Value
        End If
    End Sub

    Private Sub btnFillDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFillDelete.click
        Me.txtSrch.Text = "%delete%"
        Me.btnFind.PerformClick()
    End Sub

 
End Class