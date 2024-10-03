<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="AdminUsers.aspx.cs" Inherits="Human_Resource_Management.AdminUsers" %>

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
                            <h3 class="page-title">Users</h3>
                            <ul class="breadcrumb">
                                <li class="breadcrumb-item"><a href="Admindashboard">Dashboard</a></li>
                                <li class="breadcrumb-item active">Users</li>
                            </ul>
                        </div>
                        <div class="col-auto float-end ms-auto">
                            <a href="AdminAddUser.aspx" class="btn add-btn"><i class="fa-solid fa-plus"></i></a>
                        </div>
                    </div>
                </div>
                <!-- /Page Header -->

                <!-- Search Filter -->
                <div class="row filter-row">
                    <div class="col-sm-6 col-md-3">
                        <div class="input-block mb-4 form-focus select-focus">
                            <asp:TextBox ID="txtempnamesearch" runat="server" AutoPostBack="true" CssClass="form-control floating" OnTextChanged="txtempnamesearch_TextChanged" onkeypress="return isCharacterKey(event)" AutoCompleteType="Disabled"></asp:TextBox>
                            <label class="focus-label">Name</label>
                        </div>
                    </div>
                    <div class="col-sm-6 col-md-4">
                        <div class="input-block mb-3 form-focus select-focus">
                            <asp:DropDownList ID="ddlcompany" runat="server" CssClass="form-control" AutoPostBack="true" onfocus="hideCompanyOption()" OnSelectedIndexChanged="ddlcompany_SelectedIndexChanged">
                            </asp:DropDownList>
                            <label class="focus-label">Company</label>
                        </div>
                    </div>
                    <div class="col-sm-6 col-md-4">
                        <div class="input-block mb-3 form-focus select-focus">
                            <asp:DropDownList ID="ddlrole" runat="server" CssClass="form-control select floating" AutoPostBack="true" onfocus="hideDefaultOption()" OnSelectedIndexChanged="ddlrole_SelectedIndexChanged">
                            </asp:DropDownList>
                            <label class="focus-label">Role</label>
                        </div>
                    </div>
                    <%--<div class="col-sm-6 col-md-3">  
						<a href="#" class="btn btn-success w-100"> Search </a>  
					</div>   --%>
                </div>
                <!-- /Search Filter -->

                <div class="row">
                    <div class="col-md-12">
                        <div class="table-responsive">
                            <table class="table table-striped custom-table datatable">
                                <thead>
                                    <tr>
                                        <th>Name</th>
                                        <th>EmpId</th>
                                        <th>Company</th>
                                        <th>Password</th>
                                        <th>Created Date</th>
                                        <th>Role</th>
                                        <th>Edit</th>
                                        <th>Delete</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:PlaceHolder ID="userlogins" runat="server"></asp:PlaceHolder>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
            <!-- /Page Content -->



            <!-- Edit User Modal -->
            <div id="edit_user" class="modal custom-modal fade" role="dialog">
                <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title">Edit User</h5>
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="modal-body">
                            <asp:HiddenField ID="HiddenField1" runat="server" />
                            <asp:HiddenField ID="HiddenField2" runat="server" />
                            <div class="row">
                                <div class="col-sm-6">
                                    <div class="input-block mb-3">
                                        <label class="col-form-label">EmpId</label>
                                        <asp:TextBox ID="txtempid" runat="server" CssClass="form-control" onkeypress="return isNumberKey(event)" AutoCompleteType="Disabled"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="input-block mb-3">
                                        <label class="col-form-label">Name <span class="text-danger">*</span></label>
                                        <asp:TextBox ID="txtname" runat="server" CssClass="form-control" onkeypress="return isCharacterKey(event)" AutoCompleteType="Disabled"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="input-block mb-3">
                                        <label class="col-form-label">Email <span class="text-danger">*</span></label>
                                        <asp:TextBox ID="txtemaill" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="input-block mb-3">
                                        <label class="col-form-label">Designation <span class="text-danger">*</span></label>
                                        <asp:TextBox ID="txtdesignation" runat="server" CssClass="form-control" onkeypress="return isCharacterKey(event)" AutoCompleteType="Disabled"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="input-block mb-3">
                                        <label class="col-form-label">Password</label>
                                        <asp:TextBox ID="txtpassword" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="input-block mb-3">
                                        <label class="col-form-label">Role</label>
                                        <asp:DropDownList ID="DropDownList3" runat="server" CssClass="form-control" onfocus="ddlrolesselect()" OnSelectedIndexChanged="DropDownList3_SelectedIndexChanged" AutoPostBack="true" OnChange="updateCheckboxes()">
                                            <asp:ListItem Value="Select">Select Role</asp:ListItem>
                                            <asp:ListItem Value="Admin">Admin</asp:ListItem>
                                            <asp:ListItem Value="Manager">Manager</asp:ListItem>
                                            <asp:ListItem Value="Employee">Employee</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="input-block mb-3">
                                        <label class="col-form-label">Company </label>
                                        <asp:TextBox ID="txtcompany" runat="server" CssClass="form-control" onkeypress="return isCharacterKey(event)" AutoCompleteType="Disabled"></asp:TextBox>

                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="input-block mb-3">
                                        <label class="col-form-label">Branch</label>
                                        <asp:TextBox ID="txtbranch" runat="server" CssClass="form-control" onkeypress="return isCharacterKey(event)" AutoCompleteType="Disabled"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="input-block mb-3">
                                        <label class="col-form-label">DeptName</label>
                                        <asp:TextBox ID="txtdeptname" runat="server" CssClass="form-control" onkeypress="return isCharacterKey(event)" AutoCompleteType="Disabled"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-sm-6">
                                    <div class="input-block mb-3">
                                        <label class="col-form-label">CreatedDate<span class="text-danger">*</span></label>
                                        <asp:TextBox ID="txtcreateddatee" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                    </div>
                                </div>

                            </div>
                            <div class="table-responsive m-t-15">
                                <!--  starrt -->
                                <table class="table table-striped custom-table">
                                    <thead>
                                        <tr>
                                            <th>Admin Permission</th>
                                            <th class="text-center">Admin</th>
                                            <th class="text-center">Manager</th>
                                            <th class="text-center">Employee</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td>Role</td>
                                            <td class="text-center">
                                                <div class="checkbox-wrapper-31">
                                                    <asp:CheckBox ID="ckbadminstatus" runat="server" />
                                                    <svg viewBox="0 0 35.6 35.6">
                                                        <circle class="background" cx="17.8" cy="17.8" r="17.8"></circle>
                                                        <circle class="stroke" cx="17.8" cy="17.8" r="14.37"></circle>
                                                        <polyline class="check" points="11.78 18.12 15.55 22.23 25.17 12.87"></polyline>
                                                    </svg>
                                                </div>
                                            </td>
                                            <td class="text-center">
                                                <div class="checkbox-wrapper-31">
                                                    <asp:CheckBox ID="ckbmanagerstatus" runat="server" />
                                                    <svg viewBox="0 0 35.6 35.6">
                                                        <circle class="background" cx="17.8" cy="17.8" r="17.8"></circle>
                                                        <circle class="stroke" cx="17.8" cy="17.8" r="14.37"></circle>
                                                        <polyline class="check" points="11.78 18.12 15.55 22.23 25.17 12.87"></polyline>
                                                    </svg>
                                                </div>
                                            </td>
                                            <td class="text-center">
                                                <div class="checkbox-wrapper-31">
                                                    <asp:CheckBox ID="ckbemployeestatus" runat="server" />
                                                    <svg viewBox="0 0 35.6 35.6">
                                                        <circle class="background" cx="17.8" cy="17.8" r="17.8"></circle>
                                                        <circle class="stroke" cx="17.8" cy="17.8" r="14.37"></circle>
                                                        <polyline class="check" points="11.78 18.12 15.55 22.23 25.17 12.87"></polyline>
                                                    </svg>
                                                </div>
                                            </td>
                                        </tr>

                                    </tbody>
                                </table>
                                <table class="table table-striped custom-table">
                                    <thead>
                                        <tr>
                                            <th>Module Permission</th>
                                            <th class="text-center">Add</th>
                                            <th class="text-center">Edit</th>
                                            <th class="text-center">Delete</th>
                                        </tr>
                                    </thead>
                                    <tbody>

                                        <tr>
                                            <td>Employee</td>
                                            <td class="text-center">
                                                <div class="checkbox-wrapper-31">
                                                    <asp:CheckBox ID="ckbaddemployee" runat="server" />
                                                    <svg viewBox="0 0 35.6 35.6">
                                                        <circle class="background" cx="17.8" cy="17.8" r="17.8"></circle>
                                                        <circle class="stroke" cx="17.8" cy="17.8" r="14.37"></circle>
                                                        <polyline class="check" points="11.78 18.12 15.55 22.23 25.17 12.87"></polyline>
                                                    </svg>                                                  

                                                </div>
                                            </td>
                                            <td class="text-center">
                                                <div class="checkbox-wrapper-31">
                                                    <asp:CheckBox ID="ckbeditemployee" runat="server" />
                                                    <svg viewBox="0 0 35.6 35.6">
                                                        <circle class="background" cx="17.8" cy="17.8" r="17.8"></circle>
                                                        <circle class="stroke" cx="17.8" cy="17.8" r="14.37"></circle>
                                                        <polyline class="check" points="11.78 18.12 15.55 22.23 25.17 12.87"></polyline>
                                                    </svg>
                                                </div>
                                            </td>
                                            <td class="text-center">
                                                <div class="checkbox-wrapper-31">
                                                    <asp:CheckBox ID="ckbdeleteemployee" runat="server" />
                                                    <svg viewBox="0 0 35.6 35.6">
                                                        <circle class="background" cx="17.8" cy="17.8" r="17.8"></circle>
                                                        <circle class="stroke" cx="17.8" cy="17.8" r="14.37"></circle>
                                                        <polyline class="check" points="11.78 18.12 15.55 22.23 25.17 12.87"></polyline>
                                                    </svg>
                                                </div>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td>Holidays</td>
                                            <td class="text-center">
                                                <div class="checkbox-wrapper-31">
                                                    <asp:CheckBox ID="ckbaddholiday" runat="server" />
                                                    <svg viewBox="0 0 35.6 35.6">
                                                        <circle class="background" cx="17.8" cy="17.8" r="17.8"></circle>
                                                        <circle class="stroke" cx="17.8" cy="17.8" r="14.37"></circle>
                                                        <polyline class="check" points="11.78 18.12 15.55 22.23 25.17 12.87"></polyline>
                                                    </svg>
                                                </div>
                                            </td>
                                            <td class="text-center">
                                                <div class="checkbox-wrapper-31">
                                                    <asp:CheckBox ID="ckbeditholiday" runat="server" />
                                                    <svg viewBox="0 0 35.6 35.6">
                                                        <circle class="background" cx="17.8" cy="17.8" r="17.8"></circle>
                                                        <circle class="stroke" cx="17.8" cy="17.8" r="14.37"></circle>
                                                        <polyline class="check" points="11.78 18.12 15.55 22.23 25.17 12.87"></polyline>
                                                    </svg>
                                                </div>
                                            </td>
                                            <td class="text-center">
                                                <div class="checkbox-wrapper-31">
                                                    <asp:CheckBox ID="ckbdeleteholiday" runat="server" />
                                                    <svg viewBox="0 0 35.6 35.6">
                                                        <circle class="background" cx="17.8" cy="17.8" r="17.8"></circle>
                                                        <circle class="stroke" cx="17.8" cy="17.8" r="14.37"></circle>
                                                        <polyline class="check" points="11.78 18.12 15.55 22.23 25.17 12.87"></polyline>
                                                    </svg>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Projects</td>
                                            <td class="text-center">
                                                <div class="checkbox-wrapper-31">
                                                    <asp:CheckBox ID="ckbaddprojects" runat="server" />
                                                    <svg viewBox="0 0 35.6 35.6">
                                                        <circle class="background" cx="17.8" cy="17.8" r="17.8"></circle>
                                                        <circle class="stroke" cx="17.8" cy="17.8" r="14.37"></circle>
                                                        <polyline class="check" points="11.78 18.12 15.55 22.23 25.17 12.87"></polyline>
                                                    </svg>
                                                </div>
                                            </td>
                                            <td class="text-center">
                                                <div class="checkbox-wrapper-31">
                                                    <asp:CheckBox ID="ckbeditproject" runat="server" />
                                                    <svg viewBox="0 0 35.6 35.6">
                                                        <circle class="background" cx="17.8" cy="17.8" r="17.8"></circle>
                                                        <circle class="stroke" cx="17.8" cy="17.8" r="14.37"></circle>
                                                        <polyline class="check" points="11.78 18.12 15.55 22.23 25.17 12.87"></polyline>
                                                    </svg>
                                                </div>
                                            </td>
                                            <td class="text-center">
                                                <div class="checkbox-wrapper-31">
                                                    <asp:CheckBox ID="ckbdeleteproject" runat="server" />
                                                    <svg viewBox="0 0 35.6 35.6">
                                                        <circle class="background" cx="17.8" cy="17.8" r="17.8"></circle>
                                                        <circle class="stroke" cx="17.8" cy="17.8" r="14.37"></circle>
                                                        <polyline class="check" points="11.78 18.12 15.55 22.23 25.17 12.87"></polyline>
                                                    </svg>
                                                </div>
                                            </td>

                                        </tr>
                                        <tr>
                                            <td>Leaves</td>
                                            <td class="text-center">
                                                <div class="checkbox-wrapper-31">
                                                    <asp:CheckBox ID="ckbaddleave" runat="server" />
                                                    <svg viewBox="0 0 35.6 35.6">
                                                        <circle class="background" cx="17.8" cy="17.8" r="17.8"></circle>
                                                        <circle class="stroke" cx="17.8" cy="17.8" r="14.37"></circle>
                                                        <polyline class="check" points="11.78 18.12 15.55 22.23 25.17 12.87"></polyline>
                                                    </svg>
                                                </div>
                                            </td>
                                            <td class="text-center">
                                                <div class="checkbox-wrapper-31">
                                                    <asp:CheckBox ID="ckbeditleave" runat="server" />
                                                    <svg viewBox="0 0 35.6 35.6">
                                                        <circle class="background" cx="17.8" cy="17.8" r="17.8"></circle>
                                                        <circle class="stroke" cx="17.8" cy="17.8" r="14.37"></circle>
                                                        <polyline class="check" points="11.78 18.12 15.55 22.23 25.17 12.87"></polyline>
                                                    </svg>
                                                </div>
                                            </td>
                                            <td class="text-center">
                                                <div class="checkbox-wrapper-31">
                                                    <asp:CheckBox ID="ckbdeleteleave" runat="server" />
                                                    <svg viewBox="0 0 35.6 35.6">
                                                        <circle class="background" cx="17.8" cy="17.8" r="17.8"></circle>
                                                        <circle class="stroke" cx="17.8" cy="17.8" r="14.37"></circle>
                                                        <polyline class="check" points="11.78 18.12 15.55 22.23 25.17 12.87"></polyline>
                                                    </svg>
                                                </div>
                                            </td>

                                        </tr>
                                        <tr>
                                            <td>Assets</td>
                                            <td class="text-center">
                                                <div class="checkbox-wrapper-31">
                                                    <asp:CheckBox ID="ckbaddassets" runat="server" />
                                                    <svg viewBox="0 0 35.6 35.6">
                                                        <circle class="background" cx="17.8" cy="17.8" r="17.8"></circle>
                                                        <circle class="stroke" cx="17.8" cy="17.8" r="14.37"></circle>
                                                        <polyline class="check" points="11.78 18.12 15.55 22.23 25.17 12.87"></polyline>
                                                    </svg>
                                                </div>
                                            </td>
                                            <td class="text-center">
                                                <div class="checkbox-wrapper-31">
                                                    <asp:CheckBox ID="ckbeditassets" runat="server" />
                                                    <svg viewBox="0 0 35.6 35.6">
                                                        <circle class="background" cx="17.8" cy="17.8" r="17.8"></circle>
                                                        <circle class="stroke" cx="17.8" cy="17.8" r="14.37"></circle>
                                                        <polyline class="check" points="11.78 18.12 15.55 22.23 25.17 12.87"></polyline>
                                                    </svg>
                                                </div>
                                            </td>
                                            <td class="text-center">
                                                <div class="checkbox-wrapper-31">
                                                    <asp:CheckBox ID="ckbdeleteassets" runat="server" />
                                                    <svg viewBox="0 0 35.6 35.6">
                                                        <circle class="background" cx="17.8" cy="17.8" r="17.8"></circle>
                                                        <circle class="stroke" cx="17.8" cy="17.8" r="14.37"></circle>
                                                        <polyline class="check" points="11.78 18.12 15.55 22.23 25.17 12.87"></polyline>
                                                    </svg>
                                                </div>
                                            </td>
                                            <%--<td class="text-center">
                                                <label class="custom_check">
                                                    <asp:CheckBox ID="CheckBox1" runat="server" />
                                                    <span class="checkmark"></span>
                                                </label>
                                            </td>--%>
                                        </tr>
                                    </tbody>
                                </table>

                                <!-- end -->

                            </div>
                            <div class="submit-section">
                                <asp:Button ID="userupdatebtnclick" runat="server" Text="Submit" CssClass="btn btn-primary submit-btn" OnClick="userupdatebtnclick_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- /Edit User Modal -->

            <!-- Delete User Modal -->
            <div class="modal custom-modal fade" id="delete_user" role="dialog">
                <div class="modal-dialog modal-dialog-centered">
                    <div class="modal-content">
                        <div class="modal-body">
                            <div class="form-header">
                                <h3>Delete User</h3>
                                <p>
                                    Are you sure want to delete
                                    <asp:Label ID="Label1" runat="server" ForeColor="#cc0000"></asp:Label>
                                    ?
                                </p>
                                <asp:HiddenField ID="HiddenField3" runat="server" />
                            </div>
                            <div class="modal-btn delete-action">
                                <div class="row">
                                    <div class="col-6">
                                        <asp:Button ID="btndeleteuser" runat="server" Text="Delete" CssClass="btn btn-primary continue-btn" OnClick="btndeleteuser_Click" />
                                    </div>
                                    <div class="col-6">
                                        <a href="javascript:void(0);" data-bs-dismiss="modal" class="btn btn-primary cancel-btn">Cancel</a>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- /Delete User Modal -->

        </div>
        <!-- /Page Wrapper -->
    </div>
    <!-- /Main Wrapper -->
    <script type="text/javascript">

        function setCheckboxState(checkboxId, status) {
            $('#' + checkboxId).prop('checked', status.toLowerCase() === 'true');
        }

        function edituserlog(EmpId, Name, Email, Designation, Password, Roles, Company, Branch, DeptName, CreatedDate, AdminStatus, ManagerStatus, EmployeeStatus, AddEmployeStatus, EditEmployeStatus, DeleteEmployeStatus, AddHolidayStatus, EditHolidayStatus, DeleteHolidayStatus, AddProject, EditProject, DeleteProject, AddLeave, EditLeave, DeleteLeave, AddAssets, EditAssets, DeleteAssets) {
            $('#<%= txtempid.ClientID %>').val(EmpId);
            $('#<%= HiddenField2.ClientID %>').val(EmpId);
            $('#<%= txtname.ClientID %>').val(Name);
            $('#<%= txtemaill.ClientID %>').val(Email);
            $('#<%= txtdesignation.ClientID %>').val(Designation);
            $('#<%= txtpassword.ClientID %>').val(Password);
            $('#<%= DropDownList3.ClientID %> option').filter(function () {
                return $(this).text() === Roles;
            }).attr('selected', true);

            $('#<%= txtcompany.ClientID %>').val(Company);
            $('#<%= txtbranch.ClientID %>').val(Branch);
            $('#<%= txtdeptname.ClientID %>').val(DeptName);
            $('#<%= txtcreateddatee.ClientID %>').val(CreatedDate);

            setCheckboxState('<%= ckbadminstatus.ClientID %>', AdminStatus);
            setCheckboxState('<%= ckbmanagerstatus.ClientID %>', ManagerStatus);
            setCheckboxState('<%= ckbemployeestatus.ClientID %>', EmployeeStatus);

            setCheckboxState('<%= ckbaddemployee.ClientID %>', AddEmployeStatus);
            setCheckboxState('<%= ckbeditemployee.ClientID %>', EditEmployeStatus);
            setCheckboxState('<%= ckbdeleteemployee.ClientID %>', DeleteEmployeStatus);

            setCheckboxState('<%= ckbaddholiday.ClientID %>', AddHolidayStatus);
            setCheckboxState('<%= ckbeditholiday.ClientID %>', EditHolidayStatus);
            setCheckboxState('<%= ckbdeleteholiday.ClientID %>', DeleteHolidayStatus);

            setCheckboxState('<%= ckbaddprojects.ClientID %>', AddProject);
            setCheckboxState('<%= ckbeditproject.ClientID %>', EditProject);
            setCheckboxState('<%= ckbdeleteproject.ClientID %>', DeleteProject);

            setCheckboxState('<%= ckbaddleave.ClientID %>', AddLeave);
            setCheckboxState('<%= ckbeditleave.ClientID %>', EditLeave);
            setCheckboxState('<%= ckbdeleteleave.ClientID %>', DeleteLeave);

            setCheckboxState('<%= ckbaddassets.ClientID %>', AddAssets);
            setCheckboxState('<%= ckbeditassets.ClientID %>', EditAssets);
            setCheckboxState('<%= ckbdeleteassets.ClientID %>', DeleteAssets);

            $('#edit_user').modal('show');
        }

        function deleteuserlog(EmpId, Name) {
            document.getElementById('<%= Label1.ClientID %>').textContent = Name;
            $('#<%= HiddenField3.ClientID %>').val(EmpId);
        }




      <%--  function edituserlogrole(EmpId, Name, Email, Designation, Password, Roles, Company, Branch, DeptName, CreatedDate, AdminStatus, ManagerStatus, EmployeeStatus, AddEmployeStatus, EditEmployeStatus, DeleteEmployeStatus, AddHolidayStatus, EditHolidayStatus, DeleteHolidayStatus, AddProject, EditProject, DeleteProject, AddLeave, EditLeave, DeleteLeave, AddAssets, EditAssets, DeleteAssets) {
            $('#<%= txtempid.ClientID %>').val(EmpId);
            $('#<%= txtname.ClientID %>').val(Name);
            $('#<%= txtemaill.ClientID %>').val(Email);
            $('#<%= txtdesignation.ClientID %>').val(Designation);
            $('#<%= txtpassword.ClientID %>').val(Password);
            console.log("EmpId id is .... :" + EmpId);
            $('#<%= DropDownList3.ClientID %> option').filter(function () {
                return $(this).text() === Roles;
            }).attr('selected', true);

            $('#<%= txtcompany.ClientID %>').val(Company);
            $('#<%= txtbranch.ClientID %>').val(Branch);
            $('#<%= txtdeptname.ClientID %>').val(DeptName);


            $('#<%= txtcreateddatee.ClientID %>').val(CreatedDate);

            $('#<%= ckbadminstatus.ClientID %>').prop('checked', AdminStatus.toLowerCase() === 'true');

            $('#<%= ckbmanagerstatus.ClientID %>').prop('checked', ManagerStatus.toLowerCase() === 'true');

            $('#<%= ckbemployeestatus.ClientID %>').prop('checked', EmployeeStatus.toLowerCase() === 'true');

            $('#<%= ckbaddemployee.ClientID %>').prop('checked', AddEmployeStatus.toLowerCase() === 'true');
            $('#<%= ckbeditemployee.ClientID %>').prop('checked', EditEmployeStatus.toLowerCase() === 'true');
            $('#<%= ckbdeleteemployee.ClientID %>').prop('checked', DeleteEmployeStatus.toLowerCase() === 'true');

            $('#<%= ckbaddholiday.ClientID %>').prop('checked', AddHolidayStatus.toLowerCase() === 'true');
            $('#<%= ckbeditholiday.ClientID %>').prop('checked', EditHolidayStatus.toLowerCase() === 'true');
            $('#<%= ckbdeleteholiday.ClientID %>').prop('checked', DeleteHolidayStatus.toLowerCase() === 'true');

            $('#<%= ckbaddprojects.ClientID %>').prop('checked', AddProject.toLowerCase() === 'true');
            $('#<%= ckbeditproject.ClientID %>').prop('checked', EditProject.toLowerCase() === 'true');
            $('#<%= ckbdeleteproject.ClientID %>').prop('checked', DeleteProject.toLowerCase() === 'true');

            $('#<%= ckbaddleave.ClientID %>').prop('checked', AddLeave.toLowerCase() === 'true');
            $('#<%= ckbeditleave.ClientID %>').prop('checked', EditLeave.toLowerCase() === 'true');
            $('#<%= ckbdeleteleave.ClientID %>').prop('checked', DeleteLeave.toLowerCase() === 'true');

            $('#<%= ckbaddassets.ClientID %>').prop('checked', AddAssets.toLowerCase() === 'true');
            $('#<%= ckbeditassets.ClientID %>').prop('checked', EditAssets.toLowerCase() === 'true');
            $('#<%= ckbdeleteassets.ClientID %>').prop('checked', DeleteAssets.toLowerCase() === 'true');

            console.log("AdminStatus  is .... :" + AdminStatus);

            $('#edit_user').modal('show');
        }
