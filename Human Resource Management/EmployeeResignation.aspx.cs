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
    public partial class EmployeeResignation1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                BindResignData();

            }

        }

        public void BindResignData()
        {
            string DepartmentSession = Session["DepartmentName"].ToString();
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "SELECT * FROM ResignationLetters WHERE YEAR(ResignationDate) = YEAR(GETDATE()) AND Department = @Department AND EmpId='" + Session["EmpId"].ToString() + "'";
                        command.Parameters.AddWithValue("@Department", DepartmentSession);

                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            ResignData.Controls.Clear();
                            int RowCount = 1;
                            while (reader.Read())
                            {
                                string Name = reader["Name"].ToString();
                                string ResignStatus = reader["ResignStatus"].ToString();
                                string Id = reader["EmpId"].ToString();
                                string Department = reader["Department"].ToString();

                                DateTime NoticeDate1 = Convert.ToDateTime(reader["NoticeDate"]);
                                string NoticeDate = NoticeDate1.ToString("yyyy-MM-dd");

                                DateTime resignDate = Convert.ToDateTime(reader["ResignationDate"]);
                                string ResignDate = resignDate.ToString("yyyy-MM-dd");

                                string Reason = reader["Reason"].ToString();
                                string ReasonStatus = reader["ResignStatus"].ToString();
                                string ApprovedeBy = reader["ApprovedeBy"].ToString();
                                string ApproveDate = reader["ApproveDate"].ToString();

                                StringBuilder projectHtml = new StringBuilder();
                                projectHtml.Append("<tr>");
                                projectHtml.Append("<td><a href='profile.html'>" + Name + " </a></td>");
                                projectHtml.Append("<td>" + Id + "</td>");
                                projectHtml.Append("<td style='color:blue;'>" + Department + "</td>");
                                projectHtml.Append("<td>" + Reason + "</td>");
                                projectHtml.Append("<td>" + NoticeDate + "</td>");
                                projectHtml.Append("<td>" + ResignDate + "</td>");
                                projectHtml.Append("<td>" + ReasonStatus + "</td>");

                                if (ResignStatus != "Rejected" && ResignStatus != "Approved")
                                {
                                   // projectHtml.Append("<td><a class='dropdown-item' href='#' data-bs-toggle='modal' data-bs-target='#edit_resignation' onclick=\"editResign('" + Name + "','" + Id + "','" + ResignDate + "','" + Reason + "','" + ApprovedeBy + "','" + ApproveDate + "')\" ><i class='fa-solid fa-pencil m-r-5' style='color:#057a28;'></i></a></td>");
                                }

                                projectHtml.Append("</tr>");
                                ResignData.Controls.Add(new LiteralControl(projectHtml.ToString()));

                                RowCount++;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception and provide user-friendly error handling
                // For example, log the error to a file, event log, or database
                // Show a user-friendly message to the user
                Response.Write("An error occurred: " + ex.Message);
            }
        }


        protected void btnsubmitresign_Click(object sender, EventArgs e)
        {
            string EmpId = Session["EmpId"].ToString();
            string Name = Session["Name"].ToString();
            string Company = Session["CompanyName"].ToString();
            string Branch = Session["BranchName"].ToString();
            string Department = Session["DepartmentName"].ToString();
            string Designation = Session["Designation"].ToString();
            string ResignStatus = "Pending";

            DateTime currentDate = DateTime.Now;
            string formattedDate = currentDate.ToString("yyyy-MM-dd");
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("INSERT INTO ResignationLetters(EmpId,Name,Company,Branch,Department,Designation,NoticeDate,ResignationDate,Reason,Status,ResignStatus,ApprovedeStatus,ApprovedeBy,ApproveDate,ManagerApprove) VALUES(@EmpId,@Name,@Company,@Branch,@Department,@Designation,@NoticeDate,@ResignationDate,@Reason,@Status,@ResignStatus,@ApprovedeStatus,@ApprovedeBy,@ApproveDate,@ManagerApprove)", connection);
                    cmd.Parameters.AddWithValue("@EmpId", EmpId);
                    cmd.Parameters.AddWithValue("@Name", Name);
                    cmd.Parameters.AddWithValue("@Company", Company);
                    cmd.Parameters.AddWithValue("@Branch", Branch);
                    cmd.Parameters.AddWithValue("@Department", Department);
                    cmd.Parameters.AddWithValue("@Designation", Designation);
                    cmd.Parameters.AddWithValue("@NoticeDate", currentDate);
                    cmd.Parameters.AddWithValue("@ResignationDate", txtdate.Text);
                    cmd.Parameters.AddWithValue("@Reason", txtreason.Text);
                    cmd.Parameters.AddWithValue("@Status", "0");
                    cmd.Parameters.AddWithValue("@ResignStatus", ResignStatus);
                    cmd.Parameters.AddWithValue("@ApprovedeStatus", ResignStatus);
                    cmd.Parameters.AddWithValue("@ApprovedeBy", DBNull.Value);
                    cmd.Parameters.AddWithValue("@ApproveDate", DBNull.Value);
                    cmd.Parameters.AddWithValue("@ManagerApprove", ResignStatus);

                    int i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        Response.Write("<script>alert('Resign Success Wait for Approval...')</script>");
                        BindResignData();
                    }
                    else
                    {
                        Response.Write("<script>alert('Failed...')</script>");
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