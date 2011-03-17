<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Discipline
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
        Me.components = New System.ComponentModel.Container()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.DisciplineIDDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DescriptionDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DisciplineBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.PAF_DATADataSet = New TimesheetApproval.PAF_DATADataSet()
        Me.DisciplineTableAdapter = New TimesheetApproval.PAF_DATADataSetTableAdapters.DisciplineTableAdapter()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DisciplineBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PAF_DATADataSet, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.AutoGenerateColumns = False
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DisciplineIDDataGridViewTextBoxColumn, Me.DescriptionDataGridViewTextBoxColumn})
        Me.DataGridView1.DataSource = Me.DisciplineBindingSource
        Me.DataGridView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridView1.Location = New System.Drawing.Point(0, 0)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.Size = New System.Drawing.Size(373, 415)
        Me.DataGridView1.TabIndex = 0
        '
        'DisciplineIDDataGridViewTextBoxColumn
        '
        Me.DisciplineIDDataGridViewTextBoxColumn.DataPropertyName = "DisciplineID"
        Me.DisciplineIDDataGridViewTextBoxColumn.HeaderText = "DisciplineID"
        Me.DisciplineIDDataGridViewTextBoxColumn.MinimumWidth = 80
        Me.DisciplineIDDataGridViewTextBoxColumn.Name = "DisciplineIDDataGridViewTextBoxColumn"
        Me.DisciplineIDDataGridViewTextBoxColumn.ReadOnly = True
        Me.DisciplineIDDataGridViewTextBoxColumn.Width = 80
        '
        'DescriptionDataGridViewTextBoxColumn
        '
        Me.DescriptionDataGridViewTextBoxColumn.DataPropertyName = "Description"
        Me.DescriptionDataGridViewTextBoxColumn.HeaderText = "Description"
        Me.DescriptionDataGridViewTextBoxColumn.MinimumWidth = 250
        Me.DescriptionDataGridViewTextBoxColumn.Name = "DescriptionDataGridViewTextBoxColumn"
        Me.DescriptionDataGridViewTextBoxColumn.ReadOnly = True
        Me.DescriptionDataGridViewTextBoxColumn.Width = 250
        '
        'DisciplineBindingSource
        '
        Me.DisciplineBindingSource.DataMember = "Discipline"
        Me.DisciplineBindingSource.DataSource = Me.PAF_DATADataSet
        '
        'PAF_DATADataSet
        '
        Me.PAF_DATADataSet.DataSetName = "PAF_DATADataSet"
        Me.PAF_DATADataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'DisciplineTableAdapter
        '
        Me.DisciplineTableAdapter.ClearBeforeFill = True
        '
        'Discipline
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(373, 415)
        Me.Controls.Add(Me.DataGridView1)
        Me.Name = "Discipline"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Disciplines"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DisciplineBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PAF_DATADataSet, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents PAF_DATADataSet As TimesheetApproval.PAF_DATADataSet
    Friend WithEvents DisciplineBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents DisciplineTableAdapter As TimesheetApproval.PAF_DATADataSetTableAdapters.DisciplineTableAdapter
    Friend WithEvents DisciplineIDDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DescriptionDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
