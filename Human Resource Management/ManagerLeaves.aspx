<%@ Page Title="" Language="C#" MasterPageFile="~/Manager.Master" AutoEventWireup="true" CodeBehind="ManagerLeaves.aspx.cs" Inherits="Human_Resource_Management.ManagerLeaves" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .no-records-message {
            display: flex;
            align-items: center;
            justify-content: center;
            color: red;
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
                            <h3 class="page-title">Leaves <span>
                                <asp:Label ID="Label1" runat="server" ForeColor="Red"></asp:Label></span></h3>
                            <ul class="breadcrumb">
                                <li class="breadcrumb-item"><a href="EmployeeDashboard.aspx">Dashboard</a></li>
                                <li class="breadcrumb-item active">Leaves</li>
                            </ul>
                        </div>
                        <div class="col-auto float-end ms-auto">

                            <a href="ManagerApplyLeave.aspx" class="btn add-btn"><i class="fa-solid fa-plus"></i>Add Leave</a>
                        </div>
                    </div>
                </div>
                <!-- /Page Header -->


                <!-- Leave Statistics -->
                <div class="row">
                    <div class="col-md-3">
                        <div class="stats-info">
                            <h6>Total Leaves</h6>
                            <h4>
                                <asp:Label ID="lbltotalleaves" runat="server" Font-Size="Larger" ForeColor="deeppink"></asp:Label></h4>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="stats-info">
                            <h6>Casual Leaves</h6>
                            <h4>
                                <asp:Label ID="lblcasualleavescount" runat="server" Font-Size="Larger" ForeColor="deeppink"></asp:Label></h4>
                        </div>
                    </div>

                    <div class="col-md-3">
                        <div class="stats-info">
                            <h6>Sick Leaves </h6>
                            <h4>
                                <asp:Label ID="lblsickleavescount" runat="server" Font-Size="Larger" ForeColor="deeppink"></asp:Label></h4>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="stats-info">
                            <h6>Comp-Off Leaves</h6>
                            <h4>
                                <asp:Label ID="lblcompoff" runat="server" Font-Size="Larger" ForeColor="deeppink"></asp:Label></h4>
                        </div>
                    </div>
                </div>
                <!-- /Leave Statistics -->




                <!-- Page Tab -->
                <div class="page-menu">
                    <div class="row">
                        <div class="col-sm-12">
                            <ul class="nav nav-tabs nav-tabs-bottom">
                                <li class="nav-item">
                                    <a class="nav-link active" data-bs-toggle="tab" href="#tab_selfleaves"> Employee Leaves</a>
                                </li>
                                <li class="nav-item">
                                    <a class="nav-link" data-bs-toggle="tab" href="#tab_employeeleaves">Self Leaves</a>
                                </li>
                            </ul>
                        </div>
                    </div>
                </div>
                <!-- /Page Tab -->

                <!-- Tab Content -->
                <div class="tab-content">
                    <!-- Pending Tab -->
                    <div class="tab-pane show active" id="tab_selfleaves">
                        <div class="payroll-table card">
                            <div class="table-responsive">
                                <table class="table table-hover table-radius">
                                    <thead>
                                        <tr>
                                            <th>Name</th>
                                            <th>Leave Type</th>
                                            <th>From</th>
                                            <th>To</th>
                                            <th>No of Days</th>
                                            <th>Reason</th>
                                            <th>Status</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:PlaceHolder ID="EditEmployeeLeaves" runat="server"></asp:PlaceHolder>

                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                    <!-- Pending Tab End-->

                    <!-- Approved Tab -->
                    <div class="tab-pane" id="tab_employeeleaves">

                        <div class="payroll-table card">
                            <div class="table-responsive">
                                <table class="table table-hover table-radius">
                                    <thead>
                                        <tr>
                                            <th>ApplyDate</th>
                                            <th>Reason</th>
                                            <th>FromDate</th>
                                            <th>ToDate</th>
                                            <th>No of Days</th>
                                            <th>LeaveType</th>
                                            <th>Status</th>
                                            <th>Approved by</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:PlaceHolder ID="SelfLeavesContainer" runat="server"></asp:PlaceHolder>

                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                    <!-- Approved Tab End-->
                </div>
                <!-- Tab Content End-->

            </div>
            <asp:Literal ID="Literal1" runat="server"></asp:Literal>
            <!-- /Page Content -->



            <!-- Edit self Leave Modal -->
            <div id="edit_leave" class="modal custom-modal fade" role="dialog">
                <div class="modal-dialog modal-dialog-centered" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title">Edit Leave </h5>
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="modal-body">
                            <div class="input-block mb-3">
                                <asp:HiddenField ID="HiddenField2" runat="server" />
                                <asp:HiddenField ID="HiddenField4" runat="server" />
                                <asp:Label ID="Label3" runat="server" Text="" ForeColor="Red"></asp:Label>
                            </div>
                            <div class="input-block mb-3">
                                <label class="col-form-label">Leave Type <span class="text-danger">*</span></label>
                                <asp:DropDownList ID="ddlleavesstatus" runat="server" CssClass="ddlstatusleave form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlleavesstatus_SelectedIndexChanged" onfocus="disableddlLeaveType()">
                                    <asp:ListItem>-- Select Leave Type --</asp:ListItem>
                                    <asp:ListItem>Casual Leave</asp:ListItem>
                                    <asp:ListItem>Sick Leave</asp:ListItem>
                                    <asp:ListItem>Comp-Off Leave</asp:ListItem>
                                    <asp:ListItem>LOP Leave</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="input-block mb-3">
                                <label class="col-form-label">From <span class="text-danger">*</span></label>
                                <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                            </div>
                            <div class="input-block mb-3">
                                <label class="col-form-label">To <span class="text-danger">*</span></label>
                                <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control" TextMode="Date" AutoPostBack="true" OnTextChanged="TextBox2_TextChanged"></asp:TextBox>
                            </div>
                            <div class="input-block mb-3">
                                <label class="col-form-label">Number of days <span class="text-danger">*</span></label>
                                <asp:TextBox ID="TextBox3" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="input-block mb-3">
                                <label class="col-form-label">Leave Reason <span class="text-danger">*</span></label>
                                <asp:TextBox ID="TextBox5" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                            </div>
                            <div class="submit-section">
                                <asp:Button ID="btnleaveedit" runat="server" Text="Update.." CssClass="btn btn-primary submit-btn" OnClick="btnleaveedit_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- /Edit self Leave Modal -->




            <!-- Edit Leave Modal -->
            <div id="edit_empleave" class="modal custom-modal fade" role="dialog">
                <div class="modal-dialog modal-dialog-centered" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title">Edit Leave</h5>
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="modal-body">
                            <asp:HiddenField ID="hdnLeaveId" runat="server" />
                            <asp:HiddenField ID="HiddenField3" runat="server" />
                            <div class="input-block mb-3">
                                <label class="col-form-label">Name <span class="text-danger">*</span></label>
                                <asp:TextBox ID="txtnameedit" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="input-block mb-3">
                                <label class="col-form-label">Leave Type <span class="text-danger">*</span></label>
                                <asp:TextBox ID="txtedleavtype" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="input-block mb-3">
                                <label class="col-form-label">From <span class="text-danger">*</span></label>
                                <asp:TextBox ID="txtedfromdt" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                            </div>
                            <div class="input-block mb-3">
                                <label class="col-form-label">To <span class="text-danger">*</span></label>
                                <asp:TextBox ID="txtedtodt" runat="server" CssClass="form-control" TextMode="Date" onchange="calculateDuration()"></asp:TextBox>
                            </div>
                            <div class="input-block mb-3">
                                <label class="col-form-label">Number of days <span class="text-danger">*</span></label>
                                <asp:TextBox ID="txteddur" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>

                            <div class="input-block mb-3">
                                <label class="col-form-label">Leave Reason <span class="text-danger">*</span></label>
                                <asp:TextBox ID="txtedreason" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                            </div>
                            <div class="submit-section">
                                <asp:Button ID="btnacceptleave" runat="server" Text="Approve" CssClass="btn btn-primary submit-btn" OnClick="btnacceptleave_Click" />
                                <asp:Button ID="btnreject" runat="server" Text="Reject" CssClass="btn btn-danger submit-btn" OnClick="btnreject_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- /Edit Leave Modal -->



            <!-- Delete Leave Modal -->
            <div class="modal custom-modal fade" id="delete_approve" role="dialog">
                <div class="modal-dialog modal-dialog-centered">
                    <div class="modal-content">
                        <div class="modal-body">
                            <div class="form-header">
                                <h3>Delete Leave</h3>
                                <p>
                                    Are you sure want to Cancel this leave
                                    <asp:Label ID="Label2" runat="server" ForeColor="#cc0000"></asp:Label>
                                    ?
                                </p>
                                <asp:HiddenField ID="HiddenField1" runat="server" />
                            </div>
                            <div class="modal-btn delete-action">
                                <div class="row">
                                    <div class="col-6">
                                        <asp:Button ID="btnleavedelete" runat="server" Text="Delete" CssClass="btn btn-primary continue-btn" OnClick="btnleavedelete_Click" />
                                    </div>
                                    <div class="col-6">
                                        <a href="javascript:void(0);" data-bs-dismiss="modal" class="btn btn-primary cancel-btn">Cancel</a>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- /Delete Leave Modal -->

        </div>
        <!-- /Page Wrapper -->
    </div>

    <script type="text/javascript">


        function edit_selfleave(leaveID, leaveType, fromDate1, toDate1, numberOfDays, rs) {
            $('#<%= ddlleavesstatus.ClientID %> option').each(function () {
                if ($(this).text().trim() === leaveType.trim()) {
                    $(this).prop('selected', true);
                    console.log("drop value is " + leaveType)
                }
            });
            $('#<%= HiddenField2.ClientID %>').val(leaveID).prop('readonly', true);
            $('#<%= TextBox1.ClientID %>').val(fromDate1);
            $('#<%= TextBox2.ClientID %>').val(toDate1);
            $('#<%= TextBox3.ClientID %>').val(numberOfDays).prop('readonly', true);
            $('#<%= TextBox5.ClientID %>').val(rs);
            $('#edit_leave').modal('show');
        }


        function delete_leave(leaveID) {
            document.getElementById('<%= Label2.ClientID %>').textContent = leaveID;
            $('#<%= HiddenField1.ClientID %>').val(leaveID);
            console.log("delete leave is : " + leaveID);
        }

        function editEmpLeave(Name, LeaveId, leavetype, FromDate, ToDate, Duration, Reason, EmpId) {
            $('#<%= txtnameedit.ClientID %>').val(Name);
            $('#<%= hdnLeaveId.ClientID %>').val(LeaveId);
            $('#<%= txtedleavtype.ClientID %>').val(leavetype);
            $('#<%= txtedfromdt.ClientID %>').val(FromDate);
            $('#<%= txtedtodt.ClientID %>').val(ToDate);
            $('#<%= txteddur.ClientID %>').val(Duration).prop('readonly', true);
            $('#<%= txtedreason.ClientID %>').val(Reason);

            $('#<%= HiddenField1.ClientID %>').val(EmpId);
            console.log("EmpId is : " + EmpId);
        }
        function disableddlLeaveType() {
            var ddl = document.getElementById('<%= ddlleavesstatus.ClientID %>');
            ddl.options[0].disabled = true;
        }

    </script>



    <script type="text/javascript">
        // console.log("Start date is : " );
        $(document).ready(function () {
            $('#<%= TextBox2.ClientID %>').change(function () {
                var startDateValue = $('#<%= TextBox1.ClientID %>').val();
                var endDateValue = $(this).val();
                console.log("Start date is : " + startDateValue);
                if (!startDateValue || !endDateValue) {
                    alert('Please enter both start date and end date.');
                    return; // Exit the function
                }

                var startDate = new Date(startDateValue);
                var endDate = new Date(endDateValue);

                // Check if the end date is before the start date
                if (endDate < startDate) {
                    alert('End date cannot be before start date.');
                    $(this).val(startDateValue); // Reset the end date to the start date
                    return; // Exit the function
                }

                // Check if start date is after end date
                if (startDate > endDate) {
                    alert('Please select a valid end date.');
                    return; // Exit the function
                }

                // Calculate the number of days between the start date and end date (inclusive)
                var numberOfDays = Math.ceil((endDate - startDate) / (1000 * 60 * 60 * 24)) + 1;
                console.log("numberOfDays : " + numberOfDays);

                // Update the value of txtnoofdays TextBox
                $('#<%= TextBox3.ClientID %>').val(numberOfDays);
            });
        });

    </script>







    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap/5.1.3/js/bootstrap.bundle.min.js"></script>
    <script type="text/javascript">
        function showModal() {
            var myModal = new bootstrap.Modal(document.getElementById('edit_leave'),
                {
                    backdrop: 'static',
                    keyboard: false
                });
            myModal.show();
        }

        // Set hidden field value before postback
        function setModalState() {
            document.getElementById('<%= HiddenField4.ClientID %>').value = "1";
        }

        document.addEventListener('DOMContentLoaded', function () {
            var ddlLeavesStatus = document.getElementById('<%= ddlleavesstatus.ClientID %>');
            ddlLeavesStatus.addEventListener('change', setModalState);
        });
    </script>


    <script type="text/javascript">
        document.addEventListener('DOMContentLoaded', function () {
            var textBox = document.getElementById('<%= TextBox1.ClientID %>');

            textBox.addEventListener('change', function () {
                var selectedDate = new Date(this.value);
                if (selectedDate.getDay() === 0) { // 0 is Sunday
                    alert('Sundays are not allowed.');
                    this.value = ''; // Clear the value
                }
            });
        });
    </script>
    <script type="text/javascript">
        document.addEventListener('DOMContentLoaded', function () {
            var textBox = document.getElementById('<%= TextBox2.ClientID %>');

            textBox.addEventListener('change', function () {
                var selectedDate = new Date(this.value);
                if (selectedDate.getDay() === 0) { // 0 is Sunday
                    alert('Sundays are not allowed.');
                    this.value = ''; // Clear the value
                }
            });
        });
    </script>

</asp:Content>
