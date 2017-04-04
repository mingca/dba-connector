Imports System.Text
Imports System.Data.SqlClient

Public Class frmMainResourceWarning
    Inherits System.Windows.Forms.Form

    Public bChanged As Boolean = False
    Dim bCancelClose As Boolean = False
    Public isLoaded As Boolean = False
    Dim ctlIdentify As Control
    Dim ctlNeutral As Control
    Dim objHowClose As Short
    Dim mainDS As DataSet
    Dim mainTopic As String
    Dim mainDAdapt As SqlDataAdapter
    Public ThisID, LocalOrgID As Integer
    Dim mainBSrce As System.Windows.Forms.BindingSource

#Region "Inialize"
    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If

        MyBase.Dispose(disposing)
        Forms.Remove(Me)
    End Sub

#End Region 'initialize

#Region " Windows Form Designer generated code "

    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents btnSaveExit As System.Windows.Forms.Button
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents btnHelp As System.Windows.Forms.Button
    Friend WithEvents SqlSelectCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlUpdateCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlDeleteCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents daMainResourceWarning As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents DsMainResourceWarning1 As InfoCtr.dsMainResourceWarning
    Friend WithEvents rtbDetail As System.Windows.Forms.RichTextBox
    Friend WithEvents DtWarning As InfoCtr.DateTextBox
    Friend WithEvents cboStaff As InfoCtr.ComboBoxRelaxed
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtWarningID As System.Windows.Forms.TextBox
    Friend WithEvents MainMenu1 As System.Windows.Forms.MainMenu
    Friend WithEvents MenuItem1 As System.Windows.Forms.MenuItem
    Friend WithEvents miClose As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem2 As System.Windows.Forms.MenuItem
    Friend WithEvents miSave As System.Windows.Forms.MenuItem
    Friend WithEvents miCancel As System.Windows.Forms.MenuItem
    Friend WithEvents miDelete As System.Windows.Forms.MenuItem
    Friend WithEvents ErrorProvider1 As System.Windows.Forms.ErrorProvider
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents MainResourceWarningBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents StatusBar1 As System.Windows.Forms.StatusBar
    Friend WithEvents StatusBarPanel1 As System.Windows.Forms.StatusBarPanel
    Friend WithEvents StatusBarPanelID As System.Windows.Forms.StatusBarPanel
    Friend WithEvents StatusBarPanel2 As System.Windows.Forms.StatusBarPanel

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMainResourceWarning))
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.btnSaveExit = New System.Windows.Forms.Button()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.btnHelp = New System.Windows.Forms.Button()
        Me.SqlSelectCommand1 = New System.Data.SqlClient.SqlCommand()
        Me.SqlUpdateCommand1 = New System.Data.SqlClient.SqlCommand()
        Me.SqlDeleteCommand1 = New System.Data.SqlClient.SqlCommand()
        Me.daMainResourceWarning = New System.Data.SqlClient.SqlDataAdapter()
        Me.DsMainResourceWarning1 = New InfoCtr.dsMainResourceWarning()
        Me.rtbDetail = New System.Windows.Forms.RichTextBox()
        Me.MainResourceWarningBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.DtWarning = New InfoCtr.DateTextBox()
        Me.cboStaff = New InfoCtr.ComboBoxRelaxed()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtWarningID = New System.Windows.Forms.TextBox()
        Me.MainMenu1 = New System.Windows.Forms.MainMenu(Me.components)
        Me.MenuItem1 = New System.Windows.Forms.MenuItem()
        Me.miClose = New System.Windows.Forms.MenuItem()
        Me.MenuItem2 = New System.Windows.Forms.MenuItem()
        Me.miSave = New System.Windows.Forms.MenuItem()
        Me.miCancel = New System.Windows.Forms.MenuItem()
        Me.miDelete = New System.Windows.Forms.MenuItem()
        Me.ErrorProvider1 = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.Label5 = New System.Windows.Forms.Label()
        Me.StatusBar1 = New System.Windows.Forms.StatusBar()
        Me.StatusBarPanel1 = New System.Windows.Forms.StatusBarPanel()
        Me.StatusBarPanelID = New System.Windows.Forms.StatusBarPanel()
        Me.StatusBarPanel2 = New System.Windows.Forms.StatusBarPanel()
        Me.Panel3.SuspendLayout()
        CType(Me.DsMainResourceWarning1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MainResourceWarningBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.StatusBarPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.StatusBarPanelID, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.StatusBarPanel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.SystemColors.Highlight
        Me.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel3.Controls.Add(Me.btnSaveExit)
        Me.Panel3.Controls.Add(Me.btnDelete)
        Me.Panel3.Location = New System.Drawing.Point(756, 6)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(101, 40)
        Me.Panel3.TabIndex = 783
        '
        'btnSaveExit
        '
        Me.btnSaveExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSaveExit.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.btnSaveExit.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSaveExit.Image = CType(resources.GetObject("btnSaveExit.Image"), System.Drawing.Image)
        Me.btnSaveExit.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSaveExit.Location = New System.Drawing.Point(49, 0)
        Me.btnSaveExit.Name = "btnSaveExit"
        Me.btnSaveExit.Size = New System.Drawing.Size(40, 35)
        Me.btnSaveExit.TabIndex = 418
        Me.btnSaveExit.Text = "Close"
        Me.btnSaveExit.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSaveExit.UseVisualStyleBackColor = False
        '
        'btnDelete
        '
        Me.btnDelete.BackColor = System.Drawing.SystemColors.Control
        Me.btnDelete.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Image = CType(resources.GetObject("btnDelete.Image"), System.Drawing.Image)
        Me.btnDelete.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnDelete.Location = New System.Drawing.Point(3, 0)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(40, 35)
        Me.btnDelete.TabIndex = 416
        Me.btnDelete.Text = "Delete"
        Me.btnDelete.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnDelete.UseVisualStyleBackColor = False
        Me.btnDelete.Visible = False
        '
        'btnHelp
        '
        Me.btnHelp.Image = CType(resources.GetObject("btnHelp.Image"), System.Drawing.Image)
        Me.btnHelp.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnHelp.Location = New System.Drawing.Point(863, 6)
        Me.btnHelp.Name = "btnHelp"
        Me.btnHelp.Size = New System.Drawing.Size(25, 25)
        Me.btnHelp.TabIndex = 782
        Me.btnHelp.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnHelp.UseVisualStyleBackColor = True
        '
        'SqlSelectCommand1
        '
        Me.SqlSelectCommand1.CommandText = "dbo.MainResourceWarning"
        Me.SqlSelectCommand1.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlSelectCommand1.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.[Variant], 0, System.Data.ParameterDirection.ReturnValue, False, CType(0, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing), New System.Data.SqlClient.SqlParameter("@ID", System.Data.SqlDbType.NVarChar, 0, "WarningID")})
        '
        'SqlUpdateCommand1
        '
        Me.SqlUpdateCommand1.CommandText = "dbo.MainResourceWarningUpdate"
        Me.SqlUpdateCommand1.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlUpdateCommand1.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.[Variant], 0, System.Data.ParameterDirection.ReturnValue, False, CType(0, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing), New System.Data.SqlClient.SqlParameter("@ResourceNum", System.Data.SqlDbType.Int, 0, "ResourceNum"), New System.Data.SqlClient.SqlParameter("@WarningDate", System.Data.SqlDbType.[Date], 0, "WarningDate"), New System.Data.SqlClient.SqlParameter("@WarningDescription", System.Data.SqlDbType.VarChar, 0, "WarningDescription"), New System.Data.SqlClient.SqlParameter("@WarningStaffNum", System.Data.SqlDbType.Int, 0, "WarningStaffNum"), New System.Data.SqlClient.SqlParameter("@AlbanNum", System.Data.SqlDbType.Int, 0, "AlbanNum"), New System.Data.SqlClient.SqlParameter("@WarningStaffTxt", System.Data.SqlDbType.VarChar, 30, "WarningStaffTxt"), New System.Data.SqlClient.SqlParameter("@WarningType", System.Data.SqlDbType.VarChar, 30, "WarningType"), New System.Data.SqlClient.SqlParameter("@ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "WarningID", System.Data.DataRowVersion.Original, Nothing)})
        '
        'SqlDeleteCommand1
        '
        Me.SqlDeleteCommand1.CommandText = "dbo.MainResourceWarningDelete"
        Me.SqlDeleteCommand1.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlDeleteCommand1.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.[Variant], 0, System.Data.ParameterDirection.ReturnValue, False, CType(0, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing), New System.Data.SqlClient.SqlParameter("@ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "WarningID", System.Data.DataRowVersion.Original, Nothing)})
        '
        'daMainResourceWarning
        '
        Me.daMainResourceWarning.DeleteCommand = Me.SqlDeleteCommand1
        Me.daMainResourceWarning.SelectCommand = Me.SqlSelectCommand1
        Me.daMainResourceWarning.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "MainResourceWarning", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("WarningID", "WarningID"), New System.Data.Common.DataColumnMapping("ResourceNum", "ResourceNum"), New System.Data.Common.DataColumnMapping("WarningDate", "WarningDate"), New System.Data.Common.DataColumnMapping("WarningDescription", "WarningDescription"), New System.Data.Common.DataColumnMapping("WarningStaffNum", "WarningStaffNum"), New System.Data.Common.DataColumnMapping("AlbanNum", "AlbanNum"), New System.Data.Common.DataColumnMapping("WarningStaffTxt", "WarningStaffTxt"), New System.Data.Common.DataColumnMapping("WarningType", "WarningType"), New System.Data.Common.DataColumnMapping("Stamped", "Stamped")})})
        Me.daMainResourceWarning.UpdateCommand = Me.SqlUpdateCommand1
        '
        'DsMainResourceWarning1
        '
        Me.DsMainResourceWarning1.DataSetName = "dsMainResourceWarning"
        Me.DsMainResourceWarning1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'rtbDetail
        '
        Me.rtbDetail.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MainResourceWarningBindingSource, "WarningDescription", True))
        Me.rtbDetail.Location = New System.Drawing.Point(28, 130)
        Me.rtbDetail.Name = "rtbDetail"
        Me.rtbDetail.Size = New System.Drawing.Size(860, 374)
        Me.rtbDetail.TabIndex = 1
        Me.rtbDetail.Text = ""
        '
        'MainResourceWarningBindingSource
        '
        Me.MainResourceWarningBindingSource.DataMember = "MainResourceWarning"
        Me.MainResourceWarningBindingSource.DataSource = Me.DsMainResourceWarning1
        '
        'DtWarning
        '
        Me.DtWarning.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsMainResourceWarning1, "MainResourceWarning.WarningDate", True))
        Me.DtWarning.Location = New System.Drawing.Point(73, 44)
        Me.DtWarning.Name = "DtWarning"
        Me.DtWarning.Size = New System.Drawing.Size(103, 20)
        Me.DtWarning.TabIndex = 1
        '
        'cboStaff
        '
        Me.cboStaff.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboStaff.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboStaff.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.MainResourceWarningBindingSource, "WarningStaffNum", True))
        Me.cboStaff.FormattingEnabled = True
        Me.cboStaff.Location = New System.Drawing.Point(73, 70)
        Me.cboStaff.Name = "cboStaff"
        Me.cboStaff.RestrictContentToListItems = True
        Me.cboStaff.Size = New System.Drawing.Size(184, 21)
        Me.cboStaff.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(29, 114)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(58, 13)
        Me.Label1.TabIndex = 787
        Me.Label1.Text = "Alert Detail"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(29, 73)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(29, 13)
        Me.Label2.TabIndex = 788
        Me.Label2.Text = "Staff"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(29, 47)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(30, 13)
        Me.Label3.TabIndex = 789
        Me.Label3.Text = "Date"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlDark
        Me.Label4.Location = New System.Drawing.Point(738, 73)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(58, 13)
        Me.Label4.TabIndex = 790
        Me.Label4.Text = "WarningID"
        '
        'txtWarningID
        '
        Me.txtWarningID.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsMainResourceWarning1, "MainResourceWarning.WarningID", True))
        Me.txtWarningID.Enabled = False
        Me.txtWarningID.Location = New System.Drawing.Point(802, 73)
        Me.txtWarningID.Name = "txtWarningID"
        Me.txtWarningID.ReadOnly = True
        Me.txtWarningID.Size = New System.Drawing.Size(86, 20)
        Me.txtWarningID.TabIndex = 792
        Me.txtWarningID.TabStop = False
        '
        'MainMenu1
        '
        Me.MainMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem1, Me.MenuItem2})
        '
        'MenuItem1
        '
        Me.MenuItem1.Index = 0
        Me.MenuItem1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.miClose})
        Me.MenuItem1.Text = "File"
        '
        'miClose
        '
        Me.miClose.Index = 0
        Me.miClose.Text = "Close Window"
        '
        'MenuItem2
        '
        Me.MenuItem2.Index = 1
        Me.MenuItem2.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.miSave, Me.miCancel, Me.miDelete})
        Me.MenuItem2.Text = "Edit"
        '
        'miSave
        '
        Me.miSave.Enabled = False
        Me.miSave.Index = 0
        Me.miSave.Text = "Save Changes"
        '
        'miCancel
        '
        Me.miCancel.Index = 1
        Me.miCancel.Text = "Cancel Changes"
        '
        'miDelete
        '
        Me.miDelete.Index = 2
        Me.miDelete.Text = "Delete This Warning"
        '
        'ErrorProvider1
        '
        Me.ErrorProvider1.ContainerControl = Me
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label5.Location = New System.Drawing.Point(10, 10)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(148, 17)
        Me.Label5.TabIndex = 793
        Me.Label5.Text = "RESOURCE ALERT"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'StatusBar1
        '
        Me.StatusBar1.Location = New System.Drawing.Point(0, 619)
        Me.StatusBar1.Name = "StatusBar1"
        Me.StatusBar1.Panels.AddRange(New System.Windows.Forms.StatusBarPanel() {Me.StatusBarPanel1, Me.StatusBarPanelID, Me.StatusBarPanel2})
        Me.StatusBar1.ShowPanels = True
        Me.StatusBar1.Size = New System.Drawing.Size(984, 22)
        Me.StatusBar1.TabIndex = 794
        Me.StatusBar1.Text = "StatusBar1"
        '
        'StatusBarPanel1
        '
        Me.StatusBarPanel1.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents
        Me.StatusBarPanel1.MinWidth = 200
        Me.StatusBarPanel1.Name = "StatusBarPanel1"
        Me.StatusBarPanel1.Text = "StatusBarPanel1"
        Me.StatusBarPanel1.Width = 200
        '
        'StatusBarPanelID
        '
        Me.StatusBarPanelID.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents
        Me.StatusBarPanelID.Name = "StatusBarPanelID"
        Me.StatusBarPanelID.Text = "Resource Warning: "
        Me.StatusBarPanelID.Width = 111
        '
        'StatusBarPanel2
        '
        Me.StatusBarPanel2.Alignment = System.Windows.Forms.HorizontalAlignment.Right
        Me.StatusBarPanel2.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring
        Me.StatusBarPanel2.Name = "StatusBarPanel2"
        Me.StatusBarPanel2.Text = "Use this window to edit Resource Warnings."
        Me.StatusBarPanel2.Width = 656
        '
        'frmMainResourceWarning
        '
        Me.ClientSize = New System.Drawing.Size(984, 641)
        Me.Controls.Add(Me.StatusBar1)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.txtWarningID)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cboStaff)
        Me.Controls.Add(Me.DtWarning)
        Me.Controls.Add(Me.rtbDetail)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.btnHelp)
        Me.Menu = Me.MainMenu1
        Me.Name = "frmMainResourceWarning"
        Me.Text = "Resource Alert"
        Me.Panel3.ResumeLayout(False)
        CType(Me.DsMainResourceWarning1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MainResourceWarningBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.StatusBarPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.StatusBarPanelID, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.StatusBarPanel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

#Region "LOAD"

    'LOAD
    Private Sub frmMainResourceWarning_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Me.SuspendLayout()
        modPopup.SetStatusBarText("loading", Me.StatusBar1, 0)

SetMainDSConnection:
        Me.daMainResourceWarning.SelectCommand.Connection = sc
        Me.daMainResourceWarning.UpdateCommand.Connection = sc
        Me.daMainResourceWarning.DeleteCommand.Connection = sc
SetDefaults:
        mainDAdapt = Me.daMainResourceWarning
        mainDS = Me.DsMainResourceWarning1
        mainBSrce = Me.MainResourceWarningBindingSource
        ' ctlIdentify = Me.txtHeadline
        ctlNeutral = Me.btnHelp
        mainTopic = "Resource Alert"
LoadCombobox:
        modGlobalVar.LoadStaffCombo(Me.cboStaff, False, StaffComboChoices.CMGI)
FormSetup:

        Forms.Add(Me)
        modPopup.SetStatusBarText("Done", Me.StatusBar1, 0)
        Me.ResumeLayout()
        isLoaded = True

    End Sub 'load

    'RESET CLOSE CALLER
    Public Sub Reload()
        'RESET VARS
        objHowClose = ObjClose.btnSaveExit
        Me.StatusBarPanelID.Text = mainTopic & " ID: " & ThisID.ToString
    End Sub

#End Region     'load

#Region "Update Main"

    'mi ALLOW CLOSE WITHOUT SAVING
    Protected Sub miCloseForm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles miClose.Click

        MouseWait()
        ctlNeutral.Focus()

        If mainDS.Tables(0).Rows(0).HasVersion(DataRowVersion.Proposed) Or mainDS.HasChanges Then
            mainBSrce.EndEdit()
        End If
        objHowClose = AskAcceptChanges(mainDS, mainTopic)
        Me.Close()

CloseAll:
        MouseDefault()
    End Sub

    'SAVE & EXIT
    Private Sub btnSaveExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
          Handles btnSaveExit.Click

        objHowClose = ObjClose.btnSaveExit
        Me.Close()

    End Sub

    'CLOSING
    Private Sub frm_Closing(ByVal sender As Object, ByVal e As FormClosingEventArgs) _
        Handles MyBase.FormClosing  ''ByVal e As System.ComponentModel.CancelEventArgs) 

        Dim ctl As Control
        Dim arCtls(0) As Control

