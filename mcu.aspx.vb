Imports System.Data
Imports dbOp
Imports System.Xml.Linq
Partial Class mcu
    Inherits System.Web.UI.Page

    Private Sub btnCDRFullProcess_Click(sender As Object, e As EventArgs) Handles btnCDRFullProcess.Click
        Dim myDT = getDBTable("select id from cdr_summary", "mcudb")
        Dim err = ""
        '''DO THE LOGIN
        'Dim reqlogin As System.Net.WebRequest = Nothing
        'Dim rsplogin As System.Net.WebResponse = Nothing
        ''Try
        Dim uri As String = "http://10.1.253.136"
        'reqlogin = System.Net.WebRequest.Create(uri)
        'reqlogin.Method = "POST"
        'reqlogin.ContentType = "text/xml"
        'Dim writerlogin As New System.IO.StreamWriter(reqlogin.GetRequestStream())
        'writerlogin.WriteLine(getText("login"))
        'writerlogin.Close()
        'rsplogin = reqlogin.GetResponse()
        'Dim sr As New System.IO.StreamReader(rsplogin.GetResponseStream(), System.Text.Encoding.[Default])
        'Dim backstr As String = sr.ReadToEnd()
        'txtResponse.Text = backstr
        ''' NOW Loop through
        For Each r In myDT.Rows
            err = err & r(0)
            '' NO get cdr full
            Dim req As System.Net.WebRequest = Nothing
            Dim rsp As System.Net.WebResponse = Nothing
            'Try
            req = System.Net.WebRequest.Create(uri)
            req.Method = "POST"
            req.ContentType = "text/xml"
            Dim writer As New System.IO.StreamWriter(req.GetRequestStream())
            writer.WriteLine(getText("cdr_full", r(0)))
            writer.Close()
            rsp = req.GetResponse()
            Dim doc As XDocument = XDocument.Load(rsp.GetResponseStream())
            txtResponse.Text = doc.ToString
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
            Dim thereader As New System.IO.StringReader(str)

            Dim mytable As New DataSet

            mytable.ReadXml(thereader)
            If mytable.Tables.Count = 0 Then Continue For
            ' GridView1.DataSource = mytable.Tables(0).DefaultView.ToTable(True, {"DESTINATION_ADDRESS", "NAME", "MAX_RATE", "SOURCE_ADDRESS"})
            Dim mysites = mytable.Tables(0).AsEnumerable().[Select](Function(row) New With {
    Key .ip = row.Field(Of String)("DESTINATION_ADDRESS"),
     .site = row.Field(Of String)("NAME"),
     .bw = row.Field(Of String)("MAX_RATE"),
     .source = row.Field(Of String)("SOURCE_ADDRESS")
}).Distinct()

            GridView1.DataSource = mysites
            GridView1.DataBind()
            Dim persons = mysites.Count * 2
            For Each proj In mysites
                Dim q = "insert into cdr_full(meetingid, ip, site, bw, source) values (" & r(0) & ", '" & proj.ip & "', '" & proj.site & "', " & proj.bw & ", '" & proj.source & "' )"
                Dim ret = executeDB(q, "mcudb")
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
            Dim feedback1, feedback2 As String
            If mytable2.Tables(0).Columns.Count = 3 Then
                '   GridView1.DataSource = mytable2.Tables(0)
                '   GridView1.DataBind()

                For Each inf In mytable2.Tables(0).Rows
                    feedback1 = IIf(String.IsNullOrEmpty(inf(1).ToString), "", inf(1))
                    feedback1 = IIf(String.IsNullOrEmpty(inf(2).ToString), "", inf(2))
                    Exit For

                Next

            End If
            Dim q2 = "update cdr_summary set persons='" & persons & "', feedback1='" & feedback1 & "', feedback2='" & feedback2 & "' where id=" & r(0)
            Dim ret2 = executeDB(q2, "mcudb")
            If ret2.Contains("Error") Then
                err = err & " Query not executed.. " & ret2 & q2
            End If
            '   err = err & q2

            ' Exit For
        Next
        txtResponse.Text = err

    End Sub

    Private Sub btnMCULogin_Click(sender As Object, e As EventArgs) Handles btnMCULogin.Click
        txtXML.Text = getText("login")
    End Sub
    Private Sub btnCDRFull_Click(sender As Object, e As EventArgs) Handles btnCDRFull.Click
        txtXML.Text = getText("cdr_full")
    End Sub

    Private Sub btnCDRList_Click(sender As Object, e As EventArgs) Handles btnCDRList.Click
        txtXML.Text = getText("cdr_list")
    End Sub
    Function getText(ByVal type As String, Optional ByVal meetingid As String = "204") As String
        If type = "login" Then
            Return "  <TRANS_MCU><!-- The root Is the transaction name. -->" &
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
        "<IP>10.1.253.136</IP>" &
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
        ElseIf type = "cdr_full" Then
            Return "<TRANS_CDR_FULL> " &
                      " <TRANS_COMMON_PARAMS>" &
                        "   <MCU_TOKEN>126</MCU_TOKEN>" &
                         "  <MCU_USER_TOKEN>126</MCU_USER_TOKEN>" &
                        "   <MESSAGE_ID>147</MESSAGE_ID>" &
                       "</TRANS_COMMON_PARAMS>" &
                      " <ACTION>" &
                       "    <GET>" &
                         "      <ID>" & meetingid & "</ID>" &
                        "   </GET>" &
                      " </ACTION>" &
                  " </TRANS_CDR_FULL>"
        ElseIf type = "cdr_list" Then
            Return "<TRANS_CDR_LIST>" &
    "<TRANS_COMMON_PARAMS> " &
    "<MCU_TOKEN>126</MCU_TOKEN> " &
    "<MCU_USER_TOKEN>126</MCU_USER_TOKEN>" &
   " <MESSAGE_ID>147</MESSAGE_ID>" &
    "</TRANS_COMMON_PARAMS>" &
     "<ACTION> " &
    "<GET>" &
    "<CDR_SUMMARY_LS/>" &
    "</GET>" &
    "</ACTION>" &
    "</TRANS_CDR_LIST>"
        End If
    End Function

    Private Sub btnPost_Click(sender As Object, e As EventArgs) Handles btnPost.Click
        Dim req As System.Net.WebRequest = Nothing
        Dim rsp As System.Net.WebResponse = Nothing
        'Try
        Dim uri As String = "http://10.1.253.136"

        req = System.Net.WebRequest.Create(uri)
        req.Proxy = Nothing
        req.Method = "POST"
            req.ContentType = "text/xml"
            Dim writer As New System.IO.StreamWriter(req.GetRequestStream())
            writer.WriteLine(txtXML.Text)
            writer.Close()
            rsp = req.GetResponse()


        If txtXML.Text.Contains("TRANS_CDR_LIST") Then
            Dim sr As New System.IO.StreamReader(rsp.GetResponseStream(), System.Text.Encoding.[Default])
            Dim backstr As String = sr.ReadToEnd()
            txtResponse.Text = backstr
            Dim mytable As New DataSet
            'mytable.ReadXmlSchema(Server.MapPath("~/upload/mcu.xsd"))
            'mytable.ReadXml(rsp.GetResponseStream(), XmlReadMode.InferSchema)

            'mytable.Tables(4).Merge(mytable.Tables(5))
            'GridView1.DataSource = mytable.Tables(4) '.AsEnumerable().Where(Function(a) Not Regex.IsMatch(a("DISPLAY_NAME").ToString(), "TEST*")).CopyToDataTable()
            'GridView1.DataBind()
            '  btnXML.Text = insertTableinDB(mytable.Tables(4), "cdr_summary", "mcudb")

            Dim s = ""
            'For i = 0 To mytable.Tables(4).Columns.Count - 1
            '    s = s & mytable.Tables(4).Columns(i).ColumnName.ToString & ","
            'Next
            Dim q = ""
            Dim err = ""
            ' For Each row In mytable.Tables(4).Rows
            ' Dim starttime = DateTime.Parse(row(6))
            ' starttime = starttime.AddMinutes(330)
            ' q = "insert into cdr_summary (FILE_VERSION,NAME,ID,STATUS_STR,STATUS,GMT_OFFSET,START_TIME,RESERVE_START_TIME,MCU_FILE_NAME,FILE_SAVED,GMT_OFFSET_MINUTE,DISPLAY_NAME,RESERVED_AUDIO_PARTIES,RESERVED_VIDEO_PARTIES,CDR_SUMMARY_Id,CDR_SUMMARY_LS_Id,HOUR,MINUTE,SECOND) " &
            '     " values( 1, '" & row(1) & "', " & row(2) & ", '" & row(3) & "', " & row(4) & ", " & row(5) & ", '" & starttime.ToString("yyyy-MM-dd HH:mm:ss") & "', '" & row(7) &
            '      "', '" & row(8) & "', '" & row(9) & "', " & row(10) & ", '" & row(11) & "', " & row(12) & ", " & row(13) & ", " & row(14) &
            '", " & row(15) & ", " & row(16) & ", " & row(17) & ", " & row(18) & ") "
            ' Dim ret = executeDB(q, "mcudb")
            ' If ret.Contains("Error") Then
            '     err = err & " Query not executed.. " & ret & q

            ' End If
            '' Exit For
            'Next
            ' txtResponse.Text = err
        ElseIf txtXML.Text.Contains("TRANS_CDR_FULL") Then

            'Dim sr As New System.IO.StreamReader(rsp.GetResponseStream(), System.Text.Encoding.[Default])

            'Dim backstr As String = sr.ReadToEnd()
            'txtResponse.Text = backstr
            '''##### Create XML Document from response in memory
            Dim doc As XDocument = XDocument.Load(rsp.GetResponseStream())
            txtResponse.Text = doc.ToString
            ' Exit Sub
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
            Dim thereader As New System.IO.StringReader(str)

            Dim mytable As New DataSet
            mytable.ReadXml(thereader)
            'GridView1.DataSource = mytable.Tables(0).DefaultView.ToTable(True, {"DESTINATION_ADDRESS", "NAME", "MAX_RATE", "SOURCE_ADDRESS"})
            GridView1.DataSource = mytable.Tables(0).AsEnumerable().[Select](Function(row) New With {
    Key .ip = row.Field(Of String)("DESTINATION_ADDRESS"),
     .site = row.Field(Of String)("NAME"),
     .bw = row.Field(Of String)("MAX_RATE"),
     .source = row.Field(Of String)("SOURCE_ADDRESS")
}).Distinct()
            GridView1.DataBind()
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
            If mytable2.Tables(0).Columns.Count > 0 Then
                'GridView1.DataSource = mytable2.Tables(0)

                'GridView1.DataBind()
            End If
            txtResponse.Text = doc.ToString


            'Dim mytable As New DataSet
            'mytable.ReadXmlSchema(Server.MapPath("~/upload/cdrfull.xsd"))
            'mytable.ReadXml(rsp.GetResponseStream())

            'GridView1.DataSource = mytable.Tables(26).DefaultView.ToTable(True, {"NAME", "MAX_RATE", "SOURCE_ADDRESS", "DESTINATION_ADDRESS"})
            'GridView1.DataBind()
            'btnXML.Text = mytable.Tables(26).TableName 'H323_CALL_SETUP


        Else
            Dim sr As New System.IO.StreamReader(rsp.GetResponseStream(), System.Text.Encoding.[Default])
            Dim backstr As String = sr.ReadToEnd()
            txtResponse.Text = backstr
        End If
        rsp.Close()
        '  txtResponse.Text = backstr
        'Catch ex As Exception
        '    txtResponse.Text = "Error" & ex.Message
        'Finally
        '    If req IsNot Nothing Then
        '        req.GetRequestStream().Close()
        '    End If
        '    If rsp IsNot Nothing Then
        '        rsp.GetResponseStream().Close()
        '    End If
        'End Try

    End Sub

    Private Sub btnXML_Click(sender As Object, e As EventArgs) Handles btnXML.Click

        Dim cdrfull As New DataSet
        cdrfull.ReadXmlSchema(Server.MapPath("~/upload/cdrfull.xsd"))
        cdrfull.ReadXml(Server.MapPath("~/upload/cdrfull.xml"))

        ''' table 19/*22 contains site list. 20 contains ip address
        ''' '' 26 site, ip, bw
        'Dim cdrlist As New DataSet
        'cdrlist.ReadXmlSchema(Server.MapPath("~/upload/mcu.xsd"))
        'cdrlist.ReadXml(Server.MapPath("~/upload/mcu.xml"))
        'cdrlist.Tables(4).Merge(cdrlist.Tables(5))
        ';cdrlist.Tables(4).Merge(cdrfull.Tables(4))
        btnXML.Text = cdrfull.Tables.Count & " table" & Session("tableid")
        GridView1.DataSource = cdrfull.Tables(Session("tableid"))
        GridView1.DataBind()
        Session("tableid") = Session("tableid") + 1
        ''delete test, default, 1001
        '' retain meet, support
    End Sub

    Private Sub mcu_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Session("tableid") = 0
        End If
    End Sub


End Class
