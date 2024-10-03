using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Human_Resource_Management
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindDesignation();
                BindBranch();
            }
        }
        public void BindDesignation(string branchId = null)
        {
            try
            {
                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    // Base SQL query to fetch Designation and Branch
                    string query = @"
            SELECT DISTINCT Designation, Branch
            FROM Employees
            WHERE status = '1'";

                    // Conditionally add the branch filter if branchId is provided and not "All"
                    if (!string.IsNullOrEmpty(branchId) && branchId != "0")  // "0" represents "All"
                    {
                        query += " AND BranchId = @BranchId";
                    }

                    query += " ORDER BY Designation ASC";

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

                        // Clear any previous data in DesignationData control
                        DesignationData.Controls.Clear();

                        if (ds1.Tables[0].Rows.Count > 0)
                        {
                            int S_No = 1;
                            foreach (DataRow row in ds1.Tables[0].Rows)
                            {
                                string Designation = row["Designation"].ToString();
                                string Branch = row["Branch"].ToString();  // Fetch branch ID or branch name if available

                                // Display both Designation and Branch
                                StringBuilder projectHtml = new StringBuilder();
                                projectHtml.Append("<tr>");
                                projectHtml.Append("<td>" + S_No + "</td>");
                                projectHtml.Append("<td>" + Branch + "</td>");
                                projectHtml.Append("<td>" + Designation.ToUpper() + "</td>");
                                projectHtml.Append("<td><a class='edit' href='#' data-bs-toggle='modal' data-bs-target='#edit_designation' onclick =\"editDesignation('" + S_No + "','" + Designation + "')\"><i class='fa-solid fa-pencil m-r-5' style='color:green;'></i></a></td>");
                                projectHtml.Append("</tr>");

                                DesignationData.Controls.Add(new LiteralControl(projectHtml.ToString()));
                                S_No++;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the error or display a friendly message
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


        protected void btnupdate_Click(object sender, EventArgs e)
        {
            try
            {
                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    //using (SqlCommand sqlcmd = new SqlCommand("UPDATE Employees SET Designation=@Designation  WHERE Designation=@Desigantion1 AND Desigantion IS NOT NULL AND Desigantion <> '0'", connection))
                    using (SqlCommand sqlcmd = new SqlCommand("UPDATE Employees SET Designation=@NewDesignation   WHERE Designation=@CurrentDesignation  AND Designation IS NOT NULL AND Designation <> '0'", connection))
                    {
                        connection.Open();
                        sqlcmd.Parameters.AddWithValue("@CurrentDesignation ", txtdesignationname.Text.Trim());
                        sqlcmd.Parameters.AddWithValue("@NewDesignation ", TextBox1.Text.Trim());

                         sqlcmd.ExecuteNonQuery();
                       
                    }
                    string updateWebLoginsQuery = "UPDATE WebLogins SET Designation=@NewDesignation WHERE Designation=@CurrentDesignation";
                    using (SqlCommand sqlcmdWebLogins = new SqlCommand(updateWebLoginsQuery, connection))
                    {
                        sqlcmdWebLogins.Parameters.AddWithValue("@CurrentDesignation", txtdesignationname.Text.Trim());
                        sqlcmdWebLogins.Parameters.AddWithValue("@NewDesignation", TextBox1.Text.Trim());

                        int rowsAffectedWebLogins = sqlcmdWebLogins.ExecuteNonQuery();
                        if (rowsAffectedWebLogins > 0)
                        {
                            Response.Write("<script>alert('Designation Updated Successfully..')</script>");
                            BindDesignation();
                        }
                        else
                        {
                            Response.Write("<script>alert('Not Updated..')</script>");
                            BindDesignation();
                        }
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
                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    using (SqlCommand sqlcmd = new SqlCommand("UPDATE Employees SET Desigantion='"+TextBox1.Text+ "' WHERE Designation=@Designation", connection))
                    {
                        connection.Open();
                        sqlcmd.Parameters.AddWithValue("@Designation", txtdesignationname.Text.Trim());
                        int i = sqlcmd.ExecuteNonQuery();
                        if (i > 0)
                        {
                            Response.Write("<script>alert('Deleted Successfully..')</script>");
                            BindDesignation();
                        }
                        else
                        {
                            Response.Write("<script>alert('Not Deleted..')</script>");
                            BindDesignation();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        protected void DropDownList4_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedBranch = DropDownList4.SelectedValue;
            BindDesignation(selectedBranch);
        }
    }
}