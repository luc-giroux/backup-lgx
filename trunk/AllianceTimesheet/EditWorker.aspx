<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EditWorker.aspx.cs" Inherits="EditWorker" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register TagPrefix="myTagPrefix" TagName="AppHeader" Src="PageHeader.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Edit Worker</title>
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
                <legend>Edit Worker <i><%=(((Worker)HttpContext.Current.Session["currentWorker"]).LastName) %>&nbsp;
                                    <%=(((Worker)HttpContext.Current.Session["currentWorker"]).FirstName) %>&nbsp;</i></legend>
                <ol>
                <li>
                    <label for="Trade">Trade<em>*</em></label>
                    <asp:ComboBox ID="ComboBoxTrade" runat="server" 
                        AutoCompleteMode="SuggestAppend" CaseSensitive="False"
                        CssClass="AquaStyle" DropDownStyle="DropDownList" 
                        AppendDataBoundItems="true" >
                    </asp:ComboBox>
                </li>
                <li>
                    <label for="PositionCategory">Position Category<em>*</em></label>
                    <asp:ComboBox ID="ComboBoxPositionCategory" runat="server" 
                        AutoCompleteMode="SuggestAppend" CaseSensitive="False"
                        CssClass="AquaStyle" DropDownStyle="DropDownList" 
                        AppendDataBoundItems="true" >
                    </asp:ComboBox>
                </li>
                <li>
                    <label for="BadgeNumber">Badge Number<em>*</em></label>
                    <asp:TextBox ID="TextBoxBadgeNumber" runat="server" ></asp:TextBox>
                </li>
                <li>
                    <label for="Active">Active<em>*</em></label>
                    <asp:CheckBox ID="CheckBoxActive" runat="server" />
                </li>
                <li>
                    <label for="blank"></label>
                    <asp:Button ID="ButtonSubmit" runat="server" Text="Submit" 
                        onclick="ButtonSubmit_Click" />
                </li>
                </ol>
            </fieldset><br /><br />
            <fieldset>
                <legend>Modifications already made on <i><%=(((Worker)HttpContext.Current.Session["currentWorker"]).LastName) %>&nbsp;
                                    <%=(((Worker)HttpContext.Current.Session["currentWorker"]).FirstName) %>&nbsp;</i></legend>

                <asp:GridView ID="GridView1" runat="server" PageSize="10000" class="gridview"
                              AllowSorting="True" AutoGenerateColumns="False" 
                              AlternatingRowStyle-BackColor="WhiteSmoke" 
                    DataKeyNames="WorkerHistoryID" DataSourceID="SqlDataSourceWorkerHistory" 
                    EnableModelValidation="True">
                    <AlternatingRowStyle BackColor="WhiteSmoke" />
                    <Columns>
                        <asp:BoundField DataField="Trade" HeaderText="Trade" SortExpression="Trade" />
                        <asp:BoundField DataField="PositionCategory" HeaderText="Position Category" 
                            SortExpression="PositionCategory" />
                        <asp:BoundField DataField="FromDate" HeaderText="From Date" 
                            SortExpression="FromDate" />
                        <asp:BoundField DataField="ToDate" HeaderText="To Date" 
                            SortExpression="ToDate" />
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSourceWorkerHistory" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
                    ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" 
                    
                    SelectCommand="SELECT [Trade], [PositionCategory], [FromDate], [ToDate], [WorkerHistoryID] FROM [WorkerContractHistory] 
                                   WHERE (([ContractNumber] = ?) AND ([WorkerID] = ?)) ORDER BY [WorkerHistoryID] DESC" 
                    onselecting="SqlDataSourceWorkerHistory_Selecting">
                    <SelectParameters>
                        <asp:SessionParameter Name="ContractNumber" SessionField="currentContract" 
                            Type="String" />
                        <asp:Parameter Name="WorkerID" Type="Int32" />
                    </SelectParameters>
                </asp:SqlDataSource>

                <asp:Label ID="LabelNoRecordFound" runat="server" Visible="false">
                    No record found. 
                </asp:Label>

            </fieldset>
        </div>

        <%-- to write information into input fields --%>
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" />
            


        <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="TextBoxBadgeNumber" 
            WatermarkCssClass="watermarked" WatermarkText="This field is mandatory"></asp:TextBoxWatermarkExtender> 
        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="TextBoxBadgeNumber" FilterType="Numbers" /> 

    </form>

</body>
</html>
