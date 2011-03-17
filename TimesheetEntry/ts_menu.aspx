<%@ Page Language="vb" AutoEventWireup="false" Inherits="TimeSheetV190_HTJV.ts_menu"
    Culture="en-US" CodeFile="ts_menu.aspx.vb" %>
    
<%  'LGX: Décommenter la ligne ci dessous en cas de maintenance de l'application afin de faire pointer les user vers la page maintenance.htm%>
<%  'Response.Redirect("./maintenance.htm")%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Koniambo Nickel Project - eTimesheet Application</title>
    <link rel="stylesheet" type="text/css" href="App_Themes/Koniambo/StyleSheet.css" />
</head>
<body style="margin: 0px; background-color: #fff;">
    <form id="Form1" runat="server">
			<TABLE id="Table1" style="LEFT: 0px; TOP: 0px; HEIGHT: 124px" cellSpacing="1" cellPadding="1" width="100%" border="0">
				<TR>
					<TD bgColor="#cae1ef" colSpan="3" style="height: 22px">
						<P style="FONT-WEIGHT: bold; FONT-SIZE: 10pt; TEXT-TRANSFORM: capitalize; FONT-FAMILY: Arial" align="center"><INPUT disabled type="checkbox">&nbsp;&nbsp;&nbsp;Weekly&nbsp;Timesheet 
							&nbsp; <INPUT disabled type="checkbox"></P>
					</TD>
				</TR>
				<TR>
					<TD style="HEIGHT: 20px" align="middle" width="50%" colSpan="3"></TD>
				</TR>
				<TR>
					<TD style="HEIGHT: 20px" width="50%">&nbsp;
						<asp:label id="lbl_welcome" runat="server" Font-Size="12pt" Font-Names="Arial" Font-Bold="True" ForeColor="#06569D"></asp:label></TD>
					<TD style="HEIGHT: 20px" align="left" width="50%" colSpan="2">
                        &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp;
                        <asp:Button ID="BackButton" runat="server" Text="Back" /></TD>
				</TR>
				<TR>
					<TD colSpan="3">
						<P><asp:datagrid id="DG_TimesheetList" runat="server" Font-Size="XX-Small" Font-Names="Arial" OnItemDataBound="DG_TimesheetList_ItemDataBound" HorizontalAlign="Center" BorderStyle="None" AutoGenerateColumns="False" DataMember="TimesheetList" DataSource="<%# DS_TimesheetList %>" Width="100%">
								<SelectedItemStyle Font-Size="XX-Small" Font-Names="Arial" Wrap="False" HorizontalAlign="Center" VerticalAlign="Middle"></SelectedItemStyle>
								<EditItemStyle Wrap="False"></EditItemStyle>
								<AlternatingItemStyle Font-Size="XX-Small" Font-Names="Arial" Wrap="False" HorizontalAlign="Center" VerticalAlign="Middle" BackColor="#ededf1"></AlternatingItemStyle>
								<ItemStyle Font-Size="XX-Small" Font-Names="Arial" Wrap="False" HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
								<HeaderStyle Font-Size="XX-Small" Font-Names="Arial" Font-Bold="True" Wrap="False" HorizontalAlign="Center" VerticalAlign="Middle" BackColor="WhiteSmoke"></HeaderStyle>
								<Columns>
									<asp:ButtonColumn Text="Select" CommandName="Select">
										<HeaderStyle Wrap="False"></HeaderStyle>
										<ItemStyle Wrap="False"></ItemStyle>
									</asp:ButtonColumn>
									<asp:BoundColumn DataField="Week" SortExpression="Week" HeaderText="Week No">
										<HeaderStyle Font-Underline="True" Font-Bold="True"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="StartDate" SortExpression="StartDate" HeaderText="Start Date">
										<HeaderStyle Font-Underline="True" Font-Bold="True"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="Enddate" SortExpression="Enddate" HeaderText="End Date">
										<HeaderStyle Font-Underline="True" Font-Bold="True"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="True" DataField="Status" SortExpression="Status"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="Year" SortExpression="Year" HeaderText="TSYear"></asp:BoundColumn>
								</Columns>
							</asp:datagrid></P>
					</TD>
				</TR>
				<TR>
					<TD vAlign="center" align="middle" colSpan="3">&nbsp;</TD>
				</TR>
				<TR>
					<TD vAlign="center" align="middle" colSpan="3">
						<P>&nbsp;</P>
					</TD>
				</TR>
				<TR>
					<TD vAlign="center" align="middle" colSpan="3">&nbsp;
						</TD>
				</TR>
				<TR>
					<TD vAlign="center" align="middle" colSpan="3"></TD>
				</TR>
				<TR>
					<TD vAlign="center" align="middle" colSpan="3">&nbsp;</TD>
				</TR>
				<TR>
					<TD vAlign="center" align="middle" colSpan="3">
						<FIELDSET style="TABLE-LAYOUT: auto; FONT-SIZE: x-small; WIDTH: 442px; FONT-FAMILY: Arial; HEIGHT: 134px"><LEGEND style="FONT-SIZE: x-small; FONT-FAMILY: Garamond">Status 
								Description:</LEGEND>
							<TABLE id="Table5" cellSpacing="1" cellPadding="1" border="0">
								<TR>
									<TD style="FONT-SIZE: x-small; FONT-FAMILY: Arial" noWrap align="left">
										<LI>
											<font style="FONT-SIZE: xx-small; FONT-FAMILY: Arial" color="red">No timesheet &nbsp;&nbsp;&nbsp;&nbsp;</font>
										</LI>
									</TD>
									<TD style="FONT-SIZE: xx-small; FONT-FAMILY: Arial" noWrap align="left">New weekly timesheet(s) 
										available&nbsp;to be filled-in.</TD>
								</TR>
								<TR>
									<TD style="FONT-SIZE: x-small; FONT-FAMILY: Arial" noWrap align="left">
										<LI>
											<font style="FONT-SIZE: xx-small; FONT-FAMILY: Arial" color="blue">Updated</font>
										</LI>
									</TD>
									<TD style="FONT-SIZE: xx-small; FONT-FAMILY: Arial" noWrap align="left">Saved timesheet(s). 
										Further Changes/Updates (if any) are allowed.</TD>
								</TR>
								<TR>
									<TD style="FONT-SIZE: x-small; FONT-FAMILY: Arial" noWrap align="left">
										<LI style="FONT-SIZE: xx-small; FONT-FAMILY: Arial">
											Submitted
										</LI>
									</TD>
									<TD style="FONT-SIZE: xx-small; FONT-FAMILY: Arial" noWrap align="left">Saved and submitted 
										timesheet(s).</TD>
								</TR>
								<TR>
									<TD style="FONT-SIZE: x-small; FONT-FAMILY: Arial" noWrap></TD>
									<TD style="FONT-SIZE: xx-small; FONT-FAMILY: Arial" noWrap align="left">Any changes/updates are 
										not allowed. Have been submitted for approval.</TD>
								</TR>
								<TR>
									<TD style="FONT-SIZE: xx-small; FONT-FAMILY: Arial" noWrap align="left">
										<LI>
											<font color="red">Rejected</font>
										</LI>
									</TD>
									<TD style="FONT-SIZE: xx-small; FONT-FAMILY: Arial" noWrap align="left">Re-opened/disapproved 
										timesheet(s) that requires re-submission.</TD>
								</TR>
								<TR>
									<TD style="FONT-SIZE: x-small; FONT-FAMILY: Arial" noWrap></TD>
									<TD style="FONT-SIZE: xx-small; FONT-FAMILY: Arial" noWrap align="left">User is required to 
										click on "Submit" button to resubmit for approval.</TD>
								</TR>
								<TR>
									<TD style="FONT-SIZE: xx-small; FONT-FAMILY: Arial" noWrap align="left">
										<LI>
											Approved
										</LI>
									</TD>
									<TD style="FONT-SIZE: xx-small; FONT-FAMILY: Arial" noWrap align="left">Approved timesheet(s).</TD>
								</TR>
							</TABLE>
						</FIELDSET>
                        </TD>
				</TR>
				<TR>
				</TR>
			</TABLE>
</form>
</body>
</html>