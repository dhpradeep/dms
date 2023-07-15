Public Class emi_calculator
    Private Sub btncalcpayment_Click(sender As Object, e As EventArgs) Handles btncalcpayment.Click
        Dim Payment As Single
        Dim LoanIRate As Single
        Dim LoanDuration As Integer
        Dim LoanAmount As Integer
        ' Validate amount
        If IsNumeric(txtamnt.Text) Then
            LoanAmount = txtamnt.Text
        Else
            MsgBox("Please enter a valid amount")
            Exit Sub
        End If
        ' Validate interest rate
        If IsNumeric(txtrate.Text) Then
            LoanIRate = 0.01 * txtrate.Text / 12
        Else
            MsgBox("Invalid interest rate, please re-enter")
            Exit Sub
        End If
        ' Validate loan’s duration
        If IsNumeric(txtduration.Text) Then
            LoanDuration = txtduration.Text
        Else
            MsgBox("Please specify the loan’s duration as a number of months")
            Exit Sub
        End If
        ' If all data were validated, proceed with calculations
        Dim payEarly As DueDate
        If chkPayEarly.Checked Then
            payEarly = DueDate.BegOfPeriod
        Else
            payEarly = DueDate.EndOfPeriod
        End If
        Payment = Pmt(LoanIRate, LoanDuration, -LoanAmount, 0, payEarly)
        txtpayment.Text = Payment.ToString("#.00")
    End Sub

    Private Sub btnexit_Click(sender As Object, e As EventArgs)
        Me.Close()
    End Sub

    Private Sub emi_calculator_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If txtpayment.Text = "" Then
            MsgBox("Please calculate here first.", MsgBoxStyle.Critical, Title:="Try again")
        Else
            emi_calculator_stat.txtamnt.Text = txtamnt.Text
            emi_calculator_stat.txtduration.Text = txtduration.Text
            emi_calculator_stat.txtrate.Text = txtrate.Text
            emi_calculator_stat.ShowDialog()
        End If
    End Sub

    Private Sub emi_calculator_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then Me.Close()
    End Sub
End Class