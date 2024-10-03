<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="AdminPerformanceAppraisal.aspx.cs" Inherits="Human_Resource_Management.AdminperformanceAppraisal" %>
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
							<h3 class="page-title">Performance Appraisal</h3>
							<ul class="breadcrumb">
								<li class="breadcrumb-item"><a href="Admindashboard">Dashboard</a></li>
								<li class="breadcrumb-item active">Performance</li>
							</ul>
						</div>
						<div class="col-auto float-end ms-auto">
							<a href="#" class="btn add-btn" data-bs-toggle="modal" data-bs-target="#add_appraisal"><i class="fa-solid fa-plus"></i> Add New</a>
						</div>
					</div>
				</div>
				<!-- /Page Header -->
					
				<div class="row">
					<div class="col-md-12">
						<div class="table-responsive">
							<table class="table table-striped custom-table mb-0 datatable">
								<thead>
									<tr>
										<th>EmpId</th>
										<th>Name</th>
										<th>Quality</th>
										<th>Time</th>
										<th>Process</th>
										<th>Team Management</th>
									    <th>Knowledge Sharing</th>																			
										<th>Communications</th>
										<th>Edit</th>
									</tr>
								</thead>
								<tbody>
									<asp:PlaceHolder ID="AllPerformance" runat="server"></asp:PlaceHolder>
								</tbody>
							</table>
						</div>
					</div>
				</div>
            </div>
			<!-- /Page Content -->

			<!-- Add Performance Appraisal Modal -->
			<div id="add_appraisal" class="modal custom-modal fade" role="dialog">
				<div class="modal-dialog modal-dialog-centered modal-lg" role="document">
					<div class="modal-content">
						<div class="modal-header">
							<h5 class="modal-title">Give Performance Appraisal</h5>
							<button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close">
								<span aria-hidden="true">&times;</span>
							</button>
						</div>
						<div class="modal-body">
								<div class="row">
									<div class="col-sm-12">
										<div class="input-block mb-3">
											<label class="col-form-label">Employee</label>
											<select class="select">
												<option>Select Employee</option>
												<option>John Doe</option>
												<option>Mike Litorus</option>
											</select>
										</div>
										<div class="input-block mb-3">
											<label class="col-form-label">Select Date <span class="text-danger">*</span></label>
											<div class="cal-icon"><input class="form-control datetimepicker" type="text"></div>
										</div>
									</div>
									<div class="col-sm-12">
										<div class="card">
											<div class="card-body">
												<div class="tab-box">
													<div class="row user-tabs">
														<div class="col-lg-12 col-md-12 col-sm-12 line-tabs">
															<ul class="nav nav-tabs nav-tabs-solid">
																<li class="nav-item"><a href="#appr_technical" data-bs-toggle="tab" class="nav-link active">Technical</a></li>
																<li class="nav-item"><a href="#appr_organizational" data-bs-toggle="tab" class="nav-link">Organizational</a></li>
															</ul>
														</div>
													</div>
												</div>
												<div class="tab-content">
													<div id="appr_technical" class="pro-overview tab-pane fade show active">
														<div class="row">
															<div class="col-sm-12">
																<div class="bg-white">
																	<table class="table">
																		<thead>
																			<tr>
																			<th>Technical Competencies</th>
																			<th></th>
																			<th></th>
																			<th></th>
																			<th></th>
																			</tr>
																		</thead>
																		<tbody>
																			<tr>
																			<th colspan="2">Indicator</th>
																			<th colspan="2">Expected Value</th>
																			<th>Set Value</th>
																			</tr>
																			<tr>
																			<td colspan="2">Customer Experience</td>
																			<td colspan="2">Intermediate</td>
																			<td><select name="customer_experience" class="form-control form-select">
																				<option value="">None</option>
																				<option value="1"> Beginner</option>
																				<option value="2"> Intermediate</option>
																				<option value="3"> Advanced</option>
																				<option value="4"> Expert / Leader</option>
																				</select></td>
																			</tr>
																			<tr>
																			<td colspan="2">Marketing</td>
																			<td colspan="2">Advanced</td>
																			<td><select name="marketing" class="form-control form-select">
																				<option value="">None</option>
																				<option value="1"> Beginner</option>
																				<option value="2"> Intermediate</option>
																				<option value="3"> Advanced</option>
																				<option value="4"> Expert / Leader</option>
																				</select></td>
																			</tr>
																			<tr>
																			<td colspan="2">Management</td>
																			<td colspan="2">Advanced</td>
																			<td><select name="management" class="form-control form-select">
																				<option value="">None</option>
																				<option value="1"> Beginner</option>
																				<option value="2"> Intermediate</option>
																				<option value="3"> Advanced</option>
																				<option value="4"> Expert / Leader</option>
																				</select></td>
																			</tr>
																			<tr>
																			<td colspan="2">Administration</td>
																			<td colspan="2">Advanced</td>
																			<td><select name="administration" class="form-control form-select">
																				<option value="">None</option>
																				<option value="1"> Beginner</option>
																				<option value="2"> Intermediate</option>
																				<option value="3"> Advanced</option>
																				<option value="4"> Expert / Leader</option>
																				</select></td>
																			</tr>
																			<tr>
																			<td colspan="2">Presentation Skill</td>
																			<td colspan="2">Expert / Leader</td>
																			<td><select name="presentation_skill" class="form-control form-select">
																				<option value="">None</option>
																				<option value="1"> Beginner</option>
																				<option value="2"> Intermediate</option>
																				<option value="3"> Advanced</option>
																				<option value="4"> Expert / Leader</option>
																				</select></td>
																			</tr>
																			<tr>
																			<td colspan="2">Quality Of Work</td>
																			<td colspan="2">Expert / Leader</td>
																			<td><select name="quality_of_work" class="form-control form-select">
																				<option value="">None</option>
																				<option value="1"> Beginner</option>
																				<option value="2"> Intermediate</option>
																				<option value="3"> Advanced</option>
																				<option value="4"> Expert / Leader</option>
																				</select></td>
																			</tr>
																			<tr>
																			<td colspan="2">Efficiency</td>
																			<td colspan="2">Expert / Leader</td>
																			<td><select name="efficiency" class="form-control form-select">
																				<option value="">None</option>
																				<option value="1"> Beginner</option>
																				<option value="2"> Intermediate</option>
																				<option value="3"> Advanced</option>
																				<option value="4"> Expert / Leader</option>
																				</select></td>
																			</tr>
																		</tbody>
																	</table>
																</div>
															</div>
														</div>
													</div>
													<div class="tab-pane fade" id="appr_organizational">
														<div class="row">
															<div class="col-sm-12">
																<div class="bg-white">
																	<table class="table">
																		<thead>
																			<tr>
																				<th>Organizational Competencies</th>
																				<th></th>
																			<th></th>
																			<th></th>
																			<th></th>
																			</tr>
																			</thead>
																			<tbody>
																			<tr>
																				<th colspan="2">Indicator</th>
																				<th colspan="2">Expected Value</th>
																				<th>Set Value</th>
																			</tr>
																			<tr>
																				<td colspan="2">Integrity</td>
																				<td colspan="2">Beginner</td>
																				<td>
																					<select name="integrity" class="form-control form-select">
																						<option value="">None</option>
																						<option value="1"> Beginner</option>
																						<option value="2"> Intermediate</option>
																						<option value="3"> Advanced</option>
																					</select>
																				</td>
																			</tr>
																			<tr>
																				<td colspan="2">Professionalism</td>
																				<td colspan="2">Beginner</td>
																				<td>
																					<select name="professionalism" class="form-control form-select">
																						<option value="">None</option>
																						<option value="1"> Beginner</option>
																						<option value="2"> Intermediate</option>
																						<option value="3"> Advanced</option>
																					</select>
																				</td>
																			</tr>
																			<tr>
																				<td colspan="2">Team Work</td>
																				<td colspan="2">Intermediate</td>
																				<td>
																					<select name="team_work" class="form-control form-select">
																						<option value="">None</option>
																						<option value="1"> Beginner</option>
																						<option value="2"> Intermediate</option>
																						<option value="3"> Advanced</option>
																					</select>
																				</td>
																			</tr>
																			<tr>
																				<td colspan="2">Critical Thinking</td>
																				<td colspan="2">Advanced</td>
																				<td>
																					<select name="critical_thinking" class="form-control form-select">
																						<option value="">None</option>
																						<option value="1"> Beginner</option>
																						<option value="2"> Intermediate</option>
																						<option value="3"> Advanced</option>
																					</select>
																				</td>
																			</tr>
																			<tr>
																				<td colspan="2">Conflict Management</td>
																				<td colspan="2">Intermediate</td>
																				<td>
																					<select name="conflict_management" class="form-control form-select">
																						<option value="">None</option>
																						<option value="1"> Beginner</option>
																						<option value="2"> Intermediate</option>
																						<option value="3"> Advanced</option>
																					</select>
																				</td>
																			</tr>
																			<tr>
																				<td colspan="2">Attendance</td>
																				<td colspan="2">Intermediate</td>
																				<td>
																					<select name="attendance" class="form-control form-select">
																						<option value="">None</option>
																						<option value="1"> Beginner</option>
																						<option value="2"> Intermediate</option>
																						<option value="3"> Advanced</option>
																					</select>
																				</td>
																			</tr>
																			<tr>
																				<td colspan="2">Ability To Meet Deadline</td>
																				<td colspan="2">Advanced</td>
																				<td>
																					<select name="ability_to_meet_deadline" class="form-control form-select">
																						<option value="">None</option>
																						<option value="1"> Beginner</option>
																						<option value="2"> Intermediate</option>
																						<option value="3"> Advanced</option>
																					</select>
																				</td>
																			</tr>
																		</tbody>
																	</table>
																</div>
															</div>
														</div>
													</div>
												</div>
											</div>
										</div>
									</div>
									<div class="col-sm-12">
										<div class="input-block mb-3">
											<label class="col-form-label">Status</label>
											<select class="select">
												<option>Active</option>
												<option>Inactive</option>
											</select>
										</div>
									</div>
								</div>
									
								<div class="submit-section">
									<button class="btn btn-primary submit-btn">Submit</button>
								</div>
						</div>
					</div>
				</div>
			</div>
			<!-- /Add Performance Appraisal Modal -->
				
			<!-- Edit Performance Appraisal Modal -->
			<div id="edit_appraisal" class="modal custom-modal fade" role="dialog">
				<div class="modal-dialog modal-dialog-centered modal-lg" role="document">
					<div class="modal-content">
						<div class="modal-header">
							<h5 class="modal-title">Edit Performance Appraisal</h5>
							<button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close">
								<span aria-hidden="true">&times;</span>
							</button>
						</div>
						<div class="modal-body">
								<div class="row">
									<div class="col-sm-6">
										<div class="input-block mb-6">
											<label class="col-form-label">Employee Id</label><asp:HiddenField ID="HiddenField1" runat="server" />
											 <asp:TextBox ID="TextBox8" runat="server" class="form-control" AutoCompleteType="Disabled"></asp:TextBox>
										</div>
										<div class="input-block mb-6">
											<label class="col-form-label">Name <span class="text-danger">*</span></label>
											 <asp:TextBox ID="TextBox1" runat="server" class="form-control" AutoCompleteType="Disabled"></asp:TextBox>
										</div>
									</div>
									<div class="col-sm-12">
										<div class="card">
											<div class="card-body">
												<div class="tab-box">
													<div class="row user-tabs">
														<div class="col-lg-12 col-md-12 col-sm-12 line-tabs">
															<ul class="nav nav-tabs nav-tabs-solid">
																<li class="nav-item"><a href="#appr_technical1" data-bs-toggle="tab" class="nav-link active">Technical</a></li>
																<li class="nav-item"><a href="#appr_organizational1" data-bs-toggle="tab" class="nav-link">Organizational</a></li>																	
															</ul>
														</div>
													</div>
												</div>
												<div class="tab-content">
													<div id="appr_technical1" class="pro-overview tab-pane fade show active">
														<div class="row">
															<div class="col-sm-12">
																<div class="bg-white">
																	<table class="table">
																		<thead>
																			<tr>
																				<th>Technical Competencies</th>
																				<th></th>
																				<th></th>
																				<th></th>
																				<th></th>
																			</tr>
																		</thead>
																		<tbody>
																			<tr>
																				<th colspan="2">Indicator</th>
																				<th colspan="2">Employee Score</th>
																				<th>Manager Score</th>
																			</tr>
																			<tr>
																				<td colspan="2">Production Quantity</td>
																				<td colspan="2"><asp:TextBox ID="TextBox2" runat="server" class="form-control" ReadOnly="true"></asp:TextBox></td>
																				<td>
																					<asp:TextBox ID="TextBox3" runat="server" class="form-control" AutoCompleteType="Disabled" oninput="validateRange(this)" onkeypress="return isNumberKey(event)"></asp:TextBox>
																				</td>
																			</tr>
																			<tr>
																				<td colspan="2">Production TAT</td>
																				<td colspan="2"><asp:TextBox ID="TextBox4" runat="server" class="form-control" ReadOnly="true"></asp:TextBox></td>
																				<td>
																					<asp:TextBox ID="TextBox5" runat="server" class="form-control" AutoCompleteType="Disabled" oninput="validateRange(this)" onkeypress="return isNumberKey(event)"></asp:TextBox>
																				</td>
																			</tr>
																			<tr>
																				<td colspan="2">Process Improvement</td>
																				<td colspan="2"><asp:TextBox ID="TextBox6" runat="server" class="form-control" ReadOnly="true"></asp:TextBox></td>
																				<td>
																					<asp:TextBox ID="TextBox7" runat="server" class="form-control" AutoCompleteType="Disabled" oninput="validateRange(this)" onkeypress="return isNumberKey(event)"></asp:TextBox>
																				</td>
																			</tr>
																			<tr>
																				<td colspan="2">Team Management</td>
																				<td colspan="2"><asp:TextBox ID="TextBox9" runat="server" class="form-control" ReadOnly="true"></asp:TextBox></td>
																				<td>
																					<asp:TextBox ID="TextBox10" runat="server" class="form-control" AutoCompleteType="Disabled" oninput="validateRange(this)" onkeypress="return isNumberKey(event)"></asp:TextBox>
																				</td>
																			</tr>
																			<tr>
																				<td colspan="2">Knowledge Sharing</td>
																				<td colspan="2"><asp:TextBox ID="TextBox11" runat="server" class="form-control" ReadOnly="true"></asp:TextBox></td>
																				<td>
																					<asp:TextBox ID="TextBox12" runat="server" class="form-control" AutoCompleteType="Disabled" oninput="validateRange(this)" onkeypress="return isNumberKey(event)"></asp:TextBox>
																				</td>
																			</tr>
																			<tr>
																				<td colspan="2">Reporting and Communication</td>
																				<td colspan="2"><asp:TextBox ID="TextBox13" runat="server" class="form-control" ReadOnly="true"></asp:TextBox></td>
																				<td>
																					<asp:TextBox ID="TextBox14" runat="server" class="form-control" AutoCompleteType="Disabled" oninput="validateRange(this)" onkeypress="return isNumberKey(event)"></asp:TextBox>
																				</td>
																			</tr>
																			
																		</tbody>
																	</table>
																</div><asp:Button ID="btnpersonal" runat="server" Text="Submit" CssClass="btn btn-primary submit-btn" OnClick="btnpersonal_Click" />
															</div>
														</div>
													</div>
													
													<div class="tab-pane fade" id="appr_organizational1">
														<div class="row">
															<div class="col-sm-12">
																<div class="bg-white">
																	<table class="table">
																		<thead>
																			<tr>
																				<th>Organizational Competencies</th>
																				<th></th>
																				<th></th>
																				<th></th>
																				<th></th>
																			</tr>
																		</thead>
																		<tbody>
																			<tr>
																				<th colspan="2">Indicator</th>
																				<th colspan="2">Expected Value</th>
																				<th>Set Value</th>
																			</tr>
																			<tr>
																				<td colspan="2">Planned or Unplanned Leaves</td>
																				<td colspan="2"><asp:TextBox ID="TextBox16" runat="server" class="form-control" AutoCompleteType="Disabled" ReadOnly="true"></asp:TextBox></td>
																				<td>
																					<asp:TextBox ID="TextBox15" runat="server" class="form-control" AutoCompleteType="Disabled" onkeypress="return isNumberKey(event)" oninput="validateRange(this)"></asp:TextBox>
																				</td>
																			</tr>
																			<tr>
																				<td colspan="2">Time Consciousness</td>
																				<td colspan="2"><asp:TextBox ID="TextBox17" runat="server" class="form-control" AutoCompleteType="Disabled" ReadOnly="true"></asp:TextBox></td>
																				<td>
																					<asp:TextBox ID="TextBox18" runat="server" class="form-control" AutoCompleteType="Disabled" onkeypress="return isNumberKey(event)" oninput="validateRange(this)"></asp:TextBox>
																				</td>
																			</tr>
																			<tr>
																				<td colspan="2">Team Collaboration</td>
																				<td colspan="2"><asp:TextBox ID="TextBox19" runat="server" class="form-control" AutoCompleteType="Disabled" ReadOnly="true"></asp:TextBox></td>
																				<td>
																					<asp:TextBox ID="TextBox20" runat="server" class="form-control" AutoCompleteType="Disabled" onkeypress="return isNumberKey(event)" oninput="validateRange(this)"></asp:TextBox>
																				</td>
																			</tr>
																			<tr>
																				<td colspan="2">Professionalism & Responsiveness</td>
																				<td colspan="2"><asp:TextBox ID="TextBox21" runat="server" class="form-control" AutoCompleteType="Disabled" ReadOnly="true"></asp:TextBox></td>
																				<td>
																					<asp:TextBox ID="TextBox22" runat="server" class="form-control" AutoCompleteType="Disabled" onkeypress="return isNumberKey(event)" oninput="validateRange(this)"></asp:TextBox>
																				</td>
																			</tr>
																			<tr>
																				<td colspan="2">Adherence to policies and procedures</td>
																				<td colspan="2"><asp:TextBox ID="TextBox23" runat="server" class="form-control" AutoCompleteType="Disabled" ReadOnly="true"></asp:TextBox></td>
																				<td>
																					<asp:TextBox ID="TextBox24" runat="server" class="form-control" AutoCompleteType="Disabled" onkeypress="return isNumberKey(event)" oninput="validateRange(this)"></asp:TextBox>
																				</td>
																			</tr>
																			<tr>
																				<td colspan="2">Special Efforts, Suggestions,Ideas,etc.</td>
																				<td colspan="2"><asp:TextBox ID="TextBox25" runat="server" class="form-control" AutoCompleteType="Disabled" ReadOnly="true"></asp:TextBox></td>
																				<td>
																					<asp:TextBox ID="TextBox26" runat="server" class="form-control" AutoCompleteType="Disabled" onkeypress="return isNumberKey(event)" oninput="validateRange(this)"></asp:TextBox>
																				</td>
																			</tr>
																			<tr>
																				<td colspan="2"> Training utilization</td>
																				<td colspan="2"><asp:TextBox ID="TextBox27" runat="server" class="form-control" AutoCompleteType="Disabled" ReadOnly="true"></asp:TextBox></td>
																				<td>
																					<asp:TextBox ID="TextBox28" runat="server" class="form-control" AutoCompleteType="Disabled" onkeypress="return isNumberKey(event)" oninput="validateRange(this)"></asp:TextBox>
																				</td>
																			</tr>
																		</tbody>
																		
																	</table>
																</div><asp:Button ID="btnprofesional" runat="server" Text="Submit" CssClass="btn btn-primary submit-btn" OnClick="btnprofesional_Click" />
															</div>
														</div>
													</div>
												</div>
											</div>
										</div>
									</div>
									<div class="col-sm-12">
										<div class="input-block mb-3">
											
										</div>
									</div>
								</div>
								<div class="submit-section">
									<%--<button class="btn btn-primary submit-btn">Save2</button>--%>
								</div>
						</div>
					</div>
				</div>
			</div>
			<!-- /Edit Performance Appraisal Modal -->
				
			<!-- Delete Performance Appraisal Modal -->
			<div class="modal custom-modal fade" id="delete_appraisal" role="dialog">
				<div class="modal-dialog modal-dialog-centered">
					<div class="modal-content">
						<div class="modal-body">
							<div class="form-header">
								<h3>Delete Performance Appraisal List</h3>
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
			<!-- /Delete Performance Appraisal Modal -->		
        </div>
		<!-- /Page Wrapper -->
    </div>
	<!-- /Main Wrapper -->


	<script type="text/javascript">
        function editappraisal(Id , Name , ProQuantitySelf , PersPlanUnplanLeavesSelf , ProTATSelf , PersExTimeConsciousnessSelf , ProPMSSelf , PersExTeamColaborationSelf , ProTPDSelf , PersExProfessionalismSelf , ProTeamKnowledgeSelf , PersExAdpoliciesSelf , ProCommunicationSelf , PersExSpecialEffortsSelf , PersExTrainingUtilizationSelf)
		{
			$('#<%= TextBox8.ClientID %>').val(Id).prop('readonly', true);
            $('#<%= HiddenField1.ClientID %>').val(Id);
            $('#<%= TextBox1.ClientID %>').val(Name).prop('readonly', true);
			$('#<%= TextBox2.ClientID %>').val(ProQuantitySelf);
            $('#<%= TextBox16.ClientID %>').val(PersPlanUnplanLeavesSelf);
			$('#<%= TextBox4.ClientID %>').val(ProTATSelf);
            $('#<%= TextBox17.ClientID %>').val(PersExTimeConsciousnessSelf);
			$('#<%= TextBox6.ClientID %>').val(ProPMSSelf);
            $('#<%= TextBox19.ClientID %>').val(PersExTeamColaborationSelf);
			$('#<%= TextBox9.ClientID %>').val(ProTPDSelf);
            $('#<%= TextBox21.ClientID %>').val(PersExProfessionalismSelf);
			$('#<%= TextBox11.ClientID %>').val(ProTeamKnowledgeSelf);
            $('#<%= TextBox23.ClientID %>').val(PersExAdpoliciesSelf);
			$('#<%= TextBox13.ClientID %>').val(ProCommunicationSelf);
            $('#<%= TextBox25.ClientID %>').val(PersExSpecialEffortsSelf);
            $('#<%= TextBox27.ClientID %>').val(PersExTrainingUtilizationSelf);
          
		}
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }
        function validateRange(input) {
            var value = input.value;
            if (value !== "" && (value < 0 || value > 100)) {
                alert("Please enter a number between 0 and 100.");
                input.value = "";
            }
        }
    </script>
</asp:Content>
