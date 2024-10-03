<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="AdminProjects.aspx.cs" Inherits="Human_Resource_Management.AdminTimeSheet" %>

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
                            <h3 class="page-title">Projects</h3>
                            <ul class="breadcrumb">
                                <li class="breadcrumb-item"><a href="Admindashboard.aspx">Dashboard</a></li>
                                <li class="breadcrumb-item active">Projects</li>
                            </ul>
                        </div>
                        <div class="col-auto float-end ms-auto">
                            <%-- <a href="#" class="btn add-btn" data-bs-toggle="modal" data-bs-target="#add_todaywork"><i class="fa-solid fa-plus"></i></a>--%>
                            <a href="AdminAssignProjects.aspx" class="btn add-btn"><i class="fa-solid fa-plus"></i>Assign Project</a>
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
                                    <a class="nav-link" data-bs-toggle="tab" href="#tab_projectshistory">Projects History</a>
                                </li>

                            </ul>
                        </div>
                    </div>
                </div>
                <!-- /Page Tab -->


                <!-- Tab Content -->
                <div class="tab-content">
                    <!-- List Tab -->
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
                    <!-- List Tab End-->

                    <!-- History Tab -->

                    <div class="tab-pane" id="tab_projectshistory">
                        <asp:PlaceHolder ID="ProjectsDataBind" runat="server"></asp:PlaceHolder>

                    </div>
                    <!-- History Tab End-->
                </div>



            </div>
            <!-- /Page Content -->

            <!-- Assign Project Modal -->
            <div id="add_todaywork" class="modal custom-modal fade" role="dialog">
                <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title">Add Today Work details</h5>
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="input-block mb-3 col-sm-6">
                                    <label class="col-form-label">Project Name <span class="text-danger">*</span></label>
                                    <asp:TextBox ID="TextBox8" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="input-block mb-3 col-sm-6">
                                    <label class="col-form-label">Description <span class="text-danger">*</span></label>
                                    <asp:TextBox ID="TextBox9" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="input-block mb-3 col-sm-6">
                                    <label class="col-form-label">Target Date <span class="text-danger">*</span></label>
                                    <asp:TextBox ID="TextBox10" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                </div>
                                <div class="input-block mb-3 col-sm-6">
                                    <label class="col-form-label">Assign Department  <span class="text-danger">*</span></label>
                                    <asp:DropDownList ID="ddlbranch" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="row">
                                <div class="input-block mb-3 col-sm-6">
                                    <label class="col-form-label">Target Date <span class="text-danger">*</span></label>
                                    <asp:TextBox ID="TextBox11" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                </div>
                                <div class="input-block mb-3 col-sm-6">
                                    <label class="col-form-label">Assign Department  <span class="text-danger">*</span></label>
                                    <asp:DropDownList ID="ddlassigndepartment" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="submit-section">
                                <asp:Button ID="btnassignproject" runat="server" Text="Assign" CssClass="btn btn-primary submit-btn" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- /Add Today Work Modal -->

            <!-- Edit ProjectsListDataBind Modal -->
            <div id="edit_listwork" class="modal custom-modal fade" role="dialog">
                <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title">project Details</h5>
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="modal-body">
                            <asp:HiddenField ID="HiddenField2" runat="server" />
                            <div class="row">
                                <div class="input-block mb-3 col-sm-6">
                                    <label class="col-form-label">Project Name <span class="text-danger">*</span></label>
                                    <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="input-block mb-3 col-sm-6">
                                    <label class="col-form-label">Project Code <span class="text-danger">*</span></label>
                                    <asp:TextBox ID="TextBox12" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>

                            </div>
                            <div class="row">
                                <div class="input-block mb-3 col-sm-6">
                                    <label class="col-form-label">Assign <span class="text-danger">*</span></label>
                                    <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="input-block mb-3 col-sm-6">
                                    <label class="col-form-label">AssignDept<span class="text-danger">*</span></label>
                                    <asp:TextBox ID="TextBox4" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">

                                <div class="input-block mb-3 col-sm-6">
                                    <label class="col-form-label">AssignDate <span class="text-danger">*</span></label>
                                    <asp:TextBox ID="TextBox5" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                </div>
                                <div class="input-block mb-3 col-sm-6">
                                    <label class="col-form-label">End Date <span class="text-danger">*</span></label>
                                    <asp:TextBox ID="TextBox3" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                </div>

                            </div>
                            <div class="row">
                                <div class="input-block mb-3 col-sm-6">
                                    <label class="col-form-label">Status <span class="text-danger">*</span></label>
                                    <asp:TextBox ID="TextBox6" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="input-block mb-3 col-sm-6">
                                    <label class="col-form-label">Description<span class="text-danger">*</span></label>
                                    <asp:TextBox ID="TextBox7" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                </div>
                            </div>
                            <div class="submit-section">
                                <asp:Button ID="btnupdatelist" runat="server" Text="Update" CssClass="btn btn-primary submit-btn" OnClick="btnupdatelist_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- /Edit ProjectsListDataBind Modal -->


            <!-- Edit ProjectsDataEmpBind Modal -->
            <div id="edit_todaywork" class="modal custom-modal fade" role="dialog">
                <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title">Edit Work Details</h5>
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="modal-body">
                            <asp:HiddenField ID="HiddenField1" runat="server" />
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
                                <div class="input-block mb-3 col-sm-4">
                                    <label class="col-form-label">Start Date <span class="text-danger">*</span></label>
                                    <asp:TextBox ID="txtstartdate" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                </div>
                                <div class="input-block mb-3 col-sm-4">
                                    <label class="col-form-label">End Date <span class="text-danger">*</span></label>
                                    <asp:TextBox ID="txtenddate" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                </div>

                            </div>
                            <div class="row">
                                <div class="input-block mb-3 col-sm-4">
                                    <label class="col-form-label">Assign By <span class="text-danger">*</span></label>
                                    <asp:TextBox ID="txtassignby" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="input-block mb-3 col-sm-6">
                                    <label class="col-form-label">Assign To <span class="text-danger">*</span></label>
                                    <asp:TextBox ID="txtassignto" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="input-block mb-3 col-sm-4">
                                    <label class="col-form-label">Status<span class="text-danger">*</span></label>
                                    <asp:TextBox ID="txtstatus" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="input-block mb-3 col-sm-6">
                                    <label class="col-form-label">Completed Tasks<span class="text-danger">*</span></label>
                                    <asp:TextBox ID="txtcompleted" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="input-block mb-3">
                                <div class="input-block mb-3 col-sm-4">
                                    <label class="col-form-label">Remaining Tasks<span class="text-danger">*</span></label>
                                    <asp:TextBox ID="txtremaining" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="input-block mb-3 col-sm-6">
                                    <label class="col-form-label">Project Description <span class="text-danger">*</span></label>
                                    <asp:TextBox ID="txtdescription" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                </div>
                            </div>
                            <div class="submit-section">
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- /Edit ProjectsDataEmpBind Modal -->



            <!-- Delete Today Work Modal -->
            <div class="modal custom-modal fade" id="delete_workdetail" role="dialog">
                <div class="modal-dialog modal-dialog-centered">
                    <div class="modal-content">
                        <div class="modal-body">
                            <div class="form-header">
                                <h3>Delete Work Details</h3>
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
            <!-- Delete Today Work Modal -->

        </div>
        <!-- /Page Wrapper -->
    </div>
    <!-- /Main Wrapper -->

    <script type="text/javascript">
        function editProjectList(Id, PrName, Assign, EndDate, AssignDept, AssignDate, Status, Description, ProjectCode) {
            $('#<%= HiddenField2.ClientID %>').val(Id);
            $('#<%= TextBox1.ClientID %>').val(PrName);
            $('#<%= TextBox2.ClientID %>').val(Assign);
            $('#<%= TextBox3.ClientID %>').val(EndDate);
            $('#<%= TextBox4.ClientID %>').val(AssignDept);
            $('#<%= TextBox5.ClientID %>').val(AssignDate);
            $('#<%= TextBox6.ClientID %>').val(Status);
            $('#<%= TextBox7.ClientID %>').val(Description);
            $('#<%= TextBox12.ClientID %>').val(ProjectCode);
            console.log("PrName is : " + PrName)
        }
        function editWork(Id, PrName, AssignDate, StartDate, EndDate, Assign, Name, Status, Description, Completed, RemainingTasks) {
            $('#<%= HiddenField1.ClientID %>').val(Id);
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


            $('#edit_todaywork').modal('show');
        }
    </script>


    <!-- Include this script at the end of your HTML document, just before the closing </body> tag -->

    <%--<script type="text/javascript">
        console.log("Dropdown start");
        document.addEventListener("DOMContentLoaded", function () {
            // Wait for the DOM to fully load

            // Find the dropdown list element by its client-side ID
            var ddlProjectList = document.getElementById("<%= ddlProjectList.ClientID %>");

            // Check if the dropdown list element exists
            if (ddlProjectList) {
                // If the dropdown list element exists, attach a change event listener to it
                ddlProjectList.addEventListener("change", function (event) {
                    // Prevent the default behavior of the change event
                    event.preventDefault();

                    // Log a message to the console when the dropdown list value changes
                    console.log("Start list value changed");

                    // Optionally, you can perform additional actions here
                });
            } else {
                // Log an error message to the console if the dropdown list element is not found
                console.error("Dropdown list element not found");
            }
        });
    </script>--%>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap/5.1.3/js/bootstrap.bundle.min.js"></script>
    <script type="text/javascript">
        function showModal() {
            var myModal = new bootstrap.Modal(document.getElementById('edit_todaywork'),
                {
                    backdrop: 'static',
                    keyboard: false
                });
            myModal.show();
        }

        // Set hidden field value before postback
        function setModalState() {
            document.getElementById('<%= HiddenField1.ClientID %>').value = "1";
        }

        document.addEventListener('DOMContentLoaded', function () {
            <%--var ddlLeavesStatus = document.getElementById('<%= ddlProjectList.ClientID %>');
            ddlLeavesStatus.addEventListener('change', setModalState);--%>
        });

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
