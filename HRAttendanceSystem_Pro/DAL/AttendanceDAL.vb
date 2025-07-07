Imports System.Data.SqlClient

Public Class AttendanceDAL
    Public Shared Function AddAttendance(a As Attendance) As Boolean
        Dim query As String = "INSERT INTO Attendance (ShiftAssignmentID, AttDate, InTime, OutTime, Status)
                               VALUES (@EmpID, @Date, @In, @Out, @Status)"
        Using con = DBHelper.GetConnection()
            Using cmd As New SqlCommand(query, con)
                cmd.Parameters.AddWithValue("@EmpID", a.ShiftAssignmentID)
                cmd.Parameters.AddWithValue("@Date", a.AttDate)
                cmd.Parameters.AddWithValue("@In", a.InTime)
                cmd.Parameters.AddWithValue("@Out", a.OutTime)
                cmd.Parameters.AddWithValue("@Status", a.Status)

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

    Public Shared Function GetEmployees() As List(Of Employee)
        Dim list As New List(Of Employee)
        Dim query As String = "SELECT EmployeeID, EmpCode, EmpName FROM Employee"

        Using con = DBHelper.GetConnection()
            Using cmd As New SqlCommand(query, con)
                con.Open()
                Using reader = cmd.ExecuteReader()
                    While reader.Read()
                        list.Add(New Employee With {
                            .EmployeeID = reader("EmployeeID"),
                            .EmpCode = reader("EmpCode").ToString().Trim(),
                            .EmpName = reader("EmpName").ToString()
                        })
                    End While
                End Using
            End Using
        End Using

        Return list
    End Function

    Public Shared Function GetShiftAssignmentsByDate(empId As Integer, attDate As Date) As List(Of ShiftAssignment)
        Dim list As New List(Of ShiftAssignment)
        Dim query As String = "SELECT SA.AssignmentID, S.ShiftName, SA.StartDate, SA.EndDate 
                           FROM ShiftAssignment SA
                           JOIN Shift S ON SA.ShiftID = S.ShiftID
                           WHERE SA.EmployeeID = @EmpID AND @AttDate BETWEEN SA.StartDate AND SA.EndDate"

        Using con = DBHelper.GetConnection()
            Using cmd As New SqlCommand(query, con)
                cmd.Parameters.AddWithValue("@EmpID", empId)
                cmd.Parameters.AddWithValue("@AttDate", attDate)

                con.Open()
                Using reader = cmd.ExecuteReader()
                    While reader.Read()
                        list.Add(New ShiftAssignment With {
                            .AssignmentID = reader("AssignmentID"),
                            .DisplayName = $"{reader("ShiftName")} ({CDate(reader("StartDate")).ToShortDateString()} - {CDate(reader("EndDate")).ToShortDateString()})"
                        })
                    End While
                End Using
            End Using
        End Using
        Return list
    End Function

    Public Shared Function AddManualOT(ot As Overtime) As Boolean
        Dim query = "INSERT INTO Overtime (ShiftAssignmentID, AttDate, OT_Hours, OT_Reason, OT_Type)
                 VALUES (@ShiftAssignmentID, @AttDate, @OT_Hours, @OT_Reason, @OT_Type)"

        Using con = DBHelper.GetConnection()
            Using cmd As New SqlCommand(query, con)
                cmd.Parameters.AddWithValue("@ShiftAssignmentID", ot.ShiftAssignmentID)
                cmd.Parameters.AddWithValue("@AttDate", ot.AttDate)
                cmd.Parameters.AddWithValue("@OT_Hours", ot.OT_Hours)
                cmd.Parameters.AddWithValue("@OT_Reason", ot.OT_Reason)
                cmd.Parameters.AddWithValue("@OT_Type", ot.OT_Type)

                con.Open()
                Return cmd.ExecuteNonQuery() > 0
            End Using
        End Using
    End Function

    Public Shared Function GetAutoOTPreview(fromDate As Date, toDate As Date) As DataTable
        Dim query As String = "
        SELECT 
        E.EmpName,
        A.AttDate,
        A.OutTime,
        S.EndTime AS ShiftEndTime,
        SA.AssignmentID,
        CAST(DATEDIFF(MINUTE, S.EndTime, A.OutTime) / 60.0 AS DECIMAL(5,2)) AS CalculatedOT
    FROM Attendance A
    INNER JOIN ShiftAssignment SA ON A.ShiftAssignmentID = SA.AssignmentID
    INNER JOIN Shift S ON SA.ShiftID = S.ShiftID
    INNER JOIN Employee E ON SA.EmployeeID = E.EmployeeID
    LEFT JOIN Overtime OT ON OT.ShiftAssignmentID = SA.AssignmentID AND OT.AttDate = A.AttDate
    WHERE A.AttDate BETWEEN @from AND @to
    AND A.OutTime > S.EndTime
    AND OT.OTID IS NULL"

        Dim dt As New DataTable()
        Using con = DBHelper.GetConnection()
            Using cmd As New SqlCommand(query, con)
                cmd.Parameters.AddWithValue("@from", fromDate)
                cmd.Parameters.AddWithValue("@to", toDate)

                Using da As New SqlDataAdapter(cmd)
                    da.Fill(dt)
                End Using
            End Using
        End Using
        Return dt
    End Function

    Public Shared Function GetDepartments() As List(Of Department)
        Dim list As New List(Of Department)()
        Using con = DBHelper.GetConnection()
            Dim query = "SELECT DepartmentID, DepartmentName FROM Department"
            Using cmd As New SqlCommand(query, con)
                con.Open()
                Using reader = cmd.ExecuteReader()
                    While reader.Read()
                        list.Add(New Department With {
                        .DepartmentID = Convert.ToInt32(reader("DepartmentID")),
                        .DepartmentName = reader("DepartmentName").ToString()
                    })
                    End While
                End Using
            End Using
        End Using
        Return list
    End Function

    Public Shared Function GetAttendanceReport(fromDate As Date, toDate As Date, empId As Integer?, deptId As Integer?) As DataTable
        Dim dt As New DataTable()
        Dim query As String = "
        SELECT 
            A.AttDate,
            E.EmpName,
            D.DepartmentName,
            A.InTime,
            A.OutTime,
            A.Status,
            S.StartTime AS ShiftStart,
            S.EndTime AS ShiftEnd,
            CASE 
                WHEN A.Status = 'Absent' THEN 'Absent'
                WHEN A.InTime > DATEADD(MINUTE, S.GracePeriod, S.StartTime) THEN 'Late'
                ELSE 'Present'
            END AS AttendanceStatus
        FROM Attendance A
        INNER JOIN ShiftAssignment SA ON A.ShiftAssignmentID = SA.AssignmentID
        INNER JOIN Shift S ON SA.ShiftID = S.ShiftID
        INNER JOIN Employee E ON SA.EmployeeID = E.EmployeeID
        INNER JOIN Department D ON E.DepartmentID = D.DepartmentID
        WHERE A.AttDate BETWEEN @from AND @to
          AND (@empId IS NULL OR E.EmployeeID = @empId)
          AND (@deptId IS NULL OR D.DepartmentID = @deptId)
        ORDER BY A.AttDate, E.EmpName"

        Using con = DBHelper.GetConnection()
            Using cmd As New SqlCommand(query, con)
                cmd.Parameters.AddWithValue("@from", fromDate)
                cmd.Parameters.AddWithValue("@to", toDate)
                cmd.Parameters.AddWithValue("@empId", If(empId.HasValue, empId.Value, DBNull.Value))
                cmd.Parameters.AddWithValue("@deptId", If(deptId.HasValue, deptId.Value, DBNull.Value))

                Using da As New SqlDataAdapter(cmd)
                    da.Fill(dt)
                End Using
            End Using
        End Using

        Return dt
    End Function

End Class
