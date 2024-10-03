<%@ Page Title="" Language="C#" MasterPageFile="~/Employee.Master" AutoEventWireup="true" CodeBehind="EmployeeTimeOff.aspx.cs" Inherits="Human_Resource_Management.EmployeeTimeOff" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
							<h3 class="page-title">TimeOff</h3>
							<ul class="breadcrumb">
								<li class="breadcrumb-item"><a href="EmployeeDashboard.aspx">Dashboard</a></li>
								<li class="breadcrumb-item active">Time Off</li>
							</ul>
						</div>
						
					</div>
				</div>
				<!-- /Page Header -->
				<asp:Label ID="Label1" runat="server" Text="Label" ForeColor="Red"></asp:Label>
				<div class="row">
					<div class="col-md-3">
						<div class="input-block mb-3">
							<label class="col-form-label">Start Date & Time<span class="text-danger">*</span></label>							
							<asp:TextBox ID="TextBox1" runat="server" CssClass="form-control"></asp:TextBox>
						</div>
					</div>
					<div class="col-md-3">
						
						<div class="input-block mb-3">
							<label class="col-form-label">End Date & Time<span class="text-danger">*</span></label>							
							<asp:TextBox ID="TextBox2" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="TextBox2_TextChanged"></asp:TextBox>
						</div>
					</div>
					<div class="col-md-3">
						<div class="input-block mb-3">
							<label class="col-form-label">Reason<span class="text-danger">*</span></label>
							<asp:TextBox ID="TextBox3" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
						</div>
					</div>
					<div class="col-md-3">
						<div class="submit-section">							
							<asp:Button ID="BtnApplyTimeOff" runat="server" Text="Apply TimeOff" CssClass="btn btn-primary submit-btn" OnClick="BtnApplyTimeOff_Click" />
						</div>
					</div>
				</div>

                <div class="row" style="margin-top: 40px;"> 
					<div class="col-md-12">
						<div class="table-responsive">
							<table class="table table-striped custom-table leave-employee-table mb-0 datatable">
								<thead class="custom-header">
									<tr style="background-color:green;color:black;">
										<th>S.No</th>
										<th>Name</th>
										<th>Start Time</th>
										<th>End Time</th>
										<th>Duration</th>
										<th>Reason</th>
										<th>Status</th>
										<th>Approved by</th>
									</tr>
								</thead>	
								<tbody>
									 <asp:PlaceHolder ID="TimeOffData" runat="server"></asp:PlaceHolder>
								</tbody>
							</table>
						</div>
					</div>
				</div>
			</div>
		</div>
	</div>	
	<script>
		document.getElementById('btnApplyLeave').addEventListener('click', function() {
			var startDateTime = document.getElementById('txtStartDateTime').value;
			var endDateTime = document.getElementById('txtEndDateTime').value;
			var reason = document.getElementById('txtReason').value;

			// Parse the datetime-local values
			var startDate = new Date(startDateTime);
			var endDate = new Date(endDateTime);

			// Ensure the dates are in 24-hour format
			var formattedStartDate = formatDate(startDate);
			var formattedEndDate = formatDate(endDate);

			// Example of displaying the formatted dates
			console.log("Start Date & Time:", formattedStartDate);
			console.log("End Date & Time:", formattedEndDate);
			console.log("Reason:", reason);

			// Add your form submission logic here
			// For example, using AJAX to submit data

			// Optionally, if using a form submission
			// document.getElementById('yourFormId').submit();
		});

		function formatDate(date) {
			var year = date.getFullYear();
			var month = ('0' + (date.getMonth() + 1)).slice(-2);
			var day = ('0' + date.getDate()).slice(-2);
			var hours = ('0' + date.getHours()).slice(-2);
			var minutes = ('0' + date.getMinutes()).slice(-2);

			return `${year}-${month}-${day} ${hours}:${minutes}`;
		}
	</script>
</asp:Content>
