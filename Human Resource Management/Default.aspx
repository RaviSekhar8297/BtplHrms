<%@ Page Title="Home Page" Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Human_Resource_Management._Default" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <meta name="description" content="" />
    <meta name="author" content="Dashboard" />
    <meta name="keyword" content="Dashboard, Bootstrap, Admin, Template, Theme, Responsive, Fluid, Retina" />
    <title>Brihaspathi Technologies-HRMS</title>

    <!-- Font Awesome CDN for icons -->
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0-beta3/css/all.min.css" rel="stylesheet" />
    <!-- Latest Bootstrap CSS -->
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/css/bootstrap.min.css" />
    <!-- jQuery library -->
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.7.1/jquery.min.js"></script>
    <!-- Latest Bootstrap JavaScript -->
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/js/bootstrap.min.js"></script>

    <style type="text/css">
        /* General body styling */
        body {
            background-color: #f4f5f7;
            font-family: 'Roboto', sans-serif;
            margin: 0;
            padding: 0;
            min-height: 100vh;
            display: flex;
            justify-content: center;
            align-items: center;
            width: 100%;
        }

        .login-wrapper {
            display: flex;
            width: 100%;
            max-width: 1200px;
            justify-content: space-between;
            background-color: #fff;
            border-radius: 16px;
            box-shadow: 0 8px 24px rgba(0, 0, 0, 0.1);
            overflow: hidden;
            position: relative;
        }

        .login-container {
            padding: 40px;
            width: 40%;
            display: flex;
            flex-direction: column;
            justify-content: center;
            align-items: center;
            text-align: center;
            background-color: #fff;
            background-size: cover;
            background-position: center;
            background-repeat: no-repeat;
            transition: transform 0.6s ease-in-out;
            backface-visibility: hidden;
        }

        .image-container {
            width: 60%;
            background: #ffe6c1;
            display: flex;
            align-items: center;
            justify-content: center;
        }

            .image-container img {
                max-width: 100%;
                height: auto;
            }

        .login-container h5 {
            font-size: 20px;
            color: #f7931e;
            font-weight: 700;
            margin-bottom: 10px;
        }

        .profile-pic {
            margin-bottom: 15px;
        }

            .profile-pic img {
                width: 150px;
                height: 150px;
                border-radius: 50%;
            }

        .login-as {
            margin-bottom: 20px;
            color: #999;
        }

        .input-group {
            margin-bottom: 12px;
            position: relative;
        }

            .input-group .form-control {
                padding-left: 40px;
                padding-right: 40px;
                font-size: 14px;
                border-radius: 20px 0px 20px 0px;
                border: 1px solid #ddd;
                height: 40px;
                border-left: 30px solid #f7931e;
                outline: 1px solid #c3beb9;
                outline-offset: -1px;
            }

        .input-group-addon1 {
            position: absolute;
            left: 12px;
            top: 50%;
            transform: translateY(-50%);
            color: white;
            font-size: 12px;
            z-index: 9;
        }

        .show-password-icon {
            position: absolute;
            right: 12px;
            top: 50%;
            transform: translateY(-50%);
            cursor: pointer;
            font-size: 12px;
            color: #f7931e;
            z-index: 9;
        }

        .btn-login {
            background-color: #f7931e;
            color: white;
            border: none;
            padding: 10px;
            width: 100%;
            border-radius: 25px;
            font-size: 14px;
            font-weight: bold;
            cursor: pointer;
            transition: 0.3s ease;
        }

            .btn-login:hover {
                background-color: #f67807;
            }

        .forgot-password {
            margin-top: 10px;
            font-size: 10px;
            color: #f7931e;
            border: 1px solid #f7931e;
            padding: 4px 8px;
            border-radius: 25px;
            text-align: center;
            background-color: transparent;
            cursor: pointer;
            display: inline-block;
            transition: 0.3s ease;
            text-decoration: none;
        }

            .forgot-password:hover {
                background-color: #f7931e;
                color: white;
            }

        .forgot-password-form {
            display: none;
        }

        /* Responsive adjustments */
        @media (max-width: 992px) {
            .login-wrapper {
                flex-direction: column;
                max-height: none;
            }

            .login-container, .image-container {
                width: 100%;
            }

            .image-container {
                display: none;
            }
        }

        .img-for {
            border-radius: 50%;
        }
    </style>


</head>

