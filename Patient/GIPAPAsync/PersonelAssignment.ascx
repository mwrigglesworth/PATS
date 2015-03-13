<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PersonelAssignment.ascx.cs" Inherits="Patient_PersonelAssignment" %>
<asp:Panel ID="PanelDisplay" runat="server">
<input type="hidden" id="senderid" value="<%=choice%>" />
<input type="hidden" id="sendertype" value="Patient" />
<input type="hidden" id="user" value="<%=userRole%>" />
<input type="hidden" id="showdist" value="<%=showDistributor%>" />
    <div class="PersonelDiv">
        <div class="PersonelCol">
            Max Stations
            <%if (privUser)
              { %><a href="" class="editPeopleList" id="MaxStation">[+/-]</a><%} %><br />
            <asp:Label ID="LabelMS" runat="server" Text=""></asp:Label></div>
        <div class="PersonelCol">
            Physicians
            <%if (privUser && userRole == "TMFUser")
              { %><a href="" class="editPeopleList"  id="Physician">[+/-]</a><%} 
               else if  (privUser && userRole == "MaxStation")
              { %><a href="PhysicianTransferRequest.aspx?choice=<%=choice %>&Sender=Patient&AddType=Physician" class="lbAR"  id="A1">[+/-]</a><%} %>
            <%--<asp:HyperLink ID="hlPhys" runat="server" CssClass="lbAR" Visible="false">[+/-]</asp:HyperLink>--%>
            <asp:Label ID="LabelPhys" runat="server" Text=""></asp:Label>
            <asp:Panel ID="PanelPhysTrans" runat="server" Visible="false">
            <div class="AlertDiv" id="PhysTransDiv" style="width:180px;">
                <asp:Label ID="LabelPhysTrans" runat="server" Text=""></asp:Label></div>
            </asp:Panel><br />
            Contacts
            <%if (privUser)
              { %><a href="" id="addContact">[add]</a><%} %>
            <br />
            <asp:Label ID="LabelContacts" runat="server" Text=""></asp:Label></div>
        <div class="PersonelCol">
            Program Officers
            <%if (privUser)
              { %><a href=""  class="editPeopleList" id="TMFUser">[+/-]</a><%} %><br />
            <asp:Label ID="LabelPO" runat="server" Text=""></asp:Label><br />
            <%if (!showDistributor)
              { %>
            <div id="branchdiv">
            <asp:Label ID="LabelBranchHeader" runat="server" Text="FE Branch"></asp:Label>
            <%if (privUser)
              { %><a href="" class="editPeopleList" id="FEBranch">[+/-]</a><%} %>
            <asp:Label ID="LabelBranch" runat="server" Text=""></asp:Label>
            <asp:Panel ID="PanelBranchAssign" runat="server" Visible="false">
            <div class="AlertDiv" style="width:180px;">
                <asp:Label ID="LabelBranchAssign" runat="server" Text=""></asp:Label></div>
            </asp:Panel></div>
            <%}
              else
              { %>
            <div id="distributordiv">
            <asp:Label ID="lblDistributorHeader" runat="server" Text="Distributor"></asp:Label>
           <%if (privUser)
              { %><a href="" class="editPeopleList"  id="Distributor" >[+/-]</a><%} %><br />
            <asp:Label ID="LabelDistributor" runat="server" Text=""></asp:Label>
            <asp:Panel ID="PanelDistributorAssign" runat="server" Visible="false">
            <div class="AlertDiv" style="width:180px;">
                <asp:Label ID="LabelDistributorAssign" runat="server" Text=""></asp:Label></div>
            </asp:Panel></div>
            <%} %>
            </div>
    </div>
</asp:Panel>
<%--<asp:Panel ID="PanelUpdate" runat="server" Visible="false">
    <div class="PersonelUpdateDiv">
        <h2><asp:Label ID="LabelHeader" runat="server" Text=""></asp:Label></h2>
        <asp:Panel ID="PanelGroup" runat="server" Visible="false">
            <div class="PersonelCol">Max Station Groups:<br />
            <asp:RadioButtonList ID="rblstGroups" runat="server" 
                AutoPostBack="True" OnSelectedIndexChanged="rblstGroups_SelectedIndexChanged">
            </asp:RadioButtonList></div>
            <div class="PersonelCol">
                <asp:Label ID="LabelSuggestions" runat="server" Text=""></asp:Label></div><hr style="clear:both;" />
        </asp:Panel>
        <asp:Panel ID="PanelStockist" runat="server" Visible="false">
        <div class="PersonelColPhy">Stockist:<br />
            <asp:RadioButtonList ID="rblstStockist" runat="server">
            </asp:RadioButtonList>            
        </div>
        <div class="PersonelCol">
                <asp:Label ID="LabelStockist" runat="server" Text="" ForeColor="Red"></asp:Label></div><hr style="clear:both;" />
        </asp:Panel>
        <div style="clear:both;">
        <asp:CheckBoxList ID="cblstPersonel" runat="server" RepeatColumns="2">
        </asp:CheckBoxList></div>
        <hr />
        <asp:Button ID="ButtonUpdateMS" runat="server" Text="Update Max Stations" Visible="false"
            OnClick="ButtonUpdateMS_Click" />
        <asp:Button ID="ButtonUpdatePhys" runat="server" Text="Update Physicians" 
            Visible="false" onclick="ButtonUpdatePhys_Click" />
        <asp:Button ID="ButtonUpdatePO" runat="server" Text="Update Program Officers" 
            Visible="false" onclick="ButtonUpdatePO_Click" />
        <asp:Button ID="ButtonUpdateBranch" runat="server" Text="Update Branch / Stockist" 
            Visible="false" onclick="ButtonUpdateBranch_Click" />
    </div>
</asp:Panel>--%>
