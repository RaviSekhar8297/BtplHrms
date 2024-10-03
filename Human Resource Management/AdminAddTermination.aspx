<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="AdminAddTermination.aspx.cs" Inherits="Human_Resource_Management.AdminAddTermination" %>

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
                            <h3 class="page-title">Add Termination</h3>
                            <ul class="breadcrumb">
                                <li class="breadcrumb-item"><a href="admin-dashboard.html">Dashboard</a></li>
                                <li class="breadcrumb-item active">Termination</li>
                            </ul>
                        </div>
                        <div class="col-auto float-end ms-auto">
                            <a href="AdminAddTermination.aspx" class="btn add-btn"><i class="fa-solid fa-plus"></i>Add New</a>

                            <div class="view-icons">
                                <a href="AdminTermination.aspx" class="list-view btn btn-link"><i class="fa-solid fa-bars"></i></a>
                                <%--<a href="AdminAddEmployee.aspx" class="list-view btn btn-link active"><i class="fa-solid fa-plus"></i></a>--%>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- /Page Header -->
                <asp:Label ID="Label2" runat="server" ForeColor="Red"></asp:Label>
                <!-- Add Performance Indicator Modal -->
                <asp:Label ID="Label1" runat="server" ForeColor="Red"></asp:Label>
                <div class="row">
                    <div class="col-sm-4">
                        <div class="input-block mb-3">
                            <label class="col-form-label">Company<span class="text-danger">*</span></label>
                            <asp:DropDownList ID="ddlcompany" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlcompany_SelectedIndexChanged" onfocus="ddladddcompanydisable()"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-sm-4">
                        <div class="input-block mb-3">
                            <label class="col-form-label">Branch<span class="text-danger">*</span></label>
                            <asp:DropDownList ID="ddlbranch" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlbranch_SelectedIndexChanged" onfocus="ddladddbranchdisable()"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-sm-4">
                        <div class="input-block mb-3">
                            <label class="col-form-label">Department<span class="text-danger">*</span></label>
                            <asp:DropDownList ID="ddldepartment" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddldepartment_SelectedIndexChanged" onfocus="ddladddeptdisable()"></asp:DropDownList>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col-sm-4">
                        <div class="input-block mb-3">
                            <label class="col-form-label">Name <span class="text-danger">*</span></label>
                            <asp:DropDownList ID="ddlname" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlname_SelectedIndexChanged" onfocus="ddladdnamedisable()"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-sm-4">
                        <div class="input-block mb-3">
                            <label class="col-form-label">Employee Id <span class="text-danger">*</span></label>
                            <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-sm-4">
                        <div class="input-block mb-3">
                            <label class="col-form-label">Designation <span class="text-danger">*</span></label>
                            <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-sm-4">
                        <div class="input-block mb-3">
                            <label class="col-form-label">TerminationType <span class="text-danger">*</span></label>
                            <asp:DropDownList ID="ddlterminationtype" runat="server" CssClass="form-control" onfocus="ddlterminationdisable()">
                                <asp:ListItem> -- Select Type -- </asp:ListItem>
                                <asp:ListItem>Dismissal</asp:ListItem>
                                <asp:ListItem>Firing</asp:ListItem>
                                <asp:ListItem>Parallel Termination</asp:ListItem>
                                <asp:ListItem>Involuntary termination</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                   
                    <div class="col-sm-4">
                        <div class="input-block mb-3">
                            <label class="col-form-label">Reason<span class="text-danger">*</span></label>
                            <asp:TextBox ID="TextBox5" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                        </div>
                    </div>
                </div>

                <div class="submit-section">
                    <asp:Button ID="btntermination" runat="server" Text="Submit" CssClass="btn btn-primary submit-btn" OnClick="btntermination_Click" />
                </div>
            </div>
        </div>
    </div>
    <!-- /Add Performance Indicator Modal -->
    <script type="text/javascript">
        function ddladddcompanydisable() {
            var ddl = document.getElementById('<%= ddlcompany.ClientID %>');
            ddl.options[0].disabled = true;
        }
        function ddladddbranchdisable() {
            var ddl = document.getElementById('<%= ddlbranch.ClientID %>');
            ddl.options[0].disabled = true;
        }
        function ddladddeptdisable() {
            var ddl = document.getElementById('<%= ddldepartment.ClientID %>');
            ddl.options[0].disabled = true;
        }
        function ddladdnamedisable() {
            var ddl = document.getElementById('<%= ddlname.ClientID %>');
            ddl.options[0].disabled = true;
        }

        function ddlterminationdisable() {
            var ddl = document.getElementById('<%= ddlterminationtype.ClientID %>');
             ddl.options[0].disabled = true;
         }


    </script>


</asp:Content>
