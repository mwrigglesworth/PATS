<%@ Page Language="C#" MasterPageFile="~/PATS.master" AutoEventWireup="true" CodeFile="PORequests.aspx.cs" Inherits="TMF_PORequests" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Release">
</asp:ScriptManager>
<div style="width:100%; clear:both; text-align: left;"><h1>PO Requests</h1>
    <asp:Panel ID="PanelClosed" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>

    <div class="QueueDivHeader">
        <asp:Label ID="LabelResultCountClosed" runat="server" Text=""></asp:Label></div><div class="QueueHeaderRight"></div>
        <div class="QueueDiv"><asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="0" DynamicLayout="true" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
                        <img alt="..loading" src="../Images/loading.gif" /></ProgressTemplate>
             </asp:UpdateProgress><asp:GridView ID="dgResultsClosed" runat="server" 
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
										<asp:Label ID="lblRequestID" Text='<%# DataBinder.Eval(Container.DataItem,"RequestID") %>' Runat="server" />
									</ItemTemplate>
								</asp:TemplateField>
								<asp:TemplateField HeaderText="Process Request?">
									<ItemStyle HorizontalAlign="Center" Width="80"></ItemStyle>
									<ItemTemplate>
										<asp:CheckBox ID="chkProcess" Runat="server" Width="55" />
									</ItemTemplate>
								</asp:TemplateField>
								<asp:TemplateField HeaderText="Remove">
									<ItemStyle Width="100"></ItemStyle>
									<ItemTemplate>
                                        <asp:ImageButton ID="ButtonRemove" runat="server" ImageUrl="../Images/RemoveSmall.png" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"  OnClick = "Button1_Click" />	
                                        <asp:Button ID="ButtonRemove2" runat="server" Text="REMOVE"  OnClick = "Button2_Click" BackColor="Red" Visible="false" />
									</ItemTemplate>
								</asp:TemplateField>
								<asp:BoundField DataField="Process Manually" HeaderText="Process<br>Manually" HtmlEncode="false"><ItemStyle Width="100"></ItemStyle></asp:BoundField>
								<asp:BoundField DataField="Patient" HeaderText="Patient" HtmlEncode="false"><ItemStyle Width="300"></ItemStyle></asp:BoundField>
								<asp:BoundField DataField="request" HeaderText="Request" HtmlEncode="false"></asp:BoundField>
							</Columns>
                    </asp:GridView>
    <br />
            <asp:Button ID="ButtonClose" runat="server" Text="Close Selected Patients" 
                onclick="ButtonClose_Click" /><br />
            <asp:Label ID="LabelClose" runat="server" 
                Text="Selected patients have been closed" Font-Italic="True" ForeColor="Gray" 
                Visible="False"></asp:Label></div>
    </ContentTemplate></asp:UpdatePanel>
    <div class="LeftColSpacer"></div>
    </asp:Panel>
    <asp:Panel ID="PanelAE" runat="server">
    <asp:UpdatePanel ID="UpdatePanel8" runat="server">
