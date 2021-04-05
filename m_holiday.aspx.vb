Imports dbOp
Imports Common
Partial Class _m_holiday
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
                gvHoliday.DataSource = getDBTable("select holi || ' (' || hindi || ')' as Holiday, dt as Date, dy as Day from holiday where 1 order by uid")
                gvHoliday.DataBind()
                '   divFoot.InnerHtml = "<span class='brand'>IT-Corporate Center, SCOPE</span> &copy; <span id='copyright-year'></span> | HitCount: " & getDBsingle("select view from hits where page='ccit'") & " | Visitors Online: " & Application("OnlineUsers").ToString & " "
            End If
            '' do postback stuff here

        Catch e1 As Exception
            Response.Write("<div id='bottomline'>" & e1.Message & "</div>")
        End Try
    End Sub
End Class
