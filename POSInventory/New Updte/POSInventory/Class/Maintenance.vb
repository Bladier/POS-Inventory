Public Class Maintenance
    Dim maintable As String = "tblMaintenance"
    Dim ds As DataSet
    Dim mysql As String = String.Empty

#Region "Property and variables"
    Private _ID As Integer
    Public Property ID() As Integer
        Get
            Return _ID
        End Get
        Set(ByVal value As Integer)
            _ID = value
        End Set
    End Property

    Private _Mkey As String
    Public Property Mkey() As String
        Get
            Return _Mkey
        End Get
        Set(ByVal value As String)
            _Mkey = value
        End Set
    End Property

    Private _MValue As String
    Public Property MValue() As String
        Get
            Return _MValue
        End Get
        Set(ByVal value As String)
            _MValue = value
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
#End Region

#Region "Procedures and functions"
    Friend Sub loadIMD(ByVal id As Integer)
        mysql = "SELECT * FROM " & maintable & " WHERE ID =" & id
        ds = LoadSQL(mysql, maintable)

        If ds.Tables(0).Rows.Count = 0 Then Exit Sub

        loadbyrow(ds.Tables(0).Rows(0))
    End Sub

    Friend Sub loadbyrow(ByVal dr As DataRow)
        With dr
            _ID = .Item("ID")
            _Mkey = .Item("opt_keys")
            _MValue = IIf(IsDBNull(.Item("opt_values")), "", .Item("opt_values"))
            _Remarks = IIf(IsDBNull(.Item("Remarks")), "", .Item("Remarks"))

        End With
        Console.WriteLine("Maintenance key:" & _Mkey)
    End Sub

    Friend Sub saveMaintenance()
        mysql = "SELECT * FROM " & maintable & " WHERE opt_keys = '" & _Mkey & "'"
        ds = LoadSQL(mysql, maintable)

        If ds.Tables(0).Rows.Count = 0 Then
            Dim dsnewrow As DataRow
            dsnewrow = ds.Tables(0).NewRow
            With dsnewrow
                .Item("opt_keys") = _Mkey
                .Item("opt_values") = _MValue
                .Item("Remarks") = _Remarks
            End With
            ds.Tables(0).Rows.Add(dsnewrow)
            database.SaveEntry(ds)
        Else
            With ds.Tables(maintable).Rows(0)
                .Item("opt_keys") = _Mkey
                .Item("opt_values") = _MValue
                .Item("Remarks") = _Remarks
            End With
            database.SaveEntry(ds, False)
        End If
    End Sub

    Public Sub Load_maitenance()
        mysql = String.Format("SELECT * FROM " & maintable & " WHERE opt_keys = '{0}'", _Mkey)
        ds = New DataSet
        ds = LoadSQL(mysql)

        If ds.Tables(0).Rows.Count <> 1 Then
            Console.WriteLine("Failed to load maintenance " & _Mkey)
            Exit Sub
        End If

        loadbyrow(ds.Tables(0).Rows(0))
    End Sub
#End Region
End Class
