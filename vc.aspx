<%@ Page Language="VB" AutoEventWireup="false" CodeFile="vc.aspx.vb" Inherits="_vc" %>

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
    <script src="js/jquery.dynDateTime.min.js"></script>
    <script src="js/calendar-en.min.js"></script>
    <link href="css/calendar-blue.css" rel="stylesheet" />
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
    <script type="text/javascript">
    $(document).ready(function () {
        $("#<%=txtSchStart.ClientID %>").dynDateTime({
            showsTime: true,
            ifFormat: "%Y-%m-%d %H:%M",
            daFormat: "%l;%M %p, %e %m, %Y",
            align: "BR",
            electric: false,
            singleClick: false,
            displayArea: ".siblings('.dtcDisplayArea')",
            button: ".next()"
        });
    });
</script>
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
    <!-- Owl Carousel Assets -->
<link href="css/owl.carousel.css" rel="stylesheet" />
<script src="js/owl.carousel.js"></script>
		<script>
			$(document).ready(function() {

				$("#owl-demo").owlCarousel({
					items : 6,
					lazyLoad : true,
					autoPlay : true,
					navigation : true,
					navigationText : ["", ""],
					rewindNav : true,
					scrollPerPage : false,
					pagination : false,
					paginationNumbers : false,
				});

			});
		</script>
		<!-- //Owl Carousel Assets -->
    <script language='Javascript' type="text/javascript">
      function addFile() {
            var ni = document.getElementById("fileDiv");
            var objFileCount = document.getElementById("fileCount");
            var num = (document.getElementById("fileCount").value - 1) + 2;
            objFileCount.value = num;
            var newdiv = document.createElement("div");
            var divIdName = "file" + num + "Div";
            newdiv.setAttribute("id", divIdName);
            newdiv.innerHTML = '<input type="file" name="attachment" id="attachment"/><a href="#" onclick="javascript:removeFile(' + divIdName + ');">Remove </a>';
            ni.appendChild(newdiv);
      }

      function removeFile(divName) {
            var d = document.getElementById("fileDiv");
      d.removeChild(divName);
}
</script>
</head>
<body>
    <form id="form1" runat="server">
           <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true"></asp:ScriptManager>
    <!--==============================
              header
