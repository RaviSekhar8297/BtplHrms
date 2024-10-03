<%@ Page Title="" Language="C#" MasterPageFile="~/Employee.Master" AutoEventWireup="true" CodeBehind="EmplooyeeAttendance.aspx.cs" Inherits="Human_Resource_Management.EmplooyeeAttendance" %>

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
                            <h3 style="display: flex; align-items: center; justify-content: center; color: green;">
                                <asp:Label ID="Label2" runat="server" Font-Size="large" Text="0"></asp:Label></h3>
                        </div>
                        <div class="card-body">
                            <h5 class="card-title">Statistics</h5>
                            <div class="stats-list">
                                <div class="stats-info">
                                    <p style="color: green; font-size: 14px;">
                                        Today <strong>
                                            <asp:Label ID="lbltodayprogress" runat="server" Font-Size="Small"></asp:Label>
                                            /<span id="todaydaytime" style="color: green;"></span></strong>
                                    </p>
                                    <div class="progressaa">
                                        <progress id="presentprogress" value="0" max="100"></progress>
                                    </div>
                                </div>

                                <div class="stats-info">
                                    <p style="color: green; font-size: 14px;">
                                        This Week <strong>
                                            <asp:Label ID="lblweeklyprogress" runat="server" Font-Size="Small"></asp:Label>
                                            /<span id="todaydaytime1" style="color: green;"></span></strong>
                                    </p>
                                    <div class="progressaa">
                                        <progress id="presentprogress1" value="0" max="100"></progress>
                                    </div>
                                </div>


                                <div class="stats-info">
                                    <p style="color: green; font-size: 14px;">
                                        This Month <strong>
                                            <asp:Label ID="lblmonthlyprogress" runat="server" Font-Size="Small"></asp:Label>
                                            /<span id="todaydaytime3" style="color: green;"> </span></strong>
                                    </p>
                                    <div class="progressaa">
                                        <progress id="presentprogress2" value="0" max="100"></progress>
                                    </div>
                                </div>

                                <%--<div class="stats-info">
											<p style="color:green;font-size:14px;">Overtime <strong><asp:Label ID="lblovertimeprogress" runat="server" Font-Size="Small"></asp:Label></strong></p> 
											<div class="progressaa" >
									           <progress id="presentprogress3" value="0" max="100" ></progress>	
									         </div>
										</div>--%>
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
                <div class="col-sm-4">
                    <div class="input-block mb-3 form-focus select-focus">
                        <label class="focus-label">Select Start Date</label>
                        <asp:TextBox ID="txtstartdate" runat="server" TextMode="Date" CssClass="form-control floating datetimepicker" oninput="disableFutureDates(this)"></asp:TextBox>

                    </div>
                </div>
                <div class="col-sm-4">
                    <div class="input-block mb-3 form-focus select-focus">
                        <asp:TextBox ID="txtenddate" runat="server" TextMode="Date" CssClass="select form-control floating" oninput="disableFutureDates(this)"></asp:TextBox><%--oninput="limitToFourDigits(this)"--%>
                        <label class="focus-label">Select End Date</label>
                    </div>
                </div>
                <div class="col-sm-4">
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



    <script type="text/javascript">
        var todaytime = 9;
        var weeklytime = 54;
        var monthlytime =
            document.addEventListener("DOMContentLoaded", function () {

                let defaultDifferenceString = '<%= lbltodayprogress.Text %>';

                let timeParts = defaultDifferenceString.split(':');
                let todayhours = timeParts[0];
                console.log("to time is : " + todayhours)
                document.getElementById('<%= lbltodayprogress.ClientID %>').innerText = todayhours;

                let presentPer = document.getElementById("presentprogress");
                if (presentPer) {
                    let defaultDifference = parseInt(defaultDifferenceString) || 0;
                    let defaultDifferenceFormatted = defaultDifference < 10 ? "0" + defaultDifference.toString() : defaultDifference.toString();
                    let presentProgressValue = Math.trunc((defaultDifference / todaytime) * 100) || 0;

                    presentPer.value = presentProgressValue;
                    document.getElementById('todaydaytime').textContent = todaytime.toString();

                    let presentPer1212 = document.getElementById("presentprogress");
                }

            });

    </script>

    <script type="text/javascript">
        document.addEventListener("DOMContentLoaded", function () {
            let currentDate = new Date();
            let daysWithoutSundays = 0;
            let startDate = new Date(currentDate);
            startDate.setDate(startDate.getDate() - 7);

            for (let i = 0; i < 7; i++) {
                let currentDay = new Date(startDate);
                currentDay.setDate(currentDay.getDate() + i);

                // Check if the current day is not a Sunday (Sunday is represented by 0)
                if (currentDay.getDay() !== 0) {
                    daysWithoutSundays++;
                }
            }
            var multipliedValue = daysWithoutSundays * 9;

            let defaultDifferenceStringg = '<%= lblweeklyprogress.Text %>';

            let timeParts = defaultDifferenceStringg.split(':');
            let weekhours = timeParts[0];
            console.log("w time is : " + weekhours)
            document.getElementById('<%= lblweeklyprogress.ClientID %>').innerText = weekhours;

            let presentPer = document.getElementById("presentprogress1");
            if (presentPer) {
                let defaultDifference1 = parseInt(defaultDifferenceStringg) || 0;
                let presentProgressValue = Math.trunc((defaultDifference1 / multipliedValue) * 100) || 0;

                presentPer.value = presentProgressValue;
                document.getElementById('todaydaytime1').textContent = multipliedValue.toString();
            }
        });
    </script>

    <script type="text/javascript">
        document.addEventListener("DOMContentLoaded", function () {
            var currentDate = new Date();
            var currentMonth = currentDate.getMonth();
            var currentYear = currentDate.getFullYear();
            var daysWithoutSundays = 0;

            // Calculate the number of days in the current month excluding Sundays
            for (var day = 1; day <= new Date(currentYear, currentMonth + 1, 0).getDate(); day++) {
                var currentDay = new Date(currentYear, currentMonth, day);

                if (currentDay.getDay() !== 0) {
                    daysWithoutSundays++;
                }
            }

            var multipliedValue = daysWithoutSundays * 9;

            document.getElementById('todaydaytime3').textContent = multipliedValue.toString();

            var defaultDifferenceString = '<%= lblmonthlyprogress.Text %>';

            let timeParts = defaultDifferenceString.split(':');
            let monthhours = timeParts[0];
            console.log("m time is : " + monthhours)
            document.getElementById('<%= lblmonthlyprogress.ClientID %>').innerText = monthhours;


            var presentPer = document.getElementById("presentprogress2");

            if (presentPer) {
                var defaultDifference = parseInt(defaultDifferenceString) || 0;
                var presentProgressValue = Math.trunc((defaultDifference / multipliedValue) * 100) || 0;

                presentPer.value = presentProgressValue;
            }
        });
    </script>
    >
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
    </script>








    <%-- Api Key Working  code start -->

	<%--<script src="https://maps.googleapis.com/maps/api/js?key=YOUR_API_KEY"></script>
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
            document.getElementById('<%= Latitude.ClientID %>').value = latitude;
            document.getElementById('<%= Longitude.ClientID %>').value = longitude;

            console.log("Latitude: " + latitude);
            console.log("Longitude: " + longitude);
            getAddress(latitude, longitude);
        }

        function showError(error) {
            switch (error.code) {
                case error.PERMISSION_DENIED:
                    alert("User denied the request for Geolocation.");
                    break;
                case error.POSITION_UNAVAILABLE:
                    alert("Location information is unavailable.");
                    break;
                case error.TIMEOUT:
                    alert("The request to get user location timed out.");
                    break;
                case error.UNKNOWN_ERROR:
                    alert("An unknown error occurred.");
                    break;
            }
        }

        function getAddress(lat, lng) {
            var geocoder = new google.maps.Geocoder();
            var latlng = new google.maps.LatLng(lat, lng);

            geocoder.geocode({ 'location': latlng }, function (results, status) {
                if (status === 'OK') {
                    if (results[0]) {
                        console.log("Address: " + results[0].formatted_address);
                    } else {
                        console.log("No results found");
                    }
                } else {
                    console.log("Geocoder failed due to: " + status);
                }
            });
        }
        window.onload = getLocation;
    </script>--%>
</asp:Content>
