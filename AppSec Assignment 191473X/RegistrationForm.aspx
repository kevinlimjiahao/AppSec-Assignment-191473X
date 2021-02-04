<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegistrationForm.aspx.cs" Inherits="AppSec_Assignment_191473X.RegistrationForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Registration</title>
    <style type="text/css">
        .auto-style1 {
            width: 226px;
        }

        .auto-style2 {
            width: 235px;
        }

        .auto-style3 {
            width: 143px;
        }
    </style>
    <script type="text/javascript">
        function validate() {
            var str = document.getElementById('<%=tbPassword.ClientID %>').value;

            if (str.length < 8) {
                document.getElementById("lblChkPwd").innerHTML = "Password Length Must be at Least 8 Characters";
                document.getElementById("lblChkPwd").style.color = "Red";
                return ("too_short")
            }

            else if (str.search(/[0-9]/) == -1) {
                document.getElementById("lblChkPwd").innerHTML = "Password require at least 1 Number";
                document.getElementById("lblChkPwd").style.color = "Red";
                return ("no_number")
            }

            else if (str.search(/[A-Z]/) == -1) {
                document.getElementById("lblChkPwd").innerHTML = "Password require at least 1 Uppercase Character";
                document.getElementById("lblChkPwd").style.color = "Red";
                return ("no_upper")
            }

            else if (str.search(/[a-z]/) == -1) {
                document.getElementById("lblChkPwd").innerHTML = "Password require at least 1 Lowercase Character";
                document.getElementById("lblChkPwd").style.color = "Red";
                return ("no_lower")
            }

            else if (str.search(/[^A-Za-z0-9]/) == -1) {
                document.getElementById("lblChkPwd").innerHTML = "Password require at least 1 Special Character";
                document.getElementById("lblChkPwd").style.color = "Red";
                return ("no_special")
            }

            document.getElementById("lblChkPwd").innerHTML = "Excellent!";
            document.getElementById("lblChkPwd").style.color = "Blue";
        }
    </script>
    
</head>
<body>
    <form id="form1" runat="server">
        <div>
            Registration Form<br />
            <br />
            <table style="width:100%;">
                <tr>
                    <td class="auto-style1">First Name:</td>
                    <td class="auto-style2">
                        <asp:TextBox ID="tbFirstName" runat="server"></asp:TextBox>
                    </td>
                    <td class="auto-style3">Last Name:</td>
                    <td>
                        <asp:TextBox ID="tbLastName" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style1">Credit Card Number:</td>
                    <td class="auto-style2">
                        <asp:TextBox ID="tbCreditCard" runat="server" MaxLength="16" TextMode="Number"></asp:TextBox>
                    </td>
                    <td class="auto-style3">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style1">Email:</td>
                    <td colspan="3">
                        <asp:TextBox ID="tbEmail" runat="server" TextMode="Email"></asp:TextBox>
                        <asp:Label ID="lblEmailChk" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style1">Password:</td>
                    <td colspan="3">
                        <asp:TextBox ID="tbPassword" runat="server" TextMode="Password" onkeyup="javascript:validate()"></asp:TextBox>
                        <asp:Label ID="lblChkPwd" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style1">Date of Birth:</td>
                    <td class="auto-style2">
                        <asp:TextBox ID="tbDOB" runat="server" TextMode="Date"></asp:TextBox>
                    </td>
                    <td class="auto-style3">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style1">&nbsp;</td>
                    <td class="auto-style2">
                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" Width="186px" OnClick="btnSubmit_Click" />
                    </td>
                    <td class="auto-style3">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
            </table>
            <asp:Label ID="lblSuccess" runat="server"></asp:Label>
            <br />
            <br />
            <br />
            Already have an account?
            <asp:HyperLink ID="hyplinkLogin" runat="server" NavigateUrl="~/LoginPage.aspx">Log In</asp:HyperLink>
            <br />
        </div>
    </form>
</body>
</html>
