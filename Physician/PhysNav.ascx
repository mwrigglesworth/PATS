<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PhysNav.ascx.cs" Inherits="Physician_PhysNav" %>
<div style="float:left;"><span style="width:32px; float:left;">&nbsp;</span>
            <ul>
                <li><a href="">My Patients</a>
                    <ul>
                        <li><asp:Label ID="LabelApp" runat="server" Text=""></asp:Label></li>
                        <li><a href="../Physician/DataDisplay.aspx?dset=patientsneedingreapproval">ReApprove a Patient</a></li>
                        <li><a href="../Physician/DataDisplay.aspx?dset=closepatients">Close a Patient Case</a></li>
                        <li><a href="../Physician/DataDisplay.aspx?dset=dosechangepatients">Change Dosage</a></li>
                        <li><a href="../Physician/DataDisplay.aspx?dset=reactivatepatients">Reactivate a Closed Case</a></li>
                        <li><a href="../Physician/DataDisplay.aspx?dset=allpatients">View all my patients</a></li>
                    </ul>
                </li>
            </ul>
        <ul>
                <li><a href="">Reports</a>
                    <ul>
                        <li><a href="../Physician/DataDisplay.aspx?dset=saelog">Adverse Event Log</a></li>
                    </ul>
                </li>
            </ul>
            <ul>
                <li><a href="">Forms</a>
                    <ul>
                        <li><a href="../Physician/MOU.doc" target="_blank">MAX-Physician MOU</a></li>
                        <li><a href="../Physician/PatientConsent.doc" target="_blank">Patient Consent Form</a></li>
                        <li><a href="../Physician/PregnancyForm.doc" target="_blank">Patient Pregnancy Form</a></li>
                    </ul>
                </li>
            </ul>
            <ul>
                <li><a href="../Physician/ContactMAX.aspx"><font color="#C6DBEC">Contact MAX</font></a>
                </li>
                </ul>
            <ul>
                <li>
                    <asp:Label ID="LabelTasigna" runat="server" Text=""></asp:Label>
                </li>
            </ul>
            </div>
            <div style="width:330px; float:right; font-size:8pt; color:White;">                  
            <ul>
                <li><a href="" class="Queues">My Profile</a>
                    <ul>
                        <li><a href="../TMF/ChangePassword.aspx">Change My Password</a></li>
                        <li><a href="" id ="editSelf">Edit Profile</a></li>
                    </ul>
                </li>
            </ul>
            <ul>
                <li><a href="../Physician/Dashboard.aspx" class="Dashboard">Dashboard</a>
                </li>
            </ul>
                <asp:Label ID="LabelUser" runat="server" Text=""></asp:Label><br />
                <asp:LinkButton ID="lbLogOut" runat="server" Font-Size="8pt" 
                    onclick="lbLogOut_Click" CausesValidation="false">[Log Out]</asp:LinkButton>
            </div>