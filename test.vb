Imports System.Data.SqlClient
Public Class test

    ' Declare the drop-down button and the items it will contain.
    'Friend WithEvents dropDownButton1 As ToolStripDropDownButton
    'Friend WithEvents dropDown As ToolStripDropDown
    'Friend WithEvents buttonRed As ToolStripButton
    'Friend WithEvents buttonBlue As ToolStripButton
    'Friend WithEvents buttonYellow As ToolStripButton
    'Friend WithEvents buttonFilter As ToolStripButton
    'Friend WithEvents buttonSearch As ToolStripButton
    'Friend WithEvents btnAdd As ToolStripButton
    'Friend WithEvents txtSearch As New ToolStripTextBox

    Friend WithEvents cboGeneral As New ToolStripComboBox   'not native to context menu
    Dim isloaded As Boolean = False
    Dim Heading1 As String = "SELECT the overarching denominational group from the dropdown box."
    Dim Heading2 As String = "SELECT the specific denomination of this congregation from the list."
   
    Private Sub test_load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' LoadcboGeneral()
        Me.cboGeneral.Size = New System.Drawing.Size(300, 21)
        Me.cboGeneral.DropDownStyle = ComboBoxStyle.DropDownList
        isloaded = True
    End Sub

    'RESET COMBO to catch new adds
    Private Sub cmStrip1_Opening(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) _
        Handles cmStrip1.Opening
        Reset()
        LoadcboGeneral()
        Me.cboGeneral.SelectedIndex = -1
        e.Cancel = False
    End Sub


    'CLEAR MENU ITEMS, reset top heading and combo
    Private Function Reset() As Boolean
        Dim lbl As New ToolStripLabel
        On Error GoTo exitsub
        Me.cmStrip1.Items.Clear()
        lbl.Text = Heading1
        lbl.Enabled = False
        'lbl.BackColor = Color.Red
        Me.cmStrip1.Items.Add(lbl)
        Me.cmStrip1.Items.Add("-")
        Me.cmStrip1.Items.Add(cboGeneral)
        Return True
        Exit Function
