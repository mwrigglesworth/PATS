<%@ Page Language="C#" MasterPageFile="~/Patient/GIPAPPatient.master" AutoEventWireup="true" CodeFile="DoseChange.aspx.cs" Inherits="Patient_DoseChange" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderControlPanel" Runat="Server">
    <div class="ControlPanelDivHeader">
                    Patient Dosage Change</div>
                <div class="ControlPanelHeaderRight">
                </div>
<div class="FormDiv">
<TABLE id="Table11" cellSpacing="1" cellPadding="1" width="585" align="left" border="0">
		<TR>
			<TD><asp:validationsummary id="ValidationSummary1" runat="server" HeaderText="You are missing the following fields:"
		ShowMessageBox="True" CssClass="AlertDiv" ForeColor=""></asp:validationsummary></TD>
		</TR>
			<TD>
                <asp:Panel ID="PanelObsolete" runat="server" Visible="False">
                <div class="AlertDiv">
                The current status of this 
patient renders the following request obsolete.&nbsp; An action may have been 
executed for this patient since this request was submitted.  Click below to remove the request.
                </div>
                </asp:Panel>
            </TD>
	</TABLE>
	<div style="clear:both;">
	<TABLE id="Table22" cellSpacing="1" cellPadding="1" width="600" align="center" border="0">
			<TR>
				<TD>Request to change dosage to*:
					<asp:DropDownList id="dropDosageChange" runat="server" Width="100px">
						<asp:ListItem Value="0">Select One</asp:ListItem>
					</asp:DropDownList>
					<asp:CompareValidator id="CompareValidator10" runat="server" ErrorMessage="Must Select Dosage" Operator="NotEqual"
						ValueToCompare="0" ControlToValidate="dropDosageChange">*</asp:CompareValidator></TD>
			</TR>
			<TR>
				<TD><EM><FONT color="gray">
							<asp:Label id="LabelDoseChangeDosageMessage" ForeColor="Gray" runat="server" Font-Italic="True">*Dosages of 200mg and 260mg are pediatric only</asp:Label></FONT></EM></TD>
			</TR>
			<tr><td>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
<asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="0" DynamicLayout="true">
        <ProgressTemplate><img alt="..loading" src="../Images/loading.gif" />            
            </ProgressTemplate>
             </asp:UpdateProgress>
    <asp:CheckBox ID="cbAERelatedDoseChange" runat="server" 
                    Text="Dosage change is considered to be AE related" 
        AutoPostBack="True" oncheckedchanged="cbAERelatedDoseChange_CheckedChanged" /><br />
                    <asp:Panel ID="PanelDoseChangeReason" runat="server" 
        Visible="false" >
                    <asp:Label ID="LabelPOWarningDoseChange" runat="server" 
                            Text="PLEASE NOTE: THESE DETAILS WILL BE EMAILED IMMEDIATELY AS AN ADVERSE EVENT REPORT TO NOVARTIS" 
                            ForeColor="#943634" Visible="False"></asp:Label><br />
                            <asp:Label ID="LabelPhysWarning" runat="server" 
                            Text="PLEASE NOTE: Do not provide patient identifying information (such as name) as these details will be included in an adverse event report to Novartis" 
                            ForeColor="#943634" Visible="False"></asp:Label>
    <font color="red">Please provide further details </font>

    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" 
     ControlToValidate="txtChangeDoseReason" ErrorMessage="Please provide further details">Required
     </asp:RequiredFieldValidator>
     
     <br />
    <asp:TextBox id="txtChangeDoseReason" runat="server" Width="580px" Height="80px" TextMode="MultiLine"></asp:TextBox>
    
    <br />

     <asp:Panel ID="PanelAEDateLearned" runat="server" Visible="false">
            <b>Date AE Reported to Max:</b>
            <br/>
        <asp:DropDownList id="dropDay" runat="server">
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
				</asp:DropDownList>&nbsp;
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
				</asp:DropDownList>&nbsp;
				<asp:DropDownList id="dropYear" runat="server">
            <asp:ListItem Value="0">Year</asp:ListItem>
            </asp:DropDownList><br />
    
        <asp:CompareValidator id="CompareValidator3" runat="server" ControlToValidate="dropDay" ValueToCompare="0"
				Operator="NotEqual" ErrorMessage="Day learned">*</asp:CompareValidator>
			<asp:CompareValidator id="CompareValidator1" runat="server" ControlToValidate="dropMonth" ValueToCompare="0"
				Operator="NotEqual" ErrorMessage="Month learned">*</asp:CompareValidator>
            <asp:CompareValidator id="CompareValidator2" runat="server" ControlToValidate="dropYear" ValueToCompare="0"
				Operator="NotEqual" ErrorMessage="Year learned">*</asp:CompareValidator>
           <asp:CustomValidator ID="CustomValidator2" runat="server" ErrorMessage="Please enter a valide Date when AE was learned" 
            ControlToValidate="dropYear" ClientValidationFunction="validateDateLearned"></asp:CustomValidator><br />
           </asp:Panel>                           
    </asp:Panel>
    </ContentTemplate></asp:UpdatePanel>
                </td></tr>
			<tr><td>
                <asp:Label ID="LabelDoseChangeDefinition" runat="server" Font-Italic="True" 
                    ForeColor="#943634" 
                    Text="&lt;u&gt;Definition of AE:&lt;/u&gt; “Any untoward medical occurrence in a patient administered a pharmaceutical product that does not necessarily have a causal relationship with the treatment.  An AE can therefore be any unfavorable and unintended sign (including an abnormal laboratory finding), symptom, or disease temporally associated with the use of the medicinal product, whether or not related to the medicinal products.  In addition, all reports of the following also should be reported even if no AE has been reported: Drug-drug interaction, Drug exposure during pregnancy (via the mother or father with or without outcome), Drug use during lactation or breast-feeding, Lack of efficacy, Overdose, Drug abuse and misuse, Drug maladministration or accidental exposure, Dispensing errors / Medication errors, and Withdrawal or rebound symptoms.”" 
                    Visible="False"></asp:Label>
                </td></tr>
			<TR>
			<TD></TD>
		</TR>
		<TR>
			<TD>
				</TD>
		</TR>
			<TR>
				<TD>
                    
                </TD>
			</TR>
			<tr><TD><asp:Panel id="PanelDoseReceived" runat="server">Was this requested by the physician?<br />
                                <asp:RadioButtonList ID="rblstDosePhysReq" runat="server" 
                                    RepeatDirection="Horizontal">
                                    <asp:ListItem>No</asp:ListItem>
                                    <asp:ListItem Selected="True">Yes</asp:ListItem>
                                </asp:RadioButtonList>
                                <br /><br />This information was 
                                received via:<BR>
