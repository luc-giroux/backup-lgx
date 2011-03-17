<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WP.aspx.cs" Inherits="WP" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register TagPrefix="myTagPrefix" TagName="AppHeader" Src="PageHeader.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>WP List</title>
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
            <asp:Label ID="LabelAdd" runat="server"><a href="AddWP.aspx">Add a new WP</a></asp:Label><br /><br />
            <asp:GridView ID="GridView1" runat="server" class="gridview" AllowPaging="True" 
                AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="WPNumber" 
                DataSourceID="SqlDataSourceWP" EnableModelValidation="True" 
                AlternatingRowStyle-BackColor="WhiteSmoke">
                <Columns>
                    <asp:CommandField ShowEditButton="True" />
                    <asp:BoundField DataField="WPNumber" HeaderText="WPNumber" ReadOnly="True" 
                        SortExpression="WPNumber" />
                    
                    <asp:BoundField DataField="Description" HeaderText="Description" 
                        SortExpression="Description" />
                    <asp:CheckBoxField DataField="Completed" HeaderText="Completed" 
                        SortExpression="Completed" />
                        <asp:BoundField DataField="ContractNumber" HeaderText="ContractNumber" 
                        SortExpression="ContractNumber" ItemStyle-CssClass="hiddencolumn" 
                        Visible="false" >
<ItemStyle CssClass="hiddencolumn"></ItemStyle>
                    </asp:BoundField>
                </Columns>
            </asp:GridView>

            <asp:SqlDataSource ID="SqlDataSourceWP" runat="server" 
                ConflictDetection="CompareAllValues" 
                ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
                DeleteCommand="DELETE FROM [WP] WHERE [WPNumber] = ? AND [ContractNumber] = ? AND [Description] = ? AND (([Completed] = ?) OR ([Completed] IS NULL AND ? IS NULL))" 
                InsertCommand="INSERT INTO [WP] ([WPNumber], [ContractNumber], [Description], [Completed]) VALUES (?, ?, ?, ?)" 
                OldValuesParameterFormatString="original_{0}" 
                ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" 
                SelectCommand="SELECT * FROM [WP] WHERE ([ContractNumber] = ?) ORDER BY [WPNumber]" 
                UpdateCommand="UPDATE [WP] SET [ContractNumber] = ?, [Description] = ?, [Completed] = ? WHERE [WPNumber] = ? AND [ContractNumber] = ? AND [Description] = ? AND (([Completed] = ?) OR ([Completed] IS NULL AND ? IS NULL))">
                <DeleteParameters>
                    <asp:Parameter Name="original_WPNumber" Type="String" />
                    <asp:SessionParameter Name="original_ContractNumber" SessionField="currentContract" Type="String" />
                    <asp:Parameter Name="original_Description" Type="String" />
                    <asp:Parameter Name="original_Completed" Type="Boolean" />
                    <asp:Parameter Name="original_Completed" Type="Boolean" />
                </DeleteParameters>
                <InsertParameters>
                    <asp:Parameter Name="WPNumber" Type="String" />
                    <asp:Parameter Name="ContractNumber" Type="String" />
                    <asp:Parameter Name="Description" Type="String" />
                    <asp:Parameter Name="Completed" Type="Boolean" />
                </InsertParameters>
                <SelectParameters>
                    <asp:SessionParameter Name="ContractNumber" SessionField="currentContract" Type="String" />
                </SelectParameters>
                <UpdateParameters>
                    <asp:SessionParameter Name="ContractNumber" SessionField="currentContract" Type="String" />
                    <asp:Parameter Name="Description" Type="String" />
                    <asp:Parameter Name="Completed" Type="Boolean" />
                    <asp:Parameter Name="original_WPNumber" Type="String" />
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
