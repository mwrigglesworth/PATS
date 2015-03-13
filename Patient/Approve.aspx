<%@ Page Language="C#" MasterPageFile="~/Patient/GIPAPPatient.master" AutoEventWireup="true" CodeFile="Approve.aspx.cs" Inherits="Patient_Approve" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderControlPanel" Runat="Server">
<div class="ControlPanelDivHeader">
                    Approve Patient Application</div>
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
	<TABLE id="Table16" cellSpacing="1" cellPadding="1" width="600" align="center" border="0">
		<TR>
			<TD>Status Reason:
				<asp:DropDownList id="dropApproveStatusReason" runat="server" Width="250px">
					<asp:ListItem Value="0">Select One</asp:ListItem>
					<asp:ListItem Value="Approved by exception">Approved by exception</asp:ListItem>
					<asp:ListItem Value="Approved with Partial Coverage">Approved with Partial Coverage</asp:ListItem>
					<asp:ListItem Value="Fulfills all criteria">Fulfills all criteria</asp:ListItem>
				</asp:DropDownList>
				<asp:CompareValidator id="CompareValidator9" runat="server" ErrorMessage="Status Reason" Operator="NotEqual"
					ValueToCompare="0" ControlToValidate="dropApproveStatusReason">*</asp:CompareValidator></TD>
		</TR>
	</TABLE>
	<TABLE id="Table14" cellSpacing="1" cellPadding="1" width="600" align="center" border="0">
		<TR>
			<TD vAlign="top" width="340">
				<TABLE id="Table15" cellSpacing="1" cellPadding="1" width="100%" align="center" bgColor="#ffffff"
					border="0">
					<TR>
						<TD width="350">Approval Period Start Date:
							<asp:Label id="LabelApproveStartDate" runat="server"></asp:Label>&nbsp;
						</TD>
					</TR>
					<TR>
						<TD align="left" width="350" height="22">Approval Period End Date:
							<asp:Label id="LabelApproveEndDate" runat="server"></asp:Label>&nbsp;
						</TD>
					</TR>
					<TR>
						<TD width="350"></TD>
					</TR>
					<TR>
						<TD></TD>
					</TR>
					<TR>
						<TD>
							<asp:Panel id="PanelApproveDose" runat="server">Recommended&nbsp;Dosage*: 
<asp:DropDownList id="DropApproveDose" runat="server" Width="100px" 
                                    onselectedindexchanged="DropApproveDose_SelectedIndexChanged">
									<asp:ListItem Value="0">Select One</asp:ListItem>
								</asp:DropDownList>
<asp:CompareValidator id="CompareValidator13" runat="server" ErrorMessage="Dosage" Operator="NotEqual"
									ValueToCompare="0" ControlToValidate="DropApproveDose">*</asp:CompareValidator><BR>
<asp:Label id="LabelApproveDosageMessage" ForeColor="Gray" runat="server" Font-Italic="True">*Dosages of 200mg and 260mg are pediatric only</asp:Label></asp:Panel></TD>
					</TR>
					<TR>
						<TD>
                            <asp:Panel ID="PanelTablet" runat="server" Visible="false"><br /><br />
                            Tablet Strength: 
                        <asp:DropDownList ID="dropTablet" runat="server">
                        </asp:DropDownList>
                            </asp:Panel>
                        </TD>
					</TR>
                    <tr><td style="height:25px;"></td></tr>
				</TABLE>
				<TABLE id="Table18" cellSpacing="1" cellPadding="1" width="100%" border="0">
					<TR>
						<TD>Notes:<BR>
							<FONT face="Verdana" color="gray" size="1">Any note entered here is confidential 
								and will be viewed by personnel from The Max Foundation only.</FONT></TD>
					</TR>
					<TR>
						<TD>
							<asp:TextBox id="txtApproveNotes" runat="server" Width="100%" TextMode="MultiLine" Height="56px"></asp:TextBox></TD>
					</TR>
					<TR>
						<TD>
							<asp:Button id="ButtonApprove" runat="server" Width="75px" Text="Submit" onclick="ButtonApprove_Click"></asp:Button>&nbsp;
							<asp:Button id="ButtonCancelApprove" runat="server" Width="75px" Text="Cancel" CausesValidation="False" onclick="ButtonCancelApprove_Click"></asp:Button></TD>
					</TR>
				</TABLE>
			</TD>
			<TD vAlign="top"></TD>
		</TR>
	</TABLE>
	</div>
</asp:Content>

