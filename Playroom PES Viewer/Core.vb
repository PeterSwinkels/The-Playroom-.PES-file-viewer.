'This module's imports and settings.
Option Compare Binary
Option Explicit On
Option Infer Off
Option Strict On

Imports Microsoft.VisualBasic
Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.Convert
Imports System.Drawing
Imports System.Environment
Imports System.Globalization
Imports System.IO
Imports System.Linq
Imports System.Math
Imports System.Text
Imports System.Windows.Forms

'This module contains this program's core procedures.
Public Module CoreModule
   Private Const SIGNATURE As Byte = &H82%   'Defines the *.PES file signature.

   Private ReadOnly BYTES_TO_TEXT As Func(Of List(Of Byte), String) = Function(Bytes As List(Of Byte)) New String((From ByteO In Bytes Select ToChar(ByteO)).ToArray())                                                                               'This procedure returns the specified bytes converted to text.

   'This procudure displays the message describing the specified exception.
   Public Sub DisplayException(ExceptionO As Exception)
      Try
         If MessageBox.Show(ExceptionO.Message, My.Application.Info.Title, MessageBoxButtons.OKCancel, MessageBoxIcon.Error) = DialogResult.Cancel Then
            Application.Exit()
         End If
      Catch
         [Exit](0)
      End Try
   End Sub

   'This procedure returns the specified text with any non-displayable or all charactes converted to escape sequences.
   Public Function Escape(ToEscape As Object, Optional EscapeCharacter As Char = "/"c, Optional EscapeAll As Boolean = False) As String
      Try
         Dim Character As New Char
         Dim Escaped As New StringBuilder
         Dim Index As Integer = 0
         Dim Text As String = If(TypeOf ToEscape Is List(Of Byte), BYTES_TO_TEXT(DirectCast(ToEscape, List(Of Byte))), DirectCast(ToEscape, String))

         With Escaped
            Do Until Index >= Text.Length
               Character = Text.Chars(Index)

               If Character = EscapeCharacter AndAlso Not EscapeAll Then
                  .Append(New String(EscapeCharacter, 2))
               ElseIf (Character = ControlChars.Tab OrElse Character >= " "c) AndAlso Not EscapeAll Then
                  .Append(Character)
               ElseIf (Index < Text.Length - 1 AndAlso $"{Character}{Text.Chars(Index + 1)}" = NewLine) AndAlso Not EscapeAll Then
                  .Append(NewLine)
                  Index += 1
               Else
                  .Append($"{EscapeCharacter}{ToByte(Character):X2}")
               End If
               Index += 1
            Loop
         End With

         Return Escaped.ToString()
      Catch ExceptionO As Exception
         DisplayException(ExceptionO)
      End Try

      Return Nothing
   End Function

   'This procedure loads the specified *.PES file and returns its data.
   Public Function LoadPES(FileName As String) As List(Of Byte)
      Try
         Dim Data As New List(Of Byte)(File.ReadAllBytes(FileName))

         If Data.First = SIGNATURE Then
            Return Data
         Else
            MessageBox.Show($"""{FileName}"" is not a valid The Playroom *.PES file.", My.Application.Info.Title, MessageBoxButtons.OK, MessageBoxIcon.Error)
         End If

         Return New List(Of Byte)
      Catch ExceptionO As Exception
         DisplayException(ExceptionO)
      End Try

      Return New List(Of Byte)
   End Function

   'This procedure returns information about this program.
   Public Function ProgramInformation() As String
      Try
         With My.Application.Info
            Return $"{ .Title} v{ .Version} - by: { .CompanyName}"
         End With
      Catch ExceptionO As Exception
         DisplayException(ExceptionO)
      End Try

      Return Nothing
   End Function

   'This procedure updates the interface window's databox. 
   Public Sub UpdateDataBox(Optional NewText As String = Nothing, Optional NewDataBox As TextBox = Nothing)
      Try
         Static CurrentDataBox As TextBox = Nothing

         If NewDataBox IsNot Nothing Then
            CurrentDataBox = NewDataBox
         ElseIf Not NewText = Nothing AndAlso CurrentDataBox IsNot Nothing Then
            With CurrentDataBox
               .Text = NewText
               .Select(0, 0)
               If .TextLength < NewText.Length Then MessageBox.Show("The databox cannot display all new data.", My.Application.Info.Title, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End With
         End If
      Catch ExceptionO As Exception
         DisplayException(ExceptionO)
      End Try
   End Sub
End Module
