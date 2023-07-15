Imports System.Data.OleDb
Public Class myPrint
    Private Sub myPrint_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        saveBtn.Visible = False
        saveBtn1.Visible = False
        saveBtn2.Visible = False
        TextBox1.Enabled = False
        TextBox2.Enabled = False
        TextBox3.Enabled = False
        editBtn.Enabled = True
        editBtn1.Enabled = True
        editBtn2.Enabled = True

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

            Dim sql As String
            sql = "SELECT source FROM sources WHERE ID= 10"
            adapters = New OleDbDataAdapter(sql, connection)
            Dim dt1 As New DataTable
            adapters.Fill(dt1)
            TextBox1.Text = dt1.Rows(0).Item(0)

            Dim sql1 As String
            sql1 = "SELECT source FROM sources WHERE ID= 11"
            adapters = New OleDbDataAdapter(sql1, connection)
            Dim dt As New DataTable
            adapters.Fill(dt)
            TextBox2.Text = dt.Rows(0).Item(0)

            Dim sql2 As String
            sql2 = "SELECT source FROM sources WHERE ID= 12"
            adapters = New OleDbDataAdapter(sql2, connection)
            Dim dt2 As New DataTable
            adapters.Fill(dt2)
            TextBox3.Text = dt2.Rows(0).Item(0)

        Catch ex As Exception

        End Try
    End Sub

    Private Sub editBtn_Click(sender As Object, e As EventArgs) Handles editBtn.Click
        saveBtn.Visible = True
        editBtn.Enabled = False
        TextBox1.Enabled = True
    End Sub

    Private Sub editBtn1_Click(sender As Object, e As EventArgs) Handles editBtn1.Click
        TextBox2.Enabled = True
        saveBtn1.Visible = True
        editBtn1.Enabled = False
    End Sub

    Private Sub editBtn2_Click(sender As Object, e As EventArgs) Handles editBtn2.Click
        editBtn2.Enabled = False
        saveBtn2.Visible = True
        TextBox3.Enabled = True
    End Sub

    Private Sub saveBtn_Click(sender As Object, e As EventArgs) Handles saveBtn.Click
        saveBtn.Visible = False
        editBtn.Enabled = True
        TextBox1.Enabled = False
        Try
            sql = "UPDATE sources SET source='" & TextBox1.Text & "' WHERE ID=10"
            cmd = New OleDbCommand(sql, connection)
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            ' MsgBox(ex.ToString())
        End Try
    End Sub

    Private Sub saveBtn1_Click(sender As Object, e As EventArgs) Handles saveBtn1.Click
        saveBtn1.Visible = False
        editBtn1.Enabled = True
        TextBox2.Enabled = False
        Try
            sql = "UPDATE sources SET source='" & TextBox2.Text & "' WHERE ID=11"
            cmd = New OleDbCommand(sql, connection)
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            'MsgBox(ex.ToString())
        End Try
    End Sub

    Private Sub saveBtn2_Click(sender As Object, e As EventArgs) Handles saveBtn2.Click
        saveBtn2.Visible = False
        editBtn2.Enabled = True
        TextBox3.Enabled = False
        Try
            sql = "UPDATE sources SET source='" & TextBox3.Text & "' WHERE ID=12"
            cmd = New OleDbCommand(sql, connection)
            cmd.ExecuteNonQuery()

        Catch ex As Exception
            ' MsgBox(ex.ToString())
        End Try
    End Sub

    Private Sub myPrint_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then Me.Close()
    End Sub
End Class