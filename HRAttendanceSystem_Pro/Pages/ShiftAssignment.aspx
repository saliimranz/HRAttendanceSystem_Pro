<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ShiftAssignment.aspx.vb" Inherits="HRAttendanceSystem_Pro.Pages_ShiftAssignment" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h3>Assign Shift to Employee</h3>

            Employee:
            <asp:DropDownList ID="ddlEmployee" runat="server" AppendDataBoundItems="true">
                <asp:ListItem Text="-- Select --" Value="" />
            </asp:DropDownList><br />

            Shift:
            <asp:DropDownList ID="ddlShift" runat="server" AppendDataBoundItems="true">
                <asp:ListItem Text="-- Select --" Value="" />
            </asp:DropDownList><br />

            Start Date:
            <asp:TextBox ID="txtStartDate" runat="server" TextMode="Date" placeholder="yyyy-mm-dd" /><br />

            End Date:
            <asp:TextBox ID="txtEndDate" runat="server" TextMode="Date" placeholder="yyyy-mm-dd" /><br />

            <asp:Button ID="btnAssign" runat="server" Text="Assign Shift" OnClick="btnAssign_Click" /><br />
            <asp:Label ID="lblMsg" runat="server" ForeColor="Green" />
        </div>
    </form>
</body>
</html>
