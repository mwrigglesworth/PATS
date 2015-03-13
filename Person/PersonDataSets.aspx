<%@ Page Language="C#" MasterPageFile="~/Person/GIPAPPerson.master" AutoEventWireup="true" CodeFile="PersonDataSets.aspx.cs" Inherits="Person_PersonDataSets" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderControlPanel" Runat="Server">
    <asp:Label ID="LabelLinks" runat="server" Text=""></asp:Label>
    <div class="MainColDivHeader">
    <asp:Label ID="LabelReportTitle" runat="server" Text=""></asp:Label></div><div class="MainColDivHeaderRight"></div>
        <div class="MainColDiv">
        <asp:DataGrid ID="dgResults" runat="server" AlternatingItemStyle-BackColor="Gainsboro" BorderWidth="0px" Width="590px" GridLines="None">
<AlternatingItemStyle BackColor="Gainsboro"></AlternatingItemStyle>
            <HeaderStyle Font-Bold="True" />
    </asp:DataGrid>
        </div>
</asp:Content>

