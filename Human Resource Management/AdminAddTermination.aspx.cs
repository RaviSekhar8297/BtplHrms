using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DocumentFormat.OpenXml.Wordprocessing;

namespace Human_Resource_Management
{
    public partial class AdminAddTermination : System.Web.UI.Page
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
                        sqlCmd.CommandText = "SELECT CompanyId,CompanyName FROM Companies ";
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

                        ddlcompany.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Select Company --", ""));
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
                        sqlCmd.CommandText = "select Branch_Id, BranchName, BranchCode from Branch where [CompanyId] = '" + ddlcompany.SelectedValue + "' ";
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
                        ddldepartment.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Select department --", "0"));
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

                String connstrg = ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString;
                using (SqlConnection sqlConn = new SqlConnection(connstrg))
                {
                    using (SqlCommand sqlCmd = new SqlCommand())
                    {
                        sqlCmd.CommandText = "select * from [Employees]  where status= 1  and DepartmentId= '" + ddldepartment.SelectedValue + "'";
                        sqlCmd.Connection = sqlConn;
                        sqlConn.Open();
                        SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        ddlname.DataSource = dt;
                        ddlname.DataValueField = "EmpId";
                        ddlname.DataTextField = "LastName";
                        ddlname.DataBind();
                        sqlConn.Close();
                        ddlname.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Select Name --", "0"));
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void ddlname_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                String connstrg = ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString;
                using (SqlConnection sqlConn = new SqlConnection(connstrg))
                {
                    string query = "SELECT * FROM [Employees] WHERE status = 1 AND EmpId = @EmpId";
                    using (SqlCommand sqlCmd = new SqlCommand(query, sqlConn))
                    {
                        sqlCmd.Parameters.AddWithValue("@EmpId", ddlname.SelectedValue);

                        sqlConn.Open();
                        using (SqlDataReader sdr = sqlCmd.ExecuteReader())
                        {
                            if (sdr.Read())
                            {
                                TextBox1.Text = sdr["Designation"].ToString();
                                TextBox2.Text = sdr["EmpId"].ToString();
                            }
                            sdr.Close();
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

        protected void btntermination_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection strcon = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    strcon.Open();
                    string empId = ddlname.SelectedValue;

                    // Begin transaction
                    SqlTransaction transaction = strcon.BeginTransaction();

                    try
                    {
                        // Fetch employee details
                        SqlCommand selectCmd = new SqlCommand("SELECT * FROM [Employees] WHERE status = 1 AND EmpId = @EmpId", strcon, transaction);
                        selectCmd.Parameters.AddWithValue("@EmpId", empId);

                        using (SqlDataReader sdr = selectCmd.ExecuteReader())
                        {
                            if (sdr.Read())
                            {
                                string empName = ddlname.SelectedItem.Text; 

                                if (!string.IsNullOrEmpty(empName) && empName != "0")
                                {
                                    sdr.Close();

                                    // Insert into TerminationEmployees
                                    SqlCommand cmdTermination = new SqlCommand(@"INSERT INTO TerminationEmployees (EmpId, Name, Company, Branch, Department, Designation, TerminationType, TerminationDate, Reason) 
                                                                    VALUES (@EmpId, @Name, @Company, @Branch, @Department, @Designation, @TerminationType, @TerminationDate, @Reason)", strcon, transaction);
                                    cmdTermination.Parameters.AddWithValue("@EmpId", empId);
                                    cmdTermination.Parameters.AddWithValue("@Name", empName);
                                    cmdTermination.Parameters.AddWithValue("@Company", ddlcompany.SelectedItem.Text);
                                    cmdTermination.Parameters.AddWithValue("@Branch", ddlbranch.SelectedItem.Text);
                                    cmdTermination.Parameters.AddWithValue("@Department", ddldepartment.SelectedItem.Text);
                                    cmdTermination.Parameters.AddWithValue("@Designation", TextBox1.Text);
                                    cmdTermination.Parameters.AddWithValue("@TerminationType", ddlterminationtype.SelectedItem.Text);
                                    cmdTermination.Parameters.AddWithValue("@TerminationDate", DateTime.Now);
                                    cmdTermination.Parameters.AddWithValue("@Reason", TextBox5.Text);

                                    int terminationResult = cmdTermination.ExecuteNonQuery();

                                    // Update Employees table
                                    SqlCommand cmdUpdateEmployee = new SqlCommand(@"UPDATE Employees SET Status = '0', EmpInActiveDate = @EmpInActiveDate WHERE EmpId = @EmpId", strcon, transaction);
                                    cmdUpdateEmployee.Parameters.AddWithValue("@EmpId", empId);
                                    cmdUpdateEmployee.Parameters.AddWithValue("@EmpInActiveDate", DateTime.Now);

                                    int updateResult = cmdUpdateEmployee.ExecuteNonQuery();

                                    SqlCommand cmdUpdatewebloginse = new SqlCommand(@"UPDATE WebLogins SET Status = '0'  WHERE EmpId = @EmpId", strcon, transaction);
                                    cmdUpdatewebloginse.Parameters.AddWithValue("@EmpId", empId);
                                   

                                    int updatewebResult = cmdUpdatewebloginse.ExecuteNonQuery();

                                    if (terminationResult > 0 && updateResult > 0 && updatewebResult > 0)
                                    {
                                        transaction.Commit();
                                        Label1.ForeColor =System.Drawing.Color.Red;
                                        Label1.Text = "Successfully terminated " + empName + ".";
                                        TextBox1.Text = "";
                                        TextBox5.Text = "";
                                        ddlcompany.ClearSelection();
                                        ddlbranch.ClearSelection();
                                        ddldepartment.ClearSelection();
                                        ddlname.ClearSelection();
                                    }
                                    else
                                    {
                                        transaction.Rollback();
                                        Response.Write("<script>alert('Termination failed.')</script>");
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
                    catch (Exception ex)
                    {
                        // Rollback transaction on exception
                        transaction.Rollback();
                        Response.Write("<script>alert('Error: " + ex.Message + "')</script>");
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error: " + ex.Message + "')</script>");
            }

        }
    }
}