<%@ Page Title="" Language="C#" MasterPageFile="~/Manager.Master" AutoEventWireup="true" CodeBehind="ManagerProjects.aspx.cs" Inherits="Human_Resource_Management.ManagerProjects" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="main-wrapper">
        <div class="page-wrapper">
            <div class="content container-fluid">
                <div class="page-header">
                    <div class="row align-items-center">
                        <div class="col">
                            <h3 class="page-title">Projects</h3>
                            <ul class="breadcrumb">
                                <li class="breadcrumb-item"><a href="ManagerDashbord.aspx">Dashboard</a></li>
                                <li class="breadcrumb-item active">Lets Start....❤</li>
                            </ul>
                        </div>
                        <div class="col-auto float-end ms-auto">
                            <a href="ManagerAssignProjects.aspx" class="btn add-btn"><i class="fa-solid fa-plus"></i>Assign Project</a>
                        </div>
                    </div>
                </div>



                <!-- Page Tab -->
                <div class="page-menu">
                    <div class="row">
                        <div class="col-sm-12">
                            <ul class="nav nav-tabs nav-tabs-bottom">
                                <li class="nav-item">
                                    <a class="nav-link active" data-bs-toggle="tab" href="#tab_projectlists">Project List</a>
                                </li>
                                <li class="nav-item">
                                    <a class="nav-link" data-bs-toggle="tab" href="#tab_projectsdata">Project Data</a>
                                </li>
                            </ul>
                        </div>
                    </div>
                </div>
                <!-- /Page Tab -->


                <!-- Tab Content -->
                <div class="tab-content">
                    <!-- Pending Tab -->
                    <div class="tab-pane show active" id="tab_projectlists">
                        <div class="payroll-table card">
                            <div class="table-responsive">
                                <table class="table table-hover table-radius">
                                    <thead>
                                        <tr>
                                            <th>Id</th>
                                            <th>Project</th>
                                            <th>Assigned By</th>
                                            <th>AssignDate</th>
                                            <th>Assigned Dept</th>
                                            <th>EndDate</th>
                                            <th>Status</th>
                                            <th>Progress</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:PlaceHolder ID="ProjectsListBind" runat="server"></asp:PlaceHolder>

                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                    <!-- Pending Tab End-->

                    <!-- Approved Tab -->
                    <div class="tab-pane" id="tab_projectsdata">

                        <div class="payroll-table card">
                            <div class="table-responsive">
                                <table class="table table-hover table-radius">
                                    <thead>
                                        <tr>
                                            <th>Image</th>
                                            <th>Project</th>
                                            <th>AssignDate</th>
                                            <th>StartDate</th>
                                            <th>TargetDate</th>
                                            <th>Assigned By</th>
                                            <th>Assigned To</th>
                                            <th>Completed</th>
                                            <th>Remaining</th>
                                            <th>Status</th>
                                            <th>Progress</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:PlaceHolder ID="ProjectsDataBind" runat="server"></asp:PlaceHolder>

                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                    <!-- Approved Tab End-->
                </div>
                <!-- Tab Content End-->


                <!-- Edit ProjectsDataEmpBind Modal -->
                <div id="edit_todaywork" class="modal custom-modal fade" role="dialog">
                    <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h5 class="modal-title">Edit Project Details</h5>
                                <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                            </div>
                            <div class="modal-body">
                                <asp:HiddenField ID="HiddenField1" runat="server" />
                                <asp:HiddenField ID="HiddenField3" runat="server" />
                                <div class="row">
                                    <div class="input-block mb-3 col-sm-6">
                                        <label class="col-form-label">Project Name <span class="text-danger">*</span></label>
                                        <asp:TextBox ID="txtprojectname" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                    </div>
                                    <div class="input-block mb-3 col-sm-6">
                                        <label class="col-form-label">Assign Date <span class="text-danger">*</span></label>
                                        <asp:TextBox ID="txtassigndate" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="input-block mb-3 col-sm-6">
                                        <label class="col-form-label">Start Date <span class="text-danger">*</span></label>
                                        <asp:TextBox ID="txtstartdate" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                    </div>
                                    <div class="input-block mb-3 col-sm-6">
                                        <label class="col-form-label">End Date <span class="text-danger">*</span></label>
                                        <asp:TextBox ID="txtenddate" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                    </div>

                                </div>
                                <div class="row">
                                    <div class="input-block mb-3 col-sm-6">
                                        <label class="col-form-label">Assign By <span class="text-danger">*</span></label>
                                        <asp:TextBox ID="txtassignby" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="input-block mb-3 col-sm-6">
                                        <label class="col-form-label">Assign To <span class="text-danger">*</span></label>
                                        <asp:TextBox ID="txtassignto" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="input-block mb-3 col-sm-6">
                                        <label class="col-form-label">Completed Tasks<span class="text-danger">*</span></label>
                                        <asp:TextBox ID="txtcompleted" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="input-block mb-3 col-sm-6">
                                        <label class="col-form-label">Remaining Tasks<span class="text-danger">*</span></label>
                                        <asp:TextBox ID="txtremaining" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="input-block mb-3 col-sm-6">
                                        <label class="col-form-label">Completed Days<span class="text-danger">*</span></label>
                                        <asp:TextBox ID="txtcompleteddays" runat="server" CssClass="form-control" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                    </div>
                                    <div class="input-block mb-3 col-sm-6">
                                        <label class="col-form-label">Remaining Days<span class="text-danger">*</span></label>
                                        <asp:TextBox ID="txtremainingdays" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="input-block mb-3 col-sm-6">
                                        <label class="col-form-label">Extention Date<span class="text-danger">*</span></label>
                                        <asp:TextBox ID="txtextentiondate" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                    </div>
                                    <div class="input-block mb-3 col-sm-6">
                                        <label class="col-form-label">Project Progress <span class="text-danger">*</span></label>
                                        <asp:TextBox ID="txtproggress" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="input-block mb-3 col-sm-6">
                                        <label class="col-form-label">Status<span class="text-danger">*</span></label>
                                        <asp:TextBox ID="txtstatus" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="input-block mb-3 col-sm-6">
                                        <label class="col-form-label">Project Description <span class="text-danger">*</span></label>
                                        <asp:TextBox ID="txtdescription" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="submit-section">
                                    <asp:Button ID="btnprojectupdate" runat="server" Text="Update" CssClass="btn btn-primary submit-btn" OnClick="btnprojectupdate_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- /Edit ProjectsDataEmpBind Modal -->



                <!-- Edit ProjectsList Modal -->
                <div id="edit_listwork" class="modal custom-modal fade" role="dialog">
                    <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h5 class="modal-title">Edit Project Details</h5>
                                <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                            </div>
                            <div class="modal-body">
                                <asp:HiddenField ID="HiddenField2" runat="server" />
                                <div class="row">
                                    <div class="input-block mb-3 col-sm-6">
                                        <label class="col-form-label">Project Name <span class="text-danger">*</span></label>
                                        <asp:TextBox ID="txtlistprojectname" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                    </div>
                                    <div class="input-block mb-3 col-sm-6">
                                        <label class="col-form-label">Assign<span class="text-danger">*</span></label>
                                        <asp:TextBox ID="txtlistassign" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="input-block mb-3 col-sm-6">
                                        <label class="col-form-label">Assign Date <span class="text-danger">*</span></label>
                                        <asp:TextBox ID="txtlistassigndate" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                    </div>
                                    <div class="input-block mb-3 col-sm-6">
                                        <label class="col-form-label">End Date <span class="text-danger">*</span></label>
                                        <asp:TextBox ID="txtlistenddate" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                    </div>

                                </div>
                                <div class="row">
                                    <div class="input-block mb-3 col-sm-6">
                                        <label class="col-form-label">Priority<span class="text-danger">*</span></label>
                                        <asp:TextBox ID="txtlistpriority" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="input-block mb-3 col-sm-6">
                                        <label class="col-form-label">Total Tasks<span class="text-danger">*</span></label>
                                        <asp:TextBox ID="txtlisttotaltasks" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="input-block mb-3 col-sm-6">
                                        <label class="col-form-label">Completed Tasks<span class="text-danger">*</span></label>
                                        <asp:TextBox ID="txtlistcompletedtasks" runat="server" CssClass="form-control" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                    </div>
                                    <div class="input-block mb-3 col-sm-6">
                                        <label class="col-form-label">Description<span class="text-danger">*</span></label>
                                        <asp:TextBox ID="txtlistdescription" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="submit-section">
                                    <asp:Button ID="btnprojectlistupdate" runat="server" Text="Submit." CssClass="btn btn-primary submit-btn" OnClick="btnprojectlistupdate_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- /Edit ProjectsList Modal -->

            </div>
        </div>
    </div>
    <script type="text/javascript">
        function editWork(Id, PrName, AssignDate, StartDate, EndDate, Assign, Name, Status, Description, Completed, RemainingTasks, ExtentionDate, Completedays, RemainingDays, Progress, EmpId) {
            $('#<%= HiddenField1.ClientID %>').val(Id);
            console.log("Id is : " + Id);
            $('#<%= txtprojectname.ClientID %>').val(PrName);
            $('#<%= txtassigndate.ClientID %>').val(AssignDate);
            $('#<%= txtstartdate.ClientID %>').val(StartDate);
            $('#<%= txtenddate.ClientID %>').val(EndDate);
            $('#<%= txtassignby.ClientID %>').val(Assign);
            $('#<%= txtassignto.ClientID %>').val(Name);
            $('#<%= txtstatus.ClientID %>').val(Status);
            $('#<%= txtcompleted.ClientID %>').val(Completed);
            $('#<%= txtremaining.ClientID %>').val(RemainingTasks);
            $('#<%= txtdescription.ClientID %>').val(Description);
            $('#<%= txtextentiondate.ClientID %>').val(ExtentionDate);
            $('#<%= txtcompleteddays.ClientID %>').val(Completedays);
            $('#<%= txtremainingdays.ClientID %>').val(RemainingDays);
            $('#<%= txtproggress.ClientID %>').val(Progress + '%');
            $('#<%= HiddenField3.ClientID %>').val(EmpId);


            $('#edit_todaywork').modal('show');
        }

        function editProjectList(Id, PrName, Assign, AssignDate, EndDate, Priority, TotalTasks, CompletedTasks, Description) {
            $('#<%= HiddenField2.ClientID %>').val(Id);
            $('#<%= txtlistprojectname.ClientID %>').val(PrName);
            $('#<%= txtlistassign.ClientID %>').val(Assign);
            $('#<%= txtlistassigndate.ClientID %>').val(AssignDate);
            $('#<%= txtlistenddate.ClientID %>').val(EndDate);
            $('#<%= txtlistpriority.ClientID %>').val(Priority);
            $('#<%= txtlisttotaltasks.ClientID %>').val(TotalTasks);
            $('#<%= txtlistcompletedtasks.ClientID %>').val(CompletedTasks);
            $('#<%= txtlistdescription.ClientID %>').val(Description);
        }


       
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }
    </script>


    <style type="text/css">
        .circular-progress {
            position: relative;
            width: 40px;
            height: 40px;
            border-radius: 50%;
            background: conic-gradient( #4d9165 calc(var(--value) * 1%), #e6e6e6 calc(var(--value) * 1%) );
            display: inline-block;
            border: 1px solid #7c8c5f;
        }

            .circular-progress::before {
                content: attr(data-progress) '%';
                position: absolute;
                top: 50%;
                left: 50%;
                transform: translate(-50%, -50%);
                font-size: 12px;
                color: #333;
            }
    </style>
</asp:Content>
