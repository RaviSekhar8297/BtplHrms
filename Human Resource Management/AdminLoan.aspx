<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="AdminLoan.aspx.cs" Inherits="Human_Resource_Management.AdminLoan" %>

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
                            <h3 class="page-title">Loan Data</h3>
                            <ul class="breadcrumb">
                                <li class="breadcrumb-item"><a href="Admindashboard.aspx">Dashboard</a></li>
                                <li class="breadcrumb-item active">Loan Data</li>
                            </ul>
                        </div>
                        <div class="col-auto float-end ms-auto">
                            <a href="#" class="btn add-btn"><i class="la la-calculator"></i>Apply Loan</a>
                        </div>
                    </div>
                </div>

                <!-- /Page Header -->


                <!-- Page Tab -->
                <div class="page-menu">
                    <div class="row">
                        <div class="col-sm-12">
                            <ul class="nav nav-tabs nav-tabs-bottom">
                                <li class="nav-item">
                                    <a class="nav-link active" data-bs-toggle="tab" href="#tab_pendingloan">Pending</a>
                                </li>
                                <li class="nav-item">
                                    <a class="nav-link" data-bs-toggle="tab" href="#tab_approvedloan">Approved</a>
                                </li>
                                <li class="nav-item">
                                    <a class="nav-link" data-bs-toggle="tab" href="#tab_rejectedloan">Rejected</a>
                                </li>
                                <li class="nav-item">
                                    <a class="nav-link" data-bs-toggle="tab" href="#tab_loanhistory">History</a>
                                </li>
                                <li class="nav-item">
                                    <a class="nav-link" data-bs-toggle="tab" href="#tab_payloan">Pay</a>
                                </li>
                            </ul>
                        </div>
                    </div>
                </div>
                <!-- /Page Tab -->


                <!-- Tab Content -->
                <div class="tab-content">

                    <!-- Pending Tab -->
                    <div class="tab-pane show active" id="tab_pendingloan">
                        <div class="payroll-table card">
                            <div class="table-responsive">
                                <table class="table table-hover table-radius">
                                    <thead>
                                        <tr>
                                            <th>Name</th>
                                            <th>EmpId</th>
                                            <th>Amount</th>
                                            <th>Applied Date</th>
                                            <th>Approve/Rejected</th>
                                            <th>Loan Status</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:PlaceHolder ID="PendingLoans" runat="server"></asp:PlaceHolder>

                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                    <!-- Pending Tab End-->

                    <!-- Approved Tab -->
                    <div class="tab-pane" id="tab_approvedloan">

                        <div class="payroll-table card">
                            <div class="table-responsive">
                                <table class="table table-hover table-radius">
                                    <thead>
                                        <tr>

                                            <th>Name</th>
                                            <th>EmpId</th>
                                            <th>Loan Amount</th>
                                            <th>Applied Date </th>
                                            <th>Approved Date</th>
                                            <th>Approved By</th>
                                            <th>Status</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:PlaceHolder ID="ApprovedLoans" runat="server"></asp:PlaceHolder>

                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                    <!-- Approved Tab End-->


                    <!-- Rejected Tab -->
                    <div class="tab-pane" id="tab_rejectedloan">
                        <div class="payroll-table card">
                            <div class="table-responsive">
                                <table class="table table-hover table-radius">
                                    <thead>
                                        <tr>

                                            <th>Name</th>
                                            <th>EmpId</th>
                                            <th>Loan Amount</th>
                                            <th>Applied Date </th>
                                            <th>Rejected Date</th>
                                            <th>Rejected By</th>
                                            <th>Status</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:PlaceHolder ID="RejectedLoans" runat="server"></asp:PlaceHolder>

                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                    <!-- Rejected Tab End-->

                    <!-- History Tab -->
                    <div class="tab-pane" id="tab_loanhistory">
                        <div class="payroll-table card">
                            <div class="table-responsive">
                                <table class="table table-hover table-radius">
                                    <thead>
                                        <tr>

                                            <th>Name</th>
                                            <th>EmpId</th>
                                            <th>Amount</th>
                                            <th>Clear</th>
                                            <th>Due</th>
                                            <th>Pay Amount</th>
                                            <th>Payment Date</th>
                                            <th>Status</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:PlaceHolder ID="LoanHistory" runat="server"></asp:PlaceHolder>

                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                    <!-- History Tab End-->


                    <!-- Pay Loan Tab -->
                    <div class="tab-pane" id="tab_payloan">
                        <div class="payroll-table card">
                            <div class="table-responsive">
                                <table class="table table-hover table-radius">
                                    <thead>
                                        <tr>
                                            <th>Name</th>
                                            <th>Id</th>
                                            <th>Loan</th>
                                            <th>ApplyDate</th>
                                            <th>Clear</th>
                                            <th>Due</th>
                                            <th>LoanStatus</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:PlaceHolder ID="PayLoan" runat="server"></asp:PlaceHolder>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                    <!-- Pay Loan Tab End-->


                </div>


                <!-- Pay Loan Amount Modal -->
                <div id="edit_payLoanAmount" class="modal custom-modal fade" role="dialog">
                    <div class="modal-dialog modal-dialog-centered" role="document">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h5 class="modal-title">Loan Details</h5>
                                <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                            </div>
                            <div class="modal-body">
                                <div class="input-block mb-3">
                                    <asp:HiddenField ID="HiddenField1" runat="server" />
                                    <asp:HiddenField ID="HiddenField2" runat="server" />
                                    <asp:Label ID="Label1" runat="server" Text="" ForeColor="Red"></asp:Label>
                                    <asp:Label ID="Label2" runat="server" Text="" ForeColor="Red"></asp:Label>
                                </div>

                                <div class="input-block mb-3">
                                    <label class="col-form-label">Name <span class="text-danger">*</span></label>
                                    <asp:TextBox ID="txtdeptname" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="input-block mb-3">
                                    <label class="col-form-label">Loan Amount <span class="text-danger">*</span></label>
                                    <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control" oninput="validateTextBox()"></asp:TextBox>
                                </div>
                                <div class="input-block mb-3">
                                    <label class="col-form-label">Cleared Amount <span class="text-danger">*</span></label>
                                    <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="input-block mb-3">
                                    <label class="col-form-label">This month Your net salary <span class="text-danger">*</span></label>
                                    <asp:TextBox ID="TextBox4" runat="server" CssClass="form-control" oninput="validateTextBox()"></asp:TextBox>
                                </div>
                                <div class="input-block mb-3">
                                    <label class="col-form-label">Due amount <span class="text-danger">*</span></label>
                                    <asp:TextBox ID="TextBox3" runat="server" CssClass="form-control" oninput="validateTextBox()"></asp:TextBox>
                                </div>
                                <div class="input-block mb-3">
                                    <label class="col-form-label">Pay amount <span class="text-danger">*</span></label>
                                    <asp:TextBox ID="TextBox5" runat="server" CssClass="form-control" oninput="validateTextBox()"></asp:TextBox>
                                </div>
                                <div id="error-message" style="color: red;"></div>
                                <div class="submit-section">
                                    <asp:Button ID="btnpayamount" runat="server" Text="Pay" CssClass="btn btn-primary submit-btn" OnClick="btnpayamount_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- /Pay Loan Amount Modal -->

                <!-- approve or reject Loan Modal -->
                <div id="edit_pendingloan" class="modal custom-modal fade" role="dialog">
                    <div class="modal-dialog modal-dialog-centered" role="document">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h5 class="modal-title">Loam Approvel Form</h5>
                                <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                            </div>
                            <div class="modal-body">
                                <asp:HiddenField ID="HiddenField3id" runat="server" />
                                <asp:HiddenField ID="HiddenField4empid" runat="server" />
                                <div class="input-block mb-3">
                                    <label class="col-form-label">EmpId <span class="text-danger">*</span></label>
                                    <asp:TextBox ID="txtempid" runat="server" CssClass="form-control" AutoCompleteType="Disabled"></asp:TextBox>
                                </div>
                                <div class="input-block mb-3">
                                    <label class="col-form-label">Name<span class="text-danger">*</span></label>
                                    <asp:TextBox ID="txtname" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="input-block mb-3">
                                    <label class="col-form-label">Loan Amount <span class="text-danger">*</span></label>
                                    <asp:TextBox ID="txtloanamount" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="input-block mb-3">
                                    <label class="col-form-label">Applied Date <span class="text-danger">*</span></label>
                                    <asp:TextBox ID="txtapplieddate" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                </div>
                                <div class="input-block mb-3">
                                    <label class="col-form-label">Reason<span class="text-danger">*</span></label>
                                    <asp:TextBox ID="txtreason" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                </div>
                                <div class="submit-section">
                                    <asp:Button ID="btnloanapprove" runat="server" Text="Approve" CssClass="btn btn-primary submit-btn" OnClick="btnloanapprove_Click" />
                                    <asp:Button ID="btnloanreject" runat="server" Text="Reject" CssClass="btn btn-danger submit-btn" OnClick="btnloanreject_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- /approve or reject Loan Modal -->
            </div>
        </div>
    </div>
    <script type="text/javascript">
        function payLoanAmount(EmpId, Name, LoanAmount, ClearAmount, BalenceAmount, netSalary, Id) {
            $('#<%= HiddenField1.ClientID %>').val(EmpId);
            $('#<%= txtdeptname.ClientID %>').val(Name);
            $('#<%= TextBox1.ClientID %>').val(LoanAmount);
            $('#<%= TextBox2.ClientID %>').val(ClearAmount);
            $('#<%= TextBox3.ClientID %>').val(BalenceAmount);
            $('#<%= TextBox4.ClientID %>').val(netSalary);
            $('#<%= HiddenField2.ClientID %>').val(Id);

        }
        function editPendingLoans(Id, EmpId, Name, LoanAmount, LoanApplyDate, Reason) {
            $('#<%= HiddenField3id.ClientID %>').val(Id);
            $('#<%= HiddenField4empid.ClientID %>').val(EmpId);
            $('#<%= txtname.ClientID %>').val(Name);
            $('#<%= txtloanamount.ClientID %>').val(LoanAmount);
            $('#<%= txtapplieddate.ClientID %>').val(LoanApplyDate);
            $('#<%= txtreason.ClientID %>').val(Reason);
            $('#<%= txtempid.ClientID %>').val(EmpId);
        }
    </script>
    <script type="text/javascript">
        function validateTextBox() {
            var textBox1 = document.getElementById('<%= TextBox4.ClientID %>');
           var textBox3 = document.getElementById('<%= TextBox5.ClientID %>');
           var errorMessage = document.getElementById('error-message');
           var btnPayAmount = document.getElementById('<%= btnpayamount.ClientID %>');

            var value1 = parseFloat(textBox1.value);
            var value3 = parseFloat(textBox3.value);

            // If invalid number, show error and hide the button
            if (isNaN(value1) || isNaN(value3)) {
                errorMessage.innerText = "Please enter valid numbers.";
                errorMessage.style.color = "red";
                btnPayAmount.style.display = "none";  // Hide button
                return false;
            }

            // If value3 is greater than value1, show error and hide the button
            if (value3 > value1) {
                errorMessage.innerText = "Enter a value less than or equal to " + value1 + ".";
                errorMessage.style.color = "red";
                btnPayAmount.style.display = "none";  // Hide button
                return false;
            } else {
                errorMessage.innerText = "";  // Clear error message
                btnPayAmount.style.display = "inline-block";  // Show button
                return true;
            }
        }
    </script>

</asp:Content>
