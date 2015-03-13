<%@ Page Language="C#" MasterPageFile="~/Patient/GIPAPPatient.master" AutoEventWireup="true" CodeFile="EditPatient.aspx.cs" Inherits="Patient_EditPatient" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderControlPanel" Runat="Server">
    <h1>Edit Patient Information</h1>
    <TABLE id="Table7" cellSpacing="1" cellPadding="1" width="585" align="left" border="0">
		<TR>
			<TD><asp:validationsummary id="ValidationSummary1" runat="server" HeaderText="You are missing the following fields:"
		ShowMessageBox="True" CssClass="AlertDiv" ForeColor=""></asp:validationsummary></TD>
		</TR>
		</TABLE>
    <div class="FormTable" style="clear:both;">
<TABLE id="Table2" cellSpacing="1" cellPadding="1" width="600" border="0">
			<TR>
				<TD>First Name:</TD>
				<TD>
					<asp:TextBox id="txtFirstName" runat="server" Width="200px"></asp:TextBox>
					<asp:RequiredFieldValidator id="RequiredFieldValidator2" runat="server" ErrorMessage="First Name" ControlToValidate="txtFirstName">*</asp:RequiredFieldValidator></TD>
				<TD>Sex:</TD>
				<TD>
					<asp:RadioButtonList id="rblstSex" runat="server" RepeatDirection="Horizontal">
						<asp:ListItem Value="M">Male</asp:ListItem>
						<asp:ListItem Value="F">Female</asp:ListItem>
					</asp:RadioButtonList></TD>
			</TR>
			<TR>
				<TD height="16">Last Name:</TD>
				<TD height="16">
					<asp:TextBox id="txtLastName" runat="server" Width="200px"></asp:TextBox>
					<asp:RequiredFieldValidator id="RequiredFieldValidator3" runat="server" ErrorMessage="Last Name" ControlToValidate="txtLastName">*</asp:RequiredFieldValidator></TD>
				<TD>Birth Date:</TD>
				<TD>
					<asp:DropDownList id="cboBirthDay" runat="server">
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
					<asp:DropDownList id="cboBirthMonth" runat="server">
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
					<asp:DropDownList id="cboBirthYear" runat="server" Width="62px"></asp:DropDownList></TD>
			</TR>
            <TR runat="server" id="thai">
				<TD>Thai Name:</TD>
				<TD>
					<asp:TextBox id="txtThaiName" runat="server" Width="200px"></asp:TextBox></TD>
				<TD></TD>
			</TR>
			<TR>
				<TD>Address 1:</TD>
				<TD>
					<asp:TextBox id="txtStreet1" runat="server" Width="200px"></asp:TextBox></TD>
				<TD></TD>
				<TD vAlign="top" rowSpan="7">
					<asp:CompareValidator id="CompareValidator1" runat="server" ErrorMessage="Birth Day" ControlToValidate="cboBirthDay"
						ValueToCompare="0" Operator="NotEqual">*</asp:CompareValidator>
					<asp:CompareValidator id="CompareValidator2" runat="server" ErrorMessage="Birth Month" ControlToValidate="cboBirthMonth"
						ValueToCompare="0" Operator="NotEqual">*</asp:CompareValidator>
					<asp:CompareValidator id="CompareValidator3" runat="server" ErrorMessage="Birth Year" ControlToValidate="cboBirthYear"
						ValueToCompare="Year" Operator="NotEqual">*</asp:CompareValidator></TD>
			</TR>
			<TR>
				<TD>Address 2:</TD>
				<TD>
					<asp:TextBox id="txtStreet2" runat="server" Width="200px"></asp:TextBox></TD>
				<TD></TD>
			</TR>
			<TR>
				<TD>City:</TD>
				<TD>
					<asp:TextBox id="txtCity" runat="server" Width="200px"></asp:TextBox></TD>
				<TD></TD>
			</TR>
			<TR>
				<TD>State / Province:</TD>
				<TD>
					<asp:TextBox id="txtState" runat="server" Width="200px"></asp:TextBox></TD>
				<TD></TD>
			</TR>
			<TR>
				<TD>Postal Code:</TD>
				<TD>
					<asp:TextBox id="txtPostal" runat="server" Width="200px"></asp:TextBox></TD>
				<TD></TD>
			</TR>
			<TR>
				<TD>Country:</TD>
				<TD>
					<asp:DropDownList id="dropCountry" runat="server" Width="200px"></asp:DropDownList>
					<asp:CompareValidator id="CompareValidator4" runat="server" ErrorMessage="Country" ControlToValidate="dropCountry"
						ValueToCompare="0" Operator="NotEqual">*</asp:CompareValidator></TD>
				<TD></TD>
			</TR>
			<TR>
				<TD>Phone:</TD>
				<TD>
					<asp:TextBox id="txtPhone" runat="server" Width="200px"></asp:TextBox></TD>
				<TD></TD>
			</TR>
			<TR>
				<TD>Fax:</TD>
				<TD>
					<asp:TextBox id="txtFax" runat="server" Width="200px"></asp:TextBox></TD>
				<TD></TD>
				<TD></TD>
			</TR>
			<TR>
				<TD>Mobile:</TD>
				<TD>
					<asp:TextBox id="txtMobile" runat="server" Width="200px"></asp:TextBox></TD>
				<TD></TD>
				<TD></TD>
			</TR>
			<TR>
				<TD>Email:</TD>
				<TD>
					<asp:TextBox id="txtEmail" runat="server" Width="200px"></asp:TextBox></TD>
				<TD></TD>
				<TD></TD>
			</TR>
			<TR>
				<TD colSpan="4">
					<HR color="steelblue">
				</TD>
			</TR>
		</TABLE>
		<asp:Panel id="PanelPH" runat="server" Visible="false">
			<TABLE id="Table10" cellSpacing="1" cellPadding="1" width="600"  border="0">
				<TR>
					<TD width="170">Philadelphia +:</TD>
					<TD>
						<asp:RadioButtonList id="rblstPhilPos" runat="server" Width="210px" RepeatDirection="Horizontal">
							<asp:ListItem Value="No">No</asp:ListItem>
							<asp:ListItem Value="Yes">Yes</asp:ListItem>
							<asp:ListItem Value="Don't Know">Don't Know</asp:ListItem>
						</asp:RadioButtonList></TD>
					<TD></TD>
					<TD></TD>
				</TR>
				<TR>
					<TD width="170">If no, is patient BCR-Abl +:</TD>
					<TD>
						<asp:RadioButtonList id="rblstBCR" runat="server" Width="210px" RepeatDirection="Horizontal">
							<asp:ListItem Value="No">No</asp:ListItem>
							<asp:ListItem Value="Yes">Yes</asp:ListItem>
							<asp:ListItem Value="Don't Know">Don't Know</asp:ListItem>
						</asp:RadioButtonList></TD>
					<TD></TD>
					<TD></TD>
				</TR>
			</TABLE>
		</asp:Panel>
		<asp:Panel id="PanelPHAll" runat="server" Visible="false">
			<TABLE id="Table11" cellSpacing="1" cellPadding="1" width="600"  border="0">
				<TR>
					<TD width="170">Relapsed / Refractory:</TD>
					<TD>
						<asp:RadioButtonList id="rblstRelapsedRefractory" runat="server" RepeatDirection="Horizontal">
							<asp:ListItem Value="No">No</asp:ListItem>
							<asp:ListItem Value="Yes">Yes</asp:ListItem>
						</asp:RadioButtonList></TD>
					<TD></TD>
					<TD></TD>
				</TR>
				<TR>
					<TD width="170">Newly Diagnosed / Integrated with Chemotherapy:</TD>
					<TD>
						<asp:RadioButtonList id="rblstChemo" runat="server" RepeatDirection="Horizontal">
							<asp:ListItem Value="No">No</asp:ListItem>
							<asp:ListItem Value="Yes">Yes</asp:ListItem>
						</asp:RadioButtonList></TD>
					<TD></TD>
					<TD></TD>
				</TR>
			</TABLE>
		</asp:Panel>
