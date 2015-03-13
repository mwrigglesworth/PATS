<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PatSummary.ascx.cs" Inherits="Patient_GIPAPAsync_PatSummary" %>
<asp:Panel ID="PanelSummary" runat="server">
<input type="hidden" id="senderpin" value="<%=pin%>" />
<input type="hidden" id="senderid" value="<%=choice%>" />
    <asp:Label ID="LabelSummary" runat="server" Text=""></asp:Label><br /><br />
            <asp:Label id="LabelEAA" runat="server"></asp:Label>&nbsp;
					<asp:LinkButton id="lbEAA" runat="server" CssClass="lbAR" onclick="lbEAA_Click"></asp:LinkButton><br /><br />
					<asp:Label id="LabelEAC" runat="server"></asp:Label>&nbsp;
					<asp:LinkButton id="lbEAC" runat="server" CssClass="lbAR" onclick="lbEAC_Click"></asp:LinkButton>
</asp:Panel>
<asp:Panel ID="PanelFull" runat="server" Visible="false">
</asp:Panel>
<div class="LeftColSpacer"></div>
<asp:LinkButton ID="lbShowFull" runat="server" onclick="lbShowFull_Click">View Full Patient Info</asp:LinkButton>