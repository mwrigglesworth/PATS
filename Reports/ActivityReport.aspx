<%@ Page Title="" Language="C#" MasterPageFile="~/PATS.master" AutoEventWireup="true" CodeFile="ActivityReport.aspx.cs" Inherits="Reports_ActivityReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style1
        {
            height: 58px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="width:100%; clear:both; text-align: left;">
<h1>Program Activity</h1>
<TABLE id="Table7" cellSpacing="1" cellPadding="1" width="600" border="0">
					<TR>
						<TD width="300">Start Date:</TD><td rowspan="5">
							<asp:Label id="LabelError" runat="server" ForeColor="Red"></asp:Label></td>
					</TR>
					<TR>
						<TD>
							<asp:DropDownList id="dropStartDay" runat="server" >
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
							<asp:DropDownList id="dropStartMonth" runat="server" >
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
							<asp:DropDownList id="dropStartYear" runat="server" ></asp:DropDownList>&nbsp;
						</TD>
					</TR>
					<TR>
						<TD>End Date:</TD>
					</TR>
					<TR>
						<TD>
							<asp:DropDownList id="dropEndDay" runat="server" >
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
							<asp:DropDownList id="dropEndMonth" runat="server" >
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
							<asp:DropDownList id="dropEndYear" runat="server" ></asp:DropDownList>&nbsp;
						</TD>
					</TR>
                    <tr><td class="style1"><asp:DropDownList ID="dropCountry" runat="server" 
                            AutoPostBack="True" onselectedindexchanged="dropCountry_SelectedIndexChanged">
    </asp:DropDownList>
    </td></tr>
					<tr><td>
                        <asp:RadioButtonList ID="rblstProgram" runat="server">
                            <asp:ListItem Selected="True">GIPAP</asp:ListItem>
                            <asp:ListItem Value="NOA">NOA / NOA-GIPAP</asp:ListItem>
                            <asp:ListItem>NOA-Tasigna</asp:ListItem>
                        </asp:RadioButtonList>
                    </td></tr>
					<TR>
						<TD>
							<asp:Button id="ButtonActivity" runat="server" Width="112px" Text="Activity Report" onclick="ButtonActivity_Click"></asp:Button>
                            </TD>
					</TR>
					<TR>
						<TD></TD>
					</TR>
				</TABLE>
        <asp:Panel ID="PanelComplete" runat="server" Visible="false">
        <p>Complete Lists:</p>
	<p><asp:LinkButton ID="lbApprovals" runat="server" onclick="lbApprovals_Click">Approvals</asp:LinkButton>&nbsp;
            <asp:LinkButton ID="lbReapprovals" runat="server" onclick="lbReapprovals_Click">ReApprovals</asp:LinkButton>
            &nbsp;
            <asp:LinkButton ID="lbClosures" runat="server" onclick="lbClosures_Click">Closures</asp:LinkButton>
            &nbsp;
            <asp:LinkButton ID="lbDeny" runat="server" onclick="lbDeny_Click">Denials</asp:LinkButton>
            &nbsp;
            <asp:LinkButton ID="lbExtend" runat="server" onclick="lbExtend_Click">Extensions</asp:LinkButton>
            &nbsp;
            <asp:LinkButton ID="lbDoseChange" runat="server" onclick="lbDoseChange_Click">Dose Changes</asp:LinkButton>
            </p>
        </asp:Panel>
    <asp:Panel ID="PanelResults" runat="server" Visible="false">
    <div class="FullPageListDivHeader">
        <asp:Label ID="LabelResultCount" runat="server" Text=""></asp:Label></div><div class="FullPageListDivHeaderRight"></div>
        <div class="FullPageListDiv"><asp:DataGrid ID="dgResults" runat="server" AlternatingItemStyle-BackColor="Gainsboro" BorderWidth="0px" Width="915px">
    </asp:DataGrid></div>
    </asp:Panel>
</div>
</asp:Content>

