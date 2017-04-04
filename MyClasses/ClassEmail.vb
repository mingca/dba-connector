Imports System.Net.Mail
Imports System.Text
Imports System.Data.SqlClient
'=========================== EMAIL FUNCTIONS ==============
'TODO make this class self-contained; it uses modPopup
'============================================================
Public Class ClassEmail

    'HTML EMAIL NO ATTACHMENT; line feeds retained in Outlook   --used for msgs to DBAdmin
    Public Sub SendHTMLEmail(ByVal strTo As String, ByVal strSubject As String, ByRef strBody As String)

        Dim oMessage As New Net.Mail.MailMessage()
        oMessage.Subject = strSubject '"This is my subject"
        oMessage.Body = "<html><body>" + strBody + "</a></html></body>"
        oMessage.IsBodyHtml = True
        oMessage.From = New Net.Mail.MailAddress(Environment.UserName & "@centerforcongregations.org")
        oMessage.To.Add(New Net.Mail.MailAddress(strTo)) '"cgreen@centerforcongregations.org"))
        Dim client As New Net.Mail.SmtpClient(strMailClient)
        'oMessage.Priority = MailPriority.High

        Try
            client.Send(oMessage)
        Catch ex As Exception
            modGlobalVar.msg("ERROR: SendHTMLEmail", "please send this message to " & DBAdmin.StaffName & NextLine & ex.Message & NextLine & strSubject, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            oMessage = Nothing
        End Try
    End Sub



    ''HTML EMAIL NO ATTACHMENT;    --used for msgs to DBAdmin re unknown country
    'Public Sub SendHTMLEmail(ByVal strTo As String, ByVal strSubject As String, ByRef strBody As StringBuilder)

    '    Dim oMessage As New Net.Mail.MailMessage()
    '    oMessage.Subject = strSubject '"This is my subject"
    '    ' oMessage.Body = "<html><body>" + strBody.ToString + "</a></html></body>"
    '    oMessage.Body = strBody.ToString
    '    oMessage.IsBodyHtml = True
    '    oMessage.From = New Net.Mail.MailAddress(Environment.UserName & "@centerforcongregations.org")
    '    oMessage.To.Add(New Net.Mail.MailAddress(strTo)) '"cgreen@centerforcongregations.org"))
    '    Dim client As New Net.Mail.SmtpClient(strMailClient)
    '    'oMessage.Priority = MailPriority.High

    '    Try
    '        client.Send(oMessage)
    '    Catch ex As Exception
    '        modGlobalVar.msg("ERROR: SendHTMLEmail", "please send this message to " & DBAdmin.StaffName & NextLine & ex.Message & NextLine & strSubject, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    Finally
    '        oMessage = Nothing
    '    End Try
    'End Sub


    '====

    '    'OPEN EMAIL, GET ADDRESSES FOR OTHERS in SPREADSHEET   --used by edevent Registered or Attended
    '    Public Sub SendEventEmails(ByVal EventID As Integer, ByVal strWho As Integer, ByVal strEventName As String)
    '        MouseWait()
    '        Dim SendEmail As New ClassEmail
    '        Dim strb As New StringBuilder   'emails
    '        Dim strbSN As New StringBuilder 'for snail mail lbls if no email - not used for MailChimp
    '        Dim n As Integer = 0 'without email
    '        Dim cnt As Integer = 0 'with email

    '        Dim sql As New SqlCommand("[EventEmail]", sc)
    '        sql.CommandType = CommandType.StoredProcedure
    '        sql.Parameters.Add("@Attended", SqlDbType.Int).Value = strWho
    '        sql.Parameters.Add("@EventNum", SqlDbType.Int).Value = EventID

    '        If Not SCConnect() Then
    '            Exit Sub
    '        End If
    '        Dim drd As SqlDataReader = sql.ExecuteReader
    '        Dim i As Integer = drd.GetOrdinal("Email")

    '        While drd.Read
    '            If Not IsDBNull(drd(i)) Then 'HAVE EMAIL
    '                If drd.GetString(i) > " " Then    'has email
    '                    strb.Append(drd.GetString(i) & NextLine) ' was for outlook bcc field: ";" not NextLine) & vbLf
    '                    cnt = cnt + 1
    '                Else
    '                    n = n + 1
    '                    strbSN.Append(drd.GetString(drd.GetOrdinal("Name")) & ", ")    'contactid
    '                End If
    '            Else 'NO EMAIL
    '                n = n + 1
    '                strbSN.Append(drd.GetString(drd.GetOrdinal("Name")) & ", ")    'contactid
    '            End If
    '        End While
    '        sc.Close()
    '        drd.Close()
    '        'Dim lenStrb As Integer = strb.Length

    '        Select Case n
    '            Case Is = 0 'everyone has email
    '                If cnt > 0 Then
    '                    ' EmailOutlook(usrName, strEventName, "Everyone has email; put your message here.", strb.ToString)
    '                    ' GoTo closeAll
    '                Else 'no registrants found
    '                    modGlobalVar.Msg(MsgCodes.noResultCancel, "", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '                    GoTo closeAll
    '                End If
    '            Case Is > 0 'someone doesn't have email - do excel list
    '                MouseWait()
    '                If modPopup.StrmWriter("[MergeEvent]", EventID, UserPath & "NoEmailDatadoc", "NoEmail") = True Then 'load data for inspection
    '                Else
    '                    '  modGlobalVar.Msg("streamwriter error")
    '                End If
    '                Try 'excel easier to read than plain notepad
    '                    modPopup.DataToExcel("NoEmailDatadoc", "no Email") 'spreadsheet more legible than .txt
    '                Catch ex As Exception
    '                    ' modGlobalVar.Msg(UserPath & "\NoEmailDatadoc" & ".xls" + NextLine & ex.Message, , "can't convert ")
    '                End Try
    '                ''Aaron says don't open spreadsheet 11/14
    '                'Try
    '                '    System.Diagnostics.Process.Start(UserPath & "\NoEmailDatadoc.xls")
    '                'Catch ex As Exception
    '                '    ' modGlobalVar.Msg(UserPath & "\NoEmailDatadoc" & ".xls" + NextLine & ex.Message, , "can't open ")
    '                'End Try

    '                '   -- "<file:" & UserPath & "dataDoc.xls> "& NextLine &  Left(strbSN.ToString, strbSN.Length - 2) + ".")
    '                'what was this for??   '  & "<a  href='http://www.mywebsite.org/mypage.aspx'> mypage</a>", strb.ToString)

    '        End Select
    '        MouseDefault()
    '        EmailMailChimp(usrName, strEventName, strb.ToString, n, cnt)

    'CloseAll:
    '        strb = Nothing
    '        strbSN = Nothing

    '    End Sub

    ''open .xls with emails only to copy and paste into MailChimp Campaign
    ' ''11/14 re too many emails being sent from outlook
    'Public Sub EmailMailChimp(ByVal strTo As String, ByVal strSubject As String, ByVal strEmails As String, ByVal iNoEmail As Integer, ByVal iWithEmail As Integer)
    '    If iNoEmail > 0 Then
    '        modGlobalVar.Msg("Done. PLEASE NOTE: ", "Note: " + iNoEmail.ToString + " have no email address. see " + UserPath & "NoEmailDatadoc.xls" & NextLine & NextLine &
    '               "Send the " & iWithEmail.ToString & " using MailChimp. " + NextLine +
    '               "See the open Notepad document to add or delete emails before copy/paste into MailChimp List.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '    Else
    '        modGlobalVar.Msg("Done. READY for MAIL CHIMP: ", "Send these " & iWithEmail.ToString & " using MailChimp. " & NextLine & NextLine &
    '               "See the open Notepad document to add or delete emails before copy/paste into MailChimp List.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '    End If
    '    'OPEN MAIL CHIMP Aaron says no
    '    '  modPopup.OpenWebsite("https://login.mailchimp.com/")

    '    'COPY EMAILS TO CLIPBOARD
    '    My.Computer.Clipboard.SetText(strEmails, System.Windows.Forms.TextDataFormat.Text)

    '    'PASTE INTO NOTEPAD AND OPEN FOR EDITING - note: without using clipboard inserts an extra line feed!!
    '    Dim MyAppID = Shell("NOTEPAD.EXE", 1)
    '    AppActivate(MyAppID)
    '    SendKeys.Send("^V") 'was losing line feeds    'send(CTRL - V), Chr(22)'System.Windows.Forms.TextDataFormat.Text)
    '    ' SendKeys.Send(strEmails) '--don't use this Matt is messing around switching windows and the text goes elsewhere
    '    MyAppID = Nothing

    'End Sub

