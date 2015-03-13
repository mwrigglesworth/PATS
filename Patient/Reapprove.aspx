<%@ Page Language="C#" MasterPageFile="~/Patient/GIPAPPatient.master" AutoEventWireup="true" CodeFile="Reapprove.aspx.cs" Inherits="Patient_Reapprove" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderControlPanel" Runat="Server">
    <div class="ControlPanelDivHeader">
                    Reapprove Patient</div>
                <div class="ControlPanelHeaderRight">
                </div>
<div class="FormDiv">
<TABLE id="Table11" cellSpacing="1" cellPadding="1" width="585" align="left" border="0">
		<TR>
			<TD><asp:validationsummary id="ValidationSummary1" runat="server" HeaderText="You are missing the following fields:"
		ShowMessageBox="True" CssClass="AlertDiv" ForeColor=""></asp:validationsummary></TD>
		</TR>
		<TR>
			<TD>
                <asp:Panel ID="PanelObsolete" runat="server" Visible="False">
                <div class="AlertDiv">
                The current status of this 
patient renders the following request obsolete.&nbsp; An action may have been 
executed for this patient since this request was submitted.  Click below to remove the request.
                </div>
                </asp:Panel>
            </TD>
		</TR>
	</TABLE>
	<div style="clear:both;">
	<TABLE id="Table4" cellSpacing="1" cellPadding="1" width="600" align="center" border="0">
		<TR>
			<TD vAlign="top" style="width: 340px">
				<TABLE id="Table2" cellSpacing="1" cellPadding="1" width="100%" align="center" bgColor="#ffffff"
					border="0">
					<TR>
						<TD width="350">Approval Period Start Date:
							<asp:Label id="LabelStartDate" runat="server"></asp:Label>&nbsp;
							<asp:LinkButton id="lbEditStart" runat="server" CausesValidation="False" onclick="lbEditStart_Click">Edit Date</asp:LinkButton></TD>
					</TR>
					<TR>
						<TD align="left" width="350" height="22">Approval Period End Date:
							<asp:Label id="LabelEndDate" runat="server"></asp:Label>&nbsp;
						</TD>
					</TR>
					<TR>
						<TD width="350" height="24"></TD>
					</TR>
					<TR>
						<TD></TD>
					</TR>
					<TR>
						<TD><asp:UpdatePanel ID="UpdatePanel3" runat="server">
<ContentTemplate>
<asp:UpdateProgress ID="UpdateProgress3" runat="server" DisplayAfter="0" DynamicLayout="true">
        <ProgressTemplate><img alt="..loading" src="../Images/loading.gif" />            
            </ProgressTemplate>
             </asp:UpdateProgress>
							<asp:Panel id="PanelReapproveDose" runat="server">Dosage*: 
<asp:DropDownList id="dropDosage" runat="server" Width="100px" Enabled="False" 
                                    onselectedindexchanged="dropDosage_SelectedIndexChanged">
									<asp:ListItem Value="0">Select One</asp:ListItem>
								</asp:DropDownList>
<asp:CompareValidator id="CompareValidator5" runat="server" ErrorMessage="Dosage" Operator="NotEqual"
									ValueToCompare="0" ControlToValidate="dropDosage">*</asp:CompareValidator><BR>
