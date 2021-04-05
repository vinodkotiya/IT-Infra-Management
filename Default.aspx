<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="_Default" %>

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

  $('.count').each(function () {
      $(this).prop('Counter', 0).animate({
          Counter: $(this).text()
      }, {
          duration: 4000,
          easing: 'swing',
          step: function (now) {
              $(this).text(Math.ceil(now));
          }
      });
  });

  
 });

</script>
   
    <script>
        $(document).ready(function () {
               $('.modal-wrapper').toggleClass('open');
               $('.page-wrapper').toggleClass('blur-it');
               setTimeout(function () {
                   document.getElementById('vinModal').style.display = 'none';
               }, 10000);

               var timeleft = 10;
               var downloadTimer = setInterval(function () {
                   timeleft = timeleft - 1;
                   document.getElementById("progressBar").value = timeleft;
                   if (timeleft <= 0)
                       clearInterval(downloadTimer);
               }, 1000);

                return false;
          
        });

    </script>
    <script>
        // Get the modal
       // var modal = document.getElementById('vinModal');

        // When the user clicks anywhere outside of the modal, close it
        $('body').click(function (e) {
           // alert('hi');
            if (!$(e.target).closest('.modal-wrapper').length) {
                modal.style.display = "none";
                $(".modal-wrapper").hide();
            }
        });
    </script>
     <%-- <script src="js/jquery.bpopup.min.js"></script>
     <script type="text/javascript" >
       ///also uncomment div id="pop_up_1time" in html
         $(window).load(function() {
             $('#pop_up_1time').bPopup({
                 content: 'image', //'ajax', 'iframe' or 'image'
                 contentContainer: '.content',
                 loadUrl: 'images/bann_img1.jpg', //Uses jQuery.load() vin.jpg
                 position: [250, 150], //x, y,
                 positionStyle: 'absolute', //'fixed' or 'absolute'
                 fadeSpeed: 'slow', //can be a string ('slow'/'fast') or int
                 followSpeed: 1500, //can be a string ('slow'/'fast') or int
                 modalColor: 'white',
                 easing: 'easeOutBack', //uses jQuery easing plugin
                 speed: 3000,
                 transition: 'slideDown',
                 // onOpen: function () { alert('onOpen fired:' + i + j); },
                 //onClose: function () { alert('onClose fired'); }
                  autoClose: 10000 //Auto closes after 1000ms/1sec

             });
         })
        </script>--%>
<!--[if lt IE 9]>
 <div style=' clear: both; text-align:center; position: relative;'>
   
</div>
<script src="js/html5shiv.js"></script>
<link rel="stylesheet" media="screen" href="css/ie.css">
<![endif]-->
<!--[if lt IE 10]>
<link rel="stylesheet" media="screen" href="css/ie1.css">
<![endif]-->
    <link href="talk/font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css">
    <style>
        .vid-name{margin-top: 5px;}
.vid-name a{font-size: 19px;color: #654E2D;}
    </style>
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
                  <div class="countlabel"> Complaints Resolved: <div class="countlabel count" id="divOCMSCount" runat="server">768</div></div>
              <div class="bann_capt"><span>Online Complaint Management System</span></div>
            </a>
            <a href="vc.aspx" class="banner">
              
              <img src="images/bann_img2.jpg" alt="" />
                <div class="countlabel"> Sessions Done: <div class="countlabel count" id="divVcCount" runat="server">768</div></div>
              <div class="bann_capt">  <span>Video Conferencing</span></div>
            </a>
            <a href="nw.aspx" class="banner">
              <img src="images/bann_img3.jpg" alt="" />
              <div class="bann_capt"><span>Network</span></div>
            </a>
            <a href="vcreport.aspx" class="banner">
              <img src="images/bann_img4.jpg" alt="" />
              <div class="bann_capt"><span>Reports</span></div>
            </a>
          </div>
          <div class="clear"></div>
         
        
        </div>
      </div>
    </div>
  </div>
  <div class="clear sep__1"></div>
    <div class="row">
        
            <div class="grid_8 preffix_2">
                 <h3>Vision</h3>
              <div class="st1">'To be the World's Leading Power Company, Energizing India's Growth'</div>
                 <h3>Mission</h3>
              <div class="st1">  'Provide Reliable Power and Related Solutions in an Economical, Efficient and Environment friendly manner, driven by Innovation and Agility' </div>
              <a href="#" class="btn">Core Values - ICOMIT <br />
•
Integrity
•
Customer Focus
•
Organisational Pride
•
Mutual Respect & Trust
•
Innovation & Learning
•
Total Quality & Safety
</a>
              
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
        <div class="copyright">  <div  id="divFoot" runat="server" style="display: inline-block;"  /> <div>Website designed by <a href="#" rel="nofollow">CC-IT</a></div>
        </div>
      </div>
    </div>
  </div>
 <!-- Modal -->
<div class="modal-wrapper" id="vinModal">
  <div class="modal">
    <div class="headv">
      <span onclick="document.getElementById('vinModal').style.display='none'" 
        class="btn-closev">&times;</span>
    </div>
    <div class="contentv">
        <div id ="divPop" runat="server" />
        <%--<div class="good-job">
        <i class="fa fa-thumbs-o-up" aria-hidden="true"></i>
          
        </div>--%>
    </div>
  </div>
</div>
</footer>
    </form>
</body>
</html>
