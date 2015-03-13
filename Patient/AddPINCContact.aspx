<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="AddPINCContact.aspx.cs" Inherits="Patient_AddPINCContact" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form id="frmPINCContact" runat="server">
    

     
    <div><table style="BORDER-RIGHT: 1px solid; BORDER-TOP: 1px solid; BORDER-LEFT: 1px solid; BORDER-BOTTOM: 1px solid"
		bordercolor="royalblue" cellspacing="1" cellpadding="1" width="400" bgcolor="gainsboro"
		border="0">
    <tr align="center">
				<td>
					<asp:CheckBoxList id="cbContact" runat="server">
						<asp:ListItem Value="Phone">Phone</asp:ListItem>
						<asp:ListItem Value="In person">In person</asp:ListItem>
						<asp:ListItem Value="Mail">Mail</asp:ListItem>
						<asp:ListItem Value="Fax">Fax</asp:ListItem>
                        <%--<asp:ListItem Value="Social Media">Social Media</asp:ListItem>--%>
					</asp:CheckBoxList></TD>
				<td vAlign="top">
					<asp:CheckBoxList id="cbContact2" runat="server">
						<asp:ListItem Value="SMS">SMS</asp:ListItem>
						<asp:ListItem Value="Messenger">Messenger</asp:ListItem>
						<asp:ListItem Value="Email">Email</asp:ListItem>
						<asp:ListItem Value="Attempted Call">Attempted Call</asp:ListItem>
					</asp:CheckBoxList></TD>
			</tr>
			<tr align="center">
				<td colSpan="2">
					<asp:Button id="ButtonContact" runat="server" Text="Log Contact" Font-Size="Small" onclick="ButtonContact_Click"></asp:Button></td>
			</tr>
		</table>
    </div>
    
    </form>
    </body>
    </html>