<%@ Page Language="C#" MasterPageFile="~/Distributor/Distributor.master" AutoEventWireup="true" CodeFile="DistributorInfo.aspx.cs" Inherits="Distributor_DistributorInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderControlPanel" Runat="Server">
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
            <asp:Label ID="LabelApprove" runat="server" Text=""></asp:Label><br />
            <asp:LinkButton ID="lbApprove" CssClass="lbAR" runat="server" 
                onclick="lbApprove_Click"></asp:LinkButton>
        </div>
        <div class="PersonelColPhy">
            <asp:Label ID="LabelAdmin" runat="server" Text=""></asp:Label>
        </div>
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
</asp:Content>