<asp:Panel ID="PanelAdjGist" runat="server" Visible="false">
<TABLE id="Table1" cellSpacing="1" cellPadding="1" width="600"  border="0">
<tr><td>Is this adjuvant treatment?</td><td>
                <asp:RadioButtonList ID="rblstAdj" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem>No</asp:ListItem>
                    <asp:ListItem>Yes</asp:ListItem>
                </asp:RadioButtonList>
                <asp:Label ID="LabelAdjuvantDose" runat="server" ForeColor="Gray" 
                    Text="Patient Must be on 400mg to be marked Adjuvant" Visible="False"></asp:Label>
            </td></tr>
				<TR>
					<TD width="170">High Risk due to mitotic count and tumor size:</TD>
					<TD>
						<asp:RadioButtonList id="rblstHighRisk" runat="server" RepeatDirection="Horizontal">
							<asp:ListItem Value="No">No</asp:ListItem>
							<asp:ListItem Value="Yes">Yes</asp:ListItem>
							<asp:ListItem Value="Don't Know">Don't Know</asp:ListItem>
						</asp:RadioButtonList></TD>
					<TD></TD>
					<TD></TD>
				</TR>
				</TABLE>
</asp:Panel>
<asp:Panel ID="PanelDiagSummary" runat="server" Visible="false">
<TABLE id="Table6" cellSpacing="1" cellPadding="1" width="600"  border="0">
				<TR>
					<TD width="170">Medical Summary:</TD>
					<TD>
                        <asp:TextBox ID="txtDiagSummary" runat="server" Width="350" Height="150" TextMode="MultiLine"></asp:TextBox></TD>
					</TR>
					</TABLE>
