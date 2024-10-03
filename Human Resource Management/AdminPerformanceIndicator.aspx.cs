using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Human_Resource_Management
{
    public partial class AdminPerformanceIndicator : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindReviewData();
            }
        }
        public void BindReviewData()
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
            {
                using (SqlCommand sqlcmd = new SqlCommand("SELECT * FROM EmpPerformenceDetails WHERE Year = YEAR(GETDATE()) AND Status = '1'; ", connection))

                {
                    connection.Open();
                    SqlDataAdapter da1 = new SqlDataAdapter(sqlcmd);
                    DataSet ds1 = new DataSet();
                    da1.Fill(ds1);
                    int slnumber = 1;
                    if (ds1.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow row in ds1.Tables[0].Rows)
                        {
                            string ApplyDate = row["ReviewDate"].ToString();
                            DateTime appliedDateTime = Convert.ToDateTime(ApplyDate);
                            string ReviewDate = appliedDateTime.ToString("yyyy-MM-dd");

                            string EmpId = row["EmpId"].ToString(); 
                            string ReviewedBy = row["ReviewBy"].ToString();

                            string Designation = row["Designation"].ToString();

                            string OvarallReview = row["OvarallReview"].ToString();

                            string Name = row["EmpName"].ToString();
                            string Id = row["Id"].ToString();



                            string Regular = row["Regular"].ToString();
                            string Behaviour = row["Behaviour"].ToString();
                            string Work = row["Work"].ToString();

                            StringBuilder projectHtml = new StringBuilder();


                            projectHtml.Append("</tr>");
                            projectHtml.Append("</thead>");

                            projectHtml.Append("<tbody>");
                            projectHtml.Append("<tr>");
                            projectHtml.Append("<td>" + slnumber + "</td>");
                            projectHtml.Append("<td>" + EmpId + "</td>");
                            projectHtml.Append("<td>" + Name + "</td>");
                            projectHtml.Append("<td>" + Designation + "</td>");
                            projectHtml.Append("<td>" + ReviewDate + "</td>");
                            projectHtml.Append("<td>" + OvarallReview + "</td>");
                            projectHtml.Append("<td>" + ReviewedBy + "</td>");

                            //  projectHtml.Append("<td><a class='dropdown - item' href='#' data-bs-toggle='modal' data-bs-target='#edit_leave' onclick=\"edit_leave('" + leaveID + "','" + leaveType + "','" + fromDate1 + "','" + toDate1 + "','" + numberOfDays + "','" + rs + "')\"><i class='fa-solid fa-pencil m-r-5'></i></a></td>");


                            //  projectHtml.Append("<td><a class='dropdown - item' href='#' data-bs-toggle='modal' data-bs-target='#delete_approve' onclick=\"delete_leave('" + leaveID + "')\"><i class='fa-regular fa-trash-can m-r-5'></i></a></td>");                                  

                            //projectHtml.Append("<td class='text - end'>");
                            //projectHtml.Append("<div class='dropdown dropdown - action'>");
                            //projectHtml.Append("<a href='#' class='action-icon dropdown-toggle' data-bs-toggle='dropdown' aria-expanded='false'><i class='material-icons'>more_vert</i></a>");
                            //projectHtml.Append("<div class='dropdown - menu dropdown - menu - right'>");
                            projectHtml.Append("<td><a class='dropdown - item' href='#' data-bs-toggle='modal' data-bs-target='#edit_indicator' onclick=\"editreview('" + Id + "','" + EmpId + "','" + Name + "','" + Regular + "','" + Behaviour + "','" + Work + "','" + OvarallReview + "')\"><i class='fa-solid fa-pencil m-r-5'></i></a></td>");
                            projectHtml.Append("<td><a class='dropdown - item' href='#' data-bs-toggle='modal' data-bs-target='#delete_indicator' onclick=\"deletereview('" + Id + "','" + Name + "')\"><i class='fa-regular fa-trash-can m-r-5'></i></a></td>");
                            //projectHtml.Append("</div>");
                            //projectHtml.Append("</div>");
                            //projectHtml.Append("</ td >");
                            projectHtml.Append("</tr>");
                            projectHtml.Append("</tbody>");
                            ReviewData.Controls.Add(new LiteralControl(projectHtml.ToString()));
                            slnumber++;
                        }
                    }
                    else
                    {
                        // Literal1.Text = "<div class='no-records-message'>Here no leaves Applied the month of " + currentMonthName + "...</div>";
                    }
                }
            }
        }

        protected void btnreviewdelete_Click(object sender, EventArgs e)
        {
            try
            {
                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    using (SqlCommand sqlcmd = new SqlCommand("UPDATE EmpPerformenceDetails SET Status='0' ,DeletedBy=@DeletedBy,DeletedDate=@DeletedDate WHERE Id=@Id", connection))
                    {
                        connection.Open();
                        sqlcmd.Parameters.AddWithValue("@Id", HiddenField2.Value.Trim());
                        sqlcmd.Parameters.AddWithValue("@DeletedBy", Session["Name"].ToString());
                        sqlcmd.Parameters.AddWithValue("@DeletedDate", DateTime.Now);
                        int i = sqlcmd.ExecuteNonQuery();
                        if (i > 0)
                        {
                            Response.Write("<script>alert('Deleted Successfully..')</script>");

                        }
                        else
                        {
                            Response.Write("<script>alert('Failed..')</script>");

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnupdatereview_Click(object sender, EventArgs e)
        {
            try
            {
                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    using (SqlCommand sqlcmd = new SqlCommand("UPDATE EmpPerformenceDetails SET Regular=@Regular,Behaviour=@Behaviour,Work=@Work,OvarallReview=@OvarallReview ,UpdatedBy=@UpdatedBy,UpdatedDate=@UpdatedDate WHERE Id=@Id", connection))
                    {
                        connection.Open();
                        sqlcmd.Parameters.AddWithValue("@Id", HiddenField1.Value);
                        sqlcmd.Parameters.AddWithValue("@Regular", TextBox3.Text);

                        sqlcmd.Parameters.AddWithValue("@Behaviour", TextBox4.Text);
                        sqlcmd.Parameters.AddWithValue("@Work", TextBox5.Text);
                        double ovrReview = Convert.ToDouble(TextBox3.Text + TextBox4.Text + TextBox5.Text);
                        double total = ovrReview / 15 * 100;
                        sqlcmd.Parameters.AddWithValue("@OvarallReview", total);
                        sqlcmd.Parameters.AddWithValue("@UpdatedBy", Session["Name"].ToString());
                        sqlcmd.Parameters.AddWithValue("@UpdatedDate", DateTime.Now);
                        int i = sqlcmd.ExecuteNonQuery();
                        if (i > 0)
                        {
                            Response.Write("<script>alert('Updated Successfully..')</script>");
                        }
                        else
                        {
                            Response.Write("<script>alert('Not Updated..')</script>");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void TextBox3_TextChanged(object sender, EventArgs e)
        {
            try
            {
                double num1 = Convert.ToDouble(TextBox3.Text);

                if (num1 < 0.0 || num1 > 5.0)
                {                   
                    string script = "alert('Please enter a value between 0.0 and 5.0.');";
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);
                    TextBox3.Text = "";
                    BindReviewData();
                }
            }
            catch (FormatException)
            {
                string script = "alert('Please enter a valid number.');";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);
                TextBox3.Text = "";
                BindReviewData();
            }
        }
    }
}