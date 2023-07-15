<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class editFarmer
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
        Me.addMember = New System.Windows.Forms.Button()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.LinkLabel2 = New System.Windows.Forms.LinkLabel()
        Me.imagename = New System.Windows.Forms.PictureBox()
        Me.LinkLabel1 = New System.Windows.Forms.LinkLabel()
        Me.mid = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.nocows = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.hasShare = New System.Windows.Forms.CheckBox()
        Me.mail = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.contact = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.address = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.mname = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        CType(Me.imagename, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'addMember
        '
        Me.addMember.BackColor = System.Drawing.Color.SeaGreen
        Me.addMember.FlatAppearance.BorderSize = 0
        Me.addMember.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.addMember.Font = New System.Drawing.Font("Calibri", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.addMember.ForeColor = System.Drawing.Color.White
        Me.addMember.Location = New System.Drawing.Point(119, 372)
        Me.addMember.Name = "addMember"
        Me.addMember.Size = New System.Drawing.Size(224, 55)
        Me.addMember.TabIndex = 37
        Me.addMember.Text = "Edit Farmer"
        Me.addMember.UseVisualStyleBackColor = False
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'LinkLabel2
        '
        Me.LinkLabel2.AutoSize = True
        Me.LinkLabel2.Location = New System.Drawing.Point(383, 225)
        Me.LinkLabel2.Name = "LinkLabel2"
        Me.LinkLabel2.Size = New System.Drawing.Size(68, 13)
        Me.LinkLabel2.TabIndex = 47
        Me.LinkLabel2.TabStop = True
        Me.LinkLabel2.Text = "clean picture"
        '
        'imagename
        '
        Me.imagename.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.imagename.Location = New System.Drawing.Point(386, 65)
        Me.imagename.Name = "imagename"
        Me.imagename.Size = New System.Drawing.Size(170, 142)
        Me.imagename.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.imagename.TabIndex = 46
        Me.imagename.TabStop = False
        '
        'LinkLabel1
        '
        Me.LinkLabel1.AutoSize = True
        Me.LinkLabel1.Location = New System.Drawing.Point(470, 225)
        Me.LinkLabel1.Name = "LinkLabel1"
        Me.LinkLabel1.Size = New System.Drawing.Size(86, 13)
        Me.LinkLabel1.TabIndex = 45
        Me.LinkLabel1.TabStop = True
        Me.LinkLabel1.Text = "choose a picture"
        '
        'mid
        '
        Me.mid.Enabled = False
        Me.mid.Font = New System.Drawing.Font("Calibri", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.mid.Location = New System.Drawing.Point(134, 13)
        Me.mid.Name = "mid"
        Me.mid.Size = New System.Drawing.Size(224, 31)
        Me.mid.TabIndex = 48
        Me.mid.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Calibri", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(28, 21)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(101, 23)
        Me.Label6.TabIndex = 60
        Me.Label6.Text = "*Farmer ID:"
        '
        'nocows
        '
        Me.nocows.Font = New System.Drawing.Font("Calibri", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.nocows.Location = New System.Drawing.Point(134, 286)
        Me.nocows.Name = "nocows"
        Me.nocows.Size = New System.Drawing.Size(224, 31)
        Me.nocows.TabIndex = 55
        Me.nocows.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Calibri", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(15, 286)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(106, 23)
        Me.Label5.TabIndex = 59
        Me.Label5.Text = "No. of cows:"
        '
        'hasShare
        '
        Me.hasShare.AutoSize = True
        Me.hasShare.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control
        Me.hasShare.Font = New System.Drawing.Font("Calibri", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.hasShare.Location = New System.Drawing.Point(134, 335)
        Me.hasShare.Name = "hasShare"
        Me.hasShare.Size = New System.Drawing.Size(125, 27)
        Me.hasShare.TabIndex = 57
        Me.hasShare.Text = "ShareHolder"
        Me.hasShare.UseVisualStyleBackColor = True
        '
        'mail
        '
        Me.mail.Font = New System.Drawing.Font("Calibri", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.mail.Location = New System.Drawing.Point(134, 230)
        Me.mail.Name = "mail"
        Me.mail.Size = New System.Drawing.Size(224, 31)
        Me.mail.TabIndex = 54
        Me.mail.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Calibri", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(54, 233)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(56, 23)
        Me.Label4.TabIndex = 58
        Me.Label4.Text = "Email:"
        '
        'contact
        '
        Me.contact.Font = New System.Drawing.Font("Calibri", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.contact.Location = New System.Drawing.Point(134, 173)
        Me.contact.MaxLength = 10
        Me.contact.Name = "contact"
        Me.contact.Size = New System.Drawing.Size(224, 31)
        Me.contact.TabIndex = 52
        Me.contact.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Calibri", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(41, 181)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(74, 23)
        Me.Label3.TabIndex = 56
        Me.Label3.Text = "Contact:"
        '
        'address
        '
        Me.address.Font = New System.Drawing.Font("Calibri", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.address.Location = New System.Drawing.Point(134, 120)
        Me.address.Name = "address"
        Me.address.Size = New System.Drawing.Size(224, 31)
        Me.address.TabIndex = 51
        Me.address.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Calibri", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(41, 128)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(78, 23)
        Me.Label2.TabIndex = 53
        Me.Label2.Text = "Address:"
        '
        'mname
        '
        Me.mname.Font = New System.Drawing.Font("Calibri", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.mname.Location = New System.Drawing.Point(134, 65)
        Me.mname.Name = "mname"
        Me.mname.Size = New System.Drawing.Size(224, 31)
        Me.mname.TabIndex = 49
        Me.mname.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Calibri", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(28, 73)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(100, 23)
        Me.Label1.TabIndex = 50
        Me.Label1.Text = "*Full Name:"
        '
        'editFarmer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(600, 451)
        Me.Controls.Add(Me.mid)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.nocows)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.hasShare)
        Me.Controls.Add(Me.mail)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.contact)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.address)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.mname)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.LinkLabel2)
        Me.Controls.Add(Me.imagename)
        Me.Controls.Add(Me.LinkLabel1)
        Me.Controls.Add(Me.addMember)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.KeyPreview = True
        Me.Name = "editFarmer"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Edit Details"
        CType(Me.imagename, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents addMember As Button
    Friend WithEvents OpenFileDialog1 As OpenFileDialog
    Friend WithEvents LinkLabel2 As LinkLabel
    Friend WithEvents imagename As PictureBox
    Friend WithEvents LinkLabel1 As LinkLabel
    Friend WithEvents mid As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents nocows As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents hasShare As CheckBox
    Friend WithEvents mail As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents contact As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents address As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents mname As TextBox
    Friend WithEvents Label1 As Label
End Class
