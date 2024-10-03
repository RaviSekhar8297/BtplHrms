<%@ Page Title="" Language="C#" MasterPageFile="~/Employee.Master" AutoEventWireup="true" CodeBehind="EmployeeSelfApraisal.aspx.cs" Inherits="Human_Resource_Management.EmployeeSelfApraisal" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Page Wrapper -->
	
    <div class="page-wrapper">
			
		<!-- Page Content -->
        <div class="content container-fluid">
				
			<!-- Page Header -->
			<div class="page-header">
				<div class="row">
					<div class="col-sm-12">
						<h3 class="page-title">Performance</h3>
						<ul class="breadcrumb">
							<li class="breadcrumb-item"><a href="EmployeeDashboard.aspx">Dashboard</a></li>
							<li class="breadcrumb-item active">Performance</li>
						</ul>
					</div>
				</div>
			</div>
			<!-- /Page Header -->
					
			<section class="review-section information">
				<div class="review-header text-center">
					<h3 class="review-title">Employee Basic Information</h3>
					<p class="text-muted"></p>
				</div>
				<div class="row">
					<div class="col-md-12 col-sm-12">
						<div class="table-responsive">
							<table class="table table-bordered table-nowrap review-table mb-0">
								<tbody>
									<tr>
										<td>
												<div class="input-block mb-3">
													<label for="name">NAME</label>												
													<asp:TextBox ID="txtname" runat="server" CssClass="form-control" ReadOnly="true" ForeColor="#148048"></asp:TextBox>
												</div>
												<div class="input-block mb-3">
													<label for="depart3">Department</label>
													<asp:TextBox ID="txtdepartment" runat="server" CssClass="form-control" ReadOnly="true" ForeColor="#148048"></asp:TextBox>
												</div>
												<div class="input-block mb-3">
													<label for="departa">Designation</label>
											    	<asp:TextBox ID="txtdesignation" runat="server" CssClass="form-control" ReadOnly="true" ForeColor="#148048"></asp:TextBox>
												</div>
												<div class="input-block mb-3">
													<label for="qualif">Qualification: </label>
												<asp:TextBox ID="txtqualification" runat="server" CssClass="form-control" ReadOnly="true" ForeColor="#148048"></asp:TextBox>
												</div>
										</td>
										<td>
												<div class="input-block mb-3">
													<label for="doj">Emp ID</label>
													<asp:TextBox ID="txtempid" runat="server" CssClass="form-control" ReadOnly="true" ForeColor="#148048"></asp:TextBox>
												</div>
												<div class="input-block mb-3">
													<label for="doj">DATE OF JOIN</label>
													<asp:TextBox ID="txtdoj" runat="server" CssClass="form-control" ReadOnly="true" ForeColor="#148048"></asp:TextBox>
												</div>
												<div class="input-block mb-3">
													<label for="doc">DATE OF BIRTH</label>
													<asp:TextBox ID="txtdob" runat="server" CssClass="form-control" ReadOnly="true" ForeColor="#148048"></asp:TextBox>
												</div>
												<div class="input-block mb-3">
													<label for="qualif1">Previous years of Exp</label>
													<asp:TextBox ID="txtprevex" runat="server" CssClass="form-control" ReadOnly="true" ForeColor="#148048"></asp:TextBox>
												</div>
										</td>
										<td>
												<div class="input-block mb-3">
													<label for="name1"> RO's Name</label>
													<asp:TextBox ID="txtro1" runat="server" CssClass="form-control" ReadOnly="true" ForeColor="#148048"></asp:TextBox>
												</div>
												<div class="input-block mb-3">
													<label for="depart1"> RO Designation: </label>
													<asp:TextBox ID="txtro2" runat="server" CssClass="form-control" ReadOnly="true" ForeColor="#148048"></asp:TextBox>
												</div>
										    	<div class="input-block mb-3">
													<label for="name1"> RO's Name</label>
													<asp:TextBox ID="txtro3" runat="server" CssClass="form-control" ReadOnly="true" ForeColor="#148048"></asp:TextBox>
												</div>
												<div class="input-block mb-3">
													<label for="depart1"> RO Designation: </label>
													<asp:TextBox ID="txtro4" runat="server" CssClass="form-control" ReadOnly="true" ForeColor="#148048"></asp:TextBox>
												</div>
										</td>
									</tr>
								</tbody>
							</table>
						</div>
					</div>
				</div>
			</section>	 
					
			<section class="review-section professional-excellence">
				<div class="review-header text-center">
					<h3 class="review-title">Professional Excellence</h3>
					<p class="text-muted">Lorem ipsum dollar</p>
				</div>
				<div class="row">
					<div class="col-md-12">
						<div class="table-responsive">
							<table class="table table-bordered review-table mb-0">
								<thead>
									<tr>
										<th class="width-pixel">S.NO</th>
										<th>Key Result Area</th>
										<th>Key Performance Indicators</th>
										<th>Weightage</th>
										<th>Percentage achieved <br>( self Score )</th>
										<th>Points Scored <br>( self )</th>
										<th>Percentage achieved <br>( RO's Score )</th>
										<th>Points Scored <br>( RO )</th>
									</tr>
								</thead>
								<tbody>
									<tr>
										<td rowspan="2">1</td>
										<td rowspan="2">Production</td>
										<td>Quality</td>
										<td><asp:TextBox ID="txtdefaluttextbox1" runat="server" ReadOnly="true" CssClass="form-control" Text="30"></asp:TextBox></td>
										<td><asp:TextBox ID="TextBox1" runat="server" CssClass="form-control" oninput="validateRange(this)" OnTextChanged="TextBox_TextChanged" AutoPostBack="true" AutoCompleteType="Disabled" onkeypress="return isNumberKey(event)"></asp:TextBox></td>
										<td><asp:TextBox ID="TextBox11" runat="server" ReadOnly="true" CssClass="form-control"></asp:TextBox></td>
										<td><asp:TextBox ID="TextBox7" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox></td>
										<td><asp:TextBox ID="TextBox59" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox></td>
									</tr>
									<tr>
										<td>TAT (turn around time)</td>
										<td><asp:TextBox ID="txtdefaluttextbox2" runat="server" ReadOnly="true" CssClass="form-control">20</asp:TextBox></td>
										<td><asp:TextBox ID="TextBox2" runat="server" CssClass="form-control" oninput="validateRange(this)" AutoCompleteType="Disabled" AutoPostBack="true" OnTextChanged="TextBox_TextChanged" onkeypress="return isNumberKey(event)"></asp:TextBox></td>
										<td><asp:TextBox ID="TextBox12" runat="server" ReadOnly="true" CssClass="form-control"></asp:TextBox></td>
										<td><asp:TextBox ID="TextBox8" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox></td>
										<td><asp:TextBox ID="TextBox60" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox></td>
									</tr>
									<tr>
										<td>2</td>
										<td>Process Improvement</td>
										<td>PMS,New Ideas</td>
										<td><asp:TextBox ID="txtdefaluttextbox3" runat="server" ReadOnly="true" CssClass="form-control">20</asp:TextBox></td>
										<td><asp:TextBox ID="TextBox3" runat="server" CssClass="form-control" oninput="validateRange(this)" AutoCompleteType="Disabled" OnTextChanged="TextBox_TextChanged" AutoPostBack="true" onkeypress="return isNumberKey(event)"></asp:TextBox></td>
										<td><asp:TextBox ID="TextBox13" runat="server" ReadOnly="true" CssClass="form-control"></asp:TextBox></td>
										<td><asp:TextBox ID="TextBox9" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox></td>
										<td><asp:TextBox ID="TextBox61" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox></td>
									</tr>
									<tr>
										<td>3</td>
										<td>Team Management</td>
										<td>Team Productivity,dynaics,attendance,attrition</td>
										<td><asp:TextBox ID="txtdefaluttextbox4" runat="server" ReadOnly="true" CssClass="form-control">10</asp:TextBox></td>
										<td><asp:TextBox ID="TextBox4" runat="server" CssClass="form-control" oninput="validateRange(this)"  AutoCompleteType="Disabled" OnTextChanged="TextBox_TextChanged" AutoPostBack="true" onkeypress="return isNumberKey(event)"></asp:TextBox></td>
										<td><asp:TextBox ID="TextBox14" runat="server" ReadOnly="true" CssClass="form-control"></asp:TextBox></td>
										<td><asp:TextBox ID="TextBox10" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox></td>
										<td><asp:TextBox ID="TextBox62" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox></td>
									</tr>
									<tr>
										<td>4</td>
										<td>Knowledge Sharing</td>
										<td>Sharing the knowledge for team productivity </td>
										<td><asp:TextBox ID="txtdefaluttextbox5" runat="server" ReadOnly="true" CssClass="form-control">10</asp:TextBox></td>
										<td><asp:TextBox ID="TextBox5" runat="server" CssClass="form-control" oninput="validateRange(this)" AutoCompleteType="Disabled" OnTextChanged="TextBox_TextChanged" AutoPostBack="true" onkeypress="return isNumberKey(event)"></asp:TextBox></td>
										<td><asp:TextBox ID="TextBox15" runat="server" ReadOnly="true" CssClass="form-control"></asp:TextBox></td>
										<td><asp:TextBox ID="TextBox17" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox></td>
										<td><asp:TextBox ID="TextBox63" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox></td>
									</tr>
									<tr>
										<td>5</td>
										<td>Reporting and Communication</td>
										<td>Emails/Calls/Reports and Other Communication</td>
										<td><asp:TextBox ID="txtdefaluttextbox6" runat="server" ReadOnly="true" CssClass="form-control">10</asp:TextBox></td>
										<td><asp:TextBox ID="TextBox6" runat="server" CssClass="form-control" oninput="validateRange(this)" AutoCompleteType="Disabled" OnTextChanged="TextBox_TextChanged" AutoPostBack="true" onkeypress="return isNumberKey(event)"></asp:TextBox></td>
										<td><asp:TextBox ID="TextBox16" runat="server" ReadOnly="true" CssClass="form-control"></asp:TextBox></td>
										<td><asp:TextBox ID="TextBox18" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox></td>
										<td><asp:TextBox ID="TextBox64" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox></td>
									</tr>
									<tr>
										<td colspan="3" class="text-center">Total </td>
										<td><asp:TextBox ID="txtdefaultcount" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox></td>
										<td><asp:TextBox ID="txtpercentagetotal" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox></td>
										<td><asp:TextBox ID="txttotalscore" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox></td>
										<td><asp:TextBox ID="TextBox65" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox></td>
										<td><asp:TextBox ID="TextBox66" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox></td>
									</tr>
								</tbody>
							</table>
						</div>
					</div>
				</div>
			</section>
			<section class="review-section personal-excellence">
				<div class="review-header text-center">
					<h3 class="review-title">Personal Excellence</h3>
					<p class="text-muted">Lorem ipsum dollar</p>
				</div>
				<div class="row">
					<div class="col-md-12">
						<div class="table-responsive">
							<table class="table table-bordered review-table mb-0">
								<thead>
									<tr>
										<th class="width-pixel">S.NO</th>
										<th>Personal Attributes</th>
										<th>Key Indicators</th>
										<th>Weightage</th>
										<th>Percentage achieved <br>( self Score )</th>
										<th>Points Scored <br>( self )</th>
										<th>Percentage achieved <br>( RO's Score )</th>
										<th>Points Scored <br>( RO )</th>
									</tr>
								</thead>
								<tbody>
									<tr>
										<td rowspan="2">1</td>
										<td rowspan="2">Attendance</td>
										<td>Planned or Unplanned Leaves</td>
										<td><asp:TextBox ID="TextBox26" runat="server" CssClass="form-control" ReadOnly="true">10</asp:TextBox></td>
										<td><asp:TextBox ID="TextBox19" runat="server" CssClass="form-control" AutoCompleteType="Disabled" oninput="validateRange(this)" AutoPostBack="true" OnTextChanged="Text_TextChanged" onkeypress="return isNumberKey(event)"></asp:TextBox></td>
										<td><asp:TextBox ID="TextBox27" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox></td>
										<td><asp:TextBox ID="TextBox43" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox></td>
										<td><asp:TextBox ID="TextBox44" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox></td>
									</tr>
									<tr>
										<td>Time Consciousness</td>
										<td><asp:TextBox ID="TextBox34" runat="server" CssClass="form-control" ReadOnly="true">10</asp:TextBox></td>
										<td><asp:TextBox ID="TextBox20" runat="server" CssClass="form-control" AutoCompleteType="Disabled" oninput="validateRange(this)" AutoPostBack="true" OnTextChanged="Text_TextChanged" onkeypress="return isNumberKey(event)"></asp:TextBox></td>
										<td><asp:TextBox ID="TextBox28" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox></td>
										<td><asp:TextBox ID="TextBox45" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox></td>
										<td><asp:TextBox ID="TextBox46" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox></td>
									</tr>
									<tr>
										<td rowspan="2">2</td>
										<td rowspan="2">Attitude & Behavior</td>
										<td>Team Collaboration</td>
										<td><asp:TextBox ID="TextBox35" runat="server" CssClass="form-control" ReadOnly="true">10</asp:TextBox></td>
										<td><asp:TextBox ID="TextBox21" runat="server" CssClass="form-control" AutoCompleteType="Disabled" oninput="validateRange(this)" AutoPostBack="true" OnTextChanged="Text_TextChanged" onkeypress="return isNumberKey(event)"></asp:TextBox></td>
										<td><asp:TextBox ID="TextBox29" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox></td>
										<td><asp:TextBox ID="TextBox47" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox></td>
										<td><asp:TextBox ID="TextBox48" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox></td>
									</tr>
									<tr>
										<td>Professionalism & Responsiveness</td>
										<td><asp:TextBox ID="TextBox36" runat="server" CssClass="form-control" ReadOnly="true">10</asp:TextBox></td>
										<td><asp:TextBox ID="TextBox22" runat="server" CssClass="form-control" AutoCompleteType="Disabled" oninput="validateRange(this)" AutoPostBack="true" OnTextChanged="Text_TextChanged" onkeypress="return isNumberKey(event)"></asp:TextBox></td>
										<td><asp:TextBox ID="TextBox30" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox></td>
										<td><asp:TextBox ID="TextBox49" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox></td>
										<td><asp:TextBox ID="TextBox50" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox></td>
									</tr>
									<tr>
										<td>3</td>
										<td>Policy & Procedures </td>
										<td>Adherence to policies and procedures</td>
										<td><asp:TextBox ID="TextBox37" runat="server" CssClass="form-control" ReadOnly="true">20</asp:TextBox></td>
										<td><asp:TextBox ID="TextBox23" runat="server" CssClass="form-control" AutoCompleteType="Disabled" oninput="validateRange(this)" AutoPostBack="true" OnTextChanged="Text_TextChanged" onkeypress="return isNumberKey(event)"></asp:TextBox></td>
										<td><asp:TextBox ID="TextBox31" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox></td>
										<td><asp:TextBox ID="TextBox51" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox></td>
										<td><asp:TextBox ID="TextBox52" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox></td>
									</tr>
									<tr>
									<td>4</td>
										<td>Initiatives</td>
										<td>Special Efforts, Suggestions,Ideas,etc.</td>
										<td><asp:TextBox ID="TextBox38" runat="server" CssClass="form-control" ReadOnly="true">20</asp:TextBox></td>
										<td><asp:TextBox ID="TextBox24" runat="server" CssClass="form-control" AutoCompleteType="Disabled" oninput="validateRange(this)" AutoPostBack="true" OnTextChanged="Text_TextChanged" onkeypress="return isNumberKey(event)"></asp:TextBox></td>
										<td><asp:TextBox ID="TextBox32" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox></td>
										<td><asp:TextBox ID="TextBox53" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox></td>
										<td><asp:TextBox ID="TextBox54" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox></td>
									</tr>
									<tr>
										<td>5</td>
										<td>Continuous Skill Improvement</td>
										<td>Preparedness to move to next level & Training utilization</td>
										<td><asp:TextBox ID="TextBox39" runat="server" CssClass="form-control" ReadOnly="true">20</asp:TextBox></td>
										<td><asp:TextBox ID="TextBox25" runat="server" CssClass="form-control" AutoCompleteType="Disabled" oninput="validateRange(this)" AutoPostBack="true" OnTextChanged="Text_TextChanged" onkeypress="return isNumberKey(event)"></asp:TextBox></td>
										<td><asp:TextBox ID="TextBox33" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox></td>
										<td><asp:TextBox ID="TextBox55" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox></td>
										<td><asp:TextBox ID="TextBox56" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox></td>
									</tr>
									<tr>
										<td colspan="3" class="text-center">Total </td>
										<td><asp:TextBox ID="TextBox40" runat="server" CssClass="form-control" ReadOnly="true">100</asp:TextBox></td>
										<td><asp:TextBox ID="TextBox41" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox></td>
										<td><asp:TextBox ID="TextBox42" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox></td>
										<td><asp:TextBox ID="TextBox57" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox></td>
										<td><asp:TextBox ID="TextBox58" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox></td>
									</tr>
									<tr>
										<td colspan="3" class="text-center"><b>Total Percentage(%)</b></td>
										<td colspan="5" class="text-center"><asp:TextBox ID="TextBox67" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox></td>
									</tr>
									<tr>
										<td colspan="8" class="text-center">
											<div class="grade-span">
												<h4>Grade</h4>
												<span class="badge bg-inverse-danger">Below 65 Poor</span> 
												<span class="badge bg-inverse-warning">65-74 Average</span> 
												<span class="badge bg-inverse-info">75-84 Satisfactory</span> 
												<span class="badge bg-inverse-purple">85-92 Good</span> 
												<span class="badge bg-inverse-success">Above 92 Excellent</span>
											</div>
										</td>
									</tr>
								</tbody>
							</table>
						</div>
					</div>
				</div>
			</section>

			<%--<section class="review-section">
				<div class="review-header text-center">
					<h3 class="review-title">Special Initiatives, Achievements, contributions if any</h3>
					<p class="text-muted">Lorem ipsum dollar</p>
				</div>
				<div class="row">
					<div class="col-md-12">
						<div class="table-responsive">
							<table class="table table-bordered table-review review-table mb-0" id="table_achievements">
								<thead>
									<tr>
										<th class="width-pixel">S.NO</th>
										<th>By Self</th>
										<th>RO's Comment</th>
										<th>HOD's Comment</th>
										<th class="width-64"><button type="button" class="btn btn-primary btn-add-row"><i class="fa-solid fa-plus"></i></button></th>
									</tr>
								</thead>
								<tbody id="table_achievements_tbody">
									<tr>
										<td>1</td>
										<td><input type="text" class="form-control" ></td>
										<td><input type="text" class="form-control" ></td>
										<td><input type="text" class="form-control" ></td>
										<td></td>
									</tr>
									<tr>
										<td>2</td>
										<td><input type="text" class="form-control" ></td>
										<td><input type="text" class="form-control" ></td>
										<td><input type="text" class="form-control" ></td>
										<td></td>
									</tr>
									<tr>
										<td>3</td>
										<td><input type="text" class="form-control" ></td>
										<td><input type="text" class="form-control" ></td>
										<td><input type="text" class="form-control" ></td>
										<td></td>
									</tr>
									<tr>
										<td>4</td>
										<td><input type="text" class="form-control" ></td>
										<td><input type="text" class="form-control" ></td>
										<td><input type="text" class="form-control" ></td>
										<td></td>
									</tr>
									<tr>
										<td>5</td>
										<td><input type="text" class="form-control" ></td>
										<td><input type="text" class="form-control" ></td>
										<td><input type="text" class="form-control" ></td>
										<td></td>
									</tr>
								</tbody>
							</table>
						</div>
					</div>
				</div>
			</section>
			<section class="review-section">
				<div class="review-header text-center">
					<h3 class="review-title">Comments on the role</h3>
					<p class="text-muted">alterations if any requirred like addition/deletion of responsibilities</p>
				</div>
				<div class="row">
					<div class="col-md-12">
						<div class="table-responsive">
							<table class="table table-bordered table-review review-table mb-0" id="table_alterations">
								<thead>
									<tr>
										<th class="width-pixel">S.NO</th>
										<th>By Self</th>
										<th>RO's Comment</th>
										<th>HOD's Comment</th>
										<th class="width-64"><button type="button" class="btn btn-primary btn-add-row"><i class="fa-solid fa-plus"></i></button></th>
									</tr>
								</thead>
								<tbody id="table_alterations_tbody">
									<tr>
										<td>1</td>
										<td><input type="text" class="form-control" ></td>
										<td><input type="text" class="form-control" ></td>
										<td><input type="text" class="form-control" ></td>
										<td></td>
									</tr>
									<tr>
										<td>2</td>
										<td><input type="text" class="form-control" ></td>
										<td><input type="text" class="form-control" ></td>
										<td><input type="text" class="form-control" ></td>
										<td></td>
									</tr>
									<tr>
										<td>3</td>
										<td><input type="text" class="form-control" ></td>
										<td><input type="text" class="form-control" ></td>
										<td><input type="text" class="form-control" ></td>
										<td></td>
									</tr>
									<tr>
										<td>4</td>
										<td><input type="text" class="form-control" ></td>
										<td><input type="text" class="form-control" ></td>
										<td><input type="text" class="form-control" ></td>
										<td></td>
									</tr>
									<tr>
										<td>5</td>
										<td><input type="text" class="form-control" ></td>
										<td><input type="text" class="form-control" ></td>
										<td><input type="text" class="form-control" ></td>
										<td></td>
									</tr>
								</tbody>
							</table>
						</div>
					</div>
				</div>
			</section>
					
			<section class="review-section">
				<div class="review-header text-center">
					<h3 class="review-title">Comments on the role</h3>
					<p class="text-muted">alterations if any requirred like addition/deletion of responsibilities</p>
				</div>
				<div class="row">
					<div class="col-md-12">
						<div class="table-responsive">
							<table class="table table-bordered review-table mb-0">
								<thead>
									<tr>
										<th class="width-pixel">S.NO</th>
										<th>Strengths</th>
										<th>Area's for Improvement</th>
									</tr>
								</thead>
								<tbody>
									<tr>
										<td>1</td>
										<td><input type="text" class="form-control" ></td>
										<td><input type="text" class="form-control" ></td>
									</tr>
									<tr>
										<td>2</td>
										<td><input type="text" class="form-control" ></td>
										<td><input type="text" class="form-control" ></td>
									</tr>
									<tr>
										<td>3</td>
										<td><input type="text" class="form-control" ></td>
										<td><input type="text" class="form-control" ></td>
									</tr>
									<tr>
										<td>4</td>
										<td><input type="text" class="form-control" ></td>
										<td><input type="text" class="form-control" ></td>
									</tr>
									<tr>
										<td>5</td>
										<td><input type="text" class="form-control" ></td>
										<td><input type="text" class="form-control" ></td>
									</tr>
								</tbody>
							</table>
						</div>
					</div>
				</div>
			</section>
			<section class="review-section">
				<div class="review-header text-center">
					<h3 class="review-title">Appraisee's Strengths and Areas for Improvement perceived by the Reporting officer</h3>
					<p class="text-muted">Lorem ipsum dollar</p>
				</div>
				<div class="row">
					<div class="col-md-12">
						<div class="table-responsive">
							<table class="table table-bordered review-table mb-0">
								<thead>
									<tr>
										<th class="width-pixel">S.NO</th>
										<th>Strengths</th>
										<th>Area's for Improvement</th>
									</tr>
								</thead>
								<tbody>
									<tr>
										<td>1</td>
										<td><input type="text" class="form-control" ></td>
										<td><input type="text" class="form-control" ></td>
									</tr>
									<tr>
										<td>2</td>
										<td><input type="text" class="form-control" ></td>
										<td><input type="text" class="form-control" ></td>
									</tr>
									<tr>
										<td>3</td>
										<td><input type="text" class="form-control" ></td>
										<td><input type="text" class="form-control" ></td>
									</tr>
								</tbody>
							</table>
						</div>
					</div>
				</div>
			</section>
					
			<section class="review-section">
				<div class="review-header text-center">
					<h3 class="review-title">Appraisee's Strengths and Areas for Improvement perceived by the Head of the Department</h3>
					<p class="text-muted">Lorem ipsum dollar</p>
				</div>
				<div class="row">
					<div class="col-md-12">
						<div class="table-responsive">
							<table class="table table-bordered review-table mb-0">
								<thead>
									<tr>
										<th class="width-pixel">S.NO</th>
										<th>Strengths</th>
										<th>Area's for Improvement</th>
									</tr>
								</thead>
								<tbody>
									<tr>
										<td>1</td>
										<td><input type="text" class="form-control" ></td>
										<td><input type="text" class="form-control" ></td>
									</tr>
									<tr>
										<td>2</td>
										<td><input type="text" class="form-control" ></td>
										<td><input type="text" class="form-control" ></td>
									</tr>
									<tr>
										<td>3</td>
										<td><input type="text" class="form-control" ></td>
										<td><input type="text" class="form-control" ></td>
									</tr>
								</tbody>
							</table>
						</div>
					</div>
				</div>
			</section>
					
			<section class="review-section">
				<div class="review-header text-center">
					<h3 class="review-title">Personal Goals</h3>
					<p class="text-muted">Lorem ipsum dollar</p>
				</div>
				<div class="row">
					<div class="col-md-12">
						<div class="table-responsive">
							<table class="table table-bordered review-table mb-0">
								<thead>
									<tr>
										<th class="width-pixel">S.NO</th>
										<th>Goal Achieved during last year</th>
										<th>Goal set for current year</th>
									</tr>
								</thead>
								<tbody>
									<tr>
										<td>1</td>
										<td><input type="text" class="form-control" ></td>
										<td><input type="text" class="form-control" ></td>
									</tr>
									<tr>
										<td>2</td>
										<td><input type="text" class="form-control" ></td>
										<td><input type="text" class="form-control" ></td>
									</tr>
									<tr>
										<td>3</td>
										<td><input type="text" class="form-control" ></td>
										<td><input type="text" class="form-control" ></td>
									</tr>
								</tbody>
							</table>
						</div>
					</div>
				</div>
			</section>--%>
					
			<section class="review-section">
				<div class="review-header text-center">
					<h3 class="review-title">Personal Updates</h3>
					<p class="text-muted">Lorem ipsum dollar</p>
				</div>
				<div class="row">
					<div class="col-md-12">
						<div class="table-responsive">
							<table class="table table-bordered review-table mb-0">
								<thead>
									<tr>
										<th class="width-pixel">S.NO</th>
										<th>Last Year</th>
										<th>Yes/No</th>
										<th>Details</th>
										<th>Current Year</th>
										<th>Yes/No</th>
										<th>Details</th>
									</tr>
								</thead>
								<tbody>
									<tr>
										<td>1</td>
										<td>Married/Engaged?</td>
										<td>
											<asp:DropDownList ID="DropDownList7" runat="server" CssClass="form-control" onmousedown="hideFirstItem()">
				                                  <asp:ListItem>Select</asp:ListItem>
				                                  <asp:ListItem>Yes</asp:ListItem>
				                                  <asp:ListItem>No</asp:ListItem>
                                            </asp:DropDownList>	
										</td>
										<td><asp:TextBox ID="TextBox74" runat="server" CssClass="form-control" AutoCompleteType="Disabled" OnTextChanged="TextpersonalUpdate_TextChanged" AutoPostBack="true"></asp:TextBox></td>
										<td>Marriage Plans</td>
										<td>
											<asp:DropDownList ID="DropDownList8" runat="server" CssClass="form-control" onmousedown="hideFirstItem()">
                                                <asp:ListItem>Select</asp:ListItem>
                                                <asp:ListItem>Yes</asp:ListItem>
                                                <asp:ListItem>No</asp:ListItem>
                                            </asp:DropDownList>	
										</td>
										<td><asp:TextBox ID="TextBox75" runat="server" CssClass="form-control" AutoCompleteType="Disabled" OnTextChanged="TextpersonalUpdate_TextChanged" AutoPostBack="true"></asp:TextBox></td>
									</tr>
									<tr>
										<td>2</td>
										<td>Higher Studies/Certifications?</td>
										<td>
											<asp:DropDownList ID="DropDownList9" runat="server" CssClass="form-control" onmousedown="hideFirstItem()">
                                                <asp:ListItem>Select</asp:ListItem>
                                                <asp:ListItem>Yes</asp:ListItem>
                                                <asp:ListItem>No</asp:ListItem>
                                            </asp:DropDownList>	
										</td>
										<td><asp:TextBox ID="TextBox76" runat="server" CssClass="form-control" AutoCompleteType="Disabled" OnTextChanged="TextpersonalUpdate_TextChanged" AutoPostBack="true"></asp:TextBox></td>
										<td>Plans For Higher Study</td>
										<td>
											<asp:DropDownList ID="DropDownList10" runat="server" CssClass="form-control" onmousedown="hideFirstItem()">
                                                    <asp:ListItem>Select</asp:ListItem>
                                                    <asp:ListItem>Yes</asp:ListItem>
                                                    <asp:ListItem>No</asp:ListItem>
                                                    </asp:DropDownList>	
										</td>
										<td><asp:TextBox ID="TextBox77" runat="server" CssClass="form-control" AutoCompleteType="Disabled" OnTextChanged="TextpersonalUpdate_TextChanged" AutoPostBack="true"></asp:TextBox></td>
									</tr>
									<tr>
										<td>2</td>
										<td>Health Issues?</td>
										<td>
											<asp:DropDownList ID="DropDownList11" runat="server" CssClass="form-control" onmousedown="hideFirstItem()">
                                                <asp:ListItem>Select</asp:ListItem>
                                                <asp:ListItem>Yes</asp:ListItem>
                                                <asp:ListItem>No</asp:ListItem>
                                            </asp:DropDownList>	
										</td>
										<td><asp:TextBox ID="TextBox78" runat="server" CssClass="form-control" AutoCompleteType="Disabled" OnTextChanged="TextpersonalUpdate_TextChanged" AutoPostBack="true"></asp:TextBox></td>
										<td>Certification Plans</td>
										<td>
											<asp:DropDownList ID="DropDownList12" runat="server" CssClass="form-control" onmousedown="hideFirstItem()">
                                                <asp:ListItem>Select</asp:ListItem>
                                                <asp:ListItem>Yes</asp:ListItem>
                                                <asp:ListItem>No</asp:ListItem>
                                             </asp:DropDownList>	
										</td>
										<td><asp:TextBox ID="TextBox79" runat="server" CssClass="form-control" AutoCompleteType="Disabled" OnTextChanged="TextpersonalUpdate_TextChanged" AutoPostBack="true"></asp:TextBox></td>
									</tr>
									<tr>
										<td>2</td>
										<td>Others</td>
										<td>
											<asp:DropDownList ID="DropDownList13" runat="server" CssClass="form-control" onmousedown="hideFirstItem()">
                                                <asp:ListItem>Select</asp:ListItem>
                                                <asp:ListItem>Yes</asp:ListItem>
                                                <asp:ListItem>No</asp:ListItem>
                                            </asp:DropDownList>	
										</td>
										<td><asp:TextBox ID="TextBox80" runat="server" CssClass="form-control" AutoCompleteType="Disabled" OnTextChanged="TextpersonalUpdate_TextChanged" AutoPostBack="true"></asp:TextBox></td>
										<td>Others</td>
										<td>
											<asp:DropDownList ID="DropDownList14" runat="server" CssClass="form-control" onmousedown="hideFirstItem()">
                                               <asp:ListItem>Select</asp:ListItem>
                                               <asp:ListItem>Yes</asp:ListItem>
                                               <asp:ListItem>No</asp:ListItem>
                                             </asp:DropDownList>	
										</td>
										<td><asp:TextBox ID="TextBox81" runat="server" CssClass="form-control" AutoCompleteType="Disabled" OnTextChanged="TextpersonalUpdate_TextChanged" AutoPostBack="true"></asp:TextBox></td>
									</tr>
								</tbody>
							</table>
						</div>
					</div>
				</div>
			</section>
					
			<%--<section class="review-section">
				<div class="review-header text-center">
					<h3 class="review-title">Professional Goals Achieved for last year</h3>
					<p class="text-muted">Lorem ipsum dollar</p>
				</div>
				<div class="row">
					<div class="col-md-12">
						<div class="table-responsive">
							<table class="table table-bordered table-review review-table mb-0" id="table_goals">
								<thead>
									<tr>
										<th class="width-pixel">S.NO</th>
										<th>By Self</th>
										<th>RO's Comment</th>
										<th>HOD's Comment</th>
										<th class="width-64"><button type="button" class="btn btn-primary btn-add-row"><i class="fa-solid fa-plus"></i></button></th>
									</tr>
								</thead>
								<tbody id="table_goals_tbody">
									<tr>
										<td>1</td>
										<td><input type="text" class="form-control" ></td>
										<td><input type="text" class="form-control" ></td>
										<td><input type="text" class="form-control" ></td>
										<td></td>
									</tr>
									<tr>
										<td>2</td>
										<td><input type="text" class="form-control" ></td>
										<td><input type="text" class="form-control" ></td>
										<td><input type="text" class="form-control" ></td>
										<td></td>
									</tr>
									<tr>
										<td>3</td>
										<td><input type="text" class="form-control" ></td>
										<td><input type="text" class="form-control" ></td>
										<td><input type="text" class="form-control" ></td>
										<td></td>
									</tr>
									<tr>
										<td>4</td>
										<td><input type="text" class="form-control" ></td>
										<td><input type="text" class="form-control" ></td>
										<td><input type="text" class="form-control" ></td>
										<td></td>
									</tr>
									<tr>
										<td>5</td>
										<td><input type="text" class="form-control" ></td>
										<td><input type="text" class="form-control" ></td>
										<td><input type="text" class="form-control" ></td>
										<td></td>
									</tr>
								</tbody>
							</table>
						</div>
					</div>
				</div>
			</section>
					
			<section class="review-section">
				<div class="review-header text-center">
					<h3 class="review-title">Professional Goals for the forthcoming year</h3>
					<p class="text-muted">Lorem ipsum dollar</p>
				</div>
				<div class="row">
					<div class="col-md-12">
						<div class="table-responsive">
							<table class="table table-bordered table-review review-table mb-0" id="table_forthcoming">
								<thead>
									<tr>
										<th class="width-pixel">S.NO</th>
										<th>By Self</th>
										<th>RO's Comment</th>
										<th>HOD's Comment</th>
										<th class="width-64"><button type="button" class="btn btn-primary btn-add-row"><i class="fa-solid fa-plus"></i></button></th>
									</tr>
								</thead>
								<tbody id="table_forthcoming_tbody">
									<tr>
										<td>1</td>
										<td><input type="text" class="form-control" ></td>
										<td><input type="text" class="form-control" ></td>
										<td><input type="text" class="form-control" ></td>
										<td></td>
									</tr>
									<tr>
										<td>2</td>
										<td><input type="text" class="form-control" ></td>
										<td><input type="text" class="form-control" ></td>
										<td><input type="text" class="form-control" ></td>
										<td></td>
									</tr>
									<tr>
										<td>3</td>
										<td><input type="text" class="form-control" ></td>
										<td><input type="text" class="form-control" ></td>
										<td><input type="text" class="form-control" ></td>
										<td></td>
									</tr>
									<tr>
										<td>4</td>
										<td><input type="text" class="form-control" ></td>
										<td><input type="text" class="form-control" ></td>
										<td><input type="text" class="form-control" ></td>
										<td></td>
									</tr>
									<tr>
										<td>5</td>
										<td><input type="text" class="form-control" ></td>
										<td><input type="text" class="form-control" ></td>
										<td><input type="text" class="form-control" ></td>
										<td></td>
									</tr>
								</tbody>
							</table>
						</div>
					</div>
				</div>
			</section>
					
			<section class="review-section">
				<div class="review-header text-center">
					<h3 class="review-title">Training Requirements</h3>
					<p class="text-muted">if any to achieve the Performance Standard Targets completely</p>
				</div>
				<div class="row">
					<div class="col-md-12">
						<div class="table-responsive">
							<table class="table table-bordered table-review review-table mb-0" id="table_targets">
								<thead>
									<tr>
									<th class="width-pixel">S.NO</th>
									<th>By Self</th>
									<th>RO's Comment</th>
									<th>HOD's Comment</th>
									<th class="width-64"><button type="button" class="btn btn-primary btn-add-row"><i class="fa-solid fa-plus"></i></button></th>
									</tr>
								</thead>
								<tbody id="table_targets_tbody">
									<tr>
										<td>1</td>
										<td><input type="text" class="form-control" ></td>
										<td><input type="text" class="form-control" ></td>
										<td><input type="text" class="form-control" ></td>
										<td></td>
									</tr>
									<tr>
										<td>2</td>
										<td><input type="text" class="form-control" ></td>
										<td><input type="text" class="form-control" ></td>
										<td><input type="text" class="form-control" ></td>
										<td></td>
									</tr>
									<tr>
										<td>3</td>
										<td><input type="text" class="form-control" ></td>
										<td><input type="text" class="form-control" ></td>
										<td><input type="text" class="form-control" ></td>
										<td></td>
									</tr>
									<tr>
										<td>4</td>
										<td><input type="text" class="form-control" ></td>
										<td><input type="text" class="form-control" ></td>
										<td><input type="text" class="form-control" ></td>
										<td></td>
									</tr>
									<tr>
										<td>5</td>
										<td><input type="text" class="form-control" ></td>
										<td><input type="text" class="form-control" ></td>
										<td><input type="text" class="form-control" ></td>
										<td></td>
									</tr>
								</tbody>
							</table>
						</div>
					</div>
				</div>
			</section>

			<section class="review-section">
				<div class="review-header text-center">
					<h3 class="review-title">Any other general comments, observations, suggestions etc.</h3>
					<p class="text-muted">Lorem ipsum dollar</p>
				</div>
				<div class="row">
					<div class="col-md-12">
						<div class="table-responsive">
							<table class="table table-bordered table-review review-table mb-0" id="general_comments">
								<thead>
									<tr>
									<th class="width-pixel">S.NO</th>
									<th>Self</th>
									<th>RO</th>
									<th>HOD</th>
									<th class="width-64"><button type="button" class="btn btn-primary btn-add-row"><i class="fa-solid fa-plus"></i></button></th>
									</tr>
								</thead>
								<tbody id="general_comments_tbody" >
									<tr>
										<td>1</td>
										<td><input type="text" class="form-control" ></td>
										<td><input type="text" class="form-control" ></td>
										<td><input type="text" class="form-control" ></td>
										<td></td>
									</tr>
									<tr>
										<td>2</td>
										<td><input type="text" class="form-control" ></td>
										<td><input type="text" class="form-control" ></td>
										<td><input type="text" class="form-control" ></td>
										<td></td>
									</tr>
									<tr>
										<td>3</td>
										<td><input type="text" class="form-control" ></td>
										<td><input type="text" class="form-control" ></td>
										<td><input type="text" class="form-control" ></td>
										<td></td>
									</tr>
									<tr>
										<td>4</td>
										<td><input type="text" class="form-control" ></td>
										<td><input type="text" class="form-control" ></td>
										<td><input type="text" class="form-control" ></td>
										<td></td>
									</tr>
									<tr>
										<td>5</td>
										<td><input type="text" class="form-control" ></td>
										<td><input type="text" class="form-control" ></td>
										<td><input type="text" class="form-control" ></td>
										<td></td>
									</tr>
								</tbody>
							</table>
						</div>
					</div>
				</div>
			</section>--%>

			<section class="review-section">
				<div class="review-header text-center">
					<h3 class="review-title">For RO's Use Only</h3>
					<p class="text-muted">Lorem ipsum dollar</p>
				</div>
				<div class="row">
					<div class="col-md-12">
						<div class="table-responsive">
							<table class="table table-bordered review-table mb-0">
								<thead>
									<tr>
										<th></th>
										<th>Yes/No</th>
										<th>If Yes/No - Details</th>
									</tr>
								</thead>
								<tbody>
									<tr>
										<td>The Team member has Work related Issues</td>
										<td>											
											<asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control" onmousedown="hideFirstItem()">
												<asp:ListItem>Select</asp:ListItem>
												<asp:ListItem>Yes</asp:ListItem>
												<asp:ListItem>No</asp:ListItem>
											</asp:DropDownList>
										</td>
										<td><asp:TextBox ID="TextBox68" runat="server" CssClass="form-control" AutoPostBack="true" AutoCompleteType="Disabled" OnTextChanged="TextDropValue_TextChanged"></asp:TextBox></td>
									</tr>
									<tr>
										<td>The Team member has Leave Issues</td>
										<td>
										  <asp:DropDownList ID="DropDownList2" runat="server" CssClass="form-control" onmousedown="hideFirstItem()" >
				                            <asp:ListItem>Select</asp:ListItem>
				                            <asp:ListItem>Yes</asp:ListItem>
				                            <asp:ListItem>No</asp:ListItem>
                                           </asp:DropDownList>
										</td>
										<td><asp:TextBox ID="TextBox69" runat="server" CssClass="form-control" AutoPostBack="true" AutoCompleteType="Disabled" OnTextChanged="TextDropValue_TextChanged"></asp:TextBox></td>
									</tr>
									<tr>
										<td>The team member has Stability Issues</td>
										<td>
											<asp:DropDownList ID="DropDownList3" runat="server" CssClass="form-control" onmousedown="hideFirstItem()" >
				                              <asp:ListItem>Select</asp:ListItem>
				                              <asp:ListItem>Yes</asp:ListItem>
				                              <asp:ListItem>No</asp:ListItem>
                                            </asp:DropDownList>
										</td>
										<td><asp:TextBox ID="TextBox70" runat="server" CssClass="form-control" AutoPostBack="true" AutoCompleteType="Disabled" OnTextChanged="TextDropValue_TextChanged"></asp:TextBox></td>
									</tr>
									<tr>
										<td>The Team member exhibits non-supportive attitude</td>
										<td>
											<asp:DropDownList ID="DropDownList4" runat="server" CssClass="form-control" onmousedown="hideFirstItem()" >
				                              <asp:ListItem>Select</asp:ListItem>
				                              <asp:ListItem>Yes</asp:ListItem>
				                              <asp:ListItem>No</asp:ListItem>
                                            </asp:DropDownList>
										</td>
										<td><asp:TextBox ID="TextBox71" runat="server" CssClass="form-control" AutoPostBack="true" AutoCompleteType="Disabled" OnTextChanged="TextDropValue_TextChanged"></asp:TextBox></td>
									</tr>
									<tr>
										<td>Any other points in specific to note about the team member</td>
										<td>
											<asp:DropDownList ID="DropDownList5" runat="server" CssClass="form-control" onmousedown="hideFirstItem()" >
				                             <asp:ListItem>Select</asp:ListItem>
				                              <asp:ListItem>Yes</asp:ListItem>
				                              <asp:ListItem>No</asp:ListItem>
                                             </asp:DropDownList>
										</td>
										<td><asp:TextBox ID="TextBox72" runat="server" CssClass="form-control" AutoPostBack="true" AutoCompleteType="Disabled" OnTextChanged="TextDropValue_TextChanged"></asp:TextBox></td>
									</tr>
									<tr>
									<td>Overall Comment /Performance of the team member</td>
										<td>
											<asp:DropDownList ID="DropDownList6" runat="server" CssClass="form-control" onmousedown="hideFirstItem()" >
				                              <asp:ListItem>Select</asp:ListItem>
				                              <asp:ListItem>Yes</asp:ListItem>
				                              <asp:ListItem>No</asp:ListItem>
                                            </asp:DropDownList>
										</td>
										<td><asp:TextBox ID="TextBox73" runat="server" CssClass="form-control" AutoPostBack="true" AutoCompleteType="Disabled" OnTextChanged="TextDropValue_TextChanged"></asp:TextBox></td>
									</tr>
								</tbody>
							</table>
						</div>
					</div>
				</div>
			</section>
					
			<section class="review-section">
				<div class="review-header text-center">
					<h3 class="review-title">For HRD's Use Only</h3>
					<p class="text-muted">Lorem ipsum dollar</p>
				</div>
				<div class="row">
					<div class="col-md-12">
						<div class="table-responsive">
							<table class="table table-bordered review-table mb-0">
								<thead>
									<tr>
										<th>Overall Parameters</th>
										<th>Available Points</th>
										<th>Points Scored</th>
										<th>RO's Comment</th>
									</tr>
								</thead>
								<tbody>
									<tr>
										<td>KRAs Target Achievement Points (will be considered from the overall score specified in this document by the Reporting officer)</td>
										<td><asp:TextBox ID="TextBox82" runat="server" CssClass="form-control" AutoCompleteType="Disabled" AutoPostBack="true" OnTextChanged="PointsTotal_TextChanged"></asp:TextBox></td>
										<td><asp:TextBox ID="TextBox87" runat="server" CssClass="form-control" AutoCompleteType="Disabled" AutoPostBack="true" OnTextChanged="PointsGained_TextChanged"></asp:TextBox></td>
										<td><asp:TextBox ID="TextBox92" runat="server" CssClass="form-control" AutoCompleteType="Disabled" AutoPostBack="true" OnTextChanged="PointsHodComments_TextChanged"></asp:TextBox></td>
									</tr>
									<tr>
										<td>Professional Skills Scores(RO's Points furnished in the skill & attitude assessment sheet will be considered)</td>
										<td><asp:TextBox ID="TextBox83" runat="server" CssClass="form-control" AutoCompleteType="Disabled" AutoPostBack="true" OnTextChanged="PointsTotal_TextChanged"></asp:TextBox></td>
										<td><asp:TextBox ID="TextBox88" runat="server" CssClass="form-control" AutoCompleteType="Disabled" AutoPostBack="true" OnTextChanged="PointsGained_TextChanged"></asp:TextBox></td>
										<td><asp:TextBox ID="TextBox93" runat="server" CssClass="form-control" AutoCompleteType="Disabled" AutoPostBack="true" OnTextChanged="PointsHodComments_TextChanged"></asp:TextBox></td>
									</tr>
									<tr>
										<td>Personal Skills Scores(RO's Points furnished in the skill & attitude assessment sheet will be considered)</td>
										<td><asp:TextBox ID="TextBox84" runat="server" CssClass="form-control" AutoCompleteType="Disabled" AutoPostBack="true" OnTextChanged="PointsTotal_TextChanged"></asp:TextBox></td>
										<td><asp:TextBox ID="TextBox89" runat="server" CssClass="form-control" AutoCompleteType="Disabled" AutoPostBack="true" OnTextChanged="PointsGained_TextChanged"></asp:TextBox></td>
										<td><asp:TextBox ID="TextBox94" runat="server" CssClass="form-control" AutoCompleteType="Disabled" AutoPostBack="true" OnTextChanged="PointsHodComments_TextChanged"></asp:TextBox></td>
									</tr>
									<tr>
										<td>Special Achievements Score (HOD to furnish)</td>
										<td><asp:TextBox ID="TextBox85" runat="server" CssClass="form-control" AutoCompleteType="Disabled" AutoPostBack="true" OnTextChanged="PointsTotal_TextChanged"></asp:TextBox></td>
										<td><asp:TextBox ID="TextBox90" runat="server" CssClass="form-control" AutoCompleteType="Disabled" AutoPostBack="true" OnTextChanged="PointsGained_TextChanged"></asp:TextBox></td>
										<td><asp:TextBox ID="TextBox95" runat="server" CssClass="form-control" AutoCompleteType="Disabled" AutoPostBack="true" OnTextChanged="PointsHodComments_TextChanged"></asp:TextBox></td>
									</tr>
									<tr>
										<td>Overall Total Score</td>
										<td><asp:TextBox ID="TextBox86" runat="server" CssClass="form-control" AutoCompleteType="Disabled" ReadOnly="true" ></asp:TextBox></td>
										<td><asp:TextBox ID="TextBox91" runat="server" CssClass="form-control" AutoCompleteType="Disabled" ReadOnly="true" ></asp:TextBox></td>
										<td><asp:TextBox ID="TextBox96" runat="server" CssClass="form-control" AutoCompleteType="Disabled" AutoPostBack="true" OnTextChanged="PointsHodComments_TextChanged"></asp:TextBox></td>
									</tr>
								</tbody>
							</table>
						</div>
					</div>
				</div>
			</section>

			<div class="row">
				<div class="col-md-12">
					<div class="table-responsive">
						<table class="table table-bordered review-table mb-0">
							<thead>
								<tr>
									<th></th>
									<th>Name</th>
									<th>Signature</th>
									<th>Date</th>
								</tr>
							</thead>
							<tbody>
								<tr>
									<td>Employee</td>
									<td><input type="text" class="form-control" ></td>
									<td><input type="text" class="form-control" ></td>
									<td><input type="text" class="form-control" ></td>
								</tr>
								<tr>
									<td>Reporting Officer</td>
									<td><input type="text" class="form-control" ></td>
									<td><input type="text" class="form-control" ></td>
									<td><input type="text" class="form-control" ></td>
								</tr>
								<tr>
									<td>HOD</td>
									<td><input type="text" class="form-control" ></td>
									<td><input type="text" class="form-control" ></td>
									<td><input type="text" class="form-control" ></td>
								</tr>
								<tr>
									<td>HRD</td>
									<td><input type="text" class="form-control" ></td>
									<td><input type="text" class="form-control" ></td>
									<td><input type="text" class="form-control" ></td>
								</tr>
							</tbody>
						</table>
					</div>
				</div>
			</div>

        </div>
		<!-- /Page Content -->
				
    </div>
			<!-- /Page Wrapper -->
	<script type="text/javascript">
    function logToConsole(message) {
		console.log("value is :" + message);
		}

       
		function hideFirstItem()
		{
			
            var select = document.getElementById('<%= DropDownList1.ClientID %>');
			if (select && select.options.length > 0) {
              
				select.options[0].style.display = 'none';

                var select2 = document.getElementById('<%= DropDownList2.ClientID %>');
                if (select2 && select2.options.length > 0) {
                    select2.options[0].style.display = 'none';
                }

                var select3 = document.getElementById('<%= DropDownList3.ClientID %>');
                if (select3 && select3.options.length > 0) {
                    select3.options[0].style.display = 'none';
                }

                var select4 = document.getElementById('<%= DropDownList4.ClientID %>');
				if (select4 && select4.options.length > 0)
				{
                select4.options[0].style.display = 'none';
			    }

                var select5 = document.getElementById('<%= DropDownList5.ClientID %>');
				if (select5 && select5.options.length > 0)
				{
                select5.options[0].style.display = 'none';
			    }

                var select6 = document.getElementById('<%= DropDownList6.ClientID %>');
				if (select6 && select6.options.length > 0)
				{
                    select6.options[0].style.display = 'none';
                }
            }
        }

    </script>
	


	<script type="text/javascript">
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false; // Block non-numeric characters
            }
            return true; // Allow numeric characters
		}
        function validateRange(input) {
            var value = input.value;
            if (value !== "" && (value < 0 || value > 100)) {
                alert("Score between 0 and 100.");
                input.value = "";
            }
        }
    </script>
	

</asp:Content>
