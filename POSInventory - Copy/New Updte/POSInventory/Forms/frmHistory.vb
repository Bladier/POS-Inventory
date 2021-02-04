Public Class frmHistory



    Private Sub load_InvHistory(Optional ByVal mysql As String = "select * from inv where docStatus=1 limit 100")
        Dim ds As DataSet = LoadSQL(mysql, "inv")

        If ds.Tables(0).Rows.Count = 0 Then ListView1.Items.Clear() : Exit Sub

        ListView1.Items.Clear()
        For Each dr As DataRow In ds.Tables(0).Rows
            Dim lv As ListViewItem = ListView1.Items.Add(dr.Item("DocDate"))
            lv.SubItems.Add(dr.Item("DocNum"))
            lv.SubItems.Add(dr.Item("GrandTotal"))
            lv.Tag = dr.Item(0)
        Next

    End Sub

    Private Sub search_History(ByVal str As String)
        Dim mysql As String = "SELECT * FROM inv WHERE lower(docnum) like '%" + txtSearch.Text + "%'"
        load_InvHistory(mysql)
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        If txtSearch.Text <> "" Then
            search_History(txtSearch.Text)
        End If
    End Sub

    Private Sub frmHistory_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        load_InvHistory()
    End Sub

    Private Sub btnSelect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelect.Click
        If ListView1.SelectedItems.Count = 0 Then Exit Sub

        Dim iv As New Inv
        Dim idx As Integer = ListView1.SelectedItems(0).Tag
        iv.LoadItem(idx)
        frmAddInventory.load_History(iv)
        frmAddInventory.isVoid = True
        frmAddInventory.Show()


        Me.Close()

    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub
End Class