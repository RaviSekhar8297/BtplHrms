using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Threading;
using System.Globalization;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using System.Text;


namespace Human_Resource_Management
{
    public partial class EmployeePayslip : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            payslipiddiv.Visible = false;
            CalendarExtender1.EndDate = DateTime.Now;
           
        }

        protected void txtStartDate_TextChanged(object sender, EventArgs e)
        {
            payslipiddiv.Visible = true;
            DateTime startDate = DateTime.ParseExact(txtStartDate.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            int month = startDate.Month; 

            DateTime currentDate = new DateTime(2024, month, 1);
            string monthName = currentDate.ToString("MMMM");

            int year = DateTime.Parse(txtStartDate.Text).Year;
            int month1 = DateTime.Parse(txtStartDate.Text).Month;

            DateTime currentDate1 = DateTime.ParseExact(txtStartDate.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            string monthYearString = currentDate1.ToString("MMMM yyyy").ToUpper();
            string YearString = currentDate1.ToString("yyyy").ToUpper();
            lblpayslipmonthyear.Text = monthYearString;

            try
            {
                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    using (SqlCommand sqlcmd = new SqlCommand("SELECT * FROM Payslip WHERE EmpId = @EmpId AND Year=@Year  AND Month = @Month", connection))
                    {
                        sqlcmd.Parameters.AddWithValue("@EmpId", Session["Empid"].ToString());
                        sqlcmd.Parameters.AddWithValue("@Year", year);
                        sqlcmd.Parameters.AddWithValue("@Month", month1);
                        connection.Open();

                        using (SqlDataReader myReader = sqlcmd.ExecuteReader())
                        {
                            if (myReader.Read())
                            {
                                if (myReader["Year"].ToString() == year.ToString() && Convert.ToInt32(myReader["Month"]) == month1)
                                {
                                    lblpayslipid.Text = myReader["PayslipId"] != DBNull.Value ? myReader["PayslipId"].ToString() : "0";
                                    lblpayslipmonth.Text = myReader["Month"] != DBNull.Value ? myReader["Month"].ToString() : "0";
                                    lblpayslipempname.Text = myReader["Name"] != DBNull.Value ? myReader["Name"].ToString() : "N/A";
                                    lblpayslipdesignation.Text = myReader["Designation"] != DBNull.Value ? myReader["Designation"].ToString() : "N/A";
                                    lblpayslipempid.Text = myReader["EmpId"] != DBNull.Value ? myReader["EmpId"].ToString() : "0";

                                    if (myReader["DOJ"] != DBNull.Value && DateTime.TryParse(myReader["DOJ"].ToString(), out DateTime doj))
                                    {
                                        lblpayslipdoj.Text = doj.ToString("yyyy-MM-dd");
                                    }
                                    else
                                    {
                                        lblpayslipdoj.Text = "N/A"; // or some other default value
                                    }

                                    lbnetSalary.Text = myReader["NetSalary"] != DBNull.Value ? myReader["NetSalary"].ToString() : "0";

                                    // Earnings
                                    lblpayslipbasicSalary.Text = myReader["BasicSalary"] != DBNull.Value ? myReader["BasicSalary"].ToString() : "0";
                                    lblpaysliphra.Text = myReader["HRA"] != DBNull.Value ? myReader["HRA"].ToString() : "0";
                                    lblpayslipca.Text = myReader["CA"] != DBNull.Value ? myReader["CA"].ToString() : "0";
                                    lblpayslipother.Text = myReader["EA"] != DBNull.Value ? myReader["EA"].ToString() : "0";
                                    lblpayslipsa.Text = myReader["SA"] != DBNull.Value ? myReader["SA"].ToString() : "0";
                                    lbltotalearning.Text = myReader["GrossSalary"] != DBNull.Value ? myReader["GrossSalary"].ToString() : "0";

                                    // Deductions
                                    lblpaysliptds.Text = myReader["TDSTaxAmount"] != DBNull.Value ? myReader["TDSTaxAmount"].ToString() : "0";
                                    lblpf.Text = myReader["PF"] != DBNull.Value ? myReader["PF"].ToString() : "0";
                                    lblesi.Text = myReader["ESI"] != DBNull.Value ? myReader["ESI"].ToString() : "0";
                                    lblpayslipprofessonaltax.Text = myReader["ProfessionalTax"] != DBNull.Value ? myReader["ProfessionalTax"].ToString() : "0";
                                    lbllatelogindeduction.Text = myReader["latelogindetuction"] != DBNull.Value ? myReader["latelogindetuction"].ToString() : "0";
                                    lbltotaldeductions.Text = myReader["PfEsiProfTaxandLLDeduction"] != DBNull.Value ? myReader["PfEsiProfTaxandLLDeduction"].ToString() : "0";
                                    lblSalaryword.Text = myReader["Amountwords"] != DBNull.Value ? myReader["Amountwords"].ToString() : "0";

                                    lopded.Text = myReader["OnlyLOPDeduction"] != DBNull.Value ? myReader["OnlyLOPDeduction"].ToString() : "0";
                                    loanded.Text = myReader["PayAmount"] != DBNull.Value ? myReader["PayAmount"].ToString() : "0";

                                }
                                else
                                {

                                }
                            }
                            else
                            {
                                Response.Write("<script>alert(' The " + monthYearString + " Month  here No Pay slip updated')</script>");
                                payslipiddiv.Visible = false;
                                resetvalues();
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
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
            txtStartDate.Text = "";

        }

       

    }
}