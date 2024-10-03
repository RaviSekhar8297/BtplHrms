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
    public partial class AdminLeaveBalance : System.Web.UI.Page
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
                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    using (SqlCommand sqlcmd = new SqlCommand("SELECT LeavesList.*, Employees.Image FROM LeavesList INNER JOIN Employees ON LeavesList.EmpId = Employees.EmpId WHERE LeavesList.Status = '1' ", connection))
                    {
                        connection.Open();
                        SqlDataAdapter da1 = new SqlDataAdapter(sqlcmd);
                        DataSet ds1 = new DataSet();
                        da1.Fill(ds1);
                        if (ds1.Tables[0].Rows.Count > 0)
                        {
                            int S_No = 1;
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
                                StringBuilder projectHtml = new StringBuilder();

                                projectHtml.Append("<tr>");
                                projectHtml.Append("<td><a href='#' class='avatar'><img src=" + imageUrl + " alt='Image'></a>");
                                projectHtml.Append("<a href='#'>" + Name + " </a></td>");
                                projectHtml.Append("<td>" + EmpId + "</td>");                                                    
                                //projectHtml.Append("<td>" + TotalCasualLeaves + "</td>");
                                projectHtml.Append("<td>" + UsedCasualLeaves + "</td>");
                                projectHtml.Append("<td>" + BalenceCasualLeaves + "</td>");
                                projectHtml.Append("<td>" + TotalSickLeaves + "</td>");
                                projectHtml.Append("<td>" + UsedSickLeaves + "</td>");                               
                                projectHtml.Append("<td>" + BalenceSickLaves + "</td>");

                                projectHtml.Append("<td>" + TotalCampOffLeaves + "</td>");
                                projectHtml.Append("<td>" + UsedCampOffLeaves + "</td>");
                                projectHtml.Append("<td>" + BalenceCampOffLeaves + "</td>");                             
                                projectHtml.Append("<td>" + Year + "</td>");
                                projectHtml.Append("<td><a class='edit' href='#' data-bs-toggle='modal' data-bs-target='#edit_leaveList' onclick =\"editLeavesList('" + EmpId + "','" + Name + "','"+ UsedCasualLeaves + "','" + BalenceCasualLeaves + "','"+ UsedSickLeaves + "','"+ BalenceSickLaves + "','" + UsedCampOffLeaves + "','"+ BalenceCampOffLeaves + "','"+ Year + "')\" ><i class='fa-solid fa-pencil m-r-5' style='color:green;'></i></a></td>");
                                // projectHtml.Append("<td><a class='delete ' href='#' data-bs-toggle='modal' data-bs-target='#delete_department' onclick =\"deletedept('" + DeptId + "','" + Department + "')\"><i class='fa-regular fa-trash-can m-r-5' style='color:red;'></i></a></td>");
                                projectHtml.Append("</tr>");

                                balenceLeaves.Controls.Add(new LiteralControl(projectHtml.ToString()));
                                S_No++;
                            }
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
                        if(i > 0)
                        {
                            Response.Write("<script>alert('" + txtname.Text + " Leaves Updated Successfully...')</script>");
                            BindLeavesList();
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        protected void btnaddallempleaves_Click(object sender, EventArgs e)
        {
            string selectedLeaveType = ddlleavetype.SelectedValue; 
            int leaveTotal;

            if (string.IsNullOrEmpty(selectedLeaveType) || !int.TryParse(txtleavestotal.Text, out leaveTotal))
            {
               
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Please select a leave type and enter a valid number.');", true);
                return;
            }

            // Connection to the database
            string connectionString = ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                // Prepare the SQL query based on the selected leave type
                string query = string.Empty;

                if (selectedLeaveType == "Casual Leave")
                {
                    // Update both BalenceCasualLeaves and TotalCasualLeaves for all employees with Status = '1' and Year = DateTime.Now.Year
                    query = @"
                UPDATE LeavesList 
                SET BalenceCasualLeaves = ISNULL(BalenceCasualLeaves, 0) + @leaveTotal,
                    TotalCasualLeaves = ISNULL(TotalCasualLeaves, 0) + @leaveTotal
                WHERE Status = '1' AND Year = @currentYear";
                }
                else if (selectedLeaveType == "Comp-Off Leave")
                {
                    // Update both TotalCampOffLeaves and BalenceCampOffLeaves for all employees with Status = '1' and Year = DateTime.Now.Year
                    query = @"
                UPDATE LeavesList 
                SET TotalCampOffLeaves = ISNULL(TotalCampOffLeaves, 0) + @leaveTotal,
                    BalenceCampOffLeaves = ISNULL(BalenceCampOffLeaves, 0) + @leaveTotal
                WHERE Status = '1' AND Year = @currentYear";
                }
                else
                {
                    // Handle unexpected leave types
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Unexpected leave type selected.');", true);
                    return;
                }

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    // Add parameters to prevent SQL injection
                    cmd.Parameters.AddWithValue("@leaveTotal", leaveTotal);
                    cmd.Parameters.AddWithValue("@currentYear", DateTime.Now.Year);

                    // Execute the query
                    int rowsAffected = cmd.ExecuteNonQuery();

                    // Optionally display feedback based on the result
                    if (rowsAffected > 0)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Leaves updated successfully for all eligible employees!');", true);
                        BindLeavesList();
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('No records were updated.');", true);
                    }
                }
            }
        }
    }
}