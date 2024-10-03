using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Human_Resource_Management
{
    public partial class ManagerPayslip : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            payslipiddiv.Visible = false;
        }

        protected void txtmonth_TextChanged(object sender, EventArgs e)
        {
            payslipiddiv.Visible = true;
            string selectedMonthYear = txtmonth.Text;

            // Parse the selected month and year, it will automatically set the day to 1
            DateTime selectedDate = DateTime.ParseExact(selectedMonthYear, "yyyy-MM", null);

            int year = selectedDate.Year;  // Extract the year
            int month = selectedDate.Month; // Extract the month

            try
            {
                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    // SQL query to match the year and month (ignoring the day)
                    using (SqlCommand sqlcmd = new SqlCommand("SELECT * FROM Payslip WHERE EmpId = @EmpId AND YEAR(Date) = @Year AND MONTH(Date) = @Month", connection))
                    {
                        sqlcmd.Parameters.AddWithValue("@EmpId", Session["EmpId"].ToString());
                        sqlcmd.Parameters.AddWithValue("@Year", year);
                        sqlcmd.Parameters.AddWithValue("@Month", month);

                        connection.Open();

                        using (SqlDataReader myReader = sqlcmd.ExecuteReader())
                        {
                            if (myReader.Read())
                            {
                                lblpayslipid.Text = myReader["PayslipId"].ToString();
                                lblpayslipmonth.Text = myReader["Month"].ToString();
                                lblpayslipempname.Text = myReader["Name"].ToString();
                                lblpayslipdesignation.Text = myReader["Designation"].ToString();
                                lblpayslipempid.Text = myReader["EmpId"].ToString();

                                if (myReader["DOJ"] != DBNull.Value && DateTime.TryParse(myReader["DOJ"].ToString(), out DateTime doj))
                                {
                                    lblpayslipdoj.Text = doj.ToString("yyyy-MM-dd");
                                }

                                lbnetSalary.Text = myReader["NetSalary"].ToString();

                                // Earnings
                                lblpayslipbasicSalary.Text = myReader["BasicSalary"].ToString();
                                lblpaysliphra.Text = myReader["HRA"].ToString();
                                lblpayslipca.Text = myReader["CA"].ToString();
                                lblpayslipother.Text = myReader["EA"].ToString();
                                lblpayslipsa.Text = myReader["SA"].ToString();
                                lbltotalearning.Text = myReader["GrossSalary"].ToString();

                                // Deductions
                                lblpaysliptds.Text = myReader["TDSTaxAmount"].ToString();
                                lblpf.Text = myReader["PF"].ToString();
                                lblesi.Text = myReader["ESI"].ToString();
                                lblpayslipprofessonaltax.Text = myReader["ProfessionalTax"].ToString();
                                lbllatelogindeduction.Text = myReader["latelogindetuction"].ToString();
                                lbltotaldeductions.Text = myReader["PfEsiProfTaxandLLDeduction"].ToString();
                                lblSalaryword.Text = myReader["Amountwords"].ToString();
                            }
                            else
                            {
                                Response.Write("<script>alert('No payslip updated for the selected month.')</script>");
                                payslipiddiv.Visible = false;
                                resetvalues();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception (e.g., log the error)
                throw ex;
            }

        }
        public void resetvalues()
        {
            lblpayslipid.Text = "";
            lblpayslipmonth.Text = "";
            lblpayslipempname.Text = "";
            lblpayslipdesignation.Text = "";
            lblpayslipempid.Text = "";
            lblpayslipdoj.Text = "";
            lblpayslipbasicSalary.Text = "";
            lblpaysliphra.Text = "";
            lblpayslipca.Text = "";
            lblpaysliptds.Text = "";
            lblpf.Text = "";
            lblesi.Text = "";
            lbltotaldeductions.Text = "";
            lbnetSalary.Text = "";
            lblSalaryword.Text = "";
            lblpayslipother.Text = "";
            lbltotalearning.Text = "";
            lblpayslipsa.Text = "";
            lblpayslipprofessonaltax.Text = "";
            lbllatelogindeduction.Text = "";
            lblpayslipmonthyear.Text = "";
            txtmonth.Text = "";
        }
    }
}