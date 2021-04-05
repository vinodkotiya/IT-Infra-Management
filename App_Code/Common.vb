Imports Microsoft.VisualBasic
Imports System.Net
Imports System.IO
Public Class Common
    Public Shared Function strip(ByVal str As String) As String

        Return Regex.Replace(str, "[\[\]\\\^\$\|\?\/\*\'\+\(\)\{\}%,;><!@#\-\+]", "")
    End Function
    Public Shared Function makemenu(Optional ByVal sel As Integer = 1) As String
        'http://191.254.1.42/cmg/
        Dim a1, a2, a3, a4, a5, a6, a7 As String
        If sel = 1 Then a1 = "class='current'"
        If sel = 2 Then a2 = "class='current'"
        If sel = 3 Then a3 = "class='current'"
        If sel = 4 Then a4 = "class='current'"
        If sel = 5 Then a5 = "class='current'"
        If sel = 6 Then a6 = "class='current'"
        If sel = 7 Then a7 = "class='current'"
        Dim temp = "<ul class='sf-menu'><li " & a1 & "><a href='Default.aspx'>Home</a></li> " &
                    "<li  " & a2 & "> <a  href='services.aspx'>Services</a> </li>" &
                "	<li  " & a3 & ">  <a href ='vc.aspx'>VC</a></li> " &
                "	<li  " & a4 & ">	<a href ='vcreport.aspx' > Reports</a>		</li>" &
                  "	<li  " & a6 & ">	<a href ='nw.aspx' > Network</a>		</li>" &
                    "	<li  " & a7 & ">	<a href ='#' > <img src=images/docs.png width=44px height=44px style='margin-top:-10px;' /> </a>		</li>" &
                  "  <li  " & a5 & ">   <a href='Contacts.aspx'><img src=images/contacts.png width=44px height=44px style='margin-top:-10px;'  /></a>   </li>	</ul>"

        Return temp
    End Function
    Public Shared Function JustSendSMS(ByVal mobileNumbersseperatedbycomma As String, ByVal msg As String) As String

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
    Public Shared Function loadComments(ByVal pkgID As String, ByVal ipaddress As String, ByVal admin As Boolean, ByVal db As String) As String
        ''get comments

        ''' avatars
        ''' 
        Dim avatar() As String = {"1", "2", "3", "4", "5", "6", "7"}
        Dim i As Integer = 0
        Dim onerrorAvatar = ""
        Dim pic = ""

        Dim q = "select cid, replyid, comment, name, email, rating, likes, strftime('%d.%m.%Y %H:%M', last_updated) as t, pic from comments where not del= 1 and (replyid is null or trim(replyid) ='' or replyid = 0) and pkgid = '" & pkgID & "' order by last_updated desc"
        Dim mydt = dbOp.getDBTable(q, db) '' get all main comments
        Dim strComments = ""
        Dim delComment = ""

        For Each comment In mydt.Rows
            '''thread any reply for this comment

            Dim strReplies = ""
            q = "select cid, replyid, comment, name, email, rating, likes, strftime('%d.%m.%Y %H:%M', last_updated) as t, pic from comments where not del = 1 and replyid = " & comment(0)
            Dim replydt = dbOp.getDBTable(q, db) '' get all threaded reply
            For Each reply In replydt.Rows
                ''deletecomment
                If admin Then delComment = "<li><a href='' onclick=deleteComment('" & reply(0).ToString() & "') ><i class='fa fa-times'></i></a></li>"

                ''' get on error avatar
                onerrorAvatar = "images/73x73" & avatar(i Mod 6) & ".png"
                i = i + 1
                pic = "http://10.0.236.26:8090/bday/photos/" & reply(8).ToString.PadLeft(8, "0") & ".jpg"  'http://10.0.236.26:8090/bday/photos/00009383.jpg
                If String.IsNullOrEmpty(pic) Then pic = onerrorAvatar

                Dim namer = reply(3)
                If String.IsNullOrEmpty(namer) Then namer = Regex.Replace(reply(4), "(?<=[\w]{1})[\w-\._\+%]*(?=[\w]{1}@)", Function(m) New String("*"c, m.Length)) ''copy masked email instead
                strReplies = strReplies & "<!-- start user replies --><li><!-- current #{user} avatar --><div Class='user_avatar'><img style='width:75px; height:75px' src='" & pic & "' onerror=" & Chr(34) & "this.src='" & onerrorAvatar & "';" & Chr(34) & " ></div><!-- the comment body --><div class='comment_bodyhidden'> " &
                     "<div class='replied_to'>Re: <p> <span class='user'><b>" & namer & ":</b></span>" & reply(2) & "</p></div></div>" &
