Option Explicit On
'======================== external ============================================
'MERGE, FORMAT WORD, SEND EMAIL, TIMER, ADD NEW, CRG Combo Search, Poups, FiLe IO
'==============================================================================
Imports System.Data.SqlClient
Imports System.Text
Imports System.IO
Imports microsoft.VisualBasic
'Imports System.Net.Mail
Imports Microsoft.Office.Interop.Word
Imports System.Windows.Forms '.Form
Imports Microsoft.Office.Interop
Imports System.Drawing
Imports System.Reflection  'for Outlook task
Imports EXL = Microsoft.Office.Interop.Excel
Imports CWriter = ClassLibrary1.ClassWriter2
Imports cExternalFile = ClassLibrary1.ExternalFiles
'delivra email 11/15 -----
Imports DelivraEmail = ClassEmailDelivra.ClassEmailDelivra
Imports System.Linq 'for enumeratefiles


Module modPopup
    Dim wrdApp As Microsoft.Office.Interop.Word.Application
    Dim wrdDoc As Microsoft.Office.Interop.Word.Document
    Dim strbEmail As New StringBuilder
    Dim strOrgName, strStreet, strCity, strState, strZip, strFirst, strLast, strWho As String
    Dim strResource, strAmount, strDateDue, strDateStarted, strIssue, strMonths As String
    Private col As New Microsoft.VisualBasic.Collection 'crg filter
    Public cboCRG As ComboBox  'crg filter
    Dim z As Integer    'to use in successive ifs
    Dim iStart, iEnd As Integer
    Public iTrvChecked As Integer

#Region "Popup"

    'SIMPLE SPROC SINGLE RESULT, NO FORMATTING
    Public Sub OpenPopupDGV(ByVal strTitle As String, ByVal cmd As SqlCommand)
        Dim newFrm As New frmPopupDatagrid(strTitle, Nothing)
        Dim grd As DataGridView = newFrm.dgvResult
        Dim iNewRow As Integer = 0

        If Forms.find(ClassOpenForms.frmPopupDatagrid) Then
            ClassOpenForms.frmPopupDatagrid.Close()
        End If
        newFrm.Show()
        ClassOpenForms.frmPopupDatagrid = newFrm
        ClassOpenForms.frmPopupDatagrid.Text = strTitle
        cmd.Connection = sc
LoadGrid:
        Dim tb As New Data.DataTable
        Try
            If Not SCConnect() Then
                Exit Sub
            End If
            tb.Load(cmd.ExecuteReader)
        Catch ex As Exception
            modGlobalVar.Msg("ERROR: loading table", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
            GoTo CloseAll
        End Try
        Dim itbl As Integer = tb.Columns.Count
        Dim igrd As Int16 = grd.Columns.Count
        Dim cnt As Integer = 1

AddColumns:
        If itbl - igrd > 0 Then
            For i As Integer = igrd To igrd + (itbl - igrd)
                grd.Columns.Add(i.ToString, "") ', i.ToString)
            Next i
        End If

ColumnHeadings:
        grd.Rows.Add()
        grd.Rows(iNewRow).DefaultCellStyle.Font = New System.Drawing.Font(grd.DefaultCellStyle.Font, FontStyle.Bold)
        For h As Integer = 0 To itbl - 1
            grd(h, iNewRow).Value = tb.Columns(h).ColumnName
        Next h

DataRows:
        For Each r As DataRow In tb.Rows
            '    Me.StatusLabel.Text = " adding rows " & colRegion5lu.Item(iRegion).ToString & " : " & cnt.ToString
            grd.Rows.Add()
            iNewRow = iNewRow + 1 ' iNewRow = grd.NewRowIndex + 1 'grd.Rows.Count - 1
            For c As Integer = 0 To itbl - 1
                grd(c, iNewRow).Value = r.Item(c).ToString
            Next
            If r.Item(0).ToString.Contains("Total") Or r.Item(1).ToString.Contains("Total") Then
                grd.Rows(iNewRow).DefaultCellStyle.Font = New System.Drawing.Font(grd.DefaultCellStyle.Font, FontStyle.Bold)
            End If
            cnt = cnt + 1
        Next r
        grd.Rows.Add(2)    'blank row between runs 
        iNewRow = iNewRow + 2
        tb = Nothing

FormatGrid:
        For Each c As DataGridViewColumn In ClassOpenForms.frmPopupDatagrid.dgvResult.Columns
            If IsDate(grd(c.Index, iNewRow).Value) Then
                grd.Columns(c.Index).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                grd.Columns(c.Index).DefaultCellStyle.Format = "d"
            ElseIf IsNumeric(grd(c.Index, iNewRow).Value) Then
                grd.Columns(c.Index).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                grd.Columns(c.Index).DefaultCellStyle.Format = "n0"
            Else
                grd.Columns(c.Index).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            End If
        Next c

CloseAll:
        Try
            'sc.Close()
            cmd = Nothing

        Catch ex As Exception
            'modGlobalVar.Msg(ex.Message, , "ERROR:  ")
        End Try
    End Sub

    'OPEN DGV w TABLE  Query answer in popup DatagridView; ds = table; called by resource forms
    Public Sub OpenPopupDGV(ByVal strTitle As String, ByRef tb As System.Data.DataTable, ByVal bAllowSort As Boolean)
        Dim newFrm As New frmPopupDatagrid(strTitle, Nothing) 'no progress bar on calling form
        Dim grd As DataGridView = newFrm.dgvResult
        Dim Cnt As Integer
        'Me.StatusProgressBar.Value = 0
        '   Me.StatusLabel.Text = "Retrieving data"
        If Forms.find(ClassOpenForms.frmPopupDatagrid) Then
            Try
                ClassOpenForms.frmPopupDatagrid.Close()
            Catch ex As Exception
            End Try
        End If
        newFrm.Show()
        ClassOpenForms.frmPopupDatagrid = newFrm

LoadGrid:
        'ASSIGN DATASOURCE
        Try
            grd.DataSource = tb
        Catch ex As Exception
            '  Me.StatusProgressBar.Value = 80
            'MsgBox(ex.Message)
        End Try
        Cnt = FormatGrid(grd, bAllowSort)
        '   Me.StatusLabel.Text = "Done"
        '   Me.StatusProgressBar.Value = 100
        ClassOpenForms.frmPopupDatagrid.Text = strTitle & "  " & Cnt.ToString & " rows"

        '   Me.ProgressBar1.Value = 100
        '   Me.StatusBarPanel1.Text = "Done"
    End Sub

    'FORMAT GRID if FROM SINGLE TABLE; include num non total rows in header
    Public Function FormatGrid(ByRef grd As DataGridView, ByVal bAllowSort As Boolean) As Integer
        Dim z As Integer = grd.Columns.Count - 1
        Dim Cnt As Integer
        If z > 3 Then
            z = 3
        End If
        '  Me.StatusLabel.Text = "formatting grid"
        'BOLD TOTALS
        For i As Integer = 0 To ClassOpenForms.frmPopupDatagrid.dgvResult.Rows.Count - 1
            For y As Integer = 0 To z
                If grd(y, i).Value.ToString.Contains("Total") Or grd(y, i).Value.ToString.Contains("TOTAL") Then
                    grd.Rows(i).DefaultCellStyle.Font = New System.Drawing.Font(grd.DefaultCellStyle.Font, FontStyle.Bold)
                    Cnt = Cnt - 1
                    Exit For
                Else

                End If
            Next y
            Cnt = Cnt + 1
        Next
        '  Me.StatusProgressBar.Value = 90

        'SORT
        If bAllowSort = False Then
            For Each c As DataGridViewColumn In ClassOpenForms.frmPopupDatagrid.dgvResult.Columns
                c.SortMode = DataGridViewColumnSortMode.NotSortable
            Next c
        End If
        '   Me.StatusProgressBar.Value = 95

        'ALIGNMENT/DATE
        Dim tb As System.Data.DataTable = grd.DataSource
        For Each c As DataColumn In tb.Columns
            Select Case c.DataType.ToString
                Case Is = "System.Decimal", "System.Int32"
                    If c.ColumnName.Contains("ID") Or c.ColumnName.Contains("Yr") Then
                        grd.Columns(c.Ordinal).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    Else
                        grd.Columns(c.Ordinal).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                        grd.Columns(c.Ordinal).DefaultCellStyle.Format = "n0"
                    End If
                Case Is = "System.DateTime"
                    grd.Columns(c.Ordinal).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    grd.Columns(c.Ordinal).DefaultCellStyle.Format = "d"
                Case Else
                    grd.Columns(c.Ordinal).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            End Select
        Next
        FormatGrid = Cnt
    End Function

    'DATAGRIDVIEW COLUMN ALIGNMENT, Bold, short date
    Public Sub FormatGrid(ByRef grd As DataGridView, ByVal iLastRow As Integer)
        For Each c As DataGridViewColumn In ClassOpenForms.frmPopupDatagrid.dgvResult.Columns
            If IsDate(grd(c.Index, iLastRow).Value) Then
                grd.Columns(c.Index).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                grd.Columns(c.Index).DefaultCellStyle.Format = "d"
            ElseIf IsNumeric(grd(c.Index, iLastRow).Value) Then
                If c.Name.Contains("ID") Or c.Name.Contains("Yr") Then
                    grd.Columns(c.Index).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                Else
                    grd.Columns(c.Index).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    grd.Columns(c.Index).DefaultCellStyle.Format = "n0"
                End If
            Else
                grd.Columns(c.Index).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            End If
        Next c
        'ROW BOLD
        For Each r As DataGridViewRow In grd.Rows
            Try
                If r.Cells(0).Value.ToString.Contains("~ ~") Then
                    r.DefaultCellStyle.Font = New System.Drawing.Font(grd.DefaultCellStyle.Font, FontStyle.Bold)
                End If
            Catch ex As Exception
            End Try
        Next
    End Sub

    'DATAGRIDVIEW allow user to change column size, but first get data appropriate size
    Public Sub FixColumnSize(ByRef grd As DataGridView)
        'note the 2 grid sizing options are always visible, but don't work together!!!

        'build array of auto column sizes
        Dim arColSize(0) As Integer
        ' modGlobalVar.Msg(grd.Columns.Count, , "number of columns")
        For x As Integer = 0 To grd.Columns.Count - 1
            arColSize(x) = grd.Columns(x).Width
            ReDim Preserve arColSize(arColSize.GetUpperBound(0) + 1)
        Next
        'change setting
        grd.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.None
        'implement original column sizes
        For x As Integer = 0 To grd.Columns.Count - 1
            grd.Columns(x).Width = arColSize(x)
        Next
    End Sub

#End Region

#Region "CRG" 'filters CRG combobox by right click

    'FILTER CRG COMBOs, with bound combobox
    Public Sub SearchCRG(ByRef frm As Form, ByVal p As System.Drawing.Point, ByRef ctl As ComboBox)

        Dim pp As ContextMenu = New ContextMenu 'crg filter
        Dim i As Integer = 0
        Dim myValue As Object
        Dim eh As EventHandler = AddressOf SelectCRG
        'GET USER INPUT
        myValue = InputBox("Enter part of CRG term.  (* to restore full list.)", "Search for CRG category")
        If CType(myValue, String) > "" Then '0-length string returned on cancel
        Else
            Exit Sub
        End If
        cboCRG = ctl
        pp.MenuItems.Clear()
        'LOAD POPUPMENU
        pp.MenuItems.Add("Select a category")
        pp.MenuItems.Add("------------------")
        Dim copyRows() As DataRow = tblCRG.Select("CRGName LIKE '%" & myValue & "%'", "CRGName") ' dsCRG.Tables("luCRG").Select("CRGName LIKE '%" & myValue & "%'")
        Dim dar As DataRow
        For Each dar In copyRows
            pp.MenuItems.Add(dar.Item(1), eh)
        Next
        pp.MenuItems(0).DefaultItem = True
        pp.Show(frm, p)

    End Sub

    'SET CBO TO USER SELECTION
    Private Sub SelectCRG(ByVal obj As Object, ByVal ea As EventArgs)
        'NOTE: neither of these trigger dirty
        'Setting the Text property to Nothing or an empty string ("") sets the SelectedIndex to -1. Setting the Text property to a value that is in the Items collection sets the SelectedIndex to the index of that item. Setting the Text property to a value that is not in the collection leaves the SelectedIndex unchanged.
        cboCRG.Text = obj.text
        'cbo.SelectedIndex = cbo.FindStringexact(obj.text)
    End Sub

    '    Public Sub ExpandField(ByVal str As String, ByVal Hdg As String)

    '        Dim path As String = UserPath & "Description.docx"
    '        Dim sw As StreamWriter = New StreamWriter(path)

    '        sw.WriteLine(str)
    '        ' open text file - problem - long lines of text
    '        Try
    '            System.Diagnostics.Process.Start(path)
    '        Catch ex As Exception
    '            modGlobalVar.Msg("ERROR: ExpandField strHdg", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        End Try
    '        ' .....................................
    'CloseWriter:
    '        str = Nothing
    '        sw.Close()

    '    End Sub

    '    Public Sub ExpandField(ByVal str As String)

    '        Dim path As String = UserPath & "Description.doc"
    '        Dim sw As StreamWriter = New StreamWriter(path)

    '        sw.WriteLine(str)
    '        str = Nothing
    '        sw.Close()

    '        ' open text file - problem - long lines of text
    '        Try
    '            System.Diagnostics.Process.Start(path)
    '        Catch ex As Exception
    '            modGlobalVar.Msg("ERROR: ExpandField str", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        End Try
    '        ' .....................................
    'CloseWriter:


    '    End Sub

#End Region

#Region "Undo"

    Public Sub UndoCtl(ByRef ctl As Control)
        'clears combobox - others goes back one edit
        Select Case ctl.GetType.ToString

            Case Is = "System.Windows.Forms.TextBox", "InfoCtr.DateTextBox"
                CType(ctl, System.Windows.Forms.TextBox).Undo()

            Case Is = "System.Windows.Forms.RichTextBox"
                CType(ctl, System.Windows.Forms.RichTextBox).Undo()
                'may need to hit escape 2-3 times for combobox

            Case Is = "System.Windows.Forms.ComboBox", "InfoCtr.ComboBoxRelaxed", "InfoCtr.ComboBoxUSStates"
                CType(ctl, System.Windows.Forms.ComboBox).SelectedIndex = -1
                CType(ctl, System.Windows.Forms.ComboBox).SelectedIndex = -1
                CType(ctl, System.Windows.Forms.ComboBox).Text = ""

            Case Is = "System.Windows.Forms.DateTimePicker"
                CType(ctl, System.Windows.Forms.DateTimePicker).Value = "1/1/1911"

                ' CType(ctl, System.Windows.Forms.DateTimePicker).undo

            Case Is = "System.Windows.Forms.CheckBox"
                CType(ctl, System.Windows.Forms.CheckBox).Checked = Not CType(ctl, System.Windows.Forms.CheckBox).Checked


            Case Else 'System.Windows.Forms.TreeView
                Dim SendEmail As New ClassEmail
                SendEmail.SendHTMLEmail(DBAdmin.StaffEmail, "undo failed", "ObjectType:  " & ctl.GetType.ToString & vbCrLf & "User ID:  " & usr.ToString)
                SendEmail = Nothing

                'MsgBox(ctl.GetType.ToString, , "control type not found")
                ' modGlobalVar.msg("Sorry = cannot undo this type of field", ctl.GetType.ToString & NextLine & "you could Edit -->Cancel Changes.", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Select

    End Sub

#End Region

#Region "Case Rating"

    'TO TBLCASE
    Public Function GetLatestCaseRating(ByVal ID As Integer) As Integer
        Dim cmd As New SqlCommand("SELECT  TOP (1) CaseRating FRoM tblConversation  WHERE CaseNum =" & ID & " ORDER BY ConversDate DESC", sc)
        If Not SCConnect() Then
            Exit Function
        End If
        Try
            Return cmd.ExecuteScalar
        Catch ex As Exception
        End Try
        sc.Close()
    End Function

#End Region

#Region "Class Sound"
    Public Class SoundClass
        Declare Auto Function PlaySound Lib "winmm.dll" (ByVal name _
           As String, ByVal hmod As Integer, ByVal flags As Integer) As Integer
        ' name specifies the sound file when the SND_FILENAME flag is set.
        ' hmod specifies an executable file handle.
        ' hmod must be Nothing if the SND_RESOURCE flag is not set.
        ' flags specifies which flags are set. 

        ' The PlaySound documentation lists all valid flags.
        Public Const SND_SYNC = &H0          ' play synchronously
        Public Const SND_ASYNC = &H1         ' play asynchronously
        Public Const SND_FILENAME = &H20000  ' name is file name
        Public Const SND_RESOURCE = &H40004  ' name is resource name or atom

        Public Sub PlaySoundFile(ByVal filename As String)
            ' Plays a sound from filename.
            PlaySound(filename, Nothing, SND_FILENAME Or SND_ASYNC)
        End Sub
    End Class

#End Region

#Region "ShowStatusDefinitions"

    Public Sub ShowDefinitions(ByVal Which As String)

        Dim qryGetDef As New SqlCommand
        qryGetDef.Connection = sc
        Dim strHeading As String

        If Which = "ResourceType" Then 'use same popup grid as Reports & Utilities QV version of this report
            strHeading = "DEFINITIONS for RESOURCE TYPES and SUBTYPES"
            'This was document, but until someone updates that, this should pull the same string as on the Reports & Utilities page
            If Not SCConnect() Then
                Exit Sub
            End If
            Dim tblTmp As New Data.DataTable("tblTmp")
            Dim cmd As New SqlClient.SqlCommand("SELECT CommandString FROM  luReport WHERE (Report = 'list: resource types')", sc)
            Dim strQry As String = cmd.ExecuteScalar
            sc.Close()
            qryGetDef.CommandText = strQry '"SELECT  TOP (100) PERCENT TypeName, SubtypeName, Description FROM  vwListResourceTypes ORDER BY CASE WHEN MasterType = 0 THEN ResourceTypeID ELSE MasterType END, TypeName DESC, SubtypeName"
            modGlobalVar.LoadDataTable(tblTmp, strQry)
            OpenPopupDGV(strHeading, tblTmp, False)
            tblTmp = Nothing
            Exit Sub
        End If

        Dim frm As New frmRTB
        frm.Show()
        Select Case Which
            Case "Case"
                strHeading = "DEFINITIONS for CASE SEARCH STATUS"
            Case "Grant"
                strHeading = "DEFINITIONS for GRANT SEARCH STATUS"
            Case "GrantDetail"
                strHeading = "DEFINITIONS for Grant DETERMINATION and CLOSURE STATUS"
            Case "Workshop"
                strHeading = "DEFINITIONS for WORKSHOP SEARCH CRITERIA" & NextLine & "(for Word version, see Staff Forms, General)"

        End Select

        qryGetDef.CommandText = "SELECT 20 as StatusName, 100 as Description; SELECT  StatusName, Definition FROM dbo.luStatus WHERE Topic = N'" & Which & "' ORDER BY SortNum"
        LoadRTB(frm.RichTextBox1, qryGetDef, strHeading, False)

        Exit Sub
    End Sub

#End Region 'show case status definitions

#Region "BROWSER"

    Public Sub OpenWebsite(ByVal strPath As String)

        If strPath > "" Then
            Try
                '    System.Diagnostics.Process.Start("IExplore.exe", strPath)'change as per Aaron 3/17 - use default browser
                System.Diagnostics.Process.Start(strPath)
            Catch ex As Exception
                modGlobalVar.msg("ERROR: modPopup.OpenWebsite", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
        '.........................
        'syntax for reportserver
        'http://solomon/ReportServer?%2fTutorial&rs:Command=ListChildren
        ' "http://solomon/ReportServer?%2fTutorial%2f" & reportname & "&rs:Command=Render"
    End Sub

#End Region

#Region "Email"

    'open new email;  ContactDetail
    Public Sub EmailOutlook(ByVal strTo As String)

        '****** works except for Aaron (Office 2016)
        ' Dim mi As Microsoft.Office.Interop.Outlook.MailItem
        '***** try 3/17
        '  Dim outlookType As Type = Type.GetTypeFromProgID("Outlook.Application")
        ' Dim oOutlook As Outlook.Application = CType(System.Activator.CreateInstance(outlookType), Outlook.Application)
        'worked but with error message,so take this line out.
        'Dim mi As Outlook.MailItem

        Dim strbEmail As New StringBuilder
        MouseWait()

        strbEmail.Append("Mailto:" & strTo)
        ' mi =
        System.Diagnostics.Process.Start(strbEmail.ToString)

        MouseDefault()

    End Sub

    'open new email w multiple names in bcc -     ''disabled as per Aaron 11/14 re too many emails being sent from outlook
    'currently used for single address in bcc called by Refund Request and Grant Due Dates
    Public Sub EmailOutlook(ByVal strTo As String, ByVal strSubject As String, ByVal strBody As String, ByVal strBCC As String)

        Dim mi As Microsoft.Office.Interop.Outlook.MailItem
        Dim strbEmail As New StringBuilder

        MouseWait()
        strbEmail.Append("Mailto:" & strTo)
        strbEmail.Append("&subject=" & strSubject) 'Replace(strSubject, Chr(13), "%0d%0a"))
        strbEmail.Append("&body=" & strBody)
        strbEmail.Append("&bcc=" & IsNull(strBCC, ""))

        mi = System.Diagnostics.Process.Start(strbEmail.ToString)
        MouseDefault()

    End Sub

    'open new email w attachment used by frmINTRANET
    Public Sub EmailOutlookAttachment(ByVal strTo As String, ByVal strSubject As String, ByVal strBody As String, ByVal FileName As String)
        Dim oApp As Outlook.Application
        Dim om As Outlook.MailItem

        Try
            oApp = GetObject(, "Outlook.Application")
        Catch ex As System.Exception
            Try
                oApp = CreateObject("Outlook.Application")
            Catch ex2 As Exception
                modGlobalVar.Msg("ERROR: EmailOutlookAttachment ", ex2.Message, 0, MessageBoxIcon.Error)
            End Try
        End Try

        om = oApp.CreateItem(Outlook.OlItemType.olMailItem)
        om.To = strTo
        om.Subject = strSubject
        om.Body = strBody
        om.Attachments.Add(FileName)
        'Multiple attachments: attach each file attachment
        'For Each strfile As String In FileList
        '    If Not strfile = "" Then
        '        Dim MsgAttach As New Attachment(strfile)
        '        MailMsg.Attachments.Add(MsgAttach)
        '        MsgAttach = Nothing
        '    End If
        'Next
        om.Display(False)
        ReleaseComObject(om)
        ReleaseComObject(oApp)
        om = Nothing
        oApp = Nothing

        om = Nothing
        oApp = Nothing
    End Sub

    'send Delivra email
    Public Sub BulkEmailDelivra(ByVal subject As String, ByRef rtb As RichTextBox)
        Dim dEmail As New DelivraEmail
        dEmail.NewMail(tblSend, {usrEmail, usrFirst, subject}, rtb.Text)
    End Sub

    'setup Delivra Email
    Public Sub EmailDelivraEvent(ByVal EventNums As String, ByVal Attended As String)
        'used by event search and event detail
        Dim sql As New SqlCommand("dbo.MailUserEvent", sc)
        sql.CommandType = CommandType.StoredProcedure

        'SET UP DATA
        sql.Parameters.Add("@EventNum", SqlDbType.Text)
        sql.Parameters("@EventNum").Value = EventNums
        sql.Parameters.Add("@Attended", SqlDbType.Text)
        sql.Parameters("@Attended").Value = Attended
        'check for those with no email address


        'OPEN EMAIL  FORM
        OpenEmailForm(sql, "SEND EMAIL to Event " & Attended)



    End Sub

    '    Public Class ClassEmailOld

    '        'SMTP EMAIL for CONNECTION ERROR    --not used
    '        Public Sub SendErrorEmail(ByVal strFrom As String, ByVal strTo As String, ByVal strSubject As String, ByVal strMessage As String)            'This procedure takes string array parameters for multiple recipients and files
    '            Try
    '                ' For Each item As String In strTo
    '                ''For each to address create a mail message
    '                Dim MailMsg As New System.Net.Mail.MailMessage(New MailAddress(strFrom.Trim()), New MailAddress(strTo.Trim())) 'item))
    '                MailMsg.BodyEncoding = Encoding.Default
    '                MailMsg.Subject = strSubject.Trim()
    '                MailMsg.Body = strMessage.Trim() & NextLine
    '                '  MailMsg.Priority = MailPriority.High
    '                MailMsg.IsBodyHtml = False

    '                ''attach each file attachment
    '                'For Each strfile As String In fileList
    '                '    If Not strfile = "" Then
    '                '        Dim MsgAttach As New Attachment(strfile)
    '                '        MailMsg.Attachments.Add(MsgAttach)
    '                '    End If
    '                'Next

    '                'Smtpclient to send the mail mesage                    

    '                Dim SmtpMail As New SmtpClient
    '                SmtpMail.Host = strMailClient '"exchange.cfc.indy"
    '                SmtpMail.Send(MailMsg)
    '                'Next
    '                'Message Successful
    '            Catch ex As Exception
    '                modGlobalVar.Msg(ex.Message, MessageBoxIcon.Error, "ERROR: SendErrorEmail")
    '            End Try
    '        End Sub


    '        'OPEN EMAIL, GET ADDRESSES FOR OTHERS in SPREADSHEET   --used by edevent Registered or Attended
    '        Public Sub SendEventEmails(ByVal EventID As Integer, ByVal strWho As Integer, ByVal strEventName As String)
    '            MouseWait()
    '            Dim SendEmail As New ClassEmail
    '            Dim strb As New StringBuilder   'emails
    '            Dim strbSN As New StringBuilder 'for snail mail lbls if no email - not used for MailChimp
    '            Dim n As Integer = 0 'without email
    '            Dim cnt As Integer = 0 'with email

    '            Dim sql As New SqlCommand("[EventEmail]", sc)
    '            sql.CommandType = CommandType.StoredProcedure
    '            sql.Parameters.Add("@Attended", SqlDbType.Int).Value = strWho
    '            sql.Parameters.Add("@EventNum", SqlDbType.Int).Value = EventID

    '            If Not SCConnect() Then
    '                Exit Sub
    '            End If
    '            Dim drd As SqlDataReader = sql.ExecuteReader
    '            Dim i As Integer = drd.GetOrdinal("Email")

    '            While drd.Read
    '                If Not IsDBNull(drd(i)) Then 'HAVE EMAIL
    '                    If drd.GetString(i) > " " Then    'has email
    '                        strb.Append(drd.GetString(i) & NextLine) ' was for outlook bcc field: ";" not NextLine) & vbLf
    '                        cnt = cnt + 1
    '                    Else
    '                        n = n + 1
    '                        strbSN.Append(drd.GetString(drd.GetOrdinal("Name")) & ", ")    'contactid
    '                    End If
    '                Else 'NO EMAIL
    '                    n = n + 1
    '                    strbSN.Append(drd.GetString(drd.GetOrdinal("Name")) & ", ")    'contactid
    '                End If
    '            End While
    '            sc.Close()
    '            drd.Close()
    '            'Dim lenStrb As Integer = strb.Length

    '            Select Case n
    '                Case Is = 0 'everyone has email
    '                    If cnt > 0 Then
    '                        ' EmailOutlook(usrName, strEventName, "Everyone has email; put your message here.", strb.ToString)
    '                        ' GoTo closeAll
    '                    Else 'no registrants found
    '                        modGlobalVar.Msg("   no results found    ", MessageBoxIcon.Information, "cancelling request")
    '                        GoTo closeAll
    '                    End If
    '                Case Is > 0 'someone doesn't have email - do excel list
    '                    MouseWait()
    '                    If modPopup.StrmWriter("[MergeEvent]", EventID, UserPath & "NoEmailDatadoc", "NoEmail") = True Then 'load data for inspection
    '                    Else
    '                        '  modGlobalVar.Msg("streamwriter error")
    '                    End If
    '                    Try 'excel easier to read than plain notepad
    '                        DataToExcel("NoEmailDatadoc", "no Email") 'spreadsheet more legible than .txt
    '                    Catch ex As Exception
    '                        ' modGlobalVar.Msg(UserPath & "\NoEmailDatadoc" & ".xls" + NextLine & ex.Message, , "can't convert ")
    '                    End Try
    '                    ''Aaron says don't open spreadsheet 11/14
    '                    'Try
    '                    '    System.Diagnostics.Process.Start(UserPath & "\NoEmailDatadoc.xls")
    '                    'Catch ex As Exception
    '                    '    ' modGlobalVar.Msg(UserPath & "\NoEmailDatadoc" & ".xls" + NextLine & ex.Message, , "can't open ")
    '                    'End Try

    '                    '   -- "<file:" & UserPath & "dataDoc.xls> "& NextLine &  Left(strbSN.ToString, strbSN.Length - 2) + ".")
    '                    'what was this for??   '  & "<a  href='http://www.mywebsite.org/mypage.aspx'> mypage</a>", strb.ToString)

    '            End Select
    '            MouseDefault()
    '            EmailMailChimp(usrName, strEventName, strb.ToString, n, cnt)

    'CloseAll:
    '            strb = Nothing
    '            strbSN = Nothing

    '        End Sub

    '        'HTML EMAIL NO ATTACHMENT; line feeds retained in Outlook   --used for msgs to DBAdmin
    '        Public Sub SendHTMLEmail(ByVal strTo As String, ByVal strSubject As String, ByRef strBody As String)

    '            Dim oMessage As New Net.Mail.MailMessage()
    '            oMessage.Subject = strSubject '"This is my subject"
    '            oMessage.Body = "<html><body>" + strBody.ToString + "</a></html></body>"
    '            oMessage.IsBodyHtml = True
    '            oMessage.From = New Net.Mail.MailAddress(Environment.UserName & "@centerforcongregations.org")
    '            oMessage.To.Add(New Net.Mail.MailAddress(strTo)) '"cgreen@centerforcongregations.org"))
    '            Dim client As New Net.Mail.SmtpClient(strMailClient)
    '            'oMessage.Priority = MailPriority.High
    '            'client.Port = 25
    '            client.Credentials = CType(Environment.GetEnvironmentVariable("Credentials"), System.Net.ICredentialsByHost)    'System.Net.CredentialCache.DefaultNetworkCredentials

    '            Try
    '                client.Send(oMessage)
    '            Catch ex As Exception
    '                modGlobalVar.Msg("please send this message to " & DBAdmin.StaffName& NextLine &  ex.Message & NextLine &  strSubject, MessageBoxIcon.Error, "ERROR: SendHTMLEmail")
    '            Finally
    '                oMessage = Nothing
    '            End Try
    '        End Sub


    '        'HTML EMAIL NO ATTACHMENT; line feeds retained in Outlook   --used for msgs to DBAdmin
    '        Public Sub TestGEtOpenEmail(ByVal strTo As String, ByVal strSubject As String, ByRef strBody As String)

    '            Dim oMessage As New Net.Mail.MailMessage()
    '            oMessage.Subject = strSubject '"This is my subject"
    '            oMessage.Body = "<html><body>" + strBody.ToString + "</a></html></body>"
    '            oMessage.IsBodyHtml = True
    '            oMessage.From = New Net.Mail.MailAddress(Environment.UserName & "@centerforcongregations.org")
    '            oMessage.To.Add(New Net.Mail.MailAddress(strTo)) '"cgreen@centerforcongregations.org"))
    '            Dim client As New Net.Mail.SmtpClient(strMailClient)
    '            'oMessage.Priority = MailPriority.High
    '            'client.Port = 25
    '            client.Credentials = Environment.GetEnvironmentVariable("Credentials")   'System.Net.CredentialCache.DefaultNetworkCredentials

    '            Try
    '                client.Send(oMessage)
    '            Catch ex As Exception
    '                modGlobalVar.Msg("please send this message to " & DBAdmin.StaffName& NextLine &  ex.Message & NextLine &  strSubject, MessageBoxIcon.Error, "ERROR: TestGetOpenEmail")
    '            Finally
    '                oMessage = Nothing
    '            End Try
    '        End Sub

    '    End Class


