<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FENav.ascx.cs" Inherits="FinancialEvaluator_FENav" %>
<div style="float:left;"><span style="width:32px; float:left;">&nbsp;</span>
            <ul>
                <li><a href="">NOA Patients</a>
                    <ul>
                        <li><a href="../FinancialEvaluator/DataDisplay.aspx?dset=listpatients&status=Active">Active</a></li>
                        <li><a href="../FinancialEvaluator/DataDisplay.aspx?dset=listpatients&status=Pending">Pending</a></li>
                        <li><a href="../FinancialEvaluator/DataDisplay.aspx?dset=listpatients&status=Closed">Closed</a></li>
                        <li><a href="../FinancialEvaluator/DataDisplay.aspx?dset=listpatients&status=Denied">Denied</a></li>
                    </ul>
                </li>
            </ul>
        <ul>
                <li><a href="">Action Requests</a>
                    <ul>
                        <li><a href="../FinancialEvaluator/DataDisplay.aspx?dset=sentrequests">Requests I Sent</a></li>
                        <li><a href="../FinancialEvaluator/DataDisplay.aspx?dset=resolvedrequests">Resolved Requests</a></li>
                    </ul>
                </li>
            </ul>
            </div>
            <div style="width:330px; float:right; font-size:8pt; color:White;">                  
            <ul>
                <li><a href="" class="Queues">My Profile</a>
                    <ul>
                        <li><a href="../TMF/ChangePassword.aspx">Change My Password</a></li>
                    </ul>
                </li>
            </ul>
            <ul>
                <li><a href="../FinancialEvaluator/Dashboard.aspx" class="Dashboard">Dashboard</a>
                </li>
            </ul>
                <asp:Label ID="LabelUser" runat="server" Text=""></asp:Label><br />
                <asp:LinkButton ID="lbLogOut" runat="server" Font-Size="8pt" 
                    onclick="lbLogOut_Click" CausesValidation="false">[Log Out]</asp:LinkButton>
            </div>