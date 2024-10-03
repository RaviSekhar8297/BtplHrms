 <%@ Page Title="" Language="C#" MasterPageFile="~/Employee.Master" AutoEventWireup="true" CodeBehind="EmployeeContactDetails.aspx.cs" Inherits="Human_Resource_Management.EmployeeContactDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style type="text/css">
		.bor{
			border:1px solid #a59999;
			border-radius:10px; 
			padding:20px;
			background-color:#ddd6d6;
			display:flex;
			flex-direction:row;
			justify-content:space-around;
			margin:2px;
		}
		.con{
			
			display:flex;
			flex-direction:column;
			justify-content:center;
			align-items:center;
			height:100%;
			width:40%;
			
		}
		
		.con1{
			/*padding-left:300px;*/
			/*border:1px solid red;*/
			height:50px;
			width:50px;
			display:flex;
			flex-direction:column;
			justify-content:center;
			align-items:center;
			border-radius:50%;

		}
		.float-start1{
			float:right;
				display:flex;
			flex-direction:column;
			justify-content:center;
			align-items:center;
		}
		.scroll{
			height:580px;
			overflow:hidden;
		}
		.scroll1{
			height:100%!important;
			overflow-y:scroll;
		}
		.edit{
			color:#015207;
		
		}
		.delete{
			color:#fd0d0d;

		}
		.rounded-circle{
			max-width:50px !important;
			max-height:50px !important;

		}
	</Style>
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
							<% if (Session["AddEmployeStatus"] != null && Session["AddEmployeStatus"].ToString() == "True") { %>
                            <a href="EmployeeAddEmployee.aspx" class="btn add-btn"><i class="fa-solid fa-plus"></i> Add Contact</a>
							<% } %>
                            <div class="view-icons">
                                
                              
                                <%--  <a href="AdminAddEmployee.aspx" class="list-view btn btn-link active"><i class="fa-solid fa-plus"></i></a>--%>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- /Page Header -->

                

				<div class="row  scroll">
					<div class="contact-cat col-sm-4 col-lg-3">
								
						<div class="roles-menu">
							<asp:DropDownList ID="ddlcompany" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlcompany_SelectedIndexChanged" onfocus="hideDefaultOption()">

							</asp:DropDownList>
						</div>
					</div>
					<div class="contacts-list col-sm-8 col-lg-9 scroll1">
						<asp:PlaceHolder ID="ContactDetails" runat="server"></asp:PlaceHolder>
					</div>
				</div>
			</div>
			<!-- /Contact Main Row -->
				
			<!-- Add Contact Modal -->
			<div class="modal custom-modal fade" id="add_contact" role="dialog">
				<div class="modal-dialog modal-dialog-centered" role="document">
					<div class="modal-content">
						<div class="modal-header">
							<h5 class="modal-title">Add Contact</h5>
							<button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close">
								<span aria-hidden="true">&times;</span>
							</button>
						</div>
						<div class="modal-body">							
								<div class="input-block mb-3">
									<label class="col-form-label">Name <span class="text-danger">*</span></label>
									<input class="form-control" type="text">
								</div>
								<div class="input-block mb-3">
									<label class="col-form-label">Email Address</label>
									<input class="form-control" type="email">
								</div>
								<div class="input-block mb-3">
									<label class="col-form-label">Contact Number <span class="text-danger">*</span></label>
									<input class="form-control" type="text">
								</div>
								<div class="input-block mb-3">
									<label class="d-block col-form-label">Status</label>
									<div class="status-toggle">
										<input type="checkbox" id="contact_status" class="check">
										<label for="contact_status" class="checktoggle">checkbox</label>
									</div>
								</div>
								<div class="submit-section">
									<button class="btn btn-primary submit-btn">Submit</button>
								</div>
						</div>
					</div>
				</div>
			</div>
			<!-- /Add Contact Modal -->
				
			<!-- Edit Contact Modal -->
			<div class="modal custom-modal fade" id="edit_contact" role="dialog">
				<div class="modal-dialog modal-dialog-centered" role="document">
					<div class="modal-content">
						<div class="modal-header">
							<h5 class="modal-title">Edit Contact</h5>
							<button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close">
								<span aria-hidden="true">&times;</span>
							</button>
						</div>
						<div class="modal-body">
								<div class="input-block mb-3">
									<label class="col-form-label">Name <span class="text-danger">*</span></label>
									<input class="form-control" type="text" value="John Doe">
								</div>
								<div class="input-block mb-3">
									<label class="col-form-label">Email Address</label>
									<input class="form-control" type="email" value="johndoe@example.com">
								</div>
								<div class="input-block mb-3">
									<label class="col-form-label">Contact Number <span class="text-danger">*</span></label>
									<input class="form-control" type="text" value="9876543210">
								</div>
								<div class="input-block mb-3">
									<label class="d-block col-form-label">Status</label>
									<div class="status-toggle">
										<input type="checkbox" id="edit_contact_status" class="check">
										<label for="edit_contact_status" class="checktoggle">checkbox</label>
									</div>
								</div>
								<div class="submit-section">
									<button class="btn btn-primary submit-btn">Save</button>
								</div>
						</div>
					</div>
				</div>
			</div>
			<!-- /Edit Contact Modal -->

			<!-- Delete Contact Modal -->
			<div class="modal custom-modal fade" id="delete_contact" role="dialog">
				<div class="modal-dialog modal-dialog-centered">
					<div class="modal-content">
						<div class="modal-body">
							<div class="form-header">
								<h3>Delete Contact</h3>
								<p>Are you sure want to delete?</p>
							</div>
							<div class="modal-btn delete-action">
								<div class="row">
									<div class="col-6">
										<a href="javascript:void(0);" class="btn btn-primary continue-btn">Delete</a>
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
			<!-- /Delete Contact Modal -->			
		</div>
		<!-- /Page Wrapper -->
	</div>

	<script type="text/javascript">

        function hideDefaultOption() {
            var ddl = document.getElementById('<%= ddlcompany.ClientID %>');
             ddl.options[0].disabled = true;
         }
    </script>
</asp:Content>