--%>

        // initial bind 
        function edituserlogrole(EmpId, Name, Email, Designation, Password, Roles, Company, Branch, DeptName, CreatedDate, AdminStatus, ManagerStatus, EmployeeStatus, AddEmployeStatus, EditEmployeStatus, DeleteEmployeStatus, AddHolidayStatus, EditHolidayStatus, DeleteHolidayStatus, AddProject, EditProject, DeleteProject, AddLeave, EditLeave, DeleteLeave, AddAssets, EditAssets, DeleteAssets) {
            $('#<%= txtempid.ClientID %>').val(EmpId);
            $('#<%= HiddenField2.ClientID %>').val(EmpId);
            $('#<%= txtname.ClientID %>').val(Name);
            $('#<%= txtemaill.ClientID %>').val(Email);
            $('#<%= txtdesignation.ClientID %>').val(Designation);
            $('#<%= txtpassword.ClientID %>').val(Password);
            console.log("EmpId id is .... :" + EmpId);
            $('#<%= DropDownList3.ClientID %> option').filter(function () {
                return $(this).text() === Roles;
            }).attr('selected', true);

            $('#<%= txtcompany.ClientID %>').val(Company);
            $('#<%= txtbranch.ClientID %>').val(Branch);
            $('#<%= txtdeptname.ClientID %>').val(DeptName);
            $('#<%= txtcreateddatee.ClientID %>').val(CreatedDate);

            $('#<%= ckbadminstatus.ClientID %>').prop('checked', AdminStatus && AdminStatus.toLowerCase() === 'true');
            $('#<%= ckbmanagerstatus.ClientID %>').prop('checked', ManagerStatus && ManagerStatus.toLowerCase() === 'true');
            $('#<%= ckbemployeestatus.ClientID %>').prop('checked', EmployeeStatus && EmployeeStatus.toLowerCase() === 'true');
            $('#<%= ckbaddemployee.ClientID %>').prop('checked', AddEmployeStatus && AddEmployeStatus.toLowerCase() === 'true');
            $('#<%= ckbeditemployee.ClientID %>').prop('checked', EditEmployeStatus && EditEmployeStatus.toLowerCase() === 'true');
            $('#<%= ckbdeleteemployee.ClientID %>').prop('checked', DeleteEmployeStatus && DeleteEmployeStatus.toLowerCase() === 'true');
            $('#<%= ckbaddholiday.ClientID %>').prop('checked', AddHolidayStatus && AddHolidayStatus.toLowerCase() === 'true');
            $('#<%= ckbeditholiday.ClientID %>').prop('checked', EditHolidayStatus && EditHolidayStatus.toLowerCase() === 'true');
            $('#<%= ckbdeleteholiday.ClientID %>').prop('checked', DeleteHolidayStatus && DeleteHolidayStatus.toLowerCase() === 'true');
            $('#<%= ckbaddprojects.ClientID %>').prop('checked', AddProject && AddProject.toLowerCase() === 'true');
            $('#<%= ckbeditproject.ClientID %>').prop('checked', EditProject && EditProject.toLowerCase() === 'true');
            $('#<%= ckbdeleteproject.ClientID %>').prop('checked', DeleteProject && DeleteProject.toLowerCase() === 'true');
            $('#<%= ckbaddleave.ClientID %>').prop('checked', AddLeave && AddLeave.toLowerCase() === 'true');
            $('#<%= ckbeditleave.ClientID %>').prop('checked', EditLeave && EditLeave.toLowerCase() === 'true');
            $('#<%= ckbdeleteleave.ClientID %>').prop('checked', DeleteLeave && DeleteLeave.toLowerCase() === 'true');
            $('#<%= ckbaddassets.ClientID %>').prop('checked', AddAssets && AddAssets.toLowerCase() === 'true');
            $('#<%= ckbeditassets.ClientID %>').prop('checked', EditAssets && EditAssets.toLowerCase() === 'true');
            $('#<%= ckbdeleteassets.ClientID %>').prop('checked', DeleteAssets && DeleteAssets.toLowerCase() === 'true');

            console.log("AdminStatus is .... :" + (AdminStatus ? AdminStatus : "undefined"));

            $('#edit_user').modal('show');
        }


        function hideCompanyOption() {
            var ddl = document.getElementById('<%= ddlcompany.ClientID %>');
            ddl.options[0].disabled = true;
        }

        function hideDefaultOption() {

            var ddl = document.getElementById('<%= ddlrole.ClientID %>');
            ddl.options[0].disabled = true;
        }



        function ddlrolesselect() {
            var ddl = document.getElementById('<%= DropDownList3.ClientID %>');
            ddl.options[0].disabled = true;
        }

    </script>


    <script type="text/javascript"> // based on dropdown select value checkbox checked fine
        function updateCheckboxes() {
            var ddlRole = document.getElementById('<%= DropDownList3.ClientID %>');
            var selectedRole = ddlRole.options[ddlRole.selectedIndex].value;

            var ckbAdmin = document.getElementById('<%= ckbadminstatus.ClientID %>');
            var ckbManager = document.getElementById('<%= ckbmanagerstatus.ClientID %>');
            var ckbEmployee = document.getElementById('<%= ckbemployeestatus.ClientID %>');

            ckbAdmin.checked = false;
            ckbManager.checked = false;
            ckbEmployee.checked = false;

            if (selectedRole === "Admin") {
                ckbAdmin.checked = true;
            }
            else if (selectedRole === "Manager") {
                ckbManager.checked = true;
            }
            else if (selectedRole === "Employee") {
                ckbEmployee.checked = true;
            }

        }

        document.getElementById('<%= DropDownList3.ClientID %>').addEventListener('change', updateCheckboxes);
    </script>



    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap/5.1.3/js/bootstrap.bundle.min.js"></script>
    <script type="text/javascript">
        function showModal() {
            var myModal = new bootstrap.Modal(document.getElementById('edit_user'),
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
            var ddlLeavesStatus = document.getElementById('<%= DropDownList3.ClientID %>');
            ddlLeavesStatus.addEventListener('change', setModalState);
        }
        );
    </script>
    <script type="text/javascript">
        function isCharacterKey(event) {
            var charCode = (event.which) ? event.which : event.keyCode;
            if ((charCode >= 65 && charCode <= 90) || (charCode >= 97 && charCode <= 122) || charCode == 32) {
                return true;
            } else {
                return false;
            }
        }
        function isNumberKey(event) {
            var charCode = (event.which) ? event.which : event.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
                console.log("number is " + charCode);
            }
            return true;
        }
    </script>
    <style type="text/css">
        .checkbox-wrapper-31:hover .check {
            stroke-dashoffset: 0;
        }

        .checkbox-wrapper-31 {
            position: relative;
            display: inline-block;
            width: 30px;
            height: 30px;
        }

            .checkbox-wrapper-31 .background {
                fill: #ccc;
                transition: ease all 0.6s;
                -webkit-transition: ease all 0.6s;
            }

            .checkbox-wrapper-31 .stroke {
                fill: none;
                stroke: #fff;
                stroke-miterlimit: 10;
                stroke-width: 2px;
                stroke-dashoffset: 100;
                stroke-dasharray: 100;
                transition: ease all 0.6s;
                -webkit-transition: ease all 0.6s;
            }

            .checkbox-wrapper-31 .check {
                fill: none;
                stroke: #fff;
                stroke-linecap: round;
                stroke-linejoin: round;
                stroke-width: 2px;
                stroke-dashoffset: 22;
                stroke-dasharray: 22;
                transition: ease all 0.6s;
                -webkit-transition: ease all 0.6s;
            }

            .checkbox-wrapper-31 input[type=checkbox] {
                position: absolute;
                width: 100%;
                height: 100%;
                left: 0;
                top: 0;
                margin: 0;
                opacity: 0;
                appearance: none;
                -webkit-appearance: none;
            }

                .checkbox-wrapper-31 input[type=checkbox]:hover {
                    cursor: pointer;
                }

                .checkbox-wrapper-31 input[type=checkbox]:checked + svg .background {
                    fill: #6cbe45;
                }

                .checkbox-wrapper-31 input[type=checkbox]:checked + svg .stroke {
                    stroke-dashoffset: 0;
                }

                .checkbox-wrapper-31 input[type=checkbox]:checked + svg .check {
                    stroke-dashoffset: 0;
                }
    </style>
</asp:Content>
