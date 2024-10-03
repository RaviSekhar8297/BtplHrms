using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;
using System.Reflection.Emit;
using System.Data.Common;
using DocumentFormat.OpenXml.Office2010.Excel;
using System.Drawing;
using DocumentFormat.OpenXml.Bibliography;
using Human_Resource_Management.Roles.Employee;

namespace Human_Resource_Management
{
    public partial class AdminOvertime : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //  BindAllOverTime();
                // BindCount();
                BindAllRequestData();
                int totalEmployeeCount = GetPendingRequest();
                Label4.Text = totalEmployeeCount.ToString();
                RejectedRequests();
                BindRequestCounts();
            }
        }
        public void BindAllOverTime()
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand("sp_GetEmployeeOvertime", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            overTime.Controls.Clear();
                            while (reader.Read())
                            {
                                string Name = reader["LastName"].ToString();
                                DateTime formattedDate = Convert.ToDateTime(reader["DateOfTransaction"]);
                                string DateOfTransaction = formattedDate.ToString("yyyy-MM-dd");

                                string MinTimeOfTransaction = reader["MinTimeOfTransaction"].ToString();
                                string MaxTimeOfTransaction = reader["MaxTimeOfTransaction"].ToString();
                                string InOvertime = reader["InOvertime"].ToString();
                                string OutOvertime = reader["OutOvertime"].ToString();
                                string TotalOvertime = reader["TotalOvertime"].ToString();
                                string UserId = reader["UserId"].ToString();

                                byte[] imageData = reader["Image"] as byte[];
                                string imageUrl = "";

                                if (imageData != null)
                                {
                                    try
                                    {
                                        string base64String = Convert.ToBase64String(imageData);
                                        imageUrl = "data:image/jpeg;base64," + base64String;
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine("Error converting image to base64: " + ex.Message);
                                    }
                                }

                                StringBuilder projectHtml = new StringBuilder();
                                projectHtml.Append("<tr>");
                                projectHtml.Append("<td><a href='profile.html' class='avatar'><img src='" + imageUrl + "' alt='User Image'></a>");
                                projectHtml.Append("<a href='profile.html'>" + Name + " </a></td>");
                                projectHtml.Append("<td>" + UserId + "</td>");
                                projectHtml.Append("<td>" + DateOfTransaction + "</td>");
                                projectHtml.Append("<td>" + InOvertime + "</td>");
                                projectHtml.Append("<td>" + OutOvertime + "</td>");
                                projectHtml.Append("<td>" + TotalOvertime + "</td>");
                                projectHtml.Append("<td><a class='dropdown-item' href='#' data-bs-toggle='modal' data-bs-target='#edit_overtime' onclick=\"editovertime('" + Name + "','" + UserId + "','" + DateOfTransaction + "','" + InOvertime + "','" + OutOvertime + "','" + TotalOvertime + "')\"><i class='fa-solid fa-pencil m-r-5' style='color:#057a28;'></i></a></td>");
                                projectHtml.Append("</tr>");

                                overTime.Controls.Add(new LiteralControl(projectHtml.ToString()));
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


        public void BindCount()
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = @"SELECT COUNT(*) AS TotalCount 
FROM (
    SELECT 
        CONVERT(date, ta.DateOfTransaction) AS DateOfTransaction,
        CONVERT(time, MIN(ta.DateOfTransaction)) AS MinTimeOfTransaction,
        CONVERT(time, MAX(ta.DateOfTransaction)) AS MaxTimeOfTransaction,
        CONVERT(varchar(5), DATEADD(minute, DATEDIFF(minute, MIN(ta.DateOfTransaction), MAX(ta.DateOfTransaction)), 0), 108) AS TimeDifferenceInHHMM,
        CASE 
            WHEN CONVERT(time, MIN(ta.DateOfTransaction)) < '09:30:00' THEN CONVERT(varchar(5), DATEADD(minute, DATEDIFF(minute, CONVERT(time, MIN(ta.DateOfTransaction)), '09:30:00'), 0), 108)
            ELSE '00:00'
        END AS InOvertime,
        CASE 
            WHEN CONVERT(time, MAX(ta.DateOfTransaction)) > '18:30:00' THEN CONVERT(varchar(5), DATEADD(minute, DATEDIFF(minute, '18:30:00', CONVERT(time, MAX(ta.DateOfTransaction))), 0), 108)
            ELSE '00:00'
        END AS OutOvertime,
        CONVERT(varchar(5), DATEADD(MINUTE, 
            CASE WHEN CONVERT(time, MIN(ta.DateOfTransaction)) < '09:30:00' THEN DATEDIFF(minute, CONVERT(time, MIN(ta.DateOfTransaction)), '09:30:00') ELSE 0 END + 
            CASE WHEN CONVERT(time, MAX(ta.DateOfTransaction)) > '18:30:00' THEN DATEDIFF(minute, '18:30:00', CONVERT(time, MAX(ta.DateOfTransaction))) ELSE 0 END, 0), 108) AS TotalOvertime,
        e.LastName,
        e.EmpId,
        ta.UserId,
        CONVERT(varchar(max), CONVERT(varbinary(max), r.Image)) AS Image 
    FROM Attendance ta 
    JOIN Employees e ON ta.UserId = e.EmpId 
    JOIN WebLogins r ON e.EmpId = r.EmpId 
    WHERE 
        MONTH(ta.DateOfTransaction) = MONTH(GETDATE()) 
        AND YEAR(ta.DateOfTransaction) = YEAR(GETDATE()) 
    GROUP BY 
        CONVERT(date, ta.DateOfTransaction),
        e.LastName,
        e.EmpId,
        ta.UserId,
        CONVERT(varchar(max), CONVERT(varbinary(max), r.Image)) 
    HAVING 
        (CASE WHEN CONVERT(time, MIN(ta.DateOfTransaction)) < '09:30:00' THEN CONVERT(varchar(5), DATEADD(minute, DATEDIFF(minute, CONVERT(time, MIN(ta.DateOfTransaction)), '09:30:00'), 0), 108) ELSE '00:00' END != '00:00') 
        OR (CASE WHEN CONVERT(time, MAX(ta.DateOfTransaction)) > '18:30:00' THEN CONVERT(varchar(5), DATEADD(minute, DATEDIFF(minute, '18:30:00', CONVERT(time, MAX(ta.DateOfTransaction))), 0), 108) ELSE '00:00' END != '00:00')
) AS mainQuery;
";

                           connection.Open();
                           int totalCount = (int)command.ExecuteScalar();
                            Label1.Text = totalCount.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                Label1.Text = "Error fetching data: " + ex.Message;
            }
        }

        private int GetPendingRequest()
        {
            int count = 0;
            using (SqlConnection connection = new SqlConnection((ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString)))
            {
                string query = "SELECT COUNT(*) FROM AttendanceRequest WHERE ManagerApprove='Process' ";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    count = (int)command.ExecuteScalar();
                }
            }
            return count;
        }

        private void RejectedRequests()
        {
            int count = 0;
            string connectionString = ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString;

            string query = "SELECT COUNT(*) FROM AttendanceRequest WHERE ManagerApprove = @ManagerApprove";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ManagerApprove", "Rejected");

                        connection.Open();
                        count = (int)command.ExecuteScalar();
                        Label3.Text = count.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                Label3.Text = "Error loading data";
                throw ex;
            }
        }
        private void BindAllRequestData()
        {
            try
            {
                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    using (SqlCommand sqlcmd = new SqlCommand("SELECT * FROM AttendanceRequest ", connection))
                    {
                        connection.Open();
                        SqlDataAdapter da1 = new SqlDataAdapter(sqlcmd);
                        DataSet ds1 = new DataSet();
                        da1.Fill(ds1);
                        if (ds1.Tables[0].Rows.Count > 0)
                        {
                            int S_No = 1;
                            foreach (DataRow row in ds1.Tables[0].Rows)
                            {
                                string Id = row["Id"].ToString();
                                string EmpId = row["EmpId"].ToString();
                                string Name = row["Name"].ToString();

                                DateTime? cdob = row["RequestDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(row["RequestDate"]);
                                string RequestDate = cdob.HasValue ? cdob.Value.ToString("yyyy-MM-dd") : string.Empty;

                                string PunchTime = row["PunchTime"].ToString();
                                string RequestType = row["RequestType"].ToString(); 
                                string ManagerApprove = row["ManagerApprove"].ToString();
                                string Reason = row["Reason"].ToString();
                                string BranchId = row["BranchId"].ToString();

                                string HRApprove = row["HRApprove"].ToString();
                                StringBuilder projectHtml = new StringBuilder();
                                projectHtml.Append("<tr>");
                                projectHtml.Append("<td>" + S_No + "</td>");
                                projectHtml.Append("<td>" + Name + "</td>");
                                projectHtml.Append("<td>" + EmpId + "</td>");
                                projectHtml.Append("<td>" + RequestDate + "</td>");
                                projectHtml.Append("<td>" + RequestType + "</td>");
                                projectHtml.Append("<td>" + PunchTime + "</td>");
                                projectHtml.Append("<td>" + Reason + "</td>");
                                projectHtml.Append("<td>" + ManagerApprove + "</td>");
                                projectHtml.Append("<td>" + HRApprove + "</td>");
                                if (RequestType == "OverTime")
                                {
                                    if ((HRApprove == "Approved" || HRApprove == "Rejected") || (ManagerApprove == "Approved" || ManagerApprove == "Rejected"))
                                    {

                                    }
                                    else
                                    {
                                        projectHtml.Append("<td><a class='edit' href='#' data-bs-toggle='modal' data-bs-target='#edit_overtime' onclick =\"AproveRequest('" + Id + "','" + Name + "','" + EmpId + "','" + RequestDate + "','" + RequestType + "','" + PunchTime + "','" + Reason + "','" + ManagerApprove + "','" + BranchId + "')\" ><i class='fa-solid fa-pencil m-r-5' style='color:green;'></i></a></td>");
                                    }
                                }
                                else
                                {
                                    if ((HRApprove == "Approved" || HRApprove == "Rejected") || (ManagerApprove == "Approved" || ManagerApprove == "Rejected"))
                                    {

                                    }
                                    else
                                    {
                                        projectHtml.Append("<td><a class='edit' href='#' data-bs-toggle='modal' data-bs-target='#edit_overtime' onclick =\"AproveRequest('" + Id + "','" + Name + "','" + EmpId + "','" + RequestDate + "','" + RequestType + "','" + PunchTime + "','" + Reason + "','" + ManagerApprove + "','" + BranchId + "')\" ><i class='fa-solid fa-pencil m-r-5' style='color:green;'></i></a></td>");
                                    }
                                }
                                projectHtml.Append("</tr>");

                                overTime.Controls.Add(new LiteralControl(projectHtml.ToString()));
                                S_No++;
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


        private void BindRequestCounts()
        {
            try
            {

                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SELECT COUNT(*) AS TotalRequests FROM AttendanceRequest WHERE RequestDate >= DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()) - 2, 0) AND RequestDate < DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()) + 1, 0) ", con);
                    int TotalRequests = (int)cmd.ExecuteScalar();

                    cmd.CommandText = "SELECT COUNT(*) AS managerapprove FROM AttendanceRequest WHERE HRApprove='Approved' AND RequestDate >= DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()) - 2, 0) AND RequestDate < DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()) + 1, 0) ";
                    int managerapprove = (int)cmd.ExecuteScalar();

                    cmd.CommandText = "SELECT COUNT(*) AS pendindrequests FROM AttendanceRequest WHERE HRApprove='Pending' AND RequestDate >= DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()) - 2, 0) AND RequestDate < DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()) + 1, 0)  ";
                    int pendindrequests = (int)cmd.ExecuteScalar();

                    cmd.CommandText = "SELECT COUNT(*) AS Rejectedrequests FROM AttendanceRequest WHERE HRApprove='Rejected' AND RequestDate >= DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()) - 2, 0) AND RequestDate < DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()) + 1, 0) ";
                    int Rejected = (int)cmd.ExecuteScalar();
                    cmd.CommandTimeout = 9000;
                  
                    Label1.Text = TotalRequests.ToString();
                    Label2.Text = managerapprove.ToString();
                    Label4.Text = pendindrequests.ToString();
                    Label3.Text = Rejected.ToString();
                   
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnapprove_Click(object sender, EventArgs e)
        {
            try
            {
                string overtime = "OverTime";
                string approve = "Approved";
                string employeeId = HiddenField2.Value;
                int year = DateTime.Now.Year;
                string newPunchTime = txttime.Text;

                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    con.Open();

                    // Check if there are any records for the given employee and year
                    using (SqlCommand countCmd = new SqlCommand("SELECT COUNT(*) FROM AttendanceRequest WHERE EmpId = @EmpId AND Year = @Year AND RequestType = @RequestType", con))
                    {
                        countCmd.Parameters.AddWithValue("@EmpId", employeeId);
                        countCmd.Parameters.AddWithValue("@Year", year);
                        countCmd.Parameters.AddWithValue("@RequestType", overtime);

                        int rowCount = (int)countCmd.ExecuteScalar();

                        if (rowCount > 0)
                        {
                            // Process records if any exist
                            using (SqlCommand maxIdCmd = new SqlCommand("SELECT Id, OverTime FROM AttendanceRequest WHERE EmpId = @EmpId AND Year = @Year AND RequestType = @RequestType AND Status = '1' ORDER BY Id DESC", con))
                            {
                                maxIdCmd.Parameters.AddWithValue("@EmpId", employeeId);
                                maxIdCmd.Parameters.AddWithValue("@Year", year);
                                maxIdCmd.Parameters.AddWithValue("@RequestType", overtime);

                                SqlDataReader reader = maxIdCmd.ExecuteReader();
                                if (reader.Read())
                                {
                                    int id = reader.GetInt32(reader.GetOrdinal("Id"));
                                    string oldPunchTime = reader.GetString(reader.GetOrdinal("OverTime"));
                                    reader.Close();

                                    TimeSpan oldPunchTimeSpan = TimeSpan.Parse(oldPunchTime);
                                    TimeSpan newPunchTimeSpan = TimeSpan.Parse(newPunchTime);
                                    string newottime = newPunchTimeSpan.ToString(@"hh\:mm");
                                    TimeSpan totalPunchTimeSpan = oldPunchTimeSpan.Add(newPunchTimeSpan);

                                    TimeSpan maxAllowedTimeSpan8Hours = new TimeSpan(8, 0, 0); // 8 hours
                                    TimeSpan maxAllowedTimeSpan4Hours = new TimeSpan(4, 0, 0); // 4 hours
                                    TimeSpan overtimePunchTimeSpan = TimeSpan.Zero;
                                    string totalPunchTime;

                                    if (totalPunchTimeSpan > maxAllowedTimeSpan8Hours)
                                    {
                                        // Calculate overtime as the difference between total punch time and max allowed time (8 hours)
                                        overtimePunchTimeSpan = totalPunchTimeSpan - maxAllowedTimeSpan8Hours;
                                        totalPunchTime = overtimePunchTimeSpan.ToString(@"hh\:mm");
                                    }
                                    else
                                    {
                                        overtimePunchTimeSpan = totalPunchTimeSpan - maxAllowedTimeSpan4Hours;
                                        totalPunchTime = overtimePunchTimeSpan.ToString(@"hh\:mm");
                                    }

                                    // Update the record with the new total PunchTime
                                    using (SqlCommand updateCmd = new SqlCommand("UPDATE AttendanceRequest SET Status = '1', HRApprove = @HRApprove, PunchTime = @PunchTime, OverTime = @OverTime WHERE Id = @Id", con))
                                    {
                                        updateCmd.Parameters.AddWithValue("@HRApprove", approve);
                                        updateCmd.Parameters.AddWithValue("@PunchTime", newottime);
                                        updateCmd.Parameters.AddWithValue("@OverTime", totalPunchTime);
                                        updateCmd.Parameters.AddWithValue("@Id", HiddenField1.Value);
                                        int rowsAffected = updateCmd.ExecuteNonQuery();

                                        if (rowsAffected > 0)
                                        {
                                            // Determine leave increment based on total punch time
                                            decimal leaveIncrement = 0.0m;
                                            decimal balanceIncrement = 0.0m;

                                            if (totalPunchTimeSpan > maxAllowedTimeSpan8Hours)
                                            {
                                                leaveIncrement = 1.0m; // Increment for exceeding 8 hours
                                                balanceIncrement = 1.0m; // Update balance by 1.0
                                            }
                                            else if (totalPunchTimeSpan > maxAllowedTimeSpan4Hours)
                                            {
                                                leaveIncrement = 0.5m; // Increment for exceeding 4 hours
                                                balanceIncrement = 0.5m; // Update balance by 0.5
                                            }

                                            if (leaveIncrement > 0)
                                            {
                                                using (SqlCommand updateLeavesCmd = new SqlCommand("UPDATE LeavesList SET TotalCampOffLeaves = TotalCampOffLeaves + @LeaveIncrement, BalenceCampOffLeaves = BalenceCampOffLeaves + @BalanceIncrement WHERE EmpId = @EmpId", con))
                                                {
                                                    updateLeavesCmd.Parameters.AddWithValue("@LeaveIncrement", leaveIncrement);
                                                    updateLeavesCmd.Parameters.AddWithValue("@BalanceIncrement", balanceIncrement);
                                                    updateLeavesCmd.Parameters.AddWithValue("@EmpId", HiddenField2.Value);
                                                    updateLeavesCmd.ExecuteNonQuery();
                                                }
                                            }

                                            Response.Write("<script>alert('Approved Successfully...');</script>");

                                            BindAllRequestData();
                                            BindRequestCounts();
                                        }
                                        else
                                        {
                                            Response.Write("<script>alert('Failed...');</script>");
                                            BindAllRequestData();
                                        }
                                    }

                                }
                                else
                                {
                                    reader.Close();

                                    using (SqlCommand updateCmd = new SqlCommand("UPDATE AttendanceRequest SET Status = '1', HRApprove = @HRApprove WHERE Id = @Id", con))
                                    {
                                        updateCmd.Parameters.AddWithValue("@HRApprove", approve);
                                        updateCmd.Parameters.AddWithValue("@Id", HiddenField1.Value);

                                        int i = updateCmd.ExecuteNonQuery();
                                        if (i > 0)
                                        {
                                            Response.Write("<script>alert('Approved Successfully...');</script>");
                                            BindAllRequestData();
                                            BindRequestCounts();
                                        }
                                        else
                                        {
                                            Response.Write("<script>alert('Failed...');</script>");
                                            BindAllRequestData();
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            // If no records exist, approve the specific record without changing PunchTime
                            using (SqlCommand updateCmd = new SqlCommand("UPDATE AttendanceRequest SET Status = '1', HRApprove = @HRApprove WHERE Id = @Id", con))
                            {
                                updateCmd.Parameters.AddWithValue("@HRApprove", approve);
                                updateCmd.Parameters.AddWithValue("@Id", HiddenField1.Value);

                                int i = updateCmd.ExecuteNonQuery();
                                if (i > 0)
                                {
                                    Response.Write("<script>alert('Approved Successfully...');</script>");
                                    BindAllRequestData();
                                    BindRequestCounts();
                                }
                                else
                                {
                                    Response.Write("<script>alert('Failed...');</script>");
                                    BindAllRequestData();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception for debugging (replace with actual logging)
                System.Diagnostics.Debug.WriteLine(ex.Message);
                Response.Write("<script>alert('An error occurred. Please try again later.');</script>");
            }
        }

        protected void btnreject_Click(object sender, EventArgs e)
        {
            try
            {
                string Rejected = "Rejected";
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Update AttendanceRequest set Status='1' ,HRApprove=@HRApprove where Id=@Id", con);
                    cmd.Parameters.AddWithValue("@HRApprove", Rejected);
                    cmd.Parameters.AddWithValue("@Id", HiddenField1.Value);
                    int i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        Response.Write("<script>alert('Rejected Successufully...')</script>");
                        BindAllRequestData();
                    }
                    else
                    {
                        Response.Write("<script>Alert('Failed...')</script>");
                        BindAllRequestData();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnupdate_Click(object sender, EventArgs e)
        {
            try
            {
                string Rejected = "RequestData";
                string Approve = "Approved";
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Insert into Attendance (OrgId, MachineId, UserId, UserName, DateOfTransaction, OutDoor) values (@OrgId, @MachineId, @UserId, @UserName, @DateOfTransaction, @OutDoor)", con);
                    cmd.Parameters.AddWithValue("@OrgId", "9999");
                    cmd.Parameters.AddWithValue("@MachineId", HiddenField3.Value);
                    cmd.Parameters.AddWithValue("@UserId", txtid.Text);
                    cmd.Parameters.AddWithValue("@UserName", txtname.Text);
                    cmd.Parameters.AddWithValue("@DateOfTransaction", txttime.Text);
                    cmd.Parameters.AddWithValue("@OutDoor", Rejected);

                    SqlCommand cmd2 = new SqlCommand("Update AttendanceRequest set Status='1', HRApprove=@HRApprove where Id=@Id", con);
                    cmd2.Parameters.AddWithValue("@HRApprove", Approve);
                    cmd2.Parameters.AddWithValue("@Id", HiddenField1.Value);

                    int i = cmd.ExecuteNonQuery();
                    int k = cmd2.ExecuteNonQuery();
                    if (i > 0 && k > 0)
                    {
                        Response.Write("<script>alert('Request Accepted Successfully...')</script>");
                        BindAllRequestData();
                    }
                    else
                    {
                        Response.Write("<script>alert('Failed...');</script>");
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('An error occurred. Please try again later.');</script>");
                throw ex;
            }

        }
    }

}