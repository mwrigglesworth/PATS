<%@ Page Language="C#" MasterPageFile="~/Country/GIPAPCountry.master" AutoEventWireup="true" CodeFile="EditCountry.aspx.cs" Inherits="Country_EditCountry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderControlPanel" Runat="Server">
    <h1>Edit Country Information</h1>
<TABLE id="Table7" cellSpacing="1" cellPadding="1" width="585" align="left" border="0">
		<TR>
			<TD><asp:validationsummary id="ValidationSummary1" runat="server" HeaderText="You are missing the following fields:"
		ShowMessageBox="True" CssClass="AlertDiv" ForeColor=""></asp:validationsummary></TD>
		</TR>
		</TABLE>
    <div class="FormTable" style="clear:both;">
<TABLE id="Table1" cellSpacing="1" cellPadding="1" width="600" border="0">
		<TR>
			<TD colSpan="2">
				<asp:Label id="LabelCountryName" ForeColor="White" runat="server" 
                    Font-Bold="True"></asp:Label></TD>
		</TR>
		<TR>
			<TD width="210">Sub Region:</TD>
			<TD>
				<asp:DropDownList id="dropSubRegion" runat="server" Width="200px"></asp:DropDownList></TD>
		</TR>
		<TR>
			<TD width="210">Active GIPAP:</TD>
			<TD>
				<asp:RadioButtonList id="rblstActive" runat="server" RepeatDirection="Horizontal">
					<asp:ListItem Value="No">No</asp:ListItem>
					<asp:ListItem Value="Yes">Yes</asp:ListItem>
					<asp:ListItem Value="Pending">Pending</asp:ListItem>
				</asp:RadioButtonList></TD>
		</TR>
		<TR>
			<TD width="210">Financial Information Required:</TD>
			<TD>
				<asp:RadioButtonList id="rblstNeedFinInfo" runat="server" RepeatDirection="Horizontal">
					<asp:ListItem Value="No">No</asp:ListItem>
					<asp:ListItem Value="Yes">Yes</asp:ListItem>
				</asp:RadioButtonList></TD>
		</TR>
		<TR>
			<TD width="210">Financial Declaration Required By:</TD>
			<TD>
				<asp:DropDownList id="DropFinDay" runat="server">
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
					<asp:ListItem></asp:ListItem>
				</asp:DropDownList>
				<asp:DropDownList id="dropFinMonth" runat="server">
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
				<asp:DropDownList id="dropFinYear" runat="server">
					<asp:ListItem Value="0">Year</asp:ListItem>
				</asp:DropDownList></TD>
		</TR>
		<TR>
			<TD width="210">Email:</TD>
			<TD>
				<asp:TextBox id="txtEmail" runat="server" Width="250px"></asp:TextBox></TD>
		</TR>
		<TR>
			<TD width="210">Adverse Event Email:</TD>
			<TD>
				<asp:TextBox id="txtsaeEmail" runat="server" Width="250px"></asp:TextBox></TD>
		</TR>
		<TR>
			<TD>Pediatric Age:</TD>
			<TD>
				<asp:TextBox id="txtPediatricAge" runat="server" Width="50px"></asp:TextBox>
				<asp:CompareValidator id="CompareValidator4" runat="server" Type="Integer" ErrorMessage="Pediatric Age must be a number"
					ControlToValidate="txtPediatricAge" Operator="DataTypeCheck">*</asp:CompareValidator></TD>
		</TR>
		<TR>
			<TD>Accepting New Applications:</TD>
			<TD>
				<asp:RadioButtonList id="rblstAcceptingNewApps" runat="server" RepeatDirection="Horizontal">
					<asp:ListItem Value="No" Selected="True">No</asp:ListItem>
					<asp:ListItem Value="Yes">Yes</asp:ListItem>
				</asp:RadioButtonList></TD>
		</TR>
		<TR>
			<TD>Additional GIPAP Protocol:</TD>
			<TD>
				<asp:TextBox id="txtNotes" runat="server" Width="250px" Height="88px" TextMode="MultiLine"></asp:TextBox></TD>
		</TR>
		<TR>
			<TD colSpan="2">
				<HR color="steelblue">
				<FONT color="steelblue"><B>NOA Information</B></FONT></TD>
		</TR>
		<TR>
			<TD>Approved For NOA-Glivec:<br /><font class=Subtext>Physicians will have to be approved for NOA-Glivec individually, this setting only allows for physicians to be moved into NOA for this country</font></TD>
			<TD>
				<asp:RadioButtonList id="rblstNOAGlivec" runat="server" RepeatDirection="Horizontal">
					<asp:ListItem Value="No">No</asp:ListItem>
					<asp:ListItem Value="Yes">Yes</asp:ListItem>
				</asp:RadioButtonList></TD>
		</TR>
		<TR>
			<TD>Approved For NOA-Tasigna:<br><font class=Subtext>Physicians will have to be approved for NOA-Tasigna individually</font></TD>
			<TD>
				<asp:RadioButtonList ID="rblstNoaTasigna" runat="server">
                     <asp:ListItem Selected="True">No</asp:ListItem>
                      <asp:ListItem>1st + 2nd Line</asp:ListItem>
                       <asp:ListItem>2nd Line Only</asp:ListItem>
                    </asp:RadioButtonList></TD>
		</TR>
		<TR>
			<TD>Tasigna Pediatric Patients Accepted:</TD>
			<TD>
				<asp:RadioButtonList id="rblstTasignaPedApproved" runat="server" RepeatDirection="Horizontal">
					<asp:ListItem Value="No">No</asp:ListItem>
					<asp:ListItem Value="Yes">Yes</asp:ListItem>
				</asp:RadioButtonList></TD>
		</TR>
		<TR>
			<TD colSpan="2">
				<HR color="steelblue">
				<FONT color="steelblue"><B>CML Information</B></FONT></TD>
		</TR>
		<TR>
			<TD>CML Approved:</TD>
			<TD>
				<asp:RadioButtonList id="rblstCMLApproved" runat="server" RepeatDirection="Horizontal">
					<asp:ListItem Value="No">No</asp:ListItem>
					<asp:ListItem Value="Yes">Yes</asp:ListItem>
				</asp:RadioButtonList></TD>
		</TR>
		<TR>
			<TD>Interferon Information Required:</TD>
			<TD>
				<asp:RadioButtonList id="rblstNeedInterfInfo" runat="server" RepeatDirection="Horizontal">
					<asp:ListItem Value="No">No</asp:ListItem>
					<asp:ListItem Value="Yes">Yes</asp:ListItem>
				</asp:RadioButtonList></TD>
		</TR>
		<TR>
			<TD>CML Pediatric Patients Accepted:</TD>
			<TD>
				<asp:RadioButtonList id="rblstPediatric" runat="server" RepeatDirection="Horizontal">
					<asp:ListItem Value="No">No</asp:ListItem>
					<asp:ListItem Value="Yes">Yes</asp:ListItem>
				</asp:RadioButtonList></TD>
		</TR>
		<TR>
			<TD>Additional CML Information:</TD>
			<TD>
				<asp:TextBox id="txtCMLInfo" runat="server" Width="250px" Height="88px" TextMode="MultiLine"></asp:TextBox></TD>
		</TR>
		<TR>
			<TD colSpan="2">
				<HR color="steelblue">
				<FONT color="steelblue"><B>GIST Information</B></FONT></TD>
		</TR>
		<TR>
			<TD>GIST Approved:</TD>
			<TD>
				<asp:RadioButtonList id="rblstGistApproved" runat="server" RepeatDirection="Horizontal">
					<asp:ListItem Value="No">No</asp:ListItem>
					<asp:ListItem Value="Yes">Yes</asp:ListItem>
				</asp:RadioButtonList></TD>
		</TR>
		<TR>
			<TD>GIST Pediatric Patients Accepted:</TD>
			<TD>
				<asp:RadioButtonList id="rblstGISTPedApproved" runat="server" RepeatDirection="Horizontal">
					<asp:ListItem Value="No">No</asp:ListItem>
					<asp:ListItem Value="Yes">Yes</asp:ListItem>
				</asp:RadioButtonList></TD>
		</TR>
		<TR>
			<TD>Additional GIST Information:</TD>
			<TD>
				<asp:TextBox id="txtGISTInfo" runat="server" Width="250px" Height="88px" TextMode="MultiLine"></asp:TextBox></TD>
		</TR>
		<TR>
			<TD colSpan="2">
				<HR color="steelblue">
				<FONT color="steelblue"><B>Ph+ ALL Information</B></FONT></TD>
		</TR>
		<TR>
			<TD>Ph+ All Approved:</TD>
			<TD>
				<asp:RadioButtonList id="rblstPhAll" runat="server" Width="288px" AutoPostBack="True" onselectedindexchanged="rblstPhAll_SelectedIndexChanged">
					<asp:ListItem Value="No">No</asp:ListItem>
					<asp:ListItem Value="2nd Line Only (U.S. Label)">2nd Line Only (U.S. Label)</asp:ListItem>
					<asp:ListItem Value="1st and 2nd Line (E.U. Label)">1st and 2nd Line (E.U. Label)</asp:ListItem>
				</asp:RadioButtonList></TD>
		</TR>
		<TR>
			<TD>Ph+ All Pediatric Patients Accepted:</TD>
			<TD>
				<asp:RadioButtonList id="rblstPhallPedApproved" runat="server" RepeatDirection="Horizontal">
					<asp:ListItem Value="No">No</asp:ListItem>
					<asp:ListItem Value="Yes">Yes</asp:ListItem>
				</asp:RadioButtonList></TD>
		</TR>
		<TR>
			<TD></TD>
			<TD>
				<asp:Panel id="PanelPhAll" runat="server">Ph+ALL Approval Date:<BR>
