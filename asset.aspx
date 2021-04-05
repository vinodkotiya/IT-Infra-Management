<%@ Page Language="VB" AutoEventWireup="false" CodeFile="asset.aspx.vb" Inherits="_asset" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Information Technology - Corporate Center</title>
    <meta charset="utf-8">
    <meta http-equiv="refresh" content="600">
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
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
 <Triggers>
    <%-- <asp:PostBackTrigger ControlID="LinkButton1" />
      <asp:PostBackTrigger ControlID="btnGo" />
      <asp:AsyncPostBackTrigger ControlID="RadioButtonList1" EventName="SelectedIndexChanged" />
      <asp:AsyncPostBackTrigger ControlID="chkShowTest" EventName="CheckedChanged" />
      <asp:AsyncPostBackTrigger ControlID="chkShowSite" EventName="CheckedChanged" />--%>
    
    </Triggers>
              <ContentTemplate>
    <div class="row">
            <asp:LinkButton ID="LinkButton1" runat="server" Visible="false" Font-Underline="True">Logout</asp:LinkButton>
        <asp:Panel ID="pnlHome" runat="server" Visible="false"> 
      <div class="grid_12">
      
        <h3  style="vertical-align: middle;">
            Asset Management System <a href="http://10.0.236.168/ccit/upload/ocms.pdf" target="_blank"> <img src="images/help-icon.png" height="60px" style="vertical-align: middle;" alt="?" /> </a> </h3>
      </div>
      <div class="grid_3">
        <div class="ih-item circle effect2 left_to_right"><a href="asset.aspx?ctype=entry">
        <div class="img"><img src="images/itasset.jpg" alt="img"></div>
        <div class="info">
          <h3>Enter Asset</h3>
          <p></p>
        </div></a></div>
      </div>
      <div class="grid_3">
        <div class="ih-item circle effect2 left_to_right"><a href="asset.aspx?ctype=po">
        <div class="img"><img src="images/po.png" alt="img"></div>
        <div class="info">
          <h3>Maintain PO</h3>
          <p></p>
        </div></a></div>
      </div>
      <div class="grid_3">
        <div class="ih-item circle effect2 left_to_right"><a href="asset.aspx?ctype=assign">
        <div class="img"><img src="images/assetassign.png" alt="img"></div>
        <div class="info">
          <h3>Asset Assignment</h3>
          <p></p>
        </div></a></div>
      </div>
      
             <div class="grid_3">
        <div class="ih-item circle effect2 left_to_right"><a href="asset.aspx?ctype=report">
        <div class="img"><img src="images/report.gif" alt="img"></div>
        <div class="info">
          <h3>Asset Report</h3>
          <p>CC-IT</p>
        </div></a></div>
      </div>
             <div class="clear sep__1"></div>
             <div id="divWallofFame" runat="server" />
            </asp:Panel>
        <asp:Panel ID="pnlAssetBooking" runat="server" Visible="false"> 
            <div id="main">
                <div class="bottom_to_top"><a href="asset.aspx"> <<-Back...</a></div>
<div id="first" style="height:500px; width:800px;">


    <label class="myformlabel">Owner (emp ID)* :</label>   <asp:TextBox ID="txtuid" runat="server"  class="wrapper-dropdown-5"  Text="000000" />
     <label class="myformlabel">PO* :</label> <asp:DropDownList ID="ddlPO" DataTextField="t" DataValueField ="v" runat="server"></asp:DropDownList>  <%--<asp:TextBox ID="txtPO" runat="server"  class="wrapper-dropdown-5"  />--%><br /><br />
   <label class="myformlabel">Asset Type* :</label>  <asp:DropDownList ID="ddlCtype" runat="server" AutoPostBack="true"  DataValueField="id" DataTextField="type"  class="wrapper-dropdown-5"   EnableViewState="true">
      </asp:DropDownList> 
    <label class="myformlabel">Make* :</label>
    <asp:DropDownList ID="ddlMake" runat="server" AutoPostBack="true"  DataValueField="make" DataTextField="make"  class="wrapper-dropdown-5"   EnableViewState="true">
      </asp:DropDownList>
    &nbsp;<asp:TextBox ID="txtMake" runat="server"  class="wrapper-dropdown-5"  Text="" Width="100px" /><br /><br />
    <label class="myformlabel">Model :</label>   <asp:TextBox ID="txtModel" runat="server"  class="wrapper-dropdown-5"  />
     <label class="myformlabel">SN* :</label>   <asp:TextBox ID="txtSN" runat="server"  class="wrapper-dropdown-5"  /><br /><br />
     <label class="myformlabel">Assign Date* :</label>   <asp:TextBox ID="txtDate" runat="server"  class="wrapper-dropdown-5"  />
    <asp:CalendarExtender ID="CalendarExtender1" runat="server" 
                        TargetControlID="txtDate"  Format="yyyy-MM-dd"   BehaviorID="calendar1" >
                    </asp:CalendarExtender><br /><br />
    <label class="myformlabel">Asset Detail (Optional) :</label>
    <asp:TextBox ID="txtDescr" runat="server"  class="myformtextarea" TextMode="MultiLine" placeholder="write down here additional serial number if required or employee or location  details..." Width="600px" Height="70px" />



    <asp:Button ID="btnSubmit" runat="server" Text="Submit" class="myforminput" />
    <br />
       <div  id="divMsgComplaint" runat="server"  style="font-family: Arial, Helvetica, sans-serif; font-size: small; font-style: normal; color: #ff0000; word-spacing: normal; text-indent: inherit; text-align: justify;"  />


