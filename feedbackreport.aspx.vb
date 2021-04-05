Imports dbOp
Imports Common
Imports System.Net
Imports System.IO
Imports System.Net.Mail
Partial Class _feedbackreport
    Inherits System.Web.UI.Page
    Private IPs As New List(Of String)()
    Private ipKey As Integer

    Private Sub _Default_Load(sender As Object, e As EventArgs) Handles Me.Load
        '   Try

        If Not Page.IsPostBack Then
            loadReport()
        End If
        'Catch e1 As Exception
        '    Response.Write("<div id='bottomline'>" & e1.Message & "</div>")
        'End Try
    End Sub
    Function loadReport() As Boolean
        Dim q = "SELECT emp_no as 'Emp No',name,feedback,rating1 as 'Food Quality',rating2 as 'Cleanliness',rating3 as 'Staff Behaviour',rating4 as 'Service',strftime('%d.%m.%Y %H:%M', datetime(time_stamp,'+330 Minute')) as 'Date' from canteen_feedback "


        Dim finalDT1 As New System.Data.DataTable()
        Dim finalDT2 As New System.Data.DataTable()
        finalDT1 = dbOp.getDBTable(q)
        finalDT2 = finalDT1
        'finalDT2.Columns.Add("SN")
        'Dim i = 1
        'Dim grp = ""
        'Dim tmpRow = finalDT2.NewRow
        'For Each nwRow In finalDT1.Rows
        '    tmpRow("SN") = i
        '    finalDT2.Rows.Add(tmpRow)
        '    i = i + 1
        'Next

        GridView1.DataSource = finalDT2
        GridView1.DataBind()


        Return True
    End Function


    Private Sub LinkButton1_Click(sender As Object, e As EventArgs) Handles LinkButton1.Click
        If GridView1.Rows.Count > 0 Then

            saveExcel()
        Else
            ' lblMsg.Text = "Please Upload MPP File First."
        End If
    End Sub
    Sub saveExcel()
        ' Change the Header Row back to white color
        GridView1.HeaderRow.Style.Add("background-color", "#FFFFFF")

        ' This loop is used to apply stlye to cells based on particular row
        For Each gvrow As GridViewRow In GridView1.Rows
            gvrow.BackColor = Drawing.Color.White



            'If gvrow.Cells(4).Text = "True" Then
            '    gvrow.BackColor = Drawing.Color.Yellow
            '    'For k As Integer = 0 To gvrow.Cells.Count - 1
            '    '    gvrow.Cells(k).Style.Add("background-color", "#EFF3FB")
            '    'Next
            'End If
        Next

        Response.ClearContent()

        Response.AddHeader("content-disposition", "attachment; filename=GridViewToExcel.xls")

        Response.ContentType = "application/excel"

        Dim sWriter As New System.IO.StringWriter()

        Dim hTextWriter As New HtmlTextWriter(sWriter)

        '  divSummary.RenderControl(hTextWriter)
        GridView1.RenderControl(hTextWriter)


        Response.Write(sWriter.ToString())

        Response.End()
        'lblMsg.Text = "Excel created"

        'GridView1.RenderControl(htw)

    End Sub





End Class
