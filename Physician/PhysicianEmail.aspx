<%@ Page Language="C#" MasterPageFile="~/Physician/GIPAPPhysician.master" AutoEventWireup="true" CodeFile="PhysicianEmail.aspx.cs" Inherits="Physician_PhysicianEmail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderControlPanel" Runat="Server">
<TABLE id="Table1" cellSpacing="1" cellPadding="1" width="600" bgColor="silver" border="0">
	<TR>
		<TD colSpan="2"><asp:label id="LabelHeader" runat="server" ForeColor="SteelBlue" Font-Bold="True"></asp:label></TD>
	</TR>
	<TR>
		<TD>From:</TD>
		<TD><asp:label id="LabelFrom" runat="server"></asp:label></TD>
	</TR>
	<TR>
		<TD>To:</TD>
		<TD><asp:textbox id="txtTo" runat="server" Width="515px"></asp:textbox></TD>
	</TR>
	<TR>
		<TD>CC:</TD>
		<TD><asp:textbox id="txtCC" runat="server" Width="515px"></asp:textbox></TD>
	</TR>
	<TR>
		<TD>BCC:</TD>
		<TD><asp:textbox id="txtBCC" runat="server" Width="515px"></asp:textbox></TD>
	</TR>
	<TR>
		<TD>Subject:</TD>
		<TD><asp:textbox id="txtSubject" runat="server" Width="515px"></asp:textbox></TD>
	</TR>
	<TR>
		<TD vAlign="top">Message:</TD>
		<TD><asp:textbox id="txtMessage" runat="server" Width="515px" Height="250px" TextMode="MultiLine"></asp:textbox></TD>
	</TR>
	<TR>
		<TD></TD>
		<TD><asp:button id="ButtonSend" runat="server" Text="Send" 
                onclick="ButtonSend_Click"></asp:button>&nbsp;
			<asp:button id="ButtonCancel" runat="server" Width="50px" Text="Cancel" onclick="ButtonCancel_Click"></asp:button>&nbsp;
			<asp:button id="ButtonSendAll" runat="server" Text="Send All" 
                onclick="ButtonSendAll_Click"></asp:button></TD>
	</TR>
	<TR>
		<TD></TD>
		<TD><asp:label id="LabelError" runat="server" ForeColor="Red"></asp:label></TD>
	</TR>
	<TR>
		<TD></TD>
		<TD>
			<P align="right"><asp:label id="LabelPrintLink" runat="server"></asp:label></P>
		</TD>
	</TR>
</TABLE>
</asp:Content>

