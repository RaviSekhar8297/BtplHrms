using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DocumentFormat.OpenXml.Presentation;

namespace Human_Resource_Management
{
    public partial class ManagerAssignProjects : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindProjects();
                BindEmployees();

            }
        }
        public void BindProjects()
        {
            try
            {
                using (SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    using (SqlCommand sqlCmd = new SqlCommand())
                    {
                        sqlCmd.CommandText = "SELECT Id,ProjectName FROM ProjectsList ";
                        sqlCmd.Connection = sqlConn;
                        sqlConn.Open();
                        SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        ddlproject.DataSource = dt;
                        ddlproject.DataValueField = "Id";
                        ddlproject.DataTextField = "ProjectName";
                        ddlproject.DataBind();
                        sqlConn.Close();
                        ddlproject.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Select Project --", "0"));
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void BindEmployees()
        {
            try
            {
                using (SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    using (SqlCommand sqlCmd = new SqlCommand())
                    {
                        sqlCmd.CommandText = "SELECT EmpId,FirstName FROM Employees where DepartmentId='" + Session["DepartmentId"].ToString() + "' and EmpId!='" + Session["EmpId"].ToString() +"'";
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

                        ddlname.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Select Name --", "0"));

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
                        sqlCmd.CommandText = "INSERT INTO ProjectsData(Name,EmpId,ProjectName,ProjectDescription,ProjectDevelopment,AssignDate,StartDate,TargetDate,EndDate,ExtensionDate,AssignBy,TotalTasks,CompletedTasks,TotalDays,CompletedDays,RemainingDays,RemainingTasks,ProjectStatus,Status,Priority,Progress) VALUES(@Name,@EmpId,@ProjectName,@ProjectDescription,@ProjectDevelopment,@AssignDate,@StartDate,@TargetDate,@EndDate,@ExtensionDate,@AssignBy,@TotalTasks,@CompletedTasks,@TotalDays,@CompletedDays,@RemainingDays,@RemainingTasks,@ProjectStatus,@Status,@Priority,@Progress)";
                        sqlCmd.Connection = sqlConn;

                        // Calculate TotalDays excluding Sundays
                        DateTime startDate = DateTime.Parse(txtstartdate.Text);
                        DateTime endDate = DateTime.Parse(txttargetdate.Text);
                        int totalDays = CalculateTotalDaysExcludingSundays(startDate, endDate);

                        sqlCmd.Parameters.AddWithValue("@Name", ddlname.SelectedItem.Text);
                        sqlCmd.Parameters.AddWithValue("@EmpId", ddlname.SelectedItem.Value);
                        sqlCmd.Parameters.AddWithValue("@ProjectName", ddlproject.SelectedItem.Text);
                        sqlCmd.Parameters.AddWithValue("@ProjectDescription", txtdescription.Text);
                        sqlCmd.Parameters.AddWithValue("@ProjectDevelopment", ddlrole.SelectedItem.Text); 
                        sqlCmd.Parameters.AddWithValue("@AssignDate", DateTime.Now);
                        sqlCmd.Parameters.AddWithValue("@StartDate", startDate);
                        sqlCmd.Parameters.AddWithValue("@TargetDate", endDate);
                        sqlCmd.Parameters.AddWithValue("@EndDate", endDate);
                        sqlCmd.Parameters.AddWithValue("@ExtensionDate", endDate);// Assuming EndDate is the same as TargetDate  ExtensionDate
                        sqlCmd.Parameters.AddWithValue("@AssignBy", Session["Name"].ToString());
                        sqlCmd.Parameters.AddWithValue("@TotalTasks", txtnoofdays.Text);
                        sqlCmd.Parameters.AddWithValue("@CompletedTasks", 0);
                        sqlCmd.Parameters.AddWithValue("@TotalDays", totalDays);
                        sqlCmd.Parameters.AddWithValue("@CompletedDays", 0);
                        sqlCmd.Parameters.AddWithValue("@RemainingDays", totalDays);
                        sqlCmd.Parameters.AddWithValue("@RemainingTasks", txtnoofdays.Text);
                        sqlCmd.Parameters.AddWithValue("@ProjectStatus", "Pending");
                        sqlCmd.Parameters.AddWithValue("@Status", 0); 
                        sqlCmd.Parameters.AddWithValue("@Priority", ddlpriority.SelectedItem.Text);
                        sqlCmd.Parameters.AddWithValue("@Progress", 0);
                        sqlConn.Open();
                        int i = sqlCmd.ExecuteNonQuery();
                        if (i > 0)
                        {
                            Response.Write("<script>alert('Project Assigned to " + ddlname.SelectedItem.Text + " Successfully.')</script>");
                            ddlname.ClearSelection();
                            ddlproject.ClearSelection();
                            ddlrole.ClearSelection();
                            txtdescription.Text = "";
                            txtnoofdays.Text = "";
                            txtstartdate.Text = "";
                            txttargetdate.Text = "";
                        }
                    }
                }
            }
            catch (Exception ex)
            {               
                throw ex; 
            }
        }
        private int CalculateTotalDaysExcludingSundays(DateTime startDate, DateTime endDate)
        {
            int totalDays = 0;

            if (startDate <= endDate)
            {
                for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
                {
                    if (date.DayOfWeek != DayOfWeek.Sunday)
                    {
                        totalDays++;
                    }
                }
            }

            return totalDays;
        }

    }
}