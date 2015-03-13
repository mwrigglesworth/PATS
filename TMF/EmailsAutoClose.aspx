<%@ Page Language="C#" MasterPageFile="~/PATS.master" AutoEventWireup="true" CodeFile="EmailsAutoClose.aspx.cs" Inherits="TMF_EmailsAutoClose" %>
<%@ Register assembly="iucon.web.Controls.PartialUpdatePanel" namespace="iucon.web.Controls" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<div style="width:100%; clear:both; text-align: left;"><h1>Emails / Auto Closures - 
    <asp:Label ID="LabelPOName" runat="server" Text=""></asp:Label></h1>
<p><asp:DropDownList ID="dropOtherUsers" runat="server" AutoPostBack="True" 
                onselectedindexchanged="dropOtherUsers_SelectedIndexChanged">
            </asp:DropDownList></p>
           
    <asp:Panel ID="PanelPatientReminders" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>

    <div class="QueueDivHeader">
        <asp:Label ID="LabelResultCountPatientReminders" runat="server" Text=""></asp:Label></div><div class="QueueHeaderRight"></div>
        <div class="QueueDiv"><asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="0" DynamicLayout="true" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
                        <img alt="..loading" src="../Images/loading.gif" /></ProgressTemplate>
             </asp:UpdateProgress>
            <asp:Button ID="ButtonSelectAllPatientReminders" runat="server" 
                Text="Select All Reminders" onclick="ButtonSelectAllPatientReminders_Click" />
             <asp:GridView ID="dgPatientReminders" runat="server" 
                Width="925" AutoGenerateColumns="False" 
                AlternatingRowStyle-BackColor="Gainsboro" BorderWidth="0px" GridLines="None" HeaderStyle-BackColor="Gainsboro">
                    <Columns>
								<asp:TemplateField Visible="False" HeaderText="PatientID">
									<ItemTemplate>
										<asp:Label ID="lblPatientID" Text='<%# DataBinder.Eval(Container.DataItem,"patientID") %>' Runat="server" />
									</ItemTemplate>
								</asp:TemplateField>
								<asp:TemplateField Visible="False" HeaderText="RequestID">
									<ItemTemplate>
										<asp:Label ID="lblErrorName" Text='<%# DataBinder.Eval(Container.DataItem,"ErrorName") %>' Runat="server" />
									</ItemTemplate>
								</asp:TemplateField>
								<asp:TemplateField HeaderText="Send Email?">
									<ItemStyle HorizontalAlign="Center" Width="80"></ItemStyle>
									<ItemTemplate>
										<asp:CheckBox ID="chkProcess" Runat="server" Width="55" />
									</ItemTemplate>
								</asp:TemplateField>
								<asp:TemplateField HeaderText="Remove">
									<ItemStyle Width="100"></ItemStyle>
									<ItemTemplate>
                                        <asp:ImageButton ID="ButtonRemove" runat="server" ImageUrl="../Images/RemoveSmall.png" OnClick = "Button1_Click" />	
                                        <asp:Button ID="ButtonRemove2" runat="server" Text="REMOVE"  OnClick = "Button2PatientReminders_Click" BackColor="Red" Visible="false" CommandArgument="PatientReminders" />
									</ItemTemplate>
								</asp:TemplateField>
								<asp:BoundField DataField="SendManually" HeaderText="Send Manually" HtmlEncode="false"><ItemStyle Width="200"></ItemStyle></asp:BoundField>
								<asp:BoundField DataField="Patient" HeaderText="Patient" HtmlEncode="false"></asp:BoundField>
							</Columns>
                    </asp:GridView>
    <br />
            <asp:Button ID="ButtonSendPatientReminders" runat="server" 
                Text="Send Selected Patient Reminders" 
                onclick="ButtonSendPatientReminders_Click" /><br />
            <asp:Label ID="LabelPatientReminderError" runat="server" Text="" Visible="false" ForeColor="Red"></asp:Label>
            <asp:Label ID="LabelPatientReminders" runat="server" 
                Text="Selected patient reminders have been sent" Font-Italic="True" ForeColor="Gray" 
                Visible="False"></asp:Label></div>
    </ContentTemplate></asp:UpdatePanel>
    <div class="LeftColSpacer"></div>
    </asp:Panel>
    <asp:Panel ID="PanelSecondNotices" runat="server">
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
<ContentTemplate>

    <div class="QueueDivHeader">
        <asp:Label ID="LabelResultCountSecondNotices" runat="server" Text=""></asp:Label></div><div class="QueueHeaderRight"></div>
        <div class="QueueDiv"><asp:UpdateProgress ID="UpdateProgress2" runat="server" DisplayAfter="0" DynamicLayout="true" AssociatedUpdatePanelID="UpdatePanel2">
        <ProgressTemplate>
                        <img alt="..loading" src="../Images/loading.gif" /></ProgressTemplate>
             </asp:UpdateProgress>
            <asp:Button ID="ButtonSelectAllSecondNotices" runat="server" 
                Text="Select All Second Notices" 
                onclick="ButtonSelectAllSecondNotices_Click" />
             <asp:GridView ID="dgSecondNotices" runat="server" 
                Width="925" AutoGenerateColumns="False" 
                AlternatingRowStyle-BackColor="Gainsboro" BorderWidth="0px" GridLines="None" HeaderStyle-BackColor="Gainsboro">
                    <Columns>
								<asp:TemplateField Visible="False" HeaderText="PatientID">
									<ItemTemplate>
										<asp:Label ID="lblPatientID" Text='<%# DataBinder.Eval(Container.DataItem,"patientID") %>' Runat="server" />
									</ItemTemplate>
								</asp:TemplateField>
								<asp:TemplateField Visible="False" HeaderText="RequestID">
									<ItemTemplate>
										<asp:Label ID="lblErrorName" Text='<%# DataBinder.Eval(Container.DataItem,"ErrorName") %>' Runat="server" />
									</ItemTemplate>
								</asp:TemplateField>
								<asp:TemplateField HeaderText="Send Email?">
									<ItemStyle HorizontalAlign="Center" Width="80"></ItemStyle>
									<ItemTemplate>
										<asp:CheckBox ID="chkProcess" Runat="server" Width="55" />
									</ItemTemplate>
								</asp:TemplateField>
								<asp:TemplateField HeaderText="Remove">
									<ItemStyle Width="100"></ItemStyle>
									<ItemTemplate>
                                        <asp:ImageButton ID="ButtonRemove" runat="server" ImageUrl="../Images/RemoveSmall.png" OnClick = "Button1_Click" />	
                                        <asp:Button ID="ButtonRemove2" runat="server" Text="REMOVE"  OnClick = "Button2SecondNotices_Click" BackColor="Red" Visible="false" CommandArgument="PatientReminders" />
									</ItemTemplate>
								</asp:TemplateField>
								<asp:BoundField DataField="SendManually" HeaderText="Send Manually" HtmlEncode="false"><ItemStyle Width="200"></ItemStyle></asp:BoundField>
								<asp:BoundField DataField="Patient" HeaderText="Patient" HtmlEncode="false"></asp:BoundField>
							</Columns>
                    </asp:GridView>
    <br />
            <asp:Button ID="ButtonSendSecondNotices" runat="server" 
                Text="Send Selected Patient Second Notices" 
                onclick="ButtonSendSecondNotices_Click" /><br />
            <asp:Label ID="LabelSecondNoticesError" runat="server" Text="" Visible="false" ForeColor="Red"></asp:Label>
            <asp:Label ID="LabelSecondNotices" runat="server" 
                Text="Selected patient second notices have been sent" Font-Italic="True" ForeColor="Gray" 
                Visible="False"></asp:Label></div>
    </ContentTemplate></asp:UpdatePanel>
    <div class="LeftColSpacer"></div>
    </asp:Panel>
    <asp:Panel ID="PanelPhysicianReminders" runat="server">
    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
