Option Explicit On
Imports System.Data.SqlClient
Imports System.Linq

Public Class frmMailEmail
    Inherits System.Windows.Forms.Form

    Dim isLoaded As Boolean = False
    ' Dim htivw As DataGridView.HitTestInfo
    Dim SrchTotal As Integer 'keep count for display 
    '  Dim source1 As New BindingSource()
    Dim origLength As Integer
    Const origBody As String = "TYPE MESSAGE HERE"

#Region "Load"

    'LOAD
    'loads email grid and adds boiler plate text based on user and region
    Private Sub MailLblSimple_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'LOAD EMAIL GRID
        Me.BindingSource1.DataSource = tblSend
        SrchTotal = GetCount()

        'SET UP SIGNATURE PER REGION
        Me.rtbBody.Text = origBody & NextLine & NextLine & NextLine & usrFirst & NextLine
        If usrJobTitle > "" Then    'optional job title
            Me.rtbBody.AppendText(usrJobTitle & NextLine)
        End If

        Dim sql As New SqlCommand("SELECT OfficeName, OfficeStreet, OfficeCityZip, OfficePhone, OfficeFax + '  (fax)' AS OfficeFax  FROM luRegion WHERE  (RegionName = '" & usrRegion & "')", sc)
        Dim tbCenterAddress As New DataTable
        LoadDataTable(tbCenterAddress, sql)
        Dim r As DataRow = tbCenterAddress.Rows(0)

        Me.rtbBody.AppendText(r("OfficeName") & NextLine & r("OfficeStreet") & NextLine & r("OfficeCityZip") & NextLine & r("OfficePhone"))
        If usrExt > "" Then 'optional phone extension
            Me.rtbBody.AppendText(usrExt)
        End If
        Me.rtbBody.AppendText(NextLine & r("OfficeFax") & NextLine)
        Me.rtbBody.AppendText("www.CenterforCongregations.org" & NextLine & NextLine & "Congregational Resource Guide - Search, Save, Discuss" & NextLine & "www.thecrg.org")
        origLength = rtbBody.Text.Length

        ''OPEN EXCEL  WITH THOSE W NO EMAIL
        'Dim rows As Integer = StrmWriterEmailPrint(Me.txtSubject.Text) 'eventnums, attended
        'If rows > 0 Then
        '    MsgBox("see '" & UserPath & "NoEmails.csv', open in the background", MsgBoxStyle.Information, rows.ToString & " " & sender.tag & " with no email")
        'End If

        Forms.Add(Me)
        isLoaded = True

    End Sub

#End Region 'load

#Region "Email"

    'move from tblSend to tblReject
    Private Sub btnExclude_Click(sender As System.Object, e As System.EventArgs) Handles btnExclude.Click
        If Me.grdvwMain.SelectedRows.Count > 0 Then
        Else
            modGlobalVar.msg(MsgCodes.noRowSelected)
            Exit Sub
        End If
        Dim origRow As DataGridViewRow
        Dim newRow As DataRow 'DataGridViewRow

        For x As Integer = 0 To (Me.grdvwMain.SelectedRows.Count) - 1
            newRow = tblReject.Rows.Add({"email}", "last", "first"})

            origRow = grdvwMain.SelectedRows(x)
InsertintoRejectTable:
            Try
                For y As Integer = 0 To tblSend.Columns.Count - 1
                    newRow.Item(y) = origRow.Cells(y).Value
                Next y
            Catch ex As Exception
            End Try
            tblReject.AcceptChanges()
        Next x