CallUpdate:
        If objHowClose = ObjClose.DontSaveClose Or objHowClose = ObjClose.cancelClose Then
            GoTo CheckRequiredFields
        End If
        If DoUpdate() Then
            e.Cancel = False
        Else
            e.Cancel = True 'don't close form
            GoTo ReleaseForm
        End If

CheckRequiredFields:
        Select Case objHowClose
            Case Is = ObjClose.DontSaveClose, ObjClose.miDelete
                e.Cancel = False
                GoTo ReleaseForm
            Case Is = ObjClose.cancelClose
                e.Cancel = True
                GoTo ReleaseForm
            Case Else 'btnSaveExit, SaveClose,, ObjClose.noChanges
                arCtls = CheckRequired()
                If arCtls.GetLength(0) > 1 Then 'required info missing
                    ctl = arCtls(0)
                    ''INSERT DEFAULT DATA
                    'If objHowClose = ObjClose.SaveClose Or e.CloseReason = CloseReason.UserClosing Then
                    '    If ctl Is ctlIdentify Then
                    '        ctl.Text = usrName & " " & Today.ToShortDateString
                    '        mainBSrce.EndEdit()
                    '        mainDAdapt.Update(mainDS) 'save default data
                    '    End If
                    'End If
                    Dim strbListFields As New StringBuilder
                    For x As Integer = 0 To arCtls.GetLength(0) - 2
                        strbListFields.Append(", " & arCtls(x).Tag)
                    Next
                    e.Cancel = Not (modGlobalVar.AskCloseWithMissingInfo(objHowClose, ctl, strbListFields.ToString.Substring(2)))
                Else
                    e.Cancel = False
                End If
        End Select

