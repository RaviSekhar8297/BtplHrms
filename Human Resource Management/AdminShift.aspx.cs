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
using System.Globalization;
using DocumentFormat.OpenXml.VariantTypes;
using Org.BouncyCastle.Ocsp;
using System.Data.SqlTypes;
using DocumentFormat.OpenXml.Bibliography;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TreeView;


namespace Human_Resource_Management
{
    public partial class AdminShift : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {           
            if (!IsPostBack)
            {
                BindShiftData();
                BindDepartments();
                BindName();
                BindAssignData();
            }
        }

        public void BindName()
        {
            try
            {
                String connstrg = ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString;
                using (SqlConnection sqlConn = new SqlConnection(connstrg))
                {
                    using (SqlCommand sqlCmd = new SqlCommand())
                    {
                        sqlCmd.CommandText = "select EmpId, FirstName from Employees where Branch = '" + Session["BranchName"].ToString() + "' order by FirstName asc ";
                        sqlCmd.Connection = sqlConn;
                        sqlConn.Open();
                        SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        ddlempname.DataSource = dt;
                        ddlempname.DataValueField = "EmpId";
                        ddlempname.DataTextField = "FirstName";
                        ddlempname.DataBind();
                        sqlConn.Close();
                        ddlempname.Items.Insert(0, new ListItem("-- Select Name --", "0"));
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void BindShiftData(string searchName = "", string department = "")
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString;

                DateTime currentDate = DateTime.Now.Date;
                DateTime endDate = currentDate.AddDays(7);

                List<DateTime> datesInRange = new List<DateTime>();
                for (DateTime date = currentDate; date <= endDate; date = date.AddDays(1))
                {
                    datesInRange.Add(date);
                }

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    // Base query to retrieve employee details
                    string query = "SELECT EmpId, FirstName, Shift, Image FROM Employees WHERE Status = '1'";

                    // Append filters to the query if necessary
                    if (!string.IsNullOrEmpty(searchName))
                    {
                        query += " AND FirstName LIKE @SearchName";
                    }
                    if (!string.IsNullOrEmpty(department))
                    {
                        query += " AND Department = @Department";
                    }

                    SqlCommand cmdEmployees = new SqlCommand(query, con);
                    if (!string.IsNullOrEmpty(searchName))
                    {
                        cmdEmployees.Parameters.AddWithValue("@SearchName", "%" + searchName + "%");
                    }
                    if (!string.IsNullOrEmpty(department))
                    {
                        cmdEmployees.Parameters.AddWithValue("@Department", department);
                    }

                    SqlDataAdapter daEmployees = new SqlDataAdapter(cmdEmployees);
                    DataTable dtEmployees = new DataTable();
                    daEmployees.Fill(dtEmployees);

                    StringBuilder htmlBuilder = new StringBuilder();

                    if (dtEmployees.Rows.Count == 0)
                    {
                        htmlBuilder.Append("<thead>");
                        htmlBuilder.Append("<tr>");
                        htmlBuilder.Append("<th> Img </th>");
                        htmlBuilder.Append("<th> Name </th>");
                        htmlBuilder.Append("<th style='margin-left:15px;'> Id / Date </th>");

                        foreach (DateTime date in datesInRange)
                        {
                            htmlBuilder.Append("<th>" + date.ToString("MMM-dd") + "</th>");
                        }

                        htmlBuilder.Append("</tr>");
                        htmlBuilder.Append("</thead>");
                        htmlBuilder.Append("<tbody>");
                        htmlBuilder.Append("<tr><td colspan='" + (datesInRange.Count + 3) + "'>No employees found.</td></tr>");
                        htmlBuilder.Append("</tbody>");
                    }
                    else
                    {
                        htmlBuilder.Append("<thead>");
                        htmlBuilder.Append("<tr>");
                        htmlBuilder.Append("<th> Img </th>");
                        htmlBuilder.Append("<th> Name </th>");
                        htmlBuilder.Append("<th style='margin-left:15px;'> Id / Date </th>");

                        foreach (DateTime date in datesInRange)
                        {
                            htmlBuilder.Append("<th>" + date.ToString("MMM-dd") + "</th>");
                        }

                        htmlBuilder.Append("</tr>");
                        htmlBuilder.Append("</thead>");
                        htmlBuilder.Append("<tbody>");

                        foreach (DataRow employeeRow in dtEmployees.Rows)
                        {
                            int empId = Convert.ToInt32(employeeRow["EmpId"]);
                            string empName = employeeRow["FirstName"].ToString();
                            string defaultShift = employeeRow["Shift"].ToString();

                            htmlBuilder.Append("<tr>");

                            // Handle employee image (default image if null)
                            byte[] empImage = employeeRow["Image"] != DBNull.Value ? (byte[])employeeRow["Image"] : null;
                            if (empImage != null)
                            {
                                htmlBuilder.Append("<td><img src='data:image/jpeg;base64," + Convert.ToBase64String(empImage) + "' alt='Image' class='img1' /></td>");
                            }
                            else
                            {
                                htmlBuilder.Append("<td><img src='path/to/default/image.jpg' alt='Default Image' class='img1' /></td>");
                            }

                            htmlBuilder.Append("<td><a href='#'>" + empName + "</a></td>");
                            htmlBuilder.Append("<td><a href='#'>" + empId + "</a></td>");

                            foreach (DateTime date in datesInRange)
                            {
                                string shiftTime = defaultShift;
                                
                                
                                bool isShiftFromAssign = false;  // New flag to track whether shift is from ShiftAssign

                                // Check ShiftAssign table for specific shifts
                                SqlCommand cmdShiftAssign = new SqlCommand("SELECT ShiftTime FROM ShiftAssign WHERE EmpId = @EmpId AND ShiftDate = @ShiftDate", con);
                                cmdShiftAssign.Parameters.AddWithValue("@EmpId", empId);
                                cmdShiftAssign.Parameters.AddWithValue("@ShiftDate", date.ToString("yyyy-MM-dd"));

                                object shiftAssignResult = cmdShiftAssign.ExecuteScalar();
                                if (shiftAssignResult != null)
                                {
                                    shiftTime = shiftAssignResult.ToString();
                                    isShiftFromAssign = true;  // Shift comes from ShiftAssign, so set flag to true
                                }

                                // Format shift times if needed
                                string startTime = "", endTime = ""; string updatedShiftTime = "";
                                if (!string.IsNullOrEmpty(shiftTime))
                                {
                                    string[] shiftTimes = shiftTime.Split(new char[] { '(', ')' }, StringSplitOptions.RemoveEmptyEntries);
                                    foreach (string shiftTimePart in shiftTimes)
                                    {
                                        string[] separators = { " - ", " TO ", " to " };
                                        string[] times = shiftTimePart.Replace(" TO ", " to ").Split(separators, StringSplitOptions.RemoveEmptyEntries);
                                        if (times.Length >= 2)
                                        {
                                            // Ensure the time format is correct
                                            DateTime parsedStartTime, parsedEndTime;
                                            if (DateTime.TryParseExact(times[0].Trim(), "hh:mm tt", CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedStartTime))
                                            {
                                                startTime = parsedStartTime.ToString("HH:mm");
                                                updatedShiftTime = parsedStartTime.AddMinutes(15).ToString("HH:mm");
                                            }
                                            if (DateTime.TryParseExact(times[1].Trim(), "hh:mm tt", CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedEndTime))
                                            {
                                                endTime = parsedEndTime.ToString("HH:mm");
                                            }
                                        }
                                    }
                                }

                                // Apply red color only to the shift time from ShiftAssign
                                string shiftTimeStyle = isShiftFromAssign ? "color: red;" : ""; // Apply red color if from ShiftAssign

                                if (isShiftFromAssign)
                                {
                                    // Enable editing for shifts from ShiftAssign
                                    htmlBuilder.Append("<td><a href='#' data-bs-toggle='modal' class='shift-timee' data-bs-target='#edit_schedule' " +
                                        "style='" + shiftTimeStyle + "' " +
                                        "onclick=\"showShiftDetails('" + empId + "','" + empName + "','" + shiftTime + "','" + startTime + "','"+ updatedShiftTime + "','" + endTime + "','" + date.ToString("yyyy-MM-dd") + "')\">" + shiftTime + "</a></td>");
                                }
                                else
                                {
                                    // Display shift time but without edit functionality for default shifts
                                    htmlBuilder.Append("<td><span class='shift-time' style='" + shiftTimeStyle + "'>" + shiftTime + "</span></td>");
                                }
                            }


                            htmlBuilder.Append("</tr>");
                        }

                        htmlBuilder.Append("</tbody>");
                    }

                    AttendanceData.Controls.Add(new LiteralControl(htmlBuilder.ToString()));
                }
            }
            catch (Exception ex)
            {
                // Log or display the error message
                Response.Write("<script>alert('An error occurred: " + ex.Message + "')</script>");
            }
        }


        public void BindDepartments()
        {
            try
            {
                using (SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    using (SqlCommand sqlCmd = new SqlCommand())
                    {
                        // Corrected SQL query to get distinct department names
                        sqlCmd.CommandText = "SELECT DISTINCT Department FROM Department";
                        sqlCmd.Connection = sqlConn;
                        sqlConn.Open();
                        SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        // Bind the DropDownList
                        DropDownList4.DataSource = dt;
                        DropDownList4.DataValueField = "Department";
                        DropDownList4.DataTextField = "Department";
                        DropDownList4.DataBind();
                        sqlConn.Close();

                        DropDownList4.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Select Department--", "0"));
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception and optionally display a user-friendly message
                System.Diagnostics.Debug.WriteLine("Error: " + ex.Message);
            }


        }
        public List<string> ConvertDates(List<string> datesInRange, int year)
        {
            List<string> Dateofshift = new List<string>();
            foreach (string date in datesInRange)
            {
                DateTime parsedDate = DateTime.ParseExact(date, "MMM-dd", CultureInfo.InvariantCulture);
                string fullDate = new DateTime(year, parsedDate.Month, parsedDate.Day).ToString("yyyy-MM-dd");
                Dateofshift.Add(fullDate);
            }
            return Dateofshift;
        }


        protected void DropDownList5_SelectedIndexChanged(object sender, EventArgs e)
        {
            string ddlItem = DropDownList5.SelectedValue;
            if(ddlItem== "DayShift")
            {
                TextBox21.Text = "09:30 AM TO 06:30 PM";
                TextBox21.ReadOnly = true;
                BindShiftData();
            }
            else if(ddlItem == "NightShift")
            {
                TextBox21.Text = "10:00 PM TO 06:00 AM";
                TextBox21.ReadOnly = true;
                BindShiftData();
            }
            else
            {
                TextBox21.Text = "";
                TextBox21.ReadOnly = true;
                BindShiftData();
            }
        }

        protected void txtsearchName_TextChanged(object sender, EventArgs e)
        {
            string searchName = txtsearchName.Text.Trim();

            // Retrieve the current value of DropDownList4 from ViewState
            string selectedDepartment = ViewState["SelectedDepartment"] != null ? ViewState["SelectedDepartment"].ToString() : string.Empty;

            // Call the BindShiftData method with the current values
            BindShiftData(searchName, selectedDepartment);

        }

        protected void DropDownList4_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedDepartment = DropDownList4.SelectedValue;
            string searchName = ViewState["SearchName"] != null ? ViewState["SearchName"].ToString() : string.Empty;
            BindShiftData(searchName, selectedDepartment);
            BindAssignData();
        }

        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString;

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    string query = "INSERT INTO ShiftAssign (EmpId, Name, ShiftDate, ShiftTime, Reason, Status) " +
                                   "VALUES (@EmpId, @Name, @ShiftDate, @ShiftTime, @Reason, @Status)";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.Add("@EmpId", SqlDbType.Int).Value = ddlempname.SelectedValue;
                        cmd.Parameters.Add("@Name", SqlDbType.VarChar, 100).Value = ddlempname.SelectedItem.Text;

                        // Validate and parse date
                        DateTime shiftDate;
                        if (!DateTime.TryParse(txtdate.Text, out shiftDate))
                        {
                            Response.Write("<script>alert('Invalid date format.')</script>");
                            return;
                        }
                        cmd.Parameters.Add("@ShiftDate", SqlDbType.DateTime).Value = shiftDate;

                        // Validate and format time if needed
                        cmd.Parameters.Add("@ShiftTime", SqlDbType.VarChar, 50).Value = txttime.Text;
                        cmd.Parameters.Add("@Reason", SqlDbType.VarChar, -1).Value = txtreason.Text;
                        cmd.Parameters.Add("@Status", SqlDbType.Int).Value = 1;

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "ToastSuccess", "showToastMessage('success', 'Shift assigned successfully.');", true);
                            txtdate.Text = "";
                            txtreason.Text = "";
                            ddlempname.ClearSelection();
                            txttime.Text = "";
                            BindShiftData();
                            BindAssignData();
                        }
                        else
                        {
                            Response.Write("<script>alert('Failed to assign shift.')</script>");
                            txtdate.Text = "";
                            txtreason.Text = "";
                            ddlempname.ClearSelection();
                            txttime.Text = "";
                            BindShiftData();
                            BindAssignData();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception (you might want to log this to a file or database)
                Response.Write("<script>alert('An error occurred: " + ex.Message + "')</script>");
            }
        }

        protected void btneditassignshift_Click(object sender, EventArgs e)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString;
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "UPDATE ShiftAssign SET ShiftDate = @NewShiftDate,ShiftTime=@ShiftTime WHERE ShiftDate = @OldShiftDate AND EmpId = @EmpId";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        con.Open();
                        cmd.Parameters.AddWithValue("@NewShiftDate", TextBox12.Text);
                        cmd.Parameters.AddWithValue("@ShiftTime", TextBox13.Text);
                        cmd.Parameters.AddWithValue("@OldShiftDate", HiddenField1.Value);  
                        cmd.Parameters.AddWithValue("@EmpId", HiddenField2.Value);
                        int i = cmd.ExecuteNonQuery();
                        if (i > 0)
                        {
                            Response.Write("<script>alert('Updated Successfully...')</script>");
                            BindShiftData();
                        }
                        else
                        {
                            Response.Write("<script>alert('Failed...')</script>");
                            BindShiftData();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error: " + ex.Message);
            }
        }

        public void BindAssignData()
        {
            try
            {
                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    string query = "SELECT sa.*, e.Image FROM ShiftAssign sa INNER JOIN Employees e ON sa.EmpId = e.EmpId WHERE sa.Status = 1";
                    using (SqlCommand sqlcmd = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        SqlDataAdapter da1 = new SqlDataAdapter(sqlcmd);
                        DataSet ds1 = new DataSet();
                        da1.Fill(ds1);
                        AssignData.Controls.Clear();
                        if (ds1.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow row in ds1.Tables[0].Rows)
                            {
                                int Id = row["Id"] != DBNull.Value ? Convert.ToInt32(row["Id"]) : 0;
                                int EmpId = row["EmpId"] != DBNull.Value ? Convert.ToInt32(row["EmpId"]) : 0;
                                string Name = row["Name"] != DBNull.Value ? row["Name"].ToString() : string.Empty;
                                DateTime dt = row["ShiftDate"] != DBNull.Value ? Convert.ToDateTime(row["ShiftDate"]) : DateTime.MinValue;
                                string date = dt.ToString("yyyy-MM-dd");
                                string ShiftTime = row["ShiftTime"] != DBNull.Value ? row["ShiftTime"].ToString() : string.Empty;
                                object imageDataObj = row["Image"];
                                byte[] imageData = null;

                                // Handle image data
                                if (imageDataObj != DBNull.Value)
                                {
                                    imageData = (byte[])imageDataObj;
                                }
                                else
                                {
                                    imageData = new byte[0];
                                }
                                string base64String = Convert.ToBase64String(imageData);
                                string imageUrl = "data:image/jpeg;base64," + base64String;
                                StringBuilder projectHtml = new StringBuilder();
                                projectHtml.Append("<tr>");
                                if (imageData.Length > 0)
                                {
                                    projectHtml.Append("<td><img src='" + imageUrl + "' alt='Employee Image' style='width:50px;height:50px;'/></td>");
                                }
                                else
                                {
                                    // Placeholder image (if no image data)
                                    projectHtml.Append("<td><img src='Images/noimage.png' alt='img' style='width:50px;height:50px;border-radius:50%;'/></td>");
                                }

                                projectHtml.Append("<td>" + EmpId + "</td>");
                                projectHtml.Append("<td>" + Name + "</td>");
                                projectHtml.Append("<td>" + date + "</td>");
                                projectHtml.Append("<td>" + ShiftTime + "</td>");
                                projectHtml.Append("<td><a class='edit' href='#' data-bs-toggle='modal' data-bs-target='#edit_assignshift' onclick =\"editAssignShift('" + Id + "','" + EmpId + "','" + Name + "','" + date + "','" + ShiftTime + "')\" ><i class='fa-solid fa-pencil m-r-5' style='color:green;'></i></a></td>");
                                projectHtml.Append("<td><a class='delete' href='#' data-bs-toggle='modal' data-bs-target='#delete_assignshift' onclick =\"deleteAssignShift('" + Id + "','" + EmpId + "','" + Name + "','"+ date + "')\"><i class='fa-regular fa-trash-can m-r-5' style='color:red;'></i></a></td>");
                                projectHtml.Append("</tr>");
                                AssignData.Controls.Add(new LiteralControl(projectHtml.ToString()));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error: " + ex.Message);
                throw ex;
            }

        }

        protected void btnupdateasshift_Click(object sender, EventArgs e)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString;
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "UPDATE ShiftAssign SET ShiftDate = @NewShiftDate, ShiftTime = @ShiftTime WHERE Id = @Id AND EmpId = @EmpId";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        con.Open();
                        cmd.Parameters.Add("@NewShiftDate", SqlDbType.DateTime).Value = Convert.ToDateTime(txtassdate.Text); // Convert to DateTime
                        cmd.Parameters.Add("@ShiftTime", SqlDbType.VarChar).Value = txtasstime.Text; // Assuming ShiftTime is a string
                        cmd.Parameters.Add("@Id", SqlDbType.Int).Value = Convert.ToInt32(HiddenField4.Value); // Convert to integer
                        cmd.Parameters.Add("@EmpId", SqlDbType.Int).Value = Convert.ToInt32(HiddenField5.Value); // Convert to integer

                        int i = cmd.ExecuteNonQuery();
                        if (i > 0)
                        {
                            Response.Write("<script>alert('Updated Successfully...')</script>");
                            BindShiftData();
                        }
                        else
                        {
                            Response.Write("<script>alert('Update Failed...')</script>");
                            BindShiftData();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error: " + ex.Message);
            }
        }

        protected void btndeleteasshift_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate HiddenField values to avoid errors
                if (int.TryParse(HiddenField4.Value, out int id) && int.TryParse(HiddenField5.Value, out int empId))
                {
                    string connectionString = ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString;
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        string query = "UPDATE ShiftAssign SET Status = 0 WHERE Id = @Id AND EmpId = @EmpId";
                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            con.Open();

                            // Assign parameters using the validated values
                            cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;
                            cmd.Parameters.Add("@EmpId", SqlDbType.Int).Value = empId;

                            int i = cmd.ExecuteNonQuery();
                            if (i > 0)
                            {
                                // Use ScriptManager to show client-side alerts and better user feedback
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Deleted Successfully...');", true);
                                BindShiftData(); // Refresh the grid or data
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Deletion Failed...');", true);
                                BindShiftData(); // Refresh the grid or data even in failure
                            }
                        }
                    }
                }
                else
                {
                    // Handle invalid input scenario
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Invalid Employee or Shift ID');", true);
                }
            }
            catch (Exception ex)
            {
                // Log the error message to debug output and consider logging it to a file or database
                System.Diagnostics.Debug.WriteLine("Error: " + ex.Message);

                // Optionally, show a more user-friendly message to the user
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('An error occurred while processing the request.');", true);
            }
        }
    }
}