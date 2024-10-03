using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Human_Resource_Management
{
    public partial class Checkinout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BinsCheckinOuts();
            }
        }
        public void BinsCheckinOuts()
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString;
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = @"
            SELECT 
                e.Image,
                e.EmpId,
                e.FirstName AS Name,
                CONVERT(varchar, a.DateOfTransaction, 108) AS TransactionTime
            FROM Employees e
            LEFT JOIN Attendance a ON e.EmpId = a.UserId
            WHERE CAST(a.DateOfTransaction AS DATE) = CAST(GETDATE() AS DATE)
            ORDER BY e.EmpId, a.DateOfTransaction;
        ";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        // Dictionary to hold employee data
                        var employeeData = new Dictionary<int, List<string>>();
                        var employeeImages = new Dictionary<int, string>(); // Store Base64 image strings
                        var maxTransactions = new Dictionary<int, int>();

                        // Process the DataTable
                        foreach (DataRow row in dt.Rows)
                        {
                            int empId = (int)row["EmpId"];
                            string transactionTime = (string)row["TransactionTime"];
                            byte[] imageBytes = row["Image"] != DBNull.Value ? (byte[])row["Image"] : null;
                            string imageBase64 = imageBytes != null ? Convert.ToBase64String(imageBytes) : null;

                            if (!employeeData.ContainsKey(empId))
                            {
                                employeeData[empId] = new List<string>();
                                if (imageBase64 != null)
                                {
                                    employeeImages[empId] = $"data:image/png;base64,{imageBase64}"; // Adjust MIME type if necessary
                                }
                                else
                                {
                                    employeeImages[empId] = "data:image/png;base64,default_image_base64"; // Use a default image if none is available
                                }
                            }

                            employeeData[empId].Add(transactionTime);
                            maxTransactions[empId] = Math.Max(maxTransactions.ContainsKey(empId) ? maxTransactions[empId] : 0, employeeData[empId].Count);
                        }

                        // Find the maximum number of transactions across all employees
                        int globalMaxTransactions = maxTransactions.Values.Max();

                        // Build the HTML table
                        StringBuilder html = new StringBuilder();
                        html.Append("<table class='table table-striped custom-table mb-0 datatable'>");
                        html.Append("<thead>");
                        html.Append("<tr>");
                        html.Append("<th>Image</th>"); // Add Image column header
                        html.Append("<th>EmpId</th>");
                        html.Append("<th>FirstName</th>");

                        // Dynamically generate headers for CheckIn/CheckOut based on the maximum number of punches
                        for (int i = 1; i <= globalMaxTransactions / 2; i++)  // Since each "In" has an "Out", we divide by 2
                        {
                            html.Append($"<th>In</th>");
                            html.Append($"<th>Out</th>");
                        }

                        html.Append("</tr>");
                        html.Append("</thead>");
                        html.Append("<tbody>");

                        foreach (var emp in employeeData)
                        {
                            int empId = emp.Key;
                            List<string> times = emp.Value;

                            // Get the image URL
                            string imageUrl = employeeImages.ContainsKey(empId) ? employeeImages[empId] : "data:image/png;base64,default_image_base64";
                            string name = GetEmployeeName(empId); // Fetch name directly from the database

                            // Add employee row to the table
                            html.Append("<tr>");
                            html.Append($"<td><img src='{imageUrl}' alt='Image' style='width:50px; height:50px;border-radius:50%;' /></td>"); // Display employee image
                            html.Append($"<td>{empId}</td>");
                            html.Append($"<td>{name}</td>");

                            // Add check-in/check-out times and fill with "-" if missing
                            for (int i = 0; i < globalMaxTransactions / 2; i++)  // Again, dividing by 2 to account for In and Out pairs
                            {
                                // "In" Time
                                if (i * 2 < times.Count)
                                {
                                    html.Append($"<td>{times[i * 2]}</td>");
                                }
                                else
                                {
                                    html.Append("<td>-</td>");
                                }

                                // "Out" Time
                                if ((i * 2 + 1) < times.Count)  // Make sure there's a corresponding "Out" time
                                {
                                    html.Append($"<td>{times[i * 2 + 1]}</td>");
                                }
                                else
                                {
                                    html.Append("<td>-</td>");
                                }
                            }

                            html.Append("</tr>");
                        }

                        // Close the table
                        html.Append("</tbody>");
                        html.Append("</table>");

                        // Add the HTML to the PlaceHolder control
                        CheckInOuts.Controls.Add(new Literal { Text = html.ToString() });
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it accordingly
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        private string GetEmployeeName(int empId)
        {
            // Fetch the employee name from the database
            string connectionString = ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = "SELECT FirstName FROM Employees WHERE EmpId = @EmpId";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@EmpId", empId);
                    object result = cmd.ExecuteScalar();
                    return result != null ? result.ToString() : "Unknown";
                }
            }
        }
    }
}