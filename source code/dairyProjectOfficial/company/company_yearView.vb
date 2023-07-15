Imports System.Data.OleDb
Imports iTextSharp.text.pdf
Imports iTextSharp.text
Imports System.IO
Public Class company_yearView
    Private Sub company_yearView_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DataGridView2.Visible = False
        DataGridView1.Rows.Clear()
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

            FILL_DGVOrder2()

            FILL_DVGOrder()
        Catch ex As Exception

        End Try

    End Sub

    Private Sub FILL_DVGOrder()
        Try
            For i As Integer = 0 To DataGridView2.Rows.Count - 1
                'query for update bonus and ts'
                'to find a name:
                Dim sql3 As String
                sql3 = "SELECT mName FROM farmers WHERE ID=" & DataGridView2.Rows(i).Cells(0).Value & ""
                adapter = New OleDbDataAdapter(sql3, connection)
                Dim dtname As New DataTable
                adapter.Fill(dtname)
                Dim name, myid As String
                name = dtname.Rows(0).Item(0)
                myid = DataGridView2.Rows(i).Cells(0).Value

                'query loop

                Dim jan_amount, feb_amount, mar_amount, apr_amount, may_amount, jun_amount, july_amount, aug_amount, sep_amount, oct_amount, nov_amount, dec_amount As String

                Try
                    jan_amount = getAllMonthData(DataGridView2, i, "Jan")
                Catch ex As Exception
                    jan_amount = 0
                End Try
                Try
                    feb_amount = getAllMonthData(DataGridView2, i, "Feb")
                Catch ex As Exception
                    feb_amount = 0
                End Try
                Try
                    mar_amount = getAllMonthData(DataGridView2, i, "Mar")
                Catch ex As Exception
                    mar_amount = 0
                End Try
                Try
                    apr_amount = getAllMonthData(DataGridView2, i, "Apr")
                Catch ex As Exception
                    apr_amount = 0
                End Try
                Try
                    may_amount = getAllMonthData(DataGridView2, i, "May")
                Catch ex As Exception
                    may_amount = 0
                End Try
                Try
                    jun_amount = getAllMonthData(DataGridView2, i, "Jun")
                Catch ex As Exception
                    jun_amount = 0
                End Try
                Try
                    july_amount = getAllMonthData(DataGridView2, i, "Jul")
                Catch ex As Exception
                    july_amount = 0
                End Try
                Try
                    aug_amount = getAllMonthData(DataGridView2, i, "Aug")
                Catch ex As Exception
                    aug_amount = 0
                End Try
                Try
                    sep_amount = getAllMonthData(DataGridView2, i, "Sep")
                Catch ex As Exception
                    sep_amount = 0
                End Try
                Try
                    oct_amount = getAllMonthData(DataGridView2, i, "Oct")
                Catch ex As Exception
                    oct_amount = 0
                End Try
                Try
                    nov_amount = getAllMonthData(DataGridView2, i, "Nov")
                Catch ex As Exception
                    nov_amount = 0
                End Try
                Try
                    dec_amount = getAllMonthData(DataGridView2, i, "Dec")
                Catch ex As Exception
                    dec_amount = 0
                End Try

                Dim total_amount As Double
                total_amount = CDbl(jan_amount) + CDbl(feb_amount) + CDbl(mar_amount) + CDbl(apr_amount) + CDbl(may_amount) + CDbl(jun_amount) + CDbl(july_amount) + CDbl(aug_amount) + CDbl(sep_amount) + CDbl(oct_amount) + CDbl(nov_amount) + CDbl(dec_amount)

                With DataGridView1
                    .RowHeadersVisible = False
                    .AllowUserToAddRows = False
                    .AllowUserToResizeRows = False
                    .AllowUserToOrderColumns = False
                    .AllowUserToResizeColumns = False
                End With
                DataGridView1.ColumnCount = 15
                Dim row As String() = New String() {myid, name, jan_amount, feb_amount, mar_amount, apr_amount, may_amount, jun_amount, july_amount, aug_amount, sep_amount, oct_amount, nov_amount, dec_amount, total_amount}

                DataGridView1.Rows().Add(row)
            Next
            Dim jan, feb, mar, apr, may, jun, july, aug, sep, oct, nov, dec, tot As Double
            jan = 0
            feb = 0
            mar = 0
            apr = 0
            may = 0
            jun = 0
            july = 0
            aug = 0
            sep = 0
            oct = 0
            nov = 0
            dec = 0
            tot = 0

            For j As Integer = 0 To DataGridView1.Rows.Count - 1
                jan += Val(CDbl(DataGridView1.Rows(j).Cells(2).Value))
                feb += Val(CDbl(DataGridView1.Rows(j).Cells(3).Value))
                mar += Val(CDbl(DataGridView1.Rows(j).Cells(4).Value))
                apr += Val(CDbl(DataGridView1.Rows(j).Cells(5).Value))
                may += Val(CDbl(DataGridView1.Rows(j).Cells(6).Value))
                jun += Val(CDbl(DataGridView1.Rows(j).Cells(7).Value))
                july += Val(CDbl(DataGridView1.Rows(j).Cells(8).Value))
                aug += Val(CDbl(DataGridView1.Rows(j).Cells(9).Value))
                sep += Val(CDbl(DataGridView1.Rows(j).Cells(10).Value))
                oct += Val(CDbl(DataGridView1.Rows(j).Cells(11).Value))
                nov += Val(CDbl(DataGridView1.Rows(j).Cells(12).Value))
                dec += Val(CDbl(DataGridView1.Rows(j).Cells(13).Value))
                tot += Val(CDbl(DataGridView1.Rows(j).Cells(14).Value))
            Next

            Dim row1() As String = {"", "TOTAL", jan, feb, mar, apr, may, jun, july, aug, sep, oct, nov, dec, tot}
            DataGridView1.Rows.Add(row1)

            With DataGridView1
                .Columns(0).HeaderCell.Value = "ID"
                .Columns(1).HeaderCell.Value = "Name"
                .Columns(2).HeaderCell.Value = "Jan"
                .Columns(3).HeaderCell.Value = "Feb"
                .Columns(4).HeaderCell.Value = "Mar"
                .Columns(5).HeaderCell.Value = "Apr"
                .Columns(6).HeaderCell.Value = "May"
                .Columns(7).HeaderCell.Value = "June"
                .Columns(8).HeaderCell.Value = "July"
                .Columns(9).HeaderCell.Value = "Aug"
                .Columns(10).HeaderCell.Value = "Sep"
                .Columns(11).HeaderCell.Value = "Oct"
                .Columns(12).HeaderCell.Value = "Nov"
                .Columns(13).HeaderCell.Value = "Dec"
                .Columns(14).HeaderCell.Value = "Total"
            End With
            DataGridView1.Columns(0).FillWeight = 10
            DataGridView1.Columns(1).FillWeight = 25
            DataGridView1.Columns(2).FillWeight = 15
            DataGridView1.Columns(3).FillWeight = 15
            DataGridView1.Columns(4).FillWeight = 15
            DataGridView1.Columns(5).FillWeight = 15
            DataGridView1.Columns(6).FillWeight = 15
            DataGridView1.Columns(7).FillWeight = 15
            DataGridView1.Columns(8).FillWeight = 15
            DataGridView1.Columns(9).FillWeight = 15
            DataGridView1.Columns(10).FillWeight = 15
            DataGridView1.Columns(11).FillWeight = 15
            DataGridView1.Columns(12).FillWeight = 15
            DataGridView1.Columns(13).FillWeight = 15
            DataGridView1.Columns(14).FillWeight = 20

        Catch ex As Exception
            'MsgBox(ex.ToString())
            printmessage("No data found.", "Error", "Try again")
            Me.Close()
        End Try
    End Sub

    Private Sub FILL_DVGOrderMilk()
        Try
            For i As Integer = 0 To DataGridView2.Rows.Count - 1
                'query for update bonus and ts'
                'to find a name:
                Dim sql3 As String
                sql3 = "SELECT mName FROM farmers WHERE ID=" & DataGridView2.Rows(i).Cells(0).Value & ""
                adapter = New OleDbDataAdapter(sql3, connection)
                Dim dtname As New DataTable
                adapter.Fill(dtname)
                Dim name, myid As String
                name = dtname.Rows(0).Item(0)
                myid = DataGridView2.Rows(i).Cells(0).Value

                'query loop

                Dim jan_amount, feb_amount, mar_amount, apr_amount, may_amount, jun_amount, july_amount, aug_amount, sep_amount, oct_amount, nov_amount, dec_amount As String

                Try
                    jan_amount = getAllMonthMilkData(DataGridView2, i, "Jan")
                Catch ex As Exception
                    jan_amount = 0
                End Try
                Try
                    feb_amount = getAllMonthMilkData(DataGridView2, i, "Feb")
                Catch ex As Exception
                    feb_amount = 0
                End Try
                Try
                    mar_amount = getAllMonthMilkData(DataGridView2, i, "Mar")
                Catch ex As Exception
                    mar_amount = 0
                End Try
                Try
                    apr_amount = getAllMonthMilkData(DataGridView2, i, "Apr")
                Catch ex As Exception
                    apr_amount = 0
                End Try
                Try
                    may_amount = getAllMonthMilkData(DataGridView2, i, "May")
                Catch ex As Exception
                    may_amount = 0
                End Try
                Try
                    jun_amount = getAllMonthMilkData(DataGridView2, i, "Jun")
                Catch ex As Exception
                    jun_amount = 0
                End Try
                Try
                    july_amount = getAllMonthMilkData(DataGridView2, i, "Jul")
                Catch ex As Exception
                    july_amount = 0
                End Try
                Try
                    aug_amount = getAllMonthMilkData(DataGridView2, i, "Aug")
                Catch ex As Exception
                    aug_amount = 0
                End Try
                Try
                    sep_amount = getAllMonthMilkData(DataGridView2, i, "Sep")
                Catch ex As Exception
                    sep_amount = 0
                End Try
                Try
                    oct_amount = getAllMonthMilkData(DataGridView2, i, "Oct")
                Catch ex As Exception
                    oct_amount = 0
                End Try
                Try
                    nov_amount = getAllMonthMilkData(DataGridView2, i, "Nov")
                Catch ex As Exception
                    nov_amount = 0
                End Try
                Try
                    dec_amount = getAllMonthMilkData(DataGridView2, i, "Dec")
                Catch ex As Exception
                    dec_amount = 0
                End Try

                Dim total_amount As Double
                total_amount = CDbl(jan_amount) + CDbl(feb_amount) + CDbl(mar_amount) + CDbl(apr_amount) + CDbl(may_amount) + CDbl(jun_amount) + CDbl(july_amount) + CDbl(aug_amount) + CDbl(sep_amount) + CDbl(oct_amount) + CDbl(nov_amount) + CDbl(dec_amount)

                With DataGridView1
                    .RowHeadersVisible = False
                    .AllowUserToAddRows = False
                    .AllowUserToResizeRows = False
                    .AllowUserToOrderColumns = False
                    .AllowUserToResizeColumns = False
                End With
                DataGridView1.ColumnCount = 15
                Dim row As String() = New String() {myid, name, jan_amount, feb_amount, mar_amount, apr_amount, may_amount, jun_amount, july_amount, aug_amount, sep_amount, oct_amount, nov_amount, dec_amount, total_amount}

                DataGridView1.Rows().Add(row)

            Next
            Dim jan, feb, mar, apr, may, jun, july, aug, sep, oct, nov, dec, tot As Double
            jan = 0
            feb = 0
            mar = 0
            apr = 0
            may = 0
            jun = 0
            july = 0
            aug = 0
            sep = 0
            oct = 0
            nov = 0
            dec = 0
            tot = 0

            For j As Integer = 0 To DataGridView1.Rows.Count - 1
                jan += Val(CDbl(DataGridView1.Rows(j).Cells(2).Value))
                feb += Val(CDbl(DataGridView1.Rows(j).Cells(3).Value))
                mar += Val(CDbl(DataGridView1.Rows(j).Cells(4).Value))
                apr += Val(CDbl(DataGridView1.Rows(j).Cells(5).Value))
                may += Val(CDbl(DataGridView1.Rows(j).Cells(6).Value))
                jun += Val(CDbl(DataGridView1.Rows(j).Cells(7).Value))
                july += Val(CDbl(DataGridView1.Rows(j).Cells(8).Value))
                aug += Val(CDbl(DataGridView1.Rows(j).Cells(9).Value))
                sep += Val(CDbl(DataGridView1.Rows(j).Cells(10).Value))
                oct += Val(CDbl(DataGridView1.Rows(j).Cells(11).Value))
                nov += Val(CDbl(DataGridView1.Rows(j).Cells(12).Value))
                dec += Val(CDbl(DataGridView1.Rows(j).Cells(13).Value))
                tot += Val(CDbl(DataGridView1.Rows(j).Cells(14).Value))
            Next

            Dim row1() As String = {"", "TOTAL", jan, feb, mar, apr, may, jun, july, aug, sep, oct, nov, dec, tot}
            DataGridView1.Rows.Add(row1)
            With DataGridView1
                .Columns(0).HeaderCell.Value = "ID"
                .Columns(1).HeaderCell.Value = "Name"
                .Columns(2).HeaderCell.Value = "Jan"
                .Columns(3).HeaderCell.Value = "Feb"
                .Columns(4).HeaderCell.Value = "Mar"
                .Columns(5).HeaderCell.Value = "Apr"
                .Columns(6).HeaderCell.Value = "May"
                .Columns(7).HeaderCell.Value = "June"
                .Columns(8).HeaderCell.Value = "July"
                .Columns(9).HeaderCell.Value = "Aug"
                .Columns(10).HeaderCell.Value = "Sep"
                .Columns(11).HeaderCell.Value = "Oct"
                .Columns(12).HeaderCell.Value = "Nov"
                .Columns(13).HeaderCell.Value = "Dec"
                .Columns(14).HeaderCell.Value = "Total"
            End With
            DataGridView1.Columns(0).FillWeight = 10
            DataGridView1.Columns(1).FillWeight = 25
            DataGridView1.Columns(2).FillWeight = 15
            DataGridView1.Columns(3).FillWeight = 15
            DataGridView1.Columns(4).FillWeight = 15
            DataGridView1.Columns(5).FillWeight = 15
            DataGridView1.Columns(6).FillWeight = 15
            DataGridView1.Columns(7).FillWeight = 15
            DataGridView1.Columns(8).FillWeight = 15
            DataGridView1.Columns(9).FillWeight = 15
            DataGridView1.Columns(10).FillWeight = 15
            DataGridView1.Columns(11).FillWeight = 15
            DataGridView1.Columns(12).FillWeight = 15
            DataGridView1.Columns(13).FillWeight = 15
            DataGridView1.Columns(14).FillWeight = 20

        Catch ex As Exception
            'MsgBox(ex.ToString())
            printmessage("No data found.", "Error", "Try again")
            Me.Close()
        End Try
    End Sub

    Private Sub FILL_DGVOrder2()
        Try
            DataGridView2.Rows.Clear()

            DataGridView2.ColumnCount = 1
            With DataGridView2
                .RowHeadersVisible = False
                .Columns(0).HeaderCell.Value = "farmer_ID"
                .Columns(0).SortMode = DataGridViewColumnSortMode.NotSortable
                .AllowUserToAddRows = False
                .AllowUserToResizeRows = False
                .AllowUserToOrderColumns = False
                .AllowUserToResizeColumns = True
            End With

            'select data in datagridview1
            sql = "SELECT DISTINCT ID FROM myhistory"
            cmd = New OleDbCommand(sql, connection)
            reader = cmd.ExecuteReader()
            If Not reader.HasRows Then
                DataGridView2.Rows().Clear()
            ElseIf reader.HasRows Then
                Do While reader.Read
                    Dim row() As String = {CStr(reader.Item(0))}
                    DataGridView2.Rows.Add(row)
                Loop
            End If
        Catch ex As Exception
            'MsgBox(ex.ToString())
            printmessage("No data found.", "Error", "Try again")
            Me.Close()
        End Try
    End Sub

    Private Sub company_yearView_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        DataGridView1.Rows.Clear()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If DataGridView1.Rows.Count > 0 Then
            printfunction()
        Else
            printmessage("No data found for print", "Error", "Data not found")
        End If
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

            Dim mywidth() As Integer = {8, 20, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12}

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
            Dim filenames As String = "company_yearly_data_statement_all_" + strDate + "_.pdf"

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
                If CheckBox1.Checked = True Then
                    statement = New Paragraph("All Milk Statement:")
                Else
                    statement = New Paragraph("All Money Statement:")
                End If

            Else
                statement = New Paragraph("")
            End If
            statement.Alignment = Element.ALIGN_LEFT
            statement.IndentationLeft = 40


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

            printmessage("Print data successfully", "Info", "Successful")
        Catch ex As Exception
            'MsgBox(ex.ToString())
            MsgBox("Your pdf file is in use or your print source location is not valid.", MsgBoxStyle.Exclamation, Title:="Try again.")
        End Try
    End Sub

    Private Sub company_yearView_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then Me.Close()
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            'load milk year view
            DataGridView1.Rows.Clear()
            FILL_DVGOrderMilk()
        Else
            DataGridView1.Rows.Clear()
            FILL_DVGOrder()
            'load rs year view
        End If
    End Sub
End Class