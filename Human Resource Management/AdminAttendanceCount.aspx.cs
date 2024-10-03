using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DocumentFormat.OpenXml.Bibliography;

namespace Human_Resource_Management
{
    public partial class AdminSalarySheet : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {          
            if (!IsPostBack)
            {
                AutoInsertData();
                DateTime previousMonthDate = DateTime.Now.AddMonths(-1);
                BindData(null, previousMonthDate.Month, previousMonthDate.Year); // Initially load previous month's data
            }
        }
        private DataTable GetAttendanceData(string name = null, int? month = null, int? year = null)
        {
            DataTable dt = new DataTable();

            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT AttendanceList.*, Employees.FirstName, Employees.Image FROM AttendanceList JOIN Employees ON AttendanceList.EmpId = Employees.EmpId WHERE 1=1";

                    if (year.HasValue)
                    {
                        query += " AND AttendanceList.Year = @Year";
                    }

                    if (month.HasValue)
                    {
                        query += " AND AttendanceList.Month = @Month";
                    }

                    if (!string.IsNullOrEmpty(name))
                    {
                        query += " AND Employees.FirstName LIKE @Name";
                    }

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        if (year.HasValue)
                        {
                            command.Parameters.AddWithValue("@Year", year.Value);
                        }

                        if (month.HasValue)
                        {
                            command.Parameters.AddWithValue("@Month", month.Value);
                        }

                        if (!string.IsNullOrEmpty(name))
                        {
                            command.Parameters.AddWithValue("@Name", "%" + name + "%");
                        }

                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            dt.Load(reader);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                Debug.WriteLine(ex.Message);
            }

