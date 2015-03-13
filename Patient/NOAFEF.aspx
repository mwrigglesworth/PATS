<%@ Page Language="C#" MasterPageFile="~/Patient/GIPAPPatient.master" AutoEventWireup="true" CodeFile="NOAFEF.aspx.cs" Inherits="Patient_NOAFEF" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderControlPanel" Runat="Server">
    <table width="600">
<tr><td>
    <strong>NOA Patient Information |</strong>
    <asp:LinkButton ID="lbEdit" runat="server" OnClick="lbEdit_Click" 
        Visible="False" CausesValidation="false">Edit</asp:LinkButton>&nbsp;
    </td></tr>
    <tr><td>
        
    </td></tr>
    <tr><td>
        <asp:Panel ID="PanelMSContacted" runat="server" Visible="False">
            <table style="border: 1px solid #800000; width: 600" bgcolor="khaki" cellpadding="0">
                <tr>
                    <td><asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
<asp:UpdateProgress ID="UpdateProgress2" runat="server" DisplayAfter="0" DynamicLayout="true" AssociatedUpdatePanelID="UpdatePanel2">
        <ProgressTemplate><div style="width: 100%; background-color: White">
                        <img src="../Images/loading.gif" /></div>            
            </ProgressTemplate>
             </asp:UpdateProgress>
                            <table style="width: 100%;" border="0">
                            <tr><td><b><font color="maroon">Max Station Contact&nbsp;</font></b>
                                <asp:LinkButton ID="lbEditMsContact" runat="server" 
                                    onclick="lbEditMsContact_Click">edit</asp:LinkButton>
                                </td></tr>
                            <tr><td>
                                <asp:Label ID="LabelTMFInfo" runat="server" Font-Bold="False"></asp:Label>
                                </td></tr>
                                <tr>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Panel ID="PanelEditMsContact" runat="server" Visible=false>
                                            <table style="width:100%;">
                                            <tr><td>Has the patient been contacted by the Max Station about their NOA Approval?:</td></tr>
                                                <tr>
                                                    <td>
                                                        <asp:RadioButtonList ID="rblstMSContacted" runat="server" 
                                                            RepeatDirection="Horizontal" >
                                                            <asp:ListItem>No</asp:ListItem>
                                                            <asp:ListItem>Yes</asp:ListItem>
                                                        </asp:RadioButtonList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                                                            ControlToValidate="rblstMSContacted" 
                                                            ErrorMessage="Has the patient been contacted by the Max Station">Required</asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>What is the Payment Option Agreed Upon?:</td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:DropDownList ID="dropPaymentOption" runat="server" Enabled="False">
                                                            <asp:ListItem Value="0">Select One</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Button ID="ButtonSaveMSContact" runat="server" 
                                                            onclick="ButtonSaveMSContact_Click" Text="Save" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td></td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </td></tr>
    </table>
<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" >
<Triggers><asp:AsyncPostBackTrigger ControlID="lbEdit" EventName="Click" /></Triggers>
<ContentTemplate>
<asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="0" DynamicLayout="true" >
        <ProgressTemplate><div style="width: 100%; background-color: White">
                        <img src="../Images/loading.gif" /></div>         
            </ProgressTemplate>
             </asp:UpdateProgress>
<table width="600">
        <tr><td><asp:Panel ID="PanelOldFefs" runat="server" Visible="false">
            <table style="border: 1px solid Navy; background-color: LightCyan;" width="595">
                <tr>
                    <td>
                        <b><font color=navy>Past NOA FEFs</font></b></td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td width="50%">
                        Please select a past FEF to view:</td>
                    <td>
                        <asp:DropDownList ID="dropPastFEFs" runat="server" AutoPostBack="True" 
                            onselectedindexchanged="dropPastFEFs_SelectedIndexChanged" CausesValidation="false">
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
        </asp:Panel></td></tr>
        <tr><td>
