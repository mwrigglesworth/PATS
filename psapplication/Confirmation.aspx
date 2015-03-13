<%@ Page Title="" Language="C#" MasterPageFile="~/psapplication/MasterPage.master"
    AutoEventWireup="true" CodeFile="Confirmation.aspx.cs" Inherits="psapplication_Confirmation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="content" class="grid_12">
        <div id="center_content">
            <!-- BEGIN recent posts  -->
            <div class="recent">
                <!-- begin post -->
                <div class="post">
                    <h3>
                        Your information has been received</h3>
                    <p>
                        Thank you for your email! Your inquiry will be reviewed by a member of our team
                        and you can expect to hear back within two business days. We look forward to being
                        in contact shortly.</p>
                    <p>
                        Sincerely,<br />
                        The Max Foundation</p>
                    <hr />
                    <p>
                        <a href="http://www.themaxfoundation.org">Return to The Max Foundation Homepage</a></p>
                </div>
            </div>
            <!-- END recent posts  -->
        </div>
        <!-- END content -->
    </div>
    <!-- end content inner -->
</asp:Content>
