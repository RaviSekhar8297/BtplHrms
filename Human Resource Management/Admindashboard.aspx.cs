using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.ComponentModel;
using System.Text;
using System.Data.SqlTypes;

namespace Human_Resource_Management
{
    public partial class Admindashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string name = Session["Name"].ToString();
            lblwelcomename.Text = name;
            if (!IsPostBack)
            {
                DayBindData();
                WeeklyDataBind();
                MonthlyDataBind();
                AbsentList();
                BindBirthdaysList();
                AnniversaryList();
                NewJoinList();
                LateLogList();
                EarlyGoes();
                EarlyComesList();
                RelievingList();
            }
        }
        protected void DayBindData()
        {
            try
            {

                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SELECT COUNT(DISTINCT UserID) AS TotalUsers FROM Attendance WHERE CONVERT(DATE, DateOfTransaction) = CONVERT(DATE, GETDATE()); ", con);
                    int countPresent = (int)cmd.ExecuteScalar();

                    cmd.CommandText = "SELECT (SELECT COUNT(EmpId) FROM Employees WHERE status = '1') -(SELECT COUNT(DISTINCT UserId) FROM Attendance WHERE CONVERT(DATE, DateOfTransaction) = CONVERT(DATE, GETDATE())) AS RemainingEmployees; ";
                    int countAbsent = (int)cmd.ExecuteScalar();

                    cmd.CommandText = "SELECT COUNT(DISTINCT UserID) AS TotalUsers FROM(SELECT UserID, MIN(DateOfTransaction) AS EarliestTransaction FROM Attendance WHERE CONVERT(DATE, DateOfTransaction) = CONVERT(DATE, GETDATE()) GROUP BY UserID) AS SubQuery WHERE CAST(EarliestTransaction AS TIME) > '09:45:00'; ";
                    int countLate = (int)cmd.ExecuteScalar();

                    cmd.CommandText = "SELECT COUNT(DISTINCT UserID) AS TotalUsers FROM(SELECT UserID, MIN(DateOfTransaction) AS EarliestTransaction FROM Attendance WHERE CONVERT(DATE, DateOfTransaction) = CONVERT(DATE, GETDATE()) GROUP BY UserID) AS SubQuery WHERE CAST(EarliestTransaction AS TIME) < '09:45:00'; ";
                    int countEarlyComes = (int)cmd.ExecuteScalar();

                    cmd.CommandText = "SELECT COUNT(*) FROM Employees WHERE status='1'";
                    int Total = (int)cmd.ExecuteScalar();
                    cmd.CommandTimeout = 9000;

                    //SELECT COUNT(LateComes) AS countLate FROM AttendanceLogsForRBSDashBoard WHERE LateComes IS NOT NULL AND LateComes != '' and CAST(AttendanceDate AS DATE) = CAST(GETDATE() AS DATE)   this Query get now date 

                    // Now you have counts for present, absent, and late
                    Label1.Text = countPresent.ToString();
                    Label2.Text = countAbsent.ToString();
                    Label3.Text = countLate.ToString();
                    Label4.Text = countEarlyComes.ToString();
                    Label5.Text = Total.ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void WeeklyDataBind()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("WITH LastSevenDays AS (SELECT CAST(GETDATE() AS DATE) AS TransactionDate UNION ALL SELECT CAST(DATEADD(DAY, -1, TransactionDate) AS DATE) FROM LastSevenDays WHERE TransactionDate > DATEADD(DAY, -6, GETDATE())),TotalEmployees AS (SELECT COUNT(*) AS TotalUsers FROM employees WHERE status = '1'),DailyAttendance AS (SELECT CAST(DateOfTransaction AS DATE) AS TransactionDate,COUNT(DISTINCT UserID) AS PresentCount FROM Attendance WHERE DateOfTransaction BETWEEN DATEADD(DAY, -7, GETDATE()) AND GETDATE() GROUP BY CAST(DateOfTransaction AS DATE))SELECT COALESCE(SUM(PresentCount), 0) AS OverallPresentCount FROM DailyAttendance;", con);
                    int countPresent = (int)cmd.ExecuteScalar();

                    cmd.CommandText = "WITH LastSevenDays AS (SELECT CAST(GETDATE() AS DATE) AS TransactionDate UNION ALL SELECT CAST(DATEADD(DAY, -1, TransactionDate) AS DATE)FROM LastSevenDays WHERE TransactionDate > DATEADD(DAY, -6, GETDATE())), TotalEmployees AS (SELECT COUNT(*) AS TotalUsers FROM employees WHERE status = '1'),DailyAttendance AS (SELECT CAST(DateOfTransaction AS DATE) AS TransactionDate,COUNT(DISTINCT UserID) AS PresentCount FROM Attendance WHERE DateOfTransaction BETWEEN DATEADD(DAY, -7, GETDATE()) AND GETDATE() GROUP BY CAST(DateOfTransaction AS DATE)),DailyAbsentCount AS (SELECT lsd.TransactionDate,te.TotalUsers - COALESCE(da.PresentCount, 0) AS AbsentCount FROM LastSevenDays lsd CROSS JOIN TotalEmployees te LEFT JOIN DailyAttendance da ON lsd.TransactionDate = da.TransactionDate) SELECT COALESCE(SUM(AbsentCount), 0) AS TotalAbsents FROM DailyAbsentCount;";
                    int countAbsent = (int)cmd.ExecuteScalar();

                    cmd.CommandText = "WITH LastSevenDays AS (SELECT CAST(GETDATE() AS DATE) AS TransactionDate UNION ALL SELECT CAST(DATEADD(DAY, -1, TransactionDate) AS DATE) FROM LastSevenDays WHERE TransactionDate > DATEADD(DAY, -6, GETDATE())),TotalEmployees AS (SELECT COUNT(*) AS TotalUsers FROM employees WHERE status = '1'),DailyAttendance AS ( SELECT CAST(DateOfTransaction AS DATE) AS TransactionDate,COUNT(DISTINCT UserID) AS PresentCount FROM Attendance  WHERE DateOfTransaction BETWEEN DATEADD(DAY, -7, GETDATE()) AND GETDATE() AND CAST(DateOfTransaction AS TIME) >= '09:45' GROUP BY CAST(DateOfTransaction AS DATE))SELECT COALESCE(SUM(PresentCount), 0) AS OverallLateCount FROM DailyAttendance;";
                    int countLate = (int)cmd.ExecuteScalar();


                    cmd.CommandText = "WITH LastSevenDays AS (SELECT DATEADD(DAY, -6, CAST(GETDATE() AS DATE)) AS TransactionDate UNION ALL SELECT DATEADD(DAY, 1, TransactionDate) FROM LastSevenDays WHERE TransactionDate < CAST(GETDATE() AS DATE)),TotalEmployees AS ( SELECT COUNT(*) AS TotalUsers FROM employees WHERE status = '1'), DailyAttendance AS (SELECT ld.TransactionDate AS TransactionDate,  COUNT(DISTINCT a.UserID) AS PresentCount FROM LastSevenDays ld LEFT JOIN Attendance a ON CAST(a.DateOfTransaction AS DATE) = ld.TransactionDate AND CAST(a.DateOfTransaction AS TIME) <= '09:45'GROUP BY ld.TransactionDate) SELECT COALESCE(SUM(PresentCount), 0) AS OverallLateCount FROM DailyAttendance;";
                    int countEarlyComes = (int)cmd.ExecuteScalar();

                    cmd.CommandText = "	SELECT COUNT(*) * 7 AS LastSevenDaysTotalCount FROM Employees WHERE status = '1'; ";
                    int Total = (int)cmd.ExecuteScalar();


                    Label6.Text = countPresent.ToString();
                    Label8.Text = countAbsent.ToString();
                    Label9.Text = countLate.ToString();
                    Label10.Text = countEarlyComes.ToString();
                    Label7.Text = Total.ToString();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void MonthlyDataBind()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    con.Open();

                    // Query for Overall Present Count
                    SqlCommand cmd = new SqlCommand(@"
                WITH LastThirtyDays AS (
                    SELECT CAST(GETDATE() AS DATE) AS TransactionDate
                    UNION ALL
                    SELECT CAST(DATEADD(DAY, -1, TransactionDate) AS DATE)
                    FROM LastThirtyDays
                    WHERE TransactionDate > DATEADD(DAY, -29, GETDATE())
                ),
                TotalEmployees AS (
                    SELECT COUNT(*) AS TotalUsers FROM employees WHERE status = '1'
                ),
                DailyAttendance AS (
                    SELECT CAST(DateOfTransaction AS DATE) AS TransactionDate,
                           COUNT(DISTINCT UserID) AS PresentCount
                    FROM Attendance
                    WHERE DateOfTransaction BETWEEN DATEADD(DAY, -29, GETDATE()) AND GETDATE()
                    GROUP BY CAST(DateOfTransaction AS DATE)
                )
                SELECT COALESCE(SUM(PresentCount), 0) AS OverallPresentCount
                FROM DailyAttendance;", con);
                    int countPresent = (int)cmd.ExecuteScalar();

                    // Query for Overall Absent Count
                    cmd.CommandText = @"
                WITH LastThirtyDays AS (
                    SELECT CAST(GETDATE() AS DATE) AS TransactionDate
                    UNION ALL
                    SELECT CAST(DATEADD(DAY, -1, TransactionDate) AS DATE)
                    FROM LastThirtyDays
                    WHERE TransactionDate > DATEADD(DAY, -29, GETDATE())
                ),
                TotalEmployees AS (
                    SELECT COUNT(*) AS TotalUsers FROM employees WHERE status = '1'
                ),
                DailyAttendance AS (
                    SELECT CAST(DateOfTransaction AS DATE) AS TransactionDate,
                           COUNT(DISTINCT UserID) AS PresentCount
                    FROM Attendance
                    WHERE DateOfTransaction BETWEEN DATEADD(DAY, -29, GETDATE()) AND GETDATE()
                    GROUP BY CAST(DateOfTransaction AS DATE)
                ),
                DailyAbsentCount AS (
                    SELECT lsd.TransactionDate,
                           te.TotalUsers - COALESCE(da.PresentCount, 0) AS AbsentCount
                    FROM LastThirtyDays lsd
                    CROSS JOIN TotalEmployees te
                    LEFT JOIN DailyAttendance da ON lsd.TransactionDate = da.TransactionDate
                )
                SELECT COALESCE(SUM(AbsentCount), 0) AS TotalAbsents
                FROM DailyAbsentCount;";
                    int countAbsent = (int)cmd.ExecuteScalar();

                    // Query for Overall Late Count
                    cmd.CommandText = @"
                WITH LastThirtyDays AS (
                    SELECT CAST(GETDATE() AS DATE) AS TransactionDate
                    UNION ALL
                    SELECT CAST(DATEADD(DAY, -1, TransactionDate) AS DATE)
                    FROM LastThirtyDays
                    WHERE TransactionDate > DATEADD(DAY, -29, GETDATE())
                ),
                TotalEmployees AS (
                    SELECT COUNT(*) AS TotalUsers FROM employees WHERE status = '1'
                ),
                DailyAttendance AS (
                    SELECT CAST(DateOfTransaction AS DATE) AS TransactionDate,
                           COUNT(DISTINCT UserID) AS PresentCount
                    FROM Attendance
                    WHERE DateOfTransaction BETWEEN DATEADD(DAY, -29, GETDATE()) AND GETDATE()
                      AND CAST(DateOfTransaction AS TIME) >= '09:45'
                    GROUP BY CAST(DateOfTransaction AS DATE)
                )
                SELECT COALESCE(SUM(PresentCount), 0) AS OverallLateCount
                FROM DailyAttendance;";
                    int countLate = (int)cmd.ExecuteScalar();

                    // Query for Overall Early Comes Count
                    cmd.CommandText = @"
                WITH LastThirtyDays AS (
                    SELECT CAST(GETDATE() AS DATE) AS TransactionDate
                    UNION ALL
                    SELECT CAST(DATEADD(DAY, -1, TransactionDate) AS DATE)
                    FROM LastThirtyDays
                    WHERE TransactionDate > DATEADD(DAY, -29, GETDATE())
                ),
                TotalEmployees AS (
                    SELECT COUNT(*) AS TotalUsers FROM employees WHERE status = '1'
                ),
                DailyAttendance AS (
                    SELECT CAST(DateOfTransaction AS DATE) AS TransactionDate,
                           COUNT(DISTINCT UserID) AS PresentCount
                    FROM Attendance
                    WHERE DateOfTransaction BETWEEN DATEADD(DAY, -29, GETDATE()) AND GETDATE()
                      AND CAST(DateOfTransaction AS TIME) <= '09:45'
                    GROUP BY CAST(DateOfTransaction AS DATE)
                )
                SELECT COALESCE(SUM(PresentCount), 0) AS OverallEarlyCount
                FROM DailyAttendance;";
                    int countEarlyComes = (int)cmd.ExecuteScalar();

                    // Query for Total Employees Count
                    cmd.CommandText = "SELECT COALESCE(COUNT(*), 0) * 30 AS TotalCount FROM Employees WHERE status = '1';";
                    int Total = (int)cmd.ExecuteScalar();

                    // Display results in labels
                    Label11.Text = countPresent.ToString();
                    Label12.Text = countAbsent.ToString();
                    Label13.Text = countLate.ToString();
                    Label14.Text = countEarlyComes.ToString();
                    Label15.Text = Total.ToString();
                }
            }
            catch (Exception ex)
            {
                // Handle the exception appropriately
                throw ex;
            }
        }
        public void EarlyComesList()
        {
            try
            {
                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    using (SqlCommand sqlcmd = new SqlCommand(
                        @"SELECT t.*, e.*,  (SELECT MIN(t2.DateOfTransaction)  FROM Attendance AS t2  WHERE t2.UserId = t.UserId  AND t2.DateOfTransaction >= CONVERT(DATE, GETDATE())  AND CAST(t2.DateOfTransaction AS TIME) < '09:45:00') AS MinTime FROM Attendance AS t  INNER JOIN Employees AS e ON t.UserId = e.EmpId  WHERE t.DateOfTransaction >= CONVERT(DATE, GETDATE())  AND CAST(t.DateOfTransaction AS TIME) < '09:45:00'  AND t.DateOfTransaction = (SELECT MIN(t2.DateOfTransaction)  FROM Attendance AS t2  WHERE t2.UserId = t.UserId AND t2.DateOfTransaction >= CONVERT(DATE, GETDATE())  AND CAST(t2.DateOfTransaction AS TIME) <'09:45:00')",
                        connection))
                    {
                        connection.Open();
                        SqlDataAdapter da1 = new SqlDataAdapter(sqlcmd);
                        DataSet ds1 = new DataSet();
                        da1.Fill(ds1);
                        if (ds1.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow row in ds1.Tables[0].Rows)
                            {
                                string HyName = row["FirstName"].ToString();

                                object imageDataObj = row["Image"];
                                byte[] imageData = null;

                                if (imageDataObj != DBNull.Value)
                                {
                                    imageData = (byte[])imageDataObj;
                                }

                                else
                                {
                                    imageData = new byte[0];
                                }

                                string base64String = Convert.ToBase64String(imageData);
                                string imageUrl = "data:image/jpeg;base64," + base64String;
                                string Department = row["Department"].ToString();
                                string Designation = row["Designation"].ToString();

                                // Retrieve MinTime and format it
                                DateTime minTime = Convert.ToDateTime(row["MinTime"]);
                                string onlyTime = minTime.ToString("HH:mm"); // Format to show hours and minutes

                                StringBuilder projectHtml = new StringBuilder();

                                projectHtml.Append("<tr>");
                                projectHtml.Append("<td><a href='profile.html' class='avatar'><img src=" + imageUrl + " alt='User Image'></a>");
                                projectHtml.Append("" + HyName + "</td>");
                                projectHtml.Append("<td>" + onlyTime + "</td>"); // Add formatted MinTime to the table
                                projectHtml.Append("<td>" + Department + "</td>");
                                // projectHtml.Append("<td>" + Designation + "</td>");
                                projectHtml.Append("</tr>");

                                EarlyComesData.Controls.Add(new LiteralControl(projectHtml.ToString()));
                            }
                        }

                        else
                        {
                            Literal1.Text = "<div class='no-records-message'>No Records Found...</div>";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void LateLogList()
        {
            try
            {
                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    using (SqlCommand sqlcmd = new SqlCommand())
                    {
                        sqlcmd.Connection = connection;
                        sqlcmd.CommandText = @"
                    SELECT 
                        E.Image,
                        E.FirstName,
                        FORMAT(CONVERT(TIME, A.DateOfTransaction), 'hh\:mm') AS TimeOfTransaction,
                        E.Designation
                    FROM 
                        Employees E
                    INNER JOIN 
                        (SELECT 
                             UserId, 
                             MIN(DateOfTransaction) AS DateOfTransaction
                         FROM 
                             Attendance
                         WHERE 
                             DateOfTransaction >= CONVERT(DATE, GETDATE()) 
                         GROUP BY 
                             UserId) A ON E.EmpId = A.UserId
                    WHERE 
                        CONVERT(TIME, A.DateOfTransaction) > '09:45'
                    ORDER BY 
                        A.DateOfTransaction ASC;
                ";

                        connection.Open();
                        sqlcmd.CommandTimeout = 120;
                        SqlDataAdapter da1 = new SqlDataAdapter(sqlcmd);
                        DataSet ds1 = new DataSet();
                        da1.Fill(ds1);

                        if (ds1.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow row in ds1.Tables[0].Rows)
                            {
                                string HyName = row["FirstName"].ToString();

                                object imageDataObj = row["Image"];
                                byte[] imageData = null;

                                if (imageDataObj != DBNull.Value)
                                {
                                    imageData = (byte[])imageDataObj;
                                }
                                else
                                {
                                    imageData = new byte[0];
                                }

                                string base64String = Convert.ToBase64String(imageData);
                                string imageUrl = "data:image/jpeg;base64," + base64String;
                                string Designation = row["Designation"].ToString();
                                string TimeOfTransaction = row["TimeOfTransaction"].ToString();

                                StringBuilder projectHtml = new StringBuilder();

                                projectHtml.Append("<tr>");
                                projectHtml.Append("<td><a href='profile.html' class='avatar'><img src=" + imageUrl + " alt='User Image'></a>");
                                projectHtml.Append("" + HyName + "</td>");
                                projectHtml.Append("<td>" + TimeOfTransaction + "</td>"); // Display formatted time
                                projectHtml.Append("<td>" + Designation + "</td>");

                                projectHtml.Append("</tr>");

                                LateLogInsData.Controls.Add(new LiteralControl(projectHtml.ToString()));
                            }
                        }
                        else
                        {
                            Literal2.Text = "<div class='no-records-message'>No Records Found...</div>";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the error or display a user-friendly error message
                Console.WriteLine("Error: " + ex.Message);
            }
        }


        public void AbsentList()
        {
            try
            {
                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    using (SqlCommand sqlcmd = new SqlCommand("WITH PunchedInToday AS (  SELECT DISTINCT UserId  FROM Attendance    WHERE CONVERT(DATE, DateOfTransaction) = CONVERT(DATE, GETDATE())) SELECT   e.FirstName, e.Department,    e.Designation,   e.EmpId,   e.Image FROM   Employees e LEFT JOIN     PunchedInToday p ON e.EmpId = p.UserId WHERE  e.status = '1'   AND p.UserId IS NULL;", connection))
                    {
                        connection.Open();
                        SqlDataAdapter da1 = new SqlDataAdapter(sqlcmd);
                        DataSet ds1 = new DataSet();
                        da1.Fill(ds1);
                        if (ds1.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow row in ds1.Tables[0].Rows)
                            {
                                string HyName = row["FirstName"].ToString();

                                object imageDataObj = row["Image"];
                                byte[] imageData = null;

                                if (imageDataObj != DBNull.Value)
                                {
                                    imageData = (byte[])imageDataObj;
                                }
                                else
                                {
                                    imageData = new byte[0];
                                }

                                string base64String = Convert.ToBase64String(imageData);
                                string imageUrl = "data:image/jpeg;base64," + base64String;

                                string Department = row["Department"].ToString();
                                string Designation = row["Designation"].ToString();

                                // Build HTML for displaying each row
                                StringBuilder projectHtml = new StringBuilder();
                                projectHtml.Append("<tr>");
                                projectHtml.Append("<td><a href='profile.html' class='avatar'><img src='" + imageUrl + "' alt='User Image'></a>");
                                projectHtml.Append("" + HyName + "</td>");
                                projectHtml.Append("<td>" + Department + "</td>");
                                projectHtml.Append("<td>" + Designation + "</td>");
                                projectHtml.Append("</tr>");

                                // Add HTML to your control (e.g., a table)
                                AbsentData.Controls.Add(new LiteralControl(projectHtml.ToString()));

                            }
                        }
                        else
                        {
                            Literal3.Text = "<div class='no-records-message'>No Records Found...</div>";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions appropriately
                throw ex;
            }

        }
        public void EarlyGoes()
        {
            try
            {
                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    using (SqlCommand sqlcmd = new SqlCommand(@"
            WITH LatestTransaction AS 
            (SELECT t.UserId,  
                    MAX(t.DateOfTransaction) AS MaxTransaction  FROM Attendance t   WHERE  t.DateOfTransaction >= CONVERT(DATE, GETDATE())  
                    AND CAST(t.DateOfTransaction AS TIME) < '18:30:00'  GROUP BY t.UserId) SELECT  t.*,  e.*,  CAST(lt.MaxTransaction AS TIME) AS LastTime 
            FROM  Attendance t INNER JOIN   
                Employees e  ON t.UserId = e.EmpId INNER JOIN  
                LatestTransaction lt  ON t.UserId = lt.UserId AND t.DateOfTransaction = lt.MaxTransaction WHERE  CAST(t.DateOfTransaction AS TIME) < '18:30:00';", connection))
                    {
                        connection.Open();
                        SqlDataAdapter da1 = new SqlDataAdapter(sqlcmd);
                        DataSet ds1 = new DataSet();
                        da1.Fill(ds1);
                        if (ds1.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow row in ds1.Tables[0].Rows)
                            {
                                string HyName = row["FirstName"].ToString();

                                object imageDataObj = row["Image"];
                                byte[] imageData = null;

                                if (imageDataObj != DBNull.Value)
                                {
                                    imageData = (byte[])imageDataObj;
                                }
                                else
                                {
                                    imageData = new byte[0];
                                }

                                string base64String = Convert.ToBase64String(imageData);
                                string imageUrl = "data:image/jpeg;base64," + base64String;
                                string Department = row["Department"].ToString();

                                // Convert LastTime to string with "HH:mm" format using TimeSpan
                                TimeSpan lastTime = (TimeSpan)row["LastTime"];
                                string formattedLastTime = lastTime.ToString(@"hh\:mm");

                                StringBuilder projectHtml = new StringBuilder();

                                projectHtml.Append("<tr>");
                                projectHtml.Append("<td><a href='#' class='avatar'><img src=" + imageUrl + " alt='User Image'></a>");
                                projectHtml.Append("" + HyName + "</td>");
                                projectHtml.Append("<td>" + formattedLastTime + "</td>");  // Display formatted LastTime
                                projectHtml.Append("<td>" + Department + "</td>");
                                projectHtml.Append("</tr>");

                                EarlyGoesData.Controls.Add(new LiteralControl(projectHtml.ToString()));
                            }
                        }
                        else
                        {
                            Literal4.Text = "<div class='no-records-message'>No Records Found...</div>";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void BindBirthdaysList()
        {
            try
            {
                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    using (SqlCommand sqlcmd = new SqlCommand("SELECT * FROM Employees WHERE DATEPART(month, DOB) = DATEPART(month, GETDATE()) AND DATEPART(day, DOB) = DATEPART(day, GETDATE()) ", connection))
                    {
                        connection.Open();
                        SqlDataAdapter da1 = new SqlDataAdapter(sqlcmd);
                        DataSet ds1 = new DataSet();
                        da1.Fill(ds1);
                        if (ds1.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow row in ds1.Tables[0].Rows)
                            {
                                string HyName = row["FirstName"].ToString();

                                object imageDataObj = row["Image"];
                                byte[] imageData = null;

                                if (imageDataObj != DBNull.Value)
                                {
                                    imageData = (byte[])imageDataObj;
                                }
                                else
                                {
                                    imageData = new byte[0];
                                }

                                string base64String = Convert.ToBase64String(imageData);
                                string imageUrl = "data:image/jpeg;base64," + base64String;
                                string Department = row["Department"].ToString();
                                string Designation = row["Designation"].ToString();

                                StringBuilder projectHtml = new StringBuilder();

                                projectHtml.Append("<tr>");
                                projectHtml.Append("<td><a href='profile.html' class='avatar'><img src=" + imageUrl + " alt='User Image'></a>");
                                projectHtml.Append("" + HyName + "</td>");
                                projectHtml.Append("<td>" + Department + "</td>");
                                projectHtml.Append("<td>" + Designation + "</td>");

                                projectHtml.Append("</tr>");
                                AdminBirthdaysData.Controls.Add(new LiteralControl(projectHtml.ToString()));
                            }
                        }
                        else
                        {
                            Literal5.Text = "<div class='no-records-message'>No Records Found...</div>";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void AnniversaryList()
        {
            try
            {
                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    using (SqlCommand sqlcmd = new SqlCommand("SELECT * FROM Employees WHERE DATEPART(month, DOJ) = DATEPART(month, GETDATE()) AND DATEPART(day, DOJ) = DATEPART(day, GETDATE())", connection))
                    {
                        connection.Open();
                        SqlDataAdapter da1 = new SqlDataAdapter(sqlcmd);
                        DataSet ds1 = new DataSet();
                        da1.Fill(ds1);
                        if (ds1.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow row in ds1.Tables[0].Rows)
                            {
                                string HyName = row["FirstName"].ToString();

                                object imageDataObj = row["Image"];
                                byte[] imageData = null;

                                if (imageDataObj != DBNull.Value)
                                {
                                    imageData = (byte[])imageDataObj;
                                }
                                else
                                {
                                    imageData = new byte[0];
                                }

                                string base64String = Convert.ToBase64String(imageData);
                                string imageUrl = "data:image/jpeg;base64," + base64String;
                                string Department = row["Department"].ToString();
                                string Designation = row["Designation"].ToString();

                                StringBuilder projectHtml = new StringBuilder();

                                projectHtml.Append("<tr>");
                                projectHtml.Append("<td><a href='profile.html' class='avatar'><img src=" + imageUrl + " alt='User Image'></a>");
                                projectHtml.Append("" + HyName + "</td>");
                                projectHtml.Append("<td>" + Department + "</td>");
                                projectHtml.Append("<td>" + Designation + "</td>");

                                projectHtml.Append("</tr>");


                                AnniversaryData.Controls.Add(new LiteralControl(projectHtml.ToString()));

                            }
                        }
                        else
                        {
                            Literal6.Text = "<div class='no-records-message'>No Records Found...</div>";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void NewJoinList()
        {
            try
            {
                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    using (SqlCommand sqlcmd = new SqlCommand("SELECT * FROM Employees WHERE YEAR(DOJ) = YEAR(GETDATE()) AND MONTH(DOJ) = MONTH(GETDATE())", connection))
                    {
                        connection.Open();
                        SqlDataAdapter da1 = new SqlDataAdapter(sqlcmd);
                        DataSet ds1 = new DataSet();
                        da1.Fill(ds1);
                        if (ds1.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow row in ds1.Tables[0].Rows)
                            {
                                string HyName = row["FirstName"].ToString();

                                //byte[] imageData = (byte[])row["Image"];
                                //string base64String = Convert.ToBase64String(imageData);
                                //string imageUrl = "data:image/jpeg;base64," + base64String;
                                object imageDataObj = row["Image"];
                                byte[] imageData = null;

                                if (imageDataObj != DBNull.Value)
                                {
                                    imageData = (byte[])imageDataObj;
                                }
                                else
                                {
                                    imageData = new byte[0];
                                }

                                string base64String = Convert.ToBase64String(imageData);
                                string imageUrl = "data:image/jpeg;base64," + base64String;
                                string Department = row["Department"].ToString();
                                string Designation = row["Designation"].ToString();

                                StringBuilder projectHtml = new StringBuilder();

                                projectHtml.Append("<tr>");
                                projectHtml.Append("<td><a href='#' class='avatar'><img src=" + imageUrl + " alt='User Image'></a>");
                                projectHtml.Append("" + HyName + "</td>");
                                projectHtml.Append("<td>" + Department + "</td>");
                                projectHtml.Append("<td>" + Designation + "</td>");

                                projectHtml.Append("</tr>");


                                NewJoin.Controls.Add(new LiteralControl(projectHtml.ToString()));

                            }
                        }
                        else
                        {
                            Literal7.Text = "<div class='no-records-message'>No Records Found...</div>";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }  
        public void RelievingList()
        {
            try
            {
                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    using (SqlCommand sqlcmd = new SqlCommand("SELECT * FROM Employees WHERE EmpInActiveDate BETWEEN GETDATE() AND DATEADD(day, 7, GETDATE()) AND status = '1'", connection))
                    {
                        connection.Open();
                        SqlDataAdapter da1 = new SqlDataAdapter(sqlcmd);
                        DataSet ds1 = new DataSet();
                        da1.Fill(ds1);
                        if (ds1.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow row in ds1.Tables[0].Rows)
                            {
                                string HyName = row["FirstName"].ToString();

                                object imageDataObj = row["Image"];
                                byte[] imageData = null;

                                if (imageDataObj != DBNull.Value)
                                {
                                    imageData = (byte[])imageDataObj;
                                }
                                else
                                {
                                    imageData = new byte[0];
                                }

                                string base64String = Convert.ToBase64String(imageData);
                                string imageUrl = "data:image/jpeg;base64," + base64String;
                                string Department = row["Department"].ToString();
                                string Designation = row["Designation"].ToString();

                                StringBuilder projectHtml = new StringBuilder();

                                projectHtml.Append("<tr>");
                                projectHtml.Append("<td><a href='profile.html' class='avatar'><img src=" + imageUrl + " alt='User Image'></a>");
                                projectHtml.Append("" + HyName + "</td>");
                                projectHtml.Append("<td>" + Department + "</td>");
                                projectHtml.Append("<td>" + Designation + "</td>");
                                projectHtml.Append("</tr>");

                                RelievingData.Controls.Add(new LiteralControl(projectHtml.ToString()));
                            }
                        }
                        else
                        {
                            Literal8.Text = "<div class='no-records-message'>No Records Found...</div>";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}