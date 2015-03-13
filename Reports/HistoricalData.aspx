<%@ Page Title="" Language="C#" MasterPageFile="~/PATS.master" AutoEventWireup="true" CodeFile="HistoricalData.aspx.cs" Inherits="Reports_HistoricalData" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="width:100%; clear:both; text-align: left;">
<h1>Historical Data</h1><asp:label id="LabelDate" runat="server"></asp:label>
<asp:panel id="PanelHistoricalData" runat="server">
	<TABLE id="Table3" cellSpacing="1" cellPadding="1" width="600" align="center" border="0">
		<TR>
			<TD>
				<P><STRONG>Please select the date you would like to view information for.</STRONG></P>
			</TD>
		</TR>
	</TABLE>
	<TABLE id="Table2" cellSpacing="1" cellPadding="1" width="600" align="center" border="0">
		<TR>
			<TD width="249">
				<asp:Calendar id="CalendarHD" runat="server" onselectionchanged="CalendarHD_SelectionChanged"></asp:Calendar></TD>
			<TD vAlign="top">
				<P>&nbsp;</P>
				<P>
					<asp:Label id="LabelHD" runat="server" Visible="false">The selected date is greater than today's date, so the report was run for today.</asp:Label></P>
			</TD>
		</TR>
		<TR>
			<TD width="249"></TD>
			<TD></TD>
		</TR>
	</TABLE>
</asp:panel>
        <asp:panel id="PanelGIPAPTotals" runat="server" Visible="false">
                <div class="FullPageListDivHeader">
        <asp:Label ID="LabelResultCount" runat="server" Text=""></asp:Label></div><div class="FullPageListDivHeaderRight"></div>
        <div class="FullPageListDiv"><asp:DataGrid ID="dgResults" runat="server" AlternatingItemStyle-BackColor="Gainsboro" BorderWidth="0px" Width="915px">
    </asp:DataGrid></div>
                </asp:panel>
</div>
</asp:Content>

