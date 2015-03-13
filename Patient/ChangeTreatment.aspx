<%@ Page Language="C#" MasterPageFile="~/Patient/GIPAPPatient.master" AutoEventWireup="true" CodeFile="ChangeTreatment.aspx.cs" Inherits="Patient_ChangeTreatment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderControlPanel" Runat="Server">
    <div class="ControlPanelDivHeader">
                    Change Treatment to Tasigna</div>
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
<TABLE id="Table21" 
		cellSpacing="2" cellPadding="2" width="600" border="0">
		<TR>
			<TD width="176">Has patient previously taken generic imatinib?: 
                <FONT color="red">*</FONT></TD>
			<TD width="33">
				<asp:RequiredFieldValidator id="RequiredFieldValidator21" 
            runat="server" ErrorMessage="You must Indicate if Patient has previoulsy taken Imatinib"
					ControlToValidate="rblstTasignaImatinib">*</asp:RequiredFieldValidator>
                    
                    </TD>
			<TD>
				<asp:RadioButtonList id="rblstTasignaImatinib" runat="server" 
            ToolTip="Indicate if the patient has taken Glivec/imatinib in the past"
					RepeatDirection="Horizontal" CellPadding="0" CellSpacing="0">
					<asp:ListItem Value="0">No</asp:ListItem>
					<asp:ListItem Value="1">Yes</asp:ListItem>
				</asp:RadioButtonList></TD>
		</TR>
		<TR>
			<TD width="140">Is the patient intolerant to Glivec/Imatinib?</TD>
			<TD width="30">
				<asp:RequiredFieldValidator ID="RequiredFieldValidator22" 
            runat="server" ControlToValidate="rblstGlivecIntolerant" 
            ErrorMessage="Is the patient intolerant to Glivec?">*</asp:RequiredFieldValidator>
            </TD>
			<TD>
				<asp:RadioButtonList id="rblstGlivecIntolerant" runat="server" 
                    RepeatDirection="Horizontal" CellPadding="0"
					CellSpacing="0" onselectedindexchanged="rblstGlivecIntolerant_SelectedIndexChanged" 
                    AutoPostBack="True">
					<asp:ListItem Value="0">No</asp:ListItem>
					<asp:ListItem Value="1">Yes</asp:ListItem>
				</asp:RadioButtonList></TD>
		</TR>
		<TR>
			<TD>Is the patient resistant to Glivec/Imatinib?</TD>
			<TD>
				<asp:RequiredFieldValidator ID="RequiredFieldValidator23" 
            runat="server" ControlToValidate="rblstGlivecResistant" 
            ErrorMessage="Is the patient resistant to Glivec?">*</asp:RequiredFieldValidator>
            </TD>
			<TD>
				<asp:RadioButtonList id="rblstGlivecResistant" runat="server" 
                    RepeatDirection="Horizontal" CellPadding="0"
					CellSpacing="0" onselectedindexchanged="rblstGlivecResistant_SelectedIndexChanged" 
                    AutoPostBack="True">
					<asp:ListItem Value="0">No</asp:ListItem>
					<asp:ListItem Value="1">Yes</asp:ListItem>
				</asp:RadioButtonList></TD>
		</TR>
		<tr><td colspan="3">
            <asp:Panel ID="PanelAE" runat="server" Visible="false">
            <asp:Label ID="LabelPOWarning" runat="server" 
                            Text="PLEASE NOTE: THESE DETAILS WILL BE EMAILED IMMEDIATELY AS AN ADVERSE EVENT REPORT TO NOVARTIS" 
                            ForeColor="#943634" Visible="False"></asp:Label><br />
                            <asp:Label ID="LabelPhysWarning" runat="server" 
                            Text="PLEASE NOTE: Do not provide patient identifying information (such as name) as these details will be included in an adverse event report to Novartis" 
                            ForeColor="#943634" Visible="False"></asp:Label>
            Please provide further details:
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                    ErrorMessage="Please provide further details" ControlToValidate="txtNotes" 
                    Text="*" Enabled="false"></asp:RequiredFieldValidator><br />
            <asp:TextBox ID="txtNotes" runat="server" Height="55px" TextMode="MultiLine" 
                Width="580px"></asp:TextBox>

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
    
        <%--<asp:CustomValidator ID="DateLearnedValidateDay" runat="server"
            ControlToValidate="dropDay" ClientValidationFunction="validateDateLearned">*</asp:CustomValidator>
            <asp:CustomValidator ID="CustomValidator1" runat="server"
            ControlToValidate="dropMonth" ClientValidationFunction="validateDateLearned"></asp:CustomValidator>--%>
            <asp:CompareValidator id="CompareValidator3" runat="server" ControlToValidate="dropDay" ValueToCompare="0"
				Operator="NotEqual" ErrorMessage="Day learned">*</asp:CompareValidator>
			<asp:CompareValidator id="CompareValidator1" runat="server" ControlToValidate="dropMonth" ValueToCompare="0"
				Operator="NotEqual" ErrorMessage="Month learned">*</asp:CompareValidator>
            <asp:CompareValidator id="CompareValidator2" runat="server" ControlToValidate="dropYear" ValueToCompare="0"
				Operator="NotEqual" ErrorMessage="Year learned">*</asp:CompareValidator>
           <asp:CustomValidator ID="CustomValidator2" runat="server" ErrorMessage="Please enter a valide Date when AE was learned" 
            ControlToValidate="dropYear" ClientValidationFunction="validateDateLearned"></asp:CustomValidator><br />
           </asp:Panel>   

                
                            <asp:Label ID="LabelDefinition" runat="server" Font-Italic="True" 
                    ForeColor="#943634" 
                    Text="&lt;u&gt;Definition of AE:&lt;/u&gt; “Any untoward medical occurrence in a patient administered a pharmaceutical product that does not necessarily have a causal relationship with the treatment.  An AE can therefore be any unfavorable and unintended sign (including an abnormal laboratory finding), symptom, or disease temporally associated with the use of the medicinal product, whether or not related to the medicinal products.  In addition, all reports of the following also should be reported even if no AE has been reported: Drug-drug interaction, Drug exposure during pregnancy (via the mother or father with or without outcome), Drug use during lactation or breast-feeding, Lack of efficacy, Overdose, Drug abuse and misuse, Drug maladministration or accidental exposure, Dispensing errors / Medication errors, and Withdrawal or rebound symptoms.”" 
                    Visible="False"></asp:Label>
            </asp:Panel>
            <asp:Panel id="PanelReceive" runat="server"><br /><br />Was this requested by the physician?<br />
                                <asp:RadioButtonList ID="rblstPhysReq" runat="server" 
                                    RepeatDirection="Horizontal">
                                    <asp:ListItem>No</asp:ListItem>
                                    <asp:ListItem Selected="True">Yes</asp:ListItem>
                                </asp:RadioButtonList>
                               
                                <br />This information was 
                                received via:<BR>
