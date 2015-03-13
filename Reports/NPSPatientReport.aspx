<%@ Page Title="" Language="C#" MasterPageFile="~/PATS.master" AutoEventWireup="true" CodeFile="NPSPatientReport.aspx.cs" Inherits="Reports_NPSPatientReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div style="width:100%; clear:both; text-align: left;"><h1>
    <asp:Label ID="LabelTitle" runat="server" Text=""></asp:Label></h1>
    <asp:Label ID="LabelResultCount" runat="server" Text=""></asp:Label><br />
        <asp:DataGrid ID="dgResults" runat="server" 
                AlternatingItemStyle-BackColor="Gainsboro" BorderWidth="0px" Width="2000px"  
                AllowPaging="True" AutoGenerateColumns="false"
                onpageindexchanged="dgResults_PageIndexChanged" PageSize="50" 
                onitemdatabound="dgResults_ItemDataBound">
<AlternatingItemStyle BackColor="Gainsboro"></AlternatingItemStyle>
            <PagerStyle Mode="NumericPages" />
            <Columns>
    
    <%--<asp:CommandField ShowEditButton="True" />--%>
        
        <%--<asp:CommandField  />--%>
         <%--<asp:TemplateField ItemStyle-Width = "100px"  HeaderText = "Title">

    <ItemTemplate>
        <asp:Label ID="lblContactName" runat="server" Text='<%# Eval("Title")%>'></asp:Label>
    </ItemTemplate>
    <EditItemTemplate>
        <asp:TextBox ID="txtContactName" runat="server" Text='<%# Eval("Title")%>'></asp:TextBox>
    </EditItemTemplate> 
</asp:TemplateField>--%>
        
       <asp:BoundColumn DataField="Patient Country" HeaderText="Country"  Visible="true" HeaderStyle-Width="50px" ItemStyle-Width="50px"></asp:BoundColumn>
       <asp:BoundColumn DataField="PIN" HeaderText="PIN" HeaderStyle-Width="50px"  ItemStyle-Width="50px" ></asp:BoundColumn>
       <asp:BoundColumn DataField="INITIALS" HeaderText="Intials" HeaderStyle-Width="50px"  ItemStyle-Width="50px"></asp:BoundColumn>
       <asp:BoundColumn DataField="DIAGNOSIS" HeaderText="Diagnosis" HeaderStyle-Width="50px" ItemStyle-Width="50px" ></asp:BoundColumn>
       <asp:BoundColumn DataField="Physician" HeaderText="Physician" HeaderStyle-Width="150px" ItemStyle-Width="150px" ></asp:BoundColumn>
       <asp:BoundColumn DataField="Treatment" HeaderText="Treatment" HeaderStyle-Width="50px" ItemStyle-Width="50px"></asp:BoundColumn>
       <asp:BoundColumn DataField="Status" HeaderText="Status" HeaderStyle-Width="150px" ItemStyle-Width="150px"></asp:BoundColumn>
       <asp:BoundColumn DataField="Initial Notification Date" HeaderText="Initial Notification" HeaderStyle-Width="100px" ItemStyle-Width="100px"></asp:BoundColumn>
       <asp:BoundColumn DataField="New Request Date" HeaderText="Application" HeaderStyle-Width="100px" ItemStyle-Width="100px"></asp:BoundColumn>
       <asp:BoundColumn DataField="GMAAPPROVALDATE" HeaderStyle-Width="100px" HeaderText="GMA Approval" ItemStyle-Width="100px"></asp:BoundColumn>
       <asp:BoundColumn DataField="Denial" HeaderText="Denial" HeaderStyle-Width="100px" ItemStyle-Width="100px"></asp:BoundColumn>
       <asp:BoundColumn DataField="Closure" HeaderText="Closure" HeaderStyle-Width="100px" ItemStyle-Width="100px"></asp:BoundColumn>
       <asp:BoundColumn DataField="Dosage" HeaderText="Dosage" HeaderStyle-Width="50px" ItemStyle-Width="50px"></asp:BoundColumn>
       
    </Columns>
    </asp:DataGrid>
    </div>
</asp:Content>

