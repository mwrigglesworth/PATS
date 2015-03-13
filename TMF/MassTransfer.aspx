<%@ Page Title="" Language="C#" MasterPageFile="~/PATS.master" AutoEventWireup="true" CodeFile="MassTransfer.aspx.cs" Inherits="TMF_MassTransfer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<div style="width:100%; clear:both; text-align: left;">
<asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional" >
                        <ContentTemplate>

<div id="MassTransferPO" runat="server">
<div class="MessageDivHeader" >
        Transfer PO</div>
        <div class="MessageHeaderRight"></div>
        <div class="MessageDiv">
        
<TABLE id="TABLE1" cellSpacing="2"  cellPadding="1" width="910" border="0" bgColor="">
	<tr>
		<td width="300px">Country Name:</td>
		<td><asp:dropdownlist id="dropCountry" runat="server" Width="154px"></asp:dropdownlist></td>
	</tr>
    <tr>
		<td width="300px">Physician:&nbsp;</td>
		<td><asp:dropdownlist id="dropPhysician" runat="server"></asp:dropdownlist></td>
	</tr>
    <tr>
		<td width="300px">Program Officer:&nbsp;</td>
		<td><asp:dropdownlist id="dropSocial" runat="server"></asp:dropdownlist>
        &nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnFind" runat="server" BackColor="Maroon" 
                CssClass="roundbutton" ForeColor="White"  Text="Find" 
                Width="60px" Height="30px" onclick="btnFind_Click" />
                &nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label ID="lblPatientCount" runat="server" Text="Label" Visible="false">
                </asp:Label>
        </td>

	</tr>

    <tr>
        <td align="right">
            <asp:Button ID="ButtonPost" runat="server" BackColor="Maroon" 
                CssClass="roundbutton" ForeColor="White"  Text="Transfer" 
                Width="80px" />
            &nbsp;</td>
        <td align="left">
            <asp:Button ID="ButtonCancel" runat="server" BackColor="Maroon" 
                CausesValidation="False" CssClass="roundbutton" ForeColor="White" 
                Text="Cancel" Width="80px" />
        </td>
    </tr>
</TABLE>

</div>

    </div>
    
    
        <br /><br />

<div class="MessageDivHeader">Active File(s)</div>
<div class="MessageHeaderRight"></div>
<div class="MessageDiv">

</div>



        
      </ContentTemplate>
</asp:UpdatePanel>
    </div>
</asp:Content>

