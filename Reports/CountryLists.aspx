<%@ Page Title="" Language="C#" MasterPageFile="~/PATS.master" AutoEventWireup="true" CodeFile="CountryLists.aspx.cs" Inherits="Reports_CountryLists" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="width:100%; clear:both; text-align: left;">
<h1>Lists By Country</h1>
<asp:DropDownList ID="dropCountry" runat="server">
    </asp:DropDownList>
    <asp:CompareValidator ID="CompareValidator1" runat="server" 
        ControlToValidate="dropCountry" ErrorMessage="Select a country" 
        Operator="NotEqual" ValueToCompare="Select a Country"></asp:CompareValidator>
        <br /><br />
    <asp:DropDownList ID="dropList" runat="server">
        <asp:ListItem>Select a list</asp:ListItem>
        <asp:ListItem>Patients</asp:ListItem>
        <asp:ListItem Value="MaxStations">Max Stations</asp:ListItem>
        <asp:ListItem>Physicians</asp:ListItem>
        <asp:ListItem>Clinics</asp:ListItem>
    </asp:DropDownList><asp:CompareValidator ID="CompareValidator2" runat="server" 
        ControlToValidate="dropList" ErrorMessage="Select a list" 
        Operator="NotEqual" ValueToCompare="Select a list"></asp:CompareValidator><br /><br />
    <asp:Button ID="ButtonSub" runat="server" Text="Submit" 
        onclick="ButtonSub_Click" /><br /><br />
        <asp:panel id="PanelGIPAPTotals" runat="server" Visible="false">
                <div class="FullPageListDivHeader">
        <asp:Label ID="LabelResultCount" runat="server" Text=""></asp:Label></div><div class="FullPageListDivHeaderRight"></div>
        <div class="FullPageListDiv"><asp:DataGrid ID="dgResults" runat="server" AlternatingItemStyle-BackColor="Gainsboro" BorderWidth="0px" Width="915px">
    </asp:DataGrid></div>
                </asp:panel>
</div>
</asp:Content>

