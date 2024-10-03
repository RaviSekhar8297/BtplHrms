<%@ Page Title="" Language="C#" MasterPageFile="~/Manager.Master" AutoEventWireup="true" CodeBehind="ManagerAssignProjects.aspx.cs" Inherits="Human_Resource_Management.ManagerAssignProjects" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="main-wrapper">
        <div class="page-wrapper">
            <div class="content container-fluid">
                <div class="page-header">
                    <div class="row align-items-center">
                        <div class="col">
                            <h3 class="page-title">Projects</h3>
                            <ul class="breadcrumb">
                                <li class="breadcrumb-item"><a href="ManagerDashbord.aspx">Dashboard</a></li>
                                <li class="breadcrumb-item active"></li>
                            </ul>
                        </div>
                        <div class="col-auto float-end ms-auto">
                            <a href="ManagerProjects.aspx" class="btn add-btn"><i class="fa-solid fa-plus"></i>Project List</a>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <div class="input-block mb-3">
                            <label class="col-form-label">Project Name<span class="text-danger">*</span></label>
                            <asp:DropDownList ID="ddlproject" runat="server" CssClass="form-control" onfocus="disableproject()">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="input-block mb-3">
                            <label class="col-form-label">Assign To<span class="text-danger">*</span></label>
                            <asp:DropDownList ID="ddlname" runat="server" CssClass="form-control" onfocus="disablename()">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="input-block mb-3">
                            <label class="col-form-label">Type<span class="text-danger">*</span></label>
                            <asp:DropDownList ID="ddlrole" runat="server" CssClass="form-control" onfocus="disablerole()">
                                <asp:ListItem>-- Select Type --</asp:ListItem>
                                <asp:ListItem>Backend Web Developer</asp:ListItem>
                                <asp:ListItem>Frontend Web Developer</asp:ListItem>
                                <asp:ListItem>Full-Stack Web Developer</asp:ListItem>
                                <asp:ListItem>Graphic Designer</asp:ListItem>
                                <asp:ListItem>Testing</asp:ListItem>
                                <asp:ListItem>UI/UX Designer</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>

                    <div class="col-md-6">
                        <div class="input-block mb-3">
                            <label class="col-form-label">Start Date <span class="text-danger">*</span></label>
                            <asp:TextBox ID="txtstartdate" runat="server" CssClass="form-control" TextMode="Date" AutoCompleteType="Disabled"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="input-block mb-3">
                            <label class="col-form-label">Target Date <span class="text-danger">*</span></label>
                            <asp:TextBox ID="txttargetdate" runat="server" CssClass="form-control" TextMode="Date" AutoCompleteType="Disabled"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="input-block mb-3">
                            <label class="col-form-label">Total Tasks<span class="text-danger">*</span></label>
                            <asp:TextBox ID="txtnoofdays" runat="server" CssClass="form-control" AutoCompleteType="Disabled"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="input-block mb-3">
                            <label class="col-form-label">Priority<span class="text-danger">*</span></label>
                            <asp:DropDownList ID="ddlpriority" runat="server" CssClass="form-control" onfocus="disablepriority()">
                                <asp:ListItem>-- Select Priority --</asp:ListItem>
                                <asp:ListItem>Low</asp:ListItem>
                                <asp:ListItem>Medium</asp:ListItem>
                                <asp:ListItem>High</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="input-block mb-3">
                            <label class="col-form-label">Description<span class="text-danger">*</span></label>
                            <asp:TextBox ID="txtdescription" runat="server" CssClass="form-control" TextMode="MultiLine" AutoCompleteType="Disabled"></asp:TextBox>
                        </div>
                    </div>
                    <div class="submit-section">
                        <asp:Button ID="btnassignproject" runat="server" Text="Assign" CssClass="btn btn-primary submit-btn" OnClick="btnassignproject_Click" />
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">disablerole
        function disableproject() {
            var ddl = document.getElementById('<%= ddlproject.ClientID %>');
            ddl.options[0].disabled = true;
        }
        function disablename() {
            var ddl = document.getElementById('<%= ddlname.ClientID %>');
            ddl.options[0].disabled = true;
        }
        function disablerole() {
            var ddl = document.getElementById('<%= ddlrole.ClientID %>');
            ddl.options[0].disabled = true;
        }
        function disablepriority() {
            var ddl = document.getElementById('<%= ddlpriority.ClientID %>');
             ddl.options[0].disabled = true;
         }
    </script>
</asp:Content>