</asp:Panel>
		<asp:Panel id="PanelCML" runat="server" Visible="false">
			<TABLE id="Table3" cellSpacing="1" cellPadding="1" width="600"  border="0">
				<TR>
					<TD width="170">Current CML Phase:</TD>
					<TD>
						<asp:DropDownList id="dropCMLPhase" runat="server" Width="200px">
							<asp:ListItem Value="0">Select a Phase</asp:ListItem>
							<asp:ListItem Value="Chronic">Chronic</asp:ListItem>
							<asp:ListItem Value="Accelerated">Accelerated</asp:ListItem>
							<asp:ListItem Value="Blast Crisis">Blast Crisis</asp:ListItem>
							<asp:ListItem Value="Remission">Remission</asp:ListItem>
						</asp:DropDownList>
						<asp:CompareValidator id="CompareValidator5" runat="server" ErrorMessage="Current CML Phase" ControlToValidate="dropCMLPhase"
							ValueToCompare="0" Operator="NotEqual">*</asp:CompareValidator></TD>
					<TD></TD>
					<TD></TD>
				</TR>
				<TR>
					<TD width="170">Interferon Treatment:</TD>
					<TD>
						<asp:RadioButtonList id="rblstInterferon" runat="server" RepeatDirection="Horizontal">
							<asp:ListItem Value="No">No</asp:ListItem>
							<asp:ListItem Value="Yes">Yes</asp:ListItem>
						</asp:RadioButtonList></TD>
					<TD></TD>
					<TD></TD>
				</TR>
				<TR>
					<TD width="170">Interferon Time Length:</TD>
					<TD>
						<asp:DropDownList id="dropInterferonTime" runat="server" Width="200px">
							<asp:ListItem Value="0">Select Time</asp:ListItem>
							<asp:ListItem Value="Less than 6 Months">Less than 6 Months</asp:ListItem>
							<asp:ListItem Value="More than 6 Months">More than 6 Months</asp:ListItem>
						</asp:DropDownList></TD>
					<TD></TD>
					<TD></TD>
				</TR>
				<TR>
					<TD width="170">Intolerant:</TD>
					<TD>
						<asp:RadioButtonList id="rblstIntolerant" runat="server" RepeatDirection="Horizontal">
							<asp:ListItem Value="No">No</asp:ListItem>
							<asp:ListItem Value="Yes">Yes</asp:ListItem>
						</asp:RadioButtonList></TD>
					<TD></TD>
					<TD></TD>
				</TR>
				<TR>
					<TD width="170">Hematologic Failure:</TD>
					<TD>
						<asp:RadioButtonList id="rblstHema" runat="server" RepeatDirection="Horizontal">
							<asp:ListItem Value="No">No</asp:ListItem>
							<asp:ListItem Value="Yes">Yes</asp:ListItem>
						</asp:RadioButtonList></TD>
					<TD></TD>
					<TD></TD>
				</TR>
				<TR>
					<TD width="170" height="48">Cytogenetic Failure:</TD>
					<TD height="48">
						<asp:RadioButtonList id="rblstCyto" runat="server" RepeatDirection="Horizontal">
							<asp:ListItem Value="No">No</asp:ListItem>
							<asp:ListItem Value="Yes">Yes</asp:ListItem>
						</asp:RadioButtonList></TD>
					<TD height="48"></TD>
					<TD height="48"></TD>
				</TR>
				<TR>
					<TD width="170"></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
				</TR>
				<TR>
					<TD width="170"></TD>
					<TD></TD>
					<TD></TD>
					<TD></TD>
				</TR>
			</TABLE>
		</asp:Panel>
