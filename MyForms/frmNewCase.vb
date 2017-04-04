Option Explicit On
'------- TO DO fix date 1/1/1911
Imports System.Data.SqlClient


Public Class frmNewCase

    Dim cmgr As CurrencyManager
    Dim bCancelClose As Boolean
    Dim IsLoaded As Boolean
    Public bDirty As Boolean 'crg combo search

#Region "Load"
    'LOAD
    Private Sub frmNewCase_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.daNewCase.SelectCommand.Connection = sc
        Me.daNewCase.UpdateCommand.Connection = sc
        cmgr = CType(Me.BindingContext(Me.DsNewCase1, "MainNewCase"), CurrencyManager)
    End Sub

#End Region  'load

#Region "Update"

    'BTN SaveExitUpdate
    Private Sub btnSaveExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles btnSaveExit.Click
        MouseWait()

        If Me.cboMgr.Text = String.Empty Then
            modGlobalVar.msg("Missing Information", "please select Type of event", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Me.cboMgr.Focus()
            Exit Sub
        End If
        If Me.cboCRG.SelectedIndex = -1 Then
            modGlobalVar.msg("Missing Information", "please select a CRG issue", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Me.cboCRG.Focus()
            Exit Sub
        End If
        If Me.cboStatus.SelectedIndex = -1 Then
            modGlobalVar.msg("Missing Information", "please select the Status of the Case", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Me.cboStatus.Focus()
            Exit Sub
        End If
Update:
        Me.DsNewCase1.EnforceConstraints = False
        'ERROR HANDLING FOR END EDIT LIKE VALIDATION
        Try
            cmgr.EndCurrentEdit()
        Catch ECONSTRAINT As ConstraintException
            modGlobalVar.msg("CONSTRAINT ERROR", ECONSTRAINT.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ENULL As NoNullAllowedException
            modGlobalVar.msg("NULL ERROR", ENULL.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch EDATA As DataException
            modGlobalVar.msg("ADO.NET DATA ERROR", EDATA.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ESYSTEM As Exception
            modGlobalVar.msg("ESYSTEM ERROR", ESYSTEM.GetType.ToString & NextLine & ESYSTEM.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

BeginTransaction:
        Dim myTransaction As SqlClient.SqlTransaction
        If Not SCConnect() Then
            Exit Sub
        End If

        'myTransaction = sc.BeginTransaction
        Me.daNewCase.UpdateCommand.Transaction = myTransaction
        Try
            Me.daNewCase.UpdateCommand.Parameters("@ID").Value = Me.fldCaseID.Text
            Me.daNewCase.Update(Me.DsNewCase1.MainNewCase)
            '   myTransaction.Commit()
            bCancelClose = False
        Catch dbcex As DBConcurrencyException
            modGlobalVar.msg("ERROR: Update Failed", "someone else has changed this Case", MessageBoxButtons.OK, MessageBoxIcon.Error)
            'TODO put code here to capture changes and/or override
            '   myTransaction.Rollback()
            bCancelClose = True
        Catch eUpdate As System.Exception
            modGlobalVar.msg("ERROR:", eUpdate.Message & NextLine & eUpdate.GetType.ToString, MessageBoxButtons.OK, MessageBoxIcon.Error)
            bCancelClose = True
            '  myTransaction.Rollback()
        Finally
            sc.Close()
        End Try

        Me.Close()
        MouseDefault()
    End Sub

#End Region 'update

#Region "General"

    'RIGHT CLICK MENU
    Private Sub textbox_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) _
        Handles txtCaseName.MouseDown, txtDescription.MouseDown
        If e.Button = System.Windows.Forms.MouseButtons.Right Then
            Dim pp As New ClassRtbContextMenu(sender)
            pp.Show(Me, PointToClient(Control.MousePosition))
        End If
    End Sub

    'CALL REMOVE PARAGRAPHS
    Private Sub txtLostFocus(ByVal sender As Object, ByVal e As System.EventArgs) _
     Handles txtCaseName.LostFocus
        modGlobalVar.RemoveLineFeeds(sender)
    End Sub

    'CRG Filter
    Private Sub cboCRG_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) _
            Handles cboCRG.MouseDown
        If e.Button = System.Windows.Forms.MouseButtons.Right Then
            modPopup.SearchCRG(Me, PointToClient(Control.MousePosition), Me.cboCRG)
        End If
    End Sub

#End Region 'general

#Region "Form Specific"
    'force cboCRG dirty
    Private Sub cboCRG_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cboCRG.SelectedIndexChanged
        If IsLoaded Then
            bDirty = True 'else mystery won't save if is only change
        End If
    End Sub

    'CBO STATUS re Dates
    Private Sub cboStatus_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cboStatus.Validating
        If modGlobalVar.ValidateCBO(sender, "Status of Case", True, CanAddNew.None) = usrInput.OK Then
            If cboStatus.Text = "Closed" Then
                If modGlobalVar.msg("WARNING - Closing Case", "Are you sure you want to close this case?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                    Me.txtCloseDate.Text = Today
                Else
                    cboStatus.DroppedDown = True
                End If
            Else
                If CType(Me.txtCloseDate.Text, String) > "" Then
                    Me.txtCloseDate.Text = "1/1/1911"
                End If
            End If
        End If
    End Sub

#End Region

End Class