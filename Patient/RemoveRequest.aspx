<%@ Page Language="C#" MasterPageFile="~/Patient/GIPAPPatient.master" AutoEventWireup="true" CodeFile="RemoveRequest.aspx.cs" Inherits="Patient_RemoveRequest" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderControlPanel" Runat="Server">
<div class="ControlPanelDivHeader">
                    Remove Request</div>
                <div class="ControlPanelHeaderRight">
                </div>
<div class="FormDiv">
<p>Are you sure you want to remove this request from your queue?</p>
<asp:Button id="ButtonYes" Text="Yes" runat="server" Width="80px" onclick="ButtonYes_Click"></asp:Button>&nbsp;
				<asp:Button id="ButtonNo" Text="No" runat="server" Width="80px" onclick="ButtonNo_Click"></asp:Button>
				</div>
</asp:Content>

