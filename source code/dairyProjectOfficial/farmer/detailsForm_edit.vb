Public Class detailsForm_edit

    Dim checkValue As Double = 0.0

    Private Sub detailsForm_edit_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DateTimePicker1.Format = DateTimePickerFormat.Custom
        DateTimePicker1.CustomFormat = "yyyy/MM/dd"
        TextBox1.Visible = False
        isIds.Enabled = False
        If TextBox1.Text = "Morning" Then
            RadioButton1.Checked = True
        Else
            RadioButton2.Checked = True
        End If

    End Sub

    Private Sub milkStore_Click(sender As Object, e As EventArgs) Handles milkStore.Click
        If isMilk.Text = "" Then
            printmessage("Please Insert Milk.", "Error", "Try again")
            Return
        ElseIf Not Double.TryParse(isMilk.Text, checkValue) Then
            printmessage("Milk must be numeric.", "Error", "Try again")
            isMilk.Select()
            Return
        ElseIf Not (isFat.Text = "-" OrElse Double.TryParse(isFat.Text, checkValue)) Then
            printmessage("Fat Input error.", "Error", "Try again")
            isFat.Select()
            Return
        ElseIf Not (isLacto.Text = "-" OrElse Double.TryParse(isLacto.Text, checkValue)) Then
            printmessage("Lacto Input error.", "Error", "Try again")
            isLacto.Select()
            Return
        Else
            Dim shift As String
            If RadioButton1.Checked = True Then
                shift = "Morning"
            Else
                shift = "Evening"
            End If
            Try
                'update specific milk
                sql = "UPDATE detailsFarmer SET milk_qty='" & isMilk.Text & "', fat_qty='" & isFat.Text _
                    & "', lacto_qty='" & isLacto.Text & "', Entry_date='" & DateTimePicker1.Text & "', Entry_Shift='" & shift & "' WHERE ID=" & isIds.Text & ""
                cmd = New OleDb.OleDbCommand(sql, connection)
                    cmd.ExecuteNonQuery()
                    Me.Close()
                Catch ex As Exception
                    MsgBox(ex.ToString())
                End Try
            End If
    End Sub

    Private Sub detailsForm_edit_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then Me.Close()
    End Sub

    Private Sub isMilk_KeyPress(sender As Object, e As KeyPressEventArgs) Handles isMilk.KeyPress
        e.Handled = Not (Char.IsDigit(e.KeyChar) Or e.KeyChar = "." Or Asc(e.KeyChar) = 8)
    End Sub

    Private Sub isFat_KeyPress(sender As Object, e As KeyPressEventArgs) Handles isFat.KeyPress
        e.Handled = Not (Char.IsDigit(e.KeyChar) Or e.KeyChar = "." Or Asc(e.KeyChar) = 8 Or e.KeyChar = "-")
    End Sub

    Private Sub isLacto_KeyPress(sender As Object, e As KeyPressEventArgs) Handles isLacto.KeyPress
        e.Handled = Not (Char.IsDigit(e.KeyChar) Or e.KeyChar = "." Or Asc(e.KeyChar) = 8 Or e.KeyChar = "-")
    End Sub
End Class