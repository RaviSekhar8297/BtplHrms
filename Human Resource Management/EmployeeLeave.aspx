<%@ Page Title="" Language="C#" MasterPageFile="~/Employee.Master" AutoEventWireup="true" CodeBehind="EmployeeLeave.aspx.cs" Inherits="Human_Resource_Management.ApplyLeave" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
	
	<style type="text/css">
		
		.theade{
			background-color:green;
			color:white;
		}
		.table .custom-header {
        background-color: green;
        color: black;
        }
		table th{
			background:green!important;
			color:white!important;
		}
		.no-records-message{
			display:flex;
			align-items:center;
			justify-content:center;
			color:red;
		}
	</style>
	
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
	<div class="main-wrapper">
		<!-- Page Wrapper -->
		<div class="page-wrapper">
			
			<!-- Page Content -->
			<div class="content container-fluid">
				
				<!-- Page Header -->
				<div class="page-header">
					<div class="row align-items-center">
						<div class="col">
							<h3 class="page-title">Leaves <span><asp:Label ID="Label1" runat="server" ForeColor="Red"></asp:Label></span></h3>
							<ul class="breadcrumb">
								<li class="breadcrumb-item"><a href="EmployeeDashboard.aspx">Dashboard</a></li>
								<li class="breadcrumb-item active">Leaves</li>
							</ul>
						</div>
						<div class="col-auto float-end ms-auto">
							<% if (Session["AddLeave"] != null && Session["AddLeave"].ToString() == "True") { %>
                            <a href="EmployeeAddLeave.aspx"  class="btn add-btn"><i class="fa-solid fa-plus"></i> Add Leave</a>
							<% } %>
