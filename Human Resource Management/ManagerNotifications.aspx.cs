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
using DocumentFormat.OpenXml.ExtendedProperties;

namespace Human_Resource_Management
{
    public partial class ManagerNotifications : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindNotifications();
                UpdateNotification();
            }
        }
        public void BindNotifications()
        {
            try
            {
                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    using (SqlCommand sqlcmd = new SqlCommand(" SELECT n.*, e.Image FROM Notifications n INNER JOIN Employees e ON n.EmpId = e.EmpId  WHERE n.Department = @Department", connection))
                    {
                        sqlcmd.Parameters.AddWithValue("@Department", Session["DepartmentName"].ToString());
                        connection.Open();
                        SqlDataAdapter da1 = new SqlDataAdapter(sqlcmd);
                        DataTable dt = new DataTable();
                        da1.Fill(dt);

                        if (dt.Rows.Count > 0)
                        {
                            int S_No = 1;
                            foreach (DataRow row in dt.Rows)
                            {
                                string SendTo = row["Employees"].ToString();
                                string EmpId = row["EmpId"].ToString();
                                string Department = row["Department"].ToString();
                                string Subject = row["Subject"].ToString();
                                string Message = row["Message"].ToString();
                                string SendBy = row["SendBy"].ToString();
                                string Status = row["Status"].ToString() == "False" ? "Unseen" : "Seen";
                                DateTime? Date = row["Date"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(row["Date"]);
                                string CreatedDate = Date.HasValue ? Date.Value.ToString("yyyy-MM-dd HH:mm:ss") : string.Empty;
                                byte[] imageBytes = row["Image"] as byte[];
                                string imageUrl = string.Empty;

                                if (imageBytes != null && imageBytes.Length > 0)
                                {
                                    string base64String = Convert.ToBase64String(imageBytes);
                                    imageUrl = "data:image/png;base64," + base64String;
                                }
                                StringBuilder projectHtml = new StringBuilder();

                                projectHtml.Append("<tr>");
                                projectHtml.Append("<td><a href='#' class='avatar'><img src='" + imageUrl + "' alt='Image' height='50' width='50'></a>");
                                projectHtml.Append("" + EmpId + "</td>");
                                projectHtml.Append("<td>" + Department + "</td>");
                                projectHtml.Append("<td>" + Subject + "</td>");
                                projectHtml.Append("<td>" + Message + "</td>");
                                projectHtml.Append("<td>" + SendBy + "</td>");
                                projectHtml.Append("<td>" + SendTo + "</td>");
                                projectHtml.Append("<td>" + CreatedDate + "</td>");
                                //string statusColor = Status == "Seen" ? "green" : "red";
                                //projectHtml.Append("<td style='color:" + statusColor + ";'>" + Status + "</td>");
                                string statusIcon = Status == "Seen"
    ? "<i class='fa fa-check' aria-hidden='true' style='color:green;'></i>"
    : "<i class='fa fa-times' aria-hidden='true' style='color:red;'></i>";

                                projectHtml.Append("<td>" + statusIcon + "</td>");

                                projectHtml.Append("</tr>");

                                NotificationData.Controls.Add(new LiteralControl(projectHtml.ToString()));
                                S_No++;
                            }

                            ViewState["NotificationDataTable"] = dt;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle the exception appropriately
                throw ex;
            }
        }


        public void UpdateNotification()
        {
            try
            {
                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("UPDATE Notifications SET Status='True' WHERE Status='False' and EmpId=@EmpId", connection);
                    cmd.Parameters.AddWithValue("@EmpId", Session["EmpId"].ToString());
                    cmd.ExecuteNonQuery();                  
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnsendnotification_Click(object sender, EventArgs e)
        {
            string department = Session["DepartmentName"].ToString();
            string company = Session["CompanyName"].ToString();
            string branch = Session["BranchName"].ToString();
            string status = "False";
            string sendBy = Session["Name"].ToString();

            try
            {
                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    connection.Open();
                    SqlCommand fetchEmployeesCmd = new SqlCommand("SELECT FirstName, EmpId FROM Employees WHERE Department = @Department", connection);
                    fetchEmployeesCmd.Parameters.AddWithValue("@Department", department);
                    SqlDataReader reader = fetchEmployeesCmd.ExecuteReader();

                    // Create a list to store employee data
                    var employees = new List<(string Name, string Id)>();

                    while (reader.Read())
                    {
                        string employeeName = reader["FirstName"].ToString();
                        string empId = reader["EmpId"].ToString();
                        employees.Add((employeeName, empId));
                    }

                    reader.Close(); // Close the reader before executing the insert commands

                    // Now loop through the list and insert notifications
                    foreach (var employee in employees)
                    {
                        SqlCommand insertCmd = new SqlCommand("INSERT INTO Notifications (Company, Branch, Department, Employees, EmpId, Subject, Message, Date, Status, SendBy) VALUES (@Company, @Branch, @Department, @Employees, @EmpId, @Subject, @Message, @Date, @Status, @SendBy)", connection);

                        insertCmd.Parameters.AddWithValue("@Company", company);
                        insertCmd.Parameters.AddWithValue("@Branch", branch);
                        insertCmd.Parameters.AddWithValue("@Department", department);
                        insertCmd.Parameters.AddWithValue("@Employees", employee.Name);
                        insertCmd.Parameters.AddWithValue("@EmpId", employee.Id);
                        insertCmd.Parameters.AddWithValue("@Subject", txtsubject.Text);
                        insertCmd.Parameters.AddWithValue("@Message", txtreason.Text);
                        insertCmd.Parameters.AddWithValue("@Date", DateTime.Now);
                        insertCmd.Parameters.AddWithValue("@Status", status);
                        insertCmd.Parameters.AddWithValue("@SendBy", sendBy);

                        insertCmd.ExecuteNonQuery(); // Execute the insert command
                    }
                }

                // Display alert after successful insertion
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Send Notification Successfully');", true);
                BindNotifications();
            }
            catch (Exception ex)
            {
                // Handle the exception (logging or display a message)
                throw ex;
            }

        }
    }
}