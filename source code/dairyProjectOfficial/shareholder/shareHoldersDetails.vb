Imports System.Data.OleDb
Imports System.IO
Public Class shareHoldersDetails

    Public result As Double = 0.0

    Private Sub shareHoldersDetails_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CheckBox2.Checked = True
        DateTimePicker1.Enabled = False
        idBox.Text = ""

        DateTimePicker1.Format = DateTimePickerFormat.Custom
        DateTimePicker1.CustomFormat = "yyyy/MM/dd"

        TextBox6.Text = ""
        TextBox6.Visible = False
        CheckBox1.Checked = False

        monthlyAmt.Text = 100

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
            'end of main connection

            FILL_ComboBox()

            Dim ImageByte As Byte()
            Dim MemStream As MemoryStream
            cmd1 = New OleDbCommand("SELECT picturefile FROM farmers WHERE ID=" & idBox.Text & "", connection)
            Try
                ImageByte = cmd1.ExecuteScalar()
                MemStream = New MemoryStream(ImageByte)
                imagename.Image = System.Drawing.Image.FromStream(MemStream)
            Catch ex As Exception
                imagename.Image = Nothing
            End Try

        Catch ex As Exception
            MsgBox(ex.ToString())
            'MsgBox("Problem on connecting to database.(Error code: 300)", MsgBoxStyle.Critical, Title:="Try again")
        End Try


    End Sub

    Private Sub FILL_ComboBox()
        sql = "select ID from farmers where hasShare='YES' ORDER BY ID"
        cmd = New OleDbCommand(sql, connection)
        adapter = New OleDbDataAdapter(cmd)
        datast = New DataSet()
        adapter.Fill(datast, "farmers")
        With idBox
            .DataSource = datast.Tables(0)
            .ValueMember = "ID"
            .DisplayMember = "ID"
            .AutoCompleteMode = AutoCompleteMode.Suggest
            .AutoCompleteSource = AutoCompleteSource.ListItems
        End With
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim joindate As String = System.DateTime.Now.ToString("yyyy/MM/dd")
        If monthlyAmt.Text = "" Then
            printmessage("Please fill out all forms.", "Error", "Try again")
            monthlyAmt.Select()
        ElseIf Not Double.TryParse(monthlyAmt.Text, result) Then
            printmessage("Amount must be in double", "Error", "Try again")
            monthlyAmt.Select()
        Else
            If CheckBox1.Checked = True Then
                If TextBox6.Text = "" Then
                    printmessage("Please insert fine.", "Error", "Try again")
                    TextBox6.Select()
                    Return
                ElseIf Not Double.TryParse(TextBox6.Text, result) Then
                    printmessage("Amount must be in double", "Error", "Try again")
                    TextBox6.Select()
                    Return
                End If
                sql = "INSERT INTO incomestatement(insertdate,statement,quantity,amount)VALUES('" & joindate & "','" & nameList.Text & " pay fine.','none','" & TextBox6.Text & "')"
                cmd = New OleDbCommand(sql, connection1)
                cmd.ExecuteNonQuery()
            End If
            Try
                Dim pr_amount, updated_amount As Double
                pr_amount = selectidsql1("previousbalance", "shareholder_amount", idBox.Text, "farmer_ID")
                updated_amount = pr_amount + CDbl(monthlyAmt.Text)

                If CheckBox2.Checked = True Then
                    sql = "INSERT INTO shareHolders(f_ID ,saving_money, saving_date) VALUES(" & idBox.Text & ",'" & monthlyAmt.Text & "', '" & joindate & "')"
                Else
                    sql = "INSERT INTO shareHolders(f_ID ,saving_money, saving_date) VALUES(" & idBox.Text & ",'" & monthlyAmt.Text & "', '" & DateTimePicker1.Text & "')"
                End If
                cmd = New OleDbCommand(sql, connection)
                Dim result = cmd.ExecuteNonQuery()

                Try
                    Dim sql1 As String
                    sql1 = "UPDATE previousbalance SET shareholder_amount='" & updated_amount & "' WHERE farmer_ID=" & idBox.Text & ""
                    cmd1 = New OleDbCommand(sql1, connection1)
                    cmd1.ExecuteNonQuery()

                    'also update interest amount.
                    Dim interest_amount As Double
                    interest_amount = selectidsql1("previousbalance", "s_interest", idBox.Text, "farmer_ID")
                    'end of get
                    Dim interest_rate, updated_rate, new_rate As Double
                    interest_rate = selectidsql("sources", "source", "13")
                    updated_rate = selectidsql("sources", "source", "16")
                    new_rate = interest_rate - updated_rate
                    Dim up_amount As Double
                    up_amount = (CDbl(updated_amount) * CDbl(new_rate)) / (100 * 12)
                    up_amount = FormatCurrency(up_amount, 2)

                    'update interest amount
                    Using con As OleDbConnection = New OleDbConnection(companyCon)
                        Using cmd = con.CreateCommand()
                            cmd.CommandText = "UPDATE previousbalance SET s_interest='" & up_amount & "' WHERE farmer_ID=" & idBox.Text & ""
                            con.Open()
                            cmd.ExecuteNonQuery()
                            con.Close()
                        End Using
                    End Using
                    'end of updating amount


                Catch ex As Exception
                    'MsgBox(ex.ToString())
                    printmessage("Something went wrong.", "Error", "Try again")
                End Try

                If result = 1 Then
                    ' MsgBox("Data has been save into database.")
                    printmessage("Data has been save into database.", "Info", "Succes")
                    shareHoldersDetails_Load(e, e)
                End If

            Catch ex As Exception
                'MsgBox(ex.ToString())
                'MsgBox("Problem on updating data. (Error 301)", MsgBoxStyle.Critical, Title:="Try again")
            End Try
        End If
    End Sub

    Private Sub nameList_SelectedIndexChanged(sender As Object, e As EventArgs) Handles nameList.SelectedIndexChanged
        Dim ImageByte As Byte()
        Dim MemStream As MemoryStream
        cmd1 = New OleDbCommand("SELECT picturefile FROM farmers WHERE ID=" & idBox.Text & "", connection)
        Try
            ImageByte = cmd1.ExecuteScalar()
            MemStream = New MemoryStream(ImageByte)
            imagename.Image = Image.FromStream(MemStream)
        Catch ex As Exception
            imagename.Image = Nothing
        End Try
    End Sub

    Private Sub monthlyAmt_KeyPress(sender As Object, e As KeyPressEventArgs) Handles monthlyAmt.KeyPress
        e.Handled = Not (Char.IsDigit(e.KeyChar) Or e.KeyChar = "." Or Asc(e.KeyChar) = 8)
    End Sub

    Private Sub shareHoldersDetails_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then Me.Close()
    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged
        If CheckBox2.Checked = True Then
            DateTimePicker1.Enabled = False
        Else
            DateTimePicker1.Enabled = True
        End If
    End Sub

    Private Sub idBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles idBox.SelectedIndexChanged
        Try
            sql = "select mName from farmers where ID=" & idBox.Text & ""
            cmd = New OleDbCommand(sql, connection)
            adapter = New OleDbDataAdapter(cmd)
            datast = New DataSet()
            adapter.Fill(datast, "farmers")
            With nameList
                .DataSource = datast.Tables(0)
                .ValueMember = "mName"
                .DisplayMember = "mName"
                .AutoCompleteMode = AutoCompleteMode.Suggest
                .AutoCompleteSource = AutoCompleteSource.ListItems
            End With
            nameList.Enabled = False
            nameList.BackColor = Color.White
        Catch ex As Exception
            ' MsgBox(ex.ToString())
            'MsgBox("Problem on updating data. (Error 301)", MsgBoxStyle.Critical, Title:="Try again")
        End Try
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            TextBox6.Visible = True
        Else
            TextBox6.Visible = False
        End If
    End Sub

    Private Sub TextBox6_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox6.KeyPress
        e.Handled = Not (Char.IsDigit(e.KeyChar) Or e.KeyChar = "." Or Asc(e.KeyChar) = 8)
    End Sub
End Class