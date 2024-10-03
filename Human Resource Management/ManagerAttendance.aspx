<%@ Page Title="" Language="C#" MasterPageFile="~/Manager.Master" AutoEventWireup="true" CodeBehind="ManagerAttendance.aspx.cs" Inherits="Human_Resource_Management.ManagerAttendance" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.4/css/all.min.css" rel="stylesheet">

    <style type="text/css">
        .progressaa {
            width: 100%;
        }

            .progressaa > progress {
                width: 100% !important;
            }

        #presentprogress {
            background-color: lightgrey;
            height: 3px;
            border-radius: 5px;
        }

            #presentprogress::-webkit-progress-bar {
                background-color: lightgrey;
                height: 3px;
                border-radius: 5px;
            }

            #presentprogress::-webkit-progress-value {
                background-color: green;
                height: 3px;
                border-radius: 6px;
            }

            #presentprogress::-moz-progress-bar {
                background-color: green;
                height: 3px;
                border-radius: 5px;
            }

        .dash:hover {
            color: blue;
        }

        .see-attendance {
            width: 100%;
            /*border:3px solid red;*/
            display: flex;
            flex-direction: row;
            justify-content: space-between;
            align-items: center;
            height: 50px;
        }

        .excel-con {
            font-size: 14px;
            font-weight: 500;
            /*font-family: monospace;*/
        }

        .btnexcelclass {
            border: none;
            color: white;
            background-color: green;
            outline: none;
            border-radius: 5px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Page Wrapper -->
    <div class="page-wrapper">
        <div class="content container-fluid">

            <!-- Page Header -->
            <div class="page-header">
                <div class="row">
                    <div class="col-sm-12">
                        <h3 class="page-title">Attendance</h3>
                        <ul class="breadcrumb">
                            <li class="breadcrumb-item"><a href="EmployeeDashboard.aspx">Dashboard</a></li>
                            <li class="breadcrumb-item active">Attendance</li>
                        </ul>
                    </div>
                </div>
            </div>
            <!-- /Page Header -->

            <div class="row">
                <div class="col-md-4">
                    <div class="card punch-status">
                        <div class="card-body">
                            <h5 class="card-title">Timesheet <small class="text-muted"></small></h5>
                            <h5 class="card-title"><small class="text-muted">
                                <asp:Label ID="lbledatetime" runat="server" ForeColor="Red"></asp:Label></small></h5>
                            <div class="punch-det">
                                <h6>Punch In at</h6>
                                <p>
                                    <asp:Label ID="lblpunchdatettime" runat="server" ForeColor="Red"></asp:Label>
                                </p>
                            </div>
                            <div class="punch-info">
                                <div class="punch-hours" style="border: 1px solid red;">
                                    <span>
                                        <asp:Label ID="Label1" runat="server" ForeColor="green"></asp:Label></span>
                                   
                                </div>
                            </div>
                            <div class="punch-btn-section">
                                <asp:Button ID="btnpunchout" runat="server" Text="Punch In/Out" CssClass="btn btn-primary punch-btn" OnClick="btnpunchout_Click" OnClientClick="" />
                            </div>
                            <div class="statistics">
                                <div class="row">
                                    <div class="col-md-6 col-6 text-center">
                                        <div class="stats-box">
                                            <p>Late LogIn Time</p>
                                            <h6>
                                                <asp:Label ID="lbllatelogintime" runat="server" ForeColor="Red"></asp:Label></h6>
                                        </div>
                                    </div>
                                    <div class="col-md-6 col-6 text-center">
                                        <div class="stats-box">
                                            <p>Overtime</p>
                                            <h6>
                                                <asp:Label ID="lblovertime" runat="server" ForeColor="Green"></asp:Label></h6>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="card att-statistics">
                        <div class="card-body">
                            <h5 class="card-title">Department Attendance</h5>
                            <div class="stats-list">
                                <div class="stats-info" style="overflow-y: scroll; height: 330px;">
                                    <div class="name-time">
                                        <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="card recent-activity">
                        <div class="card-body">
                            <h5 class="card-title">Today Activity</h5>
                            <asp:PlaceHolder ID="BindAttendanceContainer" runat="server"></asp:PlaceHolder>
                            <%--<ul class="res-activity-list">
										<li>
											<p class="mb-0">Punch In at</p>
											<p class="res-activity-time">
												<i class="fa-regular fa-clock"></i>
												label1
											</p>
										</li>
										<li>
											<p class="mb-0">Punch Out at</p>
											<p class="res-activity-time">
												<i class="fa-regular fa-clock"></i>
												label2
											</p>
										</li>
										
									</ul>--%>
                        </div>
                    </div>
                </div>
            </div>

            <div class="see-attendance">
                <h3 style="color: #5379b5;">Date Wise Attendance</h3>
                <div>
                    <asp:Button ID="btnexcel" runat="server" Text="Excel" OnClick="btnexcel_Click" CssClass="btnexcelclass" />
                    <%--<i class="far fa-file-excel excel-con"> </i>--%>
                    <asp:GridView ID="GridView1" runat="server">
                    </asp:GridView>
                </div>
            </div>

            <!-- Search Filter -->

            <div class="row filter-row">
                <div class="col-sm-3">
                    <div class="input-block mb-3 form-focus select-focus">
                        <label class="focus-label">Select Start Date</label>
                        <asp:DropDownList ID="ddlname" runat="server" CssClass="form-control floating datetimepicker" onfocus="ddladdnamedisable()"></asp:DropDownList>
                    </div>
                </div>
                <div class="col-sm-3">
                    <div class="input-block mb-3 form-focus select-focus">
                        <label class="focus-label">Select Start Date</label>
                        <asp:TextBox ID="txtstartdate" runat="server" TextMode="Date" CssClass="form-control floating datetimepicker" oninput="disableFutureDates(this)"></asp:TextBox>

                    </div>
                </div>
                <div class="col-sm-3">
                    <div class="input-block mb-3 form-focus select-focus">
                        <asp:TextBox ID="txtenddate" runat="server" TextMode="Date" CssClass="select form-control floating" oninput="disableFutureDates(this)"></asp:TextBox><%--oninput="limitToFourDigits(this)"--%>
                        <label class="focus-label">Select End Date</label>
                    </div>
                </div>
                <div class="col-sm-3">
                    <div class="d-grid">
                        <asp:Button ID="btnattendancesearch" runat="server" Text="Search..." CssClass="btn btn-success" OnClick="btnattendancesearch_Click" />
                    </div>
                </div>
            </div>
            <!-- /Search Filter -->
            <div class="row">
                <div class="col-lg-12">
                    <div class="table-responsive">
                        <asp:Panel ID="tablePanel" runat="server">
                            <table class="table table-striped custom-table mb-0">
                                <thead>
                                    <tr>
                                        <th>S.No</th>
                                        <th>Name</th>
                                        <th>Date </th>
                                        <th>Punch In</th>
                                        <th>Punch Out</th>
                                        <th>Duration</th>
                                        <th>LateLogIn</th>
                                        <th>Overtime</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:PlaceHolder ID="dataAttendanceContainer" runat="server"></asp:PlaceHolder>
                                    <asp:Literal ID="AttendanceLiteral" runat="server"></asp:Literal>

                                </tbody>
                            </table>
                        </asp:Panel>
                    </div>
                </div>
            </div>

        </div>
        <!-- /Page Content -->
        <asp:HiddenField ID="Latitude" runat="server" />
        <asp:HiddenField ID="Longitude" runat="server" />
    </div>
    <!-- Page Wrapper -->
    <style type="text/css">
        .name-time {
            display: flex;
            justify-content: space-between;
        }

        .stats-info {
            margin: 10px;
        }

        .name-time {
            display: flex;
            flex-direction: column;
            align-items: flex-start;
        }

        .stats-row {
            display: flex;
            justify-content: space-between;
            width: 300px; /* Adjust as needed */
            margin-bottom: 5px;
            height: 38px;
            align-items: center;
            border-bottom: 0.5px solid #f2f3f5;
            padding: 2px;
        }

        .employee-name {
            color: green;
            /* font-size: 16px;*/
        }

        .employee-time {
            color: green;
            /* font-size: 130px;*/
        }
    </style>

    <script type="text/javascript">
        // select Year
        function limitToFourDigits(input) {
            input.value = input.value.replace(/\D/g, '');
            if (input.value.length > 4) {
                input.value = input.value.slice(0, 4);
            }
        }

        // disable futer dates
        function disableFutureDates(input) {
            var today = new Date();
            var enteredDate = new Date(input.value);

            if (enteredDate > today) {
                input.value = '';
                alert("Please select a date that is not in the future.");
            }
        }
    </script>



    <script type="text/javascript">
        function getLocation() {
            if (navigator.geolocation) {
                navigator.geolocation.getCurrentPosition(showPosition, showError);
            } else {
                alert("Geolocation is not supported by this browser.");
            }
        }

        function showPosition(position) {
            var latitude = position.coords.latitude;
            var longitude = position.coords.longitude;

            // Set the hidden field values
            document.getElementById('<%= Latitude.ClientID %>').value = latitude;
            document.getElementById('<%= Longitude.ClientID %>').value = longitude;
        }

        function showError(error) {
            switch (error.code) {
                case error.PERMISSION_DENIED:
                    alert("User denied the request for Geolocation.");
                    hidePunchButton();
                    break;
                case error.POSITION_UNAVAILABLE:
                    alert("Location information is unavailable.");
                    hidePunchButton();
                    break;
                case error.TIMEOUT:
                    alert("The request to get user location timed out.");
                    hidePunchButton();
                    break;
                case error.UNKNOWN_ERROR:
                    alert("An unknown error occurred.");
                    hidePunchButton();
                    break;
            }
        }

        function hidePunchButton() {
            var btnpunchout = document.getElementById('<%= btnpunchout.ClientID %>');
            if (btnpunchout) {
                btnpunchout.style.display = 'none';
            }
        }

        window.onload = getLocation;



        function ddladdnamedisable() {
            var ddl = document.getElementById('<%= ddlname.ClientID %>');
            ddl.options[0].disabled = true;
        }

    </script>
    <style type="text/css">
        .punch-hours {
            width: 0; /* Initially 0 width */
            height: 30px;
            background-color: green;
            transition: width 0.5s ease; /* Smooth transition effect */
        }
    </style>
   


</asp:Content>
