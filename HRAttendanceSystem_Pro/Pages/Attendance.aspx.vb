Imports System.Data.OleDb
Imports System.IO
Imports System.Reflection.Emit

Partial Class Pages_Attendance
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            LoadEmployees()
            divManual.Visible = False
            divExcel.Visible = False
        End If
    End Sub

    Protected Sub LoadEmployees()
        Dim employees = AttendanceDAL.GetEmployees()
        ddlEmployee.DataSource = employees
        ddlEmployee.DataTextField = "EmpName"
        ddlEmployee.DataValueField = "EmployeeID"
        ddlEmployee.DataBind()
    End Sub

    Protected Sub ddlType_SelectedIndexChanged(sender As Object, e As EventArgs)
        divManual.Visible = (ddlType.SelectedValue = "Manual")
        divExcel.Visible = (ddlType.SelectedValue = "Excel")
    End Sub

    Protected Sub ddlEmployee_SelectedIndexChanged(sender As Object, e As EventArgs)
        LoadShiftAssignments()
    End Sub

    Protected Sub txtDate_TextChanged(sender As Object, e As EventArgs)
        LoadShiftAssignments()
    End Sub

    Protected Sub btnSaveManual_Click(sender As Object, e As EventArgs)
        Try
            Dim att As New Attendance With {
                .ShiftAssignmentID = Integer.Parse(ddlShiftAssignment.SelectedValue),
                .AttDate = Date.Parse(txtDate.Text),
                .InTime = TimeSpan.Parse(Request.Form(txtIn.UniqueID)),
                .OutTime = TimeSpan.Parse(Request.Form(txtOut.UniqueID)),
                .Status = txtStatus.Text
            }

            If AttendanceDAL.AddAttendance(att) Then
                lblMsg.Text = "Manual Attendance Saved."
            Else
                lblMsg.ForeColor = Drawing.Color.Red
                lblMsg.Text = "Failed to save."
            End If
        Catch ex As Exception
            lblMsg.ForeColor = Drawing.Color.Red
            lblMsg.Text = "Error: " & ex.Message
        End Try
    End Sub

    Protected Sub btnUpload_Click(sender As Object, e As EventArgs)
        Try
            If fileUpload.HasFile Then
                Dim filePath = Server.MapPath("~/Uploads/" & Path.GetFileName(fileUpload.FileName))
                fileUpload.SaveAs(filePath)

                Dim connStr As String = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & filePath & ";Extended Properties='Excel 12.0 Xml;HDR=YES';"
                Dim validCount As Integer = 0
                Dim invalidEmpCodes As New List(Of String)

                Using conn As New OleDbConnection(connStr)
                    conn.Open()

                    ' Detect sheet name dynamically
                    Dim dtSchema As DataTable = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, Nothing)
                    If dtSchema.Rows.Count = 0 Then
                        lblMsg.Text = "No sheet found in Excel file."
                        Exit Sub
                    End If
                    Dim sheetName As String = dtSchema.Rows(0)("TABLE_NAME").ToString().Replace("'", "")
                    Dim cmd As New OleDbCommand("SELECT * FROM [" & sheetName & "]", conn)
                    Dim reader As OleDbDataReader = cmd.ExecuteReader()

                    If Not reader.HasRows Then
                        lblMsg.Text = "Excel sheet is empty!"
                        Exit Sub
                    End If

                    ' Get all employees once
                    Dim allEmployees = AttendanceDAL.GetEmployees()

                    While reader.Read()
                        Dim empCode = reader("EmpCode").ToString().Trim()
                        Dim emp = allEmployees.FirstOrDefault(Function(empItem) _
                            (If(empItem.EmpCode, "").Trim().ToLower() = empCode.ToLower()))

                        If emp Is Nothing Then
                            invalidEmpCodes.Add(empCode)
                            Continue While
                        End If

                        Dim attDate = Date.Parse(reader("Date").ToString())
                        Dim shiftAssignment = AttendanceDAL.GetShiftAssignmentsByDate(emp.EmployeeID, attDate).FirstOrDefault()

                        If shiftAssignment Is Nothing Then
                            invalidEmpCodes.Add(empCode & " (No shift on " & attDate.ToShortDateString() & ")")
                            Continue While
                        End If

                        Dim att As New Attendance With {
                            .ShiftAssignmentID = shiftAssignment.AssignmentID,
                            .AttDate = Date.Parse(reader("Date").ToString()),
                            .InTime = TimeSpan.Parse(reader("InTime").ToString()),
                            .OutTime = TimeSpan.Parse(reader("OutTime").ToString()),
                            .Status = reader("Status").ToString()
                        }

                        If AttendanceDAL.AddAttendance(att) Then
                            validCount += 1
                        End If
                    End While
                End Using

                If validCount = 0 Then
                    lblMsg.ForeColor = Drawing.Color.Red
                    lblMsg.Text = "No valid employees found. Please check employee codes in Excel!"
                ElseIf invalidEmpCodes.Count > 0 Then
                    lblMsg.ForeColor = Drawing.Color.DarkOrange
                    lblMsg.Text = $"Uploaded {validCount} records. Skipped invalid EmpCodes: {String.Join(", ", invalidEmpCodes.Distinct())}"
                Else
                    lblMsg.ForeColor = Drawing.Color.Green
                    lblMsg.Text = $"Excel upload successful. {validCount} attendance rows saved."
                End If
            End If
        Catch ex As Exception
            lblMsg.ForeColor = Drawing.Color.Red
            lblMsg.Text = "Error during Excel upload: " & ex.Message
        End Try
    End Sub

    Private Sub LoadShiftAssignments()
        ddlShiftAssignment.Items.Clear()

        If String.IsNullOrWhiteSpace(txtDate.Text) OrElse ddlEmployee.SelectedIndex = -1 Then
            Return
        End If

        Dim empId = Integer.Parse(ddlEmployee.SelectedValue)
        Dim attDate = Date.Parse(txtDate.Text)

        Dim assignments = AttendanceDAL.GetShiftAssignmentsByDate(empId, attDate)

        If assignments.Count > 0 Then
            ddlShiftAssignment.DataSource = assignments
            ddlShiftAssignment.DataTextField = "DisplayName" ' you’ll format this below
            ddlShiftAssignment.DataValueField = "AssignmentID"
            ddlShiftAssignment.DataBind()
        End If
    End Sub
End Class
