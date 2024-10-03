<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="AdminChangePassword.aspx.cs" Inherits="Human_Resource_Management.AdminChangePassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- Include Bootstrap CSS -->
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css">
    <style>
        /* Custom styles for responsiveness */
        .main-wrapper {
            display: flex;
            justify-content: center;
            align-items: center;
            height: 100vh;
            background-color: #f5f5f5;
        }

        .account-content {
            max-width: 400px;
            width: 100%;
            margin: auto;
            background: #fff;
            padding: 20px;
            border-radius: 5px;
            box-shadow: 0px 0px 10px 0px rgba(0, 0, 0, 0.1);
        }

        .account-logo {
            text-align: center;
            margin-bottom: 20px;
        }

        .account-title {
            font-size: 24px;
            margin-bottom: 20px;
            text-align: center;
        }

        .input-block {
            margin-bottom: 15px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="main-wrapper">
        <div class="account-content">
            <!-- Account Logo -->
            <div class="account-logo">
                <a href="admin-dashboard.html">
                    <img src="assets/img/logo2.png" alt="Dreamguy's Technologies"></a>
            </div>
            <div class="account-box">
                <div class="account-wrapper">
                    <h3 class="account-title">Change Password</h3>
                    <div class="input-block mb-3">
                        <label class="col-form-label">Employee Id</label>
                        <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                    </div>
                    <div class="input-block mb-3">
                        <label class="col-form-label">New password</label>
                        <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control" AutoCompleteType="Disabled"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBox2" ErrorMessage="Password is required." CssClass="text-danger"></asp:RequiredFieldValidator>
                    </div>
                    <div class="input-block mb-3">
                        <label class="col-form-label">Confirm password</label>
                        <asp:TextBox ID="TextBox3" runat="server" CssClass="form-control" AutoCompleteType="Disabled"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TextBox3" ErrorMessage="Confirm password is required." CssClass="text-danger"></asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="TextBox3" ControlToCompare="TextBox2" Operator="Equal" ErrorMessage="Passwords not match." CssClass="text-danger"></asp:CompareValidator>
                    </div>
                    <div class="submit-section mb-4">
                        <asp:Button ID="btnchangepassword" runat="server" Text="Update Password" CssClass="btn btn-primary btn-block submit-btn" OnClick="btnchangepassword_Click" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
