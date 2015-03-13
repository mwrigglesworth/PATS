<%@ Page Language="C#" MasterPageFile="~/Patient/GIPAPPatient.master" AutoEventWireup="true" CodeFile="ContactInfo.aspx.cs" Inherits="Patient_ContactInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderControlPanel" Runat="Server">
<input type="hidden" id="patientid" value="<%=patientid%>" />
<input type="hidden" id="contactid" value="<%=contactid%>" />
    <div class="MainColDivHeader">Patient Contact Info</div><div class="MainColDivHeaderRight"></div>
        <div class="MainColDiv">
        <asp:Label ID="LabelContactInfo" runat="server" Text=""></asp:Label><br /><br />
            <asp:Button ID="editContact" runat="server" Text="Edit Contact Information" />
        </div>
</asp:Content>

