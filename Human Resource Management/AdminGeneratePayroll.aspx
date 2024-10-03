<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="AdminGeneratePayroll.aspx.cs" Inherits="Human_Resource_Management.AdminGeneratePayroll" %>

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
                            <h3 class="page-title">Payroll</h3>
                            <ul class="breadcrumb">
                                <li class="breadcrumb-item"><a href="admin-dashboard.html">Dashboard</a></li>
                                <li class="breadcrumb-item active">Payroll</li>
                            </ul>
                        </div>
                        <div class="col-auto float-end ms-auto">
                            <div class="view-icons">
                                <a href="AdminAttendanceCount.aspx" class="list-view btn btn-link"><i class="fa-solid fa-bars"></i></a>
                            </div>
                        </div>

                    </div>
                </div>
                <!-- /Page Header -->
                <div class="row">
                    <div class="col-sm-4">
                        <div class="input-block mb-3">
                            <label class="col-form-label">Company<span class="text-danger">*</span></label>
                            <asp:DropDownList ID="ddlcompany" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlcompany_SelectedIndexChanged" onfocus="disablecompany()"></asp:DropDownList>
                            <asp:RequiredFieldValidator
                                ID="rfvEmpName"
                                runat="server"
                                ControlToValidate="ddlcompany"
                                InitialValue="0"
                                ErrorMessage="Please select an Company."
                                CssClass="text-danger"
                                ValidationGroup="vgForm" />
                        </div>
                    </div>

                    <div class="col-sm-4">
                        <div class="input-block mb-3">
                            <label class="col-form-label">Branch<span class="text-danger">*</span></label>
                            <asp:DropDownList ID="ddlbranch" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlbranch_SelectedIndexChanged" onfocus="disablebranch()"></asp:DropDownList>
                        </div>
                    </div>

                    <div class="col-sm-4">
                        <div class="input-block mb-3">
                            <label class="col-form-label">Department<span class="text-danger">*</span></label>
                            <asp:DropDownList ID="ddldepartment" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddldepartment_SelectedIndexChanged" onfocus="disabledepartment()"></asp:DropDownList>
                        </div>
                    </div>
                </div>

                <div class="row">

                    <div class="col-sm-4">
                        <div class="input-block mb-3">
                            <label class="col-form-label">Employee Name<span class="text-danger">*</span></label>
                            <asp:DropDownList ID="ddlname" runat="server" CssClass="form-control" AutoPostBack="true" onfocus="disablename()"></asp:DropDownList>
                        </div>
                    </div>

                    <div class="col-sm-4">
                        <div class="input-block mb-3">
                            <label class="col-form-label">Month<span class="text-danger">*</span></label>
                            <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control" TextMode="Month" AutoPostBack="true" OnTextChanged="TextBox2_TextChanged" ></asp:TextBox>
                            <asp:RequiredFieldValidator
                                ID="RequiredFieldValidator3"
                                runat="server"
                                ControlToValidate="TextBox2"
                                ErrorMessage="Select Month."
                                CssClass="text-danger"
                                ValidationGroup="vgForm" />
                        </div>
                    </div>

                    <div class="col-sm-4">
                        <div class="submit-section">
                            <asp:Button ID="btngenaratepayroll" runat="server" Text="Generate" CssClass="btn btn-primary submit-btn" OnClick="btngenaratepayroll_Click" ValidationGroup="vgForm" />
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        function disablecompany() {
            var ddl = document.getElementById('<%= ddlcompany.ClientID %>');
            ddl.options[0].disabled = true;
        }
        function disablebranch() {
            var ddl = document.getElementById('<%= ddlbranch.ClientID %>');
            ddl.options[0].disabled = true;
        }
        function disabledepartment() {
            var ddl = document.getElementById('<%= ddldepartment.ClientID %>');
            ddl.options[0].disabled = true;
        }
        function disablename() {
            var ddl = document.getElementById('<%= ddlname.ClientID %>');
            ddl.options[0].disabled = true;
        }

    </script>

    <script type="text/javascript">
        document.addEventListener("DOMContentLoaded", function () {
            // Get the current date
            var today = new Date();
            var year = today.getFullYear();
            var month = today.getMonth(); // Subtract 1 to exclude the current month

            // Adjust the month value
            if (month < 10) month = '0' + month; // Add leading zero if necessary

            // If the current month is January (month = 0), move to the previous year and set December
            if (month === 0) {
                year -= 1;
                month = '12';
            }

            // Set the max attribute to the previous month
            var maxMonth = year + '-' + month;
            document.getElementById('<%= TextBox2.ClientID %>').setAttribute('max', maxMonth);
        });
    </script>
</asp:Content>
