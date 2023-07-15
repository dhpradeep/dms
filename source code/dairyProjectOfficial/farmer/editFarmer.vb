Imports System.Data.OleDb
Imports System.IO
Public Class editFarmer
    Dim cn As New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & Application.StartupPath & "\mydatabase1.mdb;Jet OLEDB:Database Password=login1234")
    Dim cm As New OleDbCommand
    Dim bytImage() As Byte

    Private Sub editFarmer_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        mid.BackColor = Color.White
        Try
            'to start connection of ms-access
            connection = New OleDbConnection
            With connection
                If .State = ConnectionState.Closed Then
                    .ConnectionString = strCon
                    .Open()
                End If
            End With

            mname.Text = selectidsql("farmers", "mName", mid.Text)
            address.Text = selectidsql("farmers", "mAddress", mid.Text)
            contact.Text = selectidsql("farmers", "mContact", mid.Text)
            mail.Text = selectidsql("farmers", "mEmail", mid.Text)
            nocows.Text = selectidsql("farmers", "cows", mid.Text)
            Dim sharecheck As String
            sharecheck = selectidsql("farmers", "hasShare", mid.Text)
            If sharecheck = "YES" Then
                hasShare.Checked = True
            Else
                hasShare.Checked = False
            End If

            Dim ImageByte As Byte()
            Dim MemStream As MemoryStream
            cmd1 = New OleDbCommand("SELECT picturefile FROM farmers WHERE ID=" & mid.Text & "", connection)
            Try
                ImageByte = cmd1.ExecuteScalar()
                MemStream = New MemoryStream(ImageByte)
                imagename.Image = Image.FromStream(MemStream)
            Catch ex As Exception
                imagename.Image = Nothing
            End Try

        Catch ex As Exception
            'MsgBox(ex.ToString())
        End Try
    End Sub

    Private Sub addMember_Click(sender As Object, e As EventArgs) Handles addMember.Click
        If mname.Text = "" Then
            MsgBox("Please fillout all data.", MsgBoxStyle.Exclamation, Title:="Try again")
        Else
            Try
                If hasShare.Checked = True Then
                    If imagename.Image Is Nothing Then
                        Dim result = addfarmer("YES")
                        updateidsql("farmers", "picturefile", "", mid.Text)

                        If result = 1 Then
                            printmessage("Successfully Updated.", "Info", "Successful")
                        End If
                    Else
                        Dim ms As New System.IO.MemoryStream
                        Dim bmpImage As New Bitmap(imagename.Image)
                        bmpImage.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg)
                        bytImage = ms.ToArray()
                        ms.Close()
                        Dim result = addfarmerwithimage("YES")
                        If result = 1 Then
                            printmessage("Successfully Updated.", "Info", "Successful")
                        End If
                    End If
                Else
                    If imagename.Image Is Nothing Then
                        Dim result = addfarmer("NO")
                        updateidsql("farmers", "picturefile", "", mid.Text)

                        If result = 1 Then
                            printmessage("Successfully Updated.", "Info", "Successful")
                        End If
                    Else
                        Dim ms As New System.IO.MemoryStream
                        Dim bmpImage As New Bitmap(imagename.Image)
                        bmpImage.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg)
                        bytImage = ms.ToArray()
                        ms.Close()
                        Dim result = addfarmerwithimage("NO")
                        If result = 1 Then
                            printmessage("Successfully Updated", "Info", "Successful")
                        End If
                    End If
                End If
                Me.Close()
            Catch ex As Exception
                MsgBox("Problem on connecting to database.(Error code: 300)", MsgBoxStyle.Exclamation, Title:="Try again")
                'MsgBox(ex.ToString())
            End Try
        End If
    End Sub

    Public Function addfarmerwithimage(isshare As String)
        Dim mNames As String = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(mname.Text)
        cn.Open()
        cm.Connection = cn
        cm.CommandType = CommandType.Text
        cm.CommandText = "UPDATE `farmers` SET mName=@mName,mAddress=@mAddress,mContact=@nContact,mEmail=@mEmail,hasShare=@hasShare,cows=@cows,picturefile=@image WHERE ID=" & mid.Text & ""
        cm.Parameters.AddWithValue("@mName", mNames)
        cm.Parameters.AddWithValue("@mAddress", address.Text)
        cm.Parameters.AddWithValue("@mContact", contact.Text)
        cm.Parameters.AddWithValue("@mEmail", mail.Text)
        cm.Parameters.AddWithValue("@hasShare", isshare)
        cm.Parameters.AddWithValue("@cows", nocows.Text)
        cm.Parameters.AddWithValue("@image", bytImage)
        Dim result = cm.ExecuteNonQuery()
        cm.Dispose()
        cn.Close()
        Return result
    End Function

    Public Function addfarmer(isshare As String)
        Dim mNames As String = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(mname.Text)
        cn.Open()
        cm.Connection = cn
        cm.CommandType = CommandType.Text
        cm.CommandText = "UPDATE `farmers` SET mName=@mName,mAddress=@mAddress,mContact=@nContact,mEmail=@mEmail,hasShare=@hasShare,cows=@cows WHERE ID=" & mid.Text & ""
        cm.Parameters.AddWithValue("@mName", mNames)
        cm.Parameters.AddWithValue("@mAddress", address.Text)
        cm.Parameters.AddWithValue("@mContact", contact.Text)
        cm.Parameters.AddWithValue("@mEmail", mail.Text)
        cm.Parameters.AddWithValue("@hasShare", isshare)
        cm.Parameters.AddWithValue("@cows", nocows.Text)
        Dim result = cm.ExecuteNonQuery()
        cm.Dispose()
        cn.Close()
        Return result
    End Function

    Public Function deleteimage()
        Try
            sql = "UPDATE farmers SET picturefile='' WHERE ID=" & mid.Text & ""
            cmd = New OleDbCommand(sql, connection)
            cmd.ExecuteNonQuery()
            Return 0
        Catch ex As Exception
        End Try
        Return 0
    End Function



    Private Sub editFarmer_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then Me.Close()
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs)
        Try
            OpenFileDialog1.Filter =
        "Image Files(*.jpg,*.jpeg,*.png,*.gif)|*.jpg;*.jpeg;*.png;*.gif;| all files (*.*)| *.*"
            If OpenFileDialog1.ShowDialog = DialogResult.OK Then
                imagename.Image = Bitmap.FromFile(OpenFileDialog1.FileName)
            End If
        Catch ex As Exception
            MsgBox("Please choose a image file.", MsgBoxStyle.Critical, Title:="Try again")
        End Try

    End Sub

    Private Sub LinkLabel2_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs)
        imagename.Image = Nothing
    End Sub

    Private Sub editFarmer_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        Me.Close()
        detailsForm.Close()
    End Sub

    Private Sub LinkLabel1_LinkClicked_1(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Try
            OpenFileDialog1.Filter =
        "Image Files(*.jpg,*.jpeg,*.png,*.gif)|*.jpg;*.jpeg;*.png;*.gif;| all files (*.*)| *.*"
            If OpenFileDialog1.ShowDialog = DialogResult.OK Then
                imagename.Image = Bitmap.FromFile(OpenFileDialog1.FileName)
            End If
        Catch ex As Exception
            printmessage("Please choose a image file.", "Error", "Try again")
        End Try
    End Sub

    Private Sub LinkLabel2_LinkClicked_1(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel2.LinkClicked
        imagename.Image = Nothing
    End Sub

    Private Sub mid_KeyPress(sender As Object, e As KeyPressEventArgs) Handles mid.KeyPress
        validnumber(e)
    End Sub

    Private Sub contact_KeyPress(sender As Object, e As KeyPressEventArgs) Handles contact.KeyPress
        validnumber(e)
    End Sub

    Private Sub mail_KeyPress(sender As Object, e As KeyPressEventArgs) Handles mail.KeyPress
        e.Handled = (Char.IsWhiteSpace(e.KeyChar))
    End Sub

    Private Sub nocows_KeyPress(sender As Object, e As KeyPressEventArgs) Handles nocows.KeyPress
        validnumber(e)
    End Sub
End Class