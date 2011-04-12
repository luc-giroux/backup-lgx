<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CancelTS.aspx.cs" Inherits="CancelTS" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register TagPrefix="myTagPrefix" TagName="AppHeader" Src="PageHeader.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Cancel Timesheets</title>
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
    <link href="site.css" rel="stylesheet" type="text/css" />
    <link type="text/css" href="jquery.jnotify.css" rel="stylesheet" title="default" media="all" />
    <script type="text/javascript" src="scripts/jquery-1.4.2.min.js"></script>
    <script type="text/javascript" src="scripts/jquery.jnotify.min.js"></script>
    <script type="text/javascript">
        $(".deleteLink").click(function () {
            return confirm('Are you sure you wish to delete this record?');
        });
</script>
    <script type="text/javascript">
        //LGX : to display the prompt for cancellation
        function show_prompt() {
            var reason = prompt("Please enter the cancellation request number:", "");
            // user clicked on cancel
            if (reason == null) {
                document.getElementById("HiddenFieldCancellationRequestNo").value = "cancel";
            }
            else {
                if (reason != "") {
                    document.getElementById("HiddenFieldCancellationRequestNo").value = reason;
                }
                else {
                    document.getElementById("HiddenFieldCancellationRequestNo").value = "OK_CLICK_BUT_NO_REASON";
                }
            }

        }
    </script>
</head>
<body>

    <form id="form1" runat="server" class="appform">
        <%-- Header with user informations --%>
        <myTagPrefix:AppHeader ID="Header1" runat="server"/><br />
        <div class="appcontent">
        <asp:HiddenField ID="HiddenFieldCancellationRequestNo" runat="server" />
        <!--START : SEARCH FILTERS-->
            <fieldset>
                <legend>Search approved timesheets by : </legend>
                
                    <table>
                        <tr>
                            <td><img src="img/puce1.png" /></td>
                            <td>Timesheet number : </td>
                            <td><asp:TextBox ID="TextBoxTSNumber" runat="server" /></td>
                        </tr>
                        <tr>
                            <td><img src="img/puce1.png" /></td>
                            <td>Timesheet From date : </td>
                            <td>
                                <asp:TextBox ID="TextBoxTSFromDate" runat="server" />
                                <asp:CalendarExtender  ID="CalendarExtender1" runat="server" targetControlID="TextBoxTSFromDate" Format="dd-MMM-yy" />
                            </td>
                            <td>To date : </td>
                            <td>
                                <asp:TextBox ID="TextBoxTSToDate" runat="server" />
                                <asp:CalendarExtender  ID="CalendarExtender2" runat="server" targetControlID="TextBoxTSToDate" Format="dd-MMM-yy" />
                            </td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td>
                                <table>
                                    <tr>
                                        <td rowspan="3"><img src="img/puce1.png" /></td>
                                        <td rowspan="3">
                                            <asp:CheckBox ID="CheckBoxBaseScope" runat="server" AutoPostBack="true"
                                                oncheckedchanged="CheckBoxBaseScope_CheckedChanged" Checked="true"/>Base Scope :</td>
                                        <td>
                                            <asp:DropDownList ID="ComboBoxWS" AutoPostBack="true" runat="server" Enabled="false" BackColor="Gray"
                                                onselectedindexchanged="ComboBoxWS_SelectedIndexChanged" Width="400px"/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:DropDownList ID="ComboBoxWP" runat="server" AutoPostBack="true" AppendDataBoundItems="true" BackColor="Gray"
                                                    enabled="false" onselectedindexchanged="ComboBoxWP_SelectedIndexChanged" Width="400px">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:DropDownList ID="ComboBoxCWP" runat="server" AutoPostBack="true" AppendDataBoundItems="true"
                                                    enabled="false" BackColor="Gray" Width="400px">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td BGCOLOR="#ffcc01" style="width:5px"><b>OR</b></td>
                            <td>
                                <table>
                                    <tr>
                                        <td rowspan="2"><img src="img/puce2.png" /></td>
                                        <td rowspan="2"><asp:CheckBox ID="CheckBoxVariation" runat="server" AutoPostBack="true"
                                                oncheckedchanged="CheckBoxVariation_CheckedChanged" Checked="true"/>Variation :</td>
                                        <td>
                                            <asp:DropDownList ID="ComboBoxVariationType" runat="server" AutoPostBack="true" Enabled="false"
                                                onselectedindexchanged="ComboBoxVariationType_SelectedIndexChanged"  Width="200px"
                                                BackColor="Gray">
                                                <asp:ListItem Selected="True" Value="select" Text="Select Variation Type" />
                                                <asp:ListItem>VNR</asp:ListItem>
                                                <asp:ListItem>VPR</asp:ListItem>
                                                <asp:ListItem>CSI</asp:ListItem>
                                                <asp:ListItem>RFI</asp:ListItem>
                                                <asp:ListItem>SDM</asp:ListItem>
                                                <asp:ListItem>NCR</asp:ListItem>
                                                <asp:ListItem>VP</asp:ListItem>
                                                <asp:ListItem>AGR</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:DropDownList ID="ComboBoxVariation" runat="server" AutoPostBack="true" AppendDataBoundItems="true"
                                                 Enabled="false" Width="350px" BackColor="Gray">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>

                    <div align="left">
                        &nbsp;&nbsp;<asp:Button ID="ButtonSearchByTSNumber" runat="server" Text="Find !" 
                                    onclick="ButtonSearch_Click" Font-Bold="True" />
                        &nbsp;&nbsp;<asp:Button ID="ButtonResetFilters" runat="server" 
                            Text="Reset Filters" onclick="ButtonResetFilters_Click" />
                    </div>

            </fieldset>

            <!--END : SEARCH FILTERS-->
            <asp:Label ID="LabelRecordFound" runat="server" />
        
            
          <br />
            <asp:GridView ID="GridView1" runat="server" class="gridview" AllowPaging="True" 
                AllowSorting="false"  PageSize="10000" 
                AlternatingRowStyle-BackColor="WhiteSmoke" AutoGenerateColumns="False" 
                DataKeyNames="TimesheetNumber" DataSourceID="SqlDataSourceTS" 
                EnableModelValidation="True" 
                ondatabound="GridView1_DataBound" onrowcommand="GridView1_RowCommand" 
                onrowdatabound="GridView1_RowDataBound"  >


