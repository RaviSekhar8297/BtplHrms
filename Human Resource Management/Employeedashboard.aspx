<%@ Page Title="" Language="C#" MasterPageFile="~/Employee.Master" AutoEventWireup="true" CodeBehind="EmployeeDashboard.aspx.cs" Inherits="Human_Resource_Management.Roles.Employee.EmployeeDashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
    /* Container for the entire employee card layout */
    .parent-container {
        display: flex;
        flex-wrap: wrap;
        gap: 1rem; /* Adjust spacing between cards */
        justify-content: space-between; /* Distribute space between cards */
    }

    /* Card style */
    .employee-card {
        display: flex;
        flex-direction: column;
        align-items: center;
        justify-content: center;
        width: calc(50% - 1rem); /* 2 cards per row with gap */
        background-color: #fff;
        border: 1px solid #ddd;
        border-radius: 10px;
        box-shadow: 0 2px 5px rgba(0, 0, 0, 0.1);
        padding: 1rem;
        box-sizing: border-box;
        position: relative; /* For positioning the logo */
    }

    /* Logo style */
    .company-logo {
        position: absolute;
        top: 10px;
        left: 10px;
        width: 50px; /* Adjust size as needed */
        height: 25px; /* Adjust size as needed */
    }

    /* Image style inside the card */
    .employee-image {
        width: 100px; /* Adjust size as needed */
        height: 100px; /* Adjust size as needed */
        border-radius: 50%;
        object-fit: cover;
        margin-bottom: 0.5rem;
    }

    /* Name style */
    .employee-name {
        font-size: 18px;
        color: green;
        margin-bottom: 0.5rem;
    }

    /* Anniversary style */
    .employee-anniversary {
        font-size: 15px;
        color: #555;
    }

    .employee-container:hover {
        border-color: blue;
        background-color: #aae3d4;
        color: black;
    }

    .add-btn1 {
        background-color: #ff9b44;
        border: 1px solid #ff9b44;
        color: #ffffff;
        font-weight: 500;
        min-width: 140px;
        border-radius: 50px;
    }

    .add-btn1 i {
        margin-right: 5px;
    }

    .add-btn1:hover {
        color: #ffffff;
        background-color: #ff9b44;
        border: 1px solid #ff9b44;
    }

    /* Scrollable section styles */
    .scrollable-section {
        max-height: 370px;
        min-height: auto;
        overflow-y: auto;
    }

    .Designation{
        font-size: 12px;
    }

    /* No Birthdays/Anniversaries Image Styles */
    .no-birthdays-image,
    .no-anniversaries-image {
        display: block;
        margin: 0 auto;
        max-width: 100%;
        height: 250px;
    }
</style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--Page Wrapper Start-->
    <div class="page-wrapper">
        <!--Page Content Start-->
        <div class="content container-fluid">
            <div class="row">
                <div class="col-md-12">
                    <div class="welcome-box">
                        <div class="welcome-img">
                            <asp:Image ID="Image1" runat="server" CssClass="image1" />
                        </div>
                        <!--Name & Date Start-->
                        <div class="welcome-det">
                            <h3>Welcome
                                <asp:Label ID="lblname" runat="server" ForeColor="Red"></asp:Label></h3>
                            <p>
                                <asp:Label ID="lbldesignation" runat="server" ForeColor="green"></asp:Label></p>
                        </div>
                        <!--Name & Date End-->
                    </div>
                   
                    
                </div>
            </div>

            <div class="row">
                <!--Notifications Start-->
                <div class="col-lg-8 col-md-8">
                    <section class="dash-section">
                        <h1 class="dash-sec-title">Today - BirthDay 🎂</h1>
                        <div class="card scrollable-section" style="gap: 2rem; padding:10px;">
                            <asp:PlaceHolder ID="birthdayContainer" runat="server"></asp:PlaceHolder>
                        </div>
                    </section>

                    <section class="dash-section">
                        <h1 class="dash-sec-title">TODAY - ANNIVERSARY</h1>
                        <div class="card scrollable-section" style="gap: 1rem; padding:10px;">
                            <asp:PlaceHolder ID="AnniversaryContainer" runat="server"></asp:PlaceHolder>
                        </div>
                    </section>
                </div>


                <div class="col-lg-4 col-md-4">
                    <div class="dash-sidebar">
                        <section>
                        </section>
                        <section>
                            <h5 class="dash-title">Your Leave</h5>
                            <div class="card">
                                <div class="card-body">
                                    <div class="time-list">
                                        <div class="dash-stats-list">
                                            <h4>
                                                <asp:Label ID="Label1" runat="server" Text="Label" ForeColor="Red"></asp:Label></h4>
                                            <p>Leave Taken</p>
                                        </div>
                                        <div class="dash-stats-list">
                                            <h4>
                                                <asp:Label ID="Label2" runat="server" Text="Label" ForeColor="Green"></asp:Label></h4>
                                            <p>Remaining</p>
                                        </div>
                                    </div>
                                    <div class="request-btn">
