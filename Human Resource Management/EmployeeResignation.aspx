<%@ Page Title="" Language="C#" MasterPageFile="~/Employee.Master" AutoEventWireup="true" CodeBehind="EmployeeResignation.aspx.cs" Inherits="Human_Resource_Management.EmployeeResignation1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
                            <h3 class="page-title">Resignation</h3>
                            <ul class="breadcrumb">
                                <li class="breadcrumb-item"><a href="admin-dashboard.html">Dashboard</a></li>
                                <li class="breadcrumb-item active">Resignation</li>
                            </ul>
                        </div>
                        <div class="col-auto float-end ms-auto">
                            <%-- <a href="EmployeeAddResignation.aspx" class="btn add-btn"><i class="fa-solid fa-plus"></i>Add Resignation</a>--%>
                            <a href="#" class="btn add-btn" data-bs-toggle="modal" data-bs-target="#add_resignation"><i class="fa-solid fa-plus"></i>Add Resignation</a>
                            <div class="view-icons">
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
                                        <th>Name</th>
                                        <th>Id </th>
                                        <th>Department </th>
                                        <th>Reason </th>
                                        <th>Apply Date </th>
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
                <!-- Add Resignation Modal -->
                <div id="add_resignation" class="modal custom-modal fade" role="dialog">
                    <div class="modal-dialog modal-dialog-centered" role="document">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h5 class="modal-title">Resignation Form</h5>
                                <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                            </div>
                            <div class="modal-body">
                                <div class="input-block mb-3">
                                    <label class="col-form-label">Resignation Date<span class="text-danger">*</span></label>
                                    <asp:TextBox ID="txtdate" runat="server" CssClass="form-control" TextMode="Date" AutoCompleteType="Disabled"></asp:TextBox>
                                </div>
                                <div class="input-block mb-3">
                                    <label class="col-form-label">Reason<span class="text-danger">*</span></label>
                                    <asp:TextBox ID="txtreason" runat="server" CssClass="form-control" TextMode="MultiLine" AutoCompleteType="Disabled"></asp:TextBox>
                                </div>
                                <div class="submit-section">
                                    <asp:Button ID="btnsubmitresign" runat="server" Text="Submit" CssClass="btn btn-danger submit-btn" OnClick="btnsubmitresign_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- /Add Resignation Modal -->
            </div>
            <!-- /Page Content -->


        </div>
    </div>
</asp:Content>