<AlternatingRowStyle BackColor="WhiteSmoke"></AlternatingRowStyle>
                <Columns>
                <%-- 
                <asp:ButtonField  Text="View" CommandName="VIEW_TS"/>
                <asp:ButtonField Text="Cancel this TS" CommandName="CANCELLATION" ItemStyle-CssClass="deleteLink"/>
                <asp:ButtonField Text="Cancel and adjust this TS" CommandName="ADJUSTMENT" />
                <asp:CommandField SelectText="View TS" ShowSelectButton="True" />
                --%>
                <asp:TemplateField >
                    <ItemTemplate>
                        <asp:LinkButton ID="linkButtonView" Text="- View"  runat="server" CommandName="Select" Font-Underline="true" CommandArgument="VIEW_TS"/><br />
                        <asp:LinkButton ID="linkButtonCancel" Text="- Cancel this TS"  runat="server" CommandName="Select" CommandArgument="CANCELLATION"
                                        OnClientClick="show_prompt();" Font-Underline="true" /><br />
                        <asp:LinkButton ID="linkButtonAdjust" Text="- Cancel and adjust this TS"  runat="server" CommandName="Select" CommandArgument="ADJUSTMENT"
                                        OnClientClick="show_prompt();" Font-Underline="true" />
                    </ItemTemplate>                   
                </asp:TemplateField>
                    <asp:BoundField DataField="TimesheetNumber" HeaderText="Timesheet Number" 
                        ReadOnly="True" SortExpression="TimesheetNumber"/>
                    <asp:BoundField DataField="TimesheetDate" HeaderText="Timesheet Date" 
                        SortExpression="TimesheetDate" />
                    <asp:BoundField DataField="WSNumber" HeaderText="WS" 
                        SortExpression="WSNumber" />
                    <asp:BoundField DataField="WPNumber" HeaderText="WP" 
                        SortExpression="WPNumber" />
                    <asp:BoundField DataField="CWPNumber" HeaderText="CWP" 
                        SortExpression="CWPNumber" />
                    <asp:BoundField DataField="VariationNumber" HeaderText="Variation" 
                        SortExpression="VariationNumber" />
                    <asp:BoundField DataField="Discipline" HeaderText="Discipline" ReadOnly="True" 
                        SortExpression="Discipline" />
                    <asp:BoundField DataField="ApprovedBy" HeaderText="ApprovedBy" 
                        SortExpression="ApprovedBy" />
                    <asp:BoundField DataField="ApprovedDate" HeaderText="ApprovedDate" 
                        SortExpression="ApprovedDate" />
                    <asp:BoundField DataField="Cancelled" HeaderText="Cancelled" 
                        SortExpression="Cancelled" Visible="false"/>
                    <asp:BoundField DataField="PendingAdjustment" HeaderText="PendingAdjustment" 
                        SortExpression="PendingAdjustment" Visible="false"/>
                </Columns>


            </asp:GridView>



            <asp:SqlDataSource ID="SqlDataSourceTS" runat="server" 
                ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
                ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" >
                <SelectParameters>
                    <asp:SessionParameter Name="ContractNumber" SessionField="currentContract" 
                        Type="String" />
                </SelectParameters>
            </asp:SqlDataSource>

        </div>

        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ToolkitScriptManager>
    </form>

</body>
</html>
