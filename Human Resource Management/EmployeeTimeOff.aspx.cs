using DocumentFormat.OpenXml.Bibliography;
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
    public partial class EmployeeTimeOff : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                TextBox1.Attributes["type"] = "datetime-local";
                TextBox2.Attributes["type"] = "datetime-local";
                DisplayRemainingTimeOff();
                BindData();
            }
        }
        private void DisplayRemainingTimeOff()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                int currentYear = DateTime.Now.Year;
                int currentMonth = DateTime.Now.Month;

                string query = @"SELECT ISNULL(SUM(UsedTimeOff), 0) FROM TimeOff WHERE YEAR(FromDateTime) = @Year AND MONTH(FromDateTime) = @Month and EmpId=@EmpId and status='1'";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Year", currentYear);
                    cmd.Parameters.AddWithValue("@Month", currentMonth);
                    cmd.Parameters.AddWithValue("@EmpId", Session["EmpId"].ToString());

                    object result = cmd.ExecuteScalar();
                    int totalUsedTimeOff = result != DBNull.Value ? Convert.ToInt32(result) : 0;
                    int initialTimeOff = 120;

                    int remainingTimeOff = initialTimeOff - totalUsedTimeOff;
                    string text = remainingTimeOff >= 0 ? remainingTimeOff.ToString() : "0";
                   
                    if(remainingTimeOff == 0)
                    {
                        Label1.Text = "This Month Your are used TimeOff";
                        BtnApplyTimeOff.Visible = false;
                    }
                    else
                    {
                        Label1.Text = "This Month Your TimeOff : " + text + " Minutes Remaining";
                        BtnApplyTimeOff.Visible = true;
                    }
                }
            }
        }

        private void BindData()
        {
            try
            {
                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    using (SqlCommand sqlcmd = new SqlCommand("SELECT * FROM TimeOff WHERE EmpId = @EmpId", connection))
                    {
                        sqlcmd.Parameters.AddWithValue("@EmpId", Session["EmpId"].ToString());
                        connection.Open();
                        SqlDataAdapter da1 = new SqlDataAdapter(sqlcmd);
                        DataSet ds1 = new DataSet();
                        da1.Fill(ds1);

                        if (ds1.Tables[0].Rows.Count > 0)
                        {
                            int S_No = 1;
                            foreach (DataRow row in ds1.Tables[0].Rows)
                            {
                                string Name = row["Name"].ToString();
                                DateTime fromDateTime = Convert.ToDateTime(row["FromDateTime"]);
                                DateTime toDateTime = Convert.ToDateTime(row["ToDateTime"]);
                                string Reason = row["Reason"].ToString();
                                string ApprovedStatus = row["ApprovedStatus"].ToString();
                                string ApprovedBy = row["ApprovedBy"].ToString();

                                // Extract time only
                                string fromTime = fromDateTime.ToString("yyyy-MMM-dd HH:mm");
                                string toTime = toDateTime.ToString("yyyy-MMM-dd HH:mm");

                                // Calculate time difference
                                TimeSpan timeDiff = toDateTime - fromDateTime;
                                string formattedTimeDiff = $"{timeDiff.Hours:D2}:{timeDiff.Minutes:D2}";

                                StringBuilder projectHtml = new StringBuilder();
                                projectHtml.Append("<tr>");
                                projectHtml.Append("<td>" + S_No + "</td>");
                                projectHtml.Append("<td>" + Name + "</td>");
                                projectHtml.Append("<td>" + fromTime + "</td>"); 
                                projectHtml.Append("<td>" + toTime + "</td>");
                                projectHtml.Append("<td>" + formattedTimeDiff + "</td>");
                                projectHtml.Append("<td>" + Reason + "</td>");
                                projectHtml.Append("<td>" + ApprovedStatus + "</td>");
                                projectHtml.Append("<td>" + ApprovedBy + "</td>");
                                 // Displaying the formatted time difference
                                projectHtml.Append("</tr>");

                                TimeOffData.Controls.Add(new LiteralControl(projectHtml.ToString()));
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


        protected void TextBox2_TextChanged(object sender, EventArgs e)
        {
            BtnApplyTimeOff.Visible = true;
            DateTime date1, date2;
            if (DateTime.TryParse(TextBox1.Text, out date1) && DateTime.TryParse(TextBox2.Text, out date2))
            {
                if (date1 >= date2)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('TextBox1 should be less than TextBox2.');", true);
                    TextBox1.Text = "";
                    TextBox2.Text = "";
                    BtnApplyTimeOff.Visible = false;
                    return;
                }

                var diff = (date2 - date1).TotalMinutes;
                if (diff > 120)
                {
                    //ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('The difference  should not be more than 2 hours.');", true);
                    Label1.Text = "The difference  should not be more than 2 hours.";
                    TextBox2.Text = "";
                    BtnApplyTimeOff.Visible = false;
                    return;
                }

                string connectionString = ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    int currentYear = DateTime.Now.Year;
                    int currentMonth = DateTime.Now.Month;

                    string query = @"SELECT ISNULL(SUM(UsedTimeOff), 0) FROM TimeOff WHERE YEAR(FromDateTime) = @Year AND MONTH(FromDateTime) = @Month and EmpId=@EmpId";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@Year", currentYear);
                        cmd.Parameters.AddWithValue("@Month", currentMonth);
                        cmd.Parameters.AddWithValue("@EmpId", Session["EmpId"].ToString());

                        object result = cmd.ExecuteScalar();
                        int totalUsedTimeOff = result != DBNull.Value ? Convert.ToInt32(result) : 0;
                        int initialTimeOff = 120;

                        int remainingTimeOff = initialTimeOff - totalUsedTimeOff;
                        string text = remainingTimeOff >= 0 ? remainingTimeOff.ToString() : "0";

                        if (diff > remainingTimeOff)
                        {
                            Label1.Text = "You have only  : " + remainingTimeOff + "";
                            BtnApplyTimeOff.Visible = false;
                        }
                        else
                        {
                            Label1.Text = "You Have Only   : " + text + " TimeOff";
                        }
                    }
                }
            }
            else
            {
                Response.Write("Invalid Date and Time format.");
            }
        }

        protected void BtnApplyTimeOff_Click(object sender, EventArgs e)
        {
            string txt1Value = TextBox1.Text.Replace('T', ' ');
            string txt2Value = TextBox2.Text.Replace('T', ' ');

            DateTime date1, date2;
            if (DateTime.TryParse(txt1Value, out date1) && DateTime.TryParse(txt2Value, out date2))
            {
                if (date1 >= date2)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('TextBox1 should be less than TextBox2.');", true);
                    TextBox2.Text = "";
                    TextBox3.Text = "";
                    BindData();
                    return;
                }

                var diffMinutes = (date2 - date1).TotalMinutes;
                // Calculate hours and minutes
                int hours = (int)(diffMinutes / 60);
                int minutes = (int)(diffMinutes % 60);
                if (diffMinutes > 120)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('You have only 2 hours of time .');", true);
                    TextBox2.Text = "";
                    TextBox3.Text = "";
                    BindData();
                    return;
                }

                int usedTimeOff = (int)diffMinutes;

                // Check for time overlap
                if (CheckTimeOverlap(date1, date2))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('You have already taken time off during this period.');", true);
                    TextBox2.Text = "";
                    TextBox3.Text = "";
                    BindData();
                    return;
                }

                string connectionString = ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    int YearlyTime = 24;
                    int monthlyTime = 2;
                    int status = 0;
                    int year = DateTime.Now.Year;
                    int month = DateTime.Now.Month;
                    int TimeValues = (12 - month) - 1;
                    int blsTimeOff = TimeValues * 2;
                    string ApprStatus = "Pending";
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO TimeOff (EmpId, Name, Company, Department, Designation, YearlyTime, MonthlyTime, TotalTimeOff, UsedTimeOff, BalenceTimeOff, FromDateTime, ToDateTime, Reason, Status, Yearof, ApprovedStatus, ApprovedBy) VALUES (@EmpId, @Name, @Company, @Department, @Designation, @YearlyTime, @MonthlyTime, @TotalTimeOff, @UsedTimeOff, @BalenceTimeOff, @FromDateTime, @ToDateTime, @Reason, @Status, @Yearof, @ApprovedStatus, @ApprovedBy)", connection))
                    {
                        cmd.Parameters.AddWithValue("@EmpId", Session["EmpId"].ToString());
                        cmd.Parameters.AddWithValue("@Name", Session["Name"].ToString());
                        cmd.Parameters.AddWithValue("@Company", Session["CompanyName"].ToString());
                        cmd.Parameters.AddWithValue("@Department", Session["DepartmentName"].ToString());
                        cmd.Parameters.AddWithValue("@Designation", Session["Designation"].ToString());
                        cmd.Parameters.AddWithValue("@YearlyTime", YearlyTime);
                        cmd.Parameters.AddWithValue("@MonthlyTime", monthlyTime);
                        cmd.Parameters.AddWithValue("@TotalTimeOff", YearlyTime);
                        cmd.Parameters.AddWithValue("@UsedTimeOff", usedTimeOff);
                        cmd.Parameters.AddWithValue("@BalenceTimeOff", blsTimeOff);
                        cmd.Parameters.AddWithValue("@FromDateTime", date1);
                        cmd.Parameters.AddWithValue("@ToDateTime", date2);
                        cmd.Parameters.AddWithValue("@Reason", TextBox3.Text);
                        cmd.Parameters.AddWithValue("@Status", status);
                        cmd.Parameters.AddWithValue("@Yearof", year);
                        cmd.Parameters.AddWithValue("@ApprovedStatus", ApprStatus);
                        cmd.Parameters.AddWithValue("@ApprovedBy", ApprStatus);
                        cmd.ExecuteNonQuery();
                        DisplayRemainingTimeOff();
                        BindData();
                        TextBox1.Text = "";
                        TextBox2.Text = "";
                        TextBox3.Text = "";
                    }
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Invalid Date and Time format.');", true);
            }
        }
        private bool CheckTimeOverlap(DateTime fromDateTime, DateTime toDateTime)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT COUNT(*) FROM TimeOff WHERE (@fromDateTime < ToDateTime AND @toDateTime > FromDateTime)";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@fromDateTime", fromDateTime);
                    cmd.Parameters.AddWithValue("@toDateTime", toDateTime);
                    int count = (int)cmd.ExecuteScalar();
                    return count > 0; // Return true if there is an overlap
                }
            }
        }
    }
}