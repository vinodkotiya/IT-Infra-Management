Imports dbOp
Imports Common
Partial Class _ocmsreport
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
                If Request.Params("tech") Is Nothing Then
                    Response.Redirect("Default.aspx")
                End If
                gvReport.DataSource = getDBTable("select name || '(' || eid || ')' as name, contact,  dept || ' ' || location as Dept,  type, descr, status, closingremark, st_dt as Open, end_dt as Close from ocms where tech like'" & Request.Params("tech") & "%'  and strftime('%m', st_dt) = strftime('%m', current_date) and  strftime('%Y',st_dt) = strftime('%Y',current_date) order by st_dt desc")
                gvReport.DataBind()

                divFoot.InnerHtml = "<span class='brand'>IT-Corporate Center, SCOPE</span> &copy; <span id='copyright-year'></span> | HitCount: " & getDBsingle("select view from hits where page='ccit'") & " | Visitors Online: " & Application("OnlineUsers").ToString & " "
            End If
            '' do postback stuff here

        Catch e1 As Exception
            Response.Write("<div id='bottomline'>" & e1.Message & "</div>")
        End Try
    End Sub

End Class
