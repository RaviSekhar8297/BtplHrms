using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Threading;
using AjaxControlToolkit.HtmlEditor.ToolbarButtons;
using System.Net.Mail;
using System.Net;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Human_Resource_Management
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string token = Request.QueryString["token"];
            txtforotp.Visible = false;
            txtforpass.Visible = false;
            txtforconfirmpass.Visible = false;
            btnupdatepassword.Visible = false;
            if (!string.IsNullOrEmpty(token))
            {
                ValidateToken(token);
            }
        }

       

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            try
            {
                using (SqlConnection connstrg = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    connstrg.Open();

                    // Check if user is already logged in
                    using (SqlCommand checkCmd = new SqlCommand("SELECT UserStatus FROM WebLogins WHERE EmpId = @EmpId", connstrg))
                    {
                        checkCmd.Parameters.AddWithValue("@EmpId", username);
                        object loginStatusObj = checkCmd.ExecuteScalar();
                        string userStatus = loginStatusObj as string;
                        bool isLoggedIn = userStatus != null && userStatus.Trim() == "0";

                        if (isLoggedIn)
                        {
                            Response.Write("<script>alert('User is already logged in.');</script>");
                            return;
                        }
                    }

                    // Validate user credentials
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM WebLogins WHERE EmpId = @EmpId AND Password = @Password AND Status = '1'", connstrg))
                    {
                        cmd.Parameters.AddWithValue("@EmpId", username);
                        cmd.Parameters.AddWithValue("@Password", password);

                        SqlDataAdapter sqlda = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        sqlda.Fill(dt);

                        if (dt.Rows.Count > 0)
                        {
                            DataRow row = dt.Rows[0];
                            string EmpId = row["EmpId"].ToString().Trim();
                            string Name = row["Name"].ToString().Trim();
                            string Email = row["Email"].ToString().Trim();
                            string Companyid = row["Companyid"].ToString().Trim();
                            string CompanyName = row["CompanyName"].ToString().Trim();
                            string BranchId = row["BranchId"].ToString().Trim();
                            string BranchCode = row["BranchCode"].ToString().Trim();
                            string BranchName = row["BranchName"].ToString().Trim();
                            string DepartmentId = row["DepartmentId"].ToString().Trim();
                            string DepartmentName = row["DepartmentName"].ToString().Trim();
                            string Designation = row["Designation"].ToString().Trim();
                            string Role = row["Role"].ToString().Trim();
                            string AdminStatus = row["AdminStatus"].ToString().Trim();
                            string EmployeeStatus = row["EmployeeStatus"].ToString().Trim();
                            string ManagerStatus = row["ManagerStatus"].ToString().Trim();
                            string AddEmployeStatus = row["AddEmployeStatus"].ToString().Trim();
                            string ReadEmployeStatus = row["ReadEmployeStatus"].ToString().Trim();
                            string EditEmployeStatus = row["EditEmployeStatus"].ToString().Trim();
                            string DeleteEmployeStatus = row["DeleteEmployeStatus"].ToString().Trim();
                            string AddHolidayStatus = row["AddHolidayStatus"].ToString().Trim();
                            string EditHolidayStatus = row["EditHolidayStatus"].ToString().Trim();
                            string DeleteHolidayStatus = row["DeleteHolidayStatus"].ToString().Trim();
                            string AddProject = row["AddProject"].ToString().Trim();
                            string EditProject = row["EditProject"].ToString().Trim();
                            string DeleteProject = row["DeleteProject"].ToString().Trim();
                            string EditLeave = row["EditLeave"].ToString().Trim();
                            string DeleteLeave = row["DeleteLeave"].ToString().Trim();
                            string AddLeave = row["AddLeave"].ToString().Trim();
                            string AddAssets = row["AddAssets"].ToString().Trim();
                            string DeleteAssets = row["DeleteAssets"].ToString().Trim();
                            string EditAssets = row["EditAssets"].ToString().Trim();
                            string DOJ = row["DOJ"].ToString().Trim();

                            byte[] Image = row["Image"] != DBNull.Value ? (byte[])row["Image"] : new byte[0];
                            string base64String = Convert.ToBase64String(Image);

                            if (EmpId == username && password == row["Password"].ToString().Trim())
                            {
                                // Set session variables
                                Session["EmpId"] = EmpId;
                                Session["Name"] = Name;
                                Session["Email"] = Email;
                                Session["Companyid"] = Companyid;
                                Session["CompanyName"] = CompanyName;
                                Session["BranchId"] = BranchId;
                                Session["BranchCode"] = BranchCode;
                                Session["BranchName"] = BranchName;
                                Session["DepartmentId"] = DepartmentId;
                                Session["DepartmentName"] = DepartmentName;
                                Session["Designation"] = Designation;
                                Session["Role"] = Role;
                                Session["Image"] = base64String;
                                Session["AdminStatus"] = AdminStatus;
                                Session["EmployeeStatus"] = EmployeeStatus;
                                Session["ManagerStatus"] = ManagerStatus;
                                Session["AddEmployeStatus"] = AddEmployeStatus;
                                Session["ReadEmployeStatus"] = ReadEmployeStatus;
                                Session["EditEmployeStatus"] = EditEmployeStatus;
                                Session["DeleteEmployeStatus"] = DeleteEmployeStatus;
                                Session["AddHolidayStatus"] = AddHolidayStatus;
                                Session["EditHolidayStatus"] = EditHolidayStatus;
                                Session["DeleteHolidayStatus"] = DeleteHolidayStatus;
                                Session["AddProject"] = AddProject;
                                Session["EditProject"] = EditProject;
                                Session["DeleteProject"] = DeleteProject;
                                Session["EditLeave"] = EditLeave;
                                Session["DeleteLeave"] = DeleteLeave;
                                Session["AddLeave"] = AddLeave;
                                Session["AddAssets"] = AddAssets;
                                Session["DeleteAssets"] = DeleteAssets;
                                Session["EditAssets"] = EditAssets;
                                Session["DOJ"] = DOJ;

                                // Redirect based on user role
                                if (Role == "Admin")
                                {
                                    Response.Redirect("Admindashboard.aspx", false);
                                }
                                else if (Role == "Manager")
                                {
                                    Response.Redirect("ManagerDashbord.aspx", false);
                                }
                                else
                                {
                                    Response.Redirect("Employeedashboard.aspx", false);
                                }

                                HttpContext.Current.ApplicationInstance.CompleteRequest(); // Ensure response is completed
                            }
                            else
                            {
                                Response.Write("<script>alert('Incorrect username or password.');</script>");
                            }
                        }
                        else
                        {
                            Response.Write("<script>alert('Invalid credentials.');</script>");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception and notify the user
                // For example: log to a file or database, or display a user-friendly message
                Response.Write("<script>alert('An error occurred."+ex+" Please try again later.');</script>");
                // Consider logging the exception details for debugging
            }
        }

        private void LogLoginDetails(SqlConnection conn, string empId, string name, string Roles)
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                using (SqlCommand cmd = new SqlCommand("INSERT INTO LogDetails (EmpId, Name, LogDate,Role) VALUES (@EmpId, @Name, @LogDate,@Role)", conn))
                {
                    cmd.Parameters.AddWithValue("@EmpId", empId);
                    cmd.Parameters.AddWithValue("@Name", name);
                    cmd.Parameters.AddWithValue("@LogDate", DateTime.Now);
                    cmd.Parameters.AddWithValue("@Role", Roles);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        private void ValidateToken(string token)
        {
            try
            {
                using (SqlConnection strcon = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    strcon.Open();
                    SqlCommand selectCmd = new SqlCommand("SELECT * FROM LoginTokens WHERE Token = @Token AND IsUsed = 0 AND ExpiryDateTime > GETDATE()", strcon);
                    selectCmd.Parameters.AddWithValue("@Token", token);

                    using (SqlDataReader sdr = selectCmd.ExecuteReader())
                    {
                        if (sdr.Read())
                        {
                            // Token is valid
                            string empId = sdr["EmpId"].ToString();

                            // Mark the token as used
                            sdr.Close();
                            SqlCommand updateCmd = new SqlCommand("UPDATE LoginTokens SET IsUsed = 0 WHERE Token = @Token", strcon);
                            updateCmd.Parameters.AddWithValue("@Token", token);
                            updateCmd.ExecuteNonQuery();

                            // Redirect to a page where the user can set a new password or log in
                           // Response.Redirect("SetNewPassword.aspx?empId=" + empId);
                        }
                        else
                        {
                            // Token is invalid or expired
                            Response.Write("<script>alert('Invalid or expired token.');</script>");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error: " + ex.Message + "');</script>");
            }
        }

        protected void txtforuserid_TextChanged(object sender, EventArgs e)
        {
            int id;
            if (!int.TryParse(txtforuserid.Text.Trim(), out id))
            {
                Label1.Text = "Invalid User ID format.";
                return;
            }

            string email = string.Empty;
            string name = string.Empty;
            Random r = new Random();
            int num = r.Next(10000, 99999);
            string otp = num.ToString(); // Generate the OTP

            try
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SELECT Email,Name, Image FROM WebLogins WHERE EmpId = @EmpId AND Status = 1", con);
                    cmd.Parameters.AddWithValue("@EmpId", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read()) // Reads the first record
                        {
                            email = reader["Email"].ToString();
                            name = reader["Name"].ToString();
                            // Read the image as a byte array
                            byte[] imageBytes = reader["Image"] as byte[];

                            // Convert byte array to Base64 string
                            string base64String = imageBytes != null ? Convert.ToBase64String(imageBytes) : string.Empty;

                            // Send OTP email
                            // SendEmail(email, otp,name);
                            UpdateEmailOtp(id, otp);
                            //Label1.ForeColor = System.Drawing.Color.Green;
                            //Label1.Text = "OTP has been sent to your email.";
                            Response.Write("<script>alert('OTP send to " + email + "')</script>");
                            txtforotp.Visible = true;
                            txtforpass.Visible = false;
                            txtforconfirmpass.Visible = false;
                            btnupdatepassword.Visible = false;

                            // Set the image as a Base64 string
                            Image1.ImageUrl = "data:image/png;base64," + base64String; // Adjust mime type if necessary
                            Image1.Visible = true; // Make the image visible
                        }
                        else
                        {
                            Label1.Text = "User not found.";
                            Image1.Visible = false; // Hide the image if user not found
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle the exception as needed
                Label1.Text = "An error occurred: " + ex.Message;
            }

        }
       

        // Method to send OTP to the email
        private void SendEmail(string email, string otp,string Name)
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("software.trainee1@brihaspathi.com");

                // Add the primary recipient (id)
                mail.To.Add(email);
                mail.Subject = "Your OTP Code";
                mail.Body = "Hi " + Name + ", Your OTP code is: " + otp;
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
                smtpClient.Port = 587; // or the port you use
                smtpClient.Credentials = new NetworkCredential("software.trainee1@brihaspathi.com", "RAVI8297");
                smtpClient.EnableSsl = true;

                smtpClient.Send(mail);
            }
            catch (Exception ex)
            {
                throw new Exception("Could not send email. " + ex.Message);
            }
        }

        // Method to update EmailOtp in the database
        private void UpdateEmailOtp(int id, string otp)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("UPDATE WebLogins SET EmailOtp = @EmailOtp WHERE EmpId = @EmpId", con);
                cmd.Parameters.AddWithValue("@EmailOtp", otp);
                cmd.Parameters.AddWithValue("@EmpId", id);
                cmd.ExecuteNonQuery();
            }
        }

        protected void txtforotp_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int id;
                if (!int.TryParse(txtforuserid.Text.Trim(), out id))
                {
                    Label1.Text = "Invalid User ID format.";
                    return;
                }

                // Connect to the database
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    con.Open();

                    // Query to get the EmailOtp based on UserId
                    SqlCommand cmd = new SqlCommand("SELECT EmailOtp FROM WebLogins WHERE EmpId = @EmpId AND Status = 1", con);
                    cmd.Parameters.AddWithValue("@EmpId", id);

                    // Execute the command
                    object otpFromDb = cmd.ExecuteScalar();
                    if (otpFromDb != null)
                    {
                        string emailOtp = otpFromDb.ToString();

                        // Check if the entered OTP matches the one from the database
                        if (txtforotp.Text.Trim() == emailOtp)
                        {
                            Label1.Text = "OTP Success. Please Set New Password";
                            txtforotp.Visible = true;
                            txtforpass.Visible = true;
                            txtforconfirmpass.Visible = true;
                            btnupdatepassword.Visible = true;
                        }
                        else
                        {
                            Label1.Text = "Wrong OTP. Please try again.";
                            txtforotp.Visible = true;
                        }
                    }
                    else
                    {
                        Label1.Text = "User not found.";
                    }
                }
            }
            catch (Exception ex)
            {
                Label1.Text = "An error occurred: " + ex.Message; // Display the error message
            }
        }

        protected void btnupdatepassword_Click(object sender, EventArgs e)
        {
            try
            {
                int id;
                if (!int.TryParse(txtforuserid.Text.Trim(), out id))
                {
                    Label1.Text = "Invalid User ID format.";
                    return;
                }
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Update WebLogins Set Password=@Password WHERE EmpId = @EmpId AND Status = 1", con);
                    cmd.Parameters.AddWithValue("@Password", txtforpass.Text.Trim());
                    cmd.Parameters.AddWithValue("@EmpId", id);
                    int i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        string script = @"<script type='text/javascript'>
                        alert('Password updated successfully.');
                        setTimeout(function(){
                            window.location.href='Default.aspx';
                        }, 100); // A small delay before redirecting
                      </script>";

                        ClientScript.RegisterStartupScript(this.GetType(), "alertRedirect", script);
                    }
                    else
                    {
                        Response.Write("<script>alert('Password Updated Successfully.....')</script>");
                    }
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}