<%@ Page Title="" Language="C#" MasterPageFile="~/SubRegion/GIPAPSubRegion.master" AutoEventWireup="true" CodeFile="SubRegionInfo.aspx.cs" Inherits="SubRegion_SubRegionInfo" %>
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
    PartialUpdatePanelCaseNotes.Parameters.Add(mwParam);         %>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
<asp:UpdateProgress ID="UpdateProgress2" runat="server" DisplayAfter="0" DynamicLayout="true" AssociatedUpdatePanelID="UpdatePanel2">
        <ProgressTemplate><div style="width: 100%; background-color: White">
                        <img src="../Images/loading.gif" /></div>            
            </ProgressTemplate>
             </asp:UpdateProgress>
             <asp:Panel ID="PanelDisplay" runat="server">
    <div class="PersonelDiv">
        <div class="PersonelColPhy">
        Sub Region Coordinator:<asp:LinkButton ID="lbSRCC" CssClass="lbAR" runat="server" 
                onclick="lbSRCC_Click">[+/-]</asp:LinkButton><br />
            <asp:Label ID="LabelSRCC" runat="server" Text=""></asp:Label>
        </div>
        <div class="PersonelColPhy">
            <asp:Label ID="LabelRegion" runat="server" Text=""></asp:Label>
        </div>
        </div>
</asp:Panel>
<asp:Panel ID="PanelUpdate" runat="server" Visible="false">
    <div class="PersonelUpdateDiv">
        <h2>Sub Region Coordinator</h2>
        <div style="clear:both;">
        <asp:CheckBoxList ID="cblstPersonel" runat="server" RepeatColumns="2">
        </asp:CheckBoxList></div>
        <hr />
        <asp:Button ID="ButtonUpdateSRCC" runat="server" 
            Text="Update Sub Region Coordinator" onclick="ButtonUpdateSRCC_Click" />
        </div>
</asp:Panel>
</ContentTemplate>
</asp:UpdatePanel>
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
                    <asp:Label ID="LabelActions" runat="server" Text=""></asp:Label> 
                    </div>
                </div>
                <div class="ControlPanelDivFooter">
                </div>
                <div class="ControlPanelFooterRight">
                </div>
                <!-- end control panel-->
                <div class="LeftColSpacer"></div>
                <cc1:partialupdatepanel ID="PartialUpdatePanelCaseNotes" runat="server"
            UserControlPath="SubRegion/GIPAPAsync/SubRegionNotes.ascx">
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