<asp:DropDownList id="dropAllDay" runat="server">
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
					</asp:DropDownList>
<asp:DropDownList id="dropAllMonth" runat="server">
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
<asp:DropDownList id="dropAllYear" runat="server">
						<asp:ListItem Value="0">Year</asp:ListItem>
					</asp:DropDownList>
<asp:CompareValidator id="CompareValidator1" runat="server" ErrorMessage="Ph+ ALL Approval Day" ControlToValidate="dropAllDay"
						Operator="NotEqual" ValueToCompare="0">*</asp:CompareValidator>
<asp:CompareValidator id="CompareValidator2" runat="server" ErrorMessage="Ph+ ALL Approval Month" ControlToValidate="dropAllMonth"
						Operator="NotEqual" ValueToCompare="0">*</asp:CompareValidator>
<asp:CompareValidator id="CompareValidator3" runat="server" ErrorMessage="Ph+ ALL Approval Year" ControlToValidate="dropAllYear"
						Operator="NotEqual" ValueToCompare="0">*</asp:CompareValidator></asp:Panel></TD>
		</TR>
		<TR>
			<TD>Additional Ph+ ALL Information:</TD>
			<TD>
				<asp:TextBox id="txtPhAllInfo" runat="server" Width="250px" Height="88px" TextMode="MultiLine"></asp:TextBox></TD>
		</TR>
		<TR>
			<TD colSpan="2">
				<HR color="steelblue">
				<FONT color="steelblue"><B>DFSP Information</B></FONT></TD>
		</TR>
		<TR>
			<TD>DFSP Approved:</TD>
			<TD>
				<asp:RadioButtonList id="rblstDFSPApproved" runat="server" 
            RepeatDirection="Horizontal" 
            onselectedindexchanged="rblstDFSPApproved_SelectedIndexChanged" 
                    AutoPostBack="True">
					<asp:ListItem Value="No">No</asp:ListItem>
					<asp:ListItem Value="Yes">Yes</asp:ListItem>
				</asp:RadioButtonList></TD>
		</TR>
		<TR>
			<TD></TD>
			<TD>
				<asp:Panel id="PanelDFSP" runat="server">DFSP Approval Date:<BR>