</div>
</div>
            </asp:Panel>
          <asp:Panel ID="pnlStatus" runat="server" Visible="false"> <br />
              <label>Asset Status:</label>
                <asp:GridView ID="gvStatus" runat="server"  CssClass="EU_DataTable" >
                    <EmptyDataTemplate><div>No Data Available</div></EmptyDataTemplate></asp:GridView>
              </asp:Panel>
        <asp:Panel ID="pnlLogin" runat="server" Visible="false" BorderColor="#3333CC" BorderStyle="Groove" BorderWidth="2px"> 
             <div id="main" style="height:160px;" >
                 <div id="first" style="height:160px;"><br />
            <label>Note: Your IP Address and Location are being recorded.</label><br />
           <b> <label>Please Enter Employee Number to procede:</label></b>
                    <asp:TextBox ID="txtEid" runat="server" placeholder="6 digit emp no."></asp:TextBox> <br /><br />
                
            <asp:Button ID="btnLogin" runat="server" Text="Sign In" />
                     </div></div>
            </asp:Panel>
         <asp:Panel ID="pnlAdminLogin" runat="server" Visible="false" BorderColor="#3333CC" BorderStyle="Groove" BorderWidth="2px"> <br />
            <label>Note: Your IP Address and Location are being recorded.</label><br /><br />
            <label>AdminID:</label>
                    <asp:TextBox ID="txtAdminID" runat="server"></asp:TextBox> <br /><br />
             <label>Password:</label>
                    <asp:TextBox ID="txtAdminPwd" runat="server" TextMode="Password"></asp:TextBox> <br /><br />
                
            <asp:Button ID="btnAdminLogin" runat="server" Text="Sign In" />
            </asp:Panel>
         <asp:Panel ID="pnlPO" runat="server" Visible="false" BorderColor="#3333CC" BorderStyle="Groove" BorderWidth="2px"> <br />
          <div id="main">
                <div class="bottom_to_top"><a href="asset.aspx"> <<-Back...</a></div>
<div id="first" style="height:220px; width:700px;">   <label>.</label><br />
            <label>PO *:</label>
                    <asp:TextBox ID="txtvPO" runat="server"></asp:TextBox> 
              <label>PO Date *:</label>
                    <asp:TextBox ID="txtvPODate" runat="server"></asp:TextBox> <asp:CalendarExtender ID="CalendarExtender2" runat="server" 
                        TargetControlID="txtvPODate"  Format="yyyy-MM-dd"   BehaviorID="calendar1" >
                    </asp:CalendarExtender> <br /><br />
             <label>Vendor Name *:</label>
                    <asp:TextBox ID="txtvName" runat="server"></asp:TextBox> 
             <label>Contact:</label>
                    <asp:TextBox ID="txtvContact" runat="server"></asp:TextBox> <br /><br />
            
             <label>Install Date:</label>
                    <asp:TextBox ID="txtvInstallDate" runat="server"></asp:TextBox> 
             <asp:CalendarExtender ID="CalendarExtender3" runat="server" 
                        TargetControlID="txtvInstallDate"  Format="yyyy-MM-dd"   BehaviorID="calendar2" >
                    </asp:CalendarExtender>
                 <label>Validity:</label>
                    <asp:TextBox ID="txtvValidity" runat="server"></asp:TextBox> <br /><br />
            <asp:Button ID="btnCreatePO" runat="server" Text="Create New PO" />
    </div>
