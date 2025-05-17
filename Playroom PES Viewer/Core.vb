'This module's imports and settings.
Option Compare Binary
Option Explicit On
Option Infer Off
Option Strict On

Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Convert
Imports System.Environment
Imports System.IO
Imports System.Linq
Imports System.Text
Imports System.Windows.Forms

'This module contains this program's core procedures.
Public Module CoreModule
   Private Const SIGNATURE As Byte = &H82%   'Defines the *.PES file signature.

   Private ReadOnly BYTES_TO_TEXT As Func(Of List(Of Byte), String) = Function(Bytes As List(Of Byte)) New String((From ByteO In Bytes Select ToChar(ByteO)).ToArray())   'This procedure returns the specified bytes converted to text.
   Private ReadOnly FIXED_VALUES As New List(Of Tuple(Of Integer, Byte))                                                                                                  'Defines a list of values that are the same in every *.PES file and their position.

   'This procedure displays the message describing the specified exception.
   Public Sub DisplayException(ExceptionO As Exception)
      Try
         If MessageBox.Show(ExceptionO.Message, My.Application.Info.Title, MessageBoxButtons.OKCancel, MessageBoxIcon.Error) = DialogResult.Cancel Then
            Application.Exit()
         End If
      Catch
         [Exit](0)
      End Try
   End Sub

   'This procedure returns the specified text with any non-displayable or all characters converted to escape sequences.
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

         If Not Data.First = SIGNATURE Then
            Data = New List(Of Byte)
            MessageBox.Show($"""{FileName}"" is not a valid The Playroom *.PES file.", My.Application.Info.Title, MessageBoxButtons.OK, MessageBoxIcon.Error)
         End If

         If Not VerifyFixedValues(Data) Then
            MessageBox.Show($"""{FileName}"" does not contain the expected fixed values.", My.Application.Info.Title, MessageBoxButtons.OK, MessageBoxIcon.Warning)
         End If

         Return Data
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

   'This procedure verifies whether the fixed values are present in the specified data and returns the result.
   Private Function VerifyFixedValues(Data As List(Of Byte)) As Boolean
      Try
         Dim Verified As Boolean = True

         If FIXED_VALUES.Count = 0 Then
            FIXED_VALUES.AddRange({Tuple.Create(&H0%, SIGNATURE), Tuple.Create(&H3%, ToByte(&H0%)), Tuple.Create(&H4%, ToByte(&H2%)), Tuple.Create(&H7%, ToByte(&H0%)), Tuple.Create(&H9%, ToByte(&H0%)), Tuple.Create(&HA%, ToByte(&H0%))})
         End If

         For Each FixedValue As Tuple(Of Integer, Byte) In FIXED_VALUES
            If Not (FixedValue.Item1 < Data.Count AndAlso Data(FixedValue.Item1) = FixedValue.Item2) Then
               Verified = False
               Exit For
            End If
         Next FixedValue

         Return Verified
      Catch ExceptionO As Exception
         DisplayException(ExceptionO)
      End Try

      Return False
   End Function

   'This procedure gives the command to verify any *.PES files in the specified folder.
   Public Function VerifyFolder(Path As String) As String
      Try
         Dim Verified As New StringBuilder

         For Each PESFile As String In Directory.GetFiles(Path, "*.PES")
            If VerifyFixedValues(File.ReadAllBytes(PESFile).ToList()) Then
               Verified.Append($"{PESFile}{NewLine}")
            End If
         Next PESFile

         Return Verified.ToString()
      Catch ExceptionO As Exception
         DisplayException(ExceptionO)
      End Try

      Return Nothing
   End Function
End Module
