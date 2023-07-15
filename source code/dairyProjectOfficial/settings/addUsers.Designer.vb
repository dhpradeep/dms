<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class addUsers
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
        Me.changepasschar = New System.Windows.Forms.CheckBox()
        Me.cPassword = New System.Windows.Forms.Button()
        Me.chpassword = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.password = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.username = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'changepasschar
        '
        Me.changepasschar.AutoSize = True
        Me.changepasschar.Location = New System.Drawing.Point(288, 192)
        Me.changepasschar.Name = "changepasschar"
        Me.changepasschar.Size = New System.Drawing.Size(102, 17)
        Me.changepasschar.TabIndex = 18
        Me.changepasschar.Text = "Show Password"
        Me.changepasschar.UseVisualStyleBackColor = True
        '
        'cPassword
        '
        Me.cPassword.BackColor = System.Drawing.Color.SeaGreen
        Me.cPassword.FlatAppearance.BorderSize = 0
        Me.cPassword.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cPassword.Font = New System.Drawing.Font("Calibri", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cPassword.ForeColor = System.Drawing.Color.White
        Me.cPassword.Location = New System.Drawing.Point(166, 228)
        Me.cPassword.Name = "cPassword"
        Me.cPassword.Size = New System.Drawing.Size(224, 49)
        Me.cPassword.TabIndex = 19
        Me.cPassword.Text = "Add User"
        Me.cPassword.UseVisualStyleBackColor = False
        '
        'chpassword
        '
        Me.chpassword.Font = New System.Drawing.Font("Calibri", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chpassword.Location = New System.Drawing.Point(166, 140)
        Me.chpassword.Name = "chpassword"
        Me.chpassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.chpassword.Size = New System.Drawing.Size(224, 31)
        Me.chpassword.TabIndex = 17
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Calibri", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(-1, 143)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(166, 23)
        Me.Label3.TabIndex = 23
        Me.Label3.Text = "*Confirm Password:"
        '
        'password
        '
        Me.password.Font = New System.Drawing.Font("Calibri", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.password.Location = New System.Drawing.Point(166, 86)
        Me.password.Name = "password"
        Me.password.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.password.Size = New System.Drawing.Size(224, 31)
        Me.password.TabIndex = 16
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Calibri", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(-1, 86)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(100, 23)
        Me.Label2.TabIndex = 22
        Me.Label2.Text = "*Password:"
        '
        'username
        '
        Me.username.Font = New System.Drawing.Font("Calibri", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.username.Location = New System.Drawing.Point(166, 34)
        Me.username.Name = "username"
        Me.username.Size = New System.Drawing.Size(224, 31)
        Me.username.TabIndex = 20
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Calibri", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(-1, 34)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(103, 23)
        Me.Label1.TabIndex = 21
        Me.Label1.Text = "*Username:"
        '
        'addUsers
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(453, 350)
        Me.Controls.Add(Me.changepasschar)
        Me.Controls.Add(Me.cPassword)
        Me.Controls.Add(Me.chpassword)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.password)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.username)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.KeyPreview = True
        Me.Name = "addUsers"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Add Users"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents changepasschar As CheckBox
    Friend WithEvents cPassword As Button
    Friend WithEvents chpassword As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents password As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents username As TextBox
    Friend WithEvents Label1 As Label
End Class
