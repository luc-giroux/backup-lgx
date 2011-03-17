<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReportProgressCWP.aspx.cs" Inherits="ReportProgressCWP" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register TagPrefix="myTagPrefix" TagName="AppHeader" Src="../../PageHeader.ascx" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>CWP Progress Report</title>
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
    <link href="../../site.css" rel="stylesheet" type="text/css" />
    <link type="text/css" href="../../jquery.jnotify.css" rel="stylesheet" title="default" media="all" />
    <script type="text/javascript" src="../../scripts/jquery-1.4.2.min.js"></script>
    <script type="text/javascript" src="../../scripts/jquery.jnotify.min.js"></script>
    <script type="text/javascript" src="../../scripts/Report.js"></script>

</head>
<body>
    <form id="form1" runat="server" class="appform">     
        <%-- Header with user informations --%>
        <myTagPrefix:AppHeader ID="Header1" runat="server"/><br />


        <div class="appcontent" align="center">

            <fieldset>
                <legend>Please select your parameters : </legend>

                <label for="StartDate">From Date<em>*</em> : </label>
                <asp:TextBox ID="TextBoxStartDate" runat="server"></asp:TextBox>
                <asp:CalendarExtender  ID="CalendarExtender1" runat="server" targetControlID="TextBoxStartDate" Format="dd-MMM-yy">
                </asp:CalendarExtender>

                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="ButtonViewReport" runat="server" Text="View Report" 
                    onclick="ButtonViewReport_Click" />
                <br /><br />
            </fieldset>


           
           <br />
           <asp:Panel ID="PanelReport" runat="server" Width="100%" Height="300px">
            <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" 
                Font-Size="8pt" InteractiveDeviceInfos="(Collection)" 
                WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="100%" 
                Height="600px">
                <LocalReport ReportPath="Reports/Progress/ReportProgressCWP.rdlc">
                    <DataSources>
                        <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" Name="DataSet1" />
                    </DataSources>
                </LocalReport>
            </rsweb:ReportViewer>
            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
                SelectMethod="GetData" 
                TypeName="DataSetReportTableAdapters.ReportProgressCWPTableAdapter" 
                OldValuesParameterFormatString="original_{0}">

            </asp:ObjectDataSource>
            </asp:Panel>
            
        </div>

        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ToolkitScriptManager>
    </form>

</body>
</html>
