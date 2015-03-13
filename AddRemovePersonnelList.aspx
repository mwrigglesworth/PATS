<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddRemovePersonnelList.aspx.cs" Inherits="AddRemovePersonnelList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
     <input type="hidden" id="patientid" value="<%=SenderId%>" />
     <input type="hidden" id="transdata" value="<%=TransferData%>" />
     <input type="hidden" id="suggestion" value="<%=Suggestions%>" />
     <asp:Panel ID="NoPersonnelPanel" runat="server" Visible="false">
     <div style="text-align: center">
        No <%=AddType%> available</div>
     </asp:Panel>
     <asp:Panel ID="PersonnelPanel" runat="server">
   <div class="FormTable">
           
          <div style="overflow-y:scroll; height:400px"> 
          <asp:RadioButtonList  Visible="false" ID="rdPersonnelList" runat="server" RepeatColumns="3" RepeatDirection="Vertical" RepeatLayout="Table" CellPadding="5" CellSpacing="5" >
        </asp:RadioButtonList>
          <asp:CheckBoxList Visible="false" ID="chkBoxLst" runat="server" RepeatColumns="3" RepeatDirection="Vertical" RepeatLayout="Table" CellPadding="5" CellSpacing="5" >
        </asp:CheckBoxList>
        </div>
    </div><div>
            <div style="text-align:center"><input style="text-align:center; background-color:#7faed4; color:White; padding:5px; font-size:medium"  type="button" id="btnUpdateList" value="Update" />
            </div></div>
</asp:Panel>
    </form>
</body>
</html>
