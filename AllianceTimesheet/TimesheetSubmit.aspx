<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TimesheetSubmit.aspx.cs" Inherits="TimesheetSubmit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register TagPrefix="myTagPrefix" TagName="AppHeader" Src="PageHeader.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Submit a Timesheet</title>
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
    <link href="site.css" rel="stylesheet" type="text/css" />
    <link type="text/css" href="jquery.jnotify.css" rel="stylesheet" title="default" media="all" />
    <script type="text/javascript" src="scripts/jquery-1.4.2.min.js"></script>
    <script type="text/javascript" src="scripts/jquery.jnotify.min.js"></script>

    <style type="text/css">
        .style1
        {
            width: 600px;
        }
        .style2
        {
            width: 600px;
        }
    </style>

    <script type="text/javascript">
        /*
        *Check if the input is a number and is under 12 Hours.
        */
        function checkHours(input) {
            if (isNaN(input.value)) {
                alert("Number only.");
                input.value = "";
                input.focus();
                return ;
            }
            if (input.value > 12) {
                alert("Number of hours per day can't exceed 12.");
                input.value = "";
            }
        }

        /* 
        * Populate the textbox "Weather" for every worker in the gridview
        */
        function AddSuspension(value) {
            var inputElements = document.getElementsByTagName('input');

            for (var i = 0; i < inputElements.length; i++) {
                var myElement = inputElements[i];

                // Filter through the input types looking for text box
                if (myElement.type === "text") {

                    if (myElement.id.search("TBWeather") != -1) {
                        myElement.value = value;
                    }
                }
            }
        }

        /*
        *Update the total hours for every worker in the gridview
        */
        function calculateTotal(input) {
            var tot = 0;
            // We get all the input of the current row
            var inputElements = input.parentElement.parentElement.getElementsByTagName('input');

            // We add all the inputs of a row to calculate the total
            for (var i = 0; i < inputElements.length; i++) {
                var myElement = inputElements[i];

                if (myElement.value != "") {
                    tot = tot + parseFloat(myElement.value);

                    if (tot > 12) {
                        alert("Number of hours per day can't exceed 12.");
                        //Reset total
                        tot = tot - parseFloat(myElement.value);
                        input.parentElement.parentElement.cells[11].innerHTML = tot;
                        myElement.value = "";
                    }
                }

            }
            // We assigne the total calculate to the 11th cell of the row (ie the "total" cell)
            input.parentElement.parentElement.cells[11].innerHTML = tot;
        }

        /*
        *Enable / disable the submit button
        */
        function enableSubmitButton(input) {
            var canEnableTheButton = true;

            // We get all the input of the current row
            var inputElements = input.parentElement.parentElement.getElementsByTagName('input');
            var rows = input.parentElement.parentElement.parentElement.rows;

            // If the value of the 11th (the "total" cell) == 0, we can not activate the button
            for (var i = 1; i < rows.length; i++) {

                if (isNaN(rows[i].cells[11].innerHTML)) {
                    canEnableTheButton = false;
                }

                if (parseFloat(rows[i].cells[11].innerHTML) == 0)
                {
                    canEnableTheButton = false;
                }
            }

            // Get all the "input" of the page to retrieve the "submit timesheet"button input
            var inputElements = document.getElementsByTagName('input');
            for (var i = 0; i < inputElements.length; i++) {
                var myElement = inputElements[i];

                // Filter through the input types looking for text box
                if (myElement.type == "submit") {

                    //We are on the "Submit Timesheet" button
                    if (myElement.id.search("ButtonSubmitTS") != -1) {

                        if (canEnableTheButton) {
                            myElement.disabled = false;
                        }
                        else {
                            myElement.disabled = "disabled";
                        }

                    }
                }
            }
        }


        /*
        *Called when clic on TS Submit button. Calculate the total of all hours of the TS.
        */
        function CalculateOverallTotal() {
            var overallTotal = 0;
            var workerTable = document.getElementById('GridViewWorkerList');
            var rows = workerTable.rows;
            for (var i = 1; i < rows.length; i++) {

                if (!isNaN(rows[i].cells[11].innerHTML)) {
                    overallTotal = overallTotal + parseFloat(rows[i].cells[11].innerHTML);
                }
            }
            return overallTotal;
        }

        /*
        *Called when clic on TS Submit button. Calculate the total of Productive hours of the TS.
        */
        function CalculateProductiveTotal() {
            var overallTotal = 0;
            var workerTable = document.getElementById('GridViewWorkerList');
            var rows = workerTable.rows;

            var inputElements = workerTable.getElementsByTagName('input')
            for (var i = 0; i < inputElements.length; i = i + 8) {
                var myElement = inputElements[i].value;
                if (myElement != '' && !isNaN(myElement)) {
                    overallTotal = overallTotal + parseFloat(myElement)
                }
            }
            return overallTotal;
        }

        /*
        *Called when clic on TS Submit button. Calculate the total of TRAVEL hours of the TS.
        */
        function CalculateTravelTotal() {
            var overallTotal = 0;
            var workerTable = document.getElementById('GridViewWorkerList');
            var rows = workerTable.rows;

            var inputElements = workerTable.getElementsByTagName('input')
            for (var i = 1; i < inputElements.length; i = i + 8) {
                var myElement = inputElements[i].value;
                if (myElement != '' && !isNaN(myElement)) {
                    overallTotal = overallTotal + parseFloat(myElement)
                }
            }
            return overallTotal;
        }

        /*
        *Called when clic on TS Submit button. Calculate the total of MATERIAL hours of the TS.
        */
        function CalculateMaterialTotal() {
            var overallTotal = 0;
            var workerTable = document.getElementById('GridViewWorkerList');
            var rows = workerTable.rows;

            var inputElements = workerTable.getElementsByTagName('input')
            for (var i = 2; i < inputElements.length; i = i + 8) {
                var myElement = inputElements[i].value;
                if (myElement != '' && !isNaN(myElement)) {
                    overallTotal = overallTotal + parseFloat(myElement)
                }
            }
            return overallTotal;
        }

        /*
        *Called when clic on TS Submit button. Calculate the total of WEATHER hours of the TS.
        */
        function CalculateWeatherTotal() {
            var overallTotal = 0;
            var workerTable = document.getElementById('GridViewWorkerList');
            var rows = workerTable.rows;

            var inputElements = workerTable.getElementsByTagName('input')
            for (var i = 3; i < inputElements.length; i = i + 8) {
                var myElement = inputElements[i].value;
                if (myElement != '' && !isNaN(myElement)) {
                    overallTotal = overallTotal + parseFloat(myElement)
                }
            }
            return overallTotal;
        }

        /*
        *Called when clic on TS Submit button. Calculate the total of INDIRECT hours of the TS.
        */
        function CalculateIndirectTotal() {
            var overallTotal = 0;
            var workerTable = document.getElementById('GridViewWorkerList');
            var rows = workerTable.rows;

            var inputElements = workerTable.getElementsByTagName('input')
            for (var i = 4; i < inputElements.length; i = i + 8) {
                var myElement = inputElements[i].value;
                if (myElement != '' && !isNaN(myElement)) {
                    overallTotal = overallTotal + parseFloat(myElement)
                }
            }
            return overallTotal;
        }

        /*
        *Called when clic on TS Submit button. Calculate the total of OTHERS hours of the TS.
        */
        function CalculateOthersTotal() {
            var overallTotal = 0;
            var workerTable = document.getElementById('GridViewWorkerList');
            var rows = workerTable.rows;

            var inputElements = workerTable.getElementsByTagName('input')
            for (var i = 5; i < inputElements.length; i = i + 8) {
                var myElement = inputElements[i].value;
                if (myElement != '' && !isNaN(myElement)) {
                    overallTotal = overallTotal + parseFloat(myElement)
                }
            }
            return overallTotal;
        }

        /*
        *Called when clic on TS Submit button. Calculate the total of REWORK hours of the TS.
        */
        function CalculateReworkTotal() {
            var overallTotal = 0;
            var workerTable = document.getElementById('GridViewWorkerList');
            var rows = workerTable.rows;

            var inputElements = workerTable.getElementsByTagName('input')
            for (var i = 6; i < inputElements.length; i = i + 8) {
                var myElement = inputElements[i].value;
                if (myElement != '' && !isNaN(myElement)) {
                    overallTotal = overallTotal + parseFloat(myElement)
                }
            }
            return overallTotal;
        }

        /*
        *Called when clic on TS Submit button. Calculate the total of OTHER NON PAY hours of the TS.
        */
        function CalculateNPOthersTotal() {
            var overallTotal = 0;
            var workerTable = document.getElementById('GridViewWorkerList');
            var rows = workerTable.rows;

            var inputElements = workerTable.getElementsByTagName('input')
            for (var i = 7; i < inputElements.length; i = i + 8) {
                var myElement = inputElements[i].value;
                if (myElement != '' && !isNaN(myElement)) {
                    overallTotal = overallTotal + parseFloat(myElement)
                }
            }
            return overallTotal;
        }

    </script>

