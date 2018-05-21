Public Class frmPLU

    Private queueIMD As New CollectionItemData
    Private qtyitm As Double = 1

    Friend Sub load_Items(Optional ByVal src As String = "")
        Dim quiloader As Integer = 0
        Dim mysql As String
        Dim ds As DataSet

        lvitemList.Items.Clear()

        queueIMD.Clear()

        mysql = "Select * from ItemMaster Where Onhold=0 limit 100"

        If src <> "" Then
            mysql = "Select * from ItemMaster where Onhold=0"
            mysql += String.Format(" and lower(Itemcode) like '%{0}%' Or Lower(Description) Like '%{0}%' Or Lower(Categories) like '%{0}%' Or Lower(SubCat) Like '%{0}%'", src.ToLower)
            mysql += " Order by ItemCode Asc"
        End If

        ds = LoadSQL("Select Count(*) from Itemmaster where Onhold =0")

        Dim maxresult = ds.Tables(0).Rows(0).Item(0)
        If maxresult = 0 Then Exit Sub

        dbReaderOpen()

        Dim dsR = LoadSQL_byDataReader(mysql)

        PG_Init(True, maxresult)

        While dsR.Read
            Dim itmreader As New cItemData
            itmreader.LoadReader_Item(dsR)

            queueIMD.add(itmreader)
            addItem(itmreader)

            quiloader += 1
            pgValue(quiloader)
            If quiloader Mod 10 = 0 Then Application.DoEvents()
        End While

        dbReaderClose()
        PG_Init(False)

        autoselect()
    End Sub

    Private Sub clearFields()
        txtSearch.Clear()
        lvitemList.Items.Clear()
    End Sub

    Private Sub addItem(ByVal itm As cItemData)
        Dim lv As ListViewItem = lvitemList.Items.Add(itm.ItemCode)
        If itm.onHold Then lv.BackColor = Color.Red : lv.ForeColor = Color.White : lv.ToolTipText = String.Format("{0} is On Hold", itm.ItemCode)

        lv.SubItems.Add(itm.Description)
        lv.SubItems.Add(itm.Category)
        lv.SubItems.Add(itm.UnitofMeasure)
        lv.SubItems.Add(itm.onHand)
        lv.SubItems.Add(ToCurrency(itm.SalePrice))

    End Sub

    Private Sub autoselect()
        lvitemList.Focus()
        lvitemList.Items(0).Selected = True
    End Sub

    Friend Sub SearchSelect(ByVal str As String)
        Dim qty As String = ""
        If Not str.Contains("*") Then
            txtSearch.Text = str
        Else
            qty = str.Split("*")(0)
            qtyitm = If(IsNumeric(qty), qty, 0)
            txtSearch.Text = str.Split("*")(1)
        End If

        btnSearch.PerformClick()
    End Sub

    Private Function ToCurrency(ByVal money As Double) As String
        Return money.ToString("#,##0.00")
    End Function

    Private Sub PG_Init(ByVal st As Boolean, Optional ByVal max As Integer = 0)
        Pb_itm.Visible = st
        Pb_itm.Value = 0
        Pb_itm.Maximum = max
    End Sub

    Private Sub pgValue(ByVal val As Integer)
        Pb_itm.Value = val
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Dim str_src As String = txtSearch.Text

        load_Items(DreadKnight(str_src))
    End Sub

    Private Sub lvitemList_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles lvitemList.KeyPress

    End Sub

    Private Sub txtSearch_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearch.Leave
        autoselect()
    End Sub

    Private Sub btncancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btncancel.Click
        Me.Close()
    End Sub

    Private Sub btnselect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnselect.Click
        If lvitemList.SelectedItems.Count = 0 Then Exit Sub

        Dim idx As Integer = lvitemList.FocusedItem.Index

        Dim selected_itm As New cItemData

        selected_itm = queueIMD.Item(idx)

        If selected_itm.onHold Then
            MsgBox("ITEM IS CURRENTLY ONHOLD", MsgBoxStyle.Critical, "Information")
            Exit Sub
        End If

        Dim isSelected As Boolean = False

        For Each itm As ListViewItem In frmTransaction.lvSale.Items
            If itm.Text = selected_itm.ItemCode Then
                isSelected = True
                Exit For
            End If
        Next

        selected_itm.Quantity = qtyitm
        selected_itm.SRP = selected_itm.SalePrice
        frmTransaction.AddItem(selected_itm)
        frmTransaction.txtSearch.Clear()
        Me.Close()
    End Sub

    Private Sub Refreshitems(Optional ByVal itmcode As String = "")
        If itmcode = "" Then
            load_Items()
            Exit Sub
        End If

        Dim loadItems As New cItemData
        loadItems.ItemCode = itmcode
        loadItems.Load_ItemCode()

        Dim lv As ListViewItem = lvitemList.Items.Add(loadItems.ItemCode)

        lv.SubItems.Add(loadItems.Description)
        lv.SubItems.Add(loadItems.Category)
        lv.SubItems.Add(loadItems.UnitofMeasure)
        lv.SubItems.Add(loadItems.onHand)
        lv.SubItems.Add(ToCurrency(loadItems.SalePrice))
    End Sub
End Class