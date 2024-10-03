<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="AdminAddEmployee.aspx.cs" Inherits="Human_Resource_Management.AdminAddEmployee" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        /* General Styling */
        body {
            font-family: Arial, sans-serif;
        }

        /*.nav-pills {
			position: fixed;
			top: 0;
			width: 200px;*/ /* Adjust based on your layout */
        /*height: 100vh;
			
		}*/
        /* Tab List Styling */
        .nav-pills .nav-link {
            color: #555;
            border-radius: 0.25rem;
            padding: 10px 15px;
            margin-bottom: 5px;
            background-color: #f8f9fa;
            border: 1px solid #ddd;
            transition: background-color 0.3s, color 0.3s;
        }

            .nav-pills .nav-link:hover {
                background-color: #17a2b8;
                color: white;
            }

            .nav-pills .nav-link.active {
                background-color: #007bff;
                color: #fff;
                font-weight: bold;
                box-shadow: 0 4px 8px rgba(0, 0, 0, 0.2);
            }

        /* Tab Content Styling */
        .tab-content {
            padding: 0px 20px 0px 20px;
            border: 1px solid #ddd;
            border-radius: 0.25rem;
            background-color: #ffffff;
            box-shadow: 0px 0px 10px rgba(0, 0, 0, 0.1);
            transition: background-color 0.5s, color 0.5s;
            overflow-y: auto;
            height: 500px;
        }

            /* Style the scrollbar */
            .tab-content::-webkit-scrollbar {
                width: 12px; /* Width of the scrollbar */
            }

            .tab-content::-webkit-scrollbar-track {
                background: #fff0e6; /* Track color */
                border-radius: 10px; /* Rounded track */
            }

            .tab-content::-webkit-scrollbar-thumb {
                background-color: #ffc299; /* Scrollbar color */
                border-radius: 10px; /* Rounded scrollbar */
                border: 3px solid #f1f1f1; /* Space around scrollbar */
            }

                .tab-content::-webkit-scrollbar-thumb:hover {
                    background-color: #ffb380; /* Scrollbar color on hover */
                }


        /* Slide-In Effect */
        .tab-pane {
            opacity: 0;
            /*transform: translateX(-20px);*/
            transition: opacity 0.3s ease-in-out, transform 0.3s ease-in-out;
        }

            .tab-pane.show {
                opacity: 1;
                transform: translateX(0);
            }

        /* Input Field Styling */
        .input-block {
            margin-bottom: 15px;
        }

            .input-block label {
                font-weight: bold;
                color: #333;
            }

            .input-block .form-control {
                border-radius: 0.25rem;
                border: 1px solid #ddd;
                padding: 10px;
            }

                .input-block .form-control:focus {
                    border-color: #007bff;
                    box-shadow: none;
                }

        /* Row Styling */
        .row {
            margin-top: 20px;
        }

        .text-danger {
            color: #dc3545 !important;
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
                                <li class="breadcrumb-item"><a href="AdminAllEmployees">Employee</a></li>
                            </ul>
                        </div>
                        <div class="col-auto float-end ms-auto">
                            <div class="view-icons">
                                <a href="AdminAllEmployees.aspx" class="grid-view btn btn-link"><i class="fa fa-th"></i></a>
                                <a href="AdminEmployeeList.aspx" class="list-view btn btn-link"><i class="fa-solid fa-bars"></i></a>
                                <%--  <a href="AdminAddEmployee.aspx" class="list-view btn btn-link active"><i class="fa-solid fa-plus"></i></a>--%>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- /Page Header -->


                <!-- Add Employee Modal -->
                <div class="row">
                    <div class="col-sm-2">
                        <!-- Vertical Tabs Navigation -->
                        <ul class="nav flex-column nav-pills" id="v-pills-tab" role="tablist" aria-orientation="vertical">
                            <li class="nav-item"><a class="nav-link active" id="v-pills-Personal-tab" data-bs-toggle="pill" href="#v-pills-Personal" role="tab" aria-controls="v-pills-Personal" aria-selected="true">Personal Details</a></li>
                            <li class="nav-item"><a class="nav-link" id="v-pills-Contact-tab" data-bs-toggle="pill" href="#v-pills-Contact" role="tab" aria-controls="v-pills-Contact" aria-selected="false">Contact Details</a></li>
                            <li class="nav-item"><a class="nav-link" id="v-pills-Salary-tab" data-bs-toggle="pill" href="#v-pills-Salary" role="tab" aria-controls="v-pills-Salary" aria-selected="false">Salary Details</a> </li>
                            <li class="nav-item"><a class="nav-link" id="v-pills-Education-tab" data-bs-toggle="pill" href="#v-pills-Education" role="tab" aria-controls="v-pills-Education" aria-selected="false">Education Details</a> </li>
                            <li class="nav-item"><a class="nav-link" id="v-pills-Family-tab" data-bs-toggle="pill" href="#v-pills-Family" role="tab" aria-controls="v-pills-Family" aria-selected="false">Family Details</a> </li>
                            <li class="nav-item"><a class="nav-link" id="v-pills-Previous-tab" data-bs-toggle="pill" href="#v-pills-Previous" role="tab" aria-controls="v-pills-Previous" aria-selected="false">Previous Company</a> </li>
                            <!-- Add more tabs as needed -->
                        </ul>
                    </div>

                    <div class="col-sm-10">
                        <!-- Tab Content -->
                        <div class="tab-content" id="v-pills-tabContent">
                            <!-- Personal Details Tab -->
                            <div class="tab-pane fade show active" id="v-pills-Personal" role="tabpanel" aria-labelledby="v-pills-Personal-tab">
                                <asp:Label ID="Label1" runat="server"></asp:Label>
                                <div class="row">
                                    <!--EmployeeID-->
                                    <div class="col-sm-4">
                                        <div class="input-block mb-3">
                                            <label class="col-form-label">EmployeeID <span class="text-danger">*</span></label>
                                            <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control" AutoCompleteType="Disabled" AutoPostBack="true" OnTextChanged="TextBox1_TextChanged" onkeypress="return isNumberKey(event)"></asp:TextBox>

                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator26" runat="server" ControlToValidate="TextBox1"
                                                ErrorMessage="Enter employee Id" ValidationGroup="vgForm" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <!--/EmployeeID-->

                                    <!--BT-ID-->
                                    <div class="col-sm-4">
                                        <div class="input-block mb-3">
                                            <label class="col-form-label">BT-ID <span class="text-danger">*</span></label>
                                            <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                        </div>
                                    </div>
                                    <!--/BT-ID-->

                                    <!--Salutation-->
                                    <div class="col-sm-4">
                                        <div class="input-block mb-3">
                                            <label class="col-form-label">Salutation<span class="text-danger">*</span></label>
                                            <asp:DropDownList ID="ddlsalutation" runat="server" CssClass="form-control" onfocus="disableddlsalutation()">
                                                <asp:ListItem> -- Select Salutation -- </asp:ListItem>
                                                <asp:ListItem>Mr.</asp:ListItem>
                                                <asp:ListItem>Ms.</asp:ListItem>
                                                <asp:ListItem>Mrs.</asp:ListItem>
                                                <asp:ListItem>Dr.</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator
                                                ID="rfvEmpName"
                                                runat="server"
                                                ControlToValidate="ddlsalutation"
                                                InitialValue="0"
                                                ErrorMessage=" select Salutation."
                                                CssClass="text-danger"
                                                ValidationGroup="vgForm" />
                                        </div>
                                    </div>
                                    <!--/Salutation-->

                                    <!--FirstName-->
                                    <div class="col-sm-4">
                                        <div class="input-block mb-3">
                                            <label class="col-form-label">FirstName  <span class="text-danger">*</span></label>
                                            <asp:TextBox ID="txtaddsurname" runat="server" CssClass="form-control" onkeypress="return isCharacterKey(event)" AutoCompleteType="Disabled"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtaddsurname"
                                                ErrorMessage="Required FirstName" ValidationGroup="vgForm" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <!--/FirstName-->

                                    <!--Name-->
                                    <div class="col-sm-4">
                                        <div class="input-block mb-3">
                                            <label class="col-form-label">Name (surname)<span class="text-danger">*</span></label>
                                            <asp:TextBox ID="txtaddlastname" runat="server" CssClass="form-control" onkeypress="return isCharacterKey(event)" AutoCompleteType="Disabled"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtaddlastname"
                                                ErrorMessage="Required Name" ValidationGroup="vgForm" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <!--/Name-->

                                    <!--DOB-->
                                    <div class="col-sm-4">
                                        <div class="input-block mb-3">
                                            <label class="col-form-label">DOB</label>
                                            <asp:TextBox ID="txtadddob" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                            <asp:RequiredFieldValidator
                                                ID="RequiredFieldValidator2"
                                                runat="server"
                                                ControlToValidate="txtadddob"
                                                ErrorMessage="Date is required."
                                                CssClass="text-danger"
                                                ValidationGroup="vgForm" />
                                        </div>
                                    </div>
                                    <!--DOB-->

                                    <!--DOJ-->
                                    <div class="col-sm-4">
                                        <div class="input-block mb-3">
                                            <label class="col-form-label">DOJ<span class="text-danger">*</span></label>
                                            <asp:TextBox ID="txtadddoj" runat="server" CssClass="form-control" TextMode="Date" AutoCompleteType="Disabled"></asp:TextBox>
                                            <asp:RequiredFieldValidator
                                                ID="RequiredFieldValidator4"
                                                runat="server"
                                                ControlToValidate="txtadddoj"
                                                ErrorMessage="Date is required."
                                                CssClass="text-danger"
                                                ValidationGroup="vgForm" />
                                        </div>
                                    </div>
                                    <!--DOJ-->

                                    <!--Gender-->
                                    <div class="col-sm-4">
                                        <div class="input-block mb-3">
                                            <label class="col-form-label">Gender <span class="text-danger">*</span></label>
                                            <asp:DropDownList ID="ddlgender" runat="server" CssClass="form-control" onfocus="disableddlgender()">
                                                <asp:ListItem> -- Select Gender -- </asp:ListItem>
                                                <asp:ListItem>Male</asp:ListItem>
                                                <asp:ListItem>FeMale</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator
                                                ID="RequiredFieldValidator5"
                                                runat="server"
                                                ControlToValidate="ddlgender"
                                                InitialValue="0"
                                                ErrorMessage=" select Gender."
                                                CssClass="text-danger"
                                                ValidationGroup="vgForm" />
                                        </div>
                                    </div>
                                    <!--Gender-->

                                    <!--Company-->
                                    <div class="col-sm-4">
                                        <div class="input-block mb-3">
                                            <label class="col-form-label">Company <span class="text-danger">*</span></label>
                                            <asp:DropDownList ID="ddladdcompany" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddladdcompany_SelectedIndexChanged" onfocus="ddladdcompanydisable()">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator
                                                ID="RequiredFieldValidator6"
                                                runat="server"
                                                ControlToValidate="ddladdcompany"
                                                InitialValue="0"
                                                ErrorMessage=" select Company."
                                                CssClass="text-danger"
                                                ValidationGroup="vgForm" />
                                        </div>
                                    </div>
                                    <!--Company-->

                                    <!--Branch-->
                                    <div class="col-sm-4">
                                        <div class="input-block mb-3">
                                            <label class="col-form-label">Branch <span class="text-danger">*</span></label>
                                            <asp:DropDownList ID="ddladdbranch" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddladdbranch_SelectedIndexChanged" onfocus="ddladdbranchdisable()">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator
                                                ID="RequiredFieldValidator7"
                                                runat="server"
                                                ControlToValidate="ddladdbranch"
                                                InitialValue="0"
                                                ErrorMessage=" select Branch."
                                                CssClass="text-danger"
                                                ValidationGroup="vgForm" />
                                        </div>
                                    </div>
                                    <!--Branch-->

                                    <!--Department-->
                                    <div class="col-sm-4">
                                        <div class="input-block mb-3">
                                            <label class="col-form-label">Department <span class="text-danger">*</span></label>
                                            <asp:DropDownList ID="ddladddepartment" runat="server" CssClass="form-control" onfocus="ddladddepartmentdisable()">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator
                                                ID="RequiredFieldValidator8"
                                                runat="server"
                                                ControlToValidate="ddladddepartment"
                                                InitialValue="0"
                                                ErrorMessage=" select Department."
                                                CssClass="text-danger"
                                                ValidationGroup="vgForm" />
                                        </div>
                                    </div>
                                    <!--Department-->

                                    <!--Designation-->
                                    <div class="col-sm-4">
                                        <div class="input-block mb-3">
                                            <label class="col-form-label">Designation<span class="text-danger">*</span></label>
                                            <asp:TextBox ID="TextBox4" runat="server" CssClass="form-control" onkeypress="return isCharacterKey(event)" AutoCompleteType="Disabled"></asp:TextBox>
                                            <asp:RequiredFieldValidator
                                                ID="RequiredFieldValidator9"
                                                runat="server"
                                                ControlToValidate="TextBox4"
                                                ErrorMessage="Designation is required."
                                                CssClass="text-danger"
                                                ValidationGroup="vgForm" />
                                        </div>
                                    </div>
                                    <!--Designation-->

                                    <!--Employee Type-->
                                    <div class="col-sm-4">
                                        <div class="input-block mb-3">
                                            <label class="col-form-label">Employee Type<span class="text-danger">*</span></label>
                                            <asp:DropDownList ID="ddlemptype" runat="server" CssClass="form-control" onfocus="disableddlemptype()">
                                                <asp:ListItem> -- Select Emp Type -- </asp:ListItem>
                                                <asp:ListItem Value="1">Permanent</asp:ListItem>
                                                <asp:ListItem Value="2">Prohibition</asp:ListItem>
                                                <asp:ListItem Value="3">Intern</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator
                                                ID="RequiredFieldValidator10"
                                                runat="server"
                                                ControlToValidate="ddlemptype"
                                                InitialValue="0"
                                                ErrorMessage=" select Emp Type."
                                                CssClass="text-danger"
                                                ValidationGroup="vgForm" />
                                        </div>
                                    </div>
                                    <!--Employee Type-->

                                    <!--Blood Group-->

                                    <div class="col-sm-4">
                                        <div class="input-block mb-3">
                                            <label class="col-form-label">Blood Group<span class="text-danger">*</span></label>
                                            <asp:DropDownList ID="ddlbloodgroup" runat="server" CssClass="form-control" onfocus="disableddbloodgroup()">
                                                <asp:ListItem> -- Select Blood Group -- </asp:ListItem>
                                                <asp:ListItem>A+</asp:ListItem>
                                                <asp:ListItem>A-</asp:ListItem>
                                                <asp:ListItem>B+</asp:ListItem>
                                                <asp:ListItem>B-</asp:ListItem>
                                                <asp:ListItem>AB+</asp:ListItem>
                                                <asp:ListItem>AB-</asp:ListItem>
                                                <asp:ListItem>O+</asp:ListItem>
                                                <asp:ListItem>O-</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator
                                                ID="RequiredFieldValidator11"
                                                runat="server"
                                                ControlToValidate="ddlbloodgroup"
                                                InitialValue="0"
                                                ErrorMessage=" select Blood Group."
                                                CssClass="text-danger"
                                                ValidationGroup="vgForm" />
                                        </div>
                                    </div>
                                    <!--Blood Group-->

                                    <!--ShiftType-->
                                    <div class="col-sm-4">
                                        <div class="input-block mb-3">
                                            <label class="col-form-label">ShiftType<span class="text-danger">*</span></label>
                                            <asp:DropDownList ID="ddladdshifttype" runat="server" CssClass="form-control" onfocus="ddladdshiftdisable()" AutoPostBack="true" OnSelectedIndexChanged="ddladdshifttype_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator
                                                ID="RequiredFieldValidator12"
                                                runat="server"
                                                ControlToValidate="ddladdshifttype"
                                                InitialValue="0"
                                                ErrorMessage=" select ShiftType."
                                                CssClass="text-danger"
                                                ValidationGroup="vgForm" />
                                        </div>
                                    </div>
                                    <!--/ShiftType-->

                                    <!--/Shift-->
                                    <div class="col-sm-4">
                                        <div class="input-block mb-3">
                                            <label class="col-form-label">Shift<span class="text-danger">*</span></label>
                                            <asp:TextBox ID="txtaddshift1" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                        </div>
                                    </div>
                                    <!--/Shift-->

                                    <!--/Shift2-->
                                    <div class="col-sm-4">
                                        <div class="input-block mb-3">
                                            <label class="col-form-label">Shift2<span class="text-danger"></span></label>
                                            <asp:TextBox ID="txtaddshift2" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                        </div>
                                    </div>
                                    <!--/Shift2-->

                                    <!--/Shift3-->
                                    <div class="col-sm-4">
                                        <div class="input-block mb-3">
                                            <label class="col-form-label">Shift3<span class="text-danger"></span></label>
                                            <asp:TextBox ID="txtaddshift3" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                        </div>
                                    </div>
                                    <!--/Shift3-->

                                </div>
                            </div>
                            <!--/Personal Details Tab -->

                            <!-- Contact Details Tab -->
                            <div class="tab-pane fade" id="v-pills-Contact" role="tabpanel" aria-labelledby="v-pills-Contact-tab">
                                <div class="row">
                                    <!--Comapny Email-->
                                    <div class="col-sm-4">
                                        <div class="input-block mb-3">
                                            <label class="col-form-label">Comapny Email<span class="text-danger">*</span></label>
                                            <asp:TextBox ID="TextBox5" runat="server" CssClass="form-control" AutoCompleteType="Disabled"></asp:TextBox>
                                            <asp:RegularExpressionValidator
                                                ID="revEmail"
                                                runat="server"
                                                ControlToValidate="TextBox5"
                                                ValidationExpression="^[a-zA-Z0-9._%+-]+@gmail\.com|gmail\.in$"
                                                ErrorMessage="Please enter a valid Gmail address (e.g., user@gmail.com or user@gmail.in)."
                                                CssClass="text-danger"
                                                ValidationGroup="vgForm" />
                                        </div>
                                    </div>
                                    <!--/Comapny Email-->

                                    <!--Personal Email-->
                                    <div class="col-sm-4">
                                        <div class="input-block mb-3">
                                            <label class="col-form-label">Personal Email<span class="text-danger"></span></label>
                                            <asp:TextBox ID="TextBox6" runat="server" CssClass="form-control" AutoCompleteType="Disabled"></asp:TextBox>
                                            <asp:RegularExpressionValidator
                                                ID="RegularExpressionValidator1"
                                                runat="server"
                                                ControlToValidate="TextBox6"
                                                ValidationExpression="^[a-zA-Z0-9._%+-]+@gmail\.com|gmail\.in$"
                                                ErrorMessage="Please enter a valid Gmail address (e.g., user@gmail.com or user@gmail.in)."
                                                CssClass="text-danger"
                                                ValidationGroup="vgForm" />
                                        </div>
                                    </div>
                                    <!--/Personal Email-->

                                    <!--Comapny Mobile-->
                                    <div class="col-sm-4">
                                        <div class="input-block mb-3">
                                            <label class="col-form-label">Comapny Mobile<span class="text-danger">*</span></label>
                                            <asp:TextBox ID="txtCompanyCellNo" runat="server" CssClass="form-control" AutoCompleteType="Disabled" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                            <asp:RegularExpressionValidator
                                                ID="revCompanyCellNo"
                                                runat="server"
                                                ControlToValidate="txtCompanyCellNo"
                                                ValidationExpression="^\d{10}$"
                                                ErrorMessage="10-digit mobile number without spaces."
                                                CssClass="text-danger"
                                                ValidationGroup="vgForm" />
                                        </div>
                                    </div>
                                    <!--/Comapny Mobile-->

                                    <!--Personal Mobile-->
                                    <div class="col-sm-4">
                                        <div class="input-block mb-3">
                                            <label class="col-form-label">Personal Mobile Number<span class="text-danger">*</span></label>
                                            <asp:TextBox ID="txtPersonalCelloNo" runat="server" CssClass="form-control" AutoCompleteType="Disabled" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                            <asp:RegularExpressionValidator
                                                ID="RegularExpressionValidator2"
                                                runat="server"
                                                ControlToValidate="txtPersonalCelloNo"
                                                ValidationExpression="^\d{10}$"
                                                ErrorMessage="10-digit mobile number without spaces."
                                                CssClass="text-danger"
                                                ValidationGroup="vgForm" />
                                        </div>
                                    </div>
                                    <!--/Personal Mobile-->

                                    <!--Present Address-->
                                    <div class="col-sm-4">
                                        <div class="input-block mb-3">
                                            <label class="col-form-label">Present Address<span class="text-danger">*</span></label>
                                            <asp:TextBox ID="txtPresentAddress" runat="server" CssClass="form-control" AutoCompleteType="Disabled" TextMode="MultiLine"></asp:TextBox>
                                            <asp:RequiredFieldValidator
                                                ID="RequiredFieldValidator13"
                                                runat="server"
                                                ControlToValidate="txtPresentAddress"
                                                ErrorMessage="Address is required."
                                                CssClass="text-danger"
                                                ValidationGroup="vgForm" />

                                            <asp:RegularExpressionValidator
                                                ID="RegularExpressionValidator3"
                                                runat="server"
                                                ControlToValidate="txtPresentAddress"
                                                ValidationExpression="^.{10,}$"
                                                ErrorMessage=" at least 10 characters ."
                                                CssClass="text-danger"
                                                ValidationGroup="vgForm" />
                                        </div>
                                    </div>
                                    <!--/Present Address-->

                                    <!--Permanent Address-->
                                    <div class="col-sm-4">
                                        <div class="input-block mb-3">
                                            <label class="col-form-label">Permanent Address<span class="text-danger">*</span></label>
                                            <asp:TextBox ID="txtPermanentAddress" runat="server" CssClass="form-control" AutoCompleteType="Disabled" TextMode="MultiLine"></asp:TextBox>
                                            <asp:RequiredFieldValidator
                                                ID="rfvReason"
                                                runat="server"
                                                ControlToValidate="txtPermanentAddress"
                                                ErrorMessage="Address is required."
                                                CssClass="text-danger"
                                                ValidationGroup="vgForm" />

                                            <asp:RegularExpressionValidator
                                                ID="revReasonLength"
                                                runat="server"
                                                ControlToValidate="txtPermanentAddress"
                                                ValidationExpression="^.{10,}$"
                                                ErrorMessage="at least 10 characters."
                                                CssClass="text-danger"
                                                ValidationGroup="vgForm" />
                                        </div>
                                    </div>
                                    <!--/Permanent Address-->
                                </div>
                            </div>
                            <!--/Contact Details Tab -->

                            <!-- Salary Details Tab -->
                            <div class="tab-pane fade" id="v-pills-Salary" role="tabpanel" aria-labelledby="v-pills-Salary-tab">
                                <div class="row">

                                    <!--Bank Name -->
                                    <div class="col-sm-4">
                                        <div class="input-block mb-3">
                                            <label class="col-form-label">Bank Name<span class="text-danger">*</span></label>
                                            <asp:TextBox ID="txtBankName" runat="server" CssClass="form-control" AutoCompleteType="Disabled" onkeypress="return isCharacterKey(event)"></asp:TextBox>
                                            <asp:RequiredFieldValidator
                                                ID="RequiredFieldValidator14"
                                                runat="server"
                                                ControlToValidate="txtBankName"
                                                ErrorMessage="BankName is required."
                                                CssClass="text-danger"
                                                ValidationGroup="vgForm" />
                                        </div>
                                    </div>
                                    <!--/Bank Name -->

                                    <!--Bank Acc Number-->
                                    <div class="col-sm-4">
                                        <div class="input-block mb-3">
                                            <label class="col-form-label">Bank A/c Number<span class="text-danger">*</span></label>
                                            <asp:TextBox ID="txtBankAccNo" runat="server" CssClass="form-control" AutoCompleteType="Disabled" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                            <asp:RequiredFieldValidator
                                                ID="RequiredFieldValidator15"
                                                runat="server"
                                                ControlToValidate="txtBankAccNo"
                                                ErrorMessage="AccountNumber is required."
                                                CssClass="text-danger"
                                                ValidationGroup="vgForm" />
                                        </div>
                                    </div>
                                    <!--/Bank Acc Number-->

                                    <!--IFSC -->
                                    <div class="col-sm-4">
                                        <div class="input-block mb-3">
                                            <label class="col-form-label">IFSC<span class="text-danger">*</span></label>
                                            <asp:TextBox ID="txtIFSC" runat="server" CssClass="form-control" AutoCompleteType="Disabled"></asp:TextBox>
                                            <asp:RegularExpressionValidator
                                                ID="revIFSC"
                                                runat="server"
                                                ControlToValidate="txtIFSC"
                                                ErrorMessage="Format must be 4 uppercase letters followed by 7 digits."
                                                ValidationExpression="^[A-Z]{4}[0-9]{7}$"
                                                CssClass="text-danger"></asp:RegularExpressionValidator>
                                        </div>
                                    </div>
                                    <!--/IFSC-->

                                    <!--PF Status-->
                                    <div class="col-sm-4">
                                        <div class="input-block mb-3">
                                            <label class="col-form-label">PF Status<span class="text-danger">*</span></label>
                                            <asp:DropDownList ID="ddlpfstatus" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlpfstatus_SelectedIndexChanged" onfocus="ddladdpfstatusdisable()">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator
                                                ID="RequiredFieldValidator16"
                                                runat="server"
                                                ControlToValidate="ddlpfstatus"
                                                InitialValue="0"
                                                ErrorMessage="Please select PF Status."
                                                CssClass="text-danger"
                                                ValidationGroup="vgForm" />
                                        </div>
                                    </div>
                                    <!--/PF Status-->

                                    <!--PF Type-->
                                    <div class="col-sm-4">
                                        <div class="input-block mb-3">
                                            <label class="col-form-label">PF Type<span class="text-danger">*</span></label>
                                            <asp:DropDownList ID="ddlpfnpf" runat="server" CssClass="form-control" onfocus="ddladdpftypedisable()">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator
                                                ID="RequiredFieldValidator17"
                                                runat="server"
                                                ControlToValidate="ddlpfnpf"
                                                InitialValue="0"
                                                ErrorMessage="Please select PF Type."
                                                CssClass="text-danger"
                                                ValidationGroup="vgForm" />
                                        </div>
                                    </div>
                                    <!--/PF Type-->

                                    <!--PF Number-->
                                    <div class="col-sm-4" id="pfnumberdiv" runat="server">
                                        <div class="input-block mb-3">
                                            <label class="col-form-label">PF Number<span class="text-danger"></span></label>
                                            <asp:TextBox ID="txtPfNo" runat="server" CssClass="form-control" AutoCompleteType="Disabled" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                        </div>
                                    </div>
                                    <!--/PF Number-->

                                    <!--ESI Number-->
                                    <div class="col-sm-4" id="esinumberdiv" runat="server">
                                        <div class="input-block mb-3">
                                            <label class="col-form-label">ESI Number<span class="text-danger"></span></label>
                                            <asp:TextBox ID="txtEsino" runat="server" CssClass="form-control" AutoCompleteType="Disabled" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                        </div>
                                    </div>
                                    <!--/IFSC-->

                                    <!--Pan Number-->
                                    <div class="col-sm-4">
                                        <div class="input-block mb-3">
                                            <label class="col-form-label">PAN Number<span class="text-danger">*</span></label>
                                            <asp:TextBox ID="txtPanNo" runat="server" CssClass="form-control" AutoCompleteType="Disabled"></asp:TextBox>
                                            <asp:RegularExpressionValidator
                                                ID="revPanNo"
                                                runat="server"
                                                ControlToValidate="txtPanNo"
                                                ErrorMessage="Format must be 5 uppercase letters, followed by 4 digits, and 1 uppercase letter."
                                                ValidationExpression="^[A-Z]{5}[0-9]{4}[A-Z]{1}$"
                                                CssClass="text-danger" ValidationGroup="vgForm"></asp:RegularExpressionValidator>
                                        </div>
                                    </div>
                                    <!--/Pan Number-->

                                    <!--ExpextedSalary-->
                                    <div class="col-sm-4">
                                        <div class="input-block mb-3">
                                            <label class="col-form-label">Expt Salary<span class="text-danger">*</span></label>
                                            <asp:TextBox ID="txtaddexpectedsalary" runat="server" CssClass="form-control" AutoCompleteType="Disabled" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                            <asp:CustomValidator
                                                ID="cvSalary"
                                                runat="server"
                                                ControlToValidate="txtaddexpectedsalary"
                                                ClientValidationFunction="validateSalary"
                                                ErrorMessage="The salary must be greater than 24,000."
                                                CssClass="text-danger" ValidationGroup="vgForm"></asp:CustomValidator>
                                        </div>
                                    </div>
                                    <!--/ExpextedSalary-->

                                    <!--SalaryAnnum-->
                                    <div class="col-sm-4">
                                        <div class="input-block mb-3">
                                            <label class="col-form-label">Salary per Annum<span class="text-danger">*</span></label>
                                            <asp:TextBox ID="txtSalaryAnnum" runat="server" CssClass="form-control" AutoCompleteType="Disabled" onkeypress="return isNumberKey(event)" AutoPostBack="true" OnTextChanged="txtSalaryAnnum_TextChanged"></asp:TextBox>
                                            <asp:CustomValidator
                                                ID="CustomValidator1"
                                                runat="server"
                                                ControlToValidate="txtSalaryAnnum"
                                                ClientValidationFunction="validateSalary"
                                                ErrorMessage="The salary must be greater than 24,000."
                                                CssClass="text-danger" ValidationGroup="vgForm"></asp:CustomValidator>
                                        </div>
                                    </div>
                                    <!--/SalaryAnnum-->

                                    <!--/SalaryMonth-->
                                    <div class="col-sm-4">
                                        <div class="input-block mb-3">
                                            <label class="col-form-label">Salary Per Month<span class="text-danger"></span></label>
                                            <asp:TextBox ID="txtSalaryMonth" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                        </div>
                                    </div>
                                    <!--/SalaryMonth-->
                                </div>
                            </div>
                            <!--/Salary Details Tab -->

                            <!-- Education Details Tab -->
                            <div class="tab-pane fade" id="v-pills-Education" role="tabpanel" aria-labelledby="v-pills-Contact-tab">
                                <div class="row">
                                    <!--High Education-->
                                    <div class="col-sm-3">
                                        <div class="input-block mb-3">
                                            <label class="col-form-label">Highest Education 1<span class="text-danger">*</span></label>
                                            <asp:TextBox ID="txtHighEducation1" runat="server" CssClass="form-control" AutoCompleteType="Disabled"></asp:TextBox>
                                            <asp:RequiredFieldValidator
                                                ID="RequiredFieldValidator18"
                                                runat="server"
                                                ControlToValidate="txtHighEducation1"
                                                ErrorMessage="Education is required."
                                                CssClass="text-danger"
                                                ValidationGroup="vgForm" />
                                        </div>
                                    </div>
                                    <!--/High Education-->

                                    <!--University-->
                                    <div class="col-sm-3">
                                        <div class="input-block mb-3">
                                            <label class="col-form-label">University<span class="text-danger">*</span></label>
                                            <asp:TextBox ID="txtEducationUniversity1" runat="server" CssClass="form-control" onkeypress="return isCharacterKey(event)" AutoCompleteType="Disabled"></asp:TextBox>
                                            <asp:RequiredFieldValidator
                                                ID="RequiredFieldValidator19"
                                                runat="server"
                                                ControlToValidate="txtEducationUniversity1"
                                                ErrorMessage="Education is required."
                                                CssClass="text-danger"
                                                ValidationGroup="vgForm" />
                                        </div>
                                    </div>
                                    <!--/University-->

                                    <!--Year-->
                                    <div class="col-sm-3">
                                        <div class="input-block mb-3">
                                            <label class="col-form-label">Year<span class="text-danger">*</span></label>
                                            <asp:TextBox ID="txtEducationYear1" runat="server" CssClass="form-control" onkeypress="return isNumberKey(event)" AutoCompleteType="Disabled"></asp:TextBox>
                                            <asp:RequiredFieldValidator
                                                ID="rfvEducationYear1"
                                                runat="server"
                                                ControlToValidate="txtEducationYear1"
                                                ErrorMessage="Year is required."
                                                CssClass="text-danger"
                                                ValidationGroup="vgForm" />

                                            <asp:RegularExpressionValidator
                                                ID="revEducationYear1"
                                                runat="server"
                                                ControlToValidate="txtEducationYear1"
                                                ValidationExpression="^\d{4}$"
                                                ErrorMessage="Please enter a valid 4-digit year."
                                                CssClass="text-danger"
                                                ValidationGroup="vgForm" />
                                        </div>
                                    </div>
                                    <!--/Year-->

                                    <!--Percentage-->
                                    <div class="col-sm-3">
                                        <div class="input-block mb-3">
                                            <label class="col-form-label">Percentage<span class="text-danger"></span></label>
                                            <asp:TextBox ID="txt1educationpercentage" runat="server" CssClass="form-control" onkeypress="return isNumberKey(event)" AutoCompleteType="Disabled"></asp:TextBox>

                                        </div>
                                    </div>
                                    <!--/Percentage-->

                                    <!--High Education2-->
                                    <div class="col-sm-3">
                                        <div class="input-block mb-3">
                                            <label class="col-form-label">Highest Education2<span class="text-danger"></span></label>
                                            <asp:TextBox ID="txtHighEducation2" runat="server" CssClass="form-control" AutoCompleteType="Disabled"></asp:TextBox>
                                        </div>
                                    </div>
                                    <!--/High Education2-->

                                    <!--University2-->
                                    <div class="col-sm-3">
                                        <div class="input-block mb-3">
                                            <label class="col-form-label">University2<span class="text-danger"></span></label>
                                            <asp:TextBox ID="txtEducationUniversity2" runat="server" CssClass="form-control" onkeypress="return isCharacterKey(event)" AutoCompleteType="Disabled"></asp:TextBox>
                                        </div>
                                    </div>
                                    <!--/University2-->

                                    <!--Year2-->
                                    <div class="col-sm-3">
                                        <div class="input-block mb-3">
                                            <label class="col-form-label">Year2<span class="text-danger"></span></label>
                                            <asp:TextBox ID="txtEducationYear2" runat="server" CssClass="form-control" AutoCompleteType="Disabled" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                        </div>
                                    </div>
                                    <!--/Year2-->

                                    <!--Percentage2-->
                                    <div class="col-sm-3">
                                        <div class="input-block mb-3">
                                            <label class="col-form-label">Percentage2<span class="text-danger"></span></label>
                                            <asp:TextBox ID="txt2educationpercentage" runat="server" CssClass="form-control" AutoCompleteType="Disabled"></asp:TextBox>
                                        </div>
                                    </div>
                                    <!--/Percentage2-->
                                </div>

                            </div>
                            <!--/Education Details Tab -->

                            <!-- Family Details Tab -->
                            <div class="tab-pane fade" id="v-pills-Family" role="tabpanel" aria-labelledby="v-pills-Family-tab">
                                <div class="row">
                                    <!--Marritial Status-->
                                    <div class="col-sm-6">
                                        <div class="input-block mb-3">
                                            <label class="col-form-label">Marritial Status<span class="text-danger">*</span></label>
                                            <asp:DropDownList ID="ddlmaritialstatus" runat="server" CssClass="form-control" onfocus="disablemaritialstatus()" AutoPostBack="true" OnSelectedIndexChanged="ddlmaritialstatus_SelectedIndexChanged">
                                                <asp:ListItem> -- Select -- </asp:ListItem>
                                                <asp:ListItem>Single</asp:ListItem>
                                                <asp:ListItem>Married</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator
                                                ID="RequiredFieldValidator20"
                                                runat="server"
                                                ControlToValidate="ddlmaritialstatus"
                                                InitialValue="0"
                                                ErrorMessage="Please select Marritial Status."
                                                CssClass="text-danger"
                                                ValidationGroup="vgForm" />
                                        </div>
                                    </div>
                                    <!--/Marritial Status-->

                                    <!--Spouse-->
                                    <div class="col-sm-6" id="spoucediv" runat="server">
                                        <div class="input-block mb-3">
                                            <label class="col-form-label">Spouse Name<span class="text-danger">*</span></label>
                                            <asp:TextBox ID="txtspousename" runat="server" CssClass="form-control" AutoCompleteType="Disabled" onkeypress="return isCharacterKey(event)"></asp:TextBox>
                                        </div>
                                    </div>
                                    <!--/Spouse-->

                                    <!--Name-->
                                    <div class="col-sm-3">
                                        <div class="input-block mb-3">
                                            <label class="col-form-label">Name(Family Information)<span class="text-danger">*</span></label>
                                            <asp:TextBox ID="txtFatherName" runat="server" CssClass="form-control" AutoCompleteType="Disabled" onkeypress="return isCharacterKey(event)"></asp:TextBox>
                                        </div>
                                    </div>
                                    <!--/Name-->

                                    <!--Relation-->
                                    <div class="col-sm-3">
                                        <div class="input-block mb-3">
                                            <label class="col-form-label">Relation<span class="text-danger">*</span></label>
                                            <asp:TextBox ID="txtRelation1" runat="server" CssClass="form-control" AutoCompleteType="Disabled" onkeypress="return isCharacterKey(event)"></asp:TextBox>
                                        </div>
                                    </div>
                                    <!--/Relation-->

                                    <!--Aadhar-->
                                    <div class="col-sm-3">
                                        <div class="input-block mb-3">
                                            <label class="col-form-label">Aadhar Number<span class="text-danger">*</span></label>
                                            <asp:TextBox ID="txtFatherAadhar" runat="server" CssClass="form-control" AutoCompleteType="Disabled" onkeypress="return isNumberKey(event)"></asp:TextBox>

                                        </div>
                                    </div>
                                    <!--/Aadhar-->

                                    <!--Mobile-->
                                    <div class="col-sm-3">
                                        <div class="input-block mb-3">
                                            <label class="col-form-label">Mobile<span class="text-danger"></span></label>
                                            <asp:TextBox ID="txtFatherMobile" runat="server" CssClass="form-control" AutoCompleteType="Disabled" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                        </div>
                                    </div>
                                    <!--/Mobile-->

                                    <!--/Name-->
                                    <div class="col-sm-3">
                                        <div class="input-block mb-3">
                                            <label class="col-form-label">Name(Family Information)<span class="text-danger"></span></label>
                                            <asp:TextBox ID="txtMotherName" runat="server" CssClass="form-control" AutoCompleteType="Disabled" onkeypress="return isCharacterKey(event)"></asp:TextBox>
                                        </div>
                                    </div>
                                    <!--/Name-->

                                    <!--Relation-->
                                    <div class="col-sm-3">
                                        <div class="input-block mb-3">
                                            <label class="col-form-label">Relation<span class="text-danger"></span></label>
                                            <asp:TextBox ID="txtRelation2" runat="server" CssClass="form-control" AutoCompleteType="Disabled" onkeypress="return isCharacterKey(event)"></asp:TextBox>
                                        </div>
                                    </div>
                                    <!--/Relation-->

                                    <!--Aadhar-->
                                    <div class="col-sm-3">
                                        <div class="input-block mb-3">
                                            <label class="col-form-label">Aadhar Number<span class="text-danger"></span></label>
                                            <asp:TextBox ID="txtMotherAdhar" runat="server" CssClass="form-control" AutoCompleteType="Disabled" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                        </div>
                                    </div>
                                    <!--/Aadhar-->

                                    <!--Mobile-->
                                    <div class="col-sm-3">
                                        <div class="input-block mb-3">
                                            <label class="col-form-label">Mobile<span class="text-danger"></span></label>
                                            <asp:TextBox ID="txtMotherMobile" runat="server" CssClass="form-control" AutoCompleteType="Disabled" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                        </div>
                                    </div>
                                    <!--/Mobile-->



                                    <!--NomineeName-->
                                    <div class="col-sm-3">
                                        <div class="input-block mb-3">
                                            <label class="col-form-label">Nominee Name<span class="text-danger"></span></label>
                                            <asp:TextBox ID="txtnominename" runat="server" CssClass="form-control" onkeypress="return isCharacterKey(event)" AutoCompleteType="Disabled"></asp:TextBox>
                                            <asp:RequiredFieldValidator
                                                ID="RequiredFieldValidator21"
                                                runat="server"
                                                ControlToValidate="txtnominename"
                                                ErrorMessage="Nominee is required."
                                                CssClass="text-danger"
                                                ValidationGroup="vgForm" />

                                        </div>
                                    </div>
                                    <!--/NomineeName-->

                                    <!--NomineeNameRelation-->
                                    <div class="col-sm-3">
                                        <div class="input-block mb-3">
                                            <label class="col-form-label">Nominee Relation<span class="text-danger">*</span></label>
                                            <asp:TextBox ID="txtnominerelation" runat="server" CssClass="form-control" onkeypress="return isCharacterKey(event)" AutoCompleteType="Disabled"></asp:TextBox>
                                            <asp:RequiredFieldValidator
                                                ID="RequiredFieldValidator22"
                                                runat="server"
                                                ControlToValidate="txtnominename"
                                                ErrorMessage="Nominee Relation is required."
                                                CssClass="text-danger"
                                                ValidationGroup="vgForm" />
                                        </div>
                                    </div>
                                    <!--/NomineeNameRelation-->

                                    <!--NimonieeAadhar-->
                                    <div class="col-sm-3">
                                        <div class="input-block mb-3">
                                            <label class="col-form-label">Nimoniee Aadhar<span class="text-danger">*</span></label>
                                            <asp:TextBox ID="txtnomineeadhar" runat="server" CssClass="form-control" AutoCompleteType="Disabled" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                            <asp:RequiredFieldValidator
                                                ID="rfvFatherAadhar"
                                                runat="server"
                                                ControlToValidate="txtnomineeadhar"
                                                ErrorMessage="Aadhar number is required."
                                                CssClass="text-danger"
                                                ValidationGroup="vgForm" />

                                            <asp:RegularExpressionValidator
                                                ID="revFatherAadhar"
                                                runat="server"
                                                ControlToValidate="txtnomineeadhar"
                                                ValidationExpression="^\d{12}$"
                                                ErrorMessage="enter 12-digit Aadhar."
                                                CssClass="text-danger"
                                                ValidationGroup="vgForm" />
                                        </div>
                                    </div>
                                    <!--/NimonieeAadhar-->

                                    <!--NomineeMobileNumber-->
                                    <div class="col-sm-3">
                                        <div class="input-block mb-3">
                                            <label class="col-form-label">Nominee MobileNumber<span class="text-danger">*</span></label>
                                            <asp:TextBox ID="txtnomineemobile" runat="server" CssClass="form-control" AutoCompleteType="Disabled" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                            <asp:RequiredFieldValidator
                                                ID="RequiredFieldValidator23"
                                                runat="server"
                                                ControlToValidate="txtnomineemobile"
                                                ErrorMessage="Mobile number is required."
                                                CssClass="text-danger"
                                                ValidationGroup="vgForm" />

                                            <asp:RegularExpressionValidator
                                                ID="RegularExpressionValidator4"
                                                runat="server"
                                                ControlToValidate="txtnomineemobile"
                                                ValidationExpression="^\d{12}$"
                                                ErrorMessage="enter 10-digit Mobile."
                                                CssClass="text-danger"
                                                ValidationGroup="vgForm" />
                                        </div>
                                    </div>
                                    <!--/NomineeMobileNumber-->
                                    <!--Chaild1Name-->
                                    <div class="col-sm-4" id="chaild1div" runat="server">
                                        <div class="input-block mb-3">
                                            <label class="col-form-label">Chaild1Name<span class="text-danger"></span></label>
                                            <asp:TextBox ID="TextBox33" runat="server" CssClass="form-control" AutoCompleteType="Disabled" onkeypress="return isCharacterKey(event)"></asp:TextBox>
                                        </div>
                                    </div>
                                    <!--/Chaild1Name-->

                                    <!--Chaild2Name-->
                                    <div class="col-sm-4" id="chaild2div" runat="server">
                                        <div class="input-block mb-3">
                                            <label class="col-form-label">Chaild2Name<span class="text-danger"></span></label>
                                            <asp:TextBox ID="TextBox34" runat="server" CssClass="form-control" AutoCompleteType="Disabled" onkeypress="return isCharacterKey(event)"></asp:TextBox>
                                        </div>
                                    </div>
                                    <!--/Chaild2Name-->
                                </div>
                            </div>
                            <!--/Family Details Tab -->

                            <!-- Previous company Tab -->
                            <div class="tab-pane fade" id="v-pills-Previous" role="tabpanel" aria-labelledby="v-pills-Previous-tab">
                                <div class="row">
                                    <asp:HiddenField ID="hfActiveTab" runat="server" />
                                    <!--EmpExType-->
                                    <div class="col-sm-4">
                                        <div class="input-block mb-3">
                                            <label class="col-form-label">EmpExType<span class="text-danger">*</span></label>
                                            <asp:DropDownList ID="ddlempextype" runat="server" CssClass="form-control" AutoCompleteType="Disabled" AutoPostBack="true" OnSelectedIndexChanged="ddlempextype_SelectedIndexChanged" onfocus="disableddlextype()">
                                                <asp:ListItem> -- Select Type -- </asp:ListItem>
                                                <asp:ListItem>Fresher</asp:ListItem>
                                                <asp:ListItem>Experience</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator
                                                ID="RequiredFieldValidator24"
                                                runat="server"
                                                ControlToValidate="ddlempextype"
                                                InitialValue="0"
                                                ErrorMessage="Please select Emp Ex Type."
                                                CssClass="text-danger"
                                                ValidationGroup="vgForm" />
                                        </div>
                                    </div>
                                    <!--/EmpExType-->

                                    <!--Expirience-->
                                    <div class="col-sm-4" id="exyearsdiv" runat="server">
                                        <div class="input-block mb-3">
                                            <label class="col-form-label">Expirience<span class="text-danger">*</span></label>
                                            <asp:TextBox ID="txtexpirienceyears" runat="server" CssClass="form-control" AutoCompleteType="Disabled"></asp:TextBox>
                                        </div>
                                    </div>
                                    <!--/Expirience-->

                                    <!--LastCompanyName-->
                                    <div class="col-sm-4" id="exlast1compdiv" runat="server">
                                        <div class="input-block mb-3">
                                            <label class="col-form-label">LastCompany Name<span class="text-danger">*</span></label>
                                            <asp:TextBox ID="txtlast1company" runat="server" CssClass="form-control" AutoCompleteType="Disabled" onkeypress="return isCharacterKey(event)"></asp:TextBox>
                                        </div>
                                    </div>
                                    <!--/LastCompanyName-->

                                    <!--LastCompanyDesignation-->
                                    <div class="col-sm-4" id="exlast1desidiv" runat="server">
                                        <div class="input-block mb-3">
                                            <label class="col-form-label">LastCompany Designation<span class="text-danger">*</span></label>
                                            <asp:TextBox ID="txtlast1designation" runat="server" CssClass="form-control" AutoCompleteType="Disabled" onkeypress="return isCharacterKey(event)"></asp:TextBox>
                                        </div>
                                    </div>
                                    <!--/LastCompanyDesignation-->

                                    <!--LastCompanyFromYear-->
                                    <div class="col-sm-4" id="exlast1compfromyeardiv" runat="server">
                                        <div class="input-block mb-3">
                                            <label class="col-form-label">LastCompany FromYear<span class="text-danger">*</span></label>
                                            <asp:TextBox ID="txtlast1fromyear" runat="server" CssClass="form-control" AutoCompleteType="Disabled" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                        </div>
                                    </div>
                                    <!--/LastCompanyFromYear-->

                                    <!--LastCompanyToYear-->
                                    <div class="col-sm-4" id="exlast1comptoyeardiv" runat="server">
                                        <div class="input-block mb-3">
                                            <label class="col-form-label">LastCompanyToYear<span class="text-danger">*</span></label>
                                            <asp:TextBox ID="txtlast1toyear" runat="server" CssClass="form-control" AutoCompleteType="Disabled" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                        </div>
                                    </div>
                                    <!--/LastCompanyToYear-->

                                    <!--LastCompanySalaryAnnum-->
                                    <div class="col-sm-4" id="exlast1compsalaryannumdiv" runat="server">
                                        <div class="input-block mb-3">
                                            <label class="col-form-label">LastCompany Salary per Annum<span class="text-danger">*</span></label>
                                            <asp:TextBox ID="txtlast1salaryannum" runat="server" CssClass="form-control" AutoCompleteType="Disabled" onkeypress="return isNumberKey(event)" AutoPostBack="true" OnTextChanged="txtlast1salaryannum_TextChanged"></asp:TextBox>
                                        </div>
                                    </div>
                                    <!--/LastCompanySalaryAnnum-->

                                    <!--LastCompanySalaryMonth-->
                                    <div class="col-sm-4" id="exlast1compsalarymonthdiv" runat="server">
                                        <div class="input-block mb-3">
                                            <label class="col-form-label">LastCompany Salary per Month<span class="text-danger">*</span></label>
                                            <asp:TextBox ID="txtlast1salarymonth" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                        </div>
                                    </div>
                                    <!--/LastCompanySalaryMonth-->

                                    <!--LastCompanyMobileNumber-->
                                    <div class="col-sm-4" id="exlast1compmobilediv" runat="server">
                                        <div class="input-block mb-3">
                                            <label class="col-form-label">LastCompany MobileNumber<span class="text-danger">*</span></label>
                                            <asp:TextBox ID="txtlast1mobile" runat="server" CssClass="form-control" AutoCompleteType="Disabled" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                        </div>
                                    </div>
                                    <!--/LastCompanyMobileNumber-->

                                    <!--Last2CompanyName-->
                                    <div class="col-sm-4" id="exlast2compdiv" runat="server">
                                        <div class="input-block mb-3">
                                            <label class="col-form-label">Last2 CompanyName<span class="text-danger">*</span></label>
                                            <asp:TextBox ID="txtlast2companyname" runat="server" CssClass="form-control"  onkeypress="return isCharacterKey(event)" AutoCompleteType="Disabled"></asp:TextBox>
                                        </div>
                                    </div>
                                    <!--/Last2CompanyName-->

                                    <!--Last2CompanyDesignation-->
                                    <div class="col-sm-4" id="exlast2compdesidiv" runat="server">
                                        <div class="input-block mb-3">
                                            <label class="col-form-label">Last2CompanyDesignation<span class="text-danger">*</span></label>
                                            <asp:TextBox ID="txtlast2designation" runat="server" CssClass="form-control" onkeypress="return isCharacterKey(event)" AutoCompleteType="Disabled"></asp:TextBox>
                                        </div>
                                    </div>
                                    <!--/Last2CompanyDesignation-->

                                    <!--Last2CompanyFromYear-->
                                    <div class="col-sm-4" id="exlast2compfromyeardiv" runat="server">
                                        <div class="input-block mb-3">
                                            <label class="col-form-label">Last2CompanyFromYear<span class="text-danger">*</span></label>
                                            <asp:TextBox ID="txtlast2fromyear" runat="server" CssClass="form-control" onkeypress="return isNumberKey(event)" AutoCompleteType="Disabled"></asp:TextBox>
                                        </div>
                                    </div>
                                    <!--/Last2CompanyFromYear-->

                                    <!--Last2CompanyToYear-->
                                    <div class="col-sm-4" id="exlast2comptoyeardiv" runat="server">
                                        <div class="input-block mb-3">
                                            <label class="col-form-label">Last2CompanyToYear<span class="text-danger">*</span></label>
                                            <asp:TextBox ID="txtlast2toyear" runat="server" CssClass="form-control" onkeypress="return isNumberKey(event)" AutoCompleteType="Disabled"></asp:TextBox>
                                        </div>
                                    </div>
                                    <!--/Last2CompanyToYear-->

                                    <!--Last2CompanySalaryAnnum-->
                                    <div class="col-sm-4" id="exlast2compsalaryannumdiv" runat="server">
                                        <div class="input-block mb-3">
                                            <label class="col-form-label">Last2CompanySalaryAnnum<span class="text-danger">*</span></label>
                                            <asp:TextBox ID="txtlast2companysalaryannum" runat="server" CssClass="form-control" onkeypress="return isNumberKey(event)" AutoCompleteType="Disabled"></asp:TextBox>
                                        </div>
                                    </div>
                                    <!--/Last2CompanySalaryAnnum-->

                                    <!--Last2CompanySalaryMonth-->
                                    <div class="col-sm-4" id="exlast2compsalarymonthdiv" runat="server">
                                        <div class="input-block mb-3">
                                            <label class="col-form-label">Last2CompanySalaryMonth<span class="text-danger">*</span></label>
                                            <asp:TextBox ID="txtlast2companysalarymonth" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                        </div>
                                    </div>
                                    <!--/Last2CompanySalaryMonth-->

                                    <!--Last2CompanyMobileNumber-->
                                    <div class="col-sm-4" id="exlast2compmobilehdiv" runat="server">
                                        <div class="input-block mb-3">
                                            <label class="col-form-label">Last2CompanyMobileNumber<span class="text-danger">*</span></label>
                                            <asp:TextBox ID="txtlast2companymobile" runat="server" CssClass="form-control" AutoCompleteType="Disabled" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                        </div>
                                    </div>
                                    <!--/Last2CompanyMobileNumber-->

                                    <!--Image-->
                                    <div class="col-sm-4">
                                        <div class="input-block mb-3">
                                            <label class="col-form-label">Image<span class="text-danger">*</span></label>
                                            <asp:FileUpload ID="addimage" runat="server" CssClass="form-control" />

                                        </div>
                                    </div>
                                    <!--/Image-->
                                </div>
                                <div class="submit-section">
                                    <asp:Button ID="btnaddemployee" runat="server" Text="Add Employee" CssClass="btn btn-primary submit-btn" OnClick="btnaddemployee_Click" ValidationGroup="vgForm" />
                                </div>
                            </div>
                            <!--/Previous Details Tab -->
                        </div>
                    </div>
                </div>
                <!-- /Add Employee Modal -->
            </div>
        </div>
    </div>
    <script type="text/javascript">
        function disableddlsalutation() {
            var ddl = document.getElementById('<%= ddlsalutation.ClientID %>');
            ddl.options[0].disabled = true;
        }
        function disableddlgender() {
            var ddl = document.getElementById('<%= ddlgender.ClientID %>');
            ddl.options[0].disabled = true;
        }

        function ddladdcompanydisable() {
            var ddl = document.getElementById('<%= ddladdcompany.ClientID %>');
            ddl.options[0].disabled = true;
        }
        function ddladdbranchdisable() {
            var ddl = document.getElementById('<%= ddladdbranch.ClientID %>');
            ddl.options[0].disabled = true;
        }
        function ddladddepartmentdisable() {
            var ddl = document.getElementById('<%= ddladddepartment.ClientID %>');
            ddl.options[0].disabled = true;
        }
        function disableddlemptype() {
            var ddl = document.getElementById('<%= ddlemptype.ClientID %>');
            ddl.options[0].disabled = true;
        }
        function disableddbloodgroup() {
            var ddl = document.getElementById('<%= ddlbloodgroup.ClientID %>');
            ddl.options[0].disabled = true;
        }
        function ddladdpftypedisable() {
            var ddl = document.getElementById('<%= ddlpfnpf.ClientID %>');
            ddl.options[0].disabled = true;
        }
        function ddladdpfstatusdisable() {
            var ddl = document.getElementById('<%= ddlpfstatus.ClientID %>');
            ddl.options[0].disabled = true;
        }
        function ddladdshiftdisable() {
            var ddl = document.getElementById('<%= ddladdshifttype.ClientID %>');
            ddl.options[0].disabled = true;
        }
        function disablemaritialstatus() {
            var ddl = document.getElementById('<%= ddlmaritialstatus.ClientID %>');
            ddl.options[0].disabled = true;
        }
        function disableddlextype() {
            var ddl = document.getElementById('<%= ddlempextype.ClientID %>');
            ddl.options[0].disabled = true;
        }
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }
        function isCharacterKey(event) {
            var charCode = (event.which) ? event.which : event.keyCode;
            // Allow: A-Z, a-z, and space (charCode 32 is space)
            if ((charCode >= 65 && charCode <= 90) || (charCode >= 97 && charCode <= 122) || charCode == 32) {

                return true;
            } else {

                return false;
            }
        }
    </script>
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script>
        $(document).ready(function () {
            $('a[data-bs-toggle="pill"]').on('shown.bs.tab', function (e) {
                var target = $(e.target).attr("href"); // Get the target tab
                $(target).css('opacity', '0').animate({ opacity: 1, left: "0" }, 500);
            });
        });
    </script>
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script>
        // Save the active tab on tab change
        $(document).ready(function () {
            $('a[data-bs-toggle="pill"]').on('shown.bs.tab', function (e) {
                var activeTab = $(e.target).attr('href'); // Get the href attribute of the active tab
                $('#<%= hfActiveTab.ClientID %>').val(activeTab); // Set the hidden field value
            });

            // Set the active tab from hidden field after postback
            var activeTab = $('#<%= hfActiveTab.ClientID %>').val();
            if (activeTab) {
                $('a[href="' + activeTab + '"]').tab('show');
            }
        });
    </script>
</asp:Content>
