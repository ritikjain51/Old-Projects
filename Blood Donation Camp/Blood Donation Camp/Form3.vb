Imports System.Data.OleDb

Public Class Form3
    Dim rdr As OleDbDataReader = Nothing
    Dim dtable As DataTable
    Dim con As OleDbConnection = Nothing
    Dim adp As OleDbDataAdapter
    Dim ds As DataSet
    Dim cmd As OleDbCommand = Nothing
    Dim dt As New DataTable
    Dim cs As String = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\JUBLOOD.accdb;Persist Security Info=False;"


    Sub reset()
        user.Text = ""
        pass.Text = ""
        hospital.Text = ""
        contact.Text = ""
        email.Text = ""

    End Sub


    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles register.Click
        Dim d As Boolean = False
        If user.Text = "" Then
            MessageBox.Show("Please enter username", "Error", MessageBoxButtons.OK, MessageBoxIcon.[Error])
            user.Focus()
            Return
        End If
        If pass.Text = "" Then
            MessageBox.Show("Please enter password", "Error", MessageBoxButtons.OK, MessageBoxIcon.[Error])
            pass.Focus()
            Return
        End If
        If hospital.Text = "" Then
            MessageBox.Show("Please enter name", "Error", MessageBoxButtons.OK, MessageBoxIcon.[Error])
            hospital.Focus()
            Return
        End If
        If contact.Text = "" Then
            MessageBox.Show("Please enter contact no.", "Error", MessageBoxButtons.OK, MessageBoxIcon.[Error])
            contact.Focus()
            Return
        End If
        If email.Text = "" Then
            MessageBox.Show("Please enter email id", "Error", MessageBoxButtons.OK, MessageBoxIcon.[Error])
            email.Focus()
            Return
        End If
        If Len(user.Text) < 8 Or Len(pass.Text) < 8 Then
            MessageBox.Show("Length is less then 8 words", "Error", MessageBoxButtons.OK, MessageBoxIcon.[Error])
        End If
        Try
            con = New OleDbConnection(cs)
            con.Open()
            Dim ct As String = "select username from Login where username=@find"

            cmd = New OleDbCommand(ct)
            cmd.Connection = con
            cmd.Parameters.Add(New OleDbParameter("@find", System.Data.OleDb.OleDbType.VarChar, 30, "username"))
            cmd.Parameters("@find").Value = user.Text
            rdr = cmd.ExecuteReader()

            If rdr.Read() Then
                MessageBox.Show("Username Already Exists", "Error", MessageBoxButtons.OK, MessageBoxIcon.[Error])
                user.Text = ""
                user.Focus()
                If (rdr IsNot Nothing) Then
                    rdr.Close()
                End If
                Return
            End If

            con = New OleDbConnection(cs)
            con.Open()

            Dim cb As String = "insert into Login(username,pass, Name, phn, Email) VALUES ('" & user.Text & "','" & pass.Text & "','" & hospital.Text & "','" & contact.Text & "','" & email.Text & "')"
            cmd = New OleDbCommand(cb)
            cmd.Connection = con
            cmd.ExecuteReader()
            con.Close()
            con = New OleDbConnection(cs)
            MessageBox.Show("Successfully Registered", "User", MessageBoxButtons.OK, MessageBoxIcon.Information)
            register.Enabled = False
            d = True

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.[Error])
        End Try
        If d Then
            Me.Hide()
            Form1.Show()
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Hide()
        Form1.Show()

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

        reset()
    End Sub


End Class