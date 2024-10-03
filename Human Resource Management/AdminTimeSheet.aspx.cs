using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Human_Resource_Management
{
    public partial class AdminTimeSheet1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                DateTime endDate = DateTime.Today;
                TextBox1.Text = endDate.ToString("yyyy-MM-dd"); // Sets TextBox1 to today's date
                DateTime startDate = endDate.AddDays(-6); // Sets start date to 6 days before today
                TextBox2.Text = startDate.ToString("yyyy-MM-dd"); // Sets TextBox2 to startDate
                BindTimeSheetData(); // Loads data for the initial date range
            }

        }

        public void BindTimeSheetData()
        {
            try
            {
                if (ConfigurationManager.ConnectionStrings["NewHRMSString"] != null)
                {
                    string connectionString = ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString;
                    DateTime firstDayOfMonth = DateTime.ParseExact(TextBox2.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                    DateTime lastDayOfMonth = DateTime.ParseExact(TextBox1.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture);

                    List<string> monthOfDays = new List<string>();
                    for (DateTime date = firstDayOfMonth; date <= lastDayOfMonth; date = date.AddDays(1))
                    {
                        monthOfDays.Add(date.ToString("MMM-dd"));
                    }

                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        string query = @"SELECT A.UserId, E.FirstName, CAST(E.Image AS VARBINARY(MAX)) AS Image,  CONVERT(date,         A.DateOfTransaction) AS TransactionDate, MIN(A.DateOfTransaction) AS InTime,  MAX(A.DateOfTransaction) AS OutTime,  DATEDIFF(MINUTE, MIN(A.DateOfTransaction), MAX(A.DateOfTransaction)) AS TotalMinutes,  YEAR(DateOfTransaction) AS   TransactionYear, MONTH(DateOfTransaction) AS TransactionMonth   FROM Attendance A  JOIN Employees E ON A.UserId = E.EmpId WHERE A.DateOfTransaction >= @FirstDayOfMonth  AND A.DateOfTransaction <= @LastDayOfMonth   AND E.Status = '1'  GROUP BY A.UserId, E.FirstName, CAST(E.Image AS VARBINARY(MAX)), CONVERT(date, A.DateOfTransaction),   YEAR(DateOfTransaction), MONTH(DateOfTransaction)   ORDER BY A.UserId, CONVERT(date, A.DateOfTransaction);";

                        SqlCommand cmd1 = new SqlCommand(query, con);
                        cmd1.Parameters.AddWithValue("@FirstDayOfMonth", firstDayOfMonth);
                        cmd1.Parameters.AddWithValue("@LastDayOfMonth", lastDayOfMonth);

                        con.Open();
                        SqlDataAdapter da = new SqlDataAdapter(cmd1);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        cmd1.CommandTimeout = 9000;

                        StringBuilder htmlBuilder = new StringBuilder();
                        htmlBuilder.Append("<thead>");
                        htmlBuilder.Append("<tr>");
                        htmlBuilder.Append("<th>Image</th>");
                        htmlBuilder.Append("<th>Name </th>");
                        htmlBuilder.Append("<th> Id / Date </th>");
                        foreach (string day in monthOfDays)
                        {
                            DateTime currentDate1 = DateTime.ParseExact(day, "MMM-dd", CultureInfo.InvariantCulture);
                            if (currentDate1.DayOfWeek == DayOfWeek.Sunday)
                            {
                                htmlBuilder.Append("<th style='color:red;'>" + day + "</th>");
                            }
                            else
                            {
                                htmlBuilder.Append("<th>" + day + "</th>");
                            }
                        }
                        htmlBuilder.Append("</tr>");
                        htmlBuilder.Append("</thead>");
                        htmlBuilder.Append("<tbody>");

                        Dictionary<string, Dictionary<string, int>> attendanceData = new Dictionary<string, Dictionary<string, int>>();
                        foreach (DataRow row in dt.Rows)
                        {
                            string userId = row["UserId"] != DBNull.Value ? row["UserId"].ToString() : string.Empty;
                            string transactionDate = row["TransactionDate"] != DBNull.Value ? DateTime.Parse(row["TransactionDate"].ToString()).ToString("MMM-dd") : null;
                            int totalMinutes = row["TotalMinutes"] != DBNull.Value ? Convert.ToInt32(row["TotalMinutes"]) : 0;

                            if (!attendanceData.ContainsKey(userId))
                            {
                                attendanceData[userId] = new Dictionary<string, int>();
                            }
                            if (transactionDate != null)
                            {
                                attendanceData[userId][transactionDate] = totalMinutes;
                            }
                        }

                        var allEmployeeIds = dt.AsEnumerable().Select(row => row["UserId"].ToString()).Distinct().ToList();

                        foreach (var userId in allEmployeeIds)
                        {
                            string userName = dt.AsEnumerable().Where(row => row["UserId"].ToString() == userId).Select(row => row.Field<string>("FirstName")).FirstOrDefault();

                            byte[] empImage = dt.AsEnumerable().Where(row => row["UserId"].ToString() == userId).Select(row => row["Image"] != DBNull.Value ? (byte[])row["Image"] : null).FirstOrDefault();

                            // Null check for empImage
                            string base64Image = empImage != null ? Convert.ToBase64String(empImage) : string.Empty;

                            htmlBuilder.Append("<tr>");
                            htmlBuilder.Append("<td><img src='data:image/jpeg;base64," + base64Image + "' alt='Image' width='50px' height='40px' style='border-radius:15px;' /></td>");
                            htmlBuilder.Append("<td>" + userName + "</td>");
                            htmlBuilder.Append("<td>" + userId + "</td>");

                            foreach (string day in monthOfDays)
                            {
                                htmlBuilder.Append("<td>");
                                if (attendanceData.ContainsKey(userId) && attendanceData[userId].ContainsKey(day))
                                {
                                    int totalMinutes = attendanceData[userId][day];
                                    TimeSpan difference = TimeSpan.FromMinutes(totalMinutes);

                                    int totalHours = (int)difference.TotalHours;
                                    int totalMinutesPart = difference.Minutes;

                                    htmlBuilder.Append($"{totalHours}H {totalMinutesPart}M");
                                }
                                else
                                {
                                    htmlBuilder.Append("-");
                                }
                                htmlBuilder.Append("</td>");
                            }

                            htmlBuilder.Append("</tr>");
                        }

                        htmlBuilder.Append("</tbody>");
                        AttendanceData.Controls.Add(new LiteralControl(htmlBuilder.ToString()));
                    }
                }
                else
                {
                    // Handle case when connection string is null
                }
            }
            catch (Exception ex)
            {
                // Log exception details or handle accordingly
                // For example: LogException(ex); 
                throw ex;
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (ConfigurationManager.ConnectionStrings["NewHRMSString"] != null)
                {
                    string connectionString = ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString;

                    DateTime firstDayOfMonth = DateTime.ParseExact(TextBox2.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                    DateTime lastDayOfMonth = DateTime.ParseExact(TextBox1.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture);

                    if (firstDayOfMonth > lastDayOfMonth)
                    {
                        Response.Write("<script>alert('Please select a valid date range.')</script>");
                        return;
                    }

                    List<string> monthOfDays = new List<string>();
                    for (DateTime date = firstDayOfMonth; date <= lastDayOfMonth; date = date.AddDays(1))
                    {
                        monthOfDays.Add(date.ToString("MMM-dd"));
                    }

                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        string query = @" SELECT TOP (@TopRecords) A.UserId, E.FirstName, CAST(E.Image AS VARBINARY(MAX)) AS Image,  CONVERT(date, A.DateOfTransaction) AS TransactionDate, MIN(A.DateOfTransaction) AS InTime,   MAX(A.DateOfTransaction) AS OutTime, DATEDIFF(MINUTE, MIN(A.DateOfTransaction), MAX(A.DateOfTransaction)) AS TotalMinutes, YEAR(DateOfTransaction) AS TransactionYear, MONTH(DateOfTransaction) AS TransactionMonth  FROM Attendance A JOIN Employees E ON A.UserId = E.EmpId  WHERE A.DateOfTransaction >= @FirstDayOfMonth AND A.DateOfTransaction <= @LastDayOfMonth   AND E.Status = '1' ";

                        SqlCommand cmd1 = new SqlCommand();

                        // Add date parameters
                        cmd1.Parameters.AddWithValue("@FirstDayOfMonth", firstDayOfMonth);
                        cmd1.Parameters.AddWithValue("@LastDayOfMonth", lastDayOfMonth);

                        int topRecords;
                        if (!int.TryParse(TextBox3.Text.Trim(), out topRecords) || topRecords <= 0)
                        {
                            // If TextBox3 is not a valid number or <= 0, fetch all records
                            query += "GROUP BY A.UserId, E.FirstName, CAST(E.Image AS VARBINARY(MAX)), CONVERT(date, A.DateOfTransaction), YEAR(DateOfTransaction), MONTH(DateOfTransaction) ";
                            query += "ORDER BY A.UserId, CONVERT(date, A.DateOfTransaction);";
                            topRecords = int.MaxValue; // Fetch all records
                        }
                        else
                        {
                            // If TextBox3 has a valid number, filter by UserId
                            int enteredUserId;
                            if (!int.TryParse(TextBox3.Text.Trim(), out enteredUserId))
                            {
                                Response.Write("<script>alert('Please enter a valid UserId.')</script>");
                                return; // Exit the method or handle accordingly
                            }
                            query += "AND A.UserId = @EnteredUserId ";
                            cmd1.Parameters.AddWithValue("@EnteredUserId", enteredUserId);
                            query += "GROUP BY A.UserId, E.FirstName, CAST(E.Image AS VARBINARY(MAX)), CONVERT(date, A.DateOfTransaction), YEAR(DateOfTransaction), MONTH(DateOfTransaction) ";
                            query += "ORDER BY A.UserId, CONVERT(date, A.DateOfTransaction);";
                        }

                        cmd1.CommandText = query;
                        cmd1.Connection = con;
                        cmd1.Parameters.AddWithValue("@TopRecords", topRecords);
                        con.Open();
                        SqlDataAdapter da = new SqlDataAdapter(cmd1);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        StringBuilder htmlBuilder = new StringBuilder();
                        htmlBuilder.Append("<thead>");
                        htmlBuilder.Append("<tr>");
                        htmlBuilder.Append("<th>Image</th>");
                        htmlBuilder.Append("<th>Name</th>");
                        htmlBuilder.Append("<th>Id / Date</th>");

                        foreach (string day in monthOfDays)
                        {
                            DateTime currentDate = DateTime.ParseExact(day, "MMM-dd", CultureInfo.InvariantCulture);

                            if (currentDate.DayOfWeek == DayOfWeek.Sunday)
                            {
                                htmlBuilder.Append("<th style='color:red;'>" + day + "</th>");
                            }
                            else
                            {
                                htmlBuilder.Append("<th>" + day + "</th>");
                            }
                        }

                        htmlBuilder.Append("</tr>");
                        htmlBuilder.Append("</thead>");
                        htmlBuilder.Append("<tbody>");

                        Dictionary<string, Dictionary<string, int>> attendanceData = new Dictionary<string, Dictionary<string, int>>();

                        foreach (DataRow row in dt.Rows)
                        {
                            string userId = row["UserId"].ToString();
                            string transactionDate = row["TransactionDate"] != DBNull.Value ? DateTime.Parse(row["TransactionDate"].ToString()).ToString("MMM-dd") : null;
                            int totalMinutes = row["TotalMinutes"] != DBNull.Value ? Convert.ToInt32(row["TotalMinutes"]) : 0;

                            if (!attendanceData.ContainsKey(userId))
                            {
                                attendanceData[userId] = new Dictionary<string, int>();
                            }

                            if (transactionDate != null)
                            {
                                attendanceData[userId][transactionDate] = totalMinutes;
                            }
                        }

                        var allEmployeeIds = dt.AsEnumerable().Select(row => row["UserId"].ToString()).Distinct().ToList();

                        foreach (var userId in allEmployeeIds)
                        {
                            string userName = dt.AsEnumerable().Where(row => row["UserId"].ToString() == userId).Select(row => row.Field<string>("FirstName")).FirstOrDefault();
                            byte[] empImage = dt.AsEnumerable().Where(row => row["UserId"].ToString() == userId).Select(row => row["Image"] != DBNull.Value ? (byte[])row["Image"] : null).FirstOrDefault();

                            // Check if image is null and handle it
                            string base64Image = empImage != null ? Convert.ToBase64String(empImage) : string.Empty;

                            htmlBuilder.Append("<tr>");
                            htmlBuilder.Append("<td><img src='data:image/jpeg;base64," + base64Image + "' alt='Employee Image' width='50' height='50' style='border-radius:15px;' /></td>");
                            htmlBuilder.Append("<td>" + userName + "</td>");
                            htmlBuilder.Append("<td>" + userId + "</td>");

                            foreach (string day in monthOfDays)
                            {
                                htmlBuilder.Append("<td>");

                                if (attendanceData.ContainsKey(userId) && attendanceData[userId].ContainsKey(day))
                                {
                                    int totalMinutes = attendanceData[userId][day];
                                    TimeSpan difference = TimeSpan.FromMinutes(totalMinutes);
                                    int totalHours = (int)difference.TotalHours;
                                    int totalMinutesPart = difference.Minutes;

                                    htmlBuilder.Append($"{totalHours}H {totalMinutesPart}M");
                                }
                                else
                                {
                                    htmlBuilder.Append("-");
                                }

                                htmlBuilder.Append("</td>");
                            }

                            htmlBuilder.Append("</tr>");
                        }

                        htmlBuilder.Append("</tbody>");
                        AttendanceData.Controls.Add(new LiteralControl(htmlBuilder.ToString()));
                    }
                }
                else
                {
                    Response.Write("<script>alert('Connection string not found.')</script>");
                }
            }
            catch (FormatException ex)
            {
                Response.Write("<script>alert('Please enter valid date format.')</script>");
                TextBox1.Text = "";
                TextBox2.Text = "";
                BindTimeSheetData();
                throw ex; // Use throw; to preserve stack trace
            }
            catch (Exception ex)
            {
                // Log the exception details or handle it as needed
                Response.Write("<script>alert('An error occurred: " + ex.Message + "')</script>");
                throw; 
            }
        }
    }
}