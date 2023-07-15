Imports System.Data.OleDb
Public Class company_statement_income

    Public result As Double = 0.0

    Private Sub company_statement_income_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ComboBox1.Select()
        TextBox2.Text = ""
        DateTimePicker1.Enabled = True
        CheckBox1.Checked = False
        DateTimePicker1.Format = DateTimePickerFormat.Custom
        DateTimePicker1.CustomFormat = "yyyy/MM/dd"
        Try
            connection1 = New OleDbConnection
            With connection1
                If .State = ConnectionState.Closed Then
                    .ConnectionString = companyCon
                    .Open()
                End If
            End With

            FILL_ComboBox1()

        Catch ex As Exception
            'MsgBox(ex.ToString())
        End Try
    End Sub

    Private Sub FILL_ComboBox1()
        Try
            sql = "select particulars  FROM cashflow WHERE maintype='Income'"
            cmd = New OleDbCommand(sql, connection1)
            adapter = New OleDbDataAdapter(cmd)
            datast = New DataSet()
            adapter.Fill(datast, "cashflow")
            With ComboBox1
                .DataSource = datast.Tables(0)
                .ValueMember = "particulars"
                .DisplayMember = "particulars"
            End With
        Catch ex As Exception
            MsgBox("No particulars found. " + ex.ToString())
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If TextBox2.Text = "" Then
            printmessage("Please field out all data.", "Error", "Try again")
            TextBox2.Select()
        ElseIf ComboBox1.Text = "" Then
            printmessage("Please add some items first", "Error", "Try again")
            ComboBox1.Select()
        ElseIf Not Double.TryParse(TextBox2.Text, result) Then
            printmessage("Amount must be in double", "Error", "Try again")
            TextBox2.Select()
        Else
            If CheckBox1.Checked = True Then
                Dim insertDate As String = System.DateTime.Now.ToString("yyyy/MM/dd")
                'sql command to insert with current date
                sql = "INSERT INTO incomestatement(insertdate,statement,quantity,amount)VALUES('" & insertDate & "','" & ComboBox1.Text & "','" & TextBox1.Text & "','" & TextBox2.Text & "')"
            Else
                'sql command to insert with given date
                sql = "INSERT INTO incomestatement(insertdate,statement,quantity,amount)VALUES('" & DateTimePicker1.Text & "','" & ComboBox1.Text & "','" & TextBox1.Text & "','" & TextBox2.Text & "')"
            End If
            cmd = New OleDbCommand(sql, connection1)
            cmd.ExecuteNonQuery()
            printmessage("Successfully insert new statement.", "Info", "Success")
            company_statement_income_Load(e, e)
        End If
    End Sub

    Private Sub TextBox2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox2.KeyPress
        e.Handled = Not (Char.IsDigit(e.KeyChar) Or e.KeyChar = "." Or Asc(e.KeyChar) = 8 Or Asc(e.KeyChar) = 13)
        If Asc(e.KeyChar) = 13 Then
            Button1_Click(e, e)
        End If
    End Sub

    Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs)
        If Asc(e.KeyChar) = 27 Then
            Me.Close()
        End If
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            DateTimePicker1.Enabled = False
        Else
            DateTimePicker1.Enabled = True
        End If
    End Sub

    Private Sub company_statement_income_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then Me.Close()
    End Sub
End Class