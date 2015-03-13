<%@ Control Language="C#" AutoEventWireup="true" CodeFile="NovartisNav.ascx.cs" Inherits="NovartisUser_NovartisNav" %>
<div style="float:left;"><span style="width:32px; float:left;">&nbsp;</span>
            <ul>
                <li><a href="">View MAX Reports</a>
                    <ul>
                        <asp:Label ID="LabelCountries" runat="server" Text=""></asp:Label>
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
                <li><a href="../NovartisUser/Dashboard.aspx" class="Dashboard">Dashboard</a>
                </li>
            </ul>
                <asp:Label ID="LabelUser" runat="server" Text=""></asp:Label><br />
                <asp:LinkButton ID="lbLogOut" runat="server" Font-Size="8pt" 
                    onclick="lbLogOut_Click" CausesValidation="false">[Log Out]</asp:LinkButton>
            </div>