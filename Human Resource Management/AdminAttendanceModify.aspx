<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="AdminAttendanceModify.aspx.cs" Inherits="Human_Resource_Management.AdminAttendanceModify" %>

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
                            <h3 class="page-title">Attendance Modification Data</h3>
                            <ul class="breadcrumb">
                                <li class="breadcrumb-item"><a href="Admindashboard.aspx">Dashboard</a></li>
                                <li class="breadcrumb-item active">Attendance Modification</li>
                            </ul>
                        </div>
                    </div>
                </div>

                <!-- /Page Header -->
                <div class="row filter-row">
                    <div class="col-sm-6 col-md-3">
                        <div class="input-block mb-3 form-focus select-focus">
                            <label class="focus-label">Employee Name</label>
                            <asp:TextBox ID="txtempnamesearch" runat="server" CssClass="form-control floating" onkeypress="return isCharacterKey(event)" placeholder="Search By Name" AutoCompleteType="Disabled"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-sm-6 col-md-3">
                        <div class="input-block mb-3 form-focus select-focus">
                            <label class="focus-label">Employee Name</label>
                            <asp:TextBox ID="txtdatesearch" runat="server" CssClass="form-control floating" TextMode="Date" AutoPostBack="true" AutoCompleteType="Disabled" OnTextChanged="txtdatesearch_TextChanged"></asp:TextBox>
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
                                        <th>Image</th>
                                        <th>Name</th>
                                        <th>Id</th>
                                        <th>Date</th>
                                        <th>InTime</th>
                                        <th>OutTime</th>
                                        <th>Edit</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:PlaceHolder ID="AttendanceModify" runat="server"></asp:PlaceHolder>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
                <div id="edit_attendance" class="modal custom-modal fade" role="dialog">
                    <div class="modal-dialog modal-dialog-centered" role="document">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h5 class="modal-title">Edit Department</h5>
                                <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                            </div>
                            <div class="modal-body">
                                <asp:HiddenField ID="HiddenField1" runat="server" />
                                <div class="input-block mb-3">
                                    <label class="col-form-label">Name<span class="text-danger">*</span></label>
                                    <asp:TextBox ID="txtname" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="input-block mb-3">
                                    <label class="col-form-label">Emp Id <span class="text-danger">*</span></label>
                                    <asp:TextBox ID="txtempid" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="input-block mb-3">
                                    <label class="col-form-label">Date<span class="text-danger">*</span></label>
                                    <asp:TextBox ID="txtdate" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                </div>
                                <div class="input-block mb-3">
                                    <label class="col-form-label">In Time<span class="text-danger">*</span></label>
                                    <asp:TextBox ID="txtintime" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>

                                <div class="input-block mb-3">
                                    <label class="col-form-label">Out Time<span class="text-danger">*</span></label>
                                    <asp:TextBox ID="txtouttime" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="submit-section">
                                    <asp:Button ID="btnupdateattendance" runat="server" Text="Update" CssClass="btn btn-primary submit-btn" OnClick="btnupdateattendance_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        function editAttendance(firstName, empId, formattedDate, minTime, maxTime, AttendanceId) {
            console.log("Name is : " + AttendanceId);
            $('#<%= txtname.ClientID %>').val(firstName);
            $('#<%= txtempid.ClientID %>').val(empId);
            $('#<%= txtdate.ClientID %>').val(formattedDate);
            $('#<%= txtintime.ClientID %>').val(minTime);
            $('#<%= txtouttime.ClientID %>').val(maxTime);
            $('#<%= HiddenField1.ClientID %>').val(AttendanceId);
        }
    </script>
    <script type="text/javascript">
        document.addEventListener("DOMContentLoaded", function () {
            var dateInput = document.querySelector('#<%= txtdatesearch.ClientID %>');
            var today = new Date().toISOString().split('T')[0];
            dateInput.setAttribute('max', today);
        });

        function isCharacterKey(event) {
            var charCode = (event.which) ? event.which : event.keyCode;
            // Allow: A-Z, a-z, and space (charCode 32 is space)
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
