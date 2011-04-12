<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Help.aspx.cs" Inherits="Help" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register TagPrefix="myTagPrefix" TagName="AppHeader" Src="PageHeader.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Help</title>
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
        Please refer to the quick guide(s) below to understand how to use the application:
        <div class="appcontent">
          <ul>
            
            <asp:Panel ID="PanelContractorSupervisor" runat="server" Visible="false">
                <li>
                    <asp:HyperLink ID="HyperLinkContractorSupervisor" runat="server"  Target="_blank" NavigateUrl="~/QuickGuides/contractorSupervisor.pdf">Contractor supervisor quick guide</asp:HyperLink>
                </li>
            </asp:Panel>

            <asp:Panel ID="PanelOwnerSupervisor" runat="server" Visible="false">
                <li>
                    <asp:HyperLink ID="HyperLink2" runat="server" Target="_blank"  NavigateUrl="~/QuickGuides/OwnerSupervisor.pdf">Owner supervisor quick guide</asp:HyperLink>
                </li>
            </asp:Panel>

            <asp:Panel ID="PanelOwnerCA" runat="server" Visible="false">
                <li>
                    <asp:HyperLink ID="HyperLink1" runat="server" Target="_blank" NavigateUrl="~/QuickGuides/OwnerCA.pdf">Owner Contract Admin quick guide</asp:HyperLink>
                </li>
            </asp:Panel>            

            <asp:Panel ID="PanelContractorCA" runat="server" Visible="false">
                <li>
                    <asp:HyperLink ID="HyperLink3" runat="server" Target="_blank" NavigateUrl="~/QuickGuides/Reports.pdf">Reports quick guide</asp:HyperLink>
                </li>
            </asp:Panel>
          </ul>
          <br />
          If you encounter problems to download your quick guide, please <a href="mailto:AllianceTS-Admin@projetkoniambo.com?subject=Quick Guide Request from Alliance Timesheet Application"><u>send an email to administrator</u></a>

        </div>

    </form>

</body>
</html>
