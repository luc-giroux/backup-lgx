<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PageHeader.ascx.cs" Inherits="PageHeader" %>


<div class="info" align="center">
    <table>
        <tr>
            <td>Connected as <asp:Label ID="lblUser" runat="server" Visible="false"/> <asp:Label ID="LabelRole" runat="server" />&nbsp;&nbsp;|</td>
            <td>&nbsp;&nbsp;You are currently working on contract <b><asp:Label ID="LblContract" runat="server" /></b>&nbsp;&nbsp;|</td>
            <td>&nbsp;&nbsp;<asp:HyperLink ID="HyperLinkLogout" runat="server" NavigateUrl="~/SessionExpired.aspx" Font-Underline="true">Logout</asp:HyperLink>&nbsp;&nbsp;<asp:HyperLink ID="HyperLinkChangeContract" runat="server" Visible="false" Font-Underline="true">Change Contract</asp:HyperLink>&nbsp;&nbsp;|</td>
            <td align="right">&nbsp;&nbsp;<u><a href="mailto:AllianceTS-Admin@projetkoniambo.com?subject=Request from Alliance Timesheet Application&body=Please note that you are contacting the administrator of the application, not the contract administrator.%0D%0AIf your question is about a timesheet to approve/cancel/reject, please contact your Contract Administrator.%0D%0AOtherwise, Please write your request below:%0D%0A%0D%0A">Contact Administrator</a></u><asp:Image ID="Image1" runat="server" ImageUrl="~/img/email.png" ImageAlign="Top"/></td>
        </tr>
    </table>
</div>

<div align="left">

<asp:Menu ID="MenuApplicationAdmin" runat="server" Orientation="Horizontal"  CssClass="AspMenu"
    BackColor="#cae1ef" ForeColor="#075593" DynamicHorizontalOffset="2" Font-Names="Verdana" 
    Font-Size="1em"  StaticSubMenuIndent="10px" Visible="False">
    <DynamicHoverStyle BackColor="#ffcc01" ForeColor="White" />
    <DynamicMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
    <DynamicMenuStyle BackColor="#B5C7DE" />
    <DynamicSelectedStyle BackColor="#ffcc01" />
    <Items>
        <asp:MenuItem Text="Companies" Value="Companies" NavigateUrl="~/Company/List.aspx"></asp:MenuItem>
        <asp:MenuItem Text="Contracts" Value="Contracts" NavigateUrl="~/Contract/List.aspx"></asp:MenuItem>
        <asp:MenuItem Text="View Application Users" Value="Users" NavigateUrl="~/AppUsers.aspx"></asp:MenuItem>
        <asp:MenuItem NavigateUrl="~/AppUser/Insert.aspx" Text="Add New User" Value="Add new User" />
    </Items>
    <StaticHoverStyle BackColor="#ffcc01" ForeColor="White" />
    <StaticMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
    <StaticSelectedStyle BackColor="#507CD1" />
</asp:Menu>

<asp:Menu ID="MenuContractorSupervisor" runat="server" Orientation="Horizontal" 
    CssClass="AspMenu" DynamicHorizontalOffset="2" Font-Names="Verdana" 
    Font-Size="1em"  StaticSubMenuIndent="10px" Visible="False">
    <DynamicHoverStyle BackColor="#ffcc01" ForeColor="White" />
    <DynamicMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
    <DynamicMenuStyle BackColor="#B5C7DE" />
    <DynamicSelectedStyle BackColor="#ffcc01" />
    <Items>
        <asp:MenuItem Text="Submit a Timesheet" Value="Submit a Timesheet" 
            NavigateUrl="~/TimesheetSubmit.aspx"></asp:MenuItem>
        <asp:MenuItem Text="Rejected Timesheets" Value="Rejected Timesheets" 
            NavigateUrl="~/TSRejected.aspx"></asp:MenuItem>
        <asp:MenuItem Text="Approved Timesheets" Value="Approved Timesheets" 
            NavigateUrl="~/TSApproved.aspx"></asp:MenuItem>
        <asp:MenuItem Text="Pending Approval Timesheets" Value="Pending Approval Timesheets" 
            NavigateUrl="~/TSToApprove.aspx"></asp:MenuItem>
        <asp:MenuItem Text="Pending Adjustment Timesheets" Value="Pending Adjustment Timesheets" NavigateUrl="~/TSToAdjust.aspx" />
    </Items>
    <StaticHoverStyle BackColor="#ffcc01" ForeColor="White" />
    <StaticMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
    <StaticSelectedStyle BackColor="#507CD1" />
