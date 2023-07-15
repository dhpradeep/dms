Imports System.Data.OleDb
Public Class company_loan_more
    Public currentdate As String
    Public remainingamount As Double
    Private Sub company_loan_more_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'textbox2 = name
        'textbox1 = id

        TextBox6.Visible = False

        TextBox1.Visible = False
        TextBox2.Visible = False
        TextBox7.Visible = False
        TextBox8.Visible = False
        DateTimePicker1.Visible = False
        DateTimePicker2.Visible = False
        Label5.Visible = False
        TextBox5.Visible = False
        CheckBox1.Checked = False

        currentdate = System.DateTime.Now.ToString("yyyy/MM/dd")

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
            'end of main connection

            'FILL_DGVOrder()
            If Not CDbl(TextBox8.Text) < CDbl(TextBox7.Text) Then
                'partial sec
                fill_dvgorder1()
            Else
                ' not partial sec
                fill_dvgorder2()
            End If

        Catch ex As Exception
            'MsgBox(ex.ToString())
        End Try
    End Sub

    Private Sub fill_dvgorder2()
        DataGridView1.Rows.Clear()
    End Sub

    Private Sub fill_dvgorder1()
        DataGridView1.Rows.Clear()
        DataGridView1.ColumnCount = 5
        With DataGridView1
            .RowHeadersVisible = False
            .Columns(0).HeaderCell.Value = "No of payments"
            .Columns(1).HeaderCell.Value = "Monthly Installment"
            .Columns(2).HeaderCell.Value = "Interest"
            .Columns(3).HeaderCell.Value = "Principal"
            .Columns(4).HeaderCell.Value = "Balance"
            .Columns(0).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(1).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(2).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(3).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(4).SortMode = DataGridViewColumnSortMode.NotSortable
            .AllowUserToAddRows = False
            .AllowUserToResizeRows = False
            .AllowUserToOrderColumns = False
            .AllowUserToResizeColumns = True
        End With

        Dim interestrate As Double
        interestrate = selectidsql("sources", "source", "13")
        Dim rateofinterest As Integer = interestrate '17 in here
        Dim loanamount As Integer = TextBox8.Text '10k in here
        Dim datedifference As Integer = DateDiff(DateInterval.Month, DateTimePicker1.Value, DateTimePicker2.Value)
        Dim timeduration As Integer = datedifference '6 in here
        'LoanIRate = 0.01 * txtrate.Text / 12
        Dim ratepermonth As Single = 0.01 * rateofinterest / 12
        Dim monthlypayment As Integer = Pmt(ratepermonth, timeduration, -loanamount, 0)
        'MsgBox(monthlypayment)
        '-------------

        Dim interestamount, principalamount, nextbalance As Integer
        interestamount = loanamount * ratepermonth
        principalamount = monthlypayment - interestamount
        nextbalance = loanamount - principalamount

        Dim row1() As String = {
            1,
            monthlypayment,
            interestamount,
            principalamount,
            nextbalance}
        DataGridView1.Rows.Add(row1)

        For i As Integer = 1 To timeduration - 1
            loanamount = DataGridView1.Rows(i - 1).Cells(4).Value
            interestamount = loanamount * ratepermonth

            principalamount = monthlypayment - interestamount
            interestamount = loanamount * ratepermonth
            nextbalance = loanamount - principalamount
            If nextbalance < 5 Then
                nextbalance = 0
            End If
            Dim row() As String = {
                i + 1,
                monthlypayment,
                interestamount,
                principalamount,
                nextbalance
            }
            DataGridView1.Rows.Add(row)
        Next
    End Sub

    Private Sub FILL_DGVOrder()
        Dim i As Integer = 1
        DataGridView1.Rows.Clear()
        sql = "SELECT mypaydate,payamount,remainingamount FROM currentloan"
        cmd = New OleDbCommand(sql, connection1)

        DataGridView1.ColumnCount = 4
        With DataGridView1
            .RowHeadersVisible = False
            .Columns(0).HeaderCell.Value = "S.N"
            .Columns(1).HeaderCell.Value = "Date"
            .Columns(2).HeaderCell.Value = "Amount"
            .Columns(3).HeaderCell.Value = "Remaining Amount"
            .Columns(0).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(1).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(2).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(3).SortMode = DataGridViewColumnSortMode.NotSortable
            .AllowUserToAddRows = False
            .AllowUserToResizeRows = False
            .AllowUserToOrderColumns = False
            .AllowUserToResizeColumns = True
        End With
        DataGridView1.Columns(0).FillWeight = 15
        DataGridView1.Columns(1).FillWeight = 35
        DataGridView1.Columns(2).FillWeight = 25
        DataGridView1.Columns(3).FillWeight = 25

        reader = cmd.ExecuteReader()
        If Not reader.HasRows Then
            DataGridView1.Rows().Clear()
        ElseIf reader.HasRows Then
            Do While reader.Read
                Dim row() As String = {CStr(i), CStr(reader.Item(0)), reader.Item(1), reader.Item(2)}
                DataGridView1.Rows.Add(row)
                i += 1
            Loop
            Dim total As String = 0
            For j As Integer = 0 To DataGridView1.RowCount - 1
                total += Val(CDbl(DataGridView1.Rows(j).Cells(2).Value))
            Next
            Dim rows As String() = {"", "Total", total, ""}
            DataGridView1.Rows.Add(rows)
        End If
    End Sub


    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If CheckBox1.Checked = True Then
            If TextBox6.Text = "" Then
                printmessage("Please fill out data.", "Error", "Try again")
            Else
                Try
                    Dim sql1 As String
                    sql1 = "INSERT INTO incomestatement(insertdate,statement,quantity,amount)VALUES('" & currentdate & "','" & TextBox2.Text & " Pay fine.','none','" & TextBox6.Text & "')"
                    cmd = New OleDbCommand(sql1, connection1)
                    cmd.ExecuteNonQuery()
                    printmessage("Successfully inserted fine.", "Info", "Success")
                Catch ex As Exception
                    printmessage("Something error", "Error", "Try again")
                End Try
            End If
        Else
            printmessage("Please select a checkbox.", "Error", "Try again")
        End If
        company_loan_more_Load(e, e)
    End Sub

    Private Sub TextBox5_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox5.KeyPress
        e.Handled = Not (Char.IsDigit(e.KeyChar) Or e.KeyChar = "." Or Asc(e.KeyChar) = 8)
    End Sub

    Private Sub TextBox6_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox6.KeyPress
        e.Handled = Not (Char.IsDigit(e.KeyChar) Or e.KeyChar = "." Or Asc(e.KeyChar) = 8)
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            TextBox6.Visible = True
            TextBox6.Text = ""
        Else
            TextBox6.Visible = False
        End If
    End Sub

    Private Sub company_loan_more_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then Me.Close()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            Dim mywidth() As Integer = {20, 20, 28, 28, 28}
            printData(DataGridView1, TextBox1.Text, TextBox2.Text, mywidth)
            printmessage("Successfully printed.", "Info", "Success")
        Catch ex As Exception
            printmessage("Error on printing.", "Error", "Try again")
        End Try
    End Sub
End Class