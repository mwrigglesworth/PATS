<%@ Page Title="" Language="C#" MasterPageFile="~/Patient/GIPAPPatient.master" AutoEventWireup="true" CodeFile="Unexpire.aspx.cs" Inherits="Patient_Unexpire" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderControlPanel" Runat="Server">
<div class="ControlPanelDivHeader">
                    Un-Expire Order</div>
                <div class="ControlPanelHeaderRight">
                </div>
<div class="FormDiv">
	<div style="clear:both;">
    <asp:Panel ID="PanelTablet" runat="server"><br /><br />
        <asp:Panel ID="Panel1" runat="server">
        <div class="AlertDiv">
                <br />
                                <asp:RadioButtonList ID="rblstNotPickedUp" runat="server" 
                                    RepeatDirection="Horizontal">
                                    <asp:ListItem>The previous supply order was not picked up. Patient has been contacted and counseled.</asp:ListItem>
                                </asp:RadioButtonList><br />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" 
                                    ErrorMessage="Patient must be contacted and counseled" ControlToValidate="rblstNotPickedUp"></asp:RequiredFieldValidator>
                                </div>
        </asp:Panel>
                        <br />
                        <br />
                        <asp:Button id="ButtonReApprove" runat="server" Width="75px" Text="Submit" 
                                onclick="ButtonReApprove_Click" style="height: 26px"></asp:Button>&nbsp;
							<asp:Button id="ButtonCancel" runat="server" Width="75px" Text="Cancel" CausesValidation="False" onclick="ButtonCancel_Click"></asp:Button>
                            </asp:Panel>
    </div>
    </div>
</asp:Content>

