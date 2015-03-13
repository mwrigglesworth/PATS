<%@ Page Title="" Language="C#" MasterPageFile="~/PATS.master" AutoEventWireup="true" CodeFile="AddUserMessage.aspx.cs" Inherits="TMF_AddUserMessage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2"  ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  <asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<div  style="width:100%; clear:both; text-align: left;" >

 <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional" >
                        <ContentTemplate>

<div class="MessageDivHeader">Add User Message</div>
<div class="MessageHeaderRight"></div>
<div class="MessageDiv">
<TABLE id="Table1" cellSpacing="1" cellPadding="1" width="920px" border="0" >

        <%--<asp:HiddenField ID="CurIndex" Value="0" runat="server" />--%>
	<TR>
		<TD>User Type:&nbsp;
			<asp:CompareValidator id="CompareValidator1" runat="server" ErrorMessage="User Type" ControlToValidate="dropRole"
				Operator="NotEqual" ValueToCompare="0" ValidationGroup="NewMessage">*</asp:CompareValidator></TD>
	</TR>
	<TR>
		<TD align="center">
			<asp:DropDownList id="dropRole" runat="server"  Width="200px" OnSelectedIndexChanged="dropRole_SelectedIndexChanged" AutoPostBack="true">
				<asp:ListItem Value="0">Select a User Type</asp:ListItem>
				<asp:ListItem Value="2">MaxStation</asp:ListItem>
				<asp:ListItem Value="3">Physician</asp:ListItem>
			</asp:DropDownList></TD>
	</TR>
    <TR id= "CountryHead" runat="server" visible="false">
		<TD>Country:&nbsp;</TD>
        
	</TR>
    <TR id="CountryMenu" runat="server" visible="false">
		<TD align="center">
			<asp:DropDownList id="dropCountry" runat="server" Width="200px" >
			</asp:DropDownList></TD>
	</TR>

    <%-- <TR id="CurrentMessage" >
		<TD>Current Message:&nbsp;</TD>
	</TR>
    <TR id="CurrentMessageReg" >
		<TD>
            <asp:Label ID="LabelID" runat="server" Visible="false" Text="0"></asp:Label>&nbsp;&nbsp;
            <asp:Label ID="LabelMessage" runat="server" Text=""></asp:Label>&nbsp;&nbsp;
            <asp:LinkButton ID="ButtonDisable" runat="server" onclick="ButtonDisable_Click" 
                Visible="False">Delete</asp:LinkButton>
        </TD>
	</TR>--%>
	<TR>
		<TD>Message:
			<asp:RequiredFieldValidator id="RequiredFieldValidator1" runat="server" 
                ErrorMessage="Message" ControlToValidate="txtMessage" ValidationGroup="NewMessage">*</asp:RequiredFieldValidator></TD>
	</TR>
	<TR>
		<TD align="center">
            <asp:TextBox ID="txtMessage" runat="server" Height="200px" TextMode="MultiLine" 
                Width="540px"></asp:TextBox>
        </TD>
	</TR>
	<TR>
		<TD align="center">
			<asp:Button id="ButtonPost" CssClass="roundbutton" runat="server" Width="80px" Text="Post" BackColor="Maroon" ForeColor="White" Font-Bold="true"  onclick="ButtonPost_Click" ValidationGroup="NewMessage"></asp:Button>&nbsp;
			<asp:Button id="ButtonCancel" CssClass="roundbutton" runat="server" Width="80px" Text="Cancel" BackColor="Maroon" ForeColor="White" Font-Bold="true" CausesValidation="False" onclick="ButtonCancel_Click"></asp:Button></TD>
	</TR>
</TABLE>
<asp:ValidationSummary id="ValidationSummary1" runat="server" Width="256px" HeaderText="The Following Fields are Missing:" ValidationGroup="NewMessage"
	ShowMessageBox="True"></asp:ValidationSummary>
    </div>

          <br />              
<div class="MessageDivHeader">Active Message(s)</div>
<div class="MessageHeaderRight"></div>
<div class="MessageDiv">
<asp:GridView ID="GridMessage" runat="server" 
        Width="920px"  Font-Size="14px"
        AutoGenerateColumns="False"  GridLines="None" CellPadding="4" DataKeyNames="MessageId" 
         AllowPaging="True"  
        onpageindexchanging="GridMessage_PageIndexChanging" 
        onrowcancelingedit="GridMessage_RowCancelingEdit" 
        onrowdeleting="GridMessage_RowDeleting" onrowediting="GridMessage_RowEditing" 
        onrowupdating="GridMessage_RowUpdating">
