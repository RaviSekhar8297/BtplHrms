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

namespace Human_Resource_Management
{
    public partial class EmployeeHolidays : System.Web.UI.Page
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
                                    string did = row["HolidayId"].ToString();
                                    string HyName = row["HolidayName"].ToString();

                                    string Date  = row["HolidayDate"].ToString();
                                    DateTime holidayDateTime = Convert.ToDateTime(Date);
                                    string HyDate  = holidayDateTime.ToString("yyyy-MM-dd");
                                    string day = holidayDateTime.ToString("dddd");

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

                                    var editholiday = Session["EditHolidayStatus"] as String;

                                    if (editholiday == "True")
                                    {
                                       projectHtml.Append("<td><a href='#' data-bs-toggle='modal' data-bs-target='#editt_holiday' onclick=\"editHoliday('" + Hyid + "','" + HyName + "','" + HyDate + "','" + HyType + "','" + desc + "')\"><i class='fa-solid fa-pencil' style='color:#057a28;'></i></a></td>");
                                         
                                    }
                                    if(Session["DeleteHolidayStatus"].ToString() == "True")
                                    {
                                        // projectHtml.Append("<td><a href='#' class='delete-link' data-bs-target='#delete_holiday data-hyid='" + Hyid + "'><i class='fa-solid fa-trash' style='color:#ab0c19;'></i></a></td>");
                                        // projectHtml.Append("<td><a class='dropdown - item' href='#' data-bs-toggle='modal' data-bs-target='#delete_approve'><i class='fa-regular fa-trash-can m-r-5'></i></a></td>");
                                        //  projectHtml.Append("<td><a class='dropdown - item' href='#' data-bs-toggle='modal' data-bs-target='#delete_holiday' onclick=\"deletetid('"+ did + "')><i class='fa-regular fa-trash-can m-r-5' style='color:#057a28;></i></a></td>");
                                        projectHtml.Append("<td><a href='#' data-bs-toggle='modal' data-bs-target='#delete_holiday' onclick=\"deleteidHoliday('" + Hyid + "')\"><i class='fa-regular fa-trash-can m-r-5'></i></a></td>");
                                       
                                    }
                                    projectHtml.Append("</tr>");

                                    HolidayContainer.Controls.Add(new LiteralControl(projectHtml.ToString()));                                   
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
                        Response.Write("<script>alert('Holiday Updated..')</script>");
                    }
                    else
                    {
                        Response.Write("<script>alert('Failed..')</script>");                      
                    }
                    Response.Redirect("EmployeeHolidays.aspx");
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
                            Response.Write("<script>alert('Holiday Added Successfully..')</script>");
                            HolidaysList();
                        }
                        else
                        {
                            Response.Write("<script>alert('Failed..')</script>");
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        protected void deleteholidaybtn_Click(object sender, EventArgs e)
        {
            try
            {
                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("update HolidaysTable set Status='0' where HolidayId = '" + HiddenField1.Value + "' ", connection);
                    connection.Open();
                    int i = cmd.ExecuteNonQuery();
                    connection.Close();
                    if (i > 0)
                    {
                        Response.Write("<script>alert('Holiday Deleted Successufully..')</script>");
                        HolidaysList();
                    }
                    else
                    {
                        Response.Write("<script>alert('Failed..')</script>");
                    }
                    Response.Redirect("EmployeeHolidays.aspx");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}