<asp:DropDownList id="dropDoseReceived" runat="server">
									<asp:ListItem Value="0">Select One</asp:ListItem>
									<asp:ListItem Value="Fax from physician">Fax from physician</asp:ListItem>
									<asp:ListItem Value="Email from physician">Email from physician</asp:ListItem>
									<asp:ListItem Value="Verbal verification from physician">Verbal verification from physician</asp:ListItem>
									<asp:ListItem Value="Written notification from physician">Written notification from physician</asp:ListItem>
								</asp:DropDownList>
<asp:CustomValidator ID="CustomValidator7" runat="server" 
                                    ClientValidationFunction="validateReceiveYes" ControlToValidate="rblstDosePhysReq" 
                                    ErrorMessage="Information Received By">*</asp:CustomValidator></asp:Panel></TD></tr>
			<TR>
				<TD>
					<asp:Button id="ButtonDosageChange" runat="server" Text="Submit" 
                        onclick="ButtonDosageChange_Click"></asp:Button>&nbsp;
					<asp:Button id="ButtonCancelDosageChange" runat="server" Text="Cancel" CausesValidation="False" onclick="ButtonCancelDosageChange_Click"></asp:Button>
                    &nbsp;<asp:Button ID="ButtonRemoveRequest" runat="server" Text="Remove Request" 
                    BackColor="Red" onclick="ButtonRemoveRequest_Click" Visible="False" CausesValidation="false" />
                    <asp:Label ID="LabelAEErrorDoseChange" runat="server" ForeColor="Red" 
                        Visible="False"></asp:Label>
                </TD>
			</TR>
		</TABLE>
		</div>
		</div>
		<script language="javascript">
		    //**********************************************************************************************************************
		    function validateReceiveYes(sender, e) {
		        if (e.Value == "Yes") {
		            if ($get('<%= dropDoseReceived.ClientID  %>').value != "0") {
		                e.IsValid = true;
		            }
		            else {
		                e.IsValid = false;
		            }
		        }
		        else {
		            e.IsValid = true;
		        }
		    }

		    /****************Validate the AE Date***************************************/
		    function validateDateLearned(sender, e) {

		        var dateStr = $get('<%= dropMonth.ClientID  %>').value + "/" +
                        $get('<%= dropDay.ClientID  %>').value + "/" +
                        $get('<%= dropYear.ClientID  %>').value;

		        var valid = true;
		        //       var month = parseInt(dateStr.substring(0, 2),10);
		        //       var day = parseInt(dateStr.substring(2, 4), 10);
		        //       var year = parseInt(dateStr.substring(4, 6), 10);

		        var month = $get('<%= dropMonth.ClientID  %>').value;
		        var day = $get('<%= dropDay.ClientID  %>').value;

		        var year = $("*[id$='dropYear']").find(':selected').val();

		        if ((month < 1) || (month > 12)) {
		            valid = false;
		        }
		        else if ((day < 1) || (day > 31)) {
		            valid = false;
		        }
		        else if (((month == 4) || (month == 6) || (month == 9) || (month == 11)) && (day > 30)) {
		            valid = false;
		        }
		        else if ((month == 2) && (((year % 400) == 0) || ((year % 4) == 0)) && ((year % 100) != 0) && (day > 29)) {
		            valid = false;
		        }
		        else if ((month == 2) && ((year % 100) == 0) && (day > 29)) {
		            valid = false;
		        }

		        if (valid) {
		            e.IsValid = true;
		        }
		        else {
		            e.IsValid = false;
		        }
		    }
</script>
</asp:Content>

