Imports dbOp
Imports System.IO
Imports System.Text.RegularExpressions
Imports System.Data

Partial Class import
    Inherits System.Web.UI.Page

    Private Sub btnLoad_Click(sender As Object, e As EventArgs) Handles btnLoad.Click

        Dim fu As FileUpload = FileUpload1
        Dim i = 0
        If fu.HasFile Then
            executeDB("delete from employee")
            Dim reader As New StreamReader(fu.FileContent)
            Do

                ' do your coding 
                'Loop trough txt file and add lines to ListBox1  

                Dim textLine As String = reader.ReadLine()
                Dim mystring As String = textLine '"<br>Terms of Service<br></br>Developers<br>"

                Dim pattern1 As String = "(?<=<statement>)(.*?)(?=</statement>)"

                Dim m1 As MatchCollection = Regex.Matches(mystring, pattern1)
                Try
                    '  divMsg.InnerHtml = divMsg.InnerHtml & m1(0).ToString
                    executeDB(m1(0).ToString)
                    i = i + 1
                Catch ex As Exception
                    '' non pattern file
                End Try

            Loop While reader.Peek() <> -1
            reader.Close()
            divMsg.InnerHtml = i.ToString + " Records Inserted"
        End If
    End Sub
    Private Sub btnReportDownload_Click(sender As Object, e As EventArgs) Handles btnReportDownload.Click
        Dim mydt = getDBTable("select * from employee")
        gvReport.DataSource = mydt
        gvReport.DataBind()
        saveExcel(mydt)
    End Sub
    Sub saveExcel(mydt As datatable)

        Response.ClearContent()
        Response.Clear()
        Response.ContentType = "text/plain"
        Response.AddHeader("Content-Disposition", "attachment; filename=employee.csv;")

        Dim sb As New StringBuilder()

        Dim columnNames As String() = mydt.Columns.Cast(Of DataColumn)().[Select](Function(column) column.ColumnName).ToArray()
        sb.AppendLine(String.Join("#", columnNames))

        For Each row As DataRow In mydt.Rows
            Dim fields As String() = row.ItemArray.[Select](Function(field) field.ToString()).ToArray()
            sb.AppendLine(String.Join("#", fields))
        Next

        ' the most easy way as you have type it
        Response.Write(sb.ToString())


        Response.Flush()
        Response.[End]()



    End Sub
    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        Return
    End Sub

    Private Sub saplogin_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If Not Request.Params("mode") Is Nothing Then

            End If
        End If

    End Sub
End Class
