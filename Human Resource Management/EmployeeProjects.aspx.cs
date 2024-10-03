using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Human_Resource_Management
{
    public partial class EmployeeProjects : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               ProjectsDataEmpBind();
            }
        }
        public void ProjectsDataEmpBind()
        {
            try
            {
                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    using (SqlCommand sqlcmd = new SqlCommand("SELECT p.*, e.Image,e.Department FROM ProjectsData p INNER JOIN Employees e ON p.EmpId = e.EmpId WHERE e.Department = '" + Session["DepartmentName"].ToString() + "' and  p.EmpId='" + Session["EmpId"].ToString() +"'", connection))
                    {
                        connection.Open();
                        SqlDataAdapter da1 = new SqlDataAdapter(sqlcmd);
                        DataSet ds1 = new DataSet();
                        da1.Fill(ds1);
                        if (ds1.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow row in ds1.Tables[0].Rows)
                            {
                                string Name = row["Name"] != DBNull.Value ? row["Name"].ToString() : string.Empty;
                                string Id = row["Id"] != DBNull.Value ? row["Id"].ToString() : string.Empty;
                                string PrName = row["ProjectName"] != DBNull.Value ? row["ProjectName"].ToString() : string.Empty;
                                string Description = row["ProjectDescription"] != DBNull.Value ? row["ProjectDescription"].ToString() : string.Empty;
                                string Assign = row["AssignBy"] != DBNull.Value ? row["AssignBy"].ToString() : string.Empty;
                                string NumberOfDays = row["TotalDays"] != DBNull.Value ? row["TotalDays"].ToString() : string.Empty;
                                string Progress = row["Progress"] != DBNull.Value ? row["Progress"].ToString() : string.Empty;
                                // Handle EndDate safely 
                                string EndDate = string.Empty;
                                if (row["EndDate"] != DBNull.Value)
                                {
                                    DateTime EdDate = Convert.ToDateTime(row["EndDate"]);
                                    EndDate = EdDate.ToString("yyyy-MM-dd");
                                }
                                string ExtentionDate = string.Empty;
                                if (row["ExtensionDate"] != DBNull.Value)
                                {
                                    ExtentionDate = Convert.ToDateTime(row["ExtensionDate"]).ToString("yyyy-MM-dd");
                                }

                                // Handle AssignDate and StartDate safely
                                string AssignDate = row["AssignDate"] != DBNull.Value ? Convert.ToDateTime(row["AssignDate"]).ToString("yyyy-MM-dd") : string.Empty;
                                string StartDate = row["StartDate"] != DBNull.Value ? Convert.ToDateTime(row["StartDate"]).ToString("yyyy-MM-dd") : string.Empty;

                                string Status = row["ProjectStatus"] != DBNull.Value ? row["ProjectStatus"].ToString() : string.Empty;
                                string RemainingTasks = row["RemainingTasks"] != DBNull.Value ? row["RemainingTasks"].ToString() : string.Empty;
                                string Completed = row["CompletedTasks"] != DBNull.Value ? row["CompletedTasks"].ToString() : string.Empty;
                                string imageUrl = row["Image"] != DBNull.Value ? "data:image/jpeg;base64," + Convert.ToBase64String((byte[])row["Image"]) : "";

                                StringBuilder projectHtml = new StringBuilder();

                                projectHtml.Append("<tr>");
                                projectHtml.Append("<td><a href='#' class='avatar'><img src=" + imageUrl + " alt='Image'></a>");
                                projectHtml.Append("" + PrName + "</td>");
                                projectHtml.Append("<td>" + AssignDate + "</td>");
                                projectHtml.Append("<td>" + StartDate + "</td>");
                                projectHtml.Append("<td>" + EndDate + "</td>");
                                projectHtml.Append("<td>" + Assign + "</td>");
                                projectHtml.Append("<td>" + Name + "</td>");
                                projectHtml.Append("<td>" + Completed + "</td>");
                                projectHtml.Append("<td>" + RemainingTasks + "</td>");
                                projectHtml.Append("<td>" + Status + "</td>");
                                projectHtml.Append("<td>");
                                projectHtml.Append("<div class='circular-progress' style='--value:" + Progress + ";' data-progress='" + Progress + "'></div>");
                                projectHtml.Append("</td>");
                                projectHtml.Append("<td><a class='dropdown - item' href='#' data-bs-toggle='modal' data-bs-target='#edit_todaywork'onclick =\"editWork('" + Id + "','" + PrName + "','" + AssignDate + "','" + StartDate + "','" + EndDate + "','" + Assign + "','" + Name + "','" + Status + "','" + Description + "','" + Completed + "','" + RemainingTasks + "','" + ExtentionDate + "','" + NumberOfDays + "')\" ><i class='fa-solid fa-pencil m-r-5' style='color:#057a28;'></i></a></td>");
                                projectHtml.Append("</tr>");

                                ProjectsDataBind.Controls.Add(new LiteralControl(projectHtml.ToString()));
                            }
                        }
                        else
                        {
                            //Literal1.Text = "<div class='no-records-message'>No Records...</div>";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnprojectupdate_Click(object sender, EventArgs e)
        {
            try
            {
                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    connection.Open();

                    // Fetch project data
                    using (var sqlcmd = new SqlCommand("SELECT * FROM ProjectsData WHERE EmpId = @EmployeeId AND Id=@Id", connection))
                    {
                        sqlcmd.Parameters.AddWithValue("@EmployeeId", Session["EmpId"].ToString());
                        sqlcmd.Parameters.AddWithValue("@Id", HiddenField1.Value);

                        using (var myReader = sqlcmd.ExecuteReader())
                        {
                            if (myReader.Read())
                            {
                                int totaltasks = Convert.ToInt32(myReader["TotalTasks"]);
                                int TotalDays = Convert.ToInt32(myReader["TotalDays"]);
                                string StartDateStr = myReader["StartDate"].ToString();

                                // Convert StartDate to DateTime
                                if (!DateTime.TryParse(StartDateStr, out DateTime StartDate))
                                {
                                    StartDate = DateTime.MinValue; // Set a default value if parsing fails
                                }

                                DateTime currentDate = DateTime.Now.Date; // Get today's date
                                int completedDays = 0;

                                if (StartDate != DateTime.MinValue && currentDate > StartDate)
                                {
                                    // Iterate from StartDate to currentDate
                                    for (DateTime date = StartDate; date <= currentDate; date = date.AddDays(1))
                                    {
                                        // Count only if the day is not a Sunday
                                        if (date.DayOfWeek != DayOfWeek.Sunday)
                                        {
                                            completedDays++;
                                        }
                                    }
                                }

                                // Make sure completedDays does not exceed TotalDays
                                completedDays = Math.Min(completedDays, TotalDays);

                                // Calculate remaining days
                                int remainingDays = TotalDays - completedDays;

                                if (int.TryParse(txtcompleted.Text, out int CompletedTasks))
                                {
                                    int remainingTasks = totaltasks - CompletedTasks;
                                    myReader.Close();
                                    // Ensure the SqlCommand for update is created after the reader is closed
                                    using (var updateCmd = new SqlCommand("UPDATE ProjectsData SET CompletedTasks=@CompletedTasks, RemainingTasks=@RemainingTasks, ProjectStatus=@ProjectStatus, Progress=@Progress, CompletedDays=@CompletedDays, RemainingDays=@RemainingDays WHERE Id=@Id AND EmpId=@EmpId", connection))
                                    {
                                        updateCmd.Parameters.AddWithValue("@CompletedTasks", CompletedTasks);
                                        updateCmd.Parameters.AddWithValue("@RemainingTasks", remainingTasks);
                                        updateCmd.Parameters.AddWithValue("@ProjectStatus", remainingTasks == 0 ? "Completed" : "Working");

                                        int progress = (int)((float)CompletedTasks / totaltasks * 100);
                                        updateCmd.Parameters.AddWithValue("@Progress", progress);
                                        updateCmd.Parameters.AddWithValue("@CompletedDays", completedDays);
                                        updateCmd.Parameters.AddWithValue("@RemainingDays", remainingDays);
                                        updateCmd.Parameters.AddWithValue("@Id", HiddenField1.Value);
                                        updateCmd.Parameters.AddWithValue("@EmpId", Session["EmpId"].ToString());

                                        int i = updateCmd.ExecuteNonQuery();
                                        if (i > 0)
                                        {
                                            Response.Write("<script>alert('Updated Successfully....')</script>");
                                            ProjectsDataEmpBind();
                                        }
                                        else
                                        {
                                            Response.Write("<script>alert('Update Failed....')</script>");
                                        }
                                    }
                                }
                                else
                                {
                                    Response.Write("<script>alert('Invalid input for completed tasks')</script>");
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('An error occurred: " + ex.Message + "')</script>");
            }


        }
    }
}