<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PositionCategories.aspx.cs" Inherits="PositionCategories" %>
<%@ Register TagPrefix="myTagPrefix" TagName="AppHeader" Src="PageHeader.ascx" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
    <title>Position Categories</title>
    <link href="site.css" rel="stylesheet" type="text/css" />
</head>
<body>
    
    <form id="form1" runat="server">
        <myTagPrefix:AppHeader ID="Header1" runat="server"/><br />
        <div class="appcontent">
            <asp:Label ID="LabelAdd" runat="server"><a href="AddPositionCategory.aspx">Add a new position category</a></asp:Label> <br /><br />
            <asp:GridView ID="GridViewPositionCategories" runat="server" AllowPaging="True" 
                AllowSorting="True" AlternatingRowStyle-BackColor="WhiteSmoke"
                EnableModelValidation="True" PageSize="10000" class="gridview" 
                AutoGenerateColumns="False" DataSourceID="SqlDataSourcePositionCategory">

                <AlternatingRowStyle BackColor="WhiteSmoke"></AlternatingRowStyle>
                <Columns>
                    <asp:BoundField DataField="PositionCategory" HeaderText="Position Category" SortExpression="PositionCategory" />
                </Columns>

            </asp:GridView>


            <asp:SqlDataSource ID="SqlDataSourcePositionCategory" runat="server" 
                ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
                ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" 
                SelectCommand="SELECT [PositionCategory] FROM [PositionCategory] WHERE ([ContractNumber] = ?)">
                <SelectParameters>
                    <asp:SessionParameter Name="ContractNumber" SessionField="currentContract" 
                        Type="String" />
                </SelectParameters>
            </asp:SqlDataSource>


            <asp:Label ID="LabelNoRecordFound" runat="server" Visible="false">
               No record found.
            </asp:Label>
        </div>

    </form>
</body>
</html>