<asp:Label id="LabelReApprovalDosageMessage" ForeColor="Gray" runat="server" Font-Italic="True">*Dosages of 200mg and 260mg are pediatric only</asp:Label></asp:Panel>
                            <br />
                            <asp:Button ID="ButtonReapprovalDoseChange" runat="server" Text="Change Dosage" 
                                onclick="ButtonReapprovalDoseChange_Click" CausesValidation="false" />
                            <asp:Panel ID="PanelReapproveDoseChange" runat="server" Visible="False">
                                <asp:CheckBox ID="cbReapprovalAE" runat="server" AutoPostBack="True" 
                                    Text="Dosage change is considered to be AE related" 
                                    oncheckedchanged="cbReapprovalAE_CheckedChanged" />
                            <br />
                            <asp:Label ID="LabelreappDoseChange" runat="server" ForeColor="Red" 
                                    Text="Please enter a reason for the dosage change" Visible="False"></asp:Label>
							
                            <asp:Label ID="LabelPOWarningReapproval" runat="server" 
                            Text="PLEASE NOTE: THESE DETAILS WILL BE EMAILED IMMEDIATELY AS AN ADVERSE EVENT REPORT TO NOVARTIS" 
                            ForeColor="#943634" Visible="False"></asp:Label><br />
                            <asp:Label ID="LabelPhysWarning" runat="server" 
                            Text="PLEASE NOTE: Do not provide patient identifying information (such as name) as these details will be included in an adverse event report to Novartis" 
                            ForeColor="#943634" Visible="False"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtNotes" Enabled = "false"  ErrorMessage="Please provide further details">Required 
                            </asp:RequiredFieldValidator>
                            <br />
                            <asp:TextBox id="txtNotes" runat="server" Width="100%" TextMode="MultiLine" 
                                    Height="56px" Visible="False"></asp:TextBox>

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
    
         <asp:CompareValidator id="CompareValidator4" runat="server" ControlToValidate="dropDay" ValueToCompare="0"
				Operator="NotEqual" ErrorMessage="Day learned">*</asp:CompareValidator>
			<asp:CompareValidator id="CompareValidator6" runat="server" ControlToValidate="dropMonth" ValueToCompare="0"
				Operator="NotEqual" ErrorMessage="Month learned">*</asp:CompareValidator>
            <asp:CompareValidator id="CompareValidator8" runat="server" ControlToValidate="dropYear" ValueToCompare="0"
				Operator="NotEqual" ErrorMessage="Year learned">*</asp:CompareValidator>
           <asp:CustomValidator ID="CustomValidator10" runat="server" ErrorMessage="Please enter a valide Date when AE was learned" 
            ControlToValidate="dropYear" ClientValidationFunction="validateDateLearned"></asp:CustomValidator>
           
           </asp:Panel>   


                                   
                <asp:Label ID="LabelReapprovalDefinition" runat="server" Font-Italic="True" 
                    ForeColor="#943634" 
                    Text="&lt;u&gt;Definition of AE:&lt;/u&gt; “Any untoward medical occurrence in a patient administered a pharmaceutical product that does not necessarily have a causal relationship with the treatment.  An AE can therefore be any unfavorable and unintended sign (including an abnormal laboratory finding), symptom, or disease temporally associated with the use of the medicinal product, whether or not related to the medicinal products.  In addition, all reports of the following also should be reported even if no AE has been reported: Drug-drug interaction, Drug exposure during pregnancy (via the mother or father with or without outcome), Drug use during lactation or breast-feeding, Lack of efficacy, Overdose, Drug abuse and misuse, Drug maladministration or accidental exposure, Dispensing errors / Medication errors, and Withdrawal or rebound symptoms.”" 
                    Visible="False"></asp:Label>
                            </asp:Panel>
                            <asp:Panel ID="PanelTablet" runat="server" Visible="false"><br /><br />
                            Tablet Strength: 
                        <asp:DropDownList ID="dropTablet" runat="server">
                        </asp:DropDownList>
                            </asp:Panel>
                            </ContentTemplate></asp:UpdatePanel></TD>
					</TR>
                    <tr><td style="height:25px;"></td></tr>
					<TR>
						<TD>
							<asp:Panel id="PanelConsent" runat="server">To your knowledge has 
            the patient agreement (consent form) been signed?&nbsp; 
