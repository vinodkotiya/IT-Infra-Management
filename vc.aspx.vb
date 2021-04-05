Imports dbOp
Imports Common

Partial Class _vc
    Inherits System.Web.UI.Page

    Private Sub _Default_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack Then
                If Cache("menu3") Is Nothing Then
                    Cache.Insert("menu3", makemenu(3), Nothing, DateTime.Now.AddHours(12.0), TimeSpan.Zero)
                End If
                divMenu.InnerHtml = Cache("menu3").ToString
                If Session("Sendvcsms") Is Nothing Then Session("Sendvcsms") = True
                Session("Sendvcsms") = True
                '' executeDB("update hits set view = view+1 where page = 'LARR'")
                txtSchStart.Attributes.Add("readonly", "readonly")
                Dim mydt = getDBTable("select id, strftime('%d.%m.%Y %H:%M', sch_start) as 'Meeting Schedule', location, meetingname as 'Display Name', dept as 'dept', detail from vcon where del = 0 and sch_start >= current_date order by sch_start asc")
                gvUpcoming.DataSource = mydt
                gvUpcoming.DataBind()

                If Cache("meetingno") Is Nothing Then
                    Dim q = "select count(id) from cdr_summary where ( DISPLAY_NAME not like '%test%' and DISPLAY_NAME not like '%default%' and DISPLAY_NAME not like '%Support%' and DISPLAY_NAME not like '1%' and DISPLAY_NAME not like '0%' and DISPLAY_NAME not like '2%' and DISPLAY_NAME not like '3%' and DISPLAY_NAME not like '4%' and DISPLAY_NAME not like '5%' and DISPLAY_NAME not like '6%' and DISPLAY_NAME not like '7%' and DISPLAY_NAME not like '8%' and DISPLAY_NAME not like '9%'  ) and not (hour = 0 and minute < 20) and not hour > 8 "
                    Dim meetingno = getDBsingle(q, "mcudb")
                    Cache.Insert("meetingno", meetingno, Nothing, DateTime.Now.AddHours(1.0), TimeSpan.Zero)
                End If
                divVCCount.InnerHtml = Cache("meetingno").ToString


                executeDB("update hits set view = view+1 where page = 'ccit'")
                '' executeDB("update hits set view = view+1 where page = 'LARR'")

                divFoot.InnerHtml = "<span class='brand'>IT-Corporate Center, SCOPE</span> &copy; <span id='copyright-year'></span> | HitCount: " & getDBsingle("select view from hits where page='ccit'") & " | Visitors Online: " & Application("OnlineUsers").ToString & " "
                LoadRecordings()
            End If
            gvVCContact.DataSource = getDBTable("select oid, project, name, email, mobile from vccontact where 1 order by project")
            gvVCContact.DataBind()
            '' do postback stuff here

            'createThumbnailOnetime()

        Catch e1 As Exception
            Response.Write("<div id='bottomline'>" & e1.Message & "</div>")
        End Try
    End Sub
    Sub createThumbnailOnetime()

        Dim mydt = getDBTable("select filename from upload where hide <> 1 order by up_dt desc")
        For Each r In mydt.Rows
            Dim ffMpeg = New NReco.VideoConverter.FFMpegConverter()
            ffMpeg.GetVideoThumbnail(Server.MapPath("./upload/vcr/") + r(0), Server.MapPath("./upload/vcr/") + r(0) & ".jpg", Rnd(7000)) 'Left(r(0), 8)
        Next

    End Sub
    Sub LoadRecordings()
        Dim strend = "<div Class='item'>" &
                "<div Class='cau_left'>" &
                "<div Class='clip'> <div class='film'>" &
                 "</div></div>	Safety Review(17.08.16) <a href='/ccit/upload/vcr/safety170816.mp4' target='_blank'>File1</a> " &
                 "| <a href='/ccit/upload/vcr/safety180816.mp4' target='_blank' >File2</a> | <a href='/ccit/upload/vcr/safety190816.mp4' target='_blank' >File3</a></div>"
        Dim str = ""

        Dim mydt = getDBTable("select filename, subject, uid from upload where hide <> 1 order by up_dt desc")
        For Each r In mydt.Rows
            str = str & "<div class='item'><div class='au_left'><div class='clip'> <div class='film'>" &
                "<a href='vctube.aspx?v=" & r(2) & "' onclick=fileCount('" & Uri.EscapeDataString(r(2).ToString()) & "') target='_blank'>  <img src='/ccit/upload/vcr/" & r(0) & ".jpg' width=100% height=120px /></a></div></div>" & r(1) & "</div>	</div>"
        Next
        divVC.InnerHtml = "<div id='owl-demo' class='owl-carousel'>" & str & strend & "</div>"
    End Sub
    Private Sub btnCreateMeeting_Click(sender As Object, e As EventArgs) Handles btnCreateMeeting.Click
        If String.IsNullOrEmpty(txtSchStart.Text.Trim) Or String.IsNullOrEmpty(txtMeetDetail.Text) Or String.IsNullOrEmpty(txtDisplay.Text) Then
            divMsg.InnerHtml = "Enter proper details"
            Exit Sub
        End If
        Try
            Dim fmt As String = "yyyy-MM-dd HH:mm"
            Dim d1 = DateTime.ParseExact(txtSchStart.Text.Trim, fmt, Nothing)
        Catch ex As Exception
            divMsg.InnerHtml = "Date format is wrong. Choose date and Time from Calander Only."
            Exit Sub
        End Try

        'If txtSchStart.Text.Contains("2017-02-06") And ddlLocation.SelectedValue = "EOC TP Room" Then
        '    divMsg.InnerHtml = "TP Room booked for Maintenance on 06 Feb"
        '    Exit Sub
        'End If
        Dim q = ""
        Dim msg = ""
        Dim coord = ""
        Dim grp = "groupid = 6 Or groupid = 99"
        If ddlLocation.SelectedValue = "EOC TP Room" Then
            grp = grp & " Or groupid = 98"
        End If
        Dim techMobi = getDBSinglecommaSepearated("Select mobile from users where " & grp)
        If Not String.IsNullOrEmpty(txtCoordinator.Text) Then
            techMobi = techMobi & "," & txtCoordinator.Text
            coord = vbCrLf & "Meeting coordinator: " & txtCoordinator.Text
        End If
        txtMeetDetail.Text = txtMeetDetail.Text & coord
        If String.IsNullOrEmpty(lblMeetingid.Text) Then
            q = "select count(id) from cdr_summary where ( DISPLAY_NAME not like '%test%' and DISPLAY_NAME not like '%default%' and DISPLAY_NAME not like '%Support%' and DISPLAY_NAME not like '1%' and DISPLAY_NAME not like '0%' and DISPLAY_NAME not like '2%' and DISPLAY_NAME not like '3%' and DISPLAY_NAME not like '4%' and DISPLAY_NAME not like '5%' and DISPLAY_NAME not like '6%' and DISPLAY_NAME not like '7%' and DISPLAY_NAME not like '8%' and DISPLAY_NAME not like '9%'  ) and not (hour = 0 and minute < 20) and not hour > 8 "
            Dim meetingno = Int32.Parse(getDBsingle("select count(id)  from vcon where del = 0 and sch_start >= current_date")) + Int32.Parse(getDBsingle(q, "mcudb"))
            q = "insert into vcon (sch_start,location,detail, dept, meetingname) values('" & txtSchStart.Text.Trim & "', '" & ddlLocation.SelectedValue & "', '" & txtMeetDetail.Text.Replace("'", " ") & "', '" & ddlDept.SelectedValue & "', '" & txtDisplay.Text & "')"
            msg = "VC#" & meetingno.ToString & " " & txtDisplay.Text & " for " & ddlDept.SelectedItem.ToString & " at " & ddlLocation.SelectedItem.ToString & " on " & txtSchStart.Text & coord
        Else
            q = "update vcon set sch_start='" & txtSchStart.Text.Trim & "', location ='" & ddlLocation.SelectedValue & "', detail='" & txtMeetDetail.Text.Replace("'", " ") & "', dept='" & ddlDept.SelectedValue & "', meetingname='" & txtDisplay.Text & "' where id = " & lblMeetingid.Text
            Session("Sendvcsms") = False
        End If
        Dim ret2 = executeDB(q)
        If ret2.Contains("Error") Then
            divMsg.InnerHtml = " Query not executed.. " & ret2 & q
        Else
            Dim retsms = ""
            If Session("Sendvcsms") Then
                retsms = JustSendSMS(techMobi, msg & vbCrLf & "VC Control room: 01124388409")
                Session("Sendvcsms") = False
            Else
                divMsg.InnerHtml = "SMS already sent once/ not allowed.." & retsms & q & techMobi & msg & Session("Sendvcsms")

            End If
            If retsms = "Success" Then
                ''update sms count
                q = "update   sms set sms_count = sms_count + 3, lastvclog = '" & msg & techMobi & "SMS " & retsms & "' where sms_dt = current_date"
                Dim ret1 = executeDB(q)
                If ret1.Contains("Error") Then
                    divMsg.InnerHtml = "Unable to update sms count today" & ret1 & q
                    Exit Sub
                End If
            End If
            Response.Redirect("vc.aspx?sms=" & retsms)
        End If
    End Sub

    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        If Not String.IsNullOrEmpty(txtLogin.Text) And Not String.IsNullOrEmpty(txtPwd.Text) Then
            Dim ret = getDBsingle("select isadmin from users_vc where user ='" & txtLogin.Text & "' and pwd='" & txtPwd.Text & "'")
            If ret.Contains("Error") Then
                divMsg.InnerHtml = "Login incorrect"
            Else
                Session("isadmin") = ret
                Session("user") = txtLogin.Text
                'Session("eid") = Session("user")
                divMsg.InnerHtml = "Login Succesfull"
                LinkButton1.Visible = True
                pnlLogin.Visible = False
                pnlCreate.Visible = True
                If Session("user") = "engg" Or Session("user") = "enggadmin" Then
                    doTPStuff()
                End If
                If Not Session("editmeetingid") Is Nothing Then

                    loadmeeting(Session("editmeetingid"))

                End If
            End If
        End If
    End Sub
    Sub doTPStuff()
        ddlLocation.SelectedValue = "EOC TP Room"
        ddlLocation.Enabled = False
        ddlDept.SelectedValue = "Engg"
        ddlDept.Enabled = False
    End Sub
    Sub loadmeeting(ByVal meetingid As String)
        If Session("isadmin") = "0" Then
            Exit Sub
        End If
        lblMeetingid.Text = meetingid
        Dim mydt = getDBTable("select sch_start,location,detail, dept, meetingname from vcon where  id =" & meetingid)
        If mydt.Rows.Count > 0 Then
            txtSchStart.Text = mydt.Rows(0).Item(0).ToString
            ddlLocation.SelectedValue = mydt.Rows(0).Item(1).ToString
            txtMeetDetail.Text = mydt.Rows(0).Item(2).ToString
            ddlDept.SelectedValue = mydt.Rows(0).Item(3).ToString
            txtDisplay.Text = mydt.Rows(0).Item(4).ToString
            btnCreateMeeting.Text = "Update Meeting"
        End If
    End Sub
    Private Sub gvUpcoming_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvUpcoming.RowCommand
        If Session("isadmin") = "0" Then
            Exit Sub
        End If
        Dim value = e.CommandArgument.ToString()
        Session("editmeetingid") = value
        divMsg.InnerHtml = value
        ibtnNew_Click(Me, New ImageClickEventArgs(0, 0))

    End Sub

    Private Sub ibtnNew_Click(sender As Object, e As ImageClickEventArgs) Handles ibtnNew.Click

        If Session("user") Is Nothing Then
            pnlAdmin.Visible = True
            pnlLogin.Visible = True
            pnlVCHome.Visible = False
            pnlCreate.Visible = False
            LinkButton1.Visible = False
        Else
            LinkButton1.Visible = True
            pnlAdmin.Visible = True
            pnlLogin.Visible = False
            pnlVCHome.Visible = False
            pnlCreate.Visible = True
            divMsg.InnerHtml = Session("user")
            If Session("user") = "engg" Or Session("user") = "enggadmin" Then
                doTPStuff()
            End If
            If e.X = 0 Then loadmeeting(Session("editmeetingid"))
            '   divMsg.InnerHtml = "Login required"
        End If

    End Sub
    Protected Sub btnXls_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnXls.Click
        saveExcel()
    End Sub
    Sub saveExcel()
        ' Change the Header Row back to white color
        gvVCContact.PageSize = 70
        gvVCContact.DataBind()
        gvVCContact.HeaderRow.Style.Add("background-color", "#FFFFFF")

        ' This loop is used to apply stlye to cells based on particular row
        For Each gvrow As GridViewRow In gvVCContact.Rows
            gvrow.BackColor = Drawing.Color.White

            'If gvrow.Cells(4).Text = "True" Then
            '    gvrow.BackColor = Drawing.Color.Yellow
            '    'For k As Integer = 0 To gvrow.Cells.Count - 1
            '    '    gvrow.Cells(k).Style.Add("background-color", "#EFF3FB")
            '    'Next
            'End If
        Next

        Response.ClearContent()

        Response.AddHeader("content-disposition", "attachment; filename=vccontacts.xls")

        Response.ContentType = "application/excel"

        Dim sWriter As New System.IO.StringWriter()

        Dim hTextWriter As New HtmlTextWriter(sWriter)

        gvVCContact.RenderControl(hTextWriter)

        Response.Write(sWriter.ToString())

        Response.End()
        'lblMsg.Text = "Excel created"

        'GridView1.RenderControl(htw)

    End Sub
    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        Return
    End Sub

    Private Sub gvUpcoming_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvUpcoming.RowDataBound
        'If e.Row.RowType = DataControlRowType.DataRow Then
        '    e.Row.Cells(5).Text = HttpUtility.HtmlDecode(e.Row.Cells(5).Text)
        'End If
    End Sub

    Private Sub LinkButton1_Click(sender As Object, e As EventArgs) Handles LinkButton1.Click
        Session.Clear()
        Session.Abandon()
        Response.Redirect("Default.aspx")
    End Sub

    Protected Sub btnUpload_Click(sender As Object, e As EventArgs) Handles btnUpload.Click
        If Not String.IsNullOrEmpty(txtSubject.Text) And txtUploadKey.Text = "tandberg" Then
            Try
                Dim destfolder = "vcr"
                ' lblStatus.Text = "Uploading..."
                System.Threading.Thread.Sleep(2000)
                Dim temp As String = ""
                '####### Create Directory
                Dim uploadDir As String = ""

                uploadDir = "./upload/" & destfolder & "/"    'upload/cmg/govt/


                If Not System.IO.Directory.Exists(Server.MapPath(uploadDir)) Then
                    temp = "Creating Path " & uploadDir & "<br/>"
                    System.IO.Directory.CreateDirectory(Server.MapPath(uploadDir))
                End If

                '###### Upload File

                Dim fileName As String = "" 'ddlUploadType.SelectedValue
                Dim uploadFiles As HttpFileCollection = Request.Files
                For i As Integer = 0 To uploadFiles.Count - 1
                    Dim uploadFile As HttpPostedFile = uploadFiles(i)
                    fileName = System.IO.Path.GetFileName(uploadFile.FileName)
                    '' Check for Exception should only be word
                    'pdf True AND TRUE AND TRUE
                    'doc TRUE AND FALSE AND TRUE

                    If String.IsNullOrEmpty(fileName) Then
                        divMsg.InnerHtml = "Please attach a file..."
                        Exit Sub
                    End If
                    'remove spaces
                    '  fileName = Left(txtSubject.Text, 6) & Now.Day & Now.Second  'fileName.Replace(" ", "_")
                    'fileName = fileName.Replace("/", "_")
                    'fileName = fileName.Replace("\", "_")
                    fileName = strip(fileName)
                    ''remove single quotes , / , \ 
                    'fileName = fileName.Replace("'", "_")

                    If fileName.Trim().Length > 0 Then
                        'uploadFile.SaveAs(Server.MapPath("./Others/") + fileName)
                        uploadFile.SaveAs(Server.MapPath(uploadDir) + fileName)

                        temp = temp & "<img src='images/ok.png' />Successfully Uploaded: " & fileName & " <br/>"

                    End If
                Next
                Dim myquery As String = ""
                If temp.Contains("Successfully") Then
                    myquery = "insert into upload(subject, filename, ip, up_dt, hide, doctype) " &
               "values ('" & txtSubject.Text & "','" & fileName & "', '" & Request.UserHostAddress.ToString & "' , current_timestamp, 0, 'vc' )"
                    If executeDB(myquery) = "ok" Then
                        temp = temp & "Record Updated"
                        Dim ffMpeg = New NReco.VideoConverter.FFMpegConverter()
                        ffMpeg.GetVideoThumbnail(Server.MapPath(uploadDir) + fileName, Server.MapPath("./upload/vcr/") + fileName & ".jpg", Rnd(7000)) 'Left(r(0), 8)
                        Response.Redirect("vc.aspx?file=success&thumbnailcreated")
                    Else
                        temp = temp & "Error: " & myquery
                    End If
                Else
                    temp = temp & "Unable to upload. Try Again.<br/>" & fileName
                End If
                divMsg.InnerHtml = temp & "<br/>" '& myquery

            Catch e1 As Exception
                divMsg.InnerHtml = e1.Message
            End Try
        Else
            divMsg.InnerHtml = "Not allowed to upload"
        End If
    End Sub
End Class
