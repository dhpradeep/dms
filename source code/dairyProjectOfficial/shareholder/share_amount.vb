Imports System.Data.OleDb
Imports System.IO
Public Class share_amount
    Public results As Double = 0.0
    Dim index As Integer = 0
    Public interest_rate, interest, new_interest, interest_amount, total_amt As Double
    Private Sub share_amount_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ComboBox1.Enabled = False
        TextBox2.Text = ""
        ComboBox2.Text = ""
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
            connection1 = New OleDbConnection
            With connection1
                If .State = ConnectionState.Closed Then
                    .ConnectionString = companyCon
                    .Open()
                End If
            End With
            'end of main connection

            FILL_ComboBox()

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

            FILL_DGVOrder()

            interest_rate = selectidsql("sources", "source", "16")
            interest = selectidsql("sources", "source", "13")

        Catch ex As Exception

        End Try
    End Sub

    Private Sub FILL_DGVOrder()
        Try
            Dim i As Integer = 1
            DataGridView1.Rows.Clear()

            DataGridView1.ColumnCount = 5
            With DataGridView1
                .RowHeadersVisible = False
                .Columns(0).HeaderCell.Value = "S.N"
                .Columns(1).HeaderCell.Value = "Transaction ID"
                .Columns(2).HeaderCell.Value = "Farmer ID"
                .Columns(3).HeaderCell.Value = "Name"
                .Columns(4).HeaderCell.Value = "Amount"
                .Columns(0).SortMode = DataGridViewColumnSortMode.NotSortable
                .Columns(1).SortMode = DataGridViewColumnSortMode.NotSortable
                .Columns(2).SortMode = DataGridViewColumnSortMode.NotSortable
                .Columns(3).SortMode = DataGridViewColumnSortMode.NotSortable
                .Columns(4).SortMode = DataGridViewColumnSortMode.NotSortable
                .AllowUserToAddRows = False
                .AllowUserToResizeRows = False
                .AllowUserToOrderColumns = False
                .AllowUserToResizeColumns = False
            End With
            DataGridView1.Columns(0).FillWeight = 15
            DataGridView1.Columns(1).FillWeight = 15
            DataGridView1.Columns(2).FillWeight = 15
            DataGridView1.Columns(3).FillWeight = 30
            DataGridView1.Columns(4).FillWeight = 20

            sql = "SELECT * FROM shareamount"
            cmd = New OleDbCommand(sql, connection1)
            reader = cmd.ExecuteReader()
            If Not reader.HasRows Then
                DataGridView1.Rows().Clear()
            ElseIf reader.HasRows Then
                Do While reader.Read
                    Dim row() As String = {CStr(i), reader.Item(0), CStr(reader.Item(1)), CStr(reader.Item(2)), CStr(reader.Item(3))}
                    DataGridView1.Rows.Add(row)
                    i += 1
                Loop
            End If
        Catch ex As Exception
            'MsgBox(ex.ToString())
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

    Private Sub share_amount_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then Me.Close()
    End Sub

    Private Sub DeleteShareToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DeleteShareToolStripMenuItem.Click
        Dim amount As String
        Dim total_interest, interest_amount As Double
        amount = InputBox("Enter new amount", "Amount", DataGridView1.Rows(index).Cells(4).Value)
        If String.IsNullOrEmpty(amount) Then
            Return
        Else
            If Not Double.TryParse(amount, results) Then
                printmessage("Amount must be double.", "Error", "Try again")
                Return
            Else
                total_interest = interest - interest_rate
                interest_amount = (CDbl(amount) * total_interest) / 100
                sql = "UPDATE shareamount SET amount='" & CDbl(amount) _
                       & "' WHERE farmer_ID=" & DataGridView1.Rows(index).Cells(2).Value & ""
                cmd = New OleDbCommand(sql, connection1)
                cmd.ExecuteNonQuery()

                DataGridView1.Rows(index).Cells(4).Value = amount
            End If
        End If
    End Sub

    Private Sub DeleteShareToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles DeleteShareToolStripMenuItem1.Click
        Dim results As Integer = MessageBox.Show("Are you sure ?" & Environment.NewLine & "You can't undo this action.", "Confirmation Box", MessageBoxButtons.YesNo)
        If results = DialogResult.No Then
            'nothing to do
        ElseIf results = DialogResult.Yes Then
            'delete from database
            Try
                sql = "DELETE FROM shareamount WHERE ID=" & DataGridView1.Rows(index).Cells(1).Value & ""
                cmd = New OleDbCommand(sql, connection1)
                If cmd.ExecuteNonQuery = 1 Then
                    printmessage("Successfully deleted.", "Info", "Success")
                    share_amount_Load(e, e)
                End If
            Catch ex As Exception
                'MsgBox(ex.ToString())
            End Try
        End If
    End Sub

    Private Sub TransferShareToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TransferShareToolStripMenuItem.Click
        printmessage("We will add this features soon.", "Info", "Not available")
        'share_amount_transfer.TextBox1.Text = DataGridView1.Rows(index).Cells(2).Value
        'share_amount_transfer.ShowDialog()
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

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        ' check if users previous balance alreay exist or not
        If DataGridView1.Rows.Count > 0 Then
            Dim userId() As Integer = New Integer(DataGridView1.Rows.Count - 1) {}
            For i As Integer = 0 To DataGridView1.Rows.Count - 1
                userId(i) = DataGridView1.Rows(i).Cells(2).Value
            Next

            If userId.Contains(ComboBox2.Text) Then
                printmessage("Users alreay exist in table.", "Error", "Try again")
                Return
            End If
        End If
        'end of checking

        If ComboBox2.Text = "" OrElse TextBox2.Text = "" Then
            printmessage("Please fill out all fields.", "Error", "Try again")
            Return
        ElseIf Not Double.TryParse(TextBox2.Text, results) Then
            printmessage("Amount must be valid", "Error", "Try again")
            TextBox2.Text = ""
            TextBox2.Select()
            Return
        Else
            Try
                sql = "INSERT INTO shareamount(farmer_ID,mName,amount)VALUES('" & ComboBox2.Text & "','" & ComboBox1.Text & "','" & TextBox2.Text & "')"
                cmd = New OleDbCommand(sql, connection1)
                If cmd.ExecuteNonQuery() = 1 Then
                    printmessage("Successfully Inserted.", "Info", "Success")
                    share_amount_Load(e, e)
                End If
            Catch ex As Exception
                'MsgBox(ex.ToString())
                printmessage("Error on Insertion.", "Error", "Try again")
            End Try
        End If
    End Sub

End Class