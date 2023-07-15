Imports System.Data.OleDb
Public Class company_statement_sum
    Private Sub company_statement_sum_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'DataGridView3.Visible = False
        'DataGridView4.Visible = False

        DateTimePicker1.Format = DateTimePickerFormat.Custom
        DateTimePicker1.CustomFormat = "yyyy/MM/dd"
        DateTimePicker2.Format = DateTimePickerFormat.Custom
        DateTimePicker2.CustomFormat = "yyyy/MM/dd"

        Try
            'to start connection of ms-access
            connection1 = New OleDbConnection
            With connection1
                If .State = ConnectionState.Closed Then
                    .ConnectionString = companyCon
                    .Open()
                End If
            End With
            'select distinct values
            FILL_DGVOrder()
            FILL_DGVOrder1()

            FILL_DGVOrder3()
            FILL_DGVOrder4()
        Catch ex As Exception
            'MsgBox(ex.ToString())
        End Try
    End Sub

    Private Sub FILL_DGVOrder3()
        Try
            DataGridView3.Rows.Clear()
            For i As Integer = 0 To DataGridView3.Rows.Count - 1
                sql = "SELECT SUM(amount) FROM incomestatementhist WHERE statement='" & DataGridView3.Rows(i).Cells(0).Value & "'"
                adapters = New OleDbDataAdapter(sql, connection1)
                Dim dts As New DataTable
                adapters.Fill(dts)
                Dim totalamount As Double
                totalamount = dts.Rows(0).Item(0)
                DataGridView1.ColumnCount = 3
                Dim row As String() = New String() {i, DataGridView3.Rows(i).Cells(0).Value, totalamount}
                DataGridView1.Rows().Add(row)
            Next
            Dim total As String = 0
            For j As Integer = 0 To DataGridView1.RowCount - 1
                total += Val(CDbl(DataGridView1.Rows(j).Cells(2).Value))
            Next
            Dim rows As String() = {"", "Total", total}
            DataGridView1.Rows.Add(rows)
            With DataGridView1
                .RowHeadersVisible = False
                .Columns(0).HeaderCell.Value = "S.N"
                .Columns(1).HeaderCell.Value = "Statement"
                .Columns(2).HeaderCell.Value = "Amount"
                .Columns(0).SortMode = DataGridViewColumnSortMode.NotSortable
                .Columns(1).SortMode = DataGridViewColumnSortMode.NotSortable
                .Columns(2).SortMode = DataGridViewColumnSortMode.NotSortable
                .AllowUserToAddRows = False
                .AllowUserToResizeRows = False
                .AllowUserToOrderColumns = False
                .AllowUserToResizeColumns = True
            End With
        Catch ex As Exception
            'MsgBox(ex.ToString())
        End Try
    End Sub

    Private Sub FILL_DGVOrder4()
        Try
            DataGridView4.Rows.Clear()
            For i As Integer = 0 To DataGridView4.Rows.Count - 1
                sql = "SELECT SUM(amount) FROM expenditurestatementhist WHERE statement='" & DataGridView4.Rows(i).Cells(0).Value & "'"
                adapters = New OleDbDataAdapter(sql, connection1)
                Dim dts As New DataTable
                adapters.Fill(dts)
                Dim totalamount As Double
                totalamount = dts.Rows(0).Item(0)
                DataGridView2.ColumnCount = 3
                Dim row As String() = New String() {i, DataGridView4.Rows(i).Cells(0).Value, totalamount}
                DataGridView2.Rows().Add(row)
            Next

            Dim total As String = 0
            For j As Integer = 0 To DataGridView2.RowCount - 1
                total += Val(CDbl(DataGridView2.Rows(j).Cells(2).Value))
            Next
            Dim rows As String() = {"", "Total", total}
            DataGridView1.Rows.Add(rows)

            With DataGridView2
                .RowHeadersVisible = False
                .Columns(0).HeaderCell.Value = "S.N"
                .Columns(1).HeaderCell.Value = "Statement"
                .Columns(2).HeaderCell.Value = "Amount"
                .Columns(0).SortMode = DataGridViewColumnSortMode.NotSortable
                .Columns(1).SortMode = DataGridViewColumnSortMode.NotSortable
                .Columns(2).SortMode = DataGridViewColumnSortMode.NotSortable
                .AllowUserToAddRows = False
                .AllowUserToResizeRows = False
                .AllowUserToOrderColumns = False
                .AllowUserToResizeColumns = True
            End With
        Catch ex As Exception
            'MsgBox(ex.ToString())
        End Try
    End Sub

    Private Sub FILL_DGVOrder()
        Try
            DataGridView3.Rows.Clear()

            DataGridView3.ColumnCount = 1
            With DataGridView3
                .RowHeadersVisible = False
                .Columns(0).HeaderCell.Value = "statement"
                .Columns(0).SortMode = DataGridViewColumnSortMode.NotSortable
                .AllowUserToAddRows = False
                .AllowUserToResizeRows = False
                .AllowUserToOrderColumns = False
                .AllowUserToResizeColumns = True
            End With

            'select data in datagridview1
            sql = "SELECT DISTINCT statement FROM incomestatementhist"
            cmd = New OleDbCommand(sql, connection1)
            reader = cmd.ExecuteReader()
            If Not reader.HasRows Then
                DataGridView3.Rows().Clear()
            ElseIf reader.HasRows Then
                Do While reader.Read
                    Dim row() As String = {CStr(reader.Item(0))}
                    DataGridView3.Rows.Add(row)
                Loop
            End If
        Catch ex As Exception
            ' MsgBox(ex.ToString())
        End Try
    End Sub

    Private Sub FILL_DGVOrder1()
        Try
            DataGridView4.Rows.Clear()

            DataGridView4.ColumnCount = 1
            With DataGridView4
                .RowHeadersVisible = False
                .Columns(0).HeaderCell.Value = "statement"
                .Columns(0).SortMode = DataGridViewColumnSortMode.NotSortable
                .AllowUserToAddRows = False
                .AllowUserToResizeRows = False
                .AllowUserToOrderColumns = False
                .AllowUserToResizeColumns = True
            End With

            'select data in datagridview1
            sql = "SELECT DISTINCT statement FROM expenditurestatementhist"
            cmd = New OleDbCommand(sql, connection1)
            reader = cmd.ExecuteReader()
            If Not reader.HasRows Then
                DataGridView4.Rows().Clear()
            ElseIf reader.HasRows Then
                Do While reader.Read
                    Dim row() As String = {CStr(reader.Item(0))}
                    DataGridView4.Rows.Add(row)
                Loop
            End If
        Catch ex As Exception
            ' MsgBox(ex.ToString())
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        company_statement_sum_Load(e, e)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        FILL_DGVOrder5()

        FILL_DGVOrder6()
    End Sub

    Private Sub FILL_DGVOrder5()
        Try
            For i As Integer = 0 To DataGridView3.Rows.Count - 1
                sql = "SELECT SUM(amount) FROM incomestatementhist WHERE statement='" & DataGridView3.Rows(i).Cells(0).Value & "' AND (insertdate BETWEEN '" & DateTimePicker1.Text & "' AND '" & DateTimePicker2.Text & "')"
                adapters = New OleDbDataAdapter(sql, connection1)
                Dim dts As New DataTable
                adapters.Fill(dts)
                Dim totalamount As Double
                totalamount = dts.Rows(0).Item(0)
                DataGridView1.ColumnCount = 3
                Dim row As String() = New String() {i, DataGridView3.Rows(i).Cells(0).Value, totalamount}
                DataGridView1.Rows().Add(row)
            Next
            Dim total As String = 0
            For j As Integer = 0 To DataGridView1.RowCount - 1
                total += Val(CDbl(DataGridView1.Rows(j).Cells(2).Value))
            Next
            Dim rows As String() = {"", "Total", total}
            DataGridView1.Rows.Add(rows)
            With DataGridView1
                .RowHeadersVisible = False
                .Columns(0).HeaderCell.Value = "S.N"
                .Columns(1).HeaderCell.Value = "Statement"
                .Columns(2).HeaderCell.Value = "Amount"
                .Columns(0).SortMode = DataGridViewColumnSortMode.NotSortable
                .Columns(1).SortMode = DataGridViewColumnSortMode.NotSortable
                .Columns(2).SortMode = DataGridViewColumnSortMode.NotSortable
                .AllowUserToAddRows = False
                .AllowUserToResizeRows = False
                .AllowUserToOrderColumns = False
                .AllowUserToResizeColumns = True
            End With
        Catch ex As Exception
            'MsgBox(ex.ToString())
        End Try
    End Sub

    Private Sub FILL_DGVOrder6()
        Try
            For i As Integer = 0 To DataGridView4.Rows.Count - 1
                sql = "SELECT SUM(amount) FROM expenditurestatementhist WHERE statement='" & DataGridView3.Rows(i).Cells(0).Value & "' AND (insertdate BETWEEN '" & DateTimePicker1.Text & "' AND '" & DateTimePicker2.Text & "')"
                adapters = New OleDbDataAdapter(sql, connection1)
                Dim dts As New DataTable
                adapters.Fill(dts)
                Dim totalamount As Double
                totalamount = dts.Rows(0).Item(0)
                DataGridView2.ColumnCount = 3
                Dim row As String() = New String() {i, DataGridView4.Rows(i).Cells(0).Value, totalamount}
                DataGridView2.Rows().Add(row)
            Next

            Dim total As String = 0
            For j As Integer = 0 To DataGridView2.RowCount - 1
                total += Val(CDbl(DataGridView2.Rows(j).Cells(2).Value))
            Next
            Dim rows As String() = {"", "Total", total}
            DataGridView1.Rows.Add(rows)

            With DataGridView2
                .RowHeadersVisible = False
                .Columns(0).HeaderCell.Value = "S.N"
                .Columns(1).HeaderCell.Value = "Statement"
                .Columns(2).HeaderCell.Value = "Amount"
                .Columns(0).SortMode = DataGridViewColumnSortMode.NotSortable
                .Columns(1).SortMode = DataGridViewColumnSortMode.NotSortable
                .Columns(2).SortMode = DataGridViewColumnSortMode.NotSortable
                .AllowUserToAddRows = False
                .AllowUserToResizeRows = False
                .AllowUserToOrderColumns = False
                .AllowUserToResizeColumns = True
            End With
        Catch ex As Exception
            ' MsgBox(ex.ToString())
        End Try
    End Sub

    Private Sub company_statement_sum_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        DataGridView1.DataSource = Nothing
        DataGridView2.DataSource = Nothing
        DataGridView3.DataSource = Nothing
        DataGridView4.DataSource = Nothing
    End Sub
End Class