<asp:DropDownList id="dropDFSPDay" runat="server">
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
					</asp:DropDownList>
<asp:DropDownList id="dropDFSPMonth" runat="server">
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
<asp:DropDownList id="dropDFSPYear" runat="server">
						<asp:ListItem Value="0">Year</asp:ListItem>
					</asp:DropDownList>
<asp:CompareValidator id="CompareValidator5" runat="server" ErrorMessage="DFSP Approval Day" ControlToValidate="dropDFSPDay"
						Operator="NotEqual" ValueToCompare="0">*</asp:CompareValidator>
<asp:CompareValidator id="CompareValidator6" runat="server" ErrorMessage="DFSP Approval Month" ControlToValidate="dropDFSPMonth"
						Operator="NotEqual" ValueToCompare="0">*</asp:CompareValidator>
<asp:CompareValidator id="CompareValidator7" runat="server" ErrorMessage="DFSP Approval Year" ControlToValidate="dropDFSPYear"
						Operator="NotEqual" ValueToCompare="0">*</asp:CompareValidator></asp:Panel></TD>
		</TR>
		<TR>
			<TD>DFSP Pediatric Patients Accepted:</TD>
			<TD>
				<asp:RadioButtonList id="rblstDFSPPedApproved" runat="server" RepeatDirection="Horizontal">
					<asp:ListItem Value="No">No</asp:ListItem>
					<asp:ListItem Value="Yes">Yes</asp:ListItem>
				</asp:RadioButtonList></TD>
		</TR>
		<TR>
			<TD width="210">Additional DFSP Information:</TD>
			<TD>
				<asp:TextBox id="txtDFSPInfo" runat="server" Width="250px" Height="88px" TextMode="MultiLine"></asp:TextBox></TD>
		</TR>
		<TR>
			<TD colSpan="2">
				<HR color="steelblue">
				<FONT color="steelblue"><B>Adjuvant GIST Information</B></FONT></TD>
		</TR>
		<TR>
			<TD>Adjuvant GIST Approved:</TD>
			<TD>
				<asp:RadioButtonList id="rblstADJGistApproved" runat="server" 
            RepeatDirection="Horizontal" AutoPostBack="True" 
                    onselectedindexchanged="rblstADJGistApproved_SelectedIndexChanged" >
					<asp:ListItem Value="No">No</asp:ListItem>
					<asp:ListItem Value="Yes">Yes</asp:ListItem>
				</asp:RadioButtonList></TD>
		</TR>
		<TR>
			<TD></TD>
			<TD>
                <asp:Panel ID="PanelADJGIST" runat="server">
				<asp:DropDownList id="dropAdjGISTDay" runat="server">
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
					</asp:DropDownList>
