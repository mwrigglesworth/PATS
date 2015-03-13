<%@ Page Language="C#" MasterPageFile="~/Patient/GIPAPPatient.master" AutoEventWireup="true" CodeFile="UpdateStatusReason.aspx.cs" Inherits="Patient_UpdateStatusReason" %>

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
<TABLE id="Table23" cellSpacing="1" cellPadding="1" width="600" align="center" border="0">
			<TR>
				<TD>Change Status Reason To:&nbsp;
					<asp:DropDownList id="dropUpdateStatusReason" runat="server" Width="200px">
						<asp:ListItem Value="0">Select One</asp:ListItem>
					</asp:DropDownList>
					<asp:CompareValidator id="CompareValidator11" runat="server" ErrorMessage="Status Reason" Operator="NotEqual"
						ValueToCompare="0" ControlToValidate="dropUpdateStatusReason">*</asp:CompareValidator></TD>
			</TR>
			<TR>
				<TD>
					<asp:Button id="ButtonUpdateStatusReason" runat="server" Width="65px" Text="Submit" onclick="ButtonUpdateStatusReason_Click"></asp:Button>&nbsp;
					<asp:Button id="ButtonCancelUpdateStatusReason" runat="server" Width="65px" Text="Cancel" CausesValidation="False" onclick="ButtonCancelUpdateStatusReason_Click"></asp:Button></TD>
			</TR>
		</TABLE>
		</div>
</div>
</asp:Content>

