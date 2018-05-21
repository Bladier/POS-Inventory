Public Class frmIMDList
    Friend isAddInv As Boolean = False
    Friend isAdd_Or_Update_IMD As Boolean = False

    Private Sub frmIMDList_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If txtSearch.Text <> "" Then
            searchitem(txtSearch.Text)
        Else
            loadIMD()
        End If

    End Sub


    Private Sub loadIMD(Optional ByVal mysql As String = "select * from itemmaster order by itemid asc limit 30")
        Dim ds As DataSet = LoadSQL(mysql, "itemmaster")

        If ds.Tables(0).Rows.Count = 0 Then lvIMDList.Items.Clear() : Exit Sub

        lvIMDList.Items.Clear()
        For Each dr As DataRow In ds.Tables(0).Rows
            Dim cd As New cItemData

            cd.Load_Item(dr(0))
            additem(cd)
        Next

    End Sub


    Private Sub additem(ByVal cim As cItemData)
        If cim.ItemCode = Nothing Then Exit Sub

        With cim
            Dim lv As ListViewItem = lvIMDList.Items.Add(.ItemCode)
            lv.SubItems.Add(.Description)
            lv.SubItems.Add(.Category)
            lv.SubItems.Add(.SubCategory)
            lv.SubItems.Add(.UnitofMeasure)
            lv.SubItems.Add(.SalePrice.ToString("#,###.00"))
            lv.SubItems.Add(.onHand)
            lv.Tag = .ItemID
        End With


    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        If txtSearch.Text = "" Then Exit Sub
        searchitem(txtSearch.Text)
    End Sub

    Private Sub searchitem(ByVal str As String)
        Dim mysql As String = "Select * from itemmaster where Itemcode like '%" + txtSearch.Text + "%'"
        mysql += "Or Description like '%" + txtSearch.Text + "%'"

        loadIMD(mysql)
    End Sub

    Private Sub txtSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearch.TextChanged
        If txtSearch.Text <> "" Then
            searchitem(txtSearch.Text)
        Else
            loadIMD()
        End If

    End Sub

    Private Sub lvIMDList_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lvIMDList.DoubleClick
        selectitem()
    End Sub

    Private Sub selectitem()
        If lvIMDList.SelectedItems.Count = 0 Then Exit Sub

        Dim idx As Integer = lvIMDList.FocusedItem.Tag
        Dim cd As New cItemData

        cd.Load_Item(idx)

        If isAdd_Or_Update_IMD Then
            frmIMD.loaditem(cd)
            frmIMD.Show()
        End If

        If isAddInv Then
            frmAddInventory.loaditem(cd)
            frmAddInventory.Show()
        End If
        Me.Close()
    End Sub

    Private Sub lvIMDList_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles lvIMDList.KeyPress
        If isEnter(e) Then
            selectitem()
        End If
    End Sub
End Class