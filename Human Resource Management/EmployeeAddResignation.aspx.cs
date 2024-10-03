using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Human_Resource_Management
{
    public partial class EmployeeResignation : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindCompany();
            }

        }
        public void BindCompany()
        {
            try
            {
                using (SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    using (SqlCommand sqlCmd = new SqlCommand())
                    {
                        sqlCmd.CommandText = "SELECT CompanyId,CompanyName FROM Companies  "; //where CompanyName='" + Session["CompanyName"].ToString() + "'
                        sqlCmd.Connection = sqlConn;
                        sqlConn.Open();
                        SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        ddlcompanyresign.DataSource = dt;
                        ddlcompanyresign.DataValueField = "CompanyId";
                        ddlcompanyresign.DataTextField = "CompanyName";
                        ddlcompanyresign.DataBind();

                        sqlConn.Close();

                        ddlcompanyresign.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Select Company--", "0"));

                    }
                    HiddenField1.Value = "1";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void ddlcompanyresign_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                String connstrg = ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString;
                using (SqlConnection sqlConn = new SqlConnection(connstrg))
                {
                    using (SqlCommand sqlCmd = new SqlCommand())
                    {
                        sqlCmd.CommandText = "select Branch_Id, BranchName, BranchCode from Branch where  BranchName='" + Session["BranchName"].ToString() + "' ";
                        sqlCmd.Connection = sqlConn;
                        sqlConn.Open();
                        SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        ddlbranchresign.DataSource = dt;
                        ddlbranchresign.DataValueField = "BranchCode";
                        ddlbranchresign.DataTextField = "BranchName";
                        ddlbranchresign.DataBind();
                        sqlConn.Close();
                        ddlbranchresign.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Select Branch --", "0"));
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void ddlbranchresign_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string depa = Session["DepartmentName"].ToString();
                String connstrg = ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString;
                using (SqlConnection sqlConn = new SqlConnection(connstrg))
                {
                    using (SqlCommand sqlCmd = new SqlCommand())
                    {
                        sqlCmd.CommandText = "SELECT DeptId,Department FROM Department where Department='" + depa + "' ";
                        sqlCmd.Connection = sqlConn;
                        sqlConn.Open();
                        SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        ddldepartmentresign.DataSource = dt;
                        ddldepartmentresign.DataValueField = "DeptId";
                        ddldepartmentresign.DataTextField = "Department";
                        ddldepartmentresign.DataBind();
                        sqlConn.Close();
                        ddldepartmentresign.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Select Department --", "0"));
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void ddldepartmentresign_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string name = Session["Name"].ToString();
                string depa = Session["DepartmentName"].ToString();
                String connstrg = ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString;
                using (SqlConnection sqlConn = new SqlConnection(connstrg))
                {
                    using (SqlCommand sqlCmd = new SqlCommand())
                    {
                        sqlCmd.CommandText = "select * from Employees  where Status= 1  and FirstName= '" + name + "' and department='" + depa + "'";
                        sqlCmd.Connection = sqlConn;
                        sqlConn.Open();
                        SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        ddlnameresign.DataSource = dt;
                        ddlnameresign.DataValueField = "EmpId";
                        ddlnameresign.DataTextField = "FirstName";
                        ddlnameresign.DataBind();
                        sqlConn.Close();
                        ddlnameresign.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Select Name --", "0"));
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void ddlnameresign_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                String connstrg = ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString;
                using (SqlConnection sqlConn = new SqlConnection(connstrg))
                {
                    using (SqlCommand sqlCmd = new SqlCommand())
                    {
                        sqlCmd.CommandText = "select * from  [Employees]  where status= 1  and Designation= '" + Session["Designation"].ToString() + "'";
                        sqlCmd.Connection = sqlConn;
                        sqlConn.Open();
                        SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        ddldesignationresign.DataSource = dt;
                        ddldesignationresign.DataValueField = "EmpId";
                        ddldesignationresign.DataTextField = "Designation";
                        ddldesignationresign.DataBind();
                        sqlConn.Close();
                        ddldesignationresign.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Select Department --", "0"));
                    }
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void btnadd_Click(object sender, EventArgs e)
        {
            //  string Status = "1";
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
                    cmd.Parameters.AddWithValue("@EmpId", ddlnameresign.SelectedItem.Value);
                    cmd.Parameters.AddWithValue("@Name", ddlnameresign.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@Company", ddlcompanyresign.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@Branch", ddlbranchresign.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@Department", ddldepartmentresign.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@Designation", ddldesignationresign.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@NoticeDate", currentDate);
                    cmd.Parameters.AddWithValue("@ResignationDate", txtaddresigndate.Text);
                    cmd.Parameters.AddWithValue("@Reason", txtaddreason.Text);
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
                        Response.Redirect("EmployeeResignation.aspx");
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
        protected void ddldesignationresign_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}