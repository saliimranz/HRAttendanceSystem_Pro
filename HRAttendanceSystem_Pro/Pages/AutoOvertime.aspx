<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="AutoOvertime.aspx.vb" Inherits="HRAttendanceSystem_Pro.Pages_AutoOvertime" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Auto Overtime Calculation</title>
</head>
<body>
    <form id="form1" runat="server">
        <h3>Auto OT Preview & Save</h3>

        From:
        <asp:TextBox ID="txtFrom" runat="server" TextMode="Date" />
        To:
        <asp:TextBox ID="txtTo" runat="server" TextMode="Date" />
        <asp:Button ID="btnPreview" runat="server" Text="Preview OT" OnClick="btnPreview_Click" /><br /><br />

        <asp:GridView ID="gvOT" runat="server" AutoGenerateColumns="False" DataKeyNames="AssignmentID" >
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:CheckBox ID="chkSelect" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:BoundField DataField="EmpName" HeaderText="Employee" />
                <asp:BoundField DataField="AttDate" HeaderText="Date" />
                <asp:BoundField DataField="OutTime" HeaderText="Out Time" />
                <asp:BoundField DataField="ShiftEndTime" HeaderText="Shift End" />
                <asp:BoundField DataField="CalculatedOT" HeaderText="OT Hours" />

                <asp:TemplateField HeaderText="Reason">
                    <ItemTemplate>
                        <asp:TextBox ID="txtReason" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:BoundField DataField="AssignmentID" HeaderText="Assignment ID" />
            </Columns>
        </asp:GridView>

        <asp:Button ID="btnSaveOT" runat="server" Text="Confirm & Save OT" OnClick="btnSaveOT_Click" /><br />
        <asp:Label ID="lblMsg" runat="server" ForeColor="Green" />
    </form>
</body>
</html>
