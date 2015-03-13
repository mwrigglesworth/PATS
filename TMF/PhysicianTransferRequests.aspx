<%@ Page Language="C#" MasterPageFile="~/PATS.master" AutoEventWireup="true" CodeFile="PhysicianTransferRequests.aspx.cs" Inherits="TMF_PhysicianTransferRequests" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<div style="width:100%; clear:both; text-align: left;"><h1>Physician Transfer Requests</h1>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
    <div class="QueueDivHeader">
        <asp:Label ID="LabelResultCount" runat="server" Text=""></asp:Label></div><div class="QueueHeaderRight"></div>
        <div class="QueueDiv">
<asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="0" DynamicLayout="true" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate><img alt="..loading" src="../Images/loading.gif" /></ProgressTemplate>
             </asp:UpdateProgress><asp:GridView ID="dgRequests" runat="server" Width="915" AutoGenerateColumns="False" AlternatingRowStyle-BackColor="Gainsboro" BorderWidth="0px">
                    <Columns>
								<asp:TemplateField Visible="False" HeaderText="TransferID">
									<ItemTemplate>
										<asp:Label ID="lblTransferID" Text='<%# DataBinder.Eval(Container.DataItem,"transferRequestID") %>' Runat="server" />
									</ItemTemplate>
								</asp:TemplateField>
								<asp:TemplateField Visible="False" HeaderText="PatientID">
									<ItemTemplate>
										<asp:Label ID="lblPatientID" Text='<%# DataBinder.Eval(Container.DataItem,"patientID") %>' Runat="server" />
									</ItemTemplate>
								</asp:TemplateField>
								<asp:TemplateField HeaderText="Approve / Deny">
									<ItemStyle Width="100"></ItemStyle>
									<ItemTemplate>
										<asp:RadioButtonList ID="rblstApprove" runat=server><asp:ListItem>Approve</asp:ListItem><asp:ListItem>Deny</asp:ListItem></asp:RadioButtonList>
									</ItemTemplate>
								</asp:TemplateField>
								<asp:TemplateField HeaderText="Patient"><ItemStyle Width="180" />
									<ItemTemplate>
									<asp:Label ID="lblPatient" Text='<%# DataBinder.Eval(Container.DataItem,"Patient") %>' Runat="server" /><br />
                                    <asp:Panel ID="PanelPhysTrans" runat="server" Visible="false">
            <div class="AlertDiv" style="width:180px;">
                <asp:Label ID="LabelPhysTrans" runat="server" Text=""></asp:Label></div>
            </asp:Panel>
									</ItemTemplate>
								</asp:TemplateField>
								<asp:BoundField DataField="currentphysician" HeaderText="Current Physician" HtmlEncode="false"><ItemStyle Width=155 /></asp:BoundField>
								<asp:BoundField DataField="requestedphysician" HeaderText="Requested Physician" HtmlEncode="false"><ItemStyle Width=155 /></asp:BoundField>
								<asp:BoundField DataField="requestedby" HeaderText="Requested By" HtmlEncode="false"><ItemStyle Width=155 /></asp:BoundField>
							</Columns>
							</asp:GridView>
    <br />
            <asp:Button ID="ButtonProcess" runat="server" 
                Text="Transfer Selected Patients" onclick="ButtonProcess_Click" /><br />
            <asp:Label ID="LabelError" runat="server" Text="" Visible="false" ForeColor="Red"></asp:Label>
            <asp:Label ID="LabelRequest" runat="server" 
                Text="Selected patients have been transferred" Font-Italic="True" ForeColor="Gray" 
                Visible="False"></asp:Label></div>
    </ContentTemplate></asp:UpdatePanel>
    </div>
</asp:Content>

