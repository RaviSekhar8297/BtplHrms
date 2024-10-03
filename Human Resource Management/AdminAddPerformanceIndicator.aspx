<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="AdminAddPerformanceIndicator.aspx.cs" Inherits="Human_Resource_Management.AdminAddPerformanceIndicator" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

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
                            <h3 class="page-title">Performance Indicator</h3>
                            <ul class="breadcrumb">
                                <li class="breadcrumb-item"><a href="admin-dashboard.html">Dashboard</a></li>
                                <li class="breadcrumb-item active">Performance</li>
                            </ul>
                        </div>
                        <div class="col-auto float-end ms-auto">
                            <a href="AdminAddPerformanceIndicator.aspx" class="btn add-btn"><i class="fa-solid fa-plus"></i>Add New</a>
                        </div>
                    </div>
                </div>
                <!-- /Page Header -->
                <!-- Add Performance Indicator Modal -->
                <asp:Label ID="Label1" runat="server" ForeColor="Red"></asp:Label>
                <div class="row">
                    <div class="col-sm-4">
                        <div class="input-block mb-3">
                            <label class="col-form-label">Company<span class="text-danger">*</span></label>
                            <asp:DropDownList ID="ddlcompany" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlcompany_SelectedIndexChanged" onfocus="ddladddcompanydisable()"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-sm-4">
                        <div class="input-block mb-3">
                            <label class="col-form-label">Branch<span class="text-danger">*</span></label>
                            <asp:DropDownList ID="ddlbranch" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlbranch_SelectedIndexChanged" onfocus="ddladddbranchdisable()"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-sm-4">
                        <div class="input-block mb-3">
                            <label class="col-form-label">Department<span class="text-danger">*</span></label>
                            <asp:DropDownList ID="ddldepartment" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddldepartment_SelectedIndexChanged" onfocus="ddladddeptdisable()"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-sm-4">
                        <div class="input-block mb-3">
                            <label class="col-form-label">Name <span class="text-danger">*</span></label>
                            <asp:DropDownList ID="ddlname" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlname_SelectedIndexChanged" onfocus="ddladdnamedisable()"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-sm-4">
                        <div class="input-block mb-3">
                            <label class="col-form-label">Employee Id <span class="text-danger">*</span></label>
                            <asp:TextBox ID="txtid" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-sm-4">
                        <div class="input-block mb-3">
                            <label class="col-form-label">Designation <span class="text-danger">*</span></label>
                            <asp:TextBox ID="txtdesignation" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-sm-4">
                        <div class="input-block mb-3">
                            <label class="col-form-label"><span class="text-danger">*</span></label>
                            Regular :
       
                            <cc1:Rating ID="Rating1" runat="server" StarCssClass="Star" WaitingStarCssClass="WaitingStar" EmptyStarCssClass="Star" FilledStarCssClass="FilledStar"></cc1:Rating>
                            <br />
                            Behaviour :
       
                            <cc1:Rating ID="Rating2" runat="server" StarCssClass="Star" WaitingStarCssClass="WaitingStar" EmptyStarCssClass="Star" FilledStarCssClass="FilledStar"></cc1:Rating>
                            <br />
                            Work :
       
                            <cc1:Rating ID="Rating3" runat="server" StarCssClass="Star" WaitingStarCssClass="WaitingStar" EmptyStarCssClass="Star" FilledStarCssClass="FilledStar"></cc1:Rating>
                            <br />
                             <asp:HiddenField ID="hiddenOverallReview" runat="server" />
                            Overall review:
       
                            <asp:TextBox ID="txtovarallreview" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                        </div>
                    </div>


                </div>
                <div class="submit-section">
                    <asp:Button ID="btnreview" runat="server" Text="Submit" CssClass="btn btn-primary submit-btn" OnClick="btnreview_Click" />
                </div>
            </div>
        </div>
    </div>
    <!-- /Add Performance Indicator Modal -->
    <script type="text/javascript">
        function ddladddcompanydisable() {
            var ddl = document.getElementById('<%= ddlcompany.ClientID %>');
            ddl.options[0].disabled = true;
        }
        function ddladddbranchdisable() {
            var ddl = document.getElementById('<%= ddlbranch.ClientID %>');
            ddl.options[0].disabled = true;
        }
        function ddladddeptdisable() {
            var ddl = document.getElementById('<%= ddldepartment.ClientID %>');
            ddl.options[0].disabled = true;
        }
        function ddladdnamedisable() {
            var ddl = document.getElementById('<%= ddlname.ClientID %>');
            ddl.options[0].disabled = true;
        }


    </script>


    <style type="text/css">
        .Star {
            background-image: url(Images/Star.gif);
            height: 17px;
            width: 17px;
        }

        .WaitingStar {
            background-image: url(Images/WaitingStar.gif);
            height: 17px;
            width: 17px;
        }

        .FilledStar {
            background-image: url(Images/FilledStar.gif);
            height: 17px;
            width: 17px;
        }
    </style>

  <script type="text/javascript">
      console.log("start");

      document.addEventListener('DOMContentLoaded', function () {
          // Use setInterval to repeatedly check if the elements are available
          var checkInterval = setInterval(function () {
              var rating1Element = document.querySelector('#<%= Rating1.ClientID %>');
            var rating2Element = document.querySelector('#<%= Rating2.ClientID %>');
            var rating3Element = document.querySelector('#<%= Rating3.ClientID %>');

            console.log("called " + rating1Element);

            if (rating1Element && rating2Element && rating3Element) {
                // Elements are available, clear the interval
                clearInterval(checkInterval);

                // Add event listeners to the rating elements
                rating1Element.addEventListener('click', updateOverallReview);
                rating2Element.addEventListener('click', updateOverallReview);
                rating3Element.addEventListener('click', updateOverallReview);
            }
        }, 100); // Check every 100ms
    });

      function updateOverallReview() {
          // Use querySelector to find the hidden input values associated with the ratings
          var rating1 = parseInt(document.querySelector('#<%= Rating1.ClientID %> input[type="hidden"]').value) || 0;
        var rating2 = parseInt(document.querySelector('#<%= Rating2.ClientID %> input[type="hidden"]').value) || 0;
        var rating3 = parseInt(document.querySelector('#<%= Rating3.ClientID %> input[type="hidden"]').value) || 0;

        console.log("function " + rating1 + ", " + rating2 + ", " + rating3);

        var total = rating1 + rating2 + rating3;
        var percentage = (total / 15) * 100; // 3 ratings, each out of 5 stars

        var review;
        if (percentage > 80) {
            review = "Super";
        } else if (percentage > 60) {
            review = "Good";
        } else if (percentage > 40) {
            review = "Average";
        } else {
            review = "Poor";
        }

          document.getElementById('<%= txtovarallreview.ClientID %>').value = review;
          document.getElementById('<%= hiddenOverallReview.ClientID %>').value = review;
      }
</script>
</asp:Content>