<body>
    <form id="form1" runat="server">
        <div class="login-wrapper">
            <!-- Left side image -->
            <div class="image-container">
                <img src="assets/Images/LoginImages/Image1.jpg" alt="Login Illustration" />
            </div>

            <!-- Right side login container -->
            <div class="login-container login-form">
                <h2>HRMS</h2>
                <div class="profile-pic">
                    <img src="assets/Images/LoginImages/profile.png" alt="Avatar" />
                </div>

                <div class="login-as">
                    Sign In
           
                </div>

                <!-- User ID input with icon -->
                <div class="input-group">
                    <i class="fas fa-user input-group-addon1"></i>
                    <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" placeholder="User ID"></asp:TextBox>
                    <asp:RequiredFieldValidator
                        ID="RequiredFieldValidator4"
                        runat="server"
                        ControlToValidate="txtUsername"
                        ErrorMessage="Enter Your Id."
                        CssClass="text-danger"
                        ValidationGroup="vgForm" />
                </div>

                <!-- Password input with toggle icon -->
                <div class="input-group">
                    <i class="fas fa-lock input-group-addon1"></i>
                    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control" placeholder="Password"></asp:TextBox>
                    <i id="togglePasswordIcon" class="fas fa-eye show-password-icon" onclick="togglePassword()"></i>
                    <asp:RequiredFieldValidator
                        ID="RequiredFieldValidator1"
                        runat="server"
                        ControlToValidate="txtPassword"
                        ErrorMessage="Enter Your Password."
                        CssClass="text-danger"
                        ValidationGroup="vgForm" />
                </div>

                <!-- Login button -->
                <asp:Button ID="btnLogin" runat="server" CssClass="btn-login" Text="Sign In" OnClick="btnLogin_Click" ValidationGroup="vgForm" />

                <!-- Forgot password link -->
                <a href="javascript:void(0);" class="forgot-password" onclick="flipContainer()">Forgot Password?</a>
            </div>

            <!-- Forgot Password form -->
            <div class="login-container forgot-password-form">

                <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                <h5>Forgot Password</h5>
                <div style="border-radius: 50%;">
                    <asp:Image ID="Image1" runat="server" Height="80px" Width="80px" CssClass="img-for" />
                </div>
                <div></div>
                <div class="input-group">
                    <i class="fas fa-user input-group-addon1"></i>
                    <asp:TextBox ID="txtforuserid" runat="server" CssClass="form-control" placeholder="User Id" OnTextChanged="txtforuserid_TextChanged" AutoPostBack="true"></asp:TextBox>
                </div>

                <!-- OTP input with icon -->
                <div class="input-group">
                    <i class="fas fa-key input-group-addon1"></i>
                    <asp:TextBox ID="txtforotp" runat="server" CssClass="form-control" placeholder="Enter OTP" AutoPostBack="true" OnTextChanged="txtforotp_TextChanged"></asp:TextBox>
                </div>

                <!-- User ID input with icon -->
                <div class="input-group">
                    <i class="fas fa-user input-group-addon1"></i>
                    <asp:TextBox ID="txtforpass" runat="server" CssClass="form-control" placeholder="Set Password" onkeyup="validatePassword()"></asp:TextBox>
                    <span id="error" style="color: red;"></span>
                </div>

                <div class="input-group">
                    <i class="fas fa-user input-group-addon1"></i>
                    <asp:TextBox ID="txtforconfirmpass" runat="server" CssClass="form-control" placeholder="Confirm Password" onkeyup="validateConfirmPassword()"></asp:TextBox>
                    <span id="errormsg" style="color: red;"></span>
                </div>


                <!-- Reset Password button -->
                <asp:Button ID="btnupdatepassword" runat="server" CssClass="btn-login" Text="Update" OnClick="btnupdatepassword_Click" />


                <!-- Back to login link -->
                <a href="javascript:void(0);" class="forgot-password" onclick="flipContainer()">Back to Login</a>
            </div>
        </div>
    </form>
    <script type="text/javascript">
        function togglePassword() {
            var passwordInput = document.getElementById('txtPassword');
            var passwordIcon = document.getElementById('togglePasswordIcon');
            if (passwordInput.type === 'password') {
                passwordInput.type = 'text';
                passwordIcon.classList.remove('fa-eye');
                passwordIcon.classList.add('fa-eye-slash');
            } else {
                passwordInput.type = 'password';
                passwordIcon.classList.remove('fa-eye-slash');
                passwordIcon.classList.add('fa-eye');
            }
        }

        function flipContainer() {
            var loginForm = document.querySelector('.login-form');
            var forgotPasswordForm = document.querySelector('.forgot-password-form');
            if (loginForm.style.display === 'none') {
                loginForm.style.display = 'block';
                forgotPasswordForm.style.display = 'none';
            } else {
                loginForm.style.display = 'none';
                forgotPasswordForm.style.display = 'block';
            }
        }

        function login() {
            alert('Login functionality goes here.');
        }

        function sendResetLink() {
            alert('Password reset link functionality goes here.');
        }
    </script>


    <script type="text/javascript">
        function validatePassword() {
            // Get the password input values
            var password = document.getElementById('<%= txtforpass.ClientID %>').value;
            var errorSpan = document.getElementById('error');
            errorSpan.innerHTML = ""; // Clear previous errors

            // Regular expressions for validation
            var hasUpperCase = /[A-Z]/.test(password);
            var hasLowerCase = /[a-z]/.test(password);
            var hasNumber = /[0-9]/.test(password);

            // Check the criteria
            if (!hasUpperCase || !hasLowerCase || !hasNumber) {
                var errorMessage = "Password must contain at least:";
                if (!hasUpperCase) errorMessage += "<br>- One uppercase letter (A-Z)";
                if (!hasLowerCase) errorMessage += "<br>- One lowercase letter (a-z)";
                if (!hasNumber) errorMessage += "<br>- One number (0-9)";
                errorSpan.innerHTML = errorMessage;
                document.getElementById('<%= btnupdatepassword.ClientID %>').style.display = "none"; // Hide button
            } else {
                errorSpan.innerHTML = ""; // Clear previous error messages if password is valid
            }
        }

        function validateConfirmPassword() {
            // Get the password and confirm password input values
            var password = document.getElementById('<%= txtforpass.ClientID %>').value;
            var confirmPassword = document.getElementById('<%= txtforconfirmpass.ClientID %>').value;
            var errorSpan1 = document.getElementById('errormsg');
            errorSpan1.innerHTML = ""; // Clear previous errors

            // Check if the passwords match
            if (password === confirmPassword) {
                errorSpan1.innerHTML = "Passwords match.";
                errorSpan1.style.color = "green"; // Optional: change color for valid input
                document.getElementById('<%= btnupdatepassword.ClientID %>').style.display = "block"; // Show button
            } else {
                errorSpan1.innerHTML = "Passwords do not match.";
                errorSpan1.style.color = "red"; // Set error message color to red
                document.getElementById('<%= btnupdatepassword.ClientID %>').style.display = "none"; // Hide button
            }
        }
    </script>


</body>
</html>
