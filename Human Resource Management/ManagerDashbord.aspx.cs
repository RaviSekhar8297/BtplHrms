using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.DataVisualization.Charting;
using System.Collections.Specialized;
using System.Drawing;
using System.Globalization;
using System.Data.SqlTypes;
using System.Web.Services.Description;
using System.Text;

namespace Human_Resource_Management
{
    public partial class ManagerDashbord : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindAttendancePieChart();
                ProjectsCounts();
                DepartmentAttendance();
                string base64String = (string)Session["Image"];
                Image1.ImageUrl = "data:image/png;base64," + base64String;
                //  imageshow();
                LeavesCount();
                BirthDays();
                Anniversary();
                TimeOffCount();
                string nextHoliday = GetNextHoliday();
                lblNextHoliday.Text = nextHoliday;
                string name = Session["Name"].ToString();
                lblname.Text = name;
                lbldesignation.Text = Session["Designation"].ToString();
            }
        }
        public void BindAttendancePieChart()
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
            {
                con.Open();
                string query = @"
                    SELECT
                        (SELECT COUNT(DISTINCT Employees.EmpId) 
                         FROM Employees 
                         JOIN Attendance ON Employees.EmpId = Attendance.UserId 
                         WHERE CONVERT(DATE, Attendance.DateOfTransaction) = CONVERT(DATE, GETDATE())) AS TotalPresents,

                        (SELECT COUNT(DISTINCT Employees.EmpId) FROM Employees) - 
                        (SELECT COUNT(DISTINCT Employees.EmpId) 
                         FROM Employees 
                         JOIN Attendance ON Employees.EmpId = Attendance.UserId 
                         WHERE CONVERT(DATE, Attendance.DateOfTransaction) = CONVERT(DATE, GETDATE())) AS TotalAbsents,

                        (SELECT COUNT(DISTINCT EmpId) FROM Employees) AS TotalCount;";

                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                con.Close();
            }

            Chart1.Series["Series1"].Points.Clear();

            foreach (DataRow dr in dt.Rows)
            {


                // DataPoint for Presents
                DataPoint dp1 = new DataPoint();
                dp1.SetValueXY("Presents", Convert.ToDecimal(dr["TotalPresents"]));
                dp1.Color = Color.PaleGreen; // Color for Presents
                dp1.Label = "Presents ".ToString(); // Display count on the slice
                dp1.ToolTip = dr["TotalPresents"].ToString(); // Tooltip displaying the count
                Chart1.Series["Series1"].Points.Add(dp1);

                // DataPoint for Absents
                DataPoint dp2 = new DataPoint();
                dp2.SetValueXY("Absents", Convert.ToDecimal(dr["TotalAbsents"]));
                dp2.Color = Color.LightCoral; // Color for Absents
                dp2.Label = "Absents ".ToString(); // Display count on the slice
                dp2.ToolTip = dr["TotalAbsents"].ToString(); // Tooltip displaying the count
                Chart1.Series["Series1"].Points.Add(dp2);

                // DataPoint for Employees
                DataPoint dp3 = new DataPoint();
                dp3.SetValueXY("Employees", Convert.ToDecimal(dr["TotalCount"]));
                dp3.Color = Color.SkyBlue; // Color for Total Employees
                dp3.Label = "Employees ".ToString(); // Display count on the slice
                dp3.ToolTip = dr["TotalCount"].ToString(); // Tooltip displaying the count
                Chart1.Series["Series1"].Points.Add(dp3);
            }
        }
        private void ProjectsCounts()
        {
            try
            {
                string Department = Session["DepartmentName"].ToString();

                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SELECT COUNT(*) AS TotalProjects FROM ProjectsList where AssignDepartment='"+ Department + "' ", con);
                    int TotalProjects = (int)cmd.ExecuteScalar();

                    cmd.CommandText = "SELECT COUNT(*) AS CompletedProjects FROM ProjectsList where ProjectStatus='Completed' and AssignDepartment='" + Department + "'";
                    int CompletedProjects = (int)cmd.ExecuteScalar();

                    cmd.CommandText = "SELECT COUNT(*) AS PendingProjects FROM ProjectsList where ProjectStatus='Pending' and AssignDepartment='" + Department + "'";
                    int PendingProjects = (int)cmd.ExecuteScalar();

                    cmd.CommandText = "SELECT COUNT(*) AS WorkingProjects FROM ProjectsList where ProjectStatus='Working' and AssignDepartment='" + Department + "'";
                    int WorkingProjects = (int)cmd.ExecuteScalar();
                    cmd.CommandTimeout = 9000;

                    Label1.Text = TotalProjects.ToString();
                    Label2.Text = CompletedProjects.ToString();
                    Label4.Text = WorkingProjects.ToString();
                    Label3.Text = PendingProjects.ToString();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void DepartmentAttendance()
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
            {
                con.Open();
                string departmentName = Session["DepartmentName"].ToString();

                string query = @"
                    SELECT
                        (SELECT COUNT(DISTINCT Employees.EmpId) 
                         FROM Employees 
                         JOIN Attendance ON Employees.EmpId = Attendance.UserId 
                         WHERE CONVERT(DATE, Attendance.DateOfTransaction) = CONVERT(DATE, GETDATE())
                         AND Employees.Department = @DepartmentName) AS TotalPresents,

                        (SELECT COUNT(DISTINCT Employees.EmpId) 
                         FROM Employees 
                         WHERE Employees.Department = @DepartmentName) - 
                        (SELECT COUNT(DISTINCT Employees.EmpId) 
                         FROM Employees 
                         JOIN Attendance ON Employees.EmpId = Attendance.UserId 
                         WHERE CONVERT(DATE, Attendance.DateOfTransaction) = CONVERT(DATE, GETDATE())
                         AND Employees.Department = @DepartmentName) AS TotalAbsents,

                        (SELECT COUNT(DISTINCT Employees.EmpId) 
                         FROM Employees 
                         WHERE Employees.Department = @DepartmentName) AS TotalCount;";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@DepartmentName", departmentName);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                con.Close();
            }

            Chart2.Series["Series2"].Points.Clear();
            foreach (DataRow dr in dt.Rows)
            {
                // DataPoint for Presents
                DataPoint dp1 = new DataPoint();
                dp1.SetValueXY("Presents", Convert.ToDecimal(dr["TotalPresents"]));
                dp1.Color = Color.PaleGreen; // Color for Presents
                dp1.Label = "Presents ".ToString(); // Display count on the slice
                dp1.ToolTip = dr["TotalPresents"].ToString(); // Tooltip displaying the count
                Chart2.Series["Series2"].Points.Add(dp1);

                // DataPoint for Absents
                DataPoint dp2 = new DataPoint();
                dp2.SetValueXY("Absents", Convert.ToDecimal(dr["TotalAbsents"]));
                dp2.Color = Color.LightCoral; // Color for Absents
                dp2.Label = "Absents ".ToString(); // Display count on the slice
                dp2.ToolTip = dr["TotalAbsents"].ToString(); // Tooltip displaying the count
                Chart2.Series["Series2"].Points.Add(dp2);

                // DataPoint for Employees
                DataPoint dp3 = new DataPoint();
                dp3.SetValueXY("Employees", Convert.ToDecimal(dr["TotalCount"]));
                dp3.Color = Color.SkyBlue; // Color for Total Employees
                dp3.Label = "Employees ".ToString(); // Display count on the slice
                dp3.ToolTip = dr["TotalCount"].ToString(); // Tooltip displaying the count
                Chart2.Series["Series2"].Points.Add(dp3);
            }
        }

        public void imageshow()
        {
            try
            {
                if (Session["EmpId"] != null)
                {
                    using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                    {
                        using (SqlCommand sqlcmd = new SqlCommand("SELECT Image FROM WebLogins WHERE EmpId = @EmpId", connection))
                        {
                            sqlcmd.Parameters.AddWithValue("@EmpId", Session["EmpId"].ToString());

                            connection.Open();
                            object empImage = sqlcmd.ExecuteScalar();
                            if (empImage != null && empImage != DBNull.Value)
                            {
                                byte[] imageBytes = (byte[])empImage;
                                string base64String = Convert.ToBase64String(imageBytes);
                                //  Image1.ImageUrl = "data:image/jpeg;base64," + base64String;
                            }
                        }
                    }
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void LeavesCount()
        {
            if (Session["EmpId"] == null || Session["EmpId"].ToString() == "")
            {

            }
            else
            {
                try
                {
                    string connectionString = ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString;

                    using (var connection1 = new SqlConnection(connectionString))
                    {
                        connection1.Open();
                        string query = "SELECT * FROM LeavesList WHERE EmpId = @EmpId AND Year = @Year";

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
                                    double sls = (12 - DateTime.Now.Month) + 1;
                                    double UsedCasualLeaves = (double)reader.GetDecimal(reader.GetOrdinal("UsedCasualLeaves"));
                                    double UsedSickLeaves = (double)reader.GetDecimal(reader.GetOrdinal("UsedSickLeaves"));
                                    double UsedCampOffLeaves = (double)reader.GetDecimal(reader.GetOrdinal("UsedCampOffLeaves"));
                                    double TotalLeaves = balenceCasualLeaves + balenceSickLeaves + balenceCampOffLeaves;
                                    double UsedLeaves = UsedCasualLeaves + UsedSickLeaves + UsedCampOffLeaves;
                                    Label6.Text = TotalLeaves.ToString();

                                    Label5.Text = UsedLeaves.ToString();
                                }
                                else
                                {
                                    Label6.Text = "0";
                                    Label5.Text = "0";
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Log the exception
                    Label1.Text = "Error";
                    Label2.Text = "Error";

                    throw ex;
                }
            }
        }


        public void TimeOffCount()
        {
            if (Session["EmpId"] == null || Session["EmpId"].ToString() == "")
            {

            }
            else
            {
                try
                {
                    string empId = Session["EmpId"].ToString();
                    DateTime now = DateTime.Now;
                    int currentMonth = now.Month;
                    int currentYear = now.Year;

                    using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                    {
                        string query = @"SELECT SUM(UsedTimeOff) AS TotalUsedTimeOffs 
                                 FROM TimeOff 
                                 WHERE EmpId = @EmpId 
                                   AND MONTH(FromDateTime) = @CurrentMonth 
                                   AND YEAR(FromDateTime) = @CurrentYear";

                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@EmpId", empId);
                            cmd.Parameters.AddWithValue("@CurrentMonth", currentMonth);
                            cmd.Parameters.AddWithValue("@CurrentYear", currentYear);

                            conn.Open();
                            object result = cmd.ExecuteScalar();
                            conn.Close();

                            int totalMinutes = (result != DBNull.Value && result != null) ? Convert.ToInt32(result) : 0;
                            int hours = totalMinutes / 60;
                            int rem = 24 - (2 * DateTime.Now.Month) - hours;
                            lblslused.Text = $"{hours}";
                            lblslremaining.Text = rem.ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle the exception
                    Response.Write($"<script>alert('An error occurred: {ex.Message}');</script>");
                }
            }
        }


        private string GetNextHoliday()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString;
            string nextHoliday = "";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT TOP (1) CONCAT(SUBSTRING(DATENAME(weekday, HolidayDate), 1, 3),' ',CONVERT(VARCHAR(2), DAY(HolidayDate)),' ',LEFT(DATENAME(month, HolidayDate), 3),' ',YEAR(HolidayDate),' - ',HolidayName) AS UpcomingHoliday FROM HolidaysTable WHERE HolidayDate >= GETDATE()ORDER BY HolidayDate ASC; ";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        string upcomingHoliday = reader["UpcomingHoliday"].ToString();
                        nextHoliday = upcomingHoliday;
                    }

                    reader.Close();
                }
            }
            if (string.IsNullOrEmpty(nextHoliday))
            {
                return "No upcoming holidays";
            }
            return nextHoliday;
        }

        public void BirthDays()
        {
            if (Session["EmpId"] == null || Session["EmpId"].ToString() == "")
            {
                // Handle session or authentication issues
            }
            else
            {
                try
                {
                    using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                    {
                        using (SqlCommand sqlcmd = new SqlCommand("SELECT * FROM Employees WHERE DATEPART(month, DOB) = DATEPART(month, GETDATE()) AND DATEPART(day, DOB) = DATEPART(day, GETDATE())", connection))
                        {
                            connection.Open();
                            SqlDataAdapter da1 = new SqlDataAdapter(sqlcmd);
                            DataSet ds1 = new DataSet();
                            da1.Fill(ds1);

                            if (ds1.Tables[0].Rows.Count > 0)
                            {
                                // Create a parent container to hold all employee cards
                                System.Web.UI.HtmlControls.HtmlGenericControl parentContainer = new System.Web.UI.HtmlControls.HtmlGenericControl("div");
                                parentContainer.Attributes["class"] = "parent-container";

                                // Iterate over each employee and create their respective card
                                foreach (DataRow row in ds1.Tables[0].Rows)
                                {
                                    string Name = row["FirstName"].ToString();
                                    DateTime dob = Convert.ToDateTime(row["DOB"]);
                                    int yearsOfAnniversary = DateTime.Now.Year - dob.Year;

                                    object imageDataObj = row["Image"];
                                    byte[] imageData = imageDataObj != DBNull.Value ? (byte[])imageDataObj : new byte[0];

                                    string base64String = Convert.ToBase64String(imageData);
                                    string imageUrl = "data:image/jpeg;base64," + base64String;

                                    string Designation = row["Designation"].ToString();

                                    // Create a div for the employee card
                                    System.Web.UI.HtmlControls.HtmlGenericControl employeeCard = new System.Web.UI.HtmlControls.HtmlGenericControl("div");
                                    employeeCard.Attributes["class"] = "employee-card";

                                    // Add company logo
                                    //System.Web.UI.WebControls.Image companyLogo = new System.Web.UI.WebControls.Image();
                                    //companyLogo.ImageUrl = "assets/Images/AdminMasterImages/BTLogo.png"; // Replace with the actual path to your logo
                                    //companyLogo.CssClass = "company-logo";
                                    //employeeCard.Controls.Add(companyLogo);

                                    // Create and add the image control
                                    System.Web.UI.WebControls.Image employeeImage = new System.Web.UI.WebControls.Image();
                                    employeeImage.ImageUrl = imageUrl;
                                    employeeImage.CssClass = "employee-image";
                                    employeeCard.Controls.Add(employeeImage);

                                    // Create and add the employee name
                                    System.Web.UI.HtmlControls.HtmlGenericControl employeeNameDiv = new System.Web.UI.HtmlControls.HtmlGenericControl("div");
                                    employeeNameDiv.Attributes["class"] = "employee-name";
                                    employeeNameDiv.InnerHtml = Name.ToUpper();
                                    employeeCard.Controls.Add(employeeNameDiv);

                                    // Create and add the Designation
                                    System.Web.UI.HtmlControls.HtmlGenericControl employeedescDiv = new System.Web.UI.HtmlControls.HtmlGenericControl("div");
                                    employeedescDiv.Attributes["class"] = "Designation";
                                    employeedescDiv.InnerHtml = Designation.ToUpper();
                                    employeeCard.Controls.Add(employeedescDiv);

                                    // Add the employee card to the parent container
                                    parentContainer.Controls.Add(employeeCard);
                                }

                                // Add the parent container to the PlaceHolder
                                birthdayContainer.Controls.Add(parentContainer);
                            }
                            else
                            {
                                // No birthdays, display the birthday image
                                System.Web.UI.WebControls.Image noBirthdaysImage = new System.Web.UI.WebControls.Image();
                                noBirthdaysImage.ImageUrl = "assets/Images/AdminMasterImages/Birthday.png"; // Replace with the actual path to your image
                                noBirthdaysImage.CssClass = "no-birthdays-image";
                                birthdayContainer.Controls.Add(noBirthdaysImage);
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

        public void Anniversary()
        {
            if (Session["EmpId"] == null || Session["EmpId"].ToString() == "")
            {
                // Handle session or authentication issues
            }
            else
            {
                try
                {
                    using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                    {
                        using (SqlCommand sqlcmd = new SqlCommand("SELECT * FROM Employees WHERE DATEPART(month, DOJ) = DATEPART(month, GETDATE()) AND DATEPART(day, DOJ) = DATEPART(day, GETDATE())", connection))
                        {
                            connection.Open();
                            SqlDataAdapter da1 = new SqlDataAdapter(sqlcmd);
                            DataSet ds1 = new DataSet();
                            da1.Fill(ds1);

                            if (ds1.Tables[0].Rows.Count > 0)
                            {
                                // Create a parent container to hold all employee cards
                                System.Web.UI.HtmlControls.HtmlGenericControl parentContainer = new System.Web.UI.HtmlControls.HtmlGenericControl("div");
                                parentContainer.Attributes["class"] = "parent-container";

                                // Iterate over each employee and create their respective card
                                foreach (DataRow row in ds1.Tables[0].Rows)
                                {
                                    string Name = row["FirstName"].ToString();
                                    DateTime doj = Convert.ToDateTime(row["DOJ"]);
                                    int yearsOfAnniversary = DateTime.Now.Year - doj.Year;

                                    object imageDataObj = row["Image"];
                                    byte[] imageData = imageDataObj != DBNull.Value ? (byte[])imageDataObj : new byte[0];

                                    string base64String = Convert.ToBase64String(imageData);
                                    string imageUrl = "data:image/jpeg;base64," + base64String;

                                    string Designation = row["Designation"].ToString();

                                    // Create a div for the employee card
                                    System.Web.UI.HtmlControls.HtmlGenericControl employeeCard = new System.Web.UI.HtmlControls.HtmlGenericControl("div");
                                    employeeCard.Attributes["class"] = "employee-card";

                                    // Add company logo
                                    //System.Web.UI.WebControls.Image companyLogo = new System.Web.UI.WebControls.Image();
                                    //companyLogo.ImageUrl = "assets/Images/AdminMasterImages/BTLogo.png"; // Replace with the actual path to your logo
                                    //companyLogo.CssClass = "company-logo";
                                    //employeeCard.Controls.Add(companyLogo);

                                    // Create and add the image control
                                    System.Web.UI.WebControls.Image employeeImage = new System.Web.UI.WebControls.Image();
                                    employeeImage.ImageUrl = imageUrl;
                                    employeeImage.CssClass = "employee-image";
                                    employeeCard.Controls.Add(employeeImage);

                                    // Create and add the employee name
                                    System.Web.UI.HtmlControls.HtmlGenericControl employeeNameDiv = new System.Web.UI.HtmlControls.HtmlGenericControl("div");
                                    employeeNameDiv.Attributes["class"] = "employee-name";
                                    employeeNameDiv.InnerHtml = Name.ToUpper();
                                    employeeCard.Controls.Add(employeeNameDiv);

                                    // Create and add the Designation
                                    System.Web.UI.HtmlControls.HtmlGenericControl employeedescDiv = new System.Web.UI.HtmlControls.HtmlGenericControl("div");
                                    employeedescDiv.Attributes["class"] = "Designation";
                                    employeedescDiv.InnerHtml = Designation.ToUpper();
                                    employeeCard.Controls.Add(employeedescDiv);

                                    // Create and add the employee anniversary
                                    System.Web.UI.HtmlControls.HtmlGenericControl employeeAnniversaryDiv = new System.Web.UI.HtmlControls.HtmlGenericControl("div");
                                    employeeAnniversaryDiv.Attributes["class"] = "employee-anniversary";
                                    employeeAnniversaryDiv.InnerHtml = yearsOfAnniversary + " Year's";
                                    employeeCard.Controls.Add(employeeAnniversaryDiv);

                                    // Add the employee card to the parent container
                                    parentContainer.Controls.Add(employeeCard);
                                }

                                // Add the parent container to the PlaceHolder
                                AnniversaryContainer.Controls.Add(parentContainer);
                            }
                            else
                            {
                                // No anniversaries, display the party image
                                System.Web.UI.WebControls.Image noAnniversariesImage = new System.Web.UI.WebControls.Image();
                                noAnniversariesImage.ImageUrl = "assets/Images/AdminMasterImages/Anniversary3.jpeg"; // Replace with the actual path to your image
                                noAnniversariesImage.CssClass = "no-anniversaries-image";
                                AnniversaryContainer.Controls.Add(noAnniversariesImage);
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


        protected void btntimeoff_Click(object sender, EventArgs e)
        {
            try
            {
                int valueFromTextBox1, valueFromTextBox2;
                if (int.TryParse(TextBox1.Text, out valueFromTextBox1) && int.TryParse(TextBox2.Text, out valueFromTextBox2))
                {
                    if (valueFromTextBox1 >= valueFromTextBox2)
                    {
                        using (var connection1 = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                        {
                            using (var cmd = new SqlCommand("UPDATE TimeOff set UsedTimeOff='" + valueFromTextBox2 + "',BalenceTimeOff=@BalenceTimeOff,Reason=@Reason where EmpId='" + Session["EmpId"].ToString() + "'", connection1))
                            {
                                connection1.Open();
                                int timeoff = Convert.ToInt32(valueFromTextBox1) - Convert.ToInt32(valueFromTextBox2);
                                cmd.Parameters.AddWithValue("@BalenceTimeOff", timeoff.ToString());
                                cmd.Parameters.AddWithValue("@Reason", TextBox4.Text);
                                int i = cmd.ExecuteNonQuery();
                                if (i > 0)
                                {
                                    Response.Write("<script>alert('Succeess...')</script>");
                                    LeavesCount();
                                    BirthDays();
                                    Anniversary();
                                }
                                else
                                {
                                    Response.Write("<script>alert('Failed...')</script>");
                                }
                            }
                        }
                    }
                    else
                    {
                        Response.Write("<script>alert('Enter Less Than " + TextBox1.Text + " Hours...')</script>");
                        TextBox2.Text = "";
                        BirthDays();
                        Anniversary();
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