<%@ Page Language="C#" MasterPageFile="~/Clinic/GIPAPClinic.master" AutoEventWireup="true" CodeFile="EditClinic.aspx.cs" Inherits="Clinic_EditClinic" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderControlPanel" Runat="Server">
    <h1>Edit Clinic Information</h1>
    <TABLE id="Table7" cellSpacing="1" cellPadding="1" width="585" align="left" border="0">
		<TR>
			<TD><asp:validationsummary id="ValidationSummary1" runat="server" HeaderText="You are missing the following fields:"
		ShowMessageBox="True" CssClass="AlertDiv" ForeColor=""></asp:validationsummary></TD>
		</TR>
		</TABLE>
    <div class="FormTable" style="clear:both;">
    <TABLE id="Table3" cellSpacing="1" cellPadding="1" width="600" align="center" border="0">
		<TR>
			<TD>Clinic Name:</TD>
			<TD>
				<asp:textbox id="txtClinicName" tabIndex="1" runat="server" Width="200px" MaxLength="200"></asp:textbox>
				<asp:requiredfieldvalidator id="RequiredFieldValidator1" runat="server" ControlToValidate="txtClinicName" ErrorMessage="You Must Enter a Clinic Name">*</asp:requiredfieldvalidator>
				</TD>
			<TD>Department:</TD>
			<TD>
				<asp:textbox id="txtClinicDepartment" tabIndex="1" runat="server" Width="200px"></asp:textbox></TD>
		</TR>
		<TR>
			<TD>Phone:</TD>
			<TD>
				<asp:textbox id="txtClinicPhone" tabIndex="8" runat="server" Width="200px" DESIGNTIMEDRAGDROP="574"></asp:textbox></TD>
			<TD>Address:</TD>
			<TD>
				<asp:textbox id="txtClinicAddress1" tabIndex="2" runat="server" Width="200px"></asp:textbox></TD>
		</TR>
		<TR>
			<TD>Fax:</TD>
			<TD>
				<asp:textbox id="txtClinicFax" tabIndex="9" runat="server" Width="200px"></asp:textbox></TD>
			<TD></TD>
			<TD>
				<asp:textbox id="txtClinicAddress2" tabIndex="3" runat="server" Width="200px"></asp:textbox></TD>
		</TR>
		<TR>
			<TD>Email:</TD>
			<TD>
				<asp:textbox id="txtClinicEmail" tabIndex="10" runat="server" Width="200px"></asp:textbox>
				</TD>
			<TD>City:</TD>
			<TD>
				<asp:textbox id="txtClinicCity" tabIndex="4" runat="server" Width="200px"></asp:textbox></TD>
		</TR>
		<TR>
			<TD>
				<P align="left">&nbsp;</P>
			</TD>
			<TD></TD>
			<TD>State:</TD>
			<TD>
				<asp:textbox id="txtClinicState" tabIndex="5" runat="server" Width="200px"></asp:textbox></TD>
		</TR>
		<TR>
			<TD>
				<P align="left">&nbsp;</P>
			</TD>
			<TD></TD>
			<TD>Postal Code:</TD>
			<TD>
				<asp:textbox id="txtPostalCode" tabIndex="6" runat="server" Width="200px"></asp:textbox></TD>
		</TR>
		<TR>
			<TD></TD>
			<TD>
				<P align="left">&nbsp;</P>
			</TD>
			<TD>Country:</TD>
			<TD>
				<asp:dropdownlist id="dropCountry" tabIndex="7" runat="server" Width="200px"></asp:dropdownlist>
				<asp:comparevalidator id="CompareValidator1" runat="server" ControlToValidate="dropCountry" ErrorMessage="You must Choose a Country"
					ValueToCompare="0" Operator="NotEqual">*</asp:comparevalidator>
				</TD>
		</TR>
		<TR>
			<TD>
				<P align="left">&nbsp;</P>
			</TD>
			<TD></TD>
			<TD></TD>
			<TD></TD>
		</TR>
		<TR>
				<TD colSpan="4"><STRONG><FONT color="steelblue">Administrator&nbsp;Information</FONT></STRONG></TD>
			</TR>
			<TR>
				<TD>First Name:</TD>
				<TD>
					<asp:textbox id="txtAdminFirstName" tabIndex="11" runat="server" Width="200px"></asp:textbox></TD>
				<TD>Phone:</TD>
				<TD>
					<asp:textbox id="txtAdminPhone" tabIndex="13" runat="server" Width="200px"></asp:textbox></TD>
			</TR>
			<TR>
				<TD>Last Name:</TD>
				<TD>
					<asp:textbox id="txtAdminLastName" tabIndex="12" runat="server" Width="200px"></asp:textbox></TD>
				<TD>Fax:</TD>
				<TD>
					<asp:textbox id="txtAdminFax" tabIndex="14" runat="server" Width="200px"></asp:textbox></TD>
			</TR>
			<TR>
				<TD></TD>
				<TD></TD>
				<TD>Email:</TD>
				<TD>
					<asp:textbox id="txtAdminEmail" tabIndex="15" runat="server" Width="200px"></asp:textbox></TD>
			</TR>
			<tr><td></td><td>
                            <asp:Button ID="ButtonSave" runat="server" Text="Submit" 
                                onclick="ButtonSave_Click" />
&nbsp;
                            <asp:Button ID="ButtonCancel" runat="server" CausesValidation="False" 
                                Text="Cancel" onclick="ButtonCancel_Click" />
                            </td><td></td><td></td></tr>
	</TABLE>
    </div>
</asp:Content>