#End Region

#Region "TEXT BOXES"

    'SEARCH RTB -- 'not used??
    Public Function FindMyText(ByVal text As String, ByVal start As Integer, ByRef RTB As RichTextBox) As Integer

        Dim returnValue As Integer = -1 ' Initialize the return value to false by default.
        If text.Length > 0 And start >= 0 Then
            ' get location of the search string in richTextBox1.
            Dim indexToText As Integer = RTB.Find(text, start, RichTextBoxFinds.MatchCase, RichTextBoxFinds.None)
            If indexToText >= 0 Then ' Determine whether the text was found in richTextBox1.
                returnValue = indexToText
            End If
        End If

        Return returnValue
    End Function

#End Region 'text boxes

    'TODO URGENT - set max length of text boxes

#Region "WORD MERGE"

    'NOTES ON MERGING
    ' http://msdn.microsoft.com/en-us/library/aa140183(office.10).aspx
    ' Word does not reconize field and record delimimters in txt files so will ask.  No workaround (can print column headings twice but still will ask for record delimiter)

    'used by ContactDetail from;  SEND LETTER - SINGLE ADDRESS, SELECT LETTER or CREATE BLANK DOC
    Public Sub MailLetter(ByVal strDoc As String, ByVal bExisting As Boolean, ByVal OrgID As Integer, ByVal striWho As String, ByVal striOrgName As String, ByVal striStreet As String, ByVal striCity As String, ByVal striState As String, ByVal striZip As String, ByVal strFirstName As String, ByVal strLastName As String, ByVal Resources As String, ByVal Amount As String, ByVal DateDue As String, ByVal Issue As String, ByVal Months As String, ByVal DateStarted As String)

        Dim wrdSelection As Microsoft.Office.Interop.Word.Selection
        Dim wrdMailMerge As Microsoft.Office.Interop.Word.MailMerge
        '      Dim wrdMergeFields As Microsoft.Office.Interop.Word.MailMergeFields

        ' Dim StrToAdd As String

        ' Create an instance of Word, and make it visible.
        wrdApp = CreateObject("Word.Application")
        wrdApp.Visible = True

        '.......................
        '........................

        MouseWait()
        strStreet = striStreet 'String.Empty
        strCity = striCity 'String.Empty
        strZip = striZip 'String.Empty
        strState = striState 'String.Empty
        strAmount = strAmount 'String.Empty
        strFirst = strFirstName
        strLast = strLastName
        strWho = striWho

A1AddressSuppliedGetOrgName:
        'GET ADDRESS INFO
        'TODO ADD use home address check
        If Not strStreet = String.Empty Then 'use home address, otherwise use org address
            Dim cmd As New SqlCommand("SELECT OrgName FROM dbo.tblOrg WHERE OrgID = " & OrgID, sc)
            If Not SCConnect() Then
                Exit Sub
            End If
            Dim drdr As SqlDataReader = cmd.ExecuteReader
            drdr.Read()
            If Not IsDBNull(drdr(0)) Then
                strOrgName = drdr.GetString(0)
            End If
            drdr.Close()
            sc.Close()
            'strStreet 
            'strCity = Nz(Me.City)
            'strState = Nz(Me.State)
            'strZip = Nz(Me.Zip)

B1GetAddressAndOrgName:
            'grantReportRequest, amount, dates, issue; IntroLetter, RequestFeedback: resources, dates
        Else
            Dim cmd As New SqlCommand("SELECT OrgName, Street1, City, State, Zip FROM dbo.tblOrg WHERE OrgID = " & OrgID, sc)
            If Not SCConnect() Then
                Exit Sub
            End If


            Dim drdr As SqlDataReader = cmd.ExecuteReader
            drdr.Read()
            If Not IsDBNull(drdr(0)) Then
                strOrgName = drdr.GetString(0)
            End If
            If Not IsDBNull(drdr(1)) Then
                strStreet = drdr.GetString(1)
            End If
            If Not IsDBNull(drdr(2)) Then
                strCity = drdr.GetString(2)
            End If
            If Not IsDBNull(drdr(3)) Then
                strState = drdr.GetString(3)
            End If
            If Not IsDBNull(drdr(4)) Then
                strZip = drdr.GetString(4)
            End If
            drdr.Close()
            sc.Close()
        End If

A2SingleAddressCreateDocument:
        'GET DOCUMENT
        '............................
        Select Case bExisting
            Case False  'CREATE NEW DOC
                ' Add a new document.
                wrdDoc = wrdApp.Documents.Add
                '                .......................
                'for single line spacing of address regardless of user setting
                Dim iAfter As Integer = wrdDoc.Paragraphs.SpaceAfter
                Dim iBefore As Integer = wrdDoc.Paragraphs.SpaceBefore
                Dim iLineSpace As WdLineSpacing = wrdDoc.Paragraphs.LineSpacingRule '=wdLineSpaceRule1pt5
                '   modGlobalVar.Msg(iLineSpace.ToString, , "ilinespace")
                '  WdLineSpacing.wdLineSpaceSingle 'wdLineSpaceRule1pt5

                '  wrdDoc.SelectAllEditableRanges()
                wrdSelection = wrdApp.Selection '??
                With wrdSelection.Paragraphs
                    .LineSpacingRule = WdLineSpacing.wdLineSpaceSingle 'WdLineSpacing(wdlinespacesingle) ' 'WdLineSpacing.wdLineSpaceSingle 'WdLineSpacing.wdLineSpaceSingle '"WdLineSpace1pt5" '
                    .SpaceBefore = 0
                    .SpaceAfter = 0
                End With

                '........................

                wrdDoc.Select()
                wrdSelection = wrdApp.Selection '??

                With wrdSelection
                    .PageSetup.LeftMargin = 130
                    .TypeParagraph()
                    .TypeParagraph()
                    .TypeParagraph()
                    .TypeText(Text:=Now.ToLongDateString)
                    .TypeParagraph()
                    .TypeParagraph()
                    .TypeText(Text:=strWho)
                    .TypeParagraph()
                    .TypeText(Text:=strOrgName)
                    .TypeParagraph()
                    .TypeText(Text:=strStreet)
                    .TypeParagraph()
                    .TypeText(Text:=strCity & ", " & strState & "  " & strZip)
                    .TypeParagraph()
                    .TypeParagraph()
                    .TypeParagraph()
                    .TypeText(Text:="Dear " & strFirstName & " " & strLastName & ":")
                    .TypeParagraph()
                    .TypeParagraph()
                End With

                wrdApp.Visible = True

                wrdSelection = wrdApp.Selection '??
                ' wrdSelection.Paragraphs.LineSpacing = iLineSpace
                wrdSelection.Paragraphs.SpaceBefore = iBefore
                wrdSelection.Paragraphs.SpaceAfter = iAfter

                GoTo done


                'B2SingleAddressMergeWithExistingDocument:
                'MERGE W EXISTING DOCUMENT
            Case True

                'open existing document
                wrdDoc = wrdApp.Documents.Open(strDoc)   'C:\State University.doc")
                wrdDoc.Select()

                wrdSelection = wrdApp.Selection
                wrdMailMerge = wrdDoc.MailMerge

                '...............................................
                '............................................................
                ' Create the MailMerge Data file for Single Address.

                CreateMailMergeDataFile(Resources, Amount, DateDue, Issue, Months, DateStarted) ', Issue, Months)  'include extras here
                '...............................................
                '............................................................

                ' Perform mail merge.
                wrdMailMerge.Destination = WdMailMergeDestination.wdSendToNewDocument
                Try
                    wrdMailMerge.Execute(False)
                Catch ex As Exception
                    modGlobalVar.Msg("ERROR: MaiLetter, merge failed", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    '   modGlobalVar.Msg(ex.Message, MessageBoxIcon.Error Or vbmodGlobalVar.MsgSetForeground, "ERROR: MaiLetter, merge failed")
                End Try

                ' Close the original form document.
                wrdDoc.Saved = True
                wrdDoc.Close(False)

                '....................................
Done:
                ' Notify user we are done.
                'modGlobalVar.Msg("Mail Merge Complete.", vbmodGlobalVar.MsgSetForeground)
                modGlobalVar.Msg("Mail Merge Complete.", "see Word document.", MessageBoxButtons.OK, MessageBoxIcon.Information)
Release:
                ReleaseComObject(wrdSelection)
                ReleaseComObject(wrdMailMerge)
                ' wrdDoc.Close()
                ReleaseComObject(wrdDoc)
                '  wrdApp.Close(False)
                ReleaseComObject(wrdApp)
                wrdSelection = Nothing
                wrdDoc = Nothing
                wrdApp = Nothing

                ' Release references.
                '   wrdSelection = Nothing
                'wrdMailMerge = Nothing
                'wrdMergeFields = Nothing
                'wrdDoc = Nothing
                'wrdApp = Nothing

                ' Clean up the temp file.
                ' Kill("C:\DataDoc.doc")'leave this so Janice can edit data
        End Select

        MouseDefault()
    End Sub

#Region "CREATE MERGE DATAFILE x2"

    'CREATE DATASOURCE FILE - SINGLE ADDRESS + 2 extra fields = 11, calls fillRow
    Public Sub CreateMailMergeDataFile(ByVal Resources As String, ByVal Amount As String, ByVal DateDue As String, ByVal Issue As String, ByVal Months As String, ByVal DateStarted As String) ', ByVal str3 As String, ByVal str4 As String, ByVal str5 As String) 'Accepts 2 datafields other than address

        Dim wrdDataDoc As Microsoft.Office.Interop.Word.Document
        'Dim iCount As Integer

        MouseWait()
        ' Create a data source at C:\DataDoc.doc that contains the field data.
        wrdDoc.MailMerge.CreateDataSource(Name:=UserPath & "DataDoc.doc", HeaderRecord:="OrgName, FullName,  Prefix, FirstName, LastName,  Suffix, Street, CityStateZip, Resources, Amount, DateDue, Issue, Months, DateStarted")
        ' Open the file to insert the data.
        wrdDataDoc = wrdApp.Documents.Open(UserPath & "DataDoc.doc")
        'why add these rows
        'For iCount = 1 To 2
        '    wrdDataDoc.Tables(1).Rows.Add()
        'Next iCount
        ' Fill in the data.
        Dim strCityStateZip As String
        strCityStateZip = strCity & ", " & strState & "  " & strZip


        'SINGLE ROW MERGE w NO NAME
        FillRow(wrdDataDoc, 2, strOrgName, strWho, "", strFirst, strLast, "", strStreet, strCityStateZip, Resources, Amount, DateDue, Issue, Months, DateStarted) ', str3, str4)
        'FillRow(wrdDataDoc, 3, "Jan", "Miksovsky", _
        '      "1234 5th Street", "Charlotte, NC  98765")
        'FillRow(wrdDataDoc, 4, "Brian", "Valentine", _
        '      "12348 78th Street  Apt. 214", "Lubbock, TX  25874")
        ' Save and close the file.
        wrdDataDoc.Save()
        Try
            wrdDataDoc.Close(False)
        Catch ex As Exception
            ' modGlobalVar.Msg(ex.Message, MessageBoxIcon.Error, "can't close word")
        End Try
        wrdDataDoc = Nothing
        MouseDefault()
        ' modGlobalVar.Msg("out of create datafile")
    End Sub

    ''MERGE MULTIPLE NAMES/ADDRESSES;  parameter for storedproc which determines # of columns filled
    'Public Sub MergeMultiple(ByVal strSP As String, ByVal doMerge As String, ByVal mtemplate As String, ByVal Datadoc As String, ByVal ID As Integer)
    '    Dim wrdDataDoc As Microsoft.Office.Interop.Word.Document
    '    Dim wrdSelection As Microsoft.Office.Interop.Word.Selection
    '    Dim wrdMailMerge As Microsoft.Office.Interop.Word.MailMerge
    '    Dim wrdMergeFields As Microsoft.Office.Interop.Word.MailMergeFields

    '    ' modGlobalVar.Msg(Datadoc, , "merge multiple")
    '    ' Dim iCount As Integer
    '    MouseWait()
    '    wrdApp = CreateObject("Word.Application")
    '    wrdApp.Visible = True
    '    wrdDoc = wrdApp.Documents.Open(mtemplate) '"\\iccnas1\shared\Standard Operating Procedures\MergeInfoCtrtoWord\EdEventInfoLettervs.doc")
    '    ''...............................................

    '    ' ''SET TITLES
    '    ' '' Create a data source at C:\DataDoc.doc that contains the field data.
    '    Select Case strSP
    '        Case "[MergeNametag]"
    '            wrdDoc.MailMerge.CreateDataSource(Name:=Datadoc, HeaderRecord:="EventName, OrgName, Registrant")
    '        Case Else   'requires name and address fields
    '            wrdDoc.MailMerge.CreateDataSource(Name:=Datadoc, HeaderRecord:="OrgName, FullName,  Prefix, FirstName, LastName,  Suffix, Street, CityStateZip, Resources, Amount, DateDue, Issue, Months, DateStarted")
    '    End Select
    '    '' Open the file to insert the data.
    '    'wrdDataDoc = wrdApp.Documents.Open(Datadoc) '"C:\NametagData.doc")   '"C:\DataDoc.doc")

    '    ''FILL IN DATA
    '    'FillRow(strSP, wrdDataDoc, ID)
    '    ''.....................................

    '    'PERFORM MERGE
    '    If doMerge = "nomerge" Then  'user just needs data, will select own document to merge from, leave datadoc open
    '        ' Release references.
    '        wrdDoc.Close(False)
    '        wrdSelection = Nothing
    '        wrdMailMerge = Nothing
    '        wrdMergeFields = Nothing
    '        wrdDoc = Nothing
    '        wrdApp = Nothing
    '        wrdDataDoc.Save()

    '        ' Notify user we are done.
    '        modGlobalVar.Msg("Merge action complete", "Here is your datafile.  Use your own document to perform the merge using this data.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '    Else
    '        wrdDoc = wrdApp.Documents.Open(mtemplate)
    '        wrdDoc.Select()
    '        wrdSelection = wrdApp.Selection
    '        wrdMailMerge = wrdDoc.MailMerge

    '        ' Perform mail merge.
    '        Try
    '            wrdMailMerge.Destination = WdMailMergeDestination.wdSendToNewDocument
    '        Catch ex As Exception
    '            modGlobalVar.Msg("ERROR: no destination", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        End Try
    '        wrdMailMerge.Execute(False)


    '        ' Close the original form document.
    '        wrdDoc.Saved = True
    '        wrdDoc.Close(False)


    '        ' Release references.
    '        wrdSelection = Nothing
    '        wrdMailMerge = Nothing
    '        wrdMergeFields = Nothing
    '        wrdDoc = Nothing
    '        wrdApp = Nothing

    '        ' wrdDataDoc.Save()
    '        ' wrdDataDoc.Close(False)

    '        ' Clean up the temp file.
    '        '   Kill(Datadoc)

    '        ' Notify user we are done.    
    '        modGlobalVar.Msg("Merge Complete.", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '        '  modGlobalVar.Msg(" ".PadLeft(30, ""), MessageBoxIcon.Information Or vbmodGlobalVar.MsgSetForeground, "Merge Complete.")
    '    End If
    '    MouseDefault()
    'End Sub

#End Region 'creat merge data file

#Region "FILL MERGE DATAFILE"
    'FILL WORD TABLE W DATA - SINGLE ADDRESS; manual columns
    '"OrgName, FullName,  Prefix, FirstName, LastName,  Suffix, Street, CityStateZip, Resources, Amount, DateDue, Issue, Months, DateStarted")
    Public Sub FillRow(ByVal DataDoc As Microsoft.Office.Interop.Word.Document, ByVal Row As Integer, ByVal OrgName As String, ByVal FullName As String, _
    ByVal Prefix As String, ByVal FirstName As String, ByVal LastName As String, ByVal Suffix As String, ByVal Street As String, ByVal CityStateZip As String, ByVal Resources As String, ByVal Amount As String, ByVal DateDue As String, ByVal Issue As String, ByVal Months As String, ByVal DateStarted As String)

        With DataDoc.Tables(1)
            ' Insert the data into the specific cell.
            .Cell(Row, 1).Range.InsertAfter(OrgName)
            .Cell(Row, 2).Range.InsertAfter(FullName)
            .Cell(Row, 3).Range.InsertAfter(Prefix)
            .Cell(Row, 4).Range.InsertAfter(FirstName)
            .Cell(Row, 5).Range.InsertAfter(LastName)
            .Cell(Row, 6).Range.InsertAfter(Suffix)
            .Cell(Row, 7).Range.InsertAfter(Street)
            .Cell(Row, 8).Range.InsertAfter(CityStateZip)
            .Cell(Row, 9).Range.InsertAfter(Resources)
            .Cell(Row, 10).Range.InsertAfter(Amount)
            .Cell(Row, 11).Range.InsertAfter(DateDue)
            .Cell(Row, 12).Range.InsertAfter(Issue)
            .Cell(Row, 13).Range.InsertAfter(Months)
            .Cell(Row, 14).Range.InsertAfter(DateStarted)
        End With
    End Sub

#End Region 'fill rows in datafile

    '    '1 used by nametags, lib labels, IntroLetter, grant letters - merges without 'select' question
    Public Sub MergePerform(ByVal mtemplate As String, ByVal datafileName As String, ByVal sheetLabel As String)
        'datadoc contains full path
        Dim wrdApp As Word.Application
        Dim objWord As Word.Document
        'in case user doesn't have word open
        wrdApp = CreateObject("Word.Application")
        wrdApp.Visible = True

        Try
            objWord = GetObject(mtemplate) ', "Word.document")
        Catch ex As Exception
            modGlobalVar.msg("couldn't get template", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Dim strDataPath As String = UserPath & datafileName & ".xls" 'CType(Datadoc & ".xls", String)
        Dim strSQL As String = CType("SELECT * FROM [" & sheetLabel & "$]", String)

        '  MailMerge.MainDocumentType to wdNotAMailMergeDocument
        ' modGlobalVar.Msg(objWord.MailMerge.DataSource, , "default")

        With objWord
            ' .MailMerge.OpenDataSource(strDataPath, , , , , , , , , , , , strSQL) ', , , WdMailMergeDataSource.wdMergeInfoFromExcelDDE) 'wdMergeSubTypeWord2000") ') 'forces DDE, still hase Select Table dialog) ', , False, , , , , , , , , "DataLibLbl$", "SELECT * FROM 'DataLibLbl$'") ', , , , WdOpenFormat.wdMergeSubTypeAccess) ', , , , WdMailMergeDataSource.wdMergeInfoFromExcelDDE) ', WdOpenFormat.wdOpenFormatText) ', LinkToSource:=True Connection:="Table DataLibLbl$")
            .MailMerge.OpenDataSource(Name:=strDataPath, LinkToSource:=True, SQLStatement:=strSQL)
            .MailMerge.Destination = WdMailMergeDestination.wdSendToNewDocument
            'PAUSE = "True for Microsoft Word pause and display a troubleshooting dialog box if a mail merge error is found. False to report errors in a new document"  You do not want to set this to true or the default; set it to FALSE.
            Try
                .MailMerge.Execute(False)
                '   .Application.Visible = True
            Catch ex As Exception
                modGlobalVar.Msg("ERROR executing merge", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
                GoTo CloseAll
            End Try
        End With

        modGlobalVar.Msg("Merge Complete", objWord.MailMerge.DataSource.RecordCount.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Information)

CloseAll:  ' Release references.
        objWord.Close(False)
        strDataPath = Nothing
        strSQL = Nothing
        ReleaseComObject(objWord)
        ReleaseComObject(wrdApp)
        objWord = Nothing
        wrdApp = Nothing
        'System.Runtime.InteropServices.Marshal.ReleaseComObject(objWord)
        MouseDefault()


    End Sub

    '3 used by INFO LETTER - ATTACH HEADER but open original document for editing before manual merge
    Public Sub MergePerform(ByVal mtemplate As String, ByVal strDataXls As String, ByVal bMerge As Boolean)
        'datadoc contains full path
        Dim wrdApp As Word.Application
        Dim objWord As Word.Document
        'in case user doesn't have word open
        wrdApp = CreateObject("Word.Application")
        wrdApp.Visible = True

        Try
            objWord = GetObject(mtemplate) ', "Word.document")
        Catch ex As Exception
            modGlobalVar.Msg("couldn't get template", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Dim strDataPath As String = UserPath & strDataXls & ".xls" 'CType(Datadoc & ".xls", String)
        Dim strSQL As String = CType("SELECT * FROM [EventIntro$]", String)
        ' Dim strSQL As String = CType("SELECT * FROM [" & Datadoc.Substring(Len(UserPath) + 1) & "$]", String)

        objWord.MailMerge.OpenDataSource(Name:=strDataPath, LinkToSource:=True, SQLStatement:=strSQL)
        objWord.MailMerge.Destination = WdMailMergeDestination.wdSendToNewDocument

        ' objWord.MailMerge.OpenDataSource(strDataPath, , , , , , , , , , , , strSQL) ', , , WdMailMergeDataSource.wdMergeInfoFromExcelDDE) 'wdMergeSubTypeWord2000") ') 'forces DDE, still hase Select Table dialog) ', , False, , , , , , , , , "DataLibLbl$", "SELECT * FROM 'DataLibLbl$'") ', , , , WdOpenFormat.wdMergeSubTypeAccess) ', , , , WdMailMergeDataSource.wdMergeInfoFromExcelDDE) ', WdOpenFormat.wdOpenFormatText) ', LinkToSource:=True Connection:="Table DataLibLbl$")
        ' objWord.Application.Visible = True
