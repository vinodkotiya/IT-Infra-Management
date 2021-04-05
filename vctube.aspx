<%@ Page Language="VB" AutoEventWireup="false" CodeFile="vctube.aspx.vb" Inherits="_vctube" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Information Technology - Corporate Center</title>
    <meta charset="utf-8">
    <link rel="icon" href="images/favicon.ico?">
<link rel="shortcut icon" href="images/favicon.ico?">
 <!-- Basic Page Needs
  ================================================== -->
	<meta charset="utf-8">
	<meta name="description" content="Free Responsive Html5 Css3 Templates | zerotheme.com">
	<meta name="author" content="www.zerotheme.com">
	
    <!-- Mobile Specific Metas
  ================================================== -->
	<meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1">
    
    <!-- CSS
  ================================================== -->
  	<link rel="stylesheet" href="talk/css/zerogrid.css">
	<link rel="stylesheet" href="talk/css/style.css">
	<link rel="stylesheet" href="talk/css/menu.css">
	<!-- Owl Carousel Assets -->
	<link href="talk/css/owl.carousel.css" rel="stylesheet">
    <link href="talk/css/owl.theme.css" rel="stylesheet">
	<!-- Custom Fonts -->
    <link href="talk/font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css">
	
	<!--[if lt IE 8]>
       <div style=' clear: both; text-align:center; position: relative;'>
         <a href="http://windows.microsoft.com/en-US/internet-explorer/products/ie/home?ocid=ie6_countdown_bannercode">
           <img src="http://storage.ie6countdown.com/assets/100/talk/images/banners/warning_bar_0000_us.jpg" border="0" height="42" width="820" alt="You are using an outdated browser. For a faster, safer browsing experience, upgrade for free today." />
        </a>
      </div>
    <![endif]-->
    <!--[if lt IE 9]>
		<script src="js/html5.js"></script>
		<script src="js/css3-mediaqueries.js"></script>
	<![endif]-->
    <!-- File download counter ajax call --> 
   <script  type="text/javascript">
       function fileCount(name) {
         //  alert('');
            $.ajax({
                type: "POST",
                url: "vinservice.asmx/updatefileCount",
                data: "{filename:'" + name + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSuccess,
                error: OnError
            });
       }
       function OnSuccess(data, status) {
        // alert(data.d);  //no need to display on success
           //  $x("#testcontainer").html(data.d);
         //  $("#divNotify").html('success ' + data.d);
       }
       function OnError(request, status, error) {
          // alert('error ' + request.statusText);
             $("#divNotify").html('error ' + request.statusText);
         }
        </script>
<!-- File download counter ajax call -->
<!--[if lt IE 9]>
 <div style=' clear: both; text-align:center; position: relative;'>
   
