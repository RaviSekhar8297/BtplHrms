using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Human_Resource_Management
{
    public partial class AdminTermination : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                TerminationEmployees();
            }
        }
        public void TerminationEmployees()
        {
            try
            {
                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    using (SqlCommand sqlcmd = new SqlCommand("SELECT * FROM TerminationEmployees ", connection))
                    {
                        connection.Open();
                        SqlDataAdapter da1 = new SqlDataAdapter(sqlcmd);
                        DataSet ds1 = new DataSet();
                        da1.Fill(ds1);
                        if (ds1.Tables[0].Rows.Count > 0)
                        {
                            int S_No = 1;
                            foreach (DataRow row in ds1.Tables[0].Rows)
                            {
                                string Id = row["Id"].ToString();
                                string Name = row["Name"].ToString();
                                string Department = row["Department"].ToString();
                               
                               
                                DateTime? TerminationDate1 = row["TerminationDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(row["TerminationDate"]);
                                string TerminationDate = TerminationDate1.HasValue ? TerminationDate1.Value.ToString("yyyy-MM-dd") : string.Empty;

                                string Designation = row["Designation"].ToString();
                                string TerminationType = row["TerminationType"].ToString();
                                string Reason = row["Reason"].ToString();

                                StringBuilder projectHtml = new StringBuilder();

                                projectHtml.Append("<tr>");
                                projectHtml.Append("<td>" + S_No + "</td>");
                                projectHtml.Append("<td>" + Name + "</td>");
                                projectHtml.Append("<td>" + Department + "</td>");
                                projectHtml.Append("<td>" + Designation + "</td>");
                                projectHtml.Append("<td>" + TerminationType + "</td>");
                                projectHtml.Append("<td>" + TerminationDate + "</td>");
                                projectHtml.Append("<td>" + Reason + "</td>");
                               
                              //  projectHtml.Append("<td><a class='edit' href='#' data-bs-toggle='modal' data-bs-target='#edit_termination' onclick =\"editTermination('" + Id + "','" + Name + "','" + Department + "','" + Designation + "','" + TerminationType + "','" + TerminationDate + "','" + Reason + "')\" ><i class='fa-solid fa-pencil m-r-5' style='color:green;'></i></a></td>");
                              //  projectHtml.Append("<td><a class='delete ' href='#' data-bs-toggle='modal' data-bs-target='#delete_termination' onclick =\"deleteTermination('" + Id + "','" + Name + "')\"><i class='fa-regular fa-trash-can m-r-5' style='color:red;'></i></a></td>");
                                projectHtml.Append("</tr>");

                                TerminationData.Controls.Add(new LiteralControl(projectHtml.ToString()));
                                S_No++;
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
    }
}