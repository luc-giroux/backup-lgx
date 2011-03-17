<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Workers.aspx.cs" Inherits="Workers" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register TagPrefix="myTagPrefix" TagName="AppHeader" Src="PageHeader.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Workers List</title>
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
            <asp:Label ID="LabelCreateWorker" runat="server"><a href="AddWorker.aspx">Create a new worker</a></asp:Label> | <asp:LinkButton ID="LinkButtonAddWorkers" runat="server">Add existing workers</asp:LinkButton><br /><br />
            <asp:GridView ID="GridView1" runat="server" class="gridview" AllowPaging="True" 
                AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="WorkerId" 
                EnableModelValidation="True" PageSize="10000"
                DataSourceID="SqlDataSourceWorkers" AlternatingRowStyle-BackColor="WhiteSmoke"
                onselectedindexchanged="GridView1_SelectedIndexChanged">
                <Columns>
                    <asp:CommandField ShowSelectButton="True" SelectText="Edit" />
                    <asp:BoundField DataField="WorkerId" HeaderText="WorkerId" 
                        HeaderStyle-CssClass="hiddencolumn" ItemStyle-CssClass="hiddencolumn" SortExpression="WorkerId"/>
                    <asp:BoundField DataField="LastName" HeaderText="Last Name" 
                        SortExpression="LastName" />
                    <asp:BoundField DataField="FirstName" HeaderText="First Name" 
                        SortExpression="FirstName" />
                    <asp:BoundField DataField="BadgeNumber" HeaderText="Badge Number" 
                        SortExpression="BadgeNumber" />
                    <asp:BoundField DataField="VCCNumber" HeaderText="VCC Number" 
                        SortExpression="VCCNumber" />
                    <asp:BoundField DataField="Nationality" HeaderText="Nationality" 
                        SortExpression="Nationality" />
                    <asp:BoundField DataField="Trade" HeaderText="Trade" SortExpression="Trade" />
                    <asp:BoundField DataField="PositionCategory" HeaderText="Position Category" 
                        SortExpression="PositionCategory" />
                    <asp:BoundField DataField="ArrivalDate" HeaderText="Arrival Date" 
                        SortExpression="ArrivalDate" />
                    <asp:CheckBoxField DataField="InductionDone" HeaderText="Induction Done" 
                        SortExpression="InductionDone" />
                    <asp:CheckBoxField DataField="Active" HeaderText="Active" 
                        SortExpression="Active" />
                    
                </Columns>
            </asp:GridView>

            <asp:SqlDataSource ID="SqlDataSourceWorkers" runat="server" 
                ConflictDetection="CompareAllValues" 
                ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
                OldValuesParameterFormatString="original_{0}" 
                ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" 
                SelectCommand="SELECT W.WorkerId, LastName, FirstName, BadgeNumber, VCCNumber, 
                                Nationality, Trade, PositionCategory, Replace(Convert(varchar(9),ArrivalDate,6),' ','-') as ArrivalDate , InductionDone, Active 
                                FROM [Worker] W INNER JOIN [WorkerContract] WC ON W.WorkerId = WC.WorkerId 
                                WHERE (WC.[ContractNumber] = ?) ORDER BY [LastName], [FirstName]" >
                <SelectParameters>
                    <asp:SessionParameter Name="ContractNumber" SessionField="currentContract" Type="String" />
                </SelectParameters>
            </asp:SqlDataSource>


            <asp:Label ID="LabelNoRecordFound" runat="server" Visible="false">
               No record found. <a href="AddWorker.aspx">Add new record</a>
            </asp:Label>

        </div>


        <%-- Section for the modal popup of worker selection--%>
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" />
        <asp:ModalPopupExtender ID="MPE" runat="server"
                            TargetControlID="LinkButtonAddWorkers"
                            PopupControlID="PanelAddWorkers"
                            BackgroundCssClass="modalBackground" 
                            CancelControlID="ButtonModalCancel"
                            DropShadow="true" 
                            PopupDragHandleControlID="PanelCommentHeader" />

        <asp:Panel ID="PanelAddWorkers" runat="server" BackColor="White" ScrollBars="Vertical" Width="800px" Height="600px">
            <asp:Label ID="LabelNoExistingWorker" runat="server" Visible="false">
               No existing worker to add <br /><br />
            </asp:Label>
            <asp:GridView ID="GridViewAddWorkers" runat="server" class="gridview"  PageSize="10000"
                AlternatingRowStyle-BackColor="WhiteSmoke" AutoGenerateColumns="False" 
                DataKeyNames="WorkerId" DataSourceID="SqlDataSourceAddWorkers" 
                EnableModelValidation="True" 
                onrowdatabound="GridViewAddWorkers_RowDataBound">
                <AlternatingRowStyle BackColor="WhiteSmoke" />
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:CheckBox ID="CheckBoxWorkerSelect" runat="server"/>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="WorkerId" HeaderText="WorkerId"  HeaderStyle-CssClass="hiddencolumn"
                        InsertVisible="False" ReadOnly="True" SortExpression="WorkerId" ItemStyle-CssClass="hiddencolumn"/>
                    <asp:BoundField DataField="LastName" HeaderText="LastName" 
                        SortExpression="LastName" />
                    <asp:BoundField DataField="FirstName" HeaderText="FirstName" 
                        SortExpression="FirstName" />
                    <asp:BoundField DataField="BadgeNumber" HeaderText="BadgeNumber" 
                        SortExpression="BadgeNumber" />
                    <asp:TemplateField HeaderText="Trade">
                        <ItemTemplate>
                            <asp:DropDownList ID="DropDownListTrade" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Position Category">
                        <ItemTemplate>
                            <asp:DropDownList ID="DropDownListPositionCategory" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            
            <asp:SqlDataSource ID="SqlDataSourceAddWorkers" runat="server" 
                ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
                ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" 
                selectCommand="SELECT w.WorkerId,w.LastName,w.FirstName,w.BadgeNumber
                                FROM [Worker] w     
                                INNER JOIN [Company] c1 ON c1.[CompanyName] = w.[CompanyName]
                                INNER JOIN [Contract] ON [Contract].[CompanyName] = c1.CompanyName
                                WHERE [Contract].ContractNumber =  ?
                                AND  NOT EXISTS (SELECT 1 FROM [WorkerContract] wc  
                                         WHERE w.WorkerId = wc.workerId
                                         AND wc.ContractNumber =  [Contract].ContractNumber )">                
                <SelectParameters>
                    <asp:SessionParameter Name="ContractNumber" SessionField="currentContract" Type="String" />
                </SelectParameters>
            </asp:SqlDataSource>
            
            <div align="center">
                <asp:Button ID="ButtonAddSelectedWorkers" runat="server" Text="Add" 
                    onclick="ButtonAddSelectedWorkers_Click" />
                <asp:Button ID="ButtonModalCancel" runat="server" Text="Cancel" />
            </div>
        </asp:Panel>


    </form>

</body>
</html>
