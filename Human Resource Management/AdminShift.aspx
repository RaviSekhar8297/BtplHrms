<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="AdminShift.aspx.cs" Inherits="Human_Resource_Management.AdminShift" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        img {
            max-width: 40px !important;
            height: 40px !important;
            border-radius: 50% !important;
        }
        /* CSS for shift time link */
        .shift-time {
            text-decoration: none;
            color: inherit;
            border: none;
            padding: 2px; /* Adjust as necessary */
            transition: border 0.2s ease-in-out; /* Smooth transition */
        }

        /* CSS to add border on hover  */
        .shift-timee:hover {
            border: 2px dashed green;
        }
          .toast-custom {
            top: 160px !important; /* Start from 200 pixels from the top */
            right: 20px !important; /* Adjust right margin as needed */
            position: fixed; /* Fixes the position relative to the viewport */
            z-index: 9999; /* Ensure it appears above other elements */
        }

        .toast {
            font-size: 16px;
            padding: 15px;
            border-radius: 8px;
        }

        .toast-success {
            background-color: #28a745;
            color: white;
        }

        .toast-error {
            background-color: #dc3545;
            color: white;
        }
    </style>

    <script type="text/javascript">
        function showToastMessage(type, message) {
            toastr.options = {
                "closeButton": true,
                "progressBar": true,
                "positionClass": "toast-custom", // Use the custom class for positioning
                "timeOut": "5000", // Toast duration in milliseconds
            };

            if (type === 'success') {
                toastr.success(message);
            } else if (type === 'error') {
                toastr.error(message);
            }
        }
    </script>

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
                    <div class="row">
                        <div class="col">
                            <h3 class="page-title">Daily Scheduling</h3>
                            <ul class="breadcrumb">
                                <li class="breadcrumb-item"><a href="Admindashboard">Dashboard</a></li>
                                <li class="breadcrumb-item"><a href="AdminAllEmployees">Employees</a></li>
                                <li class="breadcrumb-item active">Shift Scheduling</li>
                            </ul>
                        </div>
                        <div class="col-auto float-end ms-auto">
                            <%--<a href="shift-list.html" class="btn add-btn m-r-5">Shifts</a>
                            <a href="#" class="btn add-btn m-r-5" data-bs-toggle="modal" data-bs-target="#add_Shift">Shifts</a>--%>
                            <a href="#" class="btn add-btn m-r-5" data-bs-toggle="modal" data-bs-target="#add_schedule">Assign Shifts</a>
                        </div>
                    </div>
                </div>
                <!-- /Page Header -->

                <!-- Page Tab -->
                <div class="page-menu">
                    <div class="row">
                        <div class="col-sm-12">
                            <ul class="nav nav-tabs nav-tabs-bottom">
                                <li class="nav-item">
                                    <a class="nav-link active" data-bs-toggle="tab" href="#tab_allshifts">All Shifts</a>
                                </li>
                                <li class="nav-item">
                                    <a class="nav-link" data-bs-toggle="tab" href="#tab_assignshifts">Assign Shifts</a>
                                </li>

                            </ul>
                        </div>
                    </div>
                </div>
                <!-- /Page Tab -->
                <!-- Content Starts -->
                <div class="tab-content">
                    <!-- List Tab -->
                    <div class="tab-pane show active" id="tab_allshifts">
                        <!-- Search Filter -->
                        <div class="row filter-row">
                            <div class="col-sm-6 col-md-3">
                                <div class="input-block mb-3 form-focus focused">
                                    <asp:TextBox ID="txtsearchName" runat="server" CssClass="form-control floating" AutoPostBack="true" AutoCompleteType="Disabled" OnTextChanged="txtsearchName_TextChanged" onkeypress="return isCharacterKey(event)"></asp:TextBox>
                                    <label class="focus-label">Employee Name</label>
                                </div>
                            </div>

                            <div class="col-sm-6 col-md-3">
                                <div class="input-block mb-3 form-focus focused">
                                    <asp:DropDownList ID="DropDownList4" runat="server" CssClass="form-control" onfocus="disableFirstItem1()" AutoPostBack="true" OnSelectedIndexChanged="DropDownList4_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <label class="focus-label">Department</label>
                                </div>
                            </div>
                        </div>
                        <!-- Search Filter -->

                        <div class="row">
                            <div class="col-md-12">
                                <div class="table-responsive">
                                    <table class="table table-striped custom-table datatable leave-employee-table">
                                        <asp:PlaceHolder ID="AttendanceData" runat="server"></asp:PlaceHolder>
                                    </table>
                                </div>
                            </div>
                        </div>
                        <!-- /Content End -->
                    </div>
                    <div class="tab-pane show active" id="tab_assignshifts">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="table-responsive">
                                    <table class="table table-striped custom-table datatable leave-employee-table">
                                        <thead>
                                            <tr>
                                                <th>Image</th>
                                                <th>EmpId</th>
                                                <th>Name</th>
                                                <th>Date</th>
                                                <th>Time</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:PlaceHolder ID="AssignData" runat="server"></asp:PlaceHolder>
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

        <!-- Add Schedule Modal -->
        <div id="add_schedule" class="modal custom-modal fade" role="dialog">
            <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title">Add Schedule</h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-sm-6">
                                <div class="input-block mb-3">
                                    <label class="col-form-label">Employee Name <span class="text-danger">*</span></label>
                                    <asp:DropDownList ID="ddlempname" runat="server" CssClass="form-control" onfocus="hidefirst()"></asp:DropDownList>
                                    <asp:RequiredFieldValidator
                                        ID="rfvEmpName"
                                        runat="server"
                                        ControlToValidate="ddlempname"
                                        InitialValue="0"
                                        ErrorMessage="Please select an employee."
                                        CssClass="text-danger"
                                        ValidationGroup="vgForm" />
                                </div>
                            </div>
                            <div class="col-sm-6">
                                <div class="input-block mb-3">
                                    <label class="col-form-label">Date</label>
                                    <asp:TextBox ID="txtdate" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                    <asp:RequiredFieldValidator
                                        ID="rfvDate"
                                        runat="server"
                                        ControlToValidate="txtdate"
                                        ErrorMessage="Date is required."
                                        CssClass="text-danger"
                                        ValidationGroup="vgForm" />
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-6">
                                <div class="input-block mb-3">
                                    <label class="col-form-label">Time (Ex : 09:30 AM - 06:30 PM)</label>
                                    <asp:TextBox ID="txttime" runat="server" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator
                                        ID="rfvTime"
                                        runat="server"
                                        ControlToValidate="txttime"
                                        ErrorMessage="Time is required."
                                        CssClass="text-danger"
                                        ValidationGroup="vgForm" />

                                    <asp:RegularExpressionValidator
                                        ID="revTime"
                                        runat="server"
                                        ControlToValidate="txttime"
                                        ValidationExpression="^(0[1-9]|1[0-2]):([0-5][0-9])\s(AM|PM)\s-\s(0[1-9]|1[0-2]):([0-5][0-9])\s(AM|PM)$"
                                        ErrorMessage="Enter a valid time range (e.g., 09:30 AM - 06:30 PM)."
                                        CssClass="text-danger"
                                        ValidationGroup="vgForm" />
                                </div>
                            </div>
                            <div class="col-sm-6">
                                <div class="input-block mb-3">
                                    <label class="col-form-label">Reason</label>
                                    <asp:TextBox ID="txtreason" runat="server" CssClass="form-control" TextMode="MultiLine" onkeyup="updateCharCount()"></asp:TextBox>
                                    <asp:RequiredFieldValidator
                                        ID="rfvReason"
                                        runat="server"
                                        ControlToValidate="txtreason"
                                        ErrorMessage="Reason is required."
                                        CssClass="text-danger"
                                        ValidationGroup="vgForm" />

                                    <asp:RegularExpressionValidator
                                        ID="revReasonLength"
                                        runat="server"
                                        ControlToValidate="txtreason"
                                        ValidationExpression="^.{20,}$"
                                        ErrorMessage="Reason must be at least 20 characters long."
                                        CssClass="text-danger"
                                        ValidationGroup="vgForm" />
                                    <span id="count"></span>
                                </div>
                            </div>
                        </div>
                        <div class="submit-section">
                            <asp:Button ID="btnsubmit" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="btnsubmit_Click" ValidationGroup="vgForm" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!-- /Add Schedule Modal -->



        <!-- Edit Assign Shift Modal -->
        <div id="edit_assignshift" class="modal custom-modal fade" role="dialog">
            <div class="modal-dialog modal-dialog-centered" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title">Edit Assign Shift</h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <asp:HiddenField ID="HiddenField4" runat="server" />
                        <asp:HiddenField ID="HiddenField5" runat="server" />
                        <div class="input-block mb-3">
                            <label class="col-form-label">Name <span class="text-danger">*</span></label>
                            <asp:TextBox ID="txtassname" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="input-block mb-3">
                            <label class="col-form-label">Date<span class="text-danger">*</span></label>
                            <asp:TextBox ID="txtassdate" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                        </div>
                        <div class="input-block mb-3">
                            <label class="col-form-label">Time<span class="text-danger">*</span></label>
                            <asp:TextBox ID="txtasstime" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator
                                ID="RequiredFieldValidator3"
                                runat="server"
                                ControlToValidate="txtasstime"
                                ErrorMessage="Time is required."
                                CssClass="text-danger"
                                ValidationGroup="editShift" />

                            <asp:RegularExpressionValidator
                                ID="RegularExpressionValidator2"
                                runat="server"
                                ControlToValidate="txtasstime"
                                ValidationExpression="^(0[1-9]|1[0-2]):([0-5][0-9])\s(AM|PM)\s-\s(0[1-9]|1[0-2]):([0-5][0-9])\s(AM|PM)$"
                                ErrorMessage="Enter a valid time range (e.g., 09:30 AM - 06:30 PM)."
                                CssClass="text-danger"
                                ValidationGroup="editShift" />
                        </div>
                        <div class="submit-section">
                            <asp:Button ID="btnupdateasshift" runat="server" Text="Update" CssClass="btn btn-primary submit-btn" ValidationGroup="editShift" OnClick="btnupdateasshift_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!-- /Edit Assign Shift Modal -->
        <!-- Delete Department Modal -->
        <div class="modal custom-modal fade" id="delete_assignshift" role="dialog">
            <div class="modal-dialog modal-dialog-centered">
                <div class="modal-content">
                    <div class="modal-body">
                        <div class="form-header">
                            <h3>Delete Assign Shift</h3>
                            <p>
                                Conirm to Delete
                         <asp:Label ID="Label1" runat="server" ForeColor="#cc0000"></asp:Label> of  <asp:Label ID="Label2" runat="server" ForeColor="#cc0000"></asp:Label>
                                ?
                            </p>
                            <asp:HiddenField ID="HiddenField3" runat="server" />
                        </div>
                        <div class="modal-btn delete-action">
                            <div class="row">
                                <div class="col-6">
                                    <asp:Button ID="btndeleteasshift" runat="server" Text="Delete" CssClass="btn btn-primary continue-btn" OnClick="btndeleteasshift_Click" />
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
        <!-- /Delete Department Modal -->



        <!--  ravi Add new Shift Panel start -->
        <!-- Add Shift Modal -->
        <div id="add_Shift" class="modal custom-modal fade" role="dialog">
            <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title">Add Shift</h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-sm-6">
                                <div class="input-block mb-3">
                                    <label class="col-form-label">Sift Type</label>
                                    <asp:DropDownList ID="DropDownList5" runat="server" CssClass="form-control" onfocus="disableFirstItem5()" AutoPostBack="true" OnSelectedIndexChanged="DropDownList5_SelectedIndexChanged">
                                        <asp:ListItem>-- Select Type --</asp:ListItem>
                                        <asp:ListItem>DayShift</asp:ListItem>
                                        <asp:ListItem>NightShift</asp:ListItem>
                                        <asp:ListItem style="color: blue;" Value="O">Rotational Shift</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-sm-6">
                                <div class="input-block mb-3">
                                    <label class="col-form-label">Shifts Time<span class="text-danger">*</span></label>
                                    <asp:TextBox ID="TextBox21" runat="server" CssClass="form-control timepicker" AutoCompleteType="Disabled"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-sm-4">
                                <div class="input-block mb-3">
                                    <label class="col-form-label">Shft Start Time  <span class="text-danger">*</span></label>
                                    <div class="input-group time">
                                        <asp:TextBox ID="TextBox19" runat="server" CssClass="form-control timepicker" AutoCompleteType="Disabled"></asp:TextBox><span class="input-group-text"><i class="fa-regular fa-clock"></i></span>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-4">
                                <div class="input-block mb-3">
                                    <label class="col-form-label">Shft End Time  <span class="text-danger">*</span></label>
                                    <div class="input-group time">
                                        <asp:TextBox ID="TextBox20" runat="server" CssClass="form-control timepicker" AutoCompleteType="Disabled"></asp:TextBox><span class="input-group-text"><i class="fa-regular fa-clock"></i></span>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-4">
                                <div class="input-block mb-3">
                                    <label class="col-form-label">Shift Duration<span class="text-danger">*</span></label>
                                    <div class="input-group time">
                                        <asp:TextBox ID="TextBox16" runat="server" CssClass="form-control timepicker" AutoCompleteType="Disabled"></asp:TextBox><span class="input-group-text"><i class="fa-regular fa-clock"></i></span>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="submit-section">
                            <%--<button class="btn btn-primary submit-btn"></button>--%>
                            <asp:Button ID="Button1" runat="server" Text="Submit" CssClass="btn btn-primary submit-btn" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!-- /Add Shift Modal -->
        <!-- Ravi add new shift end end-->

        <!-- Edit Schedule Modal -->
        <div id="edit_schedule" class="modal custom-modal fade" role="dialog">
            <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title">Edit Schedule</h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-sm-6">
                                <div class="input-block mb-3">
                                    <asp:HiddenField ID="HiddenField1" runat="server" />
                                    <asp:HiddenField ID="HiddenField2" runat="server" />
                                    <label class="col-form-label">Id <span class="text-danger">*</span></label>
                                    <asp:TextBox ID="TextBox10" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-sm-6">
                                <div class="input-block mb-3">
                                    <label class="col-form-label">Employee Name <span class="text-danger">*</span></label>
                                    <asp:TextBox ID="TextBox11" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-sm-6">
                                <div class="input-block mb-3">
                                    <label class="col-form-label">Date <span class="text-danger">*</span></label>
                                    <asp:TextBox ID="TextBox12" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                    <asp:RequiredFieldValidator
                                        ID="RequiredFieldValidator2"
                                        runat="server"
                                        ControlToValidate="TextBox12"
                                        ErrorMessage="Date is required."
                                        CssClass="text-danger"
                                        ValidationGroup="vgFormEdit" />
                                </div>
                            </div>
                            <div class="col-sm-6">
                                <div class="input-block mb-3">
                                    <label class="col-form-label">Shift Timmings <span class="text-danger">*</span></label>
                                    <asp:TextBox ID="TextBox13" runat="server" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator
                                        ID="RequiredFieldValidator1"
                                        runat="server"
                                        ControlToValidate="TextBox13"
                                        ErrorMessage="Time is required."
                                        CssClass="text-danger"
                                        ValidationGroup="vgFormEdit" />

                                    <asp:RegularExpressionValidator
                                        ID="RegularExpressionValidator1"
                                        runat="server"
                                        ControlToValidate="TextBox13"
                                        ValidationExpression="^(0[1-9]|1[0-2]):([0-5][0-9])\s(AM|PM)\s-\s(0[1-9]|1[0-2]):([0-5][0-9])\s(AM|PM)$"
                                        ErrorMessage="(e.g., 09:30 AM - 06:30 PM)."
                                        CssClass="text-danger"
                                        ValidationGroup="vgFormEdit" />
                                </div>
                            </div>
                            <div class="col-sm-4">
                                <div class="input-block mb-3">
                                    <label class="col-form-label">Start Time  <span class="text-danger">*</span></label>
                                    <asp:TextBox ID="TextBox14" runat="server" CssClass="form-control timepicker"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-sm-4">
                                <div class="input-block mb-3">
                                    <label class="col-form-label">Grace Time  <span class="text-danger">*</span></label>
                                    <asp:TextBox ID="TextBox15" runat="server" CssClass="form-control timepicker"></asp:TextBox>
                                </div>
                            </div>
                            <%--<div class="col-sm-4">
									<div class="input-block mb-3">
										<label class="col-form-label">Max Start Time  <span class="text-danger">*</span></label>
										<asp:TextBox ID="TextBox16" runat="server" CssClass="form-control timepicker"></asp:TextBox>
									</div>
								</div>--%>
                            <div class="col-sm-4">
                                <div class="input-block mb-3">
                                    <label class="col-form-label">Min End Time  <span class="text-danger">*</span></label>
                                    <asp:TextBox ID="TextBox17" runat="server" CssClass="form-control timepicker"></asp:TextBox>
                                </div>
                            </div>


                            <%-- <div class="col-sm-12">
                                <div class="input-block mb-3">
                                    <label class="col-form-label">Repeat Every</label>
                                    <asp:DropDownList ID="ddlDaysOfWeek" runat="server" onchange="updateCheckboxes()" CssClass="form-control" onmousedown="hideFirstItem()">
                                        <asp:ListItem Text="-- Select --" Value="-1"></asp:ListItem>
                                        <asp:ListItem Text="Sunday" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="Monday" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Tuesday" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="Wednesday" Value="3"></asp:ListItem>
                                        <asp:ListItem Text="Thursday" Value="4"></asp:ListItem>
                                        <asp:ListItem Text="Friday" Value="5"></asp:ListItem>
                                        <asp:ListItem Text="Saturday" Value="6"></asp:ListItem>
                                    </asp:DropDownList>
                                    <label class="col-form-label">Week(s)</label>
                                </div>
                            </div>
                            <div class="col-sm-12">
                                <div class="input-block mb-3 wday-box">
                                    <asp:CheckBox ID="SundayCheckbox" runat="server" Text="Sunday" />
                                    <asp:CheckBox ID="MondayCheckbox" runat="server" Text="Monday" />
                                    <asp:CheckBox ID="TuesdayCheckbox" runat="server" Text="Tuesday" />
                                    <asp:CheckBox ID="WednesdayCheckbox" runat="server" Text="Wednesday" />
                                    <asp:CheckBox ID="ThursdayCheckbox" runat="server" Text="Thursday" />
                                    <asp:CheckBox ID="FridayCheckbox" runat="server" Text="Friday" />
                                    <asp:CheckBox ID="SaturdayCheckbox" runat="server" Text="Saturday" />
                                </div>
                            </div>--%>
                        </div>

                        <div class="submit-section">
                            <asp:Button ID="btneditassignshift" runat="server" Text="Update" CssClass="btn btn-primary submit-btn" OnClick="btneditassignshift_Click" ValidationGroup="vgFormEdit" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!-- /Edit Schedule Modal -->
    </div>
    <!-- /Main Wrapper -->

    <script type="text/javascript">

        function disableFirstItem1() {
            var ddl = document.getElementById('<%= DropDownList4.ClientID %>');
            ddl.options[0].disabled = true;
        }

        function disableFirstItem5() {
            var ddl = document.getElementById('<%= DropDownList5.ClientID %>');
            ddl.options[0].disabled = true;
        }
    </script>

    <script type="text/javascript">

        function showShiftDetails(userId, userName, shift, startTime, updatedShiftTime, endTime, dateOfShift) {
            $('#<%= TextBox10.ClientID %>').val(userId);
            $('#<%= TextBox11.ClientID %>').val(userName);
            $('#<%= TextBox13.ClientID %>').val(shift);
            $('#<%= TextBox14.ClientID %>').val(startTime);
            $('#<%= TextBox15.ClientID %>').val(updatedShiftTime);
            $('#<%= TextBox17.ClientID %>').val(endTime);

            $('#<%= TextBox12.ClientID %>').val(dateOfShift);
            $('#<%= HiddenField1.ClientID %>').val(dateOfShift);
            $('#<%= HiddenField2.ClientID %>').val(userId);


            $('#edit_schedule').modal('show');
        }
        function hidefirst() {
            var ddl = document.getElementById('<%= ddlempname.ClientID %>');
            ddl.options[0].disabled = true;
        }
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



    <%-- <script type="text/javascript">
        function updateCheckboxes() {
            var ddlDaysOfWeek = document.getElementById('<%= ddlDaysOfWeek.ClientID %>');
            var selectedDay = parseInt(ddlDaysOfWeek.options[ddlDaysOfWeek.selectedIndex].value);

            // Reset all checkboxes
            var checkboxes = document.querySelectorAll('input[type="checkbox"]');
            for (var i = 0; i < checkboxes.length; i++) {
                checkboxes[i].checked = false;
            }

            for (var i = 0; i <= selectedDay; i++) {
                var checkboxId = '';
                switch (i) {
                    case 0:
                        checkboxId = '<%= SundayCheckbox.ClientID %>';
                        break;
                    case 1:
                        checkboxId = '<%= MondayCheckbox.ClientID %>';
                        break;
                    case 2:
                        checkboxId = '<%= TuesdayCheckbox.ClientID %>';
                        break;
                    case 3:
                        checkboxId = '<%= WednesdayCheckbox.ClientID %>';
                        break;
                    case 4:
                        checkboxId = '<%= ThursdayCheckbox.ClientID %>';
                        break;
                    case 5:
                        checkboxId = '<%= FridayCheckbox.ClientID %>';
                        break;
                    case 6:
                        checkboxId = '<%= SaturdayCheckbox.ClientID %>';
                        break;
                }
                document.getElementById(checkboxId).checked = true;
            }


            function hideFirstItem() {
                var select = document.getElementById('<%= ddlDaysOfWeek.ClientID %>');
                if (select && select.options.length > 0) {
                    select.options[0].style.display = 'none';
                }
            }


        }


    </script>--%>

    <%--<script type="text/javascript">
        $(document).ready(function () {
            // Validation function for TextBox36
            function validateShiftTime() {
                var shiftTime = $('#<%= txttime.ClientID %>').val().trim();
                var shiftTimePattern = /^([0-1]?[0-9]|2[0-3]):[0-5][0-9] [APap][Mm] - ([0-1]?[0-9]|2[0-3]):[0-5][0-9] ?[APap][Mm]?$/;
                return shiftTimePattern.test(shiftTime);
            }

            // Button click handler
            $('#<%= btnsubmit.ClientID %>').click(function () {
                // Validate TextBox36
                if (!validateShiftTime()) {
                    // If validation fails, show error and prevent submission
                    $('#<%= txttime.ClientID %>').addClass('input-error');
                    return false;
                }
                // Validation passed, remove error class if present
                $('#<%= txttime.ClientID %>').removeClass('input-error');
                return true; // Proceed with form submission
            });
        });
    </script>--%>
    <style type="text/css">
        .input-error {
            border-color: red;
        }
    </style>
    <script type="text/javascript">
        window.onload = function () {
            var today = new Date();
            // Add 1 day to the current date to disable today
            var tomorrow = new Date(today);
            tomorrow.setDate(today.getDate() + 1);

            // Format the date as yyyy-MM-dd
            var day = ("0" + tomorrow.getDate()).slice(-2);
            var month = ("0" + (tomorrow.getMonth() + 1)).slice(-2);
            var year = tomorrow.getFullYear();
            var minDate = year + "-" + month + "-" + day;

            // Set the min attribute on both txtdate and TextBox12
            document.getElementById('<%= txtdate.ClientID %>').setAttribute("min", minDate);
            document.getElementById('<%= TextBox12.ClientID %>').setAttribute("min", minDate);
        };

        function updateCharCount() {
            var textbox = document.getElementById('<%= txtreason.ClientID %>');
            var countSpan = document.getElementById('count');
            var count = textbox.value.length;

            countSpan.innerText = count + ' ';
        }
    </script>
    <script type="text/javascript">
        function editAssignShift(Id, EmpId, Name, date, ShiftTime) {
            $('#<%= HiddenField4.ClientID %>').val(Id);
            $('#<%= HiddenField5.ClientID %>').val(EmpId);
            $('#<%= txtassname.ClientID %>').val(Name);
            $('#<%= txtassdate.ClientID %>').val(date);
            $('#<%= txtasstime.ClientID %>').val(ShiftTime);
            console.log("Id is : " + Id);
        }
        function deleteAssignShift(Id, EmpId, Name, date) {
            document.getElementById('<%= Label2.ClientID %>').textContent = Name;
            document.getElementById('<%= Label1.ClientID %>').textContent = date;
            $('#<%= HiddenField4.ClientID %>').val(Id);
            $('#<%= HiddenField5.ClientID %>').val(EmpId);
        }
    </script>

</asp:Content>