<asp:DropDownList id="dropAdjGISTMonth" runat="server">
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
<asp:DropDownList id="dropAdjGISTYear" runat="server">
						<asp:ListItem Value="0">Year</asp:ListItem>
					</asp:DropDownList>
<asp:CompareValidator id="CompareValidator8" runat="server" ErrorMessage="Adj GIST Approval Day" ControlToValidate="dropAdjGISTDay"
						Operator="NotEqual" ValueToCompare="0">*</asp:CompareValidator>
<asp:CompareValidator id="CompareValidator9" runat="server" ErrorMessage="Adj GIST Approval Month" ControlToValidate="dropAdjGISTMonth"
						Operator="NotEqual" ValueToCompare="0">*</asp:CompareValidator>
<asp:CompareValidator id="CompareValidator10" runat="server" ErrorMessage="Adj GIST Approval Year" ControlToValidate="dropAdjGISTYear"
						Operator="NotEqual" ValueToCompare="0">*</asp:CompareValidator>
                </asp:Panel></TD>
		</TR>
		<TR>
			<TD>Adjuvant GIST Pediatric Patients Accepted:</TD>
			<TD>
				<asp:RadioButtonList id="rblstADJGistPedApproved" runat="server" 
            RepeatDirection="Horizontal">
					<asp:ListItem Value="No">No</asp:ListItem>
					<asp:ListItem Value="Yes">Yes</asp:ListItem>
				</asp:RadioButtonList></TD>
		</TR>
		<TR>
			<TD width="210">Additional Adjuvant GIST Information:</TD>
			<TD>
				<asp:TextBox id="txtADJGistInfo" runat="server" Width="250px" 
            Height="88px" TextMode="MultiLine"></asp:TextBox></TD>
		</TR>
		<TR>
			<TD colSpan="2">
				<HR color="steelblue">
				<FONT color="steelblue"><B>MDS /MPD Information</B></FONT></TD>
		</TR>
		<TR>
			<TD>MDS / MPD Approved:</TD>
			<TD>
				<asp:RadioButtonList id="rblstMDSapproved" runat="server" 
            RepeatDirection="Horizontal" AutoPostBack="True" 
                    onselectedindexchanged="rblstMDSapproved_SelectedIndexChanged" >
					<asp:ListItem Value="No">No</asp:ListItem>
					<asp:ListItem Value="Yes">Yes</asp:ListItem>
				</asp:RadioButtonList></TD>
		</TR>
		<TR>
			<TD></TD>
			<TD>
				<asp:Panel ID="PanelMDS" runat="server">
				<asp:DropDownList id="dropMDSDay" runat="server">
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
					</asp:DropDownList>
