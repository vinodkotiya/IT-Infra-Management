Imports dbOp
Imports Common
Imports System.Data
Imports System.Net
Imports System.IO
Partial Class _m_hospital
    Inherits System.Web.UI.Page

    Private Sub _Default_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack Then
                If Cache("menu2") Is Nothing Then
                    Cache.Insert("menu2", makemenu(2), Nothing, DateTime.Now.AddHours(12.0), TimeSpan.Zero)
                End If
                '   divMenu.InnerHtml = Cache("menu2").ToString
                executeDB("update hits set view = view+1 where page = 'ccit'")
                '' executeDB("update hits set view = view+1 where page = 'LARR'")
                gvhospital.DataSource = getDBTable("select hospital , region , end_dt as End_Date from hospital where region='CC' ")
                gvhospital.DataBind()
                rblRegion.DataSource = getDBTable("select distinct region from hospital where 1 order by region")
                rblRegion.DataBind()
                rblRegion.SelectedValue = "CC"
                '   divFoot.InnerHtml = "<span class='brand'>IT-Corporate Center, SCOPE</span> &copy; <span id='copyright-year'></span> | HitCount: " & getDBsingle("select view from hits where page='ccit'") & " | Visitors Online: " & Application("OnlineUsers").ToString & " "
                '  FindCoordinates("APOLLO%20HOSPITAL,%20NOIDA")
            End If
            '' do postback stuff here

        Catch e1 As Exception
            Response.Write("<div id='bottomline'>" & e1.Message & "</div>")
        End Try
    End Sub

    Private Sub rblRegion_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rblRegion.SelectedIndexChanged

        gvhospital.DataSource = getDBTable("select hospital , region , end_dt as End_Date from hospital where region like '%" & rblRegion.SelectedValue & "%'")
        gvhospital.DataBind()
    End Sub

    Function FindCoordinates(ByVal locationname As String)
        Dim url As String = "http://172.217.18.142/maps/api/geocode/xml?address=" + locationname + "&sensor=false" 'maps.google.com
        Dim request As WebRequest = WebRequest.Create(url)
        Using response As WebResponse = DirectCast(request.GetResponse(), HttpWebResponse)
            Using reader As New StreamReader(response.GetResponseStream(), Encoding.UTF8)
                Dim dsResult As New DataSet()
                dsResult.ReadXml(reader)
                Dim dtCoordinates As New DataTable()
                dtCoordinates.Columns.AddRange(New DataColumn(3) {New DataColumn("Id", GetType(Integer)), New DataColumn("Address", GetType(String)), New DataColumn("Latitude", GetType(String)), New DataColumn("Longitude", GetType(String))})
                For Each row As DataRow In dsResult.Tables("result").Rows
                    Dim geometry_id As String = dsResult.Tables("geometry").[Select]("result_id = " + row("result_id").ToString())(0)("geometry_id").ToString()
                    Dim location As DataRow = dsResult.Tables("location").[Select](Convert.ToString("geometry_id = ") & geometry_id)(0)
                    dtCoordinates.Rows.Add(row("result_id"), row("formatted_address"), location("lat"), location("lng"))
                Next
                If dtCoordinates.Rows.Count > 0 Then
                    'pnlScripts.Visible = True
                    'rptMarkers.DataSource = dtCoordinates
                    'rptMarkers.DataBind()
                    divMsg.InnerHtml = dtCoordinates(0)(0) + dtCoordinates(0)(1) + dtCoordinates(0)(2) + dtCoordinates(0)(3)
                End If
            End Using
        End Using
    End Function
End Class
