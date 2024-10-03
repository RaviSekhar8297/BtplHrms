<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="AdminNotificationSettings.aspx.cs" Inherits="Human_Resource_Management.AdminNotificationSettings" %>

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
                            <h3 class="page-title">Notification Settings</h3>
                            <ul class="breadcrumb">
                                <li class="breadcrumb-item"><a href="admin-dashboard.html">Dashboard</a></li>
                                <li class="breadcrumb-item active">Notification</li>
                            </ul>
                        </div>
                        <div class="col-auto float-end ms-auto">
                            <a href="AdminAddNotification.aspx" class="btn add-btn"><i class="fa-solid fa-plus"></i>Add</a>
                        </div>
                    </div>
                </div>

                <!-- /Page Header -->
                <div class="row">
                  <%--  <div class="col-md-6 offset-md-3">
                        <ul class="list-group notification-list">
                            <li class="list-group-item">Employee
								<div class="status-toggle">
                                    <input type="checkbox" id="staff_module" class="check">
                                    <label for="staff_module" class="checktoggle">checkbox</label>
                                </div>
                            </li>
                            <li class="list-group-item">Holidays
								<div class="status-toggle">
                                    <input type="checkbox" id="holidays_module" class="check" checked>
                                    <label for="holidays_module" class="checktoggle">checkbox</label>
                                </div>
                            </li>
                            <li class="list-group-item">Leaves
								<div class="status-toggle">
                                    <input type="checkbox" id="leave_module" class="check" checked>
                                    <label for="leave_module" class="checktoggle">checkbox</label>
                                </div>
                            </li>
                            <li class="list-group-item">Events
								<div class="status-toggle">
                                    <input type="checkbox" id="events_module" class="check" checked>
                                    <label for="events_module" class="checktoggle">checkbox</label>
                                </div>
                            </li>
                            <li class="list-group-item">Chat
								<div class="status-toggle">
                                    <input type="checkbox" id="chat_module" class="check" checked>
                                    <label for="chat_module" class="checktoggle">checkbox</label>
                                </div>
                            </li>
                            <li class="list-group-item">Jobs
								<div class="status-toggle">
                                    <input type="checkbox" id="job_module" class="check">
                                    <label for="job_module" class="checktoggle">checkbox</label>
                                </div>
                            </li>
                        </ul>
                    </div>--%>

                    <div class="row" style="margin-top: 20px;">
                        <div class="col-md-12">
                            <div class="table-responsive">
                                <table class="table table-striped custom-table datatable">
                                    <thead>
                                        <tr>
                                            <th>EmpId</th>
                                            <th>Branch</th>
                                            <th>Subject</th>
                                            <th>Message</th>
                                            <th>SendBy</th>
                                            <th>SendTo</th>
                                            <th>Date</th>
                                            <th>
                                                <div class="status-container">
                                                      
                                                    <i id="icon" class="fa fa-filter" style="cursor: pointer;" title="Click to show status options"></i>
                                                    <asp:DropDownList ID="DropDownList1" runat="server" CssClass="status-dropdown" AutoPostBack="true" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                                                        <asp:ListItem>Seen</asp:ListItem>
                                                        <asp:ListItem>UnSeen</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:PlaceHolder ID="NotifyData" runat="server"></asp:PlaceHolder>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </div>
        <!-- /Page Content -->
    </div>
    <!-- /Page Wrapper -->
    <script type="text/javascript">
        $(document).ready(function () {
            // When the icon is clicked
            $("#icon").click(function () {
                // Toggle the visibility of the dropdown
                $("#<%= DropDownList1.ClientID %>").toggle();
            });
        });
    </script>


    <style type="text/css">
        #icon {
            margin-left: 10px;
            font-size: 16px;
            color: #333;
        }

        .status-container {
            position: relative;
            display: inline-block;
        }

        .status-dropdown {
            position: absolute;
            top: 100%; /* Position the dropdown right below the header */
            left: 0;
            display: none; /* Initially hidden */
            z-index: 10; /* Ensure it appears above other elements */
            background-color: white;
            border: 1px solid #ccc;
            padding: 5px;
            box-shadow: 0px 4px 8px rgba(0, 0, 0, 0.1);
        }
    </style>

</asp:Content>
