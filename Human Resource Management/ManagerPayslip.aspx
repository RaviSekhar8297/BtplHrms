<%@ Page Title="" Language="C#" MasterPageFile="~/Manager.Master" AutoEventWireup="true" CodeBehind="ManagerPayslip.aspx.cs" Inherits="Human_Resource_Management.ManagerPayslip" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Page Wrapper -->
    <div class="page-wrapper">

        <!-- Page Content -->
        <div class="content container-fluid">

            <!-- Page Header -->
            <div class="page-header">
                <div class="row align-items-center">
                    <div class="col">
                        <h3 class="page-title">Payslip</h3>
                        <ul class="breadcrumb">
                            <li class="breadcrumb-item"><a href="EmployeeDashboard.aspx">Dashboard</a></li>
                            <li class="breadcrumb-item active">Payslip</li>
                        </ul>
                    </div>
                    <div class="col-auto float-end ms-auto">
                        <div class="btn-group btn-group-sm">
                            <asp:Button ID="btnPrint" runat="server" Text="Print Payslip" OnClientClick="printDiv('payslipiddiv'); return false;" />

                        </div>
                    </div>
                </div>
            </div>
            <!-- /Page Header -->
            <div class="row filter-row">
                <div class="col-sm-6 col-md-3">
                    <div class="input-block mb-3 form-focus select-focus">
                        <asp:TextBox ID="txtmonth" runat="server" CssClass="form-control" AutoPostBack="true" TextMode="Month" OnTextChanged="txtmonth_TextChanged"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="row print" id="payslipiddiv" runat="server">
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
        </div>
        <!-- /Page Content -->
        <asp:HiddenField ID="hfPayslipHtml" runat="server" />
        <asp:Literal ID="litPayslipHtml" runat="server" Visible="false" />
    </div>
    <!-- /Page Wrapper -->
    <script type="text/javascript">

        function onCalendarShown() {

            var cal = $find("calendar1");
            //Setting the default mode to month
            cal._switchMode("months", true);

            //Iterate every month Item and attach click event to it
            if (cal._monthsBody) {
                for (var i = 0; i < cal._monthsBody.rows.length; i++) {
                    var row = cal._monthsBody.rows[i];
                    for (var j = 0; j < row.cells.length; j++) {
                        Sys.UI.DomEvent.addHandler(row.cells[j].firstChild, "click", call);
                    }
                }
            }
        }
        function onCalendarHidden() {
            var cal = $find("calendar1");
            //Iterate every month Item and remove click event from it
            if (cal._monthsBody) {
                for (var i = 0; i < cal._monthsBody.rows.length; i++) {
                    var row = cal._monthsBody.rows[i];
                    for (var j = 0; j < row.cells.length; j++) {
                        Sys.UI.DomEvent.removeHandler(row.cells[j].firstChild, "click", call);
                    }
                }
            }

        }

        function call(eventElement) {
            var target = eventElement.target;
            switch (target.mode) {
                case "month":
                    var cal = $find("calendar1");
                    cal._visibleDate = target.date;
                    cal.set_selectedDate(target.date);
                    cal._switchMonth(target.date);
                    cal._blur.post(true);
                    cal.raiseDateSelectionChanged();
                    break;
            }
        }

        function printDiv() {
            // Get the content of the payslipiddiv
            var payslipContent = document.getElementById("payslipiddiv").innerHTML;

            // Create a new window
            var printWindow = window.open('', '', 'height=400,width=800');

            // Write the HTML content into the new window
            printWindow.document.write('<html><head><title>Print</title></head><body>');
            printWindow.document.write(payslipContent);
            printWindow.document.write('</body></html>');

            // Close the document and call print
            printWindow.document.close();
            printWindow.print();
        }

        // row wise print all data

        function printDiv() {
            var divToPrint = document.getElementsByClassName('print')[0];
            var anotherWindow = window.open('', 'Print-Window');
            anotherWindow.document.open();
            anotherWindow.document.write('<html><body onload="window.print()">' + divToPrint.innerHTML + '</body></html>');
            anotherWindow.document.close();
            setTimeout(function () {
                anotherWindow.close();
            }, 10);
        }


    </script>
</asp:Content>
