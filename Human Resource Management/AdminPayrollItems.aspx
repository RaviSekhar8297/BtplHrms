<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="AdminPayrollItems.aspx.cs" Inherits="Human_Resource_Management.AdminPayrollItems" %>

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
                            <h3 class="page-title">Payroll Items</h3>
                            <ul class="breadcrumb">
                                <li class="breadcrumb-item"><a href="Admindashboard">Dashboard</a></li>
                                <li class="breadcrumb-item active">Payroll Items</li>
                            </ul>
                        </div>
                    </div>
                </div>
                <!-- /Page Header -->

                <!-- Page Tab -->
                <div class="page-menu">
                    <div class="row">
                        <div class="col-sm-12">
                            <ul class="nav nav-tabs nav-tabs-bottom">
                                <li class="nav-item">
                                    <a class="nav-link active" data-bs-toggle="tab" href="#tab_additions">Additions</a>
                                </li>
                                <li class="nav-item">
                                    <a class="nav-link" data-bs-toggle="tab" href="#tab_overtime">Overtime</a>
                                </li>
                                <li class="nav-item">
                                    <a class="nav-link" data-bs-toggle="tab" href="#tab_deductions">Deductions</a>
                                </li>
                            </ul>
                        </div>
                    </div>
                </div>
                <!-- /Page Tab -->

                <!-- Tab Content -->
                <div class="tab-content">

                    <!-- Additions Tab -->
                    <div class="tab-pane show active" id="tab_additions">

                        <!-- Add Addition Button -->
                        <div class="text-end mb-4 clearfix">
                            <button class="btn btn-primary add-btn" type="button" data-bs-toggle="modal" data-bs-target="#add_addition"><i class="fa-solid fa-plus"></i>Add Addition</button>
                        </div>
                        <!-- /Add Addition Button -->

                        <!-- Payroll Additions Table -->
                        <div class="payroll-table card">
                            <div class="table-responsive">
                                <table class="table table-hover table-radius">
                                    <thead>
                                        <tr>
                                            <th>S.No</th>
                                            <th>Id</th>
                                            <th>Name</th>
                                            <th>Category</th>
                                            <th>Amount</th>
                                            <th>Edit</th>
                                            <th>Delete</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:PlaceHolder ID="AdditionsData" runat="server"></asp:PlaceHolder>
                                        <%--<tr>
											<th>Leave balance amount</th>
											<td>Monthly remuneration</td>
											<td>$5</td>
											<td class="text-end">
												<div class="dropdown dropdown-action">
													<a href="#" class="action-icon dropdown-toggle" data-bs-toggle="dropdown" aria-expanded="false"><i class="material-icons">more_vert</i></a>
													<div class="dropdown-menu dropdown-menu-right">
														<a class="dropdown-item" href="#" data-bs-toggle="modal" data-bs-target="#edit_addition"><i class="fa-solid fa-pencil m-r-5"></i> Edit</a>
														<a class="dropdown-item" href="#" data-bs-toggle="modal" data-bs-target="#delete_addition"><i class="fa-regular fa-trash-can m-r-5"></i> Delete</a>
													</div>
												</div>
											</td>
										</tr>
										<tr>
											<th>Arrears of salary</th>
											<td>Additional remuneration</td>
											<td>$8</td>
											<td class="text-end">
												<div class="dropdown dropdown-action">
													<a href="#" class="action-icon dropdown-toggle" data-bs-toggle="dropdown" aria-expanded="false"><i class="material-icons">more_vert</i></a>
													<div class="dropdown-menu dropdown-menu-right">
														<a class="dropdown-item" href="#" data-bs-toggle="modal" data-bs-target="#edit_addition"><i class="fa-solid fa-pencil m-r-5"></i> Edit</a>
														<a class="dropdown-item" href="#" data-bs-toggle="modal" data-bs-target="#delete_addition"><i class="fa-regular fa-trash-can m-r-5"></i> Delete</a>
													</div>
												</div>
											</td>
										</tr>
										<tr>
											<th>Gratuity</th>
											<td>Monthly remuneration</td>
											<td>$20</td>
											<td class="text-end">
												<div class="dropdown dropdown-action">
													<a href="#" class="action-icon dropdown-toggle" data-bs-toggle="dropdown" aria-expanded="false"><i class="material-icons">more_vert</i></a>
													<div class="dropdown-menu dropdown-menu-right">
														<a class="dropdown-item" href="#" data-bs-toggle="modal" data-bs-target="#edit_addition"><i class="fa-solid fa-pencil m-r-5"></i> Edit</a>
														<a class="dropdown-item" href="#" data-bs-toggle="modal" data-bs-target="#delete_addition"><i class="fa-regular fa-trash-can m-r-5"></i> Delete</a>
													</div>
												</div>
											</td>
										</tr>--%>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                        <!-- /Payroll Additions Table -->

                    </div>
                    <!-- Additions Tab -->

                    <!-- Overtime Tab -->
                    <div class="tab-pane" id="tab_overtime">

                        <!-- Add Overtime Button -->
                        <div class="text-end mb-4 clearfix">
                            <button class="btn btn-primary add-btn" type="button" data-bs-toggle="modal" data-bs-target="#add_overtime"><i class="fa-solid fa-plus"></i>Add Overtime</button>
                        </div>
                        <!-- /Add Overtime Button -->

                        <!-- Payroll Overtime Table -->
                        <div class="payroll-table card">
                            <div class="table-responsive">
                                <table class="table table-hover table-radius">
                                    <thead>
                                        <tr>
                                            <th>S.No</th>
                                            <th>Id</th>
                                            <th>Name</th>
                                            <th>Type</th>
                                            <th>Salary</th>
                                            <th>Edit</th>
                                            <th>Delete</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:PlaceHolder ID="OverTimeData" runat="server"></asp:PlaceHolder>
                                        <%--<tr>
											<th>Normal day OT 1.5x</th>
											<td>Hourly 1.5</td>
											<td class="text-end">
												<div class="dropdown dropdown-action">
													<a href="#" class="action-icon dropdown-toggle" data-bs-toggle="dropdown" aria-expanded="false"><i class="material-icons">more_vert</i></a>
													<div class="dropdown-menu dropdown-menu-right">
														<a class="dropdown-item" href="#" data-bs-toggle="modal" data-bs-target="#edit_overtime"><i class="fa-solid fa-pencil m-r-5"></i> Edit</a>
														<a class="dropdown-item" href="#" data-bs-toggle="modal" data-bs-target="#delete_overtime"><i class="fa-regular fa-trash-can m-r-5"></i> Delete</a>
													</div>
												</div>
											</td>
										</tr>
										<tr>
											<th>Public holiday OT 3.0x</th>
											<td>Hourly 3</td>
											<td class="text-end">
												<div class="dropdown dropdown-action">
													<a href="#" class="action-icon dropdown-toggle" data-bs-toggle="dropdown" aria-expanded="false"><i class="material-icons">more_vert</i></a>
													<div class="dropdown-menu dropdown-menu-right">
														<a class="dropdown-item" href="#" data-bs-toggle="modal" data-bs-target="#edit_overtime"><i class="fa-solid fa-pencil m-r-5"></i> Edit</a>
														<a class="dropdown-item" href="#" data-bs-toggle="modal" data-bs-target="#delete_overtime"><i class="fa-regular fa-trash-can m-r-5"></i> Delete</a>
													</div>
												</div>
											</td>
										</tr>
										<tr>
											<th>Rest day OT 2.0x</th>
											<td>Hourly 2</td>
											<td class="text-end">
												<div class="dropdown dropdown-action">
													<a href="#" class="action-icon dropdown-toggle" data-bs-toggle="dropdown" aria-expanded="false"><i class="material-icons">more_vert</i></a>
													<div class="dropdown-menu dropdown-menu-right">
														<a class="dropdown-item" href="#" data-bs-toggle="modal" data-bs-target="#edit_overtime"><i class="fa-solid fa-pencil m-r-5"></i> Edit</a>
														<a class="dropdown-item" href="#" data-bs-toggle="modal" data-bs-target="#delete_overtime"><i class="fa-regular fa-trash-can m-r-5"></i> Delete</a>
													</div>
												</div>
											</td>
										</tr>--%>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                        <!-- /Payroll Overtime Table -->

                    </div>
                    <!-- /Overtime Tab -->

                    <!-- Deductions Tab -->
                    <div class="tab-pane" id="tab_deductions">

                        <!-- Add Deductions Button -->
                        <div class="text-end mb-4 clearfix">
                            <button class="btn btn-primary add-btn" type="button" data-bs-toggle="modal" data-bs-target="#add_deduction"><i class="fa-solid fa-plus"></i>Add Deduction</button>
                        </div>
                        <!-- /Add Deductions Button -->

                        <!-- Payroll Deduction Table -->
                        <div class="payroll-table card">
                            <div class="table-responsive">
                                <table class="table table-hover table-radius">
                                    <thead>
                                        <tr>
                                            <th>S.No</th>
                                            <th>Id</th>
                                            <th>Name</th>
                                            <th>Type</th>
                                            <th>Amount</th>
                                            <th>Edit</th>
                                            <th>Delete</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:PlaceHolder ID="DeductionData" runat="server"></asp:PlaceHolder>
                                        <%--<tr>
											<th>Absent amount</th>
											<td>$12</td>
											<td class="text-end">
												<div class="dropdown dropdown-action">
													<a href="#" class="action-icon dropdown-toggle" data-bs-toggle="dropdown" aria-expanded="false"><i class="material-icons">more_vert</i></a>
													<div class="dropdown-menu dropdown-menu-right">
														<a class="dropdown-item" href="#" data-bs-toggle="modal" data-bs-target="#edit_deduction"><i class="fa-solid fa-pencil m-r-5"></i> Edit</a>
														<a class="dropdown-item" href="#" data-bs-toggle="modal" data-bs-target="#delete_deduction"><i class="fa-regular fa-trash-can m-r-5"></i> Delete</a>
													</div>
												</div>
											</td>
										</tr>
										<tr>
											<th>Advance</th>
											<td>$7</td>
											<td class="text-end">
												<div class="dropdown dropdown-action">
													<a href="#" class="action-icon dropdown-toggle" data-bs-toggle="dropdown" aria-expanded="false"><i class="material-icons">more_vert</i></a>
													<div class="dropdown-menu dropdown-menu-right">
														<a class="dropdown-item" href="#" data-bs-toggle="modal" data-bs-target="#edit_deduction"><i class="fa-solid fa-pencil m-r-5"></i> Edit</a>
														<a class="dropdown-item" href="#" data-bs-toggle="modal" data-bs-target="#delete_deduction"><i class="fa-regular fa-trash-can m-r-5"></i> Delete</a>
													</div>
												</div>
											</td>
										</tr>
										<tr>
											<th>Unpaid leave</th>
											<td>$3</td>
											<td class="text-end">
												<div class="dropdown dropdown-action">
													<a href="#" class="action-icon dropdown-toggle" data-bs-toggle="dropdown" aria-expanded="false"><i class="material-icons">more_vert</i></a>
													<div class="dropdown-menu dropdown-menu-right">
														<a class="dropdown-item" href="#" data-bs-toggle="modal" data-bs-target="#edit_deduction"><i class="fa-solid fa-pencil m-r-5"></i> Edit</a>
														<a class="dropdown-item" href="#" data-bs-toggle="modal" data-bs-target="#delete_deduction"><i class="fa-regular fa-trash-can m-r-5"></i> Delete</a>
													</div>
												</div>
											</td>
										</tr>--%>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                        <!-- /Payroll Deduction Table -->

                    </div>
                    <!-- /Deductions Tab -->

                </div>
                <!-- Tab Content -->

            </div>
            <!-- /Page Content -->

            <!-- Add Addition Modal -->
            <div id="add_addition" class="modal custom-modal fade" role="dialog">
                <div class="modal-dialog modal-dialog-centered" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title">Add Addition</h5>
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="modal-body">
                            <div class="input-block mb-3">
                                <label class="col-form-label">Name <span class="text-danger">*</span></label>
                                <asp:TextBox ID="txtaddname" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="input-block mb-3">
                                <label class="col-form-label">Category <span class="text-danger">*</span></label>
                                <asp:TextBox ID="txtaddcatagory" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <%--<div class="input-block mb-3">
									<label class="d-block col-form-label">Unit calculation</label>
									<div class="status-toggle">
										<asp:TextBox ID="TextBox2" runat="server" CssClass="form-control"></asp:TextBox>
										<label for="unit_calculation" class="checktoggle">checkbox</label>
									</div>
								</div>--%>
                            <div class="input-block mb-3">
                                <label class="col-form-label">Unit Amount</label>
                                <asp:TextBox ID="txtaddamount" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <%--	<div class="input-block mb-3">
									<label class="d-block col-form-label">Assignee</label>
									<div class="form-check form-check-inline">
										<input class="form-check-input" type="radio" name="addition_assignee" id="addition_no_emp" value="option1" checked>
										<label class="form-check-label" for="addition_no_emp">
										No assignee
										</label>
									</div>
									<div class="form-check form-check-inline">
										<input class="form-check-input" type="radio" name="addition_assignee" id="addition_all_emp" value="option2">
										<label class="form-check-label" for="addition_all_emp">
										All employees
										</label>
									</div>
									<div class="form-check form-check-inline">
										<input class="form-check-input" type="radio" name="addition_assignee" id="addition_single_emp" value="option3">
										<label class="form-check-label" for="addition_single_emp">
										Select Employee
										</label>
									</div>
									<div class="input-block mb-3">
										<select class="select">
											<option>-</option>
											<option>Select All</option>
											<option>John Doe</option>
											<option>Richard Miles</option>
										</select>
									</div>
								</div>--%>
                            <div class="submit-section">
                                <asp:Button ID="btnaddaditions" runat="server" Text="Submit" CssClass="btn btn-primary submit-btn" OnClick="btnaddaditions_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- /Add Addition Modal -->

            <!-- Edit Addition Modal -->
            <div id="edit_addition" class="modal custom-modal fade" role="dialog">
                <div class="modal-dialog modal-dialog-centered" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title">Edit Addition</h5>
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="modal-body">

                            <div class="input-block mb-3">
                                <label class="col-form-label">ID <span class="text-danger">*</span></label>
                                <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="input-block mb-3">
                                <label class="col-form-label">Name <span class="text-danger">*</span></label>
                                <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>

                            <div class="input-block mb-3">
                                <label class="col-form-label">Category</label>
                                <asp:TextBox ID="TextBox3" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="input-block mb-3">
                                <label class="col-form-label">Amount <span class="text-danger">*</span></label>
                                <asp:TextBox ID="TextBox4" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="submit-section">
                                <asp:Button ID="btnupdateadditions" runat="server" Text="Submit" CssClass="btn btn-primary submit-btn" OnClick="btnupdateadditions_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- /Edit Addition Modal -->

            <!-- Delete Addition Modal -->
            <div class="modal custom-modal fade" id="delete_addition" role="dialog">
                <div class="modal-dialog modal-dialog-centered">
                    <div class="modal-content">
                        <div class="modal-body">
                            <div class="form-header">
                                <h3>Delete Addition</h3>
                                <p>
                                    Are you sure want to delete
                                    <asp:Label ID="Label1" runat="server" ForeColor="#ff0000"></asp:Label>
                                    ?
                                </p>
                                <asp:HiddenField ID="HiddenField1" runat="server" />
                            </div>
                            <div class="modal-btn delete-action">
                                <div class="row">
                                    <div class="col-6">
                                        <asp:Button ID="btndeleteaddirtion" runat="server" Text="Delete" CssClass="btn btn-primary continue-btn" OnClick="btndeleteaddirtion_Click" />
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
            <!-- /Delete Addition Modal -->

            <!-- Add Overtime Modal -->
            <div id="add_overtime" class="modal custom-modal fade" role="dialog">
                <div class="modal-dialog modal-dialog-centered" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title">Add Overtime</h5>
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="modal-body">
                            <div class="input-block mb-3">
                                <label class="col-form-label">Name <span class="text-danger">*</span></label>
                                <asp:TextBox ID="txtaddovertimename" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="input-block mb-3">
                                <label class="col-form-label">Rate Type <span class="text-danger">*</span></label>
                                <asp:DropDownList ID="ddladdovertimetype" runat="server" CssClass="form-control">
                                    <asp:ListItem> -- Select Rate -- </asp:ListItem>
                                    <asp:ListItem>Hourly</asp:ListItem>
                                    <asp:ListItem>Day</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="input-block mb-3">
                                <label class="col-form-label">Rate <span class="text-danger">*</span></label>
                                <asp:TextBox ID="txtaddoverttimerate" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="submit-section">
                                <asp:Button ID="btnaddovertime" runat="server" Text="Submit" CssClass="btn btn-primary submit-btn" OnClick="btnaddovertime_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- /Add Overtime Modal -->

            <!-- Edit Overtime Modal -->
            <div id="edit_overtime" class="modal custom-modal fade" role="dialog">
                <div class="modal-dialog modal-dialog-centered" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title">Edit Overtime</h5>
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="modal-body">
                            <div class="input-block mb-3">
                                <label class="col-form-label">ID <span class="text-danger">*</span></label>
                                <asp:TextBox ID="TextBox5" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="input-block mb-3">
                                <label class="col-form-label">Name <span class="text-danger">*</span></label>
                                <asp:TextBox ID="TextBox6" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="input-block mb-3">
                                <label class="col-form-label">Rate Type <span class="text-danger">*</span></label>
                                <asp:TextBox ID="TextBox7" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="input-block mb-3">
                                <label class="col-form-label">Rate <span class="text-danger">*</span></label>
                                <asp:TextBox ID="TextBox8" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="submit-section">
                                <asp:Button ID="btnupdateovertime" runat="server" Text="Submit" CssClass="btn btn-primary submit-btn" OnClick="btnupdateovertime_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- /Edit Overtime Modal -->

            <!-- Delete Overtime Modal -->
            <div class="modal custom-modal fade" id="delete_overtime" role="dialog">
                <div class="modal-dialog modal-dialog-centered">
                    <div class="modal-content">
                        <div class="modal-body">
                            <div class="form-header">
                                <h3>Delete Overtime</h3>
                                <p>
                                    Are you sure want to delete
                                    <asp:Label ID="Label2" runat="server" ForeColor="#ff0000"></asp:Label>
                                    ?
                                </p>
                                <asp:HiddenField ID="HiddenField2" runat="server" />
                            </div>
                            <div class="modal-btn delete-action">
                                <div class="row">
                                    <div class="col-6">
                                        <asp:Button ID="btndeleteovertime" runat="server" Text="Delete" CssClass="btn btn-primary continue-btn" OnClick="btndeleteovertime_Click" />
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
            <!-- /Delete Overtime Modal -->

            <!-- Add Deduction Modal -->
            <div id="add_deduction" class="modal custom-modal fade" role="dialog">
                <div class="modal-dialog modal-dialog-centered" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title">Add Deduction</h5>
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="modal-body">
                            <div class="input-block mb-3">
                                <label class="col-form-label">Deduction Name <span class="text-danger">*</span></label>
                                <asp:TextBox ID="txtadddeductionname" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <%--<div class="input-block mb-3">
									<label class="d-block col-form-label">Unit calculation</label>
									<div class="status-toggle">
										<input type="checkbox" id="unit_calculation_deduction" class="check">
										<label for="unit_calculation_deduction" class="checktoggle">checkbox</label>
									</div>
								</div>--%>
                            <div class="input-block mb-3">
                                <label class="col-form-label">Deduction Type</label>
                                <asp:TextBox ID="txtadddeductiontype" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="input-block mb-3">
                                <label class="col-form-label">Deduction Amount</label>
                                <asp:TextBox ID="txtadddeductionamount" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <%--<div class="input-block mb-3">
									<label class="d-block col-form-label">Assignee</label>
									<div class="form-check form-check-inline">
										<input class="form-check-input" type="radio" name="deduction_assignee" id="deduction_no_emp" value="option1" checked>
										<label class="form-check-label" for="deduction_no_emp">
										No assignee
										</label>
									</div>
									<div class="form-check form-check-inline">
										<input class="form-check-input" type="radio" name="deduction_assignee" id="deduction_all_emp" value="option2">
										<label class="form-check-label" for="deduction_all_emp">
										All employees
										</label>
									</div>
									<div class="form-check form-check-inline">
										<input class="form-check-input" type="radio" name="deduction_assignee" id="deduction_single_emp" value="option3">
										<label class="form-check-label" for="deduction_single_emp">
										Select Employee
										</label>
									</div>
									<div class="input-block mb-3">
										<select class="select">
											<option>-</option>
											<option>Select All</option>
											<option>John Doe</option>
											<option>Richard Miles</option>
										</select>
									</div>
								</div>--%>
                            <div class="submit-section">
                                <asp:Button ID="btnadddeductions" runat="server" Text="Submit" CssClass="btn btn-primary submit-btn" OnClick="btnadddeductions_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- /Add Deduction Modal -->

            <!-- Edit Deduction Modal -->
            <div id="edit_deduction" class="modal custom-modal fade" role="dialog">
                <div class="modal-dialog modal-dialog-centered" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title">Edit Deduction</h5>
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="modal-body">
                            <div class="input-block mb-3">
                                <label class="col-form-label">ID <span class="text-danger">*</span></label>
                                <asp:TextBox ID="TextBox9" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="input-block mb-3">
                                <label class="d-block col-form-label">Name</label>
                                <asp:TextBox ID="TextBox10" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="input-block mb-3">
                                <label class="col-form-label">Type</label>
                                <asp:TextBox ID="TextBox11" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="input-block mb-3">
                                <label class="col-form-label">Amount</label>
                                <asp:TextBox ID="TextBox12" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="submit-section">
                                <asp:Button ID="btnupdatededuction" runat="server" Text="Submit" CssClass="btn btn-primary submit-btn" OnClick="btnupdatededuction_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- /Edit Addition Modal -->

            <!-- Delete Deduction Modal -->
            <div class="modal custom-modal fade" id="delete_deduction" role="dialog">
                <div class="modal-dialog modal-dialog-centered">
                    <div class="modal-content">
                        <div class="modal-body">
                            <div class="form-header">
                                <h3>Delete Deduction</h3>
                                <p>Are you sure want to delete <asp:Label ID="Label3" runat="server"  ForeColor="#ff0000"></asp:Label> ?</p>
                                <asp:HiddenField ID="HiddenField3" runat="server" />
                            </div>
                            <div class="modal-btn delete-action">
                                <div class="row">
                                    <div class="col-6">
                                         <asp:Button ID="btndeletededuction" runat="server" Text="Delete" CssClass="btn btn-primary continue-btn" OnClick="btndeletededuction_Click" />
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
            <!-- /Delete Deduction Modal -->

        </div>
        <!-- /Page Wrapper -->
    </div>
    <!-- /Main Wrapper -->
    <script type="text/javascript">

        function editadditions(Id, AdditionsName, AdditionsCatagory, AdditionAmount) {
            $('#<%= TextBox1.ClientID %>').val(Id).prop('readonly', true);
            $('#<%= TextBox2.ClientID %>').val(AdditionsName);
            $('#<%= TextBox3.ClientID %>').val(AdditionsCatagory);
            $('#<%= TextBox4.ClientID %>').val(AdditionAmount);

            $('#edit_department').modal('show');
        }
        function deleteaddadition(Id, AdditionsName) {
            document.getElementById('<%= Label1.ClientID %>').textContent = AdditionsName;
            $('#<%= HiddenField1.ClientID %>').val(Id);
        }



        function editovertime(Id, OverTimeName, OverTimeSalaryType, OverTimeSalary) {
            $('#<%= TextBox5.ClientID %>').val(Id).prop('readonly', true);
            $('#<%= TextBox6.ClientID %>').val(OverTimeName);
            $('#<%= TextBox7.ClientID %>').val(OverTimeSalaryType);
            $('#<%= TextBox8.ClientID %>').val(OverTimeSalary);
        }
        function deleteovertime(Id, OverTimeName) {
            document.getElementById('<%= Label2.ClientID %>').textContent = OverTimeName;
            $('#<%= HiddenField2.ClientID %>').val(Id);
        }



        function editdedudctions(Id, Deductionname, DeductionsType, DeductionAmount) {
            $('#<%= TextBox9.ClientID %>').val(Id).prop('readonly', true);
            $('#<%= TextBox10.ClientID %>').val(Deductionname);
            $('#<%= TextBox11.ClientID %>').val(DeductionsType);
            $('#<%= TextBox12.ClientID %>').val(DeductionAmount);
        }
        function deletedeductions(Id, Deductionname) {
            document.getElementById('<%= Label3.ClientID %>').textContent = Deductionname;
            $('#<%= HiddenField3.ClientID %>').val(Id);
        }
    </script>
</asp:Content>
