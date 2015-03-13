<%@ Page Language="C#" MasterPageFile="~/PATS.master" AutoEventWireup="true" CodeFile="ReapprovalsByPhysician.aspx.cs" Inherits="Physician_ReapprovalsByPhysician" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<div style="width:100%; clear:both; text-align: left;"><h1>Reapproval Requests</h1>
    <div class="QueueDivHeader">
        <asp:Label ID="LabelResultCount" runat="server" Text=""></asp:Label></div><div class="QueueHeaderRight"></div>
        <div class="QueueDiv">
            <asp:GridView ID="dgReapps" runat="server" Width="915" 
                AutoGenerateColumns="False" AlternatingRowStyle-BackColor="Gainsboro" 
                BorderWidth="0px" GridLines="Horizontal" AllowPaging="True" Name="dgReapps"
                onpageindexchanging="dgReapps_PageIndexChanging">
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
								<asp:BoundField DataField="currentperiod" HeaderText="Current Period" HtmlEncode="false"><ItemStyle Width=155 VerticalAlign=Top /></asp:BoundField>
								<asp:TemplateField HeaderText="Re-evaluation information was 
                                received via:">
									<ItemStyle HorizontalAlign="Center" Width="250" ></ItemStyle>
									<ItemTemplate>
										<asp:DropDownList id="dropReceived" runat="server">
									<asp:ListItem Value="0">Select One</asp:ListItem>
									<asp:ListItem Value="Fax from physician">Fax from physician</asp:ListItem>
									<asp:ListItem Value="Email from physician" Selected="True">Email from physician</asp:ListItem>
									<asp:ListItem Value="Verbal verification from physician">Verbal verification from physician</asp:ListItem>
									<asp:ListItem Value="Written notification from physician">Written notification from physician</asp:ListItem>
								</asp:DropDownList>
									</ItemTemplate>
								</asp:TemplateField>
							</Columns>
                    </asp:GridView>
    <br />
            <asp:Button ID="ButtonReapprove" runat="server" 
                Text="Reapprove Selected Patients" onclick="ButtonReapprove_Click" /><br />
            <asp:Label ID="LabelError" runat="server" Text="" Visible="false" ForeColor="Red"></asp:Label>
            <asp:Label ID="LabelReapprove" runat="server" 
                Text="Selected patients have been reapproved" Font-Italic="True" ForeColor="Gray" 
                Visible="False"></asp:Label></div>
    </div>
</asp:Content>

