<%@ Reference Page="~/ErrMsg.aspx" %>
<%@ Page Language="vb" AutoEventWireup="false" Inherits="TimeSheetV190_HTJV.ts_costcode" culture="en-US" CodeFile="ts_costcode.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Cost Code Selection</title>
		<script language="javascript">
			self.setTimeout("self.close()",60000);
		</script>
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	</HEAD>
	<body onload="javascript:self.focus();">
		<form id="Form1" method="post" runat="server">
			&nbsp; <INPUT id="Control" style="Z-INDEX: 100; LEFT: 480px; POSITION: absolute; TOP: 15px" type="hidden" name="Control" runat="server">
			<TABLE id="Table1" style="Z-INDEX: 103; LEFT: 0px; POSITION: absolute; TOP: 0px" cellSpacing="1" cellPadding="1" width="429" border="0">
				<TR>
					<TD><asp:label id="Label1" runat="server" Font-Size="XX-Small" Font-Names="Arial">Cost Code / Description</asp:label></TD>
					<TD><asp:textbox id="txtSearch" runat="server" Font-Size="XX-Small" Font-Names="Arial"></asp:textbox></TD>
					<TD><asp:button id="cmdGo" runat="server" Font-Size="XX-Small" Font-Names="Arial" Text="Search" Width="50px"></asp:button></TD>
					<TD><asp:button id="cmdShowAll" runat="server" Font-Size="XX-Small" Font-Names="Arial" Text="All" Width="50px"></asp:button></TD>
				</TR>
				<TR>
					<TD colSpan="4">
						<asp:label id="errMsg" runat="server" Font-Bold="True" Font-Size="XX-Small" Font-Names="Arial" ForeColor="Red"></asp:label></TD>
				</TR>
				<TR>
					<TD colSpan="4">
						<DIV style="OVERFLOW: auto">
							<asp:datagrid id=dgVwTSCost tabIndex=3 runat="server" Font-Names="Arial" Font-Size="XX-Small" Width="100%" AllowPaging="True" OnItemDataBound="dgVwTSCost_ItemDataBound" HorizontalAlign="Center" DataSource="<%# DS_CostCode %>" BorderWidth="1px" CellPadding="0" DataKeyField="CostCode" DataMember="VwETCostCode" AutoGenerateColumns="False" BorderStyle="Solid" BorderColor="#3366CC" PageSize="25" OnSelectedIndexChanged="Change_Causes">
								<SelectedItemStyle Font-Size="XX-Small" Font-Names="Arial" Font-Bold="True" ForeColor="#003399" BackColor="White"></SelectedItemStyle>
								<EditItemStyle Font-Size="XX-Small" Font-Names="Arial"></EditItemStyle>
								<AlternatingItemStyle Font-Size="XX-Small" Font-Names="Arial" ForeColor="#003399" BackColor="White"></AlternatingItemStyle>
								<ItemStyle Font-Size="XX-Small" Font-Names="Arial" ForeColor="#003399" BackColor="White"></ItemStyle>
								<HeaderStyle Font-Size="XX-Small" Font-Names="Arial" Font-Bold="True" Wrap="False" HorizontalAlign="Center" ForeColor="White" VerticalAlign="Middle" BackColor="#3366CC"></HeaderStyle>
								<FooterStyle Font-Size="XX-Small" Font-Names="Arial" Wrap="False" HorizontalAlign="Center" ForeColor="#003399" VerticalAlign="Top" BackColor="#99CCCC"></FooterStyle>
								<Columns>
									<asp:BoundColumn DataField="CostCode" SortExpression="CostCode" HeaderText="Cost Code">
										<HeaderStyle Font-Size="XX-Small" Font-Underline="True" Font-Bold="True" Wrap="False" HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
										<ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="Description" SortExpression="Description" HeaderText="Description">
										<HeaderStyle Font-Underline="True" Font-Bold="True" Wrap="False" HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
									</asp:BoundColumn>
									<asp:ButtonColumn Text="Select" CommandName="Select">
										<HeaderStyle Font-Size="XX-Small" Font-Names="Arial" Wrap="False" HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
										<ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
									</asp:ButtonColumn>
								</Columns>
								<PagerStyle VerticalAlign="Middle" NextPageText="Next" Font-Size="XX-Small" Font-Names="Arial" Font-Bold="True" PrevPageText="Previous" Position="Top" Wrap="False"></PagerStyle>
							</asp:datagrid>
						</DIV>
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
