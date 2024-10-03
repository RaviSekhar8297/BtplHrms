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
    public partial class AdminDepartments : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindDepartment();
                BindBranch();
            }
        }

        public void BindDepartment(string branchId = null)
        {
            try
            {
                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    // Base query to select departments
                    string query = "SELECT * FROM department WHERE SN = '1'";

                    // Conditionally add the branch filter if a specific branch is selected
                    if (!string.IsNullOrEmpty(branchId) && branchId != "0")  // "0" is the value for "All"
                    {
                        query += " AND BranchId = @BranchId";
                    }

                    using (SqlCommand sqlcmd = new SqlCommand(query, connection))
                    {
                        if (!string.IsNullOrEmpty(branchId) && branchId != "0")
                        {
                            sqlcmd.Parameters.AddWithValue("@BranchId", branchId);
                        }

                        connection.Open();
                        SqlDataAdapter da1 = new SqlDataAdapter(sqlcmd);
                        DataSet ds1 = new DataSet();
                        da1.Fill(ds1);

                        // Clear any previous data in DepartmentData control
                        DepartmentData.Controls.Clear();

                        if (ds1.Tables[0].Rows.Count > 0)
                        {
                            int S_No = 1;
                            foreach (DataRow row in ds1.Tables[0].Rows)
                            {
                                string DeptId = row["DeptId"].ToString();
                                string Department = row["Department"].ToString();
                                string CreatedBy = row["CreatedBy"] == DBNull.Value ? string.Empty : row["CreatedBy"].ToString();
                                DateTime? cdob = row["CreatedDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(row["CreatedDate"]);
                                string CreatedDate = cdob.HasValue ? cdob.Value.ToString("yyyy-MM-dd") : string.Empty;
                                string companyid = row["CompanyId"].ToString();
                                string branchid = row["BranchId"].ToString();
                                string BranchCode = row["BranchCode"].ToString();
                                string updatedby = row["UpdatedBy"] == DBNull.Value ? string.Empty : row["UpdatedBy"].ToString();
                                DateTime? updob = row["UpdatedDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(row["UpdatedDate"]);
                                string UpdatedDate = updob.HasValue ? updob.Value.ToString("yyyy-MM-dd") : string.Empty;

                                // Build the HTML structure
                                StringBuilder projectHtml = new StringBuilder();
                                projectHtml.Append("<tr>");
                                projectHtml.Append("<td>" + S_No + "</td>");
                                projectHtml.Append("<td>" + DeptId + "</td>");
                                projectHtml.Append("<td>" + Department + "</td>");
                                projectHtml.Append("<td>" + companyid + "</td>");
                                projectHtml.Append("<td><a class='edit' href='#' data-bs-toggle='modal' data-bs-target='#edit_department' onclick =\"editDepartment('" + DeptId + "','" + Department + "','" + CreatedBy + "','" + CreatedDate + "','" + companyid + "','" + branchid + "','" + BranchCode + "','" + updatedby + "','" + UpdatedDate + "')\" ><i class='fa-solid fa-pencil m-r-5' style='color:green;'></i></a></td>");
                                projectHtml.Append("<td><a class='delete' href='#' data-bs-toggle='modal' data-bs-target='#delete_department' onclick =\"deletedept('" + DeptId + "','" + Department + "')\"><i class='fa-regular fa-trash-can m-r-5' style='color:red;'></i></a></td>");
                                projectHtml.Append("</tr>");

                                DepartmentData.Controls.Add(new LiteralControl(projectHtml.ToString()));
                                S_No++;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error: " + ex.Message);
            }
        }



        public void BindBranch()
        {
            try
            {
                using (SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    using (SqlCommand sqlCmd = new SqlCommand())
                    {
                        // Corrected SQL query to get distinct department names
                        sqlCmd.CommandText = "SELECT Branch_Id,BranchName FROM Branch";
                        sqlCmd.Connection = sqlConn;
                        sqlConn.Open();
                        SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        // Bind the DropDownList
                        DropDownList4.DataSource = dt;
                        DropDownList4.DataValueField = "Branch_Id";
                        DropDownList4.DataTextField = "BranchName";
                        DropDownList4.DataBind();
                        sqlConn.Close();

                        DropDownList4.Items.Insert(0, new System.Web.UI.WebControls.ListItem("ALL", "0"));
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception and optionally display a user-friendly message
                System.Diagnostics.Debug.WriteLine("Error: " + ex.Message);
            }


        }

        protected void btndeletedepartment_Click(object sender, EventArgs e)
        {
            try
            {
                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    using (SqlCommand sqlcmd = new SqlCommand("UPDATE Department SET  SN='0' WHERE DeptId=@DeptId", connection))
                    {
                        connection.Open();
                        sqlcmd.Parameters.AddWithValue("@DeptId", HiddenField1.Value.Trim());
                        int i = sqlcmd.ExecuteNonQuery();
                        if (i > 0)
                        {
                            Response.Write("<script>alert('Deleted  Successfully..')</script>");
                            BindDepartment();
                        }
                        else
                        {
                            Response.Write("<script>alert('Not Deleted..')</script>");
                            BindDepartment();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnupdatedepartment_Click(object sender, EventArgs e)
        {
            DateTime currentDateTime = DateTime.Now;
            string formattedDateTime = currentDateTime.ToString("yyyy-MM-dd HH:mm:ss");

            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString;

                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string updateDepartmentQuery = "UPDATE Department SET Department=@Department, UpdatedBy=@UpdatedBy, UpdatedDate=@UpdatedDate WHERE DeptId=@DeptId";
                    using (SqlCommand sqlcmdDept = new SqlCommand(updateDepartmentQuery, connection))
                    {
                        sqlcmdDept.Parameters.AddWithValue("@DeptId", Convert.ToInt32(txtdeptid.Text.Trim()));
                        sqlcmdDept.Parameters.AddWithValue("@Department", txtdeptname.Text.Trim());
                        sqlcmdDept.Parameters.AddWithValue("@UpdatedBy", Session["Name"].ToString());
                        sqlcmdDept.Parameters.AddWithValue("@UpdatedDate", formattedDateTime);

                        int rowsAffectedDept = sqlcmdDept.ExecuteNonQuery();
                        if (rowsAffectedDept > 0)
                        {
                            Response.Write("<script>alert('Department Updated Successfully.')</script>");
                            BindDepartment();
                        }
                        else
                        {
                            Response.Write("<script>alert('Department Not Updated.')</script>");
                            BindDepartment();
                        }
                    }

                    string updateEmployeesQuery = "UPDATE Employees SET Department=@DepartmentName WHERE DepartmentId=@DeptId";
                    using (SqlCommand sqlcmdEmployees = new SqlCommand(updateEmployeesQuery, connection))
                    {
                        sqlcmdEmployees.Parameters.AddWithValue("@DeptId", Convert.ToInt32(txtdeptid.Text.Trim()));
                        sqlcmdEmployees.Parameters.AddWithValue("@DepartmentName", txtdeptname.Text.Trim());

                        int rowsAffectedEmployees = sqlcmdEmployees.ExecuteNonQuery();
                    }

                    // Update WebLogins table
                    string updateWebLoginsQuery = "UPDATE WebLogins SET DepartmentName=@DepartmentName WHERE DepartmentId=@DeptId";
                    using (SqlCommand sqlcmdWebLogins = new SqlCommand(updateWebLoginsQuery, connection))
                    {
                        sqlcmdWebLogins.Parameters.AddWithValue("@DeptId", Convert.ToInt32(txtdeptid.Text.Trim()));
                        sqlcmdWebLogins.Parameters.AddWithValue("@DepartmentName", txtdeptname.Text.Trim());

                        int rowsAffectedWebLogins = sqlcmdWebLogins.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions appropriately
                Response.Write("<script>alert('Error: " + ex.Message + "')</script>");
            }

        }

        protected void DropDownList4_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedBranch = DropDownList4.SelectedValue; 
            BindDepartment(selectedBranch);
        }

    }
}