<asp:Panel ID="PanelTasigna" runat="server" Visible="false">
<table width="600" >
<TR>
			<TD align="center" bgColor="#cccccc" colSpan="3">
                <font color="steelblue"><strong>Tasigna Information</strong></font></TD>
		</TR>
		<TR>
			<TD width="176">Diagnosis date:<FONT color="red">*</FONT></TD>
			<TD width="33">
				<asp:customvalidator id="Customvalidator13" runat="server" ErrorMessage="You must select the applicant's diagnosis day."
					ControlToValidate="dropTasignaDiagDay" 
            ClientValidationFunction="validateDropDown">*</asp:customvalidator>
				<asp:customvalidator id="Customvalidator14" runat="server" ErrorMessage="You must select the applicant's diagnosis month."
					ControlToValidate="dropTasignaDiagMonth" 
            ClientValidationFunction="validateDropDown">*</asp:customvalidator>
				<asp:customvalidator id="Customvalidator15" runat="server" ErrorMessage="You must select the applicant's diagnosis year."
					ControlToValidate="dropTasignaDiagYear" 
            ClientValidationFunction="validateDropDown">*</asp:customvalidator></TD>
			<TD>
				<asp:DropDownList id="dropTasignaDiagDay" runat="server" 
            Font-Names="Verdana" Font-Size="XX-Small">
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
				<asp:DropDownList id="dropTasignaDiagMonth" runat="server" 
            Font-Names="Verdana" Font-Size="XX-Small">
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
				<asp:DropDownList id="dropTasignaDiagYear" runat="server" 
            Font-Names="Verdana" Font-Size="XX-Small">
            <asp:ListItem Value="0">Year</asp:ListItem>
            </asp:DropDownList></TD>
		</TR>
		<TR>
			<TD width="176">Has patient previously taken Glivec&reg;?: 
                <FONT color="red">*</FONT></TD>
			<TD width="33">
				<asp:RequiredFieldValidator id="RequiredFieldValidator20" 
            runat="server" ErrorMessage="You must Indicate if Patient has previoulsy taken Glivec"
					ControlToValidate="rblstTasignaGlivec">*</asp:RequiredFieldValidator></TD>
			<TD>
				<asp:RadioButtonList id="rblstTasignaGlivec" runat="server" 
            ToolTip="Indicate if the patient has taken Glivec/imatinib in the past"
					RepeatDirection="Horizontal" CellPadding="0" CellSpacing="0" >
					<asp:ListItem Value="0">No</asp:ListItem>
					<asp:ListItem Value="1">Yes</asp:ListItem>
				</asp:RadioButtonList></TD>
		</TR>
		<TR>
			<TD width="176">Has patient previously taken generic imatinib?: 
                <FONT color="red">*</FONT></TD>
			<TD width="33">
				<asp:RequiredFieldValidator id="RequiredFieldValidator21" 
            runat="server" ErrorMessage="You must Indicate if Patient has previoulsy taken Imatinib"
					ControlToValidate="rblstTasignaImatinib">*</asp:RequiredFieldValidator></TD>
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
			<TD width="150">
				<asp:RadioButtonList id="rblstGlivecIntolerant" runat="server" RepeatDirection="Horizontal" CellPadding="0"
					CellSpacing="0">
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
				<asp:RadioButtonList id="rblstGlivecResistant" runat="server" RepeatDirection="Horizontal" CellPadding="0"
					CellSpacing="0">
					<asp:ListItem Value="0">No</asp:ListItem>
					<asp:ListItem Value="1">Yes</asp:ListItem>
				</asp:RadioButtonList></TD>
		</TR>
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
			<TD width="156">Has patient previously taken nilotinib/Tasigna&reg;?</TD>
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
				<asp:DropDownList id="dropTasignaStartDay" runat="server" Font-Names="Verdana" Font-Size="XX-Small">
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
				<asp:DropDownList id="dropTasignaStartMonth" runat="server" Font-Names="Verdana" Font-Size="XX-Small">
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
				<asp:DropDownList id="dropTasignaStartYear" runat="server" Font-Names="Verdana" Font-Size="XX-Small">
            <asp:ListItem Value="0">Year</asp:ListItem>
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
					RepeatDirection="Horizontal" CellPadding="0" CellSpacing="0" >
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
					RepeatDirection="Horizontal" CellPadding="0" CellSpacing="0" >
					<asp:ListItem Value="0">No</asp:ListItem>
					<asp:ListItem Value="1">Yes</asp:ListItem>
				</asp:RadioButtonList></TD>
		</TR>
