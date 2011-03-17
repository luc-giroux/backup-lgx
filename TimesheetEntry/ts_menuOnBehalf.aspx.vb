Imports Microsoft.Data.Odbc
Imports System.Data
Imports System.Data.SqlClient
Imports TPnet


Namespace TimeSheetV190_HTJV


    Partial Class ts_menuob
        Inherits System.Web.UI.Page
        Public qryScript, UID, formattedAuthUser, rs_EmpRecords
        Protected WithEvents SqlSelectCommand2 As System.Data.SqlClient.SqlCommand
        Protected WithEvents SqlInsertCommand2 As System.Data.SqlClient.SqlCommand
        Protected WithEvents SqlConnection1 As System.Data.SqlClient.SqlConnection
        Protected WithEvents SqlSelectCommand3 As System.Data.SqlClient.SqlCommand
        Protected WithEvents SqlInsertCommand3 As System.Data.SqlClient.SqlCommand
        Protected WithEvents SqlSelectCommand1 As System.Data.SqlClient.SqlCommand
        Protected WithEvents SqlInsertCommand1 As System.Data.SqlClient.SqlCommand
        Protected WithEvents DS_EmployeeList As TimesheetDatasets.EmployeeListDataTable
        Protected WithEvents DA_EmployeeList As System.Data.SqlClient.SqlDataAdapter
