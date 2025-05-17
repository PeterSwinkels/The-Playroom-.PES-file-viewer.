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
Public Class InterfaceWindow
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

         UpdateDataBox(, NewDataBox:=DataBox)

         If GetCommandLineArgs.Count > 1 Then
            DisplayPESFile(GetCommandLineArgs.Last())
         End If
      Catch ExceptionO As Exception
         DisplayException(ExceptionO)
      End Try
   End Sub

   'This procedure gives the command to load the file dropped into the data box.
   Private Sub DataBox_DragDrop(sender As Object, e As DragEventArgs) Handles DataBox.DragDrop
      Try
         If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            DisplayPESFile(DirectCast(e.Data.GetData(DataFormats.FileDrop), String()).First)
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

   'This procedure displays the dialog for loading a *.PES file.
   Private Sub LoadFileMenu_Click(sender As Object, e As EventArgs) Handles LoadFileMenu.Click
      Try
         With OpenFileDialog
            If .ShowDialog() = DialogResult.OK Then
               DisplayPESFile(.FileName)
            End If
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

   'This procedure gives the command to verify the *.PES files in a folder selected by the user in a dialog.
   Private Sub VerifyFolderMenu_Click(sender As Object, e As EventArgs) Handles VerifyFolderMenu.Click
      Try
         With New FolderBrowserDialog
            If .ShowDialog() = DialogResult.OK Then
               MessageBox.Show($"The following files have been verified as potential The Playroom *.PES files:{NewLine}{VerifyFolder(.SelectedPath)}", My.Application.Info.Title, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
         End With
      Catch ExceptionO As Exception
         DisplayException(ExceptionO)
      End Try
   End Sub

   'This procedure gives the command to load the specified *.PES file and displays it.
   Private Sub DisplayPESFile(FileName As String)
      Try
         UpdateDataBox($"File: ""{FileName}""{NewLine}{NewLine}Data:{NewLine}{Escape(LoadPES(FileName),, EscapeAll:=True)}")
      Catch ExceptionO As Exception
         DisplayException(ExceptionO)
      End Try
   End Sub
End Class
