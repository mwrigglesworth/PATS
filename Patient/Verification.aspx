<%@ Page Language="C#" MasterPageFile="~/Patient/GIPAPPatient.master" AutoEventWireup="true"
    CodeFile="Verification.aspx.cs" Inherits="Patient_Verification" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderControlPanel"
    runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="0" DynamicLayout="true">
                <ProgressTemplate>
                    <div style="width: 100%; background-color: White">
                        <img src="../Images/loading.gif" /></div>
                </ProgressTemplate>
            </asp:UpdateProgress>
            <asp:Panel ID="PanelSummary" runat="server">
                <table id="Table1" cellspacing="1" cellpadding="1" width="600" align="center" border="0">
                <tr bgcolor="silver">
                    <td>
                        <p align="center">
                            <font color="steelblue" size="4">Patient Medical and Financial Evaluation</font></p>
                    </td>
                </tr>
                    <tr>
                        <td>
                            <asp:Label ID="LabelSummary" runat="server"></asp:Label>
                            <hr>
                        </td>
                    </tr>
                </table>
                <table id="Table6" cellspacing="1" cellpadding="1" width="600" align="center" border="0">
                    <tr>
                        <td align="center">
                            <asp:LinkButton ID="lbModify" ForeColor="SteelBlue" runat="server" OnClick="lbModify_Click">Modify</asp:LinkButton>
                        </td>
                        <td align="center" width="50%">
                            <asp:LinkButton ID="lbExit" ForeColor="SteelBlue" runat="server" OnClick="lbExit_Click">Exit</asp:LinkButton>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel ID="PanelForm" runat="server" Width="100%">
            <div class="ControlPanelDivHeader">
                    Edit Verification Information</div>
                <div class="ControlPanelHeaderRight">
                </div>
