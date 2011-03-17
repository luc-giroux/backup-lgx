Public Class MainForm
    Inherits System.Windows.Forms.Form

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        SQLConnection = Me.MySqlConnection
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
    Friend WithEvents MySqlConnection As System.Data.SqlClient.SqlConnection
    Friend WithEvents MainMenu1 As System.Windows.Forms.MainMenu
    Friend WithEvents CompanyLookupDataAdapter As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents CompanyLookupSQLCommand As System.Data.SqlClient.SqlCommand
    Friend WithEvents FunctionMenu As System.Windows.Forms.MenuItem
    Friend WithEvents ExitMenu As System.Windows.Forms.MenuItem
    Friend WithEvents SqlSelectCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents CalendarDataAdapter As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents LocationSelectCommand As System.Data.SqlClient.SqlCommand
    Friend WithEvents LocationDataAdapter As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents DisciplineDataAdapter As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents CostAdjustmentReasonSelectCommand As System.Data.SqlClient.SqlCommand
    Friend WithEvents CostAdjustmentReasonDataAdapter As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents CurrencySelectCommand As System.Data.SqlClient.SqlCommand
    Friend WithEvents CurrencyDataAdapter As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents EmployeeSelectCommand As System.Data.SqlClient.SqlCommand
    Friend WithEvents EmployeeDataAdapter As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents CostsAdjustmentsMenuItem As System.Windows.Forms.MenuItem
    Friend WithEvents InvoiceEditionMenuItem As System.Windows.Forms.MenuItem
    Friend WithEvents BillingMonthDataAdapter As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents BillingMonthSQLSelectCmd As System.Data.SqlClient.SqlCommand
    Friend WithEvents MenuItem1 As System.Windows.Forms.MenuItem
    Friend WithEvents EmployeeMenuItem As System.Windows.Forms.MenuItem
    Friend WithEvents DisciplineSqlDataAdapter As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents DisciplineSqlSelectCommand As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlConnection1 As System.Data.SqlClient.SqlConnection
    Friend WithEvents LocationSqlDataAdapter As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents LocationSqlSelectCommand As System.Data.SqlClient.SqlCommand
    Friend WithEvents AssignedLocationSqlDataAdapter As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents AssignedLocationSqlSelectCommand As System.Data.SqlClient.SqlCommand
    Friend WithEvents ContractsSqlDataAdapter As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents ContractsSqlSelectCommand As System.Data.SqlClient.SqlCommand
    Friend WithEvents NewLookupDataset As PAF.NewLookupDataset
    Friend WithEvents Timesheet_Load_MenuItem As System.Windows.Forms.MenuItem
    Friend WithEvents TimesheetMenuItem As System.Windows.Forms.MenuItem
    Friend WithEvents SqlSelectCommand3 As System.Data.SqlClient.SqlCommand
    Friend WithEvents OpCentreSqlDataAdapter As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents LoadSAP As System.Windows.Forms.MenuItem
    Friend WithEvents SqlSelectCommand2 As System.Data.SqlClient.SqlCommand
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainForm))
        Me.MySqlConnection = New System.Data.SqlClient.SqlConnection()
        Me.MainMenu1 = New System.Windows.Forms.MainMenu(Me.components)
        Me.FunctionMenu = New System.Windows.Forms.MenuItem()
        Me.CostsAdjustmentsMenuItem = New System.Windows.Forms.MenuItem()
        Me.InvoiceEditionMenuItem = New System.Windows.Forms.MenuItem()
        Me.ExitMenu = New System.Windows.Forms.MenuItem()
        Me.MenuItem1 = New System.Windows.Forms.MenuItem()
        Me.EmployeeMenuItem = New System.Windows.Forms.MenuItem()
        Me.TimesheetMenuItem = New System.Windows.Forms.MenuItem()
        Me.Timesheet_Load_MenuItem = New System.Windows.Forms.MenuItem()
        Me.LoadSAP = New System.Windows.Forms.MenuItem()
        Me.CompanyLookupDataAdapter = New System.Data.SqlClient.SqlDataAdapter()
        Me.CompanyLookupSQLCommand = New System.Data.SqlClient.SqlCommand()
        Me.SqlSelectCommand1 = New System.Data.SqlClient.SqlCommand()
        Me.CalendarDataAdapter = New System.Data.SqlClient.SqlDataAdapter()
        Me.LocationSelectCommand = New System.Data.SqlClient.SqlCommand()
        Me.LocationDataAdapter = New System.Data.SqlClient.SqlDataAdapter()
        Me.CostAdjustmentReasonSelectCommand = New System.Data.SqlClient.SqlCommand()
        Me.CostAdjustmentReasonDataAdapter = New System.Data.SqlClient.SqlDataAdapter()
        Me.CurrencySelectCommand = New System.Data.SqlClient.SqlCommand()
        Me.CurrencyDataAdapter = New System.Data.SqlClient.SqlDataAdapter()
        Me.EmployeeSelectCommand = New System.Data.SqlClient.SqlCommand()
        Me.EmployeeDataAdapter = New System.Data.SqlClient.SqlDataAdapter()
        Me.BillingMonthDataAdapter = New System.Data.SqlClient.SqlDataAdapter()
        Me.BillingMonthSQLSelectCmd = New System.Data.SqlClient.SqlCommand()
        Me.DisciplineSqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter()
        Me.DisciplineSqlSelectCommand = New System.Data.SqlClient.SqlCommand()
        Me.SqlConnection1 = New System.Data.SqlClient.SqlConnection()
        Me.LocationSqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter()
        Me.LocationSqlSelectCommand = New System.Data.SqlClient.SqlCommand()
        Me.AssignedLocationSqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter()
        Me.AssignedLocationSqlSelectCommand = New System.Data.SqlClient.SqlCommand()
        Me.ContractsSqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter()
        Me.ContractsSqlSelectCommand = New System.Data.SqlClient.SqlCommand()
        Me.NewLookupDataset = New PAF.NewLookupDataset()
        Me.SqlSelectCommand3 = New System.Data.SqlClient.SqlCommand()
        Me.SqlSelectCommand2 = New System.Data.SqlClient.SqlCommand()
        CType(Me.NewLookupDataset, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'MySqlConnection
        '
        Me.MySqlConnection.FireInfoMessageEventOnUserErrors = False
        '
        'MainMenu1
        '
        Me.MainMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.FunctionMenu, Me.MenuItem1, Me.TimesheetMenuItem})
        '
        'FunctionMenu
        '
        Me.FunctionMenu.Index = 0
        Me.FunctionMenu.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.CostsAdjustmentsMenuItem, Me.InvoiceEditionMenuItem, Me.ExitMenu})
        Me.FunctionMenu.Text = "Function"
        '
        'CostsAdjustmentsMenuItem
        '
        Me.CostsAdjustmentsMenuItem.Index = 0
        Me.CostsAdjustmentsMenuItem.Text = "Cost Adjustments"
        '
        'InvoiceEditionMenuItem
        '
        Me.InvoiceEditionMenuItem.Index = 1
        Me.InvoiceEditionMenuItem.Text = "Invoice Edition"
        '
        'ExitMenu
        '
        Me.ExitMenu.Index = 2
        Me.ExitMenu.Text = "Exit"
        '
        'MenuItem1
        '
        Me.MenuItem1.Index = 1
        Me.MenuItem1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.EmployeeMenuItem})
        Me.MenuItem1.Text = "Employee"
        '
        'EmployeeMenuItem
        '
        Me.EmployeeMenuItem.Index = 0
        Me.EmployeeMenuItem.Text = "EmployeeForm"
        '
        'TimesheetMenuItem
        '
        Me.TimesheetMenuItem.Index = 2
        Me.TimesheetMenuItem.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.Timesheet_Load_MenuItem, Me.LoadSAP})
        Me.TimesheetMenuItem.Text = "Timesheet"
        '
        'Timesheet_Load_MenuItem
        '
        Me.Timesheet_Load_MenuItem.Index = 0
        Me.Timesheet_Load_MenuItem.Text = "Load"
        '
        'LoadSAP
        '
        Me.LoadSAP.Index = 1
        Me.LoadSAP.Text = "Load SAP for reconciliation"
        '
        'CompanyLookupDataAdapter
        '
        Me.CompanyLookupDataAdapter.SelectCommand = Me.CompanyLookupSQLCommand
        Me.CompanyLookupDataAdapter.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "Company", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("CompanyID", "CompanyID"), New System.Data.Common.DataColumnMapping("CompanyName", "CompanyName")})})
        '
        'CompanyLookupSQLCommand
        '
        Me.CompanyLookupSQLCommand.CommandText = "SELECT CompanyID, CompanyName FROM Company"
        Me.CompanyLookupSQLCommand.Connection = Me.MySqlConnection       
        '
        'CalendarDataAdapter
        '
        Me.CalendarDataAdapter.SelectCommand = Me.SqlSelectCommand1
        Me.CalendarDataAdapter.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "Calendar", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("ProjectWeekEndingDate", "ProjectWeekEndingDate"), New System.Data.Common.DataColumnMapping("ProjectBillingMonth", "ProjectBillingMonth")})})
        '
        'LocationSelectCommand
        '
        Me.LocationSelectCommand.CommandText = "SELECT LocationID, LocationName, CurrencyID FROM Location"
        Me.LocationSelectCommand.Connection = Me.MySqlConnection
        '
        'LocationDataAdapter
        '
        Me.LocationDataAdapter.SelectCommand = Me.LocationSelectCommand
        Me.LocationDataAdapter.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "Location", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("LocationID", "LocationID"), New System.Data.Common.DataColumnMapping("LocationName", "LocationName"), New System.Data.Common.DataColumnMapping("CurrencyID", "CurrencyID")})})     
        '
        'CostAdjustmentReasonSelectCommand
        '
        Me.CostAdjustmentReasonSelectCommand.CommandText = "SELECT ShortComment FROM CostAdjustmentReasons"
        Me.CostAdjustmentReasonSelectCommand.Connection = Me.MySqlConnection
        '
        'CostAdjustmentReasonDataAdapter
        '
        Me.CostAdjustmentReasonDataAdapter.SelectCommand = Me.CostAdjustmentReasonSelectCommand
        Me.CostAdjustmentReasonDataAdapter.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "CostAdjustmentReasons", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("ShortComment", "ShortComment")})})
        '
        'CurrencySelectCommand
        '
        Me.CurrencySelectCommand.CommandText = "SELECT CurrencyID, Description, CurrencyToProjectrate, Comments FROM Currency"
        Me.CurrencySelectCommand.Connection = Me.MySqlConnection
        '
        'CurrencyDataAdapter
        '
        Me.CurrencyDataAdapter.SelectCommand = Me.CurrencySelectCommand
        Me.CurrencyDataAdapter.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "Currency", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("CurrencyID", "CurrencyID"), New System.Data.Common.DataColumnMapping("Description", "Description"), New System.Data.Common.DataColumnMapping("CurrencyToProjectrate", "CurrencyToProjectrate"), New System.Data.Common.DataColumnMapping("Comments", "Comments")})})
        '
        'EmployeeSelectCommand
        '
        Me.EmployeeSelectCommand.CommandText = resources.GetString("EmployeeSelectCommand.CommandText")
        Me.EmployeeSelectCommand.Connection = Me.MySqlConnection
        '
        'EmployeeDataAdapter
        '
        Me.EmployeeDataAdapter.SelectCommand = Me.EmployeeSelectCommand
        Me.EmployeeDataAdapter.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "Employee", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("EmployeeID", "EmployeeID"), New System.Data.Common.DataColumnMapping("CompanyID", "CompanyID"), New System.Data.Common.DataColumnMapping("CompanyEmployeeNo", "CompanyEmployeeNo"), New System.Data.Common.DataColumnMapping("DisciplineCode", "DisciplineCode"), New System.Data.Common.DataColumnMapping("LastName", "LastName"), New System.Data.Common.DataColumnMapping("FirstName", "FirstName"), New System.Data.Common.DataColumnMapping("Email", "Email"), New System.Data.Common.DataColumnMapping("TotalHoursImported", "TotalHoursImported"), New System.Data.Common.DataColumnMapping("CompanyManagerID", "CompanyManagerID"), New System.Data.Common.DataColumnMapping("Comments", "Comments"), New System.Data.Common.DataColumnMapping("PAFNo", "PAFNo"), New System.Data.Common.DataColumnMapping("StartDate", "StartDate"), New System.Data.Common.DataColumnMapping("RatePayLocation", "RatePayLocation"), New System.Data.Common.DataColumnMapping("RatePayCurrencyID", "RatePayCurrencyID"), New System.Data.Common.DataColumnMapping("RateAssignLocation", "RateAssignLocation"), New System.Data.Common.DataColumnMapping("RateAssignCurrencyID", "RateAssignCurrencyID"), New System.Data.Common.DataColumnMapping("ActualDemobilisationDate", "ActualDemobilisationDate"), New System.Data.Common.DataColumnMapping("FullName", "FullName")})})
        '
        'BillingMonthDataAdapter
        '
        Me.BillingMonthDataAdapter.SelectCommand = Me.BillingMonthSQLSelectCmd
        Me.BillingMonthDataAdapter.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "BillingMonth", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("ProjectBillingMonth", "ProjectBillingMonth")})})
        '
        'BillingMonthSQLSelectCmd
        '
        Me.BillingMonthSQLSelectCmd.CommandText = resources.GetString("BillingMonthSQLSelectCmd.CommandText")
        Me.BillingMonthSQLSelectCmd.Connection = Me.MySqlConnection        
        '
        'SqlConnection1
        '
        Me.SqlConnection1.FireInfoMessageEventOnUserErrors = False
        '
        'LocationSqlDataAdapter
        '
        Me.LocationSqlDataAdapter.SelectCommand = Me.LocationSqlSelectCommand
        Me.LocationSqlDataAdapter.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "Location", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("LocationID", "LocationID"), New System.Data.Common.DataColumnMapping("LocationName", "LocationName"), New System.Data.Common.DataColumnMapping("CompanyID", "CompanyID"), New System.Data.Common.DataColumnMapping("CurrencyID", "CurrencyID"), New System.Data.Common.DataColumnMapping("StandardWorkingHours", "StandardWorkingHours"), New System.Data.Common.DataColumnMapping("Multiplier", "Multiplier"), New System.Data.Common.DataColumnMapping("OtherOfficeSpace", "OtherOfficeSpace"), New System.Data.Common.DataColumnMapping("OtherOfficeExpenses", "OtherOfficeExpenses"), New System.Data.Common.DataColumnMapping("OtherIT", "OtherIT"), New System.Data.Common.DataColumnMapping("OtherITCore", "OtherITCore"), New System.Data.Common.DataColumnMapping("OtherTotal", "OtherTotal"), New System.Data.Common.DataColumnMapping("Revision", "Revision"), New System.Data.Common.DataColumnMapping("RevisionEffectiveDate", "RevisionEffectiveDate"), New System.Data.Common.DataColumnMapping("Comments", "Comments"), New System.Data.Common.DataColumnMapping("SortOrder", "SortOrder")})})
        '
        'LocationSqlSelectCommand
        '
        Me.LocationSqlSelectCommand.CommandText = resources.GetString("LocationSqlSelectCommand.CommandText")
        Me.LocationSqlSelectCommand.Connection = Me.SqlConnection1
        '
        'AssignedLocationSqlDataAdapter
        '
        Me.AssignedLocationSqlDataAdapter.SelectCommand = Me.AssignedLocationSqlSelectCommand
        Me.AssignedLocationSqlDataAdapter.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "Location", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("LocationID", "LocationID"), New System.Data.Common.DataColumnMapping("LocationName", "LocationName"), New System.Data.Common.DataColumnMapping("CurrencyID", "CurrencyID")})})
        '
        'AssignedLocationSqlSelectCommand
        '
        Me.AssignedLocationSqlSelectCommand.CommandText = "SELECT LocationID, LocationName, CurrencyID FROM Location"
        Me.AssignedLocationSqlSelectCommand.Connection = Me.SqlConnection1
        '
        'ContractsSqlDataAdapter
        '
        Me.ContractsSqlDataAdapter.SelectCommand = Me.ContractsSqlSelectCommand
        Me.ContractsSqlDataAdapter.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "Contracts", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("ContractID", "ContractID"), New System.Data.Common.DataColumnMapping("Description", "Description")})})
        '
        'ContractsSqlSelectCommand
        '
        Me.ContractsSqlSelectCommand.CommandText = "SELECT ContractID, Description FROM Contracts"
        Me.ContractsSqlSelectCommand.Connection = Me.SqlConnection1
        '
        'NewLookupDataset
        '
        Me.NewLookupDataset.DataSetName = "NewLookupDataset"
        Me.NewLookupDataset.Locale = New System.Globalization.CultureInfo("en-US")
        Me.NewLookupDataset.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'SqlSelectCommand2
        '
        Me.SqlSelectCommand2.CommandText = "SELECT LocationID, LocationName FROM Location WHERE (KeyOperatingCentre IS NOT NU" & _
            "LL)"
        Me.SqlSelectCommand2.Connection = Me.SqlConnection1
        '
        'MainForm
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(292, 273)
        Me.Menu = Me.MainMenu1
        Me.Name = "MainForm"
        Me.Text = "PAF application"
        CType(Me.NewLookupDataset, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub CostsAdjustmentsMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CostsAdjustmentsMenuItem.Click
        If IsNothing(MyCostsAdjustmentForm) Then
            Try
                MyCostsAdjustmentForm = New AdjustmentsForm
                MyCostsAdjustmentForm.ShowDialog()
            Catch ex As Exception
                MsgBox(ex.Message & vbNewLine & ex.StackTrace, MsgBoxStyle.Critical, "Application Error")
                MyCostsAdjustmentForm = Nothing
            End Try
        End If
    End Sub
    'Private Sub CodeAdjustmentsMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    If IsNothing(MyCodeAdjustmentForm) Then
    '        Try
    '            MyCodeAdjustmentForm = New CodeAdjustmentForm
    '            MyCodeAdjustmentForm.ShowDialog()
    '        Catch ex As Exception
    '            MsgBox(ex.Message & vbNewLine & ex.StackTrace, MsgBoxStyle.Critical, "Application Error")
    '            MyCodeAdjustmentForm = Nothing
    '        End Try
    '    End If
    'End Sub
    Private Sub InvoiceEditionMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles InvoiceEditionMenuItem.Click
        If IsNothing(MyInvoiceEditionForm) Then
            Try
                MyInvoiceEditionForm = New InvoiceEditionForm
                MyInvoiceEditionForm.ShowDialog()
            Catch ex As Exception
                MsgBox(ex.Message & vbNewLine & ex.StackTrace, MsgBoxStyle.Critical, "Application Error")
                MyInvoiceEditionForm = Nothing
            End Try
        End If
    End Sub

    Private Sub EmployeeMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EmployeeMenuItem.Click
        If IsNothing(MyEmployeeForm) Then
            Try
                MyEmployeeForm = New EmployeeForm
                MyEmployeeForm.ShowDialog()
            Catch ex As Exception
                MsgBox(ex.Message & vbNewLine & ex.StackTrace, MsgBoxStyle.Critical, "Application Error")
                MyEmployeeForm = Nothing
            End Try
        End If
    End Sub

    Private Sub Timesheet_Load_MenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timesheet_Load_MenuItem.Click
        If IsNothing(MyTimesheetLoadForm) Then
            Try
                MyTimesheetLoadForm = New TimesheetLoadForm
                MyTimesheetLoadForm.ShowDialog()
            Catch ex As Exception
                MsgBox(ex.Message & vbNewLine & ex.StackTrace, MsgBoxStyle.Critical, "Application Error")
                MyTimesheetLoadForm = Nothing
            End Try
        End If
    End Sub


    ' caro2009 - 2010-01-20 - new sub to change the user for the connection
    Public Sub ExecuteAsUser(ByRef c As SqlClient.SqlConnection)

        Throw New Exception("ExecuteAsUser")
    End Sub ' ExecuteAsUser

    Private Sub ExitMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExitMenu.Click
        Me.Close()
    End Sub
    Private Sub MainForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim i As Integer
        Config = New ConfigClass
        ChangeSecurityContext(MySqlConnection)
        ChangeSecurityContext(SqlConnection1)

        i = CompanyLookupDataAdapter.Fill(NewLookupDataset.Company)

        i = CurrencyDataAdapter.Fill(NewLookupDataset.Currency)

        i = LocationDataAdapter.Fill(NewLookupDataset.Location)

        i = EmployeeDataAdapter.Fill(NewLookupDataset.Employee)

        i = AssignedLocationSqlDataAdapter.Fill(NewLookupDataset.AssignedLocation)

        i = ContractsSqlDataAdapter.Fill(NewLookupDataset.Contracts)

        MyLookupDataset = NewLookupDataset
        MyCompanyLookupDataAdapter = CompanyLookupDataAdapter

        Dim SecurityCheck As New SQLPermission
        Me.CostsAdjustmentsMenuItem.Enabled = SecurityCheck.IsMemberOf("PAF_FINANCE")
        Me.InvoiceEditionMenuItem.Enabled = SecurityCheck.IsMemberOf("PAF_FINANCE")
        Me.EmployeeMenuItem.Enabled = SecurityCheck.IsMemberOf("PAF_FINANCE")
        Me.Timesheet_Load_MenuItem.Enabled = False
        If SecurityCheck.IsMemberOf("PAF_FINANCE") Then
            i = CostAdjustmentReasonDataAdapter.Fill(NewLookupDataset.CostAdjustmentsReasons)
        End If
    End Sub

    Private Sub LoadSAP_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LoadSAP.Click
        If IsNothing(MySAPTimesheetLoadForm) Then
            Try
                MySAPTimesheetLoadForm = New SAPTimesheetLoadForm
                MySAPTimesheetLoadForm.ShowDialog()
            Catch ex As Exception
                MsgBox(ex.Message & vbNewLine & ex.StackTrace, MsgBoxStyle.Critical, "Application Error")
                MySAPTimesheetLoadForm = Nothing
            End Try
        End If

    End Sub

End Class
Public Module MainModule
    Public SQLConnection As SqlClient.SqlConnection
    Public MyLookupDataset As NewLookupDataset
    Public MyCostsAdjustmentForm As AdjustmentsForm = Nothing
    'Public MyCodeAdjustmentForm As CodeAdjustmentForm = Nothing
    Public MyInvoiceEditionForm As InvoiceEditionForm = Nothing
    Public MyEmployeeForm As EmployeeForm = Nothing
    Public MyTimesheetLoadForm As TimesheetLoadForm = Nothing
    Public MySAPTimesheetLoadForm As SAPTimesheetLoadForm = Nothing
    Public MyCompanyLookupDataAdapter As System.Data.SqlClient.SqlDataAdapter
    Public Config As ConfigClass
    Public Function GetSqlConnection() As SqlClient.SqlConnection
        Dim ConnectionString As String = Config.SQLConnectionString
        GetSqlConnection = New SqlClient.SqlConnection(ConnectionString)
        ChangeSecurityContext(GetSqlConnection)
    End Function
    Public Sub ChangeSecurityContext(ByVal C As SqlClient.SqlConnection)
        Dim cmd As SqlClient.SqlCommand
        Dim CommandText As String
        CommandText = "EXECUTE AS USER = '" & System.Environment.UserDomainName.ToUpper & "\" & System.Environment.UserName.ToUpper & "'"

        cmd = New SqlClient.SqlCommand(CommandText, C)
        If C.ConnectionString = "" Then
            C.ConnectionString = Config.SQLConnectionString
        End If
        If Not C.State = ConnectionState.Open Then
            C.Open()
        End If
        cmd.ExecuteNonQuery()
        cmd.Dispose()
    End Sub
End Module
Public Class SQLPermission
    Private Shared AlreadyParsedGroups As Collections.Specialized.StringCollection
    Private Shared Conn As SqlClient.SqlConnection
    Public Sub New()
        AlreadyParsedGroups = New Collections.Specialized.StringCollection
        Conn = GetSqlConnection()
    End Sub
    Private Function l_IsMemberOf(ByVal GroupName As String) As Boolean
        ' note that this function is ONE level only, 
        ' i.e. do not check for indirect group membership
        Dim username As String
        Dim MemberName As String
        username = System.Environment.UserName.ToUpper
        'username = "JCARON"
        l_IsMemberOf = False
        Try
            Dim cmd As New SqlClient.SqlCommand("sp_helprolemember " & GroupName, Conn)
            Dim sqlread As SqlClient.SqlDataReader = cmd.ExecuteReader()
            While sqlread.Read() And Not l_IsMemberOf
                MemberName = CType(sqlread.Item("MemberName"), String)
                If MemberName.ToUpper.EndsWith(username) Then
                    l_IsMemberOf = True
                End If
            End While
            sqlread.Close()

        Catch ex As Exception
            Return False
        End Try

    End Function
    Public Function IsMemberOf(ByVal GroupName As String) As Boolean
        AlreadyParsedGroups.Clear()
        Return Me.l_IsMemberOf(GroupName)
    End Function
    Public Sub Dispose()
        If Conn.State = ConnectionState.Open Then
            Conn.Close()
            Conn.Dispose()
            AlreadyParsedGroups.Clear()
        End If
    End Sub
End Class

Public Class ConfigClass
    Public ReadOnly SQLConnectionString As String
    'Public ReadOnly SchemaFolder As String
    Public Sub New()
        Dim config As New System.Configuration.AppSettingsReader
        ' Reading the connection string for the database
        SQLConnectionString = CType(config.GetValue("SQL.ConnectionString", GetType(String)), String)
        ' caro2009 - 2010-01-20 - added hardcoded account information
        If Not SQLConnectionString.Contains("SPPI") And Not SQLConnectionString.Contains("User") Then
            'SQLConnectionString = SQLConnectionString & ";User=CMAgent;Password=20Tnegamc10"
            SQLConnectionString = SQLConnectionString & ";User=TestImpersonate;Password=TestImpersonate"
        End If
        'SchemaFolder = CType(config.GetValue("SchemaFolder", GetType(String)), String)
    End Sub
End Class
Public Interface FilterableForm
    Sub SetFilter(ByVal ColumnNumber As Integer, ByVal value As String)
End Interface