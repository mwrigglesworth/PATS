<%@ Page Language="C#" MasterPageFile="~/PATS.master" AutoEventWireup="true" CodeFile="ReapprovalRequests.aspx.cs" Inherits="TMF_ReapprovalRequests" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<div style="width:100%; clear:both; text-align: left;"><h1>Reapproval Requests</h1>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
    <div class="QueueDivHeader">
        <asp:Label ID="LabelResultCount" runat="server" Text=""></asp:Label></div><div class="QueueHeaderRight"></div>
        <div class="QueueDiv">
<asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="0" DynamicLayout="true" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
                        <img alt="..loading" src="../Images/loading.gif" /></ProgressTemplate>
             </asp:UpdateProgress><asp:GridView ID="dgReapps" runat="server" Width="915" AutoGenerateColumns="False" AlternatingRowStyle-BackColor="Gainsboro" BorderWidth="0px" GridLines="None">
                    <Columns>
								<asp:TemplateField Visible="False" HeaderText="PatientID">
									<ItemTemplate>
										<asp:Label ID="lblPatientID" Text='<%# DataBinder.Eval(Container.DataItem,"patientID") %>' Runat="server" />
									</ItemTemplate>
								</asp:TemplateField>
								<asp:TemplateField HeaderText="Process Reapproval?">
									<ItemStyle HorizontalAlign="Center" Width="80"></ItemStyle>
									<ItemTemplate>
										<asp:CheckBox ID="chkProcess" Runat="server" Width="55" />
									</ItemTemplate>
								</asp:TemplateField>
								<asp:BoundField DataField="manual" HeaderText="Process<br>Manually" HtmlEncode="false"></asp:BoundField>
								<asp:BoundField DataField="header" HeaderText="Patient" HtmlEncode="false"><ItemStyle Width=150 /></asp:BoundField>
								<asp:BoundField DataField="physicianname" HeaderText="Physician" HtmlEncode="false"></asp:BoundField>
								<asp:TemplateField HeaderText="Current Period"><ItemStyle Width="155" />
									<ItemTemplate>
									<asp:Label ID="lblCurrent" Text='<%# DataBinder.Eval(Container.DataItem,"currentperiod") %>' Runat="server" /><br /><br />
                                        <asp:Image ID="ImagePickedNotPickedUp" runat="server" ImageUrl="~/Images/NotPickedUP.png" Visible="false" />
									</ItemTemplate>
								</asp:TemplateField>
								<asp:BoundField DataField="request" HeaderText="Request" HtmlEncode="false"><ItemStyle Width=155 /></asp:BoundField>
								<asp:BoundField DataField="notes" HeaderText="Notes" HtmlEncode="false"></asp:BoundField>
							</Columns>
                    </asp:GridView>
    <br />
            <asp:Button ID="ButtonReapprove" runat="server" 
                Text="Reapprove Selected Patients" onclick="ButtonReapprove_Click" /><br />
            <asp:Label ID="LabelError" runat="server" Text="" Visible="false" ForeColor="Red"></asp:Label>
            <asp:Label ID="LabelReapprove" runat="server" 
                Text="Selected patients have been reapproved" Font-Italic="True" ForeColor="Gray" 
                Visible="False"></asp:Label></div>
    </ContentTemplate></asp:UpdatePanel>
    </div>
</asp:Content>

