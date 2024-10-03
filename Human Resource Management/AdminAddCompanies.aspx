<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="AdminAddCompanies.aspx.cs" Inherits="Human_Resource_Management.AdminAddCompanies" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css">
    <style type="text/css">
        @import url(https://fonts.googleapis.com/css?family=Lato:400,900,700,300);

        .heading4 {
            font-size: 18px;
            font-weight: 400;
            font-family: 'Lato', sans-serif;
            color: #111111;
            margin: 0px 0px 5px 0px;
        }

        .heading1 {
            font-size: 30px;
            line-height: 20px;
            font-family: 'Lato', sans-serif;
            text-transform: uppercase;
            color: #1b2834;
            font-weight: 900;
        }

        .content-quality {
            float: left;
            width: 193px;
        }

            .content-quality p {
                margin-left: 10px;
                font-family: 'Open Sans', sans-serif;
                font-size: 14px;
                font-weight: 600;
                line-height: 17px;
            }

                .content-quality p span {
                    display: block;
                }

        .tabtop li a {
            font-family: 'Lato', sans-serif;
            font-weight: 700;
            color: #1b2834;
            border-radius: 0px;
            margin-right: 22.008px;
            border: 1px solid #ebebeb !important;
        }

        .tabtop .active a:before {
            content: " ";
            position: absolute;
            top: 15px;
            left: 82px;
            color: green;
            font-size: 30px;
        }

        .tabtop li a:hover {
            color: #fe8359 !important;
            text-decoration: none;
        }

        .tabtop .active a:hover {
            color: #ffffff !important;
        }

        .tabtop .active a {
            background-color: #fe7b5f !important;
            color: #FFF !important;
        }

        .margin-tops {
            margin-top: 0px;
        }

        .tabtop li a:last-child {
            padding: 10px 22px;
        }

        .thbada {
            padding: 10px 28px !important;
        }

        section p {
            font-family: 'Lato', sans-serif;
        }

        .margin-tops4 {
            margin-top: 20px;
        }

        .services {
            background-color: #d4d4d4;
            min-height: 710px;
            padding: 65px 0 27px 0;
        }

            .services a:hover {
                color: #000;
            }

            .services h1 {
                margin-top: 0px !important;
            }

        .heading-container p {
            font-family: 'Lato', sans-serif;
            text-align: center;
            font-size: 16px !important;
            text-transform: uppercase;
        }

        .style1 {
            color: #FF0000;
        }

        .form-control1 {
            display: block;
            width: 100%;
            height: calc(1.5em + .75rem + 2px);
            padding: .375rem .75rem;
            font-size: 1rem;
            font-weight: 400;
            line-height: 1.5;
            color: #ffffff;
            background-color: #fc6174;
            background-clip: padding-box;
            border: 1px solid #ced4da;
            border-radius: .25rem;
            transition: border-color .15s ease-in-out, box-shadow .15s ease-in-out;
            margin-top: 23px;
        }
    </style>
    <%-- <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>--%>
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css">
    <%-- <script type="text/javascript">
        function showimagepreview(input) {
            if (input.files && input.files[0]) {
                var filerdr = new FileReader();
                filerdr.onload = function (e) {
                    $('#imgprvw').attr('src', e.target.result);
                }
                filerdr.readAsDataURL(input.files[0]);
            }
        }
    </script>--%>
    <%-- <script type="text/javascript">
        function validate() {
            var summary = "";
            summary += isvalidFirstname();
            summary += isvalidEmail();
            summary += isvalidphoneno();
            summary += isvalidADRESS();

            if (summary != "") {
                alert(summary);
                return false;
            }
            else {
                return true;
            }
        }
    </script>--%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-wrapper">
        <div class="content container-fluid">
            <!-- Page Header -->
            <div class="page-header">
                <div class="row align-items-center">
                    <div class="col">
                        <h3 class="page-title">Add Company Details</h3>
                        <ul class="breadcrumb">
                            <li class="breadcrumb-item"><a href="Admindashboard.aspx">Dashboard</a></li>
                            <li class="breadcrumb-item active">Add Company</li>
                        </ul>
                    </div>
                </div>
                <asp:Label ID="Label1" runat="server" Text="" ForeColor="#009900"></asp:Label>
            </div>

            <!-- /Page Header -->
            <div class="row mt">
                <div class="col-lg-12">
                    <div class="clearfix"></div>
                    <div class="tabbable-panel margin-tops4" style="margin-top: -10px;">
                        <asp:HiddenField ID="hfActiveTab" runat="server" />
                        <div class="tabbable-line">
                            <ul class="nav nav-tabs tabtop tabsetting">
                                <li id="tab1" class="nav-item active"><a class="nav-link active" href="#tab_default_1" data-toggle="tab">Add Company</a> </li>
                                <li id="tab2" class="nav-item"><a class="nav-link" href="#tab_default_2" data-toggle="tab">Add Branch</a> </li>
                                <li id="tab3" class="nav-item"><a class="nav-link" href="#tab_default_3" data-toggle="tab">Add Department</a> </li>
                                <li id="tab4" class="nav-item"><a class="nav-link" href="#tab_default_5" data-toggle="tab">Add Shift</a> </li>
                                <%--<li id="tab5" class="nav-item"><a class="nav-link" href="#tab_default_6" data-toggle="tab">Add Categories</a> </li>--%>
                            </ul>

                            <div class="tab-content margin-tops">
                                <!-- Add Zone Start -->
                                <div class="tab-pane fade show active" id="tab_default_1" style="margin-top: 10px; padding: 10px; box-shadow: 1px 2px 4px 1px #8c8c8c;">
                                    <!-- Add Zone Row 1 Start -->
                                    <div class="row">
                                        <div class="col-sm-4">
                                            Company Code 
                                            <asp:TextBox ID="TextBox2" runat="server" class="form-control" AutoCompleteType="Disabled" ClientIDMode="Static"></asp:TextBox>                                          
                                        </div>
                                        <div class="col-sm-4">
                                            Company Name 
                                            <asp:TextBox ID="TextBox6" runat="server" class="form-control" AutoCompleteType="Disabled" ClientIDMode="Static"></asp:TextBox>                                           
                                        </div>
                                        <div class="col-sm-4">
                                            REG Number
                                            <asp:TextBox ID="TextBox7" runat="server" class="form-control" AutoCompleteType="Disabled"></asp:TextBox>
                                        </div>
                                    </div>
                                    <!-- Add Zone Row 1 End -->
                                    <br />
                                    <!-- Add Zone Row 2 Start -->
                                    <div class="row">
                                        <div class="col-sm-4">
                                            Registration Date
                                            <asp:TextBox ID="TextBox8" runat="server" class="form-control" AutoCompleteType="Disabled" TextMode="Date"></asp:TextBox>
                                           
                                        </div>
                                        <div class="col-sm-4">
                                            TAN Number
                                            <asp:TextBox ID="TextBox9" runat="server" class="form-control" AutoCompleteType="Disabled"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-4">
                                            Pan Number 
                                            <asp:TextBox ID="TextBox10" runat="server" class="form-control" AutoCompleteType="Disabled"></asp:TextBox>
                                           
                                        </div>
                                    </div>
                                    <!-- Add Zone Row 2 End -->
                                    <br />
                                    <!-- Add Zone Row 3 Start -->
                                    <div class="row">
                                        <div class="col-sm-4">
                                            TIN Number
                                            <asp:TextBox ID="TextBox11" runat="server" class="form-control" AutoCompleteType="Disabled"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-4">
                                            Branch Incharge Name
                                            <asp:TextBox ID="TextBox12" runat="server" class="form-control" AutoCompleteType="Disabled"></asp:TextBox>
                                          
                                        </div>
                                        <div class="col-sm-4">
                                            Designation
                                            <asp:TextBox ID="TextBox13" runat="server" class="form-control" AutoCompleteType="Disabled"></asp:TextBox>
                                        </div>
                                    </div>
                                    <!-- Add Zone Row 3 End -->
                                    <br />
                                    <!-- Add Zone Row 4 Start -->
                                    <div class="row">
                                        <div class="col-sm-4">
                                            Email
                                            <asp:TextBox ID="TextBox15" runat="server" class="form-control"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-4">
                                            Address
                                            <asp:TextBox ID="TextBox14" runat="server" class="form-control"></asp:TextBox>
                                          
                                        </div>

                                        <div class="col-sm-4">
                                            Land Line Phone Number
                                            <asp:TextBox ID="TextBox16" runat="server" class="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <!-- Add Zone Row 4 End -->
                                    <br />
                                    <!-- Add Zone Row 5 Start -->
                                    <div class="row">
                                        <div class="col-sm-4">
                                            Secondary Phone Number
                                            <asp:TextBox ID="TextBox17" runat="server" class="form-control"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-4">
                                            Mobile Number
                                            <asp:TextBox ID="TextBox18" runat="server" class="form-control"></asp:TextBox>
                                           
                                                   </div>
                                        <div class="col-sm-4">
                                            FAX Number
                                            <asp:TextBox ID="TextBox19" runat="server" class="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <!-- Add Zone Row 5 End -->
                                    <br />
                                    <!-- Add Zone Row 6 Start -->
                                    <div class="row">
                                        <div class="col-sm-4">
                                            Website URL
                                            <asp:TextBox ID="TextBox20" runat="server" class="form-control"></asp:TextBox>
                                           
                                                </div>
                                        <div class="col-sm-4">
                                            GST Number
                                            <asp:TextBox ID="TextBox21" runat="server" class="form-control"></asp:TextBox>
                                           
                                        </div>
                                        <br />
                                        <br />
                                        <div class="col-sm-4">
                                            <asp:Button ID="addcompanybtn" runat="server" Text="Submit" CssClass="form-control1" OnClick="addcompanybtn_Click1" OnClientClick="return validateAndHighlight();" />
                                        </div>
                                    </div>
                                    <!-- Add Zone Row 6 End -->
                                </div>
                                <!-- Add Zone End -->

                                <!-- Add Branch Start -->
                                <div class="tab-pane fade" id="tab_default_2" style="margin-top: 10px; padding: 10px; box-shadow: 1px 1px 1px 1px #d9d6d6;">
                                    <div class="row">
                                        <div class="col-sm-4">
                                            Company Name: 
                                            
                                                     <asp:DropDownList ID="DropDownList5" runat="server" CssClass="form-control" onfocus="drp5disable()">
                                                         <asp:ListItem Enabled="true" Text="Select " Value="1"></asp:ListItem>
                                                     </asp:DropDownList>
                                             
                                             </div>
                                        <div class="col-sm-4">
                                            Branch Name
                                            <asp:TextBox ID="TextBox23" runat="server" class="form-control"></asp:TextBox>
                                           </div>
                                      
                                    </div>

                                    <div class="row">
                                        <div class="col-md-4">
                                            <asp:Button ID="addcompanybranchbtn" runat="server" Text="Submit" CssClass="form-control1" OnClick="addcompanybranchbtn_Click" />
                                        </div>
                                    </div>
                                </div>
                                <!-- Add Branch End -->

                                <!-- Department Start -->
                                <div class="tab-pane fade" id="tab_default_3" style="margin-top: 10px; padding: 10px; box-shadow: 1px 1px 1px 1px #d9d6d6;">
                                    <!-- Department Row 1 Start -->
                                    <div class="row">
                                        <!-- RBS Zone Name Start -->
                                        <div class="col-sm-4">
                                            Company Name..:
                                             
                                                     <asp:DropDownList ID="ddldeptcompany" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddldeptcompany_SelectedIndexChanged" onfocus="ddldeptcompanydisable()">
                                                     </asp:DropDownList>
                                                
                                        </div>
                                        <!-- RBS Zone Name End -->

                                        <!-- Branch Name Start -->
                                        <div class="col-sm-4">
                                          
                                                    Branch Name:
                                            <asp:DropDownList ID="ddldeptbranch" runat="server" CssClass="form-control" onfocus="ddldeptbranchdisable()" onchange="handleCompanyChange()">
                                            </asp:DropDownList>
                                               
                                        </div>
                                        <!-- Branch Name End -->

                                        <!-- Department Start -->
                                        <div class="col-sm-4">
                                            Department
                                            <asp:TextBox ID="TextBox27" runat="server" CssClass="form-control"></asp:TextBox>
                                           </div>
                                        <!-- Department Name End -->
                                    </div>
                                    <!-- Department Row 1 End -->

                                    <!-- Department Row 2 Start -->
                                    <div class="row">

                                        <div class="col-sm-4">
                                            Short Name
                                                <asp:TextBox ID="TextBox3" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>

                                        <div class="col-sm-4">
                                            Description
                                            <asp:TextBox ID="TextBox4" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>

                                        <!-- Button Start -->
                                        <div class="col-sm-4">
                                            <asp:Button ID="btndepertment" runat="server" Text="Sumbit" CssClass="form-control1" OnClick="btndepertment_Click" />
                                        </div>
                                        <!-- Button End -->
                                    </div>
                                    <!-- Department Row 2 End -->
                                </div>
                                <!-- Department End -->

                                <!-- Shift Start -->
                                <div class="tab-pane fade" id="tab_default_5" style="margin-top: 10px; padding: 10px; box-shadow: 1px 1px 1px 1px #d9d6d6;">
                                    <!-- Shift Row 1 Start -->
                                    <div class="row">
                                        <!-- Shift Name Start -->
                                        <div class="col-md-4">
                                            Shift Name
                                            <asp:TextBox ID="TextBox34" runat="server" class="form-control"></asp:TextBox>
                                            </div>
                                        <!-- Shift Name End -->
                                        <!-- Shift Code Start -->
                                        <div class="col-md-4">
                                            Shift Code
                                            <asp:TextBox ID="TextBox35" runat="server" class="form-control"></asp:TextBox>
                                        </div>
                                        <!-- Shift Code End -->
                                        <!-- Start Time Start -->
                                        <div class="col-md-4">
                                            Start Time (Ex : 09:30 AM - 06:30 PM)
                                            <asp:TextBox ID="TextBox36" runat="server" class="form-control"></asp:TextBox>
                                          
                                        </div>
                                        <!-- Start Time End -->
                                    </div>
                                    <!-- Shift Row 1 Start -->
                                    <br />
                                    <!-- Shift Row 2 End -->
                                    <div class="row">
                                        <!-- End Time Start -->
                                        <div class="col-md-4">
                                            End Time
                                            <asp:TextBox ID="TextBox37" runat="server" class="form-control"></asp:TextBox>
                                        </div>
                                        <!-- End Time End -->

                                        <!-- Submit Start -->
                                        <div class="col-md-4">
                                            <asp:Button ID="btnaddshift" runat="server" CssClass="form-control1" Text="Submit" OnClick="btnaddshift_Click" />
                                        </div>
                                        <!-- Submit End -->
                                    </div>
                                    <!-- Shift Row 2 End -->
                                </div>
                                <!-- Shift End -->

                                <!-- Category Start -->
                                <div class="tab-pane fade" id="tab_default_6" style="margin-top: 10px; padding: 10px; box-shadow: 1px 2px 4px 1px #8c8c8c;">
                                    <!-- Category Row 1 start -->
                                    <div class="row">
                                        <div class="col-sm-4">
                                            Category
                                            <asp:TextBox ID="Category" runat="server" class="form-control"></asp:TextBox>
                                          </div>
                                        <div class="col-sm-4">
                                            Category Short Name
                                            <asp:TextBox ID="catShortname" runat="server" class="form-control"></asp:TextBox>
                                        
                                        </div>
                                        <div class="col-md-4">
                                            OT Formula
                                            <asp:DropDownList ID="OTformula" runat="server" class="form-control">
                                                <asp:ListItem>(Out Punch)-(Shift End Time)</asp:ListItem>
                                                <asp:ListItem>(Total Duration)+(Shift Hours)</asp:ListItem>
                                                <asp:ListItem>(Early Going)+(Late Coming)</asp:ListItem>
                                                <asp:ListItem>OT Not Applicable</asp:ListItem>
                                            </asp:DropDownList>
                                           
                                        </div>
                                    </div>
                                    <!-- Category Row 1 End -->
                                    <br />
                                    <!-- Category Row 2 start -->
                                    <div class="row">
                                        <div class="col-md-4">
                                            Min OT
                                            <asp:TextBox ID="Minot" runat="server" class="form-control"></asp:TextBox>
                                           
                                        </div>
                                        <div class="col-md-4">
                                            Grace Time for Late Come
                                            <asp:TextBox ID="GRT" runat="server" class="form-control"></asp:TextBox>
                                           
                                        </div>
                                        <div class="col-md-4">
                                            Grace Time for Early Go
                                            <asp:TextBox ID="NglLstpunch1" runat="server" class="form-control"></asp:TextBox>
                                           
                                        </div>
                                    </div>
                                    <!-- Category Row 2 End -->
                                    <br />
                                    <!-- Category Row 3 start -->
                                    <div class="row">
                                        <div class="col-md-2 padtop15">
                                            <asp:CheckBox ID="Weekoff1" runat="server" Text="Weakoff1" class="form-control" />
                                        </div>
                                        <div class="col-md-2">
                                            <asp:DropDownList ID="Weeloff11" runat="server" class="form-control">
                                                <asp:ListItem>Sunday</asp:ListItem>
                                                <asp:ListItem>Monday</asp:ListItem>
                                                <asp:ListItem>Tuesday</asp:ListItem>
                                                <asp:ListItem>Wednesday</asp:ListItem>
                                                <asp:ListItem>Thursday</asp:ListItem>
                                                <asp:ListItem>Friday</asp:ListItem>
                                                <asp:ListItem>Saturday</asp:ListItem>
                                            </asp:DropDownList>

                                        </div>
                                        <div class="col-md-2 padtop15">
                                            <asp:CheckBox ID="weekoff2" runat="server" Text="Weakoff2" class="form-control" />
                                        </div>
                                        <div class="col-md-2">
                                            <asp:DropDownList ID="Weekoff22" runat="server" class="form-control">
                                                <asp:ListItem>Sunday</asp:ListItem>
                                                <asp:ListItem>Monday</asp:ListItem>
                                                <asp:ListItem>Tuesday</asp:ListItem>
                                                <asp:ListItem>Wednesday</asp:ListItem>
                                                <asp:ListItem>Thursday</asp:ListItem>
                                                <asp:ListItem>Friday</asp:ListItem>
                                                <asp:ListItem>Saturday</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-2">
                                            Select a week  
                                            <asp:CheckBoxList ID="weekoff2list" runat="server" RepeatDirection="Horizontal" Height="16px" Width="274px" CssClass="form-control">
                                                <asp:ListItem style="padding: 5px">1st</asp:ListItem>
                                                <asp:ListItem style="padding: 5px">2nd</asp:ListItem>
                                                <asp:ListItem style="padding: 5px">3rd</asp:ListItem>
                                                <asp:ListItem style="padding: 5px">4th</asp:ListItem>
                                            </asp:CheckBoxList>
                                        </div>
                                    </div>
                                    <!-- Category Row 3 End -->
                                    <br />
                                    <!-- Category Row 4 start -->
                                    <div class="row">
                                        <div class="col-md-5">
                                            <asp:CheckBox ID="CHDW" Text="Calculate Half Day, if Work Duration is less than" runat="server" class="form-control" />
                                        </div>
                                        <div class="col-md-5">
                                            <asp:TextBox ID="CHWD1" runat="server" class="form-control"></asp:TextBox>
                                          
                                        </div>
                                        <div class="col-md-2 padtop15">
                                            Minutes
                                        </div>
                                    </div>
                                    <!-- Category Row 4 End -->
                                    <br />
                                    <!-- Category Row 5 start -->
                                    <div class="row">
                                        <div class="col-md-5">
                                            <asp:CheckBox ID="cawdil" runat="server" Text="Calculate Absent, if Work Duration is less than" class="form-control" />
                                        </div>
                                        <div class="col-md-5">
                                            <asp:TextBox ID="CAW" runat="server" class="form-control"></asp:TextBox>
                                           
                                        </div>
                                        <div class="col-md-2 padtop15">
                                            Minutes
                                        </div>
                                    </div>
                                    <!-- Category Row 5 End -->
                                    <br />
                                    <!-- Category Row 6 start -->
                                    <div class="row">
                                        <div class="col-md-5">
                                            <asp:CheckBox ID="MHDL" runat="server" Text="Mark Half Day, If Late By" class="form-control"></asp:CheckBox>
                                        </div>
                                        <div class="col-md-5">
                                            <asp:TextBox ID="MHDL1" runat="server" class="form-control"></asp:TextBox>
                                          
                                        </div>
                                        <div class="col-md-2 padtop15">
                                            Minutes
                                        </div>
                                    </div>
                                    <!-- Category Row 6 End -->
                                    <br />
                                    <!-- Category Row 7 start -->
                                    <div class="row">
                                        <div class="col-md-5">
                                            <asp:CheckBox ID="MHDEGB" runat="server" Text="Mark Half Day, If Early Going By" class="form-control" />
                                        </div>

                                        <div class="col-md-5">
                                            <asp:TextBox ID="MHDEGB1" runat="server" class="form-control"></asp:TextBox>                                         
                                        </div>

                                        <div class="col-md-2 padtop15">
                                            Minutes
                                        </div>
                                    </div>
                                    <!-- Category Row 7 End -->
                                    <br />
                                    <!-- Category Row 8 start -->
                                    <div class="row">
                                        <div class="col-md-offset-8 col-md-2" style="float: right">
                                            <asp:Button ID="btncatagory" runat="server" Text="Submit" CssClass="form-control1" OnClick="btncatagory_Click" />
                                        </div>
                                    </div>
                                    <!-- Category Row 8 End -->
                                </div>
                                <!-- Category End -->
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script>
        $(document).ready(function () {
            $(".nav.nav-tabs.tabtop.tabsetting").on("click", "a", function () {
                // Remove active class from all tabs
                $(".nav.nav-tabs.tabtop.tabsetting li").removeClass("active");
                // Add active class to the clicked tab
                $(this).closest('li').addClass('active');

                // Find the corresponding tab-pane and show it
                var target = $(this).attr('href');
                $(".tab-content .tab-pane").removeClass('active show');
                $(target).addClass("active show");
            });
        });


        // dept company disable 
        function ddldeptcompanydisable(e) {
            e.preventDefault()
            var ddl = document.getElementById('<%= ddldeptcompany.ClientID %>');
            ddl.options[0].disabled = true;
        }
        function ddldeptbranchdisable() {
            var ddl = document.getElementById('<%= ddldeptbranch.ClientID %>');
            ddl.options[0].disabled = true;
        }

        function drp5disable() {
            var ddl = document.getElementById('<%= DropDownList5.ClientID %>');
            ddl.options[0].disabled = true;
        }
    </script>


    <%--<script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>--%>
    <script>
        $(document).ready(function () {
            // Validation function for TextBox36
            function validateShiftTime() {
                var shiftTime = $('#<%= TextBox36.ClientID %>').val().trim();
                var shiftTimePattern = /^([0-1]?[0-9]|2[0-3]):[0-5][0-9] [APap][Mm] - ([0-1]?[0-9]|2[0-3]):[0-5][0-9] ?[APap][Mm]?$/;
                return shiftTimePattern.test(shiftTime);
            }

            // Button click handler
            $('#<%= btnaddshift.ClientID %>').click(function () {
                // Validate TextBox36
                if (!validateShiftTime()) {
                    // If validation fails, show error and prevent submission
                    $('#<%= TextBox36.ClientID %>').addClass('input-error');
                    return false;
                }
                // Validation passed, remove error class if present
                $('#<%= TextBox36.ClientID %>').removeClass('input-error');
                return true; // Proceed with form submission
            });
        });
    </script>


    <style type="text/css">
        .input-error {
            border-color: red;
        }
    </style>


    <script>
        function handleCompanyChange() {
            var selectedValue = $('#<%= ddldeptbranch.ClientID %>').val();
            return false;
        }
    </script>



   <script type="text/javascript">
       function validateAndHighlight() {
           var isValid = Page_ClientValidate(); // Trigger ASP.NET validators
           console.log("on click is ");
           if (isValid) {
               document.getElementById('TextBox2').style.borderColor = '';
               document.getElementById('TextBox6').style.borderColor = '';
           } else {
               if (!Page_IsValid) {
                   document.getElementById('TextBox2').style.borderColor = 'red';
                   document.getElementById('TextBox6').style.borderColor = 'red';
               }
           }
           return isValid;
       }
   </script>
    <script>
        $(document).ready(function () {
            var activeTab = $('#<%= hfActiveTab.ClientID %>').val();
        if (activeTab) {
            $('.nav-tabs a[href="' + activeTab + '"]').tab('show');
        }

        $('.nav-tabs a').on('click', function () {
            var tabId = $(this).attr('href');
            $('#<%= hfActiveTab.ClientID %>').val(tabId);
        });
       });
    </script>



</asp:Content>
