<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddWorker.aspx.cs" Inherits="AddWorker" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register TagPrefix="myTagPrefix" TagName="AppHeader" Src="PageHeader.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Add Worker</title>
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
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
            <fieldset>
                <legend>Create a new Worker</legend>
                <ol>
                <li>
                    <label for="LastName">Last Name<em>*</em></label>
                    <asp:TextBox ID="TextBoxLastName" runat="server" Width="300px"></asp:TextBox>
                </li>
                <li>
                    <label for="FirstName">First Name<em>*</em></label>
                    <asp:TextBox ID="TextBoxFirstName" runat="server" Width="300px"></asp:TextBox>
                </li>
                <li>
                    <label for="Nationality">Nationality<em>*</em></label>
                    <asp:ComboBox ID="ComboBoxCountry" runat="server" CssClass="AquaStyle" Width="300px"
                        AutoCompleteMode="SuggestAppend" CaseSensitive="False" DropDownStyle="DropDownList" >
                        <asp:ListItem Value="select">Select</asp:ListItem>
                        <asp:ListItem Value="Afganistan">Afghanistan</asp:ListItem>
                        <asp:ListItem Value="Albania">Albania</asp:ListItem>
                        <asp:ListItem Value="Algeria">Algeria</asp:ListItem>
                        <asp:ListItem Value="American Samoa">American Samoa</asp:ListItem>
                        <asp:ListItem Value="Andorra">Andorra</asp:ListItem>
                        <asp:ListItem Value="Angola">Angola</asp:ListItem>
                        <asp:ListItem Value="Anguilla">Anguilla</asp:ListItem>
                        <asp:ListItem Value="Antigua &amp; Barbuda">Antigua &amp; Barbuda</asp:ListItem>
                        <asp:ListItem Value="Argentina">Argentina</asp:ListItem>
                        <asp:ListItem Value="Armenia">Armenia</asp:ListItem>
                        <asp:ListItem Value="Aruba">Aruba</asp:ListItem>
                        <asp:ListItem Value="Australia">Australia</asp:ListItem>
                        <asp:ListItem Value="Austria">Austria</asp:ListItem>
                        <asp:ListItem Value="Azerbaijan">Azerbaijan</asp:ListItem>
                        <asp:ListItem Value="Bahamas">Bahamas</asp:ListItem>
                        <asp:ListItem Value="Bahrain">Bahrain</asp:ListItem>
                        <asp:ListItem Value="Bangladesh">Bangladesh</asp:ListItem>
                        <asp:ListItem Value="Barbados">Barbados</asp:ListItem>
                        <asp:ListItem Value="Belarus">Belarus</asp:ListItem>
                        <asp:ListItem Value="Belgium">Belgium</asp:ListItem>
                        <asp:ListItem Value="Belize">Belize</asp:ListItem>
                        <asp:ListItem Value="Benin">Benin</asp:ListItem>
                        <asp:ListItem Value="Bermuda">Bermuda</asp:ListItem>
                        <asp:ListItem Value="Bhutan">Bhutan</asp:ListItem>
                        <asp:ListItem Value="Bolivia">Bolivia</asp:ListItem>
                        <asp:ListItem Value="Bonaire">Bonaire</asp:ListItem>
                        <asp:ListItem Value="Bosnia &amp; Herzegovina">Bosnia &amp; Herzegovina</asp:ListItem>
                        <asp:ListItem Value="Botswana">Botswana</asp:ListItem>
                        <asp:ListItem Value="Brazil">Brazil</asp:ListItem>
                        <asp:ListItem Value="British Indian Ocean Ter">British Indian Ocean Ter</asp:ListItem>
                        <asp:ListItem Value="Brunei">Brunei</asp:ListItem>
                        <asp:ListItem Value="Bulgaria">Bulgaria</asp:ListItem>
                        <asp:ListItem Value="Burkina Faso">Burkina Faso</asp:ListItem>
                        <asp:ListItem Value="Burundi">Burundi</asp:ListItem>
                        <asp:ListItem Value="Cambodia">Cambodia</asp:ListItem>
                        <asp:ListItem Value="Cameroon">Cameroon</asp:ListItem>
                        <asp:ListItem Value="Canada">Canada</asp:ListItem>
                        <asp:ListItem Value="Canary Islands">Canary Islands</asp:ListItem>
                        <asp:ListItem Value="Cape Verde">Cape Verde</asp:ListItem>
                        <asp:ListItem Value="Cayman Islands">Cayman Islands</asp:ListItem>
                        <asp:ListItem Value="Central African Republic">Central African Republic</asp:ListItem>
                        <asp:ListItem Value="Chad">Chad</asp:ListItem>
                        <asp:ListItem Value="Channel Islands">Channel Islands</asp:ListItem>
                        <asp:ListItem Value="Chile">Chile</asp:ListItem>
                        <asp:ListItem Value="China">China</asp:ListItem>
                        <asp:ListItem Value="Christmas Island">Christmas Island</asp:ListItem>
                        <asp:ListItem Value="Cocos Island">Cocos Island</asp:ListItem>
                        <asp:ListItem Value="Colombia">Colombia</asp:ListItem>
                        <asp:ListItem Value="Comoros">Comoros</asp:ListItem>
                        <asp:ListItem Value="Congo">Congo</asp:ListItem>
                        <asp:ListItem Value="Cook Islands">Cook Islands</asp:ListItem>
                        <asp:ListItem Value="Costa Rica">Costa Rica</asp:ListItem>
                        <asp:ListItem Value="Cote DIvoire">Cote D'Ivoire</asp:ListItem>
                        <asp:ListItem Value="Croatia">Croatia</asp:ListItem>
                        <asp:ListItem Value="Cuba">Cuba</asp:ListItem>
                        <asp:ListItem Value="Curaco">Curacao</asp:ListItem>
                        <asp:ListItem Value="Cyprus">Cyprus</asp:ListItem>
                        <asp:ListItem Value="Czech Republic">Czech Republic</asp:ListItem>
                        <asp:ListItem Value="Denmark">Denmark</asp:ListItem>
                        <asp:ListItem Value="Djibouti">Djibouti</asp:ListItem>
                        <asp:ListItem Value="Dominica">Dominica</asp:ListItem>
                        <asp:ListItem Value="Dominican Republic">Dominican Republic</asp:ListItem>
                        <asp:ListItem Value="East Timor">East Timor</asp:ListItem>
                        <asp:ListItem Value="Ecuador">Ecuador</asp:ListItem>
                        <asp:ListItem Value="Egypt">Egypt</asp:ListItem>
                        <asp:ListItem Value="El Salvador">El Salvador</asp:ListItem>
                        <asp:ListItem Value="Equatorial Guinea">Equatorial Guinea</asp:ListItem>
                        <asp:ListItem Value="Eritrea">Eritrea</asp:ListItem>
                        <asp:ListItem Value="Estonia">Estonia</asp:ListItem>
                        <asp:ListItem Value="Ethiopia">Ethiopia</asp:ListItem>
                        <asp:ListItem Value="Falkland Islands">Falkland Islands</asp:ListItem>
                        <asp:ListItem Value="Faroe Islands">Faroe Islands</asp:ListItem>
                        <asp:ListItem Value="Fiji">Fiji</asp:ListItem>
                        <asp:ListItem Value="Finland">Finland</asp:ListItem>
                        <asp:ListItem Value="France">France</asp:ListItem>
                        <asp:ListItem Value="French Guiana">French Guiana</asp:ListItem>
                        <asp:ListItem Value="French Polynesia">French Polynesia</asp:ListItem>
                        <asp:ListItem Value="French Southern Ter">French Southern Ter</asp:ListItem>
                        <asp:ListItem Value="Gabon">Gabon</asp:ListItem>
                        <asp:ListItem Value="Gambia">Gambia</asp:ListItem>
                        <asp:ListItem Value="Georgia">Georgia</asp:ListItem>
                        <asp:ListItem Value="Germany">Germany</asp:ListItem>
                        <asp:ListItem Value="Ghana">Ghana</asp:ListItem>
                        <asp:ListItem Value="Gibraltar">Gibraltar</asp:ListItem>
                        <asp:ListItem Value="Great Britain">Great Britain</asp:ListItem>
                        <asp:ListItem Value="Greece">Greece</asp:ListItem>
                        <asp:ListItem Value="Greenland">Greenland</asp:ListItem>
                        <asp:ListItem Value="Grenada">Grenada</asp:ListItem>
                        <asp:ListItem Value="Guadeloupe">Guadeloupe</asp:ListItem>
                        <asp:ListItem Value="Guam">Guam</asp:ListItem>
                        <asp:ListItem Value="Guatemala">Guatemala</asp:ListItem>
                        <asp:ListItem Value="Guinea">Guinea</asp:ListItem>
                        <asp:ListItem Value="Guyana">Guyana</asp:ListItem>
                        <asp:ListItem Value="Haiti">Haiti</asp:ListItem>
                        <asp:ListItem Value="Hawaii">Hawaii</asp:ListItem>
                        <asp:ListItem Value="Honduras">Honduras</asp:ListItem>
                        <asp:ListItem Value="Hong Kong">Hong Kong</asp:ListItem>
                        <asp:ListItem Value="Hungary">Hungary</asp:ListItem>
                        <asp:ListItem Value="Iceland">Iceland</asp:ListItem>
                        <asp:ListItem Value="India">India</asp:ListItem>
                        <asp:ListItem Value="Indonesia">Indonesia</asp:ListItem>
                        <asp:ListItem Value="Iran">Iran</asp:ListItem>
                        <asp:ListItem Value="Iraq">Iraq</asp:ListItem>
                        <asp:ListItem Value="Ireland">Ireland</asp:ListItem>
                        <asp:ListItem Value="Isle of Man">Isle of Man</asp:ListItem>
                        <asp:ListItem Value="Israel">Israel</asp:ListItem>
                        <asp:ListItem Value="Italy">Italy</asp:ListItem>
                        <asp:ListItem Value="Jamaica">Jamaica</asp:ListItem>
                        <asp:ListItem Value="Japan">Japan</asp:ListItem>
                        <asp:ListItem Value="Jordan">Jordan</asp:ListItem>
                        <asp:ListItem Value="Kazakhstan">Kazakhstan</asp:ListItem>
                        <asp:ListItem Value="Kenya">Kenya</asp:ListItem>
                        <asp:ListItem Value="Kiribati">Kiribati</asp:ListItem>
                        <asp:ListItem Value="Korea North">Korea North</asp:ListItem>
                        <asp:ListItem Value="Korea South">Korea South</asp:ListItem>
                        <asp:ListItem Value="Kuwait">Kuwait</asp:ListItem>
                        <asp:ListItem Value="Kyrgyzstan">Kyrgyzstan</asp:ListItem>
                        <asp:ListItem Value="Laos">Laos</asp:ListItem>
                        <asp:ListItem Value="Latvia">Latvia</asp:ListItem>
                        <asp:ListItem Value="Lebanon">Lebanon</asp:ListItem>
                        <asp:ListItem Value="Lesotho">Lesotho</asp:ListItem>
                        <asp:ListItem Value="Liberia">Liberia</asp:ListItem>
                        <asp:ListItem Value="Libya">Libya</asp:ListItem>
                        <asp:ListItem Value="Liechtenstein">Liechtenstein</asp:ListItem>
                        <asp:ListItem Value="Lithuania">Lithuania</asp:ListItem>
                        <asp:ListItem Value="Luxembourg">Luxembourg</asp:ListItem>
                        <asp:ListItem Value="Macau">Macau</asp:ListItem>
                        <asp:ListItem Value="Macedonia">Macedonia</asp:ListItem>
                        <asp:ListItem Value="Madagascar">Madagascar</asp:ListItem>
                        <asp:ListItem Value="Malaysia">Malaysia</asp:ListItem>
                        <asp:ListItem Value="Malawi">Malawi</asp:ListItem>
                        <asp:ListItem Value="Maldives">Maldives</asp:ListItem>
                        <asp:ListItem Value="Mali">Mali</asp:ListItem>
                        <asp:ListItem Value="Malta">Malta</asp:ListItem>
                        <asp:ListItem Value="Marshall Islands">Marshall Islands</asp:ListItem>
                        <asp:ListItem Value="Martinique">Martinique</asp:ListItem>
                        <asp:ListItem Value="Mauritania">Mauritania</asp:ListItem>
                        <asp:ListItem Value="Mauritius">Mauritius</asp:ListItem>
                        <asp:ListItem Value="Mayotte">Mayotte</asp:ListItem>
                        <asp:ListItem Value="Mexico">Mexico</asp:ListItem>
                        <asp:ListItem Value="Midway Islands">Midway Islands</asp:ListItem>
                        <asp:ListItem Value="Moldova">Moldova</asp:ListItem>
                        <asp:ListItem Value="Monaco">Monaco</asp:ListItem>
                        <asp:ListItem Value="Mongolia">Mongolia</asp:ListItem>
                        <asp:ListItem Value="Montserrat">Montserrat</asp:ListItem>
                        <asp:ListItem Value="Morocco">Morocco</asp:ListItem>
                        <asp:ListItem Value="Mozambique">Mozambique</asp:ListItem>
                        <asp:ListItem Value="Myanmar">Myanmar</asp:ListItem>
                        <asp:ListItem Value="Nambia">Nambia</asp:ListItem>
                        <asp:ListItem Value="Nauru">Nauru</asp:ListItem>
                        <asp:ListItem Value="Nepal">Nepal</asp:ListItem>
                        <asp:ListItem Value="Netherland Antilles">Netherland Antilles</asp:ListItem>
                        <asp:ListItem Value="Netherlands">Netherlands (Holland, Europe)</asp:ListItem>
                        <asp:ListItem Value="Nevis">Nevis</asp:ListItem>
                        <asp:ListItem Value="New Caledonia">New Caledonia</asp:ListItem>
                        <asp:ListItem Value="New Zealand">New Zealand</asp:ListItem>
                        <asp:ListItem Value="Nicaragua">Nicaragua</asp:ListItem>
                        <asp:ListItem Value="Niger">Niger</asp:ListItem>
                        <asp:ListItem Value="Nigeria">Nigeria</asp:ListItem>
                        <asp:ListItem Value="Niue">Niue</asp:ListItem>
                        <asp:ListItem Value="Norfolk Island">Norfolk Island</asp:ListItem>
                        <asp:ListItem Value="Norway">Norway</asp:ListItem>
                        <asp:ListItem Value="Oman">Oman</asp:ListItem>
                        <asp:ListItem Value="Pakistan">Pakistan</asp:ListItem>
                        <asp:ListItem Value="Palau Island">Palau Island</asp:ListItem>
                        <asp:ListItem Value="Palestine">Palestine</asp:ListItem>
                        <asp:ListItem Value="Panama">Panama</asp:ListItem>
                        <asp:ListItem Value="Papua New Guinea">Papua New Guinea</asp:ListItem>
                        <asp:ListItem Value="Paraguay">Paraguay</asp:ListItem>
                        <asp:ListItem Value="Peru">Peru</asp:ListItem>
                        <asp:ListItem Value="Phillipines">Philippines</asp:ListItem>
                        <asp:ListItem Value="Pitcairn Island">Pitcairn Island</asp:ListItem>
                        <asp:ListItem Value="Poland">Poland</asp:ListItem>
                        <asp:ListItem Value="Portugal">Portugal</asp:ListItem>
                        <asp:ListItem Value="Puerto Rico">Puerto Rico</asp:ListItem>
                        <asp:ListItem Value="Qatar">Qatar</asp:ListItem>
                        <asp:ListItem Value="Republic of Montenegro">Republic of Montenegro</asp:ListItem>
                        <asp:ListItem Value="Republic of Serbia">Republic of Serbia</asp:ListItem>
                        <asp:ListItem Value="Reunion">Reunion</asp:ListItem>
                        <asp:ListItem Value="Romania">Romania</asp:ListItem>
                        <asp:ListItem Value="Russia">Russia</asp:ListItem>
                        <asp:ListItem Value="Rwanda">Rwanda</asp:ListItem>
                        <asp:ListItem Value="St Barthelemy">St Barthelemy</asp:ListItem>
                        <asp:ListItem Value="St Eustatius">St Eustatius</asp:ListItem>
                        <asp:ListItem Value="St Helena">St Helena</asp:ListItem>
                        <asp:ListItem Value="St Kitts-Nevis">St Kitts-Nevis</asp:ListItem>
                        <asp:ListItem Value="St Lucia">St Lucia</asp:ListItem>
                        <asp:ListItem Value="St Maarten">St Maarten</asp:ListItem>
                        <asp:ListItem Value="St Pierre &amp; Miquelon">St Pierre &amp; Miquelon</asp:ListItem>
                        <asp:ListItem Value="St Vincent &amp; Grenadines">St Vincent &amp; Grenadines</asp:ListItem>
                        <asp:ListItem Value="Saipan">Saipan</asp:ListItem>
                        <asp:ListItem Value="Samoa">Samoa</asp:ListItem>
                        <asp:ListItem Value="Samoa American">Samoa American</asp:ListItem>
                        <asp:ListItem Value="San Marino">San Marino</asp:ListItem>
                        <asp:ListItem Value="Sao Tome &amp; Principe">Sao Tome &amp; Principe</asp:ListItem>
                        <asp:ListItem Value="Saudi Arabia">Saudi Arabia</asp:ListItem>
                        <asp:ListItem Value="Senegal">Senegal</asp:ListItem>
                        <asp:ListItem Value="Seychelles">Seychelles</asp:ListItem>
                        <asp:ListItem Value="Sierra Leone">Sierra Leone</asp:ListItem>
                        <asp:ListItem Value="Singapore">Singapore</asp:ListItem>
                        <asp:ListItem Value="Slovakia">Slovakia</asp:ListItem>
                        <asp:ListItem Value="Slovenia">Slovenia</asp:ListItem>
                        <asp:ListItem Value="Solomon Islands">Solomon Islands</asp:ListItem>
                        <asp:ListItem Value="Somalia">Somalia</asp:ListItem>
                        <asp:ListItem Value="South Africa">South Africa</asp:ListItem>
                        <asp:ListItem Value="Spain">Spain</asp:ListItem>
                        <asp:ListItem Value="Sri Lanka">Sri Lanka</asp:ListItem>
                        <asp:ListItem Value="Sudan">Sudan</asp:ListItem>
                        <asp:ListItem Value="Suriname">Suriname</asp:ListItem>
                        <asp:ListItem Value="Swaziland">Swaziland</asp:ListItem>
                        <asp:ListItem Value="Sweden">Sweden</asp:ListItem>
                        <asp:ListItem Value="Switzerland">Switzerland</asp:ListItem>
                        <asp:ListItem Value="Syria">Syria</asp:ListItem>
                        <asp:ListItem Value="Tahiti">Tahiti</asp:ListItem>
                        <asp:ListItem Value="Taiwan">Taiwan</asp:ListItem>
                        <asp:ListItem Value="Tajikistan">Tajikistan</asp:ListItem>
                        <asp:ListItem Value="Tanzania">Tanzania</asp:ListItem>
                        <asp:ListItem Value="Thailand">Thailand</asp:ListItem>
                        <asp:ListItem Value="Togo">Togo</asp:ListItem>
                        <asp:ListItem Value="Tokelau">Tokelau</asp:ListItem>
                        <asp:ListItem Value="Tonga">Tonga</asp:ListItem>
                        <asp:ListItem Value="Trinidad &amp; Tobago">Trinidad &amp; Tobago</asp:ListItem>
                        <asp:ListItem Value="Tunisia">Tunisia</asp:ListItem>
                        <asp:ListItem Value="Turkey">Turkey</asp:ListItem>
                        <asp:ListItem Value="Turkmenistan">Turkmenistan</asp:ListItem>
                        <asp:ListItem Value="Turks &amp; Caicos Is">Turks &amp; Caicos Is</asp:ListItem>
                        <asp:ListItem Value="Tuvalu">Tuvalu</asp:ListItem>
                        <asp:ListItem Value="Uganda">Uganda</asp:ListItem>
                        <asp:ListItem Value="Ukraine">Ukraine</asp:ListItem>
                        <asp:ListItem Value="United Arab Erimates">United Arab Emirates</asp:ListItem>
                        <asp:ListItem Value="United Kingdom">United Kingdom</asp:ListItem>
                        <asp:ListItem Value="United States of America">United States of America</asp:ListItem>
                        <asp:ListItem Value="Uraguay">Uruguay</asp:ListItem>
                        <asp:ListItem Value="Uzbekistan">Uzbekistan</asp:ListItem>
                        <asp:ListItem Value="Vanuatu">Vanuatu</asp:ListItem>
                        <asp:ListItem Value="Vatican City State">Vatican City State</asp:ListItem>
                        <asp:ListItem Value="Venezuela">Venezuela</asp:ListItem>
                        <asp:ListItem Value="Vietnam">Vietnam</asp:ListItem>
                        <asp:ListItem Value="Virgin Islands (Brit)">Virgin Islands (Brit)</asp:ListItem>
                        <asp:ListItem Value="Virgin Islands (USA)">Virgin Islands (USA)</asp:ListItem>
                        <asp:ListItem Value="Wake Island">Wake Island</asp:ListItem>
                        <asp:ListItem Value="Wallis &amp; Futana Is">Wallis &amp; Futana Is</asp:ListItem>
                        <asp:ListItem Value="Yemen">Yemen</asp:ListItem>
                        <asp:ListItem Value="Zaire">Zaire</asp:ListItem>
                        <asp:ListItem Value="Zambia">Zambia</asp:ListItem>
                        <asp:ListItem Value="Zimbabwe">Zimbabwe</asp:ListItem>
                    </asp:ComboBox>
                </li>
                <li>
                    <label for="Trade">Trade<em>*</em></label>
                    <asp:ComboBox ID="ComboBoxTrade" runat="server" AutoCompleteMode="SuggestAppend" CaseSensitive="False" Width="300px"
                        AutoPostBack="false" CssClass="AquaStyle" DropDownStyle="DropDownList" AppendDataBoundItems="true">
                    </asp:ComboBox>
                </li>
                <li>
                    <label for="PositionCategory">Position category<em>*</em></label>
                    <asp:ComboBox ID="ComboBoxPositionCategory" runat="server" AutoCompleteMode="SuggestAppend" CaseSensitive="False"
                        AutoPostBack="false" CssClass="AquaStyle" DropDownStyle="DropDownList" AppendDataBoundItems="true" Width="300px">
                    </asp:ComboBox>
                </li>
                <li>
                    <label for="Subcontractor">Subcontractor<em>*</em></label>
                    <asp:ComboBox ID="ComboBoxSubcontractor" runat="server" AutoCompleteMode="SuggestAppend" CaseSensitive="False"
                AutoPostBack="false" CssClass="AquaStyle" DropDownStyle="DropDownList" AppendDataBoundItems="true" Width="300px">
                </asp:ComboBox>
                </li>
                <li>
                    <label for="ArrivalDate">Arrival Date<em>*</em></label>
                    <asp:TextBox ID="TextBoxArrivalDate" runat="server" Width="300px"></asp:TextBox>
                    <asp:CalendarExtender  ID="CalendarExtender1" runat="server" targetControlID="TextBoxArrivalDate">
                    </asp:CalendarExtender>
                </li>
                <li>
                    <label for="Induction">Induction Done<em>*</em></label>
                    <asp:CheckBox ID="CheckBoxInduction" runat="server" />
                </li>
                <li>
                    <label for="BadgeNumber">Badge number<em>*</em></label>
                    <asp:TextBox ID="TextBoxBadgeNumber" runat="server"></asp:TextBox>
                </li>
                <li>
                    <label for="VCCNumber">VCC Number</label>
                    <asp:TextBox ID="TextBoxVCCNumber" runat="server"></asp:TextBox>
                </li>
                <li>
                    <label for="Status">Status<em>*</em></label>
                    <asp:ComboBox ID="ComboBoxStatus" runat="server" 
                        AutoCompleteMode="SuggestAppend" CaseSensitive="False"
                        AutoPostBack="true" CssClass="AquaStyle" DropDownStyle="DropDownList" 
                        AppendDataBoundItems="true" 
                        onselectedindexchanged="ComboBoxStatus_SelectedIndexChanged">
                        <asp:ListItem Selected="True">select</asp:ListItem>
                        <asp:ListItem Text="Local" Value="L" />
                        <asp:ListItem Text="Expat" Value="E" />
                </asp:ComboBox>
                </li>
                <li>
                    <label for="ConventionHours"><asp:label runat="server" ID="LableConventionHours" Visible="false">Convention<em>*</em></asp:label></label>
                    <asp:ComboBox ID="ComboBoxConvention" runat="server" Width="300px"
                        AutoCompleteMode="SuggestAppend" CaseSensitive="False"
                        CssClass="AquaStyle" DropDownStyle="DropDownList" 
                        AppendDataBoundItems="true" Visible="false">
                         <asp:ListItem Text="select" Value="select" Selected="True"/>
                        <asp:ListItem Text="Bâtiments & Travaux Publics" Value= "550" />
                        <asp:ListItem Text="Commerce & divers" Value= "480"  />
                        <asp:ListItem Text="Gardiennage, Sécurité" Value= "550"  />
                        <asp:ListItem Text="Hôtels, Bars et Restaurants" Value= "590" />
                        <asp:ListItem Text="Industries" Value= "550"  />
                        <asp:ListItem Text="Mines & Carrières" Value= "600" />
                    </asp:ComboBox>
                </li>
                <li>
                    <label for="blank"></label>
                    <asp:Button ID="ButtonAddWorker" runat="server" Text="Add Worker" 
                     CausesValidation="false" onclick="ButtonAddWorker_Click" />
                </li>
                </ol>
            </fieldset>
        </div>

        <%-- to write information into input fields --%>
     
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ToolkitScriptManager>
        <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="TextBoxLastName" 
            WatermarkCssClass="watermarked" WatermarkText="This field is mandatory"></asp:TextBoxWatermarkExtender>
        <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="TextBoxFirstName" 
            WatermarkCssClass="watermarked" WatermarkText="This field is mandatory"></asp:TextBoxWatermarkExtender>
        <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender6" runat="server" TargetControlID="TextBoxArrivalDate" 
            WatermarkCssClass="watermarked" WatermarkText="This field is mandatory"></asp:TextBoxWatermarkExtender>
        <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender7" runat="server" TargetControlID="TextBoxBadgeNumber" 
            WatermarkCssClass="watermarked" WatermarkText="This field is mandatory"></asp:TextBoxWatermarkExtender>


        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="TextBoxBadgeNumber" FilterType="Numbers" /> 
        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="TextBoxVCCNumber" FilterType="Numbers" /> 

    </form>

</body>
</html>
