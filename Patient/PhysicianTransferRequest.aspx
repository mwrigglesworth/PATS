<%@ Page Title="" Language="C#" MasterPageFile="~/Patient/GIPAPPatient.master" AutoEventWireup="true" CodeFile="PhysicianTransferRequest.aspx.cs" Inherits="Patient_PhysicianTransferRequest" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderControlPanel" Runat="Server">
<div class="ControlPanelDivHeader">
                    Physician Transfer Request</div>
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
	<TABLE id="Table1" cellSpacing="1" cellPadding="1" width="600" align="center" border="0">
	<tr>
        <td style="width: 599px">
            <strong>Please specify the physician you would like to transfer this patient to:<asp:CompareValidator
                ID="CompareValidator1" runat="server" ControlToValidate="dropPhysician" ErrorMessage="Physician"
                Operator="NotEqual" ValueToCompare="0">*</asp:CompareValidator></strong></td>
    </tr>
    <tr>
        <td style="width: 599px">
            <asp:DropDownList ID="dropPhysician" runat="server">
            </asp:DropDownList></td>
    </tr>
    <tr>
        <td style="height: 24px; width: 599px;"><font color=gray size=1>
            This list only includes approved physicians from the same country.</font>
        </td>
    </tr>
    <tr>
        <td style="width: 599px">
            <strong>Are both physicians aware and agreeable to the transfer?<asp:RequiredFieldValidator
                ID="RequiredFieldValidator1" runat="server" ControlToValidate="rblstAgreeable"
                ErrorMessage="Are both physicians aware/agreeable?">*</asp:RequiredFieldValidator></strong></td>
    </tr>
    <tr>
        <td style="width: 599px">
            <asp:RadioButtonList ID="rblstAgreeable" runat="server">
                <asp:ListItem>No</asp:ListItem>
                <asp:ListItem>Yes</asp:ListItem>
            </asp:RadioButtonList></td>
    </tr>
    <tr>
        <td style="width: 599px">
            <asp:Button ID="ButtonRequest" runat="server" Text="Submit Request" OnClick="ButtonRequest_Click" />&nbsp;
            <asp:Button ID="ButtonCancel" runat="server" CausesValidation="False" Text="Cancel" OnClick="ButtonCancel_Click" /></td>
    </tr>
    <tr>
        <td style="width: 599px">
        </td>
    </tr>
</table>
</div>
		</div>
</asp:Content>

