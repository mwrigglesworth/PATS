<%@ Page Title="" Language="C#" MasterPageFile="~/PATS.master" AutoEventWireup="true" CodeFile="ChangePassword.aspx.cs" Inherits="Physician_ChangePassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div style="width:100%; clear:both; text-align: left;">
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<h1>Change Password</h1>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
<asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="0" DynamicLayout="true">
            <ProgressTemplate>
                <div style="width: 100%; background-color: White">
                    <img src="../Images/loading.gif" /></div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    <asp:Panel ID="PanelChange" runat="server">
    <asp:Label ID="LabelExpire" runat="server" Text="" ForeColor="Red"></asp:Label>Enter your desired password in the boxes below.  Your password must be at least 8 characters long, contain both upper and lowercase characters, and contain at least 1 numeric value and 1 symbol character.
<br /><br />
New Password:<br />
        <asp:TextBox ID="txtPassword" runat="server" Width="200px" TextMode="Password"></asp:TextBox>

                        <div id="passwordDescription" class="Subtext">Password not entered</div>

                        <div id="passwordStrength" class="strength0"></div>
                        <br /><br />
                        Confirm Password:<br />
    <asp:TextBox ID="txtPasswordConfirm" runat="server" Width="200px" TextMode="Password"></asp:TextBox><asp:Label id="LabelError" ForeColor="Red" runat="server" Visible="False">The passwords entered do not match.</asp:Label><br /><br />
    <asp:Button ID="ButtonUpdate" runat="server" Text="Update Password" 
            onclick="ButtonUpdate_Click" />&nbsp; <asp:Button
        ID="ButtonCancel" runat="server" Text="Cancel" />
        </asp:Panel>
    <asp:Panel ID="PanelThanks" runat="server" Visible="false">
    Thank you.  Your password has been updated.
    </asp:Panel>
    <asp:Panel ID="PanelNonPersonal" runat="server" Visible="false">
        You are logged in as a non-personal account, so the password can&#39;t be updated. 
        To receive your own account and password, please email
        <a href="mailto:gipap@themaxfoundation.org">gipap@themaxfoundation.org</a>.&nbsp;
    </asp:Panel>
    </ContentTemplate>
    </asp:UpdatePanel>
</div>
                <script language="javascript">
                    function passwordStrength(password) {

                        var desc = new Array();

                        desc[0] = "Very Weak";

                        desc[1] = "Weak";

                        desc[2] = "Better";

                        desc[3] = "Medium";

                        desc[4] = "Strong";

                        desc[5] = "Strongest";



                        var score = 0;



                        //if password bigger than 8 give 1 point

                        if (password.length > 8) score++;



                        //if password has both lower and uppercase characters give 1 point      

                        if ((password.match(/[a-z]/)) && (password.match(/[A-Z]/))) score++;



                        //if password has at least one number give 1 point

                        if (password.match(/\d+/)) score++;



                        //if password has at least one special caracther give 1 point

                        if (password.match(/.[!,@,#,$,%,^,&,*,?,_,~,-,(,)]/)) score++;



                        //if password bigger than 12 give another 1 point

                        if (password.length > 12) score++;



                        document.getElementById("passwordDescription").innerHTML = desc[score];

                        document.getElementById("passwordStrength").className = "strength" + score;

                    }
                </script>
</asp:Content>

