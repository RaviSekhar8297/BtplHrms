<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="AdminTimeSheet.aspx.cs" Inherits="Human_Resource_Management.AdminTimeSheet1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .icon-column {
            display: flex;
            flex-direction: column;
        }
         .row>* {
            /*flex-shrink: 0;
            width: 100%;*/
            max-width: 100%;
            padding-right: calc(var(--bs-gutter-x)* 1);
            padding-left: calc(var(--bs-gutter-x)* 1);
            margin-top: var(--bs-gutter-y);
        }

        *, ::after, ::before {
            box-sizing: border-box;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Main Wrapper -->
    <div class="main-wrapper">
        <!-- Page Wrapper -->
        <!-- Page Wrapper -->
        <div class="page-wrapper">
            <div class="content container-fluid">

                <!-- Page Header -->
                <div class="page-header">
                    <div class="row">
                        <div class="col-sm-12">
                            <h3 class="page-title">Time Sheet</h3>
                            <ul class="breadcrumb">
                                <li class="breadcrumb-item"><a href="Admindashboard.aspx">Dashboard</a></li>
                                <li class="breadcrumb-item active">Time Sheet</li>
                            </ul>
                        </div>
                    </div>
                </div>
                </div>
                <!-- /Page Header -->

                <!-- Search Filter -->
                <div class="row filter-row">
                    <div class="col-sm-6 col-md-3">
                        <div class="input-block mb-3 form-focus select-focus">
                            <asp:TextBox ID="TextBox3" runat="server" CssClass="form-control floating" AutoCompleteType="Disabled" onkeypress="return isNumberKey(event)"></asp:TextBox>
                            <label class="focus-label">Search By Id</label>
                        </div>
                    </div>
                    <div class="col-sm-6 col-md-3">
                        <div class="input-block mb-3 form-focus select-focus">
                            <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control floating" AutoCompleteType="Disabled" TextMode="Date"></asp:TextBox>
                            <label class="focus-label">Start  Date</label>
                        </div>
                    </div>
                    <div class="col-sm-6 col-md-3">
                        <div class="input-block mb-3 form-focus select-focus">

                            <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control floating" AutoCompleteType="Disabled" TextMode="Date"></asp:TextBox>
                            <label class="focus-label">End Date</label>
                        </div>
                    </div>
                    <div class="col-sm-6 col-md-3">                       
                            <asp:Button ID="Button1" runat="server" Text="Search" CssClass="btn btn-success w-100" OnClick="Button1_Click" />
                    </div>

                    <%-- <div class="col-sm-6 col-md-3">
                        <div class="input-block mb-3 form-focus select-focus">
                            <select class="select floating form-control">
                                <option>-</option>
                                <option>2019</option>
                                <option>2018</option>
                                <option>2017</option>
                                <option>2016</option>
                                <option>2015</option>
                            </select>
                            <label class="focus-label">Select Year</label>
                        </div>
                    </div>
                    <div class="col-sm-6 col-md-3">
                        <div class="d-grid">
                            <a href="#" class="btn btn-success">Search </a>
                        </div>
                    </div>--%>
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

    <script type="text/javascript">
        function isNumberKey(event) {
            var charCode = (event.which) ? event.which : event.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
                console.log("number is " + charCode);
            }
            return true;
        }
    </script>
     


</asp:Content>
