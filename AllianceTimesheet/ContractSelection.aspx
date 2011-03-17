<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ContractSelection.aspx.cs" Inherits="ContractSelection" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <title>Contract Selection</title>
    <link href="site.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
    <br />
    Please select the contract you want to work on :
    <br /><br />
        <asp:GridView ID="GridViewContract" runat="server" AutoGenerateColumns="False" 
            DataSourceID="SqlDataSourceATS" EnableModelValidation="True" 
            onselectedindexchanged="GridViewContract_SelectedIndexChanged" 
            class="gridview" Width="316px">
            <Columns>
                <asp:CommandField ShowSelectButton="True" />
                <asp:BoundField DataField="contract" HeaderText="contract" 
                    SortExpression="contract" />
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSourceATS" runat="server" 
            ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
            ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" 
            SelectCommand="SELECT DISTINCT [contract] FROM [UserRights] WHERE ([userLogin] = ?)">
            <SelectParameters>
                <asp:SessionParameter Name="userLogin" SessionField="CurrentUser" Type="String" />
            </SelectParameters>
        </asp:SqlDataSource>
    </div>
    </form>
</body>
</html>