exitsub: Return False
    End Function


    'FILL COMBO BOX W GENERAL TYPE
    Private Function LoadcboGeneral() As Boolean
        Dim dr As SqlDataReader
        Dim cmd As New SqlCommand("SELECT DISTINCT GeneralType from luDenomType ORDER BY GeneralType", sc)
        If Not SCConnect() Then
            Return False
            Exit Function
        End If
        Me.cboGeneral.Items.Add("All")
        Try
            dr = cmd.ExecuteReader()
        Catch ex As Exception
            MsgBox(ex.Message, , "reading exception")
            Me.cboGeneral.Items.Add("none found")
            dr.Close()
            sc.Close()
            cmd = Nothing
            Return False
        End Try
        If dr.HasRows Then
            'load menu items
            While dr.Read()
                Me.cboGeneral.Items.Add(dr.GetString(0))
            End While
        Else
            MsgBox("no rows")
        End If
        dr.Close()
        sc.Close()
        cmd = Nothing
        Return True
    End Function

    'CALL LOAD CONTEXT MENU 
    Private Sub cboGeneral_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboGeneral.SelectedIndexChanged
        Dim str As String
        'If Not isloaded Then
        '    Exit Sub
        'End If
        If cboGeneral.SelectedIndex > -1 Then
            Select Case sender.text
                Case Is = "All"
                    str = "SELECT DISTINCT Denomination from luDenomType  WHERE GeneralType not like 'KEEP THIS RECORD' ORDER BY Denomination"
                Case Else
                    str = "SELECT DISTINCT Denomination from luDenomType  WHERE GeneralType = '" & sender.Text & "'  ORDER BY Denomination"
            End Select
            LoadList(sender, e, str)
            str = Nothing
        End If
    End Sub

    'LOAD MENU FROM USER FILTER
    Private Sub LoadList(ByVal obj As Object, ByVal ea As EventArgs, ByVal sql As String)
        Dim lbl2 As New ToolStripLabel
        Dim dr As SqlDataReader
        Dim cmd As New SqlCommand(sql, sc)

        lbl2.Text = Heading2
        lbl2.Enabled = False
        'lbl.BackColor = Color.Blue  'why does this have no effect?
        Me.cmStrip1.Items.Add("  ")
        Me.cmStrip1.Items.Add(lbl2)
        Me.cmStrip1.Items.Add("-")
        If Not SCConnect() Then
            Exit Sub
        End If
        Try
            dr = cmd.ExecuteReader()
        Catch ex As Exception
            MsgBox(ex.Message, , "reading exception")
        End Try
        If dr.HasRows Then
            While dr.Read()
                Me.cmStrip1.Items.Add(dr.GetString(0))
            End While
        Else
        End If
        dr.Close()
        sc.Close()
        Me.cmStrip1.Items.Add("-")
        Me.cmStrip1.Items.Add("-- Add New Denomination --")
    End Sub


    'RETURN USER MENU SELECTION, ADD NEW if required
    Private Sub cmStrip1_Click(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) _
            Handles cmStrip1.ItemClicked
        Dim strGen As String
        Dim strDenom As String

        If e.ClickedItem.ToString = "-- Add New Denomination --" Then
            strGen = InputBox("like Presbyterian or Baptist", "Step 1: Enter the over-arching denomination")
            strDenom = InputBox("like Reformed Presbyterian Church of North America", "Step 2: Enter the specific local denomination of the congregation")
            If Not SCConnect() Then
                Exit Sub
            End If
            Dim myTrans As SqlClient.SqlTransaction = sc.BeginTransaction()
            Dim cmdInsert As New SqlClient.SqlCommand("INSERT INTO ludenomType(GeneralType,Denomination) VALUES ('" & IsNull(strGen, "GeneralType") & "',  '" & IsNull(strDenom, "NewDenomination") & "')", sc, myTrans)

            Try
                cmdInsert.ExecuteNonQuery()
                myTrans.Commit()
                TextBox1.Text = strDenom
            Catch exce As Exception
                MsgBox(exce.Message, MsgBoxStyle.Critical, "execute nonquery error")
                myTrans.Rollback()
            Finally
                sc.Close()
            End Try
        Else
            TextBox1.Text = e.ClickedItem.ToString
        End If

    End Sub



    ''SHOW MENU in case they don't right click
    'Private Sub TextBox1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox1.KeyPress

    '    ' Me.cmStrip1.Show()
    'End Sub

    Private Sub TextBox1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox1.KeyUp
        TextBox1.Undo()
        Me.cmStrip1.Show()
    End Sub


    'Private Sub SelectD(ByVal obj As Object, ByVal ea As EventArgs)
    '    MsgBox(obj.text, , "SelectD")
    'End Sub
    'Private Sub ToolStripComboBox1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)

    '    Dim usrvalue As String
    '    Dim dr As SqlDataReader




    '    If Not isloaded Then
    '        Exit Sub
    '    End If




    '    '  Me.ContextMenuStrip1.Items.Clear()
    '    ' Me.ContextMenuStrip1.Items.Add(Me.ToolStripComboBox1)
    '    ' Me.ContextMenuStrip1.Items.Add(Me.ComboBox1)
    '    usrvalue = Replace(Me.ComboBox1.SelectedItem, "'", "%")
    '    usrvalue = Replace(usrvalue, "*", "%")

    '    ContextMenu1.MenuItems.Add("one")
    '    ContextMenu1.MenuItems.Add("two")
    '    ContextMenu1.MenuItems.Add("three")
    '    Exit Sub

    '    Dim cmd As New SqlCommand("SELECT Denomination from luDenomType WHERE GeneralType LIKE N'" & usrvalue & "' ORDER BY GeneralType, Denomination", sc)
    '    sc.Open()   'open connection
    '    Try
    '        dr = cmd.ExecuteReader()
    '      
    '    Catch ex As Exception
    '        MsgBox(ex.Message, , "reading exception")
    '    End Try

    '    If dr.HasRows Then
    '        'LOAD MENU
    '        While dr.Read()
    '            ContextMenu1.MenuItems.Add(dr.GetString(0))
    '        End While
    '    End If
    '    dr.Close()
    '    sc.Close()

    'End Sub


    ''...........not used

    'Private Sub MakeButtons() 'initialize dropdownbutton
    '    'add a button of buttons 
    '    dropDownButton1 = New ToolStripDropDownButton()
    '    dropDown = New ToolStripDropDown()
    '    dropDownButton1.Text = "Colour"


    '    dropDown.BackColor = Color.Magenta
    '    dropDownButton1.BackColor = Color.Yellow
    '    ' Set the drop-down on the ToolStripDropDownButton.
    '    dropDownButton1.DropDown = dropDown

    '    ' Set the drop-down direction.
    '    dropDownButton1.DropDownDirection = ToolStripDropDownDirection.Left

    '    ' Do not show a drop-down arrow.
    '    dropDownButton1.ShowDropDownArrow = False

    '    ' Declare three buttons, set their foreground color and text, 
    '    ' and add the buttons to the drop-down.
    '    buttonRed = New ToolStripButton()
    '    buttonRed.ForeColor = Color.Red
    '    buttonRed.Text = "R"

    '    buttonBlue = New ToolStripButton()
    '    buttonBlue.ForeColor = Color.Blue
    '    buttonBlue.Text = "B"

    '    buttonYellow = New ToolStripButton()
    '    buttonYellow.ForeColor = Color.Green
    '    buttonYellow.Text = "G"

    '    dropDown.Items.AddRange(New ToolStripItem() {buttonRed, buttonBlue, buttonYellow})
    '    ' ToolStrip1.Items.Add(dropDownButton1)
    '    Me.ContextMenuStrip1.Items.Add(dropDownButton1)
    'End Sub

    '' Handle the buttons' click event by setting the foreground color of the
    '' form to the foreground color of the button that is clicked.
    'Public Sub colorButtonsClick(ByVal sender As [Object], ByVal e As EventArgs) _
    '    Handles buttonRed.Click, buttonBlue.Click, buttonYellow.Click
    '    Dim senderButton As ToolStripButton = CType(sender, ToolStripButton)
    '    Me.BackColor = senderButton.ForeColor
    '    '  Me.BackColor = CType(Me.ToolStripComboBox1.SelectedItem, Color)
    'End Sub

    'CLEAR MENU ITEMS

    '    Private Sub TextBox1_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) _
    'Handles TextBox1.TextChanged
    '        '  MsgBox(e.GetType.ToString, , sender.ToString)

    '    End Sub
End Class