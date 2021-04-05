<%@ Page Language="VB" AutoEventWireup="false" CodeFile="import.aspx.vb" Inherits="import" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:FileUpload ID="FileUpload1" runat="server" />
                <asp:Button ID="btnLoad" runat="server" Text="Load apk XML"  />

         <asp:Button ID="btnReportDownload" Visible="true" runat="server" Text="DownLoad CSV"  /><br />
        csv field seperated by # and not comma
           <asp:GridView ID="gvReport" runat="server" CssClass="mytable1" >
                     <EmptyDataTemplate><div>No Data Available</div></EmptyDataTemplate>      
                     </asp:GridView>
                     
        <div id="divMsg" runat="server"></div>
    </div>
    </form>
</body>
</html>
