Imports System.IO
Imports System
Public Class frmbackup

    Private Sub frmbackup_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Label1.Text = Now
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        Dim tmpath As String = GetOption("BackupPath")
        If tmpath = "" Then Exit Sub
        Dim dt As DateTime = Convert.ToDateTime(CurrentDate.ToShortDateString)
        Dim format As String = "MMddyyyy"
        Dim str As String = dt.ToString(format)
        tmpath = tmpath & "\" & dbName & str & ".sql"
        Using sw As StreamWriter = File.CreateText("backup.bat")
            sw.WriteLine("cd C:\xampp\mysql\bin")
            sw.WriteLine("mysqldump -hlocalhost -u" & fbUser & " -p" & fbPass & " " & dbName _
                         & " > " & tmpath)
            sw.WriteLine("cls ")
            sw.WriteLine("echo DONE!!! THANK YOU FOR WAITING")
            sw.WriteLine("pause")
            sw.WriteLine("exit")
        End Using
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim pro As New ProcessStartInfo(Application.StartupPath & "\backup.bat")
        pro.RedirectStandardError = True
        pro.RedirectStandardOutput = True
        pro.CreateNoWindow = False
        pro.WindowStyle = ProcessWindowStyle.Hidden
        pro.UseShellExecute = False
        Dim process As Process = process.Start(pro)

        System.Threading.Thread.Sleep(1000)
        MsgBox("Backup successfully.", MsgBoxStyle.Information, "Backup")
        Me.Close()
    End Sub
End Class