Imports dbOp
Imports Common
Imports System.Net
Imports System.IO
Imports System.Net.Mail
Partial Class _nw
    Inherits System.Web.UI.Page
    Private IPs As New List(Of String)()
    Private ipKey As Integer

    Private Sub _Default_Load(sender As Object, e As EventArgs) Handles Me.Load
        '   Try

        If Not Page.IsPostBack Then
            If Cache("menu6") Is Nothing Then
                Cache.Insert("menu6", makemenu(6), Nothing, DateTime.Now.AddHours(12.0), TimeSpan.Zero)
            End If
            divMenu.InnerHtml = Cache("menu6").ToString
            executeDB("update hits set view = view+1 where page = 'ccit'")
            '' executeDB("update hits set view = view+1 where page = 'LARR'")
            txtIPLog.Text = txtIPLog.Text & "Fetching  IP List..." & vbCrLf
            Dim mydt = getDBTable("select ip from network")
            For Each row In mydt.Rows
                IPs.Add(row(0))
                'create session object for each ip sms for the day
                If Session("SMSAttempt" + Now.ToString("dd-MM-yyyy") + row(0)) Is Nothing Then Session("SMSAttempt" + Now.ToString("dd-MM-yyyy") + row(0)) = False
            Next
            ipKey = 0
            ViewState("IPs") = IPs
            ViewState("ipKey") = ipKey

            RadioButtonList1_SelectedIndexChanged(vbNull, EventArgs.Empty)

            divFoot.InnerHtml = "<span class='brand'>IT-Corporate Center, SCOPE</span> &copy; <span id='copyright-year'></span> | HitCount: " & getDBsingle("select view from hits where page='ccit'") & " | Visitors Online: " & Application("OnlineUsers").ToString & " "
            'divFoot.InnerHtml = getTemperatureFTP()
            Dim lastupdateMin = dbOp.getDBsingle("select (strftime('%s','now')- strftime('%s',max(last_updated))) / 60 from networklog where 1 limit 1")
            If lastupdateMin < 5 And Request.Params("f") Is Nothing Then
                ' Just delete the data having ongoing onwards from last 5 min
                txtIPLog.Text = "Frequent update restricted. Report is already updated in last 5 min.." & Now & vbCrLf & "Last log >>>>>>" & vbCrLf & getDBsingle("select lastnwlog from sms where 1 order by sms_dt desc limit 1")
                Timer1.Enabled = False

            Else
                txtIPLog.Text = "Send SMS for Down links followed by Report Updation Triggered at.." & Now
                sendSMSforDownLinks
            End If
            'Exit Sub
            Dim lastupdate = dbOp.getDBsingle("select max(last_updated)  from networklog where 1 limit 1")
            divHead.InnerHtml = "<div><table><tr><td>CC-IT Network as on:" & lastupdate & " (Click on The Device to Check Current Status). </td><td> " &
            " <div style='width: 10px;	height: 10px;	border-radius: 50%; background: rgb(6, 129, 6);' /></td><td> Working &nbsp;&nbsp;</td><td> " &
            " <div  style='width: 10px;	height: 10px;	border-radius: 50%; background: rgb(246, 14, 14);' /> </td><td>Down &nbsp;&nbsp;</td><td> " &
                " <div  style='width: 10px;	height: 10px;	border-radius: 50%; background: rgb(231, 180, 83);' /> </td><td>S/D &nbsp;&nbsp;</td><td> " &
             " <div  style='width: 10px;	height: 10px;	border-radius: 50%; background: rgb(14, 14, 246);' /> </td><td>Working but status NA &nbsp;&nbsp;</td></tr></table></div>"
            '' get current  time to send email, but first check f=1 so that mail cant be send by visitors 

            Dim sendtime = "09:50" 'will send 1 hr before sendtime 17:40
            If DateDiff(DateInterval.Minute, TimeValue(Now), TimeValue(sendtime)) < 60 And DateDiff(DateInterval.Minute, TimeValue(Now), TimeValue(sendtime)) > 1 Then
                txtIPLog.Text = "Its time to send email report " & DateDiff(DateInterval.Minute, TimeValue(Now), TimeValue(sendtime)).ToString & "min left for closing window of " & sendtime & ".. But first check email already sent or not." & vbCrLf & txtIPLog.Text
                If Request.Params("f") Is Nothing Then
                    txtIPLog.Text = "This client is not authorised to send emails" & vbCrLf & txtIPLog.Text
                    Exit Sub
                Else
                    txtIPLog.Text = "This client is authorised to send emails" & vbCrLf & txtIPLog.Text
                End If

                '' double checks to prevent mail spamming 1. get value from database 2. create a date session
                If Session("EmailAttempt" + Now.ToString("dd-MM-yyyy")) Is Nothing Then
                    Session("EmailAttempt" + Now.ToString("dd-MM-yyyy")) = False
                End If
                Dim ret = getDBsingle("select lastnwlog from sms where sms_dt = current_date ")
                If ret.Contains("Email Status:ok") Or Session("EmailAttempt" + Now.ToString("dd-MM-yyyy")) Then
                    txtIPLog.Text = "Email Sending Fail:(Double checks)Todays report already emailed or EmailAttempt" & Now.ToString("dd-MM-yyyy") & Session("EmailAttempt" + Now.ToString("dd-MM-yyyy")) & ".{{" & ret & "}}" & vbCrLf & txtIPLog.Text
                Else
                    txtIPLog.Text = "Todays report not emailed and EmailAttempt" & Now.ToString("dd-MM-yyyy") & Session("EmailAttempt" + Now.ToString("dd-MM-yyyy")) & ".Triggering mail send module..." & ret & vbCrLf & txtIPLog.Text
                    Timer1.Enabled = False
                    ''''' Bypass
                    Exit Sub
                    download2Mail("nwr" + Now.ToString("dd-MM-yyyy") + ".xls")
                    '' Also send daily exception sms
                    txtIPLog.Text = "Triggering Daily Exception" & vbCrLf & txtIPLog.Text
                    sendDailyExceptionSMSforDownLinks()
                End If
            Else
                txtIPLog.Text = "Its not scheduled time to send email report.. quitting.." & DateDiff(DateInterval.Minute, TimeValue(Now), TimeValue(sendtime)).ToString & "min lapsed from closing window of " & sendtime & vbCrLf & txtIPLog.Text
            End If
        End If
        '' do postback stuff here

        'Catch e1 As Exception
        '    Response.Write("<div id='bottomline'>" & e1.Message & "</div>")
        'End Try
    End Sub
    Function loadReport(ByVal startdate As Date, ByVal enddate As Date) As Boolean
        Dim q = "SELECT n.ip, l.log_dt, l.status, (l.avbl * 100 / l.try) as 'Avail(%)'  FROM network n, networklog l where n.ip = l.ip and l.log_dt between '" & startdate.ToString("yyyy-MM-dd") & "' and '" & enddate.ToString("yyyy-MM-dd") & "' "
        Dim dprDT = dbOp.getDBTable(q)
        'Get Network Data
        Dim nwDT = getDBTable("SELECT name, ip, status,grp FROM network where 1 order by grp desc")
        Dim finalDT As New System.Data.DataTable()

        '''####### Creating Columns
        finalDT.Columns.Add("SN")
        finalDT.Columns.Add("Description")
        'finalDT.Columns.Add("IP")

        Dim CurrD As DateTime = startdate
        While (CurrD <= enddate)
            finalDT.Columns.Add(CurrD.ToString("dd.MM.yy"))
            CurrD = CurrD.AddDays(1)
        End While
        Dim i = 1
        Dim grp = ""
        For Each nwRow In nwDT.Rows


            Dim rowStr = ""

            If Not grp = nwRow("grp") Then
                Dim tmpRow1 = finalDT.NewRow
                tmpRow1("SN") = ""
                tmpRow1("Description") = nwRow("grp")
                grp = nwRow("grp")
                finalDT.Rows.Add(tmpRow1)
            End If
            Dim tmpRow = finalDT.NewRow
            tmpRow("SN") = i
            tmpRow("Description") = nwRow("name") + " (" + nwRow("ip") + ")"
            'tmpRow("IP") = nwRow("ip")
            CurrD = startdate
            ''## Loop throu all dates for each activity
            While (CurrD <= enddate)
                Dim dr = dprDT.Select(" log_dt = '" & CurrD.ToString("yyyy-MM-dd") & "' and ip = '" & nwRow("ip") & "'")
                If dr.Length > 0 Then
                    ''Not acesbile then set ststus to -1
                    tmpRow(CurrD.ToString("dd.MM.yy")) = If(nwRow("status") = "-1", "OK", If(nwRow("status") = "0", "Down", If(nwRow("status") = "2", "S/D", dr(0)(3))))
                    ' tmpRow(CurrD.ToString("dd.MM.yy")) =
                Else
                    tmpRow(CurrD.ToString("dd.MM.yy")) = ""
                End If

                CurrD = CurrD.AddDays(1)
            End While

            finalDT.Rows.Add(tmpRow)
            i = i + 1
        Next
        ' GridView1.DataSource = dprDT
        GridView1.DataSource = finalDT
        GridView1.DataBind()
        For Each gvrow As GridViewRow In GridView1.Rows
            gvrow.BackColor = Drawing.Color.White
            If gvrow.Cells(2).Text = "1" Or gvrow.Cells(2).Text = "0" Then gvrow.Cells(2).Text = "Down"
            If gvrow.Cells(2).Text = "100" Then gvrow.Cells(2).BackColor = Drawing.Color.LimeGreen
            If gvrow.Cells(2).Text = "Down" Then gvrow.Cells(2).BackColor = Drawing.Color.OrangeRed
            If gvrow.Cells(2).Text = "S/D" Then gvrow.Cells(2).BackColor = Drawing.Color.Yellow
            If gvrow.Cells(2).Text = "OK" Then gvrow.Cells(2).BackColor = Drawing.Color.CornflowerBlue
            If gvrow.Cells(1).Text = "LAN" Or gvrow.Cells(1).Text = "WAN" Or gvrow.Cells(1).Text = "VC" Or gvrow.Cells(1).Text = "IT Services" Or gvrow.Cells(1).Text = "Internet" Then gvrow.BackColor = Drawing.Color.Goldenrod
        Next
        'divMsg.InnerHtml = q & CurrD.ToString()
        Return True
    End Function
    Private Function sendDailyExceptionSMSforDownLinks() As Boolean
        'get list of ip down in last 60 min only and sms = 0
        Dim q = "select name || '-' || ip, avail from (SELECT n.name, n.ip, l.log_dt, l.status, avg(l.avbl * 100 / l.try) as 'Avail'  FROM network n, networklog l where n.ip = l.ip and n.status = 1 and l.log_dt > date('now','-2 day')  group by n.ip) where avail < 99"
        '  Dim q = "select name, l.ip, round((JulianDay(l.last_updated) - JulianDay(last_live)) * 24 * 60) as m, n.status , l.sms from networklog l, network n where m > 10 and  (not l.sms = 1 or l.sms is null) and n.status = 1 and n.ip = l.ip and log_dt = DATE(current_date, '+330 minutes')  order by m desc"
        Dim mydt = getDBTable(q)
        Dim smsMsg = "SCOPE Network Exception:" & vbCrLf
        If mydt.Rows.Count = 0 Then
            smsMsg = smsMsg & "Network 100% Available. No Link is down since yesterday."
            txtIPLog.Text = smsMsg & vbCrLf & txtIPLog.Text
            ' Return False
        Else
            smsMsg = smsMsg & "Following Links were down since yesterday with Avail(%):" & vbCrLf
            txtIPLog.Text = smsMsg & txtIPLog.Text

            For Each r In mydt.Rows

                '' make message for this trigger
                smsMsg = smsMsg & r(0) & "-" & r(1) & "%" & vbCrLf
                '' double check prevention 
            Next

        End If
        If Not String.IsNullOrEmpty(smsMsg) Then
            ''Call SMS function as there is a message
            Dim techMobi = getDBSinglecommaSepearated("select mobile from users where groupid = 99 or groupid = 4 or groupid = 97 or groupid = 6 ")
            txtIPLog.Text = "SMS Status:" & JustSendSMS(techMobi, smsMsg) & " SMS Sent: Following Link is down yesterday." & smsMsg & techMobi & vbCrLf & txtIPLog.Text
        Else
            ''SMS Already sent/attempted so aborting
            txtIPLog.Text = "SMS Already sent/attempted for this ip, so aborting.." & vbCrLf & txtIPLog.Text
        End If
        Return True
    End Function
    Private Function sendSMSforDownLinks() As Boolean
        Return True
        'get list of ip down in last 60 min only and sms = 0
        Dim q = "select name, l.ip, round((JulianDay(l.last_updated) - JulianDay(last_live)) * 24 * 60)  as m, n.status , l.sms from networklog l, network n where m > 10 and m < 60 and  (not l.sms = 1 or l.sms is null) and n.status = 1 and n.ip = l.ip and log_dt = DATE(current_date, '+330 minutes')  order by m desc"
        '  Dim q = "select name, l.ip, round((JulianDay(l.last_updated) - JulianDay(last_live)) * 24 * 60) as m, n.status , l.sms from networklog l, network n where m > 10 and  (not l.sms = 1 or l.sms is null) and n.status = 1 and n.ip = l.ip and log_dt = DATE(current_date, '+330 minutes')  order by m desc"
        Dim mydt = getDBTable(q)
        If mydt.Rows.Count = 0 Then
            txtIPLog.Text = "No Link is down in last 60 min or SMS already sent today. Aborting send sms module..." & vbCrLf & txtIPLog.Text
            Return False
        Else
            txtIPLog.Text = "Some Link is down in last 60 min. Attempting send sms module..." & vbCrLf & txtIPLog.Text
            Dim smsMsg = "" ''Caution always put this empty to prevent sms send
            For Each r In mydt.Rows

                ''set sms attemnpt session to true.. now if sms will not delivered but dont attempt sms 
                Dim sendtime = "01:30" '' dont send sms before this
                If Session("SMSAttempt" + Now.ToString("dd-MM-yyyy") + r(1)) = False And TimeValue(Now) > TimeValue(sendtime) Then
                    '' make message for this trigger
                    Dim temp = "down"
                    If r(1) = "10.0.236.21" Then temp = " Temperature exceeds 27 C "
                    smsMsg = smsMsg & r(0) & " (" & r(1) & ") " & temp & " for " & r(2) & " min " & vbCrLf
                    '' double check prevention 
                    Session("SMSAttempt" + Now.ToString("dd-MM-yyyy") + r(1)) = True
                    q = "update networklog set sms=1 where log_dt = DATE(current_date, '+330 minutes')  and ip = '" & r(1) & "'"
                    Dim ret = executeDB(q)
                    If ret.Contains("Error") Then
                        txtIPLog.Text = "unable to upadte sms record for " & q & vbCrLf & txtIPLog.Text
                    Else
                        txtIPLog.Text = "Double check prevention done sms = 1 and session " & "SMSAttempt" + Now.ToString("dd-MM-yyyy") + r(1) & " = true " & q & vbCrLf & txtIPLog.Text
                    End If
                Else
                    txtIPLog.Text = "SMSAttempt" + Now.ToString("dd-MM-yyyy") + r(1) & " = false or sms send time awaited. " & sendtime & vbCrLf & txtIPLog.Text
                End If

            Next
            If Not String.IsNullOrEmpty(smsMsg) Then
                ''Call SMS function as there is a message
                Dim techMobi = getDBSinglecommaSepearated("select mobile from users where groupid = 99 or groupid = 4 or groupid = 6 ")
                txtIPLog.Text = "SMS Status:" & JustSendSMS(techMobi, smsMsg) & " SMS Sent: Following Link is down in last 60 min." & smsMsg & techMobi & vbCrLf & txtIPLog.Text
            Else
                ''SMS Already sent/attempted so aborting
                txtIPLog.Text = "SMS Already sent/attempted for this ip, so aborting.." & vbCrLf & txtIPLog.Text
            End If
        End If
        Return True
    End Function
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Try
            'Check when last updated if in last 5 min or forced then only update or skip



            Dim msg = ""
            Timer1.Enabled = False
            IPs = ViewState("IPs")
            ipKey = ViewState("ipKey")
            If ipKey < IPs.Count Then

                Dim ret = ""
                ''' Ping from other servers web service for public ip
                If IPs(ipKey).ToString.StartsWith("8.8") Or IPs(ipKey).ToString.StartsWith("61") Or IPs(ipKey).ToString.StartsWith("216") Or IPs(ipKey).ToString.StartsWith("202") Then
                    Try
                        Dim vin As New vinWebService.vinservice()
                        ret = vin.pingIP(IPs(ipKey))
                        msg = "Try External Ping " & ipKey & "..." & IPs(ipKey)
                    Catch ex1 As Exception
                        msg = ex1.Message + "Error External Ping " & ipKey & "..." & IPs(ipKey)
                    End Try

                Else
                    '  Dim ret = vin.pingIP(IPs(ipKey))
                    ''' Ping from same servers web service for local ip
                    Dim vin As New vinservice
                    ret = vin.pingIP(IPs(ipKey))
                    msg = "Try Ping " & ipKey & "..." & IPs(ipKey)
                End If
                Dim avbl = 0
                    Dim status = 0
                    If ret.Contains("Success") Then
                        avbl = 1
                        status = 1
                    ElseIf ret.Contains("TtlExpired") Or ret.Contains("DestinationHostUnreachable") Then
                        avbl = 0
                        status = -1
                    End If
                    msg = msg & " Ping result " & ret & "..."
                    '' Check date exist or not for this IP
                    Dim q = "insert into networklog (log_dt,ip, try, avbl) select current_date,'" & IPs(ipKey) & "', 1, 1    WHERE not  EXISTS (select * from networklog where log_dt = DATE(current_date, '+330 minutes')  and ip = '" & IPs(ipKey) & "')"
                    ret = executeDB(q)
                    If ret.Contains("Error") Then
                        txtIPLog.Text = "Unable to create first log  today with ip " & IPs(ipKey) & ret & q & vbCrLf & txtIPLog.Text
                        Exit Sub
                    End If

                    '' now update the availability log
                    q = "update networklog set last_updated= DATETIME(current_timestamp, '+330 minutes'), try = try + 1, status = " & status & ", avbl = avbl + " & avbl & ", last_live = case when 1 = " & avbl & " then DATETIME(current_timestamp, '+330 minutes') else last_live end  where log_dt = DATE(current_date, '+330 minutes')  and ip = '" & IPs(ipKey) & "'"
                    ret = executeDB(q)
                    If ret.Contains("Error") Then
                        txtIPLog.Text = "Unable to update log  today with ip " & IPs(ipKey) & ret & q & vbCrLf & txtIPLog.Text
                        Exit Sub
                    End If

                    '' now update the status log
                    'q = "update network set last_updated= current_timestamp, avbl = (select (avbl*100 / try) from networklog where networklog.ip = network.ip) " &
                    '        " where rowid in (select network.rowid from networklog where networklog.ip = network.ip)'"
                    'ret = executeDB(q)
                    'If ret.Contains("Error") Then
                    '    txtIPLog.Text = "Unable to update the status log  today with ip " & IPs(ipKey) & ret & q & vbCrLf & txtIPLog.Text
                    '    Exit Sub
                    'End If

                    msg = msg & ret & " Data updated.." '& q
                    txtIPLog.Text = msg & vbCrLf & txtIPLog.Text
                    ipKey = ipKey + 1
                    ViewState("ipKey") = ipKey
                    Timer1.Enabled = True
                Else
                    Timer1.Enabled = False

                '' last task get temperature from sensor'''''''''
                Dim temp = 0
                Dim ret1 = getTemperatureFTP()
                temp = Int32.Parse(ret1)
                Dim status = If(temp < 27, 1, 0)
                '' now update the availability log
                Dim q = "update networklog set last_updated= DATETIME(current_timestamp, '+330 minutes'), try = 1, status = " & status & ", avbl =  " & temp & ", last_live = case when 1 = " & status & " then DATETIME(current_timestamp, '+330 minutes') else last_live end  where log_dt = DATE(current_date, '+330 minutes')  and ip = '10.0.236.21'"
                Dim ret = executeDB(q)
                If ret.Contains("Error") Then
                    txtIPLog.Text = "Unable to update log  today with ip 10.0.236.21" & ret & q & vbCrLf & txtIPLog.Text
                    Exit Sub
                End If
                '''last task ends''''''''''
                '''
                txtIPLog.Text = "List Over. Last task getTemperature performed " & temp & " C. Scheduler Stopped at " & ipKey & "..." & vbCrLf & txtIPLog.Text

            End If


        Catch ex As Exception
            txtIPLog.Text = ex.Message
            Timer1.Enabled = False
        End Try
    End Sub
    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Dim st_dt = DateTime.ParseExact(dt_stTextBox.Text, "dd.MM.yyyy", Nothing)
        Dim end_dt = DateTime.ParseExact(dt_endTextBox.Text, "dd.MM.yyyy", Nothing)
        If DateDiff(DateInterval.Day, st_dt, end_dt) < 32 Then
            loadReport(st_dt, end_dt.AddHours(23))
        Else
            divMsg.InnerHtml = "Range can't be more then 31 days"
        End If
    End Sub
    Private Function SendEmail(ByVal mailfrom As String, ByVal mailto As String, ByVal cc As String, ByVal bcc As String, ByVal subject As String, ByVal message As String, ByVal attach1 As String, ByVal attach2 As String, ByVal userid As String, ByVal pwd As String) As String
        'Try
        'If Not mailto.Contains("ntpc.co.in") Then
        '    Return " Fail Non NTPC mail"
        'End If
        'create the mail message
        Dim mail As New MailMessage()

        'set the addresses
        mail.IsBodyHtml = True
        mail.From = New MailAddress(mailfrom)
        mail.To.Add(mailto)
        mail.CC.Add(cc)
        'mail.Bcc.Add(bcc)
        Dim htmlView As AlternateView = AlternateView.CreateAlternateViewFromString(message + "<br/>Snapshot:" & divHead.InnerHtml & "<br/><img src=cid:nw>", Nothing, "text/html")
        'create the LinkedResource (embedded image)
        Dim path As String = HttpContext.Current.Server.MapPath("~/DPR/")
        Dim fileNameWitPath As String = path + Now.ToString("dd.MM.yy") + ".png"
        If System.IO.File.Exists(fileNameWitPath) Then
            Dim logo As New LinkedResource(fileNameWitPath)
            logo.ContentId = "nw"
            'add the LinkedResource to the appropriate view
            htmlView.LinkedResources.Add(logo)
        End If
        'set the content
        mail.Subject = subject
        ' mail.Body = message
        mail.AlternateViews.Add(htmlView)

        'add an attachment from the filesystem
        mail.Attachments.Add(New Attachment(attach1))

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
        End If

        mailClient.UseDefaultCredentials = False
        mailClient.Credentials = basicCredential
        mailClient.Send(mail)
        Return "ok"
        'Catch exp As Exception

        '    Return "Error: " & exp.Message
        'End Try

    End Function

    Function download2Mail(ByVal filename As String)
        Dim q = "insert into sms (sms_dt,sms_count) select current_date,0    WHERE not  EXISTS (select * from sms where sms_dt = current_date)"
        Dim ret = executeDB(q)
        ' Change the Header Row back to white color
        GridView1.HeaderRow.Style.Add("background-color", "#FFFFFF")
        '   gvDPR.Columns.RemoveAt(0)
        ' This loop is used to apply stlye to cells based on particular row
        For Each gvrow As GridViewRow In GridView1.Rows
            gvrow.BackColor = Drawing.Color.White
            If gvrow.Cells(2).Text = "1" Or gvrow.Cells(2).Text = "0" Then gvrow.Cells(2).Text = "Down"
            If gvrow.Cells(2).Text = "100" Then gvrow.Cells(2).BackColor = Drawing.Color.LimeGreen
            If gvrow.Cells(2).Text = "Down" Then gvrow.Cells(2).BackColor = Drawing.Color.OrangeRed
            If gvrow.Cells(2).Text = "S/D" Then gvrow.Cells(2).BackColor = Drawing.Color.Yellow
            If gvrow.Cells(2).Text = "OK" Then gvrow.Cells(2).BackColor = Drawing.Color.CornflowerBlue
            If gvrow.Cells(1).Text = "LAN" Or gvrow.Cells(1).Text = "WAN" Or gvrow.Cells(1).Text = "VC" Or gvrow.Cells(1).Text = "IT Services" Or gvrow.Cells(1).Text = "Internet" Then gvrow.BackColor = Drawing.Color.Goldenrod

        Next



        Dim path As String = Server.MapPath("~/DPR/")
        If Not System.IO.Directory.Exists(path) Then
            System.IO.Directory.CreateDirectory(path)
        End If
        If Not System.IO.File.Exists(path & filename) Then
            Using sw As New System.IO.StringWriter()
                Using hw As New HtmlTextWriter(sw)
                    Dim writer As System.IO.StreamWriter = System.IO.File.CreateText(path & filename)

                    writer.WriteLine("SCOPE-Network Report " & Now.ToString("dd.MM.yyyy"))
                    writer.WriteLine("_")
                    GridView1.RenderControl(hw)
                    writer.WriteLine(sw.ToString())
                    writer.WriteLine("_")
                    writer.WriteLine("This is a system generated report. Real time status can be seen at url http://10.0.236.168/ccit/nw.aspx")
                    writer.Close()
                End Using
            End Using
            txtIPLog.Text = "xls file generated" & vbCrLf & txtIPLog.Text
        Else
            txtIPLog.Text = "xls file already exist" & vbCrLf & txtIPLog.Text
        End If
        Dim test = 0
        Dim mailcc = "vinodkotiya@ntpc.co.in"
        Dim mailto = "vinodkotiya@ntpc.co.in"
        If test = 0 Then
            mailto = "rkanth@ntpc.co.in, akpatel@ntpc.co.in, bodhraj@ntpc.co.in, rspanwar@ntpc.co.in, vkhatri@ntpc.co.in, rajeshchoudhary@ntpc.co.in, anupambanerjee@ntpc.co.in, vinodkotiya@ntpc.co.in, ksnirwan@ntpc.co.in, meenaagarwal@ntpc.co.in"
            mailcc = "cvrao@ntpc.co.in"
        End If

        Dim msg = "Dear Ma'm/Sir " & vbCrLf & vbCrLf &
            "<br/><br/>Please find attached system generated network log of Video Conferencing,Internet Service,Lease lines & IT Services status  at CC-Scope Complex." & vbCrLf &
            "<br/><br/> Real time status can be seen at url <a href=http://10.0.236.168/ccit/nw.aspx target=_blank>ccit.ntpc.co.in/nw.aspx</a> " &
            ""
        ret = SendEmail("CCIT-NOREPLY@ntpc.co.in", mailto, mailcc, "", "SCOPE-Network Report " & Now.ToString("dd.MM.yyyy"), msg, path + filename, "", "", "")
        txtIPLog.Text = "xls file  mailed " & ret & filename & vbCrLf & txtIPLog.Text
        q = "update   sms set lastnwlog = '" & "Email Status:" & ret & vbCrLf & txtIPLog.Text & "' where sms_dt = current_date"
        Dim ret1 = executeDB(q)
        txtIPLog.Text = "Todays Report emailed " & ret1 & " from " & Request.UserHostAddress & vbCrLf & txtIPLog.Text
        Session("EmailAttempt" + Now.ToString("dd-MM-yyyy")) = True

    End Function



    Private Sub LinkButton1_Click(sender As Object, e As EventArgs) Handles LinkButton1.Click
        If GridView1.Rows.Count > 0 Then

            saveExcel()
        Else
            ' lblMsg.Text = "Please Upload MPP File First."
        End If
    End Sub
    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        Return
    End Sub
    Sub saveExcel()
        ' Change the Header Row back to white color
        GridView1.HeaderRow.Style.Add("background-color", "#FFFFFF")

        ' This loop is used to apply stlye to cells based on particular row
        For Each gvrow As GridViewRow In GridView1.Rows
            gvrow.BackColor = Drawing.Color.White

            If gvrow.Cells(2).Text = "100" Then gvrow.Cells(2).BackColor = Drawing.Color.LimeGreen
            If gvrow.Cells(2).Text = "Down" Then gvrow.Cells(2).BackColor = Drawing.Color.OrangeRed
            If gvrow.Cells(2).Text = "S/D" Then gvrow.Cells(2).BackColor = Drawing.Color.Yellow
            If gvrow.Cells(2).Text = "OK" Then gvrow.Cells(2).BackColor = Drawing.Color.CornflowerBlue
            If gvrow.Cells(1).Text = "LAN" Or gvrow.Cells(1).Text = "WAN" Or gvrow.Cells(1).Text = "VC" Or gvrow.Cells(1).Text = "IT Services" Or gvrow.Cells(1).Text = "Internet" Then gvrow.BackColor = Drawing.Color.Goldenrod

            'If gvrow.Cells(4).Text = "True" Then
            '    gvrow.BackColor = Drawing.Color.Yellow
            '    'For k As Integer = 0 To gvrow.Cells.Count - 1
            '    '    gvrow.Cells(k).Style.Add("background-color", "#EFF3FB")
            '    'Next
            'End If
        Next

        Response.ClearContent()

        Response.AddHeader("content-disposition", "attachment; filename=GridViewToExcel.xls")

        Response.ContentType = "application/excel"

        Dim sWriter As New System.IO.StringWriter()

        Dim hTextWriter As New HtmlTextWriter(sWriter)

        divSummary.RenderControl(hTextWriter)
        GridView1.RenderControl(hTextWriter)


        Response.Write(sWriter.ToString())

        Response.End()
        'lblMsg.Text = "Excel created"

        'GridView1.RenderControl(htw)

    End Sub

    Private Sub RadioButtonList1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles RadioButtonList1.SelectedIndexChanged
        Dim sel As String = RadioButtonList1.SelectedValue

        If sel = "Monthly" Then
            Dim lastmonth As Date = New DateTime(Now.Year, Now.Month, 1)

            dt_stTextBox.Text = lastmonth.ToString("dd.MM.yyyy")
            dt_endTextBox.Text = Now.ToString("dd.MM.yyyy")

            loadReport(lastmonth, Now)

        ElseIf sel = "Weekly" Then
            Dim lastweek As Date = Today.AddDays(-7)
            dt_stTextBox.Text = lastweek.ToString("dd.MM.yyyy")
            dt_endTextBox.Text = Now.ToString("dd.MM.yyyy")
            loadReport(lastweek, Now)

        ElseIf sel = "Today" Then

            Dim nextDay As Date = Today.AddHours(23)
            dt_stTextBox.Text = Today.ToString("dd.MM.yyyy")
            dt_endTextBox.Text = nextDay.ToString("dd.MM.yyyy")
            loadReport(Today, nextDay)
        ElseIf sel = "All Time" Then
            '    chkShowSite.Checked = True
            Dim prevDay As Date = New Date(2016, 1, 1)
            dt_stTextBox.Text = prevDay.ToString("dd.MM.yyyy")
            dt_endTextBox.Text = Now.ToString("dd.MM.yyyy")
            loadReport(prevDay, Now)
        End If
    End Sub
    Function getTemperatureFTP() As String
        Dim fileName As String = "data.txt"
        'FTP Server URL.
        Dim ftp As String = "ftp://10.0.236.21/"

        'FTP Folder name. Leave blank if you want to Download file from root folder.
        Dim ftpFolder As String = ""


        'Create FTP Request.
        Dim request = DirectCast(WebRequest.Create(Convert.ToString(ftp & ftpFolder) & fileName), FtpWebRequest)
        Dim resp As FtpWebResponse
        Dim stream As New StreamReader(Server.MapPath("./") & "nw.aspx.vb")
        Try

            request = DirectCast(WebRequest.Create(Convert.ToString(ftp & ftpFolder) & fileName), FtpWebRequest)
            request.Method = WebRequestMethods.Ftp.DownloadFile

            'Enter FTP Server credentials.
            request.Credentials = New NetworkCredential("apc", "manish84")
            request.UsePassive = True
            request.UseBinary = True
            request.EnableSsl = False

            'Fetch the Response and read it into a MemoryStream object.
            resp = DirectCast(request.GetResponse(), FtpWebResponse)
            stream = New StreamReader(resp.GetResponseStream())
            Try
                'Download the File.
                'resp.GetResponseStream().CopyTo(stream)
                'Response.AddHeader("content-disposition", "attachment;filename=" & fileName)
                'Response.Cache.SetCacheability(HttpCacheability.NoCache)
                'Response.BinaryWrite(stream.ToArray())
                'Response.End()
                stream.ReadLine()
                stream.ReadLine()
                stream.ReadLine()
                stream.ReadLine()
                stream.ReadLine()
                stream.ReadLine()
                stream.ReadLine()
                stream.ReadLine()
                Dim mydata = stream.ReadLine()
                stream.Close()
                stream.Dispose()
                resp.Close()
                resp.Dispose()

                request.Abort()
                mydata = Right(mydata.Replace(" ", ""), 6)
                Dim t = 0
                t = Int32.Parse(mydata)
                Return t
            Catch ex As Exception
                stream.Close()
                stream.Dispose()
                resp.Close()
                resp.Dispose()

                request.Abort()
                Return "Error" & ex.ToString  ' Throw New Exception(TryCast(ex.Response, FtpWebResponse).StatusDescription)
            End Try
        Catch ex As Exception
            stream.Close()
            stream.Dispose()
            '    resp.Close()
            ' resp.Dispose()
            request.Abort()
            Return ex.ToString
        End Try
    End Function

End Class