</asp:Menu>

<asp:Menu ID="MenuOwnerSupervisor" runat="server" Orientation="Horizontal" 
    CssClass="AspMenu" DynamicHorizontalOffset="2" Font-Names="Verdana" 
    Font-Size="1em"  StaticSubMenuIndent="10px" Visible="False">
    <DynamicHoverStyle BackColor="#ffcc01" ForeColor="White" />
    <DynamicMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
    <DynamicMenuStyle BackColor="#B5C7DE" />
    <DynamicSelectedStyle BackColor="#ffcc01" />
    <Items>
        <asp:MenuItem NavigateUrl="~/TSToApprove.aspx" Text="Timesheets To Approve" Value="Approve Timesheets" />
        <asp:MenuItem NavigateUrl="~/TSRejected.aspx" Text="Rejected Timesheets" Value="Rejected Timesheets" />
        <asp:MenuItem NavigateUrl="~/TSApproved.aspx" Text="Approved Timesheets" Value="Approved Timesheets" />
    </Items>
    <StaticHoverStyle BackColor="#ffcc01" ForeColor="White" />
    <StaticMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
    <StaticSelectedStyle BackColor="#507CD1" />
</asp:Menu>


<asp:Menu ID="MenuOwnerCA" runat="server" Orientation="Horizontal" 
    CssClass="AspMenu" DynamicHorizontalOffset="2" Font-Names="Verdana" 
    Font-Size="1em"  StaticSubMenuIndent="10px" Visible="False">
    <DynamicHoverStyle BackColor="#ffcc01" ForeColor="White" />
    <DynamicMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
    <DynamicMenuStyle BackColor="#B5C7DE" />
    <DynamicSelectedStyle BackColor="#ffcc01" />
    <Items>
        <asp:MenuItem  Text="Supervisors" Value="Supervisors" Selectable="False" >
            <asp:MenuItem NavigateUrl="~/AppUsers.aspx?role=OwnerSupervisor" Text="Owner Supervisors" Value="Owner Supervisors" />
            <asp:MenuItem NavigateUrl="~/AppUsers.aspx?role=ContractorSupervisor" Text="Contractor Supervisors" Value="Contractor Supervisors" />
            <asp:MenuItem NavigateUrl="~/AppUsers.aspx" Text="All Users" Value="Application Users" />
            <asp:MenuItem NavigateUrl="~/AppUser/Insert.aspx" Text="Add New User" Value="Add new User" />
        </asp:MenuItem>
        <asp:MenuItem Selectable="false" Text="Contract Structure" Value="Contract Structure" >
            <asp:MenuItem NavigateUrl="~/ContractStructure.aspx" Text="See Contract Structure" Value="See Contract Structure" />
            <asp:MenuItem NavigateUrl="~/EditContractStructure.aspx" Text="Edit Contract Structure" Value="Edit Contract Structure" />
            <asp:MenuItem NavigateUrl="~/WS.aspx" Text="WS" Value="WS" />
            <asp:MenuItem NavigateUrl="~/WP.aspx" Text="WP" Value="WP" />
            <asp:MenuItem NavigateUrl="~/CWP.aspx" Text="CWP" Value="CWP" />
            <asp:MenuItem NavigateUrl="~/Variations.aspx" Text="Variations" Value="Variations" />
        </asp:MenuItem>
        <asp:MenuItem Selectable="false" Text="Subcontractors" Value="Subcontractors" >
            <asp:MenuItem NavigateUrl="~/Subcontractors.aspx" Text="Subcontractors List" Value="Subcontractors List"/>
            <asp:MenuItem NavigateUrl="~/AddSubcontractor.aspx" Text="Add Subcontractors" Value="Add Subcontractors"/>
        </asp:MenuItem>
        <asp:MenuItem Text="Workers" Value="Workers" Selectable="false" >
            <asp:MenuItem NavigateUrl="~/Workers.aspx" Text="Worker List" Value="Worker List" />
            <asp:MenuItem NavigateUrl="~/Trades.aspx" Text="Trades" Value="Trades" />
            <asp:MenuItem NavigateUrl="~/PositionCategories.aspx" Text="Position Categories" Value="Position Categories" />
        </asp:MenuItem>
        <asp:MenuItem Text="Timesheets" Value="Timesheets" Selectable="false" >
            <asp:MenuItem NavigateUrl="~/cancelTS.aspx" Text="Approved" Value="Approved" />
            <asp:MenuItem NavigateUrl="~/TSRejected.aspx" Text="Rejected" Value="Rejected" />
            <asp:MenuItem NavigateUrl="~/TSToApprove.aspx" Text="Pending Approval" Value="Pending Approval" />
            <asp:MenuItem NavigateUrl="~/TSToadjust.aspx" Text="Pending Adjustment" Value="Pending Adjustment" />
        </asp:MenuItem>
        <asp:MenuItem Text="Reports" Value="Reports" Selectable="false">
            <asp:MenuItem Text="Contract" Value="Contract" Selectable="false">
                <asp:MenuItem NavigateUrl="~/Reports/Contract/ReportContractProfileHistory.aspx" Text="Contract Profile History" Value="Contract Profile History" />
                <asp:MenuItem NavigateUrl="~/Reports/Contract/ReportCWP.aspx" Text="Contract Base Scope" Value="Contract Base Scope" />
                <asp:MenuItem NavigateUrl="~/Reports/Contract/ReportVariation.aspx" Text="Contract Extra Works" Value="Contract Extra Works" />
                <asp:MenuItem NavigateUrl="~/Reports/Contract/ReportAgreedSuspension.aspx" Text="Agreed Suspension" Value="Agreed Suspension" />
                <%--
                <asp:MenuItem Text="Invoices" Value="Invoices" Selectable="false">
                </asp:MenuItem>
                --%>
            </asp:MenuItem>
            <asp:MenuItem Text="Progress" Value="Progress" Selectable="false">
                <asp:MenuItem NavigateUrl="~/Reports/Progress/ReportProgressCwp.aspx" Text="Weekly Base Scope" Value="Weekly Base Scope" />
                <asp:MenuItem NavigateUrl="~/Reports/Progress/ReportProgressVariation.aspx" Text="Weekly Extra work" Value="Weekly Extra work" />
                <%--
                <asp:MenuItem Text="Man Hour by" Value="Man Hour by" Selectable="false">
                    <asp:MenuItem NavigateUrl="~/Reports/Progress/ReportManHourByTrade.aspx" Text="Trade" Value="Trade" />
                    <asp:MenuItem NavigateUrl="~/Reports/Progress/ReportManHourByPositionCategory.aspx" Text="Position Category" Value="Position Category" />
                </asp:MenuItem>
                --%>
            </asp:MenuItem>         
        </asp:MenuItem>
    </Items>
    <StaticHoverStyle BackColor="#ffcc01" ForeColor="White" />
    <StaticMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
    <StaticSelectedStyle BackColor="#507CD1" />
