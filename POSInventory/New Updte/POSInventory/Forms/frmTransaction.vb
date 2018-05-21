Imports Microsoft.Reporting.WinForms
Public Class frmTransaction
    Friend ht_BroughtItems As New Hashtable

    Friend accessType As String = ""

    Private VAT As Double = 0
    Private DOC_TOTAL As Double = 0
    Private DOC_NOVAT As Double = 0
    Private DOC_VATTOTAL As Double = 0
    Private InvoiceNum As Double = GetOption("InvoiceNum")
    Private ReturnNum As Double = GetOption("SalesReturnNum")
    Private StockOutNum As Double = GetOption("STONum")

    'Private PRINTER_PT As String = GetOption("PRINTER")

    Private DOC_TYPE As Integer = 0
    Dim Customer As String = "One Time Customer"
    Dim IsRequired_CINum As Boolean = True
   
    Enum TransType As Integer
        Cash = 0
        Returns = 1
        StockOut = 2
        Encashment = 3
        PayMayaRetail = 4
    End Enum

    Friend TransactionMode As TransType

    Private Sub frmTransaction_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If hasVAT() Then
            lblNoVat.Visible = True
        Else
            lblNoVat.Visible = False
        End If

        ClearField()


        txtSearch.Focus()
    End Sub

    Private Function hasVAT() As Boolean

        'VAT = 12 / 100
        If VAT = 0 Then
            Return False
        Else
            Return True
        End If
    End Function

    Private Function CheckOR() As Boolean
        Dim mySql As String = "SELECT * FROM DOC WHERE CODE = "
        Dim prefix As String = ""
        Dim ControlNum As Integer = 0


        Select Case TransactionMode
            Case TransType.Returns
                prefix = "RET"
                ControlNum = ReturnNum
            Case TransType.Cash, TransType.PayMayaRetail
                prefix = "INV"
                ControlNum = InvoiceNum
            Case TransType.StockOut
                prefix = "STO"
                ControlNum = StockOutNum
        End Select

        Dim uniq As String = String.Format("'{1}#{0:000000}'", ControlNum, prefix)
        mySql &= uniq


        Dim ds As DataSet = LoadSQL(mySql)
        If ds.Tables(0).Rows.Count >= 1 Then
            MsgBox("NUMBER ALREADY EXISTED" + vbCrLf + "PLEASE BE ADVICED", MsgBoxStyle.Critical, uniq)
            Return False
        End If
        Return True
    End Function


    Private Sub AutoSelect()
        lvSale.Focus()
        If lvSale.Items.Count = 0 Then Exit Sub

        lvSale.Items(0).Selected = True
    End Sub

    Private Sub btnPost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPost.Click

        If Not MsgBox("Do you want to POST?", MsgBoxStyle.YesNo + MsgBoxStyle.Information + vbDefaultButton2, "POSTING...") = vbYes Then
            Exit Sub
        End If

        If Not CheckOR() Then Exit Sub
        If lvSale.Items.Count = 0 Then MsgBox("Nothing to be Post!", MsgBoxStyle.Critical, "Error") : Exit Sub

        Dim mySql As String, fillData As String
        Dim getLastID As Integer = 0
        Dim Remarks As String = ""
        Dim prefix As String = "", DocCode As String = ""

        If TransactionMode = TransType.StockOut Then Remarks = InputBox("PARTICULARS", "Particulars")

        'Creating Document
        mySql = "SELECT * FROM DOC LIMIT 1"
        fillData = "DOC"

        Dim ds As DataSet = LoadSQL(mySql, fillData)
        Dim dsNewRow As DataRow
        dsNewRow = ds.Tables(fillData).NewRow

        Select Case TransactionMode
            Case TransType.Returns
                prefix = "RET"
                DocCode = String.Format("{1}#{0:000000}", ReturnNum, prefix)
            Case TransType.Cash, TransType.PayMayaRetail
                prefix = "INV"
                DocCode = String.Format("{1}#{0:000000}", InvoiceNum, prefix)
            Case TransType.StockOut
                prefix = "STO"
                DocCode = String.Format("{1}#{0:000000}", StockOutNum, prefix)
        End Select

        With dsNewRow
            .Item("DOCTYPE") = DOC_TYPE
            .Item("CODE") = DocCode
            .Item("MOP") = GetModesOfPayment(TransactionMode)
            .Item("CUSTOMER") = Customer
            .Item("DOCDATE") = CurrentDate
            .Item("NOVAT") = DOC_NOVAT
            .Item("VATRATE") = VAT
            .Item("VATTOTAL") = DOC_VATTOTAL
            .Item("DOCTOTAL") = DOC_TOTAL
            .Item("USERID") = 0
            If Remarks <> "" Then .Item("REMARKS") = Remarks
        End With
        ds.Tables(fillData).Rows.Add(dsNewRow)

        database.SaveEntry(ds)
        Dim DOCID As Integer = 0

        mySql = "SELECT * FROM DOC ORDER BY DOCID DESC LIMIT 1"
        ds = LoadSQL(mySql, fillData)
        DOCID = ds.Tables(fillData).Rows(0).Item("DOCID")

        Console.Write("Loading")
        While DOCID = 0
            Application.DoEvents()
            Console.Write("Saving DOC first...+++++++++++++++++++++++>>>>>>>>>>> BladeGamer")
        End While
        Console.Write("Done saveing DOC ...+++++++++++++++++++++++>>>>>>>>>>> BladeGamer")

        'Creating DocumentLines
        mySql = "SELECT * FROM DOCLINES LIMIT 1"
        fillData = "DOCLINES"
        ds = LoadSQL(mySql, fillData)

        For Each ht As DictionaryEntry In ht_BroughtItems
            Dim itm As New cItemData
            itm = ht.Value

            dsNewRow = ds.Tables(fillData).NewRow
            With dsNewRow
                .Item("DOCID") = DOCID
                .Item("ITEMCODE") = itm.ItemCode
                .Item("DESCRIPTION") = itm.Description
                .Item("QTY") = itm.Quantity * IIf(TransactionMode = TransType.Returns, -1, 1)
                .Item("UNITPRICE") = itm.UnitPrice
                .Item("SALEPRICE") = itm.SalePrice
                .Item("ROWTOTAL") = itm.SalePrice * itm.Quantity
                .Item("UOM") = itm.UnitofMeasure
                .Item("Ending") = 0
            End With
            ds.Tables(fillData).Rows.Add(dsNewRow)

            database.SaveEntry(ds)

            If itm.isInventoriable Then
                If TransactionMode = TransType.Returns Then
                    InventoryController.AddInventory(itm.ItemCode, itm.Quantity)
                ElseIf TransactionMode = TransType.Cash _
                   OrElse TransactionMode = TransType.StockOut Then
                    InventoryController.DeductInventory(itm.ItemCode, itm.Quantity)
                End If
            End If
        Next

        ItemPosted()

        'If MsgBox("Do you want to print it?", MsgBoxStyle.Information + MsgBoxStyle.YesNo + vbDefaultButton2, "PRINT") = MsgBoxResult.Yes Then
        '    PrintOR(DOCID)
        'End If

        MsgBox("ITEM POSTED", MsgBoxStyle.Information)

        ClearField()
    End Sub

    Private Function GetDocLines_LastID() As Integer
        Dim mySql As String = "SELECT * FROM DOCLINES ORDER BY DLID DESC ROWS 1"
        Dim ds As DataSet = LoadSQL(mySql)

        Return ds.Tables(0).Rows(0).Item("DLID")
    End Function


    Private Sub ItemPosted()

        Select Case TransactionMode
            Case TransType.Returns
                ReturnNum += 1 'INCREMENT Return Control Number
                UpdateOptions("SalesReturnNum", ReturnNum)
            Case TransType.Cash, TransType.PayMayaRetail
                InvoiceNum += 1 'INCREMENT Invoice Control Number
                UpdateOptions("InvoiceNum", InvoiceNum)
            Case TransType.StockOut
                StockOutNum += 1 'INCREMENT Stock Out Control Number
                UpdateOptions("STONum", StockOutNum)
        End Select

        ht_BroughtItems.Clear()
    End Sub


    Private Sub btnCash_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCash.Click
        If ShiftMode() Then
            Load_asCash()
        End If
    End Sub

    'Private Sub btnSalesReturn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSalesReturn.Click

    '    If ShiftMode() Then
    '        btnPost.Enabled = True
    '        Load_asReturns()
    '        Exit Sub
    '    End If

    'End Sub

    Private Sub btnStockOut_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStockOut.Click
            If ShiftMode() Then
                btnPost.Enabled = True
                Load_asStockOut()
            End If
    End Sub

    '    Private Sub btnReceipt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReceipt.Click
    '        frmPrint.Show()
    '    End Sub

    Private Sub RefreshToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RefreshToolStripMenuItem.Click
        ClearField()
    End Sub

    Private Function GetModesOfPayment(ByVal x As TransType)
        Select Case x
            Case TransType.Cash
                Return "C"
            Case TransType.Returns
                Return "R"
            Case TransType.StockOut
                Return "S"
        End Select

        Return "0"
    End Function

