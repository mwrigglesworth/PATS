<%@ Page Title="" Language="C#" MasterPageFile="~/PATS.master" AutoEventWireup="true" CodeFile="Dashboard.aspx.cs" Inherits="NovartisUser_Dashboard" %>
<%@ Register Assembly="iucon.web.Controls.PartialUpdatePanel" Namespace="iucon.web.Controls"
    TagPrefix="iucon" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <input type="hidden" id="personid" value="<%=choice%>" />
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<div id="LeftCol">
        <div style="float:left; width:100px;"><img src="../Images/Dashboardp.png" /></div>
        <div><h1>My Dashboard</h1>
            <asp:Label ID="LabelProgram" runat="server" Text=""></asp:Label>
        </div>
        <div class="LeftColSpacer"></div>
        <div class="LeftColDivHeader">My Profile</div>
        <div class="LeftColDiv">
        <iucon:partialupdatepanel ID="PartialUpdatePanelAllUsers" runat="server" 
                UserControlPath="Profile.ascx">
    <LoadingTemplate>
    <img src="../Images/loading.gif" />
    </LoadingTemplate>
    <ErrorTemplate>
    Unable to load user queues.
    </ErrorTemplate>
</iucon:partialupdatepanel>

        
        </div>
    </div><!-- end left col -->
    <div id="MainCol">
    <div style="width: 100%; font-size: 14pt; text-align: left;">
            <div style="width: 72px; float: left;">
                <img alt="User Work Load" src="../Images/Queuesp.png" /></div>
            <div style="padding-left: 10px; padding-top: 5px; width: 450px; height: 52px;">
                <asp:Label ID="LabelName" runat="server" Text=""></asp:Label></div>
        </div>
        <div style="width:100%; clear:both; text-align: left; padding:5px; height:30px;">
            
        </div>
        <div style="width:100%; clear:both; text-align: left;">
        <div class="MainColDivHeader">Reports</div><div class="MainColDivHeaderRight"></div>
        <div class="MainColDiv">
            <asp:Label ID="LabelCountries" runat="server" Text=""></asp:Label>
        </div>
        </div>
    </div>
</asp:Content>

