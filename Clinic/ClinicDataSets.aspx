<%@ Page Title="" Language="C#" MasterPageFile="~/Clinic/GIPAPClinic.master" AutoEventWireup="true" CodeFile="ClinicDataSets.aspx.cs" Inherits="Clinic_ClinicDataSets" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderControlPanel" Runat="Server">
<div class="MainColDivHeader">
    <asp:Label ID="LabelHeader" runat="server" Text=""></asp:Label></div><div class="MainColDivHeaderRight"></div>
        <div class="MainColDiv">
        <asp:DataGrid ID="dgResults" runat="server" AlternatingItemStyle-BackColor="Gainsboro" BorderWidth="0px" Width="590px">
<AlternatingItemStyle BackColor="Gainsboro"></AlternatingItemStyle>
            <HeaderStyle Font-Bold="True" />
    </asp:DataGrid>
        </div>
</asp:Content>

