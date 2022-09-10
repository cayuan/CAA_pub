<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.GroupBox13 = New System.Windows.Forms.GroupBox()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.lblVersion = New System.Windows.Forms.Label()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.picStamp = New System.Windows.Forms.PictureBox()
        Me.lblCopyright = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.LinkLabel2 = New System.Windows.Forms.LinkLabel()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.LinkLabel1 = New System.Windows.Forms.LinkLabel()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AsdToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.ExitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.EditToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.WeightingToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.HelpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmdGetOrder = New System.Windows.Forms.Button()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.cmdChangeWeighting = New System.Windows.Forms.Button()
        Me.cmdtest = New System.Windows.Forms.Button()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.cmdChangeDir = New System.Windows.Forms.Button()
        Me.txtWorkDir = New System.Windows.Forms.TextBox()
        Me.cmdFly = New System.Windows.Forms.Button()
        Me.cmdStop = New System.Windows.Forms.Button()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog()
        Me.txtOutput = New System.Windows.Forms.TextBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.cmdExit = New System.Windows.Forms.Button()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.cmdARTCO = New System.Windows.Forms.Button()
        Me.GroupBox13.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        CType(Me.picStamp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStrip1.SuspendLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox13
        '
        Me.GroupBox13.Controls.Add(Me.Label22)
        Me.GroupBox13.Controls.Add(Me.lblVersion)
        Me.GroupBox13.Location = New System.Drawing.Point(6, 6)
        Me.GroupBox13.Name = "GroupBox13"
        Me.GroupBox13.Size = New System.Drawing.Size(301, 57)
        Me.GroupBox13.TabIndex = 32
        Me.GroupBox13.TabStop = False
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.Location = New System.Drawing.Point(5, 10)
        Me.Label22.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(164, 24)
        Me.Label22.TabIndex = 25
        Me.Label22.Text = "CAA Scheduling"
        '
        'lblVersion
        '
        Me.lblVersion.AutoSize = True
        Me.lblVersion.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVersion.Location = New System.Drawing.Point(28, 34)
        Me.lblVersion.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblVersion.Name = "lblVersion"
        Me.lblVersion.Size = New System.Drawing.Size(38, 20)
        Me.lblVersion.TabIndex = 26
        Me.lblVersion.Text = "v0.1"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.picStamp)
        Me.GroupBox3.Controls.Add(Me.lblCopyright)
        Me.GroupBox3.Controls.Add(Me.PictureBox1)
        Me.GroupBox3.Controls.Add(Me.LinkLabel2)
        Me.GroupBox3.Controls.Add(Me.Label13)
        Me.GroupBox3.Controls.Add(Me.Label14)
        Me.GroupBox3.Controls.Add(Me.LinkLabel1)
        Me.GroupBox3.Location = New System.Drawing.Point(3, 438)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(335, 107)
        Me.GroupBox3.TabIndex = 33
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Author Contact Infomation"
        '
        'picStamp
        '
        Me.picStamp.Image = Global.CAA.My.Resources.Resources.CA_Orange
        Me.picStamp.Location = New System.Drawing.Point(280, 11)
        Me.picStamp.Name = "picStamp"
        Me.picStamp.Size = New System.Drawing.Size(50, 50)
        Me.picStamp.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picStamp.TabIndex = 32
        Me.picStamp.TabStop = False
        '
        'lblCopyright
        '
        Me.lblCopyright.AutoSize = True
        Me.lblCopyright.Font = New System.Drawing.Font("Arial Narrow", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCopyright.Location = New System.Drawing.Point(9, 86)
        Me.lblCopyright.Name = "lblCopyright"
        Me.lblCopyright.Size = New System.Drawing.Size(267, 15)
        Me.lblCopyright.TabIndex = 23
        Me.lblCopyright.Text = "Copyright © 2020,2021 Dr. Cadmus Yuan, All rights reserved. "
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.CAA.My.Resources.Resources.fcu
        Me.PictureBox1.InitialImage = CType(resources.GetObject("PictureBox1.InitialImage"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(12, 18)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(149, 67)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox1.TabIndex = 18
        Me.PictureBox1.TabStop = False
        '
        'LinkLabel2
        '
        Me.LinkLabel2.AutoSize = True
        Me.LinkLabel2.Location = New System.Drawing.Point(174, 50)
        Me.LinkLabel2.Name = "LinkLabel2"
        Me.LinkLabel2.Size = New System.Drawing.Size(113, 13)
        Me.LinkLabel2.TabIndex = 22
        Me.LinkLabel2.TabStop = True
        Me.LinkLabel2.Text = "www.mcae.fcu.edu.tw"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(174, 24)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(90, 13)
        Me.Label13.TabIndex = 19
        Me.Label13.Text = "Dr. Cadmus Yuan"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(174, 37)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(37, 13)
        Me.Label14.TabIndex = 21
        Me.Label14.Text = "MCAE"
        '
        'LinkLabel1
        '
        Me.LinkLabel1.AutoSize = True
        Me.LinkLabel1.Location = New System.Drawing.Point(174, 66)
        Me.LinkLabel1.Name = "LinkLabel1"
        Me.LinkLabel1.Size = New System.Drawing.Size(103, 13)
        Me.LinkLabel1.TabIndex = 20
        Me.LinkLabel1.TabStop = True
        Me.LinkLabel1.Text = "cayuan@fcu.edu.tw"
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem, Me.EditToolStripMenuItem, Me.HelpToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(1115, 24)
        Me.MenuStrip1.TabIndex = 35
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AsdToolStripMenuItem, Me.ToolStripSeparator1, Me.ToolStripMenuItem1, Me.ToolStripSeparator2, Me.ExitToolStripMenuItem})
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        Me.FileToolStripMenuItem.Size = New System.Drawing.Size(37, 20)
        Me.FileToolStripMenuItem.Text = "File"
        '
        'AsdToolStripMenuItem
        '
        Me.AsdToolStripMenuItem.Name = "AsdToolStripMenuItem"
        Me.AsdToolStripMenuItem.Size = New System.Drawing.Size(116, 22)
        Me.AsdToolStripMenuItem.Text = "Open"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(113, 6)
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(116, 22)
        Me.ToolStripMenuItem1.Text = "Options"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(113, 6)
        '
        'ExitToolStripMenuItem
        '
        Me.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem"
        Me.ExitToolStripMenuItem.Size = New System.Drawing.Size(116, 22)
        Me.ExitToolStripMenuItem.Text = "Exit"
        '
        'EditToolStripMenuItem
        '
        Me.EditToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.WeightingToolStripMenuItem})
        Me.EditToolStripMenuItem.Name = "EditToolStripMenuItem"
        Me.EditToolStripMenuItem.Size = New System.Drawing.Size(39, 20)
        Me.EditToolStripMenuItem.Text = "Edit"
        '
        'WeightingToolStripMenuItem
        '
        Me.WeightingToolStripMenuItem.Name = "WeightingToolStripMenuItem"
        Me.WeightingToolStripMenuItem.Size = New System.Drawing.Size(129, 22)
        Me.WeightingToolStripMenuItem.Text = "Weighting"
        '
        'HelpToolStripMenuItem
        '
        Me.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem"
        Me.HelpToolStripMenuItem.Size = New System.Drawing.Size(44, 20)
        Me.HelpToolStripMenuItem.Text = "Help"
        '
        'GroupBox1
        '
        Me.GroupBox1.Location = New System.Drawing.Point(831, 450)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(88, 103)
        Me.GroupBox1.TabIndex = 36
        Me.GroupBox1.TabStop = False
        '
        'PictureBox2
        '
        Me.PictureBox2.Image = Global.CAA.My.Resources.Resources.sg_logo
        Me.PictureBox2.Location = New System.Drawing.Point(621, 450)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(73, 86)
        Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox2.TabIndex = 37
        Me.PictureBox2.TabStop = False
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(0, 27)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(848, 577)
        Me.TabControl1.TabIndex = 37
        '
        'TabPage1
        '
        Me.TabPage1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TabPage1.Controls.Add(Me.PictureBox2)
        Me.TabPage1.Controls.Add(Me.Panel4)
        Me.TabPage1.Controls.Add(Me.Panel3)
        Me.TabPage1.Controls.Add(Me.cmdtest)
        Me.TabPage1.Controls.Add(Me.Panel2)
        Me.TabPage1.Controls.Add(Me.cmdFly)
        Me.TabPage1.Controls.Add(Me.cmdStop)
        Me.TabPage1.Controls.Add(Me.GroupBox1)
        Me.TabPage1.Controls.Add(Me.GroupBox13)
        Me.TabPage1.ForeColor = System.Drawing.Color.Black
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(840, 551)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Devt"
        '
        'Panel4
        '
        Me.Panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel4.Controls.Add(Me.cmdARTCO)
        Me.Panel4.Controls.Add(Me.Button2)
        Me.Panel4.Controls.Add(Me.Label1)
        Me.Panel4.Controls.Add(Me.cmdGetOrder)
        Me.Panel4.Location = New System.Drawing.Point(15, 163)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(380, 73)
        Me.Panel4.TabIndex = 95
        '
        'Button2
        '
        Me.Button2.Font = New System.Drawing.Font("Calibri", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button2.Image = CType(resources.GetObject("Button2.Image"), System.Drawing.Image)
        Me.Button2.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.Button2.Location = New System.Drawing.Point(303, 11)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(63, 56)
        Me.Button2.TabIndex = 94
        Me.Button2.Text = "Chg.Dir"
        Me.Button2.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(4, 6)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(82, 17)
        Me.Label1.TabIndex = 93
        Me.Label1.Text = "Initial inputs"
        '
        'cmdGetOrder
        '
        Me.cmdGetOrder.Font = New System.Drawing.Font("Calibri", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdGetOrder.Image = CType(resources.GetObject("cmdGetOrder.Image"), System.Drawing.Image)
        Me.cmdGetOrder.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdGetOrder.Location = New System.Drawing.Point(150, 12)
        Me.cmdGetOrder.Name = "cmdGetOrder"
        Me.cmdGetOrder.Size = New System.Drawing.Size(63, 56)
        Me.cmdGetOrder.TabIndex = 91
        Me.cmdGetOrder.Text = "Order"
        Me.cmdGetOrder.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdGetOrder.UseVisualStyleBackColor = True
        '
        'Panel3
        '
        Me.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel3.Controls.Add(Me.cmdChangeWeighting)
        Me.Panel3.Location = New System.Drawing.Point(401, 69)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(174, 73)
        Me.Panel3.TabIndex = 95
        '
        'cmdChangeWeighting
        '
        Me.cmdChangeWeighting.Image = CType(resources.GetObject("cmdChangeWeighting.Image"), System.Drawing.Image)
        Me.cmdChangeWeighting.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdChangeWeighting.Location = New System.Drawing.Point(4, 7)
        Me.cmdChangeWeighting.Name = "cmdChangeWeighting"
        Me.cmdChangeWeighting.Size = New System.Drawing.Size(66, 62)
        Me.cmdChangeWeighting.TabIndex = 96
        Me.cmdChangeWeighting.Text = "Change Weighting"
        Me.cmdChangeWeighting.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdChangeWeighting.UseVisualStyleBackColor = True
        '
        'cmdtest
        '
        Me.cmdtest.Location = New System.Drawing.Point(54, 386)
        Me.cmdtest.Name = "cmdtest"
        Me.cmdtest.Size = New System.Drawing.Size(75, 23)
        Me.cmdtest.TabIndex = 95
        Me.cmdtest.Text = "test"
        Me.cmdtest.UseVisualStyleBackColor = True
        '
        'Panel2
        '
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Controls.Add(Me.Button1)
        Me.Panel2.Controls.Add(Me.Label5)
        Me.Panel2.Controls.Add(Me.cmdChangeDir)
        Me.Panel2.Controls.Add(Me.txtWorkDir)
        Me.Panel2.Location = New System.Drawing.Point(15, 69)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(380, 73)
        Me.Panel2.TabIndex = 94
        '
        'Button1
        '
        Me.Button1.Font = New System.Drawing.Font("Calibri", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.Image = CType(resources.GetObject("Button1.Image"), System.Drawing.Image)
        Me.Button1.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.Button1.Location = New System.Drawing.Point(303, 11)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(63, 56)
        Me.Button1.TabIndex = 94
        Me.Button1.Text = "Chg.Dir"
        Me.Button1.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(4, 6)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(86, 17)
        Me.Label5.TabIndex = 93
        Me.Label5.Text = "Working Dir:"
        '
        'cmdChangeDir
        '
        Me.cmdChangeDir.Font = New System.Drawing.Font("Calibri", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdChangeDir.Image = CType(resources.GetObject("cmdChangeDir.Image"), System.Drawing.Image)
        Me.cmdChangeDir.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdChangeDir.Location = New System.Drawing.Point(234, 11)
        Me.cmdChangeDir.Name = "cmdChangeDir"
        Me.cmdChangeDir.Size = New System.Drawing.Size(63, 56)
        Me.cmdChangeDir.TabIndex = 91
        Me.cmdChangeDir.Text = "F. Chg.Dir"
        Me.cmdChangeDir.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdChangeDir.UseVisualStyleBackColor = True
        '
        'txtWorkDir
        '
        Me.txtWorkDir.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtWorkDir.Location = New System.Drawing.Point(3, 26)
        Me.txtWorkDir.Multiline = True
        Me.txtWorkDir.Name = "txtWorkDir"
        Me.txtWorkDir.Size = New System.Drawing.Size(225, 41)
        Me.txtWorkDir.TabIndex = 92
        '
        'cmdFly
        '
        Me.cmdFly.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdFly.Image = CType(resources.GetObject("cmdFly.Image"), System.Drawing.Image)
        Me.cmdFly.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdFly.Location = New System.Drawing.Point(621, 367)
        Me.cmdFly.Name = "cmdFly"
        Me.cmdFly.Size = New System.Drawing.Size(72, 58)
        Me.cmdFly.TabIndex = 80
        Me.cmdFly.Text = "Fly!"
        Me.cmdFly.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdFly.UseVisualStyleBackColor = True
        '
        'cmdStop
        '
        Me.cmdStop.Location = New System.Drawing.Point(699, 387)
        Me.cmdStop.Name = "cmdStop"
        Me.cmdStop.Size = New System.Drawing.Size(75, 38)
        Me.cmdStop.TabIndex = 33
        Me.cmdStop.Text = "Stop!"
        Me.cmdStop.UseVisualStyleBackColor = True
        '
        'TabPage2
        '
        Me.TabPage2.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TabPage2.Controls.Add(Me.GroupBox3)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(919, 551)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Info"
        '
        'txtOutput
        '
        Me.txtOutput.Font = New System.Drawing.Font("Calibri", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOutput.Location = New System.Drawing.Point(0, 3)
        Me.txtOutput.Multiline = True
        Me.txtOutput.Name = "txtOutput"
        Me.txtOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtOutput.Size = New System.Drawing.Size(262, 484)
        Me.txtOutput.TabIndex = 38
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.txtOutput)
        Me.Panel1.Location = New System.Drawing.Point(850, 39)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(265, 487)
        Me.Panel1.TabIndex = 39
        '
        'cmdExit
        '
        Me.cmdExit.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmdExit.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdExit.Image = Global.CAA.My.Resources.Resources.go_out
        Me.cmdExit.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdExit.Location = New System.Drawing.Point(857, 538)
        Me.cmdExit.Name = "cmdExit"
        Me.cmdExit.Size = New System.Drawing.Size(66, 62)
        Me.cmdExit.TabIndex = 34
        Me.cmdExit.Text = "Exit"
        Me.cmdExit.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdExit.UseVisualStyleBackColor = False
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'cmdARTCO
        '
        Me.cmdARTCO.Font = New System.Drawing.Font("Calibri", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdARTCO.Image = CType(resources.GetObject("cmdARTCO.Image"), System.Drawing.Image)
        Me.cmdARTCO.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdARTCO.Location = New System.Drawing.Point(81, 11)
        Me.cmdARTCO.Name = "cmdARTCO"
        Me.cmdARTCO.Size = New System.Drawing.Size(63, 56)
        Me.cmdARTCO.TabIndex = 95
        Me.cmdARTCO.Text = "ART-CO"
        Me.cmdARTCO.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdARTCO.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1115, 607)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.cmdExit)
        Me.Controls.Add(Me.MenuStrip1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.GroupBox13.ResumeLayout(False)
        Me.GroupBox13.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        CType(Me.picStamp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents GroupBox13 As GroupBox
    Friend WithEvents Label22 As Label
    Friend WithEvents lblVersion As Label
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents picStamp As PictureBox
    Friend WithEvents lblCopyright As Label
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents LinkLabel2 As LinkLabel
    Friend WithEvents Label13 As Label
    Friend WithEvents Label14 As Label
    Friend WithEvents LinkLabel1 As LinkLabel
    Friend WithEvents cmdExit As Button
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents FileToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents AsdToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents ExitToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents EditToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents HelpToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents PictureBox2 As PictureBox
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents FolderBrowserDialog1 As FolderBrowserDialog
    Friend WithEvents cmdStop As Button
    Friend WithEvents ToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As ToolStripSeparator
    Friend WithEvents txtOutput As TextBox
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Button1 As Button
    Friend WithEvents Label5 As Label
    Friend WithEvents cmdChangeDir As Button
    Friend WithEvents txtWorkDir As TextBox
    Friend WithEvents cmdFly As Button
    Friend WithEvents cmdtest As Button
    Friend WithEvents WeightingToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Panel3 As Panel
    Friend WithEvents cmdChangeWeighting As Button
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Button2 As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents cmdGetOrder As Button
    Friend WithEvents OpenFileDialog1 As OpenFileDialog
    Friend WithEvents cmdARTCO As Button
End Class
