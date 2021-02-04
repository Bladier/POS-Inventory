Public Class frmSettings

    Dim GetMaintenance As New Maintenance

    Private Sub frmSettings_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        loadmaintenance()
    End Sub


    Private Sub loadmaintenance()

        Dim mysql As String = "SELECT * FROM TBLMAINTENANCE ORDER by ID ASC"
        Dim ds As DataSet = LoadSQL(mysql, "TBLMAINTENANCE")

        If ds.Tables(0).Rows.Count = 0 Then Exit Sub

        LvMaintenance.Items.Clear()
        For Each dr In ds.Tables(0).Rows
            GetMaintenance.loadbyrow(dr)

            addMaintenance(GetMaintenance)
        Next
    End Sub


    Private Sub addMaintenance(ByVal m As Maintenance)
        If m.Mkey = "" Then Exit Sub

        Dim lv As ListViewItem = LvMaintenance.Items.Add(m.Mkey)
        lv.SubItems.Add(m.MValue)

        lv.Tag = m.ID
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If btnSave.Text = "&Save" Then
            saveM()
        Else
            UpdateM()
        End If
    End Sub

    Private Sub saveM()
        If Not isValid() Then Exit Sub

        Dim msg As DialogResult = MsgBox("Do you want to save this to maintenance?", MsgBoxStyle.YesNo, "Maintenance")
        If msg = vbNo Then Exit Sub

        With GetMaintenance
            .Mkey = txtMKey.Text
            .MValue = txtMValue.Text
            .Remarks = txtRemakrs.Text
            .saveMaintenance()
        End With

        clear()
        loadmaintenance()
        MsgBox("Successfully saved.", MsgBoxStyle.Information)
    End Sub


    Private Sub UpdateM()
        If Not isValid() Then Exit Sub

        Dim msg As DialogResult = MsgBox("Do you want to Update this to maintenance?", MsgBoxStyle.YesNo, "Maintenance")
        If msg = vbNo Then Exit Sub

        With GetMaintenance
            .Mkey = txtMKey.Text
            .MValue = txtMValue.Text
            .Remarks = txtRemakrs.Text
            .saveMaintenance()
        End With

        clear()
        loadmaintenance()
        MsgBox("Successfully updated.", MsgBoxStyle.Information)
        btnSave.Text = "&Save"
    End Sub

    Private Sub clear()
        txtMKey.Text = "" : txtMValue.Text = "" : txtRemakrs.Text = "" : txtMKey.ReadOnly = False
    End Sub

    Private Function isValid() As Boolean
        If txtMKey.Text = "" Then txtMKey.Focus() : Return False
        Return True
    End Function

    Private Sub LvMaintenance_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LvMaintenance.DoubleClick
        If LvMaintenance.SelectedItems.Count = 0 Then Exit Sub

        GetMaintenance.loadIMD(LvMaintenance.FocusedItem.Tag)

        txtMKey.Text = GetMaintenance.Mkey
        txtMValue.Text = GetMaintenance.MValue
        txtRemakrs.Text = GetMaintenance.Remarks
        txtMKey.ReadOnly = True
        btnSave.Text = "&Update"
    End Sub
End Class