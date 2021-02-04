<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginPage.aspx.cs" Inherits="AppSec_Assignment_191473X.LoginPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login Page</title>
    <script src="https://www.google.com/recaptcha/api.js?render=6LdMzEgaAAAAAM1tmS_0EyrdFraEa3cLaM5HcwEs"></script>
    <style type="text/css">
        .auto-style1 {
            width: 120px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h3>Login</h3>
            <br />
            <br />
            <table style="width:100%;">
                <tr>
                    <td class="auto-style1">Email</td>
                    <td>
                        <asp:TextBox ID="tbLoginEmail" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style1">Password</td>
                    <td>
                        <asp:TextBox ID="tbLoginPassword" runat="server" TextMode="Password"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style1">&nbsp;</td>
                    <td>
                        <asp:Button ID="btnLogin" runat="server" Text="Login" Width="186px" OnClick="btnLogin_Click" />
                    </td>
                </tr>
            </table>
            <asp:Label ID="lbl_gScore" runat="server"></asp:Label>
            <br />
            <br />
            <asp:Label ID="lblMsg" runat="server"></asp:Label>
            <br />
            <br />
            <br />
            Don&#39;t have an account?
            <asp:HyperLink ID="hypLinkRegister" runat="server" NavigateUrl="~/RegistrationForm.aspx">Register</asp:HyperLink>
        </div>
        <input type="hidden" id="g-recaptcha-response" name="g-recaptcha-response" />
    </form>
    <script>
        grecaptcha.ready(function () {
            grecaptcha.execute('6LdMzEgaAAAAAM1tmS_0EyrdFraEa3cLaM5HcwEs', { action: 'Login' }).then(function (token) {
                document.getElementById("g-recaptcha-response").value = token;
            });
        });
    </script>
</body>
</html>
