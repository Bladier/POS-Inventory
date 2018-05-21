Public Class Inv

    Dim mysql As String, filldata = "inv", subtable = "invlines"

#Region "Properties and variables"

    Private _id As Integer
    Public Property ID() As Integer
        Get
            Return _id
        End Get
        Set(ByVal value As Integer)
            _id = value
        End Set
    End Property

    Private _DocNum As String
    Public Property DocNum() As String
        Get
            Return _DocNum
        End Get
        Set(ByVal value As String)
            _DocNum = value
        End Set
    End Property

    Private _DocDate As Date
    Public Property DocDate() As String
        Get
            Return _DocDate
        End Get
        Set(ByVal value As String)
            _DocDate = value
        End Set
    End Property

    Private _Partner As String
    Public Property Partner() As String
        Get
            Return _Partner
        End Get
        Set(ByVal value As String)
            _Partner = value
        End Set
    End Property

    Private _GrandTotal As Integer
    Public Property GrandTotal() As Integer
        Get
            Return _GrandTotal
        End Get
        Set(ByVal value As Integer)
            _GrandTotal = value
        End Set
    End Property

    Private _Remarks As String
    Public Property Remarks() As String
        Get
            Return _Remarks
        End Get
        Set(ByVal value As String)
            _Remarks = value
        End Set
    End Property

    Private _DocStatus As Boolean
    Public Property DocStatus() As Boolean
        Get
            Return _DocStatus
        End Get
        Set(ByVal value As Boolean)
            _DocStatus = value
        End Set
    End Property

    Private _InvLines As invCollection
    Public Property InvLines() As invCollection
        Get
            Return _InvLines
        End Get
        Set(ByVal value As invCollection)
            _InvLines = value
        End Set
    End Property

#End Region

#Region "Functions and Procedures"
    Public Sub LoadItem(ByVal id As Integer)
        Dim mySql As String = String.Format("SELECT * FROM " + filldata + " WHERE docid = {0}", id)
        Dim ds As DataSet = LoadSQL(mySql, filldata)

        If ds.Tables(0).Rows.Count <> 1 Then
            MsgBox("Failed to load Item", MsgBoxStyle.Critical)
            Exit Sub
        End If

        With ds.Tables(0).Rows(0)
            _id = .Item("docid")
            _DocNum = .Item("DocNum")
            _DocDate = .Item("DocDate")

            _Partner = .Item("Partner")
            _GrandTotal = .Item("Grandtotal")
            If Not IsDBNull(.Item("Remarks")) Then _Remarks = .Item("Remarks")
            _DocStatus = IIf(.Item("Docstatus") = 1, True, False)

        End With

        ' Load inventory
        mySql = String.Format("SELECT * FROM {0} WHERE docid = {1} ORDER BY InvID asc", subtable, _id)
        ds.Clear()
        ds = LoadSQL(mySql, subtable)

        _InvLines = New invCollection
        For Each dr As DataRow In ds.Tables(subtable).Rows
            Console.WriteLine(dr.Item("itemcode"))
            Dim tmplines As New invLines
            tmplines.Loadinv_row(dr)

            'Load 
            _InvLines.add(tmplines)
        Next
    End Sub

    Public Sub LoadByRow(ByVal dr1 As DataRow)
        Dim mySql As String, ds As New DataSet
        With dr1
            _id = .Item("docid")
            _DocNum = .Item("DocNum")
            _DocDate = .Item("DocDate")

            _Partner = .Item("Partner")
            _GrandTotal = .Item("Grandtotal")
            If Not IsDBNull(.Item("Remarks")) Then _Remarks = .Item("Remarks")
            _DocStatus = IIf(.Item("Docstatus") = 1, True, False)

        End With
        ' Load inventory
        mySql = String.Format("SELECT * FROM {0} WHERE docid = {1} ORDER BY InvID asc", subtable, _id)
        ds.Clear()
        ds = LoadSQL(mySql, subtable)

        _InvLines = New invCollection
        For Each dr As DataRow In ds.Tables(subtable).Rows
            Console.WriteLine(dr.Item("itemcode"))
            Dim tmplines As New invLines
            tmplines.Loadinv_row(dr)

            'Load 
            _InvLines.add(tmplines)
        Next
    End Sub
#End Region
End Class
