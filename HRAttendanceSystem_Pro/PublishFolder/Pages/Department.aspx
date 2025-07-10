<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Department.aspx.vb" Inherits="HRAttendanceSystem_Pro.Pages_Department" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h3>Department Management</h3>
            Name: <asp:TextBox ID="txtDeptName" runat="server" /><br />
            Description: <asp:TextBox ID="txtDesc" runat="server" /><br />
            <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" /><br />
            <asp:Label ID="lblMsg" runat="server" ForeColor="Green" />
        </div>
    </form>
</body>
</html>
