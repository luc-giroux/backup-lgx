<%@ Assembly Name="Microsoft.SharePoint.ApplicationPages, Version=12.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c"%>
<%@ Page Language="C#" Inherits="Microsoft.SharePoint.ApplicationPages.LoginPage" MasterPageFile="~/_layouts/extranetLogin/login.master"%>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=12.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=12.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> <%@ Import Namespace="Microsoft.SharePoint" %>

<asp:Content ContentPlaceHolderId="PlaceHolderPageTitle" runat="server">
    <SharePoint:EncodedLiteral runat="server" text="<%$Resources:wss,login_pagetitle%>" EncodeMethod='HtmlEncode'/>
</asp:Content>
<asp:Content ContentPlaceHolderId="PlaceHolderTitleBreadcrumb" runat="server">
&nbsp;
</asp:Content>
<asp:Content ContentPlaceHolderId="PlaceHolderPageTitleInTitleArea" runat="server">
    <SharePoint:EncodedLiteral runat="server" text="<%$Resources:wss,login_pagetitle%>" EncodeMethod='HtmlEncode'/>
</asp:Content>
<asp:Content ContentPlaceHolderId="PlaceHolderSiteName" runat="server"/>
<asp:Content ContentPlaceHolderId="PlaceHolderMain" runat="server">
 <asp:login id="login" FailureText="<%$Resources:wss,login_pageFailureText%>" runat=server>
    <layouttemplate>
        <table id="tot" align="center">
			<tr>
				<td noWrap><SharePoint:EncodedLiteral runat="server" text="<%$Resources:wss,login_pageUserName%>" EncodeMethod='HtmlEncode'/></td>
				<td><asp:textbox id="UserName" autocomplete="off" runat=server class="ms-long"/></td>
			</tr>
			<tr>
				<td noWrap><SharePoint:EncodedLiteral runat="server" text="<%$Resources:wss,login_pagePassword%>" EncodeMethod='HtmlEncode'/></td>
				<td><asp:textbox id="password" TextMode=Password autocomplete="off" runat=server class="ms-long"/></td>
			</tr>
			<tr>
				<td>&nbsp;&nbsp;</td>
			</tr>
			<tr>
				<td></td>
				<td align=center><asp:button CssClass="glow" id=login commandname="Login" text="<%$Resources:wss,login_pagetitle%>" runat=server /></td>
			</tr>
			<tr>
				<%--<td colSpan=2><asp:CheckBox id=RememberMe text="<%$SPHtmlEncodedResources:wss,login_pageRememberMe%>" runat=server /></td>--%>  
			</tr>
        </table>
		</br></br>
		<asp:label id=FailureText  runat=server/>
    </layouttemplate>
 </asp:login>
</asp:Content>
