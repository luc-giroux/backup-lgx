Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.SqlTypes
Imports System.Convert


Namespace TimeSheetV190_HTJV


Partial Class ts_costcode
    Inherits System.Web.UI.Page
        Protected WithEvents DA_CostCode As System.Data.SqlClient.SqlDataAdapter
        Protected WithEvents DS_CostCode As TimesheetDatasets.CostCodeDataTable
        Protected WithEvents SqlSelectCommand3 As System.Data.SqlClient.SqlCommand
    Protected WithEvents SqlConnection1 As System.Data.SqlClient.SqlConnection
    Protected WithEvents SqlSelectCommand2 As System.Data.SqlClient.SqlCommand
    Protected WithEvents SqlSelectCommand1 As System.Data.SqlClient.SqlCommand


#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim configurationAppSettings As System.Configuration.AppSettingsReader = New System.Configuration.AppSettingsReader()
            Me.DA_CostCode = New System.Data.SqlClient.SqlDataAdapter()
        Me.SqlConnection1 = New System.Data.SqlClient.SqlConnection()
        Me.SqlSelectCommand2 = New System.Data.SqlClient.SqlCommand()
            Me.DS_CostCode = New TimesheetDatasets.CostCodeDataTable
        Me.SqlSelectCommand3 = New System.Data.SqlClient.SqlCommand()
        Me.SqlSelectCommand1 = New System.Data.SqlClient.SqlCommand()
            CType(Me.DS_CostCode, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'DA_VwETCostCode
        '
            Me.DA_CostCode.SelectCommand = Me.SqlSelectCommand2
            Me.DA_CostCode.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "VwETCostCode", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("CountCTR", "CountCTR"), New System.Data.Common.DataColumnMapping("CostCode", "CostCode"), New System.Data.Common.DataColumnMapping("ProjectCode", "ProjectCode"), New System.Data.Common.DataColumnMapping("Description", "Description"), New System.Data.Common.DataColumnMapping("ProjectManager", "ProjectManager"), New System.Data.Common.DataColumnMapping("CostController", "CostController"), New System.Data.Common.DataColumnMapping("Accountant", "Accountant"), New System.Data.Common.DataColumnMapping("ProjectStartDate", "ProjectStartDate"), New System.Data.Common.DataColumnMapping("ProjectEndDate", "ProjectEndDate"), New System.Data.Common.DataColumnMapping("ExtendedStartDate", "ExtendedStartDate"), New System.Data.Common.DataColumnMapping("ExtendedEndDate", "ExtendedEndDate"), New System.Data.Common.DataColumnMapping("Client", "Client"), New System.Data.Common.DataColumnMapping("ProjectType", "ProjectType"), New System.Data.Common.DataColumnMapping("ProjectScope", "ProjectScope"), New System.Data.Common.DataColumnMapping("ProjectCat", "ProjectCat"), New System.Data.Common.DataColumnMapping("ProjCountry", "ProjCountry"), New System.Data.Common.DataColumnMapping("IndustrialSector", "IndustrialSector"), New System.Data.Common.DataColumnMapping("BudgetCode", "BudgetCode"), New System.Data.Common.DataColumnMapping("UpDownStream", "UpDownStream"), New System.Data.Common.DataColumnMapping("Cum1997", "Cum1997"), New System.Data.Common.DataColumnMapping("Cum97CI", "Cum97CI"), New System.Data.Common.DataColumnMapping("Cum97Csi", "Cum97Csi"), New System.Data.Common.DataColumnMapping("Cum97K1", "Cum97K1"), New System.Data.Common.DataColumnMapping("Cum97K2", "Cum97K2"), New System.Data.Common.DataColumnMapping("Cum97Mhr", "Cum97Mhr"), New System.Data.Common.DataColumnMapping("budgetDesc", "budgetDesc"), New System.Data.Common.DataColumnMapping("code", "code"), New System.Data.Common.DataColumnMapping("CostType", "CostType")})})
            '
        'SqlConnection1
        '
        Me.SqlConnection1.ConnectionString = CType(configurationAppSettings.GetValue("MM_CONNECTION_STRING_WTSDB", GetType(System.String)), String)
        '
        'SqlSelectCommand2
        '
            Me.SqlSelectCommand2.Connection = Me.SqlConnection1
        '
        'DS_VwETCostCode
        '
            Me.DS_CostCode.TableName = "CostCode"
            Me.DS_CostCode.Locale = New System.Globalization.CultureInfo("en-US")
            Me.DS_CostCode.Namespace = "http://www.tempuri.org/DS_CostCode.xsd"
            CType(Me.DS_CostCode, System.ComponentModel.ISupportInitialize).EndInit()

        End Sub

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Control.Value = Trim(Request.QueryString("numrows").ToString())
            'Session("startdate") = Trim(Request.QueryString("startdate").ToString())
        errMsg.Text = ""
        errMsg.Visible = False
        dgVwTSCost.Visible = True
        If Not Page.IsPostBack Then
            BindData()
        End If
    End Sub

    Sub BindData()
            Dim selectstatement As String
            Dim costCodeFilter As String

            'IFN 01-2011
            If Request.Params("company") <> "LIS" Then
                costCodeFilter = "AND CostCode <> '5800'"
            Else
                costCodeFilter = "AND CostCode = '5800'"
            End If
            If Trim(txtSearch.Text) = "" Then
                selectstatement = "SELECT CostCode, Description FROM CostCode WHERE Active = 1 " + costCodeFilter + " ORDER BY CostCode"
            Else
                selectstatement = "SELECT CostCode, Description FROM CostCode WHERE Active = 1 " + costCodeFilter + " AND (CostCode like '%" + Trim(txtSearch.Text) + "%' or Description like '%" + Trim(txtSearch.Text) + "%') ORDER BY CostCode"
            End If
            Try
                errMsg.Text = ""
                errMsg.Visible = False
                dgVwTSCost.Visible = True
                DA_CostCode.SelectCommand.CommandText = selectstatement
                DA_CostCode.Fill(DS_CostCode)
                dgVwTSCost.DataBind()
            Catch ex As SqlClient.SqlException
                errMsg.Text = "ERR-CC-1001: " + ex.Source + " - " + ex.Message
            Catch ex2 As Exception
                errMsg.Text = "ERR-CC-1002: " + ex2.Source + " - " + ex2.Message
            End Try
            If DS_CostCode.Count < 1 Then
                errMsg.Visible = True
                dgVwTSCost.Visible = False
                errMsg.Text = "Cost Code / Description Not Found!<br>The cost code maybe not yet available,<br> Please contact IT Servicedesk or Finance Department for cost code enquiry."
            End If
        End Sub

    Sub dgVwTSCost_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='#e6e6fa'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='#ffffff'")
        End If
    End Sub

    Private Sub dgVwTSCost_PageIndexChanged(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgVwTSCost.PageIndexChanged
        dgVwTSCost.CurrentPageIndex = e.NewPageIndex
        BindData()
    End Sub

        Protected Sub Change_Causes(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dgVwTSCost.SelectedIndexChanged
            Dim startdate = Trim(Session("startdate"))
            Dim strScript, ctrcheckstatement As String
            'dim ctrCount As String
            'ctrcheckstatement = "SELECT * FROM dbo.vwETTsCtr " _
            ' & "WHERE TSCOST = '" + Trim(dgVwTSCost.Items(dgVwTSCost.SelectedIndex).Cells(0).Text) + "' " _
            ' & "and ((ctrStartDate <= dateadd(d,6,'" + startdate + "') or ctrStartDate is null) " _
            ' & "and (ctrEndDate is null or ctrEndDate >= dateadd(d,6,'" + startdate + "')))"
            strScript = "<script language=JavaScript>window.opener.document.forms(0).ccode" + Control.Value + ".value = '"
            strScript += Trim(dgVwTSCost.Items(dgVwTSCost.SelectedIndex).Cells(0).Text) + "';"
            strScript += "window.opener.document.forms(0).ccdesc" + Control.Value + ".value = '"
            If UCase(Trim(dgVwTSCost.Items(dgVwTSCost.SelectedIndex).Cells(1).Text)) <> "1" Then
                strScript += UCase(Trim(dgVwTSCost.Items(dgVwTSCost.SelectedIndex).Cells(1).Text)) + "';"
            Else
                strScript += "';"
            End If
            strScript += "window.opener.document.forms(0).ccdesc" + Control.Value + ".focus();"
            strScript += "window.opener.document.forms(0).ccdesc" + Control.Value + ".select();"
            strScript += "window.opener.document.forms(0).ctrc" + Control.Value + ".value = '';"
            strScript += "window.opener.changevalue();window.close();"
            strScript += "</" + "script>"
            ClientScript.RegisterStartupScript(GetType(String), "anything", strScript)
        End Sub

    Private Sub cmdGo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdGo.Click
        dgVwTSCost.Visible = True
        errMsg.Text = ""
        errMsg.Visible = False
        dgVwTSCost.CurrentPageIndex = 0
        BindData()
    End Sub

    End Class

End Namespace
