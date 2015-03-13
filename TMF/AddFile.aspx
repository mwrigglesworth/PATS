<%@ Page Title="" Language="C#" MasterPageFile="~/PATS.master" AutoEventWireup="true" CodeFile="AddFile.aspx.cs" Inherits="TMF_AddFile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<div style="width:100%; clear:both; text-align: left;">
<asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional" >
                        <ContentTemplate>

<div id="ShareFileAdmin" runat="server">
<div class="MessageDivHeader" >
        Add File</div>
        <div class="MessageHeaderRight"></div>
        <div class="MessageDiv">
        
<TABLE id="TABLE1" cellSpacing="2"  cellPadding="1" width="910" border="0" bgColor="">
	<TR>
		<TD width="300px">File Name:
			<asp:RequiredFieldValidator id="RequiredFieldValidator1" runat="server" 
                ErrorMessage="File Name" ControlToValidate="txtFileName" ValidationGroup="NewMessage">*</asp:RequiredFieldValidator>
        </TD>
	           
		<TD><asp:TextBox ID="txtFileName" runat="server" Width="500px" ></asp:TextBox></TD>
	</TR>
    <TR>
		<TD width="300px">File Path:&nbsp;</TD>
		<TD><asp:TextBox ID="txtFilePath" runat="server" Width="500px"></asp:TextBox></TD>
	</TR>
    <tr>
        <td align="right">
            <asp:Button ID="ButtonPost" runat="server" BackColor="Maroon" 
                CssClass="roundbutton" ForeColor="White" onclick="ButtonPost_Click" Text="Post" 
                ValidationGroup="NewMessage" Width="80px" />
            &nbsp;</td>
        <td align="left">
            <asp:Button ID="ButtonCancel" runat="server" BackColor="Maroon" 
                CausesValidation="False" CssClass="roundbutton" ForeColor="White" 
                onclick="ButtonCancel_Click" Text="Cancel" Width="80px" />
        </td>
    </tr>
</TABLE>

</div>

    <%--<asp:GridView  ID="GridFiles" runat="server"         Visible="False"        AutoGenerateColumns="false"
        Width="505px" onrowcancelingedit="GridFiles_RowCancelingEdit" 
        onrowdeleting="GridFiles_RowDeleting" onrowediting="GridFiles_RowEditing" EnableSortingAndPagingCallbacks = "false"
        onrowupdating="GridFiles_RowUpdating" >
        <Columns>
            <asp:BoundField   DataField="FileID" HeaderText="File ID" ItemStyle-CssClass="hideGridColumn" HeaderStyle-CssClass = "hideGridColumn"/>
            <asp:BoundField   DataField="FileName"  HeaderText="File Name" />
            <asp:HyperLinkField DataNavigateUrlFields="NavigateFilePath" Target="_blank" DataTextField = "FilePath" HeaderText="File Path" />
            
            <asp:CommandField ShowEditButton="True" />
            <asp:CommandField ShowDeleteButton="True"  />
        </Columns>

    </asp:GridView>--%>

<asp:ValidationSummary id="ValidationSummary2" runat="server" Width="256px" HeaderText="The Following Fields are Missing:" ValidationGroup="NewMessage"
	ShowMessageBox="True"></asp:ValidationSummary>
    </div>
    
    
        <br /><br />

<div class="MessageDivHeader">Active File(s)</div>
<div class="MessageHeaderRight"></div>
<div class="MessageDiv"><asp:GridView ID="GridFiles" runat="server" Width="920px"  Font-Size="16px"
        AutoGenerateColumns="False"  GridLines="None" CellPadding="4" 
        DataKeyNames="FileID" AllowPaging="False" 
        
        onrowcancelingedit="GridFiles_RowCancelingEdit" 
        onrowdeleting="GridFiles_RowDeleting" onrowediting="GridFiles_RowEditing" 
        onrowupdating="GridFiles_RowUpdating" >
<Columns>
<%--<asp:BoundField   DataField="FileName"  HeaderText="File Name" HeaderStyle-Width="100px" ItemStyle-Width="100px" ></asp:BoundField>--%>

<asp:TemplateField HeaderText="File Name" HeaderStyle-Width="200px" ItemStyle-Width="400px">
<EditItemTemplate>
<asp:TextBox ID="txtFileNameEdit" runat="server" Text='<%#Eval("FileName") %>' Width="400px" />
</EditItemTemplate>
<ItemTemplate>
<asp:Label ID="lblFileName" runat="server"  Text='<%#Eval("FileName") %>' Width="400px"/>
</ItemTemplate>
</asp:TemplateField>

<%--<asp:BoundField DataField="NavigateFilePath" HeaderText="File Path"  Visible="true" HeaderStyle-Width="500px" ItemStyle-Width="500px" HtmlEncode="false"></asp:BoundField>--%>

<asp:TemplateField HeaderText="File Path" HeaderStyle-Width="400px" ItemStyle-Width="400px">
<EditItemTemplate>
<asp:TextBox ID="txtFilePathEdit" runat="server" Text='<%#Eval("StripFilePath") %>' Width="400px" TextMode="MultiLine" />
</EditItemTemplate>
<ItemTemplate>
<asp:Label ID="lblFilePath" runat="server" style="word-wrap:break-word;"  Text='<%#Eval("NavigateFilePath") %>' Width="400px" />
</ItemTemplate>
</asp:TemplateField>
<%--<asp:BoundField DataField="CountryName" HeaderText="Country"  Visible="true" HeaderStyle-Width="50px" ItemStyle-Width="50px"></asp:BoundField>
<asp:BoundField DataField="Message" HeaderText="Message"  Visible="true" HeaderStyle-Width="200px" ItemStyle-Width="200px"></asp:BoundField>
<asp:BoundField DataField="CreatedOn" HeaderText="Date"  Visible="true" HeaderStyle-Width="100px" ItemStyle-Width="100px"></asp:BoundField>
--%><asp:TemplateField HeaderStyle-Width="70px" ItemStyle-Width="70px" HeaderText="Action">
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
</asp:Content>


