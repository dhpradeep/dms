Imports System.Data.OleDb
Public Class add_license
    Public currentDate As String = System.DateTime.Now.ToString("dd/MM/yyyy")
    Private Sub add_license_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim expirydate As String = selectidsql("superadmin", "locking", "3")
        Dim expirydate1 As Date = Date.ParseExact(expirydate, "dd/MM/yyyy",
                   System.Globalization.DateTimeFormatInfo.InvariantInfo)
        Dim currentdate1 As Date = Date.ParseExact(currentDate, "dd/MM/yyyy",
                   System.Globalization.DateTimeFormatInfo.InvariantInfo)

        Dim span = expirydate1 - currentdate1
        Label4.Text = expirydate
        Label1.Text = span.TotalDays
        If Label1.Text < 30 Then
            Label1.ForeColor = Color.Red
        End If
    End Sub

    Private Sub add_license_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then Me.Close()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        active_license.ShowDialog()
    End Sub
End Class