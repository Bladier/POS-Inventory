Imports System.IO
Public Class qryDate
    Friend accessType As String = String.Empty
    Friend IsExportInventory As Boolean = False
    Dim appPath As String = Application.StartupPath
    Dim verified_url As String

    Enum ReportType As Integer
        Inventory = 0
        DailyCashCount = 1

    End Enum

    Friend FormType As ReportType = ReportType.Inventory
    Private Sub Generate()
        Select Case FormType
            Case ReportType.Inventory

            Case ReportType.DailyCashCount

        End Select
    End Sub

    Private Sub Inventory()

        Dim mysql As String
        If monCal.SelectionRange.Start.ToShortDateString = CurrentDate.ToShortDateString Then
            mysql = "Select * From ItemMaster"
        Else
            mysql = "Select D.CurrentDate, ITM.ItemCOde, ITM.Description, ITM.SalePrice, "
            mysql &= "DL.Ending as Onhand "
            mysql &= "From tblDaily D "
            mysql &= "Inner Join tblDailyLines as DL on D.ID = DL.DailyID "
            mysql &= "Inner Join ItemMaster ITM on ITM.ItemID = DL.ItemID "
            mysql &= "Where D.CurrentDate = '" & monCal.SelectionStart.ToShortDateString & "'"
        End If

        Dim dic As New Dictionary(Of String, String)
        dic.Add("txtMonthOf", "As of " & monCal.SelectionRange.Start.ToShortDateString)
        ' dic.Add("branchName", branchName)
        dic.Add("txtUsername", SystemUser.USERNAME)

        frmReport.ReportInit(mysql, "dsInventory", "Reports\rpt_Inventory.rdlc", dic)
        frmReport.Show()
    End Sub

    Private Sub btnGenerate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenerate.Click
        Generate()
    End Sub

    Private Sub SalesReport()
        Dim mySql As String, dsName As String, rptPath As String
        dsName = "dsSales"
        rptPath = "Reports\rpt_SalesReport.rdlc"
        mySql = "SELECT D.DOCID, "
        mySql &= "WHERE D.DOCDATE = '" & monCal.SelectionStart.ToShortDateString & "' AND D.STATUS <> 'V' AND D.MOP <> 'E' AND D.MOP <>'S' ORDER BY DL.DOCID ASC"

        Dim addParameter As New Dictionary(Of String, String)
        addParameter.Add("txtMonthOf", "DATE : " & monCal.SelectionStart.ToString("MMMM dd, yyyy"))
        ' addParameter.Add("branchName", branchName)
        addParameter.Add("txtUsername", SystemUser.USERNAME)

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
        ' addParameter.Add("branchName", branchName)
        addParameter.Add("txtUsername", SystemUser.USERNAME)

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