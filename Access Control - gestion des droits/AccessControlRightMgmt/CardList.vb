Public Class CardList
    Private List As DataSet1.CardListDataTable
    Dim Caller As MainForm
    Public Sub New(ByRef t As DataSet1.CardListDataTable, ByRef Caller As MainForm)
        InitializeComponent()
        List = t
        Me.Caller = Caller
        Me.StartPosition = FormStartPosition.CenterParent
    End Sub
    Private Sub CardList_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.DataGridCardList.DataSource = List
        Me.DataGridCardList.RowHeadersVisible = False
        Me.DataGridCardList.AutoResizeColumns()
    End Sub

    Private Sub Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonOK.Click, ButtonCancel.Click
        If Me.ButtonCancel.Equals(sender) Then
            Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Else
            Me.DialogResult = Windows.Forms.DialogResult.OK
            Caller.TextBoxCardNo.Text = Me.DataGridCardList.CurrentRow.Cells(0).Value.ToString
        End If
        Me.Close()
    End Sub

    Private Sub DataGridCardList_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridCardList.CellDoubleClick
        Me.Button_Click(Me.ButtonOK, Nothing)
    End Sub
End Class