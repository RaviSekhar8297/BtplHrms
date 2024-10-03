using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Human_Resource_Management
{
    public partial class AdminPromotion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Initialize the calendar with the current month
                DateTime today = DateTime.Today;
                ViewState["DisplayedMonth"] = new DateTime(today.Year, today.Month, 1);
                LoadCalendarForMonth((DateTime)ViewState["DisplayedMonth"]);
            }
        }
        protected void btnPrev_Click(object sender, EventArgs e)
        {
            // Move to the previous month
            DateTime currentMonth = (DateTime)ViewState["DisplayedMonth"];
            DateTime prevMonth = currentMonth.AddMonths(-1);
            ViewState["DisplayedMonth"] = prevMonth;
            LoadCalendarForMonth(prevMonth);
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            // Move to the next month
            DateTime currentMonth = (DateTime)ViewState["DisplayedMonth"];
            DateTime nextMonth = currentMonth.AddMonths(1);
            ViewState["DisplayedMonth"] = nextMonth;
            LoadCalendarForMonth(nextMonth);
        }
        private void LoadCalendarForMonth(DateTime month)
        {
            // Update the label to show the current month and year
            lblMonthYear.Text = month.ToString("MMMM yyyy");

            // Fetch the attendance data for the selected month
            DataTable attendanceData = GetAttendanceDataForMonth(month);

            // Fetch the holiday data for the selected month
            DataTable holidayData = GetHolidayData(month);

            // Fetch the leave data for the selected month
            DataTable leaveData = GetLeaveData(month);

            // Generate the calendar with the retrieved data
            GenerateCalendar(attendanceData, holidayData, leaveData, month);
        }

        private DataTable GetAttendanceDataForMonth(DateTime month)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString;
            string userId = Session["EmpId"]?.ToString();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @" SELECT CAST(DateOfTransaction AS DATE) AS AttendanceDate, MIN(CONVERT(TIME, DateOfTransaction)) AS MinTime, MAX(CONVERT(TIME, DateOfTransaction)) AS MaxTime FROM Attendance WHERE UserId = @UserId AND YEAR(DateOfTransaction) = @Year AND MONTH(DateOfTransaction) = @Month GROUP BY CAST(DateOfTransaction AS DATE) ORDER BY AttendanceDate";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserId", userId);
                    command.Parameters.AddWithValue("@Year", month.Year);
                    command.Parameters.AddWithValue("@Month", month.Month);

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    return dt;
                }
            }
        }

        private void GenerateCalendar(DataTable attendanceData, DataTable holidayData, DataTable leaveData, DateTime month)
        {
            StringBuilder sb = new StringBuilder();

            // Basic calendar structure
            sb.Append("<table class='attendance-calendar'>");

            // Weekday headers
            sb.Append("<tr>");
            string[] daysOfWeek = { "Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat" };
            foreach (string day in daysOfWeek)
            {
                sb.AppendFormat("<th>{0}</th>", day);
            }
            sb.Append("</tr>");

            // First day of the month and total days
            DateTime firstDayOfMonth = new DateTime(month.Year, month.Month, 1);
            int daysInMonth = DateTime.DaysInMonth(month.Year, month.Month);
            int startDayOfWeek = (int)firstDayOfMonth.DayOfWeek;

            // Fill in blank cells before the first day
            sb.Append("<tr>");
            for (int i = 0; i < startDayOfWeek; i++)
            {
                sb.Append("<td></td>");
            }

            // Loop through the days of the month
            for (int day = 1; day <= daysInMonth; day++)
            {
                DateTime currentDate = new DateTime(month.Year, month.Month, day);

                // Check if it's a holiday
                bool isHoliday = holidayData.Select($"HolidayDate = '{currentDate:yyyy-MM-dd}'").Length > 0;

                // Check for leave
                DataRow[] leaveRows = leaveData.Select($"FromDate <= '{currentDate:yyyy-MM-dd}' AND ToDate >= '{currentDate:yyyy-MM-dd}'");

                // Check attendance data
                DataRow[] attendanceRows = attendanceData.Select($"AttendanceDate = '{currentDate:yyyy-MM-dd}'");

                sb.Append("<td");

                // Apply background colors and display logic
                if (currentDate.DayOfWeek == DayOfWeek.Sunday)
                {
                    sb.Append(" class='sunday'");
                }
                else if (isHoliday)
                {
                    sb.Append(" class='holiday'");
                }
                else if (attendanceRows.Length > 0)
                {
                    sb.Append(" class='present'");
                }
                else
                {
                    sb.Append(" class='absent'");
                }

                sb.Append(">");

                // Display the day number
                sb.AppendFormat("<div>{0}</div>", day);

                // Display holiday image if it's a holiday
                if (isHoliday)
                {
                    sb.Append("<img class='holiday-logo' src='https://data.textstudio.com/output/sample/animated/5/5/9/4/holidays-27-4955.gif' alt='Holiday'>");
                }
                else if (attendanceRows.Length > 0)
                {
                    // Get Min/Max times for attendance
                    TimeSpan minTime = (TimeSpan)attendanceRows[0]["MinTime"];
                    TimeSpan maxTime = (TimeSpan)attendanceRows[0]["MaxTime"];

                    // Calculate the duration
                    TimeSpan duration = maxTime - minTime;

                    // Display MinTime - MaxTime, and show duration on hover
                    sb.AppendFormat(
                        "<div class='attendance-time' title='Duration: {2:hh\\:mm}'>" +
                        "{0:hh\\:mm} - {1:hh\\:mm}</div>",
                        minTime, maxTime, duration);
                }
                else if (leaveRows.Length > 0)
                {
                    // Display leave details
                    string leaveType = leaveRows[0]["leave_type"].ToString();
                    string leaveAbbreviation;

                    switch (leaveType)
                    {
                        case "Sick Leave":
                            leaveAbbreviation = "SL";
                            break;
                        case "Casual Leave":
                            leaveAbbreviation = "CL";
                            break;
                        case "Comp-Off Leave":
                            leaveAbbreviation = "Co-L";
                            break;
                        default:
                            leaveAbbreviation = leaveType; // Fallback if the type is not in the list
                            break;
                    }

                    sb.AppendFormat("<div>{0}</div>", leaveAbbreviation);
                }
                else
                {
                    sb.Append("<div>Absent</div>");
                }

                sb.Append("</td>");

                // End the row at the end of the week (Saturday)
                if ((day + startDayOfWeek) % 7 == 0)
                {
                    sb.Append("</tr><tr>");
                }
            }

            // Fill remaining cells for incomplete weeks
            int remainingCells = (startDayOfWeek + daysInMonth) % 7;
            if (remainingCells > 0)
            {
                for (int i = remainingCells; i < 7; i++)
                {
                    sb.Append("<td></td>");
                }
            }

            sb.Append("</tr>");
            sb.Append("</table>");

            // Display the calendar
            calendarLiteral.Text = sb.ToString();
        }




        private DataTable GetHolidayData(DateTime month)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @" SELECT HolidayDate FROM HolidaysTable WHERE MONTH(HolidayDate) = @Month AND YEAR(HolidayDate) = @Year";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Month", month.Month);
                    command.Parameters.AddWithValue("@Year", month.Year);

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable holidayTable = new DataTable();
                    adapter.Fill(holidayTable);
                    return holidayTable;
                }
            }
        }
        private DataTable GetLeaveData(DateTime month)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @" SELECT FromDate, ToDate, leave_type FROM LeavesStatus22 WHERE (MONTH(FromDate) = @Month OR MONTH(ToDate) = @Month) AND (YEAR(FromDate) = @Year OR YEAR(ToDate) = @Year)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Month", month.Month);
                    command.Parameters.AddWithValue("@Year", month.Year);

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable leaveTable = new DataTable();
                    adapter.Fill(leaveTable);
                    return leaveTable;
                }
            }
        }


    }
}