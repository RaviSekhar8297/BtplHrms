<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="AdminAddAssets.aspx.cs" Inherits="Human_Resource_Management.AdminAddAssets" %>

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
                            <h3 class="page-title">Assets</h3>
                            <ul class="breadcrumb">
                                <li class="breadcrumb-item"><a href="admin-dashboard.html">Dashboard</a></li>
                                <li class="breadcrumb-item active">Assets</li>
                            </ul>
                        </div>
                        <div class="col-auto float-end ms-auto">
                          
                            <%--                            <a href="#" class="btn add-btn" data-bs-toggle="modal" data-bs-target="#add_resignation"><i class="fa-solid fa-plus"></i>Add Resignation</a>--%>
                            <div class="view-icons">
                                <a href="AdminAssets.aspx" class="list-view btn btn-link"><i class="fa-solid fa-bars"></i></a>
                             
                            </div>
                        </div>
                    </div>
                </div>
                <!-- /Page Header -->
                <div class="row">
                    <div class="col-md-4">
                        <div class="input-block mb-3">
                            <label class="col-form-label">Company<span class="text-danger">*</span></label>
                            <asp:DropDownList ID="ddlcompany" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlcompany_SelectedIndexChanged" onfocus="disablecompany()"></asp:DropDownList>
                            <asp:HiddenField ID="HiddenField1" runat="server" />

                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="input-block mb-3">
                            <label class="col-form-label">Branch <span class="text-danger">*</span></label>
                            <asp:DropDownList ID="ddlbranch" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlbranch_SelectedIndexChanged" onfocus="disablebranch()"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="input-block mb-3">
                            <label class="col-form-label">Department <span class="text-danger">*</span></label>
                            <asp:DropDownList ID="ddldepartment" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddldepartment_SelectedIndexChanged" onfocus="disabledepartment()"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="input-block mb-3">
                            <label class="col-form-label">Name <span class="text-danger">*</span></label>
                            <asp:DropDownList ID="ddlname" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlname_SelectedIndexChanged" onfocus="disablename()"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="input-block mb-3">
                            <label class="col-form-label">EmpId <span class="text-danger">*</span></label>
                            <asp:TextBox ID="txtempid" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="input-block mb-3">
                            <label class="col-form-label">Designation <span class="text-danger">*</span></label>
                            <asp:TextBox ID="txtdesignation" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="input-block mb-3">
                            <label class="col-form-label">LapTop <span class="text-danger">*</span></label>
                            <asp:CheckBox ID="cblaptop" runat="server" />
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="input-block mb-3">
                            <label class="col-form-label">Mouse <span class="text-danger">*</span></label>
                            <asp:CheckBox ID="cbmouse" runat="server" />
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="input-block mb-3">
                            <label class="col-form-label">Pendrive <span class="text-danger">*</span></label>
                            <asp:CheckBox ID="cbpendrive" runat="server" />
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="input-block mb-3">
                            <label class="col-form-label">Mobile <span class="text-danger">*</span></label>
                            <asp:CheckBox ID="cbmobile" runat="server" />
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="input-block mb-3">
                            <label class="col-form-label">Sim <span class="text-danger">*</span></label>
                            <asp:CheckBox ID="cbsim" runat="server" />
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="input-block mb-3">
                            <label class="col-form-label">Bag <span class="text-danger">*</span></label>
                            <asp:CheckBox ID="cbbag" runat="server" />
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="input-block mb-3">
                            <label class="col-form-label">Remarks <span class="text-danger">*</span></label>
                            <asp:TextBox ID="txtremarks" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>




                    <div class="submit-section">
                        <asp:Button ID="btnaddassets" runat="server" Text="Submit" CssClass="btn btn-primary submit-btn" OnClick="btnaddassets_Click" />
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        function disablecompany() {
            var ddl = document.getElementById('<%= ddlcompany.ClientID %>');
            ddl.options[0].disabled = true;
        }
        function disablebranch() {
            var ddl = document.getElementById('<%= ddlbranch.ClientID %>');
            ddl.options[0].disabled = true;
        }
        function disabledepartment() {
            var ddl = document.getElementById('<%= ddldepartment.ClientID %>');
            ddl.options[0].disabled = true;
        }
        function disablename() {
            var ddl = document.getElementById('<%= ddlname.ClientID %>');
            ddl.options[0].disabled = true;
        }
    </script>
</asp:Content>
