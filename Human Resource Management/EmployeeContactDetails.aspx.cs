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

namespace Human_Resource_Management
{
    public partial class EmployeeContactDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindCompany();
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

                        ddlcompany.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Select Company--", "0"));
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
            try
            {
                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NewHRMSString"].ConnectionString))
                {
                    using (SqlCommand sqlcmd = new SqlCommand("SELECT * FROM Employees WHERE Company = @Company and Status='1'", connection))
                    {
                        sqlcmd.Parameters.AddWithValue("@Company", ddlcompany.SelectedItem.Text.Trim());
                        connection.Open();
                        SqlDataAdapter da1 = new SqlDataAdapter(sqlcmd);
                        DataSet ds1 = new DataSet();
                        da1.Fill(ds1);
                        string EditEmpStatus = Session["EditEmployeStatus"] as string;
                        string DeleteEmpStatus = Session["DeleteEmployeStatus"] as string;
                        if (ds1.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow row in ds1.Tables[0].Rows)
                            {
                                string Name = row["LastName"].ToString();
                                string Designation = row["Designation"].ToString();
                                string Department = row["Department"].ToString();
                                string Mobile = row["CompanyCellNo"].ToString();
                                object imageDataObj = row["Image"];
                                byte[] imageData = null;

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
                                projectHtml.Append("<ul class='contact - list'>");
                                projectHtml.Append("<li>");
                                projectHtml.Append("<div class='contact - cont bor'>");
                                projectHtml.Append("<div class='float - start contact - info con'>");
                                projectHtml.Append("<span class='contact - name text - ellipsis'>" + Name + "</span>");                               
                                projectHtml.Append("<span class='contact - date'>"+ Designation + "</span>");
                                projectHtml.Append("</div>");
                                projectHtml.Append("<div class='user - img con1'>");
                                projectHtml.Append("<a href='profile.html' class='avatar'>");
                                projectHtml.Append("<img src='" + imageUrl + "' class='rounded-circle' alt='' />");
                                projectHtml.Append("<span class='status online'></span>");
                                projectHtml.Append("</a>");
                                projectHtml.Append("</div>");
                                projectHtml.Append("<div class='float - start1 contact - info con'>");
                                projectHtml.Append("<span class='contact - name text - ellipsis'>" + Department + "</span>");
                                projectHtml.Append("<span class='contact - date'>" + Mobile + "</span>");
                                projectHtml.Append("</div>");
                                projectHtml.Append("<div class='dropdown - menu dropdown - menu - right editdelete' id='drpmenu'>");
                                if (EditEmpStatus == "True")
                                {
                                    projectHtml.Append("<a class='edit' href='#' data-bs-toggle='modal' data-bs-target='#edit_employee' ><i class='fa-solid fa-pencil m-r-5'></i></a>");

                                }
                                projectHtml.Append("<br/>");
                                projectHtml.Append("<br/>");
                                if (DeleteEmpStatus == "True")
                                {
                                    projectHtml.Append("<a class='delete ' href='#' data-bs-toggle='modal' data-bs-target='#delete_employee'><i class='fa-regular fa-trash-can m-r-5'></i></a>");
                                }
                                projectHtml.Append("</div>");
                                projectHtml.Append("</div>");
                                projectHtml.Append("</li>");
                                projectHtml.Append("</ul>");

                                ContactDetails.Controls.Add(new LiteralControl(projectHtml.ToString()));
                            }
                        }
                        else
                        {
                            Response.Write("<script>alert('No row at this position...')<script>");
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