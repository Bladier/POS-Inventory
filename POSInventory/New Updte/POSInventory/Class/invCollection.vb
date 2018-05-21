Public Class invCollection
    Inherits System.Collections.CollectionBase

    Public ReadOnly Property Item(ByVal index As Integer) As invLines
        Get
            Return CType(List.Item(index), invLines)
        End Get
    End Property

    Public Sub add(ByVal IDCard As invLines)
        List.Add(IDCard)
    End Sub

    Public Sub remove(ByVal index As Integer)
        If index > Count - 1 Or index < 0 Then
            System.Windows.Forms.MessageBox.Show("Index not valid!")
        Else
            List.RemoveAt(index)
        End If
    End Sub
End Class
