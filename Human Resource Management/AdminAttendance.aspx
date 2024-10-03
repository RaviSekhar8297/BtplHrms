<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="AdminAttendance.aspx.cs" Inherits="Human_Resource_Management.AdminAttendance" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .icon-column {
            display: flex;
            flex-direction: column;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Main Wrapper -->
    <div class="main-wrapper">
        <!-- Page Wrapper -->
        <div class="page-wrapper">
            <div class="content container-fluid">

                <!-- Page Header -->
                <div class="page-header">
                    <div class="row">
                        <div class="col-sm-12">
                            <h3 class="page-title">Attendance</h3>
                            <ul class="breadcrumb">
                                <li class="breadcrumb-item"><a href="Admindashboard">Dashboard</a></li>
                                <li class="breadcrumb-item active">Attendance</li>
                            </ul>
                        </div>
                    </div>
                </div>
                <!-- /Page Header -->

                <!-- Search Filter -->
                <div class="row filter-row">
                    <div class="col-sm-6 col-md-3">
                        <div class="input-block mb-3 form-focus select-focus">

                            <asp:TextBox ID="name" runat="server" CssClass="form-control" AutoCompleteType="Disabled" onkeypress="return isCharacterKey(event)"></asp:TextBox>
                            <label class="focus-label">Employee Name</label>
                        </div>
                    </div>
                    <div class="col-sm-6 col-md-3">
                        <div class="input-block mb-3 form-focus select-focus">
                            <asp:TextBox ID="TextBox2" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox>
                            <label class="focus-label">Select Month</label>
                        </div>
                    </div>
                    <div class="col-sm-6 col-md-3">
                        <div class="input-block mb-3 form-focus select-focus">

                            <asp:TextBox ID="TextBox3" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox>
                            <label class="focus-label">Select Year</label>
                        </div>
                    </div>
                    <div class="col-sm-6 col-md-3">
                        <div class="d-grid">
                            <asp:Button ID="btnsearch" runat="server" Text="Search" CssClass="btn btn-success" OnClick="btnsearch_Click" />
                        </div>
                    </div>
                </div>
                <!-- /Search Filter -->

                <div class="row">
                    <div class="col-lg-12">
                        <div class="table-responsive">
                            <table class="table table-striped custom-table table-nowrap mb-0">
                                <asp:PlaceHolder ID="AttendanceData" runat="server"></asp:PlaceHolder>

                            </table>
                        </div>
                    </div>
                </div>
            </div>
            <!-- /Page Content -->

            <!-- Attendance Modal -->
            <div class="modal custom-modal fade" id="attendance_info" role="dialog">
                <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title">Attendance Info</h5>
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="card punch-status">
                                        <div class="card-body">
                                            <h5 class="card-title">Timesheet <small class="text-muted">11 Mar 2019</small></h5>
                                            <div class="punch-det">
                                                <h6>Punch In at</h6>
                                                <p>Wed, 11th Mar 2019 10.00 AM</p>
                                            </div>
                                            <div class="punch-info">
                                                <div class="punch-hours">
                                                    <span>3.45 hrs</span>
                                                </div>
                                            </div>
                                            <div class="punch-det">
                                                <h6>Punch Out at</h6>
                                                <p>Wed, 20th Feb 2019 9.00 PM</p>
                                            </div>
                                            <div class="statistics">
                                                <div class="row">
                                                    <div class="col-md-6 col-6 text-center">
                                                        <div class="stats-box">
                                                            <p>Break</p>
                                                            <h6>1.21 hrs</h6>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6 col-6 text-center">
                                                        <div class="stats-box">
                                                            <p>Overtime</p>
                                                            <h6>3 hrs</h6>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="card recent-activity">
                                        <div class="card-body">
                                            <h5 class="card-title">Activity</h5>
                                            <ul class="res-activity-list">
                                                <li>
                                                    <p class="mb-0">Punch In at</p>
                                                    <p class="res-activity-time">
                                                        <i class="fa-regular fa-clock"></i>
                                                        10.00 AM.
                                                    </p>
                                                </li>
                                                <li>
                                                    <p class="mb-0">Punch Out at</p>
                                                    <p class="res-activity-time">
                                                        <i class="fa-regular fa-clock"></i>
                                                        11.00 AM.
                                                    </p>
                                                </li>
                                                <li>
                                                    <p class="mb-0">Punch In at</p>
                                                    <p class="res-activity-time">
                                                        <i class="fa-regular fa-clock"></i>
                                                        11.15 AM.
                                                    </p>
                                                </li>
                                                <li>
                                                    <p class="mb-0">Punch Out at</p>
                                                    <p class="res-activity-time">
                                                        <i class="fa-regular fa-clock"></i>
                                                        1.30 PM.
                                                    </p>
                                                </li>
                                                <li>
                                                    <p class="mb-0">Punch In at</p>
                                                    <p class="res-activity-time">
                                                        <i class="fa-regular fa-clock"></i>
                                                        2.00 PM.
                                                    </p>
                                                </li>
                                                <li>
                                                    <p class="mb-0">Punch Out at</p>
                                                    <p class="res-activity-time">
                                                        <i class="fa-regular fa-clock"></i>
                                                        7.30 PM.
                                                    </p>
                                                </li>
                                            </ul>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- /Attendance Modal -->

        </div>
        <!-- Page Wrapper -->
    </div>
    <!-- /Main Wrapper -->

    <script type="text/javascript">
        function isCharacterKey(event) {
            var charCode = (event.which) ? event.which : event.keyCode;
            // Allow: A-Z, a-z, and space (charCode 32 is space)
            if ((charCode >= 65 && charCode <= 90) || (charCode >= 97 && charCode <= 122) || charCode == 32) {
                console.log("Valid input, charCode: " + charCode);
                return true;
            } else {
                console.log("Invalid input, charCode: " + charCode);
                return false;
            }
        }
</script>
</asp:Content>
