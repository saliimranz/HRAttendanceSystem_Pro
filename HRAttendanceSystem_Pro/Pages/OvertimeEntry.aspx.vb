Public Class Pages_OvertimeEntry
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            LoadEmployees()
        End If
    End Sub

    Private Sub LoadEmployees()
        Dim employees = AttendanceDAL.GetEmployees()
        ddlEmployee.DataSource = employees
        ddlEmployee.DataTextField = "EmpName"
        ddlEmployee.DataValueField = "EmployeeID"
        ddlEmployee.DataBind()
    End Sub

    Protected Sub ddlEmployee_SelectedIndexChanged(sender As Object, e As EventArgs)
        LoadShiftAssignments()
    End Sub

    Protected Sub txtDate_TextChanged(sender As Object, e As EventArgs)
        LoadShiftAssignments()
    End Sub

    Private Sub LoadShiftAssignments()
        ddlShiftAssignment.Items.Clear()
        If ddlEmployee.SelectedIndex = -1 OrElse String.IsNullOrWhiteSpace(txtDate.Text) Then Return

        Dim empId = Integer.Parse(ddlEmployee.SelectedValue)
        Dim dateVal = Date.Parse(txtDate.Text)

        Dim shifts = AttendanceDAL.GetShiftAssignmentsByDate(empId, dateVal)
        ddlShiftAssignment.DataSource = shifts
        ddlShiftAssignment.DataTextField = "DisplayName"
        ddlShiftAssignment.DataValueField = "AssignmentID"
        ddlShiftAssignment.DataBind()
    End Sub

    Protected Sub btnSaveOT_Click(sender As Object, e As EventArgs)
        Try
            Dim ot As New Overtime With {
                .ShiftAssignmentID = Integer.Parse(ddlShiftAssignment.SelectedValue),
                .AttDate = Date.Parse(txtDate.Text),
                .OT_Hours = Decimal.Parse(txtHours.Text),
                .OT_Reason = txtReason.Text,
                .OT_Type = "Manual"
            }

            If AttendanceDAL.AddManualOT(ot) Then
                lblMsg.ForeColor = Drawing.Color.Green
                lblMsg.Text = "OT Saved!"
            Else
                lblMsg.ForeColor = Drawing.Color.Red
                lblMsg.Text = "Failed to save OT!"
            End If
        Catch ex As Exception
            lblMsg.ForeColor = Drawing.Color.Red
            lblMsg.Text = "Error: " & ex.Message
        End Try
    End Sub

End Class