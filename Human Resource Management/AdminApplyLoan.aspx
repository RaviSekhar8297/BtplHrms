<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="AdminApplyLoan.aspx.cs" Inherits="Human_Resource_Management.AdminApplyLoan" %>

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
                            <h3 class="page-title">Attendance Count</h3>
                            <ul class="breadcrumb">
                                <li class="breadcrumb-item"><a href="Admindashboard.aspx">Dashboard</a></li>
                                <li class="breadcrumb-item active">Attendance Count</li>
                            </ul>
                        </div>

                        <div class="col-auto float-end ms-auto">
                            <a href="AdminGeneratePayroll.aspx" class="btn add-btn"><i class="la la-calculator"></i>Generate Payroll</a>
                        </div>


                    </div>
                </div>

              <%--  <div class="row">
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


                </div>--%>
            </div>
        </div>
    </div>
</asp:Content>