<ContentTemplate>

    <div class="QueueDivHeader">
        <asp:Label ID="LabelResultCountAE" runat="server" Text=""></asp:Label></div><div class="QueueHeaderRight"></div>
        <div class="QueueDiv"><asp:UpdateProgress ID="UpdateProgress8" runat="server" DisplayAfter="0" DynamicLayout="true" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
                        <img alt="..loading" src="../Images/loading.gif" /></ProgressTemplate>
             </asp:UpdateProgress><asp:GridView ID="dgAE" runat="server" 
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
										<asp:Label ID="lblRequestID" Text='<%# DataBinder.Eval(Container.DataItem,"RequestID") %>' Runat="server" />
									</ItemTemplate>
								</asp:TemplateField>
								<asp:TemplateField Visible="False" HeaderText="RequestID">
									<ItemTemplate>
										<asp:Label ID="lblErrorName" Text='<%# DataBinder.Eval(Container.DataItem,"ErrorName") %>' Runat="server" />
									</ItemTemplate>
								</asp:TemplateField>
								<asp:TemplateField HeaderText="Process Request?">
									<ItemStyle HorizontalAlign="Center" Width="80"></ItemStyle>
									<ItemTemplate>
										<asp:CheckBox ID="chkProcess" Runat="server" Width="55" />
									</ItemTemplate>
								</asp:TemplateField>
								<asp:TemplateField HeaderText="Remove">
									<ItemStyle Width="100"></ItemStyle>
									<ItemTemplate>
                                        <asp:ImageButton ID="ButtonRemove" runat="server" ImageUrl="../Images/RemoveSmall.png" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"  OnClick = "Button1_Click" />	
                                        <asp:Button ID="ButtonRemove2" runat="server" Text="REMOVE"  OnClick = "Button2_Click" BackColor="Red" Visible="false" />
									</ItemTemplate>
								</asp:TemplateField>
								<asp:BoundField DataField="Process Manually" HeaderText="Process<br>Manually" HtmlEncode="false"><ItemStyle Width="100"></ItemStyle></asp:BoundField>
								<asp:TemplateField HeaderText="Patient"><ItemStyle Width="300" />
									<ItemTemplate>
									<asp:Label ID="lblPatient" Text='<%# DataBinder.Eval(Container.DataItem,"Patient") %>' Runat="server" /><br /><br />
                                        <asp:LinkButton ID="lbAECount" CssClass="lbAR" Text='<%# DataBinder.Eval(Container.DataItem,"saecount") + " AEs Logged" %>' OnClick = "ButtonSAE_Click" runat="server"></asp:LinkButton><br />
                                        <asp:Label ID="LabelAEHistory" runat="server" Text="" ForeColor="Gray" Font-Size="Small"></asp:Label>
									</ItemTemplate>
								</asp:TemplateField>
								<asp:BoundField DataField="request" HeaderText="Request" HtmlEncode="false"></asp:BoundField>
								</Columns>
                    </asp:GridView>
    <br />
            <asp:Button ID="ButtonAE" runat="server" Text="Log Selected AE" 
                onclick="ButtonAE_Click" /><br />
                <asp:Label ID="LabelAEError" runat="server" Text="" Visible="false" ForeColor="Red"></asp:Label>
            <asp:Label ID="LabelAE" runat="server" 
                Text="Selected AE have been logged" Font-Italic="True" ForeColor="Gray" 
                Visible="False"></asp:Label></div>
    </ContentTemplate></asp:UpdatePanel>
    <div class="LeftColSpacer"></div>
    </asp:Panel>
    <asp:Panel ID="PanelDoseChange" runat="server">
    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
<ContentTemplate>
    <div class="QueueDivHeader">
        <asp:Label ID="LabelResultCountDoseChange" runat="server" Text=""></asp:Label></div><div class="QueueHeaderRight"></div>
        <div class="QueueDiv">