#Region "Load Transactions Type"
    Private Sub Load_ORNUM()
        Select Case TransactionMode
            Case TransType.Returns
                ReturnNum = GetOption("SalesReturnNum")
            Case TransType.Cash, TransType.PayMayaRetail
                InvoiceNum = GetOption("InvoiceNum")
            Case TransType.StockOut
                StockOutNum = GetOption("STONum")
        End Select
    End Sub

    Private Sub Load_asReturns()
        lblMode.Text = "RETURNS"
        TransactionMode = TransType.Returns
        lblSearch.Text = "ITEM:"

        Load_ORNUM()
        DOC_TYPE = 3
    End Sub

    Private Sub Load_asStockOut()
        lblMode.Text = "STOCKOUT"
        TransactionMode = TransType.StockOut
        lblSearch.Text = "ITEM:"

        Load_ORNUM()
        DOC_TYPE = 4
    End Sub

    Private Sub Load_asCash()
        lblMode.Text = "CASH"
        TransactionMode = TransType.Cash
        lblSearch.Text = "ITEM:"

        Load_ORNUM()
        DOC_TYPE = 0
    End Sub

    Private Function ShiftMode() As Boolean
        If lvSale.Items.Count = 0 Then Return True

        Dim ans = MsgBox("You are about the change MODE. And the List will be clear" + vbCr + "Do you want to continue?", MsgBoxStyle.Information + MsgBoxStyle.YesNo)
        If ans = MsgBoxResult.Yes Then
            ClearField()
            Return True
        End If

        Return False
    End Function
