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
        Sales_Monthly = 3
        stockOut = 4
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
            Case ReportType.Sales_Monthly
                MonthlySalesReport()
            Case ReportType.stockOut
                StockOut()
        End Select
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
        mySql &= "WHEN 3 THEN 'RETURNS' "
        mySql &= "WHEN 4 THEN 'STOCKOUT' "
        mySql &= "End AS DOCTYPE, "
        mySql &= "D.MOP, D.CODE, D.CUSTOMER, D.DOCDATE, D.NOVAT, D.VATRATE, D.VATTOTAL, D.DOCTOTAL, "
        mySql &= "D.STATUS, D.REMARKS,"
        mySql &= "DL.ITEMCODE, DL.DESCRIPTION, DL.QTY, DL.UNITPRICE, DL.SALEPRICE, DL.ROWTOTAL "
        mySql &= "FROM DOC D "
        mySql &= "INNER JOIN DOCLINES DL ON DL.DOCID = D.DOCID "
        mySql &= "WHERE D.DOCDATE = '" & str & "' AND D.STATUS <> 'V' and D.Code LIKE '%INV#%'"

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
        Dim dt As DateTime = Convert.ToDateTime(st.ToShortDateString)
        Dim dt1 As DateTime = Convert.ToDateTime(en.ToShortDateString)
        Dim format As String = "yyyy-MM-dd"
        Dim str As String = dt.ToString(format)
        Dim str1 As String = dt1.ToString(format)

        dsName = "dsSales"
        rptPath = "Report\rpt_SalesReport.rdlc"
        mySql = "SELECT D.DOCID, "
        mySql &= "CASE D.DOCTYPE "
        mySql &= "WHEN 0 THEN 'SALES' "
        mySql &= "WHEN 1 THEN 'SALES' "
        mySql &= "WHEN 3 THEN 'RETURNS' "
        mySql &= "WHEN 4 THEN 'STOCKOUT' "
        mySql &= "End AS DOCTYPE, "
        mySql &= "D.MOP, D.CODE, D.CUSTOMER, D.DOCDATE, D.NOVAT, D.VATRATE, D.VATTOTAL, D.DOCTOTAL, "
        mySql &= "D.STATUS, D.REMARKS,"
        mySql &= "DL.ITEMCODE, DL.DESCRIPTION, DL.QTY, DL.UNITPRICE, DL.SALEPRICE, DL.ROWTOTAL "
        mySql &= "FROM DOC D "
        mySql &= "INNER JOIN DOCLINES DL ON DL.DOCID = D.DOCID "
        mySql &= "WHERE D.DOCDATE Between '" & str & "' AND '" & str1 & "' AND D.STATUS <> 'V' and D.Code LIKE '%INV#%'"

        If DEV_MODE Then Console.WriteLine(mySql)
        Dim addParameter As New Dictionary(Of String, String)
        addParameter.Add("txtMonthOf", "DATE : " & monCal.SelectionStart.ToString("MMMM dd, yyyy"))
        addParameter.Add("branchName", "GSC")
        addParameter.Add("txtUsername", SystemUser.UserName)

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

    Private Sub InventoryReport()
        Dim mySql As String
        Dim dt As DateTime = Convert.ToDateTime(monCal.SelectionStart.ToShortDateString)
        Dim format As String = "yyyy-MM-dd"
        Dim str As String = dt.ToString(format)
        mySql = "SELECT "
        mySql &= vbCrLf & "	ITM.ITEMCODE, ITM.DESCRIPTION, ITM.CATEGORIES, ITM.SUBCAT,"
        mySql &= vbCrLf & " ITM.ONHAND AS ACTUAL,"
        mySql &= vbCrLf & "    ITM.ONHAND,CAST('ITM.ADD_TIME' AS DATE) as DocDate, "
        mySql &= vbCrLf & "(SELECT Sum(doclines.QTY) "
        mySql &= vbCrLf & "FROM doc Inner Join doclines ON doc.DOCID = doclines.DOCID "
        mySql &= vbCrLf & "where doclines.ITEMCODE=ITM.ITEMCODE and doc.DocDate='" & str & "' and doc.Code LIKE '%INV#%'"
        mySql &= vbCrLf & "group by doclines.ITEMCODE)AS Consume "
        mySql &= vbCrLf & "FROM ITEMMASTER ITM "
        mySql &= vbCrLf & "WHERE ITM.ONHAND <> 0"
        mySql &= vbCrLf & "GROUP BY "
        mySql &= vbCrLf & "	ITM.ITEMCODE, ITM.DESCRIPTION, ITM.CATEGORIES, ITM.SUBCAT, ITM.ONHAND, DocDate "

        Dim dic As New Dictionary(Of String, String)
        dic.Add("txtMonthOf", dt.ToLongDateString)
        dic.Add("branchName", "GSC")

        frmReport.ReportInit(mySql, "dsInventory", "Report\rpt_InventoryPOS.rdlc", dic)
        frmReport.Show()
    End Sub

    Private Sub StockOut()
        Dim mySql As String
        Dim st As Date = GetFirstDate(monCal.SelectionStart)
        Dim en As Date = GetLastDate(monCal.SelectionEnd)

        mySql = "Select D.CODE, D.CUSTOMER, DL.ITEMCODE, DL.DESCRIPTION, DL.QTY "
        mySql &= "From Doc D INNER JOIN DOCLINES DL ON DL.DOCID = D.DOCID "
        mySql &= "Where D.CODE LIKE '%STO#%' AND D.DOCDATE BETWEEN '" & st.ToShortDateString & "' AND '" & en.ToShortDateString & "' "

        Dim dic As New Dictionary(Of String, String)
        dic.Add("txtMonthOf", "FOR THE MONTH OF " + st.ToString("MMMM yyyy"))
        dic.Add("branchName", "GSC")
        dic.Add("txtUsername", SystemUser.UserName)
        dic.Add("txtStock", "StockOut")

        frmReport.ReportInit(mySql, "dsStockOut", "Report\rpt_StockOutReport.rdlc", dic)
        frmReport.Show()
    End Sub


    Private Sub qryDate_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class