<asp:DropDownList id="dropMDSMonth" runat="server">
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
<asp:DropDownList id="dropMDSYear" runat="server">
						<asp:ListItem Value="0">Year</asp:ListItem>
					</asp:DropDownList>
<asp:CompareValidator id="CompareValidator11" runat="server" ErrorMessage="MDS Approval Day" ControlToValidate="dropMDSDay"
						Operator="NotEqual" ValueToCompare="0">*</asp:CompareValidator>
<asp:CompareValidator id="CompareValidator12" runat="server" ErrorMessage="MDS Approval Month" ControlToValidate="dropMDSMonth"
						Operator="NotEqual" ValueToCompare="0">*</asp:CompareValidator>
<asp:CompareValidator id="CompareValidator13" runat="server" ErrorMessage="MDS Approval Year" ControlToValidate="dropMDSYear"
						Operator="NotEqual" ValueToCompare="0">*</asp:CompareValidator>
                </asp:Panel></TD>
		</TR>
		<TR>
			<TD>MDS / MPD Pediatric Patients Accepted:</TD>
			<TD>
				<asp:RadioButtonList id="rblstMDSPedApproved" runat="server" 
            RepeatDirection="Horizontal">
					<asp:ListItem Value="No">No</asp:ListItem>
					<asp:ListItem Value="Yes">Yes</asp:ListItem>
				</asp:RadioButtonList></TD>
		</TR>
		<TR>
			<TD width="210">Additional MDS / MPD Information:</TD>
			<TD>
				<asp:TextBox id="txtMDSInfo" runat="server" Width="250px" 
            Height="88px" TextMode="MultiLine"></asp:TextBox></TD>
		</TR>
		<TR>
			<TD colSpan="2">
				<HR color="steelblue">
				<FONT color="steelblue"><B>Systemic Mastocytosis Information</B></FONT></TD>
		</TR>
		<TR>
			<TD>Systemic Mastocytosis Approved:</TD>
			<TD>
				<asp:RadioButtonList id="rblstMastApproved" runat="server" 
            RepeatDirection="Horizontal" AutoPostBack="True" 
                    onselectedindexchanged="rblstMastApproved_SelectedIndexChanged" >
					<asp:ListItem Value="No">No</asp:ListItem>
					<asp:ListItem Value="Yes">Yes</asp:ListItem>
				</asp:RadioButtonList></TD>
		</TR>
		<TR>
			<TD></TD>
			<TD>
				<asp:Panel ID="PanelMast" runat="server">
				<asp:DropDownList id="dropMastDay" runat="server">
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
					</asp:DropDownList>
<asp:DropDownList id="dropMastMonth" runat="server">
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
<asp:DropDownList id="dropMastYear" runat="server">
						<asp:ListItem Value="0">Year</asp:ListItem>
					</asp:DropDownList>
<asp:CompareValidator id="CompareValidator14" runat="server" ErrorMessage="Systemic Mast. Approval Day" ControlToValidate="dropMastDay"
						Operator="NotEqual" ValueToCompare="0">*</asp:CompareValidator>
<asp:CompareValidator id="CompareValidator15" runat="server" ErrorMessage="Systemic Mast. Approval Month" ControlToValidate="dropMastMonth"
						Operator="NotEqual" ValueToCompare="0">*</asp:CompareValidator>
