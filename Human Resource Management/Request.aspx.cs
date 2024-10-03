using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Human_Resource_Management
{
    public partial class Request : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindPendingLeaves();
                BindApprovedLeaves();
                BindRejectedLeaves();
                BindCount();
            }
        }
        private void BindPendingLeaves()
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = @"
                    SELECT 
                        LeavesStatus22.*, Emp.FirstName AS FirstName,
                        Emp.Image AS EmployeeImage,
                        ApplyToEmp.Image AS ApplyToImage, 
                        CCToEmp.Image AS CCToImage
                    FROM LeavesStatus22 
                    JOIN Employees AS Emp ON LeavesStatus22.EmployeedID = Emp.EmpId 
                    JOIN Employees AS ApplyToEmp ON LeavesStatus22.ApplyTo = ApplyToEmp.EmpId 
                    JOIN Employees AS CCToEmp ON LeavesStatus22.CCTo = CCToEmp.EmpId 
                    WHERE YEAR(LeavesStatus22.FromDate) = YEAR(GETDATE()) 
                    AND MONTH(LeavesStatus22.FromDate) = MONTH(GETDATE()) 
                    AND LeavesStatus22.StatusReason = 'Pending'
                    ORDER BY LeavesStatus22.FromDate, LeavesStatus22.LeaveId DESC;";

                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            LeavePendingData.Controls.Clear(); // Clear previous controls

                            while (reader.Read())
                            {
                                string FirstName = reader["FirstName"].ToString().ToUpper();
                                string name = FirstName.Length > 10 ? FirstName.Substring(0, 10) : FirstName;

                                string leaveId = reader["LeaveId"].ToString();
                                string leaveType = reader["leave_type"].ToString();
                                string duration = reader["Duration"].ToString();
                                string reason = reader["ReasontoApply"].ToString();
                                string status = reader["StatusReason"].ToString().Trim();
                                string empId = reader["EmployeedID"].ToString();

                                DateTime formattedFromDate = (DateTime)reader["FromDate"];
                                string fromDate = formattedFromDate.ToString("MMM-dd");

                                DateTime toDate = (DateTime)reader["ToDate"];
                                string toDateStr = toDate.ToString("MMM-dd");

                                // Image handling for EmployeeID
                                object empImageObj = reader["EmployeeImage"];
                                byte[] empImageData = empImageObj != DBNull.Value ? (byte[])empImageObj : new byte[0];
                                string empImageUrl = "data:image/jpeg;base64," + Convert.ToBase64String(empImageData);

                                // Image handling for ApplyTo
                                object applyToImageObj = reader["ApplyToImage"];
                                byte[] applyToImageData = applyToImageObj != DBNull.Value ? (byte[])applyToImageObj : new byte[0];
                                string applyToImageUrl = "data:image/jpeg;base64," + Convert.ToBase64String(applyToImageData);

                                // Image handling for CCTo
                                object ccToImageObj = reader["CCToImage"];
                                byte[] ccToImageData = ccToImageObj != DBNull.Value ? (byte[])ccToImageObj : new byte[0];
                                string ccToImageUrl = "data:image/jpeg;base64," + Convert.ToBase64String(ccToImageData);

                                // Build the HTML dynamically
                                StringBuilder projectHtml = new StringBuilder();
                                projectHtml.Append("<div class='firs-req'>");

                                // First-side (Employee info)
                                projectHtml.Append("<div class='first-side'>");
                                projectHtml.Append($"<img src='{empImageUrl}' alt='Employee' style='width:50px;height:50px;border-radius:50%;' />");
                                projectHtml.Append($"<span>{name}</span>");
                                projectHtml.Append($"<span>{empId}</span>");
                                projectHtml.Append("</div>"); // Close first-side

                                // Second-side (Leave details and images)
                                projectHtml.Append("<div class='second-side'>");
                                projectHtml.Append($"<span>{leaveType}</span>");
                                projectHtml.Append($"<span>{fromDate} - {toDateStr} ({duration})</span>");
                                projectHtml.Append($"<span>{reason}</span>");
                                projectHtml.Append("<span>");
                                projectHtml.Append($"<img src='{applyToImageUrl}' alt='ApplyTo' style='width:30px;height:30px;border-radius:50%;' />");
                                projectHtml.Append($"<img src='{ccToImageUrl}' alt='CCTo' style='width:30px;height:30px;border-radius:50%;' />");
                                projectHtml.Append("</span>");
                                projectHtml.Append("</div>"); // Close second-side

                                // Third-side (Edit link)
                                projectHtml.Append("<div class='third-edit'>");
                                projectHtml.Append($"<a href='#' class='edit-link' onclick=\"editPendingLeave('{FirstName}','{leaveId}', '{leaveType}', '{fromDate}', '{toDateStr}', '{duration}', '{reason}', '{status}');\"><i class='fa-solid fa-pencil m-r-5' style='color:green;'></i></a>");
                                projectHtml.Append("</div>"); // Close third-edit

                                projectHtml.Append("</div>"); // Close firs-req

                                // Add the generated HTML to the PlaceHolder control
                                LeavePendingData.Controls.Add(new LiteralControl(projectHtml.ToString()));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
               
                throw ex; // Preserve the original stack trace
            }
        }



        private void BindApprovedLeaves()
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = @"
                    SELECT 
                        LeavesStatus22.*, Emp.FirstName AS FirstName,
                        Emp.Image AS EmployeeImage,
                        ApplyToEmp.Image AS ApplyToImage, 
                        CCToEmp.Image AS CCToImage
                    FROM LeavesStatus22 
                    JOIN Employees AS Emp ON LeavesStatus22.EmployeedID = Emp.EmpId 
                    JOIN Employees AS ApplyToEmp ON LeavesStatus22.approvedby = ApplyToEmp.EmpId 
                    JOIN Employees AS CCToEmp ON LeavesStatus22.CCTo = CCToEmp.EmpId 
                    WHERE YEAR(LeavesStatus22.FromDate) = YEAR(GETDATE()) 
                    AND MONTH(LeavesStatus22.FromDate) = MONTH(GETDATE()) 
                    AND LeavesStatus22.StatusReason = 'Approved'
                    ORDER BY LeavesStatus22.FromDate, LeavesStatus22.LeaveId DESC;";

                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            LeaveApproveData.Controls.Clear(); // Clear previous controls

                            while (reader.Read())
                            {
                                string FirstName = reader["FirstName"].ToString().ToUpper();
                                string name = FirstName.Length > 10 ? FirstName.Substring(0, 10) : FirstName;

                                string leaveId = reader["LeaveId"].ToString();
                                string leaveType = reader["leave_type"].ToString();
                                string duration = reader["Duration"].ToString();
                                string reason = reader["ReasontoApply"].ToString();
                                string status = reader["StatusReason"].ToString();
                                string empId = reader["EmployeedID"].ToString();

                                DateTime formattedFromDate = (DateTime)reader["FromDate"];
                                string fromDate = formattedFromDate.ToString("MM-dd");

                                DateTime toDate = (DateTime)reader["ToDate"];
                                string toDateStr = toDate.ToString("MM-dd");

                                // Image handling for EmployeeID
                                object empImageObj = reader["EmployeeImage"];
                                byte[] empImageData = empImageObj != DBNull.Value ? (byte[])empImageObj : new byte[0];
                                string empImageUrl = "data:image/jpeg;base64," + Convert.ToBase64String(empImageData);

                                // Image handling for ApplyTo
                                object applyToImageObj = reader["ApplyToImage"];
                                byte[] applyToImageData = applyToImageObj != DBNull.Value ? (byte[])applyToImageObj : new byte[0];
                                string applyToImageUrl = "data:image/jpeg;base64," + Convert.ToBase64String(applyToImageData);

                                // Image handling for CCTo
                                object ccToImageObj = reader["CCToImage"];
                                byte[] ccToImageData = ccToImageObj != DBNull.Value ? (byte[])ccToImageObj : new byte[0];
                                string ccToImageUrl = "data:image/jpeg;base64," + Convert.ToBase64String(ccToImageData);

                                // Build the HTML dynamically
                                StringBuilder projectHtml = new StringBuilder();
                                projectHtml.Append("<div class='firs-req'>");

                                // First-side (Employee info)
                                projectHtml.Append("<div class='first-side'>");
                                projectHtml.Append($"<img src='{empImageUrl}' alt='Employee' style='width:50px;height:50px;border-radius:50%;' />");
                                projectHtml.Append($"<span>{name}</span>");
                                projectHtml.Append($"<span>{empId}</span>");
                                projectHtml.Append("</div>"); // Close first-side

                                // Second-side (Leave details and images)
                                projectHtml.Append("<div class='second-side'>");
                                projectHtml.Append($"<span>{leaveType}</span>");
                                projectHtml.Append($"<span>{fromDate} - {toDateStr} ({duration})</span>");
                                projectHtml.Append($"<span>{reason}</span>");
                                projectHtml.Append("<span>");
                                projectHtml.Append($"<img src='{applyToImageUrl}' alt='ApplyTo' style='width:30px;height:30px;border-radius:50%;' />");
                               // projectHtml.Append($"<img src='{ccToImageUrl}' alt='CCTo' style='width:30px;height:30px;border-radius:50%;' />");
                                projectHtml.Append("</span>");
                                projectHtml.Append("</div>"); // Close second-side

                                // Third-side (Edit link)
                                projectHtml.Append("<div class='third-edit'>");
                               // projectHtml.Append($"<a href='#' class='edit-link' onclick=\"editLeave('{FirstName}','{leaveId}', '{leaveType}', '{fromDate}', '{toDateStr}', '{duration}', '{reason}', '{status}');\"><i class='fa-solid fa-pencil m-r-5' style='color:green;'></i></a>");
                                projectHtml.Append("</div>"); // Close third-edit

                                projectHtml.Append("</div>"); // Close firs-req

                                // Add the generated HTML to the PlaceHolder control
                                LeaveApproveData.Controls.Add(new LiteralControl(projectHtml.ToString()));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex; // Preserve the original stack trace
            }
        }


        private void BindRejectedLeaves()
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = @"
                    SELECT 
                        LeavesStatus22.*, Emp.FirstName AS FirstName,
                        Emp.Image AS EmployeeImage,
                        ApplyToEmp.Image AS ApplyToImage, 
                        CCToEmp.Image AS CCToImage
                    FROM LeavesStatus22 
                    JOIN Employees AS Emp ON LeavesStatus22.EmployeedID = Emp.EmpId 
                    JOIN Employees AS ApplyToEmp ON LeavesStatus22.approvedby = ApplyToEmp.EmpId 
                    JOIN Employees AS CCToEmp ON LeavesStatus22.CCTo = CCToEmp.EmpId 
                    WHERE YEAR(LeavesStatus22.FromDate) = YEAR(GETDATE()) 
                    AND MONTH(LeavesStatus22.FromDate) = MONTH(GETDATE()) 
                    AND LeavesStatus22.StatusReason = 'Rejected'
                    ORDER BY LeavesStatus22.FromDate, LeavesStatus22.LeaveId DESC;";

                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            LeaveRejectedData.Controls.Clear(); // Clear previous controls

                            while (reader.Read())
                            {
                                string FirstName = reader["FirstName"].ToString().ToUpper();
                                string name = FirstName.Length > 10 ? FirstName.Substring(0, 10) : FirstName;

                                string leaveId = reader["LeaveId"].ToString();
                                string leaveType = reader["leave_type"].ToString();
                                string duration = reader["Duration"].ToString();
                                string reason = reader["ReasontoApply"].ToString();
                                string status = reader["StatusReason"].ToString();
                                string empId = reader["EmployeedID"].ToString();

                                DateTime formattedFromDate = (DateTime)reader["FromDate"];
                                string fromDate = formattedFromDate.ToString("MM-dd");

                                DateTime toDate = (DateTime)reader["ToDate"];
                                string toDateStr = toDate.ToString("MM-dd");

                                // Image handling for EmployeeID
                                object empImageObj = reader["EmployeeImage"];
                                byte[] empImageData = empImageObj != DBNull.Value ? (byte[])empImageObj : new byte[0];
                                string empImageUrl = "data:image/jpeg;base64," + Convert.ToBase64String(empImageData);

                                // Image handling for ApplyTo
                                object applyToImageObj = reader["ApplyToImage"];
                                byte[] applyToImageData = applyToImageObj != DBNull.Value ? (byte[])applyToImageObj : new byte[0];
                                string applyToImageUrl = "data:image/jpeg;base64," + Convert.ToBase64String(applyToImageData);

                                // Image handling for CCTo
                                object ccToImageObj = reader["CCToImage"];
                                byte[] ccToImageData = ccToImageObj != DBNull.Value ? (byte[])ccToImageObj : new byte[0];
                                string ccToImageUrl = "data:image/jpeg;base64," + Convert.ToBase64String(ccToImageData);

                                // Build the HTML dynamically
                                StringBuilder projectHtml = new StringBuilder();
                                projectHtml.Append("<div class='firs-req'>");

                                // First-side (Employee info)
                                projectHtml.Append("<div class='first-side'>");
                                projectHtml.Append($"<img src='{empImageUrl}' alt='Employee' style='width:50px;height:50px;border-radius:50%;' />");
                                projectHtml.Append($"<span>{name}</span>");
                                projectHtml.Append($"<span>{empId}</span>");
                                projectHtml.Append("</div>"); // Close first-side

                                // Second-side (Leave details and images)
                                projectHtml.Append("<div class='second-side'>");
                                projectHtml.Append($"<span>{leaveType}</span>");
                                projectHtml.Append($"<span>{fromDate} - {toDateStr} ({duration})</span>");
                                projectHtml.Append($"<span>{reason}</span>");
                                projectHtml.Append("<span>");
                                projectHtml.Append($"<img src='{applyToImageUrl}' alt='ApplyTo' style='width:30px;height:30px;border-radius:50%;' />");
                               // projectHtml.Append($"<img src='{ccToImageUrl}' alt='CCTo' style='width:30px;height:30px;border-radius:50%;' />");
                                projectHtml.Append("</span>");
                                projectHtml.Append("</div>"); // Close second-side

                                // Third-side (Edit link)
                                projectHtml.Append("<div class='third-edit'>");
                               // projectHtml.Append($"<a href='#' class='edit-link' onclick=\"editLeave('{FirstName}','{leaveId}', '{leaveType}', '{fromDate}', '{toDateStr}', '{duration}', '{reason}', '{status}');\"><i class='fa-solid fa-pencil m-r-5' style='color:green;'></i></a>");
                                projectHtml.Append("</div>"); // Close third-edit

                                projectHtml.Append("</div>"); // Close firs-req

                                // Add the generated HTML to the PlaceHolder control
                                LeaveRejectedData.Controls.Add(new LiteralControl(projectHtml.ToString()));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex; // Preserve the original stack trace
            }
        }


        public void BindCount()
        {
            try
            {
                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    connection.Open();

                    int currentYear = DateTime.Now.Year;
                    int currentMonth = DateTime.Now.Month;

                    // Use parameterized queries to avoid SQL injection
                    string query = "SELECT StatusReason, COUNT(*) AS Count FROM LeavesStatus22 " +
                                   "WHERE StatusReason IN ('Pending', 'Approved', 'Rejected') " +
                                   "AND YEAR(FromDate) = @Year AND MONTH(FromDate) = @Month " +
                                   "GROUP BY StatusReason";

                    // Use a dictionary to hold counts for each status
                    var counts = new Dictionary<string, int>
            {
                { "Pending", 0 },
                { "Approved", 0 },
                { "Rejected", 0 }
            };

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Year", currentYear);
                        command.Parameters.AddWithValue("@Month", currentMonth);

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string statusReason = reader.GetString(0).Trim();
                                int count = reader.GetInt32(1);

                                // Update the count for the relevant status
                                if (counts.ContainsKey(statusReason))
                                {
                                    counts[statusReason] = count;
                                }
                            }
                        }
                    }

                    // Set the label text based on the counts
                    Label1.Text = counts["Pending"].ToString();
                    Label2.Text = counts["Approved"].ToString();
                    Label3.Text = counts["Rejected"].ToString();
                }
            }
            catch (Exception ex)
            {
                
                throw ex; // Preserve the original stack trace
            }
        }
    }
}