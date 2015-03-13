<%@ Page Title="" Language="C#" MasterPageFile="~/PATS.master" AutoEventWireup="true" CodeFile="ApplicationInfo.aspx.cs" Inherits="MaxStation_ApplicationInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="width:100%; clear:both; text-align: left;"><h1>Web Applications</h1>
Please select a submit date to view the web application list for:<br />
    <asp:Calendar ID="CalendarApps" runat="server" 
        onselectionchanged="CalendarApps_SelectionChanged"></asp:Calendar><br /><br />
    <asp:Panel ID="PanelResults" runat="server" Visible="false">
    <div class="FullPageListDivHeader">
        <asp:Label ID="LabelResultCount" runat="server" Text=""></asp:Label></div><div class="FullPageListDivHeaderRight"></div>
        <div class="FullPageListDiv"><asp:DataGrid ID="dgResults" runat="server" AlternatingItemStyle-BackColor="Gainsboro" BorderWidth="0px" Width="915px">
    </asp:DataGrid></div>
    </asp:Panel>
    </div>
</asp:Content>

