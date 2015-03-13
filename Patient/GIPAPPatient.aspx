<%@ Page Language="C#" MasterPageFile="~/PATS.master" AutoEventWireup="true" CodeFile="GIPAPPatient.aspx.cs" Inherits="Patient_GIPAPPatient" %>
<%@ Register assembly="iucon.web.Controls.PartialUpdatePanel" namespace="iucon.web.Controls" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager><% string choice;
    try
    {
        choice = Request.QueryString["choice"].ToString();
    }
    catch
    {
        choice = "0";
    }
    iucon.web.Controls.Parameter mwParam = new iucon.web.Controls.Parameter("choice", choice);
    PartialUpdatePanelHead.Parameters.Add(mwParam);
    PartialUpdatePanelStatInfo.Parameters.Add(mwParam);
    PartialUpdatePanelDiagInfo.Parameters.Add(mwParam);
    PartialUpdatePanelPersonelAssignment.Parameters.Add(mwParam);          %>
    <div id="LeftCol">
        <cc1:PartialUpdatePanel ID="PartialUpdatePanelHead" runat="server" UserControlPath="Patient/GIPAPAsync/PatInfo.ascx">
            <Parameters>
                <cc1:Parameter Name="MyParameter" Value="Hello world" />
            </Parameters>
            <LoadingTemplate>
                <div style="width: 100%; background-color: White">
                    <img src="../Images/loading.gif" /></div>
            </LoadingTemplate>
            <ErrorTemplate>
                Unable to load patient info.
            </ErrorTemplate>
        </cc1:PartialUpdatePanel>   
        <cc1:PartialUpdatePanel ID="PartialUpdatePanelStatInfo" runat="server" UserControlPath="Patient/GIPAPAsync/StatInfo.ascx">
            <Parameters>
                <cc1:Parameter Name="MyParameter" Value="Hello world" />
            </Parameters>
            <LoadingTemplate>
                <div style="width: 100%; background-color: White">
                    <img src="../Images/loading.gif" /></div>
            </LoadingTemplate>
            <ErrorTemplate>
                Unable to load patient info.
            </ErrorTemplate>
        </cc1:PartialUpdatePanel> 
        <cc1:PartialUpdatePanel ID="PartialUpdatePanelDiagInfo" runat="server" UserControlPath="Patient/GIPAPAsync/DiagInfo.ascx">
            <Parameters>
                <cc1:Parameter Name="MyParameter" Value="Hello world" />
            </Parameters>
            <LoadingTemplate>
                <div style="width: 100%; background-color: White">
                    <img src="../Images/loading.gif" /></div>
            </LoadingTemplate>
            <ErrorTemplate>
                Unable to load patient info.
            </ErrorTemplate>
        </cc1:PartialUpdatePanel> 
    </div>
    <!-- end left col -->
    <div id="MainCol">
        <cc1:PartialUpdatePanel ID="PartialUpdatePanelPersonelAssignment" runat="server"
            UserControlPath="Patient/GIPAPAsync/PersonelAssignment.ascx">
            <Parameters>
                <cc1:Parameter Name="MyParameter" Value="Hello world" />
            </Parameters>
            <LoadingTemplate>
                <div style="width: 100%; background-color: White">
                    <img src="../Images/loading.gif" /></div>
            </LoadingTemplate>
            <ErrorTemplate>
                Unable to load patient info.
            </ErrorTemplate>
        </cc1:PartialUpdatePanel>
        <div class="LeftColSpacer"></div>
        <!-- start control panel -->
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="0" DynamicLayout="true">
                    <ProgressTemplate>
                        <img src="../Images/loading.gif" /></ProgressTemplate>
                </asp:UpdateProgress>
                <asp:Panel ID="PanelControlPan" runat="server">
                <div class="ControlPanelDivHeader">
                    Control Panel</div>
                <div class="ControlPanelHeaderRight">
                </div>
                <div id="ControlPanelDiv">
                    <asp:LinkButton ID="lbEdit" runat="server" onclick="lbEdit_Click">Edit Patient Info</asp:LinkButton> 
                </div>
                <div class="ControlPanelDivFooter">
                </div>
                <div class="ControlPanelFooterRight">
                </div>
                </asp:Panel>
                <asp:Panel ID="PanelLoad" runat="server" Width="600">
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>

