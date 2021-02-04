<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Detail.aspx.cs" Inherits="AppSec_Assignment_191473X.Detail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>User Details</h1>
            <br />
            <br />
            First Name:
            <asp:Label ID="lblFirstName" runat="server"></asp:Label>
            <br />
            Last Name:
            <asp:Label ID="lblLastName" runat="server"></asp:Label>
            <br />
            Email: <asp:Label ID="lblEmail" runat="server"></asp:Label>
            <br />
            Date of Birth:
            <asp:Label ID="lblDOB" runat="server"></asp:Label>
            <br />
            <br />
            <asp:Button ID="btnLogOut" runat="server" OnClick="btnLogOut_Click" Text="Log Out" />
        </div>
    </form>
</body>
</html>
