<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="AdminHolidays.aspx.cs" Inherits="Human_Resource_Management.AdminHolidays" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .upcoming-holiday {
            background-color: red;
        }

        .nodrp {
            cursor: not-allowed;
            outline: none;
        }
    </style>
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
                            <h3 class="page-title">Holidays
                                <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label></h3>
                            <ul class="breadcrumb">
                                <li class="breadcrumb-item"><a href="Admindashboard.aspx">Dashboard</a></li>
                                <li class="breadcrumb-item active">Holidays</li>
                            </ul>
                        </div>
                        <div class="col-auto float-end ms-auto">
                            <a href="#" class="btn add-btn" data-bs-toggle="modal" data-bs-target="#add_holiday"><i class="fa-solid fa-plus"></i>Add Holiday</a>
                        </div>
                    </div>
                </div>
                <!-- /Page Header -->
                <div class="row">
                    <div class="col-md-12">
                        <div class="table-responsive">
                            <table class="table table-striped custom-table mb-0">
                                <thead>
                                    <tr>
                                        <th>Id</th>
                                        <th>Title </th>
                                        <th>Holiday Date</th>
                                        <th>Day</th>
                                        <th>Holiday Type</th>
                                        <th>Holiday Name</th>
                                        <th>Edit</th>
                                        <th>Delete</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:PlaceHolder ID="HolidayContainer" runat="server"></asp:PlaceHolder>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
            <!-- /Page Content -->

            <!-- Add Holiday Modal -->
            <div class="modal custom-modal fade" id="add_holiday" role="dialog">
                <div class="modal-dialog modal-dialog-centered" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title">Add Holiday</h5>
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="modal-body">
                            <div class="input-block mb-3">
                                <label class="col-form-label">Holiday Name <span class="text-danger">*</span></label>
                                <asp:TextBox ID="txtHN" runat="server" CssClass="form-control" onkeypress="return isCharacterKey(event)" AutoCompleteType="Disabled"></asp:TextBox>
                                <asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator4"
                                    runat="server"
                                    ControlToValidate="txtHN"
                                    ErrorMessage="Holiday Name  is required."
                                    CssClass="text-danger"
                                    ValidationGroup="vgForm" />
                            </div>
                            <div class="input-block mb-3">
                                <label class="col-form-label">Holiday Date <span class="text-danger">*</span></label>
                                <asp:TextBox ID="txtHD" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                <asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator3"
                                    runat="server"
                                    ControlToValidate="txtHD"
                                    ErrorMessage="Date is required."
                                    CssClass="text-danger"
                                    ValidationGroup="vgForm" />
                            </div>
                            <div class="input-block mb-3">
                                <label class="col-form-label">Week <span class="text-danger">*</span></label>
                                <asp:TextBox ID="txtHW" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                            </div>
                            <div class="input-block mb-3">
                                <label class="col-form-label">HolidayType <span class="text-danger">*</span></label>
                                <asp:DropDownList ID="ddlHT" runat="server" onfocus="hidefirst()" CssClass="form-control">
                                    <asp:ListItem Value="">-- Select Type --</asp:ListItem>
                                    <asp:ListItem>Comman Holiday</asp:ListItem>
                                    <asp:ListItem>National Holiday</asp:ListItem>
                                    <asp:ListItem>Local Holiday</asp:ListItem>
                                    <asp:ListItem>Sudden Holiday</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator
                                    ID="rfvEmpName"
                                    runat="server"
                                    ControlToValidate="ddlHT"
                                    InitialValue=""
                                    ErrorMessage="Please select Holiday Type."
                                    CssClass="text-danger"
                                    ValidationGroup="vgForm" />
                            </div>
                            <div class="input-block mb-3">
                                <label class="col-form-label">Description<span class="text-danger">*</span></label>
                                <asp:TextBox ID="txtHDes" runat="server" CssClass="form-control" TextMode="MultiLine" onkeypress="return isCharacterKey(event)" AutoCompleteType="Disabled"></asp:TextBox>
                                <asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator13"
                                    runat="server"
                                    ControlToValidate="txtHDes"
                                    ErrorMessage="Description is required."
                                    CssClass="text-danger"
                                    ValidationGroup="vgForm" />

                                <asp:RegularExpressionValidator
                                    ID="RegularExpressionValidator3"
                                    runat="server"
                                    ControlToValidate="txtHDes"
                                    ValidationExpression="^.{10,}$"
                                    ErrorMessage=" at least 10 characters ."
                                    CssClass="text-danger"
                                    ValidationGroup="vgForm" />
                            </div>
                            <div class="submit-section">
                                <asp:Button ID="holidataddbtn" runat="server" CssClass="btn btn-primary submit-btn" Text="Submit" OnClick="holidataddbtn_Click" ValidationGroup="vgForm" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- /Add Holiday Modal -->

            <!-- Edit Holiday Modal -->
            <div class="modal custom-modal fade" id="edit_holiday" role="dialog">
                <div class="modal-dialog modal-dialog-centered" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title">Edit Holiday</h5>
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="modal-body">
                            <div class="input-block mb-3">
                                <label class="col-form-label">Holiday Name <span class="text-danger">*</span></label>
                                <input class="form-control" value="New Year" type="text">
                            </div>
                            <div class="input-block mb-3">
                                <label class="col-form-label">Holiday Date <span class="text-danger">*</span></label>
                                <div class="cal-icon">
                                    <input class="form-control datetimepicker" value="01-01-2019" type="text">
                                </div>
                            </div>
                            <div class="submit-section">
                                <button class="btn btn-primary submit-btn">Save</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- /Edit Holiday Modal -->

            <!-- Delete Holiday Modal -->
            <div class="modal custom-modal fade" id="delete_holiday" role="dialog">
                <div class="modal-dialog modal-dialog-centered">
                    <div class="modal-content">
                        <div class="modal-body">
                            <div class="form-header">
                                <h3>Delete Holiday</h3>
                                <p>
                                    Are you sure want to delete
                                    <asp:Label ID="Label2" runat="server" ForeColor="#cc0000"></asp:Label>
                                    ?
                                </p>
                                <asp:HiddenField ID="HiddenField1" runat="server" />
                            </div>
                            <div class="modal-btn delete-action">
                                <div class="row">
                                    <div class="col-6">

                                        <asp:Button ID="btndeleteholiday" runat="server" Text="Delete" CssClass="btn btn-primary continue-btn" OnClick="btndeleteholiday_Click" />
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
            <!-- /Delete Holiday Modal -->
            <!-- update Holiday panel -->
            <div class="modal custom-modal fade" id="editt_holiday" role="dialog">
                <div class="modal-dialog modal-dialog-centered" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title">Edit Holiday</h5>
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close">

                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="modal-body">
                            <div class="input-block mb-3">
                                <label class="col-form-label">Holiday Id <span class="text-danger"></span></label>
                                <asp:TextBox ID="TextBox3" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="input-block mb-3">
                                <label class="col-form-label">Holiday Name <span class="text-danger">*</span></label>
                                <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control" onkeypress="return isCharacterKey(event)" AutoCompleteType="Disabled"></asp:TextBox>
                            </div>
                            <div class="input-block mb-3">
                                <label class="col-form-label">Holiday Date <span class="text-danger">*</span></label>
                                <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                            </div>
                            <div class="input-block mb-3">
                                <label class="col-form-label">HoliDay Type <span class="text-danger">*</span></label>
                                <asp:TextBox ID="TextBox4" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="input-block mb-3">
                                <label class="col-form-label">Description <span class="text-danger">*</span></label>
                                <asp:TextBox ID="TextBox5" runat="server" CssClass="form-control" TextMode="MultiLine" onkeypress="return isCharacterKey(event)" AutoCompleteType="Disabled"></asp:TextBox>
                            </div>
                            <div class="submit-section">
                                <asp:Button ID="btnupdateholiday" runat="server" Text="Update" class="btn btn-primary submit-btn" OnClick="btnupdateholiday_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <!-- update Holiday panel -->
        </div>
        <!-- /Page Wrapper -->
    </div>
    <!-- /Main Wrapper -->

    <script type="text/javascript">
        function openEditModal(holidayId) {
            // Get the modal element using its ID
            var modal = document.getElementById('edit-btn-' + holidayId);

            // Open the modal
            modal.classList.add('show');
            modal.style.display = 'block';
        }


        function editHoliday(Hyid, HyName, HyDate, HyType, desc) {

            $('#<%= TextBox1.ClientID %>').val(HyName);
            $('#<%= TextBox2.ClientID %>').val(HyDate);
            $('#<%= TextBox3.ClientID %>').val(Hyid).prop('readonly', true);
            console.log("dfghj is : " + Hyid);

            $('#<%= TextBox4.ClientID %>').val(HyType);
            $('#<%= TextBox5.ClientID %>').val(desc);

            $('#editt_holiday').modal('show');
        }

        function deleteemp(Hyid, HyName) {
            document.getElementById('<%= Label2.ClientID %>').textContent = HyName;
            $('#<%= HiddenField1.ClientID %>').val(Hyid);
        }




       <%-- function updateHoliday() {
            var HyName = document.getElementById('TextBox1').value;
            var HyDate = document.getElementById('TextBox2').value;
            var Hyid = document.getElementById('lblempidd').value;
            var HyType = document.getElementById('TextBox4').value;
            var desc = document.getElementById('TextBox5').value;
            var rowId = $('#<%= lblempidd.ClientID %>').data('rowId'); // Retrieve row ID from data attribute
 
            $.ajax({
                type: 'POST',
                url: 'UpdateHolidayHandler.ashx',
                data: { Hyid: rowId, HyName: HyName, HyDate: HyDate, HyType: HyType, desc: desc }, // Pass row ID to the server-side handler
                success: function (response) {
                    alert(response);
                    $('#editt_holiday').modal('hide');
                },
                error: function (xhr, status, error) {
                    alert('An error occurred while updating the holiday.');
                }
            });
		}--%>



        function hideholidayselect() {
            var select = document.getElementById('<%= ddlHT.ClientID %>');
            if (select && select.options.length > 0) {
                select.options[0].style.display = 'none';
            }
        }
        function hidefirst() {
            var ddl = document.getElementById('<%= ddlHT.ClientID %>');
            ddl.options[0].disabled = true;
        }
        // holiday week display day 
        var vv = document.getElementById('<%= txtHD.ClientID %>');
        vv.addEventListener('change', () => {
            var holidayDateValue = new Date(vv.value);
            var dayNames = ['Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday'];
            var dayNumber = holidayDateValue.getDay();
            var dayName = dayNames[dayNumber];
            console.log(dayName);

            var dd = document.getElementById('<%= txtHW.ClientID %>');
            dd.value = dayName; // Bind dayName value to txtHW TextBox
        });

       
    function isCharacterKey(event) {
        var charCode = (event.which) ? event.which : event.keyCode;
        // Allow: A-Z, a-z, and space (charCode 32 is space)
        if ((charCode >= 65 && charCode <= 90) || (charCode >= 97 && charCode <= 122) || charCode == 32) {
            console.log("Valid input, charCode: " + charCode);
            return true;
        } else {
            console.log("Invalid input, charCode: " + charCode);
            return false;
        }
    }
    </script>
    <style type="text/css">
        #edit-btn- {
            color: red;
            border: none;
        }

        #TextBox3 {
            pointer-events: none;
            cursor: no-drop;
        }
    </style>
</asp:Content>
