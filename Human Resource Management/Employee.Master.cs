using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using iTextSharp.text.pdf.codec;
using System.Text;
using System.Reflection.Emit;

namespace Human_Resource_Management.Roles.Employee
{
    public partial class Employee : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            Response.Cache.SetNoStore();

            if (!IsPostBack)
            {
                //  int totalEmployeeCount = GetTotalEmployeeCount();
                // lblnotoficationcount.Text = totalEmployeeCount.ToString();
                // Label1.Text = totalEmployeeCount.ToString();
                LabelsCount();
                if (Session["EmpId"] == null)
                {
                    Response.Redirect("~/Default.aspx");
                }
                else
                {
                    // Your existing page load logic here
                    try
                    {
                        string empId = Session["EmpId"].ToString();
                    }
                    catch (NullReferenceException)
                    {
                        Response.Redirect("~/Default.aspx");
                    }
                }
            }
            if (!IsPostBack)
            {
                if (Session["EmpId"] == null)
                {
                    Response.Redirect("~/Default.aspx");
                }
                else
                {
                    LoginDetails();
                }
            }
          

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
                    if (role == "Employee")
                    {
                        string name = Session["Name"]?.ToString();
                        if (!string.IsNullOrEmpty(name) && name.Length >= 4)
                        {
                            lblloginname.Text = name.Substring(0, 4);
                        }
                        else
                        {
                            lblloginname.Text = ""; // Handle if the name is null or shorter than 4 characters
                        }

                        if (Session["Image"] != null)
                        {
                            imagedisplay();
                        }
                    }
                    else
                    {
                        Response.Redirect("~/Admindashboard.aspx"); // Use an appropriate page
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
                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    using (SqlCommand sqlcmd = new SqlCommand("SELECT * FROM Employees WHERE EmpId = @EmployeeId", connection))
                    {
                        sqlcmd.Parameters.AddWithValue("@EmployeeId", Session["EmpId"].ToString());
                        connection.Open();
                        using (SqlDataReader myReader = sqlcmd.ExecuteReader())
                        {
                            if (myReader.Read()) // Advance to the first row
                            {
                                string FirstName = myReader["FirstName"].ToString();
                                lblloginname.Text = FirstName.Length >= 4 ? FirstName.Substring(0, 4) : FirstName;

                                if (myReader["Image"] != DBNull.Value)
                                {
                                    byte[] imageBytes = (byte[])myReader["Image"];
                                    string base64String = Convert.ToBase64String(imageBytes);
                                    Image1.ImageUrl = "data:image/jpeg;base64," + base64String;
                                }
                                else
                                {
                                    lblloginname.Text = "Image not available";
                                }
                            }
                            else
                            {
                                lblloginname.Text = "No data found";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('An error occurred: " + ex.Message + "')</script>");
            }
        }

        protected void btnlogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("~/Default.aspx");
        }
        protected void LabelsCount()
        {
            int count = GetTotalEmployeeCount();

            // Update Label1: Show the count if it's greater than 0, otherwise "0"
            lblnotoficationcount.Text = count > 0 ? count.ToString() : "0";

            // Update Label2: Show the count if it's greater than 0, otherwise an empty string
            Label1.Text = count > 0 ? count.ToString() : string.Empty;
        }
        private int GetTotalEmployeeCount()
        {
            int count = 0;

            if (Session["EmpId"] != null)
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    string query = "SELECT COUNT(*) FROM Notifications WHERE Status = 'False' AND EmpId = @EmpId";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@EmpId", Session["EmpId"].ToString());
                        connection.Open();
                        count = (int)command.ExecuteScalar();
                    }
                }
            }
            else
            {

                count = -1;
            }

            return count;
        }

        //private List<int> GetIdsFromBackendData()
        //{
        //    List<int> ids = new List<int>();

        //    try
        //    {
        //        using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
        //        {
        //            string query = "SELECT Id FROM Notifications";
        //            using (SqlCommand command = new SqlCommand(query, connection))
        //            {
        //                connection.Open();
        //                using (SqlDataReader reader = command.ExecuteReader())
        //                {
        //                    while (reader.Read())
        //                    {
        //                        int id = Convert.ToInt32(reader["Id"]);
        //                        ids.Add(id);
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Handle exception
        //        throw ex;
        //    }

        //    return ids;
        //}
        //private void DisplayDivContentForId(int id)
        //{
        //    try
        //    {
        //        using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
        //        {
        //            using (SqlCommand sqlcmd = new SqlCommand("SELECT * FROM Notifications WHERE Id = @Id and status='false'", connection))
        //            {
        //                sqlcmd.Parameters.AddWithValue("@Id", id);
        //                connection.Open();
        //                using (SqlDataReader reader = sqlcmd.ExecuteReader())
        //                {
        //                    if (reader.Read())
        //                    {
        //                        string subject = reader["Subject"].ToString();
        //                        string content = reader["Message"].ToString();

        //                        StringBuilder divHtml = new StringBuilder();
        //                        divHtml.Append("<form id='form2' runat='server'>");
        //                        divHtml.Append("<div class='notification-message' OnClick= 'upDate('" + id + "')>");
        //                        divHtml.Append("<div class='chat-block d-flex'>");
        //                        divHtml.AppendFormat("<span class='avatar flex-shrink-0'><img src='assets/img/profiles/avatar-02.jpg' alt='User Image'></span>");
        //                        divHtml.Append("<div class='media-body flex-grow-1'>");
        //                        divHtml.AppendFormat("<p class='noti-details'><span class='noti-title'>{0}</span></p>", subject);
        //                        divHtml.AppendFormat("<p class='noti-time'><span class='notification-time'>{0}</span></p>", content);
        //                        divHtml.Append("</div>");
        //                        divHtml.Append("</div>");
        //                        divHtml.Append("</div>");
        //                        divHtml.Append("</form>");
        //                        // Create Literal control and add HTML content to it
        //                        Literal divLiteral = new Literal();
        //                        divLiteral.Text = divHtml.ToString();

        //                        // Add Literal control to the container
        //                        projectsContainer.Controls.Add(divLiteral);
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Handle exception
        //        throw ex;
        //    }
        //}

        //protected void btnclearnotify_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        string connectionString = ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString;
        //        using (SqlConnection connection = new SqlConnection(connectionString))
        //        {
        //            connection.Open();
        //            string query = "UPDATE Notifications SET Status = 'True' where EmpId='" + Session["EmpId"].ToString() + "'";
        //            using (SqlCommand command = new SqlCommand(query, connection))
        //            {
        //                command.ExecuteNonQuery();
        //                GetTotalEmployeeCount();
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
    }
}