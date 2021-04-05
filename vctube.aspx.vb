Imports dbOp
Imports Common

Partial Class _vctube
    Inherits System.Web.UI.Page

    Private Sub _Default_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack Then
                If Cache("menu3") Is Nothing Then
                    Cache.Insert("menu3", makemenu(3), Nothing, DateTime.Now.AddHours(12.0), TimeSpan.Zero)
                End If



                executeDB("update hits set view = view+1 where page = 'ccit'")
                '' executeDB("update hits set view = view+1 where page = 'LARR'")

                divFoot.InnerHtml = "<span class='brand'>IT-Corporate Center, SCOPE</span> &copy; <span id='copyright-year'></span> | HitCount: " & getDBsingle("select view from hits where page='ccit'") & " | Visitors Online: " & Application("OnlineUsers").ToString
                Dim v = Request.Params("v")
                If Request.Params("v") Is Nothing Then v = getDBsingle("select max(uid) from upload where hide = 0 limit 1")
                LoadRecordings(v)
                lbPkgID.Text = v '& Session("user").ToString
                divComments.InnerHtml = loadComments(lbPkgID.Text, Request.UserHostAddress, IIf(Session("user") = "9383", True, False), "vindb")

            End If

            '' do postback stuff here

            'createThumbnailOnetime()

        Catch e1 As Exception
            Response.Write("<div id='bottomline'>" & e1.Message & "</div>")
        End Try
    End Sub

    Sub LoadRecordings(ByVal v As String)

        Dim strend = "<div Class='item'>" &
                "<div Class='cau_left'>" &
                "<div Class='clip'> <div class='film'>" &
                 "</div></div>	Safety Review(17.08.16) <a href='/ccit/upload/vcr/safety170816.mp4' target='_blank'>File1</a> " &
                 "| <a href='/ccit/upload/vcr/safety180816.mp4' target='_blank' >File2</a> | <a href='/ccit/upload/vcr/safety190816.mp4' target='_blank' >File3</a></div>"
        Dim str = ""
        Dim mydt = getDBTable("select filename, subject, uid, strftime('%d.%m.%Y', up_dt) as up_dt, views, strftime('%d.%m.%Y', lastview) as lastview from upload where doctype = 'vc' and hide <> 1 and uid = " & v & " limit 1 ")
        For Each r In mydt.Rows
            divVideo.InnerHtml = "<video width='100%' height='400' controls  onclick=fileCount('" & v & "') > " &
                            "<source src='upload/vcr/" & r(0) & "' type='video/mp4'>" &
                        "</video>"
            divDownload.InnerHtml = "<a href='upload/vcr/" & r(0) & "' download  onclick=fileCount('" & v & "') > <i class='fa fa-download'>  </i> <span> Download </span> </a>"
            divView.InnerHtml = r(4)
            str = "<h1 Class='vid-name'> " & r(1) & " </h1> " &
                        " <div class='info'> " &
                            " <h5> By <a href='#'> NTPC </a>  </h5> " &
                            " <span>  <i class='fa fa-calendar'> " & " </i> " & r(3) & "</span> " &
                                        " <span>  <i class='fa fa-eye'>  </i>  " & r(4) & "</span> " &
                                           " <span>  Last View:  " & r(5) & "</span> " &
                        " </div> " &
                        " <p> Catch the exclusive video recorded during a live Video Conferencing Broadcast.. </p> " &
                        " <div class='tags'> " &
                            "<a href='#'>PMI</a> " &
                            "<a href='#'>NTPC</a> " &
                            "<a href='#'>Training</a> " &
                            "<a href='#'>Broadcast</a> " &
                            "<a href='#'>New</a> " &
                        " </div> "
        Next
        divDetail.InnerHtml = str
        str = ""
        mydt = getDBTable("select filename, subject, uid, strftime('%d.%m.%Y', up_dt) as up_dt, views, strftime('%d.%m.%Y', lastview) as lastview from upload where doctype = 'vc' and hide <> 1 and not uid = " & v & " order by views desc limit 4")
        For Each r In mydt.Rows

            str = str & " <div class='post wrap-vid'> " &
                                " <div class='zoom-container'> " &
                                    " <a href='vctube.aspx?v=" & r(2).ToString & "' onclick=fileCount('" & Uri.EscapeDataString(r(2).ToString()) & "') target='_self'> " &
                                        " <span class='zoom-caption'> " &
                                            " <i class='icon-play fa fa-play'> " & " </i> " &
                                        " </span> " &
                                        " <img src='/ccit/upload/vcr/" & r(0) & ".jpg' /> " &
                                    " </a> " &
                                " </div> " &
                                " <div class='wrapper'> " &
                                    " <h5 class='vid-name'> " & " <a href='#'> " & Left(r(1), 14) & ".. </a> " & " </h5> " &
                                    " <div class='info'> " &
                                        " <h6> By  <a href='#'> NTPC </a> " & " </h6> " &
                                        " <span>  <i class='fa fa-calendar'> " & " </i> " & r(3) & "</span> " &
                                        " <span>  <i class='fa fa-eye'>  </i>  " & r(4) & "</span> " &
                                           " <span>  Last View:  " & r(5) & "</span> " &
                                    " </div> " &
                                " </div> " &
                            " </div> "
        Next
        divPopular.InnerHtml = str
        str = ""
        mydt = getDBTable("select filename, subject, uid, strftime('%d.%m.%Y', up_dt) as up_dt1, views, strftime('%d.%m.%Y', lastview) as lastview from upload where doctype = 'vc' and hide <> 1 and not uid = " & v & " order by up_dt desc limit 4")
        For Each r In mydt.Rows
            'str = str & "<div class='item'><div class='au_left'><div class='clip'> <div class='film'>" &
            '    "<a href='/ccit/upload/vcr/" & r(0) & "' onclick=fileCount('" & Uri.EscapeDataString(r(0).ToString()) & "') target='_blank'>  <img src='/ccit/upload/vcr/" & r(0) & ".jpg' width=100% height=120px /></a></div></div>" & r(1) & "</div>	</div>"

            str = str & " <div class='post wrap-vid'> " &
                                " <div class='zoom-container'> " &
                                    " <a href='vctube.aspx?v=" & r(2).ToString & "' onclick=fileCount('" & Uri.EscapeDataString(r(2).ToString()) & "') target='_self'> " &
                                        " <span class='zoom-caption'> " &
                                            " <i class='icon-play fa fa-play'> " & " </i> " &
                                        " </span> " &
                                        " <img src='/ccit/upload/vcr/" & r(0) & ".jpg' /> " &
                                    " </a> " &
                                " </div> " &
                                " <div class='wrapper'> " &
                                    " <h5 class='vid-name'> " & " <a href='#'> " & Left(r(1), 14) & ".. </a> " & " </h5> " &
                                    " <div class='info'> " &
                                        " <h6> By  <a href='#'> NTPC </a> " & " </h6> " &
                                        " <span>  <i class='fa fa-calendar'> " & " </i> " & r(3) & "</span> " &
                                        " <span>  <i class='fa fa-eye'>  </i>  " & r(4) & "</span> " &
                                           " <span>  Last View:  " & r(5) & "</span> " &
                                    " </div> " &
                                " </div> " &
                            " </div> "
        Next
        divLatest.InnerHtml = str
        mydt = getDBTable("select filename, subject, uid, strftime('%d.%m.%Y', up_dt) as up_dt, views, strftime('%d.%m.%Y', lastview) as lastview from upload where doctype = 'vc' and hide <> 1 and not uid = " & v & " order by uid desc ")
        str = ""
        For Each r In mydt.Rows
            str = str & " <div class='item wrap-vid'> " &
                                    " <div class='zoom-container'> " &
                                        " <a href='vctube.aspx?v=" & r(2).ToString & "' onclick=fileCount('" & Uri.EscapeDataString(r(2).ToString()) & "') target='_self'> " &
                                            " <span class='zoom-caption'> " &
                                                " <i class='icon-play fa fa-play'> " & " </i> " &
                                            " </span> " &
                                              " <img src='/ccit/upload/vcr/" & r(0) & ".jpg' /> " &
                                        " </a> " &
                                    " </div> " &
                                    " <h4 class='vid-name'>  <a href='vctube.aspx?v=" & r(2).ToString & "' onclick=fileCount('" & Uri.EscapeDataString(r(2).ToString()) & "') target='_self'>" & r(1) & "</a>  </h4> <br/>" &
                                    " <div class='info'> " &
                                        " <h6> By  <a href='#'> NTPC </a> " & " </h6> " &
                                        " <span>  <i class='fa fa-calendar'> " & " </i> " & r(3) & "</span> " &
                                        " <span>  <i class='fa fa-eye'>  </i>  " & r(4) & "</span> " &
                                           " <span>  Last View:  " & r(5) & "</span> " &
                                    " </div> " &
                                " </div> "
        Next
        divAll.InnerHtml = "<h2>All Videos (" & mydt.Rows.Count & ")</h2>"
        divMore.InnerHtml = "<div id='owl-demo-1' class='owl-carousel'  >" & str & "</div>"
    End Sub
    Protected Sub btnSubmitComment_Click(sender As Object, e As EventArgs)
        '    lbMsgComment.Text = "Clicked"
        If String.IsNullOrEmpty(txtComment.Value) = True Or txtComment.Value.Contains("Please enter your") Then
            lbMsgComment.Text = "Enter something in the box above."
            Exit Sub
        End If
        Dim d As New Common
        Dim ret = getDBsingle("select name from ntpcemp where eid = '" & txtCommentEMail.Value & "'")
        If ret.Contains("Error") Then
            lbMsgComment.Text = "Your emp id is not valid"
            Exit Sub
        End If
        '' check spam mail
        '' 
        Dim name = ret
        Dim email = txtCommentEMail.Value.Replace("'", "")
        Dim totalcommentsbythisemail As String = getDBsingle("select count(name) from comments where name = '" & name & "'")
        If Not totalcommentsbythisemail.Contains("Error") Then
            Dim i = 0
            Int32.TryParse(totalcommentsbythisemail, i)
            If i > 300 Then
                lbMsgComment.Text = "You are spamming this site. Informed admin"
                Exit Sub
            Else
                lbMsgComment.Text = "valid name"
            End If
        End If
        Dim pic = email
        'If name.Contains("kotiya") Then

        '    name = "Admin"
        'End If
        Dim comment = txtComment.Value.Replace("'", " ")
        ''' Hack get name and image from comments
        ''' 


        Dim rating = txtRate.Text.Replace(",", "")
        Dim replyid = txtReplyID.Text.Replace(",", "") '0 '' default no threads
        If String.IsNullOrEmpty(replyid) Then replyid = 0
        If String.IsNullOrEmpty(rating) Then rating = 5
        Dim q = "insert into comments (pkgid,comment,name,email,rating,likes,replyid,last_updated,lastupdateby,del,pic) values (" & lbPkgID.Text & ",'" & comment & "','" & name & "','" & email & "'," & rating & ",1," & replyid & ",current_timestamp,'" & Request.UserHostAddress & "',0,'" & pic & "')"

        ret = executeDB(q)
        If ret.Contains("Error") Then
            lbMsgComment.Text = "Unable to submit." & q
            Exit Sub
        End If
        lbMsgComment.Text = "Thanks Your Comment (" & rating & ") submitted. "
        txtComment.Value = ""
        txtReplyID.Text = ""
        divComments.InnerHtml = loadComments(lbPkgID.Text, Request.UserHostAddress, IIf(Session("eid") = "9383", True, False), "vindb")
        Page.MaintainScrollPositionOnPostBack = True
        'txtCommentEMail.Text = ""
    End Sub
End Class
