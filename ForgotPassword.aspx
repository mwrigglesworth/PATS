<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ForgotPassword.aspx.cs" Inherits="ForgotPassword" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Forgot Password</title>
    <link href="css/import_all.css" rel="stylesheet" type="text/css" />
    <meta http-equiv="Content-Type" content="text/html; charset=windows-1252" />
    <meta name="keywords" content="blood cancer, rare cancer, non-profit, cancer, glivec, cml, all, gist, gipap, tipap, treatment, partnering, international" />
    <meta name="description" content="Supporting people living with cancer worldwide" />
    <meta name="company" content="The Max Foundation" />
    <meta name="copyright" content="The Max Foundation" />
    <meta http-equiv="content-language" content="en" />
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="container_12">
        <!-- begin top navigation -->
        <div id="top_nav" class="grid_12">
            <ul>
                <li><a href="http://www.themaxfoundation.org">The Max Foundation Homepage</a></li>
            </ul>
        </div>
        <!-- end top navigation -->
        <div class="clear">
        </div>
        <div id="header" class="grid_12">
        </div>
        <div class="clear">
        </div>
        <div id="content" class="grid_12">
            <div id="Loginbox" class="grid_12 alpha">
                <h2>
                    Forgot your password?</h2>
                
                        <asp:Panel ID="PanelEmail" runat="server">
                            <p>
                                Enter your User Name <strong><font color="steelblue">OR</font></strong> Email Address
                                below to have an email sent to the email address we have on file for you.:</p>
                            <p>
                                <asp:Label ID="lblUsername" runat="server" Text="Username:" /><br />
                                <asp:TextBox ID="txtUsername" runat="server"></asp:TextBox></p>
                            <p>
                                <b>OR</b>
                            </p>
                            <p>
                                <asp:Label ID="LabelEmail" runat="server" Text="Email Address:" /><br />
                                <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox></p>
                            <p>
                                <asp:Button ID="ButtonEmail" runat="server" Text="Email me my password" OnClick="ButtonEmail_Click" Height="30px" Width="150px" /></p>
                            <p>
                                <asp:Label ID="LabelError" runat="server" Text="" ForeColor="Red"></asp:Label></p>
                        </asp:Panel>
                        <asp:Panel ID="PanelThanks" runat="server" Visible="false">
                            <p>
                                An email has been sent to the email address we have on file for you that contains
                                your&nbsp;user information.&nbsp;
                            </p>
                            <p>
                                If you are still having problems, please email <a href="mailto:gipap@themaxfoundation.org">
                                    gipap@themaxfoundation.org</a>.&nbsp;
                            </p>
                            <p>
                                Thank you.</p>
                        </asp:Panel>
            </div>
        </div>
        <!-- BEGIN footer -->
        <div class="grid_12">
            <div id="footer">
                <p class="l">
                    Copyright © 2014 - <a href="http://www.themaxfoundation.org/" target="_blank">The Max
                        Foundation</a> · All Rights Reserved
                </p>
                <ul>
                    <li><a href="http://www.themaxfoundation.org/termsofuse/default.aspx" target="_blank">
                        Terms Of Use</a></li>
                    <li><a href="http://www.themaxfoundation.org/privacypolicy/default.aspx" target="_blank">
                        Privacy Policy</a></li>
                    <li><a href="http://www.themaxfoundation.org/webmaster/default.aspx" target="_blank">
                        Webmaster</a></li>
                    <li><a href="http://www.themaxfoundation.org/Default.aspx" target="_blank">Home</a></li>
                </ul>
            </div>
        </div>
        <!-- END footer -->
        <div class="clear">
        </div>
    </div>
    </form>
</body>
</html>