</div>
            </asp:Panel>
          <asp:Panel ID="pnlAdminEdit" runat="server" Visible="false" BorderColor="#3333CC" BorderStyle="Groove" BorderWidth="2px"> <br />
              <label>Asset Assignment:</label><asp:Label runat="server" id="lblCurrentID"></asp:Label> <br />
           <label>Current Owner:</label> <asp:Label runat="server" id="lblCurrentUser"></asp:Label><br />
           
            <label>New Owner:</label>
            <asp:TextBox ID="txtEditNewUID" runat="server" placeholder="6 digit Emp no" ></asp:TextBox> <br /><br />
              <label>Issue Date:</label>
            <asp:TextBox ID="txtEditDt" runat="server" placeholder="6 digit Emp no" ></asp:TextBox> <br /><br />
               <asp:CalendarExtender ID="CalendarExtender4" runat="server" 
                        TargetControlID="txtEditDt"  Format="yyyy-MM-dd"   BehaviorID="calendar2" />
                <br />
           
            <asp:Button ID="btnChange" runat="server" Text="Assign" />
              </asp:Panel>
          <asp:Panel ID="pnlAssign" runat="server" Visible="false"> <br />
              <div class="bottom_to_top"><a href="asset.aspx"> <<-Back...</a></div>
              <label>Filter:</label>  <asp:DropDownList ID="ddlAdminFilter" runat="server" AutoPostBack="true"  DataValueField="id" DataTextField="type"  class="wrapper-dropdown-5"   EnableViewState="true">
      </asp:DropDownList> &nbsp;&nbsp;&nbsp;&nbsp;
             
               &nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtSearchID" runat="server" Width="170px" placeholder ="Search Emp No, Asset ID, PO"></asp:TextBox>
               &nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnRefresh" runat="server" Text="Refresh" />
               
               <asp:GridView ID="gvAdminStatus" runat="server"  HeaderStyle-CssClass="gvHeader"
  CssClass="gvRow"
  AlternatingRowStyle-CssClass="gvAltRow"
  AutoGenerateColumns="false" AllowPaging="False" PageSize="11" AllowCustomPaging="False" >
  <Columns>
    <asp:TemplateField>
      <HeaderTemplate>
      <%--  <th colspan="6">Category</th>--%>
       
        <tr class="gvHeader">
          <th></th>
          
          <th>Asset ID</th>
             <th>Type</th>
          <th>Make</th>
            <th>Model </th>
             <th>SN </th>
            <th>Detail </th>
             <th>Owner </th>
          <th>Issue Date</th>
            <th>PO</th>
            <th>Log</th>
                     <th>!</th>
        </tr>
      </HeaderTemplate> 
      <ItemTemplate>
         <td><%# Eval("uid")%></td>
           <td><%# Eval("atype")%></td>
        <td><%# Eval("make")%></td>
         <td><%# Eval("model")%></td>
          <td><%# Eval("sn")%></td>
          <td><%# Eval("detail")%></td>
        <td><%# Eval("owner")%></td>
           <td><%# Eval("st_dt")%></td>
           <td><%# Eval("po")%></td>
           <td><%# Eval("chain")%></td>
            <td><asp:Button ID="btnEdit" runat="server" Text="Edit"  CommandArgument='<%# Eval("uid")%>' /></td>
       
      </ItemTemplate>
    </asp:TemplateField>
  </Columns>

                          </asp:GridView>
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
        <div class="socials">
          <a href="#" class="fa fa-twitter"></a>
          <a href="#" class="fa fa-facebook"></a>
          <a href="#" class="fa fa-google-plus"></a>
          <a href="#" class="fa fa-youtube-play"></a>
        </div>
         <div class="copyright"> <div  id="divFoot" runat="server" style="display: inline-block;"  /> <div>Website designed by <a href="#" rel="nofollow">CC-IT</a></div>
        </div>
      </div>
    </div>
  </div>
</footer>
    </form>
</body>
</html>
