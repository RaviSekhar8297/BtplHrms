using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Globalization;

namespace Human_Resource_Management
{
    public partial class AdminSalaryView : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            payslipiddiv.Visible = false;
            btnback.Visible = false ;
            CalendarExtender1.EndDate = DateTime.Now;
            if (!IsPostBack)
            {
                string employeeId = Request.QueryString["EmployeeId"];
                if (!string.IsNullOrEmpty(employeeId))
                {
                    Session["Empid"] = employeeId;
                }
            }
        }
        protected void txtStartDate_TextChanged(object sender, EventArgs e)
        {
            string EmId = Session["Empid"].ToString();
            payslipiddiv.Visible = true;
            btnback.Visible = true;
            DateTime startDate = DateTime.ParseExact(txtStartDate.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            int month = startDate.Month;

            DateTime currentDate = new DateTime(2024, month, 1);
            string monthName = currentDate.ToString("MMMM");

            int year = DateTime.Parse(txtStartDate.Text).Year;

            DateTime currentDate1 = DateTime.ParseExact(txtStartDate.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            string monthYearString = currentDate1.ToString("MMM yyyy").ToUpper();
            string YearString = currentDate1.ToString("yyyy").ToUpper();
            lblpayslipmonthyear.Text = monthYearString;

            try
            {
                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    using (SqlCommand sqlcmd = new SqlCommand("SELECT * FROM Payslip WHERE EmpID = @EmpNo AND Year=@Year  AND Month = @Month", connection))
                    {
                        sqlcmd.Parameters.AddWithValue("@EmpNo", EmId);
                        sqlcmd.Parameters.AddWithValue("@Year", year);
                        sqlcmd.Parameters.AddWithValue("@Month", monthName);   
                        connection.Open();

                        using (SqlDataReader myReader = sqlcmd.ExecuteReader())
                        {
                            if (myReader.Read())
                            {
                                if (myReader["Year"].ToString() == year.ToString() && myReader["Month"].ToString() == monthName)
                                {
                                    lblpayslipid.Text = myReader["Payslip"].ToString();
                                    lblpayslipmonth.Text = myReader["Month"].ToString();
                                    lblpayslipempname.Text = myReader["Name"].ToString();
                                    lblpayslipdesignation.Text = myReader["Designation"].ToString();
                                    lblpayslipempid.Text = myReader["EmpNo"].ToString();
                                    if (myReader["DOJ"] != DBNull.Value && DateTime.TryParse(myReader["DOJ"].ToString(), out DateTime doj))
                                    {
                                        lblpayslipdoj.Text = doj.ToString("yyyy-MM-dd");
                                    }
                                    lbnetSalary.Text = myReader["NetSalary"].ToString();
                                    // earnings 

                                    lblpayslipbasicSalary.Text = myReader["BasicSalary"].ToString();
                                    lblpaysliphra.Text = myReader["HRA"].ToString();
                                    lblpayslipca.Text = myReader["CA"].ToString();
                                    lblpayslipother.Text = myReader["EA"].ToString();
                                    lblpayslipsa.Text = myReader["SA"].ToString();
                                    lbltotalearning.Text = myReader["GrossSalary"].ToString();

                                    // deductions
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

                                }
                            }
                            else
                            {
                                Response.Write("<script>alert(' The " + monthYearString + " Month  here No Pay slip updated')</script>");
                                payslipiddiv.Visible = false;
                                resetvalues();
                                //Response.Redirect("AdminSalary.aspx");
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

        //protected void csvpayslipbtn_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        // Check if there is data in the div
        //        if (payslipiddiv.InnerHtml.Trim() != "")
        //        {
        //            Response.Clear();
        //            Response.Buffer = true;
        //            Response.AddHeader("content-disposition", "attachment;filename=data.csv");
        //            Response.Charset = "";
        //            Response.ContentType = "application/text";
        //            StringBuilder sBuilder = new StringBuilder();
        //            sBuilder.Append("Column1, Column2, Column3"); // Add your column headers here
        //            sBuilder.Append("\r\n");

        //            sBuilder.Append("Data1, Data2, Data3"); // Add your data here
        //            sBuilder.Append("\r\n");

        //            // Output the CSV content
        //            Response.Output.Write(sBuilder.ToString());
        //            Response.Flush();
        //            Response.End();
        //        }
        //        else
        //        {

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Handle any exceptions
        //        throw ex;
        //    }

        //}

        protected void pdfpayslipbtn_Click(object sender, EventArgs e)
        {
            //string htmlContent = payslipiddiv.InnerHtml;

            //// Path to save the generated PDF file temporarily
            //string tempPdfFilePath = HttpContext.Current.Server.MapPath("~/Temp/RR.pdf");

            //// Convert HTML content to PDF and save it to a temporary file
            //HtmlConverter.ConvertToPdf(htmlContent, new FileStream(tempPdfFilePath, FileMode.Create));

            //// Read the generated PDF file into a byte array
            //byte[] pdfBytes = File.ReadAllBytes(tempPdfFilePath);

            //// Delete the temporary PDF file
            //File.Delete(tempPdfFilePath);

            //// Send the PDF file to the response stream
            //HttpContext.Current.Response.ContentType = "application/pdf";
            //HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=RR.pdf");
            //HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            //HttpContext.Current.Response.BinaryWrite(pdfBytes);
            //HttpContext.Current.Response.End();

        }

        protected void btnback_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdminSalary.aspx");
        }
    }
}