<%@ Page Title="" Language="C#" MasterPageFile="~/Manager.Master" AutoEventWireup="true" CodeBehind="ManagerBalenceLeaves.aspx.cs" Inherits="Human_Resource_Management.ManagerBalenceLeaves" %>

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
                            <h3 class="page-title">Balance Leaves</h3>
                            <ul class="breadcrumb">
                                <li class="breadcrumb-item"><a href="Admindashboard.aspx">Dashboard</a></li>
                                <li class="breadcrumb-item active">Balance Leaves</li>
                            </ul>
                        </div>
                        <div class="col-sm-6 col-md-4">
                            <div class="input-block mb-3 form-focus select-focus">
                                <asp:TextBox ID="txtempnamesearch" runat="server" CssClass="form-control floating" placeholder="Search By Name" AutoPostBack="true" AutoCompleteType="Disabled" OnTextChanged="txtempnamesearch_TextChanged"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- /Page Header -->

                <!-- /Leave Statistics -->

                <div class="row">
                    <div class="col-md-12">
                        <div class="table-responsive">
                            <table class="table table-striped custom-table leave-employee-table mb-0 datatable">
                                <thead>
                                    <tr>
                                        <th>Name</th>
                                        <th>Id</th>
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
                                <div class="input-block mb-3">
                                    <label class="col-form-label">Name<span class="text-danger">*</span></label>
                                    <asp:TextBox ID="txtname" runat="server" CssClass="form-control"></asp:TextBox>
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
            </div>
            <!-- /Page Content -->
        </div>
    </div>
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

    </script>
</asp:Content>
