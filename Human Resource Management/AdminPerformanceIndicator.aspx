<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="AdminPerformanceIndicator.aspx.cs" Inherits="Human_Resource_Management.AdminPerformanceIndicator" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

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
                            <h3 class="page-title">Performance Indicator</h3>
                            <ul class="breadcrumb">
                                <li class="breadcrumb-item"><a href="Admindashboard">Dashboard</a></li>
                                <li class="breadcrumb-item active">Performance</li>
                            </ul>
                        </div>
                        <div class="col-auto float-end ms-auto">
                            <a href="AdminAddPerformanceIndicator.aspx" class="btn add-btn"><i class="fa-solid fa-plus"></i>Add New</a>
                        </div>
                    </div>
                </div>
                <!-- /Page Header -->

                <div class="row">
                    <div class="col-md-12">
                        <div class="table-responsive">
                            <table class="table table-striped custom-table mb-0 datatable">
                                <thead>
                                    <tr>
                                        <th class="width-thirty">S_No</th>
                                        <th>Id</th>
                                        <th>Name</th>
                                        <th>Designation</th>
                                        <th>Date</th>
                                        <th>Review</th>
                                        <th>Review By</th>
                                        <th>Edit</th>
                                        <th>Delete</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:PlaceHolder ID="ReviewData" runat="server"></asp:PlaceHolder>
                                    <%--	<tr>
										<td>1</td>
										<td>Web Designer </td>
										<td>Designing</td>
										<td>
											<h2 class="table-avatar">
												<a href="profile.html" class="avatar"><img src="assets/img/profiles/avatar-02.jpg" alt="User Image"></a>
												<a href="profile.html">John Doe </a>
											</h2>
										</td>
										<td>
											7 May 2019
										</td>
										<td>
											<div class="dropdown action-label">
												<a class="btn btn-white btn-sm btn-rounded dropdown-toggle" href="#" data-bs-toggle="dropdown" aria-expanded="false">
													<i class="fa-regular fa-circle-dot text-success"></i> Active
												</a>
												<div class="dropdown-menu">
													<a class="dropdown-item" href="#"><i class="fa-regular fa-circle-dot text-success"></i> Active</a>
													<a class="dropdown-item" href="#"><i class="fa-regular fa-circle-dot text-danger"></i> Inactive</a>
												</div>
											</div>
										</td>
										<td class="text-end">
											<div class="dropdown dropdown-action">
												<a href="#" class="action-icon dropdown-toggle" data-bs-toggle="dropdown" aria-expanded="false"><i class="material-icons">more_vert</i></a>
												<div class="dropdown-menu dropdown-menu-right">
													<a class="dropdown-item" href="#" data-bs-toggle="modal" data-bs-target="#edit_indicator"><i class="fa-solid fa-pencil m-r-5"></i> Edit</a>
													<a class="dropdown-item" href="#" data-bs-toggle="modal" data-bs-target="#delete_indicator"><i class="fa-regular fa-trash-can m-r-5"></i> Delete</a>
												</div>
											</div>
										</td>
									</tr>--%>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
            <!-- /Page Content -->



            <!-- Edit Performance Indicator Modal -->
            <div id="edit_indicator" class="modal custom-modal fade" role="dialog">
                <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title">Edit Performance Indicator</h5>
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <asp:HiddenField ID="HiddenField1" runat="server" />
                                <div class="col-sm-12">
                                    <div class="input-block mb-3">
                                        <label class="col-form-label">EmpId</label>
                                        <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-12">
                                    <div class="input-block mb-6">
                                        <label class="col-form-label">Name</label>
                                        <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="input-block mb-12">
                                        <label class="col-form-label">Regular</label>
                                        <asp:TextBox ID="TextBox3" runat="server" CssClass="form-control" OnTextChanged="TextBox3_TextChanged" AutoPostBack="true"></asp:TextBox>
                                    </div>
                                    <div class="input-block mb-3">
                                        <label class="col-form-label">Behaviour</label>
                                        <asp:TextBox ID="TextBox4" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="input-block mb-3">
                                        <label class="col-form-label">Work</label>
                                        <asp:TextBox ID="TextBox5" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="submit-section">
                                <asp:Button ID="btnupdatereview" runat="server" Text="Update" CssClass="btn btn-primary submit-btn" OnClick="btnupdatereview_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- /Edit Performance Indicator Modal -->

            <!-- Delete Performance Indicator Modal -->
            <div class="modal custom-modal fade" id="delete_indicator" role="dialog">
                <div class="modal-dialog modal-dialog-centered">
                    <div class="modal-content">
                        <div class="modal-body">
                            <div class="form-header">
                                <h3>Delete Performance Indicator List</h3>
                                <p>Are you sure want to delete
                                    <asp:Label ID="Label1" runat="server" ForeColor="Red"></asp:Label> ?</p>
                            </div>
                            <asp:HiddenField ID="HiddenField2" runat="server" />
                            <div class="modal-btn delete-action">
                                <div class="row">
                                    <div class="col-6">
                                        <asp:Button ID="btnreviewdelete" runat="server" Text="Delete" CssClass="btn btn-primary continue-btn" OnClick="btnreviewdelete_Click" />
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
            <!-- /Delete Performance Indicator Modal -->

        </div>
        <!-- /Page Wrapper -->
    </div>
    <!-- /Main Wrapper -->
    <script type="text/javascript">
        function editreview(Id, EmpId, Name, Regular, Behaviour, Work, OvarallReview) {
            $('#<%= HiddenField1.ClientID %>').val(Id);
            $('#<%= TextBox1.ClientID %>').val(EmpId).prop('readonly', true);
            $('#<%= TextBox2.ClientID %>').val(Name);
            $('#<%= TextBox3.ClientID %>').val(Regular);
            $('#<%= TextBox4.ClientID %>').val(Behaviour);
            $('#<%= TextBox5.ClientID %>').val(Work);
        }

        function deletereview(Id, Name) {
            document.getElementById('<%= Label1.ClientID %>').textContent = Name;
            $('#<%= HiddenField2.ClientID %>').val(Id);
        }
    </script>
</asp:Content>
