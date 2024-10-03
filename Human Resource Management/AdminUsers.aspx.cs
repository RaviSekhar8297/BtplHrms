using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Configuration;
using Human_Resource_Management.Roles.Employee;
using iTextSharp.text.pdf;
using DocumentFormat.OpenXml.Office.Word;
using System.Data.SqlTypes;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Net.Mail;
using System.Net;

namespace Human_Resource_Management
{
    public partial class AdminUsers : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindCompany();
                BindRolesDropdown();
                BindAllData();
            }
            if (!IsPostBack)
            {
                HiddenField1.Value = "0";
               
            }
            else
            {
                if (HiddenField1.Value == "1")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
                }
            }
        }
        public void BindCompany()
        {
            try
            {
                using (SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    using (SqlCommand sqlCmd = new SqlCommand())
                    {
                        sqlCmd.CommandText = "SELECT CompanyId,CompanyName FROM Companies ";
                        sqlCmd.Connection = sqlConn;
                        sqlConn.Open();
                        SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        ddlcompany.DataSource = dt;
                        ddlcompany.DataValueField = "CompanyId";
                        ddlcompany.DataTextField = "CompanyName";
                        ddlcompany.DataBind();

                        sqlConn.Close();

                        ddlcompany.Items.Insert(0, new System.Web.UI.WebControls.ListItem(" -- Select Company -- ", ""));
                      
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void BindRolesDropdown()
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "SELECT DISTINCT Role FROM WebLogins WHERE Status = '1'";

                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            ddlrole.Items.Clear(); // Clear existing items

                            while (reader.Read())
                            {
                                string role = reader["Role"].ToString();
                                ddlrole.Items.Add(role); // Add role to dropdown list
                            }
                        }
                    }
                }
                ddlrole.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Select Role --", ""));
            }
            catch (Exception ex)
            {
                // Handle the exception
                throw ex;
            }
        }

        private void BindAllData()
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string loggedInUserEmpId = Session["EmpId"].ToString();

                    // Initialize query to fetch data for the logged-in user
                    string query = "SELECT * FROM WebLogins WHERE EmpId = @LoggedInUserEmpId";

                    // Modify query if filters are applied
                    if (!string.IsNullOrEmpty(txtempnamesearch.Text.Trim()) ||
                        !string.IsNullOrEmpty(ddlcompany.SelectedValue) ||
                        !string.IsNullOrEmpty(ddlrole.SelectedValue))
                    {
                        query = "SELECT * FROM WebLogins WHERE Status = '1'";
                        if (!string.IsNullOrEmpty(txtempnamesearch.Text.Trim()))
                        {
                            query += " AND Name LIKE '%' + @Name + '%'";
                        }
                        if (!string.IsNullOrEmpty(ddlcompany.SelectedValue))
                        {
                            query += " AND CompanyId = @CompanyId";
                        }
                        if (!string.IsNullOrEmpty(ddlrole.SelectedValue))
                        {
                            query += " AND Role = @RoleId";
                        }
                    }

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Add parameters based on the query
                        if (query.Contains("@LoggedInUserEmpId"))
                        {
                            command.Parameters.AddWithValue("@LoggedInUserEmpId", loggedInUserEmpId);
                        }
                        if (query.Contains("@Name"))
                        {
                            command.Parameters.AddWithValue("@Name", txtempnamesearch.Text.Trim());
                        }
                        if (query.Contains("@CompanyId"))
                        {
                            command.Parameters.AddWithValue("@CompanyId", ddlcompany.SelectedValue);
                        }
                        if (query.Contains("@RoleId"))
                        {
                            command.Parameters.AddWithValue("@RoleId", ddlrole.SelectedValue);
                        }

                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        userlogins.Controls.Clear();

                        while (reader.Read())
                        {
                            string Name = reader["Name"].ToString();
                            string Email = reader["Email"].ToString();
                            string Designation = reader["Designation"].ToString();
                            string Password = reader["Password"].ToString();
                            string Roles = reader["Role"].ToString();
                            string Company = reader["CompanyName"].ToString();
                            string Branch = reader["BranchName"].ToString();
                            string DeptName = reader["DepartmentName"].ToString();
                            string EmpId = reader["EmpId"].ToString();

                            DateTime? doj = null;
                            object createdDateObj = reader["CreatedDate"];
                            if (createdDateObj != DBNull.Value)
                            {
                                doj = Convert.ToDateTime(createdDateObj);
                            }
                            string CreatedDate = doj.HasValue ? doj.Value.ToString("yyyy-MM-dd") : string.Empty;

                            string imageUrl = null;
                            object imageDataObj = reader["Image"];
                            if (imageDataObj != DBNull.Value)
                            {
                                byte[] imageData = (byte[])imageDataObj;
                                string base64String = Convert.ToBase64String(imageData);
                                imageUrl = "data:image/jpeg;base64," + base64String;
                            }

                            string AdminStatus = reader["AdminStatus"].ToString();
                            string ManagerStatus = reader["ManagerStatus"].ToString();
                            string EmployeeStatus = reader["EmployeeStatus"].ToString();

                            string AddEmployeStatus = reader["AddEmployeStatus"].ToString();
                            string EditEmployeStatus = reader["EditEmployeStatus"].ToString();
                            string DeleteEmployeStatus = reader["DeleteEmployeStatus"].ToString();

                            string AddHolidayStatus = reader["AddHolidayStatus"].ToString();
                            string EditHolidayStatus = reader["EditHolidayStatus"].ToString();
                            string DeleteHolidayStatus = reader["DeleteHolidayStatus"].ToString();


                            string AddProject = reader["AddProject"].ToString();
                            string EditProject = reader["EditProject"].ToString();
                            string DeleteProject = reader["DeleteProject"].ToString();

                            string AddLeave = reader["AddLeave"].ToString();
                            string EditLeave = reader["EditLeave"].ToString();
                            string DeleteLeave = reader["DeleteLeave"].ToString();

                            string AddAssets = reader["AddAssets"].ToString();
                            string EditAssets = reader["EditAssets"].ToString();
                            string DeleteAssets = reader["DeleteAssets"].ToString();

                            StringBuilder projectHtml = new StringBuilder();
                            projectHtml.Append("<tr>");
                            projectHtml.Append("<td>");
                            projectHtml.Append("<h2 class='table-avatar'>");
                            projectHtml.Append("<a href='profile.html' class='avatar'><img src='" + imageUrl + "' alt='User Image'></a>");
                            projectHtml.Append("<a href='profile.html'>" + Name + "</a>");
                            projectHtml.Append("</h2>");
                            projectHtml.Append("</td>");
                            projectHtml.Append("<td style='color: blue;'>" + EmpId + "</td>");
                            projectHtml.Append("<td>" + Company + "</td>");
                            projectHtml.Append("<td>" + Password + "</td>");
                            projectHtml.Append("<td>" + CreatedDate + "</td>");
                            projectHtml.Append("<td style='color: #0c5226;height:55px;width:130px;border-radius:15px;'>");

                            switch (Roles.ToLower())
                            {
                                case "admin":
                                    projectHtml.Append("<span>Admin</span>");
                                    break;
                                case "employee":
                                    projectHtml.Append("<span>Employee</span>");
                                    break;
                                case "manager":
                                    projectHtml.Append("<span style='color: red;'>Manager</span>");
                                    break;
                                default:
                                    projectHtml.Append(Roles);
                                    break;
                            }

                            projectHtml.Append("</td>");
                            projectHtml.Append("<td><a class='dropdown-item' href='#' data-bs-toggle='modal' data-bs-target='#edit_user' onclick=\"edituserlogrole('" + EmpId + "','" + Name + "','" + Email + "','" + Designation + "','" + Password + "','" + Roles + "','" + Company + "','" + Branch + "','" + DeptName + "','" + CreatedDate + "','" + AdminStatus + "','" + ManagerStatus + "','" + EmployeeStatus + "','" + AddEmployeStatus + "','" + EditEmployeStatus + "','" + DeleteEmployeStatus + "','" + AddHolidayStatus + "','" + EditHolidayStatus + "','" + DeleteHolidayStatus + "','" + AddProject + "','" + EditProject + "','" + DeleteProject + "','" + AddLeave + "','" + EditLeave + "','" + DeleteLeave + "','" + AddAssets + "','" + EditAssets + "','" + DeleteAssets + "')\"><i class='fa-solid fa-pencil m-r-5' style='color:#057a28;'></i></a></td>");
                            projectHtml.Append("<td><a class='dropdown-item' href='#' data-bs-toggle='modal' data-bs-target='#delete_user' onclick=\"deleteuserlog('" + EmpId + "','" + Name + "')\"><i class='fa-regular fa-trash-can m-r-5' style='color:#ab0c19;'></i></a></td>");
                            projectHtml.Append("</tr>");

                            userlogins.Controls.Add(new LiteralControl(projectHtml.ToString()));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle the exception
                Response.Write("<script>alert('Error: " + ex.Message + "')</script>");
            }
        }


        protected void ddlcompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindAllData();
            //txtempnamesearch.Text = "";
            //try
            //{
            //    using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
            //    {
            //        using (SqlCommand sqlcmd = new SqlCommand("SELECT * FROM WebLogins WHERE CompanyName = @CompanyName AND Status='1'", connection))
            //        {
            //            sqlcmd.Parameters.AddWithValue("@CompanyName", ddlcompany.SelectedItem.Text.Trim());
            //            connection.Open();
            //            using (SqlDataReader reader = sqlcmd.ExecuteReader())
            //            {
            //                userlogins.Controls.Clear();
            //                while (reader.Read())
            //                {
            //                    string Name = reader["Name"].ToString();
            //                    string Email = reader["Email"].ToString();
            //                    string Designation = reader["Designation"].ToString();
            //                    string Password = reader["Password"].ToString();
            //                    string Roles = reader["Role"].ToString();
            //                    string Company = reader["CompanyName"].ToString();
            //                    string Branch = reader["BranchName"].ToString();
            //                    string DeptName = reader["DepartmentName"].ToString();
            //                    string EmpId = reader["EmpId"].ToString();

            //                    DateTime? doj = null; // Nullable DateTime
            //                    object createdDateObj = reader["CreatedDate"];
            //                    if (createdDateObj != DBNull.Value)
            //                    {
            //                        doj = Convert.ToDateTime(createdDateObj);
            //                    }
            //                    string CreatedDate = doj.HasValue ? doj.Value.ToString("yyyy-MM-dd") : string.Empty;


            //                    //byte[] imageData = (byte[])reader["Image"];
            //                    //string base64String = Convert.ToBase64String(imageData);
            //                    string imageUrl = null;
            //                    // string defaultImageUrl = "";

            //                    object imageDataObj = reader["Image"];

            //                    if (imageDataObj != DBNull.Value)
            //                    {
            //                        byte[] imageData = (byte[])imageDataObj;
            //                        string base64String = Convert.ToBase64String(imageData);
            //                        imageUrl = "data:image/jpeg;base64," + base64String;
            //                        // Use 'imageUrl' as needed
            //                    }
            //                    else
            //                    {
            //                        // Handle the case where the database field is NULL
            //                        //  defaultImageUrl = "path/to/default/image.jpg";
            //                        // Proceed with defaultImageUrl or handle accordingly
            //                    }



            //                    // acces check boxes
            //                    string AdminStatus = reader["AdminStatus"].ToString();
            //                    string ManagerStatus = reader["ManagerStatus"].ToString();
            //                    string EmployeeStatus = reader["EmployeeStatus"].ToString();

            //                    string AddEmployeStatus = reader["AddEmployeStatus"].ToString();
            //                    string EditEmployeStatus = reader["EditEmployeStatus"].ToString();
            //                    string DeleteEmployeStatus = reader["DeleteEmployeStatus"].ToString();

            //                    string AddHolidayStatus = reader["AddHolidayStatus"].ToString();
            //                    string EditHolidayStatus = reader["EditHolidayStatus"].ToString();
            //                    string DeleteHolidayStatus = reader["DeleteHolidayStatus"].ToString();


            //                    string AddProject = reader["AddProject"].ToString();
            //                    string EditProject = reader["EditProject"].ToString();
            //                    string DeleteProject = reader["DeleteProject"].ToString();

            //                    string AddLeave = reader["AddLeave"].ToString();
            //                    string EditLeave = reader["EditLeave"].ToString();
            //                    string DeleteLeave = reader["DeleteLeave"].ToString();

            //                    string AddAssets = reader["AddAssets"].ToString();
            //                    string EditAssets = reader["EditAssets"].ToString();
            //                    string DeleteAssets = reader["DeleteAssets"].ToString();

            //                    StringBuilder projectHtml = new StringBuilder();
            //                    projectHtml.Append("<tr>");
            //                    projectHtml.Append("<td>");
            //                    projectHtml.Append("<h2 class='table - avatar'>");
            //                    projectHtml.Append("<a href='profile.html' class='avatar'><img src=" + imageUrl + " alt='User Image'></a>");
            //                    projectHtml.Append("<a href='profile.html'>" + Name + " </a>");
            //                    projectHtml.Append("</h2>");
            //                    projectHtml.Append("<td>");
            //                    projectHtml.Append("<td>");
            //                    projectHtml.Append("</td>");
            //                    projectHtml.Append("<td style='color:blue;'>" + EmpId + "</td>");
            //                    projectHtml.Append("<td>" + Company + "</td>");
            //                    projectHtml.Append("<td>" + Password + "</td>");
            //                    projectHtml.Append("<td>" + CreatedDate + "</td>");

            //                    projectHtml.Append("<td style='color: #0c5226;height:55px;width:130px;border-radius:15px;'>");


            //                    switch (Roles.ToLower())
            //                    {
            //                        case "Admin":
            //                            projectHtml.Append("<span style='color: blue;'>Administrator</span>");
            //                            break;
            //                        case "Employee":
            //                            projectHtml.Append("<span style='color: deeppink;border:2px solid black;height:55px !important;width:130px;border-radius:15px;'>Individual</span>");
            //                            break;
            //                        case "Manager":
            //                            projectHtml.Append("<span style='color: red;'>Manager</span>");
            //                            break;
            //                        default:
            //                            projectHtml.Append(Roles);
            //                            break;
            //                    }

            //                    projectHtml.Append("</td>");


            //                    projectHtml.Append("<td><a class='dropdown - item' href='#' data-bs-toggle='modal' data-bs-target='#edit_user'onclick =\"edituserlogcompany('" + EmpId + "','" + Name + "','" + Email + "','" + Designation + "','" + Password + "','" + Roles + "','" + Company + "','" + Branch + "','" + DeptName + "','" + CreatedDate + "','" + AdminStatus + "','" + ManagerStatus + "','" + EmployeeStatus + "','" + AddEmployeStatus + "','" + EditEmployeStatus + "','" + DeleteEmployeStatus + "','" + AddHolidayStatus + "','" + EditHolidayStatus + "','" + DeleteHolidayStatus + "','" + AddProject + "','" + EditProject + "','" + DeleteProject + "','" + AddLeave + "','" + EditLeave + "','" + DeleteLeave + "','" + AddAssets + "','" + EditAssets + "','" + DeleteAssets + "')\" ><i class='fa-solid fa-pencil m-r-5' style='color:#057a28;'></i></a></td>");
            //                    projectHtml.Append("<td><a class='dropdown - item' href='#' data-bs-toggle='modal' data-bs-target='#delete_employee'><i class='fa-regular fa-trash-can m-r-5' style='color:#ab0c19;'></i></a></td>");
            //                    projectHtml.Append("</tr>");

            //                    userlogins.Controls.Add(new LiteralControl(projectHtml.ToString()));
            //                    ddlcompany.SelectedItem.Text = "";
            //                    txtempnamesearch.Text = "";
            //                }
            //            }
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
        }

      
        protected void txtempnamesearch_TextChanged(object sender, EventArgs e)
        {
            BindAllData();
            //ddlcompany.SelectedItem.Text = "";
            //try
            //{
            //    String connstrg = ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString;
            //    using (SqlConnection sqlConn = new SqlConnection(connstrg))
            //    {
            //        using (SqlCommand sqlCmd = new SqlCommand())
            //        {
            //            sqlCmd.CommandText = "SELECT * FROM WebLogins WHERE Name LIKE '%' + @EmployeeName + '%' and status='1' ";
            //            sqlCmd.Parameters.AddWithValue("@EmployeeName", txtempnamesearch.Text.Trim());
            //            sqlCmd.Connection = sqlConn;
            //            sqlConn.Open();
            //            using (SqlDataReader reader = sqlCmd.ExecuteReader())
            //            {
            //                userlogins.Controls.Clear();
            //                while (reader.Read())
            //                {
            //                    string Name = reader["Name"].ToString();
            //                    string Email = reader["Email"].ToString();
            //                    string Designation = reader["Designation"].ToString();
            //                    string Password = reader["Password"].ToString();
            //                    string Roles = reader["Role"].ToString();
            //                    string Company = reader["CompanyName"].ToString();
            //                    string Branch = reader["BranchName"].ToString();
            //                    string DeptName = reader["DepartmentName"].ToString();
            //                    string EmpId = reader["EmpId"].ToString();

            //                    DateTime? doj = null; // Nullable DateTime
            //                    object createdDateObj = reader["CreatedDate"];
            //                    if (createdDateObj != DBNull.Value)
            //                    {
            //                        doj = Convert.ToDateTime(createdDateObj);
            //                    }
            //                    string CreatedDate = doj.HasValue ? doj.Value.ToString("yyyy-MM-dd") : string.Empty;


            //                    //byte[] imageData = (byte[])reader["Image"];
            //                    //string base64String = Convert.ToBase64String(imageData);
            //                    string imageUrl = null;
            //                    // string defaultImageUrl = "";

            //                    object imageDataObj = reader["Image"];

            //                    if (imageDataObj != DBNull.Value)
            //                    {
            //                        byte[] imageData = (byte[])imageDataObj;
            //                        string base64String = Convert.ToBase64String(imageData);
            //                        imageUrl = "data:image/jpeg;base64," + base64String;
            //                        // Use 'imageUrl' as needed
            //                    }
            //                    else
            //                    {
            //                        // Handle the case where the database field is NULL
            //                        //  defaultImageUrl = "path/to/default/image.jpg";
            //                        // Proceed with defaultImageUrl or handle accordingly
            //                    }



            //                    // acces check boxes
            //                    string AdminStatus = reader["AdminStatus"].ToString();
            //                    string ManagerStatus = reader["ManagerStatus"].ToString();
            //                    string EmployeeStatus = reader["EmployeeStatus"].ToString();

            //                    string AddEmployeStatus = reader["AddEmployeStatus"].ToString();
            //                    string EditEmployeStatus = reader["EditEmployeStatus"].ToString();
            //                    string DeleteEmployeStatus = reader["DeleteEmployeStatus"].ToString();

            //                    string AddHolidayStatus = reader["AddHolidayStatus"].ToString();
            //                    string EditHolidayStatus = reader["EditHolidayStatus"].ToString();
            //                    string DeleteHolidayStatus = reader["DeleteHolidayStatus"].ToString();


            //                    string AddProject = reader["AddProject"].ToString();
            //                    string EditProject = reader["EditProject"].ToString();
            //                    string DeleteProject = reader["DeleteProject"].ToString();

            //                    string AddLeave = reader["AddLeave"].ToString();
            //                    string EditLeave = reader["EditLeave"].ToString();
            //                    string DeleteLeave = reader["DeleteLeave"].ToString();

            //                    string AddAssets = reader["AddAssets"].ToString();
            //                    string EditAssets = reader["EditAssets"].ToString();
            //                    string DeleteAssets = reader["DeleteAssets"].ToString();

            //                    StringBuilder projectHtml = new StringBuilder();
            //                    projectHtml.Append("<tr>");
            //                    projectHtml.Append("<td>");
            //                    projectHtml.Append("<h2 class='table - avatar'>");
            //                    projectHtml.Append("<a href='profile.html' class='avatar'><img src=" + imageUrl + " alt='User Image'></a>");
            //                    projectHtml.Append("<a href='profile.html'>" + Name + " </a>");
            //                    projectHtml.Append("</h2>");
            //                    projectHtml.Append("<td>");
            //                    projectHtml.Append("<td>");
            //                    projectHtml.Append("</td>");
            //                    projectHtml.Append("<td style='color:blue;'>" + EmpId + "</td>");
            //                    projectHtml.Append("<td>" + Company + "</td>");
            //                    projectHtml.Append("<td>" + Password + "</td>");
            //                    projectHtml.Append("<td>" + CreatedDate + "</td>");

            //                    projectHtml.Append("<td style='color: #0c5226;height:55px;width:130px;border-radius:15px;'>");


            //                    switch (Roles.ToLower())
            //                    {
            //                        case "Admin":
            //                            projectHtml.Append("<span style='color: blue;'>Administrator</span>");
            //                            break;
            //                        case "Employee":
            //                            projectHtml.Append("<span style='color: deeppink;border:2px solid black;height:55px !important;width:130px;border-radius:15px;'>Individual</span>");
            //                            break;
            //                        case "Manager":
            //                            projectHtml.Append("<span style='color: red;'>Manager</span>");
            //                            break;
            //                        default:
            //                            projectHtml.Append(Roles);
            //                            break;
            //                    }

            //                    projectHtml.Append("</td>");


            //                    projectHtml.Append("<td><a class='dropdown - item' href='#' data-bs-toggle='modal' data-bs-target='#edit_user'onclick =\"edituserlogname('" + EmpId + "','" + Name + "','" + Email + "','" + Designation + "','" + Password + "','" + Roles + "','" + Company + "','" + Branch + "','" + DeptName + "','" + CreatedDate + "','" + AdminStatus + "','" + ManagerStatus + "','" + EmployeeStatus + "','" + AddEmployeStatus + "','" + EditEmployeStatus + "','" + DeleteEmployeStatus + "','" + AddHolidayStatus + "','" + EditHolidayStatus + "','" + DeleteHolidayStatus + "','" + AddProject + "','" + EditProject + "','" + DeleteProject + "','" + AddLeave + "','" + EditLeave + "','" + DeleteLeave + "','" + AddAssets + "','" + EditAssets + "','" + DeleteAssets + "')\" ><i class='fa-solid fa-pencil m-r-5' style='color:#057a28;'></i></a></td>");
            //                    projectHtml.Append("<td><a class='dropdown - item' href='#' data-bs-toggle='modal' data-bs-target='#delete_employee'><i class='fa-regular fa-trash-can m-r-5' style='color:#ab0c19;'></i></a></td>");
            //                    projectHtml.Append("</tr>");

            //                    userlogins.Controls.Add(new LiteralControl(projectHtml.ToString()));
            //                    ddlcompany.SelectedItem.Text = "";
            //                    txtempnamesearch.Text = "";
            //                }
            //            }

            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
        }

        protected void ddlrole_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindAllData();
            //try
            //{
            //    string selectedRole = ddlrole.SelectedItem.Text.Trim();

            //    string connectionString = ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString;

            //    using (SqlConnection connection = new SqlConnection(connectionString))
            //    {
            //        using (SqlCommand command = new SqlCommand())
            //        {
            //            command.Connection = connection;
            //            command.CommandText = "SELECT * FROM WebLogins WHERE Role = @Roles AND Status = '1' ORDER BY Name";
            //            command.Parameters.AddWithValue("@Roles", selectedRole);
            //            connection.Open();
            //            using (SqlDataReader reader = command.ExecuteReader())
            //            {
            //                userlogins.Controls.Clear();
            //                while (reader.Read())
            //                {
            //                    string Name = reader["Name"].ToString();
            //                    string Email = reader["Email"].ToString();
            //                    string Designation = reader["Designation"].ToString();
            //                    string Password = reader["Password"].ToString();
            //                    string Roles = reader["Role"].ToString();
            //                    string Company = reader["CompanyName"].ToString();
            //                    string Branch = reader["BranchName"].ToString();
            //                    string DeptName = reader["DepartmentName"].ToString();
            //                    string EmpId = reader["EmpId"].ToString();

            //                    DateTime? doj = null; // Nullable DateTime
            //                    object createdDateObj = reader["CreatedDate"];
            //                    if (createdDateObj != DBNull.Value)
            //                    {
            //                        doj = Convert.ToDateTime(createdDateObj);
            //                    }
            //                    string CreatedDate = doj.HasValue ? doj.Value.ToString("yyyy-MM-dd") : string.Empty;


            //                    //byte[] imageData = (byte[])reader["Image"];
            //                    //string base64String = Convert.ToBase64String(imageData);
            //                    string imageUrl = null;
            //                    // string defaultImageUrl = "";

            //                    object imageDataObj = reader["Image"];

            //                    if (imageDataObj != DBNull.Value)
            //                    {
            //                        byte[] imageData = (byte[])imageDataObj;
            //                        string base64String = Convert.ToBase64String(imageData);
            //                        imageUrl = "data:image/jpeg;base64," + base64String;
            //                        // Use 'imageUrl' as needed
            //                    }
            //                    else
            //                    {
            //                        // Handle the case where the database field is NULL
            //                        //  defaultImageUrl = "path/to/default/image.jpg";
            //                        // Proceed with defaultImageUrl or handle accordingly
            //                    }



            //                    // acces check boxes
            //                    string AdminStatus = reader["AdminStatus"].ToString();
            //                    string ManagerStatus = reader["ManagerStatus"].ToString();
            //                    string EmployeeStatus = reader["EmployeeStatus"].ToString();

            //                    string AddEmployeStatus = reader["AddEmployeStatus"].ToString();
            //                    string EditEmployeStatus = reader["EditEmployeStatus"].ToString();
            //                    string DeleteEmployeStatus = reader["DeleteEmployeStatus"].ToString();

            //                    string AddHolidayStatus = reader["AddHolidayStatus"].ToString();
            //                    string EditHolidayStatus = reader["EditHolidayStatus"].ToString();
            //                    string DeleteHolidayStatus = reader["DeleteHolidayStatus"].ToString();


            //                    string AddProject = reader["AddProject"].ToString();
            //                    string EditProject = reader["EditProject"].ToString();
            //                    string DeleteProject = reader["DeleteProject"].ToString();

            //                    string AddLeave = reader["AddLeave"].ToString();
            //                    string EditLeave = reader["EditLeave"].ToString();
            //                    string DeleteLeave = reader["DeleteLeave"].ToString();

            //                    string AddAssets = reader["AddAssets"].ToString();
            //                    string EditAssets = reader["EditAssets"].ToString();
            //                    string DeleteAssets = reader["DeleteAssets"].ToString();

            //                    StringBuilder projectHtml = new StringBuilder();
            //                    projectHtml.Append("<tr>");
            //                    projectHtml.Append("<td>");
            //                    projectHtml.Append("<h2 class='table-avatar'>");
            //                    projectHtml.Append("<a href='profile.html' class='avatar'><img src='" + imageUrl + "' alt='User Image'></a>");
            //                    projectHtml.Append("<a href='profile.html'>" + Name + "</a>");
            //                    projectHtml.Append("</h2>");
            //                    projectHtml.Append("</td>");
            //                    projectHtml.Append("<td style='color: blue;'>" + EmpId + "</td>");
            //                    projectHtml.Append("<td>" + Company + "</td>");
            //                    projectHtml.Append("<td>" + Password + "</td>");
            //                    projectHtml.Append("<td>" + CreatedDate + "</td>");
            //                    projectHtml.Append("<td>" + Roles + "</td>");

            //                    //projectHtml.Append("<td style='color: #0c5226;height:55px;width:130px;border-radius:15px;'>");


            //                    //switch (Roles.ToLower())
            //                    //{
            //                    //    case "Admin":
            //                    //        projectHtml.Append("<span style='color: blue;'>Admin</span>");
            //                    //        break;
            //                    //    case "Employee":
            //                    //        projectHtml.Append("<span style='color: deeppink;border:2px solid black;height:55px !important;width:130px;border-radius:15px;'>Employee</span>");
            //                    //        break;
            //                    //    case "Manager":
            //                    //        projectHtml.Append("<span style='color: red;'>Manager</span>");
            //                    //        break;
            //                    //    default:
            //                    //        projectHtml.Append(Roles);
            //                    //        break;
            //                    //}

            //                    //projectHtml.Append("</td>");


            //                    projectHtml.Append("<td><a class='dropdown - item' href='#' data-bs-toggle='modal' data-bs-target='#edit_user'onclick =\"edituserlogrole('" + EmpId + "','" + Name + "','" + Email + "','" + Designation + "','" + Password + "','" + Roles + "','" + Company + "','" + Branch + "','" + DeptName + "','" + CreatedDate + "','" + AdminStatus + "','" + ManagerStatus + "','" + EmployeeStatus + "','" + AddEmployeStatus + "','" + EditEmployeStatus + "','" + DeleteEmployeStatus + "','" + AddHolidayStatus + "','" + EditHolidayStatus + "','" + DeleteHolidayStatus + "','" + AddProject + "','" + EditProject + "','" + DeleteProject + "','" + AddLeave + "','" + EditLeave + "','" + DeleteLeave + "','" + AddAssets + "','" + EditAssets + "','" + DeleteAssets + "')\" ><i class='fa-solid fa-pencil m-r-5' style='color:#057a28;'></i></a></td>");
            //                    projectHtml.Append("<td><a class='dropdown - item' href='#' data-bs-toggle='modal' data-bs-target='#delete_employee' onclick=\"deleteuserlog('" + EmpId + "','" + Name + "')\"><i class='fa-regular fa-trash-can m-r-5' style='color:#ab0c19;'></i></a></td>");
            //                    projectHtml.Append("</tr>");

            //                    userlogins.Controls.Add(new LiteralControl(projectHtml.ToString()));
            //                }
            //            }
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    // Handle the exception
            //    throw ex;
            //}

        }

        protected void btndeleteuser_Click(object sender, EventArgs e)
        {
            try
            {
                String connstrg = ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString;
                using (SqlConnection sqlConn = new SqlConnection(connstrg))
                {
                    sqlConn.Open();
                    using (SqlCommand sqlCmd = new SqlCommand("UPDATE WebLogins SET Status='0' WHERE EmpId=@EmpId", sqlConn))
                    {
                        sqlCmd.Parameters.AddWithValue("@EmpId", HiddenField3.Value);
                        int i = sqlCmd.ExecuteNonQuery();
                        if (i > 0)
                        {
                            Response.Write("<script>alert('Deleted Successfully...')</script>");
                            ddlcompany.ClearSelection();
                            ddlrole.ClearSelection();
                            BindAllData();
                        }
                        else
                        {
                            Response.Write("<script>alert('Failed...')</script>");
                            ddlcompany.ClearSelection();
                            ddlrole.ClearSelection();
                            BindAllData();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

       

        private void SendEmail(string toEmail, string loginLink, string empId, string password)
        {
            try
            {
                    string fromEmail = "software.trainee1@brihaspathi.com"; // Replace with your email address
                    string fromPassword = "RAVI8297"; // Replace with your email password
                    string subject = "Your Login Details";
                    string body = $@"
                        <p>Dear Employee,</p>
                        <p>Your login details are as follows:</p>
                        <p>Employee ID: {empId}</p>
                        <p>Password: {password}</p>
                        <p>Please click the following link to login:</p>
                        <p><a href='{loginLink}'>Login</a></p>
                        <p>This link is valid for one-time use only and will expire in one hour.</p>
                        <p>Best regards,<br />Your Company Name</p>";

                    MailMessage message = new MailMessage();
                    message.From = new MailAddress(fromEmail);
                    message.To.Add(new MailAddress(toEmail));
                    message.Subject = subject;
                    message.Body = body;
                    message.IsBodyHtml = true;

                    SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587); // Replace with your SMTP server and port
                    smtp.Credentials = new NetworkCredential(fromEmail, fromPassword);
                    smtp.EnableSsl = true;
                    smtp.Send(message);

              
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error sending email: " + ex.Message + "');</script>");
            }
        }


        protected void BindCheckboxes()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString;
            string query = "SELECT * FROM WebLogins where   Status='1'  "; //and  EmpId='" + txtempid.Text + "'

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {

                        ckbadminstatus.Checked = ConvertToBoolean(reader["AdminStatus"]);
                        ckbmanagerstatus.Checked = ConvertToBoolean(reader["ManagerStatus"]);
                        ckbemployeestatus.Checked = ConvertToBoolean(reader["EmployeeStatus"]);

                        ckbaddemployee.Checked = ConvertToBoolean(reader["AddEmployeStatus"]);
                        ckbeditemployee.Checked = ConvertToBoolean(reader["EditEmployeStatus"]);
                        ckbdeleteemployee.Checked = ConvertToBoolean(reader["DeleteEmployeStatus"]);

                        ckbaddholiday.Checked = ConvertToBoolean(reader["AddHolidayStatus"]);
                        ckbeditholiday.Checked = ConvertToBoolean(reader["EditHolidayStatus"]);
                        ckbdeleteholiday.Checked = ConvertToBoolean(reader["DeleteHolidayStatus"]);

                        ckbaddprojects.Checked = ConvertToBoolean(reader["AddProject"]);
                        ckbeditproject.Checked = ConvertToBoolean(reader["EditProject"]);
                        ckbdeleteproject.Checked = ConvertToBoolean(reader["DeleteProject"]);

                        ckbaddleave.Checked = ConvertToBoolean(reader["AddLeave"]);
                        ckbeditleave.Checked = ConvertToBoolean(reader["EditLeave"]);
                        ckbdeleteleave.Checked = ConvertToBoolean(reader["deleteleave"]);

                        ckbaddassets.Checked = ConvertToBoolean(reader["AddAssets"]);
                        ckbeditassets.Checked = ConvertToBoolean(reader["DeleteAssets"]);
                        ckbdeleteassets.Checked = ConvertToBoolean(reader["EditAssets"]);
                    }

                    reader.Close();
                }
            }
        }

        private bool ConvertToBoolean(object value)
        {
            return value != null && value != DBNull.Value && bool.TryParse(value.ToString(), out bool result) && result;
        }

        protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedRole = DropDownList3.SelectedValue;

            // Reset checkboxes
            ckbadminstatus.Checked = false;
            ckbmanagerstatus.Checked = false;
            ckbemployeestatus.Checked = false;

            // Enable and check based on the selected role
            if (selectedRole == "Admin")
            {
                ckbadminstatus.Checked = true;
                ckbadminstatus.Enabled = true;
                ckbmanagerstatus.Enabled = false;
                ckbemployeestatus.Enabled = false;
            }
            else if (selectedRole == "Manager")
            {
                ckbmanagerstatus.Checked = true;
                ckbmanagerstatus.Enabled = true;
                ckbadminstatus.Enabled = false;
                ckbemployeestatus.Enabled = false;
            }
            else if (selectedRole == "Employee")
            {
                ckbemployeestatus.Checked = true;
                ckbemployeestatus.Enabled = true;
                ckbadminstatus.Enabled = false;
                ckbmanagerstatus.Enabled = false;
            }
            else
            {
                // Disable all checkboxes if 'Select' is chosen
                ckbadminstatus.Enabled = false;
                ckbmanagerstatus.Enabled = false;
                ckbemployeestatus.Enabled = false;
            }

            HiddenField1.Value = "1";
        }

        protected void userupdatebtnclick_Click(object sender, EventArgs e)
        {
            try
            {
                string connstrg = ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString;
                using (SqlConnection sqlConn = new SqlConnection(connstrg))
                {
                    sqlConn.Open();
                    using (SqlCommand sqlCmd = new SqlCommand("UPDATE WebLogins SET Role=@Role,Password=@Password, AdminStatus=@AdminStatus, ManagerStatus=@ManagerStatus, EmployeeStatus=@EmployeeStatus, AddEmployeStatus=@AddEmployeStatus, EditEmployeStatus=@EditEmployeStatus, DeleteEmployeStatus=@DeleteEmployeStatus, " +
                        "AddHolidayStatus=@AddHolidayStatus, EditHolidayStatus=@EditHolidayStatus, DeleteHolidayStatus=@DeleteHolidayStatus, AddProject=@AddProject, EditProject=@EditProject, DeleteProject=@DeleteProject, " +
                        "EditLeave=@EditLeave, DeleteLeave=@DeleteLeave, AddLeave=@AddLeave, AddAssets=@AddAssets, DeleteAssets=@DeleteAssets, EditAssets=@EditAssets WHERE EmpId=@EmpId and Role=@Role", sqlConn))
                    {
                        sqlCmd.Parameters.AddWithValue("@Role", DropDownList3.SelectedItem.Text);
                        sqlCmd.Parameters.AddWithValue("@Password", txtpassword.Text);
                        sqlCmd.Parameters.AddWithValue("@AdminStatus", ckbadminstatus.Checked.ToString()); 
                        sqlCmd.Parameters.AddWithValue("@ManagerStatus", ckbmanagerstatus.Checked.ToString());
                        sqlCmd.Parameters.AddWithValue("@EmployeeStatus", ckbemployeestatus.Checked.ToString());

                        sqlCmd.Parameters.AddWithValue("@AddEmployeStatus", ckbaddemployee.Checked.ToString());
                        sqlCmd.Parameters.AddWithValue("@EditEmployeStatus", ckbeditemployee.Checked.ToString());
                        sqlCmd.Parameters.AddWithValue("@DeleteEmployeStatus", ckbdeleteemployee.Checked.ToString());

                        sqlCmd.Parameters.AddWithValue("@AddHolidayStatus", ckbaddholiday.Checked.ToString());
                        sqlCmd.Parameters.AddWithValue("@EditHolidayStatus", ckbeditholiday.Checked.ToString());
                        sqlCmd.Parameters.AddWithValue("@DeleteHolidayStatus", ckbdeleteholiday.Checked.ToString());

                        sqlCmd.Parameters.AddWithValue("@AddProject", ckbaddprojects.Checked.ToString());
                        sqlCmd.Parameters.AddWithValue("@EditProject", ckbeditproject.Checked.ToString());
                        sqlCmd.Parameters.AddWithValue("@DeleteProject", ckbdeleteproject.Checked.ToString());

                        sqlCmd.Parameters.AddWithValue("@EditLeave", ckbeditleave.Checked.ToString());
                        sqlCmd.Parameters.AddWithValue("@DeleteLeave", ckbdeleteleave.Checked.ToString());
                        sqlCmd.Parameters.AddWithValue("@AddLeave", ckbaddleave.Checked.ToString());

                        sqlCmd.Parameters.AddWithValue("@AddAssets", ckbaddassets.Checked.ToString());
                        sqlCmd.Parameters.AddWithValue("@DeleteAssets", ckbdeleteassets.Checked.ToString());
                        sqlCmd.Parameters.AddWithValue("@EditAssets", ckbeditassets.Checked.ToString());

                        sqlCmd.Parameters.AddWithValue("@EmpId", HiddenField2.Value);

                        int i = sqlCmd.ExecuteNonQuery();
                        if (i > 0)
                        {
                            Response.Write("<script>alert('Accesses Updated Successfully...')</script>");
                            BindAllData();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error: " + ex.Message + "')</script>");
            }
        }

    }
}