#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
            Dim configurationAppSettings As System.Configuration.AppSettingsReader = New System.Configuration.AppSettingsReader()          
            Me.SqlInsertCommand1 = New System.Data.SqlClient.SqlCommand()
            Me.SqlConnection1 = New System.Data.SqlClient.SqlConnection()
            Me.SqlSelectCommand1 = New System.Data.SqlClient.SqlCommand()        
            Me.SqlInsertCommand2 = New System.Data.SqlClient.SqlCommand()
            Me.SqlSelectCommand2 = New System.Data.SqlClient.SqlCommand()         
            Me.DA_EmployeeList = New System.Data.SqlClient.SqlDataAdapter()
            Me.SqlInsertCommand3 = New System.Data.SqlClient.SqlCommand()
            Me.SqlSelectCommand3 = New System.Data.SqlClient.SqlCommand()
            Me.DS_EmployeeList = New TimesheetDatasets.EmployeeListDataTable        
            CType(Me.DS_EmployeeList, System.ComponentModel.ISupportInitialize).BeginInit()           
            '
            'SqlConnection1
            '
            Me.SqlConnection1.ConnectionString = CType(configurationAppSettings.GetValue("MM_CONNECTION_STRING_WTSDB", GetType(System.String)), String)            
            '
            'DA_VwETEmpMas
            '
            Me.DA_EmployeeList.SelectCommand = Me.SqlSelectCommand3
            Me.DA_EmployeeList.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "EmployeeList", New System.Data.Common.DataColumnMapping() { _
                    New System.Data.Common.DataColumnMapping("EmployeeID", "EmployeeID"), _
                    New System.Data.Common.DataColumnMapping("CompanyID", "CompanyID"), _
                    New System.Data.Common.DataColumnMapping("EmployeeNumber", "EmployeeNumber"), _
                    New System.Data.Common.DataColumnMapping("Name", "Name"), _
                    New System.Data.Common.DataColumnMapping("DisciplineCode", "DisciplineCode"), _
                    New System.Data.Common.DataColumnMapping("Discipline", "Discipline"), _
                    New System.Data.Common.DataColumnMapping("EmployeeLogin", "EmployeeLogin"), _
                    New System.Data.Common.DataColumnMapping("LocationID", "LocationID")})})
            '
            'DS_VwETEmpMas
            '
            Me.DS_EmployeeList.TableName = "EmployeeList"
            CType(Me.DS_EmployeeList, System.ComponentModel.ISupportInitialize).EndInit()

        End Sub

        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

        Private Function GetQueryString(ByVal EmployeeInfo As EmployeeInfoStruct) As String
            Dim sqlcmd As SqlClient.SqlCommand
            sqlcmd = New SqlCommand("SELECT [Timesheet].[dbo].[GetOnBehalfFilter]('" & EmployeeInfo.EmployeeLogin & "')", Me.SqlConnection1)

            Try
                If Me.SqlConnection1.State <> ConnectionState.Open Then
                    Me.SqlConnection1.Open()
                End If
                GetQueryString = sqlcmd.ExecuteScalar
                If Not IsNothing(GetQueryString) Then
                    Return GetQueryString
                End If
            Catch ex As Exception
                Session("errmsg") = "ERR-MOB-1003: " + ex.Message.ToString
                Response.Redirect("ErrMsg.aspx")
            End Try
            GetQueryString = "1=2" ' to prevent getting an error on the query later, by default: no rights
        End Function
        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            Session("errmsg") = ""
            Dim EmployeeInfo As EmployeeInfoStruct

            If Not Page.IsPostBack Then
                Dim configurationAppSettings As System.Configuration.AppSettingsReader = New System.Configuration.AppSettingsReader()

                Try
                    EmployeeInfo = Utils.GetEmployeeInfo(User.Identity.Name, CType(configurationAppSettings.GetValue("MM_CONNECTION_STRING_WTSDB", GetType(System.String)), String))
                Catch ex As Exception
                    Session("errmsg") = "ERR-MN-0101: Error retrieving employee information (" + ex.Message + ")"
                    Response.Redirect("ErrMsg.aspx")
                End Try
                lbl_welcome.Text = "Welcome " + EmployeeInfo.EmployeeName
                Session("EmployeeInfo") = EmployeeInfo

                qryScript = GetQueryString(EmployeeInfo)
                Session("qryScript") = qryScript
                bindDataGrid(qryScript)
                Me.txt_search.Focus()
            End If
        End Sub

        Private Sub dg_namelistOB_PageIndexChanged(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dg_namelistOB.PageIndexChanged
            dg_namelistOB.CurrentPageIndex = e.NewPageIndex
            bindDataGrid(Session("qryScript"), txt_search.Text)
        End Sub

        Public Sub bindDataGrid(ByVal TSQryScript As String, Optional ByVal TSSearchValue As String = "")
            Dim sqlstmt As String

            sqlstmt = "SELECT	EmployeeID, CompanyID, EmployeeNumber, " & _
                                "RTRIM(Lastname) + ', ' + RTRIM(FirstName) As Name, " & _
                                "DisciplineCode, Discipline, LocationID, " & _
                                "EmployeeLogin, " & _
                                "(SELECT CASE COUNT(*) WHEN 0 THEN 'Green' ELSE 'Red' END " & _
                                "   FROM [YearWeek] " & _
                                "   WHERE [YearWeek].[StartDate] BETWEEN GetDate() - 20 AND GetDate() " & _
                                "   AND [YearWeek].[EndDate] <= [EmployeeInfo].ActualDemobilisationDate" & _
                                "   AND NOT EXISTS (SELECT 1 FROM [TimesheetHeader] " & _
                                "                   WHERE([TimesheetHeader].[EmployeeID] = [EmployeeInfo].[EmployeeID]) " & _
                                "                   AND [YearWeek].[Year] = [TimesheetHeader].[Year]  " & _
                                "                   AND [YearWeek].[Week] = [TimesheetHeader].[Week]  " & _
                                "                   AND ([TimesheetHeader].[Submitted] = 1 OR         " & _
                                "                     [TimesheetHeader].[Rejected] = 1 OR          " & _
                                "                     [TimesheetHeader].[Endorsed] = 1 OR          " & _
                                "                     [TimesheetHeader].[Approved] = 1))) As Color " & _
                                "FROM employeeinfo"
            If TSSearchValue <> "" Then
                sqlstmt = sqlstmt + " WHERE " & TSQryScript & " AND CompanyID in ('HAT','TPG','LIS') and (EmployeeNumber like '%" & TSSearchValue & "%' or RTRIM(Lastname) + ', ' + RTRIM(FirstName) like '%" & TSSearchValue & "%') ORDER BY Name"
            Else
                sqlstmt = sqlstmt + " WHERE " & TSQryScript & " AND CompanyID in ('HAT','TPG','LIS') order by Name"
            End If
            DA_EmployeeList.SelectCommand.CommandText = sqlstmt
            DA_EmployeeList.SelectCommand.Connection = Me.SqlConnection1
            Try
                DA_EmployeeList.Fill(DS_EmployeeList)
                Session("EmployeeList") = DS_EmployeeList
            Catch ex As SqlClient.SqlException
                Session("errmsg") = "ERR-MOB1-2001: " + ex.Number.ToString + " - " + ex.Message
                Response.Redirect("ErrMsg.aspx")
            Catch ex2 As Exception
                Session("errmsg") = "ERR-MOB1-2002: " + ex2.Source.ToString + " - " + ex2.Message
                Response.Redirect("ErrMsg.aspx")
            End Try
            If DS_EmployeeList.Count > 0 Then
                dg_namelistOB.DataBind()
            Else
                Session("errmsg") = "ERR-MOB1-2003: No Record Found!"
                Response.Redirect("ErrMsg.aspx")
            End If
        End Sub
        Private Sub z(ByVal sender As Object, ByVal e As DataGridItemEventArgs) Handles dg_namelistOB.ItemDataBound, dg_namelistOB.ItemCreated
            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
                If Not IsNothing(e.Item.DataItem) Then
                    Dim row As DataRowView
                    Try
                        row = CType(e.Item.DataItem, DataRowView)
                        If CType(row("Color"), String) = "Red" Then
                            e.Item.ForeColor = Drawing.Color.Red
                        End If
                    Catch ex As Exception

                    End Try
                End If
            End If
        End Sub

        Sub dg_namelistOB_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
            If e.Item.ItemType = ListItemType.Item Then
                e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='#ffcc01'")
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='#ffffff'")
            End If
            If e.Item.ItemType = ListItemType.AlternatingItem Then
                e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='#ffcc01'")
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='#ededf1'")
            End If
        End Sub

        Private Sub dg_namelistOB_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dg_namelistOB.SelectedIndexChanged
            Dim EmployeeNumber As String

            'empname = dg_namelistOB.SelectedItem.Cells.Item(2).Text
            DS_EmployeeList = Session("EmployeeList")

            ' the selected index is relative to the page number of the datagrid, so we need use the EmployeeNo column as a key
            EmployeeNumber = dg_namelistOB.SelectedItem.Cells.Item(1).Text

            Dim row As TimesheetDatasets.EmployeeListRow = CType(DS_EmployeeList.Select("EmployeeNumber = '" + EmployeeNumber + "'")(0), TimesheetDatasets.EmployeeListRow)
            If row.IsEmployeeLoginNull Then
                Session("errmsg") = "ERR-MOB-2009: Employee " + EmployeeNumber + " do not have a login configured in the PAF database!"
                Response.Redirect("ErrMsg.aspx")
            End If

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
            Response.Redirect("ts_menu.aspx?OnBehalf=" + row.EmployeeLogin)
        End Sub

        Private Sub btn_search_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_search.Click

            dg_namelistOB.CurrentPageIndex = 0
            bindDataGrid(Session("qryScript"), txt_search.Text)
        End Sub

        Private Sub btn_all_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_all.Click
            dg_namelistOB.CurrentPageIndex = 0
            txt_search.Text = ""
            bindDataGrid(Session("qryScript"))
        End Sub

    End Class

End Namespace
