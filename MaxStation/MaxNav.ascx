<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MaxNav.ascx.cs" Inherits="MaxStation_MaxNav" %>
<div style="float:left;"><span style="width:32px; float:left;">&nbsp;</span>
            <ul>
                <li><a href="">Countries</a>
                    <ul>
                        <asp:Label ID="LabelCountries" runat="server" Text=""></asp:Label>
                    </ul>
                </li>
            </ul>
        <ul>
                <li><a href="">Search</a>
                    <ul>
                        <li><a href="../Patient/PatientSearch.aspx">Patients</a></li>
                        <li><a href="../Physician/PhysicianSearch.aspx">Physicians</a></li>
                        <li><a href="../Clinic/ClinicSearch.aspx">Clinics</a></li>
                    </ul>
                </li>
            </ul>
            <ul>
                <li><a href="">Add</a>
                    <ul>
                        <li><a href="../Application/GIPAPApplication.aspx">Patient</a></li>
                        <li><a href="../Physician/AddPhysician.aspx">Physician</a></li>
                    </ul>
                </li>
            </ul>
            <ul>
                <li><a href="">Reports</a>
                    <ul>
                        <li><a href="../Reports/ActivityReport.aspx">Program Activity</a></li>
                        <li><a href="../Reports/TotalsReport.aspx">Patient Totals</a></li>
                    </ul>
                </li>
            </ul> 
            </div>
            <div style="width:330px; float:right; font-size:8pt; color:White;">                  
            <ul>
                <li><a href="../MaxStation/Dashboard.aspx" class="Queues">My Queues</a>
                    <ul>
                        <li><h3>All Users</h3><a href="../MaxStation/DataDisplay.aspx?dset=pendingpatients">Pending Patients</a></li>
                        <li><a href="../MaxStation/DataDisplay.aspx?dset=pastduepatients">Past Due</a></li>
                        <li><a href="../MaxStation/DataDisplay.aspx?dset=patientsneedingreapproval">Needing Reapproval</a></li>
                        <li><a href="../MaxStation/ActivePatients.aspx">Active Patients</a></li>
                        <li><a href="../MaxStation/ApplicationInfo.aspx">Web Applications</a></li>
                        <li><a href="../MaxStation/DataDisplay.aspx?dset=clinics">My Clinics</a></li>
                        <li><a href="../MaxStation/DataDisplay.aspx?dset=physicians">My Physicians</a></li>
                        <li><h3>NOA Queues</h3><a href="../MaxStation/DataDisplay.aspx?dset=NOAPending">NOA Pending</a></li>
                        <li><a href="../MaxStation/DataDisplay.aspx?dset=unassignedNOApending">Pending - No Branch</a></li>
                        <li><a href="../MaxStation/DataDisplay.aspx?dset=NOAreactivationrequestsNoBranch">Reactivation - No Branch</a></li>
                        <li><a href="../MaxStation/DataDisplay.aspx?dset=NOAreassessmentrequestsNoBranch">Reassessment - No Branch</a></li>
                        <li><a href="../MaxStation/DataDisplay.aspx?dset=YearlyReassessment">Reassessment</a></li>
                    </ul>
                </li>
            </ul>
            <ul>
                <li><a href="../MaxStation/Dashboard.aspx" class="Dashboard">Dashboard</a>
                </li>
            </ul>
                <asp:Label ID="LabelUser" runat="server" Text=""></asp:Label><br />
                <asp:LinkButton ID="lbLogOut" runat="server" Font-Size="8pt" 
                    onclick="lbLogOut_Click" CausesValidation="false">[Log Out]</asp:LinkButton>
            </div>