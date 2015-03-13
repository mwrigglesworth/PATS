<%@ Page Title="" Language="C#" MasterPageFile="~/PATS.master" AutoEventWireup="true" CodeFile="TotalsReport.aspx.cs" Inherits="Reports_TotalsReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="width:100%; clear:both; text-align: left;">
<h1>Patient Totals</h1>
<TABLE id="Table7" cellSpacing="1" cellPadding="1" width="600" border="0">
					
					<tr><td>
                        <asp:RadioButtonList ID="rblstProgram" runat="server">
                            <asp:ListItem Selected="True">GIPAP</asp:ListItem>
                            <asp:ListItem Value="NOA">NOA / NOA-GIPAP</asp:ListItem>
                            <asp:ListItem>NOA-Tasigna</asp:ListItem>
                        </asp:RadioButtonList>
                    </td></tr>
					<TR>
						<TD>
							<asp:Button id="ButtonActivity" runat="server" Width="112px" 
                                Text="Patient Totals" onclick="ButtonActivity_Click"></asp:Button>
                            </TD>
					</TR>
					<TR>
						<TD>
							<asp:Label id="LabelError" runat="server" ForeColor="Red"></asp:Label></TD>
					</TR>
				</TABLE>
				<asp:Panel ID="PanelResults" runat="server" Visible="false">
    <div class="FullPageListDivHeader">
        <asp:Label ID="LabelResultCount" runat="server" Text=""></asp:Label></div><div class="FullPageListDivHeaderRight"></div>
        <div class="FullPageListDiv"><asp:DataGrid ID="dgResults" runat="server" AlternatingItemStyle-BackColor="Gainsboro" BorderWidth="0px" Width="915px">
    </asp:DataGrid></div>
    </asp:Panel>
</div>
</asp:Content>

