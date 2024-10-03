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
using DocumentFormat.OpenXml.Presentation;

namespace Human_Resource_Management
{
    public partial class ManagerProjects : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {              
                ProjectsListDataBind();
                ProjectsDataEmpBind();
            }
        }
        public void ProjectsListDataBind()
        {
            try
            {
                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    using (SqlCommand sqlcmd = new SqlCommand("SELECT * FROM ProjectsList where AssignDepartment='" + Session["DepartmentName"].ToString() +"'", connection))
                    {
                        connection.Open();
                        SqlDataAdapter da1 = new SqlDataAdapter(sqlcmd);
                        DataSet ds1 = new DataSet();
                        da1.Fill(ds1);

                        if (ds1.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow row in ds1.Tables[0].Rows)
                            {
                                // Safely retrieve and handle DBNull values
                                string Id = row["Id"].ToString();
                                string PrName = row["ProjectName"].ToString();
                                string Description = row["Description"].ToString();
                                string Assign = row["AssignBy"].ToString();
                                string TotalTasks = row["TotalTasks"].ToString();
                                string CompletedTasks = row["CompletedTasks"].ToString();
                                string RemainingTasks = row["RemainingTasks"].ToString();
                                string Priority = row["Priority"].ToString();
                                string Progress = row["Progress"].ToString();

                                // Handle EndDate safely
                                string EndDate = string.Empty;
                                if (row["TargetDate"] != DBNull.Value)
                                {
                                    DateTime EdDate = Convert.ToDateTime(row["TargetDate"]);
                                    EndDate = EdDate.ToString("yyyy-MM-dd");
                                }

                                string Status = row["ProjectStatus"].ToString();
                                string AssignDate = string.Empty;
                                if (row["AssignDate"] != DBNull.Value)
                                {
                                    AssignDate = Convert.ToDateTime(row["AssignDate"]).ToString("yyyy-MM-dd");
                                }

                                string AssignDept = row["AssignDepartment"].ToString();

                                StringBuilder projectHtml = new StringBuilder();

                                projectHtml.Append("<tr>");
                                projectHtml.Append("<td>" + Id + "</td>");
                                projectHtml.Append("<td>" + PrName + "</td>");
                                projectHtml.Append("<td>" + Assign + "</td>");
                                projectHtml.Append("<td>" + AssignDate + "</td>");
                                projectHtml.Append("<td>" + AssignDept + "</td>"); 
                                projectHtml.Append("<td>" + EndDate + "</td>");                               
                                projectHtml.Append("<td>" + Status + "</td>");
                                projectHtml.Append("<td>");
                                projectHtml.Append("<div class='circular-progress' style='--value:" + Progress + ";' data-progress='" + Progress + "'></div>");
                                projectHtml.Append("</td>");
                                projectHtml.Append("<td><a class='dropdown-item' href='#' data-bs-toggle='modal' data-bs-target='#edit_listwork' onclick=\"editProjectList('" + Id + "','" + PrName + "','" + Assign + "','" + AssignDate + "','" + EndDate + "','" + Priority + "','" + TotalTasks + "','" + CompletedTasks + "','" + Description + "')\"><i class='fa-solid fa-pencil m-r-5' style='color:#057a28;'></i></a></td>");
                                projectHtml.Append("</tr>");

                                ProjectsListBind.Controls.Add(new LiteralControl(projectHtml.ToString()));
                            }
                        }
                        else
                        {
                            // Handle no records case
                            // Literal1.Text = "<div class='no-records-message'>No Records...</div>";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex; // Re-throwing the exception
            }
        }

        public void ProjectsDataEmpBind()
        {
            try
            {
                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    using (SqlCommand sqlcmd = new SqlCommand("SELECT p.*, e.Image,e.Department FROM ProjectsData p INNER JOIN Employees e ON p.EmpId = e.EmpId WHERE e.Department = '" + Session["DepartmentName"].ToString() +"';", connection))
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
                                string EmpId = row["EmpId"] != DBNull.Value ? row["EmpId"].ToString() : string.Empty;
                                string PrName = row["ProjectName"] != DBNull.Value ? row["ProjectName"].ToString() : string.Empty;
                                string Description = row["ProjectDescription"] != DBNull.Value ? row["ProjectDescription"].ToString() : string.Empty;
                                string Assign = row["AssignBy"] != DBNull.Value ? row["AssignBy"].ToString() : string.Empty;
                                string Completedays = row["CompletedDays"] != DBNull.Value ? row["CompletedDays"].ToString() : string.Empty;
                                string RemainingDays = row["RemainingDays"] != DBNull.Value ? row["RemainingDays"].ToString() : string.Empty;
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
                                projectHtml.Append("<td><a href='#' class='avatar'><img src=" + imageUrl + " alt='Image'></a></td>");
                                projectHtml.Append("<td>" + PrName + "</td>");
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
                                projectHtml.Append("<td><a class='dropdown - item' href='#' data-bs-toggle='modal' data-bs-target='#edit_todaywork'onclick =\"editWork('" + Id + "','" + PrName + "','" + AssignDate + "','" + StartDate + "','" + EndDate + "','" + Assign + "','" + Name + "','" + Status + "','" + Description + "','" + Completed + "','" + RemainingTasks + "','" + ExtentionDate + "','" + Completedays + "','" + RemainingDays + "','" + Progress + "','" + EmpId + "')\" ><i class='fa-solid fa-pencil m-r-5' style='color:#057a28;'></i></a></td>");
                                //projectHtml.Append("<td><a class='dropdown - item' href='#' data-bs-toggle='modal' data-bs-target='#delete_resignation'data-hyid='" + Id + "'><i class='fa-regular fa-trash-can m-r-5' style='color:#ab0c19;'></i></a></td>");

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
                        sqlcmd.Parameters.AddWithValue("@EmployeeId", HiddenField3.Value);
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
                                    using (var updateCmd = new SqlCommand("UPDATE ProjectsData SET CompletedTasks=@CompletedTasks, RemainingTasks=@RemainingTasks, ProjectStatus=@ProjectStatus, Progress=@Progress, CompletedDays=@CompletedDays, RemainingDays=@RemainingDays,StartDate=@StartDate,EndDate=@EndDate,ExtensionDate=@ExtensionDate WHERE Id=@Id AND EmpId=@EmpId", connection))
                                    {
                                        updateCmd.Parameters.AddWithValue("@CompletedTasks", CompletedTasks);
                                        updateCmd.Parameters.AddWithValue("@RemainingTasks", remainingTasks);
                                        updateCmd.Parameters.AddWithValue("@ProjectStatus", remainingTasks == 0 ? "Completed" : "Working");

                                        int progress = (int)((float)CompletedTasks / totaltasks * 100);
                                        updateCmd.Parameters.AddWithValue("@Progress", progress);
                                        updateCmd.Parameters.AddWithValue("@CompletedDays", completedDays);
                                        updateCmd.Parameters.AddWithValue("@RemainingDays", remainingDays);

                                        updateCmd.Parameters.AddWithValue("@StartDate", txtstartdate.Text);                                    
                                        updateCmd.Parameters.AddWithValue("@EndDate", txtenddate.Text);
                                        updateCmd.Parameters.AddWithValue("@ExtensionDate", txtextentiondate.Text);

                                        updateCmd.Parameters.AddWithValue("@Id", HiddenField1.Value);
                                        updateCmd.Parameters.AddWithValue("@EmpId", HiddenField3.Value);

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

        protected void btnprojectlistupdate_Click(object sender, EventArgs e)
        {
            try
            {
                 SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString);
                 SqlCommand sqlcmd = new SqlCommand("UPDATE ProjectsList SET CompletedTasks=@CompletedTasks, RemainingTasks=@RemainingTasks, Progress=@Progress, ProjectStatus=@ProjectStatus WHERE Id=@Id", connection);

                // Validate and parse TextBox inputs
                if (!int.TryParse(txtlisttotaltasks.Text, out int totaltasks) || !int.TryParse(txtlistcompletedtasks.Text, out int completed))
                {
                    Response.Write("<script>alert('Please enter valid numeric values for tasks.')</script>");
                    return;
                }

                // Validate if completed tasks exceed total tasks
                if (totaltasks < completed)
                {
                    Response.Write("<script>alert('Enter less than or equal to total tasks for Completed Tasks...')</script>");
                    return;
                }

                // Calculate remaining tasks and progress
                int remaining = totaltasks - completed;
                float progress = (float)completed / totaltasks * 100f;

                // Parse HiddenField2 value
                if (!int.TryParse(HiddenField2.Value, out int projectId))
                {
                    Response.Write("<script>alert('Invalid project ID.')</script>");
                    return;
                }

                // Open connection and add parameters
                connection.Open();
                sqlcmd.Parameters.Add("@CompletedTasks", SqlDbType.Int).Value = completed;
                sqlcmd.Parameters.Add("@RemainingTasks", SqlDbType.Int).Value = remaining;
                sqlcmd.Parameters.Add("@Progress", SqlDbType.Float).Value = progress;
                sqlcmd.Parameters.Add("@Id", SqlDbType.Int).Value = projectId;

                // Set project status
                sqlcmd.Parameters.Add("@ProjectStatus", SqlDbType.VarChar).Value = (remaining == 0) ? "Completed" : "Working";

                // Execute update query
                int i = sqlcmd.ExecuteNonQuery();
                if (i > 0)
                {
                    Response.Write("<script>alert('Updated Successfully.')</script>");
                    ProjectsListDataBind();
                }
                else
                {
                    Response.Write("<script>alert('Project Update Failed.')</script>");
                }
            }
            catch (Exception ex)
            {
                // Log error and display a generic message
                Response.Write("<script>alert('An error occurred " + ex + " during the update. Please try again.')</script>");
            }


        }
    }
}