<asp:UpdateProgress ID="UpdateProgress4" runat="server" DisplayAfter="0" DynamicLayout="true" AssociatedUpdatePanelID="UpdatePanel4">
        <ProgressTemplate>
                        <img alt="..loading" src="../Images/loading.gif" /></ProgressTemplate>
             </asp:UpdateProgress><asp:GridView ID="dgDoseChange" runat="server" Width="925" AutoGenerateColumns="False" AlternatingRowStyle-BackColor="Gainsboro" BorderWidth="0px" GridLines="None" HeaderStyle-BackColor="Gainsboro">
                    <Columns>
								<asp:TemplateField Visible="False" HeaderText="PatientID">
									<ItemTemplate>
										<asp:Label ID="lblPatientID" Text='<%# DataBinder.Eval(Container.DataItem,"patientID") %>' Runat="server" />
									</ItemTemplate>
								</asp:TemplateField>
								<asp:TemplateField Visible="False" HeaderText="RequestID">
									<ItemTemplate>
										<asp:Label ID="lblRequestID" Text='<%# DataBinder.Eval(Container.DataItem,"RequestID") %>' Runat="server" />
									</ItemTemplate>
								</asp:TemplateField>
								<asp:TemplateField HeaderText="Process Request?">
									<ItemStyle HorizontalAlign="Center" Width="80"></ItemStyle>
									<ItemTemplate>
										<asp:CheckBox ID="chkProcess" Runat="server" Width="55" />
									</ItemTemplate>
								</asp:TemplateField>
								<asp:TemplateField HeaderText="Remove">
									<ItemStyle Width="100"></ItemStyle>
									<ItemTemplate>
                                        <asp:ImageButton ID="ButtonRemove" runat="server" ImageUrl="../Images/RemoveSmall.png" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"  OnClick = "Button1_Click" />	
                                        <asp:Button ID="ButtonRemove2" runat="server" Text="REMOVE"  OnClick = "Button2_Click" BackColor="Red" Visible="false" />
									</ItemTemplate>
								</asp:TemplateField>
								<asp:BoundField DataField="Process Manually" HeaderText="Process<br>Manually" HtmlEncode="false"><ItemStyle Width="100"></ItemStyle></asp:BoundField>
								<asp:BoundField DataField="Patient" HeaderText="Patient" HtmlEncode="false"><ItemStyle Width="300"></ItemStyle></asp:BoundField>
								<asp:BoundField DataField="request" HeaderText="Request" HtmlEncode="false"></asp:BoundField>
							</Columns>
                    </asp:GridView>
    <br />
            <asp:Button ID="ButtonDoseChange" runat="server" 
                Text="Change Selected Patient Dosages" onclick="ButtonDoseChange_Click" /><br />
            <asp:Label ID="LabelDoseChange" runat="server" 
                Text="Selected patient doses have been changed" Font-Italic="True" ForeColor="Gray" 
                Visible="False"></asp:Label></div>
    </ContentTemplate></asp:UpdatePanel>
    <div class="LeftColSpacer"></div>
    </asp:Panel>
    <asp:Panel ID="PanelReactivate" runat="server">
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
<ContentTemplate>
    <div class="QueueDivHeader">
        <asp:Label ID="LabelResultCountReactivate" runat="server" Text=""></asp:Label></div><div class="QueueHeaderRight"></div>
        <div class="QueueDiv">
        <font class="Subtext"><b>Please Note: </b>Batch functionality has been removed for Reactivations.  Each request should be processed from the patient page to prevent user error.</font>
<asp:UpdateProgress ID="UpdateProgress2" runat="server" DisplayAfter="0" DynamicLayout="true" AssociatedUpdatePanelID="UpdatePanel2">
        <ProgressTemplate>
                        <img alt="..loading" src="../Images/loading.gif" /></ProgressTemplate>
             </asp:UpdateProgress><asp:GridView ID="dgReactivate" runat="server" Width="925" AutoGenerateColumns="False" AlternatingRowStyle-BackColor="Gainsboro" BorderWidth="0px" GridLines="None" HeaderStyle-BackColor="Gainsboro">
                    <Columns>
								<asp:TemplateField Visible="False" HeaderText="PatientID">
									<ItemTemplate>
										<asp:Label ID="lblPatientID" Text='<%# DataBinder.Eval(Container.DataItem,"patientID") %>' Runat="server" />
									</ItemTemplate>
								</asp:TemplateField>
								<asp:TemplateField Visible="False" HeaderText="RequestID">
									<ItemTemplate>
										<asp:Label ID="lblRequestID" Text='<%# DataBinder.Eval(Container.DataItem,"RequestID") %>' Runat="server" />
									</ItemTemplate>
								</asp:TemplateField>
								<asp:TemplateField Visible="False" HeaderText="RequestID">
									<ItemTemplate>
										<asp:Label ID="lblErrorName" Text='<%# DataBinder.Eval(Container.DataItem,"ErrorName") %>' Runat="server" />
									</ItemTemplate>
								</asp:TemplateField>
								<asp:TemplateField Visible="false" HeaderText="Process Request?">
									<ItemStyle HorizontalAlign="Center" Width="80"></ItemStyle>
									<ItemTemplate>
										<asp:CheckBox ID="chkProcess" Runat="server" Width="55" />
									</ItemTemplate>
								</asp:TemplateField>
								<asp:TemplateField HeaderText="Remove">
									<ItemStyle Width="100"></ItemStyle>
									<ItemTemplate>
                                        <asp:ImageButton ID="ButtonRemove" runat="server" ImageUrl="../Images/RemoveSmall.png" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"  OnClick = "Button1_Click" />	
                                        <asp:Button ID="ButtonRemove2" runat="server" Text="REMOVE"  OnClick = "Button2_Click" BackColor="Red" Visible="false" />
									</ItemTemplate>
								</asp:TemplateField>
								<asp:BoundField DataField="Process Manually" HeaderText="Process<br>Manually" HtmlEncode="false"><ItemStyle Width="100"></ItemStyle></asp:BoundField>
								<asp:BoundField DataField="Patient" HeaderText="Patient" HtmlEncode="false"><ItemStyle Width="250"></ItemStyle></asp:BoundField>
								<asp:BoundField DataField="request" HeaderText="Request" HtmlEncode="false"></asp:BoundField>
								<asp:TemplateField HeaderText="Reason For Reactivation:" Visible="false">
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:DropDownList id="dropReactivation" runat="server" Width="200px">
					<asp:ListItem Value="0">Select One</asp:ListItem>
					<asp:ListItem Value="Clinical Reason">Clinical Reason</asp:ListItem>
					<asp:ListItem Value="Patient Meets GIPAP Criteria">Patient Meets GIPAP Criteria</asp:ListItem>
					<asp:ListItem Value="Re-established contact with patient">Re-established contact with patient</asp:ListItem>
					<asp:ListItem Value="Re-evaluation information provided">Re-evaluation information provided</asp:ListItem>
					<asp:ListItem Value="Not Duplicate Patient">Not Duplicate Patient</asp:ListItem>
					<asp:ListItem Value="Other">Other</asp:ListItem>
				</asp:DropDownList>
									</ItemTemplate>
								</asp:TemplateField>
							</Columns>
                    </asp:GridView>
    <br />
            <asp:Button ID="ButtonReactivate" runat="server" 
                Text="Reactivate Selected Patients" onclick="ButtonReactivate_Click" 
                Visible="False" /><br />
            <asp:Label ID="LabelReactivateError" runat="server" Text="" Visible="false" ForeColor="Red"></asp:Label>
            <asp:Label ID="LabelReactivate" runat="server" 
                Text="Selected patients have been reactivated" Font-Italic="True" ForeColor="Gray" 
                Visible="False"></asp:Label></div>
    </ContentTemplate></asp:UpdatePanel>
    <div class="LeftColSpacer"></div>
    </asp:Panel>
    <asp:Panel ID="PanelExtend" runat="server">
    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
