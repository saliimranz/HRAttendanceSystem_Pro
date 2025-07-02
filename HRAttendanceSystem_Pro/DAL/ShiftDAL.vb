Imports System.Data.SqlClient

Public Class ShiftDAL
    Public Shared Function AddShift(s As Shift) As Boolean
        Dim query As String = "INSERT INTO Shift (ShiftName, StartTime, EndTime, GracePeriod, BreakMinutes) 
                               VALUES (@Name, @StartTime, @EndTime, @Grace, @Break)"

        Using con = DBHelper.GetConnection()
            Using cmd As New SqlCommand(query, con)
                cmd.Parameters.AddWithValue("@Name", s.ShiftName)
                cmd.Parameters.AddWithValue("@StartTime", s.StartTime)
                cmd.Parameters.AddWithValue("@EndTime", s.EndTime)
                cmd.Parameters.AddWithValue("@Grace", s.GracePeriod)
                cmd.Parameters.AddWithValue("@Break", s.BreakMinutes)

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
End Class
