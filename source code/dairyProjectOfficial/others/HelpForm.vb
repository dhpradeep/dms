Imports System.Data.OleDb
Imports iTextSharp.text.pdf
Imports iTextSharp.text
Imports System.IO
Public Class HelpForm

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Try
            System.Diagnostics.Process.Start(Application.StartupPath + "\otherHelp.pdf")
        Catch ex As Exception
            MsgBox("Link is broken.", MsgBoxStyle.Critical, Title:="Error")
            'MsgBox(ex.ToString())
        End Try
    End Sub

    Private Sub LinkLabel2_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel2.LinkClicked
        Try
            System.Diagnostics.Process.Start(Application.StartupPath + "\errorHelp.docx")
        Catch ex As Exception
            MsgBox("Link is broken.", MsgBoxStyle.Critical, Title:="Error")
            'MsgBox(ex.ToString())
        End Try
    End Sub

    Private Sub HelpForm_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then Me.Close()
    End Sub
End Class