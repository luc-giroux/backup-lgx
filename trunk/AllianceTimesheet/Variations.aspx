<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Variations.aspx.cs" Inherits="Variations" %>
<%@ Register TagPrefix="myTagPrefix" TagName="AppHeader" Src="PageHeader.ascx" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
    <title>Variations</title>
    <link href="site.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <myTagPrefix:AppHeader ID="Header1" runat="server"/><br />
        <div class="appcontent">
            <asp:Label ID="LabelAdd" runat="server"><a href="AddVariation.aspx">Add a new Variation</a></asp:Label><br /><br />
            <asp:GridView ID="GridView1" runat="server" class="gridview" PageSize="10000" 
                AutoGenerateColumns="False" DataKeyNames="VariationNumber,ContractNumber" 
                DataSourceID="SqlDataSourceVariations" EnableModelValidation="True" 
                AllowPaging="True" AllowSorting="True" AlternatingRowStyle-BackColor="WhiteSmoke"
                onselectedindexchanged="GridView1_SelectedIndexChanged">
                <Columns>
                    <asp:CommandField ShowSelectButton="True" SelectText="Edit" />
                    <asp:BoundField DataField="VariationNumber" HeaderText="VariationNumber" 
                        ReadOnly="True" SortExpression="VariationNumber" />
                    <asp:BoundField DataField="ContractNumber" HeaderText="ContractNumber" 
                        ReadOnly="True" SortExpression="ContractNumber" Visible="false"/>
                    <asp:BoundField DataField="WSNumber" HeaderText="WSNumber" 
                        SortExpression="WSNumber" ReadOnly="true"/>
                    <asp:BoundField DataField="WPNumber" HeaderText="WPNumber" 
                        SortExpression="WPNumber" ReadOnly="true"/>
                    <asp:BoundField DataField="CWPNumber" HeaderText="CWPNumber" 
                        SortExpression="CWPNumber" ReadOnly="true"/>
                    <asp:BoundField DataField="VariationType" HeaderText="VariationType" 
                        SortExpression="VariationType" ReadOnly="true" />
                    <asp:BoundField DataField="Discipline" HeaderText="Discipline" 
                        SortExpression="Discipline" />
                    <asp:BoundField DataField="Description" HeaderText="Description" 
                        SortExpression="Description" />
                    <asp:CheckBoxField DataField="Completed" HeaderText="Completed" 
                        SortExpression="Completed" />
                </Columns>
            </asp:GridView>

            <asp:SqlDataSource ID="SqlDataSourceVariations" runat="server" 
                ConflictDetection="OverwriteChanges" 
                ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
                ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" 
                SelectCommand="SELECT * FROM [Variation] WHERE ([ContractNumber] = ?) ORDER BY [VariationNumber], [VariationType]" 
                UpdateCommand="UPDATE [Variation] SET [Description] = ?, [Completed] = ? WHERE [VariationNumber] = ? AND [ContractNumber] = ? " > 

                <SelectParameters>
                    <asp:SessionParameter Name="ContractNumber" SessionField="currentContract" Type="String" />
                </SelectParameters>
                <UpdateParameters>
                    <asp:Parameter Name="Description" Type="String" />
                    <asp:Parameter Name="Completed" Type="Boolean" />
                    <asp:Parameter Name="VariationNumber" Type="String" />
                    <asp:SessionParameter Name="ContractNumber" SessionField="currentContract" Type="String" />

                </UpdateParameters>
            </asp:SqlDataSource>

            <asp:Label ID="LabelNoRecordFound" runat="server" Visible="false">
               No record found. 
            </asp:Label>
        </div>

    </form>
</body>
</html>