</asp:Menu>


<asp:Menu ID="MenuContractorCA" runat="server" Orientation="Horizontal" 
    CssClass="AspMenu" DynamicHorizontalOffset="2" Font-Names="Verdana" 
    Font-Size="1em"  StaticSubMenuIndent="10px" Visible="False">
    <DynamicHoverStyle BackColor="#ffcc01" ForeColor="White" />
    <DynamicMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
    <DynamicMenuStyle BackColor="#B5C7DE" />
    <DynamicSelectedStyle BackColor="#ffcc01" />
    <Items>
        <asp:MenuItem  Text="Supervisors" Value="Supervisors" Selectable="False" >
            <asp:MenuItem NavigateUrl="~/AppUsers.aspx?role=OwnerSupervisor" Text="Owner Supervisors" Value="Owner Supervisors" />
            <asp:MenuItem NavigateUrl="~/AppUsers.aspx?role=ContractorSupervisor" Text="Contractor Supervisors" Value="Contractor Supervisors" />
            <asp:MenuItem NavigateUrl="~/AppUsers.aspx" Text="All Users" Value="Application Users" />
        </asp:MenuItem>
        <asp:MenuItem Selectable="false" Text="Contract Structure" Value="Contract Structure" >
            <asp:MenuItem NavigateUrl="~/ContractStructure.aspx" Text="See Contract Structure" Value="See Contract Structure" />
            <asp:MenuItem NavigateUrl="~/WS.aspx" Text="WS" Value="WS" />
            <asp:MenuItem NavigateUrl="~/WP.aspx" Text="WP" Value="WP" />
            <asp:MenuItem NavigateUrl="~/CWP.aspx" Text="CWP" Value="CWP" />
            <asp:MenuItem NavigateUrl="~/Variations.aspx" Text="Variations" Value="Variations" />
        </asp:MenuItem>
        <asp:MenuItem Selectable="false" Text="Subcontractors" Value="Subcontractors" >
            <asp:MenuItem NavigateUrl="~/Subcontractors.aspx" Text="Subcontractors List" Value="Subcontractors List"/>
        </asp:MenuItem>
        <asp:MenuItem Text="Workers" Value="Workers" Selectable="false" >
            <asp:MenuItem NavigateUrl="~/Workers.aspx" Text="Worker List" Value="Worker List" />
            <asp:MenuItem NavigateUrl="~/Trades.aspx" Text="Trades" Value="Trades" />
            <asp:MenuItem NavigateUrl="~/PositionCategories.aspx" Text="Position Categories" Value="Position Categories" />
        </asp:MenuItem>
        <asp:MenuItem Text="Timesheets" Value="Timesheets" Selectable="false" >
            <asp:MenuItem NavigateUrl="~/TSApproved.aspx" Text="Approved" Value="Approved" />
            <asp:MenuItem NavigateUrl="~/TSRejected.aspx" Text="Rejected" Value="Rejected" />
            <asp:MenuItem NavigateUrl="~/TSToApprove.aspx" Text="Pending Approval" Value="Pending Approval" />
        </asp:MenuItem>
        <asp:MenuItem Text="Reports" Value="Reports" Selectable="false">
            <asp:MenuItem Text="Contract" Value="Contract" Selectable="false">
                <asp:MenuItem NavigateUrl="~/Reports/Contract/ReportContractProfileHistory.aspx" Text="Contract Profile History" Value="Contract Profile History" />
                <asp:MenuItem NavigateUrl="~/Reports/Contract/ReportCWP.aspx" Text="Contract Base Scope" Value="Contract Base Scope" />
                <asp:MenuItem NavigateUrl="~/Reports/Contract/ReportVariation.aspx" Text="Contract Extra Works" Value="Contract Extra Works" />
                <asp:MenuItem NavigateUrl="~/Reports/Contract/ReportAgreedSuspension.aspx" Text="Agreed Suspension" Value="Agreed Suspension" />
                <%--
                <asp:MenuItem Text="Invoices" Value="Invoices" Selectable="false">
                </asp:MenuItem>
                --%>
            </asp:MenuItem>
            <asp:MenuItem Text="Progress" Value="Progress" Selectable="false">
                <asp:MenuItem NavigateUrl="~/Reports/Progress/ReportProgressCwp.aspx" Text="Weekly Base Scope" Value="Weekly Base Scope" />
                <asp:MenuItem NavigateUrl="~/Reports/Progress/ReportProgressVariation.aspx" Text="Weekly Extra work" Value="Weekly Extra work" />
                <%--
                <asp:MenuItem Text="Man Hour by" Value="Man Hour by" Selectable="false">
                    <asp:MenuItem NavigateUrl="~/Reports/Progress/ReportManHourByTrade.aspx" Text="Trade" Value="Trade" />
                    <asp:MenuItem NavigateUrl="~/Reports/Progress/ReportManHourByPositionCategory.aspx" Text="Position Category" Value="Position Category" />
                </asp:MenuItem>
                --%>
            </asp:MenuItem>         
        </asp:MenuItem>
    </Items>
    <StaticHoverStyle BackColor="#ffcc01" ForeColor="White" />
    <StaticMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
    <StaticSelectedStyle BackColor="#507CD1" />
</asp:Menu>

</div>
