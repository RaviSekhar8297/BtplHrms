<%@ Page Title="" Language="C#" MasterPageFile="~/Employee.Master" AutoEventWireup="true" CodeBehind="EmployeeLoan.aspx.cs" Inherits="Human_Resource_Management.EmployeeLoan" %>

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
                            <h3 class="page-title">Payroll Items</h3>
                            <ul class="breadcrumb">
                                <li class="breadcrumb-item"><a href="Admindashboard">Loan</a></li>
                                <li class="breadcrumb-item active">Payroll Items</li>
                            </ul>
                        </div>
                        <div class="col-auto float-end ms-auto">
                            <a href="#" class="btn add-btn" data-bs-toggle="modal" data-bs-target="#add_applyloan"><i class="fa-solid fa-plus"></i>Apply Loan</a>
                            <div class="view-icons">
                            </div>
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
                                    <a class="nav-link active" data-bs-toggle="tab" href="#tab_loandata">Loans</a>
                                </li>
                                <li class="nav-item">
                                    <a class="nav-link" data-bs-toggle="tab" href="#tab_loanhistory">History</a>
                                </li>
                            </ul>
                        </div>
                    </div>
                </div>
                <!-- /Page Tab -->

                <!-- Tab Content -->
                <div class="tab-content">

                    <!-- Additions Tab -->
                    <div class="tab-pane show active" id="tab_loandata">

                        <!-- Add Addition Button
                     <div class="text-end mb-4 clearfix">
                         <button class="btn btn-primary add-btn" type="button" data-bs-toggle="modal" data-bs-target="#add_addition"><i class="fa-solid fa-plus"></i>Add Addition</button>
                     </div> -->


                        <!-- Loan Data Table -->
                        <div class="payroll-table card">
                            <div class="table-responsive">
                                <table class="table table-hover table-radius">
                                    <thead>
                                        <tr>
                                            <th>S.No</th>                                            
                                            <th>Name</th>   
                                             <th>EmpId</th>
                                            <th>Amount</th>
                                            <th>Applied Date</th>
                                            <th>Approve/Rejected</th>
                                             <th>Loan Status</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:PlaceHolder ID="LoanList" runat="server"></asp:PlaceHolder>

                                    </tbody>
                                </table>
                            </div>
                        </div>
                        <!-- Loan Data Table -->

                    </div>
                    <!-- Additions Tab -->

                    <!-- loan history Tab -->
                    <div class="tab-pane" id="tab_loanhistory">

                        <div class="payroll-table card">
                            <div class="table-responsive">
                                <table class="table table-hover table-radius">
                                    <thead>
                                        <tr>
                                            <th>S.No</th>
                                            <th>Name</th>
                                            <th>Loan Amount</th>
                                            <th>Clear </th>
                                            <th>Due Amount</th>
                                            <th>Pay Amount</th>
                                            <th>Payment Date </th>
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
                    <!-- loan history Tab -->
                </div>

            </div>
            <!-- /Page Content -->




            <!-- Apply Loan Modal -->
            <div id="add_applyloan" class="modal custom-modal fade" role="dialog">
                <div class="modal-dialog modal-dialog-centered" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title">Loan Apply Form</h5>
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="modal-body">
                            <div class="input-block mb-3">
                                <label class="col-form-label">Loan Amount<span class="text-danger">*</span></label>
                                <asp:TextBox ID="txtloan" runat="server" CssClass="form-control" AutoCompleteType="Disabled" onkeypress="return isNumberKey(event)" oninput="formatNumber(this); toggleButtonVisibility()"></asp:TextBox>
                            </div>
                            <div class="input-block mb-3">
                                <label class="col-form-label">Reason<span class="text-danger">*</span></label>
                                <asp:TextBox ID="txtreason" runat="server" CssClass="form-control" TextMode="MultiLine" AutoCompleteType="Disabled" oninput="toggleButtonVisibility()"></asp:TextBox>
                            </div>
                            <div class="submit-section">
                                <asp:Button ID="btnapplyloan" runat="server" Text="Submit" CssClass="btn btn-danger submit-btn" OnClick="btnapplyloan_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- /Apply Loan Modal -->



            <!-- Apply Loan Modal -->
            <div id="edit_loanamount" class="modal custom-modal fade" role="dialog">
                <div class="modal-dialog modal-dialog-centered" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title">Loan Apply Form</h5>
                            <asp:HiddenField ID="hdfediloanamountid" runat="server" />
                            <asp:HiddenField ID="hdfediloanamountempid" runat="server" />
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="modal-body">
                            <div class="input-block mb-3">
                                <label class="col-form-label">Loan Amount<span class="text-danger">*</span></label>
                                <asp:TextBox ID="txteditloanamount" runat="server" CssClass="form-control" AutoCompleteType="Disabled" onkeypress="return isNumberKey(event)" oninput="formatNumber(this); toggleButtonVisibility()"></asp:TextBox>
                            </div>
                            <div class="submit-section">
                                <asp:Button ID="btneditloanamount" runat="server" Text="Update" CssClass="btn btn-danger submit-btn" OnClick="btneditloanamount_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- /Apply Loan Modal -->

            <!-- Delete Loan List Modal -->
            <div class="modal custom-modal fade" id="delete_loanamoun" role="dialog">
                <div class="modal-dialog modal-dialog-centered">
                    <div class="modal-content">
                        <div class="modal-body">
                            <div class="form-header">
                                <h3>Delete Department</h3>
                                <p>
                                    Confirm delete to
                                      <asp:Label ID="Label2" runat="server" ForeColor="#cc0000"></asp:Label>
                                    Loan Amoun ?
                                </p>
                              
                            </div>
                            <div class="modal-btn delete-action">
                                <div class="row">
                                    <div class="col-6">
                                        <asp:Button ID="btndeleteloanamount" runat="server" Text="Delete" CssClass="btn btn-primary continue-btn" OnClick="btndeleteloanamount_Click" />
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
            <!-- /Delete Loan List Modal -->

        </div>
    </div>
    <script type="text/javascript">
        function formatNumber(input) {
            let value = input.value.replace(/,/g, '');

            // Validate the number range
            if (value !== "" && (Number(value) < 1 || Number(value) > 10000000)) {
                alert("Please enter a value between 1 and 1 crore.");
                input.value = '';
                document.getElementById("words").innerText = ''; // Clear words display
                toggleButtonVisibility();
                return;
            }

            value = Number(value);

            if (!isNaN(value)) {
                input.value = indianNumberFormat(value);

            }

            toggleButtonVisibility();
        }

        function indianNumberFormat(num) {
            let numStr = num.toString();
            let lastThreeDigits = numStr.substring(numStr.length - 3);
            let otherDigits = numStr.substring(0, numStr.length - 3);

            if (otherDigits) {
                lastThreeDigits = ',' + lastThreeDigits;
            }

            let formattedNumber = otherDigits.replace(/\B(?=(\d{2})+(?!\d))/g, ",") + lastThreeDigits;
            return formattedNumber;
        }




        function toggleButtonVisibility() {
            var loanAmount = document.getElementById('<%= txtloan.ClientID %>').value;
            var reason = document.getElementById('<%= txtreason.ClientID %>').value;
            var submitButton = document.getElementById('<%= btnapplyloan.ClientID %>');

            if (loanAmount.trim() === "" || reason.trim() === "") {
                submitButton.style.display = "none"; // Hide the submit button
            } else {
                submitButton.style.display = "block"; // Show the submit button
            }
        }

        window.onload = function () {
            toggleButtonVisibility();
        };



        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }
    </script>




    <script type="text/javascript">
        function editLoanAmount(Id, EmpId, LoanAmount) {
            $('#<%= txteditloanamount.ClientID %>').val(LoanAmount);
            $('#<%= hdfediloanamountid.ClientID %>').val(Id);
            $('#<%= hdfediloanamountempid.ClientID %>').val(EmpId);
        }
        function deleteLoanList(Id, EmpId, LoanAmount)
        {
            document.getElementById('<%= Label2.ClientID %>').textContent = LoanAmount;
            $('#<%= hdfediloanamountid.ClientID %>').val(Id);
            $('#<%= hdfediloanamountempid.ClientID %>').val(EmpId);
        }
    </script>


</asp:Content>
