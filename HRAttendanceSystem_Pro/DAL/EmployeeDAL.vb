Imports System.Data.SqlClient

Public Class EmployeeDAL
    Public Shared Function AddEmployee(emp As Employee) As Boolean
        Dim query As String = "INSERT INTO Employee (EmpCode, EmpName, Contact, Designation, DepartmentID) 
                               VALUES (@Code, @Name, @Contact, @Desig, @DeptID)"
        Using con = DBHelper.GetConnection()
            Using cmd As New SqlCommand(query, con)
                cmd.Parameters.AddWithValue("@Code", emp.EmpCode)
                cmd.Parameters.AddWithValue("@Name", emp.EmpName)
                cmd.Parameters.AddWithValue("@Contact", emp.Contact)
                cmd.Parameters.AddWithValue("@Desig", emp.Designation)
                cmd.Parameters.AddWithValue("@DeptID", emp.DepartmentID)

                Try
                    con.Open()
                    cmd.ExecuteNonQuery()
                    Return True
                Catch ex As Exception
                    Return False
                End Try
            End Using
        End Using
    End Function

    Public Shared Function GetDepartments() As List(Of Department)
        Dim deptList As New List(Of Department)
        Dim query As String = "SELECT DepartmentID, DepartmentName FROM Department"

        Using con = DBHelper.GetConnection()
            Using cmd As New SqlCommand(query, con)
                con.Open()
                Using reader = cmd.ExecuteReader()
                    While reader.Read()
                        deptList.Add(New Department With {
                            .DepartmentID = reader("DepartmentID"),
                            .DepartmentName = reader("DepartmentName").ToString()
                        })
                    End While
                End Using
            End Using
        End Using

        Return deptList
    End Function
End Class
