using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text;
using System.Text;
using DocumentFormat.OpenXml.Bibliography;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TreeView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using DocumentFormat.OpenXml.Wordprocessing;

namespace Human_Resource_Management
{
    public partial class EmployeeLoan : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoanListData();
                LoanHistoryList();
            }
        }
        public void LoanListData()
        {
            int sessionid = Convert.ToInt32(Session["EmpId"]);
            try
            {
                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    using (SqlCommand sqlcmd = new SqlCommand("SELECT * from LoanData1 where EmpId=@EmpId", connection))
                    {
                        sqlcmd.Parameters.AddWithValue("@EmpId", sessionid); // Using parameterized queries to prevent SQL Injection
                        connection.Open();
                        SqlDataAdapter da1 = new SqlDataAdapter(sqlcmd);
                        DataSet ds1 = new DataSet();
                        da1.Fill(ds1);
                        int sln = 1;

                        if (ds1.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow row in ds1.Tables[0].Rows)
                            {
                                string Name = row["Name"] != DBNull.Value ? row["Name"].ToString() : string.Empty;
                                string Id = row["Id"] != DBNull.Value ? row["Id"].ToString() : string.Empty;
                                string LoanAmount = row["LoanAmount"] != DBNull.Value ? row["LoanAmount"].ToString() : string.Empty; 
                                string EmpId = row["EmpId"] != DBNull.Value ? row["EmpId"].ToString() : string.Empty;
                                string LoanApplyDate = row["LoanApplyDate"] != DBNull.Value ? row["LoanApplyDate"].ToString() : string.Empty;
                                string LoanStatus = row["LoanStatus"] != DBNull.Value ? row["LoanStatus"].ToString() : string.Empty;
                                string ApprovedBy = row["ApprovedBy"] != DBNull.Value ? row["ApprovedBy"].ToString() : string.Empty;
                                string Status = row["Status"] != DBNull.Value ? row["Status"].ToString() : string.Empty;
                                StringBuilder projectHtml = new StringBuilder();

                                projectHtml.Append("<tr>");
                                projectHtml.Append("<td>" + sln + "</td>");
                                projectHtml.Append("<td>" + Name + "</td>");
                                projectHtml.Append("<td>" + EmpId + "</td>");
                                projectHtml.Append("<td>" + LoanAmount + "</td>");
                                projectHtml.Append("<td>" + LoanApplyDate + "</td>");
                                projectHtml.Append("<td>" + ApprovedBy + "</td>");

                                if (LoanStatus.Trim() == "Pending")
                                {
                                    projectHtml.Append("<td style='color:#b53f18;'>" + LoanStatus + "</td>");
                                }
                                else
                                {
                                    projectHtml.Append("<td style='color:#279134;'>" + LoanStatus + "</td>");
                                }
                                if (Status.Trim() == "3")
                                {

                                }
                                else
                                {
                                    projectHtml.Append("<td><a class='edit' href='#' data-bs-toggle='modal' data-bs-target='#edit_loanamount' onclick =\"editLoanAmount('" + Id + "','" + EmpId + "','" + LoanAmount + "')\" ><i class='fa-solid fa-pencil m-r-5' style='color:green;'></i></a></td>");
                                    projectHtml.Append("<td><a class='delete ' href='#' data-bs-toggle='modal' data-bs-target='#delete_loanamoun' onclick =\"deleteLoanList('" + Id + "','" + EmpId + "','" + LoanAmount + "')\"><i class='fa-regular fa-trash-can m-r-5' style='color:red;'></i></a></td>");
                                }
                               
                                projectHtml.Append("</tr>");

                                LoanList.Controls.Add(new LiteralControl(projectHtml.ToString()));
                                sln++;
                            }
                        }
                        else
                        {
                            // No records found, display message
                            StringBuilder noRecordsHtml = new StringBuilder();
                            noRecordsHtml.Append("<tr>");
                            noRecordsHtml.Append("<td colspan='7' style='text-align:center; color:#b53f18;'>No records found</td>");
                            noRecordsHtml.Append("</tr>");

                            LoanList.Controls.Add(new LiteralControl(noRecordsHtml.ToString()));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void LoanHistoryList()
        {
            int sessionid = Convert.ToInt32(Session["EmpId"]);
            try
            {
                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    using (SqlCommand sqlcmd = new SqlCommand("SELECT * from LoanHistory where EmpId=@EmpId", connection))
                    {
                        sqlcmd.Parameters.AddWithValue("@EmpId", sessionid); // Using parameterized queries to prevent SQL Injection
                        connection.Open();
                        SqlDataAdapter da1 = new SqlDataAdapter(sqlcmd);
                        DataSet ds1 = new DataSet();
                        da1.Fill(ds1);
                        int sln = 1;

                        if (ds1.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow row in ds1.Tables[0].Rows)
                            {
                                string Name = row["Name"] != DBNull.Value ? row["Name"].ToString() : string.Empty;
                                string Id = row["Id"] != DBNull.Value ? row["Id"].ToString() : string.Empty;
                                string LoanAmount = row["LoanAmount"] != DBNull.Value ? row["LoanAmount"].ToString() : string.Empty;
                                string EmpId = row["EmpId"] != DBNull.Value ? row["EmpId"].ToString() : string.Empty;
                                string PayAmount = row["PayAmount"] != DBNull.Value ? row["PayAmount"].ToString() : string.Empty;
                                string LoanStatus = row["LoanStatus"] != DBNull.Value ? row["LoanStatus"].ToString() : string.Empty;
                                string ClearAmount = row["ClearAmount"] != DBNull.Value ? row["ClearAmount"].ToString() : string.Empty;

                                string RemainingAmount = row["RemainingAmount"] != DBNull.Value ? row["RemainingAmount"].ToString() : string.Empty;
                                string PaymentDate = row["PaymentDate"] != DBNull.Value ? row["PaymentDate"].ToString() : string.Empty;

                                StringBuilder projectHtml = new StringBuilder();

                                projectHtml.Append("<tr>");
                                projectHtml.Append("<td>" + sln + "</td>");
                                projectHtml.Append("<td>" + Name + "</td>");
                               
                                projectHtml.Append("<td>" + LoanAmount + "</td>");
                                projectHtml.Append("<td>" + ClearAmount + "</td>");
                                projectHtml.Append("<td>" + RemainingAmount + "</td>");
                                projectHtml.Append("<td>" + PayAmount + "</td>");
                                projectHtml.Append("<td>" + PaymentDate + "</td>");
                                projectHtml.Append("<td>" + LoanStatus + "</td>");

                                if (LoanStatus.Trim() == "Pending")
                                {
                                    projectHtml.Append("<td style='color:#b53f18;'>" + LoanStatus + "</td>");
                                }
                                else
                                {
                                    projectHtml.Append("<td style='color:#279134;'>" + LoanStatus + "</td>");
                                }
                                projectHtml.Append("</tr>");

                                LoanHistory.Controls.Add(new LiteralControl(projectHtml.ToString()));
                                sln++;
                            }
                        }
                        else
                        {
                            // No records found, display message
                            StringBuilder noRecordsHtml = new StringBuilder();
                            noRecordsHtml.Append("<tr>");
                            noRecordsHtml.Append("<td colspan='7' style='text-align:center; color:#b53f18;'>No records found</td>");
                            noRecordsHtml.Append("</tr>");

                            LoanHistory.Controls.Add(new LiteralControl(noRecordsHtml.ToString()));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnapplyloan_Click(object sender, EventArgs e)
        {
            // Status  0 is apply Loan and pending 
            //  1 is Approved Loan
            // 2 is Completed Loan amount 
            // 3 is Delete loan list
            // 4 is Rejeted Loan 


            int sessionid = Convert.ToInt32(Session["EmpId"]);
            try
            {
                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    connection.Open();

                    // Step 1: Check if a loan with Status = 1 already exists for the user
                    using (SqlCommand checkCmd = new SqlCommand(
                        "SELECT COUNT(*) FROM LoanData1 WHERE EmpId = @EmpId AND Status IN (0, 1, 2)", connection))
                    {
                        checkCmd.Parameters.Add(new SqlParameter("@EmpId", SqlDbType.Int) { Value = sessionid });

                        int loanCount = (int)checkCmd.ExecuteScalar(); // Get count of existing loans with Status = 1

                        // Step 2: If loanCount > 0, show an alert and return
                        if (loanCount > 0)
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('You have already applied for a loan.');", true);
                            txtreason.Text = "";
                            txtloan.Text = "";
                            LoanListData();
                            return;
                        }
                        else
                        {
                            // Step 3: If no existing loan with Status = 1, proceed to insert the new loan
                            using (SqlCommand insertCmd = new SqlCommand(
                                "INSERT INTO LoanData1 (Name, EmpId, LoanAmount,ApprovedBy, LoanApplyDate, ClearAmount, BalenceAmount, LoanStatus, Status,Reason) " +
                                "VALUES (@Name, @EmpId, @LoanAmount,@ApprovedBy ,@LoanApplyDate, @ClearAmount, @BalenceAmount, @LoanStatus, @Status,@Reason)", connection))
                            {
                                // Validate and convert loan amount
                                string reason = txtreason.Text;
                                decimal loanAmount;
                                if (!decimal.TryParse(txtloan.Text, out loanAmount))
                                {
                                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Invalid loan amount.');", true);
                                    return;
                                }

                                // Add parameters for the insert command
                                insertCmd.Parameters.Add(new SqlParameter("@Name", SqlDbType.VarChar) { Value = Session["Name"].ToString() });
                                insertCmd.Parameters.Add(new SqlParameter("@EmpId", SqlDbType.Int) { Value = sessionid });
                                insertCmd.Parameters.Add(new SqlParameter("@LoanAmount", SqlDbType.Decimal) { Value = loanAmount });
                                insertCmd.Parameters.Add(new SqlParameter("@ApprovedBy", SqlDbType.VarChar) { Value = "Pending" });
                                insertCmd.Parameters.Add(new SqlParameter("@LoanApplyDate", SqlDbType.DateTime) { Value = DateTime.Now });
                                insertCmd.Parameters.Add(new SqlParameter("@ClearAmount", SqlDbType.Decimal) { Value = 0 });
                                insertCmd.Parameters.Add(new SqlParameter("@BalenceAmount", SqlDbType.Decimal) { Value = loanAmount });
                                insertCmd.Parameters.Add(new SqlParameter("@LoanStatus", SqlDbType.VarChar) { Value = "Pending" });
                                insertCmd.Parameters.Add(new SqlParameter("@Status", SqlDbType.Int) { Value = 0 });
                                insertCmd.Parameters.Add(new SqlParameter("@Reason", SqlDbType.VarChar) { Value = reason });

                                // Step 4: Execute the insert command
                                insertCmd.ExecuteNonQuery();
                                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Loan application submitted successfully.');", true);

                                // Clear fields
                                txtloan.Text = "";
                                txtreason.Text = "";

                                // Refresh the loan list
                                LoanListData();
                                LoanHistoryList();
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('Database error: {ex.Message}');", true);
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('Error: {ex.Message}');", true);
            }

        }

        protected void btneditloanamount_Click(object sender, EventArgs e)
        {
            try
            {
                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("UPDATE LoanData1 SET LoanAmount=@LoanAmount, BalenceAmount=@BalenceAmount WHERE EmpId=@EmpId AND Id=@Id", connection))
                    {
                        connection.Open();

                        // Validate and parse the loan amount
                        decimal loanAmount;
                        if (!decimal.TryParse(txteditloanamount.Text, out loanAmount))
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Invalid loan amount.');", true);
                            return;
                        }

                        // Adding parameters with specific SQL types
                        cmd.Parameters.Add("@LoanAmount", SqlDbType.Decimal).Value = loanAmount;
                        cmd.Parameters.Add("@BalenceAmount", SqlDbType.Decimal).Value = loanAmount;
                        cmd.Parameters.Add("@EmpId", SqlDbType.NVarChar).Value = hdfediloanamountempid.Value;
                        cmd.Parameters.Add("@Id", SqlDbType.NVarChar).Value = hdfediloanamountid.Value;

                        // Execute the query and check for affected rows
                        int i = cmd.ExecuteNonQuery();
                        if (i > 0)
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Loan Amount Updated Successfully.');", true);
                            LoanListData();
                            LoanHistoryList();
                        }
                        else
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Failed to update loan amount.');", true);
                            LoanListData();
                            LoanHistoryList();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle the exception appropriately (e.g., logging)
                throw ex;
            }

        }

        protected void btndeleteloanamount_Click(object sender, EventArgs e)
        {
            try
            {
                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("UPDATE LoanData1 SET Status='3',LoanStatus='Deleted by self',ApprovedBy=@ApprovedBy WHERE EmpId=@EmpId AND Id=@Id", connection))
                    {
                        connection.Open();

                        cmd.Parameters.Add(new SqlParameter("@ApprovedBy", SqlDbType.VarChar) { Value = "Self" });
                       
                        cmd.Parameters.Add("@EmpId", SqlDbType.NVarChar).Value = hdfediloanamountempid.Value;
                        cmd.Parameters.Add("@Id", SqlDbType.NVarChar).Value = hdfediloanamountid.Value;

                        // Execute the query and check for affected rows
                        int i = cmd.ExecuteNonQuery();
                        if (i > 0)
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Loan Record deleted successfully.');", true);
                            LoanListData();
                            LoanHistoryList();
                        }
                        else
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Failed to delete loan record.');", true);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('An error occurred " + ex + " while trying to delete the loan record.');", true);
            }

        }
    }
}