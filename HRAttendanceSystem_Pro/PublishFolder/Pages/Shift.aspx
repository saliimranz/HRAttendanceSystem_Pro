<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Shift.aspx.vb" Inherits="HRAttendanceSystem_Pro.Pages_Shift" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h3>Shift Creation</h3>
    
            Shift Name:
            <asp:TextBox ID="txtShiftName" runat="server" /><br />
    
            Start Time:
            <input type="time" id="txtStartTime" runat="server" /><br />
    
            End Time:
            <input type="time" id="txtEndTime" runat="server" /><br />
    
            Grace Period (minutes):
            <asp:TextBox ID="txtGrace" runat="server" /><br />
    
            Break Duration (minutes):
            <asp:TextBox ID="txtBreak" runat="server" /><br />
    
            <asp:Button ID="btnSave" runat="server" Text="Save Shift" OnClick="btnSave_Click" /><br />
            <asp:Label ID="lblMsg" runat="server" ForeColor="Green" />
        </div>
    </form>
</body>
</html>
