Imports System.Data.OleDb

Public Class LoginForm
    Dim drag As Boolean
    Dim mousex As Integer
    Dim mousey As Integer
    Dim fulllock As String
    Dim expiryDate As String
    Public backuplock As String
    Public currentDate As String = System.DateTime.Now.ToString("dd/MM/yyyy")
    Public expirydate1, currentdate1 As Date
    Private Sub LoginForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        usernamebox.Text = ""
        passwordbox.Text = ""
        Try
            PictureBox1.Image = Image.FromFile(Application.StartupPath() + "\myImage.jpg")
        Catch ex As Exception
            PictureBox1.Image = Nothing
        End Try
        Try
                connection = New OleDbConnection
            With connection
                If .State = ConnectionState.Closed Then
                    .ConnectionString = strCon
                    .Open()
                End If
            End With

            Dim sql3 As String
            sql3 = "SELECT locking FROM superAdmin"
            adapters = New OleDbDataAdapter(sql3, connection)
            Dim dt As New DataTable
            adapters.Fill(dt)
            fulllock = dt.Rows(1).Item(0)
            backuplock = dt.Rows(0).Item(0)

            Dim expirydate As String = selectidsql("superadmin", "locking", "3")
            expirydate1 = Date.ParseExact(expirydate, "dd/MM/yyyy",
                   System.Globalization.DateTimeFormatInfo.InvariantInfo)
            currentdate1 = Date.ParseExact(currentDate, "dd/MM/yyyy",
                   System.Globalization.DateTimeFormatInfo.InvariantInfo)

            Label7.Visible = False
            add_license.Label1.Text = ""
            LinkLabel1.Visible = False
            If currentdate1 >= expirydate1 Then
                Label7.Visible = True
                LinkLabel1.Visible = True
            End If

            Label3.Text = selectidsql("sources", "source", "7")
            Label4.Text = selectidsql("sources", "source", "8")
            Label5.Text = selectidsql("sources", "source", "9")

        Catch ex As Exception
            printmessage("Problem on connecting to database(Error code: 300).", "Error", "Try again")
        End Try
    End Sub

    Private Sub Panel1_MouseDown(sender As Object, e As MouseEventArgs) Handles Panel1.MouseDown
        drag = True
        mousex = Cursor.Position.X - Me.Left
        mousey = Cursor.Position.Y - Me.Top
    End Sub

    Private Sub Panel1_MouseMove(sender As Object, e As MouseEventArgs) Handles Panel1.MouseMove
        If drag Then
            Me.Left = Cursor.Position.X - mousex
            Me.Top = Cursor.Position.Y - mousey
        End If
    End Sub

    Private Sub Panel1_MouseUp(sender As Object, e As MouseEventArgs) Handles Panel1.MouseUp
        drag = False
    End Sub

    Private Sub Label6_MouseHover(sender As Object, e As EventArgs) Handles Label6.MouseHover
        Label6.BackColor = ColorTranslator.FromHtml("#EB2F2F")
    End Sub

    Private Sub Label6_MouseLeave(sender As Object, e As EventArgs) Handles Label6.MouseLeave
        Label6.BackColor = Color.SeaGreen
    End Sub

    Private Sub Label6_Click(sender As Object, e As EventArgs) Handles Label6.Click
        Me.Close()
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles changepasschar.CheckedChanged
        If changepasschar.Checked Then
            passwordbox.PasswordChar = ControlChars.NullChar
        Else
            passwordbox.PasswordChar = "*"c
        End If
    End Sub

    Private Sub loginButton_Click(sender As Object, e As EventArgs) Handles loginButton.Click

        If (usernamebox.Text = "" OrElse passwordbox.Text = "") Then
            printmessage("Please enter some data first.", "Error", "Try again")
            usernamebox.Select()
        ElseIf usernamebox.Text = "</password>" And passwordbox.Text = "</username>" Then
            superAdmin.ShowDialog()
            usernamebox.Text = ""
            passwordbox.Text = ""
        Else
            If fulllock = "lock" Then
                printmessage("You are locked by superadmin. You can't open this application unless superadmin Unblock you.", "Error", "Locked by Superadmin")
            ElseIf currentDate1 >= expiryDate1 Then
                printmessage("Your software has expire. Please contact admin to renew your software.", "Error", "Software expire")
            Else
                Try
                    sql = "SELECT * FROM users WHERE STRCOMP(user_name,'" & usernamebox.Text & "',0)=0 AND STRCOMP(Pass_word,'" & Encrypt(passwordbox.Text) & "',0)=0"
                    cmd = New OleDbCommand(sql, connection)

                    reader = cmd.ExecuteReader()
                    If reader.Read = True Then
                        If backuplock = "lock" Then
                            printmessage("Superadmin locks your backup system.Backup system does't work unless superadmin Unblock you.", "Error", "Locked by Superadmin")
                        End If
                        PictureBox1.Image = Nothing
                        HomePage.Label9.Text = "Welcome " + usernamebox.Text
                        Me.Hide()
                        HomePage.Show()

                    Else
                        printmessage("Username or Password are wrong.", "Error", "Try again")
                        passwordbox.Text = ""
                        passwordbox.Select()
                    End If

                Catch ex As Exception
                    printmessage("Problem on connecting to database.(Error code: 300).", "Error", "Try again")
                End Try
            End If
        End If
    End Sub

    Private Sub usernamebox_KeyPress(sender As Object, e As KeyPressEventArgs) Handles usernamebox.KeyPress
        If Asc(e.KeyChar) = 13 Then
            e.Handled = True
            loginButton_Click(e, e)
        Else
            e.Handled = False
        End If
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        active_license.ShowDialog()
    End Sub

    Private Sub passwordbox_KeyPress(sender As Object, e As KeyPressEventArgs) Handles passwordbox.KeyPress
        If Asc(e.KeyChar) = 13 Then
            e.Handled = True
            loginButton_Click(e, e)
        Else
            e.Handled = False
        End If
    End Sub
End Class