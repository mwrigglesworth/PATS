<%@ Page Title="" Language="C#" MasterPageFile="~/Patient/GIPAPPatient.master" AutoEventWireup="true" CodeFile="UpdateTabletStrength.aspx.cs" Inherits="Patient_UpdateTabletStrength" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderControlPanel" Runat="Server">
<div class="ControlPanelDivHeader">
                    Update Tablet Strength</div>
                <div class="ControlPanelHeaderRight">
                </div>
<div class="FormDiv">
<TABLE id="Table11" cellSpacing="1" cellPadding="1" width="585" align="left" border="0">
		<TR>
			<TD><asp:validationsummary id="ValidationSummary1" runat="server" HeaderText="You are missing the following fields:"
		ShowMessageBox="True" CssClass="AlertDiv" ForeColor=""></asp:validationsummary></TD>
		</TR>
	</TABLE>
	<div style="clear:both;">
    <asp:Panel ID="PanelTablet" runat="server"><br /><br />
                            Tablet Strength: 
                        <asp:DropDownList ID="dropTablet" runat="server">
                        </asp:DropDownList>
                        <br />
                        <br />
                        <asp:Button id="ButtonReApprove" runat="server" Width="75px" Text="Submit" 
                                onclick="ButtonReApprove_Click" style="height: 26px"></asp:Button>&nbsp;
							<asp:Button id="ButtonCancel" runat="server" Width="75px" Text="Cancel" CausesValidation="False" onclick="ButtonCancel_Click"></asp:Button>
                            </asp:Panel>
    </div>
    </div>
</asp:Content>

