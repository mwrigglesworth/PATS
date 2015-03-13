<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PhysNotes.ascx.cs" Inherits="Physician_GIPAPAsync_PhysNotes" %>
<div class="MainColDivHeader">Physician Notes</div><div class="MainColDivHeaderRight"></div>
        <div class="MainColDiv">
        <table width="590" style="background-color:#E8F9F8"><tr><td width="450">
            <asp:TextBox ID="txtNote" runat="server" Width=450px TextMode="MultiLine" 
                Text="Add Note" ForeColor="Gray" Font-Names="Verdana">Add Note</asp:TextBox></td><td>
                <asp:Button ID="ButtonAdd" runat="server" Text="Add Note" Font-Size="XX-Small" 
                    onclick="ButtonAdd_Click" Width="95px" /></td></tr></table>
        <asp:DataGrid ID="dgCnotes" runat="server" AlternatingItemStyle-BackColor="Gainsboro" BorderWidth="0px" Width="590px" ShowHeader="false" GridLines="None">
    </asp:DataGrid>
        </div>