<asp:DropDownList id="dropReceived" runat="server">
									<asp:ListItem Value="0">Select One</asp:ListItem>
									<asp:ListItem Value="Fax from physician">Fax from physician</asp:ListItem>
									<asp:ListItem Value="Email from physician">Email from physician</asp:ListItem>
									<asp:ListItem Value="Verbal verification from physician">Verbal verification from physician</asp:ListItem>
									<asp:ListItem Value="Written notification from physician">Written notification from physician</asp:ListItem>
								</asp:DropDownList>
<asp:CustomValidator ID="CustomValidator5" runat="server" 
                                    ClientValidationFunction="validateReceiveYes" ControlToValidate="rblstPhysReq" 
                                    ErrorMessage="Information Received By">*</asp:CustomValidator><br /><br /></asp:Panel>
            </td></tr>
		<TR>
			<TD>Is the patient currently receiving dasatinib?</TD>
			<TD>
				<asp:RequiredFieldValidator id="RequiredFieldValidator24" runat="server" ErrorMessage="Is the patient receiving Dasatinib"
					ControlToValidate="rblstDasatinib">*</asp:RequiredFieldValidator></TD>
			<TD>
				<asp:RadioButtonList id="rblstDasatinib" runat="server" RepeatDirection="Horizontal" CellPadding="0"
					CellSpacing="0">
					<asp:ListItem Value="0">No</asp:ListItem>
					<asp:ListItem Value="1">Yes</asp:ListItem>
				</asp:RadioButtonList></TD>
		</TR>
		<TR>
			<TD>Is the patient intolerant to dasatinib?</TD>
			<TD>
				<asp:CustomValidator id="CustomValidator19" runat="server" ErrorMessage="Dasatinib Intolerant" ControlToValidate="rblstDasatinib"
					ClientValidationFunction="validateDasatinibIntolerant">*</asp:CustomValidator></TD>
			<TD>
				<asp:RadioButtonList id="rblstDasatinibIntolerant" runat="server" RepeatDirection="Horizontal" CellPadding="0"
					CellSpacing="0">
					<asp:ListItem Value="0">No</asp:ListItem>
					<asp:ListItem Value="1">Yes</asp:ListItem>
				</asp:RadioButtonList></TD>
		</TR>
		<TR>
			<TD>Is the patient resistant to dasatinib?</TD>
			<TD>
				<asp:CustomValidator id="CustomValidator20" runat="server" ErrorMessage="Dasatinib Resistant" ControlToValidate="rblstDasatinib"
					ClientValidationFunction="validateDasatinibResistant">*</asp:CustomValidator></TD>
			<TD>
				<asp:RadioButtonList id="rblstDasatinibResistant" runat="server" RepeatDirection="Horizontal" CellPadding="0"
					CellSpacing="0">
					<asp:ListItem Value="0">No</asp:ListItem>
					<asp:ListItem Value="1">Yes</asp:ListItem>
				</asp:RadioButtonList></TD>
		</TR>
		<TR>
			<TD width="156">Has patient previously taken nilotinib/Tasigna®?</TD>
			<TD width="20">
				<asp:RequiredFieldValidator id="RequiredFieldValidator25" runat="server" ErrorMessage="Has the patient previously taken tasigna?"
					ControlToValidate="rblstTasigna">*</asp:RequiredFieldValidator></TD>
			<TD>
				<asp:RadioButtonList id="rblstTasigna" runat="server" RepeatDirection="Horizontal" CellPadding="0" CellSpacing="0">
					<asp:ListItem Value="0">No</asp:ListItem>
					<asp:ListItem Value="1">Yes</asp:ListItem>
				</asp:RadioButtonList></TD>
		</TR>
		<TR>
			<TD width="156">If yes, what was the starting date?</TD>
			<TD width="20">
				<asp:CustomValidator id="CustomValidator21" runat="server" ErrorMessage="Tasigna Start Day" ControlToValidate="dropTasignaStartDay"
					ClientValidationFunction="validateTasignaDate">*</asp:CustomValidator>
				<asp:CustomValidator id="CustomValidator22" runat="server" ErrorMessage="Tasigna Start Month" ControlToValidate="dropTasignaStartMonth"
					ClientValidationFunction="validateTasignaDate">*</asp:CustomValidator>
				<asp:CustomValidator id="CustomValidator23" runat="server" ErrorMessage="Tasigna Start Year" ControlToValidate="dropTasignaStartYear"
					ClientValidationFunction="validateTasignaDate">*</asp:CustomValidator></TD>
			<TD>
				<asp:DropDownList id="dropTasignaStartDay" runat="server" Font-Names="Verdana">
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
				<asp:DropDownList id="dropTasignaStartMonth" runat="server" Font-Names="Verdana">
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
				<asp:DropDownList id="dropTasignaStartYear" runat="server" Font-Names="Verdana">
            <asp:ListItem Value="0">Year</asp:ListItem>
            </asp:DropDownList></TD>
		</TR>
		<TR>
			<TD width="156">If yes, what was the prescribed daily dose?</TD>
			<TD width="20">
				<asp:CustomValidator id="CustomValidator16" runat="server" ErrorMessage="Previous Tasigna Dosage" ControlToValidate="dropTasignaDose"
					ClientValidationFunction="validateTasignaDate">*</asp:CustomValidator></TD>
			<TD>
				<asp:DropDownList id="dropTasignaDose" runat="server" 
            Font-Names="Verdana">
					<asp:ListItem Value="0">Select a Dose</asp:ListItem>
					<asp:ListItem Value="400mg BID">400mg BID</asp:ListItem>
					<asp:ListItem Value="400mg QD">400mg QD</asp:ListItem>
					<asp:ListItem>300mg BID</asp:ListItem>
				</asp:DropDownList></TD>
		</TR>
		<TR>
			<TD width="156">Requested NOA Tasigna dose:</TD>
			<TD width="20">
				<asp:CompareValidator id="CompareValidator6" runat="server" 
            ErrorMessage="Requested TIPAP Dose" ControlToValidate="dropRequestedTasignaDose"
					ValueToCompare="0" Operator="NotEqual">*</asp:CompareValidator></TD>
			<TD>
				<asp:DropDownList id="dropRequestedTasignaDose" runat="server" 
            Font-Names="Verdana">
					<asp:ListItem Value="0">Select a Dose</asp:ListItem>
					<asp:ListItem Value="400mg BID">400mg BID</asp:ListItem>
					<asp:ListItem Value="400mg QD">400mg QD</asp:ListItem>
					<asp:ListItem>300mg BID</asp:ListItem>
				</asp:DropDownList></TD>
		</TR>
		<TR>
			<TD width="176">Has the patient applied to NOA Tasigna in the past?:<FONT color="red">*</FONT></TD>
			<TD width="33">
				<asp:requiredfieldvalidator id="Requiredfieldvalidator18" 
            runat="server" ErrorMessage="You must indicate if the applicant has applied for NOA Tasigna before."
					ControlToValidate="rblstNOATasigna">*</asp:requiredfieldvalidator></TD>
			<TD>
				<asp:RadioButtonList id="rblstNOATasigna" runat="server" ToolTip="Indicate whether or not the applicant has applied for GIPAP before"
					RepeatDirection="Horizontal" CellPadding="0" CellSpacing="0">
					<asp:ListItem Value="0">No</asp:ListItem>
					<asp:ListItem Value="1">Yes</asp:ListItem>
				</asp:RadioButtonList></TD>
		</TR>
		<TR>
			<TD width="176">
				<asp:Label id="Label1" runat="server">Has the NOA Tasigna 
                Patient Consent Form been signed?<font color="red">
						*</font></asp:Label></TD>
			<TD width="33">
				<asp:requiredfieldvalidator id="Requiredfieldvalidator19" 
            runat="server" ErrorMessage="You must indicate if the Patient Consent form has been collected."
					ControlToValidate="rblstTasignaPatientConsent">*</asp:requiredfieldvalidator></TD>
			<TD>
				<asp:RadioButtonList id="rblstTasignaPatientConsent" runat="server" 
            ToolTip="Indicate whether or not the applicant has applied for GIPAP before"
					RepeatDirection="Horizontal" CellPadding="0" CellSpacing="0">
					<asp:ListItem Value="0">No</asp:ListItem>
					<asp:ListItem Value="1">Yes</asp:ListItem>
				</asp:RadioButtonList></TD>
		</TR>
		<tr><td>Current CML Phase:</td><td>
            <asp:CompareValidator ID="CompareValidator7" runat="server" 
                ControlToValidate="dropCMLphase" ErrorMessage="CML Phase" 
                Operator="NotEqual" ValueToCompare="0">*</asp:CompareValidator>
            </td><td>
											<asp:dropdownlist id="dropCMLphase" runat="server" Width="138px">
												<asp:ListItem Value="0">Select CML Phase</asp:ListItem>
												<asp:ListItem Value="Accelerated">Accelerated</asp:ListItem>
												<asp:ListItem Value="Blast Crisis">Blast Crisis</asp:ListItem>
												<asp:ListItem Value="Chronic">Chronic</asp:ListItem>
												<asp:ListItem Value="Remission">Remission</asp:ListItem>
											</asp:dropdownlist>
											</td></tr>
		<tr><td>
            <asp:Button ID="ButtonSubmit" runat="server" onclick="ButtonSubmit_Click" 
                Text="Submit" />
