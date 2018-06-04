Public Class frmMonthlySales

    Private Sub btnGenerate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenerate.Click
        MonthlySalesReport()
    End Sub

    Private Sub MonthlySalesReport()
        Dim st As Date = DateTimePicker1.Text
        Dim en As Date = DateTimePicker2.Text
        Dim mySql As String, dsName As String, rptPath As String
        Dim dt As DateTime = Convert.ToDateTime(st.ToShortDateString)
        Dim dt1 As DateTime = Convert.ToDateTime(en.ToShortDateString)
        Dim format As String = "yyyy-MM-dd"
        Dim str As String = dt.ToString(format)
        Dim str1 As String = dt1.ToString(format)

        dsName = "dsMontlysales"
        rptPath = "Report\monthlySales.rdlc"
        mySql = "SELECT doc.DOCDATE,"
        mySql &= "Sum(doclines.ROWTOTAL) as Grand_GrandTotal, doc.STATUS ,doc.Code  "
        mySql &= "FROM doc Inner Join doclines ON doc.DOCID = doclines.DOCID "
        mySql &= "WHERE doc.DOCDATE Between '" & str & "' AND '" & str1 & "' and doc.STATUS <> 'V' and doc.Code LIKE '%INV#%'"
        mySql &= "Group by MONTH(doc.DOCDATE)"

        If DEV_MODE Then Console.WriteLine(mySql)
        Dim addParameter As New Dictionary(Of String, String)
        addParameter.Add("txtUsername", SystemUser.UserName)
        addParameter.Add("txtStore", IIf(GetOption("StoreName") = "", "Store not initialize", GetOption("StoreName")))

        frmReport.ReportInit(mySql, dsName, rptPath, addParameter)
        frmReport.Show()
    End Sub
End Class