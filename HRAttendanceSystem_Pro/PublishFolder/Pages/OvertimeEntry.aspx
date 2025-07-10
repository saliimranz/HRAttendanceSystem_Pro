<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="OvertimeEntry.aspx.vb" Inherits="HRAttendanceSystem_Pro.Pages_OvertimeEntry" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Manual OT Entry</title>
</head>
<body>
    <form id="form1" runat="server">
        <h3>Manual Overtime Entry</h3>

        Employee:
        <asp:DropDownList ID="ddlEmployee" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlEmployee_SelectedIndexChanged" /><br />

        Date:
        <asp:TextBox ID="txtDate" TextMode="Date" runat="server" AutoPostBack="true" OnTextChanged="txtDate_TextChanged" /><br />

        Shift Assignment:
        <asp:DropDownList ID="ddlShiftAssignment" runat="server" /><br />

        OT Hours:
        <asp:TextBox ID="txtHours" runat="server" /><br />

        OT Reason:
        <asp:TextBox ID="txtReason" runat="server" /><br />

        <asp:Button ID="btnSaveOT" runat="server" Text="Save OT" OnClick="btnSaveOT_Click" /><br />

        <asp:Label ID="lblMsg" runat="server" />
    </form>
</body>
</html>
