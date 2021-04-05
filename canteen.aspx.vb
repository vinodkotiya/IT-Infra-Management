Imports dbOp
Imports Common
Imports System.Net
Imports System.IO
Imports System.Net.Mail
Partial Class _canteen
    Inherits System.Web.UI.Page

    Private Sub _Default_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack Then





                If Request.Params("ctype") Is Nothing Then
                    pnlHome.Visible = True



                ElseIf Not Request.Params("ctype") = "admin" Then
                    If Session("eid") Is Nothing Then
                        pnlLogin.Visible = True

                        Exit Sub
                    Else

                        If Request.Params("ctype") = "status" Then
                            pnlComplaintBooking.Visible = False
                            pnlStatus.Visible = True

                            Exit Sub
                        End If
                        pnlComplaintBooking.Visible = True
                        loadComplaintForm(Request.Params("ctype").ToString)
                    End If
                Else
                    ''admin

                End If




            End If
            '' do postback stuff here

        Catch e1 As Exception
            Response.Write("<div id='bottomline'>" & e1.Message & "</div>")
        End Try
    End Sub

   
    Public Function loadComplaintForm(ByVal complainttype As String)

        lblName.Text = Session("ename")
        lblMobile.Text = Session("mobile")
        lblEmail.Text = Session("email")

        '' retrieve rax



    End Function
    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click

        If String.IsNullOrEmpty(txtEid.Text) Then
            divMsg.InnerHtml = "Please enter employee number."
            Exit Sub
        End If
        Dim mydt = getDBTable("select name, dept, cell2, email from ntpcemp where eid =" & txtEid.Text)
        If mydt.Rows.Count = 0 Then
            divMsg.InnerHtml = "Employee dosen't exist"
            ''write code to create sign up

            '' try to get employee data from other plant

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


    Private Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click


        '' check if same ipaddress is being used for another complaint today
        ''get day of last pending complaint for given complainttype



        Dim q = "insert into canteen_feedback (emp_no,name,feedback,rating1,rating2,rating3,rating4,status,time_stamp) values(" &
                        Session("eid") & ", '" & lblName.Text.Replace("'", " ") & "', '" & txtDescr.Text.Replace("'", " ") & "'," & txtRate1.Text & "," & txtRate2.Text & "," & txtRate3.Text & "," & txtRate4.Text & ", 0, current_timestamp)"
        '  divMsgComplaint.InnerHtml = q
        Dim ret = executeDB(q)
        If ret.Contains("Error") Then
            divMsg.InnerHtml = "Unable to submit Feedback contact: 7469" & ret & q
            Exit Sub
        End If
        Dim e_no As String = Session("eid")

        ''create message
        'ret = getDBsingle("select max(id) from ocms where eid = " & Session("eid") & " limit 1")
        'ret = getDBsingle(" select  cast(max(id) as text) || ', Waiting Queue:'|| cast(count(id)+1 as text) from ocms where status = 'Pending' and priority = " & ddlPriority.SelectedValue & " and typeid = " & ddlCtype.SelectedValue & " and id <= ( select max(id) as d  from ocms where eid = " & Session("eid") & "  order by id desc )  ")
        '   Dim msg = "CC-IT OCMS Complaint ID:" & ret & vbCrLf & " for " & ddlLocation.SelectedItem.ToString & " by " & lblName.Text & ". Plz check status at ccit.ntpc.co.in"
        Dim msgp = vbCrLf & "New suggestion has been recieved in Canteen Feedback System from ." & e_no & ", " & lblName.Text & ", " & lblMobile.Text & "</br><b>Feedback:</b> " & txtDescr.Text & "</br><b>Food Quality :</b> " & txtRate1.Text & "</br><b> Cleanliness and Hygiene:</b> " & txtRate2.Text & "</br><b>Staff Behaviour :</b> " & txtRate3.Text & "</br><b>Service :</b> " & txtRate4.Text & ".</br>To check status visit https://cc.ntpc.co.in/ccit/feedbackreport.aspx (Save as favourite)</br>" & vbCrLf & "- Team CC-IT </br></br>" & vbCrLf & vbCrLf & "Do not reply to this email."
        Dim emailret = "Email=empty"
        If Not String.IsNullOrEmpty(lblEmail.Text) Then
            emailret = "Email=" & SendEmail("helpdesk@ntpc.co.in", "kushalthareja@ntpc.co.in", "", "", "New Suggestion In Canteen Feedback System", "Dear Ma'm/Sir,</br></br> " & vbCrLf & vbCrLf & msgp, "", "", "", "")
        End If


        ''Get tech mobile number
        ' Exit Sub

        'Try


        ' SMS Module.. 
        ' enter todays date count


        Response.Redirect("thankyou.aspx")
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
            mail.CC.Add("mritunjaykumarverma@ntpc.co.in")
            mail.CC.Add("rajeshchoudhary@ntpc.co.in")
            'mail.Bcc.Add(bcc)
            mail.IsBodyHtml = True
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





   





End Class
