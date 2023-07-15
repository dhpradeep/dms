Imports System.Data.OleDb
Imports System.IO
Public Class company_loan

    Public interest_rate As String
    Dim index As Integer = 0

    Private Sub company_loan_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DateTimePicker1.Format = DateTimePickerFormat.Custom
        DateTimePicker1.CustomFormat = "yyyy/MM/dd"
        DateTimePicker2.Format = DateTimePickerFormat.Custom
        DateTimePicker2.CustomFormat = "yyyy/MM/dd"

        TextBox2.Visible = False
        CheckBox2.Checked = False

        Try
            DateTimePicker2.Visible = False
            'to start connection of ms-access
            connection = New OleDbConnection
            With connection
                If .State = ConnectionState.Closed Then
                    .ConnectionString = strCon
                    .Open()
                End If
            End With
            'end of main connection
            'to start connection of ms-access
            connection1 = New OleDbConnection
            With connection1
                If .State = ConnectionState.Closed Then
                    .ConnectionString = companyCon
                    .Open()
                End If
            End With
            'end of main connection

            FILL_ComboBox()

            'find interest rate from default table
            interest_rate = selectidsql("sources", "source", "13")
            ' MsgBox(interest_rate)

            Dim ImageByte As Byte()
            Dim MemStream As MemoryStream
            cmd1 = New OleDbCommand("SELECT picturefile FROM farmers WHERE ID=" & ComboBox2.Text & "", connection)
            Try
                ImageByte = cmd1.ExecuteScalar()
                MemStream = New MemoryStream(ImageByte)
                imagename.Image = Image.FromStream(MemStream)
            Catch ex As Exception
                imagename.Image = Nothing
            End Try

        Catch ex As Exception
            'MsgBox(ex.ToString())
        End Try

        FILL_DGVOrder()
        'connection1.Close()
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

            sql = "SELECT * FROM loanholder"
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

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        Dim ImageByte As Byte()
        Dim MemStream As MemoryStream
        cmd1 = New OleDbCommand("SELECT picturefile FROM farmers WHERE ID=" & ComboBox2.Text & "", connection)
        Try
            ImageByte = cmd1.ExecuteScalar()
            MemStream = New MemoryStream(ImageByte)
            imagename.Image = Image.FromStream(MemStream)
        Catch ex As Exception
            imagename.Image = Nothing
        End Try
    End Sub


    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        ' check if users alreay take a loan or not
        Dim userId() As Integer = New Integer(DataGridView1.Rows.Count - 1) {}
            For i As Integer = 0 To DataGridView1.Rows.Count - 1
                userId(i) = DataGridView1.Rows(i).Cells(2).Value
            Next

        If userId.Contains(ComboBox2.Text) Then
            Dim results As Integer = MessageBox.Show("Farmer already take a loan." & Environment.NewLine & " Do you want to continue ? ", "Yes/No", MessageBoxButtons.YesNo)
            If results = DialogResult.No Then
                'nothing to do
                Return
            End If
        End If
        'end of checking

        'to start connection of ms-access
        connection1 = New OleDbConnection
        With connection1
            If .State = ConnectionState.Closed Then
                .ConnectionString = companyCon
                .Open()
            End If
        End With
        'end of main connection
        If ComboBox1.Text = "" OrElse ComboBox2.Text = "" OrElse TextBox1.Text = "" Then
            MsgBox("Please Insert all fields first.", MsgBoxStyle.Critical, Title:="Try again")
            ComboBox1.Select()
        Else
            If CheckBox2.Checked = False Then
                Dim currentdate, paiddate As String
                currentdate = DateTimePicker2.Value.Date
                paiddate = DateTimePicker1.Value.Date
                If paiddate = currentdate Then
                    MsgBox("Please select a date.", MsgBoxStyle.Critical, Title:="Try again")
                    DateTimePicker1.Select()
                    Return
                Else
                    Dim datedifference As Integer = DateDiff(DateInterval.Month, DateTimePicker2.Value, DateTimePicker1.Value)
                    If datedifference <= 0 Then
                        MsgBox("Date Is Invalid. Please select a new date.", MsgBoxStyle.Critical, Title:="Try again")
                        DateTimePicker1.Select()
                        Return
                    Else
                        If CheckBox1.Checked = True Then
                            insertwithdate(datedifference, 1)
                        Else
                            insertwithdate(datedifference, 0)
                        End If
                    End If
                End If
            Else
                If CheckBox1.Checked = True Then
                    insertwithcustomdate(TextBox2.Text, 1)
                Else
                    insertwithcustomdate(TextBox2.Text, 0)
                End If
            End If
            company_loan_Load(e, e)
        End If
    End Sub

    Public Sub insertwithdate(paiddate As Integer, haspartial As Integer)
        Try
            If haspartial = 1 Then
                'interest_rate
                Dim Payment As Single
                Dim LoanIRate As Single
                Dim LoanAmount As Integer
                Dim interest_rates As Single = interest_rate
                LoanIRate = 0.01 * interest_rates / 12
                LoanAmount = TextBox1.Text
                Dim payEarly As DueDate
                payEarly = DueDate.EndOfPeriod
                'emi payment
                Payment = Pmt(LoanIRate, paiddate, -LoanAmount, 0, payEarly)
                sql = "INSERT INTO loanholder (farmer_ID,myname,mytakendate,myreturndate,mypartial,mypartialamount,myamount)VALUES('" & ComboBox2.Text _
                & "','" & ComboBox1.Text & "','" & DateTimePicker2.Text & "','" & DateTimePicker1.Text & "','YES','" & Payment & "','" & TextBox1.Text & "')"
            Else
                'direct payment
                ' 500 * 17 /100 = 85 * 150days / 365
                Dim payment As Single
                Dim loanAmount As Integer
                Dim interest_rates As Single = interest_rate
                loanAmount = TextBox1.Text
                Dim days As Single = (paiddate * 30)
                payment = ((((loanAmount * interest_rates) / 100) * days) / 365) + loanAmount
                sql = "INSERT INTO loanholder (farmer_ID,myname,mytakendate,myreturndate,mypartial,mypartialamount,myamount)VALUES('" & ComboBox2.Text _
                & "','" & ComboBox1.Text & "','" & DateTimePicker2.Text & "','" & DateTimePicker1.Text & "','NO','" & payment & "','" & TextBox1.Text & "')"
            End If

            cmd = New OleDbCommand(sql, connection1)
            Dim result = cmd.ExecuteNonQuery()
            If result = 1 Then
                MsgBox("Added loan holders successfully.", MsgBoxStyle.Information, Title:="Success")
                TextBox1.Text = ""
                CheckBox2.Checked = False
                DateTimePicker1.Value = Date.Now()
                ComboBox1.Select()
            End If
        Catch ex As Exception
            'MsgBox(ex.ToString())
        End Try
    End Sub

    Public Sub insertwithcustomdate(paiddate As Integer, haspartial As Integer)
        Try
            Dim myregDate As Date = Date.Now()
            myregDate = Date.Now.AddMonths(paiddate)
            Dim mystrdate As String = myregDate.ToString("yyyy/MM/dd")
            Dim currdate As String = DateTime.Now.ToString("yyyy/MM/dd")

            If haspartial = 1 Then
                'interest_rate
                Dim Payment As Single
                Dim LoanIRate As Single
                Dim LoanAmount As Integer
                Dim interest_rates As Single = interest_rate
                LoanIRate = 0.01 * interest_rates / 12
                LoanAmount = TextBox1.Text
                Dim payEarly As DueDate
                payEarly = DueDate.EndOfPeriod
                Payment = Pmt(LoanIRate, paiddate, -LoanAmount, 0, payEarly)
                sql = "INSERT INTO loanholder (farmer_ID,myname,mytakendate,myreturndate,mypartial,mypartialamount,myamount)VALUES('" & ComboBox2.Text _
                & "','" & ComboBox1.Text & "','" & currdate & "','" & mystrdate & "','YES','" & Payment & "','" & TextBox1.Text & "')"
            Else
                Dim payment As Single
                Dim loanAmount As Integer
                Dim interest_rates As Single = interest_rate
                loanAmount = TextBox1.Text
                Dim days As Single = (paiddate * 30)
                payment = ((((loanAmount * interest_rates) / 100) * days) / 365) + loanAmount
                sql = "INSERT INTO loanholder (farmer_ID,myname,mytakendate,myreturndate,mypartial,mypartialamount,myamount)VALUES('" & ComboBox2.Text _
                & "','" & ComboBox1.Text & "','" & currdate & "','" & mystrdate & "','NO','" & payment & "','" & TextBox1.Text & "')"
            End If

            cmd = New OleDbCommand(sql, connection1)
            Dim result = cmd.ExecuteNonQuery()
            If result = 1 Then
                MsgBox("Added loan holders successfully.", MsgBoxStyle.Information, Title:="Success")
                TextBox1.Text = ""
                CheckBox2.Checked = False
                DateTimePicker1.Value = Date.Now()
                ComboBox1.Select()
            End If
        Catch ex As Exception
            ' MsgBox(ex.ToString())
        End Try
    End Sub

    Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox1.KeyPress
        e.Handled = Not (Char.IsDigit(e.KeyChar) Or e.KeyChar = "." Or Asc(e.KeyChar) = 8)
    End Sub

    Private Sub company_loan_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then Me.Close()
    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged
        If CheckBox2.Checked = True Then
            TextBox2.Visible = True
            TextBox2.Text = ""
            TextBox2.Select()
            DateTimePicker1.Enabled = False
        Else
            TextBox2.Visible = False
            TextBox2.Text = ""
            DateTimePicker1.Enabled = True
            DateTimePicker1.Select()
        End If
    End Sub

    Private Sub TextBox2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox2.KeyPress
          e.Handled = Not (Char.IsDigit(e.KeyChar) Or e.KeyChar = "." Or Asc(e.KeyChar) = 8)
    End Sub

    Private Sub DataGridView1_CellMouseUp(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataGridView1.CellMouseUp
        'Right click
        Try
            If e.Button = Windows.Forms.MouseButtons.Right Then
                DataGridView1.Rows(e.RowIndex).Selected = True
                index = e.RowIndex
                DataGridView1.CurrentCell = DataGridView1.Rows(e.RowIndex).Cells(1)
                ContextMenuStrip1.Show(DataGridView1, e.Location)
                ContextMenuStrip1.Show(Cursor.Position)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub OpenToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpenToolStripMenuItem.Click
        Try
            company_loan_more.TextBox1.Text = DataGridView1.CurrentRow.Cells(2).Value.ToString()
            company_loan_more.TextBox2.Text = DataGridView1.CurrentRow.Cells(3).Value.ToString()
            company_loan_more.DateTimePicker1.Value = DataGridView1.CurrentRow.Cells(4).Value.ToString()
            company_loan_more.DateTimePicker2.Value = DataGridView1.CurrentRow.Cells(5).Value.ToString()
            company_loan_more.Text = DataGridView1.CurrentRow.Cells(3).Value.ToString()
            company_loan_more.TextBox7.Text = DataGridView1.CurrentRow.Cells(7).Value.ToString()
            company_loan_more.TextBox8.Text = DataGridView1.CurrentRow.Cells(8).Value.ToString()
            company_loan_more.ShowDialog()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub CompleteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CompleteToolStripMenuItem.Click
        'show sure dialogbox
        Dim dresults As Integer = MessageBox.Show("Are you sure ?" & Environment.NewLine & " You want to close this statement? ", "Complete Transaction?", MessageBoxButtons.YesNo)
        If dresults = DialogResult.No Then
            'nothing to do
        ElseIf dresults = DialogResult.Yes Then
            Try
                sql = "DELETE FROM loanholder WHERE ID=" & DataGridView1.Rows(index).Cells(1).Value & ""
                cmd = New OleDbCommand(sql, connection1)
                If cmd.ExecuteNonQuery = 1 Then
                    printmessage("Successfully deleated.", "Info", "Success")
                    company_loan_Load(e, e)
                End If
            Catch ex As Exception
                'MsgBox(ex.ToString())
            End Try
        End If
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox2.SelectedIndexChanged
        Try
            sql = "select mName from farmers where ID=" & ComboBox2.Text & ""
            cmd = New OleDbCommand(sql, connection)
            adapter = New OleDbDataAdapter(cmd)
            datast = New DataSet()
            adapter.Fill(datast, "farmers")
            With ComboBox1
                .DataSource = datast.Tables(0)
                .ValueMember = "mName"
                .DisplayMember = "mName"
                .AutoCompleteMode = AutoCompleteMode.Suggest
                .AutoCompleteSource = AutoCompleteSource.ListItems
            End With
            ComboBox1.Enabled = False
            ComboBox1.BackColor = Color.White
        Catch ex As Exception
            ' MsgBox(ex.ToString())
            'MsgBox("Problem on updating data. (Error 301)", MsgBoxStyle.Critical, Title:="Try again")
        End Try
    End Sub

    Private Sub company_loan_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        imagename.Image = Nothing
    End Sub

    Private Sub ClearStatementToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ClearStatementToolStripMenuItem.Click
        ' like a delete but not
        ' we have to save history of loan in clear statement sec
        Try
            Dim dresults As Integer = MessageBox.Show("Are you sure ?" & Environment.NewLine & " You want to close this statement? ", "Complete Transaction?", MessageBoxButtons.YesNo)
            If dresults = DialogResult.No Then
                'nothing to do
            ElseIf dresults = DialogResult.Yes Then

                Using con As OleDbConnection = New OleDbConnection(companyCon)
                    Using cmd = con.CreateCommand()
                        cmd.CommandText = "INSERT INTO loanholderhist SELECT * FROM loanholder WHERE ID=@id"
                        cmd.Parameters.AddWithValue("@id", DataGridView1.Rows(index).Cells(1).Value)
                        con.Open()
                        cmd.ExecuteNonQuery()
                        con.Close()
                    End Using
                End Using

                Using con As OleDbConnection = New OleDbConnection(companyCon)
                    Using cmd = con.CreateCommand()
                        cmd.CommandText = "DELETE FROM loanholder WHERE ID=@id"
                        cmd.Parameters.AddWithValue("@id", DataGridView1.Rows(index).Cells(1).Value)
                        con.Open()
                        cmd.ExecuteNonQuery()
                        con.Close()
                    End Using
                End Using
                printmessage("Clear current loan statement", "Info", "Clear statement")
                company_loan_Load(e, e)
            End If

        Catch ex As Exception
            MsgBox(ex.ToString())
        End Try
    End Sub

    Private Sub loanhist_Click(sender As Object, e As EventArgs) Handles loanhist.Click
        company_loan_hist.showDialog()
    End Sub
End Class