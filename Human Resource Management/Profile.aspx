<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="Human_Resource_Management.WebForm2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
	<style type="text/css">
		.bedit{
			border:none;
			outline:none;
			background-color:none;
		}
		.bedit:hover{
			border:none;
			outline:none;
			color:white;
			background-color:#27a160;
		}
		.colorful-gridview {
         border-collapse: collapse;
         width: 100%;
}

.colorful-gridview th {
    background-color: #4CAF50; 
    color: white;
    padding: 8px;
}
.colorful-gridview tr:nth-child(even) {
    background-color: #f2f2f2;
}

.colorful-gridview tr:nth-child(odd) {
    background-color: #ffffff; 
}
.colorful-gridview td {
    padding: 8px;
    border: 1px solid #ddd;
}
	</style>
	<script type="text/javascript">
       

		function hideFirstItem()
		{
           var select = document.getElementById('<%= dddlgender.ClientID %>');
		   if (select && select.options.length > 0)
		   {
            select.options[0].style.display = 'none';
           }
		}
		
        function hideFirststatus() {
            var select = document.getElementById('<%= ddlmaritialstatus.ClientID %>');
             if (select && select.options.length > 0) {
                 select.options[0].style.display = 'none';
             }
		} 
        function hidehmsalarystatus() {
            var select = document.getElementById('<%= ddlhmstatus.ClientID %>');
             if (select && select.options.length > 0) {
                 select.options[0].style.display = 'none';
             }
		}
        function hideddlpaytype() {
            var select = document.getElementById('<%= ddlpaymenttype.ClientID %>');
            if (select && select.options.length > 0) {
                select.options[0].style.display = 'none';
            }
		}
        function hideddlpaytype1() {
            var select = document.getElementById('<%= DropDownList1.ClientID %>');
              if (select && select.options.length > 0) {
                  select.options[0].style.display = 'none';
              }
		}
       <%-- function hideddlpaytype2() {
            var select = document.getElementById('<%= DropDownList2.ClientID %>');
              if (select && select.options.length > 0) {
                  select.options[0].style.display = 'none';
              }
		}--%>
        function hideddlpaytype3() {
            var select = document.getElementById('<%= DropDownList3.ClientID %>');
              if (select && select.options.length > 0) {
                  select.options[0].style.display = 'none';
              }
		}
        function hideddlpaytype4() {
            var select = document.getElementById('<%= DropDownList4.ClientID %>');
              if (select && select.options.length > 0) {
                  select.options[0].style.display = 'none';
              }
		}
        function hideddlpaytype5() {
            var select = document.getElementById('<%= DropDownList5.ClientID %>');
              if (select && select.options.length > 0) {
                  select.options[0].style.display = 'none';
              }
		}
      <%--  function hideddlpaytype6() {
            var select = document.getElementById('<%= DropDownList6.ClientID %>');
              if (select && select.options.length > 0) {
                  select.options[0].style.display = 'none';
              }
		}--%>
        function hideddlpaytype7() {
            var select = document.getElementById('<%= DropDownList7.ClientID %>');
              if (select && select.options.length > 0) {
                  select.options[0].style.display = 'none';
              }
		}
        function hideddlpaytype() {
            var select = document.getElementById('<%= DropDownList8.ClientID %>');
              if (select && select.options.length > 0) {
                  select.options[0].style.display = 'none';
              }
          }
    </script>
	<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css"/>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
	<%--<form id="form1" runat="server">--%>
    <!-- Page Wrapper -->
            <div class="page-wrapper">		
				<!-- Page Content -->
                <div class="content container-fluid">
				
					<!-- Page Header -->
					<div class="page-header">
						<div class="row">
							<div class="col-sm-12">
								<h3 class="page-title">Hi 🥰</h3>
								<ul class="breadcrumb">
									<li class="breadcrumb-item"><a href="admin-dashboard.html">Dashboard</a></li>
									<li class="breadcrumb-item active">Profile</li>
								</ul>
							</div>
						</div>
					</div>
					<!-- /Page Header -->					
					<div class="card mb-0">
						<div class="card-body">
							<div class="row">
								<div class="col-md-12">
									<div class="profile-view">
										<div class="profile-img-wrap">
											<div class="profile-img">
												<a href="#"><asp:Image ID="empimage" runat="server" /></a>
											</div>
										</div>
										<div class="profile-basic">
											<div class="row">
												<div class="col-md-5">
													<div class="profile-info-left">
														<h3 class="user-name m-t-0 mb-0"><asp:Label ID="lblempname" runat="server" ForeColor="#ff0000" ></asp:Label></h3>
														<h6 class="text-muted">Designation : <asp:Label ID="lbldesignation" runat="server"></asp:Label></h6>
														<small class="text-muted">Department : <asp:Label ID="lbldepartment" runat="server"></asp:Label></small>
														<div class="staff-id">Employee ID : <asp:Label ID="lblempid" runat="server"></asp:Label></div>
														<div class="small doj text-muted">Company : <asp:Label ID="lblcompany" runat="server"></asp:Label></div>
														<div class="staff-msg"><a class="btn btn-custom" href="chat.html">Send Message</a></div>
													</div>
												</div>
												<div class="col-md-7">
													<ul class="personal-info">
														<li>
															<div class="title">Branch:</div>
															<div class="text"><asp:Label ID="lblbranch" runat="server"></asp:Label></div>
														</li>
														<li>
															<div class="title">Email:</div>
															<div class="text"><span class="__cf_email__" data-cfemail="f09a9f989e949f95b09588919d809c95de939f9d"><asp:Label ID="lblemail" runat="server" Text="Label"></asp:Label></span></div>
														</li>
														<li>
															<div class="title">Role:</div>
															<div class="text"><asp:Label ID="lblrole" runat="server"></asp:Label></div>
														</li>
														<li>
															<div class="title">DeviceCode:</div>
															<div class="text"><asp:Label ID="lbldevicecode" runat="server"></asp:Label></div>
														</li>
														<li>
															<div class="title">Gender:</div>
															<div class="text">Male</div>
														</li>
														<li>
															<div class="title">Reports to:</div>
															<div class="text">
															   <div class="avatar-box">
																  <div class="avatar avatar-xs">
																	 <img src="assets/img/profiles/avatar-16.jpg" alt="User Image">
																  </div>
															   </div>
															   <a href="profile.html">
																	Jeffery Lalor
																</a>
															</div>
														</li>
													</ul>
												</div>
											</div>
										</div>
										<div class="pro-edit"><a data-bs-target="#profile_info" data-bs-toggle="modal" class="edit-icon" href="#">
											<asp:Button ID="editbtn" runat="server" Text="Edit" OnClick="editbtn_Click" /></a></div>
									</div>
								</div>
							</div>
						</div>
					</div>
					
					<div class="card tab-box">
						<div class="row user-tabs">
							<div class="col-lg-12 col-md-12 col-sm-12 line-tabs">
								<ul class="nav nav-tabs nav-tabs-bottom">
									<li class="nav-item"><a href="#emp_profile" data-bs-toggle="tab" class="nav-link active">Profile</a></li>
									<li class="nav-item"><a href="#emp_projects" data-bs-toggle="tab" class="nav-link">Projects</a></li>
									<li class="nav-item"><a href="#bank_statutory" data-bs-toggle="tab" class="nav-link">Bank & Statutory <small class="text-danger">(Admin Only)</small></a></li>
									<li class="nav-item"><a href="#emp_assets" data-bs-toggle="tab" class="nav-link">Assets</a></li>
								</ul>
							</div>
						</div>
					</div>
					
					<div class="tab-content">
					
						<!-- Profile Info Tab to bind data in labels -->
						<div id="emp_profile" class="pro-overview tab-pane fade show active">
							<div class="row">
								<div class="col-md-6 d-flex">
									<div class="card profile-box flex-fill">
										<div class="card-body">
											<h3 class="card-title" style="color:#128230;">Personal Informations <a href="#" class="edit-icon" data-bs-toggle="modal" data-bs-target="#personal_info_modal">
												<asp:Button ID="btnpropersonal" runat="server" Text="EDIT" CssClass="bedit" /></a></h3>
											<ul class="personal-info">
												<li>
													<div class="title">Name </div>
													<asp:Label ID="lblpername" runat="server" ></asp:Label>
												</li>
												<li>
													<div class="title">DOB  </div>
													<asp:Label ID="lblperdob" runat="server" ></asp:Label>
												</li>
												<li>
													<div class="title">Gender</div>
													<asp:Label ID="lblpergender" runat="server"  ></asp:Label>
												</li>
												<li>
													<div class="title">Mobile</div>
													<asp:Label ID="lblpermobile" runat="server"  ></asp:Label>
												</li>
												<li>
													<div class="title">BloodGroup</div>
													<asp:Label ID="lblperbloodgroup" runat="server" ></asp:Label>
												</li>
												<li>
													<div class="title">Emp Type</div>
													<asp:Label ID="lblpereducation" runat="server"></asp:Label>
												</li>
												<li>
													<div class="title">Pf Type</div>
												   <asp:Label ID="lblpermaritialstatus" runat="server" ></asp:Label>
												</li>
												<li>
													<div class="title">Email</div>
													  <asp:Label ID="lblperemail" runat="server" ></asp:Label>
												</li>
											</ul>
										</div>
									</div>
								</div>
								<div class="col-md-6 d-flex">
									<div class="card profile-box flex-fill">
										<div class="card-body">
											<h3 class="card-title" style="color:#850fa8;">Emergency Contact <a href="#" class="edit-icon" data-bs-toggle="modal" data-bs-target="#emergency_contact_modal"><asp:Button ID="Button2" runat="server" Text="EDIT" CssClass="bedit" /></a></h3>
											<h5 class="section-title"  style="color:#ad8036;">Primary Information</h5>
											<ul class="personal-info">
												<li>
													<div class="title">Name</div>
												    <asp:Label ID="lblemcfathername" runat="server" ></asp:Label>
												</li>
												<li>
													<div class="title">Relationship</div>
													<asp:Label ID="Label10" runat="server"  Text="Father" ></asp:Label>
												</li>
												<li>
													<div class="title">Phone </div>
												<asp:Label ID="lblemcfatherphone" runat="server" ></asp:Label>
												</li>
											</ul>
											<hr>
											<h5 class="section-title" style="color:#ad8036;">Secondary Information</h5>
											<ul class="personal-info">
												<li>
													<div class="title">Name</div>
													<asp:Label ID="lblemcmothername" runat="server" ></asp:Label>
												</li>
												<li>
													<div class="title">Relationship</div>
													<asp:Label ID="Label13" runat="server"  Text="Mother" ></asp:Label>
												</li>
												<li>
													<div class="title">Phone </div>
												   <asp:Label ID="lblemcmothermobile" runat="server" ></asp:Label>
												</li>
											</ul>
										</div>
									</div>
								</div>
							</div>
							<div class="row">
								<div class="col-md-6 d-flex">
									<div class="card profile-box flex-fill">
										<div class="card-body">
											<h3 class="card-title" style="color:#850fa8;">Bank informations<a href="#" class="edit-icon" data-bs-toggle="modal" data-bs-target="#emergency_contact_modal2"><asp:Button ID="Button3" runat="server" Text="EDIT" CssClass="bedit" /></a></h3>
                                            
											<ul class="personal-info">
												<li>
													<div class="title">Bank Name</div>
													<asp:Label ID="lblbankname" runat="server" ></asp:Label>
												</li>
												<li>
													<div class="title">Bank Account No.</div>
													<asp:Label ID="lblbankaccount" runat="server" ></asp:Label>
												</li>
												<li>
													<div class="title">IFSC Code</div>
													<asp:Label ID="lblbankifsc" runat="server" ></asp:Label>
												</li>
												<li>
													<div class="title">PAN No</div>
													<asp:Label ID="lblbankpan" runat="server" ></asp:Label>
												</li>
											</ul>
										</div>
									</div>
								</div>
								<div class="col-md-6 d-flex">
									<div class="card profile-box flex-fill">
										<div class="card-body">
											<h3 class="card-title">Family Information <a href="#" class="edit-icon" data-bs-toggle="modal" data-bs-target="#family_info_modal"><asp:Button ID="Button4" runat="server" Text="EDIT" CssClass="bedit" /></a></h3>
											<div class="table-responsive">
												<table class="table table-nowrap">
													<thead>
														<tr>
															<th>Name</th>
															<th>Relationship</th>
															<th>Aadhar</th>
															<th>Phone</th>
															<th></th>
														</tr>
													</thead>
													<tbody>
														<tr>
															<td><asp:Label ID="lblfamfathername" runat="server" ></asp:Label></td>
															<td><asp:Label ID="Label20" runat="server" Text="Father" ></asp:Label></td>
															<td><asp:Label ID="lblfamfatheraadhar" runat="server" ></asp:Label></td>
															<td><asp:Label ID="lblfammobile" runat="server" ></asp:Label></td>
															<td class="text-end">
																<div class="dropdown dropdown-action">
																	<%--<a aria-expanded="false" data-bs-toggle="dropdown" class="action-icon dropdown-toggle" href="#"><i class="material-icons">more_vert</i></a>
																	<div class="dropdown-menu dropdown-menu-right">
																		<a href="#" class="dropdown-item"><i class="fa-solid fa-pencil m-r-5"></i> Edit</a>
																		<a href="#" class="dropdown-item"><i class="fa-regular fa-trash-can m-r-5"></i> Delete</a>
																	</div>--%>
																</div>
															</td>
														</tr>
													</tbody>
												</table>
											</div>
										</div>
									</div>
								</div>
							</div>
							<div class="row">
								<div class="col-md-6 d-flex">
									<div class="card profile-box flex-fill">
										<div class="card-body">
											<h3 class="card-title">Education Informations <a href="#" class="edit-icon" data-bs-toggle="modal" data-bs-target="#education_info"><asp:Button ID="Button5" runat="server" Text="EDIT" CssClass="bedit" /></a></h3>
											<div class="experience-box">
												<ul class="experience-list">
													<li>
														<div class="experience-user">
															<div class="before-circle"></div>
														</div>
														<div class="experience-content">
															<div class="timeline-content">
																<a href="#/" class="name"><asp:Label ID="Label1" runat="server" Text="Univercity" ></asp:Label></a>
																<div><asp:Label ID="Label2" runat="server" Text="Group" ></asp:Label></div>
																<span class="time"><asp:Label ID="Label3" runat="server" Text="2021" ></asp:Label></span>
															</div>
														</div>
													</li>
													<li>
														<div class="experience-user">
															<div class="before-circle"></div>
														</div>
														<div class="experience-content">
															<div class="timeline-content">
																<a href="#/" class="name"><asp:Label ID="Label11" runat="server" Text="Univercity" ></asp:Label></a>
																<div><asp:Label ID="Label12" runat="server" Text="Group" ></asp:Label></div>
																<span class="time"><asp:Label ID="Label14" runat="server" Text="2023" ></asp:Label></span>
															</div>
														</div>
													</li>
												</ul>
											</div>
										</div>
									</div>
								</div>
								<div class="col-md-6 d-flex">
									<div class="card profile-box flex-fill">
										<div class="card-body">
											<h3 class="card-title">Experience <a href="#" class="edit-icon" data-bs-toggle="modal" data-bs-target="#experience_info"><asp:Button ID="Button6" runat="server" Text="EDIT" CssClass="bedit" /></a></h3>
											<div class="experience-box">
												<ul class="experience-list">
													<li>
														<div class="experience-user">
															<div class="before-circle"></div>
														</div>
														<div class="experience-content">
															<div class="timeline-content">
																<a href="#/" class="name"><asp:Label ID="Label4" runat="server" Text="Company1" ></asp:Label></a>
																<span class="time"><asp:Label ID="Label5" runat="server" Text="2020" ></asp:Label></span>
															</div>
														</div>
													</li>
													<li>
														<div class="experience-user">
															<div class="before-circle"></div>
														</div>
														<div class="experience-content">
															<div class="timeline-content">
																<a href="#/" class="name"><asp:Label ID="Label6" runat="server" Text="Company2" ></asp:Label></a>
																<span class="time"><asp:Label ID="Label7" runat="server" Text="2021" ></asp:Label></span>
															</div>
														</div>
													</li>
													<li>
														<div class="experience-user">
															<div class="before-circle"></div>
														</div>
														<div class="experience-content">
															<div class="timeline-content">
																<a href="#/" class="name"><asp:Label ID="Label8" runat="server" Text="Company3" ></asp:Label></a>
																<span class="time"><asp:Label ID="Label9" runat="server" Text="2022" ></asp:Label></span>
															</div>
														</div>
													</li>
												</ul>
											</div>
										</div>
									</div>
								</div>
							</div>
						</div>
						<!-- /Profile Info Tab -->
						
						<!-- Projects Tab -->
					
						<div class="tab-pane fade" id="emp_projects">
							<div class="row">
									<div id="projectsContainer" runat="server"></div>
							<%--	<div class="col-lg-4 col-sm-6 col-md-4 col-xl-3">
									<div class="card">
										<div class="card-body">
											<div class="dropdown profile-action">
												<a aria-expanded="false" data-bs-toggle="dropdown" class="action-icon dropdown-toggle" href="#"><i class="material-icons">more_vert</i></a>
												<div class="dropdown-menu dropdown-menu-right">
													<a data-bs-target="#edit_project" data-bs-toggle="modal" href="#" class="dropdown-item"><i class="fa-solid fa-pencil m-r-5"></i> Edit</a>
													<a data-bs-target="#delete_project" data-bs-toggle="modal" href="#" class="dropdown-item"><i class="fa-regular fa-trash-can m-r-5"></i> Delete</a>
												</div>
											</div>
											<h4 class="project-title"><asp:Label ID="lblprojectname" runat="server" Text="Project Name"></asp:Label></h4>

											<small class="block text-ellipsis m-b-15">
												<span class="text-xs"><asp:Label ID="lblcompletetasks" runat="server" Text="1 "></asp:Label></span> <span class="text-muted">Completed Tasks, </span>
												<span class="text-xs"><asp:Label ID="lbltotaltaska" runat="server" Text="9 "></asp:Label></span> <span class="text-muted">Total Tasks</span>
											</small>
											<p class="text-muted"><asp:TextBox ID="txtdescription" runat="server" Text="description" TextMode="MultiLine"></asp:TextBox></p>
											<div class="pro-deadline m-b-15">
												<div class="sub-title">
													Deadline:
												</div>
												<div class="text-muted">
												<asp:Label ID="lblenddate" runat="server" Text="EndDate"></asp:Label>
												</div>
											</div>
											<div class="project-members m-b-15">
												<div>Assign :</div>
												<ul class="team-members">
													<li>
														<asp:Label ID="lblprojectleader" runat="server" Text="Project Leader"></asp:Label>
													</li>
												</ul>
											</div>
											<div class="project-members m-b-15">
												<div>Team :</div>
												<ul class="team-members">
													<li>
														<a href="#" data-bs-toggle="tooltip" title="John Doe"><img src="assets/img/profiles/avatar-02.jpg" alt="User Image"></a>
													</li>
													<li>
														<a href="#" data-bs-toggle="tooltip" title="Richard Miles"><img src="assets/img/profiles/avatar-09.jpg" alt="User Image"></a>
													</li>
													<li>
														<a href="#" data-bs-toggle="tooltip" title="John Smith"><img src="assets/img/profiles/avatar-10.jpg" alt="User Image"></a>
													</li>
													<li>
														<a href="#" data-bs-toggle="tooltip" title="Mike Litorus"><img src="assets/img/profiles/avatar-05.jpg" alt="User Image"></a>
													</li>
													<li>
														<a href="#" class="all-users">+15</a>
													</li>
												</ul>
											</div>
											<p class="m-b-5">Progress <span class="text-success float-end"><asp:Label ID="lblstatus" runat="server" Text="50%"></asp:Label></span></p>
											<div class="progress progress-xs mb-0">
												<div class="w-40 progress-bar bg-success" title="" data-bs-toggle="tooltip" role="progressbar"></div>
											</div>
										</div>
									</div>
								</div>--%>
								
								<%--<div class="col-lg-4 col-sm-6 col-md-4 col-xl-3">
									<div class="card">
										<div class="card-body">
											<div class="dropdown profile-action">
												<a aria-expanded="false" data-bs-toggle="dropdown" class="action-icon dropdown-toggle" href="#"><i class="material-icons">more_vert</i></a>
												<div class="dropdown-menu dropdown-menu-right">
													<a data-bs-target="#edit_project" data-bs-toggle="modal" href="#" class="dropdown-item"><i class="fa-solid fa-pencil m-r-5"></i> Edit</a>
													<a data-bs-target="#delete_project" data-bs-toggle="modal" href="#" class="dropdown-item"><i class="fa-regular fa-trash-can m-r-5"></i> Delete</a>
												</div>
											</div>
											<h4 class="project-title"><a href="project-view.html">Project Management</a></h4>
											<small class="block text-ellipsis m-b-15">
												<span class="text-xs">2</span> <span class="text-muted">open tasks, </span>
												<span class="text-xs">5</span> <span class="text-muted">tasks completed</span>
											</small>
											<p class="text-muted">Lorem Ipsum is simply dummy text of the printing and
												typesetting industry. When an unknown printer took a galley of type and
												scrambled it...
											</p>
											<div class="pro-deadline m-b-15">
												<div class="sub-title">
													Deadline:
												</div>
												<div class="text-muted">
													17 Apr 2019
												</div>
											</div>
											<div class="project-members m-b-15">
												<div>Project Leader :</div>
												<ul class="team-members">
													<li>
														<a href="#" data-bs-toggle="tooltip" title="Jeffery Lalor"><img src="assets/img/profiles/avatar-16.jpg" alt="User Image"></a>
													</li>
												</ul>
											</div>
											<div class="project-members m-b-15">
												<div>Team :</div>
												<ul class="team-members">
													<li>
														<a href="#" data-bs-toggle="tooltip" title="John Doe"><img src="assets/img/profiles/avatar-02.jpg" alt="User Image"></a>
													</li>
													<li>
														<a href="#" data-bs-toggle="tooltip" title="Richard Miles"><img src="assets/img/profiles/avatar-09.jpg" alt="User Image"></a>
													</li>
													<li>
														<a href="#" data-bs-toggle="tooltip" title="John Smith"><img src="assets/img/profiles/avatar-10.jpg" alt="User Image"></a>
													</li>
													<li>
														<a href="#" data-bs-toggle="tooltip" title="Mike Litorus"><img src="assets/img/profiles/avatar-05.jpg" alt="User Image"></a>
													</li>
													<li>
														<a href="#" class="all-users">+15</a>
													</li>
												</ul>
											</div>
											<p class="m-b-5">Progress <span class="text-success float-end">40%</span></p>
											<div class="progress progress-xs mb-0">
												<div class="w-40" title="" data-bs-toggle="tooltip" role="progressbar" class="progress-bar bg-success" data-original-title="40%"></div>
											</div>
										</div>
									</div>
								</div>
								
								<div class="col-lg-4 col-sm-6 col-md-4 col-xl-3">
									<div class="card">
										<div class="card-body">
											<div class="dropdown profile-action">
												<a aria-expanded="false" data-bs-toggle="dropdown" class="action-icon dropdown-toggle" href="#"><i class="material-icons">more_vert</i></a>
												<div class="dropdown-menu dropdown-menu-right">
													<a data-bs-target="#edit_project" data-bs-toggle="modal" href="#" class="dropdown-item"><i class="fa-solid fa-pencil m-r-5"></i> Edit</a>
													<a data-bs-target="#delete_project" data-bs-toggle="modal" href="#" class="dropdown-item"><i class="fa-regular fa-trash-can m-r-5"></i> Delete</a>
												</div>
											</div>
											<h4 class="project-title"><a href="project-view.html">Video Calling App</a></h4>
											<small class="block text-ellipsis m-b-15">
												<span class="text-xs">3</span> <span class="text-muted">open tasks, </span>
												<span class="text-xs">3</span> <span class="text-muted">tasks completed</span>
											</small>
											<p class="text-muted">Lorem Ipsum is simply dummy text of the printing and
												typesetting industry. When an unknown printer took a galley of type and
												scrambled it...
											</p>
											<div class="pro-deadline m-b-15">
												<div class="sub-title">
													Deadline:
												</div>
												<div class="text-muted">
													17 Apr 2019
												</div>
											</div>
											<div class="project-members m-b-15">
												<div>Project Leader :</div>
												<ul class="team-members">
													<li>
														<a href="#" data-bs-toggle="tooltip" title="Jeffery Lalor"><img src="assets/img/profiles/avatar-16.jpg" alt="User Image"></a>
													</li>
												</ul>
											</div>
											<div class="project-members m-b-15">
												<div>Team :</div>
												<ul class="team-members">
													<li>
														<a href="#" data-bs-toggle="tooltip" title="John Doe"><img src="assets/img/profiles/avatar-02.jpg" alt="User Image"></a>
													</li>
													<li>
														<a href="#" data-bs-toggle="tooltip" title="Richard Miles"><img src="assets/img/profiles/avatar-09.jpg" alt="User Image"></a>
													</li>
													<li>
														<a href="#" data-bs-toggle="tooltip" title="John Smith"><img src="assets/img/profiles/avatar-10.jpg" alt="User Image"></a>
													</li>
													<li>
														<a href="#" data-bs-toggle="tooltip" title="Mike Litorus"><img src="assets/img/profiles/avatar-05.jpg" alt="User Image"></a>
													</li>
													<li>
														<a href="#" class="all-users">+15</a>
													</li>
												</ul>
											</div>
											<p class="m-b-5">Progress <span class="text-success float-end">40%</span></p>
											<div class="progress progress-xs mb-0">
												<div class="w-40" title="" data-bs-toggle="tooltip" role="progressbar" class="progress-bar bg-success" data-original-title="40%"></div>
											</div>
										</div>
									</div>
								</div>
								
								<div class="col-lg-4 col-sm-6 col-md-4 col-xl-3">
									<div class="card">
										<div class="card-body">
											<div class="dropdown profile-action">
												<a aria-expanded="false" data-bs-toggle="dropdown" class="action-icon dropdown-toggle" href="#"><i class="material-icons">more_vert</i></a>
												<div class="dropdown-menu dropdown-menu-right">
													<a data-bs-target="#edit_project" data-bs-toggle="modal" href="#" class="dropdown-item"><i class="fa-solid fa-pencil m-r-5"></i> Edit</a>
													<a data-bs-target="#delete_project" data-bs-toggle="modal" href="#" class="dropdown-item"><i class="fa-regular fa-trash-can m-r-5"></i> Delete</a>
												</div>
											</div>
											<h4 class="project-title"><a href="project-view.html">Hospital Administration</a></h4>
											<small class="block text-ellipsis m-b-15">
												<span class="text-xs">12</span> <span class="text-muted">open tasks, </span>
												<span class="text-xs">4</span> <span class="text-muted">tasks completed</span>
											</small>
											<p class="text-muted">Lorem Ipsum is simply dummy text of the printing and
												typesetting industry. When an unknown printer took a galley of type and
												scrambled it...
											</p>
											<div class="pro-deadline m-b-15">
												<div class="sub-title">
													Deadline:
												</div>
												<div class="text-muted">
													17 Apr 2019
												</div>
											</div>
											<div class="project-members m-b-15">
												<div>Project Leader :</div>
												<ul class="team-members">
													<li>
														<a href="#" data-bs-toggle="tooltip" title="Jeffery Lalor"><img src="assets/img/profiles/avatar-16.jpg" alt="User Image"></a>
													</li>
												</ul>
											</div>
											<div class="project-members m-b-15">
												<div>Team :</div>
												<ul class="team-members">
													<li>
														<a href="#" data-bs-toggle="tooltip" title="John Doe"><img src="assets/img/profiles/avatar-02.jpg" alt="User Image"></a>
													</li>
													<li>
														<a href="#" data-bs-toggle="tooltip" title="Richard Miles"><img src="assets/img/profiles/avatar-09.jpg" alt="User Image"></a>
													</li>
													<li>
														<a href="#" data-bs-toggle="tooltip" title="John Smith"><img src="assets/img/profiles/avatar-10.jpg" alt="User Image"></a>
													</li>
													<li>
														<a href="#" data-bs-toggle="tooltip" title="Mike Litorus"><img src="assets/img/profiles/avatar-05.jpg" alt="User Image"></a>
													</li>
													<li>
														<a href="#" class="all-users">+15</a>
													</li>
												</ul>
											</div>
											<p class="m-b-5">Progress <span class="text-success float-end">40%</span></p>
											<div class="progress progress-xs mb-0">
												<div class="w-40 progress-bar bg-success" title="" data-bs-toggle="tooltip" role="progressbar" data-original-title="40%"></div>
											</div>
										</div>
									</div>
								</div>--%>
							</div>
						</div>
						<!-- /Projects Tab -->
						
						<!-- Bank Statutory Tab -->
						<div class="tab-pane fade" id="bank_statutory">
							<div class="card">
								<div class="card-body">
									<h3 class="card-title"> Basic Salary Information</h3>
										<div class="row">
											<div class="col-sm-4">
												<div class="input-block mb-3">
													<label class="col-form-label">Salary basis <span class="text-danger">*</span></label>													
													<asp:DropDownList ID="ddlhmstatus" runat="server" CssClass="select form-control" onmousedown="hidehmsalarystatus()" >
													<asp:ListItem> -- Select --</asp:ListItem>
													<asp:ListItem>Weekly</asp:ListItem>
													<asp:ListItem>Monthly</asp:ListItem>
												</asp:DropDownList>
											   </div>
											</div>
											<div class="col-sm-4">
												<div class="input-block mb-3">
													<label class="col-form-label">Salary amount <small class="text-muted">per month</small></label>
													<div class="input-group">
														<span class="input-group-text">Rs</span>
														<asp:TextBox ID="txtpfsalary" runat="server" CssClass="form-control" placeholder="Type Your Salary Amount" ReadOnly="true"></asp:TextBox>
													</div>
												</div>
											</div>
											<div class="col-sm-4">
												<div class="input-block mb-3">
													<label class="col-form-label">Payment type</label>													
													<asp:DropDownList ID="ddlpaymenttype" runat="server" CssClass="select form-control" onmousedown="hideddlpaytype()" >
													<asp:ListItem> -- Select --</asp:ListItem>
													<asp:ListItem>Bank transfer</asp:ListItem>
													<asp:ListItem>Check</asp:ListItem>
													<asp:ListItem>Cash</asp:ListItem>
												</asp:DropDownList>
											   </div>
											</div>
										</div>
										<hr>
										<h3 class="card-title"> PF Information</h3>
										<div class="row">
											<div class="col-sm-4">
												<div class="input-block mb-3">
													<label class="col-form-label">PF contribution</label>														
													<asp:DropDownList ID="DropDownList1" runat="server" CssClass="select form-control" onmousedown="hideddlpaytype1()" >
													<asp:ListItem> -- Select PF Contribution --</asp:ListItem>
													<asp:ListItem>YES</asp:ListItem>
													<asp:ListItem>NO</asp:ListItem>
												</asp:DropDownList>
												</div>
											</div>
											<div class="col-sm-4">
												<div class="input-block mb-3">
													<label class="col-form-label">PF No. <span class="text-danger">*</span></label>
													<asp:TextBox ID="TextBox38" runat="server" CssClass="select form-control"></asp:TextBox>
												   <%-- <asp:DropDownList ID="DropDownList2" runat="server" CssClass="select form-control" onmousedown="hideddlpaytype2()" >
												  	    <asp:ListItem> -- Select PF Contribution --</asp:ListItem>
												    	<asp:ListItem>YES</asp:ListItem>
													   <asp:ListItem>NO</asp:ListItem>
											    	</asp:DropDownList>--%>
												</div>
											</div>
										</div>
										<div class="row">
											<div class="col-sm-4">
												<div class="input-block mb-3">
													<label class="col-form-label">Employee PF rate</label>
													<asp:DropDownList ID="DropDownList3" runat="server" CssClass="select form-control" onmousedown="hideddlpaytype3()" >
													   <asp:ListItem> -- Select PF Contribution --</asp:ListItem>
													   <asp:ListItem>YES</asp:ListItem>
													   <asp:ListItem>NO</asp:ListItem>
												   </asp:DropDownList>
												</div>
											</div>
											<div class="col-sm-4">
												<div class="input-block mb-3">
													<label class="col-form-label">Additional rate <span class="text-danger">*</span></label>
													<asp:DropDownList ID="DropDownList4" runat="server" CssClass="select form-control" onmousedown="hideddlpaytype4()" >
													   <asp:ListItem> -- Select PF Contribution --</asp:ListItem>
													     <asp:ListItem>0%</asp:ListItem>
													     <asp:ListItem>1%</asp:ListItem>
														 <asp:ListItem>2%</asp:ListItem>
													     <asp:ListItem>3%</asp:ListItem>
														 <asp:ListItem>4%</asp:ListItem>
													     <asp:ListItem>5%</asp:ListItem>
														 <asp:ListItem>6%</asp:ListItem>
													     <asp:ListItem>7%</asp:ListItem>
														 <asp:ListItem>8%</asp:ListItem>
														 <asp:ListItem>9%</asp:ListItem>
													     <asp:ListItem>10%</asp:ListItem>
												   </asp:DropDownList>
												</div>
											</div>
											<div class="col-sm-4">
												<div class="input-block mb-3">
													<label class="col-form-label">Total rate</label>
													<asp:TextBox ID="TextBox36" runat="server" CssClass="form-control" Text="2%" ReadOnly="true"></asp:TextBox>
												</div>
											</div>
									   </div>
									<%--	<div class="row">
											<div class="col-sm-4">
												<div class="input-block mb-3">
													<label class="col-form-label">Employee PF rate</label>
													<asp:DropDownList ID="DropDownList4" runat="server" CssClass="select form-control" onmousedown="hideddlpaytype()" >
													  <asp:ListItem> -- Select PF Contribution --</asp:ListItem>
												  	  <asp:ListItem>YES</asp:ListItem>
													  <asp:ListItem>NO</asp:ListItem>
											    	</asp:DropDownList>
												</div>
											</div>
											<div class="col-sm-4">
												<div class="input-block mb-3">
													<label class="col-form-label">Additional rate <span class="text-danger">*</span></label>
													<select class="select">
														<option>Select additional rate</option>
														<option>0%</option>
														<option>1%</option>
														<option>2%</option>
														<option>3%</option>
														<option>4%</option>
														<option>5%</option>
														<option>6%</option>
														<option>7%</option>
														<option>8%</option>
														<option>9%</option>
														<option>10%</option>
													</select>
												</div>
											</div>
											<div class="col-sm-4">
												<div class="input-block mb-3">
													<label class="col-form-label">Total rate</label>
													<input type="text" class="form-control" placeholder="N/A" value="11%">
												</div>
											</div>
										</div>--%>
										
										<hr>
										<h3 class="card-title"> ESI Information</h3>
										<div class="row">
											<div class="col-sm-4">
												<div class="input-block mb-3">
													<label class="col-form-label">ESI contribution</label>													
													<asp:DropDownList ID="DropDownList5" runat="server" CssClass="select form-control" onmousedown="hideddlpaytype5()" >
													  <asp:ListItem> -- Select PF Contribution --</asp:ListItem>
												  	  <asp:ListItem>YES</asp:ListItem>
													  <asp:ListItem>NO</asp:ListItem>
											    	</asp:DropDownList>
												</div>
											</div>
											<div class="col-sm-4">
												<div class="input-block mb-3">
													<label class="col-form-label">ESI No. <span class="text-danger">*</span></label>
													<asp:TextBox ID="TextBox40" runat="server" CssClass="select form-control"></asp:TextBox>													
												</div>
											</div>
										</div>
										<div class="row">
											<div class="col-sm-4">
												<div class="input-block mb-3">
													<label class="col-form-label">Employee ESI rate</label>
													<asp:DropDownList ID="DropDownList7" runat="server" CssClass="select form-control" onmousedown="hideddlpaytype7()" >
													  <asp:ListItem> -- Select PF Contribution --</asp:ListItem>
												  	  <asp:ListItem>YES</asp:ListItem>
													  <asp:ListItem>NO</asp:ListItem>
											    	</asp:DropDownList>
												</div>
											</div>
											<div class="col-sm-4">
												<div class="input-block mb-3">
													<label class="col-form-label">Additional rate <span class="text-danger">*</span></label>
													<asp:DropDownList ID="DropDownList8" runat="server" CssClass="select form-control" onmousedown="hideddlpaytype()" >
													  <asp:ListItem> -- Select PF Contribution --</asp:ListItem>
														 <asp:ListItem>0%</asp:ListItem>
												  	     <asp:ListItem>1%</asp:ListItem>
													     <asp:ListItem>2%</asp:ListItem>
														 <asp:ListItem>3%</asp:ListItem>
														 <asp:ListItem>4%</asp:ListItem>
													     <asp:ListItem>5%</asp:ListItem>
														 <asp:ListItem>6%</asp:ListItem>
													     <asp:ListItem>7%</asp:ListItem>
														 <asp:ListItem>8%</asp:ListItem>
														 <asp:ListItem>9%</asp:ListItem>
													     <asp:ListItem>10%</asp:ListItem>
											    	</asp:DropDownList>
												</div>
											</div>
											<div class="col-sm-4">
												<div class="input-block mb-3">
													<label class="col-form-label">Total rate</label>
													<asp:TextBox ID="TextBox37" runat="server" CssClass="form-control" Text="2%" ReadOnly="true"></asp:TextBox>
												</div>
											</div>
									   </div>
										
										<div class="submit-section">
											<button class="btn btn-primary submit-btn" type="submit">Save</button>
										</div>
								</div>
							</div>
						</div>
						<!-- /Bank Statutory Tab -->
						
						<!-- Assets -->
					<%--	<div class="tab-pane fade" id="emp_assets">
							<div class="table-responsive table-newdatatable">
								<table class="table table-new custom-table mb-0 datatable">
									<thead>
										<tr>
											<th>S.NO</th>
											<th>Name</th>
											<th>Asset ID</th>
											<th>Assigned Date</th>
											<th>Assignee</th>
											<th>Action</th>
										</tr>
									</thead>
									<tbody>
										<tr>
											<td>1</td>
											<td>
												<a href="assets-details.html" class="table-imgname">
													<img src="assets/img/laptop.png" class="me-2" alt="Laptop Image">
													<span>Laptop</span>
												</a>
											</td>
											<td>AST - 001</td>
											<td>22 Nov, 2022    10:32AM</td>
											<td class="table-namesplit">
												<a href="javascript:void(0);" class="table-profileimage">
													<img src="assets/img/profiles/avatar-02.jpg" class="me-2" alt="User Image">
												</a>
												<a href="javascript:void(0);" class="table-name">
													<span>John Paul Raj</span>
													<p><span class="__cf_email__" data-cfemail="701a1f181e30140215111d17050903041513185e131f1d">[email&#160;protected]</span></p>
												</a>
											</td>
											<td>
												<div class="table-actions d-flex">
													<a class="delete-table me-2" href="user-asset-details.html">
													   <img src="assets/img/icons/eye.svg" alt="Eye Icon">
													</a>
												</div>
											</td>
										</tr>
										<tr>
											<td>2</td>
											<td>
												<a href="assets-details.html" class="table-imgname">
													<img src="assets/img/laptop.png" class="me-2" alt="Laptop Image">
													<span>Laptop</span>
												</a>
											</td>
											<td>AST - 002</td>
											<td>22 Nov, 2022    10:32AM</td>
											<td class="table-namesplit">
												<a href="javascript:void(0);" class="table-profileimage" data-bs-toggle="modal" data-bs-target="#edit-asset">
													<img src="assets/img/profiles/avatar-05.jpg" class="me-2" alt="User Image">
												</a>
												<a href="javascript:void(0);" class="table-name">
													<span>Vinod Selvaraj</span>
													<p><span class="__cf_email__" data-cfemail="f4829d9a9b90da87b4908691959993818d878091979cda979b99">[email&#160;protected]</span></p>
												</a>
											</td>
											<td>
												<div class="table-actions d-flex">
													<a class="delete-table me-2" href="user-asset-details.html">
													   <img src="assets/img/icons/eye.svg" alt="Eye Icon">
													</a>
												</div>
											</td>
										</tr>
										<tr>
											<td>3</td>
											<td>
												<a href="assets-details.html" class="table-imgname">
													<img src="assets/img/keyboard.png" class="me-2" alt="Keyboard Image">
													<span>Dell Keyboard</span>
												</a>
											</td>
											<td>AST - 003</td>
											<td>22 Nov, 2022    10:32AM</td>
											<td class="table-namesplit">
												<a href="javascript:void(0);" class="table-profileimage" data-bs-toggle="modal" data-bs-target="#edit-asset">
													<img src="assets/img/profiles/avatar-03.jpg" class="me-2" alt="User Image">
												</a>
												<a href="javascript:void(0);" class="table-name">
													<span>Harika </span>
													<p><span class="__cf_email__" data-cfemail="7c141d0e15171d520a3c180e191d111b09050f08191f14521f1311">[email&#160;protected]</span></p>
												</a>
											</td>
											<td>
												<div class="table-actions d-flex">
													<a class="delete-table me-2" href="user-asset-details.html">
													   <img src="assets/img/icons/eye.svg" alt="Eye Icon">
													</a>
												</div>
											</td>
										</tr>
										<tr>
											<td>4</td>
											<td>
												<a href="#" class="table-imgname">
													<img src="assets/img/mouse.png" class="me-2" alt="Mouse Image">
													<span>Logitech Mouse</span>
												</a>
											</td>
											<td>AST - 0024</td>
											<td>22 Nov, 2022    10:32AM</td>
											<td class="table-namesplit">
												<a href="assets-details.html" class="table-profileimage" >
													<img src="assets/img/profiles/avatar-02.jpg" class="me-2" alt="User Image">
												</a>
												<a href="assets-details.html" class="table-name">
													<span>Mythili</span>
													<p><span class="__cf_email__" data-cfemail="1974606d71707570597d6b7c78747e6c606a6d7c7a71377a7674">[email&#160;protected]</span></p>
												</a>
											</td>
											<td>
												<div class="table-actions d-flex">
													<a class="delete-table me-2" href="user-asset-details.html">
													   <img src="assets/img/icons/eye.svg" alt="Eye Icon">
													</a>
												</div>
											</td>
										</tr>
										<tr>
											<td>5</td>
											<td>
												<a href="#" class="table-imgname">
													<img src="assets/img/laptop.png" class="me-2" alt="Laptop Image">
													<span>Laptop</span>
												</a>
											</td>
											<td>AST - 005</td>
											<td>22 Nov, 2022    10:32AM</td>
											<td class="table-namesplit">
												<a href="assets-details.html" class="table-profileimage" >
													<img src="assets/img/profiles/avatar-02.jpg" class="me-2" alt="User Image">
												</a>
												<a href="assets-details.html" class="table-name">
													<span>John Paul Raj</span>
													<p><span class="__cf_email__" data-cfemail="741e1b1c1a34100611151913010d070011171c5a171b19">[email&#160;protected]</span></p>
												</a>
											</td>
											<td>
												<div class="table-actions d-flex">
													<a class="delete-table me-2" href="user-asset-details.html">
													   <img src="assets/img/icons/eye.svg" alt="Eye Icon">
													</a>
												</div>
											</td>
										</tr>
										<tr>
											<td>6</td>
											<td>
												<a href="#" class="table-imgname">
													<img src="assets/img/laptop.png" class="me-2" alt="Laptop Image">
													<span>Laptop</span>
												</a>
											</td>
											<td>AST - 006</td>
											<td>22 Nov, 2022    10:32AM</td>
											<td class="table-namesplit">
												<a href="javascript:void(0);" class="table-profileimage" >
													<img src="assets/img/profiles/avatar-05.jpg" class="me-2" alt="User Image">
												</a>
												<a href="javascript:void(0);" class="table-name">
													<span>Vinod Selvaraj</span>
													<p><span class="__cf_email__" data-cfemail="1c6a75727378326f5c786e797d717b69656f68797f74327f7371">[email&#160;protected]</span></p>
												</a>
											</td>
											<td>
												<div class="table-actions d-flex">
													<a class="delete-table me-2" href="user-asset-details.html">
													   <img src="assets/img/icons/eye.svg" alt="Eye Icon">
													</a>
												</div>
											</td>
										</tr>
									</tbody>
								</table>
							</div>
						</div>--%>
						<!-- /Assets -->
						<div class="tab-pane fade" id="emp_assets" style="overflow-y:scroll;height:300px;width:100%;">
							
						<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%" CssClass="colorful-gridview" >
                            <Columns>
                                <asp:BoundField DataField="S_No" HeaderText="S.No " />
                                <asp:BoundField DataField="Name" HeaderText="Name"  />
                                <asp:BoundField DataField="EmpId" HeaderText="EmpId"  />
                                <asp:BoundField DataField="Date" HeaderText="Date" />      
                                <asp:BoundField DataField="assets" HeaderText="assets" />
                                <asp:BoundField DataField="Image" HeaderText="Image" />
                            </Columns>
                        </asp:GridView>
                       </div>
					</div>
                </div>
				<!-- /Page Content -->
				
				<!-- Profile Modal -->
				<div id="profile_info" class="modal custom-modal fade" role="dialog">
					<div class="modal-dialog modal-dialog-centered modal-lg" role="document">
						<div class="modal-content">
							<div class="modal-header">
								<h5 class="modal-title">Profile Information</h5>
								<button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close">
									<span aria-hidden="true">&times;</span>
								</button>
							</div>
							<div class="modal-body">
									<div class="row">
										<div class="col-md-12">
											<div class="profile-img-wrap edit-img">
												<asp:Image ID="editimg" runat="server" />
												<div class="fileupload btn">						
													<asp:FileUpload ID="imgupload" runat="server" />
												</div>
											</div>
											<div class="row">
												<div class="col-md-6">
													<div class="input-block mb-3">
														<label class="col-form-label">Name</label>
														<asp:TextBox ID="txtname" runat="server" CssClass="form-control"></asp:TextBox>
													</div>
												</div>
												<div class="col-md-6">
													<div class="input-block mb-3">
														<label class="col-form-label">Emp Id</label>
														<asp:TextBox ID="txtempid" runat="server" CssClass="form-control" ReadOnly="true" ></asp:TextBox>
													</div>
												</div>
												<div class="col-md-6">
													<div class="input-block mb-3">
														<label class="col-form-label">OTP</label>
														<asp:TextBox ID="txtotp" runat="server" CssClass="form-control"></asp:TextBox>
													</div>
												</div>
												<div class="col-md-6">
													<div class="input-block mb-3">
														<label class="col-form-label">Company</label>
															<asp:TextBox ID="txtcompany" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
													</div>
												</div>
												<div class="col-md-6">
													<div class="input-block mb-3">
														<label class="col-form-label">Gender</label>
														<select class="select form-control">
															<option value="male selected">Male</option>
															<option value="female">Female</option>
														</select>
													</div>
												</div>
											</div>
										</div>
									</div>
									<div class="row">
										<div class="col-md-12">
											<div class="input-block mb-3">
												<label class="col-form-label">Email</label>
												<asp:TextBox ID="txtemail" runat="server" CssClass="form-control" AutoPostBack="true"></asp:TextBox>
											</div>
										</div>
										<div class="col-md-6">
											<div class="input-block mb-3">
												<label class="col-form-label">Designation</label>
												<asp:TextBox ID="txtdesignation" runat="server" CssClass="form-control" AutoPostBack="true"></asp:TextBox>
											</div>
										</div>
										<div class="col-md-6">
											<div class="input-block mb-3">
												<label class="col-form-label">Branch</label>
											<asp:TextBox ID="txtbranch" runat="server" CssClass="form-control" AutoPostBack="true"></asp:TextBox>
											</div>
										</div>
										<div class="col-md-6">
											<div class="input-block mb-3">
												<label class="col-form-label">Role</label>
												<asp:TextBox ID="txtrole" runat="server" CssClass="form-control"></asp:TextBox>
											</div>
										</div>
										<div class="col-md-6">
											<div class="input-block mb-3">
												<label class="col-form-label">DeviceCode</label>
												<asp:TextBox ID="txtdevicecode" runat="server" CssClass="form-control"></asp:TextBox>
											</div>
										</div>
						<%--				<div class="col-md-6">
											<div class="input-block mb-3">
												<label class="col-form-label">Department <span class="text-danger">*</span></label>
												<select class="select">
													<option>Select Department</option>
													<option>Web Development</option>
													<option>IT Management</option>
													<option>Marketing</option>
												</select>
											</div>
										</div>
										<div class="col-md-6">
											<div class="input-block mb-3">
												<label class="col-form-label">Designation <span class="text-danger">*</span></label>
												<select class="select">
													<option>Select Designation</option>
													<option>Web Designer</option>
													<option>Web Developer</option>
													<option>Android Developer</option>
												</select>
											</div>
										</div>--%>
										<div class="col-md-6">
											<div class="input-block mb-3">
												<label class="col-form-label ">Reports To <span class="text-danger">*</span></label>
												<select class="select form-control">
													<option>-</option>
													<option>Manager</option>

												</select>
											</div>
										</div>
									</div>
									<div class="submit-section">
										<asp:Button ID="btneditsubmit" runat="server" Text="Update" CssClass="btn btn-primary submit-btn" OnClick="btneditsubmit_Click" />
									</div>
							</div>
						</div>
					</div>
				</div>
				<!-- /Profile Modal -->
				
				<!-- Personal Info Modal Personal Information text box update popup -->
				<div id="personal_info_modal" class="modal custom-modal fade" role="dialog">
					<div class="modal-dialog modal-dialog-centered modal-lg" role="document">
						<div class="modal-content">
							<div class="modal-header">
								<h5 class="modal-title">Personal Information</h5>
								<button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close">
									<span aria-hidden="true">&times;</span>
								</button>
							</div>
							<div class="modal-body">
								
									<div class="row">
										<div class="col-md-6">
											<div class="input-block mb-3">
												<label class="col-form-label">Employee Name</label>
												<asp:TextBox ID="txtpername" runat="server" CssClass="form-control"></asp:TextBox>
											</div>
										</div>
										<div class="col-md-6">
											<div class="input-block mb-3">
												<label class="col-form-label">DOB</label>												
													<asp:TextBox ID="txtperdob" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>												
											</div>
										</div>
										<div class="col-md-6">
											<div class="input-block mb-3">
												<label class="col-form-label">Mobile</label>
											<asp:TextBox ID="txpertmobile" runat="server" CssClass="form-control"></asp:TextBox>
											</div>
										</div>
										<div class="col-md-6">
											<div class="input-block mb-3">
												<label class="col-form-label">BloodGroup <span class="text-danger">*</span></label>
												<asp:TextBox ID="txtperbloodgroup" runat="server" CssClass="form-control"></asp:TextBox>
											</div>
										</div>
										<div class="col-md-6">
											<div class="input-block mb-3">
												<label class="col-form-label">Education</label>
													<asp:TextBox ID="txtpereducation" runat="server" CssClass="form-control"></asp:TextBox>
											</div>
										</div>
										<div class="col-md-6">
											<div class="input-block mb-3">
												<label class="col-form-label">Marital status <span class="text-danger">*</span></label>
												<asp:DropDownList ID="ddlmaritialstatus" runat="server" CssClass="select form-control" onmousedown="hideFirstItem()" >
													<asp:ListItem> -- Select --</asp:ListItem>
													<asp:ListItem>Single</asp:ListItem>
													<asp:ListItem>Married</asp:ListItem>
												</asp:DropDownList>
											</div>
										</div>
										<div class="col-md-6">
											<div class="input-block mb-3">
												<label class="col-form-label">Gender <span class="text-danger">*</span></label>
												<asp:DropDownList ID="dddlgender" runat="server" CssClass="select form-control" onmousedown="hideFirststatus()" >
													<asp:ListItem> -- Select --</asp:ListItem>
													<asp:ListItem>Male</asp:ListItem>
													<asp:ListItem>FeMale</asp:ListItem>
												</asp:DropDownList>
											</div>
										</div>
										<div class="col-md-6">
											<div class="input-block mb-3">
												<label class="col-form-label">PersonalEmail </label>
												<asp:TextBox ID="txtperemail" runat="server" CssClass="form-control"></asp:TextBox>
											</div>
										</div>
									</div>
									<div class="submit-section">
										<asp:Button ID="btnpersonaldetailpopup" runat="server" Text="Update" CssClass="btn btn-primary submit-btn" OnClick="btnpersonaldetailpopup_Click" />
									</div>

							</div>
						</div>
					</div>
				</div>
				<!-- /Personal Info Modal -->
				
				<!-- Family Info Modal -->
				<div id="family_info_modal" class="modal custom-modal fade" role="dialog">
					<div class="modal-dialog modal-dialog-centered modal-lg" role="document">
						<div class="modal-content">
							<div class="modal-header">
								<h5 class="modal-title"> Family Informations</h5>
								<button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close">
									<span aria-hidden="true">&times;</span>
								</button>
							</div>
							<div class="modal-body">
									<div class="form-scroll">
										<div class="card">
											<div class="card-body">
												<h3 class="card-title">Family Member <a href="javascript:void(0);" class="delete-icon"></a></h3><%--<i class="fa-regular fa-trash-can"></i>--%>
												<div class="row">
													<div class="col-md-6">
														<div class="input-block mb-3">
															<label class="col-form-label">Name <span class="text-danger">*</span></label>
														    <asp:TextBox ID="TextBox5" runat="server" CssClass="form-control"></asp:TextBox>
														</div>
													</div>
													<div class="col-md-6">
														<div class="input-block mb-3">
															<label class="col-form-label">Relationship <span class="text-danger">*</span></label>
															<asp:TextBox ID="TextBox6" runat="server" CssClass="form-control"></asp:TextBox>
														</div>
													</div>
													<div class="col-md-6">
														<div class="input-block mb-3">
															<label class="col-form-label">Date of birth <span class="text-danger">*</span></label>
															<asp:TextBox ID="TextBox7" runat="server" CssClass="form-control"></asp:TextBox>
														</div>
													</div>
													<div class="col-md-6">
														<div class="input-block mb-3">
															<label class="col-form-label">Phone <span class="text-danger">*</span></label>
															<asp:TextBox ID="TextBox8" runat="server" CssClass="form-control"></asp:TextBox>
														</div>
													</div>
												</div>
											</div>
										</div>
										
										<div class="card">
											<div class="card-body">
												<h3 class="card-title">Education Informations <a href="javascript:void(0);" class="delete-icon"></a></h3><%--<i class="fa-regular fa-trash-can"></i>--%>
												<div class="row">
													<div class="col-md-6">
														<div class="input-block mb-3">
															<label class="col-form-label">Name <span class="text-danger">*</span></label>
														<asp:TextBox ID="TextBox9" runat="server" CssClass="form-control"></asp:TextBox>
														</div>
													</div>
													<div class="col-md-6">
														<div class="input-block mb-3">
															<label class="col-form-label">Relationship <span class="text-danger">*</span></label>
														<asp:TextBox ID="TextBox10" runat="server" CssClass="form-control"></asp:TextBox>
														</div>
													</div>
													<div class="col-md-6">
														<div class="input-block mb-3">
															<label class="col-form-label">Date of birth <span class="text-danger">*</span></label>
														    <asp:TextBox ID="TextBox11" runat="server" CssClass="form-control"></asp:TextBox>
														</div>
													</div>
													<div class="col-md-6">
														<div class="input-block mb-3">
															<label class="col-form-label">Phone <span class="text-danger">*</span></label>
															    <asp:TextBox ID="TextBox12" runat="server" CssClass="form-control"></asp:TextBox>
														</div>
													</div>
												</div>
											<%--	<div class="add-more">
													<a href="javascript:void(0);"><i class="fa-solid fa-plus-circle"></i> Add More</a>
												</div>--%>
											</div>
										</div>
									</div>
									<div class="submit-section">
										<%--<button class="btn btn-primary submit-btn">Submit</button>--%>
										<asp:Button ID="btnfamilyinfopopup" runat="server" Text="Button" CssClass="btn btn-primary submit-btn" OnClick="btnfamilyinfopopup_Click" />
									</div>
							</div>
						</div>
					</div>
				</div>
				<!-- /Family Info Modal -->
				
				<!-- Emergency Contact Modal emergency contact primary and secondary textbox update popup -->
				<div id="emergency_contact_modal" class="modal custom-modal fade" role="dialog">
					<div class="modal-dialog modal-dialog-centered modal-lg" role="document">
						<div class="modal-content">
							<div class="modal-header">
								<h5 class="modal-title">Emergancy Contact Details </h5>
								<button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close">
									<span aria-hidden="true">&times;</span>
								</button>
							</div>
							<div class="modal-body">						
									<div class="card">
										<div class="card-body">
											<h3 class="card-title">Primary Detils</h3>
											<div class="row">
												<div class="col-md-6">
													<div class="input-block mb-3">
														<label class="col-form-label">Name <span class="text-danger">*</span></label>
														<asp:TextBox ID="txtfathernamepri" runat="server" CssClass="form-control"></asp:TextBox>
													</div>
												</div>
												<div class="col-md-6">
													<div class="input-block mb-3">
														<label class="col-form-label">Relationship <span class="text-danger">*</span></label>
														<asp:TextBox ID="txtfathernamefix" runat="server" CssClass="form-control" Text="Father"></asp:TextBox>
													</div>
												</div>
												<div class="col-md-6">
													<div class="input-block mb-3">
													<label class="col-form-label">Phone <span class="text-danger">*</span></label>
													<asp:TextBox ID="txtfatheradar" runat="server" CssClass="form-control"></asp:TextBox>
													</div>
												</div>
												<div class="col-md-6">
													<div class="input-block mb-3">
														<label class="col-form-label">Phone 2<span class="text-danger">*</span></label>
													   <asp:TextBox ID="txtfather2adhar" runat="server" CssClass="form-control"></asp:TextBox>
													</div>
												</div>
											</div>
										</div>
									</div>
									
									<div class="card">
										<div class="card-body">
											<h3 class="card-title">Primary Contact</h3>
											<div class="row">
												<div class="col-md-6">
													<div class="input-block mb-3">
														<label class="col-form-label">Name <span class="text-danger">*</span></label>
														<asp:TextBox ID="txtemcmothername" runat="server" CssClass="form-control"></asp:TextBox>
													</div>
												</div>
												<div class="col-md-6">
													<div class="input-block mb-3">
														<label class="col-form-label">Relationship <span class="text-danger">*</span></label>
														<asp:TextBox ID="TextBox13" runat="server" CssClass="form-control" Text="Mother"></asp:TextBox>
													</div>
												</div>
												<div class="col-md-6">
													<div class="input-block mb-3">
														<label class="col-form-label">Phone <span class="text-danger">*</span></label>
													<asp:TextBox ID="txtemcmotheraadhar1" runat="server" CssClass="form-control"></asp:TextBox>
													</div>
												</div>
												<div class="col-md-6">
													<div class="input-block mb-3">
														<label class="col-form-label">Phone 2<span class="text-danger">*</span></label>
														<asp:TextBox ID="txtemcmotheraadhar2" runat="server" CssClass="form-control"></asp:TextBox>
													</div>
												</div>
											</div>
										</div>
									</div>
									<div class="submit-section">
										<asp:Button ID="btnemccontactdetailsupdate" runat="server" Text="Update" CssClass="btn btn-primary submit-btn" OnClick="btnemccontactdetailsupdate_Click" />
									</div>
							</div>
						</div>
					</div>
				</div>
				<!-- /Emergency Contact Modal -->
				
				<!-- Education Modal -->
				<div id="education_info" class="modal custom-modal fade" role="dialog">
					<div class="modal-dialog modal-dialog-centered modal-lg" role="document">
						<div class="modal-content">
							<div class="modal-header">
								<h5 class="modal-title"> Education Informations</h5>
								<button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close">
									<span aria-hidden="true">&times;</span>
								</button>
							</div>
							<div class="modal-body">
									<div class="form-scroll">
										<div class="card">
											<div class="card-body">
												<h3 class="card-title">Last Two Education Informations <a href="javascript:void(0);" class="delete-icon"><i class="fa-regular fa-trash-can"></i></a></h3>
												<div class="row">
													<div class="col-md-6">
														<div class="input-block mb-3 form-focus focused">											
															<asp:TextBox ID="TextBox14" runat="server" class="form-control floating"></asp:TextBox>
															<label class="focus-label">Institution</label>
														</div>
													</div>
													<div class="col-md-6">
														<div class="input-block mb-3 form-focus focused">
															<asp:TextBox ID="TextBox15" runat="server" class="form-control floating"></asp:TextBox>
															<label class="focus-label">Subject</label>
														</div>
													</div>
													<div class="col-md-6">
														<div class="input-block mb-3 form-focus focused">
															<asp:TextBox ID="TextBox16" runat="server" class="form-control floating"></asp:TextBox>
															<label class="focus-label">Starting Date</label>
														</div>
													</div>
													<div class="col-md-6">
														<div class="input-block mb-3 form-focus focused">
															<asp:TextBox ID="TextBox17" runat="server" class="form-control floating"></asp:TextBox>
															<label class="focus-label">Complete Date</label>
														</div>
													</div>
													<div class="col-md-6">
														<div class="input-block mb-3 form-focus focused">
															<asp:TextBox ID="TextBox18" runat="server" class="form-control floating"></asp:TextBox>
															<label class="focus-label">Degree</label>
														</div>
													</div>
													<div class="col-md-6">
														<div class="input-block mb-3 form-focus focused">
															<asp:TextBox ID="TextBox19" runat="server" class="form-control floating"></asp:TextBox>
															<label class="focus-label">Grade</label>
														</div>
													</div>
												</div>
											</div>
										</div>
										
										<div class="card">
											<div class="card-body">
												<h3 class="card-title">Education Informations <a href="javascript:void(0);" class="delete-icon"><i class="fa-regular fa-trash-can"></i></a></h3>
												<div class="row">
													<div class="col-md-6">
														<div class="input-block mb-3 form-focus focused">
															<asp:TextBox ID="TextBox20" runat="server" class="form-control floating"></asp:TextBox>
															<label class="focus-label">Institution</label>
														</div>
													</div>
													<div class="col-md-6">
														<div class="input-block mb-3 form-focus focused">
															<asp:TextBox ID="TextBox21" runat="server" class="form-control floating"></asp:TextBox>
															<label class="focus-label">Subject</label>
														</div>
													</div>
													<div class="col-md-6">
														<div class="input-block mb-3 form-focus focused">
															<asp:TextBox ID="TextBox22" runat="server" class="form-control floating"></asp:TextBox>
															<label class="focus-label">Starting Date</label>
														</div>
													</div>
													<div class="col-md-6">
														<div class="input-block mb-3 form-focus focused">
															<asp:TextBox ID="TextBox23" runat="server" class="form-control floating"></asp:TextBox>
															<label class="focus-label">Complete Date</label>
														</div>
													</div>
													<div class="col-md-6">
														<div class="input-block mb-3 form-focus focused">
													    	<asp:TextBox ID="TextBox24" runat="server" class="form-control floating"></asp:TextBox>
															<label class="focus-label">Degree</label>
														</div>
													</div>
													<div class="col-md-6">
														<div class="input-block mb-3 form-focus focused">
													     	<asp:TextBox ID="TextBox25" runat="server" class="form-control floating"></asp:TextBox>
															<label class="focus-label">Grade</label>
														</div>
													</div>
												</div>
												<%--<div class="add-more">
													<a href="javascript:void(0);"><i class="fa-solid fa-plus-circle"></i> Add More</a>
												</div>--%>
											</div>
										</div>
									</div>
									<div class="submit-section">									
									 	<asp:Button ID="btneducationinfopopup" runat="server" Text="Update" CssClass="btn btn-primary submit-btn" />
									</div>
							</div>
						</div>
					</div>
				</div>
				<!-- /Education Modal -->
				
				<!-- Experience Modal -->
				<div id="experience_info" class="modal custom-modal fade" role="dialog">
					<div class="modal-dialog modal-dialog-centered modal-lg" role="document">
						<div class="modal-content">
							<div class="modal-header">
								<h5 class="modal-title">Last Two Experience Informations</h5>
								<button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close">
									<span aria-hidden="true">&times;</span>
								</button>
							</div>
							<div class="modal-body">
							<%--	<form>--%>
									<div class="form-scroll">
										<div class="card">
											<div class="card-body">
												<h3 class="card-title">Experience Informations <a href="javascript:void(0);" class="delete-icon"><i class="fa-regular fa-trash-can"></i></a></h3>
												<div class="row">
													<div class="col-md-6">
														<div class="input-block mb-3 form-focus">															
															<asp:TextBox ID="TextBox26" runat="server" class="form-control floating"></asp:TextBox>
															<label class="focus-label">Company Name</label>
														</div>
													</div>
													<div class="col-md-6">
														<div class="input-block mb-3 form-focus">
															<asp:TextBox ID="TextBox27" runat="server" class="form-control floating"></asp:TextBox>
															<label class="focus-label">Location</label>
														</div>
													</div>
													<div class="col-md-6">
														<div class="input-block mb-3 form-focus">
														    <asp:TextBox ID="TextBox28" runat="server" class="form-control floating"></asp:TextBox>
															<label class="focus-label">Job Position</label>
														</div>
													</div>
													<div class="col-md-6">
														<div class="input-block mb-3 form-focus">
											     			<asp:TextBox ID="TextBox29" runat="server" class="form-control floating"></asp:TextBox>
															<label class="focus-label">Period From</label>
														</div>
													</div>
													<div class="col-md-6">
														<div class="input-block mb-3 form-focus">
															<asp:TextBox ID="TextBox30" runat="server" class="form-control floating"></asp:TextBox>
															<label class="focus-label">Period To</label>
														</div>
													</div>
												</div>
											</div>
										</div>
										
										<div class="card">
											<div class="card-body">
												<h3 class="card-title">Experience Informations <a href="javascript:void(0);" class="delete-icon"><i class="fa-regular fa-trash-can"></i></a></h3>
												<div class="row">
													<div class="col-md-6">
														<div class="input-block mb-3 form-focus">
																<asp:TextBox ID="TextBox31" runat="server" class="form-control floating"></asp:TextBox>
															<label class="focus-label">Company Name</label>
														</div>
													</div>
													<div class="col-md-6">
														<div class="input-block mb-3 form-focus">
																<asp:TextBox ID="TextBox32" runat="server" class="form-control floating"></asp:TextBox>
															<label class="focus-label">Location</label>
														</div>
													</div>
													<div class="col-md-6">
														<div class="input-block mb-3 form-focus">
																<asp:TextBox ID="TextBox33" runat="server" class="form-control floating"></asp:TextBox>
															<label class="focus-label">Job Position</label>
														</div>
													</div>
													<div class="col-md-6">
														<div class="input-block mb-3 form-focus">
															<asp:TextBox ID="TextBox34" runat="server" class="form-control floating"></asp:TextBox>
															<label class="focus-label">Period From</label>
														</div>
													</div>
													<div class="col-md-6">
														<div class="input-block mb-3 form-focus">
															<asp:TextBox ID="TextBox35" runat="server" class="form-control floating"></asp:TextBox>
															<label class="focus-label">Period To</label>
														</div>
													</div>
												</div>
												<%--<div class="add-more">
													<a href="javascript:void(0);"><i class="fa-solid fa-plus-circle"></i> Add More</a>
												</div>--%>
											</div>
										</div>
									</div>
									<div class="submit-section">
										<asp:Button ID="btnexppopup" runat="server" Text="Update" CssClass="btn btn-primary submit-btn" />
									</div>
								<%--</form>--%>
							</div>
						</div>
					</div>
				</div>
				<!-- /Experience Modal -->
				
            </div>
			<!-- /Page Wrapper -->
			<div id="emergency_contact_modal2" class="modal custom-modal fade" role="dialog">
					<div class="modal-dialog modal-dialog-centered modal-lg" role="document">
						<div class="modal-content">
							<div class="modal-header">
								<h5 class="modal-title">Bank Information </h5>
								<button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close">
									<span aria-hidden="true">&times;</span>
								</button>
							</div>
							<div class="modal-body">						
									<div class="card">
										<div class="card-body">
											<h3 class="card-title">Primary Detils</h3>
											<div class="row">
												<div class="col-md-6">
													<div class="input-block mb-3">
														<label class="col-form-label">Bank Name <span class="text-danger">*</span></label>
														<asp:TextBox ID="TextBox1" runat="server" CssClass="form-control"></asp:TextBox>
													</div>
												</div>
												<div class="col-md-6">
													<div class="input-block mb-3">
														<label class="col-form-label">Bank Account Number <span class="text-danger">*</span></label>
														<asp:TextBox ID="TextBox2" runat="server" CssClass="form-control"></asp:TextBox>
													</div>
												</div>
												<div class="col-md-6">
													<div class="input-block mb-3">
														<label class="col-form-label">IFSC  <span class="text-danger">*</span></label>
													<asp:TextBox ID="TextBox3" runat="server" CssClass="form-control"></asp:TextBox>
													</div>
												</div>
												<div class="col-md-6">
													<div class="input-block mb-3">
														<label class="col-form-label">Pan Number</label>
													   <asp:TextBox ID="TextBox4" runat="server" CssClass="form-control"></asp:TextBox>
													</div>
												</div>
											</div>
										</div>
									</div>
																		
									<div class="submit-section">
								     <asp:Button ID="btnbankupdatepopup" runat="server" Text="Update" CssClass="btn btn-primary submit-btn" OnClick="btnbankupdatepopup_Click"  />
									</div>
							</div>
						</div>
					</div>
				</div>


	<%--	</form>--%>
</asp:Content>
