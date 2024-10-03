using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;
using System.Web.UI.WebControls;
using DocumentFormat.OpenXml.Wordprocessing;

namespace Human_Resource_Management
{
    public partial class AdminResignation : System.Web.UI.Page
    {     
        protected void Page_Load(object sender, EventArgs e)
        {        
            if (!IsPostBack)
            {
                BindResignData();
                UpdateResignStatus();
            }            
        }

        public void BindResignData()
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = @"
                    SELECT rl.*, wl.Image 
                    FROM ResignationLetters rl 
                    INNER JOIN WebLogins wl ON rl.EmpId = wl.EmpId 
                    WHERE 
                        (YEAR(rl.ResignationDate) = YEAR(GETDATE()) AND MONTH(rl.ResignationDate) = MONTH(GETDATE())) OR 
                        (YEAR(rl.ResignationDate) = YEAR(DATEADD(MONTH, -1, GETDATE())) AND MONTH(rl.ResignationDate) = MONTH(DATEADD(MONTH, -1, GETDATE()))) OR 
                        (YEAR(rl.ResignationDate) = YEAR(DATEADD(MONTH, 1, GETDATE())) AND MONTH(rl.ResignationDate) = MONTH(DATEADD(MONTH, 1, GETDATE()))) 
                    ORDER BY rl.ResignationDate DESC;";
                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            ResignData.Controls.Clear();
                            HashSet<string> processedRecords = new HashSet<string>();
                            int RowCount = 1;
                            while (reader.Read())
                            {
                                string Id = reader["EmpId"].ToString();
                                if (processedRecords.Contains(Id))
                                {
                                    continue;
                                }

                                string Name = reader["Name"].ToString();
                                string ResignStatus = reader["ResignStatus"].ToString();

                                string imageUrl = null;
                                if (reader["Image"] != DBNull.Value)
                                {
                                    byte[] imageData = (byte[])reader["Image"];
                                    string base64String = Convert.ToBase64String(imageData);
                                    imageUrl = "data:image/jpeg;base64," + base64String;
                                }
                                else
                                {
                                    // Set a default image URL if necessary
                                    // imageUrl = "path/to/default/image.jpg";
                                }

                                string Department = reader["Department"].ToString();

                                string NoticeDate = Convert.ToDateTime(reader["NoticeDate"]).ToString("yyyy-MM-dd");
                                string ResignDate = Convert.ToDateTime(reader["ResignationDate"]).ToString("yyyy-MM-dd");
                                string Reason = reader["Reason"].ToString();
                                string ReasonStatus = reader["ResignStatus"].ToString();
                                string ApprovedeBy = reader["ApprovedeBy"].ToString();
                                string ApproveDate = reader["ApproveDate"].ToString();

                                StringBuilder projectHtml = new StringBuilder();
                                projectHtml.Append("<tr>");
                                projectHtml.Append("<td><a href='profile.html' class='avatar'><img src='" + imageUrl + "' alt='User Image'></a>");
                                projectHtml.Append("<a href='profile.html'>" + System.Web.HttpUtility.HtmlEncode(Name) + " </a></td>");
                                projectHtml.Append("<td>" + System.Web.HttpUtility.HtmlEncode(Id) + "</td>");
                                projectHtml.Append("<td style='color:blue;'>" + System.Web.HttpUtility.HtmlEncode(Department) + "</td>");
                                projectHtml.Append("<td>" + System.Web.HttpUtility.HtmlEncode(Reason) + "</td>");
                                projectHtml.Append("<td>" + System.Web.HttpUtility.HtmlEncode(NoticeDate) + "</td>");
                                projectHtml.Append("<td>" + System.Web.HttpUtility.HtmlEncode(ResignDate) + "</td>");
                                projectHtml.Append("<td>" + System.Web.HttpUtility.HtmlEncode(ReasonStatus) + "</td>");

                                if (ResignStatus != "Rejected" && ResignStatus != "Approved")
                                {
                                    projectHtml.Append("<td><a class='dropdown-item' href='#' data-bs-toggle='modal' data-bs-target='#edit_resignation' onclick=\"editResign('" + System.Web.HttpUtility.HtmlEncode(Name) + "','" + System.Web.HttpUtility.HtmlEncode(Id) + "','" + System.Web.HttpUtility.HtmlEncode(ResignDate) + "','" + System.Web.HttpUtility.HtmlEncode(Reason) + "','" + System.Web.HttpUtility.HtmlEncode(ApprovedeBy) + "','" + System.Web.HttpUtility.HtmlEncode(ApproveDate) + "')\"><i class='fa-solid fa-pencil m-r-5' style='color:#057a28;'></i></a></td>");
                                }

                                projectHtml.Append("</tr>");
                                ResignData.Controls.Add(new LiteralControl(projectHtml.ToString()));

                                processedRecords.Add(Id); // Mark the record as processed
                                RowCount++;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception and provide user-friendly error handling
                // For example, log the error to a file, event log, or database
                // Show a user-friendly message to the user
                Response.Write("An error occurred: " + ex.Message);
            }
        }



