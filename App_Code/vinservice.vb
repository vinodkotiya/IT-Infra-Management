Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Web.Script.Services
Imports System.Web.Script.Serialization
Imports System.Data.SQLite
Imports System.Data
Imports System.Net.NetworkInformation

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
<System.Web.Script.Services.ScriptService()> _
<WebService(Namespace:="http://vinodkotiya.com/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Class vinservice
    Inherits System.Web.Services.WebService



    <WebMethod()> _
     <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function HelloWorld() As String
        Return "Hello World"
    End Function
    <WebMethod(Description:="say hello with name")> _
   <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function sayhelloname(ByVal name As String) As String
        Return "from asp.net web service Hello " + name
    End Function
    <WebMethod(Description:="say hello with name1")>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function updatefileCount(ByVal filename As String) As String
        filename = Server.UrlDecode(filename)
        Dim q = "update upload set views = views+1 , lastview=current_timestamp where uid='" + filename + "'" '"
        Try
            Dim r = dbOp.getDBsingle("update upload set views = views+1 , lastview=current_timestamp where uid='" + filename + "' ")
            Return "from asp.net web service Hello " + filename + r + q
        Catch e As Exception
            Return "Error: from asp.net web service Hello " + e.Message + q
        End Try

    End Function
    <WebMethod()>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetCurrentTime(ByVal filename As String) As String
        Return "Hello " & filename & Environment.NewLine & "The Current Time is: " ' & _
        ' DateTime.Now.ToString()
    End Function
    <WebMethod(Description:="say hello with name1")>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function setLikes(ByVal fileid As String, ByVal ip As String) As String
        fileid = Server.UrlDecode(fileid)
        ip = Server.UrlDecode(ip)
        '' check if already liked from this ip
        Dim q = "update comments set likes = likes + 1, lastupdateby = '" & ip & "' where cid='" + fileid + "'" '"
        Try
            Dim r = dbOp.executeDB(q)
            Return "Like added " + fileid + r '+ q
        Catch e As Exception
            Return "Error: from asp.net web service Hello " + e.Message + q
        End Try

    End Function
    <WebMethod(Description:="say hello with delete")>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function delComment(ByVal fileid As String) As String
        fileid = Server.UrlDecode(fileid)
        '' check if already liked from this ip
        Dim q = "update comments set del = 1 where cid='" + fileid + "'" '"
        Try
            Dim r = dbOp.executeDB(q)
            Return "comment deleted  " + fileid + r '+ q
        Catch e As Exception
            Return "Error: from asp.net web service Hello " + e.Message + q
        End Try

    End Function

    <WebMethod(Description:="getUserControl")>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function GetUserControlHTML(controlpath As String, doctype As String, project As String) As String
        ' Create instance of the page control
        Try
            Dim page As New Page()
            Dim headHolder = New System.Web.UI.HtmlControls.HtmlHead
            ' Create instance of the user control
            Dim userControl As UserControl = DirectCast(page.LoadControl(controlpath), UserControl)

            'Disabled ViewState- If required
            ''  userControl.EnableViewState = False

            'Acces the control via the interface
            Dim UserCtrl As ICustomParams = TryCast(userControl, ICustomParams)
            If UserCtrl IsNot Nothing Then
                UserCtrl.docType = doctype
                UserCtrl.project = project
            End If

            'Form control is mandatory on page control to process User Controls
            Dim form As New HtmlForm()

            'Add user control to the form
            form.Controls.Add(userControl)

            'Add form to the page
            page.Controls.Add(headHolder)
            page.Controls.Add(form)

            'Write the control Html to text writer
            Dim textWriter As New System.IO.StringWriter()

            'execute page on server
            HttpContext.Current.Server.Execute(page, textWriter, False)

            ' Clean up code and return html
            Dim html As String = CleanHtml(textWriter.ToString())

            Return html
        Catch e1 As Exception
            Return "Exception inside webservice: " & e1.Message
        End Try


    End Function
    <WebMethod(Description:="getUserControl")>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function GetUserControlHTML1(controlpath As String) As String
        ' Create instance of the page control
        Try
            Dim page As New Page()
            Dim headHolder = New System.Web.UI.HtmlControls.HtmlHead
            ' Create instance of the user control
            Dim userControl As UserControl = DirectCast(page.LoadControl(controlpath), UserControl)

            'Disabled ViewState- If required
            ''  userControl.EnableViewState = False

            'Acces the control via the interface
            Dim UserCtrl As ICustomParams = TryCast(userControl, ICustomParams)


            'Form control is mandatory on page control to process User Controls
            Dim form As New HtmlForm()

            'Add user control to the form
            form.Controls.Add(userControl)

            'Add form to the page
            page.Controls.Add(headHolder)
            page.Controls.Add(form)

            'Write the control Html to text writer
            Dim textWriter As New System.IO.StringWriter()

            'execute page on server
            HttpContext.Current.Server.Execute(page, textWriter, False)

            ' Clean up code and return html
            Dim html As String = CleanHtml(textWriter.ToString())

            Return html
        Catch e1 As Exception
            Return "Exception inside webservice: " & e1.Message
        End Try


    End Function
    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function checkSession() As Boolean
        If HttpContext.Current.Session("eid") Is Nothing Then
            Return False
        Else
            Return True
        End If
        ' DateTime.Now.ToString()
    End Function
    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function setSession(ByVal type As String, ByVal pro As String) As Boolean
        HttpContext.Current.Session("type") = type
        HttpContext.Current.Session("pro") = pro
        Return True

    End Function
    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function displaySession() As String
        Return HttpContext.Current.Session("eid") & HttpContext.Current.Session("ename")

    End Function
    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function setUserSession(ByVal eid As String, ByVal name As String) As String
        Try
            If Not String.IsNullOrEmpty(eid) And Not String.IsNullOrEmpty(name) Then
                '  HttpContext.Current.Session.Clear() 
                ' System.Threading.Thread.Sleep(1000)
                HttpContext.Current.Session.Add("eid", eid)
                HttpContext.Current.Session.Add("ename", name)
                'HttpContext.Current.Session("eid") = eid
                'HttpContext.Current.Session("ename") = name
                Return "success"
            Else
                Return "Empty" & eid & name
            End If
        Catch e As Exception
            Return "Error in webservice:" + e.Message
        End Try
    End Function
    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function clearSession() As String
        Try
            HttpContext.Current.Session.Abandon()
            HttpContext.Current.Session.Clear()
            Return "success"
        Catch e As Exception
            Return "Error in webservice:" + e.Message
        End Try
    End Function

    <WebMethod(Description:="authenticateuserAjax")>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function authenticateuserAjax(ByVal name As String, ByVal pwd As String) As String
        '  Return "ID is " + name + " pwd is " + pwd
        Dim mysql = "SELECT distinct name FROM users WHERE  eid ='" & name.Trim & "' and pwd='" & pwd.Trim & "' "
        Using connection As New SQLiteConnection(System.Configuration.ConfigurationManager.ConnectionStrings("vindb").ConnectionString)
            Dim sqlComm As SQLiteCommand
            Dim sqlReader As SQLiteDataReader
            Dim dt As New DataTable()
            Dim dataTableRowCount As Integer
            Dim result = ""
            Try
                connection.Open()
                sqlComm = New SQLiteCommand(mysql, connection)
                sqlReader = sqlComm.ExecuteReader()
                dt.Load(sqlReader)
                dataTableRowCount = dt.Rows.Count
                sqlReader.Close()
                sqlComm.Dispose()
                If dataTableRowCount = 1 Then
                    result = dt.Rows(0).Item(0).ToString()
                    connection.Close()
                    Return result
                Else
                    connection.Close()
                    Return "Error: Username/Password not valid."
                End If

            Catch exp As Exception

                connection.Close()
                Return "Error inside web service: " + exp.Message

            End Try
            'Close Database connection
            'and Dispose Database objects
        End Using
    End Function
    <WebMethod(Description:="getRating")>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function GetRating() As String
        Dim sql As String = "SELECT ROUND(SUM(Rating) / COUNT(Rating),1) as Average , COUNT(Rating)as Total FROM suggestion"
        Dim mydt = dbOp.getDBTable(sql)
        Dim json As String = String.Empty
        For Each row In mydt.Rows
            json += "[ {"
            json += String.Format("Average: {0}, Total: {1}", row(0), row(1))
            'json += String.Format("Average: {0}, Total: {1}", "4.1", "19")
            json += "} ]"
            Exit For
        Next

        Return json

    End Function
    <WebMethod(Description:="GetNetwork")>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function GetNetwork() As String
        Dim sql As String = "SELECT name, n.ip, x, y, linkto, l.status, (l.avbl * 100 / l.try), big, n.status   FROM network n, networklog l where n.ip = l.ip and l.log_dt = current_date order by grp desc"
        Dim mydt = dbOp.getDBTable(sql)
        Dim json As String = String.Empty
        json += "{""GetDataResult"":["
        Dim i = 1
        For Each row In mydt.Rows
            sql = "SELECT x, y  FROM network where uid = " & row(4)
            Dim subdt = dbOp.getDBTable(sql)
            Dim nextx = "null"
            Dim nexty = "null"
            Dim mainstatus = "null"
            If subdt.Rows.Count = 1 Then
                nextx = subdt.Rows(0)(0)
                nexty = subdt.Rows(0)(1)
            End If
            json += "{"
            'json += String.Format("name: '{0}', ip: '{1}', x: '{2}', y: '{3}', linkto: '{4}'", row(0), row(1), row(2), row(3), row(4))
            json += String.Format("""name"": ""{0}"", ""ip"": ""{1}"", ""x"": {2}, ""y"": {3}, ""linkto"": {4}, ""status"": {5}, ""avbl"": {6}, ""nextx"": {7}, ""nexty"": {8}, ""big"": {9}, ""mainstatus"": {10}", row(0), row(1), row(2), row(3), row(4), row(5), row(6), nextx, nexty, row(7), row(8))
            json += "}"
            If i < mydt.Rows.Count Then json += ", "
            i = i + 1

        Next
        json += "]}"
        Return json

    End Function
    <WebMethod(Description:="getIPxy")>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function getIPxy(ByVal clickedX As String, ByVal clickedY As String) As String
        Dim ip = dbOp.getDBsingle("SELECT ip || ',' || x || ',' || y  || ',' || big  || ',' || name  FROM network where (" & clickedX & " between x and x + 200) and (" & clickedY & " between y and y + 30 *big)")
        Return ip
    End Function
    <WebMethod(Description:="pingIP")>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function pingIP(ByVal ipaddress As String) As String
        Try
            Dim ping As Ping = New Ping
            Dim options As New PingOptions()
            options.DontFragment = True
            Dim garbage As Byte()
            Dim enc As System.Text.ASCIIEncoding = New System.Text.ASCIIEncoding
            Dim data As String = "edthismoduletogetthepingresponceofvariousserverandroutersandapplicationstogettherealtimedataandtosatisfythisbloodyuserswhofrequentlyusedtocallmewheneversomethinggetsdown"
            garbage = enc.GetBytes(data + data + data)
            ' garbage is used to get responce time of more than 1 ms
            Dim pingreply As PingReply = ping.Send(ipaddress, 2048, garbage, options)
            ' one more attempt
            Dim attempt = 1
            If pingreply.Status.ToString().Contains("Timed") Then
                pingreply = ping.Send(ipaddress, 2048, garbage, options)
                attempt = 2
            End If
            Return pingreply.Status.ToString() + " " + pingreply.RoundtripTime.ToString + "ms" '+ attempt.ToString + " attempt" 'Working Fine with Responce Time of 
        Catch e As Exception
            Return "fail" + e.Message
        End Try

    End Function
    <WebMethod(Description:="UploadImage")>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function UploadImage(ByVal imageData As String) As String
        Try
            Dim path As String = HttpContext.Current.Server.MapPath("~/DPR/")
            Dim fileNameWitPath As String = path + Now.ToString("dd.MM.yy") + ".png"

        Using fs As New System.IO.FileStream(fileNameWitPath, System.IO.FileMode.Create)

            Using bw As New System.IO.BinaryWriter(fs)


                Dim data As Byte() = Convert.FromBase64String(imageData)

                bw.Write(data)

                bw.Close()

            End Using
        End Using
        Return "image recieved" '& imageData
        Catch e As Exception
            Return "fail to save image" + e.Message
        End Try
    End Function

    <WebMethod(Description:="setRating")>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function setRating() As String

        Dim sql As String = "insert into suggestion "


        Return "success"

    End Function
    <WebMethod(Description:="getControlwithData")>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function getControlwithData(doctype As String, project As String) As String
        ' Create instance of the page control
        Dim gv As New GridView()
        Dim stringWriter As New System.IO.StringWriter()
        Dim htmlWriter As New HtmlTextWriter(stringWriter)
        Dim myDT = dbOp.getDBTable("select project, date_format(last_updated,'%d.%m.%y') as reviewdate,date_format(reviewdate,'%d.%m.%y') as reviewdate1,reviewby,subject,venue,filename,remarks1,remarks2, concat(project,'\\',type,'\\',filename) as url   from upload_cmg where project like '" & project & "' and type = '" & doctype & "' order by reviewdate1 desc")
        gv.Caption = dbOp.getDBsingle("select count(project)  from upload_cmg where project like '" & project & "' and type = '" & doctype & "' order by reviewdate desc") & " records found.."
        'gv.AllowPaging = True
        'gv.PageSize = 10
        'project0, 1 reviewdate, 2 reviewdate1,3reviewby,4 subject,5 venue,6 filename,7 remarks1,8 remarks2,  9 url
        For Each row In myDT.Rows
            row(9) = getfileIcon(row(9))
        Next
        ' myDT.Columns.Remove(0) 'remove 1st column
        gv.DataSource = myDT
        gv.DataBind()
        gv.HeaderRow.TableSection = TableRowSection.TableHeader
        gv.RenderControl(htmlWriter)
        Return stringWriter.ToString()
    End Function
    Private Function getfileIcon(ByVal file As String) As String
        Dim thefile As String = "\upload\" & file
        Dim img As String = "images/" & Right(thefile, 3) & ".gif"
        Dim ret = ""
        Try ' If System.IO.File.Exists(Server.MapPath("~") + thefile) Then
            Dim fs As System.IO.FileInfo = New System.IO.FileInfo(Server.MapPath("~") + thefile)
            ret = "<img src='" & img & "' width=13 height=13 border=0 onerror=" & Chr(34) & "this.src='images/file.gif';" & Chr(34) & "  />" & Math.Round((fs.Length / (1024 * 1024)), 2).ToString()   'Left(e.Row.Cells(2).Text, 2)
        Catch e1 As Exception ' Else
            ret = Math.Round((Rnd(6) * 600)).ToString & thefile & e1.Message
        End Try 'End If
        Return ret
    End Function

    Private Function CleanHtml(html As String) As String
        Return html 'Regex.Replace(html, "]*?>", "", RegexOptions.IgnoreCase)
    End Function
    <WebMethod(Description:="authenticateuser")>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function getVCCalander(ByVal myQuery As String) As DataTable
        ''this function will return DataTable from table according to myQuery
        Using connection As New SQLiteConnection(System.Configuration.ConfigurationManager.ConnectionStrings("mcudb").ConnectionString)
            ' connection.Close()
            connection.Open()

            Dim sqlComm As SQLiteCommand
            Dim sqlReader As SQLiteDataReader
            Dim dt As New DataTable("vcdata")
            Dim dataTableRowCount As Integer
            Try
                sqlComm = New SQLiteCommand(myQuery, connection)
                sqlReader = sqlComm.ExecuteReader()
                dt.Load(sqlReader)
                dataTableRowCount = dt.Rows.Count
                sqlReader.Close()
                sqlComm.Dispose()
                If Not dt Is Nothing Then

                    connection.Close()
                    Return dt
                Else
                    connection.Close()
                    Return dt.NewRow("Too many Records Found")
                End If

            Catch e As Exception
                'lblDebug.text = e.Message
                dt.Columns.Add("Error")
                Dim tmprow = dt.NewRow
                connection.Close()
                tmprow(0) = "Error: " & e.Message & myQuery
                dt.Rows.Add(tmprow)
                ' Return dt.NewRow("Error in getdatatable")
                Return dt
            End Try
            connection.Close()
        End Using
    End Function

    <WebMethod(Description:="authenticateuser")> _
  <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function authenticateuser(ByVal name As String, ByVal pwd As String) As String
        ''  Return "ID is " + name + " pwd is " + pwd
        'Dim mysql = "SELECT distinct first_name FROM users u,groups g,group_users gu WHERE u.userid = gu.userid and g.gid = gu.gid and username='" & name.Trim & "' and password=md5('" & pwd.Trim & "') and g.name like ('%')"
        'Using connection As New MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("vinConn").ConnectionString)
        '    connection.Open()
        '    Dim result As String
        '    Dim sqlComm As MySqlCommand
        '    Dim sqlReader As MySqlDataReader
        '    Dim dt As New DataTable()
        '    Dim dataTableRowCount As Integer
        '    Try
        '        sqlComm = New MySqlCommand(mysql, connection)
        '        sqlReader = sqlComm.ExecuteReader()
        '        dt.Load(sqlReader)
        '        dataTableRowCount = dt.Rows.Count
        '        sqlReader.Close()
        '        sqlComm.Dispose()
        '        If dataTableRowCount = 1 Then
        '            result = dt.Rows(0).Item(0).ToString()
        '            connection.Close()
        '            Return result
        '        Else
        '            connection.Close()
        '            Return "Error: Username/Password not valid."
        '        End If

        '    Catch exp As Exception

        '        connection.Close()
        '        Return "Error: " + exp.Message

        '    End Try
        '    'Close Database connection
        '    'and Dispose Database objects
        'End Using
    End Function
    <WebMethod()>
    Public Function GetCompletionList(prefixText As String) As String()
        'Dim items As New List(Of String)(50)
        '''this function will return single value from table by concatenating rows with hash
        'Using connection As New MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("vinConn").ConnectionString)
        '    ' connection.Close()
        '    connection.Open()

        '    Dim sqlComm As MySqlCommand
        '    Dim sqlReader As MySqlDataReader
        '    Dim dt As New DataTable()
        '    Dim j As Integer = 0
        '    Dim myquery = "SELECT distinct concat(project,' ', type) as res FROM `upload_cmg` where project like '%" & prefixText & "%' or type like '%" & prefixText & "%'" & _
        '        " order by project, type"
        '    Try
        '        sqlComm = New MySqlCommand(myquery, connection)
        '        sqlReader = sqlComm.ExecuteReader()
        '        dt.Load(sqlReader)
        '        Dim i = dt.Rows.Count

        '        sqlReader.Close()
        '        sqlComm.Dispose()
        '        If i = 0 Then
        '            connection.Close()
        '            'items = Nothing
        '            items.Add("No Records")

        '        End If

        '        While j < i

        '            items.Add(dt.Rows(j).Item(0).ToString())

        '            j = j + 1
        '        End While

        '        connection.Close()
        '        Return items.ToArray()


        '    Catch e1 As Exception
        '        'lblDebug.text = e.Message
        '        connection.Close()
        '        items.Add(e1.Message)
        '        Return items.ToArray()
        '    End Try
        '    'connection.Close()
        'End Using
    End Function
    <WebMethod(Description:="mcuLogin")>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function doMCULogin(ByVal mcuIP As String) As String
        Try

            Dim logintext = "  <TRANS_MCU><!-- The root Is the transaction name. -->" &
        "<TRANS_COMMON_PARAMS> " &
        "<MCU_TOKEN>0</MCU_TOKEN>" &
        "<MCU_USER_TOKEN>0</MCU_USER_TOKEN>" &
        "<MESSAGE_ID>1</MESSAGE_ID>" &
        "</TRANS_COMMON_PARAMS>" &
        "<!-- The next element Is the action (Login), And below it are the action's " &
        "parameters, in this case, the Login parameters. --> " &
        "<ACTION> " &
        "<LOGIN>" &
        "<!-- The next parameters are the MCU IP And the port number. -->" &
        "<MCU_IP>" &
        "<IP>" & mcuIP & "</IP>" &
        "<LISTEN_PORT>80</LISTEN_PORT>" &
        "<HOST_NAME/>" &
        "</MCU_IP>" &
        "<!-- The user And password strings.-->" &
        "<USER_NAME>SUPPORT</USER_NAME>" &
        "<PASSWORD>SUPPORT</PASSWORD>" &
        "<STATION_NAME>EMA.F3-JUDITHS</STATION_NAME>" &
        "<COMPRESSION>false</COMPRESSION>" &
        "</LOGIN>" &
        "</ACTION>" &
        "</TRANS_MCU>"

            Dim req As System.Net.WebRequest = Nothing

            Dim rsp As System.Net.WebResponse = Nothing
            'Try
            Dim uri As String = "http://" & mcuIP '"http://10.1.253.129"
            req = System.Net.WebRequest.Create(uri)
            req.Proxy = Nothing
            req.Method = "POST"
            req.ContentType = "text/xml"
            Dim writer As New System.IO.StreamWriter(req.GetRequestStream())
            writer.WriteLine(logintext)
            writer.Close()
            rsp = req.GetResponse()
            Dim doc As XDocument = XDocument.Load(rsp.GetResponseStream())
            'txtResponse.Text = doc.ToString
            Dim str = ""
            ''''##### Trim down xml document for what is required in memory. Add root node manually
            Dim childList As IEnumerable(Of XElement) =
        From el In doc.Elements("RESPONSE_TRANS_MCU").Elements("ACTION").Elements("LOGIN").Elements("MCU_TOKEN")
        Select el
            For Each rec As XElement In childList
                str = str & Strings.Trim(rec.ToString)
            Next
            str = str.Replace("<MCU_TOKEN>", "")
            str = str.Replace("</MCU_TOKEN>", "")
            If str.Trim = "0" Then Return "Login to MCU failed..."
            rsp.Close()
            Return str
        Catch ex As Exception
            Return "Error:" & ex.Message
        End Try

    End Function
    <WebMethod(Description:="mcuCDRSUmmary")>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function getCDRSummary(ByVal mcuIP As String, ByVal tokenid As String, ByVal lastmeetingid As String) As DataTable
        Try
            Dim xmltext = "<TRANS_CDR_LIST>" &
    "<TRANS_COMMON_PARAMS> " &
    "<MCU_TOKEN>" & tokenid & "</MCU_TOKEN> " &
    "<MCU_USER_TOKEN>" & tokenid & "</MCU_USER_TOKEN>" &
   " <MESSAGE_ID>147</MESSAGE_ID>" &
    "</TRANS_COMMON_PARAMS>" &
     "<ACTION> " &
    "<GET>" &
    "<CDR_SUMMARY_LS/>" &
    "</GET>" &
    "</ACTION>" &
    "</TRANS_CDR_LIST>"

            Dim req As System.Net.WebRequest = Nothing
            Dim rsp As System.Net.WebResponse = Nothing
            'Try
            Dim uri As String = "http://" & mcuIP '"http://10.1.253.129"
            req = System.Net.WebRequest.Create(uri)
            req.Proxy = Nothing
            req.Method = "POST"
            req.ContentType = "text/xml"
            Dim writer As New System.IO.StreamWriter(req.GetRequestStream())
            writer.WriteLine(xmltext)
            writer.Close()
            rsp = req.GetResponse()
            Dim doc As XDocument = XDocument.Load(rsp.GetResponseStream())
            ' Return doc.ToString & "<br>" & xmltext

            Dim str = "<root>"
            ''''##### Trim down xml document for what is required in memory. Add root node manually
            Dim childList As IEnumerable(Of XElement) =
        From el In doc.Elements("RESPONSE_TRANS_CDR_LIST").Elements("ACTION").Elements("GET").Elements("CDR_SUMMARY_LS")
        Select el
            For Each rec As XElement In childList
                str = str & Strings.Trim(rec.ToString)
            Next
            str = str & "</root>"
            '''#### Read string to load in datatable as xml from memory
          '  Return str
            Dim thereader As New System.IO.StringReader(str)

            Dim mytable As New DataSet
            mytable.ReadXml(thereader)
            mytable.Tables(1).Merge(mytable.Tables(2))
            rsp.Close()
            Return mytable.Tables(1)

        Catch ex As Exception
            '  Return ex.Message
        End Try

    End Function
    <WebMethod(Description:="mcuCDRFull")>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function getCDRMeetingDetail(ByVal mcuID As String, ByVal mcuIP As String, ByVal tokenid As String, ByVal lastmeetingid As String) As String
        '   Try
        '' lastmeetingid = 220
        Dim myDT = dbOp.getDBTable("select id from cdr_summary where  status_str  <> 'ongoing' and file_version = " & mcuID & " and id > " & lastmeetingid, "mcudb")
        Dim err = ""
        For Each r In myDT.Rows
            Dim xmltext = "<TRANS_CDR_FULL> " &
                      " <TRANS_COMMON_PARAMS>" &
                        "   <MCU_TOKEN>" & tokenid & "</MCU_TOKEN>" &
                         "  <MCU_USER_TOKEN>" & tokenid & "</MCU_USER_TOKEN>" &
                        "   <MESSAGE_ID>147</MESSAGE_ID>" &
                       "</TRANS_COMMON_PARAMS>" &
                      " <ACTION>" &
                       "    <GET>" &
                         "      <ID>" & r(0) & "</ID>" &
                        "   </GET>" &
                      " </ACTION>" &
                  " </TRANS_CDR_FULL>"

            Dim req As System.Net.WebRequest = Nothing
            Dim rsp As System.Net.WebResponse = Nothing
            'Try
            Dim uri As String = "http://" & mcuIP '"http://10.1.253.129"

            req = System.Net.WebRequest.Create(uri)
            req.Proxy = Nothing
            req.Method = "POST"
            req.ContentType = "text/xml"
            Dim writer As New System.IO.StreamWriter(req.GetRequestStream())
            writer.WriteLine(xmltext)
            writer.Close()
            rsp = req.GetResponse()
            Dim doc As XDocument = XDocument.Load(rsp.GetResponseStream())
            'Return doc.ToString & "<br>" & xmltext
            ''  dbOp.executeDB("insert into log (log, dt) values ('Rows: " & myDT.Rows.Count & " last meetingid:" & lastmeetingid & " " & doc.ToString & "', current_timestamp)", "mcudb")
            Dim str = "<root>"
            ''''##### Trim down xml document for what is required in memory. Add root node manually
            Dim childList As IEnumerable(Of XElement) =
        From el In doc.Elements("RESPONSE_TRANS_CDR_FULL").Elements("ACTION").Elements("GET").Elements("CDR_FULL").Elements("CDR_EVENT").Elements("H323_CALL_SETUP")
        Select el
            For Each rec As XElement In childList
                str = str & Strings.Trim(rec.ToString)
            Next
            str = str & "</root>"
            '''#### Read string to load in datatable as xml from memory
            '  Return str
            Dim thereader As New System.IO.StringReader(str)

            Dim mytable As New DataSet
            mytable.ReadXml(thereader)
            If mytable.Tables.Count = 0 Then Continue For
            mytable.Tables(0).DefaultView.ToTable(True, {"DESTINATION_ADDRESS", "NAME", "MAX_RATE", "SOURCE_ADDRESS"})
            Dim mysites = mytable.Tables(0).AsEnumerable().[Select](Function(row) New With {
    Key .ip = row.Field(Of String)("DESTINATION_ADDRESS"),
     .site = row.Field(Of String)("NAME"),
     .bw = row.Field(Of String)("MAX_RATE"),
     .source = row.Field(Of String)("SOURCE_ADDRESS")
}).Distinct()
            Dim persons = mysites.Count * 2
            For Each proj In mysites
                Dim q = "insert into cdr_full(meetingid, ip, site, bw, source, mcuid) values (" & r(0) & ", '" & proj.ip & "', '" & proj.site & "', " & proj.bw & ", '" & proj.source & "', " & mcuID & " )"
                Dim ret = dbOp.executeDB(q, "mcudb")
                If ret.Contains("Error") Then
                    err = err & " Query not executed.. " & ret & q
                End If
            Next
            '''##### Trim down xml document for what is required in memory. Add root node manually
            str = "<root>"
            Dim childList2 As IEnumerable(Of XElement) =
            From el In doc.Elements("RESPONSE_TRANS_CDR_FULL").Elements("ACTION").Elements("GET").Elements("CDR_FULL").Elements("CDR_EVENT").Elements("CONF_START_4").Elements("CONTACT_INFO_LIST")
            Select el
            For Each rec As XElement In childList2
                str = str & Strings.Trim(rec.ToString)
            Next
            str = str & "</root>"
            '''#### Read string to load in datatable as xml from memory
            Dim thereader2 As New System.IO.StringReader(str)

            Dim mytable2 As New DataSet
            mytable2.ReadXml(thereader2)
            Dim info1 = ""
            Dim info2 = ""
            Dim info3 = ""
            'err = err & "--" & mytable2.Tables(0).Columns.Count & str
            Dim totalinfcol = mytable2.Tables(0).Columns.Count
            '   GridView1.DataSource = mytable2.Tables(0)
            '   GridView1.DataBind()
            info2 = persons
            If totalinfcol >= 1 Then
                For Each inf In mytable2.Tables(0).Rows
                    If totalinfcol > 0 Then info1 = IIf(String.IsNullOrEmpty(inf(0).ToString), info1, inf(0))
                    If totalinfcol > 1 Then info2 = IIf(String.IsNullOrEmpty(inf(1).ToString), info2, inf(1))
                    If totalinfcol > 2 Then info3 = IIf(String.IsNullOrEmpty(inf(2).ToString), info3, inf(2))
                    ' Exit For

                Next
            End If

            Dim q2 = "update cdr_summary set info1='" & info1 & "', info2='" & info2 & "', info3='" & info3 & "' where id=" & r(0) & " and file_version = " & mcuID
            Dim ret2 = dbOp.executeDB(q2, "mcudb")
            If ret2.Contains("Error") Then
                err = err & " Query not executed.. " & ret2 & q2
            End If

        Next

        Return err
        'Catch ex As Exception
        '    Return ex.Message
        'End Try

    End Function
End Class