=================================-->
         <asp:UpdatePanel ID="upAppraisal" runat="server">
            <ContentTemplate>
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
         <asp:LinkButton ID="LinkButton1" runat="server" Visible="false" Font-Underline="True">Logout</asp:LinkButton>
      <div class="grid_12">
       <%-- <div class="ta__center">--%>
             <asp:Panel ID="pnlVCHome" runat="server" Visible="true"> 
                   <div class="clear sep__1"></div>
       <div class="container">
	
	<div class="cau_hide">
	<div class="cursual"><!--  start cursual  -->
		<h4>VC Recordings<span class="line"></span></h4>
        You must have HTML5 Compatible Latest Browser: <a href="vctube.aspx">Click Here</a>
	</div>
        <div id="divVC" runat="server" />
	</div><!----//End-img-cursual---->
	</div>
        <div style="display:block;">  <p style="float: left"> <h3>Video Conferencing (Session Done: <div class="count" id="divVCCount" style="display: inline-block; margin-top:3px;" runat="server">768</div>)</h3></p>
  <p style="float: right; color: #d7ceb2; text-shadow: 1px 1px 0px #2c2e38, 3px 3px 0px #5c5f72; font: 40px 'BazarMedium';	letter-spacing: 10px;">VC Control Room: 011-24388409</p></div>
                
                 <asp:ImageButton ID="ibtnNew" runat="server" ImageUrl="images/createnew.png" Height="32" Width="32" ImageAlign="Left" />
       <br />
          <h4>Book a New Meeting </h4> 
           
            <hr />
             <h4>Upcoming VC:</h4> <br />
             <asp:GridView Width="1200px" ID="gvUpcoming" runat="server" HeaderStyle-CssClass="gvHeader"
  CssClass="gvRow" 
  AlternatingRowStyle-CssClass="gvAltRow"
  AutoGenerateColumns="false" >
  <Columns>
    <asp:TemplateField>
      <HeaderTemplate>
      <%--  <th colspan="6">Category</th>--%>
       
        <tr class="gvHeader">
          <th></th>
          
          <th>Meeting Schedule</th>
             <th>location</th>
          <th>Display Name/Description</th>
            <th>Dept(Info1)<br />Persons(Info2)<br />Issues(Info3) </th>
           
             <th  width="32%">Detail </th>
                    <th>!</th>
        </tr>
      </HeaderTemplate> 
      <ItemTemplate>
         <td><%# Eval("Meeting Schedule")%></td>
           <td><%# Eval("Location")%></td>
        <td><%# Eval("Display Name")%></td>
        <td><%# Eval("dept")%></td>
         
          <td><%# Eval("detail")%></td>
                  <td><asp:Button ID="btnEdit" runat="server" Text="Edit"  CommandArgument='<%# Eval("id")%>' /></td>
       
      </ItemTemplate>
    </asp:TemplateField>
  </Columns>

                       <EmptyDataTemplate><div>No Meetings Available</div></EmptyDataTemplate>    </asp:GridView>
          <div class="clear"></div>
        </asp:Panel>
              <asp:Panel ID="pnlAdmin" runat="server" Visible="false"> 
                    <br />
                  <asp:Panel ID="pnlLogin" runat="server" Visible="false"> 
                      <div id="main" style="height:100px;" >
                 <div id="first" style="height:100px;">
                  <asp:Label ID="lblLogin" runat="server" Text="Login"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp
                  <asp:TextBox ID="txtLogin" runat="server"></asp:TextBox> <br />
                  <asp:Label ID="lblPwd" runat="server" Text="Password"></asp:Label>&nbsp;&nbsp;
                  <asp:TextBox ID="txtPwd" runat="server" TextMode="Password"></asp:TextBox>&nbsp;&nbsp;<asp:Button ID="btnLogin" runat="server" Text="Sign In" />
                <br /><br /><p>Contact vinodkotiya@ntpc.co.in for login id and password.</p>  </div></div>  </asp:Panel>
                    <asp:Panel ID="pnlCreate" runat="server" Visible="false"> <br />
                          <div id="main" style="height:360px;" >
                 <div id="first" style="height:360px;">
                        <table><tr><td>
                          <asp:Label ID="lblMeetingid" runat="server" Text=""></asp:Label> 
                    Meeting Start : </td><td> <asp:TextBox ID="txtSchStart" runat="server"  EnableViewState= "false" ></asp:TextBox><img src="images/calender.png" />
                        </td></tr><tr><td>
                        Meeting Location:</td><td> <asp:DropDownList ID="ddlLocation" runat="server">
                            <asp:ListItem Selected="True">PMC</asp:ListItem>
                              <asp:ListItem >EOC</asp:ListItem>
                             <asp:ListItem >EOC TP Room</asp:ListItem>
                            <asp:ListItem >PMI</asp:ListItem>
                            <asp:ListItem >IT</asp:ListItem>
                             <asp:ListItem >RHQ</asp:ListItem>
                            <asp:ListItem >Site</asp:ListItem>
                            <asp:ListItem >Committee Room1</asp:ListItem>
                            <asp:ListItem >Committee Room2</asp:ListItem>
                            <asp:ListItem >MCM</asp:ListItem>
                            <asp:ListItem >Ante Room</asp:ListItem>
                            <asp:ListItem >Commercial Conf Hall</asp:ListItem>
                            <asp:ListItem >Other</asp:ListItem>
                                          </asp:DropDownList><br />
                         </td></tr><tr><td>
                        Meeting Name: </td><td><asp:TextBox ID="txtDisplay" runat="server"></asp:TextBox>
                        Dept: <asp:DropDownList ID="ddlDept" runat="server">
                            <asp:ListItem Selected="True">PP&M</asp:ListItem>
                              <asp:ListItem >Engg</asp:ListItem>
                             <asp:ListItem >OS</asp:ListItem>
                             <asp:ListItem >PMI</asp:ListItem>
                            <asp:ListItem >IT</asp:ListItem>
                            <asp:ListItem >COMM</asp:ListItem>
                            <asp:ListItem >CP</asp:ListItem>
                            <asp:ListItem >HR</asp:ListItem>
                            <asp:ListItem >FIN</asp:ListItem>
                            <asp:ListItem >CC&M</asp:ListItem>
                            <asp:ListItem >BD</asp:ListItem>
                            <asp:ListItem >BE</asp:ListItem>
                             <asp:ListItem >Safety</asp:ListItem>
                            <asp:ListItem >Other</asp:ListItem>
                                          </asp:DropDownList></td></tr><tr><td>