        public void UpdateResignStatus()
        {
            DateTime currentDate = DateTime.Now;
            string formattedDate = currentDate.ToString("yyyy-MM-dd");

            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Select resignations for the current and previous month
                    string query = @"SELECT * FROM ResignationLetters 
                        WHERE (YEAR(ResignationDate) = YEAR(GETDATE()) AND MONTH(ResignationDate) = MONTH(GETDATE()))
                           OR (YEAR(ResignationDate) = YEAR(DATEADD(MONTH, -1, GETDATE())) AND MONTH(ResignationDate) = MONTH(DATEADD(MONTH, -1, GETDATE())))
                           OR (YEAR(ResignationDate) = YEAR(DATEADD(MONTH, 1, GETDATE())) AND MONTH(ResignationDate) = MONTH(DATEADD(MONTH, 1, GETDATE())))
                        ORDER BY ResignationDate DESC;";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            // Create a list to store EmpId values
                            List<string> empIdsToUpdate = new List<string>();

                            while (reader.Read())
                            {
                                string empId = reader["EmpId"].ToString();
                                DateTime resignDate = Convert.ToDateTime(reader["ResignationDate"]);
                                string resignDateString = resignDate.ToString("yyyy-MM-dd");

                                if (resignDateString == formattedDate)
                                {
                                    empIdsToUpdate.Add(empId);
                                }
                            }

                            // Close the SqlDataReader before executing updates
                            reader.Close();

                            // Update Employees and WebLogins tables
                            foreach (string empId in empIdsToUpdate)
                            {
                                SqlCommand empCmd = new SqlCommand("UPDATE Employees SET Status=@Status WHERE EmpId=@EmpId", connection);
                                empCmd.Parameters.AddWithValue("@Status", "0");
                                empCmd.Parameters.AddWithValue("@EmpId", empId);
                                empCmd.ExecuteNonQuery();

                                SqlCommand empCmd2 = new SqlCommand("UPDATE WebLogins SET Status=@Status WHERE EmpId=@EmpId", connection);
                                empCmd2.Parameters.AddWithValue("@Status", "0");
                                empCmd2.Parameters.AddWithValue("@EmpId", empId);
                                empCmd2.ExecuteNonQuery();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                // Log error or show appropriate message
                throw ex;
            }


        }

        protected void btnApprove_Click(object sender, EventArgs e)
        {

            string ResignStatus = "Approved";
            string ApprovedeStatus = "Approved";
            DateTime currentDate = DateTime.Now;
            string formattedDate = currentDate.ToString("yyyy-MM-dd");
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Update ResignationLetters table
                    SqlCommand resignCmd = new SqlCommand("UPDATE ResignationLetters SET ResignStatus=@ResignStatus, ApprovedeStatus=@ApprovedeStatus, ApprovedeBy=@ApprovedeBy, ApproveDate=@ApproveDate, Status='1', ApprovedeReason=@ApprovedeReason WHERE EmpId=@EmpId", connection);
                    resignCmd.Parameters.AddWithValue("@ResignStatus", ResignStatus);
                    resignCmd.Parameters.AddWithValue("@ApprovedeStatus", ApprovedeStatus);
                    resignCmd.Parameters.AddWithValue("@ApprovedeBy", Session["Name"].ToString());
                    resignCmd.Parameters.AddWithValue("@ApproveDate", currentDate);
                    resignCmd.Parameters.AddWithValue("@ApprovedeReason", hrreason.Text);
                    resignCmd.Parameters.AddWithValue("@EmpId", HiddenField2.Value);
                    int resignUpdateCount = resignCmd.ExecuteNonQuery();

                    // Update Employees table
                    SqlCommand empCmd = new SqlCommand("UPDATE Employees SET EmpInActiveDate=@EmpInActiveDate WHERE EmpId=@EmpId", connection);
                    empCmd.Parameters.AddWithValue("@EmpInActiveDate", txtedresdate.Text);
                    empCmd.Parameters.AddWithValue("@EmpId", HiddenField2.Value);
                    int empUpdateCount = empCmd.ExecuteNonQuery();

                    if (resignUpdateCount > 0 && empUpdateCount > 0)
                    {
                        Response.Write("<script>alert('Approved Successfully...')</script>");
                        BindResignData();
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
        protected void btnReject_Click(object sender, EventArgs e)
        {
            string ResignStatus = "Rejected";
            string ApprovedeStatus = "Rejected";
            DateTime currentDate = DateTime.Now;
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("UPDATE ResignationLetters SET ResignStatus=@ResignStatus,ApprovedeStatus=@ApprovedeStatus,ApprovedeReason=@ApprovedeReason,ApprovedeBy=@ApprovedeBy,ApproveDate=@ApproveDate,Status='0' WHERE EmpId=@EmpId", connection);
                    cmd.Parameters.AddWithValue("@ResignStatus", ResignStatus);
                    cmd.Parameters.AddWithValue("@ApprovedeStatus", ApprovedeStatus);
                    cmd.Parameters.AddWithValue("@ApprovedeReason", hrreason.Text);
                    cmd.Parameters.AddWithValue("@ApprovedeBy", Session["Name"].ToString());
                    cmd.Parameters.AddWithValue("@ApproveDate", currentDate);
                    cmd.Parameters.AddWithValue("@EmpId", HiddenField2.Value);
                    int i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        Response.Write("<script>alert(' Rejected Successfully...')</script>");
                        BindResignData();
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

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    string connectionString = ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString;

            //    using (SqlConnection connection = new SqlConnection(connectionString))
            //    {
            //        connection.Open();
            //        SqlCommand cmd = new SqlCommand("UPDATE ResignationLetters SET Status='0' WHERE EmpId=@EmpId", connection);
            //        cmd.Parameters.AddWithValue("@EmpId", txtedid.Text);
            //        int i = cmd.ExecuteNonQuery();
            //        if (i > 0)
            //        {
            //            Response.Write("<script>alert('Success...')</script>");
            //            BindResignData();
            //        }
            //        else
            //        {
            //            Response.Write("<script>alert('Failed...')</script>");
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
        }

       
    }
}