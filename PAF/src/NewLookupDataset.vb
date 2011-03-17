Partial Class NewLookupDataset
    Partial Class LocationDataTable

    End Class

    Partial Class EmployeeDataTable

        Private Sub EmployeeDataTable_ColumnChanging(ByVal sender As System.Object, ByVal e As System.Data.DataColumnChangeEventArgs) Handles Me.ColumnChanging
            If (e.Column.ColumnName = Me.DisciplineCodeColumn.ColumnName) Then
                'Add user code here
            End If

        End Sub

    End Class

End Class
