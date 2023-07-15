<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class HomePage
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AddFarmerToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AddCustomerToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BonusToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ShareAmountToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ViewAllToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.EMICalculatorToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.LoanToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.StatementsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.YearDataAnalysisToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SettingToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.OtherSettingToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ChangePasswordToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AddUsersToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PrintDataToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AccountSettingToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.HelpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.HelpToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.AboutUsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.LicenseToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SignOutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.rightSidebar = New System.Windows.Forms.Panel()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.todaylacto = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.todayfat = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.todaymilk = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.editBtn = New System.Windows.Forms.Button()
        Me.saveBtn = New System.Windows.Forms.Button()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.myNote = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.currTime = New System.Windows.Forms.Label()
        Me.MonthCalendar1 = New System.Windows.Forms.MonthCalendar()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.shareholders = New System.Windows.Forms.CheckBox()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.searchItem = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.milkStore = New System.Windows.Forms.Button()
        Me.Timer2 = New System.Windows.Forms.Timer(Me.components)
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.MenuStrip1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.rightSidebar.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem, Me.ToolsToolStripMenuItem, Me.ToolStripMenuItem1, Me.SettingToolStripMenuItem, Me.HelpToolStripMenuItem, Me.SignOutToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(1370, 26)
        Me.MenuStrip1.TabIndex = 0
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AddFarmerToolStripMenuItem, Me.ExitToolStripMenuItem})
        Me.FileToolStripMenuItem.Font = New System.Drawing.Font("Calibri", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FileToolStripMenuItem.Image = Global.dairyProjectOfficial.My.Resources.Resources.folder1
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        Me.FileToolStripMenuItem.Size = New System.Drawing.Size(59, 22)
        Me.FileToolStripMenuItem.Text = "File"
        '
        'AddFarmerToolStripMenuItem
        '
        Me.AddFarmerToolStripMenuItem.Image = Global.dairyProjectOfficial.My.Resources.Resources.addfarmer
        Me.AddFarmerToolStripMenuItem.Name = "AddFarmerToolStripMenuItem"
        Me.AddFarmerToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.A), System.Windows.Forms.Keys)
        Me.AddFarmerToolStripMenuItem.Size = New System.Drawing.Size(194, 22)
        Me.AddFarmerToolStripMenuItem.Text = "Add Farmer"
        '
        'ExitToolStripMenuItem
        '
        Me.ExitToolStripMenuItem.Image = Global.dairyProjectOfficial.My.Resources.Resources.exit1
        Me.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem"
        Me.ExitToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.F4), System.Windows.Forms.Keys)
        Me.ExitToolStripMenuItem.Size = New System.Drawing.Size(194, 22)
        Me.ExitToolStripMenuItem.Text = "Exit"
        '
        'ToolsToolStripMenuItem
        '
        Me.ToolsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AddCustomerToolStripMenuItem, Me.BonusToolStripMenuItem, Me.ShareAmountToolStripMenuItem, Me.ViewAllToolStripMenuItem, Me.EMICalculatorToolStripMenuItem})
        Me.ToolsToolStripMenuItem.Font = New System.Drawing.Font("Calibri", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolsToolStripMenuItem.Image = Global.dairyProjectOfficial.My.Resources.Resources.saving
        Me.ToolsToolStripMenuItem.Name = "ToolsToolStripMenuItem"
        Me.ToolsToolStripMenuItem.Size = New System.Drawing.Size(118, 22)
        Me.ToolsToolStripMenuItem.Text = "Shareholders"
        '
        'AddCustomerToolStripMenuItem
        '
        Me.AddCustomerToolStripMenuItem.Image = Global.dairyProjectOfficial.My.Resources.Resources.saving
        Me.AddCustomerToolStripMenuItem.Name = "AddCustomerToolStripMenuItem"
        Me.AddCustomerToolStripMenuItem.Size = New System.Drawing.Size(212, 22)
        Me.AddCustomerToolStripMenuItem.Text = "Add  Monthly Amount"
        '
        'BonusToolStripMenuItem
        '
        Me.BonusToolStripMenuItem.Name = "BonusToolStripMenuItem"
        Me.BonusToolStripMenuItem.Size = New System.Drawing.Size(212, 22)
        Me.BonusToolStripMenuItem.Text = "Add Previous Balance"
        '
        'ShareAmountToolStripMenuItem
        '
        Me.ShareAmountToolStripMenuItem.Name = "ShareAmountToolStripMenuItem"
        Me.ShareAmountToolStripMenuItem.Size = New System.Drawing.Size(212, 22)
        Me.ShareAmountToolStripMenuItem.Text = "Share Amount"
        '
        'ViewAllToolStripMenuItem
        '
        Me.ViewAllToolStripMenuItem.Name = "ViewAllToolStripMenuItem"
        Me.ViewAllToolStripMenuItem.Size = New System.Drawing.Size(212, 22)
        Me.ViewAllToolStripMenuItem.Text = "View All"
        '
        'EMICalculatorToolStripMenuItem
        '
        Me.EMICalculatorToolStripMenuItem.Name = "EMICalculatorToolStripMenuItem"
        Me.EMICalculatorToolStripMenuItem.Size = New System.Drawing.Size(212, 22)
        Me.EMICalculatorToolStripMenuItem.Text = "EMI Calculator"
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.LoanToolStripMenuItem, Me.StatementsToolStripMenuItem, Me.YearDataAnalysisToolStripMenuItem})
        Me.ToolStripMenuItem1.Font = New System.Drawing.Font("Calibri", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStripMenuItem1.Image = Global.dairyProjectOfficial.My.Resources.Resources.account
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(140, 22)
        Me.ToolStripMenuItem1.Text = "Company Details"
        '
        'LoanToolStripMenuItem
        '
        Me.LoanToolStripMenuItem.Name = "LoanToolStripMenuItem"
        Me.LoanToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.L), System.Windows.Forms.Keys)
        Me.LoanToolStripMenuItem.Size = New System.Drawing.Size(187, 22)
        Me.LoanToolStripMenuItem.Text = "Loan"
        '
        'StatementsToolStripMenuItem
        '
        Me.StatementsToolStripMenuItem.Name = "StatementsToolStripMenuItem"
        Me.StatementsToolStripMenuItem.Size = New System.Drawing.Size(187, 22)
        Me.StatementsToolStripMenuItem.Text = "Statements"
        '
        'YearDataAnalysisToolStripMenuItem
        '
        Me.YearDataAnalysisToolStripMenuItem.Name = "YearDataAnalysisToolStripMenuItem"
        Me.YearDataAnalysisToolStripMenuItem.Size = New System.Drawing.Size(187, 22)
        Me.YearDataAnalysisToolStripMenuItem.Text = "Year Data Analysis"
        '
        'SettingToolStripMenuItem
        '
        Me.SettingToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OtherSettingToolStripMenuItem, Me.ChangePasswordToolStripMenuItem, Me.AddUsersToolStripMenuItem, Me.PrintDataToolStripMenuItem, Me.AccountSettingToolStripMenuItem})
        Me.SettingToolStripMenuItem.Font = New System.Drawing.Font("Calibri", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SettingToolStripMenuItem.Image = Global.dairyProjectOfficial.My.Resources.Resources.settings1
        Me.SettingToolStripMenuItem.Name = "SettingToolStripMenuItem"
        Me.SettingToolStripMenuItem.Size = New System.Drawing.Size(79, 22)
        Me.SettingToolStripMenuItem.Text = "Setting"
        '
        'OtherSettingToolStripMenuItem
        '
        Me.OtherSettingToolStripMenuItem.Image = Global.dairyProjectOfficial.My.Resources.Resources.settings1
        Me.OtherSettingToolStripMenuItem.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.OtherSettingToolStripMenuItem.Name = "OtherSettingToolStripMenuItem"
        Me.OtherSettingToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.S), System.Windows.Forms.Keys)
        Me.OtherSettingToolStripMenuItem.Size = New System.Drawing.Size(184, 22)
        Me.OtherSettingToolStripMenuItem.Text = "Settings"
        '
        'ChangePasswordToolStripMenuItem
        '
        Me.ChangePasswordToolStripMenuItem.Image = Global.dairyProjectOfficial.My.Resources.Resources.password1
        Me.ChangePasswordToolStripMenuItem.Name = "ChangePasswordToolStripMenuItem"
        Me.ChangePasswordToolStripMenuItem.Size = New System.Drawing.Size(184, 22)
        Me.ChangePasswordToolStripMenuItem.Text = "Change Password"
        '
        'AddUsersToolStripMenuItem
        '
        Me.AddUsersToolStripMenuItem.Image = Global.dairyProjectOfficial.My.Resources.Resources.addUsers1
        Me.AddUsersToolStripMenuItem.Name = "AddUsersToolStripMenuItem"
        Me.AddUsersToolStripMenuItem.Size = New System.Drawing.Size(184, 22)
        Me.AddUsersToolStripMenuItem.Text = "Add Users"
        '
        'PrintDataToolStripMenuItem
        '
        Me.PrintDataToolStripMenuItem.Image = Global.dairyProjectOfficial.My.Resources.Resources.print
        Me.PrintDataToolStripMenuItem.Name = "PrintDataToolStripMenuItem"
        Me.PrintDataToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.P), System.Windows.Forms.Keys)
        Me.PrintDataToolStripMenuItem.Size = New System.Drawing.Size(184, 22)
        Me.PrintDataToolStripMenuItem.Text = "Print Data"
        '
        'AccountSettingToolStripMenuItem
        '
        Me.AccountSettingToolStripMenuItem.Image = Global.dairyProjectOfficial.My.Resources.Resources.account
        Me.AccountSettingToolStripMenuItem.Name = "AccountSettingToolStripMenuItem"
        Me.AccountSettingToolStripMenuItem.Size = New System.Drawing.Size(184, 22)
        Me.AccountSettingToolStripMenuItem.Text = "Account Setting"
        '
        'HelpToolStripMenuItem
        '
        Me.HelpToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.HelpToolStripMenuItem1, Me.AboutUsToolStripMenuItem, Me.LicenseToolStripMenuItem})
        Me.HelpToolStripMenuItem.Font = New System.Drawing.Font("Calibri", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.HelpToolStripMenuItem.Image = Global.dairyProjectOfficial.My.Resources.Resources.help_11
        Me.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem"
        Me.HelpToolStripMenuItem.Size = New System.Drawing.Size(65, 22)
        Me.HelpToolStripMenuItem.Text = "Help"
        '
        'HelpToolStripMenuItem1
        '
        Me.HelpToolStripMenuItem1.Image = Global.dairyProjectOfficial.My.Resources.Resources.info1
        Me.HelpToolStripMenuItem1.Name = "HelpToolStripMenuItem1"
        Me.HelpToolStripMenuItem1.ShortcutKeys = System.Windows.Forms.Keys.F1
        Me.HelpToolStripMenuItem1.Size = New System.Drawing.Size(183, 22)
        Me.HelpToolStripMenuItem1.Text = "Help"
        '
        'AboutUsToolStripMenuItem
        '
        Me.AboutUsToolStripMenuItem.Image = Global.dairyProjectOfficial.My.Resources.Resources.about_us
        Me.AboutUsToolStripMenuItem.Name = "AboutUsToolStripMenuItem"
        Me.AboutUsToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.F1), System.Windows.Forms.Keys)
        Me.AboutUsToolStripMenuItem.Size = New System.Drawing.Size(183, 22)
        Me.AboutUsToolStripMenuItem.Text = "About Us"
        '
        'LicenseToolStripMenuItem
        '
        Me.LicenseToolStripMenuItem.Name = "LicenseToolStripMenuItem"
        Me.LicenseToolStripMenuItem.Size = New System.Drawing.Size(183, 22)
        Me.LicenseToolStripMenuItem.Text = "Add License "
        '
        'SignOutToolStripMenuItem
        '
        Me.SignOutToolStripMenuItem.Font = New System.Drawing.Font("Calibri", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SignOutToolStripMenuItem.Image = Global.dairyProjectOfficial.My.Resources.Resources.logout1
        Me.SignOutToolStripMenuItem.Name = "SignOutToolStripMenuItem"
        Me.SignOutToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F4
        Me.SignOutToolStripMenuItem.Size = New System.Drawing.Size(88, 22)
        Me.SignOutToolStripMenuItem.Text = "Sign Out"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 689)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1370, 22)
        Me.Panel1.TabIndex = 1
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label4.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(1104, 0)
        Me.Label4.Margin = New System.Windows.Forms.Padding(3, 0, 10, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(60, 15)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "Copyright"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label1.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(1164, 0)
        Me.Label1.Margin = New System.Windows.Forms.Padding(3, 0, 10, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(13, 15)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "c"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label3.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(1177, 0)
        Me.Label3.Margin = New System.Windows.Forms.Padding(3, 0, 10, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(114, 15)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "all rights reserve to"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label2.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(1291, 0)
        Me.Label2.Margin = New System.Windows.Forms.Padding(3, 0, 10, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(79, 15)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Pinesoftware"
        '
        'rightSidebar
        '
        Me.rightSidebar.Controls.Add(Me.GroupBox1)
        Me.rightSidebar.Controls.Add(Me.editBtn)
        Me.rightSidebar.Controls.Add(Me.saveBtn)
        Me.rightSidebar.Controls.Add(Me.Label7)
        Me.rightSidebar.Controls.Add(Me.myNote)
        Me.rightSidebar.Controls.Add(Me.Label5)
        Me.rightSidebar.Controls.Add(Me.currTime)
        Me.rightSidebar.Controls.Add(Me.MonthCalendar1)
        Me.rightSidebar.Dock = System.Windows.Forms.DockStyle.Right
        Me.rightSidebar.Location = New System.Drawing.Point(1144, 26)
        Me.rightSidebar.Name = "rightSidebar"
        Me.rightSidebar.Size = New System.Drawing.Size(226, 663)
        Me.rightSidebar.TabIndex = 2
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.todaylacto)
        Me.GroupBox1.Controls.Add(Me.Label13)
        Me.GroupBox1.Controls.Add(Me.todayfat)
        Me.GroupBox1.Controls.Add(Me.Label11)
        Me.GroupBox1.Controls.Add(Me.todaymilk)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Font = New System.Drawing.Font("Calibri", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(6, 462)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(217, 198)
        Me.GroupBox1.TabIndex = 7
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Today summary"
        '
        'todaylacto
        '
        Me.todaylacto.AutoSize = True
        Me.todaylacto.Font = New System.Drawing.Font("Calibri", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.todaylacto.Location = New System.Drawing.Point(83, 97)
        Me.todaylacto.Name = "todaylacto"
        Me.todaylacto.Size = New System.Drawing.Size(38, 18)
        Me.todaylacto.TabIndex = 5
        Me.todaylacto.Text = "lacto"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Calibri", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(22, 97)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(44, 18)
        Me.Label13.TabIndex = 4
        Me.Label13.Text = "Lacto:"
        '
        'todayfat
        '
        Me.todayfat.AutoSize = True
        Me.todayfat.Font = New System.Drawing.Font("Calibri", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.todayfat.Location = New System.Drawing.Point(83, 66)
        Me.todayfat.Name = "todayfat"
        Me.todayfat.Size = New System.Drawing.Size(25, 18)
        Me.todayfat.TabIndex = 3
        Me.todayfat.Text = "fat"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Calibri", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(22, 66)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(34, 18)
        Me.Label11.TabIndex = 2
        Me.Label11.Text = "Fat: "
        '
        'todaymilk
        '
        Me.todaymilk.AutoSize = True
        Me.todaymilk.Font = New System.Drawing.Font("Calibri", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.todaymilk.Location = New System.Drawing.Point(80, 34)
        Me.todaymilk.Name = "todaymilk"
        Me.todaymilk.Size = New System.Drawing.Size(49, 18)
        Me.todaymilk.TabIndex = 1
        Me.todaymilk.Text = "milkltr"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Calibri", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(22, 34)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(42, 18)
        Me.Label8.TabIndex = 0
        Me.Label8.Text = "Milk: "
        '
        'editBtn
        '
        Me.editBtn.Location = New System.Drawing.Point(171, 418)
        Me.editBtn.Name = "editBtn"
        Me.editBtn.Size = New System.Drawing.Size(52, 27)
        Me.editBtn.TabIndex = 6
        Me.editBtn.Text = "edit"
        Me.editBtn.UseVisualStyleBackColor = True
        '
        'saveBtn
        '
        Me.saveBtn.Location = New System.Drawing.Point(115, 418)
        Me.saveBtn.Name = "saveBtn"
        Me.saveBtn.Size = New System.Drawing.Size(52, 27)
        Me.saveBtn.TabIndex = 5
        Me.saveBtn.Text = "Save"
        Me.saveBtn.UseVisualStyleBackColor = True
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(6, 228)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(108, 13)
        Me.Label7.TabIndex = 4
        Me.Label7.Text = "Place your note here:"
        '
        'myNote
        '
        Me.myNote.BackColor = System.Drawing.SystemColors.Info
        Me.myNote.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.myNote.Location = New System.Drawing.Point(3, 252)
        Me.myNote.Multiline = True
        Me.myNote.Name = "myNote"
        Me.myNote.Size = New System.Drawing.Size(220, 160)
        Me.myNote.TabIndex = 3
        Me.myNote.Text = "This is test"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Calibri", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(79, 23)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(52, 23)
        Me.Label5.TabIndex = 2
        Me.Label5.Text = "Time:"
        '
        'currTime
        '
        Me.currTime.AutoSize = True
        Me.currTime.Font = New System.Drawing.Font("Calibri", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.currTime.Location = New System.Drawing.Point(137, 23)
        Me.currTime.Name = "currTime"
        Me.currTime.Size = New System.Drawing.Size(86, 23)
        Me.currTime.TabIndex = 1
        Me.currTime.Text = "hh:mm:ss"
        '
        'MonthCalendar1
        '
        Me.MonthCalendar1.Location = New System.Drawing.Point(-1, 55)
        Me.MonthCalendar1.Name = "MonthCalendar1"
        Me.MonthCalendar1.TabIndex = 0
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(1028, 2)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(113, 19)
        Me.Label9.TabIndex = 8
        Me.Label9.Text = "Welcome admin"
        '
        'Timer1
        '
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.Label9)
        Me.Panel2.Controls.Add(Me.shareholders)
        Me.Panel2.Controls.Add(Me.TextBox2)
        Me.Panel2.Controls.Add(Me.TextBox1)
        Me.Panel2.Controls.Add(Me.searchItem)
        Me.Panel2.Controls.Add(Me.Label6)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 26)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1144, 49)
        Me.Panel2.TabIndex = 3
        '
        'shareholders
        '
        Me.shareholders.AutoSize = True
        Me.shareholders.Font = New System.Drawing.Font("Calibri", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.shareholders.Location = New System.Drawing.Point(420, 10)
        Me.shareholders.Name = "shareholders"
        Me.shareholders.Size = New System.Drawing.Size(131, 27)
        Me.shareholders.TabIndex = 10
        Me.shareholders.Text = "Shareholders"
        Me.shareholders.UseVisualStyleBackColor = True
        '
        'TextBox2
        '
        Me.TextBox2.Location = New System.Drawing.Point(557, 10)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(30, 20)
        Me.TextBox2.TabIndex = 9
        Me.TextBox2.Text = "1"
        '
        'TextBox1
        '
        Me.TextBox1.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox1.Location = New System.Drawing.Point(109, 10)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(184, 27)
        Me.TextBox1.TabIndex = 8
        '
        'searchItem
        '
        Me.searchItem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.searchItem.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.searchItem.FormattingEnabled = True
        Me.searchItem.Items.AddRange(New Object() {"ID", "Name", "Address"})
        Me.searchItem.Location = New System.Drawing.Point(299, 10)
        Me.searchItem.Name = "searchItem"
        Me.searchItem.Size = New System.Drawing.Size(115, 27)
        Me.searchItem.TabIndex = 7
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Calibri", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(36, 14)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(67, 23)
        Me.Label6.TabIndex = 6
        Me.Label6.Text = "Search:"
        '
        'milkStore
        '
        Me.milkStore.BackColor = System.Drawing.Color.SeaGreen
        Me.milkStore.FlatAppearance.BorderSize = 0
        Me.milkStore.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.milkStore.Font = New System.Drawing.Font("Calibri", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.milkStore.ForeColor = System.Drawing.Color.White
        Me.milkStore.Location = New System.Drawing.Point(12, 522)
        Me.milkStore.Name = "milkStore"
        Me.milkStore.Size = New System.Drawing.Size(224, 55)
        Me.milkStore.TabIndex = 11
        Me.milkStore.Text = "Store Milk"
        Me.milkStore.UseVisualStyleBackColor = False
        '
        'Timer2
        '
        Me.Timer2.Interval = 1000
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.DataGridView1.BackgroundColor = System.Drawing.SystemColors.ButtonFace
        Me.DataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DataGridView1.DefaultCellStyle = DataGridViewCellStyle1
        Me.DataGridView1.Location = New System.Drawing.Point(5, 77)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.Size = New System.Drawing.Size(1119, 404)
        Me.DataGridView1.TabIndex = 13
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.SeaGreen
        Me.Button1.FlatAppearance.BorderSize = 0
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.Font = New System.Drawing.Font("Calibri", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.ForeColor = System.Drawing.Color.White
        Me.Button1.Location = New System.Drawing.Point(272, 521)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(224, 55)
        Me.Button1.TabIndex = 15
        Me.Button1.Text = "Print All"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'PictureBox1
        '
        Me.PictureBox1.BackgroundImage = Global.dairyProjectOfficial.My.Resources.Resources.PINE_SOFTWARE_KO
        Me.PictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PictureBox1.Location = New System.Drawing.Point(3, 585)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(1141, 99)
        Me.PictureBox1.TabIndex = 14
        Me.PictureBox1.TabStop = False
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Calibri", 26.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(327, 232)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(278, 42)
        Me.Label10.TabIndex = 16
        Me.Label10.Text = "Farmers not found"
        '
        'HomePage
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1370, 711)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.milkStore)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.rightSidebar)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "HomePage"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Milk Dairy "
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.rightSidebar.ResumeLayout(False)
        Me.rightSidebar.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents FileToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SettingToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents HelpToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SignOutToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ChangePasswordToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents ToolsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents AddCustomerToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ExitToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents rightSidebar As Panel
    Friend WithEvents MonthCalendar1 As MonthCalendar
    Friend WithEvents Timer1 As Timer
    Friend WithEvents Label5 As Label
    Friend WithEvents currTime As Label
    Friend WithEvents AddFarmerToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Panel2 As Panel
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents searchItem As ComboBox
    Friend WithEvents Label6 As Label
    Friend WithEvents milkStore As Button
    Friend WithEvents editBtn As Button
    Friend WithEvents saveBtn As Button
    Friend WithEvents Label7 As Label
    Friend WithEvents myNote As TextBox
    Friend WithEvents OtherSettingToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Timer2 As Timer
    Friend WithEvents TextBox2 As TextBox
    Friend WithEvents AddUsersToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents HelpToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents AboutUsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents todaylacto As Label
    Friend WithEvents Label13 As Label
    Friend WithEvents todayfat As Label
    Friend WithEvents Label11 As Label
    Friend WithEvents todaymilk As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents Button1 As Button
    Friend WithEvents PrintDataToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents shareholders As CheckBox
    Friend WithEvents ToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents LoanToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents StatementsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents EMICalculatorToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents BonusToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents AccountSettingToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents YearDataAnalysisToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ViewAllToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Label9 As Label
    Friend WithEvents ShareAmountToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Label10 As Label
    Friend WithEvents LicenseToolStripMenuItem As ToolStripMenuItem
End Class
