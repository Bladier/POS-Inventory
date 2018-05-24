


Module mod_system
    ''' <summary>
    ''' This region declare the neccessary variable in this system.
    ''' </summary>
    ''' <remarks></remarks>
#Region "Global Variables"
    Dim frmCollection As New FormCollection()
    Public DEV_MODE As Boolean = False
    Public PROTOTYPE As Boolean = False

    Public CurrentDate As Date = Now
    Public SystemUser As New Sys_user
    Public UserIDX As Integer = SystemUser.UserID
    Public dailyID As Integer = 0

    Public InitialBal As Double = 0

#End Region

#Region "Store"
    Private storeDB As String = "tblDaily" 'declare storeDB as string and initialize by tblDaily.

    Friend Function OpenStore(ByVal ini As Double) As Boolean

        Dim mySql As String = "SELECT * FROM " & storeDB
        mySql &= String.Format(" WHERE currentDate = '{0}'", CurrentDate.ToString("yyyy-MM-dd"))
        Dim ds As DataSet = LoadSQL(mySql, storeDB)

        ' Do not allow previous date to OPEN if closed.
        If ds.Tables(storeDB).Rows.Count = 1 Then
            If ds.Tables(storeDB).Rows(0).Item("Status") = 0 Then
                MsgBox("You cannot select to open a previous date", MsgBoxStyle.Critical)
            Else
                MsgBox("Error in OPENING STORE", MsgBoxStyle.Critical)
            End If
            Return False
        End If

        Dim dsNewRow As DataRow
        dsNewRow = ds.Tables(storeDB).NewRow
        With dsNewRow
            .Item("CurrentDate") = CurrentDate
            .Item("InitialBal") = ini
            .Item("CashCount") = 0 'No CashCount on OPENING
            .Item("Status") = 1
            .Item("SystemInfo") = Now
            .Item("Openner") = UserIDX
        End With
        ds.Tables(storeDB).Rows.Add(dsNewRow)

        database.SaveEntry(ds)
        Console.WriteLine("Store is now OPEN!")

        Return True
    End Function

    Friend Function CheckifHasData() As Boolean

        Dim mysql As String = "SELECT * FROM ITEMMASTER WHERE ONHOLD <> 1 AND ITEMCODE <> 'SMP 00001' AND ITEMCODE <> 'SMM 00002'"
        Dim ds As DataSet = LoadSQL(mysql, "ITEMMASTER")

        If ds.Tables(0).Rows.Count = 0 Then MsgBox("You can't open store." & vbCrLf & "No INVENTORY.", MsgBoxStyle.Critical, "Error") : Return False
        Return True
    End Function

    ''' <summary>
    ''' This function select all data from tblDaily table.
    ''' </summary>
    ''' <returns>return ds after reading every transaction.</returns>
    ''' <remarks></remarks>
    Friend Function LoadLastOpening() As DataSet
        Dim mySql As String = "SELECT * FROM tblDaily ORDER BY ID DESC"
        Dim ds As DataSet = LoadSQL(mySql)

        Return ds
    End Function
    ''' <summary>
    ''' This method will load all data from storeDB.
    ''' all data will be show where status is = 1.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub LoadCurrentDate()
        Dim mySql As String = "SELECT * FROM " & storeDB
        mySql &= String.Format(" WHERE Status = 1")
        Dim ds As DataSet = LoadSQL(mySql)

        If ds.Tables(0).Rows.Count = 1 Then
            CurrentDate = ds.Tables(0).Rows(0).Item("CurrentDate")
            dailyID = ds.Tables(0).Rows(0).Item("ID")
            InitialBal = ds.Tables(0).Rows(0).Item("INITIALBAL")
            frmMain.dateSet = True
        Else
            frmMain.dateSet = False
        End If
    End Sub

    Public Function LoadLastIDNumberDaily() As Single
        Dim mySql As String = "SELECT * FROM TBLDAILY ORDER BY ID DESC"
        Dim ds As DataSet = LoadSQL(mySql)

        If ds.Tables(0).Rows.Count = 0 Then
            Return 0
        End If
        Return ds.Tables(0).Rows(0).Item("ID")
    End Function
