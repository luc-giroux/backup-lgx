<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CWP.aspx.cs" Inherits="CWP" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register TagPrefix="myTagPrefix" TagName="AppHeader" Src="PageHeader.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>CWP List</title>
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
            <asp:Label ID="LabelAdd" runat="server"><a href="AddCWP.aspx">Add a new CWP</a></asp:Label><br /><br />
            <asp:GridView ID="GridView1" runat="server" class="gridview" AllowPaging="True" 
                AllowSorting="True" AutoGenerateColumns="False" PageSize="10000"
                DataKeyNames="CWPNumber,ContractNumber" DataSourceID="SqlDataSourceCWP" 
                EnableModelValidation="True" AlternatingRowStyle-BackColor="WhiteSmoke">
                <Columns>
                    <asp:CommandField ShowEditButton="True" />
                    <asp:BoundField DataField="CWPNumber" HeaderText="CWPNumber" ReadOnly="True" 
                        SortExpression="CWPNumber" />
                    <asp:BoundField DataField="ContractNumber" HeaderText="ContractNumber" 
                        ReadOnly="True" SortExpression="ContractNumber" Visible="false"/>
                    <asp:BoundField DataField="Description" HeaderText="Description" 
                        SortExpression="Description" />
                    <asp:BoundField DataField="Discipline" HeaderText="Discipline" 
                        SortExpression="Discipline" ReadOnly="true"/>
                    <asp:CheckBoxField DataField="Completed" HeaderText="Completed" 
                        SortExpression="Completed" />
                </Columns>

            </asp:GridView>
            <asp:SqlDataSource ID="SqlDataSourceCWP" runat="server" 
                ConflictDetection="CompareAllValues" 
                ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
                OldValuesParameterFormatString="original_{0}" 
                ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" 
                SelectCommand="SELECT CWPNumber, ContractNumber, Description, Discipline, Completed FROM [CWP] WHERE ([ContractNumber] = ?) ORDER BY [CWPNumber]" 
                UpdateCommand="UPDATE [CWP] SET [Description] = ?, [Completed] = ? WHERE [CWPNumber] = ? AND [ContractNumber] = ? AND [Description] = ? AND [Discipline] = ? AND [Completed] = ?">
                <SelectParameters>
                    <asp:SessionParameter Name="ContractNumber" SessionField="currentContract" Type="String" />
                </SelectParameters>
                <UpdateParameters>
                    <asp:Parameter Name="Description" Type="String" />
                    <asp:Parameter Name="Completed" Type="Boolean" />
                    <asp:Parameter Name="original_CWPNumber" Type="String" />
                    <asp:SessionParameter Name="original_ContractNumber" SessionField="currentContract" Type="String" />
                    <asp:Parameter Name="original_Description" Type="String" />
                    <asp:Parameter Name="original_Discipline" Type="String" />
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
