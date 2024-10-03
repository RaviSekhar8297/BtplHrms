using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using iTextSharp.text.pdf;

namespace Human_Resource_Management
{
    public partial class AdminAddAssets : System.Web.UI.Page
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
                        sqlCmd.CommandText = "SELECT CompanyId,CompanyName FROM Companies order by CompanyName asc "; //where CompanyName='" + Session["CompanyName"].ToString() + "'
                        sqlCmd.Connection = sqlConn;
                        sqlConn.Open();
                        SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        ddlcompany.DataSource = dt;
                        ddlcompany.DataValueField = "CompanyId";
                        ddlcompany.DataTextField = "CompanyName";
                        ddlcompany.DataBind();

                        sqlConn.Close();

                        ddlcompany.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Select Company --", "0"));

                    }
                    HiddenField1.Value = "1";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void ddlcompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                String connstrg = ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString;
                using (SqlConnection sqlConn = new SqlConnection(connstrg))
                {
                    using (SqlCommand sqlCmd = new SqlCommand())
                    {
                        sqlCmd.CommandText = "select Branch_Id, BranchName, BranchCode from Branch where  [CompanyId] = '" + ddlcompany.SelectedValue + "' order by BranchName asc";
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

        protected void ddlbranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                String connstrg = ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString;
                using (SqlConnection sqlConn = new SqlConnection(connstrg))
                {
                    using (SqlCommand sqlCmd = new SqlCommand())
                    {
                        sqlCmd.CommandText = "SELECT DeptId,Department FROM Department where BranchId='" + ddlbranch.SelectedValue + "' order by Department asc";
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
                        ddldepartment.Items.Insert(0, new ListItem("-- Select department --", "0"));
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

                String connstrg = ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString;
                using (SqlConnection sqlConn = new SqlConnection(connstrg))
                {
                    using (SqlCommand sqlCmd = new SqlCommand())
                    {
                        sqlCmd.CommandText = "select EmpId,FirstName from [Employees]  where status= 1  and DepartmentId= '" + ddldepartment.SelectedValue + "' order by FirstName asc";
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

        protected void ddlname_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                String connstrg = ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString;
                using (SqlConnection sqlConn = new SqlConnection(connstrg))
                {
                    string query = "SELECT * FROM [Employees] WHERE status = 1 AND EmpId = @EmpId";
                    using (SqlCommand sqlCmd = new SqlCommand(query, sqlConn))
                    {
                        sqlCmd.Parameters.AddWithValue("@EmpId", ddlname.SelectedValue);

                        sqlConn.Open();
                        using (SqlDataReader sdr = sqlCmd.ExecuteReader())
                        {
                            if (sdr.Read())
                            {
                                txtdesignation.Text = sdr["Designation"].ToString();
                                txtempid.Text = sdr["EmpId"].ToString();
                            }
                            sdr.Close();
                        }
                        sqlConn.Close();
                    }
                }
                using (SqlConnection sqlConn = new SqlConnection(connstrg))
                {
                    string query = "SELECT COUNT(*) FROM Assets WHERE EmpId = @EmpId";

                    using (SqlCommand sqlCmd = new SqlCommand(query, sqlConn))
                    {
                        sqlCmd.Parameters.AddWithValue("@EmpId", ddlname.SelectedValue);

                        sqlConn.Open();
                        int count = (int)sqlCmd.ExecuteScalar();

                        if (count > 0)
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "alreadyAssigned", "alert('Assets already assigned to " + ddlname.SelectedItem.Text + "');", true);
                            ddlname.ClearSelection();
                            txtdesignation.Text = "";
                            txtempid.Text = "";
                        }
                        else
                        {

                        }
                    }
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnaddassets_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection strcon = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    strcon.Open();
                    string empId = ddlname.SelectedValue;

                    SqlCommand selectCmd = new SqlCommand("SELECT * FROM [Employees] WHERE status = 1 AND EmpId = @EmpId", strcon);
                    selectCmd.Parameters.AddWithValue("@EmpId", empId);

                    using (SqlDataReader sdr = selectCmd.ExecuteReader())
                    {
                        if (sdr.Read())
                        {
                            string empName = sdr["FirstName"].ToString();
                            string companyEmail = sdr["CompanyEmail"].ToString();
                            string designation = sdr["Designation"].ToString();
                            string companyCellNo = sdr["CompanyCellNo"].ToString();

                            if (!string.IsNullOrEmpty(empName) && empName != "0")
                            {
                               sdr.Close();
                                SqlCommand cmd = new SqlCommand(@"INSERT INTO Assets (EmpId, Name, Company, Branch, Department, Designation, LapTop, Mouse, Pendrive, Mobile, Bag, Sim, AssignDate, AssignedBy, Status, Remarks, ReturnStatus, ReturnDate, Returnremarks)
                                                      VALUES (@EmpId, @Name, @Company, @Branch, @Department, @Designation, @LapTop, @Mouse, @Pendrive, @Mobile, @Bag, @Sim, @AssignDate, @AssignedBy, @Status, @Remarks, @ReturnStatus, @ReturnDate, @Returnremarks)", strcon);
                                cmd.Parameters.AddWithValue("@EmpId", empId);
                                cmd.Parameters.AddWithValue("@Name", empName);
                                cmd.Parameters.AddWithValue("@Company", ddlcompany.SelectedItem.Text);
                                cmd.Parameters.AddWithValue("@Branch", ddlbranch.SelectedItem.Text);
                                cmd.Parameters.AddWithValue("@Department", ddldepartment.SelectedItem.Text);
                                cmd.Parameters.AddWithValue("@Designation", txtdesignation.Text);
                                cmd.Parameters.AddWithValue("@LapTop", cblaptop.Checked ? "True" : "False");
                                cmd.Parameters.AddWithValue("@Mouse", cbmouse.Checked ? "True" : "False");
                                cmd.Parameters.AddWithValue("@Pendrive", cbpendrive.Checked ? "True" : "False");
                                cmd.Parameters.AddWithValue("@Mobile", cbmobile.Checked ? "True" : "False");
                                cmd.Parameters.AddWithValue("@Bag", cbbag.Checked ? "True" : "False");
                                cmd.Parameters.AddWithValue("@Sim", cbsim.Checked ? "True" : "False");
                                cmd.Parameters.AddWithValue("@AssignDate", DateTime.Now);
                                cmd.Parameters.AddWithValue("@AssignedBy", Session["Name"].ToString());
                                cmd.Parameters.AddWithValue("@Status", "1");
                                cmd.Parameters.AddWithValue("@Remarks", txtremarks.Text);
                                cmd.Parameters.AddWithValue("@ReturnStatus", "0");
                                cmd.Parameters.AddWithValue("@ReturnDate", DBNull.Value);
                                cmd.Parameters.AddWithValue("@Returnremarks", DBNull.Value);

                                int i = cmd.ExecuteNonQuery();
                                if (i > 0)
                                {
                                    Response.Write("<script>alert('Assets assigned Successfully....')</script>");
                                    Response.Redirect("AdminAssets.aspx");
                                    ddlcompany.ClearSelection();
                                    ddlbranch.ClearSelection();
                                    ddldepartment.ClearSelection();
                                    ddlname.ClearSelection();
                                    txtdesignation.Text = "";
                                    txtremarks.Text = "";
                                    cblaptop.Checked = false;
                                    cbmouse.Checked = false;
                                    cbpendrive.Checked = false;
                                    cbmobile.Checked = false;
                                    cbbag.Checked = false;
                                    cbsim.Checked = false;
                                }
                                else
                                {
                                    Response.Write("<script>alert('Failed to assign assets.')</script>");
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