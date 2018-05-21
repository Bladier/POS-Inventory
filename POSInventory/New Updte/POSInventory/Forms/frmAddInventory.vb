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
        If txtQty.Text = "" Then Exit Sub

        Dim mysql As String = "Select * from INV limit 1"
        Dim ds As DataSet = LoadSQL(mysql, "INV")

        Dim dsnewrow As DataRow
        dsnewrow = ds.Tables(0).NewRow
        With dsnewrow
            .Item("DocNum") = GetInvNum()
            .Item("DocDate") = CurrentDate
            .Item("Partner") = "N/A"
        End With
        ds.Tables(0).Rows.Add(dsnewrow)
        database.SaveEntry(ds)

        mysql = "Select * from INV order by DocID desc Limit 1"
        ds.Clear()
        ds = LoadSQL(mysql, "INV")

        Dim docid As Integer = ds.Tables(0).Rows(0).Item("DocID")


        mysql = "Select * from InvLines limit 1"
        ds.Clear()
        ds = LoadSQL(mysql, "InvLines")

        dsnewrow = ds.Tables(0).NewRow
        With dsnewrow
            .Item("DocID") = docid
            .Item("itemcode") = cd.ItemCode
            .Item("Description") = cd.Description
            .Item("QTY") = txtQty.Text
            .Item("UOM") = cd.UnitofMeasure
            .Item("Remarks") = "UPLOADED DATE " & CurrentDate
        End With
        ds.Tables(0).Rows.Add(dsnewrow)
        database.SaveEntry(ds)

        AddInventory(cd.ItemCode, txtQty.Text)

        MsgBox("Inventory Added", MsgBoxStyle.Information, "Inventory")
        clearfields()
        ' updateInvNum()
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
        txtremarks.Clear()
        TxtUnitPrice.Clear()
        txtUOM.Clear()
        txtSearch.Clear()
        txtSearch.Focus()
    End Sub

    Private Sub txtQty_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtQty.KeyPress
        If isEnter(e) Then btnSave.PerformClick()
    End Sub
End Class
