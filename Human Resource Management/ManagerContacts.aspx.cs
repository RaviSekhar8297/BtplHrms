using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web.UI;

namespace Human_Resource_Management
{
    public partial class ManagerContacts : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                AllEmployeesBind();
            }
        }

        public void AllEmployeesBind()
        {
            string departmentName = Session["DepartmentName"].ToString();
            try
            {
                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    using (SqlCommand sqlcmd = new SqlCommand("SELECT * FROM Employees WHERE Status='1' AND Department=@DepartmentName", connection))
                    {
                        sqlcmd.Parameters.AddWithValue("@DepartmentName", departmentName);

                        connection.Open();
                        SqlDataAdapter da1 = new SqlDataAdapter(sqlcmd);
                        DataSet ds1 = new DataSet();
                        da1.Fill(ds1);

                        if (ds1.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow row in ds1.Tables[0].Rows)
                            {
                                string employeeId = row["EmployeeId"].ToString();
                                string salutation = row["Salutation"].ToString();
                                string name = row["FirstName"].ToString();
                                string company = row["Company"].ToString();
                                string branch = row["Branch"].ToString();
                                string department = row["Department"].ToString();
                                string designation = row["Designation"].ToString();
                                string email = row["CompanyEmail"].ToString();
                                string mobile = row["CompanyCellNo"].ToString();
                                string dob = Convert.ToDateTime(row["DOB"]).ToString("yyyy-MM-dd");
                                string doj = Convert.ToDateTime(row["DOJ"]).ToString("yyyy-MM-dd");
                                string salary = row["SalaryAnnum"].ToString();
                                string pfNo = row["PfNo"].ToString();
                                string esiNo = row["Esino"].ToString();
                                string shift = row["Shift"].ToString();
                                string employeeType = row["EmployeType"].ToString();
                                string deviceCode = row["BranchCode"].ToString();

                                byte[] imageData = (byte[])row["Image"];
                                string base64String = Convert.ToBase64String(imageData);
                                string imageUrl = "data:image/jpeg;base64," + base64String;

                                StringBuilder projectHtml = new StringBuilder();
                                projectHtml.Append($@"
                                    <div class='card'>
                                        <div class='edit-icon' onclick=""openEditModal('{employeeId}', '{salutation}', '{name}', '{company}', '{branch}', '{department}', '{designation}', '{email}', '{mobile}', '{dob}', '{doj}', '{salary}', '{pfNo}', '{esiNo}', '{shift}', '{employeeType}')"">
                                            <i class='fa-solid fa-pencil' style='color: green;'></i>
                                        </div>
                                        <div class='card-body'>
                                            <div class='first-col'>
                                                <img src='{imageUrl}' alt='image' class='img' />
                                            </div>
                                            <div class='second-col'>
                                                <h4 style='color:#ab91b3'>Company Details</h4>
                                                <p><strong></strong> {company}</p>
                                                <p><strong></strong> {branch}</p>                                              
                                                <p><strong></strong> {department}</p>
                                                <p><strong></strong> {designation}</p>
                                            </div>
                                            <div class='third-col'>
                                                <h4 style='color:#ab91b3'>Personal Details</h4>
                                                <p><strong></strong> {salutation} {name}</p>
                                                <p><strong></strong> {email}</p>
                                                <p><strong></strong> {doj}</p>
                                                <p><strong></strong> {mobile}</p>                                      
                                            </div>
                                            <div class='fourth-col'>
                                                <h4 style='color:#ab91b3'>Salary Details</h4>
                                                <p><strong></strong> {employeeType}</p>
                                                <p><strong></strong> {salary}</p>                                               
                                                <p><strong></strong> {pfNo}</p>
                                                <p><strong></strong> {esiNo}</p>                                       
                                            </div>
                                        </div>
                                    </div>");

                                EmployeesData.Controls.Add(new LiteralControl(projectHtml.ToString()));
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('No records found.');", true);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", $"alert('An error occurred while fetching data: {ex.Message}');", true);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    using (SqlCommand sqlcmd = new SqlCommand("UPDATE Employees SET Salutation=@Salutation, FirstName=@Name, Company=@Company, Branch=@Branch, Department=@Department, Designation=@Designation, CompanyEmail=@Email, CompanyCellNo=@Mobile, DOB=@DOB, DOJ=@DOJ, SalaryAnnum=@Salary, PfNo=@PFNo, Esino=@ESINo, Shift=@Shift, EmployeType=@EmployeeType WHERE EmployeeId=@EmployeeId", connection))
                    {
                        sqlcmd.Parameters.AddWithValue("@Salutation", txtSalutation.Text);
                        sqlcmd.Parameters.AddWithValue("@Name", txtName.Text);
                        sqlcmd.Parameters.AddWithValue("@Company", txtCompany.Text);
                        sqlcmd.Parameters.AddWithValue("@Branch", txtBranch.Text);
                        sqlcmd.Parameters.AddWithValue("@Department", txtDepartment.Text);
                        sqlcmd.Parameters.AddWithValue("@Designation", txtDesignation.Text);
                        sqlcmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                        sqlcmd.Parameters.AddWithValue("@Mobile", txtMobile.Text);
                        sqlcmd.Parameters.AddWithValue("@DOB", txtDOB.Text);
                        sqlcmd.Parameters.AddWithValue("@DOJ", txtDOJ.Text);
                        sqlcmd.Parameters.AddWithValue("@Salary", txtSalary.Text);
                        sqlcmd.Parameters.AddWithValue("@PFNo", txtPFNo.Text);
                        sqlcmd.Parameters.AddWithValue("@ESINo", txtESINo.Text);
                        sqlcmd.Parameters.AddWithValue("@Shift", txtShift.Text);
                        sqlcmd.Parameters.AddWithValue("@EmployeeType", txtEmployeeType.Text);
                        sqlcmd.Parameters.AddWithValue("@EmployeeId", txtEmployeeId.Text);

                        connection.Open();
                        sqlcmd.ExecuteNonQuery();
                    }
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Employee details updated successfully.'); window.location='ManagerContacts.aspx';", true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", $"alert('An error occurred while updating data: {ex.Message}');", true);
            }
        }
    }
}
