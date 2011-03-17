<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AdminRoleEdition.ascx.cs" Inherits="AdminRoleEdition" %>


<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" PageSize="10000"
    DataKeyNames="userLogin,role,contract" DataSourceID="SqlDataSourceUserRights" 
    EnableModelValidation="True" class="gridview" 
    AlternatingRowStyle-BackColor="WhiteSmoke" AllowPaging="True" 
    AllowSorting="True" Visible="False">
<AlternatingRowStyle BackColor="WhiteSmoke"></AlternatingRowStyle>
    <Columns>
        <asp:CommandField ShowDeleteButton="True" />
        <asp:BoundField DataField="userLogin" HeaderText="userLogin" ReadOnly="True" 
            SortExpression="userLogin" Visible="false" />
        <asp:BoundField DataField="role" HeaderText="role" ReadOnly="True" 
            SortExpression="role" />
        <asp:BoundField DataField="contract" HeaderText="contract" ReadOnly="True" 
            SortExpression="contract" />
    </Columns>
</asp:GridView>
<asp:SqlDataSource ID="SqlDataSourceUserRights" runat="server" 
    ConflictDetection="OverwriteChanges" 
    ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
    DeleteCommand="DELETE FROM [UserRights] WHERE [userLogin] = ? AND [role] = ? AND [contract] = ?" 
    InsertCommand="INSERT INTO [UserRights] ([userLogin], [role], [contract]) VALUES (?, ?, ?)" 
    OldValuesParameterFormatString="original_{0}" 
    ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" 
    SelectCommand="SELECT * FROM [UserRights] WHERE ([userLogin] = ?) ORDER BY [userLogin], [contract]">
    <DeleteParameters>
        <asp:SessionParameter Name="original_userLogin" SessionField="currentUserLogin" Type="String" />
        <asp:Parameter Name="original_role" Type="String" />
        <asp:Parameter Name="original_contract" Type="String" />
    </DeleteParameters>
    <InsertParameters>
        <asp:Parameter Name="userLogin" Type="String" />
        <asp:Parameter Name="role" Type="String" />
        <asp:Parameter Name="contract" Type="String" />
    </InsertParameters>
    <SelectParameters>
        <asp:SessionParameter Name="userLogin" SessionField="currentUserLogin" Type="String" />
    </SelectParameters>
</asp:SqlDataSource>

<asp:Label ID="LabelNoRecordFound" runat="server" Text="Label" Visible="false">No Record Found</asp:Label>
