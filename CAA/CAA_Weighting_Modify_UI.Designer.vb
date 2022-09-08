<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CAA_Weighting_Modify_UI
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
        Me.cmdUSERRead = New System.Windows.Forms.Button()
        Me.cmdUSERSave = New System.Windows.Forms.Button()
        Me.cmdExit = New System.Windows.Forms.Button()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.cboWeiType = New System.Windows.Forms.ComboBox()
        Me.dgWei = New System.Windows.Forms.DataGridView()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.dgWei, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdUSERRead
        '
        Me.cmdUSERRead.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmdUSERRead.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdUSERRead.Image = Global.CAA.My.Resources.Resources.recongnize_sia1
        Me.cmdUSERRead.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdUSERRead.Location = New System.Drawing.Point(590, 290)
        Me.cmdUSERRead.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.cmdUSERRead.Name = "cmdUSERRead"
        Me.cmdUSERRead.Size = New System.Drawing.Size(70, 69)
        Me.cmdUSERRead.TabIndex = 41
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
        Me.cmdUSERSave.Location = New System.Drawing.Point(590, 204)
        Me.cmdUSERSave.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.cmdUSERSave.Name = "cmdUSERSave"
        Me.cmdUSERSave.Size = New System.Drawing.Size(70, 66)
        Me.cmdUSERSave.TabIndex = 40
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
        Me.cmdExit.Location = New System.Drawing.Point(590, 379)
        Me.cmdExit.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.cmdExit.Name = "cmdExit"
        Me.cmdExit.Size = New System.Drawing.Size(70, 69)
        Me.cmdExit.TabIndex = 39
        Me.cmdExit.Text = "Back"
        Me.cmdExit.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdExit.UseVisualStyleBackColor = False
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabControl1.Location = New System.Drawing.Point(3, 12)
        Me.TabControl1.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(583, 441)
        Me.TabControl1.TabIndex = 42
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.Panel2)
        Me.TabPage1.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabPage1.Location = New System.Drawing.Point(4, 24)
        Me.TabPage1.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.TabPage1.Size = New System.Drawing.Size(575, 413)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Setup"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.Label6)
        Me.Panel2.Controls.Add(Me.cboWeiType)
        Me.Panel2.Controls.Add(Me.dgWei)
        Me.Panel2.Location = New System.Drawing.Point(7, 6)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(553, 397)
        Me.Panel2.TabIndex = 37
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Calibri", 14.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(4, 8)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(171, 23)
        Me.Label6.TabIndex = 91
        Me.Label6.Text = "Weighting Table Edit"
        '
        'cboWeiType
        '
        Me.cboWeiType.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboWeiType.FormattingEnabled = True
        Me.cboWeiType.Location = New System.Drawing.Point(8, 34)
        Me.cboWeiType.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.cboWeiType.Name = "cboWeiType"
        Me.cboWeiType.Size = New System.Drawing.Size(237, 27)
        Me.cboWeiType.TabIndex = 8
        '
        'dgWei
        '
        Me.dgWei.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgWei.Location = New System.Drawing.Point(8, 67)
        Me.dgWei.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.dgWei.Name = "dgWei"
        Me.dgWei.Size = New System.Drawing.Size(545, 315)
        Me.dgWei.TabIndex = 6
        '
        'TabPage2
        '
        Me.TabPage2.Location = New System.Drawing.Point(4, 24)
        Me.TabPage2.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.TabPage2.Size = New System.Drawing.Size(575, 413)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Info"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'CAA_Weighting_Modify_UI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(686, 461)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.cmdUSERRead)
        Me.Controls.Add(Me.cmdUSERSave)
        Me.Controls.Add(Me.cmdExit)
        Me.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.Name = "CAA_Weighting_Modify_UI"
        Me.Text = "CAA_Weighting_Modify_UI"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.dgWei, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents cmdUSERRead As Button
    Friend WithEvents cmdUSERSave As Button
    Friend WithEvents cmdExit As Button
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Label6 As Label
    Friend WithEvents cboWeiType As ComboBox
    Friend WithEvents dgWei As DataGridView
    Friend WithEvents TabPage2 As TabPage
End Class
