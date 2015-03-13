<%@ Page Title="" Language="C#" MasterPageFile="~/PATS.master" AutoEventWireup="true" CodeFile="ContactMAX.aspx.cs" Inherits="Physician_ContactMAX" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div style="width:100%; clear:both; text-align: left;">
<h1>Contact MAX</h1>
<asp:Panel id="PanelContact" runat="server">
	<TABLE id="Table1" cellSpacing="1" cellPadding="1" width="590" bgColor="silver" border="0">
		<TR>
			<TD>To:</TD>
			<TD><A href="mailto:gipap@themaxfoundation.org">gipap@themaxfoundation.org</A></TD>
		</TR>
		<TR>
			<TD>Subject:</TD>
			<TD>
				<asp:textbox id="txtSubject" runat="server" Width="500px"></asp:textbox>
				<asp:RequiredFieldValidator id="RequiredFieldValidator1" runat="server" ControlToValidate="txtSubject" ErrorMessage="Subject">*</asp:RequiredFieldValidator></TD>
		</TR>
		<TR>
			<TD vAlign="top">Message:</TD>
			<TD>
				<asp:textbox id="txtMessage" runat="server" Width="500px" Height="250px" TextMode="MultiLine"></asp:textbox>
				<asp:RequiredFieldValidator id="RequiredFieldValidator2" runat="server" ControlToValidate="txtMessage" ErrorMessage="Message">*</asp:RequiredFieldValidator></TD>
		</TR>
		<TR>
			<TD></TD>
			<TD>
				<asp:button id="ButtonSend" runat="server" Width="50px" Text="Send" onclick="ButtonSend_Click"></asp:button>&nbsp;
				<asp:button id="ButtonCancel" runat="server" Width="50px" Text="Cancel" CausesValidation="False" onclick="ButtonCancel_Click"></asp:button></TD>
		</TR>
		<TR>
			<TD></TD>
			<TD></TD>
		</TR>
	</TABLE>
	<asp:ValidationSummary id="ValidationSummary1" runat="server" Width="448px" HeaderText="The Following Fields are Missing:"
		ShowMessageBox="True"></asp:ValidationSummary>
</asp:Panel>
<asp:Panel id="PanelConfirmation" runat="server" Visible="False">
	<TABLE id="Table3" cellSpacing="1" cellPadding="1" width="590" border="0">
		<TR>
			<TD>
				<P><FONT size="4">Thankyou for contacting us!</FONT></P>
				<P>Your message has been sent to our GIPAP staff and we&nbsp;will contact you as 
					soon as possible regarding the matter.</P>
			</TD>
		</TR>
	</TABLE>
</asp:Panel>

</div>
</asp:Content>

