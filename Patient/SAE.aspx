<%@ Page Language="C#" MasterPageFile="~/Patient/GIPAPPatient.master" AutoEventWireup="true" CodeFile="SAE.aspx.cs" Inherits="Patient_SAE" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderControlPanel" Runat="Server">
    <asp:Panel ID="PanelExisting" runat="server" Visible="false">
    <div class="SAEDiv">
    <font color="#5A112C"><b>AE History</b></font>
        <asp:Label ID="LabelExisting" runat="server" Text=""></asp:Label></div>
        <div class="LeftColSpacer"></div>
    </asp:Panel>
    <div class="ControlPanelDivHeader">
                    Log Adverse Event</div>
                <div class="ControlPanelHeaderRight">
                </div>
<div class="FormDiv">
<TABLE id="Table11" cellSpacing="1" cellPadding="1" width="585" align="left" border="0">
		<TR>
			<TD><asp:validationsummary id="ValidationSummary2" runat="server" HeaderText="You are missing the following fields:"
		ShowMessageBox="True" CssClass="AlertDiv" ForeColor=""></asp:validationsummary></TD>
		</TR>
	</TABLE>
	<div style="clear:both;">
<table id="Table1" cellspacing="1" cellpadding="1" width="590" border="0">
    <tr><td>Email Address to be sent to:
        <asp:Label ID="LabelSAEEmail" runat="server"></asp:Label>
        </td></tr>
    <tr><td>
        <asp:Label ID="LabelEmailError" runat="server" ForeColor="Red"></asp:Label>
        </td></tr>
    </table>