Meeting Detail:</td><td>  <asp:TextBox ID="txtMeetDetail" placeholder="Enter details like Sites to be connected, IP, Contacts etc" runat="server" TextMode="Multiline" width ="300px" Height="200px"/>
             </td></tr><tr><td>Coordinator Contact         
                      </td><td><asp:TextBox ID="txtCoordinator" runat="server" MaxLength="10" TextMode="Number"></asp:TextBox>(Mobile number only)</td></tr><tr><td>           
                      </td><td><asp:Button ID="btnCreateMeeting" runat="server" Text="Create Meeting"  /></td>      </tr></table>
                  </div></div>  </asp:Panel>
                     </asp:Panel>
            <div id="divMsg" runat="server" />
     <%-- </div>--%>
    </div>
  </div>
     
</div>
    
  <div class="clear sep__1"></div>
     <a href="upload/codecconfig.pdf" >Codec Configuration</a> &nbsp;&nbsp;&nbsp;| &nbsp;&nbsp;&nbsp;
  <a href="upload/RPDesktop.exe" >Real Presence Desktop Setup(20MB)</a> &nbsp;&nbsp;&nbsp;| &nbsp;&nbsp;&nbsp;
      <a href="upload/RPD help manual.docx" >RPD Help Manual</a> &nbsp;&nbsp;&nbsp;| &nbsp;&nbsp;&nbsp;
      <a href="upload/polycomcameracontrol.msi" >Telepresence Camera control</a> &nbsp;&nbsp;&nbsp;| &nbsp;&nbsp;&nbsp;
     <a href="upload/progility.pdf" >Polycom Escalation Matrix(Site)</a> &nbsp;&nbsp;&nbsp;| &nbsp;&nbsp;&nbsp;
      <a href="upload/bcm-client.jnlp" >Videowall control</a>&nbsp;&nbsp;&nbsp;
      <div class="clear sep__1"></div>
      <asp:Button ID="btnXls" runat="server" Text="Download As Excel" />
       <asp:GridView ID="gvVCContact" runat="server"  CssClass="EU_DataTable" Caption="Site VC Contacts:" CaptionAlign="Left">
                       <EmptyDataTemplate><div>No Data Available</div></EmptyDataTemplate></asp:GridView>
      <br /> <hr /> VC Recording Upload Section
      <asp:TextBox ID="txtUploadKey" runat="server" placeholder="Enter Password" TextMode="Password"></asp:TextBox> <br />
      <asp:TextBox ID="txtSubject" runat="server" placeholder="Meeting Detail with date" Width="300px"></asp:TextBox>

      <div id="divAttachments" runat="server" /> <br />
                   
                   <div id="fileatt" runat="server"  >
            <input type="file" name="attachment" runat="server" id="attachment" onchange="document.getElementById('moreUploadsLink').style.display = 'none';"  />
            </div>
      <input type="hidden" value="0" id="fileCount" />
      <div id="fileDiv">
      </div>
      <div id="moreUploadsLink" style="display: none">
            <a href="javascript:addFile();"></a>
      </div>
      <asp:Button ID="btnUpload" runat="server" Text="Upload" OnClick="btnUpload_Click"   /> &nbsp; &nbsp; 1Min for 1 GB
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
                </ContentTemplate>
              <Triggers>
                    <asp:PostBackTrigger ControlID="btnUpload" />
                 <asp:PostBackTrigger ControlID="btnLogin" />
                   <asp:PostBackTrigger ControlID="ibtnNew" />
                   <asp:PostBackTrigger ControlID="btnXls" />
                  <asp:PostBackTrigger ControlID="btnCreateMeeting" />
                  
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
<!--End of Tawk.to Script-->
</html>
