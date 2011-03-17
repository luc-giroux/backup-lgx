Imports System.IO

Public Class TimesheetLoadForm
    Inherits System.Windows.Forms.Form

    Private Sub InitializeDataset()
        Me.SqlSelectCommand = New System.Data.SqlClient.SqlCommand
        Me.SqlInsertCommand = New System.Data.SqlClient.SqlCommand
        Me.TimesheetSqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter
        Me.TimesheetLoadDataSet = New PAF.TimesheetLoad
        CType(Me.TimesheetLoadDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'SqlSelectCommand
        '
        Me.SqlSelectCommand.CommandText = "SELECT EntryID, CompanyID, CompanyEmployeeNo, EmployeeName, Hours, PostedDate, WeekEndDate, Hold, AdjustingEntryID, LoadedBy, LoadMode FROM dbo.TimeSheet"
        Me.SqlSelectCommand.Connection = SqlConnection
        '
        'SqlInsertCommand
        '
        Me.SqlInsertCommand.CommandType = CommandType.StoredProcedure
        Me.SqlInsertCommand.CommandText = _
            "spTimesheetInsert"
        Me.SqlInsertCommand.Parameters.Add( _
            New System.Data.SqlClient.SqlParameter("@CompanyID", _
                                                    System.Data.SqlDbType.VarChar, 3, "CompanyID"))
        Me.SqlInsertCommand.Parameters.Add( _
            New System.Data.SqlClient.SqlParameter("@CompanyEmployeeNo", _
                                                    System.Data.SqlDbType.VarChar, 10, "CompanyEmployeeNo"))
        Me.SqlInsertCommand.Parameters.Add( _
            New System.Data.SqlClient.SqlParameter("@EmployeeName", _
                                                    System.Data.SqlDbType.VarChar, 100, "EmployeeName"))
        Me.SqlInsertCommand.Parameters.Add( _
            New System.Data.SqlClient.SqlParameter("@Hours", _
                                                    System.Data.SqlDbType.Float, 8, "Hours"))
        Me.SqlInsertCommand.Parameters.Add( _
            New System.Data.SqlClient.SqlParameter("@PostedDate", _
                                                    System.Data.SqlDbType.DateTime, 8, "PostedDate"))
        Me.SqlInsertCommand.Parameters.Add( _
            New System.Data.SqlClient.SqlParameter("@WeekEndDate", _
                                                    System.Data.SqlDbType.DateTime, 8, "WeekEndDate"))
        Me.SqlInsertCommand.Parameters.Add( _
            New System.Data.SqlClient.SqlParameter("@LoadedBy", _
                                                    System.Data.SqlDbType.VarChar, 50, "LoadedBy"))
        Me.SqlInsertCommand.Parameters.Add( _
            New System.Data.SqlClient.SqlParameter("@LoadMode", _
                                                    System.Data.SqlDbType.Char, 12, "LoadMode"))
        '
        'TimesheetSqlDataAdapter
        '
        Me.TimesheetSqlDataAdapter.InsertCommand = Me.SqlInsertCommand
        Me.TimesheetSqlDataAdapter.SelectCommand = Me.SqlSelectCommand

        'Database to Dataset Mappings
        Me.TimesheetSqlDataAdapter.TableMappings.AddRange( _
        New System.Data.Common.DataTableMapping() { _
        New System.Data.Common.DataTableMapping("Table", "Timesheet", New System.Data.Common.DataColumnMapping() { _
            New System.Data.Common.DataColumnMapping("EntryID", "EntryID"), _
            New System.Data.Common.DataColumnMapping("CompanyID", "CompanyID"), _
            New System.Data.Common.DataColumnMapping("CompanyEmployeeNo", "CompanyEmployeeNo"), _
            New System.Data.Common.DataColumnMapping("EmployeeName", "EmployeeName"), _
            New System.Data.Common.DataColumnMapping("WorkPackageID", "WorkPackageID"), _
            New System.Data.Common.DataColumnMapping("ProjectCode", "ProjectCode"), _
            New System.Data.Common.DataColumnMapping("Hours", "Hours"), _
            New System.Data.Common.DataColumnMapping("PostedDate", "PostedDate"), _
            New System.Data.Common.DataColumnMapping("WeekEndDate", "WeekEndDate"), _
            New System.Data.Common.DataColumnMapping("Hold", "Hold"), _
            New System.Data.Common.DataColumnMapping("AdjustingEntryID", "AdjustingEntryID"), _
            New System.Data.Common.DataColumnMapping("LoadedBy", "LoadedBy"), _
            New System.Data.Common.DataColumnMapping("LoadMode", "LoadMode")})})

        'TimesheetLoadDataSet
        '
        Me.TimesheetLoadDataSet.DataSetName = "Timesheet"
        Me.TimesheetLoadDataSet.Locale = New System.Globalization.CultureInfo("en-AU")

        CType(Me.TimesheetLoadDataSet, System.ComponentModel.ISupportInitialize).EndInit()
    End Sub

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        Me.InitializeDataset()

        'Map "Main Form.vb" dataset to dropdown list
        Me.CompanyLookup.DataSource = MyLookupDataset.Company
        Me.CompanyLookup.ValueMember = MyLookupDataset.Company.CompanyIDColumn.ColumnName
        Me.CompanyLookup.DisplayMember = MyLookupDataset.Company.CompanyNameColumn.ColumnName

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
    Friend WithEvents TimesheetLoadDataSet As PAF.TimesheetLoad
    Friend WithEvents SqlConnection As System.Data.SqlClient.SqlConnection
    Friend WithEvents TimesheetSqlDataAdapter As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents LoadFile As System.Windows.Forms.TextBox
    Friend WithEvents CompanyLookup As System.Windows.Forms.ComboBox
    Friend WithEvents LoadData As System.Windows.Forms.Button
    Friend WithEvents FileDialogButton As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.LoadData = New System.Windows.Forms.Button
        Me.TimesheetLoadDataSet = New PAF.TimesheetLoad
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.LoadFile = New System.Windows.Forms.TextBox
        Me.CompanyLookup = New System.Windows.Forms.ComboBox
        Me.FileDialogButton = New System.Windows.Forms.Button
        CType(Me.TimesheetLoadDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
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
        'TimesheetLoadDataSet
        '
        Me.TimesheetLoadDataSet.DataSetName = "TimesheetLoad"
        Me.TimesheetLoadDataSet.Locale = New System.Globalization.CultureInfo("en-US")
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
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(24, 57)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(64, 16)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Company"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'LoadFile
        '
        Me.LoadFile.Location = New System.Drawing.Point(104, 16)
        Me.LoadFile.Name = "LoadFile"
        Me.LoadFile.Size = New System.Drawing.Size(152, 20)
        Me.LoadFile.TabIndex = 4
        Me.LoadFile.Text = ""
        '
        'CompanyLookup
        '
        Me.CompanyLookup.Location = New System.Drawing.Point(105, 55)
        Me.CompanyLookup.Name = "CompanyLookup"
        Me.CompanyLookup.Size = New System.Drawing.Size(152, 21)
        Me.CompanyLookup.TabIndex = 5
        '
        'FileDialogButton
        '
        Me.FileDialogButton.Location = New System.Drawing.Point(256, 16)
        Me.FileDialogButton.Name = "FileDialogButton"
        Me.FileDialogButton.Size = New System.Drawing.Size(24, 24)
        Me.FileDialogButton.TabIndex = 6
        Me.FileDialogButton.Text = "..."
        '
        'TimesheetLoadForm
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(360, 85)
        Me.Controls.Add(Me.FileDialogButton)
        Me.Controls.Add(Me.CompanyLookup)
        Me.Controls.Add(Me.LoadFile)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.LoadData)
        Me.Name = "TimesheetLoadForm"
        Me.Text = "TimesheetLoadForm"
        CType(Me.TimesheetLoadDataSet, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    'Specific database application role execution to save timesheet data to database
    Private Sub EnableRole()

        Try
            'Create separate connection, additional to main connection, for execution of application role
            'Fire application role

            If SqlConnection.State <> ConnectionState.Open Then
                SqlConnection.Open()
            End If
            Dim cmd As New SqlClient.SqlCommand("sp_setapprole PAF_TIMESHEET_LOAD_APPLICATION, 'paf'", SqlConnection)
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
        Dim i As Integer
        'Create main connection to database
        EnableRole()

        Try
            'Fill main Timesheet Dataset with existing data from database table "Timesheet"
            i = TimesheetSqlDataAdapter.Fill(Me.TimesheetLoadDataSet.TimeSheet)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Application Error")
            Me.Close()
        End Try
    End Sub

    'Timesheet format per company
    Private Function DetermineSelectStatement(ByVal Company As String) As String
        Dim SelectSql As String = ""

        'As each company has a different timesheet format, a different SELECT statement must be assigned to retrieve
        'the right information from the .txt/.csv file 
        Select Case Company
            Case "AAM"
                SelectSql = "EMPLOYEE_NUMBER, EMPLOYEE_NAME, NETWORK_OPERATION, TOTAL_HOURS, POSTING_DATE, WEEK_ENDING_DATE, PROJECT_DEFINITION"
            Case "BEN"
                SelectSql = "EMPLOYEE_NUMBER, EMPLOYEE_NAME, NETWORK_OPERATION, TOTAL_HOURS, POSTING_DATE, WEEK_ENDING_DATE, PROJECT_DEFINITION"
            Case "CON"
                SelectSql = "EMPLOYEE_NUMBER, EMPLOYEE_NAME, NETWORK_OPERATION, TOTAL_HOURS, POSTING_DATE, WEEK_ENDING_DATE, PROJECT_DEFINITION"
            Case "FAL"
                SelectSql = "EMPLOYEE_NUMBER, EMPLOYEE_NAME, NETWORK_OPERATION, TOTAL_HOURS, POSTING_DATE, WEEK_ENDING_DATE, PROJECT_DEFINITION"
            Case "HAT"
                SelectSql = "EMPLOYEE_NUMBER, EMPLOYEE_NAME, NETWORK_OPERATION, TOTAL_HOURS, POSTING_DATE, WEEK_ENDING_DATE, PROJECT_DEFINITION"
            Case "SCE"
                SelectSql = "COMPANY, EMPLOYEE_ID, EMPLOYEE_NAME, PHASE, CENTRE, AREA, DISCIPLINE, SECTION, Posted_Week, Worked_Week, HOURS"
            Case "TPB"
                SelectSql = "EMPLOYEE_NUMBER, EMPLOYEE_NAME, NETWORK_OPERATION, TOTAL_HOURS, POSTING_DATE, WEEK_ENDING_DATE, PROJECT_DEFINITION"
            Case "TPC"
                SelectSql = "Employee_Number, Employee_Name, Dates, Location, Area, Discipline, Section, TotalNT, TotalOT"
            Case "TPD"
                SelectSql = "EMPLOYEE_NUMBER, EMPLOYEE_NAME, NETWORK_OPERATION, TOTAL_HOURS, POSTING_DATE, WEEK_ENDING_DATE, PROJECT_DEFINITION"
            Case "TPF"
                SelectSql = "COMPANY, EMPLOYEE_ID, EMPLOYEE_NAME, PHASE, CENTRE, AREA, DISCIPLINE, SECTION, Posted_Week, Worked_Week, HOURS"
            Case "TPG"
                SelectSql = "Employee_Number, Employee_Name, TSStartWeek, CostCode, TaskCode, TotalNT, TotalOT"
            Case "TPO"
                SelectSql = "Employee_Number, Employee_Name, Dates, Location, Area, Discipline, Section, TotalNT, TotalOT"
            Case "TPS"
                SelectSql = "Employee_Number, Employee_Name, Dates, Location, Area, Discipline, Section, TotalNT, TotalOT"
            Case Else
                SelectSql = "EMPLOYEE_NUMBER, EMPLOYEE_NAME, NETWORK_OPERATION, TOTAL_HOURS, POSTING_DATE, WEEK_ENDING_DATE, PROJECT_DEFINITION"
        End Select

        DetermineSelectStatement = SelectSql
    End Function

    'Load company timesheet data into database
    Private Sub LoadTimesheet(ByVal CompanyID As String)
        'Parameters
        Dim LoadDS As DataSet
        Dim newTimesheetRow As TimesheetLoad.TimeSheetRow
        Dim objDataRow As DataRow
        Dim dates As String = ""
        Dim dateData As String = ""
        Dim PostedDate As Date
        Dim WeekendData As String = ""
        Dim WeekOfYear As Integer
        Dim ProjectWeekendingDate As Date
        Dim CompanyWorkpackageID As String = ""
        Dim HatchCodeDS As New DataSet
        Dim companyCompare As String = ""
        Dim OriginalFileName As String = ""
        Dim NewFileName As String
        'Filename loaded by user
        OriginalFileName = Me.LoadFile.Text
        'MsgBox("2007/06/12: Timesheet load function disabled, waiting for timesheet file format", MsgBoxStyle.Information, "Information")
        'Return

        Try
            'Proceed to load timesheet if specified file exists
            If File.Exists(OriginalFileName) Then
                'Show user that processing is being undertaken
                LoadData.Enabled = False
                Me.Cursor = Cursors.WaitCursor
                'Rename user specified file so that it matches Company abbreviation in schema.ini file
                NewFileName = MoveFiles(CompanyID, OriginalFileName)
                'Pass timesheet data from renamed user file into another temporary dataset
                LoadDS = RetrieveDataFromFile(Path.GetFileName(NewFileName))

                'Manipulate company format of timesheet data into database required format and append to main Timesheet
                'dataset that was loaded on form load
                Select Case CompanyID
                    Case "SCE"
                        For Each objDataRow In LoadDS.Tables(CStr(Me.CompanyLookup.SelectedValue)).Rows
                            companyCompare = objDataRow.Item("COMPANY").ToString()
                            If companyCompare <> "" Then
                                If Not (companyCompare.Substring(0, 3) <> CStr(Me.CompanyLookup.SelectedValue)) Then
                                    newTimesheetRow = TimesheetLoadDataSet.TimeSheet.NewTimeSheetRow()

                                    newTimesheetRow.CompanyID = CStr(Me.CompanyLookup.SelectedValue)
                                    newTimesheetRow.CompanyEmployeeNo = (objDataRow.Item("EMPLOYEE_ID").ToString())
                                    newTimesheetRow.EmployeeName = (objDataRow.Item("EMPLOYEE_NAME").ToString())
                                    If Not IsDBNull(objDataRow.Item("Posted_Week")) Then
                                        newTimesheetRow.PostedDate = findWeekEnd_Posted_Dates(objDataRow.Item("Posted_Week").ToString())
                                    End If
                                    If Not IsDBNull(objDataRow.Item("Worked_Week")) Then
                                        newTimesheetRow.WeekEndDate = findWeekEnd_Posted_Dates(objDataRow.Item("Worked_Week").ToString())
                                    End If
                                    If Not IsDBNull(objDataRow.Item("AREA")) Then
                                        If objDataRow.Item("AREA").ToString.Substring(0, 1) = "9" Then
                                            CompanyWorkpackageID = "ALL-" & objDataRow.Item("Area").ToString & "-0-0"
                                        Else
                                            CompanyWorkpackageID = objDataRow.Item("CENTRE").ToString & "-" & CStr(CInt(objDataRow.Item("Area"))) & "-" & CStr(CInt(objDataRow.Item("Discipline"))) & "-" & CStr(CInt(objDataRow.Item("Section")))
                                        End If
                                    Else
                                        CompanyWorkpackageID = ""
                                    End If
                                    HatchCodeDS = findHatchCodeData(CompanyWorkpackageID, CStr(Me.CompanyLookup.SelectedValue))
                                    newTimesheetRow.Hours = CDbl(objDataRow.Item("HOURS"))
                                    newTimesheetRow.LoadedBy = System.Environment.UserName
                                    newTimesheetRow.LoadMode = "Automatic"
                                    Me.TimesheetLoadDataSet.TimeSheet.Rows.Add(newTimesheetRow)
                                End If
                            End If
                        Next
                    Case "TPF"
                        For Each objDataRow In LoadDS.Tables(CStr(Me.CompanyLookup.SelectedValue)).Rows
                            companyCompare = objDataRow.Item("COMPANY").ToString()
                            If companyCompare <> "" Then
                                If Not (companyCompare.Substring(0, 3) <> CStr(Me.CompanyLookup.SelectedValue)) Then
                                    newTimesheetRow = TimesheetLoadDataSet.TimeSheet.NewTimeSheetRow()

                                    newTimesheetRow.CompanyID = CStr(Me.CompanyLookup.SelectedValue)
                                    newTimesheetRow.CompanyEmployeeNo = (objDataRow.Item("EMPLOYEE_ID").ToString())
                                    newTimesheetRow.EmployeeName = (objDataRow.Item("EMPLOYEE_NAME").ToString())
                                    If Not IsDBNull(objDataRow.Item("Posted_Week")) Then
                                        newTimesheetRow.PostedDate = findWeekEnd_Posted_Dates(objDataRow.Item("Posted_Week").ToString())
                                    End If
                                    If Not IsDBNull(objDataRow.Item("Worked_Week")) Then
                                        newTimesheetRow.WeekEndDate = findWeekEnd_Posted_Dates(objDataRow.Item("Worked_Week").ToString())
                                    End If
                                    If Not IsDBNull(objDataRow.Item("AREA")) Then
                                        If objDataRow.Item("AREA").ToString.Substring(0, 1) = "9" Then
                                            CompanyWorkpackageID = "ALL-" & objDataRow.Item("Area").ToString & "-0-0"
                                        Else
                                            CompanyWorkpackageID = objDataRow.Item("CENTRE").ToString & "-" & CStr(CInt(objDataRow.Item("Area"))) & "-" & CStr(CInt(objDataRow.Item("Discipline"))) & "-" & CStr(CInt(objDataRow.Item("Section")))
                                        End If
                                    Else
                                        CompanyWorkpackageID = ""
                                    End If
                                    HatchCodeDS = findHatchCodeData(CompanyWorkpackageID, CStr(Me.CompanyLookup.SelectedValue))
                                    newTimesheetRow.Hours = CDbl(objDataRow.Item("HOURS"))
                                    newTimesheetRow.LoadedBy = System.Environment.UserName
                                    newTimesheetRow.LoadMode = "Automatic"
                                    Me.TimesheetLoadDataSet.TimeSheet.Rows.Add(newTimesheetRow)
                                End If
                            End If
                        Next
                    Case "TPC"
                        For Each objDataRow In LoadDS.Tables(CStr(Me.CompanyLookup.SelectedValue)).Rows
                            If Not IsDBNull(objDataRow.Item("Employee_Number")) Then
                                newTimesheetRow = TimesheetLoadDataSet.TimeSheet.NewTimeSheetRow()

                                newTimesheetRow.CompanyID = CStr(Me.CompanyLookup.SelectedValue)
                                newTimesheetRow.CompanyEmployeeNo = (objDataRow.Item("Employee_Number").ToString())
                                newTimesheetRow.EmployeeName = (objDataRow.Item("Employee_Name").ToString())
                                If Not IsDBNull(objDataRow.Item("Dates")) Then
                                    dates = objDataRow.Item("Dates").ToString
                                    dateData = dates.Substring(20, 12)
                                    PostedDate = CDate(dateData.Substring(2, 10))
                                    newTimesheetRow.PostedDate = PostedDate

                                    WeekendData = dates.Substring(0, 6)
                                    WeekOfYear = CInt(WeekendData.Substring(4, 2))
                                    ProjectWeekendingDate = findWeekendingDate(WeekendData, WeekOfYear)
                                    newTimesheetRow.WeekEndDate = ProjectWeekendingDate
                                End If
                                If Not IsDBNull(objDataRow.Item("AREA")) Then
                                    If objDataRow.Item("AREA").ToString.Substring(0, 1) = "9" Then
                                        CompanyWorkpackageID = "ALL-" & objDataRow.Item("Area").ToString & "-0-0"
                                    Else
                                        CompanyWorkpackageID = objDataRow.Item("Location").ToString & "-" & CStr(CInt(objDataRow.Item("Area"))) & "-" & CStr(CInt(objDataRow.Item("Discipline"))) & "-" & CStr(CInt(objDataRow.Item("Section")))
                                    End If
                                Else
                                    CompanyWorkpackageID = ""
                                End If
                                HatchCodeDS = findHatchCodeData(CompanyWorkpackageID, CStr(Me.CompanyLookup.SelectedValue))
                                newTimesheetRow.Hours = CDbl(objDataRow.Item("TotalNT")) + CDbl(objDataRow.Item("TotalOT"))
                                newTimesheetRow.LoadedBy = System.Environment.UserName
                                newTimesheetRow.LoadMode = "Automatic"
                                Me.TimesheetLoadDataSet.TimeSheet.Rows.Add(newTimesheetRow)
                            Else
                                Exit For
                            End If
                        Next
                    Case "TPG"
                        Dim CostCode As String
                        Dim TaskCode As String
                        Dim NormalTime As Double
                        Dim OverTime As Double

                        For Each objDataRow In LoadDS.Tables(CStr(Me.CompanyLookup.SelectedValue)).Rows
                            If Not IsDBNull(objDataRow.Item("EmployeeNo (A10)")) Then
                                newTimesheetRow = TimesheetLoadDataSet.TimeSheet.NewTimeSheetRow()

                                newTimesheetRow.CompanyID = CStr(Me.CompanyLookup.SelectedValue)
                                newTimesheetRow.CompanyEmployeeNo = (objDataRow.Item("EmployeeNo (A10)").ToString())
                                newTimesheetRow.EmployeeName = (objDataRow.Item("EmployeeName (A50)").ToString())
                                If Not IsDBNull(objDataRow.Item("TimesheetStartDate (A10)")) Then
                                    'dates = objDataRow.Item("TimesheetStartDate (A10)").ToString
                                    'dateData = dates.Substring(20, 12)
                                    'PostedDate = CDate(dateData.Substring(2, 10))
                                    newTimesheetRow.PostedDate = CType(objDataRow.Item("AdjustmentDate (A10)"), DateTime)
                                    'WeekendData = dates.Substring(0, 6)
                                    'WeekOfYear = CInt(WeekendData.Substring(4, 2))
                                    'ProjectWeekendingDate = findWeekendingDate(WeekendData, WeekOfYear)

                                    'edited
                                    'newTimesheetRow.WeekEndDate = CType(objDataRow.Item("TimesheetStartDate (A10)"), DateTime)

                                    'new
                                    newTimesheetRow.WeekEndDate = CType(objDataRow.Item("TimesheetStartDate (A10)"), DateTime).AddDays(-1)
                                End If
                                'original
                                'If Not IsDBNull(objDataRow.Item("Costode (A10)")) Then
                                '    CostCode = objDataRow.Item("Costode (A10)").ToString
                                '    TaskCode = objDataRow.Item("TaskCode (A10)").ToString
                                '    If CostCode.Substring(0, 1) = "9" Then
                                '        CompanyWorkpackageID = "511x-" & CostCode & "-0-0"
                                '    Else
                                '        CompanyWorkpackageID = CostCode & "-" & TaskCode
                                '    End If
                                'Else
                                '    CompanyWorkpackageID = ""
                                'End If
                                'CompanyWorkpackageID = CompanyWorkpackageID.Replace(" ", "-")

                                '-------------------------------------------------------------
                                If Not IsDBNull(objDataRow.Item("Costode (A10)")) Then
                                    CostCode = objDataRow.Item("Costode (A10)").ToString
                                    TaskCode = objDataRow.Item("TaskCode (A10)").ToString
                                    If TaskCode.Substring(0, 1) = "9" Then
                                        CompanyWorkpackageID = "511x-" & TaskCode & "-0-0"
                                    Else
                                        CompanyWorkpackageID = CostCode & "-" & TaskCode
                                        CompanyWorkpackageID = CompanyWorkpackageID.Replace(" ", "-")
                                        If CompanyWorkpackageID.Substring(13, 1) = "0" Then
                                            CompanyWorkpackageID = CompanyWorkpackageID.Remove(13, 1)
                                        End If
                                        If CompanyWorkpackageID.Substring(5, 1) = "0" Then
                                            CompanyWorkpackageID = CompanyWorkpackageID.Remove(5, 1)
                                        End If
                                    End If
                                Else
                                    CompanyWorkpackageID = ""
                                End If
                                'CompanyWorkpackageID = CompanyWorkpackageID.Replace(" ", "-")
                                '--------------------------------------------------------------

                                HatchCodeDS = findHatchCodeData(CompanyWorkpackageID, CStr(Me.CompanyLookup.SelectedValue))
                                If Not IsDBNull(objDataRow.Item("TotalNormalTime(A7)")) Then
                                    NormalTime = CDbl(objDataRow.Item("TotalNormalTime(A7)"))
                                Else
                                    NormalTime = 0
                                End If
                                If Not IsDBNull(objDataRow.Item("TotalOvertime(A7)")) Then
                                    OverTime = CDbl(objDataRow.Item("TotalOvertime(A7)"))
                                Else
                                    OverTime = 0
                                End If
                                newTimesheetRow.Hours = NormalTime + OverTime
                                newTimesheetRow.LoadedBy = System.Environment.UserName
                                newTimesheetRow.LoadMode = "Automatic"
                                Me.TimesheetLoadDataSet.TimeSheet.Rows.Add(newTimesheetRow)
                            Else
                                Exit For
                            End If
                        Next
                    Case "TPO"
                        For Each objDataRow In LoadDS.Tables(CStr(Me.CompanyLookup.SelectedValue)).Rows
                            If Not IsDBNull(objDataRow.Item("Employee_Number")) Then
                                newTimesheetRow = TimesheetLoadDataSet.TimeSheet.NewTimeSheetRow()

                                newTimesheetRow.CompanyID = CStr(Me.CompanyLookup.SelectedValue)
                                newTimesheetRow.CompanyEmployeeNo = (objDataRow.Item("Employee_Number").ToString())
                                newTimesheetRow.EmployeeName = (objDataRow.Item("Employee_Name").ToString())
                                If Not IsDBNull(objDataRow.Item("Dates")) Then
                                    dates = objDataRow.Item("Dates").ToString
                                    dateData = dates.Substring(20, 12)
                                    PostedDate = CDate(dateData.Substring(2, 10))
                                    newTimesheetRow.PostedDate = PostedDate

                                    WeekendData = dates.Substring(0, 6)
                                    WeekOfYear = CInt(WeekendData.Substring(4, 2))
                                    ProjectWeekendingDate = findWeekendingDate(WeekendData, WeekOfYear)
                                    newTimesheetRow.WeekEndDate = ProjectWeekendingDate
                                End If
                                If Not IsDBNull(objDataRow.Item("AREA")) Then
                                    If objDataRow.Item("AREA").ToString.Substring(0, 1) = "9" Then
                                        CompanyWorkpackageID = objDataRow.Item("Location").ToString & objDataRow.Item("Area").ToString & "-0-0"
                                    Else
                                        CompanyWorkpackageID = objDataRow.Item("Location").ToString & "-" & CStr(CInt(objDataRow.Item("Area"))) & "-" & CStr(CInt(objDataRow.Item("Discipline"))) & "-" & CStr(CInt(objDataRow.Item("Section")))
                                    End If
                                Else
                                    CompanyWorkpackageID = ""
                                End If
                                HatchCodeDS = findHatchCodeData(CompanyWorkpackageID, CStr(Me.CompanyLookup.SelectedValue))
                                newTimesheetRow.Hours = CDbl(objDataRow.Item("TotalNT")) + CDbl(objDataRow.Item("TotalOT"))
                                newTimesheetRow.LoadedBy = System.Environment.UserName
                                newTimesheetRow.LoadMode = "Automatic"
                                Me.TimesheetLoadDataSet.TimeSheet.Rows.Add(newTimesheetRow)
                            Else
                                Exit For
                            End If
                        Next
                    Case "TPS"
                        For Each objDataRow In LoadDS.Tables(CStr(Me.CompanyLookup.SelectedValue)).Rows
                            If Not IsDBNull(objDataRow.Item("Employee_Number")) Then
                                newTimesheetRow = TimesheetLoadDataSet.TimeSheet.NewTimeSheetRow()

                                newTimesheetRow.CompanyID = CStr(Me.CompanyLookup.SelectedValue)
                                newTimesheetRow.CompanyEmployeeNo = (objDataRow.Item("Employee_Number").ToString())
                                newTimesheetRow.EmployeeName = (objDataRow.Item("Employee_Name").ToString())
                                If Not IsDBNull(objDataRow.Item("Dates")) Then
                                    dates = objDataRow.Item("Dates").ToString
                                    dateData = dates.Substring(20, 12)
                                    PostedDate = CDate(dateData.Substring(2, 10))
                                    newTimesheetRow.PostedDate = PostedDate

                                    WeekendData = dates.Substring(0, 6)
                                    WeekOfYear = CInt(WeekendData.Substring(4, 2))
                                    ProjectWeekendingDate = findWeekendingDate(WeekendData, WeekOfYear)
                                    newTimesheetRow.WeekEndDate = ProjectWeekendingDate
                                End If
                                If Not IsDBNull(objDataRow.Item("AREA")) Then
                                    If objDataRow.Item("AREA").ToString.Substring(0, 1) = "9" Then
                                        CompanyWorkpackageID = "ALL-" & objDataRow.Item("Area").ToString & "-0-0"
                                    Else
                                        CompanyWorkpackageID = objDataRow.Item("Location").ToString & "-" & CStr(CInt(objDataRow.Item("Area"))) & "-" & CStr(CInt(objDataRow.Item("Discipline"))) & "-" & CStr(CInt(objDataRow.Item("Section")))
                                    End If
                                Else
                                    CompanyWorkpackageID = ""
                                End If
                                HatchCodeDS = findHatchCodeData(CompanyWorkpackageID, CStr(Me.CompanyLookup.SelectedValue))
                                newTimesheetRow.Hours = CDbl(objDataRow.Item("TotalNT")) + CDbl(objDataRow.Item("TotalOT"))
                                newTimesheetRow.LoadedBy = System.Environment.UserName
                                newTimesheetRow.LoadMode = "Automatic"
                                Me.TimesheetLoadDataSet.TimeSheet.Rows.Add(newTimesheetRow)
                            Else
                                Exit For
                            End If
                        Next
                    Case Else
                        'HATCH format
                        For Each objDataRow In LoadDS.Tables(CStr(Me.CompanyLookup.SelectedValue)).Rows
                            If Not IsDBNull(objDataRow.Item("EMPLOYEE_NUMBER")) Then
                                newTimesheetRow = TimesheetLoadDataSet.TimeSheet.NewTimeSheetRow()

                                newTimesheetRow.CompanyID = CStr(Me.CompanyLookup.SelectedValue)
                                newTimesheetRow.CompanyEmployeeNo = (objDataRow.Item("EMPLOYEE_NUMBER").ToString())
                                newTimesheetRow.EmployeeName = (objDataRow.Item("EMPLOYEE_NAME").ToString())
                                newTimesheetRow.Hours = CDbl(objDataRow.Item("TOTAL_HOURS"))
                                newTimesheetRow.PostedDate = CDate(objDataRow.Item("POSTING_DATE"))
                                newTimesheetRow.WeekEndDate = CDate(objDataRow.Item("WEEK_ENDING_DATE"))
                                newTimesheetRow.LoadedBy = System.Environment.UserName
                                newTimesheetRow.LoadMode = "Automatic"
                                Me.TimesheetLoadDataSet.TimeSheet.Rows.Add(newTimesheetRow)
                            Else
                                Exit For
                            End If
                        Next
                End Select

                'Pass Timesheet dataset to database
                SaveData()
            Else
                MsgBox("File Does Not Exist!")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
            LoadData.Enabled = True
            Me.Cursor = Cursors.Arrow
        End Try
    End Sub

    Private Function GetFilePath() As String
        Dim FileDialog As New OpenFileDialog
        FileDialog.Multiselect = False
        FileDialog.CheckPathExists = True
        FileDialog.CheckFileExists = True
        FileDialog.Filter = "CSV(comma delimited) (*.csv)|*.csv"
        If FileDialog.ShowDialog = DialogResult.OK Then
            Return FileDialog.FileName
        End If
        Return ""
    End Function
    'Retrieve company timesheet data from .txt / .csv file
    Private Function RetrieveDataFromFile(ByVal LoadFile As String) As DataSet
        Dim objCnn As New Odbc.OdbcConnection
        'Connection string to retrieve data from .txt / .csv file saved on local drive (C:\Test\)
        objCnn.ConnectionString = "Driver={Microsoft Text Driver (*.txt; *.csv)};Dbq=" & System.Environment.GetEnvironmentVariable("TEMP") & ";Extensions=asc,csv,tab,txt;"
        Dim objDataSet As DataSet
        Dim objSQLCommand As Odbc.OdbcCommand
        Dim objSQLAdapter As Odbc.OdbcDataAdapter

        Try
            objCnn.Open()
            objSQLCommand = New Odbc.OdbcCommand
            objSQLCommand.CommandType = CommandType.Text
            'SELECT statement based on what company timesheet being loaded
            objSQLCommand.CommandText = "SELECT " & DetermineSelectStatement(CStr(Me.CompanyLookup.SelectedValue)) & " FROM " & LoadFile
            objSQLCommand.CommandText = "SELECT * FROM " & LoadFile
            objSQLCommand.Connection = objCnn
            objSQLAdapter = New Odbc.OdbcDataAdapter(objSQLCommand)
            objDataSet = New DataSet(CStr(Me.CompanyLookup.SelectedValue))
            'Load company timesheet data into temporary dataset
            objSQLAdapter.Fill(objDataSet, CStr(Me.CompanyLookup.SelectedValue))

            Return objDataSet

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

    'Retrieve ProjectWeekEndingDate from table Calendar in database for use in timesheet load
    Private Function findWeekendingDate(ByVal dates As String, ByVal week As Integer) As Date
        Dim objCnn As SqlClient.SqlConnection
        Dim objDataSet As DataSet
        Dim objSQLCommand As SqlClient.SqlCommand
        Dim objSQLAdapter As SqlClient.SqlDataAdapter
        Dim objDataRow As DataRow

        Try
            objCnn = GetSqlConnection()
            objSQLCommand = New SqlClient.SqlCommand
            objSQLCommand.CommandType = CommandType.Text
            objSQLCommand.CommandText = "SELECT ProjectWeekEndingDate FROM dbo.Calendar WHERE  (WeekOfYear = " & week & ") AND (ProjectBillingYear = " & dates.Substring(0, 4) & ")"
            objSQLCommand.Connection = objCnn
            objSQLAdapter = New SqlClient.SqlDataAdapter(objSQLCommand)
            objDataSet = New DataSet("Calendar")
            objSQLAdapter.Fill(objDataSet, "Calendar")

            For Each objDataRow In objDataSet.Tables("Calendar").Rows
                Return CDate(objDataRow.Item("ProjectWeekEndingDate"))
            Next

        Catch ex As Exception
            MsgBox(ex.Message)

        Finally
            If Not objCnn Is Nothing Then
                objCnn.Close()
                objCnn = Nothing
            End If
        End Try
    End Function

    'Retrieve ProjectWeekEndingDate/Posted Date from table Calendar in database for use in timesheet load
    '(Different company input parameter)
    Private Function findWeekEnd_Posted_Dates(ByVal YearWeek As String) As Date
        Dim objCnn As SqlClient.SqlConnection
        Dim objDataSet As DataSet
        Dim objSQLCommand As SqlClient.SqlCommand
        Dim objSQLAdapter As SqlClient.SqlDataAdapter
        Dim objDataRow As DataRow

        Try
            objCnn = GetSqlConnection()
            objSQLCommand = New SqlClient.SqlCommand
            objSQLCommand.CommandType = CommandType.Text
            objSQLCommand.CommandText = "SELECT ProjectWeekEndingDate FROM dbo.Calendar WHERE  (WeekOfYear = " & Split(YearWeek, ".")(1) & ") AND (ProjectBillingYear = " & Split(YearWeek, ".")(0) & ")"
            objSQLCommand.Connection = objCnn
            objSQLAdapter = New SqlClient.SqlDataAdapter(objSQLCommand)
            objDataSet = New DataSet("Calendar")
            objSQLAdapter.Fill(objDataSet, "Calendar")

            For Each objDataRow In objDataSet.Tables("Calendar").Rows
                Return CDate(objDataRow.Item("ProjectWeekEndingDate"))
            Next

        Catch ex As Exception
            MsgBox(ex.Message)

        Finally
            If Not objCnn Is Nothing Then
                objCnn.Close()
                objCnn = Nothing
            End If
        End Try
    End Function

    'Retrieve Hatch SAP code and Hatch Project Code from table Workpackage in database for use in timesheet load
    Private Function findHatchCodeData(ByVal CompanyWPID As String, ByVal CompanyID As String) As DataSet
        Dim objCnn As SqlClient.SqlConnection
        Dim objDataSet As DataSet
        Dim objSQLCommand As SqlClient.SqlCommand
        Dim objSQLAdapter As SqlClient.SqlDataAdapter

        Try
            objCnn = GetSqlConnection()
            objSQLCommand = New SqlClient.SqlCommand
            objSQLCommand.CommandType = CommandType.Text
            objSQLCommand.CommandText = "SELECT TOP 1 HatchCode, HatchProjectCode FROM dbo.WorkPackage WHERE " & CompanyID & "WorkPackageID = '" & CompanyWPID & "'"
            objSQLCommand.Connection = objCnn
            objSQLAdapter = New SqlClient.SqlDataAdapter(objSQLCommand)
            objDataSet = New DataSet("WorkPackage")
            objSQLAdapter.Fill(objDataSet, "WorkPackage")

            Return objDataSet

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

    'Execute timesheet to database save routine
    Private Sub SaveData()
        Dim CommitTransaction As SqlClient.SqlTransaction
        'If new data has been appended to main Timesheet dataset, save it to the database
        If Me.TimesheetLoadDataSet.HasChanges Then
            Try

                'Begin a Transaction procedure so that if any exception is thrown whilst saving timesheet data to 
                'database the complete save process will be rolled back
                CommitTransaction = SqlConnection.BeginTransaction
                Me.SqlInsertCommand.Connection = SqlConnection
                Me.SqlInsertCommand.Transaction = CommitTransaction
                Me.TimesheetSqlDataAdapter.Update(Me.TimesheetLoadDataSet.TimeSheet)
                Me.TimesheetLoadDataSet.AcceptChanges()
                CommitTransaction.Commit()
                MsgBox("Done!")
            Catch ex As SqlClient.SqlException
                'If timesheets save process fails, roll back transaction
                MsgBox(ex.Message, MsgBoxStyle.Critical, "SQL Error")
                If Not IsNothing(CommitTransaction) Then
                    CommitTransaction.Rollback()
                End If
            End Try
            LoadData.Enabled = True
            Me.Cursor = Cursors.Arrow
        End If

    End Sub

    'Load timesheet button
    Private Sub LoadData_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LoadData.Click
        'Execute load routine
        LoadTimesheet(CStr(Me.CompanyLookup.SelectedValue))
    End Sub

    'Dispose timesheetload form
    Private Sub TimesheetLoadForm_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Closed
        MyTimesheetLoadForm = Nothing
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
        DestinationDirectory = Environment.GetEnvironmentVariable("TEMP")
        MoveFiles = DestinationDirectory & "\" & CompanyID & ".csv"
        DestinationDirectory = System.Environment.GetEnvironmentVariable("TEMP")
        File.Copy(SchemaFileDirectory & "\" & SchemaFileName, DestinationDirectory & "\schema.ini", True)
        File.Copy(SourceFileName, MoveFiles, True)
    End Function
End Class
