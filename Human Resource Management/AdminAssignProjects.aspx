<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="AdminAssignProjects.aspx.cs" Inherits="Human_Resource_Management.AdminAssignProjects" %>

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
                            <h3 class="page-title">Assign Project</h3>
                            <ul class="breadcrumb">
                                <li class="breadcrumb-item"><a href="Admindashboard.aspx">Dashboard</a></li>
                                <li class="breadcrumb-item active">Timesheet</li>
                            </ul>
                        </div>
                        <div class="col-auto float-end ms-auto">
                            <div class="view-icons">
                                <a href="AdminProjects.aspx" class="list-view btn btn-link"><i class="fa-solid fa-bars"></i></a>
                            </div>
                        </div>

                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <div class="input-block mb-3">
                            <label class="col-form-label">Project Name<span class="text-danger">*</span></label>
                            <asp:TextBox ID="txtprojectname" runat="server" CssClass="form-control" AutoCompleteType="Disabled"></asp:TextBox>
                            <asp:RequiredFieldValidator
                                ID="RequiredFieldValidator4"
                                runat="server"
                                ControlToValidate="txtprojectname"
                                ErrorMessage="Project Name is required."
                                CssClass="text-danger"
                                ValidationGroup="vgForm" />
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="input-block mb-3">
                            <label class="col-form-label">Branch <span class="text-danger">*</span></label>
                            <asp:DropDownList ID="ddlbranch" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlbranch_SelectedIndexChanged" onfocus="disablebranch()">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator
                                ID="rfvEmpName"
                                runat="server"
                                ControlToValidate="ddlbranch"
                                InitialValue="0"
                                ErrorMessage="Please select Branch."
                                CssClass="text-danger"
                                ValidationGroup="vgForm" />
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="input-block mb-3">
                            <label class="col-form-label">Department <span class="text-danger">*</span></label>
                            <asp:DropDownList ID="ddldepartment" runat="server" CssClass="form-control" onfocus="disabledepartment()" AutoPostBack="true" OnSelectedIndexChanged="ddldepartment_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator
                                ID="RequiredFieldValidator1"
                                runat="server"
                                ControlToValidate="ddldepartment"
                                InitialValue="0"
                                ErrorMessage="Please select Department."
                                CssClass="text-danger"
                                ValidationGroup="vgForm" />
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="input-block mb-3">
                            <label class="col-form-label">Assign To<span class="text-danger">*</span></label>
                            <asp:DropDownList ID="ddlmanagername" runat="server" CssClass="form-control" onfocus="disablename()">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator
                                ID="RequiredFieldValidator2"
                                runat="server"
                                ControlToValidate="ddlmanagername"
                                InitialValue="0"
                                ErrorMessage="Please select Manager."
                                CssClass="text-danger"
                                ValidationGroup="vgForm" />
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="input-block mb-3">
                            <label class="col-form-label">Start Date <span class="text-danger">*</span></label>
                            <asp:TextBox ID="txtstartdate" runat="server" CssClass="form-control" TextMode="Date" AutoCompleteType="Disabled"></asp:TextBox>
                            <asp:RequiredFieldValidator
                                ID="rfvDate"
                                runat="server"
                                ControlToValidate="txtstartdate"
                                ErrorMessage="Date is required."
                                CssClass="text-danger"
                                ValidationGroup="vgForm" />
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="input-block mb-3">
                            <label class="col-form-label">Target Date <span class="text-danger">*</span></label>
                            <asp:TextBox ID="txttargetdate" runat="server" CssClass="form-control" TextMode="Date" AutoCompleteType="Disabled"></asp:TextBox>
                            <asp:RequiredFieldValidator
                                ID="RequiredFieldValidator3"
                                runat="server"
                                ControlToValidate="txttargetdate"
                                ErrorMessage="Date is required."
                                CssClass="text-danger"
                                ValidationGroup="vgForm" />
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="input-block mb-3">
                            <label class="col-form-label">Priority <span class="text-danger">*</span></label>
                            <asp:DropDownList ID="ddlpriority" runat="server" CssClass="form-control" onfocus="disablepriority()">
                                <asp:ListItem>-- Select Priority --</asp:ListItem>
                                <asp:ListItem>Low</asp:ListItem>
                                <asp:ListItem>Medium</asp:ListItem>
                                <asp:ListItem>High</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator
                                ID="RequiredFieldValidator5"
                                runat="server"
                                ControlToValidate="ddlpriority"
                                InitialValue="0"
                                ErrorMessage="Please select Priority."
                                CssClass="text-danger"
                                ValidationGroup="vgForm" />
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="input-block mb-3">
                            <label class="col-form-label">Description<span class="text-danger">*</span></label>
                            <asp:TextBox ID="txtdescription" runat="server" CssClass="form-control" TextMode="MultiLine" AutoCompleteType="Disabled" onkeyup="updateCharCount()"></asp:TextBox>
                            <asp:RequiredFieldValidator
                                ID="rfvReason"
                                runat="server"
                                ControlToValidate="txtdescription"
                                ErrorMessage="Description is required."
                                CssClass="text-danger"
                                ValidationGroup="vgForm" />

                            <asp:RegularExpressionValidator
                                ID="revReasonLength"
                                runat="server"
                                ControlToValidate="txtdescription"
                                ValidationExpression="^.{20,}$"
                                ErrorMessage="Description must be at least 20 characters long."
                                CssClass="text-danger"
                                ValidationGroup="vgForm" />
                            <span id="count"></span>
                        </div>
                    </div>
                    <div class="submit-section">
                        <asp:Button ID="btnassignproject" runat="server" Text="Assign" CssClass="btn btn-primary submit-btn" OnClick="btnassignproject_Click" ValidationGroup="vgForm" />
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        function disablebranch() {
            var ddl = document.getElementById('<%= ddlbranch.ClientID %>');
            ddl.options[0].disabled = true;
        }
        function disabledepartment() {
            var ddl = document.getElementById('<%= ddldepartment.ClientID %>');
            ddl.options[0].disabled = true;
        }
        function disablename() {

            var ddl = document.getElementById('<%= ddlmanagername.ClientID %>');
            ddl.options[0].disabled = true;
        }
        function disablepriority() {
            var ddl = document.getElementById('<%= ddlpriority.ClientID %>');
            ddl.options[0].disabled = true;
        }
    </script>
    <script type="text/javascript">
        function updateCharCount() {
            var textbox = document.getElementById('<%= txtdescription.ClientID %>');
            var countSpan = document.getElementById('count');
            var count = textbox.value.length;

            countSpan.innerText = 'Count' + count;
        }
    </script>

</asp:Content>
