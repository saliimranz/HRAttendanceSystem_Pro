<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Attendance.aspx.vb" Inherits="HRAttendanceSystem_Pro.Pages_Attendance" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <div>
        <form id="form1" runat="server">
          <div>
            <h3>Attendance Entry</h3>

            Type:
            <asp:DropDownList ID="ddlType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlType_SelectedIndexChanged">
                <asp:ListItem Text="-- Select --" Value="" />
                <asp:ListItem Text="Manual" Value="Manual" />
                <asp:ListItem Text="Excel Upload" Value="Excel" />
            </asp:DropDownList>
            <br /><br />

            <!-- Manual Entry -->
            <div id="divManual" runat="server" visible="false">
                <h4>Manual Attendance</h4>
    
                Employee:
                <asp:DropDownList ID="ddlEmployee" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlEmployee_SelectedIndexChanged" /><br />
    
                Date:
                <asp:TextBox ID="txtDate" TextMode="Date" runat="server" AutoPostBack="true" OnTextChanged="txtDate_TextChanged" /><br />
    
                Shift Assignment:
                <asp:DropDownList ID="ddlShiftAssignment" runat="server" /><br />

                In Time:
                <input type="time" id="txtIn" runat="server" /><br />
    
                Out Time:
                <input type="time" id="txtOut" runat="server" /><br />
    
                Status:
                <asp:TextBox ID="txtStatus" runat="server" /><br />
    
                <asp:Button ID="btnSaveManual" runat="server" Text="Save Attendance" OnClick="btnSaveManual_Click" /><br />
            </div>

            <!-- Excel Upload -->
            <div id="divExcel" runat="server" visible="false">
                <h4>Excel Upload</h4>
                <asp:FileUpload ID="fileUpload" runat="server" /><br />
                <asp:Button ID="btnUpload" runat="server" Text="Upload & Save" OnClick="btnUpload_Click" /><br />
            </div>

            <asp:Label ID="lblMsg" runat="server" ForeColor="Green" />
          </div>
        </form>
    </div>
</body>
</html>
