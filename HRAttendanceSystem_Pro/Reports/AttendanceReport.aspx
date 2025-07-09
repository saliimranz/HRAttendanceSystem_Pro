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
        <div id="reportContainer" style="margin-top:20px; border: 2px dashed red; background-color: #ffecec; padding: 10px;">
            <CR:CrystalReportViewer ID="crViewer" runat="server"
                AutoDataBind="false"
                Width="100%" 
                Height="1000px"
                ToolPanelView="None"
                HasExportButton="True"
                HasPrintButton="True"
                EnableDatabaseLogonPrompt="False"
                ReuseParameterValuesOnRefresh="False"
                EnableParameterPrompt="False"
            />
        </div>


        <asp:Label ID="lblMsg" runat="server" ForeColor="Red" />
    </form>
</body>
</html>