&nbsp;
            <asp:Button ID="ButtonCancel" runat="server" CausesValidation="False" 
                Text="Cancel" />
            </td><td></td><td></td></tr>
		<tr><td></td><td></td><td></td></tr>
	</TABLE>
	</div>
</div>
<script language="javascript">
    //Function checks to see if patient has Insurance and if answer is Yes, validates the required dependent fields
    function validateTasignaDate(sender, e) {
        if ($get('<%= rblstTasigna.ClientID  %>' + '_1').checked) {
            if (e.Value == "0") {
                e.IsValid = false;
            }
            else {
                e.IsValid = true;
            }
        }
        else {
            e.IsValid = true;
        }
    }
    function validateDasatinibIntolerant(sender, e) {
        if (e.Value == "1") {
            if ($get('<%= rblstDasatinibIntolerant.ClientID  %>' + '_0').checked || $get('<%= rblstDasatinibIntolerant.ClientID  %>' + '_1').checked) {
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
    function validateDasatinibResistant(sender, e) {
        if (e.Value == "1") {
            if ($get('<%= rblstDasatinibResistant.ClientID  %>' + '_0').checked || $get('<%= rblstDasatinibResistant.ClientID  %>' + '_1').checked) {
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

    /*******************Validate the AE Date**************************/
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

