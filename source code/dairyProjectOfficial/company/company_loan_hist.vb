Imports System.Data.OleDb
Imports System.IO
Imports iTextSharp.text.pdf
Imports iTextSharp.text
Public Class company_loan_hist
    Private Sub company_loan_hist_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            'to start connection of ms-access
            connection1 = New OleDbConnection
            With connection1
                If .State = ConnectionState.Closed Then
                    .ConnectionString = companyCon
                    .Open()
                End If
            End With
            'end of main connection

            FILL_DGVOrder()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub FILL_DGVOrder()
        Try
            Dim i As Integer = 1
            DataGridView1.Rows.Clear()

            DataGridView1.ColumnCount = 9
            With DataGridView1
                .RowHeadersVisible = False
                .Columns(0).HeaderCell.Value = "S.N"
                .Columns(1).HeaderCell.Value = "Transaction ID"
                .Columns(2).HeaderCell.Value = "Farmer ID"
                .Columns(3).HeaderCell.Value = "Name"
                .Columns(4).HeaderCell.Value = "Taken date"
                .Columns(5).HeaderCell.Value = "Return date"
                .Columns(6).HeaderCell.Value = "Partial (Y/N)"
                .Columns(7).HeaderCell.Value = "Total Amount"
                .Columns(8).HeaderCell.Value = "Taken Amount"
                .Columns(0).SortMode = DataGridViewColumnSortMode.NotSortable
                .Columns(1).SortMode = DataGridViewColumnSortMode.NotSortable
                .Columns(2).SortMode = DataGridViewColumnSortMode.NotSortable
                .Columns(3).SortMode = DataGridViewColumnSortMode.NotSortable
                .Columns(4).SortMode = DataGridViewColumnSortMode.NotSortable
                .Columns(5).SortMode = DataGridViewColumnSortMode.NotSortable
                .Columns(6).SortMode = DataGridViewColumnSortMode.NotSortable
                .Columns(7).SortMode = DataGridViewColumnSortMode.NotSortable
                .Columns(8).SortMode = DataGridViewColumnSortMode.NotSortable
                .AllowUserToAddRows = False
                .AllowUserToResizeRows = False
                .AllowUserToOrderColumns = False
                .AllowUserToResizeColumns = False
            End With
            DataGridView1.Columns(0).FillWeight = 15
            DataGridView1.Columns(1).FillWeight = 20
            DataGridView1.Columns(2).FillWeight = 20
            DataGridView1.Columns(3).FillWeight = 30
            DataGridView1.Columns(4).FillWeight = 22
            DataGridView1.Columns(5).FillWeight = 22
            DataGridView1.Columns(6).FillWeight = 22
            DataGridView1.Columns(7).FillWeight = 28
            DataGridView1.Columns(8).FillWeight = 28

            sql = "SELECT * FROM loanholderhist"
            cmd = New OleDbCommand(sql, connection1)
            reader = cmd.ExecuteReader()
            If Not reader.HasRows Then
                DataGridView1.Rows().Clear()
            ElseIf reader.HasRows Then
                Do While reader.Read
                    Dim row() As String = {CStr(i), reader.Item(0), CStr(reader.Item(1)), CStr(reader.Item(2)), CStr(reader.Item(3)), CStr(reader.Item(4)), CStr(reader.Item(5)), CStr(reader.Item(6)), CStr(reader.Item(7))}
                    DataGridView1.Rows.Add(row)
                    i += 1
                Loop
            End If
        Catch ex As Exception
            'MsgBox(ex.ToString())
        End Try
    End Sub

    Private Sub company_loan_hist_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then Me.Close()
    End Sub

    Private Sub loanhist_Click(sender As Object, e As EventArgs) Handles loanhist.Click
        Try

            Dim mywidth() As Integer = {15, 15, 15, 30, 25, 25, 25, 25, 25}
            printData(DataGridView1, "", "", mywidth)

            printmessage("Successfully print document as PDF.", "Info", "Successful")
        Catch ex As Exception
            MsgBox("Previous PDF file is in use. (Error code: 306)", MsgBoxStyle.Critical, Title:="Try again")
        End Try
    End Sub
End Class