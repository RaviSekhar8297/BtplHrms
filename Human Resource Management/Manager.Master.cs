using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Human_Resource_Management
{
    public partial class Manager : System.Web.UI.MasterPage
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
                    LoginDetails();
                }
                LabelsCount();
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
                    if (role == "Manager")
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

                       
                            imagedisplay();
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
                    using (SqlCommand sqlcmd = new SqlCommand("SELECT Image FROM WebLogins WHERE EmpId = @EmpId", connection))
                    {
                        sqlcmd.Parameters.AddWithValue("@EmpId", Session["EmpId"]);

                        connection.Open();
                        object empImage = sqlcmd.ExecuteScalar();

                        if (empImage != DBNull.Value && empImage != null)
                        {
                            byte[] imageBytes = (byte[])empImage;
                            string base64String = Convert.ToBase64String(imageBytes);
                            Image1.ImageUrl = "data:image/jpeg;base64," + base64String;
                        }
                        else
                        {
                            Image1.ImageUrl = "path/to/default/image.jpg"; // Replace with the path to a default image
                        }
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
            Session.Clear(); // Clears all session variables
            Session.Abandon(); // Ends the session

            // Optionally, clear the authentication cookie if you're using forms authentication
            if (Request.IsAuthenticated)
            {
                HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, "")
                {
                    Expires = DateTime.Now.AddYears(-1) // Set the cookie to expire
                };
                Response.Cookies.Add(cookie);
            }

            // Redirect to the login page or home page
            Response.Redirect("Default.aspx", false); // Use false to avoid threading issues
            HttpContext.Current.ApplicationInstance.CompleteRequest(); // Ensures the response is terminated

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
    }
}