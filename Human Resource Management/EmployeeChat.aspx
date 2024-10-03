<%@ Page Title="" Language="C#" MasterPageFile="~/Employee.Master" AutoEventWireup="true" CodeBehind="EmployeeChat.aspx.cs" Inherits="Human_Resource_Management.EmployeeChat" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <style type="text/css">
        /* General Styles */
        body {
            font-family: Arial, sans-serif;
        }

        .img-img {
            border-radius: 50%;
            width: 40px;
            height: 40px;
        }

        .header-image {
            border-radius: 50%;
            height: 50px;
            width: 50px;
        }

        .header-name-imag {
            display: flex;
            align-items: center;
            padding: 10px;
            background-color: #f7f7f7;
            border-bottom: 1px solid #ddd;
            border-radius: 5px;
            position: sticky;
            top: 0;
            z-index: 100; /* Ensure the header stays on top of other content */
        }

        .name-main-div {
            margin-left: 15px;
            display: flex;
            flex-direction: column;
            justify-content: center;
            overflow: hidden;
            white-space: nowrap;
            text-overflow: ellipsis;
        }

        .header-name {
            font-size: 18px;
            font-weight: bold;
            color: #333;
        }

        /* Sidebar and Search */
        .sidedive {
            width: 25%;
            display: flex;
            flex-direction: column;
            padding: 0px 15px 0px 0px;
            background-color: #fafafa;
            border-right: 1px solid #ddd;
        }

        .fixDiv {
            position: sticky;
            top: 0;
            background-color: #ffffff;
            padding: 10px;
            z-index: 1000;
            margin-bottom: 15px;
        }

        .form-control {
            padding: 10px;
            border-radius: 5px;
            border: 1px solid #ccc;
            width: 100%;
        }

        .userlist-container {
            width: 100%;
            overflow-y: auto;
            max-height: 400px;
            background-color: #fff;
            border: 1px solid #ddd;
            border-radius: 5px;
            box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
            max-height: 500px;
            padding: 10px;
        }

        .userlist {
            display: flex;
            align-items: center;
            padding: 10px;
            gap: 15px;
            cursor: pointer;
            transition: background-color 0.3s ease;
            margin: 5px;
            border-radius: 20px;
        }

        .userlist:hover {
            background-color: #f0f0f0;
        }

        .img-div {
            flex-shrink: 0;
        }

        .user-info {
            display: flex;
            flex-direction: column;
            overflow: hidden;
        }

        .user-name {
            font-size: 16px;
            font-weight: bold;
            color: #333;
            overflow: hidden;
            white-space: nowrap;
            text-overflow: ellipsis;
        }

        .user-role {
            font-size: 14px;
            color: gray;
            overflow: hidden;
            white-space: nowrap;
            text-overflow: ellipsis;
        }

        /* Main Chat Area */
        .mainClass {
            display: flex;
            width: 100%;
        }

        .seconddiv {
            width: 75%;
            display: flex;
            flex-direction: column;
        }

        .main-box {
            flex-grow: 1;
            display: flex;
            flex-direction: column;
            border: 1px solid #ddd;
            background-color: #f5f5f5;
            border-radius: 5px;
            padding: 10px;
            overflow-y: auto;
            max-height: 500px;
        }

        .chat-message {
            display: flex;
            flex-direction: column;
            margin-bottom: 10px;
            padding: 10px;
            border-radius: 8px;
            max-width: 50%; /* Adjust to fit within chat container */
            word-wrap: break-word;
            position: relative;
            margin-top: 10px; /* Space before each new message */
            box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1); /* Subtle shadow for better separation */
        }

        /* Styling for messages from the logged-in user */
        .right-message {
            background-color: #dcf8c6; /* Light green background for user messages */
            align-self: flex-end; /* Align messages to the right */
            margin-left: auto; /* Align to the right side of the container */
            border-radius: 8px 8px 0 8px; /* Rounded corners on the right side */
            margin-bottom: 15px; /* Space between messages */
        }

        /* Styling for messages from others */
        .left-message {
            background-color: #ffffff; /* White background for received messages */
            align-self: flex-start; /* Align messages to the left */
            margin-right: auto; /* Align to the left side of the container */
            border-radius: 8px 8px 8px 0; /* Rounded corners on the left side */
            margin-bottom: 15px; /* Space between messages */
        }

        .seen-sender {
            background-color: #e0ffe0; /* Light green for seen messages from sender */
        }

        .unseen-sender {
            background-color: #f0f0f0; /* Light gray for unseen messages from sender */
        }

        .seen-recipient {
            background-color: #f0f0f0; /* Light blue for seen messages from recipient */
        }

        .unseen-recipient {
            background-color: #f0f0f0; /* Light gray for unseen messages from recipient */
        }

        .right-message {
            text-align: right;
        }

        .left-message {
            text-align: left;
        }

        .input-box {
            display: flex;
            gap: 10px;
            margin-top: 15px;
        }

        .btn123 {
            background-color: #007bff;
            color: white;
            padding: 10px;
            border-radius: 5px;
            display: flex;
            align-items: center;
            justify-content: center;
            cursor: pointer;
        }
        textarea.form-control {
            height: 60px;
        }
        .message-image {
            max-width: 200px; /* Adjust as needed */
            max-height: 200px; /* Adjust as needed */
            display: block;
            margin: 5px 0;
           align-self: center;

        }
        .fileupload{
            width:100px;
        }
        .chat-message-time{
            font-size:11px;
        }
        .msg-image{
            width:18px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="main-wrapper">
        <div class="page-wrapper">
            <div class="content container-fluid">
                <div class="page-header">
                    <div class="row align-items-center">
                        <div class="col">
                            <h3 class="page-title">Employees Chat</h3>
                            <ul class="breadcrumb">
                                <li class="breadcrumb-item"><a href="EmployeeDashboard.aspx">Dashboard</a></li>
                                <li class="breadcrumb-item active">Chat</li>
                            </ul>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="mainClass">
                        <div class="sidedive">
                            <div class="fixDiv">
                                <asp:TextBox ID="TextBox2" runat="server" AutoPostBack="true" placeholder="Search Employee..." AutoCompleteType="Disabled" CssClass="form-control" OnTextChanged="TextBox2_TextChanged"></asp:TextBox>
                            </div>
                            <div class="userlist-container">
                                <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
                            </div>
                        </div>

                        <div class="seconddiv">
                            <div class="main-box" id="chatdiv">
                                <div class="header-name-imag">
                                    <asp:Image ID="headerImage" runat="server" CssClass="header-image" />
                                    <div class="name-main-div">
                                        <asp:Label ID="headerName" runat="server" CssClass="header-name" />
                                    </div>
                                </div>
                                <div class="left" id="leftMessages">
                                    <asp:PlaceHolder ID="LeftChatData" runat="server"></asp:PlaceHolder>
                                </div>
                                <div class="right" id="rightMessages">
                                    <asp:PlaceHolder ID="RightChatData" runat="server"></asp:PlaceHolder>
                                </div>
                            </div>                           
                            <div class="input-box">
                                <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control" TextMode="MultiLine" placeholder="Enter Message"></asp:TextBox>
                                 <asp:FileUpload ID="FileUpload1" runat="server" CssClass="fileupload" />
                                <asp:LinkButton ID="LinkButton1" runat="server" OnClick="sendMessage_Click" CssClass="btn123">
                                    <i class="fas fa-paper-plane"></i>
                                </asp:LinkButton>
                                <asp:HiddenField ID="loggedInEmpId" runat="server" Value='<%= Session["EmpId"] %>' />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        // Get the session value directly from server-side code
        var sessionEmpId = '<%= Session["EmpId"] %>';
        console.log('Session emp value:', sessionEmpId);

        function selectEmployee(empId) {
            // sessionEmpId is already available as a JavaScript variable
            console.log('Session emp value:', sessionEmpId);
            console.log("Selected id : " + empId);

            if (sessionEmpId) {
                window.location.href = "EmployeeChat.aspx?empId=" + encodeURIComponent(empId) + "&SId=" + encodeURIComponent(sessionEmpId);
            } else {
                console.log("Session ID is not available.");
            }
        }
    </script>

    <script type="text/javascript">
        function checkFields() {
            var textBox = document.getElementById('<%= TextBox1.ClientID %>').value.trim();
        var fileUpload = document.getElementById('<%= FileUpload1.ClientID %>').value;

        var linkButton = document.getElementById('<%= LinkButton1.ClientID %>');

        if (textBox === "" && fileUpload === "") {
            linkButton.style.display = 'none'; // Hide LinkButton1
            linkButton.disabled = true; // Disable LinkButton1
        } else {
            linkButton.style.display = 'inline-block'; // Show LinkButton1
            linkButton.disabled = false; // Enable LinkButton1
        }
    }

    document.addEventListener("DOMContentLoaded", function() {
        // Run checkFields on page load
        checkFields();

        // Add event listeners
        document.getElementById('<%= TextBox1.ClientID %>').addEventListener('input', checkFields);
        document.getElementById('<%= FileUpload1.ClientID %>').addEventListener('change', checkFields);
    });
    </script>


</asp:Content>
