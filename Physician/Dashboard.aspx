<%@ Page Title="" Language="C#" MasterPageFile="~/PATS.master" AutoEventWireup="true" CodeFile="Dashboard.aspx.cs" Inherits="Physician_Dashboard" %>
<%@ Register assembly="iucon.web.Controls.PartialUpdatePanel" namespace="iucon.web.Controls" tagprefix="cc1" %>
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
            <asp:HyperLink ID="hlGIPAP" runat="server" ForeColor="SteelBlue" Visible="false">GIPAP-NOA</asp:HyperLink>
&nbsp;
        <asp:HyperLink ID="hlTIPAP" runat="server" ForeColor="PURPLE" Visible="false">TIPAP</asp:HyperLink>
        </div>
        
        
        <cc1:partialupdatepanel ID="PartialUpdatePanelAllUsers" runat="server" 
                UserControlPath="UserMessage.ascx">
			<Parameters><cc1:Parameter Name="MyParameter" Value="Hello world" />
                            </Parameters>
    <LoadingTemplate>
    <img src="../Images/loading.gif" />
    </LoadingTemplate>
    <ErrorTemplate>
    Unable to load user queues.
    </ErrorTemplate>
</cc1:partialupdatepanel>

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
                <asp:Label ID="LabelName" runat="server" Text=""></asp:Label></div>
        </div>
        <div style="width:100%; clear:both; text-align: left;">
        <div class="MainColDivHeader">Workload</div><div class="MainColDivHeaderRight"></div>
        <div class="MainColDiv"><cc1:partialupdatepanel ID="PartialUpdatePanelQueues" 
                runat="server" UserControlPath="Physician/UserQueues.ascx">
			<Parameters><cc1:Parameter Name="MyParameter" Value="Hello world" />
                            </Parameters>
    <LoadingTemplate>
    <div style="width:100%; background-color:White">
    <img src="../Images/loading.gif" /></div>
    </LoadingTemplate>
    <ErrorTemplate>
    Unable to load user queues.
    </ErrorTemplate>
</cc1:partialupdatepanel></div>
        </div>
    </div>
</asp:Content>

