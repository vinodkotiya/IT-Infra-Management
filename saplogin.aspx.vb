Imports dbOp

Partial Class saplogin
    Inherits System.Web.UI.Page

    Private Sub btnLoad_Click(sender As Object, e As EventArgs) Handles btnLoad.Click
        If String.IsNullOrEmpty(txtURI.Text) Then
            Dim myCookie As HttpCookie = New HttpCookie("NTPCSSO")
            myCookie.Value = """" & cookieEncrypt("{""user"":""" & txtEID.Text.PadLeft(6, "0") & """,""SID"":""EP1""}") & """"
            myCookie.Expires = Now.AddDays(1)
            Response.Cookies.Add(myCookie)

            Response.Redirect(ddlURI.SelectedValue & "?c=" & myCookie.Value)
        Else
            Dim q = "insert into hits (page) values ('" & txtURI.Text & "')"
            executeDB(q)
            ddlURI.DataSource = getDBTable("select page from hits where 1")
            ddlURI.DataBind()
        End If
    End Sub


    Private Sub saplogin_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If Not Request.Params("mode") Is Nothing Then
                txtEID.Visible = True
                btnLoad.Visible = True
                txtURI.Visible = True
                ddlURI.Visible = True
                ddlURI.DataSource = getDBTable("select page from hits where 1")
                ddlURI.DataBind()
            End If
        End If

    End Sub
End Class
