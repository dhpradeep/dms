Imports System.Data.OleDb
Imports System.IO
Public Class shareholders_bonus
    Public interest_rate As String
    Public results As Double = 0.0

    Private Sub shareholders_bonus_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ComboBox2.Enabled = False
        TextBox2.Text = ""

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
            'to start connection of ms-access
            connection1 = New OleDbConnection
            With connection1
                If .State = ConnectionState.Closed Then
                    .ConnectionString = companyCon
                    .Open()
                End If
            End With
            'end of main connection

            sql = "select mName from farmers where hasShare='YES'"
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

            interest_rate = selectidsql("sources", "source", "13")

            FILL_DGVOrder()

        Catch ex As Exception

        End Try

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

    Private Sub FILL_DGVOrder()
        Try
            Dim i As Integer = 1
            DataGridView1.Rows.Clear()

            DataGridView1.ColumnCount = 6
            With DataGridView1
                .RowHeadersVisible = False
                .Columns(0).HeaderCell.Value = "S.N"
                .Columns(1).HeaderCell.Value = "Name"
                .Columns(2).HeaderCell.Value = "Insert Date"
                .Columns(3).HeaderCell.Value = "Share Amount"
                .Columns(4).HeaderCell.Value = "Interest Rate"
                .Columns(5).HeaderCell.Value = "Bonus Amount"
                .Columns(0).SortMode = DataGridViewColumnSortMode.NotSortable
                .Columns(1).SortMode = DataGridViewColumnSortMode.NotSortable
                .Columns(2).SortMode = DataGridViewColumnSortMode.NotSortable
                .Columns(3).SortMode = DataGridViewColumnSortMode.NotSortable
                .Columns(4).SortMode = DataGridViewColumnSortMode.NotSortable
                .Columns(5).SortMode = DataGridViewColumnSortMode.NotSortable
                .AllowUserToAddRows = False
                .AllowUserToResizeRows = False
                .AllowUserToOrderColumns = False
                .AllowUserToResizeColumns = True
            End With
            DataGridView1.Columns(0).FillWeight = 15
            DataGridView1.Columns(1).FillWeight = 30
            DataGridView1.Columns(2).FillWeight = 30
            DataGridView1.Columns(3).FillWeight = 25
            DataGridView1.Columns(4).FillWeight = 25
            DataGridView1.Columns(5).FillWeight = 25

            sql = "SELECT mName,insertdate,shareamount,interestrate,bonusamount FROM sharebonus"
            cmd = New OleDbCommand(sql, connection1)
            reader = cmd.ExecuteReader()
            If Not reader.HasRows Then
                DataGridView1.Rows().Clear()
            ElseIf reader.HasRows Then
                Do While reader.Read
                    Dim row() As String = {CStr(i), CStr(reader.Item(0)), CStr(reader.Item(1)), CStr(reader.Item(2)), CStr(reader.Item(3)), CStr(reader.Item(4))}
                    DataGridView1.Rows.Add(row)
                    i += 1
                Loop
            End If
        Catch ex As Exception
            'MsgBox(ex.ToString())
        End Try
    End Sub


    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        Try
            sql = "select ID from farmers where mName='" & ComboBox1.Text & "'"
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
            ComboBox2.Enabled = False
            ComboBox2.BackColor = Color.White
        Catch ex As Exception
            MsgBox("Problem on updating data. (Error 301)", MsgBoxStyle.Critical, Title:="Try again")
        End Try
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If TextBox2.Text = "" Then
            MsgBox("please insert share amount first.", MsgBoxStyle.Critical, Title:="Try again")
            TextBox2.Text = ""
            TextBox2.Select()
        ElseIf Not Double.TryParse(TextBox2.Text, results) Then
            MsgBox("Amount must be valid", MsgBoxStyle.Critical, Title:="Try again")
            TextBox2.Text = ""
            TextBox2.Select()
        Else
            Try
                Dim bonusamount As Double
                bonusamount = (CDbl(interest_rate) * CDbl(TextBox2.Text)) / 100
                Dim insertDate As String = System.DateTime.Now.ToString("dd/MM/yyyy")
                sql = "INSERT INTO sharebonus(mName,insertdate,shareamount,interestrate,bonusamount)VALUES('" & ComboBox1.Text & "','" & insertDate & "','" & TextBox2.Text & "','" & interest_rate & "','" & bonusamount & "')"
                cmd = New OleDbCommand(sql, connection1)
                Dim result = cmd.ExecuteNonQuery()
                If result = 1 Then
                    MsgBox("successfully inserted")
                    shareholders_bonus_Load(e, e)
                End If
            Catch ex As Exception
                'MsgBox(ex.ToString())
            End Try
        End If
    End Sub

    Private Sub TextBox2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox2.KeyPress
        e.Handled = Not (Char.IsDigit(e.KeyChar) Or e.KeyChar = "." Or Asc(e.KeyChar) = 8)
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox2.SelectedIndexChanged
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

    Private Sub shareholders_bonus_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        imagename.Image = Nothing
    End Sub
End Class