#End Region

    ''' <summary>
    ''' This function has two arguments.
    ''' declaraton UseShellExecute as boolean and RedirectStandardOutput as boolean.
    ''' </summary>
    ''' <param name="app">app is the parameter that hold nonmodifiable value.</param>
    ''' <param name="args">args is the parameter that hold nonmodifiable value.</param>
    ''' <returns>return soutput after reading every transaction.</returns>
    ''' <remarks></remarks>
    Public Function CommandPrompt(ByVal app As String, ByVal args As String) As String
        Dim oProcess As New Process()
        Dim oStartInfo As New ProcessStartInfo(app, args)
        oStartInfo.UseShellExecute = False
        oStartInfo.RedirectStandardOutput = True
        oStartInfo.WindowStyle = ProcessWindowStyle.Hidden
        oStartInfo.CreateNoWindow = True
        oProcess.StartInfo = oStartInfo

        oProcess.Start()

        Dim sOutput As String
        Using oStreamReader As System.IO.StreamReader = oProcess.StandardOutput
            sOutput = oStreamReader.ReadToEnd()
        End Using

        Return sOutput
    End Function

    Friend Sub CreateEsk(ByVal url As String, ByVal data As Hashtable)
        If System.IO.File.Exists(url) Then System.IO.File.Delete(url)

        Dim fsEsk As New System.IO.FileStream(url, IO.FileMode.CreateNew)
        Dim refNum As String, transDate As String, branchCode As String, amount As Double, remarks As String
        Dim checkSum As String

        With data
            refNum = data(0) '0 - as RefNum
            transDate = data(1) 'transDate
            branchCode = data(2) 'branchCode
            amount = data(3) 'Amount
            remarks = data(4) 'Remarks
        End With
        checkSum = HashString(refNum & transDate & branchCode & amount & remarks)
        data.Add(5, checkSum) 'CheckSum

        Dim esk As New Runtime.Serialization.Formatters.Binary.BinaryFormatter
        esk.Serialize(fsEsk, data)
        fsEsk.Close()
    End Sub

    Friend Function LoadEsk(ByVal url) As Hashtable
        If Not System.IO.File.Exists(url) Then Return Nothing

        Dim fsEsk As New System.IO.FileStream(url, IO.FileMode.Open)
        Dim bf As New Runtime.Serialization.Formatters.Binary.BinaryFormatter

        Dim hashTable As New Hashtable
        Try
            hashTable = bf.Deserialize(fsEsk)
        Catch ex As Exception
            Console.WriteLine("It seems the file is being tampered.")
            Return Nothing
        End Try
        fsEsk.Close()

        Dim isValid As Boolean = False
        If hashTable(5) = security.HashString(hashTable(0) & hashTable(1) & hashTable(2) & hashTable(3) & hashTable(4)) Then
            isValid = True
        End If

        If isValid Then Return hashTable
        Return Nothing
    End Function

    ''' <summary>
    ''' Function use to input only numbers
    ''' </summary>
    ''' <param name="e">Keypress Event</param>
    ''' <remarks>Use the Keypress Event when calling this function</remarks>
    Friend Function DigitOnly(ByVal e As System.Windows.Forms.KeyPressEventArgs, Optional ByVal isWhole As Boolean = False)
        Console.WriteLine("char: " & e.KeyChar & " -" & Char.IsDigit(e.KeyChar))
        If e.KeyChar <> ControlChars.Back Then
            If isWhole Then
                e.Handled = Not (Char.IsDigit(e.KeyChar))
            Else
                e.Handled = Not (Char.IsDigit(e.KeyChar) Or e.KeyChar = ".")
            End If

        End If

        Return Not (Char.IsDigit(e.KeyChar))
    End Function

    ''' <summary>
    ''' this function check if the input is numeric or character.
    ''' </summary>
    ''' <param name="txt">txt here hold the numeric value.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function checkNumeric(ByVal txt As TextBox) As Boolean
        If IsNumeric(txt.Text) Then
            Return True
        End If

        Return False
    End Function

    Friend Function DreadKnight(ByVal str As String, Optional ByVal special As String = Nothing) As String
        str = str.Replace("'", "''")
        str = str.Replace("""", """""")

        If special <> Nothing Then
            str = str.Replace(special, "")
        End If

        Return str
    End Function

    ''' <summary>
    ''' Identify if the KeyPress is enter
    ''' </summary>
    ''' <param name="e">KeyPressEventArgs</param>
    ''' <returns>Boolean</returns>
    Friend Function isEnter(ByVal e As KeyPressEventArgs) As Boolean
        If Asc(e.KeyChar) = 13 Then
            Return True
        End If
        Return False
    End Function

    Friend Function GetCurrentAge(ByVal dob As Date) As Integer
        Dim age As Integer
        age = Today.Year - dob.Year
        If (dob > Today.AddYears(-age)) Then age -= 1
        Return age
    End Function

    ''' <summary>
    ''' Use to verify entry
    ''' </summary>
    ''' <param name="txtBox">TextBox of the Money</param>
    ''' <returns>Boolean</returns>
    Friend Function isMoney(ByVal txtBox As TextBox) As Boolean
        Dim isGood As Boolean = False

        If Double.TryParse(txtBox.Text, 0.0) Then
            isGood = True
        End If

        Return isGood
    End Function

    Friend Function GetFirstDate(ByVal curDate As Date) As Date
        Dim firstDay = DateSerial(curDate.Year, curDate.Month, 1)
        Return firstDay
    End Function

    Friend Function GetLastDate(ByVal curDate As Date) As Date
        Dim original As DateTime = curDate  ' The date you want to get the last day of the month for
        Dim lastOfMonth As DateTime = original.Date.AddDays(-(original.Day - 1)).AddMonths(1).AddDays(-1)

        Return lastOfMonth
    End Function
  
    ' HASHTABLE FUNCTIONS
    Public Function GetIDbyName(ByVal name As String, ByVal ht As Hashtable) As Integer
        For Each dt As DictionaryEntry In ht
            If dt.Value = name Then
                Return dt.Key
            End If
        Next

        Return 0
    End Function

    Public Function GetNameByID(ByVal id As Integer, ByVal ht As Hashtable) As String
        For Each dt As DictionaryEntry In ht
            If dt.Key = id Then
                Return dt.Value
            End If
        Next

        Return "Bl@deG@mer"
    End Function

    ''' <summary>
    ''' This method will separate the phone number.
    ''' </summary>
    ''' <param name="PhoneField"></param>
    ''' <param name="e"></param>
    ''' <param name="isPhone"></param>
    ''' <remarks></remarks>
    Friend Sub PhoneSeparator(ByVal PhoneField As TextBox, ByVal e As KeyPressEventArgs, Optional ByVal isPhone As Boolean = False)
        Dim charPos() As Integer = {}
        If PhoneField.Text = Nothing Then Return

        Select Case PhoneField.Text.Substring(0, 1)
            Case "0"
                charPos = {4, 8}
            Case "9"
                charPos = {3, 7} '922-797-7559
            Case "+"
                charPos = {3, 7, 11} '+63-919-797-7559
            Case "6"
                charPos = {2, 6, 10} '63-919-797-7559
        End Select
        If isPhone Then
            Select Case PhoneField.Text.Substring(0, 1)
                Case "0"
                    charPos = {3, 7}
                Case Else
                    charPos = {2, 6}
            End Select
        End If

        'For Each POS In charPos
        '    If PhoneField.TextLength = POS And Not e.KeyChar = vbBack Then
        '        PhoneField.Text &= "-"
        '        PhoneField.SelectionStart = POS + 1
        '    End If
        'Next
    End Sub

    Function UppercaseFirstLetter(ByVal val As String) As String
        ' Test for nothing or empty.
        If String.IsNullOrEmpty(val) Then
            Return val
        End If

        ' Convert to character array.
        Dim array() As Char = val.ToCharArray

        ' Uppercase first character.
        array(0) = Char.ToUpper(array(0))

        ' Return new string.
        Return New String(array)
    End Function

    Function Date_Calculation(ByVal EXPIRATE_DATE As Date) As Integer
        Dim ValidDate As Date = CDate(EXPIRATE_DATE)
        Dim date1 As New System.DateTime(ValidDate.Year, ValidDate.Month, ValidDate.Day)

        Dim Diff1 As System.TimeSpan = date1.Subtract(Now)

        Dim TotRemDays = (Int(Diff1.TotalDays))
        Return TotRemDays + 1
    End Function

    Public Sub CloseForms(ByVal frm As String)
        Dim formNames As New List(Of String)
        For Each Form In My.Application.OpenForms
            If Form.Name <> "frmMain" Or Not Form.name <> frm Then
                formNames.Add(Form.Name)
            End If
        Next
        For Each currentFormName As String In formNames
            Application.OpenForms(currentFormName).Close()
        Next
    End Sub


    Public Function GetOption(ByVal opt_keys)
        Dim mysql As String = "Select * from tblmaintenance where  opt_keys ='" + opt_keys + "'"
        Dim ds As DataSet = LoadSQL(mysql, "tblmaintenance")
        If ds.Tables(0).Rows.Count > 0 Then
            Return ds.Tables(0).Rows(0).Item("OPT_Values")
        End If

        Return ""
    End Function

    Public Sub UpdateOptions(ByVal opt_keys As String, ByVal val_ues As String, Optional ByVal remarks As String = "")
        Dim mysql As String = "Select * from tblmaintenance where  opt_keys ='" + opt_keys + "'"
        Dim ds As DataSet = LoadSQL(mysql, "tblmaintenance")
        If ds.Tables(0).Rows.Count = 0 Then
            Dim dsnewrow As DataRow
            dsnewrow = ds.Tables(0).NewRow
            dsnewrow.Item("Opt_Values") = val_ues
            If remarks <> "" Then dsnewrow.Item("remarks") = remarks
            ds.Tables(0).Rows.Add(dsnewrow)
            database.SaveEntry(ds)
            Exit Sub
        End If

        With ds.Tables(0).Rows(0)
            .Item("Opt_values") = val_ues
            If remarks <> "" Then .Item("remarks") = remarks
        End With
        database.SaveEntry(ds)
    End Sub

    Friend Function GetSales_Today() As Double
        Dim MySql As String
        Dim dt As DateTime = Convert.ToDateTime(CurrentDate.ToShortDateString)
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


    Public Sub closestore(ByVal cc As Double)
        Dim mysql As String = "SELECT * FROM " & storeDB & "  WHERE STATUS =1"
        Dim ds As DataSet = LoadSQL(mysql, storeDB)

        If ds.Tables(0).Rows.Count = 0 Then Exit Sub

        Dim Over_Short As Double
        Dim ini_sales As Double = 0
        ini_sales = GetSales_Today() + InitialBal

        If cc <> ini_sales Then
            Over_Short = Val(cc) - Val(ini_sales)
        End If

        With ds.Tables(0).Rows(0)
            .Item("CashCount") = cc
            .Item("Status") = 0
            .Item("Closer") = SystemUser.UserID
            .Item("Overage/Shortage") = Over_Short
        End With
        database.SaveEntry(ds, False)

        frmMain.dateSet = False
        InitialBal = 0

        MsgBox("Store successfully closer.", MsgBoxStyle.Information, "Closing")
    End Sub

#Region "Log Module"
    Const LOG_FILE As String = "syslog.txt"
    Private Sub CreateLog()
        Dim fsEsk As New System.IO.FileStream(LOG_FILE, IO.FileMode.CreateNew)
        fsEsk.Close()
    End Sub

    Friend Sub Log_Report(ByVal str As String)
        If Not System.IO.File.Exists(LOG_FILE) Then CreateLog()

        Dim recorded_log As String = _
            String.Format("[{0}] ", Now.ToString("MM/dd/yyyy HH:mm:ss")) & str

        Dim fs As New System.IO.FileStream(LOG_FILE, IO.FileMode.Append, IO.FileAccess.Write)
        Dim fw As New System.IO.StreamWriter(fs)
        fw.WriteLine(recorded_log)
        fw.Close()
        fs.Close()
        Console.WriteLine("Recorded")
    End Sub
#End Region
End Module
