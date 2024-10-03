using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Irony;

namespace Human_Resource_Management
{
    public partial class AllChangePasswords : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Retrieve empId from query string
                string empId = Request.QueryString["empId"];
                string role = Request.QueryString["role"];

                if (!string.IsNullOrEmpty(empId))
                {
                    txtempid.Text = empId;
                }
                else
                {
                    // Handle case where empId is not available (Optional)
                   // lblMessage.Text = "Employee ID is missing.";
                }
            }
        }

        protected void btnchangepassword_Click(object sender, EventArgs e)
        {
            string empidText = txtempid.Text;
            string password = txtpassword.Text.Trim();

            // Validate if empid and password are not empty
            if (string.IsNullOrEmpty(empidText) || string.IsNullOrEmpty(password))
            {
                Response.Write("<script>alert('Employee ID or Password cannot be empty.');</script>");
                return;
            }

            try
            {
                // Convert empId to integer
                int empid = Convert.ToInt32(empidText);

                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    connection.Open();

                    // Update query for password
                    SqlCommand cmd = new SqlCommand("UPDATE WebLogins SET Password=@Password WHERE EmpId=@EmpId", connection);
                    cmd.Parameters.AddWithValue("@Password", password);
                    cmd.Parameters.AddWithValue("@EmpId", empid);

                    // Execute update query
                    int result = cmd.ExecuteNonQuery();

                    if (result > 0)
                    {
                       
                        string script = @"<script type='text/javascript'>
                        alert('Password updated successfully.');
                        setTimeout(function(){
                            window.location.href='Default.aspx';
                        }, 100); // A small delay before redirecting
                      </script>";

                        // Register the script on the page
                        ClientScript.RegisterStartupScript(this.GetType(), "alertRedirect", script);
                    }
                    else
                    {
                        Response.Write("<script>alert('Failed to update password.');</script>");
                    }
                }
            }
            catch (Exception ex)
            {
                // Log error (could be logged into database or file)
                Response.Write("<script>alert('An error occurred: " + ex.Message + "');</script>");
            }
        }
    }
}