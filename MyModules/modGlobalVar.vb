Option Explicit On
'======================= FORM SETUP ===========================================
'GLOBAL VARIABLES, OPEN FORMS, LOAD COMBO BOXES, VALIDATION, STRING FORMATTING
'to retrieve name: [Enum].GetName(GetType(EnumName), integer)
'==============================================================================
Imports System.Deployment.Application
Imports System.Reflection
Imports System.Data.SqlClient
Imports System.Text
Imports System.IO


Module modGlobalVar

#Region "Structures"
    '=============== CHANGE SERVER under View - Project - Settings =================
    'orig string: Public sqlCon As String = "Data Source=SOLOMON2008\Solomon2008;Initial Catalog=InfoCtr_be;Integrated Security=True;Connection Timeout = 320"
    'from Settings: Public sqlCon As String = Global.InfoCtr.My.MySettings.Default.InfoCtr_beConnectionString '"Data Source=SOLOMON;Initial Catalog=InfoCtr_bMy.MySettings.InfoCtr_beConnectionString.tostring
    Private ReadOnly gTitle As String = My.Application.Info.ProductName
    Public ReadOnly sc As New SqlClient.SqlConnection(Global.InfoCtr.My.MySettings.Default.InfoCtr_beConnectionString)
    Public ReadOnly DeployLocation As String = Global.InfoCtr.My.MySettings.Default.PublishLocation
    Public Const strMailClient As String = "centerforcongregations-org.mail.protection.outlook.com" 'formerly mail2.centerforcongregations.org" 
    Public ReadOnly UserPath As String = "\\ICCNAS1\Users\" & Environment.UserName & "\"
    Public Const SOPPath As String = "\\ICCNAS1\Users\Shared\Standard Operating Procedures\"
    Public Const SharedPath As String = "\\ICCNAS1\Users\Shared\"
    Public Const LinkedPath As String = "\\ICCNAS1\Users\Shared\LinkedFiles\"
    Public ReadOnly strEmailBody As String = "<b>App: </b>" + My.Application.Info.ProductName + "<br><b>Connection String: </b>" + sc.ConnectionString.ToString + "<br><b>Computer Name: </b> " + IsNull(Environ("ComputerName"), "na") + "<br><b>Error: </b>"
    Public Const AmazonTag As String = "/?tag=centerforcong-20" '10/15
    Public Const CenterEmail As String = "@CenterforCongregations.org"
    Public Const grdvwColName As String = "DataGridViewTextBoxColumn"

    'for edit/visiblity permissions and messages
    Structure structStaff
        Dim StaffName As String
        Dim StaffRole As String
        Dim StaffID As Integer
        Dim StaffEmail As String
        Public Sub New(ByVal Role, ByVal Name, ByVal ID, ByVal Email)
            StaffName = Name
            StaffRole = Role
            StaffID = ID
            StaffEmail = Email
        End Sub
    End Structure

    Structure structRegistration
        Dim ContactID As Integer
        Dim EventID As Integer
        Dim OrderID As Integer
        Dim regDate As Date
        Public Function InsertNewReg() As Integer

        End Function
    End Structure

    'for tab control counts and headings
    Structure structHeadings
        Dim SingleName As String
        Dim PluralName As String

        Public Sub New(ByVal DDMenuItem, ByVal GridHeading)
            SingleName = DDMenuItem
            PluralName = GridHeading 'tag is plural UC, so can use for count + heading
        End Sub
    End Structure

    Structure structPayment
        Dim PaymentID As Integer
        Dim OrderID As Integer
        Dim PaymentType As String 'payment,refund
        Dim PaymentSearch As String 'event, oid
        Dim EventID As Integer
        Dim ContactID As Integer
    End Structure

    'for close methods
    Structure structCloseMethod
        Public sh As Short
    End Structure

    'for various closing scenarios of structCloseMethod
    Enum ObjClose As Short
        unknown = 0
        miSave = 1
        miAskSave = 2 'first check for changes
        btnSaveExit = 3 'save close
        miDelete = 4 'save close
        CloseByControl = 5 'save close
        SaveClose = 6 'save close
        DontSaveClose = 21 'close  
        noChanges = 22 'close, check required?
        cancelClose = 23
        ReloadForm = 7

        'save close uses CheckRequired and may involve adding last change staff/date 
        'to retrieve name: [Enum].GetName(GetType(EnumName), integer)
    End Enum

    'for staff dropdown queries
    Enum StaffComboIDs As Short
        CenterMin = 200 '
        CenterMax = 899 'keep Center staff between 200 and 899
        AllStaff = 6001 '=   "- All Staff -"
        NoStaff = 6002 ' =  "- No Staff -"
        CMGIFeedback = 6005 '= "CMGI Feedback Form"
        Headings = 7000     '>7000 = headings
        Outsiders = 8000    ' >8000 = outsider testing: Steve Clark, Drupal, Developer Town
        Test = 900    'cgTest, Joe Smith
        Alban = 1000    '1001-1841
        'Selectable = 999
    End Enum

    'for staff dropdown selections
    Enum StaffComboChoices
        AllAndNo 'for search forms; include AllStaff & NoStaff; exlcude Alban, 'CMGI', 'Your Name Here'
        Historic 'include Alban, exclude All, No, 'CMGI', 'Your Name Here'. for Resource detail
        CMGI 'include 'CMGI', 'Your Name Here', Alban. 'exclude All, No;  used by Resource Feedback
        Full 'use for ??  include 'CMGI form, No Staff, Your Name Here, Alban, outsiders
        Selectable  'excl All No, Alban, CMGI
        'current staff only? what about testers doing dataentry? 'exclude cmgi, your name here
    End Enum

    'for msgbox shortcuts
    Enum MsgCodes
        filterEmpty
        filterApostrophe
        invalidSearch
        noResultCancel
        noRowSelected
        noResult
        InvalidSelection
        missingInformation
        'to retrieve name: [Enum].GetName(GetType(MsgCodes), msgCode)
    End Enum

    'USER RESPONSE
    Enum usrInput As Short
        Yes = 1
        No = 2
        Retry = 3
        Cancel = 4
        OK = 5
        Ignore = 6
        AddNew = 7
        Search = 8
        None = 0
        Overwrite = 10
        Reset = 11
    End Enum

    'for COMBO BOXES
    Enum CanAddNew As Short
        AddNew = 1
        Search = 2
        OKBlank = 3
        None = 0
    End Enum

#End Region 'structures'

#Region "Variables"
    Public strVersion As String
    Public strVersionDt As Date
    '=== STAFF PERMISSIONS / MESSAGES  ==============
    Public ITDir As structStaff 'add staff forms: Aaron
    Public ResourceAdmin As structStaff  'library labels: admins
    Public DBAdmin As structStaff   'error message, add staff forms: Catharine
    Public RegonlineAdmin As structStaff    'add/edit online event ID number
    Public CRGAdmin As structStaff 'resource CRG checkbox and annotation and goto person: Carol
    Public ResourceReviewer, ResourceDir As structStaff 'approve resources: ND, Karen G
    Public DefaultCaseMgr, GrantDir As structStaff  'new cases, grant questions: Kara
    Public GrantAdmin As structStaff    'generate due dates, email: Jerri
    Public EdDir As structStaff ': Matt
    Public PayPalAdmin As structStaff    'process refunds: Sofia
    Public usrLogon As String = Environment.UserName    'to lookup staffID
    Public usrRegion, usrName, usrType, usrJobTitle, usrExt As String  'staff satellite region; Lastname, Firstname;'why was usr a string?
    Public usr As Integer 'staff id
    Public usrFirst, usrRole, usrEmail As String 'staff FirstName LastName, Role for permissions
    Public StaffDuplCongr As New Microsoft.VisualBasic.Collection
    Public StaffDuplResource As New Microsoft.VisualBasic.Collection
    Public StaffCRGFull As New Microsoft.VisualBasic.Collection 'can flag for CRGwebsite and change status
    Public StaffCRGEdit As New Microsoft.VisualBasic.Collection 'can't flag on CRG but can edit Annotation/URL
    '== end Staff permissions ======

    Public Forms As New FormsCollection
    Public NextLine = Environment.NewLine
  
    'collections
    Public colRegionlu As New Microsoft.VisualBasic.Collection    'to load region combo boxes including "all regions" + Outside Indiana + show all
    Public colRegion5lu As New Microsoft.VisualBasic.Collection 'to load region combo boxes regions + "All Indiana"
    Public colRegionOffice As New Microsoft.VisualBasic.Collection 'to load region combo boxes  regions only
    Public colRegionMailing As New Microsoft.VisualBasic.Collection 'to include 'not in region' in other region mailings
    Public colCountylu As New Microsoft.VisualBasic.Collection    'to load combo box
    Public colCounty As New Microsoft.VisualBasic.Collection 'to answer region search
    Public colModelu As New Microsoft.VisualBasic.Collection  'mode of conversation combo
    Public colResourceType As New Microsoft.VisualBasic.Collection  'type of resource
    Public colResourceSubType As New Microsoft.VisualBasic.Collection 'subtype of resource
    Public arlResourceIndex As New ArrayList ' As New Microsoft.VisualBasic.Collection 'resource index terms
    Public colCRG As New Microsoft.VisualBasic.Collection 'for multiple combos on one page
    Public colStafflu As New Microsoft.VisualBasic.Collection   'staff names to display in update fields
    Public colEventSeriesID As New Microsoft.VisualBasic.Collection 'for multiple registrations on single click
    Public colBind As New Microsoft.VisualBasic.Collection
    Public colEventDocPref As New Collection    'open ed event documents w/out Y/N prefix
    'tables & datasets
    Public tblCenter As New DataTable("tCenter")
    Public dsStaff As New DataSet, tblStaff As New DataTable("luStaff")
    Public cntStaff As Integer 'count staff for grid column generation
    Public tblCRG As New DataTable("luCRG")
    Public tblResourceIndex As New DataTable("luResourceIndex")
    Public tblResourceType As New DataTable("luResourceType")
    Public tblLibRegion As New DataTable("luLibraryRegion")
    Public tblWEvents As New DataTable("luWEvents")  'Workshop/Events as per new web registration 2012
    Public dsStatus As New DataSet, tblOrderStatus As New DataTable("tblOrderStatus"), tblCaseStatus As New DataTable("tblCaseStatus"), tblCaseStatusSrch As New DataTable("tblCaseStatusSrch"), tblSpecialMail As New DataTable("tSpecialMail")
    Public tblResourceInactive As New DataTable("tblResourceInactive"), tblConversMode As New DataTable("tblConversMode")
    Public dsCounty As New DataSet
    Public dsluCombo As New DataSet, tblCases As New DataTable("lstCases") ', dtContacts As New DataTable("lstContactNames") 'to load case dd based on OrgID
    Public tblRegistrant As New DataTable("tRegistrant")
    Public tblRegOrder As New DataTable("tRegOrder")

    Public gRegion As String
    Public gCaseDialog As Boolean = False
    Public timer1 As New System.Windows.Forms.Timer
    Public cmdRegion As New SqlCommand("[GetRegion]", sc)
    Public dv As DataView
    Public bNewContact As Boolean = False
    Public glTime As Date = System.DateTime.Now
    Public iPayeeOrgNum As Integer 'online registration
    Public grRegion As String 'popup registrations
    Public gPayment As structPayment

    '- delivra email class 11/15 --
    Public tblSend As New DataTable("tableSendEmail")
    Public tblReject As New DataTable("tableRejectEmail")
    '-----

    Public strHelpGrid As String = "                            SEARCH GRID METHODS" & NextLine & _
         "CHANGE LIST: select criteria on the left using the drop down boxes.  " & NextLine & _
        "     The list will refresh when you tab from a dropdown box, OR click the Search button, OR use the menu: Search ->Begin Search.  " & NextLine & _
  "     To reset the criteria to the default settings use the menu: Search -> Clear Criteria. " & NextLine & _
       "FILTER: right-click in a cell to only show rows containing that text in that column.  " & NextLine & _
       "     To remove filter: right-click the blue grid header. " & NextLine & _
       "COPY: click in a cell then right-click the same cell and select Copy in the shortcut menu that appears." & NextLine & _
  "GO TO MAIN PAGE/EDIT: Arrow in left margin of grid indicates selected row.  " & NextLine & _
  "     Double-click the grid to go to the item’s main page OR Use the menu: Goto ->[select item]. " & NextLine & _
        "SCROLL:  use scroll bar, arrow keys, or mouse wheel. " & NextLine & _
        "SORT: click on column heading. " & NextLine & _
  "UN-SELECT ROW: click outside the grid." & NextLine & _
  "VIEW RELATED INFORMATION: Click the Show Detail checkbox below the grid to show another grid of related information if such is available. "

    'RESOURCE INDEX COMBOs
    Public tblKeyword1, tblKeyword2, tblKeyword3, tblKeyword4 As DataTable 'DataSet

#End Region

#Region "CURSOR SHAPES"
    Public Sub MouseWait() '(ByVal frm As Form)
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
    End Sub

    Public Sub MouseDefault() '(ByVal frm As Form)
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Public Sub MousePointer()
        Cursor.Current = System.Windows.Forms.Cursors.Arrow
    End Sub

    Public Sub MouseWeb()
        Cursor.Current = System.Windows.Forms.Cursors.Hand
    End Sub

#End Region 'mouseshapes

#Region "MessageBox"

    'if no params are provided, generates generic message
    Public Sub msg(ByVal msgCode As MsgCodes)
        msg(msgCode, Nothing)
    End Sub

    'Best Practise: avoid modGlobalVar.Msg; use messagebox.show instead = too much typing
    Public Function Msg(ByVal errType As MsgCodes, ByVal btn As System.Windows.Forms.MessageBoxButtons) As System.Windows.Forms.DialogResult
        Dim strCaption, strMsg As String
        Dim symbl As MessageBoxIcon

        Select Case errType
            Case Is = MsgCodes.filterEmpty
                strCaption = "Sorry..."
                strMsg = "you cannot filter on an empty value"
                symbl = MessageBoxIcon.Information
            Case Is = MsgCodes.filterApostrophe
                strCaption = "Sorry..."
                strMsg = "you can't filter on this item because it contains an apostrophe"
                symbl = MessageBoxIcon.Information
            Case Is = MsgCodes.invalidSearch
                strCaption = "ATTENTION: Invalid Search"
                strMsg = "The field you selected to search cannot accept the search text you entered." & NextLine & "Change your search text, or select a different field to search."
                symbl = MessageBoxIcon.Exclamation
            Case Is = MsgCodes.noResultCancel
                strCaption = "Cancelling Request"
                strMsg = "no results found."
                symbl = MessageBoxIcon.Information
            Case Is = MsgCodes.noResult
                strCaption = "Done"
                strMsg = "no results found."
                symbl = MessageBoxIcon.Information
            Case Is = MsgCodes.InvalidSelection
                strCaption = "ATTENTION: Invalid Selection"
                strMsg = "The item you selected is not valid." & NextLine & "It may be a legacy item. Choose an item currently in use."
                symbl = MessageBoxIcon.Exclamation
            Case Is = MsgCodes.noRowSelected
                strCaption = "ATTENTION: no row selected"
                strMsg = "Click the gray square on the left of a row to select the row."
                symbl = MessageBoxIcon.Exclamation
            Case Is = MsgCodes.missingInformation
                strCaption = "ATTENTION: missing information"
                strMsg = "Fill in more fields."
                symbl = MessageBoxIcon.Exclamation
            Case Else
                strCaption = "error"
                strMsg = "contact " & DBAdmin.StaffName
                symbl = MessageBoxIcon.Information

        End Select

        If btn = Nothing Then
            btn = MessageBoxButtons.OK
        End If
        Msg = MessageBox.Show(IsNull(strMsg, " ".PadLeft(30, " ")), strCaption, btn, symbl)


        ''Type of message footer
        'Select Case symbl
        '    Case Is = MessageBoxIcon.Exclamation
        '        strMsg += NextLine & NextLine & "--this is a you-can-do-something message --"
        '    Case Is = MessageBoxIcon.Information
        '        strMsg += NextLine & NextLine '& "-- this is an FYI message --"
        '    Case Is = MessageBoxIcon.Error
        '        strMsg += NextLine & NextLine '& "-- this is an error message, contact: " & DBAdmin.StaffName & "--"
        'End Select
        'buttons: 0 = ok; 1 = ok cancel;  3 = YesNoCancel; 4 = YesNo; 5= retry cancel
        'icons use: ERROR = notify IT, EXCLAMATION = do something different, INFORMATION = FYI, QUESTION
        'icons: error, stop, hand 16; asterisk, information 64; warning, exclamation 48; question = 32; none = 0

    End Function

    'Best Practise: avoid modGlobalVar.Msg; use messagebox.show instead = too much typing
    Public Function Msg(ByVal strCaption As String, ByVal strMsg As String, ByVal btn As System.Windows.Forms.MessageBoxButtons,
                   ByVal symbl As MessageBoxIcon) As System.Windows.Forms.DialogResult
        Msg = MessageBox.Show(strMsg, strCaption, btn, symbl)
    End Function

#End Region 'modGlobalVar.Msg

#Region " STRING FUNCTIONS"

    ' RETURNS NullSafeString 
    Public Function IsNull(ByVal arg As Object, ByVal returnIfEmpty As String) As String 'had overloads
        Dim returnValue As String

        If (arg Is DBNull.Value) OrElse (arg Is Nothing) OrElse (arg Is String.Empty) Then
            returnValue = returnIfEmpty
        Else
            Try
                returnValue = CStr(arg).Trim
            Catch
                returnValue = returnIfEmpty
            End Try
        End If
        Return returnValue
    End Function

    ' NULL integer
    Public Function IsNull(ByVal arg As Object, ByVal returnIfEmpty As Integer) As Integer
        Dim returnValue As Integer

        Try
            returnValue = CType(arg, Integer)
        Catch
            returnValue = returnIfEmpty
        End Try
        Return returnValue
    End Function

    'NULL date
    Public Function IsNull(ByVal arg As Object, ByVal returnIfEmpty As Date) As Date
        Dim returnValue As Date

        If (arg Is DBNull.Value) OrElse (arg Is Nothing) Then
            returnValue = returnIfEmpty
        Else
            Try
                returnValue = CType(arg, Date)
            Catch
                returnValue = returnIfEmpty
            End Try
        End If
        Return returnValue
    End Function

    'REPLACE * WITH SQL REQUIRED % so users can keep  using what is familiar  'and ADD FINAL %
    Public Function GetWild(ByVal str As String, ByVal WildCheckbox As Boolean) As String
        Dim strWild As String
        If WildCheckbox Then    'add pre and post wildcards
            strWild = "%" & str.Replace("*", "%") & "%"
            strWild.Replace("'", "%")
            Return strWild
        Else
            strWild = str.Replace("*", "%")
            strWild.Replace("'", "%")
            Return strWild
        End If
    End Function

    ''REMOVE CARRIAGE RETURNS FROM NAMES AND ADDRESS FIELDS
    Public Sub RemoveLineFeeds(ByRef ctl As Control)
        'so multiple line address fields don't mess up export
        ctl.Text = Trim(Replace(Replace(Replace(ctl.Text, NextLine, " "), Chr(10), " "), Chr(13), " "))
    End Sub

    ''REPLACE CARRIAGE RETURNS wtih SEMICOLONS for REFERRAL SOURCE
    Public Sub ReplaceLineFeeds(ByRef ctl As Control)
        ctl.Text = Trim(Replace(Replace(Replace(ctl.Text, NextLine, " "), Chr(10), " "), Chr(13), "; "))
    End Sub

    'ensure email has @ .
    Public Function ValidateEmail(ByRef ctl As Control, ByRef ep As ErrorProvider) As Boolean
        If ctl.Text.Contains(".") And ctl.Text.Contains("@") Then
            ep.SetError(ctl, String.Empty)
            Return True
        Else
            ep.SetError(ctl, "fix email address")
            msg("EMAIL ERROR", UCase(ctl.Text) & " --> is not a valid email.  An email must contain one @ and one period as in .org .com .net etc.", MessageBoxButtons.OK, MessageBoxIcon.Hand)
            Return False
        End If
        'Else
        '    msg("EMAIL ERROR", UCase(ctl.Text) & " --> is not a valid email.  An email must contain one @ and one period as in .org .com .net etc.", MessageBoxButtons.OK, MessageBoxIcon.Hand)
        '    ctl.Focus()
        'End If
    End Function

    'ensure email has @ .
    Public Function ValidateEmail(ByVal txt As String) As Boolean
        If txt.Contains(".") And txt.Contains("@") Then
            Return True
        Else
            msg("EMAIL ERROR", UCase(txt) & " --> is not a valid email.  An email must contain one @ and one period as in .org .com .net etc.", MessageBoxButtons.OK, MessageBoxIcon.Hand)
            Return False
        End If
    End Function

    'Clear NonAlpha NonNumeric Characters - not used??
    Function BareCompare(ByVal input As String) As String
        Dim pattern As String = "[^A-Za-z0-9]"
        Dim replacement As String = ""

        BareCompare = UCase(RegularExpressions.Regex.Replace(input, pattern, replacement))
    End Function

#End Region     'mdGlobalVar.GetWild, isnull

#Region "LOGIN"

    'GET GLOBAL VAR USER NAME, ID, REGION, SET GLOBAL CONNECTION STRING
    Public Function GetUser() As Boolean
        Dim SendEmail As New ClassEmail
        Dim sqlUsrInfo As New SqlCommand

        sqlUsrInfo.CommandText = "SELECT * FROM vwStaffLogin  ORDER BY SortOrder " ' WHERE Logonname = '" & usrLogon & "'"
        sqlUsrInfo.Connection = sc
        gPayment.EventID = 444 'default to floating event

        If Not SCConnect() Then
            GetUser = False
            Exit Function
        End If

        Dim myReader As SqlDataReader = sqlUsrInfo.ExecuteReader()

