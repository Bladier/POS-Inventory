Public Class frmIMD
    Dim cd As cItemData

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If btnSave.Text = "&Edit" Then
            btnSave.Text = "&Update"
            disablesFields(True)
            txtItemcode.Enabled = False
            txtDescription.Enabled = False
            Exit Sub
        End If

        If btnSave.Text = "&Save" Then
            saveIMD()
            Exit Sub
        End If


        If btnSave.Text = "&Update" Then
            UpdateIMD()
            Exit Sub
        End If

    End Sub

    Private Sub saveIMD()
        If Not isValid() Then Exit Sub
        Dim msg As DialogResult = MsgBox("Do you want to save this item?", MsgBoxStyle.YesNo, "Save")
        If msg = vbNo Then Exit Sub

        Dim imd As New cItemData
        With imd
            .ItemCode = txtItemcode.Text
            .Description = txtDescription.Text
            .Category = txtCategory.Text
            .SubCategory = txtSubCat.Text
            .UnitofMeasure = txtUOM.Text
            .UnitPrice = IIf(TxtUnitPrice.Text = "", 0, TxtUnitPrice.Text)
            .SalePrice = txtSalePrice.Text
            .isSaleable = IIf(chkIssalable.Checked, True, False)
            .isInventoriable = IIf(chkisIVN.Checked, True, False)
            .onHold = IIf(chkOnOhold.Checked, True, False)
            .Remarks = txtRemarks.Text
            .Save_ItemData()
        End With

        MsgBox("Item saved.", MsgBoxStyle.Information, "Save")
        clearfields()
    End Sub

    Private Sub UpdateIMD()
        If Not isValid() Then Exit Sub
        Dim msg As DialogResult = MsgBox("Do you want to Update this item?", MsgBoxStyle.YesNo, "Update")
        If msg = vbNo Then Exit Sub

        Dim imd As New cItemData
        With imd
            .ItemCode = txtItemcode.Text
            .Description = txtDescription.Text
            .Category = txtCategory.Text
            .SubCategory = txtSubCat.Text
            .UnitofMeasure = txtUOM.Text
            .UnitPrice = IIf(TxtUnitPrice.Text = "", 0, TxtUnitPrice.Text)
            .SalePrice = txtSalePrice.Text
            .isSaleable = IIf(chkIssalable.Checked, True, False)
            .isInventoriable = IIf(chkisIVN.Checked, True, False)
            .onHold = IIf(chkOnOhold.Checked, True, False)
            .Remarks = txtRemarks.Text
            .Save_ItemData()
        End With

        MsgBox("Item updated.", MsgBoxStyle.Information, "Updated")
        clearfields()
        cd = Nothing
    End Sub


    Private Function isValid() As Boolean
        If txtItemcode.Text = "" Then txtItemcode.Focus() : Return False
        If txtDescription.Text = "" Then txtDescription.Focus() : Return False
        If txtCategory.Text = "" Then txtCategory.Focus() : Return False

        If txtUOM.Text = "" Then txtUOM.Focus() : Return False
        If txtSalePrice.Text = "" Then txtSalePrice.Focus() : Return False
        Return True
    End Function

    Private Sub clearfields()
        txtItemcode.Text = ""
        txtDescription.Text = ""
        txtCategory.Text = ""
        txtSubCat.Text = ""
        txtUOM.Text = ""
        TxtUnitPrice.Text = ""
        txtSalePrice.Text = ""
        chkisIVN.Checked = True
        chkIssalable.Checked = True
        chkOnOhold.Checked = False
        txtRemarks.Text = ""
        txtSearch.Text = ""
    End Sub

    Private Sub disablesFields(ByVal st As Boolean)
        txtItemcode.Enabled = st
        txtDescription.Enabled = st
        txtCategory.Enabled = st
        txtSubCat.Enabled = st
        txtUOM.Enabled = st
        TxtUnitPrice.Enabled = st
        txtSalePrice.Enabled = st
        chkisIVN.Enabled = st
        chkIssalable.Enabled = st
        chkOnOhold.Enabled = st
        txtRemarks.Enabled = st

    End Sub


    Friend Sub loaditem(ByVal cim As cItemData)
        If cim.ItemCode = Nothing Then Exit Sub
        With cim
            txtItemcode.Text = .ItemCode
            txtDescription.Text = .Description
            txtCategory.Text = .Category
            txtSubCat.Text = .SubCategory
            txtUOM.Text = .UnitofMeasure
            TxtUnitPrice.Text = .UnitPrice
            txtSalePrice.Text = .SalePrice

            If .isSaleable Then
                chkIssalable.Checked = True
            Else
                chkIssalable.Checked = False
            End If

            If .isInventoriable Then
                chkisIVN.Checked = True
            Else
                chkisIVN.Checked = False
            End If

            If .onHold Then
                chkOnOhold.Checked = True
            Else
                chkOnOhold.Checked = False
            End If

            txtRemarks.Text = .Remarks
        End With

        cd = cim

        btnSave.Text = "&Edit"
        disablesFields(False)

    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        frmIMDList.txtSearch.Text = txtSearch.Text
        frmIMDList.isAdd_Or_Update_IMD = True
        frmIMDList.Show()
    End Sub

    Private Sub txtSearch_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSearch.KeyPress
        If isEnter(e) Then btnSearch.PerformClick()
    End Sub

    Private Sub btncancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btncancel.Click
        Dim msg As DialogResult = MsgBox("Do you want to cancel?", MsgBoxStyle.YesNo, "Question")
        If msg = vbNo Then Exit Sub

        disablesFields(True)
        clearfields()
    End Sub
End Class