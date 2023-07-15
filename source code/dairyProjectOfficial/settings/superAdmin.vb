Imports System.Data.OleDb

Public Class superAdmin
    Private Sub superAdmin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CheckBox1.Checked = False
        CheckBox2.Checked = False
        Try
            connection = New OleDbConnection
            With connection
                If .State = ConnectionState.Closed Then
                    .ConnectionString = strCon
                    .Open()
                End If
            End With

            sql = "SELECT locking FROM superAdmin"
            cmd = New OleDbCommand(sql, connection)
            adapters = New OleDbDataAdapter(sql, connection)
            Dim dt As New DataTable
            adapters.Fill(dt)
            Dim backuplock As String = dt.Rows(0).Item(0)
            Dim fulllock As String = dt.Rows(1).Item(0)

            If backuplock = "lock" And fulllock = "unlock" Then
                CheckBox1.Checked = True
            ElseIf fulllock = "lock" Then
                CheckBox1.Checked = True
                CheckBox1.Enabled = False
                CheckBox2.Checked = True
            End If
        Catch ex As Exception
            MsgBox(ex.ToString())
        End Try
    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged
        If CheckBox2.Checked = True Then
            CheckBox1.Checked = True
            CheckBox1.Enabled = False
        Else
            CheckBox1.Enabled = True
            If CheckBox1.Checked = False Then
                Button1.Text = "UNLOCK"
            End If
        End If
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = False And CheckBox2.Checked = False Then
            Button1.Text = "UNLOCK"
        Else
            Button1.Text = "LOCK"
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If CheckBox1.Checked = True And CheckBox2.Checked = True Then
            sql = "UPDATE superAdmin SET locking='lock' WHERE ID=1 OR ID=2"
            cmd = New OleDbCommand(sql, connection)
            Dim result = cmd.ExecuteNonQuery()
            MsgBox("This system fully locked.")
        ElseIf CheckBox1.Checked = True And CheckBox2.Checked = False Then
            Dim sql3 As String
            sql = "UPDATE superAdmin SET locking='lock' WHERE ID=1"
            sql3 = "UPDATE superAdmin SET locking='unlock' WHERE ID=2"
            cmd = New OleDbCommand(sql, connection)
            cmd3 = New OleDbCommand(sql3, connection)
            cmd.ExecuteNonQuery()
            cmd3.ExecuteNonQuery()
            MsgBox("This systems backup function is locked.")
        ElseIf CheckBox1.Checked = False And CheckBox2.Checked = False Then
            sql = "UPDATE superAdmin SET locking='unlock' WHERE ID=1 OR ID=2"
            cmd = New OleDbCommand(sql, connection)
            Dim result = cmd.ExecuteNonQuery()
            MsgBox("This systems is unlocked successfully.")
        End If

        connection.Close()
        connection.Dispose()
        Application.Restart()

    End Sub
End Class