Imports System.Data.OleDb
Public Class otherSetting_account
    Dim purchse, sales, expense, fine, cogs, capital As Double
    Dim index As Integer = 0
    Dim indexs As Integer = 0

    Dim shareholder, commission, loan, retainearning As Double

    Private Sub otherSetting_account_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        itemname.Text = ""
        TextBox1.Text = ""
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

            FillItemType()

            FillComboBox()

            FILL_DGVOrder()

            FILL_DGVOrder2()

        Catch ex As Exception
            'MsgBox(ex.ToString())
        End Try
    End Sub

    Private Sub ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem1.Click
        Try
            Dim dresults As Integer = MessageBox.Show("Are you sure ?" & Environment.NewLine & " You want to close this statement? ", "Complete Transaction?", MessageBoxButtons.YesNo)
            If dresults = DialogResult.No Then
                'nothing to do
            ElseIf dresults = DialogResult.Yes Then
                sql = "DELETE FROM acctype WHERE ID=" & DataGridView2.Rows(indexs).Cells(1).Value & ""
                cmd = New OleDbCommand(sql, connection1)
                If cmd.ExecuteNonQuery = 1 Then
                    printmessage("Successfully deleted.", "Info", "Success")
                    otherSetting_account_Load(e, e)
                End If
            End If
        Catch ex As Exception
            ' MsgBox(ex.ToString())
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If TextBox1.Text = "" Then
            printmessage("Please Insert Item First", "Error", "Try again")
            TextBox1.Select()
        Else
            Try
                sql = "INSERT INTO acctype(types,maintype)VALUES('" & TextBox1.Text & "','" & ComboBox1.Text & "')"
                cmd = New OleDbCommand(sql, connection1)

                Dim sql2 As String = "SELECT types FROM acctype WHERE maintype='" & ComboBox1.Text & "'"
                cmd1 = New OleDbCommand(sql2, connection1)
                reader = cmd1.ExecuteReader()
                If reader.HasRows Then
                    Do While reader.Read
                        If TextBox1.Text = CStr(reader.Item(0)) Then
                            printmessage("Item already exist.", "Error", "Try again")
                            TextBox1.Text = ""
                            TextBox1.Select()
                            Return
                        End If
                    Loop
                End If

                Dim result = cmd.ExecuteNonQuery()
                If result = 1 Then
                    printmessage("Successfully Inserted", "Info", "Success")
                    otherSetting_account_Load(e, e)
                End If

            Catch ex As Exception
                MsgBox(ex.ToString())
            End Try
        End If
    End Sub

    Private Sub otherSetting_account_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then Me.Close()
    End Sub

    Private Sub DeleteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DeleteToolStripMenuItem.Click
        Try
            Dim dresults As Integer = MessageBox.Show("Are you sure ?" & Environment.NewLine & " You want to close this statement? ", "Complete Transaction?", MessageBoxButtons.YesNo)
            If dresults = DialogResult.No Then
                'nothing to do
            ElseIf dresults = DialogResult.Yes Then
                sql = "DELETE FROM cashflow WHERE ID=" & DataGridView1.Rows(index).Cells(1).Value & ""
                cmd = New OleDbCommand(sql, connection1)
                If cmd.ExecuteNonQuery = 1 Then
                    printmessage("Successfully deleted.", "Info", "Success")
                    otherSetting_account_Load(e, e)
                End If
            End If
        Catch ex As Exception
            ' MsgBox(ex.ToString())
        End Try
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

    Private Sub DataGridView2_CellMouseUp(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataGridView2.CellMouseUp
        'Right click
        Try
            If e.Button = Windows.Forms.MouseButtons.Right Then
                DataGridView2.Rows(e.RowIndex).Selected = True
                indexs = e.RowIndex
                DataGridView2.CurrentCell = DataGridView2.Rows(e.RowIndex).Cells(1)
                ContextMenuStrip2.Show(DataGridView2, e.Location)
                ContextMenuStrip2.Show(Cursor.Position)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub FILL_DGVOrder()
        Try
            Dim i As Integer = 1
            DataGridView1.Rows.Clear()

            DataGridView1.ColumnCount = 4
            With DataGridView1
                .RowHeadersVisible = False
                .Columns(0).HeaderCell.Value = "S.N"
                .Columns(1).HeaderCell.Value = "Item ID"
                .Columns(2).HeaderCell.Value = "Item Name"
                .Columns(3).HeaderCell.Value = "Item Type"
                .Columns(0).SortMode = DataGridViewColumnSortMode.NotSortable
                .Columns(1).SortMode = DataGridViewColumnSortMode.NotSortable
                .Columns(2).SortMode = DataGridViewColumnSortMode.NotSortable
                .Columns(3).SortMode = DataGridViewColumnSortMode.NotSortable
                .AllowUserToAddRows = False
                .AllowUserToResizeRows = False
                .AllowUserToOrderColumns = False
                .AllowUserToResizeColumns = False
            End With
            DataGridView1.Columns(0).FillWeight = 15
            DataGridView1.Columns(1).FillWeight = 15
            DataGridView1.Columns(2).FillWeight = 25
            DataGridView1.Columns(3).FillWeight = 20

            sql = "SELECT ID,particulars,acctype FROM cashflow ORDER BY ID DESC"
            cmd = New OleDbCommand(sql, connection1)
            reader = cmd.ExecuteReader()
            If Not reader.HasRows Then
                DataGridView1.Rows().Clear()
            ElseIf reader.HasRows Then
                Do While reader.Read
                    Dim row() As String = {CStr(i), CStr(reader.Item(0)), CStr(reader.Item(1)), reader.Item(2)}
                    DataGridView1.Rows.Add(row)
                    i += 1
                Loop
            End If
        Catch ex As Exception
            ' MsgBox(ex.ToString())
        End Try
    End Sub

    Private Sub FILL_DGVOrder2()
        Try
            Dim i As Integer = 1
            DataGridView2.Rows.Clear()

            DataGridView2.ColumnCount = 4
            With DataGridView2
                .RowHeadersVisible = False
                .Columns(0).HeaderCell.Value = "S.N"
                .Columns(1).HeaderCell.Value = "Item ID"
                .Columns(2).HeaderCell.Value = "Item Name"
                .Columns(3).HeaderCell.Value = "Item Type"
                .Columns(0).SortMode = DataGridViewColumnSortMode.NotSortable
                .Columns(1).SortMode = DataGridViewColumnSortMode.NotSortable
                .Columns(2).SortMode = DataGridViewColumnSortMode.NotSortable
                .Columns(3).SortMode = DataGridViewColumnSortMode.NotSortable
                .AllowUserToAddRows = False
                .AllowUserToResizeRows = False
                .AllowUserToOrderColumns = False
                .AllowUserToResizeColumns = False
            End With
            DataGridView2.Columns(0).FillWeight = 15
            DataGridView2.Columns(1).FillWeight = 15
            DataGridView2.Columns(2).FillWeight = 25
            DataGridView2.Columns(3).FillWeight = 20

            sql = "SELECT ID,types,maintype FROM acctype ORDER BY ID DESC"
            cmd = New OleDbCommand(sql, connection1)
            reader = cmd.ExecuteReader()
            If Not reader.HasRows Then
                DataGridView2.Rows().Clear()
            ElseIf reader.HasRows Then
                Do While reader.Read
                    Dim row() As String = {CStr(i), CStr(reader.Item(0)), CStr(reader.Item(1)), reader.Item(2)}
                    DataGridView2.Rows.Add(row)
                    i += 1
                Loop
            End If
        Catch ex As Exception
            'MsgBox(ex.ToString())
        End Try
    End Sub

    Private Sub FillItemType()
        Try
            sql = "select types FROM acctype"
            cmd = New OleDbCommand(sql, connection1)
            adapter = New OleDbDataAdapter(cmd)
            datast = New DataSet()
            adapter.Fill(datast, "acctype")
            With itemtype
                .DataSource = datast.Tables(0)
                .ValueMember = "types"
                .DisplayMember = "types"
            End With
        Catch ex As Exception
            MsgBox("Item Type error. " + ex.ToString())
        End Try
    End Sub

    Private Sub FillComboBox()
        Try
            sql = "select types FROM accmaintype"
            cmd = New OleDbCommand(sql, connection1)
            adapter = New OleDbDataAdapter(cmd)
            datast = New DataSet()
            adapter.Fill(datast, "accmaintype")
            With ComboBox1
                .DataSource = datast.Tables(0)
                .ValueMember = "types"
                .DisplayMember = "types"
            End With
        Catch ex As Exception
            MsgBox("Item Type error. " + ex.ToString())
        End Try
    End Sub



    Private Sub addExpenses_Click(sender As Object, e As EventArgs) Handles addExpenses.Click
        If itemname.Text = "" Then
            printmessage("Please Insert Item First", "Error", "Try again")
            itemname.Select()
        Else
            Try
                Dim itemtypes As String = selectanysql("acctype", "maintype", itemtype.Text, "types")

                sql = "INSERT INTO cashflow(particulars,amount,acctype,maintype)VALUES('" & itemname.Text & "',0,'" & itemtype.Text & "','" & itemtypes & "')"
                cmd = New OleDbCommand(sql, connection1)

                Dim sql2 As String = "SELECT particulars FROM cashflow WHERE acctype='" & itemtype.Text & "'"
                cmd1 = New OleDbCommand(sql2, connection1)
                reader = cmd1.ExecuteReader()
                If reader.HasRows Then
                    Do While reader.Read
                        If itemname.Text = CStr(reader.Item(0)) Then
                            printmessage("Item already exist.", "Error", "Try again")
                            itemname.Text = ""
                            itemname.Select()
                            Return
                        End If
                    Loop
                End If

                Dim result = cmd.ExecuteNonQuery()
                If result = 1 Then
                    printmessage("Successfully Inserted", "Info", "Success")
                    otherSetting_account_Load(e, e)
                End If
            Catch ex As Exception
                'MsgBox(ex.ToString())
            End Try
        End If
    End Sub

End Class