Partial Class Pages_Department
    Inherits System.Web.UI.Page

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim dept As New Department With {
            .DepartmentName = txtDeptName.Text.Trim(),
            .Description = txtDesc.Text.Trim()
        }

        If DepartmentDAL.AddDepartment(dept) Then
            lblMsg.Text = "Department added successfully!"
        Else
            lblMsg.ForeColor = Drawing.Color.Red
            lblMsg.Text = "Failed to add department."
        End If
    End Sub
End Class