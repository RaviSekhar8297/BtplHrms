using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Human_Resource_Management
{
    public partial class EmployeeLogins : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DateTime currentDate = DateTime.Today;
                DateTime previousMonthDate = currentDate.AddMonths(-1); 
                string date = previousMonthDate.ToString("yyyy-MM");
                TextBox1.Text = date;
                int selectedMonth = previousMonthDate.Month;
                int selectedYear = previousMonthDate.Year;
                if (Session["EmpId"] != null)
                {
                    int userId;
                    if (int.TryParse(Session["EmpId"].ToString(), out userId))
                    {
                        CalculateAttendanceMetrics(selectedMonth, selectedYear, userId, lbltotaldays, lblworkingdays, lblweekoffs, lblholidays, lblpresents, lblabsents, lbllatelogincount, lblmonthsalary, lblperdaysalary);
                    }
                    else
                    {

                    }
                }
                else
                {

                }
            }
        }
      
        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(TextBox1.Text))
            {
                DateTime selectedDate;
                if (DateTime.TryParseExact(TextBox1.Text, "yyyy-MM", null, System.Globalization.DateTimeStyles.None, out selectedDate))
                {
                    int selectedMonth = selectedDate.Month;
                    int selectedYear = selectedDate.Year;

                    // Ensure EmpId is available in session
                    if (Session["EmpId"] != null)
                    {
                        int userId;
                        if (int.TryParse(Session["EmpId"].ToString(), out userId))
                        {
                            CalculateAttendanceMetrics(selectedMonth, selectedYear, userId, lbltotaldays, lblworkingdays, lblweekoffs, lblholidays, lblpresents, lblabsents, lbllatelogincount, lblmonthsalary, lblperdaysalary);

                        }
                        else
                        {

                        }
                    }
                    else
                    {

                    }
                }
                else
                {

                }
            }
        }

        public void CalculateAttendanceMetrics(int selectedMonth, int selectedYear, int userId, Label lblTotalDays, Label lblWorkingDays, Label lblWeekOffs, Label lblHolidays, Label lblPresents, Label lblAbsents, Label lblLateLogInCount, Label lblMonthSalary, Label lblPerDaySalary)
        {
            int totalDays = DateTime.DaysInMonth(selectedYear, selectedMonth);           
            double TotalDays = 0.0;
            int weekOffs = 0;
            double holidays = 0;
            double presents = 0;
            double absents = 0;
            double halfDayCount = 0;
            int lateLogCount = 0;
            double salaryAnnum = 0;
            double CL = 0.0;
            double SL = 0.0;
            double CompOffs = 0.0;
            double PaybleDays = 0.0;
            double LOPS = 0.0;
            int PfStatusId = 0;
            string Esino = string.Empty;
            DateTime firstDayOfMonth = new DateTime(selectedYear, selectedMonth, 1);
            DateTime lastDayOfMonth = new DateTime(selectedYear, selectedMonth, totalDays);

            DateTime selectedDate = new DateTime(selectedYear, selectedMonth, 1);
            DateTime previousMonthDate = selectedDate.AddMonths(-1);

            int previousMonthYear = previousMonthDate.Year;
            int previousMonthMonth = previousMonthDate.Month;

            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
            {
                string query = @"SELECT al.*, e.* FROM AttendanceList al INNER JOIN Employees e ON al.EmpId = e.EmpId WHERE al.EmpId = @UserId AND al.FromDate >= @FirstDayOfMonth AND al.ToDate <= @LastDayOfMonth";

                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@UserId", userId);
                cmd.Parameters.AddWithValue("@FirstDayOfMonth", firstDayOfMonth);
                cmd.Parameters.AddWithValue("@LastDayOfMonth", lastDayOfMonth);
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    //DateTime currentDate1 = DateTime.Now;
                    //DateTime firstDayOfCurrentMonth = new DateTime(currentDate1.Year, currentDate1.Month, 1);
                    //DateTime firstDayOfPreviousMonth = firstDayOfCurrentMonth.AddMonths(-1);
                    //int previousMonth = firstDayOfPreviousMonth.Month;
                    //int previousYear = firstDayOfPreviousMonth.Year;

                    TotalDays = reader["TotalDays"] != DBNull.Value ? Convert.ToInt32(reader["TotalDays"]) : 0;
                    weekOffs = reader["WeekOffs"] != DBNull.Value ? Convert.ToInt32(reader["WeekOffs"]) : 0;
                    holidays = reader["HoliDays"] != DBNull.Value ? Convert.ToDouble(reader["HoliDays"]) : 0;
                    presents = reader["Presents"] != DBNull.Value ? Convert.ToDouble(reader["Presents"]) : 0;
                    absents = reader["Absents"] != DBNull.Value ? Convert.ToDouble(reader["Absents"]) : 0;
                    halfDayCount = reader["HalfDays"] != DBNull.Value ? Convert.ToDouble(reader["HalfDays"]) : 0;
                    lateLogCount = reader["LateLogs"] != DBNull.Value ? Convert.ToInt32(reader["LateLogs"]) : 0;
                    CL = reader["CL"] != DBNull.Value ? Convert.ToDouble(reader["CL"]) : 0;
                    SL = reader["SL"] != DBNull.Value ? Convert.ToDouble(reader["SL"]) : 0;
                    CompOffs = reader["CompOffs"] != DBNull.Value ? Convert.ToDouble(reader["CompOffs"]) : 0;
                    PaybleDays = reader["PaybleDays"] != DBNull.Value ? Convert.ToDouble(reader["PaybleDays"]) : 0;
                    LOPS = reader["LOPS"] != DBNull.Value ? Convert.ToDouble(reader["LOPS"]) : 0;
                    salaryAnnum = reader["SalaryAnnum"] != DBNull.Value ? Convert.ToDouble(reader["SalaryAnnum"]) : 0;
                    PfStatusId = reader["PfStatusId"] != DBNull.Value ? Convert.ToInt32(reader["PfStatusId"]) : 0;
                    Esino = reader["Esino"].ToString();

                    double loan = Convert.ToDouble(LoanData(userId, selectedMonth, selectedYear));

                    double MonthSalary = salaryAnnum / 12.0;
                    double PerDaySalary = Convert.ToDouble(TotalDays) > 0 ? Math.Round(MonthSalary / Convert.ToDouble(TotalDays), 1) : 0;
                    double MonthlyGrossSalary = Math.Round(PerDaySalary * Convert.ToDouble(PaybleDays), 1);
                    double PerDayGrossSalary = Math.Round(MonthlyGrossSalary / Convert.ToDouble(PaybleDays), 1);
                    double Basic = (MonthlyGrossSalary / 100.0) * 50.0;
                    double HRA = (MonthlyGrossSalary / 100.0) * 20.0;
                    double CA = (MonthlyGrossSalary / 100.0) * 20.0;
                    double OA = (MonthlyGrossSalary / 100.0) * 10.0;
                    double lateDays = GetLateCount(Convert.ToInt32(lateLogCount));
                    double LateDeductionTotal = lateDays * PerDayGrossSalary;
                    double LopDeduction = Convert.ToDouble(LOPS) * PerDayGrossSalary;
                    double halfdaysnumber = halfDayCount * 0.5;
                    double halfdaysamount = halfdaysnumber * PerDayGrossSalary;
                    double PF = 0.0;
                    double ESI = 0.0;
                    int PT = 0;
                    double TotalAllDeductions = PF + ESI + PT + LateDeductionTotal + LopDeduction;
                    double NetSalary = 0.0;
                    double TDSTAXAmount = GetTDSAmount(salaryAnnum);
                    double monthlyTDSAmount = TDSTAXAmount / 12;
                    // Salary deductions based on PF and ESI status
                    if (PfStatusId == 1 && Esino == "No")
                    {
                        PF = Basic > 15000 ? 1800 : (Basic * 12) / 100;
                        PT = CalculateProfessionalTax(MonthlyGrossSalary);
                        NetSalary = MonthlyGrossSalary - PF - PT - LateDeductionTotal - monthlyTDSAmount - loan;
                    }
                    else if (PfStatusId == 1 && Esino != "No")
                    {
                        PF = Basic > 15000 ? 1800 : (Basic * 12) / 100;
                        PT = CalculateProfessionalTax(MonthlyGrossSalary);
                        ESI = MonthlyGrossSalary < 21000 ? (MonthlyGrossSalary * 0.75) / 100 : 0;
                        NetSalary = MonthlyGrossSalary - PF - PT - ESI - LateDeductionTotal - monthlyTDSAmount - loan;
                    }
                    else
                    {
                        NetSalary = MonthlyGrossSalary - LateDeductionTotal - monthlyTDSAmount - loan;
                    }
                    double workingDays = totalDays - weekOffs - holidays;
                    lblTotalDays.Text = totalDays.ToString("F1");
                    lblWorkingDays.Text = workingDays.ToString("F1");
                    lblWeekOffs.Text = weekOffs.ToString("F1");
                    lblHolidays.Text = holidays.ToString("F1");
                    lblPresents.Text = presents.ToString("F1");
                    lblAbsents.Text = absents.ToString("F1");
                    lbllop.Text = LOPS.ToString();
                    lbllatelogincount.Text = lateLogCount.ToString("F1");
                    lblhalfdaycount.Text = halfDayCount.ToString();
                    lblpaybledays.Text = PaybleDays.ToString();
                    lblhalfdayamount.Text = halfdaysamount.ToString("F1");
                    lblproftax.Text = PT.ToString();
                    lblmonthsalary.Text = MonthSalary.ToString("F1");
                    lblperdaysalary.Text = PerDaySalary.ToString("F1");
                    lblgrosssalary.Text = MonthlyGrossSalary.ToString("F1");
                    lblnetsalary.Text = NetSalary.ToString("F1");
                    lblpfamount.Text = PF.ToString("F1");
                    lblesiamount.Text = ESI.ToString("F1");
                    lbllopamount.Text = LopDeduction.ToString("F1");
                    lbllatelogamount.Text = LateDeductionTotal.ToString("F1");
                    lbltdsamount.Text= monthlyTDSAmount.ToString("F1");
                    lblloan.Text = loan.ToString("F1");
                }
                connection.Close();
            }
        }
        int CalculateProfessionalTax(double grossEarningMonth)
        {
            if (grossEarningMonth <= 15000)
            {
                return 0;
            }
            else if (grossEarningMonth > 15000 && grossEarningMonth <= 20000)
            {
                return 150;
            }
            else
            {
                return 200;
            }
        }
        public static double GetTDSAmount(double salaryAnnum)
        {
            double tax = 0;
            double cess = 0;

            if (salaryAnnum <= 250000)
            {
                tax = 0;
            }
            else if (salaryAnnum <= 500000)
            {
                tax = 0.05 * (salaryAnnum - 250000);
            }
            else if (salaryAnnum <= 1000000)
            {
                tax = 0.05 * 250000 + 0.10 * (salaryAnnum - 500000);
            }
            else if (salaryAnnum <= 1250000)
            {
                tax = 0.05 * 250000 + 0.10 * 500000 + 0.15 * (salaryAnnum - 1000000);
            }
            else if (salaryAnnum <= 1500000)
            {
                tax = 0.05 * 250000 + 0.10 * 500000 + 0.15 * 250000 + 0.20 * (salaryAnnum - 1250000);
            }
            else
            {
                tax = 0.05 * 250000 + 0.10 * 500000 + 0.15 * 250000 + 0.20 * 250000 + 0.30 * (salaryAnnum - 1500000);
            }

            cess = 0.04 * tax;
            tax += cess;

            return tax;
        }

        double GetLateCount(int late)
        {
            double lateDays = 0.0;

            if (late < 4)
            {
                lateDays = 0.0; // No late days for counts less than 4
            }
            else if (late >= 4 && late <= 7)
            {
                lateDays = 0.5;
            }
            else if (late >= 8 && late <= 11)
            {
                lateDays = 1.0;
            }
            else if (late >= 12 && late <= 15)
            {
                lateDays = 1.5;
            }
            else if (late >= 16 && late <= 19)
            {
                lateDays = 2.0;
            }
            else if (late >= 20 && late <= 23)
            {
                lateDays = 2.5;
            }
            else if (late >= 24 && late <= 27)
            {
                lateDays = 3.0;
            }
            else
            {
                lateDays = 3.5;
            }

            return lateDays; // Return the calculated lateDays value
        }


        public double LoanData(int empId, int month, int year)
        {
            double loanAmount = 0.0;

            // Initialize the connection with the correct connection string
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
            {
                // Define the SQL query
                string query = "SELECT PayAmount FROM Payslip WHERE EmpId = @EmpId AND Year = @Year AND Month = @Month";

                // Create the command and associate it with the connection
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Add parameters to prevent SQL injection
                    command.Parameters.AddWithValue("@EmpId", empId);
                    command.Parameters.AddWithValue("@Year", year);
                    command.Parameters.AddWithValue("@Month", month);

                    connection.Open();
                    object result = command.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        loanAmount = Convert.ToDouble(result);
                    }
                }
            }

            return loanAmount;
        }

    }
}