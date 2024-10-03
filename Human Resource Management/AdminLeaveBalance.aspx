<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="AdminLeaveBalance.aspx.cs" Inherits="Human_Resource_Management.AdminLeaveBalance" %>

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
                            <h3 class="page-title">Balance Leaves</h3>
                            <ul class="breadcrumb">
                                <li class="breadcrumb-item"><a href="Admindashboard.aspx">Dashboard</a></li>
                                <li class="breadcrumb-item active">Balance Leaves</li>
                            </ul>
                        </div>
                        <div class="col-sm-6 col-md-4">
                            <div class="input-block mb-3 form-focus select-focus">
                                <a href="#" class="btn add-btn m-r-5" data-bs-toggle="modal" data-bs-target="#adding_Leaves">Add Leaves</a>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- /Page Header -->

                <div class="row filter-row">
                    <div class="col-sm-6 col-md-3">
                        <div class="input-block mb-3 form-focus focused">
                            <asp:TextBox ID="txtempnamesearch" runat="server" CssClass="form-control floating" placeholder="Search By Name" AutoCompleteType="Disabled" onkeypress="return isCharacterKey(event)" OnTextChanged="txtempnamesearch_TextChanged"></asp:TextBox>
                        </div>
                    </div>
                </div>

                <!-- /Leave Statistics -->

                <div class="row">
                    <div class="col-md-12">
                        <div class="table-responsive">
                            <table class="table table-striped custom-table leave-employee-table mb-0 datatable">
                                <thead>
                                    <tr>
                                        <th>Name</th>
                                        <th>Id</th>
                                        <%-- <th>CL's</th>--%>
                                        <th>Used CL's</th>
                                        <th>Bls CL's</th>
                                        <th>SL's</th>
                                        <th>Used SL's</th>
                                        <th>Bls SL's</th>
                                        <th>Comp.off</th>
                                        <th>Used Comp.off</th>
                                        <th>Bls Comp.off</th>
                                        <th>Year</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:PlaceHolder ID="balenceLeaves" runat="server"></asp:PlaceHolder>
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
                                <select class="select">
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
            <div id="edit_leaveList" class="modal custom-modal fade" role="dialog">
                <div class="modal-dialog modal-dialog-centered" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title">Edit LeaveList</h5>
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="modal-body">
                            <asp:HiddenField ID="HiddenField1" runat="server" />
                            <asp:HiddenField ID="HiddenField2" runat="server" />
                            <%-- <div class="input-block mb-3">
                                <label class="col-form-label">Employee Id<span class="text-danger">*</span></label>
                                <asp:TextBox ID="txtempid" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>--%>
                            <div class="input-block mb-3">
                                <label class="col-form-label">Name<span class="text-danger">*</span></label>
                                <asp:TextBox ID="txtname" runat="server" CssClass="form-control" AutoCompleteType="Disabled" onkeypress="return isCharacterKey(event)"></asp:TextBox>
                            </div>
                            <div class="input-block mb-3">
                                <label class="col-form-label">Used Casual Leaves <span class="text-danger">*</span></label>
                                <asp:TextBox ID="txtucls" runat="server" CssClass="form-control" onkeypress="return isNumberKey(event)"></asp:TextBox>
                            </div>
                            <div class="input-block mb-3">
                                <label class="col-form-label">Balence Casual Leaves <span class="text-danger">*</span></label>
                                <asp:TextBox ID="txtblscasualleaves" runat="server" CssClass="form-control" onkeypress="return isNumberKey(event)"></asp:TextBox>
                            </div>
                            <div class="input-block mb-3">
                                <label class="col-form-label">Used SL <span class="text-danger">*</span></label>
                                <asp:TextBox ID="txtusl" runat="server" CssClass="form-control" onkeypress="return isNumberKey(event)"></asp:TextBox>
                            </div>
                            <div class="input-block mb-3">
                                <label class="col-form-label">Balence SL <span class="text-danger">*</span></label>
                                <asp:TextBox ID="txtbsl" runat="server" CssClass="form-control" onkeypress="return isNumberKey(event)"></asp:TextBox>
                            </div>
                            <div class="input-block mb-3">
                                <label class="col-form-label">Used Comp-Offs<span class="text-danger">*</span></label>
                                <asp:TextBox ID="txtucompoffs" runat="server" CssClass="form-control" onkeypress="return isNumberKey(event)"></asp:TextBox>
                            </div>
                            <div class="input-block mb-3">
                                <label class="col-form-label">Balence Comp-Offs<span class="text-danger">*</span></label>
                                <asp:TextBox ID="txtblscompoffs" runat="server" CssClass="form-control" onkeypress="return isNumberKey(event)"></asp:TextBox>
                            </div>
                            <div class="submit-section">
                                <asp:Button ID="btnLeaveUpdate" runat="server" Text="Update" CssClass="btn btn-primary submit-btn" OnClick="btnLeaveUpdate_Click" />
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
                            <asp:HiddenField ID="HiddenField3" runat="server" />
                            <div class="form-header">
                                <h3>Delete Leave</h3>
                                <p>Are you sure want to Cancel this leave?</p>
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
             <!-- add  Leaves at all  Modal -->
            <div id="adding_Leaves" class="modal custom-modal fade" role="dialog">
                <div class="modal-dialog modal-dialog-centered" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title">Adding Leaves</h5>
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="modal-body">
                            <div class="input-block mb-3">
                                <label class="col-form-label">Leave Type<span class="text-danger">*</span></label>
                                <asp:DropDownList ID="ddlleavetype" runat="server"  CssClass="form-control">
                                    <asp:ListItem>-- Select Leave Type --</asp:ListItem>
                                    <asp:ListItem>Casual Leave</asp:ListItem>                                 
                                     <asp:ListItem>Comp-Off Leave</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="input-block mb-3">
                                <label class="col-form-label">No of Leaves<span class="text-danger">*</span></label>
                                <asp:TextBox ID="txtleavestotal" runat="server" CssClass="form-control" onkeypress="return isNumberKey(event)" AutoCompleteType="Disabled"></asp:TextBox>
                            </div>
                            <div class="submit-section">
                                <asp:Button ID="btnaddallempleaves" runat="server" Text="Submit" CssClass="btn btn-danger submit-btn" OnClick="btnaddallempleaves_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
              <!-- /add  Leaves at all  Modal -->
        </div>
        <!-- /Page Wrapper -->

    </div>
    <!-- /Main Wrapper -->

    <script type="text/javascript">
        function editLeavesList(EmpId, Name, UsedCasualLeaves, BalenceCasualLeaves, UsedSickLeaves, BalenceSickLaves, UsedCampOffLeaves, BalenceCampOffLeaves, Year) {
          <%--  $('#<%= txtempid.ClientID %>').val(EmpId).prop('readonly', true);--%>
            $('#<%= HiddenField1.ClientID %>').val(EmpId);
            $('#<%= txtname.ClientID %>').val(Name);
            console.log("Id is : " + EmpId);
            $('#<%= txtucls.ClientID %>').val(UsedCasualLeaves);
            $('#<%= txtblscasualleaves.ClientID %>').val(BalenceCasualLeaves);
            $('#<%= txtusl.ClientID %>').val(UsedSickLeaves);
            $('#<%= txtbsl.ClientID %>').val(BalenceSickLaves);
            $('#<%= txtucompoffs.ClientID %>').val(UsedCampOffLeaves);
            $('#<%= txtblscompoffs.ClientID %>').val(BalenceCampOffLeaves);

            $('#<%= HiddenField2.ClientID %>').val(Year);

        }


        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
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
</asp:Content>
