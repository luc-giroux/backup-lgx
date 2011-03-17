<%@ Page Language="vb" AutoEventWireup="false" Inherits="TimeSheetV190_HTJV.ts_hp"
    Culture="en-US" CodeFile="ts_hp.aspx.vb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Koniambo Nickel Project - Weekly Timesheet</title>
    <link rel="stylesheet" type="text/css" href="App_Themes/Koniambo/StyleSheet.css" />

    <script type="text/javascript" language="JavaScript" src="jsinclude-1.js"></script>

    <script type="text/javascript" language="JavaScript">
		function disableEnterKey()
		{
			if (window.event.keyCode == 13) window.event.keyCode = 0;
		}
    </script>

</head>
<body style="margin: 0px; background-color: #fff;">
    <form id="Form1" runat="server">
    <table id="Table3" cellspacing="1" cellpadding="1" border="0">
        <tr>
            <td>
                <table id="ts_hdr" cellspacing="1" cellpadding="1" width="100%" border="1">
                    <tr>
                        <td style="width: 55px" nowrap>
                            &nbsp;<asp:Label ID="Label1" runat="server" Width="37px" Font-Names="Arial" Font-Size="XX-Small">Name</asp:Label>
                        </td>
                        <td style="width: 185px" nowrap>
                            &nbsp;<asp:TextBox ID="hdr_nm" Style="border-right: medium none; border-top: medium none;
                                border-left: medium none; border-bottom: medium none" runat="server" Width="178px"
                                Font-Names="Arial" Font-Size="XX-Small" BorderStyle="None" ReadOnly="True"></asp:TextBox>
                        </td>
                        <td nowrap align="center" colspan="4" rowspan="4">
                            <asp:Label ID="Label2" runat="server" Font-Names="Arial" Font-Size="X-Small" Font-Bold="True">Weekly Timesheet</asp:Label>
                            <asp:Label ID="Label3" runat="server" Font-Names="Arial" Font-Size="X-Small" Font-Bold="True">Koniambo Nickel Project</asp:Label>
                        </td>
                        <td style="width: 80px" nowrap>
                            &nbsp;
                            <asp:Label ID="Label4" runat="server" Font-Names="Arial" Font-Size="XX-Small">Year</asp:Label></td>
                        <td nowrap>
                            &nbsp;
                            <asp:TextBox ID="HDRYear" runat="server" BorderStyle="None" Font-Bold="True" Font-Names="Arial"
                                Font-Size="XX-Small" Style="border-right: medium none; border-top: medium none;
                                border-left: medium none; border-bottom: medium none"></asp:TextBox></td>                       
                    </tr>
                    <tr>
                        <td style="width: 55px" nowrap>
                            &nbsp;<asp:Label ID="Label12" runat="server" Font-Names="Arial" Font-Size="XX-Small">Department</asp:Label>
                        </td>
                        <td style="width: 185px" nowrap>
                            &nbsp;<asp:TextBox ID="hdr_dept" Style="border-right: medium none; border-top: medium none;
                                border-left: medium none; border-bottom: medium none" runat="server" Width="178px"
                                Font-Names="Arial" Font-Size="XX-Small" BorderStyle="None" ReadOnly="True" Font-Bold="True"></asp:TextBox>
                        </td>
                        <td style="width: 80px" nowrap>
                            &nbsp;
                            <asp:Label ID="Label7" runat="server" Font-Names="Arial" Font-Size="XX-Small">Week No.</asp:Label></td>
                        <td nowrap>
                            &nbsp;
                            <asp:TextBox ID="hdr_week" Style="border-right: medium none; border-top: medium none;
                                border-left: medium none; border-bottom: medium none" runat="server" Font-Names="Arial"
                                Font-Size="XX-Small" BorderStyle="None" Font-Bold="True"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="width: 55px" nowrap>
                            &nbsp;<asp:Label ID="Label13" runat="server" Font-Names="Arial" Font-Size="XX-Small">Discipline</asp:Label>
                        </td>
                        <td style="width: 185px" nowrap>
                            &nbsp;<asp:TextBox ID="hdr_disc" Style="border-right: medium none; border-top: medium none;
                                border-left: medium none; border-bottom: medium none" runat="server" Width="178px"
                                Font-Names="Arial" Font-Size="XX-Small" BorderStyle="None" ReadOnly="True" Font-Bold="True"></asp:TextBox>
                        </td>
                        <td style="width: 80px" nowrap >
                            &nbsp;
                            <asp:Label ID="Label8" runat="server" Font-Names="Arial" Font-Size="XX-Small">Start Date</asp:Label></td>
                        <td nowrap >
                            &nbsp;
                            <asp:TextBox ID="hdr_sdate" Style="border-right: medium none; border-top: medium none;
                                border-left: medium none; border-bottom: medium none" runat="server" Font-Names="Arial"
                                Font-Size="XX-Small" BorderStyle="None" ReadOnly="True" Font-Bold="True"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="width: 55px" nowrap>
                            &nbsp;<asp:Label ID="Label14" runat="server" Font-Names="Arial" Font-Size="XX-Small">Position</asp:Label>
                        </td>
                        <td style="width: 185px" nowrap>
                            &nbsp;<asp:TextBox ID="hdr_pos" Style="border-right: medium none; border-top: medium none;
                                border-left: medium none; border-bottom: medium none" runat="server" Width="178px"
                                Font-Names="Arial" Font-Size="XX-Small" BorderStyle="None" ReadOnly="True" Font-Bold="True"></asp:TextBox>
                        </td>
                        <td style="width: 80px" nowrap bgcolor="#cccccc">
                            &nbsp;<asp:Label ID="Label10" runat="server" Font-Names="Arial" Font-Size="XX-Small"
                                Visible="False">Employee No.</asp:Label>
                        </td>
                        <td nowrap bgcolor="#cccccc">
                            &nbsp;<asp:TextBox ID="hdr_ono" Style="border-right: medium none; border-top: medium none;
                                border-left: medium none; border-bottom: medium none" runat="server" Font-Names="Arial"
                                Font-Size="XX-Small" BorderStyle="None" ReadOnly="True" Font-Bold="True" BackColor="#CCCCCC"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 55px" nowrap>
                            &nbsp;<asp:Label ID="Label15" runat="server" Width="67px" Font-Names="Arial" Font-Size="XX-Small">Employee No.</asp:Label>
                        </td>
                        <td style="width: 185px" nowrap>
                            &nbsp;<asp:TextBox ID="hdr_no" Style="border-right: medium none; border-top: medium none;
                                border-left: medium none; border-bottom: medium none" runat="server" Width="178px"
                                Font-Names="Arial" Font-Size="XX-Small" BorderStyle="None" ReadOnly="True" Font-Bold="True"
                                MaxLength="8"></asp:TextBox>
                        </td>
                        <td nowrap>
                            &nbsp;<asp:Label ID="Label21" runat="server" Font-Names="Arial" Font-Size="XX-Small"
                                Visible="False">Company</asp:Label>
                        </td>
                        <td nowrap>
                            &nbsp;<asp:TextBox ID="hdr_com" Style="border-right: medium none; border-top: medium none;
                                border-left: medium none; border-bottom: medium none; display:none;" runat="server" Font-Names="Arial"
                                Font-Size="XX-Small" BorderStyle="None" ReadOnly="True" Font-Bold="True"></asp:TextBox>
                        </td>
                        <td nowrap>
                            &nbsp;<asp:Label ID="Label23" runat="server" Font-Names="Arial" Font-Size="XX-Small"
                                Visible="False">Location</asp:Label>
                        </td>
                        <td nowrap>
                            &nbsp;<asp:TextBox ID="hdr_loc" Style="border-right: medium none; border-top: medium none;
                                border-left: medium none; border-bottom: medium none" runat="server" Font-Names="Arial"
                                Font-Size="XX-Small" BorderStyle="None" ReadOnly="True" Font-Bold="True" Visible="False"></asp:TextBox>
                        </td>
                        <td style="width: 80px" nowrap bgcolor="#cccccc">
                            &nbsp;<asp:Label ID="Label11" runat="server" Font-Names="Arial" Font-Size="XX-Small"
                                Visible="False">Discipline</asp:Label>
                        </td>
                        <td nowrap bgcolor="#cccccc">
                            &nbsp;<asp:TextBox ID="hdr_odisc" Style="border-right: medium none; border-top: medium none;
                                border-left: medium none; border-bottom: medium none" runat="server" Font-Names="Arial"
                                Font-Size="XX-Small" BorderStyle="None" ReadOnly="True" Font-Bold="True" BackColor="#CCCCCC"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table id="Table6" cellspacing="1" cellpadding="1" width="25%" border="0">
                    <tr>
                        <td align="middle" style="width: 60px; height: 21px">
                            <input type="button" id="AddRow" style="font-size: xx-small; font-family: Arial; width: 100px;" 
                                   value="Add row" onclick="javascript:addrow();" />
                        </td>
                        <td align="middle" style="width: 60px; height: 21px">
                            <input type="button" id="RemoveRow" style="font-size: xx-small; font-family: Arial; width: 100px;"  
                                 value="Remove row" onclick="javascript:deleterow();"/>
                            </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table id="Table1" cellspacing="0" cellpadding="1" width="100%" border="0">
                    <tbody>
                        <tr>
                            <td colspan="3" style="height: 68px">
                                <table id="project_table" width="100%" border="1" cellspacing="0" cellpadding="0">
                                    <tr id="tr0">
                                        <td style="font-size: xx-small; font-family: Arial" valign="center" align="middle" bgcolor="#cccccc">
                                            &nbsp;
                                        </td>
                                        <td style="width: 104px;" valign="center" align="middle" bgcolor="#cccccc">
                                            &nbsp;
                                        </td>
                                        <td style="width: 128px;" align="middle" bgcolor="#cccccc">
                                            &nbsp;
                                        </td>
                                        <td style="width: 107px;" nowrap align="middle" bgcolor="#cccccc">
                                            &nbsp;
                                        </td>
                                        <td style="font-size: x-small; width: 385px; font-family: Arial;" align="middle"
                                            bgcolor="#cccccc" colspan="8">
                                            <strong style="font-size: xx-small; font-family: Arial">Normal Time (NT)</strong>
                                        </td>
                                        <td align="middle" bgcolor="#cccccc" colspan="8">
                                            <strong style="font-size: xx-small; font-family: Arial">Over Time (OT)</strong>
                                        </td>
                                    </tr>
                                    <tr id="tr1">
                                        <td style="font-size: xx-small; font-family: Arial; height: 22px;" valign="center" align="middle" bgcolor="#cccccc">
                                            <!-- input id="deleteradio" disabled type="radio" name="deleteradio" -->
                                        </td>
                                        <td style="width: 104px; height: 22px;" align="middle" bgcolor="#cccccc">
                                            <strong style="font-size: xx-small; font-family: Arial">Cost Code</strong>
                                        </td>
                                        <td style="width: 128px; height: 22px;" valign="center" align="middle" bgcolor="#cccccc">
                                            <strong style="font-size: xx-small; font-family: Arial"><strong style="font-size: xx-small;
                                                font-family: Arial">Activity Code</strong></strong>
                                        </td>
                                        <td style="width: 107px; height: 22px;" nowrap align="middle" bgcolor="#cccccc">
                                            <strong style="font-size: xx-small; font-family: Arial"><strong style="font-size: xx-small;
                                                font-family: Arial">
                                                <input id="" style="border-right: medium none; border-top: medium none; font-size: xx-small;
                                                    border-left: medium none; width: 228px; border-bottom: medium none; font-family: Arial;
                                                    background-color: #cccccc; text-align: center" type="text" maxlength="150" size="32"
                                                    value="Task Description" name=""></strong></strong>
                                        </td>
                                        <td style="width: 53px; height: 22px;" align="middle" bgcolor="#cccccc">
                                            <strong style="font-size: xx-small; font-family: Arial">S</strong>
                                        </td>
                                        <td style="width: 53px; height: 22px;" align="middle" bgcolor="#cccccc">
                                            <strong style="font-size: xx-small; font-family: Arial">S</strong>
                                        </td>
                                        <td style="width: 59px; height: 22px;" align="middle" bgcolor="#cccccc">
                                            <strong style="font-size: xx-small; font-family: Arial">M</strong>
                                        </td>
                                        <td style="width: 54px; height: 22px;" align="middle" bgcolor="#cccccc">
                                            <strong style="font-size: xx-small; font-family: Arial">T</strong>
                                        </td>
                                        <td style="width: 49px; height: 22px;" align="middle" bgcolor="#cccccc">
                                            <strong style="font-size: xx-small; font-family: Arial">W</strong>
                                        </td>
                                        <td style="width: 50px; height: 22px;" align="middle" bgcolor="#cccccc">
                                            <strong style="font-size: xx-small; font-family: Arial">T</strong>
                                        </td>
                                        <td style="width: 52px; height: 22px;" align="middle" bgcolor="#cccccc">
                                            <strong style="font-size: xx-small; font-family: Arial">F</strong>
                                        </td>
                                        <td style="width: 49px; height: 22px;" align="middle" bgcolor="#cccccc">
                                            <strong style="font-size: xx-small; font-family: Arial">Total</strong>
                                        </td>
                                        <td style="width: 51px; height: 22px;" align="middle" bgcolor="#cccccc">
                                            <strong style="font-size: xx-small; font-family: Arial">S</strong>
                                        </td>
                                        <td style="width: 51px; height: 22px;" align="middle" bgcolor="#cccccc">
                                            <strong style="font-size: xx-small; font-family: Arial">S</strong>
                                        </td>
                                        <td style="width: 51px; height: 22px;" align="middle" bgcolor="#cccccc">
                                            <strong style="font-size: xx-small; font-family: Arial">M</strong>
                                        </td>
                                        <td style="width: 54px; height: 22px;" align="middle" bgcolor="#cccccc">
                                            <strong style="font-size: xx-small; font-family: Arial">T</strong>
                                        </td>
                                        <td style="width: 50px; height: 22px;" align="middle" bgcolor="#cccccc">
                                            <strong style="font-size: xx-small; font-family: Arial">W</strong>
                                        </td>
                                        <td style="width: 52px; height: 22px;" align="middle" bgcolor="#cccccc">
                                            <strong style="font-size: xx-small; font-family: Arial">T</strong>
                                        </td>
                                        <td style="width: 53px; height: 22px;" align="middle" bgcolor="#cccccc">
                                            <strong style="font-size: xx-small; font-family: Arial">F</strong>
                                        </td>
                                        <td align="middle" bgcolor="#cccccc" style="height: 22px">
                                            <strong style="font-size: xx-small; font-family: Arial">Total</strong>
                                        </td>
                                    </tr>
                                    <asp:Repeater ID="Repeater1" runat="server" >
                                        <ItemTemplate>
                                            <tr id="tr2"  >
                                                <td align="middle">
                                                    <input id="radio2delete" type="radio" name="radio2delete" value="<%# Container.ItemIndex+2 %>">
                                                </td>
                                                <td style="width: 104px" align="middle">
                                                    <input name="ccode<%# Container.ItemIndex+2 %>" type="text" id="ccode<%# Container.ItemIndex+2 %>"
                                                        style="font-size: xx-small; font-family: Arial; text-align: center; border-right: medium none;
                                                        border-top: medium none; border-bottom: medium none; border-left: medium none;"
                                                        value='<%# Eval("CostCode") %>'  onchange="javascript:changevalue();checkHours(this);" size="4" maxlength="10" onmousedown="openCostCode('<%# Container.ItemIndex+2 %>');"
                                                        readonly>&nbsp;<img alt="Cost Code List" src="images\list.gif" onclick="openCostCode('<%# Container.ItemIndex+2 %>');">
                                                </td>
                                                <td style="width: 107px" align="middle" nowrap>
                                                    <input name="ctrc<%# Container.ItemIndex+2 %>" type="text" id="ctrc<%# Container.ItemIndex+2 %>"
                                                        style="font-size: xx-small; font-family: Arial; text-align: center;" value='<%# Eval("CTRCode") %>'
                                                        size="15" maxlength="15" onfocus='this.focus();this.select();' onblur="ChangeCTRCodeUpperCase(<%# Container.ItemIndex+2 %>);"
                                                        readonly><img alt="CTR List" src="images\list.gif" onclick="openCTRCode('<%# Container.ItemIndex+2 %>');">
                                                </td>
                                                <td style="" align="middle">
                                                    <input name="ccdesc<%# Container.ItemIndex+2 %>" type="text" id="ccdesc<%# Container.ItemIndex+2 %>"
                                                        style="font-size: xx-small; font-family: Arial; text-align: left;" value='<%# Eval("Description") %>'
                                                         readonly="readonly"   size="50" maxlength="150" onfocus='this.focus();this.select();' onblur="ChangeUpperCase(<%# Container.ItemIndex+2 %>);">&nbsp;<!--IMG alt="Description List" src="images\list.gif" onclick="openCTRDesc('<%# Container.ItemIndex+2 %>');"-->
                                                </td>
                                                <td style="width: 53px" align="middle">
                                                    <input name="ntsa<%# Container.ItemIndex+2 %>" type="text" id="ntsa<%# Container.ItemIndex+2 %>"
                                                        style="font-size: xx-small; font-family: Arial; text-align: center;" value='<%# Eval("NTSaturday") %>'
                                                        size="1" maxlength="4" onblur="javascript:changevalue();checkHours(this);" onfocus='this.focus();this.select();'>
                                                </td>
                                                <td style="width: 53px" align="middle">
                                                    <input name="ntsu<%# Container.ItemIndex+2 %>" type="text" id="ntsu<%# Container.ItemIndex+2 %>"
                                                        style="font-size: xx-small; font-family: Arial; text-align: center;" value='<%# Eval("NTSunday") %>'
                                                        size="1" maxlength="4" onblur="javascript:changevalue();checkHours(this);" onfocus='this.focus();this.select();'>
                                                </td>
                                                <td style="width: 59px" align="middle">
                                                    <input name="ntmo<%# Container.ItemIndex+2 %>" type="text" id="ntmo<%# Container.ItemIndex+2 %>"
                                                        style="font-size: xx-small; font-family: Arial; text-align: center;" value='<%# Eval("NTMonday") %>'
                                                        size="1" maxlength="4" onblur="javascript:changevalue();checkHours(this);" onfocus='this.focus();this.select();'>
                                                </td>
                                                <td style="width: 54px" align="middle">
                                                    <input name="nttu<%# Container.ItemIndex+2 %>" type="text" id="nttu<%# Container.ItemIndex+2 %>"
                                                        style="font-size: xx-small; font-family: Arial; text-align: center;" value='<%# Eval("NTTuesday") %>'
                                                        size="1" maxlength="4" onblur="javascript:changevalue();checkHours(this);" onfocus='this.focus();this.select();'>
                                                </td>
                                                <td style="width: 49px" align="middle">
                                                    <input name="ntwe<%# Container.ItemIndex+2 %>" type="text" id="ntwe<%# Container.ItemIndex+2 %>"
                                                        style="font-size: xx-small; font-family: Arial; text-align: center;" value='<%# Eval("NTWednesday") %>'
                                                        size="1" maxlength="4" onblur="javascript:changevalue();checkHours(this);" onfocus='this.focus();this.select();'>
                                                </td>
                                                <td style="width: 50px" align="middle">
                                                    <input name="ntth<%# Container.ItemIndex+2 %>" type="text" id="ntth<%# Container.ItemIndex+2 %>"
                                                        style="font-size: xx-small; font-family: Arial; text-align: center;" value='<%# Eval("NTThursday") %>'
                                                        size="1" maxlength="4" onblur="javascript:changevalue();checkHours(this);" onfocus='this.focus();this.select();'>
                                                </td>
                                                <td style="width: 52px" align="middle">
                                                    <input name="ntfr<%# Container.ItemIndex+2 %>" type="text" id="ntfr<%# Container.ItemIndex+2 %>"
                                                        style="font-size: xx-small; font-family: Arial; text-align: center;" value='<%# Eval("NTFriday") %>'
                                                        size="1" maxlength="4" onblur="javascript:changevalue();checkHours(this);" onfocus='this.focus();this.select();'>
                                                </td>
                                                <td style="width: 49px" align="middle" bgcolor="#ffffcc">
                                                    <input name="ntto<%# Container.ItemIndex+2 %>" type="text" id="ntto<%# Container.ItemIndex+2 %>"
                                                        style="border-right: medium none; border-top: medium none; font-size: xx-small;
                                                        border-left: medium none; border-bottom: medium none; font-family: Arial; background-color: #ffffcc;
                                                        text-align: center" value="" size="1" maxlength="4" readonly>
                                                </td>
                                                <td style="width: 51px" align="middle">
                                                    <input name="otsa<%# Container.ItemIndex+2 %>" type="text" id="otsa<%# Container.ItemIndex+2 %>"
                                                        style="font-size: xx-small; font-family: Arial; text-align: center;" value='<%# Eval("OTSaturday") %>'
                                                        size="1" maxlength="4" onblur="javascript:changevalue();checkHours(this);" onfocus='this.focus();this.select();'>
                                                </td>
                                                <td style="width: 51px" align="middle">
                                                    <input name="otsu<%# Container.ItemIndex+2 %>" type="text" id="otsu<%# Container.ItemIndex+2 %>"
                                                        style="font-size: xx-small; font-family: Arial; text-align: center;" value='<%# Eval("OTSunday") %>'
                                                        size="1" maxlength="4" onblur="javascript:changevalue();checkHours(this);" onfocus='this.focus();this.select();'>
                                                </td>
                                                <td style="width: 51px" align="middle">
                                                    <input name="otmo<%# Container.ItemIndex+2 %>" type="text" id="otmo<%# Container.ItemIndex+2 %>"
                                                        style="font-size: xx-small; font-family: Arial; text-align: center;" value='<%# Eval("OTMonday") %>'
                                                        size="1" maxlength="4" onblur="javascript:changevalue();checkHours(this);" onfocus='this.focus();this.select();'>
                                                </td>
                                                <td style="width: 54px" align="middle">
                                                    <input name="ottu<%# Container.ItemIndex+2 %>" type="text" id="ottu<%# Container.ItemIndex+2 %>"
                                                        style="font-size: xx-small; font-family: Arial; text-align: center;" value='<%# Eval("OTTuesday") %>'
                                                        size="1" maxlength="4" onblur="javascript:changevalue();checkHours(this);" onfocus='this.focus();this.select();'>
                                                </td>
                                                <td style="width: 50px" align="middle">
                                                    <input name="otwe<%# Container.ItemIndex+2 %>" type="text" id="otwe<%# Container.ItemIndex+2 %>"
                                                        style="font-size: xx-small; font-family: Arial; text-align: center;" value='<%# Eval("OTWednesday") %>'
                                                        size="1" maxlength="4" onblur="javascript:changevalue();checkHours(this);" onfocus='this.focus();this.select();'>
                                                </td>
                                                <td style="width: 52px" align="middle">
                                                    <input name="otth<%# Container.ItemIndex+2 %>" type="text" id="otth<%# Container.ItemIndex+2 %>"
                                                        style="font-size: xx-small; font-family: Arial; text-align: center;" value='<%# Eval("OTThursday") %>'
                                                        size="1" maxlength="4" onblur="javascript:changevalue();checkHours(this);" onfocus='this.focus();this.select();'>
                                                </td>
                                                <td style="width: 53px" align="middle">
                                                    <input name="otfr<%# Container.ItemIndex+2 %>" type="text" id="otfr<%# Container.ItemIndex+2 %>"
                                                        style="font-size: xx-small; font-family: Arial; text-align: center;" value='<%# Eval("OTFriday") %>'
                                                        size="1" maxlength="4" onblur="javascript:changevalue();checkHours(this);" onfocus='this.focus();this.select();'>
                                                </td>
                                                <td align="middle" bgcolor="#ffffcc">
                                                    <input name="otto<%# Container.ItemIndex+2 %>" type="text" id="otto<%# Container.ItemIndex+2 %>"
                                                        style="border-right: medium none; border-top: medium none; font-size: xx-small;
                                                        border-left: medium none; border-bottom: medium none; font-family: Arial; background-color: #ffffcc;
                                                        text-align: center" value="" size="1" maxlength="4" readonly  >
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <asp:Repeater ID="ReadOnlyRepeater" runat="server" >
                                        <ItemTemplate>
                                            <tr id="tr3"  >
                                                <td align="middle">
                                                    <input id="radio2delete" type="radio" name="radio2delete"  disabled="disabled" value="<%# Container.ItemIndex+2 %>">
                                                </td>
                                                <td style="width: 104px" align="middle">
                                                    <input name="ccode<%# Container.ItemIndex+2 %>" type="text" id="ccode<%# Container.ItemIndex+2 %>"
                                                        style="font-size: xx-small; font-family: Arial; text-align: center; border-right: medium none;
                                                        border-top: medium none; border-bottom: medium none; border-left: medium none;"
                                                        value='<%# Eval("CostCode") %>'  size="4" maxlength="10" 
                                                        readonly>&nbsp;<img alt="Cost Code List" src="images\list.gif"  >
                                                </td>
                                                <td style="width: 107px" align="middle" nowrap>
                                                    <input name="ctrc<%# Container.ItemIndex+2 %>" type="text" id="ctrc<%# Container.ItemIndex+2 %>"
                                                        style="font-size: xx-small; font-family: Arial; text-align: center;" value='<%# Eval("CTRCode") %>'
                                                        size="15" maxlength="15" readonly>
                                                        <img alt="CTR List" src="images\list.gif" >
                                                </td>
                                                <td style="" align="middle">
                                                    <input name="ccdesc<%# Container.ItemIndex+2 %>" type="text" id="ccdesc<%# Container.ItemIndex+2 %>"
                                                        style="font-size: xx-small; font-family: Arial; text-align: left;" value='<%# Eval("Description") %>'
                                                        size="50" maxlength="150" readonly="readonly"   >&nbsp;
                                                </td>
                                                <td style="width: 53px" align="middle">
                                                    <input name="ntsa<%# Container.ItemIndex+2 %>" type="text" id="ntsa<%# Container.ItemIndex+2 %>"
                                                        style="font-size: xx-small; font-family: Arial; text-align: center;" value='<%# Eval("NTSaturday") %>'
                                                        size="1" maxlength="4" readonly="readonly" >
                                                </td>
                                                <td style="width: 53px" align="middle">
                                                    <input name="ntsu<%# Container.ItemIndex+2 %>" type="text" id="ntsu<%# Container.ItemIndex+2 %>"
                                                        style="font-size: xx-small; font-family: Arial; text-align: center;" value='<%# Eval("NTSunday") %>'
                                                        size="1" maxlength="4" readonly="readonly" >
                                                </td>
                                                <td style="width: 59px" align="middle">
                                                    <input name="ntmo<%# Container.ItemIndex+2 %>" type="text" id="ntmo<%# Container.ItemIndex+2 %>"
                                                        style="font-size: xx-small; font-family: Arial; text-align: center;" value='<%# Eval("NTMonday") %>'
                                                        size="1" maxlength="4" readonly="readonly" >
                                                </td>
                                                <td style="width: 54px" align="middle">
                                                    <input name="nttu<%# Container.ItemIndex+2 %>" type="text" id="nttu<%# Container.ItemIndex+2 %>"
                                                        style="font-size: xx-small; font-family: Arial; text-align: center;" value='<%# Eval("NTTuesday") %>'
                                                        size="1" maxlength="4" readonly="readonly" >
                                                </td>
                                                <td style="width: 49px" align="middle">
                                                    <input name="ntwe<%# Container.ItemIndex+2 %>" type="text" id="ntwe<%# Container.ItemIndex+2 %>"
                                                        style="font-size: xx-small; font-family: Arial; text-align: center;" value='<%# Eval("NTWednesday") %>'
                                                        size="1" maxlength="4" readonly="readonly" >
                                                </td>
                                                <td style="width: 50px" align="middle">
                                                    <input name="ntth<%# Container.ItemIndex+2 %>" type="text" id="ntth<%# Container.ItemIndex+2 %>"
                                                        style="font-size: xx-small; font-family: Arial; text-align: center;" value='<%# Eval("NTThursday") %>'
                                                        size="1" maxlength="4" readonly="readonly" >
                                                </td>
                                                <td style="width: 52px" align="middle">
                                                    <input name="ntfr<%# Container.ItemIndex+2 %>" type="text" id="ntfr<%# Container.ItemIndex+2 %>"
                                                        style="font-size: xx-small; font-family: Arial; text-align: center;" value='<%# Eval("NTFriday") %>'
                                                        size="1" maxlength="4" readonly="readonly" >
                                                </td>
                                                <td style="width: 49px" align="middle" bgcolor="#ffffcc">
                                                    <input name="ntto<%# Container.ItemIndex+2 %>" type="text" id="ntto<%# Container.ItemIndex+2 %>"
                                                        style="border-right: medium none; border-top: medium none; font-size: xx-small;
                                                        border-left: medium none; border-bottom: medium none; font-family: Arial; background-color: #ffffcc;
                                                        text-align: center" value="" size="1" maxlength="4" readonly>
                                                </td>
                                                <td style="width: 51px" align="middle">
                                                    <input name="otsa<%# Container.ItemIndex+2 %>" type="text" id="otsa<%# Container.ItemIndex+2 %>"
                                                        style="font-size: xx-small; font-family: Arial; text-align: center;" value='<%# Eval("OTSaturday") %>'
                                                        size="1" maxlength="4" readonly="readonly" >
                                                </td>
                                                <td style="width: 51px" align="middle">
                                                    <input name="otsu<%# Container.ItemIndex+2 %>" type="text" id="otsu<%# Container.ItemIndex+2 %>"
                                                        style="font-size: xx-small; font-family: Arial; text-align: center;" value='<%# Eval("OTSunday") %>'
                                                        size="1" maxlength="4" readonly="readonly" >
                                                </td>
                                                <td style="width: 51px" align="middle">
                                                    <input name="otmo<%# Container.ItemIndex+2 %>" type="text" id="otmo<%# Container.ItemIndex+2 %>"
                                                        style="font-size: xx-small; font-family: Arial; text-align: center;" value='<%# Eval("OTMonday") %>'
                                                        size="1" maxlength="4" readonly="readonly" >
                                                </td>
                                                <td style="width: 54px" align="middle">
                                                    <input name="ottu<%# Container.ItemIndex+2 %>" type="text" id="ottu<%# Container.ItemIndex+2 %>"
                                                        style="font-size: xx-small; font-family: Arial; text-align: center;" value='<%# Eval("OTTuesday") %>'
                                                        size="1" maxlength="4" readonly="readonly" >
                                                </td>
                                                <td style="width: 50px" align="middle">
                                                    <input name="otwe<%# Container.ItemIndex+2 %>" type="text" id="otwe<%# Container.ItemIndex+2 %>"
                                                        style="font-size: xx-small; font-family: Arial; text-align: center;" value='<%# Eval("OTWednesday") %>'
                                                        size="1" maxlength="4" readonly="readonly" >
                                                </td>
                                                <td style="width: 52px" align="middle">
                                                    <input name="otth<%# Container.ItemIndex+2 %>" type="text" id="otth<%# Container.ItemIndex+2 %>"
                                                        style="font-size: xx-small; font-family: Arial; text-align: center;" value='<%# Eval("OTThursday") %>'
                                                        size="1" maxlength="4" readonly="readonly" >
                                                </td>
                                                <td style="width: 53px" align="middle">
                                                    <input name="otfr<%# Container.ItemIndex+2 %>" type="text" id="otfr<%# Container.ItemIndex+2 %>"
                                                        style="font-size: xx-small; font-family: Arial; text-align: center;" value='<%# Eval("OTFriday") %>'
                                                        size="1" maxlength="4" readonly="readonly" >
                                                </td>
                                                <td align="middle" bgcolor="#ffffcc">
                                                    <input name="otto<%# Container.ItemIndex+2 %>" type="text" id="otto<%# Container.ItemIndex+2 %>"
                                                        style="border-right: medium none; border-top: medium none; font-size: xx-small;
                                                        border-left: medium none; border-bottom: medium none; font-family: Arial; background-color: #ffffcc;
                                                        text-align: center" value="" size="1" maxlength="4" readonly  >
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>                              
                                    <tr id="TotalRow">
                                        <td style="font-size: xx-small; font-family: Arial; height: 22px;" valign="center" align="middle" bgcolor="#ffffcc">
                                            <!-- input id="RadioFiller" disabled type="radio" name="deleteradio" -->
                                        </td>
                                        <td style="width: 104px; height: 22px;" align="middle" bgcolor="#ffffcc">
                                            <strong style="font-size: xx-small; font-family: Arial"></strong>
                                        </td>
                                        <td style="width: 128px; height: 22px;" valign="center" align="middle" bgcolor="#ffffcc">
                                            <strong style="font-size: xx-small; font-family: Arial"><strong style="font-size: xx-small;
                                                font-family: Arial"></strong></strong>
                                        </td>
                                        <td style="width: 107px; height: 22px;" nowrap align="middle" bgcolor="#ffffcc">
                                            <strong style="font-size: xx-small; font-family: Arial">
                                               Total</strong>
                                        </td>
                                        
                                        
                                         <td style="width: 53px; height: 20px;" align="center" bgcolor="#ffffcc" >
                                            <input id="ntsato" style="border-right: medium none; border-top: medium none; font-size: xx-small;
                                                border-left: medium none; border-bottom: medium none; font-family: Arial; background-color: #ffffcc;
                                                text-align: center" readonly type="text" maxlength="2" size="1" name="ntsato">
                                        </td>
                                        <td style="width: 53px; height: 20px;" align="center" bgcolor="#ffffcc">
                                            <input id="ntsuto" style="border-right: medium none; border-top: medium none; font-size: xx-small;
                                                border-left: medium none; border-bottom: medium none; font-family: Arial; background-color: #ffffcc;
                                                text-align: center" readonly type="text" maxlength="2" size="1" name="ntsuto">
                                        </td>
                                        <td style="width: 59px; height: 20px;" align="center" bgcolor="#ffffcc">
                                            <input id="ntmoto" style="border-right: medium none; border-top: medium none; font-size: xx-small;
                                                border-left: medium none; border-bottom: medium none; font-family: Arial; background-color: #ffffcc;
                                                text-align: center" readonly type="text" maxlength="2" size="1" name="ntmoto">
                                        </td>
                                        <td style="width: 54px; height: 20px;" align="center" bgcolor="#ffffcc">
                                            <input id="nttuto" style="border-right: medium none; border-top: medium none; font-size: xx-small;
                                                border-left: medium none; border-bottom: medium none; font-family: Arial; background-color: #ffffcc;
                                                text-align: center" readonly type="text" maxlength="2" size="1" name="nttuto">
                                        </td>
                                        <td style="width: 49px; height: 20px;" align="center" bgcolor="#ffffcc">
                                            <input id="ntweto" style="border-right: medium none; border-top: medium none; font-size: xx-small;
                                                border-left: medium none; border-bottom: medium none; font-family: Arial; background-color: #ffffcc;
                                                text-align: center" readonly type="text" maxlength="2" size="1" name="ntweto">
                                        </td>
                                        <td style="width: 50px; height: 20px;" align="center" bgcolor="#ffffcc">
                                            <input id="ntthto" style="border-right: medium none; border-top: medium none; font-size: xx-small;
                                                border-left: medium none; border-bottom: medium none; font-family: Arial; background-color: #ffffcc;
                                                text-align: center" readonly type="text" maxlength="2" size="1" name="ntthto">
                                        </td>
                                        <td style="width: 52px; height: 20px;" align="center" bgcolor="#ffffcc">
                                            <input id="ntfrto" style="border-right: medium none; border-top: medium none; font-size: xx-small;
                                                border-left: medium none; border-bottom: medium none; font-family: Arial; background-color: #ffffcc;
                                                text-align: center" readonly type="text" maxlength="2" size="1" name="ntfrto">
                                        </td>
                                        <td style="width: 49px; height: 20px;" align="center" bgcolor="#ffffcc">
                                            <input id="nttoto" style="border-right: medium none; border-top: medium none; font-size: xx-small;
                                                border-left: medium none; border-bottom: medium none; font-family: Arial; background-color: #ffffcc;
                                                text-align: center" readonly type="text" maxlength="2" size="1" name="nttoto">
                                        </td>
                                        <td style="width: 51px; height: 20px;" align="center" bgcolor="#ffffcc">
                                            <input id="otsato" style="border-right: medium none; border-top: medium none; font-size: xx-small;
                                                border-left: medium none; border-bottom: medium none; font-family: Arial; background-color: #ffffcc;
                                                text-align: center" readonly type="text" maxlength="2" size="1" name="otsato">
                                        </td>
                                        <td style="width: 51px; height: 20px;" align="center" bgcolor="#ffffcc">
                                            <input id="otsuto" style="border-right: medium none; border-top: medium none; font-size: xx-small;
                                                border-left: medium none; border-bottom: medium none; font-family: Arial; background-color: #ffffcc;
                                                text-align: center" readonly type="text" maxlength="2" size="1" name="otsuto">
                                        </td>
                                        <td style="width: 51px; height: 20px;" align="center" bgcolor="#ffffcc">
                                            <input id="otmoto" style="border-right: medium none; border-top: medium none; font-size: xx-small;
                                                border-left: medium none; border-bottom: medium none; font-family: Arial; background-color: #ffffcc;
                                                text-align: center" readonly type="text" maxlength="2" size="1" name="otmoto">
                                        </td>
                                        <td style="width: 54px; height: 20px;" align="center" bgcolor="#ffffcc">
                                            <input id="ottuto" style="border-right: medium none; border-top: medium none; font-size: xx-small;
                                                border-left: medium none; border-bottom: medium none; font-family: Arial; background-color: #ffffcc;
                                                text-align: center" readonly type="text" maxlength="2" size="1" name="ottuto">
                                        </td>
                                        <td style="width: 50px; height: 20px;" align="center" bgcolor="#ffffcc">
                                            <input id="otweto" style="border-right: medium none; border-top: medium none; font-size: xx-small;
                                                border-left: medium none; border-bottom: medium none; font-family: Arial; background-color: #ffffcc;
                                                text-align: center" readonly type="text" maxlength="2" size="1" name="otweto">
                                        </td>
                                        <td style="width: 52px; height: 20px;" align="center" bgcolor="#ffffcc">
                                            <input id="otthto" style="border-right: medium none; border-top: medium none; font-size: xx-small;
                                                border-left: medium none; border-bottom: medium none; font-family: Arial; background-color: #ffffcc;
                                                text-align: center" readonly type="text" maxlength="2" size="1" name="otthto">
                                        </td>
                                        <td style="width: 53px; height: 20px;" align="center" bgcolor="#ffffcc">
                                            <input id="otfrto" style="border-right: medium none; border-top: medium none; font-size: xx-small;
                                                border-left: medium none; border-bottom: medium none; font-family: Arial; background-color: #ffffcc;
                                                text-align: center" readonly type="text" maxlength="2" size="1" name="otfrto">
                                        </td>
                                        <td align="center" bgcolor="#ffffcc" style="height: 20px">
                                            <input id="ottoto" style="border-right: medium none; border-top: medium none; font-size: xx-small;
                                                border-left: medium none; border-bottom: medium none; font-family: Arial; background-color: #ffffcc;
                                                text-align: center" readonly type="text" maxlength="2" size="1" name="ottoto">
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr >
                            <td colspan="3" style="height: 20px">
                                &nbsp;<table id="RejectedTable" width="700px" border="1" cellspacing="0" cellpadding="0" style=" visibility: hidden ">
                                <tr style=" font-family:Arial; color:Red; font-size:x-small ; text-align:left; visibility:inherit " >
                                    <td colspan="5" style="height: 15px; visibility:inherit "><label style=" font-family:Arial; font-size:x-small;  visibility:inherit " >The following timesheet rows were rejected</label></td>
                                </tr>
                                <tr>
                                    <td style="width: 70px; text-align:center "><label style="font-family:Arial; font-size:xx-small ">Cost Code</label></td>
                                    <td style="width: 70px; text-align:center "><label style="font-family:Arial; font-size:xx-small ">CTR Code</label></td>
                                    <td style="width: 200px; text-align:left "><label style="font-family:Arial; font-size:xx-small ">Description</label></td>
                                    <td style="width: 120px; text-align:left "><label style="font-family:Arial; font-size:xx-small ">Rejected by</label></td>
                                    <td style="width: 200px; text-align:left "><label style="font-family:Arial; font-size:xx-small ">Reason</label></td>
                                </tr>
                                <asp:Repeater ID="RejectedRepeater" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td style="width: 70px; " align="center" >
                                            <label style="font-family:Arial; font-size:xx-small; text-align:center; color:Red "><%# Eval("CostCode") %></label>
                                        </td>
                                        <td style="width: 70px; " align="center" >
                                            <label style="font-family:Arial; font-size:xx-small; text-align:center; color:Red "><%# Eval("CTRCode") %></label>
                                        </td>
                                        <td style="width: 200px; " align="center" >
                                            <label style="font-family:Arial; font-size:xx-small; text-align:left;  overflow:scroll; color:Red "><%# Eval("Description") %></label>
                                        </td>
                                        <td style="width: 120px; " align="left">
                                            <label style="font-family:Arial; font-size:xx-small; text-align:left;  overflow:scroll; color:Red "><%# Eval("RejectedBy") %></label>
                                        </td>
                                        <td style="width: 200px; " align="left" >
                                            <label style="font-family:Arial; font-size:xx-small; text-align:left;  overflow:scroll; color:Red "><%# Eval("RejectReason") %></label>
                                        </td>
                                    </tr>
                                </ItemTemplate> 
                                </asp:Repeater>
                                </table>
                                <!-- caro2009 2010-07-12 - added a table for the "approver" -->
                                <table id="ApprovedTable" width="580px" border="1" cellspacing="0" cellpadding="0" style=" visibility: hidden ">
                                <tr style=" font-family:Arial; color: Green; font-size:x-small ; text-align:left; visibility:inherit " >
                                    <td colspan="5" style="height: 15px; visibility:inherit "><label style=" font-family:Arial; font-size:x-small;  visibility:inherit " >The following rows were approved</label></td>
                                </tr>
                                <tr>
                                    <td style="width: 70px; text-align:center "><label style="font-family:Arial; font-size:xx-small ">Cost Code</label></td>
                                    <td style="width: 70px; text-align:center "><label style="font-family:Arial; font-size:xx-small ">CTR Code</label></td>
                                    <td style="width: 250px; text-align:left "><label style="font-family:Arial; font-size:xx-small ">Description</label></td>
                                    <td style="width: 120px; text-align:left "><label style="font-family:Arial; font-size:xx-small ">Approved by</label></td>
                                    <td style="width: 70px; text-align:left "><label style="font-family:Arial; font-size:xx-small ">Date</label></td>
                                </tr>
                                <asp:Repeater ID="ApprovedRepeater" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td style="width: 70px; " align="center" >
                                            <label style="font-family:Arial; font-size:xx-small;"><%# Eval("CostCode") %></label>
                                        </td>
                                        <td style="width: 70px; " align="center" >
                                            <label style="font-family:Arial; font-size:xx-small;"><%# Eval("CTRCode") %></label>
                                        </td>
                                        <td style="width: 250px; " align="center" >
                                            <label style="font-family:Arial; font-size:xx-small;overflow:scroll;"><%# Eval("Description") %></label>
                                        </td>
                                        <td style="width: 120px; " align="center">
                                            <label style="font-family:Arial; font-size:xx-small;overflow:scroll;"><%# Eval("ApprovedBy") %></label>
                                        </td>
                                        <td style="width: 70px; " align="center" >
                                            <label style="font-family:Arial; font-size:xx-small;overflow:scroll;"><%# Eval("ApprovedDate") %></label>
                                        </td>
                                    </tr>
                                </ItemTemplate> 
                                </asp:Repeater>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td align="middle" colspan="3" style="height: 265px">
                                <table id="Table2" cellspacing="1" cellpadding="1" align="center" border="0">
                                    <tr>
                                    <input id="lbl_btnmsg" style="vertical-align: baseline; font-family: Arial; font-size: small ; text-align: center; width: 600px; border: none; color:Red "
                                    readonly="readonly" type="text" name="lbl_btnmsg" >
                                    </tr>
                                    <tr>
                                        <td valign="middle" nowrap align="center" width="25%">
                                            <asp:Button ID="btn_Save" runat="server" Font-Names="Arial" Font-Size="XX-Small"
                                                Enabled="False" Text="Save"></asp:Button>
                                        </td>
                                        <td valign="middle" nowrap align="center" width="25%">
                                            <asp:Button ID="btn_Submit" runat="server" Font-Names="Arial" Font-Size="XX-Small"
                                                Enabled="False" Text="Submit"></asp:Button>
                                        </td>
                                        <td valign="middle" nowrap align="center" width="25%">
                                            <input style="font-size: xx-small; font-family: Arial" onclick="javascript:window.location.href='ts_menu.aspx';"
                                                type="button" value="Back" name="Back" id="BackButton" runat="server">
                                        </td>
                                        <td valign="middle" nowrap align="center" >
                                            <fieldset style="table-layout: auto; font-size: xx-small; width: 100%; font-family: Arial;">
                                                <asp:checkbox ID="rb_Default" runat="server" Font-Names="Arial" Font-Size="XX-Small" Text ="Set as default timesheet" />
                                            </fieldset>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="middle" nowrap align="center" width="100%" colspan="4">
                                            <input id="count" type="hidden" size="1" name="count">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="middle" nowrap align="middle" width="100%" colspan="4">
                                            <p style="font-size: xx-small; font-family: Arial">
                                                Please press button <u>ONCE</u> only.</p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="center" nowrap align="middle" width="100%" colspan="4">
                                            <table id="Table7" style="border-collapse: collapse" bordercolor="gainsboro" cellspacing="1"
                                                cellpadding="1" border="1">
                                                <tr>
                                                    <td align="middle">
                                                        <asp:CheckBox ID="cb_SubmitNoManHours" runat="server" Font-Names="Arial" Font-Size="X-Small"
                                                            Text=' I would like to submit timesheet with no man-hours.<BR>I understand that after I press the "Submit With No Man-Hours" button,<br>it cannot submit again.'
                                                            AutoPostBack="True"></asp:CheckBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="middle">
                                                        <asp:Button ID="btn_SubmitNoManHours" runat="server" Font-Names="Arial" Font-Size="XX-Small"
                                                            Enabled="False" Text="Submit With No Man-Hours" CausesValidation="False"></asp:Button>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table id="Table4" cellspacing="1" cellpadding="1" width="700" align="center" border="0">
                    <tr>
                        <td colspan="3">
                            <fieldset style="table-layout: auto; font-size: xx-small; width: 700px; font-family: Arial">
                                <legend style="font-family: Garamond">Notes:</legend>
                                <table id="Table5" cellspacing="1" cellpadding="1" width="100%" border="0">
                                    <tr>
                                        <td style="font-size: xx-small; font-family: Arial">
                                            <li></li>
                                        </td>
                                        <td style="font-size: xx-small; font-family: Arial">
                                            <u>Save</u> button allows you to save only the entry. You are&nbsp;allowed to update
                                            the entry.&nbsp;This will not send&nbsp;for approval.
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="font-size: xx-small; font-family: Arial">
                                            <li></li>
                                        </td>
                                        <td style="font-size: xx-small; font-family: Arial">
                                            <p>
                                                <u>Submit</u> button allows you to submit the final entry. By clicking this, you
                                                will not allow to update after. The entry will submit for approval.</p>
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
<script language="javascript" type="text/jscript" >changevalue();</script>
</html>
