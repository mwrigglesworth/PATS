<%@ Page Language="C#" MasterPageFile="~/PATS.master" AutoEventWireup="true" CodeFile="AddPerson.aspx.cs" Inherits="Person_AddPerson" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="width:100%; clear:both; text-align: left;">
<h1>
    <asp:Label ID="LabelH1" runat="server" Text=""></asp:Label></h1>
    <TABLE id="Table7" cellSpacing="1" cellPadding="1" width="585" align="left" border="0">
		<TR>
			<TD><asp:validationsummary id="ValidationSummary1" runat="server" HeaderText="You are missing the following fields:"
		ShowMessageBox="True" CssClass="AlertDiv" ForeColor=""></asp:validationsummary></TD>
		</TR>
		</TABLE>
    <div class="FormTable" style="clear:both;">
    <TABLE id="Table2" cellSpacing="1" cellPadding="1" width="900" align="center" border="0">
		<TR>
			<TD>First Name:</TD>
			<TD>
				<asp:TextBox id="txtFirstName" tabIndex="1" runat="server" Width="200px"></asp:TextBox>
				<asp:RequiredFieldValidator id="RequiredFieldValidator1" runat="server" ControlToValidate="txtFirstName" ErrorMessage="First Name">*</asp:RequiredFieldValidator></TD>
			<TD>Street 1:</TD>
			<TD>
				<asp:TextBox id="txtStreet1" tabIndex="8" runat="server" Width="200px"></asp:TextBox></TD>
		</TR>
		<TR>
			<TD>Last Name:</TD>
			<TD>
				<asp:TextBox id="txtLastName" tabIndex="2" runat="server" Width="200px"></asp:TextBox>
				<asp:RequiredFieldValidator id="RequiredFieldValidator2" runat="server" ControlToValidate="txtLastName" ErrorMessage="Last Name">*</asp:RequiredFieldValidator></TD>
			<TD>Street 2:</TD>
			<TD>
				<asp:TextBox id="txtStreet2" tabIndex="9" runat="server" Width="200px"></asp:TextBox></TD>
		</TR>
		<TR>
			<TD>Sex:</TD>
			<TD>
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
			<TD>
				<asp:TextBox id="txtPhone" tabIndex="4" runat="server" Width="200px"></asp:TextBox></TD>
			<TD>State / Province:</TD>
			<TD>
				<asp:TextBox id="txtState" tabIndex="11" runat="server" Width="200px"></asp:TextBox></TD>
		</TR>
		<TR>
			<TD>Fax:</TD>
			<TD>
				<asp:TextBox id="txtFax" tabIndex="5" runat="server" Width="200px"></asp:TextBox></TD>
			<TD>Postal Code:</TD>
			<TD>
				<asp:TextBox id="txtPostalCode" tabIndex="12" runat="server" Width="200px"></asp:TextBox></TD>
		</TR>
		<TR>
			<TD>Email:</TD>
			<TD>
				<asp:TextBox id="txtEmail" tabIndex="6" runat="server" Width="200px"></asp:TextBox>
            </TD>
			<TD>Country:</TD>
			<TD>
				<asp:DropDownList id="dropCountry" tabIndex="13" runat="server" Width="200px"></asp:DropDownList>
				<asp:CompareValidator id="CompareValidator1" runat="server" ControlToValidate="dropCountry" ErrorMessage="Country"
					ValueToCompare="0" Operator="NotEqual">*</asp:CompareValidator></TD>
		</TR>
		<TR>
			<TD>Mobile:</TD>
			<TD>
				<asp:TextBox id="txtMobile" tabIndex="7" runat="server" Width="200px"></asp:TextBox></TD>
			<TD></TD>
			<TD></TD>
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
			<TD>
				<asp:Button id="ButtonSave" tabIndex="13" runat="server" Text="Save Information" onclick="ButtonSave_Click"></asp:Button><br />
				<asp:Label id="LabelError" runat="server" ForeColor="Red"></asp:Label>
				</TD>
			<TD></TD>
			<TD></TD>
		</TR>
	</TABLE>
    </div>
    </div>
</asp:Content>

