Imports System.Data.OleDb
Public Class changePassword

    Private Sub changePassword_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        password.Text = ""
        chpassword.Text = ""
        username.Text = LoginForm.usernamebox.Text
    End Sub

    Private Sub cPassword_Click(sender As Object, e As EventArgs) Handles cPassword.Click
        If username.Text = "" OrElse password.Text = "" OrElse chpassword.Text = "" Then
            MsgBox("Please fillout all data.", MsgBoxStyle.Exclamation, Title:="Try again")
        ElseIf password.Text <> chpassword.Text Then
            MsgBox("password & confirm password do not match, Try again.", MsgBoxStyle.Exclamation, Title:="Error (300))")
            password.Text = ""
            chpassword.Text = ""
            password.Select()
        ElseIf chpassword.Text.Length < 8 Then
            MsgBox("Password must be greater than 8 character", MsgBoxStyle.Exclamation, Title:="Error (301)")
            password.Text = ""
            chpassword.Text = ""
            password.Select()
        Else
            Try
                'to start connection of ms-access
                connection = New OleDbConnection
                With connection
                    If .State = ConnectionState.Closed Then
                        .ConnectionString = strCon
                        .Open()
                    End If
                End With
                'end of main connection
                sql = "UPDATE users SET pass_word = '" & Encrypt(password.Text) & "' WHERE user_name = '" & username.Text & "'"
                cmd = New OleDbCommand(sql, connection)
                Dim result = cmd.ExecuteNonQuery()
                If result = 1 Then
                    MsgBox("Password change successfully")
                    password.Text = ""
                    chpassword.Text = ""
                    Dim results As Integer = MessageBox.Show("You want to restart your application ?", "Confirmation Box", MessageBoxButtons.YesNo)
                    If results = DialogResult.No Then
                        'nothing to do
                        Me.Close()
                        otherSetting.Close()
                    ElseIf results = DialogResult.Yes Then
                        connection.Close()
                        connection.Dispose()
                        Application.Restart()
                    End If
                End If
            Catch ex As Exception
                MsgBox("Problem on connecting to database.(Error code: 300)", MsgBoxStyle.Exclamation, Title:="Try again")
                'MsgBox(ex.ToString())
            End Try

        End If
    End Sub

    Private Sub changepasschar_CheckedChanged(sender As Object, e As EventArgs) Handles changepasschar.CheckedChanged
        If changepasschar.Checked Then
            password.PasswordChar = ControlChars.NullChar
            chpassword.PasswordChar = ControlChars.NullChar
        Else
            password.PasswordChar = "*"c
            chpassword.PasswordChar = "*"c
        End If
    End Sub

    Private Sub chpassword_KeyPress(sender As Object, e As KeyPressEventArgs) Handles chpassword.KeyPress
        If Asc(e.KeyChar) = 13 Then
            e.Handled = True
            cPassword_Click(e, e)
        Else
            e.Handled = False
        End If
    End Sub

    Private Sub password_KeyPress(sender As Object, e As KeyPressEventArgs) Handles password.KeyPress
        e.Handled = (Char.IsWhiteSpace(e.KeyChar))
    End Sub

    Private Sub changePassword_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then Me.Close()
    End Sub
End Class