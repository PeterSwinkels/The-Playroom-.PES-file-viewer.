'This module's imports and settings.
Option Compare Binary
Option Explicit On
Option Infer Off
Option Strict On

Imports System
Imports System.Drawing
Imports System.Environment
Imports System.Linq
Imports System.Windows.Forms

'This module contains this program's interface.
Public Class IntefaceWindow
   'This procedure initializes this window.
   Public Sub New()
      Try
         InitializeComponent()

         My.Application.ChangeCulture("en-US")

         With My.Computer.Screen.WorkingArea
            Me.Size = New Size(CInt(.Width / 1.1), CInt(.Height / 1.1))
         End With

         Me.Text = ProgramInformation()

         ToolTip.SetToolTip(DataBox, "Drag a file into this window to view it.")

         UpdateDataBox( , NewDataBox:=DataBox)
      Catch ExceptionO As Exception
         DisplayException(ExceptionO)
      End Try
   End Sub

   'This procedure gives the command to load the file dropped into the data box.
   Private Sub DataBox_DragDrop(sender As Object, e As DragEventArgs) Handles DataBox.DragDrop
      Try
         Dim FileData As String = Nothing
         Dim FileName As String = Nothing

         If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            FileName = DirectCast(e.Data.GetData(DataFormats.FileDrop), String()).First
            FileData = Escape(LoadPES(FileName),, EscapeAll:=True)
            FileData = $"{FileName}{NewLine}{NewLine}Data:{NewLine}{FileData}"
            UpdateDataBox(FileData)
         End If
      Catch ExceptionO As Exception
         DisplayException(ExceptionO)
      End Try
   End Sub

   'This procedure handles objects being dragged into the data box.
   Private Sub DataBox_DragEnter(sender As Object, e As DragEventArgs) Handles DataBox.DragEnter
      Try
         If e.Data.GetDataPresent(DataFormats.FileDrop) Then e.Effect = DragDropEffects.All
      Catch ExceptionO As Exception
         DisplayException(ExceptionO)
      End Try
   End Sub

   'This procedure displays information about this program.
   Private Sub InformationMenu_Click(sender As Object, e As EventArgs) Handles InformationMenu.Click
      Try
         With My.Application.Info
            MessageBox.Show(.Description, ProgramInformation(), MessageBoxButtons.OK, MessageBoxIcon.Information)
         End With
      Catch ExceptionO As Exception
         DisplayException(ExceptionO)
      End Try
   End Sub

   'This procedure closes this window.
   Private Sub QuitMenu_Click(sender As Object, e As EventArgs) Handles QuitMenu.Click
      Try
         Me.Close()
      Catch ExceptionO As Exception
         DisplayException(ExceptionO)
      End Try
   End Sub
End Class
