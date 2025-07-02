Imports System.Data.SqlClient
Imports System.Configuration


Public Class DBHelper
    Public Shared Function GetConnection() As SqlConnection
        Return New SqlConnection(ConfigurationManager.ConnectionStrings("DBCS").ConnectionString)
    End Function
End Class

