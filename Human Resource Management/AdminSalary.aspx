<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="AdminSalary.aspx.cs" Inherits="Human_Resource_Management.AdminSalary" %>

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
                            <h3 class="page-title">Employee Salary</h3>
                            <ul class="breadcrumb">
                                <li class="breadcrumb-item"><a href="Admindashboard.aspx">Dashboard</a></li>
                                <li class="breadcrumb-item active">Salary</li>
                            </ul>
                        </div>
                        <div class="col-auto float-end ms-auto">
                            <a href="#" class="btn add-btn" data-bs-toggle="modal" data-bs-target="#add_salary"><i class="fa-solid fa-plus"></i>Add Salary</a>
                        </div>
                    </div>
                </div>
                <!-- /Page Header -->


                <!-- Search Filter -->
                <div class="row filter-row">
                    <div class="col-sm-6 col-md-3 col-lg-3 col-xl-3 col-12">
                        <div class="input-block mb-3 form-focus select-focus">
                            <asp:TextBox ID="txtname" runat="server" CssClass="form-control"></asp:TextBox>
                            <label class="focus-label">Employee Name</label>
                        </div>
                    </div>
                    <div class="col-sm-6 col-md-3 col-lg-3 col-xl-3 col-12">
                        <div class="input-block mb-3 form-focus select-focus">
                            <asp:TextBox ID="TextBox18" runat="server" CssClass="form-control" TextMode="Month" AutoPostBack="true" OnTextChanged="TextBox18_TextChanged"></asp:TextBox>
                            <label class="focus-label">Month</label>
                        </div>
                    </div>
                    <div class="col-sm-6 col-md-3 col-lg-3 col-xl-3 col-12">
                         <div class="input-block mb-3 form-focus select-focus">
                             <asp:LinkButton ID="lnkexcel" runat="server" OnClick="lnkexcel_Click"><i class="fa-solid fa-file-excel" style="height:100px;width:100px;"></i></asp:LinkButton>
                             </div>
                    </div>
                </div>
                <!-- /Search Filter -->

                <div class="row">
                    <div class="col-md-12">
                        <div class="table-responsive">
                            <table class="table table-striped custom-table datatable">
                                <thead>
                                    <tr style="background-color: green;">
                                        <th style="color: deeppink;">Name</th>
                                        
                                        <th style="color: deeppink;">Month Salary</th>
                                        <th style="color: deeppink;">GrossSalary</th>
                                        <th style="color: deeppink;">NetSalary</th>
                                        <th style="color: deeppink;">PF</th>
                                        <th style="color: deeppink;">ESI</th>
                                        <th style="color: deeppink;">PT</th>

                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:PlaceHolder ID="salaryData" runat="server"></asp:PlaceHolder>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
            <!-- /Page Content -->

            <!-- Add Salary Modal -->
            <div id="add_salary" class="modal custom-modal fade" role="dialog">
                <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title">Add Staff Salary</h5>
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-sm-6">
                                    <div class="input-block mb-3">
                                        <label class="col-form-label">Select Staff</label>
                                        <asp:DropDownList ID="ddlempname" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <label class="col-form-label">Amount</label>
                                    <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>                               
                            </div>                            
                            <div class="submit-section">
                                <button class="btn btn-primary submit-btn">Submit</button>
                                <asp:Button ID="btnaddamount" runat="server" Text="Submit" CssClass="btn btn-primary submit-btn" OnClick="btnaddamount_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- /Add Salary Modal -->

            <!-- Edit Salary Modal -->
            <div id="edit_salary" class="modal custom-modal fade" role="dialog">
                <div class="modal-dialog modal-dialog-centered modal-md" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title">Edit Staff Salary</h5>
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <asp:HiddenField ID="HiddenField1" runat="server" />
                                <asp:HiddenField ID="HiddenField2" runat="server" />
                                <asp:HiddenField ID="HiddenField3" runat="server" />
                                <div class="col-sm-6">
                                    <div class="input-block mb-3">
                                        <label class="col-form-label">Name</label>
                                        <asp:TextBox ID="txtnameedit" runat="server" CssClass="form-control" AutoCompleteType="Disabled"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <label class="col-form-label">Month Salary</label>
                                    <asp:TextBox ID="txtmonthsalary" runat="server" CssClass="form-control" AutoCompleteType="Disabled"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-6">
                                    <div class="input-block mb-3">
                                        <label class="col-form-label">Gross Salary</label>
                                        <asp:TextBox ID="txtgrosssalary" runat="server" CssClass="form-control" AutoCompleteType="Disabled"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <label class="col-form-label">Net Salary</label>
                                    <asp:TextBox ID="txtnetsalary" runat="server" CssClass="form-control" AutoCompleteType="Disabled"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-6">
                                    <h4 class="text-primary">Earnings</h4>
                                    <div class="input-block mb-3">
                                        <label class="col-form-label">Basic</label>
                                        <asp:TextBox ID="txtbasicsalary" runat="server" CssClass="form-control" AutoCompleteType="Disabled"></asp:TextBox>
                                    </div>
                                    <div class="input-block mb-3">
                                        <label class="col-form-label">HRA</label>
                                        <asp:TextBox ID="txthra" runat="server" CssClass="form-control" AutoCompleteType="Disabled"></asp:TextBox>
                                    </div>
                                    <div class="input-block mb-3">
                                        <label class="col-form-label">EA</label>
                                        <asp:TextBox ID="txtea" runat="server" CssClass="form-control" AutoCompleteType="Disabled"></asp:TextBox>
                                    </div>
                                    <div class="input-block mb-3">
                                        <label class="col-form-label">DA</label>
                                        <asp:TextBox ID="txtda" runat="server" CssClass="form-control" AutoCompleteType="Disabled"></asp:TextBox>
                                    </div>

                                    <div class="input-block mb-3">
                                        <label class="col-form-label">CA</label>
                                        <asp:TextBox ID="txtca" runat="server" CssClass="form-control" AutoCompleteType="Disabled"></asp:TextBox>
                                    </div>


                                </div>
                                <div class="col-sm-6">
                                    <h4 class="text-primary">Deductions</h4>
                                    <div class="input-block mb-3">
                                        <label class="col-form-label">PF</label>
                                        <asp:TextBox ID="txtpf" runat="server" CssClass="form-control" AutoCompleteType="Disabled"></asp:TextBox>
                                    </div>
                                    <div class="input-block mb-3">
                                        <label class="col-form-label">ESI</label>
                                        <asp:TextBox ID="txtesi" runat="server" CssClass="form-control" AutoCompleteType="Disabled"></asp:TextBox>
                                    </div>
                                    <div class="input-block mb-3">
                                        <label class="col-form-label">TDS</label>
                                        <asp:TextBox ID="txttds" runat="server" CssClass="form-control" AutoCompleteType="Disabled"></asp:TextBox>
                                    </div>
                                    <div class="input-block mb-3">
                                        <label class="col-form-label">Leave</label>
                                        <asp:TextBox ID="txtleave" runat="server" CssClass="form-control" AutoCompleteType="Disabled"></asp:TextBox>
                                    </div>
                                    <div class="input-block mb-3">
                                        <label class="col-form-label">Prof. Tax</label>
                                        <asp:TextBox ID="txtpt" runat="server" CssClass="form-control" AutoCompleteType="Disabled"></asp:TextBox>
                                    </div>

                                </div>
                            </div>
                            <div class="submit-section">
                                <asp:Button ID="btnupdatesalary" runat="server" Text="Update" CssClass="btn btn-primary submit-btn" OnClick="btnupdatesalary_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- /Edit Salary Modal -->

            <!-- Delete Salary Modal -->
            <div class="modal custom-modal fade" id="delete_salary" role="dialog">
                <div class="modal-dialog modal-dialog-centered">
                    <div class="modal-content">
                        <div class="modal-body">
                            <div class="form-header">
                                <h3>Delete Salary</h3>
                                <p>Are you sure want to delete?</p>
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
            <!-- /Delete Salary Modal -->

        </div>
        <!-- /Page Wrapper -->
    </div>
    <!-- /Main Wrapper -->


    <!-- Add Payslip Popup start  -->

    <div id="add_salary1" runat="server" class="modal custom-modal fade row print" role="dialog">
        <div class="col-md-12">
            <div class="card">
                <div class="card-body">
                    <h4 class="payslip-title">Payslip for the month of
                        <asp:Label ID="lblpayslipmonthyear" runat="server" ForeColor="red"></asp:Label></h4>
                    <div class="row">
                        <div class="col-sm-6 m-b-20">
                            <img src="assets/img/logo2.png" class="inv-logo" alt="Logo">
                            <ul class="list-unstyled mb-0">
                                <li>Dreamguy's Technologies</li>
                                <li>3864 Quiet Valley Lane,</li>
                                <li>Sherman Oaks, CA, 91403</li>
                            </ul>
                        </div>
                        <div class="col-sm-6 m-b-20">
                            <div class="invoice-details">
                                <h3 class="text-uppercase">Id
                                    <asp:Label ID="lblpayslipid" runat="server" ForeColor="red"></asp:Label></h3>
                                <ul class="list-unstyled">
                                    <li>Salary Month: <span>
                                        <asp:Label ID="lblpayslipmonth" runat="server" ForeColor="red"></asp:Label></span></li>
                                </ul>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-12 m-b-20">
                            <ul class="list-unstyled">
                                <li>
                                    <h5 class="mb-0"><strong>
                                        <asp:Label ID="lblpayslipempname" runat="server" ForeColor="red"></asp:Label></strong></h5>
                                </li>
                                <li><span>
                                    <asp:Label ID="lblpayslipdesignation" runat="server" ForeColor="red"></asp:Label></span></li>
                                <li>Employee ID:
                                    <asp:Label ID="lblpayslipempid" runat="server" ForeColor="red"></asp:Label></li>
                                <li>Joining Date:
                                    <asp:Label ID="lblpayslipdoj" runat="server" ForeColor="red"></asp:Label></li>
                            </ul>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-sm-6">
                            <div>
                                <div style="display: flex; justify-content: space-between; align-items: center; width: 100%;">
                                    <h4 class="m-b-10"><strong>Earnings</strong></h4>
                                    <h4 class="m-b-10"><strong>Rs </strong></h4>
                                </div>
                                <table class="table table-bordered">
                                    <tbody>
                                        <tr>
                                            <td><strong>Basic Salary</strong> <span class="float-end">
                                                <asp:Label ID="lblpayslipbasicSalary" runat="server" ForeColor="green"></asp:Label></span></td>
                                        </tr>
                                        <tr>
                                            <td><strong>House Rent Allowance (H.R.A.)</strong> <span class="float-end">
                                                <asp:Label ID="lblpaysliphra" runat="server" ForeColor="red"></asp:Label></span></td>
                                        </tr>
                                        <tr>
                                            <td><strong>Conveyance (C.A)</strong> <span class="float-end">
                                                <asp:Label ID="lblpayslipca" runat="server" ForeColor="red"></asp:Label></span></td>
                                        </tr>
                                        <tr>
                                            <td><strong>E.A</strong> <span class="float-end">
                                                <asp:Label ID="lblpayslipother" runat="server" ForeColor="red"></asp:Label></span></td>
                                        </tr>
                                        <tr>
                                            <td><strong>S.A</strong> <span class="float-end">
                                                <asp:Label ID="lblpayslipsa" runat="server" ForeColor="red"></asp:Label></span></td>
                                        </tr>
                                        <tr>
                                            <td><strong>Total Earnings</strong> <span class="float-end"><strong>
                                                <asp:Label ID="lbltotalearning" runat="server" ForeColor="red"></asp:Label></strong></span></td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div>
                                <div style="display: flex; justify-content: space-between; align-items: center; width: 100%;">
                                    <h4 class="m-b-10"><strong>Deductions</strong></h4>
                                    <h4 class="m-b-10"><strong>Rs </strong></h4>
                                </div>
                                <table class="table table-bordered">
                                    <tbody>
                                        <tr>
                                            <td><strong>Tax Deducted at Source (T.D.S.)</strong> <span class="float-end">
                                                <asp:Label ID="lblpaysliptds" runat="server" ForeColor="red"></asp:Label></span></td>
                                        </tr>
                                        <tr>
                                            <td><strong>Provident Fund(PF)</strong> <span class="float-end">
                                                <asp:Label ID="lblpf" runat="server" ForeColor="red"></asp:Label></span></td>
                                        </tr>
                                        <tr>
                                            <td><strong>ESI</strong> <span class="float-end">
                                                <asp:Label ID="lblesi" runat="server" ForeColor="red"></asp:Label></span></td>
                                        </tr>
                                        <tr>
                                            <td><strong>ProfessionalTax</strong> <span class="float-end">
                                                <asp:Label ID="lblpayslipprofessonaltax" runat="server" ForeColor="red"></asp:Label></span></td>
                                        </tr>
                                        <tr>
                                            <td><strong>LateLogIn</strong> <span class="float-end">
                                                <asp:Label ID="lbllatelogindeduction" runat="server" ForeColor="red"></asp:Label></span></td>
                                        </tr>
                                        <tr>
                                            <td><strong>Total Deductions</strong> <span class="float-end"><strong>
                                                <asp:Label ID="lbltotaldeductions" runat="server" ForeColor="red"></asp:Label></strong></span></td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                        <div class="col-sm-12">
                            <div style="display: flex; justify-content: space-between; align-items: center; width: 100%;">
                                <p>
                                    <strong>Net Salary Rs :
                                    <asp:Label ID="lbnetSalary" runat="server" ForeColor="red"></asp:Label>
                                        /-</strong>
                                </p>
                                <p>
                                    <strong>
                                        <asp:Label ID="lblSalaryword" runat="server" ForeColor="red"></asp:Label></strong>
                                </p>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <!-- Payslip Popup End -->

    <script type="text/javascript">
        function viewSalary(Name, EmployeeId, Year, Month, ActualSalaryPerMonth, grssal, netsal, bascsalary, hra, ca, ea, da, tds, pf, esi, pftx, OnlyLOPDeduction) {
            $('#<%= txtnameedit.ClientID %>').val(Name).prop('readonly', true);
            $('#<%= HiddenField1.ClientID %>').val(EmployeeId);
            $('#<%= HiddenField1.ClientID %>').val(Year);
            $('#<%= HiddenField1.ClientID %>').val(Month);
            $('#<%= txtmonthsalary.ClientID %>').val(ActualSalaryPerMonth);
            $('#<%= txtgrosssalary.ClientID %>').val(grssal);
            $('#<%= txtnetsalary.ClientID %>').val(netsal);
            $('#<%= txtbasicsalary.ClientID %>').val(bascsalary);
            $('#<%= txthra.ClientID %>').val(hra);
            $('#<%= txtea.ClientID %>').val(ea);
            $('#<%= txtda.ClientID %>').val(da);
            $('#<%= txtca.ClientID %>').val(ca);
            $('#<%= txttds.ClientID %>').val(tds);
            $('#<%= txtpf.ClientID %>').val(pf);
            $('#<%= txtesi.ClientID %>').val(esi);
            $('#<%= txtpt.ClientID %>').val(pftx);
            $('#<%= txtleave.ClientID %>').val(OnlyLOPDeduction);
        }
    </script>
</asp:Content>
