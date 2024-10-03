using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Drawing;
using DocumentFormat.OpenXml.Office.Word;
using System.Globalization;

namespace Human_Resource_Management
{
    public partial class AdminGeneratePayroll : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                TextBox2.Attributes["type"] = "month";
                BindCompany2();
            }
        }
        public void BindCompany2()
        {
            try
            {
                using (SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    using (SqlCommand sqlCmd = new SqlCommand())
                    {
                        sqlCmd.CommandText = "SELECT CompanyId,CompanyName FROM Companies order by CompanyName asc";
                        sqlCmd.Connection = sqlConn;
                        sqlConn.Open();
                        SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);


                        // add employee company
                        ddlcompany.DataSource = dt;
                        ddlcompany.DataValueField = "CompanyId";
                        ddlcompany.DataTextField = "CompanyName";
                        ddlcompany.DataBind();
                        sqlConn.Close();

                        ddlcompany.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Select Company --", "0"));
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
            btngenaratepayroll.Visible = true;
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
                        ddlbranch.Items.Insert(0, new ListItem("-- Select Branch --", "0"));
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
                        sqlCmd.CommandText = "SELECT DeptId,Department FROM Department where BranchId='" + ddlbranch.SelectedValue + "' order by Department asc ";
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
                        sqlCmd.CommandText = "select * from [Employees]  where status= 1  and DepartmentId= '" + ddldepartment.SelectedValue + "' order by FirstName asc";
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
                        ddlname.Items.Insert(0, new ListItem("-- Select Name --", "0"));
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btngenaratepayroll_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(TextBox2.Text))
            {
                string monthYearText = TextBox2.Text; // Example: "2024-03"
                DateTime selectedDate;
                DateTime firstDayOfMonth;

                if (DateTime.TryParseExact(monthYearText, "yyyy-MM", System.Globalization.CultureInfo.InvariantCulture, DateTimeStyles.None, out selectedDate))
                {
                     firstDayOfMonth = new DateTime(selectedDate.Year, selectedDate.Month, 1);                   
                }
                else
                {
                    throw new FormatException("Invalid date format in TextBox2.");
                }
                int selectedYear = selectedDate.Year;
                int selectedMonth = selectedDate.Month;

                string connstrg = ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString;
                bool isPayrollCompleted = false;
                using (SqlConnection Conn = new SqlConnection(connstrg))
                {
                    string query = @"SELECT e.*, a.* 
                             FROM Employees e 
                             INNER JOIN AttendanceList a ON e.EmpId = a.EmpId 
                             WHERE e.Status = '1' AND YEAR(a.FromDate) = @Year AND MONTH(a.FromDate) = @Month";

                    // Add filtering conditions based on the dropdown selections
                    if (!string.IsNullOrEmpty(ddlcompany.SelectedItem?.Value) && ddlcompany.SelectedItem.Value != "0")
                    {
                        query += " AND e.CompanyId = @CompanyId";
                    }
                    if (!string.IsNullOrEmpty(ddlbranch.SelectedItem?.Value) && ddlbranch.SelectedItem.Value != "0")
                    {
                        query += " AND e.BranchId = @BranchId";
                    }
                    if (!string.IsNullOrEmpty(ddldepartment.SelectedItem?.Value) && ddldepartment.SelectedItem.Value != "0")
                    {
                        query += " AND e.DepartmentId = @DepartmentId";
                    }
                    if (!string.IsNullOrEmpty(ddlname.SelectedItem?.Value) && ddlname.SelectedItem.Value != "0")
                    {
                        query += " AND e.EmpId = @EmpId";
                    }

                    using (SqlCommand Cmd = new SqlCommand(query, Conn))
                    {
                        Cmd.Parameters.AddWithValue("@Year", selectedYear);
                        Cmd.Parameters.AddWithValue("@Month", selectedMonth);
                        // Add parameters for filtering
                        if (!string.IsNullOrEmpty(ddlcompany.SelectedItem?.Value) && ddlcompany.SelectedItem.Value != "0")
                        {
                            Cmd.Parameters.AddWithValue("@CompanyId", ddlcompany.SelectedItem.Value);
                        }
                        if (!string.IsNullOrEmpty(ddlbranch.SelectedItem?.Value) && ddlbranch.SelectedItem.Value != "0")
                        {
                            Cmd.Parameters.AddWithValue("@BranchId", ddlbranch.SelectedItem.Value);
                        }
                        if (!string.IsNullOrEmpty(ddldepartment.SelectedItem?.Value) && ddldepartment.SelectedItem.Value != "0")
                        {
                            Cmd.Parameters.AddWithValue("@DepartmentId", ddldepartment.SelectedItem.Value);
                        }
                        if (!string.IsNullOrEmpty(ddlname.SelectedItem?.Value) && ddlname.SelectedItem.Value != "0")
                        {
                            Cmd.Parameters.AddWithValue("@EmpId", ddlname.SelectedItem.Value);
                        }

                        try
                        {
                            Conn.Open();
                            DataSet ds1 = new DataSet();
                            SqlDataAdapter da1 = new SqlDataAdapter(Cmd);
                            da1.Fill(ds1);

                            foreach (DataRow row in ds1.Tables[0].Rows)
                            {
                                // Extract data from the row
                                int EmpId = Convert.ToInt32(row["EmpId"]);
                                string Name = row["FirstName"].ToString().Trim();
                                DateTime DOB = Convert.ToDateTime(row["DOB"]);
                                DateTime DOJ = Convert.ToDateTime(row["DOJ"]);
                                string Designation = row["Designation"].ToString().Trim();
                                string BankName = row["BankName"].ToString().Trim();
                                string BankAccNo = row["BankAccNo"].ToString().Trim();
                                string IFSC = row["IFSC"].ToString().Trim();
                                string PfNo = row["PfNo"].ToString().Trim();
                                string PanNo = row["PanNo"].ToString().Trim();
                                int EmployeeCode = Convert.ToInt32(row["EmployeeCode"]);
                                string BranchCode = row["BranchCode"].ToString().Trim();
                                string PFStatus = row["PFStatus"].ToString().Trim();
                                int PfStatusId = Convert.ToInt32(row["PfStatusId"]);
                                string Esino = row["Esino"].ToString();
                                string Department = row["Department"].ToString();
                                string Branch = row["Branch"].ToString();

                                // Attendance-related data
                                decimal TotalDays = (decimal)row["TotalDays"];
                                decimal WorkingDays = (decimal)row["WorkingDays"];
                                int WeekOffs = (int)row["WeekOffs"];
                                decimal HoliDays = (decimal)row["HoliDays"];
                                decimal Presents = (decimal)row["Presents"];
                                decimal Absents = (decimal)row["Absents"];
                                decimal HalfDays = (decimal)row["HalfDays"];
                                int LateLogs = (int)row["LateLogs"];
                                decimal LOPs = (decimal)row["LOPs"];
                                decimal CL = (decimal)row["CL"];
                                decimal SL = (decimal)row["SL"];
                                decimal CompOffs = (decimal)row["CompOffs"];
                                decimal PaybleDays = (decimal)row["PaybleDays"];
                                int Year = Convert.ToInt32(row["Year"]);
                                string Month = row["Month"].ToString();

                                decimal leaves = CL + SL + CompOffs;
                                // Salary calculations
                                long SalaryAnnum = Convert.ToInt64(row["SalaryAnnum"]);
                                double MonthSalary = SalaryAnnum / 12.0;
                                double PerDaySalary = Convert.ToDouble(TotalDays) > 0 ? Math.Round(MonthSalary / Convert.ToDouble(TotalDays), 1) : 0;
                                double MonthlyGrossSalary = Math.Round(PerDaySalary * Convert.ToDouble(PaybleDays), 1);
                                double PerDayGrossSalary = Math.Round(MonthlyGrossSalary / Convert.ToDouble(PaybleDays), 1);
                                double Basic = (MonthlyGrossSalary / 100.0) * 50.0;
                                double HRA = (MonthlyGrossSalary / 100.0) * 20.0;
                                double CA = (MonthlyGrossSalary / 100.0) * 20.0;
                                double OA = (MonthlyGrossSalary / 100.0) * 10.0;

                                double lateDays = GetLateCount(Convert.ToInt32(LateLogs));
                                double LateDeductionTotal = lateDays * PerDayGrossSalary;
                                double LopDeduction = Convert.ToDouble(LOPs) * PerDayGrossSalary;
                                double PF = 0.0;
                                double ESI = 0.0;
                                int PT = 0;
                                
                                double NetSalary = 0.0;
                                double TDSTAXAmount = GetTDSAmount(SalaryAnnum);
                                double monthlyTDSAmount = TDSTAXAmount / 12; // Monthly TDS amount

                                // Salary deductions based on PF and ESI status
                                if (PfStatusId == 1 && Esino == "No")
                                {
                                    PF = Basic > 15000 ? 1800 : (Basic * 12) / 100;
                                    PT = CalculateProfessionalTax(MonthlyGrossSalary);
                                    NetSalary = MonthlyGrossSalary - PF - PT - LateDeductionTotal - monthlyTDSAmount;
                                }
                                else if (PfStatusId == 1 && Esino != "No")
                                {
                                    PF = Basic > 15000 ? 1800 : (Basic * 12) / 100;
                                    PT = CalculateProfessionalTax(MonthlyGrossSalary);
                                    ESI = MonthlyGrossSalary < 21000 ? (MonthlyGrossSalary * 0.75) / 100 : 0;
                                    NetSalary = MonthlyGrossSalary - PF - PT - ESI - LateDeductionTotal - monthlyTDSAmount;
                                }
                                else
                                {
                                    NetSalary = MonthlyGrossSalary - LateDeductionTotal;
                                }
                                double TotalAllDeductions = PF + ESI + PT + LateDeductionTotal + LopDeduction + monthlyTDSAmount;
                                string salaryInWords = ConvertNumberToWords((int)NetSalary);

                                string InserQuery = @"INSERT INTO [dbo].[Payslip] 
                            (Name, EmpID, DOB, DOJ, EmpCode, CompanyId, CompanyName, BranchCode, BranchName, 
                             Department, Designation, Bank, BankAc, IFSCCode, PFNo, PanNo, SalaryAnnum, 
                             ActualSalaryPerMonth, ActualPerdaySalary, BasicSalary, HRA, EA, DA, CA, IT, LIC, 
                             TaxId, Type, PFCategory, PF, ESI, ProfessionalTax, TDSTaxAmount, MA, Workingdays, 
                             Present, Absent, Holiday, HalfDayCount, PNOP, WO, Leave, WOPresent, HolidayPresent, 
                             paybledays, PfEsiProfTaxandLLDeduction, latelogin, latelogindetuction, lop, 
                             OnlyLOPDeduction, date, PerDayGrossSalary, GrossSalary, NetSalary, Amountwords, 
                             Year, Month, OnlyAbsentDaysDedutAmt, AdvanceSalary, Ded, MobileBill, CPF, CESI, SA, 
                             EmpInActiveDate, EmpStatus, AdvanceAmtGivenMonth, CreatedBy, CreatedDate, UpdatedBy, 
                             UpdatedDate, DeletedBy, DeletedDate, BalanceLeaves, ArriearSalaryAmount, 
                             ArrierSalaryGivenMonth, TDSAmountDeductMonth) 
                            VALUES 
                            (@Name, @EmpID, @DOB, @DOJ, @EmpCode, @CompanyId, @CompanyName, @BranchCode, @BranchName, 
                             @Department, @Designation, @Bank, @BankAc, @IFSCCode, @PFNo, @PanNo, @SalaryAnnum, 
                             @ActualSalaryPerMonth, @ActualPerdaySalary, @BasicSalary, @HRA, @EA, @DA, @CA, @IT, @LIC, 
                             @TaxId, @Type, @PFCategory, @PF, @ESI, @ProfessionalTax, @TDSTaxAmount, @MA, @Workingdays, 
                             @Present, @Absent, @Holiday, @HalfDayCount, @PNOP, @WO, @Leave, @WOPresent, @HolidayPresent, 
                             @paybledays, @PfEsiProfTaxandLLDeduction, @latelogin, @latelogindetuction, @lop, 
                             @OnlyLOPDeduction, @date, @PerDayGrossSalary, @GrossSalary, @NetSalary, @Amountwords, 
                             @Year, @Month, @OnlyAbsentDaysDedutAmt, @AdvanceSalary, @Ded, @MobileBill, @CPF, @CESI, @SA, 
                             @EmpInActiveDate, @EmpStatus, @AdvanceAmtGivenMonth, @CreatedBy, @CreatedDate, 
                             @UpdatedBy, @UpdatedDate, @DeletedBy, @DeletedDate, @BalanceLeaves, @ArriearSalaryAmount, 
                             @ArrierSalaryGivenMonth, @TDSAmountDeductMonth)";

                                using (SqlCommand cmdInsert = new SqlCommand(InserQuery, Conn))
                                {
                                    // Add parameters for the insert command
                                    cmdInsert.Parameters.AddWithValue("@Name", Name);
                                    cmdInsert.Parameters.AddWithValue("@EmpID", EmpId);
                                    cmdInsert.Parameters.AddWithValue("@DOB", DOB);
                                    cmdInsert.Parameters.AddWithValue("@DOJ", DOJ);
                                    cmdInsert.Parameters.AddWithValue("@EmpCode", EmployeeCode);
                                    cmdInsert.Parameters.AddWithValue("@CompanyId", ddlcompany.SelectedItem.Value);
                                    cmdInsert.Parameters.AddWithValue("@CompanyName", ddlcompany.SelectedItem.Text);
                                    cmdInsert.Parameters.AddWithValue("@BranchCode", BranchCode);
                                    cmdInsert.Parameters.AddWithValue("@BranchName", Branch);
                                    cmdInsert.Parameters.AddWithValue("@Department", Department);
                                    cmdInsert.Parameters.AddWithValue("@Designation", Designation);
                                    cmdInsert.Parameters.AddWithValue("@Bank", BankName);
                                    cmdInsert.Parameters.AddWithValue("@BankAc", BankAccNo);
                                    cmdInsert.Parameters.AddWithValue("@IFSCCode", IFSC);
                                    cmdInsert.Parameters.AddWithValue("@PFNo", PfNo);
                                    cmdInsert.Parameters.AddWithValue("@PanNo", PanNo);
                                    cmdInsert.Parameters.AddWithValue("@SalaryAnnum", SalaryAnnum);
                                    cmdInsert.Parameters.AddWithValue("@ActualSalaryPerMonth", MonthSalary);
                                    cmdInsert.Parameters.AddWithValue("@ActualPerdaySalary", PerDaySalary);
                                    cmdInsert.Parameters.AddWithValue("@BasicSalary", Basic);
                                    cmdInsert.Parameters.AddWithValue("@HRA", HRA);
                                    cmdInsert.Parameters.AddWithValue("@EA", OA); 
                                    cmdInsert.Parameters.AddWithValue("@DA", 0); 
                                    cmdInsert.Parameters.AddWithValue("@CA", CA);
                                    cmdInsert.Parameters.AddWithValue("@IT", 0); 
                                    cmdInsert.Parameters.AddWithValue("@LIC", 0); 
                                    cmdInsert.Parameters.AddWithValue("@TaxId", 0); // Adjust this as needed
                                    cmdInsert.Parameters.AddWithValue("@Type", PfStatusId); // Adjust this as needed
                                    cmdInsert.Parameters.AddWithValue("@PFCategory", PFStatus); // Adjust this as needed
                                    cmdInsert.Parameters.AddWithValue("@PF", PF.ToString("F1"));
                                    cmdInsert.Parameters.AddWithValue("@ESI", ESI);
                                    cmdInsert.Parameters.AddWithValue("@ProfessionalTax", PT);
                                    cmdInsert.Parameters.AddWithValue("@TDSTaxAmount", TDSTAXAmount); // Adjust this as needed
                                    cmdInsert.Parameters.AddWithValue("@MA", 0); // Adjust this as needed
                                    cmdInsert.Parameters.AddWithValue("@Workingdays", WorkingDays);
                                    cmdInsert.Parameters.AddWithValue("@Present", Presents);
                                    cmdInsert.Parameters.AddWithValue("@Absent", Absents);
                                    cmdInsert.Parameters.AddWithValue("@Holiday", HoliDays);
                                    cmdInsert.Parameters.AddWithValue("@HalfDayCount", HalfDays);
                                    cmdInsert.Parameters.AddWithValue("@PNOP", 0); // Adjust this as needed
                                    cmdInsert.Parameters.AddWithValue("@WO", WeekOffs);
                                    cmdInsert.Parameters.AddWithValue("@Leave", leaves); // Adjust this as needed
                                    cmdInsert.Parameters.AddWithValue("@WOPresent", 0); // Adjust this as needed
                                    cmdInsert.Parameters.AddWithValue("@HolidayPresent", 0); // Adjust this as needed
                                    cmdInsert.Parameters.AddWithValue("@paybledays", PaybleDays);
                                    cmdInsert.Parameters.AddWithValue("@PfEsiProfTaxandLLDeduction", TotalAllDeductions);
                                    cmdInsert.Parameters.AddWithValue("@latelogin", LateLogs);
                                    cmdInsert.Parameters.AddWithValue("@latelogindetuction", LateDeductionTotal);
                                    cmdInsert.Parameters.AddWithValue("@lop", LOPs);
                                    cmdInsert.Parameters.AddWithValue("@OnlyLOPDeduction", LopDeduction);

                                    // Handle the date

                                    cmdInsert.Parameters.AddWithValue("@date", firstDayOfMonth);
                                    cmdInsert.Parameters.AddWithValue("@PerDayGrossSalary", PerDayGrossSalary);
                                    cmdInsert.Parameters.AddWithValue("@GrossSalary", MonthlyGrossSalary);
                                    cmdInsert.Parameters.AddWithValue("@NetSalary", NetSalary);
                                    cmdInsert.Parameters.AddWithValue("@Amountwords", salaryInWords);
                                    cmdInsert.Parameters.AddWithValue("@Year", Year);
                                    cmdInsert.Parameters.AddWithValue("@Month", Month);
                                    cmdInsert.Parameters.AddWithValue("@OnlyAbsentDaysDedutAmt", LopDeduction); // Adjust this as needed
                                    cmdInsert.Parameters.AddWithValue("@AdvanceSalary", 0); // Adjust this as needed
                                    cmdInsert.Parameters.AddWithValue("@Ded", 0); // Adjust this as needed
                                    cmdInsert.Parameters.AddWithValue("@MobileBill", 0); // Adjust this as needed
                                    cmdInsert.Parameters.AddWithValue("@CPF", 0); // Adjust this as needed
                                    cmdInsert.Parameters.AddWithValue("@CESI", 0); // Adjust this as needed
                                    cmdInsert.Parameters.AddWithValue("@SA", 0); // Adjust this as needed
                                    cmdInsert.Parameters.AddWithValue("@EmpInActiveDate", (object)DBNull.Value); // Adjust this as needed
                                    cmdInsert.Parameters.AddWithValue("@EmpStatus", 1); // Adjust this as needed
                                    cmdInsert.Parameters.AddWithValue("@AdvanceAmtGivenMonth", (object)DBNull.Value); // Adjust this as needed
                                    cmdInsert.Parameters.AddWithValue("@CreatedBy", Session["Name"].ToString()); // Adjust this as needed
                                    cmdInsert.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
                                    cmdInsert.Parameters.AddWithValue("@UpdatedBy", (object)DBNull.Value); // Adjust this as needed
                                    cmdInsert.Parameters.AddWithValue("@UpdatedDate", (object)DBNull.Value); // Adjust this as needed
                                    cmdInsert.Parameters.AddWithValue("@DeletedBy", (object)DBNull.Value); // Adjust this as needed
                                    cmdInsert.Parameters.AddWithValue("@DeletedDate", (object)DBNull.Value); // Adjust this as needed
                                    cmdInsert.Parameters.AddWithValue("@BalanceLeaves", (object)DBNull.Value); // Adjust this as needed
                                    cmdInsert.Parameters.AddWithValue("@ArriearSalaryAmount", (object)DBNull.Value);
                                    cmdInsert.Parameters.AddWithValue("@ArrierSalaryGivenMonth", (object)DBNull.Value);// Adjust this as needed
                                    cmdInsert.Parameters.AddWithValue("@TDSAmountDeductMonth", monthlyTDSAmount); // Adjust this as needed

                                    cmdInsert.ExecuteNonQuery();
                                    isPayrollCompleted = true;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }
                }
                if (isPayrollCompleted)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Payroll process completed successfully.');", true);
                    ddlcompany.ClearSelection();
                    ddlbranch.ClearSelection();
                    ddldepartment.ClearSelection();
                    ddlname.ClearSelection();
                    TextBox2.Text = "";
                }
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

        protected void TextBox2_TextChanged(object sender, EventArgs e)
        {
            string selectedDate = TextBox2.Text;

            // Ensure the format is correct (split by '-')
            string[] dateParts = selectedDate.Split('-');

            // Parse the selected year and month
            int selectedYear = int.Parse(dateParts[0]);
            int selectedMonth = int.Parse(dateParts[1]);

            // Get the selected company, branch, department, and employee (optional)
            string selectedCompany = ddlcompany.SelectedItem?.Value;
            string selectedBranch = ddlbranch.SelectedItem?.Value;

            // Only include department if a valid department is selected
            string selectedDepartment = ddldepartment.SelectedItem?.Text;
            if (ddldepartment.SelectedItem?.Value == "0" || string.IsNullOrEmpty(ddldepartment.SelectedItem?.Value))
            {
                selectedDepartment = null; // No department selected
            }

            string selectedEmployee = ddlname.SelectedItem?.Value;

            // Check if the payslip is already generated for the selected year, month, and optional filters
            if (IsPayslipGenerated(selectedYear, selectedMonth, selectedCompany, selectedBranch, selectedDepartment, selectedEmployee))
            {
                // If payslip exists, show an alert to the user
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
                    "alert('Payslip for this month has already been generated!');", true);
                btngenaratepayroll.Visible = false;
                ddlcompany.ClearSelection();
                ddlbranch.ClearSelection();
                TextBox2.Text = "";
            }
        }
        private bool IsPayslipGenerated(int year, int month, string companyId = null, string branchId = null, string departmentId = null, string empId = null)
        {
            // Connection string to the database (from Web.config)
            string connectionString = ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString;

            // Base SQL query to check if a payslip already exists for the given year and month
            string query = "SELECT COUNT(*) FROM Payslip WHERE Year = @Year AND Month = @Month";

            // Append conditions for company, branch, department, and employee if selected
            if (!string.IsNullOrEmpty(companyId) && companyId != "0")
            {
                query += " AND CompanyId = @CompanyId";
            }

            if (!string.IsNullOrEmpty(branchId) && branchId != "0")
            {
                query += " AND BranchCode = @BranchId";
            }

            if (!string.IsNullOrEmpty(departmentId))
            {
                query += " AND Department = @DepartmentId"; // Only include if department is not null or empty
            }

            if (!string.IsNullOrEmpty(empId) && empId != "0")
            {
                query += " AND EmpID = @EmpId";
            }

            // Open a connection to the database
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                // Create the SQL command
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // Add the parameters to prevent SQL injection
                    cmd.Parameters.AddWithValue("@Year", year);
                    cmd.Parameters.AddWithValue("@Month", month);

                    // Add parameters for company, branch, department, and employee if selected
                    if (!string.IsNullOrEmpty(companyId) && companyId != "0")
                    {
                        cmd.Parameters.AddWithValue("@CompanyId", companyId);
                    }

                    if (!string.IsNullOrEmpty(branchId) && branchId != "0")
                    {
                        cmd.Parameters.AddWithValue("@BranchId", branchId);
                    }

                    if (!string.IsNullOrEmpty(departmentId))
                    {
                        cmd.Parameters.AddWithValue("@DepartmentId", departmentId); // Only add if department is selected
                    }

                    if (!string.IsNullOrEmpty(empId) && empId != "0")
                    {
                        cmd.Parameters.AddWithValue("@EmpId", empId);
                    }

                    // Open the connection
                    conn.Open();

                    // Execute the query and return the result as an integer (number of rows found)
                    int count = (int)cmd.ExecuteScalar();

                    // If the count is greater than 0, the payslip has already been generated
                    return count > 0;
                }
            }
        }

    }
}