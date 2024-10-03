<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="AdminResignation.aspx.cs" Inherits="Human_Resource_Management.AdminResignation" %>

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
                            <h3 class="page-title">Resignation</h3>
                            <ul class="breadcrumb">
                                <li class="breadcrumb-item"><a href="Admindashboard">Dashboard</a></li>
                                <li class="breadcrumb-item active">Resignation</li>
                            </ul>
                        </div>
                        <div class="col-auto float-end ms-auto">
                            <a href="AdminAddResignation.aspx" class="btn add-btn"><i class="fa-solid fa-plus"></i>Add Resignation</a>
                            <%--     <a href="#" class="btn add-btn" data-bs-toggle="modal" data-bs-target="#add_resignation"><i class="fa-solid fa-plus"></i>Add Resignation</a>--%>
                                <div class="view-icons">
                                   
                                   <%-- <a href="AdminResignation.aspx" class="list-view btn btn-link "><i class="fa-solid fa-bars"></i></a>--%>
                                  
                                </div>

                        </div>
                    </div>
                </div>
                <!-- /Page Header -->

                <div class="row">
                    <div class="col-md-12">
                        <div class="table-responsive">
                            <table class="table table-striped custom-table mb-0 datatable leave-employee-table">
                                <thead>
                                    <tr>
                                        <th>Image</th>
                                        <th>Id </th>
                                        <th>Department </th>
                                        <th>Reason </th>
                                        <th>Notice Date </th>
                                        <th>Resignation Date </th>
                                        <th>Status </th>
                                       
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:PlaceHolder ID="ResignData" runat="server"></asp:PlaceHolder>

                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
            <!-- /Page Content -->


           

            <!-- Edit Resignation Modal -->
            <div id="edit_resignation" class="modal custom-modal fade" role="dialog">
                <div class="modal-dialog modal-dialog-centered" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title">Edit Resignation</h5>
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="modal-body">
                            <asp:HiddenField ID="HiddenField2" runat="server" />
                            <div class="input-block mb-3">
                                <label class="col-form-label">Employee Name <span class="text-danger">*</span></label>
                                <asp:TextBox ID="txtedname" runat="server" CssClass="form-control" AutoCompleteType="Disabled"></asp:TextBox>
                            </div>
                            <div class="input-block mb-3">
                                <label class="col-form-label">Employee ID<span class="text-danger">*</span></label>
                                <asp:TextBox ID="txtedid" runat="server" CssClass="form-control" AutoCompleteType="Disabled" ReadOnly="true"></asp:TextBox>
                            </div>
                            <div class="input-block mb-3">
                                <label class="col-form-label">Resignation Date <span class="text-danger">*</span></label>
                                <asp:TextBox ID="txtedresdate" runat="server" CssClass="form-control" AutoCompleteType="Disabled" TextMode="Date"></asp:TextBox>
                            </div>
                            <div class="input-block mb-3">
                                <label class="col-form-label">Employee Reason<span class="text-danger">*</span></label>
                                <asp:TextBox ID="txtedreason" runat="server" CssClass="form-control" AutoCompleteType="Disabled" TextMode="MultiLine"></asp:TextBox>
                            </div>
                            <div class="input-block mb-3">
                                <label class="col-form-label">Approve / Reject Reason<span class="text-danger">*</span></label>
                                <asp:TextBox ID="hrreason" runat="server" CssClass="form-control" AutoCompleteType="Disabled" TextMode="MultiLine"></asp:TextBox>
                            </div>
                            <%--<div class="input-block mb-3">
										<label class="col-form-label">Approve/Reject Reason <span class="text-danger">*</span></label>
										<asp:TextBox ID="txtedappresreason" runat="server" CssClass="form-control" AutoCompleteType="Disabled" TextMode="MultiLine"></asp:TextBox>
									</div>--%>
                            <div class="submit-section">
                                <asp:Button ID="btnReject" runat="server" Text="Reject" CssClass="btn btn-danger submit-btn" OnClick="btnReject_Click" />
                                <asp:Button ID="btnApprove" runat="server" Text="Approve" CssClass="btn btn-primary submit-btn" OnClick="btnApprove_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- /Edit Resignation Modal -->

            <!-- Delete Resignation Modal -->
            <div class="modal custom-modal fade" id="delete_resignation" role="dialog">
                <div class="modal-dialog modal-dialog-centered">
                    <div class="modal-content">
                        <div class="modal-body">
                            <div class="form-header">
                                <h3>Delete Resignation</h3>
                                <p>Are you sure want to delete this ?</p>
                            </div>
                            <div class="modal-btn delete-action">
                                <div class="row">
                                    <div class="col-6">
                                        <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="btn btn-primary continue-btn" OnClick="btnDelete_Click" />
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
            <!-- /Delete Resignation Modal -->

        </div>
        <!-- /Page Wrapper -->

    </div>
    <!-- /Main Wrapper -->


    <script type="text/javascript"> 
        function editResign(Name, Id, ResignDate, Reason, ApproveStatus, ApproveReson) {
            $('#<%= txtedname.ClientID %>').val(Name);
            $('#<%= txtedid.ClientID %>').val(Id).prop('readonly', true);
            $('#<%= HiddenField2.ClientID %>').val(Id);
            $('#<%= txtedresdate.ClientID %>').val(ResignDate);
            $('#<%= txtedreason.ClientID %>').val(Reason);

            $('#edit_resignation').modal('show');
        }
    </script>  
</asp:Content>
