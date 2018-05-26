Public Class frmReceiptView

    Friend Sub LoadItems(ByVal IDx As Integer)
        lvSaleItem.Items.Clear()
        Dim mysql As String = "Select * From DocLines Where DocID = " & IDx
        Dim ds As DataSet = LoadSQL(mysql)

        For Each dr In ds.Tables(0).Rows
            Additems(dr)
        Next
    End Sub

    Private Sub Additems(ByVal dr As DataRow)
        With dr
            Dim lv As ListViewItem = lvSaleItem.Items.Add(.Item("DLID"))
            lv.SubItems.Add(.Item("ItemCode"))
            lv.SubItems.Add(.Item("Description"))
            Dim tmp As Double = .Item("Qty")
            lv.SubItems.Add(tmp.ToString("#,##0.00"))
            lv.SubItems.Add(FormatCurrency(.Item("SalePrice")))
            lv.SubItems.Add(FormatCurrency(.Item("RowTotal")))
        End With

    End Sub

    Private Sub frmReceiptView_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class