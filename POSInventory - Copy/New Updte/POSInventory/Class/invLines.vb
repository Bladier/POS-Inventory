Public Class invLines
    Dim mysql As String, filldata = "invlines"
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

    Private _DocId As Integer
    Public Property DocId() As Integer
        Get
            Return _DocId
        End Get
        Set(ByVal value As Integer)
            _DocId = value
        End Set
    End Property

    Private _Itemcode As String
    Public Property Itemcode() As String
        Get
            Return _Itemcode
        End Get
        Set(ByVal value As String)
            _Itemcode = value
        End Set
    End Property

    Private _desc As String
    Public Property desc() As String
        Get
            Return _desc
        End Get
        Set(ByVal value As String)
            _desc = value
        End Set
    End Property

    Private _qty As Integer
    Public Property qty() As Integer
        Get
            Return _qty
        End Get
        Set(ByVal value As Integer)
            _qty = value
        End Set
    End Property

    Private _UnitPrice As Double
    Public Property UnitPrice() As Double
        Get
            Return _UnitPrice
        End Get
        Set(ByVal value As Double)
            _UnitPrice = value
        End Set
    End Property

    Private _SalePrice As Double
    Public Property SalePrice() As Double
        Get
            Return _SalePrice
        End Get
        Set(ByVal value As Double)
            _SalePrice = value
        End Set
    End Property

    Private _RowTotal As Integer
    Public Property RowTotal() As Integer
        Get
            Return _RowTotal
        End Get
        Set(ByVal value As Integer)
            _RowTotal = value
        End Set
    End Property

    Private _UOM As String
    Public Property UOM() As String
        Get
            Return _UOM
        End Get
        Set(ByVal value As String)
            _UOM = value
        End Set
    End Property

    Private _remarks As String
    Public Property remarks() As String
        Get
            Return _remarks
        End Get
        Set(ByVal value As String)
            _remarks = value
        End Set
    End Property

#End Region


#Region "Functions and Procedures"
    Public Sub LoadItem(ByVal id As Integer)
        Dim mySql As String = String.Format("SELECT * FROM " + filldata + " WHERE docid = {0}", id)
        Dim ds As DataSet = LoadSQL(mySql, filldata)

        If ds.Tables(0).Rows.Count <> 1 Then
            MsgBox("Failed to load inventory", MsgBoxStyle.Critical)
            Exit Sub
        End If

        With ds.Tables(0).Rows(0)
            _id = .Item("InvID")
            _DocId = .Item("DocID")
            _Itemcode = .Item("Itemcode")
            _desc = .Item("Description")
            _qty = .Item("qty")
            _UnitPrice = .Item("UnitPrice")
            If Not IsDBNull(.Item("SalePrice")) Then _SalePrice = .Item("SalePrice")
            _RowTotal = .Item("RowTotal")
            _UOM = .Item("UOM")
            If Not IsDBNull(.Item("Remarks")) Then _remarks = .Item("Remarks")
        End With
    End Sub

    Public Sub Loadinv_row(ByVal dr As DataRow)
        With dr
            _id = .Item("InvID")
            _DocId = .Item("DocID")
            _Itemcode = .Item("Itemcode")
            _desc = .Item("Description")
            _qty = .Item("qty")
            _UnitPrice = .Item("UnitPrice")
            If Not IsDBNull(.Item("SalePrice")) Then _SalePrice = .Item("SalePrice")
            _RowTotal = .Item("RowTotal")
            _UOM = .Item("UOM")
            If Not IsDBNull(.Item("Remarks")) Then _remarks = .Item("Remarks")
        End With
    End Sub
#End Region
End Class
