﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddCWP.aspx.cs" Inherits="AddCWP" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register TagPrefix="myTagPrefix" TagName="AppHeader" Src="PageHeader.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Add CWP</title>
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
                <legend>Add new CWP</legend>
                <ol>
                <li>
                    <label for="CWP">CWP Number<em>*</em></label>
                    <asp:TextBox ID="TextBoxCWPNumber" runat="server" Width="300px"></asp:TextBox>
                </li>
                <li>
                    <label for="Description">Description<em>*</em></label>
                    <asp:TextBox ID="TextBoxDescription" runat="server" TextMode="MultiLine" Width="300px"></asp:TextBox>
                </li>
                <li>
                    <label for="Displine">Discipline</label>
                    <asp:ComboBox ID="ComboBoxDiscipline" runat="server" 
                        AutoCompleteMode="SuggestAppend" CaseSensitive="False"
                        CssClass="AquaStyle" DropDownStyle="DropDownList"  Width="300px"
                        AppendDataBoundItems="true">
                        <asp:ListItem Text="select" Value="select" Selected="True"/>
                        <asp:ListItem Text="Mechanical - 13 - ABOVEGROUND PIPING" Value= "Mechanical - 13 - ABOVEGROUND PIPING" />
                        <asp:ListItem Text="Mechanical - 14 - UNDERGROUND PIPING" Value= "Mechanical - 14 - UNDERGROUND PIPING" />
                        <asp:ListItem Text="Mechanical - 18 - STEEL" Value= "Mechanical - 18 - STEEL" />
                        <asp:ListItem Text="Mechanical - 19 - CLADDING" Value= "Mechanical - 19 - CLADDING" />
                        <asp:ListItem Text="Mechanical - 21 - INFRASTR / MODULE" Value= "Mechanical - 21 - INFRASTR / MODULE" />
                        <asp:ListItem Text="Mechanical - 23 - INSULATION" Value= "Mechanical - 23 - INSULATION" />
                        <asp:ListItem Text="Mechanical - 25 - TANKS / SPHERES" Value= "Mechanical - 25 - TANKS / SPHERES" />
                        <asp:ListItem Text="Mechanical - 26 - PAINTING / COATING" Value= "Mechanical - 26 - PAINTING / COATING" />
                        <asp:ListItem Text="Mechanical - 31 - REFRACTORY" Value= "Mechanical - 31 - REFRACTORY" />
                        <asp:ListItem Text="Mechanical - 42 - FIRE PROTECTION" Value= "Mechanical - 42 - FIRE PROTECTION" />
                        <asp:ListItem Text="Mechanical - 68 - EQUIPMENT (MECHANICAL)" Value= "Mechanical - 68 - EQUIPMENT (MECHANICAL)" />
                        <asp:ListItem Text="Mechanical - 85 - DUCTING" Value= "Mechanical - 85 - DUCTING" />
                        <asp:ListItem Text="CW - 01 - MINING" Value= "CW - 01 - MINING" />
                        <asp:ListItem Text="CW - 08 - EARTHWORKS" Value= "CW - 08 - EARTHWORKS" />
                        <asp:ListItem Text="CW - 09 - MAIN WHARF" Value= "CW - 09 - MAIN WHARF" />
                        <asp:ListItem Text="CW - 10 - MASS EARTHWORKS" Value= "CW - 10 - MASS EARTHWORKS" />
                        <asp:ListItem Text="CW - 11 - PILING" Value= "CW - 11 - PILING" />
                        <asp:ListItem Text="CW - 14 - UNDERGROUND PIPING (DRAIN)" Value= "CW - 14 - UNDERGROUND PIPING (DRAIN)" />
                        <asp:ListItem Text="CW - 17 - CIVIL (Concrete)" Value= "CW - 17 - CIVIL (Concrete)" />
                        <asp:ListItem Text="CW - 20 - BUILDINGS" Value= "CW - 20 - BUILDINGS" />
                        <asp:ListItem Text="CW - 32 - ROADS & PAVING" Value= "CW - 32 - ROADS & PAVING" />
                        <asp:ListItem Text="CW - 38 - DREDGING" Value= "CW - 38 - DREDGING" />
                        <asp:ListItem Text="CW - 90 - CONSTR INFRASTRUCTURE" Value= "CW - 90 - CONSTR INFRASTRUCTURE" />
                        <asp:ListItem Text="CW - 99 - GENERAL" Value= "CW - 99 - GENERAL" />
                        <asp:ListItem Text="E&I - 15 - INSTRUMENTATION" Value= "E&I - 15 - INSTRUMENTATION" />
                        <asp:ListItem Text="E&I - 16 - ELECTRICAL" Value= "E&I - 16 - ELECTRICAL" />
                        <asp:ListItem Text="E&I - 22 - ELECTRICAL (WIRE & CABLE)" Value= "E&I - 22 - ELECTRICAL (WIRE & CABLE)" />
                        <asp:ListItem Text="E&I - 29 - AUTOMATION OPERATIONS" Value= "E&I - 29 - AUTOMATION OPERATIONS" />
                    </asp:ComboBox>
                </li>
                <li>
                    <label for="blank"></label>
                    <asp:Button ID="ButtonAddCWP" runat="server" Text="Add CWP" 
                    onclick="ButtonAddCWP_OnClick" CausesValidation="false" />
                </li>
                </ol>
            </fieldset>
        </div>

        <%-- to write information into input fields --%>
     
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ToolkitScriptManager>
        <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="TextBoxCWPNumber" 
            WatermarkCssClass="watermarked" WatermarkText="This field is mandatory"></asp:TextBoxWatermarkExtender>
        <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="TextBoxDescription" 
            WatermarkCssClass="watermarked" WatermarkText="This field is mandatory"></asp:TextBoxWatermarkExtender> 


    </form>

</body>
</html>
