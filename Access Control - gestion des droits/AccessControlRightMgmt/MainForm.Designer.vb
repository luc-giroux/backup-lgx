<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MainForm
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainForm))
        Me.CardsBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.DataSet1 = New AccessControlRightMgmt.DataSet1()
        Me.CardsTableAdapter = New AccessControlRightMgmt.DataSet1TableAdapters.CardsTableAdapter()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.ComboBoxRecType = New System.Windows.Forms.ComboBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.MiFareCardNumber = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DisabledDataGridViewCheckBoxColumn = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonApply = New System.Windows.Forms.Button()
        Me.ButtonRemoveRight = New System.Windows.Forms.Button()
        Me.ButtonAddRight = New System.Windows.Forms.Button()
        Me.ListBoxAssignedRights = New System.Windows.Forms.ListBox()
        Me.ListBoxAvailableRights = New System.Windows.Forms.ListBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.TextBoxEnterprise = New System.Windows.Forms.TextBox()
        Me.PictureBoxPhoto = New System.Windows.Forms.PictureBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.TextBoxFirstname = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.TextBoxLastname = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TextBoxCardNo = New System.Windows.Forms.TextBox()
        Me.GroupBoxFind = New System.Windows.Forms.GroupBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.ButtonFind = New System.Windows.Forms.Button()
        Me.TextBoxFindLastname = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TextBoxFindCardNo = New System.Windows.Forms.TextBox()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.DataGridViewEvents = New System.Windows.Forms.DataGridView()
        Me.EventTextDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DateTimeDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DeviceNameDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.LastNameDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.FirstNameDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.EventsBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.DataGridView2 = New System.Windows.Forms.DataGridView()
        Me.ChangeDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ApplicationUserDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ChangeType = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ReasonDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.IndivActivationChangesBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.EventsTableAdapter = New AccessControlRightMgmt.DataSet1TableAdapters.EventsTableAdapter()
        Me.IndivActivationChangesTableAdapter = New AccessControlRightMgmt.DataSet1TableAdapters.IndivActivationChangesTableAdapter()
        Me.PictureBoxOnSite = New System.Windows.Forms.PictureBox()
        Me.LabelOnSite = New System.Windows.Forms.Label()
        Me.LabelOffSite = New System.Windows.Forms.Label()
        CType(Me.CardsBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataSet1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBoxPhoto, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBoxFind.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        CType(Me.DataGridViewEvents, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EventsBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage3.SuspendLayout()
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndivActivationChangesBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBoxOnSite, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'CardsBindingSource
        '
        Me.CardsBindingSource.DataMember = "Cards"
        Me.CardsBindingSource.DataSource = Me.DataSet1
        '
        'DataSet1
        '
        Me.DataSet1.DataSetName = "DataSet1"
        Me.DataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'CardsTableAdapter
        '
        Me.CardsTableAdapter.ClearBeforeFill = True
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Location = New System.Drawing.Point(2, 2)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(588, 520)
        Me.TabControl1.TabIndex = 0
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.LabelOffSite)
        Me.TabPage1.Controls.Add(Me.LabelOnSite)
        Me.TabPage1.Controls.Add(Me.PictureBoxOnSite)
        Me.TabPage1.Controls.Add(Me.Label11)
        Me.TabPage1.Controls.Add(Me.ComboBoxRecType)
        Me.TabPage1.Controls.Add(Me.Label10)
        Me.TabPage1.Controls.Add(Me.DataGridView1)
        Me.TabPage1.Controls.Add(Me.Label9)
        Me.TabPage1.Controls.Add(Me.Label8)
        Me.TabPage1.Controls.Add(Me.ButtonCancel)
        Me.TabPage1.Controls.Add(Me.ButtonApply)
        Me.TabPage1.Controls.Add(Me.ButtonRemoveRight)
        Me.TabPage1.Controls.Add(Me.ButtonAddRight)
        Me.TabPage1.Controls.Add(Me.ListBoxAssignedRights)
        Me.TabPage1.Controls.Add(Me.ListBoxAvailableRights)
        Me.TabPage1.Controls.Add(Me.Label7)
        Me.TabPage1.Controls.Add(Me.TextBoxEnterprise)
        Me.TabPage1.Controls.Add(Me.PictureBoxPhoto)
        Me.TabPage1.Controls.Add(Me.Label6)
        Me.TabPage1.Controls.Add(Me.TextBoxFirstname)
        Me.TabPage1.Controls.Add(Me.Label5)
        Me.TabPage1.Controls.Add(Me.TextBoxLastname)
        Me.TabPage1.Controls.Add(Me.Label4)
        Me.TabPage1.Controls.Add(Me.TextBoxCardNo)
        Me.TabPage1.Controls.Add(Me.GroupBoxFind)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(580, 494)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Individu"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(394, 340)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(60, 13)
        Me.Label11.TabIndex = 46
        Me.Label11.Text = "Rec Type :"
        '
        'ComboBoxRecType
        '
        Me.ComboBoxRecType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxRecType.Enabled = False
        Me.ComboBoxRecType.FormattingEnabled = True
        Me.ComboBoxRecType.Items.AddRange(New Object() {"PERMANENT", "TEMPORAIRE", "DISABLE"})
        Me.ComboBoxRecType.Location = New System.Drawing.Point(395, 359)
        Me.ComboBoxRecType.Name = "ComboBoxRecType"
        Me.ComboBoxRecType.Size = New System.Drawing.Size(175, 21)
        Me.ComboBoxRecType.TabIndex = 45
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(22, 340)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(145, 13)
        Me.Label10.TabIndex = 44
        Me.Label10.Text = "Cartes rattachées à l'individu:"
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.AutoGenerateColumns = False
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.MiFareCardNumber, Me.DisabledDataGridViewCheckBoxColumn})
        Me.DataGridView1.DataSource = Me.CardsBindingSource
        Me.DataGridView1.Location = New System.Drawing.Point(22, 359)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.Size = New System.Drawing.Size(309, 126)
        Me.DataGridView1.TabIndex = 43
        '
        'MiFareCardNumber
        '
        Me.MiFareCardNumber.DataPropertyName = "MiFareCardNumber"
        Me.MiFareCardNumber.HeaderText = "MiFareCardNumber"
        Me.MiFareCardNumber.MinimumWidth = 200
        Me.MiFareCardNumber.Name = "MiFareCardNumber"
        Me.MiFareCardNumber.ReadOnly = True
        Me.MiFareCardNumber.Width = 200
        '
        'DisabledDataGridViewCheckBoxColumn
        '
        Me.DisabledDataGridViewCheckBoxColumn.DataPropertyName = "Disabled"
        Me.DisabledDataGridViewCheckBoxColumn.HeaderText = "Disabled"
        Me.DisabledDataGridViewCheckBoxColumn.MinimumWidth = 60
        Me.DisabledDataGridViewCheckBoxColumn.Name = "DisabledDataGridViewCheckBoxColumn"
        Me.DisabledDataGridViewCheckBoxColumn.Width = 60
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(232, 209)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(91, 13)
        Me.Label9.TabIndex = 42
        Me.Label9.Text = "Groupes assignés"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(17, 209)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(102, 13)
        Me.Label8.TabIndex = 41
        Me.Label8.Text = "Groupes disponibles"
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Location = New System.Drawing.Point(414, 462)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(75, 23)
        Me.ButtonCancel.TabIndex = 40
        Me.ButtonCancel.Text = "Annuler"
        Me.ButtonCancel.UseVisualStyleBackColor = True
        '
        'ButtonApply
        '
        Me.ButtonApply.Location = New System.Drawing.Point(495, 462)
        Me.ButtonApply.Name = "ButtonApply"
        Me.ButtonApply.Size = New System.Drawing.Size(75, 23)
        Me.ButtonApply.TabIndex = 39
        Me.ButtonApply.Text = "Appliquer"
        Me.ButtonApply.UseVisualStyleBackColor = True
        '
        'ButtonRemoveRight
        '
        Me.ButtonRemoveRight.Location = New System.Drawing.Point(197, 275)
        Me.ButtonRemoveRight.Name = "ButtonRemoveRight"
        Me.ButtonRemoveRight.Size = New System.Drawing.Size(32, 23)
        Me.ButtonRemoveRight.TabIndex = 38
        Me.ButtonRemoveRight.Text = "<"
        Me.ButtonRemoveRight.UseVisualStyleBackColor = True
        '
        'ButtonAddRight
        '
        Me.ButtonAddRight.Location = New System.Drawing.Point(197, 246)
        Me.ButtonAddRight.Name = "ButtonAddRight"
        Me.ButtonAddRight.Size = New System.Drawing.Size(32, 23)
        Me.ButtonAddRight.TabIndex = 37
        Me.ButtonAddRight.Text = ">"
        Me.ButtonAddRight.UseVisualStyleBackColor = True
        '
        'ListBoxAssignedRights
        '
        Me.ListBoxAssignedRights.FormattingEnabled = True
        Me.ListBoxAssignedRights.Location = New System.Drawing.Point(235, 225)
        Me.ListBoxAssignedRights.Name = "ListBoxAssignedRights"
        Me.ListBoxAssignedRights.Size = New System.Drawing.Size(171, 95)
        Me.ListBoxAssignedRights.TabIndex = 36
        '
        'ListBoxAvailableRights
        '
        Me.ListBoxAvailableRights.FormattingEnabled = True
        Me.ListBoxAvailableRights.Location = New System.Drawing.Point(20, 225)
        Me.ListBoxAvailableRights.Name = "ListBoxAvailableRights"
        Me.ListBoxAvailableRights.Size = New System.Drawing.Size(171, 95)
        Me.ListBoxAvailableRights.TabIndex = 35
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(137, 97)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(54, 13)
        Me.Label7.TabIndex = 34
        Me.Label7.Text = "Entreprise"
        '
        'TextBoxEnterprise
        '
        Me.TextBoxEnterprise.Location = New System.Drawing.Point(140, 113)
        Me.TextBoxEnterprise.Name = "TextBoxEnterprise"
        Me.TextBoxEnterprise.ReadOnly = True
        Me.TextBoxEnterprise.Size = New System.Drawing.Size(100, 20)
        Me.TextBoxEnterprise.TabIndex = 33
        '
        'PictureBoxPhoto
        '
        Me.PictureBoxPhoto.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PictureBoxPhoto.Location = New System.Drawing.Point(425, 10)
        Me.PictureBoxPhoto.Name = "PictureBoxPhoto"
        Me.PictureBoxPhoto.Size = New System.Drawing.Size(145, 192)
        Me.PictureBoxPhoto.TabIndex = 32
        Me.PictureBoxPhoto.TabStop = False
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(137, 145)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(43, 13)
        Me.Label6.TabIndex = 31
        Me.Label6.Text = "Prénom"
        '
        'TextBoxFirstname
        '
        Me.TextBoxFirstname.Location = New System.Drawing.Point(140, 161)
        Me.TextBoxFirstname.Name = "TextBoxFirstname"
        Me.TextBoxFirstname.ReadOnly = True
        Me.TextBoxFirstname.Size = New System.Drawing.Size(100, 20)
        Me.TextBoxFirstname.TabIndex = 30
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(17, 145)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(76, 13)
        Me.Label5.TabIndex = 29
        Me.Label5.Text = "Nom de famille"
        '
        'TextBoxLastname
        '
        Me.TextBoxLastname.Location = New System.Drawing.Point(20, 161)
        Me.TextBoxLastname.Name = "TextBoxLastname"
        Me.TextBoxLastname.ReadOnly = True
        Me.TextBoxLastname.Size = New System.Drawing.Size(100, 20)
        Me.TextBoxLastname.TabIndex = 28
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(19, 97)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(66, 13)
        Me.Label4.TabIndex = 27
        Me.Label4.Text = "No. de carte"
        '
        'TextBoxCardNo
        '
        Me.TextBoxCardNo.Location = New System.Drawing.Point(20, 113)
        Me.TextBoxCardNo.Name = "TextBoxCardNo"
        Me.TextBoxCardNo.ReadOnly = True
        Me.TextBoxCardNo.Size = New System.Drawing.Size(100, 20)
        Me.TextBoxCardNo.TabIndex = 26
        '
        'GroupBoxFind
        '
        Me.GroupBoxFind.Controls.Add(Me.Label3)
        Me.GroupBoxFind.Controls.Add(Me.Label2)
        Me.GroupBoxFind.Controls.Add(Me.ButtonFind)
        Me.GroupBoxFind.Controls.Add(Me.TextBoxFindLastname)
        Me.GroupBoxFind.Controls.Add(Me.Label1)
        Me.GroupBoxFind.Controls.Add(Me.TextBoxFindCardNo)
        Me.GroupBoxFind.Location = New System.Drawing.Point(10, 10)
        Me.GroupBoxFind.Name = "GroupBoxFind"
        Me.GroupBoxFind.Size = New System.Drawing.Size(409, 69)
        Me.GroupBoxFind.TabIndex = 25
        Me.GroupBoxFind.TabStop = False
        Me.GroupBoxFind.Text = "Recherche"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(127, 35)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(19, 13)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "ou"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(167, 16)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(76, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Nom de famille"
        '
        'ButtonFind
        '
        Me.ButtonFind.Location = New System.Drawing.Point(315, 29)
        Me.ButtonFind.Name = "ButtonFind"
        Me.ButtonFind.Size = New System.Drawing.Size(75, 23)
        Me.ButtonFind.TabIndex = 3
        Me.ButtonFind.Text = "Rechercher"
        Me.ButtonFind.UseVisualStyleBackColor = True
        '
        'TextBoxFindLastname
        '
        Me.TextBoxFindLastname.Location = New System.Drawing.Point(170, 32)
        Me.TextBoxFindLastname.Name = "TextBoxFindLastname"
        Me.TextBoxFindLastname.Size = New System.Drawing.Size(127, 20)
        Me.TextBoxFindLastname.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(7, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(66, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "No. de carte"
        '
        'TextBoxFindCardNo
        '
        Me.TextBoxFindCardNo.Location = New System.Drawing.Point(10, 32)
        Me.TextBoxFindCardNo.MaxLength = 5
        Me.TextBoxFindCardNo.Name = "TextBoxFindCardNo"
        Me.TextBoxFindCardNo.Size = New System.Drawing.Size(100, 20)
        Me.TextBoxFindCardNo.TabIndex = 1
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.Label12)
        Me.TabPage2.Controls.Add(Me.DataGridViewEvents)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(580, 494)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Evènements"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(11, 10)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(187, 13)
        Me.Label12.TabIndex = 1
        Me.Label12.Text = "100 derniers évènements de l'individu:"
        '
        'DataGridViewEvents
        '
        Me.DataGridViewEvents.AllowUserToAddRows = False
        Me.DataGridViewEvents.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.DataGridViewEvents.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.DataGridViewEvents.AutoGenerateColumns = False
        Me.DataGridViewEvents.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridViewEvents.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.EventTextDataGridViewTextBoxColumn, Me.DateTimeDataGridViewTextBoxColumn, Me.DeviceNameDataGridViewTextBoxColumn, Me.LastNameDataGridViewTextBoxColumn, Me.FirstNameDataGridViewTextBoxColumn})
        Me.DataGridViewEvents.DataSource = Me.EventsBindingSource
        Me.DataGridViewEvents.Location = New System.Drawing.Point(14, 31)
        Me.DataGridViewEvents.Name = "DataGridViewEvents"
        Me.DataGridViewEvents.ReadOnly = True
        Me.DataGridViewEvents.Size = New System.Drawing.Size(553, 450)
        Me.DataGridViewEvents.TabIndex = 0
        '
        'EventTextDataGridViewTextBoxColumn
        '
        Me.EventTextDataGridViewTextBoxColumn.DataPropertyName = "EventText"
        Me.EventTextDataGridViewTextBoxColumn.HeaderText = "EventText"
        Me.EventTextDataGridViewTextBoxColumn.Name = "EventTextDataGridViewTextBoxColumn"
        Me.EventTextDataGridViewTextBoxColumn.ReadOnly = True
        '
        'DateTimeDataGridViewTextBoxColumn
        '
        Me.DateTimeDataGridViewTextBoxColumn.DataPropertyName = "DateTime"
        Me.DateTimeDataGridViewTextBoxColumn.HeaderText = "DateTime"
        Me.DateTimeDataGridViewTextBoxColumn.Name = "DateTimeDataGridViewTextBoxColumn"
        Me.DateTimeDataGridViewTextBoxColumn.ReadOnly = True
        '
        'DeviceNameDataGridViewTextBoxColumn
        '
        Me.DeviceNameDataGridViewTextBoxColumn.DataPropertyName = "DeviceName"
        Me.DeviceNameDataGridViewTextBoxColumn.HeaderText = "Porte"
        Me.DeviceNameDataGridViewTextBoxColumn.Name = "DeviceNameDataGridViewTextBoxColumn"
        Me.DeviceNameDataGridViewTextBoxColumn.ReadOnly = True
        '
        'LastNameDataGridViewTextBoxColumn
        '
        Me.LastNameDataGridViewTextBoxColumn.DataPropertyName = "LastName"
        Me.LastNameDataGridViewTextBoxColumn.HeaderText = "LastName"
        Me.LastNameDataGridViewTextBoxColumn.Name = "LastNameDataGridViewTextBoxColumn"
        Me.LastNameDataGridViewTextBoxColumn.ReadOnly = True
        '
        'FirstNameDataGridViewTextBoxColumn
        '
        Me.FirstNameDataGridViewTextBoxColumn.DataPropertyName = "FirstName"
        Me.FirstNameDataGridViewTextBoxColumn.HeaderText = "FirstName"
        Me.FirstNameDataGridViewTextBoxColumn.Name = "FirstNameDataGridViewTextBoxColumn"
        Me.FirstNameDataGridViewTextBoxColumn.ReadOnly = True
        Me.FirstNameDataGridViewTextBoxColumn.Width = 90
        '
        'EventsBindingSource
        '
        Me.EventsBindingSource.DataMember = "Events"
        Me.EventsBindingSource.DataSource = Me.DataSet1
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.DataGridView2)
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Size = New System.Drawing.Size(580, 494)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "Suivi Modifications"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'DataGridView2
        '
        Me.DataGridView2.AllowUserToAddRows = False
        Me.DataGridView2.AllowUserToDeleteRows = False
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.DataGridView2.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle2
        Me.DataGridView2.AutoGenerateColumns = False
        Me.DataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView2.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ChangeDate, Me.ApplicationUserDataGridViewTextBoxColumn, Me.ChangeType, Me.ReasonDataGridViewTextBoxColumn})
        Me.DataGridView2.DataSource = Me.IndivActivationChangesBindingSource
        Me.DataGridView2.Location = New System.Drawing.Point(17, 33)
        Me.DataGridView2.Name = "DataGridView2"
        Me.DataGridView2.ReadOnly = True
        Me.DataGridView2.Size = New System.Drawing.Size(547, 447)
        Me.DataGridView2.TabIndex = 0
        '
        'ChangeDate
        '
        Me.ChangeDate.DataPropertyName = "ChangeDate"
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter
        Me.ChangeDate.DefaultCellStyle = DataGridViewCellStyle3
        Me.ChangeDate.HeaderText = "Date"
        Me.ChangeDate.Name = "ChangeDate"
        Me.ChangeDate.ReadOnly = True
        '
        'ApplicationUserDataGridViewTextBoxColumn
        '
        Me.ApplicationUserDataGridViewTextBoxColumn.DataPropertyName = "ApplicationUser"
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter
        Me.ApplicationUserDataGridViewTextBoxColumn.DefaultCellStyle = DataGridViewCellStyle4
        Me.ApplicationUserDataGridViewTextBoxColumn.HeaderText = "Modifié Par"
        Me.ApplicationUserDataGridViewTextBoxColumn.Name = "ApplicationUserDataGridViewTextBoxColumn"
        Me.ApplicationUserDataGridViewTextBoxColumn.ReadOnly = True
        Me.ApplicationUserDataGridViewTextBoxColumn.Width = 85
        '
        'ChangeType
        '
        Me.ChangeType.DataPropertyName = "ChangeType"
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.ChangeType.DefaultCellStyle = DataGridViewCellStyle5
        Me.ChangeType.HeaderText = "Modification"
        Me.ChangeType.Name = "ChangeType"
        Me.ChangeType.ReadOnly = True
        '
        'ReasonDataGridViewTextBoxColumn
        '
        Me.ReasonDataGridViewTextBoxColumn.DataPropertyName = "Reason"
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter
        Me.ReasonDataGridViewTextBoxColumn.DefaultCellStyle = DataGridViewCellStyle6
        Me.ReasonDataGridViewTextBoxColumn.HeaderText = "Motif"
        Me.ReasonDataGridViewTextBoxColumn.Name = "ReasonDataGridViewTextBoxColumn"
        Me.ReasonDataGridViewTextBoxColumn.ReadOnly = True
        Me.ReasonDataGridViewTextBoxColumn.Width = 210
        '
        'IndivActivationChangesBindingSource
        '
        Me.IndivActivationChangesBindingSource.DataMember = "IndivActivationChanges"
        Me.IndivActivationChangesBindingSource.DataSource = Me.DataSet1
        '
        'EventsTableAdapter
        '
        Me.EventsTableAdapter.ClearBeforeFill = True
        '
        'IndivActivationChangesTableAdapter
        '
        Me.IndivActivationChangesTableAdapter.ClearBeforeFill = True
        '
        'PictureBoxOnSite
        '
        Me.PictureBoxOnSite.Image = Global.AccessControlRightMgmt.My.Resources.Resources.out
        Me.PictureBoxOnSite.Location = New System.Drawing.Point(317, 120)
        Me.PictureBoxOnSite.Name = "PictureBoxOnSite"
        Me.PictureBoxOnSite.Size = New System.Drawing.Size(35, 38)
        Me.PictureBoxOnSite.TabIndex = 47
        Me.PictureBoxOnSite.TabStop = False
        Me.PictureBoxOnSite.Visible = False
        '
        'LabelOnSite
        '
        Me.LabelOnSite.AutoSize = True
        Me.LabelOnSite.Location = New System.Drawing.Point(273, 167)
        Me.LabelOnSite.Name = "LabelOnSite"
        Me.LabelOnSite.Size = New System.Drawing.Size(115, 13)
        Me.LabelOnSite.TabIndex = 48
        Me.LabelOnSite.Text = "L'individu est sur le site"
        Me.LabelOnSite.Visible = False
        '
        'LabelOffSite
        '
        Me.LabelOffSite.AutoSize = True
        Me.LabelOffSite.Location = New System.Drawing.Point(263, 167)
        Me.LabelOffSite.Name = "LabelOffSite"
        Me.LabelOffSite.Size = New System.Drawing.Size(143, 13)
        Me.LabelOffSite.TabIndex = 49
        Me.LabelOffSite.Text = "L'individu n'est pas sur le site"
        Me.LabelOffSite.Visible = False
        '
        'MainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(591, 523)
        Me.Controls.Add(Me.TabControl1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(599, 550)
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(599, 550)
        Me.Name = "MainForm"
        Me.Text = "Gestion des droits d'accès - TAC Koniambo"
        CType(Me.CardsBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataSet1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBoxPhoto, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBoxFind.ResumeLayout(False)
        Me.GroupBoxFind.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        CType(Me.DataGridViewEvents, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EventsBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage3.ResumeLayout(False)
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndivActivationChangesBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBoxOnSite, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents DataSet1 As AccessControlRightMgmt.DataSet1
    Friend WithEvents CardsBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents CardsTableAdapter As AccessControlRightMgmt.DataSet1TableAdapters.CardsTableAdapter
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents ComboBoxRecType As System.Windows.Forms.ComboBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents MiFareCardNumber As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DisabledDataGridViewCheckBoxColumn As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents ButtonApply As System.Windows.Forms.Button
    Friend WithEvents ButtonRemoveRight As System.Windows.Forms.Button
    Friend WithEvents ButtonAddRight As System.Windows.Forms.Button
    Friend WithEvents ListBoxAssignedRights As System.Windows.Forms.ListBox
    Friend WithEvents ListBoxAvailableRights As System.Windows.Forms.ListBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents TextBoxEnterprise As System.Windows.Forms.TextBox
    Friend WithEvents PictureBoxPhoto As System.Windows.Forms.PictureBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents TextBoxFirstname As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents TextBoxLastname As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents TextBoxCardNo As System.Windows.Forms.TextBox
    Friend WithEvents GroupBoxFind As System.Windows.Forms.GroupBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents ButtonFind As System.Windows.Forms.Button
    Friend WithEvents TextBoxFindLastname As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TextBoxFindCardNo As System.Windows.Forms.TextBox
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents DataGridViewEvents As System.Windows.Forms.DataGridView
    Friend WithEvents EventsBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents EventsTableAdapter As AccessControlRightMgmt.DataSet1TableAdapters.EventsTableAdapter
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents DataGridView2 As System.Windows.Forms.DataGridView
    Friend WithEvents IndivActivationChangesBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents IndivActivationChangesTableAdapter As AccessControlRightMgmt.DataSet1TableAdapters.IndivActivationChangesTableAdapter
    Friend WithEvents EventTextDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DateTimeDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DeviceNameDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents LastNameDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents FirstNameDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents IndivActivationChangesDateDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ChangeDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ApplicationUserDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ChangeType As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ReasonDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PictureBoxOnSite As System.Windows.Forms.PictureBox
    Friend WithEvents LabelOnSite As System.Windows.Forms.Label
    Friend WithEvents LabelOffSite As System.Windows.Forms.Label

End Class
