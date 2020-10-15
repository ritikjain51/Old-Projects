Imports System.Data.OleDb
Public Class Form4

    Dim rdr As OleDbDataReader = Nothing
    Dim dtable As DataTable
    Dim con As OleDbConnection = Nothing
    Dim adp As OleDbDataAdapter
    Dim ds As DataSet
    Dim cmd As OleDbCommand = Nothing
    Dim dt As New DataTable
    Dim cs As String = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\JUBLOOD.accdb;Persist Security Info=False;"

    Sub reset()
        Name1.Text = ""
        father.Text = ""
        Address.Text = ""
        contact.Text = ""
        email.Text = ""
    End Sub



    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim gen As String = ""
        Dim donor As String = ""
        Dim d As Boolean = False
        If Name1.Text = "" Then
            MessageBox.Show("Please enter name", "Error", MessageBoxButtons.OK, MessageBoxIcon.[Error])
            Name1.Focus()
            Return
        End If
        If father.Text = "" Then
            MessageBox.Show("Please enter Father's Name", "Error", MessageBoxButtons.OK, MessageBoxIcon.[Error])
            father.Focus()
            Return
        End If
        If Address.Text = "" Then
            MessageBox.Show("Please enter Address", "Error", MessageBoxButtons.OK, MessageBoxIcon.[Error])
            Address.Focus()
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

        If male.Checked Then
            gen = "Male"
        Else
            gen = "Female"
        End If

        If home.Checked Then
            donor = "Home Donor"
        Else
            donor = "Mobile Donor"
        End If

        If ListBox1.Text = "" Then
            MessageBox.Show("Please Check Blood Group", "Error", MessageBoxButtons.OK, MessageBoxIcon.[Error])
        End If

        Try
            con = New OleDbConnection(cs)
            con.Open()
            Dim ct As String = "select username from Login where username=@find"

            cmd = New OleDbCommand(ct)
            cmd.Connection = con
            cmd.Parameters.Add(New OleDbParameter("@find", System.Data.OleDb.OleDbType.VarChar, 30, "username"))
            cmd.Parameters("@find").Value = Name1.Text
            rdr = cmd.ExecuteReader()

            If rdr.Read() Then
                MessageBox.Show("Username Already Exists", "Error", MessageBoxButtons.OK, MessageBoxIcon.[Error])
                Name1.Text = ""
                Name1.Focus()
                If (rdr IsNot Nothing) Then
                    rdr.Close()
                End If
                Return
            End If

            con = New OleDbConnection(cs)
            con.Open()

            Dim cb As String = "insert into Table1(Name,father, email,phn, donor,bg, Gender, Address,DOB,Hospital) VALUES ('" & Name1.Text & "','" & father.Text & "','" & email.Text & "','" & contact.Text & "','" & donor & "', '" & ListBox1.Text & "', '" & gen & "','" & Address.Text & "', '" & datetime.Text & "','" & Form1.hospitalName & "')"
            cmd = New OleDbCommand(cb)
            cmd.Connection = con
            cmd.ExecuteReader()
            con.Close()
            con = New OleDbConnection(cs)
            MessageBox.Show("Successfully Registered", "User", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Button1.Enabled = False
            d = True

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.[Error])
        End Try
        If d Then
            Me.Hide()
            Form1.Show()
        End If
    End Sub


    Private Function cb() As Object
        Throw New NotImplementedException
    End Function

    Private Sub Form4_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ListBox1.Items.Add("")
        ListBox1.Items.Add("A+")
        ListBox1.Items.Add("A+")
        ListBox1.Items.Add("B+")
        ListBox1.Items.Add("B-")
        ListBox1.Items.Add("AB+")
        ListBox1.Items.Add("AB-")
        ListBox1.Items.Add("O+")
        ListBox1.Items.Add("O-")
    End Sub
End Class