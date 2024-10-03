using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;
using DocumentFormat.OpenXml.Office.Word;
using System.ComponentModel.Design;

namespace Human_Resource_Management
{
    public partial class AdminLeaves : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LeavesCount();
                BindAllLeaveData();
            }
        }

        public void LeavesCount()
        {
            try
            {
                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    connection.Open();

                    // Current year and month
                    int currentYear = DateTime.Now.Year;
                    int currentMonth = DateTime.Now.Month;

                    // Query to count total leaves for the current year and month
                    SqlCommand totalleavcmd = new SqlCommand(
                        "SELECT COUNT(*) FROM LeavesStatus22 " +
                        "WHERE YEAR(FromDate) = @Year " +
                        "AND MONTH(FromDate) = @Month " +
                        "AND YEAR(ToDate) = @Year " +
                        "AND MONTH(ToDate) = @Month",
                        connection);
                    totalleavcmd.Parameters.AddWithValue("@Year", currentYear);
                    totalleavcmd.Parameters.AddWithValue("@Month", currentMonth);

                    // Query to count pending leaves
                    SqlCommand pndcmd = new SqlCommand(
                        "SELECT COUNT(*) FROM LeavesStatus22 " +
                        "WHERE StatusReason = 'Pending' " +
                        "AND YEAR(FromDate) = @Year " +
                        "AND MONTH(FromDate) = @Month " +
                        "AND YEAR(ToDate) = @Year " +
                        "AND MONTH(ToDate) = @Month",
                        connection);
                    pndcmd.Parameters.AddWithValue("@Year", currentYear);
                    pndcmd.Parameters.AddWithValue("@Month", currentMonth);

                    // Query to count approved leaves
                    SqlCommand aprcmd = new SqlCommand(
                        "SELECT COUNT(*) FROM LeavesStatus22 " +
                        "WHERE StatusReason = 'Approved' " +
                        "AND YEAR(FromDate) = @Year " +
                        "AND MONTH(FromDate) = @Month " +
                        "AND YEAR(ToDate) = @Year " +
                        "AND MONTH(ToDate) = @Month",
                        connection);
                    aprcmd.Parameters.AddWithValue("@Year", currentYear);
                    aprcmd.Parameters.AddWithValue("@Month", currentMonth);

                    // Query to count rejected leaves
                    SqlCommand rejcmd = new SqlCommand(
                        "SELECT COUNT(*) FROM LeavesStatus22 " +
                        "WHERE StatusReason = 'Rejected' " +
                        "AND YEAR(FromDate) = @Year " +
                        "AND MONTH(FromDate) = @Month " +
                        "AND YEAR(ToDate) = @Year " +
                        "AND MONTH(ToDate) = @Month",
                        connection);
                    rejcmd.Parameters.AddWithValue("@Year", currentYear);
                    rejcmd.Parameters.AddWithValue("@Month", currentMonth);

                    // Execute the commands and get the counts
                    int totalLeavesCount = (int)totalleavcmd.ExecuteScalar();
                    int pendingLeavesCount = (int)pndcmd.ExecuteScalar();
                    int approvedLeavesCount = (int)aprcmd.ExecuteScalar();
                    int rejectedLeavesCount = (int)rejcmd.ExecuteScalar();

                    // Display the counts in Labels
                    Label1.Text = totalLeavesCount.ToString();
                    Label2.Text = approvedLeavesCount.ToString();
                    Label3.Text = pendingLeavesCount.ToString();
                    Label4.Text = rejectedLeavesCount.ToString();
                }
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                Response.Write("<script>alert('Error: " + ex.Message + "')</script>");
            }
        }


        public void BindAllLeaveData()
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "SELECT LeavesStatus22.*, Employees.FirstName , Employees.Image " +
                                              "FROM LeavesStatus22 " +
                                              "JOIN Employees ON LeavesStatus22.EmployeedID = Employees.EmpId " +
                                              "WHERE YEAR(LeavesStatus22.FromDate) = YEAR(GETDATE()) " +
                                              "AND MONTH(LeavesStatus22.FromDate) = MONTH(GETDATE()) " +
                                              "ORDER BY LeavesStatus22.FromDate, LeavesStatus22.LeaveId DESC;";
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            EditLeave.Controls.Clear();
                            while (reader.Read())
                            {
                                string Name = reader["FirstName"].ToString();

                                object imageDataObj = reader["Image"];
                                byte[] imageData = (byte[])imageDataObj ?? new byte[0];
                                string base64String = Convert.ToBase64String(imageData);
                                string imageUrl = "data:image/jpeg;base64," + base64String;

                                string LeaveId = reader["LeaveId"].ToString();
                                string leavetype = reader["leave_type"].ToString();
                                DateTime formattedFromDate = (DateTime)reader["FromDate"];
                                string FromDate = formattedFromDate.ToString("yyyy-MM-dd");
                                DateTime toDate = (DateTime)reader["ToDate"];
                                string ToDate = toDate.ToString("yyyy-MM-dd");
                                string Duration = reader["Duration"].ToString();
                                string Reason = reader["ReasontoApply"].ToString();
                                string Status = reader["StatusReason"].ToString();
                                string EmpId = reader["EmployeedID"].ToString();

                                // Determine color based on status
                                string statusColor;
                                if (Status.Trim() == "Pending")
                                {
                                    statusColor = "orange";
                                }
                                else if (Status.Trim() == "Approved")
                                {
                                    statusColor = "green";
                                }
                                else if (Status.Trim() == "Rejected")
                                {
                                    statusColor = "red";
                                }
                                else
                                {
                                    statusColor = "black"; // Default color
                                }

                                StringBuilder projectHtml = new StringBuilder();
                                projectHtml.Append("<tr>");
                                projectHtml.Append("<td><a href='#' class='avatar'><img src='" + imageUrl + "' alt='User Image'></a>");
                                projectHtml.Append("<a href='profile.html'>" + Name + "</a></td>");
                                projectHtml.Append("<td style='color:" + statusColor + ";'>" + leavetype + "</td>");
                                projectHtml.Append("<td>" + FromDate + "</td>");
                                projectHtml.Append("<td>" + ToDate + "</td>");
                                projectHtml.Append("<td>" + Duration + "</td>");
                                projectHtml.Append("<td>" + Reason + "</td>");
                                projectHtml.Append("<td style='color:" + statusColor + ";'>" + Status + "</td>");

                                if (Status.Trim() == "Pending")
                                {
                                    projectHtml.Append("<td><a class='dropdown-item' href='#' data-bs-toggle='modal' data-bs-target='#edit_leave' onclick=\"editLeave('" + Name + "','" + LeaveId + "','" + leavetype + "','" + FromDate + "','" + ToDate + "','" + Duration + "','" + Reason + "','" + EmpId + "')\"><i class='fa-solid fa-pencil m-r-5' style='color:#057a28;'></i></a></td>");
                                }
                                else
                                {
                                    projectHtml.Append("<td></td>");
                                }

                                projectHtml.Append("</tr>");

                                EditLeave.Controls.Add(new LiteralControl(projectHtml.ToString()));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                Response.Write("<script>alert('Error: " + ex.Message + "')</script>");
            }
        }


        protected void ddlLeaveType_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtname.Text = "";
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = @"SELECT LeavesStatus22.*, Employees.FirstName, Employees.Image  FROM LeavesStatus22 JOIN Employees ON LeavesStatus22.EmployeedID = Employees.EmpId 
                WHERE ((YEAR(LeavesStatus22.FromDate) = YEAR(GETDATE()) AND MONTH(LeavesStatus22.FromDate) = MONTH(GETDATE())) OR 
                       (YEAR(LeavesStatus22.FromDate) = YEAR(GETDATE()) AND MONTH(LeavesStatus22.FromDate) = MONTH(GETDATE()) - 1))  AND leave_type = @LeaveType ORDER BY LeavesStatus22.FromDate DESC, LeavesStatus22.LeaveId DESC;";

                        command.Parameters.AddWithValue("@LeaveType", ddlLeaveType.SelectedItem.Text); // Assuming ddlLeaveType is a DropDownList

                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            EditLeave.Controls.Clear();

                            // Check if there are any rows returned
                            if (!reader.HasRows)
                            {
                                EditLeave.Controls.Add(new LiteralControl("<tr><td colspan='8'>No leaves found for selected type.</td></tr>"));
                            }
                            else
                            {
                                while (reader.Read())
                                {
                                    string Name = reader["FirstName"].ToString();

                                    byte[] imageData = (byte[])reader["Image"];
                                    string base64String = Convert.ToBase64String(imageData);
                                    string imageUrl = "data:image/jpeg;base64," + base64String;

                                    string LeaveId = reader["LeaveId"].ToString();

                                    string leavetype = reader["leave_type"].ToString();
                                    DateTime formattedFromDate = (DateTime)reader["FromDate"];
                                    string FromDate = formattedFromDate.ToString("yyyy-MM-dd");

                                    DateTime toDate = (DateTime)reader["ToDate"];
                                    string ToDate = toDate.ToString("yyyy-MM-dd");
                                    string Duration = reader["Duration"].ToString();
                                    string Reason = reader["ReasontoApply"].ToString();
                                    string Status = reader["StatusReason"].ToString();
                                    string EmpId = reader["EmployeedID"].ToString();

                                    StringBuilder projectHtml = new StringBuilder();
                                    projectHtml.Append("<tr>");
                                    projectHtml.Append("<td><a href='profile.html' class='avatar'><img src=" + imageUrl + " alt='User Image'></a>");
                                    projectHtml.Append("<a href='profile.html'>" + Name + " </a></td>");
                                    // projectHtml.Append("<td>" + LeaveId + "</td>");
                                    projectHtml.Append("<td style='color:blue;'>" + leavetype + "</td>");
                                    projectHtml.Append("<td>" + FromDate + "</td>");
                                    projectHtml.Append("<td>" + ToDate + "</td>");
                                    projectHtml.Append("<td>" + Duration + "</td>");
                                    projectHtml.Append("<td>" + Reason + "</td>");
                                    projectHtml.Append("<td>" + Status + "</td>");
                                    if (Status.Trim() == "Approved" || Status.Trim() == "Rejected")
                                    {

                                    }
                                    else
                                    {
                                        projectHtml.Append("<td><a class='dropdown-item' href='#' data-bs-toggle='modal' data-bs-target='#edit_leave' onclick=\"editLeave('" + Name + "','" + LeaveId + "','" + leavetype + "','" + FromDate + "','" + ToDate + "','" + Duration + "','" + Reason + "','" + EmpId + "')\"><i class='fa-solid fa-pencil m-r-5' style='color:#057a28;'></i></a></td>");
                                    }
                                    projectHtml.Append("</tr>");

                                    EditLeave.Controls.Add(new LiteralControl(projectHtml.ToString()));
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        protected void ddlLeaveStatus_SelectedIndexChanged(object sender, EventArgs e)
        {

            txtname.Text = "";
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;

                        // Construct the SQL query with parameterized query to prevent SQL injection
                        command.CommandText = @"
                SELECT LeavesStatus22.*, Employees.FirstName , Employees.Image 
                FROM LeavesStatus22 
                JOIN Employees ON LeavesStatus22.EmployeedID = Employees.EmpId 
                WHERE ((YEAR(LeavesStatus22.FromDate) = YEAR(GETDATE()) AND MONTH(LeavesStatus22.FromDate) = MONTH(GETDATE())) OR 
                       (YEAR(LeavesStatus22.FromDate) = YEAR(GETDATE()) AND MONTH(LeavesStatus22.FromDate) = MONTH(GETDATE()) - 1)) 
                      AND LeavesStatus22.StatusReason = @LeaveStatus 
                ORDER BY LeavesStatus22.FromDate DESC, LeavesStatus22.LeaveId DESC;";

                        // Clear previous parameters, if any
                        command.Parameters.Clear();
                        // Assuming ddlLeaveStatus is a DropDownList containing 'Approved', 'Pending', 'Rejected'
                        command.Parameters.AddWithValue("@LeaveStatus", ddlLeaveStatus.SelectedItem.Text);

                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            EditLeave.Controls.Clear();

                            // Check if there are any rows returned
                            if (!reader.HasRows)
                            {
                                EditLeave.Controls.Add(new LiteralControl("<tr><td colspan='8'>No leaves found for selected status.</td></tr>"));
                            }
                            else
                            {
                                while (reader.Read())
                                {
                                    string Name = reader["FirstName"].ToString();

                                    byte[] imageData = (byte[])reader["Image"];
                                    string base64String = Convert.ToBase64String(imageData);
                                    string imageUrl = "data:image/jpeg;base64," + base64String;

                                    string LeaveId = reader["LeaveId"].ToString();

                                    string leavetype = reader["leave_type"].ToString();
                                    DateTime formattedFromDate = (DateTime)reader["FromDate"];
                                    string FromDate = formattedFromDate.ToString("yyyy-MM-dd");

                                    DateTime toDate = (DateTime)reader["ToDate"];
                                    string ToDate = toDate.ToString("yyyy-MM-dd");
                                    string Duration = reader["Duration"].ToString();
                                    string Reason = reader["ReasontoApply"].ToString();
                                    string Status = reader["StatusReason"].ToString();
                                    string EmpId = reader["EmployeedID"].ToString();

                                    StringBuilder projectHtml = new StringBuilder();
                                    projectHtml.Append("<tr>");
                                    projectHtml.Append("<td><a href='profile.html' class='avatar'><img src=" + imageUrl + " alt='User Image'></a>");
                                    projectHtml.Append("<a href='profile.html'>" + Name + " </a></td>");
                                    // projectHtml.Append("<td>" + LeaveId + "</td>");
                                    projectHtml.Append("<td style='color:blue;'>" + leavetype + "</td>");
                                    projectHtml.Append("<td>" + FromDate + "</td>");
                                    projectHtml.Append("<td>" + ToDate + "</td>");
                                    projectHtml.Append("<td>" + Duration + "</td>");
                                    projectHtml.Append("<td>" + Reason + "</td>");
                                    projectHtml.Append("<td>" + Status + "</td>");
                                    if (Status.Trim() == "Approved" || Status.Trim() == "Rejected")
                                    {

                                    }
                                    else
                                    {
                                        projectHtml.Append("<td><a class='dropdown-item' href='#' data-bs-toggle='modal' data-bs-target='#edit_leave' onclick=\"editLeave('" + Name + "','" + LeaveId + "','" + leavetype + "','" + FromDate + "','" + ToDate + "','" + Duration + "','" + Reason + "','" + EmpId + "')\"><i class='fa-solid fa-pencil m-r-5' style='color:#057a28;'></i></a></td>");
                                    }
                                    projectHtml.Append("</tr>");

                                    EditLeave.Controls.Add(new LiteralControl(projectHtml.ToString()));
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }



        }

        protected void txtname_TextChanged(object sender, EventArgs e)
        {

            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "SELECT LeavesStatus22.*, Employees.FirstName , Employees.Image FROM LeavesStatus22 JOIN Employees ON LeavesStatus22.EmployeedID = Employees.EmpId WHERE YEAR(LeavesStatus22.FromDate) = YEAR(GETDATE()) AND MONTH(LeavesStatus22.FromDate) = MONTH(GETDATE()) AND Employees.FirstName LIKE '%" + txtname.Text + "%' ORDER BY LeavesStatus22.FromDate,LeavesStatus22.LeaveId DESC; ";
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            EditLeave.Controls.Clear();
                            while (reader.Read())
                            {
                                string Name = reader["FirstName"].ToString();

                                byte[] imageData = (byte[])reader["Image"];
                                string base64String = Convert.ToBase64String(imageData);
                                string imageUrl = "data:image/jpeg;base64," + base64String;

                                string LeaveId = reader["LeaveId"].ToString();

                                string leavetype = reader["leave_type"].ToString();
                                DateTime formattedFromDate = (DateTime)reader["FromDate"];
                                string FromDate = formattedFromDate.ToString("yyyy-MM-dd");

                                DateTime toDate = (DateTime)reader["ToDate"];
                                string ToDate = toDate.ToString("yyyy-MM-dd");
                                string Duration = reader["Duration"].ToString();
                                string Reason = reader["ReasontoApply"].ToString();
                                string Status = reader["Status"].ToString();
                                string EmpId = reader["EmployeedID"].ToString();

                                StringBuilder projectHtml = new StringBuilder();
                                projectHtml.Append("<tr>");
                                projectHtml.Append("<td><a href='profile.html' class='avatar'><img src=" + imageUrl + " alt='User Image'></a></td>");
                                projectHtml.Append("<td><a href='profile.html'>" + Name + " </a></td>");
                                //  projectHtml.Append("<td>" + LeaveId + "</td>");
                                projectHtml.Append("<td style='color:blue;'>" + leavetype + "</td>");
                                projectHtml.Append("<td>" + FromDate + "</td>");
                                projectHtml.Append("<td>" + ToDate + "</td>");
                                projectHtml.Append("<td>" + Duration + "</td>");
                                projectHtml.Append("<td>" + Reason + "</td>");
                                projectHtml.Append("<td>" + Status + "</td>");

                                if (Status.Trim() == "Approved" || Status.Trim() == "Rejected")
                                {

                                }
                                else
                                {
                                    projectHtml.Append("<td><a class='dropdown - item' href='#' data-bs-toggle='modal' data-bs-target='#edit_leave'onclick =\"editLeave('" + Name + "','" + LeaveId + "','" + leavetype + "','" + FromDate + "','" + ToDate + "','" + Duration + "','" + Reason + "','"+ EmpId + "')\" ><i class='fa-solid fa-pencil m-r-5' style='color:#057a28;'></i></a></td>");
                                                                                                                                                                         
                                }
                                projectHtml.Append("</tr>");

                                EditLeave.Controls.Add(new LiteralControl(projectHtml.ToString()));
                                ddlLeaveType.SelectedItem.Text = "";
                                ddlLeaveStatus.SelectedItem.Text = "";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnsearch_Click(object sender, EventArgs e)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;

                        command.CommandText = "SELECT LeavesStatus22.*, Employees.FirstName , Employees.Image FROM LeavesStatus22 JOIN Employees ON LeavesStatus22.EmployeedID = Employees.EmpId WHERE LeavesStatus22.FromDate  >='" + TextBox1.Text + "' AND LeavesStatus22.FromDate <= '" + TextBox2.Text + "' OR  LeavesStatus22.ToDate >='" + TextBox1.Text + "' AND LeavesStatus22.ToDate <= '" + TextBox2.Text + "'  ORDER BY LeavesStatus22.FromDate,LeavesStatus22.LeaveId DESC;";

                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            EditLeave.Controls.Clear();
                            if (DateTime.Parse(TextBox1.Text) <= DateTime.Parse(TextBox2.Text))
                            {
                                while (reader.Read())
                                {
                                    string Name = reader["FirstName"].ToString();

                                    byte[] imageData = (byte[])reader["Image"];
                                    string base64String = Convert.ToBase64String(imageData);
                                    string imageUrl = "data:image/jpeg;base64," + base64String;

                                    string LeaveId = reader["LeaveId"].ToString();

                                    string leavetype = reader["leave_type"].ToString();
                                    DateTime formattedFromDate = (DateTime)reader["FromDate"];
                                    string FromDate = formattedFromDate.ToString("yyyy-MM-dd");

                                    DateTime toDate1 = (DateTime)reader["ToDate"];
                                    string ToDate = toDate1.ToString("yyyy-MM-dd");
                                    string Duration = reader["Duration"].ToString();
                                    string Reason = reader["ReasontoApply"].ToString();
                                    string Status = reader["StatusReason"].ToString();
                                    string EmpId = reader["EmployeedID"].ToString();

                                    StringBuilder projectHtml = new StringBuilder();
                                    projectHtml.Append("<tr>");
                                    projectHtml.Append("<td><a href='profile.html' class='avatar'><img src=" + imageUrl + " alt='User Image'></a>");
                                    projectHtml.Append("<a href='profile.html'>" + Name + " </a></td>");
                                    //  projectHtml.Append("<td>" + LeaveId + "</td>");
                                    projectHtml.Append("<td style='color:blue;'>" + leavetype + "</td>");
                                    projectHtml.Append("<td>" + FromDate + "</td>");
                                    projectHtml.Append("<td>" + ToDate + "</td>");
                                    projectHtml.Append("<td>" + Duration + "</td>");
                                    projectHtml.Append("<td>" + Reason + "</td>");
                                    projectHtml.Append("<td>" + Status + "</td>");
                                    if (Status.Trim() == "Approved" || Status.Trim() == "Rejected")
                                    {

                                    }
                                    else
                                    {
                                        projectHtml.Append("<td><a class='dropdown - item' href='#' data-bs-toggle='modal' data-bs-target='#edit_leave'onclick =\"editLeave('" + Name + "','" + LeaveId + "','" + leavetype + "','" + FromDate + "','" + ToDate + "','" + Duration + "','" + Reason + "','" + EmpId + "')\" ><i class='fa-solid fa-pencil m-r-5' style='color:#057a28;'></i></a></td>");

                                    }

                                    projectHtml.Append("</tr>");

                                    EditLeave.Controls.Add(new LiteralControl(projectHtml.ToString()));
                                    ddlLeaveType.SelectedItem.Text = "";
                                    ddlLeaveStatus.SelectedItem.Text = "";
                                }
                            }
                            else
                            {
                                Response.Write("<script>alert('Invalid date formate ....')</script>");
                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnacceptleave_Click(object sender, EventArgs e)
        {
            try
            {
                string leaveId = hdnLeaveId.Value;
                DateTime fromDate = DateTime.Parse(txtedfromdt.Text);
                DateTime toDate = DateTime.Parse(txtedtodt.Text);
                TimeSpan difference = toDate - fromDate;
                double durationInDays = difference.TotalDays + 1;
                string Status = "1";
                string StatusReason = "Approved";
                string connectionString = ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "UPDATE LeavesStatus22 SET FromDate=@FromDate, ToDate=@ToDate, Duration=@Duration, StatusReason=@StatusReason, ReasontoApply=@ReasontoApply, Status=@Status, StatusDate=@StatusDate, approvedby=@approvedby, depthead_id=@depthead_id, ApprovedByName=@ApprovedByName WHERE LeaveId=@LeaveId";
                        command.Parameters.AddWithValue("@FromDate", fromDate);
                        command.Parameters.AddWithValue("@ToDate", toDate);
                        command.Parameters.AddWithValue("@Duration", durationInDays);
                        command.Parameters.AddWithValue("@StatusReason", StatusReason);
                        command.Parameters.AddWithValue("@ReasontoApply", txtedreason.Text);
                        command.Parameters.AddWithValue("@Status", Status);
                        command.Parameters.AddWithValue("@StatusDate", DateTime.Now);

                        // Handle approvedby and depthead_id separately
                        if (long.TryParse(Session["EmpId"].ToString(), out long approvedBy))
                        {
                            command.Parameters.AddWithValue("@approvedby", approvedBy);
                        }
                        else
                        {
                            throw new Exception("Invalid EmpId format.");
                        }

                        command.Parameters.AddWithValue("@depthead_id", Session["EmpId"].ToString());
                        command.Parameters.AddWithValue("@ApprovedByName", Session["Name"].ToString());
                        command.Parameters.AddWithValue("@LeaveId", leaveId);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            // Update LeavesList
                            Response.Write("<script>alert('Approved Suucessfully....')</script>");
                            LeavesCount();
                            BindAllLeaveData();
                        }
                        else
                        {
                            Response.Write("<script>alert('No rows were updated.')</script>");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('An error occurred while updating the database. Please try again later.')</script>");
                // Log the exception or handle it as needed
                throw ex;
            }
        }

        protected void btnreject_Click(object sender, EventArgs e)
        {
            try
            {
                string leaveId = hdnLeaveId.Value;
                DateTime fromDate = DateTime.Parse(txtedfromdt.Text);
                DateTime toDate = DateTime.Parse(txtedtodt.Text);
                double durationInDays = (toDate - fromDate).TotalDays + 1;
                string status = "1";
                string statusReason = "Rejected";
                string connectionString = ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlTransaction transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            // Update LeavesStatus22 Table
                            using (SqlCommand command = new SqlCommand())
                            {
                                command.Connection = connection;
                                command.Transaction = transaction;
                                command.CommandText = @"UPDATE LeavesStatus22 
                                            SET FromDate=@FromDate, ToDate=@ToDate, Duration=@Duration, 
                                                StatusReason=@StatusReason, ReasontoApply=@ReasontoApply, 
                                                Status=@Status, StatusDate=@StatusDate, 
                                                CL = CL - @decrCL, BalanceLeaves = BalanceLeaves + @IncreseCL, 
                                                SL = SL - @decrSL, BalanceSLeaves = BalanceSLeaves + @IncreseSL, 
                                                ML = ML - @DecrCompOff, BalanceCompOffLeaves = BalanceCompOffLeaves + @IncreseCompOff, 
                                                approvedby=@approvedby, depthead_id=@depthead_id, ApprovedByName=@ApprovedByName 
                                            WHERE LeaveId=@LeaveId";
                                command.Parameters.AddWithValue("@FromDate", fromDate);
                                command.Parameters.AddWithValue("@ToDate", toDate);
                                command.Parameters.AddWithValue("@Duration", durationInDays);
                                command.Parameters.AddWithValue("@StatusReason", statusReason);
                                command.Parameters.AddWithValue("@ReasontoApply", txtedreason.Text);
                                command.Parameters.AddWithValue("@Status", status);
                                command.Parameters.AddWithValue("@StatusDate", DateTime.Now);
                                command.Parameters.AddWithValue("@LeaveId", leaveId);

                                // Initialize the parameters with default values
                                command.Parameters.AddWithValue("@IncreseCL", 0.0);
                                command.Parameters.AddWithValue("@decrCL", 0.0);
                                command.Parameters.AddWithValue("@IncreseSL", 0.0);
                                command.Parameters.AddWithValue("@decrSL", 0.0);
                                command.Parameters.AddWithValue("@IncreseCompOff", 0.0);
                                command.Parameters.AddWithValue("@DecrCompOff", 0.0);

                                // Determine the leave type and set the correct values
                                double leaveDuration = Convert.ToDouble(txteddur.Text);
                                switch (txtedleavtype.Text)
                                {
                                    case "Casual Leave":
                                        command.Parameters["@IncreseCL"].Value = leaveDuration;
                                        command.Parameters["@decrCL"].Value = leaveDuration;
                                        break;
                                    case "Sick Leave":
                                        command.Parameters["@IncreseSL"].Value = leaveDuration;
                                        command.Parameters["@decrSL"].Value = leaveDuration;
                                        break;
                                    case "Comp-Off Leave":
                                        command.Parameters["@IncreseCompOff"].Value = leaveDuration;
                                        command.Parameters["@DecrCompOff"].Value = leaveDuration;
                                        break;
                                }

                                // Handle approvedby and depthead_id separately
                                if (long.TryParse(Session["EmpId"].ToString(), out long approvedBy))
                                {
                                    command.Parameters.AddWithValue("@approvedby", approvedBy);
                                }
                                else
                                {
                                    throw new Exception("Invalid EmpId format.");
                                }
                                command.Parameters.AddWithValue("@depthead_id", Session["EmpId"].ToString());
                                command.Parameters.AddWithValue("@ApprovedByName", Session["Name"].ToString());

                                int rowsAffected = command.ExecuteNonQuery();
                                if (rowsAffected <= 0)
                                {
                                    throw new Exception("No rows were updated in LeavesStatus22.");
                                }
                            }

                            // Update LeavesList Table
                            using (SqlCommand updateCommand = new SqlCommand())
                            {
                                updateCommand.Connection = connection;
                                updateCommand.Transaction = transaction;
                                updateCommand.CommandText = @"UPDATE LeavesList 
                                                  SET UsedCasualLeaves = UsedCasualLeaves - @IncreseCL, 
                                                      BalenceCasualLeaves = BalenceCasualLeaves + @decrCL, 
                                                      UsedSickLeaves = UsedSickLeaves - @IncreseSL, 
                                                      BalenceSickLaves = BalenceSickLaves + @decrSL, 
                                                      UsedCampOffLeaves = UsedCampOffLeaves - @IncreseCompOff, 
                                                      BalenceCampOffLeaves = BalenceCampOffLeaves + @DecrCompOff 
                                                  WHERE EmpId=@EmpId AND Year=@Year";

                                // Reuse the parameter values set earlier
                                updateCommand.Parameters.AddWithValue("@IncreseCL", 0.0);
                                updateCommand.Parameters.AddWithValue("@decrCL", 0.0);
                                updateCommand.Parameters.AddWithValue("@IncreseSL", 0.0);
                                updateCommand.Parameters.AddWithValue("@decrSL", 0.0);
                                updateCommand.Parameters.AddWithValue("@IncreseCompOff", 0.0);
                                updateCommand.Parameters.AddWithValue("@DecrCompOff", 0.0);
                                double leaveDuration = Convert.ToDouble(txteddur.Text);
                                switch (txtedleavtype.Text)
                                {
                                    case "Casual Leave":
                                        updateCommand.Parameters["@IncreseCL"].Value = leaveDuration;
                                        updateCommand.Parameters["@decrCL"].Value = leaveDuration;
                                        break;
                                    case "Sick Leave":
                                        updateCommand.Parameters["@IncreseSL"].Value = leaveDuration;
                                        updateCommand.Parameters["@decrSL"].Value = leaveDuration;
                                        break;
                                    case "Comp-Off Leave":
                                        updateCommand.Parameters["@IncreseCompOff"].Value = leaveDuration;
                                        updateCommand.Parameters["@DecrCompOff"].Value = leaveDuration;
                                        break;
                                }

                                updateCommand.Parameters.AddWithValue("@EmpId", HiddenField1.Value);
                                updateCommand.Parameters.AddWithValue("@Year", DateTime.Now.Year);

                                int updateLeaves = updateCommand.ExecuteNonQuery();
                                if (updateLeaves <= 0)
                                {
                                    throw new Exception("No leaves were updated in LeavesList.");
                                }
                            }

                            // Commit the transaction
                            transaction.Commit();
                            Response.Write("<script>alert('Leave rejected successfully.')</script>");
                            BindAllLeaveData(); 
                            LeavesCount(); 
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            Response.Write("<script>alert('An error occurred while updating the database. Please try again later.')</script>");
                            throw ex;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('An unexpected error occurred. Please contact support.')</script>");
                throw ex;
            }

        }
    }
}