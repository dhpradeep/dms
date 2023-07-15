Imports System.Data.OleDb
Imports iTextSharp.text.pdf
Imports iTextSharp.text
Imports System.IO
Public Class emi_calculator_stat
    Private Sub emi_calculator_stat_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'txtamnt.Text = "10000"
        'txtduration.Text = "6"
        'txtrate.Text = "17"

        Try

            SaveFileDialog1.Filter = "PDF(*.pdf)|*.pdf|All file(*.*)|*.*"
        SaveFileDialog1.Title = "Save PDF File"

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

        Dim rateofinterest As Integer = txtrate.Text '17 in here
        Dim loanamount As Integer = txtamnt.Text '10k in here
        Dim timeduration As Integer = txtduration.Text '6 in here
        'LoanIRate = 0.01 * txtrate.Text / 12
        Dim ratepermonth As Single = 0.01 * txtrate.Text / 12
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


        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        emi_calculator_stat_Load(e, e)
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim myfiletosave As New SaveFileDialog
        If SaveFileDialog1.ShowDialog() = DialogResult.OK Then
            If SaveFileDialog1.FileName <> "" Then
                Dim mypath As String = SaveFileDialog1.FileName
                Try
                    Dim myregDate As Date = Date.Now()
                    Dim mystrDate As String = myregDate.ToString("dd/MMM/yyyy")

                    Dim bf As Font = FontFactory.GetFont("Arial Monospaced for SAP", 7)
                    Dim fFont = New Font(bf)
                    'get data from db'
                    Dim companyName, companyAddress, companyPhone As String
                    companyName = selectidsql("sources", "source", "10")
                    companyAddress = selectidsql("sources", "source", "11")
                    companyPhone = selectidsql("sources", "source", "12")

                    'Dim mywidth() As Integer = {22, 22, 40}

                    Dim pdfTable As New PdfPTable(DataGridView1.ColumnCount)
                    pdfTable.DefaultCell.Padding = 7
                    pdfTable.WidthPercentage = 90
                    pdfTable.HorizontalAlignment = Element.ALIGN_CENTER
                    pdfTable.DefaultCell.BorderWidth = 1
                    'pdfTable.SetWidths(mywidth)

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

    Private Sub emi_calculator_stat_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then Me.Close()
    End Sub
End Class