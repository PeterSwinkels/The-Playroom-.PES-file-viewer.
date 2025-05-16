<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class IntefaceWindow
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
        Me.components = New System.ComponentModel.Container()
        Me.ToolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.DataBox = New System.Windows.Forms.TextBox()
        Me.MenuBar = New System.Windows.Forms.MenuStrip()
        Me.ProgramMainMenu = New System.Windows.Forms.ToolStripMenuItem()
        Me.InformationMenu = New System.Windows.Forms.ToolStripMenuItem()
        Me.QuitMenu = New System.Windows.Forms.ToolStripMenuItem()
        Me.ProgramMainMenuSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.MenuBar.SuspendLayout()
        Me.SuspendLayout()
        '
        'DataBox
        '
        Me.DataBox.AllowDrop = True
        Me.DataBox.BackColor = System.Drawing.SystemColors.Window
        Me.DataBox.CausesValidation = False
        Me.DataBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataBox.Font = New System.Drawing.Font("Consolas", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DataBox.Location = New System.Drawing.Point(0, 24)
        Me.DataBox.MaxLength = 0
        Me.DataBox.Multiline = True
        Me.DataBox.Name = "DataBox"
        Me.DataBox.ReadOnly = True
        Me.DataBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.DataBox.Size = New System.Drawing.Size(800, 426)
        Me.DataBox.TabIndex = 3
        '
        'MenuBar
        '
        Me.MenuBar.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ProgramMainMenu})
        Me.MenuBar.Location = New System.Drawing.Point(0, 0)
        Me.MenuBar.Name = "MenuBar"
        Me.MenuBar.Size = New System.Drawing.Size(800, 24)
        Me.MenuBar.TabIndex = 4
        Me.MenuBar.Text = "MenuStrip1"
        '
        'ProgramMainMenu
        '
        Me.ProgramMainMenu.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.InformationMenu, Me.ProgramMainMenuSeparator1, Me.QuitMenu})
        Me.ProgramMainMenu.Name = "ProgramMainMenu"
        Me.ProgramMainMenu.Size = New System.Drawing.Size(65, 20)
        Me.ProgramMainMenu.Text = "&Program"
        '
        'InformationMenu
        '
        Me.InformationMenu.Name = "InformationMenu"
        Me.InformationMenu.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.I), System.Windows.Forms.Keys)
        Me.InformationMenu.Size = New System.Drawing.Size(180, 22)
        Me.InformationMenu.Text = "&Information"
        '
        'QuitMenu
        '
        Me.QuitMenu.Name = "QuitMenu"
        Me.QuitMenu.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Q), System.Windows.Forms.Keys)
        Me.QuitMenu.Size = New System.Drawing.Size(180, 22)
        Me.QuitMenu.Text = "&Quit"
        '
        'ProgramMainMenuSeparator1
        '
        Me.ProgramMainMenuSeparator1.Name = "ProgramMainMenuSeparator1"
        Me.ProgramMainMenuSeparator1.Size = New System.Drawing.Size(177, 6)
        '
        'IntefaceWindow
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.DataBox)
        Me.Controls.Add(Me.MenuBar)
        Me.KeyPreview = True
        Me.MainMenuStrip = Me.MenuBar
        Me.Name = "IntefaceWindow"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.MenuBar.ResumeLayout(False)
        Me.MenuBar.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents ToolTip As System.Windows.Forms.ToolTip
    Friend WithEvents DataBox As System.Windows.Forms.TextBox
    Friend WithEvents MenuBar As System.Windows.Forms.MenuStrip
    Friend WithEvents ProgramMainMenu As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents InformationMenu As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents QuitMenu As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ProgramMainMenuSeparator1 As System.Windows.Forms.ToolStripSeparator
End Class
