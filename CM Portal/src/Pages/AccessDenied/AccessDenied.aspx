<%@ Assembly Name="Microsoft.SharePoint.ApplicationPages, Version=12.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c"%> <%@ Page Language="C#" Inherits="Microsoft.SharePoint.ApplicationPages.AccessDeniedPage" MasterPageFile="~/_layouts/accesDenied.master"      %> <%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %> <%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=12.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> <%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=12.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> <%@ Import Namespace="Microsoft.SharePoint" %>
<asp:Content ContentPlaceHolderId="PlaceHolderPageTitle" runat="server">
	<SharePoint:EncodedLiteral runat="server" text="<%$Resources:wss,accessDenied_pagetitle%>" EncodeMethod='HtmlEncode'/>
</asp:Content>
<asp:Content ContentPlaceHolderId="PlaceHolderPageTitleInTitleArea" runat="server">
	<SharePoint:EncodedLiteral runat="server" text="<%$Resources:wss,accessDenied_pagetitle%>" EncodeMethod='HtmlEncode'/>
</asp:Content>
<asp:Content contentplaceholderid="PlaceHolderPageImage" runat="server">
	<img id="onetidtpweb1" src="/_layouts/images/error.gif" alt="" />
</asp:Content>
<asp:Content contentplaceholderid="PlaceHolderAdditionalPageHead" runat="server">
	<meta name="Robots" content="NOINDEX " />
</asp:Content>
<asp:Content ContentPlaceHolderId="PlaceHolderMain" runat="server">
<div align="center">
 <table align=center border=0 cellpadding=0 style="background-color: white;font-family: bold 12px Arial , Gadget, sans-serif;">
	 <TR>
		<TD valign=top ><SharePoint:EncodedLiteral runat="server" text="<%$Resources:wss,accessDenied_loggedInAs%>" EncodeMethod='HtmlEncode'/>
			&nbsp;<b><asp:Label id="LabelUserName" runat="server"/></b>
		</TD>
	 </TR>
	 <TR>
		<TD>&nbsp;</TD>
	 </TR>
	 <TR>
		 <TD>
			<table width="100%">
				<tr>
					<td align="left">
						<a href="http://cm.projetkoniambo.com">Go back to site</a>
					<td>
					<TD align="right">
						<asp:HyperLink id="HLinkLoginAsAnother" Text="<%$SPHtmlEncodedResources:wss,accessDenied_logInAsAnotherOne%>"
							 runat="server"/>
						<br>
						<asp:HyperLink id="HLinkRequestAccess" Text="<%$SPHtmlEncodedResources:wss,accessDenied_requestAccess%>"
							 runat="server"/>
					</TD>
				</tr>
			</table>
		</td>	
	 </TR>
 </table>
 </div>
</asp:Content>
