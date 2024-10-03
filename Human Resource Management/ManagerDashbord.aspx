<%@ Page Title="" Language="C#" MasterPageFile="~/Manager.Master" AutoEventWireup="true" CodeBehind="ManagerDashbord.aspx.cs" Inherits="Human_Resource_Management.ManagerDashbord" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        /* Customize the PieChart styling */
        .hover-effect {
            cursor: pointer;
        }

        /* Tooltip styling */
        .tooltip {
            position: absolute;
            background-color: rgba(51, 51, 51, 0.8);
            color: #fff;
            padding: 5px;
            border-radius: 3px;
            font-size: 12px;
            visibility: hidden;
            z-index: 1000;
        }

        /* 3D-like Pie Chart styling */
        .chart-container {
            position: relative;
            width: 49%;
            height: 300px;
            margin: auto;
            border-radius:20px;
            background-color:white;
        }

        .chart-animate {
            animation: rotate 10s infinite linear;
        }

        /* Container for the entire employee card layout */
        .parent-container {
            display: flex;
            flex-wrap: wrap;
            gap: 1rem; /* Adjust spacing between cards */
            justify-content: space-between; /* Distribute space between cards */
        }

        /* Card style */
        .employee-card {
            display: flex;
            flex-direction: column;
            align-items: center;
            justify-content: center;
            width: calc(50% - 1rem); /* 2 cards per row with gap */
            background-color: #fff;
            border: 1px solid #ddd;
            border-radius: 10px;
            box-shadow: 0 2px 5px rgba(0, 0, 0, 0.1);
            padding: 1rem;
            box-sizing: border-box;
            position: relative; /* For positioning the logo */
        }

        /* Logo style */
        .company-logo {
            position: absolute;
            top: 10px;
            left: 10px;
            width: 50px; /* Adjust size as needed */
            height: 25px; /* Adjust size as needed */
        }

        /* Image style inside the card */
        .employee-image {
            width: 100px; /* Adjust size as needed */
            height: 100px; /* Adjust size as needed */
            border-radius: 50%;
            object-fit: cover;
            margin-bottom: 0.5rem;
        }

        /* Name style */
        .employee-name {
            font-size: 18px;
            color: green;
            margin-bottom: 0.5rem;
        }

        /* Anniversary style */
        .employee-anniversary {
            font-size: 15px;
            color: #555;
        }

        .employee-container:hover {
            border-color: blue;
            background-color: #aae3d4;
            color: black;
        }

        .add-btn1 {
            background-color: #ff9b44;
            border: 1px solid #ff9b44;
            color: #ffffff;
            font-weight: 500;
            min-width: 140px;
            border-radius: 50px;
        }

            .add-btn1 i {
                margin-right: 5px;
            }

            .add-btn1:hover {
                color: #ffffff;
                background-color: #ff9b44;
                border: 1px solid #ff9b44;
            }

        /* Scrollable section styles */
        .scrollable-section {
            max-height: 370px;
            min-height: auto;
            overflow-y: auto;
        }

        .Designation {
            font-size: 12px;
        }

        /* No Birthdays/Anniversaries Image Styles */
        .no-birthdays-image,
        .no-anniversaries-image {
            display: block;
            margin: 0 auto;
            max-width: 100%;
            height: 250px;
        }

        .stats-info {
            background-color: #fff;
            padding: 10px;
            border-radius: 8px;
            box-shadow: 0 2px 5px rgba(0,0,0,0.1);
            margin-bottom: 20px;
            position: relative;
            min-height: 120px;
            display: flex;
            flex-direction: column;
            justify-content: space-between;
        }

        .stats-header {
            display: flex;
            justify-content: space-between; /* Ensure text and icon are spaced */
            align-items: center; /* Align icon and text vertically */
            margin-bottom: 10px;
        }

            .stats-header h6 {
                font-size: 14px;
                color: #888;
                margin: 0; /* Remove margin for tighter layout */
            }

            .stats-header h3 {
                font-size: 20px;
                font-weight: bold;
                margin-bottom: 5px;
            }

            .stats-header .icon {
                font-size: 30px;
                color: rgba(255,255,255,0.8); /* Icon color */
            }

        .stats-info .divider {
            width: 100%;
            height: 1px;
            background-color: #ddd;
            margin: 10px 0;
        }

        .stats-info .update-time {
            font-size: 12px;
            color: #888;
            position: absolute;
            bottom: 5px;
            left: 10px;
        }


        /* Color schemes for each card */
        .bg-blue {
            background-color: #4f9eed;
        }
        /* Total Projects */
        .bg-green {
            background-color: #70cf97;
        }
        /* Completed */
        .bg-orange {
            background-color: #f6b493;
        }
        /* Working */
        .bg-red {
            background-color: #f79995;
        }
        /* Pending */


        .bg-orange {
            background-color: #f6b493;
        }
        /* Similar to the first card */
        .bg-green {
            background-color: #70cf97;
        }
        /* Similar to the second card */
        .bg-red {
            background-color: #f79995;
        }
        /* Similar to the third card */
        .bg-blue {
            background-color: #64c3d6;
        }
        /* Similar to the fourth card */
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="main-wrapper">
        <div class="page-wrapper">
            <div class="content container-fluid">
                <div class="row">
                    <div class="col-md-12">
                        <div class="welcome-box">
                            <div class="welcome-img">
                                <asp:Image ID="Image1" runat="server" CssClass="image1" />
                            </div>
                            <!--Name & Date Start-->
                            <div class="welcome-det">
                                <h3>Welcome
                                    <asp:Label ID="lblname" runat="server" ForeColor="Red"></asp:Label></h3>
                                <p>
                                    <asp:Label ID="lbldesignation" runat="server" ForeColor="green"></asp:Label>
                                </p>
                            </div>
                            <!--Name & Date End-->
                        </div>
                        <!-- Projects list Start -->
                    </div>
                </div>


                <div class="row">
                    <div class="col-md-6 col-sm-6 col-lg-6 col-xl-3">
                        <div class="stats-info bg-blue">
                            <!-- Text on the left and icon on the right -->
                            <div class="stats-header">
                                <div>
                                    <h3><asp:Label ID="Label1" runat="server" ForeColor="White" Text="0"></asp:Label></h3>
                                    <h6>
                                        Total Projects
                                    </h6>
                                </div>
                                <div class="icon">
                                    <i class="fa fa-bar-chart"></i>
                                </div>
                            </div>
                            <div class="divider"></div>
                            <span class="update-time"></span>
                        </div>
                    </div>

                    <div class="col-md-6 col-sm-6 col-lg-6 col-xl-3">
                        <div class="stats-info bg-green">
                            <div class="stats-header">
                                <div>
                                    <h3>
                                        <asp:Label ID="Label2" runat="server" ForeColor="White" Text="0"></asp:Label>
                                    </h3>
                                    <h6>
                                        Completed      
                                    </h6>
                                </div>
                                <div class="icon">
                                    <i class="fa fa-bar-chart"></i>
                                </div>
                            </div>
                            <div class="divider"></div>
                            <span class="update-time"></span>
                        </div>
                    </div>

                    <div class="col-md-6 col-sm-6 col-lg-6 col-xl-3">
                        <div class="stats-info bg-orange">
                            <div class="stats-header">
                                <div>
                                    <h3>
                                         <asp:Label ID="Label4" runat="server" ForeColor="White" Text="0"></asp:Label>
                                    </h3>
                                    <h6>
                                        Working          
                                    </h6>
                                </div>
                                <div class="icon">
                                    <i class="fa fa-bar-chart"></i>
                                </div>
                            </div>
                            <div class="divider"></div>
                            <span class="update-time"></span>
                        </div>
                    </div>

                    <div class="col-md-6 col-sm-6 col-lg-6 col-xl-3">
                        <div class="stats-info bg-red">
                            <div class="stats-header">
                                <div>
                                    <h3>
                                        <asp:Label ID="Label3" runat="server" ForeColor="White" Text="0"></asp:Label>
                                    </h3>
                                    <h6>
                                        Pending         
                                    </h6>
                                </div>
                                <div class="icon">
                                    <i class="fa fa-bar-chart"></i>
                                </div>
                            </div>
                            <div class="divider"></div>
                            <span class="update-time"></span>
                        </div>
                    </div>
                </div>
                <!-- Projects list end -->

               
                <div class="row">
                    <div class="col-md-6 chart-container">
                        <asp:Chart ID="Chart1" runat="server" Width="450" Height="300" CssClass="chart-animate hover-effect">
                            <ChartAreas>
                                <asp:ChartArea Name="ChartArea1">
                                    <InnerPlotPosition Auto="False" Height="70" Width="70" X="15" Y="15" />
                                </asp:ChartArea>
                            </ChartAreas>
                            <Series>
                                <asp:Series Name="Series1" ChartType="Doughnut" ChartArea="ChartArea1" />
                            </Series>
                            <Legends>
                                <asp:Legend Name="Legend1" />
                            </Legends>
                        </asp:Chart>
                    </div>

                    <div class="col-md-6 chart-container">
                        <asp:Chart ID="Chart2" runat="server" Width="450" Height="300" CssClass="chart-animate hover-effect">
                            <ChartAreas>
                                <asp:ChartArea Name="ChartArea2">
                                    <InnerPlotPosition Auto="False" Height="70" Width="70" X="15" Y="15" />
                                </asp:ChartArea>
                            </ChartAreas>
                            <Series>
                                <asp:Series Name="Series2" ChartType="Doughnut" ChartArea="ChartArea2" />
                            </Series>
                            <Legends>
                                <asp:Legend Name="Legend2" />
                            </Legends>
                        </asp:Chart>
                    </div>
                </div>
                <br />
                <div class="row">
                    <!--Notifications Start-->
                    <div class="col-lg-8 col-md-8">
                        <section class="dash-section">
                            <h1 class="dash-sec-title">Today - BirthDay 🎂</h1>
                            <div class="card scrollable-section" style="gap: 2rem; padding: 10px;">
                                <asp:PlaceHolder ID="birthdayContainer" runat="server"></asp:PlaceHolder>
                            </div>
                        </section>

                        <section class="dash-section">
                            <h1 class="dash-sec-title">TODAY - ANNIVERSARY</h1>
                            <div class="card scrollable-section" style="gap: 1rem; padding: 10px;">
                                <asp:PlaceHolder ID="AnniversaryContainer" runat="server"></asp:PlaceHolder>
                            </div>
                        </section>
                    </div>


                    <div class="col-lg-4 col-md-4">
                        <div class="dash-sidebar">
                            <section>
                            </section>
                            <section>
                                <h5 class="dash-title">Your Leave</h5>
                                <div class="card">
                                    <div class="card-body">
                                        <div class="time-list">
                                            <div class="dash-stats-list">
                                                <h4>
                                                    <asp:Label ID="Label5" runat="server" Text="Label" ForeColor="Red"></asp:Label></h4>
                                                <p>Leave Taken</p>
                                            </div>
                                            <div class="dash-stats-list">
                                                <h4>
                                                    <asp:Label ID="Label6" runat="server" Text="Label" ForeColor="Green"></asp:Label></h4>
                                                <p>Remaining</p>
                                            </div>
                                        </div>
                                        <div class="request-btn">
                                            <%--                                        <a class="btn add-btn1" href="#" data-bs-toggle="modal" data-bs-target="#add_leave"><i class="fa-sharp fa-solid fa-person-walking-arrow-right"></i>Apply Leave</a>--%>
                                            <a href="ManagerApplyLeave.aspx" class="btn add-btn1"><i class="fa-sharp fa-solid fa-person-walking-arrow-right"></i>Apply Leave</a>
                                        </div>
                                    </div>
                                </div>
                            </section>
                            <section>
                                <h5 class="dash-title">Your time off allowance</h5>
                                <div class="card">
                                    <div class="card-body">
                                        <div class="time-list">
                                            <div class="dash-stats-list">
                                                <h4>
                                                    <asp:Label ID="lblslused" runat="server" ForeColor="red"></asp:Label>
                                                    Hrs</h4>
                                                <p>Using</p>
                                            </div>
                                            <div class="dash-stats-list">
                                                <h4>
                                                    <asp:Label ID="lblslremaining" runat="server" ForeColor="Green"></asp:Label>
                                                    Hrs</h4>
                                                <p>Remaining</p>
                                            </div>
                                        </div>
                                        <div class="request-btn">
                                            <a class="btn add-btn1" href="ManagerTimeOffs.aspx"><i class="fa-solid fa-pencil"></i>TimeOff</a>
                                        </div>
                                    </div>
                                </div>
                            </section>
                            <section>
                                <h5 class="dash-title">Upcoming Holiday</h5>
                                <div class="card">
                                    <div class="card-body text-center">
                                        <h4 class="holiday-title mb-0">
                                            <asp:Label ID="lblNextHoliday" runat="server" ForeColor="green"></asp:Label>
                                        </h4>
                                    </div>
                                </div>
                            </section>
                        </div>
                    </div>
                </div>

                <!-- Add TimeOff Modal -->
                <div id="add_timeoff" class="modal custom-modal fade" role="dialog">
                    <div class="modal-dialog modal-dialog-centered" role="document">
                        <div class="modal-content" id="popupleave1">
                            <div class="modal-header">
                                <h5 class="modal-title">Apply TimeOff</h5>
                                <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                            </div>
                            <div class="modal-body">
                                <div class="input-block mb-3">
                                    <label class="col-form-label">Total TimeOff (Hours)<span class="text-danger">*</span></label>
                                    <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                    <asp:Label ID="Label7" runat="server" ForeColor="Red"></asp:Label>
                                </div>
                                <div class="input-block mb-3">
                                    <label class="col-form-label">Take TimeOff (Hours)<span class="text-danger">*</span></label>
                                    <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control" onkeypress="return isNumberKey(event);"></asp:TextBox>
                                </div>

                                <div class="input-block mb-3">
                                    <label class="col-form-label">Reason <span class="text-danger">*</span></label>
                                    <asp:TextBox ID="TextBox4" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                </div>
                                <div class="submit-section">
                                    <asp:Button ID="btntimeoff" runat="server" Text="Apply TimeOff" CssClass="btn btn-primary submit-btn" OnClick="btntimeoff_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- /Add TimeOff Modal -->
            </div>
        </div>
    </div>

   

</asp:Content>

