<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddWS.aspx.cs" Inherits="AddWS" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register TagPrefix="myTagPrefix" TagName="AppHeader" Src="PageHeader.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Add WS</title>
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
                    <legend>Add new WS</legend>
                    <ol>
                    <li>
                        <label for="WS">WS Number<em>*</em></label>
                        <asp:TextBox ID="TextBoxWSNumber" runat="server" Width="300px"></asp:TextBox>
                    </li>
                    <li>
                        <label for="Description">Description<em>*</em></label>
                        <asp:TextBox ID="TextBoxDescription" runat="server" TextMode="MultiLine" Width="300px"></asp:TextBox>
                    </li>
                    <li>
                        <label for="blank"></label>
                        <asp:Button ID="Button1" runat="server" Text="Add WS" onclick="ButtonAddWS_OnClick" />
                    </li>
                    </ol>
                </fieldset>

        </div>

        <%-- to write information into input fields --%>
     
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ToolkitScriptManager>
        <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="TextBoxWSNumber" 
            WatermarkCssClass="watermarked" WatermarkText="This field is mandatory"></asp:TextBoxWatermarkExtender>
        <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="TextBoxDescription" 
            WatermarkCssClass="watermarked" WatermarkText="This field is mandatory"></asp:TextBoxWatermarkExtender>

    </form>

</body>
</html>
