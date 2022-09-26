Public Class frmDiscount
    Friend itemcode As String
    Friend ht_broughtItems_discounted As Hashtable
    Private Sub frmDiscount_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtDiscount.Text = 0.00
        txtDiscount.Focus()
    End Sub

    Private Sub txtDiscount_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtDiscount.KeyPress
        DigitOnly(e)
    End Sub

    Private Sub txtDiscount_KeyDown(sender As Object, e As KeyEventArgs) Handles txtDiscount.KeyDown
        If txtDiscount.Text = 0 Then MsgBox("No discounted has been added.", MsgBoxStyle.Information) : Exit Sub
        If e.KeyCode = Keys.Enter Then
            With frmTransaction.lvSale.FindItemWithText(itemcode)
                .SubItems(3).Text = txtDiscount.Text

                Dim ItemAmount As Decimal
                ItemAmount = (.SubItems(2).Text * .SubItems(3).Text)
                .SubItems(4).Text = ItemAmount.ToString("#,##0.00")

                If ht_broughtItems_discounted.ContainsKey(itemcode) Then
                    Dim tmp As cItemData = ht_broughtItems_discounted.Item(itemcode)
                    tmp.SalePrice = txtDiscount.Text
                End If
            End With
            Me.Close()
        End If
    End Sub
End Class