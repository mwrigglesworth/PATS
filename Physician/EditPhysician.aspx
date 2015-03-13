<%@ Page Language="C#" MasterPageFile="~/Physician/GIPAPPhysician.master" AutoEventWireup="true" CodeFile="EditPhysician.aspx.cs" Inherits="Physician_EditPhysician" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderControlPanel" Runat="Server">
    <h1>Edit Physician Information</h1>
    <TABLE id="Table7" cellSpacing="1" cellPadding="1" width="585" align="left" border="0">
		<TR>
			<TD><asp:validationsummary id="ValidationSummary1" runat="server" HeaderText="You are missing the following fields:"
		ShowMessageBox="True" CssClass="AlertDiv" ForeColor=""></asp:validationsummary></TD>
		</TR>
		</TABLE>
    <div class="FormTable" style="clear:both;">
    <TABLE id="Table2" cellSpacing="1" cellPadding="1" width="600" align="center" border="0">
		<TR>
			<TD width="74">First Name:</TD>
			<TD>
				<asp:TextBox id="txtFirstName" tabIndex="1" runat="server" Width="200px"></asp:TextBox>
				<asp:RequiredFieldValidator id="RequiredFieldValidator1" runat="server" ErrorMessage="First Name" ControlToValidate="txtFirstName">*</asp:RequiredFieldValidator>
				<asp:Label id="LabelFirstName" runat="server"></asp:Label></TD>
			<TD width="101">Street 1:</TD>
			<TD style="width: 212px">
				<asp:TextBox id="txtStreet1" tabIndex="8" runat="server" Width="200px"></asp:TextBox></TD>
		</TR>
		<TR>
			<TD width="74" height="25">Last Name:</TD>
			<TD height="25">
				<asp:TextBox id="txtLastName" tabIndex="2" runat="server" Width="200px"></asp:TextBox>
				<asp:RequiredFieldValidator id="RequiredFieldValidator2" runat="server" ErrorMessage="Last Name" ControlToValidate="txtLastName">*</asp:RequiredFieldValidator>
				<asp:Label id="LabelLastName" runat="server"></asp:Label></TD>
			<TD width="101" height="25">Street 2:</TD>
			<TD height="25" style="width: 212px">
				<asp:TextBox id="txtStreet2" tabIndex="9" runat="server" Width="200px"></asp:TextBox></TD>
		</TR>
		<TR>
			<TD width="74">Sex:</TD>
			<TD>
				<asp:RadioButtonList id="rblstSex" tabIndex="3" runat="server" RepeatDirection="Horizontal">
					<asp:ListItem Value="M">Male</asp:ListItem>
					<asp:ListItem Value="F">Female</asp:ListItem>
				</asp:RadioButtonList></TD>
			<TD width="101">City:</TD>
			<TD style="width: 212px">
				<asp:TextBox id="txtCity" tabIndex="10" runat="server" Width="200px"></asp:TextBox></TD>
		</TR>
		<TR>
			<TD width="74" height="26">Specialty:</TD>
			<TD height="26">
				<asp:dropdownlist id="dropSpecialty" tabIndex="3" runat="server" Width="200px">
					<asp:ListItem Value="0">Select One</asp:ListItem>
					<asp:ListItem Value="Hematology">Hematology</asp:ListItem>
					<asp:ListItem Value="Gastroenterology">Gastroenterology</asp:ListItem>
					<asp:ListItem Value="Oncology">Oncology</asp:ListItem>
					<asp:ListItem Value="Oncohematology">Oncohematology</asp:ListItem>
				</asp:dropdownlist>
				<asp:CompareValidator id="CompareValidator1" runat="server" ErrorMessage="Specialty" ControlToValidate="dropSpecialty"
					Operator="NotEqual" ValueToCompare="0">*</asp:CompareValidator></TD>
			<TD width="101" height="26">State / Province:</TD>
			<TD height="26" style="width: 212px">
				<asp:TextBox id="txtState" tabIndex="11" runat="server" Width="200px"></asp:TextBox></TD>
		</TR>
		<TR>
			<TD width="74">Phone:</TD>
			<TD>
				<asp:TextBox id="txtPhone" tabIndex="4" runat="server" Width="200px"></asp:TextBox></TD>
			<TD width="101">Postal Code:</TD>
			<TD style="width: 212px">
				<asp:TextBox id="txtPostalCode" tabIndex="12" runat="server" Width="200px"></asp:TextBox></TD>
		</TR>
		<TR>
			<TD width="74" style="height: 45px">Fax:</TD>
			<TD style="height: 45px">
				<asp:TextBox id="txtFax" tabIndex="5" runat="server" Width="200px"></asp:TextBox></TD>
			<TD width="101" style="height: 45px">Country:</TD>
			<TD style="width: 212px; height: 45px;">
				<asp:DropDownList id="dropCountry" tabIndex="13" runat="server" Width="200px"></asp:DropDownList>
				<asp:CompareValidator id="CompareValidator2" runat="server" ErrorMessage="Country" ControlToValidate="dropCountry"
					Operator="NotEqual" ValueToCompare="0">*</asp:CompareValidator>
				<asp:Label id="LabelCountry" runat="server"></asp:Label></TD>
		</TR>
		<TR>
			<TD width="74">Email:</TD>
			<TD>
				<asp:TextBox id="txtEmail" tabIndex="6" runat="server" Width="200px" 
                    MaxLength="500"></asp:TextBox><br />
				<asp:Label id="LabelEmail" runat="server"></asp:Label></TD>
		</TR>
		<tr><td>Mobile:</td><td>
				<asp:TextBox id="txtMobile" tabIndex="7" runat="server" Width="200px"></asp:TextBox></td>
                            
                        </tr>
		<TR>
			<TD colspan="4" align="left">
                <asp:Panel ID="PanelNOA" runat="server" Width="100%">
                    NOA-Glivec Physician:<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="rblstNOA"
                                    ErrorMessage="NOA Physician">*</asp:RequiredFieldValidator><asp:RadioButtonList 
                        ID="rblstNOA" runat="server" 
                        onselectedindexchanged="rblstNOA_SelectedIndexChanged" 
                        RepeatDirection="Horizontal">
                        <asp:ListItem>No</asp:ListItem>
                        <asp:ListItem>Yes</asp:ListItem>
                    </asp:RadioButtonList>
                    <br />
                                NOA-Glivec Date:<asp:CustomValidator ID="CustomValidator1" runat="server" ClientValidationFunction="validateNOADate"
                                   ControlToValidate="DropNOADay" ErrorMessage="NOA Day">*</asp:CustomValidator>
                               <asp:CustomValidator ID="CustomValidator2" runat="server" ClientValidationFunction="validateNOADate"
                                   ControlToValidate="dropNOAMonth" ErrorMessage="NOA Month">*</asp:CustomValidator>
                               <asp:CustomValidator ID="CustomValidator3" runat="server" ClientValidationFunction="validateNOADate"
                                   ControlToValidate="dropNOAYear" ErrorMessage="NOA Year">*</asp:CustomValidator> 
                                <asp:DropDownList ID="DropNOADay" runat="server">
                                    <asp:ListItem Selected="True" Value="0">Day</asp:ListItem>
                                    <asp:ListItem Value="1">1</asp:ListItem>
                                    <asp:ListItem Value="2">2</asp:ListItem>
                                    <asp:ListItem Value="3">3</asp:ListItem>
                                    <asp:ListItem Value="4">4</asp:ListItem>
                                    <asp:ListItem Value="5">5</asp:ListItem>
                                    <asp:ListItem Value="6">6</asp:ListItem>
                                    <asp:ListItem Value="7">7</asp:ListItem>
                                    <asp:ListItem Value="8">8</asp:ListItem>
                                    <asp:ListItem Value="9">9</asp:ListItem>
                                    <asp:ListItem Value="10">10</asp:ListItem>
                                    <asp:ListItem Value="11">11</asp:ListItem>
                                    <asp:ListItem Value="12">12</asp:ListItem>
                                    <asp:ListItem Value="13">13</asp:ListItem>
                                    <asp:ListItem Value="14">14</asp:ListItem>
                                    <asp:ListItem Value="15">15</asp:ListItem>
                                    <asp:ListItem Value="16">16</asp:ListItem>
                                    <asp:ListItem Value="17">17</asp:ListItem>
                                    <asp:ListItem Value="18">18</asp:ListItem>
                                    <asp:ListItem Value="19">19</asp:ListItem>
                                    <asp:ListItem Value="20">20</asp:ListItem>
                                    <asp:ListItem Value="21">21</asp:ListItem>
                                    <asp:ListItem Value="22">22</asp:ListItem>
                                    <asp:ListItem Value="23">23</asp:ListItem>
                                    <asp:ListItem Value="24">24</asp:ListItem>
                                    <asp:ListItem Value="25">25</asp:ListItem>
                                    <asp:ListItem Value="26">26</asp:ListItem>
                                    <asp:ListItem Value="27">27</asp:ListItem>
                                    <asp:ListItem Value="28">28</asp:ListItem>
                                    <asp:ListItem Value="29">29</asp:ListItem>
                                    <asp:ListItem Value="30">30</asp:ListItem>
                                    <asp:ListItem Value="31">31</asp:ListItem>
                                    <asp:ListItem></asp:ListItem>
                                </asp:DropDownList>
                                <asp:DropDownList ID="dropNOAMonth" runat="server">
                                    <asp:ListItem Value="0">Month</asp:ListItem>
                                    <asp:ListItem Value="1">January</asp:ListItem>
                                    <asp:ListItem Value="2">February</asp:ListItem>
                                    <asp:ListItem Value="3">March</asp:ListItem>
                                    <asp:ListItem Value="4">April</asp:ListItem>
                                    <asp:ListItem Value="5">May</asp:ListItem>
                                    <asp:ListItem Value="6">June</asp:ListItem>
                                    <asp:ListItem Value="7">July</asp:ListItem>
                                    <asp:ListItem Value="8">August</asp:ListItem>
                                    <asp:ListItem Value="9">September</asp:ListItem>
                                    <asp:ListItem Value="10">October</asp:ListItem>
                                    <asp:ListItem Value="11">November</asp:ListItem>
                                    <asp:ListItem Value="12">December</asp:ListItem>
                                </asp:DropDownList>
                                <asp:DropDownList ID="dropNOAYear" runat="server" Width="62px">
                                    <asp:ListItem Value="0">Year</asp:ListItem>
                                </asp:DropDownList>
                    &nbsp;<asp:Label ID="LabelNOADate" runat="server" ForeColor="Gray" 
                        Font-Size="Small"></asp:Label>
                    <asp:Panel ID="PanelTasigna" runat="server">
                <font color="purple"><b>NOA Tasigna:</b></font>
                    <asp:RadioButtonList ID="rblstTasigna" runat="server">
                     <asp:ListItem Selected="True">No</asp:ListItem>
                      <asp:ListItem>1st + 2nd Line</asp:ListItem>
                       <asp:ListItem>2nd Line Only</asp:ListItem>
                    </asp:RadioButtonList>
                </asp:Panel>
                </asp:Panel>
            </TD>
		</TR>
		<TR>
			<TD>
				<asp:Label id="LabelCompAcc" runat="server">Computer Access:</asp:Label></TD>
			<TD>
				<asp:RadioButtonList id="rblstComputerAccess" runat="server" RepeatDirection="Horizontal">
					<asp:ListItem Value="No">No</asp:ListItem>
					<asp:ListItem Value="Yes">Yes</asp:ListItem>
					<asp:ListItem Value="Limited">Limited</asp:ListItem>
				</asp:RadioButtonList></TD>
			<TD></TD>
			<TD style="width: 212px"></TD>
		</TR>
		<TR>
			<TD colSpan="4">
				<asp:Label id="LabelNotes" runat="server">Additional Physician Information/Contact Details:</asp:Label></TD>
		</TR>
		<TR>
			<TD colSpan="4">
				<asp:TextBox id="txtNotes" runat="server" Width="580px" TextMode="MultiLine" Height="88px"></asp:TextBox></TD>
		</TR>
		<TR>
			<TD colSpan="4">
				<asp:Button id="ButtonSave" tabIndex="13" runat="server" Width="50px" Text="Save" onclick="ButtonSave_Click"></asp:Button>
				&nbsp;
				<asp:Button id="ButtonCancel" tabIndex="15" runat="server" CausesValidation="False" Width="50px"
					Text="Cancel" onclick="ButtonCancel_Click"></asp:Button></TD>
		</TR>
	</TABLE>
    </div>
    <script language="javascript">
		    //**********************************************************************************************************************
        //Function checks to see if patient has Insurance and if answer is Yes, validates the required dependent fields
        function validateNOADate(sender, e) {
            if ($get('<%= rblstNOA.ClientID  %>' + '_1').checked) {
                if (e.Value == "0") {
                    e.IsValid = false;
                }
                else {
                    e.IsValid = true;
                }
            }
            else {
                e.IsValid = true;
            }
        }
</script>
</asp:Content>

