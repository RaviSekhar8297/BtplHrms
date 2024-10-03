<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="AdminLeaves.aspx.cs" Inherits="Human_Resource_Management.AdminLeaves" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Page Wrapper -->
    <div class="page-wrapper">

        <!-- Page Content -->
        <div class="content container-fluid">

            <!-- Page Header -->
            <div class="page-header">
                <div class="row align-items-center">
                    <div class="col">
                        <h3 class="page-title">Leaves</h3>
                        <ul class="breadcrumb">
                            <li class="breadcrumb-item"><a href="Admindashboard">Dashboard</a></li>
                            <li class="breadcrumb-item active">Leaves</li>
                        </ul>
                    </div>
                    <div class="col-auto float-end ms-auto">
                        <%-- <a href="#" class="btn add-btn" data-bs-toggle="modal" data-bs-target="#add_leave"><i class="fa-solid fa-plus"></i>Add Leave</a>--%>
                    </div>
                </div>
            </div>
            <!-- /Page Header -->

            <!-- Leave Statistics -->
            <div class="row">
                <div class="col-md-3 d-flex">
                    <div class="stats-info w-100">
                        <h6>Today Leaves</h6>
                        <h2>
                            <asp:Label ID="Label1" runat="server" ForeColor="#0000ff"></asp:Label></h2>
                    </div>
                </div>
                <div class="col-md-3 d-flex">
                    <div class="stats-info w-100">
                        <h6>Approved Leaves</h6>
                        <h2>
                            <asp:Label ID="Label2" runat="server" ForeColor="#009900"></asp:Label></h2>
                    </div>
                </div>
                <div class="col-md-3 d-flex">
                    <div class="stats-info w-100">
                        <h6>Pending Leaves</h6>
                        <h2>
                            <asp:Label ID="Label3" runat="server" ForeColor="#cc00cc"></asp:Label></h2>
                    </div>
                </div>
                <div class="col-md-3 d-flex">
                    <div class="stats-info w-100">
                        <h6>Rejected Leaves</h6>
                        <h2>
                            <asp:Label ID="Label4" runat="server" ForeColor="#ff0000"></asp:Label></h2>
                    </div>
                </div>
            </div>
            <!-- /Leave Statistics -->

            <!-- Search Filter -->
            <div class="row filter-row">
                <div class="col-sm-6 col-md-3 col-lg-3 col-xl-2 col-12">
                    <div class="input-block mb-3 form-focus select-focus">
                        <asp:TextBox ID="txtname" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtname_TextChanged" onkeypress="return isCharacterKey(event)" AutoCompleteType="Disabled"></asp:TextBox>
                        <label class="focus-label">Employee Name</label>
                    </div>
                </div>
                <div class="col-sm-6 col-md-3 col-lg-3 col-xl-2 col-12">
                    <div class="input-block mb-3 form-focus select-focus">
                        <asp:DropDownList ID="ddlLeaveType" runat="server" CssClass="form-control" onmousedown="hideFirstItem2()" AutoPostBack="true" OnSelectedIndexChanged="ddlLeaveType_SelectedIndexChanged">
                            <asp:ListItem>-- Select --</asp:ListItem>
                            <asp:ListItem style="color: green;">Casual Leave</asp:ListItem>
                            <asp:ListItem style="color: red;">Sick Leave</asp:ListItem>
                            <asp:ListItem style="color: red;">LOP Leave</asp:ListItem>
                        </asp:DropDownList>
                        <label class="focus-label">Leave Type</label>
                    </div>
                </div>
                <div class="col-sm-6 col-md-3 col-lg-3 col-xl-2 col-12">
                    <div class="input-block mb-3 form-focus select-focus">
                        <asp:DropDownList ID="ddlLeaveStatus" runat="server" CssClass="form-control" onmousedown="hideFirstItem1()" AutoPostBack="true" OnSelectedIndexChanged="ddlLeaveStatus_SelectedIndexChanged">
                            <asp:ListItem Text="-- Select --"></asp:ListItem>
                            <asp:ListItem Text="Approved" Style="color: forestgreen;"></asp:ListItem>
                            <asp:ListItem Text="Pending" Style="color: deeppink;"></asp:ListItem>
                            <asp:ListItem Text="Rejected" Style="color: red;"></asp:ListItem>
                        </asp:DropDownList>
                        <label class="focus-label">Leave Status</label>
                    </div>
                </div>
                <div class="col-sm-6 col-md-3 col-lg-3 col-xl-2 col-12">
                    <div class="input-block mb-3 form-focus select-focus">
                        <asp:TextBox ID="TextBox1" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox>
                        <label class="focus-label">From</label>
                    </div>
                </div>
                <div class="col-sm-6 col-md-3 col-lg-3 col-xl-2 col-12">
                    <div class="input-block mb-3 form-focus select-focus">
                        <asp:TextBox ID="TextBox2" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox>
                        <label class="focus-label">To</label>
                    </div>
                </div>
                <div class="col-sm-6 col-md-3 col-lg-3 col-xl-2 col-12">
                    <asp:Button ID="btnsearch" runat="server" Text="Search" CssClass="btn btn-success w-100" OnClick="btnsearch_Click" />
                </div>
            </div>
            <!-- /Search Filter -->

            <div class="row">
                <div class="col-md-12">
                    <div class="table-responsive">
                        <table class="table table-striped custom-table mb-0 datatable">
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
                                <asp:PlaceHolder ID="EditLeave" runat="server"></asp:PlaceHolder>                              
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>
        <!-- /Page Content -->

        <!-- Add Leave Modal -->
        <div id="add_leave" class="modal custom-modal fade" role="dialog">
            <div class="modal-dialog modal-dialog-centered" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title">Add Leave</h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <div class="input-block mb-3">
                            <label class="col-form-label">Leave Type <span class="text-danger">*</span></label>
                            <select class="select form-control ">
                                <option>Select Leave Type</option>
                                <option>Casual Leave 12 Days</option>
                                <option>Medical Leave</option>
                                <option>Loss of Pay</option>
                            </select>
                        </div>
                        <div class="input-block mb-3">
                            <label class="col-form-label">From <span class="text-danger">*</span></label>
                            <div class="cal-icon">
                                <input class="form-control datetimepicker" type="text">
                            </div>
                        </div>
                        <div class="input-block mb-3">
                            <label class="col-form-label">To <span class="text-danger">*</span></label>
                            <div class="cal-icon">
                                <input class="form-control datetimepicker" type="text">
                            </div>
                        </div>
                        <div class="input-block mb-3">
                            <label class="col-form-label">Number of days <span class="text-danger">*</span></label>
                            <input class="form-control" readonly type="text">
                        </div>
                        <div class="input-block mb-3">
                            <label class="col-form-label">Remaining Leaves <span class="text-danger">*</span></label>
                            <input class="form-control" readonly value="12" type="text">
                        </div>
                        <div class="input-block mb-3">
                            <label class="col-form-label">Leave Reason <span class="text-danger">*</span></label>
                            <textarea rows="4" class="form-control"></textarea>
                        </div>
                        <div class="submit-section">
                            <button class="btn btn-primary submit-btn">Submit</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!-- /Add Leave Modal -->

        <!-- Edit Leave Modal -->
        <div id="edit_leave" class="modal custom-modal fade" role="dialog">
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
                        <asp:HiddenField ID="HiddenField1" runat="server" />
                        <div class="input-block mb-3">
                            <label class="col-form-label">Name <span class="text-danger">*</span></label>
                            <asp:TextBox ID="txtnameedit" runat="server" CssClass="form-control" onkeypress="return isCharacterKey(event)" AutoCompleteType="Disabled"></asp:TextBox>
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
                            <asp:TextBox ID="txtedreason" runat="server" CssClass="form-control" TextMode="MultiLine" onkeypress="return isCharacterKey(event)" AutoCompleteType="Disabled"></asp:TextBox>
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

        <!-- Approve Leave Modal -->
        <div class="modal custom-modal fade" id="approve_leave" role="dialog">
            <div class="modal-dialog modal-dialog-centered">
                <div class="modal-content">
                    <div class="modal-body">
                        <div class="form-header">
                            <h3>Leave Approve</h3>
                            <p>Are you sure want to approve for this leave?</p>
                        </div>
                        <div class="modal-btn delete-action">
                            <div class="row">
                                <div class="col-6">
                                    <a href="javascript:void(0);" class="btn btn-primary continue-btn">Approve</a>
                                </div>
                                <div class="col-6">
                                    <a href="javascript:void(0);" data-bs-dismiss="modal" class="btn btn-primary cancel-btn">Decline</a>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!-- /Approve Leave Modal -->

        <!-- Delete Leave Modal -->
        <div class="modal custom-modal fade" id="delete_approve" role="dialog">
            <div class="modal-dialog modal-dialog-centered">
                <div class="modal-content">
                    <div class="modal-body">
                        <div class="form-header">
                            <h3>Delete Leave</h3>
                            <p>Are you sure want to delete this leave?</p>
                        </div>
                        <div class="modal-btn delete-action">
                            <div class="row">
                                <div class="col-6">
                                    <a href="javascript:void(0);" class="btn btn-primary continue-btn">Delete</a>
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
    <script type="text/javascript">
        function editLeave(Name, LeaveId, leavetype, FromDate, ToDate, Duration, Reason, EmpId) {
            $('#<%= txtnameedit.ClientID %>').val(Name);
            $('#<%= hdnLeaveId.ClientID %>').val(LeaveId);
            $('#<%= txtedleavtype.ClientID %>').val(leavetype);
            $('#<%= txtedfromdt.ClientID %>').val(FromDate);
            $('#<%= txtedtodt.ClientID %>').val(ToDate);
            $('#<%= txteddur.ClientID %>').val(Duration).prop('readonly', true);
            $('#<%= txtedreason.ClientID %>').val(Reason);
           
            $('#<%= HiddenField1.ClientID %>').val(EmpId);
            console.log("EmpId is : " + EmpId);
           <%-- $('#<%= ddledstatus.ClientID %> option').each(function () {
                if ($(this).text().trim() === Status.trim()) {
                    $(this).prop('selected', true);
                    console.log("drop value is " + Status)
                }
            });--%>

            $('#edit_leave').modal('show');
        }
        function hideFirstItem1() {
            var select = document.getElementById('<%= ddlLeaveStatus.ClientID %>');
            if (select && select.options.length > 0) {
                select.options[0].style.display = 'none';
            }
        }
        function hideFirstItem2() {
            var select = document.getElementById('<%= ddlLeaveType.ClientID %>');
            if (select && select.options.length > 0) {
                select.options[0].style.display = 'none';
            }
        }


        function calculateDuration() {
            var fromDate = new Date(document.getElementById('<%=txtedfromdt.ClientID%>').value);
            var toDate = new Date(document.getElementById('<%=txtedtodt.ClientID%>').value);
            if (toDate < fromDate) {
                alert("To date must be greater than from date.");
                document.getElementById('<%=txtedtodt.ClientID%>').value = '';
                return;
                console.log("to date is : " + toDate);
            }
            var difference = toDate - fromDate;
            var sundayCount = 0;

            for (var date = new Date(fromDate); date <= toDate; date.setDate(date.getDate() + 1)) {
                if (date.getDay() === 0) {
                    sundayCount++;
                }
            }
            var daysDifference = (difference / (1000 * 60 * 60 * 24)) - sundayCount;
            daysDifference += 1;
            document.getElementById('<%=txteddur.ClientID%>').value = daysDifference.toFixed(0);
        }
    </script>
</asp:Content>
