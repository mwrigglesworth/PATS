<%@ Page Language="C#" MasterPageFile="~/PATS.master" AutoEventWireup="true" CodeFile="SentCaseNoteAlerts.aspx.cs" Inherits="TMF_SentCaseNoteAlerts" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<div style="width:100%; clear:both; text-align: left;">
<h1>Case Note Alerts I Sent</h1>
		<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
    <div class="QueueDivHeader">
        <asp:Label ID="LabelResultCount" runat="server" Text=""></asp:Label></div><div class="QueueHeaderRight"></div>
        <div class="QueueDiv">
<asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="0" DynamicLayout="true" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate><div class="ProgressDiv">
                        <img alt="..loading" src="../Images/loading.gif" /></div></ProgressTemplate>
             </asp:UpdateProgress><asp:GridView ID="dgReapps" runat="server" Width="915" AutoGenerateColumns="False" AlternatingRowStyle-BackColor="Gainsboro" BorderWidth="0px" GridLines="None" ShowHeader="false" OnRowCommand="dgReapps_RowCommand">
                    <Columns>
								<asp:TemplateField Visible="False" HeaderText="CaseNoteID">
									<ItemTemplate>
										<asp:Label ID="lblCaseNoteID" Text='<%# DataBinder.Eval(Container.DataItem,"casenoteID") %>' Runat="server" />
									</ItemTemplate>
								</asp:TemplateField>
								<asp:TemplateField Visible="False" HeaderText="PatientID">
									<ItemTemplate>
										<asp:Label ID="lblPatientID" Text='<%# DataBinder.Eval(Container.DataItem,"patientID") %>' Runat="server" />
									</ItemTemplate>
								</asp:TemplateField>
								<asp:TemplateField HeaderText="Alert" >
									<ItemTemplate>
									<asp:Label ID="lblAlert" Text='<%# DataBinder.Eval(Container.DataItem,"alert") %>' Runat="server" />
                                        <asp:Panel ID="PanelRemove" runat="server">
										<table><tr><td>
                                            <asp:TextBox ID="txtCnote" runat="server" Width="400"></asp:TextBox></td><td>
                                                <asp:Button ID="ButtonRemove" runat="server" Text="Remove" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                                </td></tr></table>
                                        </asp:Panel>
									</ItemTemplate>
								</asp:TemplateField>
							</Columns>
                    </asp:GridView></div>
    </ContentTemplate></asp:UpdatePanel>
    </div>
</asp:Content>

