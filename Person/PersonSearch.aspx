<%@ Page Language="C#" MasterPageFile="~/PATS.master" AutoEventWireup="true" CodeFile="PersonSearch.aspx.cs" Inherits="Person_PersonSearch" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="width:100%; clear:both; text-align: left;">
<h1>
    <asp:Label ID="LabelHeader" runat="server" Text=""></asp:Label></h1>
    <TABLE id="Table2" cellSpacing="1" cellPadding="1" width="900" align="center" border="0">
	<TR>
		<TD>First Name:</TD>
		<TD>
			<asp:TextBox id="txtFirstName" tabIndex="1" Width="200px" runat="server"></asp:TextBox></TD>
		<TD>Street 1:</TD>
		<TD>
			<asp:TextBox id="txtStreet1" tabIndex="8" Width="200px" runat="server"></asp:TextBox></TD>
	</TR>
	<TR>
		<TD height="25">Last Name:</TD>
		<TD height="25">
			<asp:TextBox id="txtLastName" tabIndex="2" Width="200px" runat="server"></asp:TextBox></TD>
		<TD height="25">Street 2:</TD>
		<TD height="25">
			<asp:TextBox id="txtStreet2" tabIndex="9" Width="200px" runat="server"></asp:TextBox></TD>
	</TR>
	<TR>
		<TD>Phone:</TD>
		<TD>
			<asp:TextBox id="txtPhone" tabIndex="4" Width="200px" runat="server"></asp:TextBox></TD>
		<TD>City:</TD>
		<TD>
			<asp:TextBox id="txtCity" tabIndex="10" Width="200px" runat="server"></asp:TextBox></TD>
	</TR>
	<TR>
		<TD>Fax:</TD>
		<TD>
			<asp:TextBox id="txtFax" tabIndex="5" Width="200px" runat="server"></asp:TextBox></TD>
		<TD>State / Province:</TD>
		<TD>
			<asp:TextBox id="txtState" tabIndex="11" Width="200px" runat="server"></asp:TextBox></TD>
	</TR>
	<TR>
		<TD>Email:</TD>
		<TD>
			<asp:TextBox id="txtEmail" tabIndex="6" Width="200px" runat="server"></asp:TextBox></TD>
		<TD>Postal Code:</TD>
		<TD>
			<asp:TextBox id="txtPostalCode" tabIndex="12" Width="200px" runat="server"></asp:TextBox></TD>
	</TR>
	<TR>
		<TD>Mobile:</TD>
		<TD>
			<asp:TextBox id="txtMobile" tabIndex="7" Width="200px" runat="server"></asp:TextBox></TD>
		<TD>Country:</TD>
		<TD>
			<asp:DropDownList id="dropCountry" tabIndex="13" Width="200px" runat="server"></asp:DropDownList></TD>
	</TR>
	<TR>
		<TD>Active User:</TD>
		<TD>
			<asp:RadioButtonList id="rblstActive" runat="server" RepeatDirection="Horizontal">
				<asp:ListItem Value="No">No</asp:ListItem>
				<asp:ListItem Value="Yes">Yes</asp:ListItem>
			</asp:RadioButtonList></TD>
		<TD></TD>
		<TD></TD>
	</TR>
	<TR>
		<TD></TD>
		<TD>
			<asp:Button id="ButtonSearch" tabIndex="13" Width="80px" runat="server" Text="Search" onclick="ButtonSearch_Click"></asp:Button>&nbsp;
			<asp:Button id="ButtonNew" tabIndex="15" Width="80px" runat="server" Text="New Search" onclick="ButtonNew_Click"></asp:Button></TD>
		<TD colSpan="2"></TD>
	</TR>
</TABLE>
<br />
<asp:Label id="LabelResultCount" runat="server" ForeColor="SteelBlue" Font-Bold="True"></asp:Label><br />
<asp:DataGrid id="dgResults" Width="900px" runat="server" Visible="False">
    <AlternatingItemStyle BackColor="Gainsboro" />
    <HeaderStyle BackColor="Silver" Font-Bold="True" />
    </asp:DataGrid>
</div>
</asp:Content>

