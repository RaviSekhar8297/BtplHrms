<%@ Page Title="" Language="C#" MasterPageFile="~/Employee.Master" AutoEventWireup="true" CodeBehind="EmployeeAddEmployee.aspx.cs" Inherits="Human_Resource_Management.EmployeeAddEmployee" %>

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
                            <h3 class="page-title">Employee</h3>
                            <ul class="breadcrumb">
                                <li class="breadcrumb-item"><a href="Admindashboard.aspx">Dashboard</a></li>
                                <li class="breadcrumb-item active">Employee</li>
                            </ul>
                        </div>
                        <div class="col-auto float-end ms-auto">

                            <%--							<a href="" class="btn btn-primary w-100" data-bs-toggle="modal" data-bs-target="#add_contact"><i class="fa-solid fa-plus"></i> Add Contact</a>--%>
                            <div class="view-icons">

                                <a href="EmployeeContactDetails.aspx" class="list-view btn btn-link"><i class="fa-solid fa-bars"></i></a>
                                <%--  <a href="AdminAddEmployee.aspx" class="list-view btn btn-link active"><i class="fa-solid fa-plus"></i></a>--%>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- /Page Header -->

                <div class="row">

                    <asp:HiddenField ID="HiddenField2" runat="server" />
                    <div class="row">
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
                            </div>
                        </div>
                        <div class="col-sm-4">
                            <div class="input-block mb-3">
                                <label class="col-form-label">FirstName (surname) <span class="text-danger">*</span></label>
                                <asp:TextBox ID="txtaddsurname" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator222" runat="server" ControlToValidate="txtaddsurname"
                                    ErrorMessage="Required FirstName" ValidationGroup="employeeaddcompany" ForeColor="Red"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-sm-4">
                            <div class="input-block mb-3">
                                <label class="col-form-label">Name<span class="text-danger">*</span></label>
                                <asp:TextBox ID="txtaddlastname" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtaddlastname"
                                    ErrorMessage="Required Name" ValidationGroup="employeeaddcompany" ForeColor="Red"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-sm-4">
                            <div class="input-block mb-3">
                                <label class="col-form-label">Image<span class="text-danger">*</span></label>
                                <asp:FileUpload ID="addimage" runat="server" CssClass="form-control" />
                            </div>
                        </div>
                        <div class="col-sm-4">
                            <div class="input-block mb-3">
                                <label class="col-form-label">DOB</label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtadddob"
                                    ErrorMessage="Required DOB" ValidationGroup="employeeaddcompany" ForeColor="Red"></asp:RequiredFieldValidator>
                                <asp:TextBox ID="txtadddob" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>

                            </div>
                        </div>
                        <div class="col-sm-4">
                            <div class="input-block mb-3">
                                <label class="col-form-label">DOJ<span class="text-danger">*</span></label>
                                <asp:TextBox ID="txtadddoj" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-sm-4">
                            <div class="input-block mb-3">

                                <label class="col-form-label">Gender <span class="text-danger">*</span></label>
                                <asp:DropDownList ID="ddlgender" runat="server" CssClass="form-control" onfocus="disableddlgender()">
                                    <asp:ListItem> -- Select Gender -- </asp:ListItem>
                                    <asp:ListItem>Male</asp:ListItem>
                                    <asp:ListItem>FeMale</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="input-block mb-3">
                                <label class="col-form-label">Company <span class="text-danger">*</span></label>
                                <asp:DropDownList ID="ddladdcompany" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddladdcompany_SelectedIndexChanged" onfocus="ddladdcompanydisable()">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="input-block mb-3">
                                <label class="col-form-label">Branch <span class="text-danger">*</span></label>
                                <asp:DropDownList ID="ddladdbranch" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddladdbranch_SelectedIndexChanged" onfocus="ddladdbranchnamedisable()">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="input-block mb-3">
                                <label class="col-form-label">Department <span class="text-danger">*</span></label>
                                <asp:DropDownList ID="ddladddepartment" runat="server" CssClass="form-control" onfocus="ddladddepartmentnamedisable()">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="input-block mb-3">
                                <label class="col-form-label">Designation<span class="text-danger">*</span></label>
                                <asp:TextBox ID="TextBox4" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="input-block mb-3">
                                <label class="col-form-label">Employee Type<span class="text-danger">*</span></label>
                                <asp:DropDownList ID="ddlemptype" runat="server" CssClass="form-control" onfocus="disableddlemptype()">
                                    <asp:ListItem> -- Select Emp Type -- </asp:ListItem>
                                    <asp:ListItem Value="1">Permanent</asp:ListItem>
                                    <asp:ListItem Value="2">Prohibition</asp:ListItem>
                                    <asp:ListItem Value="3">Intern</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="input-block mb-3">
                                <label class="col-form-label">Comapny Email<span class="text-danger">*</span></label>
                                <asp:TextBox ID="TextBox5" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="input-block mb-3">
                                <label class="col-form-label">Personal Email<span class="text-danger">*</span></label>
                                <asp:TextBox ID="TextBox6" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="input-block mb-3">
                                <label class="col-form-label">Comapny Mobile<span class="text-danger">*</span></label>
                                <asp:TextBox ID="txtCompanyCellNo" runat="server" CssClass="form-control" onkeypress="return isNumberKey(event)"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="input-block mb-3">
                                <label class="col-form-label">Personal Mobile Number<span class="text-danger">*</span></label>
                                <asp:TextBox ID="txtPersonalCelloNo" runat="server" CssClass="form-control" onkeypress="return isNumberKey(event)"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="input-block mb-3">
                                <label class="col-form-label">Present Address<span class="text-danger">*</span></label>
                                <asp:TextBox ID="txtPresentAddress" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="input-block mb-3">
                                <label class="col-form-label">Permanent Address<span class="text-danger">*</span></label>
                                <asp:TextBox ID="txtPermanentAddress" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-sm-6">
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
                            </div>
                        </div>

                        <div class="col-sm-6">
                            <div class="input-block mb-3">
                                <label class="col-form-label">Bank Name<span class="text-danger">*</span></label>
                                <asp:TextBox ID="txtBankName" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="input-block mb-3">
                                <label class="col-form-label">Bank Acc Number<span class="text-danger">*</span></label>
                                <asp:TextBox ID="txtBankAccNo" runat="server" CssClass="form-control" onkeypress="return isNumberKey(event)"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="input-block mb-3">
                                <label class="col-form-label">IFSC<span class="text-danger">*</span></label>
                                <asp:TextBox ID="txtIFSC" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="input-block mb-3">
                                <label class="col-form-label">PF Status<span class="text-danger">*</span></label>
                                <asp:DropDownList ID="ddlpfstatus" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlpfstatus_SelectedIndexChanged" onfocus="ddladdpfstatusdisable()">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="input-block mb-3">
                                <label class="col-form-label">PF Type<span class="text-danger">*</span></label>
                                <asp:DropDownList ID="ddlpfnpf" runat="server" CssClass="form-control" onfocus="ddladdpftypedisable()">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-sm-6" id="pfnumberdiv" runat="server">
                            <div class="input-block mb-3">
                                <label class="col-form-label">PF Number<span class="text-danger">*</span></label>
                                <asp:TextBox ID="txtPfNo" runat="server" CssClass="form-control" onkeypress="return isNumberKey(event)"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-sm-6" id="esinumberdiv" runat="server">
                            <div class="input-block mb-3">
                                <label class="col-form-label">ESI Number<span class="text-danger">*</span></label>
                                <asp:TextBox ID="txtEsino" runat="server" CssClass="form-control" onkeypress="return isNumberKey(event)"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="input-block mb-3">
                                <label class="col-form-label">Pan Number<span class="text-danger">*</span></label>
                                <asp:TextBox ID="txtPanNo" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="input-block mb-3">
                                <label class="col-form-label">ShiftType<span class="text-danger">*</span></label>
                                <asp:DropDownList ID="ddladdshifttype" runat="server" CssClass="form-control" onfocus="ddladdshiftdisable()" AutoPostBack="true" OnSelectedIndexChanged="ddladdshifttype_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="input-block mb-3">
                                <label class="col-form-label">Shift<span class="text-danger">*</span></label>
                                <asp:TextBox ID="txtaddshift1" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="input-block mb-3">
                                <label class="col-form-label">Shift2<span class="text-danger">*</span></label>
                                <asp:TextBox ID="txtaddshift2" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="input-block mb-3">
                                <label class="col-form-label">Shift3<span class="text-danger">*</span></label>
                                <asp:TextBox ID="txtaddshift3" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-sm-6">
                            <div class="input-block mb-3">
                                <label class="col-form-label">High Education<span class="text-danger">*</span></label>
                                <asp:TextBox ID="txtHighEducation1" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="input-block mb-3">
                                <label class="col-form-label">University<span class="text-danger">*</span></label>
                                <asp:TextBox ID="txtEducationUniversity1" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="input-block mb-3">
                                <label class="col-form-label">Year<span class="text-danger">*</span></label>
                                <asp:TextBox ID="txtEducationYear1" runat="server" CssClass="form-control" onkeypress="return isNumberKey(event)"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="input-block mb-3">
                                <label class="col-form-label">Percentage<span class="text-danger">*</span></label>
                                <asp:TextBox ID="txt1educationpercentage" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="input-block mb-3">
                                <label class="col-form-label">High Education2<span class="text-danger">*</span></label>
                                <asp:TextBox ID="txtHighEducation2" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="input-block mb-3">
                                <label class="col-form-label">University2<span class="text-danger">*</span></label>
                                <asp:TextBox ID="txtEducationUniversity2" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="input-block mb-3">
                                <label class="col-form-label">Year2<span class="text-danger">*</span></label>
                                <asp:TextBox ID="txtEducationYear2" runat="server" CssClass="form-control" onkeypress="return isNumberKey(event)"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="input-block mb-3">
                                <label class="col-form-label">Percentage2<span class="text-danger">*</span></label>
                                <asp:TextBox ID="txt2educationpercentage" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="input-block mb-3">
                                <label class="col-form-label">Marritial Status<span class="text-danger">*</span></label>
                                <asp:DropDownList ID="ddlmaritialstatus" runat="server" CssClass="form-control" onfocus="disablemaritialstatus()" AutoPostBack="true" OnSelectedIndexChanged="ddlmaritialstatus_SelectedIndexChanged">
                                    <asp:ListItem> -- Select -- </asp:ListItem>
                                    <asp:ListItem>Single</asp:ListItem>
                                    <asp:ListItem>Married</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-sm-6" id="spoucediv" runat="server">
                            <div class="input-block mb-3">
                                <label class="col-form-label">Spouse Name<span class="text-danger">*</span></label>
                                <asp:TextBox ID="txtspousename" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="input-block mb-3">
                                <label class="col-form-label">Name(Family Information)<span class="text-danger">*</span></label>
                                <asp:TextBox ID="txtFatherName" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="input-block mb-3">
                                <label class="col-form-label">Relation<span class="text-danger">*</span></label>
                                <asp:TextBox ID="txtRelation1" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="input-block mb-3">
                                <label class="col-form-label">Aadhar Number<span class="text-danger">*</span></label>
                                <asp:TextBox ID="txtFatherAadhar" runat="server" CssClass="form-control" onkeypress="return isNumberKey(event)"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="input-block mb-3">
                                <label class="col-form-label">Mobile<span class="text-danger">*</span></label>
                                <asp:TextBox ID="txtFatherMobile" runat="server" CssClass="form-control" onkeypress="return isNumberKey(event)"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="input-block mb-3">
                                <label class="col-form-label">Name(Family Information)<span class="text-danger">*</span></label>
                                <asp:TextBox ID="txtMotherName" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="input-block mb-3">
                                <label class="col-form-label">Relation<span class="text-danger">*</span></label>
                                <asp:TextBox ID="txtRelation2" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="input-block mb-3">
                                <label class="col-form-label">Aadhar Number<span class="text-danger">*</span></label>
                                <asp:TextBox ID="txtMotherAdhar" runat="server" CssClass="form-control" onkeypress="return isNumberKey(event)"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="input-block mb-3">
                                <label class="col-form-label">Mobile<span class="text-danger">*</span></label>
                                <asp:TextBox ID="txtMotherMobile" runat="server" CssClass="form-control" onkeypress="return isNumberKey(event)"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-sm-6" id="chaild1div" runat="server">
                            <div class="input-block mb-3">
                                <label class="col-form-label">Chaild1Name<span class="text-danger">*</span></label>
                                <asp:TextBox ID="TextBox33" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-sm-6" id="chaild2div" runat="server">
                            <div class="input-block mb-3">
                                <label class="col-form-label">Chaild2Name<span class="text-danger">*</span></label>
                                <asp:TextBox ID="TextBox34" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="input-block mb-3">
                                <label class="col-form-label">EmpExType<span class="text-danger">*</span></label>
                                <asp:DropDownList ID="ddlempextype" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlempextype_SelectedIndexChanged" onfocus="disableddlextype()">
                                    <asp:ListItem> -- Select Type -- </asp:ListItem>
                                    <asp:ListItem>Fresher</asp:ListItem>
                                    <asp:ListItem>Experience</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="input-block mb-3">
                                <label class="col-form-label">ExpextedSalary<span class="text-danger">*</span></label>
                                <asp:TextBox ID="txtaddexpectedsalary" runat="server" CssClass="form-control" onkeypress="return isNumberKey(event)"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="input-block mb-3">
                                <label class="col-form-label">SalaryAnnum<span class="text-danger">*</span></label>
                                <asp:TextBox ID="txtSalaryAnnum" runat="server" CssClass="form-control" onkeypress="return isNumberKey(event)" AutoPostBack="true" OnTextChanged="txtSalaryAnnum_TextChanged"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="input-block mb-3">
                                <label class="col-form-label">SalaryMonth<span class="text-danger">*</span></label>
                                <asp:TextBox ID="txtSalaryMonth" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-sm-6" id="exyearsdiv" runat="server">
                            <div class="input-block mb-3">
                                <label class="col-form-label">Expirience<span class="text-danger">*</span></label>
                                <asp:TextBox ID="txtexpirienceyears" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-sm-6" id="exlast1compdiv" runat="server">
                            <div class="input-block mb-3">
                                <label class="col-form-label">LastCompanyName<span class="text-danger">*</span></label>
                                <asp:TextBox ID="txtlast1company" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-sm-6" id="exlast1desidiv" runat="server">
                            <div class="input-block mb-3">
                                <label class="col-form-label">LastCompanyDesignation<span class="text-danger">*</span></label>
                                <asp:TextBox ID="txtlast1designation" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-sm-6" id="exlast1compfromyeardiv" runat="server">
                            <div class="input-block mb-3">
                                <label class="col-form-label">LastCompanyFromYear<span class="text-danger">*</span></label>
                                <asp:TextBox ID="txtlast1fromyear" runat="server" CssClass="form-control" onkeypress="return isNumberKey(event)"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-sm-6" id="exlast1comptoyeardiv" runat="server">
                            <div class="input-block mb-3">
                                <label class="col-form-label">LastCompanyToYear<span class="text-danger">*</span></label>
                                <asp:TextBox ID="txtlast1toyear" runat="server" CssClass="form-control" onkeypress="return isNumberKey(event)"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-sm-6" id="exlast1compsalaryannumdiv" runat="server">
                            <div class="input-block mb-3">
                                <label class="col-form-label">LastCompanySalaryAnnum<span class="text-danger">*</span></label>
                                <asp:TextBox ID="txtlast1salaryannum" runat="server" CssClass="form-control" onkeypress="return isNumberKey(event)" AutoPostBack="true" OnTextChanged="txtlast1salaryannum_TextChanged"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-sm-6" id="exlast1compsalarymonthdiv" runat="server">
                            <div class="input-block mb-3">
                                <label class="col-form-label">LastCompanySalaryMonth<span class="text-danger">*</span></label>
                                <asp:TextBox ID="txtlast1salarymonth" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-sm-6" id="exlast1compmobilediv" runat="server">
                            <div class="input-block mb-3">
                                <label class="col-form-label">LastCompanyMobileNumber<span class="text-danger">*</span></label>
                                <asp:TextBox ID="txtlast1mobile" runat="server" CssClass="form-control" onkeypress="return isNumberKey(event)"></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-sm-6" id="exlast2compdiv" runat="server">
                            <div class="input-block mb-3">
                                <label class="col-form-label">Last2CompanyName<span class="text-danger">*</span></label>
                                <asp:TextBox ID="txtlast2companyname" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-sm-6" id="exlast2compdesidiv" runat="server">
                            <div class="input-block mb-3">
                                <label class="col-form-label">Last2CompanyDesignation<span class="text-danger">*</span></label>
                                <asp:TextBox ID="txtlast2designation" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-sm-6" id="exlast2compfromyeardiv" runat="server">
                            <div class="input-block mb-3">
                                <label class="col-form-label">Last2CompanyFromYear<span class="text-danger">*</span></label>
                                <asp:TextBox ID="txtlast2fromyear" runat="server" CssClass="form-control" onkeypress="return isNumberKey(event)"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-sm-6" id="exlast2comptoyeardiv" runat="server">
                            <div class="input-block mb-3">
                                <label class="col-form-label">Last2CompanyToYear<span class="text-danger">*</span></label>
                                <asp:TextBox ID="txtlast2toyear" runat="server" CssClass="form-control" onkeypress="return isNumberKey(event)"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-sm-6" id="exlast2compsalaryannumdiv" runat="server">
                            <div class="input-block mb-3">
                                <label class="col-form-label">Last2CompanySalaryAnnum<span class="text-danger">*</span></label>
                                <asp:TextBox ID="txtlast2companysalaryannum" runat="server" CssClass="form-control" onkeypress="return isNumberKey(event)"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-sm-6" id="exlast2compsalarymonthdiv" runat="server">
                            <div class="input-block mb-3">
                                <label class="col-form-label">Last2CompanySalaryMonth<span class="text-danger">*</span></label>
                                <asp:TextBox ID="txtlast2companysalarymonth" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-sm-6" id="exlast2compmobilehdiv" runat="server">
                            <div class="input-block mb-3">
                                <label class="col-form-label">Last2CompanyMobileNumber<span class="text-danger">*</span></label>
                                <asp:TextBox ID="txtlast2companymobile" runat="server" CssClass="form-control" onkeypress="return isNumberKey(event)"></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-sm-6">
                            <div class="input-block mb-3">
                                <label class="col-form-label">NomineeName<span class="text-danger">*</span></label>
                                <asp:TextBox ID="txtnominename" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="input-block mb-3">
                                <label class="col-form-label">NomineeNameRelation<span class="text-danger">*</span></label>
                                <asp:TextBox ID="txtnominerelation" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="input-block mb-3">
                                <label class="col-form-label">NimonieeAadhar<span class="text-danger">*</span></label>
                                <asp:TextBox ID="txtnomineeadhar" runat="server" CssClass="form-control" onkeypress="return isNumberKey(event)"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="input-block mb-3">
                                <label class="col-form-label">NomineeMobileNumber<span class="text-danger">*</span></label>
                                <asp:TextBox ID="txtnomineemobile" runat="server" CssClass="form-control" onkeypress="return isNumberKey(event)"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="submit-section">
                        <asp:Button ID="btnaddemployee" runat="server" Text="Add Employee" CssClass="btn btn-primary submit-btn" OnClick="btnaddemployee_Click" />
                    </div>

                </div>
                <!-- /Add Employee Modal -->
                <div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