</div>
<script src="js/html5shiv.js"></script>
<link rel="stylesheet" media="screen" href="css/ie.css">
<![endif]-->
<!--[if lt IE 10]>
<link rel="stylesheet" media="screen" href="css/ie1.css">
<![endif]-->
  <%--For comment modules--%>

    <script type="text/javascript" language="javascript">
        function CopyLink() {
            //  window.clipboardData.setData("Text", location.href);
            var $temp = $("<input>");
            $("body").append($temp);
            $temp.val(location.href).select();
            document.execCommand("copy");
            $temp.remove();
            alert("Video Link Copied. Paste it anywhere to share.");
        }
        function setReplyID(option) {
          // alert(option);
           document.getElementById('txtReplyID').value = option;
           document.getElementById('txtComment').value = 'Please enter your reply here for comment #' + option;
           
            return false;
        }
        function Decide(option) {
            var temp = "";
            /*document.getElementById('lblRate').innerText = "";*/
            if (option == 1) {
                document.getElementById('Rating1').className = "Filled";
                document.getElementById('Rating2').className = "Empty";
                document.getElementById('Rating3').className = "Empty";
                document.getElementById('Rating4').className = "Empty";
                document.getElementById('Rating5').className = "Empty";
                temp = "1";
               
            }
            if (option == 2) {
                document.getElementById('Rating1').className = "Filled";
                document.getElementById('Rating2').className = "Filled";
                document.getElementById('Rating3').className = "Empty";
                document.getElementById('Rating4').className = "Empty";
                document.getElementById('Rating5').className = "Empty";
                temp = "2";
              
            }
            if (option == 3) {
                document.getElementById('Rating1').className = "Filled";
                document.getElementById('Rating2').className = "Filled";
                document.getElementById('Rating3').className = "Filled";
                document.getElementById('Rating4').className = "Empty";
                document.getElementById('Rating5').className = "Empty";
                temp = "3";
            
            }
            if (option == 4) {
                document.getElementById('Rating1').className = "Filled";
                document.getElementById('Rating2').className = "Filled";
                document.getElementById('Rating3').className = "Filled";
                document.getElementById('Rating4').className = "Filled";
                document.getElementById('Rating5').className = "Empty";
                temp = "4";
               
            }
            if (option == 5) {
                document.getElementById('Rating1').className = "Filled";
                document.getElementById('Rating2').className = "Filled";
                document.getElementById('Rating3').className = "Filled";
                document.getElementById('Rating4').className = "Filled";
                document.getElementById('Rating5').className = "Filled";
                temp = "5";
              
            }
            document.getElementById('txtRate').value = temp;
            return false;
        }
        </script>
     <script  type="text/javascript">
         var cid = '';
       function fileCount1(name,ip) {
           // alert('Are You Really want to Delete attachment??');
           cid = name;
            $.ajax({
                type: "POST",
                url: "vinservice.asmx/setLikes",
                data: "{fileid:'" + name + "',ip:'" + ip + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSuccess6,
                error: OnError6
            });
       }
       function OnSuccess6(data, status) {
          document.getElementById("cid" + cid).innerHTML = "Liked";
           //var a = document.getElementById("cid" + cid);
           //a.style.display = 'none';
          //alert("cid" + cid);

        //alert(data.d);  //no need to display on success
           //  $x("#testcontainer").html(data.d);
           //  $("#divNotify").html('success ' + data.d);
          // window.location.reload(true);
       }
       function OnError6(request, status, error) {
          alert('error ' + request.statusText);
            // $("#divNotify").html('error ' + request.statusText);
       }
       function deleteComment(name, ip) {
           // alert('Are You Really want to Delete attachment??');
           cid = name;
           $.ajax({
               type: "POST",
               url: "vinservice.asmx/delComment",
               data: "{fileid:'" + name + "'}",
               contentType: "application/json; charset=utf-8",
               dataType: "json",
               success: OnSuccess1,
               error: OnError1
           });
       }
       function OnSuccess1(data, status) {
           alert('comment deleted');
           window.location.reload(true);
       }
       function OnError1(request, status, error) {
           alert('error ' + request.statusText);
           // $("#divNotify").html('error ' + request.statusText);
       }
        </script>
    <%--For comment modules--%>
</head>
<body>
    <form id="form1" runat="server">
           <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true"></asp:ScriptManager>
    <!--==============================
              header
=================================-->
         <asp:UpdatePanel ID="upAppraisal" runat="server">
            <ContentTemplate>
<!--////////////////////////////////////Header-->
	<header>
		<div class="wrap-header">
			<div class="zerogrid">
				<div class="row">
					<h2><img src="talk/favicon.ico" style="width:60px; height:40px;" />Video Library</h2>
					
				</div>
			</div>
		</div>
		
    </header>
	<!--////////////////////////////////////Menu-->
	<a href="#" class="nav-toggle">Toggle Navigation</a>
    <nav class="cmn-tile-nav">
		<ul class="clearfix">
			<li class="colour-1"><a href="Default.aspx">Home</a></li>
			<li class="colour-2"><a href="#">Latest</a></li>
			<li class="colour-3"><a href="#">Announcements</a></li>
			<li class="colour-4"><a href="#">e-Learning</a></li>
			<li class="colour-5"><a href="#">Help</a></li>
			<li class="colour-6"><a href="#">Tech</a></li>
			<li class="colour-7"><a href="#">Other</a></li>
			<li class="colour-8"><a href="#l">More</a></li>
		</ul>
    </nav>
