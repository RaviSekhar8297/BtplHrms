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
using DocumentFormat.OpenXml.Wordprocessing;

namespace Human_Resource_Management
{
    public partial class AdminTimeSheet : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {               
                ProjectsListDataBind();
                ProjectsDataEmpBind();
            }
            if (!IsPostBack)
            {
                HiddenField1.Value = "0";
            }
            else
            {
                if (HiddenField1.Value == "1")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
                }

            }
        }

        public void ProjectsListDataBind()
        {
            try
            {
                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    using (SqlCommand sqlcmd = new SqlCommand("SELECT * FROM ProjectsList", connection))
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
                                string Progress = row["Progress"].ToString();
                                string ProjectCode = row["ProjectCode"].ToString();

                                // Handle EndDate safely
                                string EndDate = string.Empty;
                                if (row["EndDate"] != DBNull.Value)
                                {
                                    DateTime EdDate = Convert.ToDateTime(row["EndDate"]);
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
                                projectHtml.Append("<td><a class='dropdown-item' href='#' data-bs-toggle='modal' data-bs-target='#edit_listwork' onclick=\"editProjectList('"
     + HttpUtility.JavaScriptStringEncode(Id) + "','"
     + HttpUtility.JavaScriptStringEncode(PrName) + "','"
     + HttpUtility.JavaScriptStringEncode(Assign) + "','"
     + HttpUtility.JavaScriptStringEncode(EndDate) + "','"
     + HttpUtility.JavaScriptStringEncode(AssignDept) + "','"
     + HttpUtility.JavaScriptStringEncode(AssignDate) + "','"
     + HttpUtility.JavaScriptStringEncode(Status) + "','"
     + HttpUtility.JavaScriptStringEncode(Description) + "','"+ HttpUtility.JavaScriptStringEncode(ProjectCode) + "')\"><i class='fa-solid fa-pencil m-r-5' style='color:#057a28;'></i></a></td>");

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
                    using (SqlCommand sqlcmd = new SqlCommand("SELECT p.*, e.Image, e.Department FROM ProjectsData p INNER JOIN Employees e ON p.EmpId = e.EmpId WHERE e.Department = @DepartmentName;", connection))
                    {
                        sqlcmd.Parameters.AddWithValue("@DepartmentName", Session["DepartmentName"].ToString());

                        connection.Open();
                        SqlDataAdapter da1 = new SqlDataAdapter(sqlcmd);
                        DataSet ds1 = new DataSet();
                        da1.Fill(ds1);

                        if (ds1.Tables[0].Rows.Count > 0)
                        {
                            // Ensure we start with a new row
                            StringBuilder rowHtml = new StringBuilder();
                            int cardCount = 0;

                            foreach (DataRow row in ds1.Tables[0].Rows)
                            {
                                // Safely retrieve data with checks for column existence
                                string prName = row.Table.Columns.Contains("ProjectName") ? row["ProjectName"].ToString() : string.Empty;
                                string description = row.Table.Columns.Contains("ProjectDescription") ? row["ProjectDescription"].ToString() : string.Empty;
                                string assign = row.Table.Columns.Contains("AssignBy") ? row["AssignBy"].ToString() : string.Empty;
                                string status = row.Table.Columns.Contains("ProjectStatus") ? row["ProjectStatus"].ToString() : string.Empty;
                                string priority = row.Table.Columns.Contains("Priority") ? row["Priority"].ToString() : "Low"; // Default if not found
                                int TotalDays = row.Table.Columns.Contains("TotalDays") ? Convert.ToInt32(row["TotalDays"]) : 0;
                                int CompletedTasks = row.Table.Columns.Contains("CompletedTasks") ? Convert.ToInt32(row["CompletedTasks"]) : 0;
                                int RemainingTasks = row.Table.Columns.Contains("RemainingTasks") ? Convert.ToInt32(row["RemainingTasks"]) : 0;
                                string createdDate = row.Table.Columns.Contains("AssignDate") ? Convert.ToDateTime(row["AssignDate"]).ToString("MMM dd, yyyy") : string.Empty;
                                string deadline = row.Table.Columns.Contains("EndDate") ? Convert.ToDateTime(row["EndDate"]).ToString("MMM dd, yyyy") : string.Empty;
                                string imageUrl = row.Table.Columns.Contains("Image") ? "data:image/jpeg;base64," + Convert.ToBase64String((byte[])row["Image"]) : "";

                                string progress = row.Table.Columns.Contains("Progress") ? row["Progress"].ToString() : "0"; // Progress as a number without '%'

                                // Convert progress string to a numeric value
                                int progressValue;
                                if (!int.TryParse(progress, out progressValue))
                                {
                                    progressValue = 0; // Default to 0 if parsing fails
                                }

                                // Construct the card layout
                                StringBuilder cardHtml = new StringBuilder();
                                cardHtml.Append("<div class='col-md-4 col-sm-6 col-12 mb-3'>");
                                cardHtml.Append("<div class='card'>");
                                cardHtml.Append("<div class='card-header d-flex justify-content-between' style='box-shadow: rgba(9, 30, 66, 0.25) 0px 4px 8px -2px, rgba(9, 30, 66, 0.08) 0px 0px 0px 1px;'>");

                               // projectHtml.Append("<td><a href='#' class='avatar'><img src=" + imageUrl + " alt='Image'></a>");

                                cardHtml.Append("<a href='#' class='avatar'><img src=" + imageUrl + " alt='Image' width='60' height='60'></a>");
                                cardHtml.Append("<h5 class='card-title'>" + prName + "</h5>");
                                cardHtml.Append("<span class='badge bg-" + (status == "Completed" ? "success" : "danger") + "'>" + status + "</span>");
                                cardHtml.Append("</div>");
                                cardHtml.Append("<div class='card-body'>");
                                cardHtml.Append("<p class='card-text'>" + description + "</p>");
                                cardHtml.Append("<p><strong>Assign Date :</strong> " + createdDate + "</p>");
                                cardHtml.Append("<p><strong>Manager : </strong> " + assign + "</p>");
                                cardHtml.Append("<p><strong>Priority : </strong> <span class='text-" + (priority == "High" ? "danger" : "success") + "'>" + priority + "</span></p>");
                                cardHtml.Append("<p><strong>Deadline : </strong> " + deadline + "</p>");
                                cardHtml.Append("<p><strong>Total Days : </strong> " + TotalDays + "</p>");
                                cardHtml.Append("<p><strong>Completed Tasks : </strong> " + CompletedTasks + "</p>");
                                cardHtml.Append("<p><strong>Remaining Tasks : </strong> " + RemainingTasks + "</p>");
                                cardHtml.Append("<div class='progress'>");
                                cardHtml.Append("<div class='progress-bar' role='progressbar' style='width: " + progressValue + "%;' aria-valuenow='" + progressValue + "' aria-valuemin='0' aria-valuemax='100'>" + progressValue + "%</div>");
                                cardHtml.Append("</div>");
                                cardHtml.Append("</div>");
                                cardHtml.Append("</div>");
                                cardHtml.Append("</div>");

                                rowHtml.Append(cardHtml);

                                cardCount++;
                                if (cardCount % 3 == 0)
                                {
                                    // Close the row and start a new one every 3 cards
                                    ProjectsDataBind.Controls.Add(new LiteralControl("<div class='row'>"));
                                    ProjectsDataBind.Controls.Add(new LiteralControl(rowHtml.ToString()));
                                    ProjectsDataBind.Controls.Add(new LiteralControl("</div>"));
                                    rowHtml.Clear();
                                }
                            }

                            // Add any remaining cards if they don't complete a full row
                            if (rowHtml.Length > 0)
                            {
                                ProjectsDataBind.Controls.Add(new LiteralControl("<div class='row'>"));
                                ProjectsDataBind.Controls.Add(new LiteralControl(rowHtml.ToString()));
                                ProjectsDataBind.Controls.Add(new LiteralControl("</div>"));
                            }
                        }
                        else
                        {
                            // Handle no records case
                            ProjectsDataBind.Controls.Add(new LiteralControl("<div class='no-records-message'>No Records...</div>"));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                throw ex;
            }
        }

        protected void btnupdatelist_Click(object sender, EventArgs e)
        {
            try
            {
                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("UPDATE ProjectsList SET EndDate=@EndDate, Description=@Description WHERE Id=@Id", connection))
                    {
                        connection.Open();
                        cmd.Parameters.AddWithValue("@EndDate", string.IsNullOrEmpty(TextBox3.Text) ? (object)DBNull.Value : Convert.ToDateTime(TextBox3.Text));
                        cmd.Parameters.AddWithValue("@Description", string.IsNullOrEmpty(TextBox7.Text) ? (object)DBNull.Value : TextBox7.Text);
                        cmd.Parameters.AddWithValue("@Id", Convert.ToInt32(HiddenField2.Value));

                        int i = cmd.ExecuteNonQuery();
                        if (i > 0)
                        {
                            Response.Write("<script>alert('Updated Successfully..')</script>");
                            ProjectsListDataBind();
                        }
                        else
                        {
                            Response.Write("<script>alert('Failed..')</script>");
                            ProjectsListDataBind();
                        }
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