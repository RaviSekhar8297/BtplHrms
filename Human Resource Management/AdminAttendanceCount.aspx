<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="AdminAttendanceCount.aspx.cs" Inherits="Human_Resource_Management.AdminSalarySheet" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0-beta3/css/all.min.css">

    <style>
        .fixed-header-table thead th {
            position: sticky;
            top: 0;
            z-index: 100;
            background-color: #fff; /* Set this to the background color of your table header */
            box-shadow: 0 2px 2px -1px rgba(0, 0, 0, 0.4);
        }

        .button {
            float: right;
        }
    </style>
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
                            <h3 class="page-title">Attendance Count</h3>
                            <ul class="breadcrumb">
                                <li class="breadcrumb-item"><a href="Admindashboard.aspx">Dashboard</a></li>
                                <li class="breadcrumb-item active">Attendance Count</li>
                            </ul>
                        </div>

                        <div class="col-auto float-end ms-auto">
                            <a href="AdminGeneratePayroll.aspx" class="btn add-btn"><i class="la la-calculator"></i>Generate Payroll</a>
                        </div>


                    </div>
                </div>

                <!-- /Page Header -->
                <div class="row">
                    <div class="col-md-4">
                        <div class="input-block mb-3 form-focus select-focus">
                            <asp:TextBox ID="txtempnamesearch" runat="server" CssClass="form-control floating" placeholder="Search By Name" AutoCompleteType="Disabled" onkeypress="return isCharacterKey(event)"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="input-block mb-3 form-focus select-focus">
                            <asp:TextBox ID="txtmonth" runat="server" CssClass="form-control floating" AutoCompleteType="Disabled" TextMode="Month" AutoPostBack="true" OnTextChanged="txtmonth_TextChanged"></asp:TextBox>
                        </div>
                    </div>

                    <div class="col-sm-6 col-md-4">
                        <div class="input-block mb-3 form-focus select-focus">
                            <asp:Button ID="Button1" runat="server" Text="Excel" OnClick="Button1_Click" CssClass="button" />
                        </div>
                    </div>
                </div>
                <!-- /Leave Statistics -->
                <div class="row">
                    <div class="col-md-12">
                        <div class="table-responsive fixed-header-table" style="max-height: 500px; overflow-y: auto;">
                            <table class="table table-striped custom-table leave-employee-table mb-0 datatable">
                                <thead>
                                    <tr>
                                        <th>Name</th>
                                        <th>Id</th>
                                        <th>Total</th>
                                        <th>Work</th>
                                        <th>W.O</th>
                                        <th>HoliDays</th>
                                        <th>Presents</th>
                                        <th>Abs</th>
                                        <th>HalfDays</th>
                                        <th>Late</th>
                                        <th>LOPs</th>
                                        <th>CL</th>
                                        <th>SL</th>
                                        <th>Comp</th>
                                        <th>Payble</th>
                                        <th>Month</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:PlaceHolder ID="AttendanceCount" runat="server"></asp:PlaceHolder>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <!-- Edit -->
                    <div id="edit_attendancecount" class="modal custom-modal fade" role="dialog">
                        <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h5 class="modal-title">Edit Attendaance Count</h5>
                                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close">
                                        <span aria-hidden="true">&times;</span>
                                    </button>
                                </div>
                                <div class="modal-body">
                                    <asp:HiddenField ID="HiddenField1" runat="server" />
                                    <asp:HiddenField ID="HiddenField2" runat="server" />
                                    <asp:HiddenField ID="HiddenField3" runat="server" />
                                    <asp:HiddenField ID="HiddenField4" runat="server" />
                                    <asp:HiddenField ID="HiddenField5" runat="server" />
                                    <div class="row">
                                        <div class="col-sm-6">
                                            <div class="input-block mb-3">
                                                <label class="col-form-label">Name</label>
                                                <asp:TextBox ID="txtname" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-6">
                                            <div class="input-block mb-3">
                                                <label class="col-form-label">PaybleDays <span class="text-danger">*</span></label>
                                                <asp:TextBox ID="txtpaybledays" runat="server" CssClass="form-control" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-6">
                                            <div class="input-block mb-3">
                                                <label class="col-form-label">Presents <span class="text-danger">*</span></label>
                                                <asp:TextBox ID="txtpresents" runat="server" CssClass="form-control" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-6">
                                            <div class="input-block mb-3">
                                                <label class="col-form-label">Absents<span class="text-danger">*</span></label>
                                                <asp:TextBox ID="txtabsents" runat="server" CssClass="form-control" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-6">
                                            <div class="input-block mb-3">
                                                <label class="col-form-label">HalfDays <span class="text-danger">*</span></label>
                                                <asp:TextBox ID="txthalfdays" runat="server" CssClass="form-control" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-6">
                                            <div class="input-block mb-3">
                                                <label class="col-form-label">LateLogs <span class="text-danger">*</span></label>
                                                <asp:TextBox ID="txtlatelogs" runat="server" CssClass="form-control" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-6">
                                            <div class="input-block mb-3">
                                                <label class="col-form-label">LOPs <span class="text-danger">*</span></label>
                                                <asp:TextBox ID="txtlops" runat="server" CssClass="form-control" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-6">
                                            <div class="input-block mb-3">
                                                <label class="col-form-label">CL <span class="text-danger">*</span></label>
                                                <asp:TextBox ID="txtcls" runat="server" CssClass="form-control" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-6">
                                            <div class="input-block mb-3">
                                                <label class="col-form-label">SL <span class="text-danger">*</span></label>
                                                <asp:TextBox ID="txtsls" runat="server" CssClass="form-control" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-6">
                                            <div class="input-block mb-3">
                                                <label class="col-form-label">CompOffs <span class="text-danger">*</span></label>
                                                <asp:TextBox ID="txtcompoffs" runat="server" CssClass="form-control" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                            </div>
                                        </div>

                                    </div>
                                    <div class="table-responsive m-t-15">
                                    </div>
                                    <div class="submit-section">
                                        <asp:Button ID="btncountupdate" runat="server" Text="Update Count" CssClass="btn btn-primary submit-btn" OnClick="btncountupdate_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

            </div>
            <!-- /Page Content -->
        </div>
        <!-- /Page Wrapper -->

    </div>
    <!-- /Main Wrapper -->
    <script type="text/javascript">
        function editAttendanceCount(Name, Presents, Absents, HalfDays, LateLogs, LOPs, CL, SL, CompOffs, PaybleDays, EmpId, Year, Month, WeekOffs, HoliDays) {
            $('#<%= txtname.ClientID %>').val(Name).prop('readonly', true);
            $('#<%= txtpresents.ClientID %>').val(Presents);
            $('#<%= txtabsents.ClientID %>').val(Absents);
            $('#<%= txthalfdays.ClientID %>').val(HalfDays);
            $('#<%= txtlatelogs.ClientID %>').val(LateLogs);
            $('#<%= txtlops.ClientID %>').val(LOPs);
            $('#<%= txtcls.ClientID %>').val(CL);
            $('#<%= txtsls.ClientID %>').val(SL);
            $('#<%= txtcompoffs.ClientID %>').val(CompOffs);
            $('#<%= txtpaybledays.ClientID %>').val(PaybleDays).prop('readonly', true);
            $('#<%= HiddenField1.ClientID %>').val(EmpId);
            $('#<%= HiddenField2.ClientID %>').val(Year);
            $('#<%= HiddenField3.ClientID %>').val(Month);
            $('#<%= HiddenField4.ClientID %>').val(WeekOffs);
            $('#<%= HiddenField5.ClientID %>').val(HoliDays);
            console.log("Name is : " + Name);
        }


        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }
        function isCharacterKey(event) {
            var charCode = (event.which) ? event.which : event.keyCode;
            if ((charCode >= 65 && charCode <= 90) || (charCode >= 97 && charCode <= 122) || charCode == 32) {
                console.log("Valid input, charCode: " + charCode);
                return true;
            } else {
                console.log("Invalid input, charCode: " + charCode);
                return false;
            }
        }
    </script>


</asp:Content>
