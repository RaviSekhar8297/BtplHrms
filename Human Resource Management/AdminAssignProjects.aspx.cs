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
    public partial class AdminAssignProjects : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindDepartments();
            }
               
        }
        public void BindDepartments()
        {
            try
            {
                using (SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    using (SqlCommand sqlCmd = new SqlCommand())
                    {
                        sqlCmd.CommandText = "SELECT Branch_Id,BranchName FROM Branch ";
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
        protected void btnassignproject_Click(object sender, EventArgs e)
        {

            try
            {
                using (SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    using (SqlCommand sqlCmd = new SqlCommand())
                    {
                        sqlCmd.CommandText = "INSERT INTO ProjectsList(ProjectName, Description, AssignDate, StartDate, TargetDate, EndDate, ExtensionDate, AssignBy, AssignTo, AssignDepartment, ProjectStatus, Status, Priority, Progress, TotalTasks, CompletedTasks, RemainingTasks) " +
                                             "VALUES(@ProjectName, @Description, @AssignDate, @StartDate, @TargetDate, @EndDate, @ExtensionDate, @AssignBy, @AssignTo, @AssignDepartment, @ProjectStatus, @Status, @Priority, @Progress, @TotalTasks, @CompletedTasks, @RemainingTasks)";
                        sqlCmd.Connection = sqlConn;

                        // Open the connection
                        sqlConn.Open();

                        // Add parameters with explicit types
                        sqlCmd.Parameters.Add("@ProjectName", SqlDbType.VarChar).Value = txtprojectname.Text;
                        sqlCmd.Parameters.Add("@Description", SqlDbType.VarChar).Value = txtdescription.Text;
                        sqlCmd.Parameters.Add("@AssignDate", SqlDbType.DateTime).Value = DateTime.Now;
                        sqlCmd.Parameters.Add("@StartDate", SqlDbType.Date).Value = DateTime.Parse(txtstartdate.Text);
                        sqlCmd.Parameters.Add("@TargetDate", SqlDbType.Date).Value = DateTime.Parse(txttargetdate.Text);
                        sqlCmd.Parameters.Add("@EndDate", SqlDbType.Date).Value = DateTime.Parse(txttargetdate.Text);
                        sqlCmd.Parameters.Add("@ExtensionDate", SqlDbType.Date).Value = DateTime.Parse(txttargetdate.Text);
                        sqlCmd.Parameters.Add("@AssignBy", SqlDbType.VarChar).Value = Session["Name"].ToString();
                        sqlCmd.Parameters.Add("@AssignTo", SqlDbType.VarChar).Value = ddlmanagername.SelectedItem.Text;
                        sqlCmd.Parameters.Add("@AssignDepartment", SqlDbType.VarChar).Value = ddldepartment.SelectedItem.Text;
                        sqlCmd.Parameters.Add("@ProjectStatus", SqlDbType.VarChar).Value = "Not Yet Start";
                        sqlCmd.Parameters.Add("@Status", SqlDbType.Int).Value = 0;
                        sqlCmd.Parameters.Add("@Priority", SqlDbType.VarChar).Value = ddlpriority.SelectedItem.Text;
                        sqlCmd.Parameters.Add("@Progress", SqlDbType.Int).Value = 0;
                        sqlCmd.Parameters.Add("@TotalTasks", SqlDbType.Int).Value = 20;
                        sqlCmd.Parameters.Add("@CompletedTasks", SqlDbType.Int).Value = 0;
                        sqlCmd.Parameters.Add("@RemainingTasks", SqlDbType.Int).Value = 20;

                        // Execute the SQL command
                        int i = sqlCmd.ExecuteNonQuery();
                        if (i > 0)
                        {
                            Response.Write("<script>alert('Project assigned to " + ddldepartment.SelectedItem.Text + " Department successfully...')</script>");

                            // Clear input fields
                            txtprojectname.Text = "";
                            txtdescription.Text = "";
                            txtstartdate.Text = "";
                            txttargetdate.Text = "";
                            ddlmanagername.ClearSelection();
                            ddldepartment.ClearSelection();
                            ddlpriority.ClearSelection();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Preserve original exception with 'throw'
                throw ex;
            }

        }

        protected void ddlbranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
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
                using (SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    using (SqlCommand sqlCmd = new SqlCommand())
                    {
                        sqlCmd.CommandText = "select EmpId,Name from WebLogins  where status= '1'  and DepartmentName= '" + ddldepartment.SelectedItem.Text + "' and Role='Manager' ";
                        sqlCmd.Connection = sqlConn;
                        sqlConn.Open();
                        SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        ddlmanagername.DataSource = dt;
                        ddlmanagername.DataValueField = "EmpId";
                        ddlmanagername.DataTextField = "Name";
                        ddlmanagername.DataBind();
                        sqlConn.Close();
                        ddlmanagername.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Select Manager Name --", "0"));

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