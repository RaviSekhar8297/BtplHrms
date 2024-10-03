using DocumentFormat.OpenXml.Bibliography;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TreeView;

namespace Human_Resource_Management
{
    public partial class AdminAttendanceModify : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindAttendanceData();              
            }
        }
        public void BindAttendanceData(string name = null, DateTime? selectedDate = null)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString;

                using (var connection = new SqlConnection(connectionString))
                {
                    StringBuilder query = new StringBuilder(@"
                SELECT E.EmpId, E.FirstName,
                       CONVERT(VARCHAR(10), A.DateOfTransaction, 120) AS [Date],
                       MIN(CONVERT(VARCHAR(5), A.DateOfTransaction, 108)) AS InTime,
                       MAX(CONVERT(VARCHAR(5), A.DateOfTransaction, 108)) AS OutTime
                FROM Employees E
                JOIN Attendance A ON E.EmpId = A.UserId
                WHERE 1=1");

                    // Add filters for name and date if provided
                    if (!string.IsNullOrEmpty(name))
                    {
                        query.Append(" AND E.FirstName LIKE @Name");
                    }
                    if (selectedDate.HasValue)
                    {
                        query.Append(" AND CONVERT(VARCHAR(10), A.DateOfTransaction, 120) = @Date");
                    }
                    else
                    {
                        query.Append(" AND CONVERT(VARCHAR(10), A.DateOfTransaction, 120) = CONVERT(VARCHAR(10), GETDATE(), 120)");
                    }

                    query.Append(@"
                GROUP BY E.EmpId, E.FirstName, CONVERT(VARCHAR(10), A.DateOfTransaction, 120)
                ORDER BY E.EmpId");

                    using (var sqlCommand = new SqlCommand(query.ToString(), connection))
                    {
                        if (!string.IsNullOrEmpty(name))
                        {
                            sqlCommand.Parameters.AddWithValue("@Name", "%" + name + "%");
                        }
                        if (selectedDate.HasValue)
                        {
                            sqlCommand.Parameters.AddWithValue("@Date", selectedDate.Value.ToString("yyyy-MM-dd"));
                        }

                        connection.Open();
                        using (var sqlDataReader = sqlCommand.ExecuteReader())
                        {
                            StringBuilder sb = new StringBuilder();
                            while (sqlDataReader.Read())
                            {
                                // Retrieve the data
                                string firstName = sqlDataReader["FirstName"].ToString();
                                string empId = sqlDataReader["EmpId"].ToString();
                                string minTime = sqlDataReader["InTime"].ToString(); // No need for DateTime conversion
                                string maxTime = sqlDataReader["OutTime"].ToString(); // No need for DateTime conversion
                                string date = sqlDataReader["Date"].ToString(); // Already a formatted string

                                // Handle image data
                                byte[] imageData = GetEmployeeImage(empId);
                                string imageSrc = string.Empty;
                                if (imageData != null && imageData.Length > 0)
                                {
                                    // Convert byte array to Base64 string
                                    string base64Image = Convert.ToBase64String(imageData);
                                    imageSrc = "data:image/png;base64," + base64Image;
                                }
                                else
                                {
                                    // Provide a default image if none is found
                                    imageSrc = "path/to/default-image.png";
                                }

                                // Construct HTML row
                                sb.Append("<tr>");
                                sb.Append("<td><img src='" + imageSrc + "' alt='Image' style='width:50px;height:50px;border-radius:50%;' /></td>");
                                sb.Append("<td>" + System.Web.HttpUtility.HtmlEncode(firstName) + "</td>");
                                sb.Append("<td>" + System.Web.HttpUtility.HtmlEncode(empId) + "</td>");
                                sb.Append("<td>" + System.Web.HttpUtility.HtmlEncode(date) + "</td>");
                                sb.Append("<td>" + System.Web.HttpUtility.HtmlEncode(minTime) + "</td>");
                                sb.Append("<td>" + System.Web.HttpUtility.HtmlEncode(maxTime) + "</td>");
                                sb.Append("<td><a class='edit' href='#' data-bs-toggle='modal' data-bs-target='#edit_attendance' onclick =\"editAttendance('"
                                          + System.Web.HttpUtility.HtmlEncode(firstName) + "','"
                                          + System.Web.HttpUtility.HtmlEncode(empId) + "','"
                                          + System.Web.HttpUtility.HtmlEncode(date) + "','"
                                          + System.Web.HttpUtility.HtmlEncode(minTime) + "','"
                                          + System.Web.HttpUtility.HtmlEncode(maxTime) + "')\" ><i class='fa-solid fa-pencil m-r-5' style='color:green;'></i></a></td>");
                                sb.Append("</tr>");
                            }

                            // Add the constructed HTML to the PlaceHolder
                            AttendanceModify.Controls.Add(new LiteralControl(sb.ToString()));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('An error occurred: " + ex.Message + "');", true);
            }
        }



        private byte[] GetEmployeeImage(string empId)
        {
            byte[] imageData = null;

            string imageQuery = "SELECT Image FROM Employees WHERE EmpId = @EmpId";

            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
            {
                connection.Open();

                using (SqlCommand sqlCmd = new SqlCommand(imageQuery, connection))
                {
                    sqlCmd.Parameters.AddWithValue("@EmpId", empId);

                    var result = sqlCmd.ExecuteScalar();
                    if (result != DBNull.Value && result != null)
                    {
                        imageData = (byte[])result;
                    }
                }
            }

            return imageData;
        }

        protected void btnupdateattendance_Click(object sender, EventArgs e)
        {
            try
            {
                // Retrieve the values from the TextBoxes
                string empId = txtempid.Text.Trim();
                string inTime = txtintime.Text.Trim();
                string outTime = txtouttime.Text.Trim();
                DateTime date = DateTime.Parse(txtdate.Text.Trim());

                string connectionString = ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString;

                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Update InTime
                    string updateInTimeQuery = @"UPDATE Attendance SET InTimeRemarks=@InTimeRemarks, DateOfTransaction = @InTime
            WHERE UserId = @UserId AND CAST(DateOfTransaction AS DATE) = @Date
            AND DateOfTransaction = (SELECT MIN(DateOfTransaction) FROM Attendance WHERE UserId = @UserId AND CAST(DateOfTransaction AS DATE) = @Date);";

                    using (var command = new SqlCommand(updateInTimeQuery, connection))
                    {
                        command.Parameters.AddWithValue("@InTimeRemarks", "Updated");
                        command.Parameters.AddWithValue("@InTime", date.Add(TimeSpan.Parse(inTime)));
                        command.Parameters.AddWithValue("@UserId", empId); // Use Employee ID from txtempid
                        command.Parameters.AddWithValue("@Date", date);
                        command.ExecuteNonQuery();
                    }

                    // Update OutTime
                    string updateOutTimeQuery = @"UPDATE Attendance SET OutTimeRemarks=@OutTimeRemarks, DateOfTransaction = @OutTime
            WHERE UserId = @UserId AND CAST(DateOfTransaction AS DATE) = @Date AND DateOfTransaction = (
                SELECT MAX(DateOfTransaction) FROM Attendance WHERE UserId = @UserId AND CAST(DateOfTransaction AS DATE) = @Date);";

                    using (var command = new SqlCommand(updateOutTimeQuery, connection))
                    {
                        command.Parameters.AddWithValue("@OutTimeRemarks", "Updated");
                        command.Parameters.AddWithValue("@OutTime", date.Add(TimeSpan.Parse(outTime)));
                        command.Parameters.AddWithValue("@UserId", empId); // Use Employee ID from txtempid
                        command.Parameters.AddWithValue("@Date", date);
                        command.ExecuteNonQuery();
                    }
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Attendance updated successfully.');", true);
                    BindAttendanceData();
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions and optionally show an error message
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('An error occurred: " + ex.Message + "');", true);
            }
        }

        protected void txtdatesearch_TextChanged(object sender, EventArgs e)
        {
            string name = txtempnamesearch.Text.Trim();
            DateTime? selectedDate = null;

            if (!string.IsNullOrEmpty(txtdatesearch.Text))
            {
                selectedDate = DateTime.Parse(txtdatesearch.Text);
            }

            // Bind attendance data based on name and/or selected date
            BindAttendanceData(name, selectedDate);
        }
    }
}