using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Human_Resource_Management
{
    public partial class AdminLoan : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PendingLoanData();
                ApprovedLoanData();
                RejectedLoanData();
                HistoryLoanData();
                PayLoanDataBind();
            }
        }
        public void PayLoanDataBind()
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString;
                DateTime currentDate = DateTime.Now;
                DateTime firstDayOfCurrentMonth = new DateTime(currentDate.Year, currentDate.Month, 1);
                DateTime lastDayOfPreviousMonth = firstDayOfCurrentMonth.AddDays(-1);

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Query LoanData1
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM LoanData1 WHERE Status = '1'", conn))
                    {
                        SqlDataAdapter da1 = new SqlDataAdapter(cmd);
                        DataSet ds1 = new DataSet();
                        da1.Fill(ds1);

                        bool btnPayAmountVisible = false;
                        bool hasSalaryData = false;
                        bool hasLoanHistoryThisMonth = false;

                        if (ds1.Tables[0].Rows.Count > 0)
                        {
                            int S_No = 1;
                            foreach (DataRow row in ds1.Tables[0].Rows)
                            {
                                string Id = row["Id"].ToString();
                                string EmpId = row["EmpId"].ToString();
                                string Name = row["Name"].ToString();
                                double LoanAmount = Convert.ToDouble(row["LoanAmount"]);
                                DateTime? cdob = row["LoanApplyDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(row["LoanApplyDate"]);
                                string ApplyDate = cdob.HasValue ? cdob.Value.ToString("yyyy-MM-dd") : string.Empty;
                                double ClearAmount = Convert.ToDouble(row["ClearAmount"]);
                                int BalanceAmount = Convert.ToInt32(row["BalenceAmount"]);
                                string LoanStatus = row["LoanStatus"].ToString();

                                // Query PaySlip table for salary data
                                double netSalary = 0;
                                using (SqlCommand salaryCmd = new SqlCommand("SELECT ISNULL(SUM(CAST(NetSalary AS DECIMAL(18, 2))), 0) FROM PaySlip WHERE EmpID = @EmpID AND date BETWEEN @StartDate AND @EndDate", conn))
                                {
                                    salaryCmd.Parameters.AddWithValue("@EmpID", EmpId);
                                    salaryCmd.Parameters.AddWithValue("@StartDate", new DateTime(lastDayOfPreviousMonth.Year, lastDayOfPreviousMonth.Month, 1));
                                    salaryCmd.Parameters.AddWithValue("@EndDate", lastDayOfPreviousMonth);

                                    netSalary = Convert.ToDouble(salaryCmd.ExecuteScalar());
                                }

                                // Check if there's any LoanHistory for the current month
                                using (SqlCommand loanHistoryCmd = new SqlCommand("SELECT COUNT(*) FROM LoanHistory WHERE EmpId = @EmpId AND MONTH(PaymentDate) = @Month AND YEAR(PaymentDate) = @Year", conn))
                                {
                                    loanHistoryCmd.Parameters.AddWithValue("@EmpId", EmpId);
                                    loanHistoryCmd.Parameters.AddWithValue("@Month", currentDate.Month);
                                    loanHistoryCmd.Parameters.AddWithValue("@Year", currentDate.Year);

                                    hasLoanHistoryThisMonth = (int)loanHistoryCmd.ExecuteScalar() > 0;
                                }

                                // Determine button visibility based on salary data and loan history
                                if (netSalary > 0 && !hasLoanHistoryThisMonth)
                                {
                                    btnPayAmountVisible = true;
                                    hasSalaryData = true;
                                }

                                StringBuilder projectHtml = new StringBuilder();
                                projectHtml.Append("<tr>");
                                projectHtml.Append("<td>" + Name + "</td>");
                                projectHtml.Append("<td>" + EmpId + "</td>");
                                projectHtml.Append("<td>" + LoanAmount + "</td>");
                                projectHtml.Append("<td>" + ApplyDate + "</td>");
                                projectHtml.Append("<td>" + ClearAmount + "</td>");
                                projectHtml.Append("<td>" + BalanceAmount + "</td>");
                                projectHtml.Append("<td>" + LoanStatus + "</td>");
                                projectHtml.Append("<td><a class='edit' href='#' data-bs-toggle='modal' data-bs-target='#edit_payLoanAmount' onclick=\"payLoanAmount('" + EmpId + "','" + Name + "','" + LoanAmount + "','" + ClearAmount + "','" + BalanceAmount + "','" + netSalary + "','" + Id + "')\" ><i class='fa-solid fa-pencil m-r-5' style='color:green;'></i></a></td>");
                                projectHtml.Append("</tr>");

                                PayLoan.Controls.Add(new LiteralControl(projectHtml.ToString()));
                                S_No++;
                            }

                            // Update labels based on salary data and loan history
                            if (!hasSalaryData)
                            {
                                Label1.Text = "The loan amount should be paid after generating the payroll";
                            }
                            else
                            {
                                Label1.Text = string.Empty; // Clear the label if salary data is present
                            }

                            if (hasLoanHistoryThisMonth)
                            {
                                Label2.Text = "Loan Amount already paid at this month";
                            }
                            else
                            {
                                Label2.Text = string.Empty; // Clear the label if loan history does not indicate payment
                            }

                            // Set button visibility based on loan history and salary data
                            btnpayamount.Visible = btnPayAmountVisible;
                        }
                        else
                        {
                            Label1.Text = "No loan data available.";
                            btnpayamount.Visible = false;
                            StringBuilder noRecordsHtml = new StringBuilder();
                            noRecordsHtml.Append("<tr>");
                            noRecordsHtml.Append("<td colspan='7' style='text-align:center; color:#b53f18;'>No records found</td>");
                            noRecordsHtml.Append("</tr>");

                            PayLoan.Controls.Add(new LiteralControl(noRecordsHtml.ToString()));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        protected void btnpayamount_Click(object sender, EventArgs e)
        {
            double net;

            if (double.TryParse(TextBox4.Text, out net))
            {

            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Please enter a valid number.');", true);
                net = 0; 
            }

            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString;

                double totalamount, clearAmount, balenceamount, payamount;

                // Validate and convert input
                if (!double.TryParse(TextBox1.Text, out totalamount))
                {
                    Label1.Text = "Invalid total amount.";
                    return;
                }

                if (!double.TryParse(TextBox2.Text, out clearAmount))
                {
                    Label1.Text = "Invalid clear amount.";
                    return;
                }

                if (!double.TryParse(TextBox3.Text, out balenceamount))
                {
                    Label1.Text = "Invalid balance amount.";
                    return;
                }

                if (!double.TryParse(TextBox5.Text, out payamount))
                {
                    Label1.Text = "Invalid pay amount.";
                    return;
                }

                double totalclearAmount = clearAmount + payamount;
                double remainingamount = totalamount - totalclearAmount;

                string loanStatus = remainingamount == 0 ? "Completed" : "Process";
                DateTime currentDate = DateTime.Now;

                double words = net - payamount;

                string salarywords = ConvertNumberToWords((int)words);

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Update LoanData1 table
                    using (SqlCommand cmd = new SqlCommand("UPDATE LoanData1 SET ClearAmount = @ClearAmount, BalenceAmount = @BalenceAmount,Status=@Status,ClearStatus=@ClearStatus WHERE EmpId = @EmpId AND Id = @Id", conn))
                    {
                        cmd.Parameters.AddWithValue("@ClearAmount", totalclearAmount);
                        cmd.Parameters.AddWithValue("@BalenceAmount", remainingamount);
                        if (remainingamount == 0)
                        {
                            cmd.Parameters.AddWithValue("@Status", 2);
                            cmd.Parameters.AddWithValue("@ClearStatus", "Completed");
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@Status", 1);
                            cmd.Parameters.AddWithValue("@ClearStatus", "Pending");
                        }
                        cmd.Parameters.AddWithValue("@EmpId", HiddenField1.Value);
                        cmd.Parameters.AddWithValue("@Id", HiddenField2.Value);

                        cmd.ExecuteNonQuery();
                    }

                    // Update PaySlip table
                    using (SqlCommand cmd = new SqlCommand(@"UPDATE PaySlip SET LoanAmount = @LoanAmount, ClearAmount = @ClearAmount, DueAmount = @DueAmount, LoanStatus = @LoanStatus, PayAmount = @PayAmount, NetSalary = CONVERT(DECIMAL(18, 2), NetSalary) - @LoanBill ,Amountwords=@Amountwords WHERE EmpId = @EmpId AND Year = @Year AND Month = @Month", conn))
                    {
                        // Calculate the previous month and year
                        DateTime currentDate1 = DateTime.Now;
                        DateTime firstDayOfCurrentMonth = new DateTime(currentDate1.Year, currentDate1.Month, 1);
                        DateTime firstDayOfPreviousMonth = firstDayOfCurrentMonth.AddMonths(-1);
                        int previousMonth = firstDayOfPreviousMonth.Month;
                        int previousYear = firstDayOfPreviousMonth.Year;

                        // Add parameters
                        cmd.Parameters.AddWithValue("@LoanAmount", totalamount);
                        cmd.Parameters.AddWithValue("@ClearAmount", totalclearAmount);
                        cmd.Parameters.AddWithValue("@DueAmount", remainingamount);
                        cmd.Parameters.AddWithValue("@LoanStatus", "Sucess");
                        cmd.Parameters.AddWithValue("@PayAmount", payamount);
                        cmd.Parameters.AddWithValue("@LoanBill", payamount);
                        cmd.Parameters.AddWithValue("@Amountwords", salarywords);
                        cmd.Parameters.AddWithValue("@Year", previousYear);
                        cmd.Parameters.AddWithValue("@Month", previousMonth);
                        cmd.Parameters.AddWithValue("@EmpId", HiddenField1.Value);

                        cmd.ExecuteNonQuery();
                    }




                    // Insert into LoanHistory table
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO LoanHistory (Name, EmpId, LoanAmount, PayAmount, ClearAmount, RemainingAmount, PaymentDate, LoanStatus, Status) VALUES (@Name, @EmpId, @LoanAmount, @PayAmount, @ClearAmount, @RemainingAmount, @PaymentDate, @LoanStatus, @Status)", conn))
                    {
                        cmd.Parameters.AddWithValue("@Name", txtdeptname.Text);
                        cmd.Parameters.AddWithValue("@EmpId", HiddenField1.Value);
                        cmd.Parameters.AddWithValue("@LoanAmount", totalamount);
                        cmd.Parameters.AddWithValue("@PayAmount", payamount);
                        cmd.Parameters.AddWithValue("@ClearAmount", totalclearAmount);
                        cmd.Parameters.AddWithValue("@RemainingAmount", remainingamount);
                        cmd.Parameters.AddWithValue("@PaymentDate", currentDate);
                        cmd.Parameters.AddWithValue("@LoanStatus", "Sucess");
                        cmd.Parameters.AddWithValue("@Status", 0);

                        cmd.ExecuteNonQuery();
                    }
                }

                // Trigger JavaScript alert after successful operations
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Payment Updated Successfully!');", true);
                PayLoanDataBind();
            }
            catch (Exception ex)
            {
                // Log the error (log it instead of rethrowing in production)
                Label1.Text = "An error occurred: " + ex.Message;
            }
        }


        public void PendingLoanData()
        {
            try
            {
                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    using (SqlCommand sqlcmd = new SqlCommand("SELECT  LoanData1.* ,   Employees.Image FROM  LoanData1 INNER JOIN   Employees ON LoanData1.EmpId = Employees.EmpId WHERE  LoanData1.LoanStatus = 'Pending';", connection))
                    {                      
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
                                string LoanApplyDate = row["LoanApplyDate"] != DBNull.Value ? Convert.ToDateTime(row["LoanApplyDate"]).ToString("yyyy-MM-dd") : string.Empty;

                                string LoanStatus = row["LoanStatus"] != DBNull.Value ? row["LoanStatus"].ToString() : string.Empty;
                                string ApprovedBy = row["ApprovedBy"] != DBNull.Value ? row["ApprovedBy"].ToString() : string.Empty;
                                string Status = row["Status"] != DBNull.Value ? row["Status"].ToString() : string.Empty;
                                string Reason = row["Reason"] != DBNull.Value ? row["Reason"].ToString() : string.Empty;

                                string imageUrl = null;
                                object imageDataObj = row["Image"];
                                if (imageDataObj != DBNull.Value)
                                {
                                    byte[] imageData = (byte[])imageDataObj;
                                    string base64String = Convert.ToBase64String(imageData);
                                    imageUrl = "data:image/jpeg;base64," + base64String;
                                }

                                StringBuilder projectHtml = new StringBuilder();

                                projectHtml.Append("<tr>");
                                projectHtml.Append("<td><a href='#' class='avatar'><img src='" + imageUrl + "' alt='User Image'></a>");
                                projectHtml.Append(" " + Name + "</td>");
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
                               
                                    projectHtml.Append("<td><a class='edit' href='#' data-bs-toggle='modal' data-bs-target='#edit_pendingloan' onclick =\"editPendingLoans('" + Id + "','" + EmpId + "','" + Name + "','" + LoanAmount + "','"+ LoanApplyDate + "','"+ Reason + "')\" ><i class='fa-solid fa-pencil m-r-5' style='color:green;'></i></a></td>");
                                   // projectHtml.Append("<td><a class='delete ' href='#' data-bs-toggle='modal' data-bs-target='#delete_loanamoun' onclick =\"deleteLoanList('" + Id + "','" + EmpId + "','" + LoanAmount + "')\"><i class='fa-regular fa-trash-can m-r-5' style='color:red;'></i></a></td>");
                                

                                projectHtml.Append("</tr>");

                                PendingLoans.Controls.Add(new LiteralControl(projectHtml.ToString()));
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

                            PendingLoans.Controls.Add(new LiteralControl(noRecordsHtml.ToString()));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ApprovedLoanData()
        {
            try
            {
                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    using (SqlCommand sqlcmd = new SqlCommand("SELECT  LoanData1.* ,   Employees.Image FROM  LoanData1 INNER JOIN   Employees ON LoanData1.EmpId = Employees.EmpId WHERE  LoanData1.LoanStatus = 'Approved';", connection))
                    {
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
                                string ApproveDate = row["ApproveDate"] != DBNull.Value ? row["ApproveDate"].ToString() : string.Empty;
                               

                                string imageUrl = null;
                                object imageDataObj = row["Image"];
                                if (imageDataObj != DBNull.Value)
                                {
                                    byte[] imageData = (byte[])imageDataObj;
                                    string base64String = Convert.ToBase64String(imageData);
                                    imageUrl = "data:image/jpeg;base64," + base64String;
                                }

                                StringBuilder projectHtml = new StringBuilder();

                                projectHtml.Append("<tr>");
                                projectHtml.Append("<td><a href='#' class='avatar'><img src='" + imageUrl + "' alt='User Image'></a>");
                                projectHtml.Append(" " + Name + "</td>");
                                projectHtml.Append("<td>" + EmpId + "</td>");
                                projectHtml.Append("<td>" + LoanAmount + "</td>");                              
                                projectHtml.Append("<td>" + LoanApplyDate + "</td>");
                                projectHtml.Append("<td>" + ApproveDate + "</td>");
                                projectHtml.Append("<td>" + ApprovedBy + "</td>");

                                if (LoanStatus.Trim() == "Pending")
                                {
                                    projectHtml.Append("<td style='color:#b53f18;'>" + LoanStatus + "</td>");
                                }
                                else
                                {
                                    projectHtml.Append("<td style='color:#279134;'>" + LoanStatus + "</td>");
                                }

                              //  projectHtml.Append("<td><a class='edit' href='#' data-bs-toggle='modal' data-bs-target='#edit_pendingloan' onclick =\"editPendingLoans('" + Id + "','" + EmpId + "','" + LoanAmount + "')\" ><i class='fa-solid fa-pencil m-r-5' style='color:green;'></i></a></td>");
                                // projectHtml.Append("<td><a class='delete ' href='#' data-bs-toggle='modal' data-bs-target='#delete_loanamoun' onclick =\"deleteLoanList('" + Id + "','" + EmpId + "','" + LoanAmount + "')\"><i class='fa-regular fa-trash-can m-r-5' style='color:red;'></i></a></td>");


                                projectHtml.Append("</tr>");

                                ApprovedLoans.Controls.Add(new LiteralControl(projectHtml.ToString()));
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

                            ApprovedLoans.Controls.Add(new LiteralControl(noRecordsHtml.ToString()));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void RejectedLoanData()
        {
            try
            {
                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    using (SqlCommand sqlcmd = new SqlCommand("SELECT  LoanData1.* ,   Employees.Image FROM  LoanData1 INNER JOIN   Employees ON LoanData1.EmpId = Employees.EmpId WHERE  LoanData1.LoanStatus = 'Rejected';", connection))
                    {
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
                                string ApproveDate = row["ApproveDate"] != DBNull.Value ? row["ApproveDate"].ToString() : string.Empty;

                                string imageUrl = null;
                                object imageDataObj = row["Image"];
                                if (imageDataObj != DBNull.Value)
                                {
                                    byte[] imageData = (byte[])imageDataObj;
                                    string base64String = Convert.ToBase64String(imageData);
                                    imageUrl = "data:image/jpeg;base64," + base64String;
                                }

                                StringBuilder projectHtml = new StringBuilder();

                                projectHtml.Append("<tr>");
                                projectHtml.Append("<td><a href='#' class='avatar'><img src='" + imageUrl + "' alt='User Image'></a>");
                                projectHtml.Append(" " + Name + "</td>");
                                projectHtml.Append("<td>" + EmpId + "</td>");
                                projectHtml.Append("<td>" + LoanAmount + "</td>");
                                projectHtml.Append("<td>" + LoanApplyDate + "</td>");
                                projectHtml.Append("<td>" + ApproveDate + "</td>");
                                projectHtml.Append("<td>" + ApprovedBy + "</td>");

                                if (LoanStatus.Trim() == "Pending")
                                {
                                    projectHtml.Append("<td style='color:#b53f18;'>" + LoanStatus + "</td>");
                                }
                                else
                                {
                                    projectHtml.Append("<td style='color:#279134;'>" + LoanStatus + "</td>");
                                }

                                //  projectHtml.Append("<td><a class='edit' href='#' data-bs-toggle='modal' data-bs-target='#edit_pendingloan' onclick =\"editPendingLoans('" + Id + "','" + EmpId + "','" + LoanAmount + "')\" ><i class='fa-solid fa-pencil m-r-5' style='color:green;'></i></a></td>");
                                // projectHtml.Append("<td><a class='delete ' href='#' data-bs-toggle='modal' data-bs-target='#delete_loanamoun' onclick =\"deleteLoanList('" + Id + "','" + EmpId + "','" + LoanAmount + "')\"><i class='fa-regular fa-trash-can m-r-5' style='color:red;'></i></a></td>");


                                projectHtml.Append("</tr>");

                                RejectedLoans.Controls.Add(new LiteralControl(projectHtml.ToString()));
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

                            RejectedLoans.Controls.Add(new LiteralControl(noRecordsHtml.ToString()));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void HistoryLoanData()
        {
            try
            {
                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    using (SqlCommand sqlcmd = new SqlCommand("SELECT  LoanHistory.* ,   Employees.Image FROM  LoanHistory INNER JOIN   Employees ON LoanHistory.EmpId = Employees.EmpId ", connection))
                    {
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

                                string imageUrl = null;
                                object imageDataObj = row["Image"];
                                if (imageDataObj != DBNull.Value)
                                {
                                    byte[] imageData = (byte[])imageDataObj;
                                    string base64String = Convert.ToBase64String(imageData);
                                    imageUrl = "data:image/jpeg;base64," + base64String;
                                }

                                StringBuilder projectHtml = new StringBuilder();

                                projectHtml.Append("<tr>");
                                projectHtml.Append("<td><a href='#' class='avatar'><img src='" + imageUrl + "' alt='User Image'></a>");
                                projectHtml.Append(" " + Name + "</td>");
                                projectHtml.Append("<td>" + EmpId + "</td>");
                                projectHtml.Append("<td>" + LoanAmount + "</td>");
                                projectHtml.Append("<td>" + ClearAmount + "</td>");
                                projectHtml.Append("<td>" + RemainingAmount + "</td>");

                                projectHtml.Append("<td>" + PayAmount + "</td>");
                                projectHtml.Append("<td>" + PaymentDate + "</td>");
                              

                                if (LoanStatus.Trim() == "Pending")
                                {
                                    projectHtml.Append("<td style='color:#b53f18;'>" + LoanStatus + "</td>");
                                }
                                else
                                {
                                    projectHtml.Append("<td style='color:#279134;'>" + LoanStatus + "</td>");
                                }

                                //  projectHtml.Append("<td><a class='edit' href='#' data-bs-toggle='modal' data-bs-target='#edit_pendingloan' onclick =\"editPendingLoans('" + Id + "','" + EmpId + "','" + LoanAmount + "')\" ><i class='fa-solid fa-pencil m-r-5' style='color:green;'></i></a></td>");
                                // projectHtml.Append("<td><a class='delete ' href='#' data-bs-toggle='modal' data-bs-target='#delete_loanamoun' onclick =\"deleteLoanList('" + Id + "','" + EmpId + "','" + LoanAmount + "')\"><i class='fa-regular fa-trash-can m-r-5' style='color:red;'></i></a></td>");


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

        protected void btnloanapprove_Click(object sender, EventArgs e)
        {
            try
            {
                string approved = "Approved";
                string name = Session["Name"]?.ToString() ?? "Unknown";  // Ensure session value is not null

                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    connection.Open(); // You need to open the connection

                    using (SqlCommand cmd = new SqlCommand("UPDATE LoanData1 SET LoanStatus=@LoanStatus, ApprovedBy=@ApprovedBy, ApproveDate=@ApproveDate,Status = 1 WHERE EmpId=@EmpId AND Id=@Id", connection))
                    {
                        // Use proper parameter types for better performance
                        cmd.Parameters.Add("@LoanStatus", SqlDbType.NVarChar).Value = approved;
                        cmd.Parameters.Add("@ApprovedBy", SqlDbType.NVarChar).Value = name;
                        cmd.Parameters.Add("@ApproveDate", SqlDbType.DateTime).Value = DateTime.Now;
                        cmd.Parameters.Add("@EmpId", SqlDbType.NVarChar).Value = HiddenField4empid.Value;
                        cmd.Parameters.Add("@Id", SqlDbType.NVarChar).Value = HiddenField3id.Value;

                        int i = cmd.ExecuteNonQuery();

                        if (i > 0)
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Loan Approved successfully.');", true);
                        }
                        else
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Approval failed.');", true);
                        }

                        // Refresh data after approval
                        PendingLoanData();
                        ApprovedLoanData();
                        RejectedLoanData();
                    }
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('Error: {ex.Message}');", true);
            }

        }

        protected void btnloanreject_Click(object sender, EventArgs e)
        {
            try
            {
                string rejected = "Rejected";
                string name = Session["Name"]?.ToString() ?? "Unknown"; // Null check for session

                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    connection.Open(); // Ensure the connection is opened

                    using (SqlCommand cmd = new SqlCommand("UPDATE LoanData1 SET LoanStatus=@LoanStatus, ApprovedBy=@ApprovedBy, ApproveDate=@ApproveDate ,Status = 4 WHERE EmpId=@EmpId AND Id=@Id", connection))
                    {
                        cmd.Parameters.Add("@LoanStatus", SqlDbType.NVarChar).Value = rejected;
                        cmd.Parameters.Add("@ApprovedBy", SqlDbType.NVarChar).Value = name;
                        cmd.Parameters.Add("@ApproveDate", SqlDbType.DateTime).Value = DateTime.Now;
                        cmd.Parameters.Add("@EmpId", SqlDbType.NVarChar).Value = HiddenField4empid.Value;
                        cmd.Parameters.Add("@Id", SqlDbType.NVarChar).Value = HiddenField3id.Value;

                        int i = cmd.ExecuteNonQuery();
                        if (i > 0)
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Loan Rejected successfully.');", true);
                        }
                        else
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Failed to reject loan.');", true);
                        }
                        PendingLoanData();
                        ApprovedLoanData();
                        RejectedLoanData();
                    }
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('Error: {ex.Message}');", true);
            }

        }


        public static string ConvertNumberToWords(decimal number)
        {
            if (number == 0)
                return "zero rupees";

            if (number < 0)
                return "minus " + ConvertNumberToWords(Math.Abs(number));

            string words = "";

            // Split the number into integer and fractional parts
            int integerPart = (int)number;
            int fractionalPart = (int)((number - integerPart) * 100); // Consider 2 decimal places

            if (integerPart > 0)
            {
                words += ConvertIntegerPartToWords(integerPart);
            }

            if (fractionalPart > 0)
            {
                words += " and " + ConvertIntegerPartToWords(fractionalPart) + " paise";
            }
            else
            {
                words += " rupees";
            }

            return words.Trim();
        }
        private static string ConvertIntegerPartToWords(int number)
        {
            if (number == 0)
                return "zero";

            if (number < 0)
                return "minus " + ConvertIntegerPartToWords(Math.Abs(number));

            string words = "";
            if ((number / 100000) > 0)
            {
                words += ConvertIntegerPartToWords(number / 100000) + " Lakh ";
                number %= 100000;
            }

            if ((number / 1000) > 0)
            {
                words += ConvertIntegerPartToWords(number / 1000) + " Thousand ";
                number %= 1000;
            }

            if ((number / 100) > 0)
            {
                words += ConvertIntegerPartToWords(number / 100) + " Hundred ";
                number %= 100;
            }

            if (number > 0)
            {
                string[] units = new string[] {
                "Zero", "One", "Two", "Three", "Four",
                "Five", "Six", "Seven", "Eight", "Nine",
                "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen",
                "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen"
            };
                string[] tens = new string[] {
                "", "", "Twenty", "Thirty", "Forty",
                "Fifty", "Sixty", "Seventy", "Eighty", "Ninety"
            };

                if (number < 20)
                    words += units[number];
                else
                {
                    words += tens[number / 10];
                    if ((number % 10) > 0)
                        words += "-" + units[number % 10];
                }
            }

            return words.Trim();
        }
    }
}