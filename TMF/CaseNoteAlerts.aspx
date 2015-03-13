<%@ Page Language="C#" MasterPageFile="~/PATS.master" AutoEventWireup="true" CodeFile="CaseNoteAlerts.aspx.cs" Inherits="TMF_CaseNoteAlerts" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<div style="width:100%; clear:both; text-align: left;">
<h1>Case Note Alerts</h1>
		<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
    <div class="QueueDivHeader">
        <asp:Label ID="LabelResultCount" runat="server" Text=""></asp:Label></div><div class="QueueHeaderRight"></div>
        <div class="QueueDiv">
<asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="0" DynamicLayout="true" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate><div class="ProgressDiv">
                        <img alt="..loading" src="../Images/loading.gif" /></div></ProgressTemplate>
             </asp:UpdateProgress><asp:GridView ID="dgReapps" runat="server" Width="915" AutoGenerateColumns="False" AlternatingRowStyle-BackColor="Gainsboro" BorderWidth="0px" GridLines="None">
                    <Columns>
								<asp:TemplateField Visible="False" HeaderText="CaseNoteID">
									<ItemTemplate>
										<asp:Label ID="lblCaseNoteID" Text='<%# DataBinder.Eval(Container.DataItem,"casenoteID") %>' Runat="server" />
									</ItemTemplate>
								</asp:TemplateField>
								<asp:TemplateField HeaderText="Remove Alert?">
									<ItemStyle HorizontalAlign="Center" Width="80"></ItemStyle>
									<ItemTemplate>
										<asp:CheckBox ID="chkProcess" Runat="server" Width="55" />
									</ItemTemplate>
								</asp:TemplateField>
								<asp:BoundField DataField="Patient" HeaderText="Patient" HtmlEncode="false"></asp:BoundField>
								<asp:BoundField DataField="Note" HeaderText="Note" HtmlEncode="false"></asp:BoundField>
							</Columns>
                    </asp:GridView>
                    <br />
            <asp:Button ID="ButtonRemove" runat="server" Text="Remove Selected Alerts" 
                onclick="ButtonRemove_Click" /><br />
            <asp:Label ID="LabelRemove" runat="server" 
                Text="Selected alerts have been removed" Font-Italic="True" ForeColor="Gray" 
                Visible="False"></asp:Label>
                </div>
    </ContentTemplate></asp:UpdatePanel>
    </div>
</asp:Content>

