<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Subcontractors.aspx.cs" Inherits="Subcontractors" %>

<%@ Register TagPrefix="myTagPrefix" TagName="AppHeader" Src="PageHeader.ascx" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
    <title>Sucontractors</title>
    <link href="site.css" rel="stylesheet" type="text/css" />
</head>
<body>
    
    <form id="form1" runat="server">
        <myTagPrefix:AppHeader ID="Header1" runat="server"/><br />
        <div class="appcontent">

            <asp:GridView ID="GridViewSubcontractors" runat="server" class="gridview" 
                AutoGenerateColumns="False" DataKeyNames="SubcontractorName,ContractNumber" 
                DataSourceID="SqlDataSourceSubcontractors" EnableModelValidation="True" 
                AllowPaging="True" AllowSorting="True" PageSize="10000" AlternatingRowStyle-BackColor="WhiteSmoke">
                <Columns>
                    <asp:CommandField ShowEditButton="True" />
                    <asp:BoundField DataField="SubcontractorName" HeaderText="SubcontractorName" 
                        ReadOnly="True" SortExpression="SubcontractorName" />
                    <asp:BoundField DataField="ContractNumber" HeaderText="ContractNumber" 
                        ReadOnly="True" SortExpression="ContractNumber" Visible="false"/>
                    <asp:CheckBoxField DataField="Active" HeaderText="Active" 
                        SortExpression="Active" />
                </Columns>
            </asp:GridView>

            <asp:SqlDataSource ID="SqlDataSourceSubcontractors" runat="server" 
                ConflictDetection="CompareAllValues" 
                ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
                DeleteCommand="DELETE FROM [Subcontractor] WHERE [SubcontractorName] = ? AND [ContractNumber] = ? AND [Active] = ?" 
                InsertCommand="INSERT INTO [Subcontractor] ([SubcontractorName], [ContractNumber], [Active]) VALUES (?, ?, ?)" 
                OldValuesParameterFormatString="original_{0}" 
                ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" 
                SelectCommand="SELECT * FROM [Subcontractor] WHERE ([ContractNumber] = ?) ORDER BY [SubcontractorName]" 
                
                UpdateCommand="UPDATE [Subcontractor] SET [Active] = ? WHERE [SubcontractorName] = ? AND [ContractNumber] = ? AND [Active] = ?">
                <DeleteParameters>
                    <asp:Parameter Name="original_SubcontractorName" Type="String" />
                    <asp:Parameter Name="original_ContractNumber" Type="String" />
                    <asp:Parameter Name="original_Active" Type="Boolean" />
                </DeleteParameters>
                <InsertParameters>
                    <asp:Parameter Name="SubcontractorName" Type="String" />
                    <asp:Parameter Name="ContractNumber" Type="String" />
                    <asp:Parameter Name="Active" Type="Boolean" />
                </InsertParameters>
                <SelectParameters>
                    <asp:SessionParameter Name="ContractNumber" SessionField="currentContract" Type="String" />
                </SelectParameters>
                <UpdateParameters>
                    <asp:Parameter Name="Active" Type="Boolean" />
                    <asp:Parameter Name="original_SubcontractorName" Type="String" />
                    <asp:SessionParameter Name="original_ContractNumber" SessionField="currentContract" Type="String" />
                    <asp:Parameter Name="original_Active" Type="Boolean" />
                </UpdateParameters>
            </asp:SqlDataSource>

            <asp:Label ID="LabelNoRecordFound" runat="server" Visible="false">
               No record found. <a href="AddSubcontractor.aspx">Add new record</a>
            </asp:Label>
        </div>

    </form>
</body>
</html>
