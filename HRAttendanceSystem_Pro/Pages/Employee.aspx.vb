Partial Class Pages_Employee
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            LoadDepartments()
        End If
    End Sub

    Private Sub LoadDepartments()
        Dim departments = EmployeeDAL.GetDepartments()
        ddlDepartment.DataSource = departments
        ddlDepartment.DataTextField = "DepartmentName"
        ddlDepartment.DataValueField = "DepartmentID"
        ddlDepartment.DataBind()
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            Dim emp As New Employee With {
                .EmpCode = txtEmpCode.Text.Trim(),
                .EmpName = txtName.Text.Trim(),
                .Contact = txtContact.Text.Trim(),
                .Designation = txtDesignation.Text.Trim(),
                .DepartmentID = Integer.Parse(ddlDepartment.SelectedValue)
            }

            If EmployeeDAL.AddEmployee(emp) Then
                lblMsg.Text = "Employee added successfully!"
            Else
                lblMsg.ForeColor = Drawing.Color.Red
                lblMsg.Text = "Failed to add employee."
            End If
        Catch ex As Exception
            lblMsg.ForeColor = Drawing.Color.Red
            lblMsg.Text = "Error: " & ex.Message
        End Try
    End Sub
End Class
