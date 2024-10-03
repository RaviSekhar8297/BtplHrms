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
using OfficeOpenXml;
using System.Net.Mail;
using System.Net;

namespace Human_Resource_Management
{
    public partial class ManagerAttendance : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindPunch();              
                TotalInTimeOutTimeDisplay();
                tablePanel.Visible = false;
                btnexcel.Visible = false;
                BindEmployees();
                BindAttendanceData();
            }
        }

        private void BindAttendanceData()
        {
            string departmentName = Session["DepartmentName"].ToString();

            string query = @"
SELECT 
    Employees.FirstName,
    ISNULL(CONVERT(VARCHAR(5), MIN(Attendance.DateOfTransaction), 108), '00:00') AS Time,
ISNULL(CONVERT(VARCHAR(5), MAX(Attendance.DateOfTransaction), 108), '00:00') AS OutTime
FROM 
    Employees
LEFT JOIN 
    Attendance ON Employees.EmpId = Attendance.UserId 
    AND CAST(Attendance.DateOfTransaction AS DATE) = CAST(GETDATE() AS DATE)
WHERE 
    Employees.Department = @DepartmentName
GROUP BY 
    Employees.FirstName";

            string connstrg = ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString;

            using (SqlConnection sqlConn = new SqlConnection(connstrg))
            {
                using (SqlCommand sqlCmd = new SqlCommand(query, sqlConn))
                {
                    sqlCmd.Parameters.AddWithValue("@DepartmentName", departmentName);

                    sqlConn.Open();
                    SqlDataReader reader = sqlCmd.ExecuteReader();

                    PlaceHolder1.Controls.Clear();

                    StringBuilder htmlBuilder = new StringBuilder();

                    while (reader.Read())
                    {
                        string FirstName = reader["FirstName"].ToString().ToUpper();
                        string name = FirstName.Length > 6 ? FirstName.Substring(0, 6) : FirstName;
                        string time = reader["Time"].ToString();
                        string Outtime = reader["OutTime"].ToString();

                        htmlBuilder.Append($@"
                <div class='stats-row'>
                    <p class='employee-name' style='font-size: 16px;'>{name}</p>
                    <p class='employee-time' style='font-size: 16px;'>{time}</p>
                    <p class='employee-time' style='font-size: 16px;'>{Outtime}</p>
                </div>");
                    }

                    if (htmlBuilder.Length == 0)
                    {
                        htmlBuilder.Append($@"
                <div class='stats-row'>
                    <p class='employee-name'>Ramu</p>
                    <p class='employee-time'>00:00</p>
                </div>");
                    }

                    PlaceHolder1.Controls.Add(new Literal { Text = htmlBuilder.ToString() });
                }
            }
        }


        public void BindEmployees()
        {
            try
            {
                string connstrg = ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString;
                using (SqlConnection sqlConn = new SqlConnection(connstrg))
                {
                    using (SqlCommand sqlCmd = new SqlCommand())
                    {
                        sqlCmd.CommandText = "SELECT * FROM [Employees] WHERE status = 1 AND DepartmentId = @DepartmentId  ORDER BY FirstName ASC";
                        sqlCmd.Parameters.AddWithValue("@DepartmentId", Session["DepartmentId"].ToString());
                        sqlCmd.Connection = sqlConn;
                        sqlConn.Open();
                        SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        ddlname.DataSource = dt;
                        ddlname.DataValueField = "EmpId";
                        ddlname.DataTextField = "FirstName";
                        ddlname.DataBind();
                        ddlname.Items.Insert(0, new ListItem("-- Select Name --", "0"));
                        sqlConn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception or log the error
                Response.Write($"<script>alert('Error: {ex.Message}');</script>");
            }
        }

        public void BindPunch()
        {
            lblovertime.Text = "00:00";
            lbllatelogintime.Text = "00:00";
            TimeSpan minDate1 = TimeSpan.Parse("00:00");
            string minDatezero = minDate1.ToString(@"hh\:mm");
            lblpunchdatettime.Text = minDatezero;
          //  lbltodayprogress.Text = "Not Started";
          //  lblovertimeprogress.Text = "00:00";

            try
            {
                if (Session["EmpId"] != null && ConfigurationManager.ConnectionStrings["NewHRMSString"] != null)
                {
                    string userId = Session["EmpId"].ToString();
                    string connectionString = ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString;

                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        con.Open();

                        SqlCommand cmd = new SqlCommand("SELECT  DateOfTransaction,(SELECT MIN(DateOfTransaction) FROM Attendance WHERE UserId = @UserId AND CONVERT(date, DateOfTransaction) = CONVERT(date, GETDATE())) AS MinDateOfTransaction FROM Attendance WHERE UserId = @UserId AND CONVERT(date, DateOfTransaction) = CONVERT(date, GETDATE()) GROUP BY DateOfTransaction ORDER BY DateOfTransaction;", con);
                        cmd.Parameters.AddWithValue("@UserId", userId);

                        List<DateTime> punchTimesList = new List<DateTime>();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                DateTime punchTime = Convert.ToDateTime(reader["DateOfTransaction"]);
                                punchTimesList.Add(punchTime);
                                lblpunchdatettime.Text = reader["MinDateOfTransaction"].ToString();
                            }
                        }

                        DateTime[] punchTimes = punchTimesList.ToArray();

                        TimeSpan totalDifference = TimeSpan.Zero;
                        TimeSpan overtime = TimeSpan.Zero;
                        TimeSpan overtime2 = TimeSpan.Zero;
                        for (int i = 0; i < punchTimes.Length - 1; i += 2)
                        {
                            if (i == 0)
                            {
                                // lblpunchdatettime.Text = punchTimes[i].ToString();
                                DateTime punchDateTime = punchTimes[i];
                                string timeOnly = punchDateTime.ToString("hh:mm:ss tt"); // Convert to 12-hour format with AM/PM

                                string startTimeString1 = "09:30:00 AM";

                                DateTime startTime1;
                                if (DateTime.TryParseExact(startTimeString1.Trim(), "hh:mm:ss tt", CultureInfo.InvariantCulture, DateTimeStyles.None, out startTime1))
                                {
                                    DateTime endintime = DateTime.ParseExact(timeOnly, "hh:mm:ss tt", CultureInfo.InvariantCulture);
                                    if (startTime1 < endintime)
                                    {
                                        TimeSpan diff = endintime - startTime1;
                                        lbllatelogintime.Text = diff.ToString();
                                    }
                                    else
                                    {
                                        TimeSpan diff = startTime1 - endintime;
                                        lblovertime.Text = diff.ToString();
                                        //  lblovertimeprogress.Text = diff.ToString();
                                        overtime = diff;
                                    }
                                }
                                else
                                {

                                }
                            }

                            TimeSpan difference = punchTimes[i + 1] - punchTimes[i];
                            DateTime punchDateTime12 = punchTimes[i + 1];
                            string timeOnly1 = punchDateTime12.ToString("HH:mm:ss tt");

                            totalDifference += difference;
                            string startovertime = "06:30:00 PM";
                            DateTime overtimestore;
                            if (DateTime.TryParseExact(startovertime.Trim(), "hh:mm:ss tt", CultureInfo.InvariantCulture, DateTimeStyles.None, out overtimestore))
                            {
                                DateTime endovertime;
                                if (DateTime.TryParseExact(timeOnly1, "hh:mm:ss tt", CultureInfo.InvariantCulture, DateTimeStyles.None, out endovertime))
                                {
                                    if (overtimestore < endovertime)
                                    {
                                        TimeSpan diff = endovertime - overtimestore;
                                        overtime2 = diff;
                                        lblovertime.Text = (overtime2 + overtime).ToString();
                                      //  lblovertimeprogress.Text = (overtime2 + overtime).ToString();
                                    }
                                }
                                else
                                {
                                    // Handle parsing failure
                                }
                            }
                            else
                            {

                            }

                        }
                        Label1.Text = totalDifference.ToString(@"hh\:mm");
                        // lbltodayprogress.Text = totalDifference.ToString(@"hh\:mm");
                        
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
      
        protected void btnpunchout_Click(object sender, EventArgs e)
        {

            string UserId = Session["EmpId"].ToString();
            string Email = Session["Email"].ToString();
            string Name = Session["Name"].ToString();
            int flag = 1;
            string getdate = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss tt");
            string currentTimeString = DateTime.Now.ToString("HH:mm");
            string specificTimeString = "09:41";
            TimeSpan currentTime = TimeSpan.Parse(currentTimeString);
            TimeSpan specificTime = TimeSpan.Parse(specificTimeString);

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
                        sqlcmd.Parameters.AddWithValue("@EmpName", Name);
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
                            BindAttendanceData();
                            if (currentTime > specificTime)
                            {
                               // SendEmail(Name, Email, currentTimeString);
                            }
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
        private void SendEmail(string empName, string toEmail, string currentTime)
        {
            try
            {
                string fromEmail = "software.trainee1@brihaspathi.com"; // Replace with your email address
                string fromPassword = "RAVI8297"; // Replace with your email password
                string subject = "BT - Late Log In Details";
                string body = $@"
    <p>Dear {empName},</p>
    <p>The current time is {currentTime}, which is after the specified time of 09:41.</p>
    <p>Please note that you have exceeded the expected arrival time of 09:30.</p>
    <p>Your late time today is {currentTime}. To maintain punctuality and ensure the smooth operation of our daily schedule, it is crucial that you adhere to the designated time of arrival before 09:30.</p>
    <p>Regular punctuality is not only a professional expectation but also a key factor in maintaining our team's productivity and workflow. We kindly ask you to make every effort to arrive on time going forward.</p>
    <p>If you are experiencing any difficulties that affect your ability to arrive on time, please communicate with your supervisor so that we can provide the necessary support.</p>
    <p>Thank you for your attention to this matter.</p>
    <p>Best regards,<br />Brihaspathi technologies Private Limited</p>";


                MailMessage message = new MailMessage();
                message.From = new MailAddress(fromEmail);
                message.To.Add(new MailAddress(toEmail)); // Use the correct parameter name
                message.Subject = subject;
                message.Body = body;
                message.IsBodyHtml = true;

                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587); // Replace with your SMTP server and port
                smtp.Credentials = new NetworkCredential(fromEmail, fromPassword);
                smtp.EnableSsl = true;
                smtp.Send(message);
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error sending email: " + ex.Message + "');</script>");
            }
        }
        protected void btnattendancesearch_Click(object sender, EventArgs e)
        {
           
            if (ddlname.SelectedItem != null && !string.IsNullOrEmpty(ddlname.SelectedItem.Text) && ddlname.SelectedIndex != 0)
            {
                TotalInTimeOutTimeDisplay();
                BindAttendanceData();
                string Name = ddlname.SelectedItem.Text;
                int Userid = Convert.ToInt32(ddlname.SelectedValue);
                tablePanel.Visible = true;
                try
                {
                    if (Name != null && !string.IsNullOrEmpty(Name))
                    {
                        using (var connection1 = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                        {
                            connection1.Open();
                            DataSet ds1 = new DataSet();

                            using (SqlCommand sqlcmd = new SqlCommand("SELECT CONVERT(date, DateOfTransaction) AS DateOfTransaction, CONVERT(time, MIN(DateOfTransaction)) AS MinTimeOfTransaction, CONVERT(time, MAX(DateOfTransaction)) AS MaxTimeOfTransaction, CONVERT(varchar(5), DATEADD(minute, DATEDIFF(minute, MIN(DateOfTransaction), MAX(DateOfTransaction)), 0), 108) AS TimeDifferenceInHHMM FROM Attendance WHERE UserId = @UserId AND DateOfTransaction BETWEEN @startdate AND DATEADD(day, 1, @enddate) GROUP BY CONVERT(date, DateOfTransaction) ORDER BY DateOfTransaction DESC", connection1))
                            {
                                sqlcmd.Parameters.AddWithValue("@UserId", Userid);
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
                                            projectHtml.Append("<td>" + Name + "</td>");
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
            else
            {
                Response.Write("<script>alert('Select Name..')</script>");
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
                        using (SqlCommand sqlcmd = new SqlCommand("SELECT FORMAT(DateOfTransaction, 'HH:mm:ss') AS TimeOfTransaction FROM Attendance WHERE UserId = @UserId AND CONVERT(date, DateOfTransaction) = CAST(GETDATE() AS DATE)", connection1))
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
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnexcel_Click(object sender, EventArgs e)
        {
            if (ViewState["SearchAttendanceDataTable"] != null)
            {
                DataTable dt = ViewState["SearchAttendanceDataTable"] as DataTable;

                if (dt != null && dt.Rows.Count > 0)
                {
                    // Create a new DataTable to hold the export data
                    DataTable exportTable = new DataTable();

                    // Add columns to the export table
                    exportTable.Columns.Add("S.No");
                    exportTable.Columns.Add("Name");
                    exportTable.Columns.Add("Date");
                    exportTable.Columns.Add("Punch In");
                    exportTable.Columns.Add("Punch Out");
                    exportTable.Columns.Add("Duration");
                    exportTable.Columns.Add("LateLogIn");
                    exportTable.Columns.Add("Overtime");

                    // Add rows to the export table
                    int rowNum = 1;
                    foreach (DataRow row in dt.Rows)
                    {
                        DataRow newRow = exportTable.NewRow();
                        newRow["S.No"] = rowNum++;
                        newRow["Name"] = ddlname.SelectedItem.Text; // Assuming the name is the same for all rows
                        newRow["Date"] = Convert.ToDateTime(row["DateOfTransaction"]).ToString("yyyy-MM-dd");
                        newRow["Punch In"] = row["MinTimeOfTransaction"].ToString();
                        newRow["Punch Out"] = row["MaxTimeOfTransaction"].ToString();
                        newRow["Duration"] = row["TimeDifferenceInHHMM"].ToString();

                        // Calculate LateLogIn and Overtime
                        TimeSpan defaultStartTime = TimeSpan.Parse("09:30");
                        TimeSpan actualInTime = TimeSpan.Parse(row["MinTimeOfTransaction"].ToString());
                        TimeSpan lateDuration = actualInTime - defaultStartTime;
                        string LateLogIn = lateDuration > TimeSpan.Zero ? lateDuration.ToString(@"hh\:mm") : "00:00";

                        TimeSpan DefaultTimeMorning = TimeSpan.Parse("09:30");
                        TimeSpan DefaultTimeEvening = TimeSpan.Parse("18:30");
                        TimeSpan actualTimeMorning = TimeSpan.Parse(row["MinTimeOfTransaction"].ToString());
                        TimeSpan actualTimeEvening = TimeSpan.Parse(row["MaxTimeOfTransaction"].ToString());
                        TimeSpan morningOvertime = actualTimeMorning < DefaultTimeMorning ? DefaultTimeMorning - actualTimeMorning : TimeSpan.Zero;
                        TimeSpan eveningOvertime = actualTimeEvening > DefaultTimeEvening ? actualTimeEvening - DefaultTimeEvening : TimeSpan.Zero;
                        TimeSpan totalOvertime = morningOvertime + eveningOvertime;
                        string totalOvertimeString = totalOvertime.ToString(@"hh\:mm");

                        newRow["LateLogIn"] = LateLogIn;
                        newRow["Overtime"] = totalOvertimeString;

                        exportTable.Rows.Add(newRow);
                    }

                    // Set the LicenseContext property
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                    using (var memoryStream = new System.IO.MemoryStream())
                    {
                        using (var excelPackage = new ExcelPackage(memoryStream))
                        {
                            var workSheet = excelPackage.Workbook.Worksheets.Add("Sheet1");
                            workSheet.Cells["A1"].LoadFromDataTable(exportTable, true);
                            excelPackage.Save();
                        }

                        // Set up the HTTP response for downloading the file
                        Response.Clear();
                        Response.Buffer = true;
                        Response.AddHeader("content-disposition", "attachment;filename=AttendanceReport.xlsx");
                        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        Response.BinaryWrite(memoryStream.ToArray());
                        Response.End();
                    }
                }
                else
                {
                    Response.Write("<script>alert('No data available for export.')</script>");
                }
            }
            else
            {
                Response.Write("<script>alert('No data found in ViewState.')</script>");
            }
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
            TimeSpan defaultStartTime = TimeSpan.Parse("09:30");
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