Public Class Form2


    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Timer1.Start()
        Label1.Text = Form1.hospitalName
    End Sub


    Private Sub LogOUtToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LogOUtToolStripMenuItem.Click
        Form1.Show()
        Me.Hide()
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Label2.Text = Date.Now.ToString("dd MM yyy HH:mm:ss")
    End Sub

    Private Sub NewDonorToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NewDonorToolStripMenuItem.Click
        Form4.Show()
        Me.Hide()
    End Sub

    Private Sub SearchToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SearchToolStripMenuItem.Click
        Me.Hide()
        search.Show()

    End Sub
End Class

