Public Class frmLogin
    Dim user_Login As Sys_user
    Dim i As Integer = 0
    Dim Failed_attemp As Integer

    Private Sub btnLogin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLogin.Click
        If txtUsername.Text = "" Then txtUsername.Focus() : Exit Sub
        If txtPassword.Text = "" Then txtPassword.Focus() : Exit Sub

        Dim uname As String = txtUsername.Text
        Dim pword As String = DreadKnight(txtPassword.Text)

        Dim u_ser As New Sys_user

        If Not u_ser.LoginUser(uname, pword) Then
            i += 1
            If i > 3 Then
                MsgBox("You reached the MAXIMUM failed attempts!", MsgBoxStyle.Exclamation, "Information")
                End
            End If
            MsgBox("Invalid username or password!" & vbCrLf & _
                   "Please try again.", MsgBoxStyle.Critical, "Error")
            Exit Sub
        End If

        SystemUser = u_ser
        SystemUser.UserID = u_ser.UserID

        MsgBox("Welcome, " & u_ser.UserName, MsgBoxStyle.Information, "L O G I N")


    End Sub

    Private Sub txtPassword_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPassword.KeyPress
        If isEnter(e) Then btnLogin.PerformClick()
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub Clearfield()
        txtUsername.Text = ""
        txtPassword.Text = ""
        txtUsername.Focus()
    End Sub
   
    Private Sub frmLogin_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
       
    End Sub

    Private Sub btnCLose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCLose.Click
        Me.Close()
    End Sub
End Class