using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Text;
using System.Globalization;

namespace Human_Resource_Management
{
    public partial class ApplyLeave : System.Web.UI.Page
    {
       // private DateTime maxFromDate;
       // private double LOP;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                 LeavesCount();
                 LeavesList();            
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


                                    lblcasualleavescount.Text = balenceCasualLeaves.ToString();
                                    lblsickleavescount.Text = balenceSickLeaves.ToString();
                                    lblcompoff.Text = balenceCampOffLeaves.ToString();
                                    lbltotalleaves.Text = (balenceCasualLeaves + balenceSickLeaves + balenceCampOffLeaves).ToString();
                                }
                                else
                                {
                                    lbltotalleaves.Text = "0";
                                    lblcasualleavescount.Text = "0";
                                    lblsickleavescount.Text = "0";
                                    lblcompoff.Text = "0";
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Log the exception
                    lbltotalleaves.Text = "Error";
                    lblcasualleavescount.Text = "Error";
                    lblsickleavescount.Text = "Error";
                    lblcompoff.Text = "Error";
                    throw ex;
                }
            }

        }

        public void GetLeavesListEmp()
        {
            if (Session["EmpId"] == null || Session["EmpId"].ToString() == "")
            {

            }
            else
            {
                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    using (SqlCommand sqlcmd = new SqlCommand("SELECT * FROM Employees WHERE EmpId = @EmployeedID", connection))
                    {
                        sqlcmd.Parameters.AddWithValue("@EmployeedID", Session["EmpId"].ToString());
                        connection.Open();

                        using (SqlDataReader sdr = sqlcmd.ExecuteReader())
                        {
                            if (sdr.Read())
                            {
                                DateTime dojDate = Convert.ToDateTime(sdr["DOJ"]);
                                int month = dojDate.Month;
                                int year = dojDate.Year;
                                if(month!=0)
                                {
                                    int total = 12 - month;
                                    lbltotalleaves.Text = "12";
                                    int used = total - month;
                                    lblcasualleavescount.Text = "0";
                                    lblsickleavescount.Text = total.ToString();
                                    lblcompoff.Text = "0";
                                }
                                else
                                {
                                    LeavesCount();
                                }
                            }
                        }
                    }
                }

            }
        }

        public void LeavesList()
        {
            
            
            if (Session["EmpId"] == null || Session["EmpId"].ToString() == "")
            {

            }
            else
            {
                string currentMonthName = DateTime.Now.ToString("MMMM");
                try
                {
                    using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                    {
                        using (SqlCommand sqlcmd = new SqlCommand("SELECT * FROM LeavesStatus22 WHERE EmployeedID = @EmployeedID AND YEAR(FromDate) = YEAR(GETDATE()) AND MONTH(FromDate) = MONTH(GETDATE())", connection))

                        {
                            sqlcmd.Parameters.AddWithValue("@EmployeedID", Session["EmpId"].ToString());
                            connection.Open();
                            SqlDataAdapter da1 = new SqlDataAdapter(sqlcmd);
                            DataSet ds1 = new DataSet();
                            da1.Fill(ds1);
                            if (ds1.Tables[0].Rows.Count > 0)
                            {
                                foreach (DataRow row in ds1.Tables[0].Rows)
                                {
                                    string ApplyDate = row["AppliedDate"].ToString();
                                    DateTime appliedDateTime = Convert.ToDateTime(ApplyDate);
                                    string formattedDate = appliedDateTime.ToString("yyyy-MM-dd");

                                    string rs = row["ReasontoApply"].ToString();
                                    DateTime fromDate = Convert.ToDateTime(row["FromDate"]);
                                    string fromDate1 = fromDate.ToString("yyyy-MM-dd");

                                    DateTime toDate = Convert.ToDateTime(row["ToDate"]);
                                    string toDate1 = toDate.ToString("yyyy-MM-dd");

                                    TimeSpan timeDifference = toDate - fromDate;
                                    int numberOfDays = (int)timeDifference.TotalDays;
                                    numberOfDays++;
                                    string sr = row["StatusReason"].ToString();
                                    string sstatus = row["Status"].ToString();
                                    string appbyname = row["ApprovedByName"].ToString();
                                    string leaveType = row["leave_type"].ToString();
                                    string leaveID = row["LeaveId"].ToString();

                                    var Editleave = Session["EditLeave"] as string;
                                    var Deleteleave = Session["DeleteLeave"] as string;
                                    var Role = Session["Role"] as string;
                                    StringBuilder projectHtml = new StringBuilder();

                                   
                                    projectHtml.Append("</tr>");
                                    projectHtml.Append("</thead>");

                                    projectHtml.Append("<tbody>");
                                    projectHtml.Append("<tr>");
                                    projectHtml.Append("<td>" + formattedDate + "</td>");
                                    projectHtml.Append("<td>"+ rs + "</td>");
                                    projectHtml.Append("<td>" + fromDate1 + "</td>");
                                    projectHtml.Append("<td>" + toDate1 + "</td>");
                                    projectHtml.Append("<td>" + numberOfDays + "</td>");
                                    projectHtml.Append("<td>" + leaveType + "</td>");
                                    projectHtml.Append("<td style='color:red;'>" + sr + "</td>");
                                    projectHtml.Append("<td>" + appbyname + "</td>");

                                    if (Editleave == "True" && Role== "Employee")
                                    {
                                        projectHtml.Append("<td><a class='dropdown - item' href='#' data-bs-toggle='modal' data-bs-target='#edit_leave' onclick=\"edit_leave('" + leaveID + "','" + leaveType + "','" + fromDate1 + "','" + toDate1 + "','" + numberOfDays + "','"+ rs + "')\"><i class='fa-solid fa-pencil m-r-5'></i></a></td>");
                                    }
                                    if (Deleteleave == "True" && Role == "Employee")
                                    {
                                        projectHtml.Append("<td><a class='dropdown - item' href='#' data-bs-toggle='modal' data-bs-target='#delete_approve' onclick=\"delete_leave('" + leaveID + "')\"><i class='fa-regular fa-trash-can m-r-5'></i></a></td>");
                                    }

                                    //projectHtml.Append("<td class='text - end'>");
                                    //projectHtml.Append("<div class='dropdown dropdown - action'>");
                                    //projectHtml.Append("<a href='#' class='action-icon dropdown-toggle' data-bs-toggle='dropdown' aria-expanded='false'><i class='material-icons'>more_vert</i></a>");
                                    //projectHtml.Append("<div class='dropdown - menu dropdown - menu - right'>");
                                    //projectHtml.Append("<a class='dropdown - item' href='#' data-bs-toggle='modal' data-bs-target='#edit_leave'><i class='fa-solid fa-pencil m-r-5'></i> Edit</a>");
                                    // projectHtml.Append("<td><a class='dropdown - item' href='#' data-bs-toggle='modal' data-bs-target='#delete_approve'><i class='fa-regular fa-trash-can m-r-5'></i></a></td>");
                                    //projectHtml.Append("</div>");
                                    //projectHtml.Append("</div>");
                                    //projectHtml.Append("</ td >");
                                    projectHtml.Append("</tr>");
                                    projectHtml.Append("</tbody>");
                                    projectsContainer.Controls.Add(new LiteralControl(projectHtml.ToString()));
                                }
                            }
                            else
                            {
                                Literal1.Text = "<div class='no-records-message'>Here no leaves Applied the month of " + currentMonthName + "...</div>";
                            }
                        }
                    }
                }
                catch(Exception ex)
                {
                    throw ex;
                }
            }
        }

      

        protected void btnleavedelete_Click(object sender, EventArgs e)
        {
            try
            {
                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    using (SqlCommand sqlcmd = new SqlCommand("UPDATE LeavesStatus22 SET Status='0' WHERE LeaveId=@LeaveId", connection))
                    {
                        connection.Open();
                        sqlcmd.Parameters.AddWithValue("@LeaveId", HiddenField1.Value.Trim());
                        int i = sqlcmd.ExecuteNonQuery();
                        if (i > 0)
                        {
                            Response.Write("<script>alert('Deleted Successfully..')</script>");
                            LeavesList();
                        }
                        else
                        {
                            Response.Write("<script>alert('Not Deleted..')</script>");
                            LeavesList();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnleaveedit_Click(object sender, EventArgs e)
        {
            try
            {
                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    using (SqlCommand sqlcmd = new SqlCommand("UPDATE LeavesStatus22 SET Statu='0' WHERE LeaveId=@LeaveId", connection))
                    {
                        connection.Open();
                        sqlcmd.Parameters.AddWithValue("@LeaveId", HiddenField2.Value.Trim());
                        int i = sqlcmd.ExecuteNonQuery();
                        if (i > 0)
                        {
                            Response.Write("<script>alert('Deleted Successfully..')</script>");
                            LeavesList();
                        }
                        else
                        {
                            Response.Write("<script>alert('Not Deleted..')</script>");
                            LeavesList();
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

}

