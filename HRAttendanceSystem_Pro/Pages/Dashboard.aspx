<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Dashboard.aspx.vb" Inherits="HRAttendanceSystem_Pro._Default" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>HR Attendance System - Dashboard</title>
    <style>
        body {
            font-family: Segoe UI, sans-serif;
            background-color: #f0f2f5;
            padding: 50px;
        }
        h2 {
            color: #4B0082;
            margin-bottom: 20px;
        }
        .section {
            margin-bottom: 40px;
        }
        .btn {
            display: inline-block;
            margin: 5px;
            padding: 10px 20px;
            background-color: #4B0082;
            color: white;
            text-decoration: none;
            border-radius: 6px;
            transition: background-color 0.2s ease;
        }
        .btn:hover {
            background-color: #6a0dad;
        }
        .section h3 {
            color: #333;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <h2>🏠 HR Attendance System Dashboard</h2>

        <div class="section">
            <h3>🔧 Master Data</h3>
            <a href="Department.aspx" class="btn">Department Creation</a>
            <a href="Shift.aspx" class="btn">Shift Creation</a>
            <a href="Employee.aspx" class="btn">Employee Creation</a>
            <a href="ShiftAssignment.aspx" class="btn">Shift Assignment</a>
        </div>

        <div class="section">
            <h3>📅 Attendance & OT</h3>
            <a href="Attendance.aspx" class="btn">Attendance Entry</a>
            <a href="OvertimeEntry.aspx" class="btn">Manual OT Entry</a>
            <a href="AutoOvertime.aspx" class="btn">Auto OT Calculation</a>
        </div>

        <div class="section">
            <h3>📊 Reports</h3>
            <a href="Pages/AttendanceReport.aspx" class="btn">Attendance Report</a>
            <a href="Pages/OTSummary.aspx" class="btn">OT Summary Report</a>
            <a href="Pages/MonthlySummary.aspx" class="btn">Monthly Summary Report</a>
        </div>
    </form>
</body>
</html>
