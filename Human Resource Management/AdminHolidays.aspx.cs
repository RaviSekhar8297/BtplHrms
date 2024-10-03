using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Configuration;
using Org.BouncyCastle.Ocsp;
using System.Diagnostics;

namespace Human_Resource_Management
{
    public partial class AdminHolidays : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int currentYear = DateTime.Now.Year;
            Label1.Text = currentYear.ToString();
            if (!IsPostBack)
            {
                HolidaysList();
            }
        }
        public void HolidaysList()
        {
            int currentYear = DateTime.Now.Year;

            if (Session["EmpId"] == null || Session["EmpId"].ToString() == "")
            {

            }
            else
            {
                try
                {
                    using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                    {
                        using (SqlCommand sqlcmd = new SqlCommand("select * from HolidaysTable where YEAR(HolidayDate)=@Year and Status='1'", connection))
                        {
                            sqlcmd.Parameters.AddWithValue("@Year", currentYear);
                            connection.Open();
                            SqlDataAdapter da1 = new SqlDataAdapter(sqlcmd);
                            DataSet ds1 = new DataSet();
                            da1.Fill(ds1);
                            DateTime today = DateTime.Today;
                            if (ds1.Tables[0].Rows.Count > 0)
                            {
                                foreach (DataRow row in ds1.Tables[0].Rows)
                                {
                                    string Hyid = row["HolidayId"].ToString();
                                    string HyName = row["HolidayName"].ToString();

                                    string Date = row["HolidayDate"].ToString();
                                    DateTime holidayDateTime = Convert.ToDateTime(Date);
                                    string HyDate = holidayDateTime.ToString("yyyy-MM-dd");
                                    string day = holidayDateTime.ToString("dddd");

                                    //string HyDay = row["HolidayType"].ToString();
                                    string HyType = row["HolidayType"].ToString();

                                    string desc = row["Description"].ToString();

                                    StringBuilder projectHtml = new StringBuilder();

                                    projectHtml.Append("<tr>");
                                    projectHtml.Append("<td>" + Hyid + "</td>");
                                    projectHtml.Append("<td>" + HyName + "</td>");
                                    projectHtml.Append("<td>" + HyDate + "</td>");
                                    projectHtml.Append("<td>" + day + "</td>");
                                    projectHtml.Append("<td>" + HyType + "</td>");
                                    projectHtml.Append("<td>" + desc + "</td>");
                                    // projectHtml.Append("<td><a href='#' data-bs-toggle='modal' data-bs-target='#editt_holiday'><i class='fa-solid fa-pencil'></i></a></td>");  delete_holiday           
                                    // projectHtml.Append("<td><a href='#' data-bs-toggle='modal' data-bs-target='#deletee_holiday' class='delete-link' data-hyid='" + Hyid + "'><i class='fa-solid fa-trash'></i></a></td>");                                      
                                    //projectHtml.Append("</tr>");
                                    projectHtml.Append("<td><a href='#' data-bs-toggle='modal' data-bs-target='#editt_holiday' onclick=\"editHoliday('" + Hyid + "','" + HyName + "','" + HyDate + "','" + HyType + "','" + desc + "')\"><i class='fa-solid fa-pencil' style='color:#057a28;'></i></a></td>");
                                    //projectHtml.Append("<td><a href='#' data-bs-toggle='modal' data-bs-target='#delete_holiday' onclick=\"deletetHolidayid('" + Hyid + "')\"><i class='fa-solid fa-trash' style='color:#ab0c19;'></i></a></td>");
                                    projectHtml.Append("<td><a class='delete' href='#' data-bs-toggle='modal' data-bs-target='#delete_holiday'  onclick =\"deleteemp('" + Hyid + "','" + HyName + "')\"><i class='fa-regular fa-trash-can m-r-5'></i></a></td>");
                                    projectHtml.Append("</tr>");


                                    HolidayContainer.Controls.Add(new LiteralControl(projectHtml.ToString()));

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
        }

        protected void btnupdateholiday_Click(object sender, EventArgs e)
        {

            try
            {
                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("update HolidaysTable set HolidayName='" + TextBox1.Text + "',HolidayDate='" + TextBox2.Text + "',HolidayType='" + TextBox4.Text + "',Description='" + TextBox5.Text + "' where HolidayId = '" + TextBox3.Text + "' ", connection);
                    connection.Open();
                    int i = cmd.ExecuteNonQuery();
                    connection.Close();
                    if (i > 0)
                    {
                        Response.Write("<script>alert('Holiday "+ TextBox1.Text + "Updated Successfully..')</script>");
                        
                        HolidaysList();
                    }
                    else
                    {
                        Response.Write("<script>alert('Failed..')</script>");
                        HolidaysList();
                    }
                   
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void holidataddbtn_Click(object sender, EventArgs e)
        {
            string currentDateStr = DateTime.Now.ToString("yyyy-MM-dd");
            string dayOfWeek = DateTime.Parse(txtHD.Text).ToString("dddd");
            string monthofholiday = DateTime.Parse(txtHD.Text).ToString("MMMM");
            int year = DateTime.Now.Year;
            int nodays = 1;
            DateTime currentDateTime = DateTime.Now;
            string formattedDateTime = currentDateTime.ToString("yyyy-MM-dd HH:mm:ss");
            try
            {
                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand("insert into  HolidaysTable(HolidayName,HolidayType,HolidayDate,Week,Month,Year,NoOfdays,Description,Status,CompanyId,CompanyName,BranchId,BranchName,BranchCode,CreatedBy,CreatedDate,UpdatedBy,UpdatedDate,DeletedBy,DeletedDate)" +
                        " values(@HolidayName,@HolidayType,@HolidayDate,@Week,@Month,@Year,@NoOfdays,@Description,@Status,@CompanyId,@CompanyName,@BranchId,@BranchName,@BranchCode,@CreatedBy,@CreatedDate,@UpdatedBy,@UpdatedDate,@DeletedBy,@DeletedDate)", connection))
                    {
                        cmd.Parameters.AddWithValue("@HolidayName", txtHN.Text);
                        cmd.Parameters.AddWithValue("@HolidayType", ddlHT.SelectedItem.Text);
                        cmd.Parameters.AddWithValue("@HolidayDate", txtHD.Text);
                        cmd.Parameters.AddWithValue("@Week", dayOfWeek);
                        cmd.Parameters.AddWithValue("@Month", monthofholiday);
                        cmd.Parameters.AddWithValue("@Year", year);
                        cmd.Parameters.AddWithValue("@NoOfdays", nodays);
                        cmd.Parameters.AddWithValue("@Description", txtHDes.Text);
                        cmd.Parameters.AddWithValue("@Status", nodays);
                        cmd.Parameters.AddWithValue("@CompanyId", Session["Companyid"].ToString());
                        cmd.Parameters.AddWithValue("@CompanyName", Session["CompanyName"].ToString());
                        cmd.Parameters.AddWithValue("@BranchId", Session["BranchId"].ToString());
                        cmd.Parameters.AddWithValue("@BranchName", Session["BranchName"].ToString());
                        cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                        cmd.Parameters.AddWithValue("@CreatedBy", Session["Name"].ToString());
                        cmd.Parameters.AddWithValue("@CreatedDate", currentDateTime);
                        cmd.Parameters.AddWithValue("@UpdatedBy", "NULL");
                        cmd.Parameters.AddWithValue("@UpdatedDate", DBNull.Value);
                        cmd.Parameters.AddWithValue("@DeletedBy", "NULL");
                        cmd.Parameters.AddWithValue("@DeletedDate", DBNull.Value);

                        int i = cmd.ExecuteNonQuery();
                        if (i > 0)
                        {
                            if (ddlHT.SelectedItem.Text == "Sudden Holiday")
                            {
                                string selectedDateText = txtHD.Text;
                                DateTime selectedDate;
                                if (DateTime.TryParse(selectedDateText, out selectedDate))
                                {
                                    AutoInsertHolidayRecords(selectedDate);
                                }
                                else
                                {

                                }
                            }
                            Response.Write("<script>alert('"+ txtHN.Text + " Holiday Added Successfully..')</script>");
                            txtHN.Text = "";
                            txtHD.Text = "";
                            txtHW.Text = "";
                            ddlHT.ClearSelection();
                            txtHDes.Text = "";
                            HolidaysList();
                        }
                        else
                        {
                            Response.Write("<script>alert('Failed..')</script>");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btndeleteholiday_Click(object sender, EventArgs e)
        {
            try
            {
                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("update HolidaysTable set Status='0' where HolidayId = '" + HiddenField1.Value + "' ", connection);
                    connection.Open();
                    int i = cmd.ExecuteNonQuery();
                    connection.Close();
                    HolidaysList();
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        private void AutoInsertHolidayRecords(DateTime selectedDate)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString;
            List<long> presentUserIds = new List<long>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Retrieve UserIds of users who have records on the selected date
                string query = @"
            SELECT DISTINCT UserId
            FROM Attendance
            WHERE CAST(DateOfTransaction AS DATE) = @SelectedDate";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@SelectedDate", selectedDate.Date);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            long userId = reader.GetFieldValue<long>(reader.GetOrdinal("UserId"));
                            presentUserIds.Add(userId);
                        }
                    }
                }
            }

            // Insert records for these users into the Attendance table
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string insertQuery = @"
            INSERT INTO Attendance (UserId, DateOfTransaction, OutDoor)
            VALUES (@UserId, @DateOfTransaction, @OutDoor)";

                using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
                {
                    cmd.Parameters.Add("@UserId", SqlDbType.BigInt);
                    cmd.Parameters.Add("@DateOfTransaction", SqlDbType.DateTime);
                    cmd.Parameters.Add("@OutDoor", SqlDbType.NVarChar);
                    cmd.Parameters.Add("@OrgId", SqlDbType.Int); // Assuming OrgId is an integer
                    cmd.Parameters.Add("@MachineId", SqlDbType.Int);
                    cmd.Parameters["@OrgId"].Value = 9999; // Set OrgId value
                    cmd.Parameters["@MachineId"].Value = 1; 

                    foreach (long userId in presentUserIds)
                    {
                        cmd.Parameters["@UserId"].Value = userId;
                        // Add specific time to the selected date
                        DateTime dateTimeWithTime = new DateTime(selectedDate.Year, selectedDate.Month, selectedDate.Day, 6, 30, 0);
                        cmd.Parameters["@DateOfTransaction"].Value = dateTimeWithTime;
                        cmd.Parameters["@OutDoor"].Value = "Sudden Holiday";
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}