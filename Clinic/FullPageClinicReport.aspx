<%@ Page Language="C#" MasterPageFile="~/PATS.master" AutoEventWireup="true" CodeFile="FullPageClinicReport.aspx.cs" Inherits="Clinic_FullPageClinicReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="width:100%; clear:both; text-align: left;"><h1><font color="SteelBlue">
    <asp:Label ID="LabelClinicName" runat="server" Text=""></asp:Label></font></h1>
    <asp:Label id="LabelResultCount" runat="server" Font-Bold="True"></asp:Label><br />
<asp:DataGrid id="dgResults" Width="900px" runat="server">
    <AlternatingItemStyle BackColor="Gainsboro" />
    <HeaderStyle BackColor="Silver" Font-Bold="True" />
    </asp:DataGrid>
    </div>
</asp:Content>

