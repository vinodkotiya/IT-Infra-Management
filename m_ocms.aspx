<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ocms.aspx.vb" Inherits="_ocms" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>NTPC App Launcher</title>
    <meta charset="utf-8" />
		<meta name="viewport" content="width=device-width, initial-scale=1" />
		<!--[if lte IE 8]><script src="assets/js/ie/html5shiv.js"></script><![endif]-->
		<link rel="stylesheet" href="assets/css/main.css" />
		<!--[if lte IE 9]><link rel="stylesheet" href="assets/css/ie9.css" /><![endif]-->
		<!--[if lte IE 8]><link rel="stylesheet" href="assets/css/ie8.css" /><![endif]-->
</head>
<body>
    <form id="form1" runat="server">
           <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true"></asp:ScriptManager>
    <!--==============================
    <!-- Wrapper -->
		<div id="wrapper">

				<!-- Header -->
					<header id="header">
						<div class="inner">

							<!-- Logo -->
								<a href="index.html" class="logo">
								<span class="symbol"><img src="images/favicon.ico" alt="" /></span><span class="title">NTPC</span>
									</a>

							<!-- Nav -->
								<nav>
									<ul>
										<li><a href="#menu">Menu</a></li>
									</ul>
								</nav>

						</div>
					</header>

				<!-- Menu -->
					<nav id="menu">
						<h2>Menu</h2>
						<ul>
							<li><a href="index.html">Home</a></li>
							<li><a href="generic.html">Ipsum veroeros</a></li>
							<li><a href="generic.html">Tempus etiam</a></li>
							<li><a href="generic.html">Consequat dolor</a></li>
							<li><a href="elements.html">Elements</a></li>
						</ul>
					</nav>
             <div id="divMenu" runat="server" />
				<!-- Main -->
					<div id="main">
						<div class="inner">
							<h1>Online Complaint Management System</h1>
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
                  <section class="tiles">
								<article class="style6">
									<span class="image">
										<img src="images/circ_img1.jpg" alt="" />
									</span>
									<a href="m_ocms.aspx?ctype=1">
										<h2>PC-Laptop-Antivirus</h2>
										<div class="content">
											<p>Related Issues.</p>
										</div>
									</a>
								</article>
                      <article class="style1">
									<span class="image">
										<img src="images/circ_img2.jpg" alt="" />
									</span>
									<a href="m_ocms.aspx?ctype=2">
										<h2>Printers</h2>
										<div class="content">
											<p></p>
										</div>
									</a>
								</article>
                      </section>
    <div class="row">
            <asp:LinkButton ID="LinkButton1" runat="server" Visible="false" Font-Underline="True">Logout</asp:LinkButton>
        <asp:Panel ID="pnlHome" runat="server" Visible="false"> 
      <div class="grid_12" >
      
        <h3  style="vertical-align: middle; "><div>Online Complaint Management System << <div class="count" id="divOCMSCount" style="display: inline-block; padding-top:2px;" runat="server">768</div><a href="http://10.0.236.168/ccit/upload/ocms.pdf" target="_blank"> <img src="images/help-icon.png" height="60px" style="vertical-align: middle;" alt="?" /> </a> </div></h3> 
             
      </div>
      <div class="grid_3">
        <div class="ih-item circle effect2 left_to_right"><a href="ocms.aspx?ctype=1">
        <div class="img"><img src="images/circ_img1.jpg" alt="img"></div>
        <div class="info">
          <h3>PC-Laptop-Antivirus</h3>
          <p>Related Issues</p>
        </div></a></div>
      </div>
      <div class="grid_3">
        <div class="ih-item circle effect2 left_to_right"><a href="ocms.aspx?ctype=2">
        <div class="img"><img src="images/circ_img2.jpg" alt="img"></div>
        <div class="info">
          <h3>Printers</h3>
          <p></p>
        </div></a></div>
      </div>
      <div class="grid_3">
        <div class="ih-item circle effect2 left_to_right"><a href="ocms.aspx?ctype=3">
        <div class="img"><img src="images/circ_img3.jpg" alt="img"></div>
        <div class="info">
          <h3>Rax</h3>
          <p>Telephone Line</p>
        </div></a></div>
      </div>
      <div class="grid_3">
        <div class="ih-item circle effect2 left_to_right"><a href="ocms.aspx?ctype=4">
        <div class="img"><img src="images/circ_img4.jpg" alt="img"></div>
        <div class="info">
          <h3>LAN/WiFi</h3>
          <p>Internet</p>
        </div></a></div>
      </div>
           <div class="grid_3">
        <div class="ih-item circle effect2 left_to_right"><a href="ocms.aspx?ctype=6">
        <div class="img"><img src="images/email.png" alt="img"></div>
        <div class="info">
          <h3>Email</h3>
          <p>Zimbra</p>
        </div></a></div>
      </div> <div class="grid_3">
        <div class="ih-item circle effect2 left_to_right"><a href="ocms.aspx?ctype=5">
        <div class="img"><img src="images/ess.png" alt="img"></div>
        <div class="info">
          <h3>SAP/ESS</h3>
          <p>Password</p>
        </div></a></div>
      </div>
             <div class="grid_3">
        <div class="ih-item circle effect2 left_to_right"><a href="ocms.aspx?ctype=status">
        <div class="img"><img src="images/status.png" alt="img"></div>
        <div class="info">
          <h3>Status</h3>
          <p>Check</p>
        </div></a></div>
      </div>
             <div class="grid_3">
        <div class="ih-item circle effect2 left_to_right"><a href="ocms.aspx?ctype=admin">
        <div class="img"><img src="images/admin.png" alt="img"></div>
        <div class="info">
          <h3>Admin</h3>
          <p>CC-IT</p>
        </div></a></div>
      </div>
             <div class="clear sep__1"></div>
             <div id="divWallofFame" runat="server" />


            </asp:Panel>
        <asp:Panel ID="pnlComplaintBooking" runat="server" Visible="false"> 
            <div id="main">
                <div class="bottom_to_top"><a href="ocms.aspx?ctype=status"> <<-Click Here to Check Status of Prev Complaints.</a></div>
