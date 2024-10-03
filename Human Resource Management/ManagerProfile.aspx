<%@ Page Title="" Language="C#" MasterPageFile="~/Manager.Master" AutoEventWireup="true" CodeBehind="ManagerProfile.aspx.cs" Inherits="Human_Resource_Management.ManagerProfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style type="text/css">
        .bedit {
            border: none;
            outline: none;
            background-color: none;
        }

        .bedit:hover {
            border: none;
            outline: none;
            color: white;
            background-color: #27a160;
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

        .select > :nth-child(1) {
            color: red;
        }
        .personal-info-container {
            display: flex;
        }

        .left-column {
            border-right: 2px dashed #ccc; /* Dotted line with light gray color */
            padding-right: 20px; /* Optional padding for spacing */
        }

        .right-column {
            padding-left: 20px; /* Optional padding for spacing */
        }
    </style>
    <script type="text/javascript">

        function hideFirststatusddlgender() {
            var select = document.getElementById('<%= dddlgender.ClientID %>');
            if (select && select.options.length > 0) {
                select.options[0].style.display = 'none';
            }
        }

        function hideFirstItemddlmaried() {
            var select = document.getElementById('<%= ddlmaritialstatus.ClientID %>');
            if (select && select.options.length > 0) {
                select.options[0].style.display = 'none';
            }
        }

    </script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Page Wrapper -->
    <div class="page-wrapper">

        <!-- Page Content -->
        <div class="content container-fluid">

            <div class="page-header">
                <div class="row">
                    <div class="col-sm-12">
                        <h3 class="page-title">Profile</h3>
                        <ul class="breadcrumb">
                            <li class="breadcrumb-item"><a href="EmployeeDashboard.aspx">Dashboard</a></li>
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
                                        <a href="#">
                                            <asp:Image ID="empimage" runat="server" /></a>
                                    </div>
                                </div>
                                <div class="profile-basic">
                                    <div class="row personal-info-container">
                                        <div class="col-md-5 left-column">
                                            <ul class="personal-info">
                                                <li>
                                                    <div>
                                                        <h3>
                                                            <asp:Label ID="lblempname" runat="server" ForeColor="#ff0000"></asp:Label>
                                                        </h3>
                                                    </div>
                                                </li>
                                                <li>
                                                    <div class="title">
                                                        Designation :
                                                    </div>
                                                    <div class="text">
                                                        <asp:Label ID="lbldesignation" runat="server"></asp:Label>
                                                    </div>
                                                </li>
                                                <li>
                                                    <div class="title">
                                                        Department :
                                                    </div>
                                                    <div class="text">
                                                        <asp:Label ID="lbldepartment" runat="server"></asp:Label>
                                                    </div>
                                                </li>
                                                <li>
                                                    <div class="title">
                                                        Employee Id :
                                                    </div>
                                                    <div class="text">
                                                        <asp:Label ID="lblempid" runat="server"></asp:Label>
                                                    </div>
                                                </li>
                                                <li>
                                                    <div class="title">
                                                        DOJ :
                                                    </div>
                                                    <div class="text">
                                                        <asp:Label ID="lbldoj" runat="server"></asp:Label>
                                                    </div>
                                                </li>
                                                <%-- <div class="staff-msg"><a class="btn btn-custom" href="chat.html">Send Message</a></div>--%>
                                            </ul>
                                        </div>
                                        <div class="col-md-7 right-column">
                                            <ul class="personal-info">
                                                <li>
                                                    <div class="title">Company:</div>
                                                    <div class="text">
                                                        <asp:Label ID="lblcompany" runat="server"></asp:Label>
                                                    </div>
                                                </li>
                                                <li>
                                                    <div class="title">Branch:</div>
                                                    <div class="text">
                                                        <asp:Label ID="lblbranch" runat="server"></asp:Label>
                                                    </div>
                                                </li>
                                                <li>
                                                    <div class="title">Email:</div>
                                                    <div class="text">
                                                        <span class="__cf_email__" data-cfemail="f09a9f989e949f95b09588919d809c95de939f9d">
                                                            <asp:Label ID="lblemail" runat="server" Text="Label"></asp:Label></span>
                                                    </div>
                                                </li>
                                                <li>
                                                    <div class="title">Role:</div>
                                                    <div class="text">
                                                        <asp:Label ID="lblrole" runat="server"></asp:Label>
                                                    </div>
                                                </li>
                                                <li>
                                                    <div class="title">DeviceCode:</div>
                                                    <div class="text">
                                                        <asp:Label ID="lbldevicecode" runat="server"></asp:Label>
                                                    </div>
                                                </li>
                                                <li>
                                                    <div class="title">Reports to:</div>
                                                    <div class="text">
                                                        <div class="avatar-box">
                                                            <div class="avatar avatar-xs">
                                                                <%-- <img src="assets/img/profiles/avatar-16.jpg" alt="User Image">--%>
                                                                <asp:Image ID="reporterimage" runat="server" />
                                                            </div>
                                                        </div>
                                                        <a href="profile.html">
                                                            <asp:Label ID="lblreportername" runat="server"></asp:Label>
                                                        </a>
                                                    </div>
                                                </li>
                                            </ul>
                                        </div>
                                    </div>
                                </div>
                                <div class="pro-edit">
                                    <a data-bs-target="#profile_info" data-bs-toggle="modal" class="edit-icon" href="#">
                                        <i class='fa-solid fa-pencil m-r-5'></i></a>
                                </div>
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
                            <li class="nav-item"><a href="#bank_statutory" data-bs-toggle="tab" class="nav-link">Bank & Statutory <small class="text-danger"></small></a></li>
                           <%-- <li class="nav-item"><a href="#emp_assets" data-bs-toggle="tab" class="nav-link">Assets</a></li>--%>
                        </ul>
                    </div>
                </div>
            </div>

            <div class="tab-content">

                <!-- Profile Info Tab -->
                <div id="emp_profile" class="pro-overview tab-pane fade show active">
                    <div class="row">
                        <div class="col-md-6 d-flex">
                            <div class="card profile-box flex-fill">
                                <div class="card-body">
                                    <h3 class="card-title" style="color: #850fa8;">Personal Informationss <a href="#" class="edit-icon" data-bs-toggle="modal" data-bs-target="#personal_info_modal">
                                        <i class='fa-solid fa-pencil m-r-5'></i></a></h3>
                                    <ul class="personal-info">
                                        <li>
                                            <div class="title">Name </div>
                                            <asp:Label ID="lblpername" runat="server"></asp:Label>
                                        </li>
                                        <li>
                                            <div class="title">DOB  </div>
                                            <asp:Label ID="lblperdob" runat="server"></asp:Label>
                                        </li>
                                        <li>
                                            <div class="title">Gender</div>
                                            <asp:Label ID="lblpergender" runat="server"></asp:Label>
                                        </li>
                                        <li>
                                            <div class="title">Mobile</div>
                                            <asp:Label ID="lblpermobile" runat="server"></asp:Label>
                                        </li>
                                        <li>
                                            <div class="title">BloodGroup</div>
                                            <asp:Label ID="lblperbloodgroup" runat="server"></asp:Label>
                                        </li>
                                        <li>
                                            <div class="title">Emp Type</div>
                                            <asp:Label ID="lblpereducation" runat="server"></asp:Label>
                                        </li>
                                        <li>
                                            <div class="title">Pf Type</div>
                                            <asp:Label ID="lblpermaritialstatus" runat="server"></asp:Label>
                                        </li>
                                        <li>
                                            <div class="title">Email</div>
                                            <asp:Label ID="lblperemail" runat="server"></asp:Label>
                                        </li>
                                    </ul>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6 d-flex">
                            <div class="card profile-box flex-fill">
                                <div class="card-body">
                                    <h3 class="card-title" style="color: #850fa8;">Family Contacts <a href="#" class="edit-icon" data-bs-toggle="modal" data-bs-target="#emergency_contact_modal"><i class='fa-solid fa-pencil m-r-5'></i></a></h3>
                                    <h5 class="section-title" style="color: #ad8036;">Primary Information</h5>
                                    <ul class="personal-info">
                                        <li>
                                            <div class="title">Name</div>
                                            <asp:Label ID="lblemcfathername" runat="server"></asp:Label>
                                        </li>
                                        <li>
                                            <div class="title">Relationship</div>
                                            <asp:Label ID="Label10" runat="server" Text="Father"></asp:Label>
                                        </li>
                                        <li>
                                            <div class="title">Phone </div>
                                            <asp:Label ID="lblemcfatherphone" runat="server"></asp:Label>
                                        </li>
                                    </ul>
                                    <hr>
                                    <h5 class="section-title" style="color: #ad8036;">Secondary Information</h5>
                                    <ul class="personal-info">
                                        <li>
                                            <div class="title">Name</div>
                                            <asp:Label ID="lblemcmothername" runat="server"></asp:Label>
                                        </li>
                                        <li>
                                            <div class="title">Relationship</div>
                                            <asp:Label ID="Label13" runat="server" Text="Mother"></asp:Label>
                                        </li>
                                        <li>
                                            <div class="title">Phone </div>
                                            <asp:Label ID="lblemcmothermobile" runat="server"></asp:Label>
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
                                    <h3 class="card-title" style="color: #850fa8;">Bank informations<a href="#" class="edit-icon" data-bs-toggle="modal" data-bs-target="#emergency_contact_modal2"><i class='fa-solid fa-pencil m-r-5'></i></a></h3>

                                    <ul class="personal-info">
                                        <li>
                                            <div class="title">Bank Name</div>
                                            <asp:Label ID="lblbankname" runat="server"></asp:Label>
                                        </li>
                                        <li>
                                            <div class="title">Bank Account No.</div>
                                            <asp:Label ID="lblbankaccount" runat="server"></asp:Label>
                                        </li>
                                        <li>
                                            <div class="title">IFSC Code</div>
                                            <asp:Label ID="lblbankifsc" runat="server"></asp:Label>
                                        </li>
                                        <li>
                                            <div class="title">PAN No</div>
                                            <asp:Label ID="lblbankpan" runat="server"></asp:Label>
                                        </li>
                                    </ul>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6 d-flex">
                            <div class="card profile-box flex-fill">
                                <div class="card-body">
                                    <h3 class="card-title" style="color: #850fa8;">Nominee Information <a href="#" class="edit-icon" data-bs-toggle="modal" data-bs-target="#family_info_modal"><i class='fa-solid fa-pencil m-r-5'></i></a></h3>
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
                                                    <td>
                                                        <asp:Label ID="lblnomineename" runat="server"></asp:Label></td>
                                                    <td>
                                                        <asp:Label ID="lblnominerelation" runat="server"></asp:Label></td>
                                                    <td>
                                                        <asp:Label ID="lblnomineeaadhar" runat="server"></asp:Label></td>
                                                    <td>
                                                        <asp:Label ID="lblnomineemobile" runat="server"></asp:Label></td>
                                                    <%--<td class="text-end">
															<div class="dropdown dropdown-action">
																<a aria-expanded="false" data-bs-toggle="dropdown" class="action-icon dropdown-toggle" href="#"><i class="material-icons">more_vert</i></a>
																<div class="dropdown-menu dropdown-menu-right">
																	<a href="#" class="dropdown-item"><i class="fa-solid fa-pencil m-r-5"></i> Edit</a>
																	<a href="#" class="dropdown-item"><i class="fa-regular fa-trash-can m-r-5"></i> Delete</a>
																</div>
															</div>
														</td>--%>
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
                                    <h3 class="card-title" style="color: #850fa8;">Education Informations <a href="#" class="edit-icon" data-bs-toggle="modal" data-bs-target="#education_info"><i class='fa-solid fa-pencil m-r-5'></i></a></h3>
                                    <div class="experience-box">
                                        <ul class="experience-list">
                                            <li>
                                                <div class="experience-user">
                                                    <div class="before-circle"></div>
                                                </div>
                                                <div class="experience-content">
                                                    <div class="timeline-content">
                                                        <a href="#/" class="name">
                                                            <asp:Label ID="Label1" runat="server"></asp:Label></a>
                                                        <div>
                                                            <asp:Label ID="Label2" runat="server"></asp:Label>
                                                        </div>
                                                        <span class="time">
                                                            <asp:Label ID="Label3" runat="server"></asp:Label></span>
                                                    </div>
                                                </div>
                                            </li>
                                            <li>
                                                <div class="experience-user">
                                                    <div class="before-circle"></div>
                                                </div>
                                                <div class="experience-content">
                                                    <div class="timeline-content">
                                                        <a href="#/" class="name">
                                                            <asp:Label ID="Label11" runat="server"></asp:Label></a>
                                                        <div>
                                                            <asp:Label ID="Label12" runat="server"></asp:Label>
                                                        </div>
                                                        <span class="time">
                                                            <asp:Label ID="Label14" runat="server"></asp:Label></span>
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
                                    <h3 class="card-title" style="color: #850fa8;">Experience <a href="#" class="edit-icon" data-bs-toggle="modal" data-bs-target="#experience_info"><i class='fa-solid fa-pencil m-r-5'></i></a></h3>
                                    <div class="experience-box">
                                        <ul class="experience-list">
                                            <li>
                                                <div class="experience-user">
                                                    <div class="before-circle"></div>
                                                </div>
                                                <div class="experience-content">
                                                    <div class="timeline-content">
                                                        <a href="#/" class="name">
                                                            <asp:Label ID="Label4" runat="server"></asp:Label></a>
                                                        <span class="time">
                                                            <asp:Label ID="Label5" runat="server"></asp:Label></span>
                                                    </div>
                                                </div>
                                            </li>
                                            <li>
                                                <div class="experience-user">
                                                    <div class="before-circle"></div>
                                                </div>
                                                <div class="experience-content">
                                                    <div class="timeline-content">
                                                        <a href="#/" class="name">
                                                            <asp:Label ID="Label6" runat="server"></asp:Label></a>
                                                        <span class="time">
                                                            <asp:Label ID="Label7" runat="server"></asp:Label></span>
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
                    </div>
                </div>
                <!-- /Projects Tab -->

                <!-- Bank Statutory Tab -->
                <div class="tab-pane fade" id="bank_statutory">
                    <div class="card">
                        <div class="card-body">
                            <h3 class="card-title">Basic Salary Information</h3>
                            <div class="row">
                                <div class="col-sm-4">
                                    <div class="input-block mb-3">
                                        <label class="col-form-label">Salary basis <span class="text-danger">*</span></label>
                                        <%--<asp:DropDownList ID="ddlhmstatus" runat="server" CssClass="select form-control" onmousedown="hidehmsalarystatus()" >
												<asp:ListItem> -- Select --</asp:ListItem>
												<asp:ListItem>Weekly</asp:ListItem>
												<asp:ListItem>Monthly</asp:ListItem>
											</asp:DropDownList>--%>
                                        <asp:TextBox ID="TextBox38" runat="server" CssClass="form-control" placeholder="Type Your Salary Amount" ReadOnly="true" Text="Monthly"></asp:TextBox>
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
                                <%--<div class="col-sm-4">
                                   <div class="input-block mb-3">
                                       <label class="col-form-label">Payment type</label>
                                       <asp:DropDownList ID="ddlpaymenttype" runat="server" CssClass="select form-control" onmousedown="hideddlpaytype12()">
                                           <asp:ListItem> -- Select --</asp:ListItem>
                                           <asp:ListItem>Bank transfer</asp:ListItem>
                                           <asp:ListItem>Check</asp:ListItem>
                                           <asp:ListItem>Cash</asp:ListItem>
                                       </asp:DropDownList>
                                   </div>
                               </div>--%>
                            </div>
                            <hr>
                            <h3 class="card-title">PF Information</h3>
                            <div class="row">
                                <div class="col-sm-4">
                                    <div class="input-block mb-3">
                                        <label class="col-form-label">PF contribution</label>
                                        <asp:TextBox ID="TextBox39" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="input-block mb-3">
                                        <label class="col-form-label">PF No. <span class="text-danger">*</span></label>
                                        <asp:TextBox ID="TextBox40" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-4">
                                    <div class="input-block mb-3">
                                        <label class="col-form-label">Employee PF rate</label>
                                        <asp:TextBox ID="TextBox6" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="input-block mb-3">
                                        <label class="col-form-label">Employer PF rate <span class="text-danger">*</span></label>
                                        <asp:TextBox ID="TextBox7" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="input-block mb-3">
                                        <label class="col-form-label">Total rate</label>
                                        <asp:TextBox ID="TextBox36" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                            </div>


                            <hr>
                            <h3 class="card-title">ESI Information</h3>
                            <div class="row">
                                <div class="col-sm-4">
                                    <div class="input-block mb-3">
                                        <label class="col-form-label">ESI contribution</label>
                                        <asp:TextBox ID="TextBox41" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="input-block mb-3">
                                        <label class="col-form-label">ESI No. <span class="text-danger">*</span></label>
                                        <asp:TextBox ID="TextBox42" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-4">
                                    <div class="input-block mb-3">
                                        <label class="col-form-label">Employee ESI rate</label>
                                        <asp:TextBox ID="TextBox8" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="input-block mb-3">
                                        <label class="col-form-label">Employer ESI rate <span class="text-danger">*</span></label>
                                        <asp:TextBox ID="TextBox9" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="input-block mb-3">
                                        <label class="col-form-label">Total rate</label>
                                        <asp:TextBox ID="TextBox37" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
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


                <%--<div class="tab-pane fade" id="emp_assets" style="overflow-y:scroll;height:300px;width:100%;">
						
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
                      </div>--%>
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
                                            <asp:TextBox ID="txtempid" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="input-block mb-3">
                                            <label class="col-form-label">Department</label>
                                            <asp:TextBox ID="txtdepartment" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
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
                                            <label class="col-form-label">DOJ</label>
                                            <asp:TextBox ID="txtDOJ" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="input-block mb-3">
                                            <label class="col-form-label">CompanyMobile</label>
                                            <asp:TextBox ID="txtcompanymobile" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <div class="input-block mb-3">
                                    <label class="col-form-label">Email</label>
                                    <asp:TextBox ID="txtemail" runat="server" CssClass="form-control" AutoPostBack="true"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="input-block mb-3">
                                    <label class="col-form-label">Designation</label>
                                    <asp:TextBox ID="txtdesignation" runat="server" CssClass="form-control" AutoPostBack="true" ReadOnly="true"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="input-block mb-3">
                                    <label class="col-form-label">Branch</label>
                                    <asp:TextBox ID="txtbranch" runat="server" CssClass="form-control" AutoPostBack="true" ReadOnly="true"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="input-block mb-3">
                                    <label class="col-form-label">Role</label>
                                    <asp:TextBox ID="txtrole" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="input-block mb-3">
                                    <label class="col-form-label">BranchCode</label>
                                    <asp:TextBox ID="txtbranchcode" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="input-block mb-3">
                                    <label class="col-form-label ">Reports To <span class="text-danger">*</span></label>
                                    <asp:TextBox ID="TextBox5" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
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

        <!-- Personal Info Modal -->
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
                                    <label class="col-form-label">Name</label>
                                    <asp:TextBox ID="txtpername" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="input-block mb-3">
                                    <label class="col-form-label">Sur Name</label>
                                    <asp:TextBox ID="txtsurname" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="input-block mb-3">
                                    <label class="col-form-label">DOJ</label>
                                    <asp:TextBox ID="txtperDOJ" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
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
                                    <label class="col-form-label">EmployeeType</label>
                                    <asp:TextBox ID="txtemployeetype" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="input-block mb-3">
                                    <label class="col-form-label">Marital status <span class="text-danger">*</span></label>
                                    <asp:DropDownList ID="ddlmaritialstatus" runat="server" CssClass="select form-control" onmousedown="hideFirstItemddlmaried()">
                                        <asp:ListItem> -- Select --</asp:ListItem>
                                        <asp:ListItem>Single</asp:ListItem>
                                        <asp:ListItem>Married</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="input-block mb-3">
                                    <label class="col-form-label">Gender <span class="text-danger">*</span></label>
                                    <asp:DropDownList ID="dddlgender" runat="server" CssClass="select form-control" onmousedown="hideFirststatusddlgender()">
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
                        <h5 class="modal-title">Nominee Informations</h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <div class="form-scroll">
                            <div class="card">
                                <div class="card-body">
                                    <h3 class="card-title">Nominee Member <a href="javascript:void(0);" class="delete-icon"></a></h3>
                                    <%--<i class="fa-regular fa-trash-can"></i>--%>
                                    <div class="row">
                                        <div class="col-md-6">
                                            <div class="input-block mb-3">
                                                <label class="col-form-label">Name <span class="text-danger">*</span></label>
                                                <asp:TextBox ID="TextBox19" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="input-block mb-3">
                                                <label class="col-form-label">Relationship <span class="text-danger">*</span></label>
                                                <asp:TextBox ID="TextBox24" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="input-block mb-3">
                                                <label class="col-form-label">Aadhar <span class="text-danger">*</span></label>
                                                <asp:TextBox ID="TextBox25" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="input-block mb-3">
                                                <label class="col-form-label">Phone <span class="text-danger">*</span></label>
                                                <asp:TextBox ID="TextBox28" runat="server" CssClass="form-control"></asp:TextBox>
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

        <!-- Emergency Contact Modal -->
        <div id="emergency_contact_modal" class="modal custom-modal fade" role="dialog">
            <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title">Family Contact Details </h5>
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
                                            <asp:TextBox ID="txtfathernamepri" runat="server" CssClass="form-control" AutoCompleteType="Disabled"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="input-block mb-3">
                                            <label class="col-form-label">Relationship <span class="text-danger">*</span></label>
                                            <asp:TextBox ID="txtfathernamerelation1" runat="server" CssClass="form-control" AutoCompleteType="Disabled"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="input-block mb-3">
                                            <label class="col-form-label">Phone <span class="text-danger">*</span></label>
                                            <asp:TextBox ID="txtfatherphone" runat="server" CssClass="form-control" AutoCompleteType="Disabled"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="input-block mb-3">
                                            <label class="col-form-label">Aadhar No<span class="text-danger">*</span></label>
                                            <asp:TextBox ID="txtfather1adhar" runat="server" CssClass="form-control" AutoCompleteType="Disabled"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="card">
                            <div class="card-body">
                                <h3 class="card-title">Secondary Contact</h3>
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="input-block mb-3">
                                            <label class="col-form-label">Name <span class="text-danger">*</span></label>
                                            <asp:TextBox ID="txtemcmothername" runat="server" CssClass="form-control" AutoCompleteType="Disabled"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="input-block mb-3">
                                            <label class="col-form-label">Relationship <span class="text-danger">*</span></label>
                                            <asp:TextBox ID="txtmotherrelation" runat="server" CssClass="form-control" AutoCompleteType="Disabled"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="input-block mb-3">
                                            <label class="col-form-label">Phone <span class="text-danger">*</span></label>
                                            <asp:TextBox ID="txtmotherphone" runat="server" CssClass="form-control" AutoCompleteType="Disabled"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="input-block mb-3">
                                            <label class="col-form-label">Aadhar No<span class="text-danger">*</span></label>
                                            <asp:TextBox ID="txtemcmotheraadhar2" runat="server" CssClass="form-control" AutoCompleteType="Disabled"></asp:TextBox>
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
                        <h5 class="modal-title">Education Informations</h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <div class="form-scroll">
                            <div class="card">
                                <div class="card-body">
                                    <h3 class="card-title">Last One Education Informations <a href="javascript:void(0);" class="delete-icon"></a></h3>
                                    <div class="row">
                                        <div class="col-md-6">
                                            <div class="input-block mb-3 form-focus focused">
                                                <asp:TextBox ID="TextBox14" runat="server" class="form-control floating"></asp:TextBox>
                                                <label class="focus-label">University</label>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="input-block mb-3 form-focus focused">
                                                <asp:TextBox ID="TextBox15" runat="server" class="form-control floating"></asp:TextBox>
                                                <label class="focus-label">Degree</label>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="input-block mb-3 form-focus focused">
                                                <asp:TextBox ID="TextBox16" runat="server" class="form-control floating"></asp:TextBox>
                                                <label class="focus-label">Complete Date</label>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="input-block mb-3 form-focus focused">
                                                <asp:TextBox ID="TextBox17" runat="server" class="form-control floating"></asp:TextBox>
                                                <label class="focus-label">Percentage</label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="card">
                                <div class="card-body">
                                    <h3 class="card-title">Last Two Education Informations <a href="javascript:void(0);" class="delete-icon"></a></h3>
                                    <div class="row">
                                        <div class="col-md-6">
                                            <div class="input-block mb-3 form-focus focused">
                                                <asp:TextBox ID="TextBox20" runat="server" class="form-control floating"></asp:TextBox>
                                                <label class="focus-label">University</label>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="input-block mb-3 form-focus focused">
                                                <asp:TextBox ID="TextBox21" runat="server" class="form-control floating"></asp:TextBox>
                                                <label class="focus-label">Degree</label>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="input-block mb-3 form-focus focused">
                                                <asp:TextBox ID="TextBox22" runat="server" class="form-control floating"></asp:TextBox>
                                                <label class="focus-label">Complete Date</label>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="input-block mb-3 form-focus focused">
                                                <asp:TextBox ID="TextBox23" runat="server" class="form-control floating"></asp:TextBox>
                                                <label class="focus-label">Percentage</label>
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
                            <asp:Button ID="btneducationinfopopupupdate" runat="server" Text="Education Update" CssClass="btn btn-primary submit-btn" OnClick="btneducationinfopopupupdate_Click" />
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
                                    <h3 class="card-title">Experience First Informations <a href="javascript:void(0);" class="delete-icon"></a></h3>
                                    <div class="row">
                                        <div class="col-md-6">
                                            <div class="input-block mb-3 form-focus focused">
                                                <asp:TextBox ID="TextBox26" runat="server" class="form-control floating"></asp:TextBox>
                                                <label class="focus-label">Company Name</label>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="input-block mb-3 form-focus focused">
                                                <asp:TextBox ID="TextBox27" runat="server" class="form-control floating"></asp:TextBox>
                                                <label class="focus-label">SalaryAnnum</label>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="input-block mb-3 form-focus focused">
                                                <asp:TextBox ID="txtlastonedesignation" runat="server" class="form-control floating"></asp:TextBox>
                                                <label class="focus-label">Last1CompanyDesignation</label>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="input-block mb-3 form-focus focused">
                                                <asp:TextBox ID="TextBox1" runat="server" class="form-control floating"></asp:TextBox>
                                                <label class="focus-label">From1Year</label>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="input-block mb-3 form-focus focused">
                                                <asp:TextBox ID="TextBox2" runat="server" class="form-control floating"></asp:TextBox>
                                                <label class="focus-label">To1Year</label>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="input-block mb-3 form-focus focused">
                                                <asp:TextBox ID="TextBox3" runat="server" class="form-control floating"></asp:TextBox>
                                                <label class="focus-label">ContactNumber1</label>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                            </div>

                            <div class="card">
                                <div class="card-body">
                                    <h3 class="card-title">Experience Second Informations <a href="javascript:void(0);" class="delete-icon"></a></h3>
                                    <div class="row">
                                        <div class="col-md-6">
                                            <div class="input-block mb-3 form-focus focused">
                                                <asp:TextBox ID="TextBox31" runat="server" class="form-control floating"></asp:TextBox>
                                                <label class="focus-label">Company Name</label>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="input-block mb-3 form-focus focused">
                                                <asp:TextBox ID="TextBox32" runat="server" class="form-control floating"></asp:TextBox>
                                                <label class="focus-label">SalaryAnnum</label>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="input-block mb-3 form-focus focused">
                                                <asp:TextBox ID="txtlasttwodesignation" runat="server" class="form-control floating"></asp:TextBox>
                                                <label class="focus-label">Last2CompanyDesignation</label>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="input-block mb-3 form-focus focused">
                                                <asp:TextBox ID="TextBox4" runat="server" class="form-control floating"></asp:TextBox>
                                                <label class="focus-label">From2Year</label>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="input-block mb-3 form-focus focused">
                                                <asp:TextBox ID="TextBox13" runat="server" class="form-control floating"></asp:TextBox>
                                                <label class="focus-label">To2Year</label>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="input-block mb-3 form-focus focused">
                                                <asp:TextBox ID="TextBox18" runat="server" class="form-control floating"></asp:TextBox>
                                                <label class="focus-label">ContactNumber2</label>
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
                            <asp:Button ID="btnexpupdate" runat="server" Text="Update" CssClass="btn btn-primary submit-btn" OnClick="btnexpupdate_Click" />
                        </div>
                        <%--</form>--%>
                    </div>
                </div>
            </div>
        </div>
        <!-- /Experience Modal -->

    </div>
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
                                        <asp:TextBox ID="txtbankname" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="input-block mb-3">
                                        <label class="col-form-label">Bank Account Number <span class="text-danger">*</span></label>
                                        <asp:TextBox ID="txtbankaccountnumber" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="input-block mb-3">
                                        <label class="col-form-label">IFSC  <span class="text-danger">*</span></label>
                                        <asp:TextBox ID="txtifsc" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="input-block mb-3">
                                        <label class="col-form-label">Pan Number</label>
                                        <asp:TextBox ID="txtpanno" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="submit-section">
                        <asp:Button ID="btnbankupdatepopup" runat="server" Text="Update" CssClass="btn btn-primary submit-btn" OnClick="btnbankupdatepopup_Click" />
                    </div>
                </div>
            </div>
        </div>
    </div>
   
</asp:Content>
