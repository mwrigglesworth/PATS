<%@ Page Language="C#" MasterPageFile="~/PATS.master" AutoEventWireup="true" CodeFile="PatientSearch.aspx.cs" Inherits="Patient_PatientSearch" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="width:100%; clear:both; text-align: left;">
<h1>Patient Search</h1>
<asp:Panel id="PanelSearch" runat="server">
	<table id="Table2" cellSpacing="1" cellPadding="1" width="900" align="center" border="0">
		<tr>
			<td>First Name:</td>
			<td>
				<asp:textbox id="txtFirstName" runat="server"></asp:textbox></td>
			<td>Last Name:</td>
			<td>
				<asp:textbox id="txtLastName" runat="server"></asp:textbox></td>
		</tr>
		<tr>
			<td>PIN:</td>
			<td>
				<asp:textbox id="txtPIN" runat="server"></asp:textbox></td>
			<td>Phone:</td>
			<td>
				<asp:textbox id="txtPhone" runat="server"></asp:textbox></td>
		</tr>
		<tr>
			<td>City:</td>
			<td>
				<asp:textbox id="txtCity" runat="server"></asp:textbox></td>
			<td>Fax:</td>
			<td>
				<asp:textbox id="txtFax" runat="server"></asp:textbox></td>
		</tr>
		<tr>
			<td>State:</td>
			<td>
				<asp:textbox id="txtState" runat="server"></asp:textbox></td>
			<td>Email:</td>
			<td>
				<asp:textbox id="txtEmail" runat="server"></asp:textbox></td>
		</tr>
		<tr>
			<td>Country:</td>
			<td>
				<asp:dropdownlist id="dropCountry" runat="server" Width="154px"></asp:dropdownlist></td>
			<td>Gender:</td>
			<td>
				<asp:radiobuttonlist id="rblstGender" runat="server" RepeatDirection="Horizontal">
					<asp:ListItem Value="M">Male</asp:ListItem>
					<asp:ListItem Value="F">Female</asp:ListItem>
				</asp:radiobuttonlist></td>
		</tr>
		<tr>
			<td>Birthdate:</td>
			<td>
				<asp:dropdownlist id="dropBirthDay" runat="server" >
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
				<asp:dropdownlist id="dropBirthMonth" runat="server">
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
				<asp:dropdownlist id="dropBirthYear" runat="server">
					<asp:ListItem Value="0" Selected="True">Year</asp:ListItem>
				</asp:dropdownlist></td>
			<td>through:</td>
			<td>
				<asp:dropdownlist id="dropBirthDayThru" runat="server">
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
				<asp:dropdownlist id="dropBirthMonthThru" runat="server">
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
				<asp:dropdownlist id="dropBirthYearThru" runat="server">
					<asp:ListItem Value="0" Selected="True">Year</asp:ListItem>
				</asp:dropdownlist></td>
		</tr>
		<tr>
			<td colSpan="4"><STRONG>
                <font color="steelblue">Physician / PO / Max Station / 
                Program</font></STRONG></td>
		</tr>
		<tr>
			<td>Physician:</td>
			<td>
				<asp:dropdownlist id="dropPhysician" runat="server"></asp:dropdownlist></td>
			<td>Program Officer:</td>
			<td>
				<asp:dropdownlist id="dropSocial" runat="server"></asp:dropdownlist></td>
		</tr>
		<tr>
			<td>Program:</td>
			<td>
                <asp:DropDownList ID="dropProgram" runat="server">
                    <asp:ListItem Value="0">Select One</asp:ListItem>
                    <asp:ListItem>GIPAP</asp:ListItem>
                    <asp:ListItem>NOA-GIPAP</asp:ListItem>
                    <asp:ListItem>NOA</asp:ListItem>
                    <asp:ListItem>NOA-Tasigna</asp:ListItem>
                    <asp:ListItem>MYPAP</asp:ListItem>
                    <asp:ListItem>TIPAP</asp:ListItem>
                </asp:DropDownList>
            </td>
			<td>
				<asp:Label id="LabelMax" runat="server">Max Station:</asp:Label></td>
			<td>
				<asp:dropdownlist id="dropMax" runat="server"></asp:dropdownlist></td>
		</tr>
		<tr>
			<td colSpan="4"></td>
		</tr>
	</table>
	<table id="Table7" cellSpacing="1" cellPadding="1" width="900" align="center" border="0">
		<tr>
			<td colSpan="4"><STRONG>
                <FONT color="steelblue">GIPAP Status Information</FONT></STRONG></td>
		</tr>
		<tr>
			<td>GIPAP Status:</td>
			<td>
				<asp:dropdownlist id="dropStatus" runat="server" AutoPostBack="True" onselectedindexchanged="dropStatus_SelectedIndexChanged">
					<asp:ListItem Value="0">Select Status</asp:ListItem>
					<asp:ListItem Value="Active">Active</asp:ListItem>
					<asp:ListItem Value="Pending">Pending</asp:ListItem>
					<asp:ListItem Value="Closed">Closed</asp:ListItem>
					<asp:ListItem Value="Denied">Denied</asp:ListItem>
				</asp:dropdownlist></td>
			<td>Status Reason:</td>
			<td>
				<asp:dropdownlist id="dropStatReason" runat="server"></asp:dropdownlist></td>
		</tr>
		<tr>
			<td>Intake Date:</td>
			<td>
				<asp:dropdownlist id="dropIntakeDay" runat="server">
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
				<asp:dropdownlist id="dropIntakeMonth" runat="server">
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
				<asp:dropdownlist id="dropIntakeYear" runat="server">
					<asp:ListItem Value="0" Selected="True">Year</asp:ListItem>
				</asp:dropdownlist></td>
			<td>through:</td>
			<td>
				<asp:dropdownlist id="DropIntakeDayThru" runat="server">
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
				<asp:dropdownlist id="dropIntakeMonthThru" runat="server">
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
				<asp:dropdownlist id="dropIntakeYearThru" runat="server">
					<asp:ListItem Value="0" Selected="True">Year</asp:ListItem>
				</asp:dropdownlist></td>
		</tr>
		<tr>
			<td>Initail Approval:</td>
			<td>
				<asp:dropdownlist id="dropIADay" runat="server" >
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
				<asp:dropdownlist id="dropIAMonth" runat="server">
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
				<asp:dropdownlist id="dropIAYear" runat="server">
					<asp:ListItem Value="0" Selected="True">Year</asp:ListItem>
				</asp:dropdownlist></td>
			<td>through:</td>
			<td>
				<asp:dropdownlist id="DropIADayThru" runat="server">
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
				<asp:dropdownlist id="DropIAMonthThru" runat="server">
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
				<asp:dropdownlist id="dropIAYearThru" runat="server">
					<asp:ListItem Value="0" Selected="True">Year</asp:ListItem>
				</asp:dropdownlist></td>
		</tr>
		<tr>
			<td>Start Date:</td>
			<td>
				<asp:dropdownlist id="dropStartDay" runat="server">
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
				<asp:dropdownlist id="dropStartMonth" runat="server">
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
				<asp:dropdownlist id="dropStartYear" runat="server">
					<asp:ListItem Value="0" Selected="True">Year</asp:ListItem>
				</asp:dropdownlist></td>
			<td>through:</td>
			<td>
				<asp:dropdownlist id="dropStartDayThru" runat="server">
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
				<asp:dropdownlist id="dropStartMonthThru" runat="server">
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
				<asp:dropdownlist id="dropStartYearThru" runat="server">
					<asp:ListItem Value="0" Selected="True">Year</asp:ListItem>
				</asp:dropdownlist></td>
		</tr>
		<tr>
			<td>End Date:</td>
			<td>
				<asp:dropdownlist id="dropEndDay" runat="server">
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
				<asp:dropdownlist id="dropEndMonth" runat="server">
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
				<asp:dropdownlist id="dropEndYear" runat="server">
					<asp:ListItem Value="0" Selected="True">Year</asp:ListItem>
				</asp:dropdownlist></td>
			<td>through:</td>
			<td>
				<asp:dropdownlist id="dropEndDayThru" runat="server">
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
				<asp:dropdownlist id="dropEndMonthThru" runat="server">
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
				<asp:dropdownlist id="dropEndYearThru" runat="server">
					<asp:ListItem Value="0" Selected="True">Year</asp:ListItem>
				</asp:dropdownlist></td>
		</tr>
		<tr>
			<td>Close Date:</td>
			<td>
				<asp:dropdownlist id="dropCloseDay" runat="server">
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
				<asp:dropdownlist id="dropCloseMonth" runat="server">
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
				<asp:dropdownlist id="dropCloseYear" runat="server">
					<asp:ListItem Value="0" Selected="True">Year</asp:ListItem>
				</asp:dropdownlist></td>
			<td>through:</td>
			<td>
				<asp:dropdownlist id="dropCloseDayThru" runat="server">
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
				<asp:dropdownlist id="dropCloseMonthThru" runat="server">
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
				<asp:dropdownlist id="DropCloseYearThru" runat="server">
					<asp:ListItem Value="0" Selected="True">Year</asp:ListItem>
				</asp:dropdownlist></td>
		</tr>
		<tr>
			<td colSpan="4"><STRONG>
                <FONT color="steelblue">Patient Eligabiliby</FONT></STRONG></td>
		</tr>
		<tr>
			<td>Diagnosis Date:</td>
			<td>
				<asp:dropdownlist id="dropDiagDay" runat="server">
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
				<asp:dropdownlist id="dropDiagMonth" runat="server">
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
				<asp:dropdownlist id="dropDiagYear" runat="server">
					<asp:ListItem Value="0" Selected="True">Year</asp:ListItem>
				</asp:dropdownlist></td>
			<td>through:</td>
			<td>
				<asp:dropdownlist id="dropDiagDayThru" runat="server">
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
				<asp:dropdownlist id="dropDiagMonthThru" runat="server">
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
				<asp:dropdownlist id="dropDiagYearThru" runat="server">
					<asp:ListItem Value="0" Selected="True">Year</asp:ListItem>
				</asp:dropdownlist></td>
		</tr>
		<tr>
			<td>Diagnosis:</td>
			<td>
				<asp:dropdownlist id="dropDiagnosis" runat="server" name="dropDiagnosis"
                    onchange="validateAdjuvantGIST();"  >
					<asp:ListItem Value="Select One">Select One</asp:ListItem>
					<asp:ListItem Value="CML">CML</asp:ListItem>
					<asp:ListItem Value="Ph+ ALL">Ph+ ALL</asp:ListItem>
					<asp:ListItem Value="GIST">GIST</asp:ListItem>
					<asp:ListItem Value="DFSP">DFSP</asp:ListItem>
				    <asp:ListItem Enabled="False">Adjuvant GIST</asp:ListItem>
                    <asp:ListItem>MDS / MPD</asp:ListItem>
                    <asp:ListItem>Systemic Mastocytosis</asp:ListItem>
                    <asp:ListItem>HES / CEL</asp:ListItem>
				</asp:dropdownlist>
                <%--&nbsp;<asp:CheckBox ID="CheckBox1" runat="server" style="align:right;"
                    Font-Size="Small" Text="Adjuvant" TextAlign="Left" />--%>
            </td>
            
			<td runat="server" class="adjuvant" >Adjuvant GIST:</td>
			<td><asp:CheckBox ID="chkAdjuvant" runat="server"  class="adjuvant" 
                     Text="" TextAlign="Left" />
				</td>
                
		</tr>
		<tr>
			<td>Philadelphia + :</td>
			<td>
				<asp:radiobuttonlist id="rblstPhilPos" runat="server" RepeatDirection="Horizontal">
					<asp:ListItem Value="1">Yes</asp:ListItem>
					<asp:ListItem Value="0">No</asp:ListItem>
					<asp:ListItem Value="2">Not Sure</asp:ListItem>
				</asp:radiobuttonlist></td>
			<td>Dosage:</td>
			<td><asp:DropDownList id="dropDosage" runat="server">
					<asp:ListItem Value="0">Select One</asp:ListItem>
					<asp:ListItem Value="200mg">200mg</asp:ListItem>
					<asp:ListItem Value="260mg">260mg</asp:ListItem>
					<asp:ListItem Value="300mg">300mg</asp:ListItem>
					<asp:ListItem Value="400mg">400mg</asp:ListItem>
					<asp:ListItem Value="600mg">600mg</asp:ListItem>
					<asp:ListItem Value="800mg">800mg</asp:ListItem>
					<asp:ListItem Value="N/A">N/A</asp:ListItem>
				</asp:DropDownList>
				</td>
		</tr>
		<tr>
			<td>Interferon:</td>
			<td>
				<asp:RadioButtonList id="rblstInterferon" runat="server" RepeatDirection="Horizontal">
					<asp:ListItem Value="1">Yes</asp:ListItem>
					<asp:ListItem Value="0">No</asp:ListItem>
				</asp:RadioButtonList></td>
			<td>CML Phase:</td>
			<td><asp:dropdownlist id="dropCurrentPhase" runat="server">
					<asp:ListItem Value="Select CML Phase">Select CML Phase</asp:ListItem>
					<asp:ListItem Value="Accelerated">Accelerated</asp:ListItem>
					<asp:ListItem Value="Blast Crisis">Blast Crisis</asp:ListItem>
					<asp:ListItem Value="Chronic">Chronic</asp:ListItem>
				</asp:dropdownlist></td>
		</tr>
		<tr>
			<td>Refractory:</td>
			<td>
				<asp:RadioButtonList id="rblstRefractory" runat="server" RepeatDirection="Horizontal">
					<asp:ListItem Value="1">Yes</asp:ListItem>
					<asp:ListItem Value="0">No</asp:ListItem>
				</asp:RadioButtonList></td>
			<td>Unresponsive:</td>
			<td>
				<asp:RadioButtonList id="rblstUnresponsive" runat="server" RepeatDirection="Horizontal">
					<asp:ListItem Value="1">Yes</asp:ListItem>
					<asp:ListItem Value="0">No</asp:ListItem>
				</asp:RadioButtonList></td>
		</tr>
		<tr>
			<td>Hematologic Failure:</td>
			<td>
				<asp:RadioButtonList id="rblstHema" runat="server" RepeatDirection="Horizontal">
					<asp:ListItem Value="1">Yes</asp:ListItem>
					<asp:ListItem Value="0">No</asp:ListItem>
				</asp:RadioButtonList></td>
			<td>Cytogenetic Failure:</td>
			<td>
				<asp:RadioButtonList id="rblstCyto" runat="server" RepeatDirection="Horizontal">
					<asp:ListItem Value="1">Yes</asp:ListItem>
					<asp:ListItem Value="0">No</asp:ListItem>
				</asp:RadioButtonList></td>
		</tr>
		<tr>
			<td>CKit +:</td>
			<td>
				<asp:RadioButtonList id="rblstCKit" runat="server" RepeatDirection="Horizontal">
					<asp:ListItem Value="1">Yes</asp:ListItem>
					<asp:ListItem Value="0">No</asp:ListItem>
					<asp:ListItem Value="Not Sure">Not Sure</asp:ListItem>
				</asp:RadioButtonList></td>
			<td>Health Insurance:</td>
			<td>
				<asp:RadioButtonList id="rblstInsurance" runat="server" RepeatDirection="Horizontal">
					<asp:ListItem Value="1">Yes</asp:ListItem>
					<asp:ListItem Value="0">No</asp:ListItem>
				</asp:RadioButtonList></td>
		</tr>
	</table>
	<table id="Table5" cellSpacing="1" cellPadding="1" width="900" align="center" border="0">
		<tr>
			<td align="center">
				<asp:Button id="ButtonSearch" runat="server" Width="80px" Text="Search" onclick="ButtonSearch_Click"></asp:Button></td>
			<td align="center">
				<asp:Button id="ButtonNew" runat="server" Width="80px" Text="New Search" onclick="ButtonNew_Click"></asp:Button></td>
		</tr>
	</table>
