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
    public partial class ManagerBalenceLeaves : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindLeavesList();
            }
        }
        public void BindLeavesList()
        {
            try
            {
                string nameSearch = txtempnamesearch.Text.Trim(); // Get the search text
                string department = Session["DepartmentName"].ToString(); // Get department from session

                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    string query = @"
                SELECT LeavesList.*, Employees.Image, Employees.Department
                FROM LeavesList
                INNER JOIN Employees ON LeavesList.EmpId = Employees.EmpId
                WHERE LeavesList.Status = '1' 
                AND Employees.Department = @Department
                AND (@NameSearch = '' OR Employees.FirstName LIKE @NameSearch)";

                    using (SqlCommand sqlcmd = new SqlCommand(query, connection))
                    {
                        sqlcmd.Parameters.AddWithValue("@Department", department);
                        sqlcmd.Parameters.AddWithValue("@NameSearch", "%" + nameSearch + "%");

                        connection.Open();
                        SqlDataAdapter da1 = new SqlDataAdapter(sqlcmd);
                        DataSet ds1 = new DataSet();
                        da1.Fill(ds1);

                        balenceLeaves.Controls.Clear(); // Clear previous data

                        if (ds1.Tables[0].Rows.Count > 0)
                        {
                            StringBuilder projectHtml = new StringBuilder();
                            foreach (DataRow row in ds1.Tables[0].Rows)
                            {
                                string EmpId = row["EmpId"].ToString();
                                string Name = row["Name"].ToString();
                                string Department = row["Department"].ToString();
                                string Designation = row["Designation"].ToString();
                                string TotalCasualLeaves = row["TotalCasualLeaves"].ToString();
                                string UsedCasualLeaves = row["UsedCasualLeaves"].ToString();
                                string BalenceCasualLeaves = row["BalenceCasualLeaves"].ToString();
                                string TotalSickLeaves = row["TotalSickLeaves"].ToString();
                                string UsedSickLeaves = row["UsedSickLeaves"].ToString();
                                string BalenceSickLaves = row["BalenceSickLaves"].ToString();
                                string TotalCampOffLeaves = row["TotalCampOffLeaves"].ToString();
                                string UsedCampOffLeaves = row["UsedCampOffLeaves"].ToString();
                                string BalenceCampOffLeaves = row["BalenceCampOffLeaves"].ToString();
                                string Year = row["Year"].ToString();

                                object imageDataObj = row["Image"];
                                byte[] imageData = imageDataObj != DBNull.Value ? (byte[])imageDataObj : new byte[0];
                                string base64String = Convert.ToBase64String(imageData);
                                string imageUrl = "data:image/jpeg;base64," + base64String;

                                projectHtml.Append("<tr>");
                                projectHtml.Append("<td><a href='#' class='avatar'><img src='" + imageUrl + "' alt='Image'></a>");
                                projectHtml.Append("<a href='#'>" + Name + " </a></td>");
                                projectHtml.Append("<td>" + EmpId + "</td>");
                                projectHtml.Append("<td>" + UsedCasualLeaves + "</td>");
                                projectHtml.Append("<td>" + BalenceCasualLeaves + "</td>");
                                projectHtml.Append("<td>" + TotalSickLeaves + "</td>");
                                projectHtml.Append("<td>" + UsedSickLeaves + "</td>");
                                projectHtml.Append("<td>" + BalenceSickLaves + "</td>");
                                projectHtml.Append("<td>" + TotalCampOffLeaves + "</td>");
                                projectHtml.Append("<td>" + UsedCampOffLeaves + "</td>");
                                projectHtml.Append("<td>" + BalenceCampOffLeaves + "</td>");
                                projectHtml.Append("<td>" + Year + "</td>");
                                projectHtml.Append("<td><a class='edit' href='#' data-bs-toggle='modal' data-bs-target='#edit_leaveList' onclick=\"editLeavesList('" + EmpId + "','" + Name + "','" + UsedCasualLeaves + "','" + BalenceCasualLeaves + "','" + UsedSickLeaves + "','" + BalenceSickLaves + "','" + UsedCampOffLeaves + "','" + BalenceCampOffLeaves + "','" + Year + "')\"><i class='fa-solid fa-pencil m-r-5' style='color:green;'></i></a></td>");
                                projectHtml.Append("</tr>");
                            }
                            balenceLeaves.Controls.Add(new LiteralControl(projectHtml.ToString()));
                        }
                        else
                        {
                            balenceLeaves.Controls.Add(new LiteralControl("<tr><td colspan='11'>No records found</td></tr>"));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                throw ex;
            }
        }


        protected void txtempnamesearch_TextChanged(object sender, EventArgs e)
        {
            BindLeavesList();
        }

        protected void btnLeaveUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand sqlcmd = new SqlCommand("UPDATE LeavesList SET UsedCasualLeaves=@UsedCasualLeaves,BalenceCasualLeaves=@BalenceCasualLeaves,UsedSickLeaves=@UsedSickLeaves,BalenceSickLaves=@BalenceSickLaves,UsedCampOffLeaves=@UsedCampOffLeaves,BalenceCampOffLeaves=@BalenceCampOffLeaves,UpdatedBy=@UpdatedBy,UpdatedDate=@UpdatedDate WHERE EmpId=@EmpId and Year=@Year and Status = '1' ", connection))
                    {
                        sqlcmd.Parameters.AddWithValue("@UsedCasualLeaves", txtucls.Text);
                        sqlcmd.Parameters.AddWithValue("@BalenceCasualLeaves", txtblscasualleaves.Text);
                        sqlcmd.Parameters.AddWithValue("@UsedSickLeaves", txtusl.Text);
                        sqlcmd.Parameters.AddWithValue("@BalenceSickLaves", txtbsl.Text);
                        sqlcmd.Parameters.AddWithValue("@UsedCampOffLeaves", txtucompoffs.Text);
                        sqlcmd.Parameters.AddWithValue("@BalenceCampOffLeaves", txtblscompoffs.Text);
                        sqlcmd.Parameters.AddWithValue("@EmpId", HiddenField1.Value);
                        sqlcmd.Parameters.AddWithValue("@Year", DateTime.Now.Year);
                        sqlcmd.Parameters.AddWithValue("@UpdatedBy", Session["Name"].ToString());
                        sqlcmd.Parameters.AddWithValue("@UpdatedDate", DateTime.Now);
                        int i = sqlcmd.ExecuteNonQuery();
                        if (i > 0)
                        {
                            Response.Write("<script>alert('" + txtname.Text + " Leaves Updated Successfully...')</script>");
                            BindLeavesList();
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