<Columns>
<%--<asp:BoundField DataField="RoleName" HeaderText="Role"  Visible="true" HeaderStyle-Width="50px" ItemStyle-Width="50px"></asp:BoundField>--%>
<asp:TemplateField HeaderText="Role" HeaderStyle-Width="150px" ItemStyle-Width="150px">
<EditItemTemplate>
<asp:Label ID="lblRoleEdit" runat="server" Text='<%#Eval("RoleName") %>'/>
</EditItemTemplate>
<ItemTemplate>
<asp:Label ID="lblRole" runat="server" Text='<%#Eval("RoleName") %>'/>
</ItemTemplate>
</asp:TemplateField>
<%--<asp:BoundField DataField="CountryName" HeaderText="Country"  Visible="true" HeaderStyle-Width="50px" ItemStyle-Width="50px"></asp:BoundField>--%>
<asp:TemplateField HeaderText="Country" HeaderStyle-Width="150px" ItemStyle-Width="150px">
<EditItemTemplate>
<asp:Label ID="lblCountryEdit" runat="server" Text='<%#Eval("CountryName") %>'/>
</EditItemTemplate>
<ItemTemplate>
<asp:Label ID="lblCountry" runat="server" Text='<%#Eval("CountryName") %>'/>
</ItemTemplate>
</asp:TemplateField>

 <asp:TemplateField HeaderText="Message" HeaderStyle-Width="400px" ItemStyle-Width="400px">
<EditItemTemplate>
<asp:TextBox ID="txtMessage" runat="server" Text='<%#Eval("Message") %>' Width="400px" TextMode="MultiLine" />
</EditItemTemplate>
<ItemTemplate>
<asp:Label ID="lblMessage" runat="server" Text='<%#Eval("Message") %>'/>
</ItemTemplate>
</asp:TemplateField>

<asp:TemplateField HeaderText="Date" HeaderStyle-Width="150px" ItemStyle-Width="150px">
<EditItemTemplate >
<asp:Label ID="lblDateEdit" runat="server" Text='<%#Eval("CreatedOn") %>'/>
</EditItemTemplate>
<ItemTemplate>
<asp:Label ID="lblDate" runat="server" Text='<%#Eval("CreatedOn") %>'/>
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderStyle-Width="70px" ItemStyle-Width="70px" HeaderText="Action">
<%--            <ItemTemplate>
                <asp:ImageButton ID="editButton" Height="30px" Width="30px" ImageUrl="~/Images/edit.png"  ToolTip="Edit" runat="server" Text="Edit" onclick="editButton_OnClick"/>
                <asp:ImageButton ID="deleteButton" Height="30px" Width="30px" runat="server" ImageUrl="~/Images/RemoveSmall.png"  ToolTip="Delete" Text="Delete" onclick="deleteButton_OnClick" OnClientClick="javascript:return confirm('Are you sure?');"/>
            </ItemTemplate>
--%>
             <EditItemTemplate>
<asp:ImageButton ID="imgbtnUpdate" CommandName="Update" runat="server" ImageUrl="~/Images/update.png" ToolTip="Update" Height="20px" Width="20px" />
<asp:ImageButton ID="imgbtnCancel" runat="server" CommandName="Cancel" ImageUrl="~/Images/Cancel.jpg" ToolTip="Cancel" Height="20px" Width="20px" />
</EditItemTemplate>
<ItemTemplate>
<asp:ImageButton ID="imgbtnEdit" CommandName="Edit" runat="server" ImageUrl="~/Images/edit.png" ToolTip="Edit" Height="20px" Width="20px" />
<asp:ImageButton ID="imgbtnDelete" CommandName="Delete" Text="Edit" runat="server" ImageUrl="~/Images/RemoveSmall.png" ToolTip="Delete" Height="20px" Width="20px" OnClientClick="javascript:return confirm('Are you sure?');" />
</ItemTemplate>
</asp:TemplateField>
</Columns>
<SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
<HeaderStyle BackColor="Gainsboro" Font-Bold="True" ForeColor="Black" />

<AlternatingRowStyle BackColor="Gainsboro" />
        </asp:GridView>
</div>

        
        
    </ContentTemplate>
</asp:UpdatePanel>
    </div>

    <script type="text/javascript" language="javascript">

    </script>
</asp:Content>

