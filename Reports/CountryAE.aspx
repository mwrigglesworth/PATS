<%@ Page Title="" Language="C#" MasterPageFile="~/PATS.master" AutoEventWireup="true" CodeFile="CountryAE.aspx.cs" Inherits="Reports_CountryAE" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div style="width:100%; clear:both; text-align: left;">
<h1>Country Adverse Events</h1>
<TABLE id="Table7" cellSpacing="1" cellPadding="1" width="600" border="0">
<tr><td>
    <asp:DropDownList ID="dropCountry" runat="server">
    </asp:DropDownList>
    <asp:CompareValidator ID="CompareValidator1" runat="server" 
        ControlToValidate="dropCountry" ErrorMessage="Select a country" 
        Operator="NotEqual" ValueToCompare="Select a Country"></asp:CompareValidator>
</td></tr>
					<TR>
						<TD>
							<asp:Button id="ButtonSubmit" runat="server" Text="Adverse Events" 
                                onclick="ButtonSubmit_Click"></asp:Button>
                            &nbsp;</TD>
					</TR>
					<TR>
						<TD>
							</TD>
					</TR>
				</TABLE><br /><asp:Label id="LabelError" runat="server" ForeColor="Red"></asp:Label>
				<asp:panel id="PanelGIPAPTotals" runat="server" Visible="false">
                <div class="FullPageListDivHeader">
        <asp:Label ID="LabelResultCount" runat="server" Text=""></asp:Label></div><div class="FullPageListDivHeaderRight"></div>
        <div class="FullPageListDiv"><asp:DataGrid ID="dgResults" runat="server" AlternatingItemStyle-BackColor="Gainsboro" BorderWidth="0px" Width="915px">
    </asp:DataGrid></div>
                </asp:panel>
                </div>
</asp:Content>

