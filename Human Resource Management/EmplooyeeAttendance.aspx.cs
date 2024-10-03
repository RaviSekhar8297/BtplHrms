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
using System.IO;
using ClosedXML.Excel;
using System.Diagnostics;

namespace Human_Resource_Management
{
    public partial class EmplooyeeAttendance : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {            
                BindPunch();
                BindCount();
                TotalInTimeOutTimeDisplay();
                tablePanel.Visible = false;
                btnexcel.Visible = false;

                WeeklyTimeData();
                MonthlyTimeData();
            }
        }
        public void BindPunch()
        {
            // Default shift times
            string defaultStartTime = "09:30 AM"; // Example start time
            string defaultEndTime = "06:30 PM";   // Example end time

            // Reset label values to default
            lblovertime.Text = "00:00";
            lbllatelogintime.Text = "00:00";
            lblpunchdatettime.Text = TimeSpan.Zero.ToString(@"hh\:mm");
            lbltodayprogress.Text = "Not Started";
            Label1.Text = "00:00"; // Reset total working hours

            try
            {
                if (Session["EmpId"] != null && ConfigurationManager.ConnectionStrings["NewHRMSString"] != null)
                {
                    string userId = Session["EmpId"].ToString();
                    string connectionString = ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString;

                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        con.Open();

                        // Get punch times from Attendance table for the current day
                        SqlCommand cmd = new SqlCommand(@"SELECT DateOfTransaction 
                                    FROM Attendance 
                                    WHERE UserId = @UserId 
                                    AND CONVERT(date, DateOfTransaction) = CONVERT(date, GETDATE()) 
                                    ORDER BY DateOfTransaction;", con);
                        cmd.Parameters.AddWithValue("@UserId", userId);

                        List<DateTime> punchTimesList = new List<DateTime>();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                DateTime punchTime = Convert.ToDateTime(reader["DateOfTransaction"]);
                                punchTimesList.Add(punchTime);
                            }
                        }

                        // Check if there are punch times for today
                        if (punchTimesList.Count > 0)
                        {
                            DateTime mintime = punchTimesList.Min(); // Get the earliest punch time
                            DateTime maxtime = punchTimesList.Max(); // Get the latest punch time
                            lblpunchdatettime.Text = mintime.ToString(@"hh\:mm tt"); // Format output

                            // 2. Check for ShiftAssign record for the employee for today's date
                            SqlCommand cmdShift = new SqlCommand("SELECT ShiftTime FROM ShiftAssign WHERE EmpId = @EmpId AND ShiftDate = CONVERT(date, GETDATE())", con);
                            cmdShift.Parameters.AddWithValue("@EmpId", userId);

                            object shiftResult = cmdShift.ExecuteScalar();
                            string currentStartTime = defaultStartTime;
                            string currentEndTime = defaultEndTime;

                            if (shiftResult != null)
                            {
                                Label2.Text = shiftResult.ToString();
                                // ShiftAssign found, process ShiftTime (e.g., "07:00 AM - 03:00 PM")
                                string shiftTime = shiftResult.ToString();
                                string[] times = shiftTime.Split(new string[] { " - " }, StringSplitOptions.None);
                                if (times.Length == 2)
                                {
                                    currentStartTime = times[0];
                                    currentEndTime = times[1];
                                }
                            }
                            else
                            {
                                Label2.Text = $"{defaultStartTime} - {defaultEndTime}";

                            }

                            // Parse start and end times
                            DateTime parsedCurrentStartTime = DateTime.ParseExact(currentStartTime, "hh:mm tt", CultureInfo.InvariantCulture);
                            DateTime parsedCurrentEndTime = DateTime.ParseExact(currentEndTime, "hh:mm tt", CultureInfo.InvariantCulture);
                            DateTime graceTime = parsedCurrentStartTime.AddMinutes(15);

                            // Calculate total working hours and overtime
                            TimeSpan totalWorkingHours = TimeSpan.Zero;

                            // Calculate working hours between pairs
                            for (int i = 0; i < punchTimesList.Count - 1; i += 2)
                            {
                                TimeSpan difference = punchTimesList[i + 1] - punchTimesList[i];
                                totalWorkingHours += difference;
                            }

                            // Update the label for total working hours
                            Label1.Text = totalWorkingHours.ToString(@"hh\:mm");

                            // Calculate overtime
                            TimeSpan startingOvertime = TimeSpan.Zero;
                            TimeSpan endingOvertime = TimeSpan.Zero;

                            // Check if the first punch time is before the start time
                            if (mintime < parsedCurrentStartTime)
                            {
                                startingOvertime = parsedCurrentStartTime - mintime; // Overtime before the start time
                            }

                            // Check if the last punch time is after the end time
                            if (maxtime > parsedCurrentEndTime)
                            {
                                endingOvertime = maxtime - parsedCurrentEndTime; // Overtime after the end time
                            }

                            // Combine both overtime calculations
                            TimeSpan overtime = startingOvertime + endingOvertime;

                            // Update the label for overtime
                            lblovertime.Text = overtime.ToString(@"hh\:mm");

                            // Check if the earliest punch time (mintime) is greater than the grace time
                            if (mintime > graceTime)
                            {
                                // If punch time is after grace time, calculate late login
                                TimeSpan lateLoginDiff = mintime - parsedCurrentStartTime;
                                lbllatelogintime.Text = lateLoginDiff.ToString(@"hh\:mm");
                            }
                            else
                            {
                                // No late time if the punch is before or at the grace time
                                lbllatelogintime.Text = "00:00";
                            }

                            lbltodayprogress.Text = totalWorkingHours.ToString(@"hh\:mm");
                        }
                        else
                        {
                            // Handle case where there are no punch records for today
                            lblpunchdatettime.Text = "No Punches Today";
                            lbllatelogintime.Text = "00:00";
                            Label1.Text = "00:00";
                            lbltodayprogress.Text = "Not Started";
                            lblovertime.Text = "00:00";
                            Label2.Text = $"{defaultStartTime} - {defaultEndTime}";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Consider logging the exception
                throw ex;
            }
        }

        public void BindCount()
        {
            try
            {
                if (Session["EmpId"] != null && Session["EmpId"].ToString() != "")
                {
                    string userId = Session["EmpId"].ToString();
                    string connectionString = ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString;
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        con.Open();

                        string Query7days = @"DECLARE @Past7DaysStart DATE = DATEADD(day, -7, CAST(GETDATE() AS DATE)); DECLARE @Past7DaysEnd DATE = CAST(GETDATE() AS DATE); SELECT @Past7DaysStart AS Past7DaysStart,@Past7DaysEnd AS Past7DaysEnd,CONVERT(varchar(8), CONVERT(time, DATEADD(second, DATEDIFF(second, MIN(DateOfTransaction), MAX(DateOfTransaction)), 0)), 108) AS TimeDifference FROM Attendance  WHERE UserId = @UserId AND DateOfTransaction >= @Past7DaysStart AND DateOfTransaction < @Past7DaysEnd AND DATEPART(dw, DateOfTransaction) != 1;";


                        string queryMonthlyData = "DECLARE @CurrentDate DATE = CAST(GETDATE() AS DATE);DECLARE @StartOfMonth DATE = DATEADD(DAY, 1 - DAY(@CurrentDate), @CurrentDate);DECLARE @EndOfMonth DATE = EOMONTH(@CurrentDate);DECLARE @StartOfYear DATE = DATEFROMPARTS(YEAR(@CurrentDate), 1, 1);DECLARE @EndOfYear DATE = DATEFROMPARTS(YEAR(@CurrentDate), 12, 31);SELECT @StartOfMonth AS StartOfMonth,@EndOfMonth AS EndOfMonth,@StartOfYear AS StartOfYear,@EndOfYear AS EndOfYear,CONVERT(varchar(8), CONVERT(time, DATEADD(SECOND, DATEDIFF(SECOND, MIN(DateOfTransaction), MAX(DateOfTransaction)), 0)), 108) AS TimeDifference FROM Attendance WHERE UserId = @UserId AND((DateOfTransaction >= @StartOfMonth AND DateOfTransaction <= @EndOfMonth) OR(DateOfTransaction >= @StartOfYear AND DateOfTransaction <= @EndOfYear)) AND DATEPART(dw, DateOfTransaction) != 1;";

                        SqlCommand cmd7days = new SqlCommand(Query7days, con);
                        SqlCommand cmdmonthly = new SqlCommand(queryMonthlyData, con);

                        cmd7days.Parameters.AddWithValue("@UserId", userId);
                        cmdmonthly.Parameters.AddWithValue("@UserId", userId);


                        using (SqlDataReader reader1 = cmd7days.ExecuteReader())
                        {
                            if (reader1.Read())
                            {
                               // lblweeklyprogress.Text = reader1["TimeDifference"].ToString();
                            }
                            else
                            {
                              //  lblweeklyprogress.Text = "No data s";
                            }
                        }

                        using (SqlDataReader reader2 = cmdmonthly.ExecuteReader())
                        {
                            if (reader2.Read())
                            {
                                lblmonthlyprogress.Text = reader2["TimeDifference"].ToString();
                            }
                            else
                            {
                                lblmonthlyprogress.Text = "No data M";
                            }
                        }
                    }
                }
                else
                {

                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public void WeeklyTimeData()
        {
            // Initialize the total difference
            TimeSpan totalDifference = TimeSpan.Zero;

            // Retrieve the current date
            DateTime currentDate = DateTime.Today;

            // Iterate over the last 7 days
            for (int i = 0; i < 7; i++)
            {
                // Calculate the date for the current iteration
                DateTime targetDate = currentDate.AddDays(-i);

                // Retrieve the transactions for the target date and calculate the day difference
                TimeSpan dayDifference = CalculateDayDifference(targetDate);

                // Accumulate the day difference to the total difference
                totalDifference += dayDifference;
            }

            // Display the total time count in a label
            lblweeklyprogress.Text = totalDifference.ToString(@"hh\:mm\:ss");
        }

        private TimeSpan CalculateDayDifference(DateTime targetDate)
        {
            TimeSpan dayDifference = TimeSpan.Zero;

            // Check if the EmpId session variable is null
            if (Session["EmpId"] == null)
            {
                return dayDifference; // Return 0 if there is no EmpId
            }

            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
            {
                connection.Open();

                // Retrieve the transactions for the target date
                string query = "SELECT DateOfTransaction FROM Attendance WHERE UserId = @UserId AND CAST(DateOfTransaction AS DATE) = @TargetDate ORDER BY DateOfTransaction ASC";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@TargetDate", targetDate);
                command.Parameters.AddWithValue("@UserId", Session["EmpId"].ToString());

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    // Initialize prevTransactionTime to null for each new day
                    DateTime? prevTransactionTime = null;

                    // Iterate over the transactions for the target date
                    while (reader.Read())
                    {
                        DateTime currentTransactionTime = (DateTime)reader["DateOfTransaction"];
                        Console.WriteLine($"Transaction time: {currentTransactionTime}");

                        if (prevTransactionTime != null)
                        {
                            // Calculate the time difference between consecutive transactions
                            TimeSpan difference = currentTransactionTime - prevTransactionTime.Value;
                            dayDifference += difference;
                        }

                        // Update prevTransactionTime for the next iteration
                        prevTransactionTime = currentTransactionTime;
                    }
                }
            }

            Console.WriteLine($"Day difference for {targetDate}: {dayDifference}");
            return dayDifference;
        }



        public void MonthlyTimeData()
        {
            TimeSpan totalDifference = TimeSpan.Zero;

            // Retrieve the current date
            DateTime currentDate = DateTime.Today;

            // Iterate over the last 7 days
            for (int i = 0; i < 30; i++)
            {
                // Calculate the date for the current iteration
                DateTime targetDate = currentDate.AddDays(-i);

                // Retrieve the transactions for the target date and calculate the day difference
                TimeSpan dayDifference = CalculateMonthlyDifference(targetDate);

                // Accumulate the day difference to the total difference
                totalDifference += dayDifference;
            }

            lblmonthlyprogress.Text = totalDifference.ToString(@"hh\:mm\:ss");
        }

        private TimeSpan CalculateMonthlyDifference(DateTime targetDate)
        {
            TimeSpan dayDifference = TimeSpan.Zero;

            // Connect to the database
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
            {
                connection.Open();

                // Retrieve the transactions for the target date
                string query = "SELECT DateOfTransaction FROM Attendance WHERE UserId = @UserId AND CAST(DateOfTransaction AS DATE) = @TargetDate ORDER BY DateOfTransaction ASC";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@TargetDate", targetDate);
                command.Parameters.AddWithValue("@UserId", Session["EmpId"].ToString());


                using (SqlDataReader reader = command.ExecuteReader())
                {
                    // Initialize prevTransactionTime to null for each new day
                    DateTime? prevTransactionTime = null;

                    // Iterate over the transactions for the target date
                    while (reader.Read())
                    {
                        DateTime currentTransactionTime = (DateTime)reader["DateOfTransaction"];
                        Console.WriteLine($"Transaction time: {currentTransactionTime}");

                        if (prevTransactionTime != null)
                        {
                            // Calculate the time difference between consecutive transactions
                            TimeSpan difference = currentTransactionTime - prevTransactionTime.Value;
                            dayDifference += difference;

                        }

                        // Update prevTransactionTime for the next iteration
                        prevTransactionTime = currentTransactionTime;
                    }
                }
            }

            Console.WriteLine($"Day difference for {targetDate}: {dayDifference}");
            return dayDifference;
        }



        protected void btnpunchout_Click(object sender, EventArgs e)
        {
            string UserId = Session["EmpId"].ToString();
            int flag = 1;
            string getdate = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss tt");

            string latitude = Latitude.Value;
            string longitude = Longitude.Value;
            string outdoor = "MobilePunched";
            try
            {
                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    using (SqlCommand sqlcmd = new SqlCommand("INSERT INTO Attendance( OrgId,UserId,UserName,MachineId,DateOfTransaction,Direction,AttDirection,AlternateAttDirection,StatusCode,WorkCode," +
                        "Duration,Remarks,SMSFlag,VerificationMode,IsApproved,LogRecordLocation,ManagerRemarks,AttMarkAndroidIMEINo,AttenndanceMarkingType,Lattitude,Longitude,NetworkLattitude,NetworkLongitude,NetworkLocation," +
                        "ApprovedBy,LogsApproveDeclineDateTime,Temperature,TemperatureState,GeoFenceLocationId,IsSuccessAPI,APIResponseText,sIOMode,OutTimeRemarks,InTimeRemarks,OutDoor,flag,Status) values (@OrgId,@UserId,@EmpName,@MachineId,@DateOfTransaction,@Direction,@AttDirection,@AlternateAttDirection,@StatusCode,@WorkCode,@Duration,@Remarks,@SMSFlag,@VerificationMode,@IsApproved,@LogRecordLocation,@ManagerRemarks,@AttMarkAndroidIMEINo,@AttenndanceMarkingType,@Lattitude,@Longitude,@NetworkLattitude,@NetworkLongitude,@NetworkLocation,@ApprovedBy,@LogsApproveDeclineDateTime,@Temperature,@TemperatureState,@GeoFenceLocationId,@IsSuccessAPI,@APIResponseText,@sIOMode,@OutTimeRemarks,@InTimeRemarks,@OutDoor,@flag,@Status)", connection))
                    {
                        connection.Open();
                        sqlcmd.Parameters.AddWithValue("@OrgId", "9999");
                        sqlcmd.Parameters.AddWithValue("@MachineId", Session["BranchCode"].ToString());
                        sqlcmd.Parameters.AddWithValue("@UserId", Session["EmpId"].ToString());
                        sqlcmd.Parameters.AddWithValue("@EmpName", Session["Name"].ToString());                        
                        sqlcmd.Parameters.AddWithValue("@DateOfTransaction", getdate);
                        sqlcmd.Parameters.AddWithValue("@OutDoor", outdoor);

                        sqlcmd.Parameters.AddWithValue("@Direction", DBNull.Value);
                        sqlcmd.Parameters.AddWithValue("@AttDirection", DBNull.Value);
                        sqlcmd.Parameters.AddWithValue("@AlternateAttDirection", DBNull.Value);
                        sqlcmd.Parameters.AddWithValue("@StatusCode", DBNull.Value);
                        sqlcmd.Parameters.AddWithValue("@WorkCode", DBNull.Value);
                        sqlcmd.Parameters.AddWithValue("@Duration", DBNull.Value);
                        sqlcmd.Parameters.AddWithValue("@Remarks", DBNull.Value);
                        sqlcmd.Parameters.AddWithValue("@SMSFlag", DBNull.Value);
                        sqlcmd.Parameters.AddWithValue("@VerificationMode", DBNull.Value);
                        sqlcmd.Parameters.AddWithValue("@IsApproved", DBNull.Value);
                        sqlcmd.Parameters.AddWithValue("@LogRecordLocation", DBNull.Value);
                        sqlcmd.Parameters.AddWithValue("@ManagerRemarks", DBNull.Value);
                        sqlcmd.Parameters.AddWithValue("@AttMarkAndroidIMEINo", DBNull.Value);
                        sqlcmd.Parameters.AddWithValue("@AttenndanceMarkingType", DBNull.Value);
                        sqlcmd.Parameters.AddWithValue("@Lattitude", latitude);
                        sqlcmd.Parameters.AddWithValue("@Longitude", longitude);
                        sqlcmd.Parameters.AddWithValue("@NetworkLattitude", DBNull.Value);
                        sqlcmd.Parameters.AddWithValue("@NetworkLongitude", DBNull.Value);
                        sqlcmd.Parameters.AddWithValue("@NetworkLocation", DBNull.Value);
                        sqlcmd.Parameters.AddWithValue("@ApprovedBy", DBNull.Value);
                        sqlcmd.Parameters.AddWithValue("@LogsApproveDeclineDateTime", DBNull.Value);
                        sqlcmd.Parameters.AddWithValue("@Temperature", DBNull.Value);
                        sqlcmd.Parameters.AddWithValue("@TemperatureState", DBNull.Value);
                        sqlcmd.Parameters.AddWithValue("@GeoFenceLocationId", DBNull.Value);
                        sqlcmd.Parameters.AddWithValue("@IsSuccessAPI", DBNull.Value);
                        sqlcmd.Parameters.AddWithValue("@APIResponseText", DBNull.Value);
                        sqlcmd.Parameters.AddWithValue("@sIOMode", DBNull.Value);
                        sqlcmd.Parameters.AddWithValue("@OutTimeRemarks", DBNull.Value);
                        sqlcmd.Parameters.AddWithValue("@InTimeRemarks", DBNull.Value);                        
                        sqlcmd.Parameters.AddWithValue("@flag", flag);
                        sqlcmd.Parameters.AddWithValue("@Status", flag);

                        int i = sqlcmd.ExecuteNonQuery();
                        if (i > 0)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Punched Success...');", true);
                            BindPunch();
                            TotalInTimeOutTimeDisplay();
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Failed...');", true);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        protected void btnattendancesearch_Click(object sender, EventArgs e)
        {
            TotalInTimeOutTimeDisplay();
 
            tablePanel.Visible = true;
            try
            {
                if (Session["EmpId"] != null && !string.IsNullOrEmpty(Session["EmpId"].ToString()))
                {
                    using (var connection1 = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                    {
                        connection1.Open();
                        DataSet ds1 = new DataSet();

                        using (SqlCommand sqlcmd = new SqlCommand("SELECT CONVERT(date, DateOfTransaction) AS DateOfTransaction, CONVERT(time, MIN(DateOfTransaction)) AS MinTimeOfTransaction, CONVERT(time, MAX(DateOfTransaction)) AS MaxTimeOfTransaction, CONVERT(varchar(5), DATEADD(minute, DATEDIFF(minute, MIN(DateOfTransaction), MAX(DateOfTransaction)), 0), 108) AS TimeDifferenceInHHMM FROM Attendance WHERE UserId = @UserId AND DateOfTransaction BETWEEN @startdate AND DATEADD(day, 1, @enddate) GROUP BY CONVERT(date, DateOfTransaction) ORDER BY DateOfTransaction DESC", connection1))
                        {
                            sqlcmd.Parameters.AddWithValue("@UserId", Session["EmpId"].ToString());
                            sqlcmd.Parameters.AddWithValue("@startdate", DateTime.Parse(txtstartdate.Text.Trim()));
                            sqlcmd.Parameters.AddWithValue("@enddate", DateTime.Parse(txtenddate.Text.Trim()));

                            using (SqlDataAdapter da1 = new SqlDataAdapter(sqlcmd))
                            {
                                da1.Fill(ds1);

                                if (ds1.Tables[0].Rows.Count > 0)
                                {
                                    int rowNum = 1;
                                    foreach (DataRow row in ds1.Tables[0].Rows)
                                    {
                                        DateTime transactionDate = Convert.ToDateTime(row["DateOfTransaction"]);
                                        string Date = transactionDate.ToString("yyyy-MM-dd");
                                        string MinTimeOfTransaction = row["MinTimeOfTransaction"].ToString();
                                        string MaxTimeOfTransaction = row["MaxTimeOfTransaction"].ToString();
                                        string TotalDuration = row["TimeDifferenceInHHMM"].ToString();

                                        StringBuilder projectHtml = new StringBuilder();
                                        projectHtml.Append("<tr>");
                                        projectHtml.Append("<td>" + rowNum + "</td>");
                                        projectHtml.Append("<td>" + Date + "</td>");
                                        projectHtml.Append("<td>" + MinTimeOfTransaction + "</td>");
                                        projectHtml.Append("<td>" + MaxTimeOfTransaction + "</td>");
                                        projectHtml.Append("<td>" + TotalDuration + "</td>");

                                        TimeSpan defaultStartTime = TimeSpan.Parse("09:30");
                                        TimeSpan actualInTime = TimeSpan.Parse(row["MinTimeOfTransaction"].ToString());
                                        TimeSpan lateDuration = actualInTime - defaultStartTime;
                                        string LateLogIn;

                                        if (defaultStartTime > actualInTime)
                                        {
                                            LateLogIn = "00:00";
                                        }
                                        else
                                        {
                                            LateLogIn = lateDuration.ToString(@"hh\:mm");
                                        }

                                        TimeSpan DefaultTimeMorning = TimeSpan.Parse("09:30");
                                        TimeSpan DefaultTimeEvening = TimeSpan.Parse("18:30");

                                        TimeSpan actualTimeMorning = TimeSpan.Parse(row["MinTimeOfTransaction"].ToString());
                                        TimeSpan actualTimeEvening = TimeSpan.Parse(row["MaxTimeOfTransaction"].ToString());
                                        TimeSpan morningOvertime = TimeSpan.Zero;
                                        TimeSpan eveningOvertime = TimeSpan.Zero;

                                        if (actualTimeMorning < DefaultTimeMorning)
                                        {
                                            morningOvertime = actualTimeMorning - DefaultTimeMorning;
                                        }

                                        if (actualTimeEvening > DefaultTimeEvening)
                                        {
                                            eveningOvertime = DefaultTimeEvening - actualTimeEvening;
                                        }

                                        if (actualTimeMorning == TimeSpan.Zero)
                                        {
                                            morningOvertime = TimeSpan.Zero;
                                        }

                                        if (actualTimeEvening == TimeSpan.Zero)
                                        {
                                            eveningOvertime = TimeSpan.Zero;
                                        }

                                        TimeSpan totalOvertime = morningOvertime + eveningOvertime;
                                        string totalOvertimeString = totalOvertime.ToString(@"hh\:mm");

                                        projectHtml.Append("<td>" + LateLogIn + "</td>");
                                        projectHtml.Append("<td>" + totalOvertimeString + "</td>");

                                        projectHtml.Append("</tr>");

                                        dataAttendanceContainer.Controls.Add(new LiteralControl(projectHtml.ToString()));
                                        rowNum++;
                                    }
                                    btnexcel.Visible = true;
                                    GridView1.Visible = false;
                                    GridView1.DataSource = ds1.Tables[0];
                                    GridView1.DataBind();

                                    // Store DataTable in ViewState for export
                                    ViewState["SearchAttendanceDataTable"] = ds1.Tables[0];
                                }
                                else
                                {
                                    Response.Write("<script>alert('No data found for the specified date range.')</script>");
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
        public void TotalInTimeOutTimeDisplay()
        {
            try
            {
                if (Session["EmpId"] != null && Session["EmpId"].ToString() != "")
                {
                    using (var connection1 = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                    {
                        connection1.Open();
                        DataSet ds1 = new DataSet();
                        using (SqlCommand sqlcmd = new SqlCommand("SELECT FORMAT(DateOfTransaction, 'HH:mm:ss') AS TimeOfTransaction FROM Attendance WHERE UserId = @UserId AND CONVERT(date, DateOfTransaction) = CAST(GETDATE() AS DATE) ORDER BY  DateOfTransaction ASC;", connection1))
                        {
                            sqlcmd.Parameters.AddWithValue("@UserId", Session["EmpId"].ToString());
                            using (SqlDataAdapter da = new SqlDataAdapter(sqlcmd))
                            {
                                da.Fill(ds1);
                                if (ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                                {
                                    List<string> timeList = new List<string>();
                                    foreach (DataRow row in ds1.Tables[0].Rows)
                                    {
                                        string time = row["TimeOfTransaction"].ToString();
                                        timeList.Add(time);
                                    }

                                    // Loop through the timeList to generate the HTML
                                    StringBuilder projectHtml = new StringBuilder();
                                    projectHtml.Append("<ul class='res-activity-list'>");

                                    for (int i = 0; i < timeList.Count; i += 2) // Increment by 2 to handle Punch In and Punch Out
                                    {
                                        projectHtml.Append("<li>");
                                        projectHtml.Append("<p class='mb-0'>Punch In at</p>");
                                        projectHtml.Append("<p class='res-activity-time'><i class='fa-regular fa-clock' style='color:#0f39a3'> </i> " + timeList[i] + "</p>");
                                        projectHtml.Append("</li>");

                                        if (i + 1 < timeList.Count) // Ensure there's a Punch Out time available
                                        {
                                            projectHtml.Append("<li>");
                                            projectHtml.Append("<p class='mb-0'>Punch Out at</p>");
                                            projectHtml.Append("<p class='res-activity-time'><i class='fa-regular fa-clock' style='color:red;'> </i> " + timeList[i + 1] + "</p>");
                                            projectHtml.Append("</li>");
                                        }
                                    }

                                    projectHtml.Append("</ul>");
                                    BindAttendanceContainer.Controls.Add(new LiteralControl(projectHtml.ToString()));
                                }
                            }
                        }


                    }
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        //public void GridDataBind()
        //{
        //    try
        //    {
        //        if (Session["EmpId"] != null && Session["EmpId"].ToString() != "")
        //        {
        //            DataSet ds1 = GetAttendanceData(txtstartdate.Text, txtenddate.Text); // Assuming you have a method to get the attendance data
        //            DataTable dt = new DataTable();
        //            dt.Columns.Add("RowNumber", typeof(int));
        //            dt.Columns.Add("Date", typeof(string));
        //            dt.Columns.Add("MinTimeOfTransaction", typeof(string));
        //            dt.Columns.Add("MaxTimeOfTransaction", typeof(string));
        //            dt.Columns.Add("TotalDuration", typeof(string));
        //            dt.Columns.Add("LateLogIn", typeof(string));
        //            dt.Columns.Add("TotalOvertime", typeof(string));

        //            int rowNum = 1;
        //            foreach (DataRow row in ds1.Tables[0].Rows)
        //            {
        //                string dateString = row["DateOfTransaction"].ToString();
        //                DateTime transactionDate;
        //                if (DateTime.TryParseExact(dateString, "yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture, DateTimeStyles.None, out transactionDate))
        //                {
        //                    string Date = transactionDate.ToString("yyyy-MM-dd"); // date of DateOfTransaction
        //                    string MinTimeOfTransaction = row["MinTimeOfTransaction"].ToString();
        //                    string MaxTimeOfTransaction = row["MaxTimeOfTransaction"].ToString();
        //                    string TotalDuration = row["TimeDifferenceInHHMM"].ToString();

        //                    // Calculate LateLogIn and TotalOvertime here as in your original code
        //                    string LateLogIn = CalculateLateLogIn(row["MinTimeOfTransaction"].ToString());
        //                    string TotalOvertime = CalculateTotalOvertime(row["MinTimeOfTransaction"].ToString(), row["MaxTimeOfTransaction"].ToString());

        //                    DataRow newRow = dt.NewRow();
        //                    newRow["RowNumber"] = rowNum;
        //                    newRow["Date"] = Date;
        //                    newRow["MinTimeOfTransaction"] = MinTimeOfTransaction;
        //                    newRow["MaxTimeOfTransaction"] = MaxTimeOfTransaction;
        //                    newRow["TotalDuration"] = TotalDuration;
        //                    newRow["LateLogIn"] = LateLogIn;
        //                    newRow["TotalOvertime"] = TotalOvertime;
        //                    dt.Rows.Add(newRow);

        //                    rowNum++;
        //                }
        //                else
        //                {
        //                    // Handle invalid date string (e.g., log error, skip row)
        //                    Console.WriteLine("Invalid date string: " + dateString);
        //                    // Alternatively, you can skip the row: continue;
        //                }
        //            }

        //            GridView1.DataSource = dt;
        //            GridView1.DataBind();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}




        protected void btnexcel_Click(object sender, EventArgs e)
        {
            if (ViewState["SearchAttendanceDataTable"] != null)
            {
                DataTable dt = ViewState["SearchAttendanceDataTable"] as DataTable;
                ExportToExcel(dt);
            }
        }
        protected void ExportToExcel(DataTable dt)
        {
            string attachment = "attachment; filename=AttendanceData.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/vnd.ms-excel";
            string tab = "";

            foreach (DataColumn dc in dt.Columns)
            {
                Response.Write(tab + dc.ColumnName);
                tab = "\t";
            }
            Response.Write("\n");

            foreach (DataRow dr in dt.Rows)
            {
                tab = "";
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    Response.Write(tab + dr[i].ToString());
                    tab = "\t";
                }
                Response.Write("\n");
            }
            Response.End();
        }


        public override void VerifyRenderingInServerForm(Control control)
        {

        }


        public DataSet GetAttendanceData()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    connection.Open();
                    string query = "SELECT CONVERT(date, DateOfTransaction) AS DateOfTransaction,CONVERT(time, MIN(DateOfTransaction)) AS MinTimeOfTransaction,CONVERT(time, MAX(DateOfTransaction)) AS MaxTimeOfTransaction,CONVERT(varchar(5), DATEADD(minute, DATEDIFF(minute, MIN(DateOfTransaction), MAX(DateOfTransaction)), 0), 108) AS TimeDifferenceInHHMM FROM Attendance WHERE  UserId = @UserId and DateOfTransaction BETWEEN @startdate AND DATEADD(day, 1, @enddate) GROUP BY CONVERT(date, DateOfTransaction) ORDER BY DateOfTransaction DESC";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@UserId", Session["EmpId"].ToString());
                    command.Parameters.AddWithValue("@StartDate", DateTime.Parse(txtstartdate.Text.Trim()));
                    command.Parameters.AddWithValue("@EndDate", DateTime.Parse(txtenddate.Text.Trim()));
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);
                    return ds;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Method to calculate late log-in time
        public string CalculateLateLogIn(string minTimeOfTransaction)
        {
            TimeSpan defaultStartTime = TimeSpan.Parse("09:45");
            TimeSpan actualInTime = TimeSpan.Parse(minTimeOfTransaction);
            TimeSpan lateDuration = actualInTime - defaultStartTime;
            string lateLogIn;

            if (defaultStartTime > actualInTime)
            {
                lateLogIn = "00:00";
            }
            else
            {
                lateLogIn = lateDuration.ToString(@"hh\:mm");
            }

            return lateLogIn;
        }

        // Method to calculate total overtime
        public string CalculateTotalOvertime(string minTimeOfTransaction, string maxTimeOfTransaction)
        {
            TimeSpan defaultTimeMorning = TimeSpan.Parse("09:30");
            TimeSpan defaultTimeEvening = TimeSpan.Parse("18:30");

            TimeSpan actualTimeMorning = TimeSpan.Parse(minTimeOfTransaction);
            TimeSpan actualTimeEvening = TimeSpan.Parse(maxTimeOfTransaction);

            TimeSpan morningOvertime = TimeSpan.Zero;
            TimeSpan eveningOvertime = TimeSpan.Zero;

            // Calculate Morning overtime if actualTimeMorning is greater than referenceTimeMorning
            if (actualTimeMorning < defaultTimeMorning)
            {
                morningOvertime = actualTimeMorning - defaultTimeMorning;
            }

            // Calculate Evening overtime if actualTimeEvening is less than referenceTimeEvening
            if (actualTimeEvening > defaultTimeEvening)
            {
                eveningOvertime = defaultTimeEvening - actualTimeEvening;
            }

            // Check if actualTimeMorning or actualTimeEvening is 0, then set respective overtime variable to zero
            if (actualTimeMorning == TimeSpan.Zero)
            {
                morningOvertime = TimeSpan.Zero;
            }

            if (actualTimeEvening == TimeSpan.Zero)
            {
                eveningOvertime = TimeSpan.Zero;
            }

            TimeSpan totalOvertime = morningOvertime + eveningOvertime;
            string totalOvertimeString = totalOvertime.ToString(@"hh\:mm");

            return totalOvertimeString;
        }
    }
}