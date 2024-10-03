<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="AdminLeaveType.aspx.cs" Inherits="Human_Resource_Management.AdminLeaveType" %>

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
                        <h3 class="page-title">Leave Type</h3>
                        <ul class="breadcrumb">
                            <li class="breadcrumb-item"><a href="Admindashboard">Dashboard</a></li>
                            <li class="breadcrumb-item active">Leave Type</li>
                        </ul>
                    </div>
                    <div class="col-auto float-end ms-auto">
                        <a href="#" class="btn add-btn" data-bs-toggle="modal" data-bs-target="#add_leavetype"><i class="fa-solid fa-plus"></i>Add Leave Type</a>
                    </div>
                </div>
            </div>
            <!-- /Page Header -->

            <div class="row">
                <div class="col-md-12">
                    <div class="table-responsive">
                        <table class="table table-striped custom-table datatable mb-0">
                            <thead>
                                <tr>
                                    <th>S.NO</th>
                                    <th>Leave Type</th>
                                    <th>Leave Days</th>
                                    <th>CreatedDate</th>
                                    <th>Edit</th>
                                    <th>Delete</th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:PlaceHolder ID="LeavesData" runat="server"></asp:PlaceHolder>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>
        <!-- /Page Content -->

        <!-- Add Leavetype Modal -->
        <div id="add_leavetype" class="modal custom-modal fade" role="dialog">
            <div class="modal-dialog modal-dialog-centered" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title">Add Leave Type</h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <div class="input-block mb-3">
                            <label class="col-form-label">Leave Type <span class="text-danger">*</span></label>
                            <asp:TextBox ID="txtaddleavetype" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="input-block mb-3">
                            <label class="col-form-label">Number of days <span class="text-danger">*</span></label>
                            <asp:TextBox ID="txtaddleavedays" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                        </div>
                        <div class="input-block mb-3">
                            <label class="col-form-label">Description <span class="text-danger">*</span></label>
                            <asp:TextBox ID="txtadddescription" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="submit-section">
                            <asp:Button ID="btnaddleavetype" runat="server" Text="Add Leave" CssClass="btn btn-primary submit-btn" OnClick="btnaddleavetype_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!-- /Add Leavetype Modal -->

        <!-- Edit Leavetype Modal -->
        <div id="edit_leavetype" class="modal custom-modal fade" role="dialog">
            <div class="modal-dialog modal-dialog-centered" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title">Edit Leave Type</h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <div class="input-block mb-3">
                            <label class="col-form-label">Leave ID <span class="text-danger">*</span></label>
                            <asp:TextBox ID="txtedid" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="input-block mb-3">
                            <label class="col-form-label">Leave Type <span class="text-danger">*</span></label>
                            <asp:TextBox ID="txtedtype" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>

                        <div class="input-block mb-3">
                            <label class="col-form-label">Number of days <span class="text-danger">*</span></label>
                            <asp:TextBox ID="txteddays" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="input-block mb-3">
                            <label class="col-form-label">Description <span class="text-danger">*</span></label>
                            <asp:TextBox ID="txteddesc" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="input-block mb-3">
                            <label class="col-form-label">Created Date <span class="text-danger">*</span></label>
                            <asp:TextBox ID="txteddate" runat="server" CssClass="form-control" ReadOnly="true" TextMode="Date"></asp:TextBox>
                        </div>
                        <div class="submit-section">
                            <asp:Button ID="btnleaveupdate" runat="server" Text="Update" OnClick="btnleaveupdate_Click" CssClass="btn btn-primary submit-btn" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!-- /Edit Leavetype Modal -->

        <!-- Delete Leavetype Modal -->
        <div class="modal custom-modal fade" id="delete_leavetype" role="dialog">
            <div class="modal-dialog modal-dialog-centered">
                <div class="modal-content">
                    <div class="modal-body">
                        <div class="form-header">
                            <h3>Delete Leave Type</h3>
                            <p>Are you sure want to delete
                                <asp:Label ID="Label1" runat="server" ForeColor="#ff0000"></asp:Label></p>
                            <asp:HiddenField ID="HiddenField1" runat="server" />
                        </div>
                        <div class="modal-btn delete-action">
                            <div class="row">
                                <div class="col-6">
                                    <asp:Button ID="btndeleteleave" runat="server" Text="Delete" CssClass="btn btn-primary continue-btn" OnClick="btndeleteleave_Click" />
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
        <!-- /Delete Leavetype Modal -->

      <%--  <asp:FileUpload ID="FileUpload1" runat="server" />
        <asp:Button ID="btnupload" runat="server" Text="Submit" OnClick="btnupload_Click" />

        <br />--%>

       
    </div>
    <!-- /Page Wrapper -->


    <script type="text/javascript">
        function editleavetype(LeaveId, LeaveName, Days, Reason,CreatedDate) {
            $('#<%= txtedid.ClientID %>').val(LeaveId).prop('readonly', true);
            $('#<%= txtedtype.ClientID %>').val(LeaveName);
            console.log("LeaveName is  : " + LeaveName);
            $('#<%= txteddays.ClientID %>').val(Days);
            $('#<%= txteddesc.ClientID %>').val(Reason);
            $('#<%= txteddate.ClientID %>').val(CreatedDate);

        }

        function deleteleave(LeaveId, LeaveName) {
            document.getElementById('<%= Label1.ClientID %>').textContent = LeaveName;
            console.log("leave id is  : " + LeaveId);
            $('#<%= HiddenField1.ClientID %>').val(LeaveId);
        }
    </script>
</asp:Content>
