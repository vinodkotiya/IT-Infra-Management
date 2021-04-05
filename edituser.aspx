<%@ Page Language="VB" AutoEventWireup="false" CodeFile="edituser.aspx.vb" Inherits="_edituser" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Information Technology - Corporate Center</title>
    <meta charset="utf-8">

<meta name="format-detection" content="telephone=no">
<link rel="icon" href="images/favicon.ico?">
<link rel="shortcut icon" href="images/favicon.ico?">
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
             <h3>Search NTPC Contacts:</h3>
       <%-- <div class="ta__center">--%>
       
         <%-- <div class="st1">If you have any questions about this <span class="color1"><a href="http://blog.templatemonster.com/free-website-templates/" rel="nofollow">goodie</a></span>, read the post about it. <br> Find a bunch of alike <span class="color1"><a href="http://www.templatemonster.com/properties/topic/design-photography/" rel="nofollow">designs</a></span> at TemplateMonster’s website.</div>
        --%> 
           <asp:Panel ID="pnlLogin" runat="server" Visible="false"> 
                      <div id="main" style="height:100px;" >
                 <div id="first" style="height:100px;">
                  <asp:Label ID="lblLogin" runat="server" Text="Login"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp
                  <asp:TextBox ID="txtLogin" runat="server"></asp:TextBox> <br />
                  <asp:Label ID="lblPwd" runat="server" Text="Password"></asp:Label>&nbsp;&nbsp;
                  <asp:TextBox ID="txtPwd" runat="server" TextMode="Password"></asp:TextBox>&nbsp;&nbsp;<asp:Button ID="btnLogin" runat="server" Text="Sign In" />
                </div></div>   </asp:Panel>
           <asp:Panel ID="pnlSearch" runat="server" Visible="True"> 
             <div id="main" style="height:60px;" >
                 <div id="first" style="height:60px;">
            Enter Keyword :
            <asp:TextBox ID="txtSearch" runat="server" placeholder="Name, Eid, Mobile,IP, Email" Width="200px"></asp:TextBox>
                 <asp:Button ID="btnGo" runat="server" Text="Submit" />
                </div> </div></asp:Panel>
              <div  id="divMsg" runat="server"  style="font-family: Arial, Helvetica, sans-serif; font-size: small; font-style: normal; color: #ff0000; word-spacing: normal; text-indent: inherit; text-align: justify;"  />

           <asp:Panel ID="pnlUpdate" runat="server" Visible="false"> <br />
                          <div id="main" style="height:160px;" >
                 <div id="first" style="height:160px;">
                     <asp:Label ID="lblEid" runat="server" Text=""></asp:Label>
                        <table><tr><td>
                          <asp:Label ID="lblMeetingid" runat="server" Text=""></asp:Label> 
                    Name : </td><td> <asp:TextBox ID="txtName" runat="server"  ReadOnly="True"></asp:TextBox>
                        </td></tr><tr><td>
                        Mobile: </td><td><asp:TextBox ID="txtMobile" runat="server"></asp:TextBox>
                        </td></tr><tr><td>
Email:</td><td>  <asp:TextBox ID="txtEmail"  runat="server"  />
             </td></tr>
                            <tr><td>
IP Address:</td><td>  <asp:TextBox ID="txtIP"  runat="server"  />
             </td></tr>
                            <tr><td>
Extra Info:</td><td>  <asp:TextBox ID="txtExtra"  runat="server"  />
             </td></tr>
                            <tr><td>
AD</td><td>  <asp:CheckBox ID="chkAD" runat="server" />
             </td></tr>
                            <tr><td>           
                      </td><td><asp:Button ID="btnUpdate" runat="server" Text="Update Data"  /></td>      </tr></table>
                  </div></div>  </asp:Panel>
          <asp:GridView Width="1200px" ID="GridView1" runat="server" HeaderStyle-CssClass="gvHeader"
  CssClass="gvRow" 
  AlternatingRowStyle-CssClass="gvAltRow"
  AutoGenerateColumns="false" >
  <Columns>
    <asp:TemplateField>
      <HeaderTemplate>
      <%--  <th colspan="6">Category</th>--%>
       
        <tr class="gvHeader">
          <th></th>
          
          <th>name</th>
             <th>desig</th>
          <th>dept</th>
           <%-- <th>loc </th>--%>
           
             <th  >Mobile </th>
            <th  >email </th>
            <th  >eid </th>
            <th  >ipaddress </th>
            <th  >AD </th>
            <th  >Extra </th>
                    <th>!</th>
        </tr>
      </HeaderTemplate> 
      <ItemTemplate>
         <td><%# Eval("name")%></td>
           <td><%# Eval("desig")%></td>
        <td><%# Eval("dept")%></td>
       <%-- <td><%# Eval("loc")%></td>--%>
          <td><%# Eval("mobile")%></td>
          <td><%# Eval("email")%></td>
           <td><%# Eval("eid")%></td>
           <td><%# Eval("ipaddress")%></td>
           <td><%# Eval("AD")%></td>
           <td><%# Eval("extra")%></td>
                  <td><asp:Button ID="btnEdit" runat="server" Text="Edit"  CommandArgument='<%# Eval("eid")%>' /></td>
       
      </ItemTemplate>
    </asp:TemplateField>
  </Columns>

                       <EmptyDataTemplate><div>No Data Available</div></EmptyDataTemplate>    </asp:GridView>
          <div class="clear"></div>
          <%--<h3>About Us</h3>
          <div class="row">
            <div class="grid_8 preffix_2">
              <div class="st1">Dorem ipsum dolor sit amet, consectetur adipiscing elit. In mollis erat sit amet ultricies erat rutruma auctor, leo magna Integer convallis orci vel mi laoreet, at ornare lorem consequat.</div>
              <a href="#" class="btn">more</a>
            </div>
          </div>--%>
      <%--  </div>--%>
      </div>
    </div>
  </div>
  <div class="clear sep__1"></div>
  
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
        <div class="copyright"><span class="brand">IT-SCOPE</span> &copy; <span id="copyright-year"></span> | <a href="#">Privacy Policy</a> <div>Website designed by <a href="http://www.templatemonster.com/" rel="nofollow">CC-IT</a></div>
        </div>
      </div>
    </div>
  </div>
</footer>
    </form>
</body>
</html>
