<%@ Page Title="" Language="C#" MasterPageFile="~/Employee.Master" AutoEventWireup="true" CodeBehind="EmployeeLogins.aspx.cs" Inherits="Human_Resource_Management.EmployeeLogins" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0-beta3/css/all.min.css">
    <style type="text/css">
        .stats-info {
            background-color: #f8f9fa;
            border: 1px solid #dee2e6;
            padding: 10px;
            border-radius: 5px;
            position: relative;
            transition: transform 0.3s, box-shadow 0.3s;
        }

        .card-container {
            margin-bottom: 5px;
        }

        .stats-info:hover {
            transform: translateY(-3px);
            box-shadow: 0 3px 6px rgba(0, 0, 0, 0.1);
        }

        .stats-info h3 {
            margin: 0;
            position: relative;
        }

        .stats-info h6 {
            margin-bottom: 5px;
            position: relative;
        }

        .stats-info .icon-left {
            position: absolute;
            left: 10px;
            top: 50%;
            transform: translateY(-50%);
            font-size: 2em;
        }

        .stats-info .icon-bottom-right {
            position: absolute;
            right: 10px;
            bottom: 10px;
            font-size: 1.2em;
        }

        .stats-info h6 .icon-left,
        .stats-info h3 .icon-bottom-right {
            color: #007bff;
        }

        /* Different colors and icons */
        .card-total-days .icon-left {
            color: #76d7c4; /* Light Turquoise */
            content: "\f073"; /* Calendar */
        }

        .card-working-days .icon-left {
            color: #f5b7b1; /* Light Red */
            content: "\f0f4"; /* Calendar Check */
        }

        .card-week-off .icon-left {
            color: #85c1e9; /* Light Blue */
            content: "\f2e7"; /* Calendar Minus */
        }

        .card-holidays .icon-left {
            color: #f7dc6f; /* Light Yellow */
            content: "\f2d3"; /* Calendar Exclamation */
        }

        .card-presents .icon-left {
            color: #d7bde2; /* Light Purple */
            content: "\f2b9"; /* Calendar Alt */
        }

        .card-absents .icon-left {
            color: #d5dbdb; /* Light Gray */
            content: "\f2a0"; /* Calendar Times */
        }

        .card-lop .icon-left {
            color: #a3e4d7; /* Light Green */
            content: "\f2b5"; /* Calendar Week */
        }

        .card-late-logins .icon-left {
            color: #e6b0aa; /* Light Dark Red */
            content: "\f017"; /* Clock */
        }

        .card-half-day-count .icon-left {
            color: #abebc6; /* Light Green */
            content: "\f073"; /* Calendar Day */
        }

        .card-payable-days .icon-left {
            color: #aed6f1; /* Light Blue */
            content: "\f07a"; /* Calendar Plus */
        }

        .card-half-day-deduction .icon-left {
            color: #d2b4de; /* Light Purple */
            content: "\f2d2"; /* Calendar Times */
        }

        .card-professional-tax .icon-left {
            color: #f8c471; /* Light Dark Orange */
            content: "\f53d"; /* Money Bill Wave */
        }

        .card-monthly-salary .icon-left {
            color: #aed6f1; /* Light Blue */
            content: "\f155"; /* Dollar Sign */
        }

        .card-per-day-salary .icon-left {
            color: #f7b7a3; /* Light Orange */
            content: "\f3d1"; /* Coins */
        }

        .card-gross-salary .icon-left {
            color: #e6b0aa; /* Light Red */
            content: "\f53a"; /* Money Bill */
        }

        .card-net-salary .icon-left {
            color: #abebc6; /* Light Green */
            content: "\f4c3"; /* Money Check */
        }

        .card-pf-amount .icon-left {
            color: #f8c471; /* Light Dark Orange */
            content: "\f3c5"; /* Wallet */
        }

        .card-esi-amount .icon-left {
            color: #abebc6; /* Light Green */
            content: "\f53b"; /* Money Check */
        }

        .card-lop-amount .icon-left {
            color: #aeb6bf; /* Light Dark Blue */
            content: "\f571"; /* Money Check Alt */
        }

        .card-late-login-deduction .icon-left {
            color: #f5b7b1; /* Light Red */
            content: "\f53c"; /* Money Bill Wave */
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="main-wrapper">
        <!-- Page Wrapper -->
        <div class="page-wrapper">

            <!-- Page Content -->
            <div class="content container-fluid">

                <!-- Page Header -->
                <div class="page-header">
                    <div class="row align-items-center">
                        <div class="col">
                            <h3 class="page-title">Logins</h3>
                            <ul class="breadcrumb">
                                <li class="breadcrumb-item"><a href="EmployeeDashboard.aspx">Dashboard</a></li>
                                <li class="breadcrumb-item active">PayRoll</li>
                            </ul>
                        </div>

                    </div>
                </div>
                <!-- /Page Header -->

                <div class="row">
                    <div class="col-md-3">
                        <div class="input-block mb-3">
                            <label class="col-form-label">Month</label>
                            <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control" TextMode="Month" AutoPostBack="true" OnTextChanged="TextBox1_TextChanged"></asp:TextBox>

                        </div>
                    </div>
                </div>

                <div class="row">
                    <!--Total days-->
                    <div class="col-md-3 card-container">
                        <div class="stats-info card-total-days">
                            <i class="fas fa-calendar icon-left"></i>
                            <h6 style="color: grey;">Total days</h6>
                            <h3>
                                <asp:Label ID="lbltotaldays" runat="server" Text="0"></asp:Label>
                            </h3>
                        </div>
                    </div>
                    <!--/Total days-->

                    <!--Working days-->
                    <div class="col-md-3 card-container">
                        <div class="stats-info card-working-days">
                            <i class="fas fa-business-time icon-left"></i>
                            <h6 style="color: grey;">Working days</h6>
                            <h3>
                                <asp:Label ID="lblworkingdays" runat="server" Text="0"></asp:Label>
                            </h3>
                        </div>
                    </div>
                    <!--/Working days-->

                    <!--Week Off-->
                    <div class="col-md-3 card-container">
                        <div class="stats-info card-week-off">
                            <i class="fas fa-bed icon-left"></i>
                            <h6 style="color: grey;">Week Off</h6>
                            <h3>
                                <asp:Label ID="lblweekoffs" runat="server" Text="0"></asp:Label>
                            </h3>
                        </div>
                    </div>
                    <!--/Week Off-->

                    <!--Holidays-->
                    <div class="col-md-3 card-container">
                        <div class="stats-info card-holidays">
                            <i class="fas fa-umbrella-beach icon-left"></i>
                            <h6 style="color: grey;">Holidays</h6>
                            <h3>
                                <asp:Label ID="lblholidays" runat="server" Text="0"></asp:Label>
                            </h3>
                        </div>
                    </div>
                    <!--/Holidays-->

                    <!--Presents-->
                    <div class="col-md-3 card-container">
                        <div class="stats-info card-presents">
                            <i class="fas fa-calendar-alt icon-left"></i>
                            <h6 style="color: grey;">Presents</h6>
                            <h3>
                                <asp:Label ID="lblpresents" runat="server" Text="0"></asp:Label>
                            </h3>
                        </div>
                    </div>
                    <!--/Presents-->

                    <!--Absents-->
                    <div class="col-md-3 card-container">
                        <div class="stats-info card-absents">
                            <i class="fas fa-calendar-times icon-left"></i>
                            <h6 style="color: grey;">Absents</h6>
                            <h3>
                                <asp:Label ID="lblabsents" runat="server" Text="0"></asp:Label>
                            </h3>
                        </div>
                    </div>
                    <!--/Absents-->

                    <!--Perday Salary-->
                    <div class="col-md-3 card-container">
                        <div class="stats-info card-per-day-salary">
                            <i class="fas fa-coins icon-left"></i>
                            <h6 style="color: grey;">Per Day Salary</h6>
                            <h3>
                                <asp:Label ID="lblperdaysalary" runat="server" Text="0"></asp:Label>
                            </h3>
                        </div>
                    </div>
                    <!--/Perday Salary-->

                    <!--Half Day Presents-->
                    <div class="col-md-3 card-container">
                        <div class="stats-info card-presents">
                            <i class="fas fa-calendar-day icon-left"></i>
                            <h6 style="color: grey;">Half Day Presents</h6>
                            <h3>
                                <asp:Label ID="lblhalfdaycount" runat="server" Text="0"></asp:Label>
                            </h3>
                        </div>
                    </div>
                    <!--/Half Day Presents-->

                    <!--Half Day Salary-->
                    <div class="col-md-3 card-container">
                        <div class="stats-info card-lop">
                            <i class="fas fa-adjust icon-left"></i>
                            <h6 style="color: grey;">Half Day Deduction</h6>
                            <h3>
                                <asp:Label ID="lblhalfdayamount" runat="server" Text="0"></asp:Label>
                            </h3>
                        </div>
                    </div>
                    <!--/Half Day Salary-->

                    <!--Gross salary-->
                    <div class="col-md-3 card-container">
                        <div class="stats-info card-monthly-salary">
                            <i class="fas fa-dollar-sign icon-left"></i>
                            <h6 style="color: grey;">Gross salary</h6>
                            <h3>
                                <asp:Label ID="lblmonthsalary" runat="server" Text="0"></asp:Label>
                            </h3>
                        </div>
                    </div>
                    <!--/Gross salary-->

                    <!--Earned Gross-->
                    <div class="col-md-3 card-container">
                        <div class="stats-info card-gross-salary">
                            <i class="fas fa-money-bill icon-left"></i>
                            <h6 style="color: grey;">Earned Gross</h6>
                            <h3>
                                <asp:Label ID="lblgrosssalary" runat="server" Text="0"></asp:Label>
                            </h3>
                        </div>
                    </div>
                    <!--/Earned Gross-->

                    <!--Payble Days-->
                    <div class="col-md-3 card-container">
                        <div class="stats-info card-absents">
                            <i class="fas fa-calendar-plus icon-left"></i>
                            <h6 style="color: grey;">Payble Days</h6>
                            <h3>
                                <asp:Label ID="lblpaybledays" runat="server" Text="0"></asp:Label>
                            </h3>
                        </div>
                    </div>
                    <!--/Payble Days-->

                    <!--Net Salary-->
                    <div class="col-md-3 card-container">
                        <div class="stats-info card-net-salary">
                            <i class="fas fa-indian-rupee-sign icon-left"></i>
                            <h6 style="color: grey;">Net Salary</h6>
                            <h3>
                                <asp:Label ID="lblnetsalary" runat="server" Text="0"></asp:Label>
                            </h3>
                        </div>
                    </div>
                    <!--Net Salary-->

                    <!--Late Logins-->
                    <div class="col-md-3 card-container">
                        <div class="stats-info card-late-logins">
                            <i class="fas fa-clock icon-left"></i>
                            <h6 style="color: grey;">Late Logins</h6>
                            <h3>
                                <asp:Label ID="lbllatelogincount" runat="server" Text="0"></asp:Label>
                            </h3>
                        </div>
                    </div>
                    <!--/Late Logins-->

                    <!--Late login Deduction-->
                    <div class="col-md-3 card-container">
                        <div class="stats-info card-late-login-deduction">
                            <i class="fas fa-minus-circle icon-left"></i>
                            <h6 style="color: grey;">Late login Deduction</h6>
                            <h3>
                                <asp:Label ID="lbllatelogamount" runat="server" Text="0"></asp:Label>
                            </h3>
                        </div>
                    </div>
                    <!--/Late login Deduction-->

                    <!--LOP-->
                    <div class="col-md-3 card-container">
                        <div class="stats-info card-lop">
                            <i class="fas fa-calendar-week icon-left"></i>
                            <h6 style="color: grey;">LOP</h6>
                            <h3>
                                <asp:Label ID="lbllop" runat="server" Text="0"></asp:Label>
                            </h3>
                        </div>
                    </div>
                    <!--/LOP-->

                    <!--Lop Amount-->
                    <div class="col-md-3 card-container">
                        <div class="stats-info card-lop-amount">
                            <i class="fas fa-money-check-alt icon-left"></i>
                            <h6>Lop Amount</h6>
                            <h3>
                                <asp:Label ID="lbllopamount" runat="server" Text="0"></asp:Label>
                            </h3>
                        </div>
                    </div>
                    <!--/Lop Amount-->

                    <!--ProfessionalTax-->
                    <div class="col-md-3 card-container">
                        <div class="stats-info card-late-logins">
                            <i class="fas fa-receipt icon-left"></i>
                            <h6 style="color: grey;">ProfessionalTax</h6>
                            <h3>
                                <asp:Label ID="lblproftax" runat="server" Text="0"></asp:Label>
                            </h3>
                        </div>
                    </div>
                    <!--/ProfessionalTax-->

                    <!--PF Amount-->
                    <div class="col-md-3 card-container">
                        <div class="stats-info card-pf-amount">
                            <i class="fas fa-wallet icon-left"></i>
                            <h6 style="color: grey;">PF Amount</h6>
                            <h3>
                                <asp:Label ID="lblpfamount" runat="server" Text="0"></asp:Label>
                            </h3>
                        </div>
                    </div>
                    <!--/PF Amount-->

                    <!--ESI Amount-->
                    <div class="col-md-3 card-container">
                        <div class="stats-info card-esi-amount">
                            <i class="fas fa-shield-alt icon-left"></i>
                            <h6 style="color: grey;">ESI Amount</h6>
                            <h3>
                                <asp:Label ID="lblesiamount" runat="server" Text="0"></asp:Label>
                            </h3>
                        </div>
                    </div>
                    <!--/ESI Amount-->
                </div>
                <div class="row">
                    <!--Total days-->
                    <div class="col-md-3 card-container">
                        <div class="stats-info card-total-days">
                            <i class="fas fa-calendar icon-left"></i>
                            <h6 style="color: grey;">TDS Tax</h6>
                            <h3>
                                <asp:Label ID="lbltdsamount" runat="server" Text="0"></asp:Label>
                            </h3>
                        </div>
                    </div>
                    <!--/Total days-->

                    <!--Loan -->
                    <div class="col-md-3 card-container">
                        <div class="stats-info card-total-days">
                            <i class="fas fa-calendar icon-left"></i>
                            <h6 style="color: grey;">Loan</h6>
                            <h3>
                                <asp:Label ID="lblloan" runat="server" Text="0"></asp:Label>
                            </h3>
                        </div>
                    </div>
                    <!--/Loan -->
                </div>
            </div>
        </div>
    </div>
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

            var maxMonth = year + '-' + month;
            document.getElementById('<%= TextBox1.ClientID %>').setAttribute('max', maxMonth);
        });
    </script>
</asp:Content>
