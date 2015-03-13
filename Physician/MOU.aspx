<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MOU.aspx.cs" Inherits="Physician_MOU" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="style2.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <table width="750">
    <tr><td><FONT face="Arial Rounded MT Bold" color="steelblue" size="6">GIPAP / NOA</FONT><hr color="black"></td></tr>
    <tr><td align="center"><font color="gray" size="5">Memorandum of Understanding</font></td></tr>
        <tr>
            <td>
                <font color="red">IMPORTANT - PLEASE READ CAREFULLY: This MEMORANDUM OF UNDERSTANDING
                    is a legally binding contract. By clicking on the "I Accept" button below, you represent
                    that you are authorized to accept these terms and conditions on behalf of the named
                    qualified institution. If you do not agree to be bound by this Agreement in its
                    entirety, please click the "Decline" button below.</font>
            </td>
        </tr>
        <tr><td><iframe src="MOU.htm" width="750" height="200" frameborder="1" ></iframe></td></tr>
        <tr><td><TABLE id="Table6" cellSpacing="1" cellPadding="1" width="100%" border="0">
																				<TR>
																					<TD width="349" colSpan="2"><STRONG>The Max Foundation</STRONG></TD>
																					<TD colSpan="2"><STRONG>The Qualified Institution or Physician acting on behalf of the 
																							Qualified Institution</STRONG></TD>
																				</TR>
																				<TR>
																					<TD width="75">By:</TD>
																					<TD width="269">Pat Garcia-Gonzalez</TD>
																					<TD>Name:</TD>
																					<TD>
																						<asp:Label ID="LabelName" runat="server"></asp:Label>
                                                                                    </TD>
																				</TR>
																				<TR>
																					<TD width="75">Position:</TD>
																					<TD width="269">Executive Director</TD>
																					<TD></TD>
																					<TD></TD>
																				</TR>
																				<TR>
																					<TD width="75">Date:</TD>
																					<TD width="269">
																						<asp:label id="LabelPatDate" runat="server"></asp:label></TD>
																					<TD>Date:</TD>
																					<TD>
                                                                                        <asp:Label ID="LabelDate" runat="server"></asp:Label>
                                                                                    </TD>
																				</TR>
																			</TABLE></td></tr>
        <tr><td>
            <asp:Button ID="ButtonAccept" runat="server" Text="I Accept MOU" 
                onclick="ButtonAccept_Click" />
             &nbsp;
            <asp:Button ID="ButtonDecline" runat="server" onclick="ButtonDecline_Click" 
                Text="Decline" />
             </td></tr>
        <tr><td></td></tr>
        <tr><td></td></tr>
    </table>
    </form>
</body>
</html>
