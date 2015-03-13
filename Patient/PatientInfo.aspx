<%@ Page Language="C#" MasterPageFile="~/Patient/GIPAPPatient.master" AutoEventWireup="true" CodeFile="PatientInfo.aspx.cs" Inherits="Patient_GIPAPControlPanel_PatientInfo" %>
<%@ Register assembly="iucon.web.Controls.PartialUpdatePanel" namespace="iucon.web.Controls" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderControlPanel" Runat="Server">
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
    PartialUpdatePanelPersonelAssignment.Parameters.Add(mwParam);
    PartialUpdatePanelCaseNotes.Parameters.Add(mwParam);         %>
    <cc1:partialupdatepanel ID="PartialUpdatePanelPersonelAssignment" runat="server"
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
        </cc1:partialupdatepanel>
        <div class="LeftColSpacer">
        <asp:Panel ID="PanelAlert" runat="server" Width="585" Visible="False">
            <div class="AlertDiv">
                <asp:Label ID="LabelAlert" runat="server" Text=""></asp:Label></div>
            </asp:Panel>
        </div>
        <!-- start control panel -->
        <div class="ControlPanelDivHeader">
                    Control Panel</div>
                <div class="ControlPanelHeaderRight">
                </div>
                <div id="ControlPanelDiv">
                <div class="ControlPanelSplit">
                    <asp:Label ID="LabelList" runat="server" Text=""></asp:Label> 
                    </div>
                    <div class="ControlPanelSplit">
                    <asp:Label ID="LabelStatOptions" runat="server" Text=""></asp:Label> 
                    </div>
                </div>
                <div class="ControlPanelDivFooter">
                </div>
                <div class="ControlPanelFooterRight">
                </div>
                <div class="LeftColSpacer"></div>
                <cc1:partialupdatepanel ID="PartialUpdatePanelCaseNotes" runat="server"
            UserControlPath="Patient/GIPAPAsync/CaseNotes.ascx">
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
        </cc1:partialupdatepanel>
</asp:Content>

