<%@ Page Language="C#" MasterPageFile="~/PATS.master" AutoEventWireup="true" CodeFile="GIPAPApplication.aspx.cs" Inherits="Application_GIPAPApplication" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style1
        {
            height: 69px;
        }
        .style2
        {
            width: 346px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<div style="width:100%; clear:both; text-align: left;">
<asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
<asp:UpdateProgress ID="UpdateProgress2" runat="server" DisplayAfter="0" DynamicLayout="true" AssociatedUpdatePanelID="UpdatePanel2">
        <ProgressTemplate><div style="width: 100%; background-color: White">
                        <img alt="..loading" src="../Images/loading.gif" /></div>            
            </ProgressTemplate>
             </asp:UpdateProgress>
<h1>Patient Application</h1>
<div style="width:630px; float:left;">
<table id="Table7" cellSpacing="1" cellPadding="1" width="600" align="left" border="0">
		<tr>
			<td><asp:validationsummary id="ValidationSummary1" runat="server" HeaderText="You are missing the following fields:"
		ShowMessageBox="True" CssClass="AlertDiv" ForeColor=""></asp:validationsummary></td>
		</tr>
		</table>
    <asp:Panel ID="PanelIntro" runat="server">
    <div class="FormTable" style="clear:both; width:600px;">
<table id="Table32" cellSpacing="1" cellPadding="1" width="600" border="0">
		<tr>
			<td>What country is the patient being treated in?<FONT color="red">*</FONT></td>
			<td>
                <asp:CompareValidator ID="CompareValidator5" runat="server" ErrorMessage="Applicant Country" ControlToValidate="cboCountry" Operator="NotEqual" ValueToCompare="0">*</asp:CompareValidator>
				</td>
			<td>
				<asp:DropDownList id="cboCountry" runat="server" width="225px" Font-Names="Verdana" 
					AutoPostBack="True" onselectedindexchanged="cboCountry_SelectedIndexChanged"></asp:DropDownList>
            </td>
		</tr>
		<tr>
			<td>What is the applicant's diagnosis?:<FONT color="red">*</FONT></td>
			<td>
                <asp:CompareValidator ID="CompareValidator7" runat="server" ErrorMessage="Diagnosis" Text="*" ControlToValidate="cboDiagnosis" Operator="NotEqual" ValueToCompare="0">*</asp:CompareValidator>
				</td>
			<td>
				<asp:DropDownList id="cboDiagnosis" runat="server" width="225px" 
                    Font-Names="Verdana" 
					Enabled="False" >
					<asp:ListItem Value="0">Select a Disease</asp:ListItem>
					<asp:ListItem Value="CML">CML</asp:ListItem>
					<asp:ListItem Value="GIST">GIST</asp:ListItem>
				</asp:DropDownList></td>
		</tr>
		<tr>
			<td>Treating Physician:&nbsp;<FONT color="red">*</FONT>
			</td>
			<td>
				<asp:CompareValidator id="CompareValidator2" runat="server" ErrorMessage="Must Select Physician" ControlToValidate="dropPhysician"
					ValueToCompare="0" Operator="NotEqual">*</asp:CompareValidator></td>
			<td>
				<asp:DropDownList id="dropPhysician" runat="server" width="225px" 
                    Font-Names="Verdana"  Enabled="False" 
                    onselectedindexchanged="dropPhysician_SelectedIndexChanged" 
                    AutoPostBack="True"></asp:DropDownList></td>
		</tr>
            <tr><td>
                <asp:Label ID="LabelTreatment" runat="server" Text="Treatment:" Visible="False"></asp:Label>
                </td><td></td><td>
                <asp:RadioButtonList ID="rblstTreatment" runat="server" Visible="False">
                    <asp:ListItem Selected="True">Glivec</asp:ListItem>
                    <asp:ListItem>Tasigna</asp:ListItem>
                </asp:RadioButtonList>
                </td></tr>
        <tr><td colspan="3">
            <asp:Label ID="LabelTasignaCML" runat="server" ForeColor="Red" 
                Text="Tasigna is available for CML patients only" Visible="False"></asp:Label>
            </td></tr>
            <tr><td colspan="3">
                <asp:TextBox ID="txtPhysicianFirstName" runat="server" Visible="false"></asp:TextBox>
                <asp:TextBox ID="txtPhysicianLastName" runat="server" Visible="false"></asp:TextBox></td></tr>
		<tr>
			<td align="center" colSpan="3">
				<asp:Button id="ButtonCancel" runat="server" Font-Names="Verdana" Text="Cancel"
					CausesValidation="False" Width="120px" onclick="ButtonCancel_Click"></asp:Button>&nbsp;
				<asp:Button id="ButtonContinue" runat="server" Font-Names="Verdana" Text="Next Page >>"
					Width="120px" onclick="ButtonContinue_Click"></asp:Button></td>
		</tr>
	</table>
	</div>
    </asp:Panel>
    <asp:Panel ID="PanelPatient" runat="server" Visible="false">
    <div class="FormTable" style="clear:both; width:600px;">
    <table id="Table1" cellSpacing="2" cellPadding="2" width="600" border="0">
		<tr>
			<td align="center" bgColor="#cccccc" colSpan="3">
                <font color="steelblue"><strong>
                <asp:Label ID="LabelFormHeader" runat="server"></asp:Label></strong></font></td>
		</tr>
		<tr>
			<td>First Name:<FONT color="red">*</FONT></td>
			<td>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtFirstName"
                    ErrorMessage="First Name">*</asp:RequiredFieldValidator></td>
			<td>
				<asp:TextBox id="txtFirstName" runat="server" width="300px" Font-Names="Verdana" 
					ToolTip="Enter in the applicants first/given name"  CausesValidation="True"></asp:TextBox></td>
		</tr>
		<tr>
			<td>Surname/Last Name:<FONT color="red">*</FONT></td>
			<td>
				<asp:requiredfieldvalidator id="rfvalLastName" runat="server" ErrorMessage="You must enter the applicant's Last Name/Surname."
					ControlToValidate="txtLastName">*</asp:requiredfieldvalidator></td>
			<td>
				<asp:TextBox id="txtLastName" runat="server" Font-Names="Verdana"  Width="301px"
					ToolTip="Enter in the applicants surname/last name" ></asp:TextBox></td>
		</tr>
        <tr id="thai" runat="server">
			<td>Thai Characters Name:</td>
			<td>
				</td>
			<td>
				<asp:TextBox id="txtThaiName" runat="server" Font-Names="Verdana"  Width="301px"
					ToolTip="Enter in the Thai Characters Name"></asp:TextBox></td>
		</tr>

		<tr>
			<td>Sex:<FONT color="red">*</FONT></td>
			<td>
				<asp:requiredfieldvalidator id="rfValGender" runat="server" ErrorMessage="You must select the applicant's Gender."
					ControlToValidate="rbGender">*</asp:requiredfieldvalidator></td>
			<td>
				<asp:RadioButtonList id="rbGender" runat="server" ToolTip="Select the applicants gender"
					RepeatDirection="Horizontal" CellPadding="0" CellSpacing="0">
					<asp:ListItem Value="M">Male</asp:ListItem>
					<asp:ListItem Value="F">Female</asp:ListItem>
				</asp:RadioButtonList></td>
		</tr>
		<tr>
			<td>Date Of Birth:<FONT color="red">*</FONT></td>
			<td>
                <asp:CompareValidator ID="CompareValidator8" runat="server" ErrorMessage="Birth Day" Text="*" ControlToValidate="cboBirthDay" Operator="NotEqual" ValueToCompare="0"></asp:CompareValidator>
                <asp:CompareValidator ID="CompareValidator9" runat="server" ErrorMessage="Birth Month" Text="*" ControlToValidate="cboBirthMonth" Operator="NotEqual" ValueToCompare="0"></asp:CompareValidator>
                <asp:CompareValidator ID="CompareValidator10" runat="server" ErrorMessage="Birth Year" Text="*" ControlToValidate="cboBirthYear" Operator="NotEqual" ValueToCompare="0"></asp:CompareValidator>
				</td>
			<td>
				<asp:DropDownList id="cboBirthDay" runat="server" Font-Names="Verdana" >
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
				<asp:DropDownList id="cboBirthMonth" runat="server" Font-Names="Verdana" >
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
				<asp:DropDownList id="cboBirthYear" runat="server" Font-Names="Verdana" ></asp:DropDownList></td>
		</tr>
		<tr>
			<td>Street Address 1:<FONT color="red">*</FONT></td>
			<td>
				<asp:requiredfieldvalidator id="rfvalAddress1" runat="server" ErrorMessage="You must enter the applicant's Street Address."
					ControlToValidate="txtStreet1">*</asp:requiredfieldvalidator></td>
			<td>
				<asp:TextBox id="txtStreet1" runat="server" width="300px" Font-Names="Verdana" 
					ToolTip="Enter the applicants street address" ></asp:TextBox></td>
		</tr>
		<tr>
			<td>Street Address 2:</td>
			<td></td>
			<td>
				<asp:TextBox id="txtStreet2" runat="server" width="300px" Font-Names="Verdana" 
					ToolTip="Use this field for additional address information" ></asp:TextBox></td>
		</tr>
		<tr>
			<td>City:<FONT color="red">*</FONT></td>
			<td>
				<asp:requiredfieldvalidator id="rfvalApplicantCity" runat="server" ErrorMessage="You must enter the applicant's City."
					ControlToValidate="txtCity">*</asp:requiredfieldvalidator></td>
			<td>
				<asp:TextBox id="txtCity" runat="server" width="300px" Font-Names="Verdana" 
					ToolTip="Enter in the applicants city" ></asp:TextBox></td>
		</tr>
		<tr>
			<td>State/Province:</td>
			<td></td>
			<td>
				<asp:TextBox id="txtStateProvince" runat="server" width="300px" Font-Names="Verdana" 
					ToolTip="Enter in the applicants State/Province if applicable" ></asp:TextBox></td>
		</tr>
		<tr>
			<td>Postal Code:</td>
			<td></td>
			<td>
				<asp:TextBox id="txtPostalCode" runat="server" width="300px" Font-Names="Verdana" 
					ToolTip="Enter in the applicants postal code if applicable" ></asp:TextBox></td>
		</tr>
		<tr>
			<td>Phone:<FONT color="red">*</FONT></td>
			<td>
				<asp:customvalidator id="cusValApplicantContact" runat="server" ErrorMessage="You must enter an applicant's phone number, mobile number, fax number OR email address."
					ClientValidationFunction="validateApplicantContact">*</asp:customvalidator></td>
			<td>
				<asp:TextBox id="txtPhone" runat="server" width="300px" Font-Names="Verdana" 
					ToolTip="Enter in at least a phone number, fax number or email address" ></asp:TextBox></td>
		</tr>
		<tr>
			<td>Fax:</td>
			<td></td>
			<td>
				<asp:TextBox id="txtFax" runat="server" width="300px" Font-Names="Verdana" 
					ToolTip="Enter in at least a phone number, fax number or email address" ></asp:TextBox></td>
		</tr>
		<tr>
			<td>Mobile:</td>
			<td></td>
			<td>
				<asp:TextBox id="txtMobile" runat="server" width="300px" Font-Names="Verdana" 
					ToolTip="Enter in at least a phone number, fax number or email address" ></asp:TextBox></td>
		</tr>
		<tr>
			<td>Email:</td>
			<td>
				<asp:regularexpressionvalidator id="RegularExpressionValidator1" runat="server" ErrorMessage="Invalid applicant Email"
					ControlToValidate="txtEmail" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:regularexpressionvalidator></td>
			<td>
				<asp:TextBox id="txtEmail" runat="server" width="300px" Font-Names="Verdana" 
					ToolTip="Enter in at least a phone number, fax number or email address" ></asp:TextBox></td>
		</tr>
		<tr>
			<td bgColor="#cccccc" colSpan="3"><FONT color="steelblue">
					<P align="center"><FONT color="#4682b4"><FONT face="Verdana"><STRONG>Caregiver 
									Information</STRONG><br />
								Include contact information for a person other than the patient or physician 
								who can be reached, as necessary, when the patient cannot be reached. </FONT>
						</FONT>
					</P>
				</FONT>
			</td>
		</tr>
		<tr>
			<td>Contact First Name:</td>
			<td></td>
			<td>
				<asp:TextBox id="txtContactFirstName" runat="server" width="300px" Font-Names="Verdana" 
					ToolTip="Enter in the alternate contacts first/given name" ></asp:TextBox></td>
		</tr>
		<tr>
			<td>Contact Last Name:</td>
			<td></td>
			<td>
				<asp:TextBox id="txtContactLastName" runat="server" width="300px" Font-Names="Verdana" 
					ToolTip="Enter in the alternate contacts last name/surname" ></asp:TextBox></td>
		</tr>
		<tr>
			<td>Street Address 1:</td>
			<td></td>
			<td>
				<asp:TextBox id="txtContactStreet1" runat="server" width="300px" Font-Names="Verdana" 
					ToolTip="Enter in the alternate contacts street address" ></asp:TextBox></td>
		</tr>
		<tr>
			<td>Street Address 2:</td>
			<td></td>
			<td>
				<asp:TextBox id="txtContactStreet2" runat="server" width="300px" Font-Names="Verdana" 
					ToolTip="Enter in additional information about the contact address" ></asp:TextBox></td>
		</tr>
		<tr>
			<td>City:</td>
			<td></td>
			<td>
				<asp:TextBox id="txtContactCity" runat="server" width="300px" Font-Names="Verdana" 
					ToolTip="Enter in the alternate contacts city" ></asp:TextBox></td>
		</tr>
		<tr>
			<td>State/Province:</td>
			<td></td>
			<td>
				<asp:TextBox id="txtContactStateProvince" runat="server" width="300px" Font-Names="Verdana" 
					ToolTip="Enter in the alternate contacts state/province if applicaqble" ></asp:TextBox></td>
		</tr>
		<tr>
			<td>Postal Code:</td>
			<td></td>
			<td>
				<asp:TextBox id="txtContactPostalCode" runat="server" width="300px" Font-Names="Verdana" 
					ToolTip="Enter in the alternate contacts postal code if applicable" ></asp:TextBox></td>
		</tr>
		<tr>
			<td>Country:</td>
			<td>
				<asp:CustomValidator id="cusValCareCountry" runat="server" ErrorMessage="You must select the alternate contacts country."
					ControlToValidate="cboContactCountry" ClientValidationFunction="validateCaregiverCountry">*</asp:CustomValidator></td>
			<td>
				<asp:DropDownList id="cboContactCountry" runat="server" width="300px" Font-Names="Verdana" ></asp:DropDownList></td>
		</tr>
		<tr>
			<td>Phone:</td>
			<td>
				<asp:customvalidator id="cusValCaregiverContact" runat="server" ErrorMessage="You must enter the alternate contacts phone number, fax number OR email address."
					ClientValidationFunction="validateCaregiverContact">*</asp:customvalidator></td>
			<td>
				<asp:TextBox id="txtContactPhone" runat="server" width="300px" Font-Names="Verdana" 
					ToolTip="Enter in at least a phone number, fax number or email address" ></asp:TextBox></td>
		</tr>
		<tr>
			<td>Fax:</td>
			<td></td>
			<td>
				<asp:TextBox id="txtContactFax" runat="server" width="300px" Font-Names="Verdana" 
					ToolTip="Enter in at least a phone number, fax number or email address" ></asp:TextBox></td>
		</tr>
		<tr>
			<td>Mobile:</td>
			<td></td>
			<td>
				<asp:TextBox id="txtContactMobile" runat="server" width="300px" Font-Names="Verdana" 
					ToolTip="Enter in at least a phone number, fax number or email address" ></asp:TextBox></td>
		</tr>
		<tr>
			<td>Email:</td>
			<td>
				<asp:regularexpressionvalidator id="RegularExpressionValidator2" runat="server" ErrorMessage="Invalid alternate contact email"
					ControlToValidate="txtContactEmail" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:regularexpressionvalidator></td>
			<td>
				<asp:TextBox id="txtContactEmail" runat="server" width="300px" Font-Names="Verdana" 
					ToolTip="Enter in at least a phone number, fax number or email address" ></asp:TextBox></td>
		</tr>
		<tr>
			<td>Relationship To Applicant:</td>
			<td>
				<asp:CustomValidator id="cusValRelationship" runat="server" ErrorMessage="You must select the contact's relationship to the patient"
					ControlToValidate="cboRelationship" ClientValidationFunction="validateCaregiverRelationship">*</asp:CustomValidator></td>
			<td>
				<asp:DropDownList id="cboRelationship" runat="server" width="300px" Font-Names="Verdana" >
					<asp:ListItem Value="0">Select One</asp:ListItem>
					<asp:ListItem Value="Mother">Mother</asp:ListItem>
					<asp:ListItem Value="Father">Father</asp:ListItem>
					<asp:ListItem Value="Sister">Sister</asp:ListItem>
					<asp:ListItem Value="Brother">Brother</asp:ListItem>
					<asp:ListItem Value="Son">Son</asp:ListItem>
					<asp:ListItem Value="Daughter">Daughter</asp:ListItem>
					<asp:ListItem Value="Other Relative">Other Relative</asp:ListItem>
					<asp:ListItem Value="Neighbor">Neighbor</asp:ListItem>
					<asp:ListItem Value="Friend">Friend</asp:ListItem>
					<asp:ListItem Value="Husband">Husband</asp:ListItem>
					<asp:ListItem Value="Wife">Wife</asp:ListItem>
					<asp:ListItem Value="Domestic Partner">Domestic Partner</asp:ListItem>
					<asp:ListItem Value="Other">Other</asp:ListItem>
				</asp:DropDownList></td>
		</tr>
		<tr>
			<td>Contact Relationship Details:</td>
			<td></td>
			<td>
				<asp:TextBox id="txtRelationshipDetails" runat="server" width="300px" Font-Names="Verdana" 
					ToolTip="Enter in additional information about the alternate contacts relationship with the applicant"
					></asp:TextBox></td>
		</tr>
		<tr><td colspan="3" align="center" class="style1">
            <asp:Button ID="ButtonContinueContact" runat="server" 
                Text="Next Page &gt;&gt;" onclick="ButtonContinueContact_Click" /><br />
            <asp:Label ID="LabelErrorPatient" runat="server" ForeColor="Red"></asp:Label>
            </td></tr>
	</table>
    </div>
    </asp:Panel>
    <asp:Panel ID="PanelDiagHistory" runat="server" Visible="false">
    <div class="FormTable" style="clear:both; width:600px;">
    <table id="Table4" cellSpacing="2" cellPadding="2" width="600" border="0">
		<tr>
			<td align="center" bgColor="#cccccc" colSpan="3">
                <font color="steelblue"><strong>Applicant 
						History and Diagnosis Information</strong></font></td>
		</tr>
		<tr>
			<td style="width:300px;">Has an application been submitted for this patient in the past?:<FONT color="red">*</FONT></td>
			<td>
				<asp:requiredfieldvalidator id="rfValPastGIPAP" runat="server" ErrorMessage="You must indicate if the applicant has applied for GIPAP before."
					ControlToValidate="rbAppliedGIPAP">*</asp:requiredfieldvalidator></td>
			<td>
				<asp:RadioButtonList id="rbAppliedGIPAP" runat="server" ToolTip="Indicate whether or not the applicant has applied for GIPAP before"
					RepeatDirection="Horizontal" CellPadding="0" CellSpacing="0">
					<asp:ListItem Value="0">No</asp:ListItem>
					<asp:ListItem Value="1">Yes</asp:ListItem>
				</asp:RadioButtonList></td>
		</tr>
		<tr>
			<td>
				<asp:Label id="LabelPatientConsent" runat="server">Has the Patient Consent Form been signed?<font color="red">
						*</font></asp:Label></td>
			<td>
				<asp:requiredfieldvalidator id="Requiredfieldvalidator5" runat="server" ErrorMessage="You must indicate if the Patient Consent form has been collected."
					ControlToValidate="rblstPatientConsent">*</asp:requiredfieldvalidator></td>
			<td>
				<asp:RadioButtonList id="rblstPatientConsent" runat="server" ToolTip="Indicate whether or not the applicant has applied for GIPAP before"
					RepeatDirection="Horizontal" CellPadding="0" CellSpacing="0" >
					<asp:ListItem Value="0">No</asp:ListItem>
					<asp:ListItem Value="1">Yes</asp:ListItem>
				</asp:RadioButtonList></td>
		</tr>
		<tr>
			<td colSpan="3">
				<asp:Label id="LabelDoseMessage" runat="server" Font-Names="Verdana"  ForeColor="Gray"></asp:Label></td>
		</tr>
		<tr>
			<td>What is the prescribed daily dosage?:<FONT color="red">*</FONT></td>
			<td>
				<asp:CompareValidator id="CompareValidator3" runat="server" ErrorMessage="Daily Dosage" ControlToValidate="cboDosage"
					ValueToCompare="0" Operator="NotEqual">*</asp:CompareValidator></td>
			<td>
				<asp:DropDownList id="cboDosage" runat="server" width="110px" 
                    Font-Names="Verdana" onselectedindexchanged="cboDosage_SelectedIndexChanged" >
					<asp:ListItem Value="0">Select Dosage</asp:ListItem>
					<asp:ListItem Value="200mg">200mg</asp:ListItem>
					<asp:ListItem Value="260mg">260mg</asp:ListItem>
					<asp:ListItem Value="300mg">300mg</asp:ListItem>
					<asp:ListItem Value="400mg">400mg</asp:ListItem>
					<asp:ListItem Value="600mg">600mg</asp:ListItem>
					<asp:ListItem Value="800mg">800mg</asp:ListItem>
				</asp:DropDownList></td>
		</tr>
        <tr><td>
            <asp:Label ID="LabelTablet" runat="server" Text="Tablet Strength:" Visible="false"></asp:Label></td><td></td><td>
                <asp:DropDownList ID="dropTabletStrength" runat="server" Visible="false">
                </asp:DropDownList>
            </td></tr>
		<tr>
			<td>Diagnosis date:<FONT color="red">*</FONT></td>
			<td>
                <asp:CompareValidator ID="CompareValidator11" runat="server" ErrorMessage="Diagnosis Day" Text="*" ControlToValidate="cboDiagnosisDay" Operator="NotEqual" ValueToCompare="0"></asp:CompareValidator>
                <asp:CompareValidator ID="CompareValidator12" runat="server" ErrorMessage="Diagnosis Month" Text="*" ControlToValidate="cboDiagnosisMonth" Operator="NotEqual" ValueToCompare="0"></asp:CompareValidator>
                <asp:CompareValidator ID="CompareValidator13" runat="server" ErrorMessage="Diagnosis Year" Text="*" ControlToValidate="cboDiagnosisYear" Operator="NotEqual" ValueToCompare="0"></asp:CompareValidator>
				</td>
			<td>
				<asp:DropDownList id="cboDiagnosisDay" runat="server" Font-Names="Verdana" >
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
				<asp:DropDownList id="cboDiagnosisMonth" runat="server" Font-Names="Verdana" >
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
				<asp:DropDownList id="cboDiagnosisYear" runat="server" Font-Names="Verdana" ></asp:DropDownList></td>
		</tr>
		<tr>
			<td>Has patient previously taken Glivec®/imatinib?: 
                <FONT color="red">*</FONT></td>
			<td>
				<asp:RequiredFieldValidator id="RequiredFieldValidator1" runat="server" ErrorMessage="You must Indicate if Patient has previoulsy taken Glivec"
					ControlToValidate="rblstGlivec">*</asp:RequiredFieldValidator></td>
			<td>
				<asp:RadioButtonList id="rblstGlivec" runat="server" ToolTip="Indicate if the patient has taken Glivec/imatinib in the past"
					RepeatDirection="Horizontal" CellPadding="0" CellSpacing="0">
					<asp:ListItem Value="0">No</asp:ListItem>
					<asp:ListItem Value="1">Yes</asp:ListItem>
				</asp:RadioButtonList></td>
		</tr>
		<tr>
			<td>If yes, what was the starting date?:</td>
			<td>
				<asp:customvalidator id="Customvalidator1" runat="server" ErrorMessage="You must select the applicant's day started Glivec"
					ControlToValidate="GlivecStartDay" ClientValidationFunction="validateGlivecDate">*</asp:customvalidator>
				<asp:customvalidator id="Customvalidator2" runat="server" ErrorMessage="You must select the applicant'smonth started Glivec"
					ControlToValidate="GlivecStartMonth" ClientValidationFunction="validateGlivecDate">*</asp:customvalidator>
				<asp:customvalidator id="Customvalidator3" runat="server" ErrorMessage="You must select the applicant's year started Glivec."
					ControlToValidate="GlivecStartYear" ClientValidationFunction="validateGlivecDate">*</asp:customvalidator></td>
			<td>
				<asp:DropDownList id="GlivecStartDay" runat="server" Font-Names="Verdana" >
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
				<asp:DropDownList id="GlivecStartMonth" runat="server" Font-Names="Verdana" >
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
				<asp:DropDownList id="GlivecStartYear" runat="server" Font-Names="Verdana" ></asp:DropDownList></td>
		</tr>
	</table>
    </div>
    </asp:Panel>
    <asp:Panel ID="PanelDFSP" runat="server" Visible="false">
    <div class="FormTable" style="clear:both; width:600px;">
    <table id="Table16" cellSpacing="2" cellPadding="2" width="600" border="0">
		<tr>
			<td align="center" bgColor="#cccccc" colSpan="3"><FONT color="steelblue"><STRONG>DFSP&nbsp;Information</STRONG></FONT>
				| <A href="javascript:openNewWindow('Physician/DFSP.aspx','thewin','height=440,width=645,toolbar=no,scrollbars=yes')">
					<FONT color="blue">DFSP Drug Profile</FONT></A> <FONT color="red" size="1">new!</FONT></td>
		</tr>
		<tr>
			<td>Is the tumor unresectable, recurrent or metastatic?</td>
			<td>
				<asp:requiredfieldvalidator id="Requiredfieldvalidator9" runat="server" ErrorMessage="Is the tumor unresectable, recurrent or metastatic?"
					ControlToValidate="rblstRecurrent">*</asp:requiredfieldvalidator></td>
			<td>
				<asp:RadioButtonList id="rblstRecurrent" runat="server" ToolTip="Indicate if the tumor is unresectable, recurrent or metastatic"
					RepeatDirection="Horizontal" CellPadding="0" CellSpacing="0">
					<asp:ListItem Value="No">No</asp:ListItem>
					<asp:ListItem Value="Yes">Yes</asp:ListItem>
				</asp:RadioButtonList></td>
		</tr>
	</table>
    </div>
    </asp:Panel>
    <asp:Panel ID="PanelMDS" runat="server" Visible="false">
    <div class="FormTable" style="clear:both; width:600px;">
    <table id="Table18" cellSpacing="2" cellPadding="2" width="600" border="0">
		<tr>
			<td align="center" bgColor="#cccccc" >
                <font color="steelblue"><strong>MDS / MPD&#160;Information</strong></font></td>
		</tr>
		<tr><td>Please provide a medical summary, including a description of how the diagnosis was was determined:<asp:RequiredFieldValidator 
                ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtMDSSummary" 
                ErrorMessage="MDS / MPD medical summary">*</asp:RequiredFieldValidator>
            </td></tr>
		<tr><td>
            <asp:TextBox ID="txtMDSSummary" runat="server" Width="440px" 
                TextMode="MultiLine" Height="75px"></asp:TextBox></td></tr>
		<tr><td></td></tr>
		</table>
    </div>
    </asp:Panel>
    <asp:Panel ID="PanelSysMast" runat="server" Visible="false">
    <div class="FormTable" style="clear:both; width:600px;">
    <table id="Table19" cellSpacing="2" cellPadding="2" width="600" border="0">
		<tr>
			<td align="center" bgColor="#cccccc" >
                <font color="steelblue"><strong>Systemic Mastocytosis&#160;Information</strong></font></td>
		</tr>
		<tr><td>Please provide a medical summary, including a description of how the diagnosis was was determined:<asp:RequiredFieldValidator 
                ID="RequiredFieldValidator13" runat="server" ControlToValidate="txtSysMasSummary" 
                ErrorMessage="Systemic Mastocytosis medical summary">*</asp:RequiredFieldValidator>
            </td></tr>
		<tr><td>
            <asp:TextBox ID="txtSysMasSummary" runat="server" Width="440px" 
                TextMode="MultiLine" Height="75px"></asp:TextBox></td></tr>
		<tr><td></td></tr>
		</table>
    </div>
    </asp:Panel>
    <asp:Panel ID="PanelHES" runat="server" Visible="false">
    <div class="FormTable" style="clear:both; width:600px;">
    <table id="Table20" cellSpacing="2" cellPadding="2" width="600" border="0">
		<tr>
			<td align="center" bgColor="#cccccc" >
                <font color="steelblue"><strong>HES / CEL&#160;Information</strong></font></td>
		</tr>
		<tr><td>Please provide a medical summary, including a description of how the diagnosis was was determined:<asp:RequiredFieldValidator 
                ID="RequiredFieldValidator14" runat="server" ControlToValidate="txtHesSummary" 
                ErrorMessage="HES / CEL medical summary">*</asp:RequiredFieldValidator>
            </td></tr>
		<tr><td>
            <asp:TextBox ID="txtHesSummary" runat="server" Width="440px" 
                TextMode="MultiLine" Height="75px"></asp:TextBox></td></tr>
		<tr><td></td></tr>
		</table>
    </div>
    </asp:Panel>
    <asp:Panel ID="PanelALL" runat="server" Visible="false">
    <div class="FormTable" style="clear:both; width:600px;">
    <table id="Table14" cellSpacing="2" cellPadding="2" width="600" border="0">
		<tr>
			<td align="center" bgColor="#cccccc" colSpan="3"><FONT color="steelblue"><STRONG>Ph+ 
						ALL&nbsp;Information</STRONG></FONT></td>
		</tr>
		<tr>
			<td>Is&nbsp;applicant Philadelphia Chromosome Positive?:<FONT color="red">*</FONT></td>
			<td>
				<asp:requiredfieldvalidator id="Requiredfieldvalidator6" runat="server" ErrorMessage="You must indicate if the applicant is Philadelphia Chromosome Positive?."
					ControlToValidate="rblstAllPh">*</asp:requiredfieldvalidator></td>
			<td>
				<asp:RadioButtonList id="rblstAllPh" runat="server" ToolTip="Indicate if the applicant is Philadelphia Chromosome Positive"
					RepeatDirection="Horizontal" CellPadding="0" CellSpacing="0">
					<asp:ListItem Value="0">No</asp:ListItem>
					<asp:ListItem Value="1">Yes</asp:ListItem>
					<asp:ListItem Value="2">Don't Know</asp:ListItem>
				</asp:RadioButtonList></td>
		</tr>
		<tr>
			<td>If no, is patient BCR-Abl positive?:</td>
			<td>
				<asp:customvalidator id="Customvalidator12" runat="server" ErrorMessage="You must select weather the Patient is BCR-Abl +"
					ControlToValidate="rblstAllPh" ClientValidationFunction="validateALLBCR">*</asp:customvalidator></td>
			<td>
				<asp:RadioButtonList id="rblstAllBCR" runat="server" ToolTip="Indicate if the tumor is Unresectable only if the applicant is BCR-Abl positive"
					RepeatDirection="Horizontal" CellPadding="0" CellSpacing="0">
					<asp:ListItem Value="0">No</asp:ListItem>
					<asp:ListItem Value="1">Yes</asp:ListItem>
					<asp:ListItem Value="2">Don't Know</asp:ListItem>
				</asp:RadioButtonList></td>
		</tr>
		<tr>
			<td colSpan="3">
				<asp:Panel id="Panel2ndOnly" runat="server">
					<table id="Table15" cellSpacing="0" cellPadding="0" width="100%" border="0">
						<tr>
							<td>Is this patient diagnosed<BR>
								with relapsed or refractory Ph+ ALL?</td>
							<td>
								<asp:RequiredFieldValidator id="RequiredFieldValidator7" runat="server" ErrorMessage="Is this patient diagnosed with relapsed or refractory Ph+ ALL?"
									ControlToValidate="rblst2ndLineRelapsed">*</asp:RequiredFieldValidator></td>
							<td>
								<asp:RadioButtonList id="rblst2ndLineRelapsed" runat="server" RepeatDirection="Horizontal">
									<asp:ListItem Value="No">No</asp:ListItem>
									<asp:ListItem Value="Yes">Yes</asp:ListItem>
								</asp:RadioButtonList></td>
						</tr>
					</table>
				</asp:Panel></td>
		</tr>
		<tr>
			<td colSpan="3">
				<asp:Panel id="Panel1stand2nd" runat="server">
					<P>Please indicate which of the following Ph+ ALL treatment options is applicable 
						to your patient:
						<asp:RequiredFieldValidator id="RequiredFieldValidator8" runat="server" ErrorMessage="Ph+ ALL Treatment Options"
							ControlToValidate="rblst1stand2nd">*</asp:RequiredFieldValidator></P>
					<asp:RadioButtonList id="rblst1stand2nd" runat="server">
						<asp:ListItem Value="Patient is diagnosed with relapsed or refractory Ph+ ALL and treatment is monotherapy">Patient is diagnosed with relapsed or refractory Ph+ ALL and treatment is monotherapy</asp:ListItem>
						<asp:ListItem Value="Patient is newly diagnosed with Ph+ ALL and treatment is integrated with chemotherapy">Patient is newly diagnosed with Ph+ ALL and treatment is integrated with chemotherapy</asp:ListItem>
					</asp:RadioButtonList>
				</asp:Panel></td>
		</tr>
	</table>
    </div>
    </asp:Panel>
    <asp:Panel ID="PanelCML" runat="server" Visible="false">
    <div class="FormTable" style="clear:both; width:600px;">
    <table id="Table5" cellSpacing="2" cellPadding="2" width="600" border="0">
		<tr>
			<td align="center" bgColor="#cccccc" colSpan="3"><FONT color="steelblue"><STRONG>CML 
						Information</STRONG></FONT></td>
		</tr>
		<tr>
			<td>Is&nbsp;applicant Philadelphia Chromosome Positive?:<FONT color="red">*</FONT></td>
			<td>
				<asp:requiredfieldvalidator id="rfValPhilPos" runat="server" ErrorMessage="You must indicate if the applicant is Philadelphia Chromosome Positive?."
					ControlToValidate="rbPhilPos">*</asp:requiredfieldvalidator></td>
			<td>
				<asp:RadioButtonList id="rbPhilPos" runat="server" ToolTip="Indicate if the applicant is Philadelphia Chromosome Positive"
					RepeatDirection="Horizontal" CellPadding="0" CellSpacing="0">
					<asp:ListItem Value="0">No</asp:ListItem>
					<asp:ListItem Value="1">Yes</asp:ListItem>
					<asp:ListItem Value="2">Don't Know</asp:ListItem>
				</asp:RadioButtonList></td>
		</tr>
		<tr>
			<td>Is patient BCR-Abl positive?:</td>
			<td>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ErrorMessage="You must indicate if the applicant is BCR-Abl positive" ControlToValidate="rblstBCR" Text="*"></asp:RequiredFieldValidator>
				</td>
			<td>
				<asp:RadioButtonList id="rblstBCR" runat="server" ToolTip="Indicate if the tumor is Unresectable only if the applicant is BCR-Abl positive"
					RepeatDirection="Horizontal" CellPadding="0" CellSpacing="0">
					<asp:ListItem Value="0">No</asp:ListItem>
					<asp:ListItem Value="1">Yes</asp:ListItem>
					<asp:ListItem Value="2">Don't Know</asp:ListItem>
				</asp:RadioButtonList></td>
		</tr>
		<tr>
			<td>What is the phase of the CML disease?:<FONT color="red">*</FONT></td>
			<td>
                <asp:CompareValidator ID="CompareValidator14" runat="server" ErrorMessage="CML Phase" Text="*" ControlToValidate="cboCMLPhase" Operator="NotEqual" ValueToCompare="0"></asp:CompareValidator></td>
			<td>
				<asp:DropDownList id="cboCMLPhase" runat="server" >
					<asp:ListItem Value="0">Select a Phase</asp:ListItem>
					<asp:ListItem Value="Chronic">Chronic</asp:ListItem>
					<asp:ListItem Value="Accelerated">Accelerated</asp:ListItem>
					<asp:ListItem Value="Blast Crisis">Blast Crisis</asp:ListItem>
				</asp:DropDownList></td>
		</tr>
	</table>
    </div>
    </asp:Panel>
    <asp:Panel ID="PanelGIST" runat="server" Visible="false">
    <div class="FormTable" style="clear:both; width:600px;">
    <table id="Table6" cellSpacing="2" cellPadding="2" width="600" border="0">
		<tr>
			<td align="center" bgColor="#cccccc" colSpan="3"><FONT color="steelblue"><STRONG>GIST And 
						C-Kit Information</STRONG></FONT></td>
		</tr>
		<tr>
			<td>Is the applicant C-Kit (CD117) Positive?:<FONT color="red">*</FONT></td>
			<td>
				<asp:requiredfieldvalidator id="rfValCKiPos" runat="server" ErrorMessage="You must indicate if the applicant is C-Kit Positive."
					ControlToValidate="rbCKitPos">*</asp:requiredfieldvalidator></td>
			<td>
				<asp:RadioButtonList id="rbCKitPos" runat="server" ToolTip="Indicate if the applicant  is C-Kit positive"
					RepeatDirection="Horizontal">
					<asp:ListItem Value="0">No</asp:ListItem>
					<asp:ListItem Value="1">Yes</asp:ListItem>
					<asp:ListItem Value="2">Don't Know</asp:ListItem>
				</asp:RadioButtonList></td>
		</tr>
		<tr>
			<td>Is the tumor Unresectable?:<FONT color="red">*</FONT></td>
			<td>
				<asp:RequiredFieldValidator id="rfValUnresectable" runat="server" ErrorMessage="You must indicate if the tumor is Unresectable."
					ControlToValidate="rbUnresectable">*</asp:RequiredFieldValidator></td>
			<td>
				<asp:RadioButtonList id="rbUnresectable" runat="server" ToolTip="Indicate if the tumor is Unresectable only if the applicant is C-Kit positive"
					RepeatDirection="Horizontal">
					<asp:ListItem Value="0">No</asp:ListItem>
					<asp:ListItem Value="1">Yes</asp:ListItem>
					<asp:ListItem Value="2">Don't Know</asp:ListItem>
				</asp:RadioButtonList></td>
		</tr>
		<tr>
			<td>Is the tumor Metastatic?:<FONT color="red">*</FONT></td>
			<td>
				<asp:RequiredFieldValidator id="rfValMetastatic" runat="server" ErrorMessage="You must indicate if the tumor is Metastatic"
					ControlToValidate="rbMetastatic">*</asp:RequiredFieldValidator></td>
			<td>
				<asp:RadioButtonList id="rbMetastatic" runat="server" RepeatDirection="Horizontal">
					<asp:ListItem Value="0">No</asp:ListItem>
					<asp:ListItem Value="1">Yes</asp:ListItem>
					<asp:ListItem Value="2">Don't Know</asp:ListItem>
				</asp:RadioButtonList></td>
		</tr>
	</table>
    </div>
    </asp:Panel>
    <asp:Panel ID="PanelAdjGIST" runat="server" Visible="false">
    <div class="FormTable" style="clear:both; width:600px;">
    <table id="Table17" width="600" border="0">
		<tr>
			<td align="center" bgColor="#cccccc" colSpan="3"><FONT color="steelblue"><STRONG>GIST Treatment Information</STRONG></FONT></td>
		</tr>
		<tr><td style="width:350px;">Is this adjuvant treatment?</td><td>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" 
                ControlToValidate="rblstAdj" ErrorMessage="Is this Adjuvant Treatment?">*</asp:RequiredFieldValidator>
            </td><td>
                <asp:RadioButtonList ID="rblstAdj" runat="server" 
                    onselectedindexchanged="rblstAdj_SelectedIndexChanged" CausesValidation="false" CellPadding="0" CellSpacing="0"
                    RepeatDirection="Horizontal" AutoPostBack="True">
                    <asp:ListItem>No</asp:ListItem>
                    <asp:ListItem>Yes</asp:ListItem>
                </asp:RadioButtonList>
            </td></tr>
            <tr><td><font color="gray">Answering "Yes" will require a dosage of 400mg</font></td></tr>
		<tr>
			<td>Is the case intermediate or high risk due to mitotic count and tumor size?</td>
			<td>
                    <asp:customvalidator id="Customvalidator25" runat="server" ErrorMessage="Is the case intermediate or high risk due to mitotic count and tumor size?"
					ControlToValidate="rblstAdj" ClientValidationFunction="validateAdjuvantTreatment">*</asp:customvalidator>
                    </td>
			<td>
				<asp:RadioButtonList id="rblstHighRisk" runat="server" ToolTip="Is the case intermediate or high risk due to mitotic count and tumor size?"
					RepeatDirection="Horizontal" >
					<asp:ListItem Value="0">No</asp:ListItem>
					<asp:ListItem Value="1">Yes</asp:ListItem>
					<asp:ListItem Value="2">Don't Know</asp:ListItem>
				</asp:RadioButtonList></td>
		</tr>
	</table>
    </div>
    </asp:Panel>
    <asp:Panel ID="PanelTasigna" runat="server" Visible="false">
    <div class="FormTable" style="clear:both; width:600px;">
    <table id="Table21" width="600" border="0">
		<tr>
			<td align="center" bgColor="#cccccc" colSpan="3">
                <font color="steelblue"><strong>Tasigna Information</strong></font></td>
		</tr>
		<tr>
			<td class="style2">Diagnosis date:<FONT color="red">*</FONT></td>
			<td>
				<asp:CompareValidator ID="CompareValidator15" runat="server" ErrorMessage="Diagnosis Day" Text="*" ControlToValidate="dropTasignaDiagDay" Operator="NotEqual" ValueToCompare="0"></asp:CompareValidator>
				<asp:CompareValidator ID="CompareValidator16" runat="server" ErrorMessage="Diagnosis Month" Text="*" ControlToValidate="dropTasignaDiagMonth" Operator="NotEqual" ValueToCompare="0"></asp:CompareValidator>
				<asp:CompareValidator ID="CompareValidator17" runat="server" ErrorMessage="Diagnosis Year" Text="*" ControlToValidate="dropTasignaDiagYear" Operator="NotEqual" ValueToCompare="0"></asp:CompareValidator></td>
			<td>
				<asp:DropDownList id="dropTasignaDiagDay" runat="server">
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
				<asp:DropDownList id="dropTasignaDiagMonth" runat="server">
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
				<asp:DropDownList id="dropTasignaDiagYear" runat="server" >
            <asp:ListItem Value="0">Year</asp:ListItem>
            </asp:DropDownList></td>
		</tr>
		<tr>
			<td class="style2">Has patient previously taken Glivec®?: 
                <FONT color="red">*</FONT></td>
			<td>
				<asp:RequiredFieldValidator id="RequiredFieldValidator20" 
            runat="server" ErrorMessage="You must Indicate if Patient has previoulsy taken Glivec"
					ControlToValidate="rblstTasignaGlivec">*</asp:RequiredFieldValidator></td>
			<td>
				<asp:RadioButtonList id="rblstTasignaGlivec" runat="server" 
            ToolTip="Indicate if the patient has taken Glivec/imatinib in the past"
					RepeatDirection="Horizontal" CellPadding="0" CellSpacing="0">
					<asp:ListItem Value="0">No</asp:ListItem>
					<asp:ListItem Value="1">Yes</asp:ListItem>
				</asp:RadioButtonList></td>
		</tr>
		<tr>
			<td class="style2">Has patient previously taken generic imatinib?: 
                <FONT color="red">*</FONT></td>
			<td>
				<asp:RequiredFieldValidator id="RequiredFieldValidator21" 
            runat="server" ErrorMessage="You must Indicate if Patient has previoulsy taken Imatinib"
					ControlToValidate="rblstTasignaImatinib">*</asp:RequiredFieldValidator></td>
			<td>
				<asp:RadioButtonList id="rblstTasignaImatinib" runat="server" 
            ToolTip="Indicate if the patient has taken generic imatinib in the past"
					RepeatDirection="Horizontal" CellPadding="0" CellSpacing="0">
					<asp:ListItem Value="0">No</asp:ListItem>
					<asp:ListItem Value="1">Yes</asp:ListItem>
				</asp:RadioButtonList></td>
		</tr>
		<tr>
			<td class="style2">Is the patient intolerant to Glivec/Imatinib?</td>
			<td>
				<asp:RequiredFieldValidator ID="RequiredFieldValidator22" 
            runat="server" ControlToValidate="rblstGlivecIntolerant" 
            ErrorMessage="Is the patient intolerant to Glivec?">*</asp:RequiredFieldValidator>
            </td>
			<td>
				<asp:RadioButtonList id="rblstGlivecIntolerant" runat="server" RepeatDirection="Horizontal" CellPadding="0"
					CellSpacing="0">
					<asp:ListItem Value="0">No</asp:ListItem>
					<asp:ListItem Value="1">Yes</asp:ListItem>
				</asp:RadioButtonList></td>
		</tr>
		<tr>
			<td class="style2">Is the patient resistant to Glivec/Imatinib?</td>
			<td>
				<asp:RequiredFieldValidator ID="RequiredFieldValidator23" 
            runat="server" ControlToValidate="rblstGlivecResistant" 
            ErrorMessage="Is the patient resistant to Glivec?">*</asp:RequiredFieldValidator>
            </td>
			<td>
				<asp:RadioButtonList id="rblstGlivecResistant" runat="server" RepeatDirection="Horizontal" CellPadding="0"
					CellSpacing="0">
					<asp:ListItem Value="0">No</asp:ListItem>
					<asp:ListItem Value="1">Yes</asp:ListItem>
				</asp:RadioButtonList></td>
		</tr>
		<tr>
			<td class="style2">Is the patient currently receiving dasatinib?</td>
			<td>
				<asp:RequiredFieldValidator id="RequiredFieldValidator24" runat="server" ErrorMessage="Is the patient receiving Dasatinib"
					ControlToValidate="rblstDasatinib">*</asp:RequiredFieldValidator></td>
			<td>
				<asp:RadioButtonList id="rblstDasatinib" runat="server" RepeatDirection="Horizontal" CellPadding="0"
					CellSpacing="0">
					<asp:ListItem Value="0">No</asp:ListItem>
					<asp:ListItem Value="1">Yes</asp:ListItem>
				</asp:RadioButtonList></td>
		</tr>
		<tr>
			<td class="style2">Is the patient intolerant to dasatinib?</td>
			<td>
				<asp:RequiredFieldValidator id="RequiredFieldValidator17" runat="server" ErrorMessage="Is the patient intolerant to dasatinib?"
					ControlToValidate="rblstDasatinibIntolerant">*</asp:RequiredFieldValidator></td>
			<td>
				<asp:RadioButtonList id="rblstDasatinibIntolerant" runat="server" RepeatDirection="Horizontal" CellPadding="0"
					CellSpacing="0">
					<asp:ListItem Value="0">No</asp:ListItem>
					<asp:ListItem Value="1">Yes</asp:ListItem>
				</asp:RadioButtonList></td>
		</tr>
		<tr>
			<td class="style2">Is the patient resistant to dasatinib?</td>
			<td>
				<asp:RequiredFieldValidator id="RequiredFieldValidator26" runat="server" ErrorMessage="Is the patient resistant to dasatinib?"
					ControlToValidate="rblstDasatinibResistant">*</asp:RequiredFieldValidator></td>
			<td>
				<asp:RadioButtonList id="rblstDasatinibResistant" runat="server" RepeatDirection="Horizontal" CellPadding="0"
					CellSpacing="0">
					<asp:ListItem Value="0">No</asp:ListItem>
					<asp:ListItem Value="1">Yes</asp:ListItem>
				</asp:RadioButtonList></td>
		</tr>
		<tr>
			<td class="style2">Has patient previously taken nilotinib/Tasigna®?</td>
			<td>
				<asp:RequiredFieldValidator id="RequiredFieldValidator25" runat="server" ErrorMessage="Has the patient previously taken tasigna?"
					ControlToValidate="rblstTasigna">*</asp:RequiredFieldValidator></td>
			<td>
				<asp:RadioButtonList id="rblstTasigna" runat="server" RepeatDirection="Horizontal" CellPadding="0" CellSpacing="0">
					<asp:ListItem Value="0">No</asp:ListItem>
					<asp:ListItem Value="1">Yes</asp:ListItem>
				</asp:RadioButtonList></td>
		</tr>
		<tr>
			<td class="style2">If yes, what was the starting date?</td>
			<td>
				<asp:CustomValidator id="CustomValidator21" runat="server" ErrorMessage="Tasigna Start Day" ControlToValidate="dropTasignaStartDay"
					ClientValidationFunction="validateTasignaDate">*</asp:CustomValidator>
				<asp:CustomValidator id="CustomValidator22" runat="server" ErrorMessage="Tasigna Start Month" ControlToValidate="dropTasignaStartMonth"
					ClientValidationFunction="validateTasignaDate">*</asp:CustomValidator>
				<asp:CustomValidator id="CustomValidator23" runat="server" ErrorMessage="Tasigna Start Year" ControlToValidate="dropTasignaStartYear"
					ClientValidationFunction="validateTasignaDate">*</asp:CustomValidator></td>
			<td>
				<asp:DropDownList id="dropTasignaStartDay" runat="server">
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
				<asp:DropDownList id="dropTasignaStartMonth" runat="server">
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
				<asp:DropDownList id="dropTasignaStartYear" runat="server">
            <asp:ListItem Value="0">Year</asp:ListItem>
            </asp:DropDownList></td>
		</tr>
		<tr>
			<td class="style2">If yes, what was the prescribed daily dose?</td>
			<td>
				<asp:CustomValidator id="CustomValidator16" runat="server" ErrorMessage="Previous Tasigna Dosage" ControlToValidate="dropTasignaDose"
					ClientValidationFunction="validateTasignaDate">*</asp:CustomValidator></td>
			<td>
				<asp:DropDownList id="dropTasignaDose" runat="server" >
					<asp:ListItem Value="0">Select a Dose</asp:ListItem>
					<asp:ListItem Value="400mg BID">400mg BID</asp:ListItem>
					<asp:ListItem Value="400mg QD">400mg QD</asp:ListItem>
					<asp:ListItem>300mg BID</asp:ListItem>
				</asp:DropDownList></td>
		</tr>
		<tr>
			<td class="style2">Requested NOA Tasigna dose:</td>
			<td>
				<asp:CompareValidator id="CompareValidator6" runat="server" 
            ErrorMessage="Requested Tasigna Dose" ControlToValidate="dropRequestedTasignaDose"
					ValueToCompare="0" Operator="NotEqual">*</asp:CompareValidator></td>
			<td>
				<asp:DropDownList id="dropRequestedTasignaDose" runat="server">
					<asp:ListItem Value="0">Select a Dose</asp:ListItem>
					<asp:ListItem Value="400mg BID">400mg BID</asp:ListItem>
					<asp:ListItem Value="400mg QD">400mg QD</asp:ListItem>
					<asp:ListItem>300mg BID</asp:ListItem>
				</asp:DropDownList></td>
		</tr>
		<tr>
			<td class="style2">Has the patient applied to NOA Tasigna in the past?:<FONT color="red">*</FONT></td>
			<td>
				<asp:requiredfieldvalidator id="Requiredfieldvalidator18" 
            runat="server" ErrorMessage="You must indicate if the applicant has applied for NOA Tasigna before."
					ControlToValidate="rblstNOATasigna">*</asp:requiredfieldvalidator></td>
			<td>
				<asp:RadioButtonList id="rblstNOATasigna" runat="server" ToolTip="Indicate whether or not the applicant has applied for GIPAP before"
					RepeatDirection="Horizontal" CellPadding="0" CellSpacing="0">
					<asp:ListItem Value="0">No</asp:ListItem>
					<asp:ListItem Value="1">Yes</asp:ListItem>
				</asp:RadioButtonList></td>
		</tr>
		<tr>
			<td class="style2">
				<asp:Label id="Label1" runat="server">Has the NOA Tasigna 
                Patient Consent Form been signed?<font color="red">
						*</font></asp:Label></td>
			<td>
				<asp:requiredfieldvalidator id="Requiredfieldvalidator19" 
            runat="server" ErrorMessage="You must indicate if the Patient Consent form has been collected."
					ControlToValidate="rblstTasignaPatientConsent">*</asp:requiredfieldvalidator></td>
			<td>
				<asp:RadioButtonList id="rblstTasignaPatientConsent" runat="server" 
            ToolTip="Indicate whether or not the applicant has applied for GIPAP before"
					RepeatDirection="Horizontal" CellPadding="0" CellSpacing="0">
					<asp:ListItem Value="0">No</asp:ListItem>
					<asp:ListItem Value="1">Yes</asp:ListItem>
				</asp:RadioButtonList></td>
		</tr>
	</table>
    </div>
    </asp:Panel>
    <asp:Panel ID="PanelDiagButton" runat="server" Visible="false">
    <table id="Table3" width="600" border="0">
    <tr><td class="style1" align="center">
        <asp:Button ID="ButtonContinueDiag" runat="server" Text="Next Page >>" 
            onclick="ButtonContinueDiag_Click" /><br />
        <asp:Label ID="LabelErrorDiag" runat="server" ForeColor="Red"></asp:Label>
        </td></tr>
    </table>
    </asp:Panel>
    <asp:Panel ID="PanelInsurance" runat="server" Visible="false">
    <div class="FormTable" style="clear:both; width:600px;">
    <table id="Table2" width="600" border="0">
		<tr>
			<td align="center" bgColor="#cccccc" colSpan="3"><FONT color="steelblue"><STRONG>Insurance 
						Information</STRONG></FONT></td>
		</tr>
		<tr>
			<td>Is the applicant eligible for health insurance/reimbursment?:</td>
			<td>
				<asp:RequiredFieldValidator id="RequiredFieldValidator3" runat="server" ErrorMessage="You must indicate if the applicant is eligible for health insurance"
					ControlToValidate="rblstHealthInsurance">*</asp:RequiredFieldValidator></td>
			<td>
				<asp:RadioButtonList id="rblstHealthInsurance" runat="server" RepeatDirection="Horizontal" CellPadding="0"
					CellSpacing="0">
					<asp:ListItem Value="0">No</asp:ListItem>
					<asp:ListItem Value="1">Yes</asp:ListItem>
				</asp:RadioButtonList></td>
		</tr>
		<tr>
			<td>If yes, does it cover prescription drugs?:</td>
			<td>
				<asp:CustomValidator id="CustomValidator5" runat="server" ErrorMessage="You must indicate if the insurance covers prescription drugs"
					ControlToValidate="rblstHealthInsurance" ClientValidationFunction="validateCoversRx">*</asp:CustomValidator></td>
			<td>
				<asp:RadioButtonList id="rbCoversRx" runat="server" RepeatDirection="Horizontal" CellPadding="0" CellSpacing="0">
					<asp:ListItem Value="0">No</asp:ListItem>
					<asp:ListItem Value="1">Yes</asp:ListItem>
				</asp:RadioButtonList></td>
		</tr>
		<tr>
			<td>Does it cover Cancer drugs?:</td>
			<td>
				<asp:CustomValidator id="CustomValidator6" runat="server" ErrorMessage="You must indicate if the insurance covers cancer drugs"
					ControlToValidate="rblstHealthInsurance" ClientValidationFunction="validateCoversCancer">*</asp:CustomValidator></td>
			<td>
				<asp:RadioButtonList id="rbCoversCancerRx" runat="server" RepeatDirection="Horizontal" CellPadding="0"
					CellSpacing="0">
					<asp:ListItem Value="0">No</asp:ListItem>
					<asp:ListItem Value="1">Yes</asp:ListItem>
				</asp:RadioButtonList></td>
		</tr>
		<tr>
			<td>Does it cover Glivec® treatment?:</td>
			<td>
				<asp:CustomValidator id="CustomValidator7" runat="server" ErrorMessage="You must indicate if the insurance covers Glivec"
					ControlToValidate="rblstHealthInsurance" ClientValidationFunction="validateCoversGlivec">*</asp:CustomValidator></td>
			<td>
				<asp:RadioButtonList id="rbCoversGlivecRx" runat="server" RepeatDirection="Horizontal" CellPadding="0"
					CellSpacing="0">
					<asp:ListItem Value="0">No</asp:ListItem>
					<asp:ListItem Value="1">Yes</asp:ListItem>
				</asp:RadioButtonList></td>
		</tr>
	</table>
    </div>
    </asp:Panel>
    <asp:Panel ID="PanelFEF" runat="server" Visible="false">
    <div class="FormTable" style="clear:both; width:600px;">
    <table id="Table11" cellSpacing="1" cellPadding="0" width="600" border="0">
		<tr bgColor="silver">
			<td align="center" colSpan="2"><FONT color="steelblue"><STRONG>Medical Evaluation Documents 
						Collected:</STRONG></FONT></td>
		</tr>
		<tr>
			<td>Summary of Medical Chart:</td>
			<td>
				<asp:RadioButtonList id="rblstMedicalChart" runat="server" RepeatDirection="Horizontal">
					<asp:ListItem Value="No">No</asp:ListItem>
					<asp:ListItem Value="Yes">Yes</asp:ListItem>
					<asp:ListItem Value="N/A" Selected="True">N/A</asp:ListItem>
				</asp:RadioButtonList></td>
		</tr>
		<tr>
			<td>CML: Verification from Physician of Philadelphia Chromosome/BCR-Abl 
				Test Results:</td>
			<td>
				<asp:RadioButtonList id="rblstPhil" runat="server" RepeatDirection="Horizontal">
					<asp:ListItem Value="No">No</asp:ListItem>
					<asp:ListItem Value="Yes">Yes</asp:ListItem>
					<asp:ListItem Value="N/A" Selected="True">N/A</asp:ListItem>
				</asp:RadioButtonList></td>
		</tr>
		<tr>
			<td>GIST: Verification of C-Kit Test Results:</td>
			<td>
				<asp:RadioButtonList id="rblstCKit" runat="server" RepeatDirection="Horizontal">
					<asp:ListItem Value="No">No</asp:ListItem>
					<asp:ListItem Value="Yes">Yes</asp:ListItem>
					<asp:ListItem Value="N/A" Selected="True">N/A</asp:ListItem>
				</asp:RadioButtonList></td>
		</tr>
		<tr bgColor="silver">
			<td align="center" colSpan="2"><FONT color="steelblue"><STRONG>Financial&nbsp;Evaluation 
						Documents Collected:</STRONG></FONT></td>
		</tr>
		<tr>
			<td>Copy of Patient's ID:</td>
			<td>
				<asp:RadioButtonList id="rblstCopyofID" runat="server" RepeatDirection="Horizontal">
					<asp:ListItem Value="No">No</asp:ListItem>
					<asp:ListItem Value="Yes">Yes</asp:ListItem>
					<asp:ListItem Value="N/A" Selected="True">N/A</asp:ListItem>
				</asp:RadioButtonList></td>
		</tr>
		<tr>
			<td>Photo:</td>
			<td>
				<asp:RadioButtonList id="rblstPhoto" runat="server" RepeatDirection="Horizontal">
					<asp:ListItem Value="No">No</asp:ListItem>
					<asp:ListItem Value="Yes">Yes</asp:ListItem>
					<asp:ListItem Value="N/A" Selected="True">N/A</asp:ListItem>
				</asp:RadioButtonList></td>
		</tr>
		<tr>
			<td>Social Security Card:</td>
			<td>
				<asp:RadioButtonList id="rblstSScard" runat="server" RepeatDirection="Horizontal">
					<asp:ListItem Value="No">No</asp:ListItem>
					<asp:ListItem Value="Yes">Yes</asp:ListItem>
					<asp:ListItem Value="N/A" Selected="True">N/A</asp:ListItem>
				</asp:RadioButtonList></td>
		</tr>
		<tr>
			<td>Private Insurance Card:</td>
			<td>
				<asp:RadioButtonList id="rblstInsuranceCard" runat="server" RepeatDirection="Horizontal">
					<asp:ListItem Value="0">No</asp:ListItem>
					<asp:ListItem Value="1">Yes</asp:ListItem>
					<asp:ListItem Value="2">N/A</asp:ListItem>
				</asp:RadioButtonList></td>
		</tr>
		<tr>
			<td>(If yes, please identify the insurance)</td>
			<td>
				<asp:TextBox id="txtInsuranceType" runat="server" Width="250px"></asp:TextBox>
				<asp:CustomValidator id="CustomValidator8" runat="server" ErrorMessage="You must identify the insurance"
					ControlToValidate="rblstInsuranceCard" ClientValidationFunction="validateInsuranceCard">*</asp:CustomValidator></td>
		</tr>
		<tr>
			<td>Tax Return:</td>
			<td>
				<asp:RadioButtonList id="rblstTaxReturn" runat="server" RepeatDirection="Horizontal">
					<asp:ListItem Value="No">No</asp:ListItem>
					<asp:ListItem Value="Yes">Yes</asp:ListItem>
					<asp:ListItem Value="N/A" Selected="True">N/A</asp:ListItem>
				</asp:RadioButtonList></td>
		</tr>
		<tr>
			<td>Income Verification Document(s):</td>
			<td>
				<asp:RadioButtonList id="rblstSalarySlip" runat="server" RepeatDirection="Horizontal">
					<asp:ListItem Value="No">No</asp:ListItem>
					<asp:ListItem Value="Yes">Yes</asp:ListItem>
					<asp:ListItem Value="N/A" Selected="True">N/A</asp:ListItem>
				</asp:RadioButtonList></td>
		</tr>
		<tr>
			<td>Financial&nbsp;Declaration Form:</td>
			<td>
				<asp:RadioButtonList id="rblstFinAffidavit" runat="server" RepeatDirection="Horizontal">
					<asp:ListItem Value="No">No</asp:ListItem>
					<asp:ListItem Value="Yes">Yes</asp:ListItem>
					<asp:ListItem Value="N/A" Selected="True">N/A</asp:ListItem>
				</asp:RadioButtonList></td>
		</tr>
		<tr>
			<td>Utility and/or Telephone Bill:</td>
			<td>
				<asp:RadioButtonList id="rblstPhoneBill" runat="server" RepeatDirection="Horizontal">
					<asp:ListItem Value="No">No</asp:ListItem>
					<asp:ListItem Value="Yes">Yes</asp:ListItem>
					<asp:ListItem Value="N/A" Selected="True">N/A</asp:ListItem>
				</asp:RadioButtonList></td>
		</tr>
		<tr>
			<td>Other: (Please list any other documents specific to your country)</td>
			<td>
				<asp:TextBox id="txtOtherDocs" runat="server" Width="250px" Height="52px" TextMode="MultiLine"></asp:TextBox></td>
		</tr>
		<tr>
			<td>Is the applicant eligible for health 
				insurance/reimbursment?
			</td>
			<td>
				<asp:RadioButtonList id="rblstMaxInsurance" runat="server" RepeatDirection="Horizontal">
					<asp:ListItem Value="0">No</asp:ListItem>
					<asp:ListItem Value="1">Yes</asp:ListItem>
				</asp:RadioButtonList>
				<asp:RequiredFieldValidator id="RequiredFieldValidator2" runat="server" ErrorMessage="Is the applicant eligible for health insurance"
					ControlToValidate="rblstMaxInsurance">*</asp:RequiredFieldValidator></td>
		</tr>
		<tr>
			<td>If yes, does the insurance/reimbursment include prescription drugs?</td>
			<td>
				<asp:RadioButtonList id="rblstMaxRx" runat="server" RepeatDirection="Horizontal">
					<asp:ListItem Value="0">No</asp:ListItem>
					<asp:ListItem Value="1">Yes</asp:ListItem>
				</asp:RadioButtonList>
				<asp:CustomValidator id="CustomValidator9" runat="server" ErrorMessage="You must indicate if the insurance covers prescription drugs"
					ControlToValidate="rblstMaxInsurance" ClientValidationFunction="validateCoversRxMAX">*</asp:CustomValidator></td>
		</tr>
		<tr>
			<td>If yes, does the insurance/reimbursment include cancer drugs?</td>
			<td>
				<asp:RadioButtonList id="rblstMaxCancerRx" runat="server" RepeatDirection="Horizontal">
					<asp:ListItem Value="0">No</asp:ListItem>
					<asp:ListItem Value="1">Yes</asp:ListItem>
				</asp:RadioButtonList>
				<asp:CustomValidator id="CustomValidator10" runat="server" ErrorMessage="You must indicate if the insurance covers cancer drugs"
					ControlToValidate="rblstMaxInsurance" ClientValidationFunction="validateCoversCancerMAX">*</asp:CustomValidator></td>
		</tr>
		<tr>
			<td>If yes, does the insurance/reimbursment include Glivec?</td>
			<td>
				<asp:RadioButtonList id="rblstMaxGlivecRx" runat="server" RepeatDirection="Horizontal">
					<asp:ListItem Value="0">No</asp:ListItem>
					<asp:ListItem Value="1">Yes</asp:ListItem>
				</asp:RadioButtonList>
				<asp:CustomValidator id="CustomValidator11" runat="server" ErrorMessage="You must indicate if the insurance covers Glivec"
					ControlToValidate="rblstMaxInsurance" ClientValidationFunction="validateCoversGlivecMAX">*</asp:CustomValidator></td>
		</tr>
		<tr>
			<td>Number of Household Members:</td>
			<td>
				<asp:DropDownList id="dropHousehold" runat="server">
					<asp:ListItem Value="0">Select Number</asp:ListItem>
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
				</asp:DropDownList></td>
		</tr>
		<tr>
			<td>Occupation of Financially Contributing Members (please list):</td>
			<td>
				<asp:TextBox id="txtHouseholdOccupation" runat="server" Width="250px" Height="46px" TextMode="MultiLine"></asp:TextBox></td>
		</tr>
		<tr>
			<td>Household's Annual Income (in US dollars):</td>
			<td>
				<asp:TextBox id="txtHouseholdIncom" runat="server" Width="250px"></asp:TextBox></td>
		</tr>
		<tr>
			<td>Total of any additional funds received by the household (in US dollars):</td>
			<td>
				<asp:TextBox id="txtAdditionalFunds" runat="server" Width="250px"></asp:TextBox></td>
		</tr>
		<tr>
			<td>Approximate value of assets of the houshold (in US dollars):</td>
			<td>
				<asp:TextBox id="txtAssets" runat="server" Width="250px"></asp:TextBox></td>
		</tr>
	</table>
	<table id="Table12" width="600" border="0">
		<tr bgColor="silver">
			<td align="center"><FONT color="steelblue"><STRONG>Recommendation:</STRONG></FONT></td>
		</tr>
		<tr>
			<td>
				<asp:RadioButtonList id="rblstRecommendation" runat="server">
					<asp:ListItem Value="Approve">Approve</asp:ListItem>
					<asp:ListItem Value="Deny">Deny</asp:ListItem>
					<asp:ListItem Value="Request further assessment by TMF Global">Request further assessment by TMF Global</asp:ListItem>
					<asp:ListItem Value="Pending" Selected="True">Pending</asp:ListItem>
				</asp:RadioButtonList></td>
		</tr>
		<tr>
			<td>Case Summary&nbsp;(Required):
				<asp:RequiredFieldValidator id="RequiredFieldValidator4" runat="server" ErrorMessage="Case Summary" ControlToValidate="txtExplanation">*</asp:RequiredFieldValidator></td>
		</tr>
		<tr>
			<td>
				<asp:TextBox id="txtExplanation" runat="server" Width="100%" Height="106px" TextMode="MultiLine"></asp:TextBox></td>
		</tr>
	</table>
    </div>
    </asp:Panel>
    <asp:Panel ID="PanelFinancial" runat="server" Visible="false">
     <div class="FormTable" style="clear:both; width:600px;">
    <table id="Table8" width="600" border="0">
		<tr>
			<td align="center" bgColor="#cccccc" colSpan="3"><FONT color="steelblue"><STRONG>Financial 
						Information</STRONG></FONT></td>
		</tr>
		<tr>
			<td><FONT color="red"><FONT color="#000000">Patient's or primary income earner's estimated 
						annual income (US$)</FONT> *</FONT></td>
			<td>
				<asp:CompareValidator id="CompareValidator4" runat="server" ErrorMessage="Annual Income" ControlToValidate="dropIncome"
					ValueToCompare="0" Operator="NotEqual">*</asp:CompareValidator></td>
			<td>
				<asp:DropDownList id="dropIncome" runat="server">
					<asp:ListItem Value="0">Select One</asp:ListItem>
					<asp:ListItem Value="0 (no income)">0 (no income)</asp:ListItem>
					<asp:ListItem Value="1 – 1000">1 – 1000</asp:ListItem>
					<asp:ListItem Value="1000 – 5000">1000 – 5000</asp:ListItem>
					<asp:ListItem Value="5,000 – 10,000">5,000 – 10,000</asp:ListItem>
					<asp:ListItem Value="10,000 – 20,000">10,000 – 20,000</asp:ListItem>
					<asp:ListItem Value="20,000 – 30,000">20,000 – 30,000</asp:ListItem>
					<asp:ListItem Value="More than 30,000">More than 30,000</asp:ListItem>
				</asp:DropDownList></td>
		</tr>
		<tr>
			<td><FONT color="red"><FONT color="#000000">Patient's or primary income earner's 
						Occupation:</FONT> *&nbsp;</FONT></td>
			<td>
				<asp:CompareValidator id="CompareValidator1" runat="server" ErrorMessage="You must indicate Occupation"
					ControlToValidate="dropOccupation" ValueToCompare="0" Operator="NotEqual">*</asp:CompareValidator></td>
			<td>
				<asp:DropDownList id="dropOccupation" runat="server">
					<asp:ListItem Value="0">Select One</asp:ListItem>
					<asp:ListItem Value="Manufacturing">Manufacturing</asp:ListItem>
					<asp:ListItem Value="Agriculture/Fishing">Agriculture/Fishing</asp:ListItem>
					<asp:ListItem Value="Hospitality">Hospitality</asp:ListItem>
					<asp:ListItem Value="Transport">Transport</asp:ListItem>
					<asp:ListItem Value="Health/Social Work">Health/Social Work</asp:ListItem>
					<asp:ListItem Value="Government Service">Government Service</asp:ListItem>
					<asp:ListItem Value="Education">Education</asp:ListItem>
					<asp:ListItem Value="Business/Professional">Business/Professional</asp:ListItem>
					<asp:ListItem Value="Self-Employed">Self-Employed</asp:ListItem>
					<asp:ListItem Value="Student">Student</asp:ListItem>
					<asp:ListItem Value="Retired">Retired</asp:ListItem>
					<asp:ListItem Value="Unemployed">Unemployed</asp:ListItem>
					<asp:ListItem Value="Other">Other</asp:ListItem>
				</asp:DropDownList></td>
		</tr>
	</table>
	</div>
    </asp:Panel>
    <asp:Panel ID="PanelNotes" runat="server" Visible="false">
     <div class="FormTable" style="clear:both; width:600px;">
    <table id="Table28" width="600" border="0">
		<tr>
			<td align="center" bgColor="silver"><FONT color="steelblue">Please write any additional 
					information pertaining to the patient's application:</FONT></td>
		</tr>
		<tr>
			<td>
				<asp:TextBox id="txtNotes" runat="server" width="580px" ToolTip="Use this field to enter any other information or comments that you think is needed and was not included in the form above"
					Height="108px" TextMode="MultiLine"></asp:TextBox></td>
		</tr>
		<tr>
			<td align="center" class="style1">
                <asp:Button ID="ButtonContinueFinancial" runat="server" 
                    Text="Next Page &gt;&gt;" onclick="ButtonContinueFinancial_Click" />
            </td>
		</tr>
	</table>
	</div>
    </asp:Panel>
    </div>
    <div style="width:300px; float:left; font-size:10pt;">
    <asp:Panel ID="PanelValidation" runat="server" Visible="false">
    <div class='LeftColDivHeader'>Applicant Summary</div>
    <div class='LeftColDiv'>
    <asp:Label ID="LabelSummaryIntro" runat="server" Text=""></asp:Label><br />
        <asp:LinkButton ID="lbEditIntro" runat="server" CssClass="lbAR" 
            onclick="lbEditIntro_Click" CausesValidation="false">[edit]</asp:LinkButton>
        <br /><br />
        <asp:Label ID="LabelSummaryPatient" runat="server" Text=""></asp:Label><br />
        <asp:LinkButton ID="lbEditPatient" runat="server" CssClass="lbAR" 
            Visible="false" onclick="lbEditPatient_Click" CausesValidation="false">[edit]</asp:LinkButton>
        <br /><br />
        <asp:Label ID="LabelSummaryDiag" runat="server" Text=""></asp:Label><br />
        <asp:LinkButton ID="lbEditDiag" runat="server" CssClass="lbAR" Visible="false" 
            onclick="lbEditDiag_Click" CausesValidation="false">[edit]</asp:LinkButton>
            </div>
        
    </asp:Panel>
    </div>
    <div style="clear:both;">
        <asp:Panel ID="PanelSummary" runat="server" Visible="false">
        <div class="FormTable" style="clear:both; width:900px;">
        <div style="width:300px; float:left;">
        <asp:Label ID="LabelSummaryPatient2" runat="server" Text=""></asp:Label><br />
        <asp:LinkButton ID="lbEditPatient2" runat="server" CssClass="lbAR" 
                onclick="lbEditPatient2_Click">[edit]</asp:LinkButton></div>
        <div style="width:300px; float:left;">
        <asp:Label ID="LabelSummaryDiag2" runat="server" Text=""></asp:Label><br />
        <asp:LinkButton ID="lbEditDiag2" runat="server" CssClass="lbAR" 
                onclick="lbEditDiag2_Click">[edit]</asp:LinkButton></div>
        <div style="width:300px; float:left;">
        <asp:Label ID="LabelSummaryFin" runat="server" Text=""></asp:Label><br />
        <asp:LinkButton ID="lbEditFin" runat="server" CssClass="lbAR" 
                onclick="lbEditFin_Click">[edit]</asp:LinkButton></div>
        <div style="width:900px; clear:both; text-align:center; padding:10px;">
        <asp:Button ID="ButtonSubmit" runat="server" Text="Submit Patient Application" 
            onclick="ButtonSubmit_Click" BackColor="LimeGreen" /></div>
            </div>
        </asp:Panel>
        <asp:Panel ID="PanelReceived" runat="server" Visible="false">
        <h4>Information Received</h4>
        Thank you for completing the application form.  Your information has been received and will be processed by The Max Foundation as soon as possible.</asp:Panel>
    </div>
    <div style="clear:both;">
    
    </div>
</ContentTemplate>
</asp:UpdatePanel>
</div>
<script language="javascript">

		    //**********************************************************************************************************************
        function validateApplicantContact(sender, e) {
            if ($get('<%= txtPhone.ClientID  %>').value || $get('<%= txtFax.ClientID  %>').value || $get('<%= txtEmail.ClientID  %>').value || $get('<%= txtMobile.ClientID  %>').value) {
                e.IsValid = true;
            }
            else {
                e.IsValid = false;
            }
        }

        //**********************************************************************************************************************
        function validateCaregiverCountry(sender, e) {
            if ($get('<%= txtContactFirstName.ClientID  %>').value || $get('<%= txtContactLastName.ClientID  %>').value) {
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

        //**********************************************************************************************************************
        //Function checks to see if alternate contact is provided and if it is, validates that at least one contact method is entered
        function validateCaregiverContact(sender, e) {
            if ($get('<%= txtContactFirstName.ClientID  %>').value || $get('<%= txtContactLastName.ClientID  %>').value) {
                if ($get('<%= txtContactPhone.ClientID  %>').value || $get('<%= txtContactFax.ClientID  %>').value || $get('<%= txtContactEmail.ClientID  %>').value) {
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
        function validateCaregiverRelationship(sender, e) {
            if ($get('<%= txtContactFirstName.ClientID  %>').value || $get('<%= txtContactLastName.ClientID  %>').value) {
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


        //**********************************************************************************************************************
        //Function checks to see if patient has Insurance and if answer is Yes, validates the required dependent fields
        function validateGlivecDate(sender, e) {
            if ($get('<%= rblstGlivec.ClientID  %>' + '_1').checked) {
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

        //Function checks to see if Aduvant treatment has been checked Yes , validates the required dependant fields 
        //**********************************************************************************************************************
        function validateAdjuvantTreatment(sender, e) {
            if (e.Value == "Yes") {
                if ($get('<%= rblstHighRisk.ClientID  %>' + '_0').checked || $get('<%= rblstHighRisk.ClientID  %>' + '_1').checked || $get('<%= rblstHighRisk.ClientID  %>' + '_2').checked) {

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
        function validateALLBCR(sender, e) {
            if (e.Value == "0") {
                if ($get('<%= rblstAllBCR.ClientID  %>' + '_0').checked || $get('<%= rblstAllBCR.ClientID  %>' + '_1').checked || $get('<%= rblstAllBCR.ClientID  %>' + '_2').checked) {

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

        //******************************************************************************************************************************************
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

        //**********************************************************************************************************************
        function validateCoversRx(sender, e) {
            if (e.Value == "1") {
                if ($get('<%= rbCoversRx.ClientID  %>' + '_0').checked || $get('<%= rbCoversRx.ClientID  %>' + '_1').checked) {

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
        function validateCoversCancer(sender, e) {
            if (e.Value == "1") {
                if ($get('<%= rbCoversCancerRx.ClientID  %>' + '_0').checked || $get('<%= rbCoversCancerRx.ClientID  %>' + '_1').checked) {

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
        function validateCoversGlivec(sender, e) {
            if (e.Value == "1") {
                if ($get('<%= rbCoversGlivecRx.ClientID  %>' + '_0').checked || $get('<%= rbCoversGlivecRx.ClientID  %>' + '_1').checked) {

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
        /*max station validation*/
        //**********************************************************************************************************************
        function validateCoversRxMAX(sender, e) {
            if (e.Value == "1") {
                if ($get('<%= rblstMaxRx.ClientID  %>' + '_0').checked || $get('<%= rblstMaxRx.ClientID  %>' + '_1').checked) {

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
        function validateCoversCancerMAX(sender, e) {
            if (e.Value == "1") {
                if ($get('<%= rblstMaxCancerRx.ClientID  %>' + '_0').checked || $get('<%= rblstMaxCancerRx.ClientID  %>' + '_1').checked) {

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
        function validateCoversGlivecMAX(sender, e) {
            if (e.Value == "1") {
                if ($get('<%= rblstMaxGlivecRx.ClientID  %>' + '_0').checked || $get('<%= rblstMaxGlivecRx.ClientID  %>' + '_1').checked) {

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
        /*end max station validation*/
        //**********************************************************************************************************************
        function validateInsuranceCard(sender, e) {
            if (e.Value == "1") {
                if ($get('<%= txtInsuranceType.ClientID  %>').value) {
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
</script>
</asp:Content>

