<%@ Page Language="C#" MasterPageFile="~/Patient/GIPAPPatient.master" AutoEventWireup="true" CodeFile="Reactivate.aspx.cs" Inherits="Patient_Reactivate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderControlPanel" Runat="Server">
<div class="ControlPanelDivHeader">
                    Reactivate Patient Case</div>
                <div class="ControlPanelHeaderRight">
                </div>
<div class="FormDiv">
<TABLE id="Table11" cellSpacing="1" cellPadding="1" width="585" align="left" border="0">
		<TR>
			<TD><asp:validationsummary id="ValidationSummary1" runat="server" HeaderText="You are missing the following fields:"
		ShowMessageBox="True" CssClass="AlertDiv" ForeColor=""></asp:validationsummary></TD>
		</TR>
		<TR>
			<TD>
                <asp:Panel ID="PanelObsolete" runat="server" Visible="False">
                <div class="AlertDiv">
                The current status of this 
patient renders the following request obsolete.&nbsp; An action may have been 
executed for this patient since this request was submitted.  Click below to remove the request.
                </div>
                </asp:Panel>
            </TD>
		</TR>
	</TABLE>
	<div style="clear:both;">
	<TABLE id="Table12" cellSpacing="1" cellPadding="1" width="600" align="center" border="0">
		<TR>
			<TD>Reason For Reactivation:&nbsp;
				<asp:DropDownList id="dropReactivation" runat="server" Width="250px">
					<asp:ListItem Value="0">Select One</asp:ListItem>
					<asp:ListItem Value="Clinical Reason">Clinical Reason</asp:ListItem>
					<asp:ListItem Value="Patient Meets GIPAP Criteria">Patient Meets GIPAP Criteria</asp:ListItem>
					<asp:ListItem Value="Re-established contact with patient">Re-established contact with patient</asp:ListItem>
					<asp:ListItem Value="Re-evaluation information provided">Re-evaluation information provided</asp:ListItem>
					<asp:ListItem Value="Not Duplicate Patient">Not Duplicate Patient</asp:ListItem>
					<asp:ListItem Value="Other">Other</asp:ListItem>
				</asp:DropDownList>
				<asp:CompareValidator id="CompareValidator8" runat="server" ErrorMessage="Reason for Reactivation" Operator="NotEqual"
					ValueToCompare="0" ControlToValidate="dropReactivation">*</asp:CompareValidator>
				<asp:RequiredFieldValidator id="RequiredFieldValidator10" runat="server" ErrorMessage="Reason For Reactivation"
					ControlToValidate="txtReactivationReason">*</asp:RequiredFieldValidator></TD>
			<TD></TD>
		</TR>
		<TR>
			<TD>
				<asp:TextBox id="txtReactivationReason" runat="server" Width="585" TextMode="MultiLine" Height="80px"></asp:TextBox></TD>
			<TD></TD>
		</TR>
	</TABLE>
	<TABLE id="Table17" cellSpacing="1" cellPadding="1" width="600" align="center" border="0">
		<TR>
			<TD vAlign="top" width="340">
				<TABLE id="Table19" cellSpacing="1" cellPadding="1" width="100%" align="center" bgColor="#ffffff"
					border="0">
					<TR>
						<TD width="350">
							<asp:Label id="LabelStartReactivate" runat="server"></asp:Label>&nbsp;
							<asp:LinkButton id="lbEditReactivateStart" runat="server" CausesValidation="False" onclick="lbEditReactivateStart_Click">Edit Date</asp:LinkButton></TD>
					</TR>
					<TR>
						<TD align="left" width="350" height="22">
							<asp:Label id="LabelEndReactivate" runat="server"></asp:Label>&nbsp;
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
							<asp:Panel id="PanelReactivateDose" runat="server">Recommended&nbsp;Dosage:* 
<asp:DropDownList id="dropReactivateDose" runat="server" Width="100px" 
                                    onselectedindexchanged="dropReactivateDose_SelectedIndexChanged">
									<asp:ListItem Value="0">Select One</asp:ListItem>
								</asp:DropDownList>
<asp:CompareValidator id="CompareValidator14" runat="server" ErrorMessage="Dosage" Operator="NotEqual"
									ValueToCompare="0" ControlToValidate="dropReactivateDose">*</asp:CompareValidator><BR>
