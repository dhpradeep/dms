Imports System.Data.OleDb
Public Class storeMilk
    Public Property Islistbox As Object
    Public checkvalue As Double = 0.0
    Public entryDate As String = System.DateTime.Now.ToString("yyyy/MM/dd")
    Private Sub storeMilk_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Label5.Visible = False
        ComboBox1.Visible = False

        Dim hours As Integer = DateTime.Now.Hour
        If hours > 12 Then
            Label5.Text = "PM"
            RadioButton1.Checked = False
            RadioButton2.Checked = True
        Else
            Label5.Text = "AM"
            RadioButton1.Checked = True
            RadioButton2.Checked = False
        End If

        'parent form details'
        If Me.Owner.Name = "HomePage" Then
            isIds.Text = ""
            isIds.Select()
        Else
            isMilk.Select()
        End If

        isMilk.Text = ""
        isFat.Text = ""
        isLacto.Text = ""

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
            'MsgBox(ex.ToString())
        End Try

    End Sub

    Private Sub TextBox2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles isMilk.KeyPress
        e.Handled = Not (Char.IsDigit(e.KeyChar) Or e.KeyChar = "." Or Asc(e.KeyChar) = 8)
    End Sub

    Private Sub TextBox3_KeyPress(sender As Object, e As KeyPressEventArgs) Handles isFat.KeyPress
        TextBox2_KeyPress(e, e)
    End Sub

    Private Sub TextBox4_KeyPress(sender As Object, e As KeyPressEventArgs) Handles isLacto.KeyPress
        TextBox2_KeyPress(e, e)
    End Sub
    Public result As Double = 0.0
    Private Sub milkStore_Click(sender As Object, e As EventArgs) Handles milkStore.Click
        If isMilk.Text = "" Then
            printmessage("Please Insert Milk.", "Error", "Try again")
            Return
        ElseIf Not Double.TryParse(isMilk.Text, checkValue) Then
            printmessage("Milk must be numeric.", "Error", "Try again")
            isMilk.Select()
            Return
        ElseIf (Not isfat.Text = "" And Not Double.TryParse(isFat.Text, checkValue)) Then
            printmessage("Fat must be numeric.", "Error", "Try again")
            isFat.Select()
            Return
        ElseIf (Not isLacto.text = "" And Not Double.TryParse(isLacto.Text, checkValue)) Then
            printmessage("Lacto must be numeric.", "Error", "Try again")
            isLacto.Select()
            Return
        ElseIf isFat.Text = "" Then
            If isLacto.Text = "" Then
                'you only have to insert milk (not fat and lacto)
                storemilkonly()
                storeMilk_Load(e, e)
            Else
                'you have to insert milk and lacto (not fat)
                storemilkandlacto()
                storeMilk_Load(e, e)
            End If
        ElseIf isLacto.Text = "" Then
            If isFat.Text = "" Then
                'you only have to insert milk (not fat and lacto)
                storemilkonly()
                storeMilk_Load(e, e)
            Else
                'you have to insert milk and fat (not lacto)
                storemilkandfat()
                storeMilk_Load(e, e)
            End If
        Else
            'yo have to insert milk and fat and lacto
            storemilkandfatandlacto()
            storeMilk_Load(e, e)
        End If
    End Sub

    Private Sub storemilkonly()
        Try
            Dim shifts As String
            If RadioButton1.Checked = True Then
                shifts = "Morning"
            Else
                shifts = "Evening"
            End If

            Using con As OleDbConnection = New OleDbConnection(strCon)
                Using cmd = con.CreateCommand()
                    cmd.CommandText = "INSERT INTO detailsFarmer(farmers_ID,Entry_date,Entry_Shift,milk_qty,fat_qty,lacto_qty)VALUES(@id,@edate,@eshift,@milk,@fat,@lacto)"
                    cmd.Parameters.AddWithValue("@id", isIds.Text)
                    cmd.Parameters.AddWithValue("@edate", entryDate)
                    cmd.Parameters.AddWithValue("@eshift", shifts)
                    cmd.Parameters.AddWithValue("@milk", isMilk.Text)
                    cmd.Parameters.AddWithValue("@fat", "-")
                    cmd.Parameters.AddWithValue("@lacto", "-")
                    'cmd.Connection = connection
                    con.Open()
                    cmd.ExecuteNonQuery()
                    con.Close()
                End Using
            End Using

            printmessage("Successfully stored milk.", "Info", "Success")
        Catch ex As Exception
            printmessage("Farmer not found.", "Error", "Try again")
            'MsgBox(ex.ToString())
        End Try
    End Sub

    Private Sub storemilkandlacto()
        Try
            Dim shifts As String
            If RadioButton1.Checked = True Then
                shifts = "Morning"
            Else
                shifts = "Evening"
            End If

            Using con As OleDbConnection = New OleDbConnection(strCon)
                Using cmd = con.CreateCommand()
                    cmd.CommandText = "INSERT INTO detailsFarmer(farmers_ID,Entry_date,Entry_Shift,milk_qty,fat_qty,lacto_qty)VALUES(@id,@edate,@eshift,@milk,@fat,@lacto)"
                    cmd.Parameters.AddWithValue("@id", isIds.Text)
                    cmd.Parameters.AddWithValue("@edate", entryDate)
                    cmd.Parameters.AddWithValue("@eshift", shifts)
                    cmd.Parameters.AddWithValue("@milk", isMilk.Text)
                    cmd.Parameters.AddWithValue("@fat", "-")
                    cmd.Parameters.AddWithValue("@lacto", isLacto.Text)
                    con.Open()
                    cmd.ExecuteNonQuery()
                    con.Close()
                End Using
            End Using
            printmessage("Successfully stored milk and lacto.", "Info", "Success")
        Catch ex As Exception
            printmessage("Farmer not found.", "Error", "Try again")
        End Try
    End Sub

    Private Sub storemilkandfat()
        Try
            Dim shifts As String
            If RadioButton1.Checked = True Then
                shifts = "Morning"
            Else
                shifts = "Evening"
            End If

            Using con As OleDbConnection = New OleDbConnection(strCon)
                Using cmd = con.CreateCommand()
                    cmd.CommandText = "INSERT INTO detailsFarmer(farmers_ID,Entry_date,Entry_Shift,milk_qty,fat_qty,lacto_qty)VALUES(@id,@edate,@eshift,@milk,@fat,@lacto)"
                    cmd.Parameters.AddWithValue("@id", isIds.Text)
                    cmd.Parameters.AddWithValue("@edate", entryDate)
                    cmd.Parameters.AddWithValue("@eshift", shifts)
                    cmd.Parameters.AddWithValue("@milk", isMilk.Text)
                    cmd.Parameters.AddWithValue("@fat", isFat.Text)
                    cmd.Parameters.AddWithValue("@lacto", "-")
                    con.Open()
                    cmd.ExecuteNonQuery()
                    cmd.Dispose()
                    con.Close()
                End Using
            End Using
            printmessage("Successfully stored milk and fat.", "Info", "Success")
        Catch ex As Exception
            printmessage("Farmer not found.", "Error", "Try again")
        End Try
    End Sub

    Private Sub storemilkandfatandlacto()
        Try
            Dim shifts As String
            If RadioButton1.Checked = True Then
                shifts = "Morning"
            Else
                shifts = "Evening"
            End If
            Using con As OleDbConnection = New OleDbConnection(strCon)
                Using cmd = con.CreateCommand()
                    cmd.CommandText = "INSERT INTO detailsFarmer(farmers_ID,Entry_date,Entry_Shift,milk_qty,fat_qty,lacto_qty)VALUES(@id,@edate,@eshift,@milk,@fat,@lacto)"
                    cmd.Parameters.AddWithValue("@id", isIds.Text)
                    cmd.Parameters.AddWithValue("@edate", entryDate)
                    cmd.Parameters.AddWithValue("@eshift", shifts)
                    cmd.Parameters.AddWithValue("@milk", isMilk.Text)
                    cmd.Parameters.AddWithValue("@fat", isFat.Text)
                    cmd.Parameters.AddWithValue("@lacto", isLacto.Text)
                    con.Open()
                    cmd.ExecuteNonQuery()
                    con.Close()
                End Using
            End Using
            printmessage("Successfully stored milk, lacto and fat.", "Info", "Success")
        Catch ex As Exception
            printmessage("Farmer not found.", "Error", "Try again")
        End Try
    End Sub

    Private Sub storeMilk_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        detailsForm.Close()
        Me.Close()
    End Sub

    Private Sub isIds_TextChanged(sender As Object, e As EventArgs) Handles isIds.TextChanged
        Try
            If isIds.Text = "" Then
                'Test if the textbox is null, then reset the grid.
                sql = "SELECT * from farmers ORDER BY ID"
                cmd = New OleDbCommand(sql, connection)
                datast = New DataSet
                adapter = New OleDbDataAdapter
                adapter.Fill(datast, "farmers")
                Me.ListBox1.DataSource = datast.Tables("farmers")
                Me.ListBox1.DisplayMember = "mName"
            Else
                adapter = New OleDbDataAdapter("SELECT * from farmers where ID like '%" & isIds.Text & "%' ORDER BY ID", connection)
                Dim adapter1 As New OleDbDataAdapter
                adapter1 = New OleDbDataAdapter("SELECT * from farmers where ID= " & isIds.Text & " ORDER BY ID", connection)
                datast = New DataSet
                Dim datast1 As New DataSet
                adapter.Fill(datast, "farmers")
                adapter1.Fill(datast1, "farmers")
                Me.ListBox1.DataSource = datast.Tables("farmers")
                Me.ListBox1.DisplayMember = "mName"

                ComboBox1.DataSource = datast1.Tables("farmers")
                ComboBox1.ValueMember = "hasShare"
                ComboBox1.DisplayMember = "hasShare"
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ListBox1_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles ListBox1.MouseDoubleClick
        Dim name As Integer
        name = ListBox1.SelectedItem(0).ToString()
        isIds.Text = name
        isMilk.Select()
    End Sub

    Private Sub ListBox1_MouseClick(sender As Object, e As MouseEventArgs) Handles ListBox1.MouseClick
        Dim name As Integer
        name = ListBox1.SelectedItem(0).ToString()
        isIds.Text = name
        isMilk.Select()
    End Sub

    Private Sub isIds_KeyPress(sender As Object, e As KeyPressEventArgs) Handles isIds.KeyPress
        e.Handled = Not (Char.IsDigit(e.KeyChar) Or Asc(e.KeyChar) = 8)
    End Sub

    Private Sub storeMilk_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then Me.Close()
    End Sub
End Class