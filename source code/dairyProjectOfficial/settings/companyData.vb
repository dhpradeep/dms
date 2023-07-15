Imports System.IO
Imports System.Data.OleDb
Public Class companyData
    Public imagename As String
    Private Sub companyData_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        saveBtn.Visible = False
        saveBtn1.Visible = False
        saveBtn2.Visible = False
        TextBox1.Enabled = False
        TextBox2.Enabled = False
        TextBox3.Enabled = False

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
            '7 company name, 8 address ,9  phone
            Dim sql3 As String
            sql3 = "SELECT source FROM sources WHERE ID= 7"
            adapters = New OleDbDataAdapter(sql3, connection)
            Dim dt As New DataTable
            adapters.Fill(dt)
            TextBox1.Text = dt.Rows(0).Item(0)

            Dim sql2 As String
            sql2 = "SELECT source FROM sources WHERE ID= 8"
            adapters = New OleDbDataAdapter(sql2, connection)
            Dim dts As New DataTable
            adapters.Fill(dts)
            TextBox2.Text = dts.Rows(0).Item(0)

            Dim sql As String
            sql = "SELECT source FROM sources WHERE ID= 9"
            adapters = New OleDbDataAdapter(sql, connection)
            Dim dt1 As New DataTable
            adapters.Fill(dt1)
            TextBox3.Text = dt1.Rows(0).Item(0)
        Catch ex As Exception

        End Try

    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        OpenFileDialog1.Filter =
          "Image Files(*.jpg,*.jpeg)|*.jpg;*.jpeg;"
        If OpenFileDialog1.ShowDialog = DialogResult.OK Then
            PictureBox1.Image = Bitmap.FromFile(OpenFileDialog1.FileName)
            imagename = OpenFileDialog1.FileName
        End If
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Try
            If Not (imagename = "") Then
                'sql = "UPDATE imageprocess SET image='' WHERE ID=1"
                If File.Exists("myImage.jpg") Then
                    File.Delete("myImage.jpg")
                End If
                FileCopy(imagename, "myImage.jpg")
                MsgBox("Update new image successfully.")

            Else
                MsgBox("Please choose a picture first.")
            End If
        Catch ex As Exception
            MsgBox("problem on set image. Try again.")
        End Try
    End Sub

    Private Sub editBtn_Click(sender As Object, e As EventArgs) Handles editBtn.Click
        saveBtn.Visible = True
        editBtn.Enabled = False
        TextBox1.Enabled = True
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        saveBtn1.Visible = True
        Button1.Enabled = False
        TextBox2.Enabled = True
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        saveBtn2.Visible = True
        Button3.Enabled = False
        TextBox3.Enabled = True
    End Sub

    Private Sub saveBtn_Click(sender As Object, e As EventArgs) Handles saveBtn.Click
        saveBtn.Visible = False
        editBtn.Enabled = True
        TextBox1.Enabled = False
        'textbox1 data in database
        sql = "UPDATE sources SET source='" & TextBox1.Text & "' WHERE ID=7"
        cmd = New OleDbCommand(sql, connection)
        cmd.ExecuteNonQuery()
    End Sub

    Private Sub saveBtn1_Click(sender As Object, e As EventArgs) Handles saveBtn1.Click
        saveBtn1.Visible = False
        Button1.Enabled = True
        TextBox2.Enabled = False
        'textbox2 data in database
        sql = "UPDATE sources SET source='" & TextBox2.Text & "' WHERE ID=8"
        cmd = New OleDbCommand(sql, connection)
        cmd.ExecuteNonQuery()
    End Sub

    Private Sub saveBtn2_Click(sender As Object, e As EventArgs) Handles saveBtn2.Click
        saveBtn2.Visible = False
        Button3.Enabled = True
        TextBox3.Enabled = False
        'textbox3 data in database
        sql = "UPDATE sources SET source='" & TextBox3.Text & "' WHERE ID=9"
        cmd = New OleDbCommand(sql, connection)
        cmd.ExecuteNonQuery()
    End Sub

    Private Sub companyData_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then Me.Close()
    End Sub
End Class