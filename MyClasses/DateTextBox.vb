'==================== CUSTOM COMBO BOX cg ==========================================
' double click for Now
' parse to convert emptied fields to DBNull
'====================================================================================

Public Class DateTextBox
    Inherits TextBox


    'DOUBLE CLICK = NOW
    Protected Overrides Sub OnDoubleClick(e As System.EventArgs)
        Clipboard.SetText(Today()) 'note If is NOW, Undo doesn't work!!
        Me.SelectAll()
        Me.Paste()
    End Sub


    'add handler to allow delete date and satisfy dataset; must be done after databinding
    Protected Overrides Sub OnBindingContextChanged(e As System.EventArgs)
        MyBase.OnBindingContextChanged(e)
        AddHandler Me.DataBindings(0).Parse, AddressOf onParse

    End Sub

    'DELETE DATE on PARSE by dbnull.value
    Protected Sub onParse(ByVal sender As Object, e As ConvertEventArgs)
        If e.Value.ToString = "" Then
            e.Value = DBNull.Value
        Else 'no need to convert as not using format??
            Try
                e.Value = CDate(e.Value)
            Catch ex As Exception
                modGlobalVar.msg("ATTENTION: invalid date", e.Value.ToString, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End Try
        End If
    End Sub




End Class
