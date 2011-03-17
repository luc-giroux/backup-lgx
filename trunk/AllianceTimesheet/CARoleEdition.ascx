<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CARoleEdition.ascx.cs" Inherits="CARoleEdition" %>



<asp:GridView ID="GridView1" runat="server" class="gridview" PageSize="10000"
    AlternatingRowStyle-BackColor="WhiteSmoke" AutoGenerateColumns="False" 
    DataKeyNames="userLogin,role,contract" DataSourceID="SqlDataSourceUSerRights" 
    EnableModelValidation="True" AllowPaging="True" AllowSorting="True" 
    Visible="False">
<AlternatingRowStyle BackColor="WhiteSmoke"></AlternatingRowStyle>
    <Columns>
        <asp:CommandField ShowDeleteButton="True" />
        <asp:BoundField DataField="userLogin" HeaderText="userLogin" ReadOnly="True" 
            SortExpression="userLogin" Visible="false"/>
        <asp:BoundField DataField="role" HeaderText="role" ReadOnly="True" 
            SortExpression="role" />
        <asp:BoundField DataField="contract" HeaderText="contract" ReadOnly="True" 
            SortExpression="contract" Visible="false"/>
    </Columns>
</asp:GridView>
<asp:SqlDataSource ID="SqlDataSourceUSerRights" runat="server" 
    ConflictDetection="OverwriteChanges" 
    ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
    DeleteCommand="DELETE FROM [UserRights] WHERE [userLogin] = ? AND [role] = ? AND [contract] = ?" 
    InsertCommand="INSERT INTO [UserRights] ([userLogin], [role], [contract]) VALUES (?, ?, ?)" 
    OldValuesParameterFormatString="original_{0}" 
    ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" 
    SelectCommand="SELECT * FROM [UserRights] WHERE (([contract] = ?) AND ([userLogin] = ?)) ORDER BY [role]">
    <DeleteParameters>
        <asp:SessionParameter Name="original_userLogin" SessionField="currentUserLogin" Type="String" />
        <asp:Parameter Name="original_role" Type="String" />
        <asp:SessionParameter Name="original_contract" SessionField="currentContract" Type="String" />
    </DeleteParameters>
    <InsertParameters>
        <asp:Parameter Name="userLogin" Type="String" />
        <asp:Parameter Name="role" Type="String" />
        <asp:Parameter Name="contract" Type="String" />
    </InsertParameters>
    <SelectParameters>
        <asp:SessionParameter Name="contract" SessionField="currentContract" Type="String" />
        <asp:SessionParameter Name="userLogin" SessionField="currentUserLogin" Type="String" />
    </SelectParameters>
</asp:SqlDataSource>

<asp:Label ID="LabelNoRecordFound" runat="server" Text="Label" Visible="false">No Record Found</asp:Label>
