<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="AttendanceHistory.aspx.cs" Inherits="Human_Resource_Management.AttendanceHistory" %>

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
                            <h3 class="page-title">Attendance Monthly Data</h3>
                            <ul class="breadcrumb">
                                <li class="breadcrumb-item"><a href="Admindashboard.aspx">Dashboard</a></li>
                                <li class="breadcrumb-item active">Attendance Modification</li>
                            </ul>
                        </div>
                    </div>
                </div>
                <asp:LinkButton ID="lnbexcell" runat="server" OnClick="lnbexcell_Click"><i class="fa-solid fa-file-excel"></i></asp:LinkButton>
                <!-- /Page Header -->
                <div class="row filter-row">
                    <div class="col-sm-6 col-md-3">
                        <div class="input-block mb-3 form-focus select-focus">
                            <label class="focus-label">Employee Name</label>
                            <asp:TextBox ID="txtempnamesearch" runat="server" CssClass="form-control floating" placeholder="Search By Name" onkeypress="return isCharacterKey(event)" AutoCompleteType="Disabled"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-sm-6 col-md-3">
                        <div class="input-block mb-3 form-focus select-focus">
                            <label class="focus-label">Select Month</label>
                            <asp:TextBox ID="txtmonthsearch" runat="server" CssClass="form-control floating" TextMode="Month" AutoPostBack="true" AutoCompleteType="Disabled" OnTextChanged="txtmonthsearch_TextChanged"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <!-- /Leave Statistics -->
                <div class="row">
                    <div class="col-md-12">
                        <div class="table-responsive fixed-header-table" style="max-height: 500px; overflow-y: auto;">
                            <table class="table table-striped custom-table leave-employee-table mb-0 datatable">
                                <thead>
                                    <asp:PlaceHolder ID="AttendanceTablePlaceholder" runat="server"></asp:PlaceHolder>
                                </thead>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <style type="text/css">
        .headers-name {
            vertical-align: middle;
        }

        .header-date {
            text-align: center;
        }
    </style>
    <script type="text/javascript">
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
