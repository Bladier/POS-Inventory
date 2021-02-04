Public Class frmUserManagement
    Dim users As Sys_user


    Private Sub frmUserManagement_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        users = New Sys_user
        users.CreateAdministrator()

        controlUser(False)
        loadUser()
    End Sub

    Private Sub loadUser()
        Dim mysql As String = "SELECT * FROM TBLUSER ORDER BY ID ASC"
        Dim ds As DataSet = LoadSQL(mysql, "TBLUSER")

        If ds.Tables(0).Rows.Count = 0 Then Exit Sub

        lvUser.Items.Clear()
        For Each dr As DataRow In ds.Tables(0).Rows
            Dim u As New Sys_user
            u.LoadUserByRow(dr)

            addUser(u)
        Next
    End Sub

    Private Sub addUser(ByRef u_s_e_r As Sys_user)
        Dim lv As ListViewItem = lvUser.Items.Add(u_s_e_r.UserID)
        lv.SubItems.Add(u_s_e_r.firstname & " " & u_s_e_r.Lastname)

        lv.Tag = u_s_e_r.UserID
        users = u_s_e_r
    End Sub

    Private Sub lvUser_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lvUser.DoubleClick
        Dim u As New Sys_user
        u.LoadUser(lvUser.FocusedItem.Tag)


        txtFname.Text = u.firstname
        txtMname.Text = u.Middlename
        txtLname.Text = u.Lastname
        txtUname.Text = u.UserName
        cboUserRole.Text = u.userRole

        If u.userRole = "Admin" Then
            controlUser(False)
        Else
            controlUser(True)
        End If
        If u.UserStatus Then
            chkEnableDisable.Checked = True
        Else
            chkEnableDisable.Checked = False
        End If

        disableField(False)
        btnSave.Text = "&Edit"
    End Sub


    Private Sub controlUser(ByRef st As Boolean)
        lbluser.Visible = st
        chkEnableDisable.Visible = st
    End Sub

    Private Sub clearfields(ByRef str As String)
        txtFname.Text = str
        txtMname.Text = str
        txtLname.Text = str
        txtUname.Text = str
        txtPword.Text = str
        txtpword1.Text = str
        chkEnableDisable.Checked = False
        cboUserRole.SelectedItem = Nothing
        controlUser(False)
    End Sub

    Private Sub disableField(ByRef st As Boolean)
        txtFname.Enabled = st
        txtLname.Enabled = st
        txtMname.Enabled = st
        txtUname.Enabled = st
        txtPword.Enabled = st
        txtpword1.Enabled = st
        cboUserRole.Enabled = st
        chkEnableDisable.Enabled = st

        loadUser()
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If btnSave.Text = "&Edit" Then
            btnSave.Text = "&Update"
            disableField(True)
            Exit Sub
        End If

        If btnSave.Text = "&Update" Then
            UpdateUser()
            Exit Sub
        End If

        If btnSave.Text = "&Save" Then
            SaveUser()
            Exit Sub
        End If
    End Sub


    Private Sub SaveUser()
        If Not isValid() Then Exit Sub

        Dim msg As DialogResult = MsgBox("Do you want to save this user?", MsgBoxStyle.YesNo, "Question")
        If msg = vbNo Then Exit Sub

        Dim users As New Sys_user
        With users
            .firstname = txtFname.Text
            .Middlename = txtMname.Text
            .Lastname = txtLname.Text
            .UserName = txtUname.Text
            .Password = txtPword.Text
            .userRole = cboUserRole.Text
            .SaveUser()
        End With

        disableField(True)
        clearfields("")
        MsgBox("Successfully saved...", MsgBoxStyle.OkOnly, "Saved")
    End Sub


    Private Sub UpdateUser()
        If Not isValid() Then Exit Sub

        Dim msg As DialogResult = MsgBox("Do you want to update this user?", MsgBoxStyle.YesNo, "Question")
        If msg = vbNo Then Exit Sub

        Dim users As New Sys_user
        With users
            .UserID = lvUser.FocusedItem.Tag
            .firstname = txtFname.Text
            .Middlename = txtMname.Text
            .Lastname = txtLname.Text
            .UserName = txtUname.Text
            .userRole = cboUserRole.Text
            .UserStatus = IIf(chkEnableDisable.Checked, True, False)
            .SaveUser(False, txtPword.Text)
        End With

        disableField(True)
        clearfields("")
        MsgBox("Successfully updated...", MsgBoxStyle.OkOnly, "Update")
    End Sub

    Private Function isValid() As Boolean
        If btnSave.Text = "&Update" Then
            If txtFname.Text = "" Then txtFname.Focus() : Return False
            If txtLname.Text = "" Then txtLname.Focus() : Return False
            If txtUname.Text = "" Then txtUname.Focus() : Return False


            If txtPword.Text <> "" Then
                If txtpword1.Text <> txtPword.Text Then
                    MsgBox("Password Not Matched!", MsgBoxStyle.Critical, "Error")
                    Return False
                End If
            End If
            If cboUserRole.Text = "" Then cboUserRole.Focus() : Return False
        Else
            If txtFname.Text = "" Then txtFname.Focus() : Return False
            If txtLname.Text = "" Then txtLname.Focus() : Return False
            If txtUname.Text = "" Then txtUname.Focus() : Return False
            If txtPword.Text = "" Then txtPword.Focus() : Return False
            If txtpword1.Text = "" Then txtpword1.Focus() : Return False

            If txtPword.Text <> "" Then
                If txtpword1.Text <> txtPword.Text Then
                    MsgBox("Password Not Matched!", MsgBoxStyle.Critical, "Error")
                    Return False
                End If
            End If

            If cboUserRole.Text = "" Then cboUserRole.Focus() : Return False
        End If


        Return True
    End Function

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub
End Class