<div id="first">


<label class="myformlabel">Name :</label>   <asp:Label ID="lblName" runat="server"  CssClass="myformlabel" /> 
       <label class="myformlabel"> , Mobile :</label>   <asp:Label ID="lblMobile" runat="server"  CssClass="myformlabel" /><br /><br />
     <label class="myformlabel">Dept :</label>   <asp:TextBox ID="txtDept" runat="server"  class="wrapper-dropdown-5"  /><br /><br />
   <label class="myformlabel">Complaint Type :</label>  <asp:DropDownList ID="ddlCtype" runat="server" AutoPostBack="true"  DataValueField="id" DataTextField="type"  class="wrapper-dropdown-5"   EnableViewState="true">
      </asp:DropDownList><br /><br />
    <label class="myformlabel">Location :</label>
    <asp:DropDownList ID="ddlLocation" runat="server" AutoPostBack="true"  DataValueField="lid" DataTextField="loc"  class="wrapper-dropdown-5"   EnableViewState="true">
      </asp:DropDownList><br /><br />
    <label class="myformlabel">Priority(System Generated) :</label>  <asp:DropDownList ID="ddlPriority" runat="server" AutoPostBack="true" class="wrapper-dropdown-5"    EnableViewState="true" Enabled="False">
          <asp:ListItem Value="1">Normal</asp:ListItem>
          <asp:ListItem Value="2">Medium</asp:ListItem>
                   <asp:ListItem Value="3">High</asp:ListItem>
      </asp:DropDownList><br /><br />
    <label class="myformlabel">Complaint Description :</label>
    <asp:TextBox ID="txtDescr" runat="server"  class="myformtextarea" TextMode="MultiLine" placeholder="write down the issue here..." />