</asp:Panel>
<asp:Panel id="PanelResults" runat="server" Visible="False">
	<p><asp:label id="LabelResultCount" runat="server" ForeColor="SteelBlue" Font-Bold="True"></asp:label></p>
	<div align="center">
		<asp:DataGrid id="DGResults" runat="server" Width="900px" CellPadding="0" AlternatingItemStyle-BackColor="Gainsboro">
            <AlternatingItemStyle BackColor="Gainsboro" />
            <HeaderStyle BackColor="Silver" />
        </asp:DataGrid></div>
</asp:Panel>
</div>
<script language="javascript">
    //**********************************************************************************************************************
    function validateAdjuvantGIST() {
        var dropDiag = document.getElementById('<%=dropDiagnosis.ClientID %>');

        if (dropDiag.options[dropDiag.selectedIndex].value == "GIST") {
            
           
            document.getElementsByClassName('adjuvant')[0].style.visibility = 'visible';
            document.getElementsByClassName('adjuvant')[1].style.visibility = 'visible'; 
        }
        else {
            
            
            document.getElementsByClassName('adjuvant')[0].style.visibility = 'hidden';
            document.getElementsByClassName('adjuvant')[1].style.visibility = 'hidden'; 
        }
    }

    //**********************************************************************************************************************

    window.onload = validateAdjuvantGIST(); 
</script>
</asp:Content>

