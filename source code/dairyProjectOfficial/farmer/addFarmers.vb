Imports System.Data.OleDb
Imports System.IO
Public Class addFarmers
    Dim bytImage() As Byte

    Private Sub addMembers_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        mid.Text = ""
        mid.Select()
        mname.Text = ""
        address.Text = ""
        contact.Text = ""
        mail.Text = ""
        nocows.Text = ""
        imagename.Image = Nothing
        hasShare.Checked = False

    End Sub

    Private Sub addMember_Click(sender As Object, e As EventArgs) Handles addMember.Click
        If mname.Text = "" OrElse mid.Text = "" Then
            printmessage("Please fill out all data", "Error", "Try again")
            mid.Select()
            Return
        Else
            Try
                If hasShare.Checked = True Then
                    If imagename.Image Is Nothing Then
                        addfarmer("YES")
                    Else
                        Dim ms As New System.IO.MemoryStream
                        Dim bmpImage As New Bitmap(imagename.Image)
                        bmpImage.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg)
                        bytImage = ms.ToArray()
                        ms.Close()
                        addfarmerwithimage("YES")
                    End If
                Else
                    If imagename.Image Is Nothing Then
                        addfarmer("NO")
                    Else
                        Dim ms As New System.IO.MemoryStream
                        Dim bmpImage As New Bitmap(imagename.Image)
                        bmpImage.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg)
                        bytImage = ms.ToArray()
                        ms.Close()
                        addfarmerwithimage("NO")
                    End If
                End If
                Me.Close()
            Catch ex As Exception
                printmessage("ID already exist.Try new one.", "Error", "Try again")
                mid.Text = ""
                mid.Select()
                Return
            End Try
        End If
    End Sub

    Public Sub addfarmerwithimage(isshare As String)
        Dim mNames As String = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(mname.Text)
        Dim joinDate As String = System.DateTime.Now.ToString("dd/MM/yyyy")

        Using con As OleDbConnection = New OleDbConnection(strCon)
            Using cmd = con.CreateCommand()
                cmd.CommandText = "INSERT INTO `farmers` (ID,mName,mAddress,mContact,mEmail,hasShare,joinDate,cows,picturefile) VALUES (@ID,@mName,@mAddress,@mContact,@mEmail,@hasShare,@joinDate,@cows,@image)"
                cmd.Parameters.AddWithValue("@ID", mid.Text)
                cmd.Parameters.AddWithValue("@mName", mNames)
                cmd.Parameters.AddWithValue("@mAddress", address.Text)
                cmd.Parameters.AddWithValue("@mContact", contact.Text)
                cmd.Parameters.AddWithValue("@mEmail", mail.Text)
                cmd.Parameters.AddWithValue("@hasShare", isshare)
                cmd.Parameters.AddWithValue("@joinDate", joinDate)
                cmd.Parameters.AddWithValue("@cows", nocows.Text)
                cmd.Parameters.AddWithValue("@image", bytImage)
                con.Open()
                cmd.ExecuteNonQuery()
                con.Close()
            End Using
        End Using
        printmessage("Successfully inserted.", "Info", "Successful")

    End Sub

    Public Sub addfarmer(isshare As String)
        Dim mNames As String = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(mname.Text)
        Dim joinDate As String = System.DateTime.Now.ToString("dd/MM/yyyy")

        Using con As OleDbConnection = New OleDbConnection(strCon)
            Using cmd = con.CreateCommand()
                cmd.CommandText = "INSERT INTO `farmers` (ID,mName,mAddress,mContact,mEmail,hasShare,joinDate,cows) VALUES (@ID,@mName,@mAddress,@mContact,@mEmail,@hasShare,@joinDate,@cows)"
                cmd.Parameters.AddWithValue("@ID", mid.Text)
                cmd.Parameters.AddWithValue("@mName", mNames)
                cmd.Parameters.AddWithValue("@mAddress", address.Text)
                cmd.Parameters.AddWithValue("@mContact", contact.Text)
                cmd.Parameters.AddWithValue("@mEmail", mail.Text)
                cmd.Parameters.AddWithValue("@hasShare", isshare)
                cmd.Parameters.AddWithValue("@joinDate", joinDate)
                cmd.Parameters.AddWithValue("@cows", nocows.Text)
                con.Open()
                cmd.ExecuteNonQuery()
                con.Close()
            End Using
        End Using
        printmessage("Successfully Inserted", "Info", "Successful")

    End Sub

    Private Sub addFarmers_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then Me.Close()
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
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

    Private Sub LinkLabel2_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel2.LinkClicked
        imagename.Image = Nothing
    End Sub

    Private Sub contact_KeyPress(sender As Object, e As KeyPressEventArgs) Handles contact.KeyPress
        validnumber(e)
    End Sub

    Private Sub mid_KeyPress(sender As Object, e As KeyPressEventArgs) Handles mid.KeyPress
        validnumber(e)
    End Sub

    Private Sub mail_KeyPress(sender As Object, e As KeyPressEventArgs) Handles mail.KeyPress
        e.Handled = (Char.IsWhiteSpace(e.KeyChar))
    End Sub

    Private Sub nocows_KeyPress(sender As Object, e As KeyPressEventArgs) Handles nocows.KeyPress
        validnumber(e)
    End Sub
End Class