<%--							<a href="#" class="btn add-btn" data-bs-toggle="modal" data-bs-target="#add_leave"><i class="fa-solid fa-plus"></i> Add Leave</a>--%>
						</div>
					</div>
				</div>
				<!-- /Page Header -->
					
				<!-- Leave Statistics -->
				<div class="row">
					<div class="col-md-3">
						<div class="stats-info">
							<h6>Total Leaves</h6>
							<h4><asp:Label ID="lbltotalleaves" runat="server" Font-Size="Larger" ForeColor="deeppink" ></asp:Label></h4>
						</div>
					</div>
					<div class="col-md-3">
						<div class="stats-info">
							<h6>Casual Leaves</h6>
							<h4><asp:Label ID="lblcasualleavescount" runat="server" Font-Size="Larger" ForeColor="deeppink"></asp:Label></h4>
						</div>
					</div>
					
					<div class="col-md-3">
						<div class="stats-info">
							<h6>Sick Leaves </h6>
							<h4><asp:Label ID="lblsickleavescount" runat="server" Font-Size="Larger" ForeColor="deeppink" ></asp:Label></h4>
						</div>
					</div>
					<div class="col-md-3">
						<div class="stats-info">
							<h6>Comp-Off Leaves</h6>
							<h4><asp:Label ID="lblcompoff" runat="server" Font-Size="Larger" ForeColor="deeppink" ></asp:Label></h4>
						</div>
					</div>
				</div>
				<!-- /Leave Statistics -->
					
				<div class="row"> 
					<div class="col-md-12">
						<div class="table-responsive">
							<table class="table table-striped custom-table leave-employee-table mb-0 datatable">
								<thead class="custom-header">
									<tr style="background-color:green;color:black;">
										<th>ApplyDate</th>
										<th>Reason</th>
										<th>FromDate</th>
										<th>ToDate</th>
										<th>No of Days</th>
										<th>LeaveType</th>
										<th>Status</th>
										<th>Approved by</th>
									</tr>
								</thead>
								<asp:PlaceHolder ID="projectsContainer" runat="server"></asp:PlaceHolder>
								
								<%--<tbody>
									
									<tr>
											<td><asp:Label ID="Label2" runat="server"></asp:Label></td>
										<td><asp:Label ID="Label1" runat="server"></asp:Label></td>
										<td><asp:Label ID="Label3" runat="server"></asp:Label></td>
										<td><asp:Label ID="Label4" runat="server" Text="No of days"></asp:Label></td>
										<td><asp:Label ID="Label5" runat="server" ></asp:Label></td>
										
									<td class="text-center">
											<div class="action-label">
												<a class="btn btn-white btn-sm btn-rounded" href="javascript:void(0);">
													<i class="fa-regular fa-circle-dot text-purple"></i>
													<asp:DropDownList ID="ddlleavesstatus" runat="server" CssClass="ddlstatusleave" onmousedown="hideFirstItem()">
														<asp:ListItem>-- Select --</asp:ListItem>
														<asp:ListItem>Pending</asp:ListItem>
														<asp:ListItem>Approved</asp:ListItem>
													</asp:DropDownList>
												</a>
											</div>
										</td>
										<td>
											<h2 class="table-avatar">
												<a href="profile.html" class="avatar avatar-xs"><img src="assets/img/profiles/avatar-09.jpg" alt="User Image"></a>
												<a href="#">Richard Miles</a>
											</h2>
										</td>	
										<td class="text-end">
											<div class="dropdown dropdown-action">
												<a href="#" class="action-icon dropdown-toggle" data-bs-toggle="dropdown" aria-expanded="false"><i class="material-icons">more_vert</i></a>
												<div class="dropdown-menu dropdown-menu-right">
													<a class="dropdown-item" href="#" data-bs-toggle="modal" data-bs-target="#edit_leave"><i class="fa-solid fa-pencil m-r-5"></i> Edit</a>
													<a class="dropdown-item" href="#" data-bs-toggle="modal" data-bs-target="#delete_approve"><i class="fa-regular fa-trash-can m-r-5"></i> Delete</a>
												</div>
											</div>
										</td>
									</tr>						
								</tbody>	--%>
							</table>
						</div>
					</div>
				</div>
			</div><asp:Literal ID="Literal1" runat="server"></asp:Literal>
			<!-- /Page Content -->			

			
				
			<!-- Edit Leave Modal -->
			<div id="edit_leave" class="modal custom-modal fade" role="dialog">
				<div class="modal-dialog modal-dialog-centered" role="document">
					<div class="modal-content">
						<div class="modal-header">
							<h5 class="modal-title">Edit Leave </h5>
							<button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close">
								<span aria-hidden="true">&times;</span>
							</button>
						</div>
						<div class="modal-body">
					            <div class="input-block mb-3">
									<label class="col-form-label">LeaveId <span class="text-danger">*</span></label>
									<%--<asp:TextBox ID="TextBox4" runat="server" CssClass="form-control"></asp:TextBox>--%>
									<asp:HiddenField ID="HiddenField2" runat="server" />
								</div>
								<div class="input-block mb-3">
									<label class="col-form-label">Leave Type <span class="text-danger">*</span></label>
									<asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control">
										<asp:ListItem>Casual Leave</asp:ListItem>
										<asp:ListItem>Medical Leave</asp:ListItem>
										<asp:ListItem>LOP Leave</asp:ListItem>
									</asp:DropDownList>
								</div>
								<div class="input-block mb-3">
									<label class="col-form-label">From <span class="text-danger">*</span></label>									
										<asp:TextBox ID="TextBox1" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
								</div>
								<div class="input-block mb-3">
									<label class="col-form-label">To <span class="text-danger">*</span></label>									
										<asp:TextBox ID="TextBox2" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
								</div>
								<div class="input-block mb-3">
									<label class="col-form-label">Number of days <span class="text-danger">*</span></label>
									<asp:TextBox ID="TextBox3" runat="server" CssClass="form-control"></asp:TextBox>
								</div>
								
								<div class="input-block mb-3">
									<label class="col-form-label">Leave Reason <span class="text-danger">*</span></label>
									<asp:TextBox ID="TextBox5" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
								</div>
								<div class="submit-section">
									<asp:Button ID="btnleaveedit" runat="server" Text="Update.." CssClass="btn btn-primary submit-btn" OnClick="btnleaveedit_Click"  />
								</div>
							
						</div>
					</div>
				</div>
			</div>
			<!-- /Edit Leave Modal -->
				
			<!-- Delete Leave Modal -->
			<div class="modal custom-modal fade" id="delete_approve" role="dialog">
				<div class="modal-dialog modal-dialog-centered">
					<div class="modal-content">
						<div class="modal-body">
							<div class="form-header">
								<h3>Delete Leave</h3>
								<p>Are you sure want to Cancel this leave <asp:Label ID="Label2" runat="server" ForeColor="#cc0000"></asp:Label>  ?</p>
								<asp:HiddenField ID="HiddenField1" runat="server" />
							</div>
							<div class="modal-btn delete-action">
								<div class="row">
									<div class="col-6">
										<asp:Button ID="btnleavedelete" runat="server" Text="Delete" CssClass="btn btn-primary continue-btn" OnClick="btnleavedelete_Click" />
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
			<!-- /Delete Leave Modal -->
				
		</div>
		<!-- /Page Wrapper -->
	</div>
	<script type="text/javascript">


       <%-- document.getElementById('<%= txttodate.ClientID %>').addEventListener('change', calculateNumberOfDays);

        function calculateNumberOfDays() {
            var startDateValue = document.getElementById('<%= txtstartdate.ClientID %>').value;
            var endDateValue = document.getElementById('<%= txttodate.ClientID %>').value;
            var fromHalfDayValue = document.getElementById('<%= ddlfromhalfday.ClientID %>').value;
            var toHalfDayValue = document.getElementById('<%= ddltohalfday.ClientID %>').value;

            if (startDateValue && endDateValue) {
                var startDate = new Date(startDateValue);
                var endDate = new Date(endDateValue);
                var numberOfDays = 0;

                if (startDate.toDateString() === endDate.toDateString()) {
                    if ((fromHalfDayValue === "fafts" || fromHalfDayValue === "tafts") && (toHalfDayValue === "fms" || toHalfDayValue === "tms")) {
                        numberOfDays = 0.5;
                    }
				}
				else {
                    while (startDate <= endDate) {
                        if (startDate.getDay() !== 0) {
                            if (startDate.toDateString() === startDateValue && (fromHalfDayValue === "fafts" || fromHalfDayValue === "tafts")) {
                                numberOfDays += 0.5;
                            }
                            else if (startDate.toDateString() === endDateValue && (toHalfDayValue === "fms" || toHalfDayValue === "tms")) {
                                numberOfDays += 0.5;
                            }
							else
							{
                                numberOfDays++;
                            }
                        }
                        startDate.setDate(startDate.getDate() + 1); // Move to the next date
                    }
                }

                document.getElementById('<%= txtnoofdays.ClientID %>').value = numberOfDays;
			}
			else {
                document.getElementById('<%= txtnoofdays.ClientID %>').value = "";
            }


        }--%>



    </script>



	<%--<script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>

	<script type="text/javascript">
        $(document).ready(function () {
            // Get the current date
            var currentDate = new Date();
            var year = currentDate.getFullYear();
            var month = currentDate.getMonth() + 1; // Months are zero-based
            var day = currentDate.getDate();

            // Format the current date to YYYY-MM-DD
            var formattedCurrentDate = year + '-' + (month < 10 ? '0' + month : month) + '-' + (day < 10 ? '0' + day : day);

            // Get the last day of the current month
            var lastDayOfMonth = new Date(year, month, 0).getDate();
            var formattedLastDateOfMonth = year + '-' + (month < 10 ? '0' + month : month) + '-' + (lastDayOfMonth < 10 ? '0' + lastDayOfMonth : lastDayOfMonth);

            // Set the min and max attributes of the date input
            $('#<%= txtstartdate.ClientID %>').attr('min', formattedCurrentDate);
            $('#<%= txtstartdate.ClientID %>').attr('max', formattedLastDateOfMonth);
    });
    </script>--%>


	<script type="text/javascript">
        function edit_leave(leaveID, leaveType, fromDate1, toDate1, numberOfDays, rs) {
            $('#<%= DropDownList1.ClientID %> option[value="' + leaveType + '"]').prop('selected', true);
            $('#<%= HiddenField2.ClientID %>').val(leaveID).prop('readonly', true);
            $('#<%= TextBox1.ClientID %>').val(fromDate1);
            $('#<%= TextBox2.ClientID %>').val(toDate1);
            $('#<%= TextBox3.ClientID %>').val(numberOfDays);
            $('#<%= TextBox5.ClientID %>').val(rs);
            $('#edit_leave').modal('show');
		}


        function delete_leave(leaveID) {
            document.getElementById('<%= Label2.ClientID %>').textContent = leaveID;
			$('#<%= HiddenField1.ClientID %>').val(leaveID);
			console.log("delete leave is : " + leaveID);
        }


    </script>


	 
	<script type="text/javascript">
       // console.log("Start date is : " );
        $(document).ready(function () {
            $('#<%= TextBox2.ClientID %>').change(function () {
                var startDateValue = $('#<%= TextBox1.ClientID %>').val();
				var endDateValue = $(this).val();
				console.log("Start date is : " + startDateValue);
        if (!startDateValue || !endDateValue) {
            alert('Please enter both start date and end date.');
            return; // Exit the function
        }

        var startDate = new Date(startDateValue);
        var endDate = new Date(endDateValue);

        // Check if the end date is before the start date
        if (endDate < startDate) {
            alert('End date cannot be before start date.');
            $(this).val(startDateValue); // Reset the end date to the start date
            return; // Exit the function
        }

        // Check if start date is after end date
        if (startDate > endDate) {
            alert('Please select a valid end date.');
            return; // Exit the function
        }

        // Calculate the number of days between the start date and end date (inclusive)
        var numberOfDays = Math.ceil((endDate - startDate) / (1000 * 60 * 60 * 24)) + 1;
        console.log("numberOfDays : " + numberOfDays );

        // Update the value of txtnoofdays TextBox
        $('#<%= TextBox3.ClientID %>').val(numberOfDays);
    });
        });

    </script>


</asp:Content>
