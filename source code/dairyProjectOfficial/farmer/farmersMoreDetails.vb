Imports System.Data.OleDb
Imports iTextSharp.text.pdf
Imports iTextSharp.text
Imports System.IO
Imports System.Text.RegularExpressions
Public Class farmersMoreDetails

    Dim totalMilk As Integer = 0
    Dim totalFat As Integer = 0
    Dim totalLacto As Integer = 0
    Dim totalfatkg As Double
    Dim totalfatrs As Integer = 0
    Dim totalsnfkg As Double
    Dim totalsnf As Double
    Dim totalsnfrs As Integer = 0
    Dim totalamt As Double = 0
    Dim a As Integer
    Private Sub farmersMoreDetails_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'hide textbox
        'myId.Visible = False
        'Label4.Visible = False
        'Label3.Visible = False

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
        Catch ex As Exception
        End Try

        If detailsForm.hasShare.Checked = False Then
            without_shareholder()
        Else
            'shareholder account
            shareholder_account()
        End If
    End Sub

    Private Sub without_shareholder()
        Try
            DataGridView1.Rows.Clear()

            Label2.Visible = False
            DataGridView2.Visible = False
            DataGridView1.Height = 588

            DataGridView1.ColumnCount = 7
            With DataGridView1
                .DataSource = datast.Tables("detailsFarmerHistory")
                .RowHeadersVisible = False
                .Columns(0).HeaderCell.Value = "ID"
                .Columns(1).HeaderCell.Value = "Farmer ID"
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

            sql = "SELECT * FROM detailsFarmerHistory WHERE farmers_ID = " & myId.Text & "  ORDER BY Entry_date DESC"
            cmd = New OleDbCommand(sql, connection)
            reader = cmd.ExecuteReader()
            If Not reader.HasRows Then
                DataGridView1.Rows().Clear()
            ElseIf reader.HasRows Then
                Do While reader.Read
                    Dim row() As String = {(reader.Item(0)), reader.Item(1), reader.Item(2), reader.Item(3), reader.Item(4), reader.Item(5), reader.Item(6)}
                    DataGridView1.Rows.Add(row)
                Loop
            End If

            Dim total_milk As Double = 0
            Dim total_fat As Double = 0
            Dim total_lacto As Double = 0
            For j As Integer = 0 To DataGridView1.RowCount - 1
                If Regex.IsMatch(DataGridView1.Rows(j).Cells(4).Value, "^[0-9 ]+$") Then
                    total_milk += Val(CDbl(DataGridView1.Rows(j).Cells(4).Value))
                End If
                If Regex.IsMatch(DataGridView1.Rows(j).Cells(5).Value, "^[0-9 ]+$") Then
                    total_fat += Val(CDbl(DataGridView1.Rows(j).Cells(5).Value))
                End If
                If Regex.IsMatch(DataGridView1.Rows(j).Cells(6).Value, "^[0-9 ]+$") Then
                    total_lacto += Val(CDbl(DataGridView1.Rows(j).Cells(6).Value))
                End If
            Next

            Label7.Text = total_milk.ToString() + " ltr"
            Label9.Text = total_fat.ToString()
            Label11.Text = total_lacto.ToString()
            For i As Integer = 0 To DataGridView1.Rows.Count() Step +1
                i = +i
                a = i / 2
                Dim aa As String = a
                Label5.Text = aa + " days"
            Next
        Catch ex As Exception
            MsgBox("No data found. You have to insert first. (error 307)", MsgBoxStyle.Critical, Title:="Try again!")
            Me.Close()
            'MsgBox(ex.ToString())
        End Try
    End Sub

    Private Sub shareholder_account()
        Try
            DataGridView1.Rows.Clear()
            DataGridView2.Rows.Clear()
            Label2.Visible = True
            DataGridView1.Height = 277
            DataGridView2.Visible = True

            DataGridView1.ColumnCount = 7
            With DataGridView1
                .RowHeadersVisible = False
                .Columns(0).HeaderCell.Value = "ID"
                .Columns(1).HeaderCell.Value = "Farmer ID"
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

            sql = "SELECT * FROM detailsFarmerHistory WHERE farmers_ID = " & myId.Text & "  ORDER BY Entry_date DESC"
            cmd = New OleDbCommand(sql, connection)
            reader = cmd.ExecuteReader()
            If Not reader.HasRows Then
                DataGridView1.Rows().Clear()
            ElseIf reader.HasRows Then
                Do While reader.Read
                    Dim row() As String = {(reader.Item(0)), reader.Item(1), reader.Item(2), reader.Item(3), reader.Item(4), reader.Item(5), reader.Item(6)}
                    DataGridView1.Rows.Add(row)
                Loop
            End If
            Dim total_milk As Double = 0
            Dim total_fat As Double = 0
            Dim total_lacto As Double = 0

            For j As Integer = 0 To DataGridView1.RowCount - 1
                If Regex.IsMatch(DataGridView1.Rows(j).Cells(4).Value, "^[0-9]+$") Then
                    total_milk += Val(CDbl(DataGridView1.Rows(j).Cells(4).Value))
                End If
                If Regex.IsMatch(DataGridView1.Rows(j).Cells(5).Value, "^[0-9 ]+$") Then
                    total_fat += Val(CDbl(DataGridView1.Rows(j).Cells(5).Value))
                End If
                If Regex.IsMatch(DataGridView1.Rows(j).Cells(6).Value, "^[0-9 ]+$") Then
                    total_lacto += Val(CDbl(DataGridView1.Rows(j).Cells(6).Value))
                End If
            Next

            Label7.Text = total_milk.ToString() + " ltr"
            Label9.Text = total_fat.ToString()
            Label11.Text = total_lacto.ToString()
            For i As Integer = 0 To DataGridView1.Rows.Count() Step +1
                i = +i
                a = i / 2
                Dim aa As String = a
                Label5.Text = aa + " days"
            Next

            DataGridView2.ColumnCount = 3
            With DataGridView2
                .RowHeadersVisible = False
                .Columns(0).HeaderCell.Value = "ID"
                .Columns(1).HeaderCell.Value = "Saving Money"
                .Columns(2).HeaderCell.Value = "Saving Date"
                .Columns(0).SortMode = DataGridViewColumnSortMode.NotSortable
                .Columns(1).SortMode = DataGridViewColumnSortMode.NotSortable
                .Columns(2).SortMode = DataGridViewColumnSortMode.NotSortable
                .AllowUserToAddRows = False
                .AllowUserToResizeRows = False
                .AllowUserToOrderColumns = False
                .AllowUserToResizeColumns = True
            End With
            DataGridView1.Columns(0).FillWeight = 10
            DataGridView1.Columns(1).FillWeight = 15
            DataGridView1.Columns(2).FillWeight = 25

            Dim sql1 As String
            sql1 = "SELECT S_ID,saving_money,saving_date FROM shareholders WHERE f_ID = " & myId.Text & " ORDER BY saving_date DESC"
            cmd = New OleDbCommand(sql1, connection)
            reader = cmd.ExecuteReader()
            If Not reader.HasRows Then
                DataGridView2.Rows().Clear()
            ElseIf reader.HasRows Then
                Do While reader.Read
                    Dim row() As String = {(reader.Item(0)), reader.Item(1), reader.Item(2)}
                    DataGridView2.Rows.Add(row)
                Loop
            End If

            Dim total As Double = 0
            For i As Integer = 0 To DataGridView2.RowCount - 1
                'total = NewMethod(total, i)
                total += DataGridView2.Rows(i).Cells(1).Value
            Next

            Label4.Visible = True
            Label3.Visible = True
            Label3.Text = "Rs. " + total.ToString()

        Catch ex As Exception
            'MsgBox(ex.ToString())
            MsgBox("No data found. You have to insert first. (error - 307)", MsgBoxStyle.Critical, Title:="Try again!")
            'MsgBox(ex.ToString())
            Me.Close()
        End Try
    End Sub

    Private Function NewMethod(total As Integer, i As Integer) As Integer
        total += DataGridView2.Rows(i).Cells(1).Value
        Return total
    End Function

    Private Sub farmersMoreDetails_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        detailsForm.Close()
        Close()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            'get data from db'
            Dim companyName, companyAddress, companyPhone, filepath As String
            companyName = selectidsql("sources", "source", "10")
            companyAddress = selectidsql("sources", "source", "11")
            companyPhone = selectidsql("sources", "source", "12")
            filepath = selectidsql("sources", "source", "1")
            filepath = filepath + "\"
            'end of get data from db

            Dim myregDate As Date = Date.Now()
            Dim mystrDate As String = myregDate.ToString("dd/MMM/yyyy")

            Dim bf As Font = FontFactory.GetFont("Arial Monospaced for SAP", 7)
            Dim fFont = New Font(bf)

            Dim mywidth() As Integer = {22, 22, 25, 40, 30, 30}

            If DataGridView2.Visible = True Then
                'datagridview 2
                Dim shareholdertable As New PdfPTable(DataGridView2.ColumnCount)
                shareholdertable.DefaultCell.Padding = 7
                shareholdertable.WidthPercentage = 90
                shareholdertable.HorizontalAlignment = Element.ALIGN_CENTER
                shareholdertable.DefaultCell.BorderWidth = 1
                'pdfTable.SetWidths(mywidth

                'Adding Header row
                For Each shareholdercolumn As DataGridViewColumn In DataGridView2.Columns
                    Dim shareholdercell As New PdfPCell(New Phrase(shareholdercolumn.HeaderText))
                    shareholdercell.BackgroundColor = New iTextSharp.text.BaseColor(240, 240, 240)
                    shareholdertable.AddCell(shareholdercell)
                Next
                'Adding DataRow
                For Each shareholderrow As DataGridViewRow In DataGridView2.Rows
                    For Each shareholdercell As DataGridViewCell In shareholderrow.Cells
                        shareholdertable.AddCell(shareholdercell.Value.ToString())
                    Next
                Next



                Dim pdfTable As New PdfPTable(DataGridView1.ColumnCount)
                If DataGridView1.Rows.Count > 0 Then
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
                End If

                'Saving to PDF
                Dim folderPath As String = filepath
                If Not Directory.Exists(folderPath) Then
                    Directory.CreateDirectory(folderPath)
                End If

                Dim regDate As Date = Date.Now()
                Dim strDate As String = regDate.ToString("ddMMMyyyy")
                Dim filenames As String = myId.Text + " Export " + strDate + ".pdf"

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
                    statement = New Paragraph("User all Details:")
                Else
                    statement = New Paragraph("")
                End If
                statement.Alignment = Element.ALIGN_LEFT
                statement.IndentationLeft = 40
                Dim statement1 As Paragraph
                If DataGridView2.Visible = True Then
                    statement1 = New Paragraph("Share Statement:")
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
                    pdfDoc.Add(shareholdertable)
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
            Else
                Dim pdfTable As New PdfPTable(DataGridView1.ColumnCount)
                If DataGridView1.Rows.Count > 0 Then
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
                End If

                'Saving to PDF
                Dim folderPath As String = filepath
                If Not Directory.Exists(folderPath) Then
                    Directory.CreateDirectory(folderPath)
                End If

                Dim regDate As Date = Date.Now()
                Dim strDate As String = regDate.ToString("ddMMMyyyy")
                Dim filenames As String = myId.Text + " Export " + strDate + ".pdf"

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
                    statement = New Paragraph("User all Details:")
                Else
                    statement = New Paragraph("")
                End If
                statement.Alignment = Element.ALIGN_LEFT
                statement.IndentationLeft = 40
                Dim statement1 As Paragraph
                If DataGridView2.Visible = True Then
                    statement1 = New Paragraph("Share Statement:")
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
            End If

            MsgBox("Successfully print document as PDF.", MsgBoxStyle.Information, Title:="Successful")
        Catch ex As Exception
            'MsgBox(ex.ToString())
            MsgBox("Previous PDF file is in use. (Error code: 306)", MsgBoxStyle.Critical, Title:="Try again")
        End Try

    End Sub

    Private Sub farmersMoreDetails_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then Me.Close()
    End Sub
End Class