<!--=====================
          Content
======================-->
<!--////////////////////////////////////Container-->
	<section id="container" class="index-page">
		<div class="wrap-container zerogrid">
			<div class="row">
				<div id="main-content" class="col-2-3">
					<div class="wrap-vid" id="divVideo" runat="server">
                        <%--<video width="100%" height="400" controls>
                            <source src="http://10.0.236.168/ccit/upload/vcr/d14a1_170611214837136_384.mp4" type="video/mp4">
</video>--%>
					
					</div>
					<div class="row">
						<div class="share">
							<div class="col-1-4">
								<div class="wrap-col">
									<div class="box-share">
										<a href="#comment">
											<i class="fa fa-comments"></i>
											<span>Comments</span>
										</a>
									</div>
								</div>
							</div>
							<div class="col-1-4">
								<div class="wrap-col">
									<div class="box-share">
										<a href=" " onclick="CopyLink()">
											<i class="fa fa-gift"></i>
											<span>Share </span>
										</a>
									</div>
								</div>
							</div>
							<div class="col-1-4">
								<div class="wrap-col">
									<div class="box-share">
										<a href="#">
											<i class="fa fa-eye"></i>
											<span id="divView" runat="server"></span>
										</a>
									</div>
								</div>
							</div>
							<div class="col-1-4">
								<div class="wrap-col">
									<div class="box-share" id="divDownload" runat="server">
									<%--	<a href="#">
											<i class="fa fa-download"></i>
											<span>Download</span>
										</a>--%>
									</div>
								</div>
							</div>
						</div>
					</div>
                   <div id="divDetail" runat="server">
					<%--<h1 class="vid-name">NTPC CMD Gurdeep Singh Talks To ET NOW</h1>
					<div class="info">
						<h5>By <a href="#">Vinod</a></h5>
						<span><i class="fa fa-calendar"></i>25/3/2015</span> 
						<span><i class="fa fa-heart"></i>1,200</span>
					</div>
					<p>Catch Gurdeep Singh - CMD, NTPC in an exclusive conversation with ET NOW as he speaks about their way forward and more..</p>
					<div class="tags">
						<a href="#">CMD</a>
						<a href="#">NTPC</a>
						<a href="#">Talk</a>
						<a href="#">Interview</a>
						<a href="#">News</a>
					</div>--%>
                   </div>
					<section class="vid-related">
						<div class="header" id="divAll" runat="server">
							<%--<h2>All Videos</h2>--%>
						</div>
						<div class="row" runat="server" id="divMore"><!--Start Box-->
							<%--<div id="owl-demo-1" class="owl-carousel"  >--%>
								<%--<div class="item wrap-vid">
									<div class="zoom-container">
										<a href="single.html">
											<span class="zoom-caption">
												<i class="icon-play fa fa-play"></i>
											</span>
											<img src="talk/images/4.jpg" />
										</a>
									</div>
									<h3 class="vid-name"><a href="#">Video's Name</a></h3>
									<div class="info">
										<h5>By <a href="#">Kelvin</a></h5>
										<span><i class="fa fa-calendar"></i>25/3/2015</span> 
										<span><i class="fa fa-heart"></i>1,200</span>
									</div>
								</div>--%>
								
							<%--</div>--%>
						</div>
					</section>
                    <section class="vid-related">
						<div class="header" >
							<h2>Comments</h2>
						</div>
						<div class="row" ><!--Start Box-->
							 <div class="reservation"  >
                   
                      <!-- comments container -->
		<div class="comment_block">

		<!-- 
			Comments are structured in the following way:

			{ul} defines a new comment (singular)
			{li} defines a new reply to the comment {ul}

			example:

			<ul>
				<comment>
					
				</comment

					<li>
						<reply>

						</reply>
					</li>

					<li>
						<reply>

						</reply>
					</li>

					<li>
						<reply>

						</reply>
					</li>
			</ul>

		 -->

		 <!-- used by #{user} to create a new comment -->
             <%-- <asp:UpdatePanel ID="UpdatePanel12" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
            <ContentTemplate>--%>
            <a name="comment"></a>
		 <div class="create_new_comment">

			<!-- Hack is use comment#name#pic current #{user} avatar -->
		 	<div class="user_avatar">
		 		<img src="images/73x73.png">
		 	</div><!-- the input field --><div class="input_comment">
          <div style="width:200px;">    <asp:Button BorderStyle="None" ID="Rating1" onmouseover="return Decide(1);" OnClientClick="return Decide(1);" Height="20px" Width="20px" CssClass="Empty" runat="server" />
    <asp:Button BorderStyle="None" ID="Rating2" onmouseover="return Decide(2);" OnClientClick="return Decide(2);"     Height="20px" Width="20px" CssClass="Empty" runat="server" />
    <asp:Button BorderStyle="None" ID="Rating3" onmouseover="return Decide(3);" OnClientClick="return Decide(3);"     Height="20px" Width="20px" CssClass="Empty" runat="server" />
    <asp:Button BorderStyle="None" ID="Rating4" onmouseover="return Decide(4);" OnClientClick="return Decide(4);"     Height="20px" Width="20px" CssClass="Empty" runat="server" />
    <asp:Button BorderStyle="None" ID="Rating5" onmouseover="return Decide(5);" OnClientClick="return Decide(5);"     Height="20px" Width="20px" CssClass="Empty" runat="server" />
               <asp:TextBox ID="txtRate" runat="server"  Text="" Width="5px"  tabindex="-1000" style="border:0px;background-color:transparent;" />
               <asp:TextBox ID="txtReplyID" runat="server"  Text="" Width="5px"  tabindex="-1000" style="border:0px;background-color:transparent;" />
                 <%--ForeColor="#b0c8e8"--%></div>
                 <input type="text" ID="txtComment" runat="server" style="width:80%; height :60px; font-size:14px;" TextMode="MultiLine" placeholder="Join the conversation..or Give your Review... or Ask a Question??"  /> <br />
              
                
                 <input type="text"  ID="txtCommentEMail" style="width:250px;font-size:14px;" runat="server" placeholder="Employee Number"  />
                 <asp:Button ID="btnSubmitComment" runat="server" Width="50" Height="20" Text="Go" OnClick="btnSubmitComment_Click" />
                <br /> <asp:Label ID="lbMsgComment" runat="server" Text="We are recording IP address. Don't misuse the feature." Font-Names="verdana" Font-Size="Small" CssClass="user" />
		 		 
		 	</div>
           

		 </div>
             <%-- </contenttemplate>
                  </asp:UpdatePanel>--%>

		 <!-- new comment -->
            <div id="divComments" runat="server" >
	<%--	 <div class="new_comment">

			<!-- build comment -->
		 	<ul class="user_comment">

		 		<!-- current #{user} avatar -->
			 	<div class="user_avatar">
			 		<img src="https://s3.amazonaws.com/uifaces/faces/twitter/dancounsell/73.jpg">
			 	</div><!-- the comment body --><div class="comment_body">
			 		<p>Gastropub cardigan jean shorts, kogi Godard PBR&B lo-fi locavore. Organic chillwave vinyl Neutra. Bushwick Helvetica cred freegan, crucifix Godard craft beer deep v mixtape cornhole Truffaut master cleanse pour-over Odd Future beard. Portland polaroid iPhone.</p>
			 	</div>

			 	<!-- comments toolbar -->
			 	<div class="comment_toolbar">

			 		<!-- inc. date and time -->
			 		<div class="comment_details">
			 			<ul>
			 				<li><i class="fa fa-clock-o"></i> 13:94</li>
			 				<li><i class="fa fa-calendar"></i> 04/01/2015</li>
			 				<li><i class="fa fa-pencil"></i> <span class="user">John Smith</span></li>
			 			</ul>
			 		</div><!-- inc. share/reply and love --><div class="comment_tools">
			 			<ul>
			 				<li><i class="fa fa-share-alt"></i></li>
			 				<li><i class="fa fa-reply"></i></li>
			 				<li><i class="fa fa-heart love"></i></li>
			 			</ul>
			 		</div>

			 	</div>

			 	<!-- start user replies -->
		 	<li>
		 		
		 		<!-- current #{user} avatar -->
			 	<div class="user_avatar">
			 		<img src="https://s3.amazonaws.com/uifaces/faces/twitter/manugamero/73.jpg">
			 	</div><!-- the comment body --><div class="comment_body">
			 		<p><div class="replied_to"><p><span class="user">John Smith:</span>Gastropub cardigan jean shorts, kogi Godard PBR&B lo-fi locavore. Organic chillwave vinyl Neutra. Bushwick Helvetica cred freegan, crucifix Godard craft beer deep v mixtape cornhole Truffaut master cleanse pour-over Odd Future beard. Portland polaroid iPhone.</p></div>That's exactly what I was thinking!</p>
			 	</div>

			 	<!-- comments toolbar -->
			 	<div class="comment_toolbar">

			 		<!-- inc. date and time -->
			 		<div class="comment_details">
			 			<ul>
			 				<li><i class="fa fa-clock-o"></i> 14:52</li>
			 				<li><i class="fa fa-calendar"></i> 04/01/2015</li>
			 				<li><i class="fa fa-pencil"></i> <span class="user">Andrew Johnson</span></li>
			 			</ul>
			 		</div><!-- inc. share/reply and love --><div class="comment_tools">
			 			<ul>
			 				<li><i class="fa fa-share-alt"></i></li>
			 				<li><i class="fa fa-reply"></i></li>
			 				<li><i class="fa fa-heart love"><span class="love_amt"> 4</span></i></li>
			 			</ul>
			 		</div>

			 	</div>


		 	</li>

		 		<!-- start user replies -->
		 	<li>
		 		
		 		<!-- current #{user} avatar -->
			 	<div class="user_avatar">
			 		<img src="https://s3.amazonaws.com/uifaces/faces/twitter/ManikRathee/73.jpg">
			 	</div><!-- the comment body --><div class="comment_body">
			 		<p><div class="replied_to"><p><span class="user">John Smith:</span>Gastropub cardigan jean shorts, kogi Godard PBR&B lo-fi locavore. Organic chillwave vinyl Neutra. Bushwick Helvetica cred freegan, crucifix Godard craft beer deep v mixtape cornhole Truffaut master cleanse pour-over Odd Future beard. Portland polaroid iPhone.</p></div>Finally someone who actually gets it!<div class="replied_to"><p><span class="user">Andrew Johnson:</span>That's exactly what I was thinking!</p></div>That's awesome!</p>
			 	</div>

			 	<!-- comments toolbar -->
			 	<div class="comment_toolbar">

			 		<!-- inc. date and time -->
			 		<div class="comment_details">
			 			<ul>
			 				<li><i class="fa fa-clock-o"></i> 14:59</li>
			 				<li><i class="fa fa-calendar"></i> 04/01/2015</li>
			 				<li><i class="fa fa-pencil"></i> <span class="user">Simon Gregor</span></li>
			 			</ul>
			 		</div><!-- inc. share/reply and love --><div class="comment_tools">
			 			<ul>
			 				<li><i class="fa fa-share-alt"></i></li>
			 				<li><i class="fa fa-reply"></i></li>
			 				<li><i class="fa fa-heart love"><span class="love_amt"> 4039</span></i></li>
			 			</ul>
			 		</div>

			 	</div>


		 	</li>

		 	</ul>

		 </div>--%>
            </div>


		</div>
                   <%--   End of Comment box--%>
                      <asp:Label ID="lbPkgID" runat="server" Text="" Font-Size="8px" />
                   </div>
						</div>
					</section>
				</div>
				<div id="sidebar" class="col-1-3">
					<form id="form-container" action="">
						<!--<input type="submit" id="searchsubmit" value="" />-->
						<a class="search-submit-button" href="javascript:void(0)">
							<i class="fa fa-search"></i>
						</a>
						<div id="searchtext">
							<input type="text" id="s" name="s" placeholder="Search Something...">
						</div>
					</form>
					<!---- Start Widget ---->
                    <div class="widget wid-post">
						<div class="wid-header">
							<h5>Popular Videos</h5>
						</div>
						<div class="wid-content" id="divPopular" runat="server">
							
						</div>
					</div>
					<div class="widget wid-post">
						<div class="wid-header">
							<h5>Latest Videos</h5>
						</div>
						<div class="wid-content" id="divLatest" runat="server">
							<%--<div class="post wrap-vid">
								<div class="zoom-container">
									<a href="single.html">
										<span class="zoom-caption">
											<i class="icon-play fa fa-play"></i>
										</span>
										<img src="talk/images/4.jpg" />
									</a>
								</div>
								<div class="wrapper">
									<h5 class="vid-name"><a href="#">NTPC Corporate Video</a></h5>
									<div class="info">
										<h6>By <a href="#">Kelvin</a></h6>
										<span><i class="fa fa-calendar"></i>25/3/2015</span> 
										<span><i class="fa fa-heart"></i>1,200</span>
									</div>
								</div>
							</div>
							--%>
						</div>
					</div>
					
						</div>
					</div>
				</div>
			</div>
		</div>
	</section>
                
	<!--////////////////////////////////////Footer-->
	<footer>
		
		<div class="zerogrid copyright">
			<div class="wrapper">
				  <div class="copyright">  <div  id="divFoot" runat="server" style="display: inline-block;"  /> <div>Website designed by <a href="#" rel="nofollow">CC-IT</a></div>
                      <div id="divNotify" runat="server" />
			</div>
		</div>
	</footer>

	<!-- Slider -->
	<script src="talk/js/jquery-2.1.1.js"></script>
	<script src="talk/js/demo.js"></script>
	<!-- Search -->
	<script src="talk/js/modernizr.custom.js"></script>
	<script src="talk/js/classie.js"></script>
	<script src="talk/js/uisearch.js"></script>
	<script>
		new UISearch( document.getElementById( 'sb-search' ) );
	</script>
	<!-- Carousel -->
	<script src="talk/js/owl.carousel.js"></script>
    <script>
    $(document).ready(function() {

      $("#owl-demo-1").owlCarousel({
        items : 4,
        lazyLoad : true,
        navigation : true
      });
	  $("#owl-demo-2").owlCarousel({
        items : 4,
        lazyLoad : true,
        navigation : true
      });

    });
    </script>
