<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TSToAdjust.aspx.cs" Inherits="TSToAdjust" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register TagPrefix="myTagPrefix" TagName="AppHeader" Src="PageHeader.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Timesheets To Adjust</title>
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
        Timesheets To Adjust (<asp:Label ID="LabelCountTS" runat="server" />) : 
        <div class="appcontent">
            
            <asp:GridView ID="GridView1" runat="server" class="gridview" AllowPaging="True" 
                AllowSorting="True" AutoGenerateColumns="False" PageSize="10000"
                DataKeyNames="TimesheetNumber" 
                EnableModelValidation="True" AlternatingRowStyle-BackColor="WhiteSmoke"
                onselectedindexchanged="GridView1_SelectedIndexChanged" 
                DataSourceID="SqlDataSourceTS">
<AlternatingRowStyle BackColor="WhiteSmoke"></AlternatingRowStyle>
                <Columns>
                    <asp:CommandField SelectText="View" ShowSelectButton="True" />
                    <asp:BoundField DataField="TimesheetNumber" HeaderText="Timesheet Number" ReadOnly="True" 
                        SortExpression="TimesheetNumber" />
                    <asp:BoundField DataField="TimesheetDate" HeaderText="Timesheet Date" 
                        SortExpression="TimesheetDate"/>
                    <asp:BoundField DataField="WSNumber" HeaderText="WS" ReadOnly="True" 
                        SortExpression="WSNumber" />
                    <asp:BoundField DataField="WPNumber" HeaderText="WP" ReadOnly="True" 
                        SortExpression="WPNumber" />
                    <asp:BoundField DataField="CWPNumber" HeaderText="CWP" ReadOnly="True" 
                        SortExpression="CWPNumber" />
                    <asp:BoundField DataField="VariationNumber" HeaderText="Variation" ReadOnly="True" 
                        SortExpression="VariationNumber" />
                    <asp:BoundField DataField="Discipline" HeaderText="Discipline" ReadOnly="True" 
                        SortExpression="Discipline" />
                    <asp:BoundField DataField="CancelledBy" HeaderText="Cancelled By" 
                        SortExpression="CancelledBy" />
                    <asp:BoundField DataField="CancelledDate" HeaderText="Cancelled Date" 
                        SortExpression="CancelledDate" />
                    <asp:BoundField DataField="CancellationRequestNo" HeaderText="Cancellation Request No" 
                        SortExpression="CancellationRequestNo" />
                    <asp:BoundField DataField="RejectedReason" HeaderText="Rejected Reason" 
                        SortExpression="RejectedReason" />
                </Columns>

            </asp:GridView>

            <asp:SqlDataSource ID="SqlDataSourceTS" runat="server" 
                ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
                ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" 
                SelectCommand="SELECT [TimesheetNumber], Replace(Convert(varchar(9),TimesheetDate,6),' ','-') as TimesheetDate, 
                               TSH.[WSNumber], TSH.[WPNumber], TSH.[CWPNumber],
                               CASE WHEN TSH.[VariationNumber] IS NULL AND  TSH.[WSNumber] IS NULL
		                            THEN 'OTHER' ELSE TSH.[VariationNumber] END AS VariationNumber,
                               CASE 
		                            WHEN CWP.Discipline like '%Mechanical%' OR V.Discipline like '%Mechanical%' THEN 'MECHANICAL'
		                            WHEN CWP.Discipline like '%CW%' OR V.Discipline like '%CW%' THEN 'CIVIL'
		                            WHEN CWP.Discipline like '%E&I%' OR V.Discipline like '%E&I%' THEN 'E&I'
		                            ELSE 'OTHERS' END AS Discipline, 
                               [CancelledBy], [CancelledDate], [CancellationRequestNo], [RejectedReason] 
                               FROM [TimesheetHeader] TSH
                               LEFT OUTER JOIN allianceTimesheets.dbo.CWP
	                                ON CWP.CWPNumber = TSH.CWPNumber
                               LEFT OUTER JOIN allianceTimesheets.dbo.Variation V
	                                ON V.VariationNumber = TSH.VariationNumber
                               WHERE (TSH.[ContractNumber] = ?) 
                               AND [PendingAdjustment] = 1 ORDER BY [CancelledDate]">
                <SelectParameters>
                    <asp:SessionParameter Name="ContractNumber" SessionField="currentContract" 
                        Type="String" />
                </SelectParameters>
            </asp:SqlDataSource>

            <asp:Label ID="LabelNoRecordFound" runat="server" Visible="false">
               No record found.
            </asp:Label>

        </div>

    </form>

</body>
</html>