End Class
'WORKS W 365 BUT ALSO WORKS W/OUT THIS LINE ALTOGETHER client.Credentials = CType(Environment.GetEnvironmentVariable("Credentials", EnvironmentVariableTarget.User), System.Net.ICredentialsByHost)    'System.Net.CredentialCache.DefaultNetworkCredentials
' client.Host = "localhost"



''SMTP EMAIL for CONNECTION ERROR    --not used
'Public Sub SendErrorEmail(ByVal strFrom As String, ByVal strTo As String, ByVal strSubject As String, ByVal strMessage As String)            'This procedure takes string array parameters for multiple recipients and files
'    Try
'        ' For Each item As String In strTo
'        ''For each to address create a mail message
'        Dim MailMsg As New System.Net.Mail.MailMessage(New MailAddress(strFrom.Trim()), New MailAddress(strTo.Trim())) 'item))
'        MailMsg.BodyEncoding = Encoding.Default
'        MailMsg.Subject = strSubject.Trim()
'        MailMsg.Body = strMessage.Trim() & NextLine
'        '  MailMsg.Priority = MailPriority.High
'        MailMsg.IsBodyHtml = False

'        ''attach each file attachment
'        'For Each strfile As String In fileList
'        '    If Not strfile = "" Then
'        '        Dim MsgAttach As New Attachment(strfile)
'        '        MailMsg.Attachments.Add(MsgAttach)
'        '    End If
'        'Next

