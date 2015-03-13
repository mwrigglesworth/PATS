<%@ Page Language="C#" MasterPageFile="~/PATS.master" AutoEventWireup="true" CodeFile="WebApplicants.aspx.cs" Inherits="TMF_WebApplicants" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<div style="width:100%; clear:both; text-align: left;"><h1>Web Applicants</h1>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
<asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="0" DynamicLayout="true" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate><img src="../Images/loading.gif" /></ProgressTemplate>
             </asp:UpdateProgress>
    <div class="QueueDivHeader">
        <asp:Label ID="LabelResultCount" runat="server" Text=""></asp:Label></div><div class="QueueHeaderRight"></div>
        <div class="QueueDiv"><asp:GridView ID="dgResults" runat="server" Width="915" AutoGenerateColumns="False" AlternatingRowStyle-BackColor="Gainsboro" BorderWidth="0px">
                    <Columns>
								<asp:TemplateField Visible="False" HeaderText="PatientID">
									<ItemTemplate>
										<asp:Label ID="lblAppID" Text='<%# DataBinder.Eval(Container.DataItem,"ApplicantID") %>' Runat="server" />
									</ItemTemplate>
								</asp:TemplateField>
								<asp:TemplateField HeaderText="Add Patient?">
									<ItemStyle HorizontalAlign="Center" Width="80"></ItemStyle>
									<ItemTemplate>
										<asp:CheckBox ID="chkProcess" Runat="server" Width="55" />
									</ItemTemplate>
								</asp:TemplateField>
								<asp:BoundField DataField="Country" HeaderText="Country" HtmlEncode="false" ControlStyle-Width="100"></asp:BoundField>
								<asp:BoundField DataField="Applicant" HeaderText="Applicant" HtmlEncode="false" ControlStyle-Width="300"></asp:BoundField>
								<asp:TemplateField HeaderText="Program Officer:">
									<ItemStyle HorizontalAlign="Center" Width="210px"></ItemStyle>
									<ItemTemplate>
										<asp:DropDownList id="dropPO" runat="server" Width="200px">
					<asp:ListItem Value="0">Select PO</asp:ListItem>
				</asp:DropDownList>
									</ItemTemplate>
								</asp:TemplateField>
								<asp:TemplateField HeaderText="Max Station:">
									<ItemStyle HorizontalAlign="Center" Width="210px"></ItemStyle>
									<ItemTemplate>
										<asp:DropDownList id="dropMS" runat="server" Width="200px">
					<asp:ListItem Value="0">Select MAX Station</asp:ListItem>
				</asp:DropDownList>
									</ItemTemplate>
								</asp:TemplateField>
							</Columns>
                    </asp:GridView>
    <br />
            <asp:Button ID="ButtonAdd" runat="server" Text="Add Selected Patients" 
                 /><br />
                <asp:Label ID="LabelError" runat="server" Text="" Visible="false" ForeColor="Red"></asp:Label>
            <asp:Label ID="LabelAdd" runat="server" 
                Text="Selected patients have been added to PATS" Font-Italic="True" ForeColor="Gray" 
                Visible="False"></asp:Label></div>
    </ContentTemplate></asp:UpdatePanel>
   </div>
</asp:Content>

