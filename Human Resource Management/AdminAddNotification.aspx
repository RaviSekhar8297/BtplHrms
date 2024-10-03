<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="AdminAddNotification.aspx.cs" Inherits="Human_Resource_Management.AdminAddNotification" %>

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
                            <h3 class="page-title">Add Notification</h3>
                            <ul class="breadcrumb">
                                <li class="breadcrumb-item"><a href="admin-dashboard.html">Dashboard</a></li>
                                <li class="breadcrumb-item active">Notification</li>
                            </ul>
                        </div>
                        <div class="col-auto float-end ms-auto">
                           <%-- <a href="AdminAddNotification.aspx" class="btn add-btn"><i class="fa-solid fa-plus"></i>Add New</a>--%>

                            <div class="view-icons">
                                <a href="AdminNotificationSettings.aspx" class="list-view btn btn-link"><i class="fa-solid fa-bars"></i></a>
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
                            <asp:DropDownList ID="ddlcompany" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlcompany_SelectedIndexChanged" onfocus="ddldcompanydisable()"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-sm-4">
                        <div class="input-block mb-3">
                            <label class="col-form-label">Branch<span class="text-danger">*</span></label>
                            <asp:DropDownList ID="ddlbranch" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlbranch_SelectedIndexChanged" onfocus="ddlbranchdisable()"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-sm-4">
                        <div class="input-block mb-3">
                            <label class="col-form-label">Department<span class="text-danger">*</span></label>
                            <asp:DropDownList ID="ddldepartment" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddldepartment_SelectedIndexChanged" onfocus="ddldepartmentdisable()"></asp:DropDownList>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col-sm-4">
                        <div class="input-block mb-3">
                            <label class="col-form-label">Name <span class="text-danger">*</span></label>
                            <asp:DropDownList ID="ddlname" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-sm-4">
                        <div class="input-block mb-3">
                            <label class="col-form-label">Subject<span class="text-danger">*</span></label>
                            <asp:TextBox ID="txtsubject" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-sm-4">
                        <div class="input-block mb-3">
                            <label class="col-form-label">Message <span class="text-danger">*</span></label>
                            <asp:TextBox ID="txtmessage" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-4">
                        <div class="input-block mb-3">
                            <label class="col-form-label">Send To <span class="text-danger">*</span></label>
                            <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control"  onfocus="ddlnotifydisable()">
                                <asp:ListItem> -- Select Type-- </asp:ListItem>
                                <asp:ListItem>Through Mail</asp:ListItem>
                                <asp:ListItem>Notification</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-sm-4">
                        <div class="input-block mb-3">
                            
                        </div>
                    </div>
                    <div class="col-sm-4">
                        <div class="input-block mb-3">
                           
                        </div>
                    </div>
                </div>

                <div class="submit-section">
                    <asp:Button ID="btnsendnotification" runat="server" Text="Send..." CssClass="btn btn-primary submit-btn" OnClick="btnsendnotification_Click" />
                </div>
            </div>
        </div>
    </div>
    <!-- /Add Performance Indicator Modal -->
    <script type="text/javascript">
        function ddldcompanydisable() {
            var ddl = document.getElementById('<%= ddlcompany.ClientID %>');
            ddl.options[0].disabled = true;
        }
        function ddlbranchdisable() {
            var ddl = document.getElementById('<%= ddlbranch.ClientID %>');
            ddl.options[0].disabled = true;
        }
        function ddldepartmentdisable() {
            var ddl = document.getElementById('<%= ddldepartment.ClientID %>');
            ddl.options[0].disabled = true;
        }
        function ddlnotifydisable() {
            var ddl = document.getElementById('<%= DropDownList1.ClientID %>');
              ddl.options[0].disabled = true;
          }

    </script>
</asp:Content>
