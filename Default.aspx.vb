Imports dbOp
Imports Common
Partial Class _Default
    Inherits System.Web.UI.Page

    Private Sub _Default_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack Then
                If Cache("menu1") Is Nothing Then
                    Cache.Insert("menu1", makemenu(1), Nothing, DateTime.Now.AddHours(12.0), TimeSpan.Zero)
                End If
                divMenu.InnerHtml = Cache("menu1").ToString
                executeDB("update hits set view = view+1 where page = 'ccit'")
                '' executeDB("update hits set view = view+1 where page = 'LARR'")

                If Cache("meetingno") Is Nothing Then
                    Dim q = "select count(id) from cdr_summary where ( DISPLAY_NAME not like '%test%' and DISPLAY_NAME not like '%default%' and DISPLAY_NAME not like '%Support%' and DISPLAY_NAME not like '1%' and DISPLAY_NAME not like '0%' and DISPLAY_NAME not like '2%' and DISPLAY_NAME not like '3%' and DISPLAY_NAME not like '4%' and DISPLAY_NAME not like '5%' and DISPLAY_NAME not like '6%' and DISPLAY_NAME not like '7%' and DISPLAY_NAME not like '8%' and DISPLAY_NAME not like '9%'  ) and not (hour = 0 and minute < 20) and not hour > 8 "
                    Dim meetingno = getDBsingle(q, "mcudb")
                    Cache.Insert("meetingno", meetingno, Nothing, DateTime.Now.AddHours(1.0), TimeSpan.Zero)
                End If
                divVcCount.InnerHtml = Cache("meetingno").ToString

                If Cache("totalComplaint") Is Nothing Then
                    Dim totalComplaint = getDBsingle("select count(id) from ocms")
                    Cache.Insert("totalComplaint", totalComplaint, Nothing, DateTime.Now.AddHours(1.0), TimeSpan.Zero)
                End If
                divOCMSCount.InnerHtml = Cache("totalComplaint").ToString
                makePopup()
                divFoot.InnerHtml = "<span class='brand'>IT-Corporate Center, SCOPE</span> &copy; <span id='copyright-year'></span> | HitCount: " & getDBsingle("select view from hits where page='ccit'") & " | Visitors Online: " & Application("OnlineUsers").ToString & " "
            End If
            '' do postback stuff here

        Catch e1 As Exception
            Response.Write("<div id='bottomline'>" & e1.Message & "</div>")
        End Try
    End Sub
    Sub makePopup()
        Dim vcids = getDBSinglecommaSepearated("select uid from upload where hide <> 1")
        Dim vcid() = vcids.Split(",")
        '  Dim randi = CInt(Math.Ceiling(Rnd() * (Now.Second / 60) * vcid.Length))
        Dim randi = GetRandom(0, vcid.Length - 1)

        Dim str = ""
        Dim mydt = getDBTable("select filename, subject, uid, strftime('%d.%m.%Y', up_dt) as up_dt1, views, strftime('%d.%m.%Y', lastview) as lastview from upload where doctype = 'vc' and hide <> 1 and uid = " & vcid(randi) & " order by up_dt desc limit 1 ")
        For Each r In mydt.Rows

            str = " <h4>Video Library</h4><div class='banners' style='padding-top:0;'> " &
                                    " <a href='vctube.aspx?v=" & r(2).ToString & "' onclick=fileCount('" & Uri.EscapeDataString(r(2).ToString()) & "') target='_self'> " &
                                         " <img src='/ccit/upload/vcr/" & r(0) & ".jpg' style='height:200px; width:90%;' /> " &
                                    " </a> " &
                                " </div> " &
                                " <div class='wrapper'> " &
                                    " <h5 class='vid-name'> " & " <a  href='vctube.aspx?v=" & r(2).ToString & "' onclick=fileCount('" & Uri.EscapeDataString(r(2).ToString()) & "') target='_self'> " & r(1) & " </a> " & " </h5> " &
                                    " <div class='info'> " &
                                        " <h6> By  <a href='#'> NTPC </a> " & " </h6> " &
                                        " <span>  <i class='fa fa-calendar'> " & " </i> " & r(3) & "</span> " &
                                  " &nbsp;&nbsp;&nbsp;&nbsp;<span>  <i class='fa fa-eye'>  </i>  " & r(4) & "</span> " &
                                " &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span>  Last View:  " & r(5) & "</span> " &
                                    " </div> <progress value='0' max='10' id='progressBar'></progress>" &
                                " </div> "
        Next
        divPop.InnerHtml = str
    End Sub
End Class
