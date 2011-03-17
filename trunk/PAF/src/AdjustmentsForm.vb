Public Class AdjustmentsForm
    Inherits System.Windows.Forms.Form


    Private EmployeeLists As EmployeeListsClass
    Friend WithEvents EmployeeFormButton As System.Windows.Forms.Button
    Friend WithEvents ContractComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents DisciplineAdjComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents YearTableAdapter As AdjustmentsDatasetTableAdapters.YearTableAdapter
    Friend WithEvents WeekTableAdapter As AdjustmentsDatasetTableAdapters.WeekTableAdapter
    Friend WithEvents CTRCodeTableAdapter As AdjustmentsDatasetTableAdapters.CTRCodeTableAdapter
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents CTRCode As System.Windows.Forms.Label
    Friend WithEvents CTRCodeComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents NewLookupDataset As PAF.NewLookupDataset
    Friend WithEvents DisciplineBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents DisciplineTableAdapter As PAF.NewLookupDatasetTableAdapters.DisciplineTableAdapter


    Private PrivilegedSQLConnection As SqlClient.SqlConnection

    Private Class EmployeeListsClass
        Private MyList As Collections.SortedList
        Public Class EmployeeItem
            Private l_EmployeeID As Integer
            Private l_EmployeeName As String
            Public ReadOnly Property EmployeeID() As Integer
                Get
                    Return l_EmployeeID
                End Get
            End Property
            Public ReadOnly Property EmployeeName() As String
                Get
                    Return l_EmployeeName
                End Get
            End Property
            Public Sub New(ByVal pEmployeeID As Integer, ByVal p_EmployeeName As String)
                l_EmployeeID = pEmployeeID
                l_EmployeeName = p_EmployeeName
            End Sub
        End Class
        Public Class EmployeeList
            Inherits CollectionBase
            Default Public Property Item(ByVal Index As Integer) As EmployeeItem
                Get
                    Return CType(List(Index), EmployeeItem)
                End Get
                Set(ByVal Value As EmployeeItem)
                    List(Index) = Value
                End Set
            End Property
            Public Function Add(ByVal value As EmployeeItem) As Integer
                Return List.Add(value)
            End Function
            Public Function IndexOf(ByVal p_EmployeeID As Integer) As Integer
                Dim Emp As EmployeeItem
                Dim i As Integer = 0
                If IsNothing(p_EmployeeID) Then
                    Return -1
                End If
                For Each Emp In List
                    If Emp.EmployeeID = p_EmployeeID Then
                        Return i
                    End If
                    i += 1
                Next
                Return -1
            End Function 'IndexOf
            Public Function EmployeeIDOfIndex(ByVal p_Index As Integer) As Integer
                Dim i As Integer = 0
                If p_Index = -1 Then
                    Return Nothing
                End If
                Return CType(List.Item(p_Index), EmployeeItem).EmployeeID
            End Function 'EmployeeIDOfIndex
        End Class
        Default Public ReadOnly Property Item(ByVal p_CompanyID As String) As EmployeeList
            Get
                If IsNothing(p_CompanyID) Then
                    Return New EmployeeList
                End If
                If MyList.ContainsKey(p_CompanyID) Then
                    Return CType(MyList.Item(p_CompanyID), EmployeeList)
                Else ' we need to build it
                    Dim rows() As DataRow = MyLookupDataset.Employee.Select("Companyid = '" & p_CompanyID & "'", "LastName, FirstName")
                    Dim NewEmployeeList As New EmployeeList
                    Dim row As NewLookupDataset.EmployeeRow
                    For Each row In rows
                        NewEmployeeList.Add(New EmployeeItem(row.EmployeeID, row.FullName))
                    Next
                    MyList.Add(p_CompanyID, NewEmployeeList)
                    Return NewEmployeeList
                End If
            End Get
        End Property
        Public Sub New()
            MyList = New Collections.SortedList
        End Sub
    End Class
    Private Sub InitializeDataset()

        EmployeeLists = New EmployeeListsClass

        Me.SqlSelectCommand = New System.Data.SqlClient.SqlCommand
        Me.SqlInsertCommand = New System.Data.SqlClient.SqlCommand
        Me.SqlUpdateCommand = New System.Data.SqlClient.SqlCommand
        Me.SqlDeleteCommand = New System.Data.SqlClient.SqlCommand
        Me.AdjustmentsSqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter
        Me.AdjustmentsDataset = New PAF.AdjustmentsDataset
        CType(Me.AdjustmentsDataset, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'SqlSelectCommand
        '
        Me.SqlSelectCommand.CommandText = "SELECT EntryID, CompanyID, EmployeeID, PostedDate, " & _
                                                 "Year, Week, RatePayCurrencyID, RatePayLocation, " & _
                                                 "RateAssignCurrencyID, RateAssignLocation, LabourCost, " & _
                                                 "OtherOfficeSpace, OtherOfficeExpenses, " & _
                                                 "OtherIT, OtherITCore, Adjustment, " & _
                                                 "AdjustmentShortComment, AdjustmentLongComment, " & _
                                                 "AdjustmentDate, AdjustmentUser, Contract, Discipline, CTRCode FROM CostsAdjustments"
        Me.SqlSelectCommand.Connection = PrivilegedSQLConnection
        '
        'SqlInsertCommand
        '
        Me.SqlInsertCommand.CommandType = CommandType.StoredProcedure
        Me.SqlInsertCommand.Connection = PrivilegedSQLConnection
        Me.SqlInsertCommand.CommandText = _
            "CostsAdjustmentsInsert"
        Me.SqlInsertCommand.Parameters.Add( _
            New System.Data.SqlClient.SqlParameter("@CompanyID", _
                                                    System.Data.SqlDbType.VarChar, 3, "CompanyID"))
        Me.SqlInsertCommand.Parameters.Add( _
            New System.Data.SqlClient.SqlParameter("@EmployeeID", _
                                                    System.Data.SqlDbType.Int, 4, "EmployeeID"))
        Me.SqlInsertCommand.Parameters.Add( _
            New System.Data.SqlClient.SqlParameter("@PostedDate", _
                                                    System.Data.SqlDbType.DateTime, 8, "PostedDate"))
        Me.SqlInsertCommand.Parameters.Add( _
            New System.Data.SqlClient.SqlParameter("@Year", _
                                                    System.Data.SqlDbType.Int, 4, "Year"))
        Me.SqlInsertCommand.Parameters.Add( _
           New System.Data.SqlClient.SqlParameter("@Week", _
                                                   System.Data.SqlDbType.Int, 2, "Week"))
        Me.SqlInsertCommand.Parameters.Add( _
            New System.Data.SqlClient.SqlParameter("@RatePayCurrencyID", _
                                                    System.Data.SqlDbType.VarChar, 4, "RatePayCurrencyID"))
        Me.SqlInsertCommand.Parameters.Add( _
            New System.Data.SqlClient.SqlParameter("@RatePayLocation", _
                                                    System.Data.SqlDbType.VarChar, 3, "RatePayLocation"))
        Me.SqlInsertCommand.Parameters.Add( _
            New System.Data.SqlClient.SqlParameter("@RateAssignCurrencyID", _
                                                    System.Data.SqlDbType.VarChar, 4, "RateAssignCurrencyID"))
        Me.SqlInsertCommand.Parameters.Add( _
            New System.Data.SqlClient.SqlParameter("@RateAssignLocation", _
                                                    System.Data.SqlDbType.VarChar, 3, "RateAssignLocation"))
        Me.SqlInsertCommand.Parameters.Add( _
            New System.Data.SqlClient.SqlParameter("@LabourCost", _
                                                    System.Data.SqlDbType.Decimal, 9, _
                                                    System.Data.ParameterDirection.Input, False, _
                                                    CType(18, Byte), CType(2, Byte), _
                                                    "LabourCost", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand.Parameters.Add( _
            New System.Data.SqlClient.SqlParameter("@OtherOfficeSpace", _
                                                    System.Data.SqlDbType.Decimal, 9, _
                                                    System.Data.ParameterDirection.Input, True, _
                                                    CType(18, Byte), CType(2, Byte), _
                                                    "OtherOfficeSpace", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand.Parameters.Add( _
            New System.Data.SqlClient.SqlParameter("@OtherOfficeExpenses", _
                                                    System.Data.SqlDbType.Decimal, 9, _
                                                    System.Data.ParameterDirection.Input, True, _
                                                    CType(18, Byte), CType(2, Byte), _
                                                    "OtherOfficeExpenses", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand.Parameters.Add( _
            New System.Data.SqlClient.SqlParameter("@OtherIT", _
                                                    System.Data.SqlDbType.Decimal, 9, _
                                                    System.Data.ParameterDirection.Input, True, _
                                                    CType(18, Byte), CType(2, Byte), _
                                                    "OtherIT", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand.Parameters.Add( _
            New System.Data.SqlClient.SqlParameter("@OtherITCore", _
                                                    System.Data.SqlDbType.Decimal, 9, _
                                                    System.Data.ParameterDirection.Input, True, _
                                                    CType(18, Byte), CType(2, Byte), _
                                                    "OtherITCore", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand.Parameters.Add( _
            New System.Data.SqlClient.SqlParameter("@AdjustmentShortComment", _
                                                    System.Data.SqlDbType.VarChar, 256, _
                                                    "AdjustmentShortComment"))
        Me.SqlInsertCommand.Parameters.Add( _
            New System.Data.SqlClient.SqlParameter("@AdjustmentLongComment", _
                                                    System.Data.SqlDbType.VarChar, 2048, _
                                                    "AdjustmentLongComment"))
        Me.SqlInsertCommand.Parameters.Add( _
            New System.Data.SqlClient.SqlParameter("@Contract", _
                                                    System.Data.SqlDbType.VarChar, 10, _
                                                    "Contract"))
        Me.SqlInsertCommand.Parameters.Add( _
        New System.Data.SqlClient.SqlParameter("@Discipline", _
                                                System.Data.SqlDbType.VarChar, 4, _
                                                "Discipline"))
        Me.SqlInsertCommand.Parameters.Add( _
            New System.Data.SqlClient.SqlParameter("@CTRCode", _
                                                    System.Data.SqlDbType.VarChar, 10, _
                                                    "CTRCode"))

        '
        'SqlUpdateCommand
        '
        Me.SqlUpdateCommand.CommandType = CommandType.StoredProcedure
        Me.SqlUpdateCommand.Connection = PrivilegedSQLConnection
        Me.SqlUpdateCommand.CommandText = _
            "CostsAdjustmentsUpdate"
        Me.SqlUpdateCommand.Parameters.Add( _
            New System.Data.SqlClient.SqlParameter("@EntryID", _
                                                    System.Data.SqlDbType.Int, 4, "EntryID"))
        Me.SqlUpdateCommand.Parameters.Add( _
            New System.Data.SqlClient.SqlParameter("@CompanyID", _
                                                    System.Data.SqlDbType.VarChar, 3, "CompanyID"))
        Me.SqlUpdateCommand.Parameters.Add( _
            New System.Data.SqlClient.SqlParameter("@EmployeeID", _
                                                    System.Data.SqlDbType.Int, 4, "EmployeeID"))
        Me.SqlUpdateCommand.Parameters.Add( _
            New System.Data.SqlClient.SqlParameter("@PostedDate", _
                                                    System.Data.SqlDbType.DateTime, 8, "PostedDate"))
        Me.SqlUpdateCommand.Parameters.Add( _
            New System.Data.SqlClient.SqlParameter("@Year", _
                                                    System.Data.SqlDbType.Int, 4, "Year"))
        Me.SqlUpdateCommand.Parameters.Add( _
           New System.Data.SqlClient.SqlParameter("@Week", _
                                                   System.Data.SqlDbType.Int, 2, "Week"))
        Me.SqlUpdateCommand.Parameters.Add( _
            New System.Data.SqlClient.SqlParameter("@RatePayCurrencyID", _
                                                    System.Data.SqlDbType.VarChar, 4, "RatePayCurrencyID"))
        Me.SqlUpdateCommand.Parameters.Add( _
            New System.Data.SqlClient.SqlParameter("@RatePayLocation", _
                                                    System.Data.SqlDbType.VarChar, 3, "RatePayLocation"))
        Me.SqlUpdateCommand.Parameters.Add( _
            New System.Data.SqlClient.SqlParameter("@RateAssignCurrencyID", _
                                                    System.Data.SqlDbType.VarChar, 4, "RateAssignCurrencyID"))
        Me.SqlUpdateCommand.Parameters.Add( _
            New System.Data.SqlClient.SqlParameter("@RateAssignLocation", _
                                                    System.Data.SqlDbType.VarChar, 3, "RateAssignLocation"))
        Me.SqlUpdateCommand.Parameters.Add( _
            New System.Data.SqlClient.SqlParameter("@LabourCost", _
                                                    System.Data.SqlDbType.Decimal, 9, _
                                                    System.Data.ParameterDirection.Input, False, _
                                                    CType(18, Byte), CType(2, Byte), _
                                                    "LabourCost", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand.Parameters.Add( _
            New System.Data.SqlClient.SqlParameter("@OtherOfficeSpace", _
                                                    System.Data.SqlDbType.Decimal, 9, _
                                                    System.Data.ParameterDirection.Input, True, _
                                                    CType(18, Byte), CType(2, Byte), _
                                                    "OtherOfficeSpace", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand.Parameters.Add( _
            New System.Data.SqlClient.SqlParameter("@OtherOfficeExpenses", _
                                                    System.Data.SqlDbType.Decimal, 9, _
                                                    System.Data.ParameterDirection.Input, True, _
                                                    CType(18, Byte), CType(2, Byte), _
                                                    "OtherOfficeExpenses", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand.Parameters.Add( _
            New System.Data.SqlClient.SqlParameter("@OtherIT", _
                                                    System.Data.SqlDbType.Decimal, 9, _
                                                    System.Data.ParameterDirection.Input, True, _
                                                    CType(18, Byte), CType(2, Byte), _
                                                    "OtherIT", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand.Parameters.Add( _
            New System.Data.SqlClient.SqlParameter("@OtherITCore", _
                                                    System.Data.SqlDbType.Decimal, 9, _
                                                    System.Data.ParameterDirection.Input, True, _
                                                    CType(18, Byte), CType(2, Byte), _
                                                    "OtherITCore", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand.Parameters.Add( _
            New System.Data.SqlClient.SqlParameter("@AdjustmentShortComment", _
                                                    System.Data.SqlDbType.VarChar, 256, _
                                                    "AdjustmentShortComment"))
        Me.SqlUpdateCommand.Parameters.Add( _
            New System.Data.SqlClient.SqlParameter("@AdjustmentLongComment", _
                                                    System.Data.SqlDbType.VarChar, 2048, _
                                                    "AdjustmentLongComment"))
        Me.SqlUpdateCommand.Parameters.Add( _
            New System.Data.SqlClient.SqlParameter("@Contract", _
                                                    System.Data.SqlDbType.VarChar, 10, _
                                                    "Contract"))
        Me.SqlUpdateCommand.Parameters.Add( _
           New System.Data.SqlClient.SqlParameter("@Discipline", _
                                                   System.Data.SqlDbType.VarChar, 4, _
                                                   "Discipline"))
        Me.SqlUpdateCommand.Parameters.Add( _
      New System.Data.SqlClient.SqlParameter("@CTRCode", _
                                              System.Data.SqlDbType.VarChar, 10, _
                                              "CTRCode"))

        '
        'SqlDeleteCommand
        '
        Me.SqlDeleteCommand.CommandType = CommandType.StoredProcedure
        Me.SqlDeleteCommand.Connection = PrivilegedSQLConnection
        Me.SqlDeleteCommand.CommandText = _
            "CostsAdjustmentsDelete"
        Me.SqlDeleteCommand.Parameters.Add( _
            New System.Data.SqlClient.SqlParameter("@EntryID", _
                                                    System.Data.SqlDbType.Int, 4, "EntryID"))
        '
        'AdjustmentsSqlDataAdapter
        '
        Me.AdjustmentsSqlDataAdapter.InsertCommand = Me.SqlInsertCommand
        Me.AdjustmentsSqlDataAdapter.SelectCommand = Me.SqlSelectCommand
        Me.AdjustmentsSqlDataAdapter.UpdateCommand = Me.SqlUpdateCommand
        Me.AdjustmentsSqlDataAdapter.DeleteCommand = Me.SqlDeleteCommand
        Me.AdjustmentsSqlDataAdapter.TableMappings.AddRange( _
            New System.Data.Common.DataTableMapping() { _
                New System.Data.Common.DataTableMapping("Table", "Adjustments", New System.Data.Common.DataColumnMapping() { _
                    New System.Data.Common.DataColumnMapping("EntryID", "EntryID"), _
                    New System.Data.Common.DataColumnMapping("CompanyID", "CompanyID"), _
                    New System.Data.Common.DataColumnMapping("EmployeeID", "EmployeeID"), _
                    New System.Data.Common.DataColumnMapping("PostedDate", "PostedDate"), _
                    New System.Data.Common.DataColumnMapping("Year", "Year"), _
                    New System.Data.Common.DataColumnMapping("Week", "Week"), _
                    New System.Data.Common.DataColumnMapping("RatePayCurrencyID", "RatePayCurrencyID"), _
                    New System.Data.Common.DataColumnMapping("RatePayLocation", "RatePayLocation"), _
                    New System.Data.Common.DataColumnMapping("RateAssignCurrencyID", "RateAssignCurrencyID"), _
                    New System.Data.Common.DataColumnMapping("RateAssignLocation", "RateAssignLocation"), _
                    New System.Data.Common.DataColumnMapping("LabourCost", "LabourCost"), _
                    New System.Data.Common.DataColumnMapping("OtherOfficeSpace", "OtherOfficeSpace"), _
                    New System.Data.Common.DataColumnMapping("OtherOfficeExpenses", "OtherOfficeExpenses"), _
                    New System.Data.Common.DataColumnMapping("OtherIT", "OtherIT"), _
                    New System.Data.Common.DataColumnMapping("OtherITCore", "OtherITCore"), _
                    New System.Data.Common.DataColumnMapping("Adjustment", "Adjustment"), _
                    New System.Data.Common.DataColumnMapping("AdjustmentShortComment", "AdjustmentShortComment"), _
                    New System.Data.Common.DataColumnMapping("AdjustmentLongComment", "AdjustmentLongComment"), _
                    New System.Data.Common.DataColumnMapping("AdjustmentDate", "AdjustmentDate"), _
                    New System.Data.Common.DataColumnMapping("AdjustmentUser", "AdjustmentUser"), _
                    New System.Data.Common.DataColumnMapping("Contract", "Contract"), _
                    New System.Data.Common.DataColumnMapping("CTRCode", "CTRCode")})})
        '
        'AdjustmentsDataset1
        '
        Me.AdjustmentsDataset.DataSetName = "AdjustmentsDataset"
        Me.AdjustmentsDataset.Locale = New System.Globalization.CultureInfo("en-AU")
        CType(Me.AdjustmentsDataset, System.ComponentModel.ISupportInitialize).EndInit()
        Me.YearTableAdapter = New AdjustmentsDatasetTableAdapters.YearTableAdapter()
        Me.WeekTableAdapter = New AdjustmentsDatasetTableAdapters.WeekTableAdapter()
        Me.CTRCodeTableAdapter = New AdjustmentsDatasetTableAdapters.CTRCodeTableAdapter()

    End Sub

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        Me.PrivilegedSQLConnection = GetSqlConnection()
        Me.InitializeDataset()

        Me.CompanyIDComboBox.DataSource = MyLookupDataset.Company
        Me.CompanyIDComboBox.ValueMember = MyLookupDataset.Company.CompanyIDColumn.ColumnName
        Me.CompanyIDComboBox.DisplayMember = MyLookupDataset.Company.CompanyNameColumn.ColumnName
        Me.CompanyIDComboBox.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.AdjustmentsDataset, "Adjustments.CompanyID"))

        ' need to use a copy so that the binding context understand that the two are independents
        ' note that we apparently cannot create more than one currencymanager for the same data source
        ' in this version, even if documented otherwise
        Me.PostedDateComboBox.DataSource = Me.WeekTableAdapter.GetData()
        Me.PostedDateComboBox.ValueMember = "PostedDate"
        Me.PostedDateComboBox.DisplayMember = "PostedDateToDisplay"
        Me.PostedDateComboBox.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.AdjustmentsDataset, "Adjustments.PostedDate"))

        Me.YearComboBox.DataSource = YearTableAdapter.GetData()
        Me.YearComboBox.ValueMember = "Year"
        Me.YearComboBox.DisplayMember = "Year"
        Me.YearComboBox.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.AdjustmentsDataset, "Adjustments.Year"))

        Me.WeekComboBox.DataSource = Me.WeekTableAdapter.GetData()
        Me.WeekComboBox.ValueMember = "Week"
        Me.WeekComboBox.DisplayMember = "Week"
        Me.WeekComboBox.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.AdjustmentsDataset, "Adjustments.Week"))

        Me.ShortCommentComboBox.DataSource = MyLookupDataset.CostAdjustmentsReasons
        Me.ShortCommentComboBox.ValueMember = "ShortComment"
        Me.ShortCommentComboBox.DisplayMember = "ShortComment"
        Me.ShortCommentComboBox.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.AdjustmentsDataset, "Adjustments.AdjustmentShortComment"))

        Me.LabourCostCurrencyComboBox.DataSource = MyLookupDataset.Currency.Copy
        Me.LabourCostCurrencyComboBox.ValueMember = MyLookupDataset.Currency.CurrencyIDColumn.ColumnName
        Me.LabourCostCurrencyComboBox.DisplayMember = MyLookupDataset.Currency.DescriptionColumn.ColumnName
        Me.LabourCostCurrencyComboBox.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.AdjustmentsDataset, "Adjustments.RatePayCurrencyID"))

        Me.NonLabourCostCurrencyComboBox.DataSource = MyLookupDataset.Currency.Copy
        Me.NonLabourCostCurrencyComboBox.ValueMember = MyLookupDataset.Currency.CurrencyIDColumn.ColumnName
        Me.NonLabourCostCurrencyComboBox.DisplayMember = MyLookupDataset.Currency.DescriptionColumn.ColumnName
        Me.NonLabourCostCurrencyComboBox.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.AdjustmentsDataset, "Adjustments.RateAssignCurrencyID"))

        Me.RatePayLocation.DataSource = MyLookupDataset.Location.Copy
        Me.RatePayLocation.ValueMember = MyLookupDataset.Location.LocationIDColumn.ColumnName
        Me.RatePayLocation.DisplayMember = MyLookupDataset.Location.LocationNameColumn.ColumnName
        Me.RatePayLocation.DataBindings.Add(New Windows.Forms.Binding("SelectedValue", Me.AdjustmentsDataset, "Adjustments.RatePayLocation"))

        Me.RateAssignLocation.DataSource = MyLookupDataset.Location.Copy
        Me.RateAssignLocation.ValueMember = MyLookupDataset.Location.LocationIDColumn.ColumnName
        Me.RateAssignLocation.DisplayMember = MyLookupDataset.Location.LocationNameColumn.ColumnName
        Me.RateAssignLocation.DataBindings.Add(New Windows.Forms.Binding("SelectedValue", Me.AdjustmentsDataset, "Adjustments.RateAssignLocation"))

        Me.EmployeeID.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.AdjustmentsDataset, "Adjustments.EmployeeID"))

        Me.LongCommentTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.AdjustmentsDataset, "Adjustments.AdjustmentLongComment"))
        Me.EntryID.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.AdjustmentsDataset, "Adjustments.EntryID"))

        Me.LabourCostsBinding = New System.Windows.Forms.Binding("Text", Me.AdjustmentsDataset, "Adjustments.LabourCost")
        Me.LabourCostTextBox.DataBindings.Add(LabourCostsBinding)

        Me.ITCoreCostsBinding = New System.Windows.Forms.Binding("Text", Me.AdjustmentsDataset, "Adjustments.OtherITCore")
        Me.ITCoreCostsTextBox.DataBindings.Add(ITCoreCostsBinding)
        Me.ITCostsBinding = New System.Windows.Forms.Binding("Text", Me.AdjustmentsDataset, "Adjustments.OtherIT")
        Me.ITCostsTextBox.DataBindings.Add(ITCostsBinding)

        Me.OfficeExpensesCostsBinding = New System.Windows.Forms.Binding("Text", Me.AdjustmentsDataset, "Adjustments.OtherOfficeExpenses")
        Me.OfficeExpensesTextBox.DataBindings.Add(OfficeExpensesCostsBinding)
        Me.OfficeSpaceCostsBinding = New System.Windows.Forms.Binding("Text", Me.AdjustmentsDataset, "Adjustments.OtherOfficeSpace")
        Me.OfficeSpaceTextBox.DataBindings.Add(OfficeSpaceCostsBinding)

        'LGX : init combobox Contracts
        Me.ContractComboBox.DataSource = MyLookupDataset.Contracts
        Me.ContractComboBox.ValueMember = MyLookupDataset.Contracts.ContractIDColumn.ColumnName
        Me.ContractComboBox.DisplayMember = MyLookupDataset.Contracts.ContractIDColumn.ColumnName
        Me.ContractComboBox.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.AdjustmentsDataset, "Adjustments.Contract"))

        Me.DisciplineAdjComboBox.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.AdjustmentsDataset, "Adjustments.Discipline"))        

        Me.CTRCodeComboBox.DataSource = Me.CTRCodeTableAdapter.GetData()
        Me.CTRCodeComboBox.ValueMember = "CTRCode"
        Me.CTRCodeComboBox.DisplayMember = "CTRCode"
        Me.CTRCodeComboBox.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.AdjustmentsDataset, "Adjustments.CTRCode"))


        AdjustementBindingContext = Me.BindingContext(AdjustmentsDataset, "Adjustments")

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents SqlSelectCommand As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlInsertCommand As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlUpdateCommand As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlDeleteCommand As System.Data.SqlClient.SqlCommand
    Friend WithEvents AdjustmentsSqlDataAdapter As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents EntryID As System.Windows.Forms.TextBox
    Friend WithEvents EmployeeID As System.Windows.Forms.TextBox
    Friend WithEvents NextButton As System.Windows.Forms.Button
    Friend WithEvents PreviousButton As System.Windows.Forms.Button
    Friend WithEvents AdjustmentsDataset As PAF.AdjustmentsDataset
    Friend WithEvents CompanyIDComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents EmployeeIDComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents PostedDateComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents LabourCostCurrencyComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents NonLabourCostCurrencyComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents LabourCostsGroupBox As System.Windows.Forms.GroupBox
    Friend WithEvents NonLabourCostsGroupBox As System.Windows.Forms.GroupBox
    Friend WithEvents LabourCostTextBox As System.Windows.Forms.TextBox
    Friend WithEvents OfficeSpaceTextBox As System.Windows.Forms.TextBox
    Friend WithEvents OfficeExpensesTextBox As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents ITCostsTextBox As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents ITCoreCostsTextBox As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents LongCommentTextBox As System.Windows.Forms.TextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents NewButton As System.Windows.Forms.Button
    Friend WithEvents SaveButton As System.Windows.Forms.Button
    Friend Shadows WithEvents CancelButton As System.Windows.Forms.Button
    Friend WithEvents DeleteButton As System.Windows.Forms.Button
    Friend WithEvents LabourCostsBinding As Binding
    Friend WithEvents OfficeSpaceCostsBinding As Binding
    Friend WithEvents OfficeExpensesCostsBinding As Binding
    Friend WithEvents ITCostsBinding As Binding
    Friend WithEvents ITCoreCostsBinding As Binding
    Friend WithEvents RatePayLocationBinding As Binding
    Friend WithEvents RateAssignLocationBinding As Binding
    Friend WithEvents AdjustementBindingContext As BindingManagerBase
    Friend WithEvents RecordNoTextBox As System.Windows.Forms.TextBox
    Friend WithEvents YearComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents WeekComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents RatePayLocation As System.Windows.Forms.ComboBox
    Friend WithEvents RateAssignLocation As System.Windows.Forms.ComboBox
    Friend WithEvents ShortCommentComboBox As System.Windows.Forms.ComboBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.EntryID = New System.Windows.Forms.TextBox()
        Me.EmployeeID = New System.Windows.Forms.TextBox()
        Me.NextButton = New System.Windows.Forms.Button()
        Me.PreviousButton = New System.Windows.Forms.Button()
        Me.CompanyIDComboBox = New System.Windows.Forms.ComboBox()
        Me.EmployeeIDComboBox = New System.Windows.Forms.ComboBox()
        Me.PostedDateComboBox = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.LabourCostCurrencyComboBox = New System.Windows.Forms.ComboBox()
        Me.NonLabourCostCurrencyComboBox = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.LabourCostsGroupBox = New System.Windows.Forms.GroupBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.RatePayLocation = New System.Windows.Forms.ComboBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.LabourCostTextBox = New System.Windows.Forms.TextBox()
        Me.NonLabourCostsGroupBox = New System.Windows.Forms.GroupBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.ITCoreCostsTextBox = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.ITCostsTextBox = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.OfficeExpensesTextBox = New System.Windows.Forms.TextBox()
        Me.OfficeSpaceTextBox = New System.Windows.Forms.TextBox()
        Me.RateAssignLocation = New System.Windows.Forms.ComboBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.LongCommentTextBox = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.NewButton = New System.Windows.Forms.Button()
        Me.SaveButton = New System.Windows.Forms.Button()
        Me.CancelButton = New System.Windows.Forms.Button()
        Me.DeleteButton = New System.Windows.Forms.Button()
        Me.RecordNoTextBox = New System.Windows.Forms.TextBox()
        Me.YearComboBox = New System.Windows.Forms.ComboBox()
        Me.WeekComboBox = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.ShortCommentComboBox = New System.Windows.Forms.ComboBox()
        Me.EmployeeFormButton = New System.Windows.Forms.Button()
        Me.ContractComboBox = New System.Windows.Forms.ComboBox()
        Me.DisciplineAdjComboBox = New System.Windows.Forms.ComboBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.AdjustmentsDataset = New PAF.AdjustmentsDataset()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.CTRCode = New System.Windows.Forms.Label()
        Me.CTRCodeComboBox = New System.Windows.Forms.ComboBox()
        Me.NewLookupDataset = New PAF.NewLookupDataset()
        Me.DisciplineBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.DisciplineTableAdapter = New PAF.NewLookupDatasetTableAdapters.DisciplineTableAdapter()
        Me.LabourCostsGroupBox.SuspendLayout()
        Me.NonLabourCostsGroupBox.SuspendLayout()
        CType(Me.AdjustmentsDataset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NewLookupDataset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DisciplineBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'EntryID
        '
        Me.EntryID.Enabled = False
        Me.EntryID.Location = New System.Drawing.Point(96, 16)
        Me.EntryID.Name = "EntryID"
        Me.EntryID.ReadOnly = True
        Me.EntryID.Size = New System.Drawing.Size(100, 20)
        Me.EntryID.TabIndex = 0
        Me.EntryID.TabStop = False
        Me.EntryID.Text = "EntryID"
        '
        'EmployeeID
        '
        Me.EmployeeID.Location = New System.Drawing.Point(306, 80)
        Me.EmployeeID.Name = "EmployeeID"
        Me.EmployeeID.ReadOnly = True
        Me.EmployeeID.Size = New System.Drawing.Size(40, 20)
        Me.EmployeeID.TabIndex = 2
        Me.EmployeeID.Text = "EmployeeID"
        '
        'NextButton
        '
        Me.NextButton.Location = New System.Drawing.Point(464, 389)
        Me.NextButton.Name = "NextButton"
        Me.NextButton.Size = New System.Drawing.Size(32, 23)
        Me.NextButton.TabIndex = 25
        Me.NextButton.Text = ">"
        '
        'PreviousButton
        '
        Me.PreviousButton.Location = New System.Drawing.Point(432, 389)
        Me.PreviousButton.Name = "PreviousButton"
        Me.PreviousButton.Size = New System.Drawing.Size(32, 23)
        Me.PreviousButton.TabIndex = 24
        Me.PreviousButton.Text = "<"
        '
        'CompanyIDComboBox
        '
        Me.CompanyIDComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CompanyIDComboBox.Location = New System.Drawing.Point(96, 48)
        Me.CompanyIDComboBox.Name = "CompanyIDComboBox"
        Me.CompanyIDComboBox.Size = New System.Drawing.Size(136, 21)
        Me.CompanyIDComboBox.TabIndex = 1
        '
        'EmployeeIDComboBox
        '
        Me.EmployeeIDComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.EmployeeIDComboBox.Location = New System.Drawing.Point(96, 80)
        Me.EmployeeIDComboBox.Name = "EmployeeIDComboBox"
        Me.EmployeeIDComboBox.Size = New System.Drawing.Size(209, 21)
        Me.EmployeeIDComboBox.TabIndex = 2
        '
        'PostedDateComboBox
        '
        Me.PostedDateComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.PostedDateComboBox.Location = New System.Drawing.Point(367, 48)
        Me.PostedDateComboBox.Name = "PostedDateComboBox"
        Me.PostedDateComboBox.Size = New System.Drawing.Size(121, 21)
        Me.PostedDateComboBox.TabIndex = 3
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(40, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(48, 24)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "EntryID"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(32, 48)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(60, 24)
        Me.Label2.TabIndex = 9
        Me.Label2.Text = "Company"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(32, 80)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(56, 24)
        Me.Label3.TabIndex = 10
        Me.Label3.Text = "Employee"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(279, 45)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(80, 24)
        Me.Label4.TabIndex = 12
        Me.Label4.Text = "Posted Date"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'LabourCostCurrencyComboBox
        '
        Me.LabourCostCurrencyComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.LabourCostCurrencyComboBox.Location = New System.Drawing.Point(80, 24)
        Me.LabourCostCurrencyComboBox.Name = "LabourCostCurrencyComboBox"
        Me.LabourCostCurrencyComboBox.Size = New System.Drawing.Size(120, 21)
        Me.LabourCostCurrencyComboBox.TabIndex = 7
        '
        'NonLabourCostCurrencyComboBox
        '
        Me.NonLabourCostCurrencyComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.NonLabourCostCurrencyComboBox.Location = New System.Drawing.Point(112, 19)
        Me.NonLabourCostCurrencyComboBox.Name = "NonLabourCostCurrencyComboBox"
        Me.NonLabourCostCurrencyComboBox.Size = New System.Drawing.Size(120, 21)
        Me.NonLabourCostCurrencyComboBox.TabIndex = 10
        '
        'Label6
        '
        Me.Label6.Location = New System.Drawing.Point(47, 16)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(56, 24)
        Me.Label6.TabIndex = 16
        Me.Label6.Text = "Currency"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label7
        '
        Me.Label7.Location = New System.Drawing.Point(16, 24)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(56, 24)
        Me.Label7.TabIndex = 17
        Me.Label7.Text = "Currency"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'LabourCostsGroupBox
        '
        Me.LabourCostsGroupBox.Controls.Add(Me.Label16)
        Me.LabourCostsGroupBox.Controls.Add(Me.RatePayLocation)
        Me.LabourCostsGroupBox.Controls.Add(Me.Label8)
        Me.LabourCostsGroupBox.Controls.Add(Me.LabourCostTextBox)
        Me.LabourCostsGroupBox.Controls.Add(Me.Label7)
        Me.LabourCostsGroupBox.Controls.Add(Me.LabourCostCurrencyComboBox)
        Me.LabourCostsGroupBox.Location = New System.Drawing.Point(16, 179)
        Me.LabourCostsGroupBox.Name = "LabourCostsGroupBox"
        Me.LabourCostsGroupBox.Size = New System.Drawing.Size(232, 142)
        Me.LabourCostsGroupBox.TabIndex = 7
        Me.LabourCostsGroupBox.TabStop = False
        Me.LabourCostsGroupBox.Text = "Labour Costs"
        '
        'Label16
        '
        Me.Label16.Location = New System.Drawing.Point(16, 80)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(56, 24)
        Me.Label16.TabIndex = 21
        Me.Label16.Text = "Location"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'RatePayLocation
        '
        Me.RatePayLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.RatePayLocation.Location = New System.Drawing.Point(80, 80)
        Me.RatePayLocation.Name = "RatePayLocation"
        Me.RatePayLocation.Size = New System.Drawing.Size(120, 21)
        Me.RatePayLocation.TabIndex = 9
        '
        'Label8
        '
        Me.Label8.Location = New System.Drawing.Point(16, 56)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(56, 24)
        Me.Label8.TabIndex = 19
        Me.Label8.Text = "Cost"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'LabourCostTextBox
        '
        Me.LabourCostTextBox.Location = New System.Drawing.Point(80, 56)
        Me.LabourCostTextBox.Name = "LabourCostTextBox"
        Me.LabourCostTextBox.Size = New System.Drawing.Size(120, 20)
        Me.LabourCostTextBox.TabIndex = 8
        Me.LabourCostTextBox.Text = "LabourCost"
        Me.LabourCostTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'NonLabourCostsGroupBox
        '
        Me.NonLabourCostsGroupBox.Controls.Add(Me.Label17)
        Me.NonLabourCostsGroupBox.Controls.Add(Me.Label12)
        Me.NonLabourCostsGroupBox.Controls.Add(Me.ITCoreCostsTextBox)
        Me.NonLabourCostsGroupBox.Controls.Add(Me.Label11)
        Me.NonLabourCostsGroupBox.Controls.Add(Me.ITCostsTextBox)
        Me.NonLabourCostsGroupBox.Controls.Add(Me.Label10)
        Me.NonLabourCostsGroupBox.Controls.Add(Me.Label9)
        Me.NonLabourCostsGroupBox.Controls.Add(Me.OfficeExpensesTextBox)
        Me.NonLabourCostsGroupBox.Controls.Add(Me.OfficeSpaceTextBox)
        Me.NonLabourCostsGroupBox.Controls.Add(Me.NonLabourCostCurrencyComboBox)
        Me.NonLabourCostsGroupBox.Controls.Add(Me.Label6)
        Me.NonLabourCostsGroupBox.Controls.Add(Me.RateAssignLocation)
        Me.NonLabourCostsGroupBox.Location = New System.Drawing.Point(256, 137)
        Me.NonLabourCostsGroupBox.Name = "NonLabourCostsGroupBox"
        Me.NonLabourCostsGroupBox.Size = New System.Drawing.Size(240, 184)
        Me.NonLabourCostsGroupBox.TabIndex = 10
        Me.NonLabourCostsGroupBox.TabStop = False
        Me.NonLabourCostsGroupBox.Text = "Non Labour Costs"
        '
        'Label17
        '
        Me.Label17.Location = New System.Drawing.Point(24, 147)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(88, 24)
        Me.Label17.TabIndex = 25
        Me.Label17.Text = "Location"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label12
        '
        Me.Label12.Location = New System.Drawing.Point(24, 121)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(88, 24)
        Me.Label12.TabIndex = 24
        Me.Label12.Text = "IT Core Costs"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'ITCoreCostsTextBox
        '
        Me.ITCoreCostsTextBox.Location = New System.Drawing.Point(112, 124)
        Me.ITCoreCostsTextBox.Name = "ITCoreCostsTextBox"
        Me.ITCoreCostsTextBox.Size = New System.Drawing.Size(120, 20)
        Me.ITCoreCostsTextBox.TabIndex = 14
        Me.ITCoreCostsTextBox.Text = "ITCoreCosts"
        Me.ITCoreCostsTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label11
        '
        Me.Label11.Location = New System.Drawing.Point(19, 94)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(88, 24)
        Me.Label11.TabIndex = 22
        Me.Label11.Text = "IT Costs"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'ITCostsTextBox
        '
        Me.ITCostsTextBox.Location = New System.Drawing.Point(113, 98)
        Me.ITCostsTextBox.Name = "ITCostsTextBox"
        Me.ITCostsTextBox.Size = New System.Drawing.Size(120, 20)
        Me.ITCostsTextBox.TabIndex = 13
        Me.ITCostsTextBox.Text = "ITCosts"
        Me.ITCostsTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label10
        '
        Me.Label10.Location = New System.Drawing.Point(16, 66)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(88, 24)
        Me.Label10.TabIndex = 20
        Me.Label10.Text = "Office Expenses"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label9
        '
        Me.Label9.Location = New System.Drawing.Point(19, 42)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(88, 24)
        Me.Label9.TabIndex = 19
        Me.Label9.Text = "Office Space"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'OfficeExpensesTextBox
        '
        Me.OfficeExpensesTextBox.Location = New System.Drawing.Point(112, 72)
        Me.OfficeExpensesTextBox.Name = "OfficeExpensesTextBox"
        Me.OfficeExpensesTextBox.Size = New System.Drawing.Size(120, 20)
        Me.OfficeExpensesTextBox.TabIndex = 12
        Me.OfficeExpensesTextBox.Text = "OfficeExpenses"
        Me.OfficeExpensesTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'OfficeSpaceTextBox
        '
        Me.OfficeSpaceTextBox.Location = New System.Drawing.Point(113, 46)
        Me.OfficeSpaceTextBox.Name = "OfficeSpaceTextBox"
        Me.OfficeSpaceTextBox.Size = New System.Drawing.Size(120, 20)
        Me.OfficeSpaceTextBox.TabIndex = 11
        Me.OfficeSpaceTextBox.Text = "OfficeSpace"
        Me.OfficeSpaceTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'RateAssignLocation
        '
        Me.RateAssignLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.RateAssignLocation.Location = New System.Drawing.Point(113, 150)
        Me.RateAssignLocation.Name = "RateAssignLocation"
        Me.RateAssignLocation.Size = New System.Drawing.Size(120, 21)
        Me.RateAssignLocation.TabIndex = 15
        '
        'Label13
        '
        Me.Label13.Location = New System.Drawing.Point(0, 326)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(136, 24)
        Me.Label13.TabIndex = 21
        Me.Label13.Text = "Short comment for reports"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'LongCommentTextBox
        '
        Me.LongCommentTextBox.AcceptsReturn = True
        Me.LongCommentTextBox.Location = New System.Drawing.Point(136, 350)
        Me.LongCommentTextBox.Multiline = True
        Me.LongCommentTextBox.Name = "LongCommentTextBox"
        Me.LongCommentTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.LongCommentTextBox.Size = New System.Drawing.Size(360, 32)
        Me.LongCommentTextBox.TabIndex = 17
        Me.LongCommentTextBox.Text = "LongComment"
        '
        'Label14
        '
        Me.Label14.Location = New System.Drawing.Point(56, 350)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(80, 24)
        Me.Label14.TabIndex = 23
        Me.Label14.Text = "Comments"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'NewButton
        '
        Me.NewButton.Location = New System.Drawing.Point(16, 389)
        Me.NewButton.Name = "NewButton"
        Me.NewButton.Size = New System.Drawing.Size(48, 24)
        Me.NewButton.TabIndex = 20
        Me.NewButton.Text = "New"
        '
        'SaveButton
        '
        Me.SaveButton.Location = New System.Drawing.Point(72, 389)
        Me.SaveButton.Name = "SaveButton"
        Me.SaveButton.Size = New System.Drawing.Size(48, 24)
        Me.SaveButton.TabIndex = 21
        Me.SaveButton.Text = "Save"
        '
        'CancelButton
        '
        Me.CancelButton.Location = New System.Drawing.Point(128, 389)
        Me.CancelButton.Name = "CancelButton"
        Me.CancelButton.Size = New System.Drawing.Size(48, 24)
        Me.CancelButton.TabIndex = 22
        Me.CancelButton.Text = "Cancel"
        '
        'DeleteButton
        '
        Me.DeleteButton.Location = New System.Drawing.Point(184, 389)
        Me.DeleteButton.Name = "DeleteButton"
        Me.DeleteButton.Size = New System.Drawing.Size(48, 24)
        Me.DeleteButton.TabIndex = 23
        Me.DeleteButton.Text = "Delete"
        '
        'RecordNoTextBox
        '
        Me.RecordNoTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.RecordNoTextBox.Enabled = False
        Me.RecordNoTextBox.Location = New System.Drawing.Point(320, 390)
        Me.RecordNoTextBox.Name = "RecordNoTextBox"
        Me.RecordNoTextBox.ReadOnly = True
        Me.RecordNoTextBox.Size = New System.Drawing.Size(112, 20)
        Me.RecordNoTextBox.TabIndex = 32
        Me.RecordNoTextBox.TabStop = False
        Me.RecordNoTextBox.Text = "TextBox1"
        '
        'YearComboBox
        '
        Me.YearComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.YearComboBox.Location = New System.Drawing.Point(307, 16)
        Me.YearComboBox.Name = "YearComboBox"
        Me.YearComboBox.Size = New System.Drawing.Size(61, 21)
        Me.YearComboBox.TabIndex = 4
        '
        'WeekComboBox
        '
        Me.WeekComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.WeekComboBox.Location = New System.Drawing.Point(445, 16)
        Me.WeekComboBox.Name = "WeekComboBox"
        Me.WeekComboBox.Size = New System.Drawing.Size(43, 21)
        Me.WeekComboBox.TabIndex = 4
        '
        'Label5
        '
        Me.Label5.Location = New System.Drawing.Point(253, 14)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(40, 24)
        Me.Label5.TabIndex = 34
        Me.Label5.Text = "Year"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'ShortCommentComboBox
        '
        Me.ShortCommentComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ShortCommentComboBox.Location = New System.Drawing.Point(136, 326)
        Me.ShortCommentComboBox.Name = "ShortCommentComboBox"
        Me.ShortCommentComboBox.Size = New System.Drawing.Size(360, 21)
        Me.ShortCommentComboBox.TabIndex = 16
        '
        'EmployeeFormButton
        '
        Me.EmployeeFormButton.Location = New System.Drawing.Point(348, 79)
        Me.EmployeeFormButton.Name = "EmployeeFormButton"
        Me.EmployeeFormButton.Size = New System.Drawing.Size(76, 22)
        Me.EmployeeFormButton.TabIndex = 35
        Me.EmployeeFormButton.Text = "Details"
        Me.EmployeeFormButton.UseVisualStyleBackColor = True
        '
        'ContractComboBox
        '
        Me.ContractComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ContractComboBox.FormattingEnabled = True
        Me.ContractComboBox.Location = New System.Drawing.Point(96, 110)
        Me.ContractComboBox.Name = "ContractComboBox"
        Me.ContractComboBox.Size = New System.Drawing.Size(121, 21)
        Me.ContractComboBox.TabIndex = 36
        '
        'DisciplineAdjComboBox
        '
        Me.DisciplineAdjComboBox.DataSource = Me.DisciplineBindingSource
        Me.DisciplineAdjComboBox.DisplayMember = "Expr1"
        Me.DisciplineAdjComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.DisciplineAdjComboBox.FormattingEnabled = True
        Me.DisciplineAdjComboBox.Location = New System.Drawing.Point(95, 145)
        Me.DisciplineAdjComboBox.Name = "DisciplineAdjComboBox"
        Me.DisciplineAdjComboBox.Size = New System.Drawing.Size(121, 21)
        Me.DisciplineAdjComboBox.TabIndex = 41
        Me.DisciplineAdjComboBox.ValueMember = "DisciplineCode"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(40, 113)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(47, 13)
        Me.Label15.TabIndex = 37
        Me.Label15.Text = "Contract"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(403, 20)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(36, 13)
        Me.Label18.TabIndex = 38
        Me.Label18.Text = "Week"
        '
        'Label19
        '
        Me.Label19.Location = New System.Drawing.Point(0, 0)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(100, 23)
        Me.Label19.TabIndex = 41
        '
        'AdjustmentsDataset
        '
        Me.AdjustmentsDataset.DataSetName = "AdjustmentsDataset"
        Me.AdjustmentsDataset.Locale = New System.Globalization.CultureInfo("en-AU")
        Me.AdjustmentsDataset.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(37, 148)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(52, 13)
        Me.Label20.TabIndex = 40
        Me.Label20.Text = "Discipline"
        '
        'CTRCode
        '
        Me.CTRCode.AutoSize = True
        Me.CTRCode.Location = New System.Drawing.Point(304, 114)
        Me.CTRCode.Name = "CTRCode"
        Me.CTRCode.Size = New System.Drawing.Size(54, 13)
        Me.CTRCode.TabIndex = 42
        Me.CTRCode.Text = "CTRCode"
        '
        'CTRCodeComboBox
        '
        Me.CTRCodeComboBox.FormattingEnabled = True
        Me.CTRCodeComboBox.Location = New System.Drawing.Point(367, 110)
        Me.CTRCodeComboBox.Name = "CTRCodeComboBox"
        Me.CTRCodeComboBox.Size = New System.Drawing.Size(121, 21)
        Me.CTRCodeComboBox.TabIndex = 43
        '
        'NewLookupDataset
        '
        Me.NewLookupDataset.DataSetName = "NewLookupDataset"
        Me.NewLookupDataset.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'DisciplineBindingSource
        '
        Me.DisciplineBindingSource.DataMember = "Discipline"
        Me.DisciplineBindingSource.DataSource = Me.NewLookupDataset
        '
        'DisciplineTableAdapter
        '
        Me.DisciplineTableAdapter.ClearBeforeFill = True
        '
        'AdjustmentsForm
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(504, 423)
        Me.Controls.Add(Me.CTRCode)
        Me.Controls.Add(Me.Label20)
        Me.Controls.Add(Me.Label19)
        Me.Controls.Add(Me.Label18)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.ContractComboBox)
        Me.Controls.Add(Me.DisciplineAdjComboBox)
        Me.Controls.Add(Me.EmployeeFormButton)
        Me.Controls.Add(Me.ShortCommentComboBox)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.YearComboBox)
        Me.Controls.Add(Me.WeekComboBox)
        Me.Controls.Add(Me.RecordNoTextBox)
        Me.Controls.Add(Me.LongCommentTextBox)
        Me.Controls.Add(Me.EmployeeID)
        Me.Controls.Add(Me.EntryID)
        Me.Controls.Add(Me.DeleteButton)
        Me.Controls.Add(Me.CancelButton)
        Me.Controls.Add(Me.SaveButton)
        Me.Controls.Add(Me.NewButton)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.NonLabourCostsGroupBox)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.PostedDateComboBox)
        Me.Controls.Add(Me.EmployeeIDComboBox)
        Me.Controls.Add(Me.CompanyIDComboBox)
        Me.Controls.Add(Me.PreviousButton)
        Me.Controls.Add(Me.NextButton)
        Me.Controls.Add(Me.LabourCostsGroupBox)
        Me.Controls.Add(Me.CTRCodeComboBox)
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(512, 450)
        Me.MinimumSize = New System.Drawing.Size(512, 450)
        Me.Name = "AdjustmentsForm"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.Text = "Cost Adjustments"
        Me.LabourCostsGroupBox.ResumeLayout(False)
        Me.LabourCostsGroupBox.PerformLayout()
        Me.NonLabourCostsGroupBox.ResumeLayout(False)
        Me.NonLabourCostsGroupBox.PerformLayout()
        CType(Me.AdjustmentsDataset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NewLookupDataset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DisciplineBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Private Sub LoadAdjustments()
        Dim i As Integer
        Try
            AdjustmentsDataset.Adjustments.Clear()
            i = AdjustmentsSqlDataAdapter.Fill(AdjustmentsDataset.Adjustments)
            AdjustmentsDataset.Adjustments.AcceptChanges()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Application Error")
            Me.Close()
        End Try
    End Sub
    Private Sub AdjustmentsForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'NewLookupDataset.Discipline' table. You can move, or remove it, as needed.
        Me.DisciplineTableAdapter.Fill(Me.NewLookupDataset.Discipline)
        LoadAdjustments()
        If AdjustementBindingContext.Count = 0 Then
            EnableControls(False)
        End If
        EnableNavigationButtons()
        SynchronizeEmployeeList()
    End Sub
    Private Sub AdjustmentsForm_Close(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Closed
        MyCostsAdjustmentForm = Nothing
    End Sub
    Private Sub ClosingEventHandler(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        If IsOKToCancelExistingChanges() Then
            e.Cancel = False
        Else
            e.Cancel = True
        End If
    End Sub
    Private Sub NextButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NextButton.Click
        If CurrentRecordIsValid() Then
            AdjustementBindingContext.Position += 1
            EnableNavigationButtons()
        End If
    End Sub
    Private Sub PreviousButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PreviousButton.Click
        If CurrentRecordIsValid() Then
            AdjustementBindingContext.Position -= 1
            EnableNavigationButtons()
        End If
    End Sub
    Private Sub EnableNavigationButtons()
        If AdjustementBindingContext.Position + 1 = AdjustementBindingContext.Count Then
            NextButton.Enabled = False
        Else
            NextButton.Enabled = True
        End If
        If AdjustementBindingContext.Position = 0 Then
            PreviousButton.Enabled = False
        Else
            PreviousButton.Enabled = True
        End If
        Me.RecordNoTextBox.Text = "Record " & AdjustementBindingContext.Position + 1 & " of " & AdjustementBindingContext.Count
    End Sub
    Private Sub CompanyIDComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CompanyIDComboBox.SelectedIndexChanged
        If IsNothing(AdjustementBindingContext) OrElse AdjustementBindingContext.Position = -1 Then
            Return
        End If
        If CType(CType(AdjustementBindingContext.Current, Data.DataRowView).Row, AdjustmentsDataset.AdjustmentsRow).IsCompanyIDNull Then
            Me.CompanyIDComboBox.SelectedIndex = -1
            Return
        End If
        SynchronizeEmployeeList()
    End Sub
    Private Sub CompanyIDComboBox_SelectedIndexChangedCommited(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CompanyIDComboBox.SelectionChangeCommitted
        If CType(CType(AdjustementBindingContext.Current, Data.DataRowView).Row, AdjustmentsDataset.AdjustmentsRow).IsCompanyIDNull Then
            CType(CType(AdjustementBindingContext.Current, Data.DataRowView).Row, AdjustmentsDataset.AdjustmentsRow).CompanyID = CType(Me.CompanyIDComboBox.SelectedValue, String)
        Else
            If CType(CType(AdjustementBindingContext.Current, Data.DataRowView).Row, AdjustmentsDataset.AdjustmentsRow).CompanyID <> CType(Me.CompanyIDComboBox.SelectedValue, String) Then
                CType(CType(AdjustementBindingContext.Current, Data.DataRowView).Row, AdjustmentsDataset.AdjustmentsRow).CompanyID = CType(Me.CompanyIDComboBox.SelectedValue, String)
            End If
        End If
        AdjustementBindingContext.EndCurrentEdit()
    End Sub
    Private Sub SynchronizeEmployeeList(ByVal direction As Integer)
        If CType(CType(AdjustementBindingContext.Current, Data.DataRowView).Row, AdjustmentsDataset.AdjustmentsRow).IsCompanyIDNull Then
            Return
        End If

        AdjustementBindingContext.Position += direction
        Dim CurrentCompanyID As String = CType(Me.CompanyIDComboBox.SelectedValue, String)
        Dim NewEmployeeList As EmployeeListsClass.EmployeeList = EmployeeLists.Item(CurrentCompanyID)

        Me.EmployeeIDComboBox.Items.Clear()

        Dim row As EmployeeListsClass.EmployeeItem
        For Each row In NewEmployeeList
            Me.EmployeeIDComboBox.Items.Add(row.EmployeeName)
        Next

        If CType(CType(AdjustementBindingContext.Current, Data.DataRowView).Row, AdjustmentsDataset.AdjustmentsRow).IsEmployeeIDNull Then
            Me.EmployeeIDComboBox.SelectedIndex = -1
        Else
            Dim CurrentEmployee As Integer = CType(CType(AdjustementBindingContext.Current, Data.DataRowView).Row, AdjustmentsDataset.AdjustmentsRow).EmployeeID
            Me.EmployeeIDComboBox.SelectedIndex = NewEmployeeList.IndexOf(CurrentEmployee)
        End If
        Return
    End Sub
    Private Sub SynchronizeEmployeeList()
        If AdjustementBindingContext.Position = -1 Then
            Return
        End If
        If CType(CType(AdjustementBindingContext.Current, Data.DataRowView).Row, AdjustmentsDataset.AdjustmentsRow).IsCompanyIDNull Then
            Me.EmployeeIDComboBox.Items.Clear()
            Return
        End If

        Dim CurrentCompanyID As String = CType(Me.CompanyIDComboBox.SelectedValue, String)
        Dim NewEmployeeList As EmployeeListsClass.EmployeeList = EmployeeLists.Item(CurrentCompanyID)

        Me.EmployeeIDComboBox.Items.Clear()

        Dim row As EmployeeListsClass.EmployeeItem
        For Each row In NewEmployeeList
            Me.EmployeeIDComboBox.Items.Add(row.EmployeeName)
        Next

        If CType(CType(AdjustementBindingContext.Current, Data.DataRowView).Row, AdjustmentsDataset.AdjustmentsRow).IsEmployeeIDNull Then
            EmployeeIDComboBox.SelectedIndex = -1
        Else
            Dim CurrentEmployee As Integer = CType(CType(AdjustementBindingContext.Current, Data.DataRowView).Row, AdjustmentsDataset.AdjustmentsRow).EmployeeID
            Me.EmployeeIDComboBox.SelectedIndex = NewEmployeeList.IndexOf(CurrentEmployee)
        End If
    End Sub
    Private Sub EmployeeComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EmployeeIDComboBox.SelectedIndexChanged
        If IsNothing(AdjustementBindingContext) OrElse AdjustementBindingContext.Position = -1 Then
            Return
        End If
        If CType(CType(AdjustementBindingContext.Current, Data.DataRowView).Row, AdjustmentsDataset.AdjustmentsRow).IsEmployeeIDNull Then
            Me.EmployeeIDComboBox.SelectedIndex = -1
        End If
    End Sub
    Private Sub EmployeeComboBox_SelectedIndexChangedCommited(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EmployeeIDComboBox.SelectionChangeCommitted
        If Me.CompanyIDComboBox.SelectedIndex = -1 Then
            Return
        End If

        Dim MyList As EmployeeListsClass.EmployeeList = EmployeeLists.Item(CType(Me.CompanyIDComboBox.SelectedValue, String))
        Dim NewEmployeeID As Integer = MyList.EmployeeIDOfIndex(Me.EmployeeIDComboBox.SelectedIndex)
        Dim CurrentRow As AdjustmentsDataset.AdjustmentsRow = CType(CType(AdjustementBindingContext.Current, DataRowView).Row, AdjustmentsDataset.AdjustmentsRow)

        If IsNothing(NewEmployeeID) Then
            If Not CurrentRow.IsEmployeeIDNull Then
                CurrentRow.SetEmployeeIDNull()
                Me.EmployeeID.Text = ""
            End If
        Else
            If CurrentRow.IsEmployeeIDNull OrElse CurrentRow.EmployeeID <> NewEmployeeID Then
                CurrentRow.EmployeeID = NewEmployeeID
                Me.EmployeeID.Text = NewEmployeeID.ToString
            End If
        End If
        AdjustementBindingContext.EndCurrentEdit()
    End Sub
    Private Sub EmployeeID_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles EmployeeID.TextChanged
        If Not IsNothing(Me.AdjustementBindingContext) Then
            If Me.EmployeeID.Text.Length = 0 Then
                Me.EmployeeFormButton.Enabled = False
                Return
            End If
            Me.EmployeeIDComboBox.SelectedIndex = Me.EmployeeIDComboBox.Items.IndexOf(MyLookupDataset.Employee.FindByEmployeeID(CType(Me.EmployeeID.Text, Integer)).FullName)
            Me.EmployeeFormButton.Enabled = True
        End If
    End Sub
    Private Sub PostedDateComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PostedDateComboBox.SelectedIndexChanged
        If IsNothing(AdjustementBindingContext) OrElse AdjustementBindingContext.Position = -1 Then
            Return
        End If
        If CType(CType(AdjustementBindingContext.Current, Data.DataRowView).Row, AdjustmentsDataset.AdjustmentsRow).IsPostedDateNull Then
            Me.PostedDateComboBox.SelectedIndex = -1
        End If
    End Sub
    Private Sub PostedDateComboBox_SelectedIndexChangedCommited(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PostedDateComboBox.SelectionChangeCommitted
        If CType(CType(AdjustementBindingContext.Current, Data.DataRowView).Row, AdjustmentsDataset.AdjustmentsRow).IsPostedDateNull Then
            CType(CType(AdjustementBindingContext.Current, Data.DataRowView).Row, AdjustmentsDataset.AdjustmentsRow).PostedDate = CDate(Me.PostedDateComboBox.SelectedValue)
        Else
            If CType(CType(AdjustementBindingContext.Current, Data.DataRowView).Row, AdjustmentsDataset.AdjustmentsRow).PostedDate <> CDate(Me.PostedDateComboBox.SelectedValue) Then
                CType(CType(AdjustementBindingContext.Current, Data.DataRowView).Row, AdjustmentsDataset.AdjustmentsRow).PostedDate = CDate(Me.PostedDateComboBox.SelectedValue)
            End If
        End If
        AdjustementBindingContext.EndCurrentEdit()
    End Sub
    Private Sub YearComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles YearComboBox.SelectedIndexChanged
        If IsNothing(AdjustementBindingContext) OrElse AdjustementBindingContext.Position = -1 Then          
            Return
        End If
        If CType(CType(AdjustementBindingContext.Current, Data.DataRowView).Row, AdjustmentsDataset.AdjustmentsRow).IsYearNull Then
            Me.YearComboBox.SelectedIndex = -1
        End If
    End Sub
    Private Sub WeekComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles WeekComboBox.SelectedIndexChanged
        If IsNothing(AdjustementBindingContext) OrElse AdjustementBindingContext.Position = -1 Then
            Return
        End If
        If CType(CType(AdjustementBindingContext.Current, Data.DataRowView).Row, AdjustmentsDataset.AdjustmentsRow).IsWeekNull Then
            Me.WeekComboBox.SelectedIndex = -1
        End If
    End Sub
    Private Sub YearComboBox_SelectedIndexChangedCommited(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles YearComboBox.SelectionChangeCommitted
        If CType(CType(AdjustementBindingContext.Current, Data.DataRowView).Row, AdjustmentsDataset.AdjustmentsRow).IsYearNull Then
            CType(CType(AdjustementBindingContext.Current, Data.DataRowView).Row, AdjustmentsDataset.AdjustmentsRow).Year = CType(Me.YearComboBox.SelectedValue, Integer)
        Else
            If CType(CType(AdjustementBindingContext.Current, Data.DataRowView).Row, AdjustmentsDataset.AdjustmentsRow).Year <> CType(Me.YearComboBox.SelectedValue, Integer) Then
                CType(CType(AdjustementBindingContext.Current, Data.DataRowView).Row, AdjustmentsDataset.AdjustmentsRow).Year = CType(Me.YearComboBox.SelectedValue, Integer)
            End If
        End If
        AdjustementBindingContext.EndCurrentEdit()
    End Sub
    Private Sub WeekComboBox_SelectedIndexChangedCommited(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles WeekComboBox.SelectionChangeCommitted
        If CType(CType(AdjustementBindingContext.Current, Data.DataRowView).Row, AdjustmentsDataset.AdjustmentsRow).IsWeekNull Then
            CType(CType(AdjustementBindingContext.Current, Data.DataRowView).Row, AdjustmentsDataset.AdjustmentsRow).Week = CType(Me.WeekComboBox.SelectedValue, Integer)
        Else
            If CType(CType(AdjustementBindingContext.Current, Data.DataRowView).Row, AdjustmentsDataset.AdjustmentsRow).Week <> CType(Me.WeekComboBox.SelectedValue, Integer) Then
                CType(CType(AdjustementBindingContext.Current, Data.DataRowView).Row, AdjustmentsDataset.AdjustmentsRow).Week = CType(Me.WeekComboBox.SelectedValue, Integer)
            End If
        End If
        AdjustementBindingContext.EndCurrentEdit()
    End Sub
    Private Sub CTRCodeComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CTRCodeComboBox.SelectedIndexChanged
        If IsNothing(AdjustementBindingContext) OrElse AdjustementBindingContext.Position = -1 Then
            Return
        End If
        If CType(CType(AdjustementBindingContext.Current, Data.DataRowView).Row, AdjustmentsDataset.AdjustmentsRow).IsCTRCodeNull Then
            Me.CTRCodeComboBox.SelectedIndex = -1
        End If
    End Sub
    Private Sub CTRCodeComboBox_SelectedIndexChangedCommited(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CTRCodeComboBox.SelectionChangeCommitted
        If CType(CType(AdjustementBindingContext.Current, Data.DataRowView).Row, AdjustmentsDataset.AdjustmentsRow).IsCTRCodeNull Then
            CType(CType(AdjustementBindingContext.Current, Data.DataRowView).Row, AdjustmentsDataset.AdjustmentsRow).CTRCode = CStr(Me.CTRCodeComboBox.SelectedValue)
        Else
            If CType(CType(AdjustementBindingContext.Current, Data.DataRowView).Row, AdjustmentsDataset.AdjustmentsRow).CTRCode <> CStr(Me.CTRCodeComboBox.SelectedValue) Then
                CType(CType(AdjustementBindingContext.Current, Data.DataRowView).Row, AdjustmentsDataset.AdjustmentsRow).CTRCode = CStr(Me.CTRCodeComboBox.SelectedValue)
            End If
        End If
        AdjustementBindingContext.EndCurrentEdit()
    End Sub
    Private Sub LabourCostCurrencyComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LabourCostCurrencyComboBox.SelectedIndexChanged
        If IsNothing(AdjustementBindingContext) OrElse AdjustementBindingContext.Position = -1 Then
            Return
        End If
        If CType(CType(AdjustementBindingContext.Current, Data.DataRowView).Row, AdjustmentsDataset.AdjustmentsRow).IsRatePayCurrencyIDNull Then
            Me.LabourCostCurrencyComboBox.SelectedIndex = -1
        End If
    End Sub
    Private Sub LabourCostCurrencyComboBox_SelectedIndexChangedCommited(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LabourCostCurrencyComboBox.SelectionChangeCommitted
        If CType(CType(AdjustementBindingContext.Current, Data.DataRowView).Row, AdjustmentsDataset.AdjustmentsRow).IsRatePayCurrencyIDNull Then
            CType(CType(AdjustementBindingContext.Current, Data.DataRowView).Row, AdjustmentsDataset.AdjustmentsRow).RatePayCurrencyID = CType(Me.LabourCostCurrencyComboBox.SelectedValue, String)
        Else
            If CType(CType(AdjustementBindingContext.Current, Data.DataRowView).Row, AdjustmentsDataset.AdjustmentsRow).RatePayCurrencyID <> CType(Me.LabourCostCurrencyComboBox.SelectedValue, String) Then
                CType(CType(AdjustementBindingContext.Current, Data.DataRowView).Row, AdjustmentsDataset.AdjustmentsRow).RatePayCurrencyID = CType(Me.LabourCostCurrencyComboBox.SelectedValue, String)
            End If
        End If
        AdjustementBindingContext.EndCurrentEdit()
    End Sub

    Private Sub RatePayLocation_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RatePayLocation.SelectedIndexChanged
        If IsNothing(AdjustementBindingContext) OrElse AdjustementBindingContext.Position = -1 Then
            Return
        End If
        If CType(CType(AdjustementBindingContext.Current, Data.DataRowView).Row, AdjustmentsDataset.AdjustmentsRow).IsRatePayLocationNull Then
            Me.RatePayLocation.SelectedIndex = -1
        End If
    End Sub
    Private Sub RatePayLocation_SelectedIndexChangedCommited(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RatePayLocation.SelectionChangeCommitted
        If CType(CType(AdjustementBindingContext.Current, Data.DataRowView).Row, AdjustmentsDataset.AdjustmentsRow).IsRatePayLocationNull Then
            CType(CType(AdjustementBindingContext.Current, Data.DataRowView).Row, AdjustmentsDataset.AdjustmentsRow).RatePayLocation = CType(Me.RatePayLocation.SelectedValue, String)
        Else
            If CType(CType(AdjustementBindingContext.Current, Data.DataRowView).Row, AdjustmentsDataset.AdjustmentsRow).RatePayLocation <> CType(Me.RatePayLocation.SelectedValue, String) Then
                CType(CType(AdjustementBindingContext.Current, Data.DataRowView).Row, AdjustmentsDataset.AdjustmentsRow).RatePayLocation = CType(Me.RatePayLocation.SelectedValue, String)
            End If
        End If
        AdjustementBindingContext.EndCurrentEdit()
    End Sub
    Private Sub RateAssignLocation_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RateAssignLocation.SelectedIndexChanged
        If IsNothing(AdjustementBindingContext) OrElse AdjustementBindingContext.Position = -1 Then
            Return
        End If
        If CType(CType(AdjustementBindingContext.Current, Data.DataRowView).Row, AdjustmentsDataset.AdjustmentsRow).IsRateAssignLocationNull Then
            Me.RateAssignLocation.SelectedIndex = -1
        End If
    End Sub
    Private Sub RateAssignLocation_SelectedIndexChangedCommited(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RateAssignLocation.SelectionChangeCommitted
        If CType(CType(AdjustementBindingContext.Current, Data.DataRowView).Row, AdjustmentsDataset.AdjustmentsRow).IsRateAssignLocationNull Then
            CType(CType(AdjustementBindingContext.Current, Data.DataRowView).Row, AdjustmentsDataset.AdjustmentsRow).RateAssignLocation = CType(Me.RateAssignLocation.SelectedValue, String)
        Else
            If CType(CType(AdjustementBindingContext.Current, Data.DataRowView).Row, AdjustmentsDataset.AdjustmentsRow).RateAssignLocation <> CType(Me.RateAssignLocation.SelectedValue, String) Then
                CType(CType(AdjustementBindingContext.Current, Data.DataRowView).Row, AdjustmentsDataset.AdjustmentsRow).RateAssignLocation = CType(Me.RateAssignLocation.SelectedValue, String)
            End If
        End If
        AdjustementBindingContext.EndCurrentEdit()
    End Sub
    Private Sub NonLabourCostCurrencyComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NonLabourCostCurrencyComboBox.SelectedIndexChanged
        If IsNothing(AdjustementBindingContext) OrElse AdjustementBindingContext.Position = -1 Then
            Return
        End If
        If CType(CType(AdjustementBindingContext.Current, Data.DataRowView).Row, AdjustmentsDataset.AdjustmentsRow).IsRateAssignCurrencyIDNull Then
            Me.NonLabourCostCurrencyComboBox.SelectedIndex = -1
        End If
    End Sub
    Private Sub NonLabourCostCurrencyComboBox_SelectedIndexChangedCommited(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NonLabourCostCurrencyComboBox.SelectionChangeCommitted
        If CType(CType(AdjustementBindingContext.Current, Data.DataRowView).Row, AdjustmentsDataset.AdjustmentsRow).IsRateAssignCurrencyIDNull Then
            CType(CType(AdjustementBindingContext.Current, Data.DataRowView).Row, AdjustmentsDataset.AdjustmentsRow).RateAssignCurrencyID = CType(Me.NonLabourCostCurrencyComboBox.SelectedValue, String)
        Else
            If CType(CType(AdjustementBindingContext.Current, Data.DataRowView).Row, AdjustmentsDataset.AdjustmentsRow).RateAssignCurrencyID <> CType(Me.NonLabourCostCurrencyComboBox.SelectedValue, String) Then
                CType(CType(AdjustementBindingContext.Current, Data.DataRowView).Row, AdjustmentsDataset.AdjustmentsRow).RateAssignCurrencyID = CType(Me.NonLabourCostCurrencyComboBox.SelectedValue, String)
            End If
        End If
        AdjustementBindingContext.EndCurrentEdit()
    End Sub

    Private Function IsOKToCancelExistingChanges() As Boolean
        If Me.AdjustmentsDataset.HasChanges Then
            Dim response As MsgBoxResult
            response = MsgBox("Do you want to cancel your changes?", MsgBoxStyle.YesNo, "Please confirm")
            If response = MsgBoxResult.Yes Then
                Me.AdjustmentsDataset.RejectChanges()
                Return True
            Else
                Return False
            End If
        End If
        Return True
    End Function
    Private Sub MoneyTextBox_Formating(ByVal sender As Object, ByVal e As System.Windows.Forms.ConvertEventArgs) Handles LabourCostsBinding.Format, ITCoreCostsBinding.Format, ITCostsBinding.Format, OfficeExpensesCostsBinding.Format, OfficeSpaceCostsBinding.Format
        If e.Value Is System.DBNull.Value Then
            e.Value = "0.00"
            Return
        End If
        Dim d As Double = Double.Parse(CType(e.Value, String))
        e.Value = d.ToString("0.00")
    End Sub
    Private Sub NewButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NewButton.Click
        If CurrentRecordIsValid() Then
            AdjustementBindingContext.EndCurrentEdit()
            AdjustementBindingContext.AddNew()
            EnableControls(True)
            Me.EnableNavigationButtons()
            SynchronizeEmployeeList()
            CType(CType(AdjustementBindingContext.Current, Data.DataRowView).Row, AdjustmentsDataset.AdjustmentsRow).LabourCost = 0
            CType(CType(AdjustementBindingContext.Current, Data.DataRowView).Row, AdjustmentsDataset.AdjustmentsRow).OtherIT = 0
            CType(CType(AdjustementBindingContext.Current, Data.DataRowView).Row, AdjustmentsDataset.AdjustmentsRow).OtherITCore = 0
            CType(CType(AdjustementBindingContext.Current, Data.DataRowView).Row, AdjustmentsDataset.AdjustmentsRow).OtherOfficeExpenses = 0
            CType(CType(AdjustementBindingContext.Current, Data.DataRowView).Row, AdjustmentsDataset.AdjustmentsRow).OtherOfficeSpace = 0
        End If
    End Sub
    Private Sub CancelButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CancelButton.Click
        Me.Close()
    End Sub
    Private Sub DeleteButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteButton.Click
        CType(AdjustementBindingContext.Current, Data.DataRowView).Delete()
        If Me.BindingContext(AdjustmentsDataset, "Adjustments").Count = 0 Then
            EnableControls(False)
        End If
        EnableNavigationButtons()
    End Sub
    Private Sub SaveButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveButton.Click
        AdjustementBindingContext.EndCurrentEdit()
        If CurrentRecordIsValid() Then
            Try
                Try
                    'Me.AdjustmentsDataset.GetChanges() ' for debugging only
                    Me.AdjustmentsSqlDataAdapter.Update(Me.AdjustmentsDataset)
                Catch ex As DBConcurrencyException
                    Dim customErrorMessage As String
                    customErrorMessage = "Concurrency violation" & vbCrLf
                    customErrorMessage += CType(ex.Row.Item(0), String)
                    MessageBox.Show(customErrorMessage)
                    ' Replace the above code with appropriate business logic
                    ' to resolve the concurrency violation.
                End Try

                'Me.AdjustmentsDataset.AcceptChanges()
                Dim position As Integer
                position = Me.AdjustementBindingContext.Position
                Me.AdjustementBindingContext.SuspendBinding()
                LoadAdjustments()

                Me.AdjustementBindingContext.ResumeBinding()
                Me.AdjustementBindingContext.Position = position
                ' reload the information to get the entryid

            Catch ex As SqlClient.SqlException
                If ex.Message.Contains("FK_Hours_YearWeek") Then
                    MsgBox("The Year/Week must correspond to an available week in TimeSheet!", MsgBoxStyle.Critical, "SQL Error")
                Else
                    MsgBox(ex.Message, MsgBoxStyle.Critical, "SQL Error")
                End If
            End Try
        End If
    End Sub
    Private Function CurrentRecordIsValid() As Boolean
        If AdjustementBindingContext.Count = 0 Then
            Return True
        End If
        AdjustementBindingContext.EndCurrentEdit()
        Dim CurrentRow As AdjustmentsDataset.AdjustmentsRow = CType(CType(AdjustementBindingContext.Current, DataRowView).Row, AdjustmentsDataset.AdjustmentsRow)
        If IsNothing(CurrentRow) OrElse CurrentRow.RowState = DataRowState.Unchanged Then
            Return True
        End If
        If CurrentRow.IsCompanyIDNull Then
            MsgBox("A company must be selected!", MsgBoxStyle.Exclamation, "Information")
            Me.CompanyIDComboBox.Focus()
            Return False
        End If
        If CurrentRow.IsEmployeeIDNull Then
            MsgBox("An employee must be selected!", MsgBoxStyle.Exclamation, "Information")
            Me.EmployeeIDComboBox.Focus()
            Return False
        End If
        If CurrentRow.IsContractNull Then
            MsgBox("A contract must be selected!", MsgBoxStyle.Exclamation, "Information")
            Me.ContractComboBox.Focus()
            Return False
        End If
        If CurrentRow.IsDisciplineNull Then
            MsgBox("A Discipline must be selected!", MsgBoxStyle.Exclamation, "Information")
            Me.DisciplineAdjComboBox.Focus()
            Return False
        End If
        If CurrentRow.IsPostedDateNull Then
            MsgBox("A posted date must be selected!", MsgBoxStyle.Exclamation, "Information")
            Me.PostedDateComboBox.Focus()
            Return False
        End If
        If CDate(CurrentRow.PostedDate) <= Now Then
            If CurrentRow.RowState <> DataRowState.Modified OrElse Not OnlyCommentsFiledChanged(CurrentRow) Then
                MsgBox("The posted date must not be in the past!", MsgBoxStyle.Exclamation, "Information")
                Me.PostedDateComboBox.Focus()
                Return False
            End If
        End If
        If CurrentRow.IsYearNull Then
            MsgBox("A Year must be selected!", MsgBoxStyle.Exclamation, "Information")
            Me.YearComboBox.Focus()
            Return False
        End If
        If CurrentRow.IsWeekNull Then
            MsgBox("A Week must be selected!", MsgBoxStyle.Exclamation, "Information")
            Me.WeekComboBox.Focus()
            Return False
        End If
        If CurrentRow.IsCTRCodeNull Then
            MsgBox("A CTRCode must be selected!", MsgBoxStyle.Exclamation, "Information")
            Me.CTRCodeComboBox.Focus()
            Return False
        End If
        If CurrentRow.IsRatePayCurrencyIDNull Then
            MsgBox("A rate for labour cost must be selected!", MsgBoxStyle.Exclamation, "Information")
            Me.LabourCostCurrencyComboBox.Focus()
            Return False
        End If
        If CurrentRow.IsRatePayLocationNull Then
            MsgBox("A location for labour cost must be selected!", MsgBoxStyle.Exclamation, "Information")
            Me.RatePayLocation.Focus()
            Return False
        End If
        If CurrentRow.IsRateAssignCurrencyIDNull Then
            MsgBox("A rate for non-labour costs must be selected!", MsgBoxStyle.Exclamation, "Information")
            Me.NonLabourCostCurrencyComboBox.Focus()
            Return False
        End If
        If CurrentRow.IsRateAssignLocationNull Then
            MsgBox("A location for non-labour costs must be selected!", MsgBoxStyle.Exclamation, "Information")
            Me.RateAssignLocation.Focus()
            Return False
        End If
        If CurrentRow.IsAdjustmentShortCommentNull Then
            MsgBox("A short comment must be entered!", MsgBoxStyle.Exclamation, "Information")
            Me.ShortCommentComboBox.Focus()
            Return False
        End If
        If CurrentRow.IsAdjustmentLongCommentNull Then
            MsgBox("A long comment must be entered!", MsgBoxStyle.Exclamation, "Information")
            Me.LongCommentTextBox.Focus()
            Return False
        End If
        Return True
    End Function
    Private Function OnlyCommentsFiledChanged(ByVal CurrentRow As AdjustmentsDataset.AdjustmentsRow) As Boolean
        ' this function verify every fields for changes, 
        ' the only fields where changes are accepted are the two comments fields
        Dim i As Integer
        For i = 0 To AdjustmentsDataset.Adjustments.Columns.Count - 1
            If CurrentRow(i).ToString <> CurrentRow(i, DataRowVersion.Original).ToString Then
                If AdjustmentsDataset.Adjustments.Columns(i).ColumnName <> AdjustmentsDataset.Adjustments.AdjustmentShortCommentColumn.ColumnName _
                    And AdjustmentsDataset.Adjustments.Columns(i).ColumnName <> AdjustmentsDataset.Adjustments.AdjustmentLongCommentColumn.ColumnName Then
                    Return False
                End If
            End If
        Next
        Return True
    End Function

    Private Sub EnableControls(ByVal Enabled As Boolean)
        Me.CompanyIDComboBox.Enabled = Enabled
        Me.EmployeeIDComboBox.Enabled = Enabled
        Me.PostedDateComboBox.Enabled = Enabled
        Me.YearComboBox.Enabled = Enabled
        Me.WeekComboBox.Enabled = Enabled
        Me.LabourCostCurrencyComboBox.Enabled = Enabled
        Me.LabourCostTextBox.Enabled = Enabled
        Me.NonLabourCostCurrencyComboBox.Enabled = Enabled
        Me.OfficeSpaceTextBox.Enabled = Enabled
        Me.OfficeExpensesTextBox.Enabled = Enabled
        Me.ITCostsTextBox.Enabled = Enabled
        Me.ITCoreCostsTextBox.Enabled = Enabled
        Me.LongCommentTextBox.Enabled = Enabled
        Me.DeleteButton.Enabled = Enabled
        If Not Enabled Then
            Me.EmployeeIDComboBox.Items.Clear()
        End If
    End Sub

    Private Sub ShortCommentComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ShortCommentComboBox.SelectedIndexChanged
        If IsNothing(AdjustementBindingContext) OrElse AdjustementBindingContext.Position = -1 Then
            Return
        End If
        If CType(CType(AdjustementBindingContext.Current, Data.DataRowView).Row, AdjustmentsDataset.AdjustmentsRow).IsAdjustmentShortCommentNull Then
            Me.ShortCommentComboBox.SelectedIndex = -1
        End If
    End Sub
    Private Sub ShortCommentComboBox_SelectedIndexChangedCommited(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ShortCommentComboBox.SelectionChangeCommitted
        ' Return

        Dim CurrentRow As AdjustmentsDataset.AdjustmentsRow
        CurrentRow = CType(CType(AdjustementBindingContext.Current, DataRowView).Row, AdjustmentsDataset.AdjustmentsRow)
        If CurrentRow.IsAdjustmentShortCommentNull Then
            CurrentRow.AdjustmentShortComment = Me.ShortCommentComboBox.Text
        Else
            If CurrentRow.AdjustmentShortComment <> Me.ShortCommentComboBox.Text Then
                CurrentRow.AdjustmentShortComment = Me.ShortCommentComboBox.Text
            End If
        End If
        AdjustementBindingContext.EndCurrentEdit()
    End Sub
    Private Sub ShortCommentComboBoxLeaveHandler(ByVal sender As Object, ByVal e As EventArgs) Handles ShortCommentComboBox.Leave
        If CType(CType(AdjustementBindingContext.Current, Data.DataRowView).Row, AdjustmentsDataset.AdjustmentsRow).IsAdjustmentShortCommentNull OrElse _
           CType(CType(AdjustementBindingContext.Current, Data.DataRowView).Row, AdjustmentsDataset.AdjustmentsRow).AdjustmentShortComment <> Me.ShortCommentComboBox.Text Then
            CType(CType(AdjustementBindingContext.Current, Data.DataRowView).Row, AdjustmentsDataset.AdjustmentsRow).AdjustmentShortComment = Me.ShortCommentComboBox.Text
        End If
    End Sub

    Private Sub EmployeeFormButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EmployeeFormButton.Click
        Dim f As New EmployeeForm(Me.EmployeeID.Text)
        f.ShowDialog()
        f.Dispose()
    End Sub

End Class
Public Interface ColumnStyleInterface
    ReadOnly Property ColumnID() As Integer
    ReadOnly Property FilterActive() As Boolean
    Property FilterString() As String
    ReadOnly Property IsReadOnly() As Boolean
    Property Sorted() As Boolean
    Sub ClearFilter()
End Interface