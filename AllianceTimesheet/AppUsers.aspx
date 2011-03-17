<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AppUsers.aspx.cs" Inherits="AppUsers" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register TagPrefix="myTagPrefix" TagName="AppHeader" Src="PageHeader.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Application Users List</title>
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
    <!--Redirect to Session expired page after 20min from the moment the page is loaded-->
    <meta http-equiv="refresh" content="1200; URL=SessionExpired.aspx">
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

            <b><asp:Label ID="LabelOwnerSupervisors" runat="server" Text="Label" Visible="false"></asp:Label></b>
           <b><asp:Label ID="LabelContractorsSupervisors" runat="server" Text="Label" Visible="false"></asp:Label></b>

            <asp:GridView ID="GridView1" runat="server" class="gridview" PageSize="10000" 
                AlternatingRowStyle-BackColor="WhiteSmoke" AllowPaging="True" 
                AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="userLogin" 
                DataSourceID="SqlDataSourceAppUser" EnableModelValidation="True" 
                onselectedindexchanged="GridView1_SelectedIndexChanged">

<AlternatingRowStyle BackColor="WhiteSmoke"></AlternatingRowStyle>
                <Columns>
                    <asp:CommandField SelectText="View Roles" ShowDeleteButton="True" 
                        ShowEditButton="True" ShowSelectButton="True" />
                    <asp:BoundField DataField="userLogin" HeaderText="userLogin" ReadOnly="True" 
                        SortExpression="userLogin" />
                    <asp:BoundField DataField="LastName" HeaderText="LastName" 
                        SortExpression="LastName" />
                    <asp:BoundField DataField="FirstName" HeaderText="FirstName" 
                        SortExpression="FirstName" />
                    <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
                </Columns>

            </asp:GridView>



            <asp:SqlDataSource ID="SqlDataSourceAppUser" runat="server" 
                ConflictDetection="CompareAllValues" 
                ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
                DeleteCommand="DELETE FROM [AppUser] WHERE [userLogin] = ? AND [LastName] = ? AND [FirstName] = ? AND [Email] = ?" 
                InsertCommand="INSERT INTO [AppUser] ([userLogin], [LastName], [FirstName], [Email]) VALUES (?, ?, ?, ?)" 
                OldValuesParameterFormatString="original_{0}" 
                ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" 
                SelectCommand="SELECT * FROM [AppUser] ORDER BY [LastName], [FirstName]" 
                UpdateCommand="UPDATE [AppUser] SET [LastName] = ?, [FirstName] = ?, [Email] = ? WHERE [userLogin] = ? AND [LastName] = ? AND [FirstName] = ? AND [Email] = ?">
                <DeleteParameters>
                    <asp:Parameter Name="original_userLogin" Type="String" />
                    <asp:Parameter Name="original_LastName" Type="String" />
                    <asp:Parameter Name="original_FirstName" Type="String" />
                    <asp:Parameter Name="original_Email" Type="String" />
                </DeleteParameters>
                <InsertParameters>
                    <asp:Parameter Name="userLogin" Type="String" />
                    <asp:Parameter Name="LastName" Type="String" />
                    <asp:Parameter Name="FirstName" Type="String" />
                    <asp:Parameter Name="Email" Type="String" />
                </InsertParameters>
                <UpdateParameters>
                    <asp:Parameter Name="LastName" Type="String" />
                    <asp:Parameter Name="FirstName" Type="String" />
                    <asp:Parameter Name="Email" Type="String" />
                    <asp:Parameter Name="original_userLogin" Type="String" />
                    <asp:Parameter Name="original_LastName" Type="String" />
                    <asp:Parameter Name="original_FirstName" Type="String" />
                    <asp:Parameter Name="original_Email" Type="String" />
                </UpdateParameters>
            </asp:SqlDataSource>



            <asp:Label ID="LabelNoRecordFound" runat="server" Visible="false">
               No record found. 
            </asp:Label>

        </div>
    </form>

</body>
</html>