<asp:Panel ID="PanelStartNewFEF" runat="server" Visible="False" Width="600px">
<table><tr><td width="100px"><img src="../Images/create.png" /></td><td>
    <asp:LinkButton ID="lbnewfef" runat="server" CssClass="FEFLinkButton" 
        onclick="lbnewfef_Click">Click here to start a new FEF</asp:LinkButton><br />
        <font class="Subtext">A new FEF needs to be filed in all cases, even if it is decided that the patient does not need to come in for assessment.</font></td></tr></table>
</asp:Panel></td></tr>
        <tr><td>
        <asp:Label ID="LabelFEFInfo" runat="server"></asp:Label>
            
            </td></tr>
</table>
<asp:Panel ID="PanelFixed" runat="server" Visible="False" Width="600px">
<table><tr><td width="100px"><img src="../Images/delete.png" /></td><td>
    <asp:LinkButton ID="lbFixed" runat="server" ForeColor="Red" CssClass="FEFLinkButton"
                onclick="lbFixed_Click" CausesValidation="False" >Click Here to mark this application Fixed Donation Length</asp:LinkButton><br />
                <font class="Subtext">Marking the application "Fixed" will bypass the Financial Evaluation and bring the patient directly to the Max Station queue for contact / payment option if applicable.</font></td></tr></table>
</asp:Panel>
<asp:Panel ID="PanelComplete" runat="server" Visible="False" Width="600px">
<table><tr><td width="100px"><img src="../Images/schedule.png" /></td><td>
    <h3>FEF Complete</h3>
    <font class="Subtext">You have completed the FEF for this patient.  The FEF is complete, and is set to expire on the following date:</font><br /><br />
    <asp:Label ID="LabelReassessdate" runat="server" Text="" CssClass="FEFLinkButton"></asp:Label>
    </td></tr></table>
</asp:Panel>
<asp:Panel ID="PanelNoAssessment" runat="server" Visible="False" Width="600px">
<table><tr><td width="100px"><img src="../Images/complete.png" /></td><td>
    <h3>FEF Complete</h3>
    <b>You have chosen NOT TO REQUIRE REASSESSMENT</b><br /><br />
    <font class="Subtext">You have completed the FEF for this patient. Since 
    reassessment is NOT required for this patient, the donation information on this 
    FEF will NOT change as long as the patient is in the program.</font>
    </td></tr></table>
</asp:Panel>
<asp:Panel ID="PanelDeny" runat="server" Visible="False" Width="600px">
<table><tr><td width="100px"><img src="../Images/deny.png" /></td><td>
    <h3>Deny</h3>
    <b>You have chosen to DENY this application.</b><br /><br />
    <font class="Subtext">This patient will be removed from your queue, and the application will be denied.  If you have chosen to deny in error, please click "edit" on the FEF page.</font>
    </td></tr></table>
</asp:Panel>
<asp:Panel ID="PanelPending" runat="server" Visible="False" Width="600px">
<table><tr><td width="100px"><img src="../Images/hourglass.png" /></td><td>
    <h3>FEF Pending</h3>
    <font class="Subtext">More input is required on your part to process this evaluation.  This patient will remain in your queue until a recommendation of Approve/Deny is made and a donation length is set.</font>
    </td></tr></table>
</asp:Panel>
<asp:Panel ID="PanelSecure" runat="server" Visible="False" Width="600px" DefaultButton="ButtonSecure">
<table><tr><td width="100px"><img src="../Images/secure.png" /></td><td>
    <h3>Password Required</h3>
    <font class="Subtext">As an added security measure, you are required to re-enter your password in order for these changes to take effect.</font>
    <br />
    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" Width="200px"></asp:TextBox>
    &nbsp;
    <asp:Button ID="ButtonSecure" runat="server" Text="Submit" 
        onclick="ButtonSecure_Click" />
    <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" 
        ControlToValidate="txtPassword" ErrorMessage="Password Required"></asp:RequiredFieldValidator>
    <br />
    <asp:Label ID="LabelPwordError" ForeColor="Red" runat="server" Text=""></asp:Label></td></tr></table>
