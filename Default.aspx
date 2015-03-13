<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>The Max Foundation</title>
    <link href="css/import_all.css" rel="stylesheet" type="text/css" />
    <meta http-equiv="Content-Type" content="text/html; charset=windows-1252" />
    <meta name="robots" content="noodp,noydir" />
    <meta name="keywords" content="blood cancer, rare cancer, non-profit, cancer, glivec, cml, all, gist, gipap, tipap, treatment, partnering, international" />
    <meta name="description" content="Supporting people living with cancer worldwide" />
    <meta name="company" content="The Max Foundation" />
    <meta name="copyright" content="The Max Foundation" />
    <meta http-equiv="content-language" content="en" />
    <!--[if IE]>
    <style type="text/css" media="screen">
    #menu ul li {float: left; width: 100%;}
    </style>
    <![endif]-->
    <!--[if lt IE 7]>
    <style type="text/css" media="screen">
    body {
    behavior: url(css/csshover.htc);
    font-size: 100%;
    } 
    #menu ul li a {height: 1%;}  
    #menu a, #menu h2 {
    font: bold 0.7em/1.4em arial, helvetica, sans-serif;
    }
    </style>
    <![endif]-->
    <!--GOOGLE ANALYTICS CODE -->

    <script type="text/javascript">

        var _gaq = _gaq || [];
        _gaq.push(['_setAccount', 'UA-18803291-1']);
        _gaq.push(['_trackPageview']);

        (function() {
            var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;
            ga.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www') + '.google-analytics.com/ga.js';
            var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);
        })();

    </script>

</head>
<body>
    <form id="form1" runat="server">
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
        <div class="clear"></div>
       
        <div id="content" class="grid_12">
            <div id="Loginbox" class="grid_12 alpha">
                <h2>
                    MAXAID.ORG - A website of The Max Foundation for healthcare professionals
                </h2>
                <div class="grid_2 alpha"><img src="images/PatsLogo.png" alt="PATS login"/></div>
                <div class="grid_4">
                    <p>
                        <label>Username:</label><br />
                        <asp:TextBox ID="txtUsername" runat="server" CssClass="logtext"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Username" ControlToValidate="txtUsername">*</asp:RequiredFieldValidator>
                    </p>
                    <p>
                        <label>Password:</label><br />
                        <asp:TextBox ID="txtPassword" runat="server" CssClass="logtext" TextMode="Password"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Password" ControlToValidate="txtPassword">*</asp:RequiredFieldValidator>
                    </p>
                </div>
                <div class="grid_3" style="height:90px;">
                    <asp:ValidationSummary ID="vs1" runat="server" ShowMessageBox="True" HeaderText="Please complete:" />
                    <asp:Label ID="LabelError" runat="server" Text="" ForeColor="Red"></asp:Label><br />
                    <asp:ImageButton ID="ButtonLogin" runat="server" CssClass="loginbutton" AlternateText="Log In" ImageUrl="images/LoginButton.png" onclick="ButtonLogin_Click" />
                </div>
                <div class="grid_3 omega">
                    <font class="passwordtext"><h1>Forgot Your Password?</h1><br /><a href="ForgotPassword.aspx">CLICK HERE</a> to have your password sent to you via email.</font>
                </div>
            </div>
            <div class="clear"></div>
            <!-- BEGIN project news -->
            <div id="projects" class="grid_12 alpha">
                <h3>
                    Featured Projects:
                </h3>
                <div id="featured_news" runat="server" />
            </div>
            <!-- END project news -->
            <!-- BEGIN blogs -->
            <div id="blogs" class="grid_12 alpha">
                <div id="center_content">
                    <div class="recent">
                        <div class="post">
                            <h3>
                                Maximize Life Blog Posts:</h3>
                            <div id="recent_blogs" runat="server" />
                            <p>
                                <a href="http://www.themaxfoundation.org/blog/Default.aspx" target="_blank">View all blog posts >></a></p>
                            <p>
                                <a href="http://www.themaxfoundation.org/publications/NewsletterSignup.aspx" target="_blank">Sign up for Blog Updates >></a></p>
                            <script src="https://seal.verisign.com/getseal?host_name=www.maxaid.org&size=L&use_flash=YES&use_transparent=YES&lang=en"></script>
                            <br />
                            <a href="http://www.verisign.com/ssl-certificate/" target="_blank" style="color: #000000; text-decoration: none; font: 8px verdana,sans-serif; letter-spacing: .5px; text-align: center; margin: 0px; padding: 0px;">ABOUT SSL CERTIFICATES</a>
                        </div>
                    </div>
                </div>
            </div>
            <!-- END blogs -->
            
        </div>
        
        <div class="clear">
        </div>
        
        <!-- BEGIN footer -->
        <div class="grid_12">
            <div id="footer">
                <p class="l">
                    Copyright &copy; 2014 - <a href="http://www.themaxfoundation.org/" target="_blank">The Max Foundation</a> &middot; All Rights Reserved
                </p>
                <ul>
                    <li><a href="http://www.themaxfoundation.org/termsofuse/default.aspx" target="_blank">Terms Of Use</a></li>
                    <li><a href="http://www.themaxfoundation.org/privacypolicy/default.aspx" target="_blank">Privacy Policy</a></li>
                    <li><a href="http://www.themaxfoundation.org/webmaster/default.aspx" target="_blank">Webmaster</a></li>
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
