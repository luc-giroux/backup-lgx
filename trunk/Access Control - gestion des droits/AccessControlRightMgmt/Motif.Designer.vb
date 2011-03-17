<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Motif
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.RichTextBoxMotif = New System.Windows.Forms.RichTextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ButtonValiderMotif = New System.Windows.Forms.Button()
        Me.ButtonAnnulerMotif = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'RichTextBoxMotif
        '
        Me.RichTextBoxMotif.Location = New System.Drawing.Point(5, 31)
        Me.RichTextBoxMotif.MaxLength = 255
        Me.RichTextBoxMotif.Name = "RichTextBoxMotif"
        Me.RichTextBoxMotif.Size = New System.Drawing.Size(327, 53)
        Me.RichTextBoxMotif.TabIndex = 0
        Me.RichTextBoxMotif.Text = ""
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(2, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(269, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Merci de saisir un motif pour la désactivation du badge :"
        '
        'ButtonValiderMotif
        '
        Me.ButtonValiderMotif.Enabled = False
        Me.ButtonValiderMotif.Location = New System.Drawing.Point(99, 100)
        Me.ButtonValiderMotif.Name = "ButtonValiderMotif"
        Me.ButtonValiderMotif.Size = New System.Drawing.Size(62, 20)
        Me.ButtonValiderMotif.TabIndex = 2
        Me.ButtonValiderMotif.Text = "Valider"
        Me.ButtonValiderMotif.UseVisualStyleBackColor = True
        '
        'ButtonAnnulerMotif
        '
        Me.ButtonAnnulerMotif.Location = New System.Drawing.Point(178, 100)
        Me.ButtonAnnulerMotif.Name = "ButtonAnnulerMotif"
        Me.ButtonAnnulerMotif.Size = New System.Drawing.Size(62, 20)
        Me.ButtonAnnulerMotif.TabIndex = 3
        Me.ButtonAnnulerMotif.Text = "Annuler"
        Me.ButtonAnnulerMotif.UseVisualStyleBackColor = True
        '
        'Motif
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(340, 132)
        Me.ControlBox = False
        Me.Controls.Add(Me.ButtonAnnulerMotif)
        Me.Controls.Add(Me.ButtonValiderMotif)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.RichTextBoxMotif)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "Motif"
        Me.Text = "Motif"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents RichTextBoxMotif As System.Windows.Forms.RichTextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ButtonValiderMotif As System.Windows.Forms.Button
    Friend WithEvents ButtonAnnulerMotif As System.Windows.Forms.Button
End Class
