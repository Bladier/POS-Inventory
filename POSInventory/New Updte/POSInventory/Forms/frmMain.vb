Public Class frmMain

    Friend dateSet As Boolean
    Friend Sub NotyetLogin(Optional ByVal st As Boolean = True)
        If Not st Then
            LoginToolStripMenuItem.Text = "&Log Out"
        Else
            LoginToolStripMenuItem.Text = "&Login"
        End If

        UserManagementToolStripMenuItem.Enabled = Not st
        MaitenanceToolStripMenuItem.Enabled = Not st
        BackupToolStripMenuItem.Enabled = Not st
        DailyToolStripMenuItem.Enabled = Not st
        OpenStoreToolStripMenuItem.Enabled = Not st

        ToolStripButton1.Enabled = Not st
        ToolStripButton2.Enabled = Not st
        ToolStripButton3.Enabled = Not st
        ToolStripButton4.Enabled = Not st

        If st Then
            tsUser.Text = "No User yet"
        Else
            tsUser.Text = "Greetings " & SystemUser.firstname & " " & SystemUser.Lastname
        End If
    End Sub


    Friend Sub CheckStoreStatus()
        mod_system.LoadCurrentDate()
    End Sub

    Private Sub LoginToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LoginToolStripMenuItem.Click
        If LoginToolStripMenuItem.Text = "&Login" Then
            frmLogin.Show()
        Else
            Dim ans As DialogResult = MsgBox("Do you want to LOGOUT?", MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.Information, "Logout")
            If ans = Windows.Forms.DialogResult.No Then Exit Sub

            SystemUser = Nothing
            Dim formNames As New List(Of String)
            For Each Form In My.Application.OpenForms
                If Form.Name <> "frmMain" Or Not Form.name <> "frmLogin" Then
                    formNames.Add(Form.Name)
                End If
            Next
            For Each currentFormName As String In formNames
                Application.OpenForms(currentFormName).Close()
            Next
            MsgBox("Thank you!", MsgBoxStyle.Information)
            NotyetLogin()
            frmLogin.Show()
        End If
    End Sub

    Private Sub UserManagementToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UserManagementToolStripMenuItem.Click
        If SystemUser.userRole <> "Admin" Then
            MsgBox("You don't have access in this module", MsgBoxStyle.Exclamation, "Notification")
            Exit Sub
        End If
        frmUserManagement.Show()
    End Sub

    Private Sub frmMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        If SystemUser.UserName = Nothing Then
            NotyetLogin()
        Else
            NotyetLogin(False)
        End If

        frmLogin.ShowDialog()
    End Sub

    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click
        If Not dateSet Then
            MsgBox("Store is not open yet. Go to File>Open Store", MsgBoxStyle.Exclamation, "Notification")
            Exit Sub
        End If
        frmTransaction.Show()
    End Sub

    Private Sub ToolStripButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton2.Click
        If Not dateSet Then
            MsgBox("Store is not open yet. Go to File>Open Store", MsgBoxStyle.Exclamation, "Notification")
            Exit Sub
        End If
        MsgBox("You don't have access in this module", MsgBoxStyle.Exclamation, "Notification")
        Exit Sub
        frmAddInventory.Show()
    End Sub

    Private Sub ToolStripButton3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton3.Click
        If Not dateSet Then
            MsgBox("Store is not open yet. Go to File>Open Store", MsgBoxStyle.Exclamation, "Notification")
            Exit Sub
        End If
        frmIMDList.Show()
    End Sub

    Private Sub ToolStripButton4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton4.Click
        If Not dateSet Then
            MsgBox("Store is not open yet. Go to File>Open Store", MsgBoxStyle.Exclamation, "Notification")
            Exit Sub
        End If

        MsgBox("You don't have access in this module", MsgBoxStyle.Exclamation, "Notification")
        Exit Sub
        frmIMD.Show()
    End Sub



    Private Sub ExitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExitToolStripMenuItem.Click
        End
    End Sub

    Private Sub SalesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SalesToolStripMenuItem.Click
        qryDate.FormType = qryDate.ReportType.Sales
        qryDate.Show()
    End Sub

    Private Sub StockInToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StockInToolStripMenuItem.Click
        qryDate.FormType = qryDate.ReportType.StockIn
        qryDate.Show()
    End Sub

    Private Sub tmrCurrent_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrCurrent.Tick
        If dateSet Then
            tsCurrentDate.Text = CurrentDate.ToLongDateString & " " & Now.ToString("T")
            OpenStoreToolStripMenuItem.Text = "&Close Store"
        Else
            tsCurrentDate.Text = "Date not set"
            OpenStoreToolStripMenuItem.Text = "&Open Store"
        End If
    End Sub

    Private Sub OpenStoreToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenStoreToolStripMenuItem.Click
        frmOpenStore.Show()
    End Sub

    Private Sub InventoryToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles InventoryToolStripMenuItem.Click
        qryDate.FormType = qryDate.ReportType.Inventory
        qryDate.Show()
    End Sub

    Private Sub statusStrip_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles statusStrip.ItemClicked

    End Sub

    Private Sub tsCurrentDate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsCurrentDate.Click

    End Sub

    Private Sub tsUser_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsUser.Click

    End Sub

    Private Sub SalesToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SalesToolStripMenuItem1.Click
        qryDate.FormType = qryDate.ReportType.Sales_Monthly
        qryDate.Show()
    End Sub

    Private Sub StockOutToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StockOutToolStripMenuItem.Click
        qryDate.FormType = qryDate.ReportType.stockOut
        qryDate.Show()
    End Sub

    Private Sub BackupToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BackupToolStripMenuItem.Click
        frmbackup.Show()
    End Sub

    Private Sub MaitenanceToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MaitenanceToolStripMenuItem.Click
        If SystemUser.userRole <> "Admin" Then
            MsgBox("You don't have access in this module.", MsgBoxStyle.Exclamation, "Notification")
            Exit Sub
        End If
        frmSettings.Show()
    End Sub
End Class
