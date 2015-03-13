<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EditSelf.aspx.cs" Inherits="MaxStation_EditSelf" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <h1>
    <asp:Label ID="LabelH1" runat="server" Text=""></asp:Label></h1>
    <TABLE id="Table7" cellSpacing="1" cellPadding="1" width="585" align="left" border="0">
		<TR>
			<TD><asp:validationsummary id="ValidationSummary1" runat="server" HeaderText="You are missing the following fields:"
		ShowMessageBox="True" CssClass="AlertDiv" ForeColor=""></asp:validationsummary></TD>
		</TR>
		</TABLE>
    <div class="FormTable" style="clear:both;">
    <TABLE id="Table2" cellSpacing="1" cellPadding="1" width="600" align="center" border="0">
		<TR>
			<TD width="75">First Name:</TD>
			<TD width="214">
				<asp:TextBox id="txtFirstName" tabIndex="1" runat="server" Width="200px"></asp:TextBox>
				<asp:RequiredFieldValidator id="RequiredFieldValidator1" runat="server" ControlToValidate="txtFirstName" ErrorMessage="First Name">*</asp:RequiredFieldValidator></TD>
			<TD>Street 1:</TD>
			<TD>
				<asp:TextBox id="txtStreet1" tabIndex="8" runat="server" Width="200px"></asp:TextBox></TD>
		</TR>
		<TR>
			<TD height="25">Last Name:</TD>
			<TD width="214" height="25">
				<asp:TextBox id="txtLastName" tabIndex="2" runat="server" Width="200px"></asp:TextBox>
				<asp:RequiredFieldValidator id="RequiredFieldValidator2" runat="server" ControlToValidate="txtLastName" ErrorMessage="Last Name">*</asp:RequiredFieldValidator></TD>
			<TD height="25">Street 2:</TD>
			<TD height="25">
				<asp:TextBox id="txtStreet2" tabIndex="9" runat="server" Width="200px"></asp:TextBox></TD>
		</TR>
		<TR>
			<TD>Sex:</TD>
			<TD width="214">
				<asp:RadioButtonList id="rblstSex" tabIndex="3" runat="server" RepeatDirection="Horizontal">
					<asp:ListItem Value="M">Male</asp:ListItem>
					<asp:ListItem Value="F">Female</asp:ListItem>
				</asp:RadioButtonList></TD>
			<TD>City:</TD>
			<TD>
				<asp:TextBox id="txtCity" tabIndex="10" runat="server" Width="200px"></asp:TextBox></TD>
		</TR>
		<TR>
			<TD>Phone:</TD>
			<TD width="214">
				<asp:TextBox id="txtPhone" tabIndex="4" runat="server" Width="200px"></asp:TextBox></TD>
			<TD>State / Province:</TD>
			<TD>
				<asp:TextBox id="txtState" tabIndex="11" runat="server" Width="200px"></asp:TextBox></TD>
		</TR>
		<TR>
			<TD>Fax:</TD>
			<TD width="214">
				<asp:TextBox id="txtFax" tabIndex="5" runat="server" Width="200px"></asp:TextBox></TD>
			<TD>Postal Code:</TD>
			<TD>
				<asp:TextBox id="txtPostalCode" tabIndex="12" runat="server" Width="200px"></asp:TextBox></TD>
		</TR>
		<TR>
			<TD>Email:</TD>
			<TD width="214">
				<asp:TextBox id="txtEmail" tabIndex="6" runat="server" Width="200px"></asp:TextBox></TD>
			<TD>Country:</TD>
			<TD>
				<asp:DropDownList id="dropCountry" tabIndex="13" runat="server" Width="200px"></asp:DropDownList>
				<asp:CompareValidator id="CompareValidator1" runat="server" ControlToValidate="dropCountry" ErrorMessage="Country"
					ValueToCompare="0" Operator="NotEqual">*</asp:CompareValidator></TD>
		</TR>
		<TR>
			<TD>Mobile:</TD>
			<TD width="214">
				<asp:TextBox id="txtMobile" tabIndex="7" runat="server" Width="200px"></asp:TextBox></TD>
			<TD></TD>
			<TD>
                &#160;</TD>
		</TR>
		<TR>
			<TD colSpan="4">
				<asp:Label id="LabelNotes" runat="server">Notes:</asp:Label></TD>
		</TR>
		<TR>
			<TD colSpan="4">
				<asp:TextBox id="txtNotes" runat="server" Width="580" TextMode="MultiLine" Height="88px"></asp:TextBox></TD>
		</TR>
		<TR>
			<TD></TD>
			<TD width="214">
				<asp:Button id="ButtonSave" tabIndex="13" runat="server" Width="50px" Text="Save" onclick="ButtonSave_Click"></asp:Button>&nbsp;
				<asp:Button id="ButtonCancel" tabIndex="15" runat="server" Width="50px" Text="Cancel" CausesValidation="False" onclick="ButtonCancel_Click"></asp:Button></TD>
			<TD></TD>
			<TD></TD>
		</TR>
	</TABLE>
    </div>
    </div>
    </form>
</body>
</html>
