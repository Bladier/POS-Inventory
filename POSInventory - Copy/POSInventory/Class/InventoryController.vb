

Module InventoryController
    Dim mySql As String, ds As DataSet

    Const INTEGRITY_CHECK As String = "9nKFB3fmcquj4CNHjDz7ako7NCP2cpx3ja1XPVCwSgTDXjbNRKuRTgyV2ygWdcvc8mV/vG1t3leyUnkBi4x01kTBWpCzI17cr9ajzTfyBG5v0JgzlkvNGddbI/mLjBhlyCNWEPkpdJU="

    Friend Sub AddInventory(ByVal itemCode As String, Optional ByVal Qty As Double = 1)
        mySql = String.Format("SELECT * FROM ITEMMASTER WHERE ITEMCODE = '{0}'", itemCode)
        ds = LoadSQL(mySql, "ITEMMASTER")

        If Not ds.Tables(0).Rows.Count = 1 Then
            Console.WriteLine("PROBLEM ON ITEM " & itemCode)
            MsgBox("PROBLEM ON ITEM " & itemCode, MsgBoxStyle.Critical)
            Exit Sub
        End If

        ds.Tables("ITEMMASTER").Rows(0).Item("ONHAND") += Qty
        ds.Tables("ITEMMASTER").Rows(0).Item("UPDATETIME") = Now()
        database.SaveEntry(ds)
    End Sub

    Friend Sub DeductInventory(ByVal itemCode As String, Optional ByVal Qty As Double = 1)
        mySql = String.Format("SELECT * FROM ITEMMASTER WHERE ITEMCODE = '{0}'", itemCode)
        ds = LoadSQL(mySql, "ITEMMASTER")

        If Not ds.Tables(0).Rows.Count = 1 Then
            Console.WriteLine("PROBLEM ON ITEM " & itemCode)
            MsgBox("PROBLEM ON ITEM " & itemCode, MsgBoxStyle.Critical)
            Exit Sub
        End If

        ds.Tables("ITEMMASTER").Rows(0).Item("ONHAND") -= Qty
        ds.Tables("ITEMMASTER").Rows(0).Item("UPDATETIME") = Now()
        database.SaveEntry(ds, False)
    End Sub


    Friend Function TemplateIntegrityCheck(ByVal headers() As String) As Boolean
        Dim mergeHeaders As String = ""
        For Each head In headers
            mergeHeaders &= head
        Next : If HashString(mergeHeaders) = INTEGRITY_CHECK Then Return True
        Return False
    End Function


    Friend Function Get_Endig(ByVal itemcde As String, ByVal qty As Double) As Double
        mySql = String.Format("SELECT * FROM ITEMMASTER WHERE ITEMCODE = '{0}'", itemcde)
        ds = LoadSQL(mySql, "ITEMMASTER")
        Dim OnHand_stock As Double = ds.Tables(0).Rows(0).Item("OnHand") - qty

        Return OnHand_stock
    End Function
End Module
