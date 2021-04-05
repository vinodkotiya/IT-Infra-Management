Imports dbOp
Imports Common
Imports System.Net
Imports System.IO
Imports System.Net.Mail
Partial Class _ocms
    Inherits System.Web.UI.Page

    Private Sub _Default_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack Then

                If Cache("menu2") Is Nothing Then
                    Cache.Insert("menu2", makemenu(2), Nothing, DateTime.Now.AddHours(12.0), TimeSpan.Zero)
                End If
                divMenu.InnerHtml = Cache("menu2").ToString

                executeDB("update hits set view = view+1 where page = 'ocms'")
                ' divFoot.InnerHtml = "<span class='brand'>IT-Corporate Center, SCOPE</span> &copy; <span id='copyright-year'></span> | HitCount: " & getDBsingle("select view from hits where page='ocms'") & " | Visitors Online: " & Application("OnlineUsers").ToString & " "
                If Cache("totalComplaint") Is Nothing Then
                    Dim totalComplaint = getDBsingle("select count(id) from ocms")
                    Cache.Insert("totalComplaint", totalComplaint, Nothing, DateTime.Now.AddHours(1.0), TimeSpan.Zero)
                End If
                divOCMSCount.InnerHtml = Cache("totalComplaint").ToString

                If Request.Params("ctype") Is Nothing Then
                    pnlHome.Visible = True
                    If Cache("wall1") Is Nothing Then
                        Cache.Insert("wall1", getWallofFame(), Nothing, DateTime.Now.AddMinutes(10.0), TimeSpan.Zero)
                    End If
                    divWallofFame.InnerHtml = Cache("wall1").ToString

                ElseIf Not Request.Params("ctype") = "admin" Then
                    If Session("eid") Is Nothing Then
                        pnlLogin.Visible = True
                        divMsg.InnerHtml = "Your IP Address:" & Request.UserHostAddress
                        Exit Sub
                    Else
                        LinkButton1.Visible = True
                        If Request.Params("ctype") = "status" Then
                            pnlComplaintBooking.Visible = False
                            pnlStatus.Visible = True
                            loadStatus(Session("eid"))
                            Exit Sub
                        End If
                        pnlComplaintBooking.Visible = True
                        loadComplaintForm(Request.Params("ctype").ToString)
                    End If
                Else
                    ''admin
                    If Session("adminid") Is Nothing Then
                        pnlAdminLogin.Visible = True
                        Exit Sub
                    Else
                        LinkButton1.Visible = True
                        pnlAdminStatus.Visible = True
                        loadAdminStatus(Session("groupid"), Session("core"))
                    End If
                End If




            End If
            '' do postback stuff here

        Catch e1 As Exception
            Response.Write("<div id='bottomline'>" & e1.Message & "</div>")
        End Try
    End Sub
    Function getWallofFame() As String
        Dim total = getDBsingle("select count(id) from ocms")
        Dim totaltoday = getDBsingle("select count(id) from ocms where date(st_dt) = current_date")
        Dim totalMonth = getDBsingle("select count(id) from ocms where strftime('%m', st_dt) = strftime('%m', current_date) and  strftime('%Y',st_dt) = strftime('%Y',current_date)")
        Dim smstoday = getDBsingle("select sms_count from sms where date(sms_dt) = current_date")
        Dim smslog = getDBsingle("select lastsmslog from sms where date(sms_dt) = current_date")
        Dim closelog = getDBsingle("select lastcloselog from sms where date(sms_dt) = current_date")
        If smstoday.Contains("Error") Then smstoday = "0"
        Dim smstotal = getDBsingle("select sum(sms_count) from sms where 1")
        Dim smsMonth = getDBsingle("select sum(sms_count) from sms where strftime('%m', sms_dt) = strftime('%m', current_date) and  strftime('%Y',sms_dt) = strftime('%Y',current_date)")
        Dim closed = getDBsingle("select count(id) from ocms where status='Closed'")
        Dim closedMonth = getDBsingle("select count(id) from ocms where status='Closed' and strftime('%m', end_dt) = strftime('%m', current_date) and  strftime('%Y',end_dt) = strftime('%Y',current_date)")
        Dim closedtoday = getDBsingle("select count(id) from ocms where status='Closed' and date(end_dt) = current_date")
        Dim pending = getDBsingle("select count(id) from ocms where status='Pending'")
        Dim pendingtoday = getDBsingle("select count(id) from ocms where status='Pending' and date(st_dt) = current_date")
        Dim pendingMonth = getDBsingle("select count(id) from ocms where status='Pending' and strftime('%m', st_dt) = strftime('%m', current_date) and  strftime('%Y',st_dt) = strftime('%Y',current_date)")
        Dim AvgRT As Double = getDBsingle(" select avg( (julianday(end_dt)- julianday(st_dt)) *1440) from ocms where status = 'Closed' and typeid < 7")
        Dim AvgRespondTime = Math.Floor(AvgRT / 60).ToString.PadLeft(2, "0") & ":" & Math.Floor(AvgRT Mod 60).ToString.PadLeft(2, "0")
        Dim mydt = getDBTable("select eid from users where wall = 1")
        Dim techname = ""
        Dim techcomp = ""
        Dim techcomptoday = ""
        Dim techcompMonth = ""
        Dim techAvgRespondTime = ""
        Dim walloffame = getDBsingle("select tech from (select tech, count(id) as d from ocms where date(end_dt) = current_date group by tech order by d desc) where d > 0  limit 1")
        For Each r In mydt.Rows
            If walloffame = r(0) Then
                techname = techname & "<th><a href='ocmsreport.aspx?tech=" & r(0) & "'>" & r(0) & "<img src=images/star.gif height='32px' width='32px' /></a></th>"
            Else
                techname = techname & "<th><a href='ocmsreport.aspx?tech=" & r(0) & "'>" & r(0) & "</a></th>"
            End If

            techcomp = techcomp & "<td>" & getDBsingle("select count(id) from ocms where tech='" & r(0) & "'") & "</td>"
            techcomptoday = techcomptoday & "<td>" & getDBsingle("select count(id) from ocms where tech='" & r(0) & "' and date(end_dt) = current_date") & "</td>"
            techcompMonth = techcompMonth & "<td>" & getDBsingle("select count(id) from ocms where tech='" & r(0) & "' and strftime('%m', end_dt) = strftime('%m', current_date) and  strftime('%Y',end_dt) = strftime('%Y',current_date)") & "</td>"
            Dim ret = getDBsingle(" select avg( (julianday(end_dt)- julianday(st_dt)) *1440) from ocms where status = 'Closed' and tech='" & r(0) & "'")
            Dim techavgtime As Double = 0
            If Not String.IsNullOrEmpty(ret) Then techavgtime = Double.Parse(ret)
            Dim totalhour As Integer = Math.Floor(techavgtime / 60)
            Dim totalmin As Integer = techavgtime Mod 60
            techAvgRespondTime = techAvgRespondTime & "<td>" & totalhour.ToString.PadLeft(2, "0") & ":" & totalmin.ToString.PadLeft(2, "0") & "</td>"
        Next
        Dim str = "<table class='EU_DataTable'><caption style='text-align: left;'>Stats Since June'16</caption><thead><th>!</th><th>Complaints</th><th><img src=images/sms.png width=40px /></th><th><img src=images/closed.png width=40px /></th><th><img src=images/pending.png width=100px height=40px /></th> " & techname & " </thead>" &
            "<tr><th>Total</th><td>" & total & "</td><td>" & smstotal & "</td><td>" & closed & "</td><td>" & pending & "</td>" & techcomp & "</tr>" &
              "<tr><th>Month</th><td>" & totalMonth & "</td><td>" & smsMonth & "</td><td>" & closedMonth & "</td><td>" & pendingMonth & "</td>" & techcompMonth & "</tr>" &
         "<tr><th>Today</th><td>" & totaltoday & "</td><td><div title='Log: " & smslog & "'>" & smstoday & "</div></td><td><div title='Log: " & closelog & "'>" & closedtoday & "</div></td><td>" & pendingtoday & "</td>" & techcomptoday & "</tr>" &
          "<tr><th colspan=3>Avg Resolution Time per Complaint</th><td>" & AvgRespondTime & "</td><td>-</td>" & techAvgRespondTime & "</tr></table>"

        mydt = getDBTable("select type, count(id) as d from ocms where typeid < 7 group by type order by d desc")
        Dim comp = ""
        Dim compcount = ""
        For Each r In mydt.Rows
            comp = comp & "<th>" & r(0).ToString.Replace("Floor", "") & "</th>"
            compcount = compcount & "<td>" & r(1) & "</td>"
        Next
        Dim strc = "<div style='width:98%;' > <table class='EU_DataTable' style='width:98%; '><caption style='text-align: left;'>Complaints Breakup</caption><thead>" & comp & " </thead>" &
            "<tr>" & compcount & "</td>" & "</table> </div>"

        mydt = getDBTable("select location, count(id) as d from ocms where 1 group by location order by d desc")
        Dim floor = ""
        Dim floorcount = ""
        For Each r In mydt.Rows
            floor = floor & "<th>" & r(0).ToString.Replace("Floor", "") & "</th>"
            floorcount = floorcount & "<td>" & r(1) & "</td>"
        Next
        Dim strf = "<div style='width:98%; overflow:auto;' > <table class='EU_DataTable' style='width:98%; overflow:auto;'><caption style='text-align: left;'>Floorwise Breakup</caption><thead>" & floor & " </thead>" &
            "<tr>" & floorcount & "</td>" & "</table> </div>"

        mydt = getDBTable("select strftime('%m', st_dt) as m, strftime('%Y', st_dt) as y, strftime('%m.%Y', st_dt) as my, count(id) as d from ocms where 1 group by my order by y desc, m desc")
        Dim mnth = ""
        Dim mnthcount = ""
        For Each r In mydt.Rows
            mnth = mnth & "<th>" & Left(MonthName(r(0).ToString), 3) & "'" & Right(r(1).ToString, 2) & "</th>"
            mnthcount = mnthcount & "<td>" & r(3) & "</td>"
        Next
        Dim strm = "<div style='width:98%; overflow:auto;' > <table class='EU_DataTable' style='width:98%; overflow:auto;'><caption style='text-align: left;'>Monthwise Breakup</caption><thead>" & mnth & " </thead>" &
            "<tr>" & mnthcount & "</td>" & "</table> </div>"
        Return str & "<br/>" & strc & "<br/>" & strf & "<br/>" & strm
    End Function
    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        If Session("adminid") Is Nothing Then
            Response.Redirect(Request.RawUrl)
            Exit Sub
        End If
        Dim searchid = ""
        If Not String.IsNullOrEmpty(txtSearchID.Text) Then searchid = " (id = '" & txtSearchID.Text & "' or eid = '" & txtSearchID.Text & "'  or name like '%" & txtSearchID.Text & "%') and "
        If Session("groupid").ToString.Length > 4 Then
            loadAdminStatus(ddlAdminFilter.SelectedValue, ddlAdminCore.SelectedValue, searchid)
        Else
            loadAdminStatus(Session("groupid"), Session("core"), searchid)
        End If
    End Sub
    Public Function loadStatus(ByVal eid As String)
        gvStatus.DataSource = getDBTable("select id as `Complaint No`, type, descr, priority, strftime('%d.%m.%Y %H:%M', datetime(st_dt,'+330 Minute')) as 'Date', status,  strftime('%d.%m.%Y %H:%M', datetime(Ifnull(ifnull(end_dt,st_dt),current_timestamp),'+330 Minute') ) as 'Closing Date', closingremark from ocms where  eid=" & eid & " order by id desc")
        gvStatus.DataBind()

    End Function
    Public Function loadAdminStatus(ByVal groupid As String, ByVal loc As String, Optional ByVal searchID As String = "")

        Dim q = "select id , type, descr, tech, cast(priority as text) as priority, strftime('%d.%m.%Y %H:%M', datetime(st_dt,'+330 Minute')) as 'Date', status, name || ' (' || eid || ')' as name , contact,dept, location, closingremark from ocms where " &
            searchID & " typeid in (" & groupid & ") and location like '%" & loc & "%' order by status desc, priority desc, st_dt asc limit 200" &
            ""
        '"union select id , type, descr, tech, cast(priority as text) as priority, strftime('%d.%m.%Y %H:%M', datetime(st_dt,'+330 Minute')) as 'Date', status, name || ' (' || eid || ')' as name , contact,dept, location, closingremark from ocms where not status = 'Pending' and typeid in (" & groupid & ") and location like '%" & loc & "%' order by status desc, priority desc, st_dt asc "
        'divMsg.InnerHtml = q
        ' Return 1
        gvAdminStatus.DataSource = getDBTable(q)
        gvAdminStatus.DataBind()

    End Function
    Public Function loadComplaintForm(ByVal complainttype As String)
        ddlCtype.DataSource = getDBTable("select id,type from complaintype where 1")
        ddlCtype.DataBind()
        ddlCtype.SelectedValue = complainttype
        ddlLocation.DataSource = getDBTable("select lid,loc from location")
        ddlLocation.DataBind()
        lblName.Text = Session("ename")
        lblMobile.Text = Session("mobile")
        lblEmail.Text = Session("email")
        txtDept.Text = Session("dept")
        If ddlCtype.SelectedValue = 1 Then txtDescr.Text = "My PC is Lenovo make" & vbCrLf
        If ddlCtype.SelectedValue = 6 Then
            '  txtDescr.Text = "Forgot/Change Password is Users Responsibility. Follow these steps: 1. Open mail.ntpc.co.in 2. At bottom of page click forgot password. " & vbCrLf & Session("email") & vbCrLf & "For any software/network related issue give complaint in that category."
            ' btnSubmit.Enabled = False
            txtDescr.Text = "Request for installation of Zimbra Desktop Client in my PC"
        ElseIf ddlCtype.SelectedValue = 5 Then
            txtDescr.Text = "Forgot/Change Password is Users Responsibility. Follow these steps for ESS Password: 1. Open ESS 2. At bottom of page click Get Support. " & vbCrLf &
                "Follow these steps for R3 Password: 1. Open ESS and Login 2. Go to Support Messages " & vbCrLf & "For any software/network related issue give complaint in that category."
            ' btnSubmit.Enabled = False
        End If
        If ddlCtype.SelectedValue = 4 Then txtDescr.Text = "My IP: " & Request.UserHostAddress & " (Delete this IP Address if this is not your PC)"
        '' set priority
        ''get day of last complaint for given complainttype
        Dim ret = getDBsingle("select  (julianday('now')- julianday(max(st_dt))) from ocms where typeid = " & complainttype & " and status = 'Closed' and eid = " & Session("eid"))
        Dim lastcompdayAgo = 0
        If String.IsNullOrEmpty(ret) Then
            ''no complaint exist for this user
            ddlPriority.SelectedValue = 1 ''normal
        Else
            lastcompdayAgo = Double.Parse(ret)
            If lastcompdayAgo <= 3 Then
                ddlPriority.SelectedValue = 3  ''high
            ElseIf lastcompdayAgo <= 6 Then
                ddlPriority.SelectedValue = 2  'medium
            Else
                ddlPriority.SelectedValue = 1 ''normal
            End If
        End If

        '' Retrive location
        ret = getDBsingle("select  lid from ocms, location where location=loc and  eid =   " & Session("eid") & "  order by st_dt desc limit 1")
        ddlLocation.SelectedValue = ret
        '' retrieve rax
        ret = getDBsingle("select  contact from ocms where   eid =   " & Session("eid") & "  order by st_dt desc limit 1")
        If ret.Contains("/") Then txtRax.Text = ret.Split("/")(1)

        divMsgComplaint.InnerHtml = "Your IP Address is:" & Request.UserHostAddress
    End Function
    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        '''Login web service call
        '''
        If String.IsNullOrEmpty(txtEid.Text) Then
            divMsg.InnerHtml = "Please enter employee number."
            Exit Sub
        End If
        Dim mydt = getDBTable("select name, dept, cell2, email from ntpcemp where loc like '%SCOPE%' and eid =" & txtEid.Text)
        If mydt.Rows.Count = 0 Then
            divMsg.InnerHtml = "Employee dosen't exist"
            ''write code to create sign up

            '' try to get employee data from other plant
            mydt = getDBTable("select name, dept, cell2, email from ntpcemp where  eid =" & txtEid.Text)
            pnlLogin.Visible = False
            pnlNewUser.Visible = True
            txtNewEid.Text = txtEid.Text
            If mydt.Rows.Count > 0 Then
                txtNewName.Text = mydt.Rows(0).Item(0).ToString
                txtNewMobile.Text = mydt.Rows(0).Item(2).ToString
                txtNewDept.Text = mydt.Rows(0).Item(1).ToString
            End If
            Exit Sub
        End If
        Session("ename") = mydt.Rows(0).Item(0).ToString
        Session("mobile") = mydt.Rows(0).Item(2).ToString
        Session("dept") = mydt.Rows(0).Item(1).ToString
        Session("email") = mydt.Rows(0).Item(3).ToString
        Session("eid") = txtEid.Text
        Session("Sendsms") = True
        Response.Redirect(Request.RawUrl)
    End Sub
    Private Sub btnAdminLogin_Click(sender As Object, e As EventArgs) Handles btnAdminLogin.Click
        If String.IsNullOrEmpty(txtAdminID.Text) Or String.IsNullOrEmpty(txtAdminPwd.Text) Then
            divMsg.InnerHtml = "Fields empty"
            Exit Sub
        End If
        Dim ret = getDBsingle("select groupid from users where eid ='" & txtAdminID.Text & "' and pwd='" & txtAdminPwd.Text & "' limit 1")
        If ret.Contains("Error") Then
            divMsg.InnerHtml = "Login incorrect"

        Else
            Session("adminid") = txtAdminID.Text
            Session("groupid") = ret
            ''To show only thier core complaints
            Session("core") = "%" 'getDBsingle("select core from users where eid ='" & txtAdminID.Text & "'")
            Response.Redirect(Request.RawUrl)
        End If

    End Sub

    Private Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        Dim collection As MatchCollection = Regex.Matches(txtDescr.Text, "\S+")
        Dim wordcount = collection.Count
        If wordcount < 6 Then
            divMsgComplaint.InnerHtml = "Please give detailed complaint description...You just entered " & wordcount & " words casually."
            Exit Sub
        End If
        If ddlCtype.SelectedValue = 6 And Not txtDescr.Text.Contains("ntpc.co.in") Then
            divMsgComplaint.InnerHtml = "Please enter your ntpc email id in complaint description..."
            Exit Sub
        End If
        '' check if same ipaddress is being used for another complaint today
        ''get day of last pending complaint for given complainttype
        Dim q = "select  st_dt from ocms where typeid = " & ddlCtype.SelectedValue & " and status = 'Pending' and eid = " & Session("eid") & " and ipaddress = '" & Request.UserHostAddress & "'"
        Dim ret = getDBsingle(q)
        If ret.Contains("Error") Then
            ''no complaint exist for this user and ip address
            ' ddlPriority.SelectedValue = 1
        Else
            divMsgComplaint.InnerHtml = "Failed: You have already registerd a complaint for this complaint type from same IP Address which is pending. Only one complaint is allowed per day from an IP Address.." '& q
            Exit Sub
        End If

        q = "insert into ocms (eid,name,dept,priority,type,typeid,descr,location,contact,status,ipaddress,st_dt) values(" &
                        Session("eid") & ", '" & lblName.Text.Replace("'", " ") & "', '" & txtDept.Text.Replace("'", " ") & "'," & ddlPriority.SelectedValue & ", '" & ddlCtype.SelectedItem.ToString & "'," & ddlCtype.SelectedValue & ", '" &
                        txtDescr.Text.Replace("'", " ") & "', '" & ddlLocation.SelectedItem.ToString & "', '" & lblMobile.Text & "/" & txtRax.Text & "', 'Pending','" & Request.UserHostAddress & "', current_timestamp)"
        '  divMsgComplaint.InnerHtml = q
        ret = executeDB(q)
        If ret.Contains("Error") Then
            divMsg.InnerHtml = "Unable to submit complaint contact: 7469" & ret & q
            Exit Sub
        End If

        If ddlCtype.SelectedValue = 7 Then  ''do nothing for buyback
            Response.Redirect("ocms.aspx?ctype=status")
            Exit Sub
        End If
        ''create message
        'ret = getDBsingle("select max(id) from ocms where eid = " & Session("eid") & " limit 1")
        ret = getDBsingle(" select  cast(max(id) as text) || ', Waiting Queue:'|| cast(count(id)+1 as text) from ocms where status = 'Pending' and priority = " & ddlPriority.SelectedValue & " and typeid = " & ddlCtype.SelectedValue & " and id <= ( select max(id) as d  from ocms where eid = " & Session("eid") & "  order by id desc )  ")
        Dim msg = "CC-IT OCMS Complaint ID:" & ret & vbCrLf & " for " & ddlLocation.SelectedItem.ToString & " by " & lblName.Text & ". Plz check status at ccit.ntpc.co.in"
        Dim msgp = vbCrLf & "Issue:" & ddlCtype.SelectedItem.ToString & " " & txtDescr.Text & ", Our technical support will attend your complaint asap." & vbCrLf & "To check status and lodge complaint visit ccit.ntpc.co.in or http://10.0.236.168/ccit (Save as favourite)" & vbCrLf & "- Team CC-IT " & vbCrLf & vbCrLf & "Do not reply to this email."
        Dim emailret = "Email=empty"
        If Not String.IsNullOrEmpty(lblEmail.Text) Then
            emailret = "Email=" & SendEmail("helpdesk@ntpc.co.in", lblEmail.Text, "", "", "CC-IT OCMS Complaint ID:" & ret, "Dear Ma'm/Sir " & vbCrLf & vbCrLf & msg & msgp, "", "", "", "")
        End If


        ''Get tech mobile number
        ' Exit Sub
        Dim techMobi = ""
        If ddlCtype.SelectedValue = "4" Or ddlCtype.SelectedValue = "5" Then
            techMobi = getDBSinglecommaSepearated("select mobile from users where groupid = " & ddlCtype.SelectedValue & "")
        Else
            techMobi = getDBsingle("select mobile from users where core like '%" & Left(ddlLocation.SelectedItem.ToString, 5) & "%' limit 1")
        End If
        'Try


        ' SMS Module.. 
        ' enter todays date count
        q = "insert into sms (sms_dt,sms_count) select current_date,0    WHERE not  EXISTS (select * from sms where sms_dt = current_date)"
        ret = executeDB(q)
        If ret.Contains("Error") Then
            divMsg.InnerHtml = "Unable to reset SMS" & ret & q
            Exit Sub
        End If
        q = "select sms_count from sms where sms_dt = current_date"
        ret = getDBsingle(q)
        If ret.Contains("Error") Then
            divMsg.InnerHtml = "Unable to get total sms sent today" & ret & q
            Exit Sub
        End If
        If Int32.Parse(ret) > 200 Or String.IsNullOrEmpty(lblMobile.Text) Then
            q = "update   sms set sms_count = sms_count + 0, lastsmslog = '" & "sms limit excede/no number" & lblMobile.Text & "," & techMobi & msgp & "SMS " & emailret & "' where sms_dt = current_date"
            Dim ret1 = executeDB(q)
            Response.Redirect("ocms.aspx?ctype=status&q=sms limit excede/no number")
            Exit Sub  ''dont send sms
        End If
        ''send sms
        Dim retsms = ""
        If Session("Sendsms") Then
            retsms = JustSendSMS(lblMobile.Text & "," & techMobi, msg)
            Session("Sendsms") = False
        Else
            divMsg.InnerHtml = "SMS already sent once.." & ret & q

        End If
        If retsms = "Success" Then
            ''update sms count
            q = "update   sms set sms_count = sms_count + 2, lastsmslog = '" & msg & lblMobile.Text & "," & techMobi & msgp & "SMS " & retsms & emailret & "' where sms_dt = current_date"
            Dim ret1 = executeDB(q)
            If ret1.Contains("Error") Then
                divMsg.InnerHtml = "Unable to update sms count today" & ret1 & q
                Exit Sub
            End If
        Else
            divMsg.InnerHtml = "Complaint Submitted.  Unable to send sms.(log out)" & retsms & q
            Response.Redirect("ocms.aspx?ctype=status&s=SMS Failed (log out/url not resolved) " & retsms & emailret)
            Exit Sub
        End If

        Response.Redirect("ocms.aspx?ctype=status&s=SMS sent" & lblMobile.Text & "," & techMobi & "&" & emailret)
        'Catch ex As Exception



        '    Response.Redirect("ocms.aspx?ctype=status")
        'End Try
    End Sub
    ''' <summary>
    ''' Moved to common
    ''' </summary>
    ''' <param name="mobileNumbersseperatedbycomma"></param>
    ''' <param name="msg"></param>
    ''' <returns></returns>
    Private Function SendSMS(ByVal mobileNumbersseperatedbycomma As String, ByVal msg As String) As String

        'Your authentication key
        Dim authKey As String = "115167Axudvox95756a790"
        'Multiple mobiles numbers separated by comma
        Dim mobileNumber As String = mobileNumbersseperatedbycomma
        'Sender ID,While using route4 sender id should be 6 characters long.
        Dim senderId As String = "NTPCIT"
        'Your message to send, Add URL encoding here.
        Dim message As String = HttpUtility.UrlEncode(msg)

        'Prepare you post parameters
        Dim sbPostData As New StringBuilder()
        sbPostData.AppendFormat("authkey={0}", authKey)
        sbPostData.AppendFormat("&mobiles={0}", mobileNumber)
        sbPostData.AppendFormat("&message={0}", message)
        sbPostData.AppendFormat("&sender={0}", senderId)
        sbPostData.AppendFormat("&route={0}", "2")

        Try
            'Call Send SMS API
            'Dim sendSMSUri As String = "https://control.msg91.com/api/sendhttp.php"
            Dim sendSMSUri As String = "http://54.254.154.166/api/sendhttp.php"
            'Create HTTPWebrequest
            Dim proxyObject As WebProxy = New WebProxy("http://10.0.236.36:8080")
            Dim httpWReq As HttpWebRequest = DirectCast(WebRequest.Create(sendSMSUri), HttpWebRequest)
            httpWReq.Proxy = proxyObject
            'Prepare and Add URL Encoded data
            Dim encoding As New UTF8Encoding()
            Dim data As Byte() = encoding.GetBytes(sbPostData.ToString())
            'Specify post method
            httpWReq.Method = "POST"
            httpWReq.ContentType = "application/x-www-form-urlencoded"
            httpWReq.ContentLength = data.Length
            Using stream As Stream = httpWReq.GetRequestStream()
                stream.Write(data, 0, data.Length)
            End Using
            'Get the response
            Dim response As HttpWebResponse = DirectCast(httpWReq.GetResponse(), HttpWebResponse)
            Dim reader As New StreamReader(response.GetResponseStream())
            Dim responseString As String = reader.ReadToEnd()

            'Close the response
            reader.Close()
            response.Close()
            Return "Success"
        Catch ex As Exception
            Return "Error:" & ex.Message.ToString()
        End Try
    End Function
    Private Function SendEmail(ByVal mailfrom As String, ByVal mailto As String, ByVal cc As String, ByVal bcc As String, ByVal subject As String, ByVal message As String, ByVal attach1 As String, ByVal attach2 As String, ByVal userid As String, ByVal pwd As String) As String
        Try
            'If Not mailto.Contains("ntpc.co.in") Then
            '    Return " Fail Non NTPC mail"
            'End If
            'create the mail message
            Dim mail As New MailMessage()

            'set the addresses
            mail.From = New MailAddress(mailfrom)
            mail.To.Add(mailto)
            ' mail.CC.Add("vinodkotiya@ntpc.co.in")
            'mail.Bcc.Add(bcc)

            'set the content
            mail.Subject = subject
            mail.Body = message

            'add an attachment from the filesystem
            '  mail.Attachments.Add(New Attachment(attach1))

            'to add additional attachments, simply call .Add(...) again
            '  mail.Attachments.Add(New Attachment(attach2))
            ' mail.Attachments.Add(New Attachment("c:\temp\example3.txt"))

            'send the message
            Dim mailClient As System.Net.Mail.SmtpClient = New System.Net.Mail.SmtpClient()

            'This object stores the authentication values
            Dim basicCredential As System.Net.NetworkCredential = New System.Net.NetworkCredential(userid, pwd)
            'SMTP gateway IPs 10.1.10.72 or 10.1.10.73 for mail flow out side ntpc or 10.1.8.119
            If mailto.Contains("ntpc.co.in") Or mailto.Contains("NTPC.CO.IN") Then
                mailClient.Host = "10.1.10.73"
            Else
                mailClient.Host = "10.1.10.73"
                ' mail.CC.Add("vinodkotiya@ntpc.co.in")
            End If

            mailClient.UseDefaultCredentials = False
            mailClient.Credentials = basicCredential
            mailClient.Send(mail)
            Return "ok"
        Catch exp As Exception

            Return "Error: " & exp.Message
        End Try

    End Function
    Private Sub LinkButton1_Click(sender As Object, e As EventArgs) Handles LinkButton1.Click
        Session.Clear()
        Session.Abandon()
        Response.Redirect("Default.aspx")
    End Sub

    Private Sub gvAdminStatus_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvAdminStatus.RowCommand
        Dim value = e.CommandArgument.ToString()
        If value = getDBsingle("select id from ocms where status = 'Closed' and id = " & value) And String.IsNullOrEmpty(txtSearchID.Text) Then
            divMsg.InnerHtml = "Already Closed: " & value
            Exit Sub
        End If
        lblCompID.Text = value
        lblCompDetail.Text = getDBsingle("select name || ', ' || contact || ', ' ||  dept || ', ' || descr  from ocms where id=" & value & " limit 1")
        lblEmailChange.Text = getDBsingle("select email from  ntpcemp where eid = (select eid from ocms where id = " & value & ") limit 1")
        lblMobileChange.Text = getDBsingle("select cell2 from  ntpcemp where eid = (select eid from ocms where id = " & value & ") limit 1")
        pnlAdminEdit.Visible = True
        ddlEdittype.DataSource = getDBTable("select id,type from complaintype where 1")
        ddlEdittype.DataBind()
        ddlEdittype.SelectedValue = getDBsingle("select typeid  from ocms where id=" & value & " limit 1")
        ddlEditLocation.DataSource = getDBTable("select lid,loc from location")
        ddlEditLocation.DataBind()
        ddlEditLocation.SelectedValue = getDBsingle("select  lid from ocms, location where location=loc and  id =   " & value & "   limit 1")
        ddlEditBy.DataSource = getDBTable("select eid as tech from users where  1")
        ddlEditBy.DataBind()
        ddlEditBy.SelectedValue = Session("adminid")
        Session("Sendclosesms") = True
    End Sub

    Private Sub btnChange_Click(sender As Object, e As EventArgs) Handles btnChange.Click
        Dim subq = ""
        If ddlStatus.SelectedItem.ToString = "Closed" Then
            subq = ", end_dt = current_timestamp"
        End If
        Dim q = "update ocms set tech = '" & ddlEditBy.SelectedValue & "', status='" & ddlStatus.SelectedItem.ToString & "', closingremark='" & txtClosingRemark.Text & "', last_updated = current_timestamp " & subq &
            ", location='" & ddlEditLocation.SelectedItem.ToString & "', type='" & ddlEdittype.SelectedItem.ToString & "', typeid=" & ddlEdittype.SelectedValue & ", ipadmin = '" & Request.UserHostAddress & "'  where id=" & lblCompID.Text
        Dim ret = executeDB(q)
        If ret.Contains("Error") Then
            divMsg.InnerHtml = "Unable to update" & ret & q
            Exit Sub
        End If

        '' Send email/sms
        Dim emailret = "Email=No"
        Dim retsms = "SMS=Fail"
        If ddlStatus.SelectedItem.ToString = "Closed" And Not String.IsNullOrEmpty(lblEmailChange.Text) Then
            Dim mailwarn = ""
            If ddlEdittype.SelectedValue = 6 Then mailwarn = " Please ensure to also change password in your mobile/other devices. Otherwise it will lock again."
            Dim msg = "Your complaint ID " & lblCompID.Text & " has been closed on " & Now.ToString & vbCrLf & "Remarks:" & mailwarn & txtClosingRemark.Text & vbCrLf & "Check status at http://10.0.236.168/ccit" & vbCrLf & vbCrLf & "- Team CC-IT"
            emailret = "Email=" & SendEmail("helpdesk@ntpc.co.in", lblEmailChange.Text, "", "", "CC-IT OCMS Complaint ID:" & lblCompID.Text, "Dear Ma'm/Sir " & vbCrLf & msg, "", "", "", "")
            If Not String.IsNullOrEmpty(lblMobileChange.Text) And Session("Sendclosesms") Then
                retsms = JustSendSMS(lblMobileChange.Text, msg)
                Session("Sendclosesms") = False
            End If
            If retsms = "Success" Then
                ''update sms count
                q = "update   sms set sms_count = sms_count + 1, lastcloselog = '" & msg & lblMobileChange.Text & " SMS " & retsms & emailret & "' where sms_dt = current_date"
                Dim ret1 = executeDB(q)
                If ret1.Contains("Error") Then
                    divMsg.InnerHtml = "Unable to update sms count today" & ret1 & q
                    Exit Sub
                End If
            End If
        End If

        Response.Redirect("ocms.aspx?ctype=admin&" & emailret & retsms)
    End Sub

    Private Sub gvStatus_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvStatus.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Cells(3).Text = "<img src=images/" & e.Row.Cells(3).Text & ".png width=40px />"
            e.Row.Cells(5).Text = "<img src=images/" & e.Row.Cells(5).Text & ".png width=40px />"
        End If

    End Sub

    Private Sub gvAdminStatus_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvAdminStatus.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then


            'If e.Row.Cells(11).ToString.Contains("Closed") Then
            '    Dim btnEdit = e.Row.FindControl("btnEdit")

            '    btnEdit.Visible = False
            '    '   e.Row.BackColor = Drawing.Color.LightGreen
            'End If
        End If
    End Sub

    Private Sub btnNewUser_Click(sender As Object, e As EventArgs) Handles btnNewUser.Click
        If String.IsNullOrEmpty(txtNewEid.Text) Or String.IsNullOrEmpty(txtNewName.Text) Or String.IsNullOrEmpty(txtNewDept.Text) Or String.IsNullOrEmpty(txtNewMobile.Text) Or String.IsNullOrEmpty(txtNewEmail.Text) Then
            divMsg.InnerHtml = "Fill all the fields"
            Exit Sub
        End If
        Dim q = "insert into   ntpcemp (eid, name, dept, cell2, email, loc) values ('" &
            txtNewEid.Text.Replace("'", "") & "', '" & txtNewName.Text.Replace("'", "") & "', '" & txtNewDept.Text.Replace("'", "") & "', '" & txtNewMobile.Text.Replace("'", "") & "', '" &
            txtNewEmail.Text & "', 'CC-SCOPE')"
        Dim ret = executeDB(q)
        If ret.Contains("Error") Then
            divMsg.InnerHtml = "Unable to create new user" & ret & q
            Exit Sub
        End If
        Response.Redirect("ocms.aspx")
    End Sub


End Class
