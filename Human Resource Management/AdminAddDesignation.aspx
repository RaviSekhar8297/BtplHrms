<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="AdminAddDesignation.aspx.cs" Inherits="Human_Resource_Management.AdminAddDesignation" %>
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
                            <h3 class="page-title"> </h3>
                            <ul class="breadcrumb">
                                <li class="breadcrumb-item"><a href="admin-dashboard.html">Dashboard</a></li>
                                <li class="breadcrumb-item active">Designation</li>
                            </ul>
                        </div>
                        <div class="col-auto float-end ms-auto">
                            <a href="AdminAddDesignation.aspx" class="btn add-btn"><i class="fa-solid fa-plus"></i>Add New</a>
                          
                             <div class="view-icons">
                                <a href="AdminDesignation.aspx" class="list-view btn btn-link"><i class="fa-solid fa-bars"></i></a>
                                <%--<a href="AdminAddEmployee.aspx" class="list-view btn btn-link active"><i class="fa-solid fa-plus"></i></a>--%>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- /Page Header -->

                <!-- Add Performance Indicator Modal -->
                <asp:Label ID="Label1" runat="server" ForeColor="Red"></asp:Label>
                <div class="row">
                    <div class="col-sm-4">
                        <div class="input-block mb-3">
                            <label class="col-form-label">Company<span class="text-danger">*</span></label>
                            <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-sm-4">
                        <div class="input-block mb-3">
                            <label class="col-form-label">Branch<span class="text-danger">*</span></label>
                            <asp:DropDownList ID="DropDownList2" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-sm-4">
                        <div class="input-block mb-3">
                            <label class="col-form-label">Department<span class="text-danger">*</span></label>
                            <asp:DropDownList ID="DropDownList3" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col-sm-4">
                        <div class="input-block mb-3">
                            <label class="col-form-label">Designation <span class="text-danger">*</span></label>
                            <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="submit-section">
                        <asp:Button ID="btnreview" runat="server" Text="Submit" CssClass="btn btn-primary submit-btn"  />
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- /Add Performance Indicator Modal -->
    <script type="text/javascript">
        function ddladddcompanydisable() {
            var ddl = document.getElementById('<%= DropDownList1.ClientID %>');
            ddl.options[0].disabled = true;
        }
        function ddladddbranchdisable() {
            var ddl = document.getElementById('<%= DropDownList2.ClientID %>');
            ddl.options[0].disabled = true;
        }
        function ddladddeptdisable() {
            var ddl = document.getElementById('<%= DropDownList3.ClientID %>');
            ddl.options[0].disabled = true;
        }
        
    </script>
</asp:Content>
