<%@ Page Language="VB" AutoEventWireup="false" CodeFile="saplogin.aspx.vb" Inherits="saplogin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:TextBox ID="txtEID" runat="server" Visible="false" Text="0000"></asp:TextBox><br />
        <asp:TextBox ID="txtURI" runat="server" Visible="false"></asp:TextBox>
        <asp:DropDownList ID="ddlURI" runat="server" Visible="false" DataTextField="page" DataValueField="page" Width="300px"></asp:DropDownList>
         <asp:Button ID="btnLoad" runat="server" Text="Load" Visible="false" />
    </div>
    </form>
</body>
</html>