'        'Smtpclient to send the mail mesage                    

'        Dim SmtpMail As New SmtpClient
'        SmtpMail.Host = strMailClient
'        SmtpMail.Send(MailMsg)
'        'Next
'        'Message Successful
'    Catch ex As Exception
'        modGlobalVar.Msg("ERROR: SendErrorEmail", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
'    End Try
'End Sub


''HTML EMAIL NO ATTACHMENT; line feeds retained in Outlook   --NOT USED
'Public Sub TestGEtOpenEmail(ByVal strTo As String, ByVal strSubject As String, ByRef strBody As String)

'    Dim oMessage As New Net.Mail.MailMessage()
'    oMessage.Subject = strSubject '"This is my subject"
'    oMessage.Body = "<html><body>" + strBody.ToString + "</a></html></body>"
'    oMessage.IsBodyHtml = True
'    oMessage.From = New Net.Mail.MailAddress(Environment.UserName & "@centerforcongregations.org")
'    oMessage.To.Add(New Net.Mail.MailAddress(strTo)) '"cgreen@centerforcongregations.org"))
'    Dim client As New Net.Mail.SmtpClient(strMailClient)
'    'oMessage.Priority = MailPriority.High
'    'client.Port = 25
'    client.Credentials = Environment.GetEnvironmentVariable("Credentials")   'System.Net.CredentialCache.DefaultNetworkCredentials

'    Try
'        client.Send(oMessage)
'    Catch ex As Exception
'        modGlobalVar.Msg("ERROR: TestGetOpenEmail", "please send this message to " & DBAdmin.StaffName & NextLine & ex.Message & NextLine & strSubject, MessageBoxButtons.OK, MessageBoxIcon.Error)
'    Finally
'        oMessage = Nothing
'    End Try
'End Sub