#End Region

#Region "GUI"
    Private Sub ClearField()
        lvSale.Items.Clear()
        txtSearch.Text = ""
        lblNoVat.Text = Display_NoVat(0)
        Display_Total(0)
        Load_asCash()
        ht_BroughtItems.Clear()
    End Sub

    Private Function Display_Total(ByVal tot As Double) As Double
        Dim final As Double = tot.ToString("##000.00")

        Dim TOTALVAT As Double = final * VAT
        Display_NoVat(final)
        DOC_VATTOTAL = TOTALVAT

        final += TOTALVAT
        lblTotal.Text = String.Format("Php {0:#,##0.00}", final)
        DOC_TOTAL = final

        Return final
    End Function

    Private Function Display_NoVat(ByVal tot As Double) As Double
        lblNoVat.Text = String.Format("Php {0:#,##0.00}", tot)
        DOC_NOVAT = tot

        Return tot
    End Function
#End Region

    Public Sub New()
        MyBase.New()
        InitializeComponent()
        Me.MaximumSize = New Size(830, 471)
        Me.StartPosition = FormStartPosition.CenterScreen
    End Sub

    '#Region "Print"

    '    Private Sub PrintOR(ByVal docID As Integer)
    '        ' Check if able to print
    '        Dim printerName As String = PRINTER_PT
    '        If Not canPrint(printerName) Then Exit Sub

    '        ' Execute SQL
    '        Dim mySql As String = "SELECT * FROM SALES_OR WHERE DOCID = " & docID
    '        Dim ds As DataSet, fillData As String = "OR"
    '        ds = LoadSQL(mySql, fillData)

    '        ' Declare AutoPrint
    '        Dim autoPrint As Reporting
    '        Dim report As LocalReport = New LocalReport
    '        autoPrint = New Reporting

    '        ' Initialize Auto Print
    '        report.ReportPath = "Reports\OfficialReceipt.rdlc"
    '        report.DataSources.Add(New ReportDataSource(fillData, ds.Tables(fillData)))

    '        ' Assign Parameters
    '        Dim dic As New Dictionary(Of String, String)
    '        With ds.Tables(0).Rows(0)
    '            dic.Add("txtORNum", .Item("CODE"))
    '            dic.Add("txtPostingDate", .Item("DOCDATE"))
    '            dic.Add("txtCustomer", .Item("CUSTOMER"))
    '        End With

    '        ' Importer Parameters
    '        If Not dic Is Nothing Then
    '            For Each nPara In dic
    '                Dim tmpPara As New ReportParameter
    '                tmpPara.Name = nPara.Key
    '                tmpPara.Values.Add(nPara.Value)
    '                report.SetParameters(New ReportParameter() {tmpPara})
    '                Console.WriteLine(String.Format("{0}: {1}", nPara.Key, nPara.Value))
    '            Next
    '        End If

    '        ' Executing Auto Print
    '        autoPrint.Export(report)
    '        autoPrint.m_currentPageIndex = 0
    '        autoPrint.Print(printerName)

    '        'frmReport.ReportInit(mySql, "OR", "Reports\OfficialReceipt.rdlc", dic)
    '        'frmReport.Show()
    '    End Sub


    '    Private Function canPrint(ByVal printerName As String) As Boolean
    '        Try
    '            Dim printDocument As Drawing.Printing.PrintDocument = New Drawing.Printing.PrintDocument
    '            printDocument.PrinterSettings.PrinterName = printerName
    '            Return printDocument.PrinterSettings.IsValid
    '        Catch ex As Exception
    '            Return False
    '        End Try
    '    End Function
    '#End Region


    '#Region "Function"
    '    ''' <summary>
    '    ''' Get the current VAT
    '    ''' </summary>
    '    ''' <returns>Boolean</returns>
    '    ''' <remarks></remarks>
    '    ''' 
    Friend Sub AddItem(ByVal itm As cItemData)
        Dim ItemAmount As Double
        Dim hasSelected As Boolean = False

        For Each AddedItems As ListViewItem In lvSale.Items
            If AddedItems.Text = itm.ItemCode Then
                hasSelected = True
                Exit For
            End If
        Next

        If hasSelected Then
            With lvSale.FindItemWithText(itm.ItemCode)

                Console.WriteLine("Old Qty " & .SubItems(2).Text)
                .SubItems(2).Text += itm.Quantity
                Console.WriteLine("New Qty " & .SubItems(2).Text)

                ItemAmount = (.SubItems(2).Text * .SubItems(3).Text)
                .SubItems(4).Text = ItemAmount.ToString("#,##0.00")
            End With
        Else

            'If itm.ItemCode = "SMM 00001" OrElse itm.ItemCode = "SMP 00001" Then
            '    IsRequired_CINum = False
            '    If TransactionMode = TransType.Encashment Then
            '        Dim lvEncash As ListViewItem = lvSale.Items.Add(itm.ItemCode)
            '        lvEncash.SubItems.Add(itm.Description)
            '        lvEncash.SubItems.Add(itm.Quantity)

            '        lvEncash.SubItems.Add(itm.SalePrice.ToString("#,##0.00"))
            '        ItemAmount = (itm.SalePrice * itm.Quantity)

            '        lvEncash.SubItems.Add(ItemAmount.ToString("#,##0.00"))
            '        lvEncash.SubItems.Add(itm.SRP.ToString("#,##0.00"))
            '        lvEncash.SubItems.Add("One Time Customer")
            '    Else
            '        Dim Computation As SMPCompute
            '        Computation = New SMPCompute(itm.Quantity, itm.ItemCode)

            '        Dim lv As ListViewItem = lvSale.Items.Add(itm.ItemCode)
            '        lv.SubItems.Add(itm.Description)
            '        lv.SubItems.Add(Computation.TotalAmount)

            '        lv.SubItems.Add(itm.SalePrice.ToString("#,##0.00"))
            '        ItemAmount = (itm.SalePrice * Computation.TotalAmount)

            '        lv.SubItems.Add(ItemAmount.ToString("#,##0.00"))
            '        lv.SubItems.Add(itm.SRP.ToString("#,##0.00"))
            '        itm.Quantity = Computation.TotalAmount
            '        lv.SubItems.Add("One Time Customer")

            '        If TransactionMode = TransType.Cash Then
            '            Dim itm2 As New cItemData
            '            itm2.ItemCode = "SMM 00002"
            '            itm2.Load_Item()

            '            itm2.Quantity = Computation.Commission

            '            Dim lv2 As ListViewItem = lvSale.Items.Add(itm2.ItemCode)
            '            lv2.SubItems.Add(itm2.Description)
            '            lv2.SubItems.Add(itm2.Quantity)

            '            lv2.SubItems.Add(itm2.SalePrice.ToString("#,##0.00"))
            '            ItemAmount = (itm2.SalePrice * itm2.Quantity)

            '            lv2.SubItems.Add(ItemAmount.ToString("#,##0.00"))
            '            lv2.SubItems.Add(itm2.SRP.ToString("#,##0.00"))
            '            lv2.SubItems.Add("One Time Customer")

            '            ht_BroughtItems.Add(itm2.ItemCode, itm2)
            '        End If
            '    End If
            'Else
            'If NEW
            Dim lv As ListViewItem = lvSale.Items.Add(itm.ItemCode)
            lv.SubItems.Add(itm.Description)
            lv.SubItems.Add(itm.Quantity)

            lv.SubItems.Add(itm.SalePrice.ToString("#,##0.00"))
            ItemAmount = (itm.SalePrice * itm.Quantity)

            lv.SubItems.Add(ItemAmount.ToString("#,##0.00"))
            lv.SubItems.Add(itm.SRP.ToString("#,##0.00"))
            lv.SubItems.Add("One Time Customer")

        End If

        Dim src_idx As String = itm.ItemCode

        If ht_BroughtItems.ContainsKey(src_idx) Then
            Dim tmp As cItemData = ht_BroughtItems.Item(src_idx)
            tmp.Quantity += itm.Quantity
        Else
            ht_BroughtItems.Add(src_idx, itm)
        End If
        DOC_TOTAL = 0
        For Each lv As ListViewItem In lvSale.Items
            DOC_TOTAL += CDbl(lv.SubItems(4).Text)
        Next

        Display_Total(DOC_TOTAL)
    End Sub

    '    Friend Sub ClearSearch()
    '        txtSearch.Text = ""
    '        txtSearch.Focus()
    '    End Sub

    '    Private Sub ForcePosting()
    '        btnPost.PerformClick()
    '    End Sub

    '    Private Sub Form_HotKeys(ByVal e As System.Windows.Forms.KeyEventArgs)
    '        Select Case e.KeyCode
    '            Case 112 'F1
    '                'frmIMD.Show()
    '            Case 114 'F3
    '                'tsbCustomer.PerformClick()
    '            Case 116 'F5
    '                ' tsbCash.PerformClick()
    '            Case 117 'F6
    '                'tsbCheck.PerformClick()
    '            Case 120 'F9
    '                ForcePosting()
    '        End Select
    '    End Sub


    '#End Region


    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click

        frmPLU.Show()
        'frmPLU.From_Sales()
       
        If txtSearch.Text.Length > 0 Then frmPLU.SearchSelect(txtSearch.Text) : Exit Sub

        frmPLU.load_Items()
    End Sub

    '    Private Sub lvSale_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lvSale.DoubleClick
    '        If Not tmpRef.IsRequiredRefNum(lvSale.SelectedItems(0).SubItems(0).Text) Then
    '            Exit Sub
    '        End If

    '        Customer = InputBox("Enter reference number", "Reference #", Customer)
    '        If Customer = "" Then Exit Sub
    '        Dim iRowCount As Integer = lvSale.SelectedIndices(0)
    '        lvSale.SelectedItems(0).SubItems(6).Text = Customer


    '        If TransactionMode = TransType.Cash OrElse TransactionMode = TransType.PayMayaRetail Then
    '            If isGlobeWallet Then GoTo goHere
    '            lvSale.Items(iRowCount + 1).SubItems(6).Text = Customer
    '        End If
    'goHere:
    '        lvSale.SelectedItems(0).BackColor = Color.White
    '    End Sub

    '    Private Sub txtSearch_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSearch.KeyPress
    '        If isEnter(e) Then btnSearch.PerformClick()
    '    End Sub

    '    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
    '        Dim msg As DialogResult = MsgBox("Do you want cancel?", MsgBoxStyle.YesNo + MsgBoxStyle.Question, "Cancel")
    '        If msg = vbNo Then Exit Sub
    '        Me.Close()
    '    End Sub

    '    Private Sub lvSale_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles lvSale.KeyPress
    '        If isEnter(e) Then lvSale_DoubleClick(sender, e)
    '    End Sub

    '    Private Sub lvSale_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles lvSale.KeyDown
    '        If lvSale.SelectedItems.Count = 0 Then Exit Sub

    '        If e.KeyCode = Keys.Delete Then
    '            Dim idx As Integer = lvSale.FocusedItem.Index
    '            If Not IsNumeric(lvSale.Items(idx).SubItems(2).Text) Then
    '                Log_Report(String.Format("[SALES DELETE] {0} have an NON-NUMERIC QTY", lvSale.Items(idx).Text))
    '                MsgBox(String.Format("{0} has NON-NUMERIC Quantity", lvSale.Items(idx).Text), MsgBoxStyle.Critical, "INVALID ITEMCODE")
    '                Exit Sub
    '            End If

    '            Console.WriteLine("Removing " & lvSale.Items(idx).Text)

    '            If MsgBox("Do you want remove this item?", MsgBoxStyle.YesNo + MsgBoxStyle.Information + vbDefaultButton2, "Removing...") = vbYes Then
    '                Dim itm As New cItemData
    '                itm.ItemCode = lvSale.Items(idx).Text
    '                itm.Load_Item()

    '                If itm.ItemCode = "SMT 00071" Then
    '                    DOC_TOTAL -= GetEloadPrice(CDbl(lvSale.Items(idx).SubItems(2).Text)) * CDbl(lvSale.Items(idx).SubItems(2).Text)
    '                Else
    '                    DOC_TOTAL -= CDbl(lvSale.Items(idx).SubItems(3).Text) * CDbl(lvSale.Items(idx).SubItems(2).Text)
    '                End If

    '                ht_BroughtItems.Remove(itm.ItemCode)
    '                lvSale.Items(idx).Remove()

    '            Else
    '                Exit Sub
    '            End If
    '            If lvSale.Items.Count = 0 Then DOC_TOTAL = 0 : ht_BroughtItems.Clear()
    '            Display_Total(DOC_TOTAL)
    '        End If

    '    End Sub


    '    Private Sub btnOptions_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOptions.Click
    '        If TransactionMode <> TransType.Cash Then Exit Sub
    '        While Not IsNumeric(CINum)
    '            CINum = InputBox("Enter CI #", "Cash Invoice Number", "")
    '            If CINum = "" Then Exit Sub
    '        End While
    '    End Sub

    '    Private Function IsCINumExist() As Boolean
    '        If CINum = "" Then Return False
    '        Dim mysql As String = "SELECT * FROM DOC WHERE CINUM = '" & CINum & "' AND STATUS <> 0"
    '        Dim ds As DataSet = LoadSQL(mysql, "DOC")

    '        Try
    '            If ds.Tables(0).Rows.Count > 0 Then
    '                MsgBox("CI # " & CINum & " already exists.", MsgBoxStyle.Critical, "Error")
    '                Return True
    '            End If
    '        Catch ex As Exception
    '            Return False
    '        End Try
    '        Return False
    '    End Function

    '    Private Function isReferenceExists(ByVal refnum As String) As Boolean
    '        Dim mysql As String = "SELECT * FROM DOC WHERE CUSTOMER = '" & refnum & "' AND STATUS <> 0"
    '        Dim ds As DataSet = LoadSQL(mysql, "DOC")

    '        Try
    '            If ds.Tables(0).Rows.Count > 0 Then
    '                MsgBox("This Transaction is already saved or Reference # already existed.", MsgBoxStyle.Critical, "Error")
    '                Return True
    '            End If
    '        Catch ex As Exception
    '            Return False
    '        End Try
    '        Return False
    '    End Function

   
End Class