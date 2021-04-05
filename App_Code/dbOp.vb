Imports Microsoft.VisualBasic
Imports System.Data.SQLite
Imports System.Data
Imports System.Security.Cryptography
Imports System.IO
Public Class dbOp
    Public Shared Function insertTableinDB(ByVal dt As DataTable, ByVal mytablename As String, Optional ByVal myConn As String = "vindb") As String

        'Create Connection String
        Dim myquery = "select * from " & mytablename & " limit 1"
        Using connection As New SQLiteConnection(System.Configuration.ConfigurationManager.ConnectionStrings(myConn).ConnectionString)

            Try
                'connection.Close()
                connection.Open()
                '' sqlComm = New SQLiteCommand("select * from gamemaster", connection)
                Dim da As New SQLiteDataAdapter(myquery, connection)
                Dim dcmd As SQLiteCommandBuilder = New SQLiteCommandBuilder(da)
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey
                da.Update(dt)

                ''  sqlComm.Dispose()
                da.Dispose()
                connection.Close()
                insertTableinDB = "ok"


            Catch exp As Exception
                'lbldebug.Text = exp.Message
                connection.Close()
                insertTableinDB = "Error:" + exp.Message

            End Try
            '' Close Database connection
            '' and Dispose Database objects
        End Using

    End Function
    Public Shared Function getDBSinglecommaSepearated(ByVal myQuery As String, Optional ByVal myConn As String = "vindb") As String
        ''this function will return single value from table by concatenating rows with hash
        Using connection As New SQLiteConnection(System.Configuration.ConfigurationManager.ConnectionStrings(myConn).ConnectionString)
            ' connection.Close()
            connection.Open()
            Dim result As String = ""
            Dim sqlComm As SQLiteCommand
            Dim sqlReader As SQLiteDataReader
            Dim dt As New DataTable()
            Dim j As Integer = 0

            Try
                sqlComm = New SQLiteCommand(myQuery, connection)
                sqlReader = sqlComm.ExecuteReader()
                dt.Load(sqlReader)
                Dim i = dt.Rows.Count

                sqlReader.Close()
                sqlComm.Dispose()
                If i = 0 Then
                    connection.Close()
                    Return "NA"

                End If

                While j < i

                    result = result & dt.Rows(j).Item(0).ToString() & ","

                    j = j + 1
                End While

                connection.Close()
                Return result


            Catch e As Exception
                'lblDebug.text = e.Message
                connection.Close()
                result = e.Message
                Return result
            End Try
            'connection.Close()
        End Using
    End Function
    Public Shared Function getDBsingle(ByVal mysql As String, Optional ByVal myConn As String = "vindb") As String
        ''this function will return single value from table according to myQuery
        'Create Connection String
        Using connection As New SQLiteConnection(System.Configuration.ConfigurationManager.ConnectionStrings(myConn).ConnectionString)
            Dim sqlComm As SQLiteCommand
            Dim sqlReader As SQLiteDataReader
            Dim result As String
            Dim dt As New DataTable()
            Dim dataTableRowCount As Integer
            Try
                connection.Open()
                sqlComm = New SQLiteCommand(mysql, connection)
                sqlReader = sqlComm.ExecuteReader()
                dt.Load(sqlReader)
                dataTableRowCount = dt.Rows.Count
                sqlReader.Close()
                sqlComm.Dispose()
                If dataTableRowCount = 1 Then
                    result = dt.Rows(0).Item(0).ToString()
                    connection.Close()
                    Return result
                Else
                    connection.Close()
                    Return "Error:Too many Records Found"
                End If

            Catch e As Exception
                'lblDebug.text = e.Message
                connection.Close()
                Return "Error" + e.Message
            End Try
            connection.Close()
        End Using
    End Function
    Public Shared Function recentUploads(ByVal myQuery As String, ByVal type As String) As String
        ''this function will return single value from table according to myQuery
        Dim more = "<li class='vinHidden'><a href=" & Chr(34) & "#" & Chr(34) & " style='text-decoration: none; z-index:2000;' onclick=" & Chr(34) & "javascript:vinModal('" & type & "','%');return false;" & Chr(34) & "> +More..</a></li>"
        Using connection As New SQLiteConnection(System.Configuration.ConfigurationManager.ConnectionStrings("vindb").ConnectionString)
            Dim sqlComm As SQLiteCommand
            Dim sqlReader As SQLiteDataReader
            connection.Open()
            Dim result As String = "<ul>"

            Dim dt As New DataTable()
            Dim j As Integer = 0
            Dim proj As String
            Try
                sqlComm = New SQLiteCommand(myQuery, connection)
                sqlReader = sqlComm.ExecuteReader()
                dt.Load(sqlReader)
                Dim i = dt.Rows.Count

                sqlReader.Close()
                sqlComm.Dispose()
                If i = 0 Then
                    connection.Close()
                    'Return "Too many Records Found"

                End If

                While j < i

                    'type  project  subject  filename

                    proj = Uri.EscapeDataString(dt.Rows(j).Item(1).ToString())
                    If dt.Rows(j).Item(0).ToString() = "Review" Then  'for vendor
                        proj = "Vendor/" & proj
                    End If

                    result = result & "<li class='vinHidden'><a href=/larr/upload/" & proj & "/" & dt.Rows(j).Item(0).ToString() & "/" & Uri.EscapeDataString(dt.Rows(j).Item(3).ToString()) & " onclick=fileCount('" & Uri.EscapeDataString(dt.Rows(j).Item(3).ToString()) & "') target=_blank style='text-decoration: none;color: #000;'><img src=images/" & Right(dt.Rows(j).Item(3).ToString(), 3) & ".gif border=0 align=middle height=13px onerror=" & Chr(34) & "this.src='images/file.gif';" & Chr(34) & "/> " & dt.Rows(j).Item(2).ToString.Replace("CMG", "PPM") & " </a></li>"

                    j = j + 1
                End While

                connection.Close()
                Return result & more & "</ul>"


            Catch e As Exception
                'lblDebug.text = e.Message
                connection.Close()
                result = e.Message
                Return result
            End Try
            'connection.Close()
        End Using
    End Function
    Public Shared Function executeDB(ByVal mysql As String, Optional ByVal myConn As String = "vindb") As String

        'Create Connection String
        Using connection As New SQLiteConnection(System.Configuration.ConfigurationManager.ConnectionStrings(myConn).ConnectionString)
            Dim sqlComm As SQLiteCommand
            Dim sqlReader As SQLiteDataReader

            Try
                'connection.Close()
                connection.Open()
                sqlComm = New SQLiteCommand(mysql, connection)
                sqlReader = sqlComm.ExecuteReader()
                'Add Insert Statement
                sqlComm.Dispose()

                connection.Close()
                executeDB = "ok"


            Catch exp As Exception
                'lbldebug.Text = exp.Message
                connection.Close()
                executeDB = "Error:" + exp.Message

            End Try
            'Close Database connection
            'and Dispose Database objects
        End Using

    End Function
    Public Shared Function getDBTable(ByVal myQuery As String, Optional ByVal myConn As String = "vindb") As DataTable
        ''this function will return DataTable from table according to myQuery
        Using connection As New SQLiteConnection(System.Configuration.ConfigurationManager.ConnectionStrings(myConn).ConnectionString)
            ' connection.Close()
            connection.Open()

            Dim sqlComm As SQLiteCommand
            Dim sqlReader As SQLiteDataReader
            Dim dt As New DataTable()
            Dim dataTableRowCount As Integer
            Try
                sqlComm = New SQLiteCommand(myQuery, connection)
                sqlReader = sqlComm.ExecuteReader()
                dt.Load(sqlReader)
                dataTableRowCount = dt.Rows.Count
                sqlReader.Close()
                sqlComm.Dispose()
                If Not dt Is Nothing Then

                    connection.Close()
                    Return dt
                Else
                    connection.Close()
                    Return dt.NewRow("Too many Records Found")
                End If

            Catch e As Exception
                'lblDebug.text = e.Message
                dt.Columns.Add("Error")
                Dim tmprow = dt.NewRow
                connection.Close()
                tmprow(0) = "Error: " & e.Message & myQuery
                dt.Rows.Add(tmprow)
                ' Return dt.NewRow("Error in getdatatable")
                Return dt
            End Try
            connection.Close()
        End Using
    End Function
    Public Shared Function cookieEncrypt(raw As String) As String
        Using csp = New AesCryptoServiceProvider()
            Dim e As ICryptoTransform = GetCryptoTransform(csp, True)
            Dim inputBuffer As Byte() = Encoding.UTF8.GetBytes(raw)
            Dim output As Byte() = e.TransformFinalBlock(inputBuffer, 0, inputBuffer.Length)

            Dim encrypted As String = Convert.ToBase64String(output)

            Return encrypted
        End Using
    End Function

    Public Shared Function cookieDecrypt(encrypted As String) As String
        Using csp = New AesCryptoServiceProvider()
            Dim d = GetCryptoTransform(csp, False)
            Dim output As Byte() = Convert.FromBase64String(encrypted)
            Dim decryptedOutput As Byte() = d.TransformFinalBlock(output, 0, output.Length)

            Dim decypted As String = Encoding.UTF8.GetString(decryptedOutput)
            Return decypted
        End Using
    End Function

    Private Shared Function GetCryptoTransform(csp As AesCryptoServiceProvider, encrypting As Boolean) As ICryptoTransform
        csp.Mode = CipherMode.CBC
        csp.Padding = PaddingMode.PKCS7
        Dim passWord = "EPSSO@TGRE545815SKJ"
        Dim salt = "thisisthes@ltforpropertyreturn@pplication"

        'a random Init. Vector. just for testing
        Dim iv As [String] = "e675f725e675f725"

        Dim spec = New Rfc2898DeriveBytes(Encoding.UTF8.GetBytes(passWord), Encoding.UTF8.GetBytes(salt), 65536)
        Dim key As Byte() = spec.GetBytes(16)


        csp.IV = Encoding.UTF8.GetBytes(iv)
        csp.Key = key
        If encrypting Then
            Return csp.CreateEncryptor()
        End If
        Return csp.CreateDecryptor()
    End Function
    Public Shared Function GetRandom(ByVal Min As Integer, ByVal Max As Integer) As Integer
        ' by making Generator static, we preserve the same instance '
        ' (i.e., do not create new instances with the same seed over and over) '
        ' between calls '
        Static Generator As System.Random = New System.Random()
        Return Generator.Next(Min, Max)
    End Function
End Class
