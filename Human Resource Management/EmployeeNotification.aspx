<%@ Page Title="" Language="C#" MasterPageFile="~/Employee.Master" AutoEventWireup="true" CodeBehind="EmployeeNotification.aspx.cs" Inherits="Human_Resource_Management.EmployeeNotification" %>
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
                            <h3 class="page-title">Notification Settings</h3>
                            <ul class="breadcrumb">
                                <li class="breadcrumb-item"><a href="admin-dashboard.html">Dashboard</a></li>
                                <li class="breadcrumb-item active">Notification</li>
                            </ul>
                        </div>
                        <div class="col-auto float-end ms-auto">
                            <a  href="EmployeeChat.aspx" class="btn add-btn"><i class="fa-solid fa-plus"></i>Chat</a>
                        </div>
                    </div>
                </div>
				<!-- /Page Header -->
				<%--<asp:Button ID="btnExportToExcel" runat="server" Text="Excel" OnClick="btnExportToExcel_Click" />--%>
				<div class="row">
					  
					<div class="row">
						<div class="col-md-12">
							<div class="table-responsive">
								<table class="table table-striped custom-table datatable">
									<thead>
										<tr>
											<th>S.NO</th>
											<th>EmpId</th>
											<th>Company</th>
											<th>Branch</th>
											<th>Subject</th>
											<th>Message</th>
										    <th>SendBy</th>
											<th>Date</th>
										</tr>
									</thead>
									<tbody>
										<asp:PlaceHolder ID="NotificationData" runat="server"></asp:PlaceHolder>
									</tbody>
								</table>
							</div>
						</div>
					</div>
				</div>
			</div>
		</div>
		<!-- /Page Content -->	
    </div>
	<!-- /Page Wrapper -->
	

</asp:Content>
