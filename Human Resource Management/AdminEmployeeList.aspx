<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="AdminEmployeeList.aspx.cs" Inherits="Human_Resource_Management.AdminEmployeeList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .dropdown-toggle {
            display: none;
        }
        /*.thredots{
         display:none;
		}*/
        .editpanelImage {
            height: 100px;
            width: 100px;
            border: 1px solid grey;
            border-radius: 40%;
        }
    </style>
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
                            <h3 class="page-title">Employee</h3>
                            <ul class="breadcrumb">
                                <li class="breadcrumb-item"><a href="Admindashboard">Dashboard</a></li>
                                <li class="breadcrumb-item active">Employee</li>
                            </ul>
                        </div>
                        <div class="col-auto float-end ms-auto">
                            <a href="AdminAddEmployee.aspx" class="btn add-btn"><i class="fa-solid fa-plus"></i>Add Employee</a>

                            <div class="view-icons">
                                <a href="AdminAllEmployees.aspx" class="grid-view btn btn-link"><i class="fa fa-th"></i></a>
                               
                                <%-- <a href="AdminAddEmployee.aspx" class="list-view btn btn-link active"><i class="fa-solid fa-plus"></i></a>--%>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- /Page Header -->

                <!-- Search Filter -->
                <div class="row filter-row">
                    <div class="col-sm-6 col-md-4">
                        <div class="input-block mb-3 form-focus select-focus">
                            <asp:TextBox ID="txtsearch" runat="server" AutoPostBack="true" CssClass="form-control floating" AutoCompleteType="Disabled" onkeypress="return isNumberKey(event)" OnTextChanged="txtsearch_TextChanged"></asp:TextBox>
                            <label class="focus-label">Employee Id</label>
                        </div>
                    </div>
                    <div class="col-sm-6 col-md-4">
                        <div class="input-block mb-3 form-focus select-focus">
                            <asp:TextBox ID="txtempnamesearch" runat="server" AutoPostBack="true" CssClass="form-control floating" AutoCompleteType="Disabled" onkeypress="return isCharacterKey(event)" OnTextChanged="txtempnamesearch_TextChanged"></asp:TextBox>
                            <label class="focus-label">Employee Name</label>
                        </div>
                    </div>
                    <div class="col-sm-6 col-md-4">
                        <div class="input-block mb-3 form-focus select-focus">
                            <asp:DropDownList ID="ddlcompanyname" runat="server" CssClass="form-control floating" AutoPostBack="true" OnSelectedIndexChanged="ddlcompanyname_SelectedIndexChanged" onfocus="disableFirstOption()">
                            </asp:DropDownList>
                            <label class="focus-label">Branch</label>
                        </div>
                    </div>
                    <%--<div class="col-sm-6 col-md-3">  
						<a href="#" class="btn btn-success w-100"> Search </a>  
					</div>    --%>
                </div>
                <!-- /Search Filter -->

                <div class="row">
                    <div class="col-md-12">
                        <div class="table-responsive">
                            <table class="table table-striped custom-table datatable">
                                <thead>
                                    <tr>
                                        <%--<th style="color: red;">Image</th>--%>
                                        <th style="color: red;">Name</th>
                                        <th></th> <th></th><th></th>
                                        <th style="color: red;">Employee ID</th>
                                        <th style="color: red;">Mobile</th>
                                        <th class="text-nowrap" style="color: red;">Join Date</th>
                                        <th style="color: red;">Designation</th>
                                        <th style="color: red;">Edit</th>
                                        <th style="color: red;">Delete</th>
                                        <%--<th class="text-end no-sort">Action</th>--%>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:PlaceHolder ID="EmployeesData" runat="server"></asp:PlaceHolder>

                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
            <!-- /Page Content -->



            <!-- Edit Employee Modal -->
            <div id="edit_employee" class="modal custom-modal fade" role="dialog">
                <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title">Edit Employee</h5>
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="modal-body">
                            <asp:HiddenField ID="hfShowModal" runat="server" />
                            <div class="row">
                                <div class="profile-img center-img">
                                    <a href="profile.html" class="avatar">
                                        <asp:Image ID="Image2" runat="server" CssClass="editpanelImage" />
                                    </a>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-4">
                                    <div class="input-block mb-3">
                                        <label class="col-form-label">Salutaion <span class="text-danger">*</span></label>
                                        <asp:DropDownList ID="ddleditsalutation" runat="server" CssClass="form-control" onfocus="disableddlsalutation()">
                                            <asp:ListItem>Mr.</asp:ListItem>
                                            <asp:ListItem>Ms.</asp:ListItem>
                                            <asp:ListItem>Mrs.</asp:ListItem>
                                            <asp:ListItem>Dr.</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="input-block mb-3">
                                        <label class="col-form-label">Name</label>
                                        <asp:TextBox ID="txtname" runat="server" CssClass="form-control" onkeypress="return isCharacterKey(event)"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="input-block mb-3">
                                        <label class="col-form-label">Name (Surname)</label>
                                        <asp:TextBox ID="txtlastname" runat="server" CssClass="form-control" onkeypress="return isCharacterKey(event)"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="input-block mb-3">
                                        <label class="col-form-label">Employee ID <span class="text-danger">*</span></label>
                                        <asp:TextBox ID="txtempid" runat="server" CssClass="form-control" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="input-block mb-3">
                                        <label class="col-form-label">Employee BT Id <span class="text-danger">*</span></label>
                                        <asp:TextBox ID="txtbtid" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="input-block mb-3">
                                        <label class="col-form-label">DeviceCode <span class="text-danger">*</span></label>
                                        <asp:TextBox ID="txtdevicecode" runat="server" CssClass="form-control" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="input-block mb-3">
                                        <label class="col-form-label">Email <span class="text-danger">*</span></label>
                                        <asp:TextBox ID="txtemail" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="input-block mb-3">
                                        <label class="col-form-label">DOB</label>
                                        <asp:TextBox ID="txtdob" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="input-block mb-3">
                                        <label class="col-form-label">Mobile</label>
                                        <asp:TextBox ID="txtmobile" runat="server" CssClass="form-control" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-sm-4">
                                    <div class="input-block mb-3">
                                        <label class="col-form-label">Joining Date <span class="text-danger">*</span></label>
                                        <asp:TextBox ID="txtjoindate" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="input-block mb-3">
                                        <label class="col-form-label">Salary (Annum)</label>
                                        <asp:TextBox ID="txtsalary" runat="server" CssClass="form-control" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="input-block mb-3">
                                        <label class="col-form-label">Employee Type </label>
                                        <asp:DropDownList ID="ddleditemptype" runat="server" CssClass="form-control">
                                            <asp:ListItem>Permanent</asp:ListItem>
                                            <asp:ListItem>Prohibition</asp:ListItem>
                                            <asp:ListItem>Intern</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="input-block mb-3">
                                        <label class="col-form-label">Shift <span class="text-danger">*</span></label>
                                        <asp:TextBox ID="txtshift" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="input-block mb-3">
                                        <label class="col-form-label">Company</label>
                                        <asp:TextBox ID="txteditcompany" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="input-block mb-3">
                                        <label class="col-form-label">Branch <span class="text-danger">*</span></label>
                                        <asp:TextBox ID="txteditbranch" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="input-block mb-3">
                                        <label class="col-form-label">Department <span class="text-danger">*</span></label>
                                        <asp:TextBox ID="txteditdepartment" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="input-block mb-3">
                                        <label class="col-form-label">Designation <span class="text-danger">*</span></label>
                                        <asp:TextBox ID="txtdesignation" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="input-block mb-3">
                                        <label class="col-form-label">PFNumber <span class="text-danger">*</span></label>
                                        <asp:TextBox ID="txtpfnumber" runat="server" CssClass="form-control" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="input-block mb-3">
                                        <label class="col-form-label">ESI Number <span class="text-danger">*</span></label>
                                        <asp:TextBox ID="txtesinumber" runat="server" CssClass="form-control" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                    </div>
                                </div>

                            </div>
                            <div class="table-responsive m-t-15">
                            </div>
                            <div class="submit-section">
                                <asp:Button ID="btnupdateemployee" runat="server" Text="Update Employee Details" CssClass="btn btn-primary submit-btn" OnClick="btnupdateemployee_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- /Edit Employee Modal -->

            <!-- Delete Employee Modal -->
            <div class="modal custom-modal fade" id="delete_employee" role="dialog">
                <div class="modal-dialog modal-dialog-centered">
                    <div class="modal-content">
                        <div class="modal-body">
                            <div class="form-header">
                                <h3>Delete Employee</h3>
                                <p>Are you sure want to delete?</p>
                            </div>
                            <div class="modal-btn delete-action">
                                <div class="row">

                                    <div class="col-6">
                                        <asp:Button ID="btndelete" runat="server" Text="Delete" CssClass="btn btn-primary continue-btn btndelete" OnClick="btndelete_Click" />
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
            <!-- /Delete Employee Modal -->

        </div>
        <!-- /Page Wrapper -->
    </div>
    <!-- /Main Wrapper -->

    <script type="text/javascript">
        function editdatabind(imageUrl, salutaion, Name, LastName, DeviceCode, Email, DOB, Mobile, EmpId, DOJ, Salary, Branch, Company, Department, Designation, pfnumber, esinumber, shift, btid, emptype) {

            $('#<%= Image2.ClientID %>').attr('src', imageUrl);
            $('#<%= ddleditsalutation.ClientID %> option').filter(function () {
                return $(this).text() === salutaion;
            }).attr('selected', true);

            $('#<%= txtname.ClientID %>').val(Name);
            $('#<%= txtlastname.ClientID %>').val(LastName);
            console.log("last Name is :" + LastName);
            $('#<%= txtdevicecode.ClientID %>').val(DeviceCode);
            $('#<%= txtemail.ClientID %>').val(Email); // Set email using val() method
            $('#<%= txtdob.ClientID %>').val(DOB);
            $('#<%= txtmobile.ClientID %>').val(Mobile);
            $('#<%= txtempid.ClientID %>').val(EmpId).attr('selected', true);

            $('#<%= hfShowModal.ClientID %>').val(EmpId);
            $('#<%= txtjoindate.ClientID %>').val(DOJ);
            $('#<%= txtsalary.ClientID %>').val(Salary);


            $('#<%= txteditcompany.ClientID %>').val(Company);
            $('#<%= txteditbranch.ClientID %>').val(Branch);
            $('#<%= txteditdepartment.ClientID %>').val(Department);



            $('#<%= txtdesignation.ClientID %>').val(Designation);
            console.log("Department : " + Department)
            $('#<%= txtpfnumber.ClientID %>').val(pfnumber);
            $('#<%= txtesinumber.ClientID %>').val(esinumber);
            $('#<%= txtshift.ClientID %>').val(shift);
            $('#<%= txtbtid.ClientID %>').val(btid);
            $('#<%= ddleditemptype.ClientID %> option').filter(function () {
                return $(this).text() === emptype;
            }).attr('selected', true);

            $('#edit_employee').modal('show');
        }


       <%-- function hideFirstItem() {
            var select = document.getElementById('<%= ddlcompany.ClientID %>');
            if (select && select.options.length > 0) {
                select.options[0].style.display = 'none';
            }
        }

        function hideFirstdept() {
            var select = document.getElementById('<%= ddldepartment.ClientID %>');
            if (select && select.options.length > 0) {
                select.options[0].style.display = 'none';
            }
      }--%>
    </script>
    disableddlcompany
    <script type="text/javascript">
        function disableddlsalutation() {
            var ddl = document.getElementById('<%= ddleditsalutation.ClientID %>');
            ddl.options[0].disabled = true;
        }
        function disableFirstOption() {
            var ddl = document.getElementById('<%= ddlcompanyname.ClientID %>');
            if (ddl && ddl.options.length > 0) {
                ddl.options[0].disabled = true;
            }
        }

    </script>


    <style type="text/css">
        .btndelete {
            border: none;
            background: none;
            outline: none;
        }

        .input-error {
            border-color: red;
        }
    </style>
    <script type="text/javascript"> // enter emp id only numbers
        function isNumberKey(event) {
            var charCode = (event.which) ? event.which : event.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
                console.log("number is " + charCode);
            }
            return true;
        }
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
