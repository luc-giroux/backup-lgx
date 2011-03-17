<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserRights.aspx.cs" Inherits="UserRights" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register TagPrefix="myTagPrefix" TagName="AppHeader" Src="PageHeader.ascx" %>

<%@ Register TagPrefix="myTagPrefix" TagName="AdminRoleEdition" Src="AdminRoleEdition.ascx" %>
<%@ Register TagPrefix="myTagPrefix" TagName="CARoleEdition" Src="CARoleEdition.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>User Rights</title>
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
        <div class="appcontent">

           <fieldset>
                <legend>Add new Role for user <i><%=(HttpContext.Current.Session["currentUserLogin"]) %></i></legend>
                <ol>
                <li>
                    <label for="Role">Role<em>*</em></label>
                    <asp:ComboBox ID="ComboBoxRoles" runat="server" AutoCompleteMode="SuggestAppend" CaseSensitive="False" Width="300px"
                AutoPostBack="false" CssClass="AquaStyle" DropDownStyle="DropDownList" AppendDataBoundItems="true">
                </asp:ComboBox>
                </li>
                <li>
                    <asp:Label ID="LabelContract" runat="server" Visible="False">
                        <label for="Contract">Contract<em>*</em></label>
                    </asp:Label>
                    <asp:ComboBox ID="ComboBoxContract" runat="server" AutoCompleteMode="SuggestAppend" CaseSensitive="False" Width="300px"
                AutoPostBack="false" CssClass="AquaStyle" DropDownStyle="DropDownList" AppendDataBoundItems="true">
                </asp:ComboBox>
                </li>
                <li>
                    <label for="blank"></label>
                    <asp:Button ID="ButtonAddRole" runat="server" Text="Add Association"  
                        CausesValidation="false" onclick="ButtonAddRole_Click" />
                </li>
                </ol>
            </fieldset>
        <br />
        <b>Roles for user <i><%=(HttpContext.Current.Session["currentUserLogin"]) %></i></b> : <br /><br />
        
        <%-- GridView for the application Admin (can see roles on all contracts)--%>
        <myTagPrefix:AdminRoleEdition ID="AdminRoleEdition" runat="server"/>

         <%-- GridView for the Contractor Admin (can see roles only on the current contract)--%>
        <myTagPrefix:CARoleEdition ID="CARoleEdition" runat="server"/>


        </div>

        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ToolkitScriptManager>
    </form>

</body>
</html>
