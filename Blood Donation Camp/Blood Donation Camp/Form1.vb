Imports System.Data.OleDb

Public Class Form1

    Public hospitalName As String
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If TextBox1.Text = "" Then
            MsgBox("Please Enter the UserName", "User Name Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        ElseIf TextBox2.Text = "" Then
            MsgBox("Please Enter the Password", "Password Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Else

        End If
        Try
            Dim myConnection As OleDbConnection
            myConnection = New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\JUBLOOD.accdb;Persist Security Info=False;")
            Dim myCommand As OleDbCommand
            myCommand = New OleDbCommand("SELECT username, pass, Hospital FROM Login where username = @Username and pass = @UserPassword", myConnection)

            Dim uName As New OleDbParameter("@Username", SqlDbType.VarChar)
            Dim uPassword As New OleDbParameter("@UserPassword", SqlDbType.VarChar)
            uName.Value = TextBox1.Text
            uPassword.Value = TextBox2.Text
            myCommand.Parameters.Add(uName)

            myCommand.Parameters.Add(uPassword)
            myCommand.Connection.Open()

            Dim myReader As OleDbDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)

            Dim Login As Object = 0

            If myReader.HasRows Then

                myReader.Read()
                hospitalName = myReader(2)
                Login = myReader(Login)

            End If

            If Login = Nothing Then

                MsgBox("Login is Failed...Try again !", MsgBoxStyle.Critical, "Login Denied")
                TextBox1.Clear()
                TextBox2.Clear()
                TextBox1.Focus()
            End If
            myCommand.Dispose()
            myConnection.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Me.Hide()
        Form2.Show()
    End Sub

    Private Sub MsgBox(p1 As String, p2 As String, messageBoxButtons As MessageBoxButtons, messageBoxIcon As MessageBoxIcon)
        Throw New NotImplementedException
    End Sub

    Private Sub MsgBox(p1 As String, msgBoxStyle As MsgBoxStyle, p3 As String)
        Throw New NotImplementedException
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Hide()
        Form3.Show()
    End Sub
End Class
