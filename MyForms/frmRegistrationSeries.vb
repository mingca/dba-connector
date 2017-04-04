Public Class frmRegistrationSeries

    Public sql As New SqlClient.SqlCommand()

    Private Sub frmRegistrationSeries_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Click
        Me.ListBox1.SelectedIndex = -1
    End Sub


    Private Sub frmRegistrationSeries_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.ListBox1.SelectedIndex = -1
        sql = Me.SqlDataAdapter1.InsertCommand
        sql.Connection = sc
        sql.CommandType = CommandType.StoredProcedure

    End Sub

    'GET EVENT IDs FROM USER SELECTIONS
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles btnSaveExit.Click
        '..............
        'KEEP: LIST BOX FUNCTIONS
        'For i As Int16 = 1 To Me.ListBox1.SelectedItems.Count
        '    'modGlobalVar.Msg(Me.ListBox1.SelectedIndices.Item(i - 1).ToString)    'returns list number
        '    'modGlobalVar.Msg(Me.ListBox1.DisplayMember(i - 1).ToString) 'returns single letter of table name
        'Next
        '..............
        'THIS WORKS; retrieves correct fields but only from 1st selected item
        ' modGlobalVar.Msg(Me.ListBox1.SelectedItem(0).ToString, , Me.ListBox1.SelectedItem(1).ToString) 'returns list item + item not in list even though that column is first so should have lower number
        '..............
        'THIS ALSO WORKS; retrieves correct fields for all selected items
        'colEventSeriesID.Clear()
        MouseWait()
        Dim r As DataRowView

        ' sql.Parameters("@Fee").Value = InputBox("Enter amount for each subsequent entry", "ENTERING MULTIPLE REGISTRATIONS", sql.Parameters("@Fee").Value)
        Select Case sql.Parameters("@Multiple").Value 'modGlobalVar.Msg("YES: amount will be blank, and Multiple Event Series will be checked"& NextLine &  "NO: " & sql.Parameters("@Fee").Value.ToString & " will be entered for each additional registration.", MessageBoxButtons.YesNo, "One payment for full series?")
            Case Is = True
                sql.Parameters("@Fee").Value = "series"
                sql.Parameters("@Multiple").Value = True
            Case Else
                sql.Parameters("@Multiple").Value = False
        End Select

        If Not SCConnect() Then
            Exit Sub
        End If

        For x As Int16 = 0 To Me.ListBox1.SelectedItems.Count - 1
            r = Me.ListBox1.SelectedItems(x)
            ' modGlobalVar.Msg(r.Item("Registered").ToString.Substring(0, 1))
            If r.Item("Registered").ToString.Substring(0, 1) = "Y" Then
            Else
                sql.Parameters("@EventID").Value = r.Item("EventID")
                'TODO 2012 put globalcontact back if required
                '   sql.Parameters("@ContactID").Value = globalContactID
                Try
                    sql.ExecuteNonQuery()
                Catch exc As Exception
                    '  modGlobalVar.Msg(exc.Message)
                End Try
                '   colEventSeriesID.Add(r.Item("EventID"))
                '  modGlobalVar.Msg(r.Item("Registered"), , r.Item("EventID"))
            End If
        Next
        sc.Close()
        Me.Close()
        MouseDefault()
    End Sub

    'Private Sub btnSaveExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
    'Handles btnSaveExit.Click
    '    Me.Close()
    'End Sub


    Private Sub btnHelp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHelp.Click
        modGlobalVar.Msg("REGISTRATIONS for an EVENT SERIES", "Hold down the Ctrl key and click an item in the list to select it.  When you close this window, registrations will be added to those events for this individual." & NextLine & NextLine & "'Y' indicates a registration already exists for that event.", MessageBoxButtons.OK, MessageBoxIcon.Information)

    End Sub
End Class