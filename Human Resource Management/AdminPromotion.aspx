<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="AdminPromotion.aspx.cs" Inherits="Human_Resource_Management.AdminPromotion" %>
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
                            <h3 class="page-title">Promotion</h3>
                            <ul class="breadcrumb">
                                <li class="breadcrumb-item"><a href="admin-dashboard.html">Dashboard</a></li>
                                <li class="breadcrumb-item active">Promotion</li>
                            </ul>
                        </div>
                        <div class="col-auto float-end ms-auto">
                           
                            
                        </div>
                    </div>
                </div>
                <!-- /Page Header -->
               
                <div class="row">
                    <div class="col-md-12">

                        <div class="table-responsive">
                            <asp:PlaceHolder ID="CheckInOuts" runat="server"></asp:PlaceHolder>
                            
                            <div class="navigation-buttons">
                                <asp:Button ID="btnPrev" runat="server" Text="Previous Month" OnClick="btnPrev_Click" />
                                <asp:Label ID="lblMonthYear" runat="server" Font-Bold="True"></asp:Label>
                                <asp:Button ID="btnNext" runat="server" Text="Next Month" OnClick="btnNext_Click" />
                            </div>

                            <!-- Large Calendar Display -->
                            <div id="calendarContainer">
                                <asp:Literal ID="calendarLiteral" runat="server"></asp:Literal>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- /Page Content -->
        </div>
        <!-- /Page Wrapper -->
    </div>
    <!-- /Main Wrapper -->
    <style>
        /* Calendar table styling */
        table.attendance-calendar {
            width: 100%;
            border-collapse: collapse;
            font-family: Arial, sans-serif;
            font-size: 18px;
            margin: 20px 0;
        }

        /* Table cell styling */
        .attendance-calendar th, .attendance-calendar td {
            width: 14.28%;  /* 100% / 7 days = 14.28% */
            height: 120px;  /* Set a base height */
            border: 1px solid #ddd;
            text-align: center;
            vertical-align: top;
            padding: 5px;
            position: relative;
            transition: background-color 0.3s ease-in-out; /* Transition effect for hover */
        }

        /* Sunday, Holiday, Present, Absent cell styles */
        .sunday { background-color: #f5f5f5; }
        .holiday { background-color: #cce5ff; }
        .present { background-color: #d4edda; }
        .absent { background-color: #fff3cd; }

        /* Hover effect for all cells */
        .attendance-calendar td:hover {
            background-color: #f0f8ff; /* Light blue on hover */
            cursor: pointer;
        }

        /* Display day number and Min/Max times */
        .attendance-calendar td div:first-child {
            font-weight: bold;
            font-size: 24px;
            position: absolute;
            top: 5px;
            left: 5px;
        }

        .attendance-calendar td div:nth-child(2) {
            margin-top: 40px;
            font-size: 16px;
            font-weight: normal;
            color: #333;
        }

        /* Responsive image for holidays */
        .holiday-logo {
            max-width: 100%;
            max-height: 100%;
            display: block;
            margin: auto;
        }
        .attendance-time {
            position: relative;
            cursor: pointer;
        }

        .attendance-time:hover::after {
            content: attr(title); /* Use the title attribute as the content */
            position: absolute;
            background-color: #333;
            color: #fff;
            padding: 5px;
            border-radius: 5px;
            white-space: nowrap;
            top: 100%; /* Position the tooltip below the text */
            left: 50%;
            transform: translateX(-50%);
            z-index: 100;
            font-size: 12px;
            display: block;
        }

        /* Media query for smaller screens */
        @media (max-width: 768px) {
            .attendance-calendar td { height: 100px; font-size: 16px; }
            .attendance-calendar td div:first-child { font-size: 20px; }
            .attendance-calendar td div:nth-child(2) { font-size: 14px; }
        }

    </style>
</asp:Content>