ReleaseForm:
        If bCancelClose = False Then   'user OKs close form
            ClassOpenForms.frmMainResourceWarning = Nothing 'reset global var
            objHowClose = Nothing
        Else
        End If
        arCtls = Nothing
        MouseDefault()
        ' e.Cancel = bCancelClose

    End Sub

    'UPDATE BACK END, return number of records updated, return false if error updating
    Public Function DoUpdate() As Boolean
        Dim i As Integer
        MouseWait()
        modPopup.SetStatusBarText("Updating server", Me.StatusBar1, 0)

        If mainDS.Tables(0).Rows(0).HasVersion(DataRowVersion.Proposed) Or mainDS.HasChanges Then
            mainBSrce.EndEdit()
        Else
            DoUpdate = True
            GoTo CloseAll
        End If

        If mainDS.HasChanges = True Then 'this catches delete, save, asksave, save/exit, anyclose
UpdateBackend:
            Try
                i = mainDAdapt.Update(mainDS)
                DoUpdate = True
            Catch ex As Exception
                modGlobalVar.msg("ERROR: updating " & mainTopic, ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
                DoUpdate = False
            End Try
        Else
            DoUpdate = True 'completed action though no updates to be made
        End If

CloseAll:
        modPopup.SetStatusBarText("Done", Me.StatusBar1, 0)
        MouseDefault()

    End Function 'update

    'CHECK REQUIRED FIELDS w Error Provider
    Private Function CheckRequired() As Control()
        Dim ctl As Control
        Dim arCtls(0) As Control
        Dim i As Integer = 0

        'Staff - required
        ctl = Me.cboStaff
        If ctl.Text = String.Empty Then
            arCtls(i) = ctl
            i = i + 1
            Me.ErrorProvider1.SetError(ctl, "please enter a " & IsNull(ctl.Tag, ctl.Name))
            ReDim Preserve arCtls(arCtls.GetUpperBound(0) + 1)
        Else
            Me.ErrorProvider1.SetError(ctl, "")
        End If

        arCtls(i) = Me.btnHelp
        Return arCtls

    End Function

#End Region

#Region "Edit buttons"

    'SAVE
    Private Sub miSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles miSave.Click
        mainBSrce.EndEdit()
        DoUpdate()
    End Sub

    'CANCEL ALL CHANGES
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
          Handles miCancel.Click
        Try
            mainBSrce.CancelEdit()
            mainDS.RejectChanges()
        Catch ex As System.Exception
            modGlobalVar.msg("ERROR: cancelling changes ", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        modPopup.SetStatusBarText("Done", Me.StatusBar1, 0)
    End Sub

    'DELETE
    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
           Handles miDelete.Click, btnDelete.Click

        If modGlobalVar.msg("CONFIRM DELETE", "This " & mainTopic & NextLine & " WILL BE DELETED and the window closed.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

            objHowClose = ObjClose.miDelete
            mainBSrce.RemoveCurrent() 'RemoveAt(0)

            Me.Close()
        End If

    End Sub

#End Region

#Region "General"

    'COPY CURRENT ID
    Private Sub StatusBar1_DoubleClick(sender As System.Object, e As System.EventArgs) Handles StatusBar1.DoubleClick
        Clipboard.SetText(ThisID)
    End Sub

    'RIGHT CLICK MENU
    Private Sub textbox_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) _
        Handles rtbDetail.MouseDown
        If e.Button = System.Windows.Forms.MouseButtons.Right Then
            Dim pp As New ClassRTBContextMenu(sender)
            pp.Show(Me, PointToClient(Control.MousePosition))
        End If
    End Sub

    'UNDO
    Protected Overrides Function ProcessDialogKey(ByVal keyData As Keys) As Boolean
        If keyData = System.Windows.Forms.Keys.Escape Then
            modPopup.UndoCtl(Me.ActiveControl)
            '   bChanged = True
            Return True  ' True means we've processed the key
        Else
            Return MyBase.ProcessDialogKey(keyData)
        End If
    End Function

    'btnHELP
    Private Sub btnHelp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles btnHelp.Click
        ' modGlobalVar.Msg( "HELP - RESOURCE WARNING")
    End Sub

#End Region 'general

#Region "Validation"

    'VALIDATE DATE
    Private Sub DateValidation(sender As Object, e As System.ComponentModel.CancelEventArgs) _
        Handles DtWarning.Validating
        e.Cancel = modGlobalVar.ValidateDateA(sender, Me.ErrorProvider1)
    End Sub

    'VALIDATE STAFF CBO
    Private Sub cboStatus_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) _
        Handles cboStaff.Validating
        If modGlobalVar.ValidateBoundDD(sender, True, Me.ErrorProvider1, ObjClose.CloseByControl) = usrInput.OK Then
            e.Cancel = False
        Else
            e.Cancel = True
            sender.droppeddown = True
        End If
    End Sub

#End Region 'validation


End Class
