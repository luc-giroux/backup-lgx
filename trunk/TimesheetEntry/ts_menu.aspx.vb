Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing


Namespace TimeSheetV190_HTJV
    'Option Explicit On
    Partial Class ts_menu
        Inherits System.Web.UI.Page
        Dim formattedAuthUser, rs_EmpRecords, yrwkvalue, stt, nowyear, users As String
        Protected WithEvents SqlConnection1 As System.Data.SqlClient.SqlConnection
        Protected WithEvents DA_TimesheetList As System.Data.SqlClient.SqlDataAdapter
        Protected WithEvents TimesheetListSelectCommand As System.Data.SqlClient.SqlCommand
        Protected WithEvents DS_TimesheetList As TimesheetDatasets.TimesheetListDataTable

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
            Dim configurationAppSettings As System.Configuration.AppSettingsReader = New System.Configuration.AppSettingsReader()
            Me.SqlConnection1 = New System.Data.SqlClient.SqlConnection()
            Me.DA_TimesheetList = New System.Data.SqlClient.SqlDataAdapter()
            Me.TimesheetListSelectCommand = New System.Data.SqlClient.SqlCommand()
            Me.DS_TimesheetList = New TimesheetDatasets.TimesheetListDataTable
            CType(Me.DS_TimesheetList, System.ComponentModel.ISupportInitialize).BeginInit()
            '
            'SqlConnection1
            '
            Me.SqlConnection1.ConnectionString = CType(configurationAppSettings.GetValue("MM_CONNECTION_STRING_WTSDB", GetType(System.String)), String)
            '
            'DA_TimesheetList
            '
            Me.DA_TimesheetList.SelectCommand = Me.TimesheetListSelectCommand
            Me.DA_TimesheetList.TableMappings.AddRange(New System.Data.Common.DataTableMapping() _
                    {New System.Data.Common.DataTableMapping("Table", "TimesheetList", New System.Data.Common.DataColumnMapping() { _
                    New System.Data.Common.DataColumnMapping("Year", "Year"), _
                    New System.Data.Common.DataColumnMapping("Week", "Week"), _
                    New System.Data.Common.DataColumnMapping("StartDate", "StartDate"), _
                    New System.Data.Common.DataColumnMapping("EndDate", "EndDate"), _
                    New System.Data.Common.DataColumnMapping("EmployeeID", "EmployeeID"), _
                    New System.Data.Common.DataColumnMapping("Status", "Status")})})
            '
            'TimesheetListSelectCommand
            '
            Me.TimesheetListSelectCommand.CommandText = "SELECT TOP 20 [YearWeek].[Year], [YearWeek].[Week], " & _
                                                        "Replace(convert(varchar(11),[YearWeek].[StartDate],13),' ','-') As [StartDate], " & _
                                                        "Replace(convert(varchar(11),[YearWeek].[EndDate],13),' ','-') As [EndDate], ISNULL([Status], 'No timesheet') As Status FROM YearWeek " & _
                                                        "FULL OUTER JOIN (SELECT Year, Week, StartDate, EndDate, Status FROM TimesheetList WHERE EmployeeID = @EmployeeID) As  TimesheetList " & _
                                                        "ON TimesheetList.Year = YearWeek.Year AND TimesheetList.Week = YearWeek.Week " & _
                                                        "WHERE [YearWeek].[EndDate] >= (SELECT StartDate FROM EmployeeInfo WHERE EmployeeID = @EmployeeID) " & _
                                                        "AND [YearWeek].[StartDate] <= (SELECT Actualdemobilisationdate FROM EmployeeInfo WHERE EmployeeID = @EmployeeID) " & _
                                                        "ORDER BY [Year] DESC, [Week] DESC"
            DA_TimesheetList.SelectCommand.Parameters.Add("@EmployeeID", SqlDbType.Int)
            'DA_TimesheetList.SelectCommand.Parameters.Add("@DemobilisationDate", SqlDbType.DateTime)
            Me.TimesheetListSelectCommand.Connection = Me.SqlConnection1
            '
            'DS_TimesheetList
            '
            Me.DS_TimesheetList.TableName = "TimesheetList"
            Me.DS_TimesheetList.Locale = New System.Globalization.CultureInfo("en-US")
            Me.DS_TimesheetList.Namespace = "http://www.tempuri.org/DS_TimesheetList.xsd"
            CType(Me.DS_TimesheetList, System.ComponentModel.ISupportInitialize).EndInit()

        End Sub

        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region


        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            Dim configurationAppSettings As System.Configuration.AppSettingsReader = New System.Configuration.AppSettingsReader()
            Session("errmsg") = ""
            Dim EmployeeInfo As EmployeeInfoStruct

            If Not User.Identity.IsAuthenticated Then
                Session("errmsg") = "ERR-MN-0100: User is not authenticated - verify IIS configuration"
                Response.Redirect("ErrMsg.aspx")
            End If

            If Not Me.IsPostBack Then
                If Request.Params("OnBehalf") <> Nothing Then
                    Me.BackButton.Visible = True
                    EmployeeInfo = Session("EmployeeInfo")
                    Session("OnBehalfUser") = EmployeeInfo
                    Dim CurrentEmployeeName As String
                    CurrentEmployeeName = Utils.GetEmployeeInfo(User.Identity.Name, CType(configurationAppSettings.GetValue("MM_CONNECTION_STRING_WTSDB", GetType(System.String)), String)).EmployeeName
                    Try
                        EmployeeInfo = Utils.GetEmployeeInfo(Request.Params("OnBehalf"), CType(configurationAppSettings.GetValue("MM_CONNECTION_STRING_WTSDB", GetType(System.String)), String))
                        toDisplayOnBehalf(EmployeeInfo)
                    Catch ex As Exception
                        Session("errmsg") = "ERR-MN-0101: Error retrieving employee information (" + ex.Message + ")"
                        Response.Redirect("ErrMsg.aspx")
                    End Try

                    lbl_welcome.Text = "Welcome " + CurrentEmployeeName + " (on behalf of " + EmployeeInfo.EmployeeName + ")"
                Else
                    Me.BackButton.Visible = False
                    Try
                        EmployeeInfo = Utils.GetEmployeeInfo(User.Identity.Name, CType(configurationAppSettings.GetValue("MM_CONNECTION_STRING_WTSDB", GetType(System.String)), String))
                        lbl_welcome.Text = "Welcome " + EmployeeInfo.EmployeeName
                        toDisplay(EmployeeInfo)
                    Catch ex As Exception
                        Session("errmsg") = "ERR-MN-0101: Error retrieving employee information (" + ex.Message + ")"
                        Response.Redirect("ErrMsg.aspx")
                    End Try
                End If

                Session("EmployeeInfo") = EmployeeInfo

            Else
                EmployeeInfo = CType(Session("EmployeeInfo"), EmployeeInfoStruct)
            End If

            ' add : verify that the user is authorised to access this page,
            ' i.e. is configured in the timesheet database

            If IsNothing(EmployeeInfo) Then
                ' user not found in the employee table
                Session("errmsg") = "ERR-MN-0200: User login is not configured in the emplotee table."
                Response.Redirect("ErrMsg.aspx")
            End If
        End Sub

        Private Sub toDisplay(ByVal EmployeeInfo As EmployeeInfoStruct)
            Try
                DA_TimesheetList.SelectCommand.Parameters("@EmployeeID").Value = EmployeeInfo.EmployeeID
                'DA_TimesheetList.SelectCommand.Parameters("@DemobilisationDate").Value = EmployeeInfo.DemobilisationDate
                DA_TimesheetList.Fill(DS_TimesheetList)
            Catch Exp As Exception
                Session("errmsg") = "ERR-MN-2001: " + Exp.Source.ToString + " - " + Exp.Message.ToString
                Response.Redirect("ErrMsg.aspx")
            End Try
            If DS_TimesheetList.Count > 0 Then
                DG_TimesheetList.DataBind()
            Else
                Session("errmsg") = "ERR-MN-2002: No record found!"
                Response.Redirect("ErrMsg.aspx")
            End If
        End Sub

        'LGX 15/09/10 : traitement différent pour on behalf: on ne sélectionne pas uniquement les 20 dernieres TS mais TOUTES les TS
        Private Sub toDisplayOnBehalf(ByVal EmployeeInfo As EmployeeInfoStruct)
            Try
                Me.TimesheetListSelectCommand.CommandText = "SELECT [YearWeek].[Year], [YearWeek].[Week], " & _
                                                        "Replace(convert(varchar(11),[YearWeek].[StartDate],13),' ','-') As [StartDate], " & _
                                                        "Replace(convert(varchar(11),[YearWeek].[EndDate],13),' ','-') As [EndDate], ISNULL([Status], 'No timesheet') As Status FROM YearWeek " & _
                                                        "FULL OUTER JOIN (SELECT Year, Week, StartDate, EndDate, Status FROM TimesheetList WHERE EmployeeID = @EmployeeID) As  TimesheetList " & _
                                                        "ON TimesheetList.Year = YearWeek.Year AND TimesheetList.Week = YearWeek.Week " & _
                                                        "WHERE [YearWeek].[EndDate] >= (SELECT StartDate FROM EmployeeInfo WHERE EmployeeID = @EmployeeID) " & _
                                                        "AND [YearWeek].[StartDate] <= (SELECT Actualdemobilisationdate FROM EmployeeInfo WHERE EmployeeID = @EmployeeID) " & _
                                                        "ORDER BY [Year] DESC, [Week] DESC"
                DA_TimesheetList.SelectCommand.Parameters("@EmployeeID").Value = EmployeeInfo.EmployeeID
                'DA_TimesheetList.SelectCommand.Parameters("@DemobilisationDate").Value = EmployeeInfo.DemobilisationDate
                DA_TimesheetList.Fill(DS_TimesheetList)
            Catch Exp As Exception
                Session("errmsg") = "ERR-MN-2001: " + Exp.Source.ToString + " - " + Exp.Message.ToString
                Response.Redirect("ErrMsg.aspx")
            End Try
            If DS_TimesheetList.Count > 0 Then
                DG_TimesheetList.DataBind()
            Else
                Session("errmsg") = "ERR-MN-2002: No record found!"
                Response.Redirect("ErrMsg.aspx")
            End If
        End Sub

        Sub DG_TimesheetList_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
            If e.Item.ItemType = ListItemType.Item Then
                e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='#ffcc01'")
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='#ffffff'")
            End If
            If e.Item.ItemType = ListItemType.AlternatingItem Then
                e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='#ffcc01'")
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='#ededf1'")
            End If
            'Gestion des couleurs pour les statuts
            If (e.Item.Cells(4).Text = "No timesheet" Or e.Item.Cells(4).Text = "Rejected") Then
                e.Item.Cells(4).ForeColor = Color.Red
            ElseIf (e.Item.Cells(4).Text = "Updated") Then
                e.Item.Cells(4).ForeColor = Color.Blue
            End If


        End Sub

        Private Sub DG_TimesheetList_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DG_TimesheetList.SelectedIndexChanged
            Dim Week As Integer
            Dim Year As Integer
            Dim Status As String

            Status = DG_TimesheetList.SelectedItem.Cells.Item(4).Text
            Year = DG_TimesheetList.SelectedItem.Cells.Item(5).Text
            Week = DG_TimesheetList.SelectedItem.Cells.Item(1).Text

            ' stack object for "back buttons"
            Dim NavigationObject As Stack
            If IsNothing(Session("NavigationObject")) Then
                NavigationObject = New Stack
            Else
                NavigationObject = Session("NavigationObject")
            End If
            Dim s As String
            s = Request.Url.PathAndQuery
            s = s.Substring(s.LastIndexOf("/") + 1)
            NavigationObject.Push(s)
            Session("NavigationObject") = NavigationObject
            If Status = "Updated" Or Status = "Rejected" Or Status = "No timesheet" Then
                Response.Redirect("ts_hp.aspx?Year=" + Year.ToString + "&Week=" + Week.ToString)
            Else
                Response.Redirect("ts_hp.aspx?Readonly=True&Year=" + Year.ToString + "&Week=" + Week.ToString)
            End If

        End Sub

        Private Sub BackButtonClick(ByVal Sender As Object, ByVal e As EventArgs) Handles BackButton.Click
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
