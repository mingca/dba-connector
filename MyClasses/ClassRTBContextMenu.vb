Imports System
Imports System.Text
'Imports System.Drawing.Printing
Imports System.Windows.Forms
'Imports System.Runtime.InteropServices

'=========================================
'to enable right mouse editing on RTBs which don't have that as default behaviour; 
'do textboxes too to be consistent
'=========================================
Public Class ClassRTBContextMenu
    Inherits ContextMenu
    Dim usrchoice As Object
    Dim rtb As Object

    'SUB NEW
    Public Sub New(ByRef obj As Object)
        rtb = obj

        ' Dim mi As MenuItem '
        Me.MenuItems.Add("Undo", AddressOf rtbUndo)
        Me.MenuItems.Add("---")
        Me.MenuItems.Add("Cut", AddressOf rtbCut)
        Me.MenuItems.Add("Copy", AddressOf rtbCopy)
        Me.MenuItems.Add("Paste", AddressOf rtbPaste)
        If Len(rtb.selectedtext) > 0 Then
            Me.MenuItems.Add("Enclose in {{ }}", AddressOf rtbCurlyBrace)
        End If

        Me.MenuItems.Add("---")
        Me.MenuItems.Add("Select all", AddressOf rtbSelectAll)
        Me.MenuItems.Add("SpellCheck", AddressOf rtbSpellCheck)
        If rtb.GetType.ToString = "System.Windows.Forms.RichTextBox" And Len(rtb.text) > 5 Then
            Me.MenuItems.Add("Display full text in Word", AddressOf rtbDisplayFullText)
        End If
        '  modGlobalVar.Msg("items added")
        'ridiculous way to have to do this; menuitems have no name which is key
        For Each m As MenuItem In Me.MenuItems
            If m.Text = "Cut" Or m.Text = "Copy" Then
                If Len(rtb.selectedtext) > 0 Then
                Else
                    m.Enabled = False
                End If
            End If
        Next

        If Clipboard.GetDataObject().GetDataPresent(DataFormats.Text) = True Then
        Else
            Try
                Me.MenuItems.Item("Paste").Enabled = False
            Catch ex As Exception
                '  modGlobalVar.Msg(ex.Message, , "ERROR:  finding paste menuitem")
            End Try

        End If
    End Sub

    'UNDO
    Private Sub rtbUndo(ByVal sender As Object, ByVal e As EventArgs)
        rtb.Undo()
    End Sub

    'CUT
    Private Sub rtbCut(ByVal sender As Object, ByVal e As EventArgs)

        Try
            rtb.cut()
        Catch ex As Exception
            modGlobalVar.msg("ERROR: cutting ", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    'COPY
    Private Sub rtbCopy(ByVal sender As Object, ByVal e As EventArgs)
        rtb.Copy()
    End Sub

    'PASTE
    Private Sub rtbPaste(ByVal sender As Object, ByVal e As EventArgs)
        Try
            rtb.Paste()
            '  rtb.findform.bchanged = True
            'bChanged = True
        Catch ex As Exception
            modGlobalVar.msg("ERROR: pasting ", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    'SURROUND SELECTED TEXT with CURLY BRACES and INTERIOR BAR
    'REQUESTED BY DeveloperTown FOR ONLINE DESCRIPTION or CRG Resources
    Private Sub rtbCurlyBrace(ByVal sender As Object, ByVal e As EventArgs)
        'ASSUME last space indicates break between label and url
        Dim str As String = Trim(rtb.selectedtext)
        Dim i As Integer = str.LastIndexOf(" ")
        Dim NewStr As String

        'If i > 0 Then
        '    Dim str1 As String = Left(str, i)
        '    Dim str2 As String = IsNull(str.Substring(IsNull(i, 0)), "")
        '    Clipboard.SetText(" {{" + str1 + " | " + str2 + "}} ")
        'Else    'no label provided
        '    Clipboard.SetText(" {{" + str + "}} ")
        'End If
AddBarBetweenLabelandURL:
        If i > 0 Then 'label and url
            Dim str1 As String = Left(str, i)
            Dim str2 As String = IsNull(str.Substring(IsNull(i, 0)), "")
            Clipboard.SetText(str1 + " | " + str2)
        Else    'no label provided
            Clipboard.SetText(str)
        End If
AppendAmazonCode:
        If Clipboard.GetText.Contains("amazon.com") Then
            NewStr = Replace((Replace(Clipboard.GetText, AmazonTag, "") + AmazonTag), "//?tag", "/?tag")
        Else
            NewStr = Clipboard.GetText
        End If
SurroundwCurlyBraces:
        Clipboard.SetText(" {{" + NewStr + "}} ")

        Try
            rtb.Paste()
        Catch ex As Exception
            modGlobalVar.msg("ERROR: couldn't paste", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    'SELECT ALL
    Private Sub rtbSelectAll(ByVal sender As Object, ByVal e As EventArgs)
        rtb.SelectionStart = 0
        rtb.SelectionLength = rtb.Text.Length
        rtb.Focus()
    End Sub

    'SPELL CHECKER
    Private Sub rtbSpellCheck(ByVal sender As Object, ByVal e As EventArgs)
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Create Word Object
        Dim wrdApp As Microsoft.Office.Interop.Word.Application
        Dim wrdDoc As Microsoft.Office.Interop.Word.Document
        wrdApp = CreateObject("Word.Application")
        Try
            With wrdApp
                .Visible = False
                ' Check Version On computer
                'Then add the text
                '  Select Case .Version
                ' 'Office 2000
                '     Case "9.0"
                ' objDoc = .Documents.Add(, , 1, True)
                ' 'Office XP
                '     Case "10.0", "11.0"
                wrdDoc = .Documents.Add(, , 1, True)
                'Office 97
                '     Case Else ' Office 97
                ' objDoc = .Documents.Add
                ' End Select

                'Text you want to check
                .Selection.Text = rtb.Text

                ' run the check-spelling
                wrdDoc.CheckSpelling()

                'Return the text to the text box
                If Len(IsNull(.Selection.Text, "")) > 0 Then
                    rtb.Text = .Selection.Text
                End If
                '
                ' close the document
                wrdDoc.Close(False)
                ' close the Word instance
                wrdDoc = Nothing


                .Quit()

            End With
        Catch ex As Exception
            modGlobalVar.msg("ERROR: spellcheck ", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        'http://social.msdn.microsoft.com/forums/en-US/vsx/thread/2bf325ab-8e82-4651-b2ec-b29b880909f5/
        wrdApp = Nothing
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    'SHOW FULL TEXT
    Private Sub rtbDisplayFullText(ByVal sender As Object, ByVal e As EventArgs)

        Try
            rtb.SaveFile(UserPath & "\FullText.doc", RichTextBoxStreamType.RichText)
            System.Diagnostics.Process.Start(UserPath & "\FullText.doc")
        Catch ex As Exception
            modGlobalVar.msg("ERROR: opening file  ", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

End Class