<asp:CompareValidator id="CompareValidator16" runat="server" ErrorMessage="Systemic Mast. Approval Year" ControlToValidate="dropMastYear"
						Operator="NotEqual" ValueToCompare="0">*</asp:CompareValidator>
                </asp:Panel></TD>
		</TR>
		<TR>
			<TD>Systemic Mastocytosis Pediatric Patients Accepted:</TD>
			<TD>
				<asp:RadioButtonList id="rblstMASTPedApproved" runat="server" 
            RepeatDirection="Horizontal">
					<asp:ListItem Value="No">No</asp:ListItem>
					<asp:ListItem Value="Yes">Yes</asp:ListItem>
				</asp:RadioButtonList></TD>
		</TR>
		<TR>
			<TD width="210">Additional Systemic Mastocytosis Information:</TD>
			<TD>
				<asp:TextBox id="txtMASTInfo" runat="server" Width="250px" 
            Height="88px" TextMode="MultiLine"></asp:TextBox></TD>
		</TR>
		<TR>
			<TD colSpan="2">
				<HR color="steelblue">
				<FONT color="steelblue"><B>HES / CEL Information</B></FONT></TD>
		</TR>
		<TR>
			<TD>HES / CEL Approved:</TD>
			<TD>
				<asp:RadioButtonList id="rblstHESApproved" runat="server" 
            RepeatDirection="Horizontal" AutoPostBack="True" 
                    onselectedindexchanged="rblstHESApproved_SelectedIndexChanged" >
					<asp:ListItem Value="No">No</asp:ListItem>
					<asp:ListItem Value="Yes">Yes</asp:ListItem>
				</asp:RadioButtonList></TD>
		</TR>
		<TR>
			<TD></TD>
			<TD>
				<asp:Panel ID="PanelHES" runat="server">
				<asp:DropDownList id="dropHESDay" runat="server">
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
					</asp:DropDownList>
<asp:DropDownList id="dropHESMonth" runat="server">
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
<asp:DropDownList id="dropHESYear" runat="server">
						<asp:ListItem Value="0">Year</asp:ListItem>
					</asp:DropDownList>
<asp:CompareValidator id="CompareValidator17" runat="server" ErrorMessage="HES Approval Day" ControlToValidate="dropHESDay"
						Operator="NotEqual" ValueToCompare="0">*</asp:CompareValidator>
<asp:CompareValidator id="CompareValidator18" runat="server" ErrorMessage="HES Approval Month" ControlToValidate="dropHESMonth"
						Operator="NotEqual" ValueToCompare="0">*</asp:CompareValidator>
<asp:CompareValidator id="CompareValidator19" runat="server" ErrorMessage="HES Approval Year" ControlToValidate="dropHESYear"
						Operator="NotEqual" ValueToCompare="0">*</asp:CompareValidator>
                </asp:Panel></TD>
		</TR>
		<TR>
			<TD>HES / CEL Pediatric Patients Accepted:</TD>
			<TD>
				<asp:RadioButtonList id="rblstHESPedApproved" runat="server" 
            RepeatDirection="Horizontal">
					<asp:ListItem Value="No">No</asp:ListItem>
					<asp:ListItem Value="Yes">Yes</asp:ListItem>
				</asp:RadioButtonList></TD>
		</TR>
		<TR>
			<TD width="210">Additional HES / CEL Information:</TD>
			<TD>
				<asp:TextBox id="txtHESInfo" runat="server" Width="250px" 
            Height="88px" TextMode="MultiLine"></asp:TextBox></TD>
		</TR>
		<TR>
			<TD></TD>
			<TD></TD>
		</TR>
		<TR>
			<TD height="22"></TD>
			<TD height="22"></TD>
		</TR>
		<TR>
			<TD></TD>
			<TD></TD>
		</TR>
		<TR>
			<TD width="210"></TD>
			<TD></TD>
		</TR>
		<TR>
			<TD width="210"></TD>
			<TD></TD>
		</TR>
		<TR>
			<TD width="210">
				<asp:Button id="ButtonSave" runat="server" Width="50px" Text="Save" onclick="ButtonSave_Click"></asp:Button>&nbsp;
				<asp:Button id="ButtonCancel" runat="server" Width="50px" Text="Cancel" onclick="ButtonCancel_Click"></asp:Button></TD>
			<TD></TD>
		</TR>
		<TR>
			<TD width="210"></TD>
			<TD></TD>
		</TR>
	</TABLE>
	</div>
</asp:Content>

