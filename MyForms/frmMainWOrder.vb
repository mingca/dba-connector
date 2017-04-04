Imports System.Data.SqlClient

Public Class frmMainWOrder

    Dim bDelete, bCancelClose, isLoaded As Boolean
    Dim bDup As Boolean = False 'check forduplicate entries
    Public bChanged As Boolean = False
    Dim mainTbl As DataTable
    Dim ctlNeutral, ctlIdentify As Control
    Dim cntGroup As Integer = 1
    Dim iFee As Integer ' = 30
    Dim iDiscount As Integer '= 5
    Dim iGroupMin As Integer '=3
    Dim cntTeam As Integer
    Dim objHowClose As New structCloseMethod 'Object = Me.btnSaveExit 'identify object calling close


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
            Forms.Remove(Me)
        End Try
    End Sub

#End Region

#Region "Load"

    'LOAD
    Private Sub frmMainWOrder_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Forms.Add(Me)
    End Sub

    'LOAD REGISTRATION GRID 'called by Open
    Public Sub LoadGrid(ByVal iOrder As Integer) ', ByVal iEvent As Integer)
        Me.tblRegOrderTableAdapter.Fill(Me.DsNewWRegistration.tblRegOrder, iOrder)
    End Sub

#End Region 'load

#Region "Update"

    'SAVE WHEN CLOSE
    Private Sub frm_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) _
           Handles MyBase.FormClosing
        MainEventOrderBindingNavigatorSaveItem.PerformClick()
    End Sub

    'MI SAVE
    Private Sub MainEventOrderBindingNavigatorSaveItem_Click(sender As System.Object, e As System.EventArgs) _
        Handles MainEventOrderBindingNavigatorSaveItem.Click
        UpdateDB("save")
    End Sub

    'UPDATE
    Public Sub UpdateDB(ByVal How As String)
        Me.Validate()
        Me.MainWOrder2BindingSource.EndEdit()
        Me.taMainRegistOrder.Update(Me.dsMainWOrder1.MainEventOrder2)
        'Me.TableAdapterManager.UpdateAll(Me.dsMainWOrder1)
    End Sub

#End Region 'update

#Region "Edit Buttons"
    'DELETE
    Private Sub BindingNavigatorDeleteItem_Click(sender As System.Object, e As System.EventArgs) Handles BindingNavigatorDeleteItem.Click
        Me.txtOrderNotes.Text = "DELETE " + IsNull(Me.txtOrderNotes.Text, "")
        Me.Close()
    End Sub

#End Region 'edit buttons

#Region "Validation"

    ' DATE VALIDATING 
    Private Sub dtOrder_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs)
        e.Cancel = modGlobalVar.ValidateDateA(sender)
    End Sub

#End Region 'validation

#Region "ADD NEW Registration"
    'OPEN NEW REG FORM
    Private Sub btNewReg_Click(sender As System.Object, e As System.EventArgs) _
        Handles btNewReg.Click

        MouseWait()
        Dim frm As New frmNewWReg2
        If frm.Loadcombos() Then
            frm.fldOrderNum.Text = Me.fldOID.Text
        End If
        frm.cboRegistrant.SelectedIndex = -1
        frm.cboEvent.SelectedIndex = -1
        frm.cboEvent.Focus()
        frm.ShowDialog()

        MouseWait()
RefreshGrid:
        LoadGrid(Me.fldOID.Text)
        MouseDefault()
    End Sub

#End Region 'new registration

End Class