</asp:Panel>
<asp:Panel ID="PanelEdit" runat="server" Visible="False" Width="600px">
    <table width="100%">
        <tr>
            <td width="50%">
                Address has been verified:<asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                    runat="server" ErrorMessage="Address has been verified" Text="*" ControlToValidate="rblstAddressVer"></asp:RequiredFieldValidator></td>
            <td>
                <asp:RadioButtonList ID="rblstAddressVer" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem>No</asp:ListItem>
                    <asp:ListItem>Yes</asp:ListItem>
                    <asp:ListItem >Waived</asp:ListItem>
                </asp:RadioButtonList></td>
        </tr>
         <tr>
            <td width="50%">
                Bank statement has been verified:<asp:RequiredFieldValidator 
                    ID="RequiredFieldValidator7" runat="server" ControlToValidate="rblstBankState" 
                    ErrorMessage="Bank Statement" Text="*"></asp:RequiredFieldValidator>
             </td>
            <td>
                <asp:RadioButtonList ID="rblstBankState" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem>No</asp:ListItem>
                    <asp:ListItem>Yes</asp:ListItem>
                    <asp:ListItem >Waived</asp:ListItem>
                </asp:RadioButtonList></td>
        </tr>
         <tr>
            <td colspan=2><hr /><font color=gray><b>FEF Documents</b></font>
            </td>
        </tr>
         <tr>
            <td width="50%">
                Patient Consent and Information Form:<asp:RequiredFieldValidator 
                    ID="RequiredFieldValidator8" runat="server" 
                    ControlToValidate="rblstPatientConsent" ErrorMessage="Patient consent form" 
                    Text="*"></asp:RequiredFieldValidator>
             </td>
            <td><asp:RadioButtonList ID="rblstPatientConsent" runat="server" RepeatDirection="Horizontal">
                <asp:ListItem>No</asp:ListItem>
                <asp:ListItem>Yes</asp:ListItem>
                <asp:ListItem >Waived</asp:ListItem>
            </asp:RadioButtonList></td>
        </tr>
         <tr>
            <td width="50%">
                Summary of Medical Chart:<asp:RequiredFieldValidator 
                    ID="RequiredFieldValidator9" runat="server" 
                    ControlToValidate="rblstMedicalChart" ErrorMessage="Medical Chart" Text="*"></asp:RequiredFieldValidator>
             </td>
            <td><asp:RadioButtonList ID="rblstMedicalChart" runat="server" RepeatDirection="Horizontal">
                <asp:ListItem>No</asp:ListItem>
                <asp:ListItem>Yes</asp:ListItem>
                <asp:ListItem >Waived</asp:ListItem>
            </asp:RadioButtonList></td>
        </tr>
         <tr>
            <td width="50%">
                Verification of Philadelphia Chromosome / BCR-Abl Test:<asp:RequiredFieldValidator 
                    ID="RequiredFieldValidator10" runat="server" ControlToValidate="rblstPhilPos" 
                    ErrorMessage="Philadelphia / BCR test" Text="*"></asp:RequiredFieldValidator>
             </td>
            <td><asp:RadioButtonList ID="rblstPhilPos" runat="server" 
                    RepeatDirection="Horizontal" >
                <asp:ListItem>No</asp:ListItem>
                <asp:ListItem>Yes</asp:ListItem>
                <asp:ListItem >Waived</asp:ListItem>
            </asp:RadioButtonList></td>
        </tr>
         <tr>
            <td width="50%">
                Verification of C-Kit Test Results:<asp:RequiredFieldValidator 
                    ID="RequiredFieldValidator11" runat="server" ControlToValidate="rblstCKit" 
                    ErrorMessage="C-Kit test" Text="*"></asp:RequiredFieldValidator>
             </td>
            <td><asp:RadioButtonList ID="rblstCKit" runat="server" RepeatDirection="Horizontal">
                <asp:ListItem>No</asp:ListItem>
                <asp:ListItem>Yes</asp:ListItem>
                <asp:ListItem >Waived</asp:ListItem>
            </asp:RadioButtonList></td>
        </tr>
         <tr>
            <td width="50%">
                Copy of Patient's ID:<asp:RequiredFieldValidator ID="RequiredFieldValidator12" 
                    runat="server" ControlToValidate="rblstPatientID" ErrorMessage="Copy of Id" 
                    Text="*"></asp:RequiredFieldValidator>
             </td>
            <td><asp:RadioButtonList ID="rblstPatientID" runat="server" RepeatDirection="Horizontal">
                <asp:ListItem>No</asp:ListItem>
                <asp:ListItem>Yes</asp:ListItem>
                <asp:ListItem >Waived</asp:ListItem>
            </asp:RadioButtonList></td>
        </tr>
         <tr>
            <td width="50%">
                Photo:<asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" 
                    ControlToValidate="rblstPhoto" ErrorMessage="Photo" Text="*"></asp:RequiredFieldValidator>
             </td>
            <td><asp:RadioButtonList ID="rblstPhoto" runat="server" RepeatDirection="Horizontal">
                <asp:ListItem>No</asp:ListItem>
                <asp:ListItem>Yes</asp:ListItem>
                <asp:ListItem >Waived</asp:ListItem>
            </asp:RadioButtonList></td>
        </tr>
         <tr>
            <td width="50%">
                Private Insurance:<asp:RequiredFieldValidator ID="RequiredFieldValidator14" 
                    runat="server" ControlToValidate="rblstPrivateInsurance" 
                    ErrorMessage="Private Insurance" Text="*"></asp:RequiredFieldValidator>
             </td>
            <td><asp:RadioButtonList ID="rblstPrivateInsurance" runat="server" RepeatDirection="Horizontal">
                <asp:ListItem>No</asp:ListItem>
                <asp:ListItem>Yes</asp:ListItem>
                <asp:ListItem >Waived</asp:ListItem>
            </asp:RadioButtonList></td>
        </tr>
         <tr>
            <td width="50%">
                Tax Return:<asp:RequiredFieldValidator ID="RequiredFieldValidator15" 
                    runat="server" ControlToValidate="rblstTaxReturn" ErrorMessage="Tax return" 
                    Text="*"></asp:RequiredFieldValidator>
             </td>
            <td><asp:RadioButtonList ID="rblstTaxReturn" runat="server" RepeatDirection="Horizontal">
                <asp:ListItem>No</asp:ListItem>
                <asp:ListItem>Yes</asp:ListItem>
                <asp:ListItem >Waived</asp:ListItem>
            </asp:RadioButtonList></td>
        </tr>
         <tr>
            <td width="50%">
                Income Verification Document(s):<asp:RequiredFieldValidator 
                    ID="RequiredFieldValidator16" runat="server" ControlToValidate="rblstIncomeVer" 
                    ErrorMessage="Income verification" Text="*"></asp:RequiredFieldValidator>
             </td>
            <td><asp:RadioButtonList ID="rblstIncomeVer" runat="server" RepeatDirection="Horizontal">
                <asp:ListItem>No</asp:ListItem>
                <asp:ListItem>Yes</asp:ListItem>
                <asp:ListItem >Waived</asp:ListItem>
            </asp:RadioButtonList></td>
        </tr>
         <tr>
            <td width="50%">
                Financial Declaration Form - included in enrollment kit:<asp:RequiredFieldValidator 
                    ID="RequiredFieldValidator17" runat="server" ControlToValidate="rblstFinDec" 
                    ErrorMessage="Financial declaration form" Text="*"></asp:RequiredFieldValidator>
             </td>
            <td><asp:RadioButtonList ID="rblstFinDec" runat="server" RepeatDirection="Horizontal">
                <asp:ListItem>No</asp:ListItem>
                <asp:ListItem>Yes</asp:ListItem>
                <asp:ListItem >Waived</asp:ListItem>
            </asp:RadioButtonList></td>
        </tr>
         <tr>
            <td width="50%">
                Utility / Phone Bill:<asp:RequiredFieldValidator ID="RequiredFieldValidator18" 
                    runat="server" ControlToValidate="rblstUtilPhone" 
                    ErrorMessage="Utility / Phone bill" Text="*"></asp:RequiredFieldValidator>
             </td>
            <td><asp:RadioButtonList ID="rblstUtilPhone" runat="server" RepeatDirection="Horizontal">
                <asp:ListItem>No</asp:ListItem>
                <asp:ListItem>Yes</asp:ListItem>
                <asp:ListItem >Waived</asp:ListItem>
            </asp:RadioButtonList></td>
        </tr>
         <tr>
            <td width="50%">
                Other Documents:</td>
            <td>
                <asp:TextBox ID="txtOtherDocs" runat="server" MaxLength="500" Width="250px"></asp:TextBox></td>
        </tr>
         <tr>
            <td colspan=2><hr /><font color=gray><b>Insurance Information</b></font>
            </td>
        </tr>
        <TR>
			<TD>Is the applicant eligible for health insurance/reimbursment?</TD>
			<TD>
				<asp:RadioButtonList id="rblstHealthInsurance" runat="server" RepeatDirection="Horizontal">
					<asp:ListItem Value="No">No</asp:ListItem>
					<asp:ListItem Value="Yes">Yes</asp:ListItem>
				</asp:RadioButtonList>
				<asp:RequiredFieldValidator id="RequiredFieldValidator3" runat="server" ControlToValidate="rblstHealthInsurance"
					ErrorMessage="You must indicate if the applicant is eligible for health insurance">*</asp:RequiredFieldValidator></TD>
		</TR>
		<TR>
			<TD>If yes, does the insurance/reimbursment include prescription drugs?</TD>
			<TD>
				<asp:RadioButtonList id="rbCoversRx" runat="server" RepeatDirection="Horizontal">
					<asp:ListItem Value="0">No</asp:ListItem>
					<asp:ListItem Value="1">Yes</asp:ListItem>
				</asp:RadioButtonList>
				<asp:CustomValidator id="CustomValidator5" runat="server" ClientValidationFunction="validateCoversRx"
					ControlToValidate="rblstHealthInsurance" ErrorMessage="You must indicate if the insurance covers prescription drugs">*</asp:CustomValidator></TD>
		</TR>
		<TR>
			<TD>If yes, does the insurance/reimbursment include cancer drugs?</TD>
			<TD>
				<asp:RadioButtonList id="rbCoversCancerRx" runat="server" RepeatDirection="Horizontal">
					<asp:ListItem Value="No">No</asp:ListItem>
					<asp:ListItem Value="Yes">Yes</asp:ListItem>
				</asp:RadioButtonList>
				<asp:CustomValidator id="CustomValidator6" runat="server" ClientValidationFunction="validateCoversCancer"
					ControlToValidate="rblstHealthInsurance" ErrorMessage="You must indicate if the insurance covers cancer drugs">*</asp:CustomValidator></TD>
		</TR>
		<TR>
			<TD>If yes, does the insurance/reimbursment include Glivec?</TD>
			<TD>
				<asp:RadioButtonList id="rbCoversGlivecRx" runat="server" RepeatDirection="Horizontal">
					<asp:ListItem Value="No">No</asp:ListItem>
					<asp:ListItem Value="Yes">Yes</asp:ListItem>
				</asp:RadioButtonList>
				<asp:CustomValidator id="CustomValidator7" runat="server" ClientValidationFunction="validateCoversGlivec"
					ControlToValidate="rblstHealthInsurance" ErrorMessage="You must indicate if the insurance covers Glivec">*</asp:CustomValidator></TD>
		</TR>
         <tr>
            <td colspan=2><hr /><font color=gray><b>NOA Information</b></font>
            </td>
        </tr>
        <tr><td>&nbsp;Re-assessment Required?:<asp:RequiredFieldValidator 
                ID="RequiredFieldValidator6" runat="server" 
                ControlToValidate="rblstYearlyReassess" 
                ErrorMessage="Reassessment Required?">*</asp:RequiredFieldValidator>
            </td><td>
            <asp:RadioButtonList ID="rblstYearlyReassess" runat="server" 
                RepeatDirection="Horizontal">
                <asp:ListItem>No</asp:ListItem>
                <asp:ListItem>Yes</asp:ListItem>
            </asp:RadioButtonList>
            </td></tr>
            <tr><td></td><td><font class="Subtext">Please note: If this is NOT a full donation 
                patient, a reassessment will be required</font></td></tr>
         <tr>
            <td width="50%">
                Recommendation:<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                    ControlToValidate="rblstRecommendation" ErrorMessage="Recommendation">*</asp:RequiredFieldValidator></td>
            <td>
                <asp:RadioButtonList ID="rblstRecommendation" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem>Pending</asp:ListItem>
                    <asp:ListItem>Approve</asp:ListItem>
                    <asp:ListItem>Deny</asp:ListItem>
                </asp:RadioButtonList></td>
        </tr>
         <tr>
            <td colspan=2>
                </td>
        </tr>
         <tr>
            <td width="50%">
                Donation Length:</td>
            <td>
                <asp:DropDownList ID="dropDonationLength" runat="server" >
                    <asp:ListItem Value="-1">Select One</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
         <tr>
            <td width="50%"></td>
            <td></td>
        </tr>
        <tr><td>
            &nbsp;</td><td >&nbsp;</td></tr>
        <tr><td colspan="2"><hr />
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="The following fields are missing:"
                ShowMessageBox="True" />
        </td></tr>
         <tr>
            <td width="50%">
                <asp:Button ID="ButtonSave" runat="server" Text="Save" Width="108px" OnClick="ButtonSave_Click" />&nbsp;
                <asp:Button ID="ButtonCancel" runat="server" CausesValidation="False" Text="Cancel" OnClick="ButtonCancel_Click" /></td>
            <td>
            </td>
        </tr>
         <tr>
            <td width="50%">
            </td>
            <td>
            </td>
        </tr>
    </table>
