<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WS.aspx.cs" Inherits="WS" %>
<%@ Register TagPrefix="myTagPrefix" TagName="AppHeader" Src="PageHeader.ascx" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
    <title>WS</title>
    <link href="site.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <myTagPrefix:AppHeader ID="Header1" runat="server"/><br />
        <div class="appcontent">
            <asp:Label ID="LabelAdd" runat="server"><a href="AddWS.aspx">Add a new WS</a></asp:Label> <br /><br />
            <asp:GridView ID="GridViewWS" runat="server" AllowPaging="True" 
                AllowSorting="True" AutoGenerateColumns="False" AlternatingRowStyle-BackColor="WhiteSmoke"
                DataKeyNames="WSNumber,ContractNumber" DataSourceID="SqlDataSourceWS" 
                EnableModelValidation="True" PageSize="10000" class="gridview">
                <Columns>
                    <asp:CommandField ShowEditButton="True" />
                    <asp:BoundField DataField="WSNumber" HeaderText="WSNumber" ReadOnly="True" 
                        SortExpression="WSNumber" />
                    <asp:BoundField DataField="ContractNumber" HeaderText="ContractNumber" 
                        ReadOnly="True" SortExpression="ContractNumber" visible="false"/>
                    <asp:BoundField DataField="Description" HeaderText="Description" 
                        SortExpression="Description" />
                    <asp:CheckBoxField DataField="Completed" HeaderText="Completed" 
                        SortExpression="Completed" />
                </Columns>
            </asp:GridView>
            <asp:SqlDataSource ID="SqlDataSourceWS" runat="server" 
                ConflictDetection="CompareAllValues" 
                ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
                DeleteCommand="DELETE FROM [WS] WHERE [WSNumber] = ? AND [ContractNumber] = ? AND [Description] = ? AND (([Completed] = ?) OR ([Completed] IS NULL AND ? IS NULL))" 
                InsertCommand="INSERT INTO [WS] ([WSNumber], [ContractNumber], [Description], [Completed]) VALUES (?, ?, ?, ?)" 
                OldValuesParameterFormatString="original_{0}" 
                ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" 
                SelectCommand="SELECT * FROM [WS] WHERE ([ContractNumber] = ?) ORDER BY [WSNumber]" 
                
                UpdateCommand="UPDATE [WS] SET [Description] = ?, [Completed] = ? WHERE [WSNumber] = ? AND [ContractNumber] = ? AND [Description] = ? AND (([Completed] = ?) OR ([Completed] IS NULL AND ? IS NULL))">
                <DeleteParameters>
                    <asp:Parameter Name="original_WSNumber" Type="String" />
                    <asp:SessionParameter Name="original_ContractNumber" SessionField="currentContract" Type="String" />
                    <asp:Parameter Name="original_Description" Type="String" />
                    <asp:Parameter Name="original_Completed" Type="Boolean" />
                    <asp:Parameter Name="original_Completed" Type="Boolean" />
                </DeleteParameters>
                <InsertParameters>
                    <asp:Parameter Name="WSNumber" Type="String" />
                    <asp:Parameter Name="ContractNumber" Type="String" />
                    <asp:Parameter Name="Description" Type="String" />
                    <asp:Parameter Name="Completed" Type="Boolean" />
                </InsertParameters>
                <SelectParameters>
                    <asp:SessionParameter Name="ContractNumber" SessionField="currentContract" Type="String" />
                </SelectParameters>
                <UpdateParameters>
                    <asp:Parameter Name="Description" Type="String" />
                    <asp:Parameter Name="Completed" Type="Boolean" />
                    <asp:Parameter Name="original_WSNumber" Type="String" />
                    <asp:SessionParameter Name="original_ContractNumber" SessionField="currentContract" Type="String" />
                    <asp:Parameter Name="original_Description" Type="String" />
                    <asp:Parameter Name="original_Completed" Type="Boolean" />
                    <asp:Parameter Name="original_Completed" Type="Boolean" />
                </UpdateParameters>
            </asp:SqlDataSource>

            <asp:Label ID="LabelNoRecordFound" runat="server" Visible="false">
               No record found. 
            </asp:Label>
        </div>

    </form>
</body>
</html>
