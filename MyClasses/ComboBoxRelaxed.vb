
'==================== CUSTOM COMBO BOX cg ==========================================
' restrict to list, dropdown, search by successive characters not repeating first character
'====================================================================================

Public Class ComboBoxRelaxed
    'adapted from 
    'http://stackoverflow.com/questions/14981204/only-select-list-items-from-combobox

    Inherits ComboBox

    'RESTRICT TO ITEMS IN LIST
    Public Property RestrictContentToListItems As Boolean = True

    Public Sub New()

        'SUGGEST APPEND
        With Me
            .AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
            .AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            'default style is dropdown
        End With
    End Sub

    'ITEM NEEDS TO BE IN LIST
    Protected Overrides Sub OnValidating(e As System.ComponentModel.CancelEventArgs)
        'forces to be item in list
        'this OnValidating is called, then instance Validating is called
        'where the check is made to exclude titles and set error provider
        '--problem: resource detail where subtype is relation to type so is "in list" but is not in dd at that time
        If Me.Text = String.Empty Then
            'empty dd may be ok
            Me.SelectedIndex = -1
        Else
            If RestrictContentToListItems AndAlso Me.Items.Count > 0 Then
                Dim index As Integer = Me.FindString(Me.Text)
                If index > -1 Then
                    Me.SelectedIndex = index
                Else
                    e.Cancel = True
                    Me.SelectAll()
                    '  Me.Text = "" '--this needs to be here for resource detail; i took it out earlier because...?
                    Beep()
                End If
            End If
        End If
        MyBase.OnValidating(e)
    End Sub

    'ALLOW ENTER KEY TO SELECT
    Protected Overrides Sub OnKeyDown(e As System.Windows.Forms.KeyEventArgs)
        '  MyBase.OnKeyDown(e)
        If e.KeyCode = Keys.Enter Then
            Me.DroppedDown = False
            e.Handled = True
        Else
            MyBase.OnKeyDown(e)
        End If
    End Sub


End Class

