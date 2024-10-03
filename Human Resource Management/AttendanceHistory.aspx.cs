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
    public partial class AttendanceHistory : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtmonthsearch.Text = DateTime.Now.ToString("yyyy-MM");
                MonthlyAttendanceBind(DateTime.Now, null); 
            }
        }
        public void MonthlyAttendanceBind(DateTime selectedMonth, string empName = null)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString;

                // Get the year and month from the DateTime parameter
                int year = selectedMonth.Year;
                int month = selectedMonth.Month;
                DateTime firstDayOfMonth = new DateTime(year, month, 1);
                DateTime lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

                // Include the entire last day (up to 23:59:59)
                lastDayOfMonth = lastDayOfMonth.Date.AddHours(23).AddMinutes(59).AddSeconds(59);

                // Get the list of holidays between firstDayOfMonth and lastDayOfMonth
                List<DateTime> holidayDates = GetHolidayDates(firstDayOfMonth, lastDayOfMonth);

                // Build the SQL query to fetch attendance data (your query remains the same)
                string query = @"
        SELECT E.EmpId, E.FirstName, 
               CONVERT(VARCHAR(10), A.DateOfTransaction, 120) AS [Date],
               ISNULL(MIN(CONVERT(VARCHAR(5), A.DateOfTransaction, 108)), '00:00') AS InTime,
               ISNULL(MAX(CONVERT(VARCHAR(5), A.DateOfTransaction, 108)), '00:00') AS OutTime
        FROM Employees E
        LEFT JOIN Attendance A ON E.EmpId = A.UserId AND A.DateOfTransaction BETWEEN @StartDate AND @EndDate";

                if (!string.IsNullOrEmpty(empName))
                {
                    query += " WHERE E.FirstName LIKE @EmpName";
                }

                query += @"
        GROUP BY E.EmpId, E.FirstName, CONVERT(VARCHAR(10), A.DateOfTransaction, 120)
        ORDER BY E.FirstName, [Date]";

                using (var connection = new SqlConnection(connectionString))
                using (var sqlCommand = new SqlCommand(query, connection))
                {
                    sqlCommand.Parameters.AddWithValue("@StartDate", firstDayOfMonth);
                    sqlCommand.Parameters.AddWithValue("@EndDate", lastDayOfMonth);

                    if (!string.IsNullOrEmpty(empName))
                    {
                        sqlCommand.Parameters.AddWithValue("@EmpName", "%" + empName + "%");
                    }

                    connection.Open();
                    using (var sqlDataReader = sqlCommand.ExecuteReader())
                    {
                        var employeeData = new Dictionary<string, string>();
                        var attendanceData = new Dictionary<string, Dictionary<DateTime, (string InTime, string OutTime)>>();

                        while (sqlDataReader.Read())
                        {
                            string empId = sqlDataReader["EmpId"].ToString();
                            string firstName = sqlDataReader["FirstName"].ToString();
                            DateTime date = sqlDataReader["Date"] != DBNull.Value ? Convert.ToDateTime(sqlDataReader["Date"]) : DateTime.MinValue;

                            string inTime = sqlDataReader["InTime"] != DBNull.Value ? sqlDataReader["InTime"].ToString() : "00:00";
                            string outTime = sqlDataReader["OutTime"] != DBNull.Value ? sqlDataReader["OutTime"].ToString() : "00:00";

                            if (!employeeData.ContainsKey(empId))
                            {
                                employeeData[empId] = firstName;
                            }

                            if (!attendanceData.ContainsKey(empId))
                            {
                                attendanceData[empId] = new Dictionary<DateTime, (string, string)>();
                            }

                            attendanceData[empId][date] = (inTime, outTime);
                        }

                        // Create the table HTML
                        StringBuilder sb = new StringBuilder();

                        // Table header with dates and InTime/OutTime labels
                        sb.Append("<table class='table table-bordered'><thead><tr>");
                        sb.Append("<th rowspan='2' class='headers-name'>Image</th>");
                        sb.Append("<th rowspan='2' class='headers-name'>First Name</th>");
                        sb.Append("<th rowspan='2' class='headers-name'>EmpId</th>");

                        // First loop: Generate the date headers with Sunday and holiday checks
                        for (DateTime dt = firstDayOfMonth; dt <= lastDayOfMonth; dt = dt.AddDays(1))
                        {
                            bool isSunday = (dt.DayOfWeek == DayOfWeek.Sunday);
                            bool isHoliday = holidayDates.Contains(dt);

                            string style = isSunday ? "color: red;" : (isHoliday ? "color: green;" : "");

                            sb.Append("<th colspan='2' class='header-date' style='").Append(style).Append("'>").Append(dt.ToString("yyyy-MM-dd")).Append("</th>");
                        }
                        sb.Append("</tr><tr>");

                        // Second loop: Generate the InTime and OutTime columns
                        for (DateTime dt = firstDayOfMonth; dt <= lastDayOfMonth; dt = dt.AddDays(1))
                        {
                            bool isSunday = (dt.DayOfWeek == DayOfWeek.Sunday);
                            bool isHoliday = holidayDates.Contains(dt);

                            string style = isSunday ? "color: red;" : (isHoliday ? "color: green;" : "");

                            sb.Append("<th style='").Append(style).Append("'>InTime</th>");
                            sb.Append("<th style='").Append(style).Append("'>OutTime</th>");
                        }

                        sb.Append("</tr></thead><tbody>");

                        foreach (var emp in employeeData)
                        {
                            string empId = emp.Key;
                            string firstName = emp.Value;

                            sb.Append("<tr>");
                            sb.Append("<td><img src='data:image/png;base64," + GetEmployeeImageBase64(empId) + "' alt='Image' style='width:50px;height:50px;border-radius:50%;' /></td>");
                            sb.Append("<td>").Append(HttpUtility.HtmlEncode(firstName)).Append("</td>");
                            sb.Append("<td>").Append(HttpUtility.HtmlEncode(empId)).Append("</td>");

                            for (DateTime dt = firstDayOfMonth; dt <= lastDayOfMonth; dt = dt.AddDays(1))
                            {
                                bool isSunday = (dt.DayOfWeek == DayOfWeek.Sunday);
                                bool isHoliday = holidayDates.Contains(dt);

                                string style = isSunday ? "color: red;" : (isHoliday ? "color: green;" : "");

                                if (attendanceData.ContainsKey(empId) && attendanceData[empId].ContainsKey(dt))
                                {
                                    var (inTime, outTime) = attendanceData[empId][dt];
                                    sb.Append("<td style='").Append(style).Append("'>").Append(inTime).Append("</td>");
                                    sb.Append("<td style='").Append(style).Append("'>").Append(outTime).Append("</td>");
                                }
                                else
                                {
                                    sb.Append("<td style='").Append(style).Append("'>00:00</td>");
                                    sb.Append("<td style='").Append(style).Append("'>00:00</td>");
                                }
                            }

                            sb.Append("</tr>");
                        }

                        sb.Append("</tbody></table>");

                        // Save the table HTML in session and add it to the placeholder
                        Session["AttendanceTableHtml"] = sb.ToString();
                        AttendanceTablePlaceholder.Controls.Add(new LiteralControl(sb.ToString()));
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('An error occurred: " + ex.Message + "');", true);
            }
        }

        public List<DateTime> GetHolidayDates(DateTime startDate, DateTime endDate)
        {
            List<DateTime> holidayDates = new List<DateTime>();

            string connectionString = ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString;
            string query = "SELECT HolidayDate FROM HolidaysTable WHERE HolidayDate BETWEEN @StartDate AND @EndDate";

            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@StartDate", startDate);
                command.Parameters.AddWithValue("@EndDate", endDate);

                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        DateTime holidayDate = reader["HolidayDate"] != DBNull.Value ? Convert.ToDateTime(reader["HolidayDate"]) : DateTime.MinValue;
                        holidayDates.Add(holidayDate);
                    }
                }
            }

            return holidayDates;
        }


        private string GetEmployeeImageBase64(string empId)
        {
            byte[] imageData = GetEmployeeImage(empId);
            if (imageData != null && imageData.Length > 0)
            {
                return Convert.ToBase64String(imageData);
            }
            return string.Empty; 
        }

        private byte[] GetEmployeeImage(string empId)
        {
            byte[] imageData = null;
            string imageQuery = "SELECT Image FROM Employees WHERE EmpId = @EmpId";
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
            using (var sqlCmd = new SqlCommand(imageQuery, connection))
            {
                sqlCmd.Parameters.AddWithValue("@EmpId", empId);
                connection.Open();
                var result = sqlCmd.ExecuteScalar();
                if (result != DBNull.Value)
                {
                    imageData = (byte[])result;
                }
            }
            return imageData;
        }

        protected void txtmonthsearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                // Get the selected month and year from the TextBox
                string monthYearText = txtmonthsearch.Text.Trim();

                if (!string.IsNullOrEmpty(monthYearText) && DateTime.TryParseExact(monthYearText, "yyyy-MM", null, System.Globalization.DateTimeStyles.None, out DateTime selectedMonth))
                {
                    // Get the search name
                    string empName = txtempnamesearch.Text.Trim();

                    // Call MonthlyAttendanceBind with the selected month and optional employee name
                    MonthlyAttendanceBind(selectedMonth, string.IsNullOrEmpty(empName) ? null : empName);
                }
                else
                {
                    // Handle invalid month-year input
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please enter a valid month and year.');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('An error occurred: " + ex.Message + "');", true);
            }
        }

        protected void lnbexcell_Click(object sender, EventArgs e)
        {
            try
            {
                // Retrieve the HTML table from the session
                string attendanceTableHtml = Session["AttendanceTableHtml"] as string;

                if (!string.IsNullOrEmpty(attendanceTableHtml))
                {
                    attendanceTableHtml = RemoveImageColumn(attendanceTableHtml);
                    // Generate Excel file from the HTML string
                    Response.Clear();
                    Response.Buffer = true;
                    Response.AddHeader("content-disposition", "attachment;filename=AttendanceReport.xls");
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.ms-excel";
                    Response.Output.Write(attendanceTableHtml);
                    Response.Flush();
                    Response.End();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('No data to export.');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('An error occurred: " + ex.Message + "');", true);
            }
        }
        private string RemoveImageColumn(string html)
        {
            // Find the index of the column header and data cells
            string imageHeaderStart = "<th rowspan='2' class='headers-name'>Image</th>";
            string imageCellStart = "<td><img src='data:image/png;base64,";
            string imageCellEnd = "</td>";

            // Remove the image header column
            int headerStartIndex = html.IndexOf(imageHeaderStart);
            if (headerStartIndex != -1)
            {
                int headerEndIndex = html.IndexOf("</th>", headerStartIndex) + "</th>".Length;
                html = html.Remove(headerStartIndex, headerEndIndex - headerStartIndex);
            }

            // Remove image cells
            int cellStartIndex = html.IndexOf(imageCellStart);
            while (cellStartIndex != -1)
            {
                int cellEndIndex = html.IndexOf(imageCellEnd, cellStartIndex) + imageCellEnd.Length;
                html = html.Remove(cellStartIndex, cellEndIndex - cellStartIndex);

                // Find the next image cell
                cellStartIndex = html.IndexOf(imageCellStart);
            }

            return html;
        }
    }
}