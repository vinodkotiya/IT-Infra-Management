<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Default - Copy.aspx.vb" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Information Technology - Corporate Center</title>
    <meta charset="utf-8">
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
            Information Technology-SCOPE
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
<section class="content"><div class="ic">More Website Templates @ TemplateMonster.com - June 16, 2014!</div>
  <div class="container">
    <div class="row">
      <div class="grid_12">
        <div class="ta__center">
          <h2>Unique Artistic Design for All Tastes</h2>
         <%-- <div class="st1">If you have any questions about this <span class="color1"><a href="http://blog.templatemonster.com/free-website-templates/" rel="nofollow">goodie</a></span>, read the post about it. <br> Find a bunch of alike <span class="color1"><a href="http://www.templatemonster.com/properties/topic/design-photography/" rel="nofollow">designs</a></span> at TemplateMonster’s website.</div>
        --%>  <div class="banners">
            <a href="#" class="banner">
              <img src="images/bann_img1.jpg" alt="">
              <div class="bann_capt"><span>Online Complaint Management System</span></div>
            </a>
            <a href="#" class="banner">
              <img src="images/bann_img2.jpg" alt="">
              <div class="bann_capt"><span>Video Conferencing</span></div>
            </a>
            <a href="#" class="banner">
              <img src="images/bann_img3.jpg" alt="">
              <div class="bann_capt"><span>Softwares</span></div>
            </a>
            <a href="#" class="banner">
              <img src="images/bann_img4.jpg" alt="">
              <div class="bann_capt"><span>Reports</span></div>
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
  <div class="container">
    <div class="row">
      <div class="grid_12">
        <h3 class="head1">Latest News</h3>
      </div>
      <div class="grid_6">
        <img src="images/page1_img1.jpg" alt="" class="img_inner fleft">
        <div class="extra_wrapper">
          <div class="text1">
            <time datetime="2014-01-01">14 June </time>
            <a href="#">Quisque viverra</a>
          </div>
          Dipsum dolor sit amet, consecteturertolom  werto moniko
          sit amet ultricies erat rutruma auctorerttu terolp sadertto
          Integer convallis orci vel mi laoreetatwertlim wernom vert
          Ipsum dolor sit amsecteturertolom  lid ber asrot
        </div>
        <div class="clear"></div>
      </div>
      <div class="grid_6">
        <img src="images/page1_img2.jpg" alt="" class="img_inner fleft">
        <div class="extra_wrapper">
          <div class="text1">
            <time datetime="2014-01-01">17 June </time>
            <a href="#">Kuisque viverrert</a>
          </div>
          Lipsum dolor sit amet, consecteturertolom  dewas terolo
          it amet ultricies erat rutruma auctorerttu nertoli moniko
          Integer convallis orci vel mi laoreetatwertlim wastrolin der
          psum dolor sit amsecteturertolom  saterolo monikom
        </div>
        <div class="clear"></div>
      </div>
    </div>
  </div>
  <div class="clear sep__1 sp__1"></div>
  <div class="container">
    <div class="row">
      <div class="grid_12">
        <h3>Featured Projects</h3>
      </div>
      <div class="grid_3">
        <div class="ih-item circle effect2 left_to_right"><a href="#">
        <div class="img"><img src="images/circ_img1.jpg" alt="img"></div>
        <div class="info">
          <h3>Taxi Company</h3>
          <p>learn more</p>
        </div></a></div>
      </div>
      <div class="grid_3">
        <div class="ih-item circle effect2 left_to_right"><a href="#">
        <div class="img"><img src="images/circ_img2.jpg" alt="img"></div>
        <div class="info">
          <h3>Mod Portfolio</h3>
          <p>learn more</p>
        </div></a></div>
      </div>
      <div class="grid_3">
        <div class="ih-item circle effect2 left_to_right"><a href="#">
        <div class="img"><img src="images/circ_img3.jpg" alt="img"></div>
        <div class="info">
          <h3>Marathon</h3>
          <p>learn more</p>
        </div></a></div>
      </div>
      <div class="grid_3">
        <div class="ih-item circle effect2 left_to_right"><a href="#">
        <div class="img"><img src="images/circ_img4.jpg" alt="img"></div>
        <div class="info">
          <h3>Travel Agency</h3>
          <p>learn more</p>
        </div></a></div>
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
        <div class="copyright"><span class="brand">Web Design</span> &copy; <span id="copyright-year"></span> | <a href="#">Privacy Policy</a> <div>Website designed by <a href="http://www.templatemonster.com/" rel="nofollow">TemplateMonster.com</a></div>
        </div>
      </div>
    </div>
  </div>
</footer>
    </form>
</body>
</html>