CloseAll:

        '        System.Runtime.InteropServices.Marshal.ReleaseComObject(objWord)
        '        objWord.Close(False)
        ReleaseComObject(objWord)
        ReleaseComObject(wrdApp)
        objWord = Nothing
        wrdApp = Nothing
        strDataPath = Nothing
        strSQL = Nothing
        MouseDefault()

    End Sub

#End Region 'word merge

#Region "Set Suffix Prefix"
    Public Function GetPrefix(ByVal p As String) As String
        If p = String.Empty Then
            Return p
        Else
            Return p + " "
        End If
    End Function

    Public Function GetSuffix(ByVal s As String) As String
        If s = String.Empty Then
            Return s
        Else
            Return ", " & s
        End If
    End Function    'set prefix/suffix

#End Region '

#Region "Export text to Excel"

    '1 SAVE  .txt datafile as .xls EXCEL SPREADSHEET for merges or viewing; return full spreadsheet name
    Public Function DataToExcel(ByVal dataFileName As String, excelSheetName As String) As String
        '-- 8/2013 ---
        '-- lib labels, nametag, grant, maillbl, resource
        '-- save without prompting user
        '-- cg -----
        '  MouseWait()

        '**************** works except for matt
        'Dim oExcel As Object
        'Create a new instance of Excel.
        ' oExcel = CreateObject("Excel.Application")
        '***********************
        '*** try 3/17 ********************
        Dim excelType As Type = Type.GetTypeFromProgID("Excel.Application")
        Dim oExcel As Excel.Application = CType(System.Activator.CreateInstance(excelType), Excel.Application)
        '*****************************

        oExcel.DisplayAlerts = False
        Dim sht As Excel.Worksheet
        Dim strPathFileExt As String = UserPath & dataFileName & ".xls" ' & strExtension '".xls"
        Try
            'Open the text file and save it in the Excel workbook format.
            oExcel.Workbooks.OpenText(filename:=UserPath & dataFileName & ".txt", DataType:=Excel.XlTextParsingType.xlDelimited, TAB:=True)
            'oExcel.activeworkbook.saved = True 'still prompts user

            sht = oExcel.ActiveSheet
            sht.Name = excelSheetName

            oExcel.ActiveWorkbook.SaveAs(strPathFileExt, 56) 'xlWorkbookNormal = -4143
            DataToExcel = strPathFileExt
        Catch ex As System.Exception
            modGlobalVar.Msg("error: in DataToExcel", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
            DataToExcel = ""
        End Try
        'Cleanup Excel
        Try
            oExcel.Quit()
            ReleaseComObject(oExcel.workbooks)
            ReleaseComObject(oExcel)
            sht = Nothing
            oExcel = Nothing
            GC.Collect()
        Catch ex As System.Exception
        End Try
        ''========the following works but gives user notice that is in wrong format; merge doesn't happen====
        'Try
        '    My.Computer.FileSystem.CopyFile(strDataPathName & ".txt", strDataPathName & ".xls", True)
        'Catch ex As System.Exception
        '    modGlobalVar.Msg(ex.Message, , "ERROR: copying file  ")
        'End Try
        ''===========================

        '  MouseDefault()

        '       'another way - from snippet
        '        Dim fileExists As Boolean
        '        fileExists = My.Computer.FileSystem.FileExists("C:\Test.txt")
        '  My.Computer.FileSystem.WriteAllText("C:\Test.txt", String.Empty, False)


        ' If File.Exists(Left(DocName, Len(DocName) - 3) & "xls") Then File.Delete(Left(DocName, Len(DocName) - 3) & "xls")
        ' Excel.activeworkbook.SaveCopyAs(Left(DocName, Len(DocName) - 3) & "xls")

        'doc.SaveAs(@"I:\file.xls",XlFileFormat.xlWorkbookNormal())

    End Function

    '2 TRANSFER SELECTED ROWS OF DATAGRIDVIEW to EXCEL; formats width, good with special chars
    Public Sub DataToExcel(ByRef dgv As DataGridView)

        Dim r As Integer = 2 'second row of spreadsheet
        Dim numCols, numRows As Integer
        '  Me.WindowState = FormWindowState.Minimized 'in case modGlobalVar.Msg buried
        MouseWait()

SetupSpreadsheet:
        Dim xlApp As Excel.Application = New Excel.Application
        xlApp.SheetsInNewWorkbook = 1
        Dim xlWorkBook As Excel.Workbook = xlApp.Workbooks.Add
        Dim xlWorkSheet As Excel.Worksheet = xlWorkBook.Worksheets.Item(1)
        xlWorkSheet.Name = "ReportResult"

SelectDate:
        'CYCLE THROUGH GRID and TRANSFER EACH CELL DATA
        numRows = dgv.SelectedRows.Count
        If numRows > 0 Then 'use selected some
        Else 'select all rows
            dgv.SelectAll()
        End If
        numCols = dgv.Columns.Count
        numRows = dgv.SelectedRows.Count

ExportData:  '  For Each r As DataGridViewRow In Me.dgvResult.SelectedRows  'works but is reverse order user selected
        ' reverse selected rows into same order as user selected rows
        For o As Integer = numRows - 1 To 0 Step -1
            For nCol As Integer = 0 To numCols - 1
                xlWorkSheet.Cells(r, nCol + 1) = dgv.SelectedRows(o).Cells(nCol).Value()
            Next nCol
            r = r + 1
        Next o

FormatSpreadsheet:
        Dim FrmatRange As Excel.Range
        FrmatRange = xlWorkSheet.Range("A2", "A2").Resize(RowSize:=numRows, ColumnSize:=numCols)
        ' FrmatRange.Select()'not required
        With FrmatRange
            ' .WrapText = True'returns null if not all the same!!!!
            .VerticalAlignment = Excel.XlVAlign.xlVAlignTop
            '.EntireColumn.AutoFit() 'works for all columns except if contains cr!!!
        End With
        'COLUMN SIZE and WRAPPING - handles CR, etc
        For c As Integer = 1 To numCols
            If xlWorkSheet.Cells(2, c).wraptext = True Then 'cell contained line feeds
                xlWorkSheet.Cells(2, c).EntireColumn.ColumnWidth = 40
            Else
                xlWorkSheet.Cells(2, c).EntireColumn.autofit()
            End If
            Select Case xlWorkSheet.Cells(2, c).columnwidth
                Case Is > 40
                    xlWorkSheet.Cells(2, c).EntireColumn.ColumnWidth = 40
                Case Is < 5
                    xlWorkSheet.Cells(2, c).EntireColumn.ColumnWidth = 5
            End Select
        Next

Headings:  ' INSERT COLUMN HEADERS AFTER SET WIDTHS so headings will wrap instead of setting column size.
        For Each c As DataGridViewColumn In dgv.Columns
            xlWorkSheet.Cells(1, c.Index + 1) = c.HeaderText
        Next c
        xlWorkSheet.Rows(1).font.bold = True

FinalColWidths:  'force minimum column widths  or empty column will be too narrow 
        FrmatRange.EntireColumn.WrapText = True 'put here or else rows are too tall
        '     modGlobalVar.Msg(FrmatRange.Width.ToString, , "width")
        If FrmatRange.Width > 400 Then
            Try
                xlWorkSheet.PageSetup.Orientation = Excel.XlPageOrientation.xlLandscape
            Catch ex As Exception
                '    MsgBox(ex.Message, , "error spreadsheet orientation ")
            End Try
        End If
        xlApp.DisplayAlerts = False

CloseAll:
        xlApp.Visible = True
        xlApp.UserControl = True
        modGlobalVar.Msg("Spreadsheet open", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
        '    xlWorkBook.Close() - not if giving user control
        xlWorkSheet = Nothing
        xlWorkBook = Nothing
        ' xlApp.Quit() '- not if giving user control
        ReleaseComObject(xlApp)
        MouseDefault()

    End Sub

    '3 TRANSFER QUERY RESULTS to EXCEL from IDs in table; formats width, good with special chars
    Public Sub DataToExcel(ByRef tbl As Data.DataTable)
        'for specific reports not showing all desired fields in grid
        'run query in calling form to populate table
        MouseWait()
        Dim r As Integer = 2 'second row of spreadsheet
        Dim numCols, numRows As Integer
        Dim xlApp As Excel.Application = New Excel.Application
        xlApp.SheetsInNewWorkbook = 1
        Dim xlWorkBook As Excel.Workbook = xlApp.Workbooks.Add
        Dim xlWorkSheet As Excel.Worksheet = xlWorkBook.Worksheets.Item(1)
        xlWorkSheet.Name = "ReportResult"
        '  Me.WindowState = FormWindowState.Minimized 'in case modGlobalVar.Msg buried

        numCols = tbl.Columns.Count
        numRows = tbl.Rows.Count
        'INSERT COLUMN HEADERS
        For c As Integer = 0 To numCols - 1
            xlWorkSheet.Cells(1, c + 1) = tbl.Columns(c).ColumnName
        Next c

        '  For Each r As DataGridViewRow In Me.dgvResult.SelectedRows  'works but is reverse order user selected
        ' reverse selected rows into same order as user selected rows
        For o As Integer = numRows - 1 To 0 Step -1
            For nCol As Integer = 0 To numCols - 1
                xlWorkSheet.Cells(r, nCol + 1) = tbl.Rows(o)(nCol)
            Next nCol
            r = r + 1
        Next o

        'FINALIZE SPREADSHEET
        xlWorkSheet.Rows(1).font.bold = True
        Dim FrmatRange As Excel.Range
        FrmatRange = xlWorkSheet.Range("A2", "A2").Resize(RowSize:=numRows, ColumnSize:=numCols)
        ' FrmatRange.Select()'not required
        With FrmatRange
            ' .WrapText = True'returns null if not all the same!!!!
            .VerticalAlignment = Excel.XlVAlign.xlVAlignTop
            '.EntireColumn.AutoFit() 'works for all columns except if contains cr!!!
        End With
        'COLUMN SIZE and WRAPPING - hanldes CR, etc
        For c As Integer = 1 To numCols
            If xlWorkSheet.Cells(2, c).wraptext = True Then
                xlWorkSheet.Cells(2, c).EntireColumn.ColumnWidth = 50
            Else
                xlWorkSheet.Cells(2, c).EntireColumn.autofit()
            End If
            If xlWorkSheet.Cells(2, c).columnwidth > 50 Then
                xlWorkSheet.Cells(2, c).EntireColumn.ColumnWidth = 50
            End If
        Next
        FrmatRange.EntireColumn.WrapText = True 'put here or else rows are too tall
        xlApp.DisplayAlerts = False

CloseAll:
        tbl = Nothing
        xlApp.Visible = True
        xlApp.UserControl = True
        modGlobalVar.Msg("Spreadsheet open", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
        '    xlWorkBook.Close() - not if giving user control
        xlWorkSheet = Nothing
        xlWorkBook = Nothing
        ' xlApp.Quit() '- not if giving user control
        ReleaseComObject(xlApp)
        MouseDefault()
    End Sub

#End Region    'text to Excel

#Region " REPORTS in WORD"

    ' COLLECT ID#s FROM 1st COLUMN from ALL ROWS of GRID; USED BY Reports popup grid
    Public Function GetIDArray(ByRef dgv As DataGridView) As String ', ByRef cnt As Integer) As String
        Dim strb As New StringBuilder
        Dim x As Integer = 0    'omit duplicates
        ' Dim cnt As Integer = 0
        For Each row As DataGridViewRow In dgv.SelectedRows
            If x = dgv.Item(0, row.Index).Value Then
            Else
                strb.Append(dgv.Item(0, row.Index).Value)
                strb.Append(",")
                x = dgv.Item(0, row.Index).Value
                ' cnt = cnt + 1
            End If
        Next row

        ' cnt = (Len(IDarray) - Len(Replace(IDarray, ",", ""))) + 1 ' / Len(","))
        GetIDArray = strb.Replace(strb.ToString, strb.ToString.Substring(0, Len(strb.ToString) - 1)).ToString
    End Function

    'USED BY REG PAYMENT LIST
    Public Function GetIDArray(ByRef lstvw As ListView) As String ', ByRef cnt As Integer) As String
        'assumes no duplicates in listview
        Dim strb As New StringBuilder
        For Each item As ListViewItem In lstvw.CheckedItems
            strb.Append(item.Text)
            strb.Append(",")
        Next item
        GetIDArray = strb.Replace(strb.ToString, strb.ToString.Substring(0, Len(strb.ToString) - 1)).ToString
    End Function

    Public Sub PrintCaseConversation(ByVal strSQL As String)
        MouseWait()
        '1 - CREATE WORD DOCUMENT
        ' Create an instance of Word, and make it visible.
        Dim wrdApp As Word.Application
        Dim wrdDoc As Word.Document
        Dim bSelected As Boolean = False

        wrdApp = CreateObject("Word.Application")
        wrdApp.Visible = True

        ' Open A Word doc
        wrdDoc = wrdApp.Documents.Add()
        Dim cmd As New SqlCommand(strSQL, sc)
        '.....................
        'BELOW MOVED TO MDPOPUP
        '..................
        If Not SCConnect() Then
            Exit Sub
        End If


        'RUN QUERY
        Dim drdr As SqlDataReader
        Dim strbName As New StringBuilder
        Try
            drdr = cmd.ExecuteReader()
        Catch ex As Exception
            modGlobalVar.Msg("ERROR: PrintCaseConversationSQL DR", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Dim intCase As Integer = 0
        'EXPORT TO WORD
        Try
            While drdr.Read
                strbName.Length = 0
                If drdr.GetInt32(0) = intCase Then    'is same case, only do conversation
                    GoTo Conversation
                Else
                    intCase = drdr.GetInt32(0)
                End If
                wrdApp.Selection.Font.Bold = True
                wrdApp.Selection.TypeText(drdr.GetString(3))
                wrdApp.Selection.TypeParagraph()
                wrdApp.Selection.Font.Bold = False
                wrdApp.Selection.Font.Italic = True
                wrdApp.Selection.TypeText("Case Name: ") 'drdr.GetName(3).ToString)

                wrdApp.Selection.Font.Italic = False
                wrdApp.Selection.Font.Bold = True
                wrdApp.Selection.TypeText(drdr.GetString(4))
                wrdApp.Selection.Font.Bold = False
                If IsDBNull(drdr(8)) Then
                Else
                    wrdApp.Selection.Font.Italic = True
                    wrdApp.Selection.TypeText("     Case Manager: ")
                    wrdApp.Selection.Font.Italic = False
                    wrdApp.Selection.TypeText(drdr.GetString(8))
                End If
                wrdApp.Selection.TypeParagraph()
                wrdApp.Selection.TypeParagraph()
Conversation:
                'casedID, tblConversation.ConversDate, tblConversation.Notes, tblOrg.OrgName, tblCase.CaseName, 
                'tblContact.FirstName, tblContact.Lastname, BriefSummary" _
                If IsDBNull(drdr(1)) Then  'no conversations
                Else
                    wrdApp.Selection.Font.Bold = True
                    wrdApp.Selection.TypeText(drdr.GetDateTime(1))
                    wrdApp.Selection.Font.Bold = False

                    If IsDBNull(drdr(6)) Then   'name
                    Else
                        strbName.Append(drdr.GetString(6))
                    End If
                    If IsDBNull(drdr(5)) Then
                    Else
                        strbName.Append(", " & drdr.GetString(5))
                    End If
                    wrdApp.Selection.TypeText("    " & strbName.ToString)
                    If IsDBNull(drdr(7)) Then   'brief summary
                    Else
                        wrdApp.Selection.Font.Italic = True
                        wrdApp.Selection.TypeText("     Brief Summary: ")
                        wrdApp.Selection.Font.Italic = False
                        wrdApp.Selection.TypeText(drdr.GetString(7))
                    End If
                    If IsDBNull(drdr(2)) Then 'notes
                    Else
                        wrdApp.Selection.TypeParagraph()
                        wrdApp.Selection.TypeText(drdr.GetString(2))
                    End If
                End If
                wrdApp.Selection.TypeParagraph()
                wrdApp.Selection.TypeParagraph()
            End While
        Catch ex As Exception
            modGlobalVar.Msg("ERROR: PrintCaseConversationSQL Word", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        wrdDoc.Content.Paragraphs.Add()
        wrdApp.Selection.TypeText("end")

Release:
        Try
            drdr.Close()
            sc.Close()
            strSQL = Nothing

            ' wrdDoc.Close()
            ReleaseComObject(wrdDoc)
            'wrdApp.Close(False)
            ReleaseComObject(wrdApp)
            wrdDoc = Nothing
            wrdApp = Nothing
        Catch ex As Exception
        End Try
        modGlobalVar.Msg("Report Finished", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
        'modGlobalVar.Msg(" ".PadLeft(30, ""), MessageBoxIcon.Information Or vbmodGlobalVar.MsgSetForeground, "Report Finished.")


    End Sub

    '-------------------------
    'WORD - PUBLIC REPORT
    Public Sub PrintResourcePublic(ByVal strSQL As String, ByVal strHeading As String)
        MouseWait()

        Try
            If CWriter.PrintResourcePublic(strSQL, strHeading) Then
            Else
            End If
        Catch ex As Exception
            modGlobalVar.Msg("ERROR: CWriter", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        GoTo ResetMouse


ResetMouse:
        MouseDefault()
    End Sub       'public report

    'WORD - ALL FIELDS - proc already run - with progress bar
    Public Sub PrintResourceFull(ByRef tbl As System.Data.DataTable, ByVal strTitle As String, ByVal strSubhead As String, ByVal strFooter As String, ByRef progressbr As ToolStripProgressBar)

        MouseWait()
        CWriter.PrintResourceFull(tbl, strTitle, strSubhead, strFooter, progressbr)

ResetMouse:
        MouseDefault()
    End Sub       'full report,used by New Resources, Karen

    'from resource IDs
    Public Sub PrintResourceFull(ByVal IDarray As String, Optional ByVal strSubHeading As String = "")
        'TODO can the report name not be hard coded??
        'run query on luReporttable??

        Dim sp As New SqlClient.SqlCommand
        sp.CommandType = CommandType.StoredProcedure
        sp.CommandText = "[RptsResourcesFull]"
        sp.Connection = sc
        sp.Parameters.Add("@Which", SqlDbType.VarChar).Value = "ResourceSearch"
        sp.Parameters.Add("@Num", SqlDbType.VarChar).Value = IDarray
        '   modGlobalVar.Msg(sp.Parameters("@Num").Value.ToString)
        Dim tbl As New System.Data.DataTable
        If Not SCConnect() Then
            Exit Sub
        End If

        Try
            tbl.Load(sp.ExecuteReader(System.Data.CommandBehavior.CloseConnection))
        Catch ex As Exception
            modGlobalVar.Msg("ERROR: PrintResourceFull IDArray ", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        PrintResourceFull(tbl, "RESOURCE REPORT: All Fields", "Selected Resources " & strSubHeading, "All Fields", Nothing)
        tbl = Nothing
        sp = Nothing

    End Sub

    'WORD - BASIC INFO AND DESCRIPTIONS FOR CRG REVIEW
    Public Sub PrintResourceDescriptions(ByVal strSQL As String, ByVal strHeading As String)    'descriptions, for CRG analysis

        MouseWait()
        ' Dim cw As New CWriter("")
        CWriter.PrintResourceDescriptions(strSQL, strHeading)
        GoTo ResetMouse

ResetMouse:
        MouseDefault()
    End Sub       'description report

    'WORD - BASIC INFO AND DESCRIPTIONS FOR SPECIALTY REPORT
    Public Sub PrintResourceDescriptions(ByVal strSQL As String, ByVal strHeading As String, ByVal bLocal As Boolean)    'descriptions, for CRG analysis

        MouseWait()

        '1 - CREATE WORD DOCUMENT
        ' Create an instance of Word, and make it visible.
        Dim wrdApp As Word.Application
        Dim wrdDoc As Word.Document
        Dim bSelected As Boolean = False
        Dim iFld As Short
        Dim b As Boolean = False 'isdbnull
        Dim strbGroupHdg As New StringBuilder("initial value")
        Dim cmd As New SqlCommand(strSQL, sc)
        If Not SCConnect() Then
            Exit Sub
        End If
        'RUN QUERY
        Dim drdr As SqlDataReader
        Try
            drdr = cmd.ExecuteReader

        Catch ex As Exception
            modGlobalVar.Msg("ERROR: report datareader", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try


        If drdr.HasRows Then
            wrdApp = CreateObject("Word.Application")
            wrdApp.Visible = True

            ' Open A Word doc
            wrdDoc = wrdApp.Documents.Add()
            'With wrdDoc.Sections(1).Footers(1)
            '    .Range.Font.Size = 8
            '    '  .Range.Fields.Add(wrdDoc.Sections(1).Footers(1).Range, Word.WdFieldType.wdFieldNumPages)
            '    .Range.Text = CType(Now, String) + Chr(9) + "     Resource Rpt " & strHeading '+ Chr(9)
            '    .PageNumbers.Add(2)

            'End With
FooterwNumPages:
            '// Open up the footer in the word document
            wrdDoc.ActiveWindow.ActivePane.View.SeekView = Microsoft.Office.Interop.Word.WdSeekView.wdSeekCurrentPageFooter
            With wrdDoc.ActiveWindow
                .Selection.Range.Font.Size = 8
                '  .Range.Fields.Add(wrdDoc.Sections(1).Footers(1).Range, Word.WdFieldType.wdFieldNumPages)
                .Selection.TypeText(CType(Now, String) + Chr(9) + "     Resource Rpt " & strHeading + Chr(9))
                .Selection.Fields.Add(wrdApp.ActiveWindow.Selection.Range, Word.WdFieldType.wdFieldPage)
                .Selection.TypeText("/")
                .Selection.Fields.Add(wrdApp.ActiveWindow.Selection.Range, Word.WdFieldType.wdFieldNumPages)
                .ActivePane.View.SeekView = Word.WdSeekView.wdSeekMainDocument
            End With
            wrdDoc.Sections(1).Footers(1).Range.Font.Size = 8

            wrdApp.Selection.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            wrdApp.Selection.Font.Bold = True
            '  wrdApp.Selection.Font.Underline = True
            wrdApp.Selection.TypeText(strHeading)
            wrdApp.Selection.Font.Bold = False
            ' wrdApp.Selection.Font.Underline = False
            wrdApp.Selection.TypeParagraph()
            'wrdApp.Selection.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
            wrdApp.Selection.TypeParagraph()


            'EXPORT TO WORD
            While drdr.Read
                'type
                iFld = drdr.GetOrdinal("Type")
                If IsDBNull(drdr(iFld)) Then
                Else
                    If strbGroupHdg.ToString = drdr.GetString(iFld) Then 'is same heading
                    Else
                        wrdApp.Selection.Font.Size = wrdApp.Selection.Font.Size + 2
                        wrdApp.Selection.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                        wrdApp.Selection.Font.Bold = True
                        wrdApp.Selection.TypeText(UCase(drdr.GetString(iFld)))
                        wrdApp.Selection.TypeParagraph()
                        wrdApp.Selection.Font.Bold = False
                        '  wrdApp.Selection.TypeText("     ")
                        '  wrdApp.Selection.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                        wrdApp.Selection.Font.Size = wrdApp.Selection.Font.Size - 2
                        strbGroupHdg.Replace(strbGroupHdg.ToString, drdr.GetString(iFld))
                    End If
                End If

                wrdApp.Selection.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                wrdApp.Selection.TypeParagraph()
Authors:
                iFld = drdr.GetOrdinal("FirstAuthor")
                If IsDBNull(drdr(iFld)) Then
                Else
                    wrdApp.Selection.TypeText(drdr.GetString(iFld))
                    iFld = drdr.GetOrdinal("OtherAuthors")
                    If IsDBNull(drdr(iFld)) Then
                    Else
                        wrdApp.Selection.TypeText(", " + drdr.GetString(iFld))
                    End If
                    wrdApp.Selection.TypeParagraph()
                End If

ResourceName:
                iFld = drdr.GetOrdinal("ResourceName")
                ' wrdApp.Selection.TypeParagraph()
                wrdApp.Selection.Font.Bold = True
                wrdApp.Selection.Font.Size = wrdApp.Selection.Font.Size + 2
                wrdApp.Selection.TypeText(drdr.GetString(iFld))
                wrdApp.Selection.Font.Bold = False
                wrdApp.Selection.Font.Size = wrdApp.Selection.Font.Size - 2
                wrdApp.Selection.TypeParagraph()
                b = False
PublishInfo:
                iFld = drdr.GetOrdinal("Publishing")
                If IsDBNull(drdr(iFld)) Then
                Else
                    b = True
                    wrdApp.Selection.TypeText(drdr.GetString(iFld))
                End If
                If b = True Then
                    wrdApp.Selection.TypeParagraph()
                End If
                b = False
Address:        For x As Integer = drdr.GetOrdinal("Address1") To drdr.GetOrdinal("Address1") + 5
                    If IsDBNull(drdr(x)) Then
                    Else
                        b = True
                        wrdApp.Selection.TypeText(drdr.GetString(x) + "  ")
                    End If
                Next x
                If b = True Then
                    wrdApp.Selection.TypeParagraph()
                End If
                b = False
Phones:
                For x As Integer = drdr.GetOrdinal("Telephone") To drdr.GetOrdinal("Telephone2")
                    If IsDBNull(drdr(x)) Then
                    Else
                        b = True
                        wrdApp.Selection.TypeText(drdr.GetString(x) + "  ")
                    End If
                Next x
                If b = True Then
                    wrdApp.Selection.TypeParagraph()
                End If
URL:
                iFld = drdr.GetOrdinal("Website")
                If IsDBNull(drdr(iFld)) Then
                Else
                    wrdApp.Selection.Font.Color = WdColor.wdColorBlue
                    wrdApp.Selection.TypeText(drdr.GetString(iFld))
                    wrdApp.Selection.Font.Color = WdColor.wdColorBlack
                End If
                wrdApp.Selection.TypeParagraph()
                wrdApp.Selection.TypeText("                                         ------")
                wrdApp.Selection.TypeParagraph()

            End While

            wrdDoc.Content.Paragraphs.Add()
            wrdApp.Selection.Font.Italic = True
            wrdApp.Selection.TypeText("end")
            wrdApp.Selection.Font.Italic = False
            wrdDoc = Nothing
            strSQL = Nothing
            strHeading = Nothing
            modGlobalVar.Msg("report finished", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            modGlobalVar.msg(MsgCodes.noResultCancel) '"Cancelling Report", "no results found", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If

Release:
        Try
            drdr.Close()
            sc.Close()
            strSQL = Nothing

            wrdDoc.Close()
            ReleaseComObject(wrdDoc)
            wrdApp.Close(False)
            ReleaseComObject(wrdApp)
            wrdDoc = Nothing
            wrdApp = Nothing
        Catch ex As Exception
        End Try
ResetMouse:
        MouseDefault()
    End Sub       'description report

    'WORD - print generic Resource report from stored procedure with group headings
    Public Sub PrintResourceReport(ByVal SProc As SqlCommand, ByVal strTitle As String, ByVal strsubtitle As String, ByVal bPrintColumnHeadings As Boolean, ByVal strGroupOn As String, ByVal strFooter As String)
        MouseWait()
        'If Not SCConnect() Then
        '    Exit Sub
        'End If
        Try
            CWriter.PrintReportwGroup(SProc, strTitle, strsubtitle, True, strGroupOn, strFooter)
        Catch ex As Exception
            modGlobalVar.msg("ERROR: PrintReport sProc", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        MouseDefault()
    End Sub

    'WORD - print simple generic report from STRING
    Public Sub PrintReport(ByVal str As String, ByVal strTitle As String, ByVal bPrintColumnHeadings As Boolean)
        MouseWait()

        Try
            CWriter.PrintReport(str, strTitle, bPrintColumnHeadings)
        Catch ex As Exception
            modGlobalVar.msg("ERROR: PrintReport sProc", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        MouseDefault()
    End Sub

#End Region 'print resource reports

#Region "OpenReportServer"

    'misc - single ID
    Public Sub OpenReport(ByVal i As String, ByVal strName As String)
        Dim str As String = "http://solomon/ReportServer/Pages/ReportViewer.aspx?%2fInformation+Center+Reports%2f" & strName & "&rs:Command=Render&IDArray=" & i
        Try
            OpenWebsite(str)
        Catch ex As Exception
            modGlobalVar.Msg("couldn't open website", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

    'Staff laity Report
    Public Sub OpenStaffLaityRpt(ByVal strID As String, ByVal strName As String)
        Dim f As New RptViewerStaffLaity
        f.strOrgID = strID
        f.Text = "Staff & Laity " & strName
        Try
            f.Show()
        Catch ex As Exception
            modGlobalVar.Msg("ERROR: open StaffLaityRpt", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region

#Region "SSIS" 'run SSIS packages from user's machine

    '    Public Sub RunSSIS(
    '  ByVal sourceType As String, _
    '  ByVal sourceLocation As String, _
    '  ByVal packageName As String)

    '        Dim launchPackageService As New ClassRunSSIS '.launchpackage(sourceType, sourceLocation, packageName)
    '        Dim packageResult As Integer

    '        Try
    '            packageResult = launchPackageService.LaunchPackage(sourceType, sourceLocation, packageName)
    '        Catch ex As Exception
    '            MsgBox(ex.Message, , "error launching SSIS")
    '            Console.WriteLine("The following exception occurred: " & ex.Message)
    '        End Try
    '        MsgBox(CType(packageResult, PackageExecutionResult).ToString, , "SSIS Result")
    '        '   Console.WriteLine(CType(packageResult, PackageExecutionResult).ToString)
    '        '  Console.ReadKey()

    '    End Sub

    '    Private Enum PackageExecutionResult
    '        PackageSucceeded
    '        PackageFailed
    '        PackageCompleted
    '        PackageWasCancelled
    '    End Enum


#End Region 'SSIS

#Region "Display" 'StatusBar text, mouse hover

    'displays staff name when hover over update fields ust to save time loading forms
    Public Function ShowStaff(ByVal Who As String) As String ', ByVal What As String) as str ', ByRef txt As TextBox)
        If Who = String.Empty Or Who = "0" Then
            Return "unknown"
        Else
            Try
                Return colStafflu.Item(Who)
            Catch ex As System.Exception
                Return "unknown2"
            End Try
        End If

    End Function

    'SET STATUS BAR TEXT
    Public Sub SetStatusBarText(ByVal str As String, ByRef stBar As StatusBar, ByVal i As Integer)
        stBar.Panels(i).Text = str
    End Sub


#End Region 'showstaff

#Region "INSERT NEW RECORD"

    'GENERATES NEW CASE ID
    Public Function NewCase(ByVal IDOrg As Integer, ByVal usrEntry As String, ByVal iMgr As Integer, ByVal What As String, ByRef frm As Form, ByVal OrgNamePhone As String, ByVal iCRG As Integer) As Integer

        Dim strCase As String
        Dim i As Int16 = 0
        ' frm.statusbar1.Panel(0).text(-"entering new " + What)

UserInputCaseName:
        'If the user clicks Cancel, a zero-length string is returned.
        strCase = InputBox("ENTER NEW CASE NAME" & NextLine & NextLine & "1-3 words preferred. " & NextLine & "The month/year will be added for you.", "Adding new Case", usrEntry)
        If strCase = "" Then
            Exit Function
        End If
        strCase = strCase & " " & Month(Now).ToString & "/" & Year(Now).ToString

InsertRecord:
        Dim cmd As New SqlClient.SqlCommand("INSERT INTO tblCase (CaseName, OrgNum, CaseMgrNum, OpenDate, CRGNum)" _
& "VALUES('" & strCase & "', " & IDOrg & ", " & iMgr & "," & " GetDate()," & iCRG & "); SELECT @@IDENTITY ", sc)
        If Not SCConnect() Then
            Exit Function
        End If
        Try
            i = CInt(cmd.ExecuteScalar())
        Catch ex As Exception
            modGlobalVar.msg("ERROR: Failed to Save New Case", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Function
        Finally
            sc.Close()
            cmd = Nothing
        End Try


OpenCaseDialog:
        OpenNewCase(i)
        Return i
    End Function

    'INSERT NEW CONTACT -- not used??
    Public Sub NewContact(ByVal IDOrg As Integer, ByVal usrEntry As String, ByVal What As String, ByRef frm As Form, ByVal OrgNamePhone As String)
        '  Dim strName As String
        Dim str As String

        'get usr name
        'If the user clicks Cancel, a zero-length string is returned.
        ' strName = InputBox("ENTER NEW Last NAME", "Adding new Contact", usrEntry)
        'If strName = "" Then
        'Exit Sub
        'End If

        str = "INSERT INTO tblContact(LastName, OrgNum, CreateStaffNum, CreateDate) VALUES (N'new person', " & IDOrg & ", " & usr & ", GETDATE()); SELECT @@Identity"

InsertNewItem:
        If Not SCConnect() Then
            Exit Sub
        End If

        Dim myTrans As SqlClient.SqlTransaction = sc.BeginTransaction()
        Dim cmd As New SqlClient.SqlCommand(str, sc, myTrans)
        Dim newID As Integer
        Try
            newID = cmd.ExecuteScalar()
            myTrans.Commit()
        Catch exce As Exception
            modGlobalVar.msg("ERROR: New Contact ExecuteScalar", exce.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
            myTrans.Rollback()
            sc.Close()
            Exit Sub
        End Try

        sc.Close()
OpenForm:
        bNewContact = True
        'TODO change this to open dialog because could be from cbo on anther form - dialog doesn't open correct record, why is that??
        modGlobalVar.OpenMainContact(newID, "Entering New Contact", OrgNamePhone, IDOrg, True) 'Me.editOrgName.Text & " : " & Me.editPhone.Text, Me.txtOrgID.Text)
    End Sub

    ' NEW GRANT from frmOrg, MGI forms
    Public Sub NewGrant(ByVal OrgID As Integer, ByVal strType As String, ByVal bConfirm As Boolean, ByVal strOrg As String, ByVal strOrgandPhone As String, ByVal GINum As Integer, ByVal iCaseNum As Integer, ByVal firstdate As String, ByVal iPD As Integer, ByVal iClergy As Integer, ByVal iStaff As Integer)

        ' get last used number from lutable
        Dim iNum As Integer
        Dim strSuffix As String
        Dim myReader As SqlDataReader
        Dim cmd As New SqlCommand
        Dim cmd2 As New SqlCommand
        cmd.Connection = sc
        cmd2.Connection = sc

        strType = Replace(strType, "&& ", "")

        '1. CHECK FOR EXISTING OPEN GRANTS

        ''existing grant with no report received
        cmd.CommandText = "SELECT GrantIDtxt, TypeofGrant FROM tblGrant WHERE (OrgNum = " & OrgID & ") AND (GrantComplete <> 1) AND (FinalReportRecdDate IS NULL)" 'no: and (TypeofGrant = '" & strType & "')"

        If Not SCConnect() Then
            Exit Sub
        End If

        myReader = cmd.ExecuteReader

        If myReader.HasRows Then
            If bConfirm = True Then    'check for existing grants of this type
                While myReader.Read
                    If IsDBNull(myReader(1)) Then
                        Continue While
                    Else
                        If myReader.GetString(1) = strType Then
                            If modGlobalVar.msg("STOP - this organization already has a " & strType & "  grant", myReader.GetString(0) & NextLine & myReader.GetString(1) & "Do you want to add ANOTHER " & strType & " grant?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                            Else
                                myReader.Close()
                                sc.Close()
                                Exit Sub
                            End If
                        End If
                    End If
                End While
                ' myReader.Close()
            Else    'check for any open grants
                If modGlobalVar.msg("STOP - this organization already has open grants", "Do you still want to add new grant?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                Else
                    myReader.Close()
                    sc.Close()
                    Exit Sub
                End If
            End If
        End If
        myReader.Close()

        '2. INSERT NEW GRANT with new ID string
        'GET NEXT GRANT NUMBER
        'TODO account for late data entry from previous year re grant next id number; see grant form for example on AwardKay
        cmd2.CommandText = "SELECT GrantRequestNumber, GrantTypeSuffix FROM luGrantTokenTbl WHERE luGrantTokenTbl.GrantTypeName = N'" & strType & "'"

        iNum = 0
        Try
            myReader = cmd2.ExecuteReader
            If myReader.HasRows Then
                While myReader.Read
                    iNum = myReader.GetValue(0)
                    strSuffix = myReader.GetString(1) & "R"
                End While
            Else
                modGlobalVar.msg(MsgCodes.noResultCancel) 'no rows found", strType, MessageBoxButtons.OK, MessageBoxIcon.Information)
                myReader.Close()
                sc.Close()
                Exit Sub
            End If

        Catch ex As Exception
            modGlobalVar.msg("ERROR: NewGrant execute reader error", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        myReader.Close()
        iNum = iNum + 1

        'INSERT NEW GRANT
        strSuffix = Year(Now) & "-" & Format(iNum, "000") & "-" & strSuffix

        Dim sql As New SqlCommand
        sql.Connection = sc
        If firstdate = "NULL" Then
            sql.CommandText = "INSERT INTO tblGrant(GrantIDtxt, OrgNum, TypeofGrant, GrantStaffnum) VALUES ('" & strSuffix & "', " & OrgID & ", '" & strType & "', " & usr & ")"
        Else
            sql.CommandText = "INSERT INTO tblGrant(GrantIDtxt, OrgNum, TypeofGrant, GINum, CaseNum,FirstDraftReceivedDate, PDNum, SrClergyNum, GrantStaffnum) VALUES ('" & strSuffix & "', " & OrgID & ", '" & strType & "', " & GINum & ", " & iCaseNum & ", '" & firstdate & "', " & iPD & ", " & iClergy & ", " & iStaff & ")"
        End If
        Try
            sql.ExecuteNonQuery()
            sql.CommandText = "UPDATE luGrantTokenTbl SET GrantRequestNumber = " & iNum & " WHERE(GrantTypeName = '" & strType & "')"
            sql.ExecuteNonQuery()
        Catch exc As Exception
            modGlobalVar.msg("ERROR: NewGrant, didn't update token table", exc.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        sc.Close()
        Try
            modGlobalVar.OpenMainGrant(strSuffix, strOrg, strOrgandPhone, OrgID)
        Catch ex As Exception
            modGlobalVar.msg("ERROR can't open grant form", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    'NEW MANUAL ORDER
    Public Function NextOrderNumber() As Integer
        Dim i As Integer
        Dim sql As New SqlCommand("SELECT LastNumUsed from luTokentbl where What = 'RegistrationOrder'", sc)
        If Not SCConnect() Then
            Exit Function
        End If
        i = sql.ExecuteScalar + 1
        sql.CommandText = ("UPDATE luTokentbl SET LastNumused = " & i & " WHERE What = 'RegistrationOrder'")
        sql.ExecuteNonQuery()

InsertOrder:
        Dim sql2 As New SqlCommand(" INSERT INTO tblEventRegOrder2(OrderID, DtOrder, Source) VALUES (" & i & ", N'" & DateTime.Now & "', N'" & usrFirst & "')", sc)

        sql2.ExecuteNonQuery()
        sql = Nothing
        sql2 = Nothing

        sc.Close()
        Return i

    End Function

    'NEW REGISTRATION
    Public Function InsertRegistration(ByVal OrderID As Integer, EventID As Integer, ContactID As Integer) As Integer 'return new registrationid
        Dim sql2 As SqlCommand
        Dim Inpt As String
        'SET VARIABLES
        If OrderID = 0 Then
            Inpt = InputBox("Enter an existing Order Number or leave as 'new'.", "Entering New Registration", "new")
            Try
                OrderID = CType(Inpt, Integer)
            Catch ex As Exception
                OrderID = modPopup.NextOrderNumber()
            End Try
        End If

        If EventID = 0 Then
            EventID = 444
        End If

        'RUN INSERT QUERY
        If Not SCConnect() Then
            Exit Function
        End If
        sql2 = New SqlCommand("INSERT INTO tblEventReg2 (Ordernum, EventNum, ContactNum, EnteredBy, RegDate) VALUES (" & OrderID & ", " & EventID & " , " & ContactID & ", '" & usrFirst & "', '" & Now & "'); SELECT @@Identity", sc)
        InsertRegistration = sql2.ExecuteScalar()
        sc.Close()
        sql2 = Nothing

    End Function

    'NEW REGISTRATION using structure and unbound form
    Public Sub InsertRegistration(ByVal OrderID As Integer, EventID As Integer, ContactID As Integer, RegDt As Date)
        Dim sql2 As SqlCommand
        Dim newID As Integer

        'RUN INSERT QUERY
        If Not SCConnect() Then
            Exit Sub
        End If
        sql2 = New SqlCommand("INSERT INTO tblEventReg2 (Ordernum, EventNum, ContactNum, EnteredBy, RegDate) VALUES (" & OrderID & ", " & EventID & " , " & ContactID & ", '" & usrFirst & "', '" & RegDt & "'); SELECT @@Identity", sc)
        newID = sql2.ExecuteScalar()
        sc.Close()
        modGlobalVar.OpenMainWReg2(newID, "Entering new registration with: " & IsNull(usrFirst, ""), True, "Registrant")
        sql2 = Nothing
    End Sub

    'NEW GENERIC; used by Denomination
    Public Function InsertNewRecord(ByVal Str As String) As Boolean

        If Not SCConnect() Then
            Return False
            'Exit Function
        End If

        '  Dim myTrans As SqlClient.SqlTransaction = sc.BeginTransaction()
        Dim cmd As New SqlClient.SqlCommand(Str, sc) ', myTrans)
        Try
            cmd.ExecuteNonQuery()
            ' myTrans.Commit()
        Catch exce As Exception
            modGlobalVar.msg("ERROR: inserting new record", exce.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)

            '    myTrans.Rollback()
            Return False
        Finally
            sc.Close()
        End Try
        Return True

    End Function

    'INSERT or REMOVE this Contact from selected mailing List
    Public Sub MailListContact(ByVal ContctID As Integer, ByVal ContctName As String, MailLstID As Integer, ByVal MailLstName As String)
        Dim sql As SqlCommand
        If Not SCConnect() Then
            Exit Sub
        End If

        sql = New SqlCommand("INSERT INTO tblMailListContact(ContactNum, MailListNum) VALUES(" & ContctID & ", " & MailLstID & ")", sc)

        Try
            'INSERT
            sql.ExecuteNonQuery()

        Catch ex As SqlException
            '       modGlobalVar.msg("ERROR: processing index ", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
            'ASK DELETE
            If modGlobalVar.msg("CONFIRM - REMOVE contact from this mailing list?", _
                                "Click Yes to REMOVE " + UCase(ContctName) + " from the " & UCase(MailLstName) & " mail list.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                sql = New SqlCommand("DELETE FROM tblMailListContact WHERE (ContactNum = " & ContctID & ") AND (MailListNum = " & MailLstID & ")", sc)
                Try
                    sql.ExecuteNonQuery()
                    '  Catch ex2 As SqlException
                    '     modGlobalVar.msg("PERMISSION DENIED", "this is not your mailing list", MessageBoxButtons.OK, MessageBoxIcon.Hand)
                Catch ex2 As System.Exception
                    modGlobalVar.msg("ERROR: processing index ", ex2.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Finally
                    sc.Close()
                End Try
            End If
         Catch ex As System.Exception
            MsgBox(ex.Message, , "other error")
        Finally
            sc.Close()
        End Try
    End Sub

    'INSERT RESOURCE INDEX TERM or return existing ID
    Public Function CreateIndexterm(ByVal str As String) As Integer
        Dim sql As SqlCommand
        Dim i As Integer = 0
        If Not SCConnect() Then
            Exit Function
        End If

        'SEE IF ALREADY EXISTS
        sql = New SqlCommand("SELECT IndexTermID FROM luResourceIndexTerm WHERE IndexTerm = '" & str.Replace("'", "''") & "'", sc)
        Try
            i = sql.ExecuteScalar()
        Catch ex As System.Exception
            modGlobalVar.msg("ERROR: search luResourceIndex: '" & str & "'", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        'INSERT AS NEW
        If i = 0 Then
            sql = New SqlCommand("INSERT INTO luResourceIndexTerm (IndexTerm) VALUES ('" & str.Replace("'", "''") & "'); select @@Identity", sc)
            Try
                i = sql.ExecuteScalar()
            Catch ex As System.Exception
                modGlobalVar.msg("ERROR: insert new index term ", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If

        Return i

    End Function

#End Region ' insert new record

#Region "LOAD RICH TEXT BOX"  'requires columns widths

    'SUCCESSIVE QUERY STRINGS TO LOAD RTB.  COLUMN WIDTHS based on formula pixels
    'REQUIRES INITIAL QUERY TO GET FIELD LENGTHS -- not used??
    Private Sub RunQueries(ByRef rtb As RichTextBox, ByVal rdr As SqlDataReader, ByVal strHead As String)
        Dim iPrev As Integer = 12
        Dim arLength(rdr.FieldCount) As Integer '= New Integer() {}
        Dim arOrig(rdr.FieldCount) As Integer
        Dim x As Integer
        MouseWait()
        'GET COLUMN WIDTHS TO SET TABS
        Try
            rdr.Read()
        Catch ex As Exception
            rtb.AppendText("no headings retrieved for this section" & NextLine)
        End Try
        If rdr.HasRows Then
            For y As Integer = 0 To rdr.FieldCount - 1
                arLength(y) = (rtb.SelectionFont.Size * 50 / (72 * 0.75)) * (CType(rdr.GetValue(y), Integer)) + iPrev    '50 pulled out of air seems to work.  s/be 96?
                iPrev = (arLength(y))
                arOrig(y) = CType(rdr.GetValue(y), Integer)

            Next
        Else
        End If
        ' rtb.SelectAll()
        'rtb.Select(0,0)

        'TITLE
        rtb.SelectionFont = New System.Drawing.Font(rtb.Font, FontStyle.Bold)
        rtb.SelectionTabs = arLength
        rtb.AppendText(strHead & NextLine)
        rtb.AppendText("____________________________" & NextLine & NextLine) 'usrCh& NextLine &  NextLine

        '...........................................        '..........................................................................
        'HEADINGS 
        Dim offset As Integer = 0
        Dim prevOffset As Integer = 0
        Try
            rdr.NextResult()
        Catch ex4 As Exception
            modGlobalVar.msg("ERROR: RunQueries", "missing query." & NextLine & "This result may be missing formatting information." & NextLine & ex4.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        rdr.Read()

        rtb.SelectionFont = New System.Drawing.Font(rtb.Font, FontStyle.Bold)

        For x = 0 To rdr.FieldCount - 1
            rtb.AppendText(rdr.GetName(x).ToString & Chr(9))           'prevOffset = offse
        Next
        rtb.AppendText(NextLine)
        If rdr.HasRows Then
            'FIRST ROW OF DATA
            rtb.SelectionFont = New System.Drawing.Font(rtb.Font, FontStyle.Regular)
            For x = 0 To rdr.FieldCount - 1
                If IsDBNull(rdr(x)) Then
                    'rtb.Text = rtb.Text & Chr(9)
                    rtb.AppendText(Chr(9))
                Else
                    'rtb.Text = rtb.Text & rdr.GetValue(x) & Chr(9)
                    'SEE IF IS TOTAL ROW FOR BOLD
                    If Len(rdr.GetValue(x).ToString) > 5 Then
                        If (rdr.GetValue(x).ToString).Substring(0, 5) = "GRAND" Then
                            rtb.SelectionFont = New System.Drawing.Font(rtb.Font, FontStyle.Bold)
                        End If
                    End If
                    Try
                        rdr.GetDecimal(x)
                        'rtb.AppendText(Format(rdr.GetDecimal(x), "##,##0.00").ToString.PadLeft(arLength(x)) + "  ")
                        rtb.AppendText(Format(rdr.GetDecimal(x), "##,##0.00").ToString.PadLeft(13))
                    Catch ex As Exception
                        Try
                            rdr.GetDateTime(x)
                            rtb.AppendText(rdr.GetDateTime(x).ToString.PadLeft(10))
                        Catch ex3 As Exception
                            Try
                                rdr.GetInt32(x)

                                If arOrig(x) < 11 Then
                                    rtb.AppendText(Format(rdr.GetInt32(x), "##,##0").ToString.PadLeft(arOrig(x)))
                                Else
                                    rtb.AppendText(Format(rdr.GetInt32(x), "##,##0").ToString.PadLeft(arOrig(x) + 2))
                                End If
                            Catch ex2 As Exception
                                'rdr.GetString(x)
                                ' modGlobalVar.Msg("string")
                                rtb.AppendText(rdr.GetValue(x).ToString)
                            End Try
                        End Try
                    End Try
                    rtb.AppendText(Chr(9))
                    'rtb.Text = rtb.Text & rdr.GetValue(x) & Chr(9)
                    'rtb.AppendText(rdr.GetValue(x) & Chr(9))
                    '                rtb.AppendText(rdr.GetValue(x).ToString.PadRight(arLength(x) + 2))
                End If
            Next
            '            rtb.Text = rtb.Text & NextLine
            rtb.AppendText(NextLine)

            'BODY OF DATA     
            While rdr.Read()
                For x = 0 To rdr.FieldCount - 1
                    If IsDBNull(rdr(x)) Then
                        'rtb.Text = rtb.Text & Chr(9)
                        rtb.AppendText("-- " & Chr(9))
                    Else
                        'rtb.Text = rtb.Text & rdr.GetValue(x) & Chr(9)
                        'SEE IF IS TOTAL ROW FOR BOLD
                        If Len(rdr.GetValue(x).ToString) > 5 Then
                            If (rdr.GetValue(x).ToString).Substring(0, 5) = "GRAND" Then
                                rtb.SelectionFont = New System.Drawing.Font(rtb.Font, FontStyle.Bold)
                            End If
                        End If
                        '                    rtb.AppendText(rdr.GetValue(x) & Chr(9))
                        '  rtb.AppendText(rdr.GetValue(x).ToString.PadRight(arLength(x) + 2))
                        Try
                            rdr.GetDecimal(x)
                            '   modGlobalVar.Msg("yes decimal")
                            rtb.AppendText(Format(rdr.GetDecimal(x), "#,##0.00").ToString.PadLeft(13))
                        Catch ex As Exception
                            Try
                                rdr.GetDateTime(x)
                                rtb.AppendText(rdr.GetDateTime(x).ToString.PadLeft(10))
                            Catch ex3 As Exception
                                Try
                                    rdr.GetInt32(x)
                                    '    modGlobalVar.Msg("yes, int")

                                    If arOrig(x) < 11 Then
                                        rtb.AppendText(Format(rdr.GetInt32(x), "##,##0").ToString.PadLeft(arOrig(x)))
                                    Else
                                        rtb.AppendText(Format(rdr.GetInt32(x), "##,##0").ToString.PadLeft(arOrig(x) + 2))
                                    End If
                                Catch ex2 As Exception
                                    'rdr.GetString(x)
                                    '    modGlobalVar.Msg("string")
                                    rtb.AppendText(rdr.GetValue(x).ToString)
                                End Try
                            End Try
                        End Try
                        rtb.AppendText(Chr(9))
                    End If
                Next
                '            rtb.Text = rtb.Text & NextLine
                rtb.AppendText(NextLine)
            End While
        Else
            rtb.AppendText("no data retrieved for this section" & NextLine)
        End If
        MouseDefault()
    End Sub

    'LOAD RTB with STRINGS so don't adjust column width for format numbers.  COLUMN WIDTHS based on query 1/2
    Public Function LoadRTB(ByRef rtb As RichTextBox, ByRef cmd As SqlClient.SqlCommand, ByVal strHead As String, ByVal bUseTable As Boolean) As Boolean
        '  MsgBox("in load rtb 1")
        Dim rdr As SqlDataReader
        Dim iPrev As Integer
        Dim arWidth(30) As Integer
        Dim cntField As Int16
        Dim arTab(30) As Integer
        Dim arHeading(30) As String

        MouseWait()
        If Not SCConnect() Then
            Exit Function
        End If

        rtb.Text = Nothing
        rtb.SuspendLayout()

        'TITLE
        rtb.SelectionFont = New System.Drawing.Font(rtb.Font, FontStyle.Bold)
        rtb.AppendText(strHead & NextLine & NextLine)
        rtb.AppendText("____________________________" & NextLine & NextLine)

        '-------------------------------------------------------------
        Try
            rdr = cmd.ExecuteReader
        Catch ex As Exception
            modGlobalVar.msg("ERROR: LoadRTB, rdr exception", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
            LoadRTB = False
            GoTo CloseAll
        End Try
        Dim x As Integer

HeadingsFromTable:

        iPrev = 12
        If bUseTable Then

            Dim k As Int16 = 0
            Dim b As Int16 = rdr.GetOrdinal("ColWidth")
            While rdr.Read
                'build length array
                arTab(k) = (rtb.SelectionFont.Size * 50 / (72 * 0.75)) * rdr.GetValue(b) + iPrev    '50 pulled out of air seems to work.  s/be 96?
                'remember field lengths for padding
                arWidth(k) = CType(rdr.GetValue(b), Integer)
                'build heading array
                arHeading(k) = rdr.GetString(rdr.GetOrdinal("ColHeading"))
                k = k + 1
                iPrev = (arTab(k))
            End While
            If k = 0 Then
                modGlobalVar.msg(MsgCodes.noResult) ', "", MessageBoxButtons.OK, MessageBoxIcon.Information)
                LoadRTB = False
                GoTo CloseAll
            Else

                cntField = k
            End If

            ReDim Preserve arTab(k)
            ReDim Preserve arHeading(k)
            ReDim Preserve arWidth(k)


HeadingsFromSelect:
        Else 'If bUseTable = False Then
            'GET MAX FIELD LENGTH & SET TABS
            If rdr.HasRows Then
                cntField = rdr.FieldCount
            Else
                modGlobalVar.msg(MsgCodes.noResult) ', "", MessageBoxButtons.OK, MessageBoxIcon.Information)
                LoadRTB = False
                GoTo CloseAll
            End If
            rdr.Read()
            For y As Integer = 0 To cntField - 1
                'set tab width
                arTab(y) = (rtb.SelectionFont.Size * 50 / (72 * 0.75)) * (CType(rdr.GetValue(y), Integer)) + iPrev    '50 pulled out of air seems to work.  s/be 96?
                'remember field lengths for padding
                arWidth(y) = CType(rdr.GetValue(y), Integer)
                'build heading array
                arHeading(y) = rdr.GetName(y).ToString
                iPrev = (arTab(y))
            Next
        End If


SetTabs:
        rtb.SelectionTabs = arTab
        rtb.SelectionHangingIndent = arTab(0) + 15
PrintColHeadings:
        rtb.SelectionFont = New System.Drawing.Font(rtb.Font, FontStyle.Bold)


        rtb.AppendText(arHeading(0).ToString & Chr(9)) 'don't right justify first col heading
        For x = 1 To (cntField - 1)
            rtb.AppendText(arHeading(x).ToString.PadLeft(arWidth(x)) & Chr(9))
        Next

        rtb.AppendText(NextLine)

Body:
        Try
            rdr.NextResult()
        Catch ex4 As Exception
            modGlobalVar.msg("ERROR: LoadRTB Body", "missing query. This result may be missing formatting information.", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        rtb.SelectionFont = New System.Drawing.Font(rtb.Font, FontStyle.Regular)
        If rdr.HasRows Then
        Else
            modGlobalVar.msg(MsgCodes.noResult) 'Cancel, "", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadRTB = False
            GoTo CloseAll
        End If
        Do While rdr.Read()
            'bold total row
            Try
                ' If InStr(UCase(rdr.GetValue(0).ToString), "TOTAL") > 0 Or InStr(UCase(rdr.GetValue(1).ToString), "TOTAL") > 0 Then 'may not have 2 columns
                If rdr.GetValue(0).ToString.Contains("total") Or rdr.GetValue(1).ToString.Contains("total") Then
                    If InStr(UCase(rdr.GetValue(0).ToString), "GRAND") > 0 Then
                        For x = 0 To rdr.FieldCount - 1
                            rtb.AppendText(CType("____", String).PadLeft(arWidth(x)) & Chr(9))
                        Next x
                        rtb.AppendText(NextLine)
                        rtb.SelectionFont = New System.Drawing.Font(rtb.Font, FontStyle.Bold)
                    End If
                End If
            Catch ex As Exception
            End Try
            'rows
            For x = 0 To rdr.FieldCount - 1
                If IsDBNull(rdr(x)) Then
                    '    rtb.AppendText(CType("--", String).PadLeft(arWidth(x)) & Chr(9))
                Else
                    Try 'right align if numbers
                        rdr.GetDecimal(x)
                        rtb.AppendText(Format(rdr.GetDecimal(x), "##,##0.00").ToString.PadLeft(arWidth(x)))
                    Catch ex As Exception
                        Try
                            rdr.GetDateTime(x)
                            rtb.AppendText(rdr.GetDateTime(x).ToString.PadLeft(arWidth(x)))
                        Catch ex3 As Exception
                            Try
                                rdr.GetInt32(x)
                                '  If arWidth(x) < 11 Then
                                rtb.AppendText(Format(rdr.GetInt32(x), "##,##0").ToString.PadLeft(arWidth(x)))
                                'Else
                                'rtb.AppendText(Format(rdr.GetInt32(x), "##,##0").ToString.PadLeft(arWidth(x) + 2))
                                'End If
                            Catch ex2 As Exception
                                rtb.AppendText(rdr.GetValue(x).ToString)
                            End Try
                        End Try
                        ' End Try
                    Finally
                        rtb.AppendText(Chr(9))
                    End Try
                End If
            Next
            rtb.AppendText(NextLine)
        Loop
        rtb.AppendText(NextLine)

        'check for more queries
        While rdr.NextResult()
            Try
                Array.Clear(rtb.SelectionTabs, 0, cntField)
            Catch ex As Exception
                modGlobalVar.msg("ERROR: LoadRTB RTB rdr nextresult", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
            ' ReDim rtb.SelectionTabs(-1)
            GoTo HeadingsFromTable
        End While
        LoadRTB = True
        ''.........................................................................

CloseAll:
        Try
            rdr.Close()
        Catch ex As Exception
        End Try
        rtb.ResumeLayout()
        cmd = Nothing
        ' arTab = Nothing
        sc.Close()
        MouseDefault()

    End Function

    '    'LOAD RTB with STRINGS so don't adjust column width for format numbers.  COLUMN WIDTHS based on query 1/2
    '    Public Sub oldLoadRTB(ByRef rtb As RichTextBox, ByRef cmd As SqlClient.SqlCommand, ByVal strHead As String)
    '        Dim rdr As SqlDataReader
    '        MouseWait()
    '        If Not SCConnect() Then
    '            Exit Sub
    '        End If

    '        rtb.Text = Nothing
    '        rtb.SuspendLayout()
    '        Try
    '            rdr = cmd.ExecuteReader
    '        Catch ex As Exception
    '            modGlobalVar.Msg(ex.Message, MessageBoxIcon.Error, "ERROR: oldLoadRTBreading exception")
    '            GoTo CloseAll
    '        End Try
    '        Dim x As Integer

    '        'GET MAX FIELD LENGTH & SET TABS
    '        If rdr.HasRows Then
    '        Else
    '            modGlobalVar.Msg("no results found", MessageBoxIcon.Information, "Cancelling request")
    '            rdr.Close()
    '            sc.Close()
    '            Exit Sub
    '        End If
    '        rdr.Read()
    '        Dim iPrev As Integer = 12
    '        Dim arLength(rdr.FieldCount) As Integer '= New Integer() {}

    '        For y As Integer = 0 To rdr.FieldCount - 1
    '            If IsDBNull(rdr(y)) Then
    '                arLength(y) = 0
    '            Else
    '                arLength(y) = (rtb.SelectionFont.Size * 50 / (72 * 0.75)) * (CType(rdr.GetValue(y), Integer)) + iPrev    '50 pulled out of air seems to work.  s/be 96?
    '            End If
    '            iPrev = (arLength(y))
    '        Next


    '        'TITLE
    '        rtb.SelectionFont = New System.Drawing.Font(rtb.Font, FontStyle.Bold)
    '        rtb.SelectionTabs = arLength
    '        rtb.AppendText(strHead& NextLine &  NextLine) 'usrCh& NextLine &  NextLine

    '        '...........................................        '..........................................................................
    '        'HEADINGS 
    '        Dim offset As Integer = 0
    '        Dim prevOffset As Integer = 0
    '        rtb.SelectionFont = New System.Drawing.Font(rtb.Font, FontStyle.Bold)
    '        For x = 0 To rdr.FieldCount - 1
    '            rtb.AppendText(rdr.GetName(x).ToString & Chr(9))           'prevOffset = offse
    '        Next
    '        rtb.AppendText(NextLine)

    '        Try
    '            rdr.NextResult()
    '        Catch ex4 As Exception
    '            modGlobalVar.Msg("missing query. This result may be missing formatting information.", MessageBoxIcon.Error, "ERROR: oldLoadRTB")
    '        End Try
    '        rdr.Read()

    '        If rdr.HasRows Then
    '            'FIRST ROW OF DATA
    '            rtb.SelectionFont = New System.Drawing.Font(rtb.Font, FontStyle.Regular)
    '            For x = 0 To rdr.FieldCount - 1
    '                If IsDBNull(rdr(x)) Then
    '                    'rtb.Text = rtb.Text & Chr(9)
    '                    'rtb.AppendText(Chr(9))
    '                    rtb.AppendText(Chr(9))
    '                Else
    '                    rtb.AppendText(rdr.GetValue(x))
    '                    rtb.AppendText(Chr(9))
    '                End If
    '            Next
    '            '            rtb.Text = rtb.Text & NextLine
    '            rtb.AppendText(NextLine)

    '            'BODY OF DATA     
    '            While rdr.Read()
    '                For x = 0 To rdr.FieldCount - 1
    '                    If IsDBNull(rdr(x)) Then
    '                        rtb.AppendText(Chr(9))
    '                    Else
    '                        rtb.AppendText(rdr.GetValue(x))
    '                        rtb.AppendText(Chr(9))
    '                    End If
    '                Next
    '                rtb.AppendText(NextLine)
    '            End While
    '        Else
    '            rtb.AppendText("no data retrieved for this section " & NextLine)
    '        End If
    '        rtb.ResumeLayout()

    '        ''.........................................................................
    '        rdr.Close()
    'CloseAll:
    '        cmd = Nothing

    '        sc.Close()
    '        MouseDefault()

    '    End Sub


#End Region

#Region "WordForms"

    Public Sub StaffLaityRpt(ByVal ID As Integer)
        '  If CWriter.PrintStaffLaityRpt(ID) Then''''

        '  End If
        '  Exit Sub

        ' OpenStaffLaityRpt(Me.txtOrgID.Text, Me.editOrgName.Text)
        ' Exit Sub
        Dim drw As DataRow
        Dim qry As Integer
        Dim cmd As New SqlCommand("rptOrgStaffLaity", sc)
        Dim drdr As SqlDataReader
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add("@ID", SqlDbType.Int)
        cmd.Parameters("@ID").Value = ID

        '  drdr.GetType.Read() 'skip 1st result for now - is legacy for old report
OpenDoc:
        Dim wrdApp As Microsoft.Office.Interop.Word.Application
        Dim wrdDoc As Microsoft.Office.Interop.Word.Document
        Dim wSel As Microsoft.Office.Interop.Word.Selection

        wrdApp = CreateObject("Word.Application")
        Try
            wrdDoc = wrdApp.Documents.Open(SOPPath & "DocumentTemplates\StaffLaity.dot")
        Catch ex As Exception
            modGlobalVar.msg("ERROR: StaffLaityRpt ", "Template not found     " & NextLine & ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
            GoTo release
            'Exit Sub
        End Try
        wrdDoc.Select()
        wSel = wrdApp.Selection

ReturnAddress:
        wSel.GoTo(What:=Microsoft.Office.Interop.Word.WdGoToItem.wdGoToBookmark, Name:="SenderNameEmail")
        Dim drw2 As DataRow
        Dim strRegion As String
        drw = tblStaff.Rows.Find(usr)
        strRegion = drw("SatelliteRegion").ToString
        wSel.Font.Italic = True
        wSel.TypeText(usrFirst)
        wSel.Font.Italic = False
        wSel.TypeText("   (" & drw("EmailName").ToString & CenterEmail & ")")
        drw = Nothing
        wSel.GoTo(What:=Microsoft.Office.Interop.Word.WdGoToItem.wdGoToBookmark, Name:="ReturnAddress")
        'Create Center Address table first time
        If tblCenter.Rows.Count > 0 Then
        Else
            If Not SCConnect() Then
                GoTo release ' Exit Sub
            End If
            Try

                Dim cmdCenter As SqlDataReader = New SqlCommand("SELECT   OrgName, Street1 + ', ' + City + ' ' + State + '  ' + Zip AS Address, Phone, Fax, SatelliteRegion FROM tblOrg WHERE  (SatelliteRegion = '" & usrRegion & "') AND (OrgName LIKE '%center for congregations%')", sc).ExecuteReader(CommandBehavior.CloseConnection)
                tblCenter.Load(cmdCenter)
                cmdCenter = Nothing
            Catch ex As Exception
                modGlobalVar.msg("ERROR: StafLaityRpt DR", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
                GoTo release ' Exit Sub
            End Try
        End If
        drw2 = tblCenter.Rows(0)
        wSel.TypeText(drw2("Address") & NextLine & drw2("Phone") + "  Fax: " + drw2("Fax"))
        drw2 = Nothing
        If Not SCConnect() Then
            GoTo release ' Exit Sub
        End If
GetData:
        Try
            drdr = cmd.ExecuteReader(CommandBehavior.CloseConnection)
            drdr.Read()
        Catch exc As Exception
            modGlobalVar.msg("ERROR: StaffLaityRpt Getdata dr", exc.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
            GoTo Release
        End Try

OrgNameHeading:
        wSel.GoTo(What:=Microsoft.Office.Interop.Word.WdGoToItem.wdGoToBookmark, Name:="OrgName")
        wSel.TypeText(drdr.GetString(drdr.GetOrdinal("OrgName")))
        ' wSel.MoveDown(Microsoft.Office.Interop.Word.WdUnits.wdLine, -1)
OrgDetail:
        wSel.GoTo(What:=Microsoft.Office.Interop.Word.WdGoToItem.wdGoToBookmark, Name:="OrgSection")

        ' wSel.Font.Size = 11
        wSel.Font.Bold = True
        wrdApp.Selection.Font.Underline = True
        wSel.TypeText("CONGREGATION DETAIL")
        wrdApp.Selection.Font.Underline = False
        wSel.TypeParagraph()
        For i As Integer = 3 To 14
            wSel.Font.Bold = True
            wSel.TypeText(drdr.GetName(i))
            wSel.Font.Bold = False
            wSel.TypeText(":  " + drdr.GetString(i))
            If (i + 1) Mod 3 = 0 Then
                wSel.TypeParagraph()
            Else
                wSel.TypeText(vbTab + vbTab)
            End If
        Next i
        '  wSel.TypeText("Address: " + drdr.GetString(drdr.GetOrdinal("Address")) + vbTab + vbTab + "Phone: " + drdr.GetString(drdr.GetOrdinal("Phone")))
        wSel.TypeParagraph()
        'org detail here

        ' wSel.Font.Size = 10
        wSel.TypeText("  ")
        wSel.FormFields.Add(wSel.Range, Type:=Microsoft.Office.Interop.Word.WdFieldType.wdFieldFormTextInput)
        ' wSel.TypeParagraph()
GrantCases:

        wSel.GoTo(What:=Microsoft.Office.Interop.Word.WdGoToItem.wdGoToBookmark, Name:="Grants")
        wSel.Font.Color = WdColor.wdColorGray20
        wSel.TypeText(drdr.GetValue(drdr.GetOrdinal("OrgID")))
        If IsDBNull(drdr("#RealCases")) Then
        Else
            wSel.TypeText("C")
        End If
        If IsDBNull(drdr("#Grants")) Then
        Else
            wSel.TypeText("G")
        End If
        wSel.Font.Color = WdColor.wdColorBlack


PeopleDetail:
        wSel.GoTo(What:=Microsoft.Office.Interop.Word.WdGoToItem.wdGoToBookmark, Name:="PeopleSection")

        For qry = 2 To 5
            drdr.NextResult()
            wrdApp.Selection.Font.Bold = True
            ' wrdApp.Selection.Font.Underline = True
            If drdr.HasRows Then
                Select Case qry
                    Case 2 'pastoral
                        wSel.TypeParagraph()
                        '  wrdApp.Selection.Font.Bold = True
                        wSel.TypeText("PASTORAL STAFF")
                        ' wrdApp.Selection.Font.Bold = False
                        '  wSel.TypeParagraph()
                    Case 3 'staff
                        wSel.TypeParagraph()
                        wSel.TypeText("GENERAL STAFF")
                        ' wSel.TypeParagraph()
                    Case 4 'leader
                        wSel.TypeParagraph()
                        wSel.TypeText("LAY LEADERSHIP")
                    Case 5 'member
                        wSel.TypeParagraph()
                        wSel.TypeText("GENERAL CONGREGATION")
                End Select
            Else
                Continue For
            End If
            wrdApp.Selection.Font.Bold = False
            wrdApp.Selection.Font.Underline = False
            wSel.TypeParagraph()
            While drdr.Read
                With wSel
                    .TypeText("  ")
                    wSel.FormFields.Add(wSel.Range, Type:=Microsoft.Office.Interop.Word.WdFieldType.wdFieldFormCheckBox)
                    .TypeText(Text:="  " & drdr.GetString(drdr.GetOrdinal("JobTitle")) & Chr(9) & drdr.GetString(drdr.GetOrdinal("FullName")) & Chr(9) & drdr.GetString(drdr.GetOrdinal("ContactAddress")) & Chr(9) & drdr.GetString(drdr.GetOrdinal("HomePhone")) & Chr(9) & drdr.GetString(drdr.GetOrdinal("Email")))
                    If IsDBNull(drdr("Action")) Then
                    Else
                        .Font.Color = WdColor.wdColorGray20
                        .TypeText(Chr(9) & drdr.GetString(drdr.GetOrdinal("Action")))
                        .Font.Color = WdColor.wdColorBlack
                    End If
                    .TypeParagraph()
                End With
            End While
            wSel.TypeText("               ")
            wSel.FormFields.Add(wSel.Range, Type:=Microsoft.Office.Interop.Word.WdFieldType.wdFieldFormTextInput)
            wSel.TypeParagraph()
        Next qry

SetTextboxFeatures:
        For Each fld As Microsoft.Office.Interop.Word.FormField In wrdDoc.FormFields
            If fld.Type = Microsoft.Office.Interop.Word.WdFieldType.wdFieldFormTextInput Then
                With fld.TextInput
                    .EditType(Microsoft.Office.Interop.Word.WdTextFormFieldType.wdRegularText, "    Enter corrections here for the section above.  Use Shift+Enter to begin a new line.  This space will exand as you type.                                             ")
                    '.Width = 660
                End With
            End If
        Next fld

        Dim pword As Object = "unlock"
        Dim Missing As Object = System.Reflection.Missing.Value

        wrdDoc.Protect(Microsoft.Office.Interop.Word.WdProtectionType.wdAllowOnlyFormFields, Missing, pword)
        wrdApp.Visible = True
        '  wrdDoc.UserControl = True
        drdr.Close()
Release:
        cmd = Nothing
        wSel = Nothing
        wrdDoc = Nothing
        wrdApp = Nothing
    End Sub

#End Region

#Region "STREAMWRITER"

    '6 WRITES TO and OPENS EXCEL from NEW DATAGRIDVIEW; skips labels - used ONLY by LibLabels
    Public Function StrmWriter(ByRef grdv As DataGridView, ByVal strDoc As String, ByVal iBlanks As Integer, ByVal iRepeat As Integer) As Boolean   'grid
        'used by LibLabels
        'has skip labels option
        Dim sw As IO.StreamWriter
        Dim i As Integer 'count columns
        Try
            sw = New IO.StreamWriter(UserPath & strDoc & ".txt")
        Catch ex As Exception
            modGlobalVar.msg("ERROR: StrmWriter DGV", "CLOSE the document listed below then try again." & NextLine & UserPath & strDoc & ".txt" & NextLine & ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
            Exit Function
        End Try

        'WRITE HEADINGS
        Dim Col As DataGridViewTextBoxColumn
        For Each Col In grdv.Columns
            sw.Write(Col.Name.ToString & Chr(9))
            i += 1
        Next
        For x As Integer = 1 To iBlanks + 1
            sw.WriteLine()
        Next x

        'WRITE DATA
        Dim iCol As Integer
        ' Dim iRow As Integer  '- the collection is generated using the sequence with which the user selected the rows
        '  For Each Row As DataGridViewRow In grdv.SelectedRows 
        For y As Int16 = 1 To iRepeat
            For Each row As DataGridViewRow In grdv.Rows
                If row.Selected = True Then
                    '  For iRow = 2 To datatbl.Rows.Count + 1   'from grid so sorted by user
                    For iCol = 0 To i - 1     'columns
                        sw.Write(IsNull(grdv.Item(iCol, row.Index).Value, "").ToString & Chr(9))
                    Next
                    sw.WriteLine()
                End If
            Next row
        Next y
        '...........................
        sw.Close()
        sw = Nothing

        MouseDefault()

        Return True

    End Function

 
    '4 WRITES TO CSV from STORED PROC with str param - used by delivra emails
    Public Function StrmWriter(ByVal strSP As String, ByVal ParamName As String, ByVal strParam As String, ByVal strDataDoc As String, ByVal strDocType As String, ByVal bOpen As Boolean) As Integer 'returns num records
        Dim sw As IO.StreamWriter
        Dim r As Integer = 0 'number of records
        Dim FullFileName As String = strDataDoc & strDocType
        MouseWait()

        Try
            sw = New IO.StreamWriter((FullFileName))
        Catch ex As Exception
            modGlobalVar.msg("ERROR: StrmWriter str", "CLOSE the document listed below then try again." & NextLine & ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Function
        End Try

        Dim strSQL As New SqlCommand(strSP, sc)
        strSQL.Parameters.Add(ParamName, SqlDbType.VarChar)
        strSQL.Parameters(ParamName).Value = strParam
        strSQL.CommandType = CommandType.StoredProcedure

        If Not SCConnect() Then
            Exit Function
        End If

        Dim drdr As SqlDataReader
        Try
            drdr = strSQL.ExecuteReader
        Catch ex As Exception
            modGlobalVar.msg("ERROR: StrmWriterm, str DR", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        drdr.Read()
        If drdr.HasRows Then
            r = r + 1
            Dim m As Integer = drdr.FieldCount - 1
            'get col headings
            For x As Int16 = 0 To m
                sw.Write(drdr.GetName(x).ToString)
                If x = m Then
                Else
                    sw.Write(",")
                End If
                '  sw.Write(drdr.GetName(1).ToString)
            Next x
            'FIRST LINE
            sw.WriteLine()
            For x As Int16 = 0 To m
                ' sw.Write(drdr.GetString(0) & ",")
                If IsDBNull(drdr.GetValue(x)) Then
                    sw.Write(" ")   '.csv (delivra) requires data in null fields
                Else
                    sw.Write(drdr.GetValue(x))
                End If
                If x = m Then
                Else
                    sw.Write(",")
                End If
            Next x
            sw.WriteLine()
            'SUBSEQUENT LINES
            While drdr.Read
                r = r + 1
                For x As Int16 = 0 To m
                    ' sw.Write(drdr.GetString(0) & ",")
                    If IsDBNull(drdr.GetValue(x)) Then
                        sw.Write(" ")   '.csv (delivra) requires data in null fields
                    Else
                        sw.Write(drdr.GetValue(x))
                    End If
                    If x = m Then
                    Else
                        sw.Write(",")
                    End If
                Next x
                'For x As Int16 = 0 To 1
                'If IsDBNull(drdr(x)) Then
                ' sw.Write(",")
                ' Else
                '        sw.Write(drdr.GetValue(x) & ",")
                '    End If
                'Next x
                sw.WriteLine()
            End While

        Else
            modGlobalVar.msg(MsgCodes.noResultCancel) ', "", MessageBoxButtons.OK, MessageBoxIcon.Information)
            bOpen = False
        End If


CloseAll:
        drdr.Close()
        sc.Close()
        sw.Close()
        sw = Nothing

        If bOpen Then
            OpenFile(FullFileName)
        Else
        End If


        MouseDefault()
        Return r

    End Function

    '2 WRITES TO TXT from STORED PROC with INT param; rquest feedback, contact intro, introletter, nametag
    Public Function StrmWriter(ByVal strSP As String, ByVal ID As Integer, ByVal strDataDoc As String, ByVal strHeading As String) As Boolean
        Dim sw As IO.StreamWriter
        Try
            sw = New IO.StreamWriter((UserPath & strDataDoc & ".txt"))
        Catch ex As Exception
            modGlobalVar.msg("ERROR: StrmWriter, strSP", "CLOSE the document listed below then try again." & NextLine & ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try

        Dim strSQL As New SqlCommand(strSP, sc)
        'stored proc requires extra heading at end
        strSQL.Parameters.Add("@IDval", SqlDbType.Int).Value = ID
        strSQL.Parameters.Add("@Heading", SqlDbType.VarChar).Value = strHeading
        'If strHeading = "1" Or strHeading = "0" Then
        '  strSQL.Parameters.Add("@Attend", SqlDbType.Bit)
        '  strSQL.Parameters("@Attend").Value = CType(strHeading, Integer)
        'End If
        strSQL.CommandType = CommandType.StoredProcedure

        If Not SCConnect() Then
            Return False
        End If

        Dim drdr As SqlDataReader

        Try
            drdr = strSQL.ExecuteReader
        Catch ex As Exception
            modGlobalVar.msg("ERROR: StrmWriter str, DR", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
            StrmWriter = False
            GoTo CloseAll
        End Try

        Dim NumFields As Integer = drdr.FieldCount
        If drdr.HasRows Then
            'get col headings
            For x As Int16 = 0 To NumFields - 1
                sw.Write(drdr.GetName(x).ToString & Chr(9))
            Next x
            sw.WriteLine()
            'get data
            While drdr.Read
                For x As Int16 = 0 To NumFields - 2 '2 here for unused heading
                    If IsDBNull(drdr(x)) Then
                        sw.Write(Chr(9))
                    Else
                        sw.Write(drdr.GetValue(x) & Chr(9))
                    End If
                Next x
                sw.Write(strHeading)    'for record delimiter
                sw.WriteLine()
            End While
        Else

            modGlobalVar.msg(MsgCodes.noResultCancel) ', "", MessageBoxButtons.OK, MessageBoxIcon.Information)
            StrmWriter = False
            GoTo Closeall
        End If

        StrmWriter = True

CloseAll:
        Try
            drdr.Close()
            sc.Close()
            sw.Close()
            strSQL.Dispose()
        Catch ex As Exception
            modGlobalVar.msg("ERROR: StrmWriter str ", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        MouseDefault()
    End Function

    '3 WRITES TO TXT from STORED PROC with 2 TEXT param; intro letter, grantproposal, grant letters, casefeedback
    Public Function StrmWriter(ByVal sProcName As String, ByVal IDVal As String, ByVal strDataDoc As String, ByVal strHeading As String) As Boolean
        Dim sw As IO.StreamWriter
        Try
            sw = New IO.StreamWriter((UserPath & strDataDoc & ".txt"))
        Catch ex As Exception
            modGlobalVar.msg("ERROR: StrmWriter strSP", "CLOSE the document listed below then try again." & NextLine & ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try

        Dim strSQL As New SqlCommand(sProcName, sc)
        'stored proc requires extra heading at end
        strSQL.Parameters.Add("@IDtxt", SqlDbType.VarChar).Value = IDVal
        strSQL.Parameters.Add("@Heading", SqlDbType.VarChar).Value = strHeading
        'If strHeading = "1" Or strHeading = "0" Then
        '  strSQL.Parameters.Add("@Attend", SqlDbType.Bit)
        '  strSQL.Parameters("@Attend").Value = CType(strHeading, Integer)
        'End If
        strSQL.CommandType = CommandType.StoredProcedure

        If Not SCConnect() Then
            Return False
        End If

        Dim drdr As SqlDataReader

        Try
            drdr = strSQL.ExecuteReader
        Catch ex As Exception
            modGlobalVar.msg("ERROR: StrmWriter str, DR Failed", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
            StrmWriter = False
            GoTo CloseAll
        End Try

        Dim NumFields As Integer = drdr.FieldCount

        If drdr.HasRows Then
            'get col headings
            For x As Int16 = 0 To NumFields - 1
                sw.Write(drdr.GetName(x).ToString & Chr(9))
            Next x
            sw.WriteLine()
            'get data
            While drdr.Read
                For x As Int16 = 0 To NumFields - 2 '2 here for unused heading
                    If IsDBNull(drdr(x)) Then
                        sw.Write(Chr(9))
                    Else
                        sw.Write(drdr.GetValue(x) & Chr(9))
                    End If
                Next x
                sw.Write(strHeading)    'for record delimiter
                sw.WriteLine()
            End While
        Else
            modGlobalVar.msg(MsgCodes.noResultCancel) ', "", MessageBoxButtons.OK, MessageBoxIcon.Information)
            StrmWriter = False
            GoTo Closeall
        End If

        StrmWriter = True
CloseAll:
        Try
            drdr.Close()
            sc.Close()
            sw.Close()
            strSQL.Dispose()
        Catch ex As Exception
        End Try
        MouseDefault()
    End Function

    '    'INFO LETTER (those not sent), REGISTRANTS
    '    ' GENERATES DATA FILE = DO NOT PERFORM MERGE SO USER CAN SELECT OWN MERGE TEMPLATE DOCUMENT
    '    Public Sub EmailInfoLetter(ByVal What As String, ByVal ID As Integer, ByVal strEvent As String)

    '        Dim datafileName As String = "DataDoc"
    '        Dim fullDatafileName As String = DataToExcel(datafileName, "InfoLetter")
    '        Dim strSProc As String
    '        Dim YN As Integer
    '        Dim SendEmail As New ClassEmail
    '        MouseWait()
    '        If fullDatafileName = String.Empty Then 'data file not created
    '            modGlobalVar.msg("ERROR with data file", "Check if " & UserPath & "Datadoc.xls is the most recent document on your C drive has the data you need and .", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '            GoTo CloseAll
    '        End If


    '        Select Case What
    '            Case "InfoLttr"
    '                If modPopup.StrmWriter("[MergeEvent]", ID, datafileName, "InfoLetter") = True Then
    '                    'count of emails vs non emails here
    '                    Dim cmd As New SqlClient.SqlCommand("[EventEmail]", sc)
    '                    cmd.CommandType = CommandType.StoredProcedure
    '                    cmd.Parameters.Add("@EventNum", SqlDbType.Int)
    '                    cmd.Parameters.Add("@Attended", SqlDbType.SmallInt)
    '                    cmd.Parameters("@Attended").Value = 5
    '                    cmd.Parameters("@EventNum").Value = ID
    '                    If Not SCConnect() Then
    '                        Exit Sub
    '                    End If
    '                    Dim str As String = cmd.ExecuteScalar.ToString
    '                    sc.Close()
    '                    cmd = Nothing

    '                    '   If strPathFileExt = String.Empty Then
    '                    ' modGlobalVar.Msg("Check if " & UserPath & "Datadoc.xls is the most recent document in MyDocuments and has the data you need and .", MessageBoxIcon.Exclamation, "ERROR with data file")

    '                    'Else
    '                    modGlobalVar.msg("Datafile OK", str & NextLine & " To merge email instead of paper, " & NextLine & _
    '                    " click 'Edit recipient list', " & NextLine & _
    '                    " select 'Filter', and set Email  to 'is not blank'," & NextLine & _
    '                    " then 'Finish & Merge' to 'Send email messages' instead of 'edit individual documents'.", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '                    OpenFile(fullDatafileName)

    '                    'End If
    '                End If
    '                'Exit Sub
    '            Case "InfoLttrNoEmail"
    '                If modPopup.StrmWriter("[MergeEvent]", ID, datafileName, "InfoLetterNoEmail") = True Then
    '                    'count of emails vs non emails here
    '                    Dim cmd As New SqlClient.SqlCommand("[EventEmail]", sc)
    '                    cmd.CommandType = CommandType.StoredProcedure
    '                    cmd.Parameters.Add("@EventNum", SqlDbType.Int).Value = ID
    '                    cmd.Parameters.Add("@Attended", SqlDbType.SmallInt).Value = 7
    '                    If Not SCConnect() Then
    '                        Exit Sub
    '                    End If
    '                    Dim str As String = cmd.ExecuteScalar.ToString
    '                    sc.Close()
    '                    cmd = Nothing
    '                    fullDatafileName = DataToExcel(datafileName, "NoEmail InfoLetter")
    '                    '  If strPathFileExt = String.Empty Then
    '                    '      modGlobalVar.Msg("Check if " & UserPath & "Datadoc.xls is the most recent document on your C drive has the data you need and .", MessageBoxIcon.Exclamation, "ERROR with data file")
    '                    '   Else
    '                    modGlobalVar.msg("Datafile OK", str & NextLine & " To merge email instead of paper, " & NextLine & _
    '                                           " click 'Edit recipient list', " & NextLine & _
    '                                           " select 'Filter', and set Email  to 'is not blank'," & NextLine & _
    '                                           " then 'Finish & Merge' to 'Send email messages' instead of 'edit individual documents'.", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '                    OpenFile(fullDatafileName)
    '                    ' End If
    '                End If
    '                GoTo closeall

    '            Case "OpenDataFile", "Registrant"
    '                If modPopup.StrmWriter("[MergeEvent]", ID, UserPath & "Datadoc", "all") = True Then
    '                    '  If strPathFileExt = String.Empty Then
    '                    '  Else
    '                    OpenFile(fullDatafileName)
    '                    ' End If
    '                End If
    '                GoTo CloseAll
    '            Case Else
    '                modGlobalVar.msg("ERROR: MergeInfoLetter", What & NextLine & "menu item not found", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '                GoTo CloseAll
    '        End Select

    'OpenEmail:
    '        SendEmail.SendEventEmails(ID, YN, strEvent) '--disabled per Aaron 11/14 re too many emails being sent
    'CloseAll:
    '        datafileName = Nothing
    '        fullDatafileName = Nothing
    '        YN = Nothing
    '        strSProc = Nothing
    '        MouseDefault()
    '    End Sub

    '    ' GENERATES TEXT FILE ; SETS CONDITIONS --DISCONTINUED
    '    Public Sub EmailEvent(ByVal What As String, ByVal ID As Integer, ByVal strEvent As String)
    '        'Handles miEmailRegistered.Click, miEmailAttended.Click, miSpreadsheet.Click, miMergeInfoLttr.Click

    '        Dim YN As Integer
    '        Dim SendEmail As New ClassEmail
    '        MouseWait()

    '        Select Case What
    '            Case "Registered"
    '                YN = 0
    '                GoTo OpenEmail
    '            Case "Attended"
    '                YN = 1
    '                GoTo OpenEmail
    '            Case "FirstEmail"
    '                YN = 6
    '                GoTo OpenEmail
    '            Case Else
    '                modGlobalVar.msg("ERROR: EmailEvent", What & NextLine & "menu item not found", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '                GoTo CloseAll
    '        End Select
    'OpenEmail:
    '        SendEmail.SendEventEmails(ID, YN, strEvent)
    'CloseAll:
    '        ' strDataDoc = Nothing
    '        YN = Nothing
    '        ' strSProc = Nothing
    '        MouseDefault()
    '    End Sub

    'email request for refund to Paypal Admin
    Public Sub MergeRefundRequest(ByVal ID As Integer, ByVal Amnt As String, ByVal strOrder As String)
        Dim strb As New StringBuilder
        Dim strSQL As New SqlCommand("[MergeEventRefund]", sc)
        strSQL.Parameters.Add("@RegID", SqlDbType.Int).Value = ID
        strSQL.CommandType = CommandType.StoredProcedure

        If Not SCConnect() Then
            Exit Sub
        End If

        Dim drdr As SqlDataReader

        Try
            drdr = strSQL.ExecuteReader(CommandBehavior.CloseConnection)
        Catch ex As Exception
            modGlobalVar.msg("ERROR: EventRefund", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
            GoTo CloseAll
        End Try

        strb.Append("COMMENT:  ")
        strb.AppendLine("%0A") 'chr(13)
        If drdr.HasRows Then
            While drdr.Read
                For x As Int16 = 0 To drdr.FieldCount - 1
                    strb.Append(drdr.GetName(x) + ":  ")
                    If IsDBNull(drdr(x)) Then
                    Else
                        strb.Append(drdr.GetValue(x))
                    End If
                    strb.AppendLine("%0A")
                    'strb.AppendLine()
                Next x
            End While
            drdr.Close()
            strb.Append("REFUND AMOUNT:  " + Amnt)
            strb.AppendLine("%0A")
            strb.Append("REQUESTED BY:  " + usrFirst + "  " + Now())

            EmailOutlook(PayPalAdmin.StaffEmail, "Registrant Cancellation Refund: " + strOrder, strb.ToString, usrEmail)
        Else

        End If
CloseAll:
        Try
            drdr.Close()
            drdr = Nothing
            strSQL = Nothing
            strb = Nothing
        Catch ex As Exception
            '  modGlobalVar.Msg(ex.Message, , "ERROR: ")
        End Try

        MouseDefault()

    End Sub


    'WRITES TO TXT from STORED PROC with int param; merge email w leftover in print
    Public Function StrmWriterEmailPrint(ByVal strSP As String, ByVal IDVal As Integer, ByVal strDataDoc As String, ByVal strHeading As String) As Integer
        'USED BY InfoLetter
        Dim sw1 As IO.StreamWriter
        Dim cntNoEmail As Int16 = 0
        'TODO remove first writer, as sp only has one query now
        Try

            sw1 = New IO.StreamWriter((UserPath & strDataDoc & ".txt"))
        Catch ex As Exception
            modGlobalVar.msg("ERROR: StrmWriterEmailPrint", "CLOSE the document listed below then try again." & NextLine & ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
            StrmWriterEmailPrint = 0
        End Try

        Dim strSQL As New SqlCommand(strSP, sc)
        strSQL.Parameters.Add("@IDVal", SqlDbType.Int)
        strSQL.Parameters("@IDVal").Value = IDVal
        strSQL.Parameters.Add("@Heading", SqlDbType.VarChar)
        strSQL.Parameters("@Heading").Value = strHeading
        'If strHeading = "1" Or strHeading = "0" Then
        '    strSQL.Parameters.Add("@Attend", SqlDbType.Bit)
        '    strSQL.Parameters("@Attend").Value = CType(strHeading, Integer)
        'End If
        strSQL.CommandType = CommandType.StoredProcedure

        If Not SCConnect() Then
            '    modGlobalVar.Msg("no connection")
            Return False
            ' Exit Function
        End If

        '  modGlobalVar.Msg("opening datareader")
        Dim drdr As SqlDataReader

        Try
            drdr = strSQL.ExecuteReader(CommandBehavior.CloseConnection)
        Catch ex As Exception
            modGlobalVar.msg("ERROR: StrmWriterEmailPrint, DR", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
            StrmWriterEmailPrint = 0
            GoTo CloseAll
        End Try

        Dim m As Integer = drdr.FieldCount

        If drdr.HasRows Then
            StrmWriterEmailPrint = 1
            'get col headings
            For x As Int16 = 0 To drdr.FieldCount - 1
                sw1.Write(drdr.GetName(x).ToString & Chr(9))
                ' sw2.Write(drdr.GetName(x).ToString & Chr(9))
            Next x
            sw1.WriteLine()
            ' sw2.WriteLine()
            While drdr.Read
                For x As Int16 = 0 To drdr.FieldCount - 2
                    If IsDBNull(drdr(x)) Then
                        sw1.Write(Chr(9))
                    Else
                        sw1.Write(drdr.GetValue(x) & Chr(9))
                    End If
                Next x
                sw1.Write(strHeading)
                sw1.WriteLine()
                If IsDBNull(drdr(drdr.GetOrdinal("Email"))) Then
                    cntNoEmail = cntNoEmail + 1
                End If
            End While
            StrmWriterEmailPrint += cntNoEmail
        End If

        'drdr.NextResult()
        'If drdr.HasRows Then
        '    StrmWriterEmailPrint = 2
        'End If
        'While drdr.Read
        '    For x As Int16 = 0 To drdr.FieldCount - 2
        '        If IsDBNull(drdr(x)) Then
        '            sw2.Write(Chr(9))
        '        Else
        '            sw2.Write(drdr.GetValue(x) & Chr(9))
        '        End If
        '    Next x
        '    sw2.Write(strHeading)
        '    sw2.WriteLine()
        'End While

CloseAll:
        drdr.Close()
        sc.Close()
        sw1.Close()
        ' sw2.Close()
        MouseDefault()
        Return StrmWriterEmailPrint
    End Function


    '    'WRITES TO TXT from STORED PROC second results with 2 params
    Public Function StrmWriterEmailPrint(ByVal strHeading As String) As Integer 'ByVal IDVal As Integer, ByVal strHeading As String ) As Integer
        '2/16 for file listing eventers with no email to supplement email list
        Dim sw1 As IO.StreamWriter
        Dim cntNoEmail As Int16 = 0
        Try
            sw1 = New IO.StreamWriter(UserPath & "NoEmails.csv")
        Catch ex As Exception
            modGlobalVar.msg("ERROR: StrmWriterEmailPrint", "CLOSE the document listed below then try again." & NextLine & ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
            StrmWriterEmailPrint = 0
        End Try

        Dim strSQL As New SqlCommand("[MailUserEventNoEmail]", sc)
        ' strSQL.Parameters.Add("@EventNum", SqlDbType.Int)
        ' strSQL.Parameters("@EventNum").Value = IDVal
        ' strSQL.Parameters.Add("@Attended", SqlDbType.VarChar)
        ' strSQL.Parameters("@Attended").Value = strHeading
        strSQL.CommandType = CommandType.StoredProcedure

        If Not SCConnect() Then
            Return False
            Exit Function
        End If

        Dim drdr As SqlDataReader
        Try
            drdr = strSQL.ExecuteReader()
            '   drdr.NextResult() split sproc instead as was messing up email table
        Catch ex As Exception
            modGlobalVar.msg("ERROR: StrmWriterEmailPrint, DR", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
            '   StrmWriterEmailPrint = 0
            drdr.Close()
            sc.Close()
            sw1.Close()
            Return cntNoEmail
        End Try

        Dim m As Integer = drdr.FieldCount
        If drdr.HasRows Then
            '  StrmWriterEmailPrint = 1
            'get col headings
            For x As Int16 = 0 To drdr.FieldCount - 1
                sw1.Write(drdr.GetName(x).ToString + ",")
            Next x
            sw1.WriteLine()
            'get data
            While drdr.Read
                For x As Int16 = 0 To drdr.FieldCount - 1
                    If IsDBNull(drdr(x)) Then
                        sw1.Write(",")
                    Else
                        sw1.Write(Replace(drdr.GetValue(x).ToString, ",", " ") + ",")
                        ' sw1.Write(ControlChars.Tab) 'vbTab) 'Chr(9))"\t"
                    End If
                Next x
                sw1.Write(strHeading)
                sw1.WriteLine()
                '   If IsDBNull(drdr(drdr.GetOrdinal("Email"))) Then
                cntNoEmail = cntNoEmail + 1
                'End If
            End While
            ' StrmWriterEmailPrint = cntNoEmail
            drdr.Close()
            sc.Close()
            sw1.Close()

            Try
                'File.Open(UserPath & "NoEmails.csv", FileMode.Open)
                System.Diagnostics.Process.Start(UserPath & "NoEmails.csv")
            Catch ex As Exception
                MsgBox(ex.Message & NextLine & UserPath & "NoEmails.csv", , "error opening file ")
            End Try
        Else
            drdr.Close()
            sc.Close()
            sw1.Close()
        End If

CloseAll:

        ' MouseDefault()
        Return cntNoEmail
    End Function



    '    '1 WRITES TO TXT from STORED PROC with TEXT param; intro letter, 'casefeedback
    '    Public Function StrmWriter(ByVal sProcName As String, ByVal IDVal As String, ByVal strDataDoc As String) As Boolean
    '        Dim sw As IO.StreamWriter
    '        Try
    '            sw = New IO.StreamWriter((UserPath & strDataDoc & ".txt"))
    '        Catch ex As Exception
    '            modGlobalVar.Msg("CLOSE the document listed below then try again."& NextLine &  ex.Message, MessageBoxIcon.Error, "ERROR: StrmWriter strSP")
    '            Return False
    '        End Try

    '        Dim strSQL As New SqlCommand(sProcName, sc)
    '        'stored proc requires extra heading at end
    '        strSQL.Parameters.Add("@IDtxt", SqlDbType.VarChar).Value = IDVal
    '        'strSQL.Parameters.Add("@Heading", SqlDbType.VarChar).Value = strHeading
    '        'If strHeading = "1" Or strHeading = "0" Then
    '        '  strSQL.Parameters.Add("@Attend", SqlDbType.Bit)
    '        '  strSQL.Parameters("@Attend").Value = CType(strHeading, Integer)
    '        'End If
    '        strSQL.CommandType = CommandType.StoredProcedure

    '        If Not SCConnect() Then
    '            Return False
    '        End If

    '        Dim drdr As SqlDataReader

    '        Try
    '            drdr = strSQL.ExecuteReader
    '        Catch ex As Exception
    '            modGlobalVar.Msg(ex.Message, MessageBoxIcon.Error, "ERROR: StrmWriter str, DR Failed")
    '            StrmWriter = False
    '            GoTo CloseAll
    '        End Try

    '        Dim NumFields As Integer = drdr.FieldCount

    '        If drdr.HasRows Then
    '            'get col headings
    '            For x As Int16 = 0 To NumFields - 1
    '                sw.Write(drdr.GetName(x).ToString & Chr(9))
    '            Next x
    '            sw.WriteLine()
    '            'get data
    '            While drdr.Read
    '                For x As Int16 = 0 To NumFields - 2 '2 here for unused heading
    '                    If IsDBNull(drdr(x)) Then
    '                        sw.Write(Chr(9))
    '                    Else
    '                        sw.Write(drdr.GetValue(x) & Chr(9))
    '                    End If
    '                Next x
    '                sw.Write("")    'for record delimiter
    '                sw.WriteLine()
    '            End While
    '        Else
    '            modGlobalVar.Msg("   no data found       ", MessageBoxIcon.Information, "cancelling request")
    '            StrmWriter = False
    '            GoTo Closeall
    '        End If

    '        StrmWriter = True
    'CloseAll:
    '        Try
    '            drdr.Close()
    '            sc.Close()
    '            sw.Close()
    '            strSQL.Dispose()
    '        Catch ex As Exception
    '        End Try
    '        MouseDefault()
    '    End Function

#End Region  'streamwriter

#Region "File IO"
    '========= ATTACHED FILES ========
    'used by Resources, Cases, Grants, Events, Organizations (viability as venue checklist)
    'on Load/Reload: get count and list of attached files for popup menus from button or menu item
    'put count in button tag and/or button text
    'when click button, can attach new or open listed file
    'to attach new, dialog box opens defaulting to user's drive, and saving in item appropriate folder shared drive under LinkedFiles
    'LinkedFilePath = "\\ICCNAS1\Users\Shared\LinkedFiles\" + Resources\ etc.
    'RESOURCE DOWNLOADS themselves to instead to Shared\CRG_Resources
    '========== cg 4/14 ===============


    'ATTACH FILE i.e. rename with item number, store in LinkedFiles.  Case, Resource
    Public Sub AttachFiles(ByVal vWhat As String, ByVal vSavePath As String, ByVal vID As Integer)

        'MOVED TO CLASS LIBRARY
        cExternalFile.AttachFiles(vWhat, vSavePath, vID)
        Exit Sub
    End Sub

    'LOAD ATTACHED FILE NAMES INTO MENU with Button and Menu item.  Case, Resource, Event
    Public Sub FindFiles(ByVal vID As Integer, ByVal vDoc As String, ByRef vPP As ContextMenu, ByVal vEH As EventHandler, ByRef vMI As MenuItem, ByRef vBtn As Button, ByRef vIMG As Image, ByRef vTip As ToolTip)
        '-- 4/14 cg ----
        ' vID = id number to name document,vDoc = full path, vPP = Popoup Menu,  eh = event handler for popup, 
        'vMI = open menu item, vBtn = open button ,iIMG = image for button, vTip = tool tip for button
        '     FindFiles(Me.CaseID.Text, strDocPath, ppFile, ehFile, Me.miOpenFile, Me.btnViewFile, My.Resources.Report, Me.ToolTip1, Nothing)
        'MOVED TO EXTERNAL CLASS LIBRARY
        Dim x As Integer
        If vMI Is Nothing Then
            x = cExternalFile.FindFiles(vID, vDoc, vPP, vEH, vBtn, vIMG, vTip)
            vBtn.Text = "Image " & x.ToString
        Else
            x = cExternalFile.FindFiles(vID, vDoc, vPP, vEH, vMI, vBtn, vIMG, vTip)
            vBtn.Text = "Files  " & x.ToString
        End If

        If x = 0 Then
            vBtn.Tag = "Attach"
        Else
            vBtn.Tag = x.ToString
        End If

        Exit Sub
    End Sub

    'LOAD ATTACHED FILE NAMES INTO MENU with Button and Menu item.  Event
    Public Sub FindFiles(ByVal vID As Integer, ByVal vDoc As String, ByRef vPP As ContextMenu, ByVal vEH As EventHandler, ByRef vMI As MenuItem, ByRef vBtn As Button, ByRef vIMG As Image, ByRef vTip As ToolTip, ByVal vColEventDocPref As Collection)
        '-- 8/13 cg ----
        ' vID = id number to name document,vDoc = full path, vPP = Popoup Menu,  eh = event handler for popup, 
        'vMI = open menu item, vBtn = open button ,iIMG = image for button, vTip = tool tip for button
        '     FindFiles(Me.CaseID.Text, strDocPath, ppFile, ehFile, Me.miOpenFile, Me.btnViewFile, My.Resources.Report, Me.ToolTip1, Nothing)
        'MOVED TO EXTERNAL CLASS LIBRARY
        cExternalFile.FindFiles(vID, vDoc, vPP, vEH, vMI, vBtn, vIMG, vTip, vColEventDocPref)

        Exit Sub

    End Sub

    'LOAD IMAGE FILE NAMES INTO MENU. specifically images for NEW CRG  "\\ICCNAS1\Users\Shared\LinkedFiles\ResourceImages\" --not useed??
    Public Sub AttachImages(ByVal vID As Integer, ByVal vSavePath As String, ByRef vPP As ContextMenu, ByVal vEH As EventHandler, ByRef vBtn As Button)
        Dim fo As New OpenFileDialog
        Dim fs As New SaveFileDialog
        Dim txt, ext, newName, PathName As String
        Dim i As Integer
        Dim pref As String

        fo.Title = "Select image to associate with this Resource"
        fo.InitialDirectory = UserPath
        fo.Filter = ".jpg" '"All files (*.*)|*.*"
        fo.FileName = ""

        fs.Title = "Confirm name of new file "
        fs.InitialDirectory = vSavePath
        fs.Filter = "All files (*.*)|*.*"
        fs.FileName = vID.ToString & "*"

        If fo.ShowDialog = DialogResult.OK Then
            txt = fo.FileName.Substring(fo.FileName.LastIndexOf("\") + 1)
            i = txt.IndexOf(".")
            If i > 0 Then
            Else
            End If
        Else    'user cancelled open file
            Exit Sub
        End If
        ext = txt.Substring(i, Len(txt) - i)
        If ext = ".exe" Then
            modGlobalVar.msg("Invalid file: " & ext, "select a different file type", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            GoTo clearSub
        End If

        newName = InputBox("optional: change new file name before saving.  ", "Click OK to save", txt) '.Substring(0, i))
        If Len(newName) > 0 Then
            'PREVENT USER FROM CHANGING .EXTENSION
            If newName.Contains(".") Then
                Dim j As Integer = newName.IndexOf(".") 'prevent user from changing filetype
                PathName = vSavePath & vID.ToString & " " & pref & newName.Substring(0, j) & ext
            Else
                PathName = vSavePath & vID.ToString & " " & pref & newName & ext
            End If
            'SAVE FILE
            Try
                File.Copy(fo.FileName, PathName, False)
            Catch ex As Exception
                modGlobalVar.msg("ERROR: copying image 1", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
            'MAKE READONLY
            Try
                File.SetAttributes(PathName, FileAttributes.ReadOnly)
            Catch ex As Exception
                modGlobalVar.msg("ERROR: copying image 2", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If

ClearSub:
        ext = Nothing
        txt = Nothing
        newName = Nothing

    End Sub


    'GET FILE NAME returns name with extension, optional path; accepts wildcard
    Public Function GetFileName(ByVal strpath As String, ByVal strFIle As String, ByVal InclPath As Boolean) As String
        'returns file name/optional path, "Not Found","Error"
        Dim strFilenameExt As String
        strFilenameExt = Dir(strpath & strFIle)
        Try
            If strFilenameExt <> vbNullString Then
                If InclPath = True Then
                    GetFileName = strpath & strFilenameExt
                Else
                    GetFileName = strFilenameExt
                End If

            Else
                GetFileName = "not found"
            End If
        Catch ex As Exception
            GetFileName = "error"
        End Try
        strFilenameExt = Nothing

    End Function

    'TODO Test with other user has file open
    Public Function WhoHasFileOpen(ByVal strPath As String, ByVal strFilenameExt As String) As String
        'returns owner of ~$file even if ~$file not found by dir
        Dim secUtil As Object
        Dim drw As DataRow()
        Dim secDesc As Object

        Try
            secUtil = CreateObject("ADsSecurityUtility")
            secDesc = secUtil.GetSecurityDescriptor(strPath & "~$" & strFilenameExt, 1, 1) 'creator of !$ file
            WhoHasFileOpen = secDesc.owner

            drw = tblStaff.Select("Logonname = '" & secDesc.owner.Substring(4) & "'") 'sometimes = builtin\administrators
            If usr = drw(0)("StaffID") Then
                WhoHasFileOpen = "user" 'USER HAD FILE OPEN
            Else
                WhoHasFileOpen = IsNull(drw(0)("StaffFirstnameFirst"), "?") 'SOMEONE ELSE HAS FILE OPEN
            End If
        Catch ex As System.Exception 'COULD NOT FIND ~$FILE (or user 'TODO building\Adminstrator???) so is not locked
            WhoHasFileOpen = "false"
            'file creator: secDesc = secUtil.GetSecurityDescriptor(LinkedPath & "Events\" & strFileName.Substring(2), 1, 1)
        End Try
        secUtil = Nothing
        secDesc = Nothing
        drw = Nothing

    End Function

    ''GET FILE NAME given path and partial file name, returns file name + .extension with optional path
    'Public Function FindFile(ByVal strPath As String, ByVal strSrchDoc As String, ByVal strExt As String, ByVal bReturnPath As Boolean) As String 'returns first file name and extension
    '    'KEEP THIS for directory.getfiles example ---
    '    '--8/13--
    '    'some functions require only file name for filling list
    '    'others require full name and path so can open
    '    'File.exists does not take wildcard; .GetFiles will return all file types
    '    '1/16 new with framwork 4 EnumerateFiles faster than GetFiles
    '    '--- cg ---
    '    '--2/16 replaced with DIR() as this sometimes get Error: can't convert string to string
    '    Dim arFileNames 'As String()
    '    Dim strFoundFile As String

    '    Try
    '        '   arFileNames = Directory.GetFiles(strPath, strSrchDoc & strExt, SearchOption.TopDirectoryOnly)'waits for complete search
    '        arFileNames = Directory.EnumerateFiles(strPath, strSrchDoc & strExt, SearchOption.TopDirectoryOnly) 'availble before search complete
    '    Catch ex As Exception
    '        modGlobalVar.msg("ERROR: network issue", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        FindFile = "error"
    '        Exit Function
    '    End Try

    '    Try
    '        ' strFoundFile = arFileNames(0).ToString() 'incl path
    '        arFileNames.movenext() 'default positon is before first item
    '        strFoundFile = IsNull(arFileNames.current, "not found")

    '        If bReturnPath = True Then
    '            FindFile = strFoundFile 'Path.GetFullPath(arFileNames(0))
    '        Else 'return only file name
    '            FindFile = Path.GetFileName(strFoundFile) 'without path
    '        End If
    '    Catch ex As Exception
    '        FindFile = "not found"
    '    End Try

    '    'If arFileNames(0).Length = 0 Then ' = String.Empty Then

    '    'Else
    '    '    If bReturnPath = True Then
    '    '        FindFile = arFileNames(0) 'Path.GetFullPath(arFileNames(0))
    '    '    Else 'return only file name
    '    '        FindFile = Path.GetFileName(arFileNames(0))
    '    '    End If
    '    'End If
    '    '-----------
    '    '   KEEP: file name with extension: " & Path.GetFileName(FullFileName(0)) 
    '    '       "Get Directory Name: " & Path.GetDirectoryName(FullFileName(0)) 
    '    '       "Get path and file name with extension: " & Path.GetFullPath(FullFileName(0))
    '    'Directory.EnumerateFiles(strPath)' .net.4
    'End Function 'findfile

#End Region 'FileIO

#Region "External Documents"

    'OPEN A FILE
    Public Function OpenFile(ByVal strFullName As String) As Boolean   'yes = it opened
        '-- 8/13 ----
        'use process.start as it informs if file already open and gives choice of notify
        '-- cg ----
        If strFullName = "error" Then
            ' modGlobalVar.Msg("network error", MessageBoxIcon.Error, "Error opening file")
            Return False
        End If
        MouseWait()
        Dim process As System.Diagnostics.Process
        process = New System.Diagnostics.Process
        Try
            process.StartInfo.FileName = strFullName
            process.StartInfo.WindowStyle = ProcessWindowStyle.Maximized

            process.Start() 'strFullName)
            '     process.WaitForInputIdle(1000) 'TO DO could this hang indefinately?
            OpenFile = True
            ' process.WaitForExit()
        Catch ex As Exception
            '1156 ERROR_DDE_FAIL "An error occurred in sending the command to the application. "
            '5 = "no process is associated with this object"
            If Err.Number = 5 Or Err.Number = 1156 Then 'try again
                Try

                    process.Start(strFullName)
                    ' process.WaitForInputIdle(2000) 'TO DO could this hang indefinately?
                    OpenFile = True
                Catch exc As Exception
                    OpenFile = False
                    '  modGlobalVar.Msg(ex.Message & NextLine &  Err.Number & " " & Err.Description& NextLine &  strFullName, MessageBoxIcon.Error, "error opening file 2")
                End Try
            Else
                OpenFile = False
                modGlobalVar.msg("Error opening file 1", ex.Message & NextLine & Err.Number & " " & Err.Description & NextLine & strFullName, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End Try

        MouseDefault()
    End Function

    'DISPOSE OF EXCEL    'RELEASE COM OBJECT (Excel)
    Public Sub ReleaseComObject(o As Object)
        Try
            While (System.Runtime.InteropServices.Marshal.ReleaseComObject(o) > 0)
            End While
        Catch
        Finally
            o = Nothing
        End Try
    End Sub

    'format word reports
    Public Sub FormatGray(ByVal sel As Word.Selection, ByVal str As String, ByVal bColon As Boolean)
        sel.Font.Color = WdColor.wdColorGray45
        If bColon = True Then
            sel.TypeText(str & ": ")
        Else
            sel.TypeText(str)
        End If
        sel.Font.Color = WdColor.wdColorAutomatic
        str = Nothing
    End Sub

#End Region 'external documents

#Region "New User Reports"

    'EXCEL: SHADE ROWS BY SATELLITE REGION
    Public Function ShadeExcel(ByRef r1 As Excel.Range, ByVal iColNum As Integer) As Boolean
        ' Dim r1 As Excel.Range

        ' r1 = obook.ActiveSheet.usedrange

        For i As Integer = 2 To r1.Rows.Count
            r1.Rows(i).select()

            Select Case IsNull(r1.Cells.Cells(i, iColNum).value, "no").ToString
                Case Is = "Central" 'blue
                    '  r1.Cells.Interior.ColorIndex = 20
                    '  r1.Rows(i).interior.color = RGB(209, 235, 255)
                    r1.Rows(i).interior.color = RGB(232, 247, 254)
                Case Is = "NE"  'green
                    ' r1.Cells.Interior.ColorIndex = 35 '219,229,241
                    r1.Rows(i).interior.color = RGB(229, 255, 225)
                Case Is = "SW"  'tan/orange//coral
                    'r1.Rows(i).Interior.ColorIndex = 40
                    r1.Rows(i).Interior.Color = RGB(242, 221, 220) '(230, 200, 170)
                Case Is = "SE"  'darker blue as per website; red
                    r1.Rows(i).Interior.Color = RGB(184, 208, 242) ' IinfoCtr blue = 170,198,250 '125, 150, 215) '242, 202, 207) '(234, 170, 177)
                    '233,175,185
                Case Is = "NW"  'yellow
                    ' r1.Cells.Interior.ColorIndex = 36   
                    r1.Rows(i).interior.color = RGB(255, 255, 213)
                Case Else
                    ' r1.Rows(i).Interior.ColorIndex = 36
            End Select
        Next i
        Return True

    End Function

    'TREEVIEW: UNTICK CHILDREN
    Public Sub CheckChildNode(ByVal currNode As TreeNode)
        'set the children check status to the same as the current node
        Dim checkStatus As Boolean = currNode.Checked


        For Each node As TreeNode In currNode.Nodes
            node.Checked = checkStatus
            CheckChildNode(node)
            If checkStatus = True Then
                iTrvChecked = iTrvChecked + 1
            End If
        Next

    End Sub

    'TREEVIEW: UNTICK/SHADE PARENT
    Public Sub CheckParentNode(ByVal currNode As TreeNode)
        Dim parentNode As TreeNode = currNode.Parent
        Dim bChecked As Boolean = False
        Dim bNotChecked As Integer = 0
        If parentNode Is Nothing Then
            Exit Sub
        End If

        ' parentNode.Checked = True
        ' parentNode.ForeColor = Color.Black
        For Each node As TreeNode In parentNode.Nodes
            If node.Checked Then
                bChecked = True
                node.BackColor = Color.Transparent
                ' Exit For
            Else
                bNotChecked = bNotChecked + 1
                parentNode.Checked = False
            End If
        Next node

        Select Case bChecked 'at least some are checked
            Case True
                If bNotChecked > 0 Then ' some are unchecked
                    parentNode.BackColor = Color.LightGray
                    parentNode.Checked = False
                Else 'all are checked
                    parentNode.Checked = True
                    parentNode.BackColor = Color.Transparent
                End If
            Case False  'no children are checked
                parentNode.BackColor = Color.Transparent 'Color.LightGray
                parentNode.Checked = False
        End Select
        CheckParentNode(parentNode)
    End Sub

#End Region 'new user reports

#Region "Event MasterChecklist HostOverview"
    'FOR: visibility of buttons and menu items on EventSearch and Detail forms

    'ADD REGION (Excel) to existing spreadsheet - RETURNS SuccessFail
    Public Function InsertRegionDate(ByVal strFilenameExt As String, ByVal strRegion As String, dt As DateTime) As Boolean
        'works 2/6/15 cg
        Dim objExcel As New EXL.Application
        Dim objBooks As EXL.Workbooks  'to avoid double dot notation
        Dim objWorkbook As EXL.Workbook
        Dim objSheet As EXL.Worksheet
        Dim objRange As EXL.Range
        Dim iCRtn, InsertHere As Int16
        Dim bFound As Boolean = False
        '  Dim bOpen As Boolean
        Dim strUser As String

        'see if spreadsheet is already open
        strUser = WhoHasFileOpen(LinkedPath & "Events\", strFilenameExt) ''LinkedPath & "Events\" & DocName & ".xlsm") 'IsDocLocked(LinkedPath & "Events\", DocName)

        Select Case strUser
            Case Is = "user"    '
                modGlobalVar.msg("Cancelling Request.", "Cannot add this event to the Master Checklist while it is open." &
                        NextLine & "Close the MasterChecklist and try again. " & NextLine & usrFirst, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Case Is = "error"   'system error
            Case Is <> "false" 'another user
                modGlobalVar.msg("Cancelling Request.", "Cannot add this event to the Master Checklist while  " & UCase(strUser) & " has it open.", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Case Else '"false" i.e. not already open, add cols
                '   End Select
                'If strUser = "user" Then 'not open go ahead
                Try
                    objBooks = objExcel.Workbooks
                    objWorkbook = objBooks.Open(LinkedPath & "Events\" & strFilenameExt)
                    objSheet = objWorkbook.Worksheets(1)
                    'INSERT COLUMN IN DATE SEQUENCE
                    'straight insert locks cells; instead, find first blank column, copy and insert in proper place
                    'at same time segment out date based on NextLine
                    objSheet.Unprotect("event")
                    objRange = objSheet.Range("C2:M2")

FindFirstBlankColumn:
                    For Each c As EXL.Range In objRange.Cells
                        If c.Value = String.Empty Then
                            c.EntireColumn.Copy()
                            Exit For
                        End If
                    Next c

CompareDates:
                    For Each c2 As EXL.Range In objRange.Cells
                        iCRtn = InStr(c2.Value2, NextLine)
                        '   modGlobalVar.Msg(c2.Value.substring(iCRtn).ToString& NextLine &  c2.Value2.substring(iCRtn).ToString, , iCRtn.ToString)
                        If (iCRtn = 0) Then
                            InsertHere = c2.Column
                        Else
                            If CType(c2.Value2.substring(iCRtn), DateTime) > CType(dt, DateTime) Then
                                InsertHere = c2.Column
                            End If
                        End If

                        If InsertHere > 0 Then
                            ' Try
                            c2.EntireColumn.Insert()
                            objSheet.Cells(1, InsertHere).value = "Include date and your initials."
                            objSheet.Cells(2, InsertHere).value = Replace(strRegion & NextLine & dt, " ", "")
                            Exit For
                            'Catch ex As Exception
                            'modGlobalVar.Msg(ex.Message, , "failed insert column")
                            'Exit For
                            'Finally
                            'End Try
                        End If
                    Next c2
                    objSheet.Visible = Excel.XlSheetVisibility.xlSheetVisible 'required or moniker makes it hidden
                    objSheet.Protect("event")
                    objWorkbook.Save()
                    InsertRegionDate = True

                Catch ex As Exception
                    InsertRegionDate = False
                    modGlobalVar.msg("Error inserting column", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try

ReleaseExcel:
                Try
                    objWorkbook.Close()
                    ' objExcel.Quit()
                Catch ex As Exception
                    '  MsgBox(ex.Message, , "objworkbook wasn't open")
                End Try

                Try
                    objRange = Nothing
                    objSheet = Nothing
                    objWorkbook = Nothing
                    objBooks = Nothing
                    objExcel = Nothing
                    ReleaseComObject(objRange)
                    ReleaseComObject(objSheet)
                    ReleaseComObject(objWorkbook)
                    ReleaseComObject(objBooks)
                    ReleaseComObject(objExcel)
                    '  Marshal.ReleaseComObject(wbs)                    ' Marshal.ReleaseComObject(newWb)
                Catch ex As Exception
                    InsertRegionDate = False
                    modGlobalVar.msg("ERROR: releasing excel", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
        End Select
        'Else

        '' strUser = WhoHasFileOpen(LinkedPath & "Events\", strFilenameExt)
        'modGlobalVar.msg("Cancelling Request.", "Cannot add this event to the Master Checklist at this time." &
        '                 NextLine & "The Master Checklist is open by " & NextLine & strUser, MessageBoxButtons.OK, MessageBoxIcon.Information)
        'End If

    End Function

    'CONFIRM this REGION and DATE are in the MasterSpreadsheet
    Public Function isRegionDateCol(ByVal strMaster As String, ByVal EventRegion As String, ByVal EventDt As Date) As Boolean
        '2/16 use  table, not search excel
        Dim cmd As New SqlCommand("SELECT  Eventnum from luEventMasterSpreadsheet where MasterName = '" & strMaster & "' and Satelliteregion = '" & EventRegion & "' and SatelliteDate = '" & EventDt & "'", sc)
        If Not SCConnect() Then
            Exit Function
        End If
        Try
            If cmd.ExecuteScalar() > 0 Then
                isRegionDateCol = True
            Else
                isRegionDateCol = False
            End If
        Catch ex As Exception
            isRegionDateCol = False
        End Try
        sc.Close()

    End Function

    '    'CHECK REGION DATE (Excel)- RETURNS String
    '    Public Function IsRegionDateCol(ByVal strPathName As String, ByVal strRegion As String, ByVal strDate As String) As Boolean

    '        '  =========  works, but takes too long and leaves excel process and asks some users "Save?"
    '        'RETURNS T or F
    '        Dim objExcel As New Excel.Application
    '        Dim objSheet As Excel.Worksheet
    '        Dim oBooks As Excel.Workbooks 'to avoid double dot notation
    '        oBooks = objExcel.Workbooks
    '        Dim objWorkbook As Excel.Workbook
    '        Dim r As Excel.Range
    '        Dim bFound As Boolean = False

    '        Try
    '            objWorkbook = oBooks.Open(strPathName, False, True)
    '            objSheet = objWorkbook.Worksheets(1)
    '            r = objSheet.Range("C2:M2")
    '        Catch ex As Exception
    '            ' MsgBox(ex.Message, , "error  ")
    '        End Try


    '        For Each c As Excel.Range In r.Cells

    '            If c.Value2 > " " Then
    '                '   If c.Value2 = strRegion& NextLine &  strDate Then 'value2
    '                If c.Value2.contains(strRegion) And c.Value2.contains(strDate) Then
    '                    bFound = True
    '                    '     modGlobalVar.Msg(Replace(c.Value2, " ", "").ToString& NextLine &  strRegion& NextLine &  strDate, , "Matched")
    '                    Exit For
    '                Else 'does not match
    '                    '     modGlobalVar.Msg(Replace(c.Value2, " ", "").ToString& NextLine &  Replace(strRegion& NextLine &  strDate, " ", "").ToString, , "NOT matched")
    '                    'leave bfound false
    '                End If
    '            Else 'cell is empty therefore past filled range
    '                Exit For
    '            End If
    '        Next c

    'ReleaseExcel:
    '        Try
    '            r = Nothing
    '            objWorkbook.Close()
    '            '  objExcel.Quit()
    '            ReleaseComObject(objSheet)
    '            ReleaseComObject(objWorkbook)
    '            ReleaseComObject(oBooks)
    '            ReleaseComObject(objExcel)
    '        Catch ex As Exception
    '            modGlobalVar.msg("ERROR: closing Excel 2", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        Finally
    '        End Try

    '        Return bFound

    '        '    ''====== can't get this to work
    '        'Public Function IsRegionDateCol(ByVal strPath As String, ByVal strFileName As String, ByVal strRegion As String, ByVal strDate As String) As Boolean
    '        '    Dim FIles

    '        '    Try
    '        '        FIles = From chkfile In Directory.EnumerateFiles(strPath, strFileName.Substring(strPath.Length), SearchOption.TopDirectoryOnly)    '        '                From line In File.ReadLines(chkfile)    '        '                    Where line.Contains(strRegion)    '        '                    Select New With {.curFile = chkfile, .curLine = line}    '        '        ' Where (file.Contains(strRegion)) ' And file.Contains(strDate))
    '        '        '  Select New With {.curFile = chkFile, .curLine = Line}
    '        '    Catch ex As Exception
    '        '        modGlobalVar.msg("ERROR: network issue", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        '        IsRegionDateCol = False
    '        '        Exit Function
    '        '    End Try
    '        '    Try
    '        '        ' MsgBox(FIles.count.ToString)
    '        '        ' strFoundFile = arFileNames(0).ToString() 'incl path
    '        '        FIles.movenext() 'default positon is before first item
    '        '        MsgBox(IsNull(FIles.current, "not found"))
    '        '        ' If IsNull(FIles.current.ToString, "not found") = "not found" Then

    '        '        'For Each f In FIles    '        '        '    Console.WriteLine("{0}\t{1}", f.curFile, f.curLine)    '        '        'Next    '        '        'Console.WriteLine("{0} files found.", FIles.Count.ToString())    '        '        'Next f

    '        '        'Return False
    '        '        'Else
    '        '        Return True
    '        '        'End If

    '        '    Catch ex As Exception
    '        '        MsgBox(ex.Message, , "not current")
    '        '        Return False
    '        '    End Try

    '        '

    '    End Function

    ''================== TEST READ FILE =============
    Public Sub ReadExcelDoc()

        '    Dim workbook As New Excel.Workbook
        '    Try
        '        workbook.loadfromfile("\\ICCNAS1\Users\Shared\LinkedFiles\Events\commsswspring16.xlsm")
        '    Catch ex As Exception
        '        MsgBox(ex.Message, , "error  ")
        '    End Try
        '    MsgBox(workbook.Worksheets(0).cells(2, 2).value.ToString)
        '    workbook = Nothing
        '    '--------------------

        ''========'supposed to read each line==========='Encoding.ASCII)
        'Dim files = From chkFile In Directory.EnumerateFiles("\\ICCNAS1\Users\Shared\LinkedFiles\Events\", "commsswspring16*", SearchOption.TopDirectoryOnly)
        'From line In File.ReadLines(chkFile, Encoding.UTF8)
        'Where line.Contains("Include")
        '' Select New With {.curFile = chkFile, .curLine = line}()
        'Try
        '    For Each f As String In files
        '        MsgBox(f(1), , f(0))
        '    Next
        'Catch ex As Exception
        '    MsgBox(ex.Message, , "error  ")
        'End Try
        '' MsgBox(files.Count.ToString())

        'MsgBox("done")
        ' ''----------------------------------------
        ''===== read lines and IF instead of where ============
        'Try '- still can't read excel characters
        '    Dim txtFiles = Directory.EnumerateFiles("\\ICCNAS1\Users\Shared\LinkedFiles\Events\", "commsswspring16*", SearchOption.AllDirectories)

        '    For Each currentFile As String In txtFiles
        '        Dim fileName = currentFile.Substring("\\ICCNAS1\Users\Shared\LinkedFiles\Events\".Length)
        '        MsgBox(fileName)

        '        For Each line As String In File.ReadLines(currentFile)
        '            '     If line.Contains("Include") Then 'And line.Contains("2006") Then
        '            MsgBox(line)
        '            'End If
        '        Next line
        '    Next
        'Catch e As Exception
        '    MsgBox(e.Message)
        '    '  Console.WriteLine(e.Message)
        'End Try

        'MsgBox("done")
        ''=------------------------
        '=======works but no WHERE==========
        'Try 'works but no WHERE
        '    Dim txtFiles = Directory.EnumerateFiles("\\ICCNAS1\Users\Shared\LinkedFiles\Events\", "commsswspring16*", SearchOption.AllDirectories)

        '    For Each currentFile As String In txtFiles
        '        Dim fileName = currentFile.Substring("\\ICCNAS1\Users\Shared\LinkedFiles\Events\".Length)
        '        MsgBox(fileName)
        '    Next
        'Catch e As Exception
        '    MsgBox(e.Message)
        '    '  Console.WriteLine(e.Message)
        'End Try
        'MsgBox("done")
        '       '=========works but only seems to find WHERE in title? first cell?==================
        '       Try 'works but only seems to find WHERE in title? first cell?

        '           Dim files = From file In _
        'Directory.EnumerateFiles("\\ICCNAS1\Users\Shared\LinkedFiles\Events\", "commsswspring16*", SearchOption.TopDirectoryOnly)
        '           Where file.Contains("role") 'SW") ' & Environment.NewLine & "2/4/2016")
        '           'MsgBox(files.current())
        '           'files.movenext()
        '           'MsgBox(files.current())
        '           ' Show results.
        '           For Each x As String In files
        '               ' Console.WriteLine("{0}", File)
        '               MsgBox(x.ToString)
        '           Next
        '       Catch ex As Exception
        '       End Try

        '       '----------

        '  ' LINQ query for all files containing the word 'Europe'.
        '  Dim files = From chkFile In Directory.EnumerateFiles("\\ICCNAS1\Users\Shared\LinkedFiles\Events\", "commsswspring16*", SearchOption.TopDirectoryOnly)
        'From line In File.ReadLines(chkFile)
        'Where line.Contains("SW")
        '  ' Select New With {.curFile = chkFile, .curLine = line}()

        '  For Each f In files
        '      ' MsgBox(curfile.tostring, , Line.ToString)
        '      ' Console.WriteLine("{0}\t{1}", f.curFile, f.curLine)
        '  Next

        'Dim files = From chkFile In Directory.EnumerateFiles("\\ICCNAS1\Users\Shared\LinkedFiles\Events\", "commsswspring16*", SearchOption.TopDirectoryOnly)
        'From line In File.ReadLines(chkFile)
        'If str.Contains("SW") Then
        '    MsgBox("found SW")
        'Else
        '    MsgBox("not found")
        'End If
        ' End If

        'Where(File.ToLower().Contains("SW"))

        'For Each File In files
        '    MsgBox(File)
        '    ' Console.WriteLine("{0}", File)
        'Next
        '  MsgBox(files.count.ToString)
        '   Console.WriteLine("{0} files found.", files.Count.ToString())
        'Catch UAEx As UnauthorizedAccessException
        '    ' Console.WriteLine(UAEx.Message)
        'Catch PathEx As PathTooLongException
        '    ' Console.WriteLine(PathEx.Message)
        'End Try



        ''=====TEST copy file to csv and read it then delete

        'Try 'not readable file
        '    'ruk
        '    'File.WriteAllText("\\ICCNAS1\Users\Shared\LinkedFiles\Events\anothertest.txt", File.ReadAllText("\\ICCNAS1\Users\Shared\LinkedFiles\Events\anothertest.xlsx"))
        '    'file.Copy("\\ICCNAS1\Users\Shared\LinkedFiles\Events\anothertest.xlsx", "\\ICCNAS1\Users\Shared\LinkedFiles\Events\anothertest.txt")
        'Catch ex As Exception
        '    MsgBox(ex.Message, , "error  copying file")
        'End Try


        'Try           ' Create an instance of StreamReader to read from a file.
        '    Dim sr As StreamReader = New StreamReader("\\ICCNAS1\Users\Shared\LinkedFiles\Events\anothertest.txt") 'works for .txt and .csv
        '    Dim line As String
        '    For i As Integer = 1 To 5
        '        line = sr.ReadLine()
        '        MsgBox(line)
        '    Next i
        '    sr.Close()
        'Catch E As Exception
        '    ' Let the user know what went wrong.
        '    MsgBox(E.Message, , "error reading ") '& strName)
        '    '  Console.WriteLine("The file could not be read:")
        '    '  Console.WriteLine(E.Message)
        'End Try
        'Try
        '    '      File.Delete("\\ICCNAS1\Users\Shared\LinkedFiles\Events\anothertest.csv")
        'Catch ex As Exception
        '    MsgBox(ex.Message, , "error  deleting file")
        'End Try


        '------ end test copy


        '    '============== TEST OLE DB ===================
        '    ' "Provider=Microsoft.Jet.OLEDB.4.0;" & _ not on 64 bit machines
        '    oleCnctn.ConnectionString = "Provider= Microsoft.ACE.OLEDB.12.0;" & _
        '"Data Source=" & strName & _
        '"; Extended Properties=""Excel 8.0"";"
        '    MsgBox(oleCnctn.ConnectionString)
        '    '---- READER not registred on local machine 32 bit vs 64 bit + install access enging
        '    Using oleCnctn 'As New OleDbConnection(connectionString)
        '        Dim command As New OleDb.OleDbCommand("SELECT * FROM Checklist", oleCnctn)
        '        ' Sheet1 in older files

        '        oleCnctn.Open()

        '        oleAdapter = command.ExecuteReader()
        '        For i As Integer = 1 To 3
        '            oleAdapter.Read()
        '            MsgBox(oleAdapter(0).ToString())
        '            '  While reader.Read()
        '            ' Console.WriteLine(oleAdapter(0).ToString())
        '            'End While
        '        Next i


        '        oleAdapter.Close()
        '        oleCnctn.Close()
        '    End Using
        '    '---------------------------

        ''------------------
        'Try           ' Create an instance of StreamReader to read from a file.
        '    Dim sr As StreamReader = New StreamReader(strName) 'works for .txt and .csv
        '    Dim line As String
        '    ' Read and display the lines from the file until the end  of the file is reached.
        '    ' Do
        '    line = sr.ReadLine()

        '    ' Console.WriteLine(line)
        '    MsgBox(line)
        '    '   Loop Until line Is Nothing
        '    sr.Close()
        'Catch E As Exception
        '    ' Let the user know what went wrong.
        '    MsgBox(E.Message, , "error reading " & strName)
        '    '  Console.WriteLine("The file could not be read:")
        '    '  Console.WriteLine(E.Message)
        'End Try
    End Sub
    '====================================== end test read file ===========

    Public Function isFileOpen(ByVal PathFileExt As String) As Boolean
        Dim s2 As FileStream

        '  Dim fInfo As New FileInfo(PathFileExt)
        '  Return fInfo.IsReadOnly          
        '  My.Computer.FileSystem.GetFileInfo(OpenMode.
        Try
            s2 = File.Open(PathFileExt, FileMode.Open, FileAccess.Write, FileShare.None)
            s2.Close()
            '   System.IO.File.Open(PathFileExt, IO.FileMode.Open, IO.FileAccess.Write, IO.FileShare.None)
            '   FileClose(1) 'does't release file in time
            Return False 'able to open, so is not already open
            'TODO find who has file open for message to user
        Catch ex As Exception
            Return True 'can't open so must be open already
        End Try
        ' FileOpen is deprecated see My.Computer.FileSystem Object.
    End Function

    'find ~ file owner
    Public Function isDocLocked(ByVal strPathFileExt As String) As String 'returns user name
        Dim strOwner As String
        Dim drw As DataRow()
        'returns last person who saved
        Try
            '   strOwner = My.Computer.FileSystem.GetDirectoryInfo(Dir(strPathFileExt)).GetAccessControl.GetOwner(GetType(Security.Principal.NTAccount)).Value
            strOwner = My.Computer.FileSystem.GetDirectoryInfo(strPathFileExt).GetAccessControl.GetOwner(GetType(Security.Principal.NTAccount)).Value
            drw = tblStaff.Select("Logonname = '" & strOwner.Substring(4) & "'") 'cfc\ sometimes = builtin\administrators
            Return IsNull(drw(0)("StaffFirstnameFirst"), "?")
        Catch ex As System.IO.DirectoryNotFoundException
            Return "not found"
        Catch ex2 As Exception
            Return strOwner
        End Try



        '================
        'Dim finfo As System.IO.FileInfo'ROSE: ~$plannedgivingFeb2015sw.xlsm
        'finfo = New System.IO.FileInfo(LinkedPath & "Events\GreeneCountyInfoSW.xlsx")
        'dummyAccount = System.Security.Principal.NTAccount('dummy')'GreeneCountyInfoSW.xlsx
        'Owner = char(finfo.GetAccessControl.GetOwner(GetType(dummyAccount)).Value.ToString)'GroupEquippingCentral1516.xlsm
        '================


    End Function




    ' CREATE EVENT HOST SPREADSHEET from TEMPLATE with 2 text fields
    Public Function CreateSpreadsheet(ByVal strTemplate As String, ByVal strFullName As String, ByVal strText1 As String, ByVal strText2 As String) As Boolean
        '-- USED BY Event Host Overview
        '-- one dot rule
        '-- dismiss the excel object - let calling routing re-open using file io if necessary for user
        Dim objExcel As New Excel.Application
        '  Dim objWorkbook As Excel.Workbooks
        Dim wkbkTemplate As Excel.Workbook
        Dim objSheet As Excel.Worksheet

        Try
            wkbkTemplate = objExcel.Workbooks.Add(strTemplate)
            objSheet = wkbkTemplate.Worksheets(1)

            objSheet.Range("B2").Value = strText1
            objSheet.Range("B6").Value = strText2

            wkbkTemplate.SaveAs(strFullName, FileFormat:=52)    'xlsm = 52 ; 'xls = 56 (Excel8); excel5 = 39; 

            CreateSpreadsheet = True
        Catch ex As Exception
            modGlobalVar.msg("ERROR: creating excel object", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
            CreateSpreadsheet = False
        Finally
Release:
            Try
                wkbkTemplate.Close()
                '  objExcel.Quit()
                '   ReleaseComObject(r)
                ReleaseComObject(objSheet)
                ReleaseComObject(wkbkTemplate)
                '  ReleaseComObject(objbooks)
                ReleaseComObject(objExcel)

                ''try without gc.collect first
                ''GC.Collect()
                ''GC.WaitForPendingFinalizers()
                ''GC.Collect()
                ''GC.WaitForPendingFinalizers()
                ''objExcel.Quit() - don't close workbook or quit application - leave open for user
                ''--YES close and quit, then re-open new document using file IO for cleaner memory
                '' objExcel.UserControl = True
                'ReleaseComObject(objSheet)
                'wkbkTemplate.Close()
                'ReleaseComObject(wkbkTemplate)
                'objExcel.Quit()
                'ReleaseComObject(objExcel)
                ''not really necessary but not harmful
                'objSheet = Nothing
                'wkbkTemplate = Nothing
                'objExcel = Nothing
            Catch ex As Exception
                modGlobalVar.msg("ERROR closing excel object 2", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Try
    End Function

    'MENU ITEM TEXT
    Public Sub SetMenuItem(ByVal strWhat As String, ByRef mi As MenuItem, ByRef btn As Button, ByVal strFullName As String)
        '-- CALLED BY VARIOUS FORMS ON OPEN: event, org
        MouseWait()
        ' modGlobalVar.Msg(strFullName, , mi.Name)
ChangeIfFindFile:
        If My.Computer.FileSystem.FileExists(strFullName) Then
            'If File.Exists(strFullName) Then
            mi.Text = strWhat & " - Open"
            If btn Is Nothing Then
            Else
                btn.Text = strWhat & " - Open"
            End If
        Else
ResetDefault:
            mi.Text = strWhat & " - Create"
            If btn Is Nothing Then
            Else
                btn.Text = strWhat & " - Create"
            End If
        End If
        MouseDefault()
    End Sub

    '    'find spreadsheet, check and for and call add regional column
    '    Public Function FindMasterChecklist(ByVal strDoc As String, ByVal strRegion As String, dt As DateTime) As String ', ByVal bInsertRegion As Boolean) As String
    '        'does doc exist AND
    '        'does region and date column exist
    '        'called by Event Search adn Detail forms for visibility of menu items and buttons
    '        'strdoc has no extension, strexists has extension
    '        Dim strExists As String 'found document name, no path
    '        strExists = DoesDocExist("\\ICCNAS1\Users\Shared\LinkedFiles\Events\", strDoc)

    '        Select Case strExists
    '            Case Is = "No"
    '                FindMasterChecklist = "ChecklistCreate"
    '            Case Is = "Error"    'TODO what is correct option here?
    '                ' SetMasterChecklistVisibility("Create")
    '                FindMasterChecklist = "ChecklistError"
    '            Case Else 'have doc name, check for region/date column
    '                If IsRegionDateCol(strExists(1), strRegion, dt) = True Then
    '                    FindMasterChecklist = "ChecklistHasRegion"
    '                Else
    '                    FindMasterChecklist = "ChecklistInsertRegion"
    '                End If

    '        End Select
    '        Exit Function

    '        ''--- cg ---
    '        'Dim objExcel As New Excel.Application
    '        ''Dim objTemplate As Excel.Workbook
    '        'Dim objSheet As Excel.Worksheet
    '        'Dim oBooks As Excel.Workbooks 'to avoid double dot notation
    '        'oBooks = objExcel.Workbooks
    '        'Dim objWorkbook As Excel.Workbook
    '        'Dim r As Excel.Range
    '        'Dim bFound As Boolean = False
    '        'Dim arFileNames As String() 'to find document regardless of .extension


    '        ''SEE IF FILE EXISTS
    '        'Try
    '        '    arFileNames = Directory.GetFiles("\\ICCNAS1\Users\Shared\LinkedFiles\Events\", strDoc & ".*", SearchOption.TopDirectoryOnly) '".xls"  '& "*"
    '        '    If arFileNames.Length = 0 Then ' = String.Empty Then
    '        '        FindMasterChecklist = "Checklist: NotFound"
    '        '        GoTo closeall
    '        '    Else
    '        '        ' arTempNames = Directory.GetFiles("\\ICCNAS1\Users\Shared\LinkedFiles\Events\", "~$" & strDoc & ".*", SearchOption.TopDirectoryOnly) '".xls"  '& "*"
    '        '    End If
    '        'Catch ex As Exception
    '        '    FindMasterChecklist = "Checklist: error"
    '        '    GoTo closeall
    '        'End Try

    '        ''SEE IF FILE CONTAINS A COLUMN FOR THIS REGION
    '        'objWorkbook = oBooks.Open(strDoc)
    '        'objSheet = objWorkbook.Worksheets(1)
    '        'Dim strFind As String
    '        'r = objSheet.Range("C2:M2")
    '        'For Each c As Excel.Range In r.Cells
    '        '    If c.Value2 > " " Then
    '        '        strFind = strRegion& NextLine &  dt
    '        '        If Replace(c.Value2, " ", "").ToString = Replace(strRegion& NextLine &  dt, " ", "").ToString Then 'value2
    '        '            'If c.Value2 = strFind Then
    '        '            bFound = True
    '        '            '  modGlobalVar.Msg("This Master Checklist already contains this event", modGlobalVar.MsgStyle.DefaultButton1 Or MessageBoxIcon.Information, "FILE EXISTS - Cancelling Request")
    '        '            ' modGlobalVar.Msg("value: " & c.Value& NextLine &  "Value2 trim: " & Replace(c.Value2, " ", "")& NextLine &  "FE: " & Replace(strRegion& NextLine &  dt, " ", ""), , "Matched")
    '        '            Exit For
    '        '        Else
    '        '            '         modGlobalVar.Msg("value: "& NextLine &  c.Value& NextLine &  "Value2 trim: "& NextLine &  Replace(c.Value2, " ", "")& NextLine &  "FE: "& NextLine &  strFind, , "NOT Matched")
    '        '        End If
    '        '    Else
    '        '        '      modGlobalVar.Msg(Replace(strRegion& NextLine &  dt, " ", ""), , "out of filled range")
    '        '        Exit For
    '        '    End If
    '        'Next c
    '        ''TEST GET SUER: ERROR CANNOT ACCESS READ ONLY DOC
    '        'Dim users() As VariantType = objWorkbook.UserStatus
    '        'modGlobalVar.Msg(users(0), , "First user")
    '        'Exit Function
    '        '' ===== TESTING RETURNS AUTHOR?? NOT ACTIVE USER?? ===================
    '        'arFileNames = Directory.GetFiles("\\ICCNAS1\Users\Shared\LinkedFiles\Events\", "~$" & strDoc & ".*", SearchOption.TopDirectoryOnly) '".xls"  '& "*"

    '        ''isFileOpen(arFileNames(0))
    '        ''Exit Function


    '        'If bFound = True Then
    '        '    FindMasterChecklist = "Checklist: RegionMatched"
    '        '    GoTo ReleaseExcel
    '        'Else
    '        '    FindMasterChecklist = "Checklist: RegionMissing"
    '        'End If

    '        '============================= insert column =====================================
    '        ADD COLUMN FOR THIS REGION DEPENDING ON CALLING FORM
    'InsertRegion:
    '        If bInsertRegion = False Then
    '            FindMasterChecklist = "RegionNotRequired"
    '            GoTo ReleaseExcel
    '        Else
    '            'IS SPREADSHEET ALREADY OPEN?
    '            ' Try '~$ doesnt work anymore
    '            ' arFileNames = Directory.GetFiles("\\ICCNAS1\Users\Shared\LinkedFiles\Events\~$", strDoc & ".*", SearchOption.TopDirectoryOnly) '".xls"  '& "*"
    '            '  Dim fInfo As New FileInfo(arFileNames(0))
    '            ' Return the IsReadOnly property value.
    '            'If fInfo.IsReadOnly Then
    '            '  If arFileNames.Length = 0 Then
    '            'Dim strOwner As String = File.GetAccessControl(arFileNames(0)).GetOwner(GetType(Security.Principal.NTAccount)).Value()
    '            'If strOwner = "CFC\" & usrLogon Then
    '            ' modGlobalVar.Msg("Close spreadsheet and try again.", , "Do you have Checklist open?")
    '            ' Else
    '            ' modGlobalVar.Msg("This file may be Read Only until closed by " & strOwner & ". ", MessageBoxIcon.Information Or modGlobalVar.MsgStyle.modGlobalVar.MsgSetForeground, "Note: This file is already in use. ")
    '            ' End If

    '            If isFileOpen(arFileNames(0)) = True Then
    '                GoTo ReleaseExcel
    '            Else 'is not open; go ahead and add column
    '                GoTo AddColumn
    '            End If
    '        End If 'insertRegion boolean

    '        If File.Exists("\\ICCNAS1\Users\Shared\LinkedFiles\Events\" & "~$" & strDoc & ".xlsm") Then
    '            File.Open("\\ICCNAS1\Users\Shared\LinkedFiles\Events\" & "~$" & strDoc & ".xlsm", FileMode.Open)
    '            strOwner = File.GetAccessControl("\\ICCNAS1\Users\Shared\LinkedFiles\Events\" & "~$" & strDoc & ".xlsm").GetOwner(GetType(Security.Principal.NTAccount)).Value()
    '            If strOwner = "CFC\" & usrLogon Then
    '                ' GoTo AddColumn
    '                modGlobalVar.Msg("Close spreadsheet and try again.", , "Do you have Checklist open?")
    '            Else
    '                modGlobalVar.Msg("This file may be Read Only until closed by " & strOwner & ". ", MessageBoxIcon.Information Or modGlobalVar.MsgStyle.modGlobalVar.MsgSetForeground, "Note: This file is already in use. ")
    '                ' drw = dtStaff.Select("Logonname = '" & strOwner.Substring(4) & "'")
    '                'modGlobalVar.Msg("This file is Read Only until "& NextLine &  IsNull(UCase(drw(0)("StaffFirstnameFirst")), "?")& NextLine &  " closes it.  You will be notified when the file is available for editing." & NextLine _
    '                '                     & "(File name: '" + strPath & strFile + "'.", MessageBoxIcon.Information Or modGlobalVar.MsgStyle.modGlobalVar.MsgSetForeground, "Note: This file is already in use. ")
    '                'drw = Nothing
    '                'End If
    '                ' Else
    '            End If
    '            FindMasterChecklist = "NotRegion"
    '            GoTo closeall
    '        Else 'does is not open
    '        End If
    '        -- in c:  string user_name = System.IO.File.GetAccessControl(e.FullPath).GetOwner(typeof(System.Security.Principal.NTAccount)).ToString();

    'AddColumn:
    '        Try
    '            ReleaseComObject(r)
    '            ReleaseComObject(objSheet)
    '            If MasterChecklistInsertRegion(objWorkbook, strRegion, dt) = True Then
    '                FindMasterChecklist = "RegionInserted"
    '            Else
    '                FindMasterChecklist = "InsertRegionError"
    '            End If
    '        Catch ex As System.Exception
    '            FindMasterChecklist = "ExcelError"
    '        End Try
    '        '===================================== end Insert column =================================
    '        'ReleaseExcel:
    '        '        Try
    '        ''            r = Nothing
    '        '            objWorkbook.Close()
    '        '            objExcel.Quit()
    '        '            ReleaseComObject(objWorkbook)
    '        '            ReleaseComObject(oBooks)
    '        '            ReleaseComObject(objExcel)
    '        '        Catch ex As Exception
    '        '            modGlobalVar.Msg(ex.Message, , "error closing excel")
    '        '        Finally
    '        '        End Try

    '        'CLoseAll:
    '        '        strOwner = Nothing
    '        '        arFileNames = Nothing

    '    End Function

    '    'INSERT REGION COLUMN IN MASTER SPREADSHEET
    '    Public Function MasterChecklistInsertRegion(ByRef wrkbk As Excel.Workbook, ByVal strRegion As String, dt As DateTime) As Boolean
    '        'called by FindMasterChecklist, btnCreateChecklist
    '        Dim Sheet As Excel.Worksheet = wrkbk.Worksheets(1)
    '        Dim r As Excel.Range
    '        Dim iCRtn, InsertHere As Int16

    '        On Error GoTo failed

    '        'INSERT COLUMN IN DATE SEQUENCE
    '        'straight insert locks cells; instead, find first blank column, copy and insert in proper place
    '        'at same time segment out date based on NextLine
    '        Sheet.Unprotect("event")
    '        r = Sheet.Range("C2:M2")
    'FindFirstBlankColumn:
    '        For Each c As Excel.Range In r.Cells
    '            If c.Value = String.Empty Then
    '                c.EntireColumn.Copy()
    '                Exit For
    '            End If
    '        Next c

    'CompareDates:
    '        For Each c2 As Excel.Range In r.Cells
    '            iCRtn = InStr(c2.Value2, NextLine)
    '            '   modGlobalVar.Msg(c2.Value.substring(iCRtn).ToString& NextLine &  c2.Value2.substring(iCRtn).ToString, , iCRtn.ToString)
    '            If (iCRtn = 0) Then
    '                InsertHere = c2.Column
    '            Else
    '                If CType(c2.Value2.substring(iCRtn), DateTime) > CType(dt, DateTime) Then
    '                    InsertHere = c2.Column
    '                End If
    '            End If

    '            If InsertHere > 0 Then
    '                ' Try
    '                c2.EntireColumn.Insert()
    '                Sheet.Cells(1, InsertHere).value = "Include date and your initials."
    '                Sheet.Cells(2, InsertHere).value = Replace(strRegion& NextLine &  dt, " ", "")
    '                Exit For
    '                'Catch ex As Exception
    '                'modGlobalVar.Msg(ex.Message, , "failed insert column")
    '                'Exit For
    '                'Finally
    '                'End Try
    '            End If
    '        Next c2
    '        Sheet.Protect("event")
    '        wrkbk.Save()
    '        ReleaseComObject(r)
    '        ReleaseComObject(Sheet)

    '        Return True
    '        Exit Function
    'failed:
    '        modGlobalVar.Msg(Err.Description)
    '        ReleaseComObject(r)
    '        ReleaseComObject(Sheet)
    '        Return False
    '    End Function

    'Function isFileOpen(sFileName As String) As Boolean

    '    '  Dim iFileNum As Integer, lErrNum As Long

    '    '  On Error Resume Next
    '    '  iFileNum = FreeFile()
    '    'Attempt to open the file and lock it.
    '    Try
    '        My.Computer.FileSystem.OpenTextFileWriter(sFileName, True)
    '    Catch ex As Exception
    '        modGlobalVar.Msg(ex.ToString, , "try block error")
    '    End Try
    '    'FileOpen(sFileName, FileMode.Open, FileAccess.Write, FileShare.None)  'For Input Lock Read As #iFileNum
    '    '   My.Computer.FileSystem.GetFileInfo(sFileName)
    '    '  Dim f As FileInfo = My.Computer.FileSystem.GetFileInfo(sFileName)
    '    'isreadonly is never true
    '    ' f.OpenWrite()

    '    ' lErrNum = Err.Number
    '    '  On Error GoTo 0

    '    ''Check to see which error occurred.
    '    'Select Case Err.Number 'lErrNum
    '    '    Case 0
    '    '        'No error occurred.
    '    '        'File is NOT already open by another user.
    '    '        isFileOpen = False

    '    '    Case 55, 70
    '    '        'Error number for "Permission Denied."
    '    '        'File is already opened by another user.
    '    '        isFileOpen = True
    '    '        'display OWNER
    '    '        Dim strOwner As String = File.GetAccessControl(sFileName).GetOwner(GetType(Security.Principal.NTAccount)).Value()
    '    '        If strOwner = "CFC\" & usrLogon Then
    '    '            modGlobalVar.Msg("Close spreadsheet and try again.", , "Do you have Checklist open? A")
    '    '        Else
    '    '            modGlobalVar.Msg("This file may be Read Only until closed by " & strOwner & ". ", MessageBoxIcon.Information Or modGlobalVar.MsgStyle.modGlobalVar.MsgSetForeground, "Note: This file is already in use. ")
    '    '        End If
    '    '    Case 53, 5
    '    '        'File not found
    '    '        isFileOpen = False
    '    '    Case 57 'opened by self??
    '    '        'display OWNER
    '    '        Dim strOwner As String = File.GetAccessControl(sFileName).GetOwner(GetType(Security.Principal.NTAccount)).Value()
    '    '        If strOwner = "CFC\" & usrLogon Then
    '    '            modGlobalVar.Msg("Close spreadsheet and try again.", , "Do you have Checklist open? B")
    '    '        Else
    '    '            modGlobalVar.Msg("This file may be Read Only until closed by " & strOwner & ". ", MessageBoxIcon.Information Or modGlobalVar.MsgStyle.modGlobalVar.MsgSetForeground, "Note: This file is already in use. ")
    '    '        End If
    '    '        isFileOpen = True
    '    '    Case 58 'exists?? is not an error so is never caught here
    '    '        isFileOpen = False
    '    '    Case Else
    '    '        'Another error occurred.
    '    '        isFileOpen = True
    '    '        modGlobalVar.Msg(ErrNum.ToString& NextLine &  Err.Description, , "error checking IsFileOpen")
    '    'End Select
    '    FileClose()

    '    '   modGlobalVar.Msg(ErrNum.ToString & " " & Err.Description, , "error checking IsFileOpen")
    '    ' modGlobalVar.Msg(f.GetAccessControl.GetOwner(GetType(Security.Principal.NTAccount)).Value& NextLine &  f.IsReadOnly.ToString, , "GetFIleINfo")
    '    '  f = Nothing
    '    'End Select
    '    'Dim iFileNum As Integer, lErrNum As Long
    '    ''note does not work for .txt files
    '    '' lErrNum = 0
    '    '' On Error Resume Next
    '    'modGlobalVar.Msg("in file open")
    '    ''Attempt to open the file and lock it.




    '    ' Try
    '    '  File.OpenWrite(sFileName)
    '    '   FileClose(sFileName)
    '    ' Microsoft.VisualBasic.FileIO.FileSystem.GetFileInfo(
    '    ' Catch ex As IOException
    '    ' modGlobalVar.Msg(ex.Message & NextLine &  ex.HelpLink, , "is file open")
    '    ' End Try
    '    '  lErrNum = Err.Number
    '    ' On Error GoTo 0
    '    '  isFileOpen(Err.Number)

    '    '  modGlobalVar.Msg("done")
    '    ''Check to see which error occurred.
    '    '
    'End Function


#End Region 'Checklist Overview

End Module


''CHECK IF DOCUMENT IS LOCKED(File) - GENERIC - RETURNS UserName
'Public Function IsDocLocked(ByVal strPath As String, ByVal strDocName As String) As String
'    'path "\\ICCNAS1\Users\Shared\LinkedFiles\Events\"
'    'Note strDocName needs to return full extension else wildcard extension could return different document
'    'Note2 only returns first lock
'    'RETURNS NotLocked, or Username
'    '--2/16 temp file is now in users roaming profile, not shared drive??
'    Dim arTempNames As String() 'to find open documents

'    'arTempNames = Directory.GetFiles(strPath, "~$" & strDocName & ".*", SearchOption.TopDirectoryOnly)
'    Try
'        arTempNames = Directory.EnumerateFiles(strPath, "~$" & strDocName & ".*", SearchOption.TopDirectoryOnly)
'    Catch ex As Exception
'        MsgBox(ex.Message, , "error getting array ")
'    End Try
'    'Dim workDirs As List(Of String) = New List(Of String)(dirs)

'    'Dim files = From chkFile In Directory.EnumerateFiles("c:\", "*.txt", SearchOption.AllDirectories)
'    '            From line In File.ReadLines(chkFile)
'    '            Where line.Contains("Microsoft")
'    '            Select New With {.curFile = chkFile, .curLine = line}

'    '        ---------------------------
'    'error getting array 
'    '---------------------------
'    'Unable to cast object of type 'System.IO.FileSystemEnumerableIterator`1[System.String]' to type 'System.String[]'.
'    '---------------------------
'    '        OK()
'    '---------------------------


'    If arTempNames.Length = 0 Then
'        Return "NotLocked"
'    Else
'        Dim strOwner As String
'        Dim drw As DataRow()

'        strOwner = My.Computer.FileSystem.GetDirectoryInfo(arTempNames(0)).GetAccessControl.GetOwner(GetType(Security.Principal.NTAccount)).Value
'        drw = dtStaff.Select("Logonname = '" & strOwner.Substring(4) & "'")
'        Return IsNull(UCase(drw(0)("StaffFirstnameFirst")), "?")

'        '  For x As Integer = 1 To arTempNames.Length
'        ' modGlobalVar.Msg(arTempNames(x - 1).ToString, , x.ToString)
'        '   modGlobalVar.Msg(My.Computer.FileSystem.GetDirectoryInfo(arTempNames(0)).GetAccessControl.GetOwner(GetType(Security.Principal.NTAccount)).Value, , "Owner")
'        'replaced by newer MS recommendation:  Dim owner As NTAccount = _ My.Computer.FileSystem.GetDirectoryInfo("c:\resized").GetAccessControl.GetOwner(GetType(NTAccount))
'        '   Next
'    End If
'    ' modGlobalVar.Msg("", , "out of IsLocked")

'End Function

''LIST ATTACHED FILES - used by Duplicates form -- not used??
'Public Function ListFoundFiles(ByVal strWhat As String, ByVal strID As String) As String
'    '-- 8/13 cg -- TODO UNTESTED since system down
'    '' TODO QUESTION - why is this only called by cases?
'    Dim dirs As String()
'    Dim strbFilesFound As New StringBuilder

'    Select Case strWhat
'        Case strWhat.Contains("Cases")
'            '  dirs = Directory.GetFiles(LinkedFilePath & "Cases\", strID & "*")
'            dirs = Directory.EnumerateFiles(LinkedPath & "Cases\", strID & "*", SearchOption.TopDirectoryOnly)
'            If dirs.Length > 0 Then
'                strbFilesFound.Append("Case Files: " & dirs.Length.ToString)
'            End If
'        Case strWhat.Contains("Resources")
'            '   dirs = Directory.GetFiles(LinkedFilePath & "Resources\", strID.ToString & "*")
'            dirs = Directory.EnumerateFiles(LinkedPath & "Resources\", strID.ToString & "*", SearchOption.TopDirectoryOnly)
'            If dirs.Length > 0 Then
'                strbFilesFound.Append("Resource Files: " & dirs.Length.ToString)
'            End If
'        Case strWhat.Contains("Organizations")
'            ' dirs = Directory.GetFiles(LinkedFilePath & "Organizations\*", strID.ToString & "*")
'            dirs = Directory.EnumerateFiles(LinkedPath & "Organizations\*", strID.ToString & "*", SearchOption.TopDirectoryOnly)
'            If dirs.Length > 0 Then
'                strbFilesFound.Append("Organization Files: " & dirs.Length.ToString)
'            End If

'    End Select
'    ListFoundFiles = strbFilesFound.ToString

'End Function