</div>
                </ContentTemplate>
              <Triggers>
                  <%--  <asp:PostBackTrigger ControlID="btnUpload" />
                 <asp:PostBackTrigger ControlID="btnLogin" />
                   <asp:PostBackTrigger ControlID="ibtnNew" />
                   <asp:PostBackTrigger ControlID="btnXls" />
                  <asp:PostBackTrigger ControlID="btnCreateMeeting" />--%>
                  
               <%-- <asp:PostBackTrigger ControlID="btnUpdateProj" />
                    <asp:AsyncPostBackTrigger ControlID="ddlProject" EventName="SelectedIndexChanged" />
                                       <asp:AsyncPostBackTrigger ControlID="ddlUsers" EventName="SelectedIndexChanged" />
                     <asp:AsyncPostBackTrigger ControlID="RadioButtonList1" EventName="SelectedIndexChanged" />--%>

                                            </Triggers>
            </asp:UpdatePanel>
    </form>
</body>
     <!--Start of Tawk.to Script-->
<script type="text/javascript">
var Tawk_API=Tawk_API||{}, Tawk_LoadStart=new Date();
(function(){
var s1=document.createElement("script"),s0=document.getElementsByTagName("script")[0];
s1.async=true;
s1.src='https://embed.tawk.to/5954da97e9c6d324a4737ecd/default';
s1.charset='UTF-8';
s1.setAttribute('crossorigin','*');
s0.parentNode.insertBefore(s1,s0);
})();
</script>
</html>
