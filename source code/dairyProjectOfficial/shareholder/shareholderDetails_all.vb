Imports System.Data.OleDb
Imports iTextSharp.text.pdf
Imports iTextSharp.text
Imports System.IO
Public Class shareholderDetails_all
    Private Sub shareholderDetails_all_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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
                .Columns(1).HeaderCell.Value = "Farmer ID"
                .Columns(2).HeaderCell.Value = "Farmer Name"
                .Columns(3).HeaderCell.Value = "Previous Amount"
                .Columns(4).HeaderCell.Value = "Interest"
                .Columns(5).HeaderCell.Value = "Saving Amount"
                .Columns(6).HeaderCell.Value = "Interest"
                .Columns(7).HeaderCell.Value = "Share Bonus"
                .Columns(8).HeaderCell.Value = "Total Amount"
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
                .Columns(0).FillWeight = 10
                .Columns(1).FillWeight = 10
                .Columns(2).FillWeight = 25
                .Columns(3).FillWeight = 15
                .Columns(4).FillWeight = 15
                .Columns(5).FillWeight = 15
                .Columns(6).FillWeight = 15
                .Columns(7).FillWeight = 15
                .Columns(8).FillWeight = 15
            End With

            sql = "SELECT farmer_ID,mName,amount,interest,shareholder_amount,s_interest FROM previousbalance"
            cmd = New OleDbCommand(sql, connection1)
            reader = cmd.ExecuteReader()
            'end of ID and Name

            Dim sharebonus As String
            sharebonus = selectidsql("sources", "source", "18")
            'sharebonus handle
            Dim actual_bonus As String


            Dim total_amount As Integer
            If Not reader.HasRows Then
                DataGridView1.Rows().Clear()
            ElseIf reader.HasRows Then
                Do While reader.Read

                    Dim new_bonus As Double = 0.0
                    Try
                        actual_bonus = selectidsql1("shareamount", "amount", reader.Item(0), "farmer_ID")
                        new_bonus = (CDbl(actual_bonus) * CDbl(sharebonus)) / 100
                    Catch ex As Exception
                    End Try

                    total_amount = CDbl(reader.Item(2)) + CDbl(reader.Item(3)) + CDbl(reader.Item(4)) _
                        + CDbl(reader.Item(5)) + CDbl(new_bonus)

                    Dim row() As String = {CStr(i), reader.Item(0), reader.Item(1),
                        reader.Item(2), reader.Item(3), reader.Item(4),
                        reader.Item(5), new_bonus, total_amount}
                    DataGridView1.Rows.Add(row)
                    i += 1
                Loop
                Dim previous_amt, interest, saving_amount, interest1, share_bonus, total_amounts As Double
                previous_amt = 0
                interest = 0
                saving_amount = 0
                interest1 = 0
                share_bonus = 0
                total_amounts = 0

                For j As Integer = 0 To DataGridView1.Rows.Count - 1
                    previous_amt += Val(CDbl(DataGridView1.Rows(j).Cells(3).Value))
                    interest += Val(CDbl(DataGridView1.Rows(j).Cells(4).Value))
                    saving_amount += Val(CDbl(DataGridView1.Rows(j).Cells(5).Value))
                    interest1 += Val(CDbl(DataGridView1.Rows(j).Cells(6).Value))
                    share_bonus += Val(CDbl(DataGridView1.Rows(j).Cells(7).Value))
                    total_amounts += Val(CDbl(DataGridView1.Rows(j).Cells(8).Value))
                Next

                Dim row1() As String = {"", "", "TOTAL", previous_amt, interest, saving_amount, interest1, share_bonus, total_amounts}
                DataGridView1.Rows.Add(row1)
            End If

        Catch ex As Exception
            ' MsgBox(ex.ToString())
        End Try
    End Sub

    Private Sub shareholderDetails_all_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then Me.Close()
    End Sub

    Private Sub milkStore_Click(sender As Object, e As EventArgs) Handles milkStore.Click
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

            Dim mywidth() As Integer = {10, 10, 30, 20, 15, 20, 15, 20, 25}

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

            'Saving to PDF
            Dim folderPath As String = filepath
            If Not Directory.Exists(folderPath) Then
                Directory.CreateDirectory(folderPath)
            End If

            Dim regDate As Date = Date.Now()
            Dim strDate As String = regDate.ToString("ddMMMyyyy")
            Dim filenames As String = "company_yearly_statement_" + strDate + "_.pdf"

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
                statement = New Paragraph("Yearly Statement:")
            Else
                statement = New Paragraph("")
            End If
            statement.Alignment = Element.ALIGN_LEFT


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
                pdfDoc.Add(New Paragraph(note))
                pdfDoc.Add(New Paragraph(" "))
                pdfDoc.Add(New Paragraph(noteadd))
                pdfDoc.Add(New Paragraph(" "))
                pdfDoc.Add(New Paragraph(ps))
                pdfDoc.Add(New Paragraph(p))
                pdfDoc.Close()
                Stream.Close()
            End Using

            MsgBox("print data successfully.", MsgBoxStyle.Information, Title:="Successful")
            shareholderDetails_all_Load(e, e)

        Catch ex As Exception
            'MsgBox(ex.ToString())
            MsgBox("Your pdf file is in use or your print source location is not valid.", MsgBoxStyle.Exclamation, Title:="Try again.")
        End Try
    End Sub
End Class