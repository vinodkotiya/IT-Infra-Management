<%@ Page Language="VB" AutoEventWireup="false" CodeFile="nw.aspx.vb" Inherits="_nw" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Information Technology - Corporate Center</title>
    <meta charset="utf-8">
     <meta http-equiv="refresh" content="155">
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
    <%-- old browser check--%>
    <script src="js/modernizr.min.js"></script>
    <script>
  if (!Modernizr.canvas) {
      alert("Your browser is very outdated and dosen't support HTML5. Use IE 10/Chrome/Mozilla to view this page. Redirecting you to Home Page...");
      window.location = "http://10.0.236.168/ccit";
  }
 
</script>
     <%-- old browser check--%>
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
    <script type="text/javascript">
       
   function uploadCanvas() {
      
        var image = document.getElementById("canvas").toDataURL("image/png");
        image = image.replace('data:image/png;base64,', '');
      $.ajax({
                type: "POST",
                url: "vinservice.asmx/UploadImage",
                data: '{ "imageData" : "' + image + '" }',
                 contentType: 'application/json; charset=utf-8',
                 dataType: 'json',
                 success: function (data, status) {
                    // alert(data.d + 'Image saved successfully !');
                 },
                 error: function (request, status, error) {
                     //alert('Image failed to save !' + status + error);
      }

            });
    }
    </script>
    <script type="text/javascript">
        function sleep(milliseconds) {
            var start = new Date().getTime();
            for (var i = 0; i < 1e7; i++) {
                if ((new Date().getTime() - start) > milliseconds) {
                    break;
                }
            }
        }
    $(document).ready(function () {
        var canvas = $('#canvas');
        var ctx = canvas[0].getContext("2d");
        var rectWidth = 200;
       $.ajax({
           type: "POST",
           url: "vinservice.asmx/GetNetwork",
           data: "{filename:'" + name + "'}",
           contentType: "application/json; charset=utf-8",
           dataType: "json",
           success: OnSuccess,
           error: OnError
       });
       function OnSuccess(data, status) {
           // alert(data.d);  //no need to display on success
           //  $x("#testcontainer").html(data.d);
           //  $("#divNotify").html('success ' + data.d);
           //var json = $.parseJSON(data);
           //  var json = Object.keys(data.d.GetDataResult);
          // console.log(data.d);
           var vin = $.parseJSON(data.d);
           // console.log(vin.GetDataResult);
          
           $.each(vin.GetDataResult,  function (i, item) {
               // console.log(item.name);
               //   console.log(i);
               //draw a line
               //setInterval(function () { m(); }, 300);
             
                   ctx.beginPath();
                   if (item.nextx != null) {
                       ctx.moveTo(item.x + rectWidth / 2, item.y + 30);
                       ctx.lineTo(item.nextx, item.nexty);
                   }
                   ctx.strokeStyle = 'rgba(255,100,100,0.5)';
                   ctx.lineWidth = 3;
                   ctx.stroke();
                   ctx.closePath();
                   //Draw a rectangle

                   if (item.status == 1) ctx.fillStyle = 'Green';
                   if (item.status == 0) ctx.fillStyle = 'Red';
                   if (item.mainstatus == -1) ctx.fillStyle = 'blue';
                   if (item.mainstatus == 2) ctx.fillStyle = 'rgb(242,183,73)';
                   if (item.name == 'Sensor') {
                       ctx.arc(item.x, item.y, 25, 0, 2 * Math.PI, false);
                       // ctx.fillStyle = 'green';
                       ctx.fill();
                       ctx.lineWidth = 5;
                   }
                   else { ctx.fillRect(item.x, item.y, rectWidth, 30 * item.big); } //x,y,w,h}
                   ctx.shadowColor = '#999';
                   ctx.shadowBlur = 20;
                   ctx.shadowOffsetX = 15;
                   ctx.shadowOffsetY = 15;

                   // console.log(item.x + rectWidth / 2 + ' ' + item.y + 30);
                   // console.log(item.nextx + ' ' + item.nexty);
                   //draw text
                   ctx.font = '12px "Arial"';
                   if (item.big == 2) ctx.font = '18px "Arial"';
                   ctx.fillStyle = 'White';
                   ctx.textAlign = 'left';
                   //ctx.fillText(item.name, item.x + 2, item.y + 12)
                   if (item.avbl == 1) item.avbl = 0;
                   if (item.name == 'Sensor') {
                      ctx.textAlign = 'center';
                      ctx.fillText(item.avbl / 100 + ' °C', item.x, item.y + 3);
                   }
                   else { wrapText(ctx, item.name + ' / ' + item.ip + ' (' + item.avbl + '%)', item.x + 2, item.y + 12, rectWidth, 13 * item.big); }
                   
           });
           function drawTemperature()
           {
               var ctx1 = canvas[0].getContext("2d");
               ctx1.arc(310, 300, 22, 0, 2 * Math.PI, false);
               ctx1.fillStyle = 'green';
               ctx1.fill();
               ctx1.lineWidth = 5;
               ctx1.font = '8pt Calibri';
               ctx1.fillStyle = 'white';
               ctx1.textAlign = 'center';
               ctx1.fillText('26', 310, 300 + 3);
           }
           uploadCanvas();
       }
       function OnError(request, status, error) {
           // alert('error ' + request.statusText);
          // $("#divNotify").html('error ' + request.statusText);
       }
       function wrapText(context, text, x, y, maxWidth, lineHeight) {
           var words = text.split(' ');
           var line = '';

           for (var n = 0; n < words.length; n++) {
               var testLine = line + words[n] + ' ';
               var metrics = context.measureText(testLine);
               var testWidth = metrics.width;
               if (testWidth > maxWidth && n > 0) {
                   context.fillText(line, x, y);
                   line = words[n] + ' ';
                   y += lineHeight;
               }
               else {
                   line = testLine;
               }
           }
           context.fillText(line, x, y);
           //sleep(50);
       }
       
       $('#canvas').click(function (e) {
           var clickedX = e.pageX - this.offsetLeft;
           var clickedY = e.pageY - this.offsetTop;
          // alert('clicked  ' + clickedX + ',' + clickedY);
           //SELECT name, ip  FROM network where (clickedX between x and x + 200) and (clickedY between y and y + 30 *big)
           $.ajax({
               type: "POST",
               url: "vinservice.asmx/getIPxy",
               data: "{clickedX:'" + clickedX + "', clickedY:'" + clickedY + "'}",
               contentType: "application/json; charset=utf-8",
               dataType: "json",
               success: xySuccess
           });
           function xySuccess(data, status) {
               if (data.d.indexOf("Error") < 0) {
                    var ipDetail = data.d.split(',');
                    //alert(ipDetail[0]);
                   $.ajax({
                       type: "POST",
                       url: "vinservice.asmx/pingIP",
                       data: "{ipaddress:'" + ipDetail[0] + "'}",
                       contentType: "application/json; charset=utf-8",
                       dataType: "json",
                       success: pingSuccess,
                       error: pingError
                   });
                   function pingSuccess(mydata, status) {
                       console.log(ipDetail[0] + ' >> ' + mydata.d  + ipDetail[1] , ipDetail[2]);
                       if (mydata.d.indexOf("Success") > -1) ctx.fillStyle = 'Green';
                       if (mydata.d.indexOf("TimedOut") > -1) ctx.fillStyle = 'Red';
                       if (mydata.d.indexOf("DestinationHostUnreachable") > -1) ctx.fillStyle = 'Yellow';
                       if (mydata.d.indexOf("TtlExpired") > -1) ctx.fillStyle = 'Blue';
                       ctx.fillRect(ipDetail[1], ipDetail[2], rectWidth, 30 * ipDetail[3]);
                       ctx.fillStyle = 'White';
                       ctx.font = '12px "Arial"';
                       if (parseInt(ipDetail[3], 10) == 2) ctx.font = '18px "Arial"';
                       // ctx.fillText(ipDetail[0] + ' >> ' + mydata.d, ipDetail[1] + 2, ipDetail[2] + 12)
                       wrapText(ctx, ipDetail[4] + ' / ' + ipDetail[0] + ' ' + mydata.d, parseInt(ipDetail[1], 10) + 2, parseInt(ipDetail[2], 10) + 12, rectWidth, 13 * parseInt(ipDetail[3], 10))
                   }
                   function pingError(request, status, error) {
                       console.log(request);
                       alert('error ' + request.responseText + error.statusText);
                       
                       // $("#divNotify").html('error ' + request.statusText);
                   }
               } //close if
           }
       });
    });
