Imports System.IO
Imports System.Data.OleDb
Public Class otherSetting

    Public imagename As String
    Private Sub otherSetting_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Button15.Visible = False
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

            TextBox1.Text = selectidsql("sources", "source", "3")
            TextBox2.Text = selectidsql("sources", "source", "13")
            TextBox3.Text = selectidsql("sources", "source", "14")
            TextBox4.Text = selectidsql("sources", "source", "15")
            userinterestrate.Text = selectidsql("sources", "source", "16")
            bonus.Text = selectidsql("sources", "source", "18")
            TextBox5.Text = selectidsql("sources", "source", "19")
            TextBox6.Text = selectidsql("sources", "source", "20")
            TextBox7.Text = selectidsql("sources", "source", "21")
            TextBox8.Text = selectidsql("sources", "source", "22")
            TextBox9.Text = selectidsql("sources", "source", "23")
            TextBox10.Text = selectidsql("sources", "source", "24")
            TextBox11.Text = selectidsql("sources", "source", "25")
            TextBox12.Text = selectidsql("sources", "source", "26")
            TextBox13.Text = selectidsql("sources", "source", "27")

        Catch ex As Exception

        End Try

        TextBox1.Enabled = False
        TextBox2.Enabled = False
        TextBox3.Enabled = False
        TextBox4.Enabled = False
        TextBox5.Enabled = False
        TextBox6.Enabled = False
        TextBox7.Enabled = False
        TextBox8.Enabled = False
        TextBox9.Enabled = False
        TextBox10.Enabled = False
        TextBox11.Enabled = False
        TextBox12.Enabled = False
        TextBox13.Enabled = False
        userinterestrate.Enabled = False
        bonus.Enabled = False
        saveBtn.Visible = False
        saveBtn2.Visible = False
        Button9.Visible = False
        Button11.Visible = False
        Button13.Visible = False

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            Dim selectedpath As String
            With FolderBrowserDialog1
                .Description = "Select Backup Location:"
                .RootFolder = Environment.SpecialFolder.Desktop
                .SelectedPath = Environment.SpecialFolder.Desktop
                If .ShowDialog = Windows.Forms.DialogResult.OK Then
                    Try
                        selectedpath = .SelectedPath
                    Catch ex As Exception
                    End Try
                ElseIf (.ShowDialog = DialogResult.Cancel) Then
                    Return
                End If
            End With
            selectedpath = FolderBrowserDialog1.SelectedPath

            sql = "UPDATE sources SET source='" & selectedpath & "' WHERE ID=1"
            cmd = New OleDbCommand(sql, connection)
            Dim result = cmd.ExecuteNonQuery()
            If result = 1 Then
                MsgBox("Updated new path as " & selectedpath & " successfully.", MsgBoxStyle.Information, Title:="Successful")
            End If
        Catch ex As Exception
            MsgBox("Error on selecting new path. (Error code: 308)", MsgBoxStyle.Critical, Title:="Try again")
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try
            Dim selectedpath As String
            With FolderBrowserDialog1
                .Description = "Select Backup Location:"
                .RootFolder = Environment.SpecialFolder.Desktop
                .SelectedPath = Environment.SpecialFolder.Desktop
                If .ShowDialog = Windows.Forms.DialogResult.OK Then
                    Try
                        selectedpath = .SelectedPath
                    Catch ex As Exception
                    End Try
                ElseIf (.ShowDialog = DialogResult.Cancel) Then
                    Return
                End If
            End With
            selectedpath = FolderBrowserDialog1.SelectedPath

            sql = "UPDATE sources SET source='" & selectedpath & "' WHERE ID=2"
            cmd = New OleDbCommand(sql, connection)
            Dim result = cmd.ExecuteNonQuery()
            If result = 1 Then
                MsgBox("Updated new path as " & selectedpath & " successfully.", MsgBoxStyle.Information, Title:="Successful")
            End If
        Catch ex As Exception
            MsgBox("Error on selecting new path. (Error code: 308)", MsgBoxStyle.Critical, Title:="Try again.")
        End Try
    End Sub

    Private Sub btnBackup_Click(sender As Object, e As EventArgs) Handles btnBackup.Click
        Try
            Dim fbd As New FolderBrowserDialog
            If fbd.ShowDialog() = vbOK Then
                If File.Exists(fbd.SelectedPath + "\mydatabase1.mdb") OrElse File.Exists(fbd.SelectedPath + "\mydatabase2.mdb") Then
                    File.Delete(fbd.SelectedPath + "\mydatabase1.mdb")
                    File.Delete(fbd.SelectedPath + "\mydatabase2.mdb")
                End If
                File.Copy("mydatabase1.mdb", fbd.SelectedPath & "\mydatabase1.mdb")
                File.Copy("mydatabase2.mdb", fbd.SelectedPath & "\mydatabase2.mdb")
                printmessage("Backup Succesfull.", "Info", "Success")
            End If
        Catch ex As Exception
            MsgBox(ex.Message.ToString())
        End Try
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        BackupForm.ShowDialog()
    End Sub

    Private Sub editBtn_Click(sender As Object, e As EventArgs) Handles editBtn.Click
        editBtn.Enabled = False
        saveBtn.Visible = True
        TextBox1.Enabled = True
    End Sub

    Private Sub saveBtn_Click(sender As Object, e As EventArgs) Handles saveBtn.Click
        saveBtn.Visible = False
        editBtn.Enabled = True
        TextBox1.Enabled = False
        If TextBox1.Text = "" Then
            MsgBox("Please write somethings before update time.", MsgBoxStyle.Exclamation, Title:="Try again!")
            saveBtn.Visible = True
            editBtn.Enabled = False
            TextBox1.Enabled = True
        ElseIf Not IsNumeric(TextBox1.Text) Then
            MsgBox("Please only provide numeric value.", MsgBoxStyle.Exclamation, Title:="Try again!")
            saveBtn.Visible = True
            editBtn.Enabled = False
            TextBox1.Enabled = True
        Else
            Try
                sql = "UPDATE sources SET source = '" & TextBox1.Text & "' WHERE ID = 3"
                cmd = New OleDbCommand(sql, connection)
                Dim result = cmd.ExecuteNonQuery()
                If result = 1 Then
                    saveBtn.Visible = False
                    editBtn.Enabled = True
                    TextBox1.Enabled = False
                    MsgBox("Update new backup time successfully.")
                    Dim results As Integer = MessageBox.Show("To apply this setting.You have to restart the application." & Environment.NewLine & " You want to restart the application ? ", "Confirmation Box", MessageBoxButtons.YesNo)
                    If results = DialogResult.No Then
                        'nothing to do
                    ElseIf results = DialogResult.Yes Then
                        connection.Close()
                        connection.Dispose()
                        Application.Restart()
                    End If
                End If
            Catch ex As Exception
                'MsgBox(ex.ToString())
                MsgBox("No data found. (Error code: 307)", MsgBoxStyle.Critical, Title:="Try again")
            End Try
        End If
    End Sub


    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        addUsers.ShowDialog()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        changePassword.ShowDialog()
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        companyData.ShowDialog()
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        myPrint.ShowDialog()
    End Sub

    Private Sub editBtn2_Click(sender As Object, e As EventArgs) Handles editBtn2.Click
        editBtn2.Enabled = False
        saveBtn2.Visible = True
        TextBox2.Enabled = True
        TextBox3.Enabled = True
        TextBox4.Enabled = True
    End Sub

    Private Sub saveBtn2_Click(sender As Object, e As EventArgs) Handles saveBtn2.Click
        Try
            updateidsql("sources", "source", TextBox2.Text, "13")
            updateidsql("sources", "source", TextBox3.Text, "14")
            updateidsql("sources", "source", TextBox4.Text, "15")

            saveBtn2.Visible = False
            editBtn2.Enabled = True
            TextBox2.Enabled = False
            TextBox3.Enabled = False
            TextBox4.Enabled = False
            MsgBox("Update new constant value successfully.", MsgBoxStyle.Information, Title:="Success")
            Dim results As Integer = MessageBox.Show("To apply this setting.You have to restart the application." & Environment.NewLine & " You want to restart the application ? ", "Confirmation Box", MessageBoxButtons.YesNo)
            If results = DialogResult.No Then
                'nothing to do
            ElseIf results = DialogResult.Yes Then
                connection.Close()
                connection.Dispose()
                Application.Restart()
            End If
        Catch ex As Exception
            MsgBox("Error on updating.", MsgBoxStyle.Critical, Title:="Error")
        End Try
    End Sub

    Private Sub otherSetting_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then Me.Close()
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        otherSetting_account.ShowDialog()
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            CheckBox1.ForeColor = Color.Red
        Else
            CheckBox1.ForeColor = Color.Black
        End If
    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        userinterestrate.Enabled = True
        bonus.Enabled = True
        Button9.Visible = True
        Button10.Enabled = False
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        Try
            updateidsql("sources", "source", userinterestrate.Text, "16")
            'updateidsql("sources", "source", ts.Text, "17")
            updateidsql("sources", "source", bonus.Text, "18")

            Button9.Visible = False
            Button10.Enabled = True
            userinterestrate.Enabled = False
            bonus.Enabled = False
            printmessage("Update new constant value successfully.", "Info", "Success")
            Dim results As Integer = MessageBox.Show("To apply this setting.You have to restart the application." & Environment.NewLine & " You want to restart the application ? ", "Confirmation Box", MessageBoxButtons.YesNo)
            If results = DialogResult.No Then
                'nothing to do
            ElseIf results = DialogResult.Yes Then
                connection.Close()
                connection.Dispose()
                Application.Restart()
            End If
        Catch ex As Exception
            MsgBox("Error on updating.", MsgBoxStyle.Critical, Title:="Error")
        End Try
    End Sub

    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        Button12.Enabled = False
        Button11.Visible = True
        TextBox5.Enabled = True
        TextBox6.Enabled = True
        TextBox7.Enabled = True
    End Sub

    Private Sub Button14_Click(sender As Object, e As EventArgs) Handles Button14.Click
        Button14.Enabled = False
        Button13.Visible = True
        TextBox8.Enabled = True
        TextBox9.Enabled = True
        TextBox10.Enabled = True
        TextBox11.Enabled = True
        TextBox12.Enabled = True
        TextBox13.Enabled = True
    End Sub

    'TextBox5.Text = selectidsql("sources", "source", "19")
    '        TextBox6.Text = selectidsql("sources", "source", "20")
    '        TextBox7.Text = selectidsql("sources", "source", "21")
    '        TextBox8.Text = selectidsql("sources", "source", "22")
    '        TextBox9.Text = selectidsql("sources", "source", "23")
    '        TextBox10.Text = selectidsql("sources", "source", "24")
    '        TextBox11.Text = selectidsql("sources", "source", "25")
    '        TextBox12.Text = selectidsql("sources", "source", "26")
    '        TextBox13.Text = selectidsql("sources", "source", "27")
    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        Try
            updateidsql("sources", "source", TextBox5.Text, "19")
            updateidsql("sources", "source", TextBox6.Text, "20")
            updateidsql("sources", "source", TextBox7.Text, "21")

            TextBox5.Enabled = False
            TextBox6.Enabled = False
            TextBox7.Enabled = False
            Button11.Visible = False
            Button12.Enabled = True

            printmessage("Update new constant value successfully.", "Info", "Success")
            Dim results As Integer = MessageBox.Show("To apply this setting.You have to restart the application." & Environment.NewLine & " You want to restart the application ? ", "Confirmation Box", MessageBoxButtons.YesNo)
            If results = DialogResult.No Then
                'nothing to do
            ElseIf results = DialogResult.Yes Then
                connection.Close()
                connection.Dispose()
                Application.Restart()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button13_Click(sender As Object, e As EventArgs) Handles Button13.Click
        Try
            updateidsql("sources", "source", TextBox8.Text, "22")
            updateidsql("sources", "source", TextBox9.Text, "23")
            updateidsql("sources", "source", TextBox10.Text, "24")
            updateidsql("sources", "source", TextBox11.Text, "25")
            updateidsql("sources", "source", TextBox12.Text, "26")
            updateidsql("sources", "source", TextBox13.Text, "27")

            Button13.Visible = False
            Button14.Enabled = True
            TextBox8.Enabled = False
            TextBox9.Enabled = False
            TextBox10.Enabled = False
            TextBox11.Enabled = False
            TextBox12.Enabled = False
            TextBox13.Enabled = False

            printmessage("Update new constant value successfully.", "Info", "Success")
            Dim results As Integer = MessageBox.Show("To apply this setting.You have to restart the application." & Environment.NewLine & " You want to restart the application ? ", "Confirmation Box", MessageBoxButtons.YesNo)
            If results = DialogResult.No Then
                'nothing to do
            ElseIf results = DialogResult.Yes Then
                connection.Close()
                connection.Dispose()
                Application.Restart()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub otherSetting_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        TextBox1.Enabled = False
        TextBox2.Enabled = False
        TextBox3.Enabled = False
        TextBox4.Enabled = False
        TextBox5.Enabled = False
        TextBox6.Enabled = False
        TextBox7.Enabled = False
        TextBox8.Enabled = False
        TextBox9.Enabled = False
        TextBox10.Enabled = False
        TextBox11.Enabled = False
        TextBox12.Enabled = False
        TextBox13.Enabled = False
        userinterestrate.Enabled = False
        bonus.Enabled = False
        saveBtn.Visible = False
        saveBtn2.Visible = False
        Button9.Visible = False
        Button11.Visible = False
        Button13.Visible = False
    End Sub

    Private Sub Button15_Click(sender As Object, e As EventArgs) Handles Button15.Click
        printmessage("This features will be added soon", "Info", "Not available")
        'summarize_data.ShowDialog()
    End Sub
End Class