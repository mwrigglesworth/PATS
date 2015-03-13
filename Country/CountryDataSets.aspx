<%@ Page Title="" Language="C#" MasterPageFile="~/PATS.master" AutoEventWireup="true" CodeFile="CountryDataSets.aspx.cs" Inherits="Country_CountryDataSets" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div style="width:100%; clear:both; text-align: left;"><h1><font color="SteelBlue">
    <asp:Label ID="LabelCountry" runat="server" Text=""></asp:Label></font></h1>
    <div class="FullPageListDivHeader">
        <asp:Label ID="LabelResultCount" runat="server" Text=""></asp:Label></div><div class="FullPageListDivHeaderRight"></div>
        <div class="FullPageListDiv"><asp:DataGrid ID="dgResults" runat="server" AlternatingItemStyle-BackColor="Gainsboro" BorderWidth="0px" Width="915px">
<AlternatingItemStyle BackColor="Gainsboro"></AlternatingItemStyle>
            <HeaderStyle Font-Bold="True" />
    </asp:DataGrid></div>
    </div>
</asp:Content>

