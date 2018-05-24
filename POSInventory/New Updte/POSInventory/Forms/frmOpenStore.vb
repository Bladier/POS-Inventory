﻿Public Class frmOpenStore

    Dim isDisable As Boolean = DEV_MODE

    Private Sub frmOpenStore_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        txtInitial.Clear()
        LoadMoney()
        If frmMain.dateSet Then
            btnSetup.Text = "Close Store"
            GroupBox1.Text = "Cash Count"
            Label2.Text = "Enter Cash Count"
            Me.Text = "Close Store"
        Else
            btnSetup.Text = "Open Store"
            GroupBox1.Text = "Current Balance"
            Label2.Text = "Initial Balance"
            Me.Text = "Open Store"
        End If
        Console.WriteLine("Initial: " & InitialBal)
    End Sub

    Private Sub LoadMoney()
        InitialBal = GetOption("CurrentBalance")
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub
    Public Sub printReport()
        Dim mySql As String, dsName As String, rptPath As String
        Dim dt As DateTime = Now
        Dim format As String = "yyyy-MM-dd"
        Dim str As String = dt.ToString(format)


        dsName = "dailySalesReport"
        rptPath = "Report\rptDailySales.rdlc"
        mySql = "SELECT tbldaily.ID,tbldaily.CURRENTDATE,tbldaily.INITIALBAL,tbldaily.CASHCOUNT,tbldaily.STATUS,tbldaily.REMARKS," & _
            "tbldaily.SYSTEMINFO,tbldaily.`Overage/Shortage`,Concat(op.firstname,' ' , op.middlename, ' ' ,op.lastname) as " & _
            "Opener,concat(cl.firstname,' ' ,cl.middlename,' ' ,cl.lastname) as Closer FROM tbldaily Inner Join tbluser op ON tbldaily.OPENNER = op.ID" & _
            "Inner Join tbluser cl ON tbldaily.CLOSER = cl.ID where tbldaily.STATUS = 0"

        Dim addParameter As New Dictionary(Of String, String)
        addParameter.Add("txtMonthOf", "DATE : " & str)
        addParameter.Add("txtUsername", SystemUser.UserName)

        frmReport.ReportInit(mySql, dsName, rptPath, addParameter)
        frmReport.Show()
    End Sub

    Private Sub btnSetup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetup.Click
        ' If frmMain.dateSet Then MsgBox("Please execute closing", MsgBoxStyle.Critical) : Exit Sub

        If frmMain.dateSet Then
            If txtInitial.Text > 0 Then
                closestore(txtInitial.Text)
                printReport()
            End If
        Else
            If txtInitial.Text = "" Then Exit Sub
            If txtInitial.Text <= 0 Then Exit Sub

            Dim ans As DialogResult = _
          MsgBox("TODAY IS: " & vbCrLf & dtpCurrentDate.Value.ToString("MMM d, yyyy"), MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.Information, "Please CHECK")
            If Not ans = Windows.Forms.DialogResult.Yes Then
                Exit Sub
            End If

            If dtpCurrentDate.Value.ToShortDateString <> Now.Date.ToShortDateString Then
                ans = _
                    MsgBox("It seems your SYSTEM DATE and CURRENT DATE didn't match" + vbCrLf + _
                           "Are you sure about this?", MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.Information)
                If ans = Windows.Forms.DialogResult.No Then Exit Sub
            End If

            CurrentDate = dtpCurrentDate.Value

            If mod_system.OpenStore(txtInitial.Text) Then
                frmMain.dateSet = True
                dailyID = LoadLastOpening.Tables(0).Rows(0).Item("ID")
                InitialBal = txtInitial.Text

                UpdateOptions("CurrentBalance", txtInitial.Text)
            Else
                Exit Sub
            End If
        End If
      

        Me.Close()

    End Sub
End Class