using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Human_Resource_Management
{
    public partial class AdminChangePassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string Empid = Session["EmpId"].ToString();
                TextBox1.Text = Empid;
            }
        }

        protected void btnchangepassword_Click(object sender, EventArgs e)
        {
            string Role = Session["Role"].ToString();
            string Email = Session["Email"].ToString();
            string newPassword = TextBox2.Text;
            try
            {
                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("UPDATE WebLogins SET Password='" + newPassword + "' where EmpId='" + TextBox1.Text + "' AND Role=@Role ", connection);
                    connection.Open();
                    cmd.Parameters.AddWithValue("@Role", Role);
                    int i = cmd.ExecuteNonQuery();
                    connection.Close();
                    if (i > 0)
                    {
                        Response.Write("<script>alert('Password Updated Successfully..')</script>");
                        SendConfirmationEmail(Email, newPassword);
                        TextBox2.Text = "";
                        TextBox3.Text = "";
                    }
                    else
                    {
                        Response.Write("<script>alert('Failed..')</script>");
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void SendConfirmationEmail(string email,string pwd)
        {
            try
            {
                string smtpUsername = "software.trainee1@brihaspathi.com";
                string smtpPassword = "RAVI8297";
                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(smtpUsername);
                    mail.To.Add(email);
                    mail.Subject = "Password Change";
                    mail.Body = $"<p>Your password <strong>{pwd}</strong> has been Updated successfully.</p>";
                    mail.IsBodyHtml = true;

                    using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                    {
                        smtp.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
                        smtp.EnableSsl = true;
                        smtp.Send(mail);
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Failed to send confirmation email: " + ex.Message + "');</script>");
            }
        }
    }
}