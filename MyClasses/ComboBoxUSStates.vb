Public Class ComboBoxUSStates
    'get list of US state abbreviations
    'when include Canadian provinces, cannot get US states to sort first
    Inherits ComboBox
    'DO NOT RESTRICT TO ITEMS IN LIST
    Public Property RestrictContentToListItems As Boolean = False
    'Public Overloads Property AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
    'Public Overloads Property AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
    'Public Overloads Property displaymember = "StateName"
    'Public Overloads Property ValueMember = "StateAbbr"
    'Public Overloads Property sorted = False 'it sorts anyway when type in dd

    Protected tStates As New DataTable

    Public Sub New()

        Dim sql As New SqlClient.SqlCommand("SELECT StateAbbr, StateName  FROM vwGetStateDD", sc) ' ORDER BY CASE WHEN Country = 'USA' THEN ' ' ELSE Country END, StateName ", sc)
        '  Protected tStates As New DataTable
        modGlobalVar.LoadDataTable(tStates, sql)
        Me.DisplayMember = "StateName"
        Me.ValueMember = "StateAbbr"
        Me.DataSource = tStates

        'SUGGEST APPEND
        With Me
            .AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            .AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            'default style is dropdown
            .Sorted = False
        End With
        MsgBox("cboStates New")
    End Sub

    'ALLOW ENTER KEY TO SELECT
    Protected Overrides Sub OnKeyDown(e As System.Windows.Forms.KeyEventArgs)
        '  MyBase.OnKeyDown(e)
        If e.KeyCode = Keys.Enter Then
            Me.DroppedDown = False
            e.Handled = True
        Else
            MyBase.OnKeyDown(e)
        End If
    End Sub


End Class