<ContentTemplate>

    <div class="QueueDivHeader">
        <asp:Label ID="LabelResultCountPhysicianReminders" runat="server" Text=""></asp:Label></div><div class="QueueHeaderRight"></div>
        <div class="QueueDiv"><asp:UpdateProgress ID="UpdateProgress3" runat="server" DisplayAfter="0" DynamicLayout="true" AssociatedUpdatePanelID="UpdatePanel3">
        <ProgressTemplate>
                        <img alt="..loading" src="../Images/loading.gif" /></ProgressTemplate>
             </asp:UpdateProgress>
            <asp:Button ID="ButtonSelectAllPhysicianReminders" runat="server" 
                Text="Select All Reminders" onclick="ButtonSelectAllPhysicianReminders_Click" />
             <asp:GridView ID="dgPhysicianReminders" runat="server" 
                Width="925" AutoGenerateColumns="False" 
                AlternatingRowStyle-BackColor="Gainsboro" BorderWidth="0px" GridLines="None" HeaderStyle-BackColor="Gainsboro">
                    <Columns>
								<asp:TemplateField Visible="False" HeaderText="PhysicianID">
									<ItemTemplate>
										<asp:Label ID="lblPhysicianID" Text='<%# DataBinder.Eval(Container.DataItem,"personID") %>' Runat="server" />
									</ItemTemplate>
								</asp:TemplateField>
								<asp:TemplateField Visible="False" HeaderText="RequestID">
									<ItemTemplate>
										<asp:Label ID="lblErrorName" Text='<%# DataBinder.Eval(Container.DataItem,"ErrorName") %>' Runat="server" />
									</ItemTemplate>
								</asp:TemplateField>
								<asp:TemplateField HeaderText="Send Email?">
									<ItemStyle HorizontalAlign="Center" Width="80"></ItemStyle>
									<ItemTemplate>
										<asp:CheckBox ID="chkProcess" Runat="server" Width="55" />
									</ItemTemplate>
								</asp:TemplateField>
								<asp:TemplateField HeaderText="Remove">
									<ItemStyle Width="100"></ItemStyle>
									<ItemTemplate>
                                        <asp:ImageButton ID="ButtonRemove" runat="server" ImageUrl="../Images/RemoveSmall.png" OnClick = "Button1_Click" />	
                                        <asp:Button ID="ButtonRemove2" runat="server" Text="REMOVE"  OnClick = "Button2PhysicianReminders_Click" BackColor="Red" Visible="false" CommandArgument="PhysicianReminders" />
									</ItemTemplate>
								</asp:TemplateField>
								<asp:BoundField DataField="PhysicianName" HeaderText="Physician" HtmlEncode="false"></asp:BoundField>
							</Columns>
                    </asp:GridView>
    <br />
            <asp:Button ID="ButtonSendPhysicianReminders" runat="server" 
                Text="Send Selected Physician Reminders" 
                onclick="ButtonSendPhysicianReminders_Click" /><br />
            <asp:Label ID="LabelPhysicianReminderError" runat="server" Text="" Visible="false" ForeColor="Red"></asp:Label>
            <asp:Label ID="LabelPhysicianReminders" runat="server" 
                Text="Selected Physician reminders have been sent" Font-Italic="True" ForeColor="Gray" 
                Visible="False"></asp:Label></div>
    </ContentTemplate></asp:UpdatePanel>
    <div class="LeftColSpacer"></div>
    </asp:Panel>
    <asp:Panel ID="PanelAutoClose" runat="server">
    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
