<%@ Page Language="C#" MasterPageFile="~/Patient/GIPAPPatient.master" AutoEventWireup="true" CodeFile="ViewFCRequest.aspx.cs" Inherits="Patient_ViewFCRequest" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderControlPanel" Runat="Server">
<div class="ControlPanelDivHeader">
                    View NOA Patient Request</div>
                <div class="ControlPanelHeaderRight">
                </div>
<div class="FormDiv">
	<table width="600">
	<tr>
            <td>
                <asp:Label ID="LabelRequest" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Panel ID="PanelResolve" runat="server" Visible="False">
                    <asp:Button ID="ButtonReply" runat="server" 
                    Text="Resolve and Reply to this request" Width="218px" 
                        onclick="ButtonReply_Click" />
                    &nbsp;
                    <asp:Button ID="ButtonResolve" runat="server" 
                    Text="Mark as Resolved" onclick="ButtonResolve_Click" />
                    &nbsp;
                    <asp:Button ID="ButtonCancel" runat="server" 
                    BackColor="Salmon" BorderColor="Red" Text="Cancel and return to homepage" 
                    Width="197px" onclick="ButtonCancel_Click" />
                </asp:Panel>
            </td>
        </tr>
    </table>
    </div>
</asp:Content>

