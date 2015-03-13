<%@ Page Language="C#" MasterPageFile="~/Patient/GIPAPPatient.master" AutoEventWireup="true" CodeFile="Extend.aspx.cs" Inherits="Patient_Extend" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderControlPanel" Runat="Server">
    <div class="ControlPanelDivHeader">
                    Extend Current Period</div>
                <div class="ControlPanelHeaderRight">
                </div>
<div class="FormDiv">
<TABLE id="Table11" cellSpacing="1" cellPadding="1" width="585" align="left" border="0">
		<TR>
			<TD><asp:validationsummary id="ValidationSummary1" runat="server" HeaderText="You are missing the following fields:"
		ShowMessageBox="True" CssClass="AlertDiv" ForeColor=""></asp:validationsummary></TD>
		</TR>
		<TR>
			<TD>
                <asp:Panel ID="PanelObsolete" runat="server" Visible="False">
                <div class="AlertDiv">
                The current status of this 
patient renders the following request obsolete.&nbsp; An action may have been 
executed for this patient since this request was submitted.  Click below to remove the request.
                </div>
                </asp:Panel>
            </TD>
		</TR>
	</TABLE>
	<div style="clear:both;">
<TABLE id="Table8" cellSpacing="1" cellPadding="1" width="600" align="center" border="0">
		<TR>
			<TD vAlign="top" width="341">
				<TABLE id="Table9" cellSpacing="1" cellPadding="1" width="100%" border="0">
					<TR>
						<TD>Reason For Extension:
							<asp:RequiredFieldValidator id="RequiredFieldValidator7" runat="server" ErrorMessage="Reason" ControlToValidate="txtExtendReason">*</asp:RequiredFieldValidator></TD>
					</TR>
					<TR>
						<TD>
							<asp:TextBox id="txtExtendReason" runat="server" Width="100%" TextMode="MultiLine" Height="88px"></asp:TextBox></TD>
					</TR>
					<TR>
						<TD>Start Date:
							<asp:Label id="LabelEditStart" runat="server"></asp:Label>&nbsp;
						</TD>
					</TR>
					<TR>
						<TD>End&nbsp;Date:
							<asp:Label id="LabelEditEnd" runat="server"></asp:Label>&nbsp;
							<asp:LinkButton id="lbExtendEnd" runat="server" CausesValidation="False" onclick="lbExtendEnd_Click">Edit Date</asp:LinkButton></TD>
					</TR>
					<TR>
			<TD><asp:Panel id="PanelExtendReceive" runat="server"><br /><br />Was this requested by the physician?<br />
                                <asp:RadioButtonList ID="rblstExtendPhysReq" runat="server" 
                                    RepeatDirection="Horizontal">
                                    <asp:ListItem>No</asp:ListItem>
                                    <asp:ListItem Selected="True">Yes</asp:ListItem>
                                </asp:RadioButtonList>
                               
                                <br /><br />This information was 
                                received via:<BR>
<asp:DropDownList id="dropExtendReceived" runat="server">
									<asp:ListItem Value="0">Select One</asp:ListItem>
									<asp:ListItem Value="Fax from physician">Fax from physician</asp:ListItem>
									<asp:ListItem Value="Email from physician">Email from physician</asp:ListItem>
									<asp:ListItem Value="Verbal verification from physician">Verbal verification from physician</asp:ListItem>
									<asp:ListItem Value="Written notification from physician">Written notification from physician</asp:ListItem>
								</asp:DropDownList>
<asp:CustomValidator ID="CustomValidator2" runat="server" 
                                    ClientValidationFunction="validateReceiveYes" ControlToValidate="rblstExtendPhysReq" 
                                    ErrorMessage="Information Received By">*</asp:CustomValidator><br /><br /></asp:Panel></TD>
		</TR>
					<TR>
						<TD>
							<asp:Button id="ButtonExtend" runat="server" Width="75px" Text="Submit" Enabled="False" onclick="ButtonExtend_Click"></asp:Button>&nbsp;
							<asp:Button id="ButtonCancelExtend" runat="server" Width="75px" Text="Cancel" CausesValidation="False" onclick="ButtonCancelExtend_Click"></asp:Button>&nbsp;
							<asp:Button ID="ButtonRemoveRequest" runat="server" Text="Remove Request" 
                    BackColor="Red" onclick="ButtonRemoveRequest_Click" Visible="False" CausesValidation="false" /></TD>
					</TR>
				</TABLE>
				<asp:Label id="LabelExtendError" ForeColor="Red" runat="server">Please enter a date that is at least 15 days past the end date to extend.</asp:Label></TD>
			<TD vAlign="top">
				<asp:Calendar id="CalendarEditEnd" runat="server" BorderColor="#404040" BackColor="White" onselectionchanged="CalendarEditEnd_SelectionChanged"></asp:Calendar></TD>
		</TR>
	</TABLE>
	</div>
</div>
<script language="javascript">
		    //**********************************************************************************************************************
		    function validateReceiveYes(sender, e) {
		        if (e.Value == "Yes") {
		            if ($get('<%= dropExtendReceived.ClientID  %>').value != "0") {
		                e.IsValid = true;
		            }
		            else {
		                e.IsValid = false;
		            }
		        }
		        else {
		            e.IsValid = true;
		        }
		    }
</script>
</asp:Content>

