Public Class CAA_USR_UI

    Private Sub CAA_USR_UI_Load(sender As Object, e As EventArgs) Handles Me.Load

        Debug.Print(Form1.USER.USER_file_folder)



    End Sub






    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
        Form1.Show()
    End Sub


End Class