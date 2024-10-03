using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;
using DocumentFormat.OpenXml.Bibliography;
using Irony;
using System.IO;
using ClosedXML.Excel;

namespace Human_Resource_Management
{
    public partial class AdminSalary : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                AllSalaryBind(null, null);
                EmployeesBindName();
            }
        }
        public void AllSalaryBind(string employeeName, DateTime? selectedMonth)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;

                        StringBuilder query = new StringBuilder();
                        query.Append(@"SELECT Employees.*, Payslip.* FROM Employees JOIN Payslip ON Payslip.EmpID = Employees.EmpId 
                JOIN (SELECT EmpID, MAX(Date) AS MaxDate FROM Payslip GROUP BY EmpID) AS MaxDateSubquery ON Payslip.EmpID = MaxDateSubquery.EmpID AND Payslip.Date = MaxDateSubquery.MaxDate WHERE 1=1 ");

                        // Add conditions for employee name if provided
                        if (!string.IsNullOrEmpty(employeeName))
                        {
                            query.Append(" AND Employees.FirstName LIKE @EmployeeName ");
                            command.Parameters.AddWithValue("@EmployeeName", "%" + employeeName + "%");
                        }

                        // Add conditions for the selected month if provided
                        if (selectedMonth.HasValue)
                        {
                            query.Append(" AND MONTH(Payslip.Date) = @SelectedMonth ");
                            query.Append(" AND YEAR(Payslip.Date) = @SelectedYear ");
                            command.Parameters.AddWithValue("@SelectedMonth", selectedMonth.Value.Month);
                            command.Parameters.AddWithValue("@SelectedYear", selectedMonth.Value.Year);
                        }

                        command.CommandText = query.ToString();

                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            salaryData.Controls.Clear();
                            while (reader.Read())
                            {
                                string Name = reader["FirstName"].ToString();

                                byte[] imageData = (byte[])reader["Image"];
                                string base64String = Convert.ToBase64String(imageData);
                                string imageUrl = "data:image/jpeg;base64," + base64String;

                                string EmployeeId = reader["EmpId"].ToString();
                                int Year = Convert.ToInt32(reader["Year"]);
                                int Month = Convert.ToInt32(reader["Month"]);

                                DateTime formattedFromDate = (DateTime)reader["DOJ"];
                                string DOJ = formattedFromDate.ToString("yyyy-MM-dd");
                                string Company = reader["Company"].ToString();
                                string Designation = reader["Designation"].ToString();
                                string ActualSalaryPerMonth = reader["ActualSalaryPerMonth"].ToString();
                                string OnlyLOPDeduction = reader["OnlyLOPDeduction"].ToString();

                                string netsal = reader["NetSalary"].ToString();
                                string bascsalary = reader["BasicSalary"].ToString();
                                string hra = reader["HRA"].ToString();
                                string ea = reader["EA"].ToString();
                                string da = reader["DA"].ToString();
                                string ca = reader["CA"].ToString();

                                string grssal = reader["GrossSalary"].ToString();

                                string tds = reader["TDSTaxAmount"].ToString();
                                string pf = reader["PF"].ToString();
                                string esi = reader["ESI"].ToString();
                               
                                string pftx = reader["ProfessionalTax"].ToString();
                                string lltd = reader["latelogindetuction"].ToString();
                                string pflltpt = reader["PfEsiProfTaxandLLDeduction"].ToString();
                                lblSalaryword.Text = reader["Amountwords"].ToString();

                                StringBuilder projectHtml = new StringBuilder();
                                projectHtml.Append("<tr>");
                                projectHtml.Append("<td><a href='#' class='avatar'><img src='" + imageUrl + "' alt='User Image'></a>");
                                projectHtml.Append("<a href='#'>" + Name + "</a></td>");                                
                                projectHtml.Append("<td>" + ActualSalaryPerMonth + "</td>");
                                projectHtml.Append("<td>" + grssal + "</td>");
                                projectHtml.Append("<td>" + netsal + "</td>");
                                projectHtml.Append("<td>" + pf + "</td>");
                                projectHtml.Append("<td>" + esi + "</td>");
                                projectHtml.Append("<td >" + pftx + "</td>");
                                projectHtml.Append("<td><a class='edit' href='#' data-bs-toggle='modal' data-bs-target='#edit_salary' onclick =\"viewSalary('" + Name + "','" + EmployeeId + "','" + Year + "','" + Month + "','" + ActualSalaryPerMonth + "','" + grssal + "','" + netsal + "','" + bascsalary + "','" + hra + "','" + ca + "','" + ea + "','" + da + "','" + tds + "','" + pf + "','" + esi + "','" + pftx + "','" + OnlyLOPDeduction + "')\" ><i class='fa-solid fa-pencil m-r-5' style='color:green;'></i></a></td>");
                                projectHtml.Append("</tr>");
                                Session["salarySheet"] = projectHtml.ToString();
                                salaryData.Controls.Add(new LiteralControl(projectHtml.ToString()));
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

        protected void btnupdatesalary_Click(object sender, EventArgs e)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("update Payslip set GrossSalary=@GrossSalary,NetSalary=@NetSalary,BasicSalary=@BasicSalary,HRA=@HRA,EA=@EA,DA=@DA,CA=@CA,PF=@PF,ESI=@ESI,TDSTaxAmount=@TDSTaxAmount,ProfessionalTax=@ProfessionalTax,OnlyLOPDeduction=@OnlyLOPDeduction,UpdatedBy=@UpdatedBy,UpdatedDate=@UpdatedDate", connection);
                    cmd.Parameters.AddWithValue("@GrossSalary",txtgrosssalary.Text);
                    cmd.Parameters.AddWithValue("@NetSalary",txtnetsalary.Text);
                    cmd.Parameters.AddWithValue("@BasicSalary",txtbasicsalary.Text);
                    cmd.Parameters.AddWithValue("@HRA",txthra.Text);
                    cmd.Parameters.AddWithValue("@EA",txtea.Text);
                    cmd.Parameters.AddWithValue("@DA",txtda.Text);
                    cmd.Parameters.AddWithValue("@CA",txtca.Text);
                    cmd.Parameters.AddWithValue("@PF",txtpf.Text);
                    cmd.Parameters.AddWithValue("@ESI",txtesi.Text);
                    cmd.Parameters.AddWithValue("@TDSTaxAmount",txttds.Text);
                    cmd.Parameters.AddWithValue("@ProfessionalTax",txtpt.Text);
                    cmd.Parameters.AddWithValue("@OnlyLOPDeduction",txtleave.Text);
                    cmd.Parameters.AddWithValue("@UpdatedBy", Session["Name"].ToString());
                    cmd.Parameters.AddWithValue("@UpdatedDate",DateTime.Now);
                    int i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        Response.Write("<script>alert('Updated Successfully....')</script>");
                       // AllSalaryBind();
                    }
                    else
                    {
                        Response.Write("<script>alert('Not Updated....?')</script>");
                       // AllSalaryBind();
                    }
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        protected void TextBox18_TextChanged(object sender, EventArgs e)
        {
            string employeeName = txtname.Text.Trim();

            // Get the selected month and year from the TextBox18 (assuming TextBox18 uses the "Month" input type)
            DateTime? selectedMonth = null;
            if (DateTime.TryParse(TextBox18.Text + "-01", out DateTime monthDate))
            {
                selectedMonth = monthDate;
            }

            
            AllSalaryBind(employeeName, selectedMonth);
        }


        public void EmployeesBindName()
        {
            try
            {
                using (SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    using (SqlCommand sqlCmd = new SqlCommand())
                    {
                        // Corrected SQL query to get distinct department names
                        sqlCmd.CommandText = "SELECT EmpId,FirstName FROM Employees Where Status='1' order by FirstName desc";
                        sqlCmd.Connection = sqlConn;
                        sqlConn.Open();
                        SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        // Bind the DropDownList
                        ddlempname.DataSource = dt;
                        ddlempname.DataValueField = "EmpId";
                        ddlempname.DataTextField = "FirstName";
                        ddlempname.DataBind();
                        sqlConn.Close();

                        ddlempname.Items.Insert(0, new System.Web.UI.WebControls.ListItem("ALL", "0"));
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception and optionally display a user-friendly message
                System.Diagnostics.Debug.WriteLine("Error: " + ex.Message);
            }


        }

        protected void btnaddamount_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    DateTime currentDate = DateTime.Now;
                    DateTime previousMonthDate = currentDate.AddMonths(-1);
                    int selectedYear = previousMonthDate.Year;
                    int selectedMonth = previousMonthDate.Month;

                    string updateQuery;

                    // Check if "ALL" is selected, if so, update all employees
                    if (ddlempname.SelectedValue == "0") // "0" is the value for "ALL"
                    {
                        updateQuery = "UPDATE Payslip SET ExtraAmount = ISNULL(ExtraAmount, 0) + @add, NetSalary = ISNULL(NetSalary, 0) + @addAmount WHERE Year = @Year AND Month = @Month";
                    }
                    else
                    {
                        // If a specific employee is selected, update only that employee
                        updateQuery = "UPDATE Payslip SET ExtraAmount = ISNULL(ExtraAmount, 0) + @add, NetSalary = ISNULL(NetSalary, 0) + @addAmount WHERE Year = @Year AND Month = @Month AND EmpId = @EmpId";
                    }

                    using (SqlCommand cmd = new SqlCommand(updateQuery, sqlConn))
                    {
                        sqlConn.Open();

                        // Parse and handle ExtraAmount and addAmount from TextBox1
                        double extraAmount = 0;
                        if (!double.TryParse(TextBox1.Text, out extraAmount))
                        {
                            extraAmount = 0; // Default to 0 if the input is invalid
                        }

                        // Add parameters
                        cmd.Parameters.AddWithValue("@add", extraAmount);
                        cmd.Parameters.AddWithValue("@addAmount", extraAmount);
                        cmd.Parameters.AddWithValue("@Year", selectedYear);
                        cmd.Parameters.AddWithValue("@Month", selectedMonth);

                        // If a specific employee is selected, add EmpId parameter
                        if (ddlempname.SelectedValue != "0")
                        {
                            cmd.Parameters.AddWithValue("@EmpId", ddlempname.SelectedValue);
                        }

                        int affectedRows = cmd.ExecuteNonQuery();
                        if (affectedRows > 0)
                        {
                            Response.Write("<script>alert('Amount updated successfully for " + affectedRows + " record(s).')</script>");
                        }
                        else
                        {
                            Response.Write("<script>alert('No records were updated.')</script>");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the error (for example, to a database or file system)
                System.Diagnostics.Debug.WriteLine("Error: " + ex.Message);
                Response.Write("<script>alert('An error occurred: " + ex.Message + "')</script>");
            }

        }

        protected void lnkexcel_Click(object sender, EventArgs e)
        {
            try
            {
                // You can either fetch the session data that was created in the AllSalaryBind method
                string salaryHtml = Session["salarySheet"] as string;

                if (string.IsNullOrEmpty(salaryHtml))
                {
                    return;
                }

                // Now prepare the Excel file
                using (XLWorkbook workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("SalaryData");
                    worksheet.Cell(1, 1).Value = "Employee Name";
                    worksheet.Cell(1, 2).Value = "Actual Salary";
                    worksheet.Cell(1, 3).Value = "Gross Salary";
                    worksheet.Cell(1, 4).Value = "Net Salary";
                    worksheet.Cell(1, 5).Value = "PF";
                    worksheet.Cell(1, 6).Value = "ESI";
                    worksheet.Cell(1, 7).Value = "Professional Tax";

                    int row = 2;
                    using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                    {
                        using (SqlCommand command = new SqlCommand("SELECT Employees.FirstName, Payslip.* FROM Employees JOIN Payslip ON Employees.EmpId = Payslip.EmpId", connection))
                        {
                            connection.Open();
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    worksheet.Cell(row, 1).Value = reader["FirstName"].ToString();
                                    worksheet.Cell(row, 2).Value = reader["ActualSalaryPerMonth"].ToString();
                                    worksheet.Cell(row, 3).Value = reader["GrossSalary"].ToString();
                                    worksheet.Cell(row, 4).Value = reader["NetSalary"].ToString();
                                    worksheet.Cell(row, 5).Value = reader["PF"].ToString();
                                    worksheet.Cell(row, 6).Value = reader["ESI"].ToString();
                                    worksheet.Cell(row, 7).Value = reader["ProfessionalTax"].ToString();
                                    row++;
                                }
                            }
                        }
                    }

                    worksheet.Columns().AdjustToContents();

                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        workbook.SaveAs(memoryStream);
                        memoryStream.Position = 0;

                        Response.Clear();
                        Response.Buffer = true;
                        Response.AddHeader("content-disposition", "attachment;filename=SalaryData.xlsx");
                        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        Response.BinaryWrite(memoryStream.ToArray());
                        Response.Flush();
                        Response.End();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}