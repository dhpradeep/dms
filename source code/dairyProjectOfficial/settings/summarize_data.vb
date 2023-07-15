Imports System.Data.OleDb
Imports System.IO
Imports iTextSharp.text.pdf
Imports iTextSharp.text
Public Class summarize_data
    Public thisYear As String = System.DateTime.Now.ToString("yyyy")

    Private Sub summarize_data_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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

        Catch ex As Exception
            MsgBox(ex.ToString())
        End Try
    End Sub

    Private Sub summarize_data_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then Me.Close()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        Dim pdfpath As String
        pdfpath = selectidsql("sources", "source", "1")
        Dim newFolder As String = pdfpath + "\" + thisYear

        If (Not Directory.Exists(newFolder)) Then
            Directory.CreateDirectory(newFolder)
        Else
            Try
                Directory.Delete(newFolder, True)
                Directory.CreateDirectory(newFolder)
            Catch ex As Exception
                printmessage("Folder already exist.", "Error", "Try again")
            End Try
        End If

        'backup database in `currYear\database`
        backupDatabase(newFolder)

        'it makes folder of previous year
        makeFolder(newFolder)

        'now print pdf inside these folders
        printPDF(newFolder)

        'broke connection from current database
        'copy new database from other folder
        'copy needed data from old db to new db
        copyFromBackup(newFolder) 'named as data1.mdb and data2.mdb

        'copy data from old db to this db
        copyDataFromOldDbToNewDB()
        'delete old db
        'deleteOldDB()

    End Sub

    Private Sub copyDataFromOldDbToNewDB()

    End Sub

    Private Sub copyFromBackup(newFolder)
        Try
            If File.Exists(Application.StartupPath & "\backup\mydatabase1.mdb") OrElse File.Exists(Application.StartupPath & "\backup\mydatabase2.mdb") Then
                If File.Exists(Application.StartupPath & "\data1.mdb") OrElse File.Exists(Application.StartupPath & "\data2.mdb") Then
                    File.Delete(Application.StartupPath & "\data1.mdb")
                    File.Delete(Application.StartupPath & "\data2.mdb")
                End If
                File.Copy(Application.StartupPath & "\backup\mydatabase1.mdb", Application.StartupPath() & "\data1.mdb")
                File.Copy(Application.StartupPath & "\backup\mydatabase2.mdb", Application.StartupPath() & "\data2.mdb")
            Else
                MsgBox("Data not found in backup folder.")
            End If
        Catch ex As Exception
            MsgBox(ex.ToString())
        End Try
    End Sub

    Private Function makeFolder(newFolder)
        Try
            Dim milk_history, milk_total_history, loan, income, exp, shareamount, shareholders As String
            milk_history = newFolder + "\Milk_History"
            milk_total_history = newFolder + "\Milk_total_History"
            loan = newFolder + "\Loan"
            income = newFolder + "\Income_statement"
            exp = newFolder + "\Expenditure_statement"
            shareamount = newFolder + "\Share_amount"
            shareholders = newFolder + "\Shareholders"

            Directory.CreateDirectory(milk_history)
            Directory.CreateDirectory(milk_total_history)
            Directory.CreateDirectory(loan)
            Directory.CreateDirectory(income)
            Directory.CreateDirectory(exp)
            Directory.CreateDirectory(shareamount)
            Directory.CreateDirectory(shareholders)


            Dim month() As String = {"Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"}

            For i As Integer = 0 To month.Length - 1
                Directory.CreateDirectory(milk_history + "\" + month(i))
                Directory.CreateDirectory(milk_total_history + "\" + month(i))
            Next

        Catch ex As Exception
            MsgBox(ex.ToString())
        End Try

        Return newFolder
    End Function

    Private Sub backupDatabase(newFolder)
        Try
            Dim database_backup As String = newFolder + "\Database_backup"
            Directory.CreateDirectory(database_backup)
            If File.Exists(database_backup + "\mydatabase1.mdb") OrElse File.Exists(database_backup + "\mydatabase2.mdb") Then
                File.Delete(database_backup + "\mydatabase1.mdb")
                File.Delete(database_backup + "\mydatabase2.mdb")
            End If
            File.Copy("mydatabase1.mdb", database_backup & "\mydatabase1.mdb")
            File.Copy("mydatabase2.mdb", database_backup & "\mydatabase2.mdb")
        Catch ex As Exception
        End Try
    End Sub

    Private Function printPDF(newFolder)
        'get data from `detailsFarmerHistory`
        fillDataofMilkHistory()

        'get data from `myhistory`
        fillDataofHostory()

        'get data from `loanholderhist`
        fillDataofLoan()

        'get data from `incomestatementhist
        fillDataofIncome()
        'get data from `expenditurestatementhist
        fillDataofExpenditure()
        'get data from `shareamount`
        fillDataofShareamount()
        'get data from `shareholders`
        fillDataofShareholders()

        MsgBox("Print data successful.")
        Return 0
    End Function

    Private Sub fillDataofShareholders()
        Try
            Dim id_table2 As New DataTable
            sql = "SELECT DISTINCT f_ID FROM shareHolders"
            adapters = New OleDbDataAdapter(sql, connection)
            adapters.Fill(id_table2)

            For i As Integer = 0 To id_table2.Rows.Count - 1
                'select ID and name from `farmers` table
                Dim sql3 As String
                sql3 = "SELECT mName FROM [farmers] WHERE ID=" & id_table2.Rows(i).Item(0)
                adapter = New OleDbDataAdapter(sql3, connection)
                Dim dtname As New DataTable
                adapter.Fill(dtname)
                Dim name, myid As String
                name = dtname.Rows(0).Item(0)
                myid = id_table2.Rows(i).Item(0)

                Dim dict = New Dictionary(Of String, String)
                dict.Add("Jan", thisYear + "/01")
                dict.Add("Feb", thisYear + "/02")
                dict.Add("Mar", thisYear + "/03")
                dict.Add("Apr", thisYear + "/04")
                dict.Add("May", thisYear + "/05")
                dict.Add("Jun", thisYear + "/06")
                dict.Add("Jul", thisYear + "/07")
                dict.Add("Aug", thisYear + "/08")
                dict.Add("Sep", thisYear + "/09")
                dict.Add("Oct", thisYear + "/10")
                dict.Add("Nov", thisYear + "/11")
                dict.Add("Dec", thisYear + "/12")

                For Each kv As KeyValuePair(Of String, String) In dict
                    Dim sql1 As String = "SELECT s_ID,f_ID,saving_money,saving_date FROM shareHolders WHERE saving_date like '%" & kv.Value & "%'"
                    Dim j As Integer = 1
                    DataGridView1.Rows.Clear()
                    DataGridView1.ColumnCount = 5
                    With DataGridView1
                        .Columns(0).HeaderCell.Value = "S.N"
                        .Columns(1).HeaderCell.Value = "Farmer_ID"
                        .Columns(2).HeaderCell.Value = "Name"
                        .Columns(3).HeaderCell.Value = "Saving Money"
                        .Columns(4).HeaderCell.Value = "Saving Date"
                    End With

                    cmd = New OleDbCommand(sql1, connection)
                    reader = cmd.ExecuteReader()
                    If reader.HasRows Then
                        Do While reader.Read
                            Dim row() As String = {CStr(j), CStr(reader.Item(0)), CStr(reader.Item(1)), CStr(name), CStr(reader.Item(2)),
                            CStr(reader.Item(3))}
                            DataGridView1.Rows.Add(row)
                            j += 1
                        Loop
                    End If

                    Dim mywidth() As Integer = {15, 18, 25, 25, 25}
                    'send data to print as pdf only if those month has data
                    If DataGridView1.Rows.Count > 1 Then
                        printData3(DataGridView1, myid, "Shareholder_", kv.Key, mywidth)
                    End If
                Next
            Next
        Catch ex As Exception
            MsgBox(ex.ToString())
        End Try
    End Sub

    Private Sub fillDataofShareamount()
        Try
            Dim sql2 As String = "SELECT * FROM shareamount"
            Dim j As Integer = 1
            DataGridView1.Rows.Clear()
            DataGridView1.ColumnCount = 5
            With DataGridView1
                .Columns(0).HeaderCell.Value = "S.N"
                .Columns(1).HeaderCell.Value = "ID"
                .Columns(2).HeaderCell.Value = "Farmer_ID"
                .Columns(3).HeaderCell.Value = "Name"
                .Columns(4).HeaderCell.Value = "Amount"
            End With
            cmd = New OleDbCommand(sql2, connection1)
            reader = cmd.ExecuteReader()
            If reader.HasRows Then
                Do While reader.Read
                    Dim row() As String = {CStr(j), CStr(reader.Item(0)), CStr(reader.Item(1)), CStr(reader.Item(2)),
                    CStr(reader.Item(3))}
                    DataGridView1.Rows.Add(row)
                    j += 1
                Loop
            End If

            Dim mywidth() As Integer = {18, 18, 18, 25, 25}
            printData2(DataGridView1, "Share_amount", mywidth)
        Catch ex As Exception
            MsgBox(ex.ToString())
        End Try
    End Sub

    Private Sub fillDataofIncome()
        Try
            Dim sql2 As String = "SELECT * FROM incomestatementhist"
            Dim j As Integer = 1
            DataGridView1.Rows.Clear()
            DataGridView1.ColumnCount = 6
            With DataGridView1
                .Columns(0).HeaderCell.Value = "S.N"
                .Columns(1).HeaderCell.Value = "ID"
                .Columns(2).HeaderCell.Value = "Date"
                .Columns(3).HeaderCell.Value = "Statement"
                .Columns(4).HeaderCell.Value = "Quantity"
                .Columns(5).HeaderCell.Value = "Amount"
            End With
            cmd = New OleDbCommand(sql2, connection1)
            reader = cmd.ExecuteReader()
            If reader.HasRows Then
                Do While reader.Read
                    Dim row() As String = {CStr(j), CStr(reader.Item(0)), CStr(reader.Item(1)), CStr(reader.Item(2)),
                    CStr(reader.Item(3)), CStr(reader.Item(4))}
                    DataGridView1.Rows.Add(row)
                    j += 1
                Loop
            End If

            Dim mywidth() As Integer = {15, 15, 22, 30, 20, 25}
            printData2(DataGridView1, "Income_statement", mywidth)
        Catch ex As Exception
            MsgBox(ex.ToString())
        End Try
    End Sub

    Private Sub fillDataofExpenditure()
        Try
            Dim sql2 As String = "SELECT * FROM expenditurestatementhist"
            Dim j As Integer = 1
            DataGridView1.Rows.Clear()
            DataGridView1.ColumnCount = 6
            With DataGridView1
                .Columns(0).HeaderCell.Value = "S.N"
                .Columns(1).HeaderCell.Value = "ID"
                .Columns(2).HeaderCell.Value = "Date"
                .Columns(3).HeaderCell.Value = "Statement"
                .Columns(4).HeaderCell.Value = "Quantity"
                .Columns(5).HeaderCell.Value = "Amount"
            End With
            cmd = New OleDbCommand(sql2, connection1)
            reader = cmd.ExecuteReader()
            If reader.HasRows Then
                Do While reader.Read
                    Dim row() As String = {CStr(j), CStr(reader.Item(0)), CStr(reader.Item(1)), CStr(reader.Item(2)),
                    CStr(reader.Item(3)), CStr(reader.Item(4))}
                    DataGridView1.Rows.Add(row)
                    j += 1
                Loop
            End If

            Dim mywidth() As Integer = {15, 15, 22, 30, 20, 25}
            printData2(DataGridView1, "Expenditure_statement", mywidth)
        Catch ex As Exception
            MsgBox(ex.ToString())
        End Try
    End Sub

    Private Sub fillDataofLoan()
        Try
            Dim sql2 As String = "SELECT * FROM loanholderhist"
            Dim j As Integer = 1
            DataGridView1.Rows.Clear()
            DataGridView1.ColumnCount = 9
            With DataGridView1
                .Columns(0).HeaderCell.Value = "S.N"
                .Columns(1).HeaderCell.Value = "ID"
                .Columns(2).HeaderCell.Value = "Farmer_ID"
                .Columns(3).HeaderCell.Value = "Name"
                .Columns(4).HeaderCell.Value = "Taken Date"
                .Columns(5).HeaderCell.Value = "Return Date"
                .Columns(6).HeaderCell.Value = "Partial"
                .Columns(7).HeaderCell.Value = "Return Amount"
                .Columns(8).HeaderCell.Value = "Taken Amount"
            End With
            cmd = New OleDbCommand(sql2, connection1)
            reader = cmd.ExecuteReader()
            If reader.HasRows Then
                Do While reader.Read
                    Dim row() As String = {CStr(j), CStr(reader.Item(0)), CStr(reader.Item(1)), CStr(reader.Item(2)),
                    CStr(reader.Item(3)), CStr(reader.Item(4)), CStr(reader.Item(5)), CStr(reader.Item(6)),
                    CStr(reader.Item(7))}
                    DataGridView1.Rows.Add(row)
                    j += 1
                Loop
            End If

            Dim mywidth() As Integer = {15, 15, 15, 25, 25, 25, 20, 22, 22}
            printData2(DataGridView1, "Loan", mywidth)
        Catch ex As Exception
            MsgBox(ex.ToString())
        End Try
    End Sub

    Private Sub printData2(datagridview As DataGridView, Name As String, ByVal ParamArray mywidth As Integer())
        Try
            'get data from db'
            Dim companyName, companyAddress, companyPhone, filepath As String
            companyName = selectidsql("sources", "source", "10")
            companyAddress = selectidsql("sources", "source", "11")
            companyPhone = selectidsql("sources", "source", "12")
            filepath = selectidsql("sources", "source", "1")
            filepath = filepath + "\"
            'end of get data from db

            Dim pdfTable As New PdfPTable(datagridview.ColumnCount)
            pdfTable.DefaultCell.Padding = 7
            pdfTable.WidthPercentage = 90
            pdfTable.HorizontalAlignment = Element.ALIGN_CENTER
            pdfTable.DefaultCell.BorderWidth = 1
            pdfTable.SetWidths(mywidth)

            'Adding Header row
            For Each column As DataGridViewColumn In datagridview.Columns
                Dim cell As New PdfPCell(New Phrase(column.HeaderText))
                cell.BackgroundColor = New iTextSharp.text.BaseColor(240, 240, 240)
                pdfTable.AddCell(cell)
            Next

            'Adding DataRow
            For Each row As DataGridViewRow In datagridview.Rows
                For Each cell As DataGridViewCell In row.Cells
                    If Not cell.Value = "" Then
                        pdfTable.AddCell(cell.Value.ToString())
                    End If
                Next
            Next

            'Saving to PDF
            Dim folderPath As String = filepath + thisYear + "\" & Name & "\"
            If Not Directory.Exists(folderPath) Then
                Directory.CreateDirectory(folderPath)
            End If

            Dim regDate As Date = Date.Now()
            Dim strDate As String = regDate.ToString("ddMMMyyyy")
            Dim filenames As String = Name & "_" + thisYear + "_.pdf"

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

            Dim year As Paragraph = New Paragraph("")
            year = New Paragraph("Year: " + thisYear)
            year.Alignment = Element.ALIGN_LEFT
            year.IndentationLeft = 40
            Dim titles As Paragraph = New Paragraph("")
            titles = New Paragraph("Title: " + Name)
            titles.Alignment = Element.ALIGN_LEFT
            titles.IndentationLeft = 40

            Dim p As Paragraph = New Paragraph("Signature")
            p.Alignment = Element.ALIGN_RIGHT
            p.IndentationRight = 55
            Dim mytotal As Paragraph = New Paragraph("Total:")
            mytotal.Alignment = Element.ALIGN_LEFT
            mytotal.IndentationLeft = 40
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
                'Dim pdfDoc As New Document(PageSize.A2, 10.0F, 10.0F, 10.0F, 0.0F)
                Dim pdfDoc As New Document(PageSize.A4.Rotate, 20.0F, 20.0F, 20.0F, 20.0F)
                PdfWriter.GetInstance(pdfDoc, Stream)
                pdfDoc.Open()
                pdfDoc.Add(New Paragraph(" "))
                pdfDoc.Add(New Paragraph(title))
                pdfDoc.Add(New Paragraph(address))
                pdfDoc.Add(New Paragraph(phone))
                pdfDoc.Add(New Paragraph(year))
                pdfDoc.Add(New Paragraph(titles))
                pdfDoc.Add(New Paragraph(currdate))
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

        Catch ex As Exception
            MsgBox(ex.ToString())
        End Try
    End Sub

    Private Sub fillDataofHostory()
        Try
            Dim id_table1 As New DataTable
            sql = "SELECT DISTINCT ID FROM myhistory"
            adapters = New OleDbDataAdapter(sql, connection)
            adapters.Fill(id_table1)

            For i As Integer = 0 To id_table1.Rows.Count - 1
                'select ID and name from `farmers` table
                Dim sql3 As String
                sql3 = "SELECT mName FROM [farmers] WHERE ID=" & id_table1.Rows(i).Item(0)
                adapter = New OleDbDataAdapter(sql3, connection)
                Dim dtname As New DataTable
                adapter.Fill(dtname)
                Dim name, myid As String
                name = dtname.Rows(0).Item(0)
                myid = id_table1.Rows(i).Item(0)

                Dim dict = New Dictionary(Of String, String)
                dict.Add("Jan", "Jan/" + thisYear)
                dict.Add("Feb", "Feb/" + thisYear)
                dict.Add("Mar", "Mar/" + thisYear)
                dict.Add("Apr", "Apr/" + thisYear)
                dict.Add("May", "May/" + thisYear)
                dict.Add("Jun", "Jun/" + thisYear)
                dict.Add("Jul", "Jul/" + thisYear)
                dict.Add("Aug", "Aug/" + thisYear)
                dict.Add("Sep", "Sep/" + thisYear)
                dict.Add("Oct", "Oct/" + thisYear)
                dict.Add("Nov", "Nov/" + thisYear)
                dict.Add("Dec", "Dec/" + thisYear)

                For Each kv As KeyValuePair(Of String, String) In dict
                    Dim sql1 As String = "SELECT Milk, Fat, Fatkg, Fatrs, Snf, Snfkg, Snfrs, Ts, Bonus, Total FROM myhistory WHERE ID=" & id_table1.Rows(i).Item(0) & " AND Entry_date like '%" & kv.Value & "%'"
                    Dim j As Integer = 1
                    DataGridView1.Rows.Clear()
                    DataGridView1.ColumnCount = 11
                    With DataGridView1
                        .Columns(0).HeaderCell.Value = "S.N"
                        .Columns(1).HeaderCell.Value = "Milk.Qty"
                        .Columns(2).HeaderCell.Value = "Fat.Qty"
                        .Columns(3).HeaderCell.Value = "Fat.Kg"
                        .Columns(4).HeaderCell.Value = "Fat.Rs"
                        .Columns(5).HeaderCell.Value = "Snf"
                        .Columns(6).HeaderCell.Value = "Snf.Kg"
                        .Columns(7).HeaderCell.Value = "Snf.Rs"
                        .Columns(8).HeaderCell.Value = "Ts"
                        .Columns(9).HeaderCell.Value = "Bonus"
                        .Columns(10).HeaderCell.Value = "Total"
                    End With

                    cmd = New OleDbCommand(sql1, connection)
                    reader = cmd.ExecuteReader()
                    If reader.HasRows Then
                        Do While reader.Read
                            Dim row() As String = {CStr(j), CStr(reader.Item(0)), CStr(reader.Item(1)), CStr(reader.Item(2)),
                            CStr(reader.Item(3)), CStr(reader.Item(4)), CStr(reader.Item(5)), CStr(reader.Item(6)),
                            CStr(reader.Item(7)), CStr(reader.Item(8)), CStr(reader.Item(9))}
                            DataGridView1.Rows.Add(row)
                            j += 1
                        Loop
                    End If

                    Dim mywidth() As Integer = {15, 20, 18, 18, 18, 18, 18, 18, 18, 18, 18}
                    'send data to print as pdf only if those month has data
                    If DataGridView1.Rows.Count > 1 Then
                        printData1(DataGridView1, myid, name, kv.Key, mywidth)
                    End If
                Next
            Next
        Catch ex As Exception
            MsgBox(ex.ToString())
        End Try
    End Sub

    Private Sub fillDataofMilkHistory()
        Try
            Dim id_table As New DataTable
            sql = "SELECT DISTINCT farmers_ID FROM detailsFarmerHistory"
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

                Dim dict = New Dictionary(Of String, String)
                dict.Add("Jan", thisYear + "/01")
                dict.Add("Feb", thisYear + "/02")
                dict.Add("Mar", thisYear + "/03")
                dict.Add("Apr", thisYear + "/04")
                dict.Add("May", thisYear + "/05")
                dict.Add("Jun", thisYear + "/06")
                dict.Add("Jul", thisYear + "/07")
                dict.Add("Aug", thisYear + "/08")
                dict.Add("Sep", thisYear + "/09")
                dict.Add("Oct", thisYear + "/10")
                dict.Add("Nov", thisYear + "/11")
                dict.Add("Dec", thisYear + "/12")

                For Each kv As KeyValuePair(Of String, String) In dict
                    Dim sql1 As String = "SELECT milk_qty, fat_qty, lacto_qty FROM detailsFarmerHistory WHERE farmers_ID=" & id_table.Rows(i).Item(0) & " AND Entry_date like '%" & kv.Value & "%'"
                    Dim j As Integer = 1
                    DataGridView1.Rows.Clear()
                    DataGridView1.ColumnCount = 4
                    With DataGridView1
                        .Columns(0).HeaderCell.Value = "S.N"
                        .Columns(1).HeaderCell.Value = "Milk.Qty"
                        .Columns(2).HeaderCell.Value = "Fat.Qty"
                        .Columns(3).HeaderCell.Value = "Lacto.Qty"
                    End With

                    cmd = New OleDbCommand(sql1, connection)
                    reader = cmd.ExecuteReader()
                    If reader.HasRows Then
                        Do While reader.Read
                            Dim row() As String = {CStr(j), CStr(reader.Item(0)), CStr(reader.Item(1)), CStr(reader.Item(2))}
                            DataGridView1.Rows.Add(row)
                            j += 1
                        Loop
                    End If

                    Dim mywidth() As Integer = {22, 22, 22, 22}
                    'send data to print as pdf only if those month has data
                    If DataGridView1.Rows.Count > 1 Then
                        printData(DataGridView1, myid, name, kv.Key, mywidth)
                    End If
                Next
            Next
        Catch ex As Exception
            MsgBox(ex.ToString())
        End Try
    End Sub

    Private Sub printData(datagridview As DataGridView, id As String, name As String, currMonth As String, ByVal ParamArray mywidth As Integer())
        Try
            'get data from db'
            Dim companyName, companyAddress, companyPhone, filepath As String
            companyName = selectidsql("sources", "source", "10")
            companyAddress = selectidsql("sources", "source", "11")
            companyPhone = selectidsql("sources", "source", "12")
            filepath = selectidsql("sources", "source", "1")
            filepath = filepath + "\"
            'end of get data from db

            Dim pdfTable As New PdfPTable(datagridview.ColumnCount)
            pdfTable.DefaultCell.Padding = 7
            pdfTable.WidthPercentage = 90
            pdfTable.HorizontalAlignment = Element.ALIGN_CENTER
            pdfTable.DefaultCell.BorderWidth = 1
            pdfTable.SetWidths(mywidth)

            'Adding Header row
            For Each column As DataGridViewColumn In datagridview.Columns
                Dim cell As New PdfPCell(New Phrase(column.HeaderText))
                cell.BackgroundColor = New iTextSharp.text.BaseColor(240, 240, 240)
                pdfTable.AddCell(cell)
            Next

            'Adding DataRow
            For Each row As DataGridViewRow In datagridview.Rows
                For Each cell As DataGridViewCell In row.Cells
                    If Not cell.Value = "" Then
                        pdfTable.AddCell(cell.Value.ToString())
                    End If
                Next
            Next

            'Saving to PDF
            Dim folderPath As String = filepath + thisYear + "\Milk_History\" + currMonth + "\"
            If Not Directory.Exists(folderPath) Then
                Directory.CreateDirectory(folderPath)
            End If

            Dim regDate As Date = Date.Now()
            Dim strDate As String = regDate.ToString("ddMMMyyyy")
            Dim filenames As String = name + strDate + ".pdf"

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

            Dim myid As Paragraph = New Paragraph("")
            Dim myname As Paragraph = New Paragraph("")
            If Not (id = "" OrElse name = "") Then
                myid = New Paragraph("ID: " + id)
                myid.Alignment = Element.ALIGN_LEFT
                myid.IndentationLeft = 40
                myname = New Paragraph("Name: " + name)
                myname.Alignment = Element.ALIGN_LEFT
                myname.IndentationLeft = 40
            End If

            Dim p As Paragraph = New Paragraph("Signature")
            p.Alignment = Element.ALIGN_RIGHT
            p.IndentationRight = 55
            Dim mytotal As Paragraph = New Paragraph("Total:")
            mytotal.Alignment = Element.ALIGN_LEFT
            mytotal.IndentationLeft = 40
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
                'Dim pdfDoc As New Document(PageSize.A2, 10.0F, 10.0F, 10.0F, 0.0F)
                Dim pdfDoc As New Document(PageSize.A4.Rotate, 20.0F, 20.0F, 20.0F, 20.0F)
                PdfWriter.GetInstance(pdfDoc, Stream)
                pdfDoc.Open()
                pdfDoc.Add(New Paragraph(" "))
                pdfDoc.Add(New Paragraph(title))
                pdfDoc.Add(New Paragraph(address))
                pdfDoc.Add(New Paragraph(phone))
                pdfDoc.Add(New Paragraph(myid))
                pdfDoc.Add(New Paragraph(myname))
                pdfDoc.Add(New Paragraph(currdate))
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

        Catch ex As Exception
            MsgBox(ex.ToString())
        End Try
    End Sub

    Private Sub printData1(datagridview As DataGridView, id As String, name As String, currMonth As String, ByVal ParamArray mywidth As Integer())
        Try
            'get data from db'
            Dim companyName, companyAddress, companyPhone, filepath As String
            companyName = selectidsql("sources", "source", "10")
            companyAddress = selectidsql("sources", "source", "11")
            companyPhone = selectidsql("sources", "source", "12")
            filepath = selectidsql("sources", "source", "1")
            filepath = filepath + "\"
            'end of get data from db

            Dim pdfTable As New PdfPTable(datagridview.ColumnCount)
            pdfTable.DefaultCell.Padding = 7
            pdfTable.WidthPercentage = 90
            pdfTable.HorizontalAlignment = Element.ALIGN_CENTER
            pdfTable.DefaultCell.BorderWidth = 1
            pdfTable.SetWidths(mywidth)

            'Adding Header row
            For Each column As DataGridViewColumn In datagridview.Columns
                Dim cell As New PdfPCell(New Phrase(column.HeaderText))
                cell.BackgroundColor = New iTextSharp.text.BaseColor(240, 240, 240)
                pdfTable.AddCell(cell)
            Next

            'Adding DataRow
            For Each row As DataGridViewRow In datagridview.Rows
                For Each cell As DataGridViewCell In row.Cells
                    If Not cell.Value = "" Then
                        pdfTable.AddCell(cell.Value.ToString())
                    End If
                Next
            Next

            'Saving to PDF
            Dim folderPath As String = filepath + thisYear + "\Milk_total_History\" + currMonth + "\"
            If Not Directory.Exists(folderPath) Then
                Directory.CreateDirectory(folderPath)
            End If

            Dim regDate As Date = Date.Now()
            Dim strDate As String = regDate.ToString("ddMMMyyyy")
            Dim filenames As String = name + strDate + ".pdf"

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

            Dim myid As Paragraph = New Paragraph("")
            Dim myname As Paragraph = New Paragraph("")
            If Not (id = "" OrElse name = "") Then
                myid = New Paragraph("ID: " + id)
                myid.Alignment = Element.ALIGN_LEFT
                myid.IndentationLeft = 40
                myname = New Paragraph("Name: " + name)
                myname.Alignment = Element.ALIGN_LEFT
                myname.IndentationLeft = 40
            End If

            Dim p As Paragraph = New Paragraph("Signature")
            p.Alignment = Element.ALIGN_RIGHT
            p.IndentationRight = 55
            Dim mytotal As Paragraph = New Paragraph("Total:")
            mytotal.Alignment = Element.ALIGN_LEFT
            mytotal.IndentationLeft = 40
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
                'Dim pdfDoc As New Document(PageSize.A2, 10.0F, 10.0F, 10.0F, 0.0F)
                Dim pdfDoc As New Document(PageSize.A4.Rotate, 20.0F, 20.0F, 20.0F, 20.0F)
                PdfWriter.GetInstance(pdfDoc, Stream)
                pdfDoc.Open()
                pdfDoc.Add(New Paragraph(" "))
                pdfDoc.Add(New Paragraph(title))
                pdfDoc.Add(New Paragraph(address))
                pdfDoc.Add(New Paragraph(phone))
                pdfDoc.Add(New Paragraph(myid))
                pdfDoc.Add(New Paragraph(myname))
                pdfDoc.Add(New Paragraph(currdate))
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

        Catch ex As Exception
            MsgBox(ex.ToString())
        End Try
    End Sub

    Private Sub printData3(datagridview As DataGridView, id As String, name As String, currMonth As String, ByVal ParamArray mywidth As Integer())
        Try
            'get data from db'
            Dim companyName, companyAddress, companyPhone, filepath As String
            companyName = selectidsql("sources", "source", "10")
            companyAddress = selectidsql("sources", "source", "11")
            companyPhone = selectidsql("sources", "source", "12")
            filepath = selectidsql("sources", "source", "1")
            filepath = filepath + "\"
            'end of get data from db

            Dim pdfTable As New PdfPTable(datagridview.ColumnCount)
            pdfTable.DefaultCell.Padding = 7
            pdfTable.WidthPercentage = 90
            pdfTable.HorizontalAlignment = Element.ALIGN_CENTER
            pdfTable.DefaultCell.BorderWidth = 1
            pdfTable.SetWidths(mywidth)

            'Adding Header row
            For Each column As DataGridViewColumn In datagridview.Columns
                Dim cell As New PdfPCell(New Phrase(column.HeaderText))
                cell.BackgroundColor = New iTextSharp.text.BaseColor(240, 240, 240)
                pdfTable.AddCell(cell)
            Next

            'Adding DataRow
            For Each row As DataGridViewRow In datagridview.Rows
                For Each cell As DataGridViewCell In row.Cells
                    If Not cell.Value = "" Then
                        pdfTable.AddCell(cell.Value.ToString())
                    End If
                Next
            Next

            'Saving to PDF
            Dim folderPath As String = filepath + thisYear + "\Shareholders\" + currMonth + "\"
            If Not Directory.Exists(folderPath) Then
                Directory.CreateDirectory(folderPath)
            End If

            Dim regDate As Date = Date.Now()
            Dim strDate As String = regDate.ToString("ddMMMyyyy")
            Dim filenames As String = name + strDate + ".pdf"

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

            Dim myid As Paragraph = New Paragraph("")
            Dim myname As Paragraph = New Paragraph("")
            If Not (id = "" OrElse name = "") Then
                myid = New Paragraph("ID: " + id)
                myid.Alignment = Element.ALIGN_LEFT
                myid.IndentationLeft = 40
                myname = New Paragraph("Name: " + name)
                myname.Alignment = Element.ALIGN_LEFT
                myname.IndentationLeft = 40
            End If

            Dim p As Paragraph = New Paragraph("Signature")
            p.Alignment = Element.ALIGN_RIGHT
            p.IndentationRight = 55
            Dim mytotal As Paragraph = New Paragraph("Total:")
            mytotal.Alignment = Element.ALIGN_LEFT
            mytotal.IndentationLeft = 40
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
                'Dim pdfDoc As New Document(PageSize.A2, 10.0F, 10.0F, 10.0F, 0.0F)
                Dim pdfDoc As New Document(PageSize.A4.Rotate, 20.0F, 20.0F, 20.0F, 20.0F)
                PdfWriter.GetInstance(pdfDoc, Stream)
                pdfDoc.Open()
                pdfDoc.Add(New Paragraph(" "))
                pdfDoc.Add(New Paragraph(title))
                pdfDoc.Add(New Paragraph(address))
                pdfDoc.Add(New Paragraph(phone))
                pdfDoc.Add(New Paragraph(myid))
                pdfDoc.Add(New Paragraph(myname))
                pdfDoc.Add(New Paragraph(currdate))
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

        Catch ex As Exception
            MsgBox(ex.ToString())
        End Try
    End Sub


End Class