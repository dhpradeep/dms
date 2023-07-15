Imports System.Data.OleDb
Public Class active_license
    Public currentDate As String = System.DateTime.Now.ToString("dd/MM/yyyy")
    Public added_date As Boolean
    Private Sub active_license_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        activate.ForeColor = Color.White

        If add_license.Label1.Text = "" Then
            'add 1 year from now
            added_date = False

        Else
            ' add new date with current date
            added_date = True
        End If

        Try
            connection = New OleDbConnection
            With connection
                If .State = ConnectionState.Closed Then
                    .ConnectionString = strCon
                    .Open()
                End If
            End With
        Catch ex As Exception
            'printmessage("Problem on connecting to database(Error code: 300).", "Error", "Try again")
        End Try
    End Sub

    Private Sub active_license_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then Me.Close()
    End Sub

    Public Sub checkbox()
        If Not (TextBox1.Text = String.Empty) And Not (TextBox2.Text = String.Empty) And Not (TextBox3.Text = String.Empty) And Not (TextBox4.Text = String.Empty) Then
            'activate.Enabled = True
            If TextBox1.Text.Trim().Length() = 4 And TextBox2.Text.Trim().Length() = 4 And TextBox3.Text.Trim().Length() = 4 And TextBox4.Text.Trim().Length() = 4 Then
                activate.Enabled = True
            Else
                activate.Enabled = False
            End If
        End If
    End Sub

    Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox1.KeyPress
        checkbox()
    End Sub

    Private Sub TextBox2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox2.KeyPress
        checkbox()
    End Sub

    Private Sub TextBox3_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox3.KeyPress
        checkbox()
    End Sub

    Private Sub TextBox4_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox4.KeyPress
        checkbox()
    End Sub

    Private Sub activate_Click(sender As Object, e As EventArgs) Handles activate.Click
        Dim currentdate1 As Date = Date.ParseExact(currentDate, "dd/MM/yyyy",
                   System.Globalization.DateTimeFormatInfo.InvariantInfo)
        Dim expirydate As String = currentdate1.AddYears(1)

        ' check for activation code
        Dim code As String = TextBox1.Text.ToString + "-" + TextBox2.Text.ToString + "-" + TextBox3.Text.ToString + "-" + TextBox4.Text.ToString
        If active_new_license(code) = True Then
            'check if activation code already exist in databse or not
            If license_already_exist(Encrypt(code)) = False Then
                If added_date = True Then
                    'add old date
                    Dim expirydate1 As Date = Date.ParseExact(expirydate, "dd/MM/yyyy",
                   System.Globalization.DateTimeFormatInfo.InvariantInfo)
                    expirydate1 = expirydate1.AddDays(add_license.Label1.Text.ToString())
                    Dim sql1 As String = "UPDATE superAdmin SET locking='" & expirydate1.ToString("dd/MM/yyyy") & "' WHERE ID=3"
                    cmd1 = New OleDbCommand(sql1, connection)
                    cmd1.ExecuteNonQuery()

                    sql = "INSERT INTO activation_license(license,activation_date,expiry_date)VALUES('" & Encrypt(code) & "','" & currentDate & "','" & expirydate1.ToString("dd/MM/yyyy") & "')"
                    cmd = New OleDbCommand(sql, connection)
                    cmd.ExecuteNonQuery()

                    printmessage("Congratulations, Your software has been activated.", "Info", "Software activate")
                    Application.Restart()
                Else
                    'update this key in database
                    Dim sql1 As String = "UPDATE superAdmin SET locking='" & expirydate & "' WHERE ID=3"
                    cmd1 = New OleDbCommand(sql1, connection)
                    cmd1.ExecuteNonQuery()

                    sql = "INSERT INTO activation_license(license,activation_date,expiry_date)VALUES('" & Encrypt(code) & "','" & currentDate & "','" & expirydate & "')"
                    cmd = New OleDbCommand(sql, connection)
                    cmd.ExecuteNonQuery()

                    printmessage("Congratulations, Your software has been activated.", "Info", "Software activate")
                    Application.Restart()
                End If

            Else
                printmessage("Sorry, Activation code is already use.", "Error", "Activation error")
            End If
            'insert this data into database with 
            ' current date + 1 year
        Else
            printmessage("Sorry, Activation code is wrong.", "Error", "Activation error")
            TextBox1.Select()
            TextBox1.Text = ""
            TextBox2.Text = ""
            TextBox3.Text = ""
            TextBox4.Text = ""
        End If
    End Sub
End Class