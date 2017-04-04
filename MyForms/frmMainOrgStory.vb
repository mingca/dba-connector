Imports System.Data.SqlClient
Imports System.Text

Public Class frmMainOrgStory
    Inherits System.Windows.Forms.Form

    Public isLoaded As Boolean = False
    Dim ctlIdentify As Control
    Dim ctlNeutral As Control
    Dim objHowClose As Short 'New structCloseMethod  'identify object calling close
    Dim mainDS As DataSet
    Dim mainTopic As String
    Dim mainDAdapt As SqlDataAdapter
    Public ThisID, LocalOrgID As Integer
    Dim mainBSrce As System.Windows.Forms.BindingSource


#Region "Load"

    'LOAD
    Private Sub frmMainStory_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Me.SuspendLayout()
        modPopup.SetStatusBarText("loading", Me.StatusBar1, 0)

SetMainDSConnection:
        Me.daspStoryCases.SelectCommand.Connection = sc
        Me.daMainOrgStory.SelectCommand.Connection = sc
        Me.daMainOrgStory.UpdateCommand.Connection = sc
        Me.daMainOrgStory.DeleteCommand.Connection = sc

SetDefaults:
        mainDAdapt = Me.daMainOrgStory
        mainDS = Me.DsMainStory
        mainBSrce = Me.MainOrgStoryBindingSource
        ctlIdentify = Me.txtHeadline
        ctlNeutral = Me.Panel2
        mainTopic = "Organization Story"
LoadCombobox:
        modGlobalVar.LoadStaffCombo(Me.cboStaff, False, StaffComboChoices.Selectable)
FormSetup:

        Forms.Add(Me)
        modPopup.SetStatusBarText("Done", Me.StatusBar1, 0)
        Me.ResumeLayout()
        isLoaded = True
 
    End Sub

    'RESET CLOSE CALLER
    Public Sub Reload()
        'RESET VARS
        objHowClose = ObjClose.btnSaveExit
        Me.StatusBarPanelID.Text = mainTopic & " ID: " & ThisID.ToString
        modGlobalVar.LoadCaseCombo(Me.cboCase, tblCases, LocalOrgID)
    End Sub

#End Region    'load

#Region "Update Main"

    'mi ALLOW CLOSE WITHOUT SAVING
    Protected Sub miCloseForm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles miClose.Click

        MouseWait()
        ctlNeutral.Focus()

        If mainDS.Tables(0).Rows(0).HasVersion(DataRowVersion.Proposed) Or mainDS.HasChanges Then
            mainBSrce.EndEdit()
        End If
        objHowClose = AskAcceptChanges(mainDS, mainTopic)
        Me.Close()

CloseAll:
        MouseDefault()
    End Sub

    'SAVE & EXIT
    Private Sub btnSaveExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
          Handles btnSaveExit.Click

        objHowClose = ObjClose.btnSaveExit
        Me.Close()

    End Sub

    'CLOSING
    Private Sub frm_Closing(ByVal sender As Object, ByVal e As FormClosingEventArgs) _
        Handles MyBase.FormClosing  ''ByVal e As System.ComponentModel.CancelEventArgs) 

        Dim ctl As Control
        Dim arCtls(0) As Control

CallUpdate:
        If objHowClose = ObjClose.DontSaveClose Or objHowClose = ObjClose.cancelClose Then
            GoTo CheckRequiredFields
        End If
        If DoUpdate() Then
            e.Cancel = False
        Else
            e.Cancel = True 'don't close form
            GoTo ReleaseForm
        End If

CheckRequiredFields:
        Select Case objHowClose
            Case Is = ObjClose.DontSaveClose, ObjClose.miDelete
                e.Cancel = False
                GoTo ReleaseForm
            Case Is = ObjClose.cancelClose
                e.Cancel = True
                GoTo ReleaseForm
            Case Else 'btnSaveExit, SaveClose,, ObjClose.noChanges
                arCtls = CheckRequired()
                If arCtls.GetLength(0) > 1 Then 'required info missing
                    ctl = arCtls(0)
                    'INSERT DEFAULT DATA
                    If objHowClose = ObjClose.SaveClose Or e.CloseReason = CloseReason.UserClosing Then
                        If ctl Is ctlIdentify Then
                            ctl.Text = usrName & " " & Today.ToShortDateString
                            mainBSrce.EndEdit()
                            mainDAdapt.Update(mainDS) 'save default data
                        End If
                    End If
                    Dim strbListFields As New StringBuilder
                    For x As Integer = 0 To arCtls.GetLength(0) - 2
                        strbListFields.Append(", " & arCtls(x).Tag)
                    Next
                    e.Cancel = Not (modGlobalVar.AskCloseWithMissingInfo(objHowClose, ctl, strbListFields.ToString.Substring(2)))
                Else
                    e.Cancel = False
                End If
        End Select
