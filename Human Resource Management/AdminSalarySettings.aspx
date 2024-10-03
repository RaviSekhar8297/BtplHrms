<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="AdminSalarySettings.aspx.cs" Inherits="Human_Resource_Management.AdminSalarySettings" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Page Wrapper -->
    <div class="page-wrapper">

        <!-- Page Content -->
        <div class="content container-fluid">
            <div class="row">
                <!-- Page Header -->
                <div class="page-header">
                    <div class="row align-items-center">
                        <div class="col">
                            <h3 class="page-title">Department</h3>
                            <ul class="breadcrumb">
                                <li class="breadcrumb-item"><a href="Admindashboard">Salary Settings</a></li>
                                <li class="breadcrumb-item active"></li>
                            </ul>
                        </div>
                        <div class="col-auto float-end ms-auto">
                        </div>
                    </div>
                </div>
                <!-- /Page Header -->

                <div class="row">
                    <div class="col-md-12">
                        <div>
                            <table class="table table-striped custom-table mb-0 datatable">
                                <thead>
                                    <tr>
                                        <th>S.No</th>
                                        <th>PF Type</th>
                                        <th>HRA</th>
                                        <th>EA</th>
                                        <th>DA</th>
                                        <th>CA</th>
                                        <th>IT</th>
                                        <th>LIC</th>
                                        <th>PF</th>
                                        <th>ESI</th>
                                        <th>MA</th>
                                        <th>SA</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:PlaceHolder ID="PfTypeList" runat="server"></asp:PlaceHolder>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
                <!-- Edit PF Modal -->
                <div id="edit_pfTypeTax" class="modal custom-modal fade" role="dialog">
                    <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h5 class="modal-title">Edit Salary Setting</h5>
                                <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                            </div>
                            <div class="modal-body">
                                <asp:HiddenField ID="HiddenField1" runat="server" />
                                <asp:HiddenField ID="HiddenField2" runat="server" />
                                <div class="row">
                                    <div class="col-sm-4">
                                        <div class="input-block mb-3">
                                            <label class="col-form-label">Name <span class="text-danger">*</span></label>
                                            <asp:TextBox ID="txtname" runat="server" CssClass="form-control" AutoCompleteType="Disabled" onkeypress="return isCharacterKey(event)"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="input-block mb-3">
                                            <label class="col-form-label">HRA <span class="text-danger">*</span></label>
                                            <asp:TextBox ID="txthra" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="input-block mb-3">
                                            <label class="col-form-label">EA <span class="text-danger">*</span></label>
                                            <asp:TextBox ID="txtea" runat="server" CssClass="form-control" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="input-block mb-3">
                                            <label class="col-form-label">DA <span class="text-danger">*</span></label>
                                            <asp:TextBox ID="txtda" runat="server" CssClass="form-control" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="input-block mb-3">
                                            <label class="col-form-label">CA<span class="text-danger">*</span></label>
                                            <asp:TextBox ID="txtca" runat="server" CssClass="form-control" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="input-block mb-3">
                                            <label class="col-form-label">IT <span class="text-danger">*</span></label>
                                            <asp:TextBox ID="txtit" runat="server" CssClass="form-control" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="input-block mb-3">
                                            <label class="col-form-label">LIC <span class="text-danger">*</span></label>
                                            <asp:TextBox ID="txtlic" runat="server" CssClass="form-control" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="input-block mb-3">
                                            <label class="col-form-label">PF <span class="text-danger">*</span></label>
                                            <asp:TextBox ID="txtpf" runat="server" CssClass="form-control" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="input-block mb-3">
                                            <label class="col-form-label">ESI <span class="text-danger">*</span></label>
                                            <asp:TextBox ID="txtesi" runat="server" CssClass="form-control" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="input-block mb-3">
                                            <label class="col-form-label">MA <span class="text-danger">*</span></label>
                                            <asp:TextBox ID="txtma" runat="server" CssClass="form-control" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="input-block mb-3">
                                            <label class="col-form-label">SA <span class="text-danger">*</span></label>
                                            <asp:TextBox ID="txtsa" runat="server" CssClass="form-control" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="table-responsive m-t-15">
                                </div>
                                <div class="submit-section">
                                    <asp:Button ID="btncountupdate" runat="server" Text="Update" CssClass="btn btn-primary submit-btn" OnClick="btncountupdate_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- /Edit PF Modal -->
            </div>
        </div>
        <!-- /Page Content -->

    </div>
    <!-- /Page Wrapper -->

    <script type="text/javascript">
       
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }
        function isCharacterKey(event) {
            var charCode = (event.which) ? event.which : event.keyCode;
            // Allow: A-Z, a-z, and space (charCode 32 is space)
            if ((charCode >= 65 && charCode <= 90) || (charCode >= 97 && charCode <= 122) || charCode == 32) {               
                return true;
            } else {               
                return false;
            }
        }
        function editPfTax(Id, PFId, Name, HRA, EA, DA, CA, IT, LIC, PF, ESI, MA, SA) {
            $('#<%= HiddenField1.ClientID %>').val(Id);
            $('#<%= HiddenField2.ClientID %>').val(PFId);
            $('#<%= txtname.ClientID %>').val(Name);
            $('#<%= txthra.ClientID %>').val(HRA);
            $('#<%= txtea.ClientID %>').val(EA);
            $('#<%= txtda.ClientID %>').val(DA);
            $('#<%= txtca.ClientID %>').val(CA);
            $('#<%= txtit.ClientID %>').val(IT);
            $('#<%= txtlic.ClientID %>').val(LIC);
            $('#<%= txtpf.ClientID %>').val(PF);
            $('#<%= txtesi.ClientID %>').val(ESI);
            $('#<%= txtma.ClientID %>').val(MA);
            $('#<%= txtsa.ClientID %>').val(SA);
        }
    </script>
</asp:Content>
