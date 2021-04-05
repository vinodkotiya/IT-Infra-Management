<%@ Page Language="VB" AutoEventWireup="false" CodeFile="thankyou.aspx.vb" Inherits="_thankyou" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <titlCanteen Feedback/Suggestion</title>
    <meta charset="utf-8">
    

<meta http-equiv="refresh" content="5;url=https://cc.ntpc.co.in/ccit/canteen.aspx" />


<meta name="format-detection" content="telephone=no">
<link rel="icon" href="images/favicon.ico">
<link rel="shortcut icon" href="images/favicon.ico">
<link rel="stylesheet" href="css/stuck.css">
<link rel="stylesheet" href="css/style.css">
<link rel="stylesheet" href="css/ihover.css">
    <link rel="stylesheet" href="css/jquery.rateyo.min.css">
<script src="js/jquery.js"></script>
<script src="js/jquery.rateyo.min.js"></script>
    
<script src="js/script.js"></script>
<script src="js/superfish.js"></script>
<script src="js/jquery.equalheights.js"></script>
<script src="js/jquery.mobilemenu.js"></script>
<script src="js/jquery.easing.1.3.js"></script>
<script src="js/tmStickUp.js"></script>
<script src="js/jquery.ui.totop.js"></script>

   
    <style type="text/css">
        .auto-style1 {
            color: #0066FF;
        }
    </style>

   
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

 
    
     <div align="Center">
        <img src="images/ntpc-logo.png"/>
        <h3>
       
            <strong>Canteen Feedback/ Suggestion
          
        </strong>
          
        </h3>
        </div>
      
 
</header>
<!--=====================
          Content
======================-->
<section class="content"><div class="ic"></div>
 
  <div class="container">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
 <Triggers>
    <%-- <asp:PostBackTrigger ControlID="LinkButton1" />
      <asp:PostBackTrigger ControlID="btnGo" />
      <asp:AsyncPostBackTrigger ControlID="RadioButtonList1" EventName="SelectedIndexChanged" />
      <asp:AsyncPostBackTrigger ControlID="chkShowTest" EventName="CheckedChanged" />
      <asp:AsyncPostBackTrigger ControlID="chkShowSite" EventName="CheckedChanged" />--%>
    
    </Triggers>
              <ContentTemplate>
    <div align="Center">
           
        <asp:Panel ID="pnlHome" runat="server" > 
     
     
     <div align="Center">
         <h3><strong>Your Feedback Submitted Successfully</strong></h3>
&nbsp;</div>
     <div align="Center">
         <img src="images/Thank_You.png"/>
             
      </div>
     <div align="Center">

          <h3><strong><span class="auto-style1">Your Feedback helps us Improve</span> </strong></h3>
         </div>
    
         
          


            </asp:Panel>
        
          
        
         
      
        <div  id="divMsg" runat="server"  style="font-family: Arial, Helvetica, sans-serif; font-size: small; font-style: normal; color: #ff0000; word-spacing: normal; text-indent: inherit; text-align: justify;"  />

    </div>
                   </ContentTemplate>
              </asp:UpdatePanel>
  </div>
</section>
<!--==============================
              footer
=================================-->
<footer id="footer">
  <div class="container">
    <div class="row">
      <div class="grid_12">
      
         <div class="copyright"> <div  id="divFoot" runat="server" style="display: inline-block;"  /> <div> <a href="#" rel="nofollow"></a></div>
        </div>
      </div>
    </div>
  </div>
</footer>
    </form>
</body>
</html>
