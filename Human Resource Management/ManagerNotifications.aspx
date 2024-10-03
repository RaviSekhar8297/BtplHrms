<%@ Page Title="" Language="C#" MasterPageFile="~/Manager.Master" AutoEventWireup="true" CodeBehind="ManagerNotifications.aspx.cs" Inherits="Human_Resource_Management.ManagerNotifications" %>

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
                            <h3 class="page-title">Notification Settings</h3>
                            <ul class="breadcrumb">
                                <li class="breadcrumb-item"><a href="admin-dashboard.html">Dashboard</a></li>
                                <li class="breadcrumb-item active">Notification</li>
                            </ul>
                        </div>
                        <div class="col-auto float-end ms-auto">
                            <a href="#" class="btn add-btn" data-bs-toggle="modal" data-bs-target="#add_notification"><i class="fa-solid fa-plus"></i>Send Notification</a>
                            <a href="ManagerChat.aspx" class="btn add-btn"><i class="fa-solid fa-plus"></i>Chat</a>
                        </div>
                    </div>
                </div>
                <!-- /Page Header -->
                <%--<asp:Button ID="btnExportToExcel" runat="server" Text="Excel" OnClick="btnExportToExcel_Click" />--%>
                <div class="row">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="table-responsive">
                                <table class="table table-striped custom-table datatable">
                                    <thead>
                                        <tr>                                           
                                            <th>EmpId</th>                                         
                                            <th>Branch</th>
                                            <th>Subject</th>
                                            <th>Message</th>
                                            <th>SendBy</th>
                                             <th>SendTo</th>
                                            <th>Date</th>
                                            <th>Status</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:PlaceHolder ID="NotificationData" runat="server"></asp:PlaceHolder>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- Add Resignation Modal -->
                <div id="add_notification" class="modal custom-modal fade" role="dialog">
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
                                    <label class="col-form-label">Subject<span class="text-danger">*</span></label>
                                    <asp:TextBox ID="txtsubject" runat="server" CssClass="form-control" AutoCompleteType="Disabled"></asp:TextBox>
                                </div>
                                <div class="input-block mb-3">
                                    <label class="col-form-label">Message<span class="text-danger">*</span></label>
                                    <asp:TextBox ID="txtreason" runat="server" CssClass="form-control" TextMode="MultiLine" AutoCompleteType="Disabled"></asp:TextBox>
                                </div>
                                <div class="submit-section">
                                    <asp:Button ID="btnsendnotification" runat="server" Text="Send" CssClass="btn btn-danger submit-btn" OnClick="btnsendnotification_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- /Add Resignation Modal -->
            </div>
        </div>
        <!-- /Page Content -->
    </div>
    <!-- /Page Wrapper -->
</asp:Content>
