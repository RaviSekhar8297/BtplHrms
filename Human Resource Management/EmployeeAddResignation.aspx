<%@ Page Title="" Language="C#" MasterPageFile="~/Employee.Master" AutoEventWireup="true" CodeBehind="EmployeeAddResignation.aspx.cs" Inherits="Human_Resource_Management.EmployeeResignation" %>
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
                            <h3 class="page-title">Resignation</h3>
                            <ul class="breadcrumb">
                                <li class="breadcrumb-item"><a href="admin-dashboard.html">Dashboard</a></li>
                                <li class="breadcrumb-item active">Resignation</li>
                            </ul>
                        </div>
                        <div class="col-auto float-end ms-auto">
                           
                             <div class="view-icons">
                                <a href="EmployeeResignation.aspx" class="list-view btn btn-link"><i class="fa-solid fa-bars"></i></a>
                                <%--<a href="AdminAddEmployee.aspx" class="list-view btn btn-link active"><i class="fa-solid fa-plus"></i></a>--%>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- /Page Header -->
                <div class="row">
                    
                    <div class="col-md-4">
                        <div class="input-block mb-3">
                            <label class="col-form-label">Company<span class="text-danger">*</span></label>
                            <asp:DropDownList ID="ddlcompanyresign" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlcompanyresign_SelectedIndexChanged" onfocus="ddlcompanydisable()">
                            </asp:DropDownList>
                            <asp:HiddenField ID="HiddenField1" runat="server" />

                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="input-block mb-3">
                            <label class="col-form-label">Branch <span class="text-danger">*</span></label>
                            <asp:DropDownList ID="ddlbranchresign" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlbranchresign_SelectedIndexChanged" onfocus="ddlbranchdisable()">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="input-block mb-3">
                            <label class="col-form-label">Department <span class="text-danger">*</span></label>
                            <asp:DropDownList ID="ddldepartmentresign" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddldepartmentresign_SelectedIndexChanged" onfocus="ddldepartmentdisable()">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="input-block mb-3">
                            <label class="col-form-label">Name <span class="text-danger">*</span></label>
                            <asp:DropDownList ID="ddlnameresign" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlnameresign_SelectedIndexChanged" onfocus="ddlnamedisable()">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="input-block mb-3">
                            <label class="col-form-label">Designation <span class="text-danger">*</span></label>
                            <asp:DropDownList ID="ddldesignationresign" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddldesignationresign_SelectedIndexChanged" onfocus="ddldesignationdisable()">
                            </asp:DropDownList>
                        </div>  
                    </div>
                   
                    <div class="col-md-4">
                        <div class="input-block mb-3">
                            <label class="col-form-label">Resign Date <span class="text-danger">*</span></label>
                            <asp:TextBox ID="txtaddresigndate" runat="server" CssClass="form-control" TextMode="Date" AutoCompleteType="Disabled"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="input-block mb-3">
                            <label class="col-form-label">Reason<span class="text-danger">*</span></label>
                            <asp:TextBox ID="txtaddreason" runat="server" CssClass="form-control" TextMode="MultiLine" AutoCompleteType="Disabled"></asp:TextBox>
                        </div>
                    </div>
                    <div class="submit-section">
                        <asp:Button ID="btnadd" runat="server" Text="Submit" CssClass="btn btn-primary submit-btn" OnClick="btnadd_Click" />
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap/5.1.3/js/bootstrap.bundle.min.js"></script>
    <script type="text/javascript">
        function showModal() {
            var myModal = new bootstrap.Modal(document.getElementById('add_resignation'),
                {
                    backdrop: 'static',
                    keyboard: false
                });
            myModal.show();
        }

        function setModalState() {
            document.getElementById('<%= HiddenField1.ClientID %>').value = "1";
        }

        document.addEventListener('DOMContentLoaded', function () {
            var ddlLeavesStatus = document.getElementById('<%= ddlcompanyresign.ClientID %>');
         ddlLeavesStatus.addEventListener('change', setModalState);
     }
     );
    </script>
            


    <script type="text/javascript">
        function ddlcompanydisable()
        {
            var ddl = document.getElementById('<%= ddlcompanyresign.ClientID %>');
             ddl.options[0].disabled = true;
        }
        function ddlbranchdisable() {
            var ddl = document.getElementById('<%= ddlbranchresign.ClientID %>');
             ddl.options[0].disabled = true;
        }
        function ddldepartmentdisable() {
            var ddl = document.getElementById('<%= ddldepartmentresign.ClientID %>');
             ddl.options[0].disabled = true;
        }
        function ddlnamedisable() {
            var ddl = document.getElementById('<%= ddlnameresign.ClientID %>');
             ddl.options[0].disabled = true;
        }
        function ddldesignationdisable() {
            var ddl = document.getElementById('<%= ddldepartmentresign.ClientID %>');
             ddl.options[0].disabled = true;
         }
    </script>
</asp:Content>
