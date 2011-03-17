

Partial Public Class ApplicationDataSet
    Partial Class SpecialRightsDataTable

        Private Sub SpecialRightsDataTable_SpecialRightsRowChanging(ByVal sender As System.Object, ByVal e As SpecialRightsRowChangeEvent) Handles Me.SpecialRightsRowChanging

        End Sub

        Private Sub SpecialRightsDataTable_ColumnChanging(ByVal sender As System.Object, ByVal e As System.Data.DataColumnChangeEventArgs) Handles Me.ColumnChanging
            If (e.Column.ColumnName = Me.ReportFullRightsColumn.ColumnName) Then
                'Add user code here
            End If

        End Sub

    End Class

    Partial Class AutomationDataTable

    End Class

    Partial Class GenericListDataTable

    End Class

    Partial Class TimesheetDetailDataTable

        Private Sub TimesheetDetailDataTable_ColumnChanging(ByVal sender As System.Object, ByVal e As System.Data.DataColumnChangeEventArgs) Handles Me.ColumnChanging
            If (e.Column.ColumnName = Me.ApprovedDateColumn.ColumnName) Then
                'Add user code here
            End If

        End Sub

    End Class

    Partial Class PendingManHourDataTable

        Private Sub PendingManHourDataTable_ColumnChanging(ByVal sender As System.Object, ByVal e As System.Data.DataColumnChangeEventArgs) Handles Me.ColumnChanging
            If (e.Column.ColumnName = Me.Tot_OTColumn.ColumnName) Then
                'Add user code here
            End If

        End Sub

    End Class

End Class
