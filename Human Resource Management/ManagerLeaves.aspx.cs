using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Human_Resource_Management
{
    public partial class ManagerLeaves : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LeavesCount();               
                EmployeeLeavesList();
                SelfLeaves();
            }
            if (!IsPostBack)
            {
                HiddenField4.Value = "0";
            }
            else
            {
                if (HiddenField4.Value == "1")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
                }

            }
        }
        public void LeavesCount()
        {
            if (Session["EmpId"] == null || Session["EmpId"].ToString() == "")
            {

            }
            else
            {
                try
                {
                    string connectionString = ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString;

                    using (var connection1 = new SqlConnection(connectionString))
                    {
                        connection1.Open();
                        string query = "SELECT BalenceCasualLeaves, BalenceSickLaves, BalenceCampOffLeaves FROM LeavesList WHERE EmpId = @EmpId AND Year = @Year";

                        using (SqlCommand cmd = new SqlCommand(query, connection1))
                        {
                            cmd.Parameters.AddWithValue("@EmpId", Session["EmpId"].ToString());
                            cmd.Parameters.AddWithValue("@Year", DateTime.Now.Year);

                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    // Check for null and assign value or default to 0
                                    double balenceCasualLeaves = !reader.IsDBNull(reader.GetOrdinal("BalenceCasualLeaves"))
                                                                 ? (double)reader.GetDecimal(reader.GetOrdinal("BalenceCasualLeaves"))
                                                                 : 0;

                                    double balenceSickLeaves = !reader.IsDBNull(reader.GetOrdinal("BalenceSickLaves"))
                                                               ? (double)reader.GetDecimal(reader.GetOrdinal("BalenceSickLaves"))
                                                               : 0;

                                    double balenceCampOffLeaves = !reader.IsDBNull(reader.GetOrdinal("BalenceCampOffLeaves"))
                                                                  ? (double)reader.GetDecimal(reader.GetOrdinal("BalenceCampOffLeaves"))
                                                                  : 0;


                                    double sls = (12 - DateTime.Now.Month) + 1;
                                    lblcasualleavescount.Text = balenceCasualLeaves.ToString();
                                    lblsickleavescount.Text = balenceSickLeaves.ToString();
                                    lblcompoff.Text = balenceCampOffLeaves.ToString();
                                    lbltotalleaves.Text = (balenceCasualLeaves + balenceSickLeaves + balenceCampOffLeaves).ToString();
                                }
                                else
                                {
                                    lbltotalleaves.Text = "0";
                                    lblcasualleavescount.Text = "0";
                                    lblsickleavescount.Text = "0";
                                    lblcompoff.Text = "0";
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Log the exception
                    lbltotalleaves.Text = "Error";
                    lblcasualleavescount.Text = "Error";
                    lblsickleavescount.Text = "Error";
                    lblcompoff.Text = "Error";
                    throw ex;
                }
            }
        }

        public void EmployeeLeavesList()
        {
            string logid = Session["EmpId"].ToString();
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "SELECT LeavesStatus22.*, Employees.FirstName , Employees.Image FROM LeavesStatus22 JOIN Employees ON LeavesStatus22.EmployeedID = Employees.EmpId WHERE YEAR(LeavesStatus22.FromDate) = YEAR(GETDATE()) AND MONTH(LeavesStatus22.FromDate) = MONTH(GETDATE()) and LeavesStatus22.EmployeedID != '"+ logid + "' ORDER BY LeavesStatus22.FromDate,LeavesStatus22.LeaveId DESC; ";
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            EditEmployeeLeaves.Controls.Clear();
                            while (reader.Read())
                            {
                                string Name = reader["FirstName"].ToString();

                                object imageDataObj = reader["Image"];
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

                                string LeaveId = reader["LeaveId"].ToString();

                                string leavetype = reader["leave_type"].ToString();

                                DateTime formattedFromDate = (DateTime)reader["FromDate"];
                                string FromDate = formattedFromDate.ToString("yyyy-MM-dd");

                                DateTime toDate = (DateTime)reader["ToDate"];
                                string ToDate = toDate.ToString("yyyy-MM-dd");

                                string Duration = reader["Duration"].ToString();
                                string Reason = reader["ReasontoApply"].ToString();
                                string Status = reader["StatusReason"].ToString();
                                string EmpId = reader["EmployeedID"].ToString();
                                //ListItem selectedItem = ddledstatus.Items.FindByValue(Status);
                                //if (selectedItem != null)
                                //{
                                //    selectedItem.Selected = true;
                                //}

                                StringBuilder projectHtml = new StringBuilder();
                                projectHtml.Append("<tr>");
                                projectHtml.Append("<td><a href='#' class='avatar'><img src=" + imageUrl + " alt='User Image'></a>");
                                projectHtml.Append("<a href='profile.html'>" + Name + " </a></td>");
                                //  projectHtml.Append("<td>" + LeaveId + "</td>");
                                projectHtml.Append("<td style='color:blue;'>" + leavetype + "</td>");
                                projectHtml.Append("<td>" + FromDate + "</td>");
                                projectHtml.Append("<td>" + ToDate + "</td>");
                                projectHtml.Append("<td>" + Duration + "</td>");
                                projectHtml.Append("<td>" + Reason + "</td>");
                                projectHtml.Append("<td>" + Status + "</td>");
                                if (Status.Trim() == "Approved" || Status.Trim() == "Rejected")
                                {

                                }
                                else
                                {
                                    projectHtml.Append("<td><a class='dropdown - item' href='#' data-bs-toggle='modal' data-bs-target='#edit_empleave'onclick =\"editEmpLeave('" + Name + "','" + LeaveId + "','" + leavetype + "','" + FromDate + "','" + ToDate + "','" + Duration + "','" + Reason + "','" + EmpId + "')\" ><i class='fa-solid fa-pencil m-r-5' style='color:#057a28;'></i></a></td>");

                                }
                                projectHtml.Append("</tr>");

                                EditEmployeeLeaves.Controls.Add(new LiteralControl(projectHtml.ToString()));

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



        protected void btnleavedelete_Click(object sender, EventArgs e)
        {
            try
            {
                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    using (SqlCommand sqlcmd = new SqlCommand("UPDATE LeavesStatus22 SET Status='0' WHERE LeaveId=@LeaveId", connection))
                    {
                        connection.Open();
                        sqlcmd.Parameters.AddWithValue("@LeaveId", HiddenField1.Value.Trim());
                        int i = sqlcmd.ExecuteNonQuery();
                        if (i > 0)
                        {
                            Response.Write("<script>alert('Deleted Successfully..')</script>");
                            EmployeeLeavesList();
                        }
                        else
                        {
                            Response.Write("<script>alert('Not Deleted..')</script>");
                            EmployeeLeavesList();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnleaveedit_Click(object sender, EventArgs e)
        {
            try
            {
                //DateTime date1 = DateTime.Parse(TextBox1.Text);
                //DateTime date2 = DateTime.Parse(TextBox2.Text);
                //TimeSpan dateDifference = date2 - date1;
                //int daysDifference = Math.Abs(dateDifference.Days) + 1;
                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    using (SqlCommand sqlcmd = new SqlCommand("UPDATE LeavesStatus22 SET Statu='0' WHERE LeaveId=@LeaveId", connection))
                    {
                        connection.Open();
                        sqlcmd.Parameters.AddWithValue("@LeaveId", HiddenField2.Value.Trim());
                        int i = sqlcmd.ExecuteNonQuery();
                        if (i > 0)
                        {
                            Response.Write("<script>alert('Updated Successfully..')</script>");
                            EmployeeLeavesList();
                        }
                        else
                        {
                            Response.Write("<script>alert('Not Deleted..')</script>");
                            EmployeeLeavesList();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

       
        public void SelfLeaves()
        {
            if (Session["EmpId"] == null || Session["EmpId"].ToString() == "")
            {

            }
            else
            {
                string currentMonthName = DateTime.Now.ToString("MMMM");
                try
                {
                    using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                    {
                        using (SqlCommand sqlcmd = new SqlCommand("SELECT * FROM LeavesStatus22 WHERE EmployeedID = @EmpId ", connection))

                        {
                            sqlcmd.Parameters.AddWithValue("@EmpId", Session["EmpId"].ToString());
                            connection.Open();
                            SqlDataAdapter da1 = new SqlDataAdapter(sqlcmd);
                            DataSet ds1 = new DataSet();
                            da1.Fill(ds1);
                            if (ds1.Tables[0].Rows.Count > 0)
                            {
                                foreach (DataRow row in ds1.Tables[0].Rows)
                                {
                                    string ApplyDate = row["AppliedDate"].ToString();
                                    DateTime appliedDateTime = Convert.ToDateTime(ApplyDate);
                                    string formattedDate = appliedDateTime.ToString("yyyy-MM-dd");

                                    string rs = row["ReasontoApply"].ToString();
                                    DateTime fromDate = Convert.ToDateTime(row["FromDate"]);
                                    string fromDate1 = fromDate.ToString("yyyy-MM-dd");

                                    DateTime toDate = Convert.ToDateTime(row["ToDate"]);
                                    string toDate1 = toDate.ToString("yyyy-MM-dd");

                                    TimeSpan timeDifference = toDate - fromDate;
                                    int numberOfDays = (int)timeDifference.TotalDays;
                                    numberOfDays++;
                                    string sr = row["StatusReason"].ToString().Trim();
                                    string sstatus = row["Status"].ToString();
                                    string appbyname = row["ApprovedByName"].ToString();
                                    string leaveType = row["leave_type"].ToString();
                                    string leaveID = row["LeaveId"].ToString();

                                    var Editleave = Session["EditLeave"] as string;
                                    var Deleteleave = Session["DeleteLeave"] as string;
                                    var Role = Session["Role"] as string;
                                    StringBuilder projectHtml = new StringBuilder();


                                    projectHtml.Append("</tr>");
                                    projectHtml.Append("</thead>");

                                    projectHtml.Append("<tbody>");
                                    projectHtml.Append("<tr>");
                                    projectHtml.Append("<td>" + formattedDate + "</td>");
                                    projectHtml.Append("<td>" + rs + "</td>");
                                    projectHtml.Append("<td>" + fromDate1 + "</td>");
                                    projectHtml.Append("<td>" + toDate1 + "</td>");
                                    projectHtml.Append("<td>" + numberOfDays + "</td>");
                                    projectHtml.Append("<td>" + leaveType + "</td>");
                                    if (sr == "Rejected")
                                    {
                                        projectHtml.Append("<td style='color:red;'>" + sr + "</td>");
                                    }
                                    else if (sr == "Approved")
                                    {
                                        projectHtml.Append("<td style='color:#34734e;'>" + sr + "</td>");
                                    }
                                    else
                                    {
                                        projectHtml.Append("<td style='color:#de7e2a;'>" + sr + "</td>");
                                    }

                                    projectHtml.Append("<td>" + appbyname + "</td>");


                                    if (sr == "Rejected" || sr == "Approved")
                                    {

                                    }
                                    else
                                    {
                                        projectHtml.Append("<td><a class='dropdown - item' href='#' data-bs-toggle='modal' data-bs-target='#edit_leave' onclick=\"edit_selfleave('" + leaveID + "','" + leaveType + "','" + fromDate1 + "','" + toDate1 + "','" + numberOfDays + "','" + rs + "')\"><i class='fa-solid fa-pencil m-r-5'></i></a></td>");

                                    }

                                    projectHtml.Append("</tr>");
                                    projectHtml.Append("</tbody>");
                                    SelfLeavesContainer.Controls.Add(new LiteralControl(projectHtml.ToString()));
                                }
                            }
                            else
                            {
                                Literal1.Text = "<div class='no-records-message'>Here no leaves Applied the month of " + currentMonthName + "...</div>";
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
        protected void btnacceptleave_Click(object sender, EventArgs e)
        {
            try
            {
                string leaveId = hdnLeaveId.Value;
                DateTime fromDate = DateTime.Parse(txtedfromdt.Text);
                DateTime toDate = DateTime.Parse(txtedtodt.Text);
                TimeSpan difference = toDate - fromDate;
                double durationInDays = difference.TotalDays + 1;
                string Status = "1";
                string StatusReason = "Approved";
                string connectionString = ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "UPDATE LeavesStatus22 SET FromDate=@FromDate, ToDate=@ToDate, Duration=@Duration, StatusReason=@StatusReason, ReasontoApply=@ReasontoApply, Status=@Status, StatusDate=@StatusDate, approvedby=@approvedby, depthead_id=@depthead_id, ApprovedByName=@ApprovedByName WHERE LeaveId=@LeaveId";
                        command.Parameters.AddWithValue("@FromDate", fromDate);
                        command.Parameters.AddWithValue("@ToDate", toDate);
                        command.Parameters.AddWithValue("@Duration", durationInDays);
                        command.Parameters.AddWithValue("@StatusReason", StatusReason);
                        command.Parameters.AddWithValue("@ReasontoApply", txtedreason.Text);
                        command.Parameters.AddWithValue("@Status", Status);
                        command.Parameters.AddWithValue("@StatusDate", DateTime.Now);

                        // Handle approvedby and depthead_id separately
                        if (long.TryParse(Session["EmpId"].ToString(), out long approvedBy))
                        {
                            command.Parameters.AddWithValue("@approvedby", approvedBy);
                        }
                        else
                        {
                            throw new Exception("Invalid EmpId format.");
                        }

                        command.Parameters.AddWithValue("@depthead_id", Session["EmpId"].ToString());
                        command.Parameters.AddWithValue("@ApprovedByName", Session["Name"].ToString());
                        command.Parameters.AddWithValue("@LeaveId", leaveId);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            // Update LeavesList
                            Response.Write("<script>alert('Approved Suucessfully....')</script>");
                            LeavesCount();                           
                        }
                        else
                        {
                            Response.Write("<script>alert('No rows were updated.')</script>");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('An error occurred while updating the database. Please try again later.')</script>");
                // Log the exception or handle it as needed
                throw ex;
            }
        }

        protected void btnreject_Click(object sender, EventArgs e)
        {
            try
            {
                string leaveId = hdnLeaveId.Value;
                DateTime fromDate = DateTime.Parse(txtedfromdt.Text);
                DateTime toDate = DateTime.Parse(txtedtodt.Text);
                double durationInDays = (toDate - fromDate).TotalDays + 1;
                string status = "1";
                string statusReason = "Rejected";
                string connectionString = ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlTransaction transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            // Update LeavesStatus22 Table
                            using (SqlCommand command = new SqlCommand())
                            {
                                command.Connection = connection;
                                command.Transaction = transaction;
                                command.CommandText = @"UPDATE LeavesStatus22 
                                            SET FromDate=@FromDate, ToDate=@ToDate, Duration=@Duration, 
                                                StatusReason=@StatusReason, ReasontoApply=@ReasontoApply, 
                                                Status=@Status, StatusDate=@StatusDate, 
                                                CL = CL - @decrCL, BalanceLeaves = BalanceLeaves + @IncreseCL, 
                                                SL = SL - @decrSL, BalanceSLeaves = BalanceSLeaves + @IncreseSL, 
                                                ML = ML - @DecrCompOff, BalanceCompOffLeaves = BalanceCompOffLeaves + @IncreseCompOff, 
                                                approvedby=@approvedby, depthead_id=@depthead_id, ApprovedByName=@ApprovedByName 
                                            WHERE LeaveId=@LeaveId";
                                command.Parameters.AddWithValue("@FromDate", fromDate);
                                command.Parameters.AddWithValue("@ToDate", toDate);
                                command.Parameters.AddWithValue("@Duration", durationInDays);
                                command.Parameters.AddWithValue("@StatusReason", statusReason);
                                command.Parameters.AddWithValue("@ReasontoApply", txtedreason.Text);
                                command.Parameters.AddWithValue("@Status", status);
                                command.Parameters.AddWithValue("@StatusDate", DateTime.Now);
                                command.Parameters.AddWithValue("@LeaveId", leaveId);

                                // Initialize the parameters with default values
                                command.Parameters.AddWithValue("@IncreseCL", 0.0);
                                command.Parameters.AddWithValue("@decrCL", 0.0);
                                command.Parameters.AddWithValue("@IncreseSL", 0.0);
                                command.Parameters.AddWithValue("@decrSL", 0.0);
                                command.Parameters.AddWithValue("@IncreseCompOff", 0.0);
                                command.Parameters.AddWithValue("@DecrCompOff", 0.0);

                                // Determine the leave type and set the correct values
                                double leaveDuration = Convert.ToDouble(txteddur.Text);
                                switch (txtedleavtype.Text)
                                {
                                    case "Casual Leave":
                                        command.Parameters["@IncreseCL"].Value = leaveDuration;
                                        command.Parameters["@decrCL"].Value = leaveDuration;
                                        break;
                                    case "Sick Leave":
                                        command.Parameters["@IncreseSL"].Value = leaveDuration;
                                        command.Parameters["@decrSL"].Value = leaveDuration;
                                        break;
                                    case "Comp-Off Leave":
                                        command.Parameters["@IncreseCompOff"].Value = leaveDuration;
                                        command.Parameters["@DecrCompOff"].Value = leaveDuration;
                                        break;
                                }

                                // Handle approvedby and depthead_id separately
                                if (long.TryParse(Session["EmpId"].ToString(), out long approvedBy))
                                {
                                    command.Parameters.AddWithValue("@approvedby", approvedBy);
                                }
                                else
                                {
                                    throw new Exception("Invalid EmpId format.");
                                }
                                command.Parameters.AddWithValue("@depthead_id", Session["EmpId"].ToString());
                                command.Parameters.AddWithValue("@ApprovedByName", Session["Name"].ToString());

                                int rowsAffected = command.ExecuteNonQuery();
                                if (rowsAffected <= 0)
                                {
                                    throw new Exception("No rows were updated in LeavesStatus22.");
                                }
                            }

                            // Update LeavesList Table
                            using (SqlCommand updateCommand = new SqlCommand())
                            {
                                updateCommand.Connection = connection;
                                updateCommand.Transaction = transaction;
                                updateCommand.CommandText = @"UPDATE LeavesList 
                                                  SET UsedCasualLeaves = UsedCasualLeaves - @IncreseCL, 
                                                      BalenceCasualLeaves = BalenceCasualLeaves + @decrCL, 
                                                      UsedSickLeaves = UsedSickLeaves - @IncreseSL, 
                                                      BalenceSickLaves = BalenceSickLaves + @decrSL, 
                                                      UsedCampOffLeaves = UsedCampOffLeaves - @IncreseCompOff, 
                                                      BalenceCampOffLeaves = BalenceCampOffLeaves + @DecrCompOff 
                                                  WHERE EmpId=@EmpId AND Year=@Year";

                                // Reuse the parameter values set earlier
                                updateCommand.Parameters.AddWithValue("@IncreseCL", 0.0);
                                updateCommand.Parameters.AddWithValue("@decrCL", 0.0);
                                updateCommand.Parameters.AddWithValue("@IncreseSL", 0.0);
                                updateCommand.Parameters.AddWithValue("@decrSL", 0.0);
                                updateCommand.Parameters.AddWithValue("@IncreseCompOff", 0.0);
                                updateCommand.Parameters.AddWithValue("@DecrCompOff", 0.0);
                                double leaveDuration = Convert.ToDouble(txteddur.Text);
                                switch (txtedleavtype.Text)
                                {
                                    case "Casual Leave":
                                        updateCommand.Parameters["@IncreseCL"].Value = leaveDuration;
                                        updateCommand.Parameters["@decrCL"].Value = leaveDuration;
                                        break;
                                    case "Sick Leave":
                                        updateCommand.Parameters["@IncreseSL"].Value = leaveDuration;
                                        updateCommand.Parameters["@decrSL"].Value = leaveDuration;
                                        break;
                                    case "Comp-Off Leave":
                                        updateCommand.Parameters["@IncreseCompOff"].Value = leaveDuration;
                                        updateCommand.Parameters["@DecrCompOff"].Value = leaveDuration;
                                        break;
                                }

                                updateCommand.Parameters.AddWithValue("@EmpId", HiddenField1.Value);
                                updateCommand.Parameters.AddWithValue("@Year", DateTime.Now.Year);

                                int updateLeaves = updateCommand.ExecuteNonQuery();
                                if (updateLeaves <= 0)
                                {
                                    throw new Exception("No leaves were updated in LeavesList.");
                                }
                            }

                            // Commit the transaction
                            transaction.Commit();
                            Response.Write("<script>alert('Leave rejected successfully.')</script>");                          
                            LeavesCount(); // Assuming this method updates the leave count display
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            Response.Write("<script>alert('An error occurred while updating the database. Please try again later.')</script>");
                            throw ex;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // You could log the error here for further investigation.
                Response.Write("<script>alert('An unexpected error occurred. Please contact support.')</script>");
                throw ex;
            }

        }

        protected void TextBox2_TextChanged(object sender, EventArgs e)
        {
            btnleaveedit.Visible = true;
            TextBox3.ReadOnly = true;
            try
            {
                DateTime startDate, toDate;

                if (!DateTime.TryParse(TextBox1.Text, out startDate) || !DateTime.TryParse(TextBox2.Text, out toDate))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Invalid date format.');", true);
                    btnleaveedit.Visible = false;
                    return;
                }

                // Check if startDate is after toDate
                if (startDate > toDate)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Start date cannot be after end date.');", true);
                    TextBox1.Text = string.Empty;
                    TextBox2.Text = string.Empty;
                    TextBox3.Text = string.Empty;
                    TextBox3.ReadOnly = true;
                    btnleaveedit.Visible = false;
                    return;
                }

                // Check if the selected dates overlap with any existing leave range
                string connectionString = ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString;
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = @"
                SELECT COUNT(*) 
                FROM LeavesStatus22 
                WHERE (FromDate <= @ToDate AND ToDate >= @StartDate)";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@StartDate", startDate);
                        cmd.Parameters.AddWithValue("@ToDate", toDate);

                        int count = (int)cmd.ExecuteScalar();

                    }
                }

                // Calculate number of days between startDate and toDate excluding Sundays
                double numberOfDays = 0;
                for (DateTime date = startDate; date <= toDate; date = date.AddDays(1))
                {
                    if (date.DayOfWeek != DayOfWeek.Sunday)
                    {
                        numberOfDays++;
                    }
                    else
                    {
                        btnleaveedit.Visible = false;
                    }
                }
                TextBox3.Text = numberOfDays.ToString();

                // Validate leave balance
                string selectedLeaveType = ddlleavesstatus.SelectedItem.Text;
                if (Session["LeaveBalance"] != null)
                {
                    double leaveBalance = (double)((Dictionary<string, double>)Session["LeaveBalance"])[selectedLeaveType];
                    if (numberOfDays > leaveBalance)
                    {
                        string script = $"alert('You have only {leaveBalance} {selectedLeaveType}(s) available.');";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", script, true);
                        TextBox2.Text = string.Empty;
                        TextBox3.Text = string.Empty;
                        TextBox3.ReadOnly = true;
                        btnleaveedit.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('An error occurred while processing the dates.');", true);
                throw ex;
            }
        }
       
        protected void ddlleavesstatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnleaveedit.Visible = true;
            TextBox3.ReadOnly = true;
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString;

                using (var connection1 = new SqlConnection(connectionString))
                {
                    connection1.Open();
                    string query = "SELECT TotalCasualLeaves, BalenceCasualLeaves, BalenceSickLaves, BalenceCampOffLeaves,Doj FROM LeavesList WHERE EmpId = @EmpId AND Year = @Year";

                    using (SqlCommand cmd = new SqlCommand(query, connection1))
                    {
                        cmd.Parameters.AddWithValue("@EmpId", Session["EmpId"].ToString());
                        cmd.Parameters.AddWithValue("@Year", DateTime.Now.Year);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                double blscl = (double)reader.GetDecimal(reader.GetOrdinal("BalenceCasualLeaves"));
                                double blssl = (double)reader.GetDecimal(reader.GetOrdinal("BalenceSickLaves"));
                                double balenceCampOffLeaves = (double)reader.GetDecimal(reader.GetOrdinal("BalenceCampOffLeaves"));


                                DateTime doj = reader.GetDateTime(reader.GetOrdinal("Doj"));
                                string dojFormatted = doj.ToString("yyyy-MM-dd");

                                // add 3 months
                                DateTime dojPlusThreeMonths = doj.AddMonths(3);
                                string dojPlusThreeMonthsFormatted = dojPlusThreeMonths.ToString("yyyy-MM");

                                // Get current year and month
                                DateTime currentDate = DateTime.Now;
                                DateTime currentYearMonth = new DateTime(currentDate.Year, currentDate.Month, 1);
                                string nowdate = currentYearMonth.ToString("yyyy-MM");

                                // Convert string dates to DateTime objects for comparison
                                DateTime dojPlusThreeMonthsDate = DateTime.ParseExact(dojPlusThreeMonthsFormatted, "yyyy-MM", null);
                                DateTime nowDate = DateTime.ParseExact(nowdate, "yyyy-MM", null);
                                double MaxValue = 5;
                                if (nowDate > dojPlusThreeMonthsDate)
                                {
                                    double balenceCasualLeaves = blscl - (12 - DateTime.Now.Month);
                                    double balenceSickLeaves = blssl - (12 - DateTime.Now.Month) + 1;


                                    Session["LeaveBalance"] = new Dictionary<string, double>
                                    {
                                        { "Casual Leave", balenceCasualLeaves },
                                        { "Sick Leave", balenceSickLeaves },
                                        { "Comp-Off Leave", balenceCampOffLeaves },
                                        { "LOP Leave", MaxValue }
                                    };

                                    // Optionally, update UI with current balance
                                    string leaveType = ddlleavesstatus.SelectedItem.Text;
                                    double balance = (double)((Dictionary<string, double>)Session["LeaveBalance"])[leaveType];
                                    if (balance == 0)
                                    {
                                        Label3.Text = $"You have already used  {leaveType}.";
                                        btnleaveedit.Visible = false;
                                    }
                                    else
                                    {
                                        Label3.Text = $"You have {balance} days of {leaveType}.";
                                    }

                                }
                                else
                                {
                                    double balenceCasualLeaves = 0.0;
                                    double balenceSickLeaves = 0.0;

                                    Session["LeaveBalance"] = new Dictionary<string, double>
                                    {
                                        { "Casual Leave", balenceCasualLeaves },
                                        { "Sick Leave", balenceSickLeaves },
                                        { "Comp-Off Leave", balenceCampOffLeaves },
                                        { "LOP Leave", MaxValue }
                                    };

                                    // Optionally, update UI with current balance
                                    string leaveType = ddlleavesstatus.SelectedItem.Text;
                                    double balance = (double)((Dictionary<string, double>)Session["LeaveBalance"])[leaveType];
                                    if (leaveType == "Casual Leave" || leaveType == "Sick Leave")
                                    {
                                        Label3.Text = $"You will not have {leaveType} for THREE months.";
                                        btnleaveedit.Visible = false;
                                    }
                                    else
                                    {
                                        Label3.Text = $"You have {balance} days of {leaveType}.";
                                        btnleaveedit.Visible = true;
                                    }

                                }

                            }
                            else
                            {
                                Label2.Text = "0";
                            }
                        }
                    }
                }
                HiddenField4.Value = "1";
            }
          
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('An error occurred while checking leave balance.');", true);
                throw ex;
            }
        }

    }
}