<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewTS.aspx.cs" Inherits="ViewTS" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register TagPrefix="myTagPrefix" TagName="AppHeader" Src="PageHeader.ascx" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>View TS</title>
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
    <link href="site.css" rel="stylesheet" type="text/css" />
    <link type="text/css" href="jquery.jnotify.css" rel="stylesheet" title="default" media="all" />
    <script type="text/javascript" src="scripts/jquery-1.4.2.min.js"></script>
    <script type="text/javascript" src="scripts/jquery.jnotify.min.js"></script>

</head>
<body>
    <form id="form1" runat="server" class="appform">     
        <%-- Header with user informations --%>
        <myTagPrefix:AppHeader ID="Header1" runat="server"/><br />

        <div class="appcontent" align="center">

            <asp:Panel ID="PanelApprove" runat="server" Visible="false">
                <fieldset>
                    <legend>[Approve / Reject] Timesheet <%= Request.Params["TSNumber"]%></legend>
                    <ol>
                    <li>
                        <asp:RadioButtonList ID="RadioButtonListApprove" runat="server"
                            RepeatDirection="Horizontal" AutoPostBack="true"
                            onselectedindexchanged="RadioButtonListApprove_SelectedIndexChanged">
                            <asp:ListItem Selected="True">Approve</asp:ListItem>
                            <asp:ListItem>Reject</asp:ListItem>
                        </asp:RadioButtonList>
                    </li>
                    <li>
                        <asp:Label ID="LabelReject" runat="server" Text="Label" Visible="false"><label for="Reject">Reject Reason<em>*</em></label></asp:Label>
                        <asp:TextBox ID="TextBoxRejectReason" runat="server" TextMode="MultiLine" Visible="false"></asp:TextBox>
                    </li>
                    <li>
                        <label for="blank"></label>
                        <asp:Button ID="ButtonApproveReject" runat="server" Text="Approve" 
                            onclick="ButtonApproveReject_Click" OnClientClick="return confirm('Are you sure?');" />
                    </li>
                    </ol>
                </fieldset>
            </asp:Panel>

            <asp:Button ID="ButtonEdiTS" runat="server" Text="Edit this TS" Visible="false" 
                onclick="ButtonEdiTS_Click"/>

            <asp:Button ID="ButtonAdjustTS" runat="server" Text="Adjust this TS" 
                Visible="false" onclick="ButtonAdjustTS_Click" />

            <asp:Panel ID="PanelCorrectiveTS" runat="server" Visible="false">
                <br />
                Timesheet <%= Request.Params["TSNumber"] %> has been replaced by the timesheet(s) below:  <br /> <br />                       
                <asp:GridView ID="GridViewCorrectiveTS" runat="server" class="gridview" 
                    AutoGenerateColumns="False" width="30%"
                    AlternatingRowStyle-BackColor="WhiteSmoke" DataKeyNames="TimesheetNumber" 
                    DataSourceID="DataSourceCorrectiveTS" EnableModelValidation="True" 
                    onselectedindexchanged="GridViewCorrectiveTS_SelectedIndexChanged" >
                    <Columns>
                        <asp:CommandField SelectText="View" ShowSelectButton="True" />
                        <asp:BoundField DataField="TimesheetNumber" HeaderText="TimesheetNumber" ReadOnly="True" 
                            SortExpression="TimesheetNumber" />
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="DataSourceCorrectiveTS" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
                    ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" 
                    
                    SelectCommand="SELECT [TimesheetNumber] FROM [TimesheetHeader] WHERE ([AdjustmentFromTS] = ?)">
                    <SelectParameters>
                        <asp:QueryStringParameter Name="AdjustmentFromTS" QueryStringField="TSNumber" Type="String" />
                </SelectParameters>
                </asp:SqlDataSource>
                <br />
            </asp:Panel>
            
            <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" 
                Font-Size="8pt" InteractiveDeviceInfos="(Collection)" 
                WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="820px" 
                Height="400px">
                <LocalReport ReportPath="TSDetail.rdlc">
                    <DataSources>
                        <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" Name="DataSet1" />
                    </DataSources>
                </LocalReport>
            </rsweb:ReportViewer>
            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
                SelectMethod="GetData" 
                TypeName="DataSetGlobalTableAdapters.TSDetailTableAdapter" 
                OldValuesParameterFormatString="original_{0}">
                <SelectParameters>
                    <asp:QueryStringParameter Name="TimesheetNumber" QueryStringField="TSNumber" Type="String" />
                    <asp:QueryStringParameter Name="TimesheetNum" QueryStringField="TSNumber" Type="String" />
                    <%-- 
                    <asp:SessionParameter Name="TimesheetNumber" SessionField="currentTS" Type="String" />
                    <asp:SessionParameter Name="TimesheetNum" SessionField="currentTS" Type="String" />
                    --%>
                </SelectParameters>
            </asp:ObjectDataSource>
            
        </div>

        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
    </form>

</body>
</html>
