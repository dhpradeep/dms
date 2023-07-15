Imports System.Data.OleDb
Imports System.IO
Imports calculation.MyFunctions
Imports System.Text.RegularExpressions
Public Class HomePage
    Public checkvalue As Double = 0.0
    Public backuptime As Integer
    Public backuplocks As String
    Public currentDate As String = System.DateTime.Now.ToString("dd/MM/yyyy")
    Private Sub HomePage_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Me.FormBorderStyle = Windows.Forms.FormBorderStyle.None
        'Me.Location = New Point(0, 0)
        'Me.Size = SystemInformation.PrimaryMonitorSize

        'change grid view font
        Me.DataGridView1.Font = New Font("Calibri", 10, FontStyle.Regular)

        'myNote edition
        myNote.Enabled = False

        saveBtn.Visible = False

        ' copyright text
        Label1.Text = Chr(169)
        Timer1.Enabled = True

        backuplocks = LoginForm.backuplock
        If Not backuplocks = "lock" Then
            Timer2.Start()
        End If
        TextBox2.Visible = False ' false default

        TextBox1.Select()
        If searchItem.Items.Count > 0 Then
            searchItem.SelectedIndex = 1    ' The first item has index 0 '
        End If

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

            Dim todaydate As String = System.DateTime.Now.ToString("yyyy/MM/dd")
            Dim yesterdaydate As String = System.DateTime.Now.AddDays(-1).ToString("yyyy/MM/dd")

            Dim sql2 As String
            sql2 = "SELECT milk_qty, fat_qty, lacto_qty FROM detailsFarmer WHERE (Entry_date='" & todaydate _
                & "' AND Entry_Shift='Morning') OR (Entry_date='" & yesterdaydate & "' AND Entry_Shift='Evening')"
            adapters = New OleDbDataAdapter(sql2, connection)
            Dim dt As New DataTable
            adapters.Fill(dt)
            Dim countmilk, countfat, countlacto, avgfat, avglacto As Double
            avgfat = 0
            avglacto = 0
            DataGridView1.DataSource = dt
            For i As Integer = 0 To dt.Rows.Count - 1
                If Double.TryParse(dt.Rows(i).Item(0), checkvalue) Then
                    countmilk += CDbl(Val(dt.Rows(i).Item(0)))
                End If
                If Double.TryParse(dt.Rows(i).Item(1), checkvalue) Then
                    countfat += CDbl(Val(dt.Rows(i).Item(1)))
                    avgfat += 1
                End If
                If Double.TryParse(dt.Rows(i).Item(2), checkvalue) Then
                    countlacto += CDbl(Val(dt.Rows(i).Item(2)))
                    avglacto += 1
                End If
            Next

            countfat = countfat / avgfat
            countlacto = countlacto / avglacto
            'todaymilk.Text = dt.Rows(0).Item(0)
            todaymilk.Text = countmilk.ToString + " ltr"
            todayfat.Text = countfat.ToString("N2")
            todaylacto.Text = countlacto.ToString("N2")

            myNote.Text = selectidsql("mynotes", "mynote", "1")

            backuptime = selectidsql("sources", "source", "3")

            'sql = "SELECT * FROM farmers"
            sql = "SELECT ID,mName,mAddress,mContact,mEmail,hasShare,joinDate,cows FROM farmers ORDER BY ID"
            cmd = New OleDbCommand(sql, connection)
            adapter = New OleDbDataAdapter(cmd)
            datast = New DataSet()
            adapter.Fill(datast, "farmers")
            datast.Tables(0).DefaultView.Sort = "ID"
            bindingsrc = New BindingSource()

            With DataGridView1
                .DataSource = datast.Tables("farmers")
                .RowHeadersVisible = False
                .Columns(0).HeaderCell.Value = "ID"
                .Columns(1).HeaderCell.Value = "Name"
                .Columns(2).HeaderCell.Value = "Address"
                .Columns(3).HeaderCell.Value = "Contact"
                .Columns(4).HeaderCell.Value = "Email"
                .Columns(5).HeaderCell.Value = "Shareholders"
                .Columns(6).HeaderCell.Value = "Join Date"
                .Columns(7).HeaderCell.Value = "Cows"
                .Columns(0).SortMode = DataGridViewColumnSortMode.NotSortable
                .Columns(1).SortMode = DataGridViewColumnSortMode.NotSortable
                .Columns(2).SortMode = DataGridViewColumnSortMode.NotSortable
                .Columns(3).SortMode = DataGridViewColumnSortMode.NotSortable
                .Columns(4).SortMode = DataGridViewColumnSortMode.NotSortable
                .Columns(5).SortMode = DataGridViewColumnSortMode.NotSortable
                .Columns(6).SortMode = DataGridViewColumnSortMode.NotSortable
                .Columns(7).SortMode = DataGridViewColumnSortMode.NotSortable
                .AllowUserToAddRows = False
                .AllowUserToResizeRows = False
                .AllowUserToOrderColumns = False
                .AllowUserToResizeColumns = True
            End With

            ' print `Add farmer first` text
            Label10.Visible = False
            If Not DataGridView1.Rows.Count >= 1 Then
                DataGridView1.Visible = False
                Label10.Visible = True
            Else
                DataGridView1.Visible = True
                Label10.Visible = False
            End If
        Catch ex As Exception
            ' MsgBox(ex.ToString())
            MsgBox("Problem on connecting to database.(Error code: 300)", MsgBoxStyle.Exclamation, Title:="Bye")
        End Try

        Dim test As New calculation.MyFunctions
        'test.connectiontest()

        ' check the expiry date and if date is 
        ' less than 30 than add red color with remaining days
        Dim expirydate As String = selectidsql("superadmin", "locking", "3")
        Dim expirydate1 As Date = Date.ParseExact(expirydate, "dd/MM/yyyy",
                   System.Globalization.DateTimeFormatInfo.InvariantInfo)
        Dim currentdate1 As Date = Date.ParseExact(currentDate, "dd/MM/yyyy",
                   System.Globalization.DateTimeFormatInfo.InvariantInfo)
        Dim span = expirydate1 - currentdate1
        If span.TotalDays < 30 Then
            For Each mainMenu As ToolStripMenuItem In MenuStrip1.Items
                If mainMenu.Name = "HelpToolStripMenuItem" Then
                    For Each item As ToolStripItem In mainMenu.DropDownItems
                        If item.Name = "LicenseToolStripMenuItem" Then
                            item.Text = "Add license (" + span.TotalDays.ToString + ")"
                            item.ForeColor = Color.Red
                        End If
                    Next
                End If
            Next
        End If

    End Sub

    Public Sub FilterData(valueToSearch As String, valueType As String)
        If valueType = "Name" Then
            valueType = "mName"
        ElseIf valueType = "Address" Then
            valueType = "mAddress"
        End If
        Dim searchQuery As String = "SELECT ID,mName,mAddress,mContact,mEmail,hasShare,joinDate,cows From farmers WHERE " & valueType & " like '%" & valueToSearch & "%' ORDER BY ID"

        Dim command As New OleDbCommand(searchQuery, connection)
        Dim adapter As New OleDbDataAdapter(command)
        Dim table As New DataTable()
        adapter.Fill(table)
        DataGridView1.DataSource = table

    End Sub

    'for navigate url
    Private Sub NavigateWebURL(ByVal URL As String, Optional browser As String = "default")

        If Not (browser = "default") Then
            Try
                '// try set browser if there was an error (browser not installed)
                Process.Start(browser, URL)
            Catch ex As Exception
                '// use default browser
                Process.Start(URL)
            End Try

        Else
            '// use default browser
            Process.Start(URL)

        End If

    End Sub

    Private Sub SignOutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SignOutToolStripMenuItem.Click
        Try
            With connection
                If .State = ConnectionState.Open Then
                    .ConnectionString = strCon
                    .Close()
                End If
            End With
            Application.Restart()
        Catch ex As Exception
            Application.Restart()
        End Try
    End Sub

    Private Sub ExitToolStripMenuItem_Click_1(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        Try
            With connection
                If .State = ConnectionState.Open Then
                    .ConnectionString = strCon
                    .Close()
                End If
            End With
            Application.Exit()
        Catch ex As Exception
            Application.Exit()
        End Try
    End Sub

    Private Sub addMember_Click(sender As Object, e As EventArgs)
        addFarmers.ShowDialog()
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        currTime.Text = Date.Now.ToString("hh:mm:ss")
    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click
        NavigateWebURL("http://www.pinesofts.com", "default")
    End Sub

    Private Sub Label2_MouseHover(sender As Object, e As EventArgs) Handles Label2.MouseHover
        Label2.Font = New Font(Label2.Font, FontStyle.Bold)
    End Sub

    Private Sub Label2_MouseLeave(sender As Object, e As EventArgs) Handles Label2.MouseLeave
        Label2.Font = New Font(Label2.Font, FontStyle.Regular)
    End Sub

    Private Sub AddFarmerToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AddFarmerToolStripMenuItem.Click
        addFarmers.ShowDialog()
    End Sub

    Private Sub ChangePasswordToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ChangePasswordToolStripMenuItem.Click
        changePassword.ShowDialog()
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        FilterData(TextBox1.Text, searchItem.Text)
    End Sub

    Private Sub DataGridView1_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellDoubleClick
        If DataGridView1.RowCount > 0 Then
            detailsForm.isid.Text = DataGridView1.CurrentRow.Cells(0).Value.ToString()
            detailsForm.mName.Text = DataGridView1.CurrentRow.Cells(1).Value.ToString()
            detailsForm.hasShares = DataGridView1.CurrentRow.Cells(5).Value.ToString()
            detailsForm.ShowDialog()
        End If
    End Sub

    Private Sub AddCustomerToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AddCustomerToolStripMenuItem.Click
        shareHoldersDetails.ShowDialog()
    End Sub

    Private Sub HomePage_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        HomePage_Load(e, e)
    End Sub

    Private Sub milkStore_Click(sender As Object, e As EventArgs) Handles milkStore.Click
        storeMilk.ShowDialog(Me)
    End Sub

    Private Sub editBtn_Click(sender As Object, e As EventArgs) Handles editBtn.Click
        editBtn.Enabled = False
        saveBtn.Visible = True
        myNote.Enabled = True
    End Sub

    Private Sub saveBtn_Click(sender As Object, e As EventArgs) Handles saveBtn.Click
        If myNote.Text = "" Then
            MsgBox("Please write somethings before save.", MsgBoxStyle.Exclamation, Title:="Try again!")
            saveBtn.Visible = True
            editBtn.Enabled = False
            myNote.Enabled = True
        Else
            Try
                Dim result = updateidsql("mynotes", "mynote", myNote.Text, "1")
                If result = 1 Then
                    saveBtn.Visible = False
                    editBtn.Enabled = True
                    myNote.Enabled = False
                End If

            Catch ex As Exception
                'MsgBox(ex.ToString())
                MsgBox("Problem on updating. (Error code: 301)", MsgBoxStyle.Critical, Title:="Try again")
            End Try
        End If
    End Sub

    Private Sub OtherSettingToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OtherSettingToolStripMenuItem.Click
        otherSetting.ShowDialog()
    End Sub


    Public Sub backupData()
        Try
            Dim filepath As String

            filepath = selectidsql("sources", "source", "2")

            ' Dim fileDateTime As String = DateTime.Now.ToString("yyyyMMdd") & "_" & DateTime.Now.ToString("HHmmss")
            Dim filename As String = "\mydatabase1.mdb"
            Dim filename2 As String = "\mydatabase2.mdb"
            If File.Exists(filepath + "" & filename & "") OrElse File.Exists(filepath + "" & filename2 & "") Then
                File.Delete(filepath + "" & filename & "")
                File.Delete(filepath + "" & filename2 & "")
            End If
            File.Copy("mydatabase1.mdb", filepath & "" & filename & "")
            File.Copy("mydatabase2.mdb", filepath & "" & filename2 & "")
        Catch ex As Exception
            ' MsgBox(ex.Message.ToString())
            MsgBox("Problem on backup data. (Error code: 302)", MsgBoxStyle.Critical, Title:="Try again")
        End Try
    End Sub

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        TextBox2.Text = TextBox2.Text + 1
        If TextBox2.Text = backuptime * 60 And otherSetting.CheckBox1.Checked = False Then
            backupData()
            TextBox2.Text = 1
        End If
    End Sub

    Private Sub HomePage_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        Timer2.Stop()
        connection.Close()
        connection.Dispose()
    End Sub

    Private Sub AddUsersToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AddUsersToolStripMenuItem.Click
        addUsers.ShowDialog()
    End Sub

    Private Sub HelpToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles HelpToolStripMenuItem1.Click
        'HelpForm.ShowDialog()
        HelpForm.Show()
    End Sub

    Private Sub AboutUsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AboutUsToolStripMenuItem.Click
        'aboutForm.ShowDialog()
        aboutForm.Show()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'printall()
        beforePrintAll.ShowDialog()
    End Sub

    Private Sub PrintDataToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PrintDataToolStripMenuItem.Click
        viewprintdata.ShowDialog()
    End Sub

    Private Sub shareholders_CheckedChanged(sender As Object, e As EventArgs) Handles shareholders.CheckedChanged
        If shareholders.Checked = True Then
            Filtershareholder("YES")
        Else
            Filtershareholder("NO")
        End If
    End Sub

    Public Sub Filtershareholder(yesno As String)
        Dim searchQuery As String
        If yesno = "YES" Then
            searchQuery = "SELECT ID,mName,mAddress,mContact,mEmail,hasShare,joinDate,cows From farmers WHERE hasShare = 'YES' ORDER BY ID"
        Else
            searchQuery = "SELECT ID,mName,mAddress,mContact,mEmail,hasShare,joinDate,cows From farmers ORDER BY ID"
        End If
        Dim command As New OleDbCommand(searchQuery, connection)
        Dim adapter As New OleDbDataAdapter(command)
        Dim table As New DataTable()
        adapter.Fill(table)
        DataGridView1.DataSource = table
    End Sub

    Private Sub LoanToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LoanToolStripMenuItem.Click
        company_loan.ShowDialog()
    End Sub

    Private Sub StatementsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles StatementsToolStripMenuItem.Click
        company_statement.ShowDialog()
    End Sub

    Private Sub EMICalculatorToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EMICalculatorToolStripMenuItem.Click
        emi_calculator.ShowDialog()
        'emi_calculator_stat.ShowDialog()
    End Sub

    Private Sub AccountSettingToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AccountSettingToolStripMenuItem.Click
        otherSetting_account.ShowDialog()
    End Sub

    Private Sub YearDataAnalysisToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles YearDataAnalysisToolStripMenuItem.Click
        company_yearView.ShowDialog()
    End Sub

    Private Sub ViewAllToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ViewAllToolStripMenuItem.Click
        shareholderDetails_all.ShowDialog()
    End Sub

    Private Sub BonusToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BonusToolStripMenuItem.Click
        shareholders_previous_balance.ShowDialog()
    End Sub

    Private Sub DataGridView1_KeyDown(sender As Object, e As KeyEventArgs) Handles DataGridView1.KeyDown
        If e.KeyCode = Keys.Enter Then
            If DataGridView1.RowCount > 0 Then
                detailsForm.isid.Text = DataGridView1.CurrentRow.Cells(0).Value.ToString()
                detailsForm.mName.Text = DataGridView1.CurrentRow.Cells(1).Value.ToString()
                detailsForm.hasShares = DataGridView1.CurrentRow.Cells(5).Value.ToString()
                detailsForm.ShowDialog()
            End If
        End If
    End Sub

    Private Sub ShareAmountToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ShareAmountToolStripMenuItem.Click
        'open share amount
        share_amount.ShowDialog()
    End Sub

    Private Sub LicenseToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LicenseToolStripMenuItem.Click
        add_license.Show()
    End Sub
End Class
