using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlTypes;
using AjaxControlToolkit.HtmlEditor.ToolbarButtons;
using DocumentFormat.OpenXml.Wordprocessing;
using Org.BouncyCastle.Asn1.Cmp;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Vml;

namespace Human_Resource_Management
{
    public partial class AdminAddPerformanceIndicator : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            btnreview.Visible = true;
            Label1.Text = "";
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
                        sqlCmd.CommandText = "SELECT CompanyId,CompanyName FROM Companies order by CompanyName asc ";
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
                        ddlcompany.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Select Company--", "0"));
                    }
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
                        sqlCmd.CommandText = "select Branch_Id, BranchName, BranchCode from Branch where [CompanyId] = '" + ddlcompany.SelectedValue + "' order by BranchName asc ";
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
                        sqlCmd.CommandText = "SELECT DeptId,Department FROM Department where BranchId='" + ddlbranch.SelectedValue + "'  order by Department asc ";
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
                        ddldepartment.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Select departmen --", "0"));
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
                        sqlCmd.CommandText = "select * from [Employees]  where status= 1  and DepartmentId= '" + ddldepartment.SelectedValue + "' order by FirstName asc ";
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
                txtdesignation.Text = null;
                txtid.Text = null;

                string connstrg = ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString;

                using (SqlConnection sqlConn = new SqlConnection(connstrg))
                {
                    // First, retrieve the employee details
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
                                txtid.Text = sdr["EmpId"].ToString();
                            }
                            sdr.Close();
                        }
                        sqlConn.Close();
                    }

                    int currentYear = DateTime.Now.Year;
                    string checkQuery = "SELECT * FROM [EmpPerformenceDetails] WHERE EmpId = @EmpId and Status='1'";
                    using (SqlCommand checkCmd = new SqlCommand(checkQuery, sqlConn))
                    {
                        checkCmd.Parameters.AddWithValue("@EmpId", ddlname.SelectedValue);
                        sqlConn.Open();
                        SqlDataAdapter sqlda = new SqlDataAdapter(checkCmd);
                        System.Data.DataTable dt = new System.Data.DataTable();
                        sqlda.Fill(dt);
                        int RowCount = dt.Rows.Count;                     
                        sqlConn.Close();
                        IEnumerable<int> years = dt.AsEnumerable().Where(row => row["Year"] != DBNull.Value && !string.IsNullOrWhiteSpace(row["Year"].ToString())).Select(row => int.TryParse(row["Year"].ToString().Trim(), out int year) ? year : 0); 

                        int maxYear = years.Any() ? years.Max() : 0;
                        if (currentYear == maxYear)
                        {
                            Label1.Text = "You Have already Review by the  " + ddlname.SelectedItem.Text + " ";
                            btnreview.Visible = false;
                        }
                        else
                        {
                              InserEmpReviews();
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions appropriately
                Label1.Text = "Error: " + ex.Message;
            }
        }

        protected void btnreview_Click(object sender, EventArgs e)
        {
            try
            {
                string connstrg = ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString;
                int year = DateTime.Now.Year;

                using (SqlConnection sqlConn = new SqlConnection(connstrg))
                {
                    sqlConn.Open();

                    SqlCommand cmd = new SqlCommand(
                        "UPDATE EmpPerformenceDetails SET Status=@Status, Regular = @Regular, Behaviour = @Behaviour, Work = @Work, OvarallReview = @OvarallReview, " +
                        "ReviewBy = @ReviewBy, ReviewDate = @ReviewDate, ReviewTotalMarks = @ReviewTotalMarks, Year = @Year " +
                        "WHERE EmpId = @EmpId AND EmpName = @EmpName AND Status = '0'",
                        sqlConn);

                    cmd.Parameters.AddWithValue("@EmpId", Convert.ToInt32(ddlname.SelectedItem.Value));
                    cmd.Parameters.AddWithValue("@EmpName", ddlname.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@Status", "1");
                    cmd.Parameters.AddWithValue("@Regular", Convert.ToInt32(Rating1.CurrentRating));
                    cmd.Parameters.AddWithValue("@Behaviour", Convert.ToInt32(Rating2.CurrentRating));
                    cmd.Parameters.AddWithValue("@Work", Convert.ToInt32(Rating3.CurrentRating));
                    cmd.Parameters.AddWithValue("@OvarallReview", hiddenOverallReview.Value); // Use the hidden field value
                    double ovrReview = Rating1.CurrentRating + Rating2.CurrentRating + Rating3.CurrentRating;
                    double total = (ovrReview / 15) * 100;
                    cmd.Parameters.AddWithValue("@ReviewBy", Session["Name"].ToString());
                    cmd.Parameters.AddWithValue("@ReviewDate", DateTime.Now);
                    cmd.Parameters.AddWithValue("@ReviewTotalMarks", total);
                    cmd.Parameters.AddWithValue("@Year", year);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        Label1.ForeColor = System.Drawing.Color.Green;
                        Label1.Text = "Giving review to " + ddlname.SelectedItem.Text + " was successfully updated";
                        ddlcompany.ClearSelection();
                        ddlbranch.ClearSelection();
                        ddldepartment.ClearSelection();
                        ddlname.ClearSelection();
                        txtid.Text = "";
                        txtdesignation.Text = "";
                        Rating1.CurrentRating = 0;
                        Rating2.CurrentRating = 0;
                        Rating3.CurrentRating = 0;
                        txtovarallreview.Text = "";
                    }
                    else
                    {
                        Label1.ForeColor = System.Drawing.Color.Red;
                        Label1.Text = "Giving review to " + ddlname.SelectedItem.Text + " was failed to update";
                    }
                }
            }
            catch (Exception ex)
            {
                Label1.ForeColor = System.Drawing.Color.Red;
                Label1.Text = "An error occurred: " + ex.Message;
            }

        }
        public void InserEmpReviews()
        {
            try
            {
                string connstrg = ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString;
                int Year = DateTime.Now.Year;

                using (SqlConnection sqlConn = new SqlConnection(connstrg))
                {
                    sqlConn.Open(); 

                    SqlCommand cmd = new SqlCommand(
                        "INSERT INTO EmpPerformenceDetails (EmpId, EmpName, CompanyId, CompanyName, BranchId, BranchName, DepartmentId, DepartmentName, Designation, BranchCode, Status, Regular, Behaviour, Work, OvarallReview, ReviewBy, ReviewDate, ReviewTotalMarks, Year) VALUES (@EmpId, @EmpName, @CompanyId, @CompanyName, @BranchId, @BranchName, @DepartmentId, @DepartmentName, @Designation, @BranchCode, @Status, @Regular, @Behaviour, @Work, @OvarallReview, @ReviewBy, @ReviewDate, @ReviewTotalMarks, @Year)",
                        sqlConn);

                    cmd.Parameters.AddWithValue("@EmpId", Convert.ToInt32(ddlname.SelectedItem.Value));
                    cmd.Parameters.AddWithValue("@EmpName", ddlname.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@CompanyId", Convert.ToInt32(ddlcompany.SelectedValue));
                    cmd.Parameters.AddWithValue("@CompanyName", ddlcompany.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@BranchId", Convert.ToInt32(ddlbranch.SelectedValue));
                    cmd.Parameters.AddWithValue("@BranchName", ddlbranch.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@DepartmentId", Convert.ToInt32(ddldepartment.SelectedValue));
                    cmd.Parameters.AddWithValue("@DepartmentName", ddldepartment.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@Designation", txtdesignation.Text);
                    cmd.Parameters.AddWithValue("@BranchCode", Convert.ToInt32(ddlbranch.SelectedValue));
                    cmd.Parameters.AddWithValue("@Status", "0");
                    cmd.Parameters.AddWithValue("@Regular", DBNull.Value);
                    cmd.Parameters.AddWithValue("@Behaviour", DBNull.Value);
                    cmd.Parameters.AddWithValue("@Work", DBNull.Value);
                    cmd.Parameters.AddWithValue("@OvarallReview", DBNull.Value);
                    cmd.Parameters.AddWithValue("@ReviewBy", DBNull.Value);
                    cmd.Parameters.AddWithValue("@ReviewDate", DBNull.Value);
                    cmd.Parameters.AddWithValue("@ReviewTotalMarks", DBNull.Value);
                    cmd.Parameters.AddWithValue("@Year", DBNull.Value);
                    cmd.ExecuteNonQuery();
  
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

      
    }
}