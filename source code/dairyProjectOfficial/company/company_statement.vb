Imports System.Data.OleDb
Imports iTextSharp.text.pdf
Imports iTextSharp.text
Imports System.IO
Public Class company_statement

    Dim index As Integer = 0

    Private Sub company_statement_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Button1.Visible = False

        connection1 = New OleDbConnection
        With connection1
            If .State = ConnectionState.Closed Then
                .ConnectionString = companyCon
                .Open()
            End If
        End With

        FILL_DGVOrder()

        FILL_DGVOrder2()
    End Sub

    Private Sub FILL_DGVOrder()
        Try

            Dim i As Integer = 1
        DataGridView1.Rows.Clear()
        sql = "SELECT * FROM incomestatement"
        cmd = New OleDbCommand(sql, connection1)

        DataGridView1.ColumnCount = 6
        With DataGridView1
            .RowHeadersVisible = False
            .Columns(0).HeaderCell.Value = "S.N"
            .Columns(1).HeaderCell.Value = "Transaction ID"
            .Columns(2).HeaderCell.Value = "Insert Date"
            .Columns(3).HeaderCell.Value = "Statement"
            .Columns(4).HeaderCell.Value = "Quantity"
            .Columns(5).HeaderCell.Value = "Amount"
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
        DataGridView1.Columns(0).FillWeight = 10
        DataGridView1.Columns(1).FillWeight = 15
        DataGridView1.Columns(2).FillWeight = 25
        DataGridView1.Columns(3).FillWeight = 35
        DataGridView1.Columns(4).FillWeight = 25
        DataGridView1.Columns(5).FillWeight = 25


            reader = cmd.ExecuteReader()
            If Not reader.HasRows Then
                DataGridView1.Rows().Clear()
            ElseIf reader.HasRows Then
                Do While reader.Read
                    Dim row() As String = {CStr(i), (reader.Item(0)), reader.Item(1), reader.Item(2), reader.Item(3), reader.Item(4)}
                    DataGridView1.Rows.Add(row)
                    i += 1
                Loop
                Dim total As String = 0
                For j As Integer = 0 To DataGridView1.RowCount - 1
                    total += Val(CDbl(DataGridView1.Rows(j).Cells(5).Value))
                Next
                Dim rows As String() = {"", "", "", "", "Total", total}
                DataGridView1.Rows.Add(rows)
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub FILL_DGVOrder2()
        Try
            Dim i As Integer = 1
            DataGridView2.Rows.Clear()
            sql = "SELECT * FROM expenditurestatement"
            cmd = New OleDbCommand(sql, connection1)

            DataGridView2.ColumnCount = 6
            With DataGridView2
                .RowHeadersVisible = False
                .Columns(0).HeaderCell.Value = "S.N"
                .Columns(1).HeaderCell.Value = "Transaction ID"
                .Columns(2).HeaderCell.Value = "Insert Date"
                .Columns(3).HeaderCell.Value = "Statement"
                .Columns(4).HeaderCell.Value = "Quantity"
                .Columns(5).HeaderCell.Value = "Amount"
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
            DataGridView2.Columns(0).FillWeight = 10
            DataGridView2.Columns(1).FillWeight = 15
            DataGridView2.Columns(2).FillWeight = 25
            DataGridView2.Columns(3).FillWeight = 35
            DataGridView2.Columns(4).FillWeight = 25
            DataGridView2.Columns(5).FillWeight = 25

            reader = cmd.ExecuteReader()
            If Not reader.HasRows Then
                DataGridView2.Rows().Clear()
            ElseIf reader.HasRows Then
                Do While reader.Read
                    Dim row() As String = {CStr(i), (reader.Item(0)), reader.Item(1), reader.Item(2), reader.Item(3), reader.Item(4)}
                    DataGridView2.Rows.Add(row)
                    i += 1
                Loop
                Dim total As String = 0
                For j As Integer = 0 To DataGridView2.RowCount - 1
                    total += Val(CDbl(DataGridView2.Rows(j).Cells(5).Value))
                Next
                Dim rows As String() = {"", "", "", "", "Total", total}
                DataGridView2.Rows.Add(rows)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub company_statement_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        company_statement_Load(e, e)
    End Sub

    Private Sub company_statement_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then Me.Close()
    End Sub

    Private Sub AddIncomeStatementToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AddIncomeStatementToolStripMenuItem.Click
        company_statement_income.ShowDialog()
    End Sub

    Private Sub AddExpenditureStatementToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AddExpenditureStatementToolStripMenuItem.Click
        company_statement_expenditure.ShowDialog()
    End Sub

    Private Sub ViewHistoryToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ViewHistoryToolStripMenuItem.Click
        company_statement_history.ShowDialog()
    End Sub

    Private Sub printfunction()
        Try
            Dim filepath As String
            filepath = selectidsql("sources", "source", "1")
            filepath = filepath + "\"

            Dim myregDate As Date = Date.Now()
            Dim mystrDate As String = myregDate.ToString("dd/MMM/yyyy")

            Dim bf As Font = FontFactory.GetFont("Arial Monospaced for SAP", 7)
            Dim fFont = New Font(bf)

            'get data from db'
            Dim companyName, companyAddress, companyPhone As String
            companyName = selectidsql("sources", "source", "10")
            companyAddress = selectidsql("sources", "source", "11")
            companyPhone = selectidsql("sources", "source", "12")
            'end of get data from db

            Dim mywidth() As Integer = {22, 22, 25, 40, 30, 30}

            Dim pdfTable As New PdfPTable(DataGridView1.ColumnCount)
            If DataGridView1.Rows.Count > 0 Then
                pdfTable.DefaultCell.Padding = 7
                pdfTable.WidthPercentage = 90
                pdfTable.HorizontalAlignment = Element.ALIGN_CENTER
                pdfTable.DefaultCell.BorderWidth = 1
                pdfTable.SetWidths(mywidth)

                'Adding Header row
                For Each column As DataGridViewColumn In DataGridView1.Columns
                    Dim cell As New PdfPCell(New Phrase(column.HeaderText))
                    cell.BackgroundColor = New iTextSharp.text.BaseColor(240, 240, 240)
                    pdfTable.AddCell(cell)
                Next

                'Adding DataRow
                For Each row As DataGridViewRow In DataGridView1.Rows
                    For Each cell As DataGridViewCell In row.Cells
                        pdfTable.AddCell(cell.Value.ToString())
                    Next
                Next
            End If

            Dim pdfTable1 As New PdfPTable(DataGridView2.ColumnCount)
            If DataGridView2.Rows.Count > 0 Then
                pdfTable1.DefaultCell.Padding = 7
                pdfTable1.WidthPercentage = 90
                pdfTable1.HorizontalAlignment = Element.ALIGN_CENTER
                pdfTable1.DefaultCell.BorderWidth = 1
                pdfTable1.SetWidths(mywidth)

                For Each column1 As DataGridViewColumn In DataGridView2.Columns
                    Dim cell1 As New PdfPCell(New Phrase(column1.HeaderText))
                    cell1.BackgroundColor = New iTextSharp.text.BaseColor(240, 240, 240)
                    pdfTable1.AddCell(cell1)
                Next

                For Each row1 As DataGridViewRow In DataGridView2.Rows
                    For Each cell1 As DataGridViewCell In row1.Cells
                        pdfTable1.AddCell(cell1.Value.ToString())
                    Next
                Next
            End If

            'Saving to PDF
            Dim folderPath As String = filepath
            If Not Directory.Exists(folderPath) Then
                Directory.CreateDirectory(folderPath)
            End If

            Dim regDate As Date = Date.Now()
            Dim strDate As String = regDate.ToString("ddMMMyyyy")
            Dim filenames As String = "company_statement_" + strDate + "_.pdf"

            Dim strsDate As String = regDate.ToString("dd/MMM/yyyy")
            Dim title As Paragraph = New Paragraph(companyName)
            Dim address As Paragraph = New Paragraph(companyAddress)
            Dim phone As Paragraph = New Paragraph(companyPhone)
            Dim currdate As Paragraph = New Paragraph("Date: " + strsDate)
            title.Alignment = Element.ALIGN_CENTER
            address.Alignment = Element.ALIGN_CENTER
            phone.Alignment = Element.ALIGN_CENTER
            currdate.Alignment = Element.ALIGN_RIGHT
            currdate.IndentationRight = 40

            Dim p As Paragraph = New Paragraph("Signature")
            p.Alignment = Element.ALIGN_RIGHT
            p.IndentationRight = 55
            Dim ps As Paragraph = New Paragraph("________________")
            ps.Alignment = Element.ALIGN_RIGHT
            ps.IndentationRight = 40
            Dim note As Paragraph = New Paragraph("Note: ________________________________________________________________________________________________________")
            Dim noteadd As Paragraph = New Paragraph("__________________________________")
            note.Alignment = Element.ALIGN_LEFT
            note.IndentationLeft = 40
            noteadd.Alignment = Element.ALIGN_LEFT
            noteadd.IndentationLeft = 70
            Dim statement As Paragraph
            If DataGridView1.Rows.Count > 0 Then
                statement = New Paragraph("Income Statement:")
            Else
                statement = New Paragraph("")
            End If
            statement.Alignment = Element.ALIGN_LEFT
            statement.IndentationLeft = 40
            Dim statement1 As Paragraph
            If DataGridView2.Rows.Count > 0 Then
                statement1 = New Paragraph("Expenditure Statement:")
            Else
                statement1 = New Paragraph("")
            End If
            statement1.Alignment = Element.ALIGN_LEFT
            statement1.IndentationLeft = 40


            Using Stream As New FileStream(folderPath & filenames, FileMode.Create)
                Dim pdfDoc As New Document(PageSize.A4.Rotate, 20.0F, 20.0F, 20.0F, 20.0F)
                PdfWriter.GetInstance(pdfDoc, Stream)
                pdfDoc.Open()
                pdfDoc.Add(New Paragraph(" "))
                pdfDoc.Add(New Paragraph(title))
                pdfDoc.Add(New Paragraph(address))
                pdfDoc.Add(New Paragraph(phone))
                pdfDoc.Add(New Paragraph(" "))
                pdfDoc.Add(New Paragraph(currdate))
                pdfDoc.Add(New Paragraph(statement))
                pdfDoc.Add(New Paragraph(" "))
                pdfDoc.Add(pdfTable)
                pdfDoc.Add(New Paragraph(" "))
                pdfDoc.Add(New Paragraph(" "))
                pdfDoc.Add(New Paragraph(statement1))
                pdfDoc.Add(New Paragraph(" "))
                pdfDoc.Add(pdfTable1)
                pdfDoc.Add(New Paragraph(" "))
                pdfDoc.Add(New Paragraph(note))
                pdfDoc.Add(New Paragraph(" "))
                pdfDoc.Add(New Paragraph(noteadd))
                pdfDoc.Add(New Paragraph(" "))
                pdfDoc.Add(New Paragraph(ps))
                pdfDoc.Add(New Paragraph(p))
                pdfDoc.Close()
                Stream.Close()
            End Using

            sql = "INSERT INTO  incomestatementhist(insertdate,statement,quantity, amount) SELECT insertdate,statement,quantity,amount FROM incomestatement"
            cmd = New OleDbCommand(sql, connection1)
            cmd.ExecuteNonQuery()
            Dim sql1, sql2 As String
            sql1 = "INSERT INTO  expenditurestatementhist(insertdate,statement,quantity, amount) SELECT insertdate,statement,quantity,amount FROM expenditurestatement"
            cmd1 = New OleDbCommand(sql1, connection1)
            cmd1.ExecuteNonQuery()

            sql = "DELETE from incomestatement"
            cmd = New OleDbCommand(sql, connection1)
            cmd.ExecuteNonQuery()
            sql2 = "DELETE from expenditurestatement"
            cmd2 = New OleDbCommand(sql2, connection1)
            cmd2.ExecuteNonQuery()

            MsgBox("print data successfully.", MsgBoxStyle.Information, Title:="Successful")
        Catch ex As Exception
            'MsgBox(ex.ToString())
            MsgBox("Your pdf file is in use or your print source location is not valid.", MsgBoxStyle.Exclamation, Title:="Try again.")
        End Try
    End Sub

    Private Sub PrintAllToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PrintAllToolStripMenuItem.Click
        If Not DataGridView1.Rows.Count > 0 Then
            Dim results As Integer = MessageBox.Show("Income statement is empty" & Environment.NewLine & " You want to continue to print ? ", "Are you sure ?", MessageBoxButtons.YesNo)
            If results = DialogResult.No Then
                'nothing to do
            ElseIf results = DialogResult.Yes Then
                printfunction()
                company_statement_Load(e, e)
            End If
        ElseIf Not DataGridView2.Rows.Count > 0 Then
            Dim results As Integer = MessageBox.Show("Expenditure statement is empty" & Environment.NewLine & " You want to continue to print ? ", "Are you sure ?", MessageBoxButtons.YesNo)
            If results = DialogResult.No Then
                'nothing to do
            ElseIf results = DialogResult.Yes Then
                printfunction()
                company_statement_Load(e, e)
            End If
        Else
            printfunction()
            company_statement_Load(e, e)
        End If
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

    Private Sub DeleteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DeleteToolStripMenuItem.Click
        Try
            'MsgBox(DataGridView1.Rows(index).Cells(1).Value)
            sql = "DELETE FROM incomestatement WHERE ID=" & DataGridView1.Rows(index).Cells(1).Value & ""
            cmd = New OleDbCommand(sql, connection1)
            If cmd.ExecuteNonQuery = 1 Then
                printmessage("Successfully deleted.", "Info", "Success")
                company_statement_Load(e, e)
            End If
        Catch ex As Exception
            ' MsgBox(ex.ToString())
        End Try
    End Sub

    Private Sub EditToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EditToolStripMenuItem.Click
        Try
            'MsgBox("Show edit box")
            company_statement_edit.TextBox1.Text = DataGridView1.Rows(index).Cells(1).Value
            company_statement_edit.textbox4.Text = DataGridView1.Rows(index).Cells(3).Value
            company_statement_edit.DateTimePicker1.Value = DataGridView1.Rows(index).Cells(2).Value
            company_statement_edit.TextBox2.Text = DataGridView1.Rows(index).Cells(5).Value
            company_statement_edit.TextBox3.Text = DataGridView1.Rows(index).Cells(4).Value
            company_statement_edit.ShowDialog()
        Catch ex As Exception
            'MsgBox(ex.ToString())
        End Try
    End Sub

    Private Sub DataGridView2_CellMouseUp(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataGridView2.CellMouseUp
        Try
            If e.Button = Windows.Forms.MouseButtons.Right Then
                DataGridView1.Rows(e.RowIndex).Selected = True
                index = e.RowIndex
                DataGridView1.CurrentCell = DataGridView1.Rows(e.RowIndex).Cells(1)
                ContextMenuStrip2.Show(DataGridView1, e.Location)
                ContextMenuStrip2.Show(Cursor.Position)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem2.Click
        Try
            'MsgBox(DataGridView1.Rows(index).Cells(1).Value)
            sql = "DELETE FROM expenditurestatement WHERE ID=" & DataGridView2.Rows(index).Cells(1).Value & ""
            cmd = New OleDbCommand(sql, connection1)
            If cmd.ExecuteNonQuery = 1 Then
                printmessage("Successfully deleted.", "Info", "Success")
                company_statement_Load(e, e)
            End If
        Catch ex As Exception
            ' MsgBox(ex.ToString())
        End Try
    End Sub

    Private Sub ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem1.Click
        Try
            'MsgBox("Show edit box")
            company_statement_edit_exp.TextBox1.Text = DataGridView2.Rows(index).Cells(1).Value
            company_statement_edit_exp.TextBox4.Text = DataGridView2.Rows(index).Cells(3).Value
            company_statement_edit_exp.DateTimePicker1.Value = DataGridView2.Rows(index).Cells(2).Value
            company_statement_edit_exp.TextBox2.Text = DataGridView2.Rows(index).Cells(5).Value
            company_statement_edit_exp.TextBox3.Text = DataGridView2.Rows(index).Cells(4).Value
            company_statement_edit_exp.ShowDialog()
        Catch ex As Exception
            ' MsgBox(ex.ToString())
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        company_statement_sum.ShowDialog()
    End Sub
End Class