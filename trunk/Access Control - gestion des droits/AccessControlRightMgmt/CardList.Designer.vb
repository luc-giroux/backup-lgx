<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CardList
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
        Me.ButtonOK = New System.Windows.Forms.Button
        Me.ButtonCancel = New System.Windows.Forms.Button
        Me.TextBoxExplanation = New System.Windows.Forms.TextBox
        Me.DataGridCardList = New System.Windows.Forms.DataGridView
        CType(Me.DataGridCardList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ButtonOK
        '
        Me.ButtonOK.Location = New System.Drawing.Point(342, 222)
        Me.ButtonOK.Name = "ButtonOK"
        Me.ButtonOK.Size = New System.Drawing.Size(75, 23)
        Me.ButtonOK.TabIndex = 0
        Me.ButtonOK.Text = "Accepter"
        Me.ButtonOK.UseVisualStyleBackColor = True
        '
        'ButtonCancel
        '
        Me.ButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.ButtonCancel.Location = New System.Drawing.Point(261, 222)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(75, 23)
        Me.ButtonCancel.TabIndex = 1
        Me.ButtonCancel.Text = "Annuler"
        Me.ButtonCancel.UseVisualStyleBackColor = True
        '
        'TextBoxExplanation
        '
        Me.TextBoxExplanation.BackColor = System.Drawing.SystemColors.Control
        Me.TextBoxExplanation.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBoxExplanation.Location = New System.Drawing.Point(13, 12)
        Me.TextBoxExplanation.Multiline = True
        Me.TextBoxExplanation.Name = "TextBoxExplanation"
        Me.TextBoxExplanation.Size = New System.Drawing.Size(404, 61)
        Me.TextBoxExplanation.TabIndex = 3
        Me.TextBoxExplanation.Text = "Plusieurs cartes correspondent à votre critère de recherche. Veuillez choisir une" & _
            " carte dans la liste ci-dessous où canceller pour effectuer une autre recherche." & _
            ""
        '
        'DataGridCardList
        '
        Me.DataGridCardList.AllowUserToAddRows = False
        Me.DataGridCardList.AllowUserToDeleteRows = False
        Me.DataGridCardList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridCardList.Location = New System.Drawing.Point(13, 62)
        Me.DataGridCardList.MultiSelect = False
        Me.DataGridCardList.Name = "DataGridCardList"
        Me.DataGridCardList.ReadOnly = True
        Me.DataGridCardList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataGridCardList.ShowCellErrors = False
        Me.DataGridCardList.ShowCellToolTips = False
        Me.DataGridCardList.ShowEditingIcon = False
        Me.DataGridCardList.Size = New System.Drawing.Size(404, 138)
        Me.DataGridCardList.TabIndex = 4
        '
        'CardList
        '
        Me.AcceptButton = Me.ButtonOK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.ButtonCancel
        Me.ClientSize = New System.Drawing.Size(431, 257)
        Me.Controls.Add(Me.DataGridCardList)
        Me.Controls.Add(Me.TextBoxExplanation)
        Me.Controls.Add(Me.ButtonCancel)
        Me.Controls.Add(Me.ButtonOK)
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(439, 284)
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(439, 284)
        Me.Name = "CardList"
        Me.ShowInTaskbar = False
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.Text = "Sélection"
        CType(Me.DataGridCardList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ButtonOK As System.Windows.Forms.Button
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents TextBoxExplanation As System.Windows.Forms.TextBox
    Friend WithEvents DataGridCardList As System.Windows.Forms.DataGridView
End Class