<ContentTemplate>

    <div class="QueueDivHeader">
        <asp:Label ID="LabelResultCountAutoClose" runat="server" Text=""></asp:Label></div><div class="QueueHeaderRight"></div>
        <div class="QueueDiv"><asp:UpdateProgress ID="UpdateProgress4" runat="server" DisplayAfter="0" DynamicLayout="true" AssociatedUpdatePanelID="UpdatePanel4">
        <ProgressTemplate>
                        <img alt="..loading" src="../Images/loading.gif" /></ProgressTemplate>
             </asp:UpdateProgress>
            <asp:Button ID="ButtonSelectAllAutoClose" runat="server" 
                Text="Select All Patients" onclick="ButtonSelectAllAutoClose_Click" />
             <asp:GridView ID="dgAutoClose" runat="server" 
                Width="925" AutoGenerateColumns="False" 
                AlternatingRowStyle-BackColor="Gainsboro" BorderWidth="0px" GridLines="None" HeaderStyle-BackColor="Gainsboro">
                    <Columns>
								<asp:TemplateField Visible="False" HeaderText="PatientID">
									<ItemTemplate>
										<asp:Label ID="lblPatientID" Text='<%# DataBinder.Eval(Container.DataItem,"patientID") %>' Runat="server" />
									</ItemTemplate>
								</asp:TemplateField>
								<asp:TemplateField Visible="False" HeaderText="RequestID">
									<ItemTemplate>
										<asp:Label ID="lblErrorName" Text='<%# DataBinder.Eval(Container.DataItem,"ErrorName") %>' Runat="server" />
									</ItemTemplate>
								</asp:TemplateField>
								<asp:TemplateField HeaderText="Send Email?">
									<ItemStyle HorizontalAlign="Center" Width="80"></ItemStyle>
									<ItemTemplate>
										<asp:CheckBox ID="chkProcess" Runat="server" Width="55" />
									</ItemTemplate>
								</asp:TemplateField>
								<asp:TemplateField HeaderText="Remove">
									<ItemStyle Width="100"></ItemStyle>
									<ItemTemplate>
                                        <asp:ImageButton ID="ButtonRemove" runat="server" ImageUrl="../Images/RemoveSmall.png" OnClick = "Button1_Click" />	
                                        <asp:Button ID="ButtonRemove2" runat="server" Text="REMOVE"  OnClick = "Button2AutoClose_Click" BackColor="Red" Visible="false" />
									</ItemTemplate>
								</asp:TemplateField>
								<asp:BoundField DataField="Patient" HeaderText="Patient" HtmlEncode="false"></asp:BoundField>
								<asp:BoundField DataField="EndDate" HeaderText="End Date" HtmlEncode="false"></asp:BoundField>
							</Columns>
                    </asp:GridView>
    <br />
            <asp:Button ID="ButtonAutoClose" runat="server" 
                Text="Close Selected Patients" onclick="ButtonAutoClose_Click" /><br />
            <asp:Label ID="LabelAutoCloseError" runat="server" Text="" Visible="false" ForeColor="Red"></asp:Label>
            <asp:Label ID="LabelAutoClose" runat="server" 
                Text="Selected patients have been closed" Font-Italic="True" ForeColor="Gray" 
                Visible="False"></asp:Label></div>
    </ContentTemplate></asp:UpdatePanel>
    <div class="LeftColSpacer"></div>
    </asp:Panel>
</asp:Content>