</table>
</asp:Panel>
		<asp:Panel id="PanelGIST" runat="server" Visible="false">
			<TABLE id="Table4" cellSpacing="1" cellPadding="1" width="600"  border="0">
				<TR>
					<TD width="170">C-Kit (CD117) +:</TD>
					<TD align="left">
						<asp:RadioButtonList id="rblstCkit" runat="server" RepeatDirection="Horizontal">
							<asp:ListItem Value="No">No</asp:ListItem>
							<asp:ListItem Value="Yes">Yes</asp:ListItem>
							<asp:ListItem Value="Don't Know">Don't Know</asp:ListItem>
						</asp:RadioButtonList></TD>
					<TD></TD>
					<TD></TD>
				</TR>
				<TR>
					<TD width="170">Unresectable:</TD>
					<TD align="left">
						<asp:RadioButtonList id="rblstUnresectable" runat="server" RepeatDirection="Horizontal">
							<asp:ListItem Value="No">No</asp:ListItem>
							<asp:ListItem Value="Yes">Yes</asp:ListItem>
							<asp:ListItem Value="Don't Know">Don't Know</asp:ListItem>
						</asp:RadioButtonList></TD>
					<TD></TD>
					<TD></TD>
				</TR>
				<TR>
					<TD width="170">Metastatic:</TD>
					<TD align="left">
						<asp:RadioButtonList id="rblstMetastatic" runat="server" RepeatDirection="Horizontal">
							<asp:ListItem Value="No">No</asp:ListItem>
							<asp:ListItem Value="Yes">Yes</asp:ListItem>
							<asp:ListItem Value="Don't Know">Don't Know</asp:ListItem>
						</asp:RadioButtonList></TD>
					<TD></TD>
					<TD></TD>
				</TR>
			</TABLE>
		</asp:Panel>
		<asp:Panel id="PanelDFSP" runat="server" Visible="false">
			<TABLE id="Table13" cellSpacing="1" cellPadding="1" width="600"  border="0">
				<TR>
					<TD width="268">Is the tumor unresectable, recurrent or metastatic?</TD>
					<TD>
						<asp:RadioButtonList id="rblstRecurrent" runat="server" RepeatDirection="Horizontal">
							<asp:ListItem Value="No">No</asp:ListItem>
							<asp:ListItem Value="Yes">Yes</asp:ListItem>
						</asp:RadioButtonList></TD>
				</TR>
			</TABLE>
		</asp:Panel>
