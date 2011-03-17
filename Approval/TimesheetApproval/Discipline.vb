Public Class Discipline

    Private Sub Discipline_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'PAF_DATADataSet.Discipline' table. You can move, or remove it, as needed.
        Me.DisciplineTableAdapter.Fill(Me.PAF_DATADataSet.Discipline)

    End Sub
End Class