Imports dbOp
Imports Common
Partial Class _edituser
    Inherits System.Web.UI.Page

    Private Sub _Default_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack Then
                divMenu.InnerHtml = makemenu(5)

                '' executeDB("update hits set view = view+1 where page = 'LARR'")
                Dim q = "select name, desig, dept, loc, cell2 as Mobile, email, eid, ipaddress, ad, extra from ntpcemp where loc like '%SCOPE%' "
                GridView1.DataSource = getDBTable(q)
                GridView1.DataBind()

            End If
            '' do postback stuff here

        Catch e1 As Exception
            Response.Write("<div id='bottomline'>" & e1.Message & "</div>")
        End Try
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        If String.IsNullOrEmpty(txtSearch.Text) Or txtSearch.Text.Length < 5 Then
            divMsg.InnerHtml = "Enter atleast 4 character to search..."
            Exit Sub
        End If
        Dim sel = " ( eid like '%" & txtSearch.Text & "%' or name like '%" & txtSearch.Text & "%' or cell2 like '%" & txtSearch.Text & "%'  or ipaddress like '%" & txtSearch.Text & "%' or loc like '%" & txtSearch.Text & "%' or email like '%" & txtSearch.Text & "%' ) "
        Dim q = "select name, desig, dept, loc, cell2 as Mobile, email, eid, ipaddress, ad, extra from ntpcemp where loc like '%SCOPE%' and " & sel
        GridView1.DataSource = getDBTable(q)
        GridView1.DataBind()
    End Sub
    Private Sub GridView1_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridView1.RowCommand
        If Session("adminid") Is Nothing Then
            pnlLogin.Visible = True
            pnlSearch.Visible = False
            Exit Sub
        End If
        Dim value = e.CommandArgument.ToString()
        Session("editeid") = value
        divMsg.InnerHtml = value
        pnlLogin.Visible = False
        pnlSearch.Visible = False
        pnlUpdate.Visible = True
        ' ibtnNew_Click(Me, New ImageClickEventArgs(0, 0))
        loadUserDetail(Session("editeid"))
    End Sub
    Sub loadUserDetail(ByVal eid As String)
        Dim mydt = getDBTable("select name, cell2, email,  ipaddress, ad, extra from ntpcemp where  eid =" & eid)
        If mydt.Rows.Count > 0 Then
            txtName.Text = mydt.Rows(0).Item(0).ToString
            txtMobile.Text = mydt.Rows(0).Item(1).ToString
            txtEmail.Text = mydt.Rows(0).Item(2).ToString
            txtIP.Text = mydt.Rows(0).Item(3).ToString
            txtExtra.Text = mydt.Rows(0).Item(5).ToString
            Dim chk = False
            If mydt.Rows(0).Item(4).ToString = "1" Then chk = True
            chkAD.Checked = chk

        End If
    End Sub
    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        If String.IsNullOrEmpty(txtLogin.Text) Or String.IsNullOrEmpty(txtPwd.Text) Then
            divMsg.InnerHtml = "Fields empty"
            Exit Sub
        End If
        Dim ret = getDBsingle("select groupid from users where eid ='" & txtLogin.Text & "' and pwd='" & txtPwd.Text & "'")
        If ret.Contains("Error") Then
            divMsg.InnerHtml = "Login incorrect"

        Else
            Session("adminid") = txtLogin.Text
            Session("groupid") = ret
            ''To show only thier core complaints
            Session("core") = "%" 'getDBsingle("select core from users where eid ='" & txtAdminID.Text & "'")
            pnlLogin.Visible = False
            pnlSearch.Visible = True
        End If
    End Sub
    Private Sub GridView1_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridView1.RowDataBound
        'If e.Row.RowType = DataControlRowType.DataRow Then
        '    e.Row.Cells(6).Text = getDBsingle("select contact from ocms where '& e.Row.Cells(6).Text &' like")
        'End If

    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        Dim chk = 0
        If chkAD.Checked Then chk = 1
        Dim q = "update ntpcemp set name = '" & txtName.Text & "', cell2 = '" & txtMobile.Text & "', email = '" & txtEmail.Text & "', ipaddress = '" & txtIP.Text & "', ad=" & chk & ", extra = '" & txtExtra.Text & "' where eid = " & Session("editeid")
        Dim ret2 = executeDB(q)
        If ret2.Contains("Error") Then
            divMsg.InnerHtml = " Query not executed.. " & ret2 & q
        Else
            Response.Redirect("edituser.aspx")
        End If

        pnlUpdate.Visible = False
        pnlSearch.Visible = True


    End Sub
End Class
