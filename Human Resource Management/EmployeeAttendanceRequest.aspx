<%@ Page Title="" Language="C#" MasterPageFile="~/Employee.Master" AutoEventWireup="true" CodeBehind="EmployeeAttendanceRequest.aspx.cs" Inherits="Human_Resource_Management.EmployeeAttendanceRequest" %>

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
                            <h3 class="page-title">Attendance Request</h3>
                            <ul class="breadcrumb">
                                <li class="breadcrumb-item"><a href="admin-dashboard.html">Dashboard</a></li>
                                <li class="breadcrumb-item active">Attendance Request</li>
                            </ul>
                        </div>
                        <div class="col-auto float-end ms-auto">
                            <a href="#" class="btn add-btn" data-bs-toggle="modal" data-bs-target="#add_promotion"><i class="fa-solid fa-plus"></i>Add Request</a>
                        </div>
                    </div>
                </div>
                <!-- /Page Header -->

                <div class="row">
                    <div class="col-md-12">
                        <div class="table-responsive">

                            <!-- Request Table -->
                            <table class="table table-striped custom-table mb-0 datatable">
                                <thead>
                                    <tr>
                                        <th>Id</th>
                                        <th>Name</th>
                                        <th>RequestType</th>
                                        <th>OT/PunchTime</th>
                                        <th>Request Date</th>
                                        <th>Manager Approve</th>
                                        <th>Hr Approve</th>
                                        <!--<th>Status</th>-->
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:PlaceHolder ID="attendancrrequestContainer" runat="server"></asp:PlaceHolder>
                                    <%--<tr>
										<td>1</td>
										<td>
											<h2 class="table-avatar blue-link">
												<a href="profile.html" class="avatar"><img src="assets/img/profiles/avatar-02.jpg" alt="User Image"></a>
												<a href="profile.html">John Doe</a>
											</h2>
										</td>
										<td>Web Development</td>
										<td>Web Developer</td>
										<td>Sr Web Developer</td>
										<td>28 Feb 2019</td>
										<td class="text-end">
											<div class="dropdown dropdown-action">
												<a href="#" class="action-icon dropdown-toggle" data-bs-toggle="dropdown" aria-expanded="false"><i class="material-icons">more_vert</i></a>
												<div class="dropdown-menu dropdown-menu-right">
													<a class="dropdown-item" href="#" data-bs-toggle="modal" data-bs-target="#edit_promotion"><i class="fa-solid fa-pencil m-r-5"></i> Edit</a>
													<a class="dropdown-item" href="#" data-bs-toggle="modal" data-bs-target="#delete_promotion"><i class="fa-regular fa-trash-can m-r-5"></i> Delete</a>
												</div>
											</div>
										</td>
									</tr>--%>
                                </tbody>
                            </table>
                            <!-- /Promotion Table -->

                        </div>

                    </div>
                </div>
            </div>
            <!-- /Page Content -->

            <!-- Add Request Modal -->
            <div id="add_promotion" class="modal custom-modal fade" role="dialog">
                <div class="modal-dialog modal-dialog-centered" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title">Add Request</h5>
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="modal-body">
                            <asp:HiddenField ID="HiddenField1" runat="server" />
                            <asp:Label ID="Label1" runat="server" Text="" ForeColor="Red"></asp:Label>
                            <div class="input-block mb-3">
                                <label class="col-form-label">RequestType<span class="text-danger">*</span></label>
                                <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control" onmousedown="hideFirstItemddlrequset()" AutoPostBack="true" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                                    <asp:ListItem> -- Select request Type -- </asp:ListItem>
                                    <asp:ListItem>InTime Punch</asp:ListItem>
                                    <asp:ListItem>OutTime Punch</asp:ListItem>
                                    <asp:ListItem>OverTime</asp:ListItem>
                                </asp:DropDownList>                               
                            </div>
                            <div class="input-block mb-3">
                                <label class="col-form-label">Punch Date <span class="text-danger">*</span></label>
                                <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control" TextMode="Date" AutoPostBack="true" OnTextChanged="TextBox1_TextChanged"></asp:TextBox>
                               
                            </div>

                            <div class="input-block mb-3">
                                <label class="col-form-label">Min Time <span class="text-danger">*</span></label>
                                <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                            </div>

                            <div class="input-block mb-3">
                                <label class="col-form-label">Max Time <span class="text-danger">*</span></label>
                                <asp:TextBox ID="TextBox3" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                            </div>
                            <div class="input-block mb-3">
                                <label class="col-form-label">Total Time <span class="text-danger">*</span></label>
                                <asp:TextBox ID="TextBox4" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                            </div>


                            <div class="input-block mb-3">
                                <label class="col-form-label">Reason <span class="text-danger">*</span></label>
                                <asp:TextBox ID="txtreqreason" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                            </div>
                            <div class="submit-section">
                                <asp:Button ID="btnrequestsend" runat="server" Text="Send Request" CssClass="btn btn-primary submit-btn" OnClick="btnrequestsend_Click" ValidationGroup="vgForm" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- /Add Promotion Modal -->

            <!-- Edit Promotion Modal -->
            <div id="edit_promotion" class="modal custom-modal fade" role="dialog">
                <div class="modal-dialog modal-dialog-centered" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title">Edit Promotion</h5>
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="modal-body">

                            <div class="input-block mb-3">
                                <label class="col-form-label">Promotion For <span class="text-danger">*</span></label>
                                <input class="form-control" type="text" value="John Doe">
                            </div>
                            <div class="input-block mb-3">
                                <label class="col-form-label">Promotion From <span class="text-danger">*</span></label>
                                <input class="form-control" type="text" value="Web Developer" readonly>
                            </div>
                            <div class="input-block mb-3">
                                <label class="col-form-label">Promotion To <span class="text-danger">*</span></label>
                                <select class="select">
                                    <option>Web Developer</option>
                                    <option>Web Designer</option>
                                    <option>SEO Analyst</option>
                                </select>
                            </div>
                            <div class="input-block mb-3">
                                <label class="col-form-label">Promotion Date <span class="text-danger">*</span></label>
                                <div class="cal-icon">
                                    <input type="text" class="form-control datetimepicker">
                                </div>
                            </div>
                            <div class="submit-section">
                                <button class="btn btn-primary submit-btn">Save</button>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
            <!-- /Edit Promotion Modal -->

            <!-- Delete Promotion Modal -->
            <div class="modal custom-modal fade" id="delete_promotion" role="dialog">
                <div class="modal-dialog modal-dialog-centered">
                    <div class="modal-content">
                        <div class="modal-body">
                            <div class="form-header">
                                <h3>Delete Promotion</h3>
                                <p>Are you sure want to delete?</p>
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
            <!-- /Delete Promotion Modal -->

        </div>
        <!-- /Page Wrapper -->
    </div>
    <!-- /Main Wrapper -->
    <script type="text/javascript">
        function hideFirstItemddlrequset() {
            var select = document.getElementById('<%= DropDownList1.ClientID %>');
            if (select && select.options.length > 0) {
                select.options[0].style.display = 'none';
            }
        }
    </script>
    <script type="text/javascript">
        function updateTextBoxType() {
            var ddl = document.getElementById('<%= DropDownList1.ClientID %>');
            var textBox = document.getElementById('<%= TextBox1.ClientID %>');
            var selectedValue = ddl.options[ddl.selectedIndex].text;

            if (selectedValue === 'OverTime') {
                textBox.type = 'date';
            } else {
                textBox.type = 'datetime-local';
            }
        }
    </script>

    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap/5.1.3/js/bootstrap.bundle.min.js"></script>

    <script type="text/javascript">
        // add employee company 
        function showModal() {
            var myModal = new bootstrap.Modal(document.getElementById('add_promotion'),
                {
                    backdrop: 'static',
                    keyboard: false
                });
            myModal.show();
        }

        function setModalState() {
            document.getElementById('<%= HiddenField1.ClientID %>').value = "1";
        }

        document.addEventListener('DOMContentLoaded', function () {
            var ddlLeavesStatus = document.getElementById('<%= DropDownList1.ClientID %>');
            ddlLeavesStatus.addEventListener('change', setModalState);
        });
    </script>


    <script type="text/javascript">
        window.onload = function () {
            var dropdown = document.getElementById('<%= DropDownList1.ClientID %>');
        var textbox = document.getElementById('<%= TextBox1.ClientID %>');
        var button = document.getElementById('<%= btnrequestsend.ClientID %>');

            // Initially hide the button
            button.style.display = 'none';

            function checkVisibility() {
                if (dropdown.value !== "" && textbox.value !== "") {
                    button.style.display = 'block';  // Show the button
                } else {
                    button.style.display = 'none';  // Hide the button
                }
            }
            dropdown.addEventListener('change', checkVisibility);
            textbox.addEventListener('input', checkVisibility);  // 'input' works for textboxes
            checkVisibility();
        }
    </script>

</asp:Content>
