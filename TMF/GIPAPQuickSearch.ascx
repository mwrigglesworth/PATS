<%@ Control Language="C#" AutoEventWireup="true" CodeFile="GIPAPQuickSearch.ascx.cs" Inherits="TMF_GIPAPQuickSearch" %>
<div id="headerDiv" style="width:596px; height:71px; clear:both; background-image:url('../Images/GIPAPHeaderBg.png');"><div style="width:100%; height:30px; text-align:right; padding-top:10px; padding-right:10px;">
            <asp:CheckBox ID="cbNoa" runat="server" Font-Size="X-Small" ForeColor="Green" 
                Text="NOA (Partial Donation) Only:" TextAlign="Left" Visible="false" /></div>
        <span style="width:32px; float:left;">&nbsp;</span><div class="SearchTxtdiv"><asp:TextBox ID="txtPIN" runat="server" CssClass="SearchTxt" ForeColor="Gray">PIN</asp:TextBox></div>
            <span style="width:20px; float:left;">&nbsp;</span><div class="SearchTxtdiv"><asp:TextBox ID="txtFirstName" runat="server" CssClass="SearchTxt" ForeColor="Gray">First</asp:TextBox></div>
            <span style="width:20px; float:left;">&nbsp;</span><div class="SearchTxtdiv"><asp:TextBox ID="txtLastName" runat="server" CssClass="SearchTxt" ForeColor="Gray">Last</asp:TextBox></div>
            <asp:Button ID="ButtonSearch" runat="server" Text="GO!" 
        Font-Size="XX-Small" onclick="ButtonSearch_Click" CausesValidation="false" /></div>