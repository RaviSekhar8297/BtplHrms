using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Human_Resource_Management
{
    public partial class ManagerResignation : System.Web.UI.Page
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
                        command.CommandText = "SELECT * FROM ResignationLetters WHERE YEAR(ResignationDate) = YEAR(GETDATE()) AND Department = @Department";
                        command.Parameters.AddWithValue("@Department", DepartmentSession);

                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            ManagerResignData.Controls.Clear();
                            int RowCount = 1;
                            while (reader.Read())
                            {
                                string Name = reader["Name"].ToString();
                                string ResignStatus = reader["ResignStatus"].ToString();
                                string EmpId = reader["EmpId"].ToString();
                                string Id = reader["Id"].ToString();
                                string Department = reader["Department"].ToString();

                                DateTime NoticeDate1 = Convert.ToDateTime(reader["NoticeDate"]);
                                string NoticeDate = NoticeDate1.ToString("yyyy-MM-dd");

                                DateTime resignDate = Convert.ToDateTime(reader["ResignationDate"]);
                                string ResignDate = resignDate.ToString("yyyy-MM-dd");

                                string Reason = reader["Reason"].ToString();
                                string ReasonStatus = reader["ResignStatus"].ToString();
                                string ApprovedeBy = reader["ApprovedeBy"].ToString();
                                string ApproveDate = reader["ApproveDate"].ToString();
                                string ManagerApprove = reader["ManagerApprove"].ToString();

                                StringBuilder projectHtml = new StringBuilder();
                                projectHtml.Append("<tr>");
                                projectHtml.Append("<td><a href='profile.html'>" + Name + " </a></td>");
                                projectHtml.Append("<td>" + EmpId + "</td>");
                                projectHtml.Append("<td>" + Department + "</td>");
                                projectHtml.Append("<td>" + Reason + "</td>");
                                projectHtml.Append("<td>" + NoticeDate + "</td>");
                                projectHtml.Append("<td>" + ResignDate + "</td>");
                                projectHtml.Append("<td>" + ManagerApprove + "</td>");

                                if (ManagerApprove.Trim() == "Rejected" || ManagerApprove.Trim() == "Approved")
                                {

                                }
                                else
                                {
                                     projectHtml.Append("<td><a class='dropdown-item' href='#' data-bs-toggle='modal' data-bs-target='#edit_resignation' onclick=\"editResign('" + Name + "','" + Id + "','" + ResignDate + "','" + Reason + "','" + EmpId + "')\" ><i class='fa-solid fa-pencil m-r-5' style='color:#057a28;'></i></a></td>");
                                }

                                projectHtml.Append("</tr>");
                                ManagerResignData.Controls.Add(new LiteralControl(projectHtml.ToString()));

                                RowCount++;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
               
                Response.Write("An error occurred: " + ex.Message);
            }
        }

        protected void btnReject_Click(object sender, EventArgs e)
        {
            string ManagerApproved = "Rejected";
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("Update ResignationLetters set ManagerApprove=@ManagerApprove where Id=@Id and EmpId=@EmpId", connection);
                    cmd.Parameters.AddWithValue("@ManagerApprove", ManagerApproved);
                    cmd.Parameters.AddWithValue("@Id", HiddenFieldId.Value);
                    cmd.Parameters.AddWithValue("@EmpId", HiddenFieldEmpId.Value);
                    int i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        Response.Write("<script>alert('Rejected...')</script>");
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

        protected void btnApprove_Click(object sender, EventArgs e)
        {
           

            string ManagerApproved = "Approved";
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("Update ResignationLetters set ManagerApprove=@ManagerApprove where Id=@Id and EmpId=@EmpId", connection);
                    cmd.Parameters.AddWithValue("@ManagerApprove", ManagerApproved);
                    cmd.Parameters.AddWithValue("@Id", HiddenFieldId.Value);
                    cmd.Parameters.AddWithValue("@EmpId", HiddenFieldEmpId.Value);
                    int i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        Response.Write("<script>alert('Approved Successfully...')</script>");
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
                    SqlCommand cmd = new SqlCommand("INSERT INTO ResignationLetters(EmpId,Name,Company,Branch,Department,Designation,NoticeDate,ResignationDate,Reason,Status,ResignStatus,ApprovedeStatus,ApprovedeBy,ApproveDate,ManagerApprove) VALUES(@EmpId,@Name,@Company,@Branch,@Department,@Designation,@NoticeDate,@ResignationDate,@Reason,@Status,@ResignStatus,@ApprovedeStatus,@ApprovedeBy,@ApproveDate,ManagerApprove)", connection);
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