ReleaseForm:
        If e.Cancel = False Then   'user OKs close form
            ClassOpenForms.frmMainOrgStory = Nothing 'reset global var
            objHowClose = Nothing
        Else
        End If
        arCtls = Nothing
        MouseDefault()


    End Sub

    'UPDATE BACK END, return number of records updated, return false if error updating
    Public Function DoUpdate() As Boolean
        Dim i As Integer
        MouseWait()
        modPopup.SetStatusBarText("Done", Me.StatusBar1, 0)

        If mainDS.Tables(0).Rows(0).HasVersion(DataRowVersion.Proposed) Or mainDS.HasChanges Then
            mainBSrce.EndEdit()
        Else
            DoUpdate = True
            GoTo CloseAll
        End If

        If mainDS.HasChanges = True Then 'this catches delete, save, asksave, save/exit, anyclose
UpdateBackend:
            Try
                i = mainDAdapt.Update(mainDS)
                DoUpdate = True
            Catch ex As Exception
                modGlobalVar.Msg("Error updating " & mainTopic, ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
                DoUpdate = False
            End Try
        Else
            DoUpdate = True 'completed action though no updates to be made
        End If

CloseAll:
        modPopup.SetStatusBarText("Update routine complete", Me.StatusBar1, 0)
        MouseDefault()

    End Function 'update

    'CHECK REQUIRED FIELDS w Error Provider
    Private Function CheckRequired() As Control()
        Dim ctl As Control
        Dim arCtls(0) As Control
        Dim i As Integer = 0

        'Headline
        ctl = ctlIdentify
        If Replace(Replace(Replace(ctl.Text, " ", ""), Chr(10), ""), Chr(13), "") = String.Empty Then
            Me.ErrorProvider1.SetError(ctl, "please enter a " & IsNull(ctl.Tag, ctl.Name) & " or 'Delete' if it is an unwanted entry")
            arCtls(i) = ctl
            i = i + 1
            ReDim Preserve arCtls(arCtls.GetUpperBound(0) + 1)
        Else
            Me.ErrorProvider1.SetError(ctl, "")
        End If

        'Staff
        ctl = Me.cboStaff
        If ctl.Text = String.Empty Then
            arCtls(i) = ctl
            i = i + 1
            ReDim Preserve arCtls(arCtls.GetUpperBound(0) + 1)
            Me.ErrorProvider1.SetError(ctl, "please enter a " & IsNull(ctl.Tag, ctl.Name))
        Else
            Me.ErrorProvider1.SetError(ctl, "")
        End If

        arCtls(i) = ctlNeutral
        Return arCtls

    End Function

#End Region 'update

#Region "Edit Buttons"

    'SAVE
    Private Sub miSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles miSave.Click
        mainBSrce.EndEdit()
        DoUpdate()
    End Sub

    'CANCEL ALL CHANGES
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
          Handles miCancel.Click
        Try
            mainBSrce.CancelEdit()
            mainDS.RejectChanges()
        Catch ex As System.Exception
            modGlobalVar.Msg("ERROR: cancelling changes ", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        modPopup.SetStatusBarText("Changes Cancelled", Me.StatusBar1, 0)
    End Sub

    'DELETE
    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
           Handles miDelete.Click, btnDelete.Click

        If modGlobalVar.Msg("CONFIRM DELETE", mainTopic + IsNull(ctlIdentify.Text, "") & NextLine & " WILL BE DELETED and the window closed.", MessageBoxButtons.YesNo, MessageBoxIcon.Error) = DialogResult.Yes Then
            ' ctl.Text = "DELETE: " & IsNull(ctl.Text, "")
            objHowClose = ObjClose.miDelete
            mainBSrce.RemoveCurrent() 'RemoveAt(0)
            Me.Close()
        End If

    End Sub

#End Region 'menuitems

#Region "Validation"

    'STAFF - required
    'VALIDATE STAFF CBO
    Private Sub cboStaff_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) _
        Handles cboStaff.Validating
        If modGlobalVar.ValidateBoundDD(sender, True, Me.ErrorProvider1, ObjClose.CloseByControl) = usrInput.OK Then
            e.Cancel = False
        Else
            e.Cancel = True
            sender.DroppedDown = True
        End If
    End Sub

    'VALIDATE DATE
    Private Sub DateValidation(sender As Object, e As System.ComponentModel.CancelEventArgs) _
        Handles DtCreate.Validating
        e.Cancel = modGlobalVar.ValidateDateA(sender, Me.ErrorProvider1)
    End Sub

#End Region ''validation

#Region "General"

    'RIGHT CLICK MENU
    Private Sub textbox_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) _
        Handles rtbStory.MouseDown
        If e.Button = System.Windows.Forms.MouseButtons.Right Then
            Dim pp As New ClassRtbContextMenu(sender)
            pp.Show(Me, PointToClient(Control.MousePosition))
        End If
    End Sub

    'UNDO
    Protected Overrides Function ProcessDialogKey(ByVal keyData As Keys) As Boolean
        If keyData = System.Windows.Forms.Keys.Escape Then
            modPopup.UndoCtl(Me.ActiveControl)
            ' bChanged = True
            Return True  ' True means we've processed the key
        Else
            Return MyBase.ProcessDialogKey(keyData)
        End If
    End Function

    'COPY CURRENT ID
    Private Sub StatusBar1_DoubleClick(sender As System.Object, e As System.EventArgs) Handles StatusBar1.DoubleClick
        Clipboard.SetText(ThisID)
    End Sub

#End Region 'General

End Class