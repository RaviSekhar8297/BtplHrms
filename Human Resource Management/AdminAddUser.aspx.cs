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
using System.Data;
using DocumentFormat.OpenXml.Wordprocessing;

namespace Human_Resource_Management
{
    public partial class AdminAddUser : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindCompany();
                BindManager();
            }
        }
        public void BindCompany()
        {
            try
            {
                using (SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    using (SqlCommand sqlCmd = new SqlCommand())
                    {
                        sqlCmd.CommandText = "SELECT CompanyId,CompanyName FROM Companies order by CompanyName asc";
                        sqlCmd.Connection = sqlConn;
                        sqlConn.Open();
                        SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                     

                        DropDownList1.DataSource = dt;
                        DropDownList1.DataValueField = "CompanyId";
                        DropDownList1.DataTextField = "CompanyName";
                        DropDownList1.DataBind();

                        sqlConn.Close();
                        DropDownList1.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Select Company--", "0"));
                        // ddlcompany.Items.FindByValue("0").Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void BindManager()
        {
            try
            {
                string role = ddluserrole.SelectedValue;
                string query = string.Empty;

                // Determine the SQL query based on the selected role
                if (role.Equals("Manager", StringComparison.OrdinalIgnoreCase))
                {
                    query = "SELECT * FROM WebLogins WHERE AdminStatus = 'True'";
                }
                else if (role.Equals("Employee", StringComparison.OrdinalIgnoreCase))
                {
                    query = "SELECT * FROM WebLogins WHERE  ManagerStatus = 'True'";
                }
                else
                {
                    // Optionally handle other roles or default case
                    query = "SELECT * FROM WebLogins WHERE AdminStatus = 'True'"; // No results
                }

                using (SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    using (SqlCommand sqlCmd = new SqlCommand(query, sqlConn))
                    {
                        sqlConn.Open();
                        SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        ddlmanager.DataSource = dt;
                        ddlmanager.DataValueField = "EmpId";
                        ddlmanager.DataTextField = "Name";
                        ddlmanager.DataBind();

                        ddlmanager.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Select Manager --", "0"));
                    }
                }
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                throw ex;
            }
        }
        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                String connstrg = ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString;
                using (SqlConnection sqlConn = new SqlConnection(connstrg))
                {
                    using (SqlCommand sqlCmd = new SqlCommand())
                    {
                        sqlCmd.CommandText = "select Branch_Id, BranchName, BranchCode from Branch where [CompanyId] = '" + DropDownList1.SelectedValue + "' order by BranchName asc ";
                        sqlCmd.Connection = sqlConn;
                        sqlConn.Open();
                        SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        ddluserbranch.DataSource = dt;
                        ddluserbranch.DataValueField = "Branch_Id";
                        ddluserbranch.DataTextField = "BranchName";
                        ddluserbranch.DataBind();
                        sqlConn.Close();
                        ddluserbranch.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Select Branch --", "0"));
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void ddluserbranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                String connstrg = ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString;
                using (SqlConnection sqlConn = new SqlConnection(connstrg))
                {
                    using (SqlCommand sqlCmd = new SqlCommand())
                    {
                        sqlCmd.CommandText = "SELECT DeptId,Department FROM Department where BranchId='" + ddluserbranch.SelectedValue + "' order by Department asc ";
                        sqlCmd.Connection = sqlConn;
                        sqlConn.Open();
                        SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        ddluserdept.DataSource = dt;
                        ddluserdept.DataValueField = "DeptId";
                        ddluserdept.DataTextField = "Department";
                        ddluserdept.DataBind();
                        sqlConn.Close();
                        ddluserdept.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Select department --", "0"));
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void ddluserdept_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                String connstrg = ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString;
                using (SqlConnection sqlConn = new SqlConnection(connstrg))
                {
                    using (SqlCommand sqlCmd = new SqlCommand())
                    {
                        sqlCmd.CommandText = "select * from [Employees]  where status= 1  and DepartmentId= '" + ddluserdept.SelectedValue + "' order by FirstName asc";
                        sqlCmd.Connection = sqlConn;
                        sqlConn.Open();
                        SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        ddlusername.DataSource = dt;
                        ddlusername.DataValueField = "EmpId";
                        ddlusername.DataTextField = "FirstName";
                        ddlusername.DataBind();
                        sqlConn.Close();
                        ddlusername.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Select Name --", "0"));
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void ddlusername_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnuserlogin.Visible = true;
            Label1.Text = "";
            try
            {
                txtuserempid.Text = null;
                string connectionString = ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string selectedRole = ddluserrole.SelectedValue;
                    int idd = Convert.ToInt32(ddlusername.SelectedItem.Value);
                    string name = ddlusername.SelectedItem.Text;

                    string query = "SELECT COUNT(*) FROM WebLogins WHERE EmpId = @EmpId";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@EmpId", idd);
                    cmd.Parameters.AddWithValue("@Role", selectedRole);

                    conn.Open();
                    int count = (int)cmd.ExecuteScalar();

                    if (count > 0)
                    {
                        Label1.ForeColor = System.Drawing.Color.Red;
                        Label1.Text = "  "+ name + "  already Exist....";
                        ddlusername.ClearSelection();
                        btnuserlogin.Visible = false;
                        return;
                    }
                }
                using (var strcon = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    using (SqlCommand sqlcmd = new SqlCommand("select EmpId,BranchCode,Image,DOJ,EmployeeId from [Employees]  where status= 1  and EmpId= '" + ddlusername.SelectedValue + "'", strcon))
                    {
                        strcon.Open();
                        using (SqlDataReader sdr = sqlcmd.ExecuteReader())
                        {
                            sdr.Read();
                            if (IsPostBack)
                            {
                                txtuserempid.Text = sdr["EmployeeId"].ToString();
                                TextBox1.Text = sdr["BranchCode"].ToString();
                                TextBox2.Text = Convert.ToDateTime(sdr["DOJ"]).ToString("yyyy-MM-dd");
                            }
                            sdr.Close();
                        }

                        strcon.Close();
                    }
                }

                String connstrg = ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString;
                using (SqlConnection sqlConn = new SqlConnection(connstrg))
                {
                    using (SqlCommand sqlCmd = new SqlCommand())
                    {
                        sqlCmd.CommandText = "select EmpId,LastName,Designation,CompanyEmail from  [Employees]  where status= 1  and EmpId= '" + ddlusername.SelectedValue + "'";
                        sqlCmd.Connection = sqlConn;
                        sqlConn.Open();
                        SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        ddluserdesignation.DataSource = dt;
                        ddluserdesignation.DataValueField = "EmpId";
                        ddluserdesignation.DataTextField = "Designation";
                        //  lblEmail.Text = dt.Rows[0]["CompanyEmail"].ToString();
                        ddluserdesignation.DataBind();
                        sqlConn.Close();
                        ddluserdesignation.Items.Insert(0, new System.Web.UI.WebControls.ListItem(" -- Select Designation -- ", "0"));
                    }
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void ddluserrole_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedRole = ddluserrole.SelectedValue;

            // Ensure that a name is selected
            if (ddlusername.SelectedIndex == 0) // If no selection is made
            {
                Response.Write("<script>alert('Please select Name')</script>");
                ddlmanager.ClearSelection(); // Clear other dropdown selections if needed
                ddlusername.ClearSelection();
                return;
            }

            // Get the selected name after validation
            string name = ddlusername.SelectedItem.Text;

            // Reset checkboxes
            CheckBox1.Checked = false;
            CheckBox2.Checked = false;
            CheckBox3.Checked = false;

            // Enable and check based on the selected role
            switch (selectedRole)
            {
                case "Admin":
                    CheckBox1.Checked = true;
                    CheckBox1.Enabled = true;
                    CheckBox2.Enabled = false;
                    CheckBox3.Enabled = false;
                    break;

                case "Manager":
                    CheckBox2.Checked = true;
                    CheckBox2.Enabled = true;
                    CheckBox1.Enabled = false;
                    CheckBox3.Enabled = false;
                    break;

                case "Employee":
                    CheckBox3.Checked = true;
                    CheckBox3.Enabled = true;
                    CheckBox1.Enabled = false;
                    CheckBox2.Enabled = false;
                    break;

                default:
                    // If 'Select' or any invalid option is chosen
                    CheckBox1.Enabled = false;
                    CheckBox2.Enabled = false;
                    CheckBox3.Enabled = false;
                    break;
            }

        }

        protected void btnuserlogin_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection strcon = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    strcon.Open();
                    string empId = ddlusername.SelectedValue;
                    SqlCommand selectCmd = new SqlCommand("SELECT * FROM [Employees] WHERE status = 1 AND EmpId = @EmpId", strcon);
                    selectCmd.Parameters.AddWithValue("@EmpId", empId);

                    using (SqlDataReader sdr = selectCmd.ExecuteReader())
                    {
                        if (sdr.Read())
                        {
                            string empName = sdr["FirstName"].ToString();
                            string companyEmail = sdr["CompanyEmail"].ToString();
                            string designation = sdr["Designation"].ToString();
                            string companyCellNo = sdr["CompanyCellNo"].ToString();
                            string EmployeeId = sdr["EmployeeId"].ToString();


                            if (!string.IsNullOrEmpty(empName) && empName != "0")
                            {
                                // Close the reader after reading data
                                sdr.Close();

                                // Prepare the insert command
                                SqlCommand cmd = new SqlCommand(@"
                        INSERT INTO WebLogins (
                            EmpId,BTEmpId, Name, Password, Companyid, CompanyName, BranchId, BranchName,
                            DepartmentId, DepartmentName, Designation, Role, UserStatus, Status, Email, Phone,
                            EmailOtp, PhoneOTP, CreatedBy, CreatedDate, UpdatedBy, UpdatedDate, DeletedBy, DeletedDate,
                            AdminStatus, ManagerStatus, EmployeeStatus, AddEmployeStatus, ReadEmployeStatus, EditEmployeStatus,
                            DeleteEmployeStatus, AddHolidayStatus, EditHolidayStatus, DeleteHolidayStatus, AddProject, EditProject,
                            DeleteProject, EditLeave, DeleteLeave, AddLeave, AddAssets, DeleteAssets, EditAssets, BranchCode, ReportName,DOJ,ReportImage
                        ) VALUES (
                            @EmpId,@BTEmpId, @Name, @Password, @Companyid, @CompanyName, @BranchId, @BranchName,
                            @DepartmentId, @DepartmentName, @Designation, @Role, @UserStatus, @Status, @Email, @Phone,
                            @EmailOtp, @PhoneOTP, @CreatedBy, @CreatedDate, @UpdatedBy, @UpdatedDate, @DeletedBy, @DeletedDate,
                            @AdminStatus, @ManagerStatus, @EmployeeStatus, @AddEmployeStatus, @ReadEmployeStatus, @EditEmployeStatus,
                            @DeleteEmployeStatus, @AddHolidayStatus, @EditHolidayStatus, @DeleteHolidayStatus, @AddProject, @EditProject,
                            @DeleteProject, @EditLeave, @DeleteLeave, @AddLeave, @AddAssets, @DeleteAssets, @EditAssets, @BranchCode, @ReportName,@DOJ,@ReportImage
                        )", strcon);

                                // Set parameters for the insert command
                                cmd.Parameters.AddWithValue("@EmpId", empId);
                                cmd.Parameters.AddWithValue("@BTEmpId", EmployeeId);
                                cmd.Parameters.AddWithValue("@Name", empName);
                                cmd.Parameters.AddWithValue("@Password", txtuserpassword.Text);
                                cmd.Parameters.AddWithValue("@Companyid", DropDownList1.SelectedValue);
                                cmd.Parameters.AddWithValue("@CompanyName", DropDownList1.SelectedItem.Text);
                                cmd.Parameters.AddWithValue("@BranchId", ddluserbranch.SelectedValue);
                                cmd.Parameters.AddWithValue("@BranchName", ddluserbranch.SelectedItem.Text);
                                cmd.Parameters.AddWithValue("@DepartmentId", ddluserdept.SelectedValue);
                                cmd.Parameters.AddWithValue("@DepartmentName", ddluserdept.SelectedItem.Text);
                                cmd.Parameters.AddWithValue("@Designation", designation);

                                string role = ddluserrole.SelectedItem.Text;

                                cmd.Parameters.AddWithValue("@Role", role);
                                cmd.Parameters.AddWithValue("@UserStatus", "1");
                                cmd.Parameters.AddWithValue("@Status", "1");
                                cmd.Parameters.AddWithValue("@Email", companyEmail);
                                cmd.Parameters.AddWithValue("@Phone", companyCellNo); 
                                cmd.Parameters.AddWithValue("@EmailOtp", 12345);
                                cmd.Parameters.AddWithValue("@PhoneOTP", 12345);
                                cmd.Parameters.AddWithValue("@CreatedBy", Session["Name"].ToString());
                                cmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
                                cmd.Parameters.AddWithValue("@UpdatedBy", DBNull.Value);
                                cmd.Parameters.AddWithValue("@UpdatedDate", DBNull.Value);
                                cmd.Parameters.AddWithValue("@DeletedBy", DBNull.Value);
                                cmd.Parameters.AddWithValue("@DeletedDate", DBNull.Value);
                                cmd.Parameters.AddWithValue("@AdminStatus", CheckBox1.Checked ? "True" : "False");
                                cmd.Parameters.AddWithValue("@ManagerStatus", CheckBox2.Checked ? "True" : "False");
                                cmd.Parameters.AddWithValue("@EmployeeStatus", CheckBox3.Checked ? "True" : "False");
                                cmd.Parameters.AddWithValue("@AddEmployeStatus", CheckBox4.Checked ? "True" : "False");
                                cmd.Parameters.AddWithValue("@ReadEmployeStatus", "True");
                                cmd.Parameters.AddWithValue("@EditEmployeStatus", CheckBox5.Checked ? "True" : "False");
                                cmd.Parameters.AddWithValue("@DeleteEmployeStatus", CheckBox6.Checked ? "True" : "False");
                                cmd.Parameters.AddWithValue("@AddHolidayStatus", CheckBox7.Checked ? "True" : "False");
                                cmd.Parameters.AddWithValue("@EditHolidayStatus", CheckBox8.Checked ? "True" : "False");
                                cmd.Parameters.AddWithValue("@DeleteHolidayStatus", CheckBox9.Checked ? "True" : "False");
                                cmd.Parameters.AddWithValue("@AddProject", CheckBox10.Checked ? "True" : "False");
                                cmd.Parameters.AddWithValue("@EditProject", CheckBox11.Checked ? "True" : "False");
                                cmd.Parameters.AddWithValue("@DeleteProject", CheckBox12.Checked ? "True" : "False");
                                cmd.Parameters.AddWithValue("@EditLeave", CheckBox13.Checked ? "True" : "False");
                                cmd.Parameters.AddWithValue("@DeleteLeave", CheckBox14.Checked ? "True" : "False");
                                cmd.Parameters.AddWithValue("@AddLeave", CheckBox15.Checked ? "True" : "False");
                                cmd.Parameters.AddWithValue("@AddAssets", CheckBox16.Checked ? "True" : "False");
                                cmd.Parameters.AddWithValue("@DeleteAssets", CheckBox17.Checked ? "True" : "False");
                                cmd.Parameters.AddWithValue("@EditAssets", CheckBox18.Checked ? "True" : "False");
                                cmd.Parameters.AddWithValue("@BranchCode", Convert.ToInt32(ddluserbranch.SelectedValue).ToString());
                               

                                string roleofmanager = ddluserrole.SelectedItem.Text;
                                string SuperAdmin = "SuperAdmin";
                                if(roleofmanager == "Admin")
                                {
                                    cmd.Parameters.AddWithValue("@ReportName", SuperAdmin);
                                }
                                if(roleofmanager == "Manager")
                                {
                                    cmd.Parameters.AddWithValue("@ReportName", ddlmanager.SelectedItem.Text);
                                }
                                if (roleofmanager == "Employee")
                                {
                                    cmd.Parameters.AddWithValue("@ReportName", ddlmanager.SelectedItem.Text);
                                }

                                //string image_path = "Images/pngtree-lotus-flower-jpg-pink-lotus-flower-image_13023952.jpg";
                                //byte[] byes_array = System.IO.File.ReadAllBytes(Server.MapPath(image_path));
                                //string base64String = Convert.ToBase64String(byes_array);
                                //cmd.Parameters.AddWithValue("@ReportImage", base64String);

                                string image_path = "Images/pngtree-lotus-flower-jpg-pink-lotus-flower-image_13023952.jpg";
                                byte[] byte_array = System.IO.File.ReadAllBytes(Server.MapPath(image_path));
                                cmd.Parameters.AddWithValue("@ReportImage", byte_array);

                                cmd.Parameters.AddWithValue("@DOJ", Convert.ToDateTime(TextBox2.Text));


                                // Execute the insert command
                                int i = cmd.ExecuteNonQuery();
                                if (i > 0)
                                {
                                    // Generate the login token
                                    string token = Guid.NewGuid().ToString();
                                    DateTime expiryDateTime = DateTime.Now.AddHours(1); // Token valid for 1 hour

                                    // Store the token in the database
                                    SqlCommand tokenCmd = new SqlCommand("INSERT INTO LoginTokens (EmpId, Token, ExpiryDateTime, IsUsed) VALUES (@EmpId, @Token, @ExpiryDateTime, 0)", strcon);
                                    tokenCmd.Parameters.AddWithValue("@EmpId", empId);
                                    tokenCmd.Parameters.AddWithValue("@Token", token);
                                    tokenCmd.Parameters.AddWithValue("@ExpiryDateTime", expiryDateTime);
                                    tokenCmd.ExecuteNonQuery();

                                    // Generate the login link
                                   // string loginLink = $"https://localhost:44341/AllChangePasswords?token={token}";
                                  //  string loginLink = $"https://localhost:44341/AllChangePasswords?token={token}";
                                    string loginLink = $"https://localhost:44341/AllChangePasswords?token={token}&empId={empId}&Role={role}";

                                    // Send email with login details
                                  //  SendEmail(empName, companyEmail, loginLink, empId, txtuserpassword.Text);

                                    Response.Write("<script>alert('" + empName + " User login Created Successfully. Login link sent to Email.')</script>");
                                    DropDownList1.ClearSelection();
                                    ddluserbranch.ClearSelection();
                                    ddluserdept.ClearSelection();
                                    ddlusername.ClearSelection();
                                    ddluserdesignation.ClearSelection();
                                    ddluserrole.ClearSelection();
                                    txtuserempid.Text = "";
                                    TextBox1.Text = "";
                                    TextBox2.Text = "";
                                    ddlmanager.ClearSelection();
                                }
                                else
                                {
                                    Response.Write("<script>alert('" + empName + " User login Failed.')</script>");
                                }
                            }
                            else
                            {
                                Response.Write("<script>alert('Employee not found.')</script>");
                            }
                        }
                        else
                        {
                            Response.Write("<script>alert('Employee not found.')</script>");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error: " + ex.Message + "')</script>");
            }


        }
        private void SendEmail(string empName, string toEmail, string loginLink, string empId, string password)
        {
            try
            {
                string fromEmail = "software.trainee1@brihaspathi.com"; // Replace with your email address
                string fromPassword = "RAVI8297"; // Replace with your email password
                string subject = "BT - Log In Details";
                string body = $@"
                        <p>Dear {empName},</p>
                        <p>Your login details are as follows:</p>
                        <p>Employee ID: {empId}</p>
                        <p>Password: {password}</p>
                        <p>Please click the following link to login:</p>
                        <p><a href='{loginLink}'>Login</a></p>
                        <p>This link is  expire in One hour.</p>
                        <p>Best regards,<br />Your Company Name</p>";

                MailMessage message = new MailMessage();
                message.From = new MailAddress(fromEmail);
                message.To.Add(new MailAddress(toEmail));
                message.Subject = subject;
                message.Body = body;
                message.IsBodyHtml = true;

                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587); // Replace with your SMTP server and port
                smtp.Credentials = new NetworkCredential(fromEmail, fromPassword);
                smtp.EnableSsl = true;
                smtp.Send(message);


            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error sending email: " + ex.Message + "');</script>");
            }
        }
 
    }
}