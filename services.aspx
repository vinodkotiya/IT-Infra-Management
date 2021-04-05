<%@ Page Language="VB" AutoEventWireup="false" CodeFile="services.aspx.vb" Inherits="_services" %>

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
        <div class="ta__center">
         <%-- <h2>Services</h2>--%>
         <%-- <div class="st1">If you have any questions about this <span class="color1"><a href="http://blog.templatemonster.com/free-website-templates/" rel="nofollow">goodie</a></span>, read the post about it. <br> Find a bunch of alike <span class="color1"><a href="http://www.templatemonster.com/properties/topic/design-photography/" rel="nofollow">designs</a></span> at TemplateMonster’s website.</div>
        --%>  <div class="banners">
            <a href="ocms.aspx" class="banner">
              <img src="images/bann_img1.jpg" alt="" />
              <div class="bann_capt"><span>Online Complaint Management System</span></div>
            </a>
            <a href="asset.aspx" class="banner">
              <img src="images/itasset1.jpg" alt="" />
              <div class="bann_capt"><span>Asset Management System</span></div>
            </a>
            <a href="edituser.aspx" class="banner">
              <img src="images/ad.jpg" alt="" />
              <div class="bann_capt"><span>Active Directory</span></div>
            </a>
            <a href="matrix.aspx" class="banner">
              <img src="images/matrix.jpg" alt="" />
              <div class="bann_capt"><span>Escalation Matrix</span></div>
            </a>
          </div>
          <div class="clear"></div>
          <%--<h3>About Us</h3>
          <div class="row">
            <div class="grid_8 preffix_2">
              <div class="st1">Dorem ipsum dolor sit amet, consectetur adipiscing elit. In mollis erat sit amet ultricies erat rutruma auctor, leo magna Integer convallis orci vel mi laoreet, at ornare lorem consequat.</div>
              <a href="#" class="btn">more</a>
            </div>
          </div>--%>
        </div>
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
        <div class="copyright">  <div  id="divFoot" runat="server" style="display: inline-block;"  /> <div>Website designed by <a href="#" rel="nofollow">CC-IT</a></div>
        </div>
      </div>
    </div>
  </div>
</footer>
    </form>
</body>
</html>
