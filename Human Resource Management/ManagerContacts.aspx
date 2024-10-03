<%@ Page Title="" Language="C#" MasterPageFile="~/Manager.Master" AutoEventWireup="true" CodeBehind="ManagerContacts.aspx.cs" Inherits="Human_Resource_Management.ManagerContacts" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="main-wrapper">
        <div class="page-wrapper">
            <div class="content container-fluid">
                <div class="page-header">
                    <div class="row align-items-center">
                        <div class="col">
                            <h3 class="page-title">Contact Details</h3>
                            <ul class="breadcrumb">
                                <li class="breadcrumb-item"><a href="ManagerDashbord.aspx">Dashboard</a></li>
                                <li class="breadcrumb-item active">Employee Contacts</li>
                            </ul>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <asp:PlaceHolder ID="EmployeesData" runat="server"></asp:PlaceHolder>
                </div>
            </div>
        </div>
    </div>

    <!-- Edit Employee Modal -->
    <div id="editEmployeeModal" class="modal custom-modal fade" role="dialog">
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
                    <!-- Form to edit employee details -->
                    <div class="row">
                        <div class="col-md-6">
                            <asp:TextBox ID="txtEmployeeId" runat="server" CssClass="form-control" Placeholder="Employee ID" ReadOnly="true"></asp:TextBox><br />
                        </div>
                        <div class="col-md-6">
                            <asp:TextBox ID="txtSalutation" runat="server" CssClass="form-control" Placeholder="Salutation"></asp:TextBox><br />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <asp:TextBox ID="txtName" runat="server" CssClass="form-control" Placeholder="Name"></asp:TextBox><br />
                        </div>
                        <div class="col-md-6">
                            <asp:TextBox ID="txtCompany" runat="server" CssClass="form-control" Placeholder="Company"></asp:TextBox><br />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <asp:TextBox ID="txtBranch" runat="server" CssClass="form-control" Placeholder="Branch"></asp:TextBox><br />
                        </div>
                        <div class="col-md-6">
                            <asp:TextBox ID="txtDepartment" runat="server" CssClass="form-control" Placeholder="Department"></asp:TextBox><br />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <asp:TextBox ID="txtDesignation" runat="server" CssClass="form-control" Placeholder="Designation"></asp:TextBox><br />
                         </div>
                        <div class="col-md-6">
                            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" Placeholder="Email"></asp:TextBox><br />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <asp:TextBox ID="txtMobile" runat="server" CssClass="form-control" Placeholder="Mobile"></asp:TextBox><br />
                        </div>
                        <div class="col-md-6">
                            <asp:TextBox ID="txtDOB" runat="server" CssClass="form-control" Placeholder="Date of Birth"></asp:TextBox><br />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <asp:TextBox ID="txtDOJ" runat="server" CssClass="form-control" Placeholder="Date of Joining"></asp:TextBox><br />
                        </div>
                        <div class="col-md-6">
                            <asp:TextBox ID="txtSalary" runat="server" CssClass="form-control" Placeholder="Salary"></asp:TextBox><br />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <asp:TextBox ID="txtPFNo" runat="server" CssClass="form-control" Placeholder="PF Number"></asp:TextBox><br />
                         </div>
                        <div class="col-md-6">
                            <asp:TextBox ID="txtESINo" runat="server" CssClass="form-control" Placeholder="ESI Number"></asp:TextBox><br />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <asp:TextBox ID="txtShift" runat="server" CssClass="form-control" Placeholder="Shift"></asp:TextBox><br />
                         </div>
                        <div class="col-md-6">
                            <asp:TextBox ID="txtEmployeeType" runat="server" CssClass="form-control" Placeholder="Employee Type"></asp:TextBox><br />
                        </div>
                    </div>
                </div>
                <div class="submit-section">
                    <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary submit-btn" Text="Update" OnClick="btnSave_Click" Visible="false" />  
                </div>
            </div>
        </div>
    </div>

    <style type="text/css">
        .card {
            position: relative;
            border: 1px solid #ddd;
            border-radius: 4px;
            padding: 1rem;
            margin-bottom: 1rem;
          /*  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);*/
            display: flex;
            align-items: center;
        }

        .edit-icon {
            position: absolute;
            top: 0.5rem;
            right: 0.5rem;
            cursor: pointer;
        }

        .card-body {
            display: flex;
            width: 100%;
            justify-content: space-between;
            gap:0.5rem;
            /* background: linear-gradient(-45deg, rgba(0,0,0,0.22), rgba(255,255,255,0.25));
  box-shadow: 12px 12px 16px 0 rgba(0, 0, 0, 0.25),
   -8px -8px 12px 0 rgba(255, 255, 255, 0.3);*/
        }

        .first-col, .second-col, .third-col, .fourth-col {
            display: flex;
            flex-direction: column;
            justify-content: center;
           
            
        }

        .first-col {
            width: 10%;
            display: flex;
            align-items: center;
            
        }

        .first-col img {
            border-radius: 50%;
            width: 70px;
            height: 70px;
            object-fit: cover;
        }

        .second-col {
            width: 30%;
            align-items: flex-start;
            
        }

        .third-col {
            width: 30%;
            align-items: flex-start;
        }

        .fourth-col {
            width: 30%;
            align-items: flex-start;
        }
        .btn-primary{
          /*  background-color:#ff9b44!important;*/
            border: var(--bs-btn-border-width) solid #ff9b44 !important;
        }
        .submit-section{
            margin-top:0px;
            margin-bottom:20px;
        }
    </style>

    <script type="text/javascript">
        function openEditModal( employeeId, salutation, name, company, branch, department, designation, email, mobile, dob, doj, salary, pfNo, esiNo, shift, employeeType) {
            document.getElementById('<%= txtEmployeeId.ClientID %>').value = employeeId;
            document.getElementById('<%= txtSalutation.ClientID %>').value = salutation;
            document.getElementById('<%= txtName.ClientID %>').value = name;
            document.getElementById('<%= txtCompany.ClientID %>').value = company;
            document.getElementById('<%= txtBranch.ClientID %>').value = branch;
            document.getElementById('<%= txtDepartment.ClientID %>').value = department;
            document.getElementById('<%= txtDesignation.ClientID %>').value = designation;
            document.getElementById('<%= txtEmail.ClientID %>').value = email;
            document.getElementById('<%= txtMobile.ClientID %>').value = mobile;
            document.getElementById('<%= txtDOB.ClientID %>').value = dob;
            document.getElementById('<%= txtDOJ.ClientID %>').value = doj;
            document.getElementById('<%= txtSalary.ClientID %>').value = salary;
            document.getElementById('<%= txtPFNo.ClientID %>').value = pfNo;
            document.getElementById('<%= txtESINo.ClientID %>').value = esiNo;
            document.getElementById('<%= txtShift.ClientID %>').value = shift;
            document.getElementById('<%= txtEmployeeType.ClientID %>').value = employeeType;

            $('#editEmployeeModal').modal('show');
        }
    </script>

</asp:Content>
