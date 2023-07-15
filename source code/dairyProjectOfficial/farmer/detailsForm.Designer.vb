<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class detailsForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.isid = New System.Windows.Forms.TextBox()
        Me.mName = New System.Windows.Forms.TextBox()
        Me.editButton = New System.Windows.Forms.Button()
        Me.deleteButton = New System.Windows.Forms.Button()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.printReceip = New System.Windows.Forms.Button()
        Me.milkStore = New System.Windows.Forms.Button()
        Me.moreDetails = New System.Windows.Forms.Button()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.hasShare = New System.Windows.Forms.CheckBox()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.EditToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DeleteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.avgfat = New System.Windows.Forms.Label()
        Me.avglacto = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.imagename = New System.Windows.Forms.PictureBox()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.ContextMenuStrip1.SuspendLayout()
        CType(Me.imagename, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'isid
        '
        Me.isid.Enabled = False
        Me.isid.Font = New System.Drawing.Font("Calibri", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.isid.Location = New System.Drawing.Point(21, 4)
        Me.isid.Name = "isid"
        Me.isid.Size = New System.Drawing.Size(35, 31)
        Me.isid.TabIndex = 0
        '
        'mName
        '
        Me.mName.Font = New System.Drawing.Font("Calibri", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.mName.Location = New System.Drawing.Point(62, 4)
        Me.mName.Name = "mName"
        Me.mName.Size = New System.Drawing.Size(170, 31)
        Me.mName.TabIndex = 1
        '
        'editButton
        '
        Me.editButton.BackColor = System.Drawing.Color.SeaGreen
        Me.editButton.FlatAppearance.BorderSize = 0
        Me.editButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.editButton.Font = New System.Drawing.Font("Calibri", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.editButton.ForeColor = System.Drawing.Color.White
        Me.editButton.Location = New System.Drawing.Point(138, 167)
        Me.editButton.Name = "editButton"
        Me.editButton.Size = New System.Drawing.Size(137, 39)
        Me.editButton.TabIndex = 6
        Me.editButton.Text = "Edit  Details"
        Me.editButton.UseVisualStyleBackColor = False
        '
        'deleteButton
        '
        Me.deleteButton.BackColor = System.Drawing.Color.Brown
        Me.deleteButton.FlatAppearance.BorderSize = 0
        Me.deleteButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.deleteButton.Font = New System.Drawing.Font("Calibri", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.deleteButton.ForeColor = System.Drawing.Color.White
        Me.deleteButton.Location = New System.Drawing.Point(138, 217)
        Me.deleteButton.Name = "deleteButton"
        Me.deleteButton.Size = New System.Drawing.Size(137, 39)
        Me.deleteButton.TabIndex = 7
        Me.deleteButton.Text = "Delete Farmer"
        Me.deleteButton.UseVisualStyleBackColor = False
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(6, 7)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(0, 20)
        Me.TextBox1.TabIndex = 16
        '
        'DataGridView1
        '
        Me.DataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.DataGridView1.BackgroundColor = System.Drawing.SystemColors.Control
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Location = New System.Drawing.Point(0, 283)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.Size = New System.Drawing.Size(859, 316)
        Me.DataGridView1.TabIndex = 17
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.printReceip)
        Me.Panel1.Controls.Add(Me.milkStore)
        Me.Panel1.Controls.Add(Me.moreDetails)
        Me.Panel1.Controls.Add(Me.editButton)
        Me.Panel1.Controls.Add(Me.deleteButton)
        Me.Panel1.Location = New System.Drawing.Point(585, 7)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(281, 258)
        Me.Panel1.TabIndex = 18
        '
        'printReceip
        '
        Me.printReceip.BackColor = System.Drawing.Color.SeaGreen
        Me.printReceip.FlatAppearance.BorderSize = 0
        Me.printReceip.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.printReceip.Font = New System.Drawing.Font("Calibri", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.printReceip.ForeColor = System.Drawing.Color.White
        Me.printReceip.Location = New System.Drawing.Point(139, 114)
        Me.printReceip.Name = "printReceip"
        Me.printReceip.Size = New System.Drawing.Size(136, 39)
        Me.printReceip.TabIndex = 22
        Me.printReceip.Text = "Print Receipe"
        Me.printReceip.UseVisualStyleBackColor = False
        '
        'milkStore
        '
        Me.milkStore.BackColor = System.Drawing.Color.SeaGreen
        Me.milkStore.FlatAppearance.BorderSize = 0
        Me.milkStore.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.milkStore.Font = New System.Drawing.Font("Calibri", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.milkStore.ForeColor = System.Drawing.Color.White
        Me.milkStore.Location = New System.Drawing.Point(138, 57)
        Me.milkStore.Name = "milkStore"
        Me.milkStore.Size = New System.Drawing.Size(136, 39)
        Me.milkStore.TabIndex = 21
        Me.milkStore.Text = "Store Milk"
        Me.milkStore.UseVisualStyleBackColor = False
        '
        'moreDetails
        '
        Me.moreDetails.BackColor = System.Drawing.Color.SeaGreen
        Me.moreDetails.FlatAppearance.BorderSize = 0
        Me.moreDetails.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.moreDetails.Font = New System.Drawing.Font("Calibri", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.moreDetails.ForeColor = System.Drawing.Color.White
        Me.moreDetails.Image = Global.dairyProjectOfficial.My.Resources.Resources.right_arrow2
        Me.moreDetails.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.moreDetails.Location = New System.Drawing.Point(138, 3)
        Me.moreDetails.Name = "moreDetails"
        Me.moreDetails.Size = New System.Drawing.Size(137, 39)
        Me.moreDetails.TabIndex = 9
        Me.moreDetails.Text = "More Details"
        Me.moreDetails.UseVisualStyleBackColor = False
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Calibri", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(2, 251)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(62, 29)
        Me.Label6.TabIndex = 20
        Me.Label6.Text = "Day: "
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Calibri", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(50, 252)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(37, 29)
        Me.Label7.TabIndex = 21
        Me.Label7.Text = "01"
        '
        'hasShare
        '
        Me.hasShare.AutoSize = True
        Me.hasShare.Font = New System.Drawing.Font("Calibri", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.hasShare.Location = New System.Drawing.Point(247, 9)
        Me.hasShare.Name = "hasShare"
        Me.hasShare.Size = New System.Drawing.Size(123, 27)
        Me.hasShare.TabIndex = 24
        Me.hasShare.Text = "Shareholder"
        Me.hasShare.UseVisualStyleBackColor = True
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.EditToolStripMenuItem, Me.DeleteToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(108, 48)
        '
        'EditToolStripMenuItem
        '
        Me.EditToolStripMenuItem.Name = "EditToolStripMenuItem"
        Me.EditToolStripMenuItem.Size = New System.Drawing.Size(107, 22)
        Me.EditToolStripMenuItem.Text = "Edit"
        '
        'DeleteToolStripMenuItem
        '
        Me.DeleteToolStripMenuItem.Name = "DeleteToolStripMenuItem"
        Me.DeleteToolStripMenuItem.Size = New System.Drawing.Size(107, 22)
        Me.DeleteToolStripMenuItem.Text = "Delete"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Calibri", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(572, 263)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(86, 18)
        Me.Label1.TabIndex = 38
        Me.Label1.Text = "Avg. Fat Ins.:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Calibri", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(712, 263)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(99, 18)
        Me.Label2.TabIndex = 39
        Me.Label2.Text = "Avg. Lacto Ins.:"
        '
        'avgfat
        '
        Me.avgfat.AutoSize = True
        Me.avgfat.Font = New System.Drawing.Font("Calibri", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.avgfat.Location = New System.Drawing.Point(653, 263)
        Me.avgfat.Name = "avgfat"
        Me.avgfat.Size = New System.Drawing.Size(15, 18)
        Me.avgfat.TabIndex = 40
        Me.avgfat.Text = "0"
        '
        'avglacto
        '
        Me.avglacto.AutoSize = True
        Me.avglacto.Font = New System.Drawing.Font("Calibri", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.avglacto.Location = New System.Drawing.Point(807, 263)
        Me.avglacto.Name = "avglacto"
        Me.avglacto.Size = New System.Drawing.Size(15, 18)
        Me.avglacto.TabIndex = 41
        Me.avglacto.Text = "0"
        '
        'Button1
        '
        Me.Button1.Image = Global.dairyProjectOfficial.My.Resources.Resources.update
        Me.Button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button1.Location = New System.Drawing.Point(376, 11)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(84, 25)
        Me.Button1.TabIndex = 42
        Me.Button1.Text = "   Reload"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'imagename
        '
        Me.imagename.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.imagename.Location = New System.Drawing.Point(21, 63)
        Me.imagename.Name = "imagename"
        Me.imagename.Size = New System.Drawing.Size(170, 142)
        Me.imagename.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.imagename.TabIndex = 37
        Me.imagename.TabStop = False
        '
        'detailsForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(865, 611)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.avglacto)
        Me.Controls.Add(Me.avgfat)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.imagename)
        Me.Controls.Add(Me.hasShare)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.mName)
        Me.Controls.Add(Me.isid)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.KeyPreview = True
        Me.Name = "detailsForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Farmers Detail Form"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.ContextMenuStrip1.ResumeLayout(False)
        CType(Me.imagename, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents isid As TextBox
    Friend WithEvents mName As TextBox
    Friend WithEvents editButton As Button
    Friend WithEvents deleteButton As Button
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents Panel1 As Panel
    Friend WithEvents moreDetails As Button
    Friend WithEvents milkStore As Button
    Friend WithEvents Label6 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents printReceip As Button
    Friend WithEvents hasShare As CheckBox
    Friend WithEvents imagename As PictureBox
    Friend WithEvents ContextMenuStrip1 As ContextMenuStrip
    Friend WithEvents EditToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents DeleteToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents avgfat As Label
    Friend WithEvents avglacto As Label
    Friend WithEvents Button1 As Button
End Class
