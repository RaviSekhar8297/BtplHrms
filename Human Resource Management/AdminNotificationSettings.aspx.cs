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
    public partial class AdminNotificationSettings : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindNotifications();
            }
        }

        public void BindNotifications(string statusFilter = "All")
        {
            try
            {
                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    string query = "SELECT n.*, e.Image FROM Notifications n INNER JOIN Employees e ON n.EmpId = e.EmpId WHERE n.EmpId = @EmpId";
                    if (statusFilter != "All")
                    {
                        query += " AND n.Status = @Status";
                    }

                    using (SqlCommand sqlcmd = new SqlCommand(query, connection))
                    {
                        sqlcmd.Parameters.AddWithValue("@EmpId", Session["EmpId"].ToString());

                        if (statusFilter != "All")
                        {
                            sqlcmd.Parameters.AddWithValue("@Status", statusFilter == "Seen" ? "True" : "False");
                        }

                        connection.Open();
                        SqlDataAdapter da1 = new SqlDataAdapter(sqlcmd);
                        DataTable dt = new DataTable();
                        da1.Fill(dt);

                        NotifyData.Controls.Clear();

                        if (dt.Rows.Count > 0)
                        {
                            StringBuilder projectHtml = new StringBuilder();

                            foreach (DataRow row in dt.Rows)
                            {
                                string EmpId = row["EmpId"].ToString();
                                string Department = row["Department"].ToString();
                                string Subject = row["Subject"].ToString();
                                string Message = row["Message"].ToString();
                                string SendBy = row["SendBy"].ToString();
                                string SendTo = row["Employees"].ToString();
                                string Status = row["Status"].ToString() == "False" ? "Unseen" : "Seen";
                                DateTime? Date = row["Date"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(row["Date"]);
                                string CreatedDate = Date?.ToString("yyyy-MM-dd HH:mm:ss") ?? string.Empty;

                                byte[] imageBytes = row["Image"] as byte[];
                                string imageUrl = imageBytes != null && imageBytes.Length > 0
                                    ? "data:image/png;base64," + Convert.ToBase64String(imageBytes)
                                    : "default-image.png";  // Fallback image

                                string statusColor = Status == "Seen" ? "green" : "red";

                                projectHtml.Append("<tr>");
                                projectHtml.Append("<td><a href='#' class='avatar'><img src='" + imageUrl + "' alt='Image' height='50' width='50'></a>" + EmpId + "</td>");
                                projectHtml.Append("<td>" + Department + "</td>");
                                projectHtml.Append("<td>" + Subject + "</td>");
                                projectHtml.Append("<td>" + Message + "</td>");
                                projectHtml.Append("<td>" + SendBy + "</td>");
                                projectHtml.Append("<td>" + SendTo + "</td>");
                                projectHtml.Append("<td>" + CreatedDate + "</td>");
                                projectHtml.Append("<td style='color:" + statusColor + ";'>" + Status + "</td>");
                                projectHtml.Append("</tr>");
                            }

                            NotifyData.Controls.Add(new LiteralControl(projectHtml.ToString()));
                            ViewState["NotificationDataTable"] = dt;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;  // Maintain the stack trace
            }
        }


        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedStatus = DropDownList1.SelectedValue;
            BindNotifications(selectedStatus);
        }
    }
}