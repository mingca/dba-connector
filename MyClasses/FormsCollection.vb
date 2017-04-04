'Imports System.Windows.Forms

Public Class FormsCollection
    Inherits System.Collections.CollectionBase
    'http://www.ftponline.com/vsm/2002_12/online/hottips/lobel/page2.aspx

    Public Shared OpenForm As Form

    'SO CAN CALL CLOSE METHOD OF EACH OPEN FORM WHEN START FORM CLOSES
    Public Sub Add(ByVal formToAdd As System.Windows.Forms.Form)
        Try
            MyBase.InnerList.Add(formToAdd)
        Catch ex As Exception
            MsgBox(ex.Message, , "error adding")
        End Try


    End Sub

    Public Sub Remove(ByVal formToRemove As System.Windows.Forms.Form)
        MyBase.InnerList.Remove(formToRemove)
    End Sub

    'FIND OPEN FORMS SO CAN DO SOMETHING WITH THAT INSTANCE
    Public Function find(ByVal formToFind As System.Windows.Forms.Form) As Boolean
        find = False
        'For i As Integer = 0 To InnerList.Count - 1 '    For Each f as form In MyBase.InnerList
        '    MsgBox(InnerList(i).name.ToString & NextLine & formToFind.Name, , InnerList.Count.ToString & " open forms")
        'Next i
        If formToFind Is Nothing Then
            Exit Function
        End If
        For Each f As Form In MyBase.InnerList
            If f.Name = formToFind.Name Then
                '  OpenForm = f
                find = True
                Exit For
            End If
        Next
        '............................
        'doesn't work
        'If MyBase.InnerList.IndexOf(formToFind) >= 0 Then
        '    Return True
        'Else
        '    Return False
        'End If
        '........................
        'doesn't work either
        'If MyBase.InnerList.Contains(formToFind.Name) Then '***WHYNOT?  works elsewhere
        '    MsgBox("yes")
        '    find = True
        'Else
        '    MsgBox("no")
        '    find = False
        'End If
    End Function

    'FIND OPEN FORMS SO ONLY SINGLE INSTANCE WILL OPEN
    Public Function Find(ByVal formToFind As System.Windows.Forms.Form, ByVal bOpen As Boolean) As Boolean
        Find = False
        'For i As Integer = 0 To InnerList.Count - 1'    For Each f as form In MyBase.InnerList
        '    MsgBox(i.ToString & " " & InnerList(i).name.ToString, , InnerList.Count.ToString & " open forms")
        'Next i

        'FIND FORM IF IS OPEN
        If MyBase.InnerList.Contains(formToFind) Then
            Find = True
        End If

        If bOpen = False Then
            Exit Function
        End If

        'OPEN FORM
        If Find = True Then
            Try
                formToFind.WindowState = FormWindowState.Minimized 'Note: Mimize + Show works better than Activate + BringToFront
                formToFind.Show()
                formToFind.WindowState = FormWindowState.Normal
            Catch ex As Exception
                Find = False
                modGlobalVar.msg("ERROR: form to forefront ", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        Else
            Try
                formToFind.Show()
                Return True
            Catch ex As Exception
                modGlobalVar.msg("ERROR: open forms collection ", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If

    End Function

    'iterate form colllection
    '    Dim frmAny As Form
    'For Each frmAny In Forms
    '   Debug.WriteLine _
    '      ("Form " & frmAny.Text & _
    '      ", type " & frmAny.Name & ", is open")
    'Next
End Class
