Imports System.Text

Public Class frmMainWPayment

    Dim mainTbl As DataTable    'for global function
    Dim ctlIdentify, ctlNeutral As Control 'field for delete and messages
    Dim objHowClose As New structCloseMethod  'identify object calling close
    Dim bCancelClose As Boolean = False
    Dim bChanged As Boolean = False

#Region "Initialize"
    Public Sub New()
        MyBase.New()
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
        Forms.Remove(Me)
    End Sub
#End Region 'initialize

#Region "Load"

    'LOAD
    Private Sub frmMainWPayment_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

        mainTbl = Me.DsMainWPayment.MainEventPayment
        ctlIdentify = Me.txtNotes
        ctlNeutral = Me.txtNotes

        Forms.Add(Me)
    End Sub

    'FILL DATASET/GRIDS
    Public Sub OnCurrent(ByVal iPayID As Integer)
        'fill datagrid with matching registrations
        Try
            Me.MainEventPaymentTableAdapter.Fill(Me.DsMainWPayment.MainEventPayment, iPayID)
            FillGrid(iPayID)
        Catch ex As Exception
            modGlobalVar.msg("ERROR: Fill Payment", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        SetLblTrackingNum(Me.cboPaymentMethod.Text)
    End Sub

#End Region 'load

#Region "Update Main"

    'ALLOW CLOSE WITHOUT SAVING
    Protected Sub miCloseForm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
           Handles miClose.Click
        MouseWait()
        Dim bUpdated As Boolean = False

        objHowClose.sh = modGlobalVar.CloseDetailForm(Me.Tag, IsNull(ctlIdentify.Text, "'"), Me.MainEventPaymentBindingSource, ctlNeutral, mainTbl, bChanged)
        Select Case objHowClose.sh
            Case Is = ObjClose.SaveClose    'save changes, close
                UpdateDB("miClose")
            Case Is = ObjClose.cancelClose      'cancel close
                bCancelClose = True
                GoTo CloseAll
            Case Is = ObjClose.DontSaveClose       'discard changes, close
                Me.MainEventPaymentBindingSource.EndEdit()
                bChanged = False
            Case Is = ObjClose.noChanges        'no changes, close
        End Select
        Me.Close()
CloseAll:
        MouseDefault()
    End Sub

    'SAVE & EXIT
    Private Sub btnSaveExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnSaveExit.Click
        objHowClose.sh = ObjClose.btnSaveExit
        Me.Close()
    End Sub

    'CLOSING
    Private Sub frm_closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) _
           Handles MyBase.FormClosing
        UpdateDB("close")
    End Sub

    'UPDATE
    Public Sub UpdateDB(ByVal How As String)

        MouseWait()
        Try
            Me.MainEventPaymentBindingSource.EndEdit()
            Me.MainEventPaymentTableAdapter.Update(Me.DsMainWPayment.MainEventPayment)
            bChanged = False
            '  modGlobalVar.Msg("s/be updated")
        Catch ex As Exception
            modGlobalVar.msg("ERROR: Save", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        MouseDefault()
    End Sub

#End Region 'update main

#Region "Fill Tables"

    'FILL REGISTRATION GRID
    Private Sub FillGrid(ByVal iPayID As Integer)
        Me.GridPaymentRegsTableAdapter.Fill(Me.dsGridPaymentRegs.GridPaymentRegs, iPayID)
        Me.DataGridView1.Refresh()
    End Sub

    'SEARCH FOR DIFFERENT PAYMENT
    Private Sub btnSrch_Click(sender As Object, e As System.EventArgs) Handles btnSearch.Click
        OnCurrent(Me.txtSrchID.Text)
    End Sub

#End Region     'fill tables

#Region "Menu Items"
    'SAVE
    Private Sub miSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles miSave.Click
        '  mainBSrce.EndEdit()
        UpdateDB("save")
    End Sub

    'mi CANCEL CHANGES
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.MainEventPaymentBindingSource.EndEdit()
        bChanged = False
    End Sub

    'DELETE
    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles miDelete.Click

        If modGlobalVar.msg("CONFIRM DELETE", "Payment: " & IsNull(ctlIdentify.Text, "") & NextLine & " will be marked for deletion and the window closed.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = MsgBoxResult.Yes Then
            ctlIdentify.Text = "DELETE: " & IsNull(ctlIdentify.Text, "")
            objHowClose.sh = ObjClose.miDelete
            Me.Close()
        End If
    End Sub

#End Region

#Region "Form Specific"

    'PAYMENT CBO call get tracking heading
    Private Sub cboPaymentMethod_SelectionChangeCommitted(sender As System.Object, e As System.EventArgs) _
        Handles cboPaymentMethod.SelectionChangeCommitted

        Me.fldPaymentMethod.Text = sender.selecteditem
        SetLblTrackingNum(sender.text)
        bChanged = True

    End Sub

    'NEW REGISTRATION
    Private Sub btnAddReg_Click(sender As System.Object, e As System.EventArgs) Handles btnAddReg.Click
        '===use structure here
        'payment type
        'search what
        'searchID
        'how load EventID from combo??
        gPayment.PaymentType = Me.cboPaymentType.Text
        gPayment.OrderID = Me.fldOrderID.Text
        gPayment.PaymentID = Me.fldPayID.Text

        Dim frm As New frmMainWPaymentPopup '(Me.fldPayID.Text)
        frm.txtSrch.Text = gPayment.OrderID
        frm.fldPaymentID.Text = Me.fldPayID.Text
        Try
            frm.ShowDialog()
        Catch ex As System.Exception
            modGlobalVar.msg("ERROR: opening popup  ", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        FillGrid(Me.fldPayID.Text)
    End Sub

    'TRACKING LABEL CHOICES
    Private Sub SetLblTrackingNum(str As String) ' Handles fldPaymentMethod.TextChanged
        'SET LABEL TEXT of tracking number field
        If str = String.Empty Then
        Else
            Select Case Me.cboPaymentMethod.Text.Substring(0, 4)
                Case Is = "Chec"
                    Me.lblTrackingNum.Text = "Check Number: "
                Case Is = "Payp", "Cred"
                    Me.lblTrackingNum.Text = "PaypalID: "
                Case Is = "Coup", "Disc"
                    Me.lblTrackingNum.Text = "Drupal ref: "
                Case Is = "Cash"
                    Me.lblTrackingNum.Text = "not applicable"
                Case Else
                    Me.lblTrackingNum.Text = "Check Number: "
            End Select
        End If
    End Sub

    'PAYMENT CHANGE call get tracking heading
    Private Sub fldPaymentMethod_TextChanged(sender As Object, e As System.EventArgs) Handles fldPaymentMethod.TextChanged
        SetLblTrackingNum(Me.cboPaymentMethod.Text)
    End Sub

#End Region

#Region "Validation"
    'VALIDATE PAYMENT CBO
    Private Sub CBO_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) _
        Handles cboPaymentType.Validating '.Validating, cboCRG.Validating, cboMgr.Validating

        Dim CheckInput As usrInput
        CheckInput = modGlobalVar.ValidateBoundDD(sender, True, Me.ErrorProvider1, ObjClose.CloseByControl)
        If CheckInput = usrInput.OK Then
            e.Cancel = False
        Else
            e.Cancel = True
            sender.DroppedDown = True
        End If
    End Sub

#End Region 'validation

End Class