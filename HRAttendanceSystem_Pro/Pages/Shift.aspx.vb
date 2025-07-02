Partial Class Pages_Shift
    Inherits System.Web.UI.Page

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            Dim s As New Shift With {
                .ShiftName = txtShiftName.Text.Trim(),
                .StartTime = TimeSpan.Parse(Request.Form(txtStartTime.UniqueID)),
                .EndTime = TimeSpan.Parse(Request.Form(txtEndTime.UniqueID)),
                .GracePeriod = Integer.Parse(txtGrace.Text.Trim()),
                .BreakMinutes = Integer.Parse(txtBreak.Text.Trim())
            }

            If ShiftDAL.AddShift(s) Then
                lblMsg.Text = "Shift added successfully!"
            Else
                lblMsg.ForeColor = Drawing.Color.Red
                lblMsg.Text = "Failed to add shift."
            End If
        Catch ex As Exception
            lblMsg.ForeColor = Drawing.Color.Red
            lblMsg.Text = "Error: " & ex.Message
        End Try
    End Sub
End Class
