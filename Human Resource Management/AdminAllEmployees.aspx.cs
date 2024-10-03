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
using System.Data.SqlTypes;
using Org.BouncyCastle.Ocsp;
using System.IO;
using DocumentFormat.OpenXml.Bibliography;

namespace Human_Resource_Management
{
    public partial class AdminAllEmployees : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
                AllEmployeesBind();
                BindCompany2();              
            }           
        }      
      
        public void BindCompany2()
        {
            try
            {
                using (SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    using (SqlCommand sqlCmd = new SqlCommand())
                    {
                        sqlCmd.CommandText = "select Branch_Id,BranchName from Branch";
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

                                //byte[] imageData = (byte[])row["Image"];
                                //string base64String = Convert.ToBase64String(imageData);
                                //string imageUrl = "data:image/jpeg;base64," + base64String;

                                string Designation = row["Designation"].ToString();
                                string pfnumber = row["PfNo"].ToString();
                                string esinumber = row["Esino"].ToString();
                                string shift = row["Shift"].ToString();
                                string btid = row["EmployeeId"].ToString();
                                string emptype = row["EmployeType"].ToString();


                                StringBuilder projectHtml = new StringBuilder();

                                projectHtml.Append("<div class='col-md-4 col-sm-6 col-12 col-lg-4 col-xl-3'>");
                                projectHtml.Append("<div class='profile - widget divbox' style='border:1px solid red;'>");
                                projectHtml.Append("<div class='profile - img'>");
                                projectHtml.Append("<a href='profile.html' class='avatar'><img src=" + imageUrl + " alt='User Image'></a>");
                                projectHtml.Append("</div>");
                                projectHtml.Append("<div class='dropdown profile - action' thredots style='border:1px solid green;'>");
                                projectHtml.Append("<a href='#' class='action-icon dropdown-toggle' data-bs-toggle='dropdown' id='dropdownMenuButton' aria-expanded='false'><i class='material-icons'>more_vert</i></a>");
                                projectHtml.Append("</div>");
                                projectHtml.Append("<h4 class='user - name m - t - 10 mb - 0 text - ellipsis Name'><a href='profile.html'>" + Name + "</a></h4>");
                                projectHtml.Append("<div class='small text - muted designation'>" + Branch + "</div>");
                                projectHtml.Append("<div class='dropdown - menu dropdown - menu - right editdelete' id='drpmenu'>");
                                projectHtml.Append("<a class='edit' href='#' data-bs-toggle='modal' data-bs-target='#edit_employee' onclick =\"editdatabind('"+ imageUrl + "','" + salutaion + "','" + Name + "','" + DeviceCode + "','" + Email + "','" + DOB + "','" + Mobile + "','" + EmpId + "','" + DOJ + "','" + Salary + "','" + Branch + "','" + Company + "','" + Department + "','" + Designation + "','" + pfnumber + "','" + esinumber + "','" + shift + "','" + btid + "','" + emptype + "')\" ><i class='fa-solid fa-pencil m-r-5'></i></a>");
                                projectHtml.Append("<a class='delete ' href='#' data-bs-toggle='modal' data-bs-target='#delete_employee'  onclick =\"deleteemp('" + EmpId + "')\"><i class='fa-regular fa-trash-can m-r-5'></i></a>");
                                projectHtml.Append("</div>");
                                projectHtml.Append("</div>");
                                projectHtml.Append("</div>");


                                //   dot code 

                                //projectHtml.Append("<div class='col-md-4 col-sm-6 col-12 col-lg-4 col-xl-3'>");
                                //projectHtml.Append("<div class='profile-widget divbox' style='border:1px solid red;'>");
                                //projectHtml.Append("<div class='profile-img'>");
                                //projectHtml.Append("<a href='profile.html' class='avatar'><img src='" + imageUrl + "' alt='User Image'></a>");
                                //projectHtml.Append("</div>");
                                //projectHtml.Append("<div class='dropdown profile-action' style='border:1px solid green;'>");
                                //projectHtml.Append("<a href='#' class='action-icon dropdown-toggle' data-bs-toggle='dropdown' aria-expanded='false'><i class='material-icons'>more_vert</i></a>");
                                //projectHtml.Append("<div class='dropdown-menu dropdown-menu-right'>"); // Correct placement of the dropdown menu
                                //projectHtml.Append("<a class='dropdown-item' href='#' data-bs-toggle='modal' data-bs-target='#edit_employee' onclick =\"editdatabind('" + imageUrl + "','" + salutaion + "','" + Name + "','" + DeviceCode + "','" + Email + "','" + DOB + "','" + Mobile + "','" + EmpId + "','" + DOJ + "','" + Salary + "','" + Branch + "','" + Company + "','" + Department + "','" + Designation + "','" + pfnumber + "','" + esinumber + "','" + shift + "','" + btid + "','" + emptype + "')\"><i class='fa-solid fa-pencil m-r-5'></i> Edit</a>");
                                //projectHtml.Append("<a class='dropdown-item' href='#' data-bs-toggle='modal' data-bs-target='#delete_employee' onclick =\"deleteemp('" + EmpId + "')\"><i class='fa-regular fa-trash-can m-r-5'></i> Delete</a>");
                                //projectHtml.Append("</div>"); // Closing dropdown-menu
                                //projectHtml.Append("</div>"); // Closing dropdown
                                //projectHtml.Append("<h4 class='user-name m-t-10 mb-0 text-ellipsis Name'><a href='profile.html'>" + Name + "</a></h4>");
                                //projectHtml.Append("<div class='small text-muted designation'>" + Branch + "</div>");
                                //projectHtml.Append("</div>"); // Closing profile-widget
                                //projectHtml.Append("</div>");

                                //

                                EmployeesData.Controls.Add(new LiteralControl(projectHtml.ToString()));

                               // BindCheckboxesAccess(EmpId);
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
                                string Designation = row["Designation"].ToString();
                                string pfnumber = row["PfNo"].ToString();
                                string esinumber = row["Esino"].ToString();
                                string shift = row["Shift"].ToString();
                                string btid = row["EmployeeId"].ToString();
                                string emptype = row["EmployeType"].ToString();


                                StringBuilder projectHtml = new StringBuilder();

                                projectHtml.Append("<div class='col-md-4 col-sm-6 col-12 col-lg-4 col-xl-3'>");
                                projectHtml.Append("<div class='profile - widget divbox' style='border:1px solid red;'>");
                                projectHtml.Append("<div class='profile - img'>");
                                projectHtml.Append("<a href='profile.html' class='avatar'><img src=" + imageUrl + " alt='User Image'></a>");
                                projectHtml.Append("</div>");
                                projectHtml.Append("<div class='dropdown profile - action' thredots style='border:1px solid green;'>");
                                projectHtml.Append("<a href='#' class='action-icon dropdown-toggle' data-bs-toggle='dropdown' id='dropdownMenuButton' aria-expanded='false'><i class='material-icons'>more_vert</i></a>");
                                projectHtml.Append("</div>");
                                projectHtml.Append("<h4 class='user - name m - t - 10 mb - 0 text - ellipsis Name'><a href='profile.html'>" + Name + "</a></h4>");
                                projectHtml.Append("<div class='small text - muted designation'>" + Branch + "</div>");
                                projectHtml.Append("<div class='dropdown - menu dropdown - menu - right editdelete' id='drpmenu'>");
                                projectHtml.Append("<a class='edit' href='#' data-bs-toggle='modal' data-bs-target='#edit_employee' onclick =\"editdatabind('" + imageUrl + "','" + salutaion + "','" + Name + "','" + DeviceCode + "','" + Email + "','" + DOB + "','" + Mobile + "','" + EmpId + "','" + DOJ + "','" + Salary + "','" + Branch + "','" + Company + "','" + Department + "','" + Designation + "','" + pfnumber + "','" + esinumber + "','" + shift + "','" + btid + "','" + emptype + "')\" ><i class='fa-solid fa-pencil m-r-5'></i></a>");
                                projectHtml.Append("<a class='delete ' href='#' data-bs-toggle='modal' data-bs-target='#delete_employee'  onclick =\"deleteemp('" + EmpId + "')\"><i class='fa-regular fa-trash-can m-r-5'></i></a>");
                                projectHtml.Append("</div>");
                                projectHtml.Append("</div>");
                                projectHtml.Append("</div>");

                                EmployeesData.Controls.Add(new LiteralControl(projectHtml.ToString()));
                                txtempnamesearch.Text = "";
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
                        sqlCmd.CommandText = "SELECT * from  Employees where Status = '1' AND FirstName LIKE @SearchName "; 
                        sqlCmd.Parameters.AddWithValue("@SearchName", "%" + txtempnamesearch.Text.Trim() + "%");
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
                                string Designation = row["Designation"].ToString();
                                string pfnumber = row["PfNo"].ToString();
                                string esinumber = row["Esino"].ToString();
                                string shift = row["Shift"].ToString();
                                string btid = row["EmployeeId"].ToString();
                                string emptype = row["EmployeType"].ToString();


                                StringBuilder projectHtml = new StringBuilder();

                                projectHtml.Append("<div class='col-md-4 col-sm-6 col-12 col-lg-4 col-xl-3'>");
                                projectHtml.Append("<div class='profile - widget divbox' style='border:1px solid red;'>");
                                projectHtml.Append("<div class='profile - img'>");
                                projectHtml.Append("<a href='profile.html' class='avatar'><img src=" + imageUrl + " alt='User Image'></a>");
                                projectHtml.Append("</div>");
                                projectHtml.Append("<div class='dropdown profile - action' thredots style='border:1px solid green;'>");
                                projectHtml.Append("<a href='#' class='action-icon dropdown-toggle' data-bs-toggle='dropdown' id='dropdownMenuButton' aria-expanded='false'><i class='material-icons'>more_vert</i></a>");
                                projectHtml.Append("</div>");
                                projectHtml.Append("<h4 class='user - name m - t - 10 mb - 0 text - ellipsis Name'><a href='profile.html'>" + Name + "</a></h4>");
                                projectHtml.Append("<div class='small text - muted designation'>" + Branch + "</div>");
                                projectHtml.Append("<div class='dropdown - menu dropdown - menu - right editdelete' id='drpmenu'>");
                                projectHtml.Append("<a class='edit' href='#' data-bs-toggle='modal' data-bs-target='#edit_employee' onclick =\"editdatabind('" + imageUrl + "','" + salutaion + "','" + Name + "','" + DeviceCode + "','" + Email + "','" + DOB + "','" + Mobile + "','" + EmpId + "','" + DOJ + "','" + Salary + "','" + Branch + "','" + Company + "','" + Department + "','" + Designation + "','" + pfnumber + "','" + esinumber + "','" + shift + "','" + btid + "','" + emptype + "')\" ><i class='fa-solid fa-pencil m-r-5'></i></a>");
                                projectHtml.Append("<a class='delete ' href='#' data-bs-toggle='modal' data-bs-target='#delete_employee'  onclick =\"deleteemp('" + EmpId + "')\"><i class='fa-regular fa-trash-can m-r-5'></i></a>");
                                projectHtml.Append("</div>");
                                projectHtml.Append("</div>");
                                projectHtml.Append("</div>");

                                EmployeesData.Controls.Add(new LiteralControl(projectHtml.ToString()));
                                txtsearch.Text = "";
                                // BindCheckboxesAccess(EmpId);
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('No records found.');", true);
                            txtempnamesearch.Text = "";
                            AllEmployeesBind();
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
                if (ddlcompanyname.SelectedItem.Text != null)
                {
                    using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                    {
                        using (SqlCommand sqlcmd = new SqlCommand("SELECT * FROM Employees WHERE BranchId= @BranchId and Status='1'", connection))
                        {
                            sqlcmd.Parameters.AddWithValue("@BranchId", ddlcompanyname.SelectedValue.Trim());

                            SqlDataAdapter da1 = new SqlDataAdapter(sqlcmd);
                            DataSet ds1 = new DataSet();
                            da1.Fill(ds1);

                            if (ds1.Tables[0].Rows.Count > 0)
                            {
                                foreach (DataRow row in ds1.Tables[0].Rows)
                                {
                                    string salutaion = row["Salutation"].ToString();
                                    string Name = row["FirstName"].ToString();
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
                                    string Designation = row["Designation"].ToString();
                                    string pfnumber = row["PfNo"].ToString();
                                    string esinumber = row["Esino"].ToString();
                                    string shift = row["Shift"].ToString();
                                    string btid = row["EmployeeId"].ToString();
                                    string emptype = row["EmployeType"].ToString();


                                    StringBuilder projectHtml = new StringBuilder();

                                    projectHtml.Append("<div class='col-md-4 col-sm-6 col-12 col-lg-4 col-xl-3'>");
                                    projectHtml.Append("<div class='profile - widget divbox' style='border:1px solid red;'>");
                                    projectHtml.Append("<div class='profile - img'>");
                                    projectHtml.Append("<a href='profile.html' class='avatar'><img src=" + imageUrl + " alt='User Image'></a>");
                                    projectHtml.Append("</div>");
                                    projectHtml.Append("<div class='dropdown profile - action' thredots style='border:1px solid green;'>");
                                    projectHtml.Append("<a href='#' class='action-icon dropdown-toggle' data-bs-toggle='dropdown' id='dropdownMenuButton' aria-expanded='false'><i class='material-icons'>more_vert</i></a>");
                                    projectHtml.Append("</div>");
                                    projectHtml.Append("<h4 class='user - name m - t - 10 mb - 0 text - ellipsis Name'><a href='profile.html'>" + Name + "</a></h4>");
                                    projectHtml.Append("<div class='small text - muted designation'>" + Branch + "</div>");
                                    projectHtml.Append("<div class='dropdown - menu dropdown - menu - right editdelete' id='drpmenu'>");
                                    projectHtml.Append("<a class='edit' href='#' data-bs-toggle='modal' data-bs-target='#edit_employee' onclick =\"editdatabind('" + imageUrl + "','" + salutaion + "','" + Name + "','" + DeviceCode + "','" + Email + "','" + DOB + "','" + Mobile + "','" + EmpId + "','" + DOJ + "','" + Salary + "','" + Branch + "','" + Company + "','" + Department + "','" + Designation + "','" + pfnumber + "','" + esinumber + "','" + shift + "','" + btid + "','" + emptype + "')\" ><i class='fa-solid fa-pencil m-r-5'></i></a>");
                                    projectHtml.Append("<a class='delete ' href='#' data-bs-toggle='modal' data-bs-target='#delete_employee'  onclick =\"deleteemp('" + EmpId + "')\"><i class='fa-regular fa-trash-can m-r-5'></i></a>");
                                    projectHtml.Append("</div>");
                                    projectHtml.Append("</div>");
                                    projectHtml.Append("</div>");

                                    EmployeesData.Controls.Add(new LiteralControl(projectHtml.ToString()));

                                    // BindCheckboxesAccess(EmpId);
                                }
                            }
                            else
                            {
                                //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('No records found.');", true);
                                //AllEmployeesBind();
                                Response.Write("Selected  " + ddlcompanyname.SelectedItem.Text + "  No records..");
                            }

                            //if (ddlcompanyname.Items.FindByValue(ddlcompany.SelectedItem.Value) != null)
                            //{
                            //    ddlcompanyname.SelectedValue = ddlcompany.SelectedItem.Value;
                            //}
                            //else
                            //{
                            //    ddlcompanyname.SelectedValue = "";
                            //}
                        }
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please select a company.');", true);
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
                    SqlCommand command2 = new SqlCommand("UPDATE Employees SET status='0' WHERE EmpId = @EmployeeId", connection);
                    command2.Parameters.AddWithValue("@EmployeeId", HiddenField1.Value);
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

        protected void btnupdateemployee_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("UPDATE Employees SET Salutation=@Salutation,FirstName=@FirstName,CompanyEmail=@CompanyEmail,DOB=@DOB,DOJ=@DOJ,CompanyCellNo=@CompanyCellNo,SalaryAnnum=@SalaryAnnum,PfNo=@PfNo,Esino=@Esino,EmployeType=@EmployeType WHERE EmpId = @EmpId", connection);
                    cmd.Parameters.AddWithValue("@Salutation", ddleditsalutation.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@FirstName", txtname.Text);
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
                        Response.Write("<script>alert('"+ txtname.Text + " Details Updated Successfully...')</script>");
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
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}