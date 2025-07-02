Imports System.Data.SqlClient

Public Class ShiftAssignmentDAL
    Public Shared Function AssignShift(sa As ShiftAssignment) As Boolean
        Dim query As String = "INSERT INTO ShiftAssignment (EmployeeID, ShiftID, StartDate, EndDate)
                               VALUES (@EmpID, @ShiftID, @StartDate, @EndDate)"

        Using con = DBHelper.GetConnection()
            Using cmd As New SqlCommand(query, con)
                cmd.Parameters.AddWithValue("@EmpID", sa.EmployeeID)
                cmd.Parameters.AddWithValue("@ShiftID", sa.ShiftID)
                cmd.Parameters.AddWithValue("@StartDate", sa.StartDate)
                cmd.Parameters.AddWithValue("@EndDate", sa.EndDate)

                Try
                    con.Open()
                    cmd.ExecuteNonQuery()
                    Return True
                Catch ex As Exception
                    ' Optional: log error
                    Return False
                End Try
            End Using
        End Using
    End Function

    Public Shared Function GetEmployees() As List(Of Employee)
        Dim list As New List(Of Employee)
        Dim query As String = "SELECT EmployeeID, EmpName FROM Employee"

        Using con = DBHelper.GetConnection()
            Using cmd As New SqlCommand(query, con)
                con.Open()
                Using reader = cmd.ExecuteReader()
                    While reader.Read()
                        list.Add(New Employee With {
                            .EmployeeID = reader("EmployeeID"),
                            .EmpName = reader("EmpName").ToString()
                        })
                    End While
                End Using
            End Using
        End Using

        Return list
    End Function

    Public Shared Function GetShifts() As List(Of Shift)
        Dim list As New List(Of Shift)
        Dim query As String = "SELECT ShiftID, ShiftName FROM Shift"

        Using con = DBHelper.GetConnection()
            Using cmd As New SqlCommand(query, con)
                con.Open()
                Using reader = cmd.ExecuteReader()
                    While reader.Read()
                        list.Add(New Shift With {
                            .ShiftID = reader("ShiftID"),
                            .ShiftName = reader("ShiftName").ToString()
                        })
                    End While
                End Using
            End Using
        End Using

        Return list
    End Function
End Class
