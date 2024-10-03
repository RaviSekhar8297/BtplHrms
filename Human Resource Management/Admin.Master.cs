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
    public partial class Admin : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["EmpId"] == null)
                {
                    Response.Redirect("~/Default.aspx");
                }
                else
                {
                    CheckRoleAccess();
                    LoginDetails();
                }
                int totalEmployeeCount = GetTotalEmployeeCount();
                lblnotoficationcount.Text = totalEmployeeCount.ToString();
               // CreateDynamicButtons();
            }
            if (!IsPostBack)
            {
                try
                {
                    List<int> ids = GetIdsFromBackendData();
                    foreach (int id in ids)
                    {
                        DisplayDivContentForId(id);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        private void CheckRoleAccess()
        {
            string role = Session["Role"]?.ToString().Trim();
            if (role == "Employee")
            {              
                Response.Redirect("~/EmployeeDashboard.aspx");
            }
            // No need for further checks; if the user is Admin, SuperAdmin, or Manager, they can stay on the page
        }
        protected void LoginDetails()
        {
            try
            {
                if (Session["EmpId"] == null)
                {
                    Response.Redirect("~/Default.aspx");
                }
                else
                {
                    var role = Session["Role"]?.ToString().Trim();
                    if (role == "Admin")
                    {
                        string name = Session["Name"]?.ToString();
                        if (!string.IsNullOrEmpty(name) && name.Length >= 4)
                        {
                            lblname.Text = name.Substring(0, 4);
                        }
                        else
                        {
                            lblname.Text = ""; // Handle if the name is null or shorter than 4 characters
                        }

                        if (Session["Image"] != null)
                        {
                            imagedisplay();
                        }
                    }
                    else
                    {
                       // Response.Redirect("Admindashboard.aspx"); // Use an appropriate page
                    }
                }
            }
            catch (Exception)
            {
                Response.Redirect("~/Admindashboard.aspx");
            }
        }

        public void imagedisplay()
        {
            try
            {
                // Ensure that EmpId is valid
                if (Session["EmpId"] == null || string.IsNullOrEmpty(Session["EmpId"].ToString()))
                {
                    // Handle the case where the session value is invalid
                    Image1.ImageUrl = "~/images/default-placeholder.png";  // Use a default image if no EmpId is found
                    return;
                }

                // Create and open SQL connection
                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    // Prepare the SQL command to fetch the image
                    using (SqlCommand sqlcmd = new SqlCommand("SELECT Image FROM WebLogins WHERE EmpId = @EmpId", connection))
                    {
                        sqlcmd.Parameters.AddWithValue("@EmpId", Session["EmpId"]);

                        connection.Open();
                        object empImage = sqlcmd.ExecuteScalar(); // Execute the query

                        // Check if an image was retrieved and if it's not null
                        if (empImage != null && empImage is byte[])
                        {
                            byte[] imageBytes = (byte[])empImage;

                            // Check if the imageBytes array is not empty
                            if (imageBytes.Length > 0)
                            {
                                string base64String = Convert.ToBase64String(imageBytes);
                                Image1.ImageUrl = "data:image/jpeg;base64," + base64String;
                            }
                            else
                            {
                                // Handle the case where the imageBytes are empty
                                Image1.ImageUrl = "~/images/default-placeholder.png";
                            }
                        }
                        else
                        {
                            // Handle the case where no image is found for the given EmpId
                            Image1.ImageUrl = "~/images/default-placeholder.png";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the error for debugging purposes (you could also use other logging frameworks)
                System.Diagnostics.Debug.WriteLine("Error in imagedisplay: " + ex.Message);

                // Optionally, display a default image in case of an error
                Image1.ImageUrl = "~/images/default-placeholder.png";
            }
        }


        private int GetTotalEmployeeCount()
        {
            int count = 0;
            using (SqlConnection connection = new SqlConnection((ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString)))
            {
                string query = "SELECT COUNT(*) FROM Notifications WHERE status='False' and EmpId='" + Session["EmpId"].ToString() + "'";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    count = (int)command.ExecuteScalar();
                }
            }
            return count;
        }
        private void DisplayDivContentForId(int id)
        {
            try
            {
                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    using (SqlCommand sqlcmd = new SqlCommand("SELECT * FROM Notifications WHERE Id = @Id and status='false' and EmpId='" + Session["EmpId"].ToString() + "'", connection))
                    {
                        sqlcmd.Parameters.AddWithValue("@Id", id);
                        connection.Open();
                        using (SqlDataReader reader = sqlcmd.ExecuteReader())
                        {
                            int slno = 1;
                            if (reader.Read())
                            {
                                string subject = reader["Subject"].ToString();
                                string content = reader["Message"].ToString();

                                StringBuilder divHtml = new StringBuilder();
                                divHtml.Append("<form id='form2' runat='server'>");
                                divHtml.Append("<div class='notification-message' OnClick= 'upDate('" + id +"')>");
                                divHtml.Append("<div class='chat-block d-flex'>");
                                divHtml.AppendFormat("<span class='avatar flex-shrink-0'><" + slno + "></span>");
                                divHtml.Append("<div class='media-body flex-grow-1'>");
                                divHtml.AppendFormat("<p class='noti-details'><span class='noti-title'>{0}</span></p>", subject);
                                divHtml.AppendFormat("<p class='noti-time'><span class='notification-time'>{0}</span></p>", content);
                                divHtml.Append("</div>");
                                divHtml.Append("</div>");
                                divHtml.Append("</div>");
                                divHtml.Append("</form>");
                                // Create Literal control and add HTML content to it
                                Literal divLiteral = new Literal();
                                divLiteral.Text = divHtml.ToString();

                                // Add Literal control to the container
                                projectsContainer.Controls.Add(divLiteral);
                                slno ++;
                                
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                throw ex;
            }
        }

        private List<int> GetIdsFromBackendData()
        {
            List<int> ids = new List<int>();

            try
            {
                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    string query = "SELECT Id FROM Notifications";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int id = Convert.ToInt32(reader["Id"]);
                                ids.Add(id);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                throw ex;
            }

            return ids;
        }


        protected void btnclearnotify_Click(object sender, EventArgs e)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "UPDATE Notifications SET Status = 'True' where EmpId='" + Session["EmpId"].ToString() + "'";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {                     
                        command.ExecuteNonQuery();
                        GetTotalEmployeeCount();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnlogout_Click(object sender, EventArgs e)
        {
            //string userId = Session["EmpId"] as string;

            //if (!string.IsNullOrEmpty(userId))
            //{
            //    using (var connstrg = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
            //    {
            //        using (SqlCommand cmd = new SqlCommand("UPDATE WebLogins SET UserStatus = '1' WHERE EmpId = @EmpId", connstrg))
            //        {
            //            cmd.Parameters.AddWithValue("@EmpId", userId);
            //            connstrg.Open();
            //            cmd.ExecuteNonQuery();
            //        }
            //    }
            //}

            //// Clear session and redirect to login page
            //Session.Clear();
            //Response.Redirect("Default.aspx");
        }
    } 
}