<ContentTemplate>
    <div class="QueueDivHeader">
        <asp:Label ID="LabelResultCountExtend" runat="server" Text=""></asp:Label></div><div class="QueueHeaderRight"></div>
        <div class="QueueDiv">
<asp:UpdateProgress ID="UpdateProgress3" runat="server" DisplayAfter="0" DynamicLayout="true" AssociatedUpdatePanelID="UpdatePanel3">
        <ProgressTemplate>
                        <img alt="..loading" src="../Images/loading.gif" /></ProgressTemplate>
             </asp:UpdateProgress><asp:GridView ID="dgExtend" runat="server" Width="925" AutoGenerateColumns="False" AlternatingRowStyle-BackColor="Gainsboro" BorderWidth="0px" GridLines="None" HeaderStyle-BackColor="Gainsboro">
                    <Columns>
								<asp:TemplateField Visible="False" HeaderText="PatientID">
									<ItemTemplate>
										<asp:Label ID="lblPatientID" Text='<%# DataBinder.Eval(Container.DataItem,"patientID") %>' Runat="server" />
									</ItemTemplate>
								</asp:TemplateField>
								<asp:TemplateField Visible="False" HeaderText="RequestID">
									<ItemTemplate>
										<asp:Label ID="lblRequestID" Text='<%# DataBinder.Eval(Container.DataItem,"RequestID") %>' Runat="server" />
									</ItemTemplate>
								</asp:TemplateField>
								<asp:TemplateField HeaderText="Process Request?">
									<ItemStyle HorizontalAlign="Center" Width="80"></ItemStyle>
									<ItemTemplate>
										<asp:CheckBox ID="chkProcess" Runat="server" Width="55" />
									</ItemTemplate>
								</asp:TemplateField>
								<asp:TemplateField HeaderText="Remove">
									<ItemStyle Width="100"></ItemStyle>
									<ItemTemplate>
                                        <asp:ImageButton ID="ButtonRemove" runat="server" ImageUrl="../Images/RemoveSmall.png" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"  OnClick = "Button1_Click" />	
                                        <asp:Button ID="ButtonRemove2" runat="server" Text="REMOVE"  OnClick = "Button2_Click" BackColor="Red" Visible="false" />
									</ItemTemplate>
								</asp:TemplateField>
								<asp:BoundField DataField="Process Manually" HeaderText="Process<br>Manually" HtmlEncode="false"><ItemStyle Width="100"></ItemStyle></asp:BoundField>
								<asp:BoundField DataField="Patient" HeaderText="Patient" HtmlEncode="false"><ItemStyle Width="300"></ItemStyle></asp:BoundField>
								<asp:BoundField DataField="request" HeaderText="Request" HtmlEncode="false"></asp:BoundField>
							</Columns>
                    </asp:GridView>
    <br />
            <asp:Button ID="ButtonExtend" runat="server" Text="Extend Selected Patients" 
                onclick="ButtonExtend_Click" /><br />
            <asp:Label ID="LabelExtend" runat="server" 
                Text="Selected patients have been extended" Font-Italic="True" ForeColor="Gray" 
                Visible="False"></asp:Label></div>
    </ContentTemplate></asp:UpdatePanel>
    <div class="LeftColSpacer"></div>
    </asp:Panel>
    <asp:Panel ID="PanelTreatmentChange" runat="server">
    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
