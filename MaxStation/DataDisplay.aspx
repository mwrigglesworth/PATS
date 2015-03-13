<%@ Page Language="C#" MasterPageFile="~/PATS.master" AutoEventWireup="true" CodeFile="DataDisplay.aspx.cs" Inherits="MaxStation_DataDisplay" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div style="width:100%; clear:both; text-align: left;"><h1>
    <asp:Label ID="LabelTitle" runat="server" Text=""></asp:Label></h1>
    <div class="FullPageListDivHeader">
        <asp:Label ID="LabelResultCount" runat="server" Text=""></asp:Label></div><div class="FullPageListDivHeaderRight"></div>
        <div class="FullPageListDiv"><asp:DataGrid ID="dgResults" runat="server" 
                AlternatingItemStyle-BackColor="Gainsboro" BorderWidth="0px" Width="915px" 
                GridLines="None"><HeaderStyle Font-Bold="true" />
<AlternatingItemStyle BackColor="Gainsboro"></AlternatingItemStyle>
    </asp:DataGrid></div>
    </div>
</asp:Content>

