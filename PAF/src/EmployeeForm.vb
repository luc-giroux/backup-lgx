Imports System.Data
Imports System.Data.SqlClient

Public Class EmployeeForm
    Inherits System.Windows.Forms.Form

    'Global parameters
    Private ValidWeeksDataTable As EmployeeList.CalendarDataTable
    Private YesSelected As Boolean
    Private hitTestGrid As DataGrid.HitTestInfo
    Private columnToDelete As String = ""
    Private WithEvents dtp As New DateTimePicker
    Friend WithEvents Email As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents PAFRevisionNo As System.Windows.Forms.TextBox
    Friend WithEvents Login As System.Windows.Forms.Label
    Friend WithEvents TextBoxLogin As System.Windows.Forms.TextBox
    Friend WithEvents LabelActive As System.Windows.Forms.Label
    Friend WithEvents CheckBoxActive As System.Windows.Forms.CheckBox
    Friend WithEvents ComboBoxManager As System.Windows.Forms.ComboBox
    Friend WithEvents NewLookupDataset As PAF.NewLookupDataset
    Friend WithEvents CompanyManagerBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents CompanyManagerTableAdapter As PAF.NewLookupDatasetTableAdapters.CompanyManagerTableAdapter
    Friend WithEvents DisciplineBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents DisciplineTableAdapter As PAF.NewLookupDatasetTableAdapters.DisciplineTableAdapter
    Friend WithEvents EffectiveDate As System.Windows.Forms.DateTimePicker

    Private Sub InitializeDataset()

        Me.SqlSelectCommand = New System.Data.SqlClient.SqlCommand
        Me.SqlInsertCommand = New System.Data.SqlClient.SqlCommand
        Me.SqlUpdateCommand = New System.Data.SqlClient.SqlCommand
        Me.EmployeeSqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter
        Me.EmployeeListDataSet = New PAF.EmployeeList
        'Main Employee dataset initial load routine
        CType(Me.EmployeeListDataSet, System.ComponentModel.ISupportInitialize).BeginInit()

        'Setup datatgrid table column formats
        Dim MyTableStyle As New DataGridTableStyle
        MyTableStyle.MappingName = Me.EmployeeListDataSet.Employee.TableName
        MyTableStyle = MyDataTableStyle()
        Dim MyDG1TableStyle As New DataGridTableStyle
        MyDG1TableStyle.MappingName = Me.EmployeeListDataSet.Employee.TableName
        MyDG1TableStyle = MyDataTableStyle()
        Dim MyDG2TableStyle As New DataGridTableStyle
        MyDG2TableStyle.MappingName = Me.EmployeeListDataSet.Employee.TableName
        MyDG2TableStyle = MyDataTableStyle()
        Dim MyDG3TableStyle As New DataGridTableStyle
        MyDG3TableStyle.MappingName = Me.EmployeeListDataSet.Employee.TableName
        MyDG3TableStyle = MyDataTableStyle()
        ''Original TableStyle code goes here

        'Assign datagrid column styles to datagrids
        Me.EmployeeDataGrid.TableStyles.Add(MyTableStyle)
        Me.DataGrid1.TableStyles.Add(MyDG1TableStyle)
        Me.DataGrid2.TableStyles.Add(MyDG2TableStyle)
        Me.DataGrid3.TableStyles.Add(MyDG3TableStyle)

        'SqlSelectCommand
        '
        Me.SqlSelectCommand.CommandText = "SELECT EmployeeID, CompanyID, CompanyEmployeeNo, DisciplineCode, Employee.LastName, Employee.FirstName, Email, TotalHoursImported, CompanyManager.CompanyManagerID,CompanyManager.FirstName + ', ' + CompanyManager.LastName AS CompanyManagerName, Comments, PAFNo, PAFRevisionNo, EffectiveDate, StartDate, RatePayLocation, RatePayCurrencyID, EHAHourlyRate, RateAssignLocation, RateAssignCurrencyID, ActualDemobilisationDate, EmployeeLogin, IsNull(EmployeeActive, 'False') as EmployeeActive FROM dbo.Employee Inner join CompanyManager on Employee.CompanyManagerID = CompanyManager.CompanyManagerID"
        Me.SqlSelectCommand.Connection = PrivilegedSQLConnection
        '
        'SqlInsertCommand
        '
        Me.SqlInsertCommand.CommandType = CommandType.StoredProcedure
        Me.SqlInsertCommand.Connection = PrivilegedSQLConnection
        Me.SqlInsertCommand.CommandText = _
            "spEmployeeInsert"
        Me.SqlInsertCommand.Parameters.Add( _
            New System.Data.SqlClient.SqlParameter("@CompanyID", _
                                                    System.Data.SqlDbType.VarChar, 3, "CompanyID"))
        Me.SqlInsertCommand.Parameters.Add( _
            New System.Data.SqlClient.SqlParameter("@CompanyEmployeeNo", _
                                                    System.Data.SqlDbType.VarChar, 20, "CompanyEmployeeNo"))
        Me.SqlInsertCommand.Parameters.Add( _
            New System.Data.SqlClient.SqlParameter("@DisciplineCode", _
                                                    System.Data.SqlDbType.VarChar, 4, "DisciplineCode"))
        Me.SqlInsertCommand.Parameters.Add( _
            New System.Data.SqlClient.SqlParameter("@LastName", _
                                                    System.Data.SqlDbType.VarChar, 50, "LastName"))
        Me.SqlInsertCommand.Parameters.Add( _
            New System.Data.SqlClient.SqlParameter("@FirstName", _
                                                    System.Data.SqlDbType.VarChar, 50, "FirstName"))
        Me.SqlInsertCommand.Parameters.Add( _
            New System.Data.SqlClient.SqlParameter("@Email", _
                                                    System.Data.SqlDbType.VarChar, 50, "Email"))
        Me.SqlInsertCommand.Parameters.Add( _
            New System.Data.SqlClient.SqlParameter("@TotalHoursImported", _
                                                    System.Data.SqlDbType.Float, 8, "TotalHoursImported"))
        Me.SqlInsertCommand.Parameters.Add( _
            New System.Data.SqlClient.SqlParameter("@CompanyManagerID", _
                                                    System.Data.SqlDbType.Int, 50, "CompanyManagerID"))
        Me.SqlInsertCommand.Parameters.Add( _
            New System.Data.SqlClient.SqlParameter("@Comments", _
                                                    System.Data.SqlDbType.VarChar, 1024, _
                                                    "Comments"))
        Me.SqlInsertCommand.Parameters.Add( _
            New System.Data.SqlClient.SqlParameter("@PAFNo", _
                                                    System.Data.SqlDbType.VarChar, 50, "PAFNo"))
        Me.SqlInsertCommand.Parameters.Add( _
            New System.Data.SqlClient.SqlParameter("@PAFRevisionNo", _
                                                    System.Data.SqlDbType.VarChar, 50, "PAFRevisionNo"))
        Me.SqlInsertCommand.Parameters.Add( _
            New System.Data.SqlClient.SqlParameter("@EffectiveDate", _
                                                    System.Data.SqlDbType.VarChar, 50, "EffectiveDate"))
        Me.SqlInsertCommand.Parameters.Add( _
            New System.Data.SqlClient.SqlParameter("@StartDate", _
                                                    System.Data.SqlDbType.DateTime, 8, "StartDate"))
        Me.SqlInsertCommand.Parameters.Add( _
            New System.Data.SqlClient.SqlParameter("@RatePayLocation", _
                                                    System.Data.SqlDbType.VarChar, 3, "RatePayLocation"))
        Me.SqlInsertCommand.Parameters.Add( _
            New System.Data.SqlClient.SqlParameter("@RatePayCurrencyID", _
                                                    System.Data.SqlDbType.VarChar, 4, "RatePayCurrencyID"))
        Me.SqlInsertCommand.Parameters.Add( _
            New System.Data.SqlClient.SqlParameter("@EHAHourlyRate", _
                                                    System.Data.SqlDbType.Decimal, 9, _
                                                    System.Data.ParameterDirection.Input, False, _
                                                    CType(18, Byte), CType(2, Byte), _
                                                    "EHAHourlyRate", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand.Parameters.Add( _
            New System.Data.SqlClient.SqlParameter("@RateAssignLocation", _
                                                    System.Data.SqlDbType.VarChar, 3, "RateAssignLocation"))
        Me.SqlInsertCommand.Parameters.Add( _
            New System.Data.SqlClient.SqlParameter("@RateAssignCurrencyID", _
                                                    System.Data.SqlDbType.VarChar, 4, "RateAssignCurrencyID"))
        Me.SqlInsertCommand.Parameters.Add( _
            New System.Data.SqlClient.SqlParameter("@ActualDemobilisationDate", _
                                                    System.Data.SqlDbType.DateTime, 8, "ActualDemobilisationDate"))
        Me.SqlInsertCommand.Parameters.Add( _
            New System.Data.SqlClient.SqlParameter("@EmployeeLogin", _
                                                    System.Data.SqlDbType.VarChar, 40, "EmployeeLogin"))
        Me.SqlInsertCommand.Parameters.Add( _
            New System.Data.SqlClient.SqlParameter("@EmployeeActive", _
                                                    System.Data.SqlDbType.Bit, 1, "EmployeeActive"))
        '
        'SqlUpdateCommand
        '
        Me.SqlUpdateCommand.CommandType = CommandType.StoredProcedure
        Me.SqlUpdateCommand.Connection = PrivilegedSQLConnection
        Me.SqlUpdateCommand.CommandText = _
            "spEmployeeUpdate"
        Me.SqlUpdateCommand.Parameters.Add( _
            New System.Data.SqlClient.SqlParameter("@EmployeeID", _
                                                    System.Data.SqlDbType.Int, 4, "EmployeeID"))
        Me.SqlUpdateCommand.Parameters.Add( _
            New System.Data.SqlClient.SqlParameter("@CompanyID", _
                                                    System.Data.SqlDbType.VarChar, 3, "CompanyID"))
        Me.SqlUpdateCommand.Parameters.Add( _
            New System.Data.SqlClient.SqlParameter("@CompanyEmployeeNo", _
                                                    System.Data.SqlDbType.VarChar, 20, "CompanyEmployeeNo"))
        Me.SqlUpdateCommand.Parameters.Add( _
            New System.Data.SqlClient.SqlParameter("@DisciplineCode", _
                                                    System.Data.SqlDbType.VarChar, 4, "DisciplineCode"))
        Me.SqlUpdateCommand.Parameters.Add( _
            New System.Data.SqlClient.SqlParameter("@LastName", _
                                                    System.Data.SqlDbType.VarChar, 50, "LastName"))
        Me.SqlUpdateCommand.Parameters.Add( _
            New System.Data.SqlClient.SqlParameter("@FirstName", _
                                                    System.Data.SqlDbType.VarChar, 50, "FirstName"))
        Me.SqlUpdateCommand.Parameters.Add( _
            New System.Data.SqlClient.SqlParameter("@Email", _
                                                    System.Data.SqlDbType.VarChar, 50, "Email"))
        Me.SqlUpdateCommand.Parameters.Add( _
            New System.Data.SqlClient.SqlParameter("@TotalHoursImported", _
                                                    System.Data.SqlDbType.Float, 8, "TotalHoursImported"))
        Me.SqlUpdateCommand.Parameters.Add( _
            New System.Data.SqlClient.SqlParameter("@CompanyManagerID", _
                                                    System.Data.SqlDbType.Int, 50, "CompanyManagerID"))
        Me.SqlUpdateCommand.Parameters.Add( _
            New System.Data.SqlClient.SqlParameter("@Comments", _
                                                    System.Data.SqlDbType.VarChar, 1024, _
                                                    "Comments"))
        Me.SqlUpdateCommand.Parameters.Add( _
            New System.Data.SqlClient.SqlParameter("@PAFNo", _
                                                    System.Data.SqlDbType.VarChar, 50, "PAFNo"))
        Me.SqlUpdateCommand.Parameters.Add( _
            New System.Data.SqlClient.SqlParameter("@PAFRevisionNo", _
                                                    System.Data.SqlDbType.VarChar, 50, "PAFRevisionNo"))
        Me.SqlUpdateCommand.Parameters.Add( _
            New System.Data.SqlClient.SqlParameter("@EffectiveDate", _
                                                    System.Data.SqlDbType.DateTime, 50, "EffectiveDate"))
        Me.SqlUpdateCommand.Parameters.Add( _
            New System.Data.SqlClient.SqlParameter("@StartDate", _
                                                    System.Data.SqlDbType.DateTime, 8, "StartDate"))
        Me.SqlUpdateCommand.Parameters.Add( _
            New System.Data.SqlClient.SqlParameter("@RatePayLocation", _
                                                    System.Data.SqlDbType.VarChar, 3, "RatePayLocation"))
        Me.SqlUpdateCommand.Parameters.Add( _
            New System.Data.SqlClient.SqlParameter("@RatePayCurrencyID", _
                                                    System.Data.SqlDbType.VarChar, 4, "RatePayCurrencyID"))
        Me.SqlUpdateCommand.Parameters.Add( _
            New System.Data.SqlClient.SqlParameter("@EHAHourlyRate", _
                                                    System.Data.SqlDbType.Decimal, 9, _
                                                    System.Data.ParameterDirection.Input, False, _
                                                    CType(18, Byte), CType(2, Byte), _
                                                    "EHAHourlyRate", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand.Parameters.Add( _
            New System.Data.SqlClient.SqlParameter("@RateAssignLocation", _
                                                    System.Data.SqlDbType.VarChar, 3, "RateAssignLocation"))
        Me.SqlUpdateCommand.Parameters.Add( _
            New System.Data.SqlClient.SqlParameter("@RateAssignCurrencyID", _
                                                    System.Data.SqlDbType.VarChar, 4, "RateAssignCurrencyID"))
        Me.SqlUpdateCommand.Parameters.Add( _
            New System.Data.SqlClient.SqlParameter("@ActualDemobilisationDate", _
                                                    System.Data.SqlDbType.DateTime, 8, "ActualDemobilisationDate"))

        Me.SqlUpdateCommand.Parameters.Add( _
            New System.Data.SqlClient.SqlParameter("@EmployeeLogin", _
                                                    System.Data.SqlDbType.VarChar, 40, "EmployeeLogin"))
        Me.SqlUpdateCommand.Parameters.Add( _
            New System.Data.SqlClient.SqlParameter("@EmployeeActive", _
                                                    System.Data.SqlDbType.Bit, 1, "EmployeeActive"))
        '
        'EmployeeListSqlDataAdapter
        '
        Me.EmployeeSqlDataAdapter.InsertCommand = Me.SqlInsertCommand
        Me.EmployeeSqlDataAdapter.SelectCommand = Me.SqlSelectCommand
        Me.EmployeeSqlDataAdapter.UpdateCommand = Me.SqlUpdateCommand

        'Database to dataset mappings        
        Me.EmployeeSqlDataAdapter.TableMappings.AddRange( _
        New System.Data.Common.DataTableMapping() { _
        New System.Data.Common.DataTableMapping("Table", "Employee", New System.Data.Common.DataColumnMapping() { _
            New System.Data.Common.DataColumnMapping("EmployeeID", "EmployeeID"), _
            New System.Data.Common.DataColumnMapping("CompanyID", "CompanyID"), _
            New System.Data.Common.DataColumnMapping("CompanyEmployeeNo", "CompanyEmployeeNo"), _
            New System.Data.Common.DataColumnMapping("DisciplineCode", "DisciplineCode"), _
            New System.Data.Common.DataColumnMapping("LastName", "LastName"), _
            New System.Data.Common.DataColumnMapping("FirstName", "FirstName"), _
            New System.Data.Common.DataColumnMapping("Email", "Email"), _
            New System.Data.Common.DataColumnMapping("TotalHoursImported", "TotalHoursImported"), _
            New System.Data.Common.DataColumnMapping("CompanyManagerID", "CompanyManagerID"), _
            New System.Data.Common.DataColumnMapping("CompanyManagerName", "CompanyManagerName"), _
            New System.Data.Common.DataColumnMapping("Comments", "Comments"), _
            New System.Data.Common.DataColumnMapping("PAFNo", "PAFNo"), _
            New System.Data.Common.DataColumnMapping("PAFREvisionNo", "PAFRevisionNo"), _
            New System.Data.Common.DataColumnMapping("EffectiveDate", "EffectiveDate"), _
            New System.Data.Common.DataColumnMapping("StartDate", "StartDate"), _
            New System.Data.Common.DataColumnMapping("RatePayLocation", "RatePayLocation"), _
            New System.Data.Common.DataColumnMapping("RatePayCurrencyID", "RatePayCurrencyID"), _
            New System.Data.Common.DataColumnMapping("EHAHourlyRate", "EHAHourlyRate"), _
            New System.Data.Common.DataColumnMapping("RateAssignLocation", "RateAssignLocation"), _
            New System.Data.Common.DataColumnMapping("RateAssignCurrencyID", "RateAssignCurrencyID"), _
            New System.Data.Common.DataColumnMapping("ActualDemobilisationDate", "ActualDemobilisationDate"), _
            New System.Data.Common.DataColumnMapping("EmployeeLogin", "EmployeeLogin"), _
            New System.Data.Common.DataColumnMapping("EmployeeActive", "EmployeeActive")})})

        'EmployeeListDataset1
        '
        Me.EmployeeListDataSet.DataSetName = "EmployeeList"
        Me.EmployeeListDataSet.Locale = System.Globalization.CultureInfo.CurrentCulture

        'Main Employee datagrid converted to dataview for manipulation capabilities
        DataView = New DataView(Me.EmployeeListDataSet.Employee)
        DataView.AllowDelete = False
        DataView.AllowEdit = False
        DataView.AllowNew = False
        DataView.RowStateFilter = DataViewRowState.CurrentRows

        'Modififed employee rows datagrid converted to dataview for manipulation capabilities
        dvModifiedRows = New DataView(Me.EmployeeListDataSet.Employee)
        dvModifiedRows.RowStateFilter = DataViewRowState.ModifiedCurrent
        Me.DataGrid1.DataSource = dvModifiedRows

        'Original employee rows datagrid converted to dataview for manipulation capabilities
        dvOriginalRows = New DataView(Me.EmployeeListDataSet.Employee)
        dvOriginalRows.RowStateFilter = DataViewRowState.OriginalRows
        Me.DataGrid2.DataSource = dvOriginalRows

        'Original employee rows datagrid converted to dataview for implanted datepicker in employee datagrid comparison
        dvDTPHelper = New DataView(Me.EmployeeListDataSet.Employee)
        dvDTPHelper.RowStateFilter = DataViewRowState.OriginalRows
        Me.DataGrid3.DataSource = dvDTPHelper

        'Assign Main dataview to Main datagrid
        Me.EmployeeDataGrid.DataSource = DataView

        'Finish Main employee dataset initial load routine
        CType(Me.EmployeeListDataSet, System.ComponentModel.ISupportInitialize).EndInit()
    End Sub

#Region " Windows Form Designer generated code "

    Public Sub New(ByVal EmployeeID As String)
        Me.New()
        Me.EmployeeIDFilter.Text = EmployeeID
        Me.FilterButton_Click(Nothing, Nothing)
    End Sub
    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        'Create main connection to database
        PrivilegedSQLConnection = GetSqlConnection()
        Me.InitializeDataset()

        'Minor datasets from "MainForm.vb" to dropdown lists mappings
        Me.Company.DataSource = MyLookupDataset.Company
        Me.Company.ValueMember = MyLookupDataset.Company.CompanyIDColumn.ColumnName
        Me.Company.DisplayMember = MyLookupDataset.Company.CompanyNameColumn.ColumnName

        Me.PayLocation.DataSource = MyLookupDataset.Location
        Me.PayLocation.ValueMember = MyLookupDataset.Location.LocationIDColumn.ColumnName
        Me.PayLocation.DisplayMember = MyLookupDataset.Location.LocationNameColumn.ColumnName

        Me.AssignedLocation.DataSource = MyLookupDataset.AssignedLocation
        Me.AssignedLocation.ValueMember = MyLookupDataset.AssignedLocation.LocationIDColumn.ColumnName
        Me.AssignedLocation.DisplayMember = MyLookupDataset.AssignedLocation.LocationNameColumn.ColumnName

        'Bind minor datsets to dropdown lists and display Main employee dataset data
        Me.Company.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.DataView, "CompanyID"))
        Me.ComboBoxDiscipline.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.DataView, "DisciplineCode"))
        Me.ComboBoxManager.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.DataView, "CompanyManagerID"))
        Me.PayLocation.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.DataView, "RatePayLocation"))
        Me.AssignedLocation.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.DataView, "RateAssignLocation"))
        Me.RatePayCurrency.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DataView, "RatePayCurrencyID"))
        Me.RateAssignCurrency.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DataView, "RateAssignCurrencyID"))

        'Bind Main employee dataset data to textbox objects
        Me.StartDate.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DataView, "StartDate"))
        Me.Actual.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DataView, "ActualDemobilisationDate"))

        Me.EmployeeID.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DataView, "EmployeeID"))
        Me.LastName.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DataView, "LastName"))
        Me.FirstName.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DataView, "FirstName"))
        Me.Email.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DataView, "Email"))
        Me.EmployeeNo.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DataView, "CompanyEmployeeNo"))
        Me.FormNo.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DataView, "PAFNo"))
        Me.PAFRevisionNo.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DataView, "PAFRevisionNo"))
        Me.EffectiveDate.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DataView, "EffectiveDate"))
        Me.EHA.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DataView, "EHAHourlyRate"))
        Me.TotalHoursImported.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DataView, "TotalHoursImported"))
        Me.Comments.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DataView, "Comments"))
        Me.TextBoxLogin.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DataView, "EmployeeLogin"))
        Me.CheckBoxActive.DataBindings.Add(New System.Windows.Forms.Binding("Checked", Me.DataView, "EmployeeActive"))


        'Assign Binding Manager to current row in Main employee dataview
        EmployeeBindingContext = Me.BindingContext(Me.DataView)

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
    Friend WithEvents EmployeeSqlDataAdapter As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents SaveRecord As System.Windows.Forms.Button
    Friend WithEvents NewRecord As System.Windows.Forms.Button
    Friend WithEvents DownRecord As System.Windows.Forms.Button
    Friend WithEvents UpRecord As System.Windows.Forms.Button
    Friend WithEvents Records As System.Windows.Forms.Label
    Friend WithEvents EmployeeBindingContext As BindingManagerBase
    Friend WithEvents TopRecord As System.Windows.Forms.Button
    Friend WithEvents BottomRecord As System.Windows.Forms.Button
    Friend WithEvents DataView As DataView
    Friend WithEvents dvModifiedRows As DataView
    Friend WithEvents dvOriginalRows As DataView
    Friend WithEvents dvDTPHelper As DataView
    Friend WithEvents GroupBox7 As System.Windows.Forms.GroupBox
    Friend WithEvents Label40 As System.Windows.Forms.Label
    Friend WithEvents Label41 As System.Windows.Forms.Label
    Friend WithEvents Label42 As System.Windows.Forms.Label
    Friend WithEvents LastNameFilter As System.Windows.Forms.TextBox
    Friend WithEvents EmployeeIDFilter As System.Windows.Forms.TextBox
    Friend WithEvents CompanyIDFilter As System.Windows.Forms.TextBox
    Friend WithEvents FilterChoice1 As System.Windows.Forms.ComboBox
    Friend WithEvents Choice1 As System.Windows.Forms.TextBox
    Friend WithEvents FilterButton As System.Windows.Forms.Button
    Friend WithEvents RemoveFilter As System.Windows.Forms.Button
    Friend WithEvents DataGrid1 As System.Windows.Forms.DataGrid
    Friend WithEvents DataGrid2 As System.Windows.Forms.DataGrid
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents Label39 As System.Windows.Forms.Label
    Friend WithEvents Comments As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox6 As System.Windows.Forms.GroupBox
    Friend WithEvents RateAssignCurrency As System.Windows.Forms.TextBox
    Friend WithEvents RatePayCurrency As System.Windows.Forms.TextBox
    Friend WithEvents TotalHoursImported As System.Windows.Forms.TextBox
    Friend WithEvents Label38 As System.Windows.Forms.Label
    Friend WithEvents Label36 As System.Windows.Forms.Label
    Friend WithEvents EHA As System.Windows.Forms.TextBox
    Friend WithEvents Label34 As System.Windows.Forms.Label
    Friend WithEvents AssignedLocation As System.Windows.Forms.ComboBox
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents PayLocation As System.Windows.Forms.ComboBox
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents FormNo As System.Windows.Forms.TextBox
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents Actual As System.Windows.Forms.DateTimePicker
    Friend WithEvents StartDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents ComboBoxDiscipline As System.Windows.Forms.ComboBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents EmployeeNo As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents FirstName As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents LastName As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Company As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents EmployeeID As System.Windows.Forms.TextBox
    Friend WithEvents EmployeeDataGrid As System.Windows.Forms.DataGrid
    Friend WithEvents TabControl As System.Windows.Forms.TabControl
    Friend ChangedRecordPosition As Integer
    Friend CheckCurrentRow As String
    Friend WithEvents DataGrid3 As System.Windows.Forms.DataGrid
    Friend WithEvents EmployeeListDataSet As PAF.EmployeeList
    Friend WithEvents GroupBox9 As System.Windows.Forms.GroupBox
    Friend WithEvents Label48 As System.Windows.Forms.Label
    Friend WithEvents Label49 As System.Windows.Forms.Label
    Friend WithEvents Label50 As System.Windows.Forms.Label
    Friend WithEvents Label51 As System.Windows.Forms.Label
    Friend WithEvents cbLastNameASC As System.Windows.Forms.CheckBox
    Friend WithEvents SortChoice As System.Windows.Forms.ComboBox
    Friend WithEvents Label52 As System.Windows.Forms.Label
    Friend WithEvents Label53 As System.Windows.Forms.Label
    Friend WithEvents cbLastNameDESC As System.Windows.Forms.CheckBox
    Friend WithEvents cbAssignedLocDESC As System.Windows.Forms.CheckBox
    Friend WithEvents cbAssignedLocASC As System.Windows.Forms.CheckBox
    Friend WithEvents cbCompanyIDDESC As System.Windows.Forms.CheckBox
    Friend WithEvents cbCompanyIDASC As System.Windows.Forms.CheckBox
    Friend WithEvents cbSortChoiceASC As System.Windows.Forms.CheckBox
    Friend WithEvents cbSortChoiceDESC As System.Windows.Forms.CheckBox
    Friend WithEvents SortButton As System.Windows.Forms.Button
    Friend WithEvents SortClear As System.Windows.Forms.Button
    Friend WithEvents LastNamePriority As System.Windows.Forms.ComboBox
    Friend WithEvents CompanyIDPriority As System.Windows.Forms.ComboBox
    Friend WithEvents AssignedLocPriority As System.Windows.Forms.ComboBox
    Friend WithEvents SortChoicePriority As System.Windows.Forms.ComboBox
    Friend WithEvents Label54 As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.SaveRecord = New System.Windows.Forms.Button()
        Me.NewRecord = New System.Windows.Forms.Button()
        Me.DownRecord = New System.Windows.Forms.Button()
        Me.UpRecord = New System.Windows.Forms.Button()
        Me.Records = New System.Windows.Forms.Label()
        Me.TopRecord = New System.Windows.Forms.Button()
        Me.BottomRecord = New System.Windows.Forms.Button()
        Me.GroupBox7 = New System.Windows.Forms.GroupBox()
        Me.Label48 = New System.Windows.Forms.Label()
        Me.RemoveFilter = New System.Windows.Forms.Button()
        Me.FilterButton = New System.Windows.Forms.Button()
        Me.Choice1 = New System.Windows.Forms.TextBox()
        Me.FilterChoice1 = New System.Windows.Forms.ComboBox()
        Me.CompanyIDFilter = New System.Windows.Forms.TextBox()
        Me.EmployeeIDFilter = New System.Windows.Forms.TextBox()
        Me.LastNameFilter = New System.Windows.Forms.TextBox()
        Me.Label42 = New System.Windows.Forms.Label()
        Me.Label41 = New System.Windows.Forms.Label()
        Me.Label40 = New System.Windows.Forms.Label()
        Me.DataGrid1 = New System.Windows.Forms.DataGrid()
        Me.DataGrid2 = New System.Windows.Forms.DataGrid()
        Me.TabControl = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.ComboBoxManager = New System.Windows.Forms.ComboBox()
        Me.CompanyManagerBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.NewLookupDataset = New PAF.NewLookupDataset()
        Me.TextBoxLogin = New System.Windows.Forms.TextBox()
        Me.Login = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Email = New System.Windows.Forms.TextBox()
        Me.Label39 = New System.Windows.Forms.Label()
        Me.Comments = New System.Windows.Forms.TextBox()
        Me.GroupBox6 = New System.Windows.Forms.GroupBox()
        Me.RateAssignCurrency = New System.Windows.Forms.TextBox()
        Me.RatePayCurrency = New System.Windows.Forms.TextBox()
        Me.TotalHoursImported = New System.Windows.Forms.TextBox()
        Me.Label38 = New System.Windows.Forms.Label()
        Me.Label36 = New System.Windows.Forms.Label()
        Me.EHA = New System.Windows.Forms.TextBox()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.AssignedLocation = New System.Windows.Forms.ComboBox()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.PayLocation = New System.Windows.Forms.ComboBox()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.EffectiveDate = New System.Windows.Forms.DateTimePicker()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.PAFRevisionNo = New System.Windows.Forms.TextBox()
        Me.FormNo = New System.Windows.Forms.TextBox()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.LabelActive = New System.Windows.Forms.Label()
        Me.CheckBoxActive = New System.Windows.Forms.CheckBox()
        Me.Actual = New System.Windows.Forms.DateTimePicker()
        Me.StartDate = New System.Windows.Forms.DateTimePicker()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.ComboBoxDiscipline = New System.Windows.Forms.ComboBox()
        Me.DisciplineBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.Label10 = New System.Windows.Forms.Label()
        Me.EmployeeNo = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.FirstName = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.LastName = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Company = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.EmployeeID = New System.Windows.Forms.TextBox()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.EmployeeDataGrid = New System.Windows.Forms.DataGrid()
        Me.DataGrid3 = New System.Windows.Forms.DataGrid()
        Me.GroupBox9 = New System.Windows.Forms.GroupBox()
        Me.SortChoicePriority = New System.Windows.Forms.ComboBox()
        Me.AssignedLocPriority = New System.Windows.Forms.ComboBox()
        Me.CompanyIDPriority = New System.Windows.Forms.ComboBox()
        Me.SortClear = New System.Windows.Forms.Button()
        Me.cbSortChoiceDESC = New System.Windows.Forms.CheckBox()
        Me.cbSortChoiceASC = New System.Windows.Forms.CheckBox()
        Me.cbAssignedLocDESC = New System.Windows.Forms.CheckBox()
        Me.cbAssignedLocASC = New System.Windows.Forms.CheckBox()
        Me.cbCompanyIDDESC = New System.Windows.Forms.CheckBox()
        Me.cbCompanyIDASC = New System.Windows.Forms.CheckBox()
        Me.cbLastNameDESC = New System.Windows.Forms.CheckBox()
        Me.Label53 = New System.Windows.Forms.Label()
        Me.Label52 = New System.Windows.Forms.Label()
        Me.SortChoice = New System.Windows.Forms.ComboBox()
        Me.cbLastNameASC = New System.Windows.Forms.CheckBox()
        Me.Label51 = New System.Windows.Forms.Label()
        Me.Label50 = New System.Windows.Forms.Label()
        Me.Label49 = New System.Windows.Forms.Label()
        Me.SortButton = New System.Windows.Forms.Button()
        Me.LastNamePriority = New System.Windows.Forms.ComboBox()
        Me.Label54 = New System.Windows.Forms.Label()
        Me.EmployeeListDataSet = New PAF.EmployeeList()
        Me.CompanyManagerTableAdapter = New PAF.NewLookupDatasetTableAdapters.CompanyManagerTableAdapter()
        Me.DisciplineTableAdapter = New PAF.NewLookupDatasetTableAdapters.DisciplineTableAdapter()
        Me.GroupBox7.SuspendLayout()
        CType(Me.DataGrid1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGrid2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabControl.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        CType(Me.CompanyManagerBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NewLookupDataset, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox6.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        CType(Me.DisciplineBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage2.SuspendLayout()
        CType(Me.EmployeeDataGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGrid3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox9.SuspendLayout()
        CType(Me.EmployeeListDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SaveRecord
        '
        Me.SaveRecord.Location = New System.Drawing.Point(120, 415)
        Me.SaveRecord.Name = "SaveRecord"
        Me.SaveRecord.Size = New System.Drawing.Size(40, 24)
        Me.SaveRecord.TabIndex = 36
        Me.SaveRecord.Text = "Save"
        '
        'NewRecord
        '
        Me.NewRecord.Location = New System.Drawing.Point(168, 415)
        Me.NewRecord.Name = "NewRecord"
        Me.NewRecord.Size = New System.Drawing.Size(40, 24)
        Me.NewRecord.TabIndex = 37
        Me.NewRecord.Text = "New"
        '
        'DownRecord
        '
        Me.DownRecord.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DownRecord.Location = New System.Drawing.Point(40, 423)
        Me.DownRecord.Name = "DownRecord"
        Me.DownRecord.Size = New System.Drawing.Size(24, 16)
        Me.DownRecord.TabIndex = 38
        Me.DownRecord.Text = "<"
        '
        'UpRecord
        '
        Me.UpRecord.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UpRecord.Location = New System.Drawing.Point(64, 423)
        Me.UpRecord.Name = "UpRecord"
        Me.UpRecord.Size = New System.Drawing.Size(24, 16)
        Me.UpRecord.TabIndex = 39
        Me.UpRecord.Text = ">"
        '
        'Records
        '
        Me.Records.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Records.Location = New System.Drawing.Point(8, 447)
        Me.Records.Name = "Records"
        Me.Records.Size = New System.Drawing.Size(112, 16)
        Me.Records.TabIndex = 40
        Me.Records.Text = "Records"
        Me.Records.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TopRecord
        '
        Me.TopRecord.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TopRecord.Location = New System.Drawing.Point(88, 423)
        Me.TopRecord.Name = "TopRecord"
        Me.TopRecord.Size = New System.Drawing.Size(24, 16)
        Me.TopRecord.TabIndex = 42
        Me.TopRecord.Text = ">>"
        '
        'BottomRecord
        '
        Me.BottomRecord.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BottomRecord.Location = New System.Drawing.Point(16, 423)
        Me.BottomRecord.Name = "BottomRecord"
        Me.BottomRecord.Size = New System.Drawing.Size(24, 16)
        Me.BottomRecord.TabIndex = 41
        Me.BottomRecord.Text = "<<"
        '
        'GroupBox7
        '
        Me.GroupBox7.Controls.Add(Me.Label48)
        Me.GroupBox7.Controls.Add(Me.RemoveFilter)
        Me.GroupBox7.Controls.Add(Me.FilterButton)
        Me.GroupBox7.Controls.Add(Me.Choice1)
        Me.GroupBox7.Controls.Add(Me.FilterChoice1)
        Me.GroupBox7.Controls.Add(Me.CompanyIDFilter)
        Me.GroupBox7.Controls.Add(Me.EmployeeIDFilter)
        Me.GroupBox7.Controls.Add(Me.LastNameFilter)
        Me.GroupBox7.Controls.Add(Me.Label42)
        Me.GroupBox7.Controls.Add(Me.Label41)
        Me.GroupBox7.Controls.Add(Me.Label40)
        Me.GroupBox7.Location = New System.Drawing.Point(376, 415)
        Me.GroupBox7.Name = "GroupBox7"
        Me.GroupBox7.Size = New System.Drawing.Size(224, 152)
        Me.GroupBox7.TabIndex = 46
        Me.GroupBox7.TabStop = False
        Me.GroupBox7.Text = "Filters"
        '
        'Label48
        '
        Me.Label48.Location = New System.Drawing.Point(312, 66)
        Me.Label48.Name = "Label48"
        Me.Label48.Size = New System.Drawing.Size(72, 20)
        Me.Label48.TabIndex = 51
        Me.Label48.Text = "Last Name"
        Me.Label48.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'RemoveFilter
        '
        Me.RemoveFilter.Location = New System.Drawing.Point(136, 120)
        Me.RemoveFilter.Name = "RemoveFilter"
        Me.RemoveFilter.Size = New System.Drawing.Size(64, 25)
        Me.RemoveFilter.TabIndex = 50
        Me.RemoveFilter.Text = "Clear"
        '
        'FilterButton
        '
        Me.FilterButton.Location = New System.Drawing.Point(45, 120)
        Me.FilterButton.Name = "FilterButton"
        Me.FilterButton.Size = New System.Drawing.Size(64, 25)
        Me.FilterButton.TabIndex = 49
        Me.FilterButton.Text = "Filter"
        '
        'Choice1
        '
        Me.Choice1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Choice1.Location = New System.Drawing.Point(128, 88)
        Me.Choice1.Name = "Choice1"
        Me.Choice1.Size = New System.Drawing.Size(80, 20)
        Me.Choice1.TabIndex = 48
        '
        'FilterChoice1
        '
        Me.FilterChoice1.Location = New System.Drawing.Point(8, 88)
        Me.FilterChoice1.Name = "FilterChoice1"
        Me.FilterChoice1.Size = New System.Drawing.Size(112, 21)
        Me.FilterChoice1.TabIndex = 46
        Me.FilterChoice1.Text = "Your Choice"
        '
        'CompanyIDFilter
        '
        Me.CompanyIDFilter.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CompanyIDFilter.Location = New System.Drawing.Point(128, 59)
        Me.CompanyIDFilter.Name = "CompanyIDFilter"
        Me.CompanyIDFilter.Size = New System.Drawing.Size(80, 20)
        Me.CompanyIDFilter.TabIndex = 45
        '
        'EmployeeIDFilter
        '
        Me.EmployeeIDFilter.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.EmployeeIDFilter.Location = New System.Drawing.Point(128, 35)
        Me.EmployeeIDFilter.Name = "EmployeeIDFilter"
        Me.EmployeeIDFilter.Size = New System.Drawing.Size(80, 20)
        Me.EmployeeIDFilter.TabIndex = 44
        '
        'LastNameFilter
        '
        Me.LastNameFilter.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LastNameFilter.Location = New System.Drawing.Point(128, 11)
        Me.LastNameFilter.Name = "LastNameFilter"
        Me.LastNameFilter.Size = New System.Drawing.Size(80, 20)
        Me.LastNameFilter.TabIndex = 43
        '
        'Label42
        '
        Me.Label42.Location = New System.Drawing.Point(53, 59)
        Me.Label42.Name = "Label42"
        Me.Label42.Size = New System.Drawing.Size(64, 20)
        Me.Label42.TabIndex = 40
        Me.Label42.Text = "CompanyID"
        Me.Label42.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label41
        '
        Me.Label41.Location = New System.Drawing.Point(45, 35)
        Me.Label41.Name = "Label41"
        Me.Label41.Size = New System.Drawing.Size(72, 20)
        Me.Label41.TabIndex = 39
        Me.Label41.Text = "Employee ID"
        Me.Label41.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label40
        '
        Me.Label40.Location = New System.Drawing.Point(45, 11)
        Me.Label40.Name = "Label40"
        Me.Label40.Size = New System.Drawing.Size(72, 20)
        Me.Label40.TabIndex = 38
        Me.Label40.Text = "Last Name"
        Me.Label40.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'DataGrid1
        '
        Me.DataGrid1.DataMember = ""
        Me.DataGrid1.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.DataGrid1.Location = New System.Drawing.Point(8, 519)
        Me.DataGrid1.Name = "DataGrid1"
        Me.DataGrid1.Size = New System.Drawing.Size(40, 40)
        Me.DataGrid1.TabIndex = 47
        Me.DataGrid1.Visible = False
        '
        'DataGrid2
        '
        Me.DataGrid2.DataMember = ""
        Me.DataGrid2.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.DataGrid2.Location = New System.Drawing.Point(48, 519)
        Me.DataGrid2.Name = "DataGrid2"
        Me.DataGrid2.Size = New System.Drawing.Size(40, 40)
        Me.DataGrid2.TabIndex = 48
        Me.DataGrid2.Visible = False
        '
        'TabControl
        '
        Me.TabControl.Controls.Add(Me.TabPage1)
        Me.TabControl.Controls.Add(Me.TabPage2)
        Me.TabControl.ItemSize = New System.Drawing.Size(0, 18)
        Me.TabControl.Location = New System.Drawing.Point(8, 8)
        Me.TabControl.Name = "TabControl"
        Me.TabControl.SelectedIndex = 0
        Me.TabControl.Size = New System.Drawing.Size(832, 400)
        Me.TabControl.TabIndex = 49
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.ComboBoxManager)
        Me.TabPage1.Controls.Add(Me.TextBoxLogin)
        Me.TabPage1.Controls.Add(Me.Login)
        Me.TabPage1.Controls.Add(Me.Label2)
        Me.TabPage1.Controls.Add(Me.Email)
        Me.TabPage1.Controls.Add(Me.Label39)
        Me.TabPage1.Controls.Add(Me.Comments)
        Me.TabPage1.Controls.Add(Me.GroupBox6)
        Me.TabPage1.Controls.Add(Me.GroupBox5)
        Me.TabPage1.Controls.Add(Me.GroupBox3)
        Me.TabPage1.Controls.Add(Me.ComboBoxDiscipline)
        Me.TabPage1.Controls.Add(Me.Label10)
        Me.TabPage1.Controls.Add(Me.EmployeeNo)
        Me.TabPage1.Controls.Add(Me.Label8)
        Me.TabPage1.Controls.Add(Me.FirstName)
        Me.TabPage1.Controls.Add(Me.Label6)
        Me.TabPage1.Controls.Add(Me.LastName)
        Me.TabPage1.Controls.Add(Me.Label5)
        Me.TabPage1.Controls.Add(Me.Label4)
        Me.TabPage1.Controls.Add(Me.Company)
        Me.TabPage1.Controls.Add(Me.Label3)
        Me.TabPage1.Controls.Add(Me.Label1)
        Me.TabPage1.Controls.Add(Me.EmployeeID)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Size = New System.Drawing.Size(824, 374)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Employee Form"
        '
        'ComboBoxManager
        '
        Me.ComboBoxManager.DataSource = Me.CompanyManagerBindingSource
        Me.ComboBoxManager.DisplayMember = "Name"
        Me.ComboBoxManager.FormattingEnabled = True
        Me.ComboBoxManager.Location = New System.Drawing.Point(570, 7)
        Me.ComboBoxManager.Name = "ComboBoxManager"
        Me.ComboBoxManager.Size = New System.Drawing.Size(213, 21)
        Me.ComboBoxManager.TabIndex = 68
        Me.ComboBoxManager.ValueMember = "CompanyManagerID"
        '
        'CompanyManagerBindingSource
        '
        Me.CompanyManagerBindingSource.DataMember = "CompanyManager"
        Me.CompanyManagerBindingSource.DataSource = Me.NewLookupDataset
        '
        'NewLookupDataset
        '
        Me.NewLookupDataset.DataSetName = "NewLookupDataset"
        Me.NewLookupDataset.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'TextBoxLogin
        '
        Me.TextBoxLogin.Location = New System.Drawing.Point(103, 123)
        Me.TextBoxLogin.Name = "TextBoxLogin"
        Me.TextBoxLogin.Size = New System.Drawing.Size(176, 20)
        Me.TextBoxLogin.TabIndex = 67
        Me.TextBoxLogin.Text = "Login"
        '
        'Login
        '
        Me.Login.AutoSize = True
        Me.Login.Location = New System.Drawing.Point(65, 124)
        Me.Login.Name = "Login"
        Me.Login.Size = New System.Drawing.Size(33, 13)
        Me.Login.TabIndex = 66
        Me.Login.Text = "Login"
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(26, 87)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(72, 20)
        Me.Label2.TabIndex = 65
        Me.Label2.Text = "Email"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Email
        '
        Me.Email.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Email.Location = New System.Drawing.Point(104, 90)
        Me.Email.Name = "Email"
        Me.Email.Size = New System.Drawing.Size(175, 20)
        Me.Email.TabIndex = 64
        Me.Email.Text = "Email"
        '
        'Label39
        '
        Me.Label39.Location = New System.Drawing.Point(240, 283)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(64, 20)
        Me.Label39.TabIndex = 63
        Me.Label39.Text = "Comments"
        Me.Label39.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Comments
        '
        Me.Comments.Location = New System.Drawing.Point(248, 303)
        Me.Comments.Multiline = True
        Me.Comments.Name = "Comments"
        Me.Comments.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.Comments.Size = New System.Drawing.Size(552, 57)
        Me.Comments.TabIndex = 62
        Me.Comments.Text = "Comments"
        '
        'GroupBox6
        '
        Me.GroupBox6.Controls.Add(Me.RateAssignCurrency)
        Me.GroupBox6.Controls.Add(Me.RatePayCurrency)
        Me.GroupBox6.Controls.Add(Me.TotalHoursImported)
        Me.GroupBox6.Controls.Add(Me.Label38)
        Me.GroupBox6.Controls.Add(Me.Label36)
        Me.GroupBox6.Controls.Add(Me.EHA)
        Me.GroupBox6.Controls.Add(Me.Label34)
        Me.GroupBox6.Controls.Add(Me.AssignedLocation)
        Me.GroupBox6.Controls.Add(Me.Label33)
        Me.GroupBox6.Controls.Add(Me.PayLocation)
        Me.GroupBox6.Controls.Add(Me.Label32)
        Me.GroupBox6.Location = New System.Drawing.Point(248, 161)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Size = New System.Drawing.Size(552, 118)
        Me.GroupBox6.TabIndex = 61
        Me.GroupBox6.TabStop = False
        '
        'RateAssignCurrency
        '
        Me.RateAssignCurrency.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RateAssignCurrency.Location = New System.Drawing.Point(272, 55)
        Me.RateAssignCurrency.Name = "RateAssignCurrency"
        Me.RateAssignCurrency.Size = New System.Drawing.Size(56, 20)
        Me.RateAssignCurrency.TabIndex = 49
        Me.RateAssignCurrency.Text = "RateAssignCurrency"
        '
        'RatePayCurrency
        '
        Me.RatePayCurrency.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RatePayCurrency.Location = New System.Drawing.Point(272, 32)
        Me.RatePayCurrency.Name = "RatePayCurrency"
        Me.RatePayCurrency.Size = New System.Drawing.Size(56, 20)
        Me.RatePayCurrency.TabIndex = 48
        Me.RatePayCurrency.Text = "RatePayCurrency"
        '
        'TotalHoursImported
        '
        Me.TotalHoursImported.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TotalHoursImported.Location = New System.Drawing.Point(384, 80)
        Me.TotalHoursImported.Name = "TotalHoursImported"
        Me.TotalHoursImported.Size = New System.Drawing.Size(56, 20)
        Me.TotalHoursImported.TabIndex = 45
        Me.TotalHoursImported.Text = "TotalHoursImported"
        '
        'Label38
        '
        Me.Label38.Location = New System.Drawing.Point(272, 80)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(112, 20)
        Me.Label38.TabIndex = 44
        Me.Label38.Text = "Total Hours Imported"
        Me.Label38.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label36
        '
        Me.Label36.Location = New System.Drawing.Point(272, 8)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(56, 20)
        Me.Label36.TabIndex = 41
        Me.Label36.Text = "Currency"
        Me.Label36.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'EHA
        '
        Me.EHA.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.EHA.Location = New System.Drawing.Point(169, 80)
        Me.EHA.Name = "EHA"
        Me.EHA.Size = New System.Drawing.Size(56, 20)
        Me.EHA.TabIndex = 36
        Me.EHA.Text = "EHA"
        '
        'Label34
        '
        Me.Label34.Location = New System.Drawing.Point(81, 80)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(72, 20)
        Me.Label34.TabIndex = 35
        Me.Label34.Text = "Hourly Rate"
        Me.Label34.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'AssignedLocation
        '
        Me.AssignedLocation.Location = New System.Drawing.Point(169, 56)
        Me.AssignedLocation.Name = "AssignedLocation"
        Me.AssignedLocation.Size = New System.Drawing.Size(87, 21)
        Me.AssignedLocation.TabIndex = 34
        Me.AssignedLocation.Text = "AssignedLocation"
        '
        'Label33
        '
        Me.Label33.ForeColor = System.Drawing.Color.Black
        Me.Label33.Location = New System.Drawing.Point(52, 56)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(98, 20)
        Me.Label33.TabIndex = 33
        Me.Label33.Text = "Assigned Location"
        Me.Label33.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'PayLocation
        '
        Me.PayLocation.Location = New System.Drawing.Point(169, 32)
        Me.PayLocation.Name = "PayLocation"
        Me.PayLocation.Size = New System.Drawing.Size(87, 21)
        Me.PayLocation.TabIndex = 32
        Me.PayLocation.Text = "PayLocation"
        '
        'Label32
        '
        Me.Label32.Location = New System.Drawing.Point(65, 32)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(88, 20)
        Me.Label32.TabIndex = 31
        Me.Label32.Text = "Pay Location"
        Me.Label32.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.EffectiveDate)
        Me.GroupBox5.Controls.Add(Me.Label9)
        Me.GroupBox5.Controls.Add(Me.Label7)
        Me.GroupBox5.Controls.Add(Me.PAFRevisionNo)
        Me.GroupBox5.Controls.Add(Me.FormNo)
        Me.GroupBox5.Controls.Add(Me.Label29)
        Me.GroupBox5.Location = New System.Drawing.Point(316, 81)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(484, 74)
        Me.GroupBox5.TabIndex = 60
        Me.GroupBox5.TabStop = False
        '
        'EffectiveDate
        '
        Me.EffectiveDate.CustomFormat = "M/d/yyyy"
        Me.EffectiveDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.EffectiveDate.Location = New System.Drawing.Point(374, 18)
        Me.EffectiveDate.Name = "EffectiveDate"
        Me.EffectiveDate.ShowCheckBox = True
        Me.EffectiveDate.Size = New System.Drawing.Size(96, 20)
        Me.EffectiveDate.TabIndex = 39
        Me.EffectiveDate.Value = New Date(1900, 1, 1, 0, 0, 0, 0)
        '
        'Label9
        '
        Me.Label9.Location = New System.Drawing.Point(251, 18)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(108, 20)
        Me.Label9.TabIndex = 35
        Me.Label9.Text = "Effective Date"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label7
        '
        Me.Label7.Location = New System.Drawing.Point(24, 42)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(91, 20)
        Me.Label7.TabIndex = 34
        Me.Label7.Text = "PAF Revision No"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'PAFRevisionNo
        '
        Me.PAFRevisionNo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PAFRevisionNo.Location = New System.Drawing.Point(121, 42)
        Me.PAFRevisionNo.Name = "PAFRevisionNo"
        Me.PAFRevisionNo.Size = New System.Drawing.Size(94, 20)
        Me.PAFRevisionNo.TabIndex = 32
        Me.PAFRevisionNo.Text = "PAFRevisionNo"
        '
        'FormNo
        '
        Me.FormNo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormNo.Location = New System.Drawing.Point(123, 16)
        Me.FormNo.Name = "FormNo"
        Me.FormNo.Size = New System.Drawing.Size(94, 20)
        Me.FormNo.TabIndex = 31
        Me.FormNo.Text = "FormNo"
        '
        'Label29
        '
        Me.Label29.Location = New System.Drawing.Point(16, 16)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(99, 20)
        Me.Label29.TabIndex = 30
        Me.Label29.Text = "PAF Form No"
        Me.Label29.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.LabelActive)
        Me.GroupBox3.Controls.Add(Me.CheckBoxActive)
        Me.GroupBox3.Controls.Add(Me.Actual)
        Me.GroupBox3.Controls.Add(Me.StartDate)
        Me.GroupBox3.Controls.Add(Me.Label20)
        Me.GroupBox3.Controls.Add(Me.Label19)
        Me.GroupBox3.Location = New System.Drawing.Point(8, 161)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(208, 100)
        Me.GroupBox3.TabIndex = 58
        Me.GroupBox3.TabStop = False
        '
        'LabelActive
        '
        Me.LabelActive.AutoSize = True
        Me.LabelActive.Location = New System.Drawing.Point(51, 64)
        Me.LabelActive.Name = "LabelActive"
        Me.LabelActive.Size = New System.Drawing.Size(37, 13)
        Me.LabelActive.TabIndex = 40
        Me.LabelActive.Text = "Active"
        '
        'CheckBoxActive
        '
        Me.CheckBoxActive.AutoSize = True
        Me.CheckBoxActive.Checked = True
        Me.CheckBoxActive.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBoxActive.Location = New System.Drawing.Point(95, 66)
        Me.CheckBoxActive.Name = "CheckBoxActive"
        Me.CheckBoxActive.Size = New System.Drawing.Size(15, 14)
        Me.CheckBoxActive.TabIndex = 39
        Me.CheckBoxActive.UseVisualStyleBackColor = True
        '
        'Actual
        '
        Me.Actual.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.Actual.Location = New System.Drawing.Point(96, 40)
        Me.Actual.Name = "Actual"
        Me.Actual.ShowCheckBox = True
        Me.Actual.Size = New System.Drawing.Size(96, 20)
        Me.Actual.TabIndex = 38
        Me.Actual.Value = New Date(1900, 1, 1, 0, 0, 0, 0)
        '
        'StartDate
        '
        Me.StartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.StartDate.Location = New System.Drawing.Point(96, 16)
        Me.StartDate.Name = "StartDate"
        Me.StartDate.ShowCheckBox = True
        Me.StartDate.Size = New System.Drawing.Size(96, 20)
        Me.StartDate.TabIndex = 36
        Me.StartDate.Value = New Date(1900, 1, 1, 0, 0, 0, 0)
        '
        'Label20
        '
        Me.Label20.Location = New System.Drawing.Point(12, 40)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(80, 20)
        Me.Label20.TabIndex = 31
        Me.Label20.Text = "Demobilisation"
        Me.Label20.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label19
        '
        Me.Label19.Location = New System.Drawing.Point(25, 15)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(64, 20)
        Me.Label19.TabIndex = 29
        Me.Label19.Text = "Start Date"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'ComboBoxDiscipline
        '
        Me.ComboBoxDiscipline.DataSource = Me.DisciplineBindingSource
        Me.ComboBoxDiscipline.DisplayMember = "Expr1"
        Me.ComboBoxDiscipline.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxDiscipline.Location = New System.Drawing.Point(568, 40)
        Me.ComboBoxDiscipline.Name = "ComboBoxDiscipline"
        Me.ComboBoxDiscipline.Size = New System.Drawing.Size(215, 21)
        Me.ComboBoxDiscipline.TabIndex = 55
        Me.ComboBoxDiscipline.ValueMember = "DisciplineCode"
        '
        'DisciplineBindingSource
        '
        Me.DisciplineBindingSource.DataMember = "Discipline"
        Me.DisciplineBindingSource.DataSource = Me.NewLookupDataset
        '
        'Label10
        '
        Me.Label10.Location = New System.Drawing.Point(480, 40)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(72, 20)
        Me.Label10.TabIndex = 54
        Me.Label10.Text = "Discipline"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'EmployeeNo
        '
        Me.EmployeeNo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.EmployeeNo.Location = New System.Drawing.Point(344, 40)
        Me.EmployeeNo.Name = "EmployeeNo"
        Me.EmployeeNo.Size = New System.Drawing.Size(88, 20)
        Me.EmployeeNo.TabIndex = 51
        Me.EmployeeNo.Text = "EmployeeNo"
        '
        'Label8
        '
        Me.Label8.Location = New System.Drawing.Point(264, 40)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(72, 20)
        Me.Label8.TabIndex = 50
        Me.Label8.Text = "Employee No"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'FirstName
        '
        Me.FirstName.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FirstName.Location = New System.Drawing.Point(104, 64)
        Me.FirstName.Name = "FirstName"
        Me.FirstName.Size = New System.Drawing.Size(88, 20)
        Me.FirstName.TabIndex = 47
        Me.FirstName.Text = "FirstName"
        '
        'Label6
        '
        Me.Label6.Location = New System.Drawing.Point(104, 40)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(72, 20)
        Me.Label6.TabIndex = 46
        Me.Label6.Text = "FirstName"
        '
        'LastName
        '
        Me.LastName.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LastName.Location = New System.Drawing.Point(8, 64)
        Me.LastName.Name = "LastName"
        Me.LastName.Size = New System.Drawing.Size(88, 20)
        Me.LastName.TabIndex = 45
        Me.LastName.Text = "LastName"
        '
        'Label5
        '
        Me.Label5.Location = New System.Drawing.Point(8, 40)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(72, 20)
        Me.Label5.TabIndex = 44
        Me.Label5.Text = "LastName"
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(464, 8)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(100, 20)
        Me.Label4.TabIndex = 42
        Me.Label4.Text = "Manager Name"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Company
        '
        Me.Company.Location = New System.Drawing.Point(342, 8)
        Me.Company.Name = "Company"
        Me.Company.Size = New System.Drawing.Size(97, 21)
        Me.Company.TabIndex = 41
        Me.Company.Text = "Company"
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(280, 8)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(56, 20)
        Me.Label3.TabIndex = 40
        Me.Label3.Text = "Company"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(8, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(72, 20)
        Me.Label1.TabIndex = 37
        Me.Label1.Text = "Employee ID"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'EmployeeID
        '
        Me.EmployeeID.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.EmployeeID.Location = New System.Drawing.Point(80, 8)
        Me.EmployeeID.Name = "EmployeeID"
        Me.EmployeeID.ReadOnly = True
        Me.EmployeeID.Size = New System.Drawing.Size(72, 20)
        Me.EmployeeID.TabIndex = 36
        Me.EmployeeID.Text = "EmployeeID"
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.EmployeeDataGrid)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Size = New System.Drawing.Size(824, 374)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Employee Table"
        '
        'EmployeeDataGrid
        '
        Me.EmployeeDataGrid.DataMember = ""
        Me.EmployeeDataGrid.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.EmployeeDataGrid.Location = New System.Drawing.Point(8, 8)
        Me.EmployeeDataGrid.Name = "EmployeeDataGrid"
        Me.EmployeeDataGrid.ReadOnly = True
        Me.EmployeeDataGrid.Size = New System.Drawing.Size(808, 363)
        Me.EmployeeDataGrid.TabIndex = 46
        '
        'DataGrid3
        '
        Me.DataGrid3.DataMember = ""
        Me.DataGrid3.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.DataGrid3.Location = New System.Drawing.Point(88, 519)
        Me.DataGrid3.Name = "DataGrid3"
        Me.DataGrid3.Size = New System.Drawing.Size(40, 40)
        Me.DataGrid3.TabIndex = 51
        Me.DataGrid3.Visible = False
        '
        'GroupBox9
        '
        Me.GroupBox9.Controls.Add(Me.SortChoicePriority)
        Me.GroupBox9.Controls.Add(Me.AssignedLocPriority)
        Me.GroupBox9.Controls.Add(Me.CompanyIDPriority)
        Me.GroupBox9.Controls.Add(Me.SortClear)
        Me.GroupBox9.Controls.Add(Me.cbSortChoiceDESC)
        Me.GroupBox9.Controls.Add(Me.cbSortChoiceASC)
        Me.GroupBox9.Controls.Add(Me.cbAssignedLocDESC)
        Me.GroupBox9.Controls.Add(Me.cbAssignedLocASC)
        Me.GroupBox9.Controls.Add(Me.cbCompanyIDDESC)
        Me.GroupBox9.Controls.Add(Me.cbCompanyIDASC)
        Me.GroupBox9.Controls.Add(Me.cbLastNameDESC)
        Me.GroupBox9.Controls.Add(Me.Label53)
        Me.GroupBox9.Controls.Add(Me.Label52)
        Me.GroupBox9.Controls.Add(Me.SortChoice)
        Me.GroupBox9.Controls.Add(Me.cbLastNameASC)
        Me.GroupBox9.Controls.Add(Me.Label51)
        Me.GroupBox9.Controls.Add(Me.Label50)
        Me.GroupBox9.Controls.Add(Me.Label49)
        Me.GroupBox9.Controls.Add(Me.SortButton)
        Me.GroupBox9.Controls.Add(Me.LastNamePriority)
        Me.GroupBox9.Controls.Add(Me.Label54)
        Me.GroupBox9.Location = New System.Drawing.Point(608, 415)
        Me.GroupBox9.Name = "GroupBox9"
        Me.GroupBox9.Size = New System.Drawing.Size(232, 152)
        Me.GroupBox9.TabIndex = 52
        Me.GroupBox9.TabStop = False
        Me.GroupBox9.Text = "Sort"
        '
        'SortChoicePriority
        '
        Me.SortChoicePriority.Location = New System.Drawing.Point(189, 91)
        Me.SortChoicePriority.Name = "SortChoicePriority"
        Me.SortChoicePriority.Size = New System.Drawing.Size(40, 21)
        Me.SortChoicePriority.TabIndex = 57
        '
        'AssignedLocPriority
        '
        Me.AssignedLocPriority.Location = New System.Drawing.Point(189, 69)
        Me.AssignedLocPriority.Name = "AssignedLocPriority"
        Me.AssignedLocPriority.Size = New System.Drawing.Size(40, 21)
        Me.AssignedLocPriority.TabIndex = 56
        '
        'CompanyIDPriority
        '
        Me.CompanyIDPriority.Location = New System.Drawing.Point(189, 48)
        Me.CompanyIDPriority.Name = "CompanyIDPriority"
        Me.CompanyIDPriority.Size = New System.Drawing.Size(40, 21)
        Me.CompanyIDPriority.TabIndex = 55
        '
        'SortClear
        '
        Me.SortClear.Location = New System.Drawing.Point(129, 118)
        Me.SortClear.Name = "SortClear"
        Me.SortClear.Size = New System.Drawing.Size(64, 25)
        Me.SortClear.TabIndex = 54
        Me.SortClear.Text = "Clear"
        '
        'cbSortChoiceDESC
        '
        Me.cbSortChoiceDESC.Location = New System.Drawing.Point(158, 94)
        Me.cbSortChoiceDESC.Name = "cbSortChoiceDESC"
        Me.cbSortChoiceDESC.Size = New System.Drawing.Size(16, 16)
        Me.cbSortChoiceDESC.TabIndex = 13
        '
        'cbSortChoiceASC
        '
        Me.cbSortChoiceASC.Location = New System.Drawing.Point(126, 93)
        Me.cbSortChoiceASC.Name = "cbSortChoiceASC"
        Me.cbSortChoiceASC.Size = New System.Drawing.Size(16, 16)
        Me.cbSortChoiceASC.TabIndex = 12
        '
        'cbAssignedLocDESC
        '
        Me.cbAssignedLocDESC.Location = New System.Drawing.Point(158, 70)
        Me.cbAssignedLocDESC.Name = "cbAssignedLocDESC"
        Me.cbAssignedLocDESC.Size = New System.Drawing.Size(16, 16)
        Me.cbAssignedLocDESC.TabIndex = 11
        '
        'cbAssignedLocASC
        '
        Me.cbAssignedLocASC.Location = New System.Drawing.Point(126, 70)
        Me.cbAssignedLocASC.Name = "cbAssignedLocASC"
        Me.cbAssignedLocASC.Size = New System.Drawing.Size(16, 16)
        Me.cbAssignedLocASC.TabIndex = 10
        '
        'cbCompanyIDDESC
        '
        Me.cbCompanyIDDESC.Location = New System.Drawing.Point(158, 48)
        Me.cbCompanyIDDESC.Name = "cbCompanyIDDESC"
        Me.cbCompanyIDDESC.Size = New System.Drawing.Size(16, 16)
        Me.cbCompanyIDDESC.TabIndex = 9
        '
        'cbCompanyIDASC
        '
        Me.cbCompanyIDASC.Location = New System.Drawing.Point(126, 48)
        Me.cbCompanyIDASC.Name = "cbCompanyIDASC"
        Me.cbCompanyIDASC.Size = New System.Drawing.Size(16, 16)
        Me.cbCompanyIDASC.TabIndex = 8
        '
        'cbLastNameDESC
        '
        Me.cbLastNameDESC.Location = New System.Drawing.Point(158, 26)
        Me.cbLastNameDESC.Name = "cbLastNameDESC"
        Me.cbLastNameDESC.Size = New System.Drawing.Size(16, 16)
        Me.cbLastNameDESC.TabIndex = 7
        '
        'Label53
        '
        Me.Label53.Location = New System.Drawing.Point(145, 9)
        Me.Label53.Name = "Label53"
        Me.Label53.Size = New System.Drawing.Size(40, 16)
        Me.Label53.TabIndex = 6
        Me.Label53.Text = "DESC"
        Me.Label53.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label52
        '
        Me.Label52.Location = New System.Drawing.Point(116, 9)
        Me.Label52.Name = "Label52"
        Me.Label52.Size = New System.Drawing.Size(32, 16)
        Me.Label52.TabIndex = 5
        Me.Label52.Text = "ASC"
        Me.Label52.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'SortChoice
        '
        Me.SortChoice.Location = New System.Drawing.Point(6, 92)
        Me.SortChoice.Name = "SortChoice"
        Me.SortChoice.Size = New System.Drawing.Size(112, 21)
        Me.SortChoice.TabIndex = 4
        Me.SortChoice.Text = "Your Choice"
        '
        'cbLastNameASC
        '
        Me.cbLastNameASC.Location = New System.Drawing.Point(126, 26)
        Me.cbLastNameASC.Name = "cbLastNameASC"
        Me.cbLastNameASC.Size = New System.Drawing.Size(16, 16)
        Me.cbLastNameASC.TabIndex = 3
        '
        'Label51
        '
        Me.Label51.Location = New System.Drawing.Point(8, 69)
        Me.Label51.Name = "Label51"
        Me.Label51.Size = New System.Drawing.Size(104, 16)
        Me.Label51.TabIndex = 2
        Me.Label51.Text = "Assigned Location"
        Me.Label51.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label50
        '
        Me.Label50.Location = New System.Drawing.Point(50, 49)
        Me.Label50.Name = "Label50"
        Me.Label50.Size = New System.Drawing.Size(64, 16)
        Me.Label50.TabIndex = 1
        Me.Label50.Text = "CompanyID"
        Me.Label50.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label49
        '
        Me.Label49.Location = New System.Drawing.Point(48, 27)
        Me.Label49.Name = "Label49"
        Me.Label49.Size = New System.Drawing.Size(64, 16)
        Me.Label49.TabIndex = 0
        Me.Label49.Text = "Last Name"
        Me.Label49.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'SortButton
        '
        Me.SortButton.Location = New System.Drawing.Point(36, 118)
        Me.SortButton.Name = "SortButton"
        Me.SortButton.Size = New System.Drawing.Size(64, 25)
        Me.SortButton.TabIndex = 53
        Me.SortButton.Text = "Sort"
        '
        'LastNamePriority
        '
        Me.LastNamePriority.Location = New System.Drawing.Point(190, 26)
        Me.LastNamePriority.Name = "LastNamePriority"
        Me.LastNamePriority.Size = New System.Drawing.Size(40, 21)
        Me.LastNamePriority.TabIndex = 53
        '
        'Label54
        '
        Me.Label54.Location = New System.Drawing.Point(189, 9)
        Me.Label54.Name = "Label54"
        Me.Label54.Size = New System.Drawing.Size(40, 16)
        Me.Label54.TabIndex = 58
        Me.Label54.Text = "Priority"
        Me.Label54.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'EmployeeListDataSet
        '
        Me.EmployeeListDataSet.DataSetName = "EmployeeList"
        Me.EmployeeListDataSet.Locale = New System.Globalization.CultureInfo("en-AU")
        Me.EmployeeListDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'CompanyManagerTableAdapter
        '
        Me.CompanyManagerTableAdapter.ClearBeforeFill = True
        '
        'DisciplineTableAdapter
        '
        Me.DisciplineTableAdapter.ClearBeforeFill = True
        '
        'EmployeeForm
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(848, 572)
        Me.Controls.Add(Me.GroupBox9)
        Me.Controls.Add(Me.DataGrid3)
        Me.Controls.Add(Me.TabControl)
        Me.Controls.Add(Me.DataGrid2)
        Me.Controls.Add(Me.DataGrid1)
        Me.Controls.Add(Me.GroupBox7)
        Me.Controls.Add(Me.TopRecord)
        Me.Controls.Add(Me.BottomRecord)
        Me.Controls.Add(Me.Records)
        Me.Controls.Add(Me.UpRecord)
        Me.Controls.Add(Me.DownRecord)
        Me.Controls.Add(Me.NewRecord)
        Me.Controls.Add(Me.SaveRecord)
        Me.Name = "EmployeeForm"
        Me.Text = "Employee"
        Me.GroupBox7.ResumeLayout(False)
        Me.GroupBox7.PerformLayout()
        CType(Me.DataGrid1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGrid2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabControl.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        CType(Me.CompanyManagerBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NewLookupDataset, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox6.ResumeLayout(False)
        Me.GroupBox6.PerformLayout()
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        CType(Me.DisciplineBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage2.ResumeLayout(False)
        CType(Me.EmployeeDataGrid, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGrid3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox9.ResumeLayout(False)
        CType(Me.EmployeeListDataSet, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region   

    Private PrivilegedSQLConnection As SqlClient.SqlConnection
    'Events on form load
    Private Sub EmployeeForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'NewLookupDataset.Discipline' table. You can move, or remove it, as needed.
        Me.DisciplineTableAdapter.Fill(Me.NewLookupDataset.Discipline)
        'TODO: This line of code loads data into the 'NewLookupDataset.CompanyManager' table. You can move, or remove it, as needed.
        Me.CompanyManagerTableAdapter.Fill(Me.NewLookupDataset.CompanyManager)
        Dim i As Integer

        Try
            'Fill main employee Dataset with existing data from database table "Employee"
            i = EmployeeSqlDataAdapter.Fill(EmployeeListDataSet.Employee)
            'Setup employee form objects
            EnableNavigationButtons()
            populateFilterChoice()
            populateSortPriority()
            AutoSizeTable()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Application Error")
            Me.Close()
        End Try
    End Sub

    'Validate employee row
    Private Function CurrentRecordIsValid() As Boolean
        If EmployeeBindingContext.Count = 0 Then
            Return True
        End If
        'Employee must have a Last Name
        If LastName.Text = "" Then
            MsgBox("Please enter an Employee Last Name!", MsgBoxStyle.Exclamation, "Information")
            Return False
        End If
        'Employee must have a First Name
        If FirstName.Text = "" Then
            MsgBox("Please enter an Employee First Name!", MsgBoxStyle.Exclamation, "Information")
            Return False
        End If
        'Employee must have an Employee Number
        If EmployeeNo.Text = "" Then
            MsgBox("Please enter a Company Employee Number!", MsgBoxStyle.Exclamation, "Information")
            Return False
        End If
        'Employee must have a discipline
        If Me.ComboBoxDiscipline.Text = "" Then
            MsgBox("Please enter a Discipline for the employee!", MsgBoxStyle.Exclamation, "Information")
            Return False
        End If
        'Employee must have a Manager
        If Me.ComboBoxManager.Text = "" Then
            MsgBox("Please enter a Manager for the employee!", MsgBoxStyle.Exclamation, "Information")
            Return False
        End If
        Return True
    End Function

    'Execute employee to database save routine
    Private Sub SaveRecord_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveRecord.Click
        Try
            'Check employee details
            If CurrentRecordIsValid() Then
                'Save data
                Try
                    EmployeeBindingContext.EndCurrentEdit()
                    Me.EmployeeSqlDataAdapter.Update(Me.EmployeeListDataSet)
                    Me.EmployeeListDataSet.AcceptChanges()
                    dvOriginalRows.RowFilter = ""
                    YesSelected = False
                Catch ex As SqlException
                    MsgBox(ex.Message, MsgBoxStyle.Critical, "Application Error")
                End Try
            End If
            EnableNavigationButtons()

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    'Move up one record in the Main employee dataset
    Private Sub UpRecord_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UpRecord.Click
        EmployeeBindingContext.Position += 1
        EnableNavigationButtons()
        'If Employee Form tab selected, save data
        If Me.EmployeeListDataSet.HasChanges Then
            If TabControl.SelectedTab.Text = "Employee Form" Then
                Me.SaveRecord_Click(Nothing, Nothing)
            End If
        End If
    End Sub

    'Move down one record in the Main employee dataset
    Private Sub DownRecord_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DownRecord.Click
        EmployeeBindingContext.Position -= 1
        EnableNavigationButtons()
        'If Employee Form tab selected, save data
        If Me.EmployeeListDataSet.HasChanges Then
            If TabControl.SelectedTab.Text = "Employee Form" Then
                Me.SaveRecord_Click(Nothing, Nothing)
            End If
        End If
    End Sub

    'Move to first record in Main employee dataset
    Private Sub TopRecord_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TopRecord.Click
        EmployeeBindingContext.Position = EmployeeBindingContext.Count
        EnableNavigationButtons()
    End Sub

    'Move to last record in Main employee dataset
    Private Sub BottomRecord_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BottomRecord.Click
        EmployeeBindingContext.Position = 0
        EnableNavigationButtons()
    End Sub

    'Enable Main employee dataset navigation buttons
    Private Sub EnableNavigationButtons()
        'If currently on last employee record, disable UpRecord button, Else enable UpRecord button
        If EmployeeBindingContext.Position + 1 = EmployeeBindingContext.Count Then
            UpRecord.Enabled = False
        Else
            UpRecord.Enabled = True
        End If
        'If currently on first employee record, disable DownRecord button, Else enable DownRecord button
        If EmployeeBindingContext.Position = 0 Then
            DownRecord.Enabled = False
        Else
            DownRecord.Enabled = True
        End If
        'Display current employee record selected
        Me.Records.Text = "Record " & EmployeeBindingContext.Position + 1 & " of " & EmployeeBindingContext.Count
        'LGX 31/08/10: To avoid user to replace an employee by another one
        If Me.LastName.Text <> "" Then
            Me.LastName.ReadOnly = True
            Me.FirstName.ReadOnly = True
        Else
            Me.LastName.ReadOnly = False
            Me.FirstName.ReadOnly = False
        End If
    End Sub

    'Setup default parameters for new employee
    Private Sub NewRecord_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NewRecord.Click
        Dim newEmployeeRow As EmployeeList.EmployeeRow

        'LGX 06/10/10 Correction: IF the Filter was enabled, the new record was not shown on the form
        DataView.RowFilter = ""
        LastNameFilter.Text = ""
        EmployeeIDFilter.Text = ""
        CompanyIDFilter.Text = ""
        Choice1.Text = ""

        'Create new employee record
        newEmployeeRow = EmployeeListDataSet.Employee.NewEmployeeRow()

        'Assign default employee parameters to new record
        newEmployeeRow.CompanyID = "HAT"
        newEmployeeRow.LastName = ""
        newEmployeeRow.FirstName = ""
        newEmployeeRow.Email = ""
        newEmployeeRow.DisciplineCode = "110H"
        newEmployeeRow.RatePayLocation = "BRC"
        newEmployeeRow.RateAssignLocation = "BRC"
        newEmployeeRow.RatePayCurrencyID = "AUD"
        newEmployeeRow.RateAssignCurrencyID = "AUD"
        'LGX : Add a default value to the checkbox otherwise the EmployeeBindingContext.Position gets back to zero
        newEmployeeRow.EmployeeActive = True

        'Add new employee record to Main employee dataset
        Me.EmployeeListDataSet.Employee.Rows.Add(newEmployeeRow)
        'Move to new employee record
        EmployeeBindingContext.Position = EmployeeBindingContext.Count
        EnableNavigationButtons()
    End Sub

    'Fill Filter dropdown list with every possible column in employee table from databaset
    Private Sub populateFilterChoice()
        Dim objDataSet As DataSet
        Dim objSQLCommand As SqlClient.SqlCommand
        Dim objSQLAdapter As SqlClient.SqlDataAdapter
        Dim objDataRow As DataRow

        Try
            objSQLCommand = New SqlClient.SqlCommand
            objSQLCommand.CommandType = CommandType.Text
            objSQLCommand.CommandText = "SELECT column_name AS Columns FROM INFORMATION_SCHEMA.COLUMNS WHERE (TABLE_NAME = 'Employee')"
            objSQLCommand.Connection = PrivilegedSQLConnection
            objSQLAdapter = New SqlClient.SqlDataAdapter(objSQLCommand)
            objDataSet = New DataSet("INFORMATION_SCHEMA.COLUMNS")
            objSQLAdapter.Fill(objDataSet, "INFORMATION_SCHEMA.COLUMNS")

            For Each objDataRow In objDataSet.Tables("INFORMATION_SCHEMA.COLUMNS").Rows
                Me.FilterChoice1.Items.Add(objDataRow.Item("Columns"))
                Me.SortChoice.Items.Add(objDataRow.Item("Columns"))
            Next

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    'Filter main employee dataview  based on parameters selected by user
    Private Sub FilterButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FilterButton.Click
        If Me.LastNameFilter.Text <> "" Then
            DataView.RowFilter = "LastName LIKE '%" & Me.LastNameFilter.Text & "%'"
        End If
        If Me.EmployeeIDFilter.Text <> "" Then
            If Me.LastNameFilter.Text = "" Then

                Dim i As Integer
                If Not Integer.TryParse(Me.EmployeeIDFilter.Text, i) Then
                    MsgBox("Filter on EmployeeID: have to be a number!", MsgBoxStyle.Exclamation, "Warning")
                Else
                    DataView.RowFilter = "EmployeeID = " & Me.EmployeeIDFilter.Text
                End If
            Else
                DataView.RowFilter = DataView.RowFilter & " AND EmployeeID = " & Me.EmployeeIDFilter.Text
            End If
        End If
        If Me.CompanyIDFilter.Text <> "" Then
            If (Me.EmployeeIDFilter.Text = "") And (Me.LastNameFilter.Text = "") Then
                DataView.RowFilter = "CompanyID = '" & Me.CompanyIDFilter.Text & "'"
            Else
                DataView.RowFilter = DataView.RowFilter & " AND CompanyID = '" & Me.CompanyIDFilter.Text & "'"
            End If
        End If
        If Me.Choice1.Text <> "" And Me.FilterChoice1.Text <> "Your Choice" Then
            If (Me.EmployeeIDFilter.Text = "") And (Me.LastNameFilter.Text = "") And (Me.CompanyIDFilter.Text = "") Then
                DataView.RowFilter = Me.FilterChoice1.Text & " = " & DetermineFilterChoice(Me.FilterChoice1.Text)
            Else
                DataView.RowFilter = DataView.RowFilter & " AND " & Me.FilterChoice1.Text & " = " & DetermineFilterChoice(Me.FilterChoice1.Text)
            End If
        End If
        EnableNavigationButtons()
    End Sub

    'Clear filter parameters and restore Main employee dataview to original row state
    Private Sub RemoveFilter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RemoveFilter.Click
        DataView.RowFilter = ""
        Me.LastNameFilter.Text = ""
        Me.EmployeeIDFilter.Text = ""
        Me.CompanyIDFilter.Text = ""
        Me.Choice1.Text = ""
        EnableNavigationButtons()
    End Sub

    Private Sub Filter_TextChanged(ByVal sender As System.Object, ByVal e As Windows.Forms.KeyEventArgs) Handles EmployeeIDFilter.KeyDown, LastNameFilter.KeyDown, CompanyIDFilter.KeyDown, Choice1.KeyDown
        If e.KeyCode = Keys.Enter Then
            Me.FilterButton_Click(Nothing, Nothing)
        End If
    End Sub

    'Depending on what parameter type (int, string) is selected from the Filter dropdown list, assign the 
    'appropriate filter statement
    Private Function DetermineFilterChoice(ByVal Selection As String) As String
        Dim FilterCriteria As String = ""

        Select Case Selection
            Case "EmployeeID"
                FilterCriteria = Me.Choice1.Text
            Case "CompanyID"
                FilterCriteria = "'" & Me.Choice1.Text & "'"
            Case "CompanyEmployeeNo"
                FilterCriteria = "'" & Me.Choice1.Text & "'"
            Case "DisciplineCode"
                FilterCriteria = "'" & Me.Choice1.Text & "'"
            Case "LastName"
                FilterCriteria = "'" & Me.Choice1.Text & "'"
            Case "FirstName"
                FilterCriteria = "'" & Me.Choice1.Text & "'"
            Case "TotalHoursImported"
                FilterCriteria = Me.Choice1.Text
            Case "CompanyManagerID"
                FilterCriteria = "'" & Me.Choice1.Text & "'"
            Case "Comments"
                FilterCriteria = "'" & Me.Choice1.Text & "'"
            Case "PAFNo"
                FilterCriteria = "'" & Me.Choice1.Text & "'"
            Case "PAFRev"
                FilterCriteria = Me.Choice1.Text
            Case "StartDate"
                FilterCriteria = "'" & Me.Choice1.Text & "'"
            Case "RatePayLocation"
                FilterCriteria = "'" & Me.Choice1.Text & "'"
            Case "RatePayCurrencyID"
                FilterCriteria = "'" & Me.Choice1.Text & "'"
            Case "EHAHourlyRate"
                FilterCriteria = Me.Choice1.Text
            Case "RateAssignLocation"
                FilterCriteria = "'" & Me.Choice1.Text & "'"
            Case "RateAssignCurrencyID"
                FilterCriteria = "'" & Me.Choice1.Text & "'"
            Case "ActualDemobilisationDate"
                FilterCriteria = "'" & Me.Choice1.Text & "'"
            Case "EmployeeLogin"
                FilterCriteria = "'" & Me.Choice1.Text & "'"
            Case "EmployeeActive"
                FilterCriteria = "'" & Me.Choice1.Text & "'"
            Case Else
                FilterCriteria = "'" & Me.Choice1.Text & "'"
        End Select

        Return FilterCriteria
    End Function

    'Sort main employee dataview  based on parameters selected by user
    Private Sub SortButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SortButton.Click
        Dim lastnameAsc As String = ""
        Dim lastnameDesc As String = ""
        Dim companyidAsc As String = ""
        Dim companyidDesc As String = ""
        Dim assignedlocAsc As String = ""
        Dim assignedlocDesc As String = ""
        Dim sortchoiceAsc As String = ""
        Dim sortchoiceDesc As String = ""

        Dim LastNameSort As String = ""
        Dim CompanyIDSort As String = ""
        Dim AssignedLocSort As String = ""
        Dim SortChoiceSort As String = ""

        Dim DataSort As String = ""

        If cbLastNameASC.Checked Then
            lastnameAsc = "LastName ASC"
            LastNameSort = lastnameAsc
        End If
        If cbLastNameDESC.Checked Then
            lastnameDesc = "LastName DESC"
            LastNameSort = lastnameDesc
        End If
        If cbCompanyIDASC.Checked Then
            companyidAsc = "CompanyID ASC"
            CompanyIDSort = companyidAsc
        End If
        If cbCompanyIDDESC.Checked Then
            companyidDesc = "CompanyID DESC"
            CompanyIDSort = companyidDesc
        End If
        If cbAssignedLocASC.Checked Then
            assignedlocAsc = "RateAssignLocation ASC"
            AssignedLocSort = assignedlocAsc
        End If
        If cbAssignedLocDESC.Checked Then
            assignedlocDesc = "RateAssignLocation DESC"
            AssignedLocSort = assignedlocDesc
        End If
        If cbSortChoiceASC.Checked Then
            sortchoiceAsc = SortChoice.Text & " ASC"
            SortChoiceSort = sortchoiceAsc
        End If
        If cbSortChoiceDESC.Checked Then
            sortchoiceDesc = SortChoice.Text & " DESC"
            SortChoiceSort = sortchoiceDesc
        End If

        If (LastNamePriority.Text <> "") Or (CompanyIDPriority.Text <> "") Or (AssignedLocPriority.Text <> "") Or (SortChoicePriority.Text <> "") Then
            For I As Integer = 1 To 4
                If LastNamePriority.Text <> "" Then
                    If CInt(LastNamePriority.Text) = I Then
                        If LastNameSort <> "" Then
                            If DataSort <> "" Then
                                DataSort = DataSort & ", " & LastNameSort
                            Else
                                DataSort = LastNameSort
                            End If
                        End If
                    End If
                End If
                If CompanyIDPriority.Text <> "" Then
                    If CInt(CompanyIDPriority.Text) = I Then
                        If CompanyIDSort <> "" Then
                            If DataSort <> "" Then
                                DataSort = DataSort & ", " & CompanyIDSort
                            Else
                                DataSort = CompanyIDSort
                            End If
                        End If
                    End If
                End If
                If AssignedLocPriority.Text <> "" Then
                    If CInt(AssignedLocPriority.Text) = I Then
                        If AssignedLocSort <> "" Then
                            If DataSort <> "" Then
                                DataSort = DataSort & ", " & AssignedLocSort
                            Else
                                DataSort = AssignedLocSort
                            End If
                        End If
                    End If
                End If
                If SortChoicePriority.Text <> "" Then
                    If CInt(SortChoicePriority.Text) = I Then
                        If SortChoiceSort <> "" Then
                            If DataSort <> "" Then
                                DataSort = DataSort & ", " & SortChoiceSort
                            Else
                                DataSort = SortChoiceSort
                            End If
                        End If
                    End If
                End If
            Next
            DataView.Sort = DataSort
        Else
            MsgBox("Please Set Priorities Before Sorting!", MsgBoxStyle.Exclamation, "Information")
        End If
    End Sub

    'Clear sort parameters and restore Main employee dataview to original row state
    Private Sub SortClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SortClear.Click
        cbLastNameASC.Checked = False
        cbLastNameDESC.Checked = False
        cbCompanyIDASC.Checked = False
        cbCompanyIDDESC.Checked = False
        cbAssignedLocASC.Checked = False
        cbAssignedLocDESC.Checked = False
        cbSortChoiceASC.Checked = False
        cbSortChoiceDESC.Checked = False

        LastNamePriority.Text = ""
        CompanyIDPriority.Text = ""
        AssignedLocPriority.Text = ""
        SortChoicePriority.Text = ""
    End Sub

    'Automatically update the currency to reflect a change in either the Pay Location or Assigned Location by the user
    Private Function UpdateCurrencyID(ByVal Location As String) As String
        Dim objDataSet As DataSet
        Dim objSQLCommand As SqlClient.SqlCommand
        Dim objSQLAdapter As SqlClient.SqlDataAdapter
        Dim objDataRow As DataRow

        Try
            objSQLCommand = New SqlClient.SqlCommand
            objSQLCommand.CommandType = CommandType.Text
            objSQLCommand.CommandText = "SELECT CurrencyID FROM Location WHERE (LocationName = '" & Location & "')"
            objSQLCommand.Connection = PrivilegedSQLConnection
            objSQLAdapter = New SqlClient.SqlDataAdapter(objSQLCommand)
            objDataSet = New DataSet("Location")
            objSQLAdapter.Fill(objDataSet, "Location")

            For Each objDataRow In objDataSet.Tables("Location").Rows
                Return objDataRow.Item("CurrencyID").ToString
            Next

        Catch ex As Exception
            MsgBox(ex.Message)

        End Try
        Return ""
    End Function

    'Before closing form, if their is changes to the Main employee dataset, prompt the user to save or
    'cancel changes
    Private Sub EmployeeForm_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        If IsOKToCancelExistingChanges() Then
            e.Cancel = False
        Else
            e.Cancel = True
        End If
    End Sub

    'Determine if it is ok to cancel current changes to Main employee dataset
    Private Function IsOKToCancelExistingChanges() As Boolean
        EmployeeBindingContext.Position -= 1
        EmployeeBindingContext.Position += 1

        If Me.EmployeeListDataSet.HasChanges Then
            Dim response As MsgBoxResult
            response = MsgBox("Do you want to cancel your changes?", MsgBoxStyle.YesNo, "Please confirm")
            If response = MsgBoxResult.Yes Then
                Me.EmployeeListDataSet.RejectChanges()
                Return True
            Else
                Return False
            End If
        End If
        Return True
    End Function

    'Significant employee field changed on Employee Form tab
    Private Sub AssignedLocation_SelectionChangeCommitted(ByVal sender As Object, ByVal e As System.EventArgs) Handles AssignedLocation.SelectionChangeCommitted

        'Automatically change currency to match user-changed location
        Me.RateAssignCurrency.Text = UpdateCurrencyID(Me.AssignedLocation.Text)
    End Sub

    'Significant employee field changed on Employee Form tab
    Private Sub PayLocation_SelectionChangeCommitted(ByVal sender As Object, ByVal e As System.EventArgs) Handles PayLocation.SelectionChangeCommitted
        Dim answer As MsgBoxResult
        'Warn user that they must change pay rates after updating the Pay Location field, or a trigger in the 
        'database will rollback their changes
        answer = MsgBox("You have just Modified the Rate Py Location Field. You must now Modify Both Hourly Rate Fields or ALL Changes will be Lost!" & vbCrLf & vbCrLf & "Please also Update the Effective Date Field.", MsgBoxStyle.Critical, "Warning")
        'Automatically change currency to match user-changed location
        Me.RatePayCurrency.Text = UpdateCurrencyID(Me.PayLocation.Text)
    End Sub

    'Dispose Employee Form
    Private Sub EmployeeForm_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Closed
        MyEmployeeForm = Nothing
    End Sub

    'Save changed employee data when going between tabs
    Private Sub TabControl_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabControl.SelectedIndexChanged
        If Me.EmployeeListDataSet.HasChanges Then
            Me.SaveRecord_Click(Nothing, Nothing)
        End If
    End Sub

    'If any employee date field is null, then instead of displaying the default date of Now, display a date significantly
    'in the past to show the user that the field is currently not specified
    Private Sub StartDate_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles StartDate.ValueChanged
        If StartDate.Value = Now Then
            'StartDate.Value = New DateTime(2007, 10, 1)
            'Grey out the datetimepicker if the field value is null
            StartDate.Checked = True
            StartDate.Checked = False
        End If
    End Sub

    'If any employee date field is null, then instead of displaying the default date of Now, display a date significantly
    'in the past to show the user that the field is currently not specified
    Private Sub EffectiveDate_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles EffectiveDate.ValueChanged
        If EffectiveDate.Value = Now Then
            'EffectiveDate.Value = New DateTime(2007, 10, 1)
            'Grey out the datetimepicker if the field value is null
            EffectiveDate.Checked = True
            EffectiveDate.Checked = False
        End If
        If Not IsNothing(EmployeeBindingContext) Then
            Debug.Print(EmployeeBindingContext.Position.ToString)
        End If
    End Sub
    'If any employee date field is null, then instead of displaying the default date of Now, display a date significantly
    'in the past to show the user that the field is currently not specified
    Private Sub Actual_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Actual.ValueChanged
        If Actual.Value = Now Then
            'Actual.Value = New DateTime(2007, 10, 1)
            'Grey out the datetimepicker if the field value is null
            Actual.Checked = True
            Actual.Checked = False
        End If
    End Sub

    'Datagrid datetimepicker popup value changed
    Private Sub dtp_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtp.ValueChanged
        If dtp.Value <> New DateTime(2007, 10, 1) Then
            If IsDBNull(DataGrid3(hitTestGrid.Row, hitTestGrid.Column)) Then
                EmployeeDataGrid(hitTestGrid.Row, hitTestGrid.Column) = dtp.Value
            ElseIf CDate(DataGrid3(hitTestGrid.Row, hitTestGrid.Column)) <> dtp.Value Then
                EmployeeDataGrid(hitTestGrid.Row, hitTestGrid.Column) = dtp.Value
            End If
        End If
    End Sub

    'Dynaically resize all columns in the datagrid to fit all entries
    Public Sub AutoSizeCol(ByVal col As Integer)
        Dim width As Single
        width = 0
        Dim numRows As Integer
        numRows = DataView.Count
        Dim g As Graphics
        g = Graphics.FromHwnd(Me.EmployeeDataGrid.Handle)
        Dim sf As StringFormat
        sf = New StringFormat(StringFormat.GenericTypographic)
        Dim size As SizeF
        Dim OriginalWidth As SizeF
        Dim i As Integer
        i = 0

        OriginalWidth = g.MeasureString(Me.EmployeeDataGrid.TableStyles(0).GridColumnStyles(col).HeaderText.ToString, Me.EmployeeDataGrid.Font, 500, sf)

        Do While (i < numRows)
            size = g.MeasureString(Me.EmployeeDataGrid(i, col).ToString, Me.EmployeeDataGrid.Font, 500, sf)
            If OriginalWidth.Width < size.Width Then
                If (size.Width > width) Then
                    width = size.Width
                End If
            End If
            i = (i + 1)
        Loop

        If width = 0 Then
            width = OriginalWidth.Width
        End If
        g.Dispose()
        width = width + 10
        Me.EmployeeDataGrid.TableStyles(0).GridColumnStyles(col).Width = CType(width, Integer)
    End Sub

    Public Sub AutoSizeTable()
        Dim numCols As Integer
        numCols = DataView.Table.Columns.Count

        Dim i As Integer
        i = 0

        Do While (i < numCols)
            AutoSizeCol(i)
            i = (i + 1)
        Loop
    End Sub

    'Enable navigation buttons when Employee table is selected
    Private Sub EmployeeDataGrid_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles EmployeeDataGrid.Click
        EnableNavigationButtons()
    End Sub

    'To prevent the user clicking backspace in the date columns of the Main employee datagrid (causing datetimepicker
    'issues), only allow the deletion of date to be set to null with a push button
    Private Sub DeleteDate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim answer As MsgBoxResult
        answer = MsgBox("You are about to Delete the Date " & EmployeeDataGrid(hitTestGrid.Row, hitTestGrid.Column).ToString & " from Column " & columnToDelete & ". Do you wish to continue?", MsgBoxStyle.YesNo, "Please confirm")

        If answer = MsgBoxResult.Yes Then
            EmployeeDataGrid(hitTestGrid.Row, hitTestGrid.Column) = DBNull.Value
        End If
    End Sub

    'Fill sort priority dropdown lists
    Private Sub populateSortPriority()
        LastNamePriority.Items.Add("")
        CompanyIDPriority.Items.Add("")
        AssignedLocPriority.Items.Add("")
        SortChoicePriority.Items.Add("")

        For I As Integer = 1 To 4
            LastNamePriority.Items.Add(I.ToString)
            CompanyIDPriority.Items.Add(I.ToString)
            AssignedLocPriority.Items.Add(I.ToString)
            SortChoicePriority.Items.Add(I.ToString)
        Next
    End Sub

    'If corresponding sort checkbox is selected, uncheck the other
    Private Sub cbLastNameASC_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbLastNameASC.CheckedChanged
        If cbLastNameASC.Checked Then
            cbLastNameDESC.Checked = False
        End If

    End Sub

    'If corresponding sort checkbox is selected, uncheck the other
    Private Sub cbLastNameDESC_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbLastNameDESC.CheckedChanged
        If cbLastNameDESC.Checked Then
            cbLastNameASC.Checked = False
        End If
    End Sub

    'If corresponding sort checkbox is selected, uncheck the other
    Private Sub cbCompanyIDASC_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbCompanyIDASC.CheckedChanged
        If cbCompanyIDASC.Checked Then
            cbCompanyIDDESC.Checked = False
        End If
    End Sub

    'If corresponding sort checkbox is selected, uncheck the other
    Private Sub cbCompanyIDDESC_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbCompanyIDDESC.CheckedChanged
        If cbCompanyIDDESC.Checked Then
            cbCompanyIDASC.Checked = False
        End If
    End Sub

    'If corresponding sort checkbox is selected, uncheck the other
    Private Sub cbAssignedLocASC_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbAssignedLocASC.CheckedChanged
        If cbAssignedLocASC.Checked Then
            cbAssignedLocDESC.Checked = False
        End If
    End Sub

    'If corresponding sort checkbox is selected, uncheck the other
    Private Sub cbAssignedLocDESC_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbAssignedLocDESC.CheckedChanged
        If cbAssignedLocDESC.Checked Then
            cbAssignedLocASC.Checked = False
        End If
    End Sub

    'If corresponding sort checkbox is selected, uncheck the other
    Private Sub cbSortChoiceASC_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbSortChoiceASC.CheckedChanged
        If cbSortChoiceASC.Checked Then
            cbSortChoiceDESC.Checked = False
        End If
    End Sub

    'If corresponding sort checkbox is selected, uncheck the other
    Private Sub cbSortChoiceDESC_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbSortChoiceDESC.CheckedChanged
        If cbSortChoiceDESC.Checked Then
            cbSortChoiceASC.Checked = False
        End If
    End Sub

    'Setup datatgrid column formats
    Private Function MyDataTableStyle() As DataGridTableStyle
        Dim DGTS As New DataGridTableStyle
        DGTS.MappingName = Me.EmployeeListDataSet.Employee.TableName

        Dim EmployeeID As New DataGridTextBoxColumn
        EmployeeID.MappingName = "EmployeeID"
        EmployeeID.HeaderText = "EmployeeID"
        EmployeeID.Width = 80
        Dim CompanyID As New DataGridTextBoxColumn
        CompanyID.MappingName = "CompanyID"
        CompanyID.HeaderText = "CompanyID"
        CompanyID.Width = 80
        Dim CompanyEmployeeNo As New DataGridTextBoxColumn
        CompanyEmployeeNo.MappingName = "CompanyEmployeeNo"
        CompanyEmployeeNo.HeaderText = "CompanyEmployeeNo"
        CompanyEmployeeNo.Width = 80
        Dim DisciplineCode As New DataGridTextBoxColumn
        DisciplineCode.MappingName = "DisciplineCode"
        DisciplineCode.HeaderText = "DisciplineCode"
        DisciplineCode.Width = 80
        Dim LastName As New DataGridTextBoxColumn
        LastName.MappingName = "LastName"
        LastName.HeaderText = "LastName"
        LastName.Width = 80
        Dim FirstName As New DataGridTextBoxColumn
        FirstName.MappingName = "FirstName"
        FirstName.HeaderText = "FirstName"
        FirstName.Width = 80
        Dim Email As New DataGridTextBoxColumn
        Email.MappingName = "Email"
        Email.HeaderText = "Email"
        Email.Width = 80
        Dim TotalHoursImported As New DataGridTextBoxColumn
        TotalHoursImported.MappingName = "TotalHoursImported"
        TotalHoursImported.HeaderText = "TotalHoursImported"
        TotalHoursImported.Width = 80
        Dim CompanyManagerID As New DataGridTextBoxColumn
        CompanyManagerID.MappingName = "CompanyManagerID"
        CompanyManagerID.HeaderText = "CompanyManagerID"
        CompanyManagerID.Width = 80
        Dim CompanyManagerName As New DataGridTextBoxColumn
        CompanyManagerName.MappingName = "CompanyManagerName"
        CompanyManagerName.HeaderText = "CompanyManagerName"
        CompanyManagerName.Width = 80
        Dim Comments As New DataGridTextBoxColumn
        Comments.MappingName = "Comments"
        Comments.HeaderText = "Comments"
        Comments.Width = 80
        Dim PAFNo As New DataGridTextBoxColumn
        PAFNo.MappingName = "PAFNo"
        PAFNo.HeaderText = "PAFNo"
        PAFNo.Width = 80
        Dim PAFRevisionNo As New DataGridTextBoxColumn
        PAFRevisionNo.MappingName = "PAFRevisionNo"
        PAFRevisionNo.HeaderText = "PAFRevisionNo"
        PAFRevisionNo.Width = 80
        Dim EffectiveDate As New DataGridTextBoxColumn
        EffectiveDate.Format = "yyyy/MM/dd"
        EffectiveDate.MappingName = "EffectiveDate"
        EffectiveDate.HeaderText = "EffectiveDate"
        EffectiveDate.Width = 80
        Dim StartDate As New DataGridTextBoxColumn
        StartDate.Format = "yyyy/MM/dd"
        StartDate.MappingName = "StartDate"
        StartDate.HeaderText = "StartDate"
        StartDate.Width = 80
        StartDate.ReadOnly = True
        Dim RatePayLocation As New DataGridTextBoxColumn
        RatePayLocation.MappingName = "RatePayLocation"
        RatePayLocation.HeaderText = "RatePayLocation"
        RatePayLocation.Width = 80
        Dim RatePayCurrencyID As New DataGridTextBoxColumn
        RatePayCurrencyID.MappingName = "RatePayCurrencyID"
        RatePayCurrencyID.HeaderText = "RatePayCurrencyID"
        RatePayCurrencyID.Width = 80
        Dim EHAHourlyRate As New DataGridTextBoxColumn
        EHAHourlyRate.MappingName = "EHAHourlyRate"
        EHAHourlyRate.HeaderText = "EHAHourlyRate"
        EHAHourlyRate.Width = 80
        Dim RateAssignLocation As New DataGridTextBoxColumn
        RateAssignLocation.MappingName = "RateAssignLocation"
        RateAssignLocation.HeaderText = "RateAssignLocation"
        RateAssignLocation.Width = 80
        Dim RateAssignCurrencyID As New DataGridTextBoxColumn
        RateAssignCurrencyID.MappingName = "RateAssignCurrencyID"
        RateAssignCurrencyID.HeaderText = "RateAssignCurrencyID"
        RateAssignCurrencyID.Width = 80
        Dim ActualDemobilisationDate As New DataGridTextBoxColumn
        ActualDemobilisationDate.MappingName = "ActualDemobilisationDate"
        ActualDemobilisationDate.HeaderText = "ActualDemobilisationDate"
        ActualDemobilisationDate.Width = 80
        ActualDemobilisationDate.ReadOnly = True
        Dim EmployeeLogin As New DataGridTextBoxColumn
        EmployeeLogin.MappingName = "EmployeeLogin"
        EmployeeLogin.HeaderText = "EmployeeLogin"
        EmployeeLogin.Width = 80
        Dim EmployeeActiveColumn As New MyBooleanColumnClass(22, False)
        EmployeeActiveColumn.MappingName = "EmployeeActive"
        EmployeeActiveColumn.HeaderText = "Active"
        EmployeeActiveColumn.Width = 10

        DGTS.GridColumnStyles.Add(EmployeeID)
        DGTS.GridColumnStyles.Add(CompanyID)
        DGTS.GridColumnStyles.Add(CompanyEmployeeNo)
        DGTS.GridColumnStyles.Add(DisciplineCode)
        DGTS.GridColumnStyles.Add(LastName)
        DGTS.GridColumnStyles.Add(FirstName)
        DGTS.GridColumnStyles.Add(Email)
        DGTS.GridColumnStyles.Add(TotalHoursImported)
        DGTS.GridColumnStyles.Add(CompanyManagerID)
        DGTS.GridColumnStyles.Add(CompanyManagerName)
        DGTS.GridColumnStyles.Add(Comments)
        DGTS.GridColumnStyles.Add(PAFNo)
        DGTS.GridColumnStyles.Add(PAFRevisionNo)
        DGTS.GridColumnStyles.Add(EffectiveDate)
        DGTS.GridColumnStyles.Add(StartDate)
        DGTS.GridColumnStyles.Add(RatePayLocation)
        DGTS.GridColumnStyles.Add(RatePayCurrencyID)
        DGTS.GridColumnStyles.Add(EHAHourlyRate)
        DGTS.GridColumnStyles.Add(RateAssignLocation)
        DGTS.GridColumnStyles.Add(RateAssignCurrencyID)
        DGTS.GridColumnStyles.Add(ActualDemobilisationDate)
        DGTS.GridColumnStyles.Add(EmployeeLogin)
        DGTS.GridColumnStyles.Add(EmployeeActiveColumn)
        Return DGTS
    End Function

End Class
