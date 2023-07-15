Imports System.Data.OleDb
Imports iTextSharp.text.pdf
Imports iTextSharp.text
Imports System.IO
Public Class viewprintdata
    Private Sub viewprintdata_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DateTimePicker1.Value = DateTime.Now
        DateTimePicker1.Format = DateTimePickerFormat.Custom
        DateTimePicker1.CustomFormat = "dd/MM/yyyy"

        SaveFileDialog1.Filter = "PDF(*.pdf)|*.pdf|All file(*.*)|*.*"
        SaveFileDialog1.Title = "Save PDF File"

        Try
            'to start connection of ms-access
            connection = New OleDbConnection
            With connection
                If .State = ConnectionState.Closed Then
                    .ConnectionString = strCon
                    .Open()
                End If
            End With
            Button2_Click(e, e)
        Catch ex As Exception
            MsgBox("Error occurs. (Error 300)", MsgBoxStyle.Critical, Title:="Try again")
        End Try

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try
            DataGridView1.DataSource = Nothing
            DataGridView1.Rows.Clear()

            Dim mytime As String = Format(DateTimePicker1.Value, "dd/MMM/yyyy")
            sql = "SELECT * FROM myhistory where Entry_Date='" & mytime & "'"
            cmd = New OleDbCommand(sql, connection)
            reader = cmd.ExecuteReader()
            DataGridView1.ColumnCount = 13
            If reader.HasRows Then
                Do While reader.Read
                    Dim row() As String = {(reader.Item(0)), (reader.Item(1)), (reader.Item(2)), (reader.Item(3)) _
                        , (reader.Item(4)), (reader.Item(5)), (reader.Item(6)), (reader.Item(7)), (reader.Item(8)) _
                        , (reader.Item(9)), (reader.Item(10)), (reader.Item(11)), (reader.Item(12))}
                    DataGridView1.Rows.Add(row)
                Loop
            End If

            Dim total_milk, total_fat, total_fatkg, total_fatrs, total_snf, total_snfkg As Double
            Dim total_snfrs, total_ts, total_addedrs, total_amounts As Double
            total_milk = 0
            total_fat = 0
            total_fatkg = 0
            total_fatrs = 0
            total_snf = 0
            total_snfkg = 0
            total_snfrs = 0
            total_ts = 0
            total_addedrs = 0
            total_amounts = 0

            For j As Integer = 0 To DataGridView1.Rows.Count - 1
                total_milk += Val(CDbl(DataGridView1.Rows(j).Cells(3).Value))
                total_fat += Val(CDbl(DataGridView1.Rows(j).Cells(4).Value))
                total_fatkg += Val(CDbl(DataGridView1.Rows(j).Cells(5).Value))
                total_fatrs += Val(CDbl(DataGridView1.Rows(j).Cells(6).Value))
                total_snf += Val(CDbl(DataGridView1.Rows(j).Cells(7).Value))
                total_snfkg += Val(CDbl(DataGridView1.Rows(j).Cells(8).Value))
                total_snfrs += Val(CDbl(DataGridView1.Rows(j).Cells(9).Value))
                total_ts += Val(CDbl(DataGridView1.Rows(j).Cells(10).Value))
                total_addedrs += Val(CDbl(DataGridView1.Rows(j).Cells(11).Value))
                total_amounts += Val(CDbl(DataGridView1.Rows(j).Cells(12).Value))
            Next
            total_fat = CDbl(total_fat) / CDbl(DataGridView1.Rows.Count)
            total_fat = total_fat.ToString("N2")



            Dim rows1 As String() = {"", "", "TOTAL", total_milk, total_fat, total_fatkg, total_fatrs, total_snf, total_snfkg, total_snfrs, total_ts, total_addedrs, total_amounts, ""}
            DataGridView1.Rows.Add(rows1)




            With DataGridView1
                .RowHeadersVisible = False
                .Columns(0).HeaderCell.Value = "ID"
                .Columns(1).HeaderCell.Value = "Date"
                .Columns(2).HeaderCell.Value = "Name"
                .Columns(3).HeaderCell.Value = "Milk"
                .Columns(4).HeaderCell.Value = "Fat"
                .Columns(5).HeaderCell.Value = "Fat/kg"
                .Columns(6).HeaderCell.Value = "Fat/rs"
                .Columns(7).HeaderCell.Value = "Snf"
                .Columns(8).HeaderCell.Value = "Snf/kg"
                .Columns(9).HeaderCell.Value = "Snf/rs"
                .Columns(10).HeaderCell.Value = "TS"
                .Columns(11).HeaderCell.Value = "Bonus"
                .Columns(12).HeaderCell.Value = "Total Amount(Rs.)"
                .Columns(0).SortMode = DataGridViewColumnSortMode.NotSortable
                .Columns(1).SortMode = DataGridViewColumnSortMode.NotSortable
                .Columns(2).SortMode = DataGridViewColumnSortMode.NotSortable
                .Columns(3).SortMode = DataGridViewColumnSortMode.NotSortable
                .Columns(4).SortMode = DataGridViewColumnSortMode.NotSortable
                .Columns(5).SortMode = DataGridViewColumnSortMode.NotSortable
                .Columns(6).SortMode = DataGridViewColumnSortMode.NotSortable
                .Columns(7).SortMode = DataGridViewColumnSortMode.NotSortable
                .Columns(8).SortMode = DataGridViewColumnSortMode.NotSortable
                .Columns(9).SortMode = DataGridViewColumnSortMode.NotSortable
                .Columns(10).SortMode = DataGridViewColumnSortMode.NotSortable
                .Columns(11).SortMode = DataGridViewColumnSortMode.NotSortable
                .Columns(12).SortMode = DataGridViewColumnSortMode.NotSortable
                .AllowUserToAddRows = False
                .AllowUserToResizeRows = False
                .AllowUserToOrderColumns = False
                .AllowUserToResizeColumns = True
            End With

        Catch ex As Exception
            'MsgBox("No data found on this date.", MsgBoxStyle.Exclamation, Title:="Try again")
            MsgBox(ex.ToString())
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim myfiletosave As New SaveFileDialog
        If SaveFileDialog1.ShowDialog() = DialogResult.OK Then
            If SaveFileDialog1.FileName <> "" Then
                Dim mypath As String = SaveFileDialog1.FileName
                Try
                    Dim myregDate As Date = Date.Now()
                    Dim mystrDate As String = myregDate.ToString("dd/MMM/yyyy")

                    Dim bf As Font = FontFactory.GetFont("Arial Monospaced for SAP", 7)
                    Dim fFont = New Font(bf)

                    Dim companyName, companyAddress, companyPhone As String

                    companyName = selectidsql("sources", "source", "10")
                    companyAddress = selectidsql("sources", "source", "11")
                    companyPhone = selectidsql("sources", "source", "12")

                    Dim mywidth() As Integer = {15, 35, 45, 22, 22, 22, 22, 22, 22, 22, 15, 15, 31}

                    Dim pdfTable As New PdfPTable(DataGridView1.ColumnCount)
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


                    Dim regDate As Date = Date.Now()
                    Dim strDate As String = regDate.ToString("ddMMMyyyy")
                    'Dim filenames As String = strDate + "hist.pdf"


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


                    Using Stream As New FileStream(mypath, FileMode.Create)
                        Dim pdfDoc As New Document(PageSize.A4.Rotate, 20.0F, 20.0F, 20.0F, 20.0F)
                        PdfWriter.GetInstance(pdfDoc, Stream)
                        pdfDoc.Open()
                        pdfDoc.Add(New Paragraph(" "))
                        pdfDoc.Add(New Paragraph(title))
                        pdfDoc.Add(New Paragraph(address))
                        pdfDoc.Add(New Paragraph(phone))
                        pdfDoc.Add(New Paragraph(" "))
                        pdfDoc.Add(New Paragraph(" "))
                        pdfDoc.Add(New Paragraph(currdate))
                        pdfDoc.Add(New Paragraph(" "))
                        pdfDoc.Add(pdfTable)
                        pdfDoc.Add(New Paragraph(" "))
                        pdfDoc.Add(New Paragraph(note))
                        pdfDoc.Add(New Paragraph(" "))
                        pdfDoc.Add(New Paragraph(noteadd))
                        pdfDoc.Add(New Paragraph(" "))
                        'pdfDoc.Add(New Paragraph("Thank you."))
                        'pdfDoc.Add(New Paragraph("Ghachowk dairy Corporation."))
                        pdfDoc.Add(New Paragraph(ps))
                        pdfDoc.Add(New Paragraph(p))
                        pdfDoc.Close()
                        Stream.Close()
                    End Using

                    MsgBox("print data successfully.", MsgBoxStyle.Information, Title:="Successful")

                Catch ex As Exception
                    'MsgBox(ex.ToString())
                    MsgBox("Your pdf file is in use or your print source location is not valid.", MsgBoxStyle.Exclamation, Title:="Try again.")
                End Try
            Else
                MsgBox("Please Type a filename", MsgBoxStyle.Critical, Title:="Try again")
                SaveFileDialog1.Dispose()
            End If
        Else

        End If
    End Sub

    Private Sub viewprintdata_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        DataGridView1.DataSource = Nothing
    End Sub

    Private Sub viewprintdata_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then Me.Close()
    End Sub
End Class