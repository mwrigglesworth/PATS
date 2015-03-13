<%@ Page Title="" Language="C#" MasterPageFile="~/psapplication/MasterPage.master" AutoEventWireup="true" CodeFile="PSApplication.aspx.cs" Inherits="patientservices_PSApplication" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .style1
        {
            color: #FF0000;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="content" class="grid_12">
        <div id="center_content">
            <!-- BEGIN recent posts  -->
            <div class="recent">
                <!-- begin post -->
                <div class="post">
                    <h3>
                        Apply For Help: Registration Form</h3>
                    <p>
                        Through our Helpline, The Max Foundation offers <b>free programs</b>. Completing
                        this application will enable our team of advocates to follow up with you directly
                        and determine your program eligibility.</p>
                        <p><u>Please note that The Max Foundation does not offer direct financial assistance.  However, we will use our International Resource Database to make referrals for financial assistance, whenever possible.</u></p>
                    <ul>
                        <li><b>Patient Navigation:</b> A program for people anywhere in the world living with
                            CML and GIST. We assist by mapping your healthcare system, with the aim of access
                            to treatment. Advocates help with practical care and support by identifying resources
                            that match the individual needs of each patient and family.</li>
                        <li><b>Banding Together:</b> A program for people anywhere in the world living with
                            a blood cancer or rare cancer who have a contact in the US interested in helping
                            with fundraising on your behalf. Through this program, we help loved ones by providing
                            fundraising mentorship so they may raise money for your treatment-related expenses.</li>
                        <li><b>Patient Resource Matching:</b> A program linking blood cancer and rare cancer
                            survivors with relevant community resources, including clinical trial referrals,
                            referrals to clinics and physicians, as well as other individualized referrals.</li>
                    </ul>
                    <p>
                        To apply for help for yourself, please complete the Patient Information section.
                        If you are applying for someone else, please also complete the Caregiver Contact
                        Information. When you finish filling in the form, please choose submit.</p>
                    <p>
                        (<span class="style1">*</span> indicates required fields)</p>
                    <asp:Panel ID="PanelPrivacy" runat="server" Visible="false">
                        <div style="border: 1px solid maroon; background-color: khaki;">
                            <font color="maroon"><p>You must check that you have read the privacy practices at the bottom of the form in order to submit your application</p></font>
                        </div>
                    </asp:Panel>
                    <hr />
                    <p>
                        <b>HELPLINE PHONE NUMBERS: 1-888-462-9368, 425-778-8660 OR VIA EMAIL AT help@themaxfoundation.org.</b></p>
                    <h2>
                        Patient Information</h2>
                    <p>
                        <label for="author">
                            <small>Patients Given Name (First Name) <span class="style1">*</span></small></label><asp:RequiredFieldValidator
                                ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtPSFirstName"
                                ErrorMessage="Missing First Name" SetFocusOnError="True">*</asp:RequiredFieldValidator><br />
                        <asp:TextBox ID="txtPSFirstName" runat="server" size="60" TabIndex="1"></asp:TextBox>
                    </p>
                    <p>
                        <label for="author">
                            <small>Patients Family Name (Last Name) <span class="style1">*</span></small></label><asp:RequiredFieldValidator
                                ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtPSLastName"
                                ErrorMessage="Missing Last Name" SetFocusOnError="True">*</asp:RequiredFieldValidator><br />
                        <asp:TextBox ID="txtPSLastName" runat="server" size="60" TabIndex="1"></asp:TextBox>
                    </p>
                    <p>
                        <label for="author">
                            <small>Gender <span class="style1">*</span></small></label><asp:RequiredFieldValidator
                                ID="RequiredFieldValidator7" runat="server" ControlToValidate="rbPSGender" ErrorMessage="Missing Gender">*</asp:RequiredFieldValidator><br />
                        <asp:RadioButtonList ID="rbPSGender" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="Male">Male</asp:ListItem>
                            <asp:ListItem Value="Female">Female</asp:ListItem>
                        </asp:RadioButtonList>
                    </p>
                    <p>
                        <label for="author">
                            <small>Date of Birth </small>
                        </label>
                        <asp:Label ID="Labelinvalid" runat="server" Visible="False"></asp:Label>
                        <br />
                        <asp:DropDownList ID="cboBirthDay" runat="server">
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
                        <asp:DropDownList ID="cboBirthMonth" runat="server">
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
                        <asp:DropDownList ID="cboBirthYear" runat="server">
                        </asp:DropDownList>
                    </p>
                    <p>
                        <label for="author">
                            <small>Patient Address</small></label><br />
                        <asp:TextBox ID="txtPSStreet1" runat="server" size="60" TabIndex="1"></asp:TextBox>
                    </p>
                    <p>
                        <asp:TextBox ID="txtPSStreet2" runat="server" size="60" TabIndex="1"></asp:TextBox>
                    </p>
                    <p>
                        <label for="author">
                            <small>Patient City</small></label><br />
                        <asp:TextBox ID="txtPSCity" runat="server" size="40" TabIndex="1"></asp:TextBox>
                    </p>
                    <p>
                        <label for="author">
                            <small>Patient State/Province</small></label><br />
                        <asp:TextBox ID="txtPSState" runat="server" size="40" TabIndex="1"></asp:TextBox>
                    </p>
                    <p>
                        <label for="author">
                            <small>Patient Zip/Postal Code</small></label><br />
                        <asp:TextBox ID="txtPSPostal" runat="server" size="40" TabIndex="1"></asp:TextBox>
                    </p>
                    <p>
                        <label for="author">
                            <small>Patient Country <span class="style1">*</span></small></label><asp:CompareValidator
                                ID="CompareValidator2" runat="server" ControlToValidate="dropPSCountry" ErrorMessage="Missing Country"
                                ValueToCompare="0" Operator="NotEqual">*</asp:CompareValidator><br />
                        <asp:DropDownList ID="dropPSCountry" runat="server" TabIndex="12">
                        </asp:DropDownList>
                    </p>
                    <h2>
                        Please enter at least one of the four types of contact below <font color="red">*</font>
                    </h2>
                    <p>
                        <label for="author">
                            <small>Patient Phone Number</small></label><br />
                        <asp:TextBox ID="txtPSPhone" runat="server" size="40" TabIndex="1"></asp:TextBox>
                    </p>
                    <p>
                        <label for="author">
                            <small>Patient Mobile Number</small></label><br />
                        <asp:TextBox ID="txtMobile" runat="server" size="40" TabIndex="1"></asp:TextBox>
                    </p>
                    <p>
                        <label for="author">
                            <small>Patient Fax Number</small></label><br />
                        <asp:TextBox ID="txtPSFax" runat="server" size="40" TabIndex="1"></asp:TextBox>
                    </p>
                    <p>
                        <label for="author">
                            <small>Patient Email Address</small></label><br />
                        <asp:TextBox ID="txtPSEmail" runat="server" size="60" TabIndex="1"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtPSEmail"
                            ErrorMessage="Invalid Email" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator>
                    </p>
                    <h2>
                        Caregiver Information</h2>
                    <p>
                        If you are not the patient and are completing this form on behalf of the patient
                        listed above, please provide your contact information.</p>
                    <p>
                        <label for="author">
                            <small>Caregiver Given Name (First Name)</small></label><br />
                        <asp:TextBox ID="txtPSContactFirst" runat="server" size="60" TabIndex="1"></asp:TextBox>
                    </p>
                    <p>
                        <label for="author">
                            <small>Caregiver Famly Name (Last Name)</small></label><br />
                        <asp:TextBox ID="txtPSContactLast" runat="server" size="60" TabIndex="1"></asp:TextBox>
                    </p>
                    <h2>
                        Please enter at least one of the three types of contact below</h2>
                    <p>
                        <label for="author">
                            <small>Caregiver Phone Number</small></label><br />
                        <asp:TextBox ID="txtPSContactPhone" runat="server" size="40" TabIndex="1"></asp:TextBox>
                    </p>
                    <p>
                        <label for="author">
                            <small>Caregiver Fax Number</small></label><br />
                        <asp:TextBox ID="txtPSContactFax" runat="server" size="40" TabIndex="1"></asp:TextBox>
                    </p>
                    <p>
                        <label for="author">
                            <small>Caregiver Email Address</small></label><br />
                        <asp:TextBox ID="txtPSContactEmail" runat="server" size="60" TabIndex="1"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtPSEmail"
                            ErrorMessage="Invalid Email" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator>
                    </p>
                    <p>
                        <label for="author">
                            <small>Relationship to Patient</small></label><br />
                        <asp:DropDownList ID="dropPSRelationship" runat="server">
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
                            <asp:ListItem Value="Healthcare Professional">Healthcare Professional</asp:ListItem>
                            <asp:ListItem Value="Other">Other</asp:ListItem>
                        </asp:DropDownList>
                    </p>
                    <h2>
                        Patient Diagnosis Information</h2>
                    <p>
                        <label for="author">
                            <small>Diagnosis <span class="style1">*</span></small></label><asp:CompareValidator
                                ID="CompareValidator5" runat="server" ControlToValidate="dropDiagnosis" ErrorMessage="Missing Diagnosis"
                                ValueToCompare="0" Operator="NotEqual">*</asp:CompareValidator><br />
                        <asp:DropDownList ID="dropDiagnosis" TabIndex="5" runat="server">
                            <asp:ListItem Value="0" Selected="True">Select Disease</asp:ListItem>
                            <asp:ListItem>Blood Cancer</asp:ListItem>
                            <asp:ListItem>Acute Lymphocytic Leukemia (ALL)</asp:ListItem>
                            <asp:ListItem>Acute Myeloid Leukemia (AML)</asp:ListItem>
                            <asp:ListItem>Acute Promyelocytic Leukemia (APL)</asp:ListItem>
                            <asp:ListItem>Chronic Eosinophilic Leukemia (CEL)</asp:ListItem>
                            <asp:ListItem>Chronic Lymphocytic Leukemia (CLL)</asp:ListItem>
                            <asp:ListItem>Chronic Myelomonocytic Leukemia (CMML)</asp:ListItem>
                            <asp:ListItem Value="CML">Chronic Myeloid Leukemia (CML)</asp:ListItem>
                            <asp:ListItem>Hodgkin’s Lymphoma (HL)</asp:ListItem>
                            <asp:ListItem>Hypereosinophilic Syndrome (HES)</asp:ListItem>
                            <asp:ListItem>Multiple Myeloma (MM)</asp:ListItem>
                            <asp:ListItem>Myelodysplastic Syndromes (MDS/MPD)</asp:ListItem>
                            <asp:ListItem>Non-Hodgkin’s Lymphoma (NHL)</asp:ListItem>
                            <asp:ListItem>Systemic Mastocytosis (SM)</asp:ListItem>
                            <asp:ListItem>Rare Cancer</asp:ListItem>
                            <asp:ListItem>Dermatofibrosarcoma Protuberans (DFSP)</asp:ListItem>
                            <asp:ListItem Value="GIST">Gastrointestinal Stromal Tumors (GIST)</asp:ListItem>
                            <asp:ListItem>Other Cancer</asp:ListItem>
                        </asp:DropDownList>
                    </p>
                    <p>
                        <label for="author">
                            <small>Diagnosis Year</small></label><br />
                        <asp:DropDownList ID="dropDiagnosisYear" TabIndex="5" runat="server">
                        </asp:DropDownList>
                    </p>
                    <h2>
                        How Can We Help You?</h2>
                    <p>
                        Please indicate what your needs are.</p>
                    <p>
                        <label for="author">
                            <small>Patient Needs <span class="style1">*</span></small></label><asp:RequiredFieldValidator
                                ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtPSNeeds" ErrorMessage="Missing Patient Needs">*</asp:RequiredFieldValidator><br />
                        <asp:TextBox ID="txtPSNeeds" runat="server" Width="400px" Height="70px" TextMode="MultiLine"></asp:TextBox>
                    </p>
                    <p>
                        <label for="author">
                            <small>How did you hear about us? <span class="style1">*</span></small></label><asp:CompareValidator
                                ID="CompareValidator8" runat="server" ControlToValidate="dropFindOut" ErrorMessage="How did you hear about us?"
                                ValueToCompare="0" Operator="NotEqual">*</asp:CompareValidator><br />
                        <asp:DropDownList ID="dropFindOut" runat="server">
                            <asp:ListItem Value="0">Please select one</asp:ListItem>
                            <asp:ListItem>Cancer Support Organization</asp:ListItem>
                            <asp:ListItem>Friend or Family</asp:ListItem>
                            <asp:ListItem>Internet Search</asp:ListItem>
                            <asp:ListItem>Patient</asp:ListItem>
                            <asp:ListItem>Pharmaceutical Company</asp:ListItem>
                            <asp:ListItem>Physician</asp:ListItem>
                            <asp:ListItem>Social Worker</asp:ListItem>
                            <asp:ListItem>Other</asp:ListItem>
                        </asp:DropDownList>
                        &nbsp; If other, please specify:<asp:TextBox ID="txtHearOther" runat="server" size="40"
                            TabIndex="1"></asp:TextBox>
                        <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="How did you hear about us - other"
                            ClientValidationFunction="validateHearOther" ControlToValidate="dropFindOut">Required</asp:CustomValidator>
                    </p>
                    <h2>
                        Your privacy is important to us</h2>
                    <p>
                        Please read The MAX Foundation <a href="javascript:openNewWindow('PrivacyPractices.htm','thewin','height=500,width=500,toolbar=yes,scrollbars=yes')">
                            privacy practices</a> before submitting. When you have finished completing this
                        form, please check your information and then choose Submit Application</p>
                    <p>
                        <label for="author">
                            <asp:CheckBox ID="cbPrivacy" runat="server" Text="I have read and understood MAX’s Privacy Practices"
                                Checked="True" /><br />
                            <asp:Label ID="LabelPriv" runat="server" ForeColor="Red" Visible="false" Text="You must check that you have read the privacy practices to submit"></asp:Label>
                    </p>
                    <hr />
                    <p>
                        <asp:Button ID="ButtonPSSubmit" runat="server" Text="Submit Application" OnClick="ButtonPSSubmit_Click" Height="30px" Width="150px"></asp:Button>
                        <asp:Button ID="ButtonReset" runat="server" Text="Reset Application" CausesValidation="False" OnClick="ButtonReset_Click" Height="30px" Width="150px"></asp:Button>
                        <asp:Button ID="ButtonPSCancel" runat="server" Text="Cancel Application" CausesValidation="False" OnClick="ButtonPSCancel_Click" Height="30px" Width="150px"></asp:Button>
                    </p>
                </div>
            </div>
            <!-- END recent posts  -->
        </div>
        <!-- END content -->
    </div>
    <!-- end content inner -->
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="The following fields are missing"
        ShowMessageBox="True" ShowSummary="False" />
    <script language="javascript">
        //**********************************************************************************************************************
        function validateHearOther(sender, e) {
            if (e.Value == "Other") {
                if (ctl00_ContentPlaceHolder1_txtHearOther.value) {
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
    </label>
</asp:Content>
