<%@ Page Language="C#" MasterPageFile="~/Patient/GIPAPPatient.master" AutoEventWireup="true" CodeFile="Deny.aspx.cs" Inherits="Patient_Deny" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderControlPanel" Runat="Server">
<div class="ControlPanelDivHeader">
                    Deny Patient Application</div>
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
	<TABLE id="Table10" cellSpacing="1" cellPadding="1" width="600" align="center" border="0">
		<TR>
			<TD height="16">Status Reason:
				<asp:DropDownList id="dropDenyStatusReason" runat="server" Width="250px">
					<asp:ListItem Value="0">Select One</asp:ListItem>
					<asp:ListItem Value="Other Diagnosis">Other Diagnosis</asp:ListItem>
					<asp:ListItem Value="C-Kit Negative">C-Kit Negative</asp:ListItem>
					<asp:ListItem Value="GIST Tumor is Neither Metastatic or Unresectable">GIST Tumor is Neither Metastatic or Unresectable</asp:ListItem>
					<asp:ListItem Value="Philadelphia Chromosome Negative">Philadelphia Chromosome Negative</asp:ListItem>
					<asp:ListItem Value="Glivec not prescribed">Glivec not prescribed</asp:ListItem>
					<asp:ListItem Value="Patient Had Successful Surgery/BMT">Patient Had Successful Surgery/BMT</asp:ListItem>
					<asp:ListItem Value="Patient has passed away">Patient has passed away</asp:ListItem>
					<asp:ListItem Value="Patient has insurance">Patient has insurance</asp:ListItem>
					<asp:ListItem Value="Insurance covers Glivec">Insurance covers Glivec</asp:ListItem>
					<asp:ListItem Value="Does not meet financial criteria for GIPAP">Does not meet financial criteria for GIPAP</asp:ListItem>
					<asp:ListItem Value="Patient Has Access to Reimbursement">Patient Has Access to Reimbursement</asp:ListItem>
					<asp:ListItem Value="Does Not Meet Country Financial Requirements">Does Not Meet Country Financial Requirements</asp:ListItem>
					<asp:ListItem Value="Glivec not approved in country">Glivec not approved in country</asp:ListItem>
					<asp:ListItem Value="GIPAP not approved in country">GIPAP not approved in country</asp:ListItem>
					<asp:ListItem Value="Interferon treatment required">Interferon treatment required</asp:ListItem>
					<asp:ListItem Value="Duplicate Application">Duplicate Application</asp:ListItem>
					<asp:ListItem Value="Underage">Underage</asp:ListItem>
					<asp:ListItem Value="Referred to EAP">Referred to EAP</asp:ListItem>
					<asp:ListItem Value="Does Not Meet Country Citizenship Requirements">Does Not Meet Country Citizenship Requirements</asp:ListItem>
					<asp:ListItem Value="Patient’s Physician not Approved for GIPAP">Patient’s Physician not Approved for GIPAP</asp:ListItem>
					<asp:ListItem Value="Patient or Doctor Retracts Application">Patient or Doctor Retracts Application</asp:ListItem>
					<asp:ListItem Value="Unspecified / Other">Unspecified / Other</asp:ListItem>
					<asp:ListItem Value="Lost contact with patient">Lost contact with patient</asp:ListItem>
					<asp:ListItem Value="Required documents not submitted">Required documents not submitted</asp:ListItem>
					<asp:ListItem Value="Referred to Clinical Trial">Referred to Clinical Trial</asp:ListItem>
				</asp:DropDownList>
				<asp:CompareValidator id="CompareValidator6" runat="server" ErrorMessage="Status Reason" Operator="NotEqual"
					ValueToCompare="0" ControlToValidate="dropDenyStatusReason">*</asp:CompareValidator></TD>
		</TR>
		<TR>
			<TD>Notes:<BR>
				<FONT face="Verdana" color="gray" size="1">Any note entered here is confidential 
					and will be viewed by personnel from The Max Foundation only.</FONT></TD>
		</TR>
		<TR>
			<TD>
				<asp:TextBox id="txtDenyNotes" runat="server" Width="585" TextMode="MultiLine" Height="80px"></asp:TextBox></TD>
		</TR>
		<TR>
			<TD>
				<asp:Button id="ButtonDeny" runat="server" Width="75px" Text="Submit" onclick="ButtonDeny_Click"></asp:Button>&nbsp;
				<asp:Button id="ButtonDenyNo" runat="server" Width="75px" Text="Cancel" CausesValidation="False" onclick="ButtonDenyNo_Click"></asp:Button></TD>
		</TR>
	</TABLE>
	</div>
</div>
</asp:Content>

