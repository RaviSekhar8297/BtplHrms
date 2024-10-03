<%@ Page Title="" Language="C#" MasterPageFile="~/Manager.Master" AutoEventWireup="true" CodeBehind="ManagerAttendanceRequest.aspx.cs" Inherits="Human_Resource_Management.ManagerAttendanceRequest" %>

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
                            <h3 class="page-title">Attendance Requests</h3>
                            <ul class="breadcrumb">
                                <li class="breadcrumb-item"><a href="ManagerDashbord.aspx">Dashboard</a></li>
                                <li class="breadcrumb-item active">Attendance Requests</li>
                            </ul>
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
                                    <asp:PlaceHolder ID="AttendanceRequestData" runat="server"></asp:PlaceHolder>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>

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
                                <asp:HiddenField ID="HiddenField1" runat="server" />
                                <asp:HiddenField ID="HiddenField2" runat="server" />
                                <asp:HiddenField ID="HiddenField3" runat="server" />
                                <div class="input-block mb-3">
                                    <label class="col-form-label">EmpId <span class="text-danger">*</span></label>
                                    <asp:TextBox ID="txtid" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="input-block mb-3">
                                    <label class="col-form-label">Name <span class="text-danger">*</span></label>
                                    <asp:TextBox ID="txtname" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>

                                <div class="input-block mb-3">
                                    <label class="col-form-label">Date <span class="text-danger">*</span></label>
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
            </div>
        </div>
    </div>


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
