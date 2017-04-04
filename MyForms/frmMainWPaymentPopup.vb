Public Class frmMainWPaymentPopup

    Dim WhichRB As String

#Region "Initialization"

    Public Sub New()
        MyBase.New()
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
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
    Private Sub frmMainWPaymentPopup_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) _
             Handles MyBase.Load
        Forms.Add(Me)
    End Sub

#End Region 'load

#Region "Search"

    'SEARCH
    Private Sub btnSrch_Click(sender As System.Object, e As System.EventArgs) Handles btnSrch.Click
        Dim lvItem As ListViewItem
        Dim tb As New DataTable
        MouseWait()
        Dim sql As New SqlClient.SqlCommand("SELECT    distinct    tReg.RegistrationID, ISNULL(tContact.Lastname, '') + ISNULL(', ' + tContact.FirstName, '') AS Who, OrgName, Event as EventName, ISNULL(tReg.AmountIndFee, 0) AS AmountIndFee, PaymentNum, RefundNum, isnull(OrderNum,0) as OrderNum " _
& " FROM            (SELECT        RegistrationID, OrderNum, EventNum, ContactNum, PaymentNum, RefundNum, AmountIndFee FROM            tblEventReg2) AS tReg INNER JOIN " _
& "                 (SELECT        EventID, isnull(' - ' + cast(firstDate as varchar),'') + ' - ' + coalesce(EventSKU,EventName)  as Event FROM   vwGetValidEvents2) AS tEvent ON tReg.EventNum = tEvent.EventID INNER JOIN " _
& "                 (SELECT        ContactID, FirstName, Lastname, OrgName  FROM tblContact INNER JOIN tblOrg on orgnum = OrgID) AS tContact ON tReg.ContactNum = tContact.ContactID WHERE PaymentNum is null ", sc)
        sql.CommandType = CommandType.Text
        'TODO 
        'change query for Valid entries only
        'do include cancelled in case was refund attached
        'excelu paymentnum or refundnum depending on type of payment being applied

SetQueryText:
        Select Case WhichRB
            Case Is = "Order Num"
                sql.CommandText = sql.CommandText & "AND Ordernum = " & IsNull(txtSrch.Text, Me.fldPaymentID.Text)
            Case Is = "Event"
                'TODO restore after testing - setup combo box
                sql.CommandText = sql.CommandText & "AND EventID = '" & txtSrch.Text & "'" 'Me.cboResult.Text & "'"
            Case Is = "Last Name"
                sql.CommandText = sql.CommandText & "AND tcontact.lastname = '" & txtSrch.Text & "'" 'Me.cboResult.Text & "'"
            Case Is = "Congregation"
                sql.CommandText = sql.CommandText & "AND tContact.OrgName like '%" & Replace(txtSrch.Text, "*", "%") & "%'" 'Me.cboResult.Text & "'"
            Case Else
                sql.CommandText = sql.CommandText & "AND Ordernum = " & IsNull(txtSrch.Text, gPayment.OrderID)
        End Select
        sql.CommandText = sql.CommandText & " ORDER BY RegistrationID Desc"

RunQuery:
        If Not SCConnect() Then
        Else
            tb.Load(sql.ExecuteReader)
        End If
        sc.Close()
LoadListBox:
        Me.lvSource.Items.Clear()
        For Each rw As DataRow In tb.Rows
            lvItem = Me.lvSource.Items.Add(rw(0).ToString)
            lvItem.SubItems.Add(rw(1))
            lvItem.SubItems.Add(rw(2))
            lvItem.SubItems.Add(rw(3))
            lvItem.SubItems.Add(rw(4))
            lvItem.SubItems.Add(rw(7))
            lvItem.SubItems.Add(rw(0))
        Next rw

        'TODO
        'check or disable items that already have payment
FormatListBox:

CloseAll:
        tb = Nothing
        sql = Nothing
        MouseDefault()
    End Sub

    'REFRESH LABEL with COUNT of selected registrations
    Private Sub lvSource_Click(sender As Object, e As System.EventArgs) Handles lvSource.Click
        Me.btnApply.Text = "Apply Payments: " & (Me.lvSource.CheckedItems.Count + 1).ToString
    End Sub

    'CALL SEARCH
    Private Sub rbOrder_CheckedChanged(sender As System.Object, e As System.EventArgs) _
        Handles rbOrder.CheckedChanged, rbEvent.CheckedChanged, rbOrg.CheckedChanged, rbLastname.CheckedChanged
        WhichRB = sender.text
        If sender.checked = True Then
            Me.btnSrch.PerformClick()
        End If

    End Sub

#End Region 'SEARCH

#Region "UPDATE"

    'APPLY PAYMENT
    Private Sub btnApply_Click(sender As System.Object, e As System.EventArgs) Handles btnApply.Click

        If Me.lvSource.Items.Count > 0 And Me.lvSource.CheckedItems.Count = 0 Then
            modGlobalVar.msg("Cancelling Request", "please tick the checkbox for registration to include in this payment", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            GoTo OrderUpdate
        End If
        For i As Integer = 0 To Me.lvSource.CheckedItems.Count() - 1
            Me.lvSource.CheckedItems(i).ForeColor = Color.LightGray
        Next i

        Dim sql As New SqlClient.SqlCommand("[EventPaymentApply]", sc)
        sql.CommandType = CommandType.StoredProcedure
        sql.Parameters.Add("@ID", SqlDbType.Int).Value = Me.fldPaymentID.Text
        sql.Parameters.Add("@Which", SqlDbType.Text).Value = gPayment.PaymentType
        sql.Parameters.Add("@IDArray", SqlDbType.Text).Value = modPopup.GetIDArray(Me.lvSource)
        If Not SCConnect() Then
            Exit Sub
        End If
        Try
            sql.ExecuteNonQuery()
            ' modGlobalVar.Msg(structPay.PaymentType, , "structure")
        Catch ex As System.Exception
            modGlobalVar.msg("ERROR: applying financials ", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            sql = Nothing
        End Try

OrderUpdate:
        If modGlobalVar.msg("Is this payment in full?", "Click Yes to change order status to Complete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            Dim sql2 As New SqlClient.SqlCommand("UPDATE tblEventRegOrder2 SET OrderStatus = 'Completed' WHERE OrderID = " & Me.txtSrch.Text)
            sql2.Connection = sc
            sql2.CommandType = CommandType.Text
            If Not SCConnect() Then
                Exit Sub
            End If
            Try
                sql2.ExecuteNonQuery()
                ' modGlobalVar.Msg(structPay.PaymentType, , "structure")
            Catch ex As System.Exception
                modGlobalVar.msg("ERROR: updating order status ", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                sql2 = Nothing
            End Try
        End If

        Try
            sc.Close()
        Catch ex As System.Exception
        End Try

    End Sub

    'CLOSE BUTTON
    Private Sub btnClose_Click(sender As System.Object, e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

#End Region 'UPDATE

End Class



