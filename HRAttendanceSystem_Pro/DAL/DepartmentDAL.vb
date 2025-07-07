Imports System.Data.SqlClient

Public Class DepartmentDAL
    Public Shared Function AddDepartment(dept As Department) As Boolean
        Dim query As String = "INSERT INTO Department (DepartmentName, Description) VALUES (@Name, @Desc)"
        Using con = DBHelper.GetConnection()
            Using cmd As New SqlCommand(query, con)
                cmd.Parameters.AddWithValue("@Name", dept.DepartmentName)
                cmd.Parameters.AddWithValue("@Desc", dept.Description)
                Try
                    con.Open()
                    cmd.ExecuteNonQuery()
                    Return True
                Catch ex As Exception
                    ' Optional: Log error
                    Return False
                End Try
            End Using
        End Using
    End Function
End Class