<ContentTemplate>

    <div class="QueueDivHeader">
        <asp:Label ID="LabelResultCountTreatmentChange" runat="server" Text=""></asp:Label></div><div class="QueueHeaderRight"></div>
        <div class="QueueDiv"><asp:UpdateProgress ID="UpdateProgress5" runat="server" DisplayAfter="0" DynamicLayout="true" AssociatedUpdatePanelID="UpdatePanel5">
        <ProgressTemplate>
                        <img alt="..loading" src="../Images/loading.gif" /></ProgressTemplate>
             </asp:UpdateProgress><asp:GridView ID="dgTreatmentChange" runat="server" Width="925" AutoGenerateColumns="False" AlternatingRowStyle-BackColor="Gainsboro" BorderWidth="0px" GridLines="None" HeaderStyle-BackColor="Gainsboro">
                    <Columns>
								<asp:TemplateField Visible="False" HeaderText="PatientID">
									<ItemTemplate>
										<asp:Label ID="lblPatientID" Text='<%# DataBinder.Eval(Container.DataItem,"patientID") %>' Runat="server" />
									</ItemTemplate>
								</asp:TemplateField>
								<asp:TemplateField Visible="False" HeaderText="RequestID">
									<ItemTemplate>
										<asp:Label ID="lblRequestID" Text='<%# DataBinder.Eval(Container.DataItem,"RequestID") %>' Runat="server" />
									</ItemTemplate>
								</asp:TemplateField>
								<asp:TemplateField HeaderText="Process Request?">
									<ItemStyle HorizontalAlign="Center" Width="80"></ItemStyle>
									<ItemTemplate>
										<asp:CheckBox ID="chkProcess" Runat="server" Width="55" />
									</ItemTemplate>
								</asp:TemplateField>
								<asp:TemplateField HeaderText="Remove">
									<ItemStyle Width="100"></ItemStyle>
									<ItemTemplate>
                                        <asp:ImageButton ID="ButtonRemove" runat="server" ImageUrl="../Images/RemoveSmall.png" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"  OnClick = "Button1_Click" />	
                                        <asp:Button ID="ButtonRemove2" runat="server" Text="REMOVE"  OnClick = "Button2_Click" BackColor="Red" Visible="false" />
									</ItemTemplate>
								</asp:TemplateField>
								<asp:BoundField DataField="Process Manually" HeaderText="Process<br>Manually" HtmlEncode="false"><ItemStyle Width="100"></ItemStyle></asp:BoundField>
								<asp:BoundField DataField="Patient" HeaderText="Patient" HtmlEncode="false"><ItemStyle Width="300"></ItemStyle></asp:BoundField>
								<asp:BoundField DataField="request" HeaderText="Request" HtmlEncode="false"></asp:BoundField>
							</Columns>
                    </asp:GridView>
    <br />
            <asp:Button ID="ButtonTreatmentChange" runat="server" 
                Text="Approve Selected Patients for Tasigna" 
                onclick="ButtonTreatmentChange_Click" /><br />
            <asp:Label ID="LabelTreatmentChagne" runat="server" 
                Text="Selected patients have been Approved for Tasigna" Font-Italic="True" ForeColor="Gray" 
                Visible="False"></asp:Label></div>
    </ContentTemplate></asp:UpdatePanel>
    <div class="LeftColSpacer"></div>
    </asp:Panel>
    <asp:Panel ID="PanelReassess" runat="server">
    <asp:UpdatePanel ID="UpdatePanel6" runat="server">
