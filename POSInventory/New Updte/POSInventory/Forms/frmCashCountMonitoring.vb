Public Class frmCashCountMonitoring

    Private Sub frmCashCountMonitoring_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Cashcount1()
    End Sub

    Private Sub Cashcount()

        Dim dt As DateTime = Convert.ToDateTime(monCal.SelectionStart.ToShortDateString)
        Dim format As String = "yyyy-MM-dd"
        Dim str As String = dt.ToString(format)


        Dim mysql As String
        mysql = "SELECT tbldaily.ID,tbldaily.CURRENTDATE,"
        mysql &= "tbldaily.INITIALBAL,tbldaily.CASHCOUNT,tbldaily.STATUS,"
        mysql &= "tbldaily.REMARKS,tbldaily.SYSTEMINFO,tbldaily.`Overage/Shortage` as OVERSHORT,"
        mysql &= "Concat(op.firstname,' ' , op.middlename, ' ' ,op.lastname) as Opener,"
        mysql &= "concat(cl.firstname,' ' ,cl.middlename,' ' ,cl.lastname) as Closer "
        mysql &= "FROM tbldaily Inner Join tbluser op ON tbldaily.OPENNER = op.ID "
        mysql &= "Inner Join tbluser cl ON tbldaily.CLOSER = cl.ID "
        mysql &= "WHERE tbldaily.CURRENTDATE = '" & str & "'"

        Dim ds As DataSet = LoadSQL(mysql, "tbldaily")
        If ds.Tables(0).Rows.Count = 0 Then ListView1.Items.Clear() : Exit Sub

        ListView1.Items.Clear()
        For Each dr As DataRow In ds.Tables(0).Rows
            With dr
                Dim lv As ListViewItem = ListView1.Items.Add(.Item("ID"))
                lv.SubItems.Add(.Item("CurrentDate"))
                lv.SubItems.Add(.Item("INITIALBAL"))
                lv.SubItems.Add(.Item("Cashcount"))
                lv.SubItems.Add(.Item("Opener"))
                lv.SubItems.Add(.Item("Closer"))
                lv.SubItems.Add(.Item("OVERSHORT"))
                lv.SubItems.Add(.Item("Status"))
                lv.SubItems.Add(IIf(chkIsHasSales.Checked, GetSales_TodayV2(str), ""))

                lv.Tag = .Item("ID")
            End With
        Next
    End Sub

    Private Sub Cashcount1()

        Dim dt As DateTime = Convert.ToDateTime(monCal.SelectionStart.ToShortDateString)
        Dim format As String = "yyyy-MM-dd"
        Dim str As String = dt.ToString(format)


        Dim mysql As String
        mysql = "SELECT tbldaily.ID,tbldaily.CURRENTDATE,"
        mysql &= "tbldaily.INITIALBAL,tbldaily.CASHCOUNT,tbldaily.STATUS,"
        mysql &= "tbldaily.REMARKS,tbldaily.SYSTEMINFO,tbldaily.`Overage/Shortage` as OVERSHORT,"
        mysql &= "Concat(op.firstname,' ' ,op.lastname) as Opener,"
        mysql &= "concat(cl.firstname,' ' ,cl.lastname) as Closer "
        mysql &= "FROM tbldaily Inner Join tbluser op ON tbldaily.OPENNER = op.ID "
        mysql &= "Inner Join tbluser cl ON tbldaily.CLOSER = cl.ID "

        Dim ds As DataSet = LoadSQL(mysql, "tbldaily")
        If ds.Tables(0).Rows.Count = 0 Then ListView1.Items.Clear() : Exit Sub

        ListView1.Items.Clear()
        For Each dr As DataRow In ds.Tables(0).Rows
            With dr
                Dim lv As ListViewItem = ListView1.Items.Add(.Item("ID"))
                lv.SubItems.Add(.Item("CurrentDate"))
                lv.SubItems.Add(.Item("INITIALBAL"))
                lv.SubItems.Add(.Item("Cashcount"))
                lv.SubItems.Add(.Item("Opener"))
                lv.SubItems.Add(.Item("Closer"))
                lv.SubItems.Add(.Item("OVERSHORT"))
                lv.SubItems.Add(.Item("Status"))
                lv.SubItems.Add(IIf(chkIsHasSales.Checked, GetSales_TodayV2(str), ""))

                lv.Tag = .Item("ID")
            End With
        Next
    End Sub

    Friend Function GetSales_TodayV2(ByVal tmpdate As Date) As Double
        Dim MySql As String
        Dim dt As DateTime = Convert.ToDateTime(tmpdate.ToShortDateString)
        Dim format As String = "yyyy-MM-dd"
        Dim str As String = dt.ToString(format)
        Dim ds As DataSet
        Dim tmpTotal As Double = 0

        MySql = "SELECT D.DOCID, "
        MySql &= "CASE D.DOCTYPE "
        MySql &= "WHEN 0 THEN 'SALES' "
        MySql &= "WHEN 1 THEN 'SALES' "
        MySql &= "WHEN 2 THEN 'RECALL' "
        MySql &= "WHEN 3 THEN 'RETURNS' "
        MySql &= "WHEN 4 THEN 'STOCKOUT' "
        MySql &= "End AS DOCTYPE, "
        MySql &= "D.MOP, D.CODE, D.CUSTOMER, D.DOCDATE, D.NOVAT, D.VATRATE, D.VATTOTAL, D.DOCTOTAL, "
        MySql &= "D.STATUS, D.REMARKS,"
        MySql &= "DL.ITEMCODE, DL.DESCRIPTION, DL.QTY, DL.UNITPRICE, DL.SALEPRICE, DL.ROWTOTAL "
        MySql &= "FROM DOC D "
        MySql &= "INNER JOIN DOCLINES DL ON DL.DOCID = D.DOCID "
        MySql &= "WHERE D.DOCDATE = '" & str & "' AND D.STATUS <> 'V'"

        ds = LoadSQL(MySql, "DOC")

        If ds.Tables(0).Rows.Count = 0 Then Return 0.0
        For Each dr As DataRow In ds.Tables(0).Rows
            tmpTotal += dr.Item("RowTotal")
        Next
        Return tmpTotal
    End Function

    Private Sub btnGenerate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenerate.Click
        Cashcount()
    End Sub
End Class