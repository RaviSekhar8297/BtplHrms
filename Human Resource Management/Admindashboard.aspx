<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="Admindashboard.aspx.cs" Inherits="Human_Resource_Management.Admindashboard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
	<%@ Import Namespace="System.Data" %>
	<%@ Import Namespace="System.ComponentModel" %>
	 <link href="assets/css/Admindashbord.css" rel="stylesheet" />
	 <meta charset="utf-8" content="" />
	<style type="text/css">
		.progressaa{
			width:100%;
		}
		.progressaa > progress{
			width:100% !important;
		}

		.grd1{
			height:100px;		
			overflow-y:scroll;
		}

		.gridClass {
			border-collapse: collapse;
			width: 100%;
		}

		.fixTableHead { 
			overflow-y: auto; 
			height:300px;
		} 

		.fixTableHead thead th { 
			position: sticky; 
			top: 0; 
		} 

		.gridClass th {
			background-color: #4CAF50; 
			color: white;
			padding: 8px;
			position: sticky;
			top: 0;
			z-index: 1;
		}

		.gridClass tr:nth-child(even) {
			background-color: #f2f2f2;
		}

		.gridClass tr:nth-child(odd) {
			background-color: #ffffff; 
		}

		.gridClass td {
			padding: 8px;
			border: 1px solid #ddd;
		}

		.tableClass{
			width:100%;
			padding:0px 10px;
			display:flex;
			align-items:center;
			justify-content:space-around;
		}

		.th{
			background:green;
			color:black;
			position:sticky;
		}

		.trbody{
			border-bottom:1px solid grey;
		}

		.img-circle{

		}

		.birthTable{
			height:200px;
			border:2px solid red;
		}

		.no-records-message {
			
			color: #6e6e6e;
			font-size: 18px;
			font-weight: bold;
			display: flex;
			align-items: center;
			justify-content: center;
			/* height: 300px; */
			width: 45%;
			border: 1px solid #3d3a3a;
			position: absolute;
			top: 50%;
			left: 50%;
			transform: translate(-50%, -50%);
			border-radius: 20px;
		}
		/* Styles for the card */
		/* Card styling */
		.card {
			border-radius: 10px;
			box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
			overflow: hidden;
		}

		/* Table styling */
		.custom-table th, .custom-table td {
			padding: 12px 15px;
			text-align: left;
			border-bottom: 1px solid #ddd;
		}

		/* Sticky header styling */
		.table-responsive {
			position: relative;
		}

		.sticky-header th {
			position: sticky;
			top: 0;
			background-color: #f4f6f9;
			z-index: 2;
			box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
		}

		/* Row hover effect */
		.custom-table tbody tr:hover {
			background-color: #f9f9f9;
		}

		/* Avatar image styling */
		.custom-table a.avatar img {
			border-radius: 50%;
			width: 40px;
			height: 40px;
		}

		/* Link styling */
		.custom-table td a {
			text-decoration: none;
			color: #007bff;
		}

		/* Custom scrollbar */
		.table-responsive::-webkit-scrollbar {
			height: 6px; /* Adjust this value to decrease the height of the X-axis scrollbar */
			width: 8px; /* Keep this for the Y-axis (vertical) scrollbar width */
		}
		.table-responsive::-webkit-scrollbar-track {
			background: #fff0e6;
			border-radius: 10px;
		}

		.table-responsive::-webkit-scrollbar-thumb {
			background: #ffd1b3;
			border-radius: 10px;
		}

		.table-responsive::-webkit-scrollbar-thumb:hover {
			background: #555;
		}

		/* For Firefox */
		/*.table-responsive {
			scrollbar-width: thin;
			scrollbar-color: #888 #f1f1f1;
		}*/

	</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
	<%--<form id="form1" runat="server">--%>
    <div class="page-wrapper">
		<!-- Page Content -->
		<div class="content container-fluid">
			<!--Page Header Start-->
			<div class="page-header">
				<div class="row">
					<div class="col-sm-12">
						<h3 class="page-title">Welcome <asp:Label ID="lblwelcomename" runat="server" Text="Label"></asp:Label>!</h3>
						<ul class="breadcrumb">
							<li class="breadcrumb-item active">Dashboard</li>
						</ul>
					</div>
				</div>
			</div>
			<!--Page Header End-->
				
			<!--Statistics Widget -->
			<div class="row">				
				<!-- Daily Statics Start-->
					<div class="col-md-12 col-lg-12 col-xl-4 d-flex">
					<div class="card flex-fill dash-statistics">
						<div class="card-body">
							<h4 class="card-title">Daily Statistics </h4>
							<div class="stats-list">
								<!-- Daily Presents Start-->
								<div class="stats-info">
									<p >Presents <strong><asp:Label ID="Label1" runat="server"></asp:Label>(<span id="dayPresentper" style="color:red;"></span>%) </strong></p>  
									<div class="progressaa">
									    <progress id="presentprogress" value="0" max="100" ></progress>	
									</div>
								</div>
								<!-- Daily Presents End-->

								<!-- Daily Absents Start-->
								<div class="stats-info">
									<p>Absents<strong><asp:Label ID="Label2" runat="server"></asp:Label>(<span id="daypAbsentper" style="color:red;"></span>%)</strong></p>
									<div class="progressaa">
										<progress id="progressAbsent" value="0" max="100" ></progress>	
									</div>
								</div>
								<!-- Daily Absents End-->

								<!-- Daily Late Logins Start-->
								<div class="stats-info">
									<p>Late Logins<strong><asp:Label ID="Label3" runat="server"></asp:Label>(<span id="daypLatelogper" style="color:red;"></span>%)</strong></p>
									<div class="progressaa">
										<progress id="progressLate" value="0" max="100" ></progress>	
									</div>
								</div>
								<!-- Daily Late Logins End-->

								<!-- Daily Early Comes Start-->
								<div class="stats-info">
									<p>Early Comes<strong><asp:Label ID="Label4" runat="server"></asp:Label>(<span id="dayEarlyComesper" style="color:red;"></span>%)</strong></p>
									<div class="progressaa">
										<progress id="progressEarlyComes" value="0" max="100" ></progress>	
									</div>
								</div>
								<!-- Daily Early Comes End-->

								<!-- Daily Early Goes Start-->
								<div class="stats-info">
									<p>Total Employees <strong><asp:Label ID="Label5" runat="server"></asp:Label></strong></p>
									<div class="progressaa">
										<progress id="totalprogress" value="100" max="100" ></progress>							
									</div>
								</div>
								<!-- Daily Early Goes End-->
							</div>
						</div>
					</div>
				</div>
					<!-- Daily Statics End-->
				

				<!-- Weekly Statics Start-->
				<div class="col-md-12 col-lg-12 col-xl-4 d-flex">
					<div class="card flex-fill dash-statistics">
						<div class="card-body">
							<h4 class="card-title">Weekly Statistics</h4>
							<div class="stats-list">
								<!-- Weekly Presents Start-->
								<div class="stats-info">
									<p>Presents <strong><asp:Label ID="Label6" runat="server"></asp:Label>(<span id="weekpresentper" style="color:red;"></span>%)</strong></p>
									<div class="progressaa">
										  <progress id="WaeklyPresentProgrees" value="0" max="100" ></progress>	
									</div>
								</div>
								<!-- Weekly Presents End-->

								<!-- Weekly Absents Start-->
								<div class="stats-info">
									<p>Absents<strong><asp:Label ID="Label8" runat="server"></asp:Label>(<span id="weekabsentper" style="color:red;"></span>%)</strong></p>
									<div class="progressaa">
										  <progress id="WaeklyAbsentProgrees" value="0" max="100" ></progress>	
									</div>
								</div>
								<!-- Weekly Absents End-->

								<!-- Weekly Late Logins Start-->
								<div class="stats-info">
									<p>Late Logins<strong><asp:Label ID="Label9" runat="server"></asp:Label>(<span id="weeklatelogtper" style="color:red;"></span>%)</strong></p>
									<div class="progressaa">
										<progress id="WaeklyLateLogintProgrees" value="0" max="100" ></progress>	
									</div>
								</div>
								<!-- Weekly Late Logins End-->

								<!-- Weekly Early Comes Start-->
								<div class="stats-info">
									<p>Early Comes<strong><asp:Label ID="Label10" runat="server"></asp:Label>(<span id="weekearlycomeper" style="color:red;"></span>%)</strong></p>
									<div class="progressaa">
										<progress id="WaeklyEarlyComesProgrees" value="0" max="100" ></progress>	
									</div>
								</div>
								<!-- Weekly Early Comes End-->

								<!-- Weekly Early Goes Start-->
								<div class="stats-info">
									<p>Total Employees  <strong><asp:Label ID="Label7" runat="server"></asp:Label></strong></p>
									<div class="progressaa">
										<progress id="WaeklyTotalProgrees" value="100" max="100" ></progress>
									</div>
								</div>
								<!-- Weekly Early Goes End-->
							</div>
						</div>
					</div>
				</div>
				<!-- Weekly Statics End-->

				<!-- Monthly Statics Start-->
				<div class="col-md-12 col-lg-12 col-xl-4 d-flex">
					<div class="card flex-fill dash-statistics">
						<div class="card-body">
							<h5 class="card-title">Monthly Statistics</h5>
							<div class="stats-list">
								<!--Monthly Presents Start-->
								<div class="stats-info">
									<p>Presents <strong><asp:Label ID="Label11" runat="server"></asp:Label>(<span id="monthlypresentper" style="color:red;"></span>%)</strong></p>
									<div class="progressaa">
										<progress id="monthlyPresentProgrees" value="0" max="100" ></progress>
									</div>
								</div>
								<!--Monthly Presents End-->

								<!--Monthly Absents Start-->
								<div class="stats-info">
									<p>Absents<strong><asp:Label ID="Label12" runat="server"></asp:Label>(<span id="monthlyabsentper" style="color:red;"></span>%)</strong></p>
									<div class="progressaa">
										<progress id="monthlyAbsentProgrees" value="0" max="100" ></progress>
									</div>
								</div>
								<!--Monthly Absents End-->

								<!--Monthly Late Logins Start-->
								<div class="stats-info">
									<p>Late Logins<strong><asp:Label ID="Label13" runat="server"></asp:Label>(<span id="monthlylatelogper" style="color:red;"></span>%)</strong></p>
									<div class="progressaa">
										<progress id="monthlyLateLoginProgrees" value="0" max="100" ></progress>
									</div>
								</div>
								<!--Monthly Late Logins End-->

								<!--Monthly Early Comes Start-->
								<div class="stats-info">
									<p>Early Comes<strong><asp:Label ID="Label14" runat="server"></asp:Label>(<span id="monthlyearlycomeper" style="color:red;"></span>%)</strong></p>
									<div class="progressaa">
										<progress id="monthlyEarlyComesProgrees" value="0" max="100" ></progress>
									</div>
								</div>
								<!--Monthly Early Comes End-->

								<!--Monthly Early Goes Start-->
								<div class="stats-info">
									<p>Total Employees  <strong><asp:Label ID="Label15" runat="server"></asp:Label></strong></p>
									<div class="progressaa">
										<progress id="monthlyTotalProgrees" value="100" max="100" ></progress>
									</div>
								</div>
								<!--Monthly Early Goes End-->
							</div>
						</div>
					</div>
				</div>			
			
				<!-- Monthly Statics End-->
			</div>
			<!--Statistics Widget -->
					
			<div class="row">
				<!--Early Comes-->
				<div class="col-md-6 d-flex" >
					<div class="card card-table flex-fill">
						<div class="card-header">
							<h3 class="card-title mb-0">Early Comes</h3>
						</div>
						<div class="table-responsive" style="max-height: 300px;  min-height:300px; overflow-y: auto;">
							<table class="table custom-table mb-0">
								<thead class="sticky-header">
									<tr>
										<th>Name</th>
										<th>Time</th>
										<th>Department</th>
										<%--<th>Designation</th>	--%>										
									</tr>
								</thead>
								<tbody>
									<asp:PlaceHolder ID="EarlyComesData" runat="server"></asp:PlaceHolder>		
								</tbody>
									
							</table>				
							<div style="display:flex;justify-content:center;align-items:center;">
								<asp:Literal ID="Literal1" runat="server"></asp:Literal>
							</div>
						</div>
					</div>			
				</div>
				<!--/Early Comes-->

				<!--Late Logins-->
				<div class="col-md-6 d-flex">
					<div class="card card-table flex-fill">
						<div class="card-header">
							<h3 class="card-title mb-0">Late Logins</h3>
						</div>
						<!--table body start-->
						<div class="table-responsive" style="max-height: 300px;  min-height:300px; overflow-y: auto;">
							<table class="table custom-table mb-0">
								<thead class="sticky-header">
									<tr>
										<th>Name</th>
										<th>Time</th>
										<th>Department</th>
										<%--<th>Designation</th>--%>											
									</tr>
								</thead>
								<tbody>
									<asp:PlaceHolder ID="LateLogInsData" runat="server"></asp:PlaceHolder>		
								</tbody>
							</table>
							<div style="display:flex;justify-content:center;align-items:center;">
								<asp:Literal ID="Literal2" runat="server"></asp:Literal>
							</div>
						</div>
						<!--Table body End-->		
					</div>
					<!--/Late Logins-->		
				</div>
			</div>

			<div class="row">
				<!--Absents-->
				<div class="col-md-6 d-flex">
					<div class="card card-table flex-fill">
						<div class="card-header">
							<h3 class="card-title mb-0">Absents</h3>
						</div>
						<!--table body start-->
						<div class="table-responsive" style="max-height: 300px;  min-height:300px; overflow-y: auto;">
							<table class="table custom-table mb-0">
								<thead class="sticky-header">
									<tr>
										<th>Name</th>
										<th>Department</th>
										<th>Designation</th>
									</tr>
								</thead>
								<tbody>
									<asp:PlaceHolder ID="AbsentData" runat="server"></asp:PlaceHolder>
								</tbody>
							</table>
							<div style="display:flex;justify-content:center;align-items:center;">
								<asp:Literal ID="Literal3" runat="server"></asp:Literal></div>
							</div>
						</div>
						
					</div>	
				<!--/Absents-->

				<!--Early Goes-->
				<div class="col-md-6 d-flex">
					<div class="card card-table flex-fill">
						<div class="card-header">
							<h3 class="card-title mb-0">Early Goes</h3>
						</div>
						<!--table body start-->
						<div class="table-responsive" style="max-height: 300px; min-height:300px; overflow-y: auto;">
							<table class="table custom-table mb-0">
								<thead class="sticky-header">
									<tr>
										<th>Name</th>
										<th>Time</th>
										<th>Designation</th>
									</tr>
								</thead>
								<tbody>
									<asp:PlaceHolder ID="EarlyGoesData" runat="server"></asp:PlaceHolder>																			
								</tbody>
							</table>
							<div style="display:flex;justify-content:center;align-items:center;">
								<asp:Literal ID="Literal4" runat="server"></asp:Literal>
							</div>
						</div>
					</div>
				</div>		
				<!--/Early Goes-->
			</div>

			<div class="row">
				<!--Birthdays-->
				<div class="col-md-6 d-flex">
					<div class="card card-table flex-fill ">
						<div class="card-header">
							<h3 class="card-title mb-0">Birthdays 🎂</h3>
						</div>
						<!--table body start-->
						<div class="table-responsive" style="max-height: 300px; min-height:300px; overflow-y: auto;">
							<table class="table custom-table mb-0">
								<thead class="sticky-header">
									<tr>
										<th>Name</th>
										<th>Department</th>
										<th>Designation</th>
									</tr>
								</thead>
								<tbody>
									<asp:PlaceHolder ID="AdminBirthdaysData" runat="server"></asp:PlaceHolder>																			
								</tbody>
								
							</table>
							<div style="display:flex;justify-content:center;align-items:center;">
								<asp:Literal ID="Literal5" runat="server"></asp:Literal>
							</div>
							
						</div>
						<!--Table body End-->
					</div>
				</div>
				<!--/Birthdays-->

				<!--Anniversarys-->
				<div class="col-md-6 d-flex">
					<div class="card card-table flex-fill" >
						<div class="card-header">
							<h3 class="card-title mb-0">Anniversarys</h3>
						</div>
						<!--table body start-->
						<div class="table-responsive" style="max-height: 300px; min-height:300px; overflow-y: auto;">
							<table class="table custom-table mb-0">
								<thead class="sticky-header">
									<tr>
										<th>Name</th>
										<th>Department</th>
										<th>Designation</th>
									</tr>
								</thead>
								<tbody>
									<asp:PlaceHolder ID="AnniversaryData" runat="server"></asp:PlaceHolder>			
								</tbody>
							</table>
							<div style="display:flex;justify-content:center;align-items:center;">
								<asp:Literal ID="Literal6" runat="server"></asp:Literal>
							</div>
						</div>
					</div>
				</div>
				<!--/Anniversarys-->
			</div>
					
			<div class="row">
				<!--New Joinings-->
				<div class="col-md-6 d-flex">
					<div class="card card-table flex-fill">
						<div class="card-header">
							<h3 class="card-title mb-0">New Joinings</h3>
						</div>
						<!--table body start-->
						<div class="table-responsive" style="max-height: 300px;  min-height:300px; overflow-y: auto;">
							<table class="table custom-table mb-0">
								<thead class="sticky-header">
									<tr>
										<th>Name</th>
										<th>Department</th>
										<th>Designation</th>
									</tr>
								</thead>
								<tbody>
									<asp:PlaceHolder ID="NewJoin" runat="server"></asp:PlaceHolder>			
								</tbody>
							</table>
							<div style="display:flex;justify-content:center;align-items:center;">
									<asp:Literal ID="Literal7" runat="server"></asp:Literal>
							</div>
						</div>
						<!--Table body End-->
					</div>
				</div>
				<!--/New Joinings-->

				<!--Relieving's-->
				<div class="col-md-6 d-flex">
					<div class="card card-table flex-fill">
						<div class="card-header">
							<h3 class="card-title mb-0">Relieving's </h3>
						</div>
						<!--table body start-->
						<div class="card-body" style="height:300px;overflow-y:scroll;">
							<div class="table-responsive" style="max-height: 300px;  min-height:300px; overflow-y: auto;">
								<table class="table custom-table mb-0">
									<thead class="sticky-header">
										<tr>
											<th>Name</th>
											<th>Department</th>
											<th>Designation</th>
										</tr>
									</thead>
									<tbody>
										<asp:PlaceHolder ID="RelievingData" runat="server"></asp:PlaceHolder>		
									</tbody>
								</table>
								<div style="display:flex;justify-content:center;align-items:center;">
									<asp:Literal ID="Literal8" runat="server"></asp:Literal>
								</div>
							</div>
							<!--Table body End-->
						</div>
					</div>
					<!--Relieving's-->
				</div>
				<!--/Relieving's-->
			</div>
			<!-- /Page Content -->
		</div>
	</div>
	<%--	</form>--%>

	<script type="text/javascript">
        document.addEventListener("DOMContentLoaded", function () {
            var vv = "ravi";
            console.log(vv);

            // Function to safely get element text content by ID
            function getElementTextContentById(id) {
                let element = document.getElementById(id);
                return element ? element.textContent : 0;
            }

            let total = parseInt(getElementTextContentById('<%= Label5.ClientID %>')) || 0;
            let pre = parseInt(getElementTextContentById('<%= Label1.ClientID %>')) || 0;
            let absent = parseInt(getElementTextContentById('<%= Label2.ClientID %>')) || 0;
            let lateLogins = parseInt(getElementTextContentById('<%= Label3.ClientID %>')) || 0;
            let earlyComes = parseInt(getElementTextContentById('<%= Label4.ClientID %>')) || 0;

            /* Present progress */
            let presentPer = document.getElementById("presentprogress");
            if (presentPer) {
                let tPres = Math.trunc((pre / total) * 100) || 0;
                presentPer.value = tPres;
                document.getElementById('dayPresentper').textContent = tPres;
            }

            /* Absent progress */
            let Abs = document.getElementById("progressAbsent");
            if (Abs) {
                let perAbs = Math.trunc((absent / total) * 100) || 0;
                Abs.value = perAbs;
                document.getElementById('daypAbsentper').textContent = perAbs;
            }

            /* Late login progress */
            let late = document.getElementById("progressLate");
            if (late) {
                let perLate = Math.trunc((lateLogins / total) * 100) || 0;
                late.value = perLate;
                document.getElementById('daypLatelogper').textContent = perLate;
            }

            /* Early comes progress */
            let Early = document.getElementById("progressEarlyComes");
            if (Early) {
                let perEarly = Math.trunc((earlyComes / total) * 100) || 0;
                Early.value = perEarly;
                document.getElementById('dayEarlyComesper').textContent = perEarly;
            }

            /*	weekly javscrip stert  */
            let weektotal = parseInt(getElementTextContentById('<%= Label7.ClientID %>')) || 0;
            let weekpresent = parseInt(getElementTextContentById('<%= Label6.ClientID %>')) || 0;
            let weekabsent = parseInt(getElementTextContentById('<%= Label8.ClientID %>')) || 0;
            let weeklatelog = parseInt(getElementTextContentById('<%= Label9.ClientID %>')) || 0;
            let weekearlycome = parseInt(getElementTextContentById('<%= Label10.ClientID %>')) || 0;

            let weekpresentPer = document.getElementById("WaeklyPresentProgrees");
            if (weekpresentPer) {
                let wtPres = Math.trunc((weekpresent / weektotal) * 100) || 0;
                weekpresentPer.value = wtPres;
                document.getElementById('weekpresentper').textContent = wtPres;
			}

            let weekabsentPer = document.getElementById("WaeklyAbsentProgrees");
            if (weekabsentPer) {
                let wabPres = Math.trunc((weekabsent / weektotal) * 100) || 0;
                weekabsentPer.value = wabPres;
                document.getElementById('weekabsentper').textContent = wabPres;
			}

            let weeklatetPer = document.getElementById("WaeklyLateLogintProgrees");
            if (weeklatetPer) {
                let wltPres = Math.trunc((weeklatelog / weektotal) * 100) || 0;
                weeklatetPer.value = wltPres;
                document.getElementById('weeklatelogtper').textContent = wltPres;
			}

            let weekearlyPer = document.getElementById("WaeklyEarlyComesProgrees");
            if (weekearlyPer) {
                let wearPres = Math.trunc((weekearlycome / weektotal) * 100) || 0;
                weekearlyPer.value = wearPres;
                document.getElementById('weekearlycomeper').textContent = wearPres;
			}
            /*	weekly javscrip End  */

            /*	Monthly javscrip stert  */
            let monthtotal = parseInt(getElementTextContentById('<%= Label15.ClientID %>')) || 0;
            let monthkpresent = parseInt(getElementTextContentById('<%= Label11.ClientID %>')) || 0;
            let monthabsent = parseInt(getElementTextContentById('<%= Label12.ClientID %>')) || 0;
            let monthlatelog = parseInt(getElementTextContentById('<%= Label13.ClientID %>')) || 0;
            let monthearlycome = parseInt(getElementTextContentById('<%= Label14.ClientID %>')) || 0;


            let monthpresentPer = document.getElementById("monthlyPresentProgrees");
            if (monthpresentPer) {
                let mpPres = Math.trunc((monthkpresent / monthtotal) * 100) || 0;
                monthpresentPer.value = mpPres;
                document.getElementById('monthlypresentper').textContent = mpPres;
            }

            let monthabsentPer = document.getElementById("monthlyAbsentProgrees");
            if (monthabsentPer) {
                let mabPres = Math.trunc((monthabsent / monthtotal) * 100) || 0;
                monthabsentPer.value = mabPres;
                document.getElementById('monthlyabsentper').textContent = mabPres;
            }

            let monthlatetPer = document.getElementById("monthlyLateLoginProgrees");
            if (monthlatetPer) {
                let mltPres = Math.trunc((monthlatelog / monthtotal) * 100) || 0;
                monthlatetPer.value = mltPres;
                document.getElementById('monthlylatelogper').textContent = mltPres;
            }

            let monthearlyPer = document.getElementById("monthlyEarlyComesProgrees");
            if (monthearlyPer) {
                let maarPres = Math.trunc((monthearlycome / monthtotal) * 100) || 0;
                monthearlyPer.value = maarPres;
                document.getElementById('monthlyearlycomeper').textContent = maarPres;
            }
            /*	Monthly javscrip end  */
        });
    </script>
</asp:Content>