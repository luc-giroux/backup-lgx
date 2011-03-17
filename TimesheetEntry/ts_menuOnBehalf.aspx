<%@ Page Language="vb" AutoEventWireup="false" Inherits="TimeSheetV190_HTJV.ts_menuob" culture="en-US" CodeFile="ts_menuOnBehalf.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Weekly Timesheet OB (Menu)</title>
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server" defaultbutton="btn_search">
			<TABLE id="Table1" style="Z-INDEX: 102; LEFT: 0px; POSITION: absolute; TOP: 0px" cellSpacing="1" cellPadding="1" width="100%" border="0">
				<TR>
					<TD bgColor="#cae1ef" style="height: 22px">
						<P style="FONT-WEIGHT: bold; FONT-SIZE: x-small; TEXT-TRANSFORM: capitalize; FONT-FAMILY: Arial" align="center"><INPUT disabled type="checkbox">&nbsp;&nbsp;
							<asp:label id="lbl_deptnm" runat="server" Font-Size="X-Small" Font-Names="Arial">Weekly Timesheet (On-Behalf)</asp:label>&nbsp;
							<INPUT disabled type="checkbox"></P>
					</TD>
				</TR>
				<TR>
					<TD>&nbsp;
						<asp:label id="lbl_welcome" runat="server" Font-Size="XX-Small" Font-Names="Arial" Font-Bold="True" TabIndex="1"></asp:label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
						<asp:RequiredFieldValidator id="RF_TxtSearch" runat="server" Display="Dynamic" ControlToValidate="txt_search" Font-Names="Arial" Font-Size="XX-Small" TabIndex="1">Please enter name or employee number.</asp:RequiredFieldValidator></TD>
				</TR>
				<TR>
					<TD align="middle"><asp:label id="Label1" runat="server" Font-Size="XX-Small" Font-Names="Arial" TabIndex="1">Name / Emp No</asp:label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
						<asp:textbox id="txt_search" runat="server" Font-Size="XX-Small" Font-Names="Arial" MaxLength="50"></asp:textbox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
						<asp:button id="btn_search" runat="server" Font-Names="Arial" Font-Size="XX-Small" Text="Search" Width="50px"></asp:button>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
						<asp:button id="btn_all" runat="server" Font-Size="XX-Small" Font-Names="Arial" Width="50px" Text="All" CausesValidation="False"></asp:button></TD>
				</TR>
				<TR>
					<TD>
						<asp:datagrid id=dg_namelistOB runat="server" Font-Size="XX-Small" Font-Names="Arial" Width="100%" DataSource="<%# DS_EmployeeList %>" DataMember="EmployeeList" AutoGenerateColumns="False" HorizontalAlign="Center" BorderStyle="None" PageSize="50" AllowPaging="True" OnItemDataBound="dg_namelistOB_ItemDataBound">
							<SelectedItemStyle Font-Size="XX-Small" Font-Names="Arial" Wrap="False"></SelectedItemStyle>
							<AlternatingItemStyle Font-Size="XX-Small" Font-Names="Arial" Wrap="False" BackColor="#ededf1"></AlternatingItemStyle>
							<ItemStyle Font-Size="XX-Small" Font-Names="Arial" Wrap="False"></ItemStyle>
							<HeaderStyle Font-Size="XX-Small" Font-Names="Arial" Font-Bold="True" Wrap="False" HorizontalAlign="Center" VerticalAlign="Middle" BackColor="WhiteSmoke"></HeaderStyle>
							<FooterStyle Wrap="False"></FooterStyle>
							<Columns>
								<asp:ButtonColumn Text="Select" CommandName="Select">
									<ItemStyle Font-Size="XX-Small" Font-Names="Arial" Wrap="False" BorderStyle="None" HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
								</asp:ButtonColumn>
								<asp:BoundColumn DataField="EmployeeNumber" HeaderText="Emp No">
									<HeaderStyle Font-Size="XX-Small" Font-Underline="True" Font-Names="Arial" Font-Bold="True" Wrap="False" BorderStyle="None" HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
									<ItemStyle Font-Size="XX-Small" Font-Names="Arial" Wrap="False" BorderStyle="None" HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="Name" SortExpression="Name" HeaderText="Name">
									<HeaderStyle Font-Size="XX-Small" Font-Underline="True" Font-Names="Arial" Font-Bold="True" Wrap="False" BorderStyle="None" HorizontalAlign="Left" VerticalAlign="Middle"></HeaderStyle>
									<ItemStyle Font-Size="XX-Small" Font-Names="Arial" Wrap="False" BorderStyle="None" HorizontalAlign="Left" VerticalAlign="Middle" ></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="Discipline" SortExpression="Discipline" HeaderText="Department">
									<HeaderStyle Font-Size="XX-Small" Font-Underline="True" Font-Names="Arial" Font-Bold="True" Wrap="False" BorderStyle="None" HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
									<ItemStyle Font-Size="XX-Small" Font-Names="Arial" Wrap="False" BorderStyle="None" HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
								</asp:BoundColumn>
							</Columns>
							<PagerStyle NextPageText="Next" Font-Size="XX-Small" Font-Names="Arial" PrevPageText="Previous" Position="Top" Wrap="False"></PagerStyle>
						</asp:datagrid></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
