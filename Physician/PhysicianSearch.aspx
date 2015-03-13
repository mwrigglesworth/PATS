<%@ Page Language="C#" MasterPageFile="~/PATS.master" AutoEventWireup="true" CodeFile="PhysicianSearch.aspx.cs" Inherits="Physician_PhysicianSearch" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="width:100%; clear:both; text-align: left;">
<h1>Physician Search</h1>
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
		<TD>Specialty:</TD>
		<TD>
			<asp:dropdownlist id="dropSpecialty" tabIndex="3" Width="200px" runat="server">
				<asp:ListItem Value="Select One">Select One</asp:ListItem>
				<asp:ListItem Value="Hematology">Hematology</asp:ListItem>
				<asp:ListItem Value="Gastroenterology">Gastroenterology</asp:ListItem>
				<asp:ListItem Value="Oncology">Oncology</asp:ListItem>
				<asp:ListItem Value="Oncohematology">Oncohematology</asp:ListItem>
			</asp:dropdownlist></TD>
		<TD>City:</TD>
		<TD>
			<asp:TextBox id="txtCity" tabIndex="10" Width="200px" runat="server"></asp:TextBox></TD>
	</TR>
	<TR>
		<TD>Phone:</TD>
		<TD>
			<asp:TextBox id="txtPhone" tabIndex="4" Width="200px" runat="server"></asp:TextBox></TD>
		<TD>State / Province:</TD>
		<TD>
			<asp:TextBox id="txtState" tabIndex="11" Width="200px" runat="server"></asp:TextBox></TD>
	</TR>
	<TR>
		<TD>Fax:</TD>
		<TD>
			<asp:TextBox id="txtFax" tabIndex="5" Width="200px" runat="server"></asp:TextBox></TD>
		<TD>Postal Code:</TD>
		<TD>
			<asp:TextBox id="txtPostalCode" tabIndex="12" Width="200px" runat="server"></asp:TextBox></TD>
	</TR>
	<TR>
		<TD>Email:</TD>
		<TD>
			<asp:TextBox id="txtEmail" tabIndex="6" Width="200px" runat="server"></asp:TextBox></TD>
		<TD>Country:</TD>
		<TD>
			<asp:DropDownList id="dropCountry" tabIndex="13" Width="200px" runat="server"></asp:DropDownList></TD>
	</TR>
	<TR>
		<TD>Mobile:</TD>
		<TD>
			<asp:TextBox id="txtMobile" tabIndex="7" Width="200px" runat="server"></asp:TextBox></TD>
		<TD>Approved:</TD>
		<TD>
			<asp:RadioButtonList id="rblstApproved" runat="server" RepeatDirection="Horizontal">
				<asp:ListItem Value="No">No</asp:ListItem>
				<asp:ListItem Value="Yes">Yes</asp:ListItem>
				<asp:ListItem Value="Pending">Pending</asp:ListItem>
			</asp:RadioButtonList></TD>
	</TR>
	<tr>
		<td>Username:</td>
		<td>
			<asp:TextBox id="txtUsername" tabIndex="7" runat="server" Width="200px"></asp:TextBox></td>
		<td>Computer Access:</td>
		<td>
			<asp:RadioButtonList id="rblstComputerAccess" runat="server" RepeatDirection="Horizontal">
				<asp:ListItem Value="No">No</asp:ListItem>
				<asp:ListItem Value="Yes">Yes</asp:ListItem>
				<asp:ListItem Value="Limited">Limited</asp:ListItem>
			</asp:RadioButtonList></td>
	</tr>
	<TR>
		<TD >Approved Date:</TD>
		<TD >
			<asp:dropdownlist id="dropApprovedDay" tabIndex="22" runat="server" Width="51px">
				<asp:ListItem Value="0" Selected="True">Day</asp:ListItem>
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
			</asp:dropdownlist>
			<asp:dropdownlist id="dropApprovedMonth" tabIndex="5" runat="server">
				<asp:ListItem Value="0" Selected="True">Month</asp:ListItem>
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
			</asp:dropdownlist>
			<asp:dropdownlist id="dropApprovedYear" tabIndex="24" runat="server">
				<asp:ListItem Value="0" Selected="True">Year</asp:ListItem>
			</asp:dropdownlist></TD>
		<TD >thru:</TD>
		<TD>
			<asp:dropdownlist id="dropApprovedDayThru" tabIndex="22" runat="server" Width="51px">
				<asp:ListItem Value="0" Selected="True">Day</asp:ListItem>
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
			</asp:dropdownlist>
			<asp:dropdownlist id="dropApprovedMonthThru" tabIndex="5" runat="server">
				<asp:ListItem Value="0" Selected="True">Month</asp:ListItem>
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
			</asp:dropdownlist>
			<asp:dropdownlist id="dropApprovedYearThru" tabIndex="24" runat="server">
				<asp:ListItem Value="0" Selected="True">Year</asp:ListItem>
			</asp:dropdownlist></TD>
	</TR>
	<TR>
		<TD></TD>
		<TD style="height:50px;">
			<asp:Button id="ButtonSearch" tabIndex="13" Width="80px" runat="server" Text="Search" onclick="ButtonSearch_Click"></asp:Button>&nbsp;
			<asp:Button id="ButtonNew" tabIndex="15" runat="server" Text="New Search" 
                onclick="ButtonNew_Click"></asp:Button></TD>
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

