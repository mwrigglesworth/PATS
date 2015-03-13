<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EditContact.aspx.cs" Inherits="Patient_EditContact" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form id="frmContact" runat="server">
    <input type="hidden" id="patientid" value="<%=patientid%>" />
<input type="hidden" id="contactid" value="<%=contactid%>" />
    <h1><%=Action%> Patient Contact Information</h1>
    <TABLE id="Table7" cellSpacing="1" cellPadding="1" width="585" align="left" border="0">
		<TR>
			<TD><%--<asp:validationsummary id="ValidationSummary1" runat="server" HeaderText="You are missing the following fields:"
		ShowMessageBox="True" CssClass="AlertDiv" ForeColor=""></asp:validationsummary>--%></TD>
		</TR>
		</TABLE>
    <div class="FormTable" style="clear:both;">
    <TABLE id="Table2" cellSpacing="1" cellPadding="1" width="600" align="center" border="0">
		<TR>
			<TD width="75">First Name:</TD>
			<TD>
				<asp:TextBox id="txtContFirstName" Text="" tabIndex="1" runat="server" Width="200px"></asp:TextBox>
				<%--<asp:RequiredFieldValidator id="RequiredFieldValidator1" runat="server" ErrorMessage="First Name" ControlToValidate="txtFirstName">*</asp:RequiredFieldValidator>--%></TD>
			<TD width="90">Street 1:</TD>
			<TD>
				<asp:TextBox id="txtStreet1" tabIndex="8" runat="server" Width="200px"></asp:TextBox></TD>
		</TR>
		<TR>
			<TD width="75" height="25">Last Name:</TD>
			<TD height="25">
				<asp:TextBox id="txtContLastName" Text="" tabIndex="2" runat="server" Width="200px"></asp:TextBox>
				<%--<asp:RequiredFieldValidator id="RequiredFieldValidator2" runat="server" ErrorMessage="Last Name" ControlToValidate="txtLastName">*</asp:RequiredFieldValidator>--%></TD>
			<TD width="90" height="25">Street 2:</TD>
			<TD height="25">
				<asp:TextBox id="txtStreet2" tabIndex="9" runat="server" Width="200px"></asp:TextBox></TD>
		</TR>
		<TR>
			<TD width="75">Sex:</TD>
			<TD>
				<asp:RadioButtonList id="rblstSex" tabIndex="3" runat="server" RepeatDirection="Horizontal">
					<asp:ListItem Value="M">Male</asp:ListItem>
					<asp:ListItem Value="F">Female</asp:ListItem>
				</asp:RadioButtonList></TD>
			<TD width="90">City:</TD>
			<TD>
				<asp:TextBox id="txtCity" tabIndex="10" runat="server" Width="200px"></asp:TextBox></TD>
		</TR>
		<TR>
			<TD width="75">Phone:</TD>
			<TD>
				<asp:TextBox id="txtPhone" tabIndex="4" runat="server" Width="200px"></asp:TextBox></TD>
			<TD width="90">State / Province:</TD>
			<TD>
				<asp:TextBox id="txtState" tabIndex="11" runat="server" Width="200px"></asp:TextBox></TD>
		</TR>
		<TR>
			<TD width="75">Fax:</TD>
			<TD>
				<asp:TextBox id="txtFax" tabIndex="5" runat="server" Width="200px"></asp:TextBox></TD>
			<TD width="90">Postal Code:</TD>
			<TD>
				<asp:TextBox id="txtPostalCode" tabIndex="12" runat="server" Width="200px"></asp:TextBox></TD>
		</TR>
		<TR>
			<TD width="75">Email:</TD>
			<TD>
				<asp:TextBox id="txtEmail" tabIndex="6" runat="server" Width="200px"></asp:TextBox></TD>
			<TD width="90">Country:</TD>
			<TD>
				<asp:DropDownList id="dropCountry" tabIndex="13" runat="server" Width="200px"></asp:DropDownList>
				<%--<asp:CompareValidator id="CompareValidator1" runat="server" ErrorMessage="Country" ControlToValidate="dropCountry"
					Operator="NotEqual" ValueToCompare="0">*</asp:CompareValidator>--%></TD>
		</TR>
		<TR>
			<TD width="75">Mobile:</TD>
			<TD>
				<asp:TextBox id="txtMobile" tabIndex="7" runat="server" Width="200px"></asp:TextBox></TD>
			<TD width="90"></TD>
			<TD></TD>
		</TR>
		<TR>
			<TD colSpan="4">
				<HR color="steelblue">
			</TD>
		</TR>
		<TR>
			<TD width="75">Relationship to Patient:</TD>
			<TD>
				<asp:DropDownList id="dropRelationship" tabIndex="14" runat="server" Width="200px">
					<asp:ListItem Value="0">Select One</asp:ListItem>
					<asp:ListItem Value="Mother">Mother</asp:ListItem>
					<asp:ListItem Value="Father">Father</asp:ListItem>
					<asp:ListItem Value="Sister">Sister</asp:ListItem>
					<asp:ListItem Value="Brother">Brother</asp:ListItem>
					<asp:ListItem Value="Son">Son</asp:ListItem>
					<asp:ListItem Value="Daughter">Daughter</asp:ListItem>
					<asp:ListItem Value="Other Relative">Other Relative</asp:ListItem>
					<asp:ListItem Value="Neighbor">Neighbor</asp:ListItem>
					<asp:ListItem Value="Friend">Friend</asp:ListItem>
					<asp:ListItem Value="Husband">Husband</asp:ListItem>
					<asp:ListItem Value="Wife">Wife</asp:ListItem>
					<asp:ListItem Value="Domestic Partner">Domestic Partner</asp:ListItem>
					<asp:ListItem Value="Other">Other</asp:ListItem>
				</asp:DropDownList></TD>
			<TD width="90">
				<%--<asp:CompareValidator id="CompareValidator2" runat="server" ErrorMessage="Relationship" ControlToValidate="dropRelationship"
					Operator="NotEqual" ValueToCompare="0">*</asp:CompareValidator>--%></TD>
			<TD></TD>
		</TR>
		<TR>
			<TD width="75">Relationship Details:</TD>
			<TD colSpan="3">
				<asp:TextBox id="txtRelDetails" tabIndex="15" runat="server" Width="400px" TextMode="MultiLine"></asp:TextBox></TD>
		</TR>
		<TR>
			<TD width="75"></TD>
			<TD>
				<asp:Button id="ButtonSave" tabIndex="16" runat="server" 
                    Text="Save Contact Info" ></asp:Button>
				&nbsp;
				</TD>
			<TD width="90">
				<asp:Button id="ButtonCancel" tabIndex="18" runat="server" Width="50px" Text="Cancel" CausesValidation="False"></asp:Button></TD>
			<TD></TD>
		</TR>
	</TABLE>
    </div>
    </form>
    </body>
    </html>
    
