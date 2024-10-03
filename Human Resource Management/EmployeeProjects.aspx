<%@ Page Title="" Language="C#" MasterPageFile="~/Employee.Master" AutoEventWireup="true" CodeBehind="EmployeeProjects.aspx.cs" Inherits="Human_Resource_Management.EmployeeProjects" %>

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
                    </div>
                </div>
                <div class="row" id="ProjectsData" runat="server">
                    <div class="col-md-12">
                        <div class="table-responsive">
                            <table class="table table-striped custom-table mb-0 datatable">
                                <thead>
                                    <tr>                                        
                                        <th>Project</th>
                                        <th>AssignDate</th>
                                        <th>StartDate</th>
                                        <th>EndDate</th>
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
                                        <asp:TextBox ID="txtcompleted" runat="server" CssClass="form-control" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                    </div>
                                    <div class="input-block mb-3 col-sm-6">
                                        <label class="col-form-label">Remaining Tasks<span class="text-danger">*</span></label>
                                        <asp:TextBox ID="txtremaining" runat="server" CssClass="form-control" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="input-block mb-3 col-sm-6">
                                        <label class="col-form-label">Total Days<span class="text-danger">*</span></label>
                                        <asp:TextBox ID="txttotaldays" runat="server" CssClass="form-control" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                    </div>
                                    <div class="input-block mb-3 col-sm-6">
                                        <label class="col-form-label">Extention Date<span class="text-danger">*</span></label>
                                        <asp:TextBox ID="txtextentiondate" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
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
            </div>
        </div>
    </div>

    <script type="text/javascript">
        function editWork(Id, PrName, AssignDate, StartDate, EndDate, Assign, Name, Status, Description, Completed, RemainingTasks, ExtentionDate, NumberOfDays) {
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
            $('#<%= txtextentiondate.ClientID %>').val(ExtentionDate);
            $('#<%= txttotaldays.ClientID %>').val(NumberOfDays);

            $('#edit_todaywork').modal('show');
        }


        function isNumberKey(event) {
            const charCode = (event.which) ? event.which : event.keyCode;
            // Allow only numbers (0-9) and control characters (e.g., backspace)
            if (charCode >= 48 && charCode <= 57 || charCode === 8) {
                return true;
            }
            return false;
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
             border:1px solid #7c8c5f;
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
