Imports dbOp
Imports Common
Imports System.Net
Imports System.IO
'select sum(TotalAmt), count(SN), sum(TotalAmt)/count(SN) as ave from travel where `TripType` = 'D' and activity = 'O' and AirArrival < 4 and TripStartDt between '2008-04-01' and '2009-03-30'
Partial Class _vcreport
    Inherits System.Web.UI.Page
    Private totalsites As Integer = 0
    Private totalduration As Integer = 0
    Private dayduration As Integer = 0
    Private prevdayofmeeting As String = ""
    Private mcuip As String = ""
    Private tokenid As String = ""

    Private Sub _Default_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack Then
                If Cache("menu4") Is Nothing Then
                    Cache.Insert("menu4", makemenu(4), Nothing, DateTime.Now.AddHours(12.0), TimeSpan.Zero)
                End If
                divMenu.InnerHtml = Cache("menu4").ToString

                '' executeDB("update hits set view = view+1 where page = 'LARR'")
                RadioButtonList1_SelectedIndexChanged(vbNull, EventArgs.Empty)
                executeDB("update hits set view = view+1 where page = 'mcureport'")
                divFoot.InnerHtml = "<span class='brand'>IT-Corporate Center, SCOPE</span> &copy; <span id='copyright-year'></span> | HitCount: " & getDBsingle("select view from hits where page='mcureport'") & " | Visitors Online: " & Application("OnlineUsers").ToString & " "
                GridView1.DataSource = getDBTable("select uid, equip as Equipment, strftime('%d.%m.%Y', mdate) as 'Maintenance Date', remark from maintenance where 1 order by mdate desc, uid")
                GridView1.DataBind()
                divFetchlog.InnerHtml = "Report Viewed " & getDBsingle("select view from hits where page='mcureport'") & " Times.."
                Session("forcedupdate") = False
                If Request.Params("f") = "1" Then
                    Session("forcedupdate") = True
                    updatereport()
                ElseIf Not Request.Params("f") Is Nothing Then
                    'select min(id) from cdr_summary where id >=1400 and start_time > DATETIME('now', '-3 day') and file_version = 1
                    divFetchlog.InnerHtml = "Deletion triggered. for active mcu meeting id's only. Restriction up to last 4 days."
                    Dim mcuid = dbOp.getDBsingle("select mcuid from mcu where active = 1 limit 1", "mcudb")
                    Dim meetingidfordeletion = dbOp.getDBsingle("select min(id) from cdr_summary where id >=" & Request.Params("f") & " and DATE(start_time) >= DATE('now', '-7 day') and file_version = " & mcuid, "mcudb")
                    divFetchlog.InnerHtml = divFetchlog.InnerHtml & "<br/> Starting deletion from meeting " & meetingidfordeletion & " onwards in CDR summary and full."
                    If Not meetingidfordeletion.Contains("Error") And Not String.IsNullOrEmpty(meetingidfordeletion) Then

                        divFetchlog.InnerHtml = divFetchlog.InnerHtml & "<br/> Going for Final Deletion from CDR_Summary " & dbOp.executeDB("delete from cdr_summary where file_version = " & mcuid & " and id >= " & meetingidfordeletion, "mcudb")
                        divFetchlog.InnerHtml = divFetchlog.InnerHtml & "<br/> Going for Final Deletion from CDR_full " & dbOp.executeDB("delete from cdr_full where mcuid = " & mcuid & " and meetingid >= " & meetingidfordeletion, "mcudb")
                    End If
                End If
                '' do postback stuff here

            End If
        Catch e1 As Exception
            Response.Write("<div id='bottomline'>" & e1.Message & "</div>")
        End Try
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Dim st_dt = DateTime.ParseExact(dt_stTextBox.Text, "dd.MM.yyyy", Nothing)
        Dim end_dt = DateTime.ParseExact(dt_endTextBox.Text, "dd.MM.yyyy", Nothing)
        loadReport(st_dt, end_dt.AddHours(23))

    End Sub



    Private Sub LinkButton1_Click(sender As Object, e As EventArgs) Handles LinkButton1.Click
        If gvDPR.Rows.Count > 0 Then

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
        gvDPR.HeaderRow.Style.Add("background-color", "#FFFFFF")

        ' This loop is used to apply stlye to cells based on particular row
        For Each gvrow As GridViewRow In gvDPR.Rows
            gvrow.BackColor = Drawing.Color.White

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
        gvDPR.RenderControl(hTextWriter)
        hTextWriter.WriteLine("Maintenance Log:")
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
            chkShowSite.Checked = True
            Dim prevDay As Date = New Date(2016, 1, 1)
            dt_stTextBox.Text = prevDay.ToString("dd.MM.yyyy")
            dt_endTextBox.Text = Now.ToString("dd.MM.yyyy")
            loadReport(prevDay, Now)
        End If
    End Sub
    Private Sub gvDPR_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvDPR.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim meetingid = e.Row.Cells(5).Text
            ''Total site calculation
            Dim currentsite = getDBsingle("select count(distinct site) from cdr_full where meetingid=" & meetingid, "mcudb")
            If Not chkShowSite.Checked Then
                e.Row.Cells(5).Text = "(" & currentsite & ") " & getDBSinglecommaSepearated("select distinct site from cdr_full where meetingid=" & meetingid, "mcudb")
                e.Row.Cells(3).Text = e.Row.Cells(3).Text & "(" & meetingid & ")"
            Else
                e.Row.Cells(5).Text = currentsite
            End If
            totalsites = totalsites + currentsite
            '''Group by Day
            Dim hours = Int32.Parse(e.Row.Cells(4).Text.Split(":")(0))
            Dim minutes = Int32.Parse(e.Row.Cells(4).Text.Split(":")(1))
            Dim ts = New TimeSpan(hours, minutes, 0)
            totalduration = totalduration + hours * 60 + minutes

            Dim currentday = e.Row.Cells(0).Text
            If prevdayofmeeting = e.Row.Cells(0).Text Then
                ' e.Row.Cells(0).Text = ""
                dayduration = dayduration + hours * 60 + minutes

                '  e.Row.Cells(1).Text = totalhour & ":" & totalmin & "Hr / 09:00 Hr"
            Else
                dayduration = hours * 60 + minutes
                '  e.Row.Cells(0).BackColor = Drawing.Color.LightGreen
                '  e.Row.Cells(1).Text = ""
            End If
            If e.Row.Cells(1).Text = "V" Then
                Dim totalhour As Integer = Math.Floor(dayduration / 60)
                Dim totalmin = dayduration Mod 60
                Dim perc As Integer = dayduration / (9 * 60) * 100
                e.Row.Cells(1).Text = totalhour.ToString.PadLeft(2, "0") & ":" & totalmin.ToString.PadLeft(2, "0") & " Hr / 09:00 Hr = " & perc & "%"
                e.Row.Cells(0).BackColor = Drawing.Color.LightGreen
                e.Row.Cells(1).BackColor = Drawing.Color.LightGreen
            End If

            prevdayofmeeting = currentday
        End If
    End Sub
    Function loadReport(ByVal startdate As Date, ByVal enddate As Date) As Boolean
        Dim q = ""
        Dim testquery = ""
        If chkShowTest.Checked Then
            testquery = " ( DISPLAY_NAME not like '%test%' and DISPLAY_NAME not like '%default%' and DISPLAY_NAME not like '%Support%' and DISPLAY_NAME not like '1%' and DISPLAY_NAME not like '0%' and DISPLAY_NAME not like '2%' and DISPLAY_NAME not like '3%' and DISPLAY_NAME not like '4%' and DISPLAY_NAME not like '5%' and DISPLAY_NAME not like '6%' and DISPLAY_NAME not like '7%' and DISPLAY_NAME not like '8%' and DISPLAY_NAME not like '9%'  ) and   "
        End If
        Try
            q = "select strftime('%d.%m.%Y', START_TIME) as 'Date','' as 'VC Session/Day Business Hour(HH:MM)',cast(strftime('%H:%M', START_TIME) as text) as 'Start Time' ,  DISPLAY_NAME as 'Description', substr('00' || cast(HOUR as text),-2,2) || ':' || substr('00' ||cast(MINUTE as text),-2,2) as 'Meeting Duration(HH:mm)', id as 'Sites(No)', info2 as Persons from cdr_summary where " & testquery & " start_time between '" & startdate.ToString("yyyy-MM-dd HH:mm:ss") & "' and '" & enddate.ToString("yyyy-MM-dd HH:mm:ss") & "' and not (hour = 0 and minute < 20) and not hour > 38   order by start_time desc "
            Dim mydt = getDBTable(q, "mcudb")
            Dim datechangedrowindex As New List(Of Integer)
            Dim prevdayofmeet = ""
            Dim i = 0
            For Each r In mydt.Rows
                If Not prevdayofmeet = r(0) Then
                    datechangedrowindex.Add(i)
                End If
                prevdayofmeet = r(0)
                i = i + 1
                '  If r("Sites(No)").ToString.Contains("0") Then r("Sites(No)") = 5
            Next
            For Each ind In datechangedrowindex
                If Not ind = 0 Then mydt.Rows(ind - 1).Item(1) = "V"
            Next
            If mydt.Rows.Count <> 0 Then mydt.Rows(mydt.Rows.Count - 1).Item(1) = "V"
            gvDPR.DataSource = mydt
            gvDPR.DataBind()
            ' Dim totalhour As Integer = dt.AsEnumerable().Sum(Function(row) row.Field(Of Integer)("Salary"))
            Dim totalhour As Integer = Math.Floor(totalduration / 60)
            Dim totalmin = totalduration Mod 60
            Dim totaldays = GetNumberOfWorkingDays(startdate, enddate)
            '' never totaldays 0
            If totaldays = 0 Then totaldays = 1
            Dim percUtilization As Integer = totalduration / (totaldays * 9 * 60) * 100
            ''total till date

            q = "select count(id) from cdr_summary where ( DISPLAY_NAME not like '%test%' and DISPLAY_NAME not like '%default%' and DISPLAY_NAME not like '%Support%' and DISPLAY_NAME not like '1%' and DISPLAY_NAME not like '0%' and DISPLAY_NAME not like '2%' and DISPLAY_NAME not like '3%' and DISPLAY_NAME not like '4%' and DISPLAY_NAME not like '5%' and DISPLAY_NAME not like '6%' and DISPLAY_NAME not like '7%' and DISPLAY_NAME not like '8%' and DISPLAY_NAME not like '9%'  ) and not (hour = 0 and minute < 20) and not hour > 8 "
            Dim totalvcTilldate = getDBsingle(q, "mcudb")
            q = " select round(sum(hour + (minute * 1.0/60))) from cdr_summary where ( DISPLAY_NAME not like '%test%' and DISPLAY_NAME not like '%default%' and DISPLAY_NAME not like '%Support%' and DISPLAY_NAME not like '1%' and DISPLAY_NAME not like '0%' and DISPLAY_NAME not like '2%' and DISPLAY_NAME not like '3%' and DISPLAY_NAME not like '4%' and DISPLAY_NAME not like '5%' and DISPLAY_NAME not like '6%' and DISPLAY_NAME not like '7%' and DISPLAY_NAME not like '8%' and DISPLAY_NAME not like '9%'  ) and not (hour = 0 and minute < 20) and not hour > 8 "
            Dim totalvcHRTilldate = getDBsingle(q, "mcudb")
            divSummary.InnerHtml = "<table border='1px' Class=EU_DataTable><thead><th colspan=8><b>Summary (" & dt_stTextBox.Text & "-" & dt_endTextBox.Text & ")</b></th></tthead><tr><td>Total Session till date</td><td>Total Duration till date</td><td>Total Session for Period</td><td>Total VC Duration</td><td>Total Business Hour</td><td>% Utilization for selected period</td><td>Total Sites</td></tr>" &
                "<tr><td>" & totalvcTilldate & "</td><td>" & totalvcHRTilldate & " Hr</td><td>" & mydt.Rows.Count & "</td><td>" & totalhour & ":" & totalmin & "Hr</td><td>" & totaldays * 9 & ":00 Hr</td><td>" & percUtilization & "% </td><td>" & totalsites & "</td></tr></table> <hr/> "
            ' divMsg.InnerHtml = q
        Catch ex As Exception
            divMsg.InnerHtml = ex.Message & q
        End Try

    End Function
    Function GetNumberOfWorkingDays(startdt As DateTime, stopdt As DateTime) As Integer
        Dim vindays = 0
        'If startdt < stopdt Then stopdt = stopdt.AddDays(-1)
        ' divMsg.InnerHtml = startdt.ToString & " " & stopdt.ToString
        While startdt <= stopdt
            If startdt.DayOfWeek <> DayOfWeek.Sunday Then
                vindays += 1
            End If
            ' divMsg.InnerHtml = divMsg.InnerHtml & days & " " & startdt.ToString & " day-" & startdt.DayOfWeek & " | "
            startdt = startdt.AddDays(1)

        End While

        Return vindays
    End Function


    Private Sub chkShowSite_CheckedChanged(sender As Object, e As EventArgs) Handles chkShowSite.CheckedChanged
        RadioButtonList1_SelectedIndexChanged(vbNull, EventArgs.Empty)
    End Sub

    Private Sub chkShowTest_CheckedChanged(sender As Object, e As EventArgs) Handles chkShowTest.CheckedChanged
        RadioButtonList1_SelectedIndexChanged(vbNull, EventArgs.Empty)
    End Sub

    'Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
    '    btnUpdate.Enabled = False

    'End Sub
    Private Sub updatereport()
        Dim ret = ""
        Session("mcuip") = dbOp.getDBsingle("select mcuip from mcu where active = 1 limit 1", "mcudb")
        Session("mcuid") = dbOp.getDBsingle("select mcuid from mcu where active = 1 limit 1", "mcudb")
        'Check when last updated if in last 2 hr or forced then only update or skip

        Dim lastupdatehr = dbOp.getDBsingle("select (strftime('%s','now')- strftime('%s',last_fetch)) / 3600 from mcu where mcuid = " & Session("mcuid") & " limit 1", "mcudb")
        If lastupdatehr < 1 Or Session("forcedupdate") = False Then
            ' Just delete the data having ongoing onwards from last 2 days

            divFetchlog.InnerHtml = Session("mcuip") & "Frequent update restricted. Report is already updated in last 2 hour. Ongoing meeting data deletion triggered." & Now
            Exit Sub
        End If


        Session("lastmeetingid") = dbOp.getDBsingle("select max(id) from cdr_summary where file_version = " & Session("mcuid") & " limit 1", "mcudb")
        If String.IsNullOrEmpty(Session("lastmeetingid").ToString) Then Session("lastmeetingid") = "0"
        ret = vinservice.doMCULogin(Session("mcuip"))
        divFetchlog.InnerHtml = ret
        If ret.Contains("failed") Or ret.Contains("error") Then
            divFetchlog.InnerHtml = "User timed out/wrong login.." & Session("mcuip") & " - " & Now.ToString & "- " & ret
            Exit Sub
        End If
        ' Now fetch cdr_summary with ret tokenid
        '
        Session("tokenid") = ret
        divFetchlog.InnerHtml = ">>1.Login to mcu " & Session("mcuid") & " - " & Session("mcuip") & " succesfull.." & Now.ToString & "- token: " & Session("tokenid") & "  Wait 2 min to update data..<br/>"

        ''' Timer1 and Timer2 can run independatly from here mutually exclusive
        Timer1.Enabled = True
        ' Timer2.Enabled = True
    End Sub
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Try
            Dim ret = vinservice.getCDRSummary(Session("mcuip"), Session("tokenid"), Session("lastmeetingid"))
            'divMsg.InnerHtml = ret
            ''convert string id to int32 for sorting 
            Dim DC As New System.Data.DataColumn("intID", GetType(Integer))
            DC.Expression = "Convert(ID, 'System.Int32')"
            ret.Columns.Add(DC)
            Dim finalDTrows = ret.Select("intID > " & Session("lastmeetingid"))
            Dim str = ""
            For Each row In finalDTrows
                ' str = str & r(0) & r(1) & r("DISPLAY_NAME")
                Dim starttime = DateTime.Parse(row(6))
                starttime = starttime.AddMinutes(330)
                Dim q = "insert into cdr_summary (FILE_VERSION,NAME,ID,STATUS_STR,STATUS,GMT_OFFSET,START_TIME,RESERVE_START_TIME,MCU_FILE_NAME,FILE_SAVED,GMT_OFFSET_MINUTE,DISPLAY_NAME,RESERVED_AUDIO_PARTIES,RESERVED_VIDEO_PARTIES,CDR_SUMMARY_Id,CDR_SUMMARY_LS_Id,HOUR,MINUTE,SECOND) " &
                    " values( " & Session("mcuid") & ", '" & row(1) & "', '" & row(2) & "', '" & row(3) & "', '" & row(4) & "', '" & row(5) & "', '" & starttime.ToString("yyyy-MM-dd HH:mm:ss") & "', '" & row(8) &
                     "', '" & row(9) & "', '" & row(10) & "', '" & row(11) & "', '" & row(12) & "', '" & row(13) & "', '" & row(14) & "', '" & row(19) &
               "', '" & row(15) & "', '" & row(16) & "', '" & row(17) & "', '" & row(18) & "') "
                Dim res = executeDB(q, "mcudb")
                If res.Contains("Error") Then
                    str = str & " Query not executed.. " & res & q

                End If
            Next
            ''' To see the result unhide this
            'GridView1.DataSource = ret
            'GridView1.DataBind()
            divMsg.InnerHtml = str
            '' Update the timestamp for mcu
            executeDB("update mcu set last_fetch = current_timestamp  where mcuid = " & Session("mcuid"), "mcudb")
            divFetchlog.InnerHtml = divFetchlog.InnerHtml & ">>2. Summary Data Fetched Succesfully from " & Session("mcuid") & " - " & "..Fetching meeting data<br/>"
            'If ret.Contains("User not found") Then
            '    divFetchlog.InnerHtml = divFetchlog.InnerHtml & "User timed out/wrong login.."
            '    Exit Sub
            'End If
            'divFetchlog.InnerHtml = divFetchlog.InnerHtml & "Summary Data Fetched Succesfully..<br/>"
            'divMsg.InnerHtml = ret
            Timer1.Interval = 6000000
            Timer1.Enabled = False
            '' Now stop timer1 permanently and start timer2 for meeting id data
            Timer2.Enabled = True
        Catch ex As Exception
            divMsg.InnerHtml = "Error: " & ex.Message
            Timer1.Interval = 6000000
            Timer1.Enabled = False
        End Try

    End Sub

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        ' Try
        ''inserting records for status_str  <> 'ongoing'  and meetingid > lastmeeting id
        Dim lastmeetingid = dbOp.getDBsingle("select max(meetingid) from cdr_full where mcuid = " & Session("mcuid") & " limit 1", "mcudb")
        If String.IsNullOrEmpty(lastmeetingid) Then lastmeetingid = "10"  ''if there are no meetings for fresh mcu.. start from atleast 10
        Session("lastmeetingid") = lastmeetingid
        Dim ret = vinservice.getCDRMeetingDetail(Session("mcuid"), Session("mcuip"), Session("tokenid"), Session("lastmeetingid"))
        divMsg.InnerHtml = ret
            ''convert string id to int32 for sorting 

            '' Update the timestamp for mcu
            executeDB("update mcu set last_fetch = current_timestamp  where mcuid = " & Session("mcuid"), "mcudb")
            divFetchlog.InnerHtml = divFetchlog.InnerHtml & ">>3. Detail Data Fetched Succesfully " & Session("mcuid") & " - " & "..<br/>"
        Dim q = "delete from cdr_summary where file_version = " & Session("mcuid") & " and id >= (select min(id) from cdr_summary where file_version = " & Session("mcuid") & " and (strftime('%s','now')- strftime('%s',start_time)) / 3600 < 5  and status_str  = 'ongoing')"
        ret = dbOp.executeDB(q, "mcudb")
        divFetchlog.InnerHtml = divFetchlog.InnerHtml & ">>4. Ongoing meeting data deleted..<br/>" '&
        Timer2.Interval = 6000000
        Timer2.Enabled = False
        Timer3.Enabled = True

        'Catch ex As Exception
        '    divMsg.InnerHtml = "Error: " & ex.Message
        'Timer1.Interval = 6000000
        'Timer1.Enabled = False
        'Timer2.Interval = 6000000
        'Timer2.Enabled = False
        'End Try
    End Sub

    Private Sub Timer3_Tick(sender As Object, e As EventArgs) Handles Timer3.Tick
        divFetchlog.InnerHtml = divFetchlog.InnerHtml & ">>5. Delete meetings up to meetingid where participants are 0 <br/>"
        Dim meetingidfordeletion = getDBsingle("select max(meetingid) from cdr_full where mcuid = " & Session("mcuid"), "mcudb")
        '   Dim meetingidfordeletion = dbOp.getDBsingle("select min(id) from cdr_summary where id >=" & Request.Params("f") & " and DATE(start_time) >= DATE('now', '-7 day') and file_version = " & Session("mcuid"), "mcudb")
        If String.IsNullOrEmpty(meetingidfordeletion) Then meetingidfordeletion = "10"  ''if there are no meetings for fresh mcu.. start from atleast 10
        divFetchlog.InnerHtml = divFetchlog.InnerHtml & "<br/> Starting deletion from meeting " & meetingidfordeletion & " onwards in cdr full."
        If Not meetingidfordeletion.Contains("Error") And Not String.IsNullOrEmpty(meetingidfordeletion) Then

            '   divFetchlog.InnerHtml = divFetchlog.InnerHtml & "<br/> Going for Final Deletion from CDR_summary " & dbOp.executeDB("delete from cdr_summary where file_version = " & Session("mcuid") & " and id >= " & meetingidfordeletion, "mcudb")
        End If
        Timer3.Interval = 6000000
        Timer3.Enabled = False
    End Sub
End Class
