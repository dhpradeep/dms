Imports System.Data.OleDb
Imports iTextSharp.text.pdf
Imports iTextSharp.text
Imports System.IO
Imports System.Text.RegularExpressions
Public Class detailsForm
    Dim totalMilk As Double = 0
    Dim totalFat As Double = 0
    Dim totalLacto As Double = 0
    Dim totalfatkg As Double
    Dim totalfatrs As Double = 0
    Dim totalsnfkg As Double
    Dim totalsnfrs As Double = 0
    Dim totalsnf As Double
    Dim totalamt As Double
    Dim a As Integer
    Dim ts As Double
    Dim index As Integer = 0

    Public hasShares As String

    Private Sub detailsForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' for limiting the form with standard time format
        Dim baseDateString = "14/04/2018"

        Dim baseDate As Date = Date.ParseExact(baseDateString, "dd/MM/yyyy",
        System.Globalization.DateTimeFormatInfo.InvariantInfo)
        Dim datetimeBetween = DateTime.Today.Subtract(baseDate)
        Dim daysBetween = datetimeBetween.Days
        'Dim dayNumber = daysBetween Mod 15 + 1
        Dim dayNumber = daysBetween Mod 15 + 1

        Label7.Text = dayNumber
        ' end of' 
        'If Label7.Text >= 15 Then
        printReceip.Enabled = True

        ' default values
        mName.Enabled = False
        mName.BackColor = Color.White
        isid.BackColor = Color.White
        If hasShares = "YES" Then
            hasShare.Checked = True
        Else
            hasShare.Checked = False
        End If

        Try
            'to start connection of ms-access
            connection = New OleDbConnection
            With connection
                If .State = ConnectionState.Closed Then
                    .ConnectionString = strCon
                    .Open()
                End If
            End With

            Dim ImageByte As Byte()
            Dim MemStream As MemoryStream
            cmd1 = New OleDbCommand("SELECT picturefile FROM farmers WHERE ID=" & isid.Text & "", connection)
            Try
                ImageByte = cmd1.ExecuteScalar()
                MemStream = New MemoryStream(ImageByte)
                imagename.Image = System.Drawing.Image.FromStream(MemStream)
            Catch ex As Exception
                imagename.Image = Nothing
            End Try


            Dim newlimit As String = CInt(Label7.Text) * 2
            FILL_DGVOrder()

        Catch ex As Exception
            MsgBox(ex.ToString())
            'MsgBox("Error occurs while loading dataview. (Error code: 304)", MsgBoxStyle.Critical, Title:="Try again")
            'Me.Close()
        End Try
    End Sub

    Private Sub FILL_DGVOrder()
        Try

            Dim i As Integer = 1
            DataGridView1.Rows.Clear()
            sql = "SELECT ID,Entry_date,Entry_Shift,milk_qty,fat_qty,lacto_qty FROM detailsFarmer WHERE farmers_ID=" & isid.Text & " ORDER BY detailsFarmer.ID DESC"
            cmd = New OleDbCommand(sql, connection)

            DataGridView1.ColumnCount = 7
            With DataGridView1
                .RowHeadersVisible = False
                .Columns(0).HeaderCell.Value = "S.N"
                .Columns(1).HeaderCell.Value = "ID"
                .Columns(2).HeaderCell.Value = "Entry Date"
                .Columns(3).HeaderCell.Value = "Entry Shift"
                .Columns(4).HeaderCell.Value = "milk Qty/ltr"
                .Columns(5).HeaderCell.Value = "fat Qty"
                .Columns(6).HeaderCell.Value = "Lacto Qty"
                .Columns(0).SortMode = DataGridViewColumnSortMode.NotSortable
                .Columns(1).SortMode = DataGridViewColumnSortMode.NotSortable
                .Columns(2).SortMode = DataGridViewColumnSortMode.NotSortable
                .Columns(3).SortMode = DataGridViewColumnSortMode.NotSortable
                .Columns(4).SortMode = DataGridViewColumnSortMode.NotSortable
                .Columns(5).SortMode = DataGridViewColumnSortMode.NotSortable
                .Columns(6).SortMode = DataGridViewColumnSortMode.NotSortable
                .AllowUserToAddRows = False
                .AllowUserToResizeRows = False
                .AllowUserToOrderColumns = False
                .AllowUserToResizeColumns = True
            End With
            DataGridView1.Columns(0).FillWeight = 10
            DataGridView1.Columns(1).FillWeight = 15
            DataGridView1.Columns(2).FillWeight = 25
            DataGridView1.Columns(3).FillWeight = 25
            DataGridView1.Columns(4).FillWeight = 25
            DataGridView1.Columns(5).FillWeight = 25
            DataGridView1.Columns(6).FillWeight = 25

            reader = cmd.ExecuteReader()
            If Not reader.HasRows Then
                DataGridView1.Rows().Clear()
            ElseIf reader.HasRows Then
                Do While reader.Read
                    Dim row() As String = {CStr(i), (reader.Item(0)), reader.Item(1), reader.Item(2), reader.Item(3), reader.Item(4), reader.Item(5)}
                    DataGridView1.Rows.Add(row)
                    i += 1
                Loop
                Dim total_milk As Double = 0
                Dim total_fat As Double = 0
                Dim total_lacto As Double = 0
                'find out avg. fat and avg. lacto
                Dim avgfatval As Integer = 0
                Dim avglactoval As Integer = 0
                Dim totalavgfat As Double = 0
                Dim totalavglacto As Double = 0
                For j As Integer = 0 To DataGridView1.RowCount - 1
                    If Regex.IsMatch(DataGridView1.Rows(j).Cells(4).Value, "^[0-9.]+$") Then
                        total_milk += Val(CDbl(DataGridView1.Rows(j).Cells(4).Value))
                    End If
                    If Regex.IsMatch(DataGridView1.Rows(j).Cells(5).Value, "^[0-9.]+$") Then
                        total_fat += Val(CDbl(DataGridView1.Rows(j).Cells(5).Value))
                        avgfatval += 1
                    End If
                    If Regex.IsMatch(DataGridView1.Rows(j).Cells(6).Value, "^[0-9.]+$") Then
                        total_lacto += Val(CDbl(DataGridView1.Rows(j).Cells(6).Value))
                        avglactoval += 1
                    End If
                Next
                avgfat.Text = avgfatval
                avglacto.Text = avglactoval

                If total_fat = 0 Then
                    total_fat = 1
                End If
                If total_lacto = 0 Then
                    total_lacto = 1
                End If
                If avgfatval = 0 Then
                    avgfatval = 1
                End If
                If avglactoval = 0 Then
                    avglactoval = 1
                End If

                totalavgfat = total_fat / avgfatval
                totalavgfat = totalavgfat.ToString("N2")
                totalavglacto = total_lacto / avglactoval
                totalavglacto = totalavglacto.ToString("N2")

                Dim aa As String = ""
                For j As Integer = 0 To DataGridView1.Rows.Count() Step +1
                    j = +j
                    a = j / 2
                    aa = a.ToString
                Next
                Dim aaa As String = aa + " days"

                Dim rows As String() = {"", "", "TOTAL(Avg.)", aaa, total_milk, totalavgfat, totalavglacto}
                DataGridView1.Rows.Add(rows)
            End If

        Catch ex As Exception
            'MsgBox(ex.ToString())
        End Try
    End Sub


    Private Sub editButton_Click(sender As Object, e As EventArgs) Handles editButton.Click
        editFarmer.mid.Text = isid.Text
        editFarmer.ShowDialog()
    End Sub

    Private Sub detailsForm_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        mName.Enabled = False
        editButton.Enabled = True
        deleteButton.Enabled = True
        moreDetails.Enabled = True
        milkStore.Enabled = True
        printReceip.Enabled = True
        Me.Dispose()
        Me.Close()
    End Sub

    Private Sub deleteButton_Click(sender As Object, e As EventArgs) Handles deleteButton.Click
        Dim result As Integer = MessageBox.Show("Are You sure ? " & Environment.NewLine & " You can't undo this action.", "Confirmation Box", MessageBoxButtons.YesNoCancel)
        If result = DialogResult.Cancel Then
            'nothing to do
        ElseIf result = DialogResult.No Then
            'nothing to do
        ElseIf result = DialogResult.Yes Then
            Try
                'to start connection of ms-access
                connection = New OleDbConnection
                With connection
                    If .State = ConnectionState.Closed Then
                        .ConnectionString = strCon
                        .Open()
                    End If
                End With
                'to start connection of ms-access
                connection1 = New OleDbConnection
                With connection1
                    If .State = ConnectionState.Closed Then
                        .ConnectionString = companyCon
                        .Open()
                    End If
                End With

                DeleteUserFromEverywhere()

            Catch ex As Exception

            End Try
        End If
    End Sub

    Private Sub DeleteUserFromEverywhere()
        Dim sql1, sql2, sql3, sql4, sql5, sql6, sql7, sql8 As String
        Try
            sql8 = "DELETE FROM farmers WHERE ID = " & isid.Text & ""
            cmd6 = New OleDbCommand(sql8, connection)
            cmd6.ExecuteNonQuery()

            printmessage("Successfully deleted farmer '" + mName.Text + "'.", "Info", "Success")
            Me.Close()

        Catch ex As Exception
            MsgBox("Error on deleting users.(Error code: 305)", MsgBoxStyle.Critical, Title:="Try again")
            'MsgBox(ex.ToString())
        End Try
        Try
            sql = "DELETE FROM calculation WHERE ID = " & isid.Text & ""
            cmd = New OleDbCommand(sql, connection)
            cmd.ExecuteNonQuery()
        Catch ex As Exception

        End Try
        Try
            sql1 = "DELETE * FROM detailsFarmerHistory WHERE farmers_ID=" & isid.Text & ""
            cmd = New OleDbCommand(sql1, connection)
            cmd.ExecuteNonQuery()
        Catch ex As Exception
        End Try
        Try
            sql2 = "DELETE FROM myhistory WHERE ID=" & isid.Text & ""
            cmd1 = New OleDbCommand(sql2, connection)
            cmd1.ExecuteNonQuery()
        Catch ex As Exception
        End Try
        Try
            sql3 = "DELETE FROM s_bonus WHERE f_ID=" & isid.Text & ""
            cmd2 = New OleDbCommand(sql3, connection)
            cmd2.ExecuteNonQuery()
        Catch ex As Exception
        End Try
        Try
            sql4 = "DELETE FROM shareHolders WHERE f_ID=" & isid.Text & ""
            cmd3 = New OleDbCommand(sql4, connection)
            cmd3.ExecuteNonQuery()
        Catch ex As Exception
        End Try
        Try
            sql5 = "DELETE FROM loanholder WHERE farmer_ID='" & isid.Text & "'"
            cmd4 = New OleDbCommand(sql5, connection1)
            cmd4.ExecuteNonQuery()
        Catch ex As Exception
        End Try
        Try
            sql6 = "DELETE FROM previousbalance WHERE farmer_ID=" & isid.Text & ""
            cmd5 = New OleDbCommand(sql6, connection1)
            cmd5.ExecuteNonQuery()
        Catch ex As Exception
        End Try
        Try
            sql7 = "DELETE FROM sharebonus WHERE farmer_ID=" & isid.Text & ""
        Catch ex As Exception
        End Try

    End Sub

    Private Sub moreDetails_Click(sender As Object, e As EventArgs) Handles moreDetails.Click
        farmersMoreDetails.Text = isid.Text + " . " + mName.Text
        farmersMoreDetails.myId.Text = isid.Text
        farmersMoreDetails.ShowDialog()
    End Sub

    Private Sub milkStore_Click(sender As Object, e As EventArgs) Handles milkStore.Click
        storeMilk.isIds.Text = isid.Text
        storeMilk.ShowDialog(Me)
    End Sub

    Private Sub printReceip_Click(sender As Object, e As EventArgs) Handles printReceip.Click
        Try
            Dim mywidth() As Integer = {20, 20, 25, 25, 25, 25, 25}
            printData(DataGridView1, isid.Text, mName.Text, mywidth)

            printmessage("Successfully print document as PDF.", "Info", "Successful")

        Catch ex As Exception
            ' MsgBox(ex.ToString())
            MsgBox("Previous PDF file is in use. (Error code: 306)", MsgBoxStyle.Critical, Title:="Try again")
        End Try

    End Sub

    Private Sub detailsForm_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        ElseIf e.KeyCode = Keys.F5 Then
            Button1_Click(e, e)
        End If
    End Sub

    Private Sub DataGridView1_CellMouseUp(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataGridView1.CellMouseUp
        'Right click
        Try
            If e.Button = Windows.Forms.MouseButtons.Right Then
                DataGridView1.Rows(e.RowIndex).Selected = True
                Index = e.RowIndex
                DataGridView1.CurrentCell = DataGridView1.Rows(e.RowIndex).Cells(1)
                ContextMenuStrip1.Show(DataGridView1, e.Location)
                ContextMenuStrip1.Show(Cursor.Position)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub DeleteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DeleteToolStripMenuItem.Click
        Dim dresults As Integer = MessageBox.Show("Are you sure ?" & Environment.NewLine & " You want to delete this statement? ", "Delete record?", MessageBoxButtons.YesNo)
        If dresults = DialogResult.No Then
            'nothing to do
        ElseIf dresults = DialogResult.Yes Then
            Try
                sql = "DELETE FROM detailsFarmer WHERE ID=" & DataGridView1.Rows(index).Cells(1).Value & ""
                cmd = New OleDbCommand(sql, connection)
                If cmd.ExecuteNonQuery = 1 Then
                    printmessage("Successfully deleated.", "Info", "Success")
                    detailsForm_Load(e, e)
                End If
            Catch ex As Exception
                'MsgBox(ex.ToString())
            End Try
        End If
    End Sub

    Private Sub EditToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EditToolStripMenuItem.Click
        Try
            detailsForm_edit.isIds.Text = DataGridView1.Rows(index).Cells(1).Value
            detailsForm_edit.isMilk.Text = DataGridView1.Rows(index).Cells(4).Value
            detailsForm_edit.isFat.Text = DataGridView1.Rows(index).Cells(5).Value
            detailsForm_edit.isLacto.Text = DataGridView1.Rows(index).Cells(6).Value
            detailsForm_edit.DateTimePicker1.Value = DataGridView1.Rows(index).Cells(2).Value
            detailsForm_edit.TextBox1.Text = DataGridView1.Rows(index).Cells(3).Value
            detailsForm_edit.ShowDialog()
        Catch ex As Exception
            'MsgBox(ex.ToString())
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        detailsForm_Load(e, e)
    End Sub
End Class