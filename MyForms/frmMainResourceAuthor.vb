Imports System
Imports System.Data.SqlClient

Public Class frmMainResourceAuthor
    Inherits Windows.Forms.Form
    Public iResourceExtraID

    ' Dim bDelete As Boolean = False
    Dim bCancelClose As Boolean = False
    Dim cmgr As CurrencyManager
    Dim ctlNeutral As Control 'will never be invalidated
    Dim mainDS As DataSet 'for generic module calls like CloseDetailForm
    Dim mainTopic As String 'name of entity of this form - case, contact, etc
    Dim mainDAdapt As SqlDataAdapter
    Dim objHowClose As Short
    'TODO ADD HELP button

#Region "iNITIALIZE"
    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
        Forms.Remove(Me)
    End Sub

#End Region

#Region "Load"

    'LOAD
    Private Sub frmAddAuthor_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.daspMainResourceExtra.SelectCommand.Connection = sc
        Me.daspMainResourceExtra.UpdateCommand.Connection = sc
        Me.daspMainResourceExtra.DeleteCommand.Connection = sc

        Try
            Me.daspMainResourceExtra.SelectCommand.Parameters("@ID").Value = iResourceExtraID
            Me.DsMainResourceExtra.Clear()
            Me.daspMainResourceExtra.Fill(Me.DsMainResourceExtra)
        Catch ex As Exception
        End Try
setDefaults:
        ctlNeutral = Me.btnSaveExit
        cmgr = Me.BindingContext(Me.DsMainResourceExtra, "tblResourceExtra")
        mainDS = Me.DsMainResourceExtra

        'presumes frmResources is open
        If ClassOpenForms.frmMainResource.cboType.Text = "Event" Then
            Me.TabControl1.SelectTab("pgEvent")
            Me.EventDateTextBox.Focus()
        Else
            Me.TabControl1.SelectTab("pgName")
            Me.cboAuthor.Focus()
        End If
        Forms.Add(Me)
    End Sub

#End Region 'load

#Region "Update Main"

    'mi ALLOW CLOSE WITHOUT SAVING
    Protected Sub miCloseForm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles miClose.Click

        MouseWait()
        ctlNeutral.Focus()

        If mainDS.Tables(0).Rows(0).HasVersion(DataRowVersion.Proposed) Or mainDS.HasChanges Then
            cmgr.EndCurrentEdit() '  mainBSrce.EndEdit() 
        End If
        objHowClose = AskAcceptChanges(mainDS, mainTopic)
        Me.Close()

CloseAll:
        MouseDefault()
    End Sub

    'btn SAVE & EXIT 
    Private Sub btnSaveExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
      Handles btnSaveExit.Click
        objHowClose = ObjClose.btnSaveExit
        Me.Close()

    End Sub

    'CLOSING, validate, default data, release form
    Private Sub frm_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) _
            Handles MyBase.FormClosing

        ctlNeutral.Focus()
CallUpdate:
        DoUpdate("closing")

    End Sub

    'UPDATE BACKEND
    Public Sub DoUpdate(ByVal How As String)
        MouseWait()
        Me.DsMainResourceExtra.EnforceConstraints = False

        'ERROR HANDLING FOR END EDIT LIKE VALIDATION
        Try
            cmgr.EndCurrentEdit()
            'If CType(cmgr.Current, DataRowView).Row.HasVersion(DataRowVersion.Proposed) Then 'doesn't work
            '   If Me.DsMainContact1.HasChanges(DataRowState.Modified) Then 'works, but now cancel doesn't
        Catch ECONSTRAINT As ConstraintException
            modGlobalVar.Msg("CONSTRAINT ERROR", ECONSTRAINT.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ENULL As NoNullAllowedException
            modGlobalVar.Msg("NULL ERROR", ENULL.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch EDATA As DataException
            modGlobalVar.Msg("ADO.NET DATA ERROR", EDATA.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ESYSTEM As Exception
            modGlobalVar.Msg("ESYSTEM ERROR", ESYSTEM.Message & NextLine & ESYSTEM.GetType.ToString, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Dim i As Integer
        Try
            '  Me.daspMainResourceExtra.UpdateCommand.Parameters("@ID").Value = Me.iResourceExtraID
            i = Me.daspMainResourceExtra.Update(Me.DsMainResourceExtra.tblResourceExtra)
            '   myTransaction.Commit()
            bCancelClose = False
        Catch dbcex As DBConcurrencyException
            modGlobalVar.Msg("ERROR: Update Failed", "someone else has changed this entry", MessageBoxButtons.OK, MessageBoxIcon.Error)
            'TODO put code here to capture changes and/or override
            ' myTransaction.Rollback()
            bCancelClose = True
        Catch eUpdate As System.Exception
            modGlobalVar.Msg("ERROR System Exc", eUpdate.Message & NextLine & eUpdate.GetType.ToString, MessageBoxButtons.OK, MessageBoxIcon.Error)
            bCancelClose = True
            '  myTransaction.Rollback()
        Finally
            sc.Close()
        End Try
        If i > 0 Then
            Try 'put these here, not in main resource form
                ClassOpenForms.frmMainResource.ReloadExtras()
                ClassOpenForms.frmMainResource.SetLastChanged()
                ClassOpenForms.frmMainResource.ResetPersonName()
            Catch ex As System.Exception
                modGlobalVar.Msg("ERROR: update LastChanged of Resource", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        Else

        End If

CloseAll:
        ' SetStatusBarText("Update routine complete")
        MouseDefault()
    End Sub

#End Region 'update

#Region "Edit Buttons"

    'mi SAVE CHANGES
    Private Sub miSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
    Handles miSave.Click
        '   mainBSrce.EndEdit()
        DoUpdate("mi")
    End Sub

    'mi CANCEL CHANGES
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
          Handles miCancel.Click
        cmgr.CancelCurrentEdit()
    End Sub

    'mi DELETE CURRENT RECORD
    Private Sub miDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles miDelete.Click
        Dim ctl As Control = Me.LastNameTextBox

        If modGlobalVar.Msg("CONFIRM DELETE", IsNull(ctl.Text, " ") & NextLine & "WILL BE DELETED from the database." , MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            objHowClose = ObjClose.miDelete
            cmgr.RemoveAt(0)
            Me.Close()
        End If

    End Sub

#End Region

#Region "General"

    'VERIFY PHONE NUMBER FORMATTED OK
    Private Sub editPhone_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles PhoneTextBox.Leave, FaxTextBox.Leave
        If Len(sender.text) > 0 Then
            modGlobalVar.LeavePhone(sender, "USA")
        End If

    End Sub

#End Region

End Class