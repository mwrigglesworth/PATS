<%@ Page Language="C#" MasterPageFile="~/PATS.master" AutoEventWireup="true" CodeFile="ClinicSearch.aspx.cs" Inherits="Clinic_ClinicSearch" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="width:100%; clear:both; text-align: left;">
<h1>Clinic Search</h1>
<TABLE id="Table3" cellSpacing="1" cellPadding="1" width="900" align="center" border="0">
	<TR>
		<TD>Clinic Name:</TD>
		<TD>
			<asp:textbox id="txtClinicName" tabIndex="1" Width="200px" runat="server"></asp:textbox></TD>
		<TD>City:</TD>
		<TD>
			<asp:textbox id="txtClinicCity" tabIndex="4" Width="200px" runat="server"></asp:textbox></TD>
	</TR>
	<TR>
		<TD>Phone:</TD>
		<TD>
			<asp:textbox id="txtClinicPhone" tabIndex="8" Width="200px" runat="server" DESIGNTIMEDRAGDROP="574"></asp:textbox></TD>
		<TD>Country:</TD>
		<TD>
			<asp:dropdownlist id="dropCountry" tabIndex="7" Width="200px" runat="server"></asp:dropdownlist></TD>
	</TR>
	<TR>
		<TD>Fax:</TD>
		<TD>
			<asp:textbox id="txtClinicFax" tabIndex="9" Width="200px" runat="server"></asp:textbox></TD>
		<TD></TD>
		<TD></TD>
	</TR>
	<TR>
		<TD>Email:</TD>
		<TD>
			<asp:textbox id="txtClinicEmail" tabIndex="10" Width="200px" runat="server"></asp:textbox></TD>
		<TD>Approved:</TD>
		<TD>
			<asp:RadioButtonList id="rblstApproved" runat="server" RepeatDirection="Horizontal">
				<asp:ListItem Value="No">No</asp:ListItem>
				<asp:ListItem Value="Yes">Yes</asp:ListItem>
				<asp:ListItem Value="Pending">Pending</asp:ListItem>
			</asp:RadioButtonList></TD>
	</TR>
	<TR>
		<TD colSpan="4"><STRONG><FONT color="steelblue">Administrator&nbsp;Information</FONT></STRONG></TD>
	</TR>
	<TR>
		<TD>First Name:</TD>
		<TD>
			<asp:textbox id="txtAdminFirstName" tabIndex="11" Width="200px" runat="server"></asp:textbox></TD>
		<TD>Phone:</TD>
		<TD>
			<asp:textbox id="txtAdminPhone" tabIndex="13" Width="200px" runat="server"></asp:textbox></TD>
	</TR>
	<TR>
		<TD>Last Name:</TD>
		<TD>
			<asp:textbox id="txtAdminLastName" tabIndex="12" Width="200px" runat="server"></asp:textbox></TD>
		<TD>Fax:</TD>
		<TD>
			<asp:textbox id="txtAdminFax" tabIndex="14" Width="200px" runat="server"></asp:textbox></TD>
	</TR>
	<TR>
		<TD></TD>
		<TD></TD>
		<TD>Email:</TD>
		<TD>
			<asp:textbox id="txtAdminEmail" tabIndex="15" Width="200px" runat="server"></asp:textbox></TD>
	</TR>
	<TR>
		<TD></TD>
		<TD>
			<asp:Button id="ButtonSearch" runat="server" Width="80px" Text="Search" onclick="ButtonSearch_Click"></asp:Button>&nbsp;
			<asp:Button id="ButtonNew" runat="server" Width="80px" Text="New Search" onclick="ButtonNew_Click"></asp:Button></TD>
		<TD></TD>
		<TD></TD>
	</TR>
</TABLE>
<br />
<asp:Label id="LabelResultCount" runat="server" ForeColor="Steelblue" Font-Bold="True"></asp:Label><br />
<asp:DataGrid id="dgResults" Width="900px" runat="server" Visible="False">
    <AlternatingItemStyle BackColor="Gainsboro" />
    <HeaderStyle BackColor="Silver" Font-Bold="True" />
    </asp:DataGrid>
</div>
</asp:Content>

