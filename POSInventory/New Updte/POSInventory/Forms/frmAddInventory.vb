Public Class frmAddInventory
    Dim cd As cItemData

    Private Sub txtSearch_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSearch.KeyPress
        If isEnter(e) Then
            btnSearch.PerformClick()
        End If
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        frmIMDList.txtSearch.Text = txtSearch.Text
        frmIMDList.isAddInv = True
        frmIMDList.Show()
    End Sub

    Friend Sub loaditem(ByVal cim As cItemData)
        If cim.ItemCode = Nothing Then Exit Sub
        With cim
            txtItemcode.Text = .ItemCode
            txtDescription.Text = .Description

            txtUOM.Text = .UnitofMeasure
            TxtUnitPrice.Text = .UnitPrice
            txtSalePrice.Text = .SalePrice
        End With

        cd = cim
        txtQty.Focus()


        Dim itm As New cItem(cItem.DocumentType.GoodsReceipt)
        With itm
            .ItemCode = txtItemcode.Text
            .Description = txtDescription.Text
            .Load_ItemCode()
        End With
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If lvListIItems.Items.Count = 0 Then Exit Sub


        Dim mysql As String = "Select * from INV limit 1"
        Dim ds As DataSet = LoadSQL(mysql, "INV")

        Dim dsnewrow As DataRow
        dsnewrow = ds.Tables(0).NewRow
        With dsnewrow
            .Item("DocNum") = GetInvNum()
            .Item("DocDate") = CurrentDate
            .Item("GrandTotal") = lvListIItems.Items.Count
            .Item("Partner") = "N/A"
        End With
        ds.Tables(0).Rows.Add(dsnewrow)
        database.SaveEntry(ds)

        mysql = "Select * from INV order by DocID desc Limit 1"
        ds.Clear()
        ds = LoadSQL(mysql, "INV")

        Dim docid As Integer = ds.Tables(0).Rows(0).Item("DocID")

        For Each lv As ListViewItem In lvListIItems.Items

            mysql = "Select * from InvLines limit 1"
            ds.Clear()
            ds = LoadSQL(mysql, "InvLines")

            dsnewrow = ds.Tables(0).NewRow
            With dsnewrow
                .Item("DocID") = docid
                .Item("itemcode") = lv.Text
                .Item("Description") = lv.SubItems(1).Text
                .Item("UOM") = lv.SubItems(2).Text
                .Item("QTY") = lv.SubItems(3).Text
                .Item("Remarks") = "UPLOADED DATE " & CurrentDate
            End With
            ds.Tables(0).Rows.Add(dsnewrow)
            database.SaveEntry(ds)

            AddInventory(cd.ItemCode, lv.SubItems(3).Text)
        Next

        MsgBox("Inventory Added", MsgBoxStyle.Information, "Inventory")
        clearfields()
        lvListIItems.Items.Clear()
        updateInvNum()
    End Sub


    Private Function GetInvNum() As String
        Dim mysql As String = "Select * from tblmaintenance where opt_keys ='InventoryNum'"
        Dim ds As DataSet = LoadSQL(mysql, "tblmaintenance")

        Dim inum As Integer = ds.Tables(0).Rows(0).Item("opt_values")


        Return "STO#0000" & inum
    End Function

    Private Sub updateInvNum()
        Dim mysql As String = "Select * from tblmaintenance where opt_keys ='InventoryNum'"
        Dim ds As DataSet = LoadSQL(mysql, "tblmaintenance")

        Dim ix As Integer = ds.Tables(0).Rows(0).Item("opt_values")
        With ds.Tables(0).Rows(0)
            .Item("OPT_VALUES") = ix + 1
        End With
        database.SaveEntry(ds, False)
    End Sub

    Private Sub clearfields()
        txtItemcode.Clear()
        txtDescription.Clear()
        txtSalePrice.Clear()
        txtQty.Clear()

        TxtUnitPrice.Clear()
        txtUOM.Clear()
        txtSearch.Clear()
        txtSearch.Focus()
    End Sub

    Private Sub txtQty_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtQty.KeyPress
        If isEnter(e) Then btnSave.PerformClick()
    End Sub

    Private Sub frmAddInventory_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        If txtQty.Text = "" Then Exit Sub
        If txtQty.Text = 0 Then Exit Sub

        If btnAdd.Text = "&>" Then
            lvListIItems.SelectedItems(0).Text = txtItemcode.Text
            lvListIItems.SelectedItems(0).SubItems(1).Text = txtDescription.Text
            lvListIItems.SelectedItems(0).SubItems(2).Text = txtUOM.Text
            lvListIItems.SelectedItems(0).SubItems(3).Text = txtQty.Text
            lvListIItems.SelectedItems(0).SubItems(4).Text = TxtUnitPrice.Text
            lvListIItems.SelectedItems(0).SubItems(5).Text = txtSalePrice.Text
            btnAdd.Text = "&+"
            clearfields()
            Exit Sub
        End If

        For Each lv1 As ListViewItem In lvListIItems.Items
            If lv1.Text = txtItemcode.Text Then
                lv1.BackColor = Color.Cyan
                Exit Sub
            End If
        Next

        Dim lv As ListViewItem = lvListIItems.Items.Add(cd.ItemCode)
        lv.SubItems.Add(cd.Description)
        lv.SubItems.Add(cd.UnitofMeasure)
        lv.SubItems.Add(txtQty.Text)
        lv.SubItems.Add(TxtUnitPrice.Text)
        lv.SubItems.Add(txtSalePrice.Text)
        clearfields()
    End Sub

    Private Sub lvListIItems_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lvListIItems.DoubleClick
        If lvListIItems.SelectedItems.Count = 0 Then Exit Sub

        txtItemcode.Text = lvListIItems.SelectedItems(0).Text
        txtDescription.Text = lvListIItems.SelectedItems(0).SubItems(1).Text
        txtUOM.Text = lvListIItems.SelectedItems(0).SubItems(2).Text
        txtQty.Text = lvListIItems.SelectedItems(0).SubItems(3).Text
        TxtUnitPrice.Text = lvListIItems.SelectedItems(0).SubItems(4).Text
        txtSalePrice.Text = lvListIItems.SelectedItems(0).SubItems(5).Text
        btnAdd.Text = "&>"
    End Sub

    Private Sub btnRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemove.Click
        lvListIItems.SelectedItems(0).Remove()
    End Sub
End Class
