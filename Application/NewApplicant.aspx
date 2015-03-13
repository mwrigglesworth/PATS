<%@ Page Language="C#" MasterPageFile="~/PATS.master" AutoEventWireup="true" CodeFile="NewApplicant.aspx.cs" Inherits="Application_NewApplicant" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<div style="width:100%; clear:both; text-align: left;">
    <asp:Panel ID="PanelApplicant" runat="server">
    <div id="LeftCol">
    <h1><asp:label id="LabelInfo" runat="server"></asp:label></h1>
<div class='LeftColDivHeader'>New Applicant Summary</div>
    <div class='LeftColDiv'>
    <asp:label id="LabelAddress" runat="server"></asp:label>
    </div>
    </div>
    <div id="MainCol">
    <div class="ControlPanelDivHeader">
                    Assign PO / MS / Physician</div>
                <div class="ControlPanelHeaderRight">
                </div>
<div class="FormDiv">
<TABLE id="Table11" cellSpacing="1" cellPadding="1" width="585" align="left" border="0">
		<TR>
			<TD><asp:validationsummary id="ValidationSummary1" runat="server" HeaderText="You are missing the following fields:"
		ShowMessageBox="True" CssClass="AlertDiv" ForeColor=""></asp:validationsummary></TD>
		</TR>
	</TABLE>
	<div style="clear:both;">
	<table id="Table1" cellspacing="1" cellpadding="1" width="580" border="0">
		<tr><td></td><td align="right">
					<asp:linkbutton id="lbExistingPatients" runat="server" Visible="False" ForeColor="Red" CausesValidation="False" onclick="lbExistingPatients_Click">Patients Exist with a Similar Name</asp:linkbutton></td></tr>
		<tr><td><STRONG>Country:</strong><br />
							<asp:comparevalidator id="Comparevalidator4" runat="server" ErrorMessage="Country" ControlToValidate="dropCountry"
								Operator="NotEqual" ValueToCompare="Select a PO">*</asp:comparevalidator>
							<asp:DropDownList id="dropCountry" runat="server" Width="200px"></asp:DropDownList>
							<br /><br /><b>Physician:</b><br />
							<asp:comparevalidator id="CompareValidator3" runat="server" ErrorMessage="Physician" ControlToValidate="dropPhysician"
								Operator="NotEqual" ValueToCompare="Select a Physician">*</asp:comparevalidator>
							<asp:dropdownlist id="dropPhysician" runat="server" Width="200px"></asp:dropdownlist><br />
							<asp:label id="LabelCountryPhysicians" runat="server"></asp:label></td>
		<td align="right">
                        <asp:Label ID="LabelDistributorSuggestion" runat="server" Text=""></asp:Label><br />
                        <%if (showDistributor)
                          {%>
                            <span id="distributorSpan"><b>Distributor: </b><asp:DropDownList ID="dropDistributor" runat="server"></asp:DropDownList><br /></span>
                            <%} %>
                            <asp:Label ID="LabelMSSuggestion" runat="server" Text=""></asp:Label><br />
                            <b>Max Station: </b><asp:DropDownList ID="dropMaxStation" runat="server">
                            </asp:DropDownList><br /><font class="Subtext"><b>Please Note: </b>Choosing a MS is optional</font>
							<br /><br /><asp:Label id="LabelSuggestion" runat="server"></asp:Label><BR><strong>PO:</strong>
							<asp:comparevalidator id="CompareValidator1" runat="server" ErrorMessage="Program Officer" ControlToValidate="dropPO"
								Operator="NotEqual" ValueToCompare="Select a PO">*</asp:comparevalidator>
							<asp:dropdownlist id="dropPO" runat="server" Width="200px"></asp:dropdownlist></td></tr>
		<tr><td>Delete Reason:&nbsp;
					<asp:DropDownList id="dropDeleteReason" runat="server">
						<asp:ListItem Value="0">Select One</asp:ListItem>
						<asp:ListItem Value="Duplicate Application">Duplicate Application</asp:ListItem>
						<asp:ListItem Value="Other">Other</asp:ListItem>
					</asp:DropDownList></td><td></td></tr>
			<tr><td align="center">
					
				    <asp:Button ID="ButtonDelete" runat="server" BackColor="Red" ForeColor="White" 
                        Text="Delete Applicant" onclick="ButtonDelete_Click" 
                        CausesValidation="False" /><br />
                        <asp:Label ID="LabelReasonDelete" runat="server" ForeColor="Red" 
                        Visible="False">You must provide a reason to delete</asp:Label>
					
				</td>
				<td align="center">
					
				    <asp:Button ID="ButtonAdd" runat="server" BackColor="Lime" ForeColor="Green" 
                        Text="Add Applicant" onclick="ButtonAdd_Click" />
					
				</td></tr>
		</table>
		</div>
		</div>
        <div class="LeftColSpacer"></div>
        <div class="MainColDivHeader">Medical Information</div><div class="MainColDivHeaderRight"></div>
        <div class="MainColDiv">
        <div class="MainColDivider">
        <asp:label id="LabelDiagInfo" runat="server"></asp:label>
        </div>
         <div class="MainColDivider">
         <asp:label id="LabelFinancialInfo" runat="server"></asp:label>
        </div>
        </div>
    </div>
    </asp:Panel>
	<asp:panel id="PanelExistingPatients" runat="server" Visible="False">
	<div style="clear:both;">
		<asp:Label id="LabelHeader" runat="server"></asp:Label><br />
			Similar Names Exist:<br />
			<asp:DataGrid id="dgExisitngPatients" runat="server" Width="895px">
                <AlternatingItemStyle BackColor="Gainsboro" />
                <HeaderStyle BackColor="Silver" />
        </asp:DataGrid><br />
					<asp:LinkButton id="lbReturn" runat="server" ForeColor="SteelBlue" onclick="lbReturn_Click">Click Here to Return to Applicant</asp:LinkButton>
					</div>
	</asp:panel>
</div>
</asp:Content>

