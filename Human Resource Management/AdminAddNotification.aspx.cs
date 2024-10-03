using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.ExtendedProperties;
using Human_Resource_Management.Roles.Employee;
using Org.BouncyCastle.Ocsp;
using System.Web.Services.Description;
using System.Net.Mail;
using System.Net;

namespace Human_Resource_Management
{
    public partial class AdminAddNotification : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindCompany();
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
                        sqlCmd.CommandText = "SELECT CompanyId,CompanyName FROM Companies  ";
                        sqlCmd.Connection = sqlConn;
                        sqlConn.Open();
                        SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        ddlcompany.DataSource = dt;
                        ddlcompany.DataValueField = "CompanyId";
                        ddlcompany.DataTextField = "CompanyName";
                        ddlcompany.DataBind();

                        sqlConn.Close();

                        ddlcompany.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Select Company--", "0"));

                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void ddlcompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                String connstrg = ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString;
                using (SqlConnection sqlConn = new SqlConnection(connstrg))
                {
                    using (SqlCommand sqlCmd = new SqlCommand())
                    {
                        sqlCmd.CommandText = "select Branch_Id, BranchName, BranchCode from Branch where  CompanyId='" + ddlcompany.SelectedValue + "' ";
                        sqlCmd.Connection = sqlConn;
                        sqlConn.Open();
                        SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        ddlbranch.DataSource = dt;
                        ddlbranch.DataValueField = "Branch_Id";
                        ddlbranch.DataTextField = "BranchName";
                        ddlbranch.DataBind();
                        sqlConn.Close();
                        ddlbranch.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Select Branch --", "0"));
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void ddlbranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                String connstrg = ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString;
                using (SqlConnection sqlConn = new SqlConnection(connstrg))
                {
                    using (SqlCommand sqlCmd = new SqlCommand())
                    {
                        sqlCmd.CommandText = "SELECT DeptId,Department FROM Department where BranchId='" + ddlbranch.SelectedValue + "' ";
                        sqlCmd.Connection = sqlConn;
                        sqlConn.Open();
                        SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        ddldepartment.DataSource = dt;
                        ddldepartment.DataValueField = "DeptId";
                        ddldepartment.DataTextField = "Department";
                        ddldepartment.DataBind();
                        sqlConn.Close();
                        ddldepartment.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Select Department --", "0"));
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void ddldepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string connstrg = ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString;

                using (SqlConnection sqlConn = new SqlConnection(connstrg))
                {
                    using (SqlCommand sqlCmd = new SqlCommand())
                    {
                        if (ddldepartment.SelectedValue == "0")
                        {
                            sqlCmd.CommandText = "SELECT * FROM Employees WHERE Status = 1";
                        }
                        else
                        {
                            sqlCmd.CommandText = "SELECT * FROM Employees WHERE Status = 1 AND DepartmentId = @DepartmentId";
                            sqlCmd.Parameters.AddWithValue("@DepartmentId", ddldepartment.SelectedValue);
                        }

                        sqlCmd.Connection = sqlConn;
                        sqlConn.Open();
                        SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        ddlname.DataSource = dt;
                        ddlname.DataValueField = "EmpId";
                        ddlname.DataTextField = "FirstName";
                        ddlname.DataBind();
                        sqlConn.Close();

                        ddlname.Items.Insert(0, new ListItem("ALL Employees", "0"));
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnsendnotification_Click(object sender, EventArgs e)
        {
            try
            {
                string connstrg = ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString;

                if (DropDownList1.SelectedValue == "Through Mail")
                {
                    if (ddlname.SelectedValue == "0")
                    {
                        // Send email to all employees
                        SendEmailToAllEmployees(connstrg);
                    }
                    else
                    {
                        // Send email to selected employee
                        string empId = ddlname.SelectedValue;
                        SendEmailToEmployee(empId, connstrg);
                    }
                }
                else
                {
                    if (ddlname.SelectedValue == "0")
                    {
                        SendEmailToAllEmployees(connstrg);
                    }
                    else
                    {
                        // Send email to selected employee
                        string empId = ddlname.SelectedValue;
                        SendEmailToEmployee(empId, connstrg);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void SendEmailToAllEmployees(string connstrg)
        {
            try
            {
                string company = ddlcompany.SelectedItem.Text;
                string branch = ddlbranch.SelectedItem.Text;
                string department = ddldepartment.SelectedItem.Text;
                string subject = txtsubject.Text.Trim();
                string message = txtmessage.Text.Trim();

                string query = @"SELECT EmpId, LastName, CompanyEmail FROM Employees WHERE Status = 1";

                using (SqlConnection sqlConn = new SqlConnection(connstrg))
                {
                    using (SqlCommand sqlCmd = new SqlCommand(query, sqlConn))
                    {
                        sqlConn.Open();
                        SqlDataReader reader = sqlCmd.ExecuteReader();
                        while (reader.Read())
                        {
                            string empId = reader["EmpId"].ToString();
                            string employeeName = reader["LastName"].ToString();
                            string companyMail = reader["CompanyEmail"].ToString();

                            if (!string.IsNullOrEmpty(companyMail))
                            {
                                // Send email to each employee with a valid email address
                                SendEmail(company, branch, department, employeeName, empId, subject, message, companyMail, connstrg);
                            }
                            else
                            {

                            }
                        }
                        sqlConn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        protected void SendEmailToEmployee(string empId, string connstrg)
        {
            try
            {
                string company = "", branch = "", department = "", employeeName = "", companyMail = "";

                string query = @"SELECT Company, Branch, Department, LastName, CompanyEmail FROM Employees WHERE EmpId = @EmpId";

                using (SqlConnection sqlConn = new SqlConnection(connstrg))
                {
                    using (SqlCommand sqlCmd = new SqlCommand(query, sqlConn))
                    {
                        sqlCmd.Parameters.AddWithValue("@EmpId", empId);

                        sqlConn.Open();
                        SqlDataReader reader = sqlCmd.ExecuteReader();
                        if (reader.Read())
                        {
                            company = reader["Company"].ToString();
                            branch = reader["Branch"].ToString();
                            department = reader["Department"].ToString();
                            employeeName = reader["LastName"].ToString();
                            companyMail = reader["CompanyEmail"].ToString();
                        }
                        sqlConn.Close();
                    }
                }

                if (!string.IsNullOrEmpty(companyMail))
                {
                    string subject = txtsubject.Text.Trim();
                    string message = txtmessage.Text.Trim();

                    // Send email to selected employee
                    SendEmail(company, branch, department, employeeName, empId, subject, message, companyMail, connstrg);
                }
                else
                {
                    // Handle case where companyMail is empty or null (optional)
                    throw new Exception("Company mail is not available for the selected employee.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        protected void SendEmail(string company, string branch, string department, string employeeName, string empId, string subject, string message, string companyMail, string connstrg)
        {
            try
            {
                string smtpServer = "smtp.gmail.com";
                int smtpPort = 587;
                string smtpUsername = "software.trainee1@brihaspathi.com";
                string smtpPassword = "RAVI8297";
                bool enableSsl = true;

                using (SmtpClient smtpClient = new SmtpClient(smtpServer, smtpPort))
                {
                    smtpClient.EnableSsl = enableSsl;
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Credentials = new NetworkCredential(smtpUsername, smtpPassword);

                    MailMessage mail = new MailMessage();
                    mail.From = new MailAddress(smtpUsername);
                    mail.To.Add(new MailAddress(companyMail));
                    mail.Subject = subject;
                    mail.Body = $@"Dear {employeeName},<br /><br />
                          You have a new notification from {company},of {branch}.<br /><br />
                          {message}<br /><br />
                          Regards,<br />
                          HR Team";
                    mail.IsBodyHtml = true;

                    smtpClient.Send(mail);
                }

                InsertNotification(company, branch, department, employeeName, Convert.ToInt32(empId), subject, message, DateTime.Now, "False", connstrg);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void InsertNotification(string company, string branch, string department, string employeeName, int empId, string subject, string message, DateTime date, string status, string connstrg)
        {
            try
            {
                using (SqlConnection sqlConn = new SqlConnection(connstrg))
                {
                    using (SqlCommand sqlCmd = new SqlCommand("INSERT INTO Notifications (Company, Branch, Department, Employees, EmpId, Subject, Message, Date, Status) VALUES (@Company, @Branch, @Department, @Employees, @EmpId, @Subject, @Message, @Date, @Status)", sqlConn))
                    {
                        sqlCmd.Parameters.AddWithValue("@Company", company);
                        sqlCmd.Parameters.AddWithValue("@Branch", branch);
                        sqlCmd.Parameters.AddWithValue("@Department", department);
                        sqlCmd.Parameters.AddWithValue("@Employees", employeeName);
                        sqlCmd.Parameters.AddWithValue("@EmpId", empId);
                        sqlCmd.Parameters.AddWithValue("@Subject", subject);
                        sqlCmd.Parameters.AddWithValue("@Message", message);
                        sqlCmd.Parameters.AddWithValue("@Date", date);
                        sqlCmd.Parameters.AddWithValue("@Status", status);

                        sqlConn.Open();
                        int rowsAffected = sqlCmd.ExecuteNonQuery();
                        sqlConn.Close();
                        if (rowsAffected > 0)
                        {
                            Label1.ForeColor = System.Drawing.Color.Green;
                            Label1.Text = "Notification Send Successfylly....";
                            txtsubject.Text = "";
                            txtmessage.Text = "";
                            ddlcompany.ClearSelection();
                            ddlbranch.ClearSelection();
                            ddldepartment.ClearSelection();
                            ddlname.ClearSelection();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}