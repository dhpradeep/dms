<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class emi_calculator
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.btncalcpayment = New System.Windows.Forms.Button()
        Me.chkPayEarly = New System.Windows.Forms.CheckBox()
        Me.txtpayment = New System.Windows.Forms.TextBox()
        Me.txtrate = New System.Windows.Forms.TextBox()
        Me.txtduration = New System.Windows.Forms.TextBox()
        Me.txtamnt = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'btncalcpayment
        '
        Me.btncalcpayment.Font = New System.Drawing.Font("Calibri", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btncalcpayment.Location = New System.Drawing.Point(187, 268)
        Me.btncalcpayment.Name = "btncalcpayment"
        Me.btncalcpayment.Size = New System.Drawing.Size(145, 34)
        Me.btncalcpayment.TabIndex = 21
        Me.btncalcpayment.Text = "Calculate Payment"
        Me.btncalcpayment.UseVisualStyleBackColor = True
        '
        'chkPayEarly
        '
        Me.chkPayEarly.AutoSize = True
        Me.chkPayEarly.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chkPayEarly.Font = New System.Drawing.Font("Calibri", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkPayEarly.Location = New System.Drawing.Point(61, 172)
        Me.chkPayEarly.Margin = New System.Windows.Forms.Padding(4)
        Me.chkPayEarly.Name = "chkPayEarly"
        Me.chkPayEarly.Size = New System.Drawing.Size(148, 27)
        Me.chkPayEarly.TabIndex = 20
        Me.chkPayEarly.Text = "Early Payment :"
        Me.chkPayEarly.UseVisualStyleBackColor = True
        '
        'txtpayment
        '
        Me.txtpayment.Enabled = False
        Me.txtpayment.Font = New System.Drawing.Font("Calibri", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtpayment.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtpayment.Location = New System.Drawing.Point(187, 212)
        Me.txtpayment.Margin = New System.Windows.Forms.Padding(4)
        Me.txtpayment.Name = "txtpayment"
        Me.txtpayment.ReadOnly = True
        Me.txtpayment.Size = New System.Drawing.Size(132, 31)
        Me.txtpayment.TabIndex = 19
        '
        'txtrate
        '
        Me.txtrate.Font = New System.Drawing.Font("Calibri", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtrate.Location = New System.Drawing.Point(185, 120)
        Me.txtrate.Margin = New System.Windows.Forms.Padding(4)
        Me.txtrate.Name = "txtrate"
        Me.txtrate.Size = New System.Drawing.Size(132, 31)
        Me.txtrate.TabIndex = 18
        '
        'txtduration
        '
        Me.txtduration.Font = New System.Drawing.Font("Calibri", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtduration.Location = New System.Drawing.Point(185, 73)
        Me.txtduration.Margin = New System.Windows.Forms.Padding(4)
        Me.txtduration.Name = "txtduration"
        Me.txtduration.Size = New System.Drawing.Size(132, 31)
        Me.txtduration.TabIndex = 17
        '
        'txtamnt
        '
        Me.txtamnt.Font = New System.Drawing.Font("Calibri", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtamnt.Location = New System.Drawing.Point(185, 28)
        Me.txtamnt.Margin = New System.Windows.Forms.Padding(4)
        Me.txtamnt.Name = "txtamnt"
        Me.txtamnt.Size = New System.Drawing.Size(132, 31)
        Me.txtamnt.TabIndex = 16
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Calibri", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(21, 215)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(156, 23)
        Me.Label5.TabIndex = 15
        Me.Label5.Text = "Monthly Payment :"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Calibri", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(57, 123)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(118, 23)
        Me.Label3.TabIndex = 14
        Me.Label3.Text = "Interest Rate :"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Calibri", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(13, 73)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(164, 23)
        Me.Label2.TabIndex = 13
        Me.Label2.Text = "Duration (Months) :"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Calibri", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(55, 31)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(122, 23)
        Me.Label1.TabIndex = 12
        Me.Label1.Text = "Loan Amount :"
        '
        'Button1
        '
        Me.Button1.FlatAppearance.BorderColor = System.Drawing.Color.Blue
        Me.Button1.FlatAppearance.BorderSize = 2
        Me.Button1.Font = New System.Drawing.Font("Calibri", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.Location = New System.Drawing.Point(0, 313)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(98, 30)
        Me.Button1.TabIndex = 23
        Me.Button1.Text = "Statistics"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'emi_calculator
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(377, 343)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.btncalcpayment)
        Me.Controls.Add(Me.chkPayEarly)
        Me.Controls.Add(Me.txtpayment)
        Me.Controls.Add(Me.txtrate)
        Me.Controls.Add(Me.txtduration)
        Me.Controls.Add(Me.txtamnt)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.KeyPreview = True
        Me.Name = "emi_calculator"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Emi Calculator"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btncalcpayment As Button
    Friend WithEvents chkPayEarly As CheckBox
    Friend WithEvents txtpayment As TextBox
    Friend WithEvents txtrate As TextBox
    Friend WithEvents txtduration As TextBox
    Friend WithEvents txtamnt As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Button1 As Button
End Class
