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
using DocumentFormat.OpenXml.Bibliography;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TreeView;

namespace Human_Resource_Management
{
    public partial class ManagerTimeOffs : System.Web.UI.Page
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

                    if (remainingTimeOff == 0)
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
                    using (SqlCommand sqlcmd = new SqlCommand("SELECT * FROM TimeOff WHERE Department = @Department and EmpId !=@EmpId", connection))
                    {
                        sqlcmd.Parameters.AddWithValue("@Department", Session["DepartmentName"].ToString());
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
                                string Id = row["Id"].ToString();
                                string EmpId = row["EmpId"].ToString();
                                DateTime fromDateTime = Convert.ToDateTime(row["FromDateTime"]);
                                DateTime toDateTime = Convert.ToDateTime(row["ToDateTime"]);
                                string Reason = row["Reason"].ToString();
                                string ApprovedStatus = row["ApprovedStatus"].ToString();
                                string ApprovedBy = row["ApprovedBy"].ToString();

                                // Extract time only
                                string fromTime = fromDateTime.ToString("HH:mm");
                                string toTime = toDateTime.ToString("HH:mm");

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
                                if(ApprovedStatus.Trim()=="Approved" || ApprovedStatus.Trim() == "Rejected")
                                {

                                }
                                else
                                {
                                    projectHtml.Append("<td><a class='edit' href='#' data-bs-toggle='modal' data-bs-target='#edit_timeoff' onclick =\"editTimeOff('" + Id + "','" + EmpId + "','" + fromDateTime + "','" + toDateTime + "','" + Name + "','" + Reason + "')\" ><i class='fa-solid fa-pencil m-r-5' style='color:green;'></i></a></td>");
                                }
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
                // Handle or log the exception
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

                    string query = @"SELECT ISNULL(SUM(UsedTimeOff), 0) FROM TimeOff WHERE YEAR(FromDateTime) = @Year AND MONTH(FromDateTime) = @Month and EmpId=@EmpId and Status='1'";

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
                    return;
                }
                var diffMinutes = (date2 - date1).TotalMinutes;
                // Calculate hours and minutes
                int hours = (int)(diffMinutes / 60);
                int minutes = (int)(diffMinutes % 60);
                if (diffMinutes > 120)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('The difference between TextBox2 and TextBox1 should not be more than 2 hours.');", true);
                    return;
                }

                int usedTimeOff = (int)diffMinutes;

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

        protected void btnapprove_Click(object sender, EventArgs e)
        {
            string ApprovedStatus = "Approved";
            string Approvedby = Session["Name"].ToString();
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("UPDATE TimeOff SET Status='1' ,ApprovedStatus=@ApprovedStatus,ApprovedBy=@ApprovedBy where Id=@Id and EmpId=@EmpId", connection);
                    connection.Open();
                    cmd.Parameters.AddWithValue("@ApprovedStatus", ApprovedStatus);
                    cmd.Parameters.AddWithValue("@ApprovedBy", Approvedby);
                    cmd.Parameters.AddWithValue("@Id", HiddenField1.Value);
                    cmd.Parameters.AddWithValue("@EmpId", HiddenField2.Value);
                    int i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        Response.Write("<script>alert('Approved Successfully...')</script>");
                    }
                    else
                    {
                        Response.Write("<script>alert('Failed...')</script>");
                    }
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        protected void btnreject_Click(object sender, EventArgs e)
        {
            string RejectedStatus = "Rejected";
            string Rejectedby = Session["Name"].ToString();
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("UPDATE TimeOff SET Status='0' ,ApprovedStatus=@ApprovedStatus,ApprovedBy=@ApprovedBy where Id=@Id and EmpId=@EmpId", connection);
                    connection.Open();
                    cmd.Parameters.AddWithValue("@ApprovedStatus", RejectedStatus);
                    cmd.Parameters.AddWithValue("@ApprovedBy", Rejectedby);
                    cmd.Parameters.AddWithValue("@Id", HiddenField1.Value);
                    cmd.Parameters.AddWithValue("@EmpId", HiddenField2.Value);
                    int i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        Response.Write("<script>alert('Rejected ...')</script>");
                    }
                    else
                    {
                        Response.Write("<script>alert('Failed...')</script>");
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