Imports System.Data.OleDb
Public Class addUsers
    Private Sub addUsers_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        username.Select()
        username.Text = ""
        password.Text = ""
        chpassword.Text = ""
    End Sub

    Private Sub cPassword_Click(sender As Object, e As EventArgs) Handles cPassword.Click
        If username.Text = "" OrElse password.Text = "" OrElse chpassword.Text = "" Then
            MsgBox("Please fillout all data.", MsgBoxStyle.Exclamation, Title:="Try again")
            username.Select()
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
                Dim sql4 As String
                sql4 = "SELECT user_name FROM users WHERE user_name='" & username.Text & "'"
                cmd = New OleDbCommand(sql4, connection)
                adapter = New OleDbDataAdapter(cmd)
                ' Dim execute = cmd.ExecuteNonQuery()
                Dim mytable As New DataTable
                adapter.Fill(mytable)
                If mytable.Rows.Count > 0 Then
                    'user already exist
                    MsgBox("username already exist, insert new one", MsgBoxStyle.Exclamation, Title:="Error")
                    username.Text = ""
                    password.Text = ""
                    cPassword.Text = ""
                    username.Select()
                Else
                    Try
                        sql = "INSERT INTO users (user_name, pass_word)VALUES('" & username.Text & "','" & Encrypt(password.Text) & "')"
                        cmd = New OleDbCommand(sql, connection)
                        Dim result = cmd.ExecuteNonQuery()
                        If result = 1 Then
                            MsgBox("New user added successfully.")
                            username.Text = ""
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

    Private Sub username_KeyPress(sender As Object, e As KeyPressEventArgs) Handles username.KeyPress
        e.Handled = (Char.IsWhiteSpace(e.KeyChar))
    End Sub

    Private Sub password_KeyPress(sender As Object, e As KeyPressEventArgs) Handles password.KeyPress
        If Char.IsWhiteSpace(e.KeyChar) Then
            e.Handled = True
            MsgBox("Whitespace is not allowed in password.")
        End If
    End Sub

    Private Sub chpassword_KeyPress(sender As Object, e As KeyPressEventArgs) Handles chpassword.KeyPress
        password_KeyPress(e, e)
    End Sub

    Private Sub addUsers_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then Me.Close()
    End Sub
End Class