<asp:Panel ID="PanelGlivec" runat="server" Visible="false">
<table width="600" >
<TR>
								<TD style="width: 144px">Diagnosis Date:</TD>
								<TD align="left">
									<asp:DropDownList id="dropDiagDay" runat="server">
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
									<asp:DropDownList id="dropDiagMonth" runat="server">
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
									<asp:DropDownList id="dropDiagYear" runat="server" ></asp:DropDownList></TD>
							</TR>
							<TR>
								<TD style="width: 144px">Glivec Start Date:</TD>
								<TD align="left">
									<asp:DropDownList id="dropGlivecStartDay" runat="server">
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
									<asp:DropDownList id="dropGlivecStartMonth" runat="server">
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
									<asp:DropDownList id="dropGlivecStartYear" runat="server"></asp:DropDownList></TD>
							</TR>
							<TR>
								<TD style="width: 144px">Glivec&reg;/Imatinib:</TD>
								<TD align="left">
									<asp:RadioButtonList id="rblstGlivec" runat="server" RepeatDirection="Horizontal">
										<asp:ListItem Value="No">No</asp:ListItem>
										<asp:ListItem Value="Yes">Yes</asp:ListItem>
									</asp:RadioButtonList></TD>
							</TR>
							</table>
<asp:Panel id="PanelPatientConsent" runat="server">
						<TABLE id="Table9" cellSpacing="1" cellPadding="1" width="600"  border="0">
							<TR>
								<TD style="width: 144px">Patient Consent Form:</TD>
								<TD align="left">
									<asp:RadioButtonList id="rblstPatientConsent" runat="server" RepeatDirection="Horizontal">
										<asp:ListItem Value="No" Selected="True">No</asp:ListItem>
										<asp:ListItem Value="Yes">Yes</asp:ListItem>
									</asp:RadioButtonList></TD>
							</TR>
						</TABLE>
					</asp:Panel>
</asp:Panel>
		<TABLE id="Table5" cellSpacing="1" cellPadding="1" width="600"  border="0">
			<TR>
				<TD colSpan="2">
					<HR color="steelblue">
				</TD>
			</TR>
			<TR>
				<TD vAlign="top" width="341">
					<asp:Panel id="PanelIncome" runat="server">
						<TABLE id="Table12" cellSpacing="1" cellPadding="1" width="100%" border="0">
							<TR>
								<TD width="109">Annual Income:</TD>
								<TD vAlign="top">
									<asp:TextBox id="txtIncome" runat="server" Width="200px"></asp:TextBox></TD>
							</TR>
							<TR>
								<TD width="109">Occupation:</TD>
								<TD>
									<asp:DropDownList id="dropOccupation" runat="server" Width="200px">
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
										<asp:ListItem Value="Unknown">Unknown</asp:ListItem>
										<asp:ListItem Value="Other">Other</asp:ListItem>
									</asp:DropDownList></TD>
							</TR>
							<TR>
								<TD width="109"></TD>
								<TD></TD>
							</TR>
							
						</TABLE>
					</asp:Panel>
					
					<TABLE id="Table8" cellSpacing="1" cellPadding="1" width="100%" border="0">
						<TR>
							<TD width="109">Reason For Changes:</TD>
							<TD><FONT color="white">150 characters max.</FONT><BR>
								<asp:TextBox id="txtReasonForChanges" runat="server" Width="200px" Height="84px" TextMode="MultiLine"></asp:TextBox>
								<asp:RequiredFieldValidator id="RequiredFieldValidator1" runat="server" ErrorMessage="Reason For Changes" ControlToValidate="txtReasonForChanges">*</asp:RequiredFieldValidator>
								<asp:RegularExpressionValidator id="RegularExpressionValidator1" runat="server" ErrorMessage="Reason For Changes - Too many characters entered (150 max.)"
									ControlToValidate="txtReasonForChanges" ValidationExpression="^[\s\S]{0,150}$">*</asp:RegularExpressionValidator></TD>
						</TR>
						<TR>
							<TD width="109"></TD>
							<TD>
								<asp:Button id="ButtonSave" runat="server" Width="50px" Text="Save" onclick="ButtonSave_Click"></asp:Button>&nbsp;
								<asp:Button id="ButtonCancel" runat="server" CausesValidation="False" Width="50px" Text="Cancel" onclick="ButtonCancel_Click"></asp:Button></TD>
						</TR>
						<TR>
							<TD width="109"></TD>
							<TD></TD>
						</TR>
					</TABLE>
				</TD>
				<TD></TD>
			</TR>
		</TABLE>
		</div>
	
</asp:Content>

