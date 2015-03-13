<%@ Page Title="" Language="C#" MasterPageFile="~/Patient/GIPAPPatient.master" AutoEventWireup="true" CodeFile="ApproveForTasigna.aspx.cs" Inherits="Patient_ApproveForTasigna" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderControlPanel" Runat="Server">
<div class="ControlPanelDivHeader">
                    Approve for Tasigna</div>
                <div class="ControlPanelHeaderRight">
                </div>
<div class="FormDiv" style="padding:1px;">

        <asp:Label ID="labeltasignaInfo" runat="server"></asp:Label>

<table width="600">
    
    <tr><td>Approval Period Start Date:</td><td>
        <asp:Label ID="LabelTasignaStartDate" runat="server"></asp:Label>
        </td></tr>
    <tr><td>Approval Period End Date:</td><td>
        <asp:Label ID="LabelTasignaEndDate" runat="server"></asp:Label>
        </td></tr>
		<TR>
			<TD>NOA Tasigna dose:<asp:CompareValidator id="CompareValidator3" runat="server" 
            ErrorMessage="Requested TIPAP Dose" ControlToValidate="dropRequestedTasignaDose"
					ValueToCompare="0" Operator="NotEqual">*</asp:CompareValidator></TD>
			<TD>
				<asp:DropDownList id="dropRequestedTasignaDose" runat="server">
					<asp:ListItem Value="400mg BID">400mg BID</asp:ListItem>
					<asp:ListItem Value="400mg QD">400mg QD</asp:ListItem>
					<asp:ListItem>300mg BID</asp:ListItem>
				</asp:DropDownList></TD>
		</TR>
    <tr><td>Notes:</td><td>
        <asp:Label ID="LabelNotes" runat="server"></asp:Label>
        </td></tr>
    <tr><td colspan="2" style="height: 84px">
        <asp:Button ID="ButtonChangeTreatment" runat="server" Text="Approve for Tasigna" 
            onclick="ButtonChangeTreatment_Click" />
        &nbsp; <asp:Button ID="ButtonRemoveRequest" runat="server" Text="Remove Request" 
                    BackColor="Red" onclick="ButtonRemoveRequest_Click" CausesValidation="false" />
        </td></tr>
    <tr><td></td><td></td></tr>
    <tr><td></td><td></td></tr></table>
</div>
</asp:Content>

