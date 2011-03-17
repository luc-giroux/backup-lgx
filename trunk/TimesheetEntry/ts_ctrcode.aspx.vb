Namespace TimeSheetV190_HTJV

    Partial Class ts_ctrcode
        Inherits System.Web.UI.Page
        Protected WithEvents DA_CTRCode As System.Data.SqlClient.SqlDataAdapter
        Protected WithEvents DS_CTRCode As TimesheetDatasets.CTRCodeDataTable
        'Protected WithEvents DA_VwETTaskDesc As System.Data.SqlClient.SqlDataAdapter
        'Protected WithEvents DS_VwETTaskDesc As TimesheetDatasets.VwETTaskDescDataTable
        'Protected WithEvents DA_VwETCostCode As System.Data.SqlClient.SqlDataAdapter
        'Protected WithEvents DS_VwETCostCode As TimesheetDatasets.VwETCostCodeDataTable
        Protected WithEvents CTRCodeSelectCommand As System.Data.SqlClient.SqlCommand
        Protected WithEvents SqlConnection1 As System.Data.SqlClient.SqlConnection
        
#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
            Dim configurationAppSettings As System.Configuration.AppSettingsReader = New System.Configuration.AppSettingsReader()
            Me.DA_CTRCode = New System.Data.SqlClient.SqlDataAdapter()
            Me.CTRCodeSelectCommand = New System.Data.SqlClient.SqlCommand()
            Me.DS_CTRCode = New TimesheetDatasets.CTRCodeDataTable
            Me.SqlConnection1 = New System.Data.SqlClient.SqlConnection()
            CType(Me.DS_CTRCode, System.ComponentModel.ISupportInitialize).BeginInit()
            '
            'DA_vwETTsCtr
            '
            Me.DA_CTRCode.SelectCommand = Me.CTRCodeSelectCommand
            Me.DA_CTRCode.TableMappings.AddRange(New System.Data.Common.DataTableMapping() { _
                        New System.Data.Common.DataTableMapping("Table", "CTRCode", New System.Data.Common.DataColumnMapping() { _
                        New System.Data.Common.DataColumnMapping("CTRCode", "CTRCode"), _
                        New System.Data.Common.DataColumnMapping("Description", "Description")})})
            '
            'SqlConnection1
            '
            Me.SqlConnection1.ConnectionString = CType(configurationAppSettings.GetValue("MM_CONNECTION_STRING_WTSDB", GetType(System.String)), String)
            '
            'SqlSelectCommand1
            '
            Me.CTRCodeSelectCommand.CommandText = "SELECT CTRCode, Description FROM CTRCode " & _
                                                    "WHERE CostCode = @CostCode " & _
                                                    "AND Active = 1 " & _
                                                    "AND (StartDate IS NULL OR StartDate <= @CurrentDate) " & _
                                                    "AND (EndDate IS NULL OR EndDate >= @CurrentDate)"
            Me.CTRCodeSelectCommand.Parameters.Add("@CostCode", Data.SqlDbType.VarChar, 10)
            Me.CTRCodeSelectCommand.Parameters.Add("@CurrentDate", Data.SqlDbType.DateTime, 15)
            Me.CTRCodeSelectCommand.Connection = Me.SqlConnection1
            '
            'DS_vwETTsCtr
            '
            Me.DS_CTRCode.TableName = "CTRCode"
            Me.DS_CTRCode.Locale = New System.Globalization.CultureInfo("en-US")
            Me.DS_CTRCode.Namespace = "http://www.tempuri.org/DS_CTRCode.xsd"
            CType(Me.DS_CTRCode, System.ComponentModel.ISupportInitialize).EndInit()

        End Sub

        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            If Not String.IsNullOrEmpty(Request.QueryString("numrows")) Then
                Control.Value = Trim(Request.QueryString("numrows").ToString())
            Else
                Control.Value = "15"
            End If

            errMsg.Text = ""
            errMsg.Visible = False
            dgVwTSCTR.Visible = True
            If Not Page.IsPostBack Then
                BindData()
            End If

        End Sub

        Private Function GetStartDate(ByVal Year As Integer, ByVal Week As Integer) As DateTime
            Dim sqlQuery As Data.SqlClient.SqlCommand
            sqlQuery = New Data.SqlClient.SqlCommand("SELECT StartDate FROM YearWeek WHERE YEAR = @Year AND Week = @Week", Me.SqlConnection1)
            sqlQuery.Parameters.Add("@Year", Data.SqlDbType.Int)
            sqlQuery.Parameters("@Year").Value = Year
            sqlQuery.Parameters.Add("@Week", Data.SqlDbType.Int)
            sqlQuery.Parameters("@Week").Value = Week
            If sqlQuery.Connection.State <> Data.ConnectionState.Open Then
                sqlQuery.Connection.Open()
            End If
            GetStartDate = CType(sqlQuery.ExecuteScalar(), DateTime)
        End Function
        Sub BindData()
            Dim CostCode As String
            Dim Year As Int16
            Dim Week As Integer

            CostCode = Trim(Request.Params("costcode"))
            Year = CType(Request.Params("Year"), Integer)
            Week = CType(Request.Params("Week"), Integer)

            DA_CTRCode.SelectCommand.Parameters("@CostCode").Value = CostCode
            DA_CTRCode.SelectCommand.Parameters("@CurrentDate").Value = GetStartDate(Year, week)

            If Trim(txtSearch.Text) <> "" Then
                DA_CTRCode.SelectCommand.CommandText += " AND (CTRCode like '%" + Trim(txtSearch.Text) + "%' or Description like '%" + Trim(txtSearch.Text) + "%') "
            End If

            Try
                errMsg.Text = ""
                errMsg.Visible = False
                dgVwTSCTR.Visible = True
                DA_CTRCode.Fill(DS_CTRCode)
                dgVwTSCTR.DataBind()
            Catch ex As Data.SqlClient.SqlException
                errMsg.Text = "ERR-CTR-1001: " + ex.Source + " - " + ex.Message
            Catch ex2 As Exception
                errMsg.Text = "ERR-CTR-1002: " + ex2.Source + " - " + ex2.Message
            End Try
            If DS_CTRCode.Count < 1 Then
                errMsg.Visible = True
                dgVwTSCTR.Visible = False
                errMsg.Text = "Activity / CTR Code Not Found!<br>The CTR code maybe not yet available,<br> Please contact IT Servicedesk or Finance Department for CTR code enquiry."
            Else
                lblPageCount.Text = "Page " & dgVwTSCTR.CurrentPageIndex + 1 & " of " & dgVwTSCTR.PageCount
            End If
        End Sub

        Private Sub cmdGo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdGo.Click
            dgVwTSCTR.Visible = True
            errMsg.Text = ""
            errMsg.Visible = False
            dgVwTSCTR.CurrentPageIndex = 0
            BindData()
        End Sub

        Private Sub cmdShowAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdShowAll.Click
            txtSearch.Text = ""
            dgVwTSCTR.CurrentPageIndex = 0
            BindData()
        End Sub

        Sub dgVwTSCTR_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
                e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='#e6e6fa'")
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='#ffffff'")
            End If
        End Sub

        Private Sub dgVwTSCTR_PageIndexChanged(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgVwTSCTR.PageIndexChanged
            dgVwTSCTR.CurrentPageIndex = e.NewPageIndex
            BindData()
        End Sub

        Protected Sub Change_Causes(ByVal sender As System.Object, ByVal e As System.EventArgs)
            Dim strScript As String
            
            strScript = "<script language=JavaScript>window.opener.document.forms(0).ctrc" + Control.Value + ".value = '"
            strScript += UCase(Trim(dgVwTSCTR.Items(dgVwTSCTR.SelectedIndex).Cells(0).Text)) + "';"

            strScript += "window.opener.document.forms(0).ccdesc" + Control.Value + ".value = '"
            strScript += UCase(Trim(dgVwTSCTR.Items(dgVwTSCTR.SelectedIndex).Cells(1).Text)) + "';"

            strScript += "window.opener.document.forms(0).ccdesc" + Control.Value + ".focus();"
            strScript += "window.opener.document.forms(0).ccdesc" + Control.Value + ".select();"
            strScript += "window.opener.changevalue();window.close();"
            strScript += "</" + "script>"
            ClientScript.RegisterStartupScript(GetType(String), "anything", strScript)
        End Sub

    End Class
End Namespace
