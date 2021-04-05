<%@ Page Language="VB" AutoEventWireup="false" CodeFile="feedbackreport.aspx.vb" Inherits="_feedbackreport" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Canteen Feedback/Suggestionter</title>
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
  
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <!--==============================
              header
=================================-->
<header>
     <div align="Center">
       
        <h3>
        
            <strong>Canteen Feedback/ Suggestion
          
        </strong>
          
        </h3>
        </div>
      
 
</header>
        
<!--=====================
          Content
======================-->
<section class="content" id="txtLog">
 
  <br />
    
     
      
             
                
        
   
        <%--</div>--%>
    <div  id="divSummary" runat="server"   />
     <asp:LinkButton ID="LinkButton1" runat="server"> <img src="images/xls.gif" width=16 height=16 border=0 align=left /> Export to Excel</asp:LinkButton>
                   <asp:GridView ID="GridView1" runat="server"  CssClass="EU_DataTable" style="margin-left:50px;" >
                        <Columns>
        <asp:TemplateField>
              <HeaderTemplate>
                        S No.
              </HeaderTemplate>
              <ItemTemplate>
                  <%# Container.DataItemIndex + 1 %>
              </ItemTemplate>
          </asp:TemplateField>
                            </Columns>
                       <EmptyDataTemplate><div>No Data Available</div></EmptyDataTemplate></asp:GridView>
                 
              
</section>
<!--==============================
              footer
=================================-->
<footer id="footer">
  <div class="container">
    <div class="row">
      <div class="grid_12">
        <div class="socials">
         
        </div>
        
        </div>
      </div>
    </div>

</footer>
    </form>
</body>
</html>