</head>
<body>

    <form id="form1" runat="server" class="appform" submitdisabledcontrols="true">
        <%-- Header with user informations --%>
        <myTagPrefix:AppHeader ID="Header1" runat="server"/><br />
        <div class="appcontent">
            <fieldset>
                <legend><asp:Label ID="LabelFieldsetLegend" runat="server"/></legend>
                <table>
                    <tr>
                        <td class="style2" colspan="2" align="left" style="width: auto;">
                            <asp:Panel ID="PanelCorrectiveTS" runat="server" Visible="false">
                                <asp:Button ID="ButtonFinishAdjustment" runat="server" Text="Finish Adjustment" 
                                    onclick="ButtonFinishAdjustment_Click" 
                                    OnClientClick='return confirm("Are you sure to finish the adjustment ?")'/>
                                <br />
                                <asp:Label ID="LabelCorrectiveTS" runat="server" Text="This timesheet is currently replaced by the timesheet(s) below:"/>                                
                                <asp:GridView ID="GridViewCorrectiveTS" runat="server" class="gridview" 
                                    AutoGenerateColumns="False" width="30%"
                                    AlternatingRowStyle-BackColor="WhiteSmoke" DataKeyNames="TimesheetNumber" 
                                    DataSourceID="DataSourceCorrectiveTS" EnableModelValidation="True" 
                                    onselectedindexchanged="GridViewCorrectiveTS_SelectedIndexChanged" >
                                    <Columns>
                                        <asp:CommandField SelectText="View" ShowSelectButton="True" />
                                        <asp:BoundField DataField="TimesheetNumber" HeaderText="TimesheetNumber" ReadOnly="True" 
                                            SortExpression="TimesheetNumber" />
                                    </Columns>
                                </asp:GridView>
                                <asp:SqlDataSource ID="DataSourceCorrectiveTS" runat="server" 
                                    ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
                                    ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" />
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                       <td class="style2" colspan="2" align="center" style="width: auto;">
                            Timesheet Date<em>*</em>
                            <asp:TextBox ID="TextBoxTSDate" runat="server" ></asp:TextBox>
                            <asp:CalendarExtender  ID="CalendarExtender1" runat="server" targetControlID="TextBoxTSDate"  Format="dd-MMM-yy">
                            </asp:CalendarExtender>
                       </td>
                    </tr>
                    <tr>
                       <td class="style2" colspan="2" align="center" style="width: auto;">
                           <asp:RadioButtonList ID="RadioButtonListScope" runat="server" 
                               RepeatDirection="Horizontal" AutoPostBack="true"
                               onselectedindexchanged="RadioButtonListScope_SelectedIndexChanged">
                               <asp:ListItem Selected="True">Base Scope</asp:ListItem>
                               <asp:ListItem>Variation</asp:ListItem>
                           </asp:RadioButtonList>
                       </td>
                    </tr>
                    <tr>
                        <td class="style2" style="border:1px solid black"  >
                            <ol>
                                <li>
                                    <asp:DropDownList ID="ComboBoxWS" AutoPostBack="true" runat="server" 
                                        onselectedindexchanged="ComboBoxWS_SelectedIndexChanged" Width="500px"/>
                                </li>
                                <li>
                                    <asp:DropDownList ID="ComboBoxWP" runat="server" AutoPostBack="true" AppendDataBoundItems="true"
                                        enabled="false" onselectedindexchanged="ComboBoxWP_SelectedIndexChanged" Width="500px">
                                    </asp:DropDownList>
                                </li>
                                <li>
                                    <asp:DropDownList ID="ComboBoxCWP" runat="server" AutoPostBack="true" AppendDataBoundItems="true"
                                        enabled="false" onselectedindexchanged="ComboBoxCWP_SelectedIndexChanged" Width="500px">
                                    </asp:DropDownList>
                                </li>
                            </ol>
                        </td>
                        <td class="style1" style="border:1px solid black">
                            <ol>
                                <li>
                                    <asp:DropDownList ID="ComboBoxVariationType" runat="server" AutoPostBack="true" Enabled="false"
                                        onselectedindexchanged="ComboBoxVariationType_SelectedIndexChanged"  Width="200px">
                                        <asp:ListItem Selected="True" Value="select" Text="Select Variation Type *" />
                                        <asp:ListItem>VNR</asp:ListItem>
                                        <asp:ListItem>VPR</asp:ListItem>
                                        <asp:ListItem>CSI</asp:ListItem>
                                        <asp:ListItem>RFI</asp:ListItem>
                                        <asp:ListItem>SDM</asp:ListItem>
                                        <asp:ListItem>NCR</asp:ListItem>
                                        <asp:ListItem>VP</asp:ListItem>
                                    </asp:DropDownList>
                                </li>
                                <li>
                                    <asp:DropDownList ID="ComboBoxVariation" runat="server" AutoPostBack="true" AppendDataBoundItems="true"
                                         onselectedindexchanged="ComboBoxVariation_SelectedIndexChanged" Enabled="false" Width="400px">
                                    </asp:DropDownList>
                                </li>
                                <li>
                                    <asp:TextBox ID="TextBoxVariationOthers" runat="server" Enabled="false"  Width="400px"></asp:TextBox>
                                </li>
                           </ol>
                        </td>
                    </tr>
                    <tr>
                       <td class="style2" >
                           Main Activities of the day <em>*</em>:
                       </td>
                       <td></td>
                    </tr>
                    <tr>
                       <td class="style2" colspan="2" align="left" >
                           <asp:TextBox ID="TextBoxMainActivities" runat="server" TextMode="MultiLine" Width="677px"></asp:TextBox>
                       </td>
                    </tr>
                    <tr>
                        <td>Working Period:</td>
                        <td>Suspension:</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:RadioButtonList ID="RadioButtonListShifts" runat="server" 
                                RepeatDirection="Horizontal" >
                                <asp:ListItem Selected="True">Day Shift</asp:ListItem>
                                <asp:ListItem>Night Shift</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td>
                            <asp:RadioButtonList ID="RadioButtonListSuspension" runat="server" 
                                RepeatDirection="Horizontal" AutoPostBack="true"
                                onselectedindexchanged="RadioButtonListSuspension_SelectedIndexChanged">
                                <asp:ListItem >Yes</asp:ListItem>
                                <asp:ListItem Selected="True">No</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td>Sunday Derogation ? :</td>
                        <td>
                            <table>
                                <tr>
                                    <td>
                                        <asp:Label ID="LabelSuspensionDuration" runat="server" Text="Label" Visible="false">Suspension Duration :</asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBoxSuspensionDuration" runat="server" Visible="false" onblur="checkHours(this)"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="LabelTimesheetDuration" runat="server" Text="Label" Visible="false">Timesheet Duration :</asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBoxTimesheetDuration" runat="server" Visible="false" onblur="checkHours(this)"></asp:TextBox>
                                        <asp:Button ID="ButtonAddSuspension" runat="server" Text="Add" Visible="false"
                                            OnClientClick="AddSuspension(document.getElementById('TextBoxTimesheetDuration').value);"/>
                                    </td>
                                </tr>
                            </table>                            
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:RadioButtonList ID="RadioButtonListSundayDerogation" runat="server" 
                                RepeatDirection="Horizontal">
                                <asp:ListItem >Yes</asp:ListItem>
                                <asp:ListItem Selected="True">No</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td align="right">
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="ButtonAddWorkers" runat="server" Text="Add Workers" />&nbsp;
                            <asp:Button ID="ButtonCallPreviousTS" runat="server" Text="Call Previous TS" />
                        </td>
                        <!--Note : Javascript must be on a single line otherwise it does not work-->
                        <td align="right">
                            <asp:Button ID="ButtonSubmitTS" runat="server" Text="Submit Timesheet" 
                                Visible="false" onclick="ButtonSubmitTS_Click" Enabled="true"
                                OnClientClick='return confirm("Are you sure you want to submit this timesheet?\nTotal Productive = " + CalculateProductiveTotal() + "\nTotal Travel        = " + CalculateTravelTotal() + "\nTotal Material     = " + CalculateMaterialTotal() + "\nTotal Weather    = " + CalculateWeatherTotal() + "\nTotal Indirect      = " + CalculateIndirectTotal() + "\nTotal Others        = " + CalculateOthersTotal() + "\nTotal Rework(NP)= " + CalculateReworkTotal() + "\nTotal Others(NP) = " + CalculateNPOthersTotal() + "\nOverall Total        = " + CalculateOverallTotal());'/>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:GridView ID="GridViewWorkerList" runat="server" class="gridview" 
                                AutoGenerateColumns="false" HorizontalAlign="Center" 
                                onrowcommand="GridViewWorkerList_RowCommand" 
                                onrowdeleting="GridViewWorkerList_RowDeleting"
                                AlternatingRowStyle-BackColor="WhiteSmoke" >
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButtonRemoveWorker" runat="server" CommandName="Delete" CommandArgument='<%# Eval("WorkerID") %>'>Remove</asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="WorkerId" HeaderText="WorkerID" HeaderStyle-CssClass="hiddencolumn" ItemStyle-CssClass="hiddencolumn"/>
                                <asp:BoundField DataField="WorkerInfo" HeaderText="Worker"/>
                                <asp:TemplateField HeaderText="Productive" ItemStyle-HorizontalAlign="Center" ItemStyle-BackColor="AliceBlue">
                                    <ItemTemplate>
                                        <asp:TextBox ID="TBProductive" runat="server" style="width:30px" onblur="checkHours(this);calculateTotal(this);"/>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Travel" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:TextBox ID="TBTravel" runat="server" style="width:30px" onblur="checkHours(this);calculateTotal(this);"/>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Material" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:TextBox ID="TBMaterial" runat="server" style="width:30px" onblur="checkHours(this);calculateTotal(this);"/>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Weather" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:TextBox name="TBWeather" ID="TBWeather" runat="server" style="width:30px" onblur="checkHours(this);calculateTotal(this);"/>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Indirect" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:TextBox ID="TBIndirect" runat="server" style="width:30px" onblur="checkHours(this);calculateTotal(this);"/>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Others" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:TextBox ID="TBOthersPayable" runat="server" style="width:30px" onblur="checkHours(this);calculateTotal(this);"/>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Rework<br/>(Non Pay)" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:TextBox ID="TBRework" runat="server" style="width:30px" onblur="checkHours(this);calculateTotal(this);"/>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Others<br/>(Non Pay)" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:TextBox ID="TBOthersNonPayable" runat="server" style="width:30px" onblur="checkHours(this);calculateTotal(this);"/>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total" ItemStyle-HorizontalAlign="Center">
                                </asp:TemplateField>
                            </Columns>
                            </asp:GridView>
                        </td>

                    </tr>
                </table>

                
            </fieldset>
        </div>


        <%-- Section for the modal popup of worker selection--%>

        <asp:ModalPopupExtender ID="MPE" runat="server"
                            TargetControlID="ButtonAddWorkers"
                            PopupControlID="PanelAddWorkers"
                            BackgroundCssClass="modalBackground" 
                            CancelControlID="ButtonModalCancel"
                            DropShadow="true" 
                            PopupDragHandleControlID="PanelCommentHeader" />

        <asp:Panel ID="PanelAddWorkers" runat="server" BackColor="White" ScrollBars="Vertical" Width="800px" Height="500px">
            <asp:GridView ID="GridViewWorkers" runat="server" class="gridview"  PageSize="10000"
                AlternatingRowStyle-BackColor="WhiteSmoke" AutoGenerateColumns="False" 
                DataKeyNames="WorkerId" DataSourceID="SqlDataSourceWorkers" 
                EnableModelValidation="True">
                <AlternatingRowStyle BackColor="WhiteSmoke" />
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:CheckBox ID="CheckBoxWorkerSelect" runat="server"/>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="WorkerId" HeaderText="WorkerId" 
                        InsertVisible="False" ReadOnly="True" SortExpression="WorkerId" />
                    <asp:BoundField DataField="LastName" HeaderText="LastName" 
                        SortExpression="LastName" />
                    <asp:BoundField DataField="FirstName" HeaderText="FirstName" 
                        SortExpression="FirstName" />
                    <asp:BoundField DataField="BadgeNumber" HeaderText="BadgeNumber" 
                        SortExpression="BadgeNumber" />
                    <asp:BoundField DataField="SubcontractorName" HeaderText="Subcontractor" 
                        SortExpression="Subcontractor" />
                    <asp:BoundField DataField="Trade" HeaderText="Trade" SortExpression="Trade" />
                </Columns>
            </asp:GridView>
            <asp:SqlDataSource ID="SqlDataSourceWorkers" runat="server" 
                ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
                ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" 
                SelectCommand="SELECT W.[WorkerId], [LastName], [FirstName], [BadgeNumber], [SubcontractorName], [Trade] FROM [Worker] W
                                INNER JOIN [WorkerContract] WC on W.WorkerId = WC.WorkerId
                                INNER JOIN [Subcontractor] SUB on W.SubcontractorId = SUB.SubcontractorId
                                WHERE (WC.[ContractNumber] = ?) AND WC.Active = 1 ORDER BY [SubcontractorName], [Trade] asc, [LastName], [FirstName], [BadgeNumber]">
                <SelectParameters>
                    <asp:SessionParameter Name="ContractNumber" SessionField="currentContract" 
                        Type="String" />
                </SelectParameters>
            </asp:SqlDataSource>
            
            <div align="center">
                <asp:Button ID="ButtonAddSelectedWorkers" runat="server" Text="Add" 
                    onclick="ButtonAddSelectedWorkers_Click" />
                <asp:Button ID="ButtonModalCancel" runat="server" Text="Cancel" />
            </div>
        </asp:Panel>




                     <%-- Section for the modal popup of selection of a previous timesheet--%>

            <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server"
                            TargetControlID="ButtonCallPreviousTS"
                            PopupControlID="PanelPreviousTS"
                            BackgroundCssClass="modalBackground" 
                            CancelControlID="ButtonCancelPreviousTS"
                            DropShadow="true"  />

            <asp:Panel ID="PanelPreviousTS" runat="server" BackColor="White" ScrollBars="Vertical" Width="800px" Height="500px">
                <div align="center">
                    <b>Previous Timesheets :</b>
                    <br /><br />
                </div>
                <asp:GridView ID="GridViewPreviousTS" runat="server" class="gridview"  PageSize="10000"
                    AlternatingRowStyle-BackColor="WhiteSmoke" AutoGenerateColumns="False" 
                    DataKeyNames="TimesheetNumber" DataSourceID="SqlDataSourcePreviousTS" 
                    EnableModelValidation="True" 
                    onselectedindexchanged="GridViewPreviousTS_SelectedIndexChanged" >
                    <AlternatingRowStyle BackColor="WhiteSmoke" />
                    <Columns>
                        <asp:CommandField ShowSelectButton="True" />
                        <asp:BoundField DataField="TimesheetNumber" HeaderText="TS Number" 
                            ReadOnly="True" SortExpression="TimesheetNumber" />
                        <asp:BoundField DataField="TimesheetDate" HeaderText="TS Date" 
                            SortExpression="TimesheetDate" />
                        <asp:BoundField DataField="WSNumber" HeaderText="WS" 
                            SortExpression="WSNumber" />
                        <asp:BoundField DataField="WPNumber" HeaderText="WP" 
                            SortExpression="WPNumber" />
                        <asp:BoundField DataField="CWPNumber" HeaderText="CWP" 
                            SortExpression="CWPNumber" />
                        <asp:BoundField DataField="VariationNumber" HeaderText="Variation" 
                            SortExpression="CWPNumber" />
                        <asp:BoundField DataField="SubmittedDate" HeaderText="Submitted Date" 
                            SortExpression="SubmittedDate" />
                        <asp:BoundField DataField="SubmittedBy" HeaderText="Submitted by" 
                            SortExpression="SubmittedBy" />
                    </Columns>
                </asp:GridView>

                <asp:SqlDataSource ID="SqlDataSourcePreviousTS" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
                    ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" 
                    SelectCommand="SELECT [TimesheetDate], [TimesheetNumber], [WSNumber], [WPNumber], [CWPNumber], [VariationNumber],
                    [SubmittedDate], [SubmittedBy] FROM [TimesheetHeader] WHERE (([ContractNumber] = ?) AND ([SubmittedBy] = ?)) ORDER BY [SubmittedDate] DESC">
                    <SelectParameters>
                        <asp:SessionParameter Name="ContractNumber" SessionField="currentContract" Type="String" />
                        <asp:SessionParameter Name="SubmittedBy" SessionField="CurrentUser" Type="String" />
                    </SelectParameters>
                </asp:SqlDataSource>

                <div align="center">
                    <asp:Button ID="ButtonCancelPreviousTS" runat="server" Text="Cancel" />
                </div>

            </asp:Panel>


        <%-- to manage data of input fields --%>   
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ToolkitScriptManager>
        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="TextBoxTimesheetDuration" FilterType="Custom, Numbers"
            ValidChars=".," />
        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="TextBoxSuspensionDuration" FilterType="Custom, Numbers"
            ValidChars=".," />
        <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="TextBoxVariationOthers" 
            WatermarkCssClass="watermarked" WatermarkText="Others"></asp:TextBoxWatermarkExtender>


    </form>


    <script type="text/javascript">

        // We add this script at the end of the page otherwise, the elements called are not created
        window.onload = function () { initHoursOnReload(); };

        /*
        * Parse all inputs of the workers sumbission table and calcule totals.
        * Have to add this otherwise all total were lost after a reload of the page
        */
        function initHoursOnReload() {
            var workerTable = document.getElementById('GridViewWorkerList');

            if (workerTable != null) {
                var inputElements = workerTable.getElementsByTagName('input');

                // We make an incrementation of 8 to calcule the total from the first text box of each row
                for (var i = 0; i < inputElements.length; i=i+8) {
                    var currentInput = inputElements[i];
                    calculateTotal(currentInput);
                    //enableSubmitButton(currentInput);
                }
            }

        }
    </script>

</body>
</html>
