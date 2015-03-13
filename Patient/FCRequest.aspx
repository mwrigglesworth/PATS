<%@ Page Language="C#" MasterPageFile="~/Patient/GIPAPPatient.master" AutoEventWireup="true" CodeFile="FCRequest.aspx.cs" Inherits="Patient_FCRequest" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderControlPanel" Runat="Server">
<div class="ControlPanelDivHeader">
                    NOA Patient Request</div>
                <div class="ControlPanelHeaderRight">
                </div>
<div class="FormDiv">
<TABLE id="Table11" cellSpacing="1" cellPadding="1" width="585" align="left" border="0">
		<TR>
			<TD><asp:validationsummary id="ValidationSummary1" runat="server" HeaderText="You are missing the following fields:"
		ShowMessageBox="True" CssClass="AlertDiv" ForeColor=""></asp:validationsummary></TD>
		</TR>
	</TABLE>
	<div style="clear:both;">
	<table width="600">
	<tr>
                <td>
                    <asp:RadioButtonList ID="rblstToType" runat="server">
                        <asp:ListItem>Central Hub</asp:ListItem>
                        <asp:ListItem>Call Center</asp:ListItem>
                        <asp:ListItem>Branch</asp:ListItem>
                        <asp:ListItem>TMF</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="LabelReply" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>Subject:&nbsp;
                    <asp:TextBox ID="txtSubject" runat="server" MaxLength="100" Width="300px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                        ControlToValidate="txtSubject" ErrorMessage="Subject">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>Message:<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                        ControlToValidate="txtMessage" ErrorMessage="Message">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="txtMessage" runat="server" Height="116px" TextMode="MultiLine" 
                        Width="580px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="ButtonSub" runat="server" Text="Submit Request" 
                        onclick="ButtonSub_Click" />
                    &nbsp;
                    </td>
            </tr>
            <tr>
                <td></td>
            </tr>
        </table>
        </div>
        </div>
</asp:Content>

