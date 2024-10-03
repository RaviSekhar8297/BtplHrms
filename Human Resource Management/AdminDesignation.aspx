<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="AdminDesignation.aspx.cs" Inherits="Human_Resource_Management.WebForm1" %>

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
                            <h3 class="page-title">Designations</h3>
                            <ul class="breadcrumb">
                                <li class="breadcrumb-item"><a href="Admindashboard">Dashboard</a></li>
                                <li class="breadcrumb-item active">Designations</li>
                            </ul>
                        </div>
                        <div class="col-auto float-end ms-auto">
                            <%--  <a href="AdminAddDesignation.aspx" class="btn add-btn"><i class="fa-solid fa-plus"></i> Add Designation</a>--%>
                            <%--<a href="#" class="btn add-btn" data-bs-toggle="modal" data-bs-target="#add_designation"><i class="fa-solid fa-plus"></i> Add Designation</a>--%>
                        </div>
                    </div>
                </div>
                <!-- /Page Header -->

                <!-- Search Filter -->
                <div class="row filter-row">
                    <div class="col-sm-6 col-md-3">
                        <div class="input-block mb-3 form-focus focused">
                            <asp:DropDownList ID="DropDownList4" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="DropDownList4_SelectedIndexChanged">
                            </asp:DropDownList>
                            <label class="focus-label">Branch</label>
                        </div>
                    </div>
                </div>
                <!-- Search Filter -->

                <div class="row">
                    <div class="col-md-12">
                        <div class="table-responsive">
                            <table class="table table-striped custom-table mb-0 datatable">
                                <thead>
                                    <tr>
                                        <th>S No</th>
                                        <th>Branch </th>
                                        <th>Designation </th>
                                        <th>Edit </th>
                                        <%--<th>Delete</th>--%>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:PlaceHolder ID="DesignationData" runat="server"></asp:PlaceHolder>

                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
            <!-- /Page Content -->

            <!-- Add Designation Modal -->
            <div id="add_designation" class="modal custom-modal fade" role="dialog">
                <div class="modal-dialog modal-dialog-centered" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title">Add Designation</h5>
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="modal-body">
                            <div class="input-block mb-3">
                                <label class="col-form-label">Designation Name <span class="text-danger">*</span></label>
                                <input class="form-control" type="text">
                            </div>
                            <div class="input-block mb-3">
                                <label class="col-form-label">Department <span class="text-danger">*</span></label>
                                <select class="select">
                                    <option>Select Department</option>
                                    <option>Web Development</option>
                                    <option>IT Management</option>
                                    <option>Marketing</option>
                                </select>
                            </div>
                            <div class="submit-section">
                                <button class="btn btn-primary submit-btn">Submit</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- /Add Designation Modal -->

            <!-- Edit Designation Modal -->
            <div id="edit_designation" class="modal custom-modal fade" role="dialog">
                <div class="modal-dialog modal-dialog-centered" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title">Edit Designation</h5>
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="modal-body">
                            <div class="input-block mb-3">
                                <label class="col-form-label">Id <span class="text-danger">*</span></label>
                                <asp:TextBox ID="txtid" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="input-block mb-3">
                                <label class="col-form-label">Designation Name <span class="text-danger">*</span></label>
                                <asp:TextBox ID="txtdesignationname" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="input-block mb-3">
                                <label class="col-form-label">Change Designation <span class="text-danger">*</span></label>
                                <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="submit-section">
                                <asp:Button ID="btnupdate" runat="server" Text="Save" CssClass="btn btn-primary submit-btn" OnClick="btnupdate_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- /Edit Designation Modal -->

            <!-- Delete Designation Modal -->
            <div class="modal custom-modal fade" id="delete_designation" role="dialog">
                <div class="modal-dialog modal-dialog-centered">
                    <div class="modal-content">
                        <div class="modal-body">
                            <div class="form-header">
                                <h3>Delete Designation</h3>
                                <p>Are you sure want to delete
                                    <asp:Label ID="Label2" runat="server" ForeColor="#cc0000"></asp:Label>
                                    ?</p>
                                <asp:HiddenField ID="HiddenField1" runat="server" />
                            </div>
                            <div class="modal-btn delete-action">
                                <div class="row">
                                    <div class="col-6">
                                        <asp:Button ID="btndelete" runat="server" Text="Delete" CssClass="btn btn-primary continue-btn" OnClick="btndelete_Click" />
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
            <!-- /Delete Designation Modal -->

        </div>
        <!-- /Page Wrapper -->
    </div>
    <!-- /Main Wrapper -->

    <script type="text/javascript">
        function editDesignation(S_No, Desgntion) {

            $('#<%= txtid.ClientID %>').val(S_No).prop('readonly', true);
            $('#<%= txtdesignationname.ClientID %>').val(Desgntion).prop('readonly', true);
            $('#<%= TextBox1.ClientID %>').val(Desgntion);
            $('#edit_designation').modal('show');
        }

        function deleteDesignation(S_No, Desgntion) {
            document.getElementById('<%= Label2.ClientID %>').textContent = Desgntion;
            $('#<%= HiddenField1.ClientID %>').val(S_No);
        }
       

    </script>
</asp:Content>