<asp:Panel ID="PanelLog" runat="server">
    <table id="Table2" cellspacing="1" cellpadding="1" width="600" border="0">
    <tr>
        <td>
            <b>Date AE Reported to Max:</b>
            </td></tr>
        <tr>
        <td><asp:DropDownList id="dropDay" runat="server">
					<asp:ListItem Value="0" Selected="True">Day</asp:ListItem>
					<asp:ListItem Value="1">1</asp:ListItem>
					<asp:ListItem Value="2">2</asp:ListItem>
					<asp:ListItem Value="3">3</asp:ListItem>
					<asp:ListItem Value="4">4</asp:ListItem>
					<asp:ListItem Value="5">5</asp:ListItem>
					<asp:ListItem Value="6">6</asp:ListItem>
					<asp:ListItem Value="7">7</asp:ListItem>
					<asp:ListItem Value="8">8</asp:ListItem>
					<asp:ListItem Value="9">9</asp:ListItem>
					<asp:ListItem Value="10">10</asp:ListItem>
					<asp:ListItem Value="11">11</asp:ListItem>
					<asp:ListItem Value="12">12</asp:ListItem>
					<asp:ListItem Value="13">13</asp:ListItem>
					<asp:ListItem Value="14">14</asp:ListItem>
					<asp:ListItem Value="15">15</asp:ListItem>
					<asp:ListItem Value="16">16</asp:ListItem>
					<asp:ListItem Value="17">17</asp:ListItem>
					<asp:ListItem Value="18">18</asp:ListItem>
					<asp:ListItem Value="19">19</asp:ListItem>
					<asp:ListItem Value="20">20</asp:ListItem>
					<asp:ListItem Value="21">21</asp:ListItem>
					<asp:ListItem Value="22">22</asp:ListItem>
					<asp:ListItem Value="23">23</asp:ListItem>
					<asp:ListItem Value="24">24</asp:ListItem>
					<asp:ListItem Value="25">25</asp:ListItem>
					<asp:ListItem Value="26">26</asp:ListItem>
					<asp:ListItem Value="27">27</asp:ListItem>
					<asp:ListItem Value="28">28</asp:ListItem>
					<asp:ListItem Value="29">29</asp:ListItem>
					<asp:ListItem Value="30">30</asp:ListItem>
					<asp:ListItem Value="31">31</asp:ListItem>
					<asp:ListItem></asp:ListItem>
				</asp:DropDownList>
				<asp:DropDownList id="dropMonth" runat="server">
					<asp:ListItem Value="0">Month</asp:ListItem>
					<asp:ListItem Value="1">January</asp:ListItem>
					<asp:ListItem Value="2">February</asp:ListItem>
					<asp:ListItem Value="3">March</asp:ListItem>
					<asp:ListItem Value="4">April</asp:ListItem>
					<asp:ListItem Value="5">May</asp:ListItem>
					<asp:ListItem Value="6">June</asp:ListItem>
					<asp:ListItem Value="7">July</asp:ListItem>
					<asp:ListItem Value="8">August</asp:ListItem>
					<asp:ListItem Value="9">September</asp:ListItem>
					<asp:ListItem Value="10">October</asp:ListItem>
					<asp:ListItem Value="11">November</asp:ListItem>
					<asp:ListItem Value="12">December</asp:ListItem>
				</asp:DropDownList>
				<asp:DropDownList id="dropYear" runat="server">
            <asp:ListItem Value="0">Year</asp:ListItem>
            </asp:DropDownList>
            </td>
    </tr>
    <tr><td style="height:25px">
        <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Day" 
            ControlToValidate="dropDay" Operator="NotEqual" ValueToCompare="0"></asp:CompareValidator>
            <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="Month" 
            ControlToValidate="dropMonth" Operator="NotEqual" ValueToCompare="0"></asp:CompareValidator>
            <asp:CompareValidator ID="CompareValidator3" runat="server" ErrorMessage="Year" 
            ControlToValidate="dropYear" Operator="NotEqual" ValueToCompare="0"></asp:CompareValidator></td></tr>
    <tr>
        <td>
            How/From whom did you learn about this Adverse Event?:
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                ControlToValidate="rblstLearned" ErrorMessage="How did you learn of this event">Required</asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td>
            <asp:RadioButtonList ID="rblstLearned" runat="server" Width="320px" 
                AutoPostBack="True" onselectedindexchanged="rblstLearned_SelectedIndexChanged">
                <asp:ListItem Value="Physician">Physician</asp:ListItem>
                <asp:ListItem Value="Patient/caregiver">Patient/caregiver</asp:ListItem>
            </asp:RadioButtonList>
        </td>
    </tr>
    <tr><td style="height:25px">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="rblstLearned" EventName="SelectedIndexChanged" />
            </Triggers>
            <ContentTemplate>
                <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="0" DynamicLayout="true">
                    <ProgressTemplate>
                       <img alt="..loading" src="../Images/loading.gif" />
                    </ProgressTemplate>
                </asp:UpdateProgress>
                <asp:Panel ID="PanelConsent" runat="server" >
                    <asp:CheckBox ID="cbConsent" runat="server" Font-Bold="True" 
                        Font-Italic="False" Text="By clicking here you agree that:" />
                    <asp:Label ID="LabelError" runat="server" ForeColor="Red" Text="You must agree" 
                        Visible="False"></asp:Label>
                    <br /><i>I have explained to the patient/caregiver that I will share information regarding the AE with Novartis as per Health Authorities regulations. I have further explained that no identifier information will be shared, and only the Patient Identification Number (PIN) will be included in the report. Novartis might inquire with the treating physician in regards to this report.</i></asp:Panel><br />
            </ContentTemplate>
        </asp:UpdatePanel>
    </td></tr>
    <tr>
        <td>
            <p>
                Write a short description of the Adverse Event as it was described to you. Be sure
                to identify the person from who you learned of AE and a description of the AE <u><em>
                    as it will be conveyed in letter</em></u> to Novartis:
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Event Description"
                    ControlToValidate="txtEvent">*</asp:RequiredFieldValidator></p>
        </td>
    </tr>
    <tr>
        <td>
            <asp:TextBox ID="txtEvent" runat="server" Width="580px" TextMode="MultiLine" Height="104px"></asp:TextBox>
        </td>
    </tr>
    <tr><td>
        <asp:Panel ID="PanelClose" runat="server" Visible="false">
        Does this Patient Case Need to be Closed?:<br />
        <asp:RadioButtonList ID="rblstClosed" runat="server">
            <asp:ListItem>No</asp:ListItem>
            <asp:ListItem>Yes</asp:ListItem>
        </asp:RadioButtonList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                ControlToValidate="rblstClosed" 
                ErrorMessage="Does this case need to be closed?">Required</asp:RequiredFieldValidator>
        </asp:Panel>
        </td></tr>
    <tr>
        <td>
            <asp:Button ID="ButtonSubmitEvent" runat="server" Text="Submit" OnClick="ButtonSubmitEvent_Click">
            </asp:Button>&nbsp;
            <asp:Button ID="ButtonCancel" runat="server" Text="Cancel" CausesValidation="False"
                OnClick="ButtonCancel_Click"></asp:Button>
            <asp:Button ID="ButtonResolve" runat="server" CausesValidation="False" 
                ForeColor="Red" onclick="ButtonResolve_Click" 
                Text="Do not log this Adverse Event" Visible="False" />
        </td>
    </tr>
</table>
</asp:Panel>
</div>
</div>
</asp:Content>

