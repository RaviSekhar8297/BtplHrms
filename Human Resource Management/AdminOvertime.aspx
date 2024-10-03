<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="AdminOvertime.aspx.cs" Inherits="Human_Resource_Management.AdminOvertime" %>

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
                            <h3 class="page-title">Overtime</h3>
                            <ul class="breadcrumb">
                                <li class="breadcrumb-item"><a href="Admindashboard">Dashboard</a></li>
                                <li class="breadcrumb-item active">Overtime</li>
                            </ul>
                        </div>
                        <div class="col-auto float-end ms-auto">
                            <a href="#" class="btn add-btn" data-bs-toggle="modal" data-bs-target="#add_overtime"><i class="fa-solid fa-plus"></i>Add Overtime</a>
                        </div>
                    </div>
                </div>
                <!-- /Page Header -->

                <!-- Overtime Statistics -->
                <div class="row">
                    <div class="col-md-6 col-sm-6 col-lg-6 col-xl-3">
                        <div class="stats-info">
                            <h6>Total Requests</h6>
                            <h3>
                                <asp:Label ID="Label1" runat="server" ForeColor="DeepPink" Text="0"></asp:Label></h3>
                        </div>
                    </div>
                    <div class="col-md-6 col-sm-6 col-lg-6 col-xl-3">
                        <div class="stats-info">
                            <h6>Approve</h6>
                            <h3>
                                <asp:Label ID="Label2" runat="server" ForeColor="green" Text="0"></asp:Label>
                            </h3>
                        </div>
                    </div>
                    <div class="col-md-6 col-sm-6 col-lg-6 col-xl-3">
                        <div class="stats-info">
                            <h6>Pending Request</h6>
                            <h3>
                                <asp:Label ID="Label4" runat="server" ForeColor="blue" Text="0"></asp:Label></h3>
                        </div>
                    </div>
                    <div class="col-md-6 col-sm-6 col-lg-6 col-xl-3">
                        <div class="stats-info">
                            <h6>Rejected</h6>
                            <h3>
                                <asp:Label ID="Label3" runat="server" ForeColor="red" Text="0"></asp:Label></h3>
                        </div>
                    </div>
                </div>
                <!-- /Overtime Statistics -->

                <div class="row">
                    <div class="col-md-12">
                        <div class="table-responsive">
                            <table class="table table-striped custom-table leave-employee-table mb-0 datatable">
                                <thead>
                                    <tr>
                                        <th>S.No</th>
                                        <th>Name</th>
                                        <th>EmpId</th>
                                        <th>Date</th>
                                        <th>RequestType</th>
                                        <th>Time</th>
                                        <th>Reason</th>
                                        <th>Manager</th>
                                        <th>Admin</th>
                                        
                                        <%--	<th>Approved by</th>
										<th class="text-end">Actions</th>--%>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:PlaceHolder ID="overTime" runat="server"></asp:PlaceHolder>
                                    <%--<tr>
										<td>1</td>
										<td>
											<h2 class="table-avatar blue-link">
												<a href="profile.html" class="avatar"><img src="assets/img/profiles/avatar-02.jpg" alt="User Image"></a>
												<a href="profile.html">John Doe</a>
											</h2>
										</td>
										<td>8 Mar 2019</td>
										<td class="text-center">2</td>
										<td>Normal day OT 1.5x</td>
										<td>Lorem ipsum dollar</td>
										<td class="text-center">
											<div class="action-label">
												<a class="btn btn-white btn-sm btn-rounded" href="javascript:void(0);">
													<i class="fa-regular fa-circle-dot text-purple"></i> New
												</a>
											</div>
										</td>
										<td>
											<h2 class="table-avatar">
												<a href="profile.html" class="avatar avatar-xs"><img src="assets/img/profiles/avatar-09.jpg" alt="User Image"></a>
												<a href="#">Richard Miles</a>
											</h2>
										</td>
										<td class="text-end">
											<div class="dropdown dropdown-action">
												<a href="#" class="action-icon dropdown-toggle" data-bs-toggle="dropdown" aria-expanded="false"><i class="material-icons">more_vert</i></a>
												<div class="dropdown-menu dropdown-menu-right">
													<a class="dropdown-item" href="#" data-bs-toggle="modal" data-bs-target="#edit_overtime"><i class="fa-solid fa-pencil m-r-5"></i> Edit</a>
													<a class="dropdown-item" href="#" data-bs-toggle="modal" data-bs-target="#delete_overtime"><i class="fa-regular fa-trash-can m-r-5"></i> Delete</a>
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

            <!-- Add Overtime Modal -->
            <div id="add_overtime" class="modal custom-modal fade" role="dialog">
                <div class="modal-dialog modal-dialog-centered" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title">Add Overtime</h5>
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="modal-body">
                            <div class="input-block mb-3">
                                <label class="col-form-label">Select Employee <span class="text-danger">*</span></label>
                                <select class="select">
                                    <option>-</option>
                                    <option>John Doe</option>
                                    <option>Richard Miles</option>
                                    <option>John Smith</option>
                                </select>
                            </div>
                            <div class="input-block mb-3">
                                <label class="col-form-label">Overtime Date <span class="text-danger">*</span></label>
                                <div class="cal-icon">
                                    <input class="form-control datetimepicker" type="text">
                                </div>
                            </div>
                            <div class="input-block mb-3">
                                <label class="col-form-label">Overtime Hours <span class="text-danger">*</span></label>
                                <input class="form-control" type="text">
                            </div>
                            <div class="input-block mb-3">
                                <label class="col-form-label">Description <span class="text-danger">*</span></label>
                                <textarea rows="4" class="form-control"></textarea>
                            </div>
                            <div class="submit-section">
                                <button class="btn btn-primary submit-btn">Submit</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- /Add Overtime Modal -->

            <!-- Edit Overtime Modal -->
            <div id="edit_overtime" class="modal custom-modal fade" role="dialog">
                <div class="modal-dialog modal-dialog-centered" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title">Edit Overtime</h5>
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="modal-body">
                            <asp:HiddenField ID="HiddenField1" runat="server" /><asp:HiddenField ID="HiddenField2" runat="server" /><asp:HiddenField ID="HiddenField3" runat="server" />
                            <div class="input-block mb-3">
                                <label class="col-form-label">EmpId <span class="text-danger">*</span></label>
                                <asp:TextBox ID="txtid" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="input-block mb-3">
                                <label class="col-form-label">Name <span class="text-danger">*</span></label>
                                <asp:TextBox ID="txtname" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            
                            <div class="input-block mb-3">
                                <label class="col-form-label"> Date <span class="text-danger">*</span></label>
                                <asp:TextBox ID="txtdate" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                            </div>
                            <div class="input-block mb-3">
                                <label class="col-form-label">RequestType <span class="text-danger">*</span></label>
                                <asp:TextBox ID="txtreqtype" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="input-block mb-3">
                                <label class="col-form-label">Duration <span class="text-danger">*</span></label>
                                <asp:TextBox ID="txttime" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="input-block mb-3">
                                <label class="col-form-label">Reason <span class="text-danger">*</span></label>
                                <asp:TextBox ID="txtreason" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                            </div>
                            <div class="submit-section">
                                <asp:Button ID="btnapprove" runat="server" Text="Approve" CssClass="btn btn-primary submit-btn" OnClick="btnapprove_Click" />
                                <asp:Button ID="btnreject" runat="server" Text="Reject" CssClass="btn btn-danger submit-btn" OnClick="btnreject_Click" />
                                <asp:Button ID="btnupdate" runat="server" Text="Accept" CssClass="btn btn-primary submit-btn" OnClick="btnupdate_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- /Edit Overtime Modal -->

            <!-- Delete Overtime Modal -->
            <div class="modal custom-modal fade" id="delete_overtime" role="dialog">
                <div class="modal-dialog modal-dialog-centered">
                    <div class="modal-content">
                        <div class="modal-body">
                            <div class="form-header">
                                <h3>Delete Overtime</h3>
                                <p>Are you sure want to Cancel this?</p>
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
            <!-- /Delete Overtime Modal -->

        </div>
        <!-- /Page Wrapper -->
    </div>
    <!-- /Main Wrapper -->

    <script type="text/javascript">
        function AproveRequest(Id, Name, EmpId, RequestDate, RequestType, PunchTime, Reason, ManagerApprove, BranchId) {
            $('#<%= HiddenField1.ClientID %>').val(Id);
            $('#<%= txtname.ClientID %>').val(Name);
            $('#<%= txtid.ClientID %>').val(EmpId).prop('readonly', true);
            $('#<%= HiddenField2.ClientID %>').val(EmpId);
            $('#<%= txtdate.ClientID %>').val(RequestDate);
            $('#<%= txtreqtype.ClientID %>').val(RequestType);
            $('#<%= txttime.ClientID %>').val(PunchTime);
            $('#<%= txtreason.ClientID %>').val(Reason);          
           <%-- $('#<%= branchid.ClientID %>').val(BranchId).prop('readonly', true);--%>
            $('#<%= HiddenField3.ClientID %>').val(BranchId);
            $('#edit_overtime').modal('show');
            if (RequestType === "OverTime") {

                $('#<%= btnapprove.ClientID %>').show();
                $('#<%= btnreject.ClientID %>').show();
                $('#<%= btnupdate.ClientID %>').hide();
            } else {
               
                $('#<%= btnapprove.ClientID %>').hide();
                $('#<%= btnreject.ClientID %>').hide();
                $('#<%= btnupdate.ClientID %>').show();
            }
        }
    </script>

</asp:Content>