<asp:Label id="LabelReactivateDosageMessage" ForeColor="Gray" runat="server" Font-Italic="True">*Dosages of 200mg and 260mg are pediatric only</asp:Label></asp:Panel>
<asp:Panel ID="PanelTablet" runat="server" Visible="false"><br /><br />
                            Tablet Strength: 
                        <asp:DropDownList ID="dropTablet" runat="server">
                        </asp:DropDownList>
                            </asp:Panel></TD>
					</TR>
                    <tr><td style="height:25px;"></td></tr>
				</TABLE>
				<TABLE id="Table20" cellSpacing="1" cellPadding="1" width="100%" border="0">
					<TR>
						<TD>Do you&nbsp;recommend that the patient&nbsp;restart treatment?&nbsp;
							<asp:RequiredFieldValidator id="RequiredFieldValidator4" runat="server" ErrorMessage="Do you recommend patient restart treatment"
								ControlToValidate="rblstContinue">*</asp:RequiredFieldValidator></TD>
					</TR>
					<TR>
						<TD>
							<asp:RadioButtonList id="rblstContinue" runat="server" RepeatDirection="Horizontal">
								<asp:ListItem Value="No">No</asp:ListItem>
								<asp:ListItem Value="Yes" Selected="True">Yes</asp:ListItem>
							</asp:RadioButtonList></TD>
					</TR>
					<TR>
						<TD>To your knowledge has the financial status of the patient remained the same?
							<asp:RequiredFieldValidator id="RequiredFieldValidator5" runat="server" ErrorMessage="Has the financial status changed?"
								ControlToValidate="rblstReactivateFinancial">*</asp:RequiredFieldValidator></TD>
					</TR>
					<TR>
						<TD>
							<asp:RadioButtonList id="rblstReactivateFinancial" runat="server" RepeatDirection="Horizontal">
								<asp:ListItem Value="No">No</asp:ListItem>
								<asp:ListItem Value="Yes" Selected="True">Yes</asp:ListItem>
							</asp:RadioButtonList></TD>
					</TR>
					<TR>
						<TD>
							<asp:Panel id="PanelReactivateCML" runat="server">
								<TABLE id="Table21" cellSpacing="1" cellPadding="1" width="100%" border="0">
									<TR>
										<TD width="145">Current CML Phase:</TD>
										<TD>
											<asp:dropdownlist id="dropCMLReactivate" runat="server" Width="138px">
												<asp:ListItem Value="0">Select CML Phase</asp:ListItem>
												<asp:ListItem Value="Accelerated">Accelerated</asp:ListItem>
												<asp:ListItem Value="Blast Crisis">Blast Crisis</asp:ListItem>
												<asp:ListItem Value="Chronic">Chronic</asp:ListItem>
												<asp:ListItem Value="Remission">Remission</asp:ListItem>
											</asp:dropdownlist>
											<asp:CompareValidator id="CompareValidator12" runat="server" ErrorMessage="You must select a CML Phase"
												Operator="NotEqual" ValueToCompare="0" ControlToValidate="dropCMLReactivate">*</asp:CompareValidator></TD>
									</TR>
								</TABLE>
							</asp:Panel></TD>
					</TR>
					<TR>
						<TD>Notes:</TD>
					</TR>
					<TR>
						<TD>
							<asp:TextBox id="txtReactivateNote" runat="server" Width="100%" TextMode="MultiLine" Height="56px"></asp:TextBox></TD>
					</TR>
					<TR>
						<TD>
							<asp:Button id="ButtonReactivate" runat="server" Width="75px" Text="Submit" onclick="ButtonReactivate_Click"></asp:Button>&nbsp;
							<asp:Button id="ButtonCancelReactivate" runat="server" Width="75px" Text="Cancel" CausesValidation="False" onclick="ButtonCancelReactivate_Click"></asp:Button>&nbsp;
							<asp:Button ID="ButtonRemoveRequest" runat="server" Text="Remove Request" 
                    BackColor="Red" onclick="ButtonRemoveRequest_Click" Visible="False" CausesValidation="false" /></TD>
					</TR>
				</TABLE>
			</TD>
			<TD vAlign="top">
				<asp:Calendar id="CalendarReactivateStart" runat="server" BorderColor="#404040" BackColor="White" onselectionchanged="CalendarReactivateStart_SelectionChanged"></asp:Calendar></TD>
		</TR>
	</TABLE>
	</div>
</asp:Content>

