<%@ Page Language="VB" AutoEventWireup="false"  EnableEventValidation = "false" CodeFile="vcreport.aspx.vb" Inherits="_vcreport" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Information Technology - Corporate Center</title>
    <meta charset="utf-8">
     <meta http-equiv="refresh" content="18000">
<meta name="format-detection" content="telephone=no">
<link rel="icon" href="images/favicon.ico">
<link rel="shortcut icon" href="images/favicon.ico">
<link rel="stylesheet" href="css/stuck.css">
<link rel="stylesheet" href="css/style.css">
<link rel="stylesheet" href="css/ihover.css">
    
<script src="js/jquery.js"></script>

<script src="js/script.js"></script>
<script src="js/superfish.js"></script>
<script src="js/jquery.equalheights.js"></script>
<script src="js/jquery.mobilemenu.js"></script>
<script src="js/jquery.easing.1.3.js"></script>
<script src="js/tmStickUp.js"></script>
<script src="js/jquery.ui.totop.js"></script>
<script>
 $(document).ready(function(){
  $().UItoTop({ easingType: 'easeOutQuart' });
  $('#stuck_container').tmStickUp({});
  });
</script>
<!--[if lt IE 9]>
 <div style=' clear: both; text-align:center; position: relative;'>
   
</div>
<script src="js/html5shiv.js"></script>
<link rel="stylesheet" media="screen" href="css/ie.css">
<![endif]-->
<!--[if lt IE 10]>
<link rel="stylesheet" media="screen" href="css/ie1.css">
<![endif]-->
</head>
<body>
    <form id="form1" runat="server">
         <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true"></asp:ScriptManager>
    <!--==============================
              header
=================================-->
<header>
<!--==============================
            Stuck menu
=================================-->
  <section id="stuck_container">
    <div class="container">
      <div class="row">
        <div class="grid_12">
        <h1>
          <a href="Default.aspx">
            Information Technology-CC
          </a>
        </h1>
          <div class="navigation ">
            <nav>
              <div id="divMenu" runat="server" />
            </nav>
            <div class="clear"></div>
          </div>
        </div>
      </div>
    </div>
  </section>
</header>
<!--=====================
          Content
======================-->
<section class="content"><div class="ic"></div>
  <div class="container">
    <div class="row">
      <div class="grid_12">
       <%-- <div class="ta__center">--%>
          <asp:UpdatePanel ID="UpdatePanel1" runat="server">
 <Triggers>
     <asp:PostBackTrigger ControlID="LinkButton1" />
      <asp:PostBackTrigger ControlID="btnGo" />
      <asp:AsyncPostBackTrigger ControlID="RadioButtonList1" EventName="SelectedIndexChanged" />
      <asp:AsyncPostBackTrigger ControlID="chkShowTest" EventName="CheckedChanged" />
      <asp:AsyncPostBackTrigger ControlID="chkShowSite" EventName="CheckedChanged" />
    
    </Triggers>
              <ContentTemplate>
         <table border=0><tr><td>
         <fieldset style="border-style: none dashed dashed dashed; border-width: thin; border-color: #000080; background-color: #FFFFFF; color:black; font-size:15px; " class="gvhighlight"><legend><b>Step 1. Video Conferencing Reports:</b></legend>
  
                 <asp:RadioButtonList ID="RadioButtonList1" runat="server" 
                             RepeatDirection="Horizontal" Width="350px" AutoPostBack="True" RepeatColumns="4">
                             <asp:ListItem >Monthly</asp:ListItem>
                             <asp:ListItem Selected="True">Weekly</asp:ListItem>
                            <%-- <asp:ListItem>Projects</asp:ListItem>
                             <asp:ListItem>Vendors</asp:ListItem>--%>
                             <asp:ListItem >Today</asp:ListItem> 
                              <asp:ListItem>All Time</asp:ListItem>
                              
                         </asp:RadioButtonList>
             Range: Start Date:
                <asp:TextBox ID="dt_stTextBox" runat="server" Text="" CssClass="txt100" Width="70px" />
                <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="dt_stTextBox" Format="dd.MM.yyyy">
                                    </asp:CalendarExtender>
                End Date:
                <asp:TextBox ID="dt_endTextBox" runat="server" Text="" CssClass="txt100"  Width="70px" />
                <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="dt_endTextBox"   Format="dd.MM.yyyy" >
                                    </asp:CalendarExtender>
                  <asp:Button ID="btnGo" runat="server" Text="Go" /> <br />
             <asp:CheckBox ID="chkShowTest" runat="server" AutoPostBack="True" Text="Exclude Testing Session" Checked="True" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
             <asp:CheckBox ID="chkShowSite" runat="server" AutoPostBack="True" Text="Exclude Site Names" Checked="True" />
</fieldset>
                <br />
                 <fieldset><legend><b>Step 2. Download Excel File.</b></legend>
                  <asp:LinkButton ID="LinkButton1" runat="server"> <img src="images/xls.gif" width=16 height=16 border=0 align=left /> Click for Excel</asp:LinkButton>
                  </fieldset>
                   <div  id="divMsg" runat="server"  style="font-family: Arial, Helvetica, sans-serif; font-size: small; font-style: normal; color: #000080; word-spacing: normal; text-indent: inherit; text-align: justify;">
        </div>
         
                 </td> <td>
                      <div  id="divSummary" runat="server"   />
                     
</td></tr>
     </table>
    <asp:GridView ID="gvDPR" runat="server"  CssClass="EU_DataTable">
       
    
     
   <%-- <HeaderStyle BackColor="#3E3E3E" Font-Bold="True" Font-Names="cambria" ForeColor="White" />
         <RowStyle Font-Names="Calibri" />--%>

     <EmptyDataTemplate><div>No Data Available</div></EmptyDataTemplate></asp:GridView> <%--</div>--%>
                   Maintenance Log:
                   <asp:GridView ID="GridView1" runat="server"  CssClass="EU_DataTable" >
                       <EmptyDataTemplate><div>No Data Available</div></EmptyDataTemplate></asp:GridView>
                  <asp:Button ID="btnUpdate" runat="server" Text="Fetch New Data from MCU" Enabled="True" visible="false"/>   <br />
    <div  id="divFetchlog" runat="server"  style="font-family: Arial, Helvetica, sans-serif; font-size: small; font-style: normal; color: #ff0000; word-spacing: normal; text-indent: inherit; text-align: justify;"  />
  
                  Redundancy Plan:
                  <img src="upload/vcredundant.jpg" />
                  <asp:Timer ID="Timer1" runat="server" Interval="3000" Enabled="False"></asp:Timer>
     <asp:Timer ID="Timer2" runat="server" Interval="8000" Enabled="False"></asp:Timer>
                    <asp:Timer ID="Timer3" runat="server" Interval="8000" Enabled="False"></asp:Timer>
        <%--</div>--%>
                 
                  </ContentTemplate>
              </asp:UpdatePanel>
      </div>
    </div>
  </div>
  
  
</section>
<!--==============================
              footer
=================================-->
<footer id="footer">
  <div class="container">
    <div class="row">
      <div class="grid_12">
        <div class="socials">
          <a href="#" class="fa fa-twitter"></a>
          <a href="#" class="fa fa-facebook"></a>
          <a href="#" class="fa fa-google-plus"></a>
          <a href="#" class="fa fa-youtube-play"></a>
        </div>
        <div class="copyright"><div  id="divFoot" runat="server" style="display: inline-block;"  /><div>Website designed by <a href="http://10.0.236.165" rel="nofollow">CC-IT</a></div>
        </div>
      </div>
    </div>
  </div>
</footer>
    </form>
</body>
</html>