<label>Rax:</label>
 <asp:TextBox ID="txtRax" runat="server"    placeholder="Rax" TextMode="SingleLine" />
     <asp:Label ID="lblEmail" runat="server"  CssClass="myformlabel" />

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
         <asp:Panel ID="pnlNewUser" runat="server" Visible="false" BorderColor="#3333CC" BorderStyle="Groove" BorderWidth="2px"> <br />
            <label>Note: You have to be registered as SCOPE User. Fill in the details..</label><br />
            <label>Employee ID:</label>
                    <asp:TextBox ID="txtNewEid" runat="server"></asp:TextBox> <br /><br />
             <label>Name:</label>
                    <asp:TextBox ID="txtNewName" runat="server"></asp:TextBox> <br /><br />
             <label>Mobile:</label>
                    <asp:TextBox ID="txtNewMobile" runat="server"></asp:TextBox> <br /><br />
             <label>Email:</label>
                    <asp:TextBox ID="txtNewEmail" runat="server"></asp:TextBox> <br /><br />
             <label>Department:</label>
                    <asp:TextBox ID="txtNewDept" runat="server"></asp:TextBox> <br /><br />
                
            <asp:Button ID="btnNewUser" runat="server" Text="Create New User" />
            </asp:Panel>
          <asp:Panel ID="pnlAdminEdit" runat="server" Visible="false" BorderColor="#3333CC" BorderStyle="Groove" BorderWidth="2px"> <br />
              <label>Change Complaint Status:</label> 
            <asp:Label runat="server" id="lblCompID"></asp:Label><br />
            <asp:Label runat="server" id="lblCompDetail"></asp:Label>
              <asp:Label runat="server" id="lblEmailChange"></asp:Label>&nbsp;&nbsp;&nbsp;<asp:Label runat="server" id="lblMobileChange"></asp:Label><br />
              <label class="myformlabel">Change Complaint Type(Optional):</label>   <asp:DropDownList ID="ddlEdittype" runat="server" AutoPostBack="true"  DataValueField="id" DataTextField="type"  class="wrapper-dropdown-5"   EnableViewState="true">
      </asp:DropDownList>
    <label class="myformlabel">Change Location(Optional):</label>
    <asp:DropDownList ID="ddlEditLocation" runat="server" AutoPostBack="true"  DataValueField="lid" DataTextField="loc"  class="wrapper-dropdown-5"   EnableViewState="true">
      </asp:DropDownList><br /><br />
                <label class="myformlabel">Status: (Use Forward for external AMC/Lexmark/HP etc)</label>  <asp:DropDownList ID="ddlStatus" runat="server" AutoPostBack="true" class="wrapper-dropdown-5"    EnableViewState="true">
          <asp:ListItem Value="1">Pending</asp:ListItem>
          <asp:ListItem Value="2">Forward</asp:ListItem>
                   <asp:ListItem Value="3">Closed</asp:ListItem>
      </asp:DropDownList><br /><br />
            <label>Closing Remarks:</label>
            <asp:TextBox ID="txtClosingRemark" runat="server" placeholder="Please ensure and confirm from user that complaint has been resolved before closing." TextMode="MultiLine" Height="40px" Width="300px"></asp:TextBox> <br /><br />
                <br />
            <label>Closed By:</label> <asp:DropDownList ID="ddlEditBy" runat="server" AutoPostBack="true"  DataValueField="tech" DataTextField="tech"  class="wrapper-dropdown-5"   EnableViewState="true" />
    <br />
            <asp:Button ID="btnChange" runat="server" Text="Update" />
              </asp:Panel>
          <asp:Panel ID="pnlAdminStatus" runat="server" Visible="false"> <br />
              <label>Filter:</label>  <asp:DropDownList ID="ddlAdminFilter" runat="server" AutoPostBack="true"  class="wrapper-dropdown-5"  EnableViewState="true" BackColor="White">
           <asp:ListItem Value="1,2,3,4,5,6,7,8,9,10" Selected="True" >Show All</asp:ListItem>
                  <asp:ListItem Value="1" >PC/Laptop/Software/Antivirus</asp:ListItem>
          <asp:ListItem Value="2">Printers</asp:ListItem>
                   <asp:ListItem Value="3">Rax</asp:ListItem>
            <asp:ListItem Value="4">LAN/WiFi/Internet</asp:ListItem>
       <asp:ListItem Value="5">SAP/ESS</asp:ListItem>
                  <asp:ListItem Value="6">Email Zimbra</asp:ListItem>
      </asp:DropDownList> &nbsp;&nbsp;&nbsp;&nbsp;
              <asp:DropDownList ID="ddlAdminCore" runat="server" AutoPostBack="true"  class="wrapper-dropdown-5"  EnableViewState="true" BackColor="White">
           <asp:ListItem Value="%" Selected="True" >Show All</asp:ListItem>
                  <asp:ListItem Value="Core3" >Core3</asp:ListItem>
          <asp:ListItem Value="Core5">Core5</asp:ListItem>
                   <asp:ListItem Value="Core6">Core6</asp:ListItem>
            <asp:ListItem Value="Core7">Core7</asp:ListItem>
                       </asp:DropDownList>
               &nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtSearchID" runat="server" Width="170px" placeholder ="Search Emp No, Comp ID"></asp:TextBox>
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
          
          <th>CNo</th>
             <th>Type</th>
          <th width="32%">Description</th>
            <th>Priority </th>
             <th>Date </th>
            <th>Name </th>
             <th>Contact </th>
          <th>Dept</th>
            <th>Location</th>
            <th>Closing Remark</th>
              <th>By</th>
          <th>Status</th>
          <th>!</th>
        </tr>
      </HeaderTemplate> 
      <ItemTemplate>
         <td><%# Eval("id")%></td>
           <td><%# Eval("type")%></td>
        <td><%# Eval("descr")%></td>
        <td><%#  "<img src=images/" + Eval("priority") + ".png width=60px />" %></td>
          <td><%# Eval("Date")%></td>
          <td><%# Eval("name")%></td>
          <td><%# Eval("contact")%></td>
        <td><%# Eval("dept")%></td>
           <td><%# Eval("location")%></td>
           <td><%# Eval("closingremark")%></td>
           <td><%# Eval("tech")%></td>
        <td><%# "<img src=images/" + Eval("status") + ".png width=60px />"%></td>
           <td><asp:Button ID="btnEdit" runat="server" Text="Edit"  CommandArgument='<%# Eval("id")%>' /></td>
       
      </ItemTemplate>
    </asp:TemplateField>
  </Columns>

                          </asp:GridView>
              </asp:Panel>
      
        <div  id="divMsg" runat="server"  style="font-family: Arial, Helvetica, sans-serif; font-size: small; font-style: normal; color: #ff0000; word-spacing: normal; text-indent: inherit; text-align: justify;"  />

    </div>
                   </ContentTemplate>
              </asp:UpdatePanel>
  </div></div>
					</div>

				<!-- Footer -->
					<footer id="footer">
						<div class="inner">
							<section>
								<h2>Get in touch</h2>
								<form method="post" action="#">
									<div class="field half first">
										<input type="text" name="name" id="name" placeholder="Name" />
									</div>
									<div class="field half">
										<input type="email" name="email" id="email" placeholder="Email" />
									</div>
									<div class="field">
										<textarea name="message" id="message" placeholder="Message"></textarea>
									</div>
									<ul class="actions">
										<li><input type="submit" value="Send" class="special" /></li>
									</ul>
								</form>
							</section>
							<section>
								<h2>Follow</h2>
								<ul class="icons">
									<li><a href="#" class="icon style2 fa-twitter"><span class="label">Twitter</span></a></li>
									<li><a href="#" class="icon style2 fa-facebook"><span class="label">Facebook</span></a></li>
									<li><a href="#" class="icon style2 fa-instagram"><span class="label">Instagram</span></a></li>
									<li><a href="#" class="icon style2 fa-dribbble"><span class="label">Dribbble</span></a></li>
									<li><a href="#" class="icon style2 fa-github"><span class="label">GitHub</span></a></li>
									<li><a href="#" class="icon style2 fa-500px"><span class="label">500px</span></a></li>
									<li><a href="#" class="icon style2 fa-phone"><span class="label">Phone</span></a></li>
									<li><a href="#" class="icon style2 fa-envelope-o"><span class="label">Email</span></a></li>
								</ul>
							</section>
							<ul class="copyright">
								<li>&copy; Untitled. All rights reserved</li><li>Design: <a href="http://html5up.net">HTML5 UP</a></li>
							</ul>
						</div>
					</footer>

			</div>

    </form>
   
		<!-- Scripts -->
			<script src="assets/js/jquery.min.js"></script>
			<script src="assets/js/skel.min.js"></script>
			<script src="assets/js/util.js"></script>
			<!--[if lte IE 8]><script src="assets/js/ie/respond.min.js"></script><![endif]-->
			<script src="assets/js/main.js"></script>
</body>
</html>