<div class="FormDiv">
<TABLE id="Table11" cellSpacing="1" cellPadding="1" width="585" align="left" border="0">
		<TR>
			<TD><asp:validationsummary id="ValidationSummary2" runat="server" HeaderText="You are missing the following fields:"
		ShowMessageBox="True" CssClass="AlertDiv" ForeColor=""></asp:validationsummary></TD>
		</TR>
	</TABLE>
                <table id="Table2" cellspacing="1" cellpadding="0" width="590" align="center" border="0">
                <tr bgcolor="silver">
                        <td colspan="2">
                            <font color="steelblue" size="4">Medical Evaluation Documents Collected:</font>
                        </td>
                    </tr>
                    <tr>
                        <td width="290">
                            Summary of Medical Chart:
                        </td>
                        <td style="width: 307px">
                            <asp:RadioButtonList ID="rblstMedicalChart" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="No">No</asp:ListItem>
                                <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                <asp:ListItem Value="N/A" Selected="True">N/A</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td width="290">
                            CML: Verification from Physician of Philadelphia Chromosome/BCR-Abl Test Results:
                        </td>
                        <td style="width: 307px">
                            <asp:RadioButtonList ID="rblstPhil" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="No">No</asp:ListItem>
                                <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                <asp:ListItem Value="N/A" Selected="True">N/A</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td width="290">
                            GIST: Verification of C-Kit Test Results:
                        </td>
                        <td style="width: 307px">
                            <asp:RadioButtonList ID="rblstCKit" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="No">No</asp:ListItem>
                                <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                <asp:ListItem Value="N/A" Selected="True">N/A</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr bgcolor="silver">
                        <td colspan="2">
                            <font color="steelblue" size="4">Financial&nbsp;Evaluation Documents Collected:</font>
                        </td>
                    </tr>
                    <tr>
                        <td width="290">
                            Copy of Patient's ID:
                        </td>
                        <td style="width: 307px">
                            <asp:RadioButtonList ID="rblstCopyofID" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="No">No</asp:ListItem>
                                <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                <asp:ListItem Value="N/A" Selected="True">N/A</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td width="290">
                            Photo:
                        </td>
                        <td style="width: 307px">
                            <asp:RadioButtonList ID="rblstPhoto" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="No">No</asp:ListItem>
                                <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                <asp:ListItem Value="N/A" Selected="True">N/A</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td width="290">
                            Social Security Card:
                        </td>
                        <td style="width: 307px">
                            <asp:RadioButtonList ID="rblstSScard" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="No">No</asp:ListItem>
                                <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                <asp:ListItem Value="N/A" Selected="True">N/A</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td width="290">
                            Private Insurance Card:
                        </td>
                        <td style="width: 307px">
                            <asp:RadioButtonList ID="rblstInsuranceCard" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="0">No</asp:ListItem>
                                <asp:ListItem Value="1">Yes</asp:ListItem>
                                <asp:ListItem Value="2">N/A</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td width="290">
                            (If yes, please identify the insurance)
                        </td>
                        <td style="width: 307px">
                            <asp:TextBox ID="txtInsuranceType" runat="server" Width="250px"></asp:TextBox>
                            <asp:CustomValidator ID="CustomValidator8" runat="server" ClientValidationFunction="validateInsuranceCard"
                                ControlToValidate="rblstInsuranceCard" ErrorMessage="You must identify the insurance">*</asp:CustomValidator>
                        </td>
                    </tr>
                    <tr>
                        <td width="290">
                            Tax Return:
                        </td>
                        <td style="width: 307px">
                            <asp:RadioButtonList ID="rblstTaxReturn" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="No">No</asp:ListItem>
                                <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                <asp:ListItem Value="N/A" Selected="True">N/A</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td width="290">
                            Income Verification Document(s):
                        </td>
                        <td style="width: 307px">
                            <asp:RadioButtonList ID="rblstSalarySlip" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="No">No</asp:ListItem>
                                <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                <asp:ListItem Value="N/A" Selected="True">N/A</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Financial&nbsp;Declaration Form
                        </td>
                        <td style="width: 307px">
                            <asp:RadioButtonList ID="rblstFinAffidavit" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="No">No</asp:ListItem>
                                <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                <asp:ListItem Value="N/A" Selected="True">N/A</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td width="290">
                            Utility and/or Telephone Bill:
                        </td>
                        <td style="width: 307px">
                            <asp:RadioButtonList ID="rblstPhoneBill" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="No">No</asp:ListItem>
                                <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                <asp:ListItem Value="N/A" Selected="True">N/A</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td width="290">
                            Other: (Please list any other documents specific to your country)
                        </td>
                        <td style="width: 307px">
                            <asp:TextBox ID="txtOtherDocs" runat="server" Width="250px" TextMode="MultiLine"
                                Height="52px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td width="290">
                            Is the applicant eligible for health insurance/reimbursment?
                        </td>
                        <td style="width: 307px">
                            <asp:RadioButtonList ID="rblstHealthInsurance" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="No">No</asp:ListItem>
                                <asp:ListItem Value="Yes">Yes</asp:ListItem>
                            </asp:RadioButtonList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="rblstHealthInsurance"
                                ErrorMessage="You must indicate if the applicant is eligible for health insurance">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td width="290">
                            If yes, does the insurance/reimbursment include prescription drugs?
                        </td>
                        <td style="width: 307px">
                            <asp:RadioButtonList ID="rbCoversRx" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="0">No</asp:ListItem>
                                <asp:ListItem Value="1">Yes</asp:ListItem>
                            </asp:RadioButtonList>
                            <asp:CustomValidator ID="CustomValidator5" runat="server" ClientValidationFunction="validateCoversRx"
                                ControlToValidate="rblstHealthInsurance" ErrorMessage="You must indicate if the insurance covers prescription drugs">*</asp:CustomValidator>
                        </td>
                    </tr>
                    <tr>
                        <td width="290">
                            If yes, does the insurance/reimbursment include cancer drugs?
                        </td>
                        <td style="width: 307px">
                            <asp:RadioButtonList ID="rbCoversCancerRx" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="No">No</asp:ListItem>
                                <asp:ListItem Value="Yes">Yes</asp:ListItem>
                            </asp:RadioButtonList>
                            <asp:CustomValidator ID="CustomValidator6" runat="server" ClientValidationFunction="validateCoversCancer"
                                ControlToValidate="rblstHealthInsurance" ErrorMessage="You must indicate if the insurance covers cancer drugs">*</asp:CustomValidator>
                        </td>
                    </tr>
                    <tr>
                        <td width="290">
                            If yes, does the insurance/reimbursment include Glivec?
                        </td>
                        <td style="width: 307px">
                            <asp:RadioButtonList ID="rbCoversGlivecRx" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="No">No</asp:ListItem>
                                <asp:ListItem Value="Yes">Yes</asp:ListItem>
                            </asp:RadioButtonList>
                            <asp:CustomValidator ID="CustomValidator7" runat="server" ClientValidationFunction="validateCoversGlivec"
                                ControlToValidate="rblstHealthInsurance" ErrorMessage="You must indicate if the insurance covers Glivec">*</asp:CustomValidator>
                        </td>
                    </tr>
                    <tr>
                        <td width="290">
                            Number of Household Members:
                        </td>
                        <td style="width: 307px">
                            <asp:DropDownList ID="dropHousehold" runat="server" Width="112px">
                                <asp:ListItem Value="Select Number">Select Number</asp:ListItem>
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
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td width="290">
                            Occupation of Financially Contributing Members (please list):
                        </td>
                        <td style="width: 307px">
                            <asp:TextBox ID="txtHouseholdOccupation" runat="server" Width="250px" TextMode="MultiLine"
                                Height="46px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td width="290">
                            Household's Annual Income (in US dollars):
                        </td>
                        <td style="width: 307px">
                            <asp:TextBox ID="txtHouseholdIncom" runat="server" Width="250px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td width="290">
                            <p>
                                Total of any additional funds received by the household (in US dollars):</p>
                        </td>
                        <td style="width: 307px">
                            <asp:TextBox ID="txtAdditionalFunds" runat="server" Width="250px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td width="290">
                            Approximate value of assets of the houshold (in US dollars):
                        </td>
                        <td style="width: 307px">
                            <asp:TextBox ID="txtAssets" runat="server" Width="250px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td width="290">
                        </td>
                        <td style="width: 307px">
                        </td>
                    </tr>
                </table>
                <table id="Table3" cellspacing="1" cellpadding="1" width="590" align="center" border="0">
                    <tr bgcolor="silver">
                        <td colspan="2">
                            <font color="steelblue" size="4">Recommendation:</font>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:RadioButtonList ID="rblstRecommendation" runat="server" 
                                RepeatDirection="Horizontal">
                                <asp:ListItem Value="Approve">Approve</asp:ListItem>
                                <asp:ListItem Value="Deny">Deny</asp:ListItem>
                                <asp:ListItem Value="Request further assessment by TMF Global">Request further assessment by TMF Global</asp:ListItem>
                                <asp:ListItem Value="Pending" Selected="True">Pending</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Case Summary (Required):
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtExplanation"
                                ErrorMessage="Case Summary">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtExplanation" runat="server" Width="100%" TextMode="MultiLine"
                                Height="52px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Panel ID="PanelReasonChange" runat="server">
                                Reason For Changes (Required): <font color="gray">500 characters max.</font>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtChangeReason"
                                    ErrorMessage="You must provide a Reason for the changes.">*</asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtChangeReason"
                                    ErrorMessage="Reason For Changes - Too many characters (500 max)" ValidationExpression="^[\s\S]{0,500}$">*</asp:RegularExpressionValidator>
                                <asp:TextBox ID="txtChangeReason" runat="server" Width="100%" TextMode="MultiLine"
                                    Height="52px"></asp:TextBox></asp:Panel>
                        </td>
                    </tr>
                </table>
                <table id="Table4" cellspacing="1" cellpadding="1" width="600" align="center" border="0">
                    <tr>
                        <td align="center" style="height: 26px">
                            <asp:Button ID="ButtonSub" runat="server" Width="75px" Text="Submit" OnClick="ButtonSub_Click">
                            </asp:Button>
                        </td>
                        <td align="center" style="height: 26px">
                            <asp:Button ID="ButtonCancel" runat="server" Width="75px" Text="Cancel" CausesValidation="False"
                                OnClick="ButtonCancel_Click"></asp:Button>
                        </td>
                    </tr>
                </table>
                </div>
            </asp:Panel>
            <asp:Panel ID="PanelNotes" runat="server">
                <asp:DataGrid ID="dgPatientInfo" runat="server" Width="600px">
                </asp:DataGrid>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script language="javascript">
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
    </script>
</asp:Content>
