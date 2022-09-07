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
        Me.cmdExit = New System.Windows.Forms.Button()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TextBox3 = New System.Windows.Forms.TextBox()
        Me.dgWeiFile = New System.Windows.Forms.DataGridView()
        Me.cmdRegenALLWei = New System.Windows.Forms.Button()
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.cmdAssignFiles_Assign = New System.Windows.Forms.Button()
        Me.cmdAssignFiles_Choose = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        CType(Me.dgWeiFile, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdExit
        '
        Me.cmdExit.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmdExit.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdExit.Image = Global.CAA.My.Resources.Resources.go_out
        Me.cmdExit.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdExit.Location = New System.Drawing.Point(454, 480)
        Me.cmdExit.Name = "cmdExit"
        Me.cmdExit.Size = New System.Drawing.Size(66, 62)
        Me.cmdExit.TabIndex = 35
        Me.cmdExit.Text = "Back"
        Me.cmdExit.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdExit.UseVisualStyleBackColor = False
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(12, 12)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(436, 534)
        Me.TabControl1.TabIndex = 36
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.Label4)
        Me.TabPage1.Controls.Add(Me.cmdAssignFiles_Choose)
        Me.TabPage1.Controls.Add(Me.cmdAssignFiles_Assign)
        Me.TabPage1.Controls.Add(Me.ComboBox1)
        Me.TabPage1.Controls.Add(Me.cmdRegenALLWei)
        Me.TabPage1.Controls.Add(Me.dgWeiFile)
        Me.TabPage1.Controls.Add(Me.Label3)
        Me.TabPage1.Controls.Add(Me.TextBox3)
        Me.TabPage1.Controls.Add(Me.Label2)
        Me.TabPage1.Controls.Add(Me.TextBox2)
        Me.TabPage1.Controls.Add(Me.Label1)
        Me.TabPage1.Controls.Add(Me.TextBox1)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(428, 508)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Setup"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'TabPage2
        '
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(428, 508)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Info"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(90, 37)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(100, 20)
        Me.TextBox1.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(36, 37)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(39, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Label1"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(36, 74)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(39, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Label2"
        '
        'TextBox2
        '
        Me.TextBox2.Location = New System.Drawing.Point(90, 74)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(100, 20)
        Me.TextBox2.TabIndex = 2
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(36, 112)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(39, 13)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Label3"
        '
        'TextBox3
        '
        Me.TextBox3.Location = New System.Drawing.Point(90, 112)
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.Size = New System.Drawing.Size(100, 20)
        Me.TextBox3.TabIndex = 4
        '
        'dgWeiFile
        '
        Me.dgWeiFile.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgWeiFile.Location = New System.Drawing.Point(6, 294)
        Me.dgWeiFile.Name = "dgWeiFile"
        Me.dgWeiFile.Size = New System.Drawing.Size(416, 146)
        Me.dgWeiFile.TabIndex = 6
        '
        'cmdRegenALLWei
        '
        Me.cmdRegenALLWei.Location = New System.Drawing.Point(223, 167)
        Me.cmdRegenALLWei.Name = "cmdRegenALLWei"
        Me.cmdRegenALLWei.Size = New System.Drawing.Size(75, 23)
        Me.cmdRegenALLWei.TabIndex = 7
        Me.cmdRegenALLWei.Text = "ReGenALL"
        Me.cmdRegenALLWei.UseVisualStyleBackColor = True
        '
        'ComboBox1
        '
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Location = New System.Drawing.Point(39, 169)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(163, 21)
        Me.ComboBox1.TabIndex = 8
        '
        'cmdAssignFiles_Assign
        '
        Me.cmdAssignFiles_Assign.BackColor = System.Drawing.Color.MistyRose
        Me.cmdAssignFiles_Assign.Image = CType(resources.GetObject("cmdAssignFiles_Assign.Image"), System.Drawing.Image)
        Me.cmdAssignFiles_Assign.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdAssignFiles_Assign.Location = New System.Drawing.Point(223, 196)
        Me.cmdAssignFiles_Assign.Name = "cmdAssignFiles_Assign"
        Me.cmdAssignFiles_Assign.Size = New System.Drawing.Size(65, 55)
        Me.cmdAssignFiles_Assign.TabIndex = 83
        Me.cmdAssignFiles_Assign.Text = "Assign"
        Me.cmdAssignFiles_Assign.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdAssignFiles_Assign.UseVisualStyleBackColor = False
        '
        'cmdAssignFiles_Choose
        '
        Me.cmdAssignFiles_Choose.BackColor = System.Drawing.Color.Bisque
        Me.cmdAssignFiles_Choose.Image = CType(resources.GetObject("cmdAssignFiles_Choose.Image"), System.Drawing.Image)
        Me.cmdAssignFiles_Choose.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdAssignFiles_Choose.Location = New System.Drawing.Point(152, 196)
        Me.cmdAssignFiles_Choose.Name = "cmdAssignFiles_Choose"
        Me.cmdAssignFiles_Choose.Size = New System.Drawing.Size(65, 55)
        Me.cmdAssignFiles_Choose.TabIndex = 88
        Me.cmdAssignFiles_Choose.Text = "Choose"
        Me.cmdAssignFiles_Choose.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdAssignFiles_Choose.UseVisualStyleBackColor = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(25, 278)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(39, 13)
        Me.Label4.TabIndex = 89
        Me.Label4.Text = "Label4"
        '
        'CAA_USR_UI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(532, 558)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.cmdExit)
        Me.Name = "CAA_USR_UI"
        Me.Text = "CAA_USR_UI"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        CType(Me.dgWeiFile, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents cmdExit As Button
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents Label4 As Label
    Friend WithEvents cmdAssignFiles_Choose As Button
    Friend WithEvents cmdAssignFiles_Assign As Button
    Friend WithEvents ComboBox1 As ComboBox
    Friend WithEvents cmdRegenALLWei As Button
    Friend WithEvents dgWeiFile As DataGridView
    Friend WithEvents Label3 As Label
    Friend WithEvents TextBox3 As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents TextBox2 As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents TabPage2 As TabPage
End Class
