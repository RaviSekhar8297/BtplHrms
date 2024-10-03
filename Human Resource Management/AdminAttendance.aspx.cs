using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Globalization;
using System.Text;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;


namespace Human_Resource_Management
{
    public partial class AdminAttendance : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Bindattendance();

                //int currentYear = DateTime.Now.Year;
                //for (int year = currentYear; year >= currentYear - 10; year--)
                //{
                //    ddlYear.Items.Add(new System.Web.UI.WebControls.ListItem(year.ToString(), year.ToString()));
                //}
                //ddlYear.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Select Year --", ""));
            }
        }
        public void Bindattendance()
        {
            try
            {
                if (ConfigurationManager.ConnectionStrings["NewHRMSString"] != null)
                {
                    string connectionString = ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString;
                    DateTime currentDate = DateTime.Now.Date;
                    DateTime firstDayOfMonth = new DateTime(currentDate.Year, currentDate.Month, 1);
                    DateTime lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

                    List<string> monthOfDays = new List<string>();
                    for (DateTime date = firstDayOfMonth; date <= lastDayOfMonth; date = date.AddDays(1))
                    {
                        monthOfDays.Add(date.ToString("MMM-dd"));
                    }

                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        string query = @"
                    SELECT A.UserId, E.FirstName, CAST(E.Image AS VARBINARY(MAX)) AS Image, 
                           CONVERT(date, A.DateOfTransaction) AS TransactionDate, 
                           MIN(A.DateOfTransaction) AS InTime, 
                           MAX(A.DateOfTransaction) AS OutTime, 
                           CONVERT(TIME, DATEADD(SECOND, DATEDIFF(SECOND, MIN(A.DateOfTransaction), MAX(A.DateOfTransaction)), 0)) AS TimeDifference, 
                           FORMAT(MIN(A.DateOfTransaction), 'HH:mm:ss') AS InDataa, 
                           FORMAT(MAX(A.DateOfTransaction), 'HH:mm:ss') AS OutDataa, 
                           YEAR(DateOfTransaction) AS TransactionYear, 
                           MONTH(DateOfTransaction) AS TransactionMonth 
                    FROM Attendance A 
                    JOIN Employees E ON A.UserId = E.EmpId 
                    WHERE A.DateOfTransaction >= @FirstDayOfMonth 
                      AND A.DateOfTransaction <= @LastDayOfMonth 
                      AND E.Status = '1' 
                    GROUP BY A.UserId, E.FirstName, CAST(E.Image AS VARBINARY(MAX)), 
                             CONVERT(date, A.DateOfTransaction), 
                             YEAR(DateOfTransaction), 
                             MONTH(DateOfTransaction) 
                    ORDER BY A.UserId, CONVERT(date, A.DateOfTransaction);";

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
                        htmlBuilder.Append("<th>Name</th>");
                        htmlBuilder.Append("<th>Id / Date</th>");
                        
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
                        htmlBuilder.Append("<th>TotalDays</th>");
                        htmlBuilder.Append("<th>Presents</th>");
                        htmlBuilder.Append("<th>Absents</th>");
                       
                       
                        htmlBuilder.Append("</tr>");
                        htmlBuilder.Append("</thead>");
                        htmlBuilder.Append("<tbody>");

                        Dictionary<string, Dictionary<string, string>> attendanceData = new Dictionary<string, Dictionary<string, string>>();
                        foreach (DataRow row in dt.Rows)
                        {
                            string userId = row["UserId"].ToString();
                            string transactionDate = row["TransactionDate"] != DBNull.Value ? DateTime.Parse(row["TransactionDate"].ToString()).ToString("MMM-dd") : null;
                            string timeDifference = row["TimeDifference"] != DBNull.Value ? row["TimeDifference"].ToString() : null;

                            if (!attendanceData.ContainsKey(userId))
                            {
                                attendanceData[userId] = new Dictionary<string, string>();
                            }
                            if (transactionDate != null)
                            {
                                attendanceData[userId][transactionDate] = timeDifference;
                            }
                        }

                        var allEmployeeIds = dt.AsEnumerable().Select(row => row["UserId"].ToString()).Distinct().ToList();

                        foreach (var userId in allEmployeeIds)
                        {
                            string userName = dt.AsEnumerable().Where(row => row["UserId"].ToString() == userId).Select(row => row.Field<string>("FirstName")).FirstOrDefault();
                            byte[] empImage = dt.AsEnumerable().Where(row => row["UserId"].ToString() == userId).Select(row => row["Image"] != DBNull.Value ? (byte[])row["Image"] : null).FirstOrDefault();

                            int totalDays = monthOfDays.Count;
                            int presents = 0;
                            int absents = 0;

                            htmlBuilder.Append("<tr>");
                            if (empImage != null)
                            {
                                htmlBuilder.Append("<td><img src='data:image/jpeg;base64," + Convert.ToBase64String(empImage) + "' alt='Image' width='50' height='50' style='border-radius:15px;' /></td>");
                            }
                            else
                            {
                                htmlBuilder.Append("<td><img src='data:image/png;base64' alt='Img' width='50' height='50' style='border-radius:15px;' /></td>");
                            }
                            htmlBuilder.Append("<td>" + userName + "</td>");
                            htmlBuilder.Append("<td>" + userId + "</td>");

                            foreach (string day in monthOfDays)
                            {
                                htmlBuilder.Append("<td>");
                                if (attendanceData.ContainsKey(userId) && attendanceData[userId].ContainsKey(day))
                                {
                                    string timeDifference = attendanceData[userId][day];
                                    TimeSpan difference = TimeSpan.Parse(timeDifference);
                                    if (difference.TotalHours >= 8)
                                    {
                                        htmlBuilder.Append("<i class='fa-solid fa-check text-success'></i>");
                                        presents++;
                                    }
                                    else if (difference.TotalHours >= 4 && difference.TotalHours < 8)
                                    {
                                        htmlBuilder.Append("<div class='icon-column'><i class='fa-solid fa-check text-success'></i></div>");
                                        htmlBuilder.Append("<div class='icon-column'><i class='fa fa-close text-danger'></i></div>");
                                        presents++;
                                    }
                                    else
                                    {
                                        htmlBuilder.Append("<i class='fa fa-close text-danger'></i>");
                                        absents++;
                                    }
                                }
                                else
                                {
                                    DateTime currentDate1 = DateTime.ParseExact(day, "MMM-dd", CultureInfo.InvariantCulture);
                                    if (currentDate1.DayOfWeek == DayOfWeek.Sunday)
                                    {
                                        htmlBuilder.Append("<i class='fa fa-close text-danger'></i>");
                                        absents++;
                                    }
                                    else
                                    {
                                        htmlBuilder.Append("<i class='fa fa-close text-danger'></i>");
                                        absents++;
                                    }
                                }
                                htmlBuilder.Append("</td>");
                            }

                            htmlBuilder.Append("<td>" + totalDays + "</td>");
                            htmlBuilder.Append("<td>" + presents + "</td>");
                            htmlBuilder.Append("<td>" + absents + "</td>");
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
                // Handle the exception appropriately
                throw ex;
            }
        }



        //        protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
        //        {
        //            txtname.Text = "";
        //            TextBox1.Text = "";
        //            string yearofddl = ddlYear.SelectedValue;
        //            int selectedYear = int.Parse(yearofddl);
        //            DateTime startDate;
        //            DateTime endDate;

        //            // Determine the start and end dates based on the selected year
        //            if (selectedYear == DateTime.Now.Year)
        //            {
        //                startDate = new DateTime(selectedYear, 1, 1);
        //                endDate = DateTime.Now; // Current date
        //            }
        //            else
        //            {
        //                startDate = new DateTime(selectedYear, 1, 1);
        //                endDate = new DateTime(selectedYear, 12, 31); // End of the year
        //            }

        //            try
        //            {
        //                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
        //                {
        //                    using (SqlCommand cmd1 = new SqlCommand(@"
        //WITH UserAttendance AS (
        //    SELECT 
        //        A.UserId, 
        //        E.LastName, 
        //        CONVERT(date, A.DateOfTransaction) AS TransactionDate, 
        //        MIN(A.DateOfTransaction) AS InTime, 
        //        MAX(A.DateOfTransaction) AS OutTime, 
        //        CONVERT(TIME, DATEADD(SECOND, DATEDIFF(SECOND, MIN(A.DateOfTransaction), MAX(A.DateOfTransaction)), 0)) AS TimeDifference, 
        //        FORMAT(MIN(A.DateOfTransaction), 'HH:mm:ss') AS InDataa, 
        //        FORMAT(MAX(A.DateOfTransaction), 'HH:mm:ss') AS OutDataa, 
        //        YEAR(DateOfTransaction) AS TransactionYear, 
        //        MONTH(DateOfTransaction) AS TransactionMonth 
        //    FROM Attendance A 
        //    JOIN Employees E ON A.UserId = E.EmpId 
        //    WHERE YEAR(A.DateOfTransaction) = @Yearofdate 
        //    AND A.DateOfTransaction BETWEEN @StartDate AND @EndDate
        //    AND E.Status = '1' 
        //    GROUP BY A.UserId, E.LastName, CONVERT(date, A.DateOfTransaction), YEAR(DateOfTransaction), MONTH(DateOfTransaction)
        //)
        //SELECT 
        //    UA.*, 
        //    E.Image
        //FROM 
        //    UserAttendance UA
        //JOIN 
        //    Employees E ON UA.UserId = E.EmpId
        //ORDER BY 
        //    UA.UserId, UA.TransactionDate;", connection))
        //                    {
        //                        cmd1.Parameters.AddWithValue("@Yearofdate", selectedYear);
        //                        cmd1.Parameters.AddWithValue("@StartDate", startDate);
        //                        cmd1.Parameters.AddWithValue("@EndDate", endDate);

        //                        connection.Open();

        //                        SqlDataAdapter da = new SqlDataAdapter(cmd1);
        //                        DataTable dt = new DataTable();
        //                        da.Fill(dt);
        //                        cmd1.CommandTimeout = 9000;

        //                        // Check if data exists for the selected year
        //                        if (dt.Rows.Count > 0)
        //                        {
        //                            // Generate headers based on distinct months and days
        //                            StringBuilder htmlBuilder = new StringBuilder();
        //                            htmlBuilder.Append("<thead>");
        //                            htmlBuilder.Append("<tr>");
        //                            htmlBuilder.Append("<th>Image</th>");
        //                            htmlBuilder.Append("<th>Name</th>");
        //                            htmlBuilder.Append("<th>Id / Date</th>");

        //                            // Loop through each month from January to the current month or December based on the year
        //                            for (int month = 1; month <= (selectedYear == DateTime.Now.Year ? DateTime.Now.Month : 12); month++)
        //                            {
        //                                DateTime monthStartDate = new DateTime(selectedYear, month, 1);
        //                                DateTime monthEndDate = (selectedYear == DateTime.Now.Year && month == DateTime.Now.Month) ? DateTime.Now : monthStartDate.AddMonths(1).AddDays(-1);

        //                                // Loop through each day of the month
        //                                for (DateTime date = monthStartDate; date <= monthEndDate; date = date.AddDays(1))
        //                                {
        //                                    htmlBuilder.Append("<th>" + date.ToString("MMM-dd") + "</th>");
        //                                }
        //                            }
        //                            htmlBuilder.Append("</tr>");
        //                            htmlBuilder.Append("</thead>");

        //                            htmlBuilder.Append("<tbody>");
        //                            Dictionary<string, Dictionary<string, string>> attendanceData = new Dictionary<string, Dictionary<string, string>>();
        //                            foreach (DataRow row in dt.Rows)
        //                            {
        //                                string userIdd = row["UserId"].ToString();
        //                                string transactionDate = DateTime.Parse(row["TransactionDate"].ToString()).ToString("MMM-dd");
        //                                string timeDifference = row["TimeDifference"].ToString();

        //                                if (!attendanceData.ContainsKey(userIdd))
        //                                {
        //                                    attendanceData[userIdd] = new Dictionary<string, string>();
        //                                }
        //                                attendanceData[userIdd][transactionDate] = timeDifference;
        //                            }

        //                            foreach (var employeeEntry in attendanceData)
        //                            {
        //                                string userIdd = employeeEntry.Key;
        //                                string userName = dt.AsEnumerable().Where(row => Convert.ToInt32(row["UserId"]) == Convert.ToInt32(userIdd)).Select(row => row.Field<string>("LastName")).FirstOrDefault();
        //                                byte[] empImage = dt.AsEnumerable().Where(row => Convert.ToInt32(row["UserId"]) == Convert.ToInt32(userIdd)).Select(row => row["Image"] != DBNull.Value ? (byte[])row["Image"] : null).FirstOrDefault();

        //                                htmlBuilder.Append("<tr>");
        //                                if (empImage != null)
        //                                {
        //                                    htmlBuilder.Append("<td><img src='data:image/jpeg;base64," + Convert.ToBase64String(empImage) + "' alt='Employee Image' width='50' height='50' style='border-radius:15px;' /></td>");
        //                                }
        //                                else
        //                                {
        //                                    htmlBuilder.Append("<td><img src='defaultImage.png' alt='No Image' width='50' height='50' style='border-radius:15px;' /></td>");
        //                                }
        //                                htmlBuilder.Append("<td>" + userName + "</td>");
        //                                htmlBuilder.Append("<td>" + userIdd + "</td>");

        //                                // Loop through each month from January to the current month or December based on the year
        //                                for (int month = 1; month <= (selectedYear == DateTime.Now.Year ? DateTime.Now.Month : 12); month++)
        //                                {
        //                                    DateTime monthStartDate = new DateTime(selectedYear, month, 1);
        //                                    DateTime monthEndDate = (selectedYear == DateTime.Now.Year && month == DateTime.Now.Month) ? DateTime.Now : monthStartDate.AddMonths(1).AddDays(-1);

        //                                    // Loop through each day of the month
        //                                    for (DateTime date = monthStartDate; date <= monthEndDate; date = date.AddDays(1))
        //                                    {
        //                                        string monthDateKey = date.ToString("MMM-dd");

        //                                        if (employeeEntry.Value.ContainsKey(monthDateKey))
        //                                        {
        //                                            string timeDifference = employeeEntry.Value[monthDateKey];
        //                                            htmlBuilder.Append("<td>");
        //                                            if (timeDifference == "00")
        //                                            {
        //                                                htmlBuilder.Append("<i class='fa fa-close text-danger'></i>");
        //                                            }
        //                                            else
        //                                            {
        //                                                TimeSpan difference = TimeSpan.Parse(timeDifference);
        //                                                if (difference.TotalHours >= 8)
        //                                                {
        //                                                    htmlBuilder.Append("<i class='fa-solid fa-check text-success'></i>");
        //                                                }
        //                                                else if (difference.TotalHours >= 4 && difference.TotalHours < 8)
        //                                                {
        //                                                    htmlBuilder.Append("<div class='icon-column'><i class='fa-solid fa-check text-success'></i></div>");
        //                                                    htmlBuilder.Append("<div class='icon-column'><i class='fa fa-close text-danger'></i></div>");
        //                                                }
        //                                                else
        //                                                {
        //                                                    htmlBuilder.Append("<i class='fa fa-close text-danger'></i>");
        //                                                }
        //                                            }
        //                                            htmlBuilder.Append("</td>");
        //                                        }
        //                                        else
        //                                        {
        //                                            DateTime currentDate1 = DateTime.ParseExact(monthDateKey, "MMM-dd", CultureInfo.InvariantCulture);
        //                                            if (currentDate1.DayOfWeek == DayOfWeek.Sunday)
        //                                            {
        //                                                htmlBuilder.Append("<td style='color:#091c99;'>WO</td>");
        //                                            }
        //                                            else
        //                                            {
        //                                                htmlBuilder.Append("<td><i class='fa fa-close text-danger'></i></td>");
        //                                            }
        //                                        }
        //                                    }
        //                                }

        //                                htmlBuilder.Append("</tr>");
        //                            }

        //                            htmlBuilder.Append("</tbody>");
        //                            AttendanceData.Controls.Add(new LiteralControl(htmlBuilder.ToString()));
        //                        }
        //                        else
        //                        {
        //                            // No data found for the selected year
        //                            StringBuilder htmlBuilder = new StringBuilder();
        //                            htmlBuilder.Append("<p style='text-align: center; color: red;'>No data found for the selected year.</p>");
        //                            AttendanceData.Controls.Add(new LiteralControl(htmlBuilder.ToString()));
        //                        }
        //                    }
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                // Handle exceptions
        //                throw ex;
        //            }

        //        }

        //        protected void TextBox1_TextChanged(object sender, EventArgs e)
        //        {
        //            txtname.Text = "";
        //            ddlYear.SelectedValue = "";
        //            string monthYear = TextBox1.Text;
        //            DateTime selectedDate = DateTime.Parse(monthYear);
        //            int selectedYear = selectedDate.Year;
        //            int selectedMonth = selectedDate.Month;

        //            try
        //            {
        //                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
        //                {
        //                    using (SqlCommand cmd1 = new SqlCommand(@"
        //            SELECT A.UserId, E.FirstName, CAST(E.Image AS VARBINARY(MAX)) AS Image, 
        //                   CONVERT(date, A.DateOfTransaction) AS TransactionDate, 
        //                   MIN(A.DateOfTransaction) AS InTime, MAX(A.DateOfTransaction) AS OutTime, 
        //                   CONVERT(TIME, DATEADD(SECOND, DATEDIFF(SECOND, MIN(A.DateOfTransaction), MAX(A.DateOfTransaction)), 0)) AS TimeDifference, 
        //                   FORMAT(MIN(A.DateOfTransaction), 'HH:mm:ss') AS InDataa, FORMAT(MAX(A.DateOfTransaction), 'HH:mm:ss') AS OutDataa, 
        //                   YEAR(DateOfTransaction) AS TransactionYear, MONTH(DateOfTransaction) AS TransactionMonth 
        //            FROM Attendance A 
        //            JOIN Employees E ON A.UserId = E.EmpId 
        //            WHERE YEAR(A.DateOfTransaction) = @Yearofdate AND MONTH(A.DateOfTransaction) = @Monthofdate 
        //            GROUP BY A.UserId, E.FirstName, CAST(E.Image AS VARBINARY(MAX)), CONVERT(date, A.DateOfTransaction), 
        //                     YEAR(DateOfTransaction), MONTH(DateOfTransaction) 
        //            ORDER BY A.UserId, CONVERT(date, A.DateOfTransaction);
        //        ", connection))
        //                    {
        //                        cmd1.Parameters.AddWithValue("@Yearofdate", selectedYear);
        //                        cmd1.Parameters.AddWithValue("@Monthofdate", selectedMonth);

        //                        connection.Open();

        //                        SqlDataAdapter da = new SqlDataAdapter(cmd1);
        //                        DataTable dt = new DataTable();
        //                        da.Fill(dt);
        //                        cmd1.CommandTimeout = 9000;

        //                        if (dt.Rows.Count > 0)
        //                        {
        //                            StringBuilder htmlBuilder = new StringBuilder();
        //                            htmlBuilder.Append("<thead>");
        //                            htmlBuilder.Append("<tr>");
        //                            htmlBuilder.Append("<th>Image</th>");
        //                            htmlBuilder.Append("<th>Name</th>");
        //                            htmlBuilder.Append("<th>Id / Date</th>");

        //                            for (int day = 1; day <= DateTime.DaysInMonth(selectedYear, selectedMonth); day++)
        //                            {
        //                                htmlBuilder.Append("<th>" + new DateTime(selectedYear, selectedMonth, day).ToString("MMM-dd") + "</th>");
        //                            }
        //                            htmlBuilder.Append("</tr>");
        //                            htmlBuilder.Append("</thead>");
        //                            htmlBuilder.Append("<tbody>");

        //                            Dictionary<string, Dictionary<string, string>> attendanceData = new Dictionary<string, Dictionary<string, string>>();
        //                            foreach (DataRow row in dt.Rows)
        //                            {
        //                                string userIdd = row["UserId"].ToString();
        //                                string transactionDate = DateTime.Parse(row["TransactionDate"].ToString()).ToString("MMM-dd");
        //                                string timeDifference = row["TimeDifference"].ToString();

        //                                if (!attendanceData.ContainsKey(userIdd))
        //                                {
        //                                    attendanceData[userIdd] = new Dictionary<string, string>();
        //                                }
        //                                attendanceData[userIdd][transactionDate] = timeDifference;
        //                            }

        //                            foreach (var employeeEntry in attendanceData)
        //                            {
        //                                string userIdd = employeeEntry.Key;
        //                                string userName = dt.AsEnumerable().Where(row => Convert.ToInt32(row["UserId"]) == Convert.ToInt32(userIdd)).Select(row => row.Field<string>("FirstName")).FirstOrDefault();
        //                                byte[] empImage = dt.AsEnumerable().Where(row => Convert.ToInt32(row["UserId"]) == Convert.ToInt32(userIdd)).Select(row => row["Image"] != DBNull.Value ? (byte[])row["Image"] : null).FirstOrDefault();

        //                                htmlBuilder.Append("<tr>");
        //                                if (empImage != null)
        //                                {
        //                                    htmlBuilder.Append("<td><img src='data:image/jpeg;base64," + Convert.ToBase64String(empImage) + "' alt='Employee Image' width='50' height='50' style='border-radius:15px;' /></td>");
        //                                }
        //                                else
        //                                {
        //                                    htmlBuilder.Append("<td><img src='/path/to/default/image.jpg' alt='Img' width='50' height='50' style='border-radius:15px;' /></td>");
        //                                }
        //                                htmlBuilder.Append("<td>" + userName + "</td>");
        //                                htmlBuilder.Append("<td>" + userIdd + "</td>");

        //                                for (int day = 1; day <= DateTime.DaysInMonth(selectedYear, selectedMonth); day++)
        //                                {
        //                                    string monthDateKey = new DateTime(selectedYear, selectedMonth, day).ToString("MMM-dd");
        //                                    if (employeeEntry.Value.ContainsKey(monthDateKey))
        //                                    {
        //                                        string timeDifference = employeeEntry.Value[monthDateKey];
        //                                        htmlBuilder.Append("<td>");
        //                                        if (timeDifference == "00")
        //                                        {
        //                                            htmlBuilder.Append("<i class='fa fa-close text-danger'></i>");
        //                                        }
        //                                        else
        //                                        {
        //                                            TimeSpan difference = TimeSpan.Parse(timeDifference);
        //                                            if (difference.TotalHours >= 8)
        //                                            {
        //                                                htmlBuilder.Append("<i class='fa-solid fa-check text-success'></i>");
        //                                            }
        //                                            else if (difference.TotalHours >= 4 && difference.TotalHours < 8)
        //                                            {
        //                                                htmlBuilder.Append("<div class='icon-column'><i class='fa-solid fa-check text-success'></i></div>");
        //                                                htmlBuilder.Append("<div class='icon-column'><i class='fa fa-close text-danger'></i></div>");
        //                                            }
        //                                            else
        //                                            {
        //                                                htmlBuilder.Append("<i class='fa fa-close text-danger'></i>");
        //                                            }
        //                                        }
        //                                        htmlBuilder.Append("</td>");
        //                                    }
        //                                    else
        //                                    {
        //                                        DateTime currentDate1 = DateTime.ParseExact(monthDateKey, "MMM-dd", CultureInfo.InvariantCulture);
        //                                        if (currentDate1.DayOfWeek == DayOfWeek.Sunday)
        //                                        {
        //                                            htmlBuilder.Append("<td style='color:#091c99;'>WO</td>");
        //                                        }
        //                                        else
        //                                        {
        //                                            htmlBuilder.Append("<td><i class='fa fa-close text-danger'></i></td>");
        //                                        }
        //                                    }
        //                                }
        //                                htmlBuilder.Append("</tr>");
        //                            }

        //                            htmlBuilder.Append("</tbody>");
        //                            AttendanceData.Controls.Add(new LiteralControl(htmlBuilder.ToString()));
        //                            txtname.Text = "";
        //                            ddlYear.SelectedValue = "";
        //                        }
        //                        else
        //                        {
        //                            StringBuilder htmlBuilder = new StringBuilder();
        //                            htmlBuilder.Append("<p style='text-align: center; color: red;'>No data found for the selected year.</p>");
        //                            AttendanceData.Controls.Add(new LiteralControl(htmlBuilder.ToString()));
        //                            txtname.Text = "";
        //                            ddlYear.SelectedValue = "";
        //                        }
        //                    }
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                // Handle exceptions
        //                throw ex;
        //            }


        //        }

        protected void btnsearch_Click(object sender, EventArgs e)
        {
            string startDateText = TextBox2.Text;
            string endDateText = TextBox3.Text;
            string enteredName = name.Text.Trim(); // Get the name filter

            DateTime startDate;
            DateTime endDate;

            // Validate date inputs
            if (!DateTime.TryParse(startDateText, out startDate) || !DateTime.TryParse(endDateText, out endDate))
            {
                // Show alert for invalid date format
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Invalid date format.');", true);
                return;
            }

            if (startDate > endDate)
            {
                // Show alert for invalid date range
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Start date cannot be later than end date.');", true);
                return;
            }

            try
            {
                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    using (SqlCommand cmd1 = new SqlCommand(@"
SELECT A.UserId, E.FirstName, CAST(E.Image AS VARBINARY(MAX)) AS Image, 
       CONVERT(date, A.DateOfTransaction) AS TransactionDate, 
       MIN(A.DateOfTransaction) AS InTime, MAX(A.DateOfTransaction) AS OutTime, 
       CONVERT(TIME, DATEADD(SECOND, DATEDIFF(SECOND, MIN(A.DateOfTransaction), MAX(A.DateOfTransaction)), 0)) AS TimeDifference, 
       FORMAT(MIN(A.DateOfTransaction), 'HH:mm:ss') AS InDataa, FORMAT(MAX(A.DateOfTransaction), 'HH:mm:ss') AS OutDataa, 
       YEAR(DateOfTransaction) AS TransactionYear, MONTH(DateOfTransaction) AS TransactionMonth 
FROM Attendance A 
JOIN Employees E ON A.UserId = E.EmpId 
WHERE A.DateOfTransaction BETWEEN @StartDate AND @EndDate 
AND (E.FirstName LIKE @NameFilter OR @NameFilter IS NULL) 
GROUP BY A.UserId, E.FirstName, CAST(E.Image AS VARBINARY(MAX)), CONVERT(date, A.DateOfTransaction), 
         YEAR(DateOfTransaction), MONTH(DateOfTransaction) 
ORDER BY A.UserId, CONVERT(date, A.DateOfTransaction);
", connection))
                    {
                        cmd1.Parameters.AddWithValue("@StartDate", startDate);
                        cmd1.Parameters.AddWithValue("@EndDate", endDate);
                        // Use '%' for SQL LIKE query
                        cmd1.Parameters.AddWithValue("@NameFilter", string.IsNullOrEmpty(enteredName) ? (object)DBNull.Value : "%" + enteredName + "%");

                        connection.Open();

                        SqlDataAdapter da = new SqlDataAdapter(cmd1);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        cmd1.CommandTimeout = 9000;

                        if (dt.Rows.Count > 0)
                        {
                            StringBuilder htmlBuilder = new StringBuilder();
                            htmlBuilder.Append("<thead>");
                            htmlBuilder.Append("<tr>");
                            htmlBuilder.Append("<th>Image</th>");
                            htmlBuilder.Append("<th>Name</th>");
                            htmlBuilder.Append("<th>Id / Date</th>");
                            htmlBuilder.Append("<th>TotalDays</th>");
                            htmlBuilder.Append("<th>Presents</th>");
                            htmlBuilder.Append("<th>Absents</th>");

                            for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
                            {
                                htmlBuilder.Append("<th>" + date.ToString("MMM-dd") + "</th>");
                            }

                            htmlBuilder.Append("</tr>");
                            htmlBuilder.Append("</thead>");
                            htmlBuilder.Append("<tbody>");

                            Dictionary<string, Dictionary<string, string>> attendanceData = new Dictionary<string, Dictionary<string, string>>();
                            foreach (DataRow row in dt.Rows)
                            {
                                string userIdd = row["UserId"].ToString();
                                string transactionDate = DateTime.Parse(row["TransactionDate"].ToString()).ToString("MMM-dd");
                                string timeDifference = row["TimeDifference"].ToString();

                                if (!attendanceData.ContainsKey(userIdd))
                                {
                                    attendanceData[userIdd] = new Dictionary<string, string>();
                                }
                                attendanceData[userIdd][transactionDate] = timeDifference;
                            }

                            int totalDays = (endDate - startDate).Days + 1;

                            foreach (var employeeEntry in attendanceData)
                            {
                                string userIdd = employeeEntry.Key;
                                string userName = dt.AsEnumerable().Where(row => Convert.ToInt32(row["UserId"]) == Convert.ToInt32(userIdd)).Select(row => row.Field<string>("FirstName")).FirstOrDefault();
                                byte[] empImage = dt.AsEnumerable().Where(row => Convert.ToInt32(row["UserId"]) == Convert.ToInt32(userIdd)).Select(row => row["Image"] != DBNull.Value ? (byte[])row["Image"] : null).FirstOrDefault();

                                int presents = employeeEntry.Value.Count;
                                int absents = totalDays - presents;

                                htmlBuilder.Append("<tr>");
                                if (empImage != null)
                                {
                                    htmlBuilder.Append("<td><img src='data:image/jpeg;base64," + Convert.ToBase64String(empImage) + "' alt='Employee Image' width='50' height='50' style='border-radius:15px;' /></td>");
                                }
                                else
                                {
                                    htmlBuilder.Append("<td><img src='/path/to/default/image.jpg' alt='Img' width='50' height='50' style='border-radius:15px;' /></td>");
                                }
                                htmlBuilder.Append("<td>" + userName + "</td>");
                                htmlBuilder.Append("<td>" + userIdd + "</td>");
                                htmlBuilder.Append("<td>" + totalDays + "</td>");
                                htmlBuilder.Append("<td>" + presents + "</td>");
                                htmlBuilder.Append("<td>" + absents + "</td>");

                                for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
                                {
                                    string monthDateKey = date.ToString("MMM-dd");
                                    if (employeeEntry.Value.ContainsKey(monthDateKey))
                                    {
                                        string timeDifference = employeeEntry.Value[monthDateKey];
                                        htmlBuilder.Append("<td>");
                                        if (timeDifference == "00")
                                        {
                                            htmlBuilder.Append("<i class='fa fa-close text-danger'></i>");
                                        }
                                        else
                                        {
                                            TimeSpan difference = TimeSpan.Parse(timeDifference);
                                            if (difference.TotalHours >= 8)
                                            {
                                                htmlBuilder.Append("<i class='fa-solid fa-check text-success'></i>");
                                            }
                                            else if (difference.TotalHours >= 4 && difference.TotalHours < 8)
                                            {
                                                htmlBuilder.Append("<div class='icon-column'><i class='fa-solid fa-check text-success'></i></div>");
                                                htmlBuilder.Append("<div class='icon-column'><i class='fa fa-close text-danger'></i></div>");
                                            }
                                            else
                                            {
                                                htmlBuilder.Append("<i class='fa fa-close text-danger'></i>");
                                            }
                                        }
                                        htmlBuilder.Append("</td>");
                                    }
                                    else
                                    {
                                        DateTime currentDate1 = DateTime.ParseExact(monthDateKey, "MMM-dd", CultureInfo.InvariantCulture);
                                        if (currentDate1.DayOfWeek == DayOfWeek.Sunday)
                                        {
                                            htmlBuilder.Append("<td style='color:#091c99;'>WO</td>");
                                        }
                                        else
                                        {
                                            htmlBuilder.Append("<td><i class='fa fa-close text-danger'></i></td>");
                                        }
                                    }

                                }
                                htmlBuilder.Append("</tr>");
                            }

                            htmlBuilder.Append("</tbody>");
                            AttendanceData.Controls.Add(new LiteralControl(htmlBuilder.ToString()));
                        }
                        else
                        {
                            StringBuilder htmlBuilder = new StringBuilder();
                            htmlBuilder.Append("<p style='text-align: center; color: red;'>No data found for the selected date range and name.</p>");
                            AttendanceData.Controls.Add(new LiteralControl(htmlBuilder.ToString()));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
                ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('An error occurred: {ex.Message}');", true);
            }

        }
    }
}