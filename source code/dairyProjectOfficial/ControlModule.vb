Option Explicit On
'Option Strict On
Imports System.Data
Imports System.Data.OleDb
Imports System.Globalization
Imports System.Text.RegularExpressions
Imports System.Text
Imports System.Configuration
Imports System.Security.Cryptography
Imports System.IO
Imports iTextSharp.text.pdf
Imports iTextSharp.text
Module controlModule

    'connect to database files
    Public strCon As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & Application.StartupPath & "\mydatabase1.mdb;Jet OLEDB:Database Password=login1234"
    Public companyCon As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & Application.StartupPath & "\mydatabase2.mdb;Jet OLEDB:Database Password=login1234"
    Public connection As OleDb.OleDbConnection
    Public connection1 As OleDb.OleDbConnection
    Public cmd As OleDb.OleDbCommand
    Public cmd1 As OleDb.OleDbCommand
    Public cmd2 As OleDb.OleDbCommand
    Public cmd3 As OleDb.OleDbCommand
    Public cmd4 As OleDb.OleDbCommand
    Public cmd5 As OleDb.OleDbCommand
    Public cmd6 As OleDb.OleDbCommand
    Public datast As DataSet
    Public datast1 As DataSet
    Public datast2 As DataSet
    Public datasts As DataSet
    Public adapter As OleDbDataAdapter
    Public adapter1 As OleDbDataAdapter
    Public adapter2 As OleDbDataAdapter
    Public adapters As OleDbDataAdapter
    Public bindingsrc As BindingSource
    Public bindingsrc1 As BindingSource
    Public bindingsrc2 As BindingSource
    Public bindingsrcs As BindingSource
    Public reader As OleDbDataReader
    Public reader1 As OleDbDataReader
    Public sql As String
    Public dt As New DataTable
    Public ds As New DataSet

    'Public dt As DateTime = DateTime.Now
    'Public dtfInfo As DateTimeFormatInfo = DateTimeFormatInfo.InvariantInfo
    'Public myDay As String = "dd"
    'Public myMonth As String = "MMM"
    'Public myYear As String = "yyyy"

    'Public dtID As String
    'Public accType As Integer
    'Public IsFind As Boolean = False


    Public Function Encrypt(clearText As String) As String
        Dim EncryptionKey As String = "MAKV2SPBNI99212"
        Dim clearBytes As Byte() = Encoding.Unicode.GetBytes(clearText)
        Using encryptor As Aes = Aes.Create()
            Dim pdb As New Rfc2898DeriveBytes(EncryptionKey, New Byte() {&H49, &H76, &H61, &H6E, &H20, &H4D,
             &H65, &H64, &H76, &H65, &H64, &H65,
             &H76})
            encryptor.Key = pdb.GetBytes(32)
            encryptor.IV = pdb.GetBytes(16)
            Using ms As New MemoryStream()
                Using cs As New CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write)
                    cs.Write(clearBytes, 0, clearBytes.Length)
                    cs.Close()
                End Using
                clearText = Convert.ToBase64String(ms.ToArray())
            End Using
        End Using
        Return clearText
    End Function

    Public Function Decrypt(cipherText As String) As String
        Dim EncryptionKey As String = "MAKV2SPBNI99212"
        Dim cipherBytes As Byte() = Convert.FromBase64String(cipherText)
        Using encryptor As Aes = Aes.Create()
            Dim pdb As New Rfc2898DeriveBytes(EncryptionKey, New Byte() {&H49, &H76, &H61, &H6E, &H20, &H4D,
             &H65, &H64, &H76, &H65, &H64, &H65,
             &H76})
            encryptor.Key = pdb.GetBytes(32)
            encryptor.IV = pdb.GetBytes(16)
            Using ms As New MemoryStream()
                Using cs As New CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write)
                    cs.Write(cipherBytes, 0, cipherBytes.Length)
                    cs.Close()
                End Using
                cipherText = Encoding.Unicode.GetString(ms.ToArray())
            End Using
        End Using
        Return cipherText
    End Function

    Public Function active_new_license(current_code As String) As String
        Dim activate_code() As String = {
           "qIU3-dZ63-QGqU-nC5N", "zd5p-dDU5-NPL2-Ho4Z", "rK5A-YyO2-sFI3-M0Dn",
           "73GL-xPdp-8M0R-lg1D", "WnT1-gQ5i-dRct-4yho", "yOPv-UIpH-s3LZ-S7gL",
           "EHyg-qY28-R7qj-gVb8", "ur0U-YFiq-LScV-uQH4", "Qw4R-u1t3-llj7-ZpG3",
           "m77T-c6RJ-GGh3-uIKJ", "8XiO-EpKQ-gUvk-pMD7", "F2FZ-GFER-3niD-W6cu",
           "Vh1i-n0sM-M623-uLm2", "Vso1-F9ba-hHWP-Chr0", "9Oyg-sXLa-g4ZC-ieSd",
           "Fc0T-mcJ0-jhzB-2elM", "oHif-XNhM-KdGJ-nPXI", "vwoq-rRir-wTcY-ylP0",
           "Hbmt-hOXc-9MVU-eH76", "A3VD-m9ir-k3Mt-ww3r", "4Ww2-bVah-IGjW-G8Kb",
           "CJmP-RNT6-lpa3-9vPx", "o9kv-q3q3-nL0f-Tqgj", "PCfp-40ex-TaJ8-Kf6y",
           "45Aw-cf3C-KYbg-xNE9", "veo5-YxpH-ZCfJ-ThHZ", "wEW7-Rk95-ZCSn-kigD",
           "vHFy-xuCr-1OUo-6umj", "FlKK-qbK9-CHLp-OThj", "UByu-G5eL-k0kf-Pyyt",
           "Ggz1-XLPs-Aa46-bQ9N", "MQNN-xRMf-skOJ-d1cx", "dEkn-Poej-w9GF-9MUF",
           "5077-tsAG-QTNL-GhTx", "WQrC-29WE-kBCP-jSFC", "BHeb-dvjH-Si0b-dddu",
           "Q5Ba-Gd0w-iQTJ-mnVp", "Z3Oz-DUXX-mUlR-ebP8", "kQUd-Rkj4-zCbN-0Vna",
           "RM2M-WOO3-BOcX-iAh4", "uvBn-B5o7-V3qK-NSoN", "6qNc-khXn-PR33-l5oI",
           "JMCA-mBN5-5fBP-llJm", "UJvM-QZk5-897l-0MG5", "M88l-jWoE-41G6-jYkq",
           "DxXc-2Kqw-zGDI-atnP", "BOf2-6KGn-xa0e-mLMM", "XCkd-y90C-IRXK-XUO0",
           "XdU1-D8fL-axFa-teOW", "L3hu-EuPU-1QbJ-py0E"
        }
        If activate_code.Contains(current_code) Then
            Return True
        End If
        Return False
    End Function

    Public Function license_already_exist(code As String) As String
        sql = "SELECT count(*) FROM activation_license WHERE license='" & code & "'"
        cmd = New OleDbCommand(sql, connection)
        If cmd.ExecuteScalar() > 0 Then
            Return True
        End If
        Return False
    End Function

    Public Function selectallsql(tablename As String) As String
        sql = "SELECT * FROM " & tablename & ""
        cmd = New OleDbCommand(sql, connection)
        cmd.ExecuteNonQuery()
        Return 0
    End Function

    Public Function selectidsql(tablename As String, row As String, id As String, Optional ByVal rowname As String = "ID") As String
        sql = "SELECT " & row & " FROM " & tablename & " WHERE " & rowname & "= " & id & ""
        adapter = New OleDbDataAdapter(sql, connection)
        Dim dt As New DataTable
        adapter.Fill(dt)
        Return dt.Rows(0).Item(0)
    End Function

    Public Function selectidsql1(tablename As String, row As String, id As String, Optional ByVal rowname As String = "ID") As String
        sql = "SELECT " & row & " FROM " & tablename & " WHERE " & rowname & "= " & id & ""
        adapter = New OleDbDataAdapter(sql, connection1)
        Dim dt As New DataTable
        adapter.Fill(dt)
        Return dt.Rows(0).Item(0)
    End Function

    Public Function selectanysql(tablename As String, row As String, id As String, Optional ByVal rowname As String = "ID") As String
        sql = "SELECT " & row & " FROM " & tablename & " WHERE " & rowname & "= '" & id & "'"
        adapter = New OleDbDataAdapter(sql, connection1)
        Dim dt As New DataTable
        adapter.Fill(dt)
        Return dt.Rows(0).Item(0)
    End Function

    Public Function checkshareholder(tablename As String, row As String, id As String, Optional ByVal rowname As String = "ID") As String
        sql = "SELECT " & row & " FROM " & tablename & " WHERE " & rowname & "= " & id & ""
        adapter = New OleDbDataAdapter(sql, connection)
        Dim dt As New DataTable
        adapter.Fill(dt)
        Return dt.Rows(0).Item(0)
    End Function

    Public Function getAllMonthData(mygrid As DataGridView, i As Integer, month As String)
        Try
            sql = "SELECT sum(Total) FROM myhistory WHERE ID=" & mygrid.Rows(i).Cells(0).Value &
                               " AND Entry_Date like '%" & month & "%'"
            cmd = New OleDbCommand(sql, connection)
            reader = cmd.ExecuteReader()
            If Not reader.HasRows Then
                Return 0
            ElseIf reader.HasRows Then
                Do While reader.Read
                    Return (reader.Item(0))
                Loop
            End If
        Catch ex As Exception
            Return 0
        End Try
        Return 0
    End Function

    Public Function getAllMonthMilkData(mygrid As DataGridView, i As Integer, month As String)
        Try
            sql = "SELECT sum(Milk) FROM myhistory WHERE ID=" & mygrid.Rows(i).Cells(0).Value &
                               " AND Entry_Date like '%" & month & "%'"
            cmd = New OleDbCommand(sql, connection)
            reader = cmd.ExecuteReader()
            If Not reader.HasRows Then
                Return 0
            ElseIf reader.HasRows Then
                Do While reader.Read
                    Return (reader.Item(0))
                Loop
            End If
        Catch ex As Exception
            Return 0
        End Try
        Return 0
    End Function

    Public Function updateidsql(tablename As String, updaterowname As String, rowvalue As String, id As String, Optional ByVal rowname As String = "ID") As String
        sql = "UPDATE " & tablename & " SET " & updaterowname & " = '" & rowvalue & "' WHERE " & rowname & " = " & id & ""
        cmd = New OleDbCommand(sql, connection)
        Dim result = cmd.ExecuteNonQuery()
        Return result
    End Function

    Public Function selectmultiplesql(tablename As String, row As String, id As String, items As Integer, Optional ByVal rowname As String = "ID") As String
        sql = "SELECT " & row & " FROM " & tablename & " WHERE " & rowname & "= '" & id & "'"
        adapter = New OleDbDataAdapter(sql, connection)
        Dim dt As New DataTable
        adapter.Fill(dt)
        Return dt.Rows(0).Item(items)
    End Function

    Public Function printmessage(message As String, type As String, title As String)
        If type = "Info" Then
            Return MsgBox(message, MsgBoxStyle.Information, Title:=title)
        ElseIf type = "Error" Then
            Return MsgBox(message, MsgBoxStyle.Critical, Title:=title)
        ElseIf type = "Ok" Then
            Return MsgBox(message, MsgBoxStyle.OkOnly, Title:=title)
        End If
        Return 0
    End Function

    Public Function validnumber(e As KeyPressEventArgs) As KeyPressEventHandler
        e.Handled = Not (Char.IsDigit(e.KeyChar) Or Asc(e.KeyChar) = 8)
        Return Nothing
    End Function

    Public Function printData(datagridview As DataGridView, id As String, name As String, ByVal ParamArray mywidth As Integer())
        'get data from db'
        Dim companyName, companyAddress, companyPhone, filepath As String
        companyName = selectidsql("sources", "source", "10")
        companyAddress = selectidsql("sources", "source", "11")
        companyPhone = selectidsql("sources", "source", "12")
        filepath = selectidsql("sources", "source", "1")
        filepath = filepath + "\"
        'end of get data from db

        'Dim mywidth() As Integer = {20, 20, 25, 25, 25, 25, 25}

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



        Return Nothing
    End Function

End Module