<ContentTemplate>
    <div class="QueueDivHeader">
        <asp:Label ID="LabelResultCountReassess" runat="server" Text=""></asp:Label></div><div class="QueueHeaderRight"></div>
        <div class="QueueDiv">
<asp:UpdateProgress ID="UpdateProgress6" runat="server" DisplayAfter="0" DynamicLayout="true" AssociatedUpdatePanelID="UpdatePanel6">
        <ProgressTemplate>
                        <img alt="..loading" src="../Images/loading.gif" /></ProgressTemplate>
             </asp:UpdateProgress><asp:GridView ID="dgReassess" runat="server" Width="925" AutoGenerateColumns="False" AlternatingRowStyle-BackColor="Gainsboro" BorderWidth="0px" GridLines="None" HeaderStyle-BackColor="Gainsboro">
                    <Columns>
								<asp:TemplateField Visible="False" HeaderText="PatientID">
									<ItemTemplate>
										<asp:Label ID="lblPatientID" Text='<%# DataBinder.Eval(Container.DataItem,"patientID") %>' Runat="server" />
									</ItemTemplate>
								</asp:TemplateField>
								<asp:TemplateField Visible="False" HeaderText="RequestID">
									<ItemTemplate>
										<asp:Label ID="lblRequestID" Text='<%# DataBinder.Eval(Container.DataItem,"RequestID") %>' Runat="server" />
									</ItemTemplate>
								</asp:TemplateField>
								<asp:TemplateField Visible="False" HeaderText="RequestID">
									<ItemTemplate>
										<asp:Label ID="lblErrorName" Text='<%# DataBinder.Eval(Container.DataItem,"ErrorName") %>' Runat="server" />
									</ItemTemplate>
								</asp:TemplateField>
								<asp:TemplateField HeaderText="Process Request?">
									<ItemStyle HorizontalAlign="Center" Width="80"></ItemStyle>
									<ItemTemplate>
										<asp:CheckBox ID="chkProcess" Runat="server" Width="55" />
									</ItemTemplate>
								</asp:TemplateField>
								<asp:TemplateField HeaderText="Remove">
									<ItemStyle Width="100"></ItemStyle>
									<ItemTemplate>
                                        <asp:ImageButton ID="ButtonRemove" runat="server" ImageUrl="../Images/RemoveSmall.png" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"  OnClick = "Button1_Click" />	
                                        <asp:Button ID="ButtonRemove2" runat="server" Text="REMOVE"  OnClick = "Button2_Click" BackColor="Red" Visible="false" />
									</ItemTemplate>
								</asp:TemplateField>
								<asp:BoundField DataField="Process Manually" HeaderText="Process<br>Manually" HtmlEncode="false"><ItemStyle Width="100"></ItemStyle></asp:BoundField>
								<asp:BoundField DataField="Patient" HeaderText="Patient" HtmlEncode="false"><ItemStyle Width="300"></ItemStyle></asp:BoundField>
								<asp:BoundField DataField="request" HeaderText="Request" HtmlEncode="false"></asp:BoundField>
								<asp:TemplateField HeaderText="Reason For Approval:">
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:DropDownList id="dropApproveStatusReason" runat="server" Width="250px">
					<asp:ListItem Value="0">Select One</asp:ListItem>
					<asp:ListItem Value="Approved by exception">Approved by exception</asp:ListItem>
					<asp:ListItem Value="Approved with Partial Coverage">Approved with Partial Coverage</asp:ListItem>
					<asp:ListItem Value="Fulfills all criteria">Fulfills all criteria</asp:ListItem>
				</asp:DropDownList>
									</ItemTemplate>
								</asp:TemplateField>
							</Columns>
                    </asp:GridView>
    <br />
            <asp:Button ID="ButtonReassess" runat="server" 
                Text="Reassess/Approve Selected Patients" onclick="ButtonReassess_Click" /><br />
            <asp:Label ID="LabelReassessError" runat="server" Text="" Visible="false" ForeColor="Red"></asp:Label>
            <asp:Label ID="LabelReassess" runat="server" 
                Text="Selected patients have been approved" Font-Italic="True" ForeColor="Gray" 
                Visible="False"></asp:Label></div>
    </ContentTemplate></asp:UpdatePanel>
    <div class="LeftColSpacer"></div>
    </asp:Panel>
    <asp:Panel ID="PanelFEF" runat="server">
    <asp:UpdatePanel ID="UpdatePanel7" runat="server">
