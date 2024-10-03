<%@ Page Title="" Language="C#" MasterPageFile="~/Employee.Master" AutoEventWireup="true" CodeBehind="EmployeeAddLeave.aspx.cs" Inherits="Human_Resource_Management.EmployeeAddLeave" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function hideDefaultOption() {
            var ddl = document.getElementById('<%= ddlleavesstatus.ClientID %>');
            ddl.options[0].disabled = true;
        }
    </script>
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
                            <h3 class="page-title">Leaves <span>
                                <asp:Label ID="Label1" runat="server" ForeColor="Red"></asp:Label></span></h3>
                            <ul class="breadcrumb">
                                <li class="breadcrumb-item"><a href="EmployeeDashboard.aspx">Dashboard</a></li>
                                <li class="breadcrumb-item active">Leaves</li>
                            </ul>
                        </div>
                        <div class="col-auto float-end ms-auto">
                            <div class="view-icons">
                                <a href="EmployeeLeave.aspx" class="list-view btn btn-link "><i class="fa-solid fa-bars"></i></a>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- /Page Header  -->
                <div class="row">
                    <div class="col-md-3">
                        <div class="stats-info">
                            <h6>Total Leaves</h6>
                            <h4>
                                <asp:Label ID="lbltotalovertimes" runat="server" Font-Size="Larger" ForeColor="deeppink"></asp:Label></h4>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="stats-info">
                            <h6>Casual Leaves</h6>
                            <h4>
                                <asp:Label ID="lblcasualleavescount" runat="server" Font-Size="Larger" ForeColor="deeppink"></asp:Label></h4>
                        </div>
                    </div>

                    <div class="col-md-3">
                        <div class="stats-info">
                            <h6>Sick Leaves </h6>
                            <h4>
                                <asp:Label ID="lblremainingleavescount" runat="server" Font-Size="Larger" ForeColor="deeppink"></asp:Label></h4>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="stats-info">
                            <h6>Comp-Off Leaves </h6>
                            <h4>
                                <asp:Label ID="Label2" runat="server" Font-Size="Larger" ForeColor="deeppink"></asp:Label></h4>
                        </div>
                    </div>


                </div>
                <!-- Add Leave Modal -->
                <div class="row">
                    <asp:Label ID="Label3" runat="server" Text="" ForeColor="Red"></asp:Label>
                    <div class="col-md-10">
                        <div class="input-block mb-3">
                            <label class="col-form-label">Leave Type <span class="text-danger">*</span></label>
                            <asp:DropDownList ID="ddlleavesstatus" runat="server" CssClass="ddlstatusleave form-control" onfocus="hideDefaultOption()" AutoPostBack="true" OnSelectedIndexChanged="ddlleavesstatus_SelectedIndexChanged">
                                <asp:ListItem>-- Select Leave Type --</asp:ListItem>
                                <asp:ListItem>Casual Leave</asp:ListItem>
                                <asp:ListItem>Sick Leave</asp:ListItem>
                                <asp:ListItem>Comp-Off Leave</asp:ListItem>
                                <asp:ListItem>LOP Leave</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="input-block mb-3">
                            <label class="col-form-label">From Date<span class="text-danger">*</span></label>
                            <asp:TextBox ID="txtstartdate" runat="server" CssClass="form-control" TextMode="Date" AutoPostBack="true" OnTextChanged="txtstartdate_TextChanged"></asp:TextBox>
                            <asp:Label ID="leavedatealert" runat="server" ForeColor="Red"></asp:Label>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="input-block mb-3">
                            <label class="col-form-label">HalfDay from<span class="text-danger"></span></label>
                            <asp:DropDownList ID="ddlfromhalfday" runat="server" onfocus="disableFirstItem2()" CssClass="form-control">
                                <asp:ListItem Value="empty">-- Select Half Day --</asp:ListItem>
                                <asp:ListItem Value="fms">Morning Session</asp:ListItem>
                                <asp:ListItem Value="fafts">Afternoon Session</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="input-block mb-3">
                            <label class="col-form-label">To Date<span class="text-danger">*</span></label>
                            <asp:TextBox ID="txttodate" runat="server" CssClass="form-control" TextMode="Date" AutoPostBack="true" OnTextChanged="txttodate_TextChanged"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="input-block mb-3">
                            <label class="col-form-label">HalfDay To<span class="text-danger"></span></label>
                            <asp:DropDownList ID="ddltohalfday" runat="server" onfocus="disableFirstItem1()" CssClass="form-control" OnSelectedIndexChanged="ddltohalfday_SelectedIndexChanged" AutoPostBack="true">
                                <asp:ListItem Value="empty2">-- Select Half Day --</asp:ListItem>
                                <asp:ListItem Value="tms">Morning Session</asp:ListItem>
                                <asp:ListItem Value="tafts">Afternoon Session</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="input-block mb-3">
                            <label class="col-form-label">Number of days <span class="text-danger"></span></label>
                            <asp:TextBox ID="txtnoofdays" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>

                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="input-block mb-3">
                            <label class="col-form-label">Leave Reason <span class="text-danger">*</span></label>
                            <asp:TextBox ID="txtreasontoapply" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="input-block mb-3">
                            <label class="col-form-label">Apply To<span class="text-danger"></span></label>
                            <asp:DropDownList ID="ddlapplyto" runat="server" onfocus="disableapply()" CssClass="form-control">                              
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="input-block mb-3">
                            <label class="col-form-label">CC To<span class="text-danger"></span></label>
                            <asp:DropDownList ID="ddlccto" runat="server" onfocus="disablecc()" CssClass="form-control">                               
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="submit-section">
                            <asp:Button ID="btnapplyleave" runat="server" Text="Apply Leave" CssClass="btn btn-primary submit-btn" OnClick="btnapplyleave_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- /Add Leave Modal -->
    <script type="text/javascript">
        function disableFirstItem1() {
            var ddl = document.getElementById('<%= ddltohalfday.ClientID %>');
            ddl.options[0].disabled = true;
        }
        function disableFirstItem2() {
            var ddl = document.getElementById('<%= ddlfromhalfday.ClientID %>');
            ddl.options[0].disabled = true;
        }
        function disableapply() {
            var ddl = document.getElementById('<%= ddlapplyto.ClientID %>');
            ddl.options[0].disabled = true;
        }
        function disablecc() {
            var ddl = document.getElementById('<%= ddlccto.ClientID %>');
            ddl.options[0].disabled = true;
        }

        // starts date disable sundays
        document.addEventListener('DOMContentLoaded', function () {
            var dateInput = document.getElementById('<%= txtstartdate.ClientID %>');

            dateInput.addEventListener('change', function () {
                var selectedDate = new Date(dateInput.value);
                var day = selectedDate.getDay();
                if (day === 0) {
                    alert('Sundays are not allowed.');
                    dateInput.value = '';
                }
            });
        });

        document.addEventListener('DOMContentLoaded', function () {
            var dateInput = document.getElementById('<%= txttodate.ClientID %>');

            dateInput.addEventListener('change', function () {
                var selectedDate = new Date(dateInput.value);
                var day = selectedDate.getDay();
                if (day === 0) {
                    alert('Sundays are not allowed.');
                    dateInput.value = '';
                }
            });
        });
    </script>
</asp:Content>
