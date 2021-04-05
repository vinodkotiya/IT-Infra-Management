Imports dbOp
Imports Common
Imports System.Net
Imports System.IO
Imports System.Net.Mail
Partial Class _asset
    Inherits System.Web.UI.Page

    Private Sub _Default_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack Then

                If Cache("menu2") Is Nothing Then
                    Cache.Insert("menu2", makemenu(2), Nothing, DateTime.Now.AddHours(12.0), TimeSpan.Zero)
                End If
                divMenu.InnerHtml = Cache("menu2").ToString

                executeDB("update hits set view = view+1 where page = 'ocms'")
                divFoot.InnerHtml = "<span class='brand'>IT-Corporate Center, SCOPE</span> &copy; <span id='copyright-year'></span> | HitCount: " & getDBsingle("select view from hits where page='ocms'") & " | Visitors Online: " & Application("OnlineUsers").ToString & " "
                pnlPO.Visible = False
                pnlAssetBooking.Visible = False
                pnlAdminLogin.Visible = False
                pnlAssign.Visible = False
                If Request.Params("ctype") Is Nothing Then
                    pnlHome.Visible = True
                    'If Cache("wall") Is Nothing Then
                    '    Cache.Insert("wall", getWallofFame(), Nothing, DateTime.Now.AddMinutes(10.0), TimeSpan.Zero)
                    'End If
                    ''divWallofFame.InnerHtml = Cache("wall").ToString

                Else
                    'admin
                    If Session("Assetadminid") Is Nothing Then
                        pnlAdminLogin.Visible = True
                        Exit Sub
                    End If
                    If Request.Params("ctype") = "entry" Then
                        loadAssetEntryForm()
                        pnlAssetBooking.Visible = True

                    ElseIf Request.Params("ctype") = "po" Then
                        'loadAssetEntryForm()
                        pnlAssetBooking.Visible = False
                        pnlPO.Visible = True
                    ElseIf Request.Params("ctype") = "assign" Then
                        ddlAdminFilter.DataSource = getDBTable("select id,type from asset_type where 1")
                        ddlAdminFilter.DataBind()
                        ddlAdminFilter.Items.Insert(0, "%")
                        pnlAssign.Visible = True
                        loadAdminStatus("", "%")
                        'loadAssetEntryForm()
                        'pnlAssetBooking.Visible = True
                        'ElseIf Not Request.Params("ctype") = "admin" Then

                        '    LinkButton1.Visible = True
                        '        If Request.Params("ctype") = "status" Then
                        '        pnlAssetBooking.Visible = False
                        '        pnlStatus.Visible = True
                        '            loadStatus(Session("eid"))
                        '            Exit Sub
                        '        End If
                        '    pnlAssetBooking.Visible = False
                    ElseIf Request.Params("ctype") = "report" Then
                        LinkButton1.Visible = True
                        pnlStatus.Visible = True
                        loadStatus("")


                    End If
                End If



            End If
            '' do postback stuff here

        Catch e1 As Exception
            Response.Write("<div id='bottomline'>" & e1.Message & "</div>")
        End Try
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        If Session("Assetadminid") Is Nothing Then
            Response.Redirect(Request.RawUrl)
            Exit Sub
        End If
        Dim searchid = ""
        If Not String.IsNullOrEmpty(txtSearchID.Text) Then searchid = " (uid = '" & txtSearchID.Text & "' or owner = '" & txtSearchID.Text & "'  or po like '%" & txtSearchID.Text & "%') and "

        loadAdminStatus(searchid, ddlAdminFilter.SelectedItem.Text)

    End Sub
    Public Function loadStatus(ByVal eid As String)
        gvStatus.DataSource = getDBTable("select uid  , atype, make, model, cast(sn as text) as sn, detail, owner, st_dt , po, chain from asset_detail where 1 order by last_updated desc")
        gvStatus.DataBind()

    End Function
    Public Function loadAdminStatus(ByVal searchID As String, ByVal atype As String)

        Dim q = "select uid  , atype, make, model, cast(sn as text) as sn, detail, owner, st_dt , po, chain from asset_detail where " &
           searchID & " atype  like '%" & atype & "%' order by uid limit 200" &
            ""
        '"union select id , type, descr, tech, cast(priority as text) as priority, strftime('%d.%m.%Y %H:%M', datetime(st_dt,'+330 Minute')) as 'Date', status, name || ' (' || eid || ')' as name , contact,dept, location, closingremark from ocms where not status = 'Pending' and typeid in (" & groupid & ") and location like '%" & loc & "%' order by status desc, priority desc, st_dt asc "
        'divMsg.InnerHtml = q
        ' Return 1
        gvAdminStatus.DataSource = getDBTable(q)
        gvAdminStatus.DataBind()

    End Function
    Public Function loadAssetEntryForm()
        ddlCtype.DataSource = getDBTable("select id,type from asset_type where 1")
        ddlCtype.DataBind()
        ' ddlCtype.SelectedValue = assettype
        ddlPO.DataSource = getDBTable("select po as v, po || '-' || vendor as t  from asset_vendor where 1 order by po desc")
        ddlPO.DataBind()
        ddlMake.DataSource = getDBTable("select distinct make from asset_detail where 1")
        ddlMake.DataBind()
        ddlMake.Items.Insert(0, "Not in the list")
        ddlMake.SelectedValue = getDBsingle("select make from asset_detail where 1 order by last_updated desc limit 1")
        txtModel.Text = getDBsingle("select model from asset_detail where 1 order by last_updated desc limit 1")

        divMsgComplaint.InnerHtml = "Your IP Address is:" & Request.UserHostAddress
    End Function
    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        '''Login web service call
        '''
        If String.IsNullOrEmpty(txtEid.Text) Then
            divMsg.InnerHtml = "Please enter employee number."
            Exit Sub
        End If
        Dim mydt = getDBTable("select name, dept, cell2, email from ntpcemp where loc like '%SCOPE%' and eid =" & txtEid.Text)
        If mydt.Rows.Count = 0 Then
            divMsg.InnerHtml = "Employee dosen't exist"
            ''write code to create sign up

            '' try to get employee data from other plant
            mydt = getDBTable("select name, dept, cell2, email from ntpcemp where  eid =" & txtEid.Text)
            pnlLogin.Visible = False
            'pnlNewUser.Visible = True
            'txtNewEid.Text = txtEid.Text
            If mydt.Rows.Count > 0 Then
                'txtNewName.Text = mydt.Rows(0).Item(0).ToString
                'txtNewMobile.Text = mydt.Rows(0).Item(2).ToString
                'txtNewDept.Text = mydt.Rows(0).Item(1).ToString
            End If
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
    Private Sub btnAdminLogin_Click(sender As Object, e As EventArgs) Handles btnAdminLogin.Click
        If String.IsNullOrEmpty(txtAdminID.Text) Or String.IsNullOrEmpty(txtAdminPwd.Text) Then
            divMsg.InnerHtml = "Fields empty"
            Exit Sub
        End If
        Dim ret = getDBsingle("select groupid from users where eid ='" & txtAdminID.Text & "' and pwd='" & txtAdminPwd.Text & "' and groupid like '%10%'")
        If ret.Contains("Error") Then
            divMsg.InnerHtml = "Login incorrect"

        Else
            Session("Assetadminid") = txtAdminID.Text
            Session("groupid") = ret
            ''To show only thier core complaints
            Session("core") = "%" 'getDBsingle("select core from users where eid ='" & txtAdminID.Text & "'")
            Response.Redirect(Request.RawUrl)
        End If

    End Sub

    Private Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        Dim make = ddlMake.SelectedValue
        If ddlMake.SelectedIndex = 0 Then
            make = txtMake.Text
        End If
        If String.IsNullOrEmpty(make) Or String.IsNullOrEmpty(txtuid.Text) Or String.IsNullOrEmpty(txtSN.Text) Or String.IsNullOrEmpty(txtDate.Text) Then
            divMsgComplaint.InnerHtml = "Please enter the * fields..."
            Exit Sub
        End If



        Dim q = "insert into asset_detail (atype,make,model,sn,owner,st_dt,po, last_updated, chain, detail) values(" &
                          " '" & ddlCtype.SelectedItem.Text & "', '" & make.Replace("'", " ") & "', '" & txtModel.Text.Replace("'", " ") & "', '" & txtSN.Text.Replace("'", " ") & "'," & txtuid.Text & ", '" &
                        txtDate.Text.Replace("'", " ") & "', " & ddlPO.SelectedValue & ", current_timestamp, 'Asset Created','" & txtDescr.Text & "')"
        divMsgComplaint.InnerHtml = q
        Dim ret = executeDB(q)
        If ret.Contains("Error") Then
            divMsg.InnerHtml = "Unable to submit " & ret & q
            Exit Sub
        End If


        ''create message


        Response.Redirect("asset.aspx")
        'End Try
    End Sub
    Private Sub btnCreatePO_Click(sender As Object, e As EventArgs) Handles btnCreatePO.Click

        If String.IsNullOrEmpty(txtvPO.Text) Or String.IsNullOrEmpty(txtvPODate.Text) Or String.IsNullOrEmpty(txtvName.Text) Then
            divMsgComplaint.InnerHtml = "Please enter the * fields..."
            Exit Sub
        End If



        Dim q = "insert into asset_vendor (po,vendor,vcontact,podate,install_dt,validity) values(" &
                          " '" & txtvPO.Text.Replace("'", " ") & "', '" & txtvName.Text.Replace("'", " ") & "', '" & txtvContact.Text.Replace("'", " ") & "', '" & txtvPODate.Text.Replace("'", " ") & "','" & txtvInstallDate.Text & "', '" &
                        txtvValidity.Text.Replace("'", " ") & "'  )"
        divMsgComplaint.InnerHtml = q
        Dim ret = executeDB(q)
        If ret.Contains("Error") Then
            divMsg.InnerHtml = "Unable to submit " & ret & q
            Exit Sub
        End If


        ''create message


        Response.Redirect("asset.aspx")
    End Sub

    Private Sub LinkButton1_Click(sender As Object, e As EventArgs) Handles LinkButton1.Click
        Session.Clear()
        Session.Abandon()
        Response.Redirect("Default.aspx")
    End Sub

    Private Sub gvAdminStatus_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvAdminStatus.RowCommand
        Dim value = e.CommandArgument.ToString()
        lblCurrentID.Text = value
        Dim owner = getDBsingle("select name || ' ' || dept || ' ' || cell2 from  ntpcemp where eid = (select owner from asset_detail where uid = " & value & ") limit 1")
        If owner.Contains("Error") Then owner = "IT Dept"
        lblCurrentUser.Text = owner
        pnlAdminEdit.Visible = True

    End Sub

    Private Sub btnChange_Click(sender As Object, e As EventArgs) Handles btnChange.Click
        If String.IsNullOrEmpty(txtEditDt.Text) Then
            divMsg.InnerHtml = "Enter Issue date"
            Exit Sub
        End If
        Dim q = "update asset_detail set owner = '" & txtEditNewUID.Text & "', st_dt = '" & txtEditDt.Text & "', chain = chain || ' > ' ||  cast(owner as text) || ' ' || st_dt || ' ' where uid=" & lblCurrentID.Text
        Dim ret = executeDB(q)
        If ret.Contains("Error") Then
            divMsg.InnerHtml = "Unable to update" & ret & q
            Exit Sub
        End If
        'divMsg.InnerHtml = q
        Response.Redirect("asset.aspx?ctype=assign&ret=" & ret)
    End Sub

    Private Sub gvStatus_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvStatus.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Cells(3).Text = "<img src=images/" & e.Row.Cells(3).Text & ".png width=40px />"
            e.Row.Cells(5).Text = "<img src=images/" & e.Row.Cells(5).Text & ".png width=40px />"
        End If

    End Sub

    Private Sub gvAdminStatus_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvAdminStatus.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then


            'If e.Row.Cells(11).ToString.Contains("Closed") Then
            '    Dim btnEdit = e.Row.FindControl("btnEdit")

            '    btnEdit.Visible = False
            '    '   e.Row.BackColor = Drawing.Color.LightGreen
            'End If
        End If
    End Sub


End Class
