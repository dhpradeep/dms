Public Class aboutForm
    Private Sub aboutForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        aboutustext.Text = "This software is build for milk management system. Only Ghachowk dairy corporation has license to use this software."
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        aboutustext.Text = "
Ghachowk Dairy corporation would like to acknowledge the following contributors:

Supervisor:
---------------
Raju lamsal
mail:rajulamsal62@gmail.com
phone: 9846712021

Developer:
---------------
pradip dhakal
mail:dhpradeep25@gmail.com
phone: 9846751280

Documentation:
---------------
pradip poudel
mail:systemanalyst2054@gmail.com
phone: 9846901138
"
    End Sub

    Private Sub LinkLabel2_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel2.LinkClicked
        aboutustext.Text = "                             Ghachowk Dairy Udhyog                      
                          Version 1.0.1, January 2018             "
    End Sub

    Private Sub LinkLabel3_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel3.LinkClicked
        aboutustext.Text = "All rights reserved to PINESOFT Company."
    End Sub

    Private Sub LinkLabel4_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel4.LinkClicked
        Try
            System.Diagnostics.Process.Start(Application.StartupPath + "\documentationFile.pdf")
        Catch ex As Exception
            MsgBox("Link is broken.", MsgBoxStyle.Critical, Title:="Error")
            'MsgBox(ex.ToString())
        End Try
    End Sub

    Private Sub aboutForm_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then Me.Close()
    End Sub
End Class