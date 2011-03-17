Public Class Motif
    Dim Caller As MainForm
    Public Sub New(ByRef Caller As MainForm)
        InitializeComponent()
        Me.Caller = Caller
        Me.StartPosition = FormStartPosition.CenterParent
    End Sub

    Private Sub RichTextBoxMotif_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RichTextBoxMotif.TextChanged
        If RichTextBoxMotif.Text.Length = 0 Then
            Me.ButtonValiderMotif.Enabled = False
        Else
            Me.ButtonValiderMotif.Enabled = True
        End If
    End Sub

    Private Sub Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonValiderMotif.Click, ButtonAnnulerMotif.Click
        If Me.ButtonAnnulerMotif.Equals(sender) Then
            Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Else
            Me.DialogResult = Windows.Forms.DialogResult.OK
            Caller.Motif = Me.RichTextBoxMotif.Text
        End If
        Me.Close()
    End Sub
End Class