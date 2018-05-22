Imports System.IO
Public Class qryDate
    Friend accessType As String = String.Empty
    Friend IsExportInventory As Boolean = False
    Dim appPath As String = Application.StartupPath
    Dim verified_url As String

    Enum ReportType As Integer
        StockIn = 0
        Sales = 1
        Inventory = 2
    End Enum

    Friend FormType As ReportType = ReportType.StockIn
    Private Sub Generate()
        Select Case FormType
            Case ReportType.StockIn
                StockIn()
            Case ReportType.Sales
                SalesReport()
            Case ReportType.Inventory
                InventoryReport()
        End Select
    End Sub


    Private Sub InventoryReport()
        Dim mySql As String
        Dim SelectedDate As Date = monCal.SelectionStart.ToShortDateString

        mySql = "SELECT "
        mySql &= vbCrLf & "	ITM.ITEMCODE, ITM.DESCRIPTION, ITM.CATEGORIES, ITM.SUBCAT,"
        mySql &= vbCrLf & "	COALESCE(ITM.ONHAND - SUM(TBL.QTY),ITM.ONHAND) AS ACTUAL,"
        mySql &= vbCrLf & "    ITM.ONHAND,CAST('ITM.ADD_TIME' AS DATE) as DocDate "
        mySql &= vbCrLf & "FROM ("
        mySql &= vbCrLf & "SELECT "
        mySql &= vbCrLf & "    'IN' as TYPE, I.DOCDATE, IL.ITEMCODE, IL.QTY"
        mySql &= vbCrLf & "FROM INV I"
        mySql &= vbCrLf & "INNER JOIN INVLINES IL
        mySql &= vbCrLf & "ON I.DOCID = IL.DOCID"
        mySql &= vbCrLf & "WHERE I.DOCDATE > '" & SelectedDate & "'"
        mySql &= vbCrLf & "UNION"
        mySql &= vbCrLf & "SELECT "
        mySql &= vbCrLf & "    'SALES' AS TYPE, D.DOCDATE, DL.ITEMCODE, DL.QTY * -1"
        mySql &= vbCrLf & "FROM DOC D"
        mySql &= vbCrLf & "INNER JOIN DOCLINES DL"
        mySql &= vbCrLf & "ON D.DOCID = DL.DOCID"
        mySql &= vbCrLf & "WHERE D.DOCDATE > '" & SelectedDate & "'"
        mySql &= vbCrLf & ") TBL"
        mySql &= vbCrLf & "RIGHT JOIN ITEMMASTER ITM"
        mySql &= vbCrLf & "ON ITM.ITEMCODE = TBL.ITEMCODE"
        mySql &= vbCrLf & "WHERE ITM.ONHAND <> 0"
        mySql &= vbCrLf & "GROUP BY "
        mySql &= vbCrLf & "	ITM.ITEMCODE, ITM.DESCRIPTION, ITM.CATEGORIES, ITM.SUBCAT, ITM.ONHAND, DocDate "

        Dim dic As New Dictionary(Of String, String)
        dic.Add("txtMonthOf", SelectedDate.ToLongDateString)
        dic.Add("branchName", "GSC")

        frmReport.ReportInit(mySql, "dsInventory", "Report\rpt_InventoryPOS.rdlc", dic)
        frmReport.Show()
    End Sub

    Private Sub btnGenerate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenerate.Click
        Generate()
    End Sub

    Private Sub SalesReport()
        Dim mySql As String, dsName As String, rptPath As String
        Dim dt As DateTime = Convert.ToDateTime(monCal.SelectionStart.ToShortDateString)
        Dim format As String = "yyyy-MM-dd"
        Dim str As String = dt.ToString(format)
        dsName = "dsSales"
        rptPath = "Report\rpt_SalesReport.rdlc"
        mySql = "SELECT D.DOCID, "
        mySql &= "CASE D.DOCTYPE "
        mySql &= "WHEN 0 THEN 'SALES' "
        mySql &= "WHEN 1 THEN 'SALES' "
        mySql &= "WHEN 2 THEN 'RECALL' "
        mySql &= "WHEN 3 THEN 'RETURNS' "
        mySql &= "WHEN 4 THEN 'STOCKOUT' "
        mySql &= "End AS DOCTYPE, "
        mySql &= "D.MOP, D.CODE, D.CUSTOMER, D.DOCDATE, D.NOVAT, D.VATRATE, D.VATTOTAL, D.DOCTOTAL, "
        mySql &= "D.STATUS, D.REMARKS,"
        mySql &= "DL.ITEMCODE, DL.DESCRIPTION, DL.QTY, DL.UNITPRICE, DL.SALEPRICE, DL.ROWTOTAL "
        mySql &= "FROM DOC D "
        mySql &= "INNER JOIN DOCLINES DL ON DL.DOCID = D.DOCID "
        mySql &= "WHERE D.DOCDATE = '" & str & "' AND D.STATUS <> 'V'"

        If DEV_MODE Then Console.WriteLine(mySql)
        Dim addParameter As New Dictionary(Of String, String)
        addParameter.Add("txtMonthOf", "DATE : " & monCal.SelectionStart.ToString("MMMM dd, yyyy"))
        addParameter.Add("branchName", "GSC")
        addParameter.Add("txtUsername", SystemUser.UserName)

        frmReport.ReportInit(mySql, dsName, rptPath, addParameter)
        frmReport.Show()
    End Sub

    Private Sub MonthlySalesReport()
        Dim st As Date = GetFirstDate(monCal.SelectionStart)
        Dim en As Date = GetLastDate(monCal.SelectionEnd)
        Dim mySql As String, dsName As String, rptPath As String
        dsName = "dsSales"
        rptPath = "Reports\rpt_SalesReport.rdlc"
        mySql = "SELECT D.DOCID, "
        mySql &= "WHERE D.DOCDATE Between '" & st.ToShortDateString & "' AND '" & en.ToShortDateString & "' AND D.STATUS <> 'V'"

        If DEV_MODE Then Console.WriteLine(mySql)
        Dim addParameter As New Dictionary(Of String, String)
        addParameter.Add("txtMonthOf", "DATE : " & monCal.SelectionStart.ToString("MMMM dd, yyyy"))
        addParameter.Add("txtUsername", SystemUser.USERNAME)

        frmReport.ReportInit(mySql, dsName, rptPath, addParameter)
        frmReport.Show()
    End Sub

    Private Sub StockIn()
        Dim mySql As String, dsName As String, rptPath As String
        Dim dt As DateTime = Convert.ToDateTime(monCal.SelectionStart.ToShortDateString)
        Dim format As String = "yyyy-MM-dd"
        Dim str As String = dt.ToString(format)

        dsName = "dsStockIn"
        rptPath = "Report\rptStockIn.rdlc"
        mySql = "SELECT inv.DOCID,inv.DOCNUM,inv.DOCDATE,inv.DOCSTATUS, "
        mySql &= "invLines.Itemcode, invLines.DESCRIPTION, "
        mySql &= "invLines.RowTotal, invLines.qty,invLines.UOm "
        mySql &= "FROM inv Inner Join invlines ON inv.DOCID = invlines.DOCID "
        mySql &= "WHERE inv.DOCDATE = '" & str & "' AND inv.DOCSTATUS <> '0' ORDER BY inv.DOCID ASC"

        Dim addParameter As New Dictionary(Of String, String)
        addParameter.Add("txtMonthOf", "DATE : " & monCal.SelectionStart.ToString("MMMM dd, yyyy"))
        addParameter.Add("txtUsername", SystemUser.UserName)

        frmReport.ReportInit(mySql, dsName, rptPath, addParameter)
        frmReport.Show()
    End Sub

    '' STEP 4
    'Private Function NoFilter() As Boolean
    '    Select Case FormType
    '        Case ReportType.DailyCashCount
    '            Return True
    '    End Select

    '    Return False
    'End Function

    'Private Sub qryDate_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    '    'If NoFilter() Then
    '    '    cboReports.Visible = False
    '    'Else
    '    '    cboReports.Visible = True
    '    'End If
    'End Sub

    Private Sub qryDate_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class