</script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
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
<section class="content" id="txtLog"><div class="ic"></div>
  <div class="container">
      <div id="divHead" runat="server" /><div class="clear"></div>
    <div class="row">
      <div class="grid_12">
        <div class="ta__center">
         <%-- <h2>Services</h2>--%>
         <%-- <div class="st1">If you have any questions about this <span class="color1"><a href="http://blog.templatemonster.com/free-website-templates/" rel="nofollow">goodie</a></span>, read the post about it. <br> Find a bunch of alike <span class="color1"><a href="http://www.templatemonster.com/properties/topic/design-photography/" rel="nofollow">designs</a></span> at TemplateMonster’s website.</div>
        --%>  <div class="banners">
            
            <canvas id="canvas" width="1024" height="590"></canvas>
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
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
 Create Log: <asp:TextBox ID="txtIPLog" runat="server" Width="80%" Height="70px" Visible="True" TextMode="MultiLine" ReadOnly="True"></asp:TextBox>
            <asp:Timer ID="Timer1" runat="server" Enabled="True" Interval="2000"></asp:Timer>
  </ContentTemplate> </asp:UpdatePanel>
     <div class="clear sep__1"></div>
      <asp:UpdatePanel ID="UpdatePanel2" runat="server">
 <Triggers>
     <asp:PostBackTrigger ControlID="LinkButton1" />
      <asp:PostBackTrigger ControlID="btnGo" />
      <asp:AsyncPostBackTrigger ControlID="RadioButtonList1" EventName="SelectedIndexChanged" />
      
    
    </Triggers>
              <ContentTemplate>
                
         <table border="0" style="margin-left:50px;"><tr><td>
         <fieldset style="border-style: none dashed dashed dashed; border-width: thin; border-color: #000080; background-color: #FFFFFF; color:black; font-size:15px; " class="gvhighlight"><legend><b>Network Availability Reports:</b></legend>
  
                 <asp:RadioButtonList ID="RadioButtonList1" runat="server" 
                             RepeatDirection="Horizontal" Width="550px" AutoPostBack="True" RepeatColumns="4">
                             <asp:ListItem >Monthly</asp:ListItem>
                             <asp:ListItem >Weekly</asp:ListItem>
                            <%-- <asp:ListItem>Projects</asp:ListItem>
                             <asp:ListItem>Vendors</asp:ListItem>--%>
                             <asp:ListItem Selected="True">Today</asp:ListItem> 
                              
                         </asp:RadioButtonList>
             Range: Start Date:
                <asp:TextBox ID="dt_stTextBox" runat="server" Text="" CssClass="txt100" />
                <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="dt_stTextBox" Format="dd.MM.yyyy">
                                    </asp:CalendarExtender>
                End Date:
                <asp:TextBox ID="dt_endTextBox" runat="server" Text="" CssClass="txt100" />
                <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="dt_endTextBox"   Format="dd.MM.yyyy" >
                                    </asp:CalendarExtender>
                  <asp:Button ID="btnGo" runat="server" Text="Go" /> <br />
           </fieldset>
                <br />
               
                  <asp:LinkButton ID="LinkButton1" runat="server"> <img src="images/xls.gif" width=16 height=16 border=0 align=left /> Click for Excel</asp:LinkButton>
               
                   <div  id="divMsg" runat="server"  style="font-family: Arial, Helvetica, sans-serif; font-size: small; font-style: normal; color: #000080; word-spacing: normal; text-indent: inherit; text-align: justify;">
        </div>
         
                 </td> <td>
                      <div  id="divSummary" runat="server"   />
                     
</td></tr>
     </table>
   
        <%--</div>--%>
                   <asp:GridView ID="GridView1" runat="server"  CssClass="EU_DataTable" style="margin-left:50px;" >
                       <EmptyDataTemplate><div>No Data Available</div></EmptyDataTemplate></asp:GridView>
                  </ContentTemplate>
              </asp:UpdatePanel>
    
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