RemoveFromSendGrid:
        For Each r As DataGridViewRow In grdvwMain.SelectedRows
            Try
                Me.grdvwMain.Rows.Remove(r)
            Catch ex As Exception
                MsgBox(ex.Message, , "couldn't delete row") 'org type does not allow nulls - it wasn't null
            End Try
        Next

        tblSend.AcceptChanges()
        GetCount({tblSend.Rows.Count, ""})

    End Sub

    'opportunity to reinstate rejects
    Private Sub btnShowReject_Click(sender As System.Object, e As System.EventArgs) Handles btnShowReject.Click
        'MsgBox(tblReject.Rows.Count.ToString, , "rejects:")

        OpenPopupDGV("Rejected Emails: ", tblReject, True)

    End Sub

    'SEND EMAIL
    Private Sub btnSend_Click(sender As System.Object, e As System.EventArgs) Handles btnSend.Click

        'TODO move these characters to a table for lookup
       
ValidateEmails:
        Dim currentEmail As String
        Dim charArray() As String = {",", ";", ":", "[", "]", "<", ">", "|", "\", "(", ")"}

        For i As Integer = 0 To Me.grdvwMain.Rows.Count - 2
            currentEmail = Me.grdvwMain.Item(0, i).Value
            For k As Integer = 0 To charArray.Count - 1
                If currentEmail.Contains(charArray(k)) Then
                    modGlobalVar.msg("ATTENTION: invalid email", "Email contains invalid character" & charArray(k) & ". " & NextLine & "Fix this email here AND in the Contact Detail:" & NextLine & NextLine & currentEmail, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            Next
            If currentEmail.Count(Function(c As Char) c = "@") = 0 Or currentEmail.Count(Function(c As Char) c = ".") = 0 Or currentEmail.Contains("@.") Then
                modGlobalVar.msg("ATTENTION: invalid email", "Email is incomplete.  Check for the '@' sign or .domain. " & NextLine & "Fix this email here AND in the Contact Detail:" & NextLine & NextLine & currentEmail, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            'NOTE USES LINQ for Ampersand count
            If currentEmail.StartsWith(".") Or currentEmail.ToString.EndsWith(".") Or currentEmail.ToString.EndsWith("@") Or currentEmail.Contains(" or ") Or currentEmail.Contains("MailTo") Or (currentEmail.Count(Function(c As Char) c = "@") > 1) Then
                modGlobalVar.msg("ATTENTION: invalid email", "Email contains extra characters." & NextLine & "Fix this email here AND in the Contact Detail:" & NextLine & NextLine & currentEmail, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

        Next

VerifySubject:
        If txtSubject.Text = String.Empty Or txtSubject.Text = "type Subject line here" Then
            modGlobalVar.msg("ATTENTION: missing information", "Please enter a subject line", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

VerifyBody:  'default:  'type body of message here' = 25 characters
        'default message still embedded?
        If rtbBody.Text.Contains(origBody) Then
            modGlobalVar.msg("ATTENTION: missing information", "Please verify your message; " & NextLine & "( it appears to contain the default sentence: " & NextLine & origBody & ". )", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        'no signature line?
        If rtbBody.Text.Contains("Center for Congregations") Then
        Else
            modGlobalVar.msg("ATTENTION: missing information", "Please verify your signature line; " & NextLine & "( 'Center for Congregations' appears to be missing. )", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        'no message?
        Dim r As Integer = rtbBody.Text.Length  '(max pre-entered signature lines)
        If r <= origLength Then
            modGlobalVar.msg("ATTENTION: missing information", "Please enter a message for the body of your email. " & NextLine & "( your message appears to be shorter than the default closing supplied.  Please verify your message and/or add " & ((r - origLength) * -1).ToString & " spaces.)", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If


        'CONFIRM USER READY
        If modGlobalVar.msg("are you sure?", "click Yes to Send email, No to cancel", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
            '  MsgBox("this will move to delivra routine")
        Else

            Exit Sub
        End If

        MouseWait()

        'ADD SENDER to email list
        Dim LastName, FirstName As String
        LastName = usrFirst.Substring(InStr(usrFirst, " "))
        FirstName = usrFirst.Substring(0, 1)
        tblSend.Rows.Add({usrEmail, LastName, FirstName})
        tblSend.AcceptChanges()

        'SEND EMAIL
        modPopup.BulkEmailDelivra(Me.txtSubject.Text, rtbBody)

        'TODO GET RETURN INFO and send to user
        '  modPopup.EmailOutlook(usrEmail, "confirmation email", "Delivra sent this on your behalf: " & NextLine & NextLine & rtbBody.Text, Nothing)

CloseAll:
        MouseDefault()
        Me.Close()
    End Sub

#End Region 'email

#Region "General"

    Private Sub btnHelp_Click(sender As System.Object, e As System.EventArgs) Handles btnHelp.Click
        msg("EMAIL HELP",
            "The grid shows all the emails you selected. You will automatically be copied on the email." & NextLine & NextLine &
            "SCROLL right in the grid to see data in other columns to confirm this is to whom you intend to send the message." & NextLine & NextLine &
            "To REMOVE someone from this list: highlight row and click Exclude button. These can be restored." & NextLine & NextLine &
            "To ADD an email to this list: enter the Email address in the last row of the grid." & NextLine & NextLine &
            "(Note: NOTHING YOU DO HERE EFFECTS THE DATA IN THE INFORMATION CENTER.)", MessageBoxButtons.OK, MessageBoxIcon.Information)

    End Sub


    'SET STATUS BAR LEFT TEXT
    Private Sub SetStatusBarText(ByVal str As String)
        Me.StatusBarPanel3.Text = str
    End Sub

#End Region 'general

#Region "DataGridView"

    'FILTER GRID VIEW
    Protected Sub grdvwMain_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) _
         Handles grdvwMain.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Right Then
            GetCount(modGlobalVar.FilterDataGridView(sender, e, tblSend, True))
        End If
    End Sub

    'call get count; covers load, adding a row
    Private Sub grdvwMain_RowLeave(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdvwMain.RowLeave
        GetCount({Me.grdvwMain.Rows.Count - 1, ""})
    End Sub

    ' catch error to prevent system stop
    Private Sub grdvwMain_DataError(sender As System.Object, e As System.Windows.Forms.DataGridViewDataErrorEventArgs) Handles grdvwMain.DataError
        modGlobalVar.msg("Missing Data", "  ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    End Sub

    'OPEN CONTACT DETAIL
    Private Sub grdvwMain_MouseDoubleclick(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles grdvwMain.MouseDoubleClick
        Dim icol, colValue As Integer

        If Me.grdvwMain.CurrentRow Is Nothing Then
            Exit Sub
        End If
        colValue = IsNull(Me.grdvwMain.Item(icol, Me.grdvwMain.CurrentRow.Index).Value, 0)
        If colValue = 0 Then
            Exit Sub
        End If

        icol = Me.grdvwMain.CurrentRow.Cells("colContactID").ColumnIndex
        SetStatusBarText("Opening Contact Detail window")
        modGlobalVar.OpenMainContact(Me.grdvwMain.Item(icol, Me.grdvwMain.CurrentRow.Index).Value, Me.grdvwMain.Item(1, Me.grdvwMain.CurrentRow.Index).Value, Me.grdvwMain.Item(Me.grdvwMain.CurrentRow.Cells("colOrgName").ColumnIndex, Me.grdvwMain.CurrentRow.Index).Value, Me.grdvwMain.Item(icol + 1, Me.grdvwMain.CurrentRow.Index).Value)
        SetStatusBarText("Done")
    End Sub

    'update count of emails in grid 
    Private Sub GetCount(ByVal c As String())
        If c(1) = String.Empty Or c(1) = "LEFT" Then
            Me.lblResultCount.Text = "Result: " & GetCount().ToString ' c(0).ToString
        Else
            Me.lblResultCount.Text = "Result: " & c(0).ToString & "/" & SrchTotal.ToString & c(1).ToString
        End If
    End Sub

    'count distinct emails in main grid re duplicates
    Private Function GetCount() As Integer
        Dim tDistinct As New DataTable("tDistinct")
        tDistinct = tblSend.DefaultView.ToTable(True, "Email")
        GetCount = tDistinct.Rows.Count
        tDistinct = Nothing
    End Function

#End Region


End Class

