<%@ Page Language="VB" AutoEventWireup="false" CodeFile="mcu.aspx.vb" Inherits="mcu" ValidateRequest = "false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:TextBox ID="txtXML" runat="server" TextMode="MultiLine" Width="500px"  Height ="300px"></asp:TextBox><asp:Button ID="btnMCULogin" runat="server" Text="<<-MCU Login" /> 
        <asp:Button ID="btnCDRList" runat="server" Text="<<-CDR List" />
        <asp:Button ID="btnCDRFull" runat="server" Text="<<-CDR Full" /> <br />
        <asp:Button ID="btnPost" runat="server" Text="Button" /> <br />
        <asp:TextBox ID="txtResponse" runat="server" TextMode="MultiLine" width ="500px" Height ="300px"></asp:TextBox>
        <asp:Button ID="btnXML" runat="server" Text="Load XML" /> <br />
        <asp:Button ID="btnCDRFullProcess" runat="server" Text="Load cdr full in database" />
        <asp:GridView ID="GridView1" runat="server"></asp:GridView>
    </div>
    </form>
</body>
</html>
