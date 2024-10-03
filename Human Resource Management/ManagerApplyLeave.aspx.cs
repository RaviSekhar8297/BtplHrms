using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Human_Resource_Management
{
    public partial class ManagerApplyLeave : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["EmpId"] == null)
                {
                    // Handle missing session values, e.g., redirect to login page
                    Response.Redirect("Login.aspx");
                }
                else
                {
                    BindLeaveInsert();
                    LeavesCount();

                }
            }
        }
        public void BindLeaveInsert()
        {
            if (Session["EmpId"] != null && Session["EmpId"].ToString() != "")
            {
                try
                {
                    int currentYear = DateTime.Now.Year;
                    using (var connection1 = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                    {
                        using (SqlCommand sqlcmd = new SqlCommand("SELECT * FROM LeavesList WHERE EmpId = @EmployeeId", connection1))
                        {
                            sqlcmd.Parameters.AddWithValue("@EmployeeId", Session["EmpId"].ToString());
                            sqlcmd.Parameters.AddWithValue("@Year", currentYear.ToString());
                            SqlDataAdapter sqlda = new SqlDataAdapter(sqlcmd);
                            System.Data.DataTable dt = new System.Data.DataTable();
                            sqlda.Fill(dt);
                            int RowCount = dt.Rows.Count;
                            connection1.Close();

                            if (RowCount > 0)
                            {
                                IEnumerable<int> years = dt.AsEnumerable().Select(row => int.Parse(row["Year"].ToString().Trim()));
                                int maxYear = years.Max();
                                if (maxYear == currentYear)
                                {

                                }
                                else
                                {
                                    InsertEmpRecord();
                                }
                            }
                            else
                            {
                                InsertEmpRecord();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public void InsertEmpRecord()
        {
            try
            {
                using (var connection1 = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    connection1.Open();
                    int currentYear = DateTime.Now.Year;
                    DateTime currentDate = DateTime.Now;
                    double total = 12.0;
                    double zero = 0.0;
                    int status = 1;
                    DateTime doj;

                    if (DateTime.TryParse(Session["DOJ"].ToString(), out doj))
                    {
                        int dojYear = doj.Year;

                        SqlCommand cmd = new SqlCommand("Insert into LeavesList(EmpId,Name,Company,Branch,Department,Designation,Doj,TotalCasualLeaves,UsedCasualLeaves,BalenceCasualLeaves," +
                            "TotalSickLeaves,UsedSickLeaves,BalenceSickLaves,TotalCampOffLeaves,UsedCampOffLeaves,BalenceCampOffLeaves,Status,Year) values (@EmpId,@Name,@Company,@Branch,@Department,@Designation,@Doj,@TotalCasualLeaves,@UsedCasualLeaves,@BalenceCasualLeaves,@TotalSickLeaves,@UsedSickLeaves,@BalenceSickLaves,@TotalCampOffLeaves,@UsedCampOffLeaves,@BalenceCampOffLeaves,@Status,@Year)", connection1);

                        cmd.Parameters.AddWithValue("@EmpId", Session["EmpId"].ToString());
                        cmd.Parameters.AddWithValue("@Name", Session["Name"].ToString());
                        cmd.Parameters.AddWithValue("@Company", Session["CompanyName"].ToString());
                        cmd.Parameters.AddWithValue("@Branch", Session["BranchName"].ToString());
                        cmd.Parameters.AddWithValue("@Department", Session["DepartmentName"].ToString());
                        cmd.Parameters.AddWithValue("@Designation", Session["Designation"].ToString());
                        cmd.Parameters.AddWithValue("@Doj", doj);
                        DateTime dojPlus3Months = doj.AddMonths(3);
                        int monthNumber = dojPlus3Months.Month;
                        int BSls = (12 - DateTime.Now.Month) + 1;
                        if (dojYear != currentYear)
                        {
                            // If DOJ year is not equal to the current year, use full leave allocation
                            cmd.Parameters.AddWithValue("@TotalCasualLeaves", total);
                            cmd.Parameters.AddWithValue("@UsedCasualLeaves", zero);
                            cmd.Parameters.AddWithValue("@BalenceCasualLeaves", total);
                            cmd.Parameters.AddWithValue("@TotalSickLeaves", total);
                            cmd.Parameters.AddWithValue("@UsedSickLeaves", zero);
                            cmd.Parameters.AddWithValue("@BalenceSickLaves", BSls);
                            cmd.Parameters.AddWithValue("@TotalCampOffLeaves", zero);
                            cmd.Parameters.AddWithValue("@UsedCampOffLeaves", zero);
                            cmd.Parameters.AddWithValue("@BalenceCampOffLeaves", zero);
                            cmd.Parameters.AddWithValue("@Status", status);
                            cmd.Parameters.AddWithValue("@Year", currentYear);
                        }
                        else
                        {

                            if (dojPlus3Months <= currentDate)
                            {
                                int Cl = 12 - DateTime.Now.Month + 1;
                                int SL = 12 - DateTime.Now.Month + 1;
                                // If DOJ + 3 months is less than or equal to the current date, use full leave allocation
                                cmd.Parameters.AddWithValue("@TotalCasualLeaves", total);
                                cmd.Parameters.AddWithValue("@UsedCasualLeaves", zero);
                                cmd.Parameters.AddWithValue("@BalenceCasualLeaves", Cl);
                                cmd.Parameters.AddWithValue("@TotalSickLeaves", total);
                                cmd.Parameters.AddWithValue("@UsedSickLeaves", zero);
                                cmd.Parameters.AddWithValue("@BalenceSickLaves", SL);
                                cmd.Parameters.AddWithValue("@TotalCampOffLeaves", zero);
                                cmd.Parameters.AddWithValue("@UsedCampOffLeaves", zero);
                                cmd.Parameters.AddWithValue("@BalenceCampOffLeaves", zero);
                                cmd.Parameters.AddWithValue("@Status", status);
                                cmd.Parameters.AddWithValue("@Year", currentYear);
                            }
                            else
                            {
                                int Cl = 12 - monthNumber;
                                int SL = 12 - monthNumber;
                                // If DOJ + 3 months is greater than the current date, do not allocate leaves yet
                                cmd.Parameters.AddWithValue("@TotalCasualLeaves", total);
                                cmd.Parameters.AddWithValue("@UsedCasualLeaves", zero);
                                cmd.Parameters.AddWithValue("@BalenceCasualLeaves", Cl);
                                cmd.Parameters.AddWithValue("@TotalSickLeaves", total);
                                cmd.Parameters.AddWithValue("@UsedSickLeaves", zero);
                                cmd.Parameters.AddWithValue("@BalenceSickLaves", SL);
                                cmd.Parameters.AddWithValue("@TotalCampOffLeaves", zero);
                                cmd.Parameters.AddWithValue("@UsedCampOffLeaves", zero);
                                cmd.Parameters.AddWithValue("@BalenceCampOffLeaves", zero);
                                cmd.Parameters.AddWithValue("@Status", status);
                                cmd.Parameters.AddWithValue("@Year", currentYear);
                            }
                        }

                        cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        throw new FormatException("Invalid date format in Session['DOJ'].");
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void LeavesCount()
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString;

                using (var connection1 = new SqlConnection(connectionString))
                {
                    connection1.Open();
                    string query = "SELECT BalenceCasualLeaves, BalenceSickLaves, BalenceCampOffLeaves FROM LeavesList WHERE EmpId = @EmpId AND Year = @Year";

                    using (SqlCommand cmd = new SqlCommand(query, connection1))
                    {
                        cmd.Parameters.AddWithValue("@EmpId", Session["EmpId"].ToString());
                        cmd.Parameters.AddWithValue("@Year", DateTime.Now.Year);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                double balenceCasualLeaves = (double)reader.GetDecimal(reader.GetOrdinal("BalenceCasualLeaves"));
                                double balenceSickLeaves = (double)reader.GetDecimal(reader.GetOrdinal("BalenceSickLaves"));
                                double balenceCampOffLeaves = (double)reader.GetDecimal(reader.GetOrdinal("BalenceCampOffLeaves"));

                                double calculatedCL = balenceCasualLeaves - (12 - DateTime.Now.Month);
                                double NowCL = calculatedCL < 0 ? 0 : calculatedCL;

                                double RemainingMonthLeaves = 12 - DateTime.Now.Month;
                                double calculatedSL = balenceSickLeaves - RemainingMonthLeaves;
                                double NowSL = calculatedSL < 0 ? 0 : calculatedSL;

                                // Get the count of sick leave records for the current month
                                int slCount = GetCount(Session["EmpId"].ToString());

                                // Update lblremainingleavescount based on the sick leave count
                                if (slCount > 0)
                                {
                                    lblremainingleavescount.Text = "0";
                                }
                                else
                                {
                                    lblremainingleavescount.Text = "1";
                                }

                                lblcasualleavescount.Text = NowCL.ToString();
                                Label2.Text = balenceCampOffLeaves.ToString();
                                lbltotalovertimes.Text = (NowCL + NowSL + balenceCampOffLeaves).ToString();
                            }
                            else
                            {
                                lbltotalovertimes.Text = "0";
                                lblcasualleavescount.Text = "0";
                                lblremainingleavescount.Text = "0";
                                Label2.Text = "0";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                lbltotalovertimes.Text = "Error";
                lblcasualleavescount.Text = "Error";
                lblremainingleavescount.Text = "Error";
                Label2.Text = "Error";
                throw ex;
            }
        }

        private int GetCount(string empId)
        {
            int count = 0;

            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString;

                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Query to count sick leave records for the current month and year
                    string query = @"SELECT COUNT(*) FROM LeavesStatus22  WHERE EmployeedID = @EmpId AND leave_type = 'Sick Leave'
                AND ((YEAR(FromDate) = @CurrentYear AND MONTH(FromDate) = @CurrentMonth) OR (YEAR(ToDate) = @CurrentYear AND MONTH(ToDate) = @CurrentMonth))";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@EmpId", empId);
                        cmd.Parameters.AddWithValue("@CurrentMonth", DateTime.Now.Month);
                        cmd.Parameters.AddWithValue("@CurrentYear", DateTime.Now.Year);
                        count = (int)cmd.ExecuteScalar();
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                throw new Exception("Error counting sick leave usage", ex);
            }

            return count;
        }



        protected void ddlleavesstatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnapplyleave.Visible = true;
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString;

                using (var connection1 = new SqlConnection(connectionString))
                {
                    connection1.Open();
                    string query = "SELECT TotalCasualLeaves, BalenceCasualLeaves, BalenceSickLaves, BalenceCampOffLeaves,Doj FROM LeavesList WHERE EmpId = @EmpId AND Year = @Year";

                    using (SqlCommand cmd = new SqlCommand(query, connection1))
                    {
                        cmd.Parameters.AddWithValue("@EmpId", Session["EmpId"].ToString());
                        cmd.Parameters.AddWithValue("@Year", DateTime.Now.Year);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                double blscl = (double)reader.GetDecimal(reader.GetOrdinal("BalenceCasualLeaves"));
                                double blssl = (double)reader.GetDecimal(reader.GetOrdinal("BalenceSickLaves"));
                                double balenceCampOffLeaves = (double)reader.GetDecimal(reader.GetOrdinal("BalenceCampOffLeaves"));


                                DateTime doj = reader.GetDateTime(reader.GetOrdinal("Doj"));
                                string dojFormatted = doj.ToString("yyyy-MM-dd");

                                // add 3 months
                                DateTime dojPlusThreeMonths = doj.AddMonths(3);
                                string dojPlusThreeMonthsFormatted = dojPlusThreeMonths.ToString("yyyy-MM");

                                // Get current year and month
                                DateTime currentDate = DateTime.Now;
                                DateTime currentYearMonth = new DateTime(currentDate.Year, currentDate.Month, 1);
                                string nowdate = currentYearMonth.ToString("yyyy-MM");

                                // Convert string dates to DateTime objects for comparison
                                DateTime dojPlusThreeMonthsDate = DateTime.ParseExact(dojPlusThreeMonthsFormatted, "yyyy-MM", null);
                                DateTime nowDate = DateTime.ParseExact(nowdate, "yyyy-MM", null);
                                double MaxValue = 5;
                                if (nowDate > dojPlusThreeMonthsDate)
                                {
                                    double balenceCasualLeaves = blscl - (12 - DateTime.Now.Month);
                                    double balenceSickLeaves = blssl - (12 - DateTime.Now.Month);


                                    Session["LeaveBalance"] = new Dictionary<string, double>
                                    {
                                        { "Casual Leave", balenceCasualLeaves },
                                        { "Sick Leave", balenceSickLeaves },
                                        { "Comp-Off Leave", balenceCampOffLeaves },
                                        { "LOP Leave", MaxValue }
                                    };

                                    // Optionally, update UI with current balance
                                    string leaveType = ddlleavesstatus.SelectedItem.Text;
                                    double balance = (double)((Dictionary<string, double>)Session["LeaveBalance"])[leaveType];
                                    if (balance == 0)
                                    {
                                        Label3.Text = $"You have already used  {leaveType}.";
                                        btnapplyleave.Visible = false;
                                    }
                                    else
                                    {
                                        Label3.Text = $"You have {balance} days of {leaveType}.";
                                    }

                                }
                                else
                                {
                                    double balenceCasualLeaves = 0.0;
                                    double balenceSickLeaves = 0.0;

                                    Session["LeaveBalance"] = new Dictionary<string, double>
                                    {
                                        { "Casual Leave", balenceCasualLeaves },
                                        { "Sick Leave", balenceSickLeaves },
                                        { "Comp-Off Leave", balenceCampOffLeaves },
                                        { "LOP Leave", MaxValue }
                                    };

                                    // Optionally, update UI with current balance
                                    string leaveType = ddlleavesstatus.SelectedItem.Text;
                                    double balance = (double)((Dictionary<string, double>)Session["LeaveBalance"])[leaveType];
                                    if (leaveType == "Casual Leave" || leaveType == "Sick Leave")
                                    {
                                        Label3.Text = $"You will not have {leaveType} for THREE months.";
                                        btnapplyleave.Visible = false;
                                    }
                                    else
                                    {
                                        Label3.Text = $"You have {balance} days of {leaveType}.";
                                        btnapplyleave.Visible = true;
                                    }

                                }

                            }
                            else
                            {
                                Label2.Text = "0";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('An error occurred while checking leave balance.');", true);
                throw ex;
            }
        }

        protected void txtstartdate_TextChanged(object sender, EventArgs e)
        {
            int EmployeedID = Convert.ToInt32(Session["EmpId"]);
            try
            {
                // Check if a leave type is selected
                if (ddlleavesstatus.SelectedIndex <= 0)
                {
                    // Show an alert and stop further execution
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please Select Leave Type.');", true);
                    btnapplyleave.Visible = false;
                    txtstartdate.Text = string.Empty;
                    txttodate.Text = string.Empty;
                    txtnoofdays.Text = string.Empty;
                    return; // Stop further processing
                }

                btnapplyleave.Visible = true;
                DateTime selectedStartDate;
                if (DateTime.TryParse(txtstartdate.Text, out selectedStartDate))
                {
                    DateTime selectedEndDate;
                    bool isEndDateValid = DateTime.TryParse(txttodate.Text, out selectedEndDate);

                    // Check if start date is within any existing leave period
                    string connectionString = ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString;
                    using (var connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        string query = "SELECT COUNT(*) FROM LeavesStatus22 WHERE @SelectedDate BETWEEN FromDate AND ToDate and EmployeedID=@EmployeedID";
                        using (SqlCommand cmd = new SqlCommand(query, connection))
                        {
                            cmd.Parameters.AddWithValue("@SelectedDate", selectedStartDate);
                            cmd.Parameters.AddWithValue("@EmployeedID", EmployeedID);
                            int count = (int)cmd.ExecuteScalar();

                            if (count > 0)
                            {
                                Label3.Text = "This Date you have already taken Leave.";
                                txtstartdate.Text = string.Empty; // Optionally clear the date if it exists within a range
                                return;
                            }
                        }
                    }

                    // Check if start date is not later than end date
                    if (isEndDateValid && selectedStartDate > selectedEndDate)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Start date cannot be later than end date.');", true);
                        txtstartdate.Text = string.Empty; // Optionally clear the invalid date
                    }
                    else
                    {
                        Label3.Text = string.Empty; // Clear any previous messages
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Invalid date format.');", true);
                    btnapplyleave.Visible = false;
                    txtstartdate.Text = string.Empty;
                    txttodate.Text = string.Empty;
                    txtnoofdays.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('An error occurred while checking the date.');", true);
                throw ex;
            }

        }

        protected void txttodate_TextChanged(object sender, EventArgs e)
        {
            btnapplyleave.Visible = true;
            try
            {
                DateTime startDate, toDate;

                // Validate the input date format
                if (!DateTime.TryParse(txtstartdate.Text, out startDate) || !DateTime.TryParse(txttodate.Text, out toDate))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Invalid date format.');", true);
                    btnapplyleave.Visible = false;
                    return;
                }

                // Check if startDate is after toDate
                if (startDate > toDate)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Start date cannot be after end date.');", true);
                    txtstartdate.Text = string.Empty;
                    txttodate.Text = string.Empty;
                    txtnoofdays.Text = string.Empty;
                    btnapplyleave.Visible = false;
                    return;
                }

                // Get employee ID from session
                int Employeid = Convert.ToInt32(Session["EmpId"]);

                // Check if the selected dates overlap with an existing leave in the database
                string connectionString = ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString;
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = @"
            SELECT COUNT(*) 
            FROM LeavesStatus22 
            WHERE EmployeedID = @EmployeedID 
            AND (
                (@StartDate BETWEEN FromDate AND ToDate) 
                OR (@ToDate BETWEEN FromDate AND ToDate) 
                OR (FromDate BETWEEN @StartDate AND @ToDate)
                OR (ToDate BETWEEN @StartDate AND @ToDate)
            )";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@StartDate", startDate);
                        cmd.Parameters.AddWithValue("@ToDate", toDate);
                        cmd.Parameters.AddWithValue("@EmployeedID", Employeid);

                        int count = (int)cmd.ExecuteScalar();

                        if (count > 0)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('The selected date range overlaps with an existing leave range.');", true);
                            txtstartdate.Text = string.Empty;
                            txttodate.Text = string.Empty;
                            btnapplyleave.Visible = false;
                            return;
                        }
                    }
                }

                // Calculate number of leave days excluding Sundays
                double numberOfDays = 0;
                for (DateTime date = startDate; date <= toDate; date = date.AddDays(1))
                {
                    if (date.DayOfWeek != DayOfWeek.Sunday)
                    {
                        numberOfDays++;
                    }
                }

                txtnoofdays.Text = numberOfDays.ToString();

                // Validate leave balance
                string selectedLeaveType = ddlleavesstatus.SelectedItem.Text;
                if (Session["LeaveBalance"] != null)
                {
                    double leaveBalance = (double)((Dictionary<string, double>)Session["LeaveBalance"])[selectedLeaveType];
                    if (numberOfDays > leaveBalance)
                    {
                        string script = $"alert('You have only {leaveBalance} {selectedLeaveType}(s) to apply.');";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", script, true);
                        txttodate.Text = string.Empty;
                        txtnoofdays.Text = string.Empty;
                        btnapplyleave.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('An error occurred while processing the dates.');", true);
                throw ex;
            }

        }

        protected void ddltohalfday_SelectedIndexChanged(object sender, EventArgs e)
        {
            CalculateLeaveDays();
        }
        private void CalculateLeaveDays()
        {
            try
            {
                DateTime startDate, endDate;

                // Parse the dates
                if (!DateTime.TryParse(txtstartdate.Text, out startDate) || !DateTime.TryParse(txttodate.Text, out endDate))
                {
                    leavedatealert.Text = "Invalid date format.";
                    txtnoofdays.Text = string.Empty;
                    return;
                }

                // Check if start date is after end date
                if (startDate > endDate)
                {
                    leavedatealert.Text = "Start date cannot be after end date.";
                    txtnoofdays.Text = string.Empty;
                    return;
                }

                // Check if ddlfromhalfday has been selected
                if (ddlfromhalfday.SelectedValue == "empty")
                {
                    leavedatealert.Text = "Please select a Half Day from.";
                    txtnoofdays.Text = string.Empty;
                    ddltohalfday.ClearSelection();
                    return;
                }

                // Check if ddltohalfday has been selected
                if (ddltohalfday.SelectedValue == "empty2")
                {
                    leavedatealert.Text = "Please select a Half Day to.";
                    txtnoofdays.Text = string.Empty;
                    return;
                }

                // Define half-day values
                double fromHalfDay = ddlfromhalfday.SelectedValue == "fms" || ddlfromhalfday.SelectedValue == "tms" ? 0.5 : 0.0;
                double toHalfDay = ddltohalfday.SelectedValue == "fafts" || ddltohalfday.SelectedValue == "tafts" ? 0.5 : 0.0;

                // Calculate the number of days
                double numberOfDays = 0;
                DateTime tempDate = startDate.Date;

                // Special case: same day
                if (startDate.Date == endDate.Date)
                {
                    numberOfDays = fromHalfDay + toHalfDay;
                }
                else
                {
                    // Full days in between
                    int totalFullDays = 0;
                    for (DateTime date = startDate.Date.AddDays(1); date < endDate.Date; date = date.AddDays(1))
                    {
                        if (date.DayOfWeek != DayOfWeek.Sunday)
                        {
                            totalFullDays++;
                        }
                    }
                    numberOfDays = totalFullDays; // Full days between start and end dates

                    if (startDate.DayOfWeek != DayOfWeek.Sunday)
                    {
                        numberOfDays += 0.5; // Add the start date half-day if it's a valid working day
                    }

                    // Adjust for half-days on the end date
                    if (endDate.DayOfWeek != DayOfWeek.Sunday)
                    {
                        numberOfDays += 0.5; // Add the end date half-day if it's a valid working day
                    }
                }

                // Handle the scenario where `ddlfromhalfday` and `ddltohalfday` are the same
                if (startDate.Date == endDate.Date)
                {
                    numberOfDays = fromHalfDay + toHalfDay;
                }
                else
                {
                    // Correctly account for partial days on the start and end dates
                    double startDayPartial = fromHalfDay;
                    double endDayPartial = toHalfDay;

                    if (startDate.DayOfWeek == DayOfWeek.Sunday)
                    {
                        startDayPartial = 0;
                    }

                    if (endDate.DayOfWeek == DayOfWeek.Sunday)
                    {
                        endDayPartial = 0;
                    }

                    numberOfDays = (endDate.Date - startDate.Date).Days + startDayPartial + endDayPartial;
                }
                txtnoofdays.Text = numberOfDays.ToString("0.0");
                leavedatealert.Text = string.Empty; // Clear any previous messages
            }
            catch (Exception ex)
            {
                leavedatealert.Text = "An error occurred while calculating the leave days.";
                txtnoofdays.Text = string.Empty;
                throw ex;
            }
        }


        protected void btnapplyleave_Click(object sender, EventArgs e)
        {
            string idd = Session["EmpId"].ToString();

            try
            {
                using (var connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    // Check if there are any records for the current year
                    using (SqlCommand checkCmd = new SqlCommand("SELECT COUNT(*) FROM LeavesStatus22 WHERE EmployeedID = @EmployeeId AND DATEPART(YEAR, AppliedDate) = DATEPART(YEAR, GETDATE())", connection))
                    {
                        checkCmd.Parameters.AddWithValue("@EmployeeId", idd);
                        connection.Open();
                        int rowCount = (int)checkCmd.ExecuteScalar();
                        connection.Close();
                    }

                    using (SqlCommand dojcmd = new SqlCommand("SELECT e.*, l.* FROM Employees e INNER JOIN LeavesList l ON e.EmpId = l.EmpId WHERE e.EmpId = @EmployeeId AND e.Status = @Status", connection))
                    {
                        try
                        {
                            string Status = "1";
                            dojcmd.Parameters.AddWithValue("@EmployeeId", idd);
                            dojcmd.Parameters.AddWithValue("@Status", Status);
                            connection.Open();
                            SqlDataAdapter sqlda = new SqlDataAdapter(dojcmd);
                            DataTable dt = new DataTable();
                            sqlda.Fill(dt);
                            DateTime startfromDate = Convert.ToDateTime(txtstartdate.Text);
                            DateTime toDate = Convert.ToDateTime(txttodate.Text);

                            int numberOfDays = 0;
                            for (DateTime date = startfromDate; date <= toDate; date = date.AddDays(1))
                            {
                                if (date.DayOfWeek != DayOfWeek.Sunday)
                                {
                                    numberOfDays++;
                                }
                            }

                            double CasualLeaves = Convert.ToDouble(dt.Rows[0]["BalenceCasualLeaves"]);
                          //  double SickLeaves = Convert.ToDouble(dt.Rows[0]["BalenceSickLaves"]);
                            double CompOffLeaves = Convert.ToDouble(dt.Rows[0]["BalenceCampOffLeaves"]);
                            double SickLeaves = 1.0;
                            double balenceCasualLeaves = CasualLeaves - numberOfDays;
                            double balenceSickLeaves = 12 - DateTime.Now.Month;
                            double balenceCampOffLeaves = CompOffLeaves - numberOfDays;

                            DateTime currentDate = DateTime.Now;
                            string status = "0";
                            string statusReason = "Pending";
                            string aprvName = "Process";

                            using (SqlCommand cmd = new SqlCommand("insert into LeavesStatus22(EmployeedID, AppliedDate, FromDate, ToDate, Duration, BalanceLeaves, ReasontoApply, Status, StatusReason, StatusDate, CL, LOP, approvedby, depthead_id, SL, leave_type, ML, BalanceSLeaves, BalanceCompOffLeaves, CompanyId, DeviceCode, EmployeeName, CompanyName, BranchName, ApprovedByName,Department) values(@EmployeedID, @AppliedDate, @FromDate, @ToDate, @Duration, @BalanceLeaves, @ReasontoApply, @Status, @StatusReason, @StatusDate, @CL, @LOP, @approvedby, @depthead_id, @SL, @leave_type, @ML, @BalanceSLeaves, @BalanceCompOffLeaves, @CompanyId, @DeviceCode, @EmployeeName, @CompanyName, @BranchName, @ApprovedByName,@Department)", connection))
                            {
                                cmd.Parameters.AddWithValue("@EmployeedID", idd);
                                cmd.Parameters.AddWithValue("@AppliedDate", currentDate);
                                cmd.Parameters.AddWithValue("@FromDate", startfromDate);
                                cmd.Parameters.AddWithValue("@ToDate", toDate);
                                cmd.Parameters.AddWithValue("@Duration", numberOfDays);
                                cmd.Parameters.AddWithValue("@ReasontoApply", txtreasontoapply.Text);
                                cmd.Parameters.AddWithValue("@Status", status);
                                cmd.Parameters.AddWithValue("@StatusReason", statusReason);
                                cmd.Parameters.AddWithValue("@StatusDate", DBNull.Value);
                                cmd.Parameters.AddWithValue("@approvedby", DBNull.Value);
                                cmd.Parameters.AddWithValue("@depthead_id", DBNull.Value);
                                cmd.Parameters.AddWithValue("@leave_type", ddlleavesstatus.SelectedItem.Text);
                                cmd.Parameters.AddWithValue("@CompanyId", Session["CompanyId"].ToString());
                                cmd.Parameters.AddWithValue("@DeviceCode", Session["BranchCode"].ToString());
                                cmd.Parameters.AddWithValue("@EmployeeName", Session["Name"].ToString());
                                cmd.Parameters.AddWithValue("@CompanyName", Session["CompanyName"].ToString());
                                cmd.Parameters.AddWithValue("@BranchName", Session["BranchName"].ToString());
                                cmd.Parameters.AddWithValue("@Department", Session["DepartmentName"].ToString());
                                cmd.Parameters.AddWithValue("@ApprovedByName", aprvName);

                                cmd.Parameters.Add("@CL", SqlDbType.Decimal).Value = 0.0;
                                cmd.Parameters.Add("@SL", SqlDbType.Decimal).Value = 0.0;
                                cmd.Parameters.Add("@ML", SqlDbType.Decimal).Value = 0.0;
                                cmd.Parameters.Add("@LOP", SqlDbType.Decimal).Value = 0.0;
                                cmd.Parameters.Add("@BalanceLeaves", SqlDbType.Decimal).Value = 0.0;
                                cmd.Parameters.Add("@BalanceSLeaves", SqlDbType.Decimal).Value = 0.0;
                                cmd.Parameters.Add("@BalanceCompOffLeaves", SqlDbType.Decimal).Value = 0.0;

                                switch (ddlleavesstatus.SelectedItem.Text)
                                {
                                    case "Casual Leave":
                                        cmd.Parameters["@CL"].Value = numberOfDays;
                                        cmd.Parameters["@BalanceLeaves"].Value = balenceCasualLeaves;
                                        cmd.Parameters["@BalanceSLeaves"].Value = SickLeaves;
                                        cmd.Parameters["@BalanceCompOffLeaves"].Value = CompOffLeaves;
                                        UpdateLeavesList("Casual Leave", numberOfDays, Convert.ToInt32(idd));
                                        break;
                                    case "Sick Leave":
                                        cmd.Parameters["@CL"].Value = CasualLeaves;
                                        cmd.Parameters["@SL"].Value = numberOfDays;
                                        cmd.Parameters["@BalanceSLeaves"].Value = balenceSickLeaves;
                                        cmd.Parameters["@BalanceCompOffLeaves"].Value = CompOffLeaves;
                                        UpdateLeavesList("Sick Leave", numberOfDays, Convert.ToInt32(idd));
                                        break;
                                    case "Comp-Off Leave":
                                        cmd.Parameters["@CL"].Value = CasualLeaves;
                                        cmd.Parameters["@SL"].Value = SickLeaves;
                                        cmd.Parameters["@ML"].Value = numberOfDays;
                                        cmd.Parameters["@BalanceCompOffLeaves"].Value = balenceCampOffLeaves;
                                        UpdateLeavesList("Comp-Off Leave", numberOfDays, Convert.ToInt32(idd));
                                        break;
                                    case "LOP Leave":
                                        cmd.Parameters["@LOP"].Value = numberOfDays;
                                        // LOP leaves may not require an update to the balance
                                        break;
                                }

                                int n = cmd.ExecuteNonQuery();
                                if (n > 0)
                                {
                                    Response.Write("<script>alert('Leave Apply Success...')</script>");
                                    ddlleavesstatus.ClearSelection();
                                    txtstartdate.Text = "";
                                    txttodate.Text = "";
                                    txtreasontoapply.Text = "";
                                    ddlfromhalfday.ClearSelection();
                                    ddltohalfday.ClearSelection();
                                    txtnoofdays.Text = "";
                                    LeavesCount();
                                }
                                else
                                {
                                    Response.Write("<script>alert('Failed...')</script>");
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
            catch (Exception ex)
            {
                throw ex;
            }

        }
        private void UpdateLeavesList(string leaveType, decimal numberOfDays, int empId)
        {
            string updateQuery = "";
            int currentMonth = DateTime.Now.Month; // Get current month (e.g., 9 for September)
            int balanceSickLeaves = 12 - currentMonth; // Calculate remaining sick leave balance for the year

            switch (leaveType)
            {
                case "Casual Leave":
                    updateQuery = @"UPDATE LeavesList 
                            SET UsedCasualLeaves = UsedCasualLeaves + @NumberOfDays, 
                                BalenceCasualLeaves = BalenceCasualLeaves - @NumberOfDays 
                            WHERE EmpId = @EmpId AND Year = @Year";
                    break;

                case "Sick Leave":
                    updateQuery = @"UPDATE LeavesList 
                            SET UsedSickLeaves = UsedSickLeaves + @NumberOfDays, 
                                BalenceSickLaves = @BalenceSickLaves 
                            WHERE EmpId = @EmpId AND Year = @Year";
                    break;

                case "Comp-Off Leave":
                    updateQuery = @"UPDATE LeavesList 
                            SET UsedCampOffLeaves = UsedCampOffLeaves + @NumberOfDays, 
                                BalenceCampOffLeaves = BalenceCampOffLeaves - @NumberOfDays 
                            WHERE EmpId = @EmpId AND Year = @Year";
                    break;
            }

            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(updateQuery, connection))
                {
                    cmd.Parameters.AddWithValue("@NumberOfDays", numberOfDays);
                    cmd.Parameters.AddWithValue("@EmpId", empId);
                    cmd.Parameters.AddWithValue("@Year", DateTime.Now.Year);

                    // Add the calculated sick leave balance for Sick Leave case
                    if (leaveType == "Sick Leave")
                    {
                        // Adjust the balance by subtracting the number of days being used
                        cmd.Parameters.AddWithValue("@BalenceSickLaves", balanceSickLeaves);
                    }

                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

    }
}