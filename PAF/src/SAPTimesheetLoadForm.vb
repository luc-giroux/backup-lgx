Imports System.IO
Imports System.Globalization

Structure StatusStruct
    Dim StatusString As String
    Dim Working As Boolean
    Dim MaxProgressValue As Integer
    Dim ProgressValue As Integer
End Structure
Public Class SAPTimesheetLoadForm
    Inherits System.Windows.Forms.Form

    Private PrivilegedSQLConnection As SqlClient.SqlConnection
    Private nfi As NumberFormatInfo = New CultureInfo("en-US", False).NumberFormat    

    Private Sub InitializeDataset()
        Me.SqlSelectCommand = New System.Data.SqlClient.SqlCommand
        Me.SqlInsertCommand = New System.Data.SqlClient.SqlCommand
        Me.TimesheetSqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter
        Me.TimesheetLoadDS = New SAPTimesheetLoad
        CType(Me.TimesheetLoadDS, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'SqlSelectCommand - Note that this is never used, only inserts
        '
        Me.SqlSelectCommand.CommandText = "SELECT [EmployeeNumber], [EmployeeName], [EmployeeID], [WeekEndDate], [SAPHours], [EmployeeExistsInPAF], [PAFHours], [NetworkOPNO] FROM [dbo].[SAPTimesheets]"
        Me.SqlSelectCommand.Connection = PrivilegedSQLConnection
        '
        'SqlInsertCommand
        '
        Me.SqlInsertCommand.CommandType = CommandType.Text
        'Me.SqlInsertCommand.CommandText = _
        '    "INSERT INTO [SAPTimesheets] (EmployeeNumber, EmployeeName, EmployeeID, WeekEndDate, SAPHours, EmployeeExistsInPAF, PAFHours, PAFTimesheetHours)" + _
        '    " VALUES (@EmployeeNumber, @EmployeeName, @EmployeeID, @WeekEndDate, @SAPHours, @EmployeeExistsInPAF, @PAFHours, @PAFTimesheetHours)"
        Me.SqlInsertCommand.CommandText = _
            "INSERT INTO [SAPTimesheets] (EmployeeNumber, EmployeeName, EmployeeID, WeekEndDate, SAPHours, EmployeeExistsInPAF, PAFHours, PAFTimesheetHours, NetworkOPNO, NetworkCodeNotMatch, SAPCurrencyID, RatePayLocation, RatePayCurrencyID)" + _
            " VALUES (@EmployeeNumber, @EmployeeName, @EmployeeID, @WeekEndDate, @SAPHours, @EmployeeExistsInPAF, @PAFHours, @PAFTimesheetHours, @NetworkOPNO, @NetworkCodeNotMatch,@SAPCurrencyID,@RatePayLocation, @RatePayCurrencyID)"
        Me.SqlInsertCommand.Parameters.Add( _
            New System.Data.SqlClient.SqlParameter("@EmployeeNumber", _
                                                    System.Data.SqlDbType.VarChar, 6, "EmployeeNumber"))
        Me.SqlInsertCommand.Parameters.Add( _
            New System.Data.SqlClient.SqlParameter("@EmployeeName", _
                                                    System.Data.SqlDbType.VarChar, 100, "EmployeeName"))
        Me.SqlInsertCommand.Parameters.Add( _
            New System.Data.SqlClient.SqlParameter("@EmployeeID", _
                                                    System.Data.SqlDbType.Int, 8, "EmployeeID"))
        Me.SqlInsertCommand.Parameters.Add( _
            New System.Data.SqlClient.SqlParameter("@WeekEndDate", _
                                                    System.Data.SqlDbType.DateTime, 8, "WeekEndDate"))
        Me.SqlInsertCommand.Parameters.Add( _
            New System.Data.SqlClient.SqlParameter("@SAPHours", _
                                                    System.Data.SqlDbType.Float, 8, "SAPHours"))
        Me.SqlInsertCommand.Parameters.Add( _
            New System.Data.SqlClient.SqlParameter("@EmployeeExistsInPAF", _
                                                    System.Data.SqlDbType.Bit, 1, "EmployeeExistsInPAF"))
        Me.SqlInsertCommand.Parameters.Add( _
            New System.Data.SqlClient.SqlParameter("@PAFHours", _
                                                    System.Data.SqlDbType.Float, 8, "PAFHours"))
        Me.SqlInsertCommand.Parameters.Add( _
            New System.Data.SqlClient.SqlParameter("@PAFTimesheetHours", _
                                                    System.Data.SqlDbType.Float, 8, "PAFTimesheetHours"))
        Me.SqlInsertCommand.Parameters.Add( _
                    New System.Data.SqlClient.SqlParameter("@NetworkOPNO", _
                                                            System.Data.SqlDbType.Text, 8, "NetworkOPNO"))
        Me.SqlInsertCommand.Parameters.Add( _
                    New System.Data.SqlClient.SqlParameter("@NetworkCodeNotMatch", _
                                                            System.Data.SqlDbType.Bit, 1, "NetworkCodeNotMatch"))
        Me.SqlInsertCommand.Parameters.Add( _
                    New System.Data.SqlClient.SqlParameter("@SAPCurrencyID", _
                                                            System.Data.SqlDbType.VarChar, 3, "SAPCurrencyID"))
        Me.SqlInsertCommand.Parameters.Add( _
                    New System.Data.SqlClient.SqlParameter("@RatePayLocation", _
                                                            System.Data.SqlDbType.VarChar, 5, "RatePayLocation"))
        Me.SqlInsertCommand.Parameters.Add( _
                    New System.Data.SqlClient.SqlParameter("@RatePayCurrencyID", _
                                                            System.Data.SqlDbType.VarChar, 3, "RatePayCurrencyID"))

        '
        'TimesheetSqlDataAdapter
        '
        Me.TimesheetSqlDataAdapter.InsertCommand = Me.SqlInsertCommand
        Me.TimesheetSqlDataAdapter.SelectCommand = Me.SqlSelectCommand

        'Database to Dataset Mappings
        Me.TimesheetSqlDataAdapter.TableMappings.AddRange( _
        New System.Data.Common.DataTableMapping() { _
        New System.Data.Common.DataTableMapping("Table", "SAPTimesheets", New System.Data.Common.DataColumnMapping() { _
            New System.Data.Common.DataColumnMapping("EmployeeNumber", "EmployeeNumber"), _
            New System.Data.Common.DataColumnMapping("EmployeeName", "EmployeeName"), _
            New System.Data.Common.DataColumnMapping("EmployeeID", "EmployeeID"), _
            New System.Data.Common.DataColumnMapping("WeekEndDate", "WeekEndDate"), _
            New System.Data.Common.DataColumnMapping("SAPHours", "SAPHours"), _
                        New System.Data.Common.DataColumnMapping("EmployeeExistsInPAF", "EmployeeExistsInPAF"), _
            New System.Data.Common.DataColumnMapping("EntryID", "EntryID"), _
            New System.Data.Common.DataColumnMapping("EntryLocation", "EntryLocation"), _
            New System.Data.Common.DataColumnMapping("PAFHours", "PAFHours"), _
            New System.Data.Common.DataColumnMapping("NetworkOPNO", "NetworkOPNO"), _
            New System.Data.Common.DataColumnMapping("NetworkCodeNotMatch", "NetworkCodeNotMatch"), _
            New System.Data.Common.DataColumnMapping("SAPCurrencyID", "SAPCurrencyID"), _
            New System.Data.Common.DataColumnMapping("RatePayLocation", "RatePayLocation"), _
            New System.Data.Common.DataColumnMapping("RatePayCurrencyID", "RatePayCurrencyID")})})

        'TimesheetLoadDataSet
        '
        Me.TimesheetLoadDS.Locale = New System.Globalization.CultureInfo("en-AU")

        CType(Me.TimesheetLoadDS, System.ComponentModel.ISupportInitialize).EndInit()
    End Sub

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        Me.InitializeDataset()

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
    Friend WithEvents TimesheetLoadDS As SAPTimesheetLoad
    Friend WithEvents TimesheetSqlDataAdapter As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents LoadFile As System.Windows.Forms.TextBox
    Friend WithEvents LoadData As System.Windows.Forms.Button
    Friend WithEvents ClearBeforeLoadCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents ProgressBar As System.Windows.Forms.ToolStripProgressBar
    Friend WithEvents ToolStripStatusLabel1 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents FileDialogButton As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.LoadData = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.LoadFile = New System.Windows.Forms.TextBox
        Me.FileDialogButton = New System.Windows.Forms.Button
        Me.TimesheetLoadDS = New PAF.SAPTimesheetLoad
        Me.ClearBeforeLoadCheckBox = New System.Windows.Forms.CheckBox
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip
        Me.ProgressBar = New System.Windows.Forms.ToolStripProgressBar
        Me.ToolStripStatusLabel1 = New System.Windows.Forms.ToolStripStatusLabel
        CType(Me.TimesheetLoadDS, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.StatusStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'LoadData
        '
        Me.LoadData.Location = New System.Drawing.Point(280, 16)
        Me.LoadData.Name = "LoadData"
        Me.LoadData.Size = New System.Drawing.Size(72, 24)
        Me.LoadData.TabIndex = 1
        Me.LoadData.Text = "Load"
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(38, 17)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(48, 16)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Load:"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'LoadFile
        '
        Me.LoadFile.Location = New System.Drawing.Point(104, 16)
        Me.LoadFile.Name = "LoadFile"
        Me.LoadFile.Size = New System.Drawing.Size(152, 20)
        Me.LoadFile.TabIndex = 4
        '
        'FileDialogButton
        '
        Me.FileDialogButton.Location = New System.Drawing.Point(256, 16)
        Me.FileDialogButton.Name = "FileDialogButton"
        Me.FileDialogButton.Size = New System.Drawing.Size(24, 24)
        Me.FileDialogButton.TabIndex = 6
        Me.FileDialogButton.Text = "..."
        '
        'TimesheetLoadDS
        '
        Me.TimesheetLoadDS.DataSetName = "SAPTimesheetLoad"
        Me.TimesheetLoadDS.Locale = New System.Globalization.CultureInfo("en-US")
        Me.TimesheetLoadDS.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'ClearBeforeLoadCheckBox
        '
        Me.ClearBeforeLoadCheckBox.AutoSize = True
        Me.ClearBeforeLoadCheckBox.Checked = True
        Me.ClearBeforeLoadCheckBox.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ClearBeforeLoadCheckBox.Location = New System.Drawing.Point(104, 56)
        Me.ClearBeforeLoadCheckBox.Name = "ClearBeforeLoadCheckBox"
        Me.ClearBeforeLoadCheckBox.Size = New System.Drawing.Size(132, 17)
        Me.ClearBeforeLoadCheckBox.TabIndex = 7
        Me.ClearBeforeLoadCheckBox.Text = "Clear table before load"
        Me.ClearBeforeLoadCheckBox.UseVisualStyleBackColor = True
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ProgressBar, Me.ToolStripStatusLabel1})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 76)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(360, 22)
        Me.StatusStrip1.TabIndex = 8
        Me.StatusStrip1.Text = "SAPStatusStrip"
        '
        'ProgressBar
        '
        Me.ProgressBar.Name = "ProgressBar"
        Me.ProgressBar.Size = New System.Drawing.Size(200, 16)
        Me.ProgressBar.Step = 1
        Me.ProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous
        '
        'ToolStripStatusLabel1
        '
        Me.ToolStripStatusLabel1.Name = "ToolStripStatusLabel1"
        Me.ToolStripStatusLabel1.Size = New System.Drawing.Size(0, 17)
        '
        'SAPTimesheetLoadForm
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(360, 98)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.ClearBeforeLoadCheckBox)
        Me.Controls.Add(Me.FileDialogButton)
        Me.Controls.Add(Me.LoadFile)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.LoadData)
        Me.Name = "SAPTimesheetLoadForm"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.Text = "SAP Timesheet Load"
        CType(Me.TimesheetLoadDS, System.ComponentModel.ISupportInitialize).EndInit()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    'Specific database application role execution to save timesheet data to database
    Private Sub EnableRole(ByVal c As SqlClient.SqlConnection)

        Try
            'Create separate connection, additional to main connection, for execution of application role
            'Fire application role

            Dim cmd As New SqlClient.SqlCommand("sp_setapprole PAF_TIMESHEET_LOAD_APPLICATION, 'paf'", c)
            cmd.ExecuteNonQuery()
        Catch ex As SqlClient.SqlException
            'If timesheets save process fails, roll back transaction
            If ex.Number <> 2762 Then ' already enabled
                MsgBox(ex.Message, MsgBoxStyle.Critical, "SQL Error")
            End If
        End Try
    End Sub
    'Events on form load
    Private Sub TimesheetLoadForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Create main connection to database
        PrivilegedSQLConnection = GetSqlConnection()
        'IFN Correct SAP Load
        'EnableRole(PrivilegedSQLConnection)
        LoadEmployeeMappingTable()
        LoadSAPEmployeeMappingTable()
        LoadSAPNetworkMappingTable()
    End Sub
    Private Sub LoadEmployeeMappingTable()
        ' caro2009 2011-01-17 -- added isnull below, the companyemployeeno is not forced as not null in the database
        Dim cmd As New SqlClient.SqlCommand("SELECT EmployeeID, rtrim(ISNULL(CompanyEmployeeNo,'99999')) as CompanyEmployeeNo FROM [Employee] WHERE CompanyID = 'HAT'", PrivilegedSQLConnection)
        Dim reader As SqlClient.SqlDataReader = cmd.ExecuteReader
        Dim row As SAPTimesheetLoad.EmployeeLookupRow
        While reader.Read
            row = TimesheetLoadDS.EmployeeLookup.NewEmployeeLookupRow()
            row.EmployeeID = reader.GetInt32(0)
            row.CompanyEmployeeNo = reader.GetString(1)
            TimesheetLoadDS.EmployeeLookup.Rows.Add(row)
        End While
        reader.Close()
        TimesheetLoadDS.EmployeeLookup.AcceptChanges()
    End Sub
    Private Sub LoadSAPEmployeeMappingTable()
        Dim cmd As New SqlClient.SqlCommand("SELECT OldEmployeeNumber, NewEmployeeNumber FROM [SAPEmployeeMapping]", PrivilegedSQLConnection)
        Dim reader As SqlClient.SqlDataReader = cmd.ExecuteReader
        Dim row As SAPTimesheetLoad.SAPEmployeeMappingRow
        While reader.Read
            row = TimesheetLoadDS.SAPEmployeeMapping.NewSAPEmployeeMappingRow
            row.OldEmployeeNumber = reader.GetInt32(0)
            row.NewEmployeeNumber = reader.GetInt32(1)
            TimesheetLoadDS.SAPEmployeeMapping.AddSAPEmployeeMappingRow(row)
        End While
        reader.Close()
        TimesheetLoadDS.SAPEmployeeMapping.AcceptChanges()
    End Sub

    'Load SAP Network Code Mapping table
    Private Sub LoadSAPNetworkMappingTable()
        Dim cmd As New SqlClient.SqlCommand("SELECT NetworkCode, Currency FROM [SAPNetworkMapping]", PrivilegedSQLConnection)
        Dim reader As SqlClient.SqlDataReader = cmd.ExecuteReader
        Dim row As SAPTimesheetLoad.SAPNetworkMappingRow

        While reader.Read
            row = TimesheetLoadDS.SAPNetworkMapping.NewSAPNetworkMappingRow
            row.NetworkCode = reader.GetString(0)
            row.Currency = reader.GetString(1)
            TimesheetLoadDS.SAPNetworkMapping.AddSAPNetworkMappingRow(row)

        End While
        reader.Close()
        TimesheetLoadDS.SAPNetworkMapping.AcceptChanges()
    End Sub


    'Load company timesheet data into database
    Private Sub LoadTimesheet()
        Dim OriginalFileName As String = ""
        Dim NewFileName As String
        'Filename loaded by user
        Me.Status.StatusString = "Loading file"
        Me.Status.Working = True
        RaiseEvent UpdateEvent()
        OriginalFileName = Me.LoadFile.Text

        Try
            'Proceed to load timesheet if specified file exists
            If File.Exists(OriginalFileName) Then
                'Show user that processing is being undertaken
                LoadData.Enabled = False
                'Rename user specified file so that it matches Company abbreviation in schema.ini file
                NewFileName = MoveFiles("HAT", OriginalFileName)
                'Pass timesheet data from renamed user file into another temporary dataset
                RetrieveDataFromFile(Path.GetFileName(NewFileName))
            Else
                MsgBox("File Does Not Exist!")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
            Status.Working = False
            Me.Cursor = Cursors.Arrow
        End Try
        Me.Status.StatusString = ""
        RaiseEvent UpdateEvent()
    End Sub

    Private Function GetFilePath() As String
        Dim FileDialog As New OpenFileDialog
        FileDialog.Multiselect = False
        FileDialog.CheckPathExists = True
        FileDialog.CheckFileExists = True
        FileDialog.Filter = "CSV(comma delimited) (*.csv)|*.csv"
        If FileDialog.ShowDialog = Windows.Forms.DialogResult.OK Then
            Return FileDialog.FileName
        End If
        Return ""
    End Function
    'Retrieve company timesheet data from .txt / .csv file
    Private Function RetrieveDataFromFile(ByVal LoadFile As String) As SAPTimesheetLoad
        Dim objCnn As New Odbc.OdbcConnection
        'Connection string to retrieve data from .txt / .csv file saved on local drive (C:\Test\)
        objCnn.ConnectionString = "Driver={Microsoft Text Driver (*.txt; *.csv)};Dbq=" & Application.StartupPath & "\TEMP;Extensions=asc,csv,tab,txt;"
        Dim objSQLCommand As Odbc.OdbcCommand
        Dim objSQLAdapter As Odbc.OdbcDataAdapter

        Try
            objCnn.Open()
            objSQLCommand = New Odbc.OdbcCommand
            objSQLCommand.CommandType = CommandType.Text
            'SELECT statement based on what company timesheet being loaded
            'objSQLCommand.CommandText = "SELECT " & DetermineSelectStatement(CStr(Me.CompanyLookup.SelectedValue)) & " FROM " & LoadFile
            objSQLCommand.CommandText = "SELECT * FROM " & LoadFile
            objSQLCommand.Connection = objCnn
            objSQLAdapter = New Odbc.OdbcDataAdapter(objSQLCommand)
            'Load company timesheet data into temporary dataset
            ' LGX we load data from CSV file into the "SAPTImesheets.xsd".LoadFormat table
            objSQLAdapter.Fill(TimesheetLoadDS, TimesheetLoadDS.LoadFormat.TableName)

            ' caro2009 2010-08-27 cleanup of empty rows that sometimes exists in the input file
            For Each row As SAPTimesheetLoad.LoadFormatRow In TimesheetLoadDS.LoadFormat
                If row.IsEMPLOYEE_NAMENull Then
                    row.Delete()
                End If
            Next
            TimesheetLoadDS.LoadFormat.AcceptChanges()


            Return TimesheetLoadDS

        Catch ex As Exception
            MsgBox(ex.Message)

        Finally
            If Not objCnn Is Nothing Then
                objCnn.Close()
                objCnn = Nothing
            End If
        End Try
        Return Nothing
    End Function
    Private Function UpdateSAPEmployeeNumber(ByVal InputTable As SAPTimesheetLoad.LoadFormatDataTable) As SAPTimesheetLoad.LoadFormatDataTable
        Dim row As SAPTimesheetLoad.LoadFormatRow
        Dim MappingRow As SAPTimesheetLoad.SAPEmployeeMappingRow
        Me.Status.StatusString = "Updating employee no."
        Me.Status.MaxProgressValue = InputTable.Rows.Count
        Me.Status.ProgressValue = 0
        RaiseEvent UpdateEvent()
        Dim Remainder As Integer
        Dim EmployeeNumber As String = "INITIAL VALUE"

        For Each row In InputTable
            Me.Status.ProgressValue = Me.Status.ProgressValue + 1
            Math.DivRem(Me.Status.ProgressValue, 10, Remainder)
            If Remainder = 0 Then
                RaiseEvent UpdateEvent()
            End If
            ' caro2009 2010-08-27 check for the mapping only once for each employee for performance
            If row.EMPLOYEE_NUMBER <> EmployeeNumber Then
                Debugger.Log(1, "EmployeeMapping", "processing " & row.EMPLOYEE_NAME)
                EmployeeNumber = row.EMPLOYEE_NUMBER
                MappingRow = CType(Me.TimesheetLoadDS.SAPEmployeeMapping.Rows.Find(row.EMPLOYEE_NUMBER), SAPTimesheetLoad.SAPEmployeeMappingRow)
                If Not IsNothing(MappingRow) Then
                    Debugger.Log(1, "EmployeeMapping", " mapped to " & MappingRow.NewEmployeeNumber)
                End If
                Debugger.Log(1, "EmployeeMapping", vbNewLine)
            End If

            If Not IsNothing(MappingRow) Then
                row.EMPLOYEE_NUMBER = MappingRow.NewEmployeeNumber.ToString
            End If
        Next
        InputTable.AcceptChanges()
        Me.Status.StatusString = ""
        RaiseEvent UpdateEvent()
        Return InputTable
    End Function

    Private Function UpdateSAPCurrency(ByVal InputTable As SAPTimesheetLoad.LoadFormatDataTable) As SAPTimesheetLoad.LoadFormatDataTable
        Dim row As SAPTimesheetLoad.LoadFormatRow
        Dim MappingRow As SAPTimesheetLoad.SAPNetworkMappingRow
        Me.Status.StatusString = "Summarising hours per week"
        Me.Status.MaxProgressValue = InputTable.Rows.Count
        Me.Status.ProgressValue = 0
        RaiseEvent UpdateEvent()
        For Each row In InputTable

            'temp = Me.TimesheetLoadDS.SAPNetworkMapping.Rows.Find(row.NETWORK_OP).ToString

            MappingRow = CType(Me.TimesheetLoadDS.SAPNetworkMapping.Rows.Find(Trim(row.NETWORK_OP)), SAPTimesheetLoad.SAPNetworkMappingRow)
            If Not IsNothing(MappingRow) Then
                row.SAPCURRENCYID = MappingRow.Currency.ToString
            Else
                MsgBox("SAP Network mapping not found for " & row.NETWORK_OP & " please add mapping to the table.", MsgBoxStyle.Critical, "Error")
                InputTable.Clear()
                InputTable.AcceptChanges()
                Return InputTable
            End If
        Next
        InputTable.AcceptChanges()
        Return InputTable
    End Function

    Private Function SummariseHoursPerWeek(ByVal InputTable As SAPTimesheetLoad.LoadFormatDataTable) As SAPTimesheetLoad.LoadFormatDataTable
        Dim row As SAPTimesheetLoad.LoadFormatRow
        Dim newrow As SAPTimesheetLoad.LoadFormatRow
        Dim TempTable As New SAPTimesheetLoad.LoadFormatDataTable
        Me.Status.StatusString = "Summarising hours per week"
        Me.Status.MaxProgressValue = InputTable.Rows.Count
        Me.Status.ProgressValue = 0
        RaiseEvent UpdateEvent()
        Dim Remainder As Integer
        ' list of distinct employee/week
        For Each row In InputTable.Select("", "EMPLOYEE_NUMBER, WEEK_ENDING_DATE")
            Me.Status.ProgressValue = Me.Status.ProgressValue + 1
            Math.DivRem(Me.Status.ProgressValue, 10, Remainder)
            If Remainder = 0 Then
                RaiseEvent UpdateEvent()
            End If
            If IsNothing(newrow) Then
                newrow = TempTable.NewLoadFormatRow
                newrow.EMPLOYEE_NAME = row.EMPLOYEE_NAME
                newrow.EMPLOYEE_NUMBER = row.EMPLOYEE_NUMBER
                newrow.WEEK_ENDING_DATE = row.WEEK_ENDING_DATE
                newrow.TOTAL_HOURS = row.TOTAL_HOURS
                newrow.NETWORK_OP = row.NETWORK_OP
                newrow.NETWORKNOTMATCH = False
            Else
                If newrow.EMPLOYEE_NUMBER <> row.EMPLOYEE_NUMBER Or _
                   newrow.WEEK_ENDING_DATE <> row.WEEK_ENDING_DATE Then
                    TempTable.AddLoadFormatRow(newrow)
                    newrow = TempTable.NewLoadFormatRow
                    newrow.WEEK_ENDING_DATE = row.WEEK_ENDING_DATE
                    newrow.EMPLOYEE_NUMBER = row.EMPLOYEE_NUMBER
                    newrow.EMPLOYEE_NAME = row.EMPLOYEE_NAME
                    newrow.TOTAL_HOURS = row.TOTAL_HOURS
                    newrow.NETWORK_OP = row.NETWORK_OP
                    newrow.NETWORKNOTMATCH = False
                Else ' if the same employee/week, add the hours

                    If newrow.NETWORK_OP <> row.NETWORK_OP Then
                        newrow.NETWORKNOTMATCH = True
                    End If

                    newrow.NETWORK_OP = row.NETWORK_OP
                    newrow.TOTAL_HOURS = Replace(CType(Decimal.Parse(newrow.TOTAL_HOURS, nfi) + Decimal.Parse(row.TOTAL_HOURS, nfi), Decimal).ToString("#####.00"), ",", ".")
                End If
            End If
        Next

        If Not IsNothing(newrow) Then
            TempTable.AddLoadFormatRow(newrow)
        End If
        Me.Status.StatusString = ""
        RaiseEvent UpdateEvent()
        Return TempTable
    End Function
    Private Function ReformatData(ByVal t As SAPTimesheetLoad.LoadFormatDataTable) As SAPTimesheetLoad.SAPTimesheetsDataTable
        Dim SourceRow As SAPTimesheetLoad.LoadFormatRow
        Dim DestinationRow As SAPTimesheetLoad.SAPTimesheetsRow
        Dim Day As Integer
        Dim Month As Integer
        Dim Year As Integer
        Dim Split() As String
        Me.TimesheetLoadDS.SAPTimesheets.Clear()
        Me.TimesheetLoadDS.SAPTimesheets.AcceptChanges()
        Me.Status.StatusString = "Formatting data"
        Me.Status.MaxProgressValue = t.Rows.Count
        Me.Status.ProgressValue = 0
        RaiseEvent UpdateEvent()
        Dim Remainder As Integer
        For Each SourceRow In t
            Me.Status.ProgressValue = Me.Status.ProgressValue + 1
            Math.DivRem(Me.Status.ProgressValue, 10, Remainder)
            If Remainder = 0 Then
                RaiseEvent UpdateEvent()
            End If
            If Not SourceRow.IsEMPLOYEE_NUMBERNull Then
                ' check if any critical information is null
                If SourceRow.IsEMPLOYEE_NAMENull Then
                    Throw New Exception("Incomplete data row (employee name) for employee " + SourceRow.EMPLOYEE_NUMBER)
                End If
                If SourceRow.IsTOTAL_HOURSNull Then
                    Throw New Exception("Incomplete data row (total hours) for employee " + SourceRow.EMPLOYEE_NUMBER)
                End If
                If SourceRow.IsWEEK_ENDING_DATENull Then
                    Throw New Exception("Incomplete data row (week ending date) for employee " + SourceRow.EMPLOYEE_NUMBER)
                End If

                DestinationRow = TimesheetLoadDS.SAPTimesheets.NewSAPTimesheetsRow()
                DestinationRow.EmployeeName = SourceRow.EMPLOYEE_NAME
                DestinationRow.EmployeeNumber = SourceRow.EMPLOYEE_NUMBER
                DestinationRow.NetworkOPNO = SourceRow.NETWORK_OP
                DestinationRow.NetworkCodeNotMatch = SourceRow.NETWORKNOTMATCH
                DestinationRow.SAPCurrencyID = SourceRow.SAPCURRENCYID
                If SourceRow.WEEK_ENDING_DATE.Contains("/") Then
                    Split = SourceRow.WEEK_ENDING_DATE.Split(CType("/", Char))
                ElseIf SourceRow.WEEK_ENDING_DATE.Contains(".") Then
                    Split = SourceRow.WEEK_ENDING_DATE.Split(CType(".", Char))
                Else
                    Throw New Exception("Invalid date separator for employee " + SourceRow.EMPLOYEE_NUMBER)
                End If
                ' assume day/month/year
                Day = Integer.Parse(Split(0))
                Month = Integer.Parse(Split(1))
                Year = Integer.Parse(Split(2))
                Try
                    DestinationRow.WeekEndDate = New Date(Year, Month, Day)
                Catch ex As Exception
                    Throw New Exception("Invalid date format: " + SourceRow.WEEK_ENDING_DATE + " for employee " + SourceRow.EMPLOYEE_NUMBER)
                End Try
                Try
                    DestinationRow.SAPHours = Decimal.Parse(SourceRow.TOTAL_HOURS, nfi)
                Catch ex As Exception
                    Throw New Exception("Invalid hours format: " + SourceRow.TOTAL_HOURS + " for employee " + SourceRow.EMPLOYEE_NUMBER)
                End Try
                TimesheetLoadDS.SAPTimesheets.AddSAPTimesheetsRow(DestinationRow)
            End If ' employee number is null, just skip the row
        Next
        Me.Status.StatusString = ""
        RaiseEvent UpdateEvent()
        Return TimesheetLoadDS.SAPTimesheets
    End Function
    'Execute timesheet to database save routine
    Private Function ValidateData(ByVal InputTable As SAPTimesheetLoad.SAPTimesheetsDataTable) As SAPTimesheetLoad.SAPTimesheetsDataTable
        Dim TSRow As SAPTimesheetLoad.SAPTimesheetsRow
        Dim EmpRow As SAPTimesheetLoad.EmployeeLookupRow
        Dim Location As String = ""
        Dim PayCurrency As String = ""

        'Dim EmpStruct As getEmployeeStruct
        Me.Status.StatusString = "Validating data"
        Me.Status.MaxProgressValue = InputTable.Rows.Count
        Me.Status.ProgressValue = 0
        RaiseEvent UpdateEvent()
        Dim Remainder As Integer
        For Each TSRow In InputTable
            Me.Status.ProgressValue = Me.Status.ProgressValue + 1
            Math.DivRem(Me.Status.ProgressValue, 10, Remainder)
            If Remainder = 0 Then
                RaiseEvent UpdateEvent()
            End If
            EmpRow = CType(TimesheetLoadDS.EmployeeLookup.Rows.Find(Integer.Parse(TSRow.EmployeeNumber)), SAPTimesheetLoad.EmployeeLookupRow)
            If IsNothing(EmpRow) Then
                TSRow.EmployeeExistsInPAF = False
            Else
                TSRow.EmployeeID = EmpRow.EmployeeID
                TSRow.EmployeeExistsInPAF = True

                ' check if we have an equivalent number of hours
                TSRow.PAFHours = GetPAFHours(TSRow.EmployeeID, TSRow.WeekEndDate)
                TSRow.PAFTimesheetHours = GetPAFTimesheets(TSRow.EmployeeNumber, TSRow.WeekEndDate)
                TSRow.NetworkOPNO = TSRow.NetworkOPNO
                TSRow.NetworkCodeNotMatch = TSRow.NetworkCodeNotMatch

                'EmpStruct = GetEmployeeRecord(TSRow.EmployeeID, TSRow.WeekEndDate)
                'GetEmployeeRecord(TSRow.EmployeeID, TSRow.WeekEndDate, Location, PayCurrency)
                EmployeeHistory.GetEmployeeInfo(TSRow.EmployeeID, TSRow.WeekEndDate, Location, PayCurrency)


                If PayCurrency = "BRC" Then
                    Debugger.Break()
                End If


                TSRow.RatePayLocation = Location 'GetEmployeeLocationRecord(TSRow.EmployeeID, TSRow.WeekEndDate)
                TSRow.RatePayCurrencyID = PayCurrency 'GetEmployeeCurrencyRecord(TSRow.EmployeeID, TSRow.WeekEndDate)
                'TSRow.RatePayCurrencyID = EmpStruct.ratePayCurrencyID

            End If
        Next
        Me.Status.StatusString = ""
        RaiseEvent UpdateEvent()
        Return InputTable
    End Function
    Dim PafHoursCmd As New SqlClient.SqlCommand("SELECT ISNULL(SUM(Hours),0) FROM [Hours] WHERE EmployeeID = @1 AND ProjectWeekEndingDate = @2")
    Dim PafTimesheetHoursCmd As New SqlClient.SqlCommand("SELECT ISNULL(SUM(Hours),0) FROM [Timesheet] WHERE CompanyID = 'HAT' AND CompanyEmployeeNO = @1 AND WeekEndDate = @2")
    Dim GetEmployeeLocationCmd As New SqlClient.SqlCommand("SELECT ISNULL(RatePayLocation, '') FROM getEmployeeRecord (@1, @2)")
    Dim GetEmployeeCurrencyCmd As New SqlClient.SqlCommand("SELECT ISNULL(RatePayCurrencyID,'') FROM getEmployeeRecord (@1, @2)")

    Private Structure getEmployeeStruct
        Public ratePayLocation As String
        Public ratePayCurrencyID As String
    End Structure


    Dim PAF_Data As SAPTimesheetLoad.PAF_DATADataTable
    'Dim GetEmployeeCurrencyCmd As New SqlClient.SqlCommand("SELECT ISNULL(SUM(Hours),0) FROM [Timesheet] WHERE CompanyID = 'HAT' AND CompanyEmployeeNO = @1 AND WeekEndDate = @2")

    Private Sub LoadPAF_Data()
        Dim sqlcmd As SqlClient.SqlCommand
        Dim sqlText As String
        sqlText = "SELECT [Hours].[EmployeeID]," & _
                    "[Employee].[CompanyEmployeeNo] As CompanyEmployeeNo, " & _
                    "[YearWeek].[EndDate] As WeekEndDate, " & _
                    "SUM([Hours].[Hours]) As Hours," & _
                    "[Hours].[Year]," & _
                    "[Hours].[Week] " & _
                "FROM [Hours] INNER JOIN  dbo.[YearWeek] ON [YearWeek].Year = [Hours].Year AND 	[YearWeek].Week = [Hours].Week " & _
                "INNER JOIN Employee ON Hours.EmployeeID = Employee.EmployeeID  " & _
                "WHERE [Hours].[CompanyID] = 'HAT' " & _
                "GROUP BY [Hours].[EmployeeID], [Employee].[CompanyEmployeeNo],[Hours].[Year], [Hours].[Week], [YearWeek].[EndDate] " & _
                "order by [Hours].[EmployeeID], [Hours].[Year], [Hours].[Week] "

        Me.Status.StatusString = "Loading Hours and Timesheets"
        RaiseEvent UpdateEvent()

        sqlcmd = New SqlClient.SqlCommand(sqlText, PrivilegedSQLConnection)
        Dim sqlreader As SqlClient.SqlDataReader
        Dim row As SAPTimesheetLoad.PAF_DATARow
        sqlreader = sqlcmd.ExecuteReader

        PAF_Data = New SAPTimesheetLoad.PAF_DATADataTable()
        PAF_Hours = New Collections.SortedList
        PAF_Timesheets = New Collections.SortedList
        While sqlreader.Read
            row = PAF_Data.NewPAF_DATARow
            If sqlreader.IsDBNull(0) Then
                row.EmployeeID = 0
            Else
                row.EmployeeID = sqlreader.GetInt32(0)
            End If
            row.CompanyEmployeeNo = sqlreader.GetString(1)
            row.WeekEndDate = sqlreader.GetDateTime(2)
            If sqlreader.IsDBNull(3) Then
                row.Hours = 0
            Else
                row.Hours = CType(sqlreader.GetDecimal(3), Decimal)
            End If

            'LGX 05/10/10: TimesheetHours does not exist anymore
            'If sqlreader.IsDBNull(4) Then
            'row.TimesheetHours = 0
            'Else
            'row.TimesheetHours = CType(sqlreader.GetDouble(4), Decimal)
            'End If
            PAF_Data.AddPAF_DATARow(row)
            If row.EmployeeID <> 0 Then
                PAF_Hours.Add(row.WeekEndDate.ToString("yyyy/MM/dd") + " " + row.EmployeeID.ToString, row.Hours)
            End If

            'LGX 05/10/10: TimesheetHours does not exist anymore
            'If row.TimesheetHours <> 0 Then
            'PAF_Timesheets.Add(row.WeekEndDate.ToString("yyyy/MM/dd") + " " + row.CompanyEmployeeNo, row.TimesheetHours)
            'End If

        End While
        sqlreader.Close()
        sqlcmd.Dispose()

        Me.Status.StatusString = "Loading Employee History"
        RaiseEvent UpdateEvent()

        EmployeeHistory = New EmployeeHistoryClass(GetSqlConnection())
        Me.Status.StatusString = ""
        RaiseEvent UpdateEvent()

    End Sub
    Private PAF_Hours As Collections.SortedList
    Private PAF_Timesheets As Collections.SortedList

    Private EmployeeHistory As EmployeeHistoryClass

    Private PAF_LocationCurrency As Collections.SortedList

    Private Function GetPAFHours(ByVal EmployeeID As Integer, ByVal WeekEndDate As DateTime) As Decimal
        'If IsNothing(PafHoursCmd.Connection) Then
        '    PafHoursCmd.Connection = GetSqlConnection()
        'End If
        If IsNothing(PAF_Data) Then
            LoadPAF_Data()
        End If
        'PafHoursCmd.Parameters.Clear()
        'PafHoursCmd.Parameters.AddWithValue("@1", EmployeeID)
        'PafHoursCmd.Parameters.AddWithValue("@2", WeekEndDate)
        'GetPAFHours = CType(PafHoursCmd.ExecuteScalar, Decimal)
        Dim result As SAPTimesheetLoad.PAF_DATARow
        Dim Filter As String
        'Filter = "EmployeeID = " + EmployeeID.ToString
        'Filter = Filter + " AND WeekEndDate = '" & WeekEndDate.Date.ToString("yyyy/MM/dd") & "'"
        'result = CType(PAF_Data.Select("")(0), SAPTimesheetLoad.PAF_DATARow)

        Dim Hours As Decimal
        Hours = CType(PAF_Hours.Item(WeekEndDate.ToString("yyyy/MM/dd") + " " + EmployeeID.ToString), Decimal)
        If IsNothing(Hours) Then
            Hours = 0
        End If
        Return Hours

        If IsNothing(result) Then
            Return 0
        Else
            Return result.Hours
        End If
    End Function
    Private Function GetPAFTimesheets(ByVal EmployeeNumber As String, ByVal WeekEndDate As DateTime) As Decimal
        'If IsNothing(PafTimesheetHoursCmd.Connection) Then
        '    PafTimesheetHoursCmd.Connection = GetSqlConnection()
        'End If
        If IsNothing(PAF_Data) Then
            LoadPAF_Data()
        End If
        'PafTimesheetHoursCmd.Parameters.Clear()
        'PafTimesheetHoursCmd.Parameters.AddWithValue("@1", EmployeeNumber)
        'PafTimesheetHoursCmd.Parameters.AddWithValue("@2", WeekEndDate)
        'GetPAFTimesheets = CType(PafTimesheetHoursCmd.ExecuteScalar, Decimal)
        Dim result As SAPTimesheetLoad.PAF_DATARow
        Dim Filter As String
        'Filter = "EmployeeID = '" + EmployeeNumber.ToString & "'"
        'Filter = Filter + " AND WeekEndDate = '" & WeekEndDate.Date.ToString("yyyy/MM/dd") & "'"
        'result = CType(PAF_Data.Select("")(0), SAPTimesheetLoad.PAF_DATARow)

        Dim Hours As Decimal
        Hours = CType(PAF_Timesheets.Item(WeekEndDate.ToString("yyyy/MM/dd") + " " + EmployeeNumber), Decimal)
        If IsNothing(Hours) Then
            Hours = 0
        End If
        Return Hours

        If IsNothing(result) Then
            Return 0
        Else
            Return result.TimesheetHours
        End If
    End Function

    'Private GetEmployeeRecordSQLCmd As SqlClient.SqlCommand
    'Private Sub GetEmployeeRecord(ByVal EmployeeID As Integer, ByVal WeekEndDate As DateTime, ByRef Location As String, ByVal PayCurrency As String)
    '    If IsNothing(GetEmployeeRecordSQLCmd) Then
    '        GetEmployeeRecordSQLCmd = New SqlClient.SqlCommand("SELECT ISNULL(RatePayLocation, ''), ISNULL(RatePayCurrencyID,'') FROM getEmployeeRecord (@1, @2)", PrivilegedSQLConnection)
    '    End If
    '    GetEmployeeRecordSQLCmd.Parameters.Clear()
    '    GetEmployeeRecordSQLCmd.Parameters.AddWithValue("@1", EmployeeID)
    '    GetEmployeeRecordSQLCmd.Parameters.AddWithValue("@2", WeekEndDate)
    '    Dim sqlreader As SqlClient.SqlDataReader
    '    sqlreader = GetEmployeeRecordSQLCmd.ExecuteReader
    '    sqlreader.Read()
    '    Location = sqlreader.GetString(0)
    '    PayCurrency = sqlreader.GetString(1)
    '    sqlreader.Close()
    'End Sub

    Private Sub SaveData(ByVal InputTable As SAPTimesheetLoad.SAPTimesheetsDataTable)
        'Dim CommitTransaction As SqlClient.SqlTransaction
        'If new data has been appended to main Timesheet dataset, save it to the database
        If Me.TimesheetLoadDS.SAPTimesheets.Rows.Count > 0 Then
            Me.Status.StatusString = "Saving data"
            Me.Status.MaxProgressValue = InputTable.Rows.Count
            Me.Status.ProgressValue = 0
            RaiseEvent UpdateEvent()

            Dim Remainder As Integer

            Try
                'Begin a Transaction procedure so that if any exception is thrown whilst saving timesheet data to 
                'database the complete save process will be rolled back
                'CommitTransaction = PrivilegedSQLConnection.BeginTransaction
                Me.SqlInsertCommand.Connection = PrivilegedSQLConnection
                'Me.SqlInsertCommand.Transaction = CommitTransaction
                Debugger.Log(0, "Info", "Before saving the data " + Now().ToLongTimeString + vbNewLine)

                ' to be replaced by a loop to provide update to the user
                'Me.TimesheetSqlDataAdapter.Update(Me.TimesheetLoadDS.SAPTimesheets)
                'Me.TimesheetLoadDS.AcceptChanges()

                Dim sqlCmd As SqlClient.SqlCommand

                Dim sqlText As String = _
            "INSERT INTO [SAPTimesheets] (EmployeeNumber, EmployeeName, EmployeeID, WeekEndDate, SAPHours, EmployeeExistsInPAF, PAFHours, PAFTimesheetHours, NetworkOPNO, NetworkCodeNotMatch, SAPCurrencyID, RatePayLocation, RatePayCurrencyID)" + _
            " VALUES (@EmployeeNumber, @EmployeeName, @EmployeeID, @WeekEndDate, @SAPHours, @EmployeeExistsInPAF, @PAFHours, @PAFTimesheetHours, @NetworkOPNO, @NetworkCodeNotMatch,@SAPCurrencyID,@RatePayLocation, @RatePayCurrencyID)"

                sqlCmd = New SqlClient.SqlCommand(sqlText, PrivilegedSQLConnection)
                sqlCmd.Parameters.Add(New System.Data.SqlClient.SqlParameter("@EmployeeNumber", System.Data.SqlDbType.VarChar, 6))


                sqlCmd.Parameters.Add(New System.Data.SqlClient.SqlParameter("@EmployeeName", System.Data.SqlDbType.VarChar, 100))
                sqlCmd.Parameters.Add(New System.Data.SqlClient.SqlParameter("@EmployeeID", System.Data.SqlDbType.Int, 8))
                sqlCmd.Parameters.Add(New System.Data.SqlClient.SqlParameter("@WeekEndDate", System.Data.SqlDbType.DateTime, 8))
                sqlCmd.Parameters.Add(New System.Data.SqlClient.SqlParameter("@SAPHours", System.Data.SqlDbType.Float, 8))
                sqlCmd.Parameters.Add(New System.Data.SqlClient.SqlParameter("@EmployeeExistsInPAF", System.Data.SqlDbType.Bit, 1))
                sqlCmd.Parameters.Add(New System.Data.SqlClient.SqlParameter("@PAFHours", System.Data.SqlDbType.Float, 8))
                sqlCmd.Parameters.Add(New System.Data.SqlClient.SqlParameter("@PAFTimesheetHours", System.Data.SqlDbType.Float, 8))
                sqlCmd.Parameters.Add(New System.Data.SqlClient.SqlParameter("@NetworkOPNO", System.Data.SqlDbType.Text, 8))
                sqlCmd.Parameters.Add(New System.Data.SqlClient.SqlParameter("@NetworkCodeNotMatch", System.Data.SqlDbType.Bit, 1))
                sqlCmd.Parameters.Add(New System.Data.SqlClient.SqlParameter("@SAPCurrencyID", System.Data.SqlDbType.VarChar, 3))
                sqlCmd.Parameters.Add(New System.Data.SqlClient.SqlParameter("@RatePayLocation", System.Data.SqlDbType.VarChar, 5))
                sqlCmd.Parameters.Add(New System.Data.SqlClient.SqlParameter("@RatePayCurrencyID", System.Data.SqlDbType.VarChar, 3))

                For Each row As SAPTimesheetLoad.SAPTimesheetsRow In Me.TimesheetLoadDS.SAPTimesheets

                    Me.Status.ProgressValue = Me.Status.ProgressValue + 1
                    Math.DivRem(Me.Status.ProgressValue, 10, Remainder)
                    If Remainder = 0 Then
                        RaiseEvent UpdateEvent()
                    End If

                    If row.IsEmployeeNumberNull Then
                        sqlCmd.Parameters.Item("@EmployeeNumber").Value = DBNull.Value
                    Else
                        sqlCmd.Parameters.Item("@EmployeeNumber").Value = row.EmployeeNumber
                    End If

                    If row.IsEmployeeNameNull Then
                        sqlCmd.Parameters.Item("@EmployeeName").Value = DBNull.Value
                    Else
                        sqlCmd.Parameters.Item("@EmployeeName").Value = row.EmployeeName
                    End If

                    If row.IsEmployeeIDNull Then
                        sqlCmd.Parameters.Item("@EmployeeID").Value = DBNull.Value
                    Else
                        sqlCmd.Parameters.Item("@EmployeeID").Value = row.EmployeeID
                    End If

                    If row.IsWeekEndDateNull Then
                        sqlCmd.Parameters.Item("@WeekEndDate").Value = DBNull.Value
                    Else
                        sqlCmd.Parameters.Item("@WeekEndDate").Value = row.WeekEndDate
                    End If

                    If row.IsSAPHoursNull Then
                        sqlCmd.Parameters.Item("@SAPHours").Value = DBNull.Value
                    Else
                        sqlCmd.Parameters.Item("@SAPHours").Value = row.SAPHours
                    End If

                    If row.IsEmployeeExistsInPAFNull Then
                        sqlCmd.Parameters.Item("@EmployeeExistsInPAF").Value = DBNull.Value
                    Else
                        sqlCmd.Parameters.Item("@EmployeeExistsInPAF").Value = row.EmployeeExistsInPAF
                    End If

                    If row.IsPAFHoursNull Then
                        sqlCmd.Parameters.Item("@PAFHours").Value = DBNull.Value
                    Else
                        sqlCmd.Parameters.Item("@PAFHours").Value = row.PAFHours
                    End If

                    If row.IsPAFTimesheetHoursNull Then
                        sqlCmd.Parameters.Item("@PAFTimesheetHours").Value = DBNull.Value
                    Else
                        sqlCmd.Parameters.Item("@PAFTimesheetHours").Value = row.PAFTimesheetHours
                    End If

                    If row.IsNetworkOPNONull Then
                        sqlCmd.Parameters.Item("@NetworkOPNO").Value = DBNull.Value
                    Else
                        sqlCmd.Parameters.Item("@NetworkOPNO").Value = row.NetworkOPNO
                    End If

                    If row.IsNetworkCodeNotMatchNull Then
                        sqlCmd.Parameters.Item("@NetworkCodeNotMatch").Value = DBNull.Value
                    Else
                        sqlCmd.Parameters.Item("@NetworkCodeNotMatch").Value = row.NetworkCodeNotMatch
                    End If

                    If row.IsSAPCurrencyIDNull Then
                        sqlCmd.Parameters.Item("@SAPCurrencyID").Value = DBNull.Value
                    Else
                        sqlCmd.Parameters.Item("@SAPCurrencyID").Value = row.SAPCurrencyID
                    End If

                    If row.IsRatePayLocationNull Then
                        sqlCmd.Parameters.Item("@RatePayLocation").Value = DBNull.Value
                    Else
                        sqlCmd.Parameters.Item("@RatePayLocation").Value = row.RatePayLocation
                    End If

                    If row.IsRatePayCurrencyIDNull Then
                        sqlCmd.Parameters.Item("@RatePayCurrencyID").Value = DBNull.Value
                    Else
                        sqlCmd.Parameters.Item("@RatePayCurrencyID").Value = row.RatePayCurrencyID
                    End If

                    sqlCmd.ExecuteScalar()
                Next
                Me.TimesheetLoadDS.AcceptChanges()

                Me.Status.StatusString = ""
                RaiseEvent UpdateEvent()

                ' --------------------------


                ' --------------------------
                'CommitTransaction.Commit()
                Debugger.Log(0, "Info", "Done saving the data " + Now().ToLongTimeString + vbNewLine)

                Me.Status.StatusString = ""
                Me.Status.ProgressValue = 0
                RaiseEvent UpdateEvent()
                MsgBox("Done!", MsgBoxStyle.OkOnly, "SAP Timesheet load")
            Catch ex As SqlClient.SqlException
                'If timesheets save process fails, roll back transaction
                MsgBox(ex.Message, MsgBoxStyle.Critical, "SQL Error")
                'If Not IsNothing(CommitTransaction) Then
                '    CommitTransaction.Rollback()
                'End If
            End Try
            Me.Status.StatusString = ""
            RaiseEvent UpdateEvent()

        End If

    End Sub

    Private Sub ClearTable()
        Dim cmd As New SqlClient.SqlCommand("delete from SAPTimesheets")
        cmd.Connection = GetSqlConnection()
        cmd.CommandType = CommandType.Text
        cmd.ExecuteNonQuery()
    End Sub

    Private Sub Process()
        Debugger.Log(0, "Info", "Starting processing " + Now().ToLongTimeString)        
        nfi.NumberDecimalSeparator = "."        
        LoadTimesheet()
        Debugger.Log(0, "Info", "File loaded " + Now().ToLongTimeString)
        LoadPAF_Data()
        'Try
        SaveData(ValidateData(ReformatData(UpdateSAPCurrency(SummariseHoursPerWeek(UpdateSAPEmployeeNumber(Me.TimesheetLoadDS.LoadFormat))))))
        'Catch ex As FormatException
        'MsgBox("Incorrect file format !" & vbNewLine & ex.Message & " (Be careful columns order)")
        'End Try
        Debugger.Log(0, "Info", "Done with processing " + Now().ToLongTimeString)
        Me.Status.Working = False
        RaiseEvent UpdateEvent()
    End Sub
    'Structure StatusStruct
    '    Dim StatusString As String
    '    Dim Working As Boolean
    '    Dim MaxProgressValue As Integer
    '    Dim ProgressValue As Integer
    'End Structure
    Private Status As StatusStruct
    Friend Event UpdateEvent()
    Private Sub UpdateEventHandler() Handles Me.UpdateEvent
        If Me.InvokeRequired Then
            Me.Invoke(New UpdateUIDelegate(AddressOf UpdateProgress))
        Else
            Me.UpdateProgress()
        End If
    End Sub
    Private Delegate Sub UpdateUIDelegate()

    Private Sub UpdateProgress()
        Me.ProgressBar.Minimum = 0
        Me.ProgressBar.Maximum = Me.Status.MaxProgressValue
        Me.ProgressBar.Value = Status.ProgressValue
        Me.ToolStripStatusLabel1.Text = Status.StatusString
        Me.LoadData.Enabled = Not Status.Working
        If Me.Status.Working Then
            Me.Cursor = Cursors.WaitCursor
        Else
            Me.Cursor = Cursors.Arrow
        End If
        Me.Invalidate()
    End Sub
    'Load timesheet button
    Dim T1 As Threading.Thread

    Private Sub LoadData_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LoadData.Click
        LoadData.Enabled = False
        Me.Cursor = Cursors.WaitCursor

        If Me.ClearBeforeLoadCheckBox.Checked Then
            ClearTable()
        End If
        'Execute load routine
        T1 = New Threading.Thread(AddressOf Process)
        T1.Start()
        'ReformatData(SummariseHoursPerWeek(UpdateSAPEmployeeNumber(Me.TimesheetLoadDS.LoadFormat)))
        'ValidateData(ReformatData(SummariseHoursPerWeek(UpdateSAPEmployeeNumber(Me.TimesheetLoadDS.LoadFormat))))
        'SaveData(ValidateData(ReformatData(SummariseHoursPerWeek(UpdateSAPEmployeeNumber(Me.TimesheetLoadDS.LoadFormat)))))
    End Sub

    'Dispose timesheetload form
    Private Sub TimesheetLoadForm_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Closed
        If Not IsNothing(T1) Then
            If T1.ThreadState <> Threading.ThreadState.Aborted And T1.ThreadState <> Threading.ThreadState.Stopped Then
                T1.Abort()
            End If
        End If
        MySAPTimesheetLoadForm = Nothing
    End Sub

    Private Sub FileDialogButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FileDialogButton.Click
        Me.LoadFile.Text = GetFilePath()
    End Sub

    Private Function MoveFiles(ByVal CompanyID As String, ByVal SourceFileName As String) As String
        Dim SchemaFileDirectory As String
        Dim SchemaFileName As String
        Dim DestinationDirectory As String
        'SchemaFileDirectory = Config.SchemaFolder
        SchemaFileDirectory = Application.StartupPath
        SchemaFileName = "schema." & CompanyID & ".ini"
        If Not File.Exists(SchemaFileDirectory & "\" & SchemaFileName) Then
            Throw (New Exception("Schema file does not exists for the " & CompanyID & " company in folder " & SchemaFileDirectory))
        End If
        If Not File.Exists(SourceFileName) Then
            Throw (New Exception("Source file " & SourceFileName & " does not exists"))
        End If
        DestinationDirectory = Application.StartupPath & "\TEMP"
        MoveFiles = DestinationDirectory & "\" & CompanyID & ".csv"
        'DestinationDirectory = System.Environment.GetEnvironmentVariable("TEMP")
        File.Copy(SchemaFileDirectory & "\" & SchemaFileName, DestinationDirectory & "\schema.ini", True)
        File.Copy(SourceFileName, MoveFiles, True)
    End Function

    Private Class EmployeeHistoryClass
        Private EmployeeList As Collections.SortedList
        Private Status As StatusStruct
        Private Structure EmployeeInfoStruct
            Public EmployeeID As Integer
            Public EffectiveDate As DateTime
            Public HistoryDate As DateTime
            Public Location As String
            Public PayCurrency As String
        End Structure
        Public Sub New(ByVal SqlConnection As SqlClient.SqlConnection)
            EmployeeList = New Collections.SortedList
            Fill(SqlConnection, EmployeeList)
        End Sub
        Private Sub Fill(ByVal SQLConnection As SqlClient.SqlConnection, ByVal EmployeeList As Collections.SortedList)
            Dim sqlText As String
            sqlText = "SELECT EmployeeID, EffectiveDate, RateAssignLocation, RatePayCurrencyID, HistoryDate " + _
                        "FROM (" + _
                        "SELECT EmployeeID, ISNULL(EffectiveDate,'2005-10-01') as EffectiveDate, RateAssignLocation, RatePayCurrencyID, Convert(DateTime,'01-01-2020', 101) as HistoryDate  FROM Employee " + _
                        "WHERE CompanyID = 'HAT' AND RatePayCurrencyID IS NOT NULL AND RateAssignLocation IS NOT NULL " + _
                        "UNION " + _
                        "SELECT EmployeeID, ISNULL(EffectiveDate,'2005-10-01') as EffectiveDate, RateAssignLocation, RatePayCurrencyID, HistoryDate FROM EmployeeHistory " + _
                        "WHERE CompanyID = 'HAT' AND RatePayCurrencyID IS NOT NULL AND RateAssignLocation IS NOT NULL " + _
                        ") AS T " + _
                        "ORDER BY EmployeeID, HistoryDate"
            Dim sqlCmd As SqlClient.SqlCommand
            sqlCmd = New SqlClient.SqlCommand(sqlText, SQLConnection)

            Dim sqlreader As SqlClient.SqlDataReader
            sqlreader = sqlCmd.ExecuteReader
            Dim EmployeeInfo As EmployeeInfoStruct

            While sqlreader.Read()
                EmployeeInfo = New EmployeeInfoStruct
                EmployeeInfo.EmployeeID = sqlreader.GetInt32(0)
                EmployeeInfo.EffectiveDate = sqlreader.GetDateTime(1)
                EmployeeInfo.Location = sqlreader.GetString(2)
                EmployeeInfo.PayCurrency = sqlreader.GetString(3)


                If EmployeeInfo.PayCurrency = "BRC" Then
                    Debugger.Break()
                End If


                EmployeeInfo.HistoryDate = sqlreader.GetDateTime(4)
                Me.Add(EmployeeInfo)
            End While
            sqlreader.Close()
            sqlCmd.Dispose()
        End Sub
        Private Sub Add(ByVal EmployeeInfo As EmployeeInfoStruct)
            Dim EmployeeHistory As Collections.SortedList

            EmployeeHistory = CType(EmployeeList.Item(EmployeeInfo.EmployeeID), Collections.SortedList)

            If IsNothing(EmployeeHistory) Then
                ' need to create a new employee history
                EmployeeHistory = New Collections.SortedList
                EmployeeHistory.Add(EmployeeInfo.EffectiveDate, EmployeeInfo)
                EmployeeList.Add(EmployeeInfo.EmployeeID, EmployeeHistory)
            Else
                ' just need to add the new history record and resave the list
                If EmployeeHistory.ContainsKey(EmployeeInfo.EffectiveDate) Then
                    ' replace with the later version with same effective date
                    EmployeeHistory.Item(EmployeeInfo.EffectiveDate) = EmployeeInfo
                Else
                    ' just add
                    EmployeeHistory.Add(EmployeeInfo.EffectiveDate, EmployeeInfo)
                End If
                EmployeeList.Item(EmployeeInfo.EmployeeID) = EmployeeHistory
            End If
        End Sub
        Public Sub GetEmployeeInfo(ByVal EmployeeID As Integer, ByVal WeekEndDate As DateTime, ByRef Location As String, ByRef PayCurrency As String)
            Dim EmployeeHistory As Collections.SortedList
            EmployeeHistory = CType(EmployeeList.Item(EmployeeID), Collections.SortedList)
            If IsNothing(EmployeeHistory) Then
                Throw (New Exception("Error in EmployeeHistory: EmployeeID not found!"))
            End If

            Dim EffectiveEmployeeInfo As EmployeeInfoStruct

            For Each EmployeeInfo As EmployeeInfoStruct In EmployeeHistory.Values
                If EmployeeInfo.EffectiveDate < WeekEndDate Then
                    If EffectiveEmployeeInfo.EmployeeID = 0 Then ' i.e. is a new empty struct
                        EffectiveEmployeeInfo = EmployeeInfo
                    Else
                        If EmployeeInfo.EffectiveDate >= EffectiveEmployeeInfo.EffectiveDate Then
                            EffectiveEmployeeInfo = EmployeeInfo
                        End If
                    End If
                    'IFN Correct SAP Load
                Else
                    If EffectiveEmployeeInfo.EmployeeID = 0 Then ' i.e. is a new empty struct
                        EffectiveEmployeeInfo = EmployeeInfo
                    End If
                    End If
            Next
            If EffectiveEmployeeInfo.EmployeeID = 0 Then
                Throw (New Exception("Error in EmployeeHistory: no valid history found for employee " + EmployeeID.ToString + " for date " + WeekEndDate.ToString("yyyy/MM/dd")))
            End If
            Location = EffectiveEmployeeInfo.Location
            PayCurrency = EffectiveEmployeeInfo.PayCurrency
        End Sub
    End Class
End Class
