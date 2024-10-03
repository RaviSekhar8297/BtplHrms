<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="AdminAddUser.aspx.cs" Inherits="Human_Resource_Management.AdminAddUser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Main Wrapper -->
    <div class="main-wrapper">
        <!-- Page Wrapper -->
        <div class="page-wrapper">

            <!-- Page Content -->
            <div class="content container-fluid">

                <!-- Page Header -->
                <div class="page-header">
                    <div class="row align-items-center">
                        <div class="col">
                            <h3 class="page-title">Add Users</h3>
                            <ul class="breadcrumb">
                                <li class="breadcrumb-item"><a href="admin-dashboard.html">Dashboard</a></li>
                                <li class="breadcrumb-item active">Users</li>
                            </ul>
                        </div>
                        <div class="col-auto float-end ms-auto">
                            <div class="view-icons">
                                <a href="AdminUsers" class="list-view btn btn-link"><i class="fa-solid fa-bars"></i></a>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- /Page Header -->
                <asp:Label ID="Label1" runat="server"></asp:Label>
                <!-- Add User Modal -->
                <div class="row">
                    <div class="col-sm-6">
                        <div class="input-block mb-3">
                            <label class="col-form-label">Company </label>
                            <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" CssClass="form-control" onfocus="ddladddcompanydisable()">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-sm-6">
                        <div class="input-block mb-3">
                            <label class="col-form-label">Branch </label>
                            <asp:DropDownList ID="ddluserbranch" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddluserbranch_SelectedIndexChanged" onfocus="ddladddbranchdisable()">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-sm-6">
                        <div class="input-block mb-3">
                            <label class="col-form-label">Department </label>
                            <asp:DropDownList ID="ddluserdept" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddluserdept_SelectedIndexChanged" onfocus="ddladddeptdisable()">
                            </asp:DropDownList>
                        </div>
                    </div>


                    <div class="col-sm-6">
                        <div class="input-block mb-3">
                            <label class="col-form-label">Name <span class="text-danger">*</span></label>
                            <asp:DropDownList ID="ddlusername" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlusername_SelectedIndexChanged" onfocus="ddladdnamedisable()">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-sm-6">
                        <div class="input-block mb-3">
                            <label class="col-form-label">Designation <span class="text-danger">*</span></label>
                            <asp:DropDownList ID="ddluserdesignation" runat="server" CssClass="form-control" onfocus="ddladduserdesidisable()">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-sm-6">
                        <div class="input-block mb-3">
                            <label class="col-form-label">Role</label>
                            <asp:DropDownList ID="ddluserrole" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddluserrole_SelectedIndexChanged" onfocus="ddladduserroledisable()">
                                <asp:ListItem Value="">-- Select Role --</asp:ListItem>
                                <asp:ListItem Value="Admin">Admin</asp:ListItem>
                                <asp:ListItem Value="Manager">Manager</asp:ListItem>
                                <asp:ListItem Value="Employee">Employee</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-sm-6">
                        <div class="input-block mb-3">
                            <label class="col-form-label">Employee ID <span class="text-danger">*</span></label>
                            <asp:TextBox ID="txtuserempid" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-sm-6">
                        <div class="input-block mb-3">
                            <label class="col-form-label">BranchCode <span class="text-danger">*</span></label>
                            <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-sm-6">
                        <div class="input-block mb-3">
                            <label class="col-form-label">DOJ <span class="text-danger">*</span></label>
                            <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-sm-6">
                        <div class="input-block mb-3">
                            <label class="col-form-label">Manger</label>
                            <asp:DropDownList ID="ddlmanager" runat="server" CssClass="form-control" AutoPostBack="true" onfocus="ddlmanagerdisable()"></asp:DropDownList>
                        </div>
                    </div>

                    <div class="col-sm-6">
                        <div class="input-block mb-3">
                            <label class="col-form-label">Password</label>
                            <asp:TextBox ID="txtuserpassword" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtuserpassword" ErrorMessage="Password is required." CssClass="text-danger"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="col-sm-6">
                        <div class="input-block mb-3">
                            <label class="col-form-label">Confirm-Password</label>
                            <asp:TextBox ID="txtuserconpass" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtuserconpass" ErrorMessage="Confirm password is required." CssClass="text-danger"></asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtuserconpass" ControlToCompare="txtuserpassword" Operator="Equal" ErrorMessage="Passwords not match." CssClass="text-danger"></asp:CompareValidator>
                        </div>
                    </div>
                </div>
                <div class="table-responsive m-t-15">
                    <table class="table table-striped custom-table">
                        <thead>
                            <tr>
                                <th>Admin Permission</th>
                                <th class="text-center">Admin</th>
                                <th class="text-center">Manager</th>
                                <th class="text-center">Employee</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td>Roles</td>
                                <td class="text-center">
                                    <div class="checkbox-wrapper-31">
                                        <asp:CheckBox ID="CheckBox1" runat="server" />
                                        <svg viewBox="0 0 35.6 35.6">
                                            <circle class="background" cx="17.8" cy="17.8" r="17.8"></circle>
                                            <circle class="stroke" cx="17.8" cy="17.8" r="14.37"></circle>
                                            <polyline class="check" points="11.78 18.12 15.55 22.23 25.17 12.87"></polyline>
                                        </svg>
                                    </div>
                                </td>
                                <td class="text-center">
                                    <div class="checkbox-wrapper-31">
                                        <asp:CheckBox ID="CheckBox2" runat="server" />
                                        <svg viewBox="0 0 35.6 35.6">
                                            <circle class="background" cx="17.8" cy="17.8" r="17.8"></circle>
                                            <circle class="stroke" cx="17.8" cy="17.8" r="14.37"></circle>
                                            <polyline class="check" points="11.78 18.12 15.55 22.23 25.17 12.87"></polyline>
                                        </svg>
                                    </div>
                                </td>
                                <td class="text-center">
                                    <div class="checkbox-wrapper-31">
                                        <asp:CheckBox ID="CheckBox3" runat="server" />
                                        <svg viewBox="0 0 35.6 35.6">
                                            <circle class="background" cx="17.8" cy="17.8" r="17.8"></circle>
                                            <circle class="stroke" cx="17.8" cy="17.8" r="14.37"></circle>
                                            <polyline class="check" points="11.78 18.12 15.55 22.23 25.17 12.87"></polyline>
                                        </svg>
                                    </div>
                                </td>
                            </tr>

                        </tbody>
                    </table>
                    <table class="table table-striped custom-table">
                        <thead>
                            <tr>
                                <th>Module Permission</th>
                                <th class="text-center">Add</th>
                                <th class="text-center">Edit</th>
                                <th class="text-center">Delete</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td>Employee</td>
                                <td class="text-center">
                                    <div class="checkbox-wrapper-31">
                                        <asp:CheckBox ID="CheckBox4" runat="server" />
                                        <svg viewBox="0 0 35.6 35.6">
                                            <circle class="background" cx="17.8" cy="17.8" r="17.8"></circle>
                                            <circle class="stroke" cx="17.8" cy="17.8" r="14.37"></circle>
                                            <polyline class="check" points="11.78 18.12 15.55 22.23 25.17 12.87"></polyline>
                                        </svg>
                                    </div>
                                </td>

                                <td class="text-center">
                                    <div class="checkbox-wrapper-31">
                                        <asp:CheckBox ID="CheckBox5" runat="server" />
                                        <svg viewBox="0 0 35.6 35.6">
                                            <circle class="background" cx="17.8" cy="17.8" r="17.8"></circle>
                                            <circle class="stroke" cx="17.8" cy="17.8" r="14.37"></circle>
                                            <polyline class="check" points="11.78 18.12 15.55 22.23 25.17 12.87"></polyline>
                                        </svg>
                                    </div>
                                </td>
                                <td class="text-center">
                                    <div class="checkbox-wrapper-31">
                                        <asp:CheckBox ID="CheckBox6" runat="server" />
                                        <svg viewBox="0 0 35.6 35.6">
                                            <circle class="background" cx="17.8" cy="17.8" r="17.8"></circle>
                                            <circle class="stroke" cx="17.8" cy="17.8" r="14.37"></circle>
                                            <polyline class="check" points="11.78 18.12 15.55 22.23 25.17 12.87"></polyline>
                                        </svg>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>Holidays</td>
                                <td class="text-center">
                                    <div class="checkbox-wrapper-31">
                                        <asp:CheckBox ID="CheckBox7" runat="server" />
                                        <svg viewBox="0 0 35.6 35.6">
                                            <circle class="background" cx="17.8" cy="17.8" r="17.8"></circle>
                                            <circle class="stroke" cx="17.8" cy="17.8" r="14.37"></circle>
                                            <polyline class="check" points="11.78 18.12 15.55 22.23 25.17 12.87"></polyline>
                                        </svg>
                                    </div>
                                </td>
                                <td class="text-center">
                                    <div class="checkbox-wrapper-31">
                                        <asp:CheckBox ID="CheckBox8" runat="server" />
                                        <svg viewBox="0 0 35.6 35.6">
                                            <circle class="background" cx="17.8" cy="17.8" r="17.8"></circle>
                                            <circle class="stroke" cx="17.8" cy="17.8" r="14.37"></circle>
                                            <polyline class="check" points="11.78 18.12 15.55 22.23 25.17 12.87"></polyline>
                                        </svg>
                                    </div>
                                </td>
                                <td class="text-center">
                                    <div class="checkbox-wrapper-31">
                                        <asp:CheckBox ID="CheckBox9" runat="server" />
                                        <svg viewBox="0 0 35.6 35.6">
                                            <circle class="background" cx="17.8" cy="17.8" r="17.8"></circle>
                                            <circle class="stroke" cx="17.8" cy="17.8" r="14.37"></circle>
                                            <polyline class="check" points="11.78 18.12 15.55 22.23 25.17 12.87"></polyline>
                                        </svg>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>Projects</td>
                                <td class="text-center">
                                    <div class="checkbox-wrapper-31">
                                        <asp:CheckBox ID="CheckBox10" runat="server" />
                                        <svg viewBox="0 0 35.6 35.6">
                                            <circle class="background" cx="17.8" cy="17.8" r="17.8"></circle>
                                            <circle class="stroke" cx="17.8" cy="17.8" r="14.37"></circle>
                                            <polyline class="check" points="11.78 18.12 15.55 22.23 25.17 12.87"></polyline>
                                        </svg>
                                    </div>
                                </td>
                                <td class="text-center">
                                    <div class="checkbox-wrapper-31">
                                        <asp:CheckBox ID="CheckBox11" runat="server" />
                                        <svg viewBox="0 0 35.6 35.6">
                                            <circle class="background" cx="17.8" cy="17.8" r="17.8"></circle>
                                            <circle class="stroke" cx="17.8" cy="17.8" r="14.37"></circle>
                                            <polyline class="check" points="11.78 18.12 15.55 22.23 25.17 12.87"></polyline>
                                        </svg>
                                    </div>
                                </td>
                                <td class="text-center">
                                    <div class="checkbox-wrapper-31">
                                        <asp:CheckBox ID="CheckBox12" runat="server" />
                                        <svg viewBox="0 0 35.6 35.6">
                                            <circle class="background" cx="17.8" cy="17.8" r="17.8"></circle>
                                            <circle class="stroke" cx="17.8" cy="17.8" r="14.37"></circle>
                                            <polyline class="check" points="11.78 18.12 15.55 22.23 25.17 12.87"></polyline>
                                        </svg>
                                    </div>
                                </td>

                            </tr>
                            <tr>
                                <td>Leaves</td>
                                <td class="text-center">
                                    <div class="checkbox-wrapper-31">
                                        <asp:CheckBox ID="CheckBox13" runat="server" />
                                        <svg viewBox="0 0 35.6 35.6">
                                            <circle class="background" cx="17.8" cy="17.8" r="17.8"></circle>
                                            <circle class="stroke" cx="17.8" cy="17.8" r="14.37"></circle>
                                            <polyline class="check" points="11.78 18.12 15.55 22.23 25.17 12.87"></polyline>
                                        </svg>
                                    </div>
                                </td>
                                <td class="text-center">
                                    <div class="checkbox-wrapper-31">
                                        <asp:CheckBox ID="CheckBox14" runat="server" />
                                        <svg viewBox="0 0 35.6 35.6">
                                            <circle class="background" cx="17.8" cy="17.8" r="17.8"></circle>
                                            <circle class="stroke" cx="17.8" cy="17.8" r="14.37"></circle>
                                            <polyline class="check" points="11.78 18.12 15.55 22.23 25.17 12.87"></polyline>
                                        </svg>
                                    </div>
                                </td>
                                <td class="text-center">
                                    <div class="checkbox-wrapper-31">
                                        <asp:CheckBox ID="CheckBox15" runat="server" />
                                        <svg viewBox="0 0 35.6 35.6">
                                            <circle class="background" cx="17.8" cy="17.8" r="17.8"></circle>
                                            <circle class="stroke" cx="17.8" cy="17.8" r="14.37"></circle>
                                            <polyline class="check" points="11.78 18.12 15.55 22.23 25.17 12.87"></polyline>
                                        </svg>
                                    </div>
                                </td>

                            </tr>
                            <tr>
                                <td>Assets</td>
                                <td class="text-center">
                                    <div class="checkbox-wrapper-31">
                                        <asp:CheckBox ID="CheckBox16" runat="server" />
                                        <svg viewBox="0 0 35.6 35.6">
                                            <circle class="background" cx="17.8" cy="17.8" r="17.8"></circle>
                                            <circle class="stroke" cx="17.8" cy="17.8" r="14.37"></circle>
                                            <polyline class="check" points="11.78 18.12 15.55 22.23 25.17 12.87"></polyline>
                                        </svg>
                                    </div>
                                </td>
                                <td class="text-center">
                                    <div class="checkbox-wrapper-31">
                                        <asp:CheckBox ID="CheckBox17" runat="server" />
                                        <svg viewBox="0 0 35.6 35.6">
                                            <circle class="background" cx="17.8" cy="17.8" r="17.8"></circle>
                                            <circle class="stroke" cx="17.8" cy="17.8" r="14.37"></circle>
                                            <polyline class="check" points="11.78 18.12 15.55 22.23 25.17 12.87"></polyline>
                                        </svg>
                                    </div>
                                </td>
                                <td class="text-center">
                                    <%--<label class="custom_check">
                                        <asp:CheckBox ID="CheckBox18" runat="server" />
                                        <span class="checkmark"></span>
                                    </label> --%>
                                    <div class="checkbox-wrapper-31">
                                        <asp:CheckBox ID="CheckBox18" runat="server" />
                                        <svg viewBox="0 0 35.6 35.6">
                                            <circle class="background" cx="17.8" cy="17.8" r="17.8"></circle>
                                            <circle class="stroke" cx="17.8" cy="17.8" r="14.37"></circle>
                                            <polyline class="check" points="11.78 18.12 15.55 22.23 25.17 12.87"></polyline>
                                        </svg>
                                    </div>
                                </td>

                            </tr>
                        </tbody>
                    </table>
                </div>
                <div class="submit-section">
                    <asp:Button ID="btnuserlogin" runat="server" Text="Create" CssClass="btn btn-primary submit-btn" OnClientClick="return validateForm();" OnClick="btnuserlogin_Click" />
                </div>
                <!-- /Add User Modal -->
            </div>
        </div>
    </div>
    <script type="text/javascript">
        function ddladddcompanydisable() {
            var ddl = document.getElementById('<%= DropDownList1.ClientID %>');
            ddl.options[0].disabled = true;
        }
        function ddladddbranchdisable() {
            var ddl = document.getElementById('<%= ddluserbranch.ClientID %>');
            ddl.options[0].disabled = true;
        }
        function ddladddeptdisable() {
            var ddl = document.getElementById('<%= ddluserdept.ClientID %>');
            ddl.options[0].disabled = true;
        }
        function ddladdnamedisable() {
            var ddl = document.getElementById('<%= ddlusername.ClientID %>');
            ddl.options[0].disabled = true;
        }

        function ddladdnamedisable() {
            var ddl = document.getElementById('<%= ddlusername.ClientID %>');
            ddl.options[0].disabled = true;
        }

        function ddladduserroledisable() {
            var ddl = document.getElementById('<%= ddluserrole.ClientID %>');
            ddl.options[0].disabled = true;
        }
        function ddladduserdesidisable() {
            var ddl = document.getElementById('<%= ddluserdesignation.ClientID %>');
            ddl.options[0].disabled = true;
        }
        function ddlmanagerdisable() {
            var ddl = document.getElementById('<%= ddlmanager.ClientID %>');
            ddl.options[0].disabled = true;
        }
    </script>


    <script type="text/javascript">
        function validateForm() {
            // Get references to the input fields and DropDownLists
            var ddl1 = document.getElementById('<%= DropDownList1.ClientID %>');
            var ddlbranch = document.getElementById('<%= ddluserbranch.ClientID %>');
            var ddldept = document.getElementById('<%= ddluserdept.ClientID %>');
            var ddlname = document.getElementById('<%= ddlusername.ClientID %>');
            var ddldesignation = document.getElementById('<%= ddluserdesignation.ClientID %>');
            var ddlrole = document.getElementById('<%= ddluserrole.ClientID %>');

            // Get password fields
            var password = document.getElementById('<%= txtuserpassword.ClientID %>');
            var confirmPassword = document.getElementById('<%= txtuserconpass.ClientID %>');

            var isValid = true;

            // Validation for DropDownLists
            function validateDropDown(ddl) {
                var firstIndexValue = ddl.options[0].value;
                if (ddl.value === "" || ddl.value === firstIndexValue) {
                    ddl.classList.add("invalid");
                    ddl.classList.remove("valid");
                    return false;
                } else {
                    ddl.classList.remove("invalid");
                    ddl.classList.add("valid");
                    return true;
                }
            }
            isValid &= validateDropDown(ddl1);
            if (ddlbranch.value === "") {
                ddlbranch.classList.add("invalid");
                isValid = false;
            } else {
                ddlbranch.classList.remove("invalid");
                ddlbranch.classList.add("valid");
            }

            if (ddldept.value === "") {
                ddldept.classList.add("invalid");
                isValid = false;
            } else {
                ddldept.classList.remove("invalid");
                ddldept.classList.add("valid");
            }

            if (ddlname.value === "") {
                ddlname.classList.add("invalid");
                isValid = false;
            } else {
                ddlname.classList.remove("invalid");
                ddlname.classList.add("valid");
            }

            if (ddldesignation.value === "") {
                ddldesignation.classList.add("invalid");
                isValid = false;
            } else {
                ddldesignation.classList.remove("invalid");
                ddldesignation.classList.add("valid");
            }

            if (ddlrole.value === "") {
                ddlrole.classList.add("invalid");
                isValid = false;
            } else {
                ddlrole.classList.remove("invalid");
                ddlrole.classList.add("valid");
            }

            // Password match validation
            if (password.value !== confirmPassword.value) {
                password.classList.add("invalid");
                confirmPassword.classList.add("invalid");
                isValid = false;
            } else {
                password.classList.remove("invalid");
                confirmPassword.classList.remove("invalid");
                password.classList.add("valid");
                confirmPassword.classList.add("valid");
            }

            // If all validations pass
            return isValid;
        }
    </script>

    <style type="text/css">
        .invalid {
            border: 2px solid red;
        }

        .valid {
            border: 2px solid green;
        }
    </style>


    <style type="text/css">
        .checkbox-wrapper-31:hover .check {
            stroke-dashoffset: 0;
        }

        .checkbox-wrapper-31 {
            position: relative;
            display: inline-block;
            width: 30px;
            height: 30px;
        }

            .checkbox-wrapper-31 .background {
                fill: #ccc;
                transition: ease all 0.6s;
                -webkit-transition: ease all 0.6s;
            }

            .checkbox-wrapper-31 .stroke {
                fill: none;
                stroke: #fff;
                stroke-miterlimit: 10;
                stroke-width: 2px;
                stroke-dashoffset: 100;
                stroke-dasharray: 100;
                transition: ease all 0.6s;
                -webkit-transition: ease all 0.6s;
            }

            .checkbox-wrapper-31 .check {
                fill: none;
                stroke: #fff;
                stroke-linecap: round;
                stroke-linejoin: round;
                stroke-width: 2px;
                stroke-dashoffset: 22;
                stroke-dasharray: 22;
                transition: ease all 0.6s;
                -webkit-transition: ease all 0.6s;
            }

            .checkbox-wrapper-31 input[type=checkbox] {
                position: absolute;
                width: 100%;
                height: 100%;
                left: 0;
                top: 0;
                margin: 0;
                opacity: 0;
                appearance: none;
                -webkit-appearance: none;
            }

                .checkbox-wrapper-31 input[type=checkbox]:hover {
                    cursor: pointer;
                }

                .checkbox-wrapper-31 input[type=checkbox]:checked + svg .background {
                    fill: #6cbe45;
                }

                .checkbox-wrapper-31 input[type=checkbox]:checked + svg .stroke {
                    stroke-dashoffset: 0;
                }

                .checkbox-wrapper-31 input[type=checkbox]:checked + svg .check {
                    stroke-dashoffset: 0;
                }
    </style>

</asp:Content>
