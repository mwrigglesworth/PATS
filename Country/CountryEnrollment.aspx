<%@ Page Title="" Language="C#" MasterPageFile="~/Country/GIPAPCountry.master" AutoEventWireup="true" CodeFile="CountryEnrollment.aspx.cs" Inherits="Country_CountryEnrollment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderControlPanel" Runat="Server">
    <h1>Country Enrollment by Diagnosis</h1>
<TABLE id="Table7" cellSpacing="1" cellPadding="1" width="600" border="0">
					<TR>
						<TD width="300">Month:</TD>
					</TR>
					<TR>
						<TD>
							<asp:DropDownList id="dropMonth" runat="server" >
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
						</TD>
					</TR>
					<TR>
						<TD>Year:</TD>
					</TR>
					<TR>
						<TD>
							<asp:DropDownList id="dropYear" runat="server" ></asp:DropDownList>&nbsp;
						</TD>
					</TR>
					<tr><td>
                        <asp:RadioButtonList ID="rblstProgram" runat="server">
                            <asp:ListItem Selected="True">GIPAP</asp:ListItem>
                            <asp:ListItem Value="NOA">NOA / NOA-GIPAP</asp:ListItem>
                        </asp:RadioButtonList>
                    </td></tr>
					<TR>
						<TD>
							<asp:Button id="ButtonActivity" runat="server" Text="Enrollment Report" 
                                onclick="ButtonActivity_Click"></asp:Button>
                            &nbsp;</TD>
					</TR>
					<TR>
						<TD><asp:Label id="LabelError" runat="server" ForeColor="Red"></asp:Label>
							</TD>
					</TR>
				</TABLE><br />
	
    <asp:Panel ID="PanelResults" runat="server" Visible="false">
    <div class="MainColDivHeader">
    <asp:Label ID="LabelHeader" runat="server" Text=""></asp:Label></div><div class="MainColDivHeaderRight"></div>
        <div class="MainColDiv">
        <asp:DataGrid ID="dgResults" runat="server" AlternatingItemStyle-BackColor="Gainsboro" BorderWidth="0px" Width="590px">
<AlternatingItemStyle BackColor="Gainsboro"></AlternatingItemStyle>
            <HeaderStyle Font-Bold="True" />
    </asp:DataGrid>
        </div>
    </asp:Panel>
</asp:Content>