<%--                                        <a class="btn add-btn1" href="#" data-bs-toggle="modal" data-bs-target="#add_leave"><i class="fa-sharp fa-solid fa-person-walking-arrow-right"></i>Apply Leave</a>--%>
                                         <a href="EmployeeAddLeave.aspx"  class="btn add-btn1"><i class="fa-sharp fa-solid fa-person-walking-arrow-right"></i>Apply Leave</a>
                                    </div>
                                </div>
                            </div>
                        </section>
                        <section>
                            <h5 class="dash-title">Your time off allowance</h5>
                            <div class="card">
                                <div class="card-body">
                                    <div class="time-list">
                                        <div class="dash-stats-list">
                                            <h4>
                                                <asp:Label ID="lblslused" runat="server" ForeColor="red"></asp:Label>
                                                Hrs</h4>
                                            <p>Using</p>
                                        </div>
                                        <div class="dash-stats-list">
                                            <h4>
                                                <asp:Label ID="lblslremaining" runat="server" ForeColor="Green"></asp:Label>
                                                Hrs</h4>
                                            <p>Remaining</p>
                                        </div>
                                    </div>
                                    <div class="request-btn">
                                        <a class="btn add-btn1" href="EmployeeTimeOff.aspx"><i class="fa-solid fa-pencil"></i>TimeOff</a>
                                    </div>
                                </div>
                            </div>
                        </section>
                        <section>
                            <h5 class="dash-title">Upcoming Holiday</h5>
                            <div class="card">
                                <div class="card-body text-center">
                                    <h4 class="holiday-title mb-0">
                                        <asp:Label ID="lblNextHoliday" runat="server" ForeColor="green"></asp:Label></h4>
                                </div>
                            </div>
                        </section>
                    </div>
                </div>
            </div>


            <!-- Add TimeOff Modal -->
            <div id="add_timeoff" class="modal custom-modal fade" role="dialog">
                <div class="modal-dialog modal-dialog-centered" role="document">
                    <div class="modal-content" id="popupleave1">
                        <div class="modal-header">
                            <h5 class="modal-title">Apply TimeOff</h5>
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="modal-body">
                            <div class="input-block mb-3">
                                <label class="col-form-label">Total TimeOff (Hours)<span class="text-danger">*</span></label>
                                <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                <asp:Label ID="Label3" runat="server" ForeColor="Red"></asp:Label>
                            </div>
                            <div class="input-block mb-3">
                                <label class="col-form-label">Take TimeOff (Hours)<span class="text-danger">*</span></label>
                                <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control" onkeypress="return isNumberKey(event);"></asp:TextBox>
                            </div>

                            <div class="input-block mb-3">
                                <label class="col-form-label">Reason <span class="text-danger">*</span></label>
                                <asp:TextBox ID="TextBox4" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                            </div>
                            <div class="submit-section">
                                <asp:Button ID="btntimeoff" runat="server" Text="Apply TimeOff" CssClass="btn btn-primary submit-btn" OnClick="btntimeoff_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- /Add TimeOff Modal -->
        </div>
        <!--Page Content End-->
    </div>
    <!--Page Wrapper End-->


</asp:Content>