<ContentTemplate>
    <div class="QueueDivHeader">
        <asp:Label ID="LabelResultCountFEF" runat="server" Text=""></asp:Label></div><div class="QueueHeaderRight"></div>
        <div class="QueueDiv">
<asp:UpdateProgress ID="UpdateProgress7" runat="server" DisplayAfter="0" DynamicLayout="true" AssociatedUpdatePanelID="UpdatePanel7">
        <ProgressTemplate>
                        <img alt="..loading" src="../Images/loading.gif" /></ProgressTemplate>
             </asp:UpdateProgress><asp:GridView ID="dgFEF" runat="server" Width="925" AutoGenerateColumns="False" AlternatingRowStyle-BackColor="Gainsboro" BorderWidth="0px" GridLines="None" HeaderStyle-BackColor="Gainsboro">
                    <Columns>
								<asp:TemplateField Visible="False" HeaderText="PatientID">
									<ItemTemplate>
										<asp:Label ID="lblPatientID" Text='<%# DataBinder.Eval(Container.DataItem,"patientID") %>' Runat="server" />
									</ItemTemplate>
								</asp:TemplateField>
								<asp:TemplateField Visible="False" HeaderText="RequestID">
									<ItemTemplate>
										<asp:Label ID="lblRequestID" Text='<%# DataBinder.Eval(Container.DataItem,"RequestID") %>' Runat="server" />
									</ItemTemplate>
								</asp:TemplateField>
								<asp:TemplateField HeaderText="Process Request?">
									<ItemStyle HorizontalAlign="Center" Width="80"></ItemStyle>
									<ItemTemplate>
										<asp:CheckBox ID="chkProcess" Runat="server" Width="55" />
									</ItemTemplate>
								</asp:TemplateField>
								<asp:TemplateField HeaderText="Remove">
									<ItemStyle Width="100"></ItemStyle>
									<ItemTemplate>
                                        <asp:ImageButton ID="ButtonRemove" runat="server" ImageUrl="../Images/RemoveSmall.png" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"  OnClick = "Button1_Click" />	
                                        <asp:Button ID="ButtonRemove2" runat="server" Text="REMOVE"  OnClick = "Button2_Click" BackColor="Red" Visible="false" />
									</ItemTemplate>
								</asp:TemplateField>
								<asp:BoundField DataField="Process Manually" HeaderText="Process<br>Manually" HtmlEncode="false"><ItemStyle Width="100"></ItemStyle></asp:BoundField>
								<asp:BoundField DataField="Patient" HeaderText="Patient" HtmlEncode="false"><ItemStyle Width="300"></ItemStyle></asp:BoundField>
								<asp:BoundField DataField="request" HeaderText="FEF Summary" HtmlEncode="false"></asp:BoundField>
							</Columns>
                    </asp:GridView>
    <br />
            <asp:Button ID="ButtonFEF" runat="server" 
                Text="Remove Selected FEF Updates from my Queue" 
                onclick="ButtonFEF_Click" /><br />
            <asp:Label ID="LabelFEF" runat="server" 
                Text="Selected FEF Updates have been removed from your queue" Font-Italic="True" ForeColor="Gray" 
                Visible="False"></asp:Label></div>
    </ContentTemplate></asp:UpdatePanel>
    <div class="LeftColSpacer"></div>
    </asp:Panel>
    </div>
</asp:Content>

