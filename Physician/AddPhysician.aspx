<%@ Page Language="C#" MasterPageFile="~/PATS.master" AutoEventWireup="true" CodeFile="AddPhysician.aspx.cs" Inherits="Physician_AddPhysician" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="width:100%; clear:both; text-align: left;">
<h1>Add Physician</h1>
<TABLE id="Table7" cellSpacing="1" cellPadding="1" width="900" align="left" border="0">
		<TR>
			<TD><asp:validationsummary id="ValidationSummary1" runat="server" HeaderText="You are missing the following fields:"
		ShowMessageBox="True" CssClass="AlertDiv" ForeColor=""></asp:validationsummary></TD>
		</TR>
		</TABLE>
    <div class="FormTable" style="clear:both;">
    <TABLE id="Table2" cellSpacing="1" cellPadding="1" width="900" align="center" border="0">
		<TR>
			<TD >First Name:</TD>
			<TD>
				<asp:TextBox id="txtFirstName" tabIndex="1" runat="server" Width="200px"></asp:TextBox>
				<asp:RequiredFieldValidator id="RequiredFieldValidator1" runat="server" ErrorMessage="First Name" ControlToValidate="txtFirstName">*</asp:RequiredFieldValidator>
				<br /><asp:Label id="LabelFirstName" runat="server"></asp:Label></TD>
			<TD >Street 1:</TD>
			<TD >
				<asp:TextBox id="txtStreet1" tabIndex="8" runat="server" Width="200px"></asp:TextBox></TD>
		</TR>
		<TR>
			<TD >Last Name:</TD>
			<TD >
				<asp:TextBox id="txtLastName" tabIndex="2" runat="server" Width="200px"></asp:TextBox>
				<asp:RequiredFieldValidator id="RequiredFieldValidator2" runat="server" ErrorMessage="Last Name" ControlToValidate="txtLastName">*</asp:RequiredFieldValidator>
				<br /><asp:Label id="LabelLastName" runat="server"></asp:Label></TD>
			<TD >Street 2:</TD>
			<TD>
				<asp:TextBox id="txtStreet2" tabIndex="9" runat="server" Width="200px"></asp:TextBox></TD>
		</TR>
		<TR>
			<TD >Sex:</TD>
			<TD>
				<asp:RadioButtonList id="rblstSex" tabIndex="3" runat="server" RepeatDirection="Horizontal">
					<asp:ListItem Value="M">Male</asp:ListItem>
					<asp:ListItem Value="F">Female</asp:ListItem>
				</asp:RadioButtonList></TD>
			<TD >City:</TD>
			<TD>
				<asp:TextBox id="txtCity" tabIndex="10" runat="server" Width="200px"></asp:TextBox></TD>
		</TR>
		<TR>
			<TD>Specialty:</TD>
			<TD>
				<asp:dropdownlist id="dropSpecialty" tabIndex="3" runat="server" Width="200px">
					<asp:ListItem Value="0">Select One</asp:ListItem>
					<asp:ListItem Value="Hematology">Hematology</asp:ListItem>
					<asp:ListItem Value="Gastroenterology">Gastroenterology</asp:ListItem>
					<asp:ListItem Value="Oncology">Oncology</asp:ListItem>
					<asp:ListItem Value="Oncohematology">Oncohematology</asp:ListItem>
				</asp:dropdownlist>
				<asp:CompareValidator id="CompareValidator1" runat="server" ErrorMessage="Specialty" ControlToValidate="dropSpecialty"
					Operator="NotEqual" ValueToCompare="0">*</asp:CompareValidator></TD>
			<TD >State / Province:</TD>
			<TD >
				<asp:TextBox id="txtState" tabIndex="11" runat="server" Width="200px"></asp:TextBox></TD>
		</TR>
		<TR>
			<TD>Phone:</TD>
			<TD>
				<asp:TextBox id="txtPhone" tabIndex="4" runat="server" Width="200px"></asp:TextBox></TD>
			<TD>Postal Code:</TD>
			<TD>
				<asp:TextBox id="txtPostalCode" tabIndex="12" runat="server" Width="200px"></asp:TextBox></TD>
		</TR>
		<TR>
			<TD>Fax:</TD>
			<TD >
				<asp:TextBox id="txtFax" tabIndex="5" runat="server" Width="200px"></asp:TextBox></TD>
			<TD >Country:</TD>
			<TD >
				<asp:DropDownList id="dropCountry" tabIndex="13" runat="server" Width="200px" 
                    AutoPostBack="True" onselectedindexchanged="dropCountry_SelectedIndexChanged"></asp:DropDownList>
				<asp:CompareValidator id="CompareValidator2" runat="server" ErrorMessage="Country" ControlToValidate="dropCountry"
					Operator="NotEqual" ValueToCompare="0">*</asp:CompareValidator>
				<asp:Label id="LabelCountry" runat="server"></asp:Label></TD>
		</TR>
		<TR>
			<TD >Email:</TD>
			<TD>
				<asp:TextBox id="txtEmail" tabIndex="6" runat="server" Width="200px" 
                    MaxLength="500"></asp:TextBox><br />
				<asp:Label id="LabelEmail" runat="server"></asp:Label></TD>
		</TR>
		<tr><td>Mobile:</td><td>
				<asp:TextBox id="txtMobile" tabIndex="7" runat="server" Width="200px"></asp:TextBox></td>
                            
                        </tr>
		<TR>
			<TD colspan="2">
                <asp:Panel ID="PanelNOA" runat="server" Width="100%">
                    <table border="0">
                    <tr><td >
                                NOA Physician:</td>
                            <td>
                                <asp:RadioButtonList ID="rblstNOA" runat="server" RepeatDirection="Horizontal" 
                                    onselectedindexchanged="rblstNOA_SelectedIndexChanged">
                                    <asp:ListItem>No</asp:ListItem>
                                    <asp:ListItem>Yes</asp:ListItem>
                                </asp:RadioButtonList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="rblstNOA"
                                    ErrorMessage="NOA Physician">*</asp:RequiredFieldValidator></td></tr>
                        <tr>
                           <td >
                                NOA Date:</td>
                            <td >
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
                                <asp:DropDownList ID="dropNOAYear" runat="server">
                                    <asp:ListItem Value="0">Year</asp:ListItem>
                                </asp:DropDownList></td>
                        </tr>
                    </table>
                    <asp:Panel ID="PanelTasigna" runat="server">
                <font color="purple"><b>Approved for Tasigna:</b></font>
                    <asp:RadioButtonList ID="rblstTasigna" runat="server" Enabled="False">
                     <asp:ListItem Selected="True">No</asp:ListItem>
                      <asp:ListItem>1st + 2nd Line</asp:ListItem>
                       <asp:ListItem>2nd Line Only</asp:ListItem>
                    </asp:RadioButtonList>
                </asp:Panel>
                </asp:Panel>
            </TD>
			<TD colspan=2>
                
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
			<TD ></TD>
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
				<asp:Button id="ButtonSave" tabIndex="13" runat="server" Text="Save Physician" 
                    onclick="ButtonSave_Click"></asp:Button>
				&nbsp;
				<asp:Label ID="LabelError" runat="server" ForeColor="Red" Visible="True"></asp:Label>
            </TD>
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
</div>
</asp:Content>

