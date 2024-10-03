<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="AdminDepartments.aspx.cs" Inherits="Human_Resource_Management.AdminDepartments" %>

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
                            <h3 class="page-title">Department</h3>
                            <ul class="breadcrumb">
                                <li class="breadcrumb-item"><a href="Admindashboard">Dashboard</a></li>
                                <li class="breadcrumb-item active">Department</li>
                            </ul>
                        </div>
                        <div class="col-auto float-end ms-auto">

                            <%--<a href="#" class="btn add-btn" data-bs-toggle="modal" data-bs-target="#add_department"><i class="fa-solid fa-plus"></i> Add Department</a>--%>
                        </div>
                    </div>
                </div>
                <!-- /Page Header -->


                <!-- Search Filter -->
                <div class="row filter-row">
                    <div class="col-sm-6 col-md-3">
                        <div class="input-block mb-3 form-focus focused">
                            <asp:DropDownList ID="DropDownList4" runat="server" CssClass="form-control" onfocus="disableFirstItem1()" AutoPostBack="true" OnSelectedIndexChanged="DropDownList4_SelectedIndexChanged">
                            </asp:DropDownList>
                            <label class="focus-label">Branch</label>
                        </div>
                    </div>
                </div>
                <!-- Search Filter -->
                <div class="row">
                    <div class="col-md-12">
                        <div>
                            <table class="table table-striped custom-table mb-0 datatable">
                                <thead>
                                    <tr>
                                        <th>S.NO</th>
                                        <th>Department Id</th>
                                        <th>Department Name</th>
                                        <th>CompanyId</th>
                                        <th>Edit</th>
                                        <th>Delete</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:PlaceHolder ID="DepartmentData" runat="server"></asp:PlaceHolder>

                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
            <!-- /Page Content -->

            <!-- Add Department Modal -->
            <div id="add_department" class="modal custom-modal fade" role="dialog">
                <div class="modal-dialog modal-dialog-centered" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title">Add Department</h5>
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="modal-body">
                            <div class="input-block mb-3">
                                <label class="col-form-label">Company <span class="text-danger">*</span></label>
                                <input class="form-control" type="text">
                            </div>
                            <div class="input-block mb-3">
                                <label class="col-form-label">Branch <span class="text-danger">*</span></label>
                                <input class="form-control" type="text">
                            </div>
                            <div class="input-block mb-3">
                                <label class="col-form-label">Department <span class="text-danger">*</span></label>
                                <input class="form-control" type="text">
                            </div>
                            <div class="submit-section">
                                <button class="btn btn-primary submit-btn">Submit</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- /Add Department Modal -->
            <!-- Edit Department Modal -->
            <div id="edit_department" class="modal custom-modal fade" role="dialog">
                <div class="modal-dialog modal-dialog-centered" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title">Edit Department</h5>
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="modal-body">
                            <div class="input-block mb-3">
                                <label class="col-form-label">Department Id <span class="text-danger">*</span></label>
                                <asp:TextBox ID="txtdeptid" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>

                            <div class="input-block mb-3">
                                <label class="col-form-label">Department Name <span class="text-danger">*</span></label>
                                <asp:TextBox ID="txtdeptname" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="submit-section">
                                <asp:Button ID="btnupdatedepartment" runat="server" Text="Update" CssClass="btn btn-primary submit-btn" OnClick="btnupdatedepartment_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- /Edit Department Modal -->
            <!-- Delete Department Modal -->
            <div class="modal custom-modal fade" id="delete_department" role="dialog">
                <div class="modal-dialog modal-dialog-centered">
                    <div class="modal-content">
                        <div class="modal-body">
                            <div class="form-header">
                                <h3>Delete Department</h3>
                                <p>
                                    Are you sure want to delete
                                    <asp:Label ID="Label2" runat="server" ForeColor="#cc0000"></asp:Label>
                                    ?
                                </p>
                                <asp:HiddenField ID="HiddenField1" runat="server" />
                            </div>
                            <div class="modal-btn delete-action">
                                <div class="row">
                                    <div class="col-6">
                                        <asp:Button ID="btndeletedepartment" runat="server" Text="Delete" CssClass="btn btn-primary continue-btn" OnClick="btndeletedepartment_Click" />
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
        </div>
        <!-- /Page Wrapper -->
    </div>
    <!-- /Main Wrapper -->
    <script type="text/javascript">
        function editDepartment(DeptId, Department) {
            $('#<%= txtdeptid.ClientID %>').val(DeptId).prop('readonly', true);
            $('#<%= txtdeptname.ClientID %>').val(Department);
            $('#edit_department').modal('show');
        }
        function deletedept(DeptId, Department) {
            document.getElementById('<%= Label2.ClientID %>').textContent = Department;
            $('#<%= HiddenField1.ClientID %>').val(DeptId);
        }
    </script>
</asp:Content>
