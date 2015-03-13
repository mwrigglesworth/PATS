<%@ Page Language="C#" MasterPageFile="~/PATS.master" AutoEventWireup="true" CodeFile="AddEditDistributor.aspx.cs" Inherits="Distributor_AddEditDistributor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="width:100%; clear:both; text-align: left;">
<h1><%=Action%> Distributor</h1>
<TABLE id="Table7" cellSpacing="1" cellPadding="1" width="900" align="left" border="0">
		<TR>
			<TD><asp:validationsummary id="ValidationSummary1" runat="server" HeaderText="You are missing the following fields:"
		ShowMessageBox="True" CssClass="AlertDiv" ForeColor=""></asp:validationsummary></TD>
		</TR>
		</TABLE>
    <div class="FormTable" style="clear:both;">
    <TABLE id="Table3" cellSpacing="1" cellPadding="1" width="900"  border="0">
		<TR>
			<TD>Office Name:<asp:RequiredFieldValidator ID="RequiredFieldValidator1" 
                    runat="server" ControlToValidate="txtOfficeName" ErrorMessage="Office Name">*</asp:RequiredFieldValidator>
            </TD>
			<TD>
				<asp:TextBox ID="txtOfficeName" runat="server" MaxLength="200" Width="200px"></asp:TextBox>
				
            </TD>
			<TD>Address:</TD>
			<TD>
				<asp:TextBox ID="txtAddress1" runat="server" MaxLength="100" tabIndex="2" 
                    Width="200px"></asp:TextBox>
            </TD>
		</TR>
		<TR><TD>
				</TD>
			<TD valign="top"></TD>
			<TD>&#160;</TD>
			<TD>
				<asp:TextBox ID="txtAddress2" runat="server" MaxLength="100" tabIndex="3" 
                    Width="200px"></asp:TextBox></TD>
		</TR>
		<TR>
			<TD>Phone:</TD>
			<TD>
				<asp:textbox id="txtPhone" tabIndex="8" runat="server" Width="200px" 
                    DESIGNTIMEDRAGDROP="574" MaxLength="50"></asp:textbox></TD>
			<TD>City:</TD>
			<TD>
				<asp:textbox id="txtCity" tabIndex="4" runat="server" Width="200px" 
                    MaxLength="50"></asp:textbox></TD>
		</TR>
		<TR>
			<TD>
				<P align="left">Fax:</P>
			</TD>
			<TD>
				<asp:textbox id="txtFax" tabIndex="9" runat="server" Width="200px" 
                    MaxLength="50"></asp:textbox>
				</TD>
			<TD>State:</TD>
			<TD>
				<asp:textbox id="txtState" tabIndex="5" runat="server" Width="200px" 
                    MaxLength="30"></asp:textbox></TD>
		</TR>
		<TR>
			<TD>
				<P align="left">Email:</P>
			</TD>
			<TD>
				<asp:textbox id="txtEmail" tabIndex="10" runat="server" Width="200px" MaxLength="200"></asp:textbox>
				</TD>
			<TD>Postal Code:</TD>
			<TD>
				<asp:textbox id="txtPostalCode" tabIndex="6" runat="server" Width="200px" 
                    MaxLength="20"></asp:textbox></TD>
		</TR>
		<TR>
			<TD rowspan="2">
            </TD>
			<TD rowspan="2">
			</TD>
			<TD>Country:<asp:comparevalidator id="CompareValidator1" runat="server" ControlToValidate="dropCountry" ErrorMessage="You must Choose a Country"
					ValueToCompare="0" Operator="NotEqual">*</asp:comparevalidator>
				</TD>
			<TD>
				<asp:dropdownlist id="dropCountry" tabIndex="7" runat="server" Width="200px">
                </asp:dropdownlist>
				</TD>
		</TR>
		<TR>
			<TD></TD>
			<TD></TD>
		</TR><TR>
				<TD colSpan="4"><h3>Administrator&nbsp;Information</h3></TD>
			</TR>
			<TR>
				<TD>First Name:</TD>
				<TD>
					<asp:textbox id="txtAdminFirstName" tabIndex="11" runat="server" Width="200px" 
                        MaxLength="50"></asp:textbox></TD>
				<TD>Phone:</TD>
				<TD>
					<asp:textbox id="txtAdminPhone" tabIndex="13" runat="server" Width="200px" 
                        MaxLength="50"></asp:textbox></TD>
			</TR>
			<TR>
				<TD>Last Name:</TD>
				<TD>
					<asp:textbox id="txtAdminLastName" tabIndex="12" runat="server" Width="200px" 
                        MaxLength="50"></asp:textbox></TD>
				<TD>Fax:</TD>
				<TD>
					<asp:textbox id="txtAdminFax" tabIndex="14" runat="server" Width="200px" 
                        MaxLength="50"></asp:textbox></TD>
			</TR>
			<TR>
				<TD></TD>
				<TD></TD>
				<TD>Email:</TD>
				<TD>
					<asp:textbox id="txtAdminEmail" tabIndex="15" runat="server" Width="200px" 
                        MaxLength="100"></asp:textbox></TD>
			</TR>
			<tr><td></td><td></td><td>Mobile:</td><td>
					<asp:textbox id="txtAdminMobile" tabIndex="15" runat="server" Width="200px" 
                        MaxLength="50"></asp:textbox></td></tr>
                        <tr><td></td><td>
                            <asp:Button ID="ButtonSave" runat="server" Text="Save Distributor Info" 
                                onclick="ButtonSave_Click" />
                            </td><td></td><td></td></tr>
		</TABLE>
    </div>
</asp:Content>