"<!-- comments toolbar --><div class='comment_toolbar'>" &
    "<!-- inc. date and time --><div class='comment_details'><ul><li><a href='#comment' onclick=setReplyID('" & comment(0).ToString() & "') ><i class='fa fa-pencil'></i>Reply </a></li><li id='cid" & reply(0) & "'><a href='#anchorid" & comment(0).ToString() & "' onclick=fileCount1('" & reply(0).ToString() & "','" & ipaddress & "') ><i class='fa fa-heart love'></i>" & reply(6) & "</a></li><li><i class='fa fa-clock-o'></i> " & reply(7) & "</li>" & delComment & "</ul></div><!-- inc. share/reply and love --><div class='comment_tools'><ul></ul></div><!-- inc. date and time --></div><!-- comments toolbar --> </li><!-- end user replies --><br/ >"

                '<li><i class='fa fa-share-alt'></i></li><li><i class='fa fa-reply'></i> </li>
            Next

            Dim name = comment(3).ToString
            If String.IsNullOrEmpty(name) Then name = Regex.Replace(comment(4), "(?<=[\w]{1})[\w-\._\+%]*(?=[\w]{1}@)", Function(m) New String("*"c, m.Length)) ''copy masked email instead

            ''deletecomment
            If admin Then delComment = "<li><a href='' onclick=deleteComment('" & comment(0).ToString() & "') ><i class='fa fa-times'></i></a></li>"
            ''' get on error avatar
            onerrorAvatar = "images/73x73" & avatar(i Mod 6) & ".png"
            i = i + 1
            'pic = comment(8).ToString
            pic = "http://10.0.236.26:8090/bday/photos/" & comment(8).ToString.PadLeft(8, "0") & ".jpg"
            If String.IsNullOrEmpty(pic) Then pic = onerrorAvatar

            strComments = strComments & " <a name='anchorid" & comment(0).ToString() & "' />	 <div class='new_comment'> " &
        "<!-- build comment --> 	<ul class='user_comment'>" &
         "<!-- current #{user} avatar -->	<div class='user_avatar'><img style='width:75px; height:75px'  src='" & pic & "' onerror=" & Chr(34) & "this.src='" & onerrorAvatar & "';" & Chr(34) & " ></div>" &
"<!-- the comment body --><div class='comment_body'><p><span class='user'><b>" & name & ":</b></span>" & comment(2) & "</p></div>" &
     "<!-- comments toolbar --><div class='comment_toolbar'>" &
    "<!-- inc. date and time --><div class='comment_details'><ul><li><a href='#comment' onclick=setReplyID('" & comment(0).ToString() & "') ><i class='fa fa-pencil'></i> Reply</a></li><li><i class='fa fa-clock-o'></i> " & comment(7) & "</li><li id='cid" & comment(0) & "'><a href='#anchorid" & comment(0).ToString() & "' onclick=fileCount1('" & comment(0).ToString() & "','" & ipaddress & "') ><i class='fa fa-heart love'></i>" & comment(6) & "</a></li><li><i class='fa fa-star'></i>" & comment(5) & "</li>" & delComment & "</ul></div><!-- inc. share/reply and love --><div class='comment_tools'><ul></ul></div><!-- inc. date and time --></div><!-- comments toolbar -->" &
"<!-- reply will come here under <li> --> " &
strReplies &
            "</ul><!-- build comment --> " &
        " </div><!-- newcomment --> "

            '<li><i class='fa fa-share-alt'></i></li>
        Next

        Return strComments
    End Function
End Class
