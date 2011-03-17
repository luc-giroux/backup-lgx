<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EditContractStructure.aspx.cs" Inherits="EditContractStructure" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register TagPrefix="myTagPrefix" TagName="AppHeader" Src="PageHeader.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Edit contract structure</title>
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
                <legend>Edit contract structure</legend>
                <ol>
                <li>
                    <label for="WS">WS<em>*</em></label>
                    <asp:ComboBox ID="ComboBoxWS" runat="server" AutoCompleteMode="SuggestAppend" CaseSensitive="False"
                        CssClass="AquaStyle" DropDownStyle="DropDownList" AppendDataBoundItems="true" width="300px">
                </asp:ComboBox>
                </li>
                <li>
                    <label for="WP">WP<em>*</em></label>
                    <asp:ComboBox ID="ComboBoxWP" runat="server" AutoCompleteMode="SuggestAppend" CaseSensitive="False"
                        CssClass="AquaStyle" DropDownStyle="DropDownList" AppendDataBoundItems="true" width="300px">
                </asp:ComboBox>
                </li>
                <li>
                    <label for="CWP">CWP<em>*</em></label>
                    <asp:ComboBox ID="ComboBoxCWP" runat="server" AutoCompleteMode="SuggestAppend" CaseSensitive="False"
                        CssClass="AquaStyle" DropDownStyle="DropDownList" AppendDataBoundItems="true" width="300px">
                </asp:ComboBox>
                </li>
                <li>
                    <label for="AllocatedHours">Allocated Hours by CWP<em>*</em></label>
                    <asp:TextBox ID="TextBoxHours" runat="server"></asp:TextBox>
                </li>
                <li>
                    <label for="blank"></label>
                    <asp:Button ID="ButtonSubmit" runat="server" Text="Submit" 
                     CausesValidation="false" onclick="ButtonSubmit_Click"  />
                </li>
                </ol>
            </fieldset>
        </div>

        <%-- to write information into input fields --%>
     
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ToolkitScriptManager>

        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="TextBoxHours" FilterType="Custom, Numbers"
            ValidChars=".," /> 


    </form>

</body>
</html>
