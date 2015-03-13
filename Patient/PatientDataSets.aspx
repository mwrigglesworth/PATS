<%@ Page Language="C#" MasterPageFile="~/Patient/GIPAPPatient.master" AutoEventWireup="true" CodeFile="PatientDataSets.aspx.cs" Inherits="Patient_PatientDataSets" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderControlPanel" Runat="Server">
    <asp:Label ID="LabelPatientLinks" runat="server" Text=""></asp:Label>
    <div class="MainColDivHeader">
    <asp:Label ID="LabelReportTitle" runat="server" Text=""></asp:Label></div><div class="MainColDivHeaderRight"></div>
        <div class="MainColDiv">
        <asp:Label ID="LabelStringReport" runat="server" Text=""></asp:Label><br />
        <asp:DataGrid ID="dgResults" runat="server" AlternatingItemStyle-BackColor="Gainsboro" BorderWidth="0px" Width="590px" GridLines="None">
<AlternatingItemStyle BackColor="Gainsboro"></AlternatingItemStyle>
            <HeaderStyle Font-Bold="True" />
    </asp:DataGrid>
            <asp:HyperLink ID="hlAE" runat="server" CssClass="lbAR" Visible="false">Log Adverse Event</asp:HyperLink>
        </div>
</asp:Content>

