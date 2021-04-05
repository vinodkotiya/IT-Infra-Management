<%@ Page Language="VB" AutoEventWireup="false" CodeFile="canteen.aspx.vb" Inherits="_canteen" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Canteen Feedback/Suggestion</title>
    <meta charset="utf-8">
    <meta http-equiv="refresh" content="600">
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
<script>
    $(function () {

        $("#rateYo1").rateYo({
            starWidth: "40px",
            fullStar: true
        });
        $("#rateYo2").rateYo({
            starWidth: "40px",
            fullStar: true

        });
        $("#rateYo3").rateYo({
            starWidth: "40px",
            fullStar: true
        });
        $("#rateYo4").rateYo({
            starWidth: "40px",
            fullStar: true
        });

    });

    $(function () {

        $("#rateYo1").rateYo()
                    .on("rateyo.change", function (e, data) {

                        var rating1 = data.rating;
                        document.getElementById('txtRate1').value = rating1;
                     
                    });
        $("#rateYo2").rateYo()
                    .on("rateyo.change", function (e, data) {

                        var rating2 = data.rating;
                        document.getElementById('txtRate2').value = rating2;

                    });
        $("#rateYo3").rateYo()
                    .on("rateyo.change", function (e, data) {

                        var rating3 = data.rating;
                        document.getElementById('txtRate3').value = rating3;

                    });
        $("#rateYo4").rateYo()
                   .on("rateyo.change", function (e, data) {

                       var rating3 = data.rating;
                       document.getElementById('txtRate4').value = rating3;

                   });
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
           
        <asp:Panel ID="pnlHome" runat="server" Visible="false"> 
     
     
     <div align="Center">
         <img src="images/feedback.png"/>
        <div class="ih-item circle effect2 left_to_right"><a href="canteen.aspx?ctype=1">
        <div class="img"><img src="images/feedback1.jpg" alt="img"></div>
        <div class="info">
          <h3>Feedback</h3>
         
        </div></a></div>
     
      </div>
    
    
         
          


            </asp:Panel>
        <asp:Panel ID="pnlComplaintBooking" runat="server" Visible="false"> 
            <div id="main" align="Center">
               
<div id="first">


<label class="myformlabel">Name :</label>   <asp:Label ID="lblName" runat="server"  CssClass="myformlabel" /> 
       <br /><label class="myformlabel"> Mobile :</label>   <asp:Label ID="lblMobile" runat="server"  CssClass="myformlabel" /><br /> <label class="myformlabel"> Email :</label> <asp:Label ID="lblEmail" runat="server"  CssClass="myformlabel" /><br />
    <asp:Label ID="Label1" runat="server"  CssClass="myformlabel" /><br />
  <label class="myformlabel"> Food Quality :</label>  <div id="rateYo1"></div>
   <label class="myformlabel"> Cleanliness and Hygiene :</label>  <div id="rateYo2"></div>
   <label class="myformlabel"> Staff Behaviour :</label>   <div id="rateYo3"></div>
     <label class="myformlabel"> Service :</label>   <div id="rateYo4"></div>
     
               <asp:TextBox ID="txtRate1" runat="server"  Text="0" Width="0px"  tabindex="-1000" style="border:0px;background-color:transparent;" />
     <asp:TextBox ID="txtRate2" runat="server"  Text="0" Width="0px"  tabindex="-1000" style="border:0px;background-color:transparent;" />
     <asp:TextBox ID="txtRate3" runat="server"    Text="0" Width="0px"  tabindex="-1000" style="border:0px;background-color:transparent;" />
     <asp:TextBox ID="txtRate4" runat="server"    Text="0" Width="0px"  tabindex="-1000" style="border:0px;background-color:transparent;" />
               <asp:TextBox ID="txtReplyID" runat="server"  Text="" Width="5px"  tabindex="-1000" style="border:0px;background-color:transparent;" />
    <br />
    <label class="myformlabel">Feedback :</label>
    <asp:TextBox ID="txtDescr" runat="server"  class="myformtextarea" TextMode="MultiLine" placeholder="write your feedback/ suggestion here..." />


    

    <asp:Button ID="btnSubmit" runat="server" Text="Submit" class="myforminput" />
    <br /> 
       <div  id="divMsgComplaint" runat="server"  style="font-family: Arial, Helvetica, sans-serif; font-size: small; font-style: normal; color: #ff0000; word-spacing: normal; text-indent: inherit; text-align: justify;"  />


</div>
</div>
            </asp:Panel>
          <asp:Panel ID="pnlStatus" runat="server" Visible="false"> <br />
              <label>Complaint Status:</label>
                <asp:GridView ID="gvStatus" runat="server"  CssClass="EU_DataTable" >
                    <EmptyDataTemplate><div>No Data Available</div></EmptyDataTemplate></asp:GridView>
              </asp:Panel>
        <asp:Panel ID="pnlLogin" runat="server" Visible="false" BorderColor="#3333CC" BorderStyle="Groove" BorderWidth="2px"> 
             <div id="main" style="height:160px;" >
                 <div id="first" style="height:160px;"><br />
           <br />
           <b> <label>Please Enter Employee Number to proceed:</label></b>
                    <asp:TextBox ID="txtEid" runat="server" placeholder="6 digit emp no."></asp:TextBox> <br /><br />
                
            <asp:Button ID="btnLogin" runat="server" Text="Sign In" />
                     </div></div>
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
