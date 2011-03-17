<%@ Reference Page="~/ErrMsg.aspx" %>
<%@ Page Language="vb" AutoEventWireup="false" Inherits="TimeSheetV190_HTJV.ts_ctrcode" culture="en-US" CodeFile="ts_ctrcode.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>CTR/Activity Code Selection</title>
		<script language="javascript">
			self.setTimeout("self.close()",60000);
		</script>
		<meta name="GENERATOR" content="Microsoft Visual Studio.NET 7.0">
		<meta name="CODE_LANGUAGE" content="Visual Basic 7.0">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<INPUT id="Control" style="Z-INDEX: 101; LEFT: 482px; POSITION: absolute; TOP: 8px" type="hidden" name="Control" runat="server">
			<TABLE id="Table1" style="Z-INDEX: 102; LEFT: 0px; POSITION: absolute; TOP: 0px" cellSpacing="1" cellPadding="1" width="429" border="0">
				<TBODY>
					<TR>
						<TD noWrap>
							<asp:label id="Label1" runat="server" Font-Size="XX-Small" Font-Names="Arial">Activity Code / Description</asp:label></TD>
						<TD noWrap>
							<asp:textbox id="txtSearch" runat="server" Font-Size="XX-Small" Font-Names="Arial"></asp:textbox></TD>
						<TD noWrap>
							<asp:button id="cmdGo" runat="server" Font-Size="XX-Small" Font-Names="Arial" Text="Search" Width="50px"></asp:button></TD>
						<TD noWrap>
							<asp:button id="cmdShowAll" runat="server" Font-Size="XX-Small" Font-Names="Arial" Text="All" Width="50px"></asp:button></TD>
					</TR>
					<TR>
						<TD noWrap></TD>
						<TD noWrap></TD>
						<TD noWrap align="middle" colSpan="2">
							<asp:Label id="lblPageCount" runat="server" Font-Size="XX-Small" Font-Names="Arial"></asp:Label></TD>
					</TR>
					<TR>
						<TD noWrap colSpan="4">
							<asp:label id="errMsg" runat="server" Font-Size="XX-Small" Font-Names="Arial" Font-Bold="True" ForeColor="Red"></asp:label></TD>
					</TR>
					<TR>
						<TD noWrap colSpan="4">
							<DIV style="OVERFLOW: auto">
								<asp:datagrid id="dgVwTSCTR" tabIndex=3 runat="server" Font-Size="XX-Small" Font-Names="Arial" 
								    Width="100%" OnSelectedIndexChanged="Change_Causes" PageSize="25" BackColor="White" 
								    BorderColor="#3366CC" BorderStyle="Solid" AutoGenerateColumns="False" 
								    DataMember="CTRCode" DataKeyField="CTRCode" 
								    CellPadding="0" BorderWidth="1px" 
								    DataSource="<%# DS_CTRCode %>" 
								    HorizontalAlign="Center" OnItemDataBound="dgVwTSCTR_ItemDataBound" AllowPaging="True">
									<SelectedItemStyle Font-Size="XX-Small" Font-Names="Arial" Font-Bold="True" ForeColor="#CCFF99" BackColor="#009999"></SelectedItemStyle>
									<EditItemStyle Font-Size="XX-Small" Font-Names="Arial"></EditItemStyle>
									<AlternatingItemStyle Font-Size="XX-Small" Font-Names="Arial" Wrap="False" ForeColor="#003399" BackColor="White"></AlternatingItemStyle>
									<ItemStyle Font-Size="XX-Small" Font-Names="Arial" Wrap="False" ForeColor="#003399" BackColor="White"></ItemStyle>
									<HeaderStyle Font-Size="XX-Small" Font-Names="Arial" Font-Bold="True" Wrap="False" HorizontalAlign="Center" ForeColor="White" VerticalAlign="Middle" BackColor="#3366CC"></HeaderStyle>
									<FooterStyle Font-Size="XX-Small" Font-Names="Arial" Wrap="False" HorizontalAlign="Center" ForeColor="#003399" VerticalAlign="Top" BackColor="#99CCCC"></FooterStyle>
									<Columns>
										<asp:BoundColumn DataField="CTRCode" SortExpression="CTRCode" HeaderText="Task Code">
											<HeaderStyle Font-Underline="True"></HeaderStyle>
											<ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
										</asp:BoundColumn>
										<asp:BoundColumn DataField="Description" SortExpression="Description" HeaderText="Description">
											<HeaderStyle Font-Underline="True"></HeaderStyle>
										</asp:BoundColumn>
										<asp:ButtonColumn Text="Select" CommandName="Select">
											<ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
										</asp:ButtonColumn>
									</Columns>
									<PagerStyle VerticalAlign="Middle" NextPageText="Next" Font-Size="XX-Small" Font-Names="Arial" Font-Bold="True" PrevPageText="Previous" Position="Top" Wrap="False"></PagerStyle>
								</asp:datagrid>
							</DIV>
						</TD>
					</TR>
				</TBODY>
			</TABLE>
		</form>
	</body>
</HTML>
