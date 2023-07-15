Imports System.Data.OleDb
Imports System.IO
Public Class share_amount_transfer
    Private Sub share_amount_transfer_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try
            'end of main connection
            connection1 = New OleDbConnection
            With connection1
                If .State = ConnectionState.Closed Then
                    .ConnectionString = companyCon
                    .Open()
                End If
            End With
            'end of main connection

            FILL_ComboBox()

        Catch ex As Exception

        End Try
    End Sub

    Private Sub FILL_ComboBox()
        sql = "select ID from farmers where hasShare='YES' ORDER BY ID"
        cmd = New OleDbCommand(sql, connection)
        adapter = New OleDbDataAdapter(cmd)
        datast = New DataSet()
        adapter.Fill(datast, "farmers")
        With ComboBox2
            .DataSource = datast.Tables(0)
            .ValueMember = "ID"
            .DisplayMember = "ID"
            .AutoCompleteMode = AutoCompleteMode.Suggest
            .AutoCompleteSource = AutoCompleteSource.ListItems
        End With
    End Sub

    Private Sub share_amount_transfer_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then Me.Close()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If ComboBox2.Text = "" Then
            printmessage("Please select a account", "Error", "Try again")
            Return
        Else
            Try
                'transfer share here....
            Catch ex As Exception

            End Try
        End If
    End Sub
End Class