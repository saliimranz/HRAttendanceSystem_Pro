Imports System.IO
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared


Partial Class Pages_AttendanceReport
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            LoadDepartments()
            LoadEmployees()
        Else
            If Session("AttendanceRpt") IsNot Nothing Then
                System.Diagnostics.Debug.WriteLine("Crystal Viewer has report source assigned.")
                crViewer.ReportSource = Session("AttendanceRpt")
                crViewer.DataBind()
            Else
                System.Diagnostics.Debug.WriteLine("❌ Crystal Viewer has NO report source.")
            End If
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
            crViewer.ReportSource = Nothing
            crViewer.Visible = False

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

            System.Diagnostics.Debug.WriteLine("Rows found: " & dt.Rows.Count)
            System.Diagnostics.Debug.WriteLine("== Columns in dt ==")
            ' Print column names
            For Each col As DataColumn In dt.Columns
                System.Diagnostics.Debug.WriteLine("Column: " & col.ColumnName & " [" & col.DataType.ToString() & "]")
            Next

            Dim rowCount = 0
            For Each row As DataRow In dt.Rows
                Dim rowStr As String = ""
                For Each col As DataColumn In dt.Columns
                    rowStr &= $"{col.ColumnName}={row(col)} | "
                Next
                System.Diagnostics.Debug.WriteLine(rowStr)
                rowCount += 1
                If rowCount >= 5 Then Exit For
            Next

            dt.TableName = "AttendanceReportDT"

            Dim path = Server.MapPath("~/Reports/AttendanceReport.rpt")
            System.Diagnostics.Debug.WriteLine("Report Path: " & path)

            If Not File.Exists(path) Then
                lblMsg.Text = "Report file not found at path: " & path
                Exit Sub
            End If

            Dim rpt As New ReportDocument()
            Try
                rpt.Load(path)
            Catch ex As Exception
                lblMsg.Text = "Failed to load report: " & ex.Message
                Exit Sub
            End Try

            For Each table As CrystalDecisions.CrystalReports.Engine.Table In rpt.Database.Tables
                System.Diagnostics.Debug.WriteLine("Table Name: " & table.Name)
                System.Diagnostics.Debug.WriteLine("Table Location: " & table.Location)
                System.Diagnostics.Debug.WriteLine("Class Name: " & table.GetType().Name)

                Dim logonInfo As TableLogOnInfo = table.LogOnInfo
                System.Diagnostics.Debug.WriteLine("Connection Info: " & logonInfo.ConnectionInfo.DatabaseName)
            Next


            rpt.SetDataSource(dt)

            crViewer.ReportSource = rpt
            Session("AttendanceRpt") = rpt
            crViewer.Visible = True
            crViewer.DataBind()
        Catch ex As Exception
            lblMsg.Text = "Error generating report: " & ex.Message
        End Try
    End Sub
End Class
