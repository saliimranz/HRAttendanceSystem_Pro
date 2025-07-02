Partial Class Pages_ShiftAssignment
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            LoadEmployees()
            LoadShifts()
        End If
    End Sub

    Private Sub LoadEmployees()
        Dim employees = ShiftAssignmentDAL.GetEmployees()
        ddlEmployee.DataSource = employees
        ddlEmployee.DataTextField = "EmpName"
        ddlEmployee.DataValueField = "EmployeeID"
        ddlEmployee.DataBind()
    End Sub

    Private Sub LoadShifts()
        Dim shifts = ShiftAssignmentDAL.GetShifts()
        ddlShift.DataSource = shifts
        ddlShift.DataTextField = "ShiftName"
        ddlShift.DataValueField = "ShiftID"
        ddlShift.DataBind()
    End Sub

    Protected Sub btnAssign_Click(sender As Object, e As EventArgs) Handles btnAssign.Click
        Try
            Dim sa As New ShiftAssignment With {
                .EmployeeID = Integer.Parse(ddlEmployee.SelectedValue),
                .ShiftID = Integer.Parse(ddlShift.SelectedValue),
                .StartDate = Date.Parse(txtStartDate.Text),
                .EndDate = Date.Parse(txtEndDate.Text)
            }

            If ShiftAssignmentDAL.AssignShift(sa) Then
                lblMsg.Text = "Shift assigned successfully!"
            Else
                lblMsg.ForeColor = Drawing.Color.Red
                lblMsg.Text = "Failed to assign shift."
            End If
        Catch ex As Exception
            lblMsg.ForeColor = Drawing.Color.Red
            lblMsg.Text = "Error: " & ex.Message
        End Try
    End Sub
End Class
