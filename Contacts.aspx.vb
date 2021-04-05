Imports dbOp
Imports Common
Imports System.Net
Imports System.IO
Partial Class _Contacts
    Inherits System.Web.UI.Page

    Private Sub _Default_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim url As String
        Dim msg = ""
        Try

            If Not Page.IsPostBack Then
                divMenu.InnerHtml = makemenu(5)

                '' executeDB("update hits set view = view+1 where page = 'LARR'")
                If Request.Params("mode") = "pic" Then
                    Dim mydt = getDBTable("select eid from ntpcemp where 1 ")
                    For Each r In mydt.Rows
                        Try
                            url = "http://10.0.236.26:8090/bday/photos/" + r(0).ToString.PadLeft(8, "0") + ".jpg"
                            Dim file_name As String = Server.MapPath("./upload/pics/") + r(0).ToString.PadLeft(6, "0") + ".jpg"

                            save_file_from_url(file_name, url)

                            msg = msg & r(0) & " The file has been saved at: " & file_name & "<br/>"
                        Catch ex As Exception
                            msg = msg & ex.Message & "<br/>"
                        End Try

                    Next
                    divMsg.InnerHtml = msg
                End If

            End If
            '' do postback stuff here

        Catch e1 As Exception
            Response.Write("<div id='bottomline'>" & e1.Message & url & msg & "</div>")
        End Try
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        If String.IsNullOrEmpty(txtSearch.Text) Or txtSearch.Text.Length < 5 Then
            divMsg.InnerHtml = "Enter atleast 4 character to search..."
            Exit Sub
        End If
        Dim sel = "eid like '%" & txtSearch.Text & "%' or name like '%" & txtSearch.Text & "%' or cell2 like '%" & txtSearch.Text & "%'  or dept like '%" & txtSearch.Text & "%' or loc like '%" & txtSearch.Text & "%' or email like '%" & txtSearch.Text & "%' "
        Dim q = "select name, desig, dept, loc, cell2 as Mobile, email, eid from ntpcemp where " & sel
        GridView1.DataSource = getDBTable(q)
        GridView1.DataBind()
    End Sub

    Private Sub GridView1_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridView1.RowDataBound
        'If e.Row.RowType = DataControlRowType.DataRow Then
        '    e.Row.Cells(6).Text = getDBsingle("select contact from ocms where '& e.Row.Cells(6).Text &' like")
        'End If

    End Sub
    Public Sub save_file_from_url(file_name As String, url As String)
        Dim content As Byte()
        Dim request As HttpWebRequest = DirectCast(WebRequest.Create(url), HttpWebRequest)
        Dim response As WebResponse = request.GetResponse()

        Dim stream As Stream = response.GetResponseStream()

        Using br As New BinaryReader(stream)
            content = br.ReadBytes(500000)
            br.Close()
        End Using
        response.Close()

        Dim fs As New FileStream(file_name, FileMode.Create)
        Dim bw As New BinaryWriter(fs)
        Try
            bw.Write(content)
        Finally
            fs.Close()
            bw.Close()
        End Try
    End Sub

End Class
