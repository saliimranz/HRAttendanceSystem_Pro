Imports System.Data

Partial Class Pages_AutoOvertime
    Inherits System.Web.UI.Page

    Protected Sub btnPreview_Click(sender As Object, e As EventArgs)
        Try
            Dim fromDate As Date = Date.Parse(txtFrom.Text)
            Dim toDate As Date = Date.Parse(txtTo.Text)

            Dim data = AttendanceDAL.GetAutoOTPreview(fromDate, toDate)

            ' Debug: print columns
            For Each col As DataColumn In data.Columns
                System.Diagnostics.Debug.WriteLine("Column: " & col.ColumnName)
            Next

            ViewState("OTPreviewData") = data
            gvOT.DataSource = data
            gvOT.DataBind()

        Catch ex As Exception
            lblMsg.ForeColor = Drawing.Color.Red
            lblMsg.Text = "Error in preview: " & ex.Message
        End Try
    End Sub

    Protected Sub btnSaveOT_Click(sender As Object, e As EventArgs)
        Dim savedCount = 0

        For Each row As GridViewRow In gvOT.Rows
            Dim chk As CheckBox = CType(row.FindControl("chkSelect"), CheckBox)
            If chk IsNot Nothing AndAlso chk.Checked Then
                Dim assignmentID As Integer = Convert.ToInt32(gvOT.DataKeys(row.RowIndex).Value)
                System.Diagnostics.Debug.WriteLine("AssignmentID via DataKey: " & assignmentID)

                Dim attDate = Date.Parse(row.Cells(2).Text)
                Dim hours = Decimal.Parse(row.Cells(5).Text)
                Dim reason = CType(row.FindControl("txtReason"), TextBox).Text

                Dim ot As New Overtime With {
                    .ShiftAssignmentID = assignmentID,
                    .AttDate = attDate,
                    .OT_Hours = hours,
                    .OT_Reason = reason,
                    .OT_Type = "Auto"
                }

                If AttendanceDAL.AddManualOT(ot) Then
                    savedCount += 1
                End If
            End If
        Next

        lblMsg.ForeColor = Drawing.Color.Green
        lblMsg.Text = $"{savedCount} OT rows saved successfully!"
    End Sub

    'Protected Sub gvOT_RowDataBound(sender As Object, e As GridViewRowEventArgs)
    'If e.Row.RowType = DataControlRowType.DataRow Then
    'Dim hdn As HiddenField = CType(e.Row.FindControl("hdnAssignmentID"), HiddenField)
    '       System.Diagnostics.Debug.WriteLine("RowDataBound - AssignmentID = " & hdn.Value)
    'End If
    'End Sub
End Class
