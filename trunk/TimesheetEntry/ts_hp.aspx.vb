Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.SqlTypes
Imports System.Convert
Imports System.Globalization


Namespace TimeSheetV190_HTJV


    Partial Class ts_hp
        Inherits System.Web.UI.Page
        'Protected WithEvents tRow As System.Web.UI.WebControls.TableRow
        'Protected WithEvents tCell As System.Web.UI.WebControls.TableCell
        'Public wkno, modifieddate, verifieddate, vuserlogin, resubmitdate, ruserlogin, TSResubmit As String
        'Public ntsa, ntsu, ntmo, nttu, ntwe, ntth, ntfr, ntto
        'Public otsa, otsu, otmo, ottu, otwe, otth, otfr, otto
        'Public JustJoin, ccode, ccdesc, ctrc, week, stt, hourwork, Pending, Submit, diffDay, preStatus As String
        'Public ctr As Integer
        'Public count As Integer
        'Public jdate, rdate, sdate, edate As Date

        Protected WithEvents TimesheetDetail As TimesheetDatasets.TimesheetDetailDataTable
        Protected WithEvents SqlInsertCommand2 As System.Data.SqlClient.SqlCommand
        Protected WithEvents SqlConnection1 As System.Data.SqlClient.SqlConnection
        Protected WithEvents SqlSelectCommand1 As System.Data.SqlClient.SqlCommand
        Protected WithEvents SP_DeleteTS As System.Data.SqlClient.SqlCommand
        Protected WithEvents SP_InsertNewTS As System.Data.SqlClient.SqlCommand
        Protected WithEvents SP_UpdateTS As System.Data.SqlClient.SqlCommand
        Protected WithEvents sp_SubmitNewTS As System.Data.SqlClient.SqlCommand
        Protected WithEvents SP_SaveTSDefault As System.Data.SqlClient.SqlCommand
        Protected WithEvents SP_SubmitZeroHours As System.Data.SqlClient.SqlCommand
        Protected WithEvents DA_TimesheetList As System.Data.SqlClient.SqlDataAdapter
        Protected WithEvents TimesheetListSelectCommand As System.Data.SqlClient.SqlCommand
        Protected WithEvents DS_TimesheetList As TimesheetDatasets.TimesheetListDataTable
        Protected WithEvents DS_TimesheetDetails As TimesheetDatasets.TimesheetDetailDataTable
        Protected DS_RejectDetail As TimesheetDatasets.RejectDataTable
        Protected DS_ApprovedDetail As TimesheetDatasets.ApprovedDataTable

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
            Dim configurationAppSettings As System.Configuration.AppSettingsReader = New System.Configuration.AppSettingsReader()
            Me.SqlConnection1 = New System.Data.SqlClient.SqlConnection()
            Me.SqlSelectCommand1 = New System.Data.SqlClient.SqlCommand()
            Me.DA_TimesheetList = New System.Data.SqlClient.SqlDataAdapter()
            Me.SqlInsertCommand2 = New System.Data.SqlClient.SqlCommand()
            Me.TimesheetListSelectCommand = New System.Data.SqlClient.SqlCommand()
            Me.DS_TimesheetList = New TimesheetDatasets.TimesheetListDataTable
            Me.SP_DeleteTS = New System.Data.SqlClient.SqlCommand()
            Me.SP_InsertNewTS = New System.Data.SqlClient.SqlCommand()
            Me.SP_UpdateTS = New System.Data.SqlClient.SqlCommand()
            Me.sp_SubmitNewTS = New System.Data.SqlClient.SqlCommand()
            Me.SP_SaveTSDefault = New System.Data.SqlClient.SqlCommand()
            Me.SP_SubmitZeroHours = New System.Data.SqlClient.SqlCommand()

            'DA_VwETTSDefaultHoliday = New SqlClient.SqlDataAdapter
            DS_TimesheetDetails = New TimesheetDatasets.TimesheetDetailDataTable
            
            CType(Me.DS_TimesheetList, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.DS_TimesheetDetails, System.ComponentModel.ISupportInitialize).BeginInit()
            '
            'SqlConnection1
            '
            Me.SqlConnection1.ConnectionString = CType(configurationAppSettings.GetValue("MM_CONNECTION_STRING_WTSDB", GetType(System.String)), String)
            '
            'DA_TimesheetList
            '
            Me.DA_TimesheetList.SelectCommand = Me.TimesheetListSelectCommand
            Me.DA_TimesheetList.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "VwETTSWeek_All", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("TSYear", "TSYear"), New System.Data.Common.DataColumnMapping("TSWeek", "TSWeek"), New System.Data.Common.DataColumnMapping("YearWk", "YearWk"), New System.Data.Common.DataColumnMapping("StartDate", "StartDate"), New System.Data.Common.DataColumnMapping("EndDate", "EndDate"), New System.Data.Common.DataColumnMapping("EmpNo", "EmpNo"), New System.Data.Common.DataColumnMapping("Status", "Status"), New System.Data.Common.DataColumnMapping("StatusDesc", "StatusDesc")})})
            '
            ' TimesheetListSelectCommand
            '
            Me.TimesheetListSelectCommand.CommandText = "SELECT TOP 1 Year, Week, " & _
                                                        "Replace(convert(varchar(11),[StartDate],13),' ','-') as StartDate, " & _
                                                        "Replace(convert(varchar(11),[EndDate],13),' ','-') as EndDate " & _
                                                        "FROM TimesheetList WHERE Year = @Year AND Week = @Week"
            Me.TimesheetListSelectCommand.Parameters.Add("@Year", SqlDbType.Int)
            Me.TimesheetListSelectCommand.Parameters.Add("@Week", SqlDbType.Int)
            Me.TimesheetListSelectCommand.Connection = Me.SqlConnection1
            '
            ' DS_TimesheetList
            '
            Me.DS_TimesheetList.TableName = "TimesheetList"
            Me.DS_TimesheetList.Locale = New System.Globalization.CultureInfo("en-US")
            Me.DS_TimesheetList.Namespace = "http://www.tempuri.org/DS_TimesheetList.xsd"
            '
            'SP_DeleteTS
            '
            Me.SP_DeleteTS.CommandText = "dbo.[TimesheetDelete]"
            Me.SP_DeleteTS.CommandType = System.Data.CommandType.StoredProcedure
            Me.SP_DeleteTS.Connection = Me.SqlConnection1

            Me.SP_DeleteTS.Parameters.Add(New System.Data.SqlClient.SqlParameter("@EmployeeId", System.Data.SqlDbType.Int, 10))
            Me.SP_DeleteTS.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Year", System.Data.SqlDbType.Int, 10))
            Me.SP_DeleteTS.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Week", System.Data.SqlDbType.Int, 10))
            Me.SP_DeleteTS.Parameters.Add(New System.Data.SqlClient.SqlParameter("@CostCode", System.Data.SqlDbType.VarChar, 10))
            Me.SP_DeleteTS.Parameters.Add(New System.Data.SqlClient.SqlParameter("@CTRCode", System.Data.SqlDbType.VarChar, 25))
            '
            'SP_InsertNewTS
            '
            Me.SP_InsertNewTS.CommandText = "[TimesheetInsert]"
            Me.SP_InsertNewTS.CommandType = System.Data.CommandType.StoredProcedure
            Me.SP_InsertNewTS.Connection = Me.SqlConnection1

            Me.SP_InsertNewTS.Parameters.Add(New System.Data.SqlClient.SqlParameter("@EmployeeId", System.Data.SqlDbType.Int, 10))
            Me.SP_InsertNewTS.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Year", System.Data.SqlDbType.Int, 10))
            Me.SP_InsertNewTS.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Week", System.Data.SqlDbType.Int, 10))
            Me.SP_InsertNewTS.Parameters.Add(New System.Data.SqlClient.SqlParameter("@CostCode", System.Data.SqlDbType.VarChar, 10))
            Me.SP_InsertNewTS.Parameters.Add(New System.Data.SqlClient.SqlParameter("@CTRCode", System.Data.SqlDbType.VarChar, 25))

            Me.SP_InsertNewTS.Parameters.Add(New System.Data.SqlClient.SqlParameter("@NTSaturday", System.Data.SqlDbType.Money, 4))
            Me.SP_InsertNewTS.Parameters.Add(New System.Data.SqlClient.SqlParameter("@NTSunday", System.Data.SqlDbType.Money, 4))
            Me.SP_InsertNewTS.Parameters.Add(New System.Data.SqlClient.SqlParameter("@NTMonday", System.Data.SqlDbType.Money, 4))
            Me.SP_InsertNewTS.Parameters.Add(New System.Data.SqlClient.SqlParameter("@NTTuesday", System.Data.SqlDbType.Money, 4))
            Me.SP_InsertNewTS.Parameters.Add(New System.Data.SqlClient.SqlParameter("@NTWednesday", System.Data.SqlDbType.Money, 4))
            Me.SP_InsertNewTS.Parameters.Add(New System.Data.SqlClient.SqlParameter("@NTThursday", System.Data.SqlDbType.Money, 4))
            Me.SP_InsertNewTS.Parameters.Add(New System.Data.SqlClient.SqlParameter("@NTFriday", System.Data.SqlDbType.Money, 4))

            Me.SP_InsertNewTS.Parameters.Add(New System.Data.SqlClient.SqlParameter("@OTSaturday", System.Data.SqlDbType.Money, 4))
            Me.SP_InsertNewTS.Parameters.Add(New System.Data.SqlClient.SqlParameter("@OTSunday", System.Data.SqlDbType.Money, 4))
            Me.SP_InsertNewTS.Parameters.Add(New System.Data.SqlClient.SqlParameter("@OTMonday", System.Data.SqlDbType.Money, 4))
            Me.SP_InsertNewTS.Parameters.Add(New System.Data.SqlClient.SqlParameter("@OTTuesday", System.Data.SqlDbType.Money, 4))
            Me.SP_InsertNewTS.Parameters.Add(New System.Data.SqlClient.SqlParameter("@OTWednesday", System.Data.SqlDbType.Money, 4))
            Me.SP_InsertNewTS.Parameters.Add(New System.Data.SqlClient.SqlParameter("@OTThursday", System.Data.SqlDbType.Money, 4))
            Me.SP_InsertNewTS.Parameters.Add(New System.Data.SqlClient.SqlParameter("@OTFriday", System.Data.SqlDbType.Money, 4))
            Me.SP_InsertNewTS.Parameters.Add(New System.Data.SqlClient.SqlParameter("@UserLogin", System.Data.SqlDbType.VarChar, 50))


            '
            'SP_UpdateTS
            '
            Me.SP_UpdateTS.CommandText = "[TimesheetUpdate]"
            Me.SP_UpdateTS.CommandType = System.Data.CommandType.StoredProcedure
            Me.SP_UpdateTS.Connection = Me.SqlConnection1

            Me.SP_UpdateTS.Parameters.Add(New System.Data.SqlClient.SqlParameter("@EmployeeId", System.Data.SqlDbType.Int, 10))
            Me.SP_UpdateTS.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Year", System.Data.SqlDbType.Int, 10))
            Me.SP_UpdateTS.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Week", System.Data.SqlDbType.Int, 10))
            Me.SP_UpdateTS.Parameters.Add(New System.Data.SqlClient.SqlParameter("@CostCode", System.Data.SqlDbType.VarChar, 10))
            Me.SP_UpdateTS.Parameters.Add(New System.Data.SqlClient.SqlParameter("@CTRCode", System.Data.SqlDbType.VarChar, 25))

            Me.SP_UpdateTS.Parameters.Add(New System.Data.SqlClient.SqlParameter("@NTSaturday", System.Data.SqlDbType.Money, 4))
            Me.SP_UpdateTS.Parameters.Add(New System.Data.SqlClient.SqlParameter("@NTSunday", System.Data.SqlDbType.Money, 4))
            Me.SP_UpdateTS.Parameters.Add(New System.Data.SqlClient.SqlParameter("@NTMonday", System.Data.SqlDbType.Money, 4))
            Me.SP_UpdateTS.Parameters.Add(New System.Data.SqlClient.SqlParameter("@NTTuesday", System.Data.SqlDbType.Money, 4))
            Me.SP_UpdateTS.Parameters.Add(New System.Data.SqlClient.SqlParameter("@NTWednesday", System.Data.SqlDbType.Money, 4))
            Me.SP_UpdateTS.Parameters.Add(New System.Data.SqlClient.SqlParameter("@NTThursday", System.Data.SqlDbType.Money, 4))
            Me.SP_UpdateTS.Parameters.Add(New System.Data.SqlClient.SqlParameter("@NTFriday", System.Data.SqlDbType.Money, 4))

            Me.SP_UpdateTS.Parameters.Add(New System.Data.SqlClient.SqlParameter("@OTSaturday", System.Data.SqlDbType.Money, 4))
            Me.SP_UpdateTS.Parameters.Add(New System.Data.SqlClient.SqlParameter("@OTSunday", System.Data.SqlDbType.Money, 4))
            Me.SP_UpdateTS.Parameters.Add(New System.Data.SqlClient.SqlParameter("@OTMonday", System.Data.SqlDbType.Money, 4))
            Me.SP_UpdateTS.Parameters.Add(New System.Data.SqlClient.SqlParameter("@OTTuesday", System.Data.SqlDbType.Money, 4))
            Me.SP_UpdateTS.Parameters.Add(New System.Data.SqlClient.SqlParameter("@OTWednesday", System.Data.SqlDbType.Money, 4))
            Me.SP_UpdateTS.Parameters.Add(New System.Data.SqlClient.SqlParameter("@OTThursday", System.Data.SqlDbType.Money, 4))
            Me.SP_UpdateTS.Parameters.Add(New System.Data.SqlClient.SqlParameter("@OTFriday", System.Data.SqlDbType.Money, 4))
            Me.SP_UpdateTS.Parameters.Add(New System.Data.SqlClient.SqlParameter("@UserLogin", System.Data.SqlDbType.VarChar, 50))

            '
            'sp_SubmitNewTS
            '
            Me.sp_SubmitNewTS.CommandText = "dbo.[SubmitTimesheet]"
            Me.sp_SubmitNewTS.CommandType = System.Data.CommandType.StoredProcedure
            Me.sp_SubmitNewTS.Connection = Me.SqlConnection1

            Me.sp_SubmitNewTS.Parameters.Add(New System.Data.SqlClient.SqlParameter("@EmployeeId", System.Data.SqlDbType.Int, 10))
            Me.sp_SubmitNewTS.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Year", System.Data.SqlDbType.Int, 15))
            Me.sp_SubmitNewTS.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Week", System.Data.SqlDbType.Int, 4))
            Me.sp_SubmitNewTS.Parameters.Add(New System.Data.SqlClient.SqlParameter("@UserLogin", System.Data.SqlDbType.VarChar, 50))

            '
            'SP_InsertTSDefault
            '
            Me.SP_SaveTSDefault.CommandText = "dbo.[SaveTimesheetDefault]"
            Me.SP_SaveTSDefault.CommandType = System.Data.CommandType.StoredProcedure
            Me.SP_SaveTSDefault.Connection = Me.SqlConnection1

            Me.SP_SaveTSDefault.Parameters.Add(New System.Data.SqlClient.SqlParameter("@EmployeeID", System.Data.SqlDbType.Int, 10))
            Me.SP_SaveTSDefault.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Year", System.Data.SqlDbType.Int, 10))
            Me.SP_SaveTSDefault.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Week", System.Data.SqlDbType.Int, 25))
            '
            'SP_SubmitZeroHours
            '
            Me.SP_SubmitZeroHours.CommandText = "dbo.[SubmitZeroHoursTimesheet]"
            Me.SP_SubmitZeroHours.CommandType = System.Data.CommandType.StoredProcedure
            Me.SP_SubmitZeroHours.Connection = Me.SqlConnection1
            'Me.SP_SubmitZeroHours.Parameters.Add(New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, False, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
            Me.SP_SubmitZeroHours.Parameters.Add(New System.Data.SqlClient.SqlParameter("@EmployeeID", System.Data.SqlDbType.Int))
            Me.SP_SubmitZeroHours.Parameters.Add(New System.Data.SqlClient.SqlParameter("@year", System.Data.SqlDbType.Int))
            Me.SP_SubmitZeroHours.Parameters.Add(New System.Data.SqlClient.SqlParameter("@week", System.Data.SqlDbType.Int))          
            'Me.SP_SubmitZeroHours.Parameters.Add(New System.Data.SqlClient.SqlParameter("@modifieddate", System.Data.SqlDbType.DateTime, 4))
            Me.SP_SubmitZeroHours.Parameters.Add(New System.Data.SqlClient.SqlParameter("@userlogin", System.Data.SqlDbType.VarChar, 50))
            'Me.SP_SubmitZeroHours.Parameters.Add(New System.Data.SqlClient.SqlParameter("@verifieddate", System.Data.SqlDbType.DateTime, 4))
            'Me.SP_SubmitZeroHours.Parameters.Add(New System.Data.SqlClient.SqlParameter("@vuserlogin", System.Data.SqlDbType.VarChar, 50))
            'Me.SP_SubmitZeroHours.Parameters.Add(New System.Data.SqlClient.SqlParameter("@resubmitdate", System.Data.SqlDbType.DateTime, 4))
            'Me.SP_SubmitZeroHours.Parameters.Add(New System.Data.SqlClient.SqlParameter("@ruserlogin", System.Data.SqlDbType.VarChar, 50))
            'Me.SP_SubmitZeroHours.Parameters.Add(New System.Data.SqlClient.SqlParameter("@TSResubmit", System.Data.SqlDbType.VarChar, 1))

            CType(Me.DS_TimesheetList, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.DS_TimesheetDetails, System.ComponentModel.ISupportInitialize).EndInit()

        End Sub

        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

        Private Function GetStringField(ByVal sqlreader As SqlClient.SqlDataReader, ByVal Index As Integer) As String
            Try
                If sqlreader.IsDBNull(Index) Then
                    Return ""
                Else
                    Return sqlreader.GetString(Index)
                End If

            Catch ex As Exception
                Return ""
            End Try
        End Function
        Private Function GetMoneyField(ByVal sqlreader As SqlClient.SqlDataReader, ByVal Index As Integer) As Single
            Try
                If sqlreader.IsDBNull(Index) Then
                    Return 0
                Else
                    Return sqlreader.GetSqlMoney(Index)
                End If

            Catch ex As Exception
                Return 0
            End Try
        End Function
        Private Function FormatMoneyField(ByVal value As Single) As String
            Dim s As String
            s = value.ToString
            If s.IndexOf(".") > 0 Then
                s = s.Substring(0, s.IndexOf(".") + 2) ' cleanup of extra digits
                If s.Substring(s.IndexOf(".") + 1) = "00" Then
                    s = s.Substring(0, s.IndexOf(".")) ' nicer formatting
                End If
            End If
            Return s
        End Function

        Private Function GetTimesheetDetails(ByVal EmployeeId As Integer, _
                                                ByVal Year As Integer, _
                                                ByVal Week As Integer, _
                                                ByRef TimesheetTotals As TimesheetDatasets.TimesheetDetailRow) As TimesheetDatasets.TimesheetDetailDataTable
            Dim value As Single
            GetTimesheetDetails = New TimesheetDatasets.TimesheetDetailDataTable


            ' will return the totals to initialise the page the first time, other updates handled by javascript:changevalue();
            TimesheetTotals = GetTimesheetDetails.NewTimesheetDetailRow
            TimesheetTotals.NTSaturday = 0
            TimesheetTotals.NTSunday = 0
            TimesheetTotals.NTMonday = 0
            TimesheetTotals.NTTuesday = 0
            TimesheetTotals.NTWednesday = 0
            TimesheetTotals.NTThursday = 0
            TimesheetTotals.NTFriday = 0
            TimesheetTotals.NTTotal = 0

            TimesheetTotals.OTSaturday = 0
            TimesheetTotals.OTSunday = 0
            TimesheetTotals.OTMonday = 0
            TimesheetTotals.OTTuesday = 0
            TimesheetTotals.OTWednesday = 0
            TimesheetTotals.OTThursday = 0
            TimesheetTotals.OTFriday = 0
            TimesheetTotals.OTTotal = 0

            Dim TotalNTSa As Single = 0
            Dim TotalNTSu As Single = 0
            Dim TotalNTMo As Single = 0
            Dim TotalNTTu As Single = 0
            Dim TotalNTWe As Single = 0
            Dim TotalNTTh As Single = 0
            Dim TotalNTFr As Single = 0
            Dim TotalNTTot As Single = 0

            Dim TotalOTSa As Single = 0
            Dim TotalOTSu As Single = 0
            Dim TotalOTMo As Single = 0
            Dim TotalOTTu As Single = 0
            Dim TotalOTWe As Single = 0
            Dim TotalOTTh As Single = 0
            Dim TotalOTFr As Single = 0
            Dim TotalOTTot As Single = 0

            ' using simpler select to minimise the changes on the TSxxx stored procedures (for now)
            Dim SqlReader As SqlClient.SqlDataReader
            Dim sqlCmd As SqlClient.SqlCommand

            sqlCmd = New SqlClient.SqlCommand("SELECT	TimesheetDetail.CostCode, " & _
                                                        "TimesheetDetail.CTRCode, " & _
                                                        "CTRCode.Description, " & _
                                                        "TimesheetDetail.NTSaturday, " & _
                                                        "TimesheetDetail.NTSunday, " & _
                                                        "TimesheetDetail.NTMonday, " & _
                                                        "TimesheetDetail.NTTuesday, " & _
                                                        "TimesheetDetail.NTWednesday, " & _
                                                        "TimesheetDetail.NTThursday, " & _
                                                        "TimesheetDetail.NTFriday, " & _
                                                        "TimesheetDetail.TotalNormalTime, " & _
                                                        "TimesheetDetail.OTSaturday, " & _
                                                        "TimesheetDetail.OTSunday, " & _
                                                        "TimesheetDetail.OTMonday, " & _
                                                        "TimesheetDetail.OTTuesday, " & _
                                                        "TimesheetDetail.OTWednesday, " & _
                                                        "TimesheetDetail.OTThursday, " & _
                                                        "TimesheetDetail.OTFriday, " & _
                                                        "TimesheetDetail.TotalOverTime " & _
                                                "FROM TimesheetDetail " & _
                                                "INNER JOIN CTRCode ON CTRCode.CostCode = TimesheetDetail.CostCode AND CTRCode.CTRCode = TimesheetDetail.CTRCode " & _
                                                "WHERE EmployeeID = @EmployeeID " & _
                                                "AND [Year] = @Year " & _
                                                "AND [Week] = @Week", Me.SqlConnection1)
            sqlCmd.Parameters.Add(New SqlParameter("@EmployeeID", EmployeeId))
            sqlCmd.Parameters.Add(New SqlParameter("@Year", Year))
            sqlCmd.Parameters.Add(New SqlParameter("@Week", Week))
            If Me.SqlConnection1.State <> ConnectionState.Open Then
                Me.SqlConnection1.Open()
            End If
            Dim row As TimesheetDatasets.TimesheetDetailRow
            Try
                SqlReader = sqlCmd.ExecuteReader
                While SqlReader.Read
                    row = GetTimesheetDetails.NewRow
                    row.CostCode = GetStringField(SqlReader, 0)
                    row.CTRCode = GetStringField(SqlReader, 1)


                    row.Description = GetStringField(SqlReader, 2)

                    value = GetMoneyField(SqlReader, 3)
                    row.NTSaturday = FormatMoneyField(value)
                    TotalNTSa += TotalNTSa + value

                    value = GetMoneyField(SqlReader, 4)
                    row.NTSunday = FormatMoneyField(value)
                    TotalNTSu = TotalNTSu + value

                    value = GetMoneyField(SqlReader, 5)
                    row.NTMonday = FormatMoneyField(value)
                    TotalNTMo = TotalNTMo + value

                    value = GetMoneyField(SqlReader, 6)
                    row.NTTuesday = FormatMoneyField(value)
                    TotalNTTu = TotalNTTu + value

                    value = GetMoneyField(SqlReader, 7)
                    row.NTWednesday = FormatMoneyField(value)
                    TotalNTWe = TotalNTWe + value

                    value = GetMoneyField(SqlReader, 8)
                    row.NTThursday = FormatMoneyField(value)
                    TotalNTTh = TotalNTTh + value

                    value = GetMoneyField(SqlReader, 9)
                    row.NTFriday = FormatMoneyField(value)
                    TotalNTFr = TotalNTFr + value

                    value = GetMoneyField(SqlReader, 10)
                    row.NTTotal = FormatMoneyField(value)
                    TotalNTTot = TotalNTTot + value

                    value = GetMoneyField(SqlReader, 11)
                    row.OTSaturday = FormatMoneyField(value)
                    TotalOTSa = TotalOTSa + value

                    value = GetMoneyField(SqlReader, 12)
                    row.OTSunday = FormatMoneyField(value)
                    TotalOTSu = TotalOTSu + value

                    value = GetMoneyField(SqlReader, 13)
                    row.OTMonday = FormatMoneyField(value)
                    TotalOTMo = TotalOTMo + value

                    value = GetMoneyField(SqlReader, 14)
                    row.OTTuesday = FormatMoneyField(value)
                    TotalOTTu = TotalOTTu + value

                    value = GetMoneyField(SqlReader, 15)
                    row.OTWednesday = FormatMoneyField(value)
                    TotalOTWe = TotalOTWe + value

                    value = GetMoneyField(SqlReader, 16)
                    row.OTThursday = FormatMoneyField(value)
                    TotalOTTh = TotalOTTh + value

                    value = GetMoneyField(SqlReader, 17)
                    row.OTFriday = FormatMoneyField(value)
                    TotalOTFr = TotalOTFr + value

                    value = GetMoneyField(SqlReader, 18)
                    row.OTTotal = FormatMoneyField(value)
                    TotalOTTot = TotalOTTot + value

                    GetTimesheetDetails.AddTimesheetDetailRow(row)
                End While

                TimesheetTotals.NTSaturday = FormatMoneyField(TotalNTSa)
                TimesheetTotals.NTSunday = FormatMoneyField(TotalNTSu)
                TimesheetTotals.NTMonday = FormatMoneyField(TotalNTMo)
                TimesheetTotals.NTTuesday = FormatMoneyField(TotalNTTu)
                TimesheetTotals.NTWednesday = FormatMoneyField(TotalNTWe)
                TimesheetTotals.NTThursday = FormatMoneyField(TotalNTTh)
                TimesheetTotals.NTFriday = FormatMoneyField(TotalNTFr)
                TimesheetTotals.NTTotal = FormatMoneyField(TotalNTTot)

                TimesheetTotals.OTSaturday = FormatMoneyField(TotalOTSa)
                TimesheetTotals.OTSunday = FormatMoneyField(TotalOTSu)
                TimesheetTotals.OTMonday = FormatMoneyField(TotalOTMo)
                TimesheetTotals.OTTuesday = FormatMoneyField(TotalOTTu)
                TimesheetTotals.OTWednesday = FormatMoneyField(TotalOTWe)
                TimesheetTotals.OTThursday = FormatMoneyField(TotalOTTh)
                TimesheetTotals.OTFriday = FormatMoneyField(TotalOTFr)
                TimesheetTotals.OTTotal = FormatMoneyField(TotalOTTot)

            Catch ex As Exception
                Session("errmsg") = "ERR-HP-3001: GetTimesheetDetail - " + ex.Message.ToString
                Response.Redirect("ErrMsg.aspx")
            Finally
                If Not IsNothing(SqlReader) Then
                    SqlReader.Close()
                End If
            End Try
            ' If now rows were found, it is a new timesheet, try to get the default timesheet if available
            If GetTimesheetDetails.Rows.Count = 0 Then
                sqlCmd = New SqlClient.SqlCommand("SELECT [TimesheetDefault].[CostCode], [TimesheetDefault].[CtrCode], [CTRCode].[Description] " & _
                                                    "FROM [TimesheetDefault] " & _
                                                    "INNER JOIN [CTRCode] ON [CTRCode].[CostCode] = [TimesheetDefault].[CostCode] AND [CTRCode].[CTRCode] = [TimesheetDefault].[CTRCode] " & _
                                                    "INNER JOIN [CostCode] ON [CostCode].[CostCode] = [TimesheetDefault].[CostCode] " & _
                                                    "WHERE [EmployeeID] = @EmployeeID AND [CTRCode].[Active] = 1 AND [CostCode].[Active] = 1", Me.SqlConnection1)
                sqlCmd.Parameters.Add(New SqlParameter("@EmployeeID", EmployeeID))
                Try
                    SqlReader = sqlCmd.ExecuteReader
                    While SqlReader.Read
                        row = GetTimesheetDetails.NewRow
                        row.CostCode = GetStringField(SqlReader, 0)
                        row.CTRCode = GetStringField(SqlReader, 1)
                        row.Description = GetStringField(SqlReader, 2)
                        row.NTSaturday = 0
                        row.NTSunday = 0
                        row.NTMonday = 0
                        row.NTTuesday = 0
                        row.NTWednesday = 0
                        row.NTThursday = 0
                        row.NTFriday = 0
                        row.NTTotal = 0

                        row.OTSaturday = 0
                        row.OTSunday = 0
                        row.OTMonday = 0
                        row.OTTuesday = 0
                        row.OTWednesday = 0
                        row.OTThursday = 0
                        row.OTFriday = 0
                        row.OTTotal = 0

                        GetTimesheetDetails.AddTimesheetDetailRow(row)
                    End While
                Catch ex As Exception

                Finally
                    If Not IsNothing(SqlReader) Then
                        SqlReader.Close()
                    End If
                End Try
            End If
            GetTimesheetDetails.AcceptChanges()
        End Function

        Private Function GetRejectDetails(ByVal EmployeeId As Integer, _
                                                ByVal Year As Integer, _
                                                ByVal Week As Integer) As TimesheetDatasets.RejectDataTable

            GetRejectDetails = New TimesheetDatasets.RejectDataTable

            ' using simpler select to minimise the changes on the TSxxx stored procedures (for now)
            Dim SqlReader As SqlClient.SqlDataReader
            Dim sqlCmd As SqlClient.SqlCommand

            sqlCmd = New SqlClient.SqlCommand("SELECT	TimesheetDetail.CostCode, " & _
                                                        "TimesheetDetail.CTRCode, " & _
                                                        "CTRCode.Description, " & _
                                                        "TimesheetDetail.RejectedBy, " & _
                                                        "TimesheetDetail.RejectReason " & _
                                                "FROM TimesheetDetail " & _
                                                "INNER JOIN CTRCode ON CTRCode.CostCode = TimesheetDetail.CostCode AND CTRCode.CTRCode = TimesheetDetail.CTRCode " & _
                                                "WHERE EmployeeID = @EmployeeID " & _
                                                "AND [Year] = @Year " & _
                                                "AND [Week] = @Week " & _
                                                "AND [Rejected] = 1", Me.SqlConnection1)
            sqlCmd.Parameters.Add(New SqlParameter("@EmployeeID", EmployeeId))
            sqlCmd.Parameters.Add(New SqlParameter("@Year", Year))
            sqlCmd.Parameters.Add(New SqlParameter("@Week", Week))
            If Me.SqlConnection1.State <> ConnectionState.Open Then
                Me.SqlConnection1.Open()
            End If
            Dim row As TimesheetDatasets.RejectRow
            Try
                SqlReader = sqlCmd.ExecuteReader
                While SqlReader.Read
                    row = GetRejectDetails.NewRejectRow
                    row.CostCode = GetStringField(SqlReader, 0)
                    row.CTRCode = GetStringField(SqlReader, 1)
                    row.Description = GetStringField(SqlReader, 2)
                    row.RejectedBy = GetStringField(SqlReader, 3)
                    row.RejectReason = GetStringField(SqlReader, 4)
                    GetRejectDetails.AddRejectRow(row)
                End While

            Catch ex As Exception
                Session("errmsg") = "ERR-HP-3002: GetRejectDetail - " + ex.Message.ToString
                Response.Redirect("ErrMsg.aspx")
            Finally
                If Not IsNothing(SqlReader) Then
                    SqlReader.Close()
                End If
            End Try
            GetRejectDetails.AcceptChanges()
        End Function

        Private Function GetApprovedDetails(ByVal EmployeeId As Integer, _
                                        ByVal Year As Integer, _
                                        ByVal Week As Integer) As TimesheetDatasets.ApprovedDataTable

            GetApprovedDetails = New TimesheetDatasets.ApprovedDataTable

            ' using simpler select to minimise the changes on the TSxxx stored procedures (for now)
            Dim SqlReader As SqlClient.SqlDataReader
            Dim sqlCmd As SqlClient.SqlCommand

            sqlCmd = New SqlClient.SqlCommand("SELECT	TimesheetDetail.CostCode, " & _
                                                        "TimesheetDetail.CTRCode, " & _
                                                        "CTRCode.Description, " & _
                                                        "TimesheetDetail.ApprovedBy, " & _
                                                        "CONVERT(varchar(11), TimesheetDetail.ApprovedDate,13) " & _
                                                "FROM TimesheetDetail " & _
                                                "INNER JOIN CTRCode ON CTRCode.CostCode = TimesheetDetail.CostCode AND CTRCode.CTRCode = TimesheetDetail.CTRCode " & _
                                                "WHERE EmployeeID = @EmployeeID " & _
                                                "AND [Year] = @Year " & _
                                                "AND [Week] = @Week " & _
                                                "AND [Approved] = 1", Me.SqlConnection1)
            sqlCmd.Parameters.Add(New SqlParameter("@EmployeeID", EmployeeId))
            sqlCmd.Parameters.Add(New SqlParameter("@Year", Year))
            sqlCmd.Parameters.Add(New SqlParameter("@Week", Week))
            If Me.SqlConnection1.State <> ConnectionState.Open Then
                Me.SqlConnection1.Open()
            End If
            Dim row As TimesheetDatasets.ApprovedRow
            Try
                SqlReader = sqlCmd.ExecuteReader
                While SqlReader.Read
                    row = GetApprovedDetails.NewApprovedRow
                    row.CostCode = GetStringField(SqlReader, 0)
                    row.CTRCode = GetStringField(SqlReader, 1)
                    row.Description = GetStringField(SqlReader, 2)
                    row.ApprovedBy = GetStringField(SqlReader, 3)
                    row.ApprovedDate = GetStringField(SqlReader, 4)
                    GetApprovedDetails.AddApprovedRow(row)
                End While

            Catch ex As Exception
                Session("errmsg") = "ERR-HP-3003: GetApprovedDetail - " + ex.Message.ToString
                Response.Redirect("ErrMsg.aspx")
            Finally
                If Not IsNothing(SqlReader) Then
                    SqlReader.Close()
                End If
            End Try
            GetApprovedDetails.AcceptChanges()
        End Function

        Private Sub WriteTimesheet(ByVal EmployeeInfo As EmployeeInfoStruct, ByVal Year As Integer, ByVal Week As Integer, ByRef TimesheetDetail As TimesheetDatasets.TimesheetDetailDataTable, ByRef RowsToDelete As TimesheetDatasets.TimesheetDetailDataTable)
            If RowsToDelete.Count > 0 Then
                If SP_DeleteTS.Connection.State <> ConnectionState.Open Then
                    SP_DeleteTS.Connection.Open()
                End If
                For Each row As TimesheetDatasets.TimesheetDetailRow In RowsToDelete.Rows
                    SP_DeleteTS.Parameters("@EmployeeId").Value = EmployeeInfo.EmployeeID
                    SP_DeleteTS.Parameters("@Year").Value = Year
                    SP_DeleteTS.Parameters("@Week").Value = Week
                    SP_DeleteTS.Parameters("@CostCode").Value = row.CostCode
                    SP_DeleteTS.Parameters("@CTRCode").Value = row.CTRCode
                    SP_DeleteTS.ExecuteNonQuery()
                Next
            End If

            If TimesheetDetail.Rows.Count > 0 Then
                If SP_UpdateTS.Connection.State <> ConnectionState.Open Then
                    SP_UpdateTS.Connection.Open()
                End If
                For Each row As TimesheetDatasets.TimesheetDetailRow In TimesheetDetail.Rows
                    SP_UpdateTS.Parameters("@EmployeeId").Value = EmployeeInfo.EmployeeID
                    SP_UpdateTS.Parameters("@Year").Value = Year
                    SP_UpdateTS.Parameters("@Week").Value = Week
                    SP_UpdateTS.Parameters("@CostCode").Value = row.CostCode
                    SP_UpdateTS.Parameters("@CTRCode").Value = row.CTRCode
                    SP_UpdateTS.Parameters("@NTSaturday").Value = row.NTSaturday
                    SP_UpdateTS.Parameters("@NTSunday").Value = row.NTSunday
                    SP_UpdateTS.Parameters("@NTMonday").Value = row.NTMonday
                    SP_UpdateTS.Parameters("@NTTuesday").Value = row.NTTuesday
                    SP_UpdateTS.Parameters("@NTWednesday").Value = row.NTWednesday
                    SP_UpdateTS.Parameters("@NTThursday").Value = row.NTThursday
                    SP_UpdateTS.Parameters("@NTFriday").Value = row.NTFriday
                    SP_UpdateTS.Parameters("@OTSaturday").Value = row.OTSaturday
                    SP_UpdateTS.Parameters("@OTSunday").Value = row.OTSunday
                    SP_UpdateTS.Parameters("@OTMonday").Value = row.OTMonday
                    SP_UpdateTS.Parameters("@OTTuesday").Value = row.OTTuesday
                    SP_UpdateTS.Parameters("@OTWednesday").Value = row.OTWednesday
                    SP_UpdateTS.Parameters("@OTThursday").Value = row.OTThursday
                    SP_UpdateTS.Parameters("@OTFriday").Value = row.OTFriday
                    SP_UpdateTS.Parameters("@UserLogin").Value = User.Identity.Name
                    SP_UpdateTS.ExecuteNonQuery()
                Next
            End If
            TimesheetDetail.AcceptChanges()
        End Sub
        Private Sub Fill_Header(ByVal EmployeeInfo As EmployeeInfoStruct, ByVal Year As Integer, ByVal Week As Integer)
            Try
                DA_TimesheetList.SelectCommand.Parameters("@Year").Value = Year
                DA_TimesheetList.SelectCommand.Parameters("@Week").Value = Week
                DA_TimesheetList.Fill(DS_TimesheetList)
            Catch ex As SqlClient.SqlException
                Session("errmsg") = "ERR-HP-1001: " + ex.Number.ToString + " - " + ex.Message.ToString
                Response.Redirect("ErrMsg.aspx")
            Catch ex As Exception
                Session("errmsg") = "ERR-HP-1002: " + ex.Source.ToString + " - " + ex.Message.ToString
                Response.Redirect("ErrMsg.aspx")
            End Try

            hdr_no.Text = EmployeeInfo.EmployeeNumber
            hdr_nm.Text = EmployeeInfo.EmployeeName
            hdr_dept.Text = EmployeeInfo.DisciplineDescription
            hdr_disc.Text = EmployeeInfo.DisciplineCode
            hdr_pos.Text = "N/A"
            hdr_com.Text = EmployeeInfo.Company
            HDRYear.Text = Year

            If DS_TimesheetList.Count > 0 Then
                hdr_week.Text = Week.ToString
                hdr_sdate.Text = DS_TimesheetList(0).StartDate
            Else
                Session("errmsg") = "ERR-HP-1004: No Record Found for that week!"
                Response.Redirect("ErrMsg.aspx")
            End If
        End Sub

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here

            Dim EmployeeInfo As EmployeeInfoStruct
            Dim Year As Integer
            Dim Week As Integer
            Dim YearWeek As Integer
            'Dim TimesheetStatus As Char

            EmployeeInfo = CType(Session("EmployeeInfo"), EmployeeInfoStruct)
            Session("errmsg") = ""

            Week = Request.Params("Week")
            Year = Request.Params("Year")
            YearWeek = Integer.Parse(Year.ToString + Week.ToString("D2"))

            If Not Me.IsPostBack Then
                If Not IsNothing(Request.Params("ReadOnly")) Then
                    ' make all readonly...

                    Me.btn_Save.Enabled = False
                    Me.btn_Submit.Enabled = False
                    Me.cb_SubmitNoManHours.Enabled = False
                    Me.rb_Default.Enabled = False
                Else
                    btn_Submit.Attributes.Add("onclick", "changevalue()")
                    btn_Save.Attributes.Add("onclick", "changevalue()")
                End If

                ' LGX 06-08-10 Fix pour empecher les user de faire un "set default timesheet" avec des anciens cost code"
                If Week < 32 And Year < 2011 Then
                    Me.rb_Default.Enabled = False
                End If

                ' fill the page header
                Fill_Header(EmployeeInfo, Year, Week)

                Dim TimesheetTotals As TimesheetDatasets.TimesheetDetailRow
                TimesheetDetail = GetTimesheetDetails(EmployeeInfo.EmployeeID, Year, Week, TimesheetTotals)
                TimesheetDetail.Columns.Add(New DataColumn("Selected", GetType(Boolean)))

                Session("TimesheetDetail") = TimesheetDetail

                If Not IsNothing(Request.Params("ReadOnly")) Then
                    ReadOnlyRepeater.DataSource = TimesheetDetail
                    ReadOnlyRepeater.DataBind()
                    ClientScript.RegisterStartupScript(GetType(String), "Readonly", "<script language=""javascript"" type=""text/jscript"" >makeReadOnly();</script>")
                Else
                    Repeater1.DataSource = TimesheetDetail
                    Repeater1.DataBind()
                End If

                DS_RejectDetail = GetRejectDetails(EmployeeInfo.EmployeeID, Year, Week)
                If DS_RejectDetail.Count > 0 Then
                    RejectedRepeater.DataSource = DS_RejectDetail
                    RejectedRepeater.DataBind()
                    ClientScript.RegisterStartupScript(GetType(String), "ShowRejectTable", "<script language=""javascript"" type=""text/jscript"" >ShowRejectedTable();</script>")
                End If

                DS_ApprovedDetail = GetApprovedDetails(EmployeeInfo.EmployeeID, Year, Week)
                If DS_ApprovedDetail.Count > 0 Then
                    ApprovedRepeater.DataSource = DS_ApprovedDetail
                    ApprovedRepeater.DataBind()
                    ClientScript.RegisterStartupScript(GetType(String), "ShowApprovedTable", "<script language=""javascript"" type=""text/jscript"" >ShowApprovedTable();</script>")
                End If

            End If
        End Sub

        Private Function NewTimesheetRow(ByVal Timesheet As TimesheetDatasets.TimesheetDetailDataTable, _
                                                ByVal CostCode As String, _
                                                ByVal CTRCode As String, _
                                                ByVal NTSaturday As String, _
                                                ByVal NTSunday As String, _
                                                ByVal NTMonday As String, _
                                                ByVal NTTuesday As String, _
                                                ByVal NTWednesday As String, _
                                                ByVal NTThursday As String, _
                                                ByVal NTFriday As String, _
                                                ByVal OTSaturday As String, _
                                                ByVal OTSunday As String, _
                                                ByVal OTMonday As String, _
                                                ByVal OTTuesday As String, _
                                                ByVal OTWednesday As String, _
                                                ByVal OTThursday As String, _
                                                ByVal OTFriday As String) As TimesheetDatasets.TimesheetDetailRow
            NewTimesheetRow = Timesheet.NewTimesheetDetailRow
            NewTimesheetRow.CostCode = CostCode
            NewTimesheetRow.CTRCode = CTRCode
            NewTimesheetRow.NTSaturday = NTSaturday
            NewTimesheetRow.NTSunday = NTSunday
            NewTimesheetRow.NTMonday = NTMonday
            NewTimesheetRow.NTTuesday = NTTuesday
            NewTimesheetRow.NTWednesday = NTWednesday
            NewTimesheetRow.NTThursday = NTThursday
            NewTimesheetRow.NTFriday = NTFriday
            NewTimesheetRow.OTSaturday = OTSaturday
            NewTimesheetRow.OTSunday = OTSunday
            NewTimesheetRow.OTMonday = OTMonday
            NewTimesheetRow.OTTuesday = OTTuesday
            NewTimesheetRow.OTWednesday = OTWednesday
            NewTimesheetRow.OTThursday = OTThursday
            NewTimesheetRow.OTFriday = OTFriday
        End Function

        Private Function ReadRepeaterStringValue(ByVal item As String, ByVal row As Integer) As String
            ReadRepeaterStringValue = Trim(Request.Form(item + row.ToString))
        End Function
        Private Function ReadRepeaterHourValue(ByVal item As String, ByVal row As Integer) As Single
            Dim s As String
            s = Trim(Request.Form(item + row.ToString))
            If s.Length > 0 Then
                ReadRepeaterHourValue = Single.Parse(s)
            Else
                ReadRepeaterHourValue = 0
            End If
        End Function

        Private Function GetItemList() As Collections.ArrayList
            GetItemList = New Collections.ArrayList
            Dim s As String
            Dim ID As Integer
            For i As Integer = 0 To Request.Params.Count - 1
                If Request.Params.GetKey(i).StartsWith("ccdesc") Then
                    s = Request.Params.GetKey(i)
                    s = s.Replace("ccdesc", "")
                    If Integer.TryParse(s, ID) Then
                        GetItemList.Add(ID)
                    End If
                End If
            Next
        End Function
        Private Sub UpdateLocalDataset(ByRef TimesheetDetail As TimesheetDatasets.TimesheetDetailDataTable, ByRef RowsToDelete As TimesheetDatasets.TimesheetDetailDataTable)
            TimesheetDetail.Clear()
            TimesheetDetail.AcceptChanges()

            Dim row As TimesheetDatasets.TimesheetDetailRow
            Dim CostCode As String
            Dim CTRCode As String
            Dim ntsa, ntsu, ntmo, nttu, ntwe, ntth, ntfr, ntto As Single
            Dim otsa, otsu, otmo, ottu, otwe, otth, otfr, otto As Single

            Dim SelectStmt As String

            For Each ctr As Integer In GetItemList()
                CostCode = ReadRepeaterStringValue("ccode", ctr)
                CTRCode = ReadRepeaterStringValue("ctrc", ctr)
                ntsa = ReadRepeaterHourValue("ntsa", ctr)
                ntsu = ReadRepeaterHourValue("ntsu", ctr)
                ntmo = ReadRepeaterHourValue("ntmo", ctr)
                nttu = ReadRepeaterHourValue("nttu", ctr)
                ntwe = ReadRepeaterHourValue("ntwe", ctr)
                ntth = ReadRepeaterHourValue("ntth", ctr)
                ntfr = ReadRepeaterHourValue("ntfr", ctr)
                ntto = ReadRepeaterHourValue("ntto", ctr)
                otsa = ReadRepeaterHourValue("otsa", ctr)
                otsu = ReadRepeaterHourValue("otsu", ctr)
                otmo = ReadRepeaterHourValue("otmo", ctr)
                ottu = ReadRepeaterHourValue("ottu", ctr)
                otwe = ReadRepeaterHourValue("otwe", ctr)
                otth = ReadRepeaterHourValue("otth", ctr)
                otfr = ReadRepeaterHourValue("otfr", ctr)
                otto = ReadRepeaterHourValue("otto", ctr)

                ' check for rows with 0 hours
                If Math.Abs(ntsa) < 0.1 And Math.Abs(ntsu) < 0.1 And Math.Abs(ntmo) < 0.1 And Math.Abs(nttu) < 0.1 And Math.Abs(ntwe) < 0.1 And Math.Abs(ntth) < 0.1 And Math.Abs(ntfr) < 0.1 And _
                    Math.Abs(otsa) < 0.1 And Math.Abs(otsu) < 0.1 And Math.Abs(otmo) < 0.1 And Math.Abs(ottu) < 0.1 And Math.Abs(otwe) < 0.1 And Math.Abs(otth) < 0.1 And Math.Abs(otfr) < 0.1 Then
                    Continue For
                    ' note that this will also keep the row in the ToDeleteDS
                End If

                ' update the dataset
                SelectStmt = "CostCode = '" & CostCode & "' And CTRCode = '" & CTRCode & "'"
                If TimesheetDetail.Select(SelectStmt).Length > 0 Then
                    ' this mean we already have one row with the same costcode and ctrcode
                    ' we add the hours to this row
                    ' there shoudle be only one, so we can safely use the row zero
                    row = CType(TimesheetDetail.Select(SelectStmt)(0), TimesheetDatasets.TimesheetDetailRow)
                    row.NTSaturday += ntsa
                    row.NTSunday += ntsu
                    row.NTMonday += ntmo
                    row.NTTuesday += nttu
                    row.NTWednesday += ntwe
                    row.NTThursday += ntth
                    row.NTFriday += ntfr
                    row.OTSaturday += otsa
                    row.OTSunday += otsu
                    row.OTMonday += otmo
                    row.OTTuesday += ottu
                    row.OTWednesday += otwe
                    row.OTThursday += otth
                    row.OTFriday += otfr
                Else
                    ' we add a new row
                    row = Me.NewTimesheetRow(TimesheetDetail, _
                            CostCode, _
                            CTRCode, _
                            ntsa, ntsu, ntmo, nttu, ntwe, ntth, ntfr, _
                            otsa, otsu, otmo, ottu, otwe, otth, otfr)
                    TimesheetDetail.Rows.Add(row)
                End If
                If RowsToDelete.Select(SelectStmt).Length > 0 Then
                    RowsToDelete.Rows.Remove(RowsToDelete.Select(SelectStmt)(0))
                End If
            Next

            ' should do nothing - to remove after initial testing
            For Each row In RowsToDelete
                SelectStmt = "CostCode = '" & row.CostCode & "' And CTRCode = '" & row.CTRCode & "'"
                If TimesheetDetail.Select(SelectStmt).Length > 0 Then
                    TimesheetDetail.Rows.Remove(TimesheetDetail.Select(SelectStmt)(0))
                End If
            Next

        End Sub
        Private Sub btn_Save_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Save.Click

            'Dim nfo = New CultureInfo("en-US")
            Dim EmployeeInfo As EmployeeInfoStruct
            Dim Year As Integer
            Dim Week As Integer

            EmployeeInfo = CType(Session("EmployeeInfo"), EmployeeInfoStruct)
            Year = CType(Request.Params("Year"), Integer)
            Week = CType(Request.Params("Week"), Integer)

            TimesheetDetail = Session("TimesheetDetail")
            TimesheetDetail.AcceptChanges() ' just in case

            Dim RowsToDelete As TimesheetDatasets.TimesheetDetailDataTable
            RowsToDelete = TimesheetDetail.Copy

            UpdateLocalDataset(TimesheetDetail, RowsToDelete)


            ' apply the changes in the database
            WriteTimesheet(EmployeeInfo, Year, Week, TimesheetDetail, RowsToDelete)
            ' keep as default button is checked
            If rb_Default.Checked = True Then
                SaveDefault(EmployeeInfo.EmployeeID, Year, Week)
            End If

            If Not IsNothing(Session("OnBehalfUser")) Then
                Dim OnBehalfOf As String
                OnBehalfOf = EmployeeInfo.EmployeeLogin
                Session("EmployeeInfo") = Session("OnBehalfUser")
                Response.Redirect("ts_menu.aspx?OnBehalf=" + OnBehalfOf)
            End If
            Response.Redirect("ts_menu.aspx")
        End Sub

        Private Sub SaveDefault(ByVal EmployeeId As Integer, ByVal Year As Integer, ByVal Week As Integer)
            SP_SaveTSDefault.Parameters("@EmployeeID").Value = EmployeeId
            SP_SaveTSDefault.Parameters("@Year").Value = Year
            SP_SaveTSDefault.Parameters("@Week").Value = Week

            Try
                If SP_SaveTSDefault.Connection.State <> ConnectionState.Open Then
                    SP_SaveTSDefault.Connection.Open()
                End If
                SP_SaveTSDefault.ExecuteNonQuery()
            Catch Exp As Exception
                Session("errmsg") = "ERR-HP-2001: " + Exp.Source.ToString + " - " + Exp.Message.ToString
                Response.Redirect("ErrMsg.aspx")
            Finally
                SP_SaveTSDefault.Connection.Close()
            End Try
        End Sub
        Private Sub btn_Submit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Submit.Click
            'Dim nfo = New CultureInfo("en-US")
            Dim EmployeeInfo As EmployeeInfoStruct
            Dim Year As Integer
            Dim Week As Integer

            EmployeeInfo = CType(Session("EmployeeInfo"), EmployeeInfoStruct)
            Year = CType(Request.Params("Year"), Integer)
            Week = CType(Request.Params("Week"), Integer)

            TimesheetDetail = Session("TimesheetDetail")
            TimesheetDetail.AcceptChanges() ' just in case

            Dim RowsToDelete As TimesheetDatasets.TimesheetDetailDataTable
            RowsToDelete = TimesheetDetail.Copy

            UpdateLocalDataset(TimesheetDetail, RowsToDelete)

            ' apply the changes in the database
            WriteTimesheet(EmployeeInfo, Year, Week, TimesheetDetail, RowsToDelete)

            sp_SubmitNewTS.Parameters("@EmployeeId").Value = EmployeeInfo.EmployeeID
            sp_SubmitNewTS.Parameters("@Year").Value = Year
            sp_SubmitNewTS.Parameters("@Week").Value = Week
            sp_SubmitNewTS.Parameters("@UserLogin").Value = User.Identity.Name
            sp_SubmitNewTS.ExecuteNonQuery()

            ' keep as default button is checked
            If rb_Default.Checked = True Then
                SaveDefault(EmployeeInfo.EmployeeID, Year, Week)
            End If

            If Not IsNothing(Session("OnBehalfUser")) Then
                Dim OnBehalfOf As String
                OnBehalfOf = EmployeeInfo.EmployeeLogin
                Session("EmployeeInfo") = Session("OnBehalfUser")
                Response.Redirect("ts_menu.aspx?OnBehalf=" + OnBehalfOf)
            End If
            Response.Redirect("ts_menu.aspx")
        End Sub

        Private Sub cb_SubmitNoManHours_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_SubmitNoManHours.CheckedChanged
            If cb_SubmitNoManHours.Checked = True Then
                btn_SubmitNoManHours.Enabled = True
            Else
                btn_SubmitNoManHours.Enabled = False
            End If
        End Sub

        Private Sub btn_SubmitNoManHours_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_SubmitNoManHours.Click
            'Dim zd = New DateTime()
            'Dim zfo = New CultureInfo("en-US")

            Dim EmployeeInfo As EmployeeInfoStruct
            Dim Year As Integer
            Dim Week As Integer      

            EmployeeInfo = CType(Session("EmployeeInfo"), EmployeeInfoStruct)
            Year = CType(Request.Params("Year"), Integer)
            Week = CType(Request.Params("Week"), Integer)                       

            Diagnostics.Debugger.Break()
            
            SP_SubmitZeroHours.Parameters("@EmployeeId").Value = EmployeeInfo.EmployeeID
            SP_SubmitZeroHours.Parameters("@Year").Value = Year
            SP_SubmitZeroHours.Parameters("@Week").Value = Week           
            SP_SubmitZeroHours.Parameters("@userlogin").Value = User.Identity.Name

            Try
                SP_SubmitZeroHours.Connection.Open()
                SP_SubmitZeroHours.ExecuteNonQuery()
            Catch Exp As Exception
                Session("errmsg") = "ERR-HP-4001: " + Exp.Source.ToString + " - " + Exp.Message.ToString
                Response.Redirect("ErrMsg.aspx")
            Finally
                SP_SubmitZeroHours.Connection.Close()
            End Try
            Session("action") = "submitted"
            Diagnostics.Debugger.Break()
            If Not IsNothing(Session("OnBehalfUser")) Then
                Dim OnBehalfOf As String
                OnBehalfOf = EmployeeInfo.EmployeeLogin
                Session("EmployeeInfo") = Session("OnBehalfUser")
                Response.Redirect("ts_menu.aspx?OnBehalf=" + OnBehalfOf)
            End If
            Response.Redirect("ts_menu.aspx")
        End Sub

        Protected Sub BackButton_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles BackButton.ServerClick
            Dim NavigationObject As Stack
            If Not IsNothing(Session("NavigationObject")) Then
                NavigationObject = Session("NavigationObject")
                Dim s As String
                s = NavigationObject.Pop
                Session("NavigationObject") = NavigationObject
                Response.Redirect(s)
            End If
        End Sub
    End Class

End Namespace