<asp:RadioButtonList id="rbConsent" runat="server" RepeatDirection="Horizontal">
									<asp:ListItem Value="No" Selected="True">No</asp:ListItem>
									<asp:ListItem Value="Yes">Yes</asp:ListItem>
								</asp:RadioButtonList></asp:Panel></TD>
					</TR>
					<TR>
						<TD>Do you&nbsp;recommend that the patient&nbsp;continue treatment?&nbsp;
							<asp:RequiredFieldValidator id="RequiredFieldValidator1" runat="server" ErrorMessage="Do you recommend patient restart treatment"
								ControlToValidate="rblstRestart">*</asp:RequiredFieldValidator></TD>
					</TR>
					<TR>
						<TD>
							<asp:RadioButtonList id="rblstRestart" runat="server" RepeatDirection="Horizontal">
								<asp:ListItem Value="No">No</asp:ListItem>
								<asp:ListItem Value="Yes" Selected="True">Yes</asp:ListItem>
							</asp:RadioButtonList></TD>
					</TR>
				</TABLE>
				<TABLE id="Table5" cellSpacing="1" cellPadding="1" width="100%" border="0">
					<TR>
						<TD>
							<asp:Panel id="PanelAdvance" runat="server">Do you wish to log 
            this as an advanced entry? 
<asp:RadioButtonList id="rblstAdvance" runat="server" RepeatDirection="Horizontal">
									<asp:ListItem Value="No">No</asp:ListItem>
									<asp:ListItem Value="Yes">Yes</asp:ListItem>
								</asp:RadioButtonList>
<asp:RequiredFieldValidator id="RequiredFieldValidator9" runat="server" ErrorMessage="Do you wish to log this as an advanced entry?"
									ControlToValidate="rblstAdvance">*</asp:RequiredFieldValidator></asp:Panel></TD>
					</TR>
					<TR>
						<TD>
							<asp:Panel id="PanelReceive" runat="server">Was this requested by the physician?<br />
                                <asp:RadioButtonList ID="rblstPhysReq" runat="server" 
                                    RepeatDirection="Horizontal">
                                    <asp:ListItem>No</asp:ListItem>
                                    <asp:ListItem Selected="True">Yes</asp:ListItem>
                                </asp:RadioButtonList>
                                
                                <br /><br />This re-evaluation information was 
                                received via:<BR>
<asp:DropDownList id="dropReceived" runat="server">
									<asp:ListItem Value="0">Select One</asp:ListItem>
									<asp:ListItem Value="Fax from physician">Fax from physician</asp:ListItem>
									<asp:ListItem Value="Email from physician">Email from physician</asp:ListItem>
									<asp:ListItem Value="Verbal verification from physician">Verbal verification from physician</asp:ListItem>
									<asp:ListItem Value="Written notification from physician">Written notification from physician</asp:ListItem>
								</asp:DropDownList>
<asp:CustomValidator ID="CustomValidator3" runat="server" 
                                    ClientValidationFunction="validateReceiveYes" ControlToValidate="rblstPhysReq" 
                                    ErrorMessage="Information Received By">*</asp:CustomValidator></asp:Panel></TD>
					</TR>
					<TR>
						<TD>
                            <asp:Panel ID="PanelNotPickedUp" runat="server" Visible="False">
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
                        </TD>
					</TR>
					<TR>
						<TD></TD>
					</TR>
					<TR>
						<TD>
							<asp:Button id="ButtonReApprove" runat="server" Width="75px" Text="Submit" 
                                onclick="ButtonReApprove_Click" style="height: 26px"></asp:Button>&nbsp;
							<asp:Button id="ButtonCancel" runat="server" Width="75px" Text="Cancel" CausesValidation="False" onclick="ButtonCancel_Click"></asp:Button>&nbsp;
							<asp:Button ID="ButtonRemoveRequest" runat="server" Text="Remove Request" 
                    BackColor="Red" onclick="ButtonRemoveRequest_Click" Visible="False" CausesValidation="false" /></TD>
					</TR>
				</TABLE>
			</TD>
			<TD vAlign="top">
				<asp:Calendar id="CalendarStartDate" runat="server" BorderColor="#404040" BackColor="White" onselectionchanged="CalendarStartDate_SelectionChanged"></asp:Calendar>
				
				</TD>
		</TR>
	</TABLE>
	</div>
		</div>
		<script language="javascript">
		    //**********************************************************************************************************************

		    function validateReceiveYes(sender, e) {
		        if (e.Value == "Yes") {
		            if ($get('<%= dropReceived.ClientID  %>').value != "0") {
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
