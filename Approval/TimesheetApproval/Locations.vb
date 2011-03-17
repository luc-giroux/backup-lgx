Public Class Locations

    Private Sub Locations_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'PAF_DATADataSet.Location' table. You can move, or remove it, as needed.
        Me.LocationTableAdapter.Fill(Me.PAF_DATADataSet.Location)

    End Sub
End Class