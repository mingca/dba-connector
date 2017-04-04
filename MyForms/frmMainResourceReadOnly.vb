Imports System.Data.SqlClient
Imports System

'=========== cg 10/15 =================================
'only afew staff can fully edit a resource once it's been marked OnCRG.
'controlled by luStaff MemoDuties (newCRGFull, newCRGEdit)
'only Referral Source, PrivDescription and the Add related records are permitted for all
'other changes would trigger an upload to CRG website, so LastChangeDate is not saved on this form.
'======================================================
Public Class frmMainResourceReadOnly
    Inherits System.Windows.Forms.Form
    Public isLoaded As Boolean = False

    '===== for Detail form  closing routines
    Dim ctlIdentify As Control  'field for delete and messages
    Dim ctlNeutral As Control  'will never be invalidated
    Dim objHowClose As Short  ' structCloseMethod 'Object = Me.btnSaveExit 'identify object calling close
    Dim mainDS As DataSet  'for generic module calls like CloseDetailForm
    Const mainTopic As String = "ReadOnly Resource" 'name of entity of this form - case, contact, etc
    Dim mainBSrce As System.Windows.Forms.BindingSource
    Public mainTbl As DataTable
    Public ThisID As Integer


    Dim enumRecommend, enumFeedback, enumAlert, enumAuthor, emunLocation As structHeadings
    Dim GetResourceLocation As New DataTable("GetResourceLocation")
    Dim GetResourceExtra As New DataTable("GetResourceExtra")
    Dim ppFile As New ContextMenu
    Dim ppImage As New ContextMenu
    Dim ehFile As EventHandler = AddressOf ehOpenFile
    Dim ehImage As EventHandler = AddressOf ehOpenImage
    Const DocPath As String = LinkedPath & "Resources\"
    Const ImagePath As String = LinkedPath & "ResourceImages\"

#Region "Initialize"
    Public Sub New()
        MyBase.new()

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
        End Try
        Forms.Remove(Me)

    End Sub
#End Region

#Region "Load"

    'LOAD
    Private Sub frmMainResourceReadOnly_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Me.SuspendLayout()
        SetStatusBarText("loading")
setDefaults:
        enumRecommend = New structHeadings("Recommendation", "RECOMMENDATIONS")
        enumFeedback = New structHeadings("Feedback", "FEEDBACK")
        enumAlert = New structHeadings("Alert", "ALERTS")

        ctlIdentify = Me.txtResourceName
        ctlNeutral = Me.btnNew
        mainDS = Me.DsMainResourceReadOnly1
        mainTbl = Me.DsMainResourceReadOnly1.tblResource
        mainBSrce = Me.MainResourceBindingSource

        'Hide grid nulls
        Dim tbs As DataGridTableStyle
        Dim tbx As DataGridTextBoxColumn
        Dim sType As String
        For Each pg As TabPage In Me.tbMain.TabPages
            For Each ctl As Control In pg.Controls
                sType = ctl.GetType.ToString
                If sType = "System.Windows.Forms.DataGrid" Then
                    For Each tbs In CType(ctl, DataGrid).TableStyles
                        ' For Each tbs In Me.grdExtras.TableStyles
                        For Each tbx In tbs.GridColumnStyles
                            tbx.TextBox.Enabled = False
                            tbx.NullText = ""
                        Next
                    Next
                Else
                End If
            Next
        Next

        Forms.Add(Me)
        Me.ResumeLayout()

        isLoaded = True
        SetStatusBarText("Done")

    End Sub

    'RELOAD
    Public Sub Reload()

        SetStatusBarText("Reloading")
        LoadIndexTerms()
        LoadKeywordList()
        SetInactiveFlag()

        objHowClose = ObjClose.CloseByControl '(=5: form X)
        Me.StatusBarPanelID.Text = mainTopic & " ID: " & ThisID.ToString
        Me.lblEntityType.Text = mainTopic
        Me.lblEntityNum.Text = ThisID.ToString
        Me.ToolTip1.SetToolTip(Me.lblLink, txtURL.Text)

findfiles:
        FindFiles()
        FindImages()