</asp:Panel>
<asp:Panel ID="PanelNote" runat="server">
    <p><asp:LinkButton ID="lbFEFNotes" runat="server" onclick="lbFEFNotes_Click">View FEF Notes</asp:LinkButton></p>
<asp:Panel ID="PanelFEFNotes" runat="server" Visible="false" >
<table width="600" border="0" cellpadding="1" cellspacing="1">
        <tr>
            <td width=100><b>New Note:</b></td><td>
            <asp:TextBox ID="txtNote" runat="server" TextMode="MultiLine" Width="400"></asp:TextBox>
            </td><td width=100>
                <asp:Button ID="ButtonAddNote" runat="server" onclick="ButtonAddNote_Click" 
                    Text="Add Note" />
            </td>
        </tr>
    </table>
    <table width="600" border="0">
        <tr>
            <td>
                <asp:Label ID="LabelNoteCount" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:DataGrid ID="GridViewNotes" runat="server" Width="595px">
                    <HeaderStyle BackColor="Silver" Font-Bold="true" />
                    <AlternatingItemStyle BackColor="Gainsboro" />
                </asp:DataGrid>
            </td>
        </tr>
        <tr>
            <td></td>
        </tr>
    </table>
            </asp:Panel>
    <p><asp:LinkButton ID="lbRequests" runat="server" onclick="lbRequests_Click">View Requests</asp:LinkButton></p>
<asp:Panel ID="PanelRequests" runat="server" Visible="false" >
    <table style="width: 100%;">
        <tr>
            <td>
                <asp:Label ID="LabelRequests" runat="server"></asp:Label></td>
        </tr>
    </table>
</asp:Panel>
    <p><asp:LinkButton ID="lbUpdates" runat="server" onclick="lbUpdates_Click">View FEF Updates</asp:LinkButton></p>
<asp:Panel ID="PanelUpdate" runat="server" Visible="false" >
    <table style="width: 100%;">
        <tr>
            <td>
                <asp:Label ID="LabelResultCountChanges" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td>
                <asp:DataGrid ID="GridViewUpdates" runat="server" Width="595px" >
                <HeaderStyle BackColor="Silver" Font-Bold=true />
                <AlternatingItemStyle BackColor="Gainsboro" />
               </asp:DataGrid>
            </td>
        </tr>
    </table>
</asp:Panel>

 </asp:Panel>
            </ContentTemplate>
</asp:UpdatePanel>
 <script language="javascript">
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