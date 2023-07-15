Imports System.Data.OleDb
Public Class company_statement_edit_exp
    Private Sub company_statement_edit_exp_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TextBox1.Visible = False
        DateTimePicker1.Format = DateTimePickerFormat.Custom
        DateTimePicker1.CustomFormat = "yyyy/MM/dd"
        Try
            connection1 = New OleDbConnection
            With connection1
                If .State = ConnectionState.Closed Then
                    .ConnectionString = companyCon
                    .Open()
                End If
            End With

        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If TextBox2.Text = "" Then
            printmessage("Please Insert all field.", "Error", "Try again")
            TextBox2.Select()
        Else
            Try
                sql = "UPDATE expenditurestatement SET insertdate='" & DateTimePicker1.Text & "', statement='" & TextBox4.Text & "', quantity='" & TextBox3.Text & "' , amount='" & TextBox2.Text & "' WHERE ID=" & TextBox1.Text & ""
                cmd = New OleDbCommand(sql, connection1)
                If cmd.ExecuteNonQuery = 1 Then
                    printmessage("Edit successfully.", "Info", "Success")
                    Me.Close()
                End If
            Catch ex As Exception
                'MsgBox(ex.ToString())
            End Try
        End If
    End Sub

End Class