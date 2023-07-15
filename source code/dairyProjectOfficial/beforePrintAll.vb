Imports System.Data.OleDb
Imports iTextSharp.text.pdf
Imports iTextSharp.text
Imports System.IO
Imports System.Text.RegularExpressions

Public Class beforePrintAll
    Private Sub beforePrintAll_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
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
        Try
            FILL_DGVOrder()

        Catch ex As Exception
            printmessage("No data found", "Error", "Data not found")
            Me.Close()
        End Try

    End Sub

    Private Sub FILL_DGVOrder()
        Try
            Dim id_table As New DataTable
            sql = "SELECT DISTINCT farmers_ID FROM detailsFarmer"
            adapters = New OleDbDataAdapter(sql, connection)
            adapters.Fill(id_table)

            For i As Integer = 0 To id_table.Rows.Count - 1
                'select ID and name from `farmers` table
                Dim sql3 As String
                sql3 = "SELECT mName FROM [farmers] WHERE ID=" & id_table.Rows(i).Item(0)
                adapter = New OleDbDataAdapter(sql3, connection)
                Dim dtname As New DataTable

                adapter.Fill(dtname)
                Dim name, myid As String

                name = dtname.Rows(0).Item(0)

                myid = id_table.Rows(i).Item(0)
                'end of selecting id and name

                ''query loop
                Dim sql1 As String
                Dim dts As New DataTable
                sql1 = "SELECT milk_qty,fat_qty,lacto_qty FROM detailsFarmer WHERE farmers_ID=" & id_table.Rows(i).Item(0) & ""
                'adapters = New OleDbDataAdapter(sql1, connection)
                'adapters.Fill(dts)
                cmd = New OleDbCommand(sql1, connection)

                Dim milk_qty As Double = 0
                Dim fat_qty As Double = 0
                Dim lacto_qty As Double = 0
                Dim avg_fat_count As Double = 0
                Dim avg_lacto_count As Double = 0
                Dim totalamount As Double = 0

                reader = cmd.ExecuteReader()
                If reader.HasRows Then
                    Do While reader.Read
                        If Regex.IsMatch(reader.Item(0), "^[0-9.]+$") Then
                            milk_qty += Val(CDbl(reader.Item(0)))
                        End If
                        If Regex.IsMatch(reader.Item(1), "^[0-9.]+$") Then
                            fat_qty += Val(CDbl(reader.Item(1)))
                            avg_fat_count += 1
                        End If
                        If Regex.IsMatch(reader.Item(2), "^[0-9.]+$") Then
                            lacto_qty += Val(CDbl(reader.Item(2)))
                            avg_lacto_count += 1
                        End If
                    Loop
                End If

                If avg_fat_count <= 0 Then
                    avg_fat_count = 1
                End If
                If avg_lacto_count <= 0 Then
                    avg_lacto_count = 1
                End If

                Dim avgmilk, avgfat, avglacto, avgfatkg, avgsnfkg, avgsnf, bonus As Double

                avgmilk = CDbl(milk_qty)
                avgfat = CDbl(fat_qty) / CDbl(avg_fat_count)
                avgfat = FormatCurrency(avgfat, 3)
                avglacto = CDbl(lacto_qty) / CDbl(avg_lacto_count)
                avglacto = FormatCurrency(avglacto, 3)
                avgsnf = (CDbl(avgfat) + CDbl(avglacto) + 2) / 4
                avgsnf = FormatCurrency(avgsnf, 3)
                avgfatkg = (CDbl(milk_qty) * CDbl(avgfat)) / 100
                avgfatkg = FormatCurrency(avgfatkg, 3)

                avgfatkg = Math.Sign(avgfatkg) * Math.Floor(Math.Abs(avgfatkg) * 100) / 100.0
                avgsnf = Math.Sign(avgsnf) * Math.Floor(Math.Abs(avgsnf) * 100) / 100.0
                avgsnfkg = (CDbl(milk_qty) * CDbl(avgsnf)) / 100
                avglacto = Math.Sign(avglacto) * Math.Floor(Math.Abs(avglacto) * 100) / 100.0
                avgsnfkg = Math.Sign(avgsnfkg) * Math.Floor(Math.Abs(avgsnfkg) * 100) / 100.0

                Dim constfat, constsnf As Double
                Dim fatrs, snfrs As Double
                constfat = selectidsql("sources", "source", "14")
                constsnf = selectidsql("sources", "source", "15")

                fatrs = (CDbl(Val(avgfatkg)) * constfat) * 100
                fatrs = Int(fatrs)
                snfrs = (CDbl(Val(avgsnfkg)) * constsnf) * 100
                snfrs = Int(snfrs)

                totalamount = (CDbl(Val(fatrs)) + CDbl(Val(snfrs)))
                totalamount = FormatCurrency(totalamount, 2)

                Dim user_bonus, zero_bonus, mul_bonus As Double
                user_bonus = selectidsql("sources", "source", "19")
                zero_bonus = selectidsql("sources", "source", "21")
                mul_bonus = selectidsql("sources", "source", "20")

                If CDbl(avgsnf) >= user_bonus Then
                    bonus = (mul_bonus) * CDbl(milk_qty)
                Else
                    bonus = zero_bonus
                End If

                'saving to shareholder
                Dim saving As Double
                If CheckBox1.Checked = True Then
                    Dim findshareholder As String
                    findshareholder = checkshareholder("farmers", "hasShare", id_table.Rows(i).Item(0))
                    If findshareholder = "YES" Then
                        saving = milk_qty
                        Dim shareholderamount As Double
                        shareholderamount = selectidsql1("previousbalance", "shareholder_amount", id_table.Rows(i).Item(0), "farmer_ID")
                        shareholdersaving(saving, shareholderamount, id_table.Rows(i).Item(0))
                    Else
                        saving = 0
                    End If
                    'end of section
                Else
                    saving = 0
                End If


                Dim minimum1, maximum1, ts1, minumum2, ts2, ts3 As Double
                minimum1 = selectidsql("sources", "source", "22")
                maximum1 = selectidsql("sources", "source", "23")
                ts1 = selectidsql("sources", "source", "24")
                minumum2 = selectidsql("sources", "source", "25")
                ts2 = selectidsql("sources", "source", "26")
                ts3 = selectidsql("sources", "source", "27")

                Dim ts As Double
                If CDbl(milk_qty) >= minimum1 And CDbl(milk_qty) <= maximum1 Then
                    ts = ((CDbl(avgfatkg) + CDbl(avgsnfkg)) * ts1)
                ElseIf CDbl(milk_qty) >= minumum2 Then
                    ts = ((CDbl(avgfatkg) + CDbl(avgsnfkg)) * ts2)
                Else
                    ts = ts3
                End If

                ts = FormatCurrency(ts, 2)
                ts = Int(ts)
                saving = FormatCurrency(saving, 2)
                bonus = FormatCurrency(bonus, 2)
                bonus = Int(bonus)

                Dim totalfinalamount As Double
                totalfinalamount = CDbl(totalamount) + CDbl(bonus) + CDbl(ts) - CDbl(saving)

                Dim totalmilk, totalfat, totalfatrs, totallacto, totalsnfrs As String
                'changing double in 2 decimal'
                totalmilk = milk_qty.ToString("N2")
                totalfat = fat_qty.ToString("N2")
                avgfatkg = avgfatkg.ToString("N2")
                totalfatrs = fatrs.ToString("N0")
                totallacto = lacto_qty.ToString("N2")
                avgmilk = avgmilk.ToString("N2")
                avgfat = avgfat.ToString("N2")
                avgsnf = avgsnf.ToString("N2")
                avgsnfkg = avgsnfkg.ToString("N2")
                totalsnfrs = snfrs.ToString("N0")
                ts = ts.ToString("N0")
                bonus = bonus.ToString("N0")
                avglacto = avglacto.ToString("N2")
                totalamount = totalamount.ToString("N0")
                totalfinalamount = totalfinalamount.ToString("N0")

                'end of changing'

                'send data to another form for printing...

                With DataGridView1
                    .RowHeadersVisible = False
                    .AllowUserToAddRows = False
                    .AllowUserToResizeRows = False
                    .AllowUserToOrderColumns = False
                    .AllowUserToResizeColumns = False
                End With
                DataGridView1.ColumnCount = 13
                Dim row As String() = New String() {myid, name, totalmilk, avgfat,
                    avgfatkg, totalfatrs, avgsnf, avgsnfkg, totalsnfrs,
                    ts, bonus, totalfinalamount, ""}

                DataGridView1.Rows().Add(row)
            Next


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
                total_milk += Val(CDbl(DataGridView1.Rows(j).Cells(2).Value))
                total_fat += Val(CDbl(DataGridView1.Rows(j).Cells(3).Value))
                total_fatkg += Val(CDbl(DataGridView1.Rows(j).Cells(4).Value))
                total_fatrs += Val(CDbl(DataGridView1.Rows(j).Cells(5).Value))
                total_snf += Val(CDbl(DataGridView1.Rows(j).Cells(6).Value))
                total_snfkg += Val(CDbl(DataGridView1.Rows(j).Cells(7).Value))
                total_snfrs += Val(CDbl(DataGridView1.Rows(j).Cells(8).Value))
                total_ts += Val(CDbl(DataGridView1.Rows(j).Cells(9).Value))
                total_addedrs += Val(CDbl(DataGridView1.Rows(j).Cells(10).Value))
                total_amounts += Val(CDbl(DataGridView1.Rows(j).Cells(11).Value))
            Next
            total_fat = CDbl(total_fat) / CDbl(DataGridView1.Rows.Count)
            total_fat = total_fat.ToString("N2")



            Dim rows1 As String() = {"", "TOTAL", total_milk, total_fat, total_fatkg, total_fatrs, total_snf, total_snfkg, total_snfrs, total_ts, total_addedrs, total_amounts, ""}
            DataGridView1.Rows.Add(rows1)

            With DataGridView1
                .Columns(0).HeaderCell.Value = "ID"
                .Columns(1).HeaderCell.Value = "Name"
                .Columns(2).HeaderCell.Value = "Milk"
                .Columns(3).HeaderCell.Value = "Fat"
                .Columns(4).HeaderCell.Value = "Fat/kg"
                .Columns(5).HeaderCell.Value = "fat/rs"
                .Columns(6).HeaderCell.Value = "Snf"
                .Columns(7).HeaderCell.Value = "Snf/kg"
                .Columns(8).HeaderCell.Value = "Snf/rs"
                .Columns(9).HeaderCell.Value = "Ts"
                .Columns(10).HeaderCell.Value = "Added Rs."
                .Columns(11).HeaderCell.Value = "Total amount"
                .Columns(12).HeaderCell.Value = "Signature"
            End With
        Catch ex As Exception
            'MsgBox(ex.ToString())
            printmessage("No data found.", "Error", "Data not found")
            Me.Close()
        End Try
    End Sub

    Private Sub shareholdersaving(totalmilk As Double, shareholderamount As Double, farmerID As Integer)
        Try

            Using con As OleDbConnection = New OleDbConnection(companyCon)
                Using cmd = con.CreateCommand()
                    cmd.CommandText = "UPDATE previousbalance SET shareholder_amount = '" & CDbl(totalmilk) + CDbl(shareholderamount) _
                        & "' WHERE farmer_ID=" & farmerID & ""
                    con.Open()
                    cmd.ExecuteNonQuery()
                    con.Close()
                End Using
            End Using
        Catch ex As Exception
            'MsgBox(ex.ToString())
        End Try
    End Sub

    Private Sub beforePrintAll_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        DataGridView1.DataSource = Nothing
        Me.Dispose()
        Me.Close()

        Try
            'to start connection of ms-access
            connection = New OleDbConnection
            With connection
                If .State = ConnectionState.Closed Then
                    .ConnectionString = strCon
                    .Open()
                End If
            End With
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            Dim results As Integer = MessageBox.Show("Did you load your saving amount in " & Environment.NewLine & " shareholder account ?", "Confirmation Box", MessageBoxButtons.YesNo)
            If results = DialogResult.No Then
                'nothing to do
                Return
            ElseIf results = DialogResult.Yes Then
                Dim myregDate As Date = Date.Now()
                Dim mystrDate As String = myregDate.ToString("dd/MMM/yyyy")
                For row As Integer = 0 To DataGridView1.Rows.Count - 2
                    Dim constring As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & Application.StartupPath & "\mydatabase1.mdb;Jet OLEDB:Database Password=login1234"
                    Using con As New OleDbConnection(constring)
                        Using cmd1 As New OleDbCommand("INSERT INTO myhistory(ID,Entry_Date,mName,Milk,Fat,Fatkg,Fatrs,Snf,Snfkg,Snfrs,Ts,Bonus,Total) VALUES(@farmerId, @entryDate, @farmerName, @milkqty, @fatqty, @fatkg, @fatrs, @snfqty, @snfkg, @snfrs, @ts, @bonus, @totalamt)", con)
                            cmd1.Parameters.AddWithValue("@farmerId", DataGridView1.Rows(row).Cells(0).Value)
                            cmd1.Parameters.AddWithValue("@entryDate", mystrDate)
                            cmd1.Parameters.AddWithValue("@farmerName", DataGridView1.Rows(row).Cells(1).Value)
                            cmd1.Parameters.AddWithValue("@milkqty", DataGridView1.Rows(row).Cells(2).Value)
                            cmd1.Parameters.AddWithValue("@fatqty", DataGridView1.Rows(row).Cells(3).Value)
                            cmd1.Parameters.AddWithValue("@fatkg", DataGridView1.Rows(row).Cells(4).Value)
                            cmd1.Parameters.AddWithValue("@fatrs", DataGridView1.Rows(row).Cells(5).Value)
                            cmd1.Parameters.AddWithValue("@snfqty", DataGridView1.Rows(row).Cells(6).Value)
                            cmd1.Parameters.AddWithValue("@snfkg", DataGridView1.Rows(row).Cells(7).Value)
                            cmd1.Parameters.AddWithValue("@snfrs", DataGridView1.Rows(row).Cells(8).Value)
                            cmd1.Parameters.AddWithValue("@ts", DataGridView1.Rows(row).Cells(9).Value)
                            cmd1.Parameters.AddWithValue("@bonus", DataGridView1.Rows(row).Cells(10).Value)
                            cmd1.Parameters.AddWithValue("@totalamt", DataGridView1.Rows(row).Cells(11).Value)
                            con.Open()
                            cmd1.ExecuteNonQuery()
                            con.Close()
                        End Using
                    End Using
                Next

                Dim bf As Font = FontFactory.GetFont("Arial Monospaced for SAP", 7)
                Dim fFont = New Font(bf)
                sql = "SELECT source FROM sources WHERE ID = 1"
                cmd = New OleDbCommand(sql, connection)
                Dim da As OleDbDataAdapter = New OleDbDataAdapter(cmd)
                da.Fill(ds, "source")
                Dim filepath As String
                filepath = ds.Tables(0).Rows(ds.Tables(0).Rows.Count - 1).Item("source").ToString
                filepath = filepath + "\"

                'get data from db'
                Dim companyName, companyAddress, companyPhone As String

                companyName = selectidsql("sources", "source", "10")
                companyAddress = selectidsql("sources", "source", "11")
                companyPhone = selectidsql("sources", "source", "12")

                Dim mywidth() As Integer = {15, 45, 22, 22, 22, 22, 22, 22, 22, 15, 15, 31, 25}

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

                'Saving to PDF
                Dim folderPath As String = filepath
                If Not Directory.Exists(folderPath) Then
                    Directory.CreateDirectory(folderPath)
                End If

                Dim regDate As Date = Date.Now()
                Dim strDate As String = regDate.ToString("ddMMMyyyy")
                Dim filenames As String = strDate + ".pdf"

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


                Using Stream As New FileStream(folderPath & filenames, FileMode.Create)
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


                Using con As OleDbConnection = New OleDbConnection(strCon)
                    Using cmd = con.CreateCommand()
                        cmd.CommandText = "INSERT INTO detailsFarmerHistory(ID,farmers_ID,Entry_date,Entry_Shift,milk_qty,fat_qty,lacto_qty)SELECT * FROM detailsFarmer"
                        con.Open()
                        cmd.ExecuteNonQuery()
                        cmd.Dispose()
                        con.Close()
                    End Using
                End Using

                Using con As OleDbConnection = New OleDbConnection(strCon)
                    Using cmd = con.CreateCommand()
                        cmd.CommandText = "DELETE FROM detailsFarmer"
                        con.Open()
                        cmd.ExecuteNonQuery()
                        cmd.Dispose()
                        con.Close()
                    End Using
                End Using

                printmessage("print data successfully", "Info", "Successful")

                Me.Dispose()
                Me.Close()
            End If

        Catch ex As Exception
            'MsgBox(ex.ToString())
            MsgBox("Your pdf file is in use or your print source location is not valid.", MsgBoxStyle.Exclamation, Title:="Try again.")
        End Try
    End Sub

    Private Sub beforePrintAll_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then Me.Close()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If CheckBox1.Checked = True Then
            printmessage("Data send to saving accounts", "Info", "Success")
            DataGridView1.Rows.Clear()
            beforePrintAll_Load(e, e)
        Else
            printmessage("Please checked box before proceed.", "Error", "Try againa")
        End If
    End Sub
End Class