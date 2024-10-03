using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Configuration;

namespace Human_Resource_Management
{
    public partial class AdminEmployeeList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               AllEmployeesBind();
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
                        sqlCmd.CommandText = "SELECT Branch_Id,BranchName from Branch ";
                        sqlCmd.Connection = sqlConn;
                        sqlConn.Open();
                        SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                       
                        ddlcompanyname.DataSource = dt;
                        ddlcompanyname.DataValueField = "Branch_Id";
                        ddlcompanyname.DataTextField = "BranchName";
                        ddlcompanyname.DataBind();
                        sqlConn.Close();

                        ddlcompanyname.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Select Branch --", "0"));

                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

       
        public void AllEmployeesBind()
        {
            try
            {
                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    using (SqlCommand sqlcmd = new SqlCommand("SELECT * FROM Employees where Status='1'", connection))
                    {
                        connection.Open();
                        SqlDataAdapter da1 = new SqlDataAdapter(sqlcmd);
                        DataSet ds1 = new DataSet();
                        da1.Fill(ds1);
                        if (ds1.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow row in ds1.Tables[0].Rows)
                            {

                                string salutaion = row["Salutation"].ToString();
                                string Name = row["FirstName"].ToString();
                                string LastName = row["LastName"].ToString();
                                string DeviceCode = row["BranchCode"].ToString();

                                string Email = row["CompanyEmail"].ToString();
                                DateTime dob = Convert.ToDateTime(row["DOB"]);
                                string DOB = dob.ToString("yyyy-MM-dd");

                                string Mobile = row["CompanyCellNo"].ToString();
                                string EmpId = row["EmpId"].ToString();

                                DateTime doj = Convert.ToDateTime(row["DOJ"]);
                                string DOJ = doj.ToString("yyyy-MM-dd");

                                string Salary = row["SalaryAnnum"].ToString();
                                string Company = row["Company"].ToString();
                                string Companyid = row["CompanyId"].ToString();
                                string Branch = row["Branch"].ToString();
                                string Branchid = row["BranchId"].ToString();
                                string Department = row["Department"].ToString();

                                object imageDataObj = row["Image"];
                                byte[] imageData = null;

                                if (imageDataObj != DBNull.Value)
                                {
                                    imageData = (byte[])imageDataObj;
                                }
                                else
                                {
                                    imageData = new byte[0];
                                }

                                string base64String = Convert.ToBase64String(imageData);
                                string imageUrl = "data:image/jpeg;base64," + base64String;
                                // Image2.ImageUrl = "data:image/jpeg;base64," + base64String;
                                string Designation = row["Designation"].ToString();
                                string pfnumber = row["PfNo"].ToString();
                                string esinumber = row["Esino"].ToString();
                                string shift = row["Shift"].ToString();
                                string btid = row["EmployeeId"].ToString();
                                string emptype = row["EmployeType"].ToString();


                                StringBuilder projectHtml = new StringBuilder();
                                projectHtml.Append("<tr>");
                                projectHtml.Append("<td>");
                                projectHtml.Append("<h2 class='table - avatar'>");
                                projectHtml.Append("<a href='#' class='avatar'><img src=" + imageUrl + " alt='User Image'></a>");                               
                                projectHtml.Append("<a href='#'>" + Name + " </a>");
                                projectHtml.Append("</h2>");
                                projectHtml.Append("</td>");
                                projectHtml.Append("<td>");
                                projectHtml.Append("</td>");
                                projectHtml.Append("<td>");
                                projectHtml.Append("</td>");
                                projectHtml.Append("<td>");
                                projectHtml.Append("</td>");

                                projectHtml.Append("<td style='color:blue;'>"+ EmpId + "</td>");
                               // projectHtml.Append("<td>" + Email + "</td>");
                                projectHtml.Append("<td>" + Mobile + "</td>");
                                projectHtml.Append("<td>" + DOJ + "</td>");
                                projectHtml.Append("<td>"+ Department + "</td>");
                              //  projectHtml.Append("<td><a class='dropdown - item' href='#' data-bs-toggle='modal' data-bs-target='#edit_employee'onclick =\"editHoliday('" + salutaion + "','" + Name + "','" + DeviceCode + "','" + Email + "','" + DOB + "','" + Mobile + "','" + EmpId + "','" + DOJ + "','" + Salary + "','" + Branch + "','" + Company + "','" + Department + "','"+ imageUrl + "')\" ><i class='fa-solid fa-pencil m-r-5' style='color:#057a28;'></i></a></td>");
                               // projectHtml.Append("<td><a class='dropdown - item' href='#' data-bs-toggle='modal' data-bs-target='#edit_employee' onclick =\"editHoliday('" + salutaion + "','" + Name + "','" + DeviceCode + "','" + Email + "','" + DOB + "','" + Mobile + "','" + EmpId + "','" + DOJ + "','" + Salary + "','" + Branch + "','" + Company + "','" + Department + "','" + AdminStatus + "','" + ManagerStatus + "','" + EmployeeStatus + "','" + AddEmployeStatus + "','" + EditEmployeStatus + "','" + DeleteEmployeStatus + "','" + AddHolidayStatus + "','" + EditHolidayStatus + "','" + DeleteHolidayStatus + "','" + AddProject + "','" + EditProject + "','" + DeleteProject + "','" + AddLeave + "','" + EditLeave + "','" + DeleteLeave + "','" + AddAssets + "','" + EditAssets + "','" + DeleteAssets + "')\" ><i class='fa-solid fa-pencil m-r-5'></i></a></td>");
                                projectHtml.Append("<td><a class='edit' href='#' data-bs-toggle='modal' data-bs-target='#edit_employee' onclick =\"editdatabind('"+ imageUrl + "','" + salutaion + "','" + Name + "','" + LastName + "','" + DeviceCode + "','" + Email + "','" + DOB + "','" + Mobile + "','" + EmpId + "','" + DOJ + "','" + Salary + "','" + Branch + "','" + Company + "','" + Department + "','" + Designation + "','" + pfnumber + "','" + esinumber + "','" + shift + "','" + btid + "','" + emptype + "')\" ><i class='fa-solid fa-pencil m-r-5'></i></a></td>");

                                projectHtml.Append("<td><a class='dropdown - item' href='#' data-bs-toggle='modal' data-bs-target='#delete_employee'><i class='fa-regular fa-trash-can m-r-5' style='color:#ab0c19;'></i></a></td>");
                              //  projectHtml.Append("</div>");
                               // projectHtml.Append("</div>");
                               // projectHtml.Append("</td>");
                                projectHtml.Append("</tr>");


                                EmployeesData.Controls.Add(new LiteralControl(projectHtml.ToString()));

                               // BindCheckboxesAccess(EmpId);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        protected void txtsearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                String connstrg = ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString;
                using (SqlConnection sqlConn = new SqlConnection(connstrg))
                {
                    using (SqlCommand sqlCmd = new SqlCommand())
                    {
                        sqlCmd.CommandText = "SELECT * from  Employees where EmpId='" + txtsearch.Text.Trim() + "' and status='1' ";
                        sqlCmd.Connection = sqlConn;
                        sqlConn.Open();
                        SqlDataAdapter da1 = new SqlDataAdapter(sqlCmd);
                        DataSet ds1 = new DataSet();
                        da1.Fill(ds1);
                        EmployeesData.Controls.Clear();
                        if (ds1.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow row in ds1.Tables[0].Rows)
                            {

                                string salutaion = row["Salutation"].ToString();
                                string Name = row["FirstName"].ToString();
                                string LastName = row["LastName"].ToString();
                                string DeviceCode = row["BranchCode"].ToString();

                                string Email = row["CompanyEmail"].ToString();

                                DateTime dob = Convert.ToDateTime(row["DOB"]);
                                string DOB = dob.ToString("yyyy-MM-dd");

                                string Mobile = row["CompanyCellNo"].ToString();
                                string EmpId = row["EmpId"].ToString();
                                DateTime doj = Convert.ToDateTime(row["DOJ"]);
                                string DOJ = doj.ToString("yyyy-MM-dd");
                                string Salary = row["SalaryAnnum"].ToString();
                                string Company = row["Company"].ToString();
                                string Companyid = row["CompanyId"].ToString();
                                string Branch = row["Branch"].ToString();
                                string Branchid = row["BranchId"].ToString();
                                string Department = row["Department"].ToString();
                                //byte[] imageData = (byte[])row["Image"];
                                //string base64String = Convert.ToBase64String(imageData);
                                //string imageUrl = "data:image/jpeg;base64," + base64String;
                                // Image2.ImageUrl = "data:image/jpeg;base64," + base64String;

                                object imageDataObj = row["Image"];
                                byte[] imageData = null;

                                if (imageDataObj != DBNull.Value)
                                {
                                    imageData = (byte[])imageDataObj;
                                }
                                else
                                {
                                    imageData = new byte[0];
                                }

                                string base64String = Convert.ToBase64String(imageData);
                                string imageUrl = "data:image/jpeg;base64," + base64String;

                                string Designation = row["Designation"].ToString();
                                string pfnumber = row["PfNo"].ToString();
                                string esinumber = row["Esino"].ToString();
                                string shift = row["Shift"].ToString();
                                string btid = row["EmployeeId"].ToString();
                                string emptype = row["EmployeType"].ToString();


                                StringBuilder projectHtml = new StringBuilder();
                                projectHtml.Append("<tr>");
                                projectHtml.Append("<td>");
                                projectHtml.Append("<h2 class='table - avatar'>");
                                projectHtml.Append("<a href='profile.html' class='avatar'><img src=" + imageUrl + " alt='User Image'></a>");
                                projectHtml.Append("<a href='#'>" + Name + " </a>");
                                projectHtml.Append("</h2>");                                
                                projectHtml.Append("</td>");
                                projectHtml.Append("<td>");
                                projectHtml.Append("</td>");
                                projectHtml.Append("<td>");
                                projectHtml.Append("</td>");
                                projectHtml.Append("<td>");
                                projectHtml.Append("</td>");
                                projectHtml.Append("<td style='color:blue;'>" + EmpId + "</td>");
                                // projectHtml.Append("<td>" + Email + "</td>");
                                projectHtml.Append("<td>" + Mobile + "</td>");
                                projectHtml.Append("<td>" + DOJ + "</td>");
                                projectHtml.Append("<td>" + Department + "</td>");
                                 // projectHtml.Append("<td><a class='dropdown - item' href='#' data-bs-toggle='modal' data-bs-target='#edit_employee' onclick =\"editHoliday('" + salutaion + "','" + Name + "','" + DeviceCode + "','" + Email + "','" + DOB + "','" + Mobile + "','" + EmpId + "','" + DOJ + "','" + Salary + "','" + Branch + "','" + Company + "','" + Department + "','" + AdminStatus + "','" + ManagerStatus + "','" + EmployeeStatus + "','" + AddEmployeStatus + "','" + EditEmployeStatus + "','" + DeleteEmployeStatus + "','" + AddHolidayStatus + "','" + EditHolidayStatus + "','" + DeleteHolidayStatus + "','" + AddProject + "','" + EditProject + "','" + DeleteProject + "','" + AddLeave + "','" + EditLeave + "','" + DeleteLeave + "','" + AddAssets + "','" + EditAssets + "','" + DeleteAssets + "')\" ><i class='fa-solid fa-pencil m-r-5'></i></a></td>");
                                projectHtml.Append("<td><a class='edit' href='#' data-bs-toggle='modal' data-bs-target='#edit_employee' onclick =\"editdatabind('" + imageUrl + "','" + salutaion + "','" + Name + "','"+ LastName + "','" + DeviceCode + "','" + Email + "','" + DOB + "','" + Mobile + "','" + EmpId + "','" + DOJ + "','" + Salary + "','" + Branch + "','" + Company + "','" + Department + "','" + Designation + "','" + pfnumber + "','" + esinumber + "','" + shift + "','" + btid + "','" + emptype + "')\" ><i class='fa-solid fa-pencil m-r-5'></i></a></td>");

                                projectHtml.Append("<td><a class='dropdown - item' href='#' data-bs-toggle='modal' data-bs-target='#delete_employee'><i class='fa-regular fa-trash-can m-r-5' style='color:#ab0c19;'></i></a></td>");
                                //  projectHtml.Append("</div>");
                                // projectHtml.Append("</div>");
                                // projectHtml.Append("</td>");
                                projectHtml.Append("</tr>");


                                EmployeesData.Controls.Add(new LiteralControl(projectHtml.ToString()));

                                // BindCheckboxesAccess(EmpId);
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('No records found.');", true);
                            txtsearch.Text = "";
                            txtempnamesearch.Text = "";
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void txtempnamesearch_TextChanged(object sender, EventArgs e)
        {
            txtsearch.Text = "";
            try
            {
                String connstrg = ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString;
                using (SqlConnection sqlConn = new SqlConnection(connstrg))
                {
                    using (SqlCommand sqlCmd = new SqlCommand())
                    {
                        sqlCmd.CommandText = "SELECT * from  Employees where FirstName LIKE '%' + @EmployeeName + '%' and status='1' ";
                        sqlCmd.Parameters.AddWithValue("@EmployeeName", txtempnamesearch.Text.Trim());
                        sqlCmd.Connection = sqlConn;
                        sqlConn.Open();
                        SqlDataAdapter da1 = new SqlDataAdapter(sqlCmd);
                        DataSet ds1 = new DataSet();
                        da1.Fill(ds1);
                        EmployeesData.Controls.Clear();
                        if (ds1.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow row in ds1.Tables[0].Rows)
                            {

                                string salutaion = row["Salutation"].ToString();
                                string Name = row["FirstName"].ToString();
                                string LastName = row["LastName"].ToString();
                                string DeviceCode = row["BranchCode"].ToString();

                                string Email = row["CompanyEmail"].ToString();
                                DateTime dob = Convert.ToDateTime(row["DOB"]);
                                string DOB = dob.ToString("yyyy-MM-dd");
                                string Mobile = row["CompanyCellNo"].ToString();
                                string EmpId = row["EmpId"].ToString();
                                DateTime doj = Convert.ToDateTime(row["DOJ"]);
                                string DOJ = doj.ToString("yyyy-MM-dd");
                                string Salary = row["SalaryAnnum"].ToString();
                                string Company = row["Company"].ToString();
                
                                string Companyid = row["CompanyId"].ToString();
                            
                                string Branch = row["Branch"].ToString();
                                string Branchid = row["BranchId"].ToString();
                         
                                string Department = row["Department"].ToString();
                                object imageDataObj = row["Image"];
                                byte[] imageData = null;

                                if (imageDataObj != DBNull.Value)
                                {
                                    imageData = (byte[])imageDataObj;
                                }
                                else
                                {
                                    imageData = new byte[0];
                                }

                                string base64String = Convert.ToBase64String(imageData);
                                string imageUrl = "data:image/jpeg;base64," + base64String;
                                // Image2.ImageUrl = "data:image/jpeg;base64," + base64String;
                                string Designation = row["Designation"].ToString();
                                string pfnumber = row["PfNo"].ToString();
                                string esinumber = row["Esino"].ToString();
                                string shift = row["Shift"].ToString();
                                string btid = row["EmployeeId"].ToString();
                                string emptype = row["EmployeType"].ToString();



                                StringBuilder projectHtml = new StringBuilder();
                                projectHtml.Append("<tr>");
                                projectHtml.Append("<td>");
                                projectHtml.Append("<h2 class='table - avatar'>");
                                projectHtml.Append("<a href='profile.html' class='avatar'><img src=" + imageUrl + " alt='User Image'></a>");
                                projectHtml.Append("<a href='#'>" + Name + " </a>");
                                projectHtml.Append("</h2>");
                                projectHtml.Append("</td>");
                                projectHtml.Append("<td>");
                                projectHtml.Append("</td>");
                                projectHtml.Append("<td>");
                                projectHtml.Append("</td>");
                                projectHtml.Append("<td>");
                                projectHtml.Append("</td>");
                                projectHtml.Append("<td style='color:blue;'>" + EmpId + "</td>");
                                // projectHtml.Append("<td>" + Email + "</td>");
                                projectHtml.Append("<td>" + Mobile + "</td>");
                                projectHtml.Append("<td>" + DOJ + "</td>");
                                projectHtml.Append("<td>" + Department + "</td>");
                               //  projectHtml.Append("<td><a class='dropdown - item' href='#' data-bs-toggle='modal' data-bs-target='#edit_employee'onclick =\"editHoliday('" + salutaion + "','" + Name + "','" + DeviceCode + "','" + Email + "','" + DOB + "','" + Mobile + "','" + EmpId + "','" + DOJ + "','" + Salary + "','" + Branch + "','" + Company + "','" + Department + "','"+ imageUrl + "')\" ><i class='fa-solid fa-pencil m-r-5' style='color:#057a28;'></i></a></td>");
                                // projectHtml.Append("<td><a class='dropdown - item' href='#' data-bs-toggle='modal' data-bs-target='#edit_employee' onclick =\"editHoliday('" + salutaion + "','" + Name + "','" + DeviceCode + "','" + Email + "','" + DOB + "','" + Mobile + "','" + EmpId + "','" + DOJ + "','" + Salary + "','" + Branch + "','" + Company + "','" + Department + "','" + AdminStatus + "','" + ManagerStatus + "','" + EmployeeStatus + "','" + AddEmployeStatus + "','" + EditEmployeStatus + "','" + DeleteEmployeStatus + "','" + AddHolidayStatus + "','" + EditHolidayStatus + "','" + DeleteHolidayStatus + "','" + AddProject + "','" + EditProject + "','" + DeleteProject + "','" + AddLeave + "','" + EditLeave + "','" + DeleteLeave + "','" + AddAssets + "','" + EditAssets + "','" + DeleteAssets + "')\" ><i class='fa-solid fa-pencil m-r-5'></i></a></td>");
                                projectHtml.Append("<td><a class='edit' href='#' data-bs-toggle='modal' data-bs-target='#edit_employee' onclick =\"editdatabind('" + imageUrl + "','" + salutaion + "','" + Name + "','"+ LastName + "','" + DeviceCode + "','" + Email + "','" + DOB + "','" + Mobile + "','" + EmpId + "','" + DOJ + "','" + Salary + "','" + Branch + "','" + Company + "','" + Department + "','" + Designation + "','" + pfnumber + "','" + esinumber + "','" + shift + "','" + btid + "','" + emptype + "')\" ><i class='fa-solid fa-pencil m-r-5'></i></a></td>");

                                projectHtml.Append("<td><a class='dropdown - item' href='#' data-bs-toggle='modal' data-bs-target='#delete_employee'><i class='fa-regular fa-trash-can m-r-5' style='color:#ab0c19;'></i></a></td>");
                                //  projectHtml.Append("</div>");
                                // projectHtml.Append("</div>");
                                // projectHtml.Append("</td>");
                                projectHtml.Append("</tr>");


                                EmployeesData.Controls.Add(new LiteralControl(projectHtml.ToString()));

                                // BindCheckboxesAccess(EmpId);
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('No records found.');", true);
                            txtempnamesearch.Text = "";
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void ddlcompanyname_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    using (SqlCommand sqlcmd = new SqlCommand("SELECT * FROM Employees WHERE BranchId = @BranchId and Status='1'", connection))
                    {
                        sqlcmd.Parameters.AddWithValue("@BranchId", ddlcompanyname.SelectedValue.Trim());
                        connection.Open();
                        SqlDataAdapter da1 = new SqlDataAdapter(sqlcmd);
                        DataSet ds1 = new DataSet();
                        da1.Fill(ds1);
                        if (ds1.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow row in ds1.Tables[0].Rows)
                            {

                                string salutaion = row["Salutation"].ToString();
                                string Name = row["FirstName"].ToString();
                                String LastName = row["LastName"].ToString();
                                string DeviceCode = row["BranchCode"].ToString();

                                string Email = row["CompanyEmail"].ToString();
                                DateTime dob = Convert.ToDateTime(row["DOB"]);
                                string DOB = dob.ToString("yyyy-MM-dd");
                                string Mobile = row["CompanyCellNo"].ToString();
                                string EmpId = row["EmpId"].ToString();
                                DateTime doj = Convert.ToDateTime(row["DOJ"]);
                                string DOJ = doj.ToString("yyyy-MM-dd");
                                string Salary = row["SalaryAnnum"].ToString();
                                string Company = row["Company"].ToString();
                     
                                string Companyid = row["CompanyId"].ToString();
                     
                                string Branch = row["Branch"].ToString();
                                string Branchid = row["BranchId"].ToString();
              
                                string Department = row["Department"].ToString();
                                object imageDataObj = row["Image"];
                                byte[] imageData = null;

                                if (imageDataObj != DBNull.Value)
                                {
                                    imageData = (byte[])imageDataObj;
                                }
                                else
                                {
                                    imageData = new byte[0];
                                }

                                string base64String = Convert.ToBase64String(imageData);
                                string imageUrl = "data:image/jpeg;base64," + base64String;
                                // Image2.ImageUrl = "data:image/jpeg;base64," + base64String;
                                string Designation = row["Designation"].ToString();
                                string pfnumber = row["PfNo"].ToString();
                                string esinumber = row["Esino"].ToString();
                                string shift = row["Shift"].ToString();
                                string btid = row["EmployeeId"].ToString();
                                string emptype = row["EmployeType"].ToString();


                                StringBuilder projectHtml = new StringBuilder();
                                projectHtml.Append("<tr>");
                                projectHtml.Append("<td>");
                                projectHtml.Append("<h2 class='table - avatar'>");
                                projectHtml.Append("<a href='profile.html' class='avatar'><img src=" + imageUrl + " alt='User Image'></a>");
                                projectHtml.Append("<a href='profile.html'>" + Name + " </a>");
                                projectHtml.Append("</h2>");
                                projectHtml.Append("</td>");
                                projectHtml.Append("<td>");
                                projectHtml.Append("</td>");
                                projectHtml.Append("<td>");
                                projectHtml.Append("</td>");
                                projectHtml.Append("<td>");
                                projectHtml.Append("</td>");
                                projectHtml.Append("<td style='color:blue;'>" + EmpId + "</td>");
                                // projectHtml.Append("<td>" + Email + "</td>");
                                projectHtml.Append("<td>" + Mobile + "</td>");
                                projectHtml.Append("<td>" + DOJ + "</td>");
                                projectHtml.Append("<td>" + Department + "</td>");
                                 // projectHtml.Append("<td><a class='dropdown - item' href='#' data-bs-toggle='modal' data-bs-target='#edit_employee' onclick =\"editHoliday('" + salutaion + "','" + Name + "','" + DeviceCode + "','" + Email + "','" + DOB + "','" + Mobile + "','" + EmpId + "','" + DOJ + "','" + Salary + "','" + Branch + "','" + Company + "','" + Department + "','" + AdminStatus + "','" + ManagerStatus + "','" + EmployeeStatus + "','" + AddEmployeStatus + "','" + EditEmployeStatus + "','" + DeleteEmployeStatus + "','" + AddHolidayStatus + "','" + EditHolidayStatus + "','" + DeleteHolidayStatus + "','" + AddProject + "','" + EditProject + "','" + DeleteProject + "','" + AddLeave + "','" + EditLeave + "','" + DeleteLeave + "','" + AddAssets + "','" + EditAssets + "','" + DeleteAssets + "')\" ><i class='fa-solid fa-pencil m-r-5'></i></a></td>");
                                projectHtml.Append("<td><a class='edit' href='#' data-bs-toggle='modal' data-bs-target='#edit_employee' onclick =\"editdatabind('" + imageUrl + "','" + salutaion + "','" + Name + "','"+ LastName + "','" + DeviceCode + "','" + Email + "','" + DOB + "','" + Mobile + "','" + EmpId + "','" + DOJ + "','" + Salary + "','" + Branch + "','" + Company + "','" + Department + "','" + Designation + "','" + pfnumber + "','" + esinumber + "','" + shift + "','" + btid + "','" + emptype + "')\" ><i class='fa-solid fa-pencil m-r-5'></i></a></td>");

                                projectHtml.Append("<td><a class='dropdown - item' href='#' data-bs-toggle='modal' data-bs-target='#delete_employee'><i class='fa-regular fa-trash-can m-r-5' style='color:#ab0c19;'></i></a></td>");
                                //  projectHtml.Append("</div>");
                                // projectHtml.Append("</div>");
                                // projectHtml.Append("</td>");
                                projectHtml.Append("</tr>");


                                EmployeesData.Controls.Add(new LiteralControl(projectHtml.ToString()));

                                // BindCheckboxesAccess(EmpId);
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('No records found.');", true);
                        }

                        // Check if the selected value exists in the DropDownList items before setting it
                        //if (ddlcompanyname.Items.FindByValue(ddlcompany.SelectedItem.Value) != null)
                        //{
                        //    ddlcompanyname.SelectedValue = ddlcompany.SelectedItem.Value;
                        //}
                        //else
                        //{
                        //    // Handle the case where the selected value doesn't exist in the DropDownList items
                        //    // For example, you can set it to an empty string or choose a default value
                        //    ddlcompanyname.SelectedValue = "";
                        //}
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        protected void btmsave1_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    connection.Open();
                    SqlCommand command2 = new SqlCommand("UPDATE Employees SET Salutation=@Salutation, EmployeName=@EmployeName, CompanyEmail=@CompanyEmail, DOB=@DOB, CellNo=@CellNo, DOJ=@DOJ, Salary=@Salary WHERE EmployeeId = @EmployeeId", connection);
                    command2.Parameters.AddWithValue("@Salutation", ddleditsalutation.SelectedItem);
                    command2.Parameters.AddWithValue("@EmployeName", txtname.Text);
                    command2.Parameters.AddWithValue("@CompanyEmail", txtemail.Text);
                    command2.Parameters.AddWithValue("@DOB", txtdob.Text);
                    command2.Parameters.AddWithValue("@CellNo", txtmobile.Text);
                    command2.Parameters.AddWithValue("@DOJ", txtjoindate.Text);
                    command2.Parameters.AddWithValue("@Salary", txtsalary.Text);
                    command2.Parameters.AddWithValue("@EmployeeId", txtempid.Text);

                    int i = command2.ExecuteNonQuery();
                    if (i > 0)
                    {
                        Response.Write("Employee data updated successfully.");
                        AllEmployeesBind();
                    }
                    else
                    {
                        Response.Write("Failed to update employee data.");
                        AllEmployeesBind();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        protected void btndelete_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    connection.Open();
                    SqlCommand command2 = new SqlCommand("UPDATE Employees SET status='0' WHERE EmployeeId = @EmployeeId", connection);
                    command2.Parameters.AddWithValue("@EmployeeId", txtempid.Text);
                    int i = command2.ExecuteNonQuery();
                    if (i > 0)
                    {
                        Response.Write("Employee Deleted Successfully.");
                        AllEmployeesBind();
                    }
                    else
                    {
                        Response.Write("Failed to delete employee.");
                        AllEmployeesBind();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private bool ConvertToBoolean(object value)
        {
            return value != null && value != DBNull.Value && bool.TryParse(value.ToString(), out bool result) && result;
        }

        protected void btnupdateemployee_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("UPDATE Employees SET Salutation=@Salutation,FirstName=@FirstName,LastName=@LastName,CompanyEmail=@CompanyEmail,DOB=@DOB,DOJ=@DOJ,CompanyCellNo=@CompanyCellNo,SalaryAnnum=@SalaryAnnum,PfNo=@PfNo,Esino=@Esino,EmployeType=@EmployeType WHERE EmpId = @EmpId", connection);
                    cmd.Parameters.AddWithValue("@Salutation", ddleditsalutation.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@FirstName", txtname.Text);
                    cmd.Parameters.AddWithValue("@LastName", txtlastname.Text);
                    cmd.Parameters.AddWithValue("@CompanyEmail", txtemail.Text);
                    cmd.Parameters.AddWithValue("@DOB", txtdob.Text);
                    cmd.Parameters.AddWithValue("@DOJ", txtjoindate.Text);
                    cmd.Parameters.AddWithValue("@CompanyCellNo", txtmobile.Text);
                    cmd.Parameters.AddWithValue("@SalaryAnnum", txtsalary.Text);
                    cmd.Parameters.AddWithValue("@PfNo", txtpfnumber.Text);
                    cmd.Parameters.AddWithValue("@Esino", txtesinumber.Text);
                    cmd.Parameters.AddWithValue("@EmployeType", ddleditemptype.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@EmpId", hfShowModal.Value);
                    int i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        Response.Write("<script>alert('"+ txtname.Text + " Updated Successfully...')</script>");
                        ddleditsalutation.ClearSelection();
                        txtname.Text = "";
                        txtemail.Text = "";
                        txtdob.Text = "";
                        txtjoindate.Text = "";
                        txtmobile.Text = "";
                        txtpfnumber.Text = "";
                        txtesinumber.Text = "";
                        AllEmployeesBind();
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