LoadStaffTable:
        Try
            tblStaff.Load(myReader)
            cntStaff = tblStaff.Rows.Count
            myReader.Close()
            myReader = Nothing
            tblStaff.PrimaryKey = New DataColumn() {tblStaff.Columns("StaffID")}
            GetUser = True
        Catch ex As Exception
            SendEmail.SendHTMLEmail(DBAdmin.StaffEmail, "LOAD STAFF error: " & Environment.UserDomainName & "\" & Environment.UserName, ex.Message) '"App: InfoCtr&#xaConnection String:\n" & sc.ConnectionString.ToString & "&#x0aComputer Name: " & IsNull(Environ("ComputerName"), "na") & "%OD%OAError: " & exc.Message)
            GetUser = False
            myReader.Close()
            myReader = Nothing
            GetUser = False
            Exit Function
        Finally
            sc.Close()
        End Try

SetDefaults:
        usrRegion = "Central"
        usr = 0 '1042729235 ?
        usrName = "Your name here"
        usrFirst = ""
        usrEmail = "cgreen"
        usrType = "Consultant"

SetRoles:  'staff assignments for permissions and messages
        GetRoles()

GetUserName:
        Dim drw() As DataRow
        drw = tblStaff.Select("Logonname = '" & usrLogon & "'")

        Try
            usrRegion = drw(0)("SatelliteRegion") 'taffFirstNameFirst"), drw(0)("StaffID"))
            usr = drw(0)("StaffID")
            usrName = drw(0)("StaffName")
            usrFirst = drw(0)("StaffFirstNameFirst")
            usrEmail = drw(0)("EmailName") & CenterEmail '"@CenterforCongregations.org"
            usrType = drw(0)("TypeofStaff")
            usrJobTitle = drw(0)("JobTitle")
            usrExt = drw(0)("Ext")
            drw = Nothing
        Catch ex As Exception
            modGlobalVar.msg("ERROR: Name not found", Environment.UserDomainName & "\" & Environment.UserName &
                              " ~ which could be a connectivity error.  Try again. " & NextLine & NextLine & "(The DBA has been notified.)  " & ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
            SendEmail.SendHTMLEmail(DBAdmin.StaffEmail, "ASSIGN NAMES error: " & Environment.UserDomainName & "\" & Environment.UserName, ex.Message) '"App: InfoCtr&#xaConnection String:\n" & sc.ConnectionString.ToString & "&#x0aComputer Name: " & IsNull(Environ("ComputerName"), "na") & "%OD%OAError: " & exc.Message)

            GetUser = False
            drw = Nothing
            Exit Function
        Finally
        End Try

        'Brent logs in with different name from home
        'If usr = 5001 Then
        '    usr = 220
        'End If

LoginUser:  'record in and out of infocenter
        GetUser = Login()
    End Function 'getuser

    'CREATE ROW IN LOGIN TABLE
    Private Function Login() As Boolean
        Dim SendEmail As New ClassEmail
        Dim sqlUsrInfo As New SqlCommand
        Dim cmd As SqlCommand = New SqlCommand("[LogIn]", sc)

        If usr > 9000 Then  'read-only user
            SendEmail.SendHTMLEmail(DBAdmin.StaffEmail, "LOGIN Read-Only: " & Environment.UserDomainName & "\" & Environment.UserName, "") '"App: InfoCtr&#xaConnection String:\n" & sc.ConnectionString.ToString & "&#x0aComputer Name: " & IsNull(Environ("ComputerName"), "na") & "%OD%OAError: " & exc.Message)
            Exit Function
        End If

        Dim osInfo As OperatingSystem
        osInfo = Environment.OSVersion
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add("@User", SqlDbType.VarChar).Value = usrLogon 'Environ("UserName")
        cmd.Parameters.Add("@computer", SqlDbType.VarChar).Value = Environ("ComputerName")
        cmd.Parameters.Add("@In", SqlDbType.DateTime2).Value = Now
        cmd.Parameters.Add("@db", SqlDbType.VarChar).Value = sc.DataSource & " - " & gTitle   '& My.Application.Info.Title 'sc.Database
        cmd.Parameters.Add("@Version", SqlDbType.VarChar).Value = strVersion
        cmd.Parameters.Add("@VersionDate", SqlDbType.DateTime2).Value = strVersionDt
        cmd.Parameters.Add("@OSystem", SqlDbType.NVarChar).Value = Left(osInfo.VersionString, 50)
        If Not SCConnect() Then
            Login = False
            Exit Function
        End If
        Try
            cmd.ExecuteNonQuery()
            Login = True
        Catch exc As Exception
            modGlobalVar.msg("ERROR: login for " & usrLogon, exc.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
            SendEmail.SendHTMLEmail(DBAdmin.StaffEmail, "LOGIN error: " & Environment.UserDomainName & "\" & Environment.UserName, exc.Message) '"App: InfoCtr&#xaConnection String:\n" & sc.ConnectionString.ToString & "&#x0aComputer Name: " & IsNull(Environ("ComputerName"), "na") & "%OD%OAError: " & exc.Message)
            usr = 0
            Login = False
        Finally
            sc.Close()
            cmd.Dispose()
        End Try
    End Function 'LOGIN


    'LOGOUT
    Public Sub LogOut()
        Dim cmd As New SqlCommand
        Dim str As String
        Dim sendemail As New ClassEmail

        'skip; never logged in so DBA got that email already
        If usr = 0 Then
            Exit Sub
        End If

        'skip for read only staff
        If usr > 9000 Then
            sendemail.SendHTMLEmail(DBAdmin.StaffEmail, "LOGOUT readonly: " & Environment.UserDomainName & "\" & Environment.UserName, strEmailBody) '"App: InfoCtr&#xaConnection String:\n" & sc.ConnectionString.ToString & "&#x0aComputer Name: " & IsNull(Environ("ComputerName"), "na") & "%OD%OAError: " & exc.Message)
            Exit Sub
        End If

        str = "UPDATE dbo.tblLogIn SET TimeOut = '" & Now & "' WHERE (TimeOut IS NULL) AND (UsrName = '" & usrLogon & "') AND (ComputerName = '" & Environ("ComputerName") & "') AND (DBName = '" & sc.DataSource & " - " & gTitle & "')" '& My.Application.Info.Title & "')" '& sc.Database & "')"
        cmd.CommandText = str
        cmd.Connection = sc

        If Not SCConnect() Then
            Exit Sub
        End If
        Try
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            sendemail.SendHTMLEmail(DBAdmin.StaffEmail, "LOGOUT error: " & Environment.UserDomainName & "\" & Environment.UserName, strEmailBody & NextLine & ex.Message) '"App: InfoCtr&#xaConnection String:\n" & sc.ConnectionString.ToString & "&#x0aComputer Name: " & IsNull(Environ("ComputerName"), "na") & "%OD%OAError: " & exc.Message)
        Finally
            sc.Close()
        End Try
    End Sub

    'SET STAFF PERMISSION CONSTANTS 
    Public Sub GetRoles()
        Dim drw() As DataRow            'single datarow
        Dim drCollection As DataRow()   'multiple datarows

SingleRoleJobTitles:  'permissions and help contact name

        'add staff forms, dbadmin
        DBAdmin = GetRole("DBAdmin") 'New structStaff("DBAdmin", drw(0)("StaffFirstNameFirst"), drw(0)("StaffID"), drw(0)("Emailname") & CenterEmail)

        'add staff forms
        ITDir = GetRole("ITDirector")  'New structStaff("ITDirector", drw(0)("StaffFirstNameFirst"), drw(0)("StaffID"), drw(0)("Emailname") & CenterEmail)

        'approve resources
        ResourceDir = GetRole("ResourceDirector") 'New structStaff("ResourceDirector", drw(0)("StaffFirstNameFirst"), drw(0)("StaffID"), drw(0)("Emailname") & CenterEmail)

        'generate grant proposal letter
        GrantAdmin = GetRole("GrantAdmin") 'New structStaff("GrantAdmin", drw(0)("StaffFirstNameFirst"), drw(0)("StaffID"), drw(0)("Emailname") & CenterEmail)

        'approve and update resources
        ResourceReviewer = GetRole("ResourceReviewer") 'New structStaff("ResourceReviewer", drw(0)("StaffFirstNameFirst"), drw(0)("StaffID"), drw(0)("Emailname") & CenterEmail)

        'add regonline ID to event detail; also used for Jane for Drupal
        RegonlineAdmin = GetRole("RegonlineAdmin") 'New structStaff("RegonlineAdmin", drw(0)("StaffFirstNameFirst"), drw(0)("StaffID"), drw(0)("Emailname") & CenterEmail)

        'when no case mgr -- formerly DefaultCaseManager
        DefaultCaseMgr = GetRole("ResourceGrantDirector") 'New structStaff("ResourceGrantDirector", drw(0)("StaffFirstNameFirst"), drw(0)("StaffID"), drw(0)("Emailname") & CenterEmail)

        'grant questions
        GrantDir = GetRole("ResourceGrantDirector") 'New structStaff("ResourceGrantDirector", drw(0)("StaffFirstNameFirst"), drw(0)("StaffID"), drw(0)("Emailname") & CenterEmail)

        'not currently in use
        EdDir = GetRole("EdDirector") 'New structStaff("EdDirector", drw(0)("StaffFirstNameFirst"), drw(0)("StaffID"), drw(0)("Emailname") & CenterEmail)

        'online paypal refunds for events
        PayPalAdmin = GetRole("PaypalAdmin") 'New structStaff("PaypalAdmin", drw(0)("StaffFirstNameFirst"), drw(0)("StaffID"), drw(0)("Emailname") & CenterEmail)

        'full edit rights & contact for CRG resource issues
        CRGAdmin = GetRole("CRGAdmin") 'New structStaff("CRGAdmin", drw(0)("StaffFirstNameFirst"), drw(0)("StaffID"), drw(0)("Emailname") & CenterEmail)

        'TODO REDO this one if get new Central ResourceAdmin separate from DBAdmin job Title
        ResourceAdmin = New structStaff("ResourceAdmin", "Catharine", 213, "cGreen" & CenterEmail)

MultipleStaffandStaffWithMultipleDuties:
        drCollection = tblStaff.Select("MemoDuties LIKE '%duplcong%'") 'combine congr/contact
        For Each dr As DataRow In drCollection
            StaffDuplCongr.Add(dr("StaffFirstNameFirst"), dr("StaffID"))
        Next

        drCollection = tblStaff.Select("MemoDuties LIKE '%duplresource%'") 'combine resources
        For Each dr As DataRow In drCollection
            StaffDuplResource.Add(dr("StaffFirstNameFirst"), dr("StaffID"))   'dr("StaffFirstNameFirst"), dr("StaffID"))
        Next

        drCollection = tblStaff.Select("MemoDuties LIKE '%CRGFull%'") 'edit CRGWebite fields - checkbox, status, and annotation - and url
        For Each dr As DataRow In drCollection
            StaffCRGFull.Add(dr("StaffFirstNameFirst"), dr("StaffID"))
        Next

        drCollection = tblStaff.Select("MemoDuties LIKE '%CRGEdit%'") 'edit CRG annotation
        ' Jane should only have access to annotation field, not other CRG fields
        For Each dr As DataRow In drCollection
            StaffCRGEdit.Add(dr("StaffFirstNameFirst"), dr("StaffID"))
        Next
Close:
        drCollection = Nothing
        drw = Nothing
    End Sub

    'AsSIGN SPECiAL STAFF ROLES
    Private Function GetRole(ByVal What As String) As structStaff
        Dim drw() As DataRow
        Try
            drw = tblStaff.Select("Role = '" & What & "'") '"Role ='ITDirector'") 'add staff forms
            GetRole = New structStaff(What, drw(0)("StaffFirstNameFirst"), drw(0)("StaffID"), drw(0)("Emailname") & CenterEmail)
        Catch ex As Exception
            MsgBox(ex.Message, , " error getting role: " & What)
            GetRole = Nothing
        Finally
            drw = Nothing
        End Try
    End Function

#End Region 'getuser

#Region "CONNECTION"

    'opens connection
    Public Function SCConnect() As Boolean
        Dim SendEmail As New ClassEmail
        If sc.State = ConnectionState.Open Then
        Else
            Try
                sc.Open()
            Catch ex As Exception
                modGlobalVar.msg("ERROR: network error", "Can't open connection to database server.  Cancelling your action.", MessageBoxButtons.OK, MessageBoxIcon.Error)
                SendEmail.SendHTMLEmail(DBAdmin.StaffEmail, "CONNECTION error: " & Environment.UserDomainName & "\" & Environment.UserName, strEmailBody & NextLine & ex.Message) '"App: InfoCtr&#xaConnection String:\n" & sc.ConnectionString.ToString & "&#x0aComputer Name: " & IsNull(Environ("ComputerName"), "na") & "%OD%OAError: " & exc.Message)
                Return False
            End Try
        End If
        Return True
    End Function

#End Region 'connect to solomon

#Region "FillDataTables"

    'generic table loader using sproc; order status, region, case status, registration, special mail, etc  
    Public Sub LoadDataTable(ByRef tbl As DataTable, ByRef sql As SqlCommand)
        If Not SCConnect() Then
            Exit Sub
        End If
        MouseWait()
        Try
            tbl.Constraints.Clear()
            tbl.Clear()
            tbl.BeginLoadData()
            tbl.Load(sql.ExecuteReader)
        Catch ex As Exception
            modGlobalVar.msg("ERROR: mdGlobalVar couldn't fill: " & tbl.TableName.ToString, ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            tbl.EndLoadData()
            sc.Close()
            MouseDefault()
        End Try
    End Sub

    'generic table loader using sproc that does not delete sproc; for use in loops like CurrentCaseCount
    Public Sub LoadDataTable(ByRef tbl As DataTable, ByVal sql As SqlCommand, bDeleteSQL As Boolean)
        If Not SCConnect() Then
            Exit Sub
        End If
        MouseWait()
        Try
            tbl.Clear()
            tbl.BeginLoadData()
            tbl.Load(sql.ExecuteReader)
        Catch ex As Exception
            modGlobalVar.msg("ERROR: mdGlobalVar couldn't fill bdelete " & tbl.TableName.ToString, ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            tbl.EndLoadData()
            sc.Close()
            MouseDefault()
        End Try

    End Sub

    ' generic table loader using string; order status, region, case status, registratio, special mail, etc
    Public Sub LoadDataTable(ByRef tbl As DataTable, ByVal str As String)
        Dim sql As New SqlCommand
        sql.Connection = sc
        sql.CommandText = str
        If Not SCConnect() Then
            Exit Sub
        End If
        MouseWait()
        Try
            tbl.Clear()
            tbl.Load(sql.ExecuteReader)
        Catch ex As Exception
            modGlobalVar.msg("ERROR: couldn't fill str " & tbl.TableName.ToString, ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            sc.Close()
            sql = Nothing
            MouseDefault()
        End Try

    End Sub

    'restructure email tables
    Public Sub SetupEmailTables()
        For i As Integer = 1 To tblSend.Columns.Count - 1
            tblSend.Columns(i).AllowDBNull = True
            tblSend.Columns(i).ReadOnly = True
        Next i
        ' For email  not to be read only
        tblSend.Columns(0).AllowDBNull = True 's/be false?
        tblSend.Columns(0).ReadOnly = False
        'create tblreject
        tblReject = tblSend.Copy
        tblReject.Clear()
    End Sub

#End Region 'fill data tables

#Region "Create Combo Datasets"

    'CREATE REGION ARRAYS
    Public Sub CreateRegionCol()

        If Not SCConnect() Then
            Exit Sub
        End If
        MouseWait()
        Dim qryFillIndex As New SqlCommand("SELECT distinct tblOrg.SatelliteRegion, sortkey FROM  tblOrg INNER JOIN dbo.vwUtilitySortSatelliteRegion ON tblorg.SatelliteRegion = dbo.vwUtilitySortSatelliteRegion.SatelliteRegion ORDER BY dbo.vwUtilitySortSatelliteRegion.sortKey", sc) '"SELECT DISTINCT SatelliteRegion FROM dbo.luCountyZip WHERE SatelliteRegion > '' ORDER BY SatelliteRegion", sc)
        Dim i As Integer = 1
        colRegionlu.Add("All Indiana", 0)

        Dim dr As SqlDataReader = qryFillIndex.ExecuteReader(CommandBehavior.CloseConnection)
        qryFillIndex.CommandType = System.Data.CommandType.StoredProcedure

        While dr.Read()
            If UCase(dr.GetString(0).ToString) = "NOT IN REGION" Or UCase(dr.GetString(0).ToString) = "OUTSIDE INDIANA" Then
                colRegionlu.Add(dr.GetString(0), i.ToString)    'regions including 'all'
                ' colRegion5lu.Add(dr.GetString(0), i.ToString)   'regions no 'All'
                i += 1
            Else
                colRegionOffice.Add(dr.GetString(0), i.ToString)    'office regions '
                colRegionlu.Add(dr.GetString(0), i.ToString)    'regions including 'all'
                colRegion5lu.Add(dr.GetString(0), i.ToString)   'just regions
                colRegionMailing.Add(dr.GetString(0), i.ToString)
                i += 1
            End If
        End While
        dr.Close()
        sc.Close()
        colRegionlu.Add("Show All", 10)
        colRegion5lu.Add("All Regions", 10)
        colRegionMailing.Add("Whole Central Area")
        colRegionMailing.Add("Whole Northern Area")
        colRegionMailing.Add("Whole Southern Area")

        qryFillIndex = Nothing

        'only regions set up in table resource luLocation
        Dim myCommand As New SqlCommand("SELECT DISTINCT SatelliteRegion FROM luLocation WHERE SatelliteRegion <>'All'", sc)
        LoadDataTable(tblLibRegion, myCommand)
        myCommand = Nothing
        MouseDefault()
    End Sub

    'workshop events -- date reinstated 1/16
    Public Sub LoadWEventDD(ByVal which As String) '(ByVal Dte As DateTime)--no longer use this way; fill with only future events
        Dim qryFill As New SqlCommand("[GetWEventDD]", sc)
        qryFill.Parameters.Add("@Dte", SqlDbType.Date)
        Select Case which
            Case Is = "upcoming"
                qryFill.Parameters("@Dte").Value = Today
            Case Is = "all"
                'use default in sproc
            Case Else 'master checklist name
                qryFill.Parameters.Add("@ID", SqlDbType.VarChar).Value = which
        End Select
        qryFill.CommandType = System.Data.CommandType.StoredProcedure
        LoadDataTable(tblWEvents, qryFill)

    End Sub

    'workshop orders - not used??
    Public Sub LoadOrderIDTbl()
        'TODO ? can arrange sort so Regonline high ids don't show up at top of list
        tblRegOrder.Clear()
        LoadDataTable(tblRegOrder, "SELECT Distinct OrderID from tblEventRegOrder2 ORDER BY OrderID desc")
    End Sub

    'CREATE STATUS DATASET
    Public Sub LoadCaseStatusDS()
        Dim qryFill As SqlCommand
        qryFill = New SqlCommand("SELECT StatusID, StatusName, Selectable FROM luStatus WHERE Topic  = 'Case' ORDER BY SortNum", sc)
        qryFill.CommandType = System.Data.CommandType.Text

        LoadDataTable(tblCaseStatus, qryFill)

    End Sub

    'CREATE table for ModeofConversationdDD
    Public Sub LoadConversMode()
        Dim qryFill As SqlCommand
        qryFill = New SqlCommand("SELECT StatusName FROM luStatus WHERE Topic  = 'ConversMode' and selectable = 1 ORDER BY SortNum", sc)
        qryFill.CommandType = System.Data.CommandType.Text
        LoadDataTable(tblConversMode, qryFill)
    End Sub

    'CREATE Resource Inactive DATASET for dd
    Public Sub LoadResourceInactiveDS()
        Dim qryFill As SqlCommand
        qryFill = New SqlCommand("SELECT StatusName FROM luStatus WHERE Topic  = 'ResourceInactive' and selectable = 1 ORDER BY SortNum", sc)
        qryFill.CommandType = System.Data.CommandType.Text
        LoadDataTable(tblResourceInactive, qryFill)
    End Sub

    'CREATE RESOURCE INDEX ARRAYLIST
    Public Sub CreateIndexArl()
        Dim qryFillIndex As New SqlCommand("[luResourceIndex]", sc)
        Dim i As Integer = 0
        If Not SCConnect() Then
            Exit Sub
        End If
        tblResourceIndex.Load(qryFillIndex.ExecuteReader)
        sc.Close()
    End Sub

    'CREATE COUNTY COLLECTION
    Public Sub CreateCountyCol()
        Dim r As SqlDataReader
        Dim myCommand As New SqlCommand("[getCounties]", sc)
        If Not SCConnect() Then
            Exit Sub
        End If
        Try
            r = myCommand.ExecuteReader()
        Catch ex As Exception
            modGlobalVar.msg("ERROR: loading county collection ", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        While r.Read()
            If IsDBNull(r(0)) Then
            Else
                colCountylu.Add(r.GetString(0))
            End If

        End While

        r.Close()
        sc.Close()
    End Sub

    ''CREATE RESOURCE TYPE
    Public Sub CreateResourceTypeCol()
        Dim qryFill As New SqlCommand("SELECT * FROM vwLUResourceType order by TypeSubtype desc, TypeName", sc)
        qryFill.CommandType = System.Data.CommandType.Text
        modGlobalVar.LoadDataTable(tblResourceType, qryFill)

        Try
            For Each r As DataRow In tblResourceType.Rows
                If r.Item("TypeSubtype") = "T" Then
                    colResourceType.Add(r("TypeName"))
                Else
                    colResourceSubType.Add(r("TypeName"))
                End If
            Next
        Catch ex As Exception
            modGlobalVar.msg("ERROR: couldn't set type filter", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        qryFill = Nothing
    End Sub

    'Load CRG tbl
    Public Sub LoadCRGtbl()
        Dim qryFill As New SqlCommand("[luCRG]", sc)
        qryFill.CommandType = System.Data.CommandType.StoredProcedure
        modGlobalVar.LoadDataTable(tblCRG, qryFill)
    End Sub

    'CREATE STAFF COLLECTION
    Public Sub CreateStaffCol()
        Dim myCommand As New SqlCommand("SELECT StaffID, StaffFirstNameFirst from luStaff", sc) 'StaffName

        If Not SCConnect() Then
            Exit Sub
        End If
        Dim r As SqlDataReader = myCommand.ExecuteReader()

        While r.Read()
            colStafflu.Add(r.GetString(1), r.GetInt16(0))
        End While
        r.Close()
        sc.Close()
    End Sub

    'Load Registration DD
    Public Function LoadRegistrantDD(ByVal bInActive As Boolean) As Boolean
        On Error GoTo errorMsg
        Dim qryFill As New SqlCommand("[GetContactDDAddress]", sc)
        qryFill.CommandType = System.Data.CommandType.StoredProcedure
        qryFill.Parameters.Add("@IncludeInactive", SqlDbType.Bit).Value = bInActive
        LoadDataTable(tblRegistrant, qryFill)
        qryFill.Dispose()
        Return True
errorMsg:
        Return False
    End Function

    'Special Mail Flag
    Public Sub CreateSpecialMailDS()
        Dim qryFill As New SqlCommand("SELECT * from vwMailLists WHERE CreateStaffNum = " & usr, sc)

        qryFill.CommandType = System.Data.CommandType.Text
        LoadDataTable(tblSpecialMail, qryFill)
    End Sub

#End Region   'create combo datasets

#Region "Fill Combo Boxes"

    'STAFF COMBOs - datasource = dt (or dv with filter)
    Public Sub LoadStaffCombo(ByRef ctl As ComboBox, ByVal PreSelect As Boolean, ByVal Abbrev As StaffComboChoices)
        'TODO WHY does this work here, when it wouldn't work for keyword combos on resource form?
        'TODO VERIFY this will work on unrelated forms being open choosing CRG/staff
        ctl.DisplayMember = "StaffName"
        ctl.ValueMember = "StaffID"

        dv = New DataView(tblStaff)
        Select Case Abbrev
            Case StaffComboChoices.AllAndNo    'Search Forms includes AllStaff &  NoStaff for cases, outsiders; exclude Alban, CMGI
                dv.RowFilter = "(StaffID < " & StaffComboIDs.Alban &
                  " OR  (StaffID >= " & StaffComboIDs.AllStaff & " AND not(StaffID  = " & StaffComboIDs.CMGIFeedback & "))) "

            Case StaffComboChoices.Historic  'use for Resource; include Alban; exclude "AllStaff","NoStaff", CMGI
                dv.RowFilter = "(StaffID not = " & StaffComboIDs.AllStaff & " and staffid not = " & StaffComboIDs.NoStaff & "And StaffID not = " & StaffComboIDs.CMGIFeedback & ")"

            Case StaffComboChoices.CMGI 'use for resource feedback , include CMGI, YourNameHere, Alban (Richard); excl All, No
                dv.RowFilter = "(StaffID not = " & StaffComboIDs.AllStaff & " and staffid not = " & StaffComboIDs.NoStaff & ")"

            Case StaffComboChoices.Full 'use for ??  includes 'CMGI form, No Staff, Your Name Here, Alban, outsiders
                dv.RowFilter = "(StaffID not = " & StaffComboIDs.AllStaff & " and staffid not = " & StaffComboIDs.NoStaff & "And StaffID not = " & StaffComboIDs.CMGIFeedback & " and staffid not = " & StaffComboIDs.CMGIFeedback & ")"
            Case StaffComboChoices.Selectable  'use for most Detail forms; excl All No, Alban, CMGI, Your Name--?? current Center staff names and headings - only for new entity entry forms -- what if outsiders are testing??
                dv.RowFilter = "(StaffID > " & StaffComboIDs.CenterMin & "  and  StaffID < " & StaffComboIDs.CenterMax & ") or (StaffID > " & StaffComboIDs.Headings & ")" ' and  StaffID < " & StaffComboIDs.Outsiders & ")"
            Case Else
                modGlobalVar.msg("ERROR: cbo option not found", Abbrev.ToString, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Select

        ctl.DataSource = dv

        If PreSelect Then  'set combobox to username only for new records
            ctl.SelectedIndex = ctl.FindString(usrName)
        End If

    End Sub

    'CRG COMBOs - datasource = dt
    Public Sub LoadCRGCombo(ByRef ctl As ComboBox)
        ctl.DisplayMember = "CRGName"
        ctl.ValueMember = "CRGID"
        ctl.DataSource = tblCRG.Copy  'dsCRG.Tables("luCRG")
    End Sub

    'LOAD CBO - CASE STATUS
    Public Sub LoadCaseStatusCombo(ByRef ctl As ComboBox, ByVal Which As String)
        ctl.DisplayMember = "StatusName"
        ctl.ValueMember = "StatusID"
        Select Case Which
            Case Is = "Full"    'for search forms   
                ctl.DataSource = tblCaseStatus.Copy
            Case Is = "Selectable" 'for dataentry forms
                Dim dv As DataView
                dv = New DataView(tblCaseStatus.Copy)
                dv.RowFilter = "Selectable = 1"
                ctl.DataSource = dv
            Case Else
                modGlobalVar.msg("ERROR: typo Load Case", Which, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Select
    End Sub

    'Load Case status List
    Public Sub LoadStatusList(ByRef ctl As ListBox, ByVal Abbrev As String) 'show full list on search forms, not case forms
        ctl.DisplayMember = "StatusName"
        ctl.ValueMember = "StatusID"
        'TODO VERIFY it's ok to use same datatable when different forms might overlap calls
        'TODO change table name based on input to make this generic
        Select Case Abbrev
            Case Is = "Full"    'for search forms   
                ctl.DataSource = tblCaseStatus.Copy 'dsStatus.Tables("dtStatusCase")
            Case Is = "Selectable" 'for dataentry forms
                Dim dv As DataView
                dv = New DataView(tblCaseStatus.Copy) 'dsStatus.Tables("dtStatusCase"))
                dv.RowFilter = "Selectable = 1"
                ctl.DataSource = dv
            Case Else
                modGlobalVar.msg("ERROR: typo Load Status", Abbrev, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Select
    End Sub

    'ORDER STATUS combo; main order/payment
    Public Sub LoadOrderStatusCombo(ByRef ctl As ComboBox)
        ctl.Items.Add("")
        For Each r As DataRow In tblOrderStatus.Rows
            ctl.Items.Add(r("StatusName"))
        Next
    End Sub

    'LOAD CONTACT NAMES
    Public Sub LoadContactCombo(ByRef ctl As ComboBox, ByRef tbl As DataTable, ByVal id As Integer)

        Dim cmd As New SqlCommand("[luContactNames]", sc)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add("@IDVal", SqlDbType.Int).Value = id

        If Not SCConnect() Then
            Exit Sub
        End If
        Try
            tbl.Load(cmd.ExecuteReader(CommandBehavior.CloseConnection))
        Catch ex As Exception
            modGlobalVar.msg("ERROR: can't fill contact combo", ex.Message & NextLine & "param: " & id.ToString, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        ctl.ValueMember = "ContactID"
        ctl.DisplayMember = "ContactName"
        ctl.DataSource = tbl
    End Sub

    'LOAD CASE NAMES
    Public Sub LoadCaseCombo(ByRef ctl As ComboBox, ByRef tbl As DataTable, ByVal id As Integer)
        Dim cmd As New SqlCommand("[luCaseNames]", sc)
        cmd.CommandType = System.Data.CommandType.StoredProcedure
        cmd.Parameters.Add("@OrgID", SqlDbType.Int).Value = id

        If Not SCConnect() Then
            Exit Sub
        End If
        Try
            tbl.Load(cmd.ExecuteReader(CommandBehavior.CloseConnection))
        Catch ex As Exception
            modGlobalVar.msg("Error: can't fill case combo", ex.Message & NextLine & "param: " & id.ToString, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        ctl.ValueMember = "CaseID"
        ctl.DisplayMember = "CaseName"
        ctl.DataSource = tbl
    End Sub

    'REGION COMBOs
    Public Sub LoadRegionCombo(ByRef ctl As ComboBox, ByVal strWhich As String)
        Select Case strWhich
            Case Is = "Indiana" '5 regions + 'All indiana'
                ctl.DataSource = colRegion5lu
            Case Is = "All" 'verything, incl out of state
                ctl.DataSource = colRegionlu
            Case Is = "Office"  'don't include "not in region"
                ctl.DataSource = colRegionOffice
                '  Case Is = "Region"  'don't include "All Regions"
                'ctl.DisplayMember = "SatelliteRegion"
                ' ctl.ValueMember = "SatelliteRegion"
                '     ctl.DataSource = colRegion5lu
            Case Is = "Mail"
                ctl.DataSource = colRegionMailing
            Case Is = "Library"   'get only regions already listed in table lulocation
                ctl.DisplayMember = "SatelliteRegion"
                ctl.ValueMember = "SatelliteRegion"
                ctl.DataSource = tblLibRegion.Copy
        End Select
    End Sub

    'REGION COMBOs
    Public Sub LoadRegionCombo(ByRef ctl As ComboBox, ByVal strWhich As String, ByVal strDefault As String)
        Select Case strWhich
            Case Is = "All" 'include 'All Regions'
                ctl.DataSource = colRegionlu
            Case Is = "Office"  'don't include "not in region"
                ctl.DataSource = colRegionOffice
            Case Is = "Region"  'don't include "All Regions"
                'ctl.DisplayMember = "SatelliteRegion"
                ' ctl.ValueMember = "SatelliteRegion"
                ctl.DataSource = colRegion5lu
            Case Is = "Mail"
                ctl.DataSource = colRegionMailing
            Case Is = "Library"   'get only regions already listed in table lulocation
                ctl.DisplayMember = "SatelliteRegion"
                ctl.ValueMember = "SatelliteRegion"
                ctl.DataSource = tblLibRegion.Copy
        End Select
        If strDefault = String.Empty Then
        Else
            ctl.SelectedIndex = ctl.FindStringExact(strDefault)
        End If
    End Sub

    'EVENT TYPE COMBO
    Public Sub LoadEventTypeCombo(ByRef ctl As ComboBox, ByVal bAll As Boolean)
        Dim da As SqlDataAdapter = New SqlDataAdapter
        Dim ds As New DataSet
        Dim myCommand As New SqlCommand
        myCommand.Connection = sc
        If bAll = False Then    'don't include retired terms for new events
            myCommand.CommandText = "SELECT StatusName as TypeofEvent FROM luStatus WHERE Topic = 'Workshop' AND  Ball is null ORDER BY Sortnum"
        Else 'include retired terms for historic lookup
            myCommand.CommandText = "SELECT StatusName as TypeofEvent FROM luStatus WHERE Topic = 'Workshop'  ORDER BY Sortnum" ', sc) 'TypeofEvent from tblEdEvent WHERE TypeofEvent IS NOT NULL", sc)
        End If
        da.SelectCommand = myCommand
        Try
            da.Fill(ds)
        Catch ex As Exception
            modGlobalVar.msg("ERROR: can't fill event type combo", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        ctl.DisplayMember = "TypeofEvent"
        ctl.ValueMember = "TypeofEvent"
        ctl.DataSource = ds.Tables(0)
        da.Dispose()
        'ds.Dispose()
    End Sub

#End Region 'load combo boxes

#Region "New Validate Combo"

    'for SEARCH FORMS
    Public Function NewValidateCombo(ByRef ctl As ComboBox, ByVal bRequired As Boolean) As Boolean '=valid

        Dim str As String
        Dim What As String = IsNull(ctl.Tag, ctl.Name)
        Dim iFound As Integer

        If bRequired = False And ctl.Text = String.Empty Then
            ctl.SelectedIndex = -1
            ctl.SelectedIndex = -1
            Return True
        End If

        'EMPTY or HEADING
        If Replace(IsNull(ctl.Text, " "), " ", "") = String.Empty Then
            str = "  "
        ElseIf ctl.Text.Contains("---") Then
            str = "  "  'have 2 spaces here; some combos have singlespace headings for spacing
        Else
            str = ctl.Text
        End If

        iFound = ctl.FindStringExact(str)
        If iFound >= 0 Then
            Return True
        End If
        ''CHECK NOT HEADING
        If bRequired Then
            modGlobalVar.msg("ATTENTION: Invalid Selection", "Select an item from the " & What & " dropdown.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ctl.DroppedDown = True
            Return False
        Else
            ctl.Text = String.Empty
            Return True
        End If
    End Function

    'VALIDAT CBO for Headings
    Public Function NewIsHeading(ByRef ctl As ComboBox) As Boolean
        If ctl.Text.Contains("---") Then
            ctl.Focus()
            ctl.DroppedDown = True
            Return True
        Else
            Return False
        End If
    End Function

    'VALIDATE  CBO FOR DETAIL FORMS w ErrorProvider
    Public Function ValidateBoundDD(ByRef cbo As ComboBox, ByVal bRequired As Boolean, ByRef EP As ErrorProvider, ByVal HowCalled As ObjClose) As usrInput ', ByVal OldVal As Integer) As Boolean
        Dim what As String = IsNull(cbo.Tag, cbo.Name)
        Dim iFound As Integer

        If cbo.Text = String.Empty And bRequired = False Then
            cbo.SelectedValue = DBNull.Value
            EP.SetError(cbo, "")
            Return usrInput.OK
        End If

        'HEADING
        If cbo.Text.Contains("---") Then
            EP.SetError(cbo, "select a valid " & what)
            '  cbo.DataBindings.Item(0).ReadValue() 'reset to old value --was causing recursive problem?
            If HowCalled = ObjClose.CloseByControl Then '?? why is this here??
                cbo.Focus()
                cbo.DroppedDown = True
            End If
            Return usrInput.Retry
        End If

        iFound = cbo.FindStringExact(cbo.Text)

        'VALID SELECTION
        If iFound > -1 Then
            EP.SetError(cbo, "")
            Return usrInput.OK
        End If

        'INVALID 
        If iFound = -1 And cbo.Text > "" Then
            '  cbo.Text = ""
            EP.SetError(cbo, "invalid selection")
            '  If HowCalled = ObjClose.CloseByControl Then
            '  cbo.Focus()
            '  cbo.DroppedDown = True
            'End If
            Return usrInput.Search
        End If

        'IS EMPTY OK?
        If bRequired = True Then
            EP.SetError(cbo, "please enter a " & what & NextLine & " or use 'Menu Edit--Delete' to delete this whole record entirely")
            ''   If HowCalled = ObjClose.CloseByControl Then
            '  cbo.Focus()
            '  cbo.DroppedDown = True
            ' ' cbo.DataBindings.Item(0).ReadValue() 'reset to old value--was causing recursive problem?
            ''End If
            Return usrInput.Retry
        Else
            cbo.SelectedValue = DBNull.Value
            EP.SetError(cbo, "")
            '   If bAddNew = True Then
            '   Return usrInput.Ignore
            'Else
            Return usrInput.OK
            'End If
        End If

    End Function

    'VALIDATE STAFF CBO FOR SEARCH FORMS when no error symbol is required; exclude only Headings
    Public Function NewValidateStaff(ByRef cbo As ComboBox, ByVal bRequired As Boolean) As Boolean ', ByVal StaffHeading As conStaffHeading) As Boolean
        Dim iFound As Integer
        Dim what As String = IsNull(cbo.Tag, cbo.Name)
        'EMPTY 
        If Replace(IsNull(cbo.Text, " "), " ", "") = String.Empty Or cbo.Text.Contains("---") Then
            cbo.Text = ""
        End If
        iFound = cbo.FindStringExact(cbo.Text)

        ''CHECK NOT HEADING
        If iFound = -1 Or (cbo.SelectedValue > StaffComboIDs.Headings And cbo.SelectedValue < StaffComboIDs.Outsiders) Then ''StaffHeading Then
            modGlobalVar.msg("ATTENTION: Invalid selection", "Select staff name from " & what & " Dropdown.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            cbo.DroppedDown = True
            NewValidateStaff = False
        Else
            NewValidateStaff = True
        End If
    End Function

    'VALIDATE LIST BOX for search forms
    Public Function NewCheckList(ByVal str As String, ByRef lst As Control, ByVal strTopic As String) As Boolean
        If str = Nothing Or str.Contains("---") Then
            modGlobalVar.msg("ATTENTION: Invalid Selection", "please select a different " & strTopic & " and search again", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            NewCheckList = False
            lst.Focus()
        Else
            NewCheckList = True
        End If
    End Function

    'CBO NotRequired, NotAddNew, no EP
    Public Function ValidateCBO(ByRef cbo As ComboBox, ByVal what As String, ByVal isRequired As Boolean, ByVal AddNew As CanAddNew) As usrInput

        'HEADING
        If cbo.Text.Contains("---") Then
            cbo.Focus()    'form side
            cbo.DroppedDown = True
            Return usrInput.Retry
        End If

        'OK
        If cbo.SelectedIndex > -1 Then
            Return usrInput.OK
        End If

        'EMPTY STRING
        If Replace(IsNull(cbo.Text, " "), " ", "") = String.Empty And AddNew = CanAddNew.OKBlank Then
            Return usrInput.Ignore
        End If

        'ADD NEW
        If AddNew = CanAddNew.AddNew Then
            If modGlobalVar.msg("Do you wish to enter a new " & what & "?", "INVALID ENTRY", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                Return usrInput.Yes 'enter new item; requery combo; select new entry
            Else    'don't enter new
                Try
                    cbo.FindForm.Controls("statusbarpanel1").Text = "edit the " & what & " from the " & IsNull(cbo.Tag, cbo.Name) & " Detail window" ', modGlobalVar.MsgStyle.DefaultButton1 Or MessageBoxIcon.Information, "To change the " & what & " name:")
                Catch ex As Exception
                End Try
            End If
        End If

        'OPEN SEARCH WINDOW
        If AddNew = CanAddNew.Search Then
            If modGlobalVar.msg("Open Search Window?", IsNull(cbo.Text, "unknown") & " " & what & " not found", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                Return usrInput.Search
            Else
                Return usrInput.Retry
            End If
        Else    'CANNOT ADD NEW, don't need to search
        End If

SetMessage:

        If isRequired Then
            cbo.Focus()    'form side
            cbo.DroppedDown = True
            Return usrInput.Retry
        Else
            Return usrInput.OK
        End If

    End Function


#End Region 'new validate combo

#Region "GetCounts"

    'VALID REGISTRATIONS;  'includes count of cancelled
    Public Function CountValidRegistrations(ByVal id As Integer, What As String) As String
        Select Case What
            Case Is = "Event" 'qry returns string
                Return "WITH GetRegs AS (SELECT   EventNum, COUNT(RegistrationID) AS NumRegistrations       FROM            tblEventReg2       WHERE        (Cancelled = 0) AND (EventNum = " & id & " )   GROUP BY EventNum), GetCancels AS     (SELECT        EventNum, COUNT(RegistrationID) AS NumCancelled       FROM            tblEventReg2 AS tblEventReg2_1       WHERE        (Cancelled = 1) AND (EventNum =" & id & " )       GROUP BY EventNum)     SELECT        'Registrations: ' + CAST(isnull(GetRegs_1.NumRegistrations,0) as varchar) +  CASE WHEN NumCancelled > 0 THEN '  (Cancellelations: ' + CAST(GetCancels_1.NumCancelled AS varchar) + ')' ELSE '' END AS Expr1 ,  COALESCE (GetRegs_1.EventNum, GetCancels_1.EventNum) AS EventNum     FROM            GetRegs AS GetRegs_1 FULL OUTER JOIN     GetCancels AS GetCancels_1 ON GetRegs_1.EventNum = GetCancels_1.EventNum"
                'not working string"SELECT 'Registrations: ' + CAST(ISNULL(NumRegistrations, 0) AS varchar) + '  Cancelled: ' + CAST(ISNULL(NumCancelled, 0) AS varchar) AS Expr1 FROM  (SELECT   CASE WHEN Cancelled = 0 THEN COUNT(tblEventReg2.RegistrationID) END AS NumRegistrations,   CASE WHEN Cancelled = 1 THEN COUNT(tbleventReg2.Registrationid) END AS NumCancelled    FROM    (SELECT  RegistrationID AS RegNum  FROM  vwGetValidEventRegs2) AS vvr INNER JOIN  tblEventReg2 ON vvr.RegNum = tblEventReg2.RegistrationID  WHERE (tblEventReg2.EventNum = " & id & " ) AND (tblEventReg2.ContactNum > 0) AND (tblEventReg2.Notes IS NULL OR  tblEventReg2.Notes NOT LIKE 'Delete%')  GROUP BY tblEventReg2.Cancelled) AS derivedtbl_1  "
            Case Is = "EventDetail" 'qry returns int
                Return "SELECT COUNT(RegistrationID) FROM (SELECT RegistrationID as RegNum FROM vwgetvalidEventRegs2) as vvr INNER JOIN tblEventReg2 ON Regnum = registrationid WHERE EventNum = " & id & " AND ContactNum > 0  AND (Notes is null or Notes not like 'Delete%') " 'and (cancelled <>1)
            Case Is = "Contact"
                Return "SELECT COUNT(RegistrationID) FROM (SELECT RegistrationID as RegNum FROM vwgetvalidEventRegs2) as vvr INNER JOIN tblEventReg2 ON Regnum = registrationid WHERE  ContactNum = " & id & " and (cancelled <>1) AND (Notes is null or Notes not like 'Delete%') "
            Case Is = "Attended"
                Return "SELECT COUNT(RegistrationID) FROM (SELECT RegistrationID as RegNum FROM vwgetvalidEventRegs2) as vvr INNER JOIN tblEventReg2 ON Regnum = registrationid WHERE EventNum = " & id & " AND ContactNum > 0 and (Attended = 1) AND (Notes is null or Notes not like 'Delete%') "
            Case Else
                Return String.Empty
        End Select
    End Function

#End Region

#Region "SatelliteRegional Colours"

    'TODO move these to resource file or external table
    Public Function GetRegionColor(ByVal Reg As String) As System.Drawing.Color
        '  
        Select Case IsNull(Reg, "Other")
            Case "Central"  'pastel blue
                ' Return Color.FromKnownColor(KnownColor.InactiveCaptionText)
                Return Color.FromArgb(195, 219, 255)
            Case "NE"   'pastel green
                Return Color.FromArgb(175, 210, 175) '175, 210, 175) '(221, 245, 221) '230, 255, 230) 'Honeydew too light

            Case "SE", "SC" 'dark blue as per website'bluered-orange 240, 200, 195 matches map, but is too pink
                Return Color.FromArgb(170, 198, 250) ' ok(125, 150, 215)    'darker blue
                'old red  Return Color.FromArgb(234, 170, 177) '(255, 195, 175) '255, 225, 210) '225, 215) '155, 155) '210, 110, 110) '255, 128, 110) '245, 200, 190) '238, 210, 200) '240, 200, 185) '215, 190) '175, 155) '240, 213, 200) '240, 200, 195) '235, 170, 160) '255, 168, 168) '204, 130, 153)  '210, 120, 120)  '255, 158, 158) '(255, 199, 204) '(220, 120, 120)    '230, 170, 190)    '250, 235, 230) 'MistyRose  'Linen  'BlanchedAlmond '.wheat
                '     Case "Not in Region"   '"SW"   'purple-
                '         Return Color.FromArgb(201, 185, 201) 'Thistle    'FromArgb(240, 204, 168) '222, 174, 120)    'BurlyWood  'FromArgb(222, 184, 135) 'tan: 210,180,140) '225, 200, 255)    'Thistle    235, 230, 255
            Case "NW"   'pastel yellow
                Return Color.LemonChiffon 'FromArgb(255, 255, 192) ' or LightGoldenrodYellow   'lemonchiffon
            Case "SW"   ' pastel brown
                Return Color.FromArgb(242, 221, 220) ''230, 200, 170) '225, 178, 145) '225, 160, 160) '238, 178, 148) '(220, 160, 122) '.fromargb(200, 160, 122)=light burlywood   'BurlyWood too intense
            Case "South" 'obsolete
                Return Color.FromArgb(255, 204, 204) '235, 172, 175) '225, 178, 145) '238, 178, 148) '(220, 160, 122) '.fromargb(200, 160, 122)=light burlywood   'BurlyWood too intense

            Case Else

                Return Color.LightGray
        End Select
    End Function
#End Region   'region background colors on various forms

#Region "Grid Functions"

    'CAPTURE GRID LEFT BUTTONCLICK
    Public Function CheckDouble(ByVal sender As Object, ByVal e As MouseEventArgs) As Date
        If e.Button = MouseButtons.Left Then
            ' gridMouseDownTime = DateTime.Now
            Return (DateTime.Now)
        End If
    End Function

    'FILTER DATAGRIDVIEW datatable returns array: count of distinct rows and heading text
    Public Function FilterDataGridView(ByRef grdvw As DataGridView, ByVal e As System.Windows.Forms.MouseEventArgs, ByRef tbl As DataTable, ByVal bCanAdd As Boolean) As String()
        '--12/15 used by srchCase, srchResource, Email, Library

        Dim htivw As DataGridView.HitTestInfo = grdvw.HitTest(e.X, e.Y)
        '  Dim source1 As New BindingSource()
        Dim dv As DataView = tbl.DefaultView
        Dim EmptyRow As Integer = (CType(bCanAdd, Integer) * -1)    '(convert boolean to subtractable integer)

        If e.Button = System.Windows.Forms.MouseButtons.Right Then
            'SET VARs BASED ON GRID SELECTED

            If htivw.Type = DataGrid.HitTestType.Cell Then    'legitimate cell clicked - SET FILTER

                'CHECK FOR NULL
                If IsDBNull(grdvw.Item(htivw.ColumnIndex, htivw.RowIndex)) Then 'nulls cause filter error
                    modGlobalVar.msg(MsgCodes.filterEmpty)
                    FilterDataGridView = {(grdvw.RowCount - EmptyRow).ToString, ""}
                End If

                'SET FILTER using dataview incl replace apostrophe
                Try
                    dv.RowFilter = grdvw.Columns(htivw.ColumnIndex).DataPropertyName & " = '" & Replace(grdvw.Item(htivw.ColumnIndex, htivw.RowIndex).Value, "'", "''") & " '"
                Catch ex As System.Data.EvaluateException
                Catch ex As Exception
                    MsgBox(ex.Message, , "dgv Filter error")
                End Try
                 grdvw.DataSource = dv
                '  Return {(grdvw.RowCount - EmptyRow).ToString, ".  Filtered on " & grdvw.Columns(htivw.ColumnIndex).HeaderText}
                'for RESOURCES with multiple authors
                FilterDataGridView = {CountDistinctRows(dv).ToString, "  Filtered on " & grdvw.Columns(htivw.ColumnIndex).HeaderText}
            Else
                'CLEAR FILTER 'click not in a cell
                dv.RowFilter = ""
                grdvw.DataSource = tbl
                FilterDataGridView = ({tbl.Rows.Count.ToString, ""})

            End If

            grdvw.ClearSelection()
            'first cell is  selected by default, might as well highlight row so user can tell
            'If grdvw.Rows.Count > 0 Then
            '    grdvw.Rows(0).Selected = True
            '    'grdvw.CurrentCell = grdvw(3, 0)
            'End If

        Else 'LEFT CLICK
            FilterDataGridView = {(grdvw.RowCount - EmptyRow).ToString, "LEFT"}
        End If



    End Function

    'FILTER DATAGRIDVIEW dataview returns array: count of distinct rows and heading text
    Public Function FilterDataGridView(ByRef grdvw As DataGridView, ByVal e As System.Windows.Forms.MouseEventArgs, ByRef dv As DataView, ByVal bCanAdd As Boolean) As String()
        '--2/16 used by srchCase xxxxxxxxxxx, srchResource, Email, Library
        Dim htivw As DataGridView.HitTestInfo = grdvw.HitTest(e.X, e.Y)
        Dim EmptyRow As Integer = (CType(bCanAdd, Integer) * -1)    '(convert boolean to subtractable integer)

IS_RIGHT_CLICK:
        If e.Button = System.Windows.Forms.MouseButtons.Right Then
            grdvw.ClearSelection() 'required or multiple rows highlighted

            'IS LEGITIMATE CELL - SET FILTER
            If htivw.Type = DataGrid.HitTestType.Cell Then

                'CHECK FOR NULL
                If IsDBNull(grdvw.Item(htivw.ColumnIndex, htivw.RowIndex)) Then 'nulls cause filter error
                    modGlobalVar.msg(MsgCodes.filterEmpty)
                    FilterDataGridView = {(grdvw.RowCount - EmptyRow).ToString, ""}
                End If

                'SET FILTER using dataview incl replace apostrophe
                Try
                    dv.RowFilter = grdvw.Columns(htivw.ColumnIndex).DataPropertyName & " = '" & Replace(grdvw.Item(htivw.ColumnIndex, htivw.RowIndex).Value, "'", "''") & " '"
                Catch ex As System.Data.EvaluateException
                Catch ex As Exception
                    MsgBox(ex.Message, , "dgv Filter error")
                End Try
                FilterDataGridView = {CountDistinctRows(dv).ToString, "  Filtered on " & grdvw.Columns(htivw.ColumnIndex).HeaderText}

                'IS NOT CELL - CLEAR FILTER 
            Else
                dv.RowFilter = ""
                FilterDataGridView = ({dv.Table.Rows.Count.ToString, "CLEAR"})
            End If
            'first cell is  selected by default, but row is not recognized by calling form
            '   grdvw.CurrentCell = grdvw(3, 0)
IS_LEFT_CLICK:
        ElseIf e.Button = System.Windows.Forms.MouseButtons.Left Then
            FilterDataGridView = {(grdvw.RowCount - EmptyRow).ToString, "LEFT"}
        Else 'future some other kind of click
            FilterDataGridView = {"Other"}
        End If

    End Function

    'counts number of rows after filter applied.  assume 1st column is unique
    Private Function CountDistinctRows(ByRef dvFiltered As DataView) As Integer
        '--potential non-unique rows: srchResource, Library
        Dim tblDistinct As DataTable = dvFiltered.ToTable("tblDistinct", True, dvFiltered.Table.Columns(0).ColumnName)
        Return tblDistinct.Rows.Count
    End Function

#End Region 'grid functions

#Region "FormatPhoneZip"

    'FORMATS .TEXT TO TAG PROPERTY
    Public Function LeavePhone(ByRef sender As System.Object, ByVal strCountry As String) As Boolean ' ByVal e As System.EventArgs)
        Dim str As String

        'remove non-numeric characters
        str = System.Text.RegularExpressions.Regex.Replace(sender.text, "[^\d]", "")
        MsgBox(str)
        'remove initial 1
        If str.StartsWith("1") Then
            str = str.Substring(1)
        End If

        If UCase(strCountry) = "USA" Or UCase(strCountry) = "CANADA" Then
            If sender.text.length < 10 Then
                modGlobalVar.msg("ATTENTION: Incomplete entry", FormatPhone(str) & NextLine & "not enough digits for a phone number." &
                                 NextLine & "(Area Code is required).", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                sender.select()
                sender.Select(1, InStr(str, ")") - 2)
                Return False
            End If

            Select Case Len(str)
                Case Is = 10 'add formatting
                    sender.text = FormatPhone(str)
                    Return True
                    'Case Is = 14 And (InStr(str, "-") = 10 And str.Substring(0, 1) = "(" And str.Substring(4, 1) = ")") ' AOK
                    '    Exit Function
                Case Else
                    modGlobalVar.msg("ATTENTION: Incomplete entry", FormatPhone(str) & NextLine & "Wrong number of digits for a phone number.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    sender.select()
                    sender.Select(1, InStr(str, ")") - 2)
                    Return False
            End Select
        Else '  '??is not a N.American number?
            If IsNumeric(str) Then
                Return True
            Else
                Return False
            End If
        End If

    End Function

    '  FORMAT NUMERIC CONTENT OF TEXTBOX WHEN IT LOSES FOCUS
    Public Function FormatPhone(ByVal sPhone As String) As String
        If sPhone = String.Empty Then
            Return "(  )"
        Else
            Dim d As Double
            d = sPhone
            Return d.ToString("(###) ###-####") 'Format(d, roTB.Tag)
        End If
    End Function

    'FORMAT ZIPCODE TEXTBOX
    Public Function FormatZip(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal strState As String) As String 'returns usrinput or valid zip
        'Handles TextBox1.Leave

        If Len(LTrim(sender.text)) < 5 Then
            modGlobalVar.msg("ATTENTION: not enough digits", "Please check zip code", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return usrInput.Retry
        End If
        'CHECK NUMERIC/USA
        If strState <> "IN" Then
            Return usrInput.Ignore
        End If

        If Not IsNumeric(Left(sender.text, 5)) Then
            If modGlobalVar.msg("Is this a USA address?", "Checking Zip code", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                modGlobalVar.msg("ATTENTION: invalid zip", "Please check zip. Non-numeric characters found.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Return usrInput.Retry
            Else    'check rest of number
            End If
        End If

        'FORMAT TEXT
        Select Case Len(sender.Text)
            Case 5  'fine
                Return usrInput.OK
            Case 10 And Mid(sender.text, 6, 1) = "-"
                If IsNumeric(Right(sender.text, 4)) Then
                    Return usrInput.OK
                Else
                    modGlobalVar.msg("ATTENTION: invalid zip", "Please check zip. Non-numeric characters found.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Return usrInput.Retry
                End If
                'fine
            Case 6  'presume 5 digits with hyphen
                Return Left(sender.text, 5)
            Case 9 And IsNumeric(Right(sender.text, 4))  'add hyphen
                Return Mid(sender.Text, 1, 5) & "-" & Mid(sender.Text, 6, 4)
            Case Is <> 9 'not enough/too many numbers
                modGlobalVar.msg("ATTENTION: not enough digits", "Please check zip code", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                Return usrInput.Retry
        End Select
        Return "False"
    End Function

    'Specific City MisSpellings
    Public Function CheckCity(ByRef ctl As Control, ByVal str As String) As String
        If str = "Indpls." Or str = "Indpls" Or str = "Indianpolis" Or str = "Indianapols" Or str = "Inidanpolis" Or str = "indpls" Or str = "indy" Or str = "Indy" Or str = "Indianapoils" Then
            Return "Indianapolis"
        End If
        If str = "Ft Wayne" Or str = "Ft. Wayne" Or str = "ft wayne" Then
            Return "Fort Wayne"
        End If
        'PO doesn't like periods in city names
        If InStr(str, ".") Then
            Return StrConv(Replace(str, ".", ""), VbStrConv.ProperCase)
        End If
        Return StrConv(str, VbStrConv.ProperCase)
    End Function

#End Region  'format phone/zip

#Region "OPEN FORMS"

    'VERIFY Item hasn't been DELETED since grid filled
    Private Function CheckExists(ByVal What As String, ByVal Qry As String) As Boolean
        If Not SCConnect() Then
            Exit Function
        End If
        Dim sql As New SqlCommand(Qry, sc)
        sql.CommandType = CommandType.Text

        Try
            Dim i As Integer = sql.ExecuteScalar

            If i > 0 Then
                CheckExists = True
            Else
                msg("ATTENTION: No Results, Cancelling Request.", "This " & What & " no longer exists.", MessageBoxButtons.OK, MessageBoxIcon.Information)
                CheckExists = False
            End If
        Catch ex As Exception
            msg("Execute query glitch", "during CheckExists", MessageBoxButtons.OK, MessageBoxIcon.Error)
            CheckExists = True
        Finally
            sc.Close()
        End Try

    End Function

    'send emails directly, bypassing excel view. using Delivra 11/15
    Public Sub OpenEmailForm(ByRef sql As SqlCommand, ByVal sHeading As String)
        Dim f As New frmMailEmail

        '1. fill tables
        modGlobalVar.LoadDataTable(tblSend, sql)
        If tblSend.Rows.Count > 0 Then
            SetupEmailTables()
            '2. open form for user to check emails and add subject and body
            f.Text = sHeading
            f.Show()
        Else
            modGlobalVar.msg(MsgCodes.noResultCancel)
        End If

    End Sub

    'send emails directly, import user emails; bypassing excel view. using Delivra 3/17
    Public Sub OpenEmailForm(ByRef sql As SqlCommand, ByVal sHeading As String, ByVal b As Boolean)
        Dim f As New frmMailEmail

        '1. fill tables
        modGlobalVar.LoadDataTable(tblSend, sql)
        If tblSend.Rows.Count > 0 Then
            SetupEmailTables()
            '2. open form for user to check emails and add subject and body
            f.Text = sHeading
            f.Show()
        Else
            modGlobalVar.msg(MsgCodes.noResultCancel)
        End If

    End Sub
    'MAIN frmMainOrg - from Contact, Case, Recommendation; need OrgName
    Public Sub OpenMainOrg(ByVal ID As Integer, ByVal ItemName As String) ', ByRef ds As DataSet, ByRef da As SqlClient.SqlDataAdapter, ByRef mi As MenuItem, ByVal tbl As String, ByVal param As SqlClient.SqlParameter)   'ByVal GotoName As String,)
        Dim newOrg As New frmMainOrg
        Dim strName As String
        Dim c As Integer 'count rows
        If ID > 0 Then
            'check not  deleted since opened search grid
            If CheckExists("Organization", "SELECT OrgID From tblOrg where OrgID = " & ID) = False Then
                Exit Sub
            End If
        Else
            Exit Sub
        End If

        Try
            ClassOpenForms.frmMainOrg.editOrgName.Focus() 'already open
            ' ClassOpenForms.frmMainOrg = ClassOpenForms.frmMainOrg
            ClassOpenForms.frmMainOrg.BringToFront()
            ClassOpenForms.frmMainOrg.WindowState = FormWindowState.Normal

            'ask user to save changes
            If ClassOpenForms.frmMainOrg.LookForChanges = True Then
                strName = UCase(ClassOpenForms.frmMainOrg.editOrgName.Text)
                Select Case modGlobalVar.msg("WAIT - confirm save changes?", "'" & strName & "' Detail must close." & NextLine & NextLine &
                                   "Click Yes to save changes." & NextLine &
                                   "Click No to discard changes" & NextLine &
                                   "Click Cancel to go back to '" & strName & "'", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
                    Case DialogResult.Yes
                        ClassOpenForms.frmMainOrg.DoUpdate() 'update current record before reloading
                    Case DialogResult.No 'reload without saving changes to current record
                    Case DialogResult.Cancel 'go back to current record without opening this new one
                        Exit Sub
                End Select
            End If

            'Try
            '    ClassOpenForms.frmMainOrg.DsMainOrg1.Clear()

            '    GoTo FillMainForm
            'Catch e2 As Exception
            '    modGlobalVar.msg("ERROR: can't bring to front", e2.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
            '    Exit Sub
            'End Try
        Catch ex As Exception
        End Try

FormNotOpen:  'show form, attach global variable
        Try
            newOrg.Show()
            ClassOpenForms.frmMainOrg = newOrg
        Catch xe As Exception
            modGlobalVar.msg("ERROR: can't open new", xe.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        '----------------------------
        'ClassOpenForms.frmMainOrg = frm
        '-----------------------------
FillMainForm:
        ClassOpenForms.frmMainOrg.ThisID = ID
        'ClassOpenForms.frmMainOrg.DsMainOrg1.Clear()
        ClassOpenForms.frmMainOrg.DsMainOrgWAddress1.Clear()
        '  ClassOpenForms.frmMainOrg.DsMainOrg1.EnforceConstraints = False
        ClassOpenForms.frmMainOrg.DsMainOrgWAddress1.EnforceConstraints = False

        '  ClassOpenForms.frmMainOrg.daMainOrg.SelectCommand.Parameters("@ID").Value = ID
        ClassOpenForms.frmMainOrg.daOrg2.SelectCommand.Parameters("@ID").Value = ID
        ClassOpenForms.frmMainOrg.daOrg2Address.SelectCommand.Parameters("@OrgID").Value = ID

        '....................

        Try
            cmdRegion.Parameters("@ID").Value = ID
            If SCConnect() Then
                'frm.fldRegion.BackColor = mdGlobalVar.RegionColor(IsNull(cmdRegion.ExecuteScalar, "none"))
                ClassOpenForms.frmMainOrg.pnlMain.BackColor = modGlobalVar.GetRegionColor(IsNull(cmdRegion.ExecuteScalar, "none"))
                ' ClassOpenForms.frmMainOrg.lblMailingAddress.BackColor = modGlobalVar.GetRegionColor(IsNull(cmdRegion.ExecuteScalar, "none"))
            End If
        Catch ex As Exception
            modGlobalVar.msg("ERROR: can't set back colour", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            sc.Close()
        End Try

        Try
            ClassOpenForms.frmMainOrg.ReLoad()
            '  ClassOpenForms.frmMainOrg.daMainOrg.Fill(ClassOpenForms.frmMainOrg.DsMainOrg1, "MainOrg")
            c = ClassOpenForms.frmMainOrg.daOrg2Address.Fill(ClassOpenForms.frmMainOrg.DsMainOrgWAddress1, "tblAddress1")
            ClassOpenForms.frmMainOrg.daOrg2.Fill(ClassOpenForms.frmMainOrg.DsMainOrgWAddress1, "tblOrg1")
            ClassOpenForms.frmMainOrg.GetHiddenStaffNames()
        Catch ex As Exception
            modGlobalVar.msg("ERROR: can't FILL mainorg " + ID.ToString, ex.Message & NextLine & ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        ClassOpenForms.frmMainOrg.Text = "ORGANIZATION DETAIL: " & ItemName
        'set visiblity of AddNewAddress button
        ClassOpenForms.frmMainOrg.btnNewPhysical.Visible = Not CType((c), Boolean)
        MouseDefault()
    End Sub

    'MAIN frmContact - from Conversation, Registration; need FullName, OrgName
    Public Sub OpenMainContact(ByVal ID As Integer, ByVal ItemName As String, ByVal GotoOrg As String, ByVal IDOrg As Integer) 'param name, ds name, da name, first control name for focus, form name, miSave name, tablename
        Dim newContact As New frmMainContact
        Dim strName As String
        If ID > 0 Then
            'check not  deleted since opened search grid
            If CheckExists("Contact", "SELECT ContactID From tblContact where ContactID = " & ID) = False Then
                Exit Sub
            End If
        Else
            Exit Sub
        End If

        Try
            ClassOpenForms.frmMainContact.txtFirstName.Focus() 'form already open
            ClassOpenForms.frmMainContact.BringToFront()
            ClassOpenForms.frmMainContact.WindowState = FormWindowState.Normal


            'ask user to save changes
            If ClassOpenForms.frmMainContact.LookForChanges = True Then
                strName = UCase(ClassOpenForms.frmMainContact.txtLastName.Text)
                Select Case modGlobalVar.msg("WAIT - confirm save changes?", "'" & strName & "' Detail must close." & NextLine & NextLine &
                                   "Click Yes to save changes." & NextLine &
                                   "Click No to discard changes" & NextLine &
                                   "Click Cancel to go back to '" & strName & "'", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
                    Case DialogResult.Yes
                        ClassOpenForms.frmMainContact.DoUpdate() 'update current record before reloading
                    Case DialogResult.No 'reload without saving changes to current record
                    Case DialogResult.Cancel 'go back to current record without opening this new one
                        Exit Sub
                End Select
            End If

            Try
                ClassOpenForms.frmMainContact.DsMainContact1.Clear()
                GoTo FillMainForm
            Catch e2 As Exception
                modGlobalVar.msg("ERROR: can't bring to front", e2.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
        Catch ex As Exception
        End Try

FormNotOpen:  'show form, attach global variable
        Try
            newContact.Show()
            ClassOpenForms.frmMainContact = newContact
        Catch ex As Exception
            modGlobalVar.msg("ERROR: couldn't open main contact", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try


FillMainForm:
        ClassOpenForms.frmMainContact.ThisID = ID
        ClassOpenForms.frmMainContact.LocalOrgID = IDOrg
        ClassOpenForms.frmMainContact.DsMainContact1.Clear()
        ClassOpenForms.frmMainContact.daMainContact.SelectCommand.Parameters("@ID").Value = ID
        ClassOpenForms.frmMainContact.DsMainContact1.EnforceConstraints = False
        '        ClassOpenForms.frmMainContact.isLoaded = False

        Try
            ClassOpenForms.frmMainContact.daMainContact.Fill(ClassOpenForms.frmMainContact.DsMainContact1, "MainContact")
        Catch ex As Exception
            modGlobalVar.msg("ERROR: can't FILL mainContact " + ID.ToString, ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Try
            ClassOpenForms.frmMainContact.Reload()
        Catch ex As Exception
            modGlobalVar.msg("ERROR:can't reload mainContact " + ID.ToString, ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try



SetHeadings:
        ClassOpenForms.frmMainContact.Text = "CONTACT DETAIL: " + ItemName
        ClassOpenForms.frmMainContact.fldGoToOrg.Text = GotoOrg

        'add regional colour
        If IDOrg > 0 Then
            cmdRegion.Parameters("@ID").Value = IDOrg
            If Not SCConnect() Then
                Exit Sub
            End If

            Try
                ClassOpenForms.frmMainContact.Panel1.BackColor = modGlobalVar.GetRegionColor(IsNull(cmdRegion.ExecuteScalar, "none"))
            Catch ex As Exception
                ' modGlobalVar.Msg("ERROR: setting contact color", "Org " & IDOrg.ToString & NextLine & ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                sc.Close()
            End Try
        Else
        End If
        Try
            ClassOpenForms.frmMainContact.chkRemovePostal.Checked = Not ClassOpenForms.frmMainContact.chkPostal.Checked
            ClassOpenForms.frmMainContact.chkRemoveEmail.Checked = Not ClassOpenForms.frmMainContact.chkEmail.Checked
            ClassOpenForms.frmMainContact.isLoaded = True
        Catch ex As Exception
            '  modGlobalVar.Msg("ERROR: reloading contact ", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        MouseDefault()
    End Sub    'contact

    'OPEN MAIN CONTACT as DIALOGUE to ADD NEW and UPDATE CALLING COMBO
    Public Sub OpenMainContact(ByVal ID As Integer, ByVal ItemName As String, ByVal GotoOrg As String, ByVal IDOrg As Integer, ByVal Dialg As Boolean) 'param name, ds name, da name, first control name for focus, form name, miSave name, tablename
        Dim newContact As New frmMainContact
        If ID > 0 Then
            'check not  deleted sincen opened search grid
            If CheckExists("Contact", "SELECT ContactID From tblContact where ContactID = " & ID) = False Then
                Exit Sub
            End If
        Else
            Exit Sub
        End If
        ClassOpenForms.frmMainContact = newContact
FillMainForm:
        ClassOpenForms.frmMainContact.isLoaded = False
        '        ClassOpenForms.frmMainContact.DsMainContact1.Clear()
        '        ClassOpenForms.frmMainContact.DsMainContact1.EnforceConstraints = False
        newContact.daMainContact.SelectCommand.Parameters("@ID").Value = ID
        newContact.daMainContact.SelectCommand.Connection = sc
        'ClassOpenForms.frmMainContact.iContact = ID
        If Not SCConnect() Then
            Exit Sub
        End If

        Try
            newContact.daMainContact.Fill(newContact.DsMainContact1, "MainContact")
        Catch ex As Exception
            modGlobalVar.msg("ERROR: can't FILL mainContact " + ID.ToString, ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
SetHeadings:
        newContact.Text = "ENTERING NEW CONTACT DETAIL: " + ItemName
        ' ClassOpenForms.frmMainConversation.strOrgName = GotoName
        newContact.fldGoToOrg.Text = GotoOrg

        'add regional colour
        If IDOrg > 0 Then
            cmdRegion.Parameters("@ID").Value = IDOrg
            Try
                newContact.Panel1.BackColor = modGlobalVar.GetRegionColor(IsNull(cmdRegion.ExecuteScalar, "none"))

            Catch ex As Exception
                ' modGlobalVar.Msg(ex.Message, , IDOrg.ToString)
            Finally
                sc.Close()
            End Try
        End If

        ClassOpenForms.frmMainContact.chkRemovePostal.Checked = Not ClassOpenForms.frmMainContact.chkPostal.Checked
        ClassOpenForms.frmMainContact.chkRemoveEmail.Checked = Not ClassOpenForms.frmMainContact.chkEmail.Checked
        ' ClassOpenForms.frmMainContact.Reload()
        ClassOpenForms.frmMainContact.isLoaded = True

        Try
            newContact.ShowDialog()
        Catch xe As Exception
            ' modGlobalVar.Msg(xe.Message, , "can't open new")
        End Try

        MouseDefault()
    End Sub 'contact dialog

    'MAIN frmCASE - from Conversation, Recommendation, Grant; need CaseName, OrgName
    Public Sub OpenMainCase(ByVal ID As Integer, ByVal ItemName As String, ByVal GotoOrg As String, ByVal IDOrg As Integer) ', ByVal bDialog As Boolean) 'param name, ds name, da name, first control name for focus, form name, miSave name, tablename
        Dim newCase As New frmMainCase
        If ID > 0 Then
            'check not  deleted sincen opened search grid
            If CheckExists("Case", "SELECT CaseID From tblCase where CaseID = " & ID) = False Then
                Exit Sub
            End If
        Else
            Exit Sub
        End If
        Try
            ClassOpenForms.frmMainCase.TxtCaseName.Focus() 'already open
            ClassOpenForms.frmMainCase.DoUpdate()
            Try
                ClassOpenForms.frmMainCase.BringToFront()
                ClassOpenForms.frmMainCase.WindowState = FormWindowState.Normal
                GoTo FillMainForm
            Catch e2 As Exception
                '   modGlobalVar.Msg("ERROR: can't bring to front", e2.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
        Catch e As Exception    'don't put error message here
        End Try
FormNotOpen:  'show form, attach global variable
        ClassOpenForms.frmMainCase = newCase
        Try
            newCase.Show()
            'End If
        Catch ex As Exception
            modGlobalVar.msg("ERROR: can't open case detail", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

FIllMainForm:
        ClassOpenForms.frmMainCase.isLoaded = False
        ClassOpenForms.frmMainCase.ThisID = ID
        ClassOpenForms.frmMainCase.LocalOrgID = IDOrg
        ClassOpenForms.frmMainCase.ReLoad()

        ClassOpenForms.frmMainCase.DsMainCase1.Clear()
        ClassOpenForms.frmMainCase.DsMainCase1.EnforceConstraints = False
        ClassOpenForms.frmMainCase.daspMainCase.SelectCommand.Parameters("@ID").Value = ID
        ClassOpenForms.frmMainCase.daspMainCase.SelectCommand.Connection = sc

        Try
            ClassOpenForms.frmMainCase.daspMainCase.Fill(ClassOpenForms.frmMainCase.DsMainCase1, "MainCase")
            '  MsgBox(ClassOpenForms.frmMainCase.DsMainCase1.MainCase.Rows.Count.ToString, , "filled")

        Catch ex As Exception
            modGlobalVar.msg("ERROR: can't FILL maincase 2 " + ID.ToString, ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        '[Continue]:
        'ADD REGIONAL COLOR
        cmdRegion.Parameters("@ID").Value = IDOrg
        If Not SCConnect() Then
            Exit Sub
        End If

        Try
            ClassOpenForms.frmMainCase.Panel1.BackColor = modGlobalVar.GetRegionColor(IsNull(cmdRegion.ExecuteScalar, "none"))
        Catch ex As Exception
        Finally
            sc.Close()
        End Try
        Try
            ClassOpenForms.frmMainCase.Text = "CASE DETAIL: " & ItemName
            ClassOpenForms.frmMainCase.fldGotoOrg.Text = GotoOrg
            ClassOpenForms.frmMainCase.isLoaded = True
        Catch ex As Exception
            modGlobalVar.msg("ERROR: finalize  open case detail", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        MouseDefault()

    End Sub

    'BARE BONES NEW MAIN CASE as DIALOGUE
    Public Sub OpenNewCase(ByVal i As Integer)
        Dim frm As New frmNewCase

        MouseWait()
LoadCombos:
        LoadCRGCombo(frm.cboCRG)
        LoadStaffCombo(frm.cboMgr, False, StaffComboChoices.Selectable)
        LoadCaseStatusCombo(frm.cboStatus, "Selectable") ', "Case")
        frm.daNewCase.SelectCommand.Connection = sc
        frm.daNewCase.SelectCommand.Parameters("@ID").Value = i
        frm.daNewCase.Fill(frm.DsNewCase1)
        frm.cboStatus.SelectedIndex = frm.cboStatus.FindStringExact("New")

        frm.ShowDialog()
        MouseDefault()

    End Sub

    'BARE BONES NEW MAIN RESOURCE as DIALOGUE
    Public Sub OpenNewResource(ByVal i As Integer)
        Dim frm As New frmNewResource

        frm.ThisID = i
        frm.ShowDialog()
        'open main form to complete edit
        OpenMainResource(i, frm.editResourceName.Text)
        frm = Nothing

    End Sub

    ' OPEN MAIN frmCONVERSATION - from CaseConversation, ContactConversations, srchContactsConversations; need OrgName, PersonName
    Public Sub OpenMainConversation(ByVal ID As Integer, ByVal ItemName As String, ByVal GotoOrg As String, ByVal OrgNum As Integer)
        Dim newConversation As New frmMainConversation
        If ID > 0 Then
            'check not  deleted sincen opened search grid
            If CheckExists("Conversation", "SELECT ConversID From tblConversation where ConversID = " & ID) = False Then
                Exit Sub
            End If
        Else
            Exit Sub
        End If
        Try
            ClassOpenForms.frmMainConversation.txtConverseDate.Focus() 'already open
            ClassOpenForms.frmMainConversation.DoUpdate()
            Try
                ClassOpenForms.frmMainConversation.BringToFront()
                ClassOpenForms.frmMainConversation.WindowState = FormWindowState.Normal

                GoTo FillMainForm
            Catch e2 As Exception
                '   modGlobalVar.Msg("ERROR: can't bring to front", e2.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
        Catch e As Exception    'don't put error message here
        End Try

FormNotOpen:  'show form, attach global variable
        Try
            ClassOpenForms.frmMainConversation = newConversation
            newConversation.Show()
        Catch ex As Exception
            modGlobalVar.msg("ERROR: can't open new conv", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

FIllMainForm:
        ClassOpenForms.frmMainConversation.isLoaded = False
        ClassOpenForms.frmMainConversation.ThisID = ID
        ClassOpenForms.frmMainConversation.LocalOrgID = OrgNum
        Try
            ClassOpenForms.frmMainConversation.Reload()
        Catch ex As Exception
            modGlobalVar.msg("ERROR: can't RELOAD main conversation " + ID.ToString, ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try


        ClassOpenForms.frmMainConversation.DsMainConversation1.Clear()
        ClassOpenForms.frmMainConversation.DsMainConversation1.EnforceConstraints = False
        ClassOpenForms.frmMainConversation.daspMainConversation.SelectCommand.Parameters("@ID").Value = ID
        ClassOpenForms.frmMainConversation.bAskNoContact = False
        Try
            ClassOpenForms.frmMainConversation.daspMainConversation.Fill(ClassOpenForms.frmMainConversation.DsMainConversation1, "MainConversation")
        Catch ex As Exception
            modGlobalVar.msg("ERROR: can't FILL main conversation " + ID.ToString, ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        ClassOpenForms.frmMainConversation.Text = "CONVERSATION DETAIL: " & ItemName
        ClassOpenForms.frmMainConversation.fldGotoOrg.Text = GotoOrg
        ClassOpenForms.frmMainConversation.isLoaded = True
        '========== 7/15 readonly if org or contact inactive ==== as


        MouseDefault()

    End Sub

    'FOR FRM CONVERSATION
    Public Function SubstrBriefSummary(ByVal str As String) As String
        Dim l As Int16
        l = Len(str)
        Select Case l
            Case Is > 20
                Return str.Substring(0, 19) & "..."
            Case Is > 1
                '                Return str.Substring(0, l)
                Return str
            Case Else
                Return ""
        End Select
    End Function

    'OPEN MAIN frmGrant - from frmMainOrg, SrchOrg, SrchCase; need OrgName
    Public Sub OpenMainGrant(ByVal ID As String, ByVal ItemName As String, ByVal GotoOrg As String, ByVal IDOrg As Integer) 'param name, ds name, da name, first control name for focus, form name, miSave name, tablename
        Dim newGrant As New frmMainGrant
        If Forms.find(newGrant) Then 'is already open
            Try
                ClassOpenForms.frmMainGrant.DoUpdate()
                Try
                    ClassOpenForms.frmMainGrant.BringToFront()
                    ClassOpenForms.frmMainGrant.WindowState = FormWindowState.Normal
                Catch e2 As Exception
                    Exit Sub
                End Try
            Catch e As Exception    'don't put error message here
            End Try
        Else
FormNotOpen:  'show form, attach global variable

            Try
                ClassOpenForms.frmMainGrant = newGrant
                newGrant.Show()
            Catch xe As Exception
                ' modGlobalVar.Msg(xe.Message, , "can't open new")
            End Try
        End If

FillMainForm:
        ClassOpenForms.frmMainGrant.localOrgID = IDOrg
        ClassOpenForms.frmMainGrant.ThisID = ID
        ClassOpenForms.frmMainGrant.isLoaded = False
        ClassOpenForms.frmMainGrant.DsMainGrant1.Clear()
        ClassOpenForms.frmMainGrant.Reload() 'needs to be here or comboboxes don't match
        ClassOpenForms.frmMainGrant.daspMainGrant.SelectCommand.Parameters("@Original_GrantIDTxt").Value = ID
        ClassOpenForms.frmMainGrant.DsMainGrant1.EnforceConstraints = False
        Try
            ClassOpenForms.frmMainGrant.daspMainGrant.Fill(ClassOpenForms.frmMainGrant.DsMainGrant1, "MainGrant")
            ClassOpenForms.frmMainGrant.GrantID = ClassOpenForms.frmMainGrant.DsMainGrant1.MainGrant.Rows(0)("GrantID")
            ClassOpenForms.frmMainGrant.FindFiles()
        Catch ex As Exception
            modGlobalVar.msg("ERROR: can't FILL mainGrant " + ID, ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try


        'Try
        '    ClassOpenForms.frmMainGrant.Reload()
        'Catch ex As Exception
        '    modGlobalVar.Msg(ex.Message, MessageBoxIcon.Error, "can't load OrgBasedCombos")
        'End Try
SetHeadings:
        Try
            ClassOpenForms.frmMainGrant.Text = "GRANT DETAIL @: " + ItemName
            ClassOpenForms.frmMainGrant.fldGotoOrg.Text = GotoOrg
            ClassOpenForms.frmMainGrant.isLoaded = True
        Catch ex As Exception
        End Try
SetFlag:
        Try
            If ClassOpenForms.frmMainGrant.DsMainGrant1.MainGrant.Rows(0).Item("EIN") = 0 And
                (ClassOpenForms.frmMainGrant.DsMainGrant1.MainGrant.Rows(0).Item("CheckMailedDate").ToString > "" Or
                 ClassOpenForms.frmMainGrant.DsMainGrant1.MainGrant.Rows(0).Item("CheckNum").ToString > "0") Then
                ClassOpenForms.frmMainGrant.flagEIN.Visible = True
            Else
                ClassOpenForms.frmMainGrant.flagEIN.Visible = False
            End If
        Catch ex As Exception
            modGlobalVar.msg("ERROR: finalize grant open", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        MouseDefault()
    End Sub

    'OPEN GRANT DETAIL or MGI APPLICATION DETAIL
    Public Sub OpenMGIForm(ByVal id As Integer, ByVal strMGIName As String, ByVal strOrgGoTo As String, ByVal orgnum As Integer)
        Dim iorgid As Integer = IsNull(orgnum, 0)
        Select Case strMGIName
            Case Is = "CMG ", "Communty Ministry Grant"
                modGlobalVar.OpenMainCMG(id, strOrgGoTo, iorgid)
            Case Is = "YMGI", "Youth Ministry Grant"
                modGlobalVar.OpenMainYMGI(id, strOrgGoTo, iorgid) 'Me.grdvwMain.Item(2, Me.grdvwMain.CurrentRow.Index).Value & " : " & Me.grdvwMain.Item(6, Me.grdMain.CurrentRowIndex).Value, Me.grdMain.Item(1, Me.grdvwMain.CurrentRow.Index).value)
            Case Is = "TGI ", "Technology Grant"
                modGlobalVar.OpenMainTGI(id, strOrgGoTo, iorgid)
            Case Is = "TMGI", "Technology Ministry Grant"
                modGlobalVar.OpenMainLTGI(id, strOrgGoTo, iorgid)
            Case Is = "LTGI", "Life Together Implementation Grant", "Life Together Planning Grant"
                modGlobalVar.OpenMainLTGI(id, strOrgGoTo, iorgid)
            Case Else
                modGlobalVar.msg("Sorry, archived data not available",
                                 "Sacred Space and Computers & Ministry Application details not available." & NextLine &
                                 "see " + DBAdmin.StaffName + " if you need the MGI application details.", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Select
    End Sub

    'OPEN MAIN frmMainGrantLTGI - from SrchGrant, mainOrg, mainCase?, mainGrant?; need OrgName
    Public Sub OpenMainLTGI(ByVal ID As Integer, ByVal GotoOrg As String, ByVal IDOrg As Integer)    ', ByVal ItemName As String
        Dim newMGI As New frmMainGrantLTGI
        If Forms.find(newMGI) Then 'is already open
            Try
                ClassOpenForms.frmMainGrantLTGI.UpdateDB("minimize")
                Try
                    ClassOpenForms.frmMainGrantLTGI.BringToFront()
                    ClassOpenForms.frmMainGrantLTGI.WindowState = FormWindowState.Normal
                    ClassOpenForms.frmMainGrantLTGI.LoadOrgBasedCombos(IDOrg)
                    'ClassOpenForms.frmMainMGI.LoadOrgBasedCombos(IDOrg)    ''that are based on current record
                Catch e2 As Exception
                    '   modGlobalVar.Msg("ERROR: can't bring to front", e2.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End Try
            Catch e As Exception    'don't put error message here
            End Try
        Else
FormNotOpen:  'show form, attach global variable
            Try
                ClassOpenForms.frmMainGrantLTGI = newMGI
                newMGI.Show()
            Catch xe As Exception
                ' modGlobalVar.Msg(xe.Message, , "can't open new")
            End Try
        End If

FillMainForm:
        ClassOpenForms.frmMainGrantLTGI.isLoaded = False
        ClassOpenForms.frmMainGrantLTGI.dsMainGrantLTGI1.Clear()
        ClassOpenForms.frmMainGrantLTGI.LoadOrgBasedCombos(IDOrg)
        ClassOpenForms.frmMainGrantLTGI.dsMainGrantLTGI1.EnforceConstraints = False
        ClassOpenForms.frmMainGrantLTGI.daspMainLTGI.SelectCommand.Parameters("@ID").Value = ID
        Try
            ClassOpenForms.frmMainGrantLTGI.daspMainLTGI.Fill(ClassOpenForms.frmMainGrantLTGI.dsMainGrantLTGI1, "MainGrantLTGI")
        Catch ex As Exception
            modGlobalVar.msg("ERROR: can't FILL mainMGI " + ID.ToString, ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

SetHeadings:

        Try
            '   Dim da As SqlDataAdapter = New SqlDataAdapter
            Dim cmd As New SqlCommand("SELECT ConsultantName FROM luConsultant WHERE Type = 'LTGI Spring Consultant'", sc)
            Dim ds As DataSet = New DataSet()
            Dim drdr As SqlDataReader = cmd.ExecuteReader()
            Try
                ds.Tables(0).Load(drdr)
                drdr.Close()
                '  da.Fill(ds)
            Catch ex As Exception
                modGlobalVar.msg("ERROR: can't fill consultant combo", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
            ClassOpenForms.frmMainGrantLTGI.cboOnsiteConsultant.DisplayMember = "ConsultantName"
            ClassOpenForms.frmMainGrantLTGI.cboOnsiteConsultant.DataSource = ds.Tables(0)
            '  da.Dispose()
        Catch ex As Exception

        End Try
        Try
            ClassOpenForms.frmMainGrantLTGI.Text = "MAJOR GRANT LT INITIATIVE DETAIL @ : " + GotoOrg
            '  ClassOpenForms.frmMainGrantLTGI.iOrg = IDOrg
            ClassOpenForms.frmMainGrantLTGI.fldGotoOrg.Text = GotoOrg
            ClassOpenForms.frmMainGrantLTGI.isLoaded = True
            ' CallingForm.StatusBar1.Text = "Done"
        Catch ex As Exception
        End Try
        cmdRegion = Nothing
        MouseDefault()
    End Sub

    'TGI
    'OPEN MAIN frmMainTMGI - from SrchGrant, mainOrg, mainCase?, mainGrant?; need OrgName
    Public Sub OpenMainTGI(ByVal ID As Integer, ByVal GotoOrg As String, ByVal IDOrg As Integer)    ', ByVal ItemName As String
        Dim newTGI As New frmMainGrantTGI
        'modGlobalVar.Msg(ID.ToString, , ItemName)
        'modGlobalVar.Msg(GotoOrg, , IDOrg.ToString)
        'strGrantID = ID

        If Forms.find(newTGI) Then 'is already open
            Try
                ClassOpenForms.frmMainTGI.UpdateDB("minimize")
                Try
                    ClassOpenForms.frmMainTGI.BringToFront()
                    ClassOpenForms.frmMainTGI.WindowState = FormWindowState.Normal
                    ' ClassOpenForms.frmMainTGI.LoadOrgBasedCombos(IDOrg)
                    'ClassOpenForms.frmMainMGI.LoadOrgBasedCombos(IDOrg)    ''that are based on current record
                Catch e2 As Exception
                    '   modGlobalVar.Msg("ERROR: can't bring to front", e2.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End Try
            Catch e As Exception    'don't put error message here
            End Try
        Else
FormNotOpen:  'show form, attach global variable
            Try
                ClassOpenForms.frmMainTGI = newTGI
                newTGI.Show()
            Catch xe As Exception
                ' modGlobalVar.Msg(xe.Message, , "can't open new")
            End Try
        End If

FillMainForm:
        ClassOpenForms.frmMainTGI.isLoaded = False
        'ClassOpenForms.frmMainTGI.dsMainGrantLTGI1.Clear()

        ClassOpenForms.frmMainTGI.DsMainGrantTGI.EnforceConstraints = False
        ' ClassOpenForms.frmMainGrantLTGI.daspMainLTGI.SelectCommand.Parameters("@ID").Value = ID
        Try
            ClassOpenForms.frmMainTGI.MainGrantTGITableAdapter.Fill(ClassOpenForms.frmMainTGI.DsMainGrantTGI.MainGrantTGI, ID)
            ' ClassOpenForms.frmMainTGI.daspMainLTGI.Fill(ClassOpenForms.frmMainGrantLTGI.dsMainGrantLTGI1.MainGrantLTGI)
            ClassOpenForms.frmMainTGI.LoadOrgBasedCombos(IDOrg)
        Catch ex As Exception
            modGlobalVar.msg("ERROR: can't FILL mainTGI " + ID.ToString, ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        'If IsNull(ClassOpenForms.frmMainTGI.DsMainGrantTGI1.tblGrantTGI.Rows(0).Item("GrantIDTxt"), "") > "" Then
        '    ClassOpenForms.frmMainTGI.btnGrant.Text = "Go to Grant"
        'Else
        '    ClassOpenForms.frmMainTGI.btnGrant.Text = "Create Grant"
        'End If

SetHeadings:

        Try
            ClassOpenForms.frmMainTGI.Text = "TECHNOLOGY MAJOR GRANT INITIATIVE DETAIL @ : " + GotoOrg
            '  ClassOpenForms.frmMainGrantLTGI.iOrg = IDOrg
            ClassOpenForms.frmMainTGI.fldGotoOrg.Text = GotoOrg
            ClassOpenForms.frmMainTGI.isLoaded = True

            'If IsNull(ClassOpenForms.frmMainTGI.DsMainGrantTGI.MainGrantTGI.Rows(0).Item("GrantIDTxt"), "") > "" Then
            '    ClassOpenForms.frmMainTGI.btnGrant.Visible = True
            'Else
            '    ClassOpenForms.frmMainTGI.btnGrant.Visible = False
            'End If

            ' CallingForm.StatusBar1.Text = "Done"
        Catch ex As Exception
            ' modGlobalVar.Msg(ex.Message)
        End Try
        MouseDefault()
    End Sub

    'TMIG2
    'OPEN MAIN frmMainTMGI - from SrchGrant, mainOrg, mainCase?, mainGrant?; need OrgName
    Public Sub OpenMainTMGI(ByVal ID As Integer, ByVal GotoOrg As String, ByVal IDOrg As Integer)    ', ByVal ItemName As String
        Dim newTMGI As New frmMainGrantTMGI
        'modGlobalVar.Msg(ID.ToString, , ItemName)
        'modGlobalVar.Msg(GotoOrg, , IDOrg.ToString)
        'strGrantID = ID

        If Forms.find(newTMGI) Then 'is already open
            Try
                ClassOpenForms.frmMainTMGI.UpdateDB("minimize")
                Try
                    ClassOpenForms.frmMainTMGI.BringToFront()
                    ClassOpenForms.frmMainTMGI.WindowState = FormWindowState.Normal
                    ' ClassOpenForms.frmMainTMGI.LoadOrgBasedCombos(IDOrg)
                    'ClassOpenForms.frmMainMGI.LoadOrgBasedCombos(IDOrg)    ''that are based on current record
                Catch e2 As Exception
                    '   modGlobalVar.Msg("ERROR: can't bring to front", e2.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End Try
            Catch e As Exception    'don't put error message here
            End Try
        Else
FormNotOpen:  'show form, attach global variable
            Try
                ClassOpenForms.frmMainTMGI = newTMGI
                newTMGI.Show()
            Catch xe As Exception
                ' modGlobalVar.Msg(xe.Message, , "can't open new")
            End Try
        End If

FillMainForm:
        ClassOpenForms.frmMainTMGI.isLoaded = False
        'ClassOpenForms.frmMainTMGI.dsMainGrantLTGI1.Clear()

        ClassOpenForms.frmMainTMGI.DsMainGrantTMGI.EnforceConstraints = False
        ' ClassOpenForms.frmMainGrantLTGI.daspMainLTGI.SelectCommand.Parameters("@ID").Value = ID
        Try
            ClassOpenForms.frmMainTMGI.LoadOrgBasedCombos(IDOrg)
            ClassOpenForms.frmMainTMGI.tblGrantTMGITableAdapter.Fill(ClassOpenForms.frmMainTMGI.DsMainGrantTMGI.tblGrantTMGI, ID)
            ' ClassOpenForms.frmMainTMGI.daspMainLTGI.Fill(ClassOpenForms.frmMainGrantLTGI.dsMainGrantLTGI1.MainGrantLTGI)
            ClassOpenForms.frmMainTMGI.GrantButtonTxt()
        Catch ex As Exception
            modGlobalVar.msg("ERROR: can't FILL mainTMGI " + ID.ToString, ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        'If IsNull(ClassOpenForms.frmMainTMGI.DsMainGrantTMGI1.tblGrantTMGI.Rows(0).Item("GrantIDTxt"), "") > "" Then
        '    ClassOpenForms.frmMainTMGI.btnGrant.Text = "Go to Grant"
        'Else
        '    ClassOpenForms.frmMainTMGI.btnGrant.Text = "Create Grant"
        'End If

SetHeadings:

        Try
            ClassOpenForms.frmMainTMGI.Text = "TECHNOLOGY & MINISTRY MAJOR GRANT INITIATIVE DETAIL @ : " + GotoOrg
            '  ClassOpenForms.frmMainGrantLTGI.iOrg = IDOrg
            ClassOpenForms.frmMainTMGI.fldGotoOrg.Text = GotoOrg
            ClassOpenForms.frmMainTMGI.isLoaded = True
            'put in LoadOrgBasedCombos
            'If IsNull(ClassOpenForms.frmMainTMGI.DsMainGrantTMGI.tblGrantTMGI.Rows(0).Item("GrantIDTxt"), "") > "" Then
            '    ClassOpenForms.frmMainTMGI.btnGrant.Visible = True
            'Else
            '    ClassOpenForms.frmMainTMGI.btnGrant.Visible = False
            'End If

            ' CallingForm.StatusBar1.Text = "Done"
        Catch ex As Exception
            ' modGlobalVar.Msg(ex.Message)
        End Try
        MouseDefault()
    End Sub

    'OPEN MAIN YMGI
    Public Sub OpenMainYMGI(ByVal ID As Integer, ByVal GotoOrg As String, ByVal IDOrg As Integer)    ', ByVal ItemName As String
        Dim newYMGI As New frmMainGrantYMGI

        If ID < 56 Then
            modGlobalVar.msg("ARCHIVED DATA", "that first round YMGI application data is archived." + NextLine +
                    "see " + DBAdmin.StaffName + " if you need details", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        If Forms.find(newYMGI) Then 'is already open
            Try
                ClassOpenForms.frmMainYMGI.DoUpdate() '("minimize")
                Try
                    ClassOpenForms.frmMainYMGI.BringToFront()
                    ClassOpenForms.frmMainYMGI.WindowState = FormWindowState.Normal
                    ' ClassOpenForms.frmMainYMGI.LoadOrgBasedCombos(IDOrg)
                    'ClassOpenForms.frmMainMGI.LoadOrgBasedCombos(IDOrg)    ''that are based on current record
                Catch e2 As Exception
                    '   modGlobalVar.Msg("ERROR: can't bring to front", e2.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End Try
            Catch e As Exception    'don't put error message here
            End Try
        Else
FormNotOpen:  'show form, attach global variable
            Try
                ClassOpenForms.frmMainYMGI = newYMGI
                newYMGI.Show()
            Catch xe As Exception
                ' modGlobalVar.Msg(xe.Message, , "can't open new")
            End Try
        End If

FillMainForm:
        '   ClassOpenForms.frmMainYMGI.isLoaded = False
        ClassOpenForms.frmMainYMGI.ThisID = ID
        ClassOpenForms.frmMainYMGI.DsMainGrantYMGI.EnforceConstraints = False
        Try
            ClassOpenForms.frmMainYMGI.LoadOrgBasedCombos(IDOrg)
        Catch ex As Exception
            modGlobalVar.msg("ERROR: can't FILL YMGI LoadOrgbased Org#:" + IDOrg.ToString, ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Try
            ClassOpenForms.frmMainYMGI.tblGrantYMGITableAdapter.Fill(ClassOpenForms.frmMainYMGI.DsMainGrantYMGI.tblGrantYMGI, ID)
        Catch ex As Exception
            modGlobalVar.msg("ERROR: can't FILL YMGI GI#:" + ID.ToString, ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Try
            ClassOpenForms.frmMainYMGI.GrantButtonVisible()
        Catch ex As Exception
            modGlobalVar.msg("ERROR: YMGI grant button :", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        'If IsNull(ClassOpenForms.frmMainYMGI.DsMainGrantYMGI1.tblGrantYMGI.Rows(0).Item("GrantIDTxt"), "") > "" Then
        '    ClassOpenForms.frmMainYMGI.btnGrant.Text = "Go to Grant"
        'Else
        '    ClassOpenForms.frmMainYMGI.btnGrant.Text = "Create Grant"
        'End If

SetHeadings:

        Try
            ClassOpenForms.frmMainYMGI.Text = "YOUTH MINISTRY MAJOR GRANT INITIATIVE DETAIL @ : " + GotoOrg
            ClassOpenForms.frmMainYMGI.fldGotoOrg.Text = GotoOrg
            ClassOpenForms.frmMainYMGI.ThisID = ID
            ' ClassOpenForms.frmMainYMGI.isLoaded = True
            'put in LoadOrgBasedCombos
            'If IsNull(ClassOpenForms.frmMainYMGI.DsMainGrantYMGI.tblGrantYMGI.Rows(0).Item("GrantIDTxt"), "") > "" Then
            '    ClassOpenForms.frmMainYMGI.btnGrant.Visible = True
            'Else
            '    ClassOpenForms.frmMainYMGI.btnGrant.Visible = False
            'End If
            ' CallingForm.StatusBar1.Text = "Done"
        Catch ex As Exception
            ' modGlobalVar.Msg(ex.Message)
        End Try


        If ClassOpenForms.frmMainYMGI.dtGrantDeadline.Text = String.Empty Then
        Else
            If CType(ClassOpenForms.frmMainYMGI.dtGrantDeadline.Text, Date).ToShortDateString = CType(ClassOpenForms.frmMainYMGI.rbJan.Text, Date).ToShortDateString Then
                ClassOpenForms.frmMainYMGI.rbJan.Checked = True
            Else
                If CType(ClassOpenForms.frmMainYMGI.dtGrantDeadline.Text, Date).ToShortDateString = CType(ClassOpenForms.frmMainYMGI.rbMarch.Text, Date).ToShortDateString Then
                    ClassOpenForms.frmMainYMGI.rbMarch.Checked = True
                End If
            End If
        End If


        MouseDefault()
    End Sub

    'OPEN MAIN CMG
    Public Sub OpenMainCMG(ByVal ID As Integer, ByVal GotoOrg As String, ByVal IDOrg As Integer)    ', ByVal ItemName As String
        Dim newCMG As New frmMainGrantCMG

        'in cases are multiple seperate rounds of the mgi
        'If ID < 56 Then
        '    modGlobalVar.msg("ARCHIVED DATA", "that first round CMG application data is archived." + NextLine +
        '            "see " + DBAdmin.StaffName + " if you need details", MessageBoxButtons.OK, MessageBoxIcon.Information)
        '    Exit Sub
        'End If

        If Forms.find(newCMG) Then 'is already open
            Try
                ClassOpenForms.frmMainCMG.DoUpdate() '("minimize")
                Try
                    ClassOpenForms.frmMainCMG.BringToFront()
                    ClassOpenForms.frmMainCMG.WindowState = FormWindowState.Normal
                    ' ClassOpenForms.frmMainCMG.LoadOrgBasedCombos(IDOrg)
                    'ClassOpenForms.frmMainMGI.LoadOrgBasedCombos(IDOrg)    ''that are based on current record
                Catch e2 As Exception
                    '   modGlobalVar.Msg("ERROR: can't bring to front", e2.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End Try
            Catch e As Exception    'don't put error message here
            End Try
        Else
FormNotOpen:  'show form, attach global variable
            Try
                ClassOpenForms.frmMainCMG = newCMG
                newCMG.Show()
            Catch xe As Exception
                ' modGlobalVar.Msg(xe.Message, , "can't open new")
            End Try
        End If

FillMainForm:
        '   ClassOpenForms.frmMainCMG.isLoaded = False
        ClassOpenForms.frmMainCMG.ThisID = ID
        ClassOpenForms.frmMainCMG.LocalOrgID = IDOrg
        ClassOpenForms.frmMainCMG.DsMainGrantCMG.EnforceConstraints = False


        Try
            ClassOpenForms.frmMainCMG.LoadOrgBasedCombos(IDOrg)
        Catch ex As Exception
            modGlobalVar.msg("ERROR: can't FILL CMG LoadOrgbased Org#: " + IDOrg.ToString, ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Try
            ClassOpenForms.frmMainCMG.MainGrantCMGTableAdapter.Fill(ClassOpenForms.frmMainCMG.DsMainGrantCMG.MainGrantCMG, ID)
        Catch ex As Exception
            modGlobalVar.msg("ERROR: can't FILL CMG GI#:" + ID.ToString, ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Try
            ClassOpenForms.frmMainCMG.GrantButtonVisible()
        Catch ex As Exception
            modGlobalVar.msg("ERROR: CMG grant button :", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

SetHeadings:

        Try
            ClassOpenForms.frmMainCMG.Text = "COMMUNITY MINISTRY MAJOR GRANT INITIATIVE DETAIL @ : " + GotoOrg
            ClassOpenForms.frmMainCMG.fldGotoOrg.Text = GotoOrg
            ClassOpenForms.frmMainCMG.ThisID = ID
            ClassOpenForms.frmMainCMG.GetCaseMgr()
        Catch ex As Exception
            ' modGlobalVar.Msg(ex.Message)
        End Try


        If ClassOpenForms.frmMainCMG.dtGrantDeadline.Text = String.Empty Then
        Else
            If CType(ClassOpenForms.frmMainCMG.dtGrantDeadline.Text, Date).ToShortDateString = CType(ClassOpenForms.frmMainCMG.rbGroup1.Tag, Date).ToShortDateString Then
                ClassOpenForms.frmMainCMG.rbGroup1.Checked = True
            Else
                ClassOpenForms.frmMainCMG.rbGroup2.Checked = True
            End If
        End If
        ClassOpenForms.frmMainCMG.isLoaded = True

        MouseDefault()
    End Sub

    'SRCH W EVENTS - do here so can reload combo boxes if form not open
    Public Sub OpenSrchWEvent(ByVal ID As Integer, ByVal bGotoOrder As Boolean) ', ByRef ds As DataSet, ByRef da As SqlClient.SqlDataAdapter, ByRef mi As MenuItem, ByVal tbl As String, ByVal param As SqlClient.SqlParameter)   'ByVal GotoName As String,)
        MouseWait()
        If Forms.find(frmSrchEvent, True) = True Then
            If bGotoOrder = True Then
                frmSrchEvent.TabControl1.SelectedTab = frmSrchEvent.pgOrder
                frmSrchEvent.txtSrchOrder.Text = ID
                frmSrchEvent.btnSrchOrder.PerformClick()
            Else
                ' frmSrchEvent.TabControl1.SelectedTab = frmSrchEvent.pgEvent
            End If
        End If
        MouseDefault()

    End Sub

    'OPEN MAIN frmMainWorkshopEvent - from SrchEdEvent
    Public Sub OpenMainWEvent(ByVal ID As String, ByVal ItemName As String, ByVal bDialog As Boolean) 'param name, ds name, da name, first control name for focus, form name, miSave name, tablename
        Dim newEvent As New frmMainWEvent

        'iEventID = ID
        If Forms.find(newEvent) = True Then 'is already open
            Try
                ClassOpenForms.frmMainWEvent.DoUpdate()
                Try
                    ClassOpenForms.frmMainWEvent.BringToFront()
                    ClassOpenForms.frmMainWEvent.WindowState = FormWindowState.Normal
                    'ClassOpenForms.frmMainEdEvent.LoadOrgBasedCombos(IDOrg)    ''that are based on current record
                Catch e2 As Exception
                    '   modGlobalVar.Msg("ERROR: can't bring to front", e2.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End Try
            Catch e As Exception    'don't put error message here
            End Try
            Try
                ClassOpenForms.frmMainWEvent.dsMainWEvent.Clear()
                ClassOpenForms.frmMainWEvent.dsMainWEvent.EnforceConstraints = False
                '  frmMainWEvent.dsGetEventRegList2a.EnforceConstraints = False
            Catch ex As Exception
                modGlobalVar.msg("ERROR: can't clear " + ID.ToString, ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        Else
FormNotOpen:  'show form, attach global variable
            Try
                ClassOpenForms.frmMainWEvent = newEvent
                newEvent.Show()
            Catch ex As Exception
                modGlobalVar.msg("ERROR: in MainEvent open module", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
        'modGlobalVar.Msg("ok1")
FillMainForm:
        ClassOpenForms.frmMainWEvent.isLoaded = False

        Try
            ' ClassOpenForms.frmMainWEvent.getEventRegistrantList2TableAdapter.Fill(ClassOpenForms.frmMainWEvent.dsGetEventRegList2a.getEventRegistrantList2, ID, "Event")
            ClassOpenForms.frmMainWEvent.MainEventSetupTableAdapter.Fill(ClassOpenForms.frmMainWEvent.dsMainWEvent.MainEventSetup, ID)
        Catch ex As Exception
            modGlobalVar.msg("ERROR: can't FILL mainWEvent " + ID.ToString, ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        ClassOpenForms.frmMainWEvent.dgvChecklist.Columns.Clear()
        ClassOpenForms.frmMainWEvent.isLoaded = True


SetHeadings:
        Try
            ClassOpenForms.frmMainWEvent.Text = "W. EVENT DETAIL: " + ItemName
            ' ClassOpenForms.frmMainGrant.fldGotoOrg.Text = GotoOrg
            ClassOpenForms.frmMainWEvent.ThisID = ID
            ClassOpenForms.frmMainWEvent.Reload()

            'If bDialog = True Then
            '    LoadEventTypeCombo(ClassOpenForms.frmMainWEvent.cboType, False)
            'Else
            '    LoadEventTypeCombo(ClassOpenForms.frmMainWEvent.cboType, True)
            'End If
            ' CallingForm.StatusBar1.Text = "Done"
        Catch ex As Exception
            '  modGlobalVar.Msg(ex.Message, , "Error: finding file")
        End Try
        'modGlobalVar.Msg("ok4")
        '  modGlobalVar.Msg(sc.State.ToString, , "out of globalvar")
        MouseDefault()
    End Sub

    'OPEN MAIN frm REGISTRATION 2
    Public Sub OpenMainWReg2(ByVal ID As Integer, ByVal strHeading As String, ByVal bShowCombo As Boolean, ByVal strWhich As String)    ', ByVal ItemName As String

        If ID > 0 Then
            'check not  deleted sincen opened search grid
            If CheckExists("Registration", "SELECT RegistrationID From tblEventReg2 where RegistrationID = " & ID) = False Then
                Exit Sub
            End If
        Else
            Exit Sub
        End If

        Dim newReg As New frmMainWReg2

        'modGlobalVar.Msg("registrations are temporarily under construction", , "cancelling request")
        'Exit Sub
        If Forms.find(newReg) Then 'is already open
            Try
                ClassOpenForms.frmMainWReg2.UpdateDB()
                Try
                    ClassOpenForms.frmMainWReg2.BringToFront()
                    ClassOpenForms.frmMainWReg2.WindowState = FormWindowState.Normal
                Catch e2 As Exception
                    '   modGlobalVar.Msg("ERROR: can't bring to front", e2.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End Try
            Catch e As Exception    'don't put error message here
            End Try
        Else
FormNotOpen:  'show form, attach global variable
            Try
                ClassOpenForms.frmMainWReg2 = newReg
                newReg.Show()
            Catch ex As Exception
                modGlobalVar.msg("ERROR: can't open Main Registration Detail", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
FillMainForm:
        ClassOpenForms.frmMainWReg2.ThisID = ID
        ClassOpenForms.frmMainWReg2.dsMainEventReg2.EnforceConstraints = False
        ClassOpenForms.frmMainWReg2.dsMainEventReg2.Clear()

        Try
            ClassOpenForms.frmMainWReg2.MainEventReg2TableAdapter1.Fill(ClassOpenForms.frmMainWReg2.dsMainEventReg2.MainEventReg2, ID, strWhich)
            ClassOpenForms.frmMainWReg2.ReLoad()
        Catch ex As Exception
            modGlobalVar.msg("ERROR: can't FILL Main Registration " + ID.ToString, ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try


SetHeadings:
        ''   ClassOpenForms.frmMainWReg2.cboEvent.Visible = bShowCombo
        'Try
        '    ClassOpenForms.frmMainWReg2.cboEvent.SelectedIndex = -1
        '    ClassOpenForms.frmMainWReg2.isLoaded = True
        'Catch ex As Exception
        'End Try
        MouseDefault()

    End Sub

    'OPEN NEW frm ORDER
    Public Sub OpenNewWOrder2(ByVal ID As Integer, ByVal strHeading As String, ByVal bShowEvent As Boolean, ByVal bShowPayee As Boolean)    ', ByVal ItemName As String
        Dim newReg As New frmNewWOrder2

        If Forms.find(newReg) Then 'is already open
            Try
                ClassOpenForms.frmNewWOrder.UpdateDB("minimize")
                Try
                    ClassOpenForms.frmNewWOrder.BringToFront()
                    ClassOpenForms.frmNewWOrder.WindowState = FormWindowState.Normal
                Catch e2 As Exception
                    '   modGlobalVar.Msg("ERROR: can't bring to front", e2.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End Try
            Catch e As Exception    'don't put error message here
            End Try
        Else
FormNotOpen:  'show form, attach global variable
            Try
                ClassOpenForms.frmNewWOrder = newReg
                newReg.Show()
            Catch ex As Exception
                modGlobalVar.msg("ERROR can't open new order", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
        'modGlobalVar.Msg(ID.ToString)
FillMainForm:
        ' ClassOpenForms.frmMainRegistration.isLoaded = False
        '   ClassOpenForms.frmNewWOrder.WorkshopDDTableAdapter.Clear()
        ClassOpenForms.frmNewWOrder.DsNewWRegistration.EnforceConstraints = False

        Try
            ' ClassOpenForms.frmNewWOrder.tblEventDDTableAdapter.Fill(ClassOpenForms.frmNewWOrder.DsNewWRegistration.tblEventDD)
            If bShowPayee = True Then   'is new reg, use RegID
                ClassOpenForms.frmNewWOrder.TblNewWRegistrationTableAdapter.FillbyRegID(ClassOpenForms.frmNewWOrder.DsNewWRegistration.tblNewWRegistration, ID)
            Else    'is editing, use Order id
                ClassOpenForms.frmNewWOrder.TblNewWRegistrationTableAdapter.FillByOrderID(ClassOpenForms.frmNewWOrder.DsNewWRegistration.tblNewWRegistration, ID)
                '  modGlobalVar.Msg(ClassOpenForms.frmNewWOrder.DsNewWRegistration.tblNewWRegistration.Rows.Count.ToString, , "registrations found")
            End If
        Catch ex As Exception
            modGlobalVar.msg("ERROR: can't FILL new W Registration Order " + ID.ToString, ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

SetHeadings:
        Try
            newReg.Text = strHeading   '"NEW W REGISTRATION ORDER DETAIL : " +
            newReg.cboEvent.Visible = bShowEvent
            newReg.lblevent.Visible = bShowEvent
            newReg.Reload(bShowPayee)
        Catch ex As Exception
        End Try
        MouseDefault()
    End Sub

    'OPEN PAYMENT DETAIL
    Public Sub OpenMainWPayment(ByVal id As Integer)
        Dim frm As New frmMainWPayment

        If Forms.find(frm) Then 'is already open
            Try
                ClassOpenForms.frmMainWPayment.UpdateDB("minimize")
                Try
                    ClassOpenForms.frmMainWPayment.BringToFront()
                    ClassOpenForms.frmMainWPayment.WindowState = FormWindowState.Normal
                Catch e2 As Exception
                    '   modGlobalVar.Msg("ERROR: can't bring to front", e2.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End Try
            Catch e As Exception    'don't put error message here
            End Try
        Else
FormNotOpen:  'show form, attach global variable
            Try
                ClassOpenForms.frmMainWPayment = frm
                '  frm.Show()
            Catch ex As Exception
                modGlobalVar.msg("ERROR: can't open new payment", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
        Try
            frm.OnCurrent(id)
        Catch ex As System.Exception
            ' modGlobalVar.Msg(ex.Message, , "ERROR:   ")
        End Try
        Try
            'frm.Show()'try as dialog to force refresh of eventsearch
            frm.ShowDialog()
        Catch ex As System.Exception
            ' modGlobalVar.Msg(ex.Message, , "ERROR:   ")
        End Try

    End Sub

    'RESTRICT PERMISSIONS ON CRG RESOURCES by opening different forms
    Public Sub OpenResourceChoice(ByVal ID As Integer, ByVal ItemName As String)
        If ItemName = "NewResource" Or StaffCRGFull.Contains(usr) Or StaffCRGEdit.Contains(usr) Then ''use full resource detail form
            GoTo OpenMain
        End If

        Dim sql As New SqlCommand("SELECT NewCRG from TblResource WHERE ICCResourceID = " & ID, sc)
        If Not SCConnect() Then
            Exit Sub
        End If
        Try
            If (sql.ExecuteScalar.ToString) = True Then
                OpenMainResourceRO(ID, ItemName) 'read only resource detail
                Exit Sub
            End If
        Catch ex As Exception
            modGlobalVar.msg("ERROR: on RO Resource scalar", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            sc.Close()
        End Try
OpenMain:
        OpenMainResource(ID, ItemName)

    End Sub

    'OPEN MAIN frmRESOURCE - from CaseConversation, ContactConversations, srchContactsConversations; need OrgName, PersonName
    Public Sub OpenMainResource(ByVal ID As Integer, ByVal ItemName As String) 'param name, ds name, da name, first control name for focus, form name, miSave name, tablename
        Dim frmResource As New frmMainResource
        Dim strName As String

        If Forms.find(frmResource) Then 'is already open
            Try
                'ClassOpenForms.frmMainResource.UpdateDB("minimize")
                If ClassOpenForms.frmMainResource.LookForChanges = True Then
                    'ask user to save changes
                    strName = UCase(ClassOpenForms.frmMainResource.txtResourceName.Text) '.Substring(1, 10)
                    Select Case modGlobalVar.msg("WAIT - confirm save changes?", "'" & strName & "' Detail must close." & NextLine & NextLine &
                                       "Click Yes to save changes." & NextLine &
                                       "Click No to discard changes" & NextLine &
                                       "Click Cancel to go back to '" & strName & "'", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
                        Case DialogResult.Yes
                            ClassOpenForms.frmMainResource.DoUpdate() 'update current record before reloading
                        Case DialogResult.No 'reload without saving changes to current record
                        Case DialogResult.Cancel 'go back to current record without opening this new one
                            Exit Sub
                    End Select
                End If

                Try
                    ClassOpenForms.frmMainResource.BringToFront()
                    ClassOpenForms.frmMainResource.WindowState = FormWindowState.Normal
                Catch e2 As Exception
                    modGlobalVar.msg("ERROR: can't bring to front", e2.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End Try
            Catch e As Exception    'don't put error message here
            End Try
        Else

FormNotOpen:  'show form, attach global variable
            Try
                ClassOpenForms.frmMainResource = frmResource
            Catch ex As Exception
                modGlobalVar.msg("ERROR: can't attach frm resource", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
            Try
                ClassOpenForms.frmMainResource.Show()
            Catch ex As Exception
                modGlobalVar.msg("ERROR: can't show Resource Detail", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If

FillMainForm:
        ClassOpenForms.frmMainResource.isLoaded = False
        ClassOpenForms.frmMainResource.ThisID = ID
        ' ClassOpenForms.frmMainResource.DsMainResourceAll1.Clear()
        'ClassOpenForms.frmMainResource.DsMainResourceAll1.EnforceConstraints = False
        Try
            ClassOpenForms.frmMainResource.MainResourceTableAdapter.Fill(ClassOpenForms.frmMainResource.DsMainResource1.MainResource, ID)
        Catch ex As Exception
            modGlobalVar.msg("ERROR: can't FILL main resource " + ID.ToString, ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        ClassOpenForms.frmMainResource.Reload()
        Try
            ClassOpenForms.frmMainResource.SetTabCaptions(ID)
            ClassOpenForms.frmMainResource.Text = "RESOURCE DETAIL: " + ItemName
            ClassOpenForms.frmMainResource.isLoaded = True
        Catch ex As Exception
            '  modGlobalVar.Msg("ERROR: can't set Resource headings", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        MouseDefault()
    End Sub

    'OPEN MAIN frmRESOURCE READ ONLY re CRG permission - from CaseConversation, ContactConversations, srchContactsConversations; need OrgName, PersonName
    Public Sub OpenMainResourceRO(ByVal ID As Integer, ByVal ItemName As String) 'param name, ds name, da name, first control name for focus, form name, miSave name, tablename
        Dim frmResourceRO As New frmMainResourceReadOnly

        If Forms.find(frmResourceRO) Then 'is already open

            Try
                ClassOpenForms.frmMainResourceRO.DoUpdate()
                Try
                    ClassOpenForms.frmMainResourceRO.BringToFront()
                    ClassOpenForms.frmMainResourceRO.WindowState = FormWindowState.Normal
                Catch e2 As Exception
                    '   modGlobalVar.Msg("ERROR: can't bring to front", e2.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End Try
            Catch e As Exception    'don't put error message here
            End Try
        Else

FormNotOpen:  'show form, attach global variable
            ClassOpenForms.frmMainResourceRO = frmResourceRO
            Try
                frmResourceRO.Show()
            Catch ex As Exception
                modGlobalVar.msg("ERROR: can't show RO Resource ", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If

FillMainForm:
        frmResourceRO.isLoaded = False
        frmResourceRO.ThisID = ID

        Try
            frmResourceRO.tblResourceTableAdapter.Fill(frmResourceRO.DsMainResourceReadOnly1.tblResource, ID)
        Catch ex As Exception
            modGlobalVar.msg("ERROR: filling RO Resource " + ID.ToString, ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        frmResourceRO.Reload()
        Try
            frmResourceRO.SetTabCaptions(ID)
            frmResourceRO.Text = "CRG RESOURCE - READ ONLY DETAIL: " + ItemName
            frmResourceRO.isLoaded = True
        Catch ex As Exception
        End Try
        MouseDefault()
    End Sub

    'OPEN MAIN frmRECOMMENDATION - from MainResource 
    Public Sub OpenMainRecommend(ByVal ID As Integer, ByVal ItemName As String, ByVal Orgnum As Integer) 'param name, ds name, da name, first control name for focus, form name, miSave name, tablename
        If ID > 0 Then
            'check not  deleted sincen opened search grid
            If CheckExists("Recommendation", "SELECT RecommendID From tblResourceRecommend where RecommendID = " & ID) = False Then
                Exit Sub
            End If
        Else
            Exit Sub
        End If
        ' modGlobalVar.Msg(sc.State.ToString, , "opening recommend, this should say False")
        ' modGlobalVar.Msg("Recomendation window is not ready yet", modGlobalVar.MsgStyle.DefaultButton1 Or MessageBoxIcon.Information, "Sorry")
        Dim newRecommend As New frmMainResourceRecommend
        If Forms.find(newRecommend) Then 'is already open
            Try
                'ClassOpenForms.frmMainResourceRecommend.UpdateDB("minimize")
                Try
                    ClassOpenForms.frmMainResourceRecommend.BringToFront()
                    ClassOpenForms.frmMainResourceRecommend.WindowState = FormWindowState.Normal
                Catch e2 As Exception
                    '   modGlobalVar.Msg("ERROR: can't bring to front", e2.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End Try
            Catch e As Exception    'don't put error message here
            End Try
        Else

FormNotOpen:  'show form, attach global variable

            Try
                ClassOpenForms.frmMainResourceRecommend = newRecommend
            Catch ex As Exception
                modGlobalVar.msg("ERROR: can't attach Reommend", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
            Try
                newRecommend.Show()
            Catch ex As Exception
                modGlobalVar.msg("ERROR: can't show Recommend ", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
        sc.Close()
FillMainForm:
        ClassOpenForms.frmMainResourceRecommend.isLoaded = False
        ClassOpenForms.frmMainResourceRecommend.DsMainResourceRecommend1.Clear()


        'If iOrgID = 0 Then
        'Else
        '    Try
        '        ClassOpenForms.frmMainResourceRecommend.LoadOrgBasedCombos(iOrgID)
        '    Catch ex As Exception
        '        modGlobalVar.Msg(ex.Message)
        '    End Try
        'End If
        'SetHeadings:
        ClassOpenForms.frmMainResourceRecommend.Text = "RECOMMENDATION DETAIL for : " + ItemName
        Try
            ClassOpenForms.frmMainResourceRecommend.LocalOrgID = IsNull(Orgnum, 0)   ''that are based on current record
            ClassOpenForms.frmMainResourceRecommend.ThisID = ID
            ClassOpenForms.frmMainResourceRecommend.Reload()
        Catch ex As Exception
            ' modGlobalVar.Msg("ERROR: can't set headings", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        ClassOpenForms.frmMainResourceRecommend.DsMainResourceRecommend1.EnforceConstraints = False
        ClassOpenForms.frmMainResourceRecommend.daMainRecommendation.SelectCommand.Parameters("@ID").Value = ID
        Try
            ClassOpenForms.frmMainResourceRecommend.daMainRecommendation.Fill(ClassOpenForms.frmMainResourceRecommend.DsMainResourceRecommend1, "MainResRecommend")
        Catch ex As Exception
            modGlobalVar.msg("ERROR: can't FILL mainrecommend " + ID.ToString, ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        ClassOpenForms.frmMainResourceRecommend.isLoaded = True
        MouseDefault()
    End Sub

    'OPEN MAIN frmFEEDBACK - from MainResource 
    Public Sub OpenMainFeedback(ByVal ID As Integer, ByVal ResourceID As Integer, ByVal ItemName As String, ByVal Orgnum As Integer) 'param name, ds name, da name, first control name for focus, form name, miSave name, tablename
        If ID > 0 Then
            'check not  deleted sincen opened search grid
            If CheckExists("Feedback", "SELECT FeedbackID From tblResourceFeedback where FeedbackID = " & ID) = False Then
                Exit Sub
            End If
        Else
            Exit Sub
        End If

        Dim newFeedback As New frmMainResourceFeedback

        If Forms.find(newFeedback) Then 'is already open
            Try
                ClassOpenForms.frmMainResourceFeedback.DoUpdate() 'UpdateDB("minimize")
                Try
                    ClassOpenForms.frmMainResourceFeedback.BringToFront()
                    ClassOpenForms.frmMainResourceFeedback.WindowState = FormWindowState.Normal
                Catch e2 As Exception
                    '   modGlobalVar.Msg(e2.Message, MessageBoxIcon.Error, "can't bring to front")
                    Exit Sub
                End Try
            Catch e As Exception    'don't put error message here
            End Try
        Else

FormNotOpen:  'show form, attach global variable
            Try
                ClassOpenForms.frmMainResourceFeedback = newFeedback
            Catch ex As Exception
                modGlobalVar.msg("ERROR: can't attach Feedback", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
            Try
                newFeedback.Show()
            Catch ex As Exception
                modGlobalVar.msg("ERROR: can't show Feedback", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
        sc.Close()
        'SetHeadings:
        Try
            ClassOpenForms.frmMainResourceFeedback.LoadOrgBasedCombos(IsNull(Orgnum, 0))
            ClassOpenForms.frmMainResourceFeedback.Text = "FEEDBACK DETAIL for Resource: " + ItemName

        Catch ex As Exception

        End Try

FillMainForm:
        ClassOpenForms.frmMainResourceFeedback.isLoaded = False
        ClassOpenForms.frmMainResourceFeedback.DsMainFeedback1.Clear()
        ClassOpenForms.frmMainResourceFeedback.DsMainFeedback1.EnforceConstraints = False
        Try
            ClassOpenForms.frmMainResourceFeedback.MainResourceFeedbackTableAdapter.Fill(ClassOpenForms.frmMainResourceFeedback.DsMainFeedback1.MainResourceFeedback, ID)

            'fill twice or orgbased combos default to first entry
            'ClassOpenForms.frmMainFeedback.daspMainFeedback.Fill(ClassOpenForms.frmMainFeedback.dsMainFeedback1, "tblResourceFeedback")
            '   ClassOpenForms.frmMainResourceFeedback.daspMainFeedback.Fill(ClassOpenForms.frmMainResourceFeedback.dsMainFeedback1, "tblResourceFeedback")
            '   ClassOpenForms.frmMainResourceFeedback.LoadOrgBasedCombos(ClassOpenForms.frmMainResourceFeedback.dsMainFeedback1.tblResourceFeedback.Rows(0).Item("OrgNum"))
            '   ClassOpenForms.frmMainResourceFeedback.dsMainFeedback1.Clear()
            '      ClassOpenForms.frmMainFeedback.dsMainFeedback1.EnforceConstraints = False
            '  ClassOpenForms.frmMainResourceFeedback.daspMainFeedback.Fill(ClassOpenForms.frmMainResourceFeedback.dsMainFeedback1, "tblResourceFeedback")
            '  ClassOpenForms.frmMainResourceFeedback.dsMainFeedback1.tblResourceFeedback.Rows(0).BeginEdit()
        Catch ex As Exception
            modGlobalVar.msg("ERROR: can't FILL mainFeedback " + ID.ToString, ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error) 'always has error - conflict w loadorgbased combos
        End Try

        ClassOpenForms.frmMainResourceFeedback.isLoaded = True
        MouseDefault()
    End Sub

    'OPEN MAIN frmResourceLocation - from MainResource 
    Public Sub OpenMainResourceLocation(ByVal ID As Integer, ByVal ResourceID As Integer, ByVal ItemName As String, ByVal strRegion As String) 'param name, ds name, da name, first control name for focus, form name, miSave name, tablename
        If ID > 0 Then
            'check not  deleted sincen opened search grid
            If CheckExists("Location", "SELECT ResourceLocationID From tblResourceLocation where ResourceLocationID = " & ID) = False Then
                Exit Sub
            End If
        Else
            Exit Sub
        End If

        Dim newResourceLocation As New frmMainResourceLocation
        gRegion = IsNull(strRegion, "Central")

        If Forms.find(newResourceLocation) Then 'is already open
            Try
                ClassOpenForms.frmMainResourceLocation.DoUpdate()
                Try
                    ClassOpenForms.frmMainResourceLocation.BringToFront()
                    ClassOpenForms.frmMainResourceLocation.WindowState = FormWindowState.Normal
                Catch e2 As Exception
                    '   modGlobalVar.Msg(e2.Message, MessageBoxIcon.Error, "can't bring to front")
                    Exit Sub
                End Try
            Catch e As Exception    'don't put error message here
            End Try
        Else

FormNotOpen:  'show form, attach global variable
            Try
                ClassOpenForms.frmMainResourceLocation = newResourceLocation
            Catch ex As Exception
                modGlobalVar.msg("ERROR: can't attach Location", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
            Try
                newResourceLocation.Show()
            Catch ex As Exception
                modGlobalVar.msg("ERROR: can't show Location ", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If


FillMainForm:
        ClassOpenForms.frmMainResourceLocation.isLoaded = False
        ClassOpenForms.frmMainResourceLocation.ThisID = ID
        ClassOpenForms.frmMainResourceLocation.LocalResourceID = ResourceID
        ClassOpenForms.frmMainResourceLocation.DsMainResourceLocation1.Clear()
        ClassOpenForms.frmMainResourceLocation.DsMainResourceLocation1.EnforceConstraints = False
        ClassOpenForms.frmMainResourceLocation.daMainResourceLocation.SelectCommand.Parameters("@ID").Value = ID
        ClassOpenForms.frmMainResourceLocation.Reload()
        ' modGlobalVar.Msg("about to fill", , ID.ToString)
        Try
            ClassOpenForms.frmMainResourceLocation.daMainResourceLocation.Fill(ClassOpenForms.frmMainResourceLocation.DsMainResourceLocation1, "MainResourceLocation")
            ' ClassOpenForms.frmMainResourceLocation.MainResourceLocationTableAdapter.Fill(ClassOpenForms.frmMainResourceLocation.DsMainResourceLocation1.MainResourceLocation, ID)

        Catch ex As Exception
            modGlobalVar.msg("ERROR: can't FILL mainResourceLocation " + ID.ToString, ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        'SetHeadings:
        Try
            ClassOpenForms.frmMainResourceLocation.Text = "Location for Resource: " + ItemName
            '   ClassOpenForms.frmMainResourceLocation.reload()
            ClassOpenForms.frmMainResourceLocation.isLoaded = True
        Catch ex As Exception
        End Try
        MouseDefault()
    End Sub

    ' OPEN MAIN frmResourceALERT - from MainResource
    Public Sub OpenMainResourceWarning(ByVal ID As Integer, ByVal ResourceID As Integer, ByVal ItemName As String) 'param name, ds name, da name, first control name for focus, form name, miSave name, tablename
        Dim newWarning As New frmMainResourceWarning
        '  Dim i As Integer
        If Forms.find(newWarning) Then 'is already open
            Try
                ClassOpenForms.frmMainResourceWarning.DoUpdate()
                Try
                    ClassOpenForms.frmMainResourceWarning.BringToFront()
                    ClassOpenForms.frmMainResourceWarning.WindowState = FormWindowState.Normal
                Catch e2 As Exception
                    '   modGlobalVar.Msg("ERROR: can't bring to front", e2.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End Try
            Catch e As Exception    'don't put error message here
            End Try
        Else

FormNotOpen:  'show form, attach global variable
            Try
                ClassOpenForms.frmMainResourceWarning = newWarning
            Catch ex As Exception
                modGlobalVar.msg("ERROR: can't attach Resource Warning", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
            Try
                newWarning.Show()
            Catch ex As Exception
                modGlobalVar.msg("can't show Resource Warning ", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
        sc.Close()
FillMainForm:
        ClassOpenForms.frmMainResourceWarning.isLoaded = False
        ClassOpenForms.frmMainResourceWarning.DsMainResourceWarning1.Clear()
        ClassOpenForms.frmMainResourceWarning.DsMainResourceWarning1.EnforceConstraints = False
        ClassOpenForms.frmMainResourceWarning.daMainResourceWarning.SelectCommand.Parameters("@ID").Value = ID
        Try
            ClassOpenForms.frmMainResourceWarning.Reload()
            ClassOpenForms.frmMainResourceWarning.daMainResourceWarning.Fill(ClassOpenForms.frmMainResourceWarning.DsMainResourceWarning1, "MainResourceWarning")
        Catch ex As Exception
            modGlobalVar.msg("ERROR: can't FILL mainWarning " + ID.ToString, ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally

        End Try

        'SetHeadings:
        Try
            ClassOpenForms.frmMainResourceWarning.Text = "Warning DETAIL for Resource: " + ItemName
        Catch ex As Exception
        End Try

        ClassOpenForms.frmMainResourceWarning.isLoaded = True

        MouseDefault()
    End Sub

    'OPEN MAIN ORG STORY
    Public Sub OpenMainStory(ByVal ID As Integer, ByVal GotoOrg As String, ByVal IDOrg As Integer) 'param name, ds name, da name, first control name for focus, form name, miSave name, tablename
        Dim newStory As New frmMainOrgStory
        If Forms.find(newStory) Then
            Try
                'ClassOpenForms.frmMainOrgStory.txtHeadline.Focus()
                ClassOpenForms.frmMainOrgStory.DoUpdate()
                Try
                    ClassOpenForms.frmMainOrgStory.BringToFront()
                    ClassOpenForms.frmMainOrgStory.WindowState = FormWindowState.Normal
                    GoTo FillMainForm
                Catch e2 As Exception
                    modGlobalVar.msg("ERROR: can't bring Org Story to front", e2.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End Try
            Catch e As Exception    'don't put error message here
            End Try
        Else

FormNotOpen:  'show form, attach global variable
            Try
                ClassOpenForms.frmMainOrgStory = newStory
                newStory.Show()
            Catch ex As Exception
                modGlobalVar.msg("can't open OrgStory", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
FillMainForm:
        ClassOpenForms.frmMainOrgStory.isLoaded = False
        ClassOpenForms.frmMainOrgStory.DsMainStory.Clear()
        ClassOpenForms.frmMainOrgStory.DsMainStory.EnforceConstraints = False
        ClassOpenForms.frmMainOrgStory.daMainOrgStory.SelectCommand.Connection = sc
        ClassOpenForms.frmMainOrgStory.daMainOrgStory.SelectCommand.Parameters("@ID").Value = ID
        ClassOpenForms.frmMainOrgStory.ThisID = ID
        ClassOpenForms.frmMainOrgStory.LocalOrgID = IDOrg
        Try
            ClassOpenForms.frmMainOrgStory.Reload()
            ClassOpenForms.frmMainOrgStory.daMainOrgStory.Fill(ClassOpenForms.frmMainOrgStory.DsMainStory, "MainOrgStory")
        Catch ex As Exception
            modGlobalVar.msg("ERROR: can't FILL mainStory " + ID.ToString, ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
SetHeadings:

        ' ClassOpenForms.frmMainContact.Text = "CONTACT DETAIL: " + ItemName
        ' ClassOpenForms.frmMainConversation.strOrgName = GotoName
        ClassOpenForms.frmMainOrgStory.fldGotoOrg.Text = GotoOrg

        MouseDefault()
    End Sub

    'OPEN MAIN ORG ALERT
    Public Sub OpenMainAlert(ByVal ID As Integer, ByVal GotoOrg As String, ByVal IDOrg As Integer) 'param name, ds name, da name, first control name for focus, form name, miSave name, tablename
        Dim frm As New frmMainOrgAlert
        ' frm.daMainOrgAlert.SelectCommand.Parameters("@ID").Value = ID
        ' frm.Show()

        Dim newAlert As New frmMainOrgAlert
        If Forms.find(newAlert) Then
            Try
                'ClassOpenForms.frmMainOrgAlert.txtHeadline.Focus()
                ClassOpenForms.frmMainOrgAlert.DoUpdate()
                Try
                    ClassOpenForms.frmMainOrgAlert.BringToFront()
                    ClassOpenForms.frmMainOrgAlert.WindowState = FormWindowState.Normal
                    GoTo FillMainForm
                Catch e2 As Exception
                    modGlobalVar.msg("ERROR: can't bring  Org alertto front", e2.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End Try
            Catch e As Exception    'don't put error message here
            End Try
        Else
FormNotOpen:  'show form, attach global variable
            Try
                ClassOpenForms.frmMainOrgAlert = newAlert
                newAlert.Show()
            Catch ex As Exception
                modGlobalVar.msg("ERROR: can't open OrgAlert", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
FillMainForm:
        ClassOpenForms.frmMainOrgAlert.isLoaded = False
        ClassOpenForms.frmMainOrgAlert.ThisID = ID
        ClassOpenForms.frmMainOrgAlert.LocalOrgID = IDOrg
        '   ClassOpenForms.frmMainOrgAlert.Reload()
        ClassOpenForms.frmMainOrgAlert.DsMainOrgAlert1.Clear()
        ClassOpenForms.frmMainOrgAlert.DsMainOrgAlert1.EnforceConstraints = False
        ClassOpenForms.frmMainOrgAlert.daMainOrgAlert.SelectCommand.Connection = sc
        ClassOpenForms.frmMainOrgAlert.daMainOrgAlert.SelectCommand.Parameters("@ID").Value = ID
        'ClassOpenForms.frmMainOrgAlert.TblOrgAlertTableAdapter.GetData.s.Parameters("@AlertID").Value = ID
        'ClassOpenForms.frmMainContact.iContact = ID
        Try

            ClassOpenForms.frmMainOrgAlert.daMainOrgAlert.Fill(ClassOpenForms.frmMainOrgAlert.DsMainOrgAlert1)
            ClassOpenForms.frmMainOrgAlert.Reload()
        Catch ex As Exception
            modGlobalVar.msg("ERROR: can't FILL mainAlert " + ID.ToString, ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
SetHeadings:

        ' ClassOpenForms.frmMainContact.Text = "CONTACT DETAIL: " + ItemName
        ' ClassOpenForms.frmMainConversation.strOrgName = GotoName
        ClassOpenForms.frmMainOrgAlert.fldGotoOrg.Text = GotoOrg
        ClassOpenForms.frmMainOrgAlert.isLoaded = True
        MouseDefault()
    End Sub

    ' OPEN MAIN frmCONVERSATION - from CaseConversation, ContactConversations, srchContactsConversations; need OrgName, PersonName
    Public Sub OpenMainStaffConversation(ByVal ID As Integer)
        Dim newStaffConv As New frmMainStaffConversation

        Try
            ClassOpenForms.frmMainStaffConversation.txtConverseDate.Focus() 'already open
            ClassOpenForms.frmMainStaffConversation.UpdateDB("minimize")
            Try
                ClassOpenForms.frmMainStaffConversation.BringToFront()
                ClassOpenForms.frmMainStaffConversation.WindowState = FormWindowState.Normal
                ' ClassOpenForms.frmMainConversation.closesave()
                GoTo FillMainForm
            Catch e2 As Exception
                modGlobalVar.msg("ERROR: can't bring Staff Conv to front", e2.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
        Catch e As Exception    'don't put error message here
        End Try

FormNotOpen:  'show form, attach global variable
        Try
            ClassOpenForms.frmMainStaffConversation = newStaffConv
            newStaffConv.Show()
        Catch ex As Exception
            modGlobalVar.msg("ERROR: can't open Staff Conv", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

FIllMainForm:
        ' ClassOpenForms.frmMainConversation.Update()
        '   ClassOpenForms.frmMainStaffConversation.isLoaded = False
        ' ClassOpenForms.frmMainStaffConversation.DsMainConversation1.Clear()

        '  ClassOpenForms.frmMainStaffConversation.DsMainConversation1.EnforceConstraints = False
        '  ClassOpenForms.frmMainStaffConversation.daspMainConversation.SelectCommand.Parameters("@ID").Value = ID
        newStaffConv.TblStaffConversationTableAdapter.Fill(newStaffConv.DsStaffConv1.MainStaffConversation, ID)

        '     Try
        'ClassOpenForms.frmMainConversation.LoadOrgBasedCombos()
        '    ClassOpenForms.frmMainConversation.daspMainConversation.Fill(ClassOpenForms.frmMainConversation.DsMainConversation1, "tblConversation")
        '    Catch ex As Exception
        'modGlobalVar.Msg(ex.Message, , "can't FILL main staff conversation " + ID.ToString)
        '    End Try

        '   ClassOpenForms.frmMainConversation.isLoaded = True
        ' CallingForm.StatusBar1.Text = "Done"
        MouseDefault()

    End Sub

    'REGISTRATION REPORT VIEWER - with choice of reports
    Public Sub OpenReportRegistration(ByVal ID As Integer, ByVal ItemName As String)
        Dim f As New rptViewerRegistration
        f.ID = ID
        f.Text = "Reports for : " & ItemName
        If Forms.find(f) Then
            '   modGlobalVar.Msg("found")
            ClassOpenForms.rptVwRegistration.BringToFront()
            ClassOpenForms.rptVwRegistration.WindowState = FormWindowState.Normal
        Else
            ' modGlobalVar.Msg("opening new")
            f.Show()
            ClassOpenForms.rptVwRegistration = f
        End If

    End Sub

    'REGISTRATION REPORT VIEWER - cancellation report
    Public Sub OpenReportCancellation(ByVal ID As Integer, ByVal ItemName As String)
        Dim f As New rptViewerRegistration
        f.ID = ID
        f.Text = "Reports for : " & ItemName
        If Forms.find(f) Then
            '   modGlobalVar.Msg("found")
            ClassOpenForms.rptVwRegistration.BringToFront()
            ClassOpenForms.rptVwRegistration.WindowState = FormWindowState.Normal
        Else
            ' modGlobalVar.Msg("opening new")
            f.Show()
            ClassOpenForms.rptVwRegistration = f
        End If
        ' f.cboSelect.DroppedDown = True
        f.cboSelect.SelectedIndex = f.cboSelect.FindStringExact("Cancellation Phone List")
        '        f.cboSelect.DroppedDown = False  'hmmm cause report source error
    End Sub

    'OPEN FORMS THAT ARE NOT CONNECTED TO DB - why???
    Public Sub OpenNonDataForm(ByVal chkfrm As Form)
        Dim f As New frmGeography   'chkfrm works here with error task
        If Forms.find(chkfrm) Then
            ClassOpenForms.frmGeography.BringToFront()
            ClassOpenForms.frmGeography.WindowState = FormWindowState.Normal
        Else
            f.Show()
            ClassOpenForms.frmGeography = f
        End If
        ''is found, but can't bring tofront
        'If Forms.find(chkfrm) Then
        '    modGlobalVar.Msg("found")
        '    chkfrm.BringToFront()
        '    chkfrm.WindowState = FormWindowState.Normal
        'Else
        '    modGlobalVar.Msg("not found")
        '    chkfrm.Show()
        'End If
    End Sub

    'new switchboard
    Public Sub OpenfrmSwitchboard(ByVal strTitle As String)
        Dim newFrm As New frmSwitchboard
        newFrm.Show()

    End Sub

    'PENDING by event or region
    Public Sub OpenPendingRegistrations(ByVal cntPendings As Int16, ByVal iEvent As Integer, ByVal strRegion As String)
        Dim frm As New frmMainWPending
        Dim strMsg As String
        frm.cboRegion.SelectedIndex = frm.cboRegion.FindStringExact(IsNull(strRegion, usrRegion))
        frm.DsRegPending.EnforceConstraints = False


        If cntPendings > 0 Then 'event with pendings selected
            frm.tblRegPendingTableAdapter.FillByEvent(frm.DsRegPending.tblEventRegPending2, iEvent)
            GoTo showform
        End If

        If iEvent > 0 Then
            strMsg = "FYI no Pendings for this event."
        Else 'no event selected
            strMsg = "FYI no Event selected."
        End If

        'SEARCH BY REGION ... no Event 
        If IsNull(strRegion, usrRegion) = "All Indiana" Then 'skip region search
            GoTo FillAll
        Else
            cntPendings = frm.tblRegPendingTableAdapter.FillByRegion(frm.DsRegPending.tblEventRegPending2, IsNull(strRegion, usrRegion))
            If cntPendings > 0 Then
                If modGlobalVar.msg(strMsg, "Show all " & cntPendings & " Pending registrations for " & IsNull(strRegion, usrRegion) & " instead?", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) = DialogResult.OK Then
                    GoTo showform
                Else
                    Exit Sub
                End If
            Else 'no pendings per region and/or no event selected - search all
            End If
        End If
FillAll:
        cntPendings = frm.tblRegPendingTableAdapter.FillAll(frm.DsRegPending.tblEventRegPending2)
        If cntPendings > 0 Then
            If IsNull(strRegion, usrRegion) = "All Indiana" Then
                If modGlobalVar.msg(strMsg, "Show all " & cntPendings & " statewide Pendings?", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) = DialogResult.OK Then
                    GoTo showform
                Else
                    Exit Sub
                End If
            Else 'msgbox include region name
                If modGlobalVar.msg(strMsg, "Show all " & cntPendings & " statewide Pendings?" & NextLine & "NO registrations pending for " & IsNull(strRegion, usrRegion) & ".", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) = DialogResult.OK Then
                    GoTo showform
                Else
                    Exit Sub
                End If
            End If
        Else
            modGlobalVar.msg(MsgCodes.noResultCancel)
            Exit Sub
        End If


ShowForm:
            frm.Show()
            strMsg = Nothing
    End Sub

    'PENDING by ORDER#
    Public Sub OpenPendingRegistrations(ByVal iOrder As Integer)
        Dim frm As New frmMainWPending

        frm.tblRegPendingTableAdapter.FillByOrder(frm.DsRegPending.tblEventRegPending2, iOrder)
        frm.Show()

    End Sub

    Public Sub OpenMainWOrder(ByVal id As Integer, ByVal bDialog As Boolean)
        Dim frm As New frmMainWOrder
        'LOAD STATUS COMBO
        LoadOrderStatusCombo(frm.cboOrderStatus)

FillMainForm:
        Try
            frm.taMainRegistOrder.Fill(frm.dsMainWOrder1.MainEventOrder2, id)
        Catch ex As Exception
            modGlobalVar.msg("ERROR: can't fill order tbl", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        frm.LoadGrid(id)
        frm.ShowDialog()

        MouseDefault()
    End Sub


    ''send emails directly, with subject line
    'Public Sub OpenEmailForm(ByRef sql As SqlCommand, ByVal sHeading As String, ByVal subjct As String)
    '    Dim f As New frmMailEmail

    '    '1. fill tables
    '    modGlobalVar.LoadDataTable(tblSend, sql)
    '    If tblSend.Rows.Count > 0 Then
    '        SetupEmailTables()
    '        '2. open form for user to check emails and add subject and body
    '        If sHeading.Contains("MGI") Then
    '            f.txtSubject.Text = "Center for Congregations " & tblSend.Rows(0)(subjct) & " Application"
    '        Else
    '            f.txtSubject.Text = tblSend.Rows(0)(subjct)
    '        End If

    '        f.Text = sHeading
    '        f.Show()
    '    Else
    '        modGlobalVar.msg(MsgCodes.noResultCancel)
    '    End If

    'End Sub

#End Region  'open forms

#Region "Form Setup"

    'ENABLES DOUBLE-CLICK from textboxes in datagrid
    Public Sub EnableGridTextboxes(ByRef grd As DataGrid)

        Dim tbs As DataGridTableStyle
        Dim tbx As DataGridTextBoxColumn
        For Each tbs In grd.TableStyles
            For Each tbx In tbs.GridColumnStyles
                tbx.TextBox.Enabled = False
                tbx.NullText = "" '?optional?
            Next
        Next
    End Sub

    'CLEAR SECONDARY SELECTION INDICATORS
    Public Sub ClearIDLbls(ByRef lblID As Label, ByRef lblWhat As Label)
        lblID.Text = String.Empty
        lblWhat.Text = String.Empty
    End Sub

    'hide all but one page on tab control; esp for frmAddNew so only 1 dataset is loaded and 1 OK button/update works
    Public Sub HideTabPage(ByVal strPage As String, ByRef strTab As TabControl)
        For x As Integer = strTab.TabPages.Count - 1 To 0 Step -1
            If strTab.TabPages(x).Name = strPage Then
            Else
                Try
                    strTab.TabPages.Remove(strTab.TabPages(x))
                Catch ex As Exception
                    modGlobalVar.msg("ERROR: HideTabPage ", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End If
        Next
    End Sub

#End Region 'form setup

#Region "CLOSING FORM DATA CHANGES"
    'TODO install this?

    'GIVE USER CHOICE OF EXIT WITHOUT SAVE; boolean cancel close 
    Public Function ExitWithoutSave(ByVal b As Boolean, ByVal What As String) As Short
        Dim UsrChoice As MsgBoxResult

        If b = True Then 'changes
            UsrChoice = modGlobalVar.msg("ATTENTION: " & What & " needs to close first. ", "Click Yes to save changes." & NextLine &
                               "Click No to discard changes." & NextLine &
                               "Click Cancel to return to " & What & ".", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)

        End If

        Select Case UsrChoice
            Case Is = DialogResult.Yes      'save changes
                ExitWithoutSave = ObjClose.miSave

            Case Is = DialogResult.No       'discard changes
                ExitWithoutSave = ObjClose.noChanges

            Case Is = MsgBoxResult.Cancel   'cancel close
                ExitWithoutSave = ObjClose.cancelClose

        End Select

    End Function

    'RETURN MESSAGE uses string instead of controls
    Public Function GetMissingMessage(ByVal bUpdated As Boolean) As String
        If bUpdated Then
            GetMissingMessage = "Your changes have been saved,                         " & NextLine & "but please fill in the " & NextLine
            '    modGlobalVar.Msg("please fill in the " & UCase(IsNull(ctl.Tag, ctl.Name)) & "                    ", MessageBoxIcon.Exclamation, "WAIT - your changes have been saved, but required information is missing")
        Else
            GetMissingMessage = "Please fill in the                       " & NextLine
            '  modGlobalVar.Msg("please fill in the " & UCase(IsNull(ctl.Tag, ctl.Name)) & "                  ", MessageBoxIcon.Exclamation, "WAIT - required information is missing")
        End If
    End Function

    'GIVE USER CHOICE OF EXIT WITHOUT SAVE; boolean cancel close
    Public Function AskAcceptChanges(ByVal b As Boolean, ByVal What As String) As Short
        Dim UsrChoice As MsgBoxResult

        If b = True Then 'changes
            UsrChoice = modGlobalVar.msg("Confirm Close " & What & " Detail", "Save changes and Exit?" & NextLine & NextLine &
                               "Click Yes to save and close." & NextLine &
                               "Click No to discard changes and close." & NextLine &
                               "Click Cancel keep Detail window open.", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
        Else 'no changes
            UsrChoice = modGlobalVar.msg("Confirm Close " & What & " Detail", "No changes recognized." & NextLine & NextLine &
                               "Click OK to close." & NextLine &
                               "Click Cancel keep Detail window open.", MessageBoxButtons.OKCancel, MessageBoxIcon.Question)
        End If

        Select Case UsrChoice
            Case Is = DialogResult.Yes      'save changes and close
                AskAcceptChanges = ObjClose.SaveClose

            Case Is = DialogResult.No       'discard changes and close
                AskAcceptChanges = ObjClose.DontSaveClose

            Case Is = MsgBoxResult.Cancel   'cancel close
                AskAcceptChanges = ObjClose.cancelClose

            Case Is = MsgBoxResult.Ok       'no changes, close
                AskAcceptChanges = ObjClose.noChanges

        End Select

    End Function

    'GIVE USER CHOICE OF EXIT WITHOUT SAVE; boolean cancel close
    Public Function AskAcceptChanges(ByRef ds As DataSet, ByVal What As String) As Short
        Dim UsrChoice As MsgBoxResult

        If ds.HasChanges Then
            UsrChoice = modGlobalVar.msg("Confirm Close " & What & " Detail", "Save changes and Exit?" & NextLine & NextLine &
                               "Click Yes to save and close." & NextLine &
                               "Click No to discard changes and close." & NextLine &
                               "Click Cancel keep Detail window open.", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
        Else 'no changes
            UsrChoice = modGlobalVar.msg("Confirm Close " & What & " Detail", "No changes recognized." & NextLine & NextLine &
                               "Click OK to close." & NextLine &
                               "Click Cancel keep Detail window open.", MessageBoxButtons.OKCancel, MessageBoxIcon.Question)
        End If

        Select Case UsrChoice
            Case Is = DialogResult.Yes      'save changes and close
                AskAcceptChanges = ObjClose.SaveClose

            Case Is = DialogResult.No       'discard changes and close
                AskAcceptChanges = ObjClose.DontSaveClose

            Case Is = MsgBoxResult.Cancel   'cancel close
                AskAcceptChanges = ObjClose.cancelClose

            Case Is = MsgBoxResult.Ok       'no changes, close
                AskAcceptChanges = ObjClose.noChanges

        End Select

    End Function

    'ASK USER FOR MISSING INFO; returns Cancelclose
    Public Function AskCloseWithMissingInfo(ByVal iOKClose As Short, ByRef ctl As Control, ByVal strListMissing As String) As Boolean
        Select Case iOKClose

            Case Is = ObjClose.btnSaveExit  'force user to go back
                modGlobalVar.msg("CANCELLING CLOSE - required information is missing", GetMissingMessage(False) & strListMissing.ToString,
                                 MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                ctl.Focus()
                AskCloseWithMissingInfo = False

            Case Is = ObjClose.SaveClose    'insert and save default info, and allow to exit
                If modGlobalVar.msg("WAIT: missing important information!", GetMissingMessage(True) & strListMissing & NextLine &
                          "Default information may be inserted for you." & NextLine & NextLine & "Do you still want to close this window?", MessageBoxButtons.YesNo, MessageBoxIcon.Question
                          ) = DialogResult.Yes Then

                    modGlobalVar.msg("Closing but with missing ...", strListMissing, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    AskCloseWithMissingInfo = True
                Else
                    ctl.Focus()
                    AskCloseWithMissingInfo = False
                End If

            Case Is = ObjClose.noChanges   'user made no edits; allow them to do nothing and close
                If modGlobalVar.msg("WAIT: missing important information!", strListMissing & NextLine & "Do you still want to close this window? ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    modGlobalVar.msg("Closing but with missing ... ", strListMissing, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    AskCloseWithMissingInfo = True
                Else
                    ctl.Focus()
                    AskCloseWithMissingInfo = False
                End If

                'bypassed in calling form
                'Case Is = ObjClose.DontSaveClose 'use canceled changes, allow close or will be confusing
                '   CloseWithMissingInfo = True
                ' Case Is = ObjClose.cancelClose
                '   CloseWithMissingInfo = True
            Case Else
                '   modGlobalVar.Msg(iOKClose.ToString, , "?how is this closing?")
                AskCloseWithMissingInfo = True
        End Select

    End Function

    'cmgr CHECK FOR CHANGES
    Public Function AnyChanges(ByRef btnDefault As Control, ByRef cmgr As CurrencyManager, ByRef tbl As DataTable, ByVal bChanged As Boolean) As Boolean
        btnDefault.Focus()  'to force text box recognization of change, although still needs currency mgr to work
        'this also causes Validation where applicable
        Dim dr As DataRow
        If tbl.Rows.Count > 0 Then
            dr = tbl.Rows(0)
        Else
            Return False
        End If

        ' dr.EndEdit()

        'If CType(cmgr.Current, DataRowView).Row.HasVersion(DataRowVersion.Proposed) Then
        '    modGlobalVar.Msg("cmgr proposed")
        'End If
        'If dtbl.Rows(0).RowState = DataRowState.Modified Then
        '    modGlobalVar.Msg("row state modified")
        'End If
        'If bChanged = True Then 'If CType(cmgr.Current, DataRowView).Row.HasVersion(DataRowVersion.Proposed) Or bChanged = True Then
        '    modGlobalVar.Msg("bchanged")
        'End If
        'If bChanged = True Then
        '    Return True
        'End If
        ' Try


        If CType(cmgr.Current, DataRowView).Row.HasVersion(DataRowVersion.Proposed) = True Or dr.RowState = DataRowState.Modified = True Then  'misses comboboxes changed programatically or backspaced
            For Each c As DataColumn In tbl.Columns
                '    If dr(c, DataRowVersion.Proposed) Is dr(c, DataRowVersion.Current) Then
                '    Else
                '  modGlobalVar.Msg(dr.Item(c, DataRowVersion.Current).ToString, , c.ColumnName)
                'str = NorthwindDataSet1.Customers(0)("CompanyName", DataRowVersion.Current).ToString()
                Try
                    If dr.Item(c, DataRowVersion.Original).ToString = IsNull(dr.Item(c, DataRowVersion.Proposed).ToString, dr.Item(c, DataRowVersion.Current).ToString) Then
                        'Else
                        '    If dr.Item(c, DataRowVersion.Current).ToString = dr.Item(c, DataRowVersion.Proposed).ToString Then
                    Else
                        ' modGlobalVar.Msg("current: '" & dr.Item(c, DataRowVersion.Current) & "'"& NextLine &  "Proposed: '" & dr.Item(c, DataRowVersion.Proposed) & "'"& NextLine &  "Original: '" & dr.Item(c, DataRowVersion.Original) & "'", , c.ColumnName)
                        Return True
                        '        End If
                    End If
                Catch ex As Exception
                End Try
                'endif
            Next c

        End If
        'Catch ex As Exception
        'End Try

        Return False
    End Function 'any changes cmgr

    'tableadapter CHECK FOR CHANGES with bChanged
    Public Function AnyChanges(ByRef btnDefault As Control, ByRef bs As BindingSource, ByRef tbl As DataTable, ByVal bChanged As Boolean) As Boolean

        Try
            btnDefault.Focus()  'to force text box recognization of change
            'this also causes Validation where applicable
        Catch ex As Exception
            modGlobalVar.msg("ERROR: focus default btn 1 ", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Dim dr As DataRow
        If tbl.Rows.Count > 0 Then
            dr = tbl.Rows(0)
        Else
            Return False
        End If

        If bChanged = True Then
            Return True
        End If
        Try
            If CType(bs.Current, DataRowView).Row.HasVersion(DataRowVersion.Proposed) = True Or dr.RowState = DataRowState.Modified = True Then  'misses comboboxes changed programatically or backspaced
                '  modGlobalVar.Msg("Yes")
                For Each c As DataColumn In tbl.Columns
                    Try
                        If dr.Item(c, DataRowVersion.Original).ToString = IsNull(dr.Item(c, DataRowVersion.Proposed).ToString, dr.Item(c, DataRowVersion.Current).ToString) Then
                        Else
                            Return True
                        End If
                    Catch ex As Exception
                    End Try
                Next c
            Else
                '  modGlobalVar.Msg("no")
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try

    End Function 'any changes bindingsource

    'tableadapter CHECK FOR CHANGES
    Public Function AnyChanges(ByRef btnDefault As Control, ByRef bs As BindingSource, ByRef tbl As DataTable) As Boolean
        '1
        Dim row As DataRow = tbl.Rows(0)

        Try
            btnDefault.Focus()  'to force text box recognization of change
        Catch ex As Exception
            '  modGlobalVar.msg("ERROR: focus default btn. 2 ", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Try
            If CType(bs.Current, DataRowView).Row.HasVersion(DataRowVersion.Proposed) = True Then

                For c As Integer = 0 To row.Table.Columns.Count - 1
                    If IsNull(row(c, DataRowVersion.Current), "").ToString = IsNull(row(c, DataRowVersion.Proposed), "").ToString Then 'data not changed

                        'MsgBox(row(c, DataRowVersion.Current).ToString & NextLine & row(c, DataRowVersion.Proposed).ToString, , "SAME " & c.ToString)
                    Else
                        '--use FOR CONCURRENCY
                        '--MsgBox(row(c, DataRowVersion.Current).ToString & NextLine & row(c, DataRowVersion.Proposed).ToString, , "DIFFERENT " & c.ToString)
                        Return True
                        Exit Function
                    End If
                Next c
                '   MsgBox("change not found")
                Return False
            Else
                '    MsgBox("no proposed version ")
                Return False
            End If
        Catch ex As Exception
            '  MsgBox(ex.Message, , "Error AnyChanges")
            Return False
        End Try
    End Function

    'UPDATE SINGLE ROW; want to return message? -- not used??
    Public Function UpdateDBSingle(ByVal entityName As String, ByRef da As SqlDataAdapter, ds As DataSet) As Integer
        'TODO set error here; display from calling form

        Try
            da.Update(ds)
            UpdateDBSingle = True
        Catch dbcex As DBConcurrencyException

            modGlobalVar.msg("ERROR: conflict", "someone else has changed this " & entityName, MessageBoxButtons.YesNo, MessageBoxIcon.Information)
            'TODO put code here to list to users changes and/or override
            UpdateDBSingle = False
        Catch eUpdate As System.Exception
            modGlobalVar.msg("ERROR in update dbSingle", eUpdate.GetType.ToString & NextLine & eUpdate.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
            UpdateDBSingle = False
        Finally
        End Try
    End Function

    'currency mgr SAVE or JUST CLOSE DETAIL FORMS cmgr -- not used/?
    Public Function DetailSaveClose(ByVal FrmHeading As String, ByRef ItemText As String, ByRef cmgr As CurrencyManager, ByVal ctlNeutral As Control, ByVal maintbl As DataTable, ByRef bChanged As Boolean) As Short

        If AnyChanges(ctlNeutral, cmgr, maintbl, bChanged) = True Then
            Select Case modGlobalVar.msg("CLOSING " & (FrmHeading) & " DETAIL mgr", "Record: " & ItemText & NextLine & NextLine & "SAVE CHANGES BEFORE CLOSING?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
                'THERE ARE CHANGES
                Case Is = DialogResult.Yes                  'save changes
                    DetailSaveClose = ObjClose.SaveClose
                Case Is = DialogResult.No                   'discard changes, but close
                    cmgr.CancelCurrentEdit()
                    DetailSaveClose = ObjClose.DontSaveClose
                Case Is = MsgBoxResult.Cancel               'cancel close
                    DetailSaveClose = ObjClose.cancelClose
            End Select
        Else    'data has not changed
            '???  modGlobalVar.Msg(ClassOpenForms.frmMainCase.cboStatus.SelectedIndex.ToString, , "closing selected index")
            If modGlobalVar.msg("CLOSING " & FrmHeading & " DETAIL", "Record: " & ItemText & NextLine & NextLine & "No changes found.", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) = MsgBoxResult.Ok Then
                DetailSaveClose = ObjClose.noChanges      'no changes, close
            Else
                DetailSaveClose = ObjClose.cancelClose    'cancel close
            End If
        End If
    End Function 'close cmgr

    'tbladapter SAVE or JUST CLOSE DETAIL FORMS bs
    Public Function CloseDetailForm(ByVal FrmHeading As String, ByRef ItemText As String, ByRef bs As BindingSource, ByVal ctlNeutral As Control, ByVal maintbl As DataTable, ByRef bChanged As Boolean) As Short

        If AnyChanges(ctlNeutral, bs, maintbl, bChanged) = True Then
            Select Case modGlobalVar.msg("CLOSING " & (FrmHeading) & " DETAIL bs", "Record: " & ItemText & NextLine & "SAVE CHANGES BEFORE CLOSING?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
                'THERE ARE CHANGES
                Case Is = DialogResult.Yes                  'save changes
                    CloseDetailForm = ObjClose.SaveClose
                Case Is = DialogResult.No                   'discard changes, but close
                    bs.CancelEdit()
                    CloseDetailForm = ObjClose.DontSaveClose
                Case Is = MsgBoxResult.Cancel               'cancel close
                    CloseDetailForm = ObjClose.cancelClose
            End Select
        Else    'data has not changed
            '  modGlobalVar.Msg(ClassOpenForms.frmMainCase.cboStatus.SelectedIndex.ToString, , "closing selected index")
            If modGlobalVar.msg("CLOSING " & FrmHeading & " DETAIL", "Record: " & ItemText & NextLine & NextLine & "No changes found.", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) = MsgBoxResult.Ok Then
                CloseDetailForm = ObjClose.noChanges      'no changes, close
            Else
                CloseDetailForm = ObjClose.cancelClose    'cancel close
            End If
        End If
    End Function 'close bindingsource

#End Region 'closing form data validation

#Region "Concurrency"

    ' Private Temptbl As New dsMainGrant.MainGrantDataTable

    'load message body 'tblorg, tblContact, tblResource. all tables WITH lastChangeDate
    Public Function CreateMessage_1(ByVal cr As System.Data.DataRow, ByVal da As SqlDataAdapter, ByRef tmptbl As Data.DataTable, ByRef mainTbl As Data.DataTable) As String 'dsMainGrant.MainGrantRow
        Dim s As String()
        'get BE row values
        GetCurrentRowInDB_2(tmptbl, da)
        'get last changed by info and FE row values compared to BE
        s = GetChangedBy5(tmptbl)
        ' Return GetChangedBy5(tmptbl)(0) &
        Return s(0) &
       GetRowData_3(cr, Data.DataRowVersion.Current, mainTbl, tmptbl).ToString & NextLine & NextLine &
        GetMsgInstructions(s(1) & "'s")
    End Function

    'load message body tblGrant, tblConversation, all tables without lastChangeDate
    Public Function CreateMessage_1NoLastChanged(ByVal cr As System.Data.DataRow, ByVal da As SqlDataAdapter, ByRef tmptbl As Data.DataTable, ByRef mainTbl As Data.DataTable) As String 'dsMainGrant.MainGrantRow
        'get BE row values
        GetCurrentRowInDB_2(tmptbl, da)
        'get FE row values compared to BE
        Return "This data has CHANGED since you opened it. " &
                GetRowData_3(cr, Data.DataRowVersion.Current, mainTbl, tmptbl).ToString & NextLine &
        GetMsgInstructions("")
        ''" If you proceed you will OVERWRITE the new data in the database.." &
    End Function

    'get lastchanged values from be
    Private Function GetChangedBy5(ByRef tmptbl As Data.DataTable) As String()
        Dim sDate, sName As String
        Dim drw() As DataRow
        'GetDate
        Try
            sDate = tmptbl.Rows(0)("LastChangeDate").ToString()
        Catch ex As Exception
        End Try

        'GetStaffName
        Try
            drw = tblStaff.Select("StaffID = " & tmptbl.Rows(0).Item("LastChangeStaffNum").ToString)
            sName = IsNull(drw(0)("StaffFirstNameFirst"), "unk name")
        Catch ex As Exception
            sName = "unk name"
        End Try
        Return ({("This data has CHANGED since you opened it.   " & NextLine &
              "         Changed by: " & sName & " at " & IsNull(sDate, "unk")).ToString, UCase(sName.Substring(0, sName.IndexOf(" ")))}) '
        '   "     If you proceed you will OVERWRITE the new data in the database.     " & NextLine &
        sDate = Nothing
        sName = Nothing
        drw = Nothing

    End Function

    'get back end data
    Private Function GetCurrentRowInDB_2(ByRef tmptbl As Data.DataTable, ByVal da As SqlDataAdapter) As System.Data.DataRow 'dsMainGrant.MainGrantRow
        '--------------------------------------------------------------------------
        ' This method loads a temporary table with current records from the database
        ' and returns the current values from the row that caused the exception.
        '--------------------------------------------------------------------------
        da.Fill(tmptbl)
        Dim currentRowInDb As System.Data.DataRow = tmptbl.Rows(0) 'FindByGrantIDTxt(ThisID)
        Return currentRowInDb
    End Function

    'compare column by column between BE and FE
    Private Function GetRowData_3(ByVal custRow As System.Data.DataRow, ByVal RowVersion As Data.DataRowVersion, ByRef mainTbl As Data.DataTable, ByRef tmptbl As Data.DataTable) As StringBuilder
        Dim x As String
        Dim rowData As New StringBuilder
        'my version - collects only changed columns
        For i As Integer = 0 To mainTbl.Columns.Count - 1
            x = UCase(custRow.Table.Columns(i).ColumnName)

            If UCase(IsNull(mainTbl.Rows(0).Item(i, DataRowVersion.Current), String.Empty)) = UCase(IsNull(tmptbl.Rows(0).Item(i), String.Empty)) Then
            Else
                rowData.Append(NextLine & NextLine & ">" & x & NextLine)
                rowData.Append("   -YOUR DATA: " & IsNull(mainTbl.Rows(0).Item(i, DataRowVersion.Current), String.Empty) & NextLine)
                rowData.Append("   - DATABASE : " & IsNull(tmptbl.Rows(0).Item(i), String.Empty))
                ' rowData &= NextLine & x & NextLine &
                ' " -YOUR DATA: " & IsNull(maintbl.Rows(0).Item(i, DataRowVersion.Current), String.Empty) & NextLine &
                ' " -DATABASE: " & IsNull(Temptbl.Rows(0).Item(i), String.Empty)
                '  " -ORIGINAL:  " & IsNull(maintbl.Rows(0).Item(i, DataRowVersion.Original), String.Empty) & NextLine &
            End If
        Next

        '--original, collects all columns for each rowVersion iteration -------------------------------------------------------------
        ' This method takes a CustomersRow and RowVersion 
        ' and returns a string of column values to display to the user.
        '--------------------------------------------------------------------------
        'For i As Integer = 0 To custRow.ItemArray.Length - 1
        '    '   If IsNull(custRow.Item(i, DataRowVersion.Original), String.Empty) = IsNull(custRow.Item(i, DataRowVersion.Current), String.Empty) Then
        '    '  Else
        '    x = custRow.Table.Columns(i).ColumnName
        '    rowData &= "; " & x & ": " & custRow.Item(i, RowVersion).ToString() & " "
        '    '  End If
        'Next
        Return rowData
        rowData = Nothing
    End Function

    'set text for message instructions
    Private Function GetMsgInstructions(ByVal sName As String) As String
        Return " =================== " & NextLine &
            "YES will REFRESH your screen with current Database values," & NextLine & "    your changes will be LOST." & NextLine &
            "NO  will keep your screen as it is." &
            NextLine & " =================== " & NextLine &
            "SUGGESTIONS: if you are making a simple change, click YES " & NextLine & "  then redo your change after the screen refreshes." & NextLine &
            "If you click NO, you can copy your proposed changes to some place like Notepad, then exit again, this time clicking Yes.  Now paste your changes back onto the refreshed screen." & NextLine & NextLine &
            "Discard your changes and Refresh your screen with current data?"

        '   "YES will OVER-WRITE database with Your data and close Detail window." & NextLine &
    End Function

    'ask user overwrite
    Public Function ProcessDialogResult_4(ByVal response As Windows.Forms.DialogResult, ByRef mainTbl As Data.DataTable, ByVal da As SqlDataAdapter, ByRef tmptbl As Data.DataTable) As usrInput
        ' This method takes the DialogResult selected by the user and 
        '• updates the BE database with the new values or 
        '• clears changes and resets the table in the FE dataset with the values currently in the database and cancels close or
        '• just cancels close
        Select Case response
            'Case Windows.Forms.DialogResult.Yes 'overwrite backend
            '    mainTbl.Merge(tmptbl, True)
            '    da.Update(mainTbl)
            '    ' SetStatusBarText("update done")
            '    Return usrInput.Overwrite
            Case Windows.Forms.DialogResult.Yes 'reset Front end
                mainTbl.Merge(tmptbl)
                ' SetStatusBarText("refreshing data")
                Return usrInput.Reset
            Case Windows.Forms.DialogResult.No 'cancel close
                ' bDirty = True
                '  SetStatusBarText("update cancelled")
                Return usrInput.Cancel
        End Select
    End Function

    'adapted from https://msdn.microsoft.com/en-us/library/ms171936(v=vs.100).aspx
#End Region 'concurrency

#Region "Date Validation"

    'DELETE DATE on PARSE
    Public Sub DateParse(ByVal sender As Object, e As ConvertEventArgs) ' Handles dtBinding.Parse
        '7/14 CALLED BY AddHandler for each date text box
        'must addHandler in LOAD after binding created
        'SAMPLE   AddHandler Me.txtCaseOpen.DataBindings(0).Parse, AddressOf mdGlobalVar.DateParse
        '  MsgBox("global var parse")
        If e.Value.ToString = "" Then
            e.Value = DBNull.Value
            '  ElseIf sender.name = "fldAccessDt" Then
            'e.Value = FormatDateTime(e.Value, "MMMM dd, yyyy")
            '     sender.text = FormatDateTime(e.Value, "MMMM dd, yyyy")
        Else 'no need toconvert as not using format??
            Try
                e.Value = CDate(e.Value)
            Catch ex As Exception
                modGlobalVar.msg("ATTENTION: invalid date", e.Value.ToString, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End Try

            ' e.Value = CDate(e.Value).ToString("d")
            ' e.Value = FormatDateTime(e.Value, vbGeneralDate) 'ToShortDateString 
        End If
        'http://msdn2.microsoft.com/en-us/library/system.windows.forms.binding.format(d=robot).aspx
    End Sub

    'VALIDATE DATE - CALL (w ERROR PROVIDER)
    Public Function ValidateDateA(ByRef ctl As Control, ByRef ep As ErrorProvider) As Boolean
        '-- 7/14 CALLED BY VALIDATING method of each date text box
        'SAMPLE  e.Cancel = mdGlobalVar.ValidateDateA(sender, Me.ErrorProvider1)
        Select Case ValidateDateB(ctl)
            Case Is = MsgBoxResult.Ok
                ep.SetError(ctl, "")
                Return False 'mdGlobalVar.ValidateDateA = False
            Case Is = MsgBoxResult.Retry
                ep.SetError(ctl, "please enter a valid date")
                Return True 'mdGlobalVar.ValidateDateA = True
            Case Is = MsgBoxResult.Abort
                ep.SetError(ctl, "please check this date range")
                Return True 'mdGlobalVar.ValidateDateA = True
            Case Else
                ep.SetError(ctl, "")
                Return False 'mdGlobalVar.ValidateDateA = False
        End Select

    End Function 'A. validate date call

    'VALIDATE DATE - CALL (no ERROR PROVIDER)
    Public Function ValidateDateA(ByRef ctl As Control) As Boolean
        '-- 7/14 CALLED BY VALIDATING method of each date text box
        'SAMPLE  e.Cancel = mdGlobalVar.ValidateDateA(sender)
        Select Case ValidateDateB(ctl)
            Case Is = MsgBoxResult.Ok
                Return False 'mdGlobalVar.ValidateDateA = False
            Case Is = MsgBoxResult.Retry
                Return True 'mdGlobalVar.ValidateDateA = True
            Case Is = MsgBoxResult.Abort
                Return True 'mdGlobalVar.ValidateDateA = True
            Case Else
                Return False 'mdGlobalVar.ValidateDateA = False
        End Select
    End Function 'A. validate date call

    'VALIDATE DATE - DO 7/14
    Public Function ValidateDateB(ByRef ctl As Control) As MsgBoxResult
        '-- CAlled by mdGlobalVar.ValidateDateA
        Select Case ctl.Text
            Case Is = String.Empty
                Return MsgBoxResult.Ok
            Case Else
                If IsDate(ctl.Text) Then
                    Dim d As Date = CType(ctl.Text, Date)
                    If d > CType("1/1/1997", Date) And d < CType("12/31/2099", Date) Then
                        Return MsgBoxResult.Ok
                    Else
                        Return MsgBoxResult.Abort
                    End If
                Else
                    Return MsgBoxResult.Retry
                End If
        End Select
    End Function 'validate date

    'DELETE DATE no status bar
    Public Function DeleteDate(ByVal sender As Control, ByVal col As Object) As Boolean
        Dim b As String = sender.DataBindings.Item(0).PropertyName.ToString    '.text or .value for datatimepicker
        Dim txt As String '= IsNull(sender.DataBindings("text").value.ToString, "") ' sender.Text 

        Select Case b
            Case Is = "Text"
                txt = IsNull(sender.Text, "")
                'Case "Value"
                '     txt = IsNull(sender.value, "")
        End Select

        If IsNull(col, "") = txt Then
            DeleteDate = False
            GoTo CloseAll
        End If

        If (txt = Nothing Or txt = " " Or txt = String.Empty) Then
            '   modGlobalVar.Msg("null", , txt)
            Try
                sender.Text = "1/1/1911"
                'col = System.DBNull.Value
                DeleteDate = True

            Catch ex As Exception
                modGlobalVar.msg("ERROR: can't make null", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
                DeleteDate = False
            End Try
        Else
            '   modGlobalVar.Msg("date", , txt)
            DeleteDate = False
        End If

        'Try
        '    col = CType(txt, DateTime)
        '    Return True
        'Catch ex As Exception
        '    modGlobalVar.Msg(ex.Message, , "can't change date")
        '    Return False
        'End Try
        'End If
CloseAll:
        b = Nothing
        txt = Nothing
        Return DeleteDate
    End Function

    ''double click for current date
    'Public Sub DateDoubleClick(ByVal sender As Object, e As MouseEventArgs)
    '    sender.text = Now()
    'End Sub

#End Region 'eliminate time from dates

#Region "Currency"
    'GET CURRENCY FORMAT
    Public Sub FormatCurrencyFld(ByRef obj As Object, ByVal prop As String, ByRef ds As DataSet, ByRef fld As String)
        Dim b As Binding

        ' Creates the binding first. 
        b = New Binding(prop, ds, fld)
        ' Add the delegates to the event
        AddHandler b.Format, AddressOf GetCurrency
        'AddHandler b.Parse, AddressOf CurrencyStringToDecimal
        obj.DataBindings.Add(b)
    End Sub

    'FORMAT CURRENCY
    Private Sub GetCurrency(ByVal sender As Object, ByVal cevent As ConvertEventArgs)
        ' modGlobalVar.Msg("in get currency")
        If IsNumeric(cevent.Value) Then
            cevent.Value = Format(cevent.Value, "##,##0.00")
        Else
            ''    If InStr(cevent.Value, "$") > 0 Then
            ''        Replace(cevent.Value, "$", "")
            ''        modGlobalVar.Msg("$")
            ''    End If
            'modGlobalVar.Msg("This field will only accept numbers and a period.", MessageBoxIcon.Exclamation, "Not a valid entry")
        End If

    End Sub

#End Region

#Region "Crazy Things"
    'if a text box is read only, the forecolor has no effect
    'enabled = false even prevents tooltip from showing up
    'only text boxes have an undo
    'AcceptsReturn has no effect on multiline textbox if there is no default button on page
    'grid column alignment right has no margin at all; labels have too much of a margin
    'gridrowwidth changes and not related to entries
    '    MessageBox.Show("Row header width is: " & grdMain.RowHeaderWidth)
    '   modGlobalVar.Msg(Me.grdMain.TableStyles(0).RowHeaderWidth.ToString, , Me.grdMain.TableStyles(1).RowHeaderWidth.ToString)
    'square root sign turns itself into a v on frmResources
    'menu items do not generate a sender.name
    'datagridview will not ...
    'VIEWs can be sorted, but the sort is lost if accessed through a STORED PROCEDURE
    'AutoComplete doesn't work well with slashes
    'RichTextBox TABS are by PIXEL
    'to concatenate styles in modGlobalVar.Msg (combine question mark with yes/no, and select default button), use OR !! not add!
    '    Dim style As modGlobalVar.MsgStyle
    '    style = modGlobalVar.MsgStyle.DefaultButton2 Or MessageBoxIcon.Error Or MessageBoxButtons.YesNo
    '    response = modGlobalVar.Msg(msg, style, title)

    'COMBOBOX events not triggered depending on how user get there:
    'The documentation for the ComboBox.SelectionChangeCommitted Event (http://msdn.microsoft.com/en-us/library/system.windows.forms.combobox.selectionchangecommitted(v=vs.110).aspx) 
    'does not accurately describe the behavior for a ComboBox that displays as a DropDownList. 
    '    There are methods that a user can change the selection which do not trigger the Event. 
    '    How to reproduce: open the drop down of a ComboBox with either a mouse click or alt+down, use the up or down arrow key to change the selection, and press ESC, click the control (not the drop down), or click outside of the control. 
    '    I consider that to be a user making the change as the MSDN Remarks describe. It makes sense be the name of the Event that it shouldn't be fired when the user doesn't finalize his choice. 
    'What's even more misleading on MSDN is "Do not use SelectedIndexChanged or SelectedValueChanged to capture user changes, because those events are also raised when the selection changes programmatically." What do I use to capture a user change then? Obviously not SelectionChangeCommitted because that's not always fired. 


#Region "sign datagrid vs datagridview"
    'changes req'd re DATAGRID VS DATAGRIDVIEW
    '.item(row, col); now is .item(col,row)!!!
    '.item(r,c); now is .item(c,r).value
    '.currentRowIndex; now is .currentRow.Index
    '.CurrentCell.ColumnNumber; now is .currentCell.ColumnIndex 
    '.select(row); now is .row(index).selected = true
    '.unSelect(row); now is .ClearSelection
    '.captionText; now no caption at all
    '.tablestyle; now no tablestyles
    '.navigateback, (which shouldn't actually be doing anything if is no parent table); now does not exist
    '.hti dimmed as datagrid.hittestinfo; now must be dimmed as datagridview.hittestinfo
    '.hti.column; now .hti.columnindex
    '.mapping name; now 'datapropertyname
    'selectionChanged now is triggered by leaving as well as entering!!!

    'ADVANTAGE of DATAGRIDVIEW for this application:
    'multiple row selection allowed
    'user can edit without potential of adding

    'ADVANTAGE of DATAGRID
    'faster in most cases
    'tablestyles, so can have more than one layout per grid
    '....................................
#End Region
#End Region

#Region "Misc"
    '------------------
    'CONTEXT MENU FOR REGION INSTEAD OF USING TABLE AND COMBO BOX
    '-------------------
    'Public Sub AddRegionMenu()

    '    ''Dim mni As New MenuItem
    '    'ppRegion.MenuItems.Add("Central")
    '    'ppRegion.MenuItems.Add("NE")

    'End Sub

    'LOAD DATASET FOR cboREGION ON ALL FORMS

    'Public Sub LoadRegionAll()
    '    drAll = dtAll.NewRow
    '    dr(0) = "All Regions"
    '    dt.Rows.InsertAt(dr, 0)
    '    dt.Columns.Add("SatelliteRegion", GetType(String))
    '    dsRegion.Tables.Add(dt)

    '    dr = dt.NewRow()
    '    dr!Region = "Central"
    '    dt.Rows.Add(dr)
    '    dr = dt.NewRow()
    '    dr!Region = "NE"
    '    dt.Rows.Add(dr)
    '    dr = dt.NewRow()
    '    dr!Region = "NW"
    '    dt.Rows.Add(dr)
    '    dr = dt.NewRow()
    '    dr!Region = "SC"
    '    dt.Rows.Add(dr)
    '    dr = dt.NewRow()
    '    dr!Region = "SW"
    '    dt.Rows.Add(dr)

    'End Sub
    ''CHANGE COLOR OF ADDRESS PANEL DEPENDING ON SATELLITE REGION
    'Private Sub cboRegion_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocboRegion.SelectedIndexChanged
    '    'Me.editOrgName.BackColor() = mdGlobalVar.RegionColor(Me.cboRegion.SelectedItem)
    '    ' Me.BackColor = mdGlobalVar.RegionColor(Me.cboRegion.SelectedItem)
    '    'cboRegion.BackColor = mdGlobalVar.RegionColor(Me.cboRegion.SelectedItem)
    '    Me.pnlAddress.BackColor = mdGlobalVar.RegionColor(Me.cboRegion.SelectedItem)

    'End Sub

    'ENHANCE: CAPTURE EDITS AND HIGHLIGHT CONTROL COLOR on all forms

    ''CHANGE COLOR OF ADDRESS PANEL DEPENDING ON SATELLITE REGION
    'Private Sub cboRegion_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocboRegion.SelectedIndexChanged
    '    'Me.editOrgName.BackColor() = mdGlobalVar.RegionColor(Me.cboRegion.SelectedItem)
    '    ' Me.BackColor = mdGlobalVar.RegionColor(Me.cboRegion.SelectedItem)
    '    'cboRegion.BackColor = mdGlobalVar.RegionColor(Me.cboRegion.SelectedItem)
    '    Me.pnlAddress.BackColor = mdGlobalVar.RegionColor(Me.cboRegion.SelectedItem)
    '..............
    'LoadRegionCombo:-not editable so is readonly label
    '        For i = 1 To colRegionlu.Count - 1    '
    '            Me.cboRegion.Items.Add(colRegionlu(i + 1))
    'Next
    '................


    'UPDATE RELATED TABLES IN DATASET
    '    This example shows how to send updates from a dataset that contains two related data tables.

    'Example
    '    Private Sub UpdateDB()
    '        Dim DeletedChildRecords As DataTable = _
    '            DsNorthwind1.Orders.GetChanges(DataRowState.Deleted)
    '        Dim NewChildRecords As DataTable = _
    '            DsNorthwind1.Orders.GetChanges(DataRowState.Added)
    '        Dim ModifiedChildRecords As DataTable = _
    '            DsNorthwind1.Orders.GetChanges(DataRowState.Modified)
    '        Try
    '            If Not DeletedChildRecords Is Nothing Then
    '                daOrders.Update(DeletedChildRecords)
    '                DeletedChildRecords.Dispose()
    '            End If
    '            daCustomers.Update(DsNorthwind1, "Customers")
    '            If Not ModifiedChildRecords Is Nothing Then
    '                daOrders.Update(ModifiedChildRecords)
    '                ModifiedChildRecords.Dispose()
    '            End If
    '            If Not NewChildRecords Is Nothing Then
    '                daOrders.Update(NewChildRecords)
    '                NewChildRecords.Dispose()
    '            End If
    '            DsNorthwind1.AcceptChanges()
    '        Catch ex As Exception
    '            ' Update error, resolve and try again.
    '            MessageBox.Show("Error during update")
    '        End Try
    '    End Sub
    'Compiling the Code
    'This example requires: 

    'References to the System and System.Data namespaces. 
    'A typed dataset named DsNorthwind1. 
    'Two DataTable objects named Customers and Orders in the DsNorthwind1 dataset. 
    'Two data adapters named daCustomers and daOrders. 
    '........................................................

    'End Sub
#End Region

#Region "Application Setup"

    '"Class Hide Close X " 'not required with new SaveExit button
    Class CloseButton

        Private Declare Function EnableMenuItem Lib "user32" (ByVal menu As Integer, ByVal ideEnableItem As Integer, ByVal enable As Integer) As Integer

        Private Const SC_CLOSE As Integer = &HF060
        Private Const MF_BYCOMMAND As Integer = &H0
        Private Const MF_GRAYED As Integer = &H1
        Private Const MF_ENABLED As Integer = &H0

        Private Sub New()
        End Sub


        'THIS WORKS BUT FORMS NO LONGER CENTER SCREEN
        Public Declare Function GetSystemMenu Lib "user32" (ByVal hwnd As Integer, ByVal bRevert As Integer) As Integer
        Public Declare Function RemoveMenu Lib "user32" (ByVal hMenu As Integer, ByVal nPosition As Integer, ByVal wFlags As Integer) As Integer
        Private Const MF_BYPOSITION As Integer = &H400

        Public Shared Sub Disable(ByRef frm As System.Windows.Forms.Form)
            Dim hSysMenu As Integer
            hSysMenu = GetSystemMenu(frm.Handle.ToInt32, 0)
            RemoveMenu(hSysMenu, 6, MF_BYPOSITION)
            RemoveMenu(hSysMenu, 5, MF_BYPOSITION)
        End Sub

    End Class

    ' This will create a Application Reference file on the users desktop
    Public Sub CreateShortcut()
        ' <summary>
        ' This will create a Application Reference file on the users desktop
        ' if they do not already have one when the program is loaded.
        ' Check for them running the deployed version before doing this,
        ' so it doesn't kick it when you're running it from Visual Studio.
        ' Need to import: System.Deployment.Application and System.Reflection </summary>

        If (System.Deployment.Application.ApplicationDeployment.IsNetworkDeployed) Then
            Dim ad As ApplicationDeployment = ApplicationDeployment.CurrentDeployment
            If (ad.IsFirstRun) Then 'first time user has run the app since installation or update
                Dim code As Assembly = Assembly.GetExecutingAssembly()
                Dim company As String = String.Empty
                '  Dim description As String = String.Empty
                '  Dim strTitle As String = String.Empty
                If (Attribute.IsDefined(code, GetType(AssemblyCompanyAttribute))) Then
                    Dim ascompany As AssemblyCompanyAttribute = _
                        CType(Attribute.GetCustomAttribute(code, _
                        GetType(AssemblyCompanyAttribute)), AssemblyCompanyAttribute)
                    company = ascompany.Company
                End If
                '  modGlobalVar.Msg(company, , "Company")
                'If (Attribute.IsDefined(code, GetType(AssemblyDescriptionAttribute))) Then
                '    Dim asdescription As AssemblyDescriptionAttribute = _
                '        CType(Attribute.GetCustomAttribute(code, _
                '        GetType(AssemblyDescriptionAttribute)), AssemblyDescriptionAttribute)
                '    description = asdescription.Description
                'End If
                'modGlobalVar.Msg(description, , "description")
                'strTitle = My.Application.Info.ProductName
                '   modGlobalVar.Msg(Title, , "Product Name")
                If (company.Length > 0 AndAlso gTitle.Length > 0) Then
                    Dim desktopPath As String = String.Empty
                    desktopPath = String.Concat( _
                        Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "\", gTitle, ".appref-ms")
                    Dim shortcutName As String = String.Empty
                    shortcutName = String.Concat( _
                        Environment.GetFolderPath(Environment.SpecialFolder.Programs), "\", company, "\", gTitle, ".appref-ms")
                    '       modGlobalVar.Msg(shortcutName, , "shortcut name")
                    Try
                        System.IO.File.Copy(shortcutName, desktopPath, True)
                    Catch ex As Exception
                        '  modGlobalVar.Msg(ex.Message, , "error copying shortcut")
                    End Try
                End If
                'Else
                'modGlobalVar.Msg("not first time")
            End If
            'Else
            'modGlobalVar.Msg("not network deployed")
        End If

        'The code basically looks for the shortcut on the start menu and copies it to the desktop. To locate the entry on the start menu, it has to have the name of the folder and the name of the shortcut itself. 
        'In a ClickOnce deployment, the Publisher Name is used for the name of the folder, and the Product Name is used for the name of the actual shortcut. 
        'Rather than hardcoding those values, the code for the shortcut uses Reflection to retrieve the corresponding information from the Assembly. It assumes the 
        '----------- Assembly Company matches the Publisher Name, and the Assembly Description matches the Product Name.
        'From the error you are showing here, that’s not the case. It looks like it can’t find the shortcut it wants to copy. So check your Assembly information against your Publish Options and make those two values match, and it should work.

    End Sub

#End Region

End Module

'