            return dt;
        }

        public void BindData(string name = null, int? month = null, int? year = null)
        {
            DataTable dt = GetAttendanceData(name, month, year);

            // Create a GridView for exporting (if necessary)
            GridView gridView = new GridView();
            gridView.DataSource = dt;
            gridView.DataBind();

            AttendanceCount.Controls.Clear();

            // Loop through the DataTable and build the HTML for display
            foreach (DataRow row in dt.Rows)
            {
                string Name = row["FirstName"].ToString();

                byte[] imageData = row["Image"] != DBNull.Value ? (byte[])row["Image"] : new byte[0];
                string base64String = Convert.ToBase64String(imageData);
                string imageUrl = "data:image/jpeg;base64," + base64String;

                int EmpId = Convert.ToInt32(row["EmpId"]);
                decimal TotalDays = (decimal)row["TotalDays"];
                decimal WorkingDays = (decimal)row["WorkingDays"];
                int WeekOffs = (int)row["WeekOffs"];
                decimal HoliDays = (decimal)row["HoliDays"];
                decimal Presents = (decimal)row["Presents"];
                decimal Absents = (decimal)row["Absents"];
                decimal HalfDays = (decimal)row["HalfDays"];
                int LateLogs = (int)row["LateLogs"];
                decimal LOPs = (decimal)row["LOPs"];
                decimal CL = (decimal)row["CL"];
                decimal SL = (decimal)row["SL"];
                decimal CompOffs = (decimal)row["CompOffs"];
                decimal PaybleDays = (decimal)row["PaybleDays"];
                int Year = Convert.ToInt32(row["Year"]);
                int Month = Convert.ToInt32(row["Month"]);

                // Convert the numeric month to its full name
                string MonthName = GetMonthName(Month);

                StringBuilder projectHtml = new StringBuilder();
                projectHtml.Append("<tr>");
                projectHtml.Append("<td><a href='#' class='avatar'><img src='" + imageUrl + "' alt='Image'></a>");
                projectHtml.Append("<a href='#'>" + Name + "</a></td>");
                projectHtml.Append("<td>" + EmpId + "</td>");
                projectHtml.Append("<td>" + TotalDays + "</td>");
                projectHtml.Append("<td>" + WorkingDays + "</td>");
                projectHtml.Append("<td>" + WeekOffs + "</td>");
                projectHtml.Append("<td>" + HoliDays + "</td>");
                projectHtml.Append("<td>" + Presents + "</td>");
                projectHtml.Append("<td>" + Absents + "</td>");
                projectHtml.Append("<td>" + HalfDays + "</td>");
                projectHtml.Append("<td>" + LateLogs + "</td>");
                projectHtml.Append("<td>" + LOPs + "</td>");
                projectHtml.Append("<td>" + CL + "</td>");
                projectHtml.Append("<td>" + SL + "</td>");
                projectHtml.Append("<td>" + CompOffs + "</td>");
                projectHtml.Append("<td>" + PaybleDays + "</td>");
                projectHtml.Append("<td>" + MonthName + "</td>");  // Display the full month name
                projectHtml.Append("<td><a class='edit' href='#' data-bs-toggle='modal' data-bs-target='#edit_attendancecount' onclick =\"editAttendanceCount('" + Name + "','" + Presents + "','" + Absents + "','" + HalfDays + "','" + LateLogs + "','" + LOPs + "','" + CL + "','" + SL + "','" + CompOffs + "','" + PaybleDays + "','" + EmpId + "','" + Year + "','" + Month + "','" + WeekOffs + "','" + HoliDays + "')\" ><i class='fa-solid fa-pencil m-r-5' style='color:green;'></i></a></td>");
                projectHtml.Append("</tr>");

                AttendanceCount.Controls.Add(new LiteralControl(projectHtml.ToString()));
            }
        }

        private string GetMonthName(int monthNumber)
        {
            try
            {
                DateTimeFormatInfo dateTimeFormat = CultureInfo.CurrentCulture.DateTimeFormat;
                return dateTimeFormat.GetMonthName(monthNumber);
            }
            catch (Exception ex)
            {
                // Handle potential errors in month conversion
                Debug.WriteLine(ex.Message);
                return "Unknown";
            }
        }



        public void AutoInsertData()
        {
            try
            {
                DateTime now = DateTime.Now;
                DateTime firstDayOfCurrentMonth = new DateTime(now.Year, now.Month, 1);
                DateTime firstDayOfPreviousMonth = firstDayOfCurrentMonth.AddMonths(-1);
                int previousMonth = firstDayOfPreviousMonth.Month;
                int previousYear = firstDayOfPreviousMonth.Year;

                int totalDays = DateTime.DaysInMonth(previousYear, previousMonth);
                DateTime lastDayOfMonth = new DateTime(previousYear, previousMonth, totalDays);

                string connectionString = ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString;

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Check if the data for the previous month already exists
                    using (SqlCommand checkCmd = new SqlCommand("SELECT COUNT(*) FROM AttendanceList WHERE Month = @Month AND Year = @Year and EmpId=1027", conn))
                    {
                        checkCmd.Parameters.AddWithValue("@Month", previousMonth);
                        checkCmd.Parameters.AddWithValue("@Year", previousYear);

                        int count = (int)checkCmd.ExecuteScalar();

                        if (count == 0)
                        {
                            // Fetch all employees who are active
                            List<(int EmpId, string EmployeeName, DateTime DOJ)> employees = new List<(int, string, DateTime)>();

                            using (SqlCommand cmd = new SqlCommand("SELECT EmpId, FirstName, Doj FROM Employees WHERE Status = '1' and EmpId=1027", conn))
                            {
                                using (SqlDataReader reader = cmd.ExecuteReader())
                                {
                                    while (reader.Read())
                                    {
                                        employees.Add((reader.GetInt32(reader.GetOrdinal("EmpId")),
                                                       reader.GetString(reader.GetOrdinal("FirstName")),
                                                       reader.GetDateTime(reader.GetOrdinal("Doj"))));
                                    }
                                }
                            }

                            // Get holidays list for the previous month
                            List<DateTime> holidaysList = GetHolidaysList(conn, previousMonth, previousYear);

                            // Process each employee
                            foreach (var emp in employees)
                            {
                                int presents = 0, halfDayCount = 0, absents = 0, lateLogCount = 0, sundayCount = 0, weekOffs = 0;
                                List<DateTime> daysWithRecords = new List<DateTime>();

                                // Fetch Attendance Data for the employee
                                using (SqlCommand cmd = new SqlCommand(@"SELECT CAST(DateOfTransaction AS DATE) AS TransactionDate, 
                                                         MIN(DateOfTransaction) AS MinDateTime, 
                                                         MAX(DateOfTransaction) AS MaxDateTime 
                                                    FROM Attendance  
                                                    WHERE UserId = @UserId AND 
                                                          MONTH(DateOfTransaction) = @Month AND 
                                                          YEAR(DateOfTransaction) = @Year 
                                                    GROUP BY CAST(DateOfTransaction AS DATE)", conn))
                                {
                                    cmd.Parameters.AddWithValue("@UserId", emp.EmpId);
                                    cmd.Parameters.AddWithValue("@Month", previousMonth);
                                    cmd.Parameters.AddWithValue("@Year", previousYear);

                                    using (SqlDataReader reader = cmd.ExecuteReader())
                                    {
                                        while (reader.Read())
                                        {
                                            DateTime transactionDate = reader.GetDateTime(reader.GetOrdinal("TransactionDate"));
                                            TimeSpan punchDuration = reader.GetDateTime(reader.GetOrdinal("MaxDateTime")) - reader.GetDateTime(reader.GetOrdinal("MinDateTime"));

                                            if (transactionDate.DayOfWeek == DayOfWeek.Sunday)
                                            {
                                                sundayCount++;
                                            }
                                            else if (holidaysList.Contains(transactionDate))
                                            {

                                            }
                                            else
                                            {
                                                if (punchDuration.TotalHours > 8)
                                                {
                                                    presents++;
                                                }
                                                else if (punchDuration.TotalHours > 4)
                                                {
                                                    halfDayCount++;
                                                }
                                                else
                                                {
                                                    absents++;
                                                }

                                                // Late log classification
                                                if (reader.GetDateTime(reader.GetOrdinal("MinDateTime")).TimeOfDay > new TimeSpan(9, 40, 0))
                                                {
                                                    lateLogCount++;
                                                }

                                                if (!daysWithRecords.Contains(transactionDate))
                                                {
                                                    daysWithRecords.Add(transactionDate);
                                                }
                                            }
                                        }
                                    }
                                }

                                // Count WeekOffs and Absents
                                DateTime currentDay = firstDayOfPreviousMonth;
                                while (currentDay <= lastDayOfMonth)
                                {
                                    if (currentDay.DayOfWeek == DayOfWeek.Sunday)
                                    {
                                        weekOffs++;
                                    }
                                    else if (!daysWithRecords.Contains(currentDay.Date) && !holidaysList.Contains(currentDay.Date))
                                    {
                                        absents++;
                                    }
                                    currentDay = currentDay.AddDays(1);
                                }

                                // Fetch Leave data
                                // Initialize variables
                                decimal totalCasualLeave = 0, totalSickLeave = 0, totalCompOffLeave = 0, totalLOP = 0;

                                using (SqlCommand cmd = new SqlCommand(@"
    SELECT 
        leave_type,
        ISNULL(SUM(Duration), 0) AS TotalDuration
    FROM 
        LeavesStatus22 
    WHERE 
        MONTH(FromDate) = @SelectedMonth AND 
        YEAR(FromDate) = @SelectedYear AND 
        EmployeedID = @EmpId
    GROUP BY 
        leave_type", conn))
                                {
                                    // Add parameters to the SQL command
                                    cmd.Parameters.AddWithValue("@EmpId", emp.EmpId);
                                    cmd.Parameters.AddWithValue("@SelectedMonth", previousMonth);
                                    cmd.Parameters.AddWithValue("@SelectedYear", previousYear);

                                    // Execute the query
                                    using (SqlDataReader reader = cmd.ExecuteReader())
                                    {
                                        while (reader.Read())
                                        {
                                            string leaveType = reader.GetString(reader.GetOrdinal("leave_type"));
                                            decimal totalDuration = reader.GetDecimal(reader.GetOrdinal("TotalDuration"));

                                            Debug.WriteLine($"LeaveType: {leaveType}, Duration: {totalDuration}");

                                            switch (leaveType)
                                            {
                                                case "Casual Leave":
                                                    totalCasualLeave = totalDuration;
                                                    break;
                                                case "Sick Leave":
                                                    totalSickLeave = totalDuration;
                                                    break;
                                                case "Comp-Off Leave": // Ensure this matches exactly
                                                    totalCompOffLeave = totalDuration;
                                                    break;
                                                case "LOP":
                                                    totalLOP = totalDuration;
                                                    break;
                                                    // Handle other leave types if necessary
                                            }
                                        }

                                    }
                                }

                                // Now totalCasualLeave, totalSickLeave, totalCompOffLeave, and totalLOP have the calculated values


                                // Calculate days
                                double HalfDays = halfDayCount * 0.5;
                                decimal workingDays = totalDays - weekOffs - holidaysList.Count;
                                decimal payableDays = presents + (decimal)HalfDays + weekOffs + holidaysList.Count + totalCasualLeave + totalSickLeave + totalCompOffLeave;
                                decimal LOPS = totalDays - payableDays;

                                // Insert into AttendanceList
                                using (SqlCommand cmd = new SqlCommand(@"INSERT INTO AttendanceList 
                                                      (Name, EmpId, DOJ, FromDate, ToDate, TotalDays, 
                                                       WorkingDays, WeekOffs, HoliDays, Presents, Absents, 
                                                       HalfDays, LateLogs, CL, SL, CompOffs, PaybleDays, 
                                                       LOPs, Year, Month, Status) 
                                                VALUES (@Name, @EmpId, @DOJ, @FromDate, @ToDate, @TotalDays, 
                                                        @WorkingDays, @WeekOffs, @HoliDays, @Presents, @Absents, 
                                                        @HalfDays, @LateLogs, @CL, @SL, @CompOffs, @PaybleDays, 
                                                        @LOPs, @Year, @Month, @Status)", conn))
                                {
                                    cmd.Parameters.AddWithValue("@Name", emp.EmployeeName);
                                    cmd.Parameters.AddWithValue("@EmpId", emp.EmpId);
                                    cmd.Parameters.AddWithValue("@DOJ", emp.DOJ);
                                    cmd.Parameters.AddWithValue("@FromDate", firstDayOfPreviousMonth);
                                    cmd.Parameters.AddWithValue("@ToDate", lastDayOfMonth);
                                    cmd.Parameters.AddWithValue("@TotalDays", totalDays);
                                    cmd.Parameters.AddWithValue("@WorkingDays", workingDays);
                                    cmd.Parameters.AddWithValue("@WeekOffs", weekOffs);
                                    cmd.Parameters.AddWithValue("@HoliDays", holidaysList.Count);
                                    cmd.Parameters.AddWithValue("@Presents", presents);
                                    cmd.Parameters.AddWithValue("@Absents", absents);
                                    cmd.Parameters.AddWithValue("@HalfDays", HalfDays);
                                    cmd.Parameters.AddWithValue("@LateLogs", lateLogCount);
                                    cmd.Parameters.AddWithValue("@CL", totalCasualLeave);
                                    cmd.Parameters.AddWithValue("@SL", totalSickLeave);
                                    cmd.Parameters.AddWithValue("@CompOffs", totalCompOffLeave);
                                    cmd.Parameters.AddWithValue("@PaybleDays", payableDays);
                                    cmd.Parameters.AddWithValue("@LOPs", LOPS);
                                    cmd.Parameters.AddWithValue("@Year", previousYear);
                                    cmd.Parameters.AddWithValue("@Month", previousMonth);
                                    cmd.Parameters.AddWithValue("@Status", 1);

                                    cmd.ExecuteNonQuery();
                                    BindData();
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


        private List<DateTime> GetHolidaysList(SqlConnection conn, int month, int year)
        {
            List<DateTime> holidaysList = new List<DateTime>();

            using (SqlCommand cmd = new SqlCommand("SELECT HolidayDate FROM HolidaysTable WHERE MONTH(HolidayDate) = @Month AND YEAR(HolidayDate) = @Year and Status = 1 and HolidayType != 'Sudden Holiday'", conn))
            {
                cmd.Parameters.AddWithValue("@Month", month);
                cmd.Parameters.AddWithValue("@Year", year);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        holidaysList.Add(reader.GetDateTime(reader.GetOrdinal("HolidayDate")));
                    }
                }
            }

            return holidaysList;
        }



        protected void Button1_Click(object sender, EventArgs e)
        {
            string searchTerm = txtempnamesearch.Text.Trim();

            // Get the selected month and year from the TextBox
            int? selectedMonth = null;
            int? selectedYear = null;

            if (!string.IsNullOrEmpty(txtmonth.Text))
            {
                // Extract year and month from the TextBox value (format: yyyy-MM)
                string[] dateParts = txtmonth.Text.Split('-');
                selectedYear = int.Parse(dateParts[0]);
                selectedMonth = int.Parse(dateParts[1]);
            }
            else
            {
                // Default to previous month if nothing is selected
                DateTime previousMonth = DateTime.Now.AddMonths(-1);
                selectedMonth = previousMonth.Month;
                selectedYear = previousMonth.Year;
            }

            // Fetch the data based on selected values or previous month by default
            DataTable dt = GetAttendanceData(searchTerm, selectedMonth, selectedYear);

            if (dt.Columns.Contains("FirstName"))
            {
                dt.Columns.Remove("FirstName");
            }
            if (dt.Columns.Contains("Status"))
            {
                dt.Columns.Remove("Status");
            }

            string attachment = "attachment; filename=AttendanceData.xls";
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.AddHeader("content-disposition", attachment);
            HttpContext.Current.Response.ContentType = "application/ms-excel";
            HttpContext.Current.Response.Charset = "";

            using (System.IO.StringWriter sw = new System.IO.StringWriter())
            {
                using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                {
                    GridView gridView = new GridView();
                    gridView.DataSource = dt;
                    gridView.DataBind();

                    gridView.RenderControl(hw);
                    HttpContext.Current.Response.Output.Write(sw.ToString());
                    HttpContext.Current.Response.Flush();
                    HttpContext.Current.Response.End();
                }
            }
        }


        protected void btncountupdate_Click(object sender, EventArgs e)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString;
                string NameUpdate = Session["Name"]?.ToString() ?? "Unknown";

                // Parse input values and calculate the new PaybleDays
                decimal presents = string.IsNullOrEmpty(txtpresents.Text) ? 0 : Convert.ToDecimal(txtpresents.Text);
                decimal absents = string.IsNullOrEmpty(txtabsents.Text) ? 0 : Convert.ToDecimal(txtabsents.Text);
                decimal halfDays = string.IsNullOrEmpty(txthalfdays.Text) ? 0 : Convert.ToDecimal(txthalfdays.Text);
                int lateLogs = string.IsNullOrEmpty(txtlatelogs.Text) ? 0 : Convert.ToInt32(txtlatelogs.Text);
                decimal cl = string.IsNullOrEmpty(txtcls.Text) ? 0 : Convert.ToDecimal(txtcls.Text);
                decimal sl = string.IsNullOrEmpty(txtsls.Text) ? 0 : Convert.ToDecimal(txtsls.Text);
                decimal compOffs = string.IsNullOrEmpty(txtcompoffs.Text) ? 0 : Convert.ToDecimal(txtcompoffs.Text);
                decimal lops = string.IsNullOrEmpty(txtlops.Text) ? 0 : Convert.ToDecimal(txtlops.Text);
                decimal weekOffs = string.IsNullOrEmpty(HiddenField4.Value) ? 0 : Convert.ToDecimal(HiddenField4.Value);
                decimal holidays = string.IsNullOrEmpty(HiddenField5.Value) ? 0 : Convert.ToDecimal(HiddenField5.Value);
                // Calculate the new PaybleDays
                decimal paybleDays = presents + halfDays + cl + sl + compOffs + weekOffs + holidays;

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand Cmd = new SqlCommand("UPDATE AttendanceList SET Presents=@Presents, Absents=@Absents, HalfDays=@HalfDays, LateLogs=@LateLogs, CL=@CL, SL=@SL, CompOffs=@CompOffs, PaybleDays=@PaybleDays, LOPs=@LOPs, UpdatedBy=@UpdatedBy, UpdatedDate=@UpdatedDate WHERE Month = @Month AND Year = @Year AND EmpID=@EmpId", conn))
                    {
                        Cmd.Parameters.Add("@Presents", SqlDbType.Decimal).Value = presents;
                        Cmd.Parameters.Add("@Absents", SqlDbType.Decimal).Value = absents;
                        Cmd.Parameters.Add("@HalfDays", SqlDbType.Decimal).Value = halfDays;
                        Cmd.Parameters.Add("@LateLogs", SqlDbType.Int).Value = lateLogs;
                        Cmd.Parameters.Add("@CL", SqlDbType.Decimal).Value = cl;
                        Cmd.Parameters.Add("@SL", SqlDbType.Decimal).Value = sl;
                        Cmd.Parameters.Add("@CompOffs", SqlDbType.Decimal).Value = compOffs;
                        Cmd.Parameters.Add("@PaybleDays", SqlDbType.Decimal).Value = paybleDays; // Updated PaybleDays
                        Cmd.Parameters.Add("@LOPs", SqlDbType.Decimal).Value = lops;
                        Cmd.Parameters.Add("@Year", SqlDbType.VarChar).Value = HiddenField2.Value;
                        Cmd.Parameters.Add("@Month", SqlDbType.VarChar).Value = HiddenField3.Value;
                        Cmd.Parameters.Add("@EmpId", SqlDbType.VarChar).Value = HiddenField1.Value;
                        Cmd.Parameters.Add("@UpdatedBy", SqlDbType.VarChar).Value = NameUpdate;
                        Cmd.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = DateTime.Now;

                        int rowsAffected = Cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            Response.Write("<script>alert('" + txtname.Text + " Attendance Count Updated.....!')</script>");
                            BindData();
                        }
                        else
                        {
                            Response.Write("<script>alert('Update Failed.....?')</script>");
                            BindData();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error: " + ex.Message + "')</script>");
            }

        }

        protected void txtmonth_TextChanged(object sender, EventArgs e)
        {
            DateTime selectedMonth;
            string name = txtempnamesearch.Text.Trim();

            if (DateTime.TryParse(txtmonth.Text, out selectedMonth))
            {
                // Call BindData with the selected month, year, and name (if any)
                BindData(name, selectedMonth.Month, selectedMonth.Year);
            }
            else
            {
                // Handle the case where the month is not valid (optional)
                // Display some message or handle the error
            }
        }
    }
}