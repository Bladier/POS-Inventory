Imports DeathCodez.Security

''' <summary>
''' NOTE: Privilege PDuNxp8S9q0= means SUPER USER
''' </summary>
''' <remarks>NOTE: Privilege PDuNxp8S9q0= means SUPER USER</remarks>
Public Class Sys_user

    Private fillData As String = "tbluser"
    Private mySql As String = String.Empty

#Region "Properties"
    Private _userID As Integer
    Public Property UserID() As Integer
        Get
            Return _userID
        End Get
        Set(ByVal value As Integer)
            _userID = value
        End Set
    End Property


    Private _firstname As String
    Public Property firstname() As String
        Get
            Return _firstname
        End Get
        Set(ByVal value As String)
            _firstname = value
        End Set
    End Property

    Private _Middlename As String
    Public Property Middlename() As String
        Get
            Return _Middlename
        End Get
        Set(ByVal value As String)
            _Middlename = value
        End Set
    End Property

    Private _Lastname As String
    Public Property Lastname() As String
        Get
            Return _Lastname
        End Get
        Set(ByVal value As String)
            _Lastname = value
        End Set
    End Property

    Private _userName As String
    Public Property UserName() As String
        Get
            Return _userName
        End Get
        Set(ByVal value As String)
            _userName = value
        End Set
    End Property

    Private _password As String
    Public Property Password() As String
        Get
            Return _password
        End Get
        Set(ByVal value As String)
            _password = value
        End Set
    End Property



    Private _userRole As String
    Public Property userRole() As String
        Get
            Return _userRole
        End Get
        Set(ByVal value As String)
            _userRole = value
        End Set
    End Property

    Private _UserStatus As Boolean
    Public Property UserStatus() As Boolean
        Get
            Return _UserStatus
        End Get
        Set(ByVal value As Boolean)
            _UserStatus = value
        End Set
    End Property


    Private _lastLogin As Date
    Public Property LastLogin() As Date
        Get
            Return _lastLogin
        End Get
        Set(ByVal value As Date)
            _lastLogin = value
        End Set
    End Property
#End Region

#Region "Procedures and Functions"
    Public Sub CreateAdministrator(Optional ByVal pass As String = "9999")
        mySql = "SELECT * FROM " & fillData
        Dim user As String, fname, lname, Role As String, ds As DataSet
        user = "Admin" : fname = "Admin" : lname = "Admin" : Role = "Admin"

        mySql &= String.Format(" WHERE Uname = '{0}'", user, EncryptString(pass))

        Console.WriteLine("Create SQL: " & mySql)

        ds = LoadSQL(mySql, fillData)
        If ds.Tables(fillData).Rows.Count > 0 Then Exit Sub

        Dim dsNewRow As DataRow
        dsNewRow = ds.Tables(fillData).NewRow
        With dsNewRow
            .Item("Firstname") = fname
            .Item("Lastname") = lname
            .Item("Uname") = user
            .Item("Pword") = Encrypt(pass)
            .Item("userrole") = Role
            .Item("status") = 1
        End With
        ds.Tables(fillData).Rows.Add(dsNewRow)
        database.SaveEntry(ds, True)
    End Sub

    Public Sub LoadUserByRow(ByVal dr As DataRow)
        'On Error Resume Next

        With dr
            _userID = .Item("ID")
            _firstname = .Item("Firstname")
            If Not IsDBNull(.Item("Middlename")) Then _Middlename = .Item("Middlename")
            _Lastname = .Item("Lastname")
            _userName = .Item("UName")
            _password = .Item("Pword")
            _userRole = .Item("UserRole")
            _UserStatus = IIf(.Item("Status") = 1, True, False)
            If Not IsDBNull(.Item("LastLogin")) Then _lastLogin = .Item("LastLogin")
        End With

    End Sub

    Public Sub SaveUser(Optional ByVal isNew As Boolean = True, Optional ByVal NewPassword As String = "")
        mySql = "SELECT * FROM " & fillData
        If Not isNew Then mySql &= " WHERE ID = " & _userID

        Dim ds As DataSet = LoadSQL(mySql, fillData)
        If isNew Then
            Dim dsNewRow As DataRow
            dsNewRow = ds.Tables(fillData).NewRow
            With dsNewRow
                .Item("Firstname") = _firstname
                .Item("Middlename") = _Middlename
                .Item("lastname") = _Lastname
                .Item("Uname") = _userName
                .Item("Pword") = Encrypt(_password)
                .Item("UserRole") = _userRole
                .Item("STATUS") = 1
            End With
            ds.Tables(fillData).Rows.Add(dsNewRow)
        Else
            With ds.Tables(0).Rows(0)
                .Item("Firstname") = _firstname
                .Item("Middlename") = _Middlename
                .Item("lastname") = _Lastname
                .Item("uname") = _userName
                .Item("UserRole") = _userRole
                If NewPassword <> "" Then
                    .Item("Pword") = Encrypt(NewPassword)
                End If
                .Item("Status") = IIf(_UserStatus, 1, 0)
            End With
        End If

        database.SaveEntry(ds, isNew)
        Console.WriteLine(_userName & " saved.")
    End Sub

    Public Sub LoadUser(ByVal id As Integer)
        mySql = "SELECT * FROM " & fillData & " WHERE ID = " & id
        Dim ds As DataSet = LoadSQL(mySql)
        If ds.Tables(0).Rows.Count = 0 Then Exit Sub


        LoadUserByRow(ds.Tables(0).Rows(0))
        Console.WriteLine(String.Format("[ComputerUser] UserID {0} - {1} Loaded", _userID, _userName))
    End Sub

    Public Function LoginUser(ByVal user As String, ByVal password As String) As Boolean
        mySql = "SELECT ID, LCase (uname) FROM " & fillData
        mySql &= vbCrLf & String.Format(" WHERE LCase (Uname) = LCase ('{0}') AND Pword = '{1}' AND STATUS <> '0'", user, Encrypt(password))
        Dim ds As DataSet

        ds = LoadSQL(mySql)
        If ds.Tables(0).Rows.Count = 0 Then Return False

        LoadUser(ds.Tables(0).Rows(0).Item("ID"))

        Return True
    End Function

    Public Sub UpdateLogin()
        mySql = "SELECT * FROM " & fillData & " WHERE ID = " & _userID
        Dim ds As DataSet = LoadSQL(mySql, fillData)
        ds.Tables(0).Rows(0).Item("LastLogin") = CurrentDate
        database.SaveEntry(ds, False)
        Console.WriteLine("Login Saved")
    End Sub

    Public Sub ChangePassword()
        Dim mySql As String = "SELECT * FROM tbluser WHERE ID = " & SystemUser.USERID
        Dim ds As DataSet = LoadSQL(mySql, fillData)

        ds.Tables(fillData).Rows(0).Item("Pword") = Encrypt(_password)
        SaveEntry(ds, False)
    End Sub

    Public Sub DeleteUser(ByVal Status As Boolean)
        Dim mySql As String = "SELECT * FROM tbluser WHERE ID = " & _userID
        Dim ds As DataSet = LoadSQL(mySql, fillData)
        If Status = True Then
            ds.Tables(fillData).Rows(0).Item("STATUS") = "1"
        Else
            ds.Tables(fillData).Rows(0).Item("STATUS") = "0"
        End If
        SaveEntry(ds, False)
        ' End If
    End Sub

#End Region
End Class
