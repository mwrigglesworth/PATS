<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CaseNotes.ascx.cs" Inherits="Patient_GIPAPAsync_CaseNotes" %>
<div class="MainColDivHeader">Case Notes</div><div class="MainColDivHeaderRight"></div>
        <div class="MainColDiv">
        <table width="590" style="background-color:#E8F9F8"><tr><td width="450">
            <asp:TextBox ID="txtNote" runat="server" Width=450px TextMode="MultiLine" 
                Text="Add Note" ForeColor="Gray" Font-Names="Verdana">Add Note</asp:TextBox></td><td>
                <asp:Button ID="ButtonAdd" runat="server" Text="Add Note" Font-Size="XX-Small" 
                    onclick="ButtonAdd_Click" Width="95px" /><br />
                    <asp:LinkButton ID="lbAlert" runat="server" onclick="lbAlert_Click">Note with Alert</asp:LinkButton></td></tr></table>
            <asp:Panel ID="PanelAlert" runat="server" Visible="false">
            <table width="590" style="background-color:#E8F9F8"><tr><td width="250">
            Effective Date:<br />
                <asp:DropDownList id="dropDay" runat="server">
						<asp:ListItem Value="1">1</asp:ListItem>
						<asp:ListItem Value="2">2</asp:ListItem>
						<asp:ListItem Value="3">3</asp:ListItem>
						<asp:ListItem Value="4">4</asp:ListItem>
						<asp:ListItem Value="5">5</asp:ListItem>
						<asp:ListItem Value="6">6</asp:ListItem>
						<asp:ListItem Value="7">7</asp:ListItem>
						<asp:ListItem Value="8">8</asp:ListItem>
						<asp:ListItem Value="9">9</asp:ListItem>
						<asp:ListItem Value="10">10</asp:ListItem>
						<asp:ListItem Value="11">11</asp:ListItem>
						<asp:ListItem Value="12">12</asp:ListItem>
						<asp:ListItem Value="13">13</asp:ListItem>
						<asp:ListItem Value="14">14</asp:ListItem>
						<asp:ListItem Value="15">15</asp:ListItem>
						<asp:ListItem Value="16">16</asp:ListItem>
						<asp:ListItem Value="17">17</asp:ListItem>
						<asp:ListItem Value="18">18</asp:ListItem>
						<asp:ListItem Value="19">19</asp:ListItem>
						<asp:ListItem Value="20">20</asp:ListItem>
						<asp:ListItem Value="21">21</asp:ListItem>
						<asp:ListItem Value="22">22</asp:ListItem>
						<asp:ListItem Value="23">23</asp:ListItem>
						<asp:ListItem Value="24">24</asp:ListItem>
						<asp:ListItem Value="25">25</asp:ListItem>
						<asp:ListItem Value="26">26</asp:ListItem>
						<asp:ListItem Value="27">27</asp:ListItem>
						<asp:ListItem Value="28">28</asp:ListItem>
						<asp:ListItem Value="29">29</asp:ListItem>
						<asp:ListItem Value="30">30</asp:ListItem>
						<asp:ListItem Value="31">31</asp:ListItem>
						<asp:ListItem></asp:ListItem>
					</asp:DropDownList>
					<asp:DropDownList id="dropMonth" runat="server">
						<asp:ListItem Value="1">January</asp:ListItem>
						<asp:ListItem Value="2">February</asp:ListItem>
						<asp:ListItem Value="3">March</asp:ListItem>
						<asp:ListItem Value="4">April</asp:ListItem>
						<asp:ListItem Value="5">May</asp:ListItem>
						<asp:ListItem Value="6">June</asp:ListItem>
						<asp:ListItem Value="7">July</asp:ListItem>
						<asp:ListItem Value="8">August</asp:ListItem>
						<asp:ListItem Value="9">September</asp:ListItem>
						<asp:ListItem Value="10">October</asp:ListItem>
						<asp:ListItem Value="11">November</asp:ListItem>
						<asp:ListItem Value="12">December</asp:ListItem>
					</asp:DropDownList>
					<asp:DropDownList id="dropYear" runat="server" Width="62px"></asp:DropDownList>
            </td><td><asp:Label ID="LabelErr" runat="server" Text="" ForeColor="Red"></asp:Label><br />
                <asp:Button ID="ButtonAlert" runat="server" Text="Add Note With Alert" 
                        onclick="ButtonAlert_Click"  />                    
                </td></tr></table>
            </asp:Panel>
        <asp:DataGrid ID="dgCnotes" runat="server" AlternatingItemStyle-BackColor="Gainsboro" BorderWidth="0px" Width="590px" ShowHeader="false" GridLines="None">
    </asp:DataGrid>
        </div>