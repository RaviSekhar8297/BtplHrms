<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="AdminAllEmployees.aspx.cs" Inherits="Human_Resource_Management.AdminAllEmployees" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .divbox {
            display: flex;
            flex-direction: column;
            justify-content: center;
            align-items: center;
            height: 170px;
            width: 260px;
            margin-bottom: 10px;
            background-color: #ffffff;
            border: 1px solid #ededed !important;
            overflow: hidden;
            border-radius: 4px;
            -webkit-box-shadow: 0 1px 1px 0 rgba(0, 0, 0, 0.2);
            -moz-box-shadow: 0 1px 1px 0 rgba(0, 0, 0, 0.2);
            box-shadow: 0 1px 1px 0 rgba(0, 0, 0, 0.2);
        }

        .thredots {
            position: relative;
            top: 70px;
            left: 50px;
        }

        .dropdown-toggle {
            display: none;
        }

        .edit {
            float: left;
            margin: 23px 70px 1px 20px;
            padding: 1px 1px 1px 1px;
            color: green;
        }

        .delete {
            float: right;
            margin: 23px 33px 1px 48px;
            padding: 1px 1px 1px 1px;
            color: red;
        }

        .Name {
            padding-top: 31px;
            font-size: 12px !important;
        }

        .designation {
            font-size: 10px !important;
        }

        .avatar > img {
            width: 55px !important;
            height: 55px !important;
            border: 1px solid black;
            box-shadow: 0px 1px 1px 0px rgb(213 195 195 / 20%);
        }

        img {
            max-width: 55px !important;
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
                                <li class="breadcrumb-item"><a href="Admindashboard.aspx">Dashboard</a></li>
                                <li class="breadcrumb-item active">Employee</li>
                            </ul>
                        </div>
                        <div class="col-auto float-end ms-auto">
                            <%--<a href="#" class="btn add-btn" data-bs-toggle="modal" data-bs-target="#add_employee"><i class="fa-solid fa-plus"></i>Add Employee</a>--%>
                            <a href="AdminAddEmployee.aspx" class="btn add-btn"><i class="fa-solid fa-plus"></i>Add Employee</a>
                            <div class="view-icons">

                                <a href="AdminEmployeeList.aspx" class="list-view btn btn-link"><i class="fa-solid fa-bars"></i></a>
                                <%--  <a href="AdminAddEmployee.aspx" class="list-view btn btn-link active"><i class="fa-solid fa-plus"></i></a>--%>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- /Page Header -->

                <!-- Search Filter -->
                <div class="row filter-row">
                    <div class="col-sm-6 col-md-4">
                        <div class="input-block mb-3 form-focus select-focus">
                            <label class="focus-label">Employee Id</label>
                            <asp:TextBox ID="txtsearch" runat="server" AutoPostBack="true" CssClass="form-control floating" OnTextChanged="txtsearch_TextChanged" onkeypress="return isNumberKey(event)" AutoCompleteType="Disabled"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-sm-6 col-md-4">
                        <div class="input-block mb-3 form-focus select-focus">
                            <label class="focus-label">Employee Name</label>
                            <asp:TextBox ID="txtempnamesearch" runat="server" AutoPostBack="true" CssClass="form-control floating" OnTextChanged="txtempnamesearch_TextChanged" AutoCompleteType="Disabled" onkeypress="return isCharacterKey(event)"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-sm-6 col-md-4 form-focus select-focus">
                        <asp:DropDownList ID="ddlcompanyname" runat="server" AutoPostBack="true" CssClass="input-block mb-3 form-focus select-focus form-control" OnSelectedIndexChanged="ddlcompanyname_SelectedIndexChanged" onfocus="companydisable()">
                        </asp:DropDownList>
                        <label class="focus-label">Branch</label>
                        <%--</div>--%>
                    </div>
                    <%--<div class="col-sm-6 col-md-3">
						<div class="d-grid">
							<a href="#" class="btn btn-success w-100"> Search </a>  
						</div>  
					</div>--%>
                </div>
                <!-- Search Filter -->

                <div class="row staff-grid-row">
                    <asp:PlaceHolder ID="EmployeesData" runat="server"></asp:PlaceHolder>
                    <%--<div class="col-md-4 col-sm-6 col-12 col-lg-4 col-xl-3">
						<div class="profile-widget">
							<div class="profile-img">
								<a href="profile.html" class="avatar"><img src="assets/img/profiles/avatar-02.jpg" alt="User Image"></a>
							</div>
							<div class="dropdown profile-action">
								<a href="#" class="action-icon dropdown-toggle" data-bs-toggle="dropdown" aria-expanded="false"><i class="material-icons">more_vert</i></a>
								<div class="dropdown-menu dropdown-menu-right">
									<a class="dropdown-item" href="#" data-bs-toggle="modal" data-bs-target="#edit_employee"><i class="fa-solid fa-pencil m-r-5"></i> Edit</a>
									<a class="dropdown-item" href="#" data-bs-toggle="modal" data-bs-target="#delete_employee"><i class="fa-regular fa-trash-can m-r-5"></i> Delete</a>
								</div>
							</div>
							<h4 class="user-name m-t-10 mb-0 text-ellipsis"><a href="profile.html">John Doe</a></h4>
							<div class="small text-muted">Web Designer</div>
						</div>
					</div>--%>
                </div>
            </div>
            <!-- /Page Content -->



            <!-- Edit Employee Modal first -->
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
                               <%-- <div class="profile-img center-img">
                                    <a href="#" class="avatar">
                                        <asp:Image ID="Image2" runat="server" CssClass="editpanelImage" />
                                       
                                    </a>
                                </div>--%>
                                <div class="profile-img-wrap edit-img">
                                    <asp:Image ID="Image2" runat="server" />
                                    <div class="fileupload btn">
                                        <asp:FileUpload ID="FileUpload1" runat="server" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-4">
                                    <div class="input-block mb-3">
                                        <label class="col-form-label">Salutaion <span class="text-danger">*</span></label>
                                        <asp:DropDownList ID="ddleditsalutation" runat="server" CssClass="form-control">
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
                                        <label class="col-form-label">Salary </label>
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
                                <div class="col-sm-4">
                                    <div class="input-block mb-3">
                                        <label class="col-form-label">Company</label>
                                        <asp:TextBox ID="txtcomanyedit" runat="server" CssClass="form-control"></asp:TextBox>
                                        <%--                                        <asp:DropDownList ID="ddlcompany" runat="server" CssClass="form-control" AutoPostBack="true" onfocus="ddlcompanydisable()" OnSelectedIndexChanged="ddlcompany_SelectedIndexChanged"></asp:DropDownList>--%>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="input-block mb-3">
                                        <label class="col-form-label">Branch <span class="text-danger">*</span></label>
                                        <asp:TextBox ID="txtbranchedit" runat="server" CssClass="form-control"></asp:TextBox>
                                        <%--                                        <asp:DropDownList ID="ddlbranch" runat="server" CssClass="form-control" AutoPostBack="true" onfocus="ddlbranchdisable()" OnSelectedIndexChanged="ddlbranch_SelectedIndexChanged"></asp:DropDownList>--%>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="input-block mb-3">
                                        <label class="col-form-label">Department <span class="text-danger">*</span></label>
                                        <asp:TextBox ID="txtdepartmentedit" runat="server" CssClass="form-control"></asp:TextBox>
                                        <%--                                        <asp:DropDownList ID="ddldepartment" runat="server" CssClass="form-control" onfocus="ddldeptdisable()"></asp:DropDownList>--%>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="input-block mb-3">
                                        <label class="col-form-label">Designation <span class="text-danger">*</span></label>
                                        <asp:TextBox ID="txtdesignation" runat="server" CssClass="form-control" onkeypress="return isCharacterKey(event)"></asp:TextBox>
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
                                <div class="col-md-4">
                                    <div class="input-block mb-3">
                                        <label class="col-form-label">Shift <span class="text-danger">*</span></label>
                                        <asp:TextBox ID="txtshift" runat="server" CssClass="form-control"></asp:TextBox>
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
                                <p>
                                    Are you sure want to delete
                                    <asp:Label ID="Label1" runat="server" ForeColor="#ff0000"></asp:Label>
                                    ?
                                </p>
                            </div>
                            <div class="modal-btn delete-action">
                                <div class="row">
                                    <div class="col-6">
                                        <asp:Button ID="btndelete" runat="server" Text="Delete" CssClass="btn btn-primary continue-btn btndelete" OnClick="btndelete_Click" />
                                    </div>
                                    <div class="col-6">
                                        <a href="javascript:void(0);" data-bs-dismiss="modal" class="btn btn-primary cancel-btn">Cancel</a>
                                    </div>
                                    <asp:HiddenField ID="HiddenField1" runat="server" />
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
        function editdatabind(imageUrl, salutaion, Name, DeviceCode, Email, DOB, Mobile, EmpId, DOJ, Salary, Branch, Company, Department, Designation, pfnumber, esinumber, shift, btid, emptype) {
            $('#<%= Image2.ClientID %>').attr('src', imageUrl);
            $('#<%= ddleditsalutation.ClientID %> option').filter(function () {
                return $(this).text() === salutaion;
            }).attr('selected', true);

            $('#<%= txtname.ClientID %>').val(Name);
            $('#<%= txtdevicecode.ClientID %>').val(DeviceCode);
            $('#<%= txtemail.ClientID %>').val(Email); // Set email using val() method
            $('#<%= txtdob.ClientID %>').val(DOB);
            $('#<%= txtmobile.ClientID %>').val(Mobile);
            $('#<%= txtempid.ClientID %>').val(EmpId);
            $('#<%= hfShowModal.ClientID %>').val(EmpId);
            $('#<%= txtjoindate.ClientID %>').val(DOJ);
            $('#<%= txtsalary.ClientID %>').val(Salary);

            $('#<%= txtcomanyedit.ClientID %>').val(Company);
            $('#<%= txtbranchedit.ClientID %>').val(Branch);

            $('#<%= txtdepartmentedit.ClientID %>').val(Department);



            $('#<%= txtdesignation.ClientID %>').val(Designation);
            $('#<%= txtpfnumber.ClientID %>').val(pfnumber);
            $('#<%= txtesinumber.ClientID %>').val(esinumber);
            $('#<%= txtshift.ClientID %>').val(shift);
            $('#<%= txtbtid.ClientID %>').val(btid);
            $('#<%= ddleditemptype.ClientID %> option').filter(function () {
                return $(this).text() === emptype;
            }).attr('selected', true);

            $('#edit_employee').modal('show');
        }


        function deleteemp(EmpId) {
            document.getElementById('<%= Label1.ClientID %>').innerText = EmpId;
            $('#<%= HiddenField1.ClientID %>').val(EmpId);
            console.log("empid  is : " + EmpId);
            console.log("HiddenField1 is : " + EmpId);
        }


    </script>

    <style type="text/css">
        .btndelete {
            border: none;
            background: none;
            outline: none;
        }

        .center-img {
            display: flex;
            justify-content: center; /* Center horizontally */
            align-items: center; /* Center vertically (if needed) */
            height: 100%; /* Adjust as necessary to ensure vertical centering */
            text-align: center; /* Center text horizontally */
            margin-bottom: 20px;
            margin-top: -20px;
        }
    </style>

    <script type="text/javascript">
        function disableddlsalutation() {
            var ddl = document.getElementById('<%= ddleditsalutation.ClientID %>');
            ddl.options[0].disabled = true;
        }
        function companydisable() {
            var ddl = document.getElementById('<%= ddlcompanyname.ClientID %>');
            ddl.options[0].disabled = true;
        }
    </script>




    <%--  <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap/5.1.3/js/bootstrap.bundle.min.js"></script>
   
    <script type="text/javascript">
        // add employee company 
        function showModal2() {
            var myModal = new bootstrap.Modal(document.getElementById('add_employee'),
                {
                    backdrop: 'static',
                    keyboard: false
                });
            myModal.show();
        }

        // Set hidden field value before postback
        function setModalState() {
            document.getElementById('<%= HiddenField2.ClientID %>').value = "1";
        }

        document.addEventListener('DOMContentLoaded', function () {
            var ddlLeavesStatus = document.getElementById('<%= ddladdcompany.ClientID %>');
            ddlLeavesStatus.addEventListener('change', setModalState);
        });
    </script>--%>

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

    <style type="text/css">
        .input-error {
            border-color: red;
        }
    </style>

    <%-- <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script>  // add emp validations
        $(document).ready(function () {
            $('#<%= txtCompanyCellNo.ClientID %>').on('keyup blur', function () {
                var phoneNumber = $('#<%= txtCompanyCellNo.ClientID %>').val();
                if (phoneNumber.length === 10 && /^\d+$/.test(phoneNumber)) {
                    $('#<%= txtCompanyCellNo.ClientID %>').removeClass('input-error');
                }
                else {
                    $('#<%= txtCompanyCellNo.ClientID %>').addClass('input-error');
                }
            });
        });
    </script>--%>

    <%-- <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script> // this is shift based code 
       $(document).ready(function () {
            $('#<%= TextBox5.ClientID %>').on('keyup blur', function () {
                validateShiftTime();
            });

            function validateShiftTime() {
                var shiftTime = $('#<%= TextBox5.ClientID %>').val();
                var shiftTimePattern = /^([0-1][0-9]|2[0-3]):[0-5][0-9] [APap][Mm] - ([0-1][0-9]|2[0-3]):[0-5][0-9] [APap][Mm]$/;
                if (shiftTimePattern.test(shiftTime)) {
                    $('#<%= TextBox5.ClientID %>').removeClass('input-error');
                    return true;
                } else {
                    $('#<%= TextBox5.ClientID %>').addClass('input-error');
                    return false;
                }
            }
        });
    </script>--%>
</asp:Content>
