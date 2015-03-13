<%@ Page Language="C#" MasterPageFile="~/PATS.master" AutoEventWireup="true" CodeFile="Dashboard.aspx.cs" Inherits="TMF_Dashboard" %>
<%@ Register Assembly="iucon.web.Controls.PartialUpdatePanel" Namespace="iucon.web.Controls"
    TagPrefix="iucon" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<div id="LeftCol">
        <div style="float:left; width:100px;"><img src="../Images/Dashboardp.png" /></div>
        <div><h1>My Dashboard</h1>
            <asp:Label ID="LabelProgram" runat="server" Text=""></asp:Label>
        </div>
        <div class="LeftColSpacer">
            <asp:HyperLink ID="hlGIPAP" runat="server" Font-Underline="false" ForeColor="SteelBlue">GIPAP-NOA</asp:HyperLink>
&nbsp;
        <asp:HyperLink ID="hlPINC" runat="server" ForeColor="DeepPink" Font-Underline="false" >PINC</asp:HyperLink>
&nbsp;
        <asp:HyperLink ID="hlMYPAP" runat="server" ForeColor="DarkOrange" Font-Underline="false" style="text-decoration:none;">MYPAP</asp:HyperLink>
&nbsp;
        <asp:HyperLink ID="hlTIPAP" runat="server" Font-Underline="false" ForeColor="PURPLE">TIPAP</asp:HyperLink>
        </div>
        <div class="LeftColDivHeader">All Users</div>
        <div class="LeftColDiv">
        <iucon:PartialUpdatePanel ID="PartialUpdatePanelAllUsers" runat="server" UserControlPath="TMF/AllUsers.ascx">
			<Parameters><iucon:Parameter Name="MyParameter" Value="Hello world" />
                            </Parameters>
    <LoadingTemplate>
    <img src="../Images/loading.gif" />
    </LoadingTemplate>
    <ErrorTemplate>
    Unable to load user queues.
    </ErrorTemplate>
</iucon:PartialUpdatePanel></div>
    </div><!-- end left col -->
    <div id="MainCol">
        <% string choice;
    try
    {
        choice = Request.QueryString["choice"].ToString();
    }
    catch
    {
        choice = "0";
    }
    iucon.web.Controls.Parameter mwParam = new iucon.web.Controls.Parameter("choice", choice);
    PartialUpdatePanelQueues.Parameters.Add(mwParam);
    PartialUpdatePanelAllUsers.Parameters.Add(mwParam);                  %>
    <div style="width: 100%; font-size: 14pt; text-align: left;">
            <div style="width: 72px; float: left;">
                <img alt="User Work Load" src="../Images/Queuesp.png" /></div>
            <div style="padding-left: 10px; padding-top: 5px; width: 450px; height: 52px;">
                <asp:Label ID="LabelName" runat="server" Text=""></asp:Label><br />
                <asp:LinkButton ID="lbMakeTempHp" runat="server" ForeColor="Gray" Font-Size="10pt"
                    Visible="False" onclick="lbMakeTempHp_Click">Temporarily Make this My Homepage</asp:LinkButton></div>
        </div>
        <div style="width:100%; clear:both; text-align: left;">
            <asp:Panel ID="PanelTempHp" runat="server" Width="585" Visible="False">
            <div class="AlertDiv">You have chosen to temporarily make this user's page your homepage.&nbsp; If you
                                would like your own homepage to be the default again, please click the link below:<br /><br />
                                <asp:LinkButton ID="lbMyHomepage" runat="server" 
                    onclick="lbMyHomepage_Click">Return to My Homepage</asp:LinkButton></div>
            </asp:Panel>
        </div>
        <div style="width:100%; clear:both; text-align: left; padding:5px; height:30px;">
            <asp:DropDownList ID="dropOtherUsers" runat="server" AutoPostBack="True" 
                onselectedindexchanged="dropOtherUsers_SelectedIndexChanged">
            </asp:DropDownList>
        </div>
        <div style="width:100%; clear:both; text-align: left;">
        <div class="MainColDivHeader">Workload
        <asp:HyperLink ID="QueMypap" runat="server" ForeColor="DarkOrange" Font-Bold="false" Font-Underline="false" NavigateUrl="../GIPAPTrusted.aspx?reqform=home"></asp:HyperLink>
        <asp:HyperLink ID="QuePinc" runat="server" ForeColor="DeepPink" Font-Bold="false" Font-Underline="false" NavigateUrl="../GIPAPTrusted.aspx?reqform=home"></asp:HyperLink>
        </div><div class="MainColDivHeaderRight"></div>
        <div class="MainColDiv"><iucon:PartialUpdatePanel ID="PartialUpdatePanelQueues" runat="server" UserControlPath="TMF/UserQueues.ascx">
			<Parameters><iucon:Parameter Name="MyParameter" Value="Hello world" />
                            </Parameters>
    <LoadingTemplate>
    <div style="width:100%; background-color:White">
    <img src="../Images/loading.gif" /></div>
    </LoadingTemplate>
    <ErrorTemplate>
    Unable to load user queues.
    </ErrorTemplate>
</iucon:PartialUpdatePanel></div>
        </div>
    </div>
</asp:Content>

