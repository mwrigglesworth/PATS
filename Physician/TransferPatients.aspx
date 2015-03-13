<%@ Page Language="C#" MasterPageFile="~/Physician/GIPAPPhysician.master" AutoEventWireup="true" CodeFile="TransferPatients.aspx.cs" Inherits="Physician_TransferPatients" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderControlPanel" Runat="Server">
    <h1>Transfer Patinets</h1>
<table width="600">
    <tr>
        <td bgcolor="#E8F9F8">
            <asp:Label ID="LabelFrom" runat="server"></asp:Label></td>
    </tr>
    <tr>
        <td>
            <strong>Please specify the physician you would like to transfer patients to:</strong></td>
    </tr>
    <tr>
        <td>
            <asp:DropDownList ID="dropPhysician" runat="server" AutoPostBack="True" OnSelectedIndexChanged="dropPhysician_SelectedIndexChanged">
            </asp:DropDownList><font color=gray size=1>
            This list only includes approved physicians from the same country.</font></td>
    </tr>
    <tr>
        <td>
            <asp:Panel ID="PanelTo" runat="server" Width="100%" Visible="false">
                <strong>You have chosen:</strong><br />
                    <br />
                    <asp:Label ID="LabelTo" runat="server"></asp:Label><br />
                <br />
                <strong>Transfer in PINC as well?:
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Transfer in PINC?" ControlToValidate="rblstPINC">*</asp:RequiredFieldValidator></strong><asp:RadioButtonList
                        ID="rblstPINC" runat="server">
                        <asp:ListItem>No</asp:ListItem>
                        <asp:ListItem>Yes</asp:ListItem>
                    </asp:RadioButtonList><br />
                <strong>Is this correct?</strong><br />
                    <font color="gray" size="1">By choosing "yes" you will be transferring all patients (Active, Denied, Closed,
                    Pending) from Dr. <asp:Label ID="LabelDRFrom" runat="server"></asp:Label>
                        to Dr. 
                        <asp:Label ID="LabelDRTo" runat="server"></asp:Label><br />
                        <asp:Button ID="ButtonTransfer" runat="server" Text="Yes" Font-Size="XX-Small" OnClick="ButtonTransfer_Click" Width="80%" />&nbsp;
                        <asp:Button ID="ButtonNo" runat="server" Text="No" Width="70px" Font-Size="XX-Small" OnClick="ButtonNo_Click" /></font></asp:Panel>
        </td>
    </tr>
    <tr>
        <td>
        </td>
    </tr>
    <tr>
        <td>
        </td>
    </tr>
    <tr>
        <td>
        </td>
    </tr>
</table>
</asp:Content>

