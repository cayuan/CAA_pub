<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CAA_USR_UI
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(CAA_USR_UI))
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.cboWeiType = New System.Windows.Forms.ComboBox()
        Me.cmdAssignFiles_Choose = New System.Windows.Forms.Button()
        Me.dgWeiFile = New System.Windows.Forms.DataGridView()
        Me.cmdRegenALLWei = New System.Windows.Forms.Button()
        Me.cmdAssignFiles_Assign = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.cmdChange_WorkDir = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmdFChange_WorkDir = New System.Windows.Forms.Button()
        Me.txtUserFilePath = New System.Windows.Forms.TextBox()
        Me.cmdcmdChange_BaseDBDir = New System.Windows.Forms.Button()
        Me.txtBaseDBDir = New System.Windows.Forms.TextBox()
        Me.cmdFChange_BaseDBDir = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtWorkDir = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog()
        Me.cmdUSERRead = New System.Windows.Forms.Button()
        Me.cmdUSERSave = New System.Windows.Forms.Button()
        Me.cmdExit = New System.Windows.Forms.Button()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.txtSelectedFile = New System.Windows.Forms.TextBox()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.dgWeiFile, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabControl1.Location = New System.Drawing.Point(4, 9)
        Me.TabControl1.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(583, 522)
        Me.TabControl1.TabIndex = 36
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.Panel2)
        Me.TabPage1.Controls.Add(Me.Panel1)
        Me.TabPage1.Controls.Add(Me.Label5)
        Me.TabPage1.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabPage1.Location = New System.Drawing.Point(4, 24)
        Me.TabPage1.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.TabPage1.Size = New System.Drawing.Size(575, 494)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Setup"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.txtSelectedFile)
        Me.Panel2.Controls.Add(Me.Label6)
        Me.Panel2.Controls.Add(Me.cboWeiType)
        Me.Panel2.Controls.Add(Me.cmdAssignFiles_Choose)
        Me.Panel2.Controls.Add(Me.dgWeiFile)
        Me.Panel2.Controls.Add(Me.cmdRegenALLWei)
        Me.Panel2.Controls.Add(Me.cmdAssignFiles_Assign)
        Me.Panel2.Location = New System.Drawing.Point(14, 155)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(553, 327)
        Me.Panel2.TabIndex = 37
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Calibri", 14.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(4, 8)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(209, 23)
        Me.Label6.TabIndex = 91
        Me.Label6.Text = "Source of Weighting Files"
        '
        'cboWeiType
        '
        Me.cboWeiType.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboWeiType.FormattingEnabled = True
        Me.cboWeiType.Location = New System.Drawing.Point(8, 41)
        Me.cboWeiType.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.cboWeiType.Name = "cboWeiType"
        Me.cboWeiType.Size = New System.Drawing.Size(190, 23)
        Me.cboWeiType.TabIndex = 8
        '
        'cmdAssignFiles_Choose
        '
        Me.cmdAssignFiles_Choose.BackColor = System.Drawing.Color.Bisque
        Me.cmdAssignFiles_Choose.Image = CType(resources.GetObject("cmdAssignFiles_Choose.Image"), System.Drawing.Image)
        Me.cmdAssignFiles_Choose.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdAssignFiles_Choose.Location = New System.Drawing.Point(274, 41)
        Me.cmdAssignFiles_Choose.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.cmdAssignFiles_Choose.Name = "cmdAssignFiles_Choose"
        Me.cmdAssignFiles_Choose.Size = New System.Drawing.Size(76, 63)
        Me.cmdAssignFiles_Choose.TabIndex = 88
        Me.cmdAssignFiles_Choose.Text = "Choose"
        Me.cmdAssignFiles_Choose.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdAssignFiles_Choose.UseVisualStyleBackColor = False
        '
        'dgWeiFile
        '
        Me.dgWeiFile.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgWeiFile.Location = New System.Drawing.Point(4, 110)
        Me.dgWeiFile.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.dgWeiFile.Name = "dgWeiFile"
        Me.dgWeiFile.Size = New System.Drawing.Size(531, 185)
        Me.dgWeiFile.TabIndex = 6
        '
        'cmdRegenALLWei
        '
        Me.cmdRegenALLWei.Location = New System.Drawing.Point(442, 77)
        Me.cmdRegenALLWei.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.cmdRegenALLWei.Name = "cmdRegenALLWei"
        Me.cmdRegenALLWei.Size = New System.Drawing.Size(88, 27)
        Me.cmdRegenALLWei.TabIndex = 7
        Me.cmdRegenALLWei.Text = "ReGenALL"
        Me.cmdRegenALLWei.UseVisualStyleBackColor = True
        '
        'cmdAssignFiles_Assign
        '
        Me.cmdAssignFiles_Assign.BackColor = System.Drawing.Color.MistyRose
        Me.cmdAssignFiles_Assign.Image = CType(resources.GetObject("cmdAssignFiles_Assign.Image"), System.Drawing.Image)
        Me.cmdAssignFiles_Assign.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdAssignFiles_Assign.Location = New System.Drawing.Point(358, 41)
        Me.cmdAssignFiles_Assign.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.cmdAssignFiles_Assign.Name = "cmdAssignFiles_Assign"
        Me.cmdAssignFiles_Assign.Size = New System.Drawing.Size(76, 63)
        Me.cmdAssignFiles_Assign.TabIndex = 83
        Me.cmdAssignFiles_Assign.Text = "Assign"
        Me.cmdAssignFiles_Assign.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdAssignFiles_Assign.UseVisualStyleBackColor = False
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.cmdChange_WorkDir)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.cmdFChange_WorkDir)
        Me.Panel1.Controls.Add(Me.txtUserFilePath)
        Me.Panel1.Controls.Add(Me.cmdcmdChange_BaseDBDir)
        Me.Panel1.Controls.Add(Me.txtBaseDBDir)
        Me.Panel1.Controls.Add(Me.cmdFChange_BaseDBDir)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.txtWorkDir)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Location = New System.Drawing.Point(14, 34)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(553, 115)
        Me.Panel1.TabIndex = 37
        '
        'cmdChange_WorkDir
        '
        Me.cmdChange_WorkDir.Location = New System.Drawing.Point(441, 71)
        Me.cmdChange_WorkDir.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.cmdChange_WorkDir.Name = "cmdChange_WorkDir"
        Me.cmdChange_WorkDir.Size = New System.Drawing.Size(88, 31)
        Me.cmdChange_WorkDir.TabIndex = 94
        Me.cmdChange_WorkDir.Text = "Change"
        Me.cmdChange_WorkDir.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(4, 10)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(103, 19)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "USER File Path"
        '
        'cmdFChange_WorkDir
        '
        Me.cmdFChange_WorkDir.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdFChange_WorkDir.Location = New System.Drawing.Point(346, 71)
        Me.cmdFChange_WorkDir.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.cmdFChange_WorkDir.Name = "cmdFChange_WorkDir"
        Me.cmdFChange_WorkDir.Size = New System.Drawing.Size(88, 31)
        Me.cmdFChange_WorkDir.TabIndex = 93
        Me.cmdFChange_WorkDir.Text = "F. Change"
        Me.cmdFChange_WorkDir.UseVisualStyleBackColor = True
        '
        'txtUserFilePath
        '
        Me.txtUserFilePath.Location = New System.Drawing.Point(130, 7)
        Me.txtUserFilePath.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.txtUserFilePath.Name = "txtUserFilePath"
        Me.txtUserFilePath.ReadOnly = True
        Me.txtUserFilePath.Size = New System.Drawing.Size(208, 27)
        Me.txtUserFilePath.TabIndex = 0
        '
        'cmdcmdChange_BaseDBDir
        '
        Me.cmdcmdChange_BaseDBDir.Location = New System.Drawing.Point(441, 35)
        Me.cmdcmdChange_BaseDBDir.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.cmdcmdChange_BaseDBDir.Name = "cmdcmdChange_BaseDBDir"
        Me.cmdcmdChange_BaseDBDir.Size = New System.Drawing.Size(88, 31)
        Me.cmdcmdChange_BaseDBDir.TabIndex = 92
        Me.cmdcmdChange_BaseDBDir.Text = "Change"
        Me.cmdcmdChange_BaseDBDir.UseVisualStyleBackColor = True
        '
        'txtBaseDBDir
        '
        Me.txtBaseDBDir.Location = New System.Drawing.Point(130, 40)
        Me.txtBaseDBDir.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.txtBaseDBDir.Name = "txtBaseDBDir"
        Me.txtBaseDBDir.ReadOnly = True
        Me.txtBaseDBDir.Size = New System.Drawing.Size(208, 27)
        Me.txtBaseDBDir.TabIndex = 2
        '
        'cmdFChange_BaseDBDir
        '
        Me.cmdFChange_BaseDBDir.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdFChange_BaseDBDir.Location = New System.Drawing.Point(346, 35)
        Me.cmdFChange_BaseDBDir.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.cmdFChange_BaseDBDir.Name = "cmdFChange_BaseDBDir"
        Me.cmdFChange_BaseDBDir.Size = New System.Drawing.Size(88, 31)
        Me.cmdFChange_BaseDBDir.TabIndex = 91
        Me.cmdFChange_BaseDBDir.Text = "F. Change"
        Me.cmdFChange_BaseDBDir.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(4, 44)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(97, 19)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Base DB Path"
        '
        'txtWorkDir
        '
        Me.txtWorkDir.Location = New System.Drawing.Point(130, 75)
        Me.txtWorkDir.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.txtWorkDir.Name = "txtWorkDir"
        Me.txtWorkDir.ReadOnly = True
        Me.txtWorkDir.Size = New System.Drawing.Size(208, 27)
        Me.txtWorkDir.TabIndex = 4
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(4, 79)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(65, 19)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Work Dir"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Calibri", 14.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(10, 8)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(158, 23)
        Me.Label5.TabIndex = 90
        Me.Label5.Text = "USER Profile Setup"
        '
        'TabPage2
        '
        Me.TabPage2.Location = New System.Drawing.Point(4, 24)
        Me.TabPage2.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.TabPage2.Size = New System.Drawing.Size(575, 494)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Info"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'cmdUSERRead
        '
        Me.cmdUSERRead.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmdUSERRead.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdUSERRead.Image = Global.CAA.My.Resources.Resources.recongnize_sia1
        Me.cmdUSERRead.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdUSERRead.Location = New System.Drawing.Point(591, 365)
        Me.cmdUSERRead.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.cmdUSERRead.Name = "cmdUSERRead"
        Me.cmdUSERRead.Size = New System.Drawing.Size(77, 72)
        Me.cmdUSERRead.TabIndex = 38
        Me.cmdUSERRead.Text = "Read"
        Me.cmdUSERRead.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdUSERRead.UseVisualStyleBackColor = False
        '
        'cmdUSERSave
        '
        Me.cmdUSERSave.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.cmdUSERSave.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdUSERSave.Image = Global.CAA.My.Resources.Resources.write_ss
        Me.cmdUSERSave.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdUSERSave.Location = New System.Drawing.Point(591, 287)
        Me.cmdUSERSave.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.cmdUSERSave.Name = "cmdUSERSave"
        Me.cmdUSERSave.Size = New System.Drawing.Size(77, 72)
        Me.cmdUSERSave.TabIndex = 37
        Me.cmdUSERSave.Text = "Save"
        Me.cmdUSERSave.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdUSERSave.UseVisualStyleBackColor = False
        '
        'cmdExit
        '
        Me.cmdExit.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmdExit.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdExit.Image = Global.CAA.My.Resources.Resources.go_out
        Me.cmdExit.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdExit.Location = New System.Drawing.Point(591, 443)
        Me.cmdExit.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.cmdExit.Name = "cmdExit"
        Me.cmdExit.Size = New System.Drawing.Size(77, 72)
        Me.cmdExit.TabIndex = 35
        Me.cmdExit.Text = "Back"
        Me.cmdExit.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdExit.UseVisualStyleBackColor = False
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'txtSelectedFile
        '
        Me.txtSelectedFile.Location = New System.Drawing.Point(8, 70)
        Me.txtSelectedFile.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.txtSelectedFile.Name = "txtSelectedFile"
        Me.txtSelectedFile.ReadOnly = True
        Me.txtSelectedFile.Size = New System.Drawing.Size(231, 27)
        Me.txtSelectedFile.TabIndex = 92
        '
        'CAA_USR_UI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(683, 537)
        Me.Controls.Add(Me.cmdUSERRead)
        Me.Controls.Add(Me.cmdUSERSave)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.cmdExit)
        Me.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.Name = "CAA_USR_UI"
        Me.Text = "CAA_USR_UI"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.dgWeiFile, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents cmdExit As Button
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents cmdAssignFiles_Choose As Button
    Friend WithEvents cmdAssignFiles_Assign As Button
    Friend WithEvents cboWeiType As ComboBox
    Friend WithEvents cmdRegenALLWei As Button
    Friend WithEvents dgWeiFile As DataGridView
    Friend WithEvents Label3 As Label
    Friend WithEvents txtWorkDir As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents txtBaseDBDir As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents txtUserFilePath As TextBox
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents cmdFChange_BaseDBDir As Button
    Friend WithEvents Label5 As Label
    Friend WithEvents cmdChange_WorkDir As Button
    Friend WithEvents cmdFChange_WorkDir As Button
    Friend WithEvents cmdcmdChange_BaseDBDir As Button
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Label6 As Label
    Friend WithEvents FolderBrowserDialog1 As FolderBrowserDialog
    Friend WithEvents cmdUSERSave As Button
    Friend WithEvents cmdUSERRead As Button
    Friend WithEvents OpenFileDialog1 As OpenFileDialog
    Friend WithEvents txtSelectedFile As TextBox
End Class
