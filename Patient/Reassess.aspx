<%@ Page Language="C#" MasterPageFile="~/Patient/GIPAPPatient.master" AutoEventWireup="true" CodeFile="Reassess.aspx.cs" Inherits="Patient_Reassess" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderControlPanel" Runat="Server">
    <div class="ControlPanelDivHeader">
                    Reassess Denied Patient Application</div>
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
	<TABLE id="Table13" cellSpacing="1" cellPadding="1" width="600" align="center" border="0">
		<TR>
			<TD>Please enter the reason for re-assessment for this patient:</TD>
		</TR>
		<TR>
			<TD>
				<asp:TextBox id="txtCaseNote" runat="server" Width="400px" TextMode="MultiLine" Height="112px"></asp:TextBox>
				<asp:RequiredFieldValidator id="RequiredFieldValidator3" runat="server" ErrorMessage="Reason For ReAssessment"
					ControlToValidate="txtCaseNote">*</asp:RequiredFieldValidator></TD>
		</TR>
		<TR>
			<TD>
				<asp:Button id="ButtonReAssess" runat="server" Width="75px" Text="Submit" onclick="ButtonReAssess_Click"></asp:Button>&nbsp;
				<asp:Button id="ButtonCancelReAssess" runat="server" Width="75px" Text="Cancel" CausesValidation="False" onclick="ButtonCancelReAssess_Click"></asp:Button>&nbsp;
				<asp:Button ID="ButtonRemoveRequest" runat="server" Text="Remove Request" 
                    BackColor="Red" onclick="ButtonRemoveRequest_Click" Visible="False" CausesValidation="false" /></TD>
		</TR>
	</TABLE>
	</div>
</asp:Content>

