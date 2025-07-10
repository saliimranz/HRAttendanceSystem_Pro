<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Employee.aspx.vb" Inherits="HRAttendanceSystem_Pro.Pages_Employee" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h3>Employee Creation</h3>

            Emp Code:
            <asp:TextBox ID="txtEmpCode" runat="server" /><br />

            Name:
            <asp:TextBox ID="txtName" runat="server" /><br />

            Contact:
            <asp:TextBox ID="txtContact" runat="server" /><br />

            Designation:
            <asp:TextBox ID="txtDesignation" runat="server" /><br />

            Department:
            <asp:DropDownList ID="ddlDepartment" runat="server" AppendDataBoundItems="true">
                <asp:ListItem Text="-- Select --" Value="" />
            </asp:DropDownList><br />

            <asp:Button ID="btnSave" runat="server" Text="Save Employee" OnClick="btnSave_Click" /><br />
            <asp:Label ID="lblMsg" runat="server" ForeColor="Green" />
        </div>
    </form>
</body>
</html>
