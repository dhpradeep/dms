Imports System.IO
Imports System.Data.OleDb

Public Class BackupForm
    Private Sub btnBackup_Click(sender As Object, e As EventArgs)
        Try
            Dim fbd As New FolderBrowserDialog
            If fbd.ShowDialog() = vbOK Then
                If File.Exists(fbd.SelectedPath + "\mydatabase1.mdb") Then
                    File.Delete(fbd.SelectedPath + "\mydatabase1.mdb")
                End If
                File.Copy("mydatabase1.mdb", fbd.SelectedPath & "\mydatabase1.mdb")
                MsgBox("Done")
            End If
        Catch ex As Exception
            MsgBox(ex.Message.ToString())
        End Try
    End Sub

    Private Sub btnRestore_Click(sender As Object, e As EventArgs)
        'System.IO.Directory.Delete(path, True) 
        Dim filepath As String
        Try
            OpenFileDialog1.Filter = "DB FILE (*.mdb)|*.mdb|All files (*.*)|*.*"

            If OpenFileDialog1.ShowDialog = DialogResult.OK Then
                filepath = OpenFileDialog1.FileName
                'File.Delete("mydatabase1.mdb")
                MsgBox(filepath)
                System.IO.Directory.Delete("mydatabase1.mdb", True)
                File.Copy(filepath, "mydatabase2.mdb")
                MsgBox("Done...")
                Application.Restart()
            End If
        Catch ex As Exception
            'MsgBox(ex.ToString())
        End Try

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        OpenFileDialog1.ShowDialog()
        TextBox1.Text = OpenFileDialog1.FileName
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        SaveFileDialog1.ShowDialog()
        TextBox2.Text = SaveFileDialog1.FileName
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If TextBox1.Text = "" OrElse TextBox2.Text = "" Then
            MsgBox("Please select path.", MsgBoxStyle.Critical, Title:="Error!")
            Button1.Select()
        Else
            Dim result As Integer = MessageBox.Show("Are You sure ? " & Environment.NewLine & " You can't undo this action.", "Confirmation Box", MessageBoxButtons.YesNoCancel)
            If result = DialogResult.Cancel Then
                'nothing to do
            ElseIf result = DialogResult.No Then
                'nothing to do
            ElseIf result = DialogResult.Yes Then
                Try
                    FileCopy(TextBox1.Text, TextBox2.Text)
                    MsgBox("Restore database successfully.")
                    Application.Restart()
                Catch ex As Exception
                    MsgBox("Problem occurs. Try again! (Error code: 303)", MsgBoxStyle.Critical, Title:="Try again")
                End Try
            End If
        End If
    End Sub

    Private Sub BackupForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TextBox1.Text = ""
        TextBox2.Text = ""
        Button3.Select()
    End Sub

    Private Sub BackupForm_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then Me.Close()
    End Sub
End Class