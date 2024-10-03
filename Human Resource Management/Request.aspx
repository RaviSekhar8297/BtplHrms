<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="Request.aspx.cs" Inherits="Human_Resource_Management.Request" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="main-wrapper">
        <!-- Page Wrapper -->
        <div class="page-wrapper">

            <!-- Page Content -->
            <div class="content container-fluid">

                <div class="row filter-row">
                    <div class="col-sm-6 col-md-3"></div>
                </div>
                <div class="row filter-row">
                    <div class="col-sm-6 col-md-4">
                        <div class="divpending-1">
                            <span style="font-size: 20px; color: #fff">Pending</span>
                            <span style="color: #fff">
                                <asp:Label ID="Label1" runat="server" Text="0"></asp:Label></span>
                        </div>
                        <%--<div class="firs-req">
                            <div class="first-side">
                                <img src="#" alt="image" />
                                <span>Ravi</span>
                                <span>1027</span>
                            </div>
                            <div class="second-side">
                                <span>Sick Leave</span>
                                <span>09/10 - 2</span>
                                <span>Suffering From Fever</span>
                                <span>
                                    <img src="#" alt="r" />
                                    <img src="#" alt="r" /></span>
                            </div>
                            <div class="third-edit">
                                <span>Ed</span>
                            </div>
                           
                        </div>    --%>
                        <asp:PlaceHolder ID="LeavePendingData" runat="server"></asp:PlaceHolder>
                    </div>

                    <div class="col-sm-6 col-md-4">
                        <div class="divapprove-1">
                            <span style="font-size: 20px; color: #fff">Approve
                            </span>
                            <span style="font-size: 20px; color: #fff"><asp:Label ID="Label2" runat="server" Text="0"></asp:Label>
                            </span>
                        </div>
                        <asp:PlaceHolder ID="LeaveApproveData" runat="server"></asp:PlaceHolder>
                    </div>
                    <div class="col-sm-6 col-md-4">
                        <div class="divreject-1">
                            <span style="font-size: 20px; color: #fff">Reject
                            </span>
                            <span style="font-size: 20px; color: #fff"><asp:Label ID="Label3" runat="server" Text="0"></asp:Label>
                            </span>
                        </div>
                        <asp:PlaceHolder ID="LeaveRejectedData" runat="server"></asp:PlaceHolder>
                    </div>
                </div>
                <!-- Edit Leave Modal -->
                <div id="edit_leave" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="editLeaveLabel" aria-hidden="true">
                    <div class="modal-dialog modal-dialog-centered" role="document">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h5 class="modal-title" id="editLeaveLabel">Edit Leave</h5>
                                <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                            </div>
                            <div class="modal-body">
                                <!-- Leave ID (Hidden Field) -->
                                <input type="hidden" id="leaveIdField" name="leaveIdField" />

                                <div class="mb-3">
                                    <label for="leaveTypeField" class="form-label">Name</label>
                                    <input type="text" class="form-control" id="nameField" name="leaveTypeField" required>
                                </div>
                                <!-- Leave Type -->
                                <div class="mb-3">
                                    <label for="leaveTypeField" class="form-label">Leave Type</label>
                                    <input type="text" class="form-control" id="leaveTypeField" name="leaveTypeField" required>
                                </div>

                                <!-- From Date -->
                                <div class="mb-3">
                                    <label for="fromDateField" class="form-label">From Date</label>
                                    <input type="date" class="form-control" id="fromDateField" name="fromDateField" required>
                                </div>

                                <!-- To Date -->
                                <div class="mb-3">
                                    <label for="toDateField" class="form-label">To Date</label>
                                    <input type="date" class="form-control" id="toDateField" name="toDateField" required>
                                </div>

                                <!-- Duration (Read-Only) -->
                                <div class="mb-3">
                                    <label for="durationField" class="form-label">Duration (Days)</label>
                                    <input type="number" class="form-control" id="durationField" name="durationField" readonly>
                                </div>

                                <!-- Reason -->
                                <div class="mb-3">
                                    <label for="reasonField" class="form-label">Reason</label>
                                    <textarea class="form-control" id="reasonField" name="reasonField" rows="3" required></textarea>
                                </div>

                                <div class="modal-footer">
                                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
                                    <button type="submit" class="btn btn-primary">Save Changes</button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>


            </div>
        </div>
    </div>
    <style type="text/css">
        .divpending-1 {
            height: 60px;
            /*border:1px solid red;*/
            box-shadow: rgba(100, 100, 111, 0.2) 0px 7px 29px 0px;
            background-image: linear-gradient(-20deg, #f794a4 0%, #fdd6bd 100%);
            display: flex;
            justify-content: space-around;
            align-items: center;
            margin-bottom: 30px;
        }

        .divapprove-1 {
            height: 60px;
            /*border:1px solid red;*/
            box-shadow: rgba(100, 100, 111, 0.2) 0px 7px 29px 0px;
            background-image: linear-gradient(to top, #0ba360 0%, #3cba92 100%);
            display: flex;
            justify-content: space-around;
            align-items: center;
             margin-bottom: 30px;
        }

        .divreject-1 {
            height: 60px;
            /*border:1px solid red;*/
            box-shadow: rgba(100, 100, 111, 0.2) 0px 7px 29px 0px;
            background-image: linear-gradient(to top, #f77062 0%, #fe5196 100%);
            display: flex;
            justify-content: space-around;
            align-items: center;
            margin-bottom: 30px;
        }

        .firs-req {
            height: 170px;
            box-shadow: rgba(60, 64, 67, 0.3) 0px 1px 2px 0px, rgba(60, 64, 67, 0.15) 0px 1px 3px 1px;
            display: flex;
            flex-direction: row;
            margin-bottom: 10px;
            border-top: 5px solid #8080ff;
        }

        .first-side {
            width: 30%;
            display: flex;
            flex-direction: column;
            justify-content: space-around;
            align-items: center;
        }

        .second-side {
            width: 65%;
            display: flex;
            justify-content: space-around;
            align-items: center;
            flex-direction: column;
        }

        .third-edit {
            width: 5%;
            display: flex;
            align-items: flex-start;
            justify-content: flex-start;
        }

        span {
            font-size: 16px;
        }
    </style>


    <script type="text/javascript">
        function editPendingLeave(FirstName,leaveId, leaveType, fromDate, toDate, duration, reason, status) {
            // Populate the modal fields with the leave data
            document.getElementById('nameField').value = FirstName;
            document.getElementById('leaveIdField').value = leaveId;
            document.getElementById('leaveTypeField').value = leaveType;

            // Set the dates properly formatted (YYYY-MM-DD)
            document.getElementById('fromDateField').value = fromDate;
            document.getElementById('toDateField').value = toDate;

            // Set duration and make it read-only
            document.getElementById('durationField').value = duration;

            // Populate reason
            document.getElementById('reasonField').value = reason;
            console.log("duration is : " + FirstName);
            // Open the modal (assuming you're using Bootstrap)
            $('#edit_leave').modal('show');
        }


    </script>
</asp:Content>
