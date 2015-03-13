<%@ Page Title="" Language="C#" MasterPageFile="~/PATS.master" AutoEventWireup="true" CodeFile="AEReportLog.aspx.cs" Inherits="Reports_AEReportLog" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style1
        {
            height: 41px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="width:100%; clear:both; text-align: left;">
<h1>Adverse Event Report Log</h1>
<TABLE id="Table7" cellSpacing="1" cellPadding="1" width="600" border="0">
					<TR>
						<TD>Start Date:</TD>
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
						<TD>End End Date:</TD>
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
                    <tr><td class="style1"><asp:DropDownList ID="dropCountry" runat="server">
    </asp:DropDownList>
    </td></tr>
					<tr><td>
                        <asp:RadioButtonList ID="rblstProgram" runat="server">
                            <asp:ListItem Selected="True" Value="GIPAP">GIPAP / NOA</asp:ListItem>
                            <asp:ListItem Value="TIPAP">TIPAP</asp:ListItem>
                            <asp:ListItem>MYPAP</asp:ListItem>
                            <asp:ListItem>NPS</asp:ListItem>
                        </asp:RadioButtonList>
                        </td></tr>
					<TR>
						<TD>
							<asp:Button ID="ButtonSubmit" runat="server" Text="Adverse Event Log" 
                                onclick="ButtonSubmit_Click" />
                            </TR>
					<TR>
						<TD>
							<asp:Label id="LabelError" runat="server" ForeColor="Red"></asp:Label></TD>
					</TR>
				</TABLE>
                <br /><font class="Subtext">Please note the earliest date the report can be run for is January 1, 2012.  If your start date is earlier than this date, it will be run from January 1, 2012.</font><br /><br />
                <asp:panel id="PanelGIPAPTotals" runat="server" Visible="false">
                
        <asp:Label ID="LabelResultCount" runat="server" Text=""></asp:Label><br /><asp:GridView id="dgGipapTotals" runat="server" AllowSorting="true" OnSorting="dgGipapToals_Sorting" AutoGenerateColumns="false" >
				<HeaderStyle BackColor="Silver" />
                                                <AlternatingRowStyle BackColor="Gainsboro" />
                                                <Columns>
                                                <asp:BoundField ControlStyle-Width="100" DataField="country" HeaderText="Country" HeaderStyle-HorizontalAlign="Left" SortExpression="country" />
                                                <asp:BoundField ControlStyle-Width="50" DataField="PIN" HeaderText="PIN" HeaderStyle-HorizontalAlign="Left"  />
                                                <asp:BoundField ControlStyle-Width="50" DataField="Physician" HeaderText="Physician" HeaderStyle-HorizontalAlign="Left" SortExpression="Physician" />
                                                <asp:BoundField ControlStyle-Width="110" DataField="Date Learned" HeaderText="Date AE Reported to Max" HeaderStyle-HorizontalAlign="Left" HtmlEncode="false"  />
                                                <asp:BoundField ControlStyle-Width="50" DataField="Date" HeaderText="Date" HeaderStyle-HorizontalAlign="Left" SortExpression="Date"  />
                                                <asp:BoundField ControlStyle-Width="50" DataField="Learned From" HeaderText="Learned From" HeaderStyle-HorizontalAlign="Left" />
                                                <asp:BoundField ControlStyle-Width="200" DataField="Event" HeaderText="Event" HeaderStyle-HorizontalAlign="Left" HtmlEncode="false" />
                                                <asp:BoundField ControlStyle-Width="100" DataField="Email" HeaderText="Email<br><font color=gray>Click to view</font>" HeaderStyle-HorizontalAlign="Left" HtmlEncode="false" />
                                                <asp:BoundField ControlStyle-Width="50" DataField="Email Sent" HeaderText="Email Sent" HeaderStyle-HorizontalAlign="Left" />
                                                <asp:BoundField ControlStyle-Width="50" DataField="TREATMENT" HeaderText="Treatment" HeaderStyle-HorizontalAlign="Left" />
                                                <asp:BoundField ControlStyle-Width="50" DataField="Program" HeaderText="Program" HeaderStyle-HorizontalAlign="Left" />
                                                <asp:BoundField ControlStyle-Width="50" DataField="ProgramID" HeaderText="Program ID" HeaderStyle-HorizontalAlign="Left" />
                                                </Columns>
                                                </asp:GridView>
                </asp:panel>
</div>
</asp:Content>