LoadLocationGrid:
        Dim sqlLocate As New SqlCommand("[GetResLocation]", sc)
        sqlLocate.CommandType = CommandType.StoredProcedure
        sqlLocate.Parameters.Add("@IDVal", SqlDbType.Int).Value = ThisID
        Try
            modGlobalVar.LoadDataTable(GetResourceLocation, sqlLocate)
        Catch ex As System.Exception
            modGlobalVar.msg("ERROR: loading locations  ", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
        End Try
        Me.grdLocation.DataSource = GetResourceLocation

LoadExtraGrid:
        'Reset defaults re multiple column width table styles
        Me.tsContacts.MappingName = ""
        Me.tsEvents.MappingName = ""
        Me.tsResourceName.MappingName = ""
        Me.tsAuthors.MappingName = ""
        Select Case Me.lblType.Text
            Case Is = "Article", "Book", "Web Resource", "Periodical"
                Me.tsAuthors.MappingName = "getResourceExtra"
                Me.grdExtras.CaptionText = "                    Authors/Editors"
                Me.lblLink.Text = "Resource Link"
            Case Is = "Event"
                Me.tsEvents.MappingName = "getResourceExtra"
                Me.grdExtras.CaptionText = "                    Event Dates/Locations"
            Case Is = "Media"
                Me.tsAuthors.MappingName = "getResourceExtra"
                Me.grdExtras.CaptionText = "                    Presenters"
            Case Is = "Organization"
                Me.tsContacts.MappingName = "getResourceExtra"
                Me.grdExtras.CaptionText = "                    Contacts"
            Case Is = "Periodical"
                Me.grdExtras.Visible = False
            Case Is = "Person"
                Me.tsResourceName.MappingName = "getResourceExtra"
                Me.grdExtras.CaptionText = "              Resource Name - Last, First"
            Case Else
                'modGlobalVar.Msg(lblType.Text, , "type not found")
        End Select

        Dim sqlExtra As New SqlCommand("[GetResourceExtraRO]", sc)
        sqlExtra.CommandType = CommandType.StoredProcedure
        sqlExtra.Parameters.Add("@ID", SqlDbType.Int).Value = ThisID
        Try
            modGlobalVar.LoadDataTable(GetResourceExtra, sqlExtra)
        Catch ex As System.Exception
            modGlobalVar.msg("ERROR: loading extras  ", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
        End Try
        Me.grdExtras.DataSource = GetResourceExtra
        SelectAddressTab()

CloseAll:
        sqlLocate = Nothing
        sqlExtra = Nothing
        SetStatusBarText("Done reloading")

    End Sub

    'VISIBILITY OF ADDRESS TAB CONTENTS
    Private Sub SelectAddressTab()

        Select Case IsNull(Me.lblType.Text, "")
            Case Is = "Organization", "Person", "Event"
                '  Me.lblAddressRequired.Visible = True
                Me.tbSource.SelectedTab = Me.oldAddress

            Case Is = "Equipment" 'retired
                '  Me.lblAddressRequired.Visible = True
                Me.tbSource.SelectedTab = Me.oldAddress

            Case "Article"
                Me.tbSource.SelectedTab = Me.pgPublish
                Me.tbPublishing.SelectedTab = Me.pgArticle
                '  Me.lblAddressRequired.Visible = False

            Case "Book", "Software", "Periodical"
                Me.tbSource.SelectedTab = Me.pgPublish
                Me.tbPublishing.SelectedTab = Me.pgBook
                '  Me.lblAddressRequired.Visible = False

            Case "Media"
                Me.tbSource.SelectedTab = Me.pgPublish
                Me.tbPublishing.SelectedTab = Me.pgMedia
                ' Me.lblAddressRequired.Visible = False

            Case Else 'WebResource
                Me.tbSource.SelectedTab = Me.oldAddress
        End Select

        'SPONSORING ORG FIELD
        Select Case IsNull(lblType.Text, "")
            Case Is = "Event"
                Me.lblAuxOrgName.Text = "Sponsoring Organization Name"
                Me.txtSponsoringOrg.Visible = True
                '  Case Is = "Person"
                '   Me.lblAuxOrgName.Text = "optional Company Name"
                '   Me.txtSponsoringOrg.Visible = True
            Case Else
                Me.lblAuxOrgName.Text = ""
                Me.txtSponsoringOrg.Visible = False
        End Select
    End Sub

    Private Sub FindFiles()
        'IMAGES
        modPopup.FindFiles(ThisID, ImagePath, ppImage, ehImage, Nothing, Me.btnImage, Me.btnImage.Image, Me.ToolTip1)
    End Sub

    Private Sub FindImages()
        'IMAGES
        modPopup.FindFiles(ThisID, ImagePath, ppImage, ehImage, Nothing, Me.btnImage, Me.btnImage.Image, Me.ToolTip1)
    End Sub

    'GET INDEX LIST
    Private Sub LoadIndexTerms()

        Dim sql As New SqlCommand("SELECT dbo.fDynamicString('ResourceIndexTerms', DEFAULT, " & ThisID & ") AS IndexTerms", sc)
        If Not SCConnect() Then
            Exit Sub
        End If
        Try
            fldIndexTerms.Text = sql.ExecuteScalar.ToString
        Catch ex As System.Exception
            modGlobalVar.msg("ERROR: index reload IndexTems ", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        sc.Close()
    End Sub

    'GET KEYWORD LIST
    Private Sub LoadKeywordList()

        Dim sql As New SqlCommand("SELECT dbo.fDynamicString('ResourceCRGs', DEFAULT, " & ThisID & ") AS KeyWords", sc)
        If Not SCConnect() Then
            Exit Sub
        End If
        Try
            Me.lblKeyWords.Text = sql.ExecuteScalar.ToString
        Catch ex As System.Exception
            modGlobalVar.msg("ERROR: index reload Keywords  ", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        sc.Close()
    End Sub

    'MAKE INACTIVE FLAG VISIBLE/INVISIBLE
    Private Sub SetInactiveFlag()
        Me.flagInactive.Visible = Not Me.chkActive.Checked
    End Sub

#End Region 'load

#Region "Update Close"

    'SAVE & EXIT (prevent exit if required field missing)
    Private Sub btnSaveExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
      Handles btnSaveExit.Click
        objHowClose = ObjClose.btnSaveExit
        Me.Close()
    End Sub

    'CLOSING & ask user & data validation & Release Form
    Private Sub frm_Closing(ByVal sender As Object, ByVal e As FormClosingEventArgs) _
               Handles MyBase.FormClosing

        Dim bChanges As Boolean

        'If objHowClose = ObjClose.miDelete Then
        '    GoTo callupdate
        'End If

LookForChanges:
        bChanges = modGlobalVar.AnyChanges(ctlNeutral, mainBSrce, mainTbl)

CheckCallingMethod:
        If objHowClose = ObjClose.miAskSave Then
            Select Case AskAcceptChanges(bChanges, mainTopic)
                Case Is = ObjClose.cancelClose
                    e.Cancel = True
                    GoTo ReleaseForm
                Case Is = ObjClose.DontSaveClose
                    ' GoTo validate ' notify of missing fields??
                    e.Cancel = False
                    GoTo ReleaseForm
                Case Is = ObjClose.SaveClose
                    e.Cancel = False
                    GoTo callupdate
            End Select

        End If
SkipUpdate:  'so LastChangeDate doesn't get updated
        If bChanges = False Then
            e.Cancel = False
            GoTo ReleaseForm
        End If
CallUpdate:
        MainResourceBindingSource.Current("LastChangeStaffNum") = usr

        If DoUpdate() Then
            e.Cancel = False
        Else                'update didn't work
            e.Cancel = True 'don't close form
            GoTo ReleaseForm
        End If

VALIDATE:  'check required fields; allow user to leave anyway if used menu
        Select Case objHowClose
            Case Is = ObjClose.DontSaveClose, ObjClose.miDelete
                e.Cancel = False
                GoTo ReleaseForm
            Case Is = ObjClose.cancelClose
                e.Cancel = True
                GoTo ReleaseForm
            Case Else 'btnSaveExit, SaveClose,, ObjClose.noChanges

        End Select

ReleaseForm:
        If e.Cancel = False Then   'user OKs close form
            ClassOpenForms.frmMainResourceRO = Nothing 'reset global var
        Else
        End If
    End Sub

    'UPDATE
    Public Function DoUpdate() As Boolean
        Dim i As Integer

        MouseWait()
        mainBSrce.EndEdit() 'gets rid of proposed version

        ''when jane makes changes to CRGDescription, change date
        'If StaffEditCRG.Contains(usr) Then
        '    mainTbl.Rows(0)("LastChangeDate") = Today
        'End If

UpdateBackend:
        SetStatusBarText("Updating server 1")
        Try
            i = Me.tblResourceTableAdapter.Update(mainTbl)
            DoUpdate = True
        Catch ex As Exception
            modGlobalVar.msg("Error updating " & mainTopic, ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
            DoUpdate = False
        Finally
            '  modGlobalVar.Msg("main: " & i.ToString& NextLine &  "addresses: " & f.ToString, , "updated")
        End Try

CloseAll:
        SetStatusBarText("Update routine complete")
        MouseDefault()
    End Function

#End Region 'update

#Region "Menu Items"

    'ALLOW CLOSE WITHOUT SAVING
    Private Sub miCloseForm_Click(ByVal sender As Object, ByVal e As System.EventArgs) _
        Handles miClose.Click
        objHowClose = ObjClose.miAskSave
        Me.Close()
    End Sub

    'mi SAVE
    Private Sub miSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles miSave.Click
        objHowClose = ObjClose.miSave
        DoUpdate()
    End Sub

    'mi CANCEL CHANGES
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles miCancel.Click
        Try
            mainBSrce.CancelEdit()
            mainDS.RejectChanges()
        Catch ex As System.Exception
            modGlobalVar.msg("ERROR: cancelling changes ", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        SetStatusBarText("Changes Cancelled")
    End Sub

#End Region 'edit buttons

#Region "General"

    'SET STATUS BAR LEFT TEXT
    Private Sub SetStatusBarText(ByVal str As String)
        Me.StatusBarPanel1.Text = str
    End Sub

    'HELP BUTTON
    Private Sub btnHelp_Click(ByVal sender As Object, ByVal e As System.EventArgs) _
            Handles miHelpGeneral.Click, btnHelp.Click
        modGlobalVar.msg("HELP", "TO ADD NEW RESOURCE:" & NextLine & NextLine & "Go to Resource Search window and Click New button" + NextLine _
               & NextLine & "TO ADD CONTACT or AUTHOR or LOCATION or RECOMMENDATION etc.:" & NextLine & "Click the New button" + NextLine _
               + "TO EDIT CONTACT OR AUTHOR, double-click the name in the grid" + NextLine _
               + "TO EDIT RESOURCE NAME of a person type resource, use the Author/Contact grid to make changes. They will be reflected in the resource name when you exit the grid." + NextLine _
               & NextLine & "CRG WEBSITE FIELDS are restricted; you may not have authority to make changes (CRG Checkbox, Status Dropdown, and CRG Annotation)", MessageBoxButtons.OK, MessageBoxIcon.Information)

    End Sub

    'HELP: Resource Definitions
    Private Sub miDefinitions_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles miDefinitions.Click
        modPopup.ShowDefinitions("ResourceType")
    End Sub

    'HELP: CRG WEBSITE
    Private Sub miCRG_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles miCRG.Click
        modGlobalVar.msg("CRG WEBSITE HELP", "Only selected staff can edit CRG Resources.  Contact " & CRGAdmin.StaffName & "." & NextLine & NextLine &
               "The 'On CRG checkbox' is what signals a resource is ready for uploading to the CRG website", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    'UNDO
    Protected Overrides Function ProcessDialogKey(ByVal keyData As Keys) As Boolean
        If keyData = System.Windows.Forms.Keys.Escape Then
            ' If Me.ActiveControl Is Me.cboType Then  'required field
            ' Else
            modPopup.UndoCtl(Me.ActiveControl)
            ' bChanged = True
            'End If
            Return True  ' True means we've processed the key
        Else
            Return MyBase.ProcessDialogKey(keyData)
        End If
    End Function

    'COPY CURRENT ID
    Private Sub StatusBar1_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StatusBar1.DoubleClick
        Clipboard.SetText(ThisID)
    End Sub

    'DISPLAY STAFF NAME FROM ID
    Private Sub TextBox_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) _
             Handles fldReviewStaff.MouseHover, fldCreatedBy.MouseHover, fldChangeBy.MouseHover

        Me.ToolTip1.SetToolTip(sender, sender.tag & ": " & modPopup.ShowStaff(sender.text))

    End Sub

    'display mouse url
    Private Sub txtURL_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) _
    Handles txtURL.MouseHover ', txtWebsite.MouseHover
        MouseWeb()
    End Sub

    'restore mouse shape after url
    Private Sub txturl_mouseleave(ByVal sender As System.Object, ByVal e As System.EventArgs) _
       Handles txtURL.MouseLeave ', txtWebsite.MouseLeave
        MouseDefault()
    End Sub

    'open link from textbox
    Private Sub txtURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles txtURL.Click ', txtWebsite.Click
        modPopup.OpenWebsite(sender.text)
    End Sub

    'open link from RTB
    Private Sub rtbDescriptions_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkClickedEventArgs) _
Handles rtbDescriptions.LinkClicked
        modPopup.OpenWebsite(e.LinkText)
    End Sub

    'referral source used in xml and other reports so remove line feeds
    Private Sub txtReferralSource_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtReferralSource.Leave
        modGlobalVar.ReplaceLineFeeds(txtReferralSource)
    End Sub

#End Region 'general

#Region "Secondary Datasets"

    'GET TAB CAPTION COUNT
    Public Sub SetTabCaptions(ByVal ID As Integer)
SetTabCaptionsWCount:
        Dim cmdCntID As New SqlCommand
        Dim i As Integer = 0

        cmdCntID.Connection = sc
        If Not SCConnect() Then
            Exit Sub
        End If

        'RECOMMENDATIONS
        cmdCntID.CommandText = "SELECT COUNT(RecommendID) FROM tblResourceRecommend WHERE ResourceNum = " & ID
        i = 0
        Try
            i = cmdCntID.ExecuteScalar()
        Catch ex As Exception
            '      modGlobalVar.Msg(ex.Message)
        End Try
        Me.tbMain.TabPages(1).Text = i.ToString() & "  " & Me.tbMain.TabPages(1).Tag

        'FEEDBACK
        cmdCntID.CommandText = "SELECT COUNT(FeedbackID) FROM tblResourceFeedback WHERE ResourceNum = " & ID
        i = 0
        Try
            i = cmdCntID.ExecuteScalar()
        Catch ex As Exception
            '      modGlobalVar.Msg(ex.Message)
        End Try
        Me.tbMain.TabPages(2).Text = "   " & i.ToString() & "  " & Me.tbMain.TabPages(2).Tag & "     "

        'ALERTS
        cmdCntID.CommandText = "SELECT COUNT(WarningID) FROM tblResourceWarning WHERE ResourceNum = " & ID
        Try
            i = cmdCntID.ExecuteScalar()
        Catch ex As Exception
            '      modGlobalVar.Msg(ex.Message)
        End Try
        Me.tbMain.TabPages(3).Text = "   " & i.ToString() & "  " & Me.tbMain.TabPages(3).Tag & "   "
        If i > 0 Then Me.flagAlert.Visible = True

        sc.Close()
        cmdCntID.Dispose()

    End Sub

    'CALL FILL SECONDARY from tab control
    Private Sub TabControl1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbMain.SelectedIndexChanged
        If isLoaded Then
            FillSecondary()
        End If
    End Sub

    'FILL SECONDARY
    Private Sub FillSecondary()
        SetStatusBarText("Retrieving data")
        Dim cmd As New SqlCommand
        Dim tbl As DataTable
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Connection = sc
        cmd.Parameters.Add("@IDFld", System.Data.SqlDbType.VarChar, 30).Value = "ResourceNum"
        cmd.Parameters.Add("@IDVal", SqlDbType.Int).Value = ThisID

        Select Case tbMain.SelectedTab.Tag
            Case Is = enumRecommend.PluralName   '
                cmd.CommandText = "[GetResRecommendation]"
                tbl = New DataTable("GetResRecommendation")
                grdRecommend.DataSource = tbl
            Case Is = enumFeedback.PluralName 'strData2  '
                cmd.CommandText = "[GetResFeedback]"
                tbl = New DataTable("GetResFeedback")
                grdFeedback.DataSource = tbl
            Case Is = enumAlert.PluralName 'strData3  '
                cmd.CommandText = "[GetResAlert]"
                tbl = New DataTable("GetResAlert")
                grdAlert.DataSource = tbl
                'TODO add addresses
                '  Case Is = "Address"
            Case Else
                GoTo closeall
                Exit Sub
        End Select

        Try
            modGlobalVar.LoadDataTable(tbl, cmd)
        Catch ex As System.Exception
            modGlobalVar.msg("ERROR: loading " + tbMain.SelectedTab.Tag, ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
        End Try

CloseAll:
        StatusBar1.Panels(0).Text = "Done"
    End Sub

#End Region 'secondary datasets

#Region "Description Fields"

    'BIND rtbDescription to appropriate description field
    Private Sub tbDescriptions_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles tbDescriptions.SelectedIndexChanged
        Dim str As String
        MouseWait()
        If isLoaded Then
SetFieldBinding:
            str = Trim(tbDescriptions.SelectedTab.Tag)

            Me.rtbDescriptions.DataBindings.Clear()
            Me.rtbDescriptions.DataBindings.Add(New Binding("Text", Me.MainResourceBindingSource, str))

            ''ALLOW EDITS TO PRIVATE DESCRIPTION
            'If str = "PrivateDescription" Then
            '    Me.rtbDescriptions.ReadOnly = False
            'Else
            '    Me.rtbDescriptions.ReadOnly = True
            'End If
SetPermission:
            EnforceNewCRGPermission(str)
        End If

        str = Nothing
        MouseDefault()
    End Sub

    'ONLY SELECT FEW CAN EDIT 'NEW CRG ANNOTATION' see tblStaff
    Private Sub EnforceNewCRGPermission(ByVal str As String)
        If StaffCRGEdit.Contains(usr) Then
            Me.rtbDescriptions.ReadOnly = False
        Else
            If str = "NewCRGDescription" Then
                Me.rtbDescriptions.ReadOnly = True
            Else
                Me.rtbDescriptions.ReadOnly = False
            End If
        End If
    End Sub

    'label overlapping tab heading
    Private Sub btnPrivDescr_Click(sender As System.Object, e As System.EventArgs) Handles btnPrivDescr.Click
        Me.tbDescriptions.SelectedTab = Me.tbDescriptions.TabPages(0)
    End Sub

    'CALL RIGHT CLICK EDIT MENU
    Private Sub rtbDescriptions_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) _
        Handles rtbDescriptions.MouseDown ', txtResourceName.MouseDown

        If e.Button = Windows.Forms.MouseButtons.Right Then
            Dim pp As New ClassRTBContextMenu(sender)
            pp.Show(Me, PointToClient(Control.MousePosition))
        Else
        End If
    End Sub

#End Region 'description

#Region "Add Item"

    'POPUP ADD NEW Case, Contact, Grant
    Private Sub PopupNew(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles miNew.Click, btnNew.Click

        'popup menu asking for usr input

        Dim cm As New ContextMenu
        Dim eh As EventHandler = AddressOf AddNew

        cm.MenuItems.Add("SELECT ITEM TO ADD") 'TO: " + ctlIdentify.Text)
        cm.MenuItems.Add("_______________________________________")
        cm.MenuItems.Add("Recommendation", eh)
        cm.MenuItems.Add("Feedback", eh)
        cm.MenuItems.Add("Alert", eh)
        cm.MenuItems.Add("Location", eh)
        cm.MenuItems(2).DefaultItem = True
        cm.Show(Me, New Point(500, 10))

    End Sub

    'ADD NEW
    Private Sub AddNew(ByVal obj As Object, ByVal ea As EventArgs)
        Dim str As String
        Dim iType As Integer 'consultant vw admin

        SetStatusBarText("Adding new " & obj.text.ToString)
        If modGlobalVar.msg("About to enter a new " & obj.text.ToString, "Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then
            Exit Sub
        End If

        If usrType = "Consultant" Then
            iType = usr
        Else
            iType = 0   'could be admin entering from grant app so don't default to their name
        End If

        Select Case obj.text.ToString
            Case Is = enumRecommend.SingleName 'strData1 'recommendation
                str = "INSERT INTO tblResourceRecommend(ResourceNum, RecommendStaffNum, RecommendDate, Used) VALUES (" & ThisID & ", " & iType & ", GETDATE(), 'unknown'); SELECT @@Identity"

            Case Is = enumFeedback.SingleName 'feedback
                str = "INSERT INTO tblResourceFeedback(ResourceNum, FeedbackStaffNum, FeedbackDate) VALUES (" & ThisID & ", " & usr & ", GETDATE()); SELECT @@Identity"

            Case Is = enumAlert.SingleName 'warning
                str = "INSERT INTO tblResourceWarning(ResourceNum, WarningStaffNum, WarningDate) VALUES (" & ThisID & ", " & usr & ", GETDATE()); SELECT @@Identity"
                '=========== only Carol can add/edit these ======================
                '   Case Is = "Author/Editor" 'author
                '       str = "INSERT INTO tblResourceExtra (ResourceNum, AuthorEditor) VALUES (" & ThisID & ", 'Author'); SELECT @@Identity"

                'Case Is = "Author/Editor/Presenter" 'presenter
                '    str = "INSERT INTO tblResourceExtra (ResourceNum, AuthorEditor) VALUES (" & ThisID & ", 'Presenter'); SELECT @@Identity"

                'Case Is = "Author/Editor/Contact" 'periodical
                '    str = "INSERT INTO tblResourceExtra (ResourceNum, AuthorEditor) VALUES (" & ThisID & ", 'Editor'); SELECT @@Identity"

                'Case Is = "Event Info" 'Event
                '    str = "INSERT INTO tblResourceExtra (ResourceNum, AuthorEditor) VALUES (" & ThisID & ", 'Event'); SELECT @@Identity"

                '  Case Is = "Resource Contact" 'contact
                '      str = "INSERT INTO tblResourceExtra (ResourceNum, AuthorEditor) VALUES (" & ThisID & ",'Contact'); SELECT @@Identity"

            Case Is = "Location"
                'TODO Why isn't OrderWhen a date field?'location
                str = "INSERT INTO tblResourceLocation (ResourceNum, SatelliteRegion, OrderWhen, orderstaffnum) VALUES (" & ThisID & ", N'" & usrRegion & "', N'" & Now.ToString & "', " & usr & " ); SELECT @@Identity"

            Case Else
                modGlobalVar.msg("ERROR: 2 not found", obj.text.ToString, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
        End Select

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
            modGlobalVar.msg("ERROR: commit", exce.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
            myTrans.Rollback()
            sc.Close()
            Exit Sub
        End Try

        sc.Close()
OpenForm:
        Select Case obj.text.ToString    'UCase(TabControl1.SelectedTab.Tag)
            Case Is = enumRecommend.SingleName 'recommend
                modGlobalVar.OpenMainRecommend(newID, ctlIdentify.Text, 0)
            Case Is = enumFeedback.SingleName 'feedback
                modGlobalVar.OpenMainFeedback(newID, ThisID, ctlIdentify.Text, 0)
                'mdGlobalVar.OpenMainCase(newID, "Entering New Case", Me.editOrgName.Text & " : " & Me.editPhone.Text, Me.txtOrgID.Text)
            Case Is = enumAlert.SingleName 'warning
                modGlobalVar.OpenMainResourceWarning(newID, ThisID, ctlIdentify.Text)
            Case Is = "Location"
                modGlobalVar.OpenMainResourceLocation(newID, ThisID, ctlIdentify.Text, usrRegion)
        End Select
        SetStatusBarText("Done")
        'TODO update count on tab

    End Sub

#End Region 'add item

#Region "Attach Files"

    'COPY FILE to shared drive
    Private Sub miAttach_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles miAttach.Click
        MouseWait()
AttachFile:
        Try
            modPopup.AttachFiles("Resource", DocPath, ThisID)
        Catch ex As Exception
            modGlobalVar.msg("ERROR: attach RO resource files", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
ReloadList:
        FindFiles()
        'modPopup.FindFiles(ThisID, strDocPath, ppFile, ehFile, Me.miOpenFile, Me.btnOpenFile, My.Resources.btnAttached, Me.ToolTip1) ', Nothing)

        SetStatusBarText("Done")
        MouseDefault()

    End Sub

    'ATTACH/OPEN FILE
    Private Sub btnOpenFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnOpenFile.Click
        If sender.Tag.ToString = "Attach" Then
            Me.miAttach.PerformClick()
            ' ehOpenFile(sender, Nothing)
        Else
            ppFile.Show(Me, New Point(600, 10))
        End If
    End Sub

    'POPUP MENU HANDLER
    Private Sub ehOpenFile(ByVal obj As Object, ByVal ea As EventArgs)

        MouseWait()
        If obj.text = "Attach File" Then
            Me.miAttach.PerformClick()
        Else
            If OpenFile(DocPath & ThisID.ToString & " " & obj.text) = True Then
                SetStatusBarText("file opened")
            End If
        End If
        Exit Sub
        MouseDefault()

    End Sub

    'ATACH/OPEN IMAGE
    Private Sub btnImage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
      Handles btnImage.Click
        MouseWait()
        If sender.Tag.ToString = "Attach" Then
AttachFile:
            Try
                modPopup.AttachFiles("ResourceImage", ImagePath, ThisID)
            Catch ex As Exception
                modGlobalVar.msg("ERROR: attach RO image files 2", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
ReloadList:
            '   FindFiles(ThisID, strDocPath, ppFile, ehFile, Me.miOpenFile, Me.btnOpenFile, My.Resources.btnAttached, Me.ToolTip1) ', Nothing)
            FindImages()
            'modPopup.FindFiles(ThisID, strImagePath, ppImage, ehFile, Nothing, Me.btnImage, Me.btnImage.Image, Me.ToolTip1) ', Nothing)
        Else
            ppImage.Show(Me, New Point(600, 10))
        End If


        ' FindImages(ThisID, strImagePath, ppImage, ehFile, Me.btnImage)
        SetStatusBarText("Done")
        MouseDefault()

    End Sub

    'IMAGE POPUP  HANDLER
    Private Sub ehOpenImage(ByVal obj As Object, ByVal ea As EventArgs)
        MouseWait()
        If obj.text = "Attach File" Then
            Try
                modPopup.AttachFiles("ResourceImage", ImagePath, ThisID)
            Catch ex As Exception
                modGlobalVar.msg("ERROR: attach RO resource image files 3", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
ReloadList:
            FindImages()
            'modPopup.FindFiles(ThisID, strImagePath, ppImage, ehImage, Nothing, Me.btnImage, Me.btnImage.Image, Me.ToolTip1)
        Else
            If OpenFile(ImagePath & ThisID.ToString & " " & obj.text) = True Then
                SetStatusBarText("image file opened")
            End If
        End If
        MouseDefault()
    End Sub

#End Region 'attach files

#Region "Open Forms"

    'GRD EXTRAS DBLCLICK
    Private Sub grdExtras_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) _
        Handles grdExtras.DoubleClick
        modPopup.OpenPopupDGV("Resource Author/Contact/Details", GetResourceExtra, True)
    End Sub

    'GRID DOUBLE CLICK to Open Forms
    Private Sub grd_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) _
            Handles grdRecommend.DoubleClick, grdFeedback.DoubleClick, grdLocation.DoubleClick, grdAlert.DoubleClick

        If sender.CurrentRowIndex >= 0 Then
            If sender.Item(sender.CurrentRowIndex, 0).ToString > "" Then
                OpenForms(sender.name)
            Else 'data issue
            End If
        Else
            modGlobalVar.msg("ATTENTION: Insufficient data", "Select a row in the grid, or " & NextLine & "to enter new " + sender.name.ToString.Substring(3, Len(sender.name.ToString) - 3) + ": click the New button", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
    End Sub

    'OPEN FORMS
    Private Sub OpenForms(ByVal obj As String)
        MouseWait()
        SetStatusBarText("opening " & Me.tbMain.SelectedTab.Tag)

        If obj = "grdLocation" Then
            Try
                modGlobalVar.OpenMainResourceLocation(Me.grdLocation.Item(Me.grdLocation.CurrentRowIndex, 0), ThisID, ctlIdentify.Text, Me.grdLocation.Item(Me.grdLocation.CurrentRowIndex, 1))
            Catch ex As Exception
            End Try
        Else

            Select Case Me.tbMain.SelectedTab.Tag
                Case Is = enumRecommend.PluralName '"Recommendation"
                    modGlobalVar.OpenMainRecommend(Me.grdRecommend.Item(Me.grdRecommend.CurrentRowIndex, 0), ctlIdentify.Text, IsNull(Me.grdRecommend.Item(Me.grdRecommend.CurrentRowIndex, 2), 0))
                Case Is = enumFeedback.PluralName '"Feedback"
                    Dim i As Integer = IsNull(Me.grdFeedback.Item(Me.grdFeedback.CurrentRowIndex, Me.grdFeedback.TableStyles(0).GridColumnStyles.IndexOf(Me.grdFeedback.TableStyles(0).GridColumnStyles("OrgNum"))), 0)
                    modGlobalVar.OpenMainFeedback(Me.grdFeedback.Item(Me.grdFeedback.CurrentRowIndex, 0), ThisID, ctlIdentify.Text, i)
                Case Is = enumAlert.PluralName '"Alert"
                    modGlobalVar.OpenMainResourceWarning(Me.grdAlert.Item(Me.grdAlert.CurrentRowIndex, 0), ThisID, ctlIdentify.Text)
                Case Is = "RESOURCE"
                    'don't open this is read only due to CRG

            End Select
        End If
        SetStatusBarText("Done")
        MouseDefault()
    End Sub

    'CLEAR GRID SELECTION AND LOSE FOCUS (note: selection stays with arrow)
    Private Sub grdUnselect_Click(ByVal sender As Object, ByVal e As System.EventArgs) _
            Handles MyBase.Click, grdRecommend.LostFocus, grdExtras.LostFocus, grdFeedback.LostFocus, grdLocation.LostFocus

        If sender.GetType.ToString = "Windows.Forms.DataGrid" Then
            If sender.CurrentRowIndex > -1 Then
                sender.UnSelect(sender.CurrentRowIndex)
                sender.NavigateBack()
            End If
        Else
        End If
    End Sub

#End Region 'open forms

End Class