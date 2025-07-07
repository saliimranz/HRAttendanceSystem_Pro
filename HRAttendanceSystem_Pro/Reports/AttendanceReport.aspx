<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="AttendanceReport.aspx.vb" Inherits="HRAttendanceSystem_Pro.Pages_AttendanceReport" %>
<%@ Register Assembly="CrystalDecisions.Web" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Attendance Report</title>
</head>
<body>
    <form id="form1" runat="server">
        <h3>Attendance Report</h3>

        <div>
            From:
            <asp:TextBox ID="txtFrom" runat="server" TextMode="Date" />
            To:
            <asp:TextBox ID="txtTo" runat="server" TextMode="Date" />
            <br /><br />
            Department:
            <asp:DropDownList ID="ddlDept" runat="server" />
            Employee:
            <asp:DropDownList ID="ddlEmployee" runat="server" />
            <br /><br />
            <asp:Button ID="btnGenerate" runat="server" Text="Generate Report" OnClick="btnGenerate_Click" />
        </div>

        <br />
        <CR:CrystalReportViewer ID="crViewer" runat="server" AutoDataBind="true" Width="100%" />
        <asp:Label ID="lblMsg" runat="server" ForeColor="Red" />
    </form>
</body>
</html>
