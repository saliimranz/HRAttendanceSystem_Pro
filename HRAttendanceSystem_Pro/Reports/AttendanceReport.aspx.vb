Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared


Partial Class Pages_AttendanceReport
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            LoadDepartments()
            LoadEmployees()
        End If
    End Sub

    Private Sub LoadDepartments()
        Dim depts = AttendanceDAL.GetDepartments()
        ddlDept.DataSource = depts
        ddlDept.DataTextField = "DepartmentName"
        ddlDept.DataValueField = "DepartmentID"
        ddlDept.DataBind()
        ddlDept.Items.Insert(0, New ListItem("-- All --", ""))
    End Sub

    Private Sub LoadEmployees()
        Dim emps = AttendanceDAL.GetEmployees()
        ddlEmployee.DataSource = emps
        ddlEmployee.DataTextField = "EmpName"
        ddlEmployee.DataValueField = "EmployeeID"
        ddlEmployee.DataBind()
        ddlEmployee.Items.Insert(0, New ListItem("-- All --", ""))
    End Sub

    Protected Sub btnGenerate_Click(sender As Object, e As EventArgs)
        Try
            Dim fromDate As Date = Date.Parse(txtFrom.Text)
            Dim toDate As Date = Date.Parse(txtTo.Text)
            Dim empId As Integer? = If(String.IsNullOrWhiteSpace(ddlEmployee.SelectedValue), CType(Nothing, Integer?), Convert.ToInt32(ddlEmployee.SelectedValue))
            Dim deptId As Integer? = If(String.IsNullOrWhiteSpace(ddlDept.SelectedValue), CType(Nothing, Integer?), Convert.ToInt32(ddlDept.SelectedValue))

            Dim dt = AttendanceDAL.GetAttendanceReport(fromDate, toDate, empId, deptId)

            If dt.Rows.Count = 0 Then
                lblMsg.Text = "No data found for selected filters."
                crViewer.ReportSource = Nothing
                Exit Sub
            End If

            Dim rpt As New ReportDocument()
            rpt.Load(Server.MapPath("~/Reports/AttendanceReport.rpt"))
            rpt.SetDataSource(dt)

            crViewer.ReportSource = rpt
        Catch ex As Exception
            lblMsg.Text = "Error generating report: " & ex.Message
        End Try
    End Sub
End Class
