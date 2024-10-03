using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Text;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Reflection.Emit;
using System.Data.SqlTypes;

namespace Human_Resource_Management
{
    public partial class EmployeeAttendanceRequest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                 RequestBindData();               
            }
            if (!IsPostBack)
            {
                HiddenField1.Value = "0";
            }
            else
            {
                if (HiddenField1.Value == "1")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
                }
            }
        }

        public void RequestBindData()
        {
            try
            {
                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    using (SqlCommand sqlcmd = new SqlCommand("SELECT * FROM AttendanceRequest WHERE EmpId = @EmpId", connection))
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
                                string EmpId = row["EmpId"].ToString();
                                string Name = row["Name"].ToString();
                                string RequestType = row["RequestType"].ToString();
                                string PunchTime = row["PunchTime"].ToString();
                                string RequestDate = row["RequestDate"].ToString();
                                string ManagerApprove = row["ManagerApprove"].ToString(); 
                                string HRApprove = row["HRApprove"].ToString();
                               // string Status = row["Status"].ToString();

                                StringBuilder projectHtml = new StringBuilder();
                                projectHtml.Append("<tr>");
                                projectHtml.Append("<td>" + EmpId + "</td>");
                                projectHtml.Append("<td>" + Name + "</td>");
                                projectHtml.Append("<td>" + RequestType + "</td>");
                                projectHtml.Append("<td>" + PunchTime + "</td>");
                                projectHtml.Append("<td>" + RequestDate + "</td>");
                                projectHtml.Append("<td>" + ManagerApprove + "</td>");
                                projectHtml.Append("<td>" + HRApprove + "</td>");
                              //  projectHtml.Append("<td>" + Status + "</td>");
                                projectHtml.Append("</tr>");
                                attendancrrequestContainer.Controls.Add(new LiteralControl(projectHtml.ToString()));
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

        //public void RequestDataInsert()
        //{
        //    try
        //    {
        //        using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
        //        {
        //            using (SqlCommand sqlcmd = new SqlCommand("SELECT * FROM AttendanceRequest WHERE EmpId = @EmpId and Status='1'", connection))
        //            {
        //                sqlcmd.Parameters.AddWithValue("@EmpId", Session["EmpId"].ToString());
        //                connection.Open();
        //                using (SqlDataReader myReader = sqlcmd.ExecuteReader())
        //                {
        //                    if (myReader.Read())
        //                    {
        //                        string ManagerApprove = myReader["ManagerApprove"].ToString();
        //                        string HRApprove = myReader["HRApprove"].ToString();
        //                        string PunchTime = myReader["PunchTime"].ToString();
        //                        string Status = myReader["Status"].ToString();
        //                        if (ManagerApprove.ToString() == "1")
        //                        {
        //                            if (HRApprove.ToString() == "1")
        //                            {
        //                                if (Status.ToString() == "1")
        //                                {
                                           
        //                                }                                        
        //                            }
        //                            else
        //                            {
        //                                Response.Write("<script>alert('Wait For HRApprove Approve...')</script>");
        //                            }
        //                        }
        //                        else
        //                        {
        //                            Response.Write("<script>alert('Wait For Manager Approve...')</script>");
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
       

        protected void btnrequestsend_Click(object sender, EventArgs e)
        {
            try
            {
                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    connection.Open();
                    int status = 0;
                    string approve = "Pending";
                    string punchTime = TextBox1.Text.Replace('T', ' ');
                   
                    string reqDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
                    using (SqlCommand sqlcmd = new SqlCommand("insert into AttendanceRequest(EmpId,Name,CompanyId,CompanyName,BranchId,BranchCode,BranchName,Department,Designation,RequestType,PunchDate,PunchTime,OverTime,RequestDate,Status,Reason,ManagerApprove,HRApprove,Year)" +
                        " values (@EmpId,@Name,@CompanyId,@CompanyName,@BranchId,@BranchCode,@BranchName,@Department,@Designation,@RequestType,@PunchDate,@PunchTime,@OverTime,@RequestDate,@Status,@Reason,@ManagerApprove,@HRApprove,@Year)", connection))
                    {
                        sqlcmd.Parameters.AddWithValue("@EmpId", Session["EmpId"].ToString());
                        sqlcmd.Parameters.AddWithValue("@Name", Session["Name"].ToString());
                        sqlcmd.Parameters.AddWithValue("@CompanyId", Session["Companyid"].ToString());
                        sqlcmd.Parameters.AddWithValue("@CompanyName", Session["CompanyName"].ToString());
                        sqlcmd.Parameters.AddWithValue("@BranchId", Session["BranchId"].ToString());
                        sqlcmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                        sqlcmd.Parameters.AddWithValue("@BranchName", Session["BranchName"].ToString());
                        sqlcmd.Parameters.AddWithValue("@Department", Session["DepartmentName"].ToString());
                        sqlcmd.Parameters.AddWithValue("@Designation", Session["Designation"].ToString());
                        sqlcmd.Parameters.AddWithValue("@RequestType", DropDownList1.SelectedItem.Text);
                        sqlcmd.Parameters.AddWithValue("@PunchDate", TextBox1.Text);

                        if (DropDownList1.SelectedItem.Text == "OverTime")
                        {
                            sqlcmd.Parameters.AddWithValue("@PunchTime", TextBox4.Text);
                            sqlcmd.Parameters.AddWithValue("@OverTime", TextBox4.Text);
                        }
                        else
                        {
                            sqlcmd.Parameters.AddWithValue("@PunchTime", punchTime);
                            sqlcmd.Parameters.AddWithValue("@OverTime", DBNull.Value);
                        }

                        sqlcmd.Parameters.AddWithValue("@RequestDate", reqDate);
                        sqlcmd.Parameters.AddWithValue("@Status", status);
                        sqlcmd.Parameters.AddWithValue("@Reason", txtreqreason.Text);
                        sqlcmd.Parameters.AddWithValue("@ManagerApprove", approve);
                        sqlcmd.Parameters.AddWithValue("@HRApprove", approve);
                        sqlcmd.Parameters.AddWithValue("@Year", DateTime.Now.Year);
                        int i = sqlcmd.ExecuteNonQuery();
                        if (i > 0)
                        {
                            Response.Write("<script>alert('Request Sent Successfully...')</script>");
                            txtreqreason.Text = "";
                            TextBox1.Text = "";
                            TextBox2.Text = "";
                            TextBox3.Text = "";
                            TextBox4.Text = "";
                            DropDownList1.ClearSelection();
                            // Response.Redirect("EmployeeAttendanceRequest");
                            RequestBindData();
                        }
                        else
                        {
                            Response.Write("<script>alert('Request Failed...')</script>");
                            RequestBindData();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DropDownList1.SelectedItem.Text == "OverTime")
            {
                TextBox1.TextMode = TextBoxMode.Date;
                RequestBindData();
            }
            else
            {
                TextBox1.TextMode = TextBoxMode.DateTimeLocal;
                RequestBindData();
            }

            HiddenField1.Value = "1";
        }

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {
            Label1.Text = "";
            TextBox2.Text = "";
            TextBox3.Text = "";
            TextBox4.Text = "";
            HiddenField1.Value = "1";
            DateTime selectedDate;
            if (DateTime.TryParse(TextBox1.Text, out selectedDate))
            {
                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    connection.Open();

                    // Check if the selected date is in AttendanceRequest table
                    using (SqlCommand checkCmd = new SqlCommand("SELECT COUNT(*) FROM AttendanceRequest WHERE EmpId = @EmpId AND PunchDate = @PunchDate", connection))
                    {
                        checkCmd.Parameters.AddWithValue("@EmpId", Session["EmpId"].ToString());
                        checkCmd.Parameters.AddWithValue("@PunchDate", TextBox1.Text);

                        int count = (int)checkCmd.ExecuteScalar();
                        if (count > 0)
                        {
                            Label1.Text = "You have already sent a request for " + TextBox1.Text + " date.";
                            TextBox1.Text = "";
                            btnrequestsend.Visible = false;
                            RequestBindData();
                            return;
                        }
                    }
                }

                // Continue with specific logic based on DropDownList1 selection
                string selectText = DropDownList1.SelectedItem.Text;
                if (selectText == "OverTime")
                {
                    btnrequestsend.Visible = true;
                    RequestBindData();
                    GetInOutTimes(selectedDate);
                }
                else
                {
                    using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                    {
                        connection.Open();
                        using (SqlCommand checkCmd = new SqlCommand("SELECT MIN(DateOfTransaction) AS MinDateOfTransaction, MAX(DateOfTransaction) AS MaxDateOfTransaction FROM Attendance WHERE UserId = @EmpId AND CAST(DateOfTransaction AS DATE) = @SelectedDate", connection))
                        {
                            checkCmd.Parameters.AddWithValue("@EmpId", Session["EmpId"].ToString());
                            checkCmd.Parameters.AddWithValue("@SelectedDate", selectedDate.Date);
                            using (var reader = checkCmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    // Get values from reader
                                    DateTime? minDateOfTransaction = reader["MinDateOfTransaction"] != DBNull.Value ? (DateTime?)reader["MinDateOfTransaction"] : null;
                                    DateTime? maxDateOfTransaction = reader["MaxDateOfTransaction"] != DBNull.Value ? (DateTime?)reader["MaxDateOfTransaction"] : null;

                                    string defaultTime = "00:00:00";

                                    if (selectText == "InTime Punch")
                                    {
                                        TextBox2.Text = selectedDate.ToString("yyyy-MM-dd HH:mm:ss");
                                        TextBox3.Text = maxDateOfTransaction.HasValue ? maxDateOfTransaction.Value.ToString("yyyy-MM-dd HH:mm:ss") : selectedDate.ToString("yyyy-MM-dd") + " " + defaultTime;

                                        // Calculate the difference
                                        TimeSpan difference = maxDateOfTransaction.HasValue ? (maxDateOfTransaction.Value - selectedDate) : TimeSpan.Zero;
                                        TextBox4.Text = difference.ToString(@"hh\:mm\:ss"); // Custom format
                                    }
                                    else
                                    {
                                        TextBox2.Text = minDateOfTransaction.HasValue ? minDateOfTransaction.Value.ToString("yyyy-MM-dd HH:mm:ss") : selectedDate.ToString("yyyy-MM-dd") + " " + defaultTime;
                                        TextBox3.Text = selectedDate.ToString("yyyy-MM-dd HH:mm:ss");

                                        // Calculate the difference
                                        TimeSpan difference = minDateOfTransaction.HasValue ? (selectedDate - minDateOfTransaction.Value) : TimeSpan.Zero;
                                        TextBox4.Text = difference.ToString(@"hh\:mm\:ss"); // Custom format
                                    }
                                }
                                else
                                {
                                    // If no data is found, set default times
                                    string defaultTime = "00:00:00";
                                    TextBox2.Text = selectedDate.ToString("yyyy-MM-dd") + " " + defaultTime;
                                    TextBox3.Text = selectedDate.ToString("yyyy-MM-dd") + " " + defaultTime;
                                    TextBox4.Text = "No data found";
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                TextBox4.Text = "Invalid selected date format";
            }
        }
        private void GetInOutTimes(DateTime selectedDate)
        {
            string UserId = Session["EmpId"].ToString();
            string connectionString = ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString;

            // Define threshold times
            TimeSpan startOfDay = new TimeSpan(09, 30, 0); // 09:30 AM
            TimeSpan endOfDay = new TimeSpan(18, 30, 0);   // 06:30 PM
            TimeSpan oneHour = new TimeSpan(1, 0, 0);      // 1 hour

            using (var connection = new SqlConnection(connectionString))
            {
                string query = @"SELECT MIN(DateOfTransaction) AS MinDateOfTransaction, MAX(DateOfTransaction) AS MaxDateOfTransaction FROM Attendance WHERE UserId = @EmpId 
        AND CAST(DateOfTransaction AS DATE) = @SelectedDate";

                using (var sqlcmd = new SqlCommand(query, connection))
                {
                    sqlcmd.Parameters.AddWithValue("@EmpId", UserId);
                    sqlcmd.Parameters.AddWithValue("@SelectedDate", selectedDate.Date);

                    connection.Open();
                    using (var reader = sqlcmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            DateTime? minDateOfTransaction = reader["MinDateOfTransaction"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(reader["MinDateOfTransaction"]) : null;
                            DateTime? maxDateOfTransaction = reader["MaxDateOfTransaction"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(reader["MaxDateOfTransaction"]) : null;

                            if (minDateOfTransaction.HasValue && maxDateOfTransaction.HasValue)
                            {
                                // Calculate In-Time overtime
                                TimeSpan minTime = minDateOfTransaction.Value.TimeOfDay;
                                TimeSpan inOvertime = minTime < startOfDay ? startOfDay - minTime : TimeSpan.Zero;

                                // Calculate Out-Time overtime
                                TimeSpan maxTime = maxDateOfTransaction.Value.TimeOfDay;
                                TimeSpan outOvertime = maxTime > endOfDay ? maxTime - endOfDay : TimeSpan.Zero;

                                // Total Overtime
                                TimeSpan totalOvertime = inOvertime + outOvertime;

                                // Check if total overtime is more than 1 hour
                                if (totalOvertime >= oneHour)
                                {
                                    // Set values in TextBoxes
                                    TextBox2.Text = minTime.ToString(@"hh\:mm"); // Min Time
                                    TextBox3.Text = maxTime.ToString(@"hh\:mm"); // Max Time
                                    TextBox4.Text = totalOvertime.ToString(@"hh\:mm"); // Total Overtime
                                    btnrequestsend.Visible = true; // Show button if overtime is 1 hour or more
                                }
                                else
                                {
                                    TextBox4.Text = totalOvertime.ToString(@"hh\:mm"); // Total Overtime
                                    Label1.Text = "Per Day Minimum over time is GreaterThan 1 Hour";
                                    TextBox1.Text = "";
                                    TextBox2.Text = minTime.ToString(@"hh\:mm"); // Min Time
                                    TextBox3.Text = maxTime.ToString(@"hh\:mm");
                                    btnrequestsend.Visible = false; // Hide button if overtime is less than 1 hour
                                }
                            }
                            else
                            {
                                // Handle the case where no records are found for the selected date
                                TextBox2.Text = "00:00";
                                TextBox3.Text = "00:00";
                                TextBox4.Text = "00:00";
                                btnrequestsend.Visible = false; // Hide button if no records found
                            }
                        }
                        else
                        {
                            TextBox2.Text = "00:00";
                            TextBox3.Text = "00:00";
                            TextBox4.Text = "00:00";
                            btnrequestsend.Visible = false; // Hide button if no records found
